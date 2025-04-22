using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Shougun.Printing.Common
{
    /// <summary>
    /// 連続印刷時の中断シーケンス制御クラス
    /// 連続印刷時は同じ帳票設定で印刷するので設定情報のキャッシュの制御も行う
    /// </summary>
    public class AbortPrinting
    {
        /// <summary>
        /// 連続印刷の開始（呼び出し：将軍側）
        /// </summary>
        public static bool BeginSequence()
        {
            var directory = LocalDirectories.GetPrintingtDirectory(Initializer.ProcessMode, true);
            if (LastError.HasError)
            {
                UI.ErrorMessageBox.ShowLastError();
                return false;
            }

            // 連続印刷中は帳票設定の同期処理を中断させる
            ReportSettingsSyncThread.Interrupt(true);

            // 帳票の種類を指定された場合、連続印刷中は余白設定は同じ設定でよいので余白設定を予め読み込みキャッシュさせる
            ReportSettingsManager.EnableMarginDeltaCache(true);

            try
            {
                // 中断オンフラグファイルを削除
                File.Delete(AbortPrinting.getAbortFlagFilePath(directory, true));

                // 中断オフフラグファイルを作成
                var path = AbortPrinting.getAbortFlagFilePath(directory, false);
                using (var file = new FileStream(path, FileMode.Create))
                {
                    file.Close();
                }
            }
            catch
            {
            }
            return true;
        }

        /// <summary>
        /// 連続印刷の中断可能の問い合わせ（呼び出し：印刷側）
        /// </summary>
        public static bool CanAborting()
        {
            bool result = false;
            var directory = LocalDirectories.GetPrintingtDirectory(Initializer.ProcessMode, false);
            if (!string.IsNullOrEmpty(directory))
            {
                result = File.Exists(AbortPrinting.getAbortFlagFilePath(directory, false));
            }
            return result;
        }

        /// <summary>
        /// 連続印刷の中断（呼び出し：印刷側）
        /// </summary>
        public static void AbortSequence()
        {
            Debug.WriteLine("AbortSequence()");
            var directory = LocalDirectories.GetPrintingtDirectory(Initializer.ProcessMode, false);
            if (string.IsNullOrEmpty(directory))
            {
                return;
            }
            var abortOff = AbortPrinting.getAbortFlagFilePath(directory, false);
            var abortOn = AbortPrinting.getAbortFlagFilePath(directory, true);
            try
            {
                if (File.Exists(abortOff))
                {
                    // ファイル abort.off を abort.onにリネーム
                    Debug.WriteLine("abort off -> on");
                    File.Move(abortOff, abortOn);

                    // 将軍側が中断要求を受け入れるのを最大30秒間待つ
                    for (int i = 0; i < 60 ; i ++)
                    {
                        // 将軍側がabort.onを検出すると削除するのでファイルが無くなるのを検出する
                        if (!File.Exists(abortOn))
                        {
                            Debug.WriteLine("detected abort acceptance.");
                            break;
                        }
                        Debug.WriteLine("waiting abort acceptance.");
                        System.Threading.Thread.Sleep(500);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 連続印刷の中断要求があるかどうかの確認（呼び出し：将軍側）
        /// </summary>
        public static bool IsAbortRequired()
        {
            bool result = false;
            var directory = LocalDirectories.GetPrintingtDirectory(Initializer.ProcessMode, false);
            if (!string.IsNullOrEmpty(directory))
            {
                result = File.Exists(AbortPrinting.getAbortFlagFilePath(directory, true));
            }
            return result;
        }

        /// <summary>
        /// 連続印刷の終了（呼び出し：将軍側）
        /// </summary>
        public static void TerminateSequence()
        {
            // 連続印刷が終わったら帳票設定の同期処理を再開させる
            ReportSettingsSyncThread.Interrupt(false);

            // 余白設定読み込みのキャッシュクリア
            ReportSettingsManager.EnableMarginDeltaCache(false);
            
            // Printingディレクトリの掃除
            var directory = LocalDirectories.GetPrintingtDirectory(Initializer.ProcessMode, false);
            if (string.IsNullOrEmpty(directory))
            {
                return;
            }
            var abortOn = AbortPrinting.getAbortFlagFilePath(directory, true);
            if (File.Exists(abortOn))
            {
                var files = Directory.EnumerateFiles(directory, "*.xps");
                foreach (var path in files)
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch 
                    { 
                    }
                }
            }
            File.Delete(AbortPrinting.getAbortFlagFilePath(directory, false));
            File.Delete(AbortPrinting.getAbortFlagFilePath(directory, true));
        }

        private static void deleteAbortFlagFiles(string directory)
        {
            if (!string.IsNullOrEmpty(directory))
            {
                File.Delete(AbortPrinting.getAbortFlagFilePath(directory, false));
                File.Delete(AbortPrinting.getAbortFlagFilePath(directory, true));
            }
        }

        private static string getAbortFlagFilePath(string directory, bool on)
        {
            return Path.Combine(directory, on ? "abort.on" : "abort.off");
        }
    }
}
