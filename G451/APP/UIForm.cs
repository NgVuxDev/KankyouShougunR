using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Utility;
using Seasar.Framework.Container.Factory;
using Seasar.Framework.Container;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

namespace Shougun.Core.Common.Login
{
    /// <summary>
    /// 画面表示完了デリゲート
    /// </summary>
    public delegate void DisplayCompletionEventHandler();

    /// <summary>
    /// ログイン画面クラス
    /// </summary>
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// ユーザーログインのDao
        /// </summary>
        private IT_USER_LOGINDao userLoginDao;

        /// <summary>
        /// 画面表示完了イベント
        /// </summary>
        public event DisplayCompletionEventHandler OnDisplayCompleted;

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// DB接続先コンボボックスの選択中のインデックス
        /// </summary>
        private int selectedDatabaseComboBoxIndex;

        /// <summary>
        /// フォームコンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.LOGIN)
        {
            this.InitializeComponent();

            this.logic = new LogicClass(this);

            this.LoginIDTextbox.CausesValidation = true;
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            // カーソルを待機カーソルに変更
            Cursor.Current = Cursors.WaitCursor;

            this.userLoginDao = DaoInitUtility.GetComponent<IT_USER_LOGINDao>();

            try
            {
                base.OnLoad(e);

                this.OKButton.Click += this.OKButton_Click;
                this.CancelButton.Click += (obj, ev) => { this.Close(); };

                this.LoginIDTextbox.Validating += this.LoginIDTextbox_Validating;

                // GetCodeMasterField Control.Leave時に行われるチェック処理用。MasterAccessで使われる。FocusOutCheckMethodとセット。～CD固定のため今回不要
                // this.LoginIDTextbox.GetCodeMasterField = "";
                this.LoginIDTextbox.PopupWindowId = WINDOW_ID.M_SHAIN;
                this.LoginIDTextbox.PopupGetMasterField = "login_id,shain_name_ryaku";
                //// this.LoginIDTextbox.DBFieldsName = "login_id";

                var dblist = this.logic.GetDBConnectionList();
                if (dblist.Count() == 0)
                {
                    MessageBox.Show("データベース定義がありません", Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.SetAllControlsEnabled(false);
                    this.DatabaseCombobox.Enabled = false;
                    this.CancelButton.Focus();
                }
                else
                {
                    this.DatabaseCombobox.DisplayMember = "DispName";
                    this.DatabaseCombobox.ValueMember = "ConnectionString";
                    this.DatabaseCombobox.DataSource = dblist;
                }

                // 前回または先頭のDBを選択状態にする
                var dbcurrentConfig = dblist.Where(s => s.DispName.Equals(AppData.LastLoginDbName)).FirstOrDefault();
                if (dbcurrentConfig == null)
                {
                    dbcurrentConfig = dblist.First();
                }

                // AppData/ローカルファイル/ログ出力先の切り替え
                if (!AppData.PrepareLocalDataFiles(dbcurrentConfig.DispName))
                {
                    this.SetAllControlsEnabled(false);
                    this.DatabaseCombobox.Enabled = false;
                    this.CancelButton.Focus();
                }

                // 前回ログイン情報を取得
                var loginInfo = this.logic.GetLoginInfo();
                this.LoginIDTextbox.Text = loginInfo.Code;
                this.PassWordTextbox.Text = loginInfo.Pwd;
                this.PasswordSaveCheckbox.Checked = loginInfo.PwdSaved;


                this.DatabaseCombobox.SelectedItem = dbcurrentConfig;

                if (dbcurrentConfig.CanConnect())
                {
                    // S2コンテナーとマスタ情報をロード
                    this.S2ContainerReload(dbcurrentConfig.ConnectionString);
                    this.FormReload();

                    this.LoginIDTextbox_Validating(this.LoginIDTextbox, new CancelEventArgs(false));
                }
                else if (dblist.Count == 1)
                {
                    // 接続先設定が１つかつ接続できない場合、全てのコントロールを無効にする。
                    MessageBox.Show("指定された接続先データベースに接続出来ませんでした。", "システムエラー", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.SetAllControlsEnabled(false);
                    this.DatabaseCombobox.Enabled = false;
                    this.CancelButton.Focus();
                }
                else
                {
                    // 接続不可の場合、DatabaseCombobox以外を無効に。
                    MessageBox.Show("指定された接続先データベースに接続出来ませんでした。", "システムエラー", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.SetAllControlsEnabled(false);
                    this.DatabaseCombobox.Focus();
                }

                this.DatabaseCombobox.GotFocus += this.DatabaseCombobox_GotFocus;
                this.DatabaseCombobox.SelectionChangeCommitted += this.DatabaseCombobox_SelectionChangeCommitted;
                this.DatabaseCombobox.Leave += this.DatabaseCombobox_Leave;
            }
            catch (Exception ex)
            {
                this.SetAllControlsEnabled(false);
                this.DatabaseCombobox.Enabled = false;
                this.CancelButton.Focus();
                throw ex;
            }

            // カーソルを元に戻す
            Cursor.Current = Cursors.Default;

            // 画面表示完了のイベントを呼び出す(スプラッシュスクリーンが閉じる)
            if (OnDisplayCompleted != null)
            {
                OnDisplayCompleted();
            }
        }

        /// <summary>
        /// Windowsメッセージ処理
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x112;
            const int SC_CLOSE = 0xF060;

            // 閉じる[x]ボタン時はValidate停止
            if (m.Msg == WM_SYSCOMMAND && m.WParam.ToInt32() == SC_CLOSE)
            {
                this.AutoValidate = AutoValidate.Disable;
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// ログインボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, EventArgs e)
        {
            if (this.LoginIDTextbox.Text.Trim().Length == 0)
            {
                MessageBox.Show("ログインユーザーIDが未入力です。", Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.LoginIDTextbox.Focus();
                return;
            }

            var currentUser = this.logic.GetLoginUser(this.LoginIDTextbox.Text.Trim(), this.PassWordTextbox.Text.Trim());
            if (currentUser == null)
            {
                MessageBox.Show("パスワードが正しくありません。\nもう一度入力してください。", Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.PassWordTextbox.Focus();
                return;
            }
            else
            {
                // ログにバージョン情報出力
                LogUtility.Info(string.Format("Base Version: {0}", AppConfig.BaseVersion.ToString()));
                if (AppConfig.IsCustomized)
                {
                    LogUtility.Info(string.Format("Customize Version: {0}", AppConfig.CustomizeVersion.ToString()));
                }
                LogUtility.Info(string.Format("Configuration: TerminalMode={0}, Series={1}, User Cal={2}",
                            AppConfig.IsTerminalMode, AppConfig.Series, AppConfig.UserCal));

                LogUtility.Info(string.Format("Application: Name={0}, Type={1}",
                            AppConfig.AppName, AppConfig.AppType.ToString()));

                try
                {
                    // デフォルトログイン情報更新
                    this.logic.UpdateXmlLoginInfo(this.LoginIDTextbox.Text, this.PassWordTextbox.Text, this.PasswordSaveCheckbox.Visible & this.PasswordSaveCheckbox.Checked);

                    // デフォルト選択DB情報更新
                    var dto = (DBConnectionDTO)this.DatabaseCombobox.SelectedItem;
                    AppData.LastLoginDbName = dto.DispName;

                    // ログにログイン情報出力
                    LogUtility.Info(string.Format("Login: Database={0}, Login User={1}({2})", dto.DispName, currentUser.SHAIN_NAME_RYAKU, currentUser.LOGIN_ID));

                    // クラウド時は帳票関連を書き換え
                    if (r_framework.Configuration.AppConfig.IsTerminalMode)
                    {
                        // 設定のプリンタ名
                        this.logic.ReplaceRedirectPrinterName();

                        // DBから取得したテンプレートファイル名
                        this.logic.ReplaceTemplatePathToCloud();
                    }
                }
                catch (System.IO.IOException ex)
                {
                    MessageBox.Show("設定ファイルの保存に失敗しました。\r\n" + ex.Message, Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // ログインユーザー情報設定
                SystemProperty.UserName = currentUser.SHAIN_NAME_RYAKU;
                SystemProperty.SetCurrentShain(currentUser);
                SystemProperty.PrintSettings.SetReportList(this.logic.ReportListPrinterSettings);

                // 最大表示画面数設定
                var maxWindow = this.logic.GetMaxWindowCountByShain(currentUser.SHAIN_CD);
                SystemProperty.SetMaxWindowCount(maxWindow);

                // 権限情報をログインユーザーの部署CDと社員CD（null・空白含む）で絞り込み
                var currentMenuAuth = this.logic.MenuAuthorizationAll.Where(n => n.BUSHO_CD == currentUser.BUSHO_CD && (n.SHAIN_CD == currentUser.SHAIN_CD || string.IsNullOrWhiteSpace(n.SHAIN_CD))).ToArray();
                SystemProperty.Shain.SetAuth(currentMenuAuth);

                // 20141211 ブン お気に入りモッド start
                var currentBookmark = this.logic.BookmarkAll.Where(n => n.BUSHO_CD == currentUser.BUSHO_CD && n.SHAIN_CD == currentUser.SHAIN_CD && !n.MY_FAVORITE.IsNull).ToArray();
                SystemProperty.Shain.SetBookmark(currentBookmark);
                // 20141211 ブン お気に入りモッド end

                // ユーザー利用制限対応 2014.10.14
                string errorMessage;
                if (!this.logic.CanLogin(this.LoginIDTextbox.Text.Trim(), out errorMessage))
                {
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        MessageBox.Show(errorMessage, Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    return;
                }

                //Communicate InxsSubApplication Start
                if (r_framework.Configuration.AppConfig.AppOptions.IsInxs())
                {
                    //Update all views
                    this.logic.RefreshAllViews();

                    //Get AppSettingsInxs
                    DataTable tbDataInxs = this.logic.GetAppSettingInxs(currentUser.SHAIN_CD);
                    if (tbDataInxs == null || tbDataInxs.Rows.Count == 0)
                    {
                        MessageBox.Show("INXSサブアプリケーションが規定の場所に存在しません。システム管理者に連絡してください。", Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    SystemProperty.SetAppSettingInxs(tbDataInxs);
                }
                //Communicate InxsSubApplication End

                // リモートの場合のみ
                if (AppConfig.IsTerminalMode)
                {
                    // セッション数を確認する
                    if (!this.logic.ShellCmd(out errorMessage))
                    {
                        if (!string.IsNullOrEmpty(errorMessage))
                        {
                            MessageBox.Show(errorMessage, Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        return;
                    }
                }

                // LogDBへ接続
                logic.ConnectToLogDB(); // 20211229 ログイン情報

                // ログにVersion情報出力
                LogUtility.Info(Environment.NewLine +
                                "================= バージョン情報 =================" +
                                Environment.NewLine +
                                AppConfig.VersionInfo +
                                "================= バージョン情報 =================");
                // ユーザー利用制限対応 2014.10.14

                // メニュー表示前にhtmlファイルを削除する
                MapboxGLJSLogic gljsLogic = new MapboxGLJSLogic();
                gljsLogic.htmlFileDelete();

                // Menu表示
                new Menu.ShougunMenu(new CommonInformation(this.logic.SystemInformation, this.logic.CorpInformation, currentUser)).Show();

                this.Hide();
            }
        }

        /// <summary>
        /// <para>ログインIDチェック。自前でチェック。</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginIDTextbox_Validating(object sender, CancelEventArgs e)
        {
            // キャンセルボタン時は何もしない
            if (this.ActiveControl == this.CancelButton)
            {
                return;
            }

            // 未入力時はログインユーザー名をクリア
            if (this.LoginIDTextbox.Text.Trim().Length == 0)
            {
                this.LoginNameTextbox.Clear();
                return;
            }

            var shortName = this.logic.ShainAll.Where(s => s.LOGIN_ID == this.LoginIDTextbox.Text.Trim()).Select(s => s.SHAIN_NAME_RYAKU).FirstOrDefault();
            if (shortName == null)
            {
                e.Cancel = true;

                MessageBox.Show("社員マスタに存在しないコードが入力されました。", Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                this.LoginIDTextbox.SelectAll();
                this.LoginNameTextbox.Clear();
            }
            else
            {
                this.LoginNameTextbox.Text = shortName;
            }
        }

        /// <summary>
        /// 接続先データベースコンボボックスフォーカスイン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseCombobox_GotFocus(object sender, EventArgs e)
        {
            // 現在選択インデックスを保存する
            this.selectedDatabaseComboBoxIndex = this.DatabaseCombobox.SelectedIndex;
        }

        /// <summary>
        /// 接続先データベースコンボボックスクローズ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseCombobox_Leave(object sender, EventArgs e)
        {
            this.CheckSelectedDatabaseConnection();
        }

        /// <summary>
        /// 接続先データベースコンボボックス変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseCombobox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.CheckSelectedDatabaseConnection();
        }

        /// <summary>
        /// データベース接続チェック
        /// </summary>
        private void CheckSelectedDatabaseConnection()
        {
            // 変更されなかった場合はチェックしない
            if (this.DatabaseCombobox.SelectedIndex == this.selectedDatabaseComboBoxIndex)
            {
                return;
            }

            // 選択インデックス登録
            this.selectedDatabaseComboBoxIndex = this.DatabaseCombobox.SelectedIndex;

            // カーソルを待機カーソルに変更
            Cursor.Current = Cursors.WaitCursor;

            // AppData/ローカルファイル/ログ出力先の切り替え
            var dto = (DBConnectionDTO)this.DatabaseCombobox.SelectedItem;
            if (!AppData.PrepareLocalDataFiles(dto.DispName))
            {
                this.SetAllControlsEnabled(false);
            }
            else if (dto.CanConnect())
            {
                // 前回ログイン情報を取得
                var loginInfo = this.logic.GetLoginInfo();
                this.LoginIDTextbox.Text = loginInfo.Code;
                this.PassWordTextbox.Text = loginInfo.Pwd;
                this.PasswordSaveCheckbox.Checked = loginInfo.PwdSaved;
                this.LoginNameTextbox.Clear();

                // S2コンテナーとマスタ情報をリロード
                this.S2ContainerReload(dto.ConnectionString);
                this.FormReload();

                // ログインIDから将軍ユーザー名前を表示
                this.LoginIDTextbox_Validating(this.LoginIDTextbox, new CancelEventArgs(false));

                // 各コントロールの有効化
                this.SetAllControlsEnabled(true);

                // ログインユーザーIDテキストボックスにフォーカス
                this.LoginIDTextbox.Focus();
            }
            else
            {
                MessageBox.Show("指定された接続先データベースに接続出来ませんでした。", Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                // 各コントロールの無効化
                this.SetAllControlsEnabled(false);

                // 接続先データベースコンボボックスにフォーカス
                this.DatabaseCombobox.Focus();
                this.DatabaseCombobox.SelectionLength = 0;
            }

            // カーソルを元に戻す
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// パスワード関連の画面制御(M_SYS_INFOを取得していること)
        /// </summary>
        private void SetPassWordOption()
        {
            if (this.logic.SystemInformation.SYS_PWD_SAVE_KBN.IsNull)
            {
                this.PasswordSaveCheckbox.Visible = false;
            }
            else
            {
                this.PasswordSaveCheckbox.Visible = (int)this.logic.SystemInformation.SYS_PWD_SAVE_KBN == 1;
            }

            if ((int)this.logic.SystemInformation.COMMON_PASSWORD_DISP == 2)
            {
                this.PassWordTextbox.PasswordChar = '*';
            }
        }

        /// <summary>
        /// 画面項目の再ロード(DBに接続できること)
        /// </summary>
        private void FormReload()
        {
            this.logic.LoadMasterData();

            if (this.logic.SystemInformation != null)
            {
                this.SetPassWordOption();
            }

            var shainDataTable = this.logic.GetPopUpShainData(this.LoginIDTextbox.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()));

            this.LoginIDTextbox.PopupDataHeaderTitle = new string[] { "ログインID", "社員略称名" };
            this.LoginIDTextbox.PopupDataSource = shainDataTable;
            this.SHAIN_SEARCH_BUTTON.PopupDataHeaderTitle = new string[] { "ログインID", "社員略称名" };
            this.SHAIN_SEARCH_BUTTON.PopupDataSource = shainDataTable;
        }

        /// <summary>
        /// S2Containerの再作成
        /// </summary>
        /// <param name="connectionString">接続文字列</param>
        private void S2ContainerReload(string connectionString)
        {
            // DB接続文字列の動的設定
            var container = (IS2Container)SingletonS2ContainerFactory.Container.GetComponent(Constans.DAO);
            var dataSource = (Seasar.Extension.Tx.Impl.TxDataSource)container.GetComponent("DataSource");
            dataSource.ConnectionString = connectionString;
        }

        /// <summary>
        /// キャンセルボタン、データベースコンボボックスを除く全てのタブ移動可能なコントロールを
        /// 一括で有効/無効にします。
        /// </summary>
        /// <param name="set"></param>
        private void SetAllControlsEnabled(bool set)
        {
            var controls = this.Controls.Cast<Control>().Where(n => n.TabStop && n != this.DatabaseCombobox && n != this.CancelButton);
            foreach (Control control in controls)
            {
                control.Enabled = set;
            }
        }

        /// <summary>
        /// ボタンをEnterで実行可能に。基底クラスのOnKeyDownは実行しない。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            //base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (e.KeyCode == Keys.Enter && this.ActiveControl == this.OKButton)
                {
                    this.OKButton.PerformClick();
                }
                else if (e.KeyCode == Keys.Enter && this.ActiveControl == this.CancelButton)
                {
                    this.CancelButton.PerformClick();
                }
                else
                {
                    this.ProcessTabKey(!e.Shift);
                }
            }
        }

        /// <summary>
        /// 切断ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (AppConfig.IsTerminalMode)
            {
                DeleteLoginDataCloud();
            }
            else
            {
                DeleteLoginDataOnpre();
            }
        }

        #region クラウドデータ削除機能
        /// <summary>
        /// ログインデータを削除(クラウド)
        /// </summary>
        private void DeleteLoginDataCloud()
        {
            string selectQuery = "SELECT * FROM T_USER_LOGIN WHERE LOGIN_TIME = " +
                                 "(SELECT MIN(LOGIN_TIME) FROM T_USER_LOGIN WHERE LOGIN_COUNTER > 0)";
            DataTable userLoginDt = userLoginDao.GetDateForStringSql(selectQuery);

            if (userLoginDt.Rows.Count == 0)
            {
                MessageBox.Show("現在ログイン中のユーザーはいません。",
                                "インフォメーション",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            LogUtility.Debug(Environment.NewLine +
                             "============ 削除対象ログイン情報 ============" +
                             Environment.NewLine +
                             "ログインID : " + userLoginDt.Rows[0]["LOGIN_ID"].ToString() +
                             Environment.NewLine +
                             "ユーザー名 : " + userLoginDt.Rows[0]["CLIENT_USER_NAME"].ToString() +
                             Environment.NewLine +
                             "============ 削除対象ログイン情報 ============");

            if (MessageBox.Show("以下現在ログイン中でログイン時間が最も古いログインID、ユーザー名です。" +
                                Environment.NewLine +
                                Environment.NewLine +
                                "ログインID : " + userLoginDt.Rows[0]["LOGIN_ID"].ToString() +
                                ", ユーザー名 : " + userLoginDt.Rows[0]["CLIENT_USER_NAME"].ToString() +
                                Environment.NewLine +
                                Environment.NewLine +
                                "削除してもよろしいですか?",
                                "確認",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // 削除SQL実行処理
                string loginId = SqlCreateUtility.CounterplanEscapeSequence2(userLoginDt.Rows[0]["LOGIN_ID"].ToString());
                string userName = SqlCreateUtility.CounterplanEscapeSequence2(userLoginDt.Rows[0]["CLIENT_USER_NAME"].ToString());
                string delQuery = "DELETE FROM T_USER_LOGIN WHERE LOGIN_ID = '" +
                                  loginId +
                                  "' AND CLIENT_USER_NAME = '" +
                                  userName + "'";
                int resultCnt = userLoginDao.DeleteDateForStringSql(delQuery);
                MessageBox.Show(resultCnt + "件、削除が完了しました。",
                                "通知",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }
        #endregion クラウドデータ削除機能

        #region オンプレデータ削除機能
        /// <summary>
        /// ログイン情報を削除(オンプレ)
        /// </summary>
        private void DeleteLoginDataOnpre()
        {
            // 削除対象データ格納
            List<LoginInfo> deleteList = new List<LoginInfo>();

            try
            {
                deleteList = GetTargetDeleteList();

                // 削除対象データが0件の場合
                if (deleteList.Count == 0)
                {
                    MessageBox.Show("データベースに残っているログイン情報データはありません。",
                                    "インフォメーション",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return;
                }

                // メッセージボックスに表示する形式に変換
                string strTargetDelete = CreateTargetDeleteInfo(deleteList);

                LogUtility.Debug(Environment.NewLine +
                                 "============ 削除対象ログイン情報 ============" +
                                 Environment.NewLine +
                                 strTargetDelete +
                                 "============ 削除対象ログイン情報 ============");

                if (MessageBox.Show("以下データベースに残っているログイン情報(ログインID、PC名)です。" + Environment.NewLine +
                                    Environment.NewLine + strTargetDelete + Environment.NewLine + "削除してもよろしいですか?",
                                    "確認",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // 削除SQL実行処理
                    int resultCnt = userLoginDao.DeleteDateForStringSql("DELETE FROM T_USER_LOGIN WHERE " + CreateWhereSql(deleteList));
                    MessageBox.Show(resultCnt + "件、削除が完了しました。",
                                    "通知",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
            catch (Exception e)
            {
                LogUtility.Debug(e);
                throw e;
            }
        }

        /// <summary>
        /// 削除対象データ取得
        /// </summary>
        /// <returns>削除対象List</returns>
        private List<LoginInfo> GetTargetDeleteList()
        {
            // セッション情報(sp_who)格納
            DataTable sessionDt = new DataTable();
            // ログインユーザーテーブル(T_USER_LOGIN)格納
            DataTable userLoginDt = new DataTable();
            // 削除対象データ格納
            List<LoginInfo> deleteList = new List<LoginInfo>();

            // セッション情報
            sessionDt = userLoginDao.GetDateForStringSql("exec sp_who");
            DataRow[] sessionDr = sessionDt.Select("TRIM(hostname) <> '' AND dbname <> 'master'");
            // セッション情報から取得したホスト名一覧格納用
            ArrayList hostNameArrayList = new ArrayList();

            for (int i = 0; i < sessionDr.Count(); i++)
            {
                if (!hostNameArrayList.Contains(sessionDr[i]["hostname"].ToString().Trim()))
                {
                    hostNameArrayList.Add(sessionDr[i]["hostname"].ToString().Trim());
                }
            }

            // ユーザーログインテーブル情報(ログイン中のデータを取得)
            userLoginDt = userLoginDao.GetDateForStringSql("SELECT * FROM T_USER_LOGIN WHERE LOGIN_COUNTER > 0");

            foreach (DataRow userLoginDr in userLoginDt.Rows)
            {
                // セッション情報(hostname)にユーザーログインテーブルのPC名が含まれていない場合、削除対象
                if (!hostNameArrayList.Contains(userLoginDr["CLIENT_COMPUTER_NAME"].ToString()))
                {
                    LoginInfo li = new LoginInfo();
                    li.loginID = userLoginDr["LOGIN_ID"].ToString();
                    li.computerName = userLoginDr["CLIENT_COMPUTER_NAME"].ToString();

                    deleteList.Add(li);
                }
            }

            return deleteList;
        }

        /// <summary>
        /// Where文を作成する
        /// </summary>
        /// <param name="loginInfoList">削除対象データ格納List</param>
        /// <returns>条件文</returns>
        private string CreateWhereSql(List<LoginInfo> loginInfoList)
        {
            ArrayList queryArray = new ArrayList();

            for (int i = 0; i < loginInfoList.Count; i++)
            {
                string computerName = SqlCreateUtility.CounterplanEscapeSequence2(loginInfoList[i].computerName);
                queryArray.Add("CLIENT_COMPUTER_NAME = '" + computerName + "'");
            }

            return string.Join(" OR ", queryArray.ToArray());
        }

        /// <summary>
        /// 削除対象になるログインデータ情報を作成する
        /// </summary>
        /// <param name="loginInfoList">削除対象データ格納List</param>
        /// <returns>削除対象ログインデータ情報</returns>
        private string CreateTargetDeleteInfo(List<LoginInfo> loginInfoList)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < loginInfoList.Count; i++)
            {
                if (i == 10)
                {
                    sb.AppendLine("削除対象データが11件以上存在します。");
                    break;
                }
                sb.AppendLine("ログインID : " + loginInfoList[i].loginID +
                              ", PC名 : " + loginInfoList[i].computerName);
            }

            return sb.ToString();
        }

        public void BtnPopupMethod()
        {
            this.LoginIDTextbox.Focus();
        }

        /// <summary>
        /// ログイン情報保持クラス
        /// </summary>
        private class LoginInfo
        {
            public string loginID { get; set; }
            public string computerName { get; set; }
        }
        #endregion オンプレデータ削除機能
    }
}
