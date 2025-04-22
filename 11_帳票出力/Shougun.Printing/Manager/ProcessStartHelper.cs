using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Shougun.Printing.Common;

namespace Shougun.Printing.Manager
{
    /// <summary>
    /// プロセス起動クラス
    /// </summary>
    public class ProcessStartHelper
    {
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// クライアント側でプロセスを起動させる
        /// </summary>
        public static void startProcess()
        {
            // ClientFilePathInfo.txtのフルパス
            string clientFilePathInfo = Path.Combine(LocalDirectories.PrintingDirectory, "ClientFilePathInfo.txt");

            if (!File.Exists(clientFilePathInfo))
            {
                return;
            }

            try
            {
                // ファイルが読み込めるようになるまで待機
                ProcessStartHelper.waitingAllowFileAccess(clientFilePathInfo);

                using (var fs = new FileStream(clientFilePathInfo, FileMode.Open,
                                    FileAccess.Read, FileShare.None))
                using (var sr = new StreamReader(fs, Encoding.UTF8))
                {
                    string strClientFilePath = sr.ReadLine();

                    // 『tsclist』を置換
                    strClientFilePath = strClientFilePath.Replace(@"\\tsclient\", "");
                    if (strClientFilePath.IndexOf(":") == -1)
                    {
                        strClientFilePath = strClientFilePath.Insert(1, ":");
                    }

                    if (File.Exists(strClientFilePath))
                    {
                        // プロセス起動
                        using (Process p = Process.Start(strClientFilePath))
                        {
                            IntPtr hWnd = p.MainWindowHandle;
                            if (hWnd != null)
                            {
                                p.WaitForInputIdle(30000);
                                SetForegroundWindow(hWnd);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                // ファイル削除、5回試す
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        if (File.Exists(clientFilePathInfo))
                        {
                            File.Delete(clientFilePathInfo);
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        System.Threading.Thread.Sleep(150);
                        continue;
                    }
                }
            }
        }

        /// <summary>					
        /// クライアント側でプロセスを起動させる					
        /// </summary>					
        public static void startProcess2()
        {
            string clientFilePathInfo2 = Path.Combine(LocalDirectories.PrintingDirectory, "ClientFilePathInfo2.txt");

            if (!File.Exists(clientFilePathInfo2))
            {
                return;
            }

            try
            {
                System.Threading.Thread.Sleep(50);

                using (var fs = new FileStream(clientFilePathInfo2, FileMode.Open,
                                    FileAccess.Read, FileShare.None))
                using (var sr = new StreamReader(fs, Encoding.UTF8))
                {
                    //URLの取得					
                    string strClientFilePath2 = sr.ReadLine();
                    //Chromで起動
                    System.Diagnostics.Process.Start(strClientFilePath2);
                }
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                // ファイル削除、5回試す					
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        if (File.Exists(clientFilePathInfo2))
                        {
                            File.Delete(clientFilePathInfo2);
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        System.Threading.Thread.Sleep(150);
                        continue;
                    }
                }
            }
        }

        private static void waitingAllowFileAccess(string filePath)
        {
            Stream st = null;

            int maxCount = 10;
            int waitTime = 50;

            for (int i = 0; i < maxCount; i++)
            {
                try
                {
                    if ((st = File.Open(filePath, FileMode.Open,
                                    FileAccess.Read, FileShare.None)) != null)
                    {
                        break;
                    }
                }
                catch
                {
                    System.Threading.Thread.Sleep(waitTime);
                }
            }

            st.Close();
            st = null;
        }

        /// <summary>
        /// クライアント側でプロセス起動
        /// </summary>
        public static void startPreviewProcess()
        {
            try
            {
                // 最新ファイル取得
                var file = new DirectoryInfo(LocalDirectories.FilePreviewDirectory).GetFiles()
                                                                                   .OrderByDescending(n => n.CreationTime)
                                                                                   .FirstOrDefault();
                if (file == null || !IsFinishedFileCreate(file.FullName, 50, 120000))
                {
                    return;
                }

                // プロセス起動
                using (Process p = Process.Start(file.FullName))
                {
                    IntPtr hWnd = p.MainWindowHandle;
                    if (hWnd != null)
                    {
                        p.WaitForInputIdle(50000);
                        SetForegroundWindow(hWnd);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 指定したファイルが読取り可能か判定
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="sleep">1回辺りのスリープ時間</param>
        /// <param name="limit">処理判定の上限時間</param>
        /// <returns></returns>
        private static bool IsFinishedFileCreate(string @path, int sleep, int limit)
        {
            if (!File.Exists(path))
            {
                return false;
            }

            int counter = GetLoopCounter(sleep, limit);
            // sleepで指定されたミリ秒だけスリープしつつlimitを超えるまで繰返し
            for (int i = 0; i < counter; i++)
            {
                try
                {
                    using (var file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        if (file != null && 0 < file.Length)
                        {
                            return true;
                        }
                        else if (file != null && file.Length <= 0)
                        {
                            if (i == (counter - 1))
                            {
                                // 最後のループで0byteの場合は中身がないと判断
                                break;
                            }
                            else
                            {
                                // 0byteの場合はコピー中の可能性があるのでリトライ
                                Thread.Sleep(sleep);
                            }
                        }
                    }
                }
                catch
                {
                    Thread.Sleep(sleep);
                }
            }

            return false;
        }

        /// <summary>
        /// ポーリングする際の上限数を取得
        /// </summary>
        /// <param name="sleep"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private static int GetLoopCounter(int sleep, int limit)
        {
            int count = 1;
            if (sleep < limit)
            {
                var no = limit / sleep;
                var rem = limit % sleep;
                if (rem != 0)
                {
                    // 端数があった場合は1回加算
                    no += 1;
                }
                count = no;
            }

            return count;
        }

        #region mapbox連携

        /// <summary>
        /// クライアント側でプロセス起動
        /// 基本はGoogleChromeで起動
        /// GoogleChromeがなければFirefoxで起動
        /// Firefoxがなければ既定のブラウザで起動
        /// </summary>
        public static void startMapProcess()
        {
            try
            {
                // 最新ファイル取得
                var file = new DirectoryInfo(LocalDirectories.MapsDirectory).GetFiles()
                                                                                   .OrderByDescending(n => n.CreationTime)
                                                                                   .FirstOrDefault();
                if (file == null || !IsFinishedFileCreate(file.FullName, 50, 120000))
                {
                    return;
                }

                DirectoryInfo dirMapInfo = new DirectoryInfo(LocalDirectories.PrintingDirectory);
                WebBrowserReader webLogic = new WebBrowserReader();

                // 既定のブラウザのパスを取得する
                List<BrowserDto> browserList = webLogic.browserInfoRead();
                BrowserDto browser = new BrowserDto();

                // GoogleChromeを探す
                browser = browserList.Find(n => n.Name == "Google Chrome");
                if (browser != null)
                {
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = browser.Path;
                    psi.Arguments = file.FullName;
                    psi.UseShellExecute = false;
                    using (Process p = Process.Start(psi)) { }
                    return;
                }

                // Firefoxを探す
                browser = browserList.Find(n => n.Name == "Mozilla Firefox");
                if (browser != null)
                {
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = browser.Path;
                    psi.Arguments = file.FullName;
                    psi.UseShellExecute = false;
                    using (Process p = Process.Start(psi)) { }
                    return;
                }
                // どちらもなければ既定のブラウザで起動
                using (Process p = Process.Start(file.FullName)) { }
            }
            catch
            {
            }
        }

        #endregion
    }
}
