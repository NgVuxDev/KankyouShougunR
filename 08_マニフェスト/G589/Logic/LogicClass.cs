using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Message;
using Shougun.Core.PaperManifest.Himodukeichiran.DAO;
using Shougun.Core.PaperManifest.Himodukeichiran.DBAccesser;
using Seasar.Framework.Exceptions;


namespace Shougun.Core.PaperManifest.Himodukeichiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicClass : IBuisinessLogic
    {
        #region フィールド

        // 抽出対象区分
        const int IJIMANIFEST = 1;
        const int NIJIMANIFEST = 2;

        // 紐付状況
        const int SUMI = 1;
        const int MI = 2;
        const int ALL = 3;

        // マニフェスト種類
        const int SANPAI_CHOKKO = 1;
        const int SANPAI_TUMIKAE = 2;
        const int KENPAI = 3;
        const int DENSHI = 4;
        const int ALL_MANIFEST = 5;

        // 抽出日付
        const int KOUFU_DATE = 1;
        const int UNPAN_SHURYOU_DATE = 2;
        const int SHOBUN_SHURYOU_DATE = 3;
        const int SAISHU_SHOBUN_SHURYOU_DATE = 4;

        // 抽出業者
        const int HAICHU_JIGYOSHA = 1;
        const int UNPAN_JUTAKUSHA = 2;
        const int SHOBUN_JUTAKUSHA = 2;
        const int SAISHU_SHOBUN_BASHO = 2;

        const string MSG_ERR_MANI_DATA_NOT_FOUND = "{0}マニフェスト情報が紐づいていません";//VAN 20210507 #148581

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.Himodukeichiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// DBアクセッサー
        /// </summary>
        private DBAccessor accessor;

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        // 20140605 katen 不具合No.4690 start‏
        /// <summary>
        /// マニフェスト検索Dao
        /// </summary>
        private CommonEntryDaoCls EntryDao;
        // 20140605 katen 不具合No.4690 start‏

        /// <summary>
        /// UriageShiharaiIchiranForm form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// HeaderForm headForm
        /// </summary>
        UIHeader headForm;

        /// <summary>
        /// 社員コード
        /// </summary>
        public string syainCode { get; set; }

        /// <summary>
        /// 伝種区分
        /// </summary>
        public DENSHU_KBN denShu_Kbn { get; set; }

        /// <summary>
        /// SQL文
        /// </summary>
        private StringBuilder sql;

        /// <summary>
        /// 作成したSQL
        /// </summary>
        public string mcreateSql { get; set; }

        /// <summary>
        /// コントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private DAOClass daoIchiran;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// フッター
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// 作成したSQL
        /// </summary>
        public string createSql { get; set; }

        // 20140606 katen 不具合No.4690 start‏
        /// <summary>共通</summary>
        ManifestoLogic mlogic = null;
        // 20140606 katen 不具合No.4690 end‏

        /// <summary>
        /// システム設定
        /// </summary>
        internal M_SYS_INFO sysInfo { get; set; }

        private MessageBoxShowLogic MsgBox;
        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable searchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public string searchString { get; set; }

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
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }

        #endregion

        #region 一覧列名

        /// <summary>SYSTEM_ID</summary>
        internal readonly string KOUFU_NO_IJI = "交付番号（1次）";
        internal readonly string KOUFU_DATE_IJI = "交付日付（1次）";
        internal readonly string HAISHU_JIGYOSHA_IJI = "排出事業者（1次）";
        internal readonly string HAISHU_JIGYOBA_IJI = "排出事業場（1次）";
        internal readonly string UNPAN_JUTAKUSHA_IJI = "運搬受託者（1次）";
        internal readonly string HAIKIBUTU_SHURUI_IJI = "廃棄物種類（1次）";
        internal readonly string GENNYOU_SU_IJI = "減容後数量（1次）";
        internal readonly string UNPAN_END_DATE_IJI = "運搬終了日（1次）";
        internal readonly string SHOBUN_END_DATE_IJI = "処分終了日（1次）";
        internal readonly string KOUFU_NO_NIJI = "交付番号（2次）";
        internal readonly string KOUFU_DATE_NIJI = "交付日付（2次）";
        internal readonly string UNPAN_JUTAKUSHA_NIJI = "運搬受託者（2次）";
        internal readonly string SHOBUN_JUTAKUSHA_NIJI = "処分受託者（2次）";
        internal readonly string SHOBUN_JIGYOBA_NIJI = "処分事業場（2次）";
        internal readonly string LAST_SHOBUN_GENBA_NIJI = "最終処分場所";
        internal readonly string HAIKIBUTU_SHURUI_NIJI = "廃棄物種類（2次）";
        internal readonly string KANSAN_SU_NIJI = "換算後数量（2次）";
        internal readonly string UNPAN_END_DATE_NIJI = "運搬終了日（2次）";
        internal readonly string SHOBUN_END_DATE_NIJI = "処分終了日（2次）";
        internal readonly string LAST_SHOBUN_END_DATE = "最終処分終了日";
        internal readonly string HIMOTUKI_JYOUKYOU = "紐付状況";
        internal readonly string HIDDEN_HAIKI_KBN_CD_IJI = "HIDDEN_HAIKI_KBN_CD_IJI";
        internal readonly string HIDDEN_HAIKI_KBN_CD_NIJI = "HIDDEN_HAIKI_KBN_CD_NIJI";
        internal readonly string HIDDEN_SYSTEM_ID_IJI = "HIDDEN_SYSTEM_ID_IJI";
        internal readonly string HIDDEN_SYSTEM_ID_NIJI = "HIDDEN_SYSTEM_ID_NIJI";
        internal readonly string HIDDEN_SEQ_IJI = "HIDDEN_SEQ_IJI";
        internal readonly string HIDDEN_SEQ_NIJI = "HIDDEN_SEQ_NIJI";
        internal readonly string HIDDEN_KANRI_ID_IJI = "HIDDEN_KANRI_ID_IJI";
        internal readonly string HIDDEN_KANRI_ID_NIJI = "HIDDEN_KANRI_ID_NIJI";
        internal readonly string HIDDEN_LATEST_SEQ_IJI = "HIDDEN_LATEST_SEQ_IJI";
        internal readonly string HIDDEN_LATEST_SEQ_NIJI = "HIDDEN_LATEST_SEQ_NIJI";

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.form = targetForm;
            this.searchResult = new DataTable();
            this.dto = new DTOClass();
            this.daoIchiran = DaoInitUtility.GetComponent<DAOClass>();
            // 20140605 katen 不具合No.4690 start‏
            this.EntryDao = DaoInitUtility.GetComponent<CommonEntryDaoCls>();
            this.mlogic = new ManifestoLogic();
            // 20140605 katen 不具合No.4690 start‏
            // Accessor
            this.accessor = new DBAccessor();
            this.MsgBox = new MessageBoxShowLogic();
        }
        #endregion

        #region へーだ設定
        /// <summary>
        /// へーだ設定
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
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                sysInfo = this.sysInfoDao.GetAllDataForCode(this.form.SystemId.ToString());
                if (sysInfo != null)
                {
                    // システム情報からアラート件数を取得
                    this.alertCount = (int)sysInfo.ICHIRAN_ALERT_KENSUU;
                }

                this.allControl = this.form.allControl;
                //行の追加オプション(false)
                this.form.customDataGridView1.AllowUserToAddRows = false;

                // 上で子フォームへのデータバインドを行っている為、以下の初期化処理はそれより下に配置（二度呼ばれることを避ける）
                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                //画面の初期表示時日付CDを設定する
                DateTime now = this.parentForm.sysDate;
                this.form.HIDUKE_FROM.Value = now;
                this.form.HIDUKE_TO.Value = now;

                //検索ボタンの初期表示
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.bt_func1.Text = string.Empty;
                parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle("マニフェスト紐付状況一覧");
                //抽出対象区分にフォーカスをセットする
                this.form.txtNum_ChushutuTaishouKbn.Focus();

                //アラート件数
                M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
                this.headForm.InitialNumberAlert = int.Parse(mSysInfo.ICHIRAN_ALERT_KENSUU.ToString());
                this.headForm.NumberAlert = this.headForm.InitialNumberAlert;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
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

        #endregion

        #region イベント処理の初期化
        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            parentForm = (BusinessBaseForm)this.form.Parent;

            //抽出業者切り替えイベント
            this.form.txtNum_ChushutuGyosha.TextChanged += new EventHandler(this.form.txtNum_ChushutuGyosha_TextChanged);

            //Functionボタンのイベント生成
            parentForm.bt_func2.Click += new EventHandler(this.form.bt_func2_Click);              //F2 新規
            parentForm.bt_func3.Click += new System.EventHandler(this.form.bt_func3_Click);       //F3 修正
            parentForm.bt_func4.Click += new System.EventHandler(this.form.bt_func4_Click);       //F4 削除
            parentForm.bt_func6.Click += new System.EventHandler(this.form.bt_func6_Click);       //F6 CSV出力
            parentForm.bt_func7.Click += new System.EventHandler(this.form.bt_func7_Click);       //F7 検索条件クリア
            parentForm.bt_func8.Click += new System.EventHandler(this.form.bt_func8_Click);       //F8 検索
            parentForm.bt_func10.Click += new System.EventHandler(this.form.bt_func10_Click);     //F10 並び替え
            parentForm.bt_func11.Click += new System.EventHandler(this.form.bt_func11_Click);          //F11 フィルタ
            parentForm.bt_func12.Click += new System.EventHandler(this.form.bt_func12_Click);     //閉じる
            //VAN 20210507 #148581 S
            parentForm.bt_process1.Click += new System.EventHandler(this.form.bt_process1_Click);    //[1]一次マニ表示
            parentForm.bt_process2.Click += new System.EventHandler(this.form.bt_process2_Click);    //[2]二次マニ表示
            //VAN 20210507 #148581 E

            //明細データダブルクリックイベント
            this.form.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.form.customDataGridView1_CellDoubleClick);
            //明細フォーマット
            this.form.customDataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(this.form.customDataGridView1_CellFormatting);
            //前回値保存の仕組み初期化
            this.form.EnterEventInit();
            //画面上でESCキー押下時のイベント生成
            this.form.PreviewKeyDown += new PreviewKeyDownEventHandler(this.form.form_PreviewKeyDown); //form上でのESCキー押下でFocus移動

            /// 20141128 Houkakou 「紐付状況一覧」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.form.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDUKE_TO_MouseDoubleClick);
            /// 20141128 Houkakou 「紐付状況一覧」のダブルクリックを追加する　end
            LogUtility.DebugMethodEnd();
        }

        #endregion

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

        #region 検索
        /// <summary>
        /// 検索
        /// </summary>
        public int Search()
        {
            int ret_cnt = 0;
            try
            {
                LogUtility.DebugMethodStart();

                this.searchResult = new DataTable();
                sql = new StringBuilder();

                #region SELECT句
                sql.Append("SELECT *");
                sql.Append("  FROM (SELECT DISTINCT");
                sql.Append("       CASE");
                sql.Append("       WHEN MANIFEST_RELATION.NEXT_SYSTEM_ID IS NOT NULL");
                sql.Append("       THEN CASE");
                sql.Append("            WHEN FIRST_MANIFEST.SYSTEM_ID IS NOT NULL AND SECOND_MANIFEST.SYSTEM_ID IS NOT NULL");
                sql.Append("            THEN '済'");
                sql.Append("            WHEN FIRST_MANIFEST.SYSTEM_ID IS NULL AND SECOND_MANIFEST.SYSTEM_ID IS NOT NULL");
                // 20140606 katen 不具合No.4692 start‏
                sql.Append("            THEN '紐付不整合'");
                sql.Append("            WHEN FIRST_MANIFEST.SYSTEM_ID IS NOT NULL AND SECOND_MANIFEST.SYSTEM_ID IS NULL");
                sql.Append("            THEN '紐付不整合'");
                sql.Append("            END");
                sql.Append("       ELSE CASE");
                sql.Append("            WHEN FIRST_MANIFEST.HAIKI_KBN_CD != 4");
                sql.Append("                THEN CASE");
                sql.Append("                    WHEN FIRST_MANIFEST.SYSTEM_ID IS NOT NULL AND FIRST_MANIFEST.LAST_SBN_END_DATE IS NOT NULL");
                sql.Append("                    THEN '2次なし'");
                sql.Append("                    ELSE '未'");
                sql.Append("                END");
                sql.Append("            ELSE CASE");
                sql.Append("                WHEN FIRST_MANIFEST.SBN_ENDREP_KBN = 2");
                sql.Append("                THEN '2次なし'");
                sql.Append("                ELSE '未'");
                sql.Append("                END");
                sql.Append("            END");
                // 20140606 katen 不具合No.4692 end‏
                sql.AppendFormat(" END                                 AS \"{0}\",", this.HIMOTUKI_JYOUKYOU);           //紐付状況
                sql.AppendFormat(" FIRST_MANIFEST.MANIFEST_ID          AS \"{0}\",", this.KOUFU_NO_IJI);                //交付番号（1次）
                sql.AppendFormat(" FIRST_MANIFEST.KOUFU_DATE           AS \"{0}\",", this.KOUFU_DATE_IJI);              //交付日付（1次）
                sql.AppendFormat(" LTRIM(FIRST_MANIFEST.HST_GYOUSHA_NAME) AS \"{0}\",", this.HAISHU_JIGYOSHA_IJI);      //排出事業者（1次）
                sql.AppendFormat(" LTRIM(FIRST_MANIFEST.HST_GENBA_NAME)   AS \"{0}\",", this.HAISHU_JIGYOBA_IJI);       //排出事業場（1次）
                sql.AppendFormat(" FIRST_MANIFEST.UPN_GYOUSHA_NAME     AS \"{0}\",", this.UNPAN_JUTAKUSHA_IJI);         //運搬受託者（1次）
                sql.AppendFormat(" FIRST_MANIFEST.HAIKI_SHURUI_NAME    AS \"{0}\",", this.HAIKIBUTU_SHURUI_IJI);        //廃棄物種類（1次）
                sql.AppendFormat(" FIRST_MANIFEST.GENNYOU_SUU          AS \"{0}\",", this.GENNYOU_SU_IJI);              //減容後数量（1次）
                sql.AppendFormat(" FIRST_MANIFEST.UPN_END_DATE         AS \"{0}\",", this.UNPAN_END_DATE_IJI);          //運搬終了日（1次）
                sql.AppendFormat(" FIRST_MANIFEST.SBN_END_DATE         AS \"{0}\",", this.SHOBUN_END_DATE_IJI);         //処分終了日（1次）
                sql.AppendFormat(" SECOND_MANIFEST.MANIFEST_ID         AS \"{0}\",", this.KOUFU_NO_NIJI);               //交付番号（2次）
                sql.AppendFormat(" SECOND_MANIFEST.KOUFU_DATE          AS \"{0}\",", this.KOUFU_DATE_NIJI);             //交付日付（2次）
                sql.AppendFormat(" SECOND_MANIFEST.UPN_GYOUSHA_NAME    AS \"{0}\",", this.UNPAN_JUTAKUSHA_NIJI);        //運搬受託者（2次）
                sql.AppendFormat(" SECOND_MANIFEST.SBN_JYUTAKUSHA_NAME AS \"{0}\",", this.SHOBUN_JUTAKUSHA_NIJI);       //処分受託者（2次）
                sql.AppendFormat(" SECOND_MANIFEST.UPN_SAKI_GENBA_NAME AS \"{0}\",", this.SHOBUN_JIGYOBA_NIJI);         //処分事業場（2次）
                sql.AppendFormat(" SECOND_MANIFEST.LAST_SBN_GENBA_NAME AS \"{0}\",", this.LAST_SHOBUN_GENBA_NIJI);      //最終処分場所
                sql.AppendFormat(" SECOND_MANIFEST.HAIKI_SHURUI_NAME   AS \"{0}\",", this.HAIKIBUTU_SHURUI_NIJI);       //廃棄物種類（2次）
                sql.AppendFormat(" SECOND_MANIFEST.KANSAN_SUU          AS \"{0}\",", this.KANSAN_SU_NIJI);              //換算後数量（2次）
                sql.AppendFormat(" SECOND_MANIFEST.UPN_END_DATE        AS \"{0}\",", this.UNPAN_END_DATE_NIJI);         //運搬終了日（2次）
                sql.AppendFormat(" SECOND_MANIFEST.SBN_END_DATE        AS \"{0}\",", this.SHOBUN_END_DATE_NIJI);        //処分終了日（2次）
                sql.Append("       CASE");
                sql.Append("       WHEN MANIFEST_RELATION.NEXT_SYSTEM_ID IS NOT NULL");
                sql.Append("       THEN CASE");
                sql.Append("            WHEN FIRST_MANIFEST.SYSTEM_ID IS NOT NULL AND SECOND_MANIFEST.SYSTEM_ID IS NOT NULL");
                sql.Append("            THEN SECOND_MANIFEST.LAST_SBN_END_DATE");
                sql.Append("            WHEN FIRST_MANIFEST.SYSTEM_ID IS NULL AND SECOND_MANIFEST.SYSTEM_ID IS NOT NULL");
                sql.Append("            THEN SECOND_MANIFEST.LAST_SBN_END_DATE");
                sql.Append("            WHEN FIRST_MANIFEST.SYSTEM_ID IS NOT NULL AND SECOND_MANIFEST.SYSTEM_ID IS NULL");
                sql.Append("            THEN FIRST_MANIFEST.LAST_SBN_END_DATE");
                sql.Append("            END");
                sql.Append("       ELSE CASE");
                sql.Append("            WHEN FIRST_MANIFEST.SYSTEM_ID IS NULL AND SECOND_MANIFEST.SYSTEM_ID IS NOT NULL");
                sql.Append("            THEN SECOND_MANIFEST.LAST_SBN_END_DATE");
                sql.Append("            WHEN FIRST_MANIFEST.SYSTEM_ID IS NOT NULL AND SECOND_MANIFEST.SYSTEM_ID IS NULL");
                sql.Append("            THEN FIRST_MANIFEST.LAST_SBN_END_DATE");
                sql.Append("            END");
                sql.AppendFormat(" END                                 AS \"{0}\",", this.LAST_SHOBUN_END_DATE);        //最終処分終了日
                sql.AppendFormat(" FIRST_MANIFEST.HAIKI_KBN_CD         AS \"{0}\",", this.HIDDEN_HAIKI_KBN_CD_IJI);     //廃棄区分（1次）（非表示）
                sql.AppendFormat(" FIRST_MANIFEST.SYSTEM_ID            AS \"{0}\",", this.HIDDEN_SYSTEM_ID_IJI);        //システムID（1次）（非表示）
                sql.AppendFormat(" FIRST_MANIFEST.SEQ                  AS \"{0}\",", this.HIDDEN_SEQ_IJI);              //SEQ（1次）（非表示）
                sql.AppendFormat(" FIRST_MANIFEST.KANRI_ID             AS \"{0}\",", this.HIDDEN_KANRI_ID_IJI);         //管理ID（1次）（非表示）
                sql.AppendFormat(" FIRST_MANIFEST.LATEST_SEQ           AS \"{0}\",", this.HIDDEN_LATEST_SEQ_IJI);       //最終SEQ（1次）（非表示）
                sql.AppendFormat(" SECOND_MANIFEST.HAIKI_KBN_CD        AS \"{0}\",", this.HIDDEN_HAIKI_KBN_CD_NIJI);    //廃棄区分（2次）（非表示）
                sql.AppendFormat(" SECOND_MANIFEST.SYSTEM_ID           AS \"{0}\",", this.HIDDEN_SYSTEM_ID_NIJI);       //システムID（2次）（非表示）
                sql.AppendFormat(" SECOND_MANIFEST.SEQ                 AS \"{0}\",", this.HIDDEN_SEQ_NIJI);             //SEQ（2次）（非表示）
                sql.AppendFormat(" SECOND_MANIFEST.KANRI_ID            AS \"{0}\",", this.HIDDEN_KANRI_ID_NIJI);        //管理ID（2次）（非表示）
                sql.AppendFormat(" SECOND_MANIFEST.LATEST_SEQ          AS \"{0}\" ", this.HIDDEN_LATEST_SEQ_NIJI);      //最終SEQ（2次）（非表示）
                sql.Append("");
                String MOutputPatternSelect = this.selectQuery;
                if (!String.IsNullOrEmpty(MOutputPatternSelect))
                {
                    sql.Append(", ");
                    sql.Append(MOutputPatternSelect);
                }
                #endregion

                #region FROM句
                sql.Append("          FROM (SELECT * FROM T_MANIFEST_RELATION WHERE DELETE_FLG = 0) MANIFEST_RELATION");
                sql.Append("     FULL JOIN (");

                //紙マニ（1次）のデータ
                sql.Append("                SELECT TME_FIRST.SYSTEM_ID,");
                sql.Append("                       TME_FIRST.SEQ,");
                sql.Append("                       NULL AS KANRI_ID,");
                sql.Append("                       NULL AS LATEST_SEQ,");
                sql.Append("                       TME_FIRST.KOUFU_KBN,");
                sql.Append("                       TME_FIRST.KOUFU_DATE,");
                sql.Append("                       TME_FIRST.MANIFEST_ID,");
                sql.Append("                       TME_FIRST.HAIKI_KBN_CD,");
                sql.Append("                       TME_FIRST.HST_GYOUSHA_CD,");
                sql.Append("                       TME_FIRST.SBN_GYOUSHA_CD AS SBN_JYUTAKUSHA_CD,");
                sql.Append("                       TMD_FIRST.LAST_SBN_GYOUSHA_CD,");
                sql.Append("                       TME_FIRST.HST_GENBA_CD,");
                sql.Append("                       TMD_FIRST.LAST_SBN_GENBA_CD,");
                sql.Append("                       RTRIM(SUBSTRING(ISNULL(TME_FIRST.HST_GYOUSHA_NAME, ''),1, 40)) + SUBSTRING(ISNULL(TME_FIRST.HST_GYOUSHA_NAME, ''),41, 40) AS HST_GYOUSHA_NAME,");
                sql.Append("                       RTRIM(SUBSTRING(ISNULL(TME_FIRST.HST_GENBA_NAME, ''),1, 40)) + SUBSTRING(ISNULL(TME_FIRST.HST_GENBA_NAME, ''),41, 40) AS HST_GENBA_NAME,");
                sql.Append("                       TMD_FIRST.DETAIL_SYSTEM_ID,");
                sql.Append("                       TMU_FIRST_UPN.UPN_GYOUSHA_CD,");
                sql.Append("                       TMU_FIRST_SBN.UPN_SAKI_GENBA_CD,");
                sql.Append("                       TMU_FIRST_SBN.UPN_SAKI_GENBA_NAME,");
                sql.Append("                       TMU_FIRST_UPN.UPN_GYOUSHA_NAME,");
                sql.Append("                       TMU_FIRST_SBN.UPN_END_DATE,");
                sql.Append("                       TMD_FIRST.SBN_END_DATE,");
                sql.Append("                       TMD_FIRST.HAIKI_SHURUI_CD,");
                sql.Append("                       HAIKI_FIRST.HAIKI_SHURUI_NAME,");
                sql.Append("                       TMD_FIRST.GENNYOU_SUU,");
                sql.Append("                       TMD_FIRST.KANSAN_SUU,");
                sql.Append("                       TMD_FIRST.LAST_SBN_END_DATE,");
                sql.Append("                       NULL AS SBN_ENDREP_KBN");
                sql.Append("                  FROM (SELECT * FROM T_MANIFEST_ENTRY WHERE FIRST_MANIFEST_KBN = 0 AND DELETE_FLG = 0) TME_FIRST");
                sql.Append("             LEFT JOIN T_MANIFEST_DETAIL TMD_FIRST");
                sql.Append("                    ON TME_FIRST.SYSTEM_ID = TMD_FIRST.SYSTEM_ID");
                sql.Append("                   AND TME_FIRST.SEQ       = TMD_FIRST.SEQ");
                //sql.Append("             LEFT JOIN T_MANIFEST_UPN TMU_FIRST");
                sql.Append("             LEFT JOIN T_MANIFEST_UPN TMU_FIRST_UPN");
                sql.Append("                    ON TME_FIRST.SYSTEM_ID = TMU_FIRST_UPN.SYSTEM_ID");
                sql.Append("                   AND TME_FIRST.SEQ       = TMU_FIRST_UPN.SEQ");
                // 20140616 katen 不具合No.4809 start‏
                sql.Append("                   AND TMU_FIRST_UPN.UPN_ROUTE_NO = 1");
                //sql.Append("             LEFT JOIN (SELECT T_MANIFEST_ENTRY.SYSTEM_ID,");
                //sql.Append("                               T_MANIFEST_ENTRY.SEQ,");
                //sql.Append("                               MAX(T_MANIFEST_UPN.UPN_ROUTE_NO) AS UPN_ROUTE_NO");
                //sql.Append("                          FROM T_MANIFEST_ENTRY");
                //sql.Append("                    INNER JOIN T_MANIFEST_UPN");
                //sql.Append("                            ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_UPN.SYSTEM_ID");
                //sql.Append("                           AND T_MANIFEST_ENTRY.SEQ       = T_MANIFEST_UPN.SEQ");
                //sql.Append("                           AND T_MANIFEST_UPN.UPN_END_DATE IS NOT NULL");
                //sql.Append("                         WHERE T_MANIFEST_ENTRY.DELETE_FLG = 'false'");
                //sql.Append("                      GROUP BY T_MANIFEST_ENTRY.SYSTEM_ID,");
                //sql.Append("                               T_MANIFEST_ENTRY.SEQ) TMU_SEARCH");
                //sql.Append("                    ON TMU_FIRST.SYSTEM_ID    = TMU_SEARCH.SYSTEM_ID");
                //sql.Append("                   AND TMU_FIRST.SEQ          = TMU_SEARCH.SEQ");
                //sql.Append("                   AND TMU_FIRST.UPN_ROUTE_NO = TMU_SEARCH.UPN_ROUTE_NO");
                //sql.Append("             LEFT JOIN T_MANIFEST_UPN MIN_TMU");
                //sql.Append("                    ON TME_FIRST.SYSTEM_ID  = MIN_TMU.SYSTEM_ID");
                //sql.Append("                   AND TME_FIRST.SEQ        = MIN_TMU.SEQ");
                //sql.Append("                   AND MIN_TMU.UPN_ROUTE_NO = 1");
                // 20140616 katen 不具合No.4809 end‏
                // 処分事業場を現場入力済みのMAXルート番号で取得する
                sql.Append("             LEFT JOIN (");
                sql.Append("                        SELECT TMU.*");
                sql.Append("                        FROM T_MANIFEST_UPN TMU,");
                sql.Append("                             (SELECT SYSTEM_ID, SEQ, MAX(UPN_ROUTE_NO) AS UPN_ROUTE_NO");
                sql.Append("                              FROM (");
                sql.Append("                                     SELECT SYSTEM_ID, SEQ, UPN_ROUTE_NO");
                sql.Append("                                     FROM T_MANIFEST_UPN");
                sql.Append("                                     WHERE UPN_GYOUSHA_CD IS NOT NULL AND UPN_GYOUSHA_CD <> '' AND UPN_SAKI_GYOUSHA_CD IS NOT NULL AND UPN_SAKI_GYOUSHA_CD <> ''");
                sql.Append("                                   ) TMU_GROUP");
                sql.Append("                              GROUP BY SYSTEM_ID, SEQ");
                sql.Append("                             ) AS TMU_ROUTE_SBN");
                sql.Append("                        WHERE TMU.SYSTEM_ID = TMU_ROUTE_SBN.SYSTEM_ID");
                sql.Append("                              AND TMU.SEQ = TMU_ROUTE_SBN.SEQ");
                sql.Append("                              AND TMU.UPN_ROUTE_NO = TMU_ROUTE_SBN.UPN_ROUTE_NO");
                sql.Append("                       ) TMU_FIRST_SBN");
                sql.Append("                    ON TME_FIRST.SYSTEM_ID = TMU_FIRST_SBN.SYSTEM_ID");
                sql.Append("                   AND TME_FIRST.SEQ       = TMU_FIRST_SBN.SEQ");
                sql.Append("             LEFT JOIN M_HAIKI_SHURUI HAIKI_FIRST");
                sql.Append("                    ON TMD_FIRST.HAIKI_SHURUI_CD = HAIKI_FIRST.HAIKI_SHURUI_CD");
                sql.Append("                   AND TME_FIRST.HAIKI_KBN_CD    = HAIKI_FIRST.HAIKI_KBN_CD");

                sql.Append("             UNION ALL");

                //電子マニ（1次）のデータ
                sql.Append("                SELECT (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.DETAIL_SYSTEM_ID ELSE R18EX.SYSTEM_ID END) AS SYSTEM_ID,");
                sql.Append("                       (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.SEQ ELSE R18EX.SEQ END) AS SEQ,");
                sql.Append("                       DMT.KANRI_ID                                                          AS KANRI_ID,");
                sql.Append("                       DMT.LATEST_SEQ                                                        AS LATEST_SEQ,");
                // #1398 電子マニには「交付区分」が存在しないため、一覧画面で指定された交付区分をそのまま使用する
                sql.AppendFormat(
                           "                       ''                                                                   AS KOUFU_KBN,");
                sql.Append("                       CASE ISDATE(DT_R18.HIKIWATASHI_DATE) ");
                sql.Append("                       WHEN 0 ");
                sql.Append("                       THEN NULL");
                sql.Append("                       ELSE");
                sql.Append("                       CONVERT(datetime,DT_R18.HIKIWATASHI_DATE,120) ");
                sql.Append("                       END                                                                   AS KOUFU_DATE,");
                sql.Append("                       DT_R18.MANIFEST_ID                                                    AS MANIFEST_ID,");
                sql.Append("                       CAST('4' AS SMALLINT )                                                AS HAIKI_KBN_CD,");
                sql.Append("                       R18EX.HST_GYOUSHA_CD                                                  AS HST_GYOUSHA_CD,");
                sql.Append("                       R18EX.SBN_GYOUSHA_CD                                                  AS SBN_JYUTAKUSHA_CD,");
                sql.Append("                       DT_R13_EX.LAST_SBN_GYOUSHA_CD                                         AS LAST_SBN_GYOUSHA_CD,");
                sql.Append("                       R18EX.HST_GENBA_CD                                                    AS HST_GENBA_CD,");
                sql.Append("                       DT_R13_EX.LAST_SBN_GENBA_CD                                           AS LAST_SBN_GENBA_CD,");
                sql.Append("                       DT_R18.HST_SHA_NAME                                                   AS HST_GYOUSHA_NAME,");
                sql.Append("                       DT_R18.HST_JOU_NAME                                                   AS HST_GENBA_NAME,");
                sql.Append("                       (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.DETAIL_SYSTEM_ID ELSE R18EX.SYSTEM_ID END) AS DETAIL_SYSTEM_ID,");
                sql.Append("                       MINR19EX.UPN_GYOUSHA_CD                                               AS UPN_GYOUSHA_CD,");
                sql.Append("                       MAXR19EX.UPNSAKI_GENBA_CD                                             AS UPN_SAKI_GENBA_CD,");
                sql.Append("                       MAXR19.UPNSAKI_JOU_NAME                                               AS UPN_SAKI_GENBA_NAME,");
                sql.Append("                       MAXR19.UPN_SHA_NAME                                                   AS UPN_GYOUSHA_NAME,");
                sql.Append("                       CONVERT(datetime, CASE");
                sql.Append("                                         WHEN MAXR19.UPN_END_DATE = '0' OR MAXR19.UPN_END_DATE = ''");
                sql.Append("                                         THEN NULL");
                sql.Append("                                         ELSE MAXR19.UPN_END_DATE");
                sql.Append("                                         END ,120)                                           AS UPN_END_DATE,");
                sql.Append("                       CONVERT(datetime, CASE");
                sql.Append("                                         WHEN DT_R18.SBN_END_DATE = '0' OR DT_R18.SBN_END_DATE = ''");
                sql.Append("                                         THEN NULL");
                sql.Append("                                         ELSE DT_R18.SBN_END_DATE");
                sql.Append("                                         END ,120)                                           AS SBN_END_DATE,");
                sql.Append("                       (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.HAIKI_DAI_CODE + MIX.HAIKI_CHU_CODE + MIX.HAIKI_SHO_CODE");
                sql.Append("                             ELSE DT_R18.HAIKI_DAI_CODE + DT_R18.HAIKI_CHU_CODE + DT_R18.HAIKI_SHO_CODE END) AS HAIKI_SHURUI_CD,");
                sql.Append("                       (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.HAIKI_SHURUI_NAME ELSE DT_R18.HAIKI_SHURUI END) AS HAIKI_SHURUI_NAME,");
                sql.Append("                       (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.GENNYOU_SUU ELSE R18EX.GENNYOU_SUU END) AS GENNYOU_SUU,");
                sql.Append("                       (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.KANSAN_SUU");
                sql.Append("                       ELSE (SELECT CASE WHEN TBL.KANSANSHIKI=0 THEN DT_R18.HAIKI_SUU*TBL.KANSANCHI");
                sql.Append("                                    ELSE DT_R18.HAIKI_SUU/TBL.KANSANCHI");
                sql.Append("                                    END");
                sql.Append("                          FROM (SELECT KANSANSHIKI,");
                sql.Append("                                       KANSANCHI");
                sql.Append("                                  FROM M_MANIFEST_KANSAN");
                sql.Append("                                 WHERE DELETE_FLG           = 0 ");
                sql.Append("                                   AND HOUKOKUSHO_BUNRUI_CD = MDHS.HOUKOKUSHO_BUNRUI_CD");
                sql.Append("                                   AND HAIKI_NAME_CD        = (SELECT TOP 1 HAIKI_NAME_CD");
                sql.Append("                                                                 FROM M_DENSHI_HAIKI_NAME");
                sql.Append("                                                                WHERE DELETE_FLG = 0");
                sql.Append("                                                                  AND HAIKI_NAME = DT_R18.HAIKI_NAME");
                sql.Append("                                                               )");
                sql.Append("                                   AND UNIT_CD              = DT_R18.HAIKI_UNIT_CODE");
                sql.Append("                                   AND NISUGATA_CD          = DT_R18.NISUGATA_CODE");
                sql.Append("                                ) TBL");
                sql.Append("                       ) END)                                                                     AS KANSAN_SUU,");
                sql.Append("                       (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL AND MIX.SBN_ENDREP_KBN = 2 THEN MIX.LAST_SBN_END_DATE ELSE DT_R13.LAST_SBN_END_DATE END) AS LAST_SBN_END_DATE,");
                sql.Append("                       (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.SBN_ENDREP_KBN ELSE DT_R18.SBN_ENDREP_KBN END) AS SBN_ENDREP_KBN");
                sql.Append("                  FROM DT_MF_TOC DMT");
                sql.Append("            INNER JOIN DT_R18");
                sql.Append("                    ON DMT.KANRI_ID   = DT_R18.KANRI_ID");
                sql.Append("                   AND DMT.LATEST_SEQ = DT_R18.SEQ");
                sql.Append("                   AND DT_R18.MANIFEST_ID IS NOT NULL");
                sql.Append("                   AND DT_R18.MANIFEST_ID <> ''");
                sql.Append("            INNER JOIN DT_R18_EX R18EX");
                sql.Append("                    ON R18EX.KANRI_ID   = DT_R18.KANRI_ID");
                sql.Append("                   AND R18EX.DELETE_FLG = 0");
                sql.Append("            LEFT JOIN (");
                sql.Append("                        SELECT");
                sql.Append("                            M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_NAME");
                sql.Append("                            ,DT_R18_MIX.*");
                sql.Append("                        FROM");
                sql.Append("                            DT_R18_MIX");
                sql.Append("                        INNER JOIN (SELECT * FROM M_DENSHI_HAIKI_SHURUI WHERE DELETE_FLG = 0) AS M_DENSHI_HAIKI_SHURUI");
                sql.Append("                            ON (DT_R18_MIX.HAIKI_DAI_CODE + DT_R18_MIX.HAIKI_CHU_CODE + DT_R18_MIX.HAIKI_SHO_CODE) = M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_CD");
                sql.Append("                        WHERE DT_R18_MIX.DELETE_FLG = 0");
                sql.Append("                    ) AS MIX");
                sql.Append("                    ON R18EX.SYSTEM_ID = MIX.SYSTEM_ID");
                sql.Append("            INNER JOIN (SELECT DT_R19_EX.* FROM DT_R19_EX");
                sql.Append("                  INNER JOIN");
                sql.Append("                  (SELECT KANRI_ID,MAX(UPN_ROUTE_NO) UPN_ROUTE_NO FROM DT_R19_EX WHERE DELETE_FLG = 0 GROUP BY KANRI_ID) UPNMAX");
                sql.Append("                  ON DT_R19_EX.KANRI_ID = UPNMAX.KANRI_ID AND DT_R19_EX.UPN_ROUTE_NO = UPNMAX.UPN_ROUTE_NO AND DT_R19_EX.DELETE_FLG = 0");
                sql.Append("                  ) MAXR19EX ON R18EX.SYSTEM_ID = MAXR19EX.SYSTEM_ID AND R18EX.SEQ = MAXR19EX.SEQ");
                sql.Append("            INNER JOIN DT_R19 MAXR19");
                sql.Append("                    ON MAXR19.KANRI_ID = DT_R18.KANRI_ID");
                sql.Append("                   AND MAXR19.SEQ = DT_R18.SEQ");
                sql.Append("                   AND MAXR19EX.UPN_ROUTE_NO = MAXR19.UPN_ROUTE_NO");
                sql.Append("            INNER JOIN DT_R19_EX MINR19EX");
                sql.Append("                    ON R18EX.SYSTEM_ID = MINR19EX.SYSTEM_ID");
                sql.Append("                   AND R18EX.SEQ = MINR19EX.SEQ");
                sql.Append("                   AND MINR19EX.UPN_ROUTE_NO = 1");
                sql.Append("       LEFT OUTER JOIN DT_R13");
                sql.Append("                    ON DT_R13.KANRI_ID = DT_R18.KANRI_ID");
                sql.Append("                   AND DT_R13.SEQ      = DT_R18.SEQ");
                sql.Append("       LEFT OUTER JOIN DT_R13_EX");
                sql.Append("                    ON DT_R13_EX.KANRI_ID = DT_R13.KANRI_ID");
                sql.Append("                   AND DT_R13_EX.DELETE_FLG = 0");
                sql.Append("       LEFT OUTER JOIN M_DENSHI_HAIKI_SHURUI MDHS");
                sql.Append("                    ON MDHS.HAIKI_SHURUI_CD = DT_R18.HAIKI_DAI_CODE + DT_R18.HAIKI_CHU_CODE + DT_R18.HAIKI_SHO_CODE");
                sql.Append("                   AND MDHS.DELETE_FLG      = 0");
                sql.Append("       LEFT OUTER JOIN M_GYOUSHA HST_GYOUSHA");
                sql.Append("                    ON R18EX.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD");
                sql.Append("       WHERE ");
                sql.Append("                   ((DT_R18.FIRST_MANIFEST_FLAG IS NULL OR DT_R18.FIRST_MANIFEST_FLAG = '') OR ISNULL(HST_GYOUSHA.JISHA_KBN, 0) = 0)");
                sql.Append("                   AND (DMT.STATUS_FLAG = 4)");

                sql.Append("               ) FIRST_MANIFEST");
                sql.Append("            ON MANIFEST_RELATION.FIRST_SYSTEM_ID = FIRST_MANIFEST.DETAIL_SYSTEM_ID AND MANIFEST_RELATION.FIRST_HAIKI_KBN_CD = FIRST_MANIFEST.HAIKI_KBN_CD");
                sql.Append("     FULL JOIN (");

                //紙マニ（2次）のデータ
                sql.Append("                SELECT TME_SECOND.SYSTEM_ID,");
                sql.Append("                       TME_SECOND.SEQ,");
                sql.Append("                       NULL AS KANRI_ID,");
                sql.Append("                       NULL AS LATEST_SEQ,");
                sql.Append("                       TME_SECOND.KOUFU_KBN,");
                sql.Append("                       TME_SECOND.KOUFU_DATE,");
                sql.Append("                       TME_SECOND.MANIFEST_ID,");
                sql.Append("                       TME_SECOND.HAIKI_KBN_CD,");
                sql.Append("                       TME_SECOND.HST_GYOUSHA_CD,");
                // 20140617 katen 不具合No.4810 start‏
                //sql.Append("                       TME_SECOND.SBN_JYUTAKUSHA_CD,");
                sql.Append("                       TME_SECOND.SBN_GYOUSHA_CD                               AS SBN_JYUTAKUSHA_CD,");
                // 20140617 katen 不具合No.4810 end‏
                sql.Append("                       TMD_SECOND.LAST_SBN_GYOUSHA_CD,");
                sql.Append("                       TME_SECOND.HST_GENBA_CD,");
                // 20140617 katen 不具合No.4810 start‏
                //sql.Append("                       TME_SECOND.LAST_SBN_GENBA_CD,");
                //sql.Append("                       TME_SECOND.SBN_JYUTAKUSHA_NAME,");
                //sql.Append("                       TME_SECOND.LAST_SBN_GENBA_NAME,");
                sql.Append("                       TMD_SECOND.LAST_SBN_GENBA_CD,");
                sql.Append("                       TME_SECOND.SBN_GYOUSHA_NAME                             AS SBN_JYUTAKUSHA_NAME,");
                sql.Append("                       LAST_SBN_GENBA.GENBA_NAME1 + LAST_SBN_GENBA.GENBA_NAME2 AS LAST_SBN_GENBA_NAME,");
                // 20140617 katen 不具合No.4810 end‏
                sql.Append("                       TMD_SECOND.DETAIL_SYSTEM_ID,");
                sql.Append("                       TMU_SECOND_UPN.UPN_GYOUSHA_CD,");
                sql.Append("                       TMU_SECOND_SBN.UPN_SAKI_GENBA_CD,");
                sql.Append("                       TMU_SECOND_SBN.UPN_SAKI_GENBA_NAME,");
                sql.Append("                       TMU_SECOND_UPN.UPN_GYOUSHA_NAME,");
                sql.Append("                       TMU_SECOND_SBN.UPN_END_DATE,");
                sql.Append("                       TMD_SECOND.SBN_END_DATE,");
                sql.Append("                       TMD_SECOND.HAIKI_SHURUI_CD,");
                sql.Append("                       HAIKI_SECOND.HAIKI_SHURUI_NAME,");
                sql.Append("                       TMD_SECOND.GENNYOU_SUU,");
                sql.Append("                       TMD_SECOND.KANSAN_SUU,");
                sql.Append("                       TMD_SECOND.LAST_SBN_END_DATE");
                sql.Append("                  FROM (SELECT * FROM T_MANIFEST_ENTRY WHERE FIRST_MANIFEST_KBN = 1 AND DELETE_FLG = 0) TME_SECOND ");
                sql.Append("             LEFT JOIN T_MANIFEST_DETAIL TMD_SECOND");
                sql.Append("                    ON TME_SECOND.SYSTEM_ID = TMD_SECOND.SYSTEM_ID");
                sql.Append("                   AND TME_SECOND.SEQ       = TMD_SECOND.SEQ");
                //sql.Append("             LEFT JOIN T_MANIFEST_UPN TMU_SECOND");
                sql.Append("             LEFT JOIN T_MANIFEST_UPN TMU_SECOND_UPN");
                sql.Append("                    ON TME_SECOND.SYSTEM_ID = TMU_SECOND_UPN.SYSTEM_ID");
                sql.Append("                   AND TME_SECOND.SEQ       = TMU_SECOND_UPN.SEQ");
                // 20140616 katen 不具合No.4809 start‏
                sql.Append("                   AND TMU_SECOND_UPN.UPN_ROUTE_NO = 1");
                //sql.Append("             LEFT JOIN (SELECT T_MANIFEST_ENTRY.SYSTEM_ID,");
                //sql.Append("                               T_MANIFEST_ENTRY.SEQ,");
                //sql.Append("                               MAX(T_MANIFEST_UPN.UPN_ROUTE_NO) AS UPN_ROUTE_NO");
                //sql.Append("                          FROM T_MANIFEST_ENTRY");
                //sql.Append("                    INNER JOIN T_MANIFEST_UPN");
                //sql.Append("                            ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_UPN.SYSTEM_ID");
                //sql.Append("                           AND T_MANIFEST_ENTRY.SEQ       = T_MANIFEST_UPN.SEQ");
                //sql.Append("                           AND T_MANIFEST_UPN.UPN_END_DATE IS NOT NULL");
                //sql.Append("                         WHERE T_MANIFEST_ENTRY.DELETE_FLG = 'false'");
                //sql.Append("                      GROUP BY T_MANIFEST_ENTRY.SYSTEM_ID,");
                //sql.Append("                               T_MANIFEST_ENTRY.SEQ) TMU_SEARCH");
                //sql.Append("                    ON TMU_SECOND.SYSTEM_ID    = TMU_SEARCH.SYSTEM_ID");
                //sql.Append("                   AND TMU_SECOND.SEQ          = TMU_SEARCH.SEQ");
                //sql.Append("                   AND TMU_SECOND.UPN_ROUTE_NO = TMU_SEARCH.UPN_ROUTE_NO");
                //sql.Append("             LEFT JOIN T_MANIFEST_UPN MIN_TMU");
                //sql.Append("                    ON TME_SECOND.SYSTEM_ID = MIN_TMU.SYSTEM_ID");
                //sql.Append("                   AND TME_SECOND.SEQ       = MIN_TMU.SEQ");
                //sql.Append("                   AND MIN_TMU.UPN_ROUTE_NO = 1");
                // 20140616 katen 不具合No.4809 end‏
                // 処分事業場を現場入力済みのMAXルート番号で取得する
                sql.Append("             LEFT JOIN (");
                sql.Append("                        SELECT TMU.*");
                sql.Append("                        FROM T_MANIFEST_UPN TMU,");
                sql.Append("                             (SELECT SYSTEM_ID, SEQ, MAX(UPN_ROUTE_NO) AS UPN_ROUTE_NO");
                sql.Append("                              FROM (");
                sql.Append("                                     SELECT SYSTEM_ID, SEQ, UPN_ROUTE_NO");
                sql.Append("                                     FROM T_MANIFEST_UPN");
                sql.Append("                                     WHERE UPN_GYOUSHA_CD IS NOT NULL AND UPN_GYOUSHA_CD <> '' AND UPN_SAKI_GYOUSHA_CD IS NOT NULL AND UPN_SAKI_GYOUSHA_CD <> ''");
                sql.Append("                                   ) TMU_GROUP");
                sql.Append("                              GROUP BY SYSTEM_ID, SEQ");
                sql.Append("                             ) AS TMU_ROUTE_SBN");
                sql.Append("                        WHERE TMU.SYSTEM_ID = TMU_ROUTE_SBN.SYSTEM_ID");
                sql.Append("                              AND TMU.SEQ = TMU_ROUTE_SBN.SEQ");
                sql.Append("                              AND TMU.UPN_ROUTE_NO = TMU_ROUTE_SBN.UPN_ROUTE_NO");
                sql.Append("                       ) TMU_SECOND_SBN");
                sql.Append("                    ON TME_SECOND.SYSTEM_ID = TMU_SECOND_SBN.SYSTEM_ID");
                sql.Append("                   AND TME_SECOND.SEQ       = TMU_SECOND_SBN.SEQ");
                sql.Append("             LEFT JOIN M_HAIKI_SHURUI HAIKI_SECOND");
                sql.Append("                    ON TMD_SECOND.HAIKI_SHURUI_CD = HAIKI_SECOND.HAIKI_SHURUI_CD");
                sql.Append("                   AND TME_SECOND.HAIKI_KBN_CD    = HAIKI_SECOND.HAIKI_KBN_CD");
                // 20140617 katen 不具合No.4810 start‏
                sql.Append("             LEFT JOIN M_GENBA AS LAST_SBN_GENBA");
                sql.Append("                    ON TMD_SECOND.LAST_SBN_GYOUSHA_CD = LAST_SBN_GENBA.GYOUSHA_CD");
                sql.Append("                   AND TMD_SECOND.LAST_SBN_GENBA_CD   = LAST_SBN_GENBA.GENBA_CD");
                sql.Append("                   AND LAST_SBN_GENBA.DELETE_FLG      = 'false'");
                // 20140617 katen 不具合No.4810 end‏

                sql.Append("             UNION ALL");

                //電子マニ（2次）のデータ
                sql.Append("                SELECT R18EX.SYSTEM_ID,");
                sql.Append("                       R18EX.SEQ,");
                sql.Append("                       DMT.KANRI_ID                                                          AS KANRI_ID,");
                sql.Append("                       DMT.LATEST_SEQ                                                        AS LATEST_SEQ,");
                // #1398 電子マニには「交付区分」が存在しないため、一覧画面で指定された交付区分をそのまま使用する
                sql.AppendFormat(
                           "                       ''                                                                    AS KOUFU_KBN,");
                sql.Append("                       CASE ISDATE(DT_R18.HIKIWATASHI_DATE)");
                sql.Append("                       WHEN 0 ");
                sql.Append("                       THEN NULL");
                sql.Append("                       ELSE");
                sql.Append("                       CONVERT(datetime,DT_R18.HIKIWATASHI_DATE,120)");
                sql.Append("                       END                                                                   AS KOUFU_DATE,");
                sql.Append("                       DT_R18.MANIFEST_ID                                                    AS MANIFEST_ID,");
                sql.Append("                       CAST('4' AS SMALLINT )                                                AS HAIKI_KBN_CD,");
                sql.Append("                       R18EX.HST_GYOUSHA_CD                                                  AS HST_GYOUSHA_CD,");
                sql.Append("                       R18EX.SBN_GYOUSHA_CD                                                  AS SBN_JYUTAKUSHA_CD,");
                sql.Append("                       DT_R13_EX.LAST_SBN_GYOUSHA_CD                                         AS LAST_SBN_GYOUSHA_CD,");
                sql.Append("                       R18EX.HST_GENBA_CD                                                    AS HST_GENBA_CD,");
                sql.Append("                       DT_R13_EX.LAST_SBN_GENBA_CD                                           AS LAST_SBN_GENBA_CD,");
                sql.Append("                       DT_R18.SBN_SHA_NAME                                                   AS SBN_JYUTAKUSHA_NAME,");
                sql.Append("                       DT_R13.LAST_SBN_JOU_NAME                                              AS LAST_SBN_GENBA_NAME,");
                sql.Append("                       R18EX.SYSTEM_ID                                                       AS DETAIL_SYSTEM_ID,");
                sql.Append("                       MINR19EX.UPN_GYOUSHA_CD                                               AS UPN_GYOUSHA_CD,");
                sql.Append("                       MAXR19EX.UPNSAKI_GENBA_CD                                             AS UPN_SAKI_GENBA_CD,");
                sql.Append("                       MAXR19.UPNSAKI_JOU_NAME                                               AS UPN_SAKI_GENBA_NAME,");
                sql.Append("                       MAXR19.UPN_SHA_NAME                                                   AS UPN_GYOUSHA_NAME,");
                sql.Append("                       CONVERT(datetime, CASE");
                sql.Append("                                         WHEN MAXR19.UPN_END_DATE = '0' OR MAXR19.UPN_END_DATE = ''");
                sql.Append("                                         THEN NULL");
                sql.Append("                                         ELSE MAXR19.UPN_END_DATE");
                sql.Append("                                         END ,120)                                           AS UPN_END_DATE,");
                sql.Append("                       CONVERT(datetime, CASE");
                sql.Append("                                         WHEN DT_R18.SBN_END_DATE = '0' OR DT_R18.SBN_END_DATE = ''");
                sql.Append("                                         THEN NULL");
                sql.Append("                                         ELSE DT_R18.SBN_END_DATE");
                sql.Append("                                         END ,120)                                           AS SBN_END_DATE,");
                sql.Append("                       DT_R18.HAIKI_DAI_CODE + DT_R18.HAIKI_CHU_CODE + DT_R18.HAIKI_SHO_CODE AS HAIKI_SHURUI_CD,");
                sql.Append("                       DT_R18.HAIKI_SHURUI                                                   AS HAIKI_SHURUI_NAME,");
                sql.Append("                       DT_R18.HAIKI_KAKUTEI_SUU                                              AS GENNYOU_SUU,");
                sql.Append("                       R18EX.KANSAN_SUU                                              AS KANSAN_SUU,");
                sql.Append("                       DT_R13.LAST_SBN_END_DATE");
                sql.Append("                  FROM DT_MF_TOC DMT");
                sql.Append("            INNER JOIN DT_R18");
                sql.Append("                    ON DMT.KANRI_ID   = DT_R18.KANRI_ID");
                sql.Append("                   AND DMT.LATEST_SEQ = DT_R18.SEQ");
                sql.Append("                   AND DT_R18.MANIFEST_ID IS NOT NULL");
                sql.Append("                   AND DT_R18.MANIFEST_ID <> ''");
                sql.Append("            INNER JOIN DT_R18_EX R18EX");
                sql.Append("                    ON R18EX.KANRI_ID   = DT_R18.KANRI_ID");
                sql.Append("                   AND R18EX.DELETE_FLG = 0");
                sql.Append("            INNER JOIN (SELECT DT_R19_EX.* FROM DT_R19_EX");
                sql.Append("                  INNER JOIN");
                sql.Append("                  (SELECT KANRI_ID,MAX(UPN_ROUTE_NO) UPN_ROUTE_NO FROM DT_R19_EX WHERE DELETE_FLG = 0 GROUP BY KANRI_ID) UPNMAX");
                sql.Append("                  ON DT_R19_EX.KANRI_ID = UPNMAX.KANRI_ID AND DT_R19_EX.UPN_ROUTE_NO = UPNMAX.UPN_ROUTE_NO AND DT_R19_EX.DELETE_FLG = 0");
                sql.Append("                  ) MAXR19EX ON R18EX.SYSTEM_ID = MAXR19EX.SYSTEM_ID AND R18EX.SEQ = MAXR19EX.SEQ");
                sql.Append("            INNER JOIN DT_R19 MAXR19");
                sql.Append("                    ON MAXR19.KANRI_ID = DT_R18.KANRI_ID");
                sql.Append("                   AND MAXR19.SEQ = DT_R18.SEQ");
                sql.Append("                   AND MAXR19EX.UPN_ROUTE_NO = MAXR19.UPN_ROUTE_NO");
                sql.Append("            INNER JOIN DT_R19_EX MINR19EX");
                sql.Append("                    ON R18EX.SYSTEM_ID = MINR19EX.SYSTEM_ID");
                sql.Append("                   AND R18EX.SEQ = MINR19EX.SEQ");
                sql.Append("                   AND MINR19EX.UPN_ROUTE_NO = 1");
                sql.Append("       LEFT OUTER JOIN DT_R13");
                sql.Append("                    ON DT_R13.KANRI_ID = DT_R18.KANRI_ID");
                sql.Append("                   AND DT_R13.SEQ      = DT_R18.SEQ");
                sql.Append("       LEFT OUTER JOIN DT_R13_EX");
                sql.Append("                    ON DT_R13_EX.KANRI_ID = DT_R13.KANRI_ID");
                sql.Append("                   AND DT_R13_EX.DELETE_FLG = 0");
                sql.Append("       LEFT OUTER JOIN M_DENSHI_HAIKI_SHURUI MDHS");
                sql.Append("                    ON MDHS.HAIKI_SHURUI_CD = DT_R18.HAIKI_DAI_CODE + DT_R18.HAIKI_CHU_CODE + DT_R18.HAIKI_SHO_CODE");
                sql.Append("                   AND MDHS.DELETE_FLG      = 0");
                sql.Append("       LEFT OUTER JOIN M_GYOUSHA HST_GYOUSHA");
                sql.Append("                    ON R18EX.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD");
                sql.Append("       WHERE ");
                sql.Append("                   (DT_R18.FIRST_MANIFEST_FLAG IS NOT NULL AND DT_R18.FIRST_MANIFEST_FLAG <> '' AND ISNULL(HST_GYOUSHA.JISHA_KBN, 0) = 1)");
                sql.Append("                   AND (DMT.STATUS_FLAG = 4)");

                sql.Append("               ) SECOND_MANIFEST");
                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
                //sql.Append("            ON MANIFEST_RELATION.NEXT_SYSTEM_ID   = SECOND_MANIFEST.SYSTEM_ID AND MANIFEST_RELATION.NEXT_HAIKI_KBN_CD = SECOND_MANIFEST.HAIKI_KBN_CD");
                sql.Append("            ON MANIFEST_RELATION.NEXT_SYSTEM_ID   = SECOND_MANIFEST.DETAIL_SYSTEM_ID AND MANIFEST_RELATION.NEXT_HAIKI_KBN_CD = SECOND_MANIFEST.HAIKI_KBN_CD");
                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end
                #endregion

                #region WHERE句
                var sqlWhere = new StringBuilder();
                //抽出対象区分によって、検索条件の限定テーブルを設定する
                string tableName = this.form.txtNum_ChushutuTaishouKbn.Text == "1" ? "FIRST_MANIFEST" : "SECOND_MANIFEST";

                if (!String.IsNullOrEmpty(this.form.cantxt_KohuNo.Text))
                {
                    //交付番号が空じゃない場合、交付番号の検索条件を追加する
                    sqlWhere.AppendFormat("           AND {0}.MANIFEST_ID  = '{1}'", new object[] { tableName, this.form.cantxt_KohuNo.Text });
                }

                //マニフェスト種類
                switch (this.form.txtNum_ManifestShurui.Text)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                        //マニフェスト種類が「1.直行」「2.建廃」「3.積替」「4.電子」を選んだ場合、廃棄区分の検索条件を追加する
                        //そして「5.すべて」を選んだ場合、廃棄区分の検索条件を追加しない
                        sqlWhere.AppendFormat("           AND {0}.HAIKI_KBN_CD = {1}", new object[] { tableName, this.form.txtNum_ManifestShurui.Text });
                        break;
                }
                //抽出日付区分
                switch (this.form.txtNum_ChushutuHiduke.Text)
                {
                    case "1":
                        //交付年月日
                        sqlWhere.AppendFormat("           AND {0}.KOUFU_DATE BETWEEN '{1}' AND '{2}'", new object[] { tableName, this.form.HIDUKE_FROM.Value, this.form.HIDUKE_TO.Value });
                        break;
                    case "2":
                        //運搬終了日
                        sqlWhere.AppendFormat("           AND {0}.UPN_END_DATE BETWEEN '{1}' AND '{2}'", new object[] { tableName, this.form.HIDUKE_FROM.Value, this.form.HIDUKE_TO.Value });
                        break;
                    case "3":
                        //処分終了日
                        sqlWhere.AppendFormat("           AND {0}.SBN_END_DATE BETWEEN '{1}' AND '{2}'", new object[] { tableName, this.form.HIDUKE_FROM.Value, this.form.HIDUKE_TO.Value });
                        break;
                    case "4":
                        //最終処分終了日
                        sqlWhere.AppendFormat("           AND {0}.LAST_SBN_END_DATE BETWEEN '{1}' AND '{2}'", new object[] { tableName, this.form.HIDUKE_FROM.Value, this.form.HIDUKE_TO.Value });
                        break;
                }
                //業者CDが空じゃない場合
                if (!string.IsNullOrEmpty(this.form.cantxt_GyousyaCd.Text))
                {
                    //抽出業者区分
                    switch (this.form.txtNum_ChushutuGyosha.Text)
                    {
                        case "1":
                            //排出事業者
                            sqlWhere.AppendFormat("           AND {0}.HST_GYOUSHA_CD      = '{1}'", new object[] { tableName, this.form.cantxt_GyousyaCd.Text.PadLeft(6, '0') });
                            break;
                        case "2":
                            //運搬受託者
                            sqlWhere.AppendFormat("           AND {0}.UPN_GYOUSHA_CD      = '{1}'", new object[] { tableName, this.form.cantxt_GyousyaCd.Text.PadLeft(6, '0') });
                            break;
                        case "3":
                            //処分受託者
                            sqlWhere.AppendFormat("           AND {0}.SBN_JYUTAKUSHA_CD   = '{1}'", new object[] { tableName, this.form.cantxt_GyousyaCd.Text.PadLeft(6, '0') });
                            break;
                        case "4":
                            //最終処分場所
                            sqlWhere.AppendFormat("           AND {0}.LAST_SBN_GYOUSHA_CD = '{1}'", new object[] { tableName, this.form.cantxt_GyousyaCd.Text.PadLeft(6, '0') });
                            break;
                    }
                }
                //現場CDが空じゃない場合
                if (!string.IsNullOrEmpty(this.form.cantxt_GenbaCd.Text))
                {
                    //抽出業者区分
                    switch (this.form.txtNum_ChushutuGyosha.Text)
                    {
                        case "1":
                            //排出事業者
                            sqlWhere.AppendFormat("           AND {0}.HST_GENBA_CD      = '{1}'", new object[] { tableName, this.form.cantxt_GenbaCd.Text.PadLeft(6, '0') });
                            break;
                        case "2":
                        case "3":
                            //運搬受託者と処分受託者
                            sqlWhere.AppendFormat("           AND {0}.UPN_SAKI_GENBA_CD = '{1}'", new object[] { tableName, this.form.cantxt_GenbaCd.Text.PadLeft(6, '0') });
                            break;
                        case "4":
                            //最終処分場所
                            sqlWhere.AppendFormat("           AND {0}.LAST_SBN_GENBA_CD = '{1}'", new object[] { tableName, this.form.cantxt_GenbaCd.Text.PadLeft(6, '0') });
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(sqlWhere.ToString()))
                {
                    //検索条件がある場合、WHEREを追加する
                    sql.Append("         WHERE ");
                    //上記全ての条件が「AND」を付けているので、一番目の「AND」を削除する
                    sql.Append(sqlWhere.ToString().Substring(sqlWhere.ToString().IndexOf("AND") + "AND".Length));
                }
                #endregion

                sql.Append("                ) SEARCH_DATA");
                //紐付状況の検索条件を追加する
                sql.Append(" WHERE 紐付状況 IS NOT NULL");
                switch (this.form.txtNum_HimodukeJyoukyou.Text)
                {
                    case "1":
                        sql.Append("   AND 紐付状況 = '済'");
                        break;
                    case "2":
                        sql.Append("   AND 紐付状況 = '未'");
                        break;
                }

                #region ORDERBY句
                sql.Append(" ORDER BY ");
                sql.AppendFormat(" \"{0}\", \"{1}\"", new object[] { this.KOUFU_DATE_IJI, this.KOUFU_DATE_NIJI });
                #endregion

                this.createSql = sql.ToString();
                //検索を行う
                this.searchResult = daoIchiran.getdateforstringsql(createSql);
                ret_cnt = this.searchResult.Rows.Count;

                //初期化
                this.form.customDataGridView1.DataSource = null;
                this.form.customDataGridView1.Rows.Clear();
                this.form.customDataGridView1.Columns.Clear();

                this.form.Table = this.searchResult;
                //検索結果を画面に表示する
                this.form.ShowData();

                //読込データ件数
                if (this.form.customDataGridView1 != null)
                {
                    this.headForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                }
                else
                {
                    this.headForm.ReadDataNumber.Text = "0";
                }

                //フォーカス初期化
                if (this.form.customDataGridView1.Columns.Count > 0 && this.form.customDataGridView1.Rows.Count > 0)
                {
                    this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1[0, 0];
                }

                // 20140605 katen 不具合No.4689 start‏
                if (this.form.customDataGridView1.Columns.Count > 0)
                {
                    Color color_iji = Color.FromArgb(0, 105, 51);
                    Color color_niji = Color.FromArgb(0, 51, 160);
                    for (int i = 0; i < this.form.customDataGridView1.ColumnCount; i++)
                    {
                        //9列目までは一次、後は二次
                        if (i <= 9)
                        {
                            this.form.customDataGridView1.Columns[i].HeaderCell.Style.BackColor = color_iji;
                        }
                        else
                        {
                            this.form.customDataGridView1.Columns[i].HeaderCell.Style.BackColor = color_niji;
                        }
                    }
                }

                // 20140605 katen 不具合No.4689 end‏
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret_cnt = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret_cnt);
            }
            //取得件数
            return ret_cnt;
        }
        #endregion


        #region 検索Ver02
        /// <summary>
        /// 検索
        ///   Search()では検索の処理時間がかかる
        ///   SearchVer02()では、「日付」を「1.交付年月日」とした場合のみ処理時間が短縮されるように改良
        /// </summary>
        public int SearchVer02()
        {
            //#region 表示を初期化
            //    this.headForm.ReadDataNumber.Text = "";
            //    this.headForm.ReadDataNumber.Refresh();
            //    //DataGridView
            //    this.form.customDataGridView1.DataSource = null;
            //    this.form.customDataGridView1.Rows.Clear();
            //    this.form.customDataGridView1.Columns.Clear();
            //    this.form.customDataGridView1.Refresh();
            //    this.form.Refresh();
            //    Application.DoEvents();
            //#endregion

            int ret_cnt = 0;
            try
            {
                LogUtility.DebugMethodStart();

                this.searchResult = new DataTable();
                sql = new StringBuilder();

                #region SELECT句
                //sql.AppendLine("");
                //sql.Append("WITH DT_R18_TMP AS (\n");
                //sql.Append("    SELECT *\n");
                //sql.Append("    FROM   DT_R18\n");
                ////sql.Append("    WHERE  HIKIWATASHI_DATE BETWEEN '2019/04/01 0:00:00' AND '2019/04/01 0:00:00'\n");
                //sql.AppendFormat("    WHERE  HIKIWATASHI_DATE BETWEEN '{0}' AND '{1}'\n", this.form.HIDUKE_FROM.Value, this.form.HIDUKE_TO.Value);
                //sql.Append(")\n");

                sql.AppendLine("");
                sql.AppendLine("WITH DT_R18_TMP AS (");
                sql.AppendLine("    SELECT DT_R18.*");
                sql.AppendLine("    FROM DT_MF_TOC INNER JOIN DT_R18 ON DT_MF_TOC.KANRI_ID = DT_R18.KANRI_ID AND DT_MF_TOC.LATEST_SEQ = DT_R18.SEQ ");
                //sql.AppendLine("    WHERE  HIKIWATASHI_DATE BETWEEN '2019/04/01 0:00:00' AND '2019/04/01 0:00:00'\n");
                //日付はFromToどちらも入力必須である前提
                //string HidukeFrom = ((DateTime)this.form.HIDUKE_FROM.Value).ToString("yyyyMMdd");
                //string HidukeTo = ((DateTime)this.form.HIDUKE_TO.Value).ToString("yyyyMMdd");
                //sql.AppendFormat("    WHERE  HIKIWATASHI_DATE BETWEEN '{0}' AND '{1}'\n", HidukeFrom, HidukeTo);
                sql.AppendLine(")");

                sql.Append("SELECT *");
                sql.Append("  FROM (SELECT DISTINCT");
                sql.Append("       CASE");
                sql.Append("       WHEN MANIFEST_RELATION.NEXT_SYSTEM_ID IS NOT NULL");
                sql.Append("       THEN CASE");
                sql.Append("            WHEN FIRST_MANIFEST.SYSTEM_ID IS NOT NULL AND SECOND_MANIFEST.SYSTEM_ID IS NOT NULL");
                sql.Append("            THEN '済'");
                sql.Append("            WHEN FIRST_MANIFEST.SYSTEM_ID IS NULL AND SECOND_MANIFEST.SYSTEM_ID IS NOT NULL");
                // 20140606 katen 不具合No.4692 start‏
                sql.Append("            THEN '紐付不整合'");
                sql.Append("            WHEN FIRST_MANIFEST.SYSTEM_ID IS NOT NULL AND SECOND_MANIFEST.SYSTEM_ID IS NULL");
                sql.Append("            THEN '紐付不整合'");
                sql.Append("            END");
                sql.Append("       ELSE CASE");
                sql.Append("            WHEN FIRST_MANIFEST.HAIKI_KBN_CD != 4");
                sql.Append("                THEN CASE");
                sql.Append("                    WHEN FIRST_MANIFEST.SYSTEM_ID IS NOT NULL AND FIRST_MANIFEST.LAST_SBN_END_DATE IS NOT NULL");
                sql.Append("                    THEN '2次なし'");
                sql.Append("                    ELSE '未'");
                sql.Append("                END");
                sql.Append("            ELSE CASE");
                sql.Append("                WHEN FIRST_MANIFEST.SBN_ENDREP_KBN = 2");
                sql.Append("                THEN '2次なし'");
                sql.Append("                ELSE '未'");
                sql.Append("                END");
                sql.Append("            END");
                // 20140606 katen 不具合No.4692 end‏
                sql.AppendFormat(" END                                 AS \"{0}\",", this.HIMOTUKI_JYOUKYOU);           //紐付状況
                sql.AppendFormat(" FIRST_MANIFEST.MANIFEST_ID          AS \"{0}\",", this.KOUFU_NO_IJI);                //交付番号（1次）
                sql.AppendFormat(" FIRST_MANIFEST.KOUFU_DATE           AS \"{0}\",", this.KOUFU_DATE_IJI);              //交付日付（1次）
                sql.AppendFormat(" LTRIM(FIRST_MANIFEST.HST_GYOUSHA_NAME) AS \"{0}\",", this.HAISHU_JIGYOSHA_IJI);      //排出事業者（1次）
                sql.AppendFormat(" LTRIM(FIRST_MANIFEST.HST_GENBA_NAME)   AS \"{0}\",", this.HAISHU_JIGYOBA_IJI);       //排出事業場（1次）
                sql.AppendFormat(" FIRST_MANIFEST.UPN_GYOUSHA_NAME     AS \"{0}\",", this.UNPAN_JUTAKUSHA_IJI);         //運搬受託者（1次）
                sql.AppendFormat(" FIRST_MANIFEST.HAIKI_SHURUI_NAME    AS \"{0}\",", this.HAIKIBUTU_SHURUI_IJI);        //廃棄物種類（1次）
                sql.AppendFormat(" FIRST_MANIFEST.GENNYOU_SUU          AS \"{0}\",", this.GENNYOU_SU_IJI);              //減容後数量（1次）
                sql.AppendFormat(" FIRST_MANIFEST.UPN_END_DATE         AS \"{0}\",", this.UNPAN_END_DATE_IJI);          //運搬終了日（1次）
                sql.AppendFormat(" FIRST_MANIFEST.SBN_END_DATE         AS \"{0}\",", this.SHOBUN_END_DATE_IJI);         //処分終了日（1次）
                sql.AppendFormat(" SECOND_MANIFEST.MANIFEST_ID         AS \"{0}\",", this.KOUFU_NO_NIJI);               //交付番号（2次）
                sql.AppendFormat(" SECOND_MANIFEST.KOUFU_DATE          AS \"{0}\",", this.KOUFU_DATE_NIJI);             //交付日付（2次）
                sql.AppendFormat(" SECOND_MANIFEST.UPN_GYOUSHA_NAME    AS \"{0}\",", this.UNPAN_JUTAKUSHA_NIJI);        //運搬受託者（2次）
                sql.AppendFormat(" SECOND_MANIFEST.SBN_JYUTAKUSHA_NAME AS \"{0}\",", this.SHOBUN_JUTAKUSHA_NIJI);       //処分受託者（2次）
                sql.AppendFormat(" SECOND_MANIFEST.UPN_SAKI_GENBA_NAME AS \"{0}\",", this.SHOBUN_JIGYOBA_NIJI);         //処分事業場（2次）
                sql.AppendFormat(" SECOND_MANIFEST.LAST_SBN_GENBA_NAME AS \"{0}\",", this.LAST_SHOBUN_GENBA_NIJI);      //最終処分場所
                sql.AppendFormat(" SECOND_MANIFEST.HAIKI_SHURUI_NAME   AS \"{0}\",", this.HAIKIBUTU_SHURUI_NIJI);       //廃棄物種類（2次）
                sql.AppendFormat(" SECOND_MANIFEST.KANSAN_SUU          AS \"{0}\",", this.KANSAN_SU_NIJI);              //換算後数量（2次）
                sql.AppendFormat(" SECOND_MANIFEST.UPN_END_DATE        AS \"{0}\",", this.UNPAN_END_DATE_NIJI);         //運搬終了日（2次）
                sql.AppendFormat(" SECOND_MANIFEST.SBN_END_DATE        AS \"{0}\",", this.SHOBUN_END_DATE_NIJI);        //処分終了日（2次）
                sql.Append("       CASE");
                sql.Append("       WHEN MANIFEST_RELATION.NEXT_SYSTEM_ID IS NOT NULL");
                sql.Append("       THEN CASE");
                sql.Append("            WHEN FIRST_MANIFEST.SYSTEM_ID IS NOT NULL AND SECOND_MANIFEST.SYSTEM_ID IS NOT NULL");
                sql.Append("            THEN SECOND_MANIFEST.LAST_SBN_END_DATE");
                sql.Append("            WHEN FIRST_MANIFEST.SYSTEM_ID IS NULL AND SECOND_MANIFEST.SYSTEM_ID IS NOT NULL");
                sql.Append("            THEN SECOND_MANIFEST.LAST_SBN_END_DATE");
                sql.Append("            WHEN FIRST_MANIFEST.SYSTEM_ID IS NOT NULL AND SECOND_MANIFEST.SYSTEM_ID IS NULL");
                sql.Append("            THEN FIRST_MANIFEST.LAST_SBN_END_DATE");
                sql.Append("            END");
                sql.Append("       ELSE CASE");
                sql.Append("            WHEN FIRST_MANIFEST.SYSTEM_ID IS NULL AND SECOND_MANIFEST.SYSTEM_ID IS NOT NULL");
                sql.Append("            THEN SECOND_MANIFEST.LAST_SBN_END_DATE");
                sql.Append("            WHEN FIRST_MANIFEST.SYSTEM_ID IS NOT NULL AND SECOND_MANIFEST.SYSTEM_ID IS NULL");
                sql.Append("            THEN FIRST_MANIFEST.LAST_SBN_END_DATE");
                sql.Append("            END");
                sql.AppendFormat(" END                                 AS \"{0}\",", this.LAST_SHOBUN_END_DATE);        //最終処分終了日
                sql.AppendFormat(" FIRST_MANIFEST.HAIKI_KBN_CD         AS \"{0}\",", this.HIDDEN_HAIKI_KBN_CD_IJI);     //廃棄区分（1次）（非表示）
                sql.AppendFormat(" FIRST_MANIFEST.SYSTEM_ID            AS \"{0}\",", this.HIDDEN_SYSTEM_ID_IJI);        //システムID（1次）（非表示）
                sql.AppendFormat(" FIRST_MANIFEST.SEQ                  AS \"{0}\",", this.HIDDEN_SEQ_IJI);              //SEQ（1次）（非表示）
                sql.AppendFormat(" FIRST_MANIFEST.KANRI_ID             AS \"{0}\",", this.HIDDEN_KANRI_ID_IJI);         //管理ID（1次）（非表示）
                sql.AppendFormat(" FIRST_MANIFEST.LATEST_SEQ           AS \"{0}\",", this.HIDDEN_LATEST_SEQ_IJI);       //最終SEQ（1次）（非表示）
                sql.AppendFormat(" SECOND_MANIFEST.HAIKI_KBN_CD        AS \"{0}\",", this.HIDDEN_HAIKI_KBN_CD_NIJI);    //廃棄区分（2次）（非表示）
                sql.AppendFormat(" SECOND_MANIFEST.SYSTEM_ID           AS \"{0}\",", this.HIDDEN_SYSTEM_ID_NIJI);       //システムID（2次）（非表示）
                sql.AppendFormat(" SECOND_MANIFEST.SEQ                 AS \"{0}\",", this.HIDDEN_SEQ_NIJI);             //SEQ（2次）（非表示）
                sql.AppendFormat(" SECOND_MANIFEST.KANRI_ID            AS \"{0}\",", this.HIDDEN_KANRI_ID_NIJI);        //管理ID（2次）（非表示）
                sql.AppendFormat(" SECOND_MANIFEST.LATEST_SEQ          AS \"{0}\" ", this.HIDDEN_LATEST_SEQ_NIJI);      //最終SEQ（2次）（非表示）
                sql.Append("");
                String MOutputPatternSelect = this.selectQuery;
                if (!String.IsNullOrEmpty(MOutputPatternSelect))
                {
                    sql.Append(", ");
                    sql.Append(MOutputPatternSelect);
                }
                #endregion

                #region FROM句
                //sql.Append("          FROM (SELECT * FROM T_MANIFEST_RELATION WHERE DELETE_FLG = 0) MANIFEST_RELATION");
                sql.AppendLine("");
                sql.Append("FROM (SELECT * FROM T_MANIFEST_RELATION WHERE DELETE_FLG = 0) MANIFEST_RELATION");

                sql.Append("     FULL JOIN (");

                //紙マニ（1次）のデータ
                sql.Append("                SELECT TME_FIRST.SYSTEM_ID,");
                sql.Append("                       TME_FIRST.SEQ,");
                sql.Append("                       NULL AS KANRI_ID,");
                sql.Append("                       NULL AS LATEST_SEQ,");
                sql.Append("                       TME_FIRST.KOUFU_KBN,");
                sql.Append("                       TME_FIRST.KOUFU_DATE,");
                sql.Append("                       TME_FIRST.MANIFEST_ID,");
                sql.Append("                       TME_FIRST.HAIKI_KBN_CD,");
                sql.Append("                       TME_FIRST.HST_GYOUSHA_CD,");
                sql.Append("                       TME_FIRST.SBN_GYOUSHA_CD AS SBN_JYUTAKUSHA_CD,");
                sql.Append("                       TMD_FIRST.LAST_SBN_GYOUSHA_CD,");
                sql.Append("                       TME_FIRST.HST_GENBA_CD,");
                sql.Append("                       TMD_FIRST.LAST_SBN_GENBA_CD,");
                sql.Append("                       RTRIM(SUBSTRING(ISNULL(TME_FIRST.HST_GYOUSHA_NAME, ''),1, 40)) + SUBSTRING(ISNULL(TME_FIRST.HST_GYOUSHA_NAME, ''),41, 40) AS HST_GYOUSHA_NAME,");
                sql.Append("                       RTRIM(SUBSTRING(ISNULL(TME_FIRST.HST_GENBA_NAME, ''),1, 40)) + SUBSTRING(ISNULL(TME_FIRST.HST_GENBA_NAME, ''),41, 40) AS HST_GENBA_NAME,");
                sql.Append("                       TMD_FIRST.DETAIL_SYSTEM_ID,");
                sql.Append("                       TMU_FIRST_UPN.UPN_GYOUSHA_CD,");
                sql.Append("                       TMU_FIRST_SBN.UPN_SAKI_GENBA_CD,");
                sql.Append("                       TMU_FIRST_SBN.UPN_SAKI_GENBA_NAME,");
                sql.Append("                       TMU_FIRST_UPN.UPN_GYOUSHA_NAME,");
                sql.Append("                       TMU_FIRST_SBN.UPN_END_DATE,");
                sql.Append("                       TMD_FIRST.SBN_END_DATE,");
                sql.Append("                       TMD_FIRST.HAIKI_SHURUI_CD,");
                sql.Append("                       HAIKI_FIRST.HAIKI_SHURUI_NAME,");
                sql.Append("                       TMD_FIRST.GENNYOU_SUU,");
                sql.Append("                       TMD_FIRST.KANSAN_SUU,");
                sql.Append("                       TMD_FIRST.LAST_SBN_END_DATE,");
                sql.Append("                       NULL AS SBN_ENDREP_KBN");
                sql.Append("                  FROM (SELECT * FROM T_MANIFEST_ENTRY WHERE FIRST_MANIFEST_KBN = 0 AND DELETE_FLG = 0) TME_FIRST");
                sql.Append("             LEFT JOIN T_MANIFEST_DETAIL TMD_FIRST");
                sql.Append("                    ON TME_FIRST.SYSTEM_ID = TMD_FIRST.SYSTEM_ID");
                sql.Append("                   AND TME_FIRST.SEQ       = TMD_FIRST.SEQ");
                //sql.Append("             LEFT JOIN T_MANIFEST_UPN TMU_FIRST");
                sql.Append("             LEFT JOIN T_MANIFEST_UPN TMU_FIRST_UPN");
                sql.Append("                    ON TME_FIRST.SYSTEM_ID = TMU_FIRST_UPN.SYSTEM_ID");
                sql.Append("                   AND TME_FIRST.SEQ       = TMU_FIRST_UPN.SEQ");
                // 20140616 katen 不具合No.4809 start‏
                sql.Append("                   AND TMU_FIRST_UPN.UPN_ROUTE_NO = 1");
                //sql.Append("             LEFT JOIN (SELECT T_MANIFEST_ENTRY.SYSTEM_ID,");
                //sql.Append("                               T_MANIFEST_ENTRY.SEQ,");
                //sql.Append("                               MAX(T_MANIFEST_UPN.UPN_ROUTE_NO) AS UPN_ROUTE_NO");
                //sql.Append("                          FROM T_MANIFEST_ENTRY");
                //sql.Append("                    INNER JOIN T_MANIFEST_UPN");
                //sql.Append("                            ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_UPN.SYSTEM_ID");
                //sql.Append("                           AND T_MANIFEST_ENTRY.SEQ       = T_MANIFEST_UPN.SEQ");
                //sql.Append("                           AND T_MANIFEST_UPN.UPN_END_DATE IS NOT NULL");
                //sql.Append("                         WHERE T_MANIFEST_ENTRY.DELETE_FLG = 'false'");
                //sql.Append("                      GROUP BY T_MANIFEST_ENTRY.SYSTEM_ID,");
                //sql.Append("                               T_MANIFEST_ENTRY.SEQ) TMU_SEARCH");
                //sql.Append("                    ON TMU_FIRST.SYSTEM_ID    = TMU_SEARCH.SYSTEM_ID");
                //sql.Append("                   AND TMU_FIRST.SEQ          = TMU_SEARCH.SEQ");
                //sql.Append("                   AND TMU_FIRST.UPN_ROUTE_NO = TMU_SEARCH.UPN_ROUTE_NO");
                //sql.Append("             LEFT JOIN T_MANIFEST_UPN MIN_TMU");
                //sql.Append("                    ON TME_FIRST.SYSTEM_ID  = MIN_TMU.SYSTEM_ID");
                //sql.Append("                   AND TME_FIRST.SEQ        = MIN_TMU.SEQ");
                //sql.Append("                   AND MIN_TMU.UPN_ROUTE_NO = 1");
                // 20140616 katen 不具合No.4809 end‏
                // 処分事業場を現場入力済みのMAXルート番号で取得する
                sql.Append("             LEFT JOIN (");
                sql.Append("                        SELECT TMU.*");
                sql.Append("                        FROM T_MANIFEST_UPN TMU,");
                sql.Append("                             (SELECT SYSTEM_ID, SEQ, MAX(UPN_ROUTE_NO) AS UPN_ROUTE_NO");
                sql.Append("                              FROM (");
                sql.Append("                                     SELECT SYSTEM_ID, SEQ, UPN_ROUTE_NO");
                sql.Append("                                     FROM T_MANIFEST_UPN");
                sql.Append("                                     WHERE UPN_GYOUSHA_CD IS NOT NULL AND UPN_GYOUSHA_CD <> '' AND UPN_SAKI_GYOUSHA_CD IS NOT NULL AND UPN_SAKI_GYOUSHA_CD <> ''");
                sql.Append("                                   ) TMU_GROUP");
                sql.Append("                              GROUP BY SYSTEM_ID, SEQ");
                sql.Append("                             ) AS TMU_ROUTE_SBN");
                sql.Append("                        WHERE TMU.SYSTEM_ID = TMU_ROUTE_SBN.SYSTEM_ID");
                sql.Append("                              AND TMU.SEQ = TMU_ROUTE_SBN.SEQ");
                sql.Append("                              AND TMU.UPN_ROUTE_NO = TMU_ROUTE_SBN.UPN_ROUTE_NO");
                sql.Append("                       ) TMU_FIRST_SBN");
                sql.Append("                    ON TME_FIRST.SYSTEM_ID = TMU_FIRST_SBN.SYSTEM_ID");
                sql.Append("                   AND TME_FIRST.SEQ       = TMU_FIRST_SBN.SEQ");
                sql.Append("             LEFT JOIN M_HAIKI_SHURUI HAIKI_FIRST");
                sql.Append("                    ON TMD_FIRST.HAIKI_SHURUI_CD = HAIKI_FIRST.HAIKI_SHURUI_CD");
                sql.Append("                   AND TME_FIRST.HAIKI_KBN_CD    = HAIKI_FIRST.HAIKI_KBN_CD");

                sql.Append("             UNION ALL");

                //電子マニ（1次）のデータ
                sql.Append("                SELECT (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.DETAIL_SYSTEM_ID ELSE R18EX.SYSTEM_ID END) AS SYSTEM_ID,");
                sql.Append("                       (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.SEQ ELSE R18EX.SEQ END) AS SEQ,");
                sql.Append("                       DMT.KANRI_ID                                                          AS KANRI_ID,");
                sql.Append("                       DMT.LATEST_SEQ                                                        AS LATEST_SEQ,");
                // #1398 電子マニには「交付区分」が存在しないため、一覧画面で指定された交付区分をそのまま使用する
                sql.AppendFormat(
                           "                       ''                                                                   AS KOUFU_KBN,");
                sql.Append("                       CASE ISDATE(DT_R18_TMP.HIKIWATASHI_DATE) ");
                sql.Append("                       WHEN 0 ");
                sql.Append("                       THEN NULL");
                sql.Append("                       ELSE");
                sql.Append("                       CONVERT(datetime,DT_R18_TMP.HIKIWATASHI_DATE,120) ");
                sql.Append("                       END                                                                   AS KOUFU_DATE,");
                sql.Append("                       DT_R18_TMP.MANIFEST_ID                                                    AS MANIFEST_ID,");
                sql.Append("                       CAST('4' AS SMALLINT )                                                AS HAIKI_KBN_CD,");
                sql.Append("                       R18EX.HST_GYOUSHA_CD                                                  AS HST_GYOUSHA_CD,");
                sql.Append("                       R18EX.SBN_GYOUSHA_CD                                                  AS SBN_JYUTAKUSHA_CD,");
                sql.Append("                       DT_R13_EX.LAST_SBN_GYOUSHA_CD                                         AS LAST_SBN_GYOUSHA_CD,");
                sql.Append("                       R18EX.HST_GENBA_CD                                                    AS HST_GENBA_CD,");
                sql.Append("                       DT_R13_EX.LAST_SBN_GENBA_CD                                           AS LAST_SBN_GENBA_CD,");
                sql.Append("                       DT_R18_TMP.HST_SHA_NAME                                                   AS HST_GYOUSHA_NAME,");
                sql.Append("                       DT_R18_TMP.HST_JOU_NAME                                                   AS HST_GENBA_NAME,");
                sql.Append("                       (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.DETAIL_SYSTEM_ID ELSE R18EX.SYSTEM_ID END) AS DETAIL_SYSTEM_ID,");
                sql.Append("                       MINR19EX.UPN_GYOUSHA_CD                                               AS UPN_GYOUSHA_CD,");
                sql.Append("                       MAXR19EX.UPNSAKI_GENBA_CD                                             AS UPN_SAKI_GENBA_CD,");
                sql.Append("                       MAXR19.UPNSAKI_JOU_NAME                                               AS UPN_SAKI_GENBA_NAME,");
                sql.Append("                       MAXR19.UPN_SHA_NAME                                                   AS UPN_GYOUSHA_NAME,");
                sql.Append("                       CONVERT(datetime, CASE");
                sql.Append("                                         WHEN MAXR19.UPN_END_DATE = '0' OR MAXR19.UPN_END_DATE = ''");
                sql.Append("                                         THEN NULL");
                sql.Append("                                         ELSE MAXR19.UPN_END_DATE");
                sql.Append("                                         END ,120)                                           AS UPN_END_DATE,");
                sql.Append("                       CONVERT(datetime, CASE");
                sql.Append("                                         WHEN DT_R18_TMP.SBN_END_DATE = '0' OR DT_R18_TMP.SBN_END_DATE = ''");
                sql.Append("                                         THEN NULL");
                sql.Append("                                         ELSE DT_R18_TMP.SBN_END_DATE");
                sql.Append("                                         END ,120)                                           AS SBN_END_DATE,");
                sql.Append("                       (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.HAIKI_DAI_CODE + MIX.HAIKI_CHU_CODE + MIX.HAIKI_SHO_CODE");
                sql.Append("                             ELSE DT_R18_TMP.HAIKI_DAI_CODE + DT_R18_TMP.HAIKI_CHU_CODE + DT_R18_TMP.HAIKI_SHO_CODE END) AS HAIKI_SHURUI_CD,");
                sql.Append("                       (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.HAIKI_SHURUI_NAME ELSE DT_R18_TMP.HAIKI_SHURUI END) AS HAIKI_SHURUI_NAME,");
                sql.Append("                       (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.GENNYOU_SUU ELSE R18EX.GENNYOU_SUU END) AS GENNYOU_SUU,");
                sql.Append("                       (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.KANSAN_SUU");
                sql.Append("                       ELSE (SELECT CASE WHEN TBL.KANSANSHIKI=0 THEN DT_R18_TMP.HAIKI_SUU*TBL.KANSANCHI");
                sql.Append("                                    ELSE DT_R18_TMP.HAIKI_SUU/TBL.KANSANCHI");
                sql.Append("                                    END");
                sql.Append("                          FROM (SELECT KANSANSHIKI,");
                sql.Append("                                       KANSANCHI");
                sql.Append("                                  FROM M_MANIFEST_KANSAN");
                sql.Append("                                 WHERE DELETE_FLG           = 0 ");
                sql.Append("                                   AND HOUKOKUSHO_BUNRUI_CD = MDHS.HOUKOKUSHO_BUNRUI_CD");
                sql.Append("                                   AND HAIKI_NAME_CD        = (SELECT TOP 1 HAIKI_NAME_CD");
                sql.Append("                                                                 FROM M_DENSHI_HAIKI_NAME");
                sql.Append("                                                                WHERE DELETE_FLG = 0");
                sql.Append("                                                                  AND HAIKI_NAME = DT_R18_TMP.HAIKI_NAME");
                sql.Append("                                                               )");
                sql.Append("                                   AND UNIT_CD              = DT_R18_TMP.HAIKI_UNIT_CODE");
                sql.Append("                                   AND NISUGATA_CD          = DT_R18_TMP.NISUGATA_CODE");
                sql.Append("                                ) TBL");
                sql.Append("                       ) END)                                                                     AS KANSAN_SUU,");
                sql.Append("                       (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL AND MIX.SBN_ENDREP_KBN = 2 THEN MIX.LAST_SBN_END_DATE ELSE DT_R13.LAST_SBN_END_DATE END) AS LAST_SBN_END_DATE,");
                sql.Append("                       (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.SBN_ENDREP_KBN ELSE DT_R18_TMP.SBN_ENDREP_KBN END) AS SBN_ENDREP_KBN");
                sql.Append("                  FROM DT_MF_TOC DMT");
                sql.Append("            INNER JOIN DT_R18_TMP");
                sql.Append("                    ON DMT.KANRI_ID   = DT_R18_TMP.KANRI_ID");
                sql.Append("                   AND DMT.LATEST_SEQ = DT_R18_TMP.SEQ");
                sql.Append("                   AND DT_R18_TMP.MANIFEST_ID IS NOT NULL");
                sql.Append("                   AND DT_R18_TMP.MANIFEST_ID <> ''");
                sql.Append("            INNER JOIN DT_R18_EX R18EX");
                sql.Append("                    ON R18EX.KANRI_ID   = DT_R18_TMP.KANRI_ID");
                sql.Append("                   AND R18EX.DELETE_FLG = 0");
                sql.Append("            LEFT JOIN (");
                sql.Append("                        SELECT");
                sql.Append("                            M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_NAME");
                sql.Append("                            ,DT_R18_MIX.*");
                sql.Append("                        FROM");
                sql.Append("                            DT_R18_MIX");
                sql.Append("                        INNER JOIN (SELECT * FROM M_DENSHI_HAIKI_SHURUI WHERE DELETE_FLG = 0) AS M_DENSHI_HAIKI_SHURUI");
                sql.Append("                            ON (DT_R18_MIX.HAIKI_DAI_CODE + DT_R18_MIX.HAIKI_CHU_CODE + DT_R18_MIX.HAIKI_SHO_CODE) = M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_CD");
                sql.Append("                        WHERE DT_R18_MIX.DELETE_FLG = 0");
                sql.Append("                    ) AS MIX");
                sql.Append("                    ON R18EX.SYSTEM_ID = MIX.SYSTEM_ID");
                sql.Append("            INNER JOIN (SELECT DT_R19_EX.* FROM DT_R19_EX");
                sql.Append("                  INNER JOIN");
                sql.Append("                  (SELECT KANRI_ID,MAX(UPN_ROUTE_NO) UPN_ROUTE_NO FROM DT_R19_EX WHERE DELETE_FLG = 0 GROUP BY KANRI_ID) UPNMAX");
                sql.Append("                  ON DT_R19_EX.KANRI_ID = UPNMAX.KANRI_ID AND DT_R19_EX.UPN_ROUTE_NO = UPNMAX.UPN_ROUTE_NO AND DT_R19_EX.DELETE_FLG = 0");
                sql.Append("                  ) MAXR19EX ON R18EX.SYSTEM_ID = MAXR19EX.SYSTEM_ID AND R18EX.SEQ = MAXR19EX.SEQ");
                sql.Append("            INNER JOIN DT_R19 MAXR19");
                sql.Append("                    ON MAXR19.KANRI_ID = DT_R18_TMP.KANRI_ID");
                sql.Append("                   AND MAXR19.SEQ = DT_R18_TMP.SEQ");
                sql.Append("                   AND MAXR19EX.UPN_ROUTE_NO = MAXR19.UPN_ROUTE_NO");
                sql.Append("            INNER JOIN DT_R19_EX MINR19EX");
                sql.Append("                    ON R18EX.SYSTEM_ID = MINR19EX.SYSTEM_ID");
                sql.Append("                   AND R18EX.SEQ = MINR19EX.SEQ");
                sql.Append("                   AND MINR19EX.UPN_ROUTE_NO = 1");
                sql.Append("       LEFT OUTER JOIN DT_R13");
                sql.Append("                    ON DT_R13.KANRI_ID = DT_R18_TMP.KANRI_ID");
                sql.Append("                   AND DT_R13.SEQ      = DT_R18_TMP.SEQ");
                sql.Append("       LEFT OUTER JOIN DT_R13_EX");
                sql.Append("                    ON DT_R13_EX.KANRI_ID = DT_R13.KANRI_ID");
                sql.Append("                   AND DT_R13_EX.DELETE_FLG = 0");
                sql.Append("       LEFT OUTER JOIN M_DENSHI_HAIKI_SHURUI MDHS");
                sql.Append("                    ON MDHS.HAIKI_SHURUI_CD = DT_R18_TMP.HAIKI_DAI_CODE + DT_R18_TMP.HAIKI_CHU_CODE + DT_R18_TMP.HAIKI_SHO_CODE");
                sql.Append("                   AND MDHS.DELETE_FLG      = 0");
                sql.Append("       LEFT OUTER JOIN M_GYOUSHA HST_GYOUSHA");
                sql.Append("                    ON R18EX.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD");
                sql.Append("       WHERE ");
                sql.Append("                   ((DT_R18_TMP.FIRST_MANIFEST_FLAG IS NULL OR DT_R18_TMP.FIRST_MANIFEST_FLAG = '') OR ISNULL(HST_GYOUSHA.JISHA_KBN, 0) = 0)");
                sql.Append("                   AND (DMT.STATUS_FLAG = 4)");

                sql.Append("               ) FIRST_MANIFEST");
                sql.Append("            ON MANIFEST_RELATION.FIRST_SYSTEM_ID = FIRST_MANIFEST.DETAIL_SYSTEM_ID AND MANIFEST_RELATION.FIRST_HAIKI_KBN_CD = FIRST_MANIFEST.HAIKI_KBN_CD");
                sql.Append("     FULL JOIN (");

                //紙マニ（2次）のデータ
                sql.Append("                SELECT TME_SECOND.SYSTEM_ID,");
                sql.Append("                       TME_SECOND.SEQ,");
                sql.Append("                       NULL AS KANRI_ID,");
                sql.Append("                       NULL AS LATEST_SEQ,");
                sql.Append("                       TME_SECOND.KOUFU_KBN,");
                sql.Append("                       TME_SECOND.KOUFU_DATE,");
                sql.Append("                       TME_SECOND.MANIFEST_ID,");
                sql.Append("                       TME_SECOND.HAIKI_KBN_CD,");
                sql.Append("                       TME_SECOND.HST_GYOUSHA_CD,");
                // 20140617 katen 不具合No.4810 start‏
                //sql.Append("                       TME_SECOND.SBN_JYUTAKUSHA_CD,");
                sql.Append("                       TME_SECOND.SBN_GYOUSHA_CD                               AS SBN_JYUTAKUSHA_CD,");
                // 20140617 katen 不具合No.4810 end‏
                sql.Append("                       TMD_SECOND.LAST_SBN_GYOUSHA_CD,");
                sql.Append("                       TME_SECOND.HST_GENBA_CD,");
                // 20140617 katen 不具合No.4810 start‏
                //sql.Append("                       TME_SECOND.LAST_SBN_GENBA_CD,");
                //sql.Append("                       TME_SECOND.SBN_JYUTAKUSHA_NAME,");
                //sql.Append("                       TME_SECOND.LAST_SBN_GENBA_NAME,");
                sql.Append("                       TMD_SECOND.LAST_SBN_GENBA_CD,");
                sql.Append("                       TME_SECOND.SBN_GYOUSHA_NAME                             AS SBN_JYUTAKUSHA_NAME,");
                sql.Append("                       LAST_SBN_GENBA.GENBA_NAME1 + LAST_SBN_GENBA.GENBA_NAME2 AS LAST_SBN_GENBA_NAME,");
                // 20140617 katen 不具合No.4810 end‏
                sql.Append("                       TMD_SECOND.DETAIL_SYSTEM_ID,");
                sql.Append("                       TMU_SECOND_UPN.UPN_GYOUSHA_CD,");
                sql.Append("                       TMU_SECOND_SBN.UPN_SAKI_GENBA_CD,");
                sql.Append("                       TMU_SECOND_SBN.UPN_SAKI_GENBA_NAME,");
                sql.Append("                       TMU_SECOND_UPN.UPN_GYOUSHA_NAME,");
                sql.Append("                       TMU_SECOND_SBN.UPN_END_DATE,");
                sql.Append("                       TMD_SECOND.SBN_END_DATE,");
                sql.Append("                       TMD_SECOND.HAIKI_SHURUI_CD,");
                sql.Append("                       HAIKI_SECOND.HAIKI_SHURUI_NAME,");
                sql.Append("                       TMD_SECOND.GENNYOU_SUU,");
                sql.Append("                       TMD_SECOND.KANSAN_SUU,");
                sql.Append("                       TMD_SECOND.LAST_SBN_END_DATE");
                sql.Append("                  FROM (SELECT * FROM T_MANIFEST_ENTRY WHERE FIRST_MANIFEST_KBN = 1 AND DELETE_FLG = 0) TME_SECOND ");
                sql.Append("             LEFT JOIN T_MANIFEST_DETAIL TMD_SECOND");
                sql.Append("                    ON TME_SECOND.SYSTEM_ID = TMD_SECOND.SYSTEM_ID");
                sql.Append("                   AND TME_SECOND.SEQ       = TMD_SECOND.SEQ");
                //sql.Append("             LEFT JOIN T_MANIFEST_UPN TMU_SECOND");
                sql.Append("             LEFT JOIN T_MANIFEST_UPN TMU_SECOND_UPN");
                sql.Append("                    ON TME_SECOND.SYSTEM_ID = TMU_SECOND_UPN.SYSTEM_ID");
                sql.Append("                   AND TME_SECOND.SEQ       = TMU_SECOND_UPN.SEQ");
                // 20140616 katen 不具合No.4809 start‏
                sql.Append("                   AND TMU_SECOND_UPN.UPN_ROUTE_NO = 1");
                //sql.Append("             LEFT JOIN (SELECT T_MANIFEST_ENTRY.SYSTEM_ID,");
                //sql.Append("                               T_MANIFEST_ENTRY.SEQ,");
                //sql.Append("                               MAX(T_MANIFEST_UPN.UPN_ROUTE_NO) AS UPN_ROUTE_NO");
                //sql.Append("                          FROM T_MANIFEST_ENTRY");
                //sql.Append("                    INNER JOIN T_MANIFEST_UPN");
                //sql.Append("                            ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_UPN.SYSTEM_ID");
                //sql.Append("                           AND T_MANIFEST_ENTRY.SEQ       = T_MANIFEST_UPN.SEQ");
                //sql.Append("                           AND T_MANIFEST_UPN.UPN_END_DATE IS NOT NULL");
                //sql.Append("                         WHERE T_MANIFEST_ENTRY.DELETE_FLG = 'false'");
                //sql.Append("                      GROUP BY T_MANIFEST_ENTRY.SYSTEM_ID,");
                //sql.Append("                               T_MANIFEST_ENTRY.SEQ) TMU_SEARCH");
                //sql.Append("                    ON TMU_SECOND.SYSTEM_ID    = TMU_SEARCH.SYSTEM_ID");
                //sql.Append("                   AND TMU_SECOND.SEQ          = TMU_SEARCH.SEQ");
                //sql.Append("                   AND TMU_SECOND.UPN_ROUTE_NO = TMU_SEARCH.UPN_ROUTE_NO");
                //sql.Append("             LEFT JOIN T_MANIFEST_UPN MIN_TMU");
                //sql.Append("                    ON TME_SECOND.SYSTEM_ID = MIN_TMU.SYSTEM_ID");
                //sql.Append("                   AND TME_SECOND.SEQ       = MIN_TMU.SEQ");
                //sql.Append("                   AND MIN_TMU.UPN_ROUTE_NO = 1");
                // 20140616 katen 不具合No.4809 end‏
                // 処分事業場を現場入力済みのMAXルート番号で取得する
                sql.Append("             LEFT JOIN (");
                sql.Append("                        SELECT TMU.*");
                sql.Append("                        FROM T_MANIFEST_UPN TMU,");
                sql.Append("                             (SELECT SYSTEM_ID, SEQ, MAX(UPN_ROUTE_NO) AS UPN_ROUTE_NO");
                sql.Append("                              FROM (");
                sql.Append("                                     SELECT SYSTEM_ID, SEQ, UPN_ROUTE_NO");
                sql.Append("                                     FROM T_MANIFEST_UPN");
                sql.Append("                                     WHERE UPN_GYOUSHA_CD IS NOT NULL AND UPN_GYOUSHA_CD <> '' AND UPN_SAKI_GYOUSHA_CD IS NOT NULL AND UPN_SAKI_GYOUSHA_CD <> ''");
                sql.Append("                                   ) TMU_GROUP");
                sql.Append("                              GROUP BY SYSTEM_ID, SEQ");
                sql.Append("                             ) AS TMU_ROUTE_SBN");
                sql.Append("                        WHERE TMU.SYSTEM_ID = TMU_ROUTE_SBN.SYSTEM_ID");
                sql.Append("                              AND TMU.SEQ = TMU_ROUTE_SBN.SEQ");
                sql.Append("                              AND TMU.UPN_ROUTE_NO = TMU_ROUTE_SBN.UPN_ROUTE_NO");
                sql.Append("                       ) TMU_SECOND_SBN");
                sql.Append("                    ON TME_SECOND.SYSTEM_ID = TMU_SECOND_SBN.SYSTEM_ID");
                sql.Append("                   AND TME_SECOND.SEQ       = TMU_SECOND_SBN.SEQ");
                sql.Append("             LEFT JOIN M_HAIKI_SHURUI HAIKI_SECOND");
                sql.Append("                    ON TMD_SECOND.HAIKI_SHURUI_CD = HAIKI_SECOND.HAIKI_SHURUI_CD");
                sql.Append("                   AND TME_SECOND.HAIKI_KBN_CD    = HAIKI_SECOND.HAIKI_KBN_CD");
                // 20140617 katen 不具合No.4810 start‏
                sql.Append("             LEFT JOIN M_GENBA AS LAST_SBN_GENBA");
                sql.Append("                    ON TMD_SECOND.LAST_SBN_GYOUSHA_CD = LAST_SBN_GENBA.GYOUSHA_CD");
                sql.Append("                   AND TMD_SECOND.LAST_SBN_GENBA_CD   = LAST_SBN_GENBA.GENBA_CD");
                sql.Append("                   AND LAST_SBN_GENBA.DELETE_FLG      = 'false'");
                // 20140617 katen 不具合No.4810 end‏

                sql.Append("             UNION ALL");

                //電子マニ（2次）のデータ
                sql.Append("                SELECT R18EX.SYSTEM_ID,");
                sql.Append("                       R18EX.SEQ,");
                sql.Append("                       DMT.KANRI_ID                                                          AS KANRI_ID,");
                sql.Append("                       DMT.LATEST_SEQ                                                        AS LATEST_SEQ,");
                // #1398 電子マニには「交付区分」が存在しないため、一覧画面で指定された交付区分をそのまま使用する
                sql.AppendFormat(
                           "                       ''                                                                    AS KOUFU_KBN,");
                sql.Append("                       CASE ISDATE(DT_R18_TMP.HIKIWATASHI_DATE)");
                sql.Append("                       WHEN 0 ");
                sql.Append("                       THEN NULL");
                sql.Append("                       ELSE");
                sql.Append("                       CONVERT(datetime,DT_R18_TMP.HIKIWATASHI_DATE,120)");
                sql.Append("                       END                                                                   AS KOUFU_DATE,");
                sql.Append("                       DT_R18_TMP.MANIFEST_ID                                                    AS MANIFEST_ID,");
                sql.Append("                       CAST('4' AS SMALLINT )                                                AS HAIKI_KBN_CD,");
                sql.Append("                       R18EX.HST_GYOUSHA_CD                                                  AS HST_GYOUSHA_CD,");
                sql.Append("                       R18EX.SBN_GYOUSHA_CD                                                  AS SBN_JYUTAKUSHA_CD,");
                sql.Append("                       DT_R13_EX.LAST_SBN_GYOUSHA_CD                                         AS LAST_SBN_GYOUSHA_CD,");
                sql.Append("                       R18EX.HST_GENBA_CD                                                    AS HST_GENBA_CD,");
                sql.Append("                       DT_R13_EX.LAST_SBN_GENBA_CD                                           AS LAST_SBN_GENBA_CD,");
                sql.Append("                       DT_R18_TMP.SBN_SHA_NAME                                                   AS SBN_JYUTAKUSHA_NAME,");
                sql.Append("                       DT_R13.LAST_SBN_JOU_NAME                                              AS LAST_SBN_GENBA_NAME,");
                sql.Append("                       R18EX.SYSTEM_ID                                                       AS DETAIL_SYSTEM_ID,");
                sql.Append("                       MINR19EX.UPN_GYOUSHA_CD                                               AS UPN_GYOUSHA_CD,");
                sql.Append("                       MAXR19EX.UPNSAKI_GENBA_CD                                             AS UPN_SAKI_GENBA_CD,");
                sql.Append("                       MAXR19.UPNSAKI_JOU_NAME                                               AS UPN_SAKI_GENBA_NAME,");
                sql.Append("                       MAXR19.UPN_SHA_NAME                                                   AS UPN_GYOUSHA_NAME,");
                sql.Append("                       CONVERT(datetime, CASE");
                sql.Append("                                         WHEN MAXR19.UPN_END_DATE = '0' OR MAXR19.UPN_END_DATE = ''");
                sql.Append("                                         THEN NULL");
                sql.Append("                                         ELSE MAXR19.UPN_END_DATE");
                sql.Append("                                         END ,120)                                           AS UPN_END_DATE,");
                sql.Append("                       CONVERT(datetime, CASE");
                sql.Append("                                         WHEN DT_R18_TMP.SBN_END_DATE = '0' OR DT_R18_TMP.SBN_END_DATE = ''");
                sql.Append("                                         THEN NULL");
                sql.Append("                                         ELSE DT_R18_TMP.SBN_END_DATE");
                sql.Append("                                         END ,120)                                           AS SBN_END_DATE,");
                sql.Append("                       DT_R18_TMP.HAIKI_DAI_CODE + DT_R18_TMP.HAIKI_CHU_CODE + DT_R18_TMP.HAIKI_SHO_CODE AS HAIKI_SHURUI_CD,");
                sql.Append("                       DT_R18_TMP.HAIKI_SHURUI                                                   AS HAIKI_SHURUI_NAME,");
                sql.Append("                       DT_R18_TMP.HAIKI_KAKUTEI_SUU                                              AS GENNYOU_SUU,");
                sql.Append("                       R18EX.KANSAN_SUU                                              AS KANSAN_SUU,");
                sql.Append("                       DT_R13.LAST_SBN_END_DATE");
                sql.Append("                  FROM DT_MF_TOC DMT");
                sql.Append("            INNER JOIN DT_R18_TMP");
                sql.Append("                    ON DMT.KANRI_ID   = DT_R18_TMP.KANRI_ID");
                sql.Append("                   AND DMT.LATEST_SEQ = DT_R18_TMP.SEQ");
                sql.Append("                   AND DT_R18_TMP.MANIFEST_ID IS NOT NULL");
                sql.Append("                   AND DT_R18_TMP.MANIFEST_ID <> ''");
                sql.Append("            INNER JOIN DT_R18_EX R18EX");
                sql.Append("                    ON R18EX.KANRI_ID   = DT_R18_TMP.KANRI_ID");
                sql.Append("                   AND R18EX.DELETE_FLG = 0");
                sql.Append("            INNER JOIN (SELECT DT_R19_EX.* FROM DT_R19_EX");
                sql.Append("                  INNER JOIN");
                sql.Append("                  (SELECT KANRI_ID,MAX(UPN_ROUTE_NO) UPN_ROUTE_NO FROM DT_R19_EX WHERE DELETE_FLG = 0 GROUP BY KANRI_ID) UPNMAX");
                sql.Append("                  ON DT_R19_EX.KANRI_ID = UPNMAX.KANRI_ID AND DT_R19_EX.UPN_ROUTE_NO = UPNMAX.UPN_ROUTE_NO AND DT_R19_EX.DELETE_FLG = 0");
                sql.Append("                  ) MAXR19EX ON R18EX.SYSTEM_ID = MAXR19EX.SYSTEM_ID AND R18EX.SEQ = MAXR19EX.SEQ");
                sql.Append("            INNER JOIN DT_R19 MAXR19");
                sql.Append("                    ON MAXR19.KANRI_ID = DT_R18_TMP.KANRI_ID");
                sql.Append("                   AND MAXR19.SEQ = DT_R18_TMP.SEQ");
                sql.Append("                   AND MAXR19EX.UPN_ROUTE_NO = MAXR19.UPN_ROUTE_NO");
                sql.Append("            INNER JOIN DT_R19_EX MINR19EX");
                sql.Append("                    ON R18EX.SYSTEM_ID = MINR19EX.SYSTEM_ID");
                sql.Append("                   AND R18EX.SEQ = MINR19EX.SEQ");
                sql.Append("                   AND MINR19EX.UPN_ROUTE_NO = 1");
                sql.Append("       LEFT OUTER JOIN DT_R13");
                sql.Append("                    ON DT_R13.KANRI_ID = DT_R18_TMP.KANRI_ID");
                sql.Append("                   AND DT_R13.SEQ      = DT_R18_TMP.SEQ");
                sql.Append("       LEFT OUTER JOIN DT_R13_EX");
                sql.Append("                    ON DT_R13_EX.KANRI_ID = DT_R13.KANRI_ID");
                sql.Append("                   AND DT_R13_EX.DELETE_FLG = 0");
                sql.Append("       LEFT OUTER JOIN M_DENSHI_HAIKI_SHURUI MDHS");
                sql.Append("                    ON MDHS.HAIKI_SHURUI_CD = DT_R18_TMP.HAIKI_DAI_CODE + DT_R18_TMP.HAIKI_CHU_CODE + DT_R18_TMP.HAIKI_SHO_CODE");
                sql.Append("                   AND MDHS.DELETE_FLG      = 0");
                sql.Append("       LEFT OUTER JOIN M_GYOUSHA HST_GYOUSHA");
                sql.Append("                    ON R18EX.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD");
                sql.Append("       WHERE ");
                sql.Append("                   (DT_R18_TMP.FIRST_MANIFEST_FLAG IS NOT NULL AND DT_R18_TMP.FIRST_MANIFEST_FLAG <> '' AND ISNULL(HST_GYOUSHA.JISHA_KBN, 0) = 1)");
                sql.Append("                   AND (DMT.STATUS_FLAG = 4)");

                sql.Append("               ) SECOND_MANIFEST");
                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
                //sql.Append("            ON MANIFEST_RELATION.NEXT_SYSTEM_ID   = SECOND_MANIFEST.SYSTEM_ID AND MANIFEST_RELATION.NEXT_HAIKI_KBN_CD = SECOND_MANIFEST.HAIKI_KBN_CD");
                sql.Append("            ON MANIFEST_RELATION.NEXT_SYSTEM_ID   = SECOND_MANIFEST.DETAIL_SYSTEM_ID AND MANIFEST_RELATION.NEXT_HAIKI_KBN_CD = SECOND_MANIFEST.HAIKI_KBN_CD");
                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end
                #endregion

                #region WHERE句
                var sqlWhere = new StringBuilder();
                //抽出対象区分によって、検索条件の限定テーブルを設定する
                string tableName = this.form.txtNum_ChushutuTaishouKbn.Text == "1" ? "FIRST_MANIFEST" : "SECOND_MANIFEST";

                if (!String.IsNullOrEmpty(this.form.cantxt_KohuNo.Text))
                {
                    //交付番号が空じゃない場合、交付番号の検索条件を追加する
                    sqlWhere.AppendFormat("           AND {0}.MANIFEST_ID  = '{1}'", new object[] { tableName, this.form.cantxt_KohuNo.Text });
                }

                //マニフェスト種類
                switch (this.form.txtNum_ManifestShurui.Text)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                        //マニフェスト種類が「1.直行」「2.建廃」「3.積替」「4.電子」を選んだ場合、廃棄区分の検索条件を追加する
                        //そして「5.すべて」を選んだ場合、廃棄区分の検索条件を追加しない
                        sqlWhere.AppendFormat("           AND {0}.HAIKI_KBN_CD = {1}", new object[] { tableName, this.form.txtNum_ManifestShurui.Text });
                        break;
                }
                //抽出日付区分
                switch (this.form.txtNum_ChushutuHiduke.Text)
                {
                    case "1":
                        //交付年月日
                        sqlWhere.AppendFormat("           AND {0}.KOUFU_DATE BETWEEN '{1}' AND '{2}'", new object[] { tableName, this.form.HIDUKE_FROM.Value, this.form.HIDUKE_TO.Value });
                        break;
                    case "2":
                        //運搬終了日
                        sqlWhere.AppendFormat("           AND {0}.UPN_END_DATE BETWEEN '{1}' AND '{2}'", new object[] { tableName, this.form.HIDUKE_FROM.Value, this.form.HIDUKE_TO.Value });
                        break;
                    case "3":
                        //処分終了日
                        sqlWhere.AppendFormat("           AND {0}.SBN_END_DATE BETWEEN '{1}' AND '{2}'", new object[] { tableName, this.form.HIDUKE_FROM.Value, this.form.HIDUKE_TO.Value });
                        break;
                    case "4":
                        //最終処分終了日
                        sqlWhere.AppendFormat("           AND {0}.LAST_SBN_END_DATE BETWEEN '{1}' AND '{2}'", new object[] { tableName, this.form.HIDUKE_FROM.Value, this.form.HIDUKE_TO.Value });
                        break;
                }
                //業者CDが空じゃない場合
                if (!string.IsNullOrEmpty(this.form.cantxt_GyousyaCd.Text))
                {
                    //抽出業者区分
                    switch (this.form.txtNum_ChushutuGyosha.Text)
                    {
                        case "1":
                            //排出事業者
                            sqlWhere.AppendFormat("           AND {0}.HST_GYOUSHA_CD      = '{1}'", new object[] { tableName, this.form.cantxt_GyousyaCd.Text.PadLeft(6, '0') });
                            break;
                        case "2":
                            //運搬受託者
                            sqlWhere.AppendFormat("           AND {0}.UPN_GYOUSHA_CD      = '{1}'", new object[] { tableName, this.form.cantxt_GyousyaCd.Text.PadLeft(6, '0') });
                            break;
                        case "3":
                            //処分受託者
                            sqlWhere.AppendFormat("           AND {0}.SBN_JYUTAKUSHA_CD   = '{1}'", new object[] { tableName, this.form.cantxt_GyousyaCd.Text.PadLeft(6, '0') });
                            break;
                        case "4":
                            //最終処分場所
                            sqlWhere.AppendFormat("           AND {0}.LAST_SBN_GYOUSHA_CD = '{1}'", new object[] { tableName, this.form.cantxt_GyousyaCd.Text.PadLeft(6, '0') });
                            break;
                    }
                }
                //現場CDが空じゃない場合
                if (!string.IsNullOrEmpty(this.form.cantxt_GenbaCd.Text))
                {
                    //抽出業者区分
                    switch (this.form.txtNum_ChushutuGyosha.Text)
                    {
                        case "1":
                            //排出事業者
                            sqlWhere.AppendFormat("           AND {0}.HST_GENBA_CD      = '{1}'", new object[] { tableName, this.form.cantxt_GenbaCd.Text.PadLeft(6, '0') });
                            break;
                        case "2":
                        case "3":
                            //運搬受託者と処分受託者
                            sqlWhere.AppendFormat("           AND {0}.UPN_SAKI_GENBA_CD = '{1}'", new object[] { tableName, this.form.cantxt_GenbaCd.Text.PadLeft(6, '0') });
                            break;
                        case "4":
                            //最終処分場所
                            sqlWhere.AppendFormat("           AND {0}.LAST_SBN_GENBA_CD = '{1}'", new object[] { tableName, this.form.cantxt_GenbaCd.Text.PadLeft(6, '0') });
                            break;
                    }
                }

                // WHERE句を追加
                if (!string.IsNullOrEmpty(sqlWhere.ToString()))
                {
                    //検索条件がある場合、WHEREを追加する
                    //sql.Append("         WHERE ");
                    sql.Append("\n"); //sumi WHEREの前で改行する
                    sql.Append("WHERE ");
                    //上記全ての条件が「AND」を付けているので、一番目の「AND」を削除する
                    sql.Append(sqlWhere.ToString().Substring(sqlWhere.ToString().IndexOf("AND") + "AND".Length));
                }
                #endregion

                sql.Append("                ) SEARCH_DATA");
                //紐付状況の検索条件を追加する
                sql.Append(" WHERE 紐付状況 IS NOT NULL");
                switch (this.form.txtNum_HimodukeJyoukyou.Text)
                {
                    case "1":
                        sql.Append("   AND 紐付状況 = '済'");
                        break;
                    case "2":
                        sql.Append("   AND 紐付状況 = '未'");
                        break;
                }

                #region ORDERBY句
                sql.Append(" ORDER BY ");
                sql.AppendFormat(" \"{0}\", \"{1}\"", new object[] { this.KOUFU_DATE_IJI, this.KOUFU_DATE_NIJI });
                #endregion

                this.createSql = sql.ToString();
                //検索を行う
                this.searchResult = daoIchiran.getdateforstringsql(createSql);
                ret_cnt = this.searchResult.Rows.Count;

                ////初期化
                //this.form.customDataGridView1.DataSource = null;
                //this.form.customDataGridView1.Rows.Clear();
                //this.form.customDataGridView1.Columns.Clear();

                this.form.Table = this.searchResult;
                //検索結果を画面に表示する
                this.form.ShowData();

                //読込データ件数
                if (this.form.customDataGridView1 != null)
                {
                    this.headForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                }
                else
                {
                    this.headForm.ReadDataNumber.Text = "0";
                }

                //フォーカス初期化
                if (this.form.customDataGridView1.Columns.Count > 0 && this.form.customDataGridView1.Rows.Count > 0)
                {
                    this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1[0, 0];
                }

                // 20140605 katen 不具合No.4689 start‏
                if (this.form.customDataGridView1.Columns.Count > 0)
                {
                    Color color_iji = Color.FromArgb(0, 105, 51);
                    Color color_niji = Color.FromArgb(0, 51, 160);
                    for (int i = 0; i < this.form.customDataGridView1.ColumnCount; i++)
                    {
                        //9列目までは一次、後は二次
                        if (i <= 9)
                        {
                            this.form.customDataGridView1.Columns[i].HeaderCell.Style.BackColor = color_iji;
                        }
                        else
                        {
                            this.form.customDataGridView1.Columns[i].HeaderCell.Style.BackColor = color_niji;
                        }
                    }
                }

                // 20140605 katen 不具合No.4689 end‏
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret_cnt = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret_cnt);
            }
            //取得件数
            return ret_cnt;
        }
        #endregion

        public void IntializeResultForm()
        {
            #region 表示を初期化
            this.headForm.ReadDataNumber.Text = "";
            this.headForm.ReadDataNumber.Refresh();
            //DataGridView
            this.form.customDataGridView1.DataSource = null;
            this.form.customDataGridView1.Rows.Clear();
            this.form.customDataGridView1.Columns.Clear();
            this.form.customDataGridView1.Refresh();
            this.form.Refresh();
            Application.DoEvents();
            #endregion
        }

        #region ラベルの背景色を変更
        /// <summary>
        /// ラベルの背景色を変更
        /// </summary>
        /// <param name="BackColor">設定する色</param>
        public void SetColor(Color BackColor)
        {
            LogUtility.DebugMethodStart(BackColor);

            this.headForm.lb_title.BackColor = BackColor;
            this.headForm.label4.BackColor = BackColor;
            this.headForm.label5.BackColor = BackColor;
            this.allControl.OfType<Label>().ToList().ForEach(c => c.BackColor = BackColor);
            this.form.label5.BackColor = Color.Transparent;
            LogUtility.DebugMethodEnd(BackColor);
        }
        #endregion

        #region 画面に必須のファンクション
        /// <summary>
        /// 論理削除処理
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登録
        /// </summary>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 画面遷移
        /// <summary>
        /// 画面遷移
        /// </summary>
        public void FormChanges(WINDOW_TYPE WindowType)
        {
            LogUtility.DebugMethodStart();

            String kanriId = string.Empty;
            String latestSeq = string.Empty;
            try
            {
                #region 新規
                if (WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    if ("5".Equals(this.form.txtNum_ManifestShurui.Text))
                    {
                        //「全て」が選択される場合、メッセージ「廃棄物区分は、「5.全て」以外の区分を選択してください。」を表示する
                        MessageBoxUtility.MessageBoxShow("E051", "廃棄物区分は、「5.全て」以外の区分");
                        this.form.txtNum_ManifestShurui.Focus();
                        return;
                    }
                    //画面起動
                    this.form.ParamOut_WinType = (int)WindowType;
                    this.form.ParamOut_SysID = string.Empty;

                    switch (this.form.txtNum_ManifestShurui.Text)
                    {
                        case "1"://G119 産廃（直行）マニフェスト一覧
                            FormManager.OpenFormWithAuth("G119", WindowType, WindowType, "", this.form.ParamOut_SysID, "", this.form.ParamOut_WinType, null, null, null, Convert.ToInt16(this.form.txtNum_ChushutuTaishouKbn.Text));
                            break;

                        case "2"://G121 建廃マニフェスト一覧
                            FormManager.OpenFormWithAuth("G121", WindowType, WindowType, "", this.form.ParamOut_SysID, "", this.form.ParamOut_WinType, null, null, null, Convert.ToInt16(this.form.txtNum_ChushutuTaishouKbn.Text));
                            break;

                        case "3"://G120 産廃（積替）マニフェスト一覧
                            FormManager.OpenFormWithAuth("G120", WindowType, WindowType, "", this.form.ParamOut_SysID, "", this.form.ParamOut_WinType, null, null, null, Convert.ToInt16(this.form.txtNum_ChushutuTaishouKbn.Text));
                            break;

                        case "4"://電子
                            FormManager.OpenFormWithAuth("G141", WindowType, WindowType, kanriId, latestSeq, null, null, null, Convert.ToInt16(this.form.txtNum_ChushutuTaishouKbn.Text));
                            break;
                    }
                    // 20140606 katen 不具合No.4691 end‏
                    return;
                }
                #endregion

                //検索結果(マニフェストパターン)が1件もない場合
                if (this.searchResult.Rows.Count <= 0)
                {
                    switch (WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG://新規
                            return;
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正
                            //メッセージ「修正するマニフェストを　マニフェスト紐付一覧　から選択してください。」を表示する
                            MessageBoxUtility.MessageBoxShow("E029", "修正するマニフェスト", "マニフェスト紐付一覧");
                            return;

                        case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除
                            //メッセージ「削除するマニフェストを　マニフェスト紐付一覧　から選択してください。」を表示する
                            MessageBoxUtility.MessageBoxShow("E029", "削除するマニフェスト", "マニフェスト紐付一覧");
                            return;
                    }
                }

                //画面で行が選択されていない場合
                if (this.form.customDataGridView1.Rows.Count > 0
                    && this.form.customDataGridView1.CurrentRow == null)
                {
                    switch (WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG://新規
                            return;
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正
                            //メッセージ「修正するマニフェストを　マニフェスト紐付一覧　から選択してください。」を表示する
                            MessageBoxUtility.MessageBoxShow("E029", "修正するマニフェスト", "マニフェスト紐付一覧");
                            return;

                        case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除
                            //メッセージ「削除するマニフェストを　マニフェスト紐付一覧　から選択してください。」を表示する
                            MessageBoxUtility.MessageBoxShow("E029", "削除するマニフェスト", "マニフェスト紐付一覧");
                            return;
                    }
                    return;
                }

                //選んだデータを行目を取得
                int i = this.form.customDataGridView1.CurrentRow.Index;

                string manifestKbn = string.Empty;
                string system_id = string.Empty;
                string seq = string.Empty;
                //選んだ「抽出対象区分」によって、選んだデータの中に廃棄区分、システムID、SEQ、管理ID、最終SEQを取得する
                if (this.form.txtNum_ChushutuTaishouKbn.Text == "1")
                {
                    manifestKbn = Convert.ToString(this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_HAIKI_KBN_CD_IJI].Value);
                    system_id = Convert.ToString(this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_SYSTEM_ID_IJI].Value);
                    seq = Convert.ToString(this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_SEQ_IJI].Value);
                    kanriId = Convert.ToString(this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_KANRI_ID_IJI].Value);
                    latestSeq = Convert.ToString(this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_LATEST_SEQ_IJI].Value);
                }
                else
                {
                    manifestKbn = Convert.ToString(this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_HAIKI_KBN_CD_NIJI].Value);
                    system_id = Convert.ToString(this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_SYSTEM_ID_NIJI].Value);
                    seq = Convert.ToString(this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_SEQ_NIJI].Value);
                    kanriId = Convert.ToString(this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_KANRI_ID_NIJI].Value);
                    latestSeq = Convert.ToString(this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_LATEST_SEQ_NIJI].Value);
                }
                switch (manifestKbn)
                {
                    case "1"://産廃（直行）
                    case "2"://建廃
                    case "3"://産廃（積替）

                        //SYSTEM_IDが取得できない場合。
                        if (string.IsNullOrEmpty(system_id) || string.IsNullOrEmpty(seq))
                        {
                            switch (WindowType)
                            {
                                case WINDOW_TYPE.NEW_WINDOW_FLAG://新規
                                    break;

                                case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正
                                case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除
                                    //メッセージ「該当データは存在しません。\n他ユーザーによって削除されたか、未登録のデータです。」を表示する
                                    MessageBoxUtility.MessageBoxShow("E045");
                                    return;
                            }
                        }

                        //SYSTEM_IDが取得できた場合の存在チェック。
                        DataTable dtPaper = new DataTable();
                        dto = new DTOClass();
                        dto.SYSTEM_ID = system_id;
                        dto.SEQ = seq;
                        dto.FIRST_MANIFEST_KBN = this.form.txtNum_ChushutuTaishouKbn.Text == "1" ? "0" : "1";
                        dto.HAIKI_KBN_CD = manifestKbn;
                        dtPaper = this.daoIchiran.GetDataForEntity(dto);
                        if (dtPaper.Rows.Count <= 0)
                        {
                            //データを取得できない場合
                            switch (WindowType)
                            {
                                case WINDOW_TYPE.NEW_WINDOW_FLAG://新規
                                    break;

                                case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正
                                case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除
                                    //メッセージ「該当データは存在しません。\n他ユーザーによって削除されたか、未登録のデータです。」を表示する
                                    MessageBoxUtility.MessageBoxShow("E045");
                                    return;
                            }
                        }
                        break;

                    case "4"://電子
                        //KANRI_IDが取得できない場合。
                        if (string.IsNullOrEmpty(kanriId) || string.IsNullOrEmpty(latestSeq))
                        {
                            switch (WindowType)
                            {
                                case WINDOW_TYPE.NEW_WINDOW_FLAG://新規
                                    break;

                                case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正
                                case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除
                                    //メッセージ「該当データは存在しません。\n他ユーザーによって削除されたか、未登録のデータです。」を表示する
                                    MessageBoxUtility.MessageBoxShow("E045");
                                    return;
                            }
                        }

                        //KANRI_IDが取得できた場合の存在チェック。
                        DataTable dtDenshi = new DataTable();
                        dto = new DTOClass();
                        dto.KANRI_ID = kanriId;
                        dto.LATEST_SEQ = latestSeq;
                        dto.FIRST_MANIFEST_KBN = this.form.txtNum_ChushutuTaishouKbn.Text == "1" ? "0" : "1";
                        dto.HAIKI_KBN_CD = manifestKbn;
                        dtDenshi = this.daoIchiran.GetDataForEntity(dto);
                        if (dtDenshi.Rows.Count <= 0)
                        {
                            //データを取得できない場合
                            switch (WindowType)
                            {
                                case WINDOW_TYPE.NEW_WINDOW_FLAG://新規
                                    break;

                                case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正
                                case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除
                                    //メッセージ「該当データは存在しません。\n他ユーザーによって削除されたか、未登録のデータです。」を表示する
                                    MessageBoxUtility.MessageBoxShow("E045");
                                    return;
                            }
                        }
                        break;
                    default:
                        //「廃棄区分」を取得できない場合
                        //メッセージ「該当データは存在しません。\n他ユーザーによって削除されたか、未登録のデータです。」を表示する
                        MessageBoxUtility.MessageBoxShow("E045");
                        break;
                }

                // 画面ID取得
                var formID = string.Empty;
                switch (manifestKbn)
                {
                    //G119 産廃（直行）マニフェスト一覧
                    case "1": formID = "G119"; break;
                    //G121 建廃マニフェスト一覧
                    case "2": formID = "G121"; break;
                    //G120 産廃（積替）マニフェスト一覧
                    case "3": formID = "G120"; break;
                    //電子
                    case "4": formID = "G141"; break;
                }

                // 修正モードの場合権限チェック
                if (WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    if (r_framework.Authority.Manager.CheckAuthority(formID, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        // 修正モードに変更
                        WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    }
                    else if (r_framework.Authority.Manager.CheckAuthority(formID, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        // 参照モードに変更
                        WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E158", "修正");
                        return;
                    }
                }

                //画面起動
                this.form.ParamOut_WinType = (int)WindowType;
                this.form.ParamOut_SysID = system_id;
                string haikiKbn = manifestKbn;
                switch (haikiKbn)
                {
                    case "1"://G119 産廃（直行）マニフェスト一覧
                        FormManager.OpenFormWithAuth("G119", WindowType, WindowType, "", this.form.ParamOut_SysID, "", this.form.ParamOut_WinType);
                        break;

                    case "2"://G121 建廃マニフェスト一覧
                        FormManager.OpenFormWithAuth("G121", WindowType, WindowType, "", this.form.ParamOut_SysID, "", this.form.ParamOut_WinType);
                        break;

                    case "3"://G120 産廃（積替）マニフェスト一覧
                        FormManager.OpenFormWithAuth("G120", WindowType, WindowType, "", this.form.ParamOut_SysID, "", this.form.ParamOut_WinType);
                        break;

                    case "4"://電子
                        FormManager.OpenFormWithAuth("G141", WindowType, WindowType, kanriId, latestSeq);
                        break;

                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormChanges", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }

            LogUtility.DebugMethodEnd();

            return;
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
                DateTime now;
                //アラート件数
                this.headForm.NumberAlert = this.headForm.InitialNumberAlert;
                //抽出対象区分
                // 20140618 ria EV004875 マニフェスト入力の[F7]状況ボタンを押下時に伝票紐付一覧が開く start
                //this.form.txtNum_ChushutuTaishouKbn.Text = "1";
                this.form.txtNum_ChushutuTaishouKbn.Text = string.IsNullOrEmpty(Convert.ToString(this.form.formManiFlag)) ? "1" : Convert.ToString(this.form.formManiFlag);
                // 20140618 ria EV004875 マニフェスト入力の[F7]状況ボタンを押下時に伝票紐付一覧が開く end
                //紐付状況
                this.form.txtNum_HimodukeJyoukyou.Text = "1";
                //マニフェスト種類
                // 20140618 ria EV004875 マニフェスト入力の[F7]状況ボタンを押下時に伝票紐付一覧が開く start
                //this.form.txtNum_ManifestShurui.Text = "5";
                this.form.txtNum_ManifestShurui.Text = string.IsNullOrEmpty(this.form.formHaikiKbn) ? "5" : this.form.formHaikiKbn;
                // 20140618 ria EV004875 マニフェスト入力の[F7]状況ボタンを押下時に伝票紐付一覧が開く end
                //抽出日付
                this.form.txtNum_ChushutuHiduke.Text = "1";
                //抽出業者
                this.form.txtNum_ChushutuGyosha.Text = "1";
                //交付番号
                this.form.cantxt_KohuNo.Text = string.Empty;
                now = this.parentForm.sysDate;
                //抽出日付FROM
                this.form.HIDUKE_FROM.Value = now;
                //抽出日付TO
                this.form.HIDUKE_TO.Value = now;
                //業者CD
                this.form.cantxt_GyousyaCd.Text = string.Empty;
                //業者名
                this.form.ctxt_GyousyaName.Text = string.Empty;
                //現場CD
                this.form.cantxt_GenbaCd.Text = string.Empty;
                //現場名
                this.form.ctxt_GenbaName.Text = string.Empty;

                //並び順ソートヘッダー
                this.form.customSortHeader1.ClearCustomSortSetting();
                //フィルタをクリア
                this.form.customSearchHeader1.ClearCustomSearchSetting();

                switch (Kbn)
                {
                    case "Initial"://初期表示
                        break;
                    case "ClsSearchCondition"://検索条件をクリア
                        this.searchResult.Clear();
                        break;
                }

                //読込データ件数
                this.headForm.ReadDataNumber.Text = "0";

                //アラート件数
                this.headForm.alertNumber.Text = this.headForm.NumberAlert.ToString();
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
        }
        #endregion

        #region 画面チェック
        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <returns></returns>
        internal Boolean SearchCheck()
        {
            bool isErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                var allControlAndHeaderControls = allControl.ToList();
                allControlAndHeaderControls.AddRange(this.form.controlUtil.GetAllControls(this.headForm));
                var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());
                this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
                if (this.form.RegistErrorFlag)
                {
                    //必須チェックエラーフォーカス処理
                    this.SetErrorFocus();

                    isErr = true;
                    LogUtility.DebugMethodEnd(isErr);
                    return isErr;
                }

                DateTime dtpFrom = DateTime.Parse(this.form.HIDUKE_FROM.GetResultText());
                DateTime dtpTo = DateTime.Parse(this.form.HIDUKE_TO.GetResultText());
                DateTime dtpFromWithoutTime = DateTime.Parse(dtpFrom.ToShortDateString());
                DateTime dtpToWithoutTime = DateTime.Parse(dtpTo.ToShortDateString());

                int diff = dtpFromWithoutTime.CompareTo(dtpToWithoutTime);

                if (0 < diff)
                {
                    //対象期間内でないならエラーメッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    this.form.HIDUKE_FROM.IsInputErrorOccured = true;
                    this.form.HIDUKE_TO.IsInputErrorOccured = true;
                    // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 start
                    MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                    if (this.form.txtNum_ChushutuHiduke.Text == "1")
                    {
                        string[] errorMsg = { "交付年月日From", "交付年月日To" };
                        msglogic.MessageBoxShow("E030", errorMsg);
                    }
                    else if (this.form.txtNum_ChushutuHiduke.Text == "2")
                    {
                        string[] errorMsg = { "運搬終了日From", "運搬終了日To" };
                        msglogic.MessageBoxShow("E030", errorMsg);
                    }
                    else if (this.form.txtNum_ChushutuHiduke.Text == "3")
                    {
                        string[] errorMsg = { "処分終了日From", "処分終了日To" };
                        msglogic.MessageBoxShow("E030", errorMsg);
                    }
                    else if (this.form.txtNum_ChushutuHiduke.Text == "4")
                    {
                        string[] errorMsg = { "最終処分終了日From", "最終処分終了日To" };
                        msglogic.MessageBoxShow("E030", errorMsg);
                    }
                    // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 end
                    this.form.HIDUKE_FROM.Select();
                    this.form.HIDUKE_FROM.Focus();
                    isErr = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                isErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isErr);
            }
            return isErr;
        }

        //アラート件数
        public Boolean CheckNumberAlert(Int32 Kensu)
        {
            LogUtility.DebugMethodStart();

            Boolean check = false;

            if (Int32.Parse(this.headForm.NumberAlert.ToString()) < Kensu)
            {
                //検索件数がアラート件数を超えた場合
                //メッセージ「検索件数がアラート件数を超えました。\n表示を行いますか？」を表示する
                switch (MessageBoxUtility.MessageBoxShow("C025"))
                {
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                        check = true;
                        break;
                }
            }

            LogUtility.DebugMethodEnd();
            return check;
        }

        /// 交付番号入力チェック
        /// </summary>
        /// <returns>true:異常 false:正常</returns>
        public bool ChkKohuNo()
        {
            bool isErr = false;
            try
            {
                LogUtility.DebugMethodStart();
                string ret = ManifestoLogic.ChkKoufuNo(this.form.cantxt_KohuNo.Text, false);

                if (!string.IsNullOrEmpty(ret))
                {
                    //エラー時は自前で表示
                    Message.MessageBoxUtility.MessageBoxShowError(ret);
                }
                isErr = !string.IsNullOrEmpty(ret);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkKohuNo", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                isErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isErr);
            }
            return isErr;
        }

        /// <summary>
        /// 交付番号存在チェック
        /// </summary>
        public Boolean ExistKohuNo(string HaikiKbnCD, string ManifestId, ref string SystemId, ref string Seq, ref string SeqRD)
        {
            bool isNotExist = true;
            try
            {
                LogUtility.DebugMethodStart(HaikiKbnCD, ManifestId, SystemId, Seq, SeqRD);

                if (HaikiKbnCD == string.Empty || ManifestId == string.Empty)
                {
                    LogUtility.DebugMethodEnd(isNotExist, HaikiKbnCD, ManifestId, SystemId, Seq, SeqRD);
                    return isNotExist;
                }

                //マニフェスト一式（マニ明細、マニ印字明細除く）データ読み込み
                var Search = new CommonSerchParameterDtoCls();
                Search.MANIFEST_ID = ManifestId;
                Search.HAIKI_KBN_CD = HaikiKbnCD;

                DataTable dt = this.EntryDao.GetDataForEntity(Search);
                if (dt.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(isNotExist, HaikiKbnCD, ManifestId, SystemId, Seq, SeqRD);
                    return isNotExist;
                }

                SystemId = dt.Rows[0]["SYSTEM_ID"].ToString();
                Seq = dt.Rows[0]["SEQ"].ToString();
                SeqRD = dt.Rows[0]["TMRD_SEQ"].ToString();
                isNotExist = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ExistKohuNo", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                isNotExist = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isNotExist, HaikiKbnCD, ManifestId, SystemId, Seq, SeqRD);
            }
            return isNotExist;
        }
        #endregion

        #region 必須チェックエラーフォーカス処理
        /// <summary>
        /// 必須チェックエラーフォーカス処理
        /// </summary>
        /// <returns></returns>
        private void SetErrorFocus()
        {
            Control target = null;
            foreach (Control control in this.form.allControl)
            {
                if (control is ICustomTextBox)
                {
                    if (((ICustomTextBox)control).IsInputErrorOccured)
                    {
                        if (target != null)
                        {
                            if (target.TabIndex > control.TabIndex)
                            {
                                target = control;
                            }
                        }
                        else
                        {
                            target = control;
                        }
                    }
                }
            }
            //ヘッダーチェック
            foreach (Control control in this.headForm.allControl)
            {
                if (control is ICustomTextBox)
                {
                    if (((ICustomTextBox)control).IsInputErrorOccured)
                    {
                        target = control;
                    }
                }
            }
            if (target != null)
            {
                target.Focus();
            }
        }
        #endregion 必須チェックエラーフォーカス処理

        // 20140711 ria EV005194 業者CDを手入力しフォーカスアウトした時、業者名がセットされない、 start
        /// <summary>
        /// 業者チェック(排出事業者、運搬受託者、処分受託者) 
        /// </summary>
        /// <param name="obj">チェックコントロール</param>
        /// <param name="colname">チェックカラム名称</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkGyosya(object obj, string colname)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(obj, colname);

                TextBox txt = (TextBox)obj;
                if (txt.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new CommonGenbaDtoCls();
                Serch.GYOUSHA_CD = txt.Text;
                //最終処分業者の場合、最終処分場区分の条件を追加した
                this.searchResult = new DataTable();
                DataTable dt = mlogic.GetGenbaAll(Serch);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][colname].ToString() == "True")
                        {
                            ret = 0;
                            LogUtility.DebugMethodEnd(ret);
                            return ret;
                        }
                    }
                }
                this.form.messageShowLogic.MessageBoxShow("E020", "業者");

                txt.Focus();
                txt.SelectAll();
                ret = 2;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGyosya", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 現場チェック(排出事業者、運搬受託者、処分受託者) 
        /// </summary>
        /// <param name="genba">現場CD</param>
        /// <param name="gyosya">事業者CD</param>
        /// <param name="colname">チェックカラム名称</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkJigyouba(object genba, object gyosya, string colname)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(genba, gyosya, colname);

                TextBox txt1 = (TextBox)genba;
                TextBox txt2 = (TextBox)gyosya;

                //空
                if (txt1.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new CommonGenbaDtoCls();
                Serch.GENBA_CD = txt1.Text;
                Serch.GYOUSHA_CD = txt2.Text;

                this.searchResult = new DataTable();
                DataTable dt = this.mlogic.GetGenba(Serch);
                switch (dt.Rows.Count)
                {
                    case 0:
                        this.form.messageShowLogic.MessageBoxShow("E020", "現場");
                        break;

                    case 1:
                        if (dt.Rows[0][colname].ToString() == "True")
                        {
                            txt1.Text = dt.Rows[0]["GENBA_CD"].ToString();
                            txt2.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                            ret = 0;
                            LogUtility.DebugMethodEnd(ret);
                            return ret;
                        }
                        this.form.messageShowLogic.MessageBoxShow("E058");
                        break;

                    default:
                        switch (colname)
                        {
                            case "HAISHUTSU_NIZUMI_GENBA_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "排出事業者");
                                break;

                            case "SAISHUU_SHOBUNJOU_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "最終処分の業者");
                                break;

                            case "SHOBUN_NIOROSHI_GENBA_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "処分受託者");
                                break;

                            case "UNPAN_JUTAKUSHA_KAISHA_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "運搬受託者");
                                break;
                        }
                        break;
                }
                txt1.Focus();
                txt1.SelectAll();
                ret = 2;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkJigyouba", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 現場マスタから住所情報を取得してTextBoxに設定
        /// </summary>
        /// <param name="NameFlg">現場名 部分採用 All:正式名称1+正式名称2、Part1:正式名称1のみ</param>
        /// <param name="txt_gyoshaCd">業者CD</param>
        /// <param name="txt_JigyoubaCd">現場CD</param>
        /// <param name="txt_JigyoubaName">現場名</param>
        /// <param name="HAISHUTSU_NIZUMI_GENBA_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="SAISHUU_SHOBUNJOU_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="SHOBUN_NIOROSHI_GENBA_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="TSUMIKAEHOKAN_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="ISNOT_NEED_DELETE_FLG">削除チェックいるかどうかの判断フラッグ</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int SetAddressJigyouba(string NameFlg, CustomTextBox txt_gyoshaCd, CustomTextBox txt_JigyoubaCd, CustomTextBox ctxt_GenbaName
            , bool HAISHUTSU_NIZUMI_GENBA_KBN
            , bool SAISHUU_SHOBUNJOU_KBN
            , bool SHOBUN_NIOROSHI_GENBA_KBN
            , bool TSUMIKAEHOKAN_KBN
            , bool ISNOT_NEED_DELETE_FLG = false
            )
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(NameFlg, txt_gyoshaCd, txt_JigyoubaCd, ctxt_GenbaName
                    , HAISHUTSU_NIZUMI_GENBA_KBN, SAISHUU_SHOBUNJOU_KBN, SHOBUN_NIOROSHI_GENBA_KBN, TSUMIKAEHOKAN_KBN, ISNOT_NEED_DELETE_FLG);

                //空
                if (txt_gyoshaCd.Text == string.Empty || txt_JigyoubaCd.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new CommonGenbaDtoCls();
                Serch.GYOUSHA_CD = txt_gyoshaCd.Text;
                Serch.GENBA_CD = txt_JigyoubaCd.Text;
                Serch.ISNOT_NEED_DELETE_FLG = true;

                //区分
                if (HAISHUTSU_NIZUMI_GENBA_KBN)
                {
                    Serch.HAISHUTSU_NIZUMI_GYOUSHA_KBN = "1";
                    Serch.HAISHUTSU_NIZUMI_GENBA_KBN = "1";
                }
                if (SAISHUU_SHOBUNJOU_KBN)
                {
                    Serch.SAISHUU_SHOBUNJOU_KBN = "1";
                    Serch.SHOBUN_NIOROSHI_GYOUSHA_KBN = "1";
                }
                if (SHOBUN_NIOROSHI_GENBA_KBN)
                {
                    Serch.SHOBUN_NIOROSHI_GENBA_KBN = "1";
                    Serch.SHOBUN_NIOROSHI_GYOUSHA_KBN = "1";
                }
                if (TSUMIKAEHOKAN_KBN)
                {
                    Serch.TSUMIKAEHOKAN_KBN = "1";
                }

                DataTable dt = this.mlogic.GetGenba(Serch);
                switch (dt.Rows.Count)
                {
                    case 1://正常
                        break;

                    default://エラー
                        ret = 2;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                }

                //現場名
                if (ctxt_GenbaName != null)
                {

                    switch (NameFlg)
                    {
                        case "All"://「正式名称1 + 正式名称2」をセットする。
                            ctxt_GenbaName.Text = dt.Rows[0]["GENBA_NAME"].ToString();
                            break;

                        case "Part1"://「正式名称1」をセットする。
                            ctxt_GenbaName.Text = dt.Rows[0]["GENBA_NAME1"].ToString();
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetAddressJigyouba", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #region ポップアップの条件設定

        /// <summary>
        /// ポップアップの条件設定
        /// </summary>
        public void SetFilteringPopupData()
        {
            LogUtility.DebugMethodStart();

            //ポップアップ設定
            //抽出業者区分
            switch (this.form.txtNum_ChushutuGyosha.Text)
            {
                case "1":
                    //排出
                    Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                        this.form.cantxt_GyousyaCd,
                        this.form.ctxt_GyousyaName,
                        this.form.cbtn_GyousyaSan,
                        this.form.cantxt_GenbaCd,
                        this.form.ctxt_GenbaName,
                        this.form.cbtn_GenbaSan,
                        Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.KAMI,
                        Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.HAISHUTSU_NIZUMI_GYOUSHA,
                        false, false, Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.HAISHUTSU_NIZUMI_GENBA,
                        false, true, true);
                    break;

                case "2":
                    //運搬
                    Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                        this.form.cantxt_GyousyaCd,
                        this.form.ctxt_GyousyaName,
                        null,
                        null,
                        null,
                        null, Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.KAMI,
                        Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.UNPAN_JUTAKUSHA_KAISHA,
                        false, false, Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.NONE,
                        false, true, true);
                    break;

                case "3":
                    //処分
                    Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                        this.form.cantxt_GyousyaCd,
                        this.form.ctxt_GyousyaName,
                        this.form.cbtn_GyousyaSan,
                        this.form.cantxt_GenbaCd,
                        this.form.ctxt_GenbaName,
                        this.form.cbtn_GenbaSan,
                        Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.KAMI,
                        Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.SHOBUN_NIOROSHI_GYOUSHA,
                        false, false, Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.SHOBUN_NIOROSHI_GENBA,
                        false, true, true);
                    break;

                case "4":
                    //最終処分
                    Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                        this.form.cantxt_GyousyaCd,
                        this.form.ctxt_GyousyaName,
                        this.form.cbtn_GyousyaSan,
                        this.form.cantxt_GenbaCd,
                        this.form.ctxt_GenbaName,
                        this.form.cbtn_GenbaSan,
                        Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.KAMI,
                        Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.SHOBUN_NIOROSHI_GYOUSHA,
                        false, false, Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.SAISHUU_SHOBUNJOU,
                        false, true, true);
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        //業者区分
        public string SetCheckGyoushaItem()
        {
            LogUtility.DebugMethodStart();
            string returnFlg = string.Empty;
            switch (this.form.txtNum_ChushutuGyosha.Text)
            {
                case "1":
                    //排出
                    returnFlg = "HAISHUTSU_NIZUMI_GYOUSHA_KBN";
                    break;

                case "2":
                    //運搬
                    returnFlg = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                    break;

                case "3":
                //処分
                case "4":
                    //最終処分
                    returnFlg = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
                    break;
            }
            LogUtility.DebugMethodEnd();
            return returnFlg;
        }

        //現場区分
        public string SetCheckGenbanItem()
        {
            LogUtility.DebugMethodStart();

            string returnFlg = string.Empty;

            switch (this.form.txtNum_ChushutuGyosha.Text)
            {
                case "1":
                    //排出
                    returnFlg = "HAISHUTSU_NIZUMI_GENBA_KBN";
                    break;

                case "2":
                    //運搬
                    returnFlg = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                    break;

                case "3":
                    //処分
                    returnFlg = "SHOBUN_NIOROSHI_GENBA_KBN";
                    break;
                case "4":
                    //最終処分
                    returnFlg = "SAISHUU_SHOBUNJOU_KBN";
                    break;
            }
            LogUtility.DebugMethodEnd();
            return returnFlg;
        }
        // 20140711 ria EV005194 業者CDを手入力しフォーカスアウトした時、業者名がセットされない、 end

        /// 20141128 Houkakou 「紐付状況一覧」のダブルクリックを追加する　start
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
        /// 20141128 Houkakou 「紐付状況一覧」のダブルクリックを追加する　end

        //VAN 20210507 #148581 S
        #region 画面遷移
        /// <summary>
        /// 一次・二次マニ画面遷移
        /// </summary>
        /// <param name="isIchiji">true : 一次, false : 二次</param>
        public void FormChangesByManiKbn(bool isIchiji)
        {
            LogUtility.DebugMethodStart(isIchiji);

            try
            {
                //選んだデータ行を取得
                var targetRow = this.form.customDataGridView1.CurrentRow;

                WINDOW_TYPE WindowType = WINDOW_TYPE.NONE;

                string manifestKbn = string.Empty;
                string manifestNo = string.Empty;
                string system_id = string.Empty;
                string seq = string.Empty;
                string kanriId = string.Empty;
                string latestSeq = string.Empty;
                string maniKbnName = string.Empty;

                //選んだデータの中に廃棄区分、システムID、SEQ、管理ID、最終SEQ、交付番号を取得する
                if (isIchiji)
                {
                    manifestKbn = Convert.ToString(targetRow.Cells[this.HIDDEN_HAIKI_KBN_CD_IJI].Value);
                    system_id = Convert.ToString(targetRow.Cells[this.HIDDEN_SYSTEM_ID_IJI].Value);
                    seq = Convert.ToString(targetRow.Cells[this.HIDDEN_SEQ_IJI].Value);
                    kanriId = Convert.ToString(targetRow.Cells[this.HIDDEN_KANRI_ID_IJI].Value);
                    latestSeq = Convert.ToString(targetRow.Cells[this.HIDDEN_LATEST_SEQ_IJI].Value);
                    maniKbnName = "一次";
                    manifestNo = Convert.ToString(targetRow.Cells[this.KOUFU_NO_IJI].Value);
                }
                else
                {
                    manifestKbn = Convert.ToString(targetRow.Cells[this.HIDDEN_HAIKI_KBN_CD_NIJI].Value);
                    system_id = Convert.ToString(targetRow.Cells[this.HIDDEN_SYSTEM_ID_NIJI].Value);
                    seq = Convert.ToString(targetRow.Cells[this.HIDDEN_SEQ_NIJI].Value);
                    kanriId = Convert.ToString(targetRow.Cells[this.HIDDEN_KANRI_ID_NIJI].Value);
                    latestSeq = Convert.ToString(targetRow.Cells[this.HIDDEN_LATEST_SEQ_NIJI].Value);
                    maniKbnName = "二次";
                    manifestNo = Convert.ToString(targetRow.Cells[this.KOUFU_NO_NIJI].Value);
                }

                //マニ区分が取得できない場合
                if (string.IsNullOrEmpty(manifestKbn))
                {
                    this.MsgBox.MessageBoxShowError(String.Format(MSG_ERR_MANI_DATA_NOT_FOUND, maniKbnName));
                    return;
                }

                switch (manifestKbn)
                {
                    case "1"://産廃（直行）
                    case "2"://建廃
                    case "3"://産廃（積替）

                        //SYSTEM_IDが取得できた場合の存在チェック。
                        DataTable dtPaper = new DataTable();
                        dto = new DTOClass();
                        dto.SYSTEM_ID = system_id;
                        dto.FIRST_MANIFEST_KBN = isIchiji ? "0" : "1";
                        dto.HAIKI_KBN_CD = manifestKbn;
                        dtPaper = this.daoIchiran.GetDataForEntity(dto);
                        if (dtPaper == null || dtPaper.Rows.Count == 0)
                        {
                            //データを取得できない場合
                            this.MsgBox.MessageBoxShow("E045");
                            return;
                        }
                        break;

                    case "4"://電子

                        //KANRI_IDが取得できた場合の存在チェック。
                        DataTable dtDenshi = new DataTable();
                        dto = new DTOClass();
                        dto.KANRI_ID = kanriId;
                        dto.FIRST_MANIFEST_KBN = isIchiji ? "0" : "1";
                        dto.HAIKI_KBN_CD = manifestKbn;
                        dtDenshi = this.daoIchiran.GetDataForEntity(dto);
                        if (dtDenshi == null || dtDenshi.Rows.Count == 0)
                        {
                            //データを取得できない場合
                            this.MsgBox.MessageBoxShow("E045");
                            return;
                        }
                        break;
                }

                // 画面ID取得
                var formID = string.Empty;
                switch (manifestKbn)
                {
                    //G119 産廃（直行）マニフェスト
                    case "1": formID = "G119"; break;
                    //G121 建廃マニフェスト
                    case "2": formID = "G121"; break;
                    //G120 産廃（積替）マニフェスト
                    case "3": formID = "G120"; break;
                    //G141 電子マニフェスト
                    case "4": formID = "G141"; break;
                }

                // 修正モードの場合権限チェック
                if (r_framework.Authority.Manager.CheckAuthority(formID, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // 修正モードに変更
                    WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                }
                else if (r_framework.Authority.Manager.CheckAuthority(formID, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    // 参照モードに変更
                    WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E158", "修正");
                    return;
                }

                //画面起動
                switch (manifestKbn)
                {
                    case "1"://G119 産廃（直行）マニフェスト
                        FormManager.OpenFormWithAuth("G119", WindowType, WindowType, "", system_id, "", WindowType);
                        break;

                    case "2"://G121 建廃マニフェスト
                        FormManager.OpenFormWithAuth("G121", WindowType, WindowType, "", system_id, "", WindowType);
                        break;

                    case "3"://G120 産廃（積替）マニフェスト
                        FormManager.OpenFormWithAuth("G120", WindowType, WindowType, "", system_id, "", WindowType);
                        break;

                    case "4"://G141 電子マニフェスト
                        FormManager.OpenFormWithAuth("G141", WindowType, WindowType, kanriId, string.Empty);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormChangesByManiKbn", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }

            LogUtility.DebugMethodEnd();

            return;
        }
        #endregion
        //VAN 20210507 #148581 E
    }
}