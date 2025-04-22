using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Message;
using r_framework.Authority;
using Seasar.Framework.Exceptions;
using Shougun.Core.Adjustment.Shiharaiichiran.CustomControls_Ex;
using System.Collections.Generic;
using Shougun.Core.ExternalConnection.CommunicateLib.Dtos;
using r_framework.Dto;
using Shougun.Core.ExternalConnection.CommunicateLib;
using Shougun.Core.ExternalConnection.CommunicateLib.Utility;
using Shougun.Core.Adjustment.Shiharaiichiran.DTO;
using Shougun.Core.Common.BusinessCommon;
using r_framework.CustomControl;
using System.Drawing;
using Shougun.Core.Adjustment.Shiharaishimesyori.DAO;
using System.Collections.ObjectModel;

namespace Shougun.Core.Adjustment.Shiharaiichiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド
        /// <summary>
        /// Header - 拠点CD初期値：99
        /// </summary>
        private readonly string KYOTEN_CD_INIT = "99";

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
        public UIHeader headform;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// 拠点マスタ
        /// </summary>
        private IM_KYOTENDao m_kyotendao;

        /// <summary>
        /// 取引先マスタ
        /// </summary>
        private IM_TORIHIKISAKIDao mtorihikisakiDao;

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private TSDDaoCls t_ichirandao;

        /// <summary>
        /// 検索モード
        /// </summary>
        enum SearchMode
        {
            //簡易検索
            SIMPLE = 1,
            //汎用検索
            GENERAL = 2,
        }

        /// <summary>
        /// 選択中検索モード
        /// </summary>
        private SearchMode searchMode;

        internal MessageBoxShowLogic errmessage;

        public const string CELL_CHECKBOX_DEL = "CHECKBOX_DEL"; //PhuocLoc 2021/05/14 #148575
        //160020 S
        internal string GamenFlg = "1";
        internal const string CELL_SHUKKIN_YOTEI_BI_HENKO = "出金予定日(変更後)";
        internal const string CELL_TIMESTAMP = "TIMESTAMP";
        internal const string CELL_SHIHARAI_DATE = "SHIHARAI_DATE";
        //160020 E
        private Control[] allControl;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// SELECT句
        /// </summary>
        public string selectQuery { get; set; }

        /// <summary>
        /// ORDERBY句
        /// </summary>
        public string orderByQuery { get; set; }

        /// <summary>
        /// JOIN句
        /// </summary>
        public string joinQuery { get; set; }

        /// <summary>
        /// 作成したSQL
        /// </summary>
        public string createSql { get; set; }

        /// <summary>
        /// 締め処理画面連携 - 画面初期表示用DTO
        /// </summary>
        public DTOClass InitDto { get; set; }

        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
        private const string CELL_CHECKBOX = "CHECKBOX";
        private const string CELL_UPLOAD_STATUS = "UPLOAD_STATUS";
        private const string CELL_DOWNLOAD_STATUS = "DOWNLOAD_STATUS";
        private const string CELL_INXS_SHIHARAI_KBN = "INXS_SHIHARAI_KBN";
        private const string CELL_SEISAN_NUMBER = "必須精算番号";

        internal readonly bool isInxsShiharaiUpload;

        internal string[] selectedSeisanNumber = null;
        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new DTOClass();
            this.t_ichirandao = DaoInitUtility.GetComponent<TSDDaoCls>();
            this.m_kyotendao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.mtorihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.errmessage = new MessageBoxShowLogic();
            this.isInxsShiharaiUpload = r_framework.Configuration.AppConfig.AppOptions.IsInxsShiharai(); // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339

            LogUtility.DebugMethodEnd(targetForm);
        }

        #region 各初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.parentForm = (BusinessBaseForm)this.form.Parent;

                //add button INXS支払明細書発行 start refs #158003
                if (this.isInxsShiharaiUpload)
                {
                    this.AddSubFunction();
                }
                //add button INXS支払明細書発行 end

                // ボタンのテキストを初期化
                this.ButtonInit();

                this.EventInit();

                this.SetHeaderInit();
                this.SetMainFormInit();
                this.SetStyleDtGridView();
                this.SearchModeInit();
                //160020 S
                this.LockButtonShukkin();
                this.allControl = this.form.allControl;
                //160020 E

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
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
            if (!this.isInxsShiharaiUpload)
            {
				//remove button process6 refs #158003
                buttonSetting = buttonSetting.Where(w => w.ButtonName != "bt_process6").ToArray();
            }
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
            //160020 S
            //if (!this.isInxsSeikyuusho)
            //{
            //    parentForm.bt_process2.Enabled = false;
            //    parentForm.bt_process2.Text = string.Empty;
            //    parentForm.bt_process5.Enabled = false;
            //    parentForm.bt_process5.Text = string.Empty;
            //}
            this.ChangeMod();
            //160020 E
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        /// <returns></returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            Type cType = this.GetType();
            string strButtonInfoXmlPath = cType.Namespace;
            strButtonInfoXmlPath += ".Setting.ButtonSetting.xml";
            LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, strButtonInfoXmlPath));

            return buttonSetting.LoadButtonSetting(thisAssembly, strButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //TODO: 汎用/簡易検索(F1)ボタン
            //160020 S
            parentForm.bt_func1.Click += new System.EventHandler(this.bt_func1_Click);       //一括入力
            parentForm.bt_func2.Click += new System.EventHandler(this.bt_func2_Click);       //切替
            //160020 E

            //参照ボタン(F3)イベント生成
            parentForm.bt_func3.Click += new EventHandler(bt_func3_Click);

            //削除ボタン(F4)イベント生成
            parentForm.bt_func4.Click += new EventHandler(bt_func4_Click);

            //CSV出力ボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(bt_func6_Click);

            //条件クリアボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(bt_func7_Click);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(bt_func8_Click);
            parentForm.bt_func9.Click += new System.EventHandler(this.bt_func9_Click);       //登録 160020

            //並替移動ボタン(F10)イベント生成
            parentForm.bt_func10.Click += new EventHandler(bt_func10_Click);

            //フィルタボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(bt_func11_Click);  

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);

            //プロセスボタンイベント生成
            parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);
            parentForm.bt_process2.Click += new EventHandler(bt_process2_Click);             //請求締処理
            parentForm.bt_process3.Click += new EventHandler(bt_process3_Click);             //請求書発行
            parentForm.bt_process4.Click += new EventHandler(bt_process4_Click);             //現金出金
            parentForm.bt_process5.Click += new EventHandler(bt_process5_Click);             //振込出金 160020

            parentForm.FormClosing += new FormClosingEventHandler(SetPrevStatus);   // No.2002

            //画面上でESCキー押下時のイベント生成 TODO　二次開発 
            //this.form.PreviewKeyDown += new PreviewKeyDownEventHandler(form_PreviewKeyDown); //form上でのESCキー押下でFocus移動
            //ダブルクリック時の動作は「F3 修正」と同様の処理を行う
            this.form.customDataGridView1.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(customDataGridView1_MouseDoubleClick);
            this.form.SHUKKIN_YOTEI_DATE_TO.MouseDoubleClick += new MouseEventHandler(SHUKKIN_YOTEI_DATE_TO_MouseDoubleClick);//160020

            /// 20141128 Houkakou 「支払明細一覧」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.form.dtpDateTo.MouseDoubleClick += new MouseEventHandler(dtpDateTo_MouseDoubleClick);
            /// 20141128 Houkakou 「支払明細一覧」のダブルクリックを追加する　end
            
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
            if (this.isInxsShiharaiUpload)
            {
                parentForm.bt_process2.Click += new EventHandler(Bt_process2_Click);
                parentForm.bt_process5.Click += new EventHandler(Bt_process5_Click);
                var bt_process6 = parentForm.ProcessButtonPanel.Controls.Find("bt_process6", false)[0] as CustomButton;
                bt_process6.Click += new EventHandler(Bt_process6_Click);
                parentForm.OnReceiveMessageEvent += ParentForm_OnReceiveMessageEvent;
            }

            this.form.customDataGridView1.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(customDataGridView1_ColumnHeaderMouseClick);
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

            //PhuocLoc 2021/05/14 #148575 -Start
            this.form.customDataGridView1.CellClick += new DataGridViewCellEventHandler(customDataGridView1_CellClick);
            this.form.customDataGridView1.SelectionChanged += new EventHandler(customDataGridView1_SelectionChanged);
            //PhuocLoc 2021/05/14 #148575 -End
            this.form.customDataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(customDataGridView1_CellFormatting);//160020
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// HeaderForm設定
        /// </summary>
        /// <param name="hs"></param>
        public void SetHeader(UIHeader hs)
        {
            this.headform = hs;
        }

        #endregion

        #region Functionボタン 押下処理

        /// <summary>
        /// //160020 S
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void bt_func1_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> arrRow = GetRowChecked();
            if (arrRow == null || arrRow.Count == 0)
            {
                return;
            }
            this.form.SHUKKIN_YOTEI_BI_HENKOU.BackColor = Constans.NOMAL_COLOR;
            if (this.form.SHUKKIN_YOTEI_BI_HENKOU.Text == string.Empty)
            {
                this.form.SHUKKIN_YOTEI_BI_HENKOU.BackColor = Constans.ERROR_COLOR;
                MessageBoxUtility.MessageBoxShowError("変更後出金予定日は必須項目です。入力してください。");
                this.form.SHUKKIN_YOTEI_BI_HENKOU.Focus();
                return;
            }
            foreach (DataGridViewRow row in arrRow)
            {
                row.Cells[CELL_SHUKKIN_YOTEI_BI_HENKO].Value = this.form.SHUKKIN_YOTEI_BI_HENKOU.Text;
            }
        }
        /// <summary>
        /// 切替 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func2_Click(object sender, EventArgs e)
        {
            //[汎用検索]をクリア
            this.form.searchString.Clear();
            //一覧明細をクリア
            int k = this.form.customDataGridView1.Rows.Count;
            for (int i = k; i >= 1; i--)
            {
                this.form.customDataGridView1.Rows.RemoveAt(this.form.customDataGridView1.Rows[i - 1].Index);
            }
            this.headform.txtReadDataCnt.Text = "0";
            //ソートヘッダクリア
            this.form.customSortHeader1.ClearCustomSortSetting();
            // フィルタをクリア
            this.form.customSearchHeader1.ClearCustomSearchSetting();
            if (this.GamenFlg.Equals("1"))
            {
                this.GamenFlg = "2";
            }
            else
            {
                this.GamenFlg = "1";
            }
            this.ChangeMod();
            this.HeaderCheckBoxSupportMod();
            this.LockButtonShukkin();
        }
        //160020 E
        /// <summary>
        /// F3 参照処理
        /// </summary>
        public void bt_func3_Click(object sender, EventArgs e)
        {
            // 参照
            Edit(WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
        }

        /// <summary>
        /// F4 削除処理
        /// </summary>
        public void bt_func4_Click(object sender, EventArgs e)
        {
            //PhuocLoc 2021/05/14 #148575 -Start
            // 権限チェック(更新権限有無で判定)
            //if (r_framework.Authority.Manager.CheckAuthority("G112", WINDOW_TYPE.UPDATE_WINDOW_FLAG))
            //{
            //    // 削除
            //    Edit(WINDOW_TYPE.DELETE_WINDOW_FLAG);
            //}
            List<ShiharaiDeleteDto> lstShiharaiDeleteDto = this.GetRowsCheckedDelete();
            if (lstShiharaiDeleteDto != null && lstShiharaiDeleteDto.Count > 0)
            {
                //PhuocLoc 2021/06/28 #152180 -Start
                if (r_framework.Authority.Manager.CheckAuthority("G112", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DeleteMultiShiharai(lstShiharaiDeleteDto);
                }
                else
                {
                    MessageBoxUtility.MessageBoxShow("E158", "削除");
                }
                //PhuocLoc 2021/06/28 #152180 -End
            }
            else
            {
                // 権限チェック(更新権限有無で判定)
                if (r_framework.Authority.Manager.CheckAuthority("G112", WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    // 削除
                    Edit(WINDOW_TYPE.DELETE_WINDOW_FLAG);
                }
            }
            //PhuocLoc 2021/05/14 #148575 -End
        }

        public void bt_func6_Click(object sender, EventArgs e)
        {
            // No.2180
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // 一覧にデータ行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    // アラートを表示し、CSV出力処理はしない
                    MessageBoxUtility.MessageBoxShow("E044");
                }
                else
                {
                    if (MessageBoxUtility.MessageBoxShow("C012") == DialogResult.Yes)
                    {
                        CSVExport exp = new CSVExport();
                        exp.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, DENSHU_KBNExt.ToTitleString(this.form.DenshuKbn), this.form);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        public void bt_func7_Click(object sender, EventArgs e)
        {
            //[汎用検索]をクリア
            this.form.searchString.Clear();
            //一覧明細をクリア
            int k = this.form.customDataGridView1.Rows.Count;
            for (int i = k; i >= 1; i--)
            {
                this.form.customDataGridView1.Rows.RemoveAt(this.form.customDataGridView1.Rows[i - 1].Index);
            }
            this.headform.txtReadDataCnt.Text = "0";
            //ソートヘッダクリア
            this.form.customSortHeader1.ClearCustomSortSetting();
            //伝票日付のクリア
            this.SetDenpyouHidukeInit();
            //this.form.dtpDateFrom.Text = DateTime.Now.ToString(); // No.2292
            //this.form.dtpDateTo.Text = DateTime.Now.ToString();   // No.2292
            //拠点のクリア
            //this.headform.txtKyotenCd.Text = "";                  // No.2292
            //this.headform.txtKyotenNameRyaku.Text = "";           // No.2292
            //取引先のクリア
            this.form.TORIHIKISAKI_CD.Text = "";
            this.form.TORIHIKISAKI_NAME_RYAKU.Text = "";
            //並び順をクリア
            this.form.customSortHeader1.ClearCustomSortSetting();
            // フィルタをクリア
            this.form.customSearchHeader1.ClearCustomSearchSetting();

            //160020 S
            this.form.dtpDateFrom.IsInputErrorOccured = false;
            this.form.dtpDateTo.IsInputErrorOccured = false;
            this.form.SHUKKIN_YOTEI_DATE_FROM.IsInputErrorOccured = false;
            this.form.SHUKKIN_YOTEI_DATE_TO.IsInputErrorOccured = false;
            this.form.SHUKKIN_YOTEI_DATE_FROM.Text = string.Empty;
            this.form.SHUKKIN_YOTEI_DATE_TO.Text = string.Empty;
            if (this.GamenFlg.Equals("1"))
            {
                this.form.ZEI_KOMI_KBN.Text = "3";
            }
            this.form.SHUKKIN_YOTEI_BI_HENKOU.Text = string.Empty;
            //160020 E
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        public void bt_func8_Click(object sender, EventArgs e)
        {
            // パターン未登録の場合検索処理を行わない
            if (this.form.PatternNo == 0)
            {
                MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }
            //160020 S
            this.SetRequiredSetting();
            var autoCheckLogic = new AutoRegistCheckLogic(this.GetAllControl(), this.GetAllControl());
            this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
            if (this.form.RegistErrorFlag)
            {
                return;
            }
            //160020 E
            bool searchErrorFlag = false;
            this.form.dtpDateFrom.IsInputErrorOccured = false;
            this.form.dtpDateTo.IsInputErrorOccured = false;

            if (!string.IsNullOrEmpty(this.form.dtpDateFrom.GetResultText())
                && !string.IsNullOrEmpty(this.form.dtpDateTo.GetResultText()))
            {
                DateTime dtpFrom = DateTime.Parse(this.form.dtpDateFrom.GetResultText());
                DateTime dtpTo = DateTime.Parse(this.form.dtpDateTo.GetResultText());
                DateTime dtpFromWithoutTime = DateTime.Parse(dtpFrom.ToShortDateString());
                DateTime dtpToWithoutTime = DateTime.Parse(dtpTo.ToShortDateString());

                int diff = dtpFromWithoutTime.CompareTo(dtpToWithoutTime);

                if (0 < diff)
                {
                    //対象期間内でないならエラーメッセージ表示
                    this.form.dtpDateFrom.IsInputErrorOccured = true;
                    this.form.dtpDateTo.IsInputErrorOccured = true;
                    // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 start
                    MessageBoxUtility.MessageBoxShow("E030", "支払日付From", "支払日付To");
                    // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 end
                    this.form.dtpDateFrom.Select();
                    this.form.dtpDateFrom.Focus();
                    searchErrorFlag = true;
                }
            }
            //160020 S
            //else
            //{
            //    this.form.dtpDateFrom.IsInputErrorOccured = string.IsNullOrEmpty(this.form.dtpDateFrom.GetResultText());
            //    this.form.dtpDateTo.IsInputErrorOccured = string.IsNullOrEmpty(this.form.dtpDateTo.GetResultText());
            //    MessageBoxUtility.MessageBoxShow("E001", "支払日付");
            //    if (string.IsNullOrEmpty(this.form.dtpDateFrom.GetResultText()))
            //    {
            //        this.form.dtpDateFrom.Focus();
            //    }
            //    else
            //    {
            //        this.form.dtpDateTo.Focus();
            //    }
            //    searchErrorFlag = true;
            //}
            if (!searchErrorFlag)
            {
                this.form.SHUKKIN_YOTEI_DATE_FROM.IsInputErrorOccured = false;
                this.form.SHUKKIN_YOTEI_DATE_TO.IsInputErrorOccured = false;

                if (!string.IsNullOrEmpty(this.form.SHUKKIN_YOTEI_DATE_FROM.GetResultText())
                    && !string.IsNullOrEmpty(this.form.SHUKKIN_YOTEI_DATE_TO.GetResultText()))
                {
                    DateTime dtpFrom = DateTime.Parse(this.form.SHUKKIN_YOTEI_DATE_FROM.GetResultText());
                    DateTime dtpTo = DateTime.Parse(this.form.SHUKKIN_YOTEI_DATE_TO.GetResultText());
                    DateTime dtpFromWithoutTime = DateTime.Parse(dtpFrom.ToShortDateString());
                    DateTime dtpToWithoutTime = DateTime.Parse(dtpTo.ToShortDateString());

                    int diff = dtpFromWithoutTime.CompareTo(dtpToWithoutTime);

                    if (0 < diff)
                    {
                        //対象期間内でないならエラーメッセージ表示
                        this.form.SHUKKIN_YOTEI_DATE_FROM.IsInputErrorOccured = true;
                        this.form.SHUKKIN_YOTEI_DATE_TO.IsInputErrorOccured = true;
                        MessageBoxUtility.MessageBoxShow("E030", "出金予定日From", "出金予定日To");
                        this.form.SHUKKIN_YOTEI_DATE_FROM.Select();
                        this.form.SHUKKIN_YOTEI_DATE_FROM.Focus();
                        searchErrorFlag = true;
                    }
                }
            }
            //160020 E

            if (!searchErrorFlag)
            {
                if (!string.IsNullOrEmpty(this.form.searchString.Text))
                {
                    // 二次開発のため未使用
                    string getSearchString = this.form.searchString.Text.Replace("\r", "").Replace("\n", "");
                }

                this.Search();

                //PhuocLoc 2021/05/14 #148575 -Start
                if (this.form.customDataGridView1.Columns.Contains(CELL_CHECKBOX_DEL))
                {
                    DataGridViewCheckBoxHeaderCell header = this.form.customDataGridView1.Columns[CELL_CHECKBOX_DEL].HeaderCell as DataGridViewCheckBoxHeaderCell;
                    if (header != null)
                    {
                        header._checked = false;
                    }
                }
                //PhuocLoc 2021/05/14 #148575 -End

                if (string.IsNullOrEmpty(this.form.searchString.Text))
                {
                    this.form.searchString.Clear();
                }

                this.form.customDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                this.form.customDataGridView1.MultiSelect = false;

                //検索後に1行目を選択状態にする。
                if (this.form.customDataGridView1.Rows.Count > 0)
                {
                    this.form.customDataGridView1.Rows[0].Selected = true;
                }
            }
        }
        //登録 160020
        public void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                if (GamenFlg.Equals("1"))
                {
                    return;
                }
                List<DataGridViewRow> arrRow = GetRowChecked(true);
                if (arrRow == null || arrRow.Count == 0)
                {
                    return;
                }
                SeisanDenpyouDao seisanDao = DaoInitUtility.GetComponent<SeisanDenpyouDao>();
                using (Transaction tran = new Transaction())
                {
                    foreach (DataGridViewRow r in arrRow)
                    {
                        T_SEISAN_DENPYOU data = seisanDao.GetDataByKey(r.Cells[CELL_SEISAN_NUMBER].Value.ConvertToInt64());
                        if (data != null)
                        {
                            data.SHUKKIN_YOTEI_BI = r.Cells[CELL_SHUKKIN_YOTEI_BI_HENKO].Value.ConvertToDateTime();
                            data.TIME_STAMP = (byte[])r.Cells[CELL_TIMESTAMP].Value;
                            var databinder = new DataBinderLogic<T_SEISAN_DENPYOU>(data);
                            databinder.SetSystemProperty(data, false);
                            seisanDao.Update(data);
                        }
                    }
                    tran.Commit();
                }
                MessageBoxUtility.MessageBoxShow("I001", "登録");
                this.Search();
            }
            catch (Exception ee)
            {
                LogUtility.Error("bt_func9_Click", ee);
                if (ee is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.form.RegistErrorFlag = true;
                    MessageBoxUtility.MessageBoxShow("E080");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// F10 並び替え
        /// </summary>
        public void bt_func10_Click(object sender, EventArgs e)
        {
            //this.form.customSortHeader1.Focus();
            this.form.customSortHeader1.ShowCustomSortSettingDialog();
        }

        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            this.form.customSearchHeader1.ShowCustomSearchSettingDialog();

            if (this.form.customDataGridView1 != null)
            {
                this.headform.txtReadDataCnt.Text = this.form.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.headform.txtReadDataCnt.Text = "0";
            }
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        public void bt_func12_Click(object sender, EventArgs e)
        {
            // 以下の項目をセッティングファイルに保存する
            Properties.Settings.Default.SET_KYOTEN_CD = this.headform.txtKyotenCd.Text;                                                //拠点CD

            DateTime resultDt;
            //if (this.form.dtpDateFrom.Value == null)
            if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text) && DateTime.TryParse(this.form.dtpDateFrom.Text, out resultDt))
            {
                Properties.Settings.Default.SET_HIDUKE_FROM = DateTime.Parse(this.form.dtpDateFrom.Value.ToString()).ToShortDateString();          //伝票日付From
            }
            else
            {
                Properties.Settings.Default.SET_HIDUKE_FROM = string.Empty;
                // CustomDateTimePicker Valueのget return DateTime.ParseExactでエラーになる為、Emptyにしておく
                this.form.dtpDateFrom.Text = string.Empty;
            }

            //if (this.form.dtpDateTo.Value == null)
            if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text) && DateTime.TryParse(this.form.dtpDateTo.Text, out resultDt))
            {
                Properties.Settings.Default.SET_HIDUKE_TO = DateTime.Parse(this.form.dtpDateTo.Value.ToString()).ToShortDateString();              //伝票日付To
            }
            else
            {
                Properties.Settings.Default.SET_HIDUKE_TO = string.Empty;
                // CustomDateTimePicker Valueのget return DateTime.ParseExactでエラーになる為、Emptyにしておく
                this.form.dtpDateTo.Text = string.Empty;
            }
            // 取引先CD
            Properties.Settings.Default.SET_TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
            Properties.Settings.Default.Save();

            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.Close();
        }

        #endregion

        #region プロセスボタン押下処理

        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                var sysID = this.form.OpenPatternIchiran();
                this.form.setLogicSelect();
                // 適用ボタンが押された場合
                if (!string.IsNullOrEmpty(sysID))
                {
                    this.form.SetPatternBySysId(sysID);
                    this.form.ShowData();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_process1_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        ///  支払締処理画面へ遷移
        /// </summary>
        private void bt_process2_Click(object sender, System.EventArgs e)
        {
            FormManager.OpenFormWithAuth("G110", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
        }

        /// <summary>
        /// 支払明細書発行画面へ遷移
        /// </summary>
        private void bt_process3_Click(object sender, System.EventArgs e)
        {
            FormManager.OpenFormWithAuth("G116", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
        }

        /// <summary>
        /// 現金出金
        /// </summary>
        private void bt_process4_Click(object sender, System.EventArgs e)
        {
            if (this.GamenFlg.Equals("2"))
            {
                return;
            }
            CallNyuuShukkinGamen(1);
        }
        /// <summary>
        /// 振込出金
        /// </summary>
        private void bt_process5_Click(object sender, System.EventArgs e)
        {
            if (this.GamenFlg.Equals("2"))
            {
                return;
            }
            CallNyuuShukkinGamen(2);
        }

        #endregion

        #region DB関連処理

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
        /// 検索処理用
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            //SELECT句未取得なら検索できない
            if (string.IsNullOrEmpty(this.selectQuery))
            {
                this.headform.txtReadDataCnt.Text = "0";
                return 0;
            }

            //検索用SQL作成
            MakeSearchSql();

            //検索実行
            this.SearchResult = new DataTable();
            this.SearchResult = t_ichirandao.GetDateForStringSql(this.createSql);
            //160020 S       
            this.SearchResult.Columns.Cast<DataColumn>().ToList().ForEach(c => c.ReadOnly = true);
            if (this.GamenFlg.Equals("2"))
            {
                if (this.SearchResult.Columns.Contains(CELL_SHUKKIN_YOTEI_BI_HENKO))
                {
                    this.SearchResult.Columns[CELL_SHUKKIN_YOTEI_BI_HENKO].ReadOnly = false;
                }
            }
            //160020 E
            this.form.ShowData();
            //160020 S              
            if (this.GamenFlg.Equals("2"))
            {
                for (int index = 0; index < this.form.customDataGridView1.Columns.Count; index++)
                {
                    DataGridViewColumn column = this.form.customDataGridView1.Columns[index];

                    if (CELL_SHUKKIN_YOTEI_BI_HENKO.Equals(column.Name))
                    {
                        this.form.customDataGridView1.Columns.RemoveAt(index);
                        DgvCustomDataTimeColumn newColumnDataTime = new DgvCustomDataTimeColumn();
                        newColumnDataTime.Name = column.Name;
                        newColumnDataTime.DataPropertyName = column.DataPropertyName;
                        this.form.customDataGridView1.Columns.Insert(index, newColumnDataTime);
                    }

                    //有効化
                    if (CELL_SHUKKIN_YOTEI_BI_HENKO.Equals(column.Name))
                    {
                        column.ReadOnly = false;
                    }
                }
                this.form.customDataGridView1.Refresh();
            }
            DataGridViewColumn col = this.form.customDataGridView1.Columns[CELL_CHECKBOX_DEL];
            if (col is DataGridViewCheckBoxColumn)
            {
                DataGridViewCheckBoxHeaderCell header = col.HeaderCell as DataGridViewCheckBoxHeaderCell;
                if (header != null)
                {
                    header._checked = false;
                }
            }
            this.form.customDataGridView1.Refresh();
            //160020 E
            //読込データ件数を取得
            if (this.form.customDataGridView1 != null)
            {
                this.headform.txtReadDataCnt.Text = this.form.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.headform.txtReadDataCnt.Text = "0";
            }
            if (this.headform.txtReadDataCnt.Text == "0" && !this.form.IsNoneAlert)
            {
                MessageBoxUtility.MessageBoxShow("C001");
            }

            this.form.IsNoneAlert = false;

            LogUtility.DebugMethodEnd();

            return SearchResult.Rows.Count;
        }

        /// <summary>
        /// 更新処理用（未使用）
        /// </summary>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion DB関連処理

        #region メソッド

        /// <summary>
        /// ヘッダ初期値設定
        /// </summary>
        private void SetHeaderInit()
        {
            ////前回保存値がない場合はシステム設定ファイルから拠点CDを設定する
            ////拠点CDを取得  
            ////前回値ありの場合
            //if (!string.IsNullOrEmpty(Properties.Settings.Default.SET_KYOTEN_CD))
            //{
            //    var kyotenCd = Properties.Settings.Default.SET_KYOTEN_CD;
            //    this.headform.txtKyotenCd.Text = string.Empty;
            //    var kyoten_cd = 0;
            //    //数字チェック + 空チェック
            //    var kyoten_res = int.TryParse(kyotenCd, out kyoten_cd);
            //    if (kyoten_res)
            //    {
            //        M_KYOTEN mKyoten = new M_KYOTEN();
            //        mKyoten.KYOTEN_CD = (SqlInt16)kyoten_cd;
            //        //削除フラグがたっていない適用期間内の情報を取得する
            //        var mKyotenList = m_kyotendao.GetAllValidData(mKyoten);
            //        if (mKyotenList.Count() > 0)
            //        {
            //            this.headform.txtKyotenCd.Text = String.Format("{0:D2}", kyoten_cd);
            //        }
            //    }
            //    //前回保存値がブランクの場合
            //}
            //else if (Properties.Settings.Default.SET_KYOTEN_CD == null)
            //{
            //    this.headform.txtKyotenCd.Text = "";
            //}
            ////前回保存値がない場合
            //else
            //{
            //    XMLAccessor fileAccess = new XMLAccessor();
            //    CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();
            //    this.headform.txtKyotenCd.Text = String.Format("{0:D2}", int.Parse(configProfile.ItemSetVal1));
            //}

            if (this.InitDto != null)
            {
                // 締め処理画面からの引継ぎ時
                this.headform.txtKyotenCd.Text = this.InitDto.InitKyotenCd;
            }
            else
            {
                /* 拠点CD初期値は「99」固定 */
                this.headform.txtKyotenCd.Text = KYOTEN_CD_INIT;
            }


            // ユーザ拠点名称の取得
            if (this.headform.txtKyotenCd.Text != null)
            {
                M_KYOTEN mKyoten = new M_KYOTEN();
                mKyoten = (M_KYOTEN)m_kyotendao.GetDataByCd(this.headform.txtKyotenCd.Text);
                if (mKyoten == null || this.headform.txtKyotenCd.Text == "")
                {
                    this.headform.txtKyotenCd.Text = "";
                    this.headform.txtKyotenNameRyaku.Text = "";
                }
                else
                {
                    this.headform.txtKyotenNameRyaku.Text = mKyoten.KYOTEN_NAME_RYAKU;
                }
            }

            // 読込データ件数初期値0
            this.headform.txtReadDataCnt.Text = "0";
            //160020 S
            this.form.SHUKKIN_YOTEI_DATE_FROM.Text = string.Empty;
            this.form.SHUKKIN_YOTEI_DATE_TO.Text = string.Empty;
            this.form.ZEI_KOMI_KBN.Text = "3";
            this.form.SHUKKIN_YOTEI_BI_HENKOU.Text = string.Empty;
            //160020 E
        }

        /// <summary>
        /// メインフォーム初期値設定
        /// </summary>
        private void SetMainFormInit()
        {
            this.SetDenpyouHidukeInit();

            /* 取引先 */
            // 取引先CD
            if (this.InitDto != null)
            {
                // 締め処理画面からの引継ぎ時
                this.form.TORIHIKISAKI_CD.Text = this.InitDto.InitTorihiksiakiCd;
            }
            else
            {
                this.form.TORIHIKISAKI_CD.Text = Properties.Settings.Default.SET_TORIHIKISAKI_CD;
            }
            // 取引先名
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                M_TORIHIKISAKI mTorihikisaki = new M_TORIHIKISAKI();
                mTorihikisaki = (M_TORIHIKISAKI)mtorihikisakiDao.GetDataByCd(this.form.TORIHIKISAKI_CD.Text);

                if (mTorihikisaki == null)
                {
                    this.form.TORIHIKISAKI_CD.Text = string.Empty;
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                }
                else
                {
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = mTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                }
            }
        }

        /// <summary>
        /// 汎用/簡易検索の初期化処理
        /// ※現在、簡易検索のみのため簡易検索で初期化
        /// </summary>
        private void SearchModeInit()
        {
            this.form.SIMPLE_SEARCH_PANEL.Visible = true;
            this.form.searchString.Visible = false;
            this.searchMode = SearchMode.SIMPLE;
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
        /// 明細行ダブルクリック処理
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void customDataGridView1_MouseDoubleClick(object sender, EventArgs e)
        {
            DataGridViewCellMouseEventArgs cell = (DataGridViewCellMouseEventArgs)e;
            if (cell.RowIndex >= 0)
            {
                // 参照
                Edit(WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
            }
        }

        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
        /// <summary>
        /// ヘッダーのチェックボックスクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn col = this.form.customDataGridView1.Columns[e.ColumnIndex];
            if (col is DataGridViewCheckBoxColumn)
            {
                DataGridViewCheckBoxHeaderCell header = col.HeaderCell as DataGridViewCheckBoxHeaderCell;
                if (header != null)
                {
                    header.MouseClick(e);
                }
            }
        }
        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

        /// <summary>
        /// 支払明細書確認画面遷移処理
        /// </summary>
        /// <param name="type">WINDOW_TYPE</param>
        public void Edit(WINDOW_TYPE type)
        {
            if (this.form.customDataGridView1.SelectedRows.Count == 0)
            {
                // 念のため初期化
                string param = "伝票";
                if (type == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    param = "削除する伝票";
                }
                else if (type == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                {
                    param = "参照する伝票";
                }
                MessageBoxUtility.MessageBoxShow("E051", param);
                return;
            }

            //最新データかチェック
            if (type == WINDOW_TYPE.DELETE_WINDOW_FLAG && !CheckDelData())
            {
                MessageBoxUtility.MessageBoxShow("I012", "精算伝票");
                return;
            }

            string strShiharaiNumber = "";
            strShiharaiNumber = this.form.customDataGridView1.SelectedRows[0].Cells["必須精算番号"].Value.ToString();

            //支払明細書確認画面表示
            FormManager.OpenForm("G111", strShiharaiNumber, type);
        }

        /// <summary>
        /// SQL作成
        /// </summary>
        public void MakeSearchSql()
        {
            //SQL文格納StringBuilder
            var sql = new StringBuilder();
            sql.Append(" SELECT DISTINCT ");
            sql.Append(this.selectQuery);
            //必須取得項目
            sql.Append(",T_SEISAN_DENPYOU.SEISAN_NUMBER AS \"必須精算番号\",T_SEISAN_DENPYOU.TORIHIKISAKI_CD AS \"必須取引先CD\"");

            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
            if (this.isInxsShiharaiUpload)
            {
                sql.Append(",T_SEISAN_DENPYOU_INXS.UPLOAD_STATUS,T_SEISAN_DENPYOU_INXS.DOWNLOAD_STATUS,TORIHIKISAKI_SHIHARAI.INXS_SHIHARAI_KBN ");
            }
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end
            //160020 S
            sql.Append(",(ISNULL(KONKAI_SHIHARAI_GAKU,0) + ISNULL(KONKAI_SEI_UTIZEI_GAKU,0) + ISNULL(KONKAI_SEI_SOTOZEI_GAKU,0) + ISNULL(KONKAI_DEN_UTIZEI_GAKU,0) + ISNULL(KONKAI_DEN_SOTOZEI_GAKU,0) + ISNULL(KONKAI_MEI_UTIZEI_GAKU,0) + ISNULL(KONKAI_MEI_SOTOZEI_GAKU,0)) - ISNULL(SHUKKIN.SUM_KESHIKOMI_GAKU,0) AS SHUKKIN_KOMI_GAKU");
            sql.Append(",T_SEISAN_DENPYOU.TIME_STAMP AS TIMESTAMP");
            sql.Append(",T_SEISAN_DENPYOU.SEISAN_DATE AS SHIHARAI_DATE");
            //160020 E
            //FROM句作成
            sql.Append(" FROM ");
            sql.Append(" T_SEISAN_DENPYOU ");
            //160020 S
            sql.Append(" LEFT JOIN ( ");
            sql.Append(" SELECT KESHIKOMI.SEISAN_NUMBER, SUM(KESHIKOMI.KESHIKOMI_GAKU) AS SUM_KESHIKOMI_GAKU ");
            sql.Append(" FROM T_SHUKKIN_ENTRY E INNER JOIN T_SHUKKIN_KESHIKOMI KESHIKOMI  ");
            sql.Append(" ON E.SYSTEM_ID = KESHIKOMI.SYSTEM_ID  ");
            sql.Append(" WHERE E.DELETE_FLG = 0 AND KESHIKOMI.DELETE_FLG = 0  ");
            sql.Append(" GROUP BY KESHIKOMI.SEISAN_NUMBER ) SHUKKIN");
            sql.Append(" ON T_SEISAN_DENPYOU.SEISAN_NUMBER = SHUKKIN.SEISAN_NUMBER ");
            //160020 E
            sql.Append(this.joinQuery);

            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
            if (this.isInxsShiharaiUpload)
            {
                sql.Append(" LEFT JOIN T_SEISAN_DENPYOU_INXS ON T_SEISAN_DENPYOU_INXS.SEISAN_NUMBER = T_SEISAN_DENPYOU.SEISAN_NUMBER ");
                sql.Append(" LEFT JOIN (SELECT TORIHIKISAKI_CD, INXS_SHIHARAI_KBN FROM M_TORIHIKISAKI_SHIHARAI) AS TORIHIKISAKI_SHIHARAI ON T_SEISAN_DENPYOU.TORIHIKISAKI_CD = TORIHIKISAKI_SHIHARAI.TORIHIKISAKI_CD ");
            }
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

            sql.Append(" WHERE ");
            sql.Append(" T_SEISAN_DENPYOU.DELETE_FLG = 0 ");

            //画面で日付選択が入力日付の場合
            if (this.form.dtpDateFrom.Value != null)
            {
                sql.Append(" AND T_SEISAN_DENPYOU.SEISAN_DATE >= '" + DateTime.Parse(this.form.dtpDateFrom.Value.ToString()).ToShortDateString() + "' ");
            }
            if (this.form.dtpDateTo.Value != null)
            {
                sql.Append(" AND T_SEISAN_DENPYOU.SEISAN_DATE <= '" + DateTime.Parse(this.form.dtpDateTo.Value.ToString()).ToShortDateString() + "' ");
            }
            //160020 S
            if (this.form.SHUKKIN_YOTEI_DATE_FROM.Value != null)
            {
                sql.Append(" AND T_SEISAN_DENPYOU.SHUKKIN_YOTEI_BI >= '" + DateTime.Parse(this.form.SHUKKIN_YOTEI_DATE_FROM.Value.ToString()).ToShortDateString() + "' ");
            }
            if (this.form.SHUKKIN_YOTEI_DATE_TO.Value != null)
            {
                sql.Append(" AND T_SEISAN_DENPYOU.SHUKKIN_YOTEI_BI <= '" + DateTime.Parse(this.form.SHUKKIN_YOTEI_DATE_TO.Value.ToString()).ToShortDateString() + "' ");
            }
            if (this.form.ZEI_KOMI_KBN.Text != "3")
            {
                if (this.form.ZEI_KOMI_KBN.Text == "1")
                {
                    sql.Append(" AND (ISNULL(KONKAI_SHIHARAI_GAKU,0) + ISNULL(KONKAI_SEI_UTIZEI_GAKU,0) + ISNULL(KONKAI_SEI_SOTOZEI_GAKU,0) + ISNULL(KONKAI_DEN_UTIZEI_GAKU,0) + ISNULL(KONKAI_DEN_SOTOZEI_GAKU,0) + ISNULL(KONKAI_MEI_UTIZEI_GAKU,0) + ISNULL(KONKAI_MEI_SOTOZEI_GAKU,0)) - ISNULL(SHUKKIN.SUM_KESHIKOMI_GAKU,0) != 0 ");
                }
                if (this.form.ZEI_KOMI_KBN.Text == "2")
                {
                    sql.Append(" AND (ISNULL(KONKAI_SHIHARAI_GAKU,0) + ISNULL(KONKAI_SEI_UTIZEI_GAKU,0) + ISNULL(KONKAI_SEI_SOTOZEI_GAKU,0) + ISNULL(KONKAI_DEN_UTIZEI_GAKU,0) + ISNULL(KONKAI_DEN_SOTOZEI_GAKU,0) + ISNULL(KONKAI_MEI_UTIZEI_GAKU,0) + ISNULL(KONKAI_MEI_SOTOZEI_GAKU,0)) - ISNULL(SHUKKIN.SUM_KESHIKOMI_GAKU,0) = 0 ");
                }
            }
            //160020 E
            //画面で拠点CDがnull無いの場合
            if (!string.IsNullOrEmpty(this.headform.txtKyotenCd.Text))
            {
                sql.Append(" AND T_SEISAN_DENPYOU.KYOTEN_CD = " + int.Parse(this.headform.txtKyotenCd.Text) + " ");
            }

            //画面で取引先CDがnullで無い場合
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                sql.Append(" AND T_SEISAN_DENPYOU.TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text + "' ");
            }

            if (!string.IsNullOrEmpty(orderByQuery))
            {
                sql.Append(" ORDER BY ");
                sql.Append(this.orderByQuery);
            }

            this.createSql = sql.ToString();
            sql.Append("");
        }

        /// <summary>
        /// 削除対象が最新データかチェックする
        /// </summary>
        /// <returns></returns>
        internal bool CheckDelData()
        {
            int MaxSeikyuuNumber = t_ichirandao.CheckDelData(this.form.customDataGridView1.SelectedRows[0].Cells["必須取引先CD"].Value.ToString());

            if (MaxSeikyuuNumber == 0
                || Int32.Parse(this.form.customDataGridView1.SelectedRows[0].Cells["必須精算番号"].Value.ToString()) < MaxSeikyuuNumber)
            {
                return false;
            }

            return true;
        }

        // No.2002
        /// <summary>
        /// Windowクローズ処理。
        /// </summary>
        internal void SetPrevStatus(object sender, EventArgs e)
        {
            // 以下の項目をセッティングファイルに保存する
            //拠点CD
            if (this.headform.txtKyotenCd.Text != "")
            {
                Properties.Settings.Default.SET_KYOTEN_CD = this.headform.txtKyotenCd.Text;
            }
            else
            {
                Properties.Settings.Default.SET_KYOTEN_CD = null;
            }

            DateTime resultDt;
            //if (this.form.dtpDateFrom.Value == null)
            if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text) && DateTime.TryParse(this.form.dtpDateFrom.Text, out resultDt))
            {
                Properties.Settings.Default.SET_HIDUKE_FROM = DateTime.Parse(this.form.dtpDateFrom.Value.ToString()).ToShortDateString();          //伝票日付From
            }
            else
            {
                Properties.Settings.Default.SET_HIDUKE_FROM = string.Empty;
                // CustomDateTimePicker Valueのget return DateTime.ParseExactでエラーになる為、Emptyにしておく
                this.form.dtpDateFrom.Text = string.Empty;
            }

            //if (this.form.dtpDateTo.Value == null)
            if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text) && DateTime.TryParse(this.form.dtpDateTo.Text, out resultDt))
            {
                Properties.Settings.Default.SET_HIDUKE_TO = DateTime.Parse(this.form.dtpDateTo.Value.ToString()).ToShortDateString();              //伝票日付To
            }
            else
            {
                Properties.Settings.Default.SET_HIDUKE_TO = string.Empty;
                // CustomDateTimePicker Valueのget return DateTime.ParseExactでエラーになる為、Emptyにしておく
                this.form.dtpDateTo.Text = string.Empty;
            }
            // 取引先CD
            Properties.Settings.Default.SET_TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
            Properties.Settings.Default.Save();

        }
        #endregion

        /// 20141128 Houkakou 「支払明細一覧」のダブルクリックを追加する　start
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpDateTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.dtpDateFrom;
            var ToTextBox = this.form.dtpDateTo;
            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141128 Houkakou 「支払明細一覧」のダブルクリックを追加する　end
        
        //thongh 2015/09/14 #13030 start
        /// <summary>
        /// 伝票日付初期値設定
        /// </summary>
        private void SetDenpyouHidukeInit()
        {
            /* 伝票日付From */
            if (this.InitDto != null && !string.IsNullOrEmpty(this.InitDto.InitDenpyouHiduke))
            {
                // 締め処理画面からの引継ぎ時
                this.form.dtpDateFrom.Text = this.InitDto.InitDenpyouHiduke;
            }
            else if (String.IsNullOrEmpty(Shougun.Core.Adjustment.Shiharaiichiran.Properties.Settings.Default.SET_HIDUKE_FROM))
            {
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //this.form.dtpDateFrom.Text = DateTime.Now.ToString();
                this.form.dtpDateFrom.Text = this.parentForm.sysDate.ToString();
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            }
            else
            {
                this.form.dtpDateFrom.Text = Shougun.Core.Adjustment.Shiharaiichiran.Properties.Settings.Default.SET_HIDUKE_FROM;
                Properties.Settings.Default.SET_HIDUKE_FROM = string.Empty;
            }

            /* 伝票日付To */
            if (this.InitDto != null && !string.IsNullOrEmpty(this.InitDto.InitDenpyouHiduke))
            {
                // 締め処理画面からの引継ぎ時
                this.form.dtpDateTo.Text = this.InitDto.InitDenpyouHiduke;
                this.InitDto.InitDenpyouHiduke = string.Empty;
            }
            else if (String.IsNullOrEmpty(Shougun.Core.Adjustment.Shiharaiichiran.Properties.Settings.Default.SET_HIDUKE_TO))
            {
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //this.form.dtpDateTo.Text = DateTime.Now.ToString();
                this.form.dtpDateTo.Text = this.parentForm.sysDate.ToString();
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            }
            else
            {
                this.form.dtpDateTo.Text = Shougun.Core.Adjustment.Shiharaiichiran.Properties.Settings.Default.SET_HIDUKE_TO;
                Properties.Settings.Default.SET_HIDUKE_TO = string.Empty;
            }
        }
        //thongh 2015/09/14 #13030 end

        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
        #region INXS Shiharai

        public void HeaderCheckBoxSupport()
        {
            if (r_framework.Configuration.AppConfig.AppOptions.IsInxsShiharai())
            {
                if (!this.form.customDataGridView1.Columns.Contains(CELL_CHECKBOX))
                {
                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.Name = CELL_CHECKBOX;
                    newColumn.HeaderText = "対象\n ";
                    newColumn.Width = 60;
                    DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(1, true);
                    newheader.Value = "対象\n ";
                    newColumn.HeaderCell = newheader;
                    newColumn.Resizable = DataGridViewTriState.False;
                    if (this.form.customDataGridView1.Columns.Count > 0)
                    {
                        this.form.customDataGridView1.Columns.Insert(0, newColumn);
                    }
                    else
                    {
                        this.form.customDataGridView1.Columns.Add(newColumn);
                    }
                }
            }
            else
            {
                if (this.form.customDataGridView1.Columns.Contains(CELL_CHECKBOX))
                {
                    this.form.customDataGridView1.Columns.Remove(CELL_CHECKBOX);
                }
            }
        }

        private bool IsValidSelect(ref List<CommonKeyDto> selectedNumber)
        {
            selectedNumber = GetSelectedSeisanNumber();
            if (selectedNumber.Count == 0)
            {
                this.errmessage.MessageBoxShow("E076");
                return false;
            }
            return true;
        }

        private bool IsInxsAuthority()
        {
            if (!SystemProperty.Shain.InxsTantouFlg)
            {
                return false;
            }
            return true;
        }

        private List<CommonKeyDto> GetSelectedSeisanNumber()
        {
            return this.form.customDataGridView1.Rows.Cast<DataGridViewRow>().Where(w => w.Cells[CELL_CHECKBOX].ReadOnly == false
                                                                                         && w.Cells[CELL_CHECKBOX].Value != null
                                                                                         && (bool)w.Cells[CELL_CHECKBOX].Value
                                                                                         && w.Cells[CELL_SEISAN_NUMBER].Value != null)
                                                                             .Select(s => new CommonKeyDto() { Id = Convert.ToInt64(s.Cells[CELL_SEISAN_NUMBER].Value.ToString()) })
                                                                             .ToList();
        }

        private void ExecuteSubAppCommand(object requestDto)
        {
            RemoteAppCls remoteAppCls = new RemoteAppCls();
            var token = remoteAppCls.GenerateToken(new CommunicateTokenDto()
            {
                TransactionId = this.form.transactionId,
                ReferenceID = -1
            });
            var execCommandDto = new ExecuteCommandDto()
            {
                Token = token,
                Type = ExternalConnection.CommunicateLib.Enums.NotificationType.ExecuteCommand,
                Args = new object[] { JsonUtility.SerializeObject(requestDto) }
            };
            remoteAppCls.ExecuteCommand(Constans.StartFormText, execCommandDto);

        }

        private void Bt_process2_Click(object sender, EventArgs e)
        {
            List<CommonKeyDto> selectedNumber = new List<CommonKeyDto>();
            if (!IsValidSelect(ref selectedNumber))
            {
                return;
            }

            var requestDto = new
            {
                CommandName = 4,//Get download status
                ShougunParentWindowName = this.parentForm.Text,
                CommandArgs = selectedNumber
            };

            ExecuteSubAppCommand(requestDto);

            this.bt_func8_Click(null, null);
            this.errmessage.MessageBoxShow("I026");
        }

        private void Bt_process5_Click(object sender, EventArgs e)
        {
            if (!IsInxsAuthority())
            {
                return;
            }
            List<CommonKeyDto> selectedNumber = new List<CommonKeyDto>();
            if (!IsValidSelect(ref selectedNumber))
            {
                return;
            }

            var checkSelectRow = this.form.customDataGridView1.Rows.Cast<DataGridViewRow>().Where(w => w.Cells[CELL_CHECKBOX].Value != null
                                                                                                   && (bool)w.Cells[CELL_CHECKBOX].Value
                                                                                                   && w.Cells[CELL_DOWNLOAD_STATUS].Value != null
                                                                                                   && w.Cells[CELL_DOWNLOAD_STATUS].Value.ToString() == "2"
                                                                                               ).Count();
            if (checkSelectRow > 0)
            {
                this.errmessage.MessageBoxShow("E298");
                return;
            }

            var requestDto = new
            {
                CommandName = 5,//delete seisan data
                ShougunParentWindowName = this.parentForm.Text,
                CommandArgs = selectedNumber
            };
            this.selectedSeisanNumber = requestDto.CommandArgs.Select(s => s.Id.ToString()).ToArray();

            ExecuteSubAppCommand(requestDto);

            this.bt_func8_Click(null, null);
            foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
            {
                string seisanNumber = dgvRow.Cells["必須精算番号"].Value.ToString();
                if (this.selectedSeisanNumber != null)
                {
                    if (this.selectedSeisanNumber.Contains(seisanNumber))
                    {
                        this.form.customDataGridView1.Rows[dgvRow.Index].Cells[CELL_CHECKBOX].Value = true;
                    }
                    else
                    {
                        this.form.customDataGridView1.Rows[dgvRow.Index].Cells[CELL_CHECKBOX].Value = false;
                    }
                    this.selectedSeisanNumber = null;
                }
            }

            this.errmessage.MessageBoxShow("I028");
        }

        private void ParentForm_OnReceiveMessageEvent(string message)
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    var arg = JsonUtility.DeserializeObject<CommunicateAppDto>(message);
                    if (arg != null)
                    {
                        var msgDto = (CommunicateAppDto)arg;
                        var token = JsonUtility.DeserializeObject<CommunicateTokenDto>(msgDto.Token);
                        if (token != null)
                        {
                            var tokenDto = (CommunicateTokenDto)token;
                            if (tokenDto.TransactionId == this.form.transactionId)
                            {
                                if (msgDto.Args.Length > 0 && msgDto.Args[0] != null)
                                {
                                    var responeDto = JsonUtility.DeserializeObject<InxsSubAppResponseDto>(msgDto.Args[0].ToString());
                                    if (responeDto != null)
                                    {
                                        this.ShowInxsMessage(responeDto.MessageType, responeDto.ResponseMessage);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// CONFIRM = 1, WARN = 2, ERROR = 3, INFO = 4,
        /// </summary>
        /// <param name="type"></param>
        /// <param name="strMsg"></param>
        private void ShowInxsMessage(int type, string strMsg)
        {
            switch (type)
            {
                case 1:
                    this.errmessage.MessageBoxShowConfirm(strMsg);
                    break;
                case 2:
                    this.errmessage.MessageBoxShowWarn(strMsg);
                    break;
                case 3:
                    this.errmessage.MessageBoxShowError(strMsg);
                    break;
                case 4:
                    this.errmessage.MessageBoxShowInformation(strMsg);
                    break;
            }
        }

        #endregion
        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

        //PhuocLoc 2021/05/14 #148575 -Start
        internal void HeaderCheckBoxSupportMod()
        {
            LogUtility.DebugMethodStart();
            if (!this.form.customDataGridView1.Columns.Contains(CELL_CHECKBOX_DEL))
            {
                DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
                //160020 S
                if (this.GamenFlg.Equals("2"))
                {
                    newheader.Value = "";
                }
                else
                {
                    newheader.Value = "削除\n ";
                }
                //160020 E
                newheader.Tag = "すべての支払明細データを一括削除したい場合、チェックを付けてください";
                DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                newColumn.Name = CELL_CHECKBOX_DEL;
                //160020 S
                if (this.GamenFlg.Equals("2"))
                {
                    newColumn.HeaderText = "  \n ";
                }
                else
                {
                    newColumn.HeaderText = "削除\n ";
                }
                //160020 E
                newColumn.Width = 42;
                newColumn.FillWeight = 42;
                newColumn.MinimumWidth = 42;
                newColumn.HeaderCell = newheader;
                newColumn.Resizable = DataGridViewTriState.False;
                newColumn.Tag = "一括削除の対象データの場合、チェックを付けてください";
                newColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                if (this.form.customDataGridView1.Columns.Count > 0)
                {
                    this.form.customDataGridView1.Columns.Insert(0, newColumn);
                }
                else
                {
                    this.form.customDataGridView1.Columns.Add(newColumn);
                }
                this.form.customDataGridView1.Columns[CELL_CHECKBOX_DEL].ToolTipText = "処理対象とする場合はチェックしてください";
            }
            else
            {
                //160020 S
                DataGridViewColumn col = this.form.customDataGridView1.Columns[CELL_CHECKBOX_DEL];
                if (col is DataGridViewCheckBoxColumn)
                {
                    DataGridViewCheckBoxHeaderCell header = col.HeaderCell as DataGridViewCheckBoxHeaderCell;
                    if (header != null)
                    {
                        if (this.GamenFlg.Equals("2"))
                        {
                            header.Value = "  \n ";
                        }
                        else
                        {
                            header.Value = "削除\n ";
                        }
                    }
                }
                this.form.customDataGridView1.Refresh();
                //160020 E
            }
            LogUtility.DebugMethodEnd();
        }

        internal bool DeleteMultiShiharai(List<ShiharaiDeleteDto> lstShiharaiDeleteDto)
        {
            bool isError = false;
            if (lstShiharaiDeleteDto != null && lstShiharaiDeleteDto.Count > 0)
            {
                long[] arrShiharaiNumber = lstShiharaiDeleteDto.Select(c => c.ShiharaiNumber).ToArray();
                if (this.CheckMultiDelData(lstShiharaiDeleteDto))
                {
                    #region Delete
                    try
                    {
                        LogUtility.DebugMethodStart(lstShiharaiDeleteDto);
                        // トランザクション開始（エラーまたはコミットしなければ自動でロールバックされる）
                        using (Transaction tran = new Transaction())
                        {
                            //清算伝票更新
                            this.t_ichirandao.UpdateMultiShiharai(arrShiharaiNumber);
                            //清算伝票_鑑更新
                            this.t_ichirandao.UpdateMultiShiharaiKagami(arrShiharaiNumber);
                            //清算明細
                            this.t_ichirandao.UpdateMultiShiharaiDetail(arrShiharaiNumber);
                            if (this.isInxsShiharaiUpload && IsInxsAuthority())
                            {
                                List<CommonKeyDto> selectedNumber = new List<CommonKeyDto>();
                                selectedNumber = GetSelectedSeisanNumberMod();
                                foreach (CommonKeyDto number in selectedNumber)
                                {
                                    var requestDto = new
                                    {
                                        CommandName = 6,//delete seisan data
                                        ShougunParentWindowName = this.parentForm.Text,
                                        CommandArgs = new List<CommonKeyDto>() { number }
                                    };
                                    ExecuteSubAppCommand(requestDto);
                                }
                            }
                            // コミット
                            tran.Commit();
                            // 完了メッセージ表示
                            this.errmessage.MessageBoxShow("I001", "削除");
                        }
                        this.bt_func8_Click(null, null);
                    }
                    catch (Exception ex)
                    {
                        isError = true;
                        LogUtility.Debug(ex);
                        if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                        {
                            this.errmessage.MessageBoxShow("E080", "");
                        }
                        else if (ex is SQLRuntimeException)
                        {
                            this.errmessage.MessageBoxShow("E093", "");
                        }
                        else
                        {
                            this.errmessage.MessageBoxShow("E245", "");
                        }
                    }
                    finally
                    {
                        LogUtility.DebugMethodEnd(isError, lstShiharaiDeleteDto);
                    }
                    #endregion
                }
            }
            return isError;
        }

        internal List<ShiharaiDeleteDto> GetRowsCheckedDelete()
        {
            List<ShiharaiDeleteDto> lstShiharaiDeleteDto = new List<ShiharaiDeleteDto>();
            if (this.form.customDataGridView1.Columns.Contains(CELL_CHECKBOX_DEL))
            {
                for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                {
                    DataGridViewRow row = this.form.customDataGridView1.Rows[i];
                    if (row.Cells[CELL_CHECKBOX_DEL].Value != null && bool.Parse(row.Cells[CELL_CHECKBOX_DEL].Value.ToString()))
                    {
                        ShiharaiDeleteDto dto = new ShiharaiDeleteDto();
                        dto.ShiharaiNumber = long.Parse(this.form.customDataGridView1.Rows[i].Cells[CELL_SEISAN_NUMBER].Value.ToString());
                        dto.TorihikisakiCd = this.form.customDataGridView1.Rows[i].Cells["必須取引先CD"].Value.ToString();
                        lstShiharaiDeleteDto.Add(dto);
                    }
                }
            }

            List<ShiharaiDeleteDto> SortedList = lstShiharaiDeleteDto.OrderBy(o => o.TorihikisakiCd).ToList();

            return SortedList;
        }

        private bool CheckMultiDelData(List<ShiharaiDeleteDto> lstShiharaiDeleteDto)
        {
            try
            {
                LogUtility.DebugMethodStart(lstShiharaiDeleteDto);
                if (lstShiharaiDeleteDto != null && lstShiharaiDeleteDto.Count > 0)
                {
                    var lstTorihikisakiCd = lstShiharaiDeleteDto.Select(c => c.TorihikisakiCd).Distinct();
                    foreach (var torihikisakiCd in lstTorihikisakiCd)
                    {
                        long[] arrShiharaiNumber = lstShiharaiDeleteDto.Where(c => c.TorihikisakiCd == torihikisakiCd).OrderBy(c => c.ShiharaiNumber).Select(c => c.ShiharaiNumber).ToArray();
                        foreach (var ShiharaiNumber in arrShiharaiNumber)
                        {
                            long[] arrNextShiharaiNumber = this.t_ichirandao.GetListShiharaiNumber(torihikisakiCd, ShiharaiNumber);
                            var result = arrNextShiharaiNumber.Intersect(arrShiharaiNumber);
                            if (result.Count() == arrNextShiharaiNumber.Length) //OK
                            {
                                break;
                            }
                            else
                            {
                                foreach (long ShiharaiNext in arrNextShiharaiNumber)
                                {
                                    if (!arrShiharaiNumber.Contains(ShiharaiNext))
                                    {
                                        T_SEISAN_DENPYOU ShiharaiEntity = this.t_ichirandao.GetDataByCd(ShiharaiNext.ToString());
                                        if (ShiharaiEntity != null)
                                        {
                                            if (arrShiharaiNumber[arrShiharaiNumber.Count() - 1] < ShiharaiNext)
                                            {
                                                this.errmessage.MessageBoxShow("E305", ShiharaiEntity.TORIHIKISAKI_CD.ToString(), ShiharaiEntity.SEISAN_DATE.Value.ToString("yyyy/MM/dd"), ShiharaiEntity.SEISAN_NUMBER.ToString());
                                            }
                                            else
                                            {
                                                this.errmessage.MessageBoxShow("E304", ShiharaiEntity.TORIHIKISAKI_CD.ToString(), ShiharaiEntity.SEISAN_DATE.Value.ToString("yyyy/MM/dd"), ShiharaiEntity.SEISAN_NUMBER.ToString());
                                            }
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.errmessage.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.errmessage.MessageBoxShow("E093", "");
                }
                else
                {
                    this.errmessage.MessageBoxShow("E245", "");
                }
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(lstShiharaiDeleteDto);
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            if (this.form.customDataGridView1.CurrentCell != null && this.form.customDataGridView1.CurrentCell.OwningColumn.Name == CELL_CHECKBOX_DEL)
            {
                if (this.GamenFlg.Equals("1"))//160020
                {
                    parentForm.lb_hint.Text = "一括削除の対象データの場合、チェックを付けてください";
                }
                //160020 S
                else
                {
                    parentForm.lb_hint.Text = "";
                }
                //160020 E
            }
            else
            {
                parentForm.lb_hint.Text = string.Empty;
            }
            this.LockButtonShukkin();//160020
            if (this.form.customDataGridView1.Columns.Contains("CHECKBOX_DEL") && this.form.customDataGridView1.CurrentRow != null)
            {
                this.form.customDataGridView1.CurrentRow.Cells["CHECKBOX_DEL"].ReadOnly = false;
                this.form.customDataGridView1.CurrentRow.Cells["CHECKBOX_DEL"].Style.BackColor = Constans.NOMAL_COLOR;
                this.form.customDataGridView1.Refresh();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            if (this.GamenFlg.Equals("1"))//160020
            {
                if (this.form.customDataGridView1.Columns[e.ColumnIndex].Name.Equals(CELL_CHECKBOX_DEL))
                {
                    if (e.RowIndex == -1)
                    {
                        parentForm.lb_hint.Text = "すべての支払明細データを一括削除したい場合、チェックを付けてください";
                    }
                    else
                    {
                        parentForm.lb_hint.Text = "一括削除の対象データの場合、チェックを付けてください";
                    }
                }
            }
            //160020 S
            else
            {
                if (e.RowIndex == -1)
                {
                    parentForm.lb_hint.Text = "";
                }
                else
                {
                    parentForm.lb_hint.Text = "";
                }
            }
            //160020 E
        }

        private List<CommonKeyDto> GetSelectedSeisanNumberMod()
        {
            return this.form.customDataGridView1.Rows.Cast<DataGridViewRow>().Where(w => w.Cells[CELL_CHECKBOX_DEL].ReadOnly == false
                                                                                         && w.Cells[CELL_CHECKBOX_DEL].Value != null
                                                                                         && (bool)w.Cells[CELL_CHECKBOX_DEL].Value
                                                                                         && w.Cells[CELL_SEISAN_NUMBER].Value != null)
                                                                             .Select(s => new CommonKeyDto() { Id = Convert.ToInt64(s.Cells[CELL_SEISAN_NUMBER].Value.ToString()) })
                                                                             .ToList();
        }
        //PhuocLoc 2021/05/14 #148575 -End

        #region #160020
        internal void SetRequiredSetting()
        {
            // 初期化
            this.form.dtpDateFrom.BackColor = Constans.NOMAL_COLOR;
            this.form.dtpDateTo.BackColor = Constans.NOMAL_COLOR;
            this.form.ZEI_KOMI_KBN.BackColor = Constans.NOMAL_COLOR;

            this.form.dtpDateFrom.RegistCheckMethod = null;
            this.form.dtpDateTo.RegistCheckMethod = null;
            this.form.ZEI_KOMI_KBN.RegistCheckMethod = null;

            // 設定
            SelectCheckDto existCheck = new SelectCheckDto();
            existCheck.CheckMethodName = "必須チェック";
            Collection<SelectCheckDto> excitChecks = new Collection<SelectCheckDto>();
            excitChecks.Add(existCheck);

            this.form.dtpDateFrom.RegistCheckMethod = excitChecks;
            this.form.dtpDateTo.RegistCheckMethod = excitChecks;
            this.form.ZEI_KOMI_KBN.RegistCheckMethod = excitChecks;
        }
        private Control[] GetAllControl()
        {
            List<Control> allControl = new List<Control>();
            allControl.AddRange(this.allControl);
            allControl.AddRange(this.form.controlUtil.GetAllControls(this.parentForm));

            return allControl.ToArray();
        }
        private void ChangeMod()
        {
            if (GamenFlg.Equals("1"))
            {
                this.form.label3.Visible = false;
                this.form.SHUKKIN_YOTEI_BI_HENKOU.Visible = false;
                this.form.SHUKKIN_YOTEI_BI_HENKOU.Text = string.Empty;
                parentForm.bt_func1.Enabled = false;
                parentForm.bt_func2.Text = "[F2]    切替";
                parentForm.bt_func3.Enabled = true;
                parentForm.bt_func4.Enabled = true;
                parentForm.bt_func6.Enabled = true;
                parentForm.bt_func9.Enabled = false;
                parentForm.bt_func9.Text = string.Empty;
                parentForm.bt_process2.Enabled = true;
                parentForm.bt_process3.Enabled = true;
                parentForm.bt_process4.Enabled = true;
                parentForm.bt_process5.Enabled = true;
                this.headform.lb_title.Text = "支払明細一覧";
                //this.headform.lb_title.Size = new System.Drawing.Size(188, 35);
                this.form.ZEI_KOMI_KBN.Text = "3";
                this.form.ZEI_KOMI_KBN.Enabled = true;
                this.form.ZEI_KOMI_KBN_1.Enabled = true;
                this.form.ZEI_KOMI_KBN_2.Enabled = true;
                this.form.ZEI_KOMI_KBN_3.Enabled = true;
            }
            else
            {
                this.form.label3.Visible = true;
                this.form.SHUKKIN_YOTEI_BI_HENKOU.Visible = true;
                this.form.SHUKKIN_YOTEI_BI_HENKOU.Text = string.Empty;
                parentForm.bt_func1.Enabled = true;
                parentForm.bt_func2.Text = "[F2]    一覧";
                parentForm.bt_func3.Enabled = false;
                parentForm.bt_func4.Enabled = false;
                parentForm.bt_func6.Enabled = false;
                parentForm.bt_func9.Enabled = true;
                parentForm.bt_func9.Text = "[F9]     登録";
                parentForm.bt_process2.Enabled = false;
                parentForm.bt_process3.Enabled = false;
                parentForm.bt_process4.Enabled = false;
                parentForm.bt_process5.Enabled = false;
                this.headform.lb_title.Text = "出金予定日変更";
                //this.headform.lb_title.Size = new System.Drawing.Size(243, 35);
                this.form.ZEI_KOMI_KBN.Text = "1";
                this.form.ZEI_KOMI_KBN.Enabled = false;
                this.form.ZEI_KOMI_KBN_1.Enabled = true;
                this.form.ZEI_KOMI_KBN_2.Enabled = false;
                this.form.ZEI_KOMI_KBN_3.Enabled = false;
            }
        }
        private void SHUKKIN_YOTEI_DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.SHUKKIN_YOTEI_DATE_FROM;
            var ToTextBox = this.form.SHUKKIN_YOTEI_DATE_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        private List<DataGridViewRow> GetRowChecked(bool isF9Flg = false)
        {
            if (!this.form.customDataGridView1.Columns.Contains(CELL_SHUKKIN_YOTEI_BI_HENKO))
            {
                return null;
            }
            if (this.form.customDataGridView1.Rows.Count == 0)
            {
                MessageBoxUtility.MessageBoxShow("E325");
                return null;
            }
            int errF9 = 0;
            List<DataGridViewRow> arrRow = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells[CELL_SHUKKIN_YOTEI_BI_HENKO], false);
                bool check = ObjectExtension.ConvertToBoolean(row.Cells[CELL_CHECKBOX_DEL].Value, false);
                if (check)
                {
                    if (isF9Flg)
                    {
                        string nyuukinHenkou = StringUtil.ConverToString(row.Cells[CELL_SHUKKIN_YOTEI_BI_HENKO].Value);
                        if (string.IsNullOrEmpty(nyuukinHenkou))
                        {
                            ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells[CELL_SHUKKIN_YOTEI_BI_HENKO], true);
                            errF9 = 1;
                        }
                        else
                        {
                            DateTime seikyuuDate = row.Cells[CELL_SHIHARAI_DATE].Value.ConvertToDateTime();
                            DateTime nyuukinDate = row.Cells[CELL_SHUKKIN_YOTEI_BI_HENKO].Value.ConvertToDateTime();
                            if (seikyuuDate.CompareTo(nyuukinDate) > 0)
                            {
                                ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells[CELL_SHUKKIN_YOTEI_BI_HENKO], true);
                                errF9 = 2;
                            }
                        }
                    }
                    arrRow.Add(row);
                }
            }
            if (arrRow.Count == 0)
            {
                MessageBoxUtility.MessageBoxShow("E325");
                return null;
            }
            if (errF9 == 1)
            {
                MessageBoxUtility.MessageBoxShow("E001", "出金予定日(変更後)");
                return null;
            }
            if (errF9 == 2)
            {
                string[] errorMsg = { "支払日付", "出金予定日" };
                MessageBoxUtility.MessageBoxShow("E030", errorMsg);
                return null;
            }
            return arrRow;
        }
        internal void LockButtonShukkin()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            if (this.GamenFlg.Equals("2"))
            {
                parentForm.bt_process4.Enabled = false;
                parentForm.bt_process5.Enabled = false;
                return;
            }
            parentForm.bt_process4.Enabled = false;
            parentForm.bt_process5.Enabled = false;
            if (this.form.customDataGridView1.RowCount > 0)
            {
                DataGridViewRow currenRow = this.form.customDataGridView1.CurrentRow;
                if (currenRow != null)
                {
                    decimal nyuukingaku = NumberUtil.ConvertToDecimal(currenRow.Cells["SHUKKIN_KOMI_GAKU"].Value);
                    if (nyuukingaku != 0)
                    {
                        parentForm.bt_process4.Enabled = true;
                        parentForm.bt_process5.Enabled = true;
                    }
                }
            }
        }
        private void CallNyuuShukkinGamen(int kbn)
        {
            LogUtility.DebugMethodStart(kbn);
            if (this.form.customDataGridView1.RowCount > 0)
            {
                DataGridViewRow currenRow = this.form.customDataGridView1.CurrentRow;
                if (currenRow != null)
                {
                    List<string> nyuukinPrm = nyuukinPrm = new List<string>();
                    //取引先CD
                    nyuukinPrm.Add(currenRow.Cells["必須取引先CD"].Value.ConvertToString(string.Empty));
                    //請求日付
                    nyuukinPrm.Add(currenRow.Cells[CELL_SHIHARAI_DATE].Value.ConvertToString(string.Empty));
                    //実行⇒現金出金
                    if (kbn == 1)
                    {
                        //入金区分CD
                        nyuukinPrm.Add("1");
                    }
                    //実行⇒振込入金
                    else
                    {
                        //入金区分CD
                        nyuukinPrm.Add("2");
                    }
                    nyuukinPrm.Add(currenRow.Cells[CELL_SEISAN_NUMBER].Value.ConvertToString(string.Empty));
                    FormManager.OpenFormWithAuth("G090", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, nyuukinPrm);
                }
            }
            else
            {
                MessageBoxUtility.MessageBoxShow("E325");
            }
            LogUtility.DebugMethodEnd();
        }
        private void customDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                if (this.form.customDataGridView1.Columns.Contains("CHECKBOX_DEL"))
                {
                    DataGridViewRow rows = this.form.customDataGridView1.Rows[e.RowIndex];
                    rows.Cells["CHECKBOX_DEL"].ReadOnly = false;
                    rows.Cells["CHECKBOX_DEL"].Style.BackColor = Constans.NOMAL_COLOR;
                }

            }
        }
        #endregion

        #region add button INXS支払明細書発行 refs #158003

        private void AddSubFunction()
        {
            CustomButton bt_process6 = new CustomButton();
            bt_process6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            bt_process6.DefaultBackColor = System.Drawing.Color.Empty;
            bt_process6.Enabled = false;
            bt_process6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            bt_process6.Location = new System.Drawing.Point(3, 153);
            bt_process6.Name = "bt_process6";
            bt_process6.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            bt_process6.Size = new System.Drawing.Size(150, 30);
            bt_process6.TabIndex = 396;
            bt_process6.Tag = "";
            bt_process6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            bt_process6.UseVisualStyleBackColor = false;

            parentForm.ProcessButtonPanel.Controls.Add(bt_process6);

            parentForm.ProcessButtonPanel.Location = new Point(1024, 498);
            parentForm.ProcessButtonPanel.Size = new Size(156, 208);

            parentForm.lb_process.Location = new Point(3, 185);
            parentForm.txb_process.Location = new Point(110, 184);
            parentForm.txb_process.CharacterLimitList = new char[] {
                '1',
                '2',
                '3',
                '4',
                '5',
                '6'};
            parentForm.txb_process.RangeSetting.Max = 6;
        }

        private void Bt_process6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            FormManager.OpenFormWithAuth("G747", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);

            LogUtility.DebugMethodEnd(sender, e);
        }
		
		#endregion
    }
}
