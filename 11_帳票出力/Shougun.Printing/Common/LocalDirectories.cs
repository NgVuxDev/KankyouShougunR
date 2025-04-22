using System;
using System.Configuration;
using System.IO;

namespace Shougun.Printing.Common
{
    /// <summary>
    /// 環境将軍R印刷管理/環境/ローカルクラス
    /// </summary>
    public class LocalDirectories
    {
        /// <summary>
        /// アプリケーションプログラムディレクトリ
        /// ex) c:Program Files(x86)/ediosn/KankyouShougunR
        /// ex) c:Program Files(x86)/ediosn/Shougun.Printing
        /// </summary>
        static public string AppProgramDirectory
        {
            get
            {
                return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }

        /// <summary>
        /// アプリケーションローカルデータディレクトリ
        /// ex) c:/Users/(user)/AppData/Local/株式会社Edison/ShougunReport
        /// </summary>
        static public string AppLocalDataDirectory { get; private set; }

        /// <summary>
        /// 帳票設定ファイル保存ディレクトリ
        /// ex) c:/Users/(user)/AppData/Local/株式会社Edison/ShougunReport/Settings
        /// </summary>
        static public string SettingsDirectory { get; private set; }

        /// <summary>
        /// 印刷XPSファイル入出力ディレクトリ
        /// ex) c:/Users/(user)/AppData/Local/株式会社Edison/ShougunReport/Printing
        /// </summary>
        static public string PrintingDirectory { get; private set; }

        /// <summary>
        /// 印刷XPSファイル統合用ディレクトリ
        /// ex) c:/Users/(user)/AppData/Local/株式会社Edison/ShougunReport/PrintingTMP
        /// </summary>
        static public string PrintingTMPDirectory { get; private set; }

        /// <summary>
        /// 印刷済みXPSファイルバックアップディレクトり
        /// ex) c:/Users/(user)/AppData/Local/株式会社Edison/ShougunReport/Backup
        /// </summary>
        static public string BackupDirectory { get; private set; }

        /// <summary>
        /// プログラムバージョンアップファイルディレクトり
        /// ex) c:/Users/(user)/AppData/Local/株式会社Edison/ShougunReport/Verup
        /// </summary>
        static public string VerupDirectory { get; private set; }

        /// <summary>
        /// 印刷プログラムのログ出力ディレクトり
        /// ex) c:/Users/(user)/AppData/Local/株式会社Edison/ShougunReport/Logs
        /// </summary>
        static public string LogsDirectory { get; private set; }

        /// <summary>
        /// 印刷PGのログファイル名
        /// </summary>
        static public string logFileName { get; private set; }

        /// <summary>
        /// 印刷PGのログ出力モードフラグ
        /// 拡張子なしで"/ShougunReport/Settings/"に"LoggingMode"というファイルを作成すると
        /// ログ出力モード
        /// </summary>
        static public bool IsLoggingMode { get; private set; }

        /// <summary>
        /// プレビューファイルディレクトり
        /// ex) c:/Users/(user)/AppData/Local/株式会社Edison/ShougunReport/FilePreview
        /// </summary>
        static public string FilePreviewDirectory { get; private set; }

        /// <summary>
        /// 地図の出力ディレクトり
        /// ex) c:/Users/(user)/AppData/Local/株式会社Edison/ShougunReport/Maps
        /// </summary>
        static public string MapsDirectory { get; private set; }

        /// <summary>
        /// ローカルディレクトリの初期化
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        static public bool Initialize()
        {
            if (Initializer.ProcessMode == ProcessMode.NotSet)
            {
                LastError.Message = "ProcessMode is not set.";
                return false;
            }

            try
            {
                var configFilePath = ConfigurationManager.OpenExeConfiguration(
                            ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;

                var appData = Path.GetDirectoryName(configFilePath);
                appData = Path.GetDirectoryName(appData);
                appData = Path.GetDirectoryName(appData);
                appData = Path.Combine(appData, "ShougunReport");

                LocalDirectories.AppLocalDataDirectory = appData;
                LocalDirectories.SettingsDirectory = Path.Combine(appData, "Settings");
                LocalDirectories.PrintingDirectory = Path.Combine(appData, "Printing");
                LocalDirectories.PrintingTMPDirectory = Path.Combine(appData, "PrintingTMP");
                LocalDirectories.BackupDirectory = Path.Combine(appData, "Backup");
                LocalDirectories.VerupDirectory = Path.Combine(appData, "Verup");
                LocalDirectories.LogsDirectory = Path.Combine(appData, "Logs");
                LocalDirectories.FilePreviewDirectory = Path.Combine(appData, "FilePreview");
                LocalDirectories.MapsDirectory = Path.Combine(appData, "Maps");

                string[] directories = { LocalDirectories.AppLocalDataDirectory,
                                         LocalDirectories.SettingsDirectory, 
                                         LocalDirectories.PrintingDirectory, 
                                         LocalDirectories.PrintingTMPDirectory, 
                                         LocalDirectories.BackupDirectory,
                                         LocalDirectories.VerupDirectory,
                                         LocalDirectories.LogsDirectory,
                                         LocalDirectories.FilePreviewDirectory,
                                         LocalDirectories.MapsDirectory};
                foreach (var directory in directories)
                {
                    Directory.CreateDirectory(directory);
                    if (!Directory.Exists(directory))
                    {
                        LastError.Message = string.Format("印刷用ディレクトリの作成に失敗しました。\r\n{0}", directory);
                        return false;
                    }
                }

                // logger初期化(フォルダの情報が初期化された後で実行)
                LocalDirectories.SetLoggingMode();
                LocalDirectories.CreateLogFile();
                LocalDirectories.CleanUpOldLogfiles();

                return true;
            }
            catch (Exception ex)
            {
                LastError.Exception = ex;
            }
            return false;
        }

        public static string GetPrintingtDirectory(ProcessMode processMode, bool doCheckExist = true)
        {
            LastError.Clear();
            string directory = null;
            if (processMode == ProcessMode.CloudServerSideProcess)
            {
                directory = ServerSettings.GetClientSidePrintingDirectory();
            }
            else
            {
                directory = LocalDirectories.PrintingDirectory;
            }

            if (!string.IsNullOrEmpty(directory))
            {
                if (doCheckExist)
                {
                    if (Directory.Exists(directory))
                    {
                        return directory;
                    }
                }
                else
                {
                    return directory;
                }
            }
            if (!LastError.HasError)
            {
                LastError.Message = "帳票出力先ディレクトリにアクセスできません。\r\n開発元に問い合わせしてください";
            }
            return directory;
        }

        /// <summary>
        /// ログ出力モード設定
        /// </summary>
        public static void SetLoggingMode()
        {
            LocalDirectories.IsLoggingMode = false;

            if (Directory.Exists(LocalDirectories.SettingsDirectory)
                && File.Exists(Path.Combine(LocalDirectories.SettingsDirectory, @"LoggingMode")))
            {
                LocalDirectories.IsLoggingMode = true;
            }
        }

        /// <summary>
        /// ログファイルの生成
        /// 既に同じ日付でファイルが作成されている場合は何もしない。
        /// </summary>
        public static void CreateLogFile()
        {
            if (!LocalDirectories.IsLoggingMode)
            {
                return;
            }

            try
            {
                LocalDirectories.logFileName = "Print" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                if (!File.Exists(LocalDirectories.LogsDirectory + "\\" + LocalDirectories.logFileName))
                {
                    using (var fs = File.Create(LocalDirectories.LogsDirectory + "\\" + LocalDirectories.logFileName))
                    {
                        if (fs != null)
                        {
                            fs.Close();
                        }
                    }
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 古いログファイルの削除
        /// </summary>
        public static void CleanUpOldLogfiles()
        {
            try
            {
                var a = new TimeSpan(-7, 0, 0, 0);
                var logFiles = System.IO.Directory.EnumerateFiles(LocalDirectories.LogsDirectory, "*.log");
                foreach (var logFilePath in logFiles)
                {
                    var timeStamp = System.IO.File.GetLastWriteTime(logFilePath);
                    var span = timeStamp - System.DateTime.Now;
                    if (span.CompareTo(a) < 0)
                    {
                        System.IO.File.Delete(logFilePath);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Infoレベルのログを出力
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            if (!LocalDirectories.IsLoggingMode)
            {
                return;
            }

            try
            {
                using (var sw = new StreamWriter(LocalDirectories.LogsDirectory + "\\" + LocalDirectories.logFileName, true, System.Text.Encoding.GetEncoding("shift_jis")))
                {
                    string outputMessage = string.Empty;
                    outputMessage = "INFO  " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " " + message;
                    sw.WriteLine(outputMessage);
                    sw.Close();
                }
            }
            catch
            {
            }
        }

        #region mapbox連携

        public static string GetMapDirectory(ProcessMode processMode, bool doCheckExist = true)
        {
            LastError.Clear();
            string directory = null;
            if (processMode == ProcessMode.CloudServerSideProcess)
            {
                directory = ServerSettings.GetClientSideMapsDirectory();
            }
            else
            {
                directory = LocalDirectories.MapsDirectory;
            }

            if (!string.IsNullOrEmpty(directory))
            {
                if (doCheckExist)
                {
                    if (Directory.Exists(directory))
                    {
                        return directory;
                    }
                }
                else
                {
                    return directory;
                }
            }
            if (!LastError.HasError)
            {
                LastError.Message = "地図出力先ディレクトリにアクセスできません。\r\n開発元に問い合わせしてください";
            }
            return directory;
        }

        #endregion
    }
}
