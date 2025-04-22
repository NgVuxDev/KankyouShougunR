using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using r_framework.Configuration;
using Seasar.Framework.Container.Factory;

namespace Shougun.Core.Common.Login
{
    /// <summary>
    /// Program
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// このクラス専用のロガー
        /// </summary>
        private static log4net.ILog log

        
        /// <summary>
        /// スプラッシュスクリーンの参照
        /// </summary>
        private static Shougun.SplashScreen splashScreen = null;

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        internal static void Main(string[] args)
        {
            // VisualStyleの初期化（何らかのウインドウ表示の前に呼び出す）
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // スプラッシュスクリーンの表示開始
            splashScreen = new Shougun.SplashScreen();
            splashScreen.Show();
            splashScreen.Refresh();

            // システムの日付書式からアプリケーションで使用する日付書式にローカライズする。日付書式だけ。
            System.Globalization.CultureInfo TempCulture = (System.Globalization.CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            TempCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            Thread.CurrentThread.CurrentCulture = TempCulture;
            TempCulture = null;

            // カレントディレクトリを実行ファイルの親ディレクトリに設定する。
            var thisFile = Assembly.GetEntryAssembly().Location;
            Directory.SetCurrentDirectory(Path.GetDirectoryName(thisFile));

            // バージョンファイルの読み込みと構成プロパティの設定
            string errorMessage;
            if (!AppConfig.LoadVersionFile("KankyouShougunR.version.txt", out errorMessage))
            {
                showAppInitFatalError("バージョンファイルの読み込みができませんでした。プログラムを終了します。\r\n" + errorMessage);
                return;
            }

            // /verオプション付の場合はバージョン情報を表示して終了する。
            if (args.Length > 0)
            {
                if (args[0].ToLower().CompareTo("/ver") == 0)
                {
                    Program.ShowVersionInfoDialog();
                    return;
                }
            }

            // 開発時、検証用に多重起動を許す、隠し操作
            if (!File.Exists("Open Multiple"))
            {
                // ミューテックスによる多重起動防止
                r_framework.FormManager.FormManager.gMutex = new System.Threading.Mutex(false, typeof(Program).FullName);
                if (!r_framework.FormManager.FormManager.gMutex.WaitOne(0, false))
                {
                    // クラウドで既に起動中の場合、ポップアップを表示せず終了する(前回起動したRを使う)
                    if (!AppConfig.IsTerminalMode)
                    {
                        showAppInitFatalError("多重起動はできません。");
                    }
                    return;
                }
            }

            //プロテクトチェック(参照設定で、Protectを追加する)
            AppConfig.ProtectModeSet(ShougunProtect.Program.Open_Check());

#if DEBUG
            if(Debugger.IsAttached == true)
            {
                // デバッガ起動時はエラー箇所を特定するため、catchしない
                showMainWindow();
            }
            else
#endif
            {
                try
                {
                    showMainWindow();
                }
                catch(FileLoadException ex)
                {
                    showAppInitFatalError("ファイルの読み込みに失敗しました。\r\n" + ex.Message);
                    log.Fatal(ex.Message, ex);
                }
                catch(FileNotFoundException ex)
                {
                    showAppInitFatalError("必要なファイルが見つかりませんでした。\r\n" + ex.Message);
                    log.Fatal(ex.Message, ex);
                }
                catch(IOException ex)
                {
                    showAppInitFatalError("設定ファイルの保存に失敗しました。\r\n" + ex.Message);
                    log.Fatal(ex.Message, ex);
                }
                catch(Exception ex)
                {
                    showAppInitFatalError("システムでエラーが発生しました。\r\n開発元にご連絡ください。\r\n" + ex.Message);
                    log.Fatal(ex.Message, ex);
                }
            }
        }

        /// <summary>
        /// メインウインドウの表示
        /// </summary>
        private static void showMainWindow()
        {
            // デフォルトDB接続情報を取得(DatasBaseConnectionList.xmlから前回接続DBの要素または先頭要素を取得)
            var dbInfo = XmlManager.GetDatabaseConnectItem(AppData.LastLoginDbName);
            if(dbInfo == null)
            {
                showAppInitFatalError("DB接続情報ファイルの読み込みに失敗しました。\r\n");
                return;
            }
            var dbDispName = dbInfo.Element("DispName").Value;
            if(string.IsNullOrEmpty(dbDispName))
            {
                showAppInitFatalError("DB接続情報ファイルの内容が不正です。\r\n");
                return;
            }

            // AppData/ローカルファイル/ログ出力先の切り替え
            if(!AppData.PrepareLocalDataFiles(dbDispName))
            {
                // エラーの内容はPrepareLocalDataFiles内でメッセージボックスに表示する。
                return;
            }

            // ロガーの作成
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            // 起動に必要な設定ファイルがそろっているか確認（不足なら終了）
            string[] files;
            if(!ExistsSettingFiles(out files))
            {
                showAppInitFatalError("設定ファイルが見つかりませんでした。\r\n" + files[0]);
                return;
            }

            // 印刷処理の初期化
            Shougun.Printing.Common.ProcessMode processMode;
            if (r_framework.Configuration.AppConfig.IsTerminalMode)
            {
                processMode = Shougun.Printing.Common.ProcessMode.CloudServerSideProcess;
            }
            else
            {
                processMode = Shougun.Printing.Common.ProcessMode.OnPremisesFrontProcess;
            }

            if (!Shougun.Printing.Common.Initializer.Initialize(processMode))
            {
                showAppInitFatalError(Shougun.Printing.Common.LastError.FullMessage);
                return;
            }

            // S2Containerを初期化
            var container = S2ContainerFactory.Create(SingletonS2ContainerFactory.ConfigPath);
            SingletonS2ContainerFactory.Container = container;

            // メインウインドウスレッド全体の例外ハンドラ
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            // メインウインドウの表示
            var loginForm = new UIForm();
            loginForm.OnDisplayCompleted += OnLoginFormDisplayCompleted;
            Application.Run(loginForm);

            // 印刷バックグラウンドプロセスを終了させる。
            CommonChouhyouPopup.Logic.ContinuousPrinting.Terminalte();
            Shougun.Printing.Common.Initializer.TerminateBackgroundPrintingProcess();
        }

        /// <summary>
        /// アプリ初期化エラー（ログ出力ができない状況のために使用する）
        /// </summary>
        private static void showAppInitFatalError(string message)
        {
            MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// LOGIN画面表示完了イベントハンドラ
        /// </summary>
        private static void OnLoginFormDisplayCompleted()
        {
            // スプラッシュスクリーンの表示終了
            if (splashScreen != null)
            {
                splashScreen.Close();
                splashScreen.Dispose();
                splashScreen = null;
            }
        }

        /// <summary>
        /// Exception発生時の処理（仮）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            showAppInitFatalError("システムでエラーが発生しました。\r\n開発元にご連絡ください。\r\n" + e.Exception.Message);
            if (log != null)
            {
                log.Fatal(e.Exception.Message, e.Exception);
            }
        }

        /// <summary>
        /// DatabaseConnectList.xml,
        /// CurrentUserCustomConfigProfile.xml,
        /// r-frameworkConfig.xml,
        /// Settings\menu.xml または menu.xml,
        /// dicon\App.dicon,
        /// dicon\Dao.dicon,
        /// dicon\Tx.dicon,
        /// dicon\Ado.dicon
        /// dicon\Dao_File.dicon,
        /// dicon\Tx_File.dicon,
        /// dicon\Ado_File.dicon
        /// が存在するかチェックします。
        /// </summary>
        /// <param name="notExistsFiles">存在しないファイルのリスト</param>
        /// <returns>True : 全て存在, False : 存在しないファイルが１つ以上</returns>
        private static bool ExistsSettingFiles(out string[] notExistsFiles)
        {
            var list = new string[] { XmlManager.DBConfigFile, XmlManager.UserConfigFile, SingletonS2ContainerFactory.ConfigPath, 
                XmlManager.DaoDiconFile, XmlManager.TxDiconFile, XmlManager.AdoDiconFile,
                XmlManager.DaoFileDiconFile, XmlManager.TxFileDiconFile, XmlManager.AdoFileDiconFile,
                XmlManager.DaoFileDiconLog, XmlManager.TxFileDiconLog, XmlManager.AdoFileDiconLog};
                //XmlManager.MenuFile };

            notExistsFiles = list.Where(n => !File.Exists(n)).ToArray<string>();

            return notExistsFiles.Count() == 0;
        }

        public static void ShowVersionInfoDialog()
        {
            /*
            var buf = new StringBuilder();

            VersionInfo b = AppConfig.BaseVersion;
            VersionInfo c = AppConfig.CustomizeVersion;

            buf.AppendFormat("【{0}】\r\n", b.ProductName);

            if (AppConfig.IsCustomized)
            {
                buf.AppendFormat("　ユーザー： {0}\r\n", c.CustomerName);
                buf.AppendFormat("　バージョン： {0}.{1}.{2}.{3}\r\n",
                    c.MajorVersion, c.MinorVersion, c.BuildNumber, c.Revision);
                buf.AppendFormat("　作成日時　： {0}\r\n", c.BuildDate.ToString("yyyy/MM/dd HH:mm"));
                buf.Append("\r\n");
                buf.AppendFormat("【基本パッケージ】\r\n");
            }

            buf.AppendFormat("　バージョン： {0}.{1}.{2}.{3}\r\n",
                b.MajorVersion, b.MinorVersion, b.BuildNumber, b.Revision);
            buf.AppendFormat("　作成日時　： {0}\r\n", b.BuildDate.ToString("yyyy/MM/dd HH:mm"));
            buf.AppendFormat("　{0}\r\n", b.ProductInfo);
            buf.Append("\r\n");

            buf.AppendFormat("【構成情報】\r\n");
            buf.AppendFormat("　{0}({0})\r\n", AppConfig.AppName, AppConfig.Series); 
            buf.AppendFormat("　{0}{1}\r\n",
                (AppConfig.IsTerminalMode ? "クラウド" : "オンプレミス"), "製品版"); 
            buf.AppendFormat("　CAL数 {0}\r\n", AppConfig.UserCal);
            buf.Append("\r\n");

            buf.AppendFormat("　{0}\r\n", b.VenderName);
            buf.AppendFormat("　{0}\r\n", b.VenderInfo);

            System.Windows.Forms.MessageBox.Show(buf.ToString(), b.ProductName + " バージョン情報",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            */

            var versionDialog = new APP.VersionInfoDialog(AppConfig.VersionInfo);
            versionDialog.StartPosition = FormStartPosition.CenterParent;
            versionDialog.ShowDialog();
            versionDialog.Dispose();
        }

    }
}
