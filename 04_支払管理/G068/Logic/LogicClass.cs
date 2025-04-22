using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Data.SqlTypes;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.App;
using DataGridViewCheckBoxColumnHeader;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.SalesManagement.Shiharaikakuteinyuryoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {

        #region Const
        
        /// <summary>
        /// 伝票区分（支払）
        /// </summary>
        private const String DENPYOU_KBN_SHIHARAI = "2";
        /// <summary>
        /// DB確定区分（確定）
        /// </summary>
        private const String KAKUTEI_KBN_KAKUTEI = "1";
        /// <summary>
        /// DB確定区分（未確定）
        /// </summary>
        private const String KAKUTEI_KBN_MIKAKUTEI = "2";
        /// <summary>
        /// 情報確定利用区分（確定登録を利用する）
        /// </summary>
        private const String KAKUTEI_USE_KBN_USE = "1";
        /// <summary>
        /// 日付区分（伝票日付）
        /// </summary>
        private const String HIDUKE_KBN_DENPYOU = "1";

        #endregion

        #region フィールド
        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// HeaderForm
        /// </summary>
        private UIHeader headForm;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// 拠点マスタ
        /// </summary>
        private IM_KYOTENDao m_kyotendao;

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private DAOClass t_ichirandao;

        /// <summary>
        /// 受入明細Dao
        /// </summary>
        private UKEIREK_DETAIL_DaoCls ukeirek_detail_daocls;
        /// <summary>
        /// 出荷明細Dao
        /// </summary>
        private SHUKKAK_DETAIL_DaoCls shukkak_detail_daocls;
        /// <summary>
        /// 売上／支払明細Dao
        /// </summary>
        private UR_SHK_DETAIL_DaoCls ur_shk_detail_daocls;
        /// <summary>
        /// 受入入力Dao
        /// </summary>
        private UKEIREK_ENTRY_DaoCls ukeirek_entry_daocls;
        /// <summary>
        /// 出荷入力Dao
        /// </summary>
        private SHUKKAK_ENTRY_DaoCls shukkak_entry_daocls;
        /// <summary>
        /// 売上／支払入力Dao
        /// </summary>
        private UR_SHK_ENTRY_DaoCls ur_shk_entry_daocls;

        /// <summary>
        /// メッセージクラス
        /// </summary>
        private MessageBoxShowLogic msgcls;
        /// <summary>
        /// DBAccessor
        /// </summary>
        private Shougun.Core.Common.BusinessCommon.DBAccessor dbaccss;

        /// <summary>
        /// 受入明細list
        /// </summary>
        private List<T_UKEIRE_DETAIL> mukeiremslist;

        /// <summary>
        /// 受入入力list（追加用）
        /// </summary>
        private List<T_UKEIRE_ENTRY> mukeireentrylistadd;

        /// <summary>
        /// 受入入力list（更新用）
        /// </summary>
        private List<T_UKEIRE_ENTRY> mukeireentrylistupdata;

        /// <summary>
        /// 出荷明細list
        /// </summary>
        private List<T_SHUKKA_DETAIL> mshukamslist;

        /// <summary>
        /// 出荷入力list（追加用）
        /// </summary>
        private List<T_SHUKKA_ENTRY> mshukaentrylistadd;

        /// <summary>
        /// 出荷入力list（更新用）
        /// </summary>
        private List<T_SHUKKA_ENTRY> mshukaentrylistupdata;

        /// <summary>
        /// 売上／支払明細list
        /// </summary>
        private List<T_UR_SH_DETAIL> murshmslist;

        /// <summary>
        /// 売上／支払入力list（追加用）
        /// </summary>
        private List<T_UR_SH_ENTRY> murshentrylistadd;

        /// <summary>
        /// 売上／支払入力list（更新用）
        /// </summary>
        private List<T_UR_SH_ENTRY> murshentrylistupdata;

        /// <summary>
        /// システム情報
        /// </summary>
        private M_SYS_INFO msysinfo;

        #endregion フィールド

        #region プロパティ

        /// <summary>
        /// SELECT句
        /// </summary>
        public string selectQuery { get; set; }

        /// <summary>
        /// ORDERBY句
        /// </summary>
        public string orderByQuery { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable searchResult { get; set; }

        /// <summary>
        /// 作成したSQL
        /// </summary>
        public string mcreateSql { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public string searchString { get; set; }

        private bool isRegist;

        #endregion プロパティ

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">フォームオブジェクト</param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new DTOClass();
            this.t_ichirandao = DaoInitUtility.GetComponent<DAOClass>();
            this.m_kyotendao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.ukeirek_detail_daocls = DaoInitUtility.GetComponent<UKEIREK_DETAIL_DaoCls>();
            this.ukeirek_entry_daocls = DaoInitUtility.GetComponent<UKEIREK_ENTRY_DaoCls>();
            this.shukkak_detail_daocls = DaoInitUtility.GetComponent<SHUKKAK_DETAIL_DaoCls>();
            this.shukkak_entry_daocls = DaoInitUtility.GetComponent<SHUKKAK_ENTRY_DaoCls>();
            this.ur_shk_detail_daocls = DaoInitUtility.GetComponent<UR_SHK_DETAIL_DaoCls>();
            this.ur_shk_entry_daocls = DaoInitUtility.GetComponent<UR_SHK_ENTRY_DaoCls>();
            this.dbaccss = new Shougun.Core.Common.BusinessCommon.DBAccessor();
            // メッセージ出力用
            this.msgcls = new MessageBoxShowLogic();
            this.isRegist = true;

            LogUtility.DebugMethodEnd(targetForm);
        }

        #endregion コンストラクタ
        
        #region 初期処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得
                this.msysinfo = dbaccss.GetSysInfo();
                // ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();

                // 親フォームオブジェクト取得
                parentForm = (BusinessBaseForm)this.form.Parent;

                // 各初期値設定
                this.SetHeaderInit();
                this.SetFormInit();
                // グリッドスタイル設定
                this.SetStyleDtGridView();
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInit", ex2);
                this.msgcls.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgcls.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            parentForm.bt_process1.TextAlign = ContentAlignment.MiddleCenter;
            parentForm.bt_process2.TextAlign = ContentAlignment.MiddleCenter;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            string ButtonInfoXmlPath = this.GetType().Namespace;
            ButtonInfoXmlPath = ButtonInfoXmlPath + ".Setting.ButtonSetting.xml";
            LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath));
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //確定解除ボタン(F1)イベント生成
            parentForm.bt_func1.Click += new EventHandler(bt_func1_Click);

            //条件クリアボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(bt_func7_Click);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(bt_func8_Click);

            //確定登録ボタン(F9)イベント生成
            parentForm.bt_func9.Click += new EventHandler(bt_func9_Click);

            //並び替えボタン(F10)イベント生成
            parentForm.bt_func10.Click += new EventHandler(bt_func10_Click);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);

            //プロセスボタンイベント生成
            parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);

            //グリッドセル選択イベント生成
            this.form.customDataGridView1.CellContentClick += new DataGridViewCellEventHandler(customDataGridView1_CellContentClick);

            //ヘッダ
            headForm.txtHidukeSentaku.TextChanged += new EventHandler(txtHidukeSentaku_TextChanged);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// HeaderForm設定
        /// </summary>
        /// <param name="hs"></param>
        public void SetHeader(UIHeader hs)
        {
            this.headForm = hs;
        }

        #endregion 初期処理

        #region Functionボタン 押下処理

        /// <summary>
        /// F1 確定解除
        /// </summary>
        public void bt_func1_Click(object sender, EventArgs e)
        {
            DataTable dbkoshin = (DataTable)this.form.customDataGridView1.DataSource;
            if (dbkoshin != null)
            {
                if (dbkoshin.Rows.Count > 0)
                {
                    PasswordPopupForm pwform = new PasswordPopupForm(msysinfo.SHIHARAI_KAKUTEI_KAIJO_PASSWORD, "2");
                    pwform.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                    pwform.ShowDialog();
                    if (pwform.rightpassword)
                    {
                        this.KakuteiKaijyo();
                        if (this.isRegist)
                        {
                            int retCount = this.Search();
                            if (retCount == -1)
                            {
                                return;
                            }
                            else if (retCount == 0)
                            {
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShow("C001");
                                return;
                            }
                            //確定解除完了メッセージを表示
                            msgcls.MessageBoxShow("I001", "確定解除");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        public void bt_func7_Click(object sender, EventArgs e)
        {
            this.form.searchString.Clear();

            //前の結果をクリア
            int k = this.form.customDataGridView1.Rows.Count;
            for (int i = k; i >= 1; i--)
            {
                this.form.customDataGridView1.Rows.RemoveAt(this.form.customDataGridView1.Rows[i - 1].Index);
            }

            this.form.chkUkeire.Checked = false;
            this.form.chkShukka.Checked = false;
            this.form.chkUriageshiharai.Checked = false;
            this.headForm.txtKyotenCd.Clear();
            this.headForm.txtKyotenNameRyaku.Clear();
            this.headForm.dtpDateTo.Value = this.parentForm.sysDate;
            this.headForm.dtpDateFrom.Value = this.parentForm.sysDate;
            this.headForm.txtReadDataCnt.Text = "0";
            this.headForm.txtAlertNumber.Clear();
            this.headForm.rdoDenpyouHiduke.Checked = true;
            this.form.rdoSubete.Checked = true;
            //if (this.form.customDataGridView1.Columns.Contains("確定区分"))
            //{
            //    this.form.customDataGridView1.Columns.Remove("確定区分");
            //}
            //ソートヘッダクリア
            this.form.customSortHeader1.ClearCustomSortSetting();
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        public void bt_func8_Click(object sender, EventArgs e)
        {
            if (this.form.chkUkeire.Checked || this.form.chkShukka.Checked
                || this.form.chkUriageshiharai.Checked)
            {
                if (!string.IsNullOrEmpty(this.form.searchString.Text))
                {
                    string getSearchString = this.form.searchString.Text.Replace("\r", "").Replace("\n", "");
                    this.searchString = getSearchString;
                }

                //検索処理実行
                Cursor preCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                int retCount = this.Search();
                Cursor.Current = preCursor;

                if (string.IsNullOrEmpty(this.form.searchString.Text))               
                {
                    this.form.searchString.Clear();
                    this.form.searchString.Focus();
                }

                if (retCount == 0)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                }
            }

        }

        /// <summary>
        /// F9 確定登録
        /// </summary>
        public void bt_func9_Click(object sender, EventArgs e)
        {
            DataTable dbkoshin = (DataTable)this.form.customDataGridView1.DataSource;
            if (dbkoshin != null)
            {
                if (dbkoshin.Rows.Count > 0)
                {
                    //確定登録の確認メッセージを表示
                    msgcls = new MessageBoxShowLogic();
                    if (msgcls.MessageBoxShow("C046", "確定登録") == DialogResult.Yes)
                    {
                        bool tourokuFlg = false;
                        Cursor preCursor = Cursor.Current;
                        Cursor.Current = Cursors.WaitCursor;
                        tourokuFlg = this.KakuteiTouroku();
                        if (this.isRegist)
                        {                             
                            int count = this.Search();
                            if (count == -1)
                            {
                                return;
                            }
                            Cursor.Current = preCursor;

                            if (tourokuFlg)
                            {
                                //確定登録完了メッセージを表示
                                msgcls.MessageBoxShow("I001", "確定登録");
                            }
                            else
                            {
                                //未登録メッセージを表示
                                msgcls.MessageBoxShow("E046", "対象データが未変更", "確定");
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// F10 並び替え
        /// </summary>
        public void bt_func10_Click(object sender, EventArgs e)
        {
            this.form.customSortHeader1.ShowCustomSortSettingDialog();
            for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
            {
                this.form.customDataGridView1.Rows[i].Cells["確定区分"].Value = this.form.customDataGridView1.Rows[i].Cells["明細・確定区分"].Value;
            }
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        public void bt_func12_Click(object sender, EventArgs e)
        {
            // 以下の項目をセッティングファイルに保存する
            Properties.Settings.Default.SET_KYOTEN_CD = this.headForm.txtKyotenCd.Text;     //拠点CD
            
            DateTime resultDt;
            //if (this.headForm.dtpDateFrom.Value == null)
            if (!string.IsNullOrEmpty(this.headForm.dtpDateFrom.Text) && DateTime.TryParse(this.headForm.dtpDateFrom.Text, out resultDt))
            {
                Properties.Settings.Default.SET_HIDUKE_FROM = DateTime.Parse(this.headForm.dtpDateFrom.Value.ToString()).ToShortDateString();          //伝票日付From
            }
            else
            {
                Properties.Settings.Default.SET_HIDUKE_FROM = string.Empty;
                // CustomDateTimePicker Valueのget return DateTime.ParseExactでエラーになる為、Emptyにしておく
                this.headForm.dtpDateFrom.Text = string.Empty;
            }
            
            //if (this.headForm.dtpDateTo.Value == null)
            if (!string.IsNullOrEmpty(this.headForm.dtpDateTo.Text) && DateTime.TryParse(this.headForm.dtpDateTo.Text, out resultDt))
            {
                Properties.Settings.Default.SET_HIDUKE_TO = DateTime.Parse(this.headForm.dtpDateTo.Value.ToString()).ToShortDateString();              //伝票日付To
            }
            else
            {
                Properties.Settings.Default.SET_HIDUKE_TO = string.Empty;
                // CustomDateTimePicker Valueのget return DateTime.ParseExactでエラーになる為、Emptyにしておく
                this.headForm.dtpDateTo.Text = string.Empty;
            }

            Properties.Settings.Default.SET_HIDUKE_KBN = this.headForm.txtHidukeSentaku.Text;   //日付区分
            Properties.Settings.Default.Save();

            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.Close();
        }

        #endregion Functionボタン 押下処理

        #region プロセスボタン押下処理

        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            //戻り値
            String rtnSysID = String.Empty;

            int denshukbn = (int)DENSHU_KBN.SHIHARAI_KAKUTEI_NYUURYOKU;
            var assembly = Assembly.LoadFrom("PatternIchiran.dll");
            // 社員コード、伝種区分を共通画面に渡す
            var callForm1 = (SuperForm)assembly.CreateInstance(
                    "Shougun.Core.Common.PatternIchiran.UIForm",
                    false,
                    BindingFlags.CreateInstance,
                    null,
                    //new object[] { this.form.ShainCd, DENSHU_KBN.SHIHARAI_KAKUTEI_NYUURYOKU.ToString() },
                    new object[] { this.form.ShainCd, denshukbn.ToString() },
                    null,
                    null
                  );
            if (callForm1.IsDisposed)
            {
                return;
            }
            var businessForm = new BusinessBaseForm(callForm1, WINDOW_TYPE.NONE);
            var ret = businessForm.ShowDialog();

            //戻り値
            Type baseObj = assembly.GetType("Shougun.Core.Common.PatternIchiran.UIForm");
            PropertyInfo val = baseObj.GetProperty("ParamOut_SysID");
            rtnSysID = (String)val.GetValue(callForm1, null);

        }
        #endregion プロセスボタン押下処理

        #region ヘッダ部イベント

        /// <summary>
        /// ヘッダ日付選択テキスト入力時
        /// </summary>
        private void txtHidukeSentaku_TextChanged(object sender, System.EventArgs e)
        {
            if (this.headForm.txtHidukeSentaku.Text.Equals("1"))
            {
                this.headForm.lblHidukeNyuuryoku.Text = "伝票日付";
            }
            else if (this.headForm.txtHidukeSentaku.Text.Equals("2"))
            {
                this.headForm.lblHidukeNyuuryoku.Text = "入力日付";
            }
        }
        #endregion ヘッダ部イベント

        #region その他イベント

        /// <summary>
        /// グリッドセルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                DataGridViewRow dgvRow = this.form.customDataGridView1.Rows[e.RowIndex];

                // 利用区分が「利用しない」の場合はチェックボックス操作不可
                if (!dgvRow.Cells["情報確定利用区分"].Value.ToString().Equals(KAKUTEI_USE_KBN_USE))
                {
                    return;
                }

                if (this.msysinfo.SYS_KAKUTEI__TANNI_KBN.Equals(SqlInt16.Parse("1")))
                {
                    // [システム設定.システム確定登録単位区分]=1:伝票単位であれば、伝票の確定フラグと
                    // 伝票に紐つく全ての明細の確定フラグも設定する

                    string systemid = dgvRow.Cells["システムID"].Value.ToString();
                    string seq = dgvRow.Cells["枝番"].Value.ToString();
                    if (dgvRow.Cells["確定区分"].Value.Equals(true))
                    {
                        foreach (DataGridViewRow dgvrow in this.form.customDataGridView1.Rows)
                        {
                            if (systemid.Equals(dgvrow.Cells["システムID"].Value.ToString()) &&
                                seq.Equals(dgvrow.Cells["枝番"].Value.ToString()))
                            {
                                dgvrow.Cells["確定区分"].Value = false;
                                dgvrow.Cells["明細・確定区分"].Value = false;
                            }
                        }
                    }
                    else if (dgvRow.Cells["確定区分"].Value.Equals(false))
                    {
                        foreach (DataGridViewRow dgvrow in this.form.customDataGridView1.Rows)
                        {
                            if (systemid.Equals(dgvrow.Cells["システムID"].Value.ToString()) &&
                                seq.Equals(dgvrow.Cells["枝番"].Value.ToString()))
                            {
                                dgvrow.Cells["確定区分"].Value = true;
                                dgvrow.Cells["明細・確定区分"].Value = true;
                            }
                        }
                    }
                }
                else
                {
                    // [システム設定.システム確定登録単位区分]=2:明細単位であれば、該当明細の確定フラグを設定する
                    if (dgvRow.Cells["確定区分"].Value.Equals(true))
                    {
                        dgvRow.Cells["確定区分"].Value = false;
                        dgvRow.Cells["明細・確定区分"].Value = false;
                    }
                    else if (dgvRow.Cells["確定区分"].Value.Equals(false))
                    {
                        dgvRow.Cells["確定区分"].Value = true;
                        dgvRow.Cells["明細・確定区分"].Value = true;
                    }
                }
            }
        }

        /// <summary>
        /// グリッドヘッダチェックボックスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ch_OnCheckBoxClicked(object sender, datagridviewCheckboxHeaderEventArgs e)
        {
            //this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1.Rows[0].Cells["分類"];
            if (this.form.customDataGridView1.RowCount != 0)
            {
                this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1.Rows[0].Cells["確定区分"];

                foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                {
                    if (e.CheckedState)
                    {
                        if (dgvRow.Cells["情報確定利用区分"].Value.ToString().Equals(KAKUTEI_USE_KBN_USE))
                        {
                            dgvRow.Cells["確定区分"].Value = true;
                            dgvRow.Cells["明細・確定区分"].Value = true;
                        }

                    }
                    else
                    {
                        if (dgvRow.Cells["情報確定利用区分"].Value.ToString().Equals(KAKUTEI_USE_KBN_USE))
                        {
                            dgvRow.Cells["確定区分"].Value = false;
                            dgvRow.Cells["明細・確定区分"].Value = false;
                        }

                    }
                }
            }
        }

        #endregion その他イベント

        #region DB関連処理

        #region 未使用
        /// <summary>
        /// 論理削除処理用（未使用）
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 物理削除処理用（未使用）
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登録処理用（未使用）
        /// </summary>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新処理用（未使用）
        /// </summary>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion 未使用

        #region 検索処理

        /// <summary>
        /// 検索処理を行う
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                //受入
                if (this.form.chkUkeire.Checked && !this.form.chkShukka.Checked
                    && !this.form.chkUriageshiharai.Checked)
                {
                    var sql = new StringBuilder();
                    sql.Append(" SELECT ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 明細・確定区分, ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 比較確定区分, ");
                    sql.Append("SUMMARY.分類 , ");
                    sql.Append("SUMMARY.JYOHOU_RIYOU_KUBUN AS 情報確定利用区分, ");
                    sql.Append("SUMMARY.SYSTEM_ID AS システムID, ");
                    sql.Append("SUMMARY.SEQ AS 枝番, ");
                    if (string.IsNullOrEmpty(this.selectQuery))
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID ");
                    }
                    else
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID, ");
                        sql.Append(this.selectQuery);
                    }
                    sql.Append(" FROM ");
                    MakeSearchUkeire(sql);
                    MakeWhereSql(sql);
                    this.mcreateSql = sql.ToString();
                }
                //出荷
                if (!this.form.chkUkeire.Checked && this.form.chkShukka.Checked
                   && !this.form.chkUriageshiharai.Checked)
                {
                    var sql = new StringBuilder();
                    sql.Append(" SELECT ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 明細・確定区分, ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 比較確定区分, ");
                    sql.Append("SUMMARY.分類 , ");
                    sql.Append("SUMMARY.JYOHOU_RIYOU_KUBUN AS 情報確定利用区分, ");
                    sql.Append("SUMMARY.SYSTEM_ID AS システムID, ");
                    sql.Append("SUMMARY.SEQ AS 枝番, ");
                    if (string.IsNullOrEmpty(this.selectQuery))
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID ");
                    }
                    else
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID, ");
                        sql.Append(this.selectQuery);
                    }
                    sql.Append(" FROM ");
                    MakeSearchShukka(sql);
                    MakeWhereSql(sql);
                    this.mcreateSql = sql.ToString();
                }
                // 売上/支払
                if (!this.form.chkUkeire.Checked && !this.form.chkShukka.Checked
                     && this.form.chkUriageshiharai.Checked)
                {

                    var sql = new StringBuilder();
                    sql.Append(" SELECT ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 明細・確定区分, ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 比較確定区分, ");
                    sql.Append("SUMMARY.分類 , ");
                    sql.Append("SUMMARY.JYOHOU_RIYOU_KUBUN AS 情報確定利用区分, ");
                    sql.Append("SUMMARY.SYSTEM_ID AS システムID, ");
                    sql.Append("SUMMARY.SEQ AS 枝番, ");
                    if (string.IsNullOrEmpty(this.selectQuery))
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID ");
                    }
                    else
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID, ");
                        sql.Append(this.selectQuery);
                    }
                    sql.Append(" FROM ");
                    MakeSearchUriageShiharai(sql);
                    MakeWhereSql(sql);
                    this.mcreateSql = sql.ToString();
                }
                //受入、出荷
                if (this.form.chkUkeire.Checked && this.form.chkShukka.Checked
                     && !this.form.chkUriageshiharai.Checked)
                {

                    var sql = new StringBuilder();
                    sql.Append(" SELECT ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 明細・確定区分, ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 比較確定区分, ");
                    sql.Append("SUMMARY.分類 , ");
                    sql.Append("SUMMARY.JYOHOU_RIYOU_KUBUN AS 情報確定利用区分, ");
                    sql.Append("SUMMARY.SYSTEM_ID AS システムID, ");
                    sql.Append("SUMMARY.SEQ AS 枝番, ");
                    if (string.IsNullOrEmpty(this.selectQuery))
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID ");
                    }
                    else
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID, ");
                        sql.Append(this.selectQuery);
                    }
                    sql.Append(" FROM ");
                    MakeSearchUkeire(sql);
                    MakeWhereSql(sql);
                    sql.Append(" UNION ALL ");
                    sql.Append(" SELECT ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 明細・確定区分, ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 比較確定区分, ");
                    sql.Append("SUMMARY.分類 , ");
                    sql.Append("SUMMARY.JYOHOU_RIYOU_KUBUN AS 情報確定利用区分, ");
                    sql.Append("SUMMARY.SYSTEM_ID AS システムID, ");
                    sql.Append("SUMMARY.SEQ AS 枝番, ");
                    if (string.IsNullOrEmpty(this.selectQuery))
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID ");
                    }
                    else
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID, ");
                        sql.Append(this.selectQuery);
                    }
                    sql.Append(" FROM ");
                    MakeSearchShukka(sql);
                    MakeWhereSql(sql);
                    this.mcreateSql = sql.ToString();
                }
                //受入、売上/支払
                if (this.form.chkUkeire.Checked && !this.form.chkShukka.Checked
                      && this.form.chkUriageshiharai.Checked)
                {

                    var sql = new StringBuilder();
                    sql.Append(" SELECT ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 明細・確定区分, ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 比較確定区分, ");
                    sql.Append("SUMMARY.分類 , ");
                    sql.Append("SUMMARY.JYOHOU_RIYOU_KUBUN AS 情報確定利用区分, ");
                    sql.Append("SUMMARY.SYSTEM_ID AS システムID, ");
                    sql.Append("SUMMARY.SEQ AS 枝番, ");
                    if (string.IsNullOrEmpty(this.selectQuery))
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID ");
                    }
                    else
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID, ");
                        sql.Append(this.selectQuery);
                    }
                    sql.Append(" FROM ");
                    MakeSearchUkeire(sql);
                    MakeWhereSql(sql);
                    sql.Append(" UNION ALL ");
                    sql.Append(" SELECT ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 明細・確定区分, ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 比較確定区分, ");
                    sql.Append("SUMMARY.分類 , ");
                    sql.Append("SUMMARY.JYOHOU_RIYOU_KUBUN AS 情報確定利用区分, ");
                    sql.Append("SUMMARY.SYSTEM_ID AS システムID, ");
                    sql.Append("SUMMARY.SEQ AS 枝番, ");
                    if (string.IsNullOrEmpty(this.selectQuery))
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID ");
                    }
                    else
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID, ");
                        sql.Append(this.selectQuery);
                    }
                    sql.Append(" FROM ");
                    MakeSearchUriageShiharai(sql);
                    MakeWhereSql(sql);
                    this.mcreateSql = sql.ToString();
                }
                //出荷、売上/支払
                if (!this.form.chkUkeire.Checked && this.form.chkShukka.Checked
                    && this.form.chkUriageshiharai.Checked)
                {

                    var sql = new StringBuilder();
                    sql.Append(" SELECT ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 明細・確定区分, ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 比較確定区分, ");
                    sql.Append("SUMMARY.分類 , ");
                    sql.Append("SUMMARY.JYOHOU_RIYOU_KUBUN AS 情報確定利用区分, ");
                    sql.Append("SUMMARY.SYSTEM_ID AS システムID, ");
                    sql.Append("SUMMARY.SEQ AS 枝番, ");
                    if (string.IsNullOrEmpty(this.selectQuery))
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID ");
                    }
                    else
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID, ");
                        sql.Append(this.selectQuery);
                    }
                    sql.Append(" FROM ");
                    MakeSearchShukka(sql);
                    MakeWhereSql(sql);
                    sql.Append(" UNION ALL ");
                    sql.Append(" SELECT ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 明細・確定区分, ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 比較確定区分, ");
                    sql.Append("SUMMARY.分類 , ");
                    sql.Append("SUMMARY.JYOHOU_RIYOU_KUBUN AS 情報確定利用区分, ");
                    sql.Append("SUMMARY.SYSTEM_ID AS システムID, ");
                    sql.Append("SUMMARY.SEQ AS 枝番, ");
                    if (string.IsNullOrEmpty(this.selectQuery))
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID ");
                    }
                    else
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID, ");
                        sql.Append(this.selectQuery);
                    }
                    sql.Append(" FROM ");
                    MakeSearchUriageShiharai(sql);
                    MakeWhereSql(sql);
                    this.mcreateSql = sql.ToString();
                }
                ///受入、出荷、売上/支払
                if (this.form.chkUkeire.Checked && this.form.chkShukka.Checked
                   && this.form.chkUriageshiharai.Checked)
                {

                    var sql = new StringBuilder();
                    sql.Append(" SELECT ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 明細・確定区分, ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 比較確定区分, ");
                    sql.Append("SUMMARY.分類 , ");
                    sql.Append("SUMMARY.JYOHOU_RIYOU_KUBUN AS 情報確定利用区分, ");
                    sql.Append("SUMMARY.SYSTEM_ID AS システムID, ");
                    sql.Append("SUMMARY.SEQ AS 枝番, ");
                    if (string.IsNullOrEmpty(this.selectQuery))
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID ");
                    }
                    else
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID, ");
                        sql.Append(this.selectQuery);
                    }
                    sql.Append(" FROM ");
                    MakeSearchUkeire(sql);
                    MakeWhereSql(sql);
                    sql.Append(" UNION ALL ");
                    sql.Append(" SELECT ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 明細・確定区分, ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 比較確定区分, ");
                    sql.Append("SUMMARY.分類 , ");
                    sql.Append("SUMMARY.JYOHOU_RIYOU_KUBUN AS 情報確定利用区分, ");
                    sql.Append("SUMMARY.SYSTEM_ID AS システムID, ");
                    sql.Append("SUMMARY.SEQ AS 枝番, ");
                    if (string.IsNullOrEmpty(this.selectQuery))
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID ");
                    }
                    else
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID, ");
                        sql.Append(this.selectQuery);
                    }
                    sql.Append(" FROM ");
                    MakeSearchShukka(sql);
                    MakeWhereSql(sql);
                    sql.Append(" UNION ALL ");
                    sql.Append(" SELECT ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 明細・確定区分, ");
                    sql.Append("SUMMARY.DETAIL_KAKUTEI_KBN AS 比較確定区分, ");
                    sql.Append("SUMMARY.分類 , ");
                    sql.Append("SUMMARY.JYOHOU_RIYOU_KUBUN AS 情報確定利用区分, ");
                    sql.Append("SUMMARY.SYSTEM_ID AS システムID, ");
                    sql.Append("SUMMARY.SEQ AS 枝番, ");
                    if (string.IsNullOrEmpty(this.selectQuery))
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID ");
                    }
                    else
                    {
                        sql.Append("SUMMARY.DETAIL_DETAIL_SYSTEM_ID AS 明細・システムID, ");
                        sql.Append(this.selectQuery);
                    }
                    sql.Append(" FROM ");
                    MakeSearchUriageShiharai(sql);
                    MakeWhereSql(sql);
                    this.mcreateSql = sql.ToString();
                }
                //検索実行
                this.searchResult = new DataTable();
                DataTable db = new DataTable();
                DataTable dtResult = new DataTable();

                if (!string.IsNullOrEmpty(this.mcreateSql))
                {
                    db = t_ichirandao.getdateforstringsql(this.mcreateSql);
                }

                // 取得したデータのグリッド用加工
                dtResult = db.Clone();
                foreach (DataColumn col in dtResult.Columns)
                {
                    if (col.ColumnName == "明細・確定区分")
                    {
                        //Boolean型
                        col.DataType = typeof(Boolean);
                    }
                    if (col.ColumnName == "比較確定区分")
                    {
                        //Boolean型
                        col.DataType = typeof(Boolean);
                    }

                    if (col.ColumnName == "滞留登録区分")
                    {
                        col.DataType = typeof(int);
                    }

                }
                foreach (DataRow row in db.Rows)
                {
                    DataRow rowNew = dtResult.NewRow();

                    if (db.Columns.Contains("明細・確定区分"))
                    {
                        if (row["明細・確定区分"].ToString().Equals(KAKUTEI_KBN_KAKUTEI))
                        {

                            rowNew["明細・確定区分"] = true;
                            rowNew["比較確定区分"] = true;
                        }
                        else
                        {
                            rowNew["明細・確定区分"] = false;
                            rowNew["比較確定区分"] = false;
                        }
                    }

                    // 明細・確定区分, 比較確定区分以外のカラムを全て新しい行にコピーする
                    foreach (DataColumn col in db.Columns.Cast<DataColumn>().Where(n => n.ColumnName != "明細・確定区分" && n.ColumnName != "比較確定区分"))
                    {
                        rowNew[col.ColumnName] = row[col.ColumnName];
                    }

                    #region 個別にコピー（コメントアウト）

                    //if (db.Columns.Contains("分類"))
                    //{
                    //    rowNew["分類"] = row["分類"];
                    //}
                    //if (db.Columns.Contains("システムID"))
                    //{
                    //    rowNew["システムID"] = row["システムID"];
                    //}
                    //if (db.Columns.Contains("枝番"))
                    //{
                    //    rowNew["枝番"] = row["枝番"];
                    //}
                    //if (db.Columns.Contains("滞留登録区分"))
                    //{
                    //    rowNew["滞留登録区分"] = row["滞留登録区分"];
                    //}
                    //if (db.Columns.Contains("拠点CD"))
                    //{
                    //    rowNew["拠点CD"] = row["拠点CD"];
                    //}
                    //if (db.Columns.Contains("伝票番号"))
                    //{
                    //    rowNew["伝票番号"] = row["伝票番号"];
                    //}
                    //if (db.Columns.Contains("日連番"))
                    //{
                    //    rowNew["日連番"] = row["日連番"];
                    //}
                    //if (db.Columns.Contains("年連番"))
                    //{
                    //    rowNew["年連番"] = row["年連番"];
                    //}
                    //if (db.Columns.Contains("確定区分"))
                    //{
                    //    rowNew["確定区分"] = row["確定区分"];
                    //}
                    //if (db.Columns.Contains("伝票日付"))
                    //{
                    //    rowNew["伝票日付"] = row["伝票日付"];
                    //}
                    //if (db.Columns.Contains("売上日付"))
                    //{
                    //    rowNew["売上日付"] = row["売上日付"];
                    //}
                    //if (db.Columns.Contains("支払日付"))
                    //{
                    //    rowNew["支払日付"] = row["支払日付"];
                    //}
                    //if (db.Columns.Contains("取引先CD"))
                    //{
                    //    rowNew["取引先CD"] = row["取引先CD"];
                    //}
                    //if (db.Columns.Contains("取引先名"))
                    //{
                    //    rowNew["取引先名"] = row["取引先名"];
                    //}
                    //if (db.Columns.Contains("業者CD"))
                    //{
                    //    rowNew["業者CD"] = row["業者CD"];
                    //}
                    //if (db.Columns.Contains("業者名"))
                    //{
                    //    rowNew["業者名"] = row["業者名"];
                    //}
                    //if (db.Columns.Contains("現場CD"))
                    //{
                    //    rowNew["現場CD"] = row["現場CD"];
                    //}
                    //if (db.Columns.Contains("現場名"))
                    //{
                    //    rowNew["現場名"] = row["現場名"];
                    //}

                    //if (db.Columns.Contains("荷積業者CD"))
                    //{
                    //    rowNew["荷積業者CD"] = row["荷積業者CD"];
                    //}
                    //if (db.Columns.Contains("荷積業者名"))
                    //{
                    //    rowNew["荷積業者名"] = row["荷積業者名"];
                    //}
                    //if (db.Columns.Contains("荷積現場CD"))
                    //{
                    //    rowNew["荷積現場CD"] = row["荷積現場CD"];
                    //}
                    //if (db.Columns.Contains("荷積現場名"))
                    //{
                    //    rowNew["荷積現場名"] = row["荷積現場名"];
                    //}

                    //if (db.Columns.Contains("荷降業者CD"))
                    //{
                    //    rowNew["荷降業者CD"] = row["荷降業者CD"];
                    //}
                    //if (db.Columns.Contains("荷降業者名"))
                    //{
                    //    rowNew["荷降業者名"] = row["荷降業者名"];
                    //}
                    //if (db.Columns.Contains("営業担当者CD"))
                    //{
                    //    rowNew["営業担当者CD"] = row["営業担当者CD"];
                    //}
                    //if (db.Columns.Contains("営業担当者名"))
                    //{
                    //    rowNew["営業担当者名"] = row["営業担当者名"];
                    //}
                    //if (db.Columns.Contains("入力担当者CD"))
                    //{
                    //    rowNew["入力担当者CD"] = row["入力担当者CD"];
                    //}
                    //if (db.Columns.Contains("入力担当者名"))
                    //{
                    //    rowNew["入力担当者名"] = row["入力担当者名"];
                    //}
                    //if (db.Columns.Contains("車輌CD"))
                    //{
                    //    rowNew["車輌CD"] = row["車輌CD"];
                    //}
                    //if (db.Columns.Contains("車輌名"))
                    //{
                    //    rowNew["車輌名"] = row["車輌名"];
                    //}
                    //if (db.Columns.Contains("車種CD"))
                    //{
                    //    rowNew["車種CD"] = row["車種CD"];
                    //}
                    //if (db.Columns.Contains("車種名"))
                    //{
                    //    rowNew["車種名"] = row["車種名"];
                    //}
                    //if (db.Columns.Contains("運搬業者CD"))
                    //{
                    //    rowNew["運搬業者CD"] = row["運搬業者CD"];
                    //}
                    //if (db.Columns.Contains("運搬業者名"))
                    //{
                    //    rowNew["運搬業者名"] = row["運搬業者名"];
                    //}
                    //if (db.Columns.Contains("運転者CD"))
                    //{
                    //    rowNew["運転者CD"] = row["運転者CD"];
                    //}
                    //if (db.Columns.Contains("運転者名"))
                    //{
                    //    rowNew["運転者名"] = row["運転者名"];
                    //}
                    //if (db.Columns.Contains("人数"))
                    //{
                    //    rowNew["人数"] = row["人数"];
                    //}
                    //if (db.Columns.Contains("形態区分CD"))
                    //{
                    //    rowNew["形態区分CD"] = row["形態区分CD"];
                    //}
                    //if (db.Columns.Contains("台貫区分"))
                    //{
                    //    rowNew["台貫区分"] = row["台貫区分"];
                    //}
                    //if (db.Columns.Contains("コンテナ操作CD"))
                    //{
                    //    rowNew["コンテナ操作CD"] = row["コンテナ操作CD"];
                    //}
                    //if (db.Columns.Contains("マニフェスト種類CD"))
                    //{
                    //    rowNew["マニフェスト種類CD"] = row["マニフェスト種類CD"];
                    //}
                    //if (db.Columns.Contains("マニフェスト手配CD"))
                    //{
                    //    rowNew["マニフェスト手配CD"] = row["マニフェスト手配CD"];
                    //}
                    //if (db.Columns.Contains("伝票備考"))
                    //{
                    //    rowNew["伝票備考"] = row["伝票備考"];
                    //}
                    //if (db.Columns.Contains("滞留備考"))
                    //{
                    //    rowNew["滞留備考"] = row["滞留備考"];
                    //}
                    //if (db.Columns.Contains("受付番号"))
                    //{
                    //    rowNew["受付番号"] = row["受付番号"];
                    //}
                    //if (db.Columns.Contains("計量番号"))
                    //{
                    //    rowNew["計量番号"] = row["計量番号"];
                    //}
                    //if (db.Columns.Contains("領収書番号"))
                    //{
                    //    rowNew["領収書番号"] = row["領収書番号"];
                    //}
                    //if (db.Columns.Contains("正味合計"))
                    //{
                    //    rowNew["正味合計"] = row["正味合計"];
                    //}
                    //if (db.Columns.Contains("売上消費税率"))
                    //{
                    //    rowNew["売上消費税率"] = row["売上消費税率"];
                    //}
                    //if (db.Columns.Contains("売上金額合計"))
                    //{
                    //    rowNew["売上金額合計"] = row["売上金額合計"];
                    //}
                    //if (db.Columns.Contains("売上伝票毎消費税外税"))
                    //{
                    //    rowNew["売上伝票毎消費税外税"] = row["売上伝票毎消費税外税"];
                    //}
                    //if (db.Columns.Contains("売上伝票毎消費税内税"))
                    //{
                    //    rowNew["売上伝票毎消費税内税"] = row["売上伝票毎消費税内税"];
                    //}
                    //if (db.Columns.Contains("売上明細毎消費税外税合計"))
                    //{
                    //    rowNew["売上明細毎消費税外税合計"] = row["売上明細毎消費税外税合計"];
                    //}
                    //if (db.Columns.Contains("売上明細毎消費税内税合計"))
                    //{
                    //    rowNew["売上明細毎消費税内税合計"] = row["売上明細毎消費税内税合計"];
                    //}
                    //if (db.Columns.Contains("品名別売上金額合計"))
                    //{
                    //    rowNew["品名別売上金額合計"] = row["品名別売上金額合計"];
                    //}
                    //if (db.Columns.Contains("品名別売上消費税外税合計"))
                    //{
                    //    rowNew["品名別売上消費税外税合計"] = row["品名別売上消費税外税合計"];
                    //}
                    //if (db.Columns.Contains("品名別売上消費税内税合計"))
                    //{
                    //    rowNew["品名別売上消費税内税合計"] = row["品名別売上消費税内税合計"];
                    //}
                    //if (db.Columns.Contains("支払消費税率"))
                    //{
                    //    rowNew["支払消費税率"] = row["支払消費税率"];
                    //}
                    //if (db.Columns.Contains("支払金額合計"))
                    //{
                    //    rowNew["支払金額合計"] = row["支払金額合計"];
                    //}
                    //if (db.Columns.Contains("支払伝票毎消費税外税"))
                    //{
                    //    rowNew["支払伝票毎消費税外税"] = row["支払伝票毎消費税外税"];
                    //}
                    //if (db.Columns.Contains("支払伝票毎消費税内税"))
                    //{
                    //    rowNew["支払伝票毎消費税内税"] = row["支払伝票毎消費税内税"];
                    //}
                    //if (db.Columns.Contains("支払明細毎消費税外税合計"))
                    //{
                    //    rowNew["支払明細毎消費税外税合計"] = row["支払明細毎消費税外税合計"];
                    //}
                    //if (db.Columns.Contains("支払明細毎消費税内税合計"))
                    //{
                    //    rowNew["支払明細毎消費税内税合計"] = row["支払明細毎消費税内税合計"];
                    //}
                    //if (db.Columns.Contains("品名別支払金額合計"))
                    //{
                    //    rowNew["品名別支払金額合計"] = row["品名別支払金額合計"];
                    //}
                    //if (db.Columns.Contains("品名別支払消費税外税合計"))
                    //{
                    //    rowNew["品名別支払消費税外税合計"] = row["品名別支払消費税外税合計"];
                    //}
                    //if (db.Columns.Contains("品名別支払消費税内税合計"))
                    //{
                    //    rowNew["品名別支払消費税内税合計"] = row["品名別支払消費税内税合計"];
                    //}
                    //if (db.Columns.Contains("売上税計算区分CD"))
                    //{
                    //    rowNew["売上税計算区分CD"] = row["売上税計算区分CD"];
                    //}
                    //if (db.Columns.Contains("売上税区分CD"))
                    //{
                    //    rowNew["売上税区分CD"] = row["売上税区分CD"];
                    //}
                    //if (db.Columns.Contains("売上取引区分CD"))
                    //{
                    //    rowNew["売上取引区分CD"] = row["売上取引区分CD"];
                    //}
                    //if (db.Columns.Contains("支払税計算区分CD"))
                    //{
                    //    rowNew["支払税計算区分CD"] = row["支払税計算区分CD"];
                    //}
                    //if (db.Columns.Contains("支払税区分CD"))
                    //{
                    //    rowNew["支払税区分CD"] = row["支払税区分CD"];
                    //}
                    //if (db.Columns.Contains("支払取引区分CD"))
                    //{
                    //    rowNew["支払取引区分CD"] = row["支払取引区分CD"];
                    //}

                    //if (db.Columns.Contains("支払取引区分CD"))
                    //{
                    //    rowNew["支払取引区分CD"] = row["支払取引区分CD"];
                    //}

                    //if (db.Columns.Contains("月極一括作成区分"))
                    //{
                    //    rowNew["月極一括作成区分"] = row["月極一括作成区分"];
                    //}
                    //if (db.Columns.Contains("検収日付"))
                    //{
                    //    rowNew["検収日付"] = row["検収日付"];
                    //}
                    //if (db.Columns.Contains("出荷時正味合計"))
                    //{
                    //    rowNew["出荷時正味合計"] = row["出荷時正味合計"];
                    //}
                    //if (db.Columns.Contains("検収時正味合計"))
                    //{
                    //    rowNew["検収時正味合計"] = row["検収時正味合計"];
                    //}
                    //if (db.Columns.Contains("差分"))
                    //{
                    //    rowNew["差分"] = row["差分"];
                    //}
                    //if (db.Columns.Contains("出荷金額合計"))
                    //{
                    //    rowNew["出荷金額合計"] = row["出荷金額合計"];
                    //}
                    //if (db.Columns.Contains("検収金額合計"))
                    //{
                    //    rowNew["検収金額合計"] = row["検収金額合計"];
                    //}
                    //if (db.Columns.Contains("差額"))
                    //{
                    //    rowNew["差額"] = row["差額"];
                    //}
                    //if (db.Columns.Contains("作成者"))
                    //{
                    //    rowNew["作成者"] = row["作成者"];
                    //}
                    //if (db.Columns.Contains("作成日時"))
                    //{
                    //    rowNew["作成日時"] = row["作成日時"];
                    //}
                    //if (db.Columns.Contains("作成PC"))
                    //{
                    //    rowNew["作成PC"] = row["作成PC"];
                    //}
                    //if (db.Columns.Contains("最終更新者"))
                    //{
                    //    rowNew["最終更新者"] = row["最終更新者"];
                    //}
                    //if (db.Columns.Contains("最終更新日時"))
                    //{
                    //    rowNew["最終更新日時"] = row["最終更新日時"];
                    //}
                    //if (db.Columns.Contains("最終更新PC"))
                    //{
                    //    rowNew["最終更新PC"] = row["最終更新PC"];
                    //}

                    //if (db.Columns.Contains("情報確定利用区分"))
                    //{
                    //    rowNew["情報確定利用区分"] = row["情報確定利用区分"];
                    //}

                    //if (db.Columns.Contains("明細・システムID"))
                    //{
                    //    rowNew["明細・システムID"] = row["明細・システムID"];
                    //}
                    //if (db.Columns.Contains("明細・枝番"))
                    //{
                    //    rowNew["明細・枝番"] = row["明細・枝番"];
                    //}
                    //if (db.Columns.Contains("明細・明細システムID"))
                    //{
                    //    rowNew["明細・明細システムID"] = row["明細・明細システムID"];
                    //}
                    //if (db.Columns.Contains("明細・伝票番号"))
                    //{
                    //    rowNew["明細・伝票番号"] = row["明細・伝票番号"];
                    //}
                    //if (db.Columns.Contains("明細・行番号"))
                    //{
                    //    rowNew["明細・行番号"] = row["明細・行番号"];
                    //}
                    //if (db.Columns.Contains("明細・売上支払日付"))
                    //{
                    //    rowNew["明細・売上支払日付"] = row["明細・売上支払日付"];
                    //}
                    //if (db.Columns.Contains("明細・伝票区分CD"))
                    //{
                    //    rowNew["明細・伝票区分CD"] = row["明細・伝票区分CD"];
                    //}
                    //if (db.Columns.Contains("明細・品名CD"))
                    //{
                    //    rowNew["明細・品名CD"] = row["明細・品名CD"];
                    //}
                    //if (db.Columns.Contains("明細・品名"))
                    //{
                    //    rowNew["明細・品名"] = row["明細・品名"];
                    //}
                    //if (db.Columns.Contains("明細・数量"))
                    //{
                    //    rowNew["明細・数量"] = row["明細・数量"];
                    //}
                    //if (db.Columns.Contains("明細・単位CD"))
                    //{
                    //    rowNew["明細・単位CD"] = row["明細・単位CD"];
                    //}
                    //if (db.Columns.Contains("明細・単価"))
                    //{
                    //    rowNew["明細・単価"] = row["明細・単価"];
                    //}
                    //if (db.Columns.Contains("明細・金額"))
                    //{
                    //    rowNew["明細・金額"] = row["明細・金額"];
                    //}
                    //if (db.Columns.Contains("明細・消費税外税"))
                    //{
                    //    rowNew["明細・消費税外税"] = row["明細・消費税外税"];
                    //}
                    //if (db.Columns.Contains("明細・消費税内税"))
                    //{
                    //    rowNew["明細・消費税内税"] = row["明細・消費税内税"];
                    //}
                    //if (db.Columns.Contains("明細・品名別税区分CD"))
                    //{
                    //    rowNew["明細・品名別税区分CD"] = row["明細・品名別税区分CD"];
                    //}
                    //if (db.Columns.Contains("明細・品名別金額"))
                    //{
                    //    rowNew["明細・品名別金額"] = row["明細・品名別金額"];
                    //}
                    //if (db.Columns.Contains("明細・品名別消費税外税"))
                    //{
                    //    rowNew["明細・品名別消費税外税"] = row["明細・品名別消費税外税"];
                    //}
                    //if (db.Columns.Contains("明細・品名別消費税内税"))
                    //{
                    //    rowNew["明細・品名別消費税内税"] = row["明細・品名別消費税内税"];
                    //}
                    //if (db.Columns.Contains("明細・明細備考"))
                    //{
                    //    rowNew["明細・明細備考"] = row["明細・明細備考"];
                    //}
                    //if (db.Columns.Contains("明細・荷姿数量"))
                    //{
                    //    rowNew["明細・荷姿数量"] = row["明細・荷姿数量"];
                    //}
                    //if (db.Columns.Contains("明細・荷姿単位CD"))
                    //{
                    //    rowNew["明細・荷姿単位CD"] = row["明細・荷姿単位CD"];
                    //}
                    //if (db.Columns.Contains("明細・タイムスタンプ"))
                    //{
                    //    rowNew["明細・タイムスタンプ"] = row["明細・タイムスタンプ"];
                    //}

                    #endregion 個別にコピー（コメントアウト）

                    dtResult.Rows.Add(rowNew);
                }
                // 確定区分のカラムがあったら一旦削除(パターン一覧には登録しない想定だが)
                if (this.form.customDataGridView1.Columns.Contains("確定区分"))
                {
                    this.form.customDataGridView1.Columns.Remove("確定区分");
                }

                // 確定区分カラムを追加（チェックボックス）
                int count = dtResult.Rows.Count;

                if (db.Columns.Contains("明細・確定区分"))
                {
                    dtResult.Columns["明細・確定区分"].ReadOnly = false;

                }
                DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                newColumn.Name = "確定区分";
                DataGridviewCheckboxHeaderCell newheader = new DataGridviewCheckboxHeaderCell();
                newheader.OnCheckBoxClicked +=
                        new DataGridViewCheckBoxColumnHeader.DataGridviewCheckboxHeaderCell.datagridviewcheckboxHeaderEventHander(
                                ch_OnCheckBoxClicked);
                newColumn.HeaderCell = newheader;

                if (this.form.customDataGridView1.Columns.Count > 0)
                {
                    this.form.customDataGridView1.Columns.Insert(0, newColumn);
                }
                else
                {
                    this.form.customDataGridView1.Columns.Add(newColumn);
                }
                this.searchResult = dtResult;

                //読込データ件数を取得
                this.headForm.txtReadDataCnt.Text = count.ToString();
                // 一覧表示
                this.form.ShowData();

                // グリッド表示時にソートが反映されるため、dtresultとは別の並び順になっているのでdtResultをこれ以降使ってはいけない
                for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                {
                    this.form.customDataGridView1.Rows[i].Cells["確定区分"].Value = this.form.customDataGridView1.Rows[i].Cells["明細・確定区分"].Value;
                }
                //非表示項目
                foreach (DataGridViewColumn column in this.form.customDataGridView1.Columns)
                {
                    if (column.Name.Equals("明細・確定区分") || column.Name.Equals("情報確定利用区分")
                        || column.Name.Equals("比較確定区分"))
                    {
                        column.Visible = false;
                    }
                }

                // ソートされている場合、searchResultの中身とグリッドのソート状態が合っていないので、件数取得以外には使えないので注意
                return searchResult.Rows.Count;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.msgcls.MessageBoxShow("E245", "");
                return -1;
            }
        }

        /// <summary>
        /// 受入明細検索
        /// </summary>
        /// <param name="system_id"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        private DataTable SearchUkeireDetail(string system_id, string seq)
        {

            DataTable db = new DataTable();
            //SQL文格納StringBuilder
            var sql = new StringBuilder();
            sql.Append(" SELECT * ");
            sql.Append(" FROM ");
            sql.Append(" T_UKEIRE_DETAIL ");
            sql.Append(" WHERE ");
            sql.Append(" SYSTEM_ID = " + "'" + system_id + "'");
            sql.Append(" AND SEQ = " + "'" + seq + "'");

            db = t_ichirandao.getdateforstringsql(sql.ToString());
            return db;
        }

        /// <summary>
        /// 受入入力検索
        /// </summary>
        /// <param name="system_id"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        private DataTable SearchUkeireEntry(string system_id, string seq)
        {

            DataTable db = new DataTable();
            //SQL文格納StringBuilder
            var sql = new StringBuilder();
            sql.Append(" SELECT * ");

            sql.Append(" FROM ");
            sql.Append(" T_UKEIRE_ENTRY ");
            sql.Append(" WHERE ");
            sql.Append(" SYSTEM_ID = " + "'" + system_id + "'");
            sql.Append(" AND SEQ = " + "'" + seq + "'");
            db = t_ichirandao.getdateforstringsql(sql.ToString());
            return db;
        }

        /// <summary>
        /// 出荷明細検索
        /// </summary>
        /// <param name="system_id"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        private DataTable SearchShukaDetail(string system_id, string seq)
        {

            DataTable db = new DataTable();
            //SQL文格納StringBuilder
            var sql = new StringBuilder();
            sql.Append(" SELECT * ");

            sql.Append(" FROM ");
            sql.Append(" T_SHUKKA_DETAIL ");
            sql.Append(" WHERE ");
            sql.Append(" SYSTEM_ID = " + "'" + system_id + "'");
            sql.Append(" AND SEQ = " + "'" + seq + "'");

            db = t_ichirandao.getdateforstringsql(sql.ToString());
            return db;
        }

        /// <summary>
        /// 出荷入力検索
        /// </summary>
        /// <param name="system_id"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        private DataTable SearchShukaEntry(string system_id, string seq)
        {

            DataTable db = new DataTable();
            //SQL文格納StringBuilder
            var sql = new StringBuilder();
            sql.Append(" SELECT * ");

            sql.Append(" FROM ");
            sql.Append(" T_SHUKKA_ENTRY ");
            sql.Append(" WHERE ");
            sql.Append(" SYSTEM_ID = " + "'" + system_id + "'");
            sql.Append(" AND SEQ = " + "'" + seq + "'");
            db = t_ichirandao.getdateforstringsql(sql.ToString());
            return db;
        }

        /// <summary>
        /// 売上／支払明細検索
        /// </summary>
        /// <param name="system_id"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        private DataTable SearchUrShDetail(string system_id, string seq)
        {

            DataTable db = new DataTable();
            //SQL文格納StringBuilder
            var sql = new StringBuilder();
            sql.Append(" SELECT * ");

            sql.Append(" FROM ");
            sql.Append(" T_UR_SH_DETAIL ");
            sql.Append(" WHERE ");
            sql.Append(" SYSTEM_ID = " + "'" + system_id + "'");
            sql.Append(" AND SEQ = " + "'" + seq + "'");

            db = t_ichirandao.getdateforstringsql(sql.ToString());
            return db;
        }

        /// <summary>
        /// 売上／支払入力検索
        /// </summary>
        /// <param name="system_id"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        private DataTable SearchUrShEntry(string system_id, string seq)
        {

            DataTable db = new DataTable();
            //SQL文格納StringBuilder
            var sql = new StringBuilder();
            sql.Append(" SELECT * ");

            sql.Append(" FROM ");
            sql.Append(" T_UR_SH_ENTRY ");
            sql.Append(" WHERE ");
            sql.Append(" SYSTEM_ID = " + "'" + system_id + "'");
            sql.Append(" AND SEQ = " + "'" + seq + "'");
            db = t_ichirandao.getdateforstringsql(sql.ToString());
            return db;
        }

        #endregion

        #endregion DB関連処理

        #region メソッド

        /// <summary>
        /// ヘッダ初期値設定
        /// </summary>
        private void SetHeaderInit()
        {
            this.headForm.txtKyotenCd.Text = Properties.Settings.Default.SET_KYOTEN_CD;

            //前回保存値がない場合、または空の場合はシステム設定ファイルから拠点CDを設定する
            XMLAccessor fileAccess = new XMLAccessor();
            CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();
            if (string.IsNullOrEmpty(this.headForm.txtKyotenCd.Text))
            {
                this.headForm.txtKyotenCd.Text = String.Format("{0:D2}", int.Parse(configProfile.ItemSetVal1));
            }

            // ユーザ拠点名称の取得
            M_KYOTEN mKyoten = new M_KYOTEN();
            mKyoten = (M_KYOTEN)m_kyotendao.GetDataByCd(this.headForm.txtKyotenCd.Text);
            if (mKyoten == null)
            {
                this.headForm.txtKyotenNameRyaku.Text = "";
            }
            else
            {
                if (this.headForm.txtKyotenCd.Text != "")
                {
                    this.headForm.txtKyotenNameRyaku.Text = mKyoten.KYOTEN_NAME_RYAKU;
                }
            }

            // 要求残件対応 ID:148　2013/12/25
            // アラート件数非表示のみの対応のためコメントアウト
            // // システム情報からアラート件数を取得
            // this.headForm.txtAlertNumber.Text = msysinfo.ICHIRAN_ALERT_KENSUU.ToString();

            // 日付
            if (String.IsNullOrEmpty(Properties.Settings.Default.SET_HIDUKE_FROM))
            {
                this.headForm.dtpDateFrom.Text = this.parentForm.sysDate.ToString();
            }
            else
            {
                this.headForm.dtpDateFrom.Text = Properties.Settings.Default.SET_HIDUKE_FROM;
            }
            if (String.IsNullOrEmpty(Properties.Settings.Default.SET_HIDUKE_TO))
            {
                this.headForm.dtpDateTo.Text = this.parentForm.sysDate.ToString();
            }
            else
            {
                this.headForm.dtpDateTo.Text = Properties.Settings.Default.SET_HIDUKE_TO;
            }
            // 日付区分
            if (string.IsNullOrEmpty(Properties.Settings.Default.SET_HIDUKE_KBN))
            {
                // 前回入力していない場合は伝票日付をセット
                this.headForm.txtHidukeSentaku.Text = HIDUKE_KBN_DENPYOU;
            }
            else
            {
                this.headForm.txtHidukeSentaku.Text = Properties.Settings.Default.SET_HIDUKE_KBN;
            }

            // 読込データ件数初期値0
            this.headForm.txtReadDataCnt.Text = "0";
        }

        /// <summary>
        /// メインフォーム初期値設定
        /// </summary>
        private void SetFormInit()
        {
            // No.1781
            // システム情報を取得
            this.form.txtKakuteiKbn.Text = this.msysinfo.SHIHARAI_HYOUJI_JOUKEN.Value.ToString();
            this.form.chkShukka.Checked = this.msysinfo.SHIHARAI_HYOUJI_JOUKEN_SHUKKA.Value;
            this.form.chkUkeire.Checked = this.msysinfo.SHIHARAI_HYOUJI_JOUKEN_UKEIRE.Value;
            this.form.chkUriageshiharai.Checked = this.msysinfo.SHIHARAI_HYOUJI_JOUKEN_URIAGESHIHARAI.Value;
        }

        /// <summary>
        /// 画面でDataGridViewのスタイル設定
        /// </summary>
        private void SetStyleDtGridView()
        {
            //行の追加オプション(false)
            this.form.customDataGridView1.AllowUserToAddRows = false;
            ////ヘッダの背景色
            //this.form.customDataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro;
            ////ヘッダの文字色
            //this.form.customDataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        }

        /// <summary>
        /// 確定登録
        /// </summary>
        /// <remarks>現在のデータを論理削除後に新規データとして追加する</remarks>
        /// <returns>登録・更新があれば:true なければ:false</returns>
        [Transaction]
        public virtual bool KakuteiTouroku()
        {
            LogUtility.DebugMethodStart();

            int tourokuKensu = 0;
            this.isRegist = true;

            try
            {
                // 明細から登録用DTO作成
                this.GetMeisaiIchiranData("TOUROKU");

                // トランザクション開始（エラーまたはコミットしなければ自動でロールバックされる）
                using (Transaction tran = new Transaction())
                {
                    if (mukeiremslist != null && mukeiremslist.Count() > 0
                        && mukeireentrylistadd != null && mukeireentrylistadd.Count() > 0
                        && mukeireentrylistupdata != null && mukeireentrylistupdata.Count() > 0)
                    {
                        foreach (T_UKEIRE_DETAIL ukeire in mukeiremslist)
                        {
                            tourokuKensu += ukeirek_detail_daocls.Insert(ukeire);
                        }
                        foreach (T_UKEIRE_ENTRY ukeire in mukeireentrylistadd)
                        {
                            tourokuKensu += ukeirek_entry_daocls.Insert(ukeire);
                        }
                        foreach (T_UKEIRE_ENTRY ukeire in mukeireentrylistupdata)
                        {
                            tourokuKensu += ukeirek_entry_daocls.Update(ukeire);
                        }
                    }

                    if (mshukamslist != null && mshukamslist.Count() > 0
                       && mshukaentrylistadd != null && mshukaentrylistadd.Count() > 0
                       && mshukaentrylistupdata != null && mshukaentrylistupdata.Count() > 0)
                    {
                        foreach (T_SHUKKA_DETAIL shuka in mshukamslist)
                        {
                            tourokuKensu += shukkak_detail_daocls.Insert(shuka);
                        }
                        foreach (T_SHUKKA_ENTRY shuka in mshukaentrylistadd)
                        {
                            tourokuKensu += shukkak_entry_daocls.Insert(shuka);
                        }
                        foreach (T_SHUKKA_ENTRY shuka in mshukaentrylistupdata)
                        {
                            tourokuKensu += shukkak_entry_daocls.Update(shuka);
                        }
                    }

                    if (murshmslist != null && murshmslist.Count() > 0
                       && murshentrylistadd != null && murshentrylistadd.Count() > 0
                       && murshentrylistupdata != null && murshentrylistupdata.Count() > 0)
                    {
                        foreach (T_UR_SH_DETAIL ursh in murshmslist)
                        {
                            tourokuKensu += ur_shk_detail_daocls.Insert(ursh);
                        }
                        foreach (T_UR_SH_ENTRY ursh in murshentrylistadd)
                        {
                            tourokuKensu += ur_shk_entry_daocls.Insert(ursh);
                        }
                        foreach (T_UR_SH_ENTRY ursh in murshentrylistupdata)
                        {
                            tourokuKensu += ur_shk_entry_daocls.Update(ursh);
                        }
                    }

                    // コミット
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    // 排他エラー
                    //メッセージ出力して継続
                    msgcls.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    msgcls.MessageBoxShow("E093", "");
                }
                else
                {
                    msgcls.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
            }

            LogUtility.DebugMethodEnd();

            bool isTouroku = false;
            if (tourokuKensu > 0)
            {
                isTouroku = true;
            }
            return isTouroku;

        }

        /// <summary>
        /// 確定解除
        /// </summary>
        /// <remarks>現在のデータを論理削除後に新規データとして追加する</remarks>
        [Transaction]
        public virtual void KakuteiKaijyo()
        {
            LogUtility.DebugMethodStart();
            this.isRegist = true;

            try
            {
                // 明細から登録用DTO作成
                this.GetMeisaiIchiranData("KAIJYO");

                // トランザクション開始（エラーまたはコミットしなければ自動でロールバックされる）
                using (Transaction tran = new Transaction())
                {
                    if (mukeiremslist != null && mukeiremslist.Count() > 0
                         && mukeireentrylistadd != null && mukeireentrylistadd.Count() > 0
                         && mukeireentrylistupdata != null && mukeireentrylistupdata.Count() > 0)
                    {
                        foreach (T_UKEIRE_DETAIL ukeire in mukeiremslist)
                        {
                            int CntMopkUpd = ukeirek_detail_daocls.Insert(ukeire);
                        }
                        foreach (T_UKEIRE_ENTRY ukeire in mukeireentrylistadd)
                        {
                            int CntMopkUpd = ukeirek_entry_daocls.Insert(ukeire);
                        }
                        foreach (T_UKEIRE_ENTRY ukeire in mukeireentrylistupdata)
                        {
                            int CntMopkUpd = ukeirek_entry_daocls.Update(ukeire);
                        }
                    }

                    if (mshukamslist != null && mshukamslist.Count() > 0
                       && mshukaentrylistadd != null && mshukaentrylistadd.Count() > 0
                       && mshukaentrylistupdata != null && mshukaentrylistupdata.Count() > 0)
                    {
                        foreach (T_SHUKKA_DETAIL shuka in mshukamslist)
                        {
                            int CntMopkUpd = shukkak_detail_daocls.Insert(shuka);
                        }
                        foreach (T_SHUKKA_ENTRY shuka in mshukaentrylistadd)
                        {
                            int CntMopkUpd = shukkak_entry_daocls.Insert(shuka);
                        }
                        foreach (T_SHUKKA_ENTRY shuka in mshukaentrylistupdata)
                        {
                            int CntMopkUpd = shukkak_entry_daocls.Update(shuka);
                        }
                    }

                    if (murshmslist != null && murshmslist.Count() > 0
                       && murshentrylistadd != null && murshentrylistadd.Count() > 0
                       && murshentrylistupdata != null && murshentrylistupdata.Count() > 0)
                    {
                        foreach (T_UR_SH_DETAIL ursh in murshmslist)
                        {
                            int CntMopkUpd = ur_shk_detail_daocls.Insert(ursh);
                        }
                        foreach (T_UR_SH_ENTRY ursh in murshentrylistadd)
                        {
                            int CntMopkUpd = ur_shk_entry_daocls.Insert(ursh);
                        }
                        foreach (T_UR_SH_ENTRY ursh in murshentrylistupdata)
                        {
                            int CntMopkUpd = ur_shk_entry_daocls.Update(ursh);
                        }
                    }

                    // コミット
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    // 排他エラー
                    //メッセージ出力して継続
                    msgcls.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    msgcls.MessageBoxShow("E093", "");
                }
                else
                {
                    msgcls.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
            }

            LogUtility.DebugMethodEnd();
        }

        #region 検索関連

        /// <summary>
        /// Where条件作成
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeWhereSql(StringBuilder sql)
        {
            sql.Append(" WHERE ");
            sql.Append(" M1.DELETE_FLG = 0 ");

            if (this.headForm.dtpDateFrom.Value != null || this.headForm.dtpDateTo.Value != null)
            {
                sql.Append(" AND ");
            }

            if (this.headForm.rdoDenpyouHiduke.Checked)
            {
                if (this.headForm.dtpDateFrom.Value != null)
                {
                    sql.Append(" M1.DENPYOU_DATE >= '" + DateTime.Parse(this.headForm.dtpDateFrom.Value.ToString()) + "' ");
                }

                if (this.headForm.dtpDateFrom.Value != null && this.headForm.dtpDateTo.Value != null)
                {
                    sql.Append(" AND ");
                }

                if (this.headForm.dtpDateTo.Value != null)
                {
                    sql.Append(" M1.DENPYOU_DATE <= '" + DateTime.Parse(this.headForm.dtpDateTo.Value.ToString()) + "' ");
                }
            }
            else if (this.headForm.rdoNyuuryokuHiduke.Checked)
            {
                if (this.headForm.dtpDateFrom.Value != null)
                {
                    sql.Append(" M1.CREATE_DATE >= '" + DateTime.Parse(this.headForm.dtpDateFrom.Value.ToString()) + "' ");
                }

                if (this.headForm.dtpDateFrom.Value != null && this.headForm.dtpDateTo.Value != null)
                {
                    sql.Append(" AND ");
                }

                if (this.headForm.dtpDateTo.Value != null)
                {
                    sql.Append(" M1.CREATE_DATE <= '" + DateTime.Parse(this.headForm.dtpDateTo.Value.ToString()) + "' ");
                }
            }

            if (!String.IsNullOrEmpty(this.headForm.txtKyotenCd.Text))
            {
                sql.Append(" AND M1.KYOTEN_CD = '" + this.headForm.txtKyotenCd.Text + "' ");
            }
            
            sql.Append(" ) AS SUMMARY ");
        }

        /// <summary>
        /// 受入検索
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSearchUkeire(StringBuilder sql)
        {
            //SQL文格納StringBuilder
            sql.Append(" (SELECT ");
            sql.Append(" M1.SYSTEM_ID ");
            sql.Append(" ,M1.SEQ ");
            sql.Append(" ,M1.TAIRYUU_KBN ");
            sql.Append(" ,M1.KYOTEN_CD ");
            sql.Append(" ,N1.KYOTEN_NAME_RYAKU AS KYOTEN_NAME");
            sql.Append(" ,M1.UKEIRE_NUMBER AS DENPYOU_NUMBER");
            sql.Append(" ,M1.DATE_NUMBER ");
            sql.Append(" ,M1.YEAR_NUMBER ");
            sql.Append(" ,M1.KAKUTEI_KBN ");
            sql.Append(" ,M1.DENPYOU_DATE ");
            sql.Append(" ,M1.URIAGE_DATE ");
            sql.Append(" ,M1.SHIHARAI_DATE ");
            sql.Append(" ,M1.TORIHIKISAKI_CD ");
            sql.Append(" ,M1.TORIHIKISAKI_NAME ");
            sql.Append(" ,M1.GYOUSHA_CD ");
            sql.Append(" ,M1.GYOUSHA_NAME ");
            sql.Append(" ,M1.GENBA_CD ");
            sql.Append(" ,M1.GENBA_NAME ");

            sql.Append(" ,NULL AS NIZUMI_GYOUSHA_CD ");
            sql.Append(" ,NULL AS NIZUMI_GYOUSHA_NAME ");
            sql.Append(" ,NULL AS NIZUMI_GENBA_CD ");
            sql.Append(" ,NULL AS NIZUMI_GENBA_NAME ");

            sql.Append(" ,M1.NIOROSHI_GYOUSHA_CD ");
            sql.Append(" ,M1.NIOROSHI_GYOUSHA_NAME ");
            sql.Append(" ,M1.NIOROSHI_GENBA_CD ");
            sql.Append(" ,M1.NIOROSHI_GENBA_NAME ");
            sql.Append(" ,M1.EIGYOU_TANTOUSHA_CD ");
            sql.Append(" ,M1.EIGYOU_TANTOUSHA_NAME ");
            sql.Append(" ,M1.NYUURYOKU_TANTOUSHA_CD ");
            sql.Append(" ,M1.NYUURYOKU_TANTOUSHA_NAME ");
            sql.Append(" ,M1.SHARYOU_CD ");
            sql.Append(" ,M1.SHARYOU_NAME ");
            sql.Append(" ,M1.SHASHU_CD ");
            sql.Append(" ,M1.SHASHU_NAME ");
            sql.Append(" ,M1.UNPAN_GYOUSHA_CD ");
            sql.Append(" ,M1.UNPAN_GYOUSHA_NAME ");
            sql.Append(" ,M1.UNTENSHA_CD ");
            sql.Append(" ,M1.UNTENSHA_NAME ");
            sql.Append(" ,M1.NINZUU_CNT ");
            sql.Append(" ,M1.KEITAI_KBN_CD ");
            sql.Append(" ,N3.KEITAI_KBN_NAME_RYAKU AS KEITAI_KBN_NAME ");
            sql.Append(" ,M1.DAIKAN_KBN ");
            sql.Append(" ,M1.CONTENA_SOUSA_CD ");
            sql.Append(" ,N4.CONTENA_SOUSA_NAME_RYAKU AS CONTENA_SOUSA_NAME ");
            sql.Append(" ,M1.MANIFEST_SHURUI_CD ");
            sql.Append(" ,N5.MANIFEST_SHURUI_NAME_RYAKU AS MANIFEST_SHURUI_NAME ");
            sql.Append(" ,M1.MANIFEST_TEHAI_CD ");
            sql.Append(" ,N6.MANIFEST_TEHAI_NAME_RYAKU AS MANIFEST_TEHAI_NAME ");
            sql.Append(" ,M1.DENPYOU_BIKOU ");
            sql.Append(" ,M1.TAIRYUU_BIKOU ");
            sql.Append(" ,M1.UKETSUKE_NUMBER ");
            sql.Append(" ,M1.KEIRYOU_NUMBER ");
            sql.Append(" ,M1.RECEIPT_NUMBER ");
            sql.Append(" ,M1.NET_TOTAL ");
            sql.Append(" ,M1.URIAGE_SHOUHIZEI_RATE ");
            sql.Append(" ,M1.URIAGE_KINGAKU_TOTAL ");
            sql.Append(" ,M1.URIAGE_TAX_SOTO ");
            sql.Append(" ,M1.URIAGE_TAX_UCHI ");
            sql.Append(" ,M1.URIAGE_TAX_SOTO_TOTAL ");
            sql.Append(" ,M1.URIAGE_TAX_UCHI_TOTAL ");
            sql.Append(" ,M1.HINMEI_URIAGE_KINGAKU_TOTAL ");
            sql.Append(" ,M1.HINMEI_URIAGE_TAX_SOTO_TOTAL ");
            sql.Append(" ,M1.HINMEI_URIAGE_TAX_UCHI_TOTAL ");
            sql.Append(" ,M1.SHIHARAI_SHOUHIZEI_RATE ");
            sql.Append(" ,M1.SHIHARAI_KINGAKU_TOTAL ");
            sql.Append(" ,M1.SHIHARAI_TAX_SOTO ");
            sql.Append(" ,M1.SHIHARAI_TAX_UCHI ");
            sql.Append(" ,M1.SHIHARAI_TAX_SOTO_TOTAL ");
            sql.Append(" ,M1.SHIHARAI_TAX_UCHI_TOTAL ");
            sql.Append(" ,M1.HINMEI_SHIHARAI_KINGAKU_TOTAL ");
            sql.Append(" ,M1.HINMEI_SHIHARAI_TAX_SOTO_TOTAL ");
            sql.Append(" ,M1.HINMEI_SHIHARAI_TAX_UCHI_TOTAL ");
            sql.Append(" ,M1.URIAGE_ZEI_KEISAN_KBN_CD ");
            sql.Append(" ,CASE M1.URIAGE_ZEI_KEISAN_KBN_CD ");
            sql.Append(" WHEN '1' THEN '伝票毎' ");
            sql.Append(" WHEN '2' THEN '請求毎' ");
            sql.Append(" WHEN '3' THEN '明細毎' ");
            sql.Append(" ELSE '' ");
            sql.Append(" END AS URIAGE_ZEI_KEISAN_KBN_NAME ");
            sql.Append(" ,M1.URIAGE_ZEI_KBN_CD ");
            sql.Append(" ,CASE M1.URIAGE_ZEI_KBN_CD ");
            sql.Append(" WHEN '1' THEN '外税' ");
            sql.Append(" WHEN '2' THEN '内税' ");
            sql.Append(" WHEN '3' THEN '非課税' ");
            sql.Append(" ELSE '' ");
            sql.Append(" END AS URIAGE_ZEI_KBN_NAME ");
            sql.Append(" ,M1.URIAGE_TORIHIKI_KBN_CD ");
            sql.Append(" ,N7.TORIHIKI_KBN_NAME_RYAKU AS URIAGE_TORIHIKI_KBN_NAME ");
            sql.Append(" ,M1.SHIHARAI_ZEI_KEISAN_KBN_CD ");
            sql.Append(" ,CASE M1.SHIHARAI_ZEI_KEISAN_KBN_CD ");
            sql.Append(" WHEN '1' THEN '伝票毎' ");
            sql.Append(" WHEN '2' THEN '請求毎' ");
            sql.Append(" WHEN '3' THEN '明細毎' ");
            sql.Append(" ELSE '' ");
            sql.Append(" END AS SHIHARAI_ZEI_KEISAN_KBN_NAME ");
            sql.Append(" ,M1.SHIHARAI_ZEI_KBN_CD ");
            sql.Append(" ,CASE M1.SHIHARAI_ZEI_KBN_CD ");
            sql.Append(" WHEN '1' THEN '外税' ");
            sql.Append(" WHEN '2' THEN '内税' ");
            sql.Append(" WHEN '3' THEN '非課税' ");
            sql.Append(" ELSE '' ");
            sql.Append(" END AS SHIHARAI_ZEI_KBN_NAME ");
            sql.Append(" ,M1.SHIHARAI_TORIHIKI_KBN_CD ");
            sql.Append(" ,N8.TORIHIKI_KBN_NAME_RYAKU AS SHIHARAI_TORIHIKI_KBN_NAME ");

            sql.Append(" ,NULL AS TSUKI_CREATE_KBN ");
            sql.Append(" ,NULL AS KENSHU_DATE ");
            sql.Append(" ,NULL AS SHUKKA_NET_TOTAL ");
            sql.Append(" ,NULL AS KENSHU_NET_TOTAL ");
            sql.Append(" ,NULL AS SABUN ");
            sql.Append(" ,NULL AS SHUKKA_KINGAKU_TOTAL ");
            sql.Append(" ,NULL AS KENSHU_KINGAKU_TOTAL ");
            sql.Append(" ,NULL AS SAGAKU ");

            sql.Append(" ,M1.CREATE_USER ");
            sql.Append(" ,M1.CREATE_DATE ");
            sql.Append(" ,M1.CREATE_PC ");
            sql.Append(" ,M1.UPDATE_USER ");
            sql.Append(" ,M1.UPDATE_DATE ");
            sql.Append(" ,M1.UPDATE_PC ");
            sql.Append(" ,M1.DELETE_FLG ");
            sql.Append(" ,M1.TIME_STAMP ");

            sql.Append(" ,M3.UKEIRE_KAKUTEI_USE_KBN AS JYOHOU_RIYOU_KUBUN ");

            sql.Append(" ,M2.KAKUTEI_KBN AS DETAIL_KAKUTEI_KBN ");
            sql.Append(" ,'受入' AS 分類 ");
            sql.Append(" ,M2.SYSTEM_ID AS DETAIL_SYSTEM_ID ");
            sql.Append(" ,M2.SEQ AS DETAIL_SEQ ");
            sql.Append(" ,M2.DETAIL_SYSTEM_ID AS DETAIL_DETAIL_SYSTEM_ID ");
            sql.Append(" ,M2.UKEIRE_NUMBER AS DETAIL_DENPYOU_NUMBER ");
            sql.Append(" ,M2.ROW_NO AS DETAIL_ROW_NO ");
            sql.Append(" ,M2.URIAGESHIHARAI_DATE AS DETAIL_URIAGESHIHARAI_DATE ");

            sql.Append(" ,M2.STACK_JYUURYOU AS DETAIL_STACK_JYUURYOU ");
            sql.Append(" ,M2.EMPTY_JYUURYOU AS DETAIL_EMPTY_JYUURYOU ");
            sql.Append(" ,M2.WARIFURI_JYUURYOU AS DETAIL_WARIFURI_JYUURYOU ");
            sql.Append(" ,M2.WARIFURI_PERCENT AS DETAIL_WARIFURI_PERCENT ");
            sql.Append(" ,M2.CHOUSEI_JYUURYOU AS DETAIL_CHOUSEI_JYUURYOU ");
            sql.Append(" ,M2.CHOUSEI_PERCENT AS DETAIL_CHOUSEI_PERCENT ");
            sql.Append(" ,M2.YOUKI_CD AS DETAIL_YOUKI_CD ");
            sql.Append(" ,N10.YOUKI_NAME_RYAKU AS DETAIL_YOUKI_NAME ");
            sql.Append(" ,M2.YOUKI_SUURYOU AS DETAIL_YOUKI_SUURYOU ");
            sql.Append(" ,M2.YOUKI_JYUURYOU AS DETAIL_YOUKI_JYUURYOU ");

            sql.Append(" ,M2.DENPYOU_KBN_CD AS DETAIL_DENPYOU_KBN_CD ");
            sql.Append(" ,N9.DENPYOU_KBN_NAME_RYAKU AS DETAIL_DENPYOU_KBN_NAME ");
            sql.Append(" ,M2.HINMEI_CD AS DETAIL_HINMEI_CD ");
            sql.Append(" ,M2.HINMEI_NAME AS DETAIL_HINMEI_NAME ");

            sql.Append(" ,M2.NET_JYUURYOU AS DETAIL_NET_JYUURYOU ");

            sql.Append(" ,M2.SUURYOU AS DETAIL_SUURYOU ");
            sql.Append(" ,M2.UNIT_CD AS DETAIL_UNIT_CD ");
            sql.Append(" ,N11.UNIT_NAME_RYAKU AS DETAIL_UNIT_NAME ");

            sql.Append(" ,M2.TANKA AS DETAIL_TANKA ");
            sql.Append(" ,M2.KINGAKU AS DETAIL_KINGAKU ");
            sql.Append(" ,M2.TAX_SOTO AS DETAIL_TAX_SOTO ");
            sql.Append(" ,M2.TAX_UCHI AS DETAIL_TAX_UCHI ");
            sql.Append(" ,M2.HINMEI_ZEI_KBN_CD AS DETAIL_HINMEI_ZEI_KBN_CD ");
            sql.Append(" ,CASE M2.HINMEI_ZEI_KBN_CD ");
            sql.Append(" WHEN '1' THEN '外税' ");
            sql.Append(" WHEN '2' THEN '内税' ");
            sql.Append(" WHEN '3' THEN '非課税' ");
            sql.Append(" ELSE '' ");
            sql.Append(" END AS DETAIL_HINMEI_ZEI_KBN_NAME ");

            sql.Append(" ,M2.HINMEI_KINGAKU AS DETAIL_HINMEI_KINGAKU ");
            sql.Append(" ,M2.HINMEI_TAX_SOTO AS DETAIL_HINMEI_TAX_SOTO ");
            sql.Append(" ,M2.HINMEI_TAX_UCHI AS DETAIL_HINMEI_TAX_UCHI ");
            sql.Append(" ,M2.MEISAI_BIKOU AS DETAIL_MEISAI_BIKOU ");
            sql.Append(" ,M2.NISUGATA_SUURYOU AS DETAIL_NISUGATA_SUURYOU ");
            sql.Append(" ,M2.NISUGATA_UNIT_CD AS DETAIL_NISUGATA_UNIT_CD");
            sql.Append(" ,N12.UNIT_NAME_RYAKU AS DETAIL_NISUGATA_UNIT_NAME");
            sql.Append(" ,M2.TIME_STAMP AS DETAIL_TIME_STAMP ");
            sql.Append(" FROM T_UKEIRE_ENTRY M1 ");
            sql.Append(" INNER JOIN T_UKEIRE_DETAIL M2 ON ");
            sql.Append(" M1.SYSTEM_ID = M2.SYSTEM_ID  ");
            sql.Append(" AND M1.SEQ = M2.SEQ ");
            //伝票区分＝支払
            sql.Append(" AND M2.DENPYOU_KBN_CD = " + DENPYOU_KBN_SHIHARAI);
            if (this.form.rdoMikakutei.Checked)
            {
                // NULLの場合は未確定扱い
                sql.Append(" AND (M2.KAKUTEI_KBN IS NULL OR M2.KAKUTEI_KBN = '" + KAKUTEI_KBN_MIKAKUTEI + "') ");
            }
            if (this.form.rdoKakuteizumi.Checked)
            {
                sql.Append(" AND M2.KAKUTEI_KBN = '" + KAKUTEI_KBN_KAKUTEI + "' ");
            }
            sql.Append(" INNER JOIN M_SYS_INFO M3 ON M3.SYS_ID = '0' ");
            sql.Append(" LEFT JOIN M_KYOTEN N1 ON M1.KYOTEN_CD = N1.KYOTEN_CD ");
            sql.Append(" LEFT JOIN M_KEITAI_KBN N3 ON M1.KEITAI_KBN_CD = N3.KEITAI_KBN_CD ");
            sql.Append(" LEFT JOIN M_CONTENA_SOUSA N4 ON M1.CONTENA_SOUSA_CD = N4.CONTENA_SOUSA_CD ");
            sql.Append(" LEFT JOIN M_MANIFEST_SHURUI N5 ON M1.MANIFEST_SHURUI_CD = N5.MANIFEST_SHURUI_CD ");
            sql.Append(" LEFT JOIN M_MANIFEST_TEHAI N6 ON M1.MANIFEST_TEHAI_CD = N6.MANIFEST_TEHAI_CD ");
            sql.Append(" LEFT JOIN M_TORIHIKI_KBN N7 ON M1.URIAGE_TORIHIKI_KBN_CD = N7.TORIHIKI_KBN_CD ");
            sql.Append(" LEFT JOIN M_TORIHIKI_KBN N8 ON M1.SHIHARAI_TORIHIKI_KBN_CD = N8.TORIHIKI_KBN_CD ");
            sql.Append(" LEFT JOIN M_DENPYOU_KBN N9 ON M2.DENPYOU_KBN_CD = N9.DENPYOU_KBN_CD ");
            sql.Append(" LEFT JOIN M_YOUKI N10 ON M2.YOUKI_CD = N10.YOUKI_CD ");
            sql.Append(" LEFT JOIN M_UNIT N11 ON M2.UNIT_CD = N11.UNIT_CD ");
            sql.Append(" LEFT JOIN M_UNIT N12 ON M2.NISUGATA_UNIT_CD = N12.UNIT_CD");

        }
        /// <summary>
        /// 出荷検索
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSearchShukka(StringBuilder sql)
        {
            //SQL文格納StringBuilder
            sql.Append(" (SELECT ");
            sql.Append(" M1.SYSTEM_ID ");
            sql.Append(" ,M1.SEQ ");
            sql.Append(" ,M1.TAIRYUU_KBN ");
            sql.Append(" ,M1.KYOTEN_CD ");
            sql.Append(" ,N1.KYOTEN_NAME_RYAKU AS KYOTEN_NAME");
            sql.Append(" ,M1.SHUKKA_NUMBER AS DENPYOU_NUMBER");
            sql.Append(" ,M1.DATE_NUMBER ");
            sql.Append(" ,M1.YEAR_NUMBER ");
            sql.Append(" ,M1.KAKUTEI_KBN ");
            sql.Append(" ,M1.DENPYOU_DATE ");
            sql.Append(" ,M1.URIAGE_DATE ");
            sql.Append(" ,M1.SHIHARAI_DATE ");
            sql.Append(" ,M1.TORIHIKISAKI_CD ");
            sql.Append(" ,M1.TORIHIKISAKI_NAME ");
            sql.Append(" ,M1.GYOUSHA_CD ");
            sql.Append(" ,M1.GYOUSHA_NAME ");
            sql.Append(" ,M1.GENBA_CD ");
            sql.Append(" ,M1.GENBA_NAME ");
            sql.Append(" ,M1.NIZUMI_GYOUSHA_CD ");
            sql.Append(" ,M1.NIZUMI_GYOUSHA_NAME ");
            sql.Append(" ,M1.NIZUMI_GENBA_CD ");
            sql.Append(" ,M1.NIZUMI_GENBA_NAME ");

            sql.Append(" ,NULL AS NIOROSHI_GYOUSHA_CD ");
            sql.Append(" ,NULL AS NIOROSHI_GYOUSHA_NAME ");
            sql.Append(" ,NULL AS NIOROSHI_GENBA_CD ");
            sql.Append(" ,NULL AS NIOROSHI_GENBA_NAME ");

            sql.Append(" ,M1.EIGYOU_TANTOUSHA_CD ");
            sql.Append(" ,M1.EIGYOU_TANTOUSHA_NAME ");
            sql.Append(" ,M1.NYUURYOKU_TANTOUSHA_CD ");
            sql.Append(" ,M1.NYUURYOKU_TANTOUSHA_NAME ");
            sql.Append(" ,M1.SHARYOU_CD ");
            sql.Append(" ,M1.SHARYOU_NAME ");
            sql.Append(" ,M1.SHASHU_CD ");
            sql.Append(" ,M1.SHASHU_NAME ");
            sql.Append(" ,M1.UNPAN_GYOUSHA_CD ");
            sql.Append(" ,M1.UNPAN_GYOUSHA_NAME ");
            sql.Append(" ,M1.UNTENSHA_CD ");
            sql.Append(" ,M1.UNTENSHA_NAME ");
            sql.Append(" ,M1.NINZUU_CNT ");
            sql.Append(" ,M1.KEITAI_KBN_CD ");
            sql.Append(" ,N3.KEITAI_KBN_NAME_RYAKU AS KEITAI_KBN_NAME ");
            sql.Append(" ,M1.DAIKAN_KBN ");
            sql.Append(" ,M1.CONTENA_SOUSA_CD ");
            sql.Append(" ,N4.CONTENA_SOUSA_NAME_RYAKU AS CONTENA_SOUSA_NAME ");
            sql.Append(" ,M1.MANIFEST_SHURUI_CD ");
            sql.Append(" ,N5.MANIFEST_SHURUI_NAME_RYAKU AS MANIFEST_SHURUI_NAME ");
            sql.Append(" ,M1.MANIFEST_TEHAI_CD ");
            sql.Append(" ,N6.MANIFEST_TEHAI_NAME_RYAKU AS MANIFEST_TEHAI_NAME ");
            sql.Append(" ,M1.DENPYOU_BIKOU ");
            sql.Append(" ,M1.TAIRYUU_BIKOU ");
            sql.Append(" ,M1.UKETSUKE_NUMBER ");
            sql.Append(" ,M1.KEIRYOU_NUMBER ");
            sql.Append(" ,M1.RECEIPT_NUMBER ");
            sql.Append(" ,M1.NET_TOTAL ");
            sql.Append(" ,M1.URIAGE_SHOUHIZEI_RATE ");
            sql.Append(" ,M1.URIAGE_AMOUNT_TOTAL AS URIAGE_KINGAKU_TOTAL ");
            sql.Append(" ,M1.URIAGE_TAX_SOTO ");
            sql.Append(" ,M1.URIAGE_TAX_UCHI ");
            sql.Append(" ,M1.URIAGE_TAX_SOTO_TOTAL ");
            sql.Append(" ,M1.URIAGE_TAX_UCHI_TOTAL ");
            sql.Append(" ,M1.HINMEI_URIAGE_KINGAKU_TOTAL ");
            sql.Append(" ,M1.HINMEI_URIAGE_TAX_SOTO_TOTAL ");
            sql.Append(" ,M1.HINMEI_URIAGE_TAX_UCHI_TOTAL ");
            sql.Append(" ,M1.SHIHARAI_SHOUHIZEI_RATE ");
            sql.Append(" ,M1.SHIHARAI_AMOUNT_TOTAL AS SHIHARAI_KINGAKU_TOTAL");
            sql.Append(" ,M1.SHIHARAI_TAX_SOTO ");
            sql.Append(" ,M1.SHIHARAI_TAX_UCHI ");
            sql.Append(" ,M1.SHIHARAI_TAX_SOTO_TOTAL ");
            sql.Append(" ,M1.SHIHARAI_TAX_UCHI_TOTAL ");
            sql.Append(" ,M1.HINMEI_SHIHARAI_KINGAKU_TOTAL ");
            sql.Append(" ,M1.HINMEI_SHIHARAI_TAX_SOTO_TOTAL ");
            sql.Append(" ,M1.HINMEI_SHIHARAI_TAX_UCHI_TOTAL ");
            sql.Append(" ,M1.URIAGE_ZEI_KEISAN_KBN_CD ");
            sql.Append(" ,CASE M1.URIAGE_ZEI_KEISAN_KBN_CD ");
            sql.Append(" WHEN '1' THEN '伝票毎' ");
            sql.Append(" WHEN '2' THEN '請求毎' ");
            sql.Append(" WHEN '3' THEN '明細毎' ");
            sql.Append(" ELSE '' ");
            sql.Append(" END AS URIAGE_ZEI_KEISAN_KBN_NAME ");
            sql.Append(" ,M1.URIAGE_ZEI_KBN_CD ");
            sql.Append(" ,CASE M1.URIAGE_ZEI_KBN_CD ");
            sql.Append(" WHEN '1' THEN '外税' ");
            sql.Append(" WHEN '2' THEN '内税' ");
            sql.Append(" WHEN '3' THEN '非課税' ");
            sql.Append(" ELSE '' ");
            sql.Append(" END AS URIAGE_ZEI_KBN_NAME ");
            sql.Append(" ,M1.URIAGE_TORIHIKI_KBN_CD ");
            sql.Append(" ,N7.TORIHIKI_KBN_NAME_RYAKU AS URIAGE_TORIHIKI_KBN_NAME ");
            sql.Append(" ,M1.SHIHARAI_ZEI_KEISAN_KBN_CD ");
            sql.Append(" ,CASE M1.SHIHARAI_ZEI_KEISAN_KBN_CD ");
            sql.Append(" WHEN '1' THEN '伝票毎' ");
            sql.Append(" WHEN '2' THEN '請求毎' ");
            sql.Append(" WHEN '3' THEN '明細毎' ");
            sql.Append(" ELSE '' ");
            sql.Append(" END AS SHIHARAI_ZEI_KEISAN_KBN_NAME ");
            sql.Append(" ,M1.SHIHARAI_ZEI_KBN_CD ");
            sql.Append(" ,CASE M1.SHIHARAI_ZEI_KBN_CD ");
            sql.Append(" WHEN '1' THEN '外税' ");
            sql.Append(" WHEN '2' THEN '内税' ");
            sql.Append(" WHEN '3' THEN '非課税' ");
            sql.Append(" ELSE '' ");
            sql.Append(" END AS SHIHARAI_ZEI_KBN_NAME ");
            sql.Append(" ,M1.SHIHARAI_TORIHIKI_KBN_CD ");
            sql.Append(" ,N8.TORIHIKI_KBN_NAME_RYAKU AS SHIHARAI_TORIHIKI_KBN_NAME ");

            sql.Append(" ,NULL AS TSUKI_CREATE_KBN ");
            sql.Append(" ,M1.KENSHU_DATE ");
            sql.Append(" ,M1.SHUKKA_NET_TOTAL ");
            sql.Append(" ,M1.KENSHU_NET_TOTAL ");
            sql.Append(" ,M1.SABUN ");
            sql.Append(" ,M1.SHUKKA_KINGAKU_TOTAL ");
            sql.Append(" ,M1.KENSHU_KINGAKU_TOTAL ");
            sql.Append(" ,M1.SAGAKU ");

            sql.Append(" ,M1.CREATE_USER ");
            sql.Append(" ,M1.CREATE_DATE ");
            sql.Append(" ,M1.CREATE_PC ");
            sql.Append(" ,M1.UPDATE_USER ");
            sql.Append(" ,M1.UPDATE_DATE ");
            sql.Append(" ,M1.UPDATE_PC ");
            sql.Append(" ,M1.DELETE_FLG ");
            sql.Append(" ,M1.TIME_STAMP ");

            sql.Append(" ,M3.SHUKKA_KAKUTEI_USE_KBN AS JYOHOU_RIYOU_KUBUN ");

            sql.Append(" ,M2.KAKUTEI_KBN DETAIL_KAKUTEI_KBN ");
            sql.Append(" ,'出荷' 分類 ");
            sql.Append(" ,M2.SYSTEM_ID DETAIL_SYSTEM_ID ");
            sql.Append(" ,M2.SEQ DETAIL_SEQ ");
            sql.Append(" ,M2.DETAIL_SYSTEM_ID DETAIL_DETAIL_SYSTEM_ID ");
            sql.Append(" ,M2.SHUKKA_NUMBER DETAIL_DENPYOU_NUMBER ");
            sql.Append(" ,M2.ROW_NO DETAIL_ROW_NO ");
            sql.Append(" ,M2.URIAGESHIHARAI_DATE DETAIL_URIAGESHIHARAI_DATE ");

            sql.Append(" ,M2.STACK_JYUURYOU DETAIL_STACK_JYUURYOU ");
            sql.Append(" ,M2.EMPTY_JYUURYOU DETAIL_EMPTY_JYUURYOU ");
            sql.Append(" ,M2.WARIFURI_JYUURYOU DETAIL_WARIFURI_JYUURYOU ");
            sql.Append(" ,M2.WARIFURI_PERCENT DETAIL_WARIFURI_PERCENT ");
            sql.Append(" ,M2.CHOUSEI_JYUURYOU DETAIL_CHOUSEI_JYUURYOU ");
            sql.Append(" ,M2.CHOUSEI_PERCENT DETAIL_CHOUSEI_PERCENT ");
            sql.Append(" ,M2.YOUKI_CD DETAIL_YOUKI_CD ");
            sql.Append(" ,N10.YOUKI_NAME_RYAKU AS DETAIL_YOUKI_NAME ");
            sql.Append(" ,M2.YOUKI_SUURYOU DETAIL_YOUKI_SUURYOU ");
            sql.Append(" ,M2.YOUKI_JYUURYOU DETAIL_YOUKI_JYUURYOU ");


            sql.Append(" ,M2.DENPYOU_KBN_CD DETAIL_DENPYOU_KBN_CD ");
            sql.Append(" ,N9.DENPYOU_KBN_NAME_RYAKU AS DETAIL_DENPYOU_KBN_NAME ");
            sql.Append(" ,M2.HINMEI_CD DETAIL_HINMEI_CD ");
            sql.Append(" ,M2.HINMEI_NAME DETAIL_HINMEI_NAME ");

            sql.Append(" ,M2.NET_JYUURYOU DETAIL_NET_JYUURYOU ");

            sql.Append(" ,M2.SUURYOU DETAIL_SUURYOU ");
            sql.Append(" ,M2.UNIT_CD DETAIL_UNIT_CD ");
            sql.Append(" ,N11.UNIT_NAME_RYAKU AS DETAIL_UNIT_NAME ");
            sql.Append(" ,M2.TANKA DETAIL_TANKA ");
            sql.Append(" ,M2.KINGAKU DETAIL_KINGAKU ");
            sql.Append(" ,M2.TAX_SOTO DETAIL_TAX_SOTO ");
            sql.Append(" ,M2.TAX_UCHI DETAIL_TAX_UCHI ");
            sql.Append(" ,M2.HINMEI_ZEI_KBN_CD DETAIL_HINMEI_ZEI_KBN_CD ");
            sql.Append(" ,CASE M2.HINMEI_ZEI_KBN_CD ");
            sql.Append(" WHEN '1' THEN '外税' ");
            sql.Append(" WHEN '2' THEN '内税' ");
            sql.Append(" WHEN '3' THEN '非課税' ");
            sql.Append(" ELSE '' ");
            sql.Append(" END AS DETAIL_HINMEI_ZEI_KBN_NAME ");
            sql.Append(" ,M2.HINMEI_KINGAKU DETAIL_HINMEI_KINGAKU ");
            sql.Append(" ,M2.HINMEI_TAX_SOTO DETAIL_HINMEI_TAX_SOTO ");
            sql.Append(" ,M2.HINMEI_TAX_UCHI DETAIL_HINMEI_TAX_UCHI ");
            sql.Append(" ,M2.MEISAI_BIKOU DETAIL_MEISAI_BIKOU ");
            sql.Append(" ,M2.NISUGATA_SUURYOU DETAIL_NISUGATA_SUURYOU ");
            sql.Append(" ,M2.NISUGATA_UNIT_CD DETAIL_NISUGATA_UNIT_CD ");
            sql.Append(" ,N12.UNIT_NAME_RYAKU AS DETAIL_NISUGATA_UNIT_NAME");
            sql.Append(" ,M2.TIME_STAMP DETAIL_TIME_STAMP ");
            sql.Append(" FROM T_SHUKKA_ENTRY M1 ");
            sql.Append(" INNER JOIN T_SHUKKA_DETAIL M2 ON ");
            sql.Append(" M1.SYSTEM_ID = M2.SYSTEM_ID ");
            sql.Append(" AND M1.SEQ = M2.SEQ ");
            //伝票区分＝支払
            sql.Append(" AND M2.DENPYOU_KBN_CD = " + DENPYOU_KBN_SHIHARAI);
            if (this.form.rdoMikakutei.Checked)
            {
                // NULLの場合は未確定扱い
                sql.Append(" AND (M2.KAKUTEI_KBN IS NULL OR M2.KAKUTEI_KBN = '" + KAKUTEI_KBN_MIKAKUTEI + "') ");
            }
            if (this.form.rdoKakuteizumi.Checked)
            {
                sql.Append(" AND M2.KAKUTEI_KBN = '" + KAKUTEI_KBN_KAKUTEI + "' ");
            }
            sql.Append(" INNER JOIN M_SYS_INFO M3 ON M3.SYS_ID = '0' ");
            sql.Append(" LEFT JOIN M_KYOTEN N1 ON M1.KYOTEN_CD = N1.KYOTEN_CD ");
            sql.Append(" LEFT JOIN M_KEITAI_KBN N3 ON M1.KEITAI_KBN_CD = N3.KEITAI_KBN_CD ");
            sql.Append(" LEFT JOIN M_CONTENA_SOUSA N4 ON M1.CONTENA_SOUSA_CD = N4.CONTENA_SOUSA_CD ");
            sql.Append(" LEFT JOIN M_MANIFEST_SHURUI N5 ON M1.MANIFEST_SHURUI_CD = N5.MANIFEST_SHURUI_CD ");
            sql.Append(" LEFT JOIN M_MANIFEST_TEHAI N6 ON M1.MANIFEST_TEHAI_CD = N6.MANIFEST_TEHAI_CD ");
            sql.Append(" LEFT JOIN M_TORIHIKI_KBN N7 ON M1.URIAGE_TORIHIKI_KBN_CD = N7.TORIHIKI_KBN_CD ");
            sql.Append(" LEFT JOIN M_TORIHIKI_KBN N8 ON M1.SHIHARAI_TORIHIKI_KBN_CD = N8.TORIHIKI_KBN_CD ");
            sql.Append(" LEFT JOIN M_DENPYOU_KBN N9 ON M2.DENPYOU_KBN_CD = N9.DENPYOU_KBN_CD ");
            sql.Append(" LEFT JOIN M_YOUKI N10 ON M2.YOUKI_CD = N10.YOUKI_CD ");
            sql.Append(" LEFT JOIN M_UNIT N11 ON M2.UNIT_CD = N11.UNIT_CD ");
            sql.Append(" LEFT JOIN M_UNIT N12 ON M2.NISUGATA_UNIT_CD = N12.UNIT_CD");

        }
        /// <summary>
        /// 売上_支払検索
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSearchUriageShiharai(StringBuilder sql)
        {
            //SQL文格納StringBuilder
            sql.Append(" (SELECT ");
            sql.Append(" M1.SYSTEM_ID ");
            sql.Append(" ,M1.SEQ ");
            sql.Append(" ,NULL AS TAIRYUU_KBN ");
            sql.Append(" ,M1.KYOTEN_CD ");
            sql.Append(" ,N1.KYOTEN_NAME_RYAKU AS KYOTEN_NAME");
            sql.Append(" ,M1.UR_SH_NUMBER AS DENPYOU_NUMBER");
            sql.Append(" ,M1.DATE_NUMBER ");
            sql.Append(" ,M1.YEAR_NUMBER ");
            sql.Append(" ,M1.KAKUTEI_KBN ");
            sql.Append(" ,M1.DENPYOU_DATE ");
            sql.Append(" ,M1.URIAGE_DATE ");
            sql.Append(" ,M1.SHIHARAI_DATE ");
            sql.Append(" ,M1.TORIHIKISAKI_CD ");
            sql.Append(" ,M1.TORIHIKISAKI_NAME ");
            sql.Append(" ,M1.GYOUSHA_CD ");
            sql.Append(" ,M1.GYOUSHA_NAME ");
            sql.Append(" ,M1.GENBA_CD ");
            sql.Append(" ,M1.GENBA_NAME ");
            sql.Append(" ,M1.NIZUMI_GYOUSHA_CD ");
            sql.Append(" ,M1.NIZUMI_GYOUSHA_NAME ");
            sql.Append(" ,M1.NIZUMI_GENBA_CD ");
            sql.Append(" ,M1.NIZUMI_GENBA_NAME ");

            sql.Append(" ,M1.NIOROSHI_GYOUSHA_CD ");
            sql.Append(" ,M1.NIOROSHI_GYOUSHA_NAME ");
            sql.Append(" ,M1.NIOROSHI_GENBA_CD ");
            sql.Append(" ,M1.NIOROSHI_GENBA_NAME ");

            sql.Append(" ,M1.EIGYOU_TANTOUSHA_CD ");
            sql.Append(" ,M1.EIGYOU_TANTOUSHA_NAME ");
            sql.Append(" ,M1.NYUURYOKU_TANTOUSHA_CD ");
            sql.Append(" ,M1.NYUURYOKU_TANTOUSHA_NAME ");
            sql.Append(" ,M1.SHARYOU_CD ");
            sql.Append(" ,M1.SHARYOU_NAME ");
            sql.Append(" ,M1.SHASHU_CD ");
            sql.Append(" ,M1.SHASHU_NAME ");
            sql.Append(" ,M1.UNPAN_GYOUSHA_CD ");
            sql.Append(" ,M1.UNPAN_GYOUSHA_NAME ");
            sql.Append(" ,M1.UNTENSHA_CD ");
            sql.Append(" ,M1.UNTENSHA_NAME ");
            sql.Append(" ,M1.NINZUU_CNT ");
            sql.Append(" ,M1.KEITAI_KBN_CD ");
            sql.Append(" ,N3.KEITAI_KBN_NAME_RYAKU AS KEITAI_KBN_NAME ");
            sql.Append(" ,NULL AS DAIKAN_KBN ");
            sql.Append(" ,M1.CONTENA_SOUSA_CD ");
            sql.Append(" ,N4.CONTENA_SOUSA_NAME_RYAKU AS CONTENA_SOUSA_NAME ");
            sql.Append(" ,M1.MANIFEST_SHURUI_CD ");
            sql.Append(" ,N5.MANIFEST_SHURUI_NAME_RYAKU AS MANIFEST_SHURUI_NAME ");
            sql.Append(" ,M1.MANIFEST_TEHAI_CD ");
            sql.Append(" ,N6.MANIFEST_TEHAI_NAME_RYAKU AS MANIFEST_TEHAI_NAME ");
            sql.Append(" ,M1.DENPYOU_BIKOU ");
            sql.Append(" ,NULL AS TAIRYUU_BIKOU ");
            sql.Append(" ,M1.UKETSUKE_NUMBER ");
            sql.Append(" ,NULL AS KEIRYOU_NUMBER ");
            sql.Append(" ,M1.RECEIPT_NUMBER ");
            sql.Append(" ,NULL AS NET_TOTAL ");
            sql.Append(" ,M1.URIAGE_SHOUHIZEI_RATE ");
            sql.Append(" ,M1.URIAGE_AMOUNT_TOTAL AS URIAGE_KINGAKU_TOTAL ");
            sql.Append(" ,M1.URIAGE_TAX_SOTO ");
            sql.Append(" ,M1.URIAGE_TAX_UCHI ");
            sql.Append(" ,M1.URIAGE_TAX_SOTO_TOTAL ");
            sql.Append(" ,M1.URIAGE_TAX_UCHI_TOTAL ");
            sql.Append(" ,M1.HINMEI_URIAGE_KINGAKU_TOTAL ");
            sql.Append(" ,M1.HINMEI_URIAGE_TAX_SOTO_TOTAL ");
            sql.Append(" ,M1.HINMEI_URIAGE_TAX_UCHI_TOTAL ");
            sql.Append(" ,M1.SHIHARAI_SHOUHIZEI_RATE ");
            sql.Append(" ,M1.SHIHARAI_AMOUNT_TOTAL AS SHIHARAI_KINGAKU_TOTAL");
            sql.Append(" ,M1.SHIHARAI_TAX_SOTO ");
            sql.Append(" ,M1.SHIHARAI_TAX_UCHI ");
            sql.Append(" ,M1.SHIHARAI_TAX_SOTO_TOTAL ");
            sql.Append(" ,M1.SHIHARAI_TAX_UCHI_TOTAL ");
            sql.Append(" ,M1.HINMEI_SHIHARAI_KINGAKU_TOTAL ");
            sql.Append(" ,M1.HINMEI_SHIHARAI_TAX_SOTO_TOTAL ");
            sql.Append(" ,M1.HINMEI_SHIHARAI_TAX_UCHI_TOTAL ");
            sql.Append(" ,M1.URIAGE_ZEI_KEISAN_KBN_CD ");
            sql.Append(" ,CASE M1.URIAGE_ZEI_KEISAN_KBN_CD ");
            sql.Append(" WHEN '1' THEN '伝票毎' ");
            sql.Append(" WHEN '2' THEN '請求毎' ");
            sql.Append(" WHEN '3' THEN '明細毎' ");
            sql.Append(" ELSE '' ");
            sql.Append(" END AS URIAGE_ZEI_KEISAN_KBN_NAME ");
            sql.Append(" ,M1.URIAGE_ZEI_KBN_CD ");
            sql.Append(" ,CASE M1.URIAGE_ZEI_KBN_CD ");
            sql.Append(" WHEN '1' THEN '外税' ");
            sql.Append(" WHEN '2' THEN '内税' ");
            sql.Append(" WHEN '3' THEN '非課税' ");
            sql.Append(" ELSE '' ");
            sql.Append(" END AS URIAGE_ZEI_KBN_NAME ");
            sql.Append(" ,M1.URIAGE_TORIHIKI_KBN_CD ");
            sql.Append(" ,N7.TORIHIKI_KBN_NAME_RYAKU AS URIAGE_TORIHIKI_KBN_NAME ");
            sql.Append(" ,M1.SHIHARAI_ZEI_KEISAN_KBN_CD ");
            sql.Append(" ,CASE M1.SHIHARAI_ZEI_KEISAN_KBN_CD ");
            sql.Append(" WHEN '1' THEN '伝票毎' ");
            sql.Append(" WHEN '2' THEN '請求毎' ");
            sql.Append(" WHEN '3' THEN '明細毎' ");
            sql.Append(" ELSE '' ");
            sql.Append(" END AS SHIHARAI_ZEI_KEISAN_KBN_NAME ");
            sql.Append(" ,M1.SHIHARAI_ZEI_KBN_CD ");
            sql.Append(" ,CASE M1.SHIHARAI_ZEI_KBN_CD ");
            sql.Append(" WHEN '1' THEN '外税' ");
            sql.Append(" WHEN '2' THEN '内税' ");
            sql.Append(" WHEN '3' THEN '非課税' ");
            sql.Append(" ELSE '' ");
            sql.Append(" END AS SHIHARAI_ZEI_KBN_NAME ");
            sql.Append(" ,M1.SHIHARAI_TORIHIKI_KBN_CD ");
            sql.Append(" ,N8.TORIHIKI_KBN_NAME_RYAKU AS SHIHARAI_TORIHIKI_KBN_NAME ");

            sql.Append(" ,M1.TSUKI_CREATE_KBN ");
            sql.Append(" ,NULL AS KENSHU_DATE ");
            sql.Append(" ,NULL AS SHUKKA_NET_TOTAL ");
            sql.Append(" ,NULL AS KENSHU_NET_TOTAL ");
            sql.Append(" ,NULL AS SABUN ");
            sql.Append(" ,NULL AS SHUKKA_KINGAKU_TOTAL ");
            sql.Append(" ,NULL AS KENSHU_KINGAKU_TOTAL ");
            sql.Append(" ,NULL AS SAGAKU ");

            sql.Append(" ,M1.CREATE_USER ");
            sql.Append(" ,M1.CREATE_DATE ");
            sql.Append(" ,M1.CREATE_PC ");
            sql.Append(" ,M1.UPDATE_USER ");
            sql.Append(" ,M1.UPDATE_DATE ");
            sql.Append(" ,M1.UPDATE_PC ");
            sql.Append(" ,M1.DELETE_FLG ");
            sql.Append(" ,M1.TIME_STAMP ");

            sql.Append(" ,M3.UR_SH_KAKUTEI_USE_KBN AS JYOHOU_RIYOU_KUBUN ");

            sql.Append("  ,M2.KAKUTEI_KBN DETAIL_KAKUTEI_KBN ");
            sql.Append(" ,'売上_支払' 分類 ");
            sql.Append(" ,M2.SYSTEM_ID DETAIL_SYSTEM_ID ");
            sql.Append(" ,M2.SEQ DETAIL_SEQ ");
            sql.Append(" ,M2.DETAIL_SYSTEM_ID DETAIL_DETAIL_SYSTEM_ID ");
            sql.Append(" ,M2.UR_SH_NUMBER DETAIL_DENPYOU_NUMBER ");
            sql.Append(" ,M2.ROW_NO DETAIL_ROW_NO ");
            sql.Append(" ,M2.URIAGESHIHARAI_DATE DETAIL_URIAGESHIHARAI_DATE ");

            sql.Append(" ,NULL AS DETAIL_STACK_JYUURYOU ");
            sql.Append(" ,NULL AS DETAIL_EMPTY_JYUURYOU ");
            sql.Append(" ,NULL AS DETAIL_WARIFURI_JYUURYOU ");
            sql.Append(" ,NULL AS DETAIL_WARIFURI_PERCENT ");
            sql.Append(" ,NULL AS DETAIL_CHOUSEI_JYUURYOU ");
            sql.Append(" ,NULL AS DETAIL_CHOUSEI_PERCENT ");
            sql.Append(" ,NULL AS DETAIL_YOUKI_CD ");
            sql.Append(" ,NULL AS DETAIL_YOUKI_NAME ");
            sql.Append(" ,NULL AS DETAIL_YOUKI_SUURYOU ");
            sql.Append(" ,NULL AS DETAIL_YOUKI_JYUURYOU ");


            sql.Append(" ,M2.DENPYOU_KBN_CD DETAIL_DENPYOU_KBN_CD ");
            sql.Append(" ,N9.DENPYOU_KBN_NAME_RYAKU AS DETAIL_DENPYOU_KBN_NAME ");
            sql.Append(" ,M2.HINMEI_CD DETAIL_HINMEI_CD ");
            sql.Append(" ,M2.HINMEI_NAME DETAIL_HINMEI_NAME ");

            sql.Append(" ,NULL AS DETAIL_NET_JYUURYOU ");

            sql.Append(" ,M2.SUURYOU DETAIL_SUURYOU ");
            sql.Append(" ,M2.UNIT_CD DETAIL_UNIT_CD ");
            sql.Append(" ,N11.UNIT_NAME_RYAKU AS DETAIL_UNIT_NAME ");
            sql.Append(" ,M2.TANKA DETAIL_TANKA  ");
            sql.Append(" ,M2.KINGAKU DETAIL_KINGAKU  ");
            sql.Append(" ,M2.TAX_SOTO DETAIL_TAX_SOTO ");
            sql.Append(" ,M2.TAX_UCHI DETAIL_TAX_UCHI ");
            sql.Append(" ,M2.HINMEI_ZEI_KBN_CD DETAIL_HINMEI_ZEI_KBN_CD ");
            sql.Append(" ,CASE M2.HINMEI_ZEI_KBN_CD ");
            sql.Append(" WHEN '1' THEN '外税' ");
            sql.Append(" WHEN '2' THEN '内税' ");
            sql.Append(" WHEN '3' THEN '非課税' ");
            sql.Append(" ELSE '' ");
            sql.Append(" END AS DETAIL_HINMEI_ZEI_KBN_NAME ");
            sql.Append(" ,M2.HINMEI_KINGAKU DETAIL_HINMEI_KINGAKU ");
            sql.Append(" ,M2.HINMEI_TAX_SOTO DETAIL_HINMEI_TAX_SOTO ");
            sql.Append(" ,M2.HINMEI_TAX_UCHI DETAIL_HINMEI_TAX_UCHI ");
            sql.Append(" ,M2.MEISAI_BIKOU DETAIL_MEISAI_BIKOU ");
            sql.Append(" ,M2.NISUGATA_SUURYOU DETAIL_NISUGATA_SUURYOU ");
            sql.Append(" ,M2.NISUGATA_UNIT_CD DETAIL_NISUGATA_UNIT_CD ");
            sql.Append(" ,N12.UNIT_NAME_RYAKU AS DETAIL_NISUGATA_UNIT_NAME");
            sql.Append(" ,M2.TIME_STAMP DETAIL_TIME_STAMP ");
            sql.Append(" FROM T_UR_SH_ENTRY M1 ");
            sql.Append(" INNER JOIN T_UR_SH_DETAIL M2 ON ");
            sql.Append(" M1.SYSTEM_ID = M2.SYSTEM_ID ");
            sql.Append(" AND M1.SEQ = M2.SEQ ");
            //伝票区分＝支払
            sql.Append(" AND M2.DENPYOU_KBN_CD = " + DENPYOU_KBN_SHIHARAI);
            if (this.form.rdoMikakutei.Checked)
            {
                // NULLの場合は未確定扱い
                sql.Append(" AND (M2.KAKUTEI_KBN IS NULL OR M2.KAKUTEI_KBN = '" + KAKUTEI_KBN_MIKAKUTEI + "') ");
            }
            if (this.form.rdoKakuteizumi.Checked)
            {
                sql.Append(" AND M2.KAKUTEI_KBN = '" + KAKUTEI_KBN_KAKUTEI + "' ");
            }
            sql.Append(" INNER JOIN M_SYS_INFO M3 ON M3.SYS_ID = '0' ");
            sql.Append(" LEFT JOIN M_KYOTEN N1 ON M1.KYOTEN_CD = N1.KYOTEN_CD ");
            sql.Append(" LEFT JOIN M_KEITAI_KBN N3 ON M1.KEITAI_KBN_CD = N3.KEITAI_KBN_CD ");
            sql.Append(" LEFT JOIN M_CONTENA_SOUSA N4 ON M1.CONTENA_SOUSA_CD = N4.CONTENA_SOUSA_CD ");
            sql.Append(" LEFT JOIN M_MANIFEST_SHURUI N5 ON M1.MANIFEST_SHURUI_CD = N5.MANIFEST_SHURUI_CD ");
            sql.Append(" LEFT JOIN M_MANIFEST_TEHAI N6 ON M1.MANIFEST_TEHAI_CD = N6.MANIFEST_TEHAI_CD ");
            sql.Append(" LEFT JOIN M_TORIHIKI_KBN N7 ON M1.URIAGE_TORIHIKI_KBN_CD = N7.TORIHIKI_KBN_CD ");
            sql.Append(" LEFT JOIN M_TORIHIKI_KBN N8 ON M1.SHIHARAI_TORIHIKI_KBN_CD = N8.TORIHIKI_KBN_CD ");
            sql.Append(" LEFT JOIN M_DENPYOU_KBN N9 ON M2.DENPYOU_KBN_CD = N9.DENPYOU_KBN_CD ");
            //sql.Append(" LEFT JOIN M_YOUKI N10 ON M2.YOUKI_CD = N10.YOUKI_CD ");
            sql.Append(" LEFT JOIN M_UNIT N11 ON M2.UNIT_CD = N11.UNIT_CD ");
            sql.Append(" LEFT JOIN M_UNIT N12 ON M2.NISUGATA_UNIT_CD = N12.UNIT_CD");
        }

        #endregion 検索関連

        /// <summary>
        /// 採番処理
        /// </summary>
        private int SaiBan(string tablename, string systemid)
        {
            //SQL文格納StringBuilder
            var sql = new StringBuilder();

            sql.Append(" SELECT ");
            sql.Append(" MAX(SEQ) MAX");
            sql.Append(" FROM ");
            sql.Append(" " + tablename);
            sql.Append(" WHERE ");
            sql.Append(" SYSTEM_ID = " + systemid);
            DataTable searchSaiban = new DataTable();
            searchSaiban = t_ichirandao.getdateforstringsql(sql.ToString());
            if (searchSaiban.Rows.Count > 0)
            {
                return Convert.ToInt32(searchSaiban.Rows[0]["MAX"].ToString());
            }
            else
            {
                return 0;
            }
        }

        #region 明細データ取得

        /// <summary>
        /// 受入データ取得
        /// </summary>
        private void GetUkeireData(string kubun, DataTable ukeireDt)
        {
            if (kubun == "TOUROKU")
            {
                List<T_UKEIRE_DETAIL> ukeireMsList = new List<T_UKEIRE_DETAIL>();
                List<T_UKEIRE_ENTRY> ukeireEntryListAdd = new List<T_UKEIRE_ENTRY>();
                List<T_UKEIRE_ENTRY> ukeireEntryListUpdata = new List<T_UKEIRE_ENTRY>();

                string systemidukeire = "";
                string sequkeire = "";
                int row = 1;
                int saiban = 0;
                foreach (DataRow dgvRow in ukeireDt.Rows)
                {
                    if (dgvRow["明細・確定区分"].ToString().Equals("True") && dgvRow["情報確定利用区分"].ToString().Equals(KAKUTEI_USE_KBN_USE)
                       && !dgvRow["明細・確定区分"].ToString().Equals(dgvRow["比較確定区分"].ToString()))
                    {
                        DataTable dbDetail = new DataTable();
                        DataTable dbEntry = new DataTable();
                        T_UKEIRE_DETAIL ukeireMs = new T_UKEIRE_DETAIL();
                        T_UKEIRE_ENTRY ukeireEntryAdd = new T_UKEIRE_ENTRY();
                        T_UKEIRE_ENTRY ukeireEntryUpdata = new T_UKEIRE_ENTRY();
                        // WHOカラム設定共通ロジック呼び出し用
                        var dataBind_ukeireMs = new DataBinderLogic<T_UKEIRE_DETAIL>(ukeireMs);
                        var dataBind_ukeireEntryAdd = new DataBinderLogic<T_UKEIRE_ENTRY>(ukeireEntryAdd);
                        var dataBind_ukeireEntryUpdata = new DataBinderLogic<T_UKEIRE_ENTRY>(ukeireEntryUpdata);

                        if (!dgvRow["システムID"].ToString().Equals(systemidukeire) || !dgvRow["枝番"].ToString().Equals(sequkeire))
                        {
                            // 前の行とシステムIDが異なるか、または枝番が異なる場合

                            // 明細データは伝票区分関係なく全部取得する（SEQを更新するため）
                            dbDetail = SearchUkeireDetail(dgvRow["システムID"].ToString(), dgvRow["枝番"].ToString());
                            dbEntry = SearchUkeireEntry(dgvRow["システムID"].ToString(), dgvRow["枝番"].ToString());

                            if (!dgvRow["システムID"].ToString().Equals(systemidukeire))
                            {
                                // 前の行とシステムIDが異なる場合（受入入力が別）

                                saiban = SaiBan("T_UKEIRE_ENTRY", dgvRow["システムID"].ToString()) + 1;
                            }
                            else if (dgvRow["システムID"].ToString().Equals(systemidukeire) && !dgvRow["枝番"].ToString().Equals(sequkeire))
                            {
                                // 前の行とシステムIDは同じで枝番だけ異なる場合

                                saiban = saiban + 1;
                            }

                            // 受入入力データ（1件）
                            foreach (DataRow dr in dbEntry.Rows)
                            {
                                // 受入入力論理削除用データ
                                ukeireEntryUpdata = new T_UKEIRE_ENTRY();
                                if (!String.IsNullOrEmpty(dr["SYSTEM_ID"].ToString()))
                                {
                                    ukeireEntryUpdata.SYSTEM_ID = SqlInt64.Parse(dr["SYSTEM_ID"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SEQ"].ToString()))
                                {
                                    ukeireEntryUpdata.SEQ = SqlInt32.Parse(dr["SEQ"].ToString());
                                }
                                //*************
                                if (!String.IsNullOrEmpty(dr["TAIRYUU_KBN"].ToString()))
                                {
                                    ukeireEntryUpdata.TAIRYUU_KBN = SqlBoolean.Parse(dr["TAIRYUU_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KYOTEN_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.KYOTEN_CD = SqlInt16.Parse(dr["KYOTEN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["UKEIRE_NUMBER"].ToString()))
                                {
                                    ukeireEntryUpdata.UKEIRE_NUMBER = SqlInt64.Parse(dr["UKEIRE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DATE_NUMBER"].ToString()))
                                {
                                    ukeireEntryUpdata.DATE_NUMBER = SqlInt32.Parse(dr["DATE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["YEAR_NUMBER"].ToString()))
                                {
                                    ukeireEntryUpdata.YEAR_NUMBER = SqlInt32.Parse(dr["YEAR_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                {
                                    ukeireEntryUpdata.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DENPYOU_DATE"].ToString()))
                                {
                                    ukeireEntryUpdata.DENPYOU_DATE = SqlDateTime.Parse(dr["DENPYOU_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_DATE"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_DATE = SqlDateTime.Parse(dr["URIAGE_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_DATE"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_DATE = SqlDateTime.Parse(dr["SHIHARAI_DATE"].ToString());
                                }
                                ukeireEntryUpdata.TORIHIKISAKI_CD = dr["TORIHIKISAKI_CD"].ToString();
                                ukeireEntryUpdata.TORIHIKISAKI_NAME = dr["TORIHIKISAKI_NAME"].ToString();
                                ukeireEntryUpdata.GYOUSHA_CD = dr["GYOUSHA_CD"].ToString();
                                ukeireEntryUpdata.GYOUSHA_NAME = dr["GYOUSHA_NAME"].ToString();
                                ukeireEntryUpdata.GENBA_CD = dr["GENBA_CD"].ToString();
                                ukeireEntryUpdata.GENBA_NAME = dr["GENBA_NAME"].ToString();
                                ukeireEntryUpdata.NIOROSHI_GYOUSHA_CD = dr["NIOROSHI_GYOUSHA_CD"].ToString();
                                ukeireEntryUpdata.NIOROSHI_GYOUSHA_NAME = dr["NIOROSHI_GYOUSHA_NAME"].ToString();
                                ukeireEntryUpdata.NIOROSHI_GENBA_CD = dr["NIOROSHI_GENBA_CD"].ToString();
                                ukeireEntryUpdata.NIOROSHI_GENBA_NAME = dr["NIOROSHI_GENBA_NAME"].ToString();
                                ukeireEntryUpdata.EIGYOU_TANTOUSHA_CD = dr["EIGYOU_TANTOUSHA_CD"].ToString();
                                ukeireEntryUpdata.EIGYOU_TANTOUSHA_NAME = dr["EIGYOU_TANTOUSHA_NAME"].ToString();
                                ukeireEntryUpdata.NYUURYOKU_TANTOUSHA_CD = dr["NYUURYOKU_TANTOUSHA_CD"].ToString();
                                ukeireEntryUpdata.NYUURYOKU_TANTOUSHA_NAME = dr["NYUURYOKU_TANTOUSHA_NAME"].ToString();
                                ukeireEntryUpdata.SHARYOU_CD = dr["SHARYOU_CD"].ToString();
                                ukeireEntryUpdata.SHARYOU_NAME = dr["SHARYOU_NAME"].ToString();
                                ukeireEntryUpdata.SHASHU_CD = dr["SHASHU_CD"].ToString();
                                ukeireEntryUpdata.SHASHU_NAME = dr["SHASHU_NAME"].ToString();
                                ukeireEntryUpdata.UNPAN_GYOUSHA_CD = dr["UNPAN_GYOUSHA_CD"].ToString();
                                ukeireEntryUpdata.UNPAN_GYOUSHA_NAME = dr["UNPAN_GYOUSHA_NAME"].ToString();
                                ukeireEntryUpdata.UNTENSHA_CD = dr["UNTENSHA_CD"].ToString();
                                ukeireEntryUpdata.UNTENSHA_NAME = dr["UNTENSHA_NAME"].ToString();
                                if (!String.IsNullOrEmpty(dr["NINZUU_CNT"].ToString()))
                                {
                                    ukeireEntryUpdata.NINZUU_CNT = SqlInt16.Parse(dr["NINZUU_CNT"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEITAI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.KEITAI_KBN_CD = SqlInt16.Parse(dr["KEITAI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DAIKAN_KBN"].ToString()))
                                {
                                    ukeireEntryUpdata.DAIKAN_KBN = SqlInt16.Parse(dr["DAIKAN_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CONTENA_SOUSA_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.CONTENA_SOUSA_CD = SqlInt16.Parse(dr["CONTENA_SOUSA_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_SHURUI_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.MANIFEST_SHURUI_CD = SqlInt16.Parse(dr["MANIFEST_SHURUI_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_TEHAI_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.MANIFEST_TEHAI_CD = SqlInt16.Parse(dr["MANIFEST_TEHAI_CD"].ToString());
                                }
                                ukeireEntryUpdata.DENPYOU_BIKOU = dr["DENPYOU_BIKOU"].ToString();
                                ukeireEntryUpdata.TAIRYUU_BIKOU = dr["TAIRYUU_BIKOU"].ToString();
                                if (!String.IsNullOrEmpty(dr["UKETSUKE_NUMBER"].ToString()))
                                {
                                    ukeireEntryUpdata.UKETSUKE_NUMBER = SqlInt64.Parse(dr["UKETSUKE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEIRYOU_NUMBER"].ToString()))
                                {
                                    ukeireEntryUpdata.KEIRYOU_NUMBER = SqlInt64.Parse(dr["KEIRYOU_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["RECEIPT_NUMBER"].ToString()))
                                {
                                    ukeireEntryUpdata.RECEIPT_NUMBER = SqlInt32.Parse(dr["RECEIPT_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["NET_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.NET_TOTAL = SqlDecimal.Parse(dr["NET_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_SHOUHIZEI_RATE"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["URIAGE_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_KINGAKU_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_KINGAKU_TOTAL = SqlDecimal.Parse(dr["URIAGE_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_TAX_SOTO = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_TAX_UCHI = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.HINMEI_URIAGE_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.HINMEI_URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.HINMEI_URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_KINGAKU_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_KINGAKU_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_TAX_SOTO = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_TAX_UCHI = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.HINMEI_SHIHARAI_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_ZEI_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["URIAGE_TORIHIKI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_ZEI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString());
                                }
                                ukeireEntryUpdata.TIME_STAMP = (byte[])(dr["TIME_STAMP"]);
                                //*************
                                
                                // WHOカラム設定
                                dataBind_ukeireEntryUpdata.SetSystemProperty(ukeireEntryUpdata, false);
                                ukeireEntryUpdata.DELETE_FLG = SqlBoolean.True;

                                // 受入入力追加用データ
                                ukeireEntryAdd = new T_UKEIRE_ENTRY();
                                if (!String.IsNullOrEmpty(dr["SYSTEM_ID"].ToString()))
                                {
                                    ukeireEntryAdd.SYSTEM_ID = SqlInt64.Parse(dr["SYSTEM_ID"].ToString());
                                }
                                ukeireEntryAdd.SEQ = SqlInt32.Parse(saiban.ToString());
                                if (!String.IsNullOrEmpty(dr["TAIRYUU_KBN"].ToString()))
                                {
                                    ukeireEntryAdd.TAIRYUU_KBN = SqlBoolean.Parse(dr["TAIRYUU_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KYOTEN_CD"].ToString()))
                                {
                                    ukeireEntryAdd.KYOTEN_CD = SqlInt16.Parse(dr["KYOTEN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["UKEIRE_NUMBER"].ToString()))
                                {
                                    ukeireEntryAdd.UKEIRE_NUMBER = SqlInt64.Parse(dr["UKEIRE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DATE_NUMBER"].ToString()))
                                {
                                    ukeireEntryAdd.DATE_NUMBER = SqlInt32.Parse(dr["DATE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["YEAR_NUMBER"].ToString()))
                                {
                                    ukeireEntryAdd.YEAR_NUMBER = SqlInt32.Parse(dr["YEAR_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                {
                                    ukeireEntryAdd.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DENPYOU_DATE"].ToString()))
                                {
                                    ukeireEntryAdd.DENPYOU_DATE = SqlDateTime.Parse(dr["DENPYOU_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_DATE"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_DATE = SqlDateTime.Parse(dr["URIAGE_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_DATE"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_DATE = SqlDateTime.Parse(dr["SHIHARAI_DATE"].ToString());
                                }
                                ukeireEntryAdd.TORIHIKISAKI_CD = dr["TORIHIKISAKI_CD"].ToString();
                                ukeireEntryAdd.TORIHIKISAKI_NAME = dr["TORIHIKISAKI_NAME"].ToString();
                                ukeireEntryAdd.GYOUSHA_CD = dr["GYOUSHA_CD"].ToString();
                                ukeireEntryAdd.GYOUSHA_NAME = dr["GYOUSHA_NAME"].ToString();
                                ukeireEntryAdd.GENBA_CD = dr["GENBA_CD"].ToString();
                                ukeireEntryAdd.GENBA_NAME = dr["GENBA_NAME"].ToString();
                                ukeireEntryAdd.NIOROSHI_GYOUSHA_CD = dr["NIOROSHI_GYOUSHA_CD"].ToString();
                                ukeireEntryAdd.NIOROSHI_GYOUSHA_NAME = dr["NIOROSHI_GYOUSHA_NAME"].ToString();
                                ukeireEntryAdd.NIOROSHI_GENBA_CD = dr["NIOROSHI_GENBA_CD"].ToString();
                                ukeireEntryAdd.NIOROSHI_GENBA_NAME = dr["NIOROSHI_GENBA_NAME"].ToString();
                                ukeireEntryAdd.EIGYOU_TANTOUSHA_CD = dr["EIGYOU_TANTOUSHA_CD"].ToString();
                                ukeireEntryAdd.EIGYOU_TANTOUSHA_NAME = dr["EIGYOU_TANTOUSHA_NAME"].ToString();
                                ukeireEntryAdd.NYUURYOKU_TANTOUSHA_CD = dr["NYUURYOKU_TANTOUSHA_CD"].ToString();
                                ukeireEntryAdd.NYUURYOKU_TANTOUSHA_NAME = dr["NYUURYOKU_TANTOUSHA_NAME"].ToString();
                                ukeireEntryAdd.SHARYOU_CD = dr["SHARYOU_CD"].ToString();
                                ukeireEntryAdd.SHARYOU_NAME = dr["SHARYOU_NAME"].ToString();
                                ukeireEntryAdd.SHASHU_CD = dr["SHASHU_CD"].ToString();
                                ukeireEntryAdd.SHASHU_NAME = dr["SHASHU_NAME"].ToString();
                                ukeireEntryAdd.UNPAN_GYOUSHA_CD = dr["UNPAN_GYOUSHA_CD"].ToString();
                                ukeireEntryAdd.UNPAN_GYOUSHA_NAME = dr["UNPAN_GYOUSHA_NAME"].ToString();
                                ukeireEntryAdd.UNTENSHA_CD = dr["UNTENSHA_CD"].ToString();
                                ukeireEntryAdd.UNTENSHA_NAME = dr["UNTENSHA_NAME"].ToString();
                                if (!String.IsNullOrEmpty(dr["NINZUU_CNT"].ToString()))
                                {
                                    ukeireEntryAdd.NINZUU_CNT = SqlInt16.Parse(dr["NINZUU_CNT"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEITAI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryAdd.KEITAI_KBN_CD = SqlInt16.Parse(dr["KEITAI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DAIKAN_KBN"].ToString()))
                                {
                                    ukeireEntryAdd.DAIKAN_KBN = SqlInt16.Parse(dr["DAIKAN_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CONTENA_SOUSA_CD"].ToString()))
                                {
                                    ukeireEntryAdd.CONTENA_SOUSA_CD = SqlInt16.Parse(dr["CONTENA_SOUSA_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_SHURUI_CD"].ToString()))
                                {
                                    ukeireEntryAdd.MANIFEST_SHURUI_CD = SqlInt16.Parse(dr["MANIFEST_SHURUI_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_TEHAI_CD"].ToString()))
                                {
                                    ukeireEntryAdd.MANIFEST_TEHAI_CD = SqlInt16.Parse(dr["MANIFEST_TEHAI_CD"].ToString());
                                }
                                ukeireEntryAdd.DENPYOU_BIKOU = dr["DENPYOU_BIKOU"].ToString();
                                ukeireEntryAdd.TAIRYUU_BIKOU = dr["TAIRYUU_BIKOU"].ToString();
                                if (!String.IsNullOrEmpty(dr["UKETSUKE_NUMBER"].ToString()))
                                {
                                    ukeireEntryAdd.UKETSUKE_NUMBER = SqlInt64.Parse(dr["UKETSUKE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEIRYOU_NUMBER"].ToString()))
                                {
                                    ukeireEntryAdd.KEIRYOU_NUMBER = SqlInt64.Parse(dr["KEIRYOU_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["RECEIPT_NUMBER"].ToString()))
                                {
                                    ukeireEntryAdd.RECEIPT_NUMBER = SqlInt32.Parse(dr["RECEIPT_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["NET_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.NET_TOTAL = SqlDecimal.Parse(dr["NET_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_SHOUHIZEI_RATE"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["URIAGE_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_KINGAKU_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_KINGAKU_TOTAL = SqlDecimal.Parse(dr["URIAGE_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_TAX_SOTO = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_TAX_UCHI = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.HINMEI_URIAGE_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.HINMEI_URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.HINMEI_URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_KINGAKU_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_KINGAKU_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_TAX_SOTO = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_TAX_UCHI = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.HINMEI_SHIHARAI_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_ZEI_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["URIAGE_TORIHIKI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_ZEI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString());
                                }
                                //ukeireEntryAdd.TIME_STAMP = (byte[])(dr["TIME_STAMP"]);
                                // WHOカラム設定
                                dataBind_ukeireEntryAdd.SetSystemProperty(ukeireEntryAdd, false);

                                if (!String.IsNullOrEmpty(dr["DELETE_FLG"].ToString()))
                                {
                                    ukeireEntryAdd.DELETE_FLG = SqlBoolean.Parse(dr["DELETE_FLG"].ToString());
                                }
                                ukeireEntryListAdd.Add(ukeireEntryAdd);
                                ukeireEntryListUpdata.Add(ukeireEntryUpdata);
                            }
                            // 受入明細（複数件）
                            foreach (DataRow dr in dbDetail.Rows)
                            {
                                ukeireMs = new T_UKEIRE_DETAIL();
                                if (!String.IsNullOrEmpty(dr["SYSTEM_ID"].ToString()))
                                {
                                    ukeireMs.SYSTEM_ID = SqlInt64.Parse(dr["SYSTEM_ID"].ToString());
                                }

                                ukeireMs.SEQ = SqlInt32.Parse(saiban.ToString());

                                if (!String.IsNullOrEmpty(dr["DETAIL_SYSTEM_ID"].ToString()))
                                {
                                    ukeireMs.DETAIL_SYSTEM_ID = SqlInt64.Parse(dr["DETAIL_SYSTEM_ID"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["UKEIRE_NUMBER"].ToString()))
                                {
                                    ukeireMs.UKEIRE_NUMBER = SqlInt64.Parse(dr["UKEIRE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["ROW_NO"].ToString()))
                                {
                                    ukeireMs.ROW_NO = SqlInt16.Parse(dr["ROW_NO"].ToString());
                                }
                                // 伝票単位の場合、全明細を確定にする
                                if (this.msysinfo.SYS_KAKUTEI__TANNI_KBN.Equals(SqlInt16.Parse("1")))
                                {

                                    // 売上伝票の明細データは確定しない

                                    if (DENPYOU_KBN_SHIHARAI.Equals(dr["DENPYOU_KBN_CD"].ToString()))
                                    {
                                        // 伝票区分が支払の場合
                                        ukeireMs.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_KAKUTEI);
                                    }
                                    else
                                    {
                                        // 伝票区分が売上の場合
                                        if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                        {
                                            // 値を保持する
                                            ukeireMs.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    // 明細単位の場合は後でセットする
                                    if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                    {
                                        ukeireMs.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                    }
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGESHIHARAI_DATE"].ToString()))
                                {
                                    ukeireMs.URIAGESHIHARAI_DATE = SqlDateTime.Parse(dr["URIAGESHIHARAI_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["STACK_JYUURYOU"].ToString()))
                                {
                                    ukeireMs.STACK_JYUURYOU = SqlDecimal.Parse(dr["STACK_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["EMPTY_JYUURYOU"].ToString()))
                                {
                                    ukeireMs.EMPTY_JYUURYOU = SqlDecimal.Parse(dr["EMPTY_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["WARIFURI_JYUURYOU"].ToString()))
                                {
                                    ukeireMs.WARIFURI_JYUURYOU = SqlDecimal.Parse(dr["WARIFURI_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["WARIFURI_PERCENT"].ToString()))
                                {
                                    ukeireMs.WARIFURI_PERCENT = SqlDecimal.Parse(dr["WARIFURI_PERCENT"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CHOUSEI_JYUURYOU"].ToString()))
                                {
                                    ukeireMs.CHOUSEI_JYUURYOU = SqlInt64.Parse(dr["CHOUSEI_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CHOUSEI_PERCENT"].ToString()))
                                {
                                    ukeireMs.CHOUSEI_PERCENT = SqlDecimal.Parse(dr["CHOUSEI_PERCENT"].ToString());
                                }
                                ukeireMs.YOUKI_CD = dr["YOUKI_CD"].ToString();
                                if (!String.IsNullOrEmpty(dr["YOUKI_SUURYOU"].ToString()))
                                {
                                    ukeireMs.YOUKI_SUURYOU = SqlDecimal.Parse(dr["YOUKI_SUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["YOUKI_JYUURYOU"].ToString()))
                                {
                                    ukeireMs.YOUKI_JYUURYOU = SqlDecimal.Parse(dr["YOUKI_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DENPYOU_KBN_CD"].ToString()))
                                {
                                    ukeireMs.DENPYOU_KBN_CD = SqlInt16.Parse(dr["DENPYOU_KBN_CD"].ToString());
                                }
                                ukeireMs.HINMEI_CD = dr["HINMEI_CD"].ToString();
                                ukeireMs.HINMEI_NAME = dr["HINMEI_NAME"].ToString();
                                if (!String.IsNullOrEmpty(dr["NET_JYUURYOU"].ToString()))
                                {
                                    ukeireMs.NET_JYUURYOU = SqlInt64.Parse(dr["NET_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SUURYOU"].ToString()))
                                {
                                    ukeireMs.SUURYOU = SqlDecimal.Parse(dr["SUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["UNIT_CD"].ToString()))
                                {
                                    ukeireMs.UNIT_CD = SqlInt16.Parse(dr["UNIT_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["TANKA"].ToString()))
                                {
                                    ukeireMs.TANKA = SqlDecimal.Parse(dr["TANKA"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KINGAKU"].ToString()))
                                {
                                    ukeireMs.KINGAKU = SqlDecimal.Parse(dr["KINGAKU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["TAX_SOTO"].ToString()))
                                {
                                    ukeireMs.TAX_SOTO = SqlDecimal.Parse(dr["TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["TAX_UCHI"].ToString()))
                                {
                                    ukeireMs.TAX_UCHI = SqlDecimal.Parse(dr["TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_ZEI_KBN_CD"].ToString()))
                                {
                                    ukeireMs.HINMEI_ZEI_KBN_CD = SqlInt16.Parse(dr["HINMEI_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_KINGAKU"].ToString()))
                                {
                                    ukeireMs.HINMEI_KINGAKU = SqlDecimal.Parse(dr["HINMEI_KINGAKU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_TAX_SOTO"].ToString()))
                                {
                                    ukeireMs.HINMEI_TAX_SOTO = SqlDecimal.Parse(dr["HINMEI_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_TAX_UCHI"].ToString()))
                                {
                                    ukeireMs.HINMEI_TAX_UCHI = SqlDecimal.Parse(dr["HINMEI_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MEISAI_BIKOU"].ToString()))
                                {
                                    ukeireMs.MEISAI_BIKOU = dr["MEISAI_BIKOU"].ToString();
                                }
                                if (!String.IsNullOrEmpty(dr["NISUGATA_SUURYOU"].ToString()))
                                {
                                    ukeireMs.NISUGATA_SUURYOU = SqlDecimal.Parse(dr["NISUGATA_SUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["NISUGATA_SUURYOU"].ToString()))
                                {
                                    ukeireMs.NISUGATA_UNIT_CD = SqlInt16.Parse(dr["NISUGATA_UNIT_CD"].ToString());
                                }
                                //ukeireMs.TIME_STAMP = (byte[])(dr["TIME_STAMP"]);
                                // WHOカラム設定
                                dataBind_ukeireMs.SetSystemProperty(ukeireMs, false);

                                ukeireMsList.Add(ukeireMs);
                            }
                        }
                        foreach (T_UKEIRE_DETAIL item in ukeireMsList)
                        {
                            // 1つ前の枝番で同じ明細・システムIDの明細がグリッド上にあったら確定区分を確定

                            if (item.SYSTEM_ID.ToString().Equals(dgvRow["システムID"].ToString())
                                && (item.SEQ - 1).ToString().Equals(dgvRow["枝番"].ToString())
                                && item.DETAIL_SYSTEM_ID.ToString().Equals(dgvRow["明細・システムID"].ToString()))
                            {
                                item.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_KAKUTEI);
                                //item.SEQ = SqlInt16.Parse(saiban.ToString());
                            }

                        }

                        row++;
                        systemidukeire = dgvRow["システムID"].ToString();
                        sequkeire = dgvRow["枝番"].ToString();
                    }
                }

                // 明細の確定区分がすべて確定済になったら入力の確定区分も確定済とする
                string systemid = "";
                string seq = "";
                bool flag = false;
                int j = 1;
                foreach (T_UKEIRE_DETAIL item in ukeireMsList)
                {
                    if (j == 1)
                    {
                        systemid = item.SYSTEM_ID.ToString();
                        seq = item.SEQ.ToString();
                    }

                    if (item.SYSTEM_ID.ToString().Equals(systemid) && item.SEQ.ToString().Equals(seq))
                    {
                        // 未確定ならtrue(NULLの場合があるので確定以外で判定)
                        if (!item.KAKUTEI_KBN.ToString().Equals(KAKUTEI_KBN_KAKUTEI))
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        foreach (T_UKEIRE_ENTRY itemEntry in ukeireEntryListAdd)
                        {
                            if (SqlInt64.Parse(systemid) == itemEntry.SYSTEM_ID && SqlInt64.Parse(seq) == itemEntry.SEQ)
                            {
                                // trueなら未確定
                                if (flag)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_MIKAKUTEI);
                                }
                                else
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_KAKUTEI);
                                }
                            }

                        }
                        flag = false;
                    }
                    if (j == ukeireMsList.Count)
                    {
                        // 未確定ならtrue(NULLの場合があるので確定以外で判定)
                        if (!item.KAKUTEI_KBN.ToString().Equals(KAKUTEI_KBN_KAKUTEI))
                        {
                            flag = true;
                        }

                        foreach (T_UKEIRE_ENTRY itemEntry in ukeireEntryListAdd)
                        {
                            if (item.SYSTEM_ID == itemEntry.SYSTEM_ID && item.SEQ == itemEntry.SEQ)
                            {
                                // trueなら未確定
                                if (flag)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_MIKAKUTEI);
                                }
                                else
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_KAKUTEI);
                                }
                            }

                        }
                        flag = false;

                    }
                    systemid = item.SYSTEM_ID.ToString();
                    seq = item.SEQ.ToString();
                    j++;

                }
                this.mukeiremslist = ukeireMsList;
                this.mukeireentrylistadd = ukeireEntryListAdd;
                this.mukeireentrylistupdata = ukeireEntryListUpdata;
            }
            else if (kubun == "KAIJYO")
            {
                List<T_UKEIRE_DETAIL> ukeireMsList = new List<T_UKEIRE_DETAIL>();
                List<T_UKEIRE_ENTRY> ukeireEntryListAdd = new List<T_UKEIRE_ENTRY>();
                List<T_UKEIRE_ENTRY> ukeireEntryListUpdata = new List<T_UKEIRE_ENTRY>();

                string systemidukeire = "";
                string sequkeire = "";
                int row = 1;
                int saiban = 0;
                foreach (DataRow dgvRow in ukeireDt.Rows)
                {
                    if (dgvRow["明細・確定区分"].ToString().Equals("False") && dgvRow["情報確定利用区分"].ToString().Equals(KAKUTEI_USE_KBN_USE)
                        && !dgvRow["明細・確定区分"].ToString().Equals(dgvRow["比較確定区分"].ToString()))
                    {
                        DataTable dbDetail = new DataTable();
                        DataTable dbEntry = new DataTable();
                        T_UKEIRE_DETAIL ukeireMs = new T_UKEIRE_DETAIL();
                        T_UKEIRE_ENTRY ukeireEntryAdd = new T_UKEIRE_ENTRY();
                        T_UKEIRE_ENTRY ukeireEntryUpdata = new T_UKEIRE_ENTRY();
                        // WHOカラム設定共通ロジック呼び出し用
                        var dataBind_ukeireMs = new DataBinderLogic<T_UKEIRE_DETAIL>(ukeireMs);
                        var dataBind_ukeireEntryAdd = new DataBinderLogic<T_UKEIRE_ENTRY>(ukeireEntryAdd);
                        var dataBind_ukeireEntryUpdata = new DataBinderLogic<T_UKEIRE_ENTRY>(ukeireEntryUpdata);

                        if (!dgvRow["システムID"].ToString().Equals(systemidukeire) || !dgvRow["枝番"].ToString().Equals(sequkeire))
                        {
                            dbDetail = SearchUkeireDetail(dgvRow["システムID"].ToString(), dgvRow["枝番"].ToString());
                            dbEntry = SearchUkeireEntry(dgvRow["システムID"].ToString(), dgvRow["枝番"].ToString());

                            if (!dgvRow["システムID"].ToString().Equals(systemidukeire))
                            {
                                saiban = SaiBan("T_UKEIRE_ENTRY", dgvRow["システムID"].ToString()) + 1;
                            }
                            else if (dgvRow["システムID"].ToString().Equals(systemidukeire) && !dgvRow["枝番"].ToString().Equals(sequkeire))
                            {
                                saiban = saiban + 1;
                            }

                            foreach (DataRow dr in dbEntry.Rows)
                            {
                                ukeireEntryUpdata = new T_UKEIRE_ENTRY();
                                if (!String.IsNullOrEmpty(dr["SYSTEM_ID"].ToString()))
                                {
                                    ukeireEntryUpdata.SYSTEM_ID = SqlInt64.Parse(dr["SYSTEM_ID"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SEQ"].ToString()))
                                {
                                    ukeireEntryUpdata.SEQ = SqlInt32.Parse(dr["SEQ"].ToString());
                                }
                                //********
                                if (!String.IsNullOrEmpty(dr["TAIRYUU_KBN"].ToString()))
                                {
                                    ukeireEntryUpdata.TAIRYUU_KBN = SqlBoolean.Parse(dr["TAIRYUU_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KYOTEN_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.KYOTEN_CD = SqlInt16.Parse(dr["KYOTEN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["UKEIRE_NUMBER"].ToString()))
                                {
                                    ukeireEntryUpdata.UKEIRE_NUMBER = SqlInt64.Parse(dr["UKEIRE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DATE_NUMBER"].ToString()))
                                {
                                    ukeireEntryUpdata.DATE_NUMBER = SqlInt32.Parse(dr["DATE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["YEAR_NUMBER"].ToString()))
                                {
                                    ukeireEntryUpdata.YEAR_NUMBER = SqlInt32.Parse(dr["YEAR_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                {
                                    ukeireEntryUpdata.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DENPYOU_DATE"].ToString()))
                                {
                                    ukeireEntryUpdata.DENPYOU_DATE = SqlDateTime.Parse(dr["DENPYOU_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_DATE"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_DATE = SqlDateTime.Parse(dr["URIAGE_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_DATE"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_DATE = SqlDateTime.Parse(dr["SHIHARAI_DATE"].ToString());
                                }
                                ukeireEntryUpdata.TORIHIKISAKI_CD = dr["TORIHIKISAKI_CD"].ToString();
                                ukeireEntryUpdata.TORIHIKISAKI_NAME = dr["TORIHIKISAKI_NAME"].ToString();
                                ukeireEntryUpdata.GYOUSHA_CD = dr["GYOUSHA_CD"].ToString();
                                ukeireEntryUpdata.GYOUSHA_NAME = dr["GYOUSHA_NAME"].ToString();
                                ukeireEntryUpdata.GENBA_CD = dr["GENBA_CD"].ToString();
                                ukeireEntryUpdata.GENBA_NAME = dr["GENBA_NAME"].ToString();
                                ukeireEntryUpdata.NIOROSHI_GYOUSHA_CD = dr["NIOROSHI_GYOUSHA_CD"].ToString();
                                ukeireEntryUpdata.NIOROSHI_GYOUSHA_NAME = dr["NIOROSHI_GYOUSHA_NAME"].ToString();
                                ukeireEntryUpdata.NIOROSHI_GENBA_CD = dr["NIOROSHI_GENBA_CD"].ToString();
                                ukeireEntryUpdata.NIOROSHI_GENBA_NAME = dr["NIOROSHI_GENBA_NAME"].ToString();
                                ukeireEntryUpdata.EIGYOU_TANTOUSHA_CD = dr["EIGYOU_TANTOUSHA_CD"].ToString();
                                ukeireEntryUpdata.EIGYOU_TANTOUSHA_NAME = dr["EIGYOU_TANTOUSHA_NAME"].ToString();
                                ukeireEntryUpdata.NYUURYOKU_TANTOUSHA_CD = dr["NYUURYOKU_TANTOUSHA_CD"].ToString();
                                ukeireEntryUpdata.NYUURYOKU_TANTOUSHA_NAME = dr["NYUURYOKU_TANTOUSHA_NAME"].ToString();
                                ukeireEntryUpdata.SHARYOU_CD = dr["SHARYOU_CD"].ToString();
                                ukeireEntryUpdata.SHARYOU_NAME = dr["SHARYOU_NAME"].ToString();
                                ukeireEntryUpdata.SHASHU_CD = dr["SHASHU_CD"].ToString();
                                ukeireEntryUpdata.SHASHU_NAME = dr["SHASHU_NAME"].ToString();
                                ukeireEntryUpdata.UNPAN_GYOUSHA_CD = dr["UNPAN_GYOUSHA_CD"].ToString();
                                ukeireEntryUpdata.UNPAN_GYOUSHA_NAME = dr["UNPAN_GYOUSHA_NAME"].ToString();
                                ukeireEntryUpdata.UNTENSHA_CD = dr["UNTENSHA_CD"].ToString();
                                ukeireEntryUpdata.UNTENSHA_NAME = dr["UNTENSHA_NAME"].ToString();
                                if (!String.IsNullOrEmpty(dr["NINZUU_CNT"].ToString()))
                                {
                                    ukeireEntryUpdata.NINZUU_CNT = SqlInt16.Parse(dr["NINZUU_CNT"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEITAI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.KEITAI_KBN_CD = SqlInt16.Parse(dr["KEITAI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DAIKAN_KBN"].ToString()))
                                {
                                    ukeireEntryUpdata.DAIKAN_KBN = SqlInt16.Parse(dr["DAIKAN_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CONTENA_SOUSA_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.CONTENA_SOUSA_CD = SqlInt16.Parse(dr["CONTENA_SOUSA_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_SHURUI_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.MANIFEST_SHURUI_CD = SqlInt16.Parse(dr["MANIFEST_SHURUI_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_TEHAI_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.MANIFEST_TEHAI_CD = SqlInt16.Parse(dr["MANIFEST_TEHAI_CD"].ToString());
                                }
                                ukeireEntryUpdata.DENPYOU_BIKOU = dr["DENPYOU_BIKOU"].ToString();
                                ukeireEntryUpdata.TAIRYUU_BIKOU = dr["TAIRYUU_BIKOU"].ToString();
                                if (!String.IsNullOrEmpty(dr["UKETSUKE_NUMBER"].ToString()))
                                {
                                    ukeireEntryUpdata.UKETSUKE_NUMBER = SqlInt64.Parse(dr["UKETSUKE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEIRYOU_NUMBER"].ToString()))
                                {
                                    ukeireEntryUpdata.KEIRYOU_NUMBER = SqlInt64.Parse(dr["KEIRYOU_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["RECEIPT_NUMBER"].ToString()))
                                {
                                    ukeireEntryUpdata.RECEIPT_NUMBER = SqlInt32.Parse(dr["RECEIPT_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["NET_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.NET_TOTAL = SqlDecimal.Parse(dr["NET_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_SHOUHIZEI_RATE"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["URIAGE_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_KINGAKU_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_KINGAKU_TOTAL = SqlDecimal.Parse(dr["URIAGE_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_TAX_SOTO = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_TAX_UCHI = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.HINMEI_URIAGE_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.HINMEI_URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.HINMEI_URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_KINGAKU_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_KINGAKU_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_TAX_SOTO = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_TAX_UCHI = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.HINMEI_SHIHARAI_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    ukeireEntryUpdata.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_ZEI_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.URIAGE_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["URIAGE_TORIHIKI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_ZEI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryUpdata.SHIHARAI_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString());
                                }
                                ukeireEntryUpdata.TIME_STAMP = (byte[])(dr["TIME_STAMP"]);
                                //********
                                // WHOカラム設定
                                dataBind_ukeireEntryUpdata.SetSystemProperty(ukeireEntryUpdata, false);
                                ukeireEntryUpdata.DELETE_FLG = SqlBoolean.True;

                                ukeireEntryAdd = new T_UKEIRE_ENTRY();
                                if (!String.IsNullOrEmpty(dr["SYSTEM_ID"].ToString()))
                                {
                                    ukeireEntryAdd.SYSTEM_ID = SqlInt64.Parse(dr["SYSTEM_ID"].ToString());
                                }
                                ukeireEntryAdd.SEQ = SqlInt32.Parse(saiban.ToString());
                                if (!String.IsNullOrEmpty(dr["TAIRYUU_KBN"].ToString()))
                                {
                                    ukeireEntryAdd.TAIRYUU_KBN = SqlBoolean.Parse(dr["TAIRYUU_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KYOTEN_CD"].ToString()))
                                {
                                    ukeireEntryAdd.KYOTEN_CD = SqlInt16.Parse(dr["KYOTEN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["UKEIRE_NUMBER"].ToString()))
                                {
                                    ukeireEntryAdd.UKEIRE_NUMBER = SqlInt64.Parse(dr["UKEIRE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DATE_NUMBER"].ToString()))
                                {
                                    ukeireEntryAdd.DATE_NUMBER = SqlInt32.Parse(dr["DATE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["YEAR_NUMBER"].ToString()))
                                {
                                    ukeireEntryAdd.YEAR_NUMBER = SqlInt32.Parse(dr["YEAR_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                {
                                    ukeireEntryAdd.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DENPYOU_DATE"].ToString()))
                                {
                                    ukeireEntryAdd.DENPYOU_DATE = SqlDateTime.Parse(dr["DENPYOU_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_DATE"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_DATE = SqlDateTime.Parse(dr["URIAGE_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_DATE"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_DATE = SqlDateTime.Parse(dr["SHIHARAI_DATE"].ToString());
                                }
                                ukeireEntryAdd.TORIHIKISAKI_CD = dr["TORIHIKISAKI_CD"].ToString();
                                ukeireEntryAdd.TORIHIKISAKI_NAME = dr["TORIHIKISAKI_NAME"].ToString();
                                ukeireEntryAdd.GYOUSHA_CD = dr["GYOUSHA_CD"].ToString();
                                ukeireEntryAdd.GYOUSHA_NAME = dr["GYOUSHA_NAME"].ToString();
                                ukeireEntryAdd.GENBA_CD = dr["GENBA_CD"].ToString();
                                ukeireEntryAdd.GENBA_NAME = dr["GENBA_NAME"].ToString();
                                ukeireEntryAdd.NIOROSHI_GYOUSHA_CD = dr["NIOROSHI_GYOUSHA_CD"].ToString();
                                ukeireEntryAdd.NIOROSHI_GYOUSHA_NAME = dr["NIOROSHI_GYOUSHA_NAME"].ToString();
                                ukeireEntryAdd.NIOROSHI_GENBA_CD = dr["NIOROSHI_GENBA_CD"].ToString();
                                ukeireEntryAdd.NIOROSHI_GENBA_NAME = dr["NIOROSHI_GENBA_NAME"].ToString();
                                ukeireEntryAdd.EIGYOU_TANTOUSHA_CD = dr["EIGYOU_TANTOUSHA_CD"].ToString();
                                ukeireEntryAdd.EIGYOU_TANTOUSHA_NAME = dr["EIGYOU_TANTOUSHA_NAME"].ToString();
                                ukeireEntryAdd.NYUURYOKU_TANTOUSHA_CD = dr["NYUURYOKU_TANTOUSHA_CD"].ToString();
                                ukeireEntryAdd.NYUURYOKU_TANTOUSHA_NAME = dr["NYUURYOKU_TANTOUSHA_NAME"].ToString();
                                ukeireEntryAdd.SHARYOU_CD = dr["SHARYOU_CD"].ToString();
                                ukeireEntryAdd.SHARYOU_NAME = dr["SHARYOU_NAME"].ToString();
                                ukeireEntryAdd.SHASHU_CD = dr["SHASHU_CD"].ToString();
                                ukeireEntryAdd.SHASHU_NAME = dr["SHASHU_NAME"].ToString();
                                ukeireEntryAdd.UNPAN_GYOUSHA_CD = dr["UNPAN_GYOUSHA_CD"].ToString();
                                ukeireEntryAdd.UNPAN_GYOUSHA_NAME = dr["UNPAN_GYOUSHA_NAME"].ToString();
                                ukeireEntryAdd.UNTENSHA_CD = dr["UNTENSHA_CD"].ToString();
                                ukeireEntryAdd.UNTENSHA_NAME = dr["UNTENSHA_NAME"].ToString();
                                if (!String.IsNullOrEmpty(dr["NINZUU_CNT"].ToString()))
                                {
                                    ukeireEntryAdd.NINZUU_CNT = SqlInt16.Parse(dr["NINZUU_CNT"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEITAI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryAdd.KEITAI_KBN_CD = SqlInt16.Parse(dr["KEITAI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DAIKAN_KBN"].ToString()))
                                {
                                    ukeireEntryAdd.DAIKAN_KBN = SqlInt16.Parse(dr["DAIKAN_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CONTENA_SOUSA_CD"].ToString()))
                                {
                                    ukeireEntryAdd.CONTENA_SOUSA_CD = SqlInt16.Parse(dr["CONTENA_SOUSA_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_SHURUI_CD"].ToString()))
                                {
                                    ukeireEntryAdd.MANIFEST_SHURUI_CD = SqlInt16.Parse(dr["MANIFEST_SHURUI_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_TEHAI_CD"].ToString()))
                                {
                                    ukeireEntryAdd.MANIFEST_TEHAI_CD = SqlInt16.Parse(dr["MANIFEST_TEHAI_CD"].ToString());
                                }
                                ukeireEntryAdd.DENPYOU_BIKOU = dr["DENPYOU_BIKOU"].ToString();
                                ukeireEntryAdd.TAIRYUU_BIKOU = dr["TAIRYUU_BIKOU"].ToString();
                                if (!String.IsNullOrEmpty(dr["UKETSUKE_NUMBER"].ToString()))
                                {
                                    ukeireEntryAdd.UKETSUKE_NUMBER = SqlInt64.Parse(dr["UKETSUKE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEIRYOU_NUMBER"].ToString()))
                                {
                                    ukeireEntryAdd.KEIRYOU_NUMBER = SqlInt64.Parse(dr["KEIRYOU_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["RECEIPT_NUMBER"].ToString()))
                                {
                                    ukeireEntryAdd.RECEIPT_NUMBER = SqlInt32.Parse(dr["RECEIPT_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["NET_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.NET_TOTAL = SqlDecimal.Parse(dr["NET_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_SHOUHIZEI_RATE"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["URIAGE_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_KINGAKU_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_KINGAKU_TOTAL = SqlDecimal.Parse(dr["URIAGE_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_TAX_SOTO = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_TAX_UCHI = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.HINMEI_URIAGE_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.HINMEI_URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.HINMEI_URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_KINGAKU_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_KINGAKU_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_TAX_SOTO = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_TAX_UCHI = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.HINMEI_SHIHARAI_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    ukeireEntryAdd.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_ZEI_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryAdd.URIAGE_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["URIAGE_TORIHIKI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_ZEI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    ukeireEntryAdd.SHIHARAI_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString());
                                }
                                //ukeireEntryAdd.TIME_STAMP = (byte[])(dr["TIME_STAMP"]);
                                // WHOカラム設定
                                dataBind_ukeireEntryAdd.SetSystemProperty(ukeireEntryAdd, false);

                                if (!String.IsNullOrEmpty(dr["DELETE_FLG"].ToString()))
                                {
                                    ukeireEntryAdd.DELETE_FLG = SqlBoolean.Parse(dr["DELETE_FLG"].ToString());
                                }
                                ukeireEntryListAdd.Add(ukeireEntryAdd);
                                ukeireEntryListUpdata.Add(ukeireEntryUpdata);
                            }

                            foreach (DataRow dr in dbDetail.Rows)
                            {
                                ukeireMs = new T_UKEIRE_DETAIL();
                                if (!String.IsNullOrEmpty(dr["SYSTEM_ID"].ToString()))
                                {
                                    ukeireMs.SYSTEM_ID = SqlInt64.Parse(dr["SYSTEM_ID"].ToString());
                                }

                                ukeireMs.SEQ = SqlInt32.Parse(saiban.ToString());

                                if (!String.IsNullOrEmpty(dr["DETAIL_SYSTEM_ID"].ToString()))
                                {
                                    ukeireMs.DETAIL_SYSTEM_ID = SqlInt64.Parse(dr["DETAIL_SYSTEM_ID"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["UKEIRE_NUMBER"].ToString()))
                                {
                                    ukeireMs.UKEIRE_NUMBER = SqlInt64.Parse(dr["UKEIRE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["ROW_NO"].ToString()))
                                {
                                    ukeireMs.ROW_NO = SqlInt16.Parse(dr["ROW_NO"].ToString());
                                }
                                // 伝票単位の場合、全明細を未確定にする
                                if (this.msysinfo.SYS_KAKUTEI__TANNI_KBN.Equals(SqlInt16.Parse("1")))
                                {

                                    // 売上伝票の明細データは確定しない

                                    if (DENPYOU_KBN_SHIHARAI.Equals(dr["DENPYOU_KBN_CD"].ToString()))
                                    {
                                        // 伝票区分が支払の場合
                                        ukeireMs.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_MIKAKUTEI);
                                    }
                                    else
                                    {
                                        // 伝票区分が売上の場合
                                        if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                        {
                                            // 値を保持する
                                            ukeireMs.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    // 明細単位の場合は後でセットする
                                    if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                    {
                                        ukeireMs.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                    }
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGESHIHARAI_DATE"].ToString()))
                                {
                                    ukeireMs.URIAGESHIHARAI_DATE = SqlDateTime.Parse(dr["URIAGESHIHARAI_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["STACK_JYUURYOU"].ToString()))
                                {
                                    ukeireMs.STACK_JYUURYOU = SqlDecimal.Parse(dr["STACK_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["EMPTY_JYUURYOU"].ToString()))
                                {
                                    ukeireMs.EMPTY_JYUURYOU = SqlDecimal.Parse(dr["EMPTY_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["WARIFURI_JYUURYOU"].ToString()))
                                {
                                    ukeireMs.WARIFURI_JYUURYOU = SqlDecimal.Parse(dr["WARIFURI_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["WARIFURI_PERCENT"].ToString()))
                                {
                                    ukeireMs.WARIFURI_PERCENT = SqlDecimal.Parse(dr["WARIFURI_PERCENT"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CHOUSEI_JYUURYOU"].ToString()))
                                {
                                    ukeireMs.CHOUSEI_JYUURYOU = SqlInt64.Parse(dr["CHOUSEI_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CHOUSEI_PERCENT"].ToString()))
                                {
                                    ukeireMs.CHOUSEI_PERCENT = SqlDecimal.Parse(dr["CHOUSEI_PERCENT"].ToString());
                                }
                                ukeireMs.YOUKI_CD = dr["YOUKI_CD"].ToString();
                                if (!String.IsNullOrEmpty(dr["YOUKI_SUURYOU"].ToString()))
                                {
                                    ukeireMs.YOUKI_SUURYOU = SqlDecimal.Parse(dr["YOUKI_SUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["YOUKI_JYUURYOU"].ToString()))
                                {
                                    ukeireMs.YOUKI_JYUURYOU = SqlDecimal.Parse(dr["YOUKI_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DENPYOU_KBN_CD"].ToString()))
                                {
                                    ukeireMs.DENPYOU_KBN_CD = SqlInt16.Parse(dr["DENPYOU_KBN_CD"].ToString());
                                }
                                ukeireMs.HINMEI_CD = dr["HINMEI_CD"].ToString();
                                ukeireMs.HINMEI_NAME = dr["HINMEI_NAME"].ToString();
                                if (!String.IsNullOrEmpty(dr["NET_JYUURYOU"].ToString()))
                                {
                                    ukeireMs.NET_JYUURYOU = SqlInt64.Parse(dr["NET_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SUURYOU"].ToString()))
                                {
                                    ukeireMs.SUURYOU = SqlDecimal.Parse(dr["SUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["UNIT_CD"].ToString()))
                                {
                                    ukeireMs.UNIT_CD = SqlInt16.Parse(dr["UNIT_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["TANKA"].ToString()))
                                {
                                    ukeireMs.TANKA = SqlDecimal.Parse(dr["TANKA"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KINGAKU"].ToString()))
                                {
                                    ukeireMs.KINGAKU = SqlDecimal.Parse(dr["KINGAKU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["TAX_SOTO"].ToString()))
                                {
                                    ukeireMs.TAX_SOTO = SqlDecimal.Parse(dr["TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["TAX_UCHI"].ToString()))
                                {
                                    ukeireMs.TAX_UCHI = SqlDecimal.Parse(dr["TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_ZEI_KBN_CD"].ToString()))
                                {
                                    ukeireMs.HINMEI_ZEI_KBN_CD = SqlInt16.Parse(dr["HINMEI_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_KINGAKU"].ToString()))
                                {
                                    ukeireMs.HINMEI_KINGAKU = SqlDecimal.Parse(dr["HINMEI_KINGAKU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_TAX_SOTO"].ToString()))
                                {
                                    ukeireMs.HINMEI_TAX_SOTO = SqlDecimal.Parse(dr["HINMEI_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_TAX_UCHI"].ToString()))
                                {
                                    ukeireMs.HINMEI_TAX_UCHI = SqlDecimal.Parse(dr["HINMEI_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MEISAI_BIKOU"].ToString()))
                                {
                                    ukeireMs.MEISAI_BIKOU = dr["MEISAI_BIKOU"].ToString();
                                }
                                if (!String.IsNullOrEmpty(dr["NISUGATA_SUURYOU"].ToString()))
                                {
                                    ukeireMs.NISUGATA_SUURYOU = SqlDecimal.Parse(dr["NISUGATA_SUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["NISUGATA_SUURYOU"].ToString()))
                                {
                                    ukeireMs.NISUGATA_UNIT_CD = SqlInt16.Parse(dr["NISUGATA_UNIT_CD"].ToString());
                                }
                                //ukeireMs.TIME_STAMP = (byte[])(dr["TIME_STAMP"]);
                                // WHOカラム設定
                                dataBind_ukeireMs.SetSystemProperty(ukeireMs, false);

                                ukeireMsList.Add(ukeireMs);
                            }
                        }
                        foreach (T_UKEIRE_DETAIL item in ukeireMsList)
                        {
                            if (item.SYSTEM_ID.ToString().Equals(dgvRow["システムID"].ToString())
                            && (item.SEQ - 1).ToString().Equals(dgvRow["枝番"].ToString())
                            && item.DETAIL_SYSTEM_ID.ToString().Equals(dgvRow["明細・システムID"].ToString()))
                            {
                                item.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_MIKAKUTEI);
                                //item.SEQ = SqlInt16.Parse(saiban.ToString());
                            }

                        }

                        row++;
                        systemidukeire = dgvRow["システムID"].ToString();
                        sequkeire = dgvRow["枝番"].ToString();
                    }
                }

                string systemid = "";
                string seq = "";
                bool flag = false;
                int j = 1;
                foreach (T_UKEIRE_DETAIL item in ukeireMsList)
                {
                    if (j == 1)
                    {
                        systemid = item.SYSTEM_ID.ToString();
                        seq = item.SEQ.ToString();
                    }

                    if (item.SYSTEM_ID.ToString().Equals(systemid) && item.SEQ.ToString().Equals(seq))
                    {

                        // 未確定ならtrue(NULLの場合があるので確定以外で判定)
                        if (!item.KAKUTEI_KBN.ToString().Equals(KAKUTEI_KBN_KAKUTEI))
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        foreach (T_UKEIRE_ENTRY itemEntry in ukeireEntryListAdd)
                        {
                            if (SqlInt64.Parse(systemid) == itemEntry.SYSTEM_ID && SqlInt64.Parse(seq) == itemEntry.SEQ)
                            {
                                if (flag == true)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_MIKAKUTEI);
                                }
                                else if (flag == false)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_KAKUTEI);
                                }
                            }

                        }
                        flag = false;
                    }
                    if (j == ukeireMsList.Count)
                    {

                        // 未確定ならtrue(NULLの場合があるので確定以外で判定)
                        if (!item.KAKUTEI_KBN.ToString().Equals(KAKUTEI_KBN_KAKUTEI))
                        {
                            flag = true;
                        }


                        foreach (T_UKEIRE_ENTRY itemEntry in ukeireEntryListAdd)
                        {
                            if (item.SYSTEM_ID == itemEntry.SYSTEM_ID && item.SEQ == itemEntry.SEQ)
                            {
                                if (flag == true)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_MIKAKUTEI);
                                }
                                else if (flag == false)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_KAKUTEI);
                                }
                            }

                        }
                        flag = false;

                    }
                    systemid = item.SYSTEM_ID.ToString();
                    seq = item.SEQ.ToString();
                    j++;

                }
                this.mukeiremslist = ukeireMsList;
                this.mukeireentrylistadd = ukeireEntryListAdd;
                this.mukeireentrylistupdata = ukeireEntryListUpdata;
            }
        }

        /// <summary>
        /// 出荷データ取得
        /// </summary>
        private void GetShukaData(string kubun, DataTable shukaDt)
        {
            if (kubun == "TOUROKU")
            {
                List<T_SHUKKA_DETAIL> shukaMsList = new List<T_SHUKKA_DETAIL>();
                List<T_SHUKKA_ENTRY> shukaEntryListAdd = new List<T_SHUKKA_ENTRY>();
                List<T_SHUKKA_ENTRY> shukaEntryListUpdata = new List<T_SHUKKA_ENTRY>();


                string systemidukeire = "";
                string sequkeire = "";
                int row = 1;
                int saiban = 0;
                foreach (DataRow dgvRow in shukaDt.Rows)
                {
                    if (dgvRow["明細・確定区分"].ToString().Equals("True") && dgvRow["情報確定利用区分"].ToString().Equals(KAKUTEI_USE_KBN_USE)
                        && !dgvRow["明細・確定区分"].ToString().Equals(dgvRow["比較確定区分"].ToString()))
                    {
                        DataTable dbDetail = new DataTable();
                        DataTable dbEntry = new DataTable();
                        T_SHUKKA_DETAIL shukkaMs = new T_SHUKKA_DETAIL();
                        T_SHUKKA_ENTRY shukkaEntryAdd = new T_SHUKKA_ENTRY();
                        T_SHUKKA_ENTRY shukkaEntryUpdata = new T_SHUKKA_ENTRY();
                        // WHOカラム設定共通ロジック呼び出し用
                        var dataBind_shukkaMs = new DataBinderLogic<T_SHUKKA_DETAIL>(shukkaMs);
                        var dataBind_shukkaEntryAdd = new DataBinderLogic<T_SHUKKA_ENTRY>(shukkaEntryAdd);
                        var dataBind_shukkaEntryUpdata = new DataBinderLogic<T_SHUKKA_ENTRY>(shukkaEntryUpdata);

                        if (!dgvRow["システムID"].ToString().Equals(systemidukeire) || !dgvRow["枝番"].ToString().Equals(sequkeire))
                        {
                            dbDetail = SearchShukaDetail(dgvRow["システムID"].ToString(), dgvRow["枝番"].ToString());
                            dbEntry = SearchShukaEntry(dgvRow["システムID"].ToString(), dgvRow["枝番"].ToString());

                            if (!dgvRow["システムID"].ToString().Equals(systemidukeire))
                            {
                                saiban = SaiBan("T_SHUKKA_ENTRY", dgvRow["システムID"].ToString()) + 1;
                            }
                            else if (dgvRow["システムID"].ToString().Equals(systemidukeire) && !dgvRow["枝番"].ToString().Equals(sequkeire))
                            {
                                saiban = saiban + 1;
                            }

                            foreach (DataRow dr in dbEntry.Rows)
                            {
                                shukkaEntryUpdata = new T_SHUKKA_ENTRY();
                                if (!String.IsNullOrEmpty(dr["SYSTEM_ID"].ToString()))
                                {
                                    shukkaEntryUpdata.SYSTEM_ID = SqlInt64.Parse(dr["SYSTEM_ID"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SEQ"].ToString()))
                                {
                                    shukkaEntryUpdata.SEQ = SqlInt32.Parse(dr["SEQ"].ToString());
                                }
                                //**********
                                if (!String.IsNullOrEmpty(dr["TAIRYUU_KBN"].ToString()))
                                {
                                    shukkaEntryUpdata.TAIRYUU_KBN = SqlBoolean.Parse(dr["TAIRYUU_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KYOTEN_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.KYOTEN_CD = SqlInt16.Parse(dr["KYOTEN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHUKKA_NUMBER"].ToString()))
                                {
                                    shukkaEntryUpdata.SHUKKA_NUMBER = SqlInt64.Parse(dr["SHUKKA_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DATE_NUMBER"].ToString()))
                                {
                                    shukkaEntryUpdata.DATE_NUMBER = SqlInt32.Parse(dr["DATE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["YEAR_NUMBER"].ToString()))
                                {
                                    shukkaEntryUpdata.YEAR_NUMBER = SqlInt32.Parse(dr["YEAR_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                {
                                    shukkaEntryUpdata.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DENPYOU_DATE"].ToString()))
                                {
                                    shukkaEntryUpdata.DENPYOU_DATE = SqlDateTime.Parse(dr["DENPYOU_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_DATE"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_DATE = SqlDateTime.Parse(dr["URIAGE_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_DATE"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_DATE = SqlDateTime.Parse(dr["SHIHARAI_DATE"].ToString());
                                }
                                shukkaEntryUpdata.TORIHIKISAKI_CD = dr["TORIHIKISAKI_CD"].ToString();
                                shukkaEntryUpdata.TORIHIKISAKI_NAME = dr["TORIHIKISAKI_NAME"].ToString();
                                shukkaEntryUpdata.GYOUSHA_CD = dr["GYOUSHA_CD"].ToString();
                                shukkaEntryUpdata.GYOUSHA_NAME = dr["GYOUSHA_NAME"].ToString();
                                shukkaEntryUpdata.GENBA_CD = dr["GENBA_CD"].ToString();
                                shukkaEntryUpdata.GENBA_NAME = dr["GENBA_NAME"].ToString();
                                shukkaEntryUpdata.NIZUMI_GYOUSHA_CD = dr["NIZUMI_GYOUSHA_CD"].ToString();
                                shukkaEntryUpdata.NIZUMI_GYOUSHA_NAME = dr["NIZUMI_GYOUSHA_NAME"].ToString();
                                shukkaEntryUpdata.NIZUMI_GENBA_CD = dr["NIZUMI_GENBA_CD"].ToString();
                                shukkaEntryUpdata.NIZUMI_GENBA_NAME = dr["NIZUMI_GENBA_NAME"].ToString();
                                shukkaEntryUpdata.EIGYOU_TANTOUSHA_CD = dr["EIGYOU_TANTOUSHA_CD"].ToString();
                                shukkaEntryUpdata.EIGYOU_TANTOUSHA_NAME = dr["EIGYOU_TANTOUSHA_NAME"].ToString();
                                shukkaEntryUpdata.NYUURYOKU_TANTOUSHA_CD = dr["NYUURYOKU_TANTOUSHA_CD"].ToString();
                                shukkaEntryUpdata.NYUURYOKU_TANTOUSHA_NAME = dr["NYUURYOKU_TANTOUSHA_NAME"].ToString();
                                shukkaEntryUpdata.SHARYOU_CD = dr["SHARYOU_CD"].ToString();
                                shukkaEntryUpdata.SHARYOU_NAME = dr["SHARYOU_NAME"].ToString();
                                shukkaEntryUpdata.SHASHU_CD = dr["SHASHU_CD"].ToString();
                                shukkaEntryUpdata.SHASHU_NAME = dr["SHASHU_NAME"].ToString();
                                shukkaEntryUpdata.UNPAN_GYOUSHA_CD = dr["UNPAN_GYOUSHA_CD"].ToString();
                                shukkaEntryUpdata.UNPAN_GYOUSHA_NAME = dr["UNPAN_GYOUSHA_NAME"].ToString();
                                shukkaEntryUpdata.UNTENSHA_CD = dr["UNTENSHA_CD"].ToString();
                                shukkaEntryUpdata.UNTENSHA_NAME = dr["UNTENSHA_NAME"].ToString();
                                if (!String.IsNullOrEmpty(dr["NINZUU_CNT"].ToString()))
                                {
                                    shukkaEntryUpdata.NINZUU_CNT = SqlInt16.Parse(dr["NINZUU_CNT"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEITAI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.KEITAI_KBN_CD = SqlInt16.Parse(dr["KEITAI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DAIKAN_KBN"].ToString()))
                                {
                                    shukkaEntryUpdata.DAIKAN_KBN = SqlInt16.Parse(dr["DAIKAN_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CONTENA_SOUSA_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.CONTENA_SOUSA_CD = SqlInt16.Parse(dr["CONTENA_SOUSA_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_SHURUI_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.MANIFEST_SHURUI_CD = SqlInt16.Parse(dr["MANIFEST_SHURUI_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_TEHAI_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.MANIFEST_TEHAI_CD = SqlInt16.Parse(dr["MANIFEST_TEHAI_CD"].ToString());
                                }
                                shukkaEntryUpdata.DENPYOU_BIKOU = dr["DENPYOU_BIKOU"].ToString();
                                shukkaEntryUpdata.TAIRYUU_BIKOU = dr["TAIRYUU_BIKOU"].ToString();
                                if (!String.IsNullOrEmpty(dr["UKETSUKE_NUMBER"].ToString()))
                                {
                                    shukkaEntryUpdata.UKETSUKE_NUMBER = SqlInt64.Parse(dr["UKETSUKE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEIRYOU_NUMBER"].ToString()))
                                {
                                    shukkaEntryUpdata.KEIRYOU_NUMBER = SqlInt64.Parse(dr["KEIRYOU_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["RECEIPT_NUMBER"].ToString()))
                                {
                                    shukkaEntryUpdata.RECEIPT_NUMBER = SqlInt32.Parse(dr["RECEIPT_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["NET_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.NET_TOTAL = SqlDecimal.Parse(dr["NET_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_SHOUHIZEI_RATE"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["URIAGE_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_AMOUNT_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_AMOUNT_TOTAL = SqlDecimal.Parse(dr["URIAGE_AMOUNT_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_TAX_SOTO = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_TAX_UCHI = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.HINMEI_URIAGE_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.HINMEI_URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.HINMEI_URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_AMOUNT_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_AMOUNT_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_AMOUNT_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_TAX_SOTO = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_TAX_UCHI = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.HINMEI_SHIHARAI_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_ZEI_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["URIAGE_TORIHIKI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_ZEI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString());
                                }
                                shukkaEntryUpdata.TIME_STAMP = (byte[])(dr["TIME_STAMP"]);

                                //**********
                                // WHOカラム設定
                                dataBind_shukkaEntryUpdata.SetSystemProperty(shukkaEntryUpdata, false);

                                shukkaEntryUpdata.DELETE_FLG = SqlBoolean.True;

                                shukkaEntryAdd = new T_SHUKKA_ENTRY();
                                if (!String.IsNullOrEmpty(dr["SYSTEM_ID"].ToString()))
                                {
                                    shukkaEntryAdd.SYSTEM_ID = SqlInt64.Parse(dr["SYSTEM_ID"].ToString());
                                }
                                shukkaEntryAdd.SEQ = SqlInt32.Parse(saiban.ToString());
                                if (!String.IsNullOrEmpty(dr["TAIRYUU_KBN"].ToString()))
                                {
                                    shukkaEntryAdd.TAIRYUU_KBN = SqlBoolean.Parse(dr["TAIRYUU_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KYOTEN_CD"].ToString()))
                                {
                                    shukkaEntryAdd.KYOTEN_CD = SqlInt16.Parse(dr["KYOTEN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHUKKA_NUMBER"].ToString()))
                                {
                                    shukkaEntryAdd.SHUKKA_NUMBER = SqlInt64.Parse(dr["SHUKKA_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DATE_NUMBER"].ToString()))
                                {
                                    shukkaEntryAdd.DATE_NUMBER = SqlInt32.Parse(dr["DATE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["YEAR_NUMBER"].ToString()))
                                {
                                    shukkaEntryAdd.YEAR_NUMBER = SqlInt32.Parse(dr["YEAR_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                {
                                    shukkaEntryAdd.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DENPYOU_DATE"].ToString()))
                                {
                                    shukkaEntryAdd.DENPYOU_DATE = SqlDateTime.Parse(dr["DENPYOU_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_DATE"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_DATE = SqlDateTime.Parse(dr["URIAGE_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_DATE"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_DATE = SqlDateTime.Parse(dr["SHIHARAI_DATE"].ToString());
                                }
                                shukkaEntryAdd.TORIHIKISAKI_CD = dr["TORIHIKISAKI_CD"].ToString();
                                shukkaEntryAdd.TORIHIKISAKI_NAME = dr["TORIHIKISAKI_NAME"].ToString();
                                shukkaEntryAdd.GYOUSHA_CD = dr["GYOUSHA_CD"].ToString();
                                shukkaEntryAdd.GYOUSHA_NAME = dr["GYOUSHA_NAME"].ToString();
                                shukkaEntryAdd.GENBA_CD = dr["GENBA_CD"].ToString();
                                shukkaEntryAdd.GENBA_NAME = dr["GENBA_NAME"].ToString();
                                shukkaEntryAdd.NIZUMI_GYOUSHA_CD = dr["NIZUMI_GYOUSHA_CD"].ToString();
                                shukkaEntryAdd.NIZUMI_GYOUSHA_NAME = dr["NIZUMI_GYOUSHA_NAME"].ToString();
                                shukkaEntryAdd.NIZUMI_GENBA_CD = dr["NIZUMI_GENBA_CD"].ToString();
                                shukkaEntryAdd.NIZUMI_GENBA_NAME = dr["NIZUMI_GENBA_NAME"].ToString();
                                shukkaEntryAdd.EIGYOU_TANTOUSHA_CD = dr["EIGYOU_TANTOUSHA_CD"].ToString();
                                shukkaEntryAdd.EIGYOU_TANTOUSHA_NAME = dr["EIGYOU_TANTOUSHA_NAME"].ToString();
                                shukkaEntryAdd.NYUURYOKU_TANTOUSHA_CD = dr["NYUURYOKU_TANTOUSHA_CD"].ToString();
                                shukkaEntryAdd.NYUURYOKU_TANTOUSHA_NAME = dr["NYUURYOKU_TANTOUSHA_NAME"].ToString();
                                shukkaEntryAdd.SHARYOU_CD = dr["SHARYOU_CD"].ToString();
                                shukkaEntryAdd.SHARYOU_NAME = dr["SHARYOU_NAME"].ToString();
                                shukkaEntryAdd.SHASHU_CD = dr["SHASHU_CD"].ToString();
                                shukkaEntryAdd.SHASHU_NAME = dr["SHASHU_NAME"].ToString();
                                shukkaEntryAdd.UNPAN_GYOUSHA_CD = dr["UNPAN_GYOUSHA_CD"].ToString();
                                shukkaEntryAdd.UNPAN_GYOUSHA_NAME = dr["UNPAN_GYOUSHA_NAME"].ToString();
                                shukkaEntryAdd.UNTENSHA_CD = dr["UNTENSHA_CD"].ToString();
                                shukkaEntryAdd.UNTENSHA_NAME = dr["UNTENSHA_NAME"].ToString();
                                if (!String.IsNullOrEmpty(dr["NINZUU_CNT"].ToString()))
                                {
                                    shukkaEntryAdd.NINZUU_CNT = SqlInt16.Parse(dr["NINZUU_CNT"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEITAI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryAdd.KEITAI_KBN_CD = SqlInt16.Parse(dr["KEITAI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DAIKAN_KBN"].ToString()))
                                {
                                    shukkaEntryAdd.DAIKAN_KBN = SqlInt16.Parse(dr["DAIKAN_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CONTENA_SOUSA_CD"].ToString()))
                                {
                                    shukkaEntryAdd.CONTENA_SOUSA_CD = SqlInt16.Parse(dr["CONTENA_SOUSA_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_SHURUI_CD"].ToString()))
                                {
                                    shukkaEntryAdd.MANIFEST_SHURUI_CD = SqlInt16.Parse(dr["MANIFEST_SHURUI_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_TEHAI_CD"].ToString()))
                                {
                                    shukkaEntryAdd.MANIFEST_TEHAI_CD = SqlInt16.Parse(dr["MANIFEST_TEHAI_CD"].ToString());
                                }
                                shukkaEntryAdd.DENPYOU_BIKOU = dr["DENPYOU_BIKOU"].ToString();
                                shukkaEntryAdd.TAIRYUU_BIKOU = dr["TAIRYUU_BIKOU"].ToString();
                                if (!String.IsNullOrEmpty(dr["UKETSUKE_NUMBER"].ToString()))
                                {
                                    shukkaEntryAdd.UKETSUKE_NUMBER = SqlInt64.Parse(dr["UKETSUKE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEIRYOU_NUMBER"].ToString()))
                                {
                                    shukkaEntryAdd.KEIRYOU_NUMBER = SqlInt64.Parse(dr["KEIRYOU_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["RECEIPT_NUMBER"].ToString()))
                                {
                                    shukkaEntryAdd.RECEIPT_NUMBER = SqlInt32.Parse(dr["RECEIPT_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["NET_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.NET_TOTAL = SqlDecimal.Parse(dr["NET_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_SHOUHIZEI_RATE"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["URIAGE_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_AMOUNT_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_AMOUNT_TOTAL = SqlDecimal.Parse(dr["URIAGE_AMOUNT_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_TAX_SOTO = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_TAX_UCHI = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.HINMEI_URIAGE_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.HINMEI_URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.HINMEI_URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_AMOUNT_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_AMOUNT_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_AMOUNT_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_TAX_SOTO = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_TAX_UCHI = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.HINMEI_SHIHARAI_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_ZEI_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["URIAGE_TORIHIKI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_ZEI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString());
                                }
                                //shukkaEntryAdd.TIME_STAMP = (byte[])(dr["TIME_STAMP"]);
                                // WHOカラム設定
                                dataBind_shukkaEntryAdd.SetSystemProperty(shukkaEntryAdd, false);

                                if (!String.IsNullOrEmpty(dr["DELETE_FLG"].ToString()))
                                {
                                    shukkaEntryAdd.DELETE_FLG = SqlBoolean.Parse(dr["DELETE_FLG"].ToString());
                                }
                                shukaEntryListAdd.Add(shukkaEntryAdd);
                                shukaEntryListUpdata.Add(shukkaEntryUpdata);
                            }

                            foreach (DataRow dr in dbDetail.Rows)
                            {
                                shukkaMs = new T_SHUKKA_DETAIL();
                                if (!String.IsNullOrEmpty(dr["SYSTEM_ID"].ToString()))
                                {
                                    shukkaMs.SYSTEM_ID = SqlInt64.Parse(dr["SYSTEM_ID"].ToString());
                                }

                                shukkaMs.SEQ = SqlInt32.Parse(saiban.ToString());

                                if (!String.IsNullOrEmpty(dr["DETAIL_SYSTEM_ID"].ToString()))
                                {
                                    shukkaMs.DETAIL_SYSTEM_ID = SqlInt64.Parse(dr["DETAIL_SYSTEM_ID"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHUKKA_NUMBER"].ToString()))
                                {
                                    shukkaMs.SHUKKA_NUMBER = SqlInt64.Parse(dr["SHUKKA_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["ROW_NO"].ToString()))
                                {
                                    shukkaMs.ROW_NO = SqlInt16.Parse(dr["ROW_NO"].ToString());
                                }
                                // 伝票単位の場合、全明細を確定にする
                                if (this.msysinfo.SYS_KAKUTEI__TANNI_KBN.Equals(SqlInt16.Parse("1")))
                                {

                                    // 売上伝票の明細データは確定しない

                                    if (DENPYOU_KBN_SHIHARAI.Equals(dr["DENPYOU_KBN_CD"].ToString()))
                                    {
                                        // 伝票区分が支払の場合
                                        shukkaMs.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_KAKUTEI);
                                    }
                                    else
                                    {
                                        // 伝票区分が売上の場合
                                        if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                        {
                                            // 値を保持する
                                            shukkaMs.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    // 明細単位の場合は後でセットする
                                    if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                    {
                                        shukkaMs.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                    }
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGESHIHARAI_DATE"].ToString()))
                                {
                                    shukkaMs.URIAGESHIHARAI_DATE = SqlDateTime.Parse(dr["URIAGESHIHARAI_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["STACK_JYUURYOU"].ToString()))
                                {
                                    shukkaMs.STACK_JYUURYOU = SqlDecimal.Parse(dr["STACK_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["EMPTY_JYUURYOU"].ToString()))
                                {
                                    shukkaMs.EMPTY_JYUURYOU = SqlDecimal.Parse(dr["EMPTY_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["WARIFURI_JYUURYOU"].ToString()))
                                {
                                    shukkaMs.WARIFURI_JYUURYOU = SqlDecimal.Parse(dr["WARIFURI_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["WARIFURI_PERCENT"].ToString()))
                                {
                                    shukkaMs.WARIFURI_PERCENT = SqlDecimal.Parse(dr["WARIFURI_PERCENT"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CHOUSEI_JYUURYOU"].ToString()))
                                {
                                    shukkaMs.CHOUSEI_JYUURYOU = SqlInt64.Parse(dr["CHOUSEI_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CHOUSEI_PERCENT"].ToString()))
                                {
                                    shukkaMs.CHOUSEI_PERCENT = SqlDecimal.Parse(dr["CHOUSEI_PERCENT"].ToString());
                                }
                                shukkaMs.YOUKI_CD = dr["YOUKI_CD"].ToString();
                                if (!String.IsNullOrEmpty(dr["YOUKI_SUURYOU"].ToString()))
                                {
                                    shukkaMs.YOUKI_SUURYOU = SqlDecimal.Parse(dr["YOUKI_SUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["YOUKI_JYUURYOU"].ToString()))
                                {
                                    shukkaMs.YOUKI_JYUURYOU = SqlDecimal.Parse(dr["YOUKI_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DENPYOU_KBN_CD"].ToString()))
                                {
                                    shukkaMs.DENPYOU_KBN_CD = SqlInt16.Parse(dr["DENPYOU_KBN_CD"].ToString());
                                }
                                shukkaMs.HINMEI_CD = dr["HINMEI_CD"].ToString();
                                shukkaMs.HINMEI_NAME = dr["HINMEI_NAME"].ToString();
                                if (!String.IsNullOrEmpty(dr["NET_JYUURYOU"].ToString()))
                                {
                                    shukkaMs.NET_JYUURYOU = SqlInt64.Parse(dr["NET_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SUURYOU"].ToString()))
                                {
                                    shukkaMs.SUURYOU = SqlDecimal.Parse(dr["SUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["UNIT_CD"].ToString()))
                                {
                                    shukkaMs.UNIT_CD = SqlInt16.Parse(dr["UNIT_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["TANKA"].ToString()))
                                {
                                    shukkaMs.TANKA = SqlDecimal.Parse(dr["TANKA"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KINGAKU"].ToString()))
                                {
                                    shukkaMs.KINGAKU = SqlDecimal.Parse(dr["KINGAKU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["TAX_SOTO"].ToString()))
                                {
                                    shukkaMs.TAX_SOTO = SqlDecimal.Parse(dr["TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["TAX_UCHI"].ToString()))
                                {
                                    shukkaMs.TAX_UCHI = SqlDecimal.Parse(dr["TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_ZEI_KBN_CD"].ToString()))
                                {
                                    shukkaMs.HINMEI_ZEI_KBN_CD = SqlInt16.Parse(dr["HINMEI_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_KINGAKU"].ToString()))
                                {
                                    shukkaMs.HINMEI_KINGAKU = SqlDecimal.Parse(dr["HINMEI_KINGAKU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_TAX_SOTO"].ToString()))
                                {
                                    shukkaMs.HINMEI_TAX_SOTO = SqlDecimal.Parse(dr["HINMEI_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_TAX_UCHI"].ToString()))
                                {
                                    shukkaMs.HINMEI_TAX_UCHI = SqlDecimal.Parse(dr["HINMEI_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MEISAI_BIKOU"].ToString()))
                                {
                                    shukkaMs.MEISAI_BIKOU = dr["MEISAI_BIKOU"].ToString();
                                }
                                if (!String.IsNullOrEmpty(dr["NISUGATA_SUURYOU"].ToString()))
                                {
                                    shukkaMs.NISUGATA_SUURYOU = SqlDecimal.Parse(dr["NISUGATA_SUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["NISUGATA_SUURYOU"].ToString()))
                                {
                                    shukkaMs.NISUGATA_UNIT_CD = SqlInt16.Parse(dr["NISUGATA_UNIT_CD"].ToString());
                                }
                                //shukkaMs.TIME_STAMP = (byte[])(dr["TIME_STAMP"]);
                                // WHOカラム設定
                                dataBind_shukkaMs.SetSystemProperty(shukkaMs, false);

                                shukaMsList.Add(shukkaMs);
                            }
                        }
                        foreach (T_SHUKKA_DETAIL item in shukaMsList)
                        {
                            if (item.SYSTEM_ID.ToString().Equals(dgvRow["システムID"].ToString())
                            && (item.SEQ - 1).ToString().Equals(dgvRow["枝番"].ToString())
                            && item.DETAIL_SYSTEM_ID.ToString().Equals(dgvRow["明細・システムID"].ToString()))
                            {
                                item.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_KAKUTEI);
                                //item.SEQ = SqlInt16.Parse(saiban.ToString());
                            }
                        }

                        row++;
                        systemidukeire = dgvRow["システムID"].ToString();
                        sequkeire = dgvRow["枝番"].ToString();
                    }
                }

                string syetemid = "";
                string seq = "";
                bool flag = false;
                int j = 1;
                foreach (T_SHUKKA_DETAIL item in shukaMsList)
                {
                    if (j == 1)
                    {
                        syetemid = item.SYSTEM_ID.ToString();
                        seq = item.SEQ.ToString();
                    }

                    if (item.SYSTEM_ID.ToString().Equals(syetemid) && item.SEQ.ToString().Equals(seq))
                    {
                        // 未確定ならtrue(NULLの場合があるので確定以外で判定)
                        if (!item.KAKUTEI_KBN.ToString().Equals(KAKUTEI_KBN_KAKUTEI))
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        foreach (T_SHUKKA_ENTRY itemEntry in shukaEntryListAdd)
                        {
                            if (SqlInt64.Parse(syetemid) == itemEntry.SYSTEM_ID && SqlInt64.Parse(seq) == itemEntry.SEQ)
                            {
                                if (flag == true)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_MIKAKUTEI);
                                }
                                else if (flag == false)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_KAKUTEI);
                                }
                            }

                        }
                        flag = false;
                    }
                    if (j == shukaMsList.Count)
                    {
                        // 未確定ならtrue(NULLの場合があるので確定以外で判定)
                        if (!item.KAKUTEI_KBN.ToString().Equals(KAKUTEI_KBN_KAKUTEI))
                        {
                            flag = true;
                        }


                        foreach (T_SHUKKA_ENTRY itemEntry in shukaEntryListAdd)
                        {
                            if (item.SYSTEM_ID == itemEntry.SYSTEM_ID && item.SEQ == itemEntry.SEQ)
                            {
                                if (flag == true)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_MIKAKUTEI);
                                }
                                else if (flag == false)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_KAKUTEI);
                                }
                            }

                        }
                        flag = false;

                    }
                    syetemid = item.SYSTEM_ID.ToString();
                    seq = item.SEQ.ToString();
                    j++;

                }
                this.mshukamslist = shukaMsList;
                this.mshukaentrylistadd = shukaEntryListAdd;
                this.mshukaentrylistupdata = shukaEntryListUpdata;
            }
            else if (kubun == "KAIJYO")
            {
                List<T_SHUKKA_DETAIL> shukaMsList = new List<T_SHUKKA_DETAIL>();
                List<T_SHUKKA_ENTRY> shukaEntryListAdd = new List<T_SHUKKA_ENTRY>();
                List<T_SHUKKA_ENTRY> shukaEntryListUpdata = new List<T_SHUKKA_ENTRY>();


                string systemidukeire = "";
                string sequkeire = "";
                int row = 1;
                int saiban = 0;
                foreach (DataRow dgvRow in shukaDt.Rows)
                {
                    if (dgvRow["明細・確定区分"].ToString().Equals("False") && dgvRow["情報確定利用区分"].ToString().Equals(KAKUTEI_USE_KBN_USE)
                        && !dgvRow["明細・確定区分"].ToString().Equals(dgvRow["比較確定区分"].ToString()))
                    {
                        DataTable dbDetail = new DataTable();
                        DataTable dbEntry = new DataTable();
                        T_SHUKKA_DETAIL shukkaMs = new T_SHUKKA_DETAIL();
                        T_SHUKKA_ENTRY shukkaEntryAdd = new T_SHUKKA_ENTRY();
                        T_SHUKKA_ENTRY shukkaEntryUpdata = new T_SHUKKA_ENTRY();
                        // WHOカラム設定共通ロジック呼び出し用
                        var dataBind_shukkaMs = new DataBinderLogic<T_SHUKKA_DETAIL>(shukkaMs);
                        var dataBind_shukkaEntryAdd = new DataBinderLogic<T_SHUKKA_ENTRY>(shukkaEntryAdd);
                        var dataBind_shukkaEntryUpdata = new DataBinderLogic<T_SHUKKA_ENTRY>(shukkaEntryUpdata);

                        if (!dgvRow["システムID"].ToString().Equals(systemidukeire) || !dgvRow["枝番"].ToString().Equals(sequkeire))
                        {
                            dbDetail = SearchShukaDetail(dgvRow["システムID"].ToString(), dgvRow["枝番"].ToString());
                            dbEntry = SearchShukaEntry(dgvRow["システムID"].ToString(), dgvRow["枝番"].ToString());

                            if (!dgvRow["システムID"].ToString().Equals(systemidukeire))
                            {
                                saiban = SaiBan("T_SHUKKA_ENTRY", dgvRow["システムID"].ToString()) + 1;
                            }
                            else if (dgvRow["システムID"].ToString().Equals(systemidukeire) && !dgvRow["枝番"].ToString().Equals(sequkeire))
                            {
                                saiban = saiban + 1;
                            }

                            foreach (DataRow dr in dbEntry.Rows)
                            {
                                shukkaEntryUpdata = new T_SHUKKA_ENTRY();
                                if (!String.IsNullOrEmpty(dr["SYSTEM_ID"].ToString()))
                                {
                                    shukkaEntryUpdata.SYSTEM_ID = SqlInt64.Parse(dr["SYSTEM_ID"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SEQ"].ToString()))
                                {
                                    shukkaEntryUpdata.SEQ = SqlInt32.Parse(dr["SEQ"].ToString());
                                }
                                //*********
                                if (!String.IsNullOrEmpty(dr["TAIRYUU_KBN"].ToString()))
                                {
                                    shukkaEntryUpdata.TAIRYUU_KBN = SqlBoolean.Parse(dr["TAIRYUU_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KYOTEN_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.KYOTEN_CD = SqlInt16.Parse(dr["KYOTEN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHUKKA_NUMBER"].ToString()))
                                {
                                    shukkaEntryUpdata.SHUKKA_NUMBER = SqlInt64.Parse(dr["SHUKKA_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DATE_NUMBER"].ToString()))
                                {
                                    shukkaEntryUpdata.DATE_NUMBER = SqlInt32.Parse(dr["DATE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["YEAR_NUMBER"].ToString()))
                                {
                                    shukkaEntryUpdata.YEAR_NUMBER = SqlInt32.Parse(dr["YEAR_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                {
                                    shukkaEntryUpdata.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DENPYOU_DATE"].ToString()))
                                {
                                    shukkaEntryUpdata.DENPYOU_DATE = SqlDateTime.Parse(dr["DENPYOU_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_DATE"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_DATE = SqlDateTime.Parse(dr["URIAGE_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_DATE"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_DATE = SqlDateTime.Parse(dr["SHIHARAI_DATE"].ToString());
                                }
                                shukkaEntryUpdata.TORIHIKISAKI_CD = dr["TORIHIKISAKI_CD"].ToString();
                                shukkaEntryUpdata.TORIHIKISAKI_NAME = dr["TORIHIKISAKI_NAME"].ToString();
                                shukkaEntryUpdata.GYOUSHA_CD = dr["GYOUSHA_CD"].ToString();
                                shukkaEntryUpdata.GYOUSHA_NAME = dr["GYOUSHA_NAME"].ToString();
                                shukkaEntryUpdata.GENBA_CD = dr["GENBA_CD"].ToString();
                                shukkaEntryUpdata.GENBA_NAME = dr["GENBA_NAME"].ToString();
                                shukkaEntryUpdata.NIZUMI_GYOUSHA_CD = dr["NIZUMI_GYOUSHA_CD"].ToString();
                                shukkaEntryUpdata.NIZUMI_GYOUSHA_NAME = dr["NIZUMI_GYOUSHA_NAME"].ToString();
                                shukkaEntryUpdata.NIZUMI_GENBA_CD = dr["NIZUMI_GENBA_CD"].ToString();
                                shukkaEntryUpdata.NIZUMI_GENBA_NAME = dr["NIZUMI_GENBA_NAME"].ToString();
                                shukkaEntryUpdata.EIGYOU_TANTOUSHA_CD = dr["EIGYOU_TANTOUSHA_CD"].ToString();
                                shukkaEntryUpdata.EIGYOU_TANTOUSHA_NAME = dr["EIGYOU_TANTOUSHA_NAME"].ToString();
                                shukkaEntryUpdata.NYUURYOKU_TANTOUSHA_CD = dr["NYUURYOKU_TANTOUSHA_CD"].ToString();
                                shukkaEntryUpdata.NYUURYOKU_TANTOUSHA_NAME = dr["NYUURYOKU_TANTOUSHA_NAME"].ToString();
                                shukkaEntryUpdata.SHARYOU_CD = dr["SHARYOU_CD"].ToString();
                                shukkaEntryUpdata.SHARYOU_NAME = dr["SHARYOU_NAME"].ToString();
                                shukkaEntryUpdata.SHASHU_CD = dr["SHASHU_CD"].ToString();
                                shukkaEntryUpdata.SHASHU_NAME = dr["SHASHU_NAME"].ToString();
                                shukkaEntryUpdata.UNPAN_GYOUSHA_CD = dr["UNPAN_GYOUSHA_CD"].ToString();
                                shukkaEntryUpdata.UNPAN_GYOUSHA_NAME = dr["UNPAN_GYOUSHA_NAME"].ToString();
                                shukkaEntryUpdata.UNTENSHA_CD = dr["UNTENSHA_CD"].ToString();
                                shukkaEntryUpdata.UNTENSHA_NAME = dr["UNTENSHA_NAME"].ToString();
                                if (!String.IsNullOrEmpty(dr["NINZUU_CNT"].ToString()))
                                {
                                    shukkaEntryUpdata.NINZUU_CNT = SqlInt16.Parse(dr["NINZUU_CNT"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEITAI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.KEITAI_KBN_CD = SqlInt16.Parse(dr["KEITAI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DAIKAN_KBN"].ToString()))
                                {
                                    shukkaEntryUpdata.DAIKAN_KBN = SqlInt16.Parse(dr["DAIKAN_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CONTENA_SOUSA_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.CONTENA_SOUSA_CD = SqlInt16.Parse(dr["CONTENA_SOUSA_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_SHURUI_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.MANIFEST_SHURUI_CD = SqlInt16.Parse(dr["MANIFEST_SHURUI_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_TEHAI_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.MANIFEST_TEHAI_CD = SqlInt16.Parse(dr["MANIFEST_TEHAI_CD"].ToString());
                                }
                                shukkaEntryUpdata.DENPYOU_BIKOU = dr["DENPYOU_BIKOU"].ToString();
                                shukkaEntryUpdata.TAIRYUU_BIKOU = dr["TAIRYUU_BIKOU"].ToString();
                                if (!String.IsNullOrEmpty(dr["UKETSUKE_NUMBER"].ToString()))
                                {
                                    shukkaEntryUpdata.UKETSUKE_NUMBER = SqlInt64.Parse(dr["UKETSUKE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEIRYOU_NUMBER"].ToString()))
                                {
                                    shukkaEntryUpdata.KEIRYOU_NUMBER = SqlInt64.Parse(dr["KEIRYOU_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["RECEIPT_NUMBER"].ToString()))
                                {
                                    shukkaEntryUpdata.RECEIPT_NUMBER = SqlInt32.Parse(dr["RECEIPT_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["NET_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.NET_TOTAL = SqlDecimal.Parse(dr["NET_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_SHOUHIZEI_RATE"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["URIAGE_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_AMOUNT_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_AMOUNT_TOTAL = SqlDecimal.Parse(dr["URIAGE_AMOUNT_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_TAX_SOTO = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_TAX_UCHI = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.HINMEI_URIAGE_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.HINMEI_URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.HINMEI_URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_AMOUNT_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_AMOUNT_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_AMOUNT_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_TAX_SOTO = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_TAX_UCHI = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.HINMEI_SHIHARAI_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    shukkaEntryUpdata.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_ZEI_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.URIAGE_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["URIAGE_TORIHIKI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_ZEI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryUpdata.SHIHARAI_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString());
                                }
                                shukkaEntryUpdata.TIME_STAMP = (byte[])(dr["TIME_STAMP"]);

                                //***********
                                // WHOカラム設定
                                dataBind_shukkaEntryUpdata.SetSystemProperty(shukkaEntryUpdata, false);
                                shukkaEntryUpdata.DELETE_FLG = SqlBoolean.True;

                                shukkaEntryAdd = new T_SHUKKA_ENTRY();
                                if (!String.IsNullOrEmpty(dr["SYSTEM_ID"].ToString()))
                                {
                                    shukkaEntryAdd.SYSTEM_ID = SqlInt64.Parse(dr["SYSTEM_ID"].ToString());
                                }
                                shukkaEntryAdd.SEQ = SqlInt32.Parse(saiban.ToString());
                                if (!String.IsNullOrEmpty(dr["TAIRYUU_KBN"].ToString()))
                                {
                                    shukkaEntryAdd.TAIRYUU_KBN = SqlBoolean.Parse(dr["TAIRYUU_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KYOTEN_CD"].ToString()))
                                {
                                    shukkaEntryAdd.KYOTEN_CD = SqlInt16.Parse(dr["KYOTEN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHUKKA_NUMBER"].ToString()))
                                {
                                    shukkaEntryAdd.SHUKKA_NUMBER = SqlInt64.Parse(dr["SHUKKA_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DATE_NUMBER"].ToString()))
                                {
                                    shukkaEntryAdd.DATE_NUMBER = SqlInt32.Parse(dr["DATE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["YEAR_NUMBER"].ToString()))
                                {
                                    shukkaEntryAdd.YEAR_NUMBER = SqlInt32.Parse(dr["YEAR_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                {
                                    shukkaEntryAdd.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DENPYOU_DATE"].ToString()))
                                {
                                    shukkaEntryAdd.DENPYOU_DATE = SqlDateTime.Parse(dr["DENPYOU_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_DATE"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_DATE = SqlDateTime.Parse(dr["URIAGE_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_DATE"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_DATE = SqlDateTime.Parse(dr["SHIHARAI_DATE"].ToString());
                                }
                                shukkaEntryAdd.TORIHIKISAKI_CD = dr["TORIHIKISAKI_CD"].ToString();
                                shukkaEntryAdd.TORIHIKISAKI_NAME = dr["TORIHIKISAKI_NAME"].ToString();
                                shukkaEntryAdd.GYOUSHA_CD = dr["GYOUSHA_CD"].ToString();
                                shukkaEntryAdd.GYOUSHA_NAME = dr["GYOUSHA_NAME"].ToString();
                                shukkaEntryAdd.GENBA_CD = dr["GENBA_CD"].ToString();
                                shukkaEntryAdd.GENBA_NAME = dr["GENBA_NAME"].ToString();
                                shukkaEntryAdd.NIZUMI_GYOUSHA_CD = dr["NIZUMI_GYOUSHA_CD"].ToString();
                                shukkaEntryAdd.NIZUMI_GYOUSHA_NAME = dr["NIZUMI_GYOUSHA_NAME"].ToString();
                                shukkaEntryAdd.NIZUMI_GENBA_CD = dr["NIZUMI_GENBA_CD"].ToString();
                                shukkaEntryAdd.NIZUMI_GENBA_NAME = dr["NIZUMI_GENBA_NAME"].ToString();
                                shukkaEntryAdd.EIGYOU_TANTOUSHA_CD = dr["EIGYOU_TANTOUSHA_CD"].ToString();
                                shukkaEntryAdd.EIGYOU_TANTOUSHA_NAME = dr["EIGYOU_TANTOUSHA_NAME"].ToString();
                                shukkaEntryAdd.NYUURYOKU_TANTOUSHA_CD = dr["NYUURYOKU_TANTOUSHA_CD"].ToString();
                                shukkaEntryAdd.NYUURYOKU_TANTOUSHA_NAME = dr["NYUURYOKU_TANTOUSHA_NAME"].ToString();
                                shukkaEntryAdd.SHARYOU_CD = dr["SHARYOU_CD"].ToString();
                                shukkaEntryAdd.SHARYOU_NAME = dr["SHARYOU_NAME"].ToString();
                                shukkaEntryAdd.SHASHU_CD = dr["SHASHU_CD"].ToString();
                                shukkaEntryAdd.SHASHU_NAME = dr["SHASHU_NAME"].ToString();
                                shukkaEntryAdd.UNPAN_GYOUSHA_CD = dr["UNPAN_GYOUSHA_CD"].ToString();
                                shukkaEntryAdd.UNPAN_GYOUSHA_NAME = dr["UNPAN_GYOUSHA_NAME"].ToString();
                                shukkaEntryAdd.UNTENSHA_CD = dr["UNTENSHA_CD"].ToString();
                                shukkaEntryAdd.UNTENSHA_NAME = dr["UNTENSHA_NAME"].ToString();
                                if (!String.IsNullOrEmpty(dr["NINZUU_CNT"].ToString()))
                                {
                                    shukkaEntryAdd.NINZUU_CNT = SqlInt16.Parse(dr["NINZUU_CNT"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEITAI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryAdd.KEITAI_KBN_CD = SqlInt16.Parse(dr["KEITAI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DAIKAN_KBN"].ToString()))
                                {
                                    shukkaEntryAdd.DAIKAN_KBN = SqlInt16.Parse(dr["DAIKAN_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CONTENA_SOUSA_CD"].ToString()))
                                {
                                    shukkaEntryAdd.CONTENA_SOUSA_CD = SqlInt16.Parse(dr["CONTENA_SOUSA_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_SHURUI_CD"].ToString()))
                                {
                                    shukkaEntryAdd.MANIFEST_SHURUI_CD = SqlInt16.Parse(dr["MANIFEST_SHURUI_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_TEHAI_CD"].ToString()))
                                {
                                    shukkaEntryAdd.MANIFEST_TEHAI_CD = SqlInt16.Parse(dr["MANIFEST_TEHAI_CD"].ToString());
                                }
                                shukkaEntryAdd.DENPYOU_BIKOU = dr["DENPYOU_BIKOU"].ToString();
                                shukkaEntryAdd.TAIRYUU_BIKOU = dr["TAIRYUU_BIKOU"].ToString();
                                if (!String.IsNullOrEmpty(dr["UKETSUKE_NUMBER"].ToString()))
                                {
                                    shukkaEntryAdd.UKETSUKE_NUMBER = SqlInt64.Parse(dr["UKETSUKE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEIRYOU_NUMBER"].ToString()))
                                {
                                    shukkaEntryAdd.KEIRYOU_NUMBER = SqlInt64.Parse(dr["KEIRYOU_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["RECEIPT_NUMBER"].ToString()))
                                {
                                    shukkaEntryAdd.RECEIPT_NUMBER = SqlInt32.Parse(dr["RECEIPT_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["NET_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.NET_TOTAL = SqlDecimal.Parse(dr["NET_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_SHOUHIZEI_RATE"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["URIAGE_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_AMOUNT_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_AMOUNT_TOTAL = SqlDecimal.Parse(dr["URIAGE_AMOUNT_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_TAX_SOTO = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_TAX_UCHI = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.HINMEI_URIAGE_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.HINMEI_URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.HINMEI_URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_AMOUNT_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_AMOUNT_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_AMOUNT_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_TAX_SOTO = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_TAX_UCHI = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.HINMEI_SHIHARAI_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    shukkaEntryAdd.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_ZEI_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryAdd.URIAGE_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["URIAGE_TORIHIKI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_ZEI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    shukkaEntryAdd.SHIHARAI_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString());
                                }
                                //shukkaEntryAdd.TIME_STAMP = (byte[])(dr["TIME_STAMP"]);
                                // WHOカラム設定
                                dataBind_shukkaEntryAdd.SetSystemProperty(shukkaEntryAdd, false);
                                if (!String.IsNullOrEmpty(dr["DELETE_FLG"].ToString()))
                                {
                                    shukkaEntryAdd.DELETE_FLG = SqlBoolean.Parse(dr["DELETE_FLG"].ToString());
                                }
                                shukaEntryListAdd.Add(shukkaEntryAdd);
                                shukaEntryListUpdata.Add(shukkaEntryUpdata);
                            }

                            foreach (DataRow dr in dbDetail.Rows)
                            {
                                shukkaMs = new T_SHUKKA_DETAIL();
                                if (!String.IsNullOrEmpty(dr["SYSTEM_ID"].ToString()))
                                {
                                    shukkaMs.SYSTEM_ID = SqlInt64.Parse(dr["SYSTEM_ID"].ToString());
                                }

                                shukkaMs.SEQ = SqlInt32.Parse(saiban.ToString());

                                if (!String.IsNullOrEmpty(dr["DETAIL_SYSTEM_ID"].ToString()))
                                {
                                    shukkaMs.DETAIL_SYSTEM_ID = SqlInt64.Parse(dr["DETAIL_SYSTEM_ID"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHUKKA_NUMBER"].ToString()))
                                {
                                    shukkaMs.SHUKKA_NUMBER = SqlInt64.Parse(dr["SHUKKA_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["ROW_NO"].ToString()))
                                {
                                    shukkaMs.ROW_NO = SqlInt16.Parse(dr["ROW_NO"].ToString());
                                }
                                // 伝票単位の場合、全明細を未確定にする
                                if (this.msysinfo.SYS_KAKUTEI__TANNI_KBN.Equals(SqlInt16.Parse("1")))
                                {

                                    // 売上伝票の明細データは確定しない

                                    if (DENPYOU_KBN_SHIHARAI.Equals(dr["DENPYOU_KBN_CD"].ToString()))
                                    {
                                        // 伝票区分が支払の場合
                                        shukkaMs.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_MIKAKUTEI);
                                    }
                                    else
                                    {
                                        // 伝票区分が売上の場合
                                        if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                        {
                                            // 値を保持する
                                            shukkaMs.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    // 明細単位の場合は後でセットする
                                    if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                    {
                                        shukkaMs.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                    }
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGESHIHARAI_DATE"].ToString()))
                                {
                                    shukkaMs.URIAGESHIHARAI_DATE = SqlDateTime.Parse(dr["URIAGESHIHARAI_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["STACK_JYUURYOU"].ToString()))
                                {
                                    shukkaMs.STACK_JYUURYOU = SqlDecimal.Parse(dr["STACK_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["EMPTY_JYUURYOU"].ToString()))
                                {
                                    shukkaMs.EMPTY_JYUURYOU = SqlDecimal.Parse(dr["EMPTY_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["WARIFURI_JYUURYOU"].ToString()))
                                {
                                    shukkaMs.WARIFURI_JYUURYOU = SqlDecimal.Parse(dr["WARIFURI_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["WARIFURI_PERCENT"].ToString()))
                                {
                                    shukkaMs.WARIFURI_PERCENT = SqlDecimal.Parse(dr["WARIFURI_PERCENT"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CHOUSEI_JYUURYOU"].ToString()))
                                {
                                    shukkaMs.CHOUSEI_JYUURYOU = SqlInt64.Parse(dr["CHOUSEI_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CHOUSEI_PERCENT"].ToString()))
                                {
                                    shukkaMs.CHOUSEI_PERCENT = SqlDecimal.Parse(dr["CHOUSEI_PERCENT"].ToString());
                                }
                                shukkaMs.YOUKI_CD = dr["YOUKI_CD"].ToString();
                                if (!String.IsNullOrEmpty(dr["YOUKI_SUURYOU"].ToString()))
                                {
                                    shukkaMs.YOUKI_SUURYOU = SqlDecimal.Parse(dr["YOUKI_SUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["YOUKI_JYUURYOU"].ToString()))
                                {
                                    shukkaMs.YOUKI_JYUURYOU = SqlDecimal.Parse(dr["YOUKI_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DENPYOU_KBN_CD"].ToString()))
                                {
                                    shukkaMs.DENPYOU_KBN_CD = SqlInt16.Parse(dr["DENPYOU_KBN_CD"].ToString());
                                }
                                shukkaMs.HINMEI_CD = dr["HINMEI_CD"].ToString();
                                shukkaMs.HINMEI_NAME = dr["HINMEI_NAME"].ToString();
                                if (!String.IsNullOrEmpty(dr["NET_JYUURYOU"].ToString()))
                                {
                                    shukkaMs.NET_JYUURYOU = SqlInt64.Parse(dr["NET_JYUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SUURYOU"].ToString()))
                                {
                                    shukkaMs.SUURYOU = SqlDecimal.Parse(dr["SUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["UNIT_CD"].ToString()))
                                {
                                    shukkaMs.UNIT_CD = SqlInt16.Parse(dr["UNIT_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["TANKA"].ToString()))
                                {
                                    shukkaMs.TANKA = SqlDecimal.Parse(dr["TANKA"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KINGAKU"].ToString()))
                                {
                                    shukkaMs.KINGAKU = SqlDecimal.Parse(dr["KINGAKU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["TAX_SOTO"].ToString()))
                                {
                                    shukkaMs.TAX_SOTO = SqlDecimal.Parse(dr["TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["TAX_UCHI"].ToString()))
                                {
                                    shukkaMs.TAX_UCHI = SqlDecimal.Parse(dr["TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_ZEI_KBN_CD"].ToString()))
                                {
                                    shukkaMs.HINMEI_ZEI_KBN_CD = SqlInt16.Parse(dr["HINMEI_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_KINGAKU"].ToString()))
                                {
                                    shukkaMs.HINMEI_KINGAKU = SqlDecimal.Parse(dr["HINMEI_KINGAKU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_TAX_SOTO"].ToString()))
                                {
                                    shukkaMs.HINMEI_TAX_SOTO = SqlDecimal.Parse(dr["HINMEI_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_TAX_UCHI"].ToString()))
                                {
                                    shukkaMs.HINMEI_TAX_UCHI = SqlDecimal.Parse(dr["HINMEI_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MEISAI_BIKOU"].ToString()))
                                {
                                    shukkaMs.MEISAI_BIKOU = dr["MEISAI_BIKOU"].ToString();
                                }
                                if (!String.IsNullOrEmpty(dr["NISUGATA_SUURYOU"].ToString()))
                                {
                                    shukkaMs.NISUGATA_SUURYOU = SqlDecimal.Parse(dr["NISUGATA_SUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["NISUGATA_SUURYOU"].ToString()))
                                {
                                    shukkaMs.NISUGATA_UNIT_CD = SqlInt16.Parse(dr["NISUGATA_UNIT_CD"].ToString());
                                }
                                //shukkaMs.TIME_STAMP = (byte[])(dr["TIME_STAMP"]);
                                // WHOカラム設定
                                dataBind_shukkaMs.SetSystemProperty(shukkaMs, false);

                                shukaMsList.Add(shukkaMs);
                            }
                        }
                        foreach (T_SHUKKA_DETAIL item in shukaMsList)
                        {
                            if (item.SYSTEM_ID.ToString().Equals(dgvRow["システムID"].ToString())
                            && (item.SEQ - 1).ToString().Equals(dgvRow["枝番"].ToString())
                            && item.DETAIL_SYSTEM_ID.ToString().Equals(dgvRow["明細・システムID"].ToString()))
                            {
                                item.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_MIKAKUTEI);
                                //item.SEQ = SqlInt16.Parse(saiban.ToString());

                            }
                        }

                        row++;
                        systemidukeire = dgvRow["システムID"].ToString();
                        sequkeire = dgvRow["枝番"].ToString();
                    }
                }

                string syetemid = "";
                string seq = "";
                bool flag = false;
                int j = 1;
                foreach (T_SHUKKA_DETAIL item in shukaMsList)
                {
                    if (j == 1)
                    {
                        syetemid = item.SYSTEM_ID.ToString();
                        seq = item.SEQ.ToString();
                    }

                    if (item.SYSTEM_ID.ToString().Equals(syetemid) && item.SEQ.ToString().Equals(seq))
                    {
                        // 未確定ならtrue(NULLの場合があるので確定以外で判定)
                        if (!item.KAKUTEI_KBN.ToString().Equals(KAKUTEI_KBN_KAKUTEI))
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        foreach (T_SHUKKA_ENTRY itemEntry in shukaEntryListAdd)
                        {
                            if (SqlInt64.Parse(syetemid) == itemEntry.SYSTEM_ID && SqlInt64.Parse(seq) == itemEntry.SEQ)
                            {
                                if (flag == true)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_MIKAKUTEI);
                                }
                                else if (flag == false)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_KAKUTEI);
                                }
                            }

                        }
                        flag = false;
                    }
                    if (j == shukaMsList.Count)
                    {
                        // 未確定ならtrue(NULLの場合があるので確定以外で判定)
                        if (!item.KAKUTEI_KBN.ToString().Equals(KAKUTEI_KBN_KAKUTEI))
                        {
                            flag = true;
                        }


                        foreach (T_SHUKKA_ENTRY itemEntry in shukaEntryListAdd)
                        {
                            if (item.SYSTEM_ID == itemEntry.SYSTEM_ID && item.SEQ == itemEntry.SEQ)
                            {
                                if (flag == true)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_MIKAKUTEI);
                                }
                                else if (flag == false)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_KAKUTEI);
                                }
                            }

                        }
                        flag = false;

                    }
                    syetemid = item.SYSTEM_ID.ToString();
                    seq = item.SEQ.ToString();
                    j++;

                }
                this.mshukamslist = shukaMsList;
                this.mshukaentrylistadd = shukaEntryListAdd;
                this.mshukaentrylistupdata = shukaEntryListUpdata;
            }
        }

        /// <summary>
        /// 売上支払データ取得
        /// </summary>
        private void GetURSHData(string kubun, DataTable urshDt)
        {
            if (kubun == "TOUROKU")
            {
                List<T_UR_SH_DETAIL> urshMsList = new List<T_UR_SH_DETAIL>();
                List<T_UR_SH_ENTRY> urshEntryListAdd = new List<T_UR_SH_ENTRY>();
                List<T_UR_SH_ENTRY> urshEntryListUpdata = new List<T_UR_SH_ENTRY>();

                string syetemidursh = "";
                string seqursh = "";
                int row = 1;
                int saiban = 0;
                foreach (DataRow dgvRow in urshDt.Rows)
                {
                    if (dgvRow["明細・確定区分"].ToString().Equals("True") && dgvRow["情報確定利用区分"].ToString().Equals(KAKUTEI_USE_KBN_USE)
                        && !dgvRow["明細・確定区分"].ToString().Equals(dgvRow["比較確定区分"].ToString()))
                    {
                        DataTable dbDetail = new DataTable();
                        DataTable dbEntry = new DataTable();
                        T_UR_SH_DETAIL urshMs = new T_UR_SH_DETAIL();
                        T_UR_SH_ENTRY urshEntryAdd = new T_UR_SH_ENTRY();
                        T_UR_SH_ENTRY urshEntryUpdata = new T_UR_SH_ENTRY();
                        // WHOカラム設定共通ロジック呼び出し用
                        var dataBind_urshMs = new DataBinderLogic<T_UR_SH_DETAIL>(urshMs);
                        var dataBind_urshEntryAdd = new DataBinderLogic<T_UR_SH_ENTRY>(urshEntryAdd);
                        var dataBind_urshEntryUpdata = new DataBinderLogic<T_UR_SH_ENTRY>(urshEntryUpdata);

                        if (!dgvRow["システムID"].ToString().Equals(syetemidursh) || !dgvRow["枝番"].ToString().Equals(seqursh))
                        {
                            dbDetail = SearchUrShDetail(dgvRow["システムID"].ToString(), dgvRow["枝番"].ToString());
                            dbEntry = SearchUrShEntry(dgvRow["システムID"].ToString(), dgvRow["枝番"].ToString());

                            if (!dgvRow["システムID"].ToString().Equals(syetemidursh))
                            {
                                saiban = SaiBan("T_UR_SH_ENTRY", dgvRow["システムID"].ToString()) + 1;
                            }
                            else if (dgvRow["システムID"].ToString().Equals(syetemidursh) && !dgvRow["枝番"].ToString().Equals(seqursh))
                            {
                                saiban = saiban + 1;
                            }

                            foreach (DataRow dr in dbEntry.Rows)
                            {
                                urshEntryUpdata = new T_UR_SH_ENTRY();
                                if (!String.IsNullOrEmpty(dr["SYSTEM_ID"].ToString()))
                                {
                                    urshEntryUpdata.SYSTEM_ID = SqlInt64.Parse(dr["SYSTEM_ID"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SEQ"].ToString()))
                                {
                                    urshEntryUpdata.SEQ = SqlInt32.Parse(dr["SEQ"].ToString());
                                }
                                //**********
                                if (!String.IsNullOrEmpty(dr["KYOTEN_CD"].ToString()))
                                {
                                    urshEntryUpdata.KYOTEN_CD = SqlInt16.Parse(dr["KYOTEN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["UR_SH_NUMBER"].ToString()))
                                {
                                    urshEntryUpdata.UR_SH_NUMBER = SqlInt64.Parse(dr["UR_SH_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DATE_NUMBER"].ToString()))
                                {
                                    urshEntryUpdata.DATE_NUMBER = SqlInt32.Parse(dr["DATE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["YEAR_NUMBER"].ToString()))
                                {
                                    urshEntryUpdata.YEAR_NUMBER = SqlInt32.Parse(dr["YEAR_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                {
                                    urshEntryUpdata.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DENPYOU_DATE"].ToString()))
                                {
                                    urshEntryUpdata.DENPYOU_DATE = SqlDateTime.Parse(dr["DENPYOU_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_DATE"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_DATE = SqlDateTime.Parse(dr["URIAGE_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_DATE"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_DATE = SqlDateTime.Parse(dr["SHIHARAI_DATE"].ToString());
                                }
                                urshEntryUpdata.TORIHIKISAKI_CD = dr["TORIHIKISAKI_CD"].ToString();
                                urshEntryUpdata.TORIHIKISAKI_NAME = dr["TORIHIKISAKI_NAME"].ToString();
                                urshEntryUpdata.GYOUSHA_CD = dr["GYOUSHA_CD"].ToString();
                                urshEntryUpdata.GYOUSHA_NAME = dr["GYOUSHA_NAME"].ToString();
                                urshEntryUpdata.GENBA_CD = dr["GENBA_CD"].ToString();
                                urshEntryUpdata.GENBA_NAME = dr["GENBA_NAME"].ToString();
                                urshEntryUpdata.NIZUMI_GYOUSHA_CD = dr["NIZUMI_GYOUSHA_CD"].ToString();
                                urshEntryUpdata.NIZUMI_GYOUSHA_NAME = dr["NIZUMI_GYOUSHA_NAME"].ToString();
                                urshEntryUpdata.NIZUMI_GENBA_CD = dr["NIZUMI_GENBA_CD"].ToString();
                                urshEntryUpdata.NIZUMI_GENBA_NAME = dr["NIZUMI_GENBA_NAME"].ToString();
                                urshEntryUpdata.NIOROSHI_GYOUSHA_CD = dr["NIOROSHI_GYOUSHA_CD"].ToString();
                                urshEntryUpdata.NIOROSHI_GYOUSHA_NAME = dr["NIOROSHI_GYOUSHA_NAME"].ToString();
                                urshEntryUpdata.NIOROSHI_GENBA_CD = dr["NIOROSHI_GENBA_CD"].ToString();
                                urshEntryUpdata.NIOROSHI_GENBA_NAME = dr["NIOROSHI_GENBA_NAME"].ToString();
                                urshEntryUpdata.EIGYOU_TANTOUSHA_CD = dr["EIGYOU_TANTOUSHA_CD"].ToString();
                                urshEntryUpdata.EIGYOU_TANTOUSHA_NAME = dr["EIGYOU_TANTOUSHA_NAME"].ToString();
                                urshEntryUpdata.NYUURYOKU_TANTOUSHA_CD = dr["NYUURYOKU_TANTOUSHA_CD"].ToString();
                                urshEntryUpdata.NYUURYOKU_TANTOUSHA_NAME = dr["NYUURYOKU_TANTOUSHA_NAME"].ToString();
                                urshEntryUpdata.SHARYOU_CD = dr["SHARYOU_CD"].ToString();
                                urshEntryUpdata.SHARYOU_NAME = dr["SHARYOU_NAME"].ToString();
                                urshEntryUpdata.SHASHU_CD = dr["SHASHU_CD"].ToString();
                                urshEntryUpdata.SHASHU_NAME = dr["SHASHU_NAME"].ToString();
                                urshEntryUpdata.UNPAN_GYOUSHA_CD = dr["UNPAN_GYOUSHA_CD"].ToString();
                                urshEntryUpdata.UNPAN_GYOUSHA_NAME = dr["UNPAN_GYOUSHA_NAME"].ToString();
                                urshEntryUpdata.UNTENSHA_CD = dr["UNTENSHA_CD"].ToString();
                                urshEntryUpdata.UNTENSHA_NAME = dr["UNTENSHA_NAME"].ToString();
                                if (!String.IsNullOrEmpty(dr["NINZUU_CNT"].ToString()))
                                {
                                    urshEntryUpdata.NINZUU_CNT = SqlInt16.Parse(dr["NINZUU_CNT"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEITAI_KBN_CD"].ToString()))
                                {
                                    urshEntryUpdata.KEITAI_KBN_CD = SqlInt16.Parse(dr["KEITAI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CONTENA_SOUSA_CD"].ToString()))
                                {
                                    urshEntryUpdata.CONTENA_SOUSA_CD = SqlInt16.Parse(dr["CONTENA_SOUSA_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_SHURUI_CD"].ToString()))
                                {
                                    urshEntryUpdata.MANIFEST_SHURUI_CD = SqlInt16.Parse(dr["MANIFEST_SHURUI_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_TEHAI_CD"].ToString()))
                                {
                                    urshEntryUpdata.MANIFEST_TEHAI_CD = SqlInt16.Parse(dr["MANIFEST_TEHAI_CD"].ToString());
                                }
                                urshEntryUpdata.DENPYOU_BIKOU = dr["DENPYOU_BIKOU"].ToString();
                                if (!String.IsNullOrEmpty(dr["UKETSUKE_NUMBER"].ToString()))
                                {
                                    urshEntryUpdata.UKETSUKE_NUMBER = SqlInt64.Parse(dr["UKETSUKE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["RECEIPT_NUMBER"].ToString()))
                                {
                                    urshEntryUpdata.RECEIPT_NUMBER = SqlInt32.Parse(dr["RECEIPT_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_SHOUHIZEI_RATE"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["URIAGE_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_AMOUNT_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_AMOUNT_TOTAL = SqlDecimal.Parse(dr["URIAGE_AMOUNT_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_TAX_SOTO = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_TAX_UCHI = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.HINMEI_URIAGE_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.HINMEI_URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.HINMEI_URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_AMOUNT_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_AMOUNT_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_AMOUNT_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_TAX_SOTO = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_TAX_UCHI = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.HINMEI_SHIHARAI_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KBN_CD"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_ZEI_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["URIAGE_TORIHIKI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KBN_CD"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_ZEI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString());
                                }
                                urshEntryUpdata.TIME_STAMP = (byte[])(dr["TIME_STAMP"]);
                                //************
                                // WHOカラム設定
                                dataBind_urshEntryUpdata.SetSystemProperty(urshEntryUpdata, false);
                                urshEntryUpdata.DELETE_FLG = SqlBoolean.True;

                                // 登録用データ
                                urshEntryAdd = new T_UR_SH_ENTRY();
                                if (!String.IsNullOrEmpty(dr["SYSTEM_ID"].ToString()))
                                {
                                    urshEntryAdd.SYSTEM_ID = SqlInt64.Parse(dr["SYSTEM_ID"].ToString());
                                }
                                urshEntryAdd.SEQ = SqlInt32.Parse(saiban.ToString());
                                if (!String.IsNullOrEmpty(dr["KYOTEN_CD"].ToString()))
                                {
                                    urshEntryAdd.KYOTEN_CD = SqlInt16.Parse(dr["KYOTEN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["UR_SH_NUMBER"].ToString()))
                                {
                                    urshEntryAdd.UR_SH_NUMBER = SqlInt64.Parse(dr["UR_SH_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DATE_NUMBER"].ToString()))
                                {
                                    urshEntryAdd.DATE_NUMBER = SqlInt32.Parse(dr["DATE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["YEAR_NUMBER"].ToString()))
                                {
                                    urshEntryAdd.YEAR_NUMBER = SqlInt32.Parse(dr["YEAR_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                {
                                    urshEntryAdd.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DENPYOU_DATE"].ToString()))
                                {
                                    urshEntryAdd.DENPYOU_DATE = SqlDateTime.Parse(dr["DENPYOU_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_DATE"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_DATE = SqlDateTime.Parse(dr["URIAGE_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_DATE"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_DATE = SqlDateTime.Parse(dr["SHIHARAI_DATE"].ToString());
                                }
                                urshEntryAdd.TORIHIKISAKI_CD = dr["TORIHIKISAKI_CD"].ToString();
                                urshEntryAdd.TORIHIKISAKI_NAME = dr["TORIHIKISAKI_NAME"].ToString();
                                urshEntryAdd.GYOUSHA_CD = dr["GYOUSHA_CD"].ToString();
                                urshEntryAdd.GYOUSHA_NAME = dr["GYOUSHA_NAME"].ToString();
                                urshEntryAdd.GENBA_CD = dr["GENBA_CD"].ToString();
                                urshEntryAdd.GENBA_NAME = dr["GENBA_NAME"].ToString();
                                urshEntryAdd.NIZUMI_GYOUSHA_CD = dr["NIZUMI_GYOUSHA_CD"].ToString();
                                urshEntryAdd.NIZUMI_GYOUSHA_NAME = dr["NIZUMI_GYOUSHA_NAME"].ToString();
                                urshEntryAdd.NIZUMI_GENBA_CD = dr["NIZUMI_GENBA_CD"].ToString();
                                urshEntryAdd.NIZUMI_GENBA_NAME = dr["NIZUMI_GENBA_NAME"].ToString();
                                urshEntryAdd.NIOROSHI_GYOUSHA_CD = dr["NIOROSHI_GYOUSHA_CD"].ToString();
                                urshEntryAdd.NIOROSHI_GYOUSHA_NAME = dr["NIOROSHI_GYOUSHA_NAME"].ToString();
                                urshEntryAdd.NIOROSHI_GENBA_CD = dr["NIOROSHI_GENBA_CD"].ToString();
                                urshEntryAdd.NIOROSHI_GENBA_NAME = dr["NIOROSHI_GENBA_NAME"].ToString();
                                urshEntryAdd.EIGYOU_TANTOUSHA_CD = dr["EIGYOU_TANTOUSHA_CD"].ToString();
                                urshEntryAdd.EIGYOU_TANTOUSHA_NAME = dr["EIGYOU_TANTOUSHA_NAME"].ToString();
                                urshEntryAdd.NYUURYOKU_TANTOUSHA_CD = dr["NYUURYOKU_TANTOUSHA_CD"].ToString();
                                urshEntryAdd.NYUURYOKU_TANTOUSHA_NAME = dr["NYUURYOKU_TANTOUSHA_NAME"].ToString();
                                urshEntryAdd.SHARYOU_CD = dr["SHARYOU_CD"].ToString();
                                urshEntryAdd.SHARYOU_NAME = dr["SHARYOU_NAME"].ToString();
                                urshEntryAdd.SHASHU_CD = dr["SHASHU_CD"].ToString();
                                urshEntryAdd.SHASHU_NAME = dr["SHASHU_NAME"].ToString();
                                urshEntryAdd.UNPAN_GYOUSHA_CD = dr["UNPAN_GYOUSHA_CD"].ToString();
                                urshEntryAdd.UNPAN_GYOUSHA_NAME = dr["UNPAN_GYOUSHA_NAME"].ToString();
                                urshEntryAdd.UNTENSHA_CD = dr["UNTENSHA_CD"].ToString();
                                urshEntryAdd.UNTENSHA_NAME = dr["UNTENSHA_NAME"].ToString();
                                if (!String.IsNullOrEmpty(dr["NINZUU_CNT"].ToString()))
                                {
                                    urshEntryAdd.NINZUU_CNT = SqlInt16.Parse(dr["NINZUU_CNT"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEITAI_KBN_CD"].ToString()))
                                {
                                    urshEntryAdd.KEITAI_KBN_CD = SqlInt16.Parse(dr["KEITAI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CONTENA_SOUSA_CD"].ToString()))
                                {
                                    urshEntryAdd.CONTENA_SOUSA_CD = SqlInt16.Parse(dr["CONTENA_SOUSA_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_SHURUI_CD"].ToString()))
                                {
                                    urshEntryAdd.MANIFEST_SHURUI_CD = SqlInt16.Parse(dr["MANIFEST_SHURUI_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_TEHAI_CD"].ToString()))
                                {
                                    urshEntryAdd.MANIFEST_TEHAI_CD = SqlInt16.Parse(dr["MANIFEST_TEHAI_CD"].ToString());
                                }
                                urshEntryAdd.DENPYOU_BIKOU = dr["DENPYOU_BIKOU"].ToString();
                                if (!String.IsNullOrEmpty(dr["UKETSUKE_NUMBER"].ToString()))
                                {
                                    urshEntryAdd.UKETSUKE_NUMBER = SqlInt64.Parse(dr["UKETSUKE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["RECEIPT_NUMBER"].ToString()))
                                {
                                    urshEntryAdd.RECEIPT_NUMBER = SqlInt32.Parse(dr["RECEIPT_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_SHOUHIZEI_RATE"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["URIAGE_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_AMOUNT_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_AMOUNT_TOTAL = SqlDecimal.Parse(dr["URIAGE_AMOUNT_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_TAX_SOTO = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_TAX_UCHI = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.HINMEI_URIAGE_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.HINMEI_URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.HINMEI_URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_AMOUNT_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_AMOUNT_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_AMOUNT_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_TAX_SOTO = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_TAX_UCHI = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.HINMEI_SHIHARAI_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KBN_CD"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_ZEI_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["URIAGE_TORIHIKI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KBN_CD"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_ZEI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString());
                                }
                                //urshEntryAdd.TIME_STAMP = (byte[])(dr["TIME_STAMP"]);
                                // WHOカラム設定
                                dataBind_urshEntryAdd.SetSystemProperty(urshEntryAdd, false);
                                if (!String.IsNullOrEmpty(dr["DELETE_FLG"].ToString()))
                                {
                                    urshEntryAdd.DELETE_FLG = SqlBoolean.Parse(dr["DELETE_FLG"].ToString());
                                }
                                urshEntryListAdd.Add(urshEntryAdd);
                                urshEntryListUpdata.Add(urshEntryUpdata);
                            }

                            foreach (DataRow dr in dbDetail.Rows)
                            {
                                urshMs = new T_UR_SH_DETAIL();
                                if (!String.IsNullOrEmpty(dr["SYSTEM_ID"].ToString()))
                                {
                                    urshMs.SYSTEM_ID = SqlInt64.Parse(dr["SYSTEM_ID"].ToString());
                                }

                                urshMs.SEQ = SqlInt32.Parse(saiban.ToString());

                                if (!String.IsNullOrEmpty(dr["DETAIL_SYSTEM_ID"].ToString()))
                                {
                                    urshMs.DETAIL_SYSTEM_ID = SqlInt64.Parse(dr["DETAIL_SYSTEM_ID"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["UR_SH_NUMBER"].ToString()))
                                {
                                    urshMs.UR_SH_NUMBER = SqlInt64.Parse(dr["UR_SH_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["ROW_NO"].ToString()))
                                {
                                    urshMs.ROW_NO = SqlInt16.Parse(dr["ROW_NO"].ToString());
                                }
                                // 伝票単位の場合、全明細を確定にする
                                if (this.msysinfo.SYS_KAKUTEI__TANNI_KBN.Equals(SqlInt16.Parse("1")))
                                {

                                    // 売上伝票の明細データは確定しない

                                    if (DENPYOU_KBN_SHIHARAI.Equals(dr["DENPYOU_KBN_CD"].ToString()))
                                    {
                                        // 伝票区分が支払の場合
                                        urshMs.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_KAKUTEI);
                                    }
                                    else
                                    {
                                        // 伝票区分が売上の場合
                                        if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                        {
                                            // 値を保持する
                                            urshMs.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    // 明細単位の場合は後でセットする
                                    if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                    {
                                        urshMs.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                    }
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGESHIHARAI_DATE"].ToString()))
                                {
                                    urshMs.URIAGESHIHARAI_DATE = SqlDateTime.Parse(dr["URIAGESHIHARAI_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DENPYOU_KBN_CD"].ToString()))
                                {
                                    urshMs.DENPYOU_KBN_CD = SqlInt16.Parse(dr["DENPYOU_KBN_CD"].ToString());
                                }
                                urshMs.HINMEI_CD = dr["HINMEI_CD"].ToString();
                                urshMs.HINMEI_NAME = dr["HINMEI_NAME"].ToString();
                                if (!String.IsNullOrEmpty(dr["SUURYOU"].ToString()))
                                {
                                    urshMs.SUURYOU = SqlDecimal.Parse(dr["SUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["UNIT_CD"].ToString()))
                                {
                                    urshMs.UNIT_CD = SqlInt16.Parse(dr["UNIT_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["TANKA"].ToString()))
                                {
                                    urshMs.TANKA = SqlDecimal.Parse(dr["TANKA"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KINGAKU"].ToString()))
                                {
                                    urshMs.KINGAKU = SqlDecimal.Parse(dr["KINGAKU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["TAX_SOTO"].ToString()))
                                {
                                    urshMs.TAX_SOTO = SqlDecimal.Parse(dr["TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["TAX_UCHI"].ToString()))
                                {
                                    urshMs.TAX_UCHI = SqlDecimal.Parse(dr["TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_ZEI_KBN_CD"].ToString()))
                                {
                                    urshMs.HINMEI_ZEI_KBN_CD = SqlInt16.Parse(dr["HINMEI_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_KINGAKU"].ToString()))
                                {
                                    urshMs.HINMEI_KINGAKU = SqlDecimal.Parse(dr["HINMEI_KINGAKU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_TAX_SOTO"].ToString()))
                                {
                                    urshMs.HINMEI_TAX_SOTO = SqlDecimal.Parse(dr["HINMEI_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_TAX_UCHI"].ToString()))
                                {
                                    urshMs.HINMEI_TAX_UCHI = SqlDecimal.Parse(dr["HINMEI_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MEISAI_BIKOU"].ToString()))
                                {
                                    urshMs.MEISAI_BIKOU = dr["MEISAI_BIKOU"].ToString();
                                }
                                if (!String.IsNullOrEmpty(dr["NISUGATA_SUURYOU"].ToString()))
                                {
                                    urshMs.NISUGATA_SUURYOU = SqlDecimal.Parse(dr["NISUGATA_SUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["NISUGATA_UNIT_CD"].ToString()))
                                {
                                    urshMs.NISUGATA_UNIT_CD = SqlInt16.Parse(dr["NISUGATA_UNIT_CD"].ToString());
                                }
                                //urshMs.TIME_STAMP = (byte[])(dr["TIME_STAMP"]);
                                // WHOカラム設定
                                dataBind_urshMs.SetSystemProperty(urshMs, false);

                                urshMsList.Add(urshMs);
                            }
                        }
                        foreach (T_UR_SH_DETAIL item in urshMsList)
                        {
                            if (item.SYSTEM_ID.ToString().Equals(dgvRow["システムID"].ToString())
                            && (item.SEQ - 1).ToString().Equals(dgvRow["枝番"].ToString())
                            && item.DETAIL_SYSTEM_ID.ToString().Equals(dgvRow["明細・システムID"].ToString()))
                            {
                                item.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_KAKUTEI);
                                //item.SEQ = SqlInt16.Parse(saiban.ToString());
                            }

                        }


                        row++;
                        syetemidursh = dgvRow["システムID"].ToString();
                        seqursh = dgvRow["枝番"].ToString();
                    }
                }

                string syetemid = "";
                string seq = "";
                bool flag = false;
                int j = 1;
                foreach (T_UR_SH_DETAIL item in urshMsList)
                {
                    if (j == 1)
                    {
                        syetemid = item.SYSTEM_ID.ToString();
                        seq = item.SEQ.ToString();
                    }

                    if (item.SYSTEM_ID.ToString().Equals(syetemid) && item.SEQ.ToString().Equals(seq))
                    {
                        // 未確定ならtrue(NULLの場合があるので確定以外で判定)
                        if (!item.KAKUTEI_KBN.ToString().Equals(KAKUTEI_KBN_KAKUTEI))
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        foreach (T_UR_SH_ENTRY itemEntry in urshEntryListAdd)
                        {
                            if (SqlInt64.Parse(syetemid) == itemEntry.SYSTEM_ID && SqlInt64.Parse(seq) == itemEntry.SEQ)
                            {
                                if (flag == true)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_MIKAKUTEI);
                                }
                                else if (flag == false)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_KAKUTEI);
                                }
                            }

                        }
                        flag = false;
                    }
                    if (j == urshMsList.Count)
                    {
                        // 未確定ならtrue(NULLの場合があるので確定以外で判定)
                        if (!item.KAKUTEI_KBN.ToString().Equals(KAKUTEI_KBN_KAKUTEI))
                        {
                            flag = true;
                        }


                        foreach (T_UR_SH_ENTRY itemEntry in urshEntryListAdd)
                        {
                            if (item.SYSTEM_ID == itemEntry.SYSTEM_ID && item.SEQ == itemEntry.SEQ)
                            {
                                if (flag == true)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_MIKAKUTEI);
                                }
                                else if (flag == false)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_KAKUTEI);
                                }
                            }

                        }
                        flag = false;

                    }
                    syetemid = item.SYSTEM_ID.ToString();
                    seq = item.SEQ.ToString();
                    j++;

                }
                this.murshmslist = urshMsList;
                this.murshentrylistadd = urshEntryListAdd;
                this.murshentrylistupdata = urshEntryListUpdata;
            }
            else if (kubun == "KAIJYO")
            {
                List<T_UR_SH_DETAIL> urshMsList = new List<T_UR_SH_DETAIL>();
                List<T_UR_SH_ENTRY> urshEntryListAdd = new List<T_UR_SH_ENTRY>();
                List<T_UR_SH_ENTRY> urshEntryListUpdata = new List<T_UR_SH_ENTRY>();


                string syetemidursh = "";
                string seqursh = "";
                int row = 1;
                int saiban = 0;
                foreach (DataRow dgvRow in urshDt.Rows)
                {
                    if (dgvRow["明細・確定区分"].ToString().Equals("False") && dgvRow["情報確定利用区分"].ToString().Equals(KAKUTEI_USE_KBN_USE)
                        && !dgvRow["明細・確定区分"].ToString().Equals(dgvRow["比較確定区分"].ToString()))
                    {
                        DataTable dbDetail = new DataTable();
                        DataTable dbEntry = new DataTable();
                        T_UR_SH_DETAIL urshMs = new T_UR_SH_DETAIL();
                        T_UR_SH_ENTRY urshEntryAdd = new T_UR_SH_ENTRY();
                        T_UR_SH_ENTRY urshEntryUpdata = new T_UR_SH_ENTRY();
                        // WHOカラム設定共通ロジック呼び出し用
                        var dataBind_urshMs = new DataBinderLogic<T_UR_SH_DETAIL>(urshMs);
                        var dataBind_urshEntryAdd = new DataBinderLogic<T_UR_SH_ENTRY>(urshEntryAdd);
                        var dataBind_urshEntryUpdata = new DataBinderLogic<T_UR_SH_ENTRY>(urshEntryUpdata);

                        if (!dgvRow["システムID"].ToString().Equals(syetemidursh) || !dgvRow["枝番"].ToString().Equals(seqursh))
                        {
                            dbDetail = SearchUrShDetail(dgvRow["システムID"].ToString(), dgvRow["枝番"].ToString());
                            dbEntry = SearchUrShEntry(dgvRow["システムID"].ToString(), dgvRow["枝番"].ToString());

                            if (!dgvRow["システムID"].ToString().Equals(syetemidursh))
                            {
                                saiban = SaiBan("T_UR_SH_ENTRY", dgvRow["システムID"].ToString()) + 1;
                            }
                            else if (dgvRow["システムID"].ToString().Equals(syetemidursh) && !dgvRow["枝番"].ToString().Equals(seqursh))
                            {
                                saiban = saiban + 1;
                            }

                            foreach (DataRow dr in dbEntry.Rows)
                            {
                                urshEntryUpdata = new T_UR_SH_ENTRY();
                                if (!String.IsNullOrEmpty(dr["SYSTEM_ID"].ToString()))
                                {
                                    urshEntryUpdata.SYSTEM_ID = SqlInt64.Parse(dr["SYSTEM_ID"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SEQ"].ToString()))
                                {
                                    urshEntryUpdata.SEQ = SqlInt32.Parse(dr["SEQ"].ToString());
                                }
                                //*************
                                if (!String.IsNullOrEmpty(dr["KYOTEN_CD"].ToString()))
                                {
                                    urshEntryUpdata.KYOTEN_CD = SqlInt16.Parse(dr["KYOTEN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["UR_SH_NUMBER"].ToString()))
                                {
                                    urshEntryUpdata.UR_SH_NUMBER = SqlInt64.Parse(dr["UR_SH_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DATE_NUMBER"].ToString()))
                                {
                                    urshEntryUpdata.DATE_NUMBER = SqlInt32.Parse(dr["DATE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["YEAR_NUMBER"].ToString()))
                                {
                                    urshEntryUpdata.YEAR_NUMBER = SqlInt32.Parse(dr["YEAR_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                {
                                    urshEntryUpdata.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DENPYOU_DATE"].ToString()))
                                {
                                    urshEntryUpdata.DENPYOU_DATE = SqlDateTime.Parse(dr["DENPYOU_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_DATE"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_DATE = SqlDateTime.Parse(dr["URIAGE_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_DATE"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_DATE = SqlDateTime.Parse(dr["SHIHARAI_DATE"].ToString());
                                }
                                urshEntryUpdata.TORIHIKISAKI_CD = dr["TORIHIKISAKI_CD"].ToString();
                                urshEntryUpdata.TORIHIKISAKI_NAME = dr["TORIHIKISAKI_NAME"].ToString();
                                urshEntryUpdata.GYOUSHA_CD = dr["GYOUSHA_CD"].ToString();
                                urshEntryUpdata.GYOUSHA_NAME = dr["GYOUSHA_NAME"].ToString();
                                urshEntryUpdata.GENBA_CD = dr["GENBA_CD"].ToString();
                                urshEntryUpdata.GENBA_NAME = dr["GENBA_NAME"].ToString();
                                urshEntryUpdata.NIZUMI_GYOUSHA_CD = dr["NIZUMI_GYOUSHA_CD"].ToString();
                                urshEntryUpdata.NIZUMI_GYOUSHA_NAME = dr["NIZUMI_GYOUSHA_NAME"].ToString();
                                urshEntryUpdata.NIZUMI_GENBA_CD = dr["NIZUMI_GENBA_CD"].ToString();
                                urshEntryUpdata.NIZUMI_GENBA_NAME = dr["NIZUMI_GENBA_NAME"].ToString();
                                urshEntryUpdata.NIOROSHI_GYOUSHA_CD = dr["NIOROSHI_GYOUSHA_CD"].ToString();
                                urshEntryUpdata.NIOROSHI_GYOUSHA_NAME = dr["NIOROSHI_GYOUSHA_NAME"].ToString();
                                urshEntryUpdata.NIOROSHI_GENBA_CD = dr["NIOROSHI_GENBA_CD"].ToString();
                                urshEntryUpdata.NIOROSHI_GENBA_NAME = dr["NIOROSHI_GENBA_NAME"].ToString();
                                urshEntryUpdata.EIGYOU_TANTOUSHA_CD = dr["EIGYOU_TANTOUSHA_CD"].ToString();
                                urshEntryUpdata.EIGYOU_TANTOUSHA_NAME = dr["EIGYOU_TANTOUSHA_NAME"].ToString();
                                urshEntryUpdata.NYUURYOKU_TANTOUSHA_CD = dr["NYUURYOKU_TANTOUSHA_CD"].ToString();
                                urshEntryUpdata.NYUURYOKU_TANTOUSHA_NAME = dr["NYUURYOKU_TANTOUSHA_NAME"].ToString();
                                urshEntryUpdata.SHARYOU_CD = dr["SHARYOU_CD"].ToString();
                                urshEntryUpdata.SHARYOU_NAME = dr["SHARYOU_NAME"].ToString();
                                urshEntryUpdata.SHASHU_CD = dr["SHASHU_CD"].ToString();
                                urshEntryUpdata.SHASHU_NAME = dr["SHASHU_NAME"].ToString();
                                urshEntryUpdata.UNPAN_GYOUSHA_CD = dr["UNPAN_GYOUSHA_CD"].ToString();
                                urshEntryUpdata.UNPAN_GYOUSHA_NAME = dr["UNPAN_GYOUSHA_NAME"].ToString();
                                urshEntryUpdata.UNTENSHA_CD = dr["UNTENSHA_CD"].ToString();
                                urshEntryUpdata.UNTENSHA_NAME = dr["UNTENSHA_NAME"].ToString();
                                if (!String.IsNullOrEmpty(dr["NINZUU_CNT"].ToString()))
                                {
                                    urshEntryUpdata.NINZUU_CNT = SqlInt16.Parse(dr["NINZUU_CNT"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEITAI_KBN_CD"].ToString()))
                                {
                                    urshEntryUpdata.KEITAI_KBN_CD = SqlInt16.Parse(dr["KEITAI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CONTENA_SOUSA_CD"].ToString()))
                                {
                                    urshEntryUpdata.CONTENA_SOUSA_CD = SqlInt16.Parse(dr["CONTENA_SOUSA_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_SHURUI_CD"].ToString()))
                                {
                                    urshEntryUpdata.MANIFEST_SHURUI_CD = SqlInt16.Parse(dr["MANIFEST_SHURUI_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_TEHAI_CD"].ToString()))
                                {
                                    urshEntryUpdata.MANIFEST_TEHAI_CD = SqlInt16.Parse(dr["MANIFEST_TEHAI_CD"].ToString());
                                }
                                urshEntryUpdata.DENPYOU_BIKOU = dr["DENPYOU_BIKOU"].ToString();
                                if (!String.IsNullOrEmpty(dr["UKETSUKE_NUMBER"].ToString()))
                                {
                                    urshEntryUpdata.UKETSUKE_NUMBER = SqlInt64.Parse(dr["UKETSUKE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["RECEIPT_NUMBER"].ToString()))
                                {
                                    urshEntryUpdata.RECEIPT_NUMBER = SqlInt32.Parse(dr["RECEIPT_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_SHOUHIZEI_RATE"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["URIAGE_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_AMOUNT_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_AMOUNT_TOTAL = SqlDecimal.Parse(dr["URIAGE_AMOUNT_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_TAX_SOTO = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_TAX_UCHI = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.HINMEI_URIAGE_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.HINMEI_URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.HINMEI_URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_AMOUNT_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_AMOUNT_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_AMOUNT_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_TAX_SOTO = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_TAX_UCHI = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.HINMEI_SHIHARAI_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    urshEntryUpdata.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KBN_CD"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_ZEI_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    urshEntryUpdata.URIAGE_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["URIAGE_TORIHIKI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KBN_CD"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_ZEI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    urshEntryUpdata.SHIHARAI_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString());
                                }
                                urshEntryUpdata.TIME_STAMP = (byte[])(dr["TIME_STAMP"]);
                                //*************
                                // WHOカラム設定
                                dataBind_urshEntryUpdata.SetSystemProperty(urshEntryUpdata, false);
                                urshEntryUpdata.DELETE_FLG = SqlBoolean.True;

                                urshEntryAdd = new T_UR_SH_ENTRY();
                                if (!String.IsNullOrEmpty(dr["SYSTEM_ID"].ToString()))
                                {
                                    urshEntryAdd.SYSTEM_ID = SqlInt64.Parse(dr["SYSTEM_ID"].ToString());
                                }
                                urshEntryAdd.SEQ = SqlInt32.Parse(saiban.ToString());
                                if (!String.IsNullOrEmpty(dr["KYOTEN_CD"].ToString()))
                                {
                                    urshEntryAdd.KYOTEN_CD = SqlInt16.Parse(dr["KYOTEN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["UR_SH_NUMBER"].ToString()))
                                {
                                    urshEntryAdd.UR_SH_NUMBER = SqlInt64.Parse(dr["UR_SH_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DATE_NUMBER"].ToString()))
                                {
                                    urshEntryAdd.DATE_NUMBER = SqlInt32.Parse(dr["DATE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["YEAR_NUMBER"].ToString()))
                                {
                                    urshEntryAdd.YEAR_NUMBER = SqlInt32.Parse(dr["YEAR_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                {
                                    urshEntryAdd.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DENPYOU_DATE"].ToString()))
                                {
                                    urshEntryAdd.DENPYOU_DATE = SqlDateTime.Parse(dr["DENPYOU_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_DATE"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_DATE = SqlDateTime.Parse(dr["URIAGE_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_DATE"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_DATE = SqlDateTime.Parse(dr["SHIHARAI_DATE"].ToString());
                                }
                                urshEntryAdd.TORIHIKISAKI_CD = dr["TORIHIKISAKI_CD"].ToString();
                                urshEntryAdd.TORIHIKISAKI_NAME = dr["TORIHIKISAKI_NAME"].ToString();
                                urshEntryAdd.GYOUSHA_CD = dr["GYOUSHA_CD"].ToString();
                                urshEntryAdd.GYOUSHA_NAME = dr["GYOUSHA_NAME"].ToString();
                                urshEntryAdd.GENBA_CD = dr["GENBA_CD"].ToString();
                                urshEntryAdd.GENBA_NAME = dr["GENBA_NAME"].ToString();
                                urshEntryAdd.NIZUMI_GYOUSHA_CD = dr["NIZUMI_GYOUSHA_CD"].ToString();
                                urshEntryAdd.NIZUMI_GYOUSHA_NAME = dr["NIZUMI_GYOUSHA_NAME"].ToString();
                                urshEntryAdd.NIZUMI_GENBA_CD = dr["NIZUMI_GENBA_CD"].ToString();
                                urshEntryAdd.NIZUMI_GENBA_NAME = dr["NIZUMI_GENBA_NAME"].ToString();
                                urshEntryAdd.NIOROSHI_GYOUSHA_CD = dr["NIOROSHI_GYOUSHA_CD"].ToString();
                                urshEntryAdd.NIOROSHI_GYOUSHA_NAME = dr["NIOROSHI_GYOUSHA_NAME"].ToString();
                                urshEntryAdd.NIOROSHI_GENBA_CD = dr["NIOROSHI_GENBA_CD"].ToString();
                                urshEntryAdd.NIOROSHI_GENBA_NAME = dr["NIOROSHI_GENBA_NAME"].ToString();
                                urshEntryAdd.EIGYOU_TANTOUSHA_CD = dr["EIGYOU_TANTOUSHA_CD"].ToString();
                                urshEntryAdd.EIGYOU_TANTOUSHA_NAME = dr["EIGYOU_TANTOUSHA_NAME"].ToString();
                                urshEntryAdd.NYUURYOKU_TANTOUSHA_CD = dr["NYUURYOKU_TANTOUSHA_CD"].ToString();
                                urshEntryAdd.NYUURYOKU_TANTOUSHA_NAME = dr["NYUURYOKU_TANTOUSHA_NAME"].ToString();
                                urshEntryAdd.SHARYOU_CD = dr["SHARYOU_CD"].ToString();
                                urshEntryAdd.SHARYOU_NAME = dr["SHARYOU_NAME"].ToString();
                                urshEntryAdd.SHASHU_CD = dr["SHASHU_CD"].ToString();
                                urshEntryAdd.SHASHU_NAME = dr["SHASHU_NAME"].ToString();
                                urshEntryAdd.UNPAN_GYOUSHA_CD = dr["UNPAN_GYOUSHA_CD"].ToString();
                                urshEntryAdd.UNPAN_GYOUSHA_NAME = dr["UNPAN_GYOUSHA_NAME"].ToString();
                                urshEntryAdd.UNTENSHA_CD = dr["UNTENSHA_CD"].ToString();
                                urshEntryAdd.UNTENSHA_NAME = dr["UNTENSHA_NAME"].ToString();
                                if (!String.IsNullOrEmpty(dr["NINZUU_CNT"].ToString()))
                                {
                                    urshEntryAdd.NINZUU_CNT = SqlInt16.Parse(dr["NINZUU_CNT"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KEITAI_KBN_CD"].ToString()))
                                {
                                    urshEntryAdd.KEITAI_KBN_CD = SqlInt16.Parse(dr["KEITAI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["CONTENA_SOUSA_CD"].ToString()))
                                {
                                    urshEntryAdd.CONTENA_SOUSA_CD = SqlInt16.Parse(dr["CONTENA_SOUSA_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_SHURUI_CD"].ToString()))
                                {
                                    urshEntryAdd.MANIFEST_SHURUI_CD = SqlInt16.Parse(dr["MANIFEST_SHURUI_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MANIFEST_TEHAI_CD"].ToString()))
                                {
                                    urshEntryAdd.MANIFEST_TEHAI_CD = SqlInt16.Parse(dr["MANIFEST_TEHAI_CD"].ToString());
                                }
                                urshEntryAdd.DENPYOU_BIKOU = dr["DENPYOU_BIKOU"].ToString();
                                if (!String.IsNullOrEmpty(dr["UKETSUKE_NUMBER"].ToString()))
                                {
                                    urshEntryAdd.UKETSUKE_NUMBER = SqlInt64.Parse(dr["UKETSUKE_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["RECEIPT_NUMBER"].ToString()))
                                {
                                    urshEntryAdd.RECEIPT_NUMBER = SqlInt32.Parse(dr["RECEIPT_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_SHOUHIZEI_RATE"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["URIAGE_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_AMOUNT_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_AMOUNT_TOTAL = SqlDecimal.Parse(dr["URIAGE_AMOUNT_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_TAX_SOTO = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_TAX_UCHI = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.HINMEI_URIAGE_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.HINMEI_URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.HINMEI_URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_SHOUHIZEI_RATE = SqlDecimal.Parse(dr["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_AMOUNT_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_AMOUNT_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_AMOUNT_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_TAX_SOTO = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_TAX_UCHI = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.HINMEI_SHIHARAI_KINGAKU_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                                {
                                    urshEntryAdd.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(dr["HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_ZEI_KBN_CD"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_ZEI_KBN_CD = SqlInt16.Parse(dr["URIAGE_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGE_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    urshEntryAdd.URIAGE_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["URIAGE_TORIHIKI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_ZEI_KBN_CD"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_ZEI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString()))
                                {
                                    urshEntryAdd.SHIHARAI_TORIHIKI_KBN_CD = SqlInt16.Parse(dr["SHIHARAI_TORIHIKI_KBN_CD"].ToString());
                                }
                                //urshEntryAdd.TIME_STAMP = (byte[])(dr["TIME_STAMP"]);
                                // WHOカラム設定
                                dataBind_urshEntryAdd.SetSystemProperty(urshEntryAdd, false);
                                if (!String.IsNullOrEmpty(dr["DELETE_FLG"].ToString()))
                                {
                                    urshEntryAdd.DELETE_FLG = SqlBoolean.Parse(dr["DELETE_FLG"].ToString());
                                }
                                urshEntryListAdd.Add(urshEntryAdd);
                                urshEntryListUpdata.Add(urshEntryUpdata);
                            }

                            foreach (DataRow dr in dbDetail.Rows)
                            {
                                urshMs = new T_UR_SH_DETAIL();
                                if (!String.IsNullOrEmpty(dr["SYSTEM_ID"].ToString()))
                                {
                                    urshMs.SYSTEM_ID = SqlInt64.Parse(dr["SYSTEM_ID"].ToString());
                                }
                                urshMs.SEQ = SqlInt32.Parse(saiban.ToString());
                                if (!String.IsNullOrEmpty(dr["DETAIL_SYSTEM_ID"].ToString()))
                                {
                                    urshMs.DETAIL_SYSTEM_ID = SqlInt64.Parse(dr["DETAIL_SYSTEM_ID"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["UR_SH_NUMBER"].ToString()))
                                {
                                    urshMs.UR_SH_NUMBER = SqlInt64.Parse(dr["UR_SH_NUMBER"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["ROW_NO"].ToString()))
                                {
                                    urshMs.ROW_NO = SqlInt16.Parse(dr["ROW_NO"].ToString());
                                }
                                // 伝票単位の場合、全明細を未確定にする
                                if (this.msysinfo.SYS_KAKUTEI__TANNI_KBN.Equals(SqlInt16.Parse("1")))
                                {

                                    // 売上伝票の明細データは確定しない

                                    if (DENPYOU_KBN_SHIHARAI.Equals(dr["DENPYOU_KBN_CD"].ToString()))
                                    {
                                        // 伝票区分が支払の場合
                                        urshMs.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_MIKAKUTEI);
                                    }
                                    else
                                    {
                                        // 伝票区分が売上の場合
                                        if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                        {
                                            // 値を保持する
                                            urshMs.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    // 明細単位の場合は後でセットする
                                    if (!String.IsNullOrEmpty(dr["KAKUTEI_KBN"].ToString()))
                                    {
                                        urshMs.KAKUTEI_KBN = SqlInt16.Parse(dr["KAKUTEI_KBN"].ToString());
                                    }
                                }
                                if (!String.IsNullOrEmpty(dr["URIAGESHIHARAI_DATE"].ToString()))
                                {
                                    urshMs.URIAGESHIHARAI_DATE = SqlDateTime.Parse(dr["URIAGESHIHARAI_DATE"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["DENPYOU_KBN_CD"].ToString()))
                                {
                                    urshMs.DENPYOU_KBN_CD = SqlInt16.Parse(dr["DENPYOU_KBN_CD"].ToString());
                                }
                                urshMs.HINMEI_CD = dr["HINMEI_CD"].ToString();
                                urshMs.HINMEI_NAME = dr["HINMEI_NAME"].ToString();
                                if (!String.IsNullOrEmpty(dr["SUURYOU"].ToString()))
                                {
                                    urshMs.SUURYOU = SqlDecimal.Parse(dr["SUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["UNIT_CD"].ToString()))
                                {
                                    urshMs.UNIT_CD = SqlInt16.Parse(dr["UNIT_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["TANKA"].ToString()))
                                {
                                    urshMs.TANKA = SqlDecimal.Parse(dr["TANKA"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["KINGAKU"].ToString()))
                                {
                                    urshMs.KINGAKU = SqlDecimal.Parse(dr["KINGAKU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["TAX_SOTO"].ToString()))
                                {
                                    urshMs.TAX_SOTO = SqlDecimal.Parse(dr["TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["TAX_UCHI"].ToString()))
                                {
                                    urshMs.TAX_UCHI = SqlDecimal.Parse(dr["TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_ZEI_KBN_CD"].ToString()))
                                {
                                    urshMs.HINMEI_ZEI_KBN_CD = SqlInt16.Parse(dr["HINMEI_ZEI_KBN_CD"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_KINGAKU"].ToString()))
                                {
                                    urshMs.HINMEI_KINGAKU = SqlDecimal.Parse(dr["HINMEI_KINGAKU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_TAX_SOTO"].ToString()))
                                {
                                    urshMs.HINMEI_TAX_SOTO = SqlDecimal.Parse(dr["HINMEI_TAX_SOTO"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["HINMEI_TAX_UCHI"].ToString()))
                                {
                                    urshMs.HINMEI_TAX_UCHI = SqlDecimal.Parse(dr["HINMEI_TAX_UCHI"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["MEISAI_BIKOU"].ToString()))
                                {
                                    urshMs.MEISAI_BIKOU = dr["MEISAI_BIKOU"].ToString();
                                }
                                if (!String.IsNullOrEmpty(dr["NISUGATA_SUURYOU"].ToString()))
                                {
                                    urshMs.NISUGATA_SUURYOU = SqlDecimal.Parse(dr["NISUGATA_SUURYOU"].ToString());
                                }
                                if (!String.IsNullOrEmpty(dr["NISUGATA_SUURYOU"].ToString()))
                                {
                                    urshMs.NISUGATA_UNIT_CD = SqlInt16.Parse(dr["NISUGATA_UNIT_CD"].ToString());
                                }
                                //urshMs.TIME_STAMP = (byte[])(dr["TIME_STAMP"]);
                                // WHOカラム設定
                                dataBind_urshMs.SetSystemProperty(urshMs, false);

                                urshMsList.Add(urshMs);
                            }
                        }
                        foreach (T_UR_SH_DETAIL item in urshMsList)
                        {
                            if (item.SYSTEM_ID.ToString().Equals(dgvRow["システムID"].ToString())
                            && (item.SEQ - 1).ToString().Equals(dgvRow["枝番"].ToString())
                            && item.DETAIL_SYSTEM_ID.ToString().Equals(dgvRow["明細・システムID"].ToString()))
                            {
                                item.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_MIKAKUTEI);
                                //item.SEQ = SqlInt16.Parse(saiban.ToString());
                            }
                        }

                        row++;
                        syetemidursh = dgvRow["システムID"].ToString();
                        seqursh = dgvRow["枝番"].ToString();
                    }
                }

                string syetemid = "";
                string seq = "";
                bool flag = false;
                int j = 1;
                foreach (T_UR_SH_DETAIL item in urshMsList)
                {
                    if (j == 1)
                    {
                        syetemid = item.SYSTEM_ID.ToString();
                        seq = item.SEQ.ToString();
                    }

                    if (item.SYSTEM_ID.ToString().Equals(syetemid) && item.SEQ.ToString().Equals(seq))
                    {
                        // 未確定ならtrue(NULLの場合があるので確定以外で判定)
                        if (!item.KAKUTEI_KBN.ToString().Equals(KAKUTEI_KBN_KAKUTEI))
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        foreach (T_UR_SH_ENTRY itemEntry in urshEntryListAdd)
                        {
                            if (SqlInt64.Parse(syetemid) == itemEntry.SYSTEM_ID && SqlInt64.Parse(seq) == itemEntry.SEQ)
                            {
                                if (flag == true)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_MIKAKUTEI);
                                }
                                else if (flag == false)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_KAKUTEI);
                                }
                            }

                        }
                        flag = false;
                    }
                    if (j == urshMsList.Count)
                    {
                        // 未確定ならtrue(NULLの場合があるので確定以外で判定)
                        if (!item.KAKUTEI_KBN.ToString().Equals(KAKUTEI_KBN_KAKUTEI))
                        {
                            flag = true;
                        }


                        foreach (T_UR_SH_ENTRY itemEntry in urshEntryListAdd)
                        {
                            if (item.SYSTEM_ID == itemEntry.SYSTEM_ID && item.SEQ == itemEntry.SEQ)
                            {
                                if (flag == true)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_MIKAKUTEI);
                                }
                                else if (flag == false)
                                {
                                    itemEntry.KAKUTEI_KBN = SqlInt16.Parse(KAKUTEI_KBN_KAKUTEI);
                                }
                            }

                        }
                        flag = false;

                    }
                    syetemid = item.SYSTEM_ID.ToString();
                    seq = item.SEQ.ToString();
                    j++;

                }
                this.murshmslist = urshMsList;
                this.murshentrylistadd = urshEntryListAdd;
                this.murshentrylistupdata = urshEntryListUpdata;
            }
        }

        /// <summary>
        /// 一覧データ取得
        /// </summary>
        /// <param name="kubun"></param>
        private void GetMeisaiIchiranData(string kubun)
        {
            LogUtility.DebugMethodStart();

            DataTable dbkoshin = (DataTable)this.form.customDataGridView1.DataSource;
            DataTable ukeireDt = dbkoshin.Clone();　　      //受入
            DataTable shukaDt = dbkoshin.Clone();          //出荷    
            DataTable uriageshiharaiDt = dbkoshin.Clone();  //売上_支払

            foreach (DataRow Row in dbkoshin.Rows)
            {
                if (Row["分類"].ToString().Equals("受入"))
                {
                    ukeireDt.ImportRow(Row);
                }
                if (Row["分類"].ToString().Equals("出荷"))
                {
                    shukaDt.ImportRow(Row);
                }
                if (Row["分類"].ToString().Equals("売上_支払"))
                {
                    uriageshiharaiDt.ImportRow(Row);
                }

            }

            GetUkeireData(kubun, ukeireDt);
            GetShukaData(kubun, shukaDt);
            GetURSHData(kubun, uriageshiharaiDt);

            LogUtility.DebugMethodEnd();
        }

        #endregion 明細データ取得

        #endregion メソッド

    }
}
