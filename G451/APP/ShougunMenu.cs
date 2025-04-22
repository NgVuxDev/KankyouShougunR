using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.FileUpload.FileUploadCommon.Logic;
using System.Reflection;
using r_framework.CommunicateApp;

namespace Shougun.Core.Common.Menu
{
    /// <summary>
    /// メインメニュークラス
    /// </summary>
    public partial class ShougunMenu : SuperForm
    {
        /// <summary>
        /// 各画面で使用するログイン情報
        /// </summary>
        private CommonInformation commonInfo;

        /// <summary>
        /// リボンメニュー
        /// </summary>
        private SuperForm ribbonForm;

        /// <summary>
        /// エラーコード１（アセンブリ無エラー）
        /// </summary>
        private string errorCd1 = "E135";

        // 20140630 ria No.730 マニフェスト追加機能仕様_４ start
        /// <summary>共通</summary>
        Shougun.Core.Common.BusinessCommon.Logic.SyuuryouBiKeikokuLogic Slogic = null;
        // 20140630 ria No.730 マニフェスト追加機能仕様_４ end

        /// <summary>
        /// TOPへの情報公開（画面名）
        /// </summary>
        private string topHeNoJouhouKoukai = "TOPへの情報公開";

        /// <summary>
        /// 取引先DAO
        /// </summary>
        private IM_TORIHIKISAKIDao TorihikisakiDao;

        /// <summary>
        /// 業者DAO
        /// </summary>
        private IM_GYOUSHADao GyoushaDao;

        /// <summary>
        /// 現場DAO
        /// </summary>
        private IM_GENBADao GenbaDao;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="menuConfig">menu.xmlのパス</param>
        /// <param name="info">コモンインフォーメーション</param>
        public ShougunMenu(CommonInformation info)
        {
            this.InitializeComponent();

            this.commonInfo = info;

            this.ribbonForm = new RibbonMainMenu(Shougun.Core.Common.Login.XmlManager.MenuFile, info);

            // 20140630 ria No.730 マニフェスト追加機能仕様_４ start
            this.Slogic = new Common.BusinessCommon.Logic.SyuuryouBiKeikokuLogic();
            // 20140630 ria No.730 マニフェスト追加機能仕様_４ end

            string verText;
            if (r_framework.Configuration.AppConfig.IsCustomized)
            {
                var v = r_framework.Configuration.AppConfig.CustomizeVersion;
                verText = string.Format("Version:{0}.{1}.{2}.{3} Build:{4} {5}\r\n",
                   v.MajorVersion, v.MinorVersion, v.BuildNumber, v.Revision,
                   v.BuildDate.ToString("yyyy/MM/dd"), v.CustomerName);
            }
            else
            {
                var v = r_framework.Configuration.AppConfig.BaseVersion;
                verText = string.Format("Version:{0}.{1}.{2}.{3} Build:{4} {5}\r\n",
                   v.MajorVersion, v.MinorVersion, v.BuildNumber, v.Revision,
                   v.BuildDate.ToString("yyyy/MM/dd"), v.ProductInfo);
            }

            this.VersionLabel.Text = verText;
        }

        /// <summary>
        /// システム設定情報
        /// </summary>
        public M_SYS_INFO SysInfo
        {
            get { return this.commonInfo.SysInfo; }
        }

        /// <summary>
        /// 会社情報
        /// </summary>
        public M_CORP_INFO CorpInfo
        {
            get { return this.commonInfo.CorpInfo; }
        }

        /// <summary>
        /// ログイン社員情報
        /// </summary>
        public M_SHAIN CurrentShain
        {
            get { return this.commonInfo.CurrentShain; }
        }

        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SystemProperty.SetInxsSettings(Path.Combine(AppData.AppExecutableDirectory, Constans.INXS_PROGRAM_PATH));

            this.ShowContent(this.ribbonForm, new Point(0, 0), new Size());
            this.SetSeriesThema();

            r_framework.FormManager.FormManager.UserRibbonMenu = (RibbonMainMenu)this.ribbonForm;

            // 電マニ通知区分のいずれかが"1.表示する"の場合は通知フォームを追加する
            if (this.commonInfo.SysInfo.MANIFEST_JYUUYOU_DISP_KBN == 1 ||
                this.commonInfo.SysInfo.MANIFEST_OSHIRASE_DISP_KBN == 1 ||
                this.commonInfo.SysInfo.MANIFEST_RIREKI_DISP_KBN == 1)
            {
                // 通知情報更新の為のイベントハンドラ追加
                this.Activated += new System.EventHandler(this.ShougunMenu_Activated);
            }

            if (AppConfig.AppOptions.IsFileUpload())
            {
                // DB接続
                if (!string.IsNullOrEmpty(this.commonInfo.SysInfo.DB_FILE_CONNECT) && !new FileUploadLogic().ConnectDB())
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShowWarn("ファイルアップロード用DBの接続が行えませんでした。\r\nシステム管理者に報告を行い、確認を行ってもらってください。");
                }
            }

            // CTI連携テキストファイル監視スタート
            this.MonitoringCTIRenkeiFile();

            // 最大化
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            var ItemValue = this.GetUserProfileValue(userProfile, "画面表示サイズ");
            if (ItemValue == "2")
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }

            //試用期間残数表示
            ShougunProtect.Program.TrialDisplay();
            //CongBinh 20210712 #152800 S
            if (this.commonInfo.SysInfo.SUPPORT_KBN == 2)
            {
                this.lblSupport.Visible = false;
                this.pnlSupport.Visible = false;
            }
            //CongBinh 20210712 #152800 E
            this.ribbonForm.Focus();
        }

        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
        {
            LogUtility.DebugMethodStart(profile, key);

            string result = string.Empty;

            foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
            {
                if (item.Name.Equals(key))
                {
                    result = item.Value;
                }
            }

            LogUtility.DebugMethodEnd(result);
            return result;
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        /// <param name="showForm">画面</param>
        /// <param name="showLocation">表示位置</param>
        /// <param name="showSize">サイズ</param>
        private void ShowContent(Form showForm, Point showLocation, Size showSize)
        {
            showForm.TopLevel = false;
            if (!showSize.IsEmpty)
            {
                showForm.Size = showSize;
            }

            showForm.Location = showLocation;
            this.Controls.Add(showForm);
            showForm.Show();

            // 最前面へ移動
            showForm.BringToFront();
        }

        /// <summary>
        /// オンプレ構成でメインメニュー画面が表示されたら印刷バックグラウンドプロセスを開始。
        /// （クラウドの場合は印刷プロセスはクライアント側マシンに常駐実行している）
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (!Shougun.Printing.Common.Initializer.StartBackgroundPrintingProcess())
            {
                Shougun.Printing.Common.UI.ErrorMessageBox.ShowLastError();
            }
            CommonChouhyouPopup.Logic.ContinuousPrinting.Initialize();

            //Communicate InxsSubApplication Start
            LauncherDto launcherDto = new LauncherDto()
            {
                ShainCD = SystemProperty.Shain.CD,
                ShougunConfigPath = r_framework.Configuration.AppData.CurrentUserCustomConfigProfilePath,
                Args = null
            };
            InxsLauncher.LaunchSupApp(launcherDto);
            //Communicate InxsSubApplication End

            // 20140630 ria No.730 マニフェスト追加機能仕様_４ start
            if (this.Slogic.CheckSysInfo())
            {
                this.Slogic.CheckSyuuryouBiKeikoku();
            }
            // 20140630 ria No.730 マニフェスト追加機能仕様_４ end
        }

        /// <summary>
        /// フォームを閉じる際の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShougunMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ApplicationExit()によってクローズされようとしている時はそのまま閉じる
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                // 終了手続き
                FormManager.ExitShougunR(this);

                // 帰ってくると言うことはキャンセルされたと言うことなのでイベントをキャンセルする
                e.Cancel = true;
            }
        }

        /// <summary>
        /// メインメニュー画面の終了時、印刷バックグラウンドプロセスも終了させる
        /// </summary>
        /// <param name="e"></param>
        private void ShougunMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Communicate InxsSubApplication Start
            InxsLauncher.CloseSubApp();
            //Communicate InxsSubApplication End
            this.cleanUpOldLogfiles();
            this.cleanUpPreviewFiles();
            Shougun.Printing.Common.Initializer.TerminateBackgroundPrintingProcess();
            // CTI連携テキストファイルの監視処理停止
            if (this.ctiFileWatchTimer != null)
            {
                this.ctiFileWatchTimer.Stop();
            }
        }

        /// <summary>
        /// 30日以前の古いログファイルを削除する
        /// </summary>
        private void cleanUpOldLogfiles()
        {
            try
            {
                var a = new TimeSpan(-30, 0, 0, 0);
                var logFiles = System.IO.Directory.EnumerateFiles(AppData.LogOutputDirectoryPath);
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
        /// ファイルアップロードのプレビュー用にダウンロードした一時ファイルを削除する（オンプレのみ）
        /// </summary>
        private void cleanUpPreviewFiles()
        {
            try
            {
                if (AppConfig.IsTerminalMode)
                {
                    return;
                }

                var folderPath = Path.Combine(Path.GetTempPath(), FileUploadLogic.TEMPORARY_FOLDER);
                if (!Directory.Exists(folderPath))
                {
                    return;
                }

                // 一時ファイルのため、作成から1日経過したファイルは全て削除
                var a = new TimeSpan(-1, 0, 0, 0);
                var previewFiles = Directory.EnumerateFiles(folderPath);
                foreach (var previewFile in previewFiles)
                {
                    var timeStamp = File.GetLastWriteTime(previewFile);
                    var span = timeStamp - System.DateTime.Now;
                    if (span.CompareTo(a) < 0)
                    {
                        File.Delete(previewFile);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// フォームがアクティブになった際の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShougunMenu_Activated(object sender, System.EventArgs e)
        {

            ///プロテクト/限定メニューの時は、他の画面に遷移しないようにお知らせ欄を空欄にする
            if (!AppConfig.ProtectMode)
            {
                // お知らせ欄を空にする
                this.tsuuchiJouhou1.Visible = false;
                return;
            }

            try
            {
                // 通知情報の更新
                if (!this.tsuuchiJouhou1.Reload())
                {
                    // 失敗（アセンブリ無）の場合はイベントハンドラを解除してメッセージを表示する
                    this.Activated -= new System.EventHandler(this.ShougunMenu_Activated);
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow(this.errorCd1, this.topHeNoJouhouKoukai);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                // メッセージが設定されているかどうかチェック
                this.tsuuchiJouhou1.Visible = this.tsuuchiJouhou1.MessageCount > 0;
            }
        }

        #region CTI連携

        /// <summary>CTI連携テキストファイル監視用</summary>
        private System.Windows.Forms.Timer ctiFileWatchTimer = null;
        private DateTime lastCtiFileWriteTime = DateTime.Now;

        /// <summary>CTI連携設定</summary>
        private string ctiUse = string.Empty;
        private string ctiFileFullPath = string.Empty;
        private short ctiFileDetectTime = 1000;

        /// <summary>ファイル読み込みに失敗した場合に</summary>
        private bool detectAuthError = false;

        /// <summary>
        /// CTI連携テキストファイル監視イベント追加処理
        /// </summary>
        private void MonitoringCTIRenkeiFile()
        {
            try
            {
                // オプションが有効かつ、CurrentUserCustomConfigProfile.xmlのuse=1の場合にファイル監視実行。
                if (r_framework.Configuration.AppConfig.AppOptions.IsCti())
                {
                    this.ctiUse = string.Empty;
                    this.ctiFileFullPath = string.Empty;

                    CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();

                    if (userProfile.Settings.CtiSettings != null
                        && userProfile.Settings.CtiSettings.Values != null)
                    {
                        // CurrentUserCustomConfigProfile.xmlのUse
                        this.ctiUse = userProfile.Settings.CtiSettings.Values.Use;

                        // CurrentUserCustomConfigProfile.xmlのFilePath
                        this.ctiFileFullPath = userProfile.Settings.CtiSettings.Values.FilePath;

                        // CurrentUserCustomConfigProfile.xmlのFileDetectTime
                        short tempctiFileDetectTime = 1000;
                        if (short.TryParse(userProfile.Settings.CtiSettings.Values.FileDetectTime, out tempctiFileDetectTime))
                        {
                            this.ctiFileDetectTime = tempctiFileDetectTime;
                        }
                    }

                    // CurrentUserCustomConfigProfile.xmlのuse=1の場合以外、ファイル監視実行しない
                    if (!this.ctiUse.Equals("1"))
                    {
                        return;
                    }

                    // Timer初期化
                    ctiFileWatchTimer = null;
                    this.lastCtiFileWriteTime = DateTime.Now;

                    // ファイルが存在していればファイルの最終更新日を取得
                    // クラウド環境の場合、サーバーとクライアントマシンとで時刻がずれている可能性があるため
                    if (string.IsNullOrEmpty(this.ctiFileFullPath)
                        || !File.Exists(this.ctiFileFullPath))
                    {
                        lastCtiFileWriteTime = File.GetLastWriteTime(this.ctiFileFullPath);
                    }

                    ctiFileWatchTimer = new System.Windows.Forms.Timer();
                    ctiFileWatchTimer.Tick += new EventHandler(this.OnTick_ctiFileWatchTimer);
                    ctiFileWatchTimer.Interval = this.ctiFileDetectTime;
                    ctiFileWatchTimer.Start();
                }
            }
            catch (Exception ex)
            {
                // バッググラウンドで動かすので画面にはエラーは表示せず、ログにのみ情報を出力
                LogUtility.Error("Error", ex);
            }
        }

        /// <summary>
        /// Timerのポーリングイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTick_ctiFileWatchTimer(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.ctiFileFullPath)
                    || !File.Exists(this.ctiFileFullPath))
                {
                    return;
                }

                // ファイル更新チェック
                var tn = File.GetLastWriteTime(this.ctiFileFullPath);
                if (tn.CompareTo(this.lastCtiFileWriteTime) <= 0)
                {
                    // ファイルの更新が無い。
                    return;
                }

                // 比較用の日付を更新
                this.lastCtiFileWriteTime = tn;

                // ファイル解析 & データ検索
                this.FileWatcher_Action(@ctiFileFullPath);
                this.detectAuthError = false;
            }
            catch (Exception ex)
            {
                // 繰り返し発生する可能性があるため、連続で検出しないようにする。
                if (!this.detectAuthError)
                {
                    // バッググラウンドで動かすので画面にはエラーは表示せず、ログにのみ情報を出力
                    LogUtility.Error("Error", ex);
                }

                this.detectAuthError = true;
            }
        }

        /// <summary>
        /// CTI連携テキストファイル更新
        /// </summary>
        /// <param name="fullPath">CTI連携テキストファイル</param>
        /// <param name="firstAction">初期処理</param>
        private void FileWatcher_Action(string fullPath)
        {
            string telNum = string.Empty;

            var targetFile = new FileInfo(fullPath);

            #region CTI連携テキストファイル
            //1	 電話番号 環境将軍RのDBに登録されている電話番号が設定させる。
            //2  種類  1：取引先、2.業者、3現場
            //3  取引先CD 環境将軍Rのマスタに設定されている取引先CDが設定される。
            //4  業者CD  環境将軍Rのマスタに設定されている業者CDが設定される。
            //5  現場CD  環境将軍Rのマスタに設定されている現場CDが設定される。
            string[] ctiArray = new string[6];
            using (var streamReader = new StreamReader(fullPath, System.Text.Encoding.GetEncoding("shift_jis")))
            {
                int i = 0;
                while (streamReader.Peek() >= 0)
                {
                    // TODO: 解析方法、電話番号の整形については別途連絡予定
                    ctiArray[i] = streamReader.ReadLine();
                    i++;
                }
            }
            #endregion

            // 電話番号(1行目)が存在しない場合は何もしない。
            if (string.IsNullOrEmpty(ctiArray[0]))
            {
                return;
            }

            //DAOのインスタンス
            this.TorihikisakiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKIDao>();
            this.GyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            this.GenbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();

            // DBを検索する
            DataTable dataResult = null;
            if (ctiArray[1] == "1") // 1：取引先
            {
                //DAOのインスタンス
                dataResult = TorihikisakiDao.GetCtiRenkeiData(ctiArray[0], ctiArray[2], ctiArray[1]);
                DataResultSet(dataResult);
            }
            else if (ctiArray[1] == "2")  // 2:業者
            {
                //DAOのインスタンス
                dataResult = GyoushaDao.GetCtiRenkeiData(ctiArray[0], ctiArray[3], ctiArray[1]);
                DataResultSet(dataResult);
            }
            else if (ctiArray[1] == "3")  // 3:現場
            {
                //DAOのインスタンス
                dataResult = GenbaDao.GetCtiRenkeiData(ctiArray[0], ctiArray[4], ctiArray[3], ctiArray[1]);
                DataResultSet(dataResult);
            }
            else
            {
                var messageShowLogic = new MessageBoxShowLogic();

                //DAOのインスタンス
                // 現場
                dataResult = GenbaDao.GetCtiRenkeiData(ctiArray[0], string.Empty, string.Empty, ctiArray[1]);

                if (dataResult != null && dataResult.Rows.Count > 1)
                {

                    messageShowLogic.MessageBoxShowInformation("取引先、業者、現場を特定できませんでした。");

                    return;
                }
                else if (dataResult != null && dataResult.Rows.Count == 1)
                {
                    DataResultSet(dataResult);
                    return;
                }

                //DAOのインスタンス
                // 業者
                dataResult = GyoushaDao.GetCtiRenkeiData(ctiArray[0], string.Empty, ctiArray[1]);

                if (dataResult != null && dataResult.Rows.Count > 1)
                {

                    messageShowLogic.MessageBoxShowInformation("取引先、業者、現場を特定できませんでした。");

                    return;
                }
                else if (dataResult != null && dataResult.Rows.Count == 1)
                {
                    DataResultSet(dataResult);
                    return;
                }

                //DAOのインスタンス
                // 取引先
                dataResult = TorihikisakiDao.GetCtiRenkeiData(ctiArray[0], string.Empty, ctiArray[1]);

                if (dataResult == null || dataResult.Rows.Count == 0)
                {

                    messageShowLogic.MessageBoxShow("C001");

                    return;
                }
                else if (dataResult != null && dataResult.Rows.Count > 1)
                {

                    messageShowLogic.MessageBoxShowInformation("取引先、業者、現場を特定できませんでした。");

                    return;
                }
                else if (dataResult != null && dataResult.Rows.Count == 1)
                {
                    DataResultSet(dataResult);

                    return;
                }
            }
        }

        /// <summary>
        /// CTI連携検索結果設定
        /// </summary>
        /// <param name="source"></param>
        private void DataResultSet(DataTable dataResult)
        {
            if (dataResult == null || dataResult.Rows.Count == 0)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("C001");

            }
            else if (dataResult.Rows.Count == 1)
            {
                // 顧客カルテ
                string strFormId = "G173";
                DataRow row = dataResult.Rows[0];
                // 引数 arg[0] : 取引先CD
                string torihikisakiCd = row["TORIHIKISAKI_CD"] == null ? string.Empty : row["TORIHIKISAKI_CD"].ToString();
                // 引数 arg[1] : 業者CD
                string gyoushaCd = row["GYOUSHA_CD"] == null ? string.Empty : row["GYOUSHA_CD"].ToString();
                // 引数 arg[2] : 現場CD
                string genbaCd = row["GENBA_CD"] == null ? string.Empty : row["GENBA_CD"].ToString();
                //フォーム起動
                FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, torihikisakiCd, gyoushaCd, genbaCd);
            }

        }
        #endregion

        /// <summary>
        /// メインメニューをシリーズに合わせて初期化します
        /// </summary>
        private void SetSeriesThema()
        {
            if (this.ribbonForm == null)
            {
                return;
            }

            this.SuspendLayout();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(ShougunMenu));

            switch (AppConfig.AppType)
            {
                case AppConfig.AppTypes.HaishaShougun:
                    this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("haisha_logo")));
                    this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("haisha_bg")));
                    this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
                    break;
                case AppConfig.AppTypes.ManifestShougun:
                    this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("manifest_logo")));
                    this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("manifest_bg")));
                    this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(239)))), ((int)(((byte)(235)))));
                    break;
                case AppConfig.AppTypes.KeiryouShougun:
                    this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("keiryo_logo")));
                    this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("keiryo_bg")));
                    this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
                    break;
                default:
                    this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("kankyou_logo")));
                    this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("kankyou_bg")));
                    this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
                    break;
            }

            // メインメニュータイトルの設定
            // format) 製品名(シリーズ識別子) CAL数 接続DB名 ログインユーザー名
            // ex) 環境将軍R(A1) 10CAL 株式会社エジソン 浅沼啓太
            var windowTitle = string.Format("{0}({1}) {2}CAL {3} {4}",
                    AppConfig.AppName,       // アプリケーション名
                    AppConfig.Series,        // シリーズ識別子
                    AppConfig.UserCal,       // CAL数
                    AppData.LastLoginDbName, // 接続DB名
                    r_framework.Dto.SystemProperty.UserName);   // ログインユーザー名
            r_framework.Dto.SystemProperty.SetCommonWindowTitleText(windowTitle);

            this.Text = windowTitle;
            this.Tag = AppConfig.AppName;
            r_framework.Dto.SystemProperty.NameFormName = this.Text;
            this.ResumeLayout(false);
            this.PerformLayout();
        }


        private void VersionLabel_DoubleClick(object sender, EventArgs e)
        {
            Shougun.Core.Common.Login.Program.ShowVersionInfoDialog();
        }

        /// <summary>
        /// ログ保存ポップアップの呼出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveLogLabel_DoubleClick(object sender, EventArgs e)
        {
            using (var slf = new SaveLogFilePopup())
            {
                slf.StartPosition = FormStartPosition.CenterParent;
                slf.ShowDialog();
                slf.Dispose();
            }
        }

        /// <summary>
        /// 非アクティブ時のイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            // クラウド版でマスタメニューを開いたまま、
            // 他のアプリをアクティブにした時、
            // マスタメニューが残ってしまう対応
            if (!AppConfig.IsForeground() && AppConfig.IsTerminalMode)
            {
                ((RibbonMainMenu)this.ribbonForm).CloseOrbDropDown();
            }
        }
        //CongBinh 20210712 #152800 S
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblOpenLinkSupport_Click(object sender, EventArgs e)
        {
            if (this.commonInfo.SysInfo.SUPPORT_KBN == 1)
            {
                try
                {
                    System.Diagnostics.Process.Start(this.commonInfo.SysInfo.SUPPORT_URL);
                }
                catch
                {
                }
            }
        }
        //CongBinh 20210712 #152800 E
    }
}
