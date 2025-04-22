using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.Common.DenpyouRenkeiIchiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Common.DenpyouRenkeiIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// HeaderForm headForm
        /// </summary>
        private UIHeader headForm;

        /// <summary>
        /// フッター
        /// </summary>
        public BusinessBaseForm parentForm;

        /// <summary>
        /// コントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// ソート設定情報
        /// </summary>
        private SortSettingInfo sortSettingInfo = null;

        /// <summary>
        /// フィルタ設定情報
        /// </summary>
        private SearchSettingInfo searchSettingInfo = null;

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// DAO
        /// </summary>
        private DAOClass dao;

        private IM_KYOTENDao kyotenDao;
        private IM_TORIHIKISAKIDao torihikisakiDao;
        private IM_GYOUSHADao gyoushaDao;
        private IM_GENBADao genbaDao;
        public Color normalColor = Color.FromArgb(232, 247, 240);
        public Color bullColor = Color.FromArgb(0, 51, 160);
        public MessageBoxShowLogic msgLogic;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果_対象
        /// </summary>
        public DataTable searchResult { get; set; }

        /// <summary>
        /// 検索結果_派生
        /// </summary>
        public DataTable searchResult_hasei { get; set; }

        /// <summary>
        /// 検索結果_連携
        /// </summary>
        public DataTable searchResult_renkei { get; set; }

        private IM_SYS_INFODao sysInfoDao;
        private M_SYS_INFO sysInfoEntity;

        #endregion

        #region 一覧列名

        internal readonly string HIDDEN_DENPYOU_KBN = "HIDDEN_DENPYOU_KBN";
        internal readonly string HIDDEN_DENPYOU_TYPE = "HIDDEN_DENPYOU_TYPE";
        internal readonly string HIDDEN_DENPYOU_NO = "HIDDEN_DENPYOU_NO";
        internal readonly string HIDDEN_DENPYOU_DATE = "HIDDEN_DENPYOU_DATE";
        internal readonly string HIDDEN_TORIHIKISAKI_NAME = "HIDDEN_TORIHIKISAKI_NAME";
        internal readonly string HIDDEN_GYOUSHA_NAME = "HIDDEN_GYOUSHA_NAME";
        internal readonly string HIDDEN_GENBA_NAME = "HIDDEN_GENBA_NAME";
        internal readonly string HIDDEN_UKETSUKE_NUMBER = "HIDDEN_UKETSUKE_NUMBER";
        internal readonly string HIDDEN_SYSTEM_ID = "HIDDEN_SYSTEM_ID";
        internal readonly string HIDDEN_HAIKI_KBN_CD = "HIDDEN_HAIKI_KBN_CD";
        internal readonly string HIDDEN_RENKEI_DENSHU_KBN_CD = "HIDDEN_RENKEI_DENSHU_KBN_CD";
        internal readonly string HIDDEN_RENKEI_SYSTEM_ID = "HIDDEN_RENKEI_SYSTEM_ID";
        internal readonly string HIDDEN_HAIKI_KBN_CD_R = "HIDDEN_HAIKI_KBN_CD_R";
        internal readonly string HIDDEN_SYSTEM_ID_R = "HIDDEN_SYSTEM_ID_R";

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            this.form = targetForm;
            this.dto = new DTOClass();

            msgLogic = new MessageBoxShowLogic();
            this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.dao = DaoInitUtility.GetComponent<DAOClass>();
            this.sortSettingInfo = SortSettingHelper.LoadSortSettingInfo("UIForm.ENTRY_Ichiran");
            this.searchSettingInfo = SearchSettingHelper.LoadSearchSettingInfo("UIForm.ENTRY_Ichiran");
        }

        #endregion

        #region ヘッダ設定

        /// <summary>
        /// ヘッダ設定
        /// </summary>
        /// <returns></returns>
        public void SetHeader(UIHeader hs)
        {
            this.headForm = hs;
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal void WindowInit()
        {
            LogUtility.DebugMethodStart();

            this.allControl = this.form.allControl;

            var ribbon = (RibbonMainMenu)this.parentForm.ribbonForm;

            // ヘッダー（フッター）を初期化
            this.HeaderInit();

            // ボタンのテキストを初期化
            this.ButtonInit();

            // イベントの初期化処理
            this.EventInit();

            ClearScreen("Initial");
            this.form.ENTRY_Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.HASEI_Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.RENKEI_Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            UIHeader targetHeader = (UIHeader)parentForm.headerForm;
            this.headForm = targetHeader;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }

        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        #endregion

        #region イベント処理の初期化

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            parentForm = (BusinessBaseForm)this.form.Parent;

            //Functionボタンのイベント生成
            parentForm.bt_func7.Click += new System.EventHandler(this.form.bt_func7_Click);       //F7 検索条件クリア
            parentForm.bt_func8.Click += new System.EventHandler(this.form.bt_func8_Click);       //F8 検索
            parentForm.bt_func10.Click += new System.EventHandler(this.form.bt_func10_Click);     //F10 並び替え
            parentForm.bt_func11.Click += new System.EventHandler(this.form.bt_func11_Click);     //F11 フィルタ
            parentForm.bt_func12.Click += new System.EventHandler(this.form.bt_func12_Click);     //閉じる

            parentForm.bt_process1.Click += new System.EventHandler(this.form.bt_process1_Click); //対象伝票参照
            parentForm.bt_process2.Click += new System.EventHandler(this.form.bt_process2_Click); //派生伝票参照
            parentForm.bt_process3.Click += new System.EventHandler(this.form.bt_process3_Click); //CSV出力

            this.headForm.KYOTEN_CD.Validated += new EventHandler(this.form.KYOTEN_CD_Validated);
            // 20150824 katen #11971 伝票連携状況一覧 並び替えのテキスト部をクリックすると、並び替えのポップアップ出現 start‏
            //this.form.txboxSortSettingInfo.Enter += new EventHandler(this.form.txboxSortSettingInfo_Enter);
            //this.form.txtSearchSettingInfo.Enter += new EventHandler(this.form.txtSearchSettingInfo_Enter);
            // 20150824 katen #11971 伝票連携状況一覧 並び替えのテキスト部をクリックすると、並び替えのポップアップ出現 end‏
            //明細データダブルクリックイベント
            this.form.ENTRY_Ichiran.CellDoubleClick += new DataGridViewCellEventHandler(this.form.ENTRY_Ichiran_CellDoubleClick);
            this.form.HASEI_Ichiran.CellDoubleClick += new DataGridViewCellEventHandler(this.form.HASEI_Ichiran_CellDoubleClick);
            this.form.RENKEI_Ichiran.CellDoubleClick += new DataGridViewCellEventHandler(this.form.RENKEI_Ichiran_CellDoubleClick);
            this.form.RENKEI_Ichiran.CellPainting += new DataGridViewCellPaintingEventHandler(this.form.RENKEI_Ichiran_CellPainting);

            // 「To」のイベント生成
            this.form.DATE_TO.MouseDoubleClick += new MouseEventHandler(this.form.DATE_TO_MouseDoubleClick);

            this.form.DENPYOU_KBN.TextChanged += new EventHandler(this.form.DENPYOU_KBN_TextChanged);

            //受入出荷画面サイズ選択取得
            HearerSysInfoInit();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 検索

        #region 検索_対象

        /// <summary>
        /// 検索_対象
        /// </summary>
        public int Search_Entry()
        {
            LogUtility.DebugMethodStart();

            if (!this.DataCheck())
            {
                return 0;
            }

            int ret_cnt = 0;
            this.searchResult = new DataTable();

            this.dto = new DTOClass();
            if (!string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text) && this.headForm.KYOTEN_CD.Text != "99")
            {
                this.dto.KYOTEN_CD = Convert.ToInt16(this.headForm.KYOTEN_CD.Text);
            }
            if (!string.IsNullOrEmpty(this.form.DATE_FROM.Text))
            {
                this.dto.DENPYOU_DATE_FROM = Convert.ToDateTime(this.form.DATE_FROM.Text);
            }
            if (!string.IsNullOrEmpty(this.form.DATE_TO.Text))
            {
                this.dto.DENPYOU_DATE_TO = Convert.ToDateTime(this.form.DATE_TO.Text);
            }
            if (!string.IsNullOrEmpty(this.form.DENPYOU_NO.Text))
            {
                this.dto.DENPYOU_NO = this.form.DENPYOU_NO.Text;
            }
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                this.dto.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                this.dto.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                this.dto.GENBA_CD = this.form.GENBA_CD.Text;
            }
            this.dto.UKETSUKE_FLG = this.form.UKETSUKE_FLG.Checked;
            this.dto.UKEIRE_FLG = this.form.UKEIRE_FLG.Checked;
            this.dto.SHUKKA_FLG = this.form.SHUKKA_FLG.Checked;
            this.dto.UR_SH_FLG = this.form.UR_SH_FLG.Checked;
            this.dto.MANI_FLG = this.form.MANI_FLG.Checked;
            this.dto.UNCHIN_FLG = this.form.UNCHIN_FLG.Checked;
            this.dto.DAINOU_FLG = this.form.DAINOU_FLG.Checked;

            //検索を行う
            switch (this.form.DENPYOU_KBN.Text)
            {
                case "1":
                    this.searchResult = this.dao.GetUketsukeEntryData(this.dto);
                    break;

                case "2":
                    this.searchResult = this.dao.GetUkeireEntryData(this.dto);
                    break;

                case "3":
                    this.searchResult = this.dao.GetShukkaEntryData(this.dto);
                    break;

                case "4":
                    this.searchResult = this.dao.GetUrShEntryData(this.dto);
                    break;

                case "5":
                    this.searchResult = this.dao.GetManiEntryData(this.dto);
                    break;

                case "6":
                    this.searchResult = this.dao.GetUnchinEntryData(this.dto);
                    break;

                case "7":
                    this.searchResult = this.dao.GetDainouEntryData(this.dto);
                    break;
            }
            ret_cnt = this.searchResult.Rows.Count;

            //初期化
            this.form.ENTRY_Ichiran.DataSource = null;
            this.form.ENTRY_Ichiran.IsBrowsePurpose = false;
            this.form.ENTRY_Ichiran.Rows.Clear();
            this.form.ENTRY_Ichiran.Columns.Clear();

            if (this.searchResult != null)
            {
                SortDataTable(this.searchResult);
                SearchDataTable(this.searchResult, this.searchSettingInfo);
            }

            this.form.ENTRY_Ichiran.DataSource = this.searchResult;

            //フォーカス初期化
            //if (ret_cnt > 0)
            if (this.form.ENTRY_Ichiran.Rows.Count > 0)
            {
                this.form.ENTRY_Ichiran.CurrentCell = this.form.ENTRY_Ichiran[0, 0];
            }
            else
            {
                msgLogic.MessageBoxShow("C001");
            }

            foreach (DataGridViewColumn col in this.form.ENTRY_Ichiran.Columns)
            {
                switch (col.Name)
                {
                    case "HIDDEN_SYSTEM_ID":
                    case "HIDDEN_UKETSUKE_NUMBER":
                    case "HIDDEN_HAIKI_KBN_CD":
                    case "HIDDEN_RENKEI_DENSHU_KBN_CD":
                    case "HIDDEN_RENKEI_SYSTEM_ID":
                    case "HIDDEN_DENPYOU_KBN":
                    case "HIDDEN_DENPYOU_TYPE":
                        col.Visible = false;
                        break;

                    case "現場":
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;
                }
                col.ReadOnly = true;
            }

            this.form.ENTRY_Ichiran.ReadOnly = true;
            this.form.ENTRY_Ichiran.IsBrowsePurpose = true;
            this.form.ENTRY_Ichiran.Refresh();

            LogUtility.DebugMethodEnd();

            return ret_cnt;
        }

        #endregion

        #region 検索_派生

        /// <summary>
        /// 検索_派生
        /// </summary>
        public int Search_Hasei()
        {
            LogUtility.DebugMethodStart();

            this.dto.UKETSUKE_FLG = this.form.UKETSUKE_FLG.Checked;
            this.dto.UKEIRE_FLG = this.form.UKEIRE_FLG.Checked;
            this.dto.SHUKKA_FLG = this.form.SHUKKA_FLG.Checked;
            this.dto.UR_SH_FLG = this.form.UR_SH_FLG.Checked;
            this.dto.MANI_FLG = this.form.MANI_FLG.Checked;
            this.dto.UNCHIN_FLG = this.form.UNCHIN_FLG.Checked;
            this.dto.DAINOU_FLG = this.form.DAINOU_FLG.Checked;

            DataGridViewRow row = this.form.ENTRY_Ichiran.CurrentRow;
            if (row == null)
            {
                return 0;
            }

            string denpyouKbn = Convert.ToString(row.Cells["伝票"].Value);
            string denpyouNo = Convert.ToString(row.Cells["伝票番号"].Value);
            string uketsukeNo = Convert.ToString(row.Cells[this.HIDDEN_UKETSUKE_NUMBER].Value);
            string systemId = Convert.ToString(row.Cells[this.HIDDEN_SYSTEM_ID].Value);

            switch (denpyouKbn)
            {
                case "収集受付":
                    this.dto.UKETSUKE_NUMBER = denpyouNo;
                    this.dto.SYSTEM_ID = systemId;
                    this.searchResult_hasei = this.dao.GetUketsukeSSHaseiData(this.dto);
                    break;

                case "出荷受付":
                    this.dto.UKETSUKE_NUMBER = denpyouNo;
                    this.dto.SYSTEM_ID = systemId;
                    this.searchResult_hasei = this.dao.GetUketsukeSKHaseiData(this.dto);
                    break;

                case "持込受付":
                    this.dto.UKETSUKE_NUMBER = denpyouNo;
                    this.dto.SYSTEM_ID = systemId;
                    this.searchResult_hasei = this.dao.GetUketsukeMKHaseiData(this.dto);
                    break;

                case "物販受付":
                    this.dto.UKETSUKE_NUMBER = denpyouNo;
                    this.dto.SYSTEM_ID = systemId;
                    this.searchResult_hasei = this.dao.GetUketsukeBPHaseiData(this.dto);
                    break;

                case "受入":
                    this.dto.UKETSUKE_NUMBER = uketsukeNo;
                    this.dto.RENKEI_NUMBER = denpyouNo;
                    this.dto.SYSTEM_ID = systemId;
                    this.searchResult_hasei = this.dao.GetUkeireHaseiData(this.dto);
                    break;

                case "出荷":
                    this.dto.UKETSUKE_NUMBER = uketsukeNo;
                    this.dto.RENKEI_NUMBER = denpyouNo;
                    this.dto.SYSTEM_ID = systemId;
                    this.searchResult_hasei = this.dao.GetShukkaHaseiData(this.dto);
                    break;

                case "売上支払":
                    this.dto.UKETSUKE_NUMBER = uketsukeNo;
                    this.dto.RENKEI_NUMBER = denpyouNo;
                    this.dto.SYSTEM_ID = systemId;
                    this.searchResult_hasei = this.dao.GetUrShHaseiData(this.dto);
                    break;

                case "マニフェスト":
                    this.dto.RENKEI_NUMBER = denpyouNo;
                    this.dto.SYSTEM_ID = systemId;
                    this.dto.RENKEI_DENSHU_KBN_CD = Convert.ToString(row.Cells[this.HIDDEN_RENKEI_DENSHU_KBN_CD].Value);
                    this.dto.RENKEI_SYSTEM_ID = Convert.ToString(row.Cells[this.HIDDEN_RENKEI_SYSTEM_ID].Value);
                    this.searchResult_hasei = this.dao.GetManiHaseiData(this.dto);
                    break;

                case "運賃":
                    this.dto.RENKEI_NUMBER = uketsukeNo;
                    this.dto.SYSTEM_ID = systemId;
                    this.dto.RENKEI_DENSHU_KBN_CD = Convert.ToString(row.Cells[this.HIDDEN_RENKEI_DENSHU_KBN_CD].Value);
                    this.searchResult_hasei = this.dao.GetUnchinHaseiData(this.dto);
                    break;

                case "代納":
                    this.dto.RENKEI_NUMBER = denpyouNo;
                    this.dto.SYSTEM_ID = systemId;
                    this.searchResult_hasei = this.dao.GetDainouHaseiData(this.dto);
                    break;
            }
            int ret_cnt = this.searchResult_hasei.Rows.Count;

            //初期化
            this.form.HASEI_Ichiran.DataSource = null;
            this.form.HASEI_Ichiran.IsBrowsePurpose = false;
            this.form.HASEI_Ichiran.Rows.Clear();
            this.form.HASEI_Ichiran.Columns.Clear();
            this.form.HASEI_Ichiran.DataSource = this.searchResult_hasei;

            //フォーカス初期化
            if (ret_cnt > 0)
            {
                this.form.HASEI_Ichiran.CurrentCell = this.form.HASEI_Ichiran[0, 0];
            }

            foreach (DataGridViewColumn col in this.form.HASEI_Ichiran.Columns)
            {
                switch (col.Name)
                {
                    case "HIDDEN_SYSTEM_ID":
                    case "HIDDEN_UKETSUKE_NUMBER":
                    case "HIDDEN_HAIKI_KBN_CD":
                    case "HIDDEN_RENKEI_DENSHU_KBN_CD":
                        col.Visible = false;
                        break;

                    case "現場":
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;
                }
            }

            this.form.HASEI_Ichiran.ReadOnly = true;
            this.form.HASEI_Ichiran.IsBrowsePurpose = true;
            this.form.HASEI_Ichiran.Refresh();

            LogUtility.DebugMethodEnd();

            return ret_cnt;
        }

        #endregion

        #region 検索_連携

        /// <summary>
        /// 検索_連携
        /// </summary>
        public int Search_Renkei()
        {
            LogUtility.DebugMethodStart();

            if (!this.DataCheck())
            {
                return 0;
            }

            int ret_cnt = 0;
            this.searchResult_renkei = new DataTable();

            this.dto = new DTOClass();
            if (!string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text) && this.headForm.KYOTEN_CD.Text != "99")
            {
                this.dto.KYOTEN_CD = Convert.ToInt16(this.headForm.KYOTEN_CD.Text);
            }
            if (!string.IsNullOrEmpty(this.form.DATE_FROM.Text))
            {
                this.dto.DENPYOU_DATE_FROM = Convert.ToDateTime(this.form.DATE_FROM.Text);
            }
            if (!string.IsNullOrEmpty(this.form.DATE_TO.Text))
            {
                this.dto.DENPYOU_DATE_TO = Convert.ToDateTime(this.form.DATE_TO.Text);
            }
            if (!string.IsNullOrEmpty(this.form.DENPYOU_NO.Text))
            {
                this.dto.DENPYOU_NO = this.form.DENPYOU_NO.Text;
            }
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                this.dto.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                this.dto.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                this.dto.GENBA_CD = this.form.GENBA_CD.Text;
            }
            this.dto.RENKEI_KBN = this.form.RENKEI_KBN.Text;

            //検索を行う
            switch (this.form.DENPYOU_KBN.Text)
            {
                case "1":
                    this.searchResult_renkei = this.dao.GetUketsukeRenkeiData(this.dto);
                    break;

                case "2":
                    this.searchResult_renkei = this.dao.GetUkeireRenkeiData(this.dto);
                    break;

                case "3":
                    this.searchResult_renkei = this.dao.GetShukkaRenkeiData(this.dto);
                    break;

                case "4":
                    this.searchResult_renkei = this.dao.GetUrShRenkeiData(this.dto);
                    break;

                case "7":
                    this.searchResult_renkei = this.dao.GetDainouRenkeiData(this.dto);
                    break;
            }
            ret_cnt = this.searchResult_renkei.Rows.Count;

            //初期化
            this.form.RENKEI_Ichiran.DataSource = null;
            this.form.RENKEI_Ichiran.Rows.Clear();
            this.form.RENKEI_Ichiran.Columns.Clear();
            foreach (DataColumn col in this.searchResult_renkei.Columns)
            {
                col.ReadOnly = false;
            }

            if (this.searchResult_renkei != null)
            {
                SortDataTable(this.searchResult_renkei);
                SearchDataTable(this.searchResult_renkei, this.searchSettingInfo);
            }

            //this.form.RENKEI_Ichiran.DataSource = this.searchResult_renkei;

            DataTable dtCopy = this.searchResult_renkei.DefaultView.ToTable();
            DataTable dtDataSource = this.CreateDataTable(dtCopy);
            this.form.RENKEI_Ichiran.DataSource = dtDataSource;

            //フォーカス初期化
            //if (ret_cnt > 0)
            if (this.form.RENKEI_Ichiran.Rows.Count > 0)
            {
                this.form.RENKEI_Ichiran.CurrentCell = this.form.RENKEI_Ichiran[0, 0];
            }
            else
            {
                foreach (DataGridViewColumn col in this.form.RENKEI_Ichiran.Columns)
                {
                    switch (col.Name)
                    {
                        case "HIDDEN_DENPYOU_NO":
                        case "HIDDEN_DENPYOU_TYPE":
                        case "HIDDEN_DENPYOU_KBN":
                        case "HIDDEN_DENPYOU_DATE":
                        case "HIDDEN_TORIHIKISAKI_NAME":
                        case "HIDDEN_GYOUSHA_NAME":
                        case "HIDDEN_GENBA_NAME":
                        case "HIDDEN_SYSTEM_ID":
                        case "HIDDEN_HAIKI_KBN_CD":
                        case "HIDDEN_SYSTEM_ID_R":
                        case "HIDDEN_HAIKI_KBN_CD_R":
                            col.Visible = false;
                            break;
                    }
                }

                for (int i = 6; i < this.form.RENKEI_Ichiran.Columns.Count; i++)
                {
                    DataGridViewColumn col = this.form.RENKEI_Ichiran.Columns[i];
                    col.HeaderCell.Style.BackColor = bullColor;
                    switch (i)
                    {
                        case 6:
                            col.HeaderText = "伝票";
                            break;

                        case 7:
                            col.HeaderText = "伝票番号";
                            break;

                        case 8:
                            col.HeaderText = "伝票日付";
                            break;

                        case 9:
                            col.HeaderText = "取引先";
                            break;

                        case 10:
                            col.HeaderText = "業者";
                            break;

                        case 11:
                            col.HeaderText = "現場";
                            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            break;
                    }
                }
                this.form.RENKEI_Ichiran.Refresh();

                msgLogic.MessageBoxShow("C001");
                return ret_cnt;
            }

            //SetGridView();

            foreach (DataGridViewColumn col in this.form.RENKEI_Ichiran.Columns)
            {
                switch (col.Name)
                {
                    case "HIDDEN_DENPYOU_NO":
                    case "HIDDEN_DENPYOU_TYPE":
                    case "HIDDEN_DENPYOU_KBN":
                    case "HIDDEN_DENPYOU_DATE":
                    case "HIDDEN_TORIHIKISAKI_NAME":
                    case "HIDDEN_GYOUSHA_NAME":
                    case "HIDDEN_GENBA_NAME":
                    case "HIDDEN_SYSTEM_ID":
                    case "HIDDEN_HAIKI_KBN_CD":
                    case "HIDDEN_SYSTEM_ID_R":
                    case "HIDDEN_HAIKI_KBN_CD_R":
                        col.Visible = false;
                        break;
                }
                col.ReadOnly = true;
            }

            for (int i = 6; i < this.form.RENKEI_Ichiran.Columns.Count; i++)
            {
                DataGridViewColumn col = this.form.RENKEI_Ichiran.Columns[i];
                col.HeaderCell.Style.BackColor = bullColor;
                switch (i)
                {
                    case 6:
                        col.HeaderText = "伝票";
                        break;

                    case 7:
                        col.HeaderText = "伝票番号";
                        break;

                    case 8:
                        col.HeaderText = "伝票日付";
                        break;

                    case 9:
                        col.HeaderText = "取引先";
                        break;

                    case 10:
                        col.HeaderText = "業者";
                        break;

                    case 11:
                        col.HeaderText = "現場";
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;
                }
            }
            this.form.RENKEI_Ichiran.Refresh();

            LogUtility.DebugMethodEnd();

            return ret_cnt;
        }

        #endregion

        #endregion

        #region 並べ替え

        public void DataSort()
        {
            if (this.form.KINOU_KBN_1.Checked)
            {
                var dataTable = this.form.ENTRY_Ichiran.DataSource as DataTable;
                {
                    SetDataGridViewColumns_E(this.form.ENTRY_Ichiran);
                    var dlg = new SortSettingForm(sortSettingInfo);
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        this.form.txboxSortSettingInfo.Text = sortSettingInfo.SortSettingCaption;
                        if (dataTable != null)
                        {
                            SortDataTable(dataTable);
                        }
                    }
                    dlg.Dispose();
                }
            }
            else if (this.form.KINOU_KBN_2.Checked)
            {
                var dataTable = this.form.RENKEI_Ichiran.DataSource as DataTable;
                {
                    SetDataGridViewColumns_R(this.form.RENKEI_Ichiran);
                    var dlg = new SortSettingForm(sortSettingInfo);
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        this.form.txboxSortSettingInfo.Text = sortSettingInfo.SortSettingCaption;
                        if (dataTable != null)
                        {
                            SortDataTable(dataTable);
                        }
                    }
                    dlg.Dispose();
                }
            }
        }

        #endregion

        #region フィルタ

        public void DataSearch()
        {
            if (this.form.KINOU_KBN_1.Checked)
            {
                var dataTable = this.form.ENTRY_Ichiran.DataSource as DataTable;
                this.SetColumnsToSearchColumns_E(this.form.ENTRY_Ichiran);
                var dlg = new SearchSettingForm(searchSettingInfo);
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        this.form.txtSearchSettingInfo.Text = searchSettingInfo.SearchSettingCaption;
                        if (dataTable != null)
                        {
                            SearchDataTable(dataTable, searchSettingInfo);
                        }
                    }
                }
                dlg.Dispose();
            }
            else if (this.form.KINOU_KBN_2.Checked)
            {
                //DataTable dataTable = this.form.RENKEI_Ichiran.DataSource as DataTable;
                DataTable dataTable = this.searchResult_renkei;
                SetColumnsToSearchColumns_R(this.form.RENKEI_Ichiran);
                var dlg = new SearchSettingForm(searchSettingInfo);
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        this.form.txtSearchSettingInfo.Text = searchSettingInfo.SearchSettingCaption;
                        if (dataTable != null)
                        {
                            SearchDataTable(dataTable, searchSettingInfo);

                            DataTable dtCopy = dataTable.DefaultView.ToTable();
                            DataTable dtDataSource = this.CreateDataTable(dtCopy);
                            this.form.RENKEI_Ichiran.DataSource = dtDataSource;
                        }
                    }
                }
                dlg.Dispose();
            }
        }

        #endregion

        #region 画面クリア

        /// <summary>
        /// 画面クリア
        /// </summary>
        public void ClearScreen(String Kbn)
        {
            LogUtility.DebugMethodStart();

            try
            {
                // 拠点
                if (string.IsNullOrEmpty(Properties.Settings.Default.KYOTEN_CD))
                {
                    XMLAccessor fileAccess = new XMLAccessor();
                    CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();

                    this.headForm.KYOTEN_CD.Text = configProfile.ItemSetVal1.PadLeft(2, '0');
                    M_KYOTEN data = this.kyotenDao.GetDataByCd(this.headForm.KYOTEN_CD.Text);
                    if (data != null)
                    {
                        this.headForm.KYOTEN_NAME.Text = data.KYOTEN_NAME_RYAKU;
                    }
                }
                else
                {
                    this.headForm.KYOTEN_CD.Text = Properties.Settings.Default.KYOTEN_CD;
                    M_KYOTEN data = this.kyotenDao.GetDataByCd(this.headForm.KYOTEN_CD.Text);
                    if (data != null)
                    {
                        this.headForm.KYOTEN_NAME.Text = data.KYOTEN_NAME_RYAKU;
                    }
                }

                // 機能区分
                if (string.IsNullOrEmpty(Properties.Settings.Default.KINOU_KBN))
                {
                    this.form.KINOU_KBN.Text = "1";
                }
                else
                {
                    this.form.KINOU_KBN.Text = Properties.Settings.Default.KINOU_KBN;
                }

                // 検索対象伝票
                if (string.IsNullOrEmpty(Properties.Settings.Default.DENPYOU_KBN))
                {
                    this.form.DENPYOU_KBN.Text = "1";
                }
                else
                {
                    this.form.DENPYOU_KBN.Text = Properties.Settings.Default.DENPYOU_KBN;
                }

                // 伝票日付
                this.form.DATE_FROM.Text = Properties.Settings.Default.DATE_FROM;
                this.form.DATE_TO.Text = Properties.Settings.Default.DATE_TO;

                // 伝票番号
                this.form.DENPYOU_NO.Text = Properties.Settings.Default.DENPYOU_NO;

                // 処理区分
                if (string.IsNullOrEmpty(Properties.Settings.Default.RENKEI_KBN))
                {
                    this.form.RENKEI_KBN.Text = "1";
                }
                else
                {
                    this.form.RENKEI_KBN.Text = Properties.Settings.Default.RENKEI_KBN;
                }
                this.form.UKETSUKE_FLG.Checked = Properties.Settings.Default.UKETSUKE_FLG;
                this.form.UKEIRE_FLG.Checked = Properties.Settings.Default.UKEIRE_FLG;
                this.form.SHUKKA_FLG.Checked = Properties.Settings.Default.SHUKKA_FLG;
                this.form.UR_SH_FLG.Checked = Properties.Settings.Default.UR_SH_FLG;
                this.form.MANI_FLG.Checked = Properties.Settings.Default.MANI_FLG;
                this.form.UNCHIN_FLG.Checked = Properties.Settings.Default.UNCHIN_FLG;
                this.form.DAINOU_FLG.Checked = Properties.Settings.Default.DAINOU_FLG;

                // 取引先
                if (string.IsNullOrEmpty(Properties.Settings.Default.TORIHIKISAKI_CD))
                {
                    this.form.TORIHIKISAKI_CD.Text = "";
                    this.form.TORIHIKISAKI_NAME.Text = "";
                }
                else
                {
                    this.form.TORIHIKISAKI_CD.Text = Properties.Settings.Default.TORIHIKISAKI_CD;
                    M_TORIHIKISAKI data = this.torihikisakiDao.GetDataByCd(this.form.TORIHIKISAKI_CD.Text);
                    if (data != null)
                    {
                        this.form.TORIHIKISAKI_NAME.Text = data.TORIHIKISAKI_NAME_RYAKU;
                    }
                }

                // 業者
                if (string.IsNullOrEmpty(Properties.Settings.Default.GYOUSHA_CD))
                {
                    this.form.GYOUSHA_CD.Text = "";
                    this.form.GYOUSHA_NAME.Text = "";
                }
                else
                {
                    this.form.GYOUSHA_CD.Text = Properties.Settings.Default.GYOUSHA_CD;
                    M_GYOUSHA data = this.gyoushaDao.GetDataByCd(this.form.GYOUSHA_CD.Text);
                    if (data != null)
                    {
                        this.form.GYOUSHA_NAME.Text = data.GYOUSHA_NAME_RYAKU;
                    }
                }

                // 現場
                if (string.IsNullOrEmpty(Properties.Settings.Default.GENBA_CD) || string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    this.form.GENBA_CD.Text = "";
                    this.form.GENBA_NAME.Text = "";
                }
                else
                {
                    this.form.GENBA_CD.Text = Properties.Settings.Default.GENBA_CD;
                    M_GENBA data = new M_GENBA();
                    data.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    data.GENBA_CD = this.form.GENBA_CD.Text;
                    data = this.genbaDao.GetDataByCd(data);
                    if (data != null)
                    {
                        this.form.GENBA_NAME.Text = data.GENBA_NAME_RYAKU;
                    }
                }

                SetEnableByKinou();
                SetEnableByDenpyou();
                SetDateName();

                //並び順ソートヘッダー
                sortSettingInfo.Clear();
                this.form.txboxSortSettingInfo.Text = sortSettingInfo.SortSettingCaption;

                //フィルタ
                searchSettingInfo.Clear();
                this.form.txtSearchSettingInfo.Text = searchSettingInfo.SearchSettingCaption;

                switch (Kbn)
                {
                    case "Initial"://初期表示
                        break;

                    case "ClsSearchCondition"://検索条件をクリア
                        if (this.searchResult != null)
                        {
                            this.searchResult.Clear();
                            this.form.ENTRY_Ichiran.DataSource = this.searchResult;
                        }
                        if (this.searchResult_hasei != null)
                        {
                            this.searchResult_hasei.Clear();
                            this.form.HASEI_Ichiran.DataSource = this.searchResult_hasei;
                        }
                        if (this.searchResult_renkei != null)
                        {
                            this.searchResult_renkei.Clear();
                            this.form.RENKEI_Ichiran.DataSource = this.searchResult_renkei;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 画面遷移

        /// <summary>
        /// 画面遷移
        /// </summary>
        public void FormChanges(int formKbn)
        {
            LogUtility.DebugMethodStart();

            try
            {
                DataGridViewRow row = null;
                string denpyouKbn = "";
                string denpyouNo = "";
                string systemId = "";
                WINDOW_TYPE windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                switch (formKbn)
                {
                    case 1:
                        row = this.form.ENTRY_Ichiran.CurrentRow;
                        if (row == null)
                        {
                            msgLogic.MessageBoxShow("E051", "対象データ");
                            return;
                        }
                        denpyouKbn = Convert.ToString(row.Cells["伝票"].Value);
                        denpyouNo = Convert.ToString(row.Cells["伝票番号"].Value);
                        systemId = Convert.ToString(row.Cells[this.HIDDEN_SYSTEM_ID].Value);
                        break;

                    case 2:
                        row = this.form.HASEI_Ichiran.CurrentRow;
                        if (row == null)
                        {
                            msgLogic.MessageBoxShow("E051", "対象データ");
                            return;
                        }
                        denpyouKbn = Convert.ToString(row.Cells["伝票"].Value);
                        denpyouNo = Convert.ToString(row.Cells["伝票番号"].Value);
                        systemId = Convert.ToString(row.Cells[this.HIDDEN_SYSTEM_ID].Value);
                        break;

                    case 3:
                        row = this.form.RENKEI_Ichiran.CurrentRow;
                        if (row == null)
                        {
                            msgLogic.MessageBoxShow("E051", "対象データ");
                            return;
                        }
                        denpyouKbn = Convert.ToString(row.Cells[0].Value);
                        denpyouNo = Convert.ToString(row.Cells[1].Value);
                        systemId = Convert.ToString(row.Cells[this.HIDDEN_SYSTEM_ID].Value);
                        break;

                    case 4:
                        row = this.form.RENKEI_Ichiran.CurrentRow;
                        if (row == null)
                        {
                            msgLogic.MessageBoxShow("E051", "対象データ");
                            return;
                        }
                        denpyouKbn = Convert.ToString(row.Cells[6].Value);
                        denpyouNo = Convert.ToString(row.Cells[7].Value);
                        systemId = Convert.ToString(row.Cells[this.HIDDEN_SYSTEM_ID_R].Value);
                        break;
                }

                //20151006 hoanghm #11966 未連携の運賃伝票の行を、ダブルクリックまたは [2]連携伝票参照を押下してもシステムエラーが発生しない
                if (!string.IsNullOrEmpty(denpyouNo) || (string.IsNullOrEmpty(denpyouNo) && !string.IsNullOrEmpty(systemId)))
                {
                    switch (denpyouKbn)
                    {
                        case "収集受付":
                            FormManager.OpenFormWithAuth("G015", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyouNo);
                            break;

                        case "出荷受付":
                            FormManager.OpenFormWithAuth("G016", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyouNo);
                            break;

                        case "持込受付":
                            FormManager.OpenFormWithAuth("G018", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyouNo);
                            break;

                        case "受入":
                            if (this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                            {
                                FormManager.OpenFormWithAuth("G051", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyouNo);
                            }
                            else
                            {
                                FormManager.OpenFormWithAuth("G721", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyouNo);
                            }
                            break;

                        case "出荷":
                            if (this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                            {
                                FormManager.OpenFormWithAuth("G053", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyouNo);
                            }
                            else
                            {
                                FormManager.OpenFormWithAuth("G722", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyouNo);
                            }
                            break;

                        case "売上支払":
                            FormManager.OpenFormWithAuth("G054", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyouNo);
                            break;

                        case "マニフェスト":
                            string haikiKbn = "";
                            switch (formKbn)
                            {
                                case 1:
                                case 2:
                                case 3:
                                    haikiKbn = Convert.ToString(row.Cells[this.HIDDEN_HAIKI_KBN_CD].Value);
                                    break;

                                case 4:
                                    haikiKbn = Convert.ToString(row.Cells[this.HIDDEN_HAIKI_KBN_CD_R].Value);
                                    break;
                            }
                            switch (haikiKbn)
                            {
                                case "1":
                                    FormManager.OpenFormWithAuth("G119", windowType, windowType, "", systemId, "", windowType);
                                    break;

                                case "2":
                                    FormManager.OpenFormWithAuth("G121", windowType, windowType, "", systemId, "", windowType);
                                    break;

                                case "3":
                                    FormManager.OpenFormWithAuth("G120", windowType, windowType, "", systemId, "", windowType);
                                    break;
                            }
                            break;

                        case "運賃":
                            FormManager.OpenFormWithAuth("G153", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyouNo);
                            break;

                        case "代納":
                            FormManager.OpenFormWithAuth("G161", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyouNo);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();

            return;
        }

        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void HearerSysInfoInit()
        {
            // システム情報を取得し、初期値をセットする
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
            if (sysInfo != null)
            {
                this.sysInfoEntity = sysInfo[0];
            }
        }

        #endregion

        #region CSV出力

        /// <summary>
        /// CSV出力
        /// </summary>
        public void CSV()
        {
            LogUtility.DebugMethodStart();

            // 一覧に明細行がない場合
            if (this.form.RENKEI_Ichiran.RowCount == 0)
            {
                // アラートを表示し、CSV出力処理はしない
                msgLogic.MessageBoxShow("E044");
            }
            else
            {
                // CSV出力確認メッセージを表示する
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    DataTable table = this.form.RENKEI_Ichiran.DataSource as DataTable;
                    DataTable dt = table.Copy();
                    foreach (DataRow row in dt.Rows)
                    {
                        row[0] = row[this.HIDDEN_DENPYOU_KBN];
                        row[1] = row[this.HIDDEN_DENPYOU_NO];
                        row[2] = row[this.HIDDEN_DENPYOU_DATE];
                        row[3] = row[this.HIDDEN_TORIHIKISAKI_NAME];
                        row[4] = row[this.HIDDEN_GYOUSHA_NAME];
                        row[5] = row[this.HIDDEN_GENBA_NAME];
                    }

                    dt.Columns.Remove(this.HIDDEN_DENPYOU_TYPE);
                    dt.Columns.Remove(this.HIDDEN_DENPYOU_KBN);
                    dt.Columns.Remove(this.HIDDEN_DENPYOU_NO);
                    dt.Columns.Remove(this.HIDDEN_DENPYOU_DATE);
                    dt.Columns.Remove(this.HIDDEN_TORIHIKISAKI_NAME);
                    dt.Columns.Remove(this.HIDDEN_GYOUSHA_NAME);
                    dt.Columns.Remove(this.HIDDEN_GENBA_NAME);
                    dt.Columns.Remove(this.HIDDEN_SYSTEM_ID);
                    dt.Columns.Remove(this.HIDDEN_HAIKI_KBN_CD);
                    dt.Columns.Remove(this.HIDDEN_SYSTEM_ID_R);
                    dt.Columns.Remove(this.HIDDEN_HAIKI_KBN_CD_R);
                    this.form.RENKEI_Ichiran.DataSource = dt;

                    // 共通部品を利用して、画面に表示されているデータをCSVに出力する
                    var CSVExport = new CSVExport();
                    CSVExport.ConvertCustomDataGridViewToCsv(this.form.RENKEI_Ichiran, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_DENPYOU_RENKEI_JOUKYOU_ICHIRAN), this.form);
                    this.form.RENKEI_Ichiran.DataSource = table;
                }
            }

            LogUtility.DebugMethodEnd();

            return;
        }

        #endregion

        #region Validated

        #region 拠点 Validated

        /// <summary>
        /// 拠点 Validated
        /// </summary>
        internal void KYOTEN_CD_Validated()
        {
            string cd = this.headForm.KYOTEN_CD.Text;
            if (cd != this.form.beforeCd || this.form.isError)
            {
                this.form.isError = false;
                if (string.IsNullOrEmpty(cd))
                {
                    this.headForm.KYOTEN_NAME.Text = "";
                    return;
                }

                M_KYOTEN data = new M_KYOTEN();
                data.KYOTEN_CD = Convert.ToInt16(cd);
                data.ISNOT_NEED_DELETE_FLG = true;
                data = kyotenDao.GetAllValidData(data).FirstOrDefault();
                if (data == null)
                {
                    this.form.isError = true;
                    this.headForm.KYOTEN_NAME.Text = "";
                    this.headForm.KYOTEN_CD.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E020", "拠点");
                    this.headForm.KYOTEN_CD.Focus();
                }
                else
                {
                    this.headForm.KYOTEN_NAME.Text = data.KYOTEN_NAME_RYAKU;
                }
            }
        }

        #endregion

        #region 取引先 Validated

        /// <summary>
        /// 取引先 Validated
        /// </summary>
        internal void TORIHIKISAKI_CD_Validated()
        {
            string cd = this.form.TORIHIKISAKI_CD.Text;
            if (cd != this.form.beforeCd || this.form.isError)
            {
                this.form.isError = false;
                if (string.IsNullOrEmpty(cd))
                {
                    this.form.TORIHIKISAKI_NAME.Text = "";
                    return;
                }

                M_TORIHIKISAKI data = torihikisakiDao.GetDataByCd(cd);
                if (data == null)
                {
                    this.form.isError = true;
                    this.form.TORIHIKISAKI_NAME.Text = "";
                    this.form.TORIHIKISAKI_CD.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E020", "取引先");
                    this.form.TORIHIKISAKI_CD.Focus();
                }
                else
                {
                    this.form.TORIHIKISAKI_NAME.Text = data.TORIHIKISAKI_NAME_RYAKU;
                }
            }
        }

        #endregion

        #region 業者 Validated

        /// <summary>
        /// 業者 Validated
        /// </summary>
        internal void GYOUSHA_CD_Validated()
        {
            string cd = this.form.GYOUSHA_CD.Text;
            if (cd != this.form.beforeCd || this.form.isError)
            {
                this.form.isError = false;

                if (string.IsNullOrEmpty(cd))
                {
                    this.form.GYOUSHA_NAME.Text = "";
                    this.form.GENBA_CD.Text = "";
                    this.form.GENBA_NAME.Text = "";
                    return;
                }

                M_GYOUSHA data = gyoushaDao.GetDataByCd(cd);
                if (data == null
                    || (!string.IsNullOrEmpty(this.form.DENPYOU_KBN.Text) && data.GYOUSHAKBN_MANI.IsFalse)
                    || (this.form.DENPYOU_KBN.Text == "5" && data.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsFalse)
                    || (this.form.DENPYOU_KBN.Text == "6" && data.UNPAN_JUTAKUSHA_KAISHA_KBN.IsFalse))
                {
                    this.form.isError = true;
                    this.form.GYOUSHA_NAME.Text = "";
                    this.form.GENBA_CD.Text = "";
                    this.form.GENBA_NAME.Text = "";
                    this.form.GYOUSHA_CD.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.form.GYOUSHA_CD.Focus();
                }
                else
                {
                    this.form.GYOUSHA_NAME.Text = data.GYOUSHA_NAME_RYAKU;
                    this.form.GENBA_CD.Text = "";
                    this.form.GENBA_NAME.Text = "";
                }
            }
        }

        #endregion

        #region 現場 Validated

        /// <summary>
        /// 現場 Validated
        /// </summary>
        internal void GENBA_CD_Validated()
        {
            string gyoushaCd = this.form.GYOUSHA_CD.Text;
            string genbaCd = this.form.GENBA_CD.Text;
            if (genbaCd != this.form.beforeCd || this.form.isError)
            {
                this.form.isError = false;

                if (string.IsNullOrEmpty(genbaCd))
                {
                    this.form.GENBA_NAME.Text = "";
                    return;
                }

                if (string.IsNullOrEmpty(gyoushaCd))
                {
                    this.form.isError = true;
                    msgLogic.MessageBoxShow("E051", "業者");
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_NAME.Text = "";
                    this.form.GENBA_CD.Focus();
                    return;
                }

                M_GENBA genba = new M_GENBA();
                genba.GYOUSHA_CD = gyoushaCd;
                genba.GENBA_CD = genbaCd;
                genba = genbaDao.GetDataByCd(genba);
                if (genba == null || (this.form.DENPYOU_KBN.Text == "5" && genba.HAISHUTSU_NIZUMI_GENBA_KBN.IsFalse))
                {
                    this.form.isError = true;
                    this.form.GENBA_NAME.Text = "";
                    this.form.GENBA_CD.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.form.GENBA_CD.Focus();
                }
                else
                {
                    this.form.GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                }
                return;
            }
        }

        #endregion

        #endregion

        #region 活性化設定 機能別

        internal void SetEnableByKinou()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            if (this.searchResult != null)
            {
                this.searchResult.Clear();
                this.form.ENTRY_Ichiran.DataSource = this.searchResult;
            }
            if (this.searchResult_hasei != null)
            {
                this.searchResult_hasei.Clear();
                this.form.HASEI_Ichiran.DataSource = this.searchResult_hasei;
            }
            if (this.searchResult_renkei != null)
            {
                this.searchResult_renkei.Clear();
                this.form.RENKEI_Ichiran.DataSource = this.searchResult_renkei;
            }
            if (this.form.KINOU_KBN_2.Checked)
            {
                this.form.DENPYOU_KBN_5.Enabled = false;
                this.form.DENPYOU_KBN_6.Enabled = false;
                this.form.PL_SHORI_KBN.Enabled = true;
                this.form.PL_SHORI_KBN.BackColor = this.normalColor;
                this.form.RENKEI_KBN.BackColor = Constans.NOMAL_COLOR;
                this.form.PL_SHORI_TAISHOU.Enabled = false;
                this.form.PL_SHORI_TAISHOU.BackColor = Constans.DISABLE_COLOR;
                this.form.UKETSUKE_FLG.BackColor = Constans.DISABLE_COLOR;
                this.form.UKEIRE_FLG.BackColor = Constans.DISABLE_COLOR;
                this.form.SHUKKA_FLG.BackColor = Constans.DISABLE_COLOR;
                this.form.UR_SH_FLG.BackColor = Constans.DISABLE_COLOR;
                this.form.MANI_FLG.BackColor = Constans.DISABLE_COLOR;
                this.form.UNCHIN_FLG.BackColor = Constans.DISABLE_COLOR;
                this.form.DAINOU_FLG.BackColor = Constans.DISABLE_COLOR;
                this.form.ENTRY_Ichiran.Visible = false;
                this.form.HASEI_Ichiran.Visible = false;
                this.form.RENKEI_Ichiran.Visible = true;
                for (int i = 6; i < this.form.RENKEI_Ichiran.Columns.Count; i++)
                {
                    DataGridViewColumn col = this.form.RENKEI_Ichiran.Columns[i];
                    col.HeaderCell.Style.BackColor = bullColor;
                }
                this.sortSettingInfo = SortSettingHelper.LoadSortSettingInfo("UIForm.RENKEI_Ichiran");
                this.sortSettingInfo.Clear();
                this.form.txboxSortSettingInfo.Text = sortSettingInfo.SortSettingCaption;

                this.searchSettingInfo = SearchSettingHelper.LoadSearchSettingInfo("UIForm.RENKEI_Ichiran");
                this.searchSettingInfo.Clear();
                this.form.txtSearchSettingInfo.Text = searchSettingInfo.SearchSettingCaption;

                parentForm.bt_process2.Text = "[2] 連携伝票参照";
                parentForm.bt_process3.Enabled = true;
                parentForm.bt_process3.Text = "[3] CSV出力";
            }
            else
            {
                this.form.DENPYOU_KBN_5.Enabled = true;
                this.form.DENPYOU_KBN_6.Enabled = true;
                this.form.PL_SHORI_KBN.Enabled = false;
                this.form.PL_SHORI_KBN.BackColor = Constans.DISABLE_COLOR;
                this.form.PL_SHORI_TAISHOU.Enabled = true;
                this.form.PL_SHORI_TAISHOU.BackColor = this.normalColor;
                this.form.UKETSUKE_FLG.BackColor = this.normalColor;
                this.form.UKEIRE_FLG.BackColor = this.normalColor;
                this.form.SHUKKA_FLG.BackColor = this.normalColor;
                this.form.UR_SH_FLG.BackColor = this.normalColor;
                this.form.MANI_FLG.BackColor = this.normalColor;
                this.form.UNCHIN_FLG.BackColor = this.normalColor;
                this.form.DAINOU_FLG.BackColor = this.normalColor;
                this.form.ENTRY_Ichiran.Visible = true;
                this.form.HASEI_Ichiran.Visible = true;
                this.form.RENKEI_Ichiran.Visible = false;
                this.sortSettingInfo = SortSettingHelper.LoadSortSettingInfo("UIForm.ENTRY_Ichiran");
                sortSettingInfo.Clear();
                this.form.txboxSortSettingInfo.Text = sortSettingInfo.SortSettingCaption;
                this.searchSettingInfo = SearchSettingHelper.LoadSearchSettingInfo("UIForm.ENTRY_Ichiran");
                searchSettingInfo.Clear();
                this.form.txboxSortSettingInfo.Text = searchSettingInfo.SearchSettingCaption;
                parentForm.bt_process2.Text = "[2] 派生伝票参照";
                parentForm.bt_process3.Enabled = false;
                parentForm.bt_process3.Text = "";
            }
        }

        #endregion

        #region 活性化設定 伝票別

        internal void SetEnableByDenpyou()
        {
            if (this.form.DENPYOU_KBN_6.Checked)
            {
                this.form.TORIHIKISAKI_CD.Enabled = false;
                this.form.TORIHIKISAKI_NAME.Enabled = false;
                this.form.TORIHIKISAKI_POPUP.Enabled = false;
                this.form.GENBA_CD.Enabled = false;
                this.form.GENBA_NAME.Enabled = false;
                this.form.GENBA_POPUP.Enabled = false;
                this.form.TORIHIKISAKI_CD.Text = "";
                this.form.TORIHIKISAKI_NAME.Text = "";
                this.form.GENBA_CD.Text = "";
                this.form.GENBA_NAME.Text = "";
            }
            else
            {
                this.form.TORIHIKISAKI_CD.Enabled = true;
                this.form.TORIHIKISAKI_NAME.Enabled = true;
                this.form.TORIHIKISAKI_POPUP.Enabled = true;
                this.form.GENBA_CD.Enabled = true;
                this.form.GENBA_NAME.Enabled = true;
                this.form.GENBA_POPUP.Enabled = true;
            }
        }

        #endregion

        #region 日付名設定

        internal void SetDateName()
        {
            if (this.form.DENPYOU_KBN_1.Checked)
            {
                this.form.LBL_DENPYOU_NO.Text = "伝票番号";
                this.form.LBL_DENPYOU_DATE.Text = "作業日";
                this.form.LBL_GYOUSHA.Text = "業者";
                this.form.LBL_GENBA.Text = "現場";
            }
            else if (this.form.DENPYOU_KBN_5.Checked)
            {
                this.form.LBL_DENPYOU_NO.Text = "交付番号";
                this.form.LBL_DENPYOU_DATE.Text = "交付年月日";
                this.form.LBL_GYOUSHA.Text = "排出事業者";
                this.form.LBL_GENBA.Text = "排出事業場";
            }
            else if (this.form.DENPYOU_KBN_6.Checked)
            {
                this.form.LBL_DENPYOU_NO.Text = "伝票番号";
                this.form.LBL_DENPYOU_DATE.Text = "伝票日付";
                this.form.LBL_GYOUSHA.Text = "運搬業者";
                this.form.LBL_GENBA.Text = "現場";
            }
            else
            {
                this.form.LBL_DENPYOU_NO.Text = "伝票番号";
                this.form.LBL_DENPYOU_DATE.Text = "伝票日付";
                this.form.LBL_GYOUSHA.Text = "業者";
                this.form.LBL_GENBA.Text = "現場";
            }
            if (this.searchResult != null)
            {
                this.searchResult.Clear();
                this.form.ENTRY_Ichiran.DataSource = this.searchResult;
            }
            if (this.searchResult_hasei != null)
            {
                this.searchResult_hasei.Clear();
                this.form.HASEI_Ichiran.DataSource = this.searchResult_hasei;
            }
            if (this.searchResult_renkei != null)
            {
                this.searchResult_renkei.Clear();
                this.form.RENKEI_Ichiran.DataSource = this.searchResult_renkei;
            }
        }

        #endregion

        #region ソート列取得

        public void SetDataGridViewColumns_E(DataGridView grid)
        {
            // ソート用に表示カラムのみコピー
            var gridColumns = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn gridColumn in grid.Columns)
            {
                if (gridColumn.Visible)
                {
                    gridColumns.Add(gridColumn);
                }
            }

            // 表示インデックスでソート
            gridColumns.Sort(
                delegate(DataGridViewColumn x, DataGridViewColumn y)
                {
                    return x.DisplayIndex - y.DisplayIndex;
                }
            );

            // グリッドの表示列タイトルでリスト作成
            this.sortSettingInfo.ViewColumns.Clear();
            foreach (DataGridViewColumn gridColumn in gridColumns)
            {
                var viewColumn = new CustomSortColumn(gridColumn.Name, gridColumn.HeaderText, true);
                if (gridColumn.HeaderText == "伝票")
                {
                    viewColumn = new CustomSortColumn(this.HIDDEN_DENPYOU_KBN, gridColumn.HeaderText, true);
                }
                this.sortSettingInfo.ViewColumns.Add(viewColumn);
            }

            // 存在しているものだけをソート項目に残す
            CustomSortColumn[] tempColumns = new CustomSortColumn[6];
            this.sortSettingInfo.SortColumns.CopyTo(tempColumns);
            this.sortSettingInfo.SortColumns.Clear();
            foreach (var sortColumn in tempColumns)
            {
                if (sortColumn == null)
                {
                    continue;
                }
                foreach (var col in this.sortSettingInfo.ViewColumns)
                {
                    if (col.Name.Equals(sortColumn.Name))
                    {
                        this.sortSettingInfo.SortColumns.Add(sortColumn);
                        break;
                    }
                }
            }
        }

        public void SetDataGridViewColumns_R(DataGridView grid)
        {
            //// ソート用に表示カラムのみコピー
            //var gridColumns = new List<DataGridViewColumn>();
            //DataGridViewColumn gridColumn = null;
            //gridColumn = grid.Columns[this.HIDDEN_DENPYOU_KBN];
            //gridColumns.Add(gridColumn);
            //gridColumn = grid.Columns[this.HIDDEN_DENPYOU_NO];
            //gridColumns.Add(gridColumn);
            //gridColumn = grid.Columns[this.HIDDEN_DENPYOU_DATE];
            //gridColumns.Add(gridColumn);
            //gridColumn = grid.Columns[this.HIDDEN_TORIHIKISAKI_NAME];
            //gridColumns.Add(gridColumn);
            //gridColumn = grid.Columns[this.HIDDEN_GYOUSHA_NAME];
            //gridColumns.Add(gridColumn);
            //gridColumn = grid.Columns[this.HIDDEN_GENBA_NAME];
            //gridColumns.Add(gridColumn);

            //// 表示インデックスでソート
            //gridColumns.Sort(
            //    delegate(DataGridViewColumn x, DataGridViewColumn y)
            //    {
            //        return x.DisplayIndex - y.DisplayIndex;
            //    }
            //);

            // グリッドの表示列タイトルでリスト作成
            this.sortSettingInfo.ViewColumns.Clear();
            var viewColumn = new CustomSortColumn(this.HIDDEN_DENPYOU_TYPE, "伝票", true);
            this.sortSettingInfo.ViewColumns.Add(viewColumn);
            viewColumn = new CustomSortColumn(this.HIDDEN_DENPYOU_NO, "伝票番号", true);
            this.sortSettingInfo.ViewColumns.Add(viewColumn);
            viewColumn = new CustomSortColumn(this.HIDDEN_DENPYOU_DATE, "伝票日付", true);
            this.sortSettingInfo.ViewColumns.Add(viewColumn);
            viewColumn = new CustomSortColumn(this.HIDDEN_TORIHIKISAKI_NAME, "取引先", true);
            this.sortSettingInfo.ViewColumns.Add(viewColumn);
            viewColumn = new CustomSortColumn(this.HIDDEN_GYOUSHA_NAME, "業者", true);
            this.sortSettingInfo.ViewColumns.Add(viewColumn);
            viewColumn = new CustomSortColumn(this.HIDDEN_GENBA_NAME, "現場", true);
            this.sortSettingInfo.ViewColumns.Add(viewColumn);

            // 存在しているものだけをソート項目に残す
            CustomSortColumn[] tempColumns = new CustomSortColumn[6];
            this.sortSettingInfo.SortColumns.CopyTo(tempColumns);
            this.sortSettingInfo.SortColumns.Clear();
            foreach (var sortColumn in tempColumns)
            {
                if (sortColumn == null)
                {
                    continue;
                }
                foreach (var col in this.sortSettingInfo.ViewColumns)
                {
                    if (col.Name.Equals(sortColumn.Name))
                    {
                        this.sortSettingInfo.SortColumns.Add(sortColumn);
                        break;
                    }
                }
            }
        }

        #endregion

        #region ソート

        /// <summary>
        /// ソート
        /// </summary>
        public void SortDataTable(DataTable dataTable)
        {
            if (dataTable == null)
            {
                return;
            }

            if (sortSettingInfo == null)
            {
                return;
            }

            sortSettingInfo.SetDataTableColumns(dataTable);
            var sb = new StringBuilder();
            string name = "";

            foreach (var item in sortSettingInfo.SortColumns)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }
                name = item.Name;
                sb.AppendFormat("{0} {1}", name, item.IsAsc ? "ASC" : "DESC");
            }

            dataTable.DefaultView.Sort = sb.ToString();
        }

        #endregion

        #region チェック

        #region 日付チェック

        internal bool DateCheck()
        {
            if (string.IsNullOrEmpty(this.form.DATE_FROM.Text) || string.IsNullOrEmpty(this.form.DATE_TO.Text))
            {
                return true;
            }

            DateTime from = Convert.ToDateTime(this.form.DATE_FROM.Text);
            DateTime to = Convert.ToDateTime(this.form.DATE_TO.Text);

            if (from.CompareTo(to) > 0)
            {
                this.form.DATE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.DATE_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { this.form.LBL_DENPYOU_DATE.Text + "From", this.form.LBL_DENPYOU_DATE.Text + "To" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.DATE_FROM.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region 必須チェック

        internal bool DataCheck()
        {
            if (!DateCheck())
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.form.DATE_FROM.Text) && string.IsNullOrEmpty(this.form.DATE_TO.Text)
                && string.IsNullOrEmpty(this.form.DENPYOU_NO.Text))
            {
                this.form.DATE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.DATE_TO.BackColor = Constans.ERROR_COLOR;
                this.form.DENPYOU_NO.BackColor = Constans.ERROR_COLOR;
                msgLogic.MessageBoxShow("E001", string.Format("{0}FromTo、もしくは{1}のどちらか", this.form.LBL_DENPYOU_DATE.Text, this.form.LBL_DENPYOU_NO.Text));
                this.form.DATE_FROM.Focus();
                return false;
            }
            else if (this.form.KINOU_KBN_2.Checked && string.IsNullOrEmpty(this.form.RENKEI_KBN.Text))
            {
                this.form.RENKEI_KBN.BackColor = Constans.ERROR_COLOR;
                msgLogic.MessageBoxShow("E001", "処理区分");
                this.form.RENKEI_KBN.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #endregion

        #region 一覧設定

        internal void SetGridView()
        {
            string denpyouKbn = "";
            string denpyouNo = "";
            string denpyouDate = "";
            string torihikisaki = "";
            string gyousha = "";
            string genba = "";
            string denpyouKbnTmp = "";
            string denpyouNoTmp = "";
            string denpyouDateTmp = "";
            string torihikisakiTmp = "";
            string gyoushaTmp = "";
            string genbaTmp = "";
            DataGridViewRow row = null;
            for (int i = 0; i < this.form.RENKEI_Ichiran.Rows.Count; i++)
            {
                row = this.form.RENKEI_Ichiran.Rows[i];
                denpyouKbnTmp = Convert.ToString(row.Cells[this.HIDDEN_DENPYOU_KBN].Value);
                denpyouNoTmp = Convert.ToString(row.Cells[this.HIDDEN_DENPYOU_NO].Value);
                denpyouDateTmp = Convert.ToString(row.Cells[this.HIDDEN_DENPYOU_DATE].Value);
                torihikisakiTmp = Convert.ToString(row.Cells[this.HIDDEN_TORIHIKISAKI_NAME].Value);
                gyoushaTmp = Convert.ToString(row.Cells[this.HIDDEN_GYOUSHA_NAME].Value);
                genbaTmp = Convert.ToString(row.Cells[this.HIDDEN_GENBA_NAME].Value);
                if (denpyouKbnTmp == denpyouKbn && denpyouNoTmp == denpyouNo && denpyouDateTmp == denpyouDate
                    && torihikisakiTmp == torihikisaki && gyoushaTmp == gyousha && genbaTmp == genba)
                {
                    row.Cells[0].Value = "";
                    row.Cells[1].Value = DBNull.Value;
                    row.Cells[2].Value = "";
                    row.Cells[3].Value = "";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = "";
                }
                else
                {
                    denpyouKbn = denpyouKbnTmp;
                    denpyouNo = denpyouNoTmp;
                    denpyouDate = denpyouDateTmp;
                    torihikisaki = torihikisakiTmp;
                    gyousha = gyoushaTmp;
                    genba = genbaTmp;
                }
            }
        }

        #endregion

        #region 既存

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 20150730 hoanghm add

        public void SetColumnsToSearchColumns_E(DataGridView grid)
        {
            // 抽出用に表示カラムのみコピー
            var gridColumns = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn gridColumn in grid.Columns)
            {
                if (gridColumn.Visible)
                {
                    gridColumns.Add(gridColumn);
                }
            }

            // 表示インデックスでソート
            gridColumns.Sort(
                delegate(DataGridViewColumn x, DataGridViewColumn y)
                {
                    return x.DisplayIndex - y.DisplayIndex;
                }
            );

            // グリッドの表示列タイトルでリスト作成
            this.searchSettingInfo.ViewColumns.Clear();
            foreach (DataGridViewColumn gridColumn in gridColumns)
            {
                var viewColumn = new CustomSearchColumn(gridColumn.HeaderText, gridColumn.HeaderText, "", gridColumn.ValueType);
                searchSettingInfo.ViewColumns.Add(viewColumn);
            }
            // 存在しているものだけをソート項目に残す
            CustomSearchColumn[] tempColumns = new CustomSearchColumn[6];
            this.searchSettingInfo.SearchColumns.CopyTo(tempColumns);
            this.searchSettingInfo.SearchColumns.Clear();
            foreach (var sortColumn in tempColumns)
            {
                if (sortColumn == null)
                {
                    continue;
                }
                foreach (var col in this.searchSettingInfo.ViewColumns)
                {
                    if (col.Name.Equals(sortColumn.Name))
                    {
                        this.searchSettingInfo.SearchColumns.Add(sortColumn);
                        break;
                    }
                }
            }
        }

        public void SetColumnsToSearchColumns_R(DataGridView grid)
        {
            //// ソート用に表示カラムのみコピー
            //var gridColumns = new List<DataGridViewColumn>();
            //DataGridViewColumn gridColumn = null;
            //gridColumn = grid.Columns[this.HIDDEN_DENPYOU_KBN];
            //gridColumns.Add(gridColumn);
            //gridColumn = grid.Columns[this.HIDDEN_DENPYOU_NO];
            //gridColumns.Add(gridColumn);
            //gridColumn = grid.Columns[this.HIDDEN_DENPYOU_DATE];
            //gridColumns.Add(gridColumn);
            //gridColumn = grid.Columns[this.HIDDEN_TORIHIKISAKI_NAME];
            //gridColumns.Add(gridColumn);
            //gridColumn = grid.Columns[this.HIDDEN_GYOUSHA_NAME];
            //gridColumns.Add(gridColumn);
            //gridColumn = grid.Columns[this.HIDDEN_GENBA_NAME];
            //gridColumns.Add(gridColumn);

            //// 表示インデックスでソート
            //gridColumns.Sort(
            //    delegate(DataGridViewColumn x, DataGridViewColumn y)
            //    {
            //        return x.DisplayIndex - y.DisplayIndex;
            //    }
            //);

            // グリッドの表示列タイトルでリスト作成
            this.searchSettingInfo.ViewColumns.Clear();
            var viewColumn = new CustomSearchColumn("伝票", "伝票", "", typeof(String));
            this.searchSettingInfo.ViewColumns.Add(viewColumn);
            viewColumn = new CustomSearchColumn("伝票番号", "伝票番号", "", typeof(Int64));
            this.searchSettingInfo.ViewColumns.Add(viewColumn);
            viewColumn = new CustomSearchColumn("伝票日付", "伝票日付", "", typeof(String));
            this.searchSettingInfo.ViewColumns.Add(viewColumn);
            viewColumn = new CustomSearchColumn("取引先", "取引先", "", typeof(String));
            this.searchSettingInfo.ViewColumns.Add(viewColumn);
            viewColumn = new CustomSearchColumn("業者", "業者", "", typeof(String));
            this.searchSettingInfo.ViewColumns.Add(viewColumn);
            viewColumn = new CustomSearchColumn("現場", "現場", "", typeof(String));
            this.searchSettingInfo.ViewColumns.Add(viewColumn);

            // 存在しているものだけをソート項目に残す
            CustomSearchColumn[] tempColumns = new CustomSearchColumn[6];
            this.searchSettingInfo.SearchColumns.CopyTo(tempColumns);
            this.searchSettingInfo.SearchColumns.Clear();
            foreach (var searchColumn in tempColumns)
            {
                if (searchColumn == null)
                {
                    continue;
                }
                foreach (var col in this.searchSettingInfo.ViewColumns)
                {
                    if (col.Name.Equals(searchColumn.Name))
                    {
                        this.searchSettingInfo.SearchColumns.Add(searchColumn);
                        break;
                    }
                }
            }
        }

        public void SearchDataTable(DataTable dataTable, SearchSettingInfo searchSettingInfo)
        {
            if (dataTable == null)
            {
                return;
            }

            if (searchSettingInfo == null)
            {
                return;
            }

            searchSettingInfo.SetDataTableColumns(dataTable);
            this.form.txtSearchSettingInfo.Text = searchSettingInfo.SearchSettingCaption;
            var sb = new StringBuilder();

            string errorMessage = string.Empty;
            var hasError = HasErrorSearchColumns(searchSettingInfo, dataTable, out errorMessage);

            if (hasError)
            {
                // 条件にエラーがある場合は、アラート表示後フィルタ無しで抽出
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("W006", errorMessage);
            }
            else
            {
                foreach (var item in searchSettingInfo.SearchColumns)
                {
                    string rowFilter = CreateRowFilterItem(dataTable, item);
                    if (!string.IsNullOrEmpty(rowFilter))
                    {
                        if (0 < sb.Length)
                        {
                            sb.Append(" AND ");
                        }

                        sb.Append(rowFilter);
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine(sb);

            dataTable.CaseSensitive = true;
            string sql = sb.ToString();
            dataTable.DefaultView.RowFilter = sql;
        }

        private bool HasErrorSearchColumns(SearchSettingInfo settingInfo, DataTable dataTable, out string errorMessage)
        {
            errorMessage = string.Empty;
            bool hasError = false;
            List<string> errList = new List<string>();

            foreach (var item in settingInfo.SearchColumns)
            {
                var t = dataTable.Columns[item.Name].DataType;

                if (t == typeof(String))
                {
                    if (item.Name.Contains("日付"))
                    {
                        foreach (var inputValue in item.Filter.Split(','))
                        {
                            if (this.HasErrorDateTime(inputValue))
                            {
                                errList.Add(item.Name);

                                hasError = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (var inputValue in item.Filter.Split(','))
                        {
                            if (string.IsNullOrWhiteSpace(inputValue))
                            {
                                errList.Add(item.Name);

                                hasError = true;
                                break;
                            }
                        }
                    }
                }
                else if (t == typeof(DateTime))
                {
                    foreach (var inputValue in item.Filter.Split(','))
                    {
                        if (this.HasErrorDateTime(inputValue))
                        {
                            errList.Add(item.Name);

                            hasError = true;
                            break;
                        }
                    }
                }
                else if (t == typeof(Int16))
                {
                    foreach (var inputValue in item.Filter.Split(','))
                    {
                        Int16 result;
                        if (!Int16.TryParse(inputValue, out result))
                        {
                            errList.Add(item.Name);

                            hasError = true;
                            break;
                        }
                    }
                }
                else if (t == typeof(Int32))
                {
                    foreach (var inputValue in item.Filter.Split(','))
                    {
                        Int32 result;
                        if (!Int32.TryParse(inputValue, out result))
                        {
                            errList.Add(item.Name);

                            hasError = true;
                            break;
                        }
                    }
                }
                else if (t == typeof(Int64))
                {
                    foreach (var inputValue in item.Filter.Split(','))
                    {
                        Int64 result;
                        if (!Int64.TryParse(inputValue, out result))
                        {
                            errList.Add(item.Name);

                            hasError = true;
                            break;
                        }
                    }
                }
                else if (t == typeof(Double))
                {
                    foreach (var inputValue in item.Filter.Split(','))
                    {
                        Double result;
                        if (!Double.TryParse(inputValue, out result))
                        {
                            errList.Add(item.Name);

                            hasError = true;
                            break;
                        }
                    }
                }
                else if (t == typeof(Decimal))
                {
                    foreach (var inputValue in item.Filter.Split(','))
                    {
                        Decimal result;
                        if (!Decimal.TryParse(inputValue, out result))
                        {
                            errList.Add(item.Name);

                            hasError = true;
                            break;
                        }
                    }
                }
                else if (t == typeof(Boolean))
                {
                    foreach (var inputValue in item.Filter.Split(','))
                    {
                        Boolean result;
                        if (!Boolean.TryParse(inputValue, out result))
                        {
                            errList.Add(item.Name);

                            hasError = true;
                            break;
                        }
                    }
                }
            }

            var sb = new StringBuilder();
            foreach (var err in errList)
            {
                if (0 < sb.Length)
                {
                    sb.Append(", ");
                }

                sb.Append(err);
            }
            errorMessage = sb.ToString();

            return hasError;
        }

        private string CreateRowFilterItem(DataTable dataTable, CustomSearchColumn column)
        {
            if (dataTable == null || column == null)
            {
                return null;
            }

            var sb = new StringBuilder();
            var t = dataTable.Columns[column.Name].DataType;

            if (t == typeof(String) && !column.Name.Contains("日付"))
            {
                foreach (var str in column.Filter.Split(','))
                {
                    if (0 < sb.Length)
                    {
                        sb.Append(" OR ");
                    }
                    else
                    {
                        sb.Append("( ");
                    }

                    // LIKE検索用にエスケープ。ToEscapeStrは不可
                    StringBuilder sbLike = new StringBuilder();
                    for (int i = 0; i < str.Trim().Length; i++)
                    {
                        char c = str[i];
                        if (c == '*' || c == '%' || c == '[' || c == ']')
                        {
                            sbLike.Append("[").Append(c).Append("]");
                        }
                        else if (c == '\'')
                        {
                            sbLike.Append("''");
                        }
                        else
                        {
                            sbLike.Append(c);
                        }
                    }
                    string item = string.Format("'%{0}%'", sbLike.ToString());
                    string name = ToEscapeStr(column.Name);

                    sb.AppendFormat("{0} LIKE {1}", name, item);
                }

                if (0 < sb.Length)
                {
                    sb.AppendFormat(")");
                }
                return sb.ToString();
            }
            else if (t == typeof(DateTime) || column.Name.Contains("日付"))
            {
                foreach (var str in column.Filter.Split(','))
                {
                    if (0 < sb.Length)
                    {
                        sb.Append(" OR ");
                    }
                    else
                    {
                        sb.Append("( ");
                    }

                    DateTime dt;

                    if (!DateTime.TryParse(str, out dt))
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("W006", column.Name);
                        return "";
                    }

                    DateTime dtMin = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
                    DateTime dtMax = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);

                    string name = ToEscapeStr(column.Name);

                    // 作成日時、最終更新日時(CREATE_DATE,UPDATE_DATE)等、
                    // 時分秒をもつ日付も考慮して、0:00:00～23:59:59の範囲検索
                    sb.AppendFormat("(#{0}# <= {1} AND {1} <= #{2}#)", dtMin, name, dtMax);
                }

                if (0 < sb.Length)
                {
                    sb.AppendFormat(")");
                }
                return sb.ToString();
            }
            else
            {
                // 数値系の条件作成を想定
                foreach (var str in column.Filter.Split(','))
                {
                    if (0 < sb.Length)
                    {
                        sb.Append(", ");
                    }

                    sb.AppendFormat("{0}", str.Trim());
                }

                string name = ToEscapeStr(column.Name);

                var filter = string.Format("{0} IN ({1})", name, sb);
                return filter;
            }
        }

        private string ToEscapeStr(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            string result = str;

            // エスケープ対象の特殊文字
            var kigos = "~()#\\=><+-*%&|^\"[]!,.\'`{}?/:;@";

            foreach (var kigo in kigos)
            {
                if (result.Contains(kigo))
                {
                    result = "[" + result + "]";
                    break;
                }
            }

            return result;
        }

        private bool HasErrorDateTime(string inputValue)
        {
            DateTime time;
            if (!DateTime.TryParse(inputValue, out time))
            {
                return true;
            }

            // YYYY/MM チェック
            if (System.Text.RegularExpressions.Regex.IsMatch(inputValue,
                @"^(\d{4}|\d{3})(\/|-|\.)([1-9]|0[1-9]|1[012])$"))
            {
                // YYYY/MMの場合、YYYY/MM/01に変換されるが月単位で検索が可能だと誤解される恐れがあるため
                return true;
            }

            // 時分秒(HH:MM:SS等) チェック
            if (inputValue.Contains(":"))
            {
                // YYYY/MM/DD HH:MM:SS
                // YYYY/MM/DD HH:MM
                // HH:MM:SS
                // HH:MM

                // 時分秒に関しては、基本設定されずにデータ登録されているので入力時はエラーとする
                return true;
            }

            return false;
        }

        internal DataTable CreateDataTable(DataTable inputTable)
        {
            string denpyouKbn = "";
            string denpyouNo = "";
            string denpyouDate = "";
            string torihikisaki = "";
            string gyousha = "";
            string genba = "";
            string denpyouKbnTmp = "";
            string denpyouNoTmp = "";
            string denpyouDateTmp = "";
            string torihikisakiTmp = "";
            string gyoushaTmp = "";
            string genbaTmp = "";
            DataRow row;
            for (int i = 0; i < inputTable.Rows.Count; i++)
            {
                row = inputTable.Rows[i];
                denpyouKbnTmp = Convert.ToString(row[this.HIDDEN_DENPYOU_KBN]);
                denpyouNoTmp = Convert.ToString(row[this.HIDDEN_DENPYOU_NO]);
                denpyouDateTmp = Convert.ToString(row[this.HIDDEN_DENPYOU_DATE]);
                torihikisakiTmp = Convert.ToString(row[this.HIDDEN_TORIHIKISAKI_NAME]);
                gyoushaTmp = Convert.ToString(row[this.HIDDEN_GYOUSHA_NAME]);
                genbaTmp = Convert.ToString(row[this.HIDDEN_GENBA_NAME]);
                if (denpyouKbnTmp == denpyouKbn && denpyouNoTmp == denpyouNo && denpyouDateTmp == denpyouDate
                    && torihikisakiTmp == torihikisaki && gyoushaTmp == gyousha && genbaTmp == genba)
                {
                    row[0] = "";
                    row[1] = DBNull.Value;
                    row[2] = DBNull.Value;
                    row[3] = "";
                    row[4] = "";
                    row[5] = "";
                }
                else
                {
                    denpyouKbn = denpyouKbnTmp;
                    denpyouNo = denpyouNoTmp;
                    denpyouDate = denpyouDateTmp;
                    torihikisaki = torihikisakiTmp;
                    gyousha = gyoushaTmp;
                    genba = genbaTmp;
                }
            }
            return inputTable;
        }

        #endregion

        #region 業者、現場 POPUP設定

        /// <summary>
        /// 業者、現場 POPUP設定
        /// </summary>
        internal void SetGyoushaAndGenbaPopup()
        {
            this.form.GYOUSHA_CD.PopupSearchSendParams.Clear();
            this.form.GYOUSHA_POPUP.PopupSearchSendParams.Clear();

            if (!string.IsNullOrEmpty(this.form.DENPYOU_KBN.Text))
            {
                PopupSearchSendParamDto searchDto1 = new PopupSearchSendParamDto();
                PopupSearchSendParamDto searchDto2 = new PopupSearchSendParamDto();

                searchDto1.And_Or = CONDITION_OPERATOR.AND;
                searchDto1.KeyName = "GYOUSHAKBN_MANI";
                searchDto1.Value = "TRUE";

                this.form.GYOUSHA_CD.PopupSearchSendParams.Add(searchDto1);
                this.form.GYOUSHA_POPUP.PopupSearchSendParams.Add(searchDto1);

                switch (this.form.DENPYOU_KBN.Text)
                {
                    // マニフェスト
                    case "5":
                        searchDto2.And_Or = CONDITION_OPERATOR.AND;
                        searchDto2.KeyName = "HAISHUTSU_NIZUMI_GYOUSHA_KBN";
                        searchDto2.Value = "TRUE";

                        this.form.GYOUSHA_CD.PopupSearchSendParams.Add(searchDto2);
                        this.form.GYOUSHA_POPUP.PopupSearchSendParams.Add(searchDto2);
                        break;
                    // 運賃
                    case "6":
                        searchDto2.And_Or = CONDITION_OPERATOR.AND;
                        searchDto2.KeyName = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                        searchDto2.Value = "TRUE";

                        this.form.GYOUSHA_CD.PopupSearchSendParams.Add(searchDto2);
                        this.form.GYOUSHA_POPUP.PopupSearchSendParams.Add(searchDto2);
                        break;

                    default:
                        break;
                }
            }

            this.form.GENBA_CD.PopupSearchSendParams.Clear();
            this.form.GENBA_POPUP.PopupSearchSendParams.Clear();

            PopupSearchSendParamDto searchDto3 = new PopupSearchSendParamDto();
            searchDto3.And_Or = CONDITION_OPERATOR.AND;
            searchDto3.Control = "GYOUSHA_CD";
            searchDto3.KeyName = "GYOUSHA_CD";

            this.form.GENBA_CD.PopupSearchSendParams.Add(searchDto3);
            this.form.GENBA_POPUP.PopupSearchSendParams.Add(searchDto3);

            if (!string.IsNullOrEmpty(this.form.DENPYOU_KBN.Text))
            {
                PopupSearchSendParamDto searchDto4 = new PopupSearchSendParamDto();
                PopupSearchSendParamDto searchDto5 = new PopupSearchSendParamDto();
                PopupSearchSendParamDto searchDto6 = new PopupSearchSendParamDto();

                if (this.form.DENPYOU_KBN.Text != "6")
                {
                    searchDto4.And_Or = CONDITION_OPERATOR.AND;
                    searchDto4.KeyName = "M_GYOUSHA.GYOUSHAKBN_MANI";
                    searchDto4.Value = "TRUE";

                    this.form.GENBA_CD.PopupSearchSendParams.Add(searchDto4);
                    this.form.GENBA_POPUP.PopupSearchSendParams.Add(searchDto4);

                    switch (this.form.DENPYOU_KBN.Text)
                    {
                        // マニフェスト
                        case "5":
                            searchDto5.And_Or = CONDITION_OPERATOR.AND;
                            searchDto5.KeyName = "M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN";
                            searchDto5.Value = "TRUE";

                            searchDto6.And_Or = CONDITION_OPERATOR.AND;
                            searchDto6.KeyName = "HAISHUTSU_NIZUMI_GENBA_KBN";
                            searchDto6.Value = "TRUE";

                            this.form.GENBA_CD.PopupSearchSendParams.Add(searchDto5);
                            this.form.GENBA_POPUP.PopupSearchSendParams.Add(searchDto5);
                            this.form.GENBA_CD.PopupSearchSendParams.Add(searchDto6);
                            this.form.GENBA_POPUP.PopupSearchSendParams.Add(searchDto6);
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        #endregion
    }
}