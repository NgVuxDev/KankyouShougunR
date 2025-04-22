using System;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Stock.ZaikoShimeSyori.Const;
using Shougun.Core.Stock.ZaikoShimeSyori.Entity;
using r_framework.Dto;
using Shougun.Core.Stock.ZaikoShimeSyori.DTO;

namespace Shougun.Core.Stock.ZaikoShimeSyori.APP
{
    public partial class F18_G170UIForm : SuperForm
    {

        #region フィールド

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private F18Logic logic;

        /// <summary>
        /// メッセージロジック
        /// </summary>
        private readonly MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        /// <summary>
        /// 検索条件backup：業者コード
        /// </summary>
        private String condition_backup_gyosyaCD { get; set; }
        /// <summary>
        /// 検索条件backup：業者名
        /// </summary>
        private String condition_backup_gyosyaNM { get; set; }
        /// <summary>
        /// 検索条件backup：現場コード
        /// </summary>
        private String condition_backup_genbaCD { get; set; }
        /// <summary>
        /// 検索条件backup：現場名
        /// </summary>
        private String condition_backup_genbaNM { get; set; }
        /// <summary>
        /// 検索条件：締対象期間from(月の初日)
        /// </summary>
        private DateTime condition_simeTaisyouKikanFrom { get; set; }
        /// <summary>
        /// 検索条件：締対象期間to(月の最終日)
        /// </summary>
        private DateTime condition_simeTaisyouKikanTo { get; set; }
        /// <summary>
        /// 検索結果：締情報の検索結果
        /// </summary>
        private DataTable result_simeiInfo { get; set; }

        /// <summary>
        /// 編集条件：評価方法
        /// </summary>
        private int zaikoHyoukaHouhou { get; set; }

        internal bool isRegistErr { get; set; }

        #endregion


        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public F18_G170UIForm()
            : base(WINDOW_ID.T_ZAIKO_SIMESYORI, WINDOW_TYPE.NONE)
        {
            LogUtility.DebugMethodStart();

            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new F18Logic(this);

            LogUtility.DebugMethodEnd();
        }
        #endregion


        #region 画面読み込み処理
        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            //// 初回登録ユーザと日付を初期化
            //F18_G170UIHeaderForm headerForm = (F18_G170UIHeaderForm)(((BusinessBaseForm)this.Parent).headerForm);
            //headerForm.createUser.Text = SystemProperty.UserName;
            //headerForm.createDate.Text = DateTime.Today.ToString("yyyy/MM/dd HH:mm:ss");

            // 検索条件初期化
            this.condition_simeTaisyouKikanFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            this.condition_simeTaisyouKikanTo = condition_simeTaisyouKikanFrom.AddMonths(1).AddSeconds(-1);

            // 編集条件：評価方法 初期化
            this.zaikoHyoukaHouhou = this.logic.getZaikoHyoukaHouhou();
            if (this.zaikoHyoukaHouhou == -1) { return; }

            // ボタン初期化処理
            this.ButtonInit();
            // ボタンイベントの初期化処理
            this.ButtonEventInit();

            // 画面項目初期値設定
            this.zaikoHyoukaHouhouShowValue.Text = F18_G170ConstCls.ZAIKO_HYOUKA_HOUHOU[this.zaikoHyoukaHouhou];
            this.refleshViewDate();

            // 一覧を初期検索
            this.refleshSimeInfo();

            LogUtility.DebugMethodEnd();
        }
        #endregion


        #region ボタン関連初期化処理

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        /// <returns></returns>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            // ボタン設定の読込
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            ButtonSetting[] buttonSettingArray = buttonSetting.LoadButtonSetting(thisAssembly, F18_G170ConstCls.BUTTON_INFO_XML_PATH);
            var parentForm = (BusinessBaseForm)this.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSettingArray, parentForm, this.WindowType);

            // 必要ない項目を無効化
            parentForm.bt_process1.Enabled = false;
            parentForm.bt_process1.Visible = false;
            parentForm.bt_process2.Enabled = false;
            parentForm.bt_process2.Visible = false;
            parentForm.bt_process3.Enabled = false;
            parentForm.bt_process3.Visible = false;
            parentForm.bt_process4.Enabled = false;
            parentForm.bt_process4.Visible = false;
            parentForm.bt_process5.Enabled = false;
            parentForm.bt_process5.Visible = false;
            parentForm.txb_process.Enabled = false;
            parentForm.txb_process.Visible = false;
            parentForm.lb_process.Visible = false;
            parentForm.ProcessButtonPanel.Enabled = false;
            parentForm.ProcessButtonPanel.Visible = false;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタンイベントの初期化処理
        /// </summary>
        /// <returns></returns>
        private void ButtonEventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.Parent;

            //(F1 先月)イベント生成
            parentForm.bt_func1.Click -= new EventHandler(this.PrewMonth);
            parentForm.bt_func1.Click += new EventHandler(this.PrewMonth);

            //(F2 翌月)イベント生成
            parentForm.bt_func2.Click -= new EventHandler(this.NextMonth);
            parentForm.bt_func2.Click += new EventHandler(this.NextMonth);

            //(F4 削除)イベント生成
            parentForm.bt_func4.Click -= new EventHandler(this.Delete);
            parentForm.bt_func4.Click += new EventHandler(this.Delete);

            //(F5 印刷)イベント生成
            parentForm.bt_func5.Click -= new EventHandler(this.Print);
            parentForm.bt_func5.Click += new EventHandler(this.Print);

            //(F6 CSV出力)イベント生成
            parentForm.bt_func6.Click -= new EventHandler(this.CsvOutput);
            parentForm.bt_func6.Click += new EventHandler(this.CsvOutput);

            //(F8 検索)イベント生成
            parentForm.bt_func8.Click -= new EventHandler(this.Search);
            parentForm.bt_func8.Click += new EventHandler(this.Search);

            //(F9 実行)イベント生成
            parentForm.bt_func9.Click -= new EventHandler(this.Regist);
            parentForm.bt_func9.Click += new EventHandler(this.Regist);

            //(F12 閉じる)イベント生成
            parentForm.bt_func12.Click -= new EventHandler(this.FormClose);
            parentForm.bt_func12.Click += new EventHandler(this.FormClose);

            LogUtility.DebugMethodEnd();
        }
        #endregion


        #region  ボタンイベント処理
        /// <summary>
        /// F1 前月
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void PrewMonth(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {

                //base.OnLoad(e);

                // 検索

                // 画面日付表示をリフレッシュ
                this.condition_simeTaisyouKikanTo = this.condition_simeTaisyouKikanFrom.AddSeconds(-1);// 先月の末 = 当月初日 - 1 Seconds
                this.condition_simeTaisyouKikanFrom = this.condition_simeTaisyouKikanFrom.AddMonths(-1);// 先月の初日 = 当月01日 - 1ヶ月
                this.refleshViewDate();

                // 一覧締情報再検索
                this.refleshSimeInfo();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// F2 翌月
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NextMonth(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {

                //base.OnLoad(e);

                // 画面日付表示をリフレッシュ
                this.condition_simeTaisyouKikanFrom = this.condition_simeTaisyouKikanFrom.AddMonths(1);// 来月初日 = 当月初日 + 1ヶ月
                this.condition_simeTaisyouKikanTo = this.condition_simeTaisyouKikanFrom.AddMonths(1).AddSeconds(-1);// 再来月初日 - 1 Seconds => (来月初日 +  1ヶ月)  - 1 Seconds
                this.refleshViewDate();

                // 一覧締情報再検索
                this.refleshSimeInfo();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// F4 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Delete(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                //base.OnLoad(e);

                if (this.msgLogic.MessageBoxShow(F18_G170ConstCls.C_MSGID_DELETE) == DialogResult.Yes)
                {
                    // 締情報削除
                    this.logic.deleteSimeInfo(this.GYOUSHA_CD.Text,
                                                this.GENBA_CD.Text,
                                                this.condition_simeTaisyouKikanFrom,
                                                this.condition_simeTaisyouKikanTo);
                    if (this.isRegistErr) { return; }

                    // 一覧締情報再検索
                    if (!this.refleshSimeInfo()) { return; }

                    // 完了メッセージ
                    this.msgLogic.MessageBoxShow(F18_G170ConstCls.I_MSG_ID_PROCESS_FINISHED, F18_G170ConstCls.I_MSG_PARAM_DELETE_FINISHED);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F5 印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Print(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {

                //base.OnLoad(e);

                F18_G170_SLIP_CONDITION_Dto genbaInfoCondition = new F18_G170_SLIP_CONDITION_Dto();
                genbaInfoCondition.gyoushaCD = this.condition_backup_gyosyaCD;
                genbaInfoCondition.genbaCD = this.condition_backup_genbaCD;
                genbaInfoCondition.simeTaisyouKikanFrom = this.condition_simeTaisyouKikanFrom;
                genbaInfoCondition.simeTaisyouKikanTo = this.condition_simeTaisyouKikanTo;
                genbaInfoCondition.hyoukaHouhou = F18_G170ConstCls.ZAIKO_HYOUKA_HOUHOU[this.zaikoHyoukaHouhou];

                this.logic.printSlip(genbaInfoCondition, this.result_simeiInfo);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F6 CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CsvOutput(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                //base.OnLoad(e);

                var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
                var title = F18_G170ConstCls.CSV_DIALOG_TITLE;
                var initialPath = @"C:\Temp";
                var windowHandle = this.Handle;
                var isFileSelect = false;
                var isTerminalMode = SystemProperty.IsTerminalMode;
                var fileName = F18_G170ConstCls.CSV_DIALOG_INIT_FILE_NAME + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

                browserForFolder = null;

                if (false == String.IsNullOrEmpty(filePath))
                {
                    if (!this.logic.outputCsv(this.result_simeiInfo, filePath + "\\" + fileName)) { return; }

                    // 出力完了後メッセージ表示
                    (new MessageBoxShowLogic()).MessageBoxShow(F18_G170ConstCls.CSV_FINISH_MSG_ID, F18_G170ConstCls.CSV_FINISH_MSG_PARAM);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                //base.OnLoad(e);

                // 検索
                this.refleshSimeInfo();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F9 実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (this.msgLogic.MessageBoxShow(F18_G170ConstCls.C_MSGID_REGIST, F18_G170ConstCls.C_MSGPARAM_REGIST) == DialogResult.Yes)
                {
                    // 実行
                    string errMsgID =
                        this.logic.insertSimeInfo(this.GYOUSHA_CD.Text,
                                                    this.GENBA_CD.Text,
                                                    this.condition_simeTaisyouKikanFrom,
                                                    this.condition_simeTaisyouKikanTo,
                                                    this.zaikoHyoukaHouhou);
                    if (this.isRegistErr) { return; }

                    // 実行結果より、メッセージを表示
                    switch (errMsgID)
                    {
                        case F18_G170ConstCls.E_MSGID_INSERT_DATA_EXIST:
                            // 該当月締め済みエラー時
                            this.msgLogic.MessageBoxShow(F18_G170ConstCls.E_MSGID_INSERT_DATA_EXIST, F18_G170ConstCls.E_MSGPARAM_INSERT_DATA_EXIST);
                            return;
                        case F18_G170ConstCls.E_MSGID_INSERT_DATA_NOTEXIST:
                            // 締情報が存在しないエラー時
                            this.msgLogic.MessageBoxShow(F18_G170ConstCls.E_MSGID_INSERT_DATA_NOTEXIST);
                            return;
                        case F18_G170ConstCls.E_MSG_INSERT:
                            // insert時例外発生
                            MessageBox.Show(F18_G170ConstCls.E_MSG_INSERT, F18_G170ConstCls.E_MSG_INSERT_DIALOG_TITLE);
                            return;
                        default:
                            break;
                    }

                    // 一覧締情報再検索
                    if (!this.refleshSimeInfo()) { return; }

                    // 完了メッセージ
                    this.msgLogic.MessageBoxShow(F18_G170ConstCls.I_MSG_ID_PROCESS_FINISHED, F18_G170ConstCls.I_MSG_PARAM_INSERT_FINISHED);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                var parentForm = (BusinessBaseForm)this.Parent;
                this.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region #現場#項目の処理
        /// <summary>
        /// マウスでfocusを移動時、#現場#項目の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void GENNBA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                var genbaCd = this.GENBA_CD.Text.Trim();

                if (genbaCd != "")
                {
                    if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
                    {
                        this.msgLogic.MessageBoxShow("E051", "荷降業者");
                        e.Cancel = true;
                        return;
                    }

                    string gyoushaCD = this.GYOUSHA_CD.Text.Trim();
                    bool catchErr = false;
                    GenbaInfo[] genbaInfo = this.logic.getGenbaInfo(gyoushaCD, genbaCd, out catchErr);
                    if (catchErr) { return; }
                    if (genbaInfo != null && genbaInfo.Length > 0)
                    {
                        if (genbaInfo.Length == 1)
                        {
                            // 1件の場合、結果データを画面に設定
                            this.GYOUSHA_CD.Text = genbaInfo[0].RET_GYOUSHA_CD;
                            this.GYOUSHA_NAME_RYAKU.Text = genbaInfo[0].RET_GYOUSHA_NAME_RYAKU;
                            this.GENBA_CD.Text = genbaInfo[0].RET_GENBA_CD;
                            this.GENBA_NAME_RYAKU.Text = genbaInfo[0].RET_GENBA_NAME_RYAKU;
                            this.condition_backup_gyosyaCD = this.GYOUSHA_CD.Text;
                            this.condition_backup_gyosyaNM = this.GYOUSHA_NAME_RYAKU.Text;
                            this.condition_backup_genbaCD = this.GENBA_CD.Text;
                            this.condition_backup_genbaNM = this.GENBA_NAME_RYAKU.Text;
                        }
                        else
                        {
                            // 複数の場合、検索popup画面表示?
                            SendKeys.Send(" ");

                            // イベント中止
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        // データなしの場合

                        // イベント中止
                        e.Cancel = true;
                        // 枠色
                        this.GENBA_CD.IsInputErrorOccured = true;
                        // メッセージ表示
                        this.msgLogic.MessageBoxShow(F18_G170ConstCls.E_MSGID_SEARCH_GENBA_INFO_NOT_EXIST, F18_G170ConstCls.E_MSGPARAM_SEARCH_GENBA_INFO_NOT_EXIST);
                        // 現場名クリア
                        this.GENBA_NAME_RYAKU.Text = "";
                        this.condition_backup_genbaNM = this.GENBA_NAME_RYAKU.Text;
                    }
                }
                else
                {
                    this.GENBA_CD.Text = "";
                    this.GENBA_NAME_RYAKU.Text = "";
                    this.condition_backup_genbaCD = this.GENBA_CD.Text;
                    this.condition_backup_genbaNM = this.GENBA_NAME_RYAKU.Text;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion


        #region 各内部処理

        /// <summary>
        /// 画面上の表示日付をリフレッシュ
        /// </summary>
        private void refleshViewDate()
        {
            LogUtility.DebugMethodStart();

            this.dtp_simeTaishoKikanFrom.Value = this.condition_simeTaisyouKikanFrom;
            this.dtp_simeTaishoKikanTo.Value = this.condition_simeTaisyouKikanTo;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面条件より締情報を取得し、画面表示を更新    (※データテスト可能箇所)
        /// </summary>
        private bool refleshSimeInfo()
        {
            bool ret = true;
            LogUtility.DebugMethodStart();

            // 検索条件backup更新
            this.condition_backup_gyosyaCD = this.GYOUSHA_CD.Text;
            this.condition_backup_gyosyaNM = this.GYOUSHA_NAME_RYAKU.Text;
            this.condition_backup_genbaCD = this.GENBA_CD.Text;
            this.condition_backup_genbaNM = this.GENBA_NAME_RYAKU.Text;

            // 一覧情報を更新
            bool catchErr = false;
            this.result_simeiInfo =
                this.logic.searchSimeInfo(this.GYOUSHA_CD.Text,
                                            this.GENBA_CD.Text,
                                            this.condition_simeTaisyouKikanFrom,
                                            this.condition_simeTaisyouKikanTo,
                                            out catchErr);
            if (catchErr)
            {
                ret = false;
                return ret;
            }
            this.Ichiran.DataSource = this.result_simeiInfo;

            // 登録・更新者情報を非表示
            this.Ichiran.Columns["CREATE_USER"].Visible = false;
            this.Ichiran.Columns["CREATE_DATE"].Visible = false;
            this.Ichiran.Columns["UPDATE_USER"].Visible = false;
            this.Ichiran.Columns["UPDATE_DATE"].Visible = false;

            // ヘッダー部の初回登録、最終更新を初期化
            var parentForm = (BusinessBaseForm)this.Parent;
            DetailedHeaderForm header = (DetailedHeaderForm)parentForm.headerForm;

            // 一覧データ有無より、ボタン状態を更新
            if (this.Ichiran.RowCount > 0)
            {
                // 表示データあり時

                // 最終更新日時ソート
                DataView dv = new DataView(this.result_simeiInfo);
                dv.Sort = "UPDATE_DATE Desc";
                DataTable dtSort = dv.ToTable();

                // ヘッダー部の初回登録、最終更新を設定
                header.CreateUser.Text = dtSort.Rows[0]["CREATE_USER"].ToString();
                header.CreateDate.Text = dtSort.Rows[0]["CREATE_DATE"].ToString();
                header.LastUpdateUser.Text = dtSort.Rows[0]["UPDATE_USER"].ToString();
                header.LastUpdateDate.Text = dtSort.Rows[0]["UPDATE_DATE"].ToString();

                // ①[実行F9]は利用不可
                parentForm.bt_func9.Enabled = false;
                // ②[削除F4][印刷F5][CSV出力F6]は利用不可
                parentForm.bt_func4.Enabled = true;
                parentForm.bt_func5.Enabled = true;
                parentForm.bt_func6.Enabled = true;
            }
            else
            {
                // 表示データなし

                // ヘッダー部の初回登録、最終更新をクリア
                header.CreateUser.Text = string.Empty;
                header.CreateDate.Text = string.Empty;
                header.LastUpdateUser.Text = string.Empty;
                header.LastUpdateDate.Text = string.Empty;

                // ①[実行F9]は利用可能
                parentForm.bt_func9.Enabled = true;
                // ②[削除F4][印刷F5][CSV出力F6]は利用不可
                parentForm.bt_func4.Enabled = false;
                parentForm.bt_func5.Enabled = false;
                parentForm.bt_func6.Enabled = false;
            }

            // ※未来の月への画面表示はできない
            // ※当月しか締めはできない
            if (DateTime.Today.Year == this.condition_simeTaisyouKikanFrom.Year
                && DateTime.Today.Month == this.condition_simeTaisyouKikanFrom.Month)
            {
                // 当月の場合
                // [F2 翌月]を無効化
                parentForm.bt_func2.Enabled = false;
            }
            else
            {
                // その他場合

                // [F2 翌月]を有効化
                parentForm.bt_func2.Enabled = true;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion

        #region 現場 KeyPress イベント
        /// <summary>
        /// [enter]または[tab]でfocusを移動時、#現場#項目の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_KeyPress(object sender, KeyPressEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {

                if (e.KeyChar.Equals('\r') || e.KeyChar.Equals('\t'))
                {
                    var genbaCd = this.GENBA_CD.Text.Trim();

                    if (genbaCd != "")
                    {
                        if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
                        {
                            this.msgLogic.MessageBoxShow("E051", "荷降業者");
                            e.Handled = true;
                            this.GENBA_CD.Focus();
                            return;
                        }

                        genbaCd = this.GENBA_CD.Text = this.GENBA_CD.Text.Trim().PadLeft(6, '0');
                        string gyoushaCD = this.GYOUSHA_CD.Text.Trim();
                        bool catchErr = false;
                        GenbaInfo[] genbaInfo = this.logic.getGenbaInfo(gyoushaCD, genbaCd, out catchErr);
                        if (catchErr) { return; }
                        if (genbaInfo != null && genbaInfo.Length > 0)
                        {
                            if (genbaInfo.Length == 1)
                            {
                                // 1件の場合、結果データを画面に設定
                                this.GYOUSHA_CD.Text = genbaInfo[0].RET_GYOUSHA_CD;
                                this.GYOUSHA_NAME_RYAKU.Text = genbaInfo[0].RET_GYOUSHA_NAME_RYAKU;
                                this.GENBA_CD.Text = genbaInfo[0].RET_GENBA_CD;
                                this.GENBA_NAME_RYAKU.Text = genbaInfo[0].RET_GENBA_NAME_RYAKU;
                                this.condition_backup_gyosyaCD = this.GYOUSHA_CD.Text;
                                this.condition_backup_gyosyaNM = this.GYOUSHA_NAME_RYAKU.Text;
                                this.condition_backup_genbaCD = this.GENBA_CD.Text;
                                this.condition_backup_genbaNM = this.GENBA_NAME_RYAKU.Text;
                            }
                            else
                            {
                                // 複数の場合、検索popup画面表示?
                                SendKeys.Send(" ");

                                // イベント中止
                                e.Handled = true;
                                this.GENBA_CD.Focus();
                            }
                        }
                        else
                        {
                            // データなしの場合

                            // イベント中止
                            e.Handled = true;
                            this.GENBA_CD.Focus();

                            // 枠色
                            this.GENBA_CD.IsInputErrorOccured = true;
                            // メッセージ表示
                            this.msgLogic.MessageBoxShow(F18_G170ConstCls.E_MSGID_SEARCH_GENBA_INFO_NOT_EXIST, F18_G170ConstCls.E_MSGPARAM_SEARCH_GENBA_INFO_NOT_EXIST);
                            // 現場名クリア
                            this.GENBA_NAME_RYAKU.Text = "";
                            this.condition_backup_genbaNM = this.GENBA_NAME_RYAKU.Text;
                        }
                    }
                    else
                    {
                        this.GENBA_CD.Text = "";
                        this.GENBA_NAME_RYAKU.Text = "";
                        this.condition_backup_genbaCD = this.GENBA_CD.Text;
                        this.condition_backup_genbaNM = this.GENBA_NAME_RYAKU.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 業者ポップアップ後処理
        /// <summary>
        /// 業者ポップアップ後処理
        /// </summary>
        public void AfterGyoushaPopup()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // [現場CD]、[現場]を空にする
                this.GENBA_CD.Text = String.Empty;
                
                this.GENBA_NAME_RYAKU.Text = String.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error("AfterGyoushaPopup", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion
    }
}
