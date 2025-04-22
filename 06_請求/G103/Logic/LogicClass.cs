
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
using Shougun.Core.Billing.Seikyuichiran.DAO;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Message;
using r_framework.Authority;
using Seasar.Framework.Exceptions;
using Shougun.Core.Billing.Seikyuichiran.CustomControls_Ex;
using System.Collections.Generic;
using r_framework.Dto;
using Shougun.Core.ExternalConnection.CommunicateLib.Utility;
using Shougun.Core.Billing.Seikyuichiran.DTO;
using Shougun.Core.ExternalConnection.CommunicateLib;
using Shougun.Core.ExternalConnection.CommunicateLib.Dtos;
using Shougun.Core.Common.BusinessCommon;
using r_framework.CustomControl;
using System.Drawing;
using System.Collections.ObjectModel;
using r_framework.CustomControl;
using Shougun.Core.Billing.SeikyuShimeShori;


namespace Shougun.Core.Billing.Seikyuichiran
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Billing.Seikyuichiran.Setting.ButtonSetting.xml";

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
        private SeikyuuIchiranForm form;

        /// <summary>
        /// HeaderForm headForm
        /// </summary>
        private UIHeader headForm;

        /// <summary>	
        /// 拠点マスタ	
        /// </summary>	
        private IM_KYOTENDao mkyotenDao;

        /// <summary>
        /// 取引先マスタ
        /// </summary>
        private IM_TORIHIKISAKIDao mtorihikisakiDao;

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private TSDDaoCls daoIchiran;

        //PhuocLoc 2021/05/14 #148574 -Start
        /// <summary>
        /// 入金消込
        /// </summary>
        private TNKDao nyuukinKeshikomiDao;
        //PhuocLoc 2021/05/14 #148574 -End

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

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        internal BusinessBaseForm parentForm;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
        #endregion

        #region プロパティ

        public DataGridViewSelectionMode SelectionMode { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public string SearchString { get; set; }

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
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }

        private Control[] allControl;

        /// <summary>
        /// 締め処理画面連携 - 画面初期表示用DTO
        /// </summary>
        public DTOClass InitDto { get; set; }

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
        private const string CELL_CHECKBOX = "CHECKBOX";
        private const string CELL_UPLOAD_STATUS = "UPLOAD_STATUS";
        private const string CELL_DOWNLOAD_STATUS = "DOWNLOAD_STATUS";
        private const string CELL_INXS_SEIKYUU_KBN = "INXS_SEIKYUU_KBN";
        private const string CELL_SEIKYUU_NUMBER = "必須請求番号";

        internal readonly bool isInxsSeikyuusho;

        internal string[] selectedSeikyuuNumber = null;
        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

        //PhuocLoc 2021/05/14 #148574 -Start
        public const string CELL_CHECKBOX_DEL = "CHECKBOX_DEL";
        //PhuocLoc 2021/05/14 #148574 -End
        //160015 S
        internal string GamenFlg = "1";
        internal const string CELL_NYUUKIN_YOTEI_BI_HENKO = "入金予定日(変更後)";
        internal const string CELL_TIMESTAMP = "TIMESTAMP";
        internal const string CELL_SEIKYUU_DATE = "SEIKYUU_DATE";
        //160015 E
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(SeikyuuIchiranForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new DTOClass();
            this.daoIchiran = DaoInitUtility.GetComponent<TSDDaoCls>();
            this.mkyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.mtorihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
            this.isInxsSeikyuusho = r_framework.Configuration.AppConfig.AppOptions.IsInxsSeikyuusho();
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

            this.nyuukinKeshikomiDao = DaoInitUtility.GetComponent<TNKDao>(); //PhuocLoc 2021/05/14 #148574

            LogUtility.DebugMethodEnd();
        }

        #region 画面初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end

                //add button INXS請求書発行 start refs #158002
                if (this.isInxsSeikyuusho)
                {
                    this.AddSubFunction();
                }
                //add button INXS請求書発行 end

                //　ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();
                this.SetHeaderInit();
                this.SetMainFormInit();
                this.dtGridView();
                this.allControl = this.form.allControl;
                // 汎用/簡易検索の初期化
                this.searchModeInit();
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

        #region ボタンの初期化

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        public void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            if (!this.isInxsSeikyuusho)
            {
				//remove button process6 refs #158002
                buttonSetting = buttonSetting.Where(w => w.ButtonName != "bt_process6").ToArray();
            }
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
            //160015 S
            //if (!this.isInxsSeikyuusho)
            //{
            //    parentForm.bt_process2.Enabled = false;
            //    parentForm.bt_process2.Text = string.Empty;
            //    parentForm.bt_process5.Enabled = false;
            //    parentForm.bt_process5.Text = string.Empty;
            //}
            this.ChangeMod();
            //160015 E
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end
        }

        #region ボタン情報の設定
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

        #endregion

        #region イベント処理の初期化
        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        public void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;
            //customTextBoxでのエンターキー押下イベント生成
            //this.form.searchString.KeyDown += new KeyEventHandler(SearchStringKeyDown);       //汎用検索(SearchString)
            //parentForm.txb_process.KeyDown += new KeyEventHandler(txb_process_KeyDown);      //処理No(ESC)

            //Functionボタンのイベント生成
            //TODO: 汎用/簡易検索ボタン
            //160015 S
            parentForm.bt_func1.Click += new System.EventHandler(this.bt_func1_Click);       //一括入力
            parentForm.bt_func2.Click += new System.EventHandler(this.bt_func2_Click);       //切替
            //160015 E
            parentForm.bt_func3.Click += new System.EventHandler(this.bt_func3_Click);       //参照処理
            parentForm.bt_func4.Click += new System.EventHandler(this.bt_func4_Click);       //削除処理
            parentForm.bt_func6.Click += new System.EventHandler(this.bt_func6_Click);       //CSV
            parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);       //検索条件クリア
            parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);       //検索
            parentForm.bt_func9.Click += new System.EventHandler(this.bt_func9_Click);       //登録 160015
            parentForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);     //検索
            parentForm.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);     //F11 フィルタ
            parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);     //閉じる

            parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);             //パターン一覧画面へ遷移
            parentForm.bt_process2.Click += new EventHandler(bt_process2_Click);             //請求締処理
            parentForm.bt_process3.Click += new EventHandler(bt_process3_Click);             //請求書発行
            parentForm.bt_process4.Click += new EventHandler(bt_process4_Click);             //現金入金
            parentForm.bt_process5.Click += new EventHandler(bt_process5_Click);             //振込入金 160015
            //parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);             //パターン一覧画面へ遷移
            //parentForm.bt_process2.Click += new EventHandler(bt_process2_Click);             //検索条件設定画面へ遷移

            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
            if (this.isInxsSeikyuusho)
            {
                parentForm.bt_process2.Click += new EventHandler(Bt_process2_Click);
                parentForm.bt_process5.Click += new EventHandler(Bt_process5_Click);
				//add button INXS請求書発行 start refs #158002
                var bt_process6 = parentForm.ProcessButtonPanel.Controls.Find("bt_process6", false)[0] as CustomButton;
                bt_process6.Click += new EventHandler(Bt_process6_Click);
				//add button INXS請求書発行 end
                parentForm.OnReceiveMessageEvent += ParentForm_OnReceiveMessageEvent;
            }

            parentForm.FormClosing += new FormClosingEventHandler(SetPrevStatus);   // No.2002
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

            parentForm.FormClosing += new FormClosingEventHandler(SetPrevStatus);   // No.2002

            //画面上でESCキー押下時のイベント生成 TODO　二次開発 
            //this.form.PreviewKeyDown += new PreviewKeyDownEventHandler(form_PreviewKeyDown); //form上でのESCキー押下でFocus移動
            //ダブルクリック時の動作は「F3 修正」と同様の処理を行う
            this.form.customDataGridView1.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(customDataGridView1_MouseDoubleClick);

            /// 20141201 Houkakou 「請求一覧」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.form.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDUKE_TO_MouseDoubleClick);
            /// 20141201 Houkakou 「請求一覧」のダブルクリックを追加する　end

            /// 20141203 Houkakou 「請求一覧」の日付チェックを追加する　start
            this.form.HIDUKE_FROM.Leave += new System.EventHandler(HIDUKE_FROM_Leave);
            this.form.HIDUKE_TO.Leave += new System.EventHandler(HIDUKE_TO_Leave);
            /// 20141203 Houkakou 「請求一覧」の日付チェックを追加する　end

            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
            this.form.customDataGridView1.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(customDataGridView1_ColumnHeaderMouseClick);
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

            //PhuocLoc 2021/05/14 #148574 -Start
            this.form.customDataGridView1.CellClick += new DataGridViewCellEventHandler(customDataGridView1_CellClick);
            this.form.customDataGridView1.SelectionChanged += new EventHandler(customDataGridView1_SelectionChanged);
            //PhuocLoc 2021/05/14 #148574 -End
            this.form.NYUUKIN_YOTEI_DATE_TO.MouseDoubleClick += new MouseEventHandler(NYUUKIN_YOTEI_DATE_TO_MouseDoubleClick);//160015
            this.form.customDataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(customDataGridView1_CellFormatting);//160015
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ヘッダ初期値設定

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
            //    this.headForm.KYOTEN_CD.Text = string.Empty;
            //    var kyoten_cd = 0;
            //    //数字チェック + 空チェック
            //    var kyoten_res = int.TryParse(kyotenCd, out kyoten_cd);
            //    if (kyoten_res)
            //    {
            //        M_KYOTEN mKyoten = new M_KYOTEN();
            //        mKyoten.KYOTEN_CD = (SqlInt16)kyoten_cd;
            //        //削除フラグがたっていない適用期間内の情報を取得する
            //        var mKyotenList = mkyotenDao.GetAllValidData(mKyoten);
            //        if (mKyotenList.Count() > 0)
            //        {
            //            this.headForm.KYOTEN_CD.Text = String.Format("{0:D2}", kyoten_cd);
            //        }
            //    }
            //    //前回保存値がブランクの場合
            //}
            //else if (Properties.Settings.Default.SET_KYOTEN_CD == null)
            //{
            //    this.headForm.KYOTEN_CD.Text = "";
            //}
            ////前回保存値がない場合
            //else
            //{
            //    XMLAccessor fileAccess = new XMLAccessor();
            //    CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();
            //    this.headForm.KYOTEN_CD.Text = String.Format("{0:D2}", int.Parse(configProfile.ItemSetVal1));
            //}

            if (this.InitDto != null)
            {
                // 締め処理画面からの引継ぎ時
                this.headForm.KYOTEN_CD.Text = this.InitDto.InitKyotenCd;
            }
            else
            {
                /* 拠点CD初期値は「99」固定 */
                this.headForm.KYOTEN_CD.Text = KYOTEN_CD_INIT;
            }




            // ユーザ拠点名称の取得
            if (this.headForm.KYOTEN_CD.Text != null)
            {
                M_KYOTEN mKyoten = new M_KYOTEN();
                mKyoten = (M_KYOTEN)mkyotenDao.GetDataByCd(this.headForm.KYOTEN_CD.Text);
                if (mKyoten == null || this.headForm.KYOTEN_CD.Text == "")
                {
                    this.headForm.KYOTEN_CD.Text = "";
                    this.headForm.KYOTEN_NAME_RYAKU.Text = "";
                }
                else
                {
                    this.headForm.KYOTEN_NAME_RYAKU.Text = mKyoten.KYOTEN_NAME_RYAKU;
                }
            }

            // 読込データ件数初期値0
            this.headForm.ReadDataNumber.Text = "0";
            //160015 S
            this.form.NYUUKIN_YOTEI_DATE_FROM.Text = string.Empty;
            this.form.NYUUKIN_YOTEI_DATE_TO.Text = string.Empty;
            this.form.ZEI_KOMI_KBN.Text = "3";
            this.form.NYUUKIN_YOTEI_BI_HENKOU.Text = string.Empty;
            //160015 E
        }

        #endregion

        #region メインフォーム初期値設定

        /// <summary>
        /// メインフォーム初期値設定
        /// </summary>
        private void SetMainFormInit()
        {
            //伝票日付
            var parentForm = (BusinessBaseForm)this.form.Parent;

            this.SetDenpyouHidukeInit();

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

        #endregion

        #region DataGridViewのスタイル設定

        /// <summary>
        /// 画面でDataGridViewのスタイル設定
        /// </summary>
        private void dtGridView()
        {
            //行の追加オプション(false)
            this.form.customDataGridView1.AllowUserToAddRows = false;
            ////ヘッダの背景色
            //this.form.customDataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro;
            ////ヘッダの文字色
            //this.form.customDataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        }

        #endregion

        #region 汎用/簡易検索の初期化
        /// <summary>
        /// 汎用/簡易検索の初期化処理
        /// ※現在、簡易検索のみのため簡易検索で初期化
        /// </summary>
        private void searchModeInit()
        {
            this.form.SIMPLE_SEARCH_PANEL.Visible = true;
            this.form.searchString.Visible = false;
            this.searchMode = SearchMode.SIMPLE;
        }
        #endregion

        #endregion

        #region HeaderForm

        /// <summary>
        /// HeaderForm.cs設定
        /// </summary>
        /// /// <returns>hs</returns>
        public void SetHeader(UIHeader hs)
        {
            this.headForm = hs;
        }

        #endregion

        #region 請求一覧

        /// <summary>
        /// 請求一覧
        /// </summary>
        public void MakeSearchCondition1()
        {

            //SQL文格納StringBuilder
            var sql = new StringBuilder();
            sql.Append(" SELECT DISTINCT ");
            sql.Append(this.selectQuery);
            //必須取得項目
            sql.Append(",T_SEIKYUU_DENPYOU.SEIKYUU_NUMBER AS \"必須請求番号\",T_SEIKYUU_DENPYOU.TORIHIKISAKI_CD AS \"必須取引先CD\"");

            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
            if (this.isInxsSeikyuusho)
            {
                sql.Append(",T_SEIKYUU_DENPYOU_INXS.UPLOAD_STATUS,T_SEIKYUU_DENPYOU_INXS.DOWNLOAD_STATUS,TORIHIKISAKI_SEIKYUU.INXS_SEIKYUU_KBN ");
            }
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end
            //160015 S
            sql.Append(",(ISNULL(KONKAI_URIAGE_GAKU,0) + ISNULL(KONKAI_SEI_UTIZEI_GAKU,0) + ISNULL(KONKAI_SEI_SOTOZEI_GAKU,0) + ISNULL(KONKAI_DEN_UTIZEI_GAKU,0) + ISNULL(KONKAI_DEN_SOTOZEI_GAKU,0) + ISNULL(KONKAI_MEI_UTIZEI_GAKU,0) + ISNULL(KONKAI_MEI_SOTOZEI_GAKU,0)) - ISNULL(NYUUKIN.SUM_KESHIKOMI_GAKU,0) AS NYUUKIN_KOMI_GAKU");
            sql.Append(",T_SEIKYUU_DENPYOU.TIME_STAMP AS TIMESTAMP");
            sql.Append(",T_SEIKYUU_DENPYOU.SEIKYUU_DATE AS SEIKYUU_DATE");
            //160015 E
            #region FROM句

            //FROM句作成
            sql.Append(" FROM ");
            sql.Append(" T_SEIKYUU_DENPYOU ");
            //160015 S
            sql.Append(" LEFT JOIN ( ");
            sql.Append(" SELECT KESHIKOMI.SEIKYUU_NUMBER, SUM(KESHIKOMI.KESHIKOMI_GAKU) AS SUM_KESHIKOMI_GAKU ");
            sql.Append(" FROM T_NYUUKIN_ENTRY E INNER JOIN T_NYUUKIN_KESHIKOMI KESHIKOMI  ");
            sql.Append(" ON E.SYSTEM_ID = KESHIKOMI.SYSTEM_ID  ");
            sql.Append(" WHERE E.DELETE_FLG = 0 AND KESHIKOMI.DELETE_FLG = 0  ");
            sql.Append(" GROUP BY KESHIKOMI.SEIKYUU_NUMBER ) NYUUKIN");
            sql.Append(" ON T_SEIKYUU_DENPYOU.SEIKYUU_NUMBER = NYUUKIN.SEIKYUU_NUMBER ");
            //160015 E

            sql.Append(this.joinQuery);

            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
            if (this.isInxsSeikyuusho)
            {
                sql.Append(" LEFT JOIN T_SEIKYUU_DENPYOU_INXS ON T_SEIKYUU_DENPYOU_INXS.SEIKYUU_NUMBER = T_SEIKYUU_DENPYOU.SEIKYUU_NUMBER ");
                sql.Append(" LEFT JOIN (SELECT TORIHIKISAKI_CD, INXS_SEIKYUU_KBN FROM M_TORIHIKISAKI_SEIKYUU) AS TORIHIKISAKI_SEIKYUU ON T_SEIKYUU_DENPYOU.TORIHIKISAKI_CD = TORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD ");
            }
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

            #endregion

            #region WHERE句

            sql.Append(" WHERE ");
            sql.Append(" T_SEIKYUU_DENPYOU.DELETE_FLG = 0 ");

            //画面で日付選択が入力日付の場合
            if (this.form.HIDUKE_FROM.Value != null)
            {
                sql.Append(" AND T_SEIKYUU_DENPYOU.SEIKYUU_DATE >= '" + DateTime.Parse(this.form.HIDUKE_FROM.Value.ToString()).ToShortDateString() + "' ");
            }
            if (this.form.HIDUKE_TO.Value != null)
            {
                sql.Append(" AND T_SEIKYUU_DENPYOU.SEIKYUU_DATE <= '" + DateTime.Parse(this.form.HIDUKE_TO.Value.ToString()).ToShortDateString() + "' ");
            }
            //160015 S
            if (this.form.NYUUKIN_YOTEI_DATE_FROM.Value != null)
            {
                sql.Append(" AND T_SEIKYUU_DENPYOU.NYUUKIN_YOTEI_BI >= '" + DateTime.Parse(this.form.NYUUKIN_YOTEI_DATE_FROM.Value.ToString()).ToShortDateString() + "' ");
            }
            if (this.form.NYUUKIN_YOTEI_DATE_TO.Value != null)
            {
                sql.Append(" AND T_SEIKYUU_DENPYOU.NYUUKIN_YOTEI_BI <= '" + DateTime.Parse(this.form.NYUUKIN_YOTEI_DATE_TO.Value.ToString()).ToShortDateString() + "' ");
            }
            if (this.form.ZEI_KOMI_KBN.Text != "3")
            {
                if (this.form.ZEI_KOMI_KBN.Text == "1")
                {
                    sql.Append(" AND (ISNULL(KONKAI_URIAGE_GAKU,0) + ISNULL(KONKAI_SEI_UTIZEI_GAKU,0) + ISNULL(KONKAI_SEI_SOTOZEI_GAKU,0) + ISNULL(KONKAI_DEN_UTIZEI_GAKU,0) + ISNULL(KONKAI_DEN_SOTOZEI_GAKU,0) + ISNULL(KONKAI_MEI_UTIZEI_GAKU,0) + ISNULL(KONKAI_MEI_SOTOZEI_GAKU,0)) - ISNULL(NYUUKIN.SUM_KESHIKOMI_GAKU,0) != 0 ");
                }
                if (this.form.ZEI_KOMI_KBN.Text == "2")
                {
                    sql.Append(" AND (ISNULL(KONKAI_URIAGE_GAKU,0) + ISNULL(KONKAI_SEI_UTIZEI_GAKU,0) + ISNULL(KONKAI_SEI_SOTOZEI_GAKU,0) + ISNULL(KONKAI_DEN_UTIZEI_GAKU,0) + ISNULL(KONKAI_DEN_SOTOZEI_GAKU,0) + ISNULL(KONKAI_MEI_UTIZEI_GAKU,0) + ISNULL(KONKAI_MEI_SOTOZEI_GAKU,0)) - ISNULL(NYUUKIN.SUM_KESHIKOMI_GAKU,0) = 0 ");
                }
            }
            //160015 E

            //画面で拠点CDがnull無いの場合
            if (!string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text))
            {
                sql.Append(" AND T_SEIKYUU_DENPYOU.KYOTEN_CD = " + int.Parse(this.headForm.KYOTEN_CD.Text) + " ");
            }

            //画面で取引先CDがnullで無い場合検索条件に追加
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                sql.Append(" AND T_SEIKYUU_DENPYOU.TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text + "' ");
            }

            #region ORDERBY句

            if (!string.IsNullOrEmpty(orderByQuery))
            {
                sql.Append(" ORDER BY ");
                sql.Append(this.orderByQuery);
            }

            #endregion

            #endregion

            this.createSql = sql.ToString();
            sql.Append("");
        }

        #endregion

        #region Functionボタン 押下処理
        //160015 S
        /// <summary>
        /// 一括入力
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
            this.form.NYUUKIN_YOTEI_BI_HENKOU.BackColor = Constans.NOMAL_COLOR;
            if (this.form.NYUUKIN_YOTEI_BI_HENKOU.Text == string.Empty)
            {
                this.form.NYUUKIN_YOTEI_BI_HENKOU.BackColor = Constans.ERROR_COLOR;
                MessageBoxUtility.MessageBoxShowError("変更後入金予定日は必須項目です。入力してください。");
                this.form.NYUUKIN_YOTEI_BI_HENKOU.Focus();
                return;
            }
            foreach (DataGridViewRow row in arrRow)
            {
                row.Cells[CELL_NYUUKIN_YOTEI_BI_HENKO].Value = this.form.NYUUKIN_YOTEI_BI_HENKOU.Text;
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
            this.headForm.ReadDataNumber.Text = "0";
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
            this.LockButtonNyuukin();
        }
        //160015 E
        /// <summary>
        /// F3 参照処理
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void bt_func3_Click(object sender, EventArgs e)
        {
            // 参照
            Edit(WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
        }

        /// <summary>
        /// F4 削除処理
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void bt_func4_Click(object sender, EventArgs e)
        {
            //PhuocLoc 2021/05/14 #148574 -Start
            // 権限チェック(更新権限有無で判定)
            //if (r_framework.Authority.Manager.CheckAuthority("G103", WINDOW_TYPE.UPDATE_WINDOW_FLAG))
            //{
            //    // 削除
            //    Edit(WINDOW_TYPE.DELETE_WINDOW_FLAG);
            //}
            List<SeikyuuDeleteDto> lstSeiikyuuDeleteDto = this.GetRowsCheckedDelete();
            if (lstSeiikyuuDeleteDto != null && lstSeiikyuuDeleteDto.Count > 0)
            {
                //PhuocLoc 2021/06/28 #152179 -Start
                if (r_framework.Authority.Manager.CheckAuthority("G103", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DeleteMultiSeikyuu(lstSeiikyuuDeleteDto);
                }
                else
                {
                    MessageBoxUtility.MessageBoxShow("E158", "削除");
                }
                //PhuocLoc 2021/06/28 #152179 -End
            }
            else
            {
                // 権限チェック(更新権限有無で判定)
                if (r_framework.Authority.Manager.CheckAuthority("G103", WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    // 削除
                    Edit(WINDOW_TYPE.DELETE_WINDOW_FLAG);
                }
            }
            //PhuocLoc 2021/05/14 #148574 -End
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
            return;
        }

        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
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
            this.headForm.ReadDataNumber.Text = "0";
            //ソートヘッダクリア
            this.form.customSortHeader1.ClearCustomSortSetting();
            // フィルタをクリア
            this.form.customSearchHeader1.ClearCustomSearchSetting();
            //伝票日付のクリア
            this.SetDenpyouHidukeInit();
            //this.form.HIDUKE_FROM.Text = DateTime.Now.ToString(); // No.2292
            //this.form.HIDUKE_TO.Text = DateTime.Now.ToString();   // No.2292
            //拠点のクリア
            //this.headForm.KYOTEN_CD.Text = "";                    // No.2292
            //this.headForm.KYOTEN_NAME_RYAKU.Text = "";            // No.2292
            //取引先のクリア
            this.form.TORIHIKISAKI_CD.Text = "";
            this.form.TORIHIKISAKI_NAME_RYAKU.Text = "";
            //160015 S
            this.form.HIDUKE_FROM.IsInputErrorOccured = false;
            this.form.HIDUKE_TO.IsInputErrorOccured = false;
            this.form.NYUUKIN_YOTEI_DATE_FROM.IsInputErrorOccured = false;
            this.form.NYUUKIN_YOTEI_DATE_TO.IsInputErrorOccured = false;
            this.form.NYUUKIN_YOTEI_DATE_FROM.Text = string.Empty;
            this.form.NYUUKIN_YOTEI_DATE_TO.Text = string.Empty;
            if (this.GamenFlg.Equals("1"))
            {
                this.form.ZEI_KOMI_KBN.Text = "3";
            }
            this.form.NYUUKIN_YOTEI_BI_HENKOU.Text = string.Empty;
            //160015 E
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void bt_func8_Click(object sender, EventArgs e)
        {
            // パターン未登録の場合検索処理を行わない
            if (this.form.PatternNo == 0)
            {
                MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }
            //160015 S
            this.SetRequiredSetting();
            var autoCheckLogic = new AutoRegistCheckLogic(this.GetAllControl(), this.GetAllControl());
            this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
            if (this.form.RegistErrorFlag)
            {
                return;
            }
            //160015 E
            bool searchErrorFlag = false;
            this.form.HIDUKE_FROM.IsInputErrorOccured = false;
            this.form.HIDUKE_TO.IsInputErrorOccured = false;

            if (!string.IsNullOrEmpty(this.form.HIDUKE_FROM.GetResultText())
                && !string.IsNullOrEmpty(this.form.HIDUKE_TO.GetResultText()))
            {
                DateTime dtpFrom = DateTime.Parse(this.form.HIDUKE_FROM.GetResultText());
                DateTime dtpTo = DateTime.Parse(this.form.HIDUKE_TO.GetResultText());
                DateTime dtpFromWithoutTime = DateTime.Parse(dtpFrom.ToShortDateString());
                DateTime dtpToWithoutTime = DateTime.Parse(dtpTo.ToShortDateString());

                int diff = dtpFromWithoutTime.CompareTo(dtpToWithoutTime);

                if (0 < diff)
                {
                    //対象期間内でないならエラーメッセージ表示
                    this.form.HIDUKE_FROM.IsInputErrorOccured = true;
                    this.form.HIDUKE_TO.IsInputErrorOccured = true;
                    MessageBoxUtility.MessageBoxShow("E030", "請求日付From", "請求日付To");
                    this.form.HIDUKE_FROM.Select();
                    this.form.HIDUKE_FROM.Focus();
                    searchErrorFlag = true;
                }
            }
            //160015 S
            //else
            //{
            //    this.form.HIDUKE_FROM.IsInputErrorOccured = string.IsNullOrEmpty(this.form.HIDUKE_FROM.GetResultText());
            //    this.form.HIDUKE_TO.IsInputErrorOccured = string.IsNullOrEmpty(this.form.HIDUKE_TO.GetResultText());
            //    MessageBoxUtility.MessageBoxShow("E001", "請求日付");
            //    if (string.IsNullOrEmpty(this.form.HIDUKE_FROM.GetResultText()))
            //    {
            //        this.form.HIDUKE_FROM.Focus();                    
            //    }
            //    else
            //    {
            //        this.form.HIDUKE_TO.Focus();                    
            //    }
            //    searchErrorFlag = true;
            //}
            if (!searchErrorFlag)
            {
                this.form.NYUUKIN_YOTEI_DATE_FROM.IsInputErrorOccured = false;
                this.form.NYUUKIN_YOTEI_DATE_TO.IsInputErrorOccured = false;

                if (!string.IsNullOrEmpty(this.form.NYUUKIN_YOTEI_DATE_FROM.GetResultText())
                    && !string.IsNullOrEmpty(this.form.NYUUKIN_YOTEI_DATE_TO.GetResultText()))
                {
                    DateTime dtpFrom = DateTime.Parse(this.form.NYUUKIN_YOTEI_DATE_FROM.GetResultText());
                    DateTime dtpTo = DateTime.Parse(this.form.NYUUKIN_YOTEI_DATE_TO.GetResultText());
                    DateTime dtpFromWithoutTime = DateTime.Parse(dtpFrom.ToShortDateString());
                    DateTime dtpToWithoutTime = DateTime.Parse(dtpTo.ToShortDateString());

                    int diff = dtpFromWithoutTime.CompareTo(dtpToWithoutTime);

                    if (0 < diff)
                    {
                        //対象期間内でないならエラーメッセージ表示
                        this.form.NYUUKIN_YOTEI_DATE_FROM.IsInputErrorOccured = true;
                        this.form.NYUUKIN_YOTEI_DATE_TO.IsInputErrorOccured = true;
                        MessageBoxUtility.MessageBoxShow("E030", "入金予定日From", "入金予定日To");
                        this.form.NYUUKIN_YOTEI_DATE_FROM.Select();
                        this.form.NYUUKIN_YOTEI_DATE_FROM.Focus();
                        searchErrorFlag = true;
                    }
                }
            }
            //160015 E

            if (!searchErrorFlag)
            {

                this.form.setLogicSelect();

                if (!string.IsNullOrEmpty(this.form.searchString.Text))
                {
                    string getSearchString = this.form.searchString.Text.Replace("\r", "").Replace("\n", "");
                    this.SearchString = getSearchString;
                }

                this.Search();

                //PhuocLoc 2021/05/14 #148574 -Start
                if (this.form.customDataGridView1.Columns.Contains(CELL_CHECKBOX_DEL))
                {
                    DataGridViewCheckBoxHeaderCell header = this.form.customDataGridView1.Columns[CELL_CHECKBOX_DEL].HeaderCell as DataGridViewCheckBoxHeaderCell;
                    if (header != null)
                    {
                        header._checked = false;
                    }
                }
                //PhuocLoc 2021/05/14 #148574 -End

                if (string.IsNullOrEmpty(this.form.searchString.Text))
                {
                    this.form.searchString.Clear();
                    this.form.searchString.Focus();
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
        //登録 160015
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
                SeikyuuDenpyouDao seikyuuDao = DaoInitUtility.GetComponent<SeikyuuDenpyouDao>();
                using (Transaction tran = new Transaction())
                {
                    foreach (DataGridViewRow r in arrRow)
                    {
                        T_SEIKYUU_DENPYOU data = seikyuuDao.GetDataByKey(r.Cells[CELL_SEIKYUU_NUMBER].Value.ConvertToInt64());
                        if (data != null)
                        {
                            data.NYUUKIN_YOTEI_BI = r.Cells[CELL_NYUUKIN_YOTEI_BI_HENKO].Value.ConvertToDateTime();
                            data.TIME_STAMP = (byte[])r.Cells[CELL_TIMESTAMP].Value;
                            var databinder = new DataBinderLogic<T_SEIKYUU_DENPYOU>(data);
                            databinder.SetSystemProperty(data, false);
                            seikyuuDao.Update(data);
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
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void bt_func10_Click(object sender, EventArgs e)
        {
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
                this.headForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.headForm.ReadDataNumber.Text = "0";
            }
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void bt_func12_Click(object sender, EventArgs e)
        {
            // 以下の項目をセッティングファイルに保存する
            Properties.Settings.Default.SET_KYOTEN_CD = this.headForm.KYOTEN_CD.Text;                                                //拠点CD

            DateTime resultDt;
            //if (this.form.HIDUKE_FROM.Value == null)
            if (!string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text) && DateTime.TryParse(this.form.HIDUKE_FROM.Text, out resultDt))
            {
                Properties.Settings.Default.SET_HIDUKE_FROM = DateTime.Parse(this.form.HIDUKE_FROM.Value.ToString()).ToShortDateString();          //伝票日付From
            }
            else
            {
                Properties.Settings.Default.SET_HIDUKE_FROM = string.Empty;
                // CustomDateTimePicker Valueのget return DateTime.ParseExactでエラーになる為、Emptyにしておく
                this.form.HIDUKE_FROM.Text = string.Empty;
            }
            
            //if (this.form.HIDUKE_TO.Value == null)
            if (!string.IsNullOrEmpty(this.form.HIDUKE_TO.Text) && DateTime.TryParse(this.form.HIDUKE_TO.Text, out resultDt))
            {
                Properties.Settings.Default.SET_HIDUKE_TO = DateTime.Parse(this.form.HIDUKE_TO.Value.ToString()).ToShortDateString();              //伝票日付To
            }
            else
            {
                Properties.Settings.Default.SET_HIDUKE_TO = string.Empty;
                // CustomDateTimePicker Valueのget return DateTime.ParseExactでエラーになる為、Emptyにしておく
                this.form.HIDUKE_TO.Text = string.Empty;
            }

            // 取引先CD
            Properties.Settings.Default.SET_TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;

            Properties.Settings.Default.Save();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.Close();
        }

        #region 明細行ダブルクリック処理

        /// <summary>
        /// 明細行ダブルクリック処理
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void customDataGridView1_MouseDoubleClick(object sender, EventArgs e)
        {
            string strSeikyuuNumber = "";

            DataGridViewCellMouseEventArgs cell = (DataGridViewCellMouseEventArgs)e;
            if (cell.RowIndex >= 0)
            {
                strSeikyuuNumber = this.form.customDataGridView1.Rows[cell.RowIndex].Cells["必須請求番号"].Value.ToString();
                // 参照
                Edit(WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
            }
        }

        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
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
        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

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
            // No.1079
            //            if (type == WINDOW_TYPE.DELETE_WINDOW_FLAG && !CheckDelData())
            //            {
            //                this.msgcls.MessageBoxShow("I012", "請求伝票");
            //                return;
            //            }
            if (type == WINDOW_TYPE.DELETE_WINDOW_FLAG)
            {
                int iret = CheckDelData();
                if (iret == 1)
                {
                    // 削除済みデータ
                    MessageBoxUtility.MessageBoxShow("E045");
                    return;
                }
                else if (iret == 2)
                {
                    // 削除不可データ
                    MessageBoxUtility.MessageBoxShow("I012", "請求伝票");
                    return;
                }
            }

            string strSeikyuuNumber = "";
            strSeikyuuNumber = this.form.customDataGridView1.SelectedRows[0].Cells["必須請求番号"].Value.ToString();

            // Setting保存
            this.setSetting();

            //請求確認画面表示
            FormManager.OpenForm("G102", strSeikyuuNumber, type);
        }

        #endregion

        #region Settingsの値の保存

        /// <summary>
        /// Settingsの値の保存
        /// </summary>
        public void setSetting()
        {
            Properties.Settings.Default.SET_KYOTEN_CD = this.headForm.KYOTEN_CD.Text;                  //拠点CD
            Properties.Settings.Default.SET_HIDUKE_FROM = this.form.HIDUKE_FROM.Text;              //伝票日付From
            Properties.Settings.Default.SET_HIDUKE_TO = this.form.HIDUKE_TO.Text;                  //伝票日付To
            // 取引先CD
            Properties.Settings.Default.SET_TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
        }

        #endregion

        #endregion

        #region プロセスボタン押下処理

        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        public void bt_process1_Click(object sender, System.EventArgs e)
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
        /// 請求締処理画面へ遷移
        /// </summary>
        public void bt_process2_Click(object sender, System.EventArgs e)
        {
            FormManager.OpenFormWithAuth("G101", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);

        }

        /// <summary>
        /// 請求書発行画面へ遷移
        /// </summary>
        public void bt_process3_Click(object sender, System.EventArgs e)
        {
            FormManager.OpenFormWithAuth("G107", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
        }

        /// <summary>
        /// 現金入金  160015
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_process4_Click(object sender, System.EventArgs e)
        {
            if (this.GamenFlg.Equals("2"))
            {
                return;
            }
            CallNyuuShukkinGamen(1);
        }
        /// <summary>
        /// 振込入金 160015
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_process5_Click(object sender, System.EventArgs e)
        {
            if (this.GamenFlg.Equals("2"))
            {
                return;
            }
            CallNyuuShukkinGamen(2);
        }
        #endregion

        #region 検索処理

        /// <summary>
        /// 検索処理を行う
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            //SELECT句未取得なら検索できない
            if (string.IsNullOrEmpty(this.selectQuery))
            {
                this.headForm.ReadDataNumber.Text = "0";
                return 0;
            }

            //請求一覧
            MakeSearchCondition1();

            //検索実行
            this.SearchResult = new DataTable();
            this.SearchResult = daoIchiran.getdateforstringsql(this.createSql);

            int count = SearchResult.Rows.Count;
            if (count == 0)
            {
                this.form.ShowData();
                //読込データ件数を取得
                if (this.form.customDataGridView1 != null)
                {
                    this.headForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                }
                else
                {
                    this.headForm.ReadDataNumber.Text = "0";
                }

                if (this.headForm.ReadDataNumber.Text.Equals("0") && !this.form.IsNoneAlert)
                {
                    MessageBoxUtility.MessageBoxShow("C001");
                }
                this.form.IsNoneAlert = false;
            }
            else
            {
                //160015 S
                this.SearchResult.Columns.Cast<DataColumn>().ToList().ForEach(c => c.ReadOnly = true);
                if (this.GamenFlg.Equals("2"))
                {
                    if (this.SearchResult.Columns.Contains(CELL_NYUUKIN_YOTEI_BI_HENKO))
                    {
                        this.SearchResult.Columns[CELL_NYUUKIN_YOTEI_BI_HENKO].ReadOnly = false;
                    }
                }
                //160015 E
                //読込データ件数を取得
                this.headForm.ReadDataNumber.Text = count.ToString();
                this.form.ShowData();
                //160015 S
                if (this.GamenFlg.Equals("2"))
                {
                    for (int index = 0; index < this.form.customDataGridView1.Columns.Count; index++)
                    {
                        DataGridViewColumn column = this.form.customDataGridView1.Columns[index];

                        if (CELL_NYUUKIN_YOTEI_BI_HENKO.Equals(column.Name))
                        {
                            this.form.customDataGridView1.Columns.RemoveAt(index);
                            DgvCustomDataTimeColumn newColumnDataTime = new DgvCustomDataTimeColumn();
                            newColumnDataTime.Name = column.Name;
                            newColumnDataTime.DataPropertyName = column.DataPropertyName;
                            this.form.customDataGridView1.Columns.Insert(index, newColumnDataTime);
                        }

                        //有効化
                        if (CELL_NYUUKIN_YOTEI_BI_HENKO.Equals(column.Name))
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
                //160015 E
                //読込データ件数を取得
                if (this.form.customDataGridView1 != null)
                {
                    this.headForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                }
                else
                {
                    this.headForm.ReadDataNumber.Text = "0";
                }

                if (this.headForm.ReadDataNumber.Text.Equals("0"))
                {
                    MessageBoxUtility.MessageBoxShow("C001");
                }
                return SearchResult.Rows.Count;
            }

            LogUtility.DebugMethodEnd();

            return 0;
        }

        /// <summary>
        /// 削除対象が最新データかチェックする
        /// </summary>
        /// <returns></returns>
        //        internal bool CheckDelData()
        internal int CheckDelData()
        {
            int MaxSeikyuuNumber = daoIchiran.CheckDelData(this.form.customDataGridView1.SelectedRows[0].Cells["必須取引先CD"].Value.ToString());

            // No.1079
            //            if (MaxSeikyuuNumber == 0
            //                || Int32.Parse(this.form.customDataGridView1.SelectedRows[0].Cells["必須請求番号"].Value.ToString()) < MaxSeikyuuNumber)
            //            {
            //                return false;
            //            }
            //            return true;
            if (MaxSeikyuuNumber == 0)
            {
                return 1;
            }
            else if (Int32.Parse(this.form.customDataGridView1.SelectedRows[0].Cells["必須請求番号"].Value.ToString()) < MaxSeikyuuNumber)
            {
                return 2;
            }

            return 0;
        }
        #endregion

        #region unused

        public void LogicalDelete()
        {
            //throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            //throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            //throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            //throw new NotImplementedException();
        }

        #endregion

        // No.2002
        /// <summary>
        /// Windowクローズ処理。
        /// </summary>
        internal void SetPrevStatus(object sender, EventArgs e)
        {
            // 以下の項目をセッティングファイルに保存する
            //拠点CD
            if (this.headForm.KYOTEN_CD.Text != "")
            {
                Properties.Settings.Default.SET_KYOTEN_CD = this.headForm.KYOTEN_CD.Text;
            }
            else
            {
                Properties.Settings.Default.SET_KYOTEN_CD = null;
            }

            DateTime resultDt;
            //if (this.form.HIDUKE_FROM.Value == null)
            if (!string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text) && DateTime.TryParse(this.form.HIDUKE_FROM.Text, out resultDt))
            {
                Properties.Settings.Default.SET_HIDUKE_FROM = DateTime.Parse(this.form.HIDUKE_FROM.Value.ToString()).ToShortDateString();          //伝票日付From
            }
            else
            {
                Properties.Settings.Default.SET_HIDUKE_FROM = string.Empty;
                // CustomDateTimePicker Valueのget return DateTime.ParseExactでエラーになる為、Emptyにしておく
                this.form.HIDUKE_FROM.Text = string.Empty;
            }

            //if (this.form.HIDUKE_TO.Value == null)
            if (!string.IsNullOrEmpty(this.form.HIDUKE_TO.Text) && DateTime.TryParse(this.form.HIDUKE_TO.Text, out resultDt))
            {
                Properties.Settings.Default.SET_HIDUKE_TO = DateTime.Parse(this.form.HIDUKE_TO.Value.ToString()).ToShortDateString();              //伝票日付To
            }
            else
            {
                Properties.Settings.Default.SET_HIDUKE_TO = string.Empty;
                // CustomDateTimePicker Valueのget return DateTime.ParseExactでエラーになる為、Emptyにしておく
                this.form.HIDUKE_TO.Text = string.Empty;
            }

            // 取引先CD
            Properties.Settings.Default.SET_TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;

            Properties.Settings.Default.Save();
        }

        /// 20141201 Houkakou 「請求一覧」のダブルクリックを追加する　start
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.HIDUKE_FROM;
            var ToTextBox = this.form.HIDUKE_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141201 Houkakou 「請求一覧」のダブルクリックを追加する　end

        /// 20141203 Houkakou 「請求一覧」の日付チェックを追加する　start
        #region HIDUKE_FROM_Leaveイベント
        /// <summary>
        /// TEKIYOU_BEGIN_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void HIDUKE_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.HIDUKE_TO.Text))
            {
                this.form.HIDUKE_TO.IsInputErrorOccured = false;
                this.form.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region HIDUKE_TO_Leaveイベント
        /// <summary>
        /// TEKIYOU_END_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void HIDUKE_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text))
            {
                this.form.HIDUKE_FROM.IsInputErrorOccured = false;
                this.form.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion
        /// 20141203 Houkakou 「請求一覧」の日付チェックを追加する　end
        
        //thongh 2015/09/14 #13030 start
        /// <summary>
        /// 伝票日付初期値設定
        /// </summary>
        private void SetDenpyouHidukeInit()
        {
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            DateTime now = this.parentForm.sysDate;
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            //伝票日付From
            if (this.InitDto != null && !string.IsNullOrEmpty(this.InitDto.InitDenpyouHiduke))
            {
                // 締め処理画面からの引継ぎ時
                this.form.HIDUKE_FROM.Text = this.InitDto.InitDenpyouHiduke;
            }
            else if ("".Equals(Properties.Settings.Default.SET_HIDUKE_FROM))
            {
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //this.form.HIDUKE_FROM.Text = DateTime.Now.ToString();
                this.form.HIDUKE_FROM.Text = now.ToString();
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            }
            else
            {
                this.form.HIDUKE_FROM.Text = Properties.Settings.Default.SET_HIDUKE_FROM;
                Properties.Settings.Default.SET_HIDUKE_FROM = string.Empty;
            }
            //伝票日付To
            if (this.InitDto != null && !string.IsNullOrEmpty(this.InitDto.InitDenpyouHiduke))
            {
                // 締め処理画面からの引継ぎ時
                this.form.HIDUKE_TO.Text = this.InitDto.InitDenpyouHiduke;
                this.InitDto.InitDenpyouHiduke = string.Empty;
            }
            else if ("".Equals(Properties.Settings.Default.SET_HIDUKE_TO))
            {
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //this.form.HIDUKE_TO.Text = DateTime.Now.ToString();
                this.form.HIDUKE_TO.Text = now.ToString();
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            }
            else
            {
                this.form.HIDUKE_TO.Text = Properties.Settings.Default.SET_HIDUKE_TO;
                Properties.Settings.Default.SET_HIDUKE_TO = string.Empty;
            }
        }
        //thongh 2015/09/14 #13030 end

        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
        #region INXS Seikyuu

        public void HeaderCheckBoxSupport()
        {
            if (r_framework.Configuration.AppConfig.AppOptions.IsInxsSeikyuusho())
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
            selectedNumber = GetSelectedSeikyuuNumber();
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

        private List<CommonKeyDto> GetSelectedSeikyuuNumber()
        {
            return this.form.customDataGridView1.Rows.Cast<DataGridViewRow>().Where(w => w.Cells[CELL_CHECKBOX].ReadOnly == false
                                                                                         && w.Cells[CELL_CHECKBOX].Value != null
                                                                                         && (bool)w.Cells[CELL_CHECKBOX].Value
                                                                                         && w.Cells[CELL_SEIKYUU_NUMBER].Value != null)
                                                                             .Select(s => new CommonKeyDto() { Id = Convert.ToInt64(s.Cells[CELL_SEIKYUU_NUMBER].Value.ToString()) })
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
                CommandName = 1,//Get download status
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
                this.errmessage.MessageBoxShow("E297");
                return;
            }

            var requestDto = new
            {
                CommandName = 2,//delete seikyuu data
                ShougunParentWindowName = this.parentForm.Text,
                CommandArgs = selectedNumber
            };
            this.selectedSeikyuuNumber = requestDto.CommandArgs.Select(s => s.Id.ToString()).ToArray();

            ExecuteSubAppCommand(requestDto);

            this.bt_func8_Click(null, null);
            foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
            {
                string seikyuuNumber = dgvRow.Cells["必須請求番号"].Value.ToString();
                if (this.selectedSeikyuuNumber != null)
                {
                    if (this.selectedSeikyuuNumber.Contains(seikyuuNumber))
                    {
                        this.form.customDataGridView1.Rows[dgvRow.Index].Cells[CELL_CHECKBOX].Value = true;
                    }
                    else
                    {
                        this.form.customDataGridView1.Rows[dgvRow.Index].Cells[CELL_CHECKBOX].Value = false;
                    }
                    this.selectedSeikyuuNumber = null;
                }
            }

            this.errmessage.MessageBoxShow("I027");
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
        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

        //PhuocLoc 2021/05/14 #148574 -Start
        internal void HeaderCheckBoxSupportMod()
        {
            LogUtility.DebugMethodStart();
            if (!this.form.customDataGridView1.Columns.Contains(CELL_CHECKBOX_DEL))
            {
                DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
                //160015 S
                if (this.GamenFlg.Equals("2"))
                {
                    newheader.Value = "";
                }
                else
                {
                    newheader.Value = "削除\n ";
                }
                //160015 E
                newheader.Tag = "すべての請求データを一括削除したい場合、チェックを付けてください";
                DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                newColumn.Name = CELL_CHECKBOX_DEL;
                //160015 S
                if (this.GamenFlg.Equals("2"))
                {
                    newColumn.HeaderText = "  \n ";
                }
                else
                {
                    newColumn.HeaderText = "削除\n ";
                }
                //160015 E
                newColumn.Width = 42;
                newColumn.FillWeight = 42;
                newColumn.MinimumWidth = 42;
                newColumn.HeaderCell = newheader;
                newColumn.ReadOnly = false;
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
                //160015 S
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
                //160015 E
            }
            LogUtility.DebugMethodEnd();
        }
        internal bool DeleteMultiSeikyuu(List<SeikyuuDeleteDto> lstSeiikyuuDeleteDto)
        {
            bool isError = false;
            bool isShowMess = false;
            if (lstSeiikyuuDeleteDto != null && lstSeiikyuuDeleteDto.Count > 0)
            {
                long[] arrSeikyuuNumber = lstSeiikyuuDeleteDto.Select(c => c.SeikyuuNumber).ToArray();
                if (this.CheckMultiDelData(lstSeiikyuuDeleteDto))
                {
                    T_NYUUKIN_KESHIKOMI[] KeshikomiList = this.CheckMultiKeshikomi(arrSeikyuuNumber);
                    string MessConfirm = string.Empty;
                    if (KeshikomiList != null && KeshikomiList.Count() > 0)
                    {
                        isShowMess = true;
                        MessConfirm = "入金消込が行われている請求書が含まれています。請求書の削除処理を行いますか。";
                        int RowCount = 1;
                        foreach (T_NYUUKIN_KESHIKOMI KeshikomiEntity in KeshikomiList)
                        {
                            if (RowCount > 5)
                            {
                                MessConfirm += "\n（計" + KeshikomiList.Count() + "件）";
                                break;
                            }
                            MessConfirm += "\n（請求番号=" + KeshikomiEntity.SEIKYUU_NUMBER.ToString() + "、入金番号＝" + KeshikomiEntity.NYUUKIN_NUMBER.ToString() + "）";
                            RowCount++;
                        }
                    }
                    if (isShowMess)
                    {
                        DialogResult result = this.errmessage.MessageBoxShowConfirm(MessConfirm, MessageBoxDefaultButton.Button2);
                        if (result != DialogResult.OK && result != DialogResult.Yes)
                        {
                            return true;
                        }
                    }
                    #region Delete
                    try
                    {
                        LogUtility.DebugMethodStart(lstSeiikyuuDeleteDto);
                        // トランザクション開始（エラーまたはコミットしなければ自動でロールバックされる）
                        using (Transaction tran = new Transaction())
                        {
                            //請求伝票更新
                            this.daoIchiran.UpdateMultiSeikyu(arrSeikyuuNumber);
                            //請求伝票_鑑更新
                            this.daoIchiran.UpdateMultiSeikyuKagami(arrSeikyuuNumber);
                            //請求明細
                            this.daoIchiran.UpdateMultiSeikyuDetail(arrSeikyuuNumber);
                            //入金消込更新
                            this.daoIchiran.UpdateMultiNyukin(arrSeikyuuNumber);
                            if (this.isInxsSeikyuusho && IsInxsAuthority())
                            {
                                List<CommonKeyDto> selectedNumber = new List<CommonKeyDto>();
                                selectedNumber = GetSelectedSeikyuuNumberMod();
                                foreach (CommonKeyDto number in selectedNumber)
                                {
                                    var requestDto = new
                                    {
                                        CommandName = 3,//delete seikyuu data
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
                        LogUtility.DebugMethodEnd(isError, lstSeiikyuuDeleteDto);
                    }
                    #endregion
                }
            }
            return isError;
        }
        internal List<SeikyuuDeleteDto> GetRowsCheckedDelete()
        {
            List<SeikyuuDeleteDto> lstSeiikyuuDeleteDto = new List<SeikyuuDeleteDto>();
            if (this.form.customDataGridView1.Columns.Contains(CELL_CHECKBOX_DEL))
            {
                for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                {
                    DataGridViewRow row = this.form.customDataGridView1.Rows[i];
                    if (row.Cells[CELL_CHECKBOX_DEL].Value != null && bool.Parse(row.Cells[CELL_CHECKBOX_DEL].Value.ToString()))
                    {
                        SeikyuuDeleteDto dto = new SeikyuuDeleteDto();
                        dto.SeikyuuNumber = long.Parse(this.form.customDataGridView1.Rows[i].Cells["必須請求番号"].Value.ToString());
                        dto.TorihikisakiCd = this.form.customDataGridView1.Rows[i].Cells["必須取引先CD"].Value.ToString();
                        lstSeiikyuuDeleteDto.Add(dto);
                    }
                }
            }

            List<SeikyuuDeleteDto> SortedList = lstSeiikyuuDeleteDto.OrderBy(o => o.TorihikisakiCd).ToList();

            return SortedList;
        }
        private bool CheckMultiDelData(List<SeikyuuDeleteDto> lstSeiikyuuDeleteDto)
        {
            try
            {
                LogUtility.DebugMethodStart(lstSeiikyuuDeleteDto);
                if (lstSeiikyuuDeleteDto != null && lstSeiikyuuDeleteDto.Count > 0)
                {
                    var lstTorihikisakiCd = lstSeiikyuuDeleteDto.Select(c => c.TorihikisakiCd).Distinct();
                    foreach (var torihikisakiCd in lstTorihikisakiCd)
                    {
                        long[] arrSeikyuuNumber = lstSeiikyuuDeleteDto.Where(c => c.TorihikisakiCd == torihikisakiCd).OrderBy(c => c.SeikyuuNumber).Select(c => c.SeikyuuNumber).ToArray();
                        foreach (var seikyuuNumber in arrSeikyuuNumber)
                        {
                            long[] arrNextSeikyuuNumber = this.daoIchiran.GetListSeikyuuNumber(torihikisakiCd, seikyuuNumber);
                            var result = arrNextSeikyuuNumber.Intersect(arrSeikyuuNumber);
                            if (result.Count() == arrNextSeikyuuNumber.Length) //OK
                            {
                                break;
                            }
                            else
                            {
                                foreach (long SeikyuNext in arrNextSeikyuuNumber)
                                {
                                    if (!arrSeikyuuNumber.Contains(SeikyuNext))
                                    {
                                        T_SEIKYUU_DENPYOU seikyuuEntity = this.daoIchiran.GetDataByCd(SeikyuNext.ToString());
                                        if (seikyuuEntity != null)
                                        {
                                            if (arrSeikyuuNumber[arrSeikyuuNumber.Count() - 1] < SeikyuNext)
                                            {
                                                this.errmessage.MessageBoxShow("E303", seikyuuEntity.TORIHIKISAKI_CD.ToString(), seikyuuEntity.SEIKYUU_DATE.Value.ToString("yyyy/MM/dd"), seikyuuEntity.SEIKYUU_NUMBER.ToString());
                                            }
                                            else
                                            {
                                                this.errmessage.MessageBoxShow("E302", seikyuuEntity.TORIHIKISAKI_CD.ToString(), seikyuuEntity.SEIKYUU_DATE.Value.ToString("yyyy/MM/dd"), seikyuuEntity.SEIKYUU_NUMBER.ToString());
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
                LogUtility.DebugMethodEnd(lstSeiikyuuDeleteDto);
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
                if (this.GamenFlg.Equals("1"))//160015
                {
                    parentForm.lb_hint.Text = "一括削除の対象データの場合、チェックを付けてください";
                }
                //160015 S
                else
                {
                    parentForm.lb_hint.Text = "";
                }
                //160015 E
            }
            else
            {
                parentForm.lb_hint.Text = string.Empty;
            }
            this.LockButtonNyuukin();//160015  
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
            if (e.ColumnIndex < 0)
            {
                return;
            }
            var parentForm = (BusinessBaseForm)this.form.Parent;
            if (this.form.customDataGridView1.Columns[e.ColumnIndex].Name.Equals(CELL_CHECKBOX_DEL))
            {
                if (this.GamenFlg.Equals("1"))//160015
                {
                    if (e.RowIndex == -1)
                    {
                        parentForm.lb_hint.Text = "すべての請求データを一括削除したい場合、チェックを付けてください";
                    }
                    else
                    {
                        parentForm.lb_hint.Text = "一括削除の対象データの場合、チェックを付けてください";
                    }
                }
                //160015 S
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
                //160015 E
            }
        }
        private List<CommonKeyDto> GetSelectedSeikyuuNumberMod()
        {
            return this.form.customDataGridView1.Rows.Cast<DataGridViewRow>().Where(w => w.Cells[CELL_CHECKBOX_DEL].ReadOnly == false
                                                                                         && w.Cells[CELL_CHECKBOX_DEL].Value != null
                                                                                         && (bool)w.Cells[CELL_CHECKBOX_DEL].Value
                                                                                         && w.Cells[CELL_SEIKYUU_NUMBER].Value != null)
                                                                             .Select(s => new CommonKeyDto() { Id = Convert.ToInt64(s.Cells[CELL_SEIKYUU_NUMBER].Value.ToString()) })
                                                                             .ToList();
        }
        private T_NYUUKIN_KESHIKOMI[] CheckMultiKeshikomi(long[] arrSeikyuuNumber)
        {
            try
            {
                LogUtility.DebugMethodStart(arrSeikyuuNumber);
                if (arrSeikyuuNumber != null)
                {
                    var keshikomiEntitys = this.nyuukinKeshikomiDao.GetDataForEntity(arrSeikyuuNumber);
                    if (keshikomiEntitys != null && 0 < keshikomiEntitys.Length)
                    {
                        return keshikomiEntitys;
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
                return null;
            }
            finally
            {
                LogUtility.DebugMethodEnd(arrSeikyuuNumber);
            }
            return null;
        }
        //PhuocLoc 2021/05/14 #148574 -End
        #region #158518 - >158519
        internal void SetRequiredSetting()
        {
            // 初期化
            this.form.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
            this.form.ZEI_KOMI_KBN.BackColor = Constans.NOMAL_COLOR;

            this.form.HIDUKE_FROM.RegistCheckMethod = null;
            this.form.HIDUKE_TO.RegistCheckMethod = null;
            this.form.ZEI_KOMI_KBN.RegistCheckMethod = null;

            // 設定
            SelectCheckDto existCheck = new SelectCheckDto();
            existCheck.CheckMethodName = "必須チェック";
            Collection<SelectCheckDto> excitChecks = new Collection<SelectCheckDto>();
            excitChecks.Add(existCheck);

            this.form.HIDUKE_FROM.RegistCheckMethod = excitChecks;
            this.form.HIDUKE_TO.RegistCheckMethod = excitChecks;
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
                this.form.NYUUKIN_YOTEI_BI_HENKOU.Visible = false;
                this.form.NYUUKIN_YOTEI_BI_HENKOU.Text = string.Empty;
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
                this.headForm.lb_title.Text = "請求一覧";
                this.headForm.lb_title.Size = new System.Drawing.Size(188, 35);
                this.form.ZEI_KOMI_KBN.Text = "3";
                this.form.ZEI_KOMI_KBN.Enabled = true;
                this.form.ZEI_KOMI_KBN_1.Enabled = true;
                this.form.ZEI_KOMI_KBN_2.Enabled = true;
                this.form.ZEI_KOMI_KBN_3.Enabled = true;
            }
            else
            {
                this.form.label3.Visible = true;
                this.form.NYUUKIN_YOTEI_BI_HENKOU.Visible = true;
                this.form.NYUUKIN_YOTEI_BI_HENKOU.Text = string.Empty;
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
                this.headForm.lb_title.Text = "入金予定日変更";
                this.headForm.lb_title.Size = new System.Drawing.Size(243, 35);
                this.form.ZEI_KOMI_KBN.Text = "1";
                this.form.ZEI_KOMI_KBN.Enabled = false;
                this.form.ZEI_KOMI_KBN_1.Enabled = true;
                this.form.ZEI_KOMI_KBN_2.Enabled = false;
                this.form.ZEI_KOMI_KBN_3.Enabled = false;
            }
        }
        private void NYUUKIN_YOTEI_DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.NYUUKIN_YOTEI_DATE_FROM;
            var ToTextBox = this.form.NYUUKIN_YOTEI_DATE_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        private List<DataGridViewRow> GetRowChecked(bool isF9Flg = false)
        {
            if (!this.form.customDataGridView1.Columns.Contains(CELL_NYUUKIN_YOTEI_BI_HENKO))
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
                ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells[CELL_NYUUKIN_YOTEI_BI_HENKO], false);
                bool check = ObjectExtension.ConvertToBoolean(row.Cells[CELL_CHECKBOX_DEL].Value, false);
                if (check)
                {
                    if (isF9Flg)
                    {
                        string nyuukinHenkou = StringUtil.ConverToString(row.Cells[CELL_NYUUKIN_YOTEI_BI_HENKO].Value);
                        if (string.IsNullOrEmpty(nyuukinHenkou))
                        {
                            ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells[CELL_NYUUKIN_YOTEI_BI_HENKO], true);
                            errF9 = 1;
                        }
                        else
                        {
                            DateTime seikyuuDate = row.Cells[CELL_SEIKYUU_DATE].Value.ConvertToDateTime();
                            DateTime nyuukinDate = row.Cells[CELL_NYUUKIN_YOTEI_BI_HENKO].Value.ConvertToDateTime();
                            if (seikyuuDate.CompareTo(nyuukinDate) > 0)
                            {
                                ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells[CELL_NYUUKIN_YOTEI_BI_HENKO], true);
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
                MessageBoxUtility.MessageBoxShow("E001", "入金予定日(変更後)");
                return null;
            }
            if (errF9 == 2)
            {
                string[] errorMsg = { "請求日付", "入金予定日" };
                MessageBoxUtility.MessageBoxShow("E030", errorMsg);
                return null;
            }
            return arrRow;
        }
        internal void LockButtonNyuukin()
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
                    decimal nyuukingaku = NumberUtil.ConvertToDecimal(currenRow.Cells["NYUUKIN_KOMI_GAKU"].Value);
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
                    nyuukinPrm.Add(currenRow.Cells[CELL_SEIKYUU_DATE].Value.ConvertToString(string.Empty));
                    //実行⇒現金入金
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
                    nyuukinPrm.Add(currenRow.Cells[CELL_SEIKYUU_NUMBER].Value.ConvertToString(string.Empty));
                    FormManager.OpenFormWithAuth("G619", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, nyuukinPrm);
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

        #region INXS請求書発行 refs #158002

        private void AddSubFunction()
        {
            CustomButton bt_process6 = new CustomButton();
            bt_process6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            bt_process6.DefaultBackColor = System.Drawing.Color.Empty;
            bt_process6.Enabled = false;
            bt_process6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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

            FormManager.OpenFormWithAuth("G745", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion
    }
}
