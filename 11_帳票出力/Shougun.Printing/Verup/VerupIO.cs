using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Reflection;

namespace Shougun.Printing.Verup
{
    class VerupIO
    {
        public static readonly string RevisionFileName = "Shougun.Printing.Revision.txt";
        public static readonly string VerupProgramName = "Shougun.Printing.Verup.exe";
        public static readonly string ClientProgramName = "Shougun.Printing.Client.exe";
        public static readonly string VerupFlagFileame = "verup";

        /// <summary>
        /// 指定されたディレクトリの"Shougun.Printing.Revision.txt"に書いてあるリビジョン番号を取得する
        /// </summary>
        public static int ReadRevisionFile(string directory)
        {
            int revision = 0;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    string path = Path.Combine(directory, RevisionFileName);
                    if (File.Exists(path))
                    {
                        string data = File.ReadAllText(path, Encoding.UTF8);
                        revision = int.Parse(data);
                    }
                    break;
                }
                catch
                {
                    // 書き込み側がファイルクローズ間際の可能性があるので、300ms周期でリトライする
                    System.Threading.Thread.Sleep(300);
                }
            }
            return revision;
        }

        /// <summary>
        /// サーバー側アプリケーションプログラムのディレクトリを取得する
        /// </summary>
        public static string GetLocalApplicationDirectory()
        {
            var appFile = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(appFile);
            return directory;
        }

        /// <summary>
        /// サーバー側帳票設定データディレクトリを取得する。
        /// クライアント側のXPS出力ディレクトリが記載しているファイルが置いてある場所。
        /// </summary>
        public static string GetLocalReportDirectory()
        {
            var configFilePath = ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;

            var directory = Path.GetDirectoryName(configFilePath);
            directory = Path.GetDirectoryName(directory);
            directory = Path.GetDirectoryName(directory);
            directory = Path.Combine(directory, "ShougunReport");
            if (Directory.Exists(directory))
            {
                return directory;
            }
            return null;
        }


        /// <summary>
        /// サーバー側帳票設定データディレクトリを取得する。
        /// クライアント側のXPS出力ディレクトリが記載しているファイルが置いてある場所。
        /// </summary>
        public static string GetLocalVerupDirectory()
        {
            var directory = VerupIO.GetLocalReportDirectory();
            if (directory != null)
            {
                return Path.Combine(directory, "Verup");
            }
            return null;
        }


        public static string GetLocalPrintingDirectory()
        {
            var directory = VerupIO.GetLocalReportDirectory();
            if (directory != null)
            {
                return Path.Combine(directory, "Printing");
            }
            return null;
        }

        #region WTSAPI
        //"C:\Program Files\Microsoft SDKs\Windows\v7.0A\Include\WtsApi32.h"

        public static IntPtr WTS_CURRENT_SERVER_HANDLE = (IntPtr)null;
        public static int WTS_CURRENT_SESSION = -1;
        /// <summary>
        /// 指定したターミナルサーバー上の、指定したセッションの情報を取得します。。
        /// </summary>
        [DllImport("wtsapi32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern bool WTSQuerySessionInformation(
            IntPtr hServer,
            int SessionId,
            WTS_INFO_CLASS WTSInfoClass,
            out IntPtr ppBuffer,
            out uint pBytesReturned
            );

        /// <summary>
        /// ターミナルサービス関数で確保したメモリを解放します。
        /// </summary>
        /// <param name="memory"></param>
        [DllImport("wtsapi32.dll", ExactSpelling = true, SetLastError = false)]
        public static extern void WTSFreeMemory(IntPtr pMemory);

        /// <summary>
        /// WTSQuerySessionInformation で要求できる情報の種類
        /// </summary>
        public enum WTS_INFO_CLASS
        {
            WTSInitialProgram,
            WTSApplicationName,
            WTSWorkingDirectory,
            WTSOEMId,
            WTSSessionId,
            WTSUserName,
            WTSWinStationName,
            WTSDomainName,
            WTSConnectState,
            WTSClientBuildNumber,
            WTSClientName,
            WTSClientDirectory,
            WTSClientProductId,
            WTSClientHardwareId,
            WTSClientAddress,
            WTSClientDisplay,
            WTSClientProtocolType,
            WTSIdleTime,
            WTSLogonTime,
            WTSIncomingBytes,
            WTSOutgoingBytes,
            WTSIncomingFrames,
            WTSOutgoingFrames,
            WTSClientInfo,
            WTSSessionInfo,
            WTSSessionInfoEx,
            WTSConfigInfo,
            WTSValidationInfo,
            WTSSessionAddressV4,
            WTSIsRemoteSession
        };
        #endregion

        private static string getClientComputerName()
        {
            string clientName = "";

            IntPtr ppBuffer = IntPtr.Zero;
            uint size = 0;

            try
            {
                WTSQuerySessionInformation(
                    WTS_CURRENT_SERVER_HANDLE, WTS_CURRENT_SESSION,
                    WTS_INFO_CLASS.WTSClientName,
                    out ppBuffer, out size);
                if (ppBuffer != IntPtr.Zero)
                {
                    clientName = Marshal.PtrToStringAnsi(ppBuffer);
                }
            }
            catch
            {
            }
            finally
            {
                if (ppBuffer != IntPtr.Zero)
                {
                    WTSFreeMemory(ppBuffer);
                }
            }
            return clientName;
        }

        /// <summary>
        /// サーバ側設定ファイルパスを取得する
        /// </summary>
        public static string GetServerPrintSettingsFilePath()
        {
            string clientName = getClientComputerName();
            if (string.IsNullOrEmpty(clientName))
            {
                return null;
            }

            // サーバー側印刷設定用ファイルを読み込む
            string settingsFile = Path.Combine(VerupIO.GetLocalReportDirectory(), @"Settings\ServerPrintSettings_" + clientName);
            if (!File.Exists(settingsFile))
            {
                // 過去Verとの互換性のためファイルが見つからなかったらクライアントPC名の付いていないファイル名に置き換える
                settingsFile = Path.Combine(VerupIO.GetLocalReportDirectory(), @"Settings\ServerPrintSettings");
            }
            return settingsFile;
        }

        /// <summary>
        /// クライアント側XPS出力用のディレクトリを取得する
        /// </summary>
        public static string GetClientPrintingDirectory()
        {
            var settingsFile = GetServerPrintSettingsFilePath();
            if (File.Exists(settingsFile))
            {
                string clientPrintingDir = File.ReadAllText(settingsFile, Encoding.UTF8);
                // リダイレクトドライブのXPS出力先ディレクトリパスが書いてある
                if (Directory.Exists(clientPrintingDir))
                {
                    return clientPrintingDir;
                }
            }
            return null;
        }

        /// <summary>
        /// クライアント側バージョンアップ用のディレクトリを取得する
        /// </summary>
        public static string GetClientVerupDirectory()
        {
            var printingDirectory = VerupIO.GetClientPrintingDirectory();
            if (printingDirectory != null)
            {
                // XPS出力先ディレクトリと同じ階層にあるVerupフォルダへのパスを作成
                var clientVerupDir = Path.Combine(Path.GetDirectoryName(printingDirectory), "Verup");
                return clientVerupDir;
            }
            return null;
        }

        public static List<string> GetDirectoryFilesList(string directory, string pattern)
        {
            var list = new List<string>();
            var enumFiles = Directory.EnumerateFiles(directory, pattern);
            foreach (var path in enumFiles)
            {
                list.Add(Path.GetFileName(path));
            }
            return list;
        }

        public static List<string> GetDirectoryFilesList(string directory)
        {
            return GetDirectoryFilesList(directory, "*");
        }

        public static bool ExistsVerupProgram(string directory)
        {
            if (!string.IsNullOrEmpty(directory))
            {
                var verupFlagFile = Path.Combine(directory, VerupIO.VerupProgramName);
                return File.Exists(verupFlagFile);
            }
            return false;
        }


        public static void CreateVerupFlagFile(string directory)
        {
            var verupFlagFile = Path.Combine(directory, VerupIO.VerupFlagFileame);

            Exception exception = null;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    File.WriteAllText(verupFlagFile, "verup", Encoding.UTF8);
                    return;
                }
                catch (Exception ex)
                {
                    exception = ex;
                    System.Threading.Thread.Sleep(500);
                }
            }
            throw new Exception("更新フラグファイルの作成に失敗しました。", exception);
        }

        public static bool ExistsVerupFlagFile(string directory)
        {
            if (!string.IsNullOrEmpty(directory))
            {
                var verupFlagFile = Path.Combine(directory, VerupIO.VerupFlagFileame);
                return File.Exists(verupFlagFile);
            }
            return false;
        }

        public static void DeleteVerupFlagFile(string directory)
        {
            var verupFlagFile = Path.Combine(directory, VerupIO.VerupFlagFileame);
            if (File.Exists(verupFlagFile))
            {
                VerupIO.deleteFile(verupFlagFile);
            }
            else
            {
                throw new Exception("ファイルが存在しません。 - " + verupFlagFile);
            }
        }

        public static void DeleteFiles(string directory, IEnumerable<string> files)
        {
            if (Directory.Exists(directory))
            {
                // リビジョン番号ファイルがあれば先に削除する
                foreach (var file in files)
                {
                    if (file.CompareTo(VerupIO.RevisionFileName) == 0)
                    {
                        var path = Path.Combine(directory, file);
                        VerupIO.deleteFile(path);
                    }
                }
                
                // その他のファイルを削除する
                foreach (var file in files)
                {
                    if (file.CompareTo(VerupIO.RevisionFileName) != 0)
                    {
                        var path = Path.Combine(directory, file);
                        VerupIO.deleteFile(path);
                    }
                }
            }
        }

        public static void DeleteFiles(string directory)
        {
            if (Directory.Exists(directory))
            {
                var enumFiles = Directory.EnumerateFiles(directory);
                VerupIO.DeleteFiles(directory, enumFiles);
            }
        }

        public static void CopyFiles(string fromDirectory, string toDirectory, IEnumerable<string> files, bool fromNoError = false)
        {
            // コピー先のディレクトリが無ければ作成する
            if (!Directory.Exists(toDirectory))
            {
                Directory.CreateDirectory(toDirectory);
            }

            // リビジョン番号ファイル以外のファイルを最初にコピーする
            foreach (var file in files)
            {
                if (file.CompareTo(VerupIO.RevisionFileName) != 0)
                {
                    var srcPath = Path.Combine(fromDirectory, file);
                    var dstPath = Path.Combine(toDirectory, file);
                    if (fromNoError && !File.Exists(srcPath))
                    {
                        continue;
                    }
                    VerupIO.copyFile(srcPath, dstPath);
                }
            }

            // リビジョン番号ファイルがあれば最後にコピーする
            foreach (var file in files)
            {
                if (file.CompareTo(VerupIO.RevisionFileName) == 0)
                {
                    var srcPath = Path.Combine(fromDirectory, file);
                    var dstPath = Path.Combine(toDirectory, file);
                    if (fromNoError && !File.Exists(srcPath))
                    {
                        continue;
                    }
                    VerupIO.copyFile(srcPath, dstPath);
                }
            }
        }

        public static void CopyFiles(string fromDirectory, string toDirectory)
        {
            var enumFiles = Directory.EnumerateFiles(fromDirectory);
            VerupIO.CopyFiles(fromDirectory, toDirectory, enumFiles);
        }

        private static void deleteFile(string path)
        {
            Exception exception = null;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    File.Delete(path);
                    return;
                }
                catch (Exception ex)
                {
                    exception = ex;
                    System.Threading.Thread.Sleep(500);
                }
            }
            var message = string.Format("ファイルの削除に失敗しました。\r\n\'{0}\'", path);
            throw new Exception(message, exception);
        }

        private static void copyFile(string from, string to)
        {
            Exception exception = null;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    if (File.Exists(to))
                    {
                        File.Delete(to);
                    }
                    File.Copy(from, to);
                    return;
                }
                catch (Exception ex)
                {
                    exception = ex;
                    System.Threading.Thread.Sleep(500);
                }
            }
            var message = string.Format("ファイルのコピーに失敗しました。\r\nfrom '{0}\'\r\nto '{1}\'", from, to);
            throw new Exception(message, exception);
        }

        public static string GetVersionUpLogFilePasth()
        {
            return Path.Combine(VerupIO.GetLocalReportDirectory(), "Shougun.Printing.Verup.log");
        }

        public static void WriteVerupLogFile(string path, string contnets)
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    File.WriteAllText(path, contnets, Encoding.UTF8);
                    return;
                }
                catch
                {
                    System.Threading.Thread.Sleep(500);
                }
            }
        }
    }
}
