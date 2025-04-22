using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;


namespace Shougun.Printing.Common
{
    /// <summary>
    /// XPS印刷/印刷クラス
    /// アプリから利用する。
    /// </summary>
    public static class Initializer
    {
        /// <summary>
        /// 処理モードを取得する
        /// </summary>
        public static ProcessMode ProcessMode { get; private set; }

        /// <summary>
        /// 設定されている処理モードは印刷を実行するモードか？
        /// </summary>
        public static bool IsPrintingMode
        {
            get
            {
                switch (Initializer.ProcessMode)
                {
                    case ProcessMode.OnPremisesBackProcess:
                    case ProcessMode.CloudClientSideProcess:
                        return true;
                    case ProcessMode.OnPremisesFrontProcess:
                    case ProcessMode.CloudServerSideProcess:
                    default:
                        break;
                }
                return false;
            }
        }

        /// <summary>
        /// 設定されている処理モードは設定のみで印刷を実行しないモードか？
        /// </summary>
        public static bool IsSettingsOnlyMode
        {
            get
            {
                return !Initializer.IsPrintingMode;
            }
        }

        /// <summary>
        /// 初期化
        /// アプリケーション起動時に一度だけ実行すること。
        /// </summary>
        /// <param name="isTerminalMode">ターミナル接続の場合はtrue、オンプレミスの場合はfalse</param>
        /// <param name="isServer">サーバー側の場合はtrue、クライアント側の場合はfalse。オンプレミスの場合は無視</param>
        /// <returns></returns>
        public static bool Initialize(ProcessMode processMode)
        {
            bool success = false;
            LastError.Clear();

            Initializer.ProcessMode = processMode;
            UI.FormStyle.SetDefaultStyle();

            if (LocalDirectories.Initialize())
            {
                if (ReportSettingsManager.Instance.Initialize())
                {
                    success = true;

                    // クラウド/サーバー側ならクライアントバージョンアッププロセスの起動
                    if (Initializer.ProcessMode == Common.ProcessMode.CloudServerSideProcess)
                    {
                        int serverRev, clientRev;
                        if (Shougun.Printing.Verup.Launcher.ExistsVerupServerSide(out serverRev, out clientRev))
                        {
                            Shougun.Printing.Verup.Launcher.LaunchVerupServerSide();
                        }
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// 印刷用XPSファイル出力先ディレクトリを取得する。無効な場合はnull。
        /// nullの場合はエラーメッセージを表示する。
        /// </summary>
        public static string GetXpsFilePrintingDirectory(bool silent)
        {
            LastError.Clear();

            string directory = LocalDirectories.GetPrintingtDirectory(Initializer.ProcessMode, true);
            if (LastError.HasError)
            {
                if (!silent)
                {
                    UI.ErrorMessageBox.ShowLastError();
                }
            }
            else if (Initializer.ProcessMode == ProcessMode.OnPremisesFrontProcess)
            {
                // オンプレのフロントプロセスならバックグラウンドプロセスがいなければ起動させる。
                Initializer.StartBackgroundPrintingProcess(false);
            }

            return directory;
        }

        public static string GetXpsFilePrintingDirectory()
        {
            return GetXpsFilePrintingDirectory(false); // true:エラー表示モード
        }

        public static string GetXpsFilePrintingDirectoryNonMsg()
        {
            return GetXpsFilePrintingDirectory(true); // true:サイレントモード
        }

        public static bool StartBackgroundPrintingProcess()
        {
            if (Initializer.ProcessMode == ProcessMode.CloudServerSideProcess)
            {
                ReportSettingsSyncThread.Start();
            }

            bool success = true;

            if (Initializer.ProcessMode == ProcessMode.OnPremisesFrontProcess)
            {
                success = Initializer.StartBackgroundPrintingProcess(true);
            }
            return success;
        }

        public static bool StartBackgroundPrintingProcess(bool reboot)
        {
            LastError.Clear();
            try
            {
                bool onpremises = (Initializer.ProcessMode == Common.ProcessMode.OnPremisesFrontProcess);
                Shougun.Printing.Verup.Launcher.LaunchBackgroundPrintingProcess(
                    LocalDirectories.AppProgramDirectory, reboot, onpremises);
                return true;
            }
            catch (Exception ex)
            {
                LastError.Exception = ex;
            }

            LastError.Message = "印刷プログラムの起動に失敗しました。";
            return false;
        }

        public static void TerminateBackgroundPrintingProcess()
        {
            try
            {
                ReportSettingsSyncThread.Stop();
                Shougun.Printing.Verup.Launcher.TerminateBackgroundPrintingProcess();
            }
            catch
            {
            }
        }
    }
}
