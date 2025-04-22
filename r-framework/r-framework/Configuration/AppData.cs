using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using r_framework.Utility;

namespace r_framework.Configuration
{
    /// <summary>
    /// アプリケーションデータクラス
    /// </summary>
    public static class AppData
    {
        /// <summary>
        /// このクラス専用のロガー
        /// </summary>
        private static log4net.ILog log = null;
        
        /// <summary>
        /// 最後のログインDB名(表示用)を取得する。
        /// </summary>
        static public string LastLoginDbName
        {
            get
            {
                return Properties.Settings.Default.LastLoginDbName;
            }
            set
            {
                Properties.Settings.Default.LastLoginDbName = value;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// ローカルデータ保存先の切り替え、最初の実行時のファイル準備を行う。
        /// 保存先はWindowsログインユーザー、プログラム起動パス、接続先DBによって変更する。
        /// ログ出力先ディレクトリもローカルデータ保存先に切り替える。
        /// ログイン時の接続DB選択切り替え時およびログイン実行時に呼び出す。それ以外からは呼び出さないこと。
        /// </summary>
        /// <param name="dbName">接続DB名(表示用)</param>
        /// <param name="dbName">接続DB名を前回接続DBとして保存するかどうか</param>
        static public Boolean PrepareLocalDataFiles(string dbName)
        {
            string errmsg = "start";
            try
            {
                // user.configのフルパスファイル名
                errmsg = "user.config";
                AppData.UserConfigFilePath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
                    // ex) "C:\Users\E704\AppData\Local\株式会社Edison\KankyouShougunR.exe_Url_1bcexunyc4543zfdq13us0v4g5ht12dr\1.0.0.0\user.config"


                // user.config作成（DB名を書き込むことで確実にuser.configとそのディレクトリが存在するようにする）
                errmsg = "dbName.save";
                if (!File.Exists(AppData.UserConfigFilePath))
                {
                    LastLoginDbName = dbName;
                }

                // 起動アプリのフルパスファイル名
                errmsg = "app.path";
                AppData.AppExecutableFilePath = Application.ExecutablePath;
                    // ex) C:\Program Files\Edison\Release\KankyouShougunR.exe

                // 起動アプリのディレクトリ
                errmsg = "app.dir";
                AppData.AppExecutableDirectory = Path.GetDirectoryName(AppExecutableFilePath);
                    // ex) C:\Program Files\Edison\Release

                // 個別設定データの格納場所(user.confogのフォルダの親フォルダの下のDB名別のフォルダ)
                errmsg = "app.data";
                AppData.LocalAppDataDirectory = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(UserConfigFilePath)), dbName);
                    // ex) "C:\Users\E704\AppData\Local\株式会社Edison\KankyouShougunR.exe_Url_1bcexunyc4543zfdq13us0v4g5ht12dr\つくば受入れ1"

                // このクラス用のロガーを作成
                if (AppData.log == null)
                {
                    errmsg = "logger";
                    AppData.log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                }

                // ログ出力先の設定（app.configではfile="nul"とすること。ここで出力先を設定して初めてログ出力されるようになる）
                errmsg = "log.dir";
                AppData.LogOutputDirectoryPath = Path.Combine(AppData.LocalAppDataDirectory, @"log\");
                // ex) "C:\Users\E704\AppData\Local\株式会社Edison\KankyouShougunR.exe_Url_1bcexunyc4543zfdq13us0v4g5ht12dr\つくば受入れ1\log\"
                LogUtility.SetLogOutputDirectory(AppData.LogOutputDirectoryPath);

                // 最初のCurrentUserCustomConfigProfile準備
                errmsg = "cuccpp.xml";
                AppData.CurrentUserCustomConfigProfilePath = AppData.PrepareLocalAppDataFile("CurrentUserCustomConfigProfile.xml");
                log.InfoFormat("{0}", AppData.CurrentUserCustomConfigProfilePath);

                return true;
            }
            catch (Exception ex)
            {
                errmsg += " " + ex.Message;    
            }

            MessageBox.Show("ローカルデータの初期化に失敗しました。\r\nプログラムを実行できません。\r\nエラーコード=" + errmsg, 
                Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            return false;
        }


        /// <summary>
        /// 起動アプリのファイル名をフルパスで取得する。
        /// </summary>
        static public string AppExecutableFilePath
        {
            get; 
            private set;
        }
        /// <summary>
        /// 起動アプリのディレクトリ名をフルパスで取得する。
        /// </summary>
        static public string AppExecutableDirectory
        {
            get; 
            private set;
        }

        /// <summary>
        /// user.configファイルのフルパスを取得する。
        /// </summary>
        static public string UserConfigFilePath
        {
            get;
            private set;
        }

        /// <summary>
        /// ローカルAppDataを格納するディレクトリを取得する。
        /// </summary>
        static public string LocalAppDataDirectory
        {
            get;
            private set;
        }

        /// <summary>
        /// ローカルAppDataのログ出力先ディレクトリのパスを取得する。
        /// </summary>
        static public string LogOutputDirectoryPath
        {
            get;
            private set;
        }

        /// <summary>
        /// ローカルAppDataのCurrentUserCustomConfigProfile.xmlへのフルパスを取得する。
        /// ローカルAppDataのディレクトリは起動アプリのパスに依存する。
        /// </summary>
        static public string CurrentUserCustomConfigProfilePath
        {
            get;
            private set;
        }

        /// <summary>
        /// ローカルAppDataファイルを作成する。
        /// 引数で指定されたファイル（サブディレクトリ付きも可）のAppDataフォルダ上でのフルパスを返却する。
        /// ファイルがAppDataフォルダ内に存在しない場合は起動したexeと同じディレクトリに存在する同名のファイルがAppDataフォルダにコピーされる。
        /// 既にAppDataフォルダ内に存在する場合は何もせずに単にフルパスを返却する。
        /// </summary>
        /// <exception cref="System.IO.File.Copy, System.IO.CreateDirectoryと同じ例外"/>
        static public string PrepareLocalAppDataFile(string fileName)
        {
            // 個別設定保存ディレクトリ内に指定のサブフォルダ/ファイルがあるか確認する。
            var localFilePath = Path.Combine(LocalAppDataDirectory, fileName);
            if (!File.Exists(localFilePath))
            {
                log.InfoFormat("MakeLocalAppDataFile() Local file not exist. {0}", localFilePath);

                // サブディレクトリ付きで指定された場合はまずサブディレクトリを作成する
                var localDirectoryPath = Path.GetDirectoryName(localFilePath);
                if (!Directory.Exists(localDirectoryPath))
                {
                    log.InfoFormat("Directory.CreateDirectory({0})", localFilePath);
                    Directory.CreateDirectory(localDirectoryPath);
                }

                // 起動ディレクト内の同名のファイルを保存ディレクトリにコピーする
                string sourceFilePath = Path.Combine(AppExecutableDirectory, fileName);
                if (File.Exists(sourceFilePath))
                {
                    log.InfoFormat("File.Copy({0}, {1})", sourceFilePath, localFilePath);
                    File.Copy(sourceFilePath, localFilePath);
                }
                else
                {
                    // 保存ディレクトリにもアプリ起動ディレクトリにも無い場合
                    log.WarnFormat("Source file not exist. {0}", sourceFilePath);
                }
            }

            return localFilePath;
        }
    }
}
