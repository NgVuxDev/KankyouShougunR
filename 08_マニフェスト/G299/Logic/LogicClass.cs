using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.PaperManifest.ManifestPattern.DAO;
using Shougun.Core.Common.BusinessCommon.Dto;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.PaperManifest.ManifestPattern
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
        private string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.ManifestPattern.Setting.ButtonSetting.xml";

        /// <summary>
        /// DAO
        /// </summary>
        public GetResultDaoCls dao_GetResult;
        public SetTMPEDaoCls dao_SetTMPE;
        public SetDPR18DaoCls dao_SetDPR18;
       
        /// <summary>
        /// DTO
        /// </summary>
        private TMPEDtoCls dto_TMPE;
        private DPR18DtoCls dto_DPR18;

        /// <summary>
        /// Form
        /// </summary>
        private UIHeader header = null;
        private UIForm form = null;
        private BusinessBaseForm footer = null;

        /// <summary>
        /// 作成したSQL
        /// </summary>
        public string createSql { get; set; }

        private MessageBoxShowLogic MsgBox;

        // 20140529 syunrei No.730 マニフェストパターン一覧 start
        #region 規定値フィールド

        /// <summary>
        /// マニ収集運搬更新Dao
        /// </summary>
        private PtUpnDaoCls PtUpnDao;

        /// <summary>
        /// マニ印字更新Dao
        /// </summary>
        private PtPrtDaoCls PtPrtDao;

        /// <summary>
        /// マニ印字明細更新Dao
        /// </summary>
        private PtDetailPrtDaoCls PtDetailPrtDao;

        /// <summary>
        /// マニ明細更新Dao
        /// </summary>
        private PtDetailDaoCls PtDetailDao;

        /// <summary>
        /// マニ印字_建廃_形状更新Dao
        /// </summary>
        private PtKeijyouDaoCls PtKeijyouDao;

        /// <summary>
        /// マニ印字_建廃_荷姿更新Dao
        /// </summary>
        private PtNisugataDaoCls PtNisugataDao;

        /// <summary>
        /// マニ印字_建廃_処分方法更新Dao
        /// </summary>
        private PtHouhouDaoCls PtHouhouDao;

        /// <summary>
        /// timesTamp
        /// </summary>
        private String delTimesTamp;
        #endregion


     
//        /// <summary>非表示列名(論理名)</summary>
//        public static string[] RemoveColumnsName = new[]{
//#region Columns
//"取引先CD"
//,"取引先名"
//,"排出事業者郵便番号"
//,"排出事業者電話番号"
//,"排出事業者住所"
//,"排出事業場郵便番号"
//,"排出事業場電話番号"
//,"排出事業場住所"
//,"備考"
//,"最終処分の場所（予定）区分"
//,"最終処分の場所（予定）業者CD"
//,"最終処分の場所（予定）現場CD"
//,"最終処分の場所（予定）現場名称"
//,"最終処分の場所（予定）郵便番号"
//,"最終処分の場所（予定）電話番号"
//,"最終処分の場所（予定）住所"
//,"処分受託者郵便番号"
//,"処分受託者電話番号"
//,"処分受託者住所"
//,"処分の受領者CD"
//,"処分の受領者名称"
//,"処分の受領担当者CD"
//,"処分の受領担当者名"
//,"処分受領日"
//,"処分の受託者CD"
//,"処分の受託者名称"
//,"処分担当者CD"
//,"処分担当者名"
//,"照合確認B1票"
//,"照合確認B2票"
//,"照合確認B4票"
//,"照合確認B6票"
//,"照合確認D票"
//,"照合確認E票"
//,"区間1：運搬受託者郵便番号"
//,"区間1：運搬受託者電話番号"
//,"区間1：運搬受託者住所"
//,"区間1：運搬方法CD"
//,"区間1：運搬方法名"
//,"区間1：車種CD"
//,"区間1：車種名"
//,"区間1：車輌CD"
//,"区間1：車輌名"
//,"区間1：積替保管有無"
//,"区間1：積替保管有無名称"
//,"区間1：運搬先区分"
//,"区間1：運搬先区分名"
//,"区間1：運搬先の事業者CD"
//,"区間1：運搬先の事業場CD"
//,"区間1：運搬先の事業場名称"
//,"区間1：運搬先の事業場郵便番号"
//,"区間1：運搬先の事業場電話番号"
//,"区間1：運搬先の事業場住所"
//,"区間1：運搬の受託者CD"
//,"区間1：運搬の受託者名称"
//,"区間1：運転者CD"
//,"区間1：運転者名"
//,"区間1：運搬終了年月日"
//,"区間2：運搬受託者CD"
//,"区間2：運搬受託者名称"
//,"区間2：運搬受託者郵便番号"
//,"区間2：運搬受託者電話番号"
//,"区間2：運搬受託者住所"
//,"区間2：運搬方法CD"
//,"区間2：運搬方法名"
//,"区間2：車種CD"
//,"区間2：車種名"
//,"区間2：車輌CD"
//,"区間2：車輌名"
//,"区間2：積替保管有無"
//,"区間2：積替保管有無名称"
//,"区間2：運搬先区分"
//,"区間2：運搬先区分名"
//,"区間2：運搬先の事業者CD"
//,"区間2：運搬先の事業場CD"
//,"区間2：運搬先の事業場名称"
//,"区間2：運搬先の事業場郵便番号"
//,"区間2：運搬先の事業場電話番号"
//,"区間2：運搬先の事業場住所"
//,"区間2：運搬の受託者CD"
//,"区間2：運搬の受託者名称"
//,"区間2：運転者CD"
//,"区間2：運転者名"
//,"区間2：運搬終了年月日"
//,"区間3：運搬受託者CD"
//,"区間3：運搬受託者名称"
//,"区間3：運搬受託者郵便番号"
//,"区間3：運搬受託者電話番号"
//,"区間3：運搬受託者住所"
//,"区間3：運搬方法CD"
//,"区間3：運搬方法名"
//,"区間3：車種CD"
//,"区間3：車種名"
//,"区間3：車輌CD"
//,"区間3：車輌名"
//,"区間3：積替保管有無"
//,"区間3：積替保管有無名称"
//,"区間3：運搬先区分"
//,"区間3：運搬先区分名"
//,"区間3：運搬先の事業者CD"
//,"区間3：運搬先の事業場CD"
//,"区間3：運搬先の事業場名称"
//,"区間3：運搬先の事業場郵便番号"
//,"区間3：運搬先の事業場電話番号"
//,"区間3：運搬先の事業場住所"
//,"区間3：運搬の受託者CD"
//,"区間3：運搬の受託者名称"
//,"区間3：運転者CD"
//,"区間3：運転者名"
//,"区間3：運搬終了年月日"
//,"廃棄物名称CD"
//,"廃棄物名称","荷姿CD"
//,"荷姿名","減容後数量（表示のみ）"
//,"積替保管場郵便番号" //積替保管
//,"積替保管場電話番号"
//,"積替保管場住所"
//#endregion
//        };

//        /// <summary>非表示列名(物理名)</summary>
//        private static string[] removeColumnsPhysical = new[]{
//#region Columns
// "LIST_REGIST_KBN"
//,"HAIKI_KBN_CD"
//,"FIRST_MANIFEST_KBN"
//,"PATTERN_NAME"
//,"PATTERN_FURIGANA"
//,"USE_DEFAULT_KBN"
//,"KYOTEN_CD"
//,"TORIHIKISAKI_CD"
//,"JIZEN_NUMBER"
//,"JIZEN_DATE"
//,"KOUFU_DATE"
//,"KOUFU_KBN"
//,"MANIFEST_ID"
//,"SEIRI_ID"
//,"KOUFU_TANTOUSHA"
//,"KOUFU_TANTOUSHA_SHOZOKU"
//,"HST_GYOUSHA_CD"
//,"HST_GYOUSHA_NAME"
//,"HST_GYOUSHA_POST"
//,"HST_GYOUSHA_TEL"
//,"HST_GYOUSHA_ADDRESS"
//,"HST_GENBA_CD"
//,"HST_GENBA_NAME"
//,"HST_GENBA_POST"
//,"HST_GENBA_TEL"
//,"HST_GENBA_ADDRESS"
//,"BIKOU"
//,"KONGOU_SHURUI_CD"
//,"HAIKI_SUU"
//,"HAIKI_UNIT_CD"
//,"TOTAL_SUU"
//,"TOTAL_KANSAN_SUU"
//,"TOTAL_GENNYOU_SUU"
//,"CHUUKAN_HAIKI_KBN"
//,"CHUUKAN_HAIKI"
//,"LAST_SBN_YOTEI_KBN"
//,"LAST_SBN_YOTEI_GYOUSHA_CD"
//,"LAST_SBN_YOTEI_GENBA_CD"
//,"LAST_SBN_YOTEI_GENBA_NAME"
//,"LAST_SBN_YOTEI_GENBA_POST"
//,"LAST_SBN_YOTEI_GENBA_TEL"
//,"LAST_SBN_YOTEI_GENBA_ADDRESS"
//,"SBN_GYOUSHA_CD"
//,"SBN_GYOUSHA_NAME"
//,"SBN_GYOUSHA_POST"
//,"SBN_GYOUSHA_TEL"
//,"SBN_GYOUSHA_ADDRESS"
//,"TMH_GYOUSHA_CD"
//,"TMH_GYOUSHA_NAME"
//,"TMH_GENBA_CD"
//,"TMH_GENBA_NAME"
//,"TMH_GENBA_POST"
//,"TMH_GENBA_TEL"
//,"TMH_GENBA_ADDRESS"
//,"YUUKA_KBN"
//,"YUUKA_SUU"
//,"YUUKA_UNIT_CD"
//,"SBN_JYURYOUSHA_CD"
//,"SBN_JYURYOUSHA_NAME"
//,"SBN_JYURYOU_TANTOU_CD"
//,"SBN_JYURYOU_TANTOU_NAME"
//,"SBN_JYURYOU_DATE"
//,"SBN_JYUTAKUSHA_CD"
//,"SBN_JYUTAKUSHA_NAME"
//,"SBN_TANTOU_CD"
//,"SBN_TANTOU_NAME"
//,"LAST_SBN_GYOUSHA_CD"
//,"LAST_SBN_GENBA_CD"
//,"LAST_SBN_GENBA_NAME"
//,"LAST_SBN_GENBA_POST"
//,"LAST_SBN_GENBA_TEL"
//,"LAST_SBN_GENBA_ADDRESS"
//,"LAST_SBN_GENBA_NUMBER"
//,"LAST_SBN_CHECK_NAME"
//,"CHECK_B1"
//,"CHECK_B2"
//,"CHECK_B4"
//,"CHECK_B6"
//,"CHECK_D"
//,"CHECK_E"
//,"RENKEI_DENSHU_KBN_CD"
//,"RENKEI_SYSTEM_ID"
//,"RENKEI_MEISAI_SYSTEM_ID"
//,"DELETE_FLG"
//#endregion
//        };

//        /// <summary>電子非表示列名(物理名)</summary>
//        private static string[] removeColumnsPhysicalDenshi = new[]{
//#region Columns
// "PATTERN_NAME"
//,"PATTERN_FURIGANA"
//,"MANIFEST_ID"
//,"MANIFEST_KBN"
//,"SHOUNIN_FLAG"
//,"HIKIWATASHI_DATE"
//,"UPN_ENDREP_FLAG"
//,"SBN_ENDREP_FLAG"
//,"LAST_SBN_ENDREP_FLAG"
//,"KAKIN_DATE"
//,"REGI_DATE"
//,"UPN_SBN_REP_LIMIT_DATE"
//,"LAST_SBN_REP_LIMIT_DATE"
//,"RESV_LIMIT_DATE"
//,"SBN_ENDREP_KBN"
//,"HST_SHA_EDI_MEMBER_ID"
//,"HST_SHA_NAME"
//,"HST_SHA_POST"
//,"HST_SHA_ADDRESS1"
//,"HST_SHA_ADDRESS2"
//,"HST_SHA_ADDRESS3"
//,"HST_SHA_ADDRESS4"
//,"HST_SHA_TEL"
//,"HST_SHA_FAX"
//,"HST_JOU_NAME"
//,"HST_JOU_POST_NO"
//,"HST_JOU_ADDRESS1"
//,"HST_JOU_ADDRESS2"
//,"HST_JOU_ADDRESS3"
//,"HST_JOU_ADDRESS4"
//,"HST_JOU_TEL"
//,"REGI_TAN"
//,"HIKIWATASHI_TAN_NAME"
//,"HAIKI_DAI_CODE"
//,"HAIKI_CHU_CODE"
//,"HAIKI_SHO_CODE"
//,"HAIKI_SAI_CODE"
//,"HAIKI_BUNRUI"
//,"HAIKI_SHURUI"
//,"HAIKI_NAME"
//,"HAIKI_SUU"
//,"HAIKI_UNIT_CODE"
//,"SUU_KAKUTEI_CODE"
//,"HAIKI_KAKUTEI_SUU"
//,"HAIKI_KAKUTEI_UNIT_CODE"
//,"NISUGATA_CODE"
//,"NISUGATA_NAME"
//,"NISUGATA_SUU"
//,"SBN_SHA_MEMBER_ID"
//,"SBN_SHA_NAME"
//,"SBN_SHA_POST"
//,"SBN_SHA_ADDRESS1"
//,"SBN_SHA_ADDRESS2"
//,"SBN_SHA_ADDRESS3"
//,"SBN_SHA_ADDRESS4"
//,"SBN_SHA_TEL"
//,"SBN_SHA_FAX"
//,"SBN_SHA_KYOKA_ID"
//,"SAI_SBN_SHA_MEMBER_ID"
//,"SAI_SBN_SHA_NAME"
//,"SAI_SBN_SHA_POST"
//,"SAI_SBN_SHA_ADDRESS1"
//,"SAI_SBN_SHA_ADDRESS2"
//,"SAI_SBN_SHA_ADDRESS3"
//,"SAI_SBN_SHA_ADDRESS4"
//,"SAI_SBN_SHA_TEL"
//,"SAI_SBN_SHA_FAX"
//,"SAI_SBN_SHA_KYOKA_ID"
//,"SBN_WAY_CODE"
//,"SBN_WAY_NAME"
//,"SBN_SHOUNIN_FLAG"
//,"SBN_END_DATE"
//,"HAIKI_IN_DATE"
//,"RECEPT_SUU"
//,"RECEPT_UNIT_CODE"
//,"UPN_TAN_NAME"
//,"CAR_NO"
//,"REP_TAN_NAME"
//,"SBN_TAN_NAME"
//,"SBN_END_REP_DATE"
//,"SBN_REP_BIKOU"
//,"KENGEN_CODE"
//,"LAST_SBN_JOU_KISAI_FLAG"
//,"FIRST_MANIFEST_FLAG"
//,"LAST_SBN_END_DATE"
//,"LAST_SBN_END_REP_DATE"
//,"SHUSEI_DATE"
//,"CANCEL_FLAG"
//,"CANCEL_DATE"
//,"LAST_UPDATE_DATE"
//,"YUUGAI_CNT"
//,"UPN_ROUTE_CNT"
//,"LAST_SBN_PLAN_CNT"
//,"LAST_SBN_CNT"
//,"RENRAKU_CNT"
//,"BIKOU_CNT"
//,"FIRST_MANIFEST_CNT"
//,"HST_GYOUSHA_CD"
//,"HST_GENBA_CD"
//,"SBN_GYOUSHA_CD"
//,"SBN_GENBA_CD"
//,"NO_REP_SBN_EDI_MEMBER_ID"
//,"HAIKI_NAME_CD"
//,"SBN_HOUHOU_CD"
//,"HOUKOKU_TANTOUSHA_CD"
//,"SBN_TANTOUSHA_CD"
//,"UPN_TANTOUSHA_CD"
//,"SHARYOU_CD"
//,"KANSAN_SUU"
//,"CREATE_USER"
//,"CREATE_DATE"
//,"CREATE_PC"
//,"UPDATE_USER"
//,"UPDATE_DATE"
//,"UPDATE_PC"
//,"DELETE_FLG"
//#endregion
        //        };
        // 20140529 syunrei No.730 マニフェストパターン一覧 end
        #endregion


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
        /// 検索結果(マニフェストパターン　表示用)
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 更新条件(マニフェストパターン)
        /// </summary>
        public List<T_MANIFEST_PT_ENTRY> TmpeList { get; set; }

        /// <summary>
        /// 更新条件(電子マニフェストパターン)
        /// </summary>
        public List<DT_PT_R18> Dpr18List { get; set; }

        private Control[] allControl;

        internal Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic mlogic { get; private set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            //DTO
            this.dto_TMPE = new TMPEDtoCls();
            this.dto_DPR18 = new DPR18DtoCls();

            //DAO
            this.dao_GetResult = DaoInitUtility.GetComponent<DAO.GetResultDaoCls>();
            this.dao_SetTMPE = DaoInitUtility.GetComponent<DAO.SetTMPEDaoCls>();
            this.dao_SetDPR18 = DaoInitUtility.GetComponent<DAO.SetDPR18DaoCls>();


            // 20140529 syunrei No.730 マニフェストパターン一覧 start
            this.PtUpnDao = DaoInitUtility.GetComponent<PtUpnDaoCls>();
            this.PtPrtDao = DaoInitUtility.GetComponent<PtPrtDaoCls>();
            this.PtDetailPrtDao = DaoInitUtility.GetComponent<PtDetailPrtDaoCls>();
            this.PtKeijyouDao = DaoInitUtility.GetComponent<PtKeijyouDaoCls>();
            this.PtNisugataDao = DaoInitUtility.GetComponent<PtNisugataDaoCls>();
            this.PtHouhouDao = DaoInitUtility.GetComponent<PtHouhouDaoCls>();
            this.PtDetailDao = DaoInitUtility.GetComponent<PtDetailDaoCls>();
            // 20140529 syunrei No.730 マニフェストパターン一覧 end

            this.SearchResult = new DataTable();

            this.mlogic = new Common.BusinessCommon.Logic.ManifestoLogic();
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd(true);
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            UIHeader targetHeader = (UIHeader)parentForm.headerForm;
            this.header = targetHeader;

            //フッターの初期化
            BusinessBaseForm targetFooter = (BusinessBaseForm)parentForm;
            this.footer = targetFooter;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();

            //親フォームのボタン表示
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

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

            LogUtility.DebugMethodEnd();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();            
            var parentForm = (BusinessBaseForm)this.form.Parent;
            //155786 S
            parentForm.bt_func1.Click += new EventHandler(this.form.bt_func1_Click);
            switch (this.form.ListRegistKbn)
            {
                case "true":
                    parentForm.bt_func1.Enabled = false;
                    break;

                case "false":
                default:
                    parentForm.bt_func1.Enabled = true;
                    //parentForm.bt_func9.Enabled = false;//一旦非表示
                    break;
            }
            parentForm.bt_func2.Click += new EventHandler(this.form.bt_func2_Click);
            parentForm.bt_func2.Enabled = false;
            //155786 E
            //適用ボタン(F3)イベント生成
            parentForm.bt_func3.Click += new EventHandler(this.form.bt_func3_Click);

            //削除ボタン(F4)イベント生成
            parentForm.bt_func4.Click += new EventHandler(this.form.bt_func4_Click);

            //条件クリアボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.bt_func7_Click);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);

            //規定登録ボタン(F9)イベント生成
            //155786 S
            //this.form.C_Regist(parentForm.bt_func9);
            //parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);
            ////parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;
            //switch (this.form.ListRegistKbn)
            //{
            //    case "true":
            //        parentForm.bt_func9.Enabled = false;
            //        break;

            //    case "false":
            //    default:
            //        parentForm.bt_func9.Enabled = true;
            //        //parentForm.bt_func9.Enabled = false;//一旦非表示
            //        break;
            //}
            //155786 E
            if (this.form.HaikiKbnCD.Equals("4"))
            {
                parentForm.bt_func9.Text = string.Empty;
                parentForm.bt_func9.Enabled = false;
                //159144 S
                parentForm.bt_func1.Enabled = false;
                parentForm.bt_func2.Enabled = false;          
                parentForm.bt_func1.Text = string.Empty;
                parentForm.bt_func2.Text = string.Empty;
                //159144 E
                //parentForm.bt_func9.Click -= new EventHandler(this.form.bt_func9_Click);
            }

            //並替移動ボタン(F10)イベント生成
            parentForm.bt_func10.Click += new EventHandler(this.form.bt_func10_Click);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            //【1】パターン一覧(1)イベント生成
            parentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);

            //【2】検索条件設定(2)イベント生成
            parentForm.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);

            // 20140529 syunrei No.730 マニフェストパターン一覧 start
           
            //プロパティ設定

            this.form.cantxt_HaisyutugyoshaCD.FocusOutCheckMethod.Clear();
            this.form.cantxt_HaisyutugenbaCD.FocusOutCheckMethod.Clear();
            this.form.cantxt_UnpangyoshaCD.FocusOutCheckMethod.Clear();
            this.form.cantxt_ShobungyoshaCD.FocusOutCheckMethod.Clear();
            this.form.cantxt_ShobunGenbaCD.FocusOutCheckMethod.Clear();
         
            //ポップアップ設定

            //排出
            Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                this.form.cantxt_HaisyutugyoshaCD,
                this.form.ctxt_HaisyutugyoshaName,
                this.form.cbtn_HaisyutuGyousyaSan,
                this.form.cantxt_HaisyutugenbaCD,
                this.form.ctxt_HaisyutugenbaName,
                this.form.cbtn_HaisyutuJigyoubaSan,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.KAMI,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.HAISHUTSU_NIZUMI_GYOUSHA,
                false, false, Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.HAISHUTSU_NIZUMI_GENBA,
                false, true, true);
            //運搬
            Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                this.form.cantxt_UnpangyoshaCD,
                this.form.ctxt_UnpangyoshaName,
                this.form.cbtn_UnpanJyutaku1San,
                null,
                null,
                null, Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.KAMI,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.UNPAN_JUTAKUSHA_KAISHA,
                false, false, Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.NONE,
                false, true, true);

            //処分
            Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                this.form.cantxt_ShobungyoshaCD,
                this.form.ctxt_ShobungyoshaName,
                this.form.casbtn_SyobunJyutaku,
                this.form.cantxt_ShobunGenbaCD,
                this.form.ctxt_ShobunGenbaName,
                this.form.cbtn_UnpanJyugyobaSan,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.KAMI,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.SHOBUN_NIOROSHI_GYOUSHA,
                false, false, Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.SHOBUN_NIOROSHI_GENBA,
                false, true, true);
            // 20140529 syunrei No.730 マニフェストパターン一覧 end
            LogUtility.DebugMethodEnd();
        }        

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                //DTO
                var TMPEDtoCls = new TMPEDtoCls();
                var DPR18DtoCls = new DPR18DtoCls();

                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;
                this.form.customDataGridView1.AllowUserToAddRows = false;                             //行の追加オプション(false)

                //アラート件数
                M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
                this.header.InitialNumberAlert = int.Parse(mSysInfo.ICHIRAN_ALERT_KENSUU.ToString());
                this.header.NumberAlert = this.header.InitialNumberAlert;

                this.form.customDataGridView1.TabIndex = 500;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        //アラート件数
        public Boolean CheckNumberAlert(Int32 Kensu)
        {
            LogUtility.DebugMethodStart();

            Boolean check = false;

            if (Int32.Parse(this.header.NumberAlert.ToString()) < Kensu)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                switch (msgLogic.MessageBoxShow("C025"))
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

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            int count = 0;
            var msgLogic = new MessageBoxShowLogic();
            this.form.isRegistErr = false;
            try
            {
                using (Transaction tran = new Transaction())
                {
                    switch (msgLogic.MessageBoxShow("C026"))
                    {
                        case DialogResult.Yes:
                            switch (this.form.HaikiKbnCD)
                            {
                                case "1"://産廃（直行）
                                case "2"://建廃
                                case "3"://産廃（積替）
                                    //マニパターンのデータ作成
                                    this.MakeRegister("LgcDelTMPE");
                                    if (TmpeList != null && TmpeList.Count() > 0)
                                    {
                                        // 20140613 syunrei EV004732_品目を複数セットしパターン作成した場合、パターンを削除できなくなる start
                                        //foreach (T_MANIFEST_PT_ENTRY tmpe in TmpeList)
                                        //{
                                        //    count = dao_SetTMPE.Update(tmpe);
                                        //}
                                        count = dao_SetTMPE.Update(TmpeList[0]);
                                        // 20140613 syunrei EV004732_品目を複数セットしパターン作成した場合、パターンを削除できなくなる end
                                    }
                                    break;

                                case "4"://電子
                                    //電子マニパターンのデータ作成
                                    this.MakeRegister("LgcDelDPR18");
                                    if (Dpr18List != null && Dpr18List.Count() > 0)
                                    {
                                        // 20140613 syunrei EV004732_品目を複数セットしパターン作成した場合、パターンを削除できなくなる start
                                        //foreach (DT_PT_R18 dpr18 in Dpr18List)
                                        //{
                                        //    count = dao_SetDPR18.Update(dpr18);
                                        //}
                                        count = dao_SetDPR18.Update(Dpr18List[0]);
                                        // 20140613 syunrei EV004732_品目を複数セットしパターン作成した場合、パターンを削除できなくなる end
                                    }
                                    break;
                            }
                            msgLogic.MessageBoxShow("I001", "削除");
                            switch (this.Search())
                            {
                                case 0:
                                    var messageShowLogic = new MessageBoxShowLogic();
                                    messageShowLogic.MessageBoxShow("C001");
                                    break;
                                case -1:
                                    return;
                            }
                            break;

                        case DialogResult.No:
                            return;
                    }
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDelete", ex);
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
                this.form.isRegistErr = true;
            }

            LogUtility.DebugMethodEnd();

            return;
        }

        /// <summary>
        ///物理削除処理
        /// </summary>
        public void PhysicalDelete()
        {
            LogUtility.DebugMethodStart();

            try
            {

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

        /// <summary>
        /// 登録
        /// </summary>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart();

            try
            {
                using (Transaction tran = new Transaction())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                   
                    switch (msgLogic.MessageBoxShow("C036"))
                    {
                        case DialogResult.Yes:

                            this.MakeRegister("InsTMPE");
                            if (TmpeList != null && TmpeList.Count() > 0)
                            {
                                foreach (T_MANIFEST_PT_ENTRY tmpe in TmpeList)
                                {
                                    int count = dao_SetTMPE.Insert(tmpe);
                                }
                            }

                            this.MakeRegister("LgcDelTMPE");
                            if (TmpeList != null && TmpeList.Count() > 0)
                            {
                                foreach (T_MANIFEST_PT_ENTRY tmpe in TmpeList)
                                {
                                    int count = dao_SetTMPE.Update(tmpe);
                                }
                            }

                            var messageShowLogic = new MessageBoxShowLogic();
                            messageShowLogic.MessageBoxShow("I001", "登録");

                            if (this.Search() == -1) { return; }
                            break;
                    }
                    tran.Commit();
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

                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 登録事前確認処理
        /// </summary>
        public Boolean CheckRegister()
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();
            return true;
        }

        /// <summary>
        /// 更新情報を作成する
        /// </summary>
        public void MakeRegister(String kbn)
        {
            LogUtility.DebugMethodStart(kbn);

            List<T_MANIFEST_PT_ENTRY> TmpList = new List<T_MANIFEST_PT_ENTRY>();
            T_MANIFEST_PT_ENTRY tmpe;

            List<DT_PT_R18> DprList = new List<DT_PT_R18>();
            DT_PT_R18 dpr18;

            //ユーザー情報
            String UsrName = System.Environment.UserName;
            UsrName = UsrName.Length > 16 ? UsrName.Substring(0, 16) : UsrName;

            int j = this.form.customDataGridView1.SelectedCells[0].RowIndex;
            Int64 systemID = Int64.Parse(this.form.customDataGridView1.Rows[j].Cells[0].Value.ToString());

            DataRow[] dataRows = SearchResult.Select("SYSTEM_ID =" + systemID);

            switch (kbn)
            {
                case "InsTMPE"://マニフェストパターン 挿入用データ
                    for (int i = 0; i < dataRows.Length; i++)
                    {
                        tmpe = new T_MANIFEST_PT_ENTRY();
                        tmpe.SYSTEM_ID = SqlInt64.Parse(dataRows[i]["SYSTEM_ID"].ToString());
                        tmpe.SEQ = SqlInt32.Parse(dataRows[i]["SEQ"].ToString()) + 1;
                        tmpe.LIST_REGIST_KBN = String.IsNullOrEmpty(dataRows[i]["LIST_REGIST_KBN"].ToString()) ? SqlBoolean.Null : SqlBoolean.Parse(dataRows[i]["LIST_REGIST_KBN"].ToString());
                        tmpe.HAIKI_KBN_CD = String.IsNullOrEmpty(dataRows[i]["HAIKI_KBN_CD"].ToString()) ? SqlInt16.Null : SqlInt16.Parse(dataRows[i]["HAIKI_KBN_CD"].ToString());
                        tmpe.FIRST_MANIFEST_KBN = String.IsNullOrEmpty(dataRows[i]["FIRST_MANIFEST_KBN"].ToString()) ? SqlBoolean.Null : SqlBoolean.Parse(dataRows[i]["FIRST_MANIFEST_KBN"].ToString());
                        tmpe.PATTERN_NAME = dataRows[i]["PATTERN_NAME"].ToString();
                        tmpe.PATTERN_FURIGANA = dataRows[i]["PATTERN_FURIGANA"].ToString();
                        tmpe.USE_DEFAULT_KBN = true;
                        tmpe.KYOTEN_CD = String.IsNullOrEmpty(dataRows[i]["KYOTEN_CD"].ToString()) ? SqlInt16.Null : SqlInt16.Parse(dataRows[i]["KYOTEN_CD"].ToString());
                        tmpe.TORIHIKISAKI_CD = dataRows[i]["TORIHIKISAKI_CD"].ToString();
                        tmpe.JIZEN_NUMBER = dataRows[i]["JIZEN_NUMBER"].ToString();
                        tmpe.JIZEN_DATE = String.IsNullOrEmpty(dataRows[i]["JIZEN_DATE"].ToString()) ? SqlDateTime.Null : SqlDateTime.Parse(dataRows[i]["JIZEN_DATE"].ToString());
                        tmpe.KOUFU_DATE = String.IsNullOrEmpty(dataRows[i]["KOUFU_DATE"].ToString()) ? SqlDateTime.Null : SqlDateTime.Parse(dataRows[i]["KOUFU_DATE"].ToString());
                        tmpe.KOUFU_KBN = String.IsNullOrEmpty(dataRows[i]["KOUFU_KBN"].ToString()) ? SqlInt16.Null : SqlInt16.Parse(dataRows[i]["KOUFU_KBN"].ToString());
                        tmpe.MANIFEST_ID = dataRows[i]["MANIFEST_ID"].ToString();
                        tmpe.SEIRI_ID = dataRows[i]["SEIRI_ID"].ToString();
                        tmpe.KOUFU_TANTOUSHA = dataRows[i]["KOUFU_TANTOUSHA"].ToString();
                        tmpe.KOUFU_TANTOUSHA_SHOZOKU = dataRows[i]["KOUFU_TANTOUSHA_SHOZOKU"].ToString();
                        tmpe.HST_GYOUSHA_CD = dataRows[i]["HST_GYOUSHA_CD"].ToString();
                        tmpe.HST_GYOUSHA_NAME = dataRows[i]["HST_GYOUSHA_NAME"].ToString();
                        tmpe.HST_GYOUSHA_POST = dataRows[i]["HST_GYOUSHA_POST"].ToString();
                        tmpe.HST_GYOUSHA_TEL = dataRows[i]["HST_GYOUSHA_TEL"].ToString();
                        tmpe.HST_GYOUSHA_ADDRESS = dataRows[i]["HST_GYOUSHA_ADDRESS"].ToString();
                        tmpe.HST_GENBA_CD = dataRows[i]["HST_GENBA_CD"].ToString();
                        tmpe.HST_GENBA_NAME = dataRows[i]["HST_GENBA_NAME"].ToString();
                        tmpe.HST_GENBA_POST = dataRows[i]["HST_GENBA_POST"].ToString();
                        tmpe.HST_GENBA_TEL = dataRows[i]["HST_GENBA_TEL"].ToString();
                        tmpe.HST_GENBA_ADDRESS = dataRows[i]["HST_GENBA_ADDRESS"].ToString();
                        tmpe.BIKOU = dataRows[i]["BIKOU"].ToString();
                        tmpe.KONGOU_SHURUI_CD = dataRows[i]["KONGOU_SHURUI_CD"].ToString();
                        tmpe.HAIKI_SUU = String.IsNullOrEmpty(dataRows[i]["HAIKI_SUU"].ToString()) ? SqlDecimal.Null : SqlDecimal.Parse(dataRows[i]["HAIKI_SUU"].ToString());
                        tmpe.HAIKI_UNIT_CD = String.IsNullOrEmpty(dataRows[i]["HAIKI_UNIT_CD"].ToString()) ? SqlInt16.Null : SqlInt16.Parse(dataRows[i]["HAIKI_UNIT_CD"].ToString());
                        tmpe.TOTAL_SUU = String.IsNullOrEmpty(dataRows[i]["TOTAL_SUU"].ToString()) ? SqlDecimal.Null : SqlDecimal.Parse(dataRows[i]["TOTAL_SUU"].ToString());
                        tmpe.TOTAL_KANSAN_SUU = String.IsNullOrEmpty(dataRows[i]["TOTAL_KANSAN_SUU"].ToString()) ? SqlDecimal.Null : SqlDecimal.Parse(dataRows[i]["TOTAL_KANSAN_SUU"].ToString());
                        tmpe.TOTAL_GENNYOU_SUU = String.IsNullOrEmpty(dataRows[i]["TOTAL_GENNYOU_SUU"].ToString()) ? SqlDecimal.Null : SqlDecimal.Parse(dataRows[i]["TOTAL_GENNYOU_SUU"].ToString());
                        tmpe.CHUUKAN_HAIKI_KBN = String.IsNullOrEmpty(dataRows[i]["CHUUKAN_HAIKI_KBN"].ToString()) ? SqlInt16.Null : SqlInt16.Parse(dataRows[i]["CHUUKAN_HAIKI_KBN"].ToString());
                        tmpe.CHUUKAN_HAIKI = dataRows[i]["CHUUKAN_HAIKI"].ToString();
                        tmpe.LAST_SBN_YOTEI_KBN = String.IsNullOrEmpty(dataRows[i]["LAST_SBN_YOTEI_KBN"].ToString()) ? SqlInt16.Null : SqlInt16.Parse(dataRows[i]["LAST_SBN_YOTEI_KBN"].ToString());
                        tmpe.LAST_SBN_YOTEI_GYOUSHA_CD = dataRows[i]["LAST_SBN_YOTEI_GYOUSHA_CD"].ToString();
                        tmpe.LAST_SBN_YOTEI_GENBA_CD = dataRows[i]["LAST_SBN_YOTEI_GENBA_CD"].ToString();
                        tmpe.LAST_SBN_YOTEI_GENBA_NAME = dataRows[i]["LAST_SBN_YOTEI_GENBA_NAME"].ToString();
                        tmpe.LAST_SBN_YOTEI_GENBA_POST = dataRows[i]["LAST_SBN_YOTEI_GENBA_POST"].ToString();
                        tmpe.LAST_SBN_YOTEI_GENBA_TEL = dataRows[i]["LAST_SBN_YOTEI_GENBA_TEL"].ToString();
                        tmpe.LAST_SBN_YOTEI_GENBA_ADDRESS = dataRows[i]["LAST_SBN_YOTEI_GENBA_ADDRESS"].ToString();
                        tmpe.SBN_GYOUSHA_CD = dataRows[i]["SBN_GYOUSHA_CD"].ToString();
                        tmpe.SBN_GYOUSHA_NAME = dataRows[i]["SBN_GYOUSHA_NAME"].ToString();
                        tmpe.SBN_GYOUSHA_POST = dataRows[i]["SBN_GYOUSHA_POST"].ToString();
                        tmpe.SBN_GYOUSHA_TEL = dataRows[i]["SBN_GYOUSHA_TEL"].ToString();
                        tmpe.SBN_GYOUSHA_ADDRESS = dataRows[i]["SBN_GYOUSHA_ADDRESS"].ToString();
                        tmpe.TMH_GYOUSHA_CD = dataRows[i]["TMH_GYOUSHA_CD"].ToString();
                        tmpe.TMH_GYOUSHA_NAME = dataRows[i]["TMH_GYOUSHA_NAME"].ToString();
                        tmpe.TMH_GENBA_CD = dataRows[i]["TMH_GENBA_CD"].ToString();
                        tmpe.TMH_GENBA_NAME = dataRows[i]["TMH_GENBA_NAME"].ToString();
                        tmpe.TMH_GENBA_POST = dataRows[i]["TMH_GENBA_POST"].ToString();
                        tmpe.TMH_GENBA_TEL = dataRows[i]["TMH_GENBA_TEL"].ToString();
                        tmpe.TMH_GENBA_ADDRESS = dataRows[i]["TMH_GENBA_ADDRESS"].ToString();
                        tmpe.YUUKA_KBN = String.IsNullOrEmpty(dataRows[i]["YUUKA_KBN"].ToString()) ? SqlInt16.Null : SqlInt16.Parse(dataRows[i]["YUUKA_KBN"].ToString());
                        tmpe.YUUKA_SUU = String.IsNullOrEmpty(dataRows[i]["YUUKA_SUU"].ToString()) ? SqlDecimal.Null : SqlDecimal.Parse(dataRows[i]["YUUKA_SUU"].ToString());
                        tmpe.YUUKA_UNIT_CD = String.IsNullOrEmpty(dataRows[i]["YUUKA_UNIT_CD"].ToString()) ? SqlInt16.Null : SqlInt16.Parse(dataRows[i]["YUUKA_UNIT_CD"].ToString());
                        tmpe.SBN_JYURYOUSHA_CD = dataRows[i]["SBN_JYURYOUSHA_CD"].ToString();
                        tmpe.SBN_JYURYOUSHA_NAME = dataRows[i]["SBN_JYURYOUSHA_NAME"].ToString();
                        tmpe.SBN_JYURYOU_TANTOU_CD = dataRows[i]["SBN_JYURYOU_TANTOU_CD"].ToString();
                        tmpe.SBN_JYURYOU_TANTOU_NAME = dataRows[i]["SBN_JYURYOU_TANTOU_NAME"].ToString();
                        tmpe.SBN_JYURYOU_DATE = String.IsNullOrEmpty(dataRows[i]["SBN_JYURYOU_DATE"].ToString()) ? SqlDateTime.Null : SqlDateTime.Parse(dataRows[i]["SBN_JYURYOU_DATE"].ToString());
                        tmpe.SBN_JYUTAKUSHA_CD = dataRows[i]["SBN_JYUTAKUSHA_CD"].ToString();
                        tmpe.SBN_JYUTAKUSHA_NAME = dataRows[i]["SBN_JYUTAKUSHA_NAME"].ToString();
                        tmpe.SBN_TANTOU_CD = dataRows[i]["SBN_TANTOU_CD"].ToString();
                        tmpe.SBN_TANTOU_NAME = dataRows[i]["SBN_TANTOU_NAME"].ToString();
                        tmpe.LAST_SBN_GYOUSHA_CD = dataRows[i]["LAST_SBN_GYOUSHA_CD"].ToString();
                        tmpe.LAST_SBN_GENBA_CD = dataRows[i]["LAST_SBN_GENBA_CD"].ToString();
                        tmpe.LAST_SBN_GENBA_NAME = dataRows[i]["LAST_SBN_GENBA_NAME"].ToString();
                        tmpe.LAST_SBN_GENBA_POST = dataRows[i]["LAST_SBN_GENBA_POST"].ToString();
                        tmpe.LAST_SBN_GENBA_TEL = dataRows[i]["LAST_SBN_GENBA_TEL"].ToString();
                        tmpe.LAST_SBN_GENBA_ADDRESS = dataRows[i]["LAST_SBN_GENBA_ADDRESS"].ToString();
                        tmpe.LAST_SBN_GENBA_NUMBER = dataRows[i]["LAST_SBN_GENBA_NUMBER"].ToString();
                        tmpe.LAST_SBN_CHECK_NAME = dataRows[i]["LAST_SBN_CHECK_NAME"].ToString();
                        tmpe.CHECK_B1 = String.IsNullOrEmpty(dataRows[i]["CHECK_B1"].ToString()) ? SqlDateTime.Null : SqlDateTime.Parse(dataRows[i]["CHECK_B1"].ToString());
                        tmpe.CHECK_B2 = String.IsNullOrEmpty(dataRows[i]["CHECK_B2"].ToString()) ? SqlDateTime.Null : SqlDateTime.Parse(dataRows[i]["CHECK_B2"].ToString());
                        tmpe.CHECK_B4 = String.IsNullOrEmpty(dataRows[i]["CHECK_B4"].ToString()) ? SqlDateTime.Null : SqlDateTime.Parse(dataRows[i]["CHECK_B4"].ToString());
                        tmpe.CHECK_B6 = String.IsNullOrEmpty(dataRows[i]["CHECK_B6"].ToString()) ? SqlDateTime.Null : SqlDateTime.Parse(dataRows[i]["CHECK_B6"].ToString());
                        tmpe.CHECK_D = String.IsNullOrEmpty(dataRows[i]["CHECK_D"].ToString()) ? SqlDateTime.Null : SqlDateTime.Parse(dataRows[i]["CHECK_D"].ToString());
                        tmpe.CHECK_E = String.IsNullOrEmpty(dataRows[i]["CHECK_E"].ToString()) ? SqlDateTime.Null : SqlDateTime.Parse(dataRows[i]["CHECK_E"].ToString());
                        tmpe.RENKEI_DENSHU_KBN_CD = String.IsNullOrEmpty(dataRows[i]["RENKEI_DENSHU_KBN_CD"].ToString()) ? SqlInt16.Null : SqlInt16.Parse(dataRows[i]["RENKEI_DENSHU_KBN_CD"].ToString());
                        tmpe.RENKEI_SYSTEM_ID = String.IsNullOrEmpty(dataRows[i]["RENKEI_SYSTEM_ID"].ToString()) ? SqlInt64.Null : SqlInt64.Parse(dataRows[i]["RENKEI_SYSTEM_ID"].ToString());
                        tmpe.RENKEI_MEISAI_SYSTEM_ID = String.IsNullOrEmpty(dataRows[i]["RENKEI_MEISAI_SYSTEM_ID"].ToString()) ? SqlInt64.Null : SqlInt64.Parse(dataRows[i]["RENKEI_MEISAI_SYSTEM_ID"].ToString());

                        DataBinderLogic<T_MANIFEST_PT_ENTRY> WHO = new DataBinderLogic<T_MANIFEST_PT_ENTRY>(tmpe);
                        WHO.SetSystemProperty(tmpe, false);

                        tmpe.DELETE_FLG = String.IsNullOrEmpty(dataRows[i]["DELETE_FLG"].ToString()) ? SqlBoolean.Null : SqlBoolean.Parse(dataRows[i]["DELETE_FLG"].ToString());
                        //TODO:排他制御の修正
                        Int32 data2 = Int32.Parse(dataRows[i]["TIME_STAMP"].ToString());
                        tmpe.TIME_STAMP = ConvertStrByte.In32ToByteArray(data2);

                        TmpList.Add(tmpe);
                    }
                    this.TmpeList = TmpList;
                    break;

                case "LgcDelTMPE"://マニフェストパターン　論理削除用データ
                    for (int i = 0; i < dataRows.Length; i++)
                    {
                        tmpe = new T_MANIFEST_PT_ENTRY();
                        tmpe.SYSTEM_ID = SqlInt64.Parse(dataRows[i]["SYSTEM_ID"].ToString());
                        tmpe.SEQ = SqlInt32.Parse(dataRows[i]["SEQ"].ToString());

                        DataBinderLogic<T_MANIFEST_PT_ENTRY> WHO = new DataBinderLogic<T_MANIFEST_PT_ENTRY>(tmpe);
                        WHO.SetSystemProperty(tmpe, true);

                        tmpe.DELETE_FLG = true;
                        //TODO:排他制御の修正
                        Int32 data2 = Int32.Parse(dataRows[i]["TIME_STAMP"].ToString());
                        tmpe.TIME_STAMP = ConvertStrByte.In32ToByteArray(data2);

                        TmpList.Add(tmpe);
                    }
                    this.TmpeList = TmpList;
                    break;

                case "LgcDelDPR18"://電子マニフェストパターン　論理削除用データ
                    for (int i = 0; i < dataRows.Length; i++)
                    {
                        dpr18 = new DT_PT_R18();
                        dpr18.SYSTEM_ID = SqlInt64.Parse(dataRows[i]["SYSTEM_ID"].ToString());
                        dpr18.SEQ = SqlInt32.Parse(dataRows[i]["SEQ"].ToString());

                        DataBinderLogic<DT_PT_R18> WHO = new DataBinderLogic<DT_PT_R18>(dpr18);
                        WHO.SetSystemProperty(dpr18, true);

                        dpr18.DELETE_FLG = true;
                        //TODO:排他制御の修正
                        Int32 data2 = Int32.Parse(dataRows[i]["TIME_STAMP"].ToString());
                        dpr18.TIME_STAMP = ConvertStrByte.In32ToByteArray(data2);

                        DprList.Add(dpr18);
                    }
                    this.Dpr18List = DprList;
                    break;

            }

            LogUtility.DebugMethodEnd(kbn);
        }

        /// <summary>
        /// 検索
        /// </summary>
        public Int32 Search()
        {
            LogUtility.DebugMethodStart();
            //MOD NHU 20211005 #155786 S
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.bt_func2.Enabled = false;
            //MOD NHU 20211005 #155786 E

            //20140616 syunrei EV004722_拠点について start
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            // 20140618 syunrei EV004902_電子マニフェストパターン一覧で検索時に拠点が必須になっている start
            if (string.IsNullOrEmpty(this.header.KYOTEN_CD.Text) && this.form.HaikiKbnCD!="4")
            // 20140618 syunrei EV004902_電子マニフェストパターン一覧で検索時に拠点が必須になっている end
            {
                msgLogic.MessageBoxShow("E001", "拠点CD");
                this.header.KYOTEN_CD.Focus();
                this.header.KYOTEN_CD.BackColor=System.Drawing.Color.Red;
                return 1;
            }
            //20140616 syunrei EV004722_拠点について end

            Int32 count = 0;
            String MOutputPatternSelect = string.Empty;
            try
            {
                //検索
                //switch (this.form.HaikiKbnCD)
                switch (this.form.HaikiKbnCD)
                {
                    case "1"://産廃（直行）
                    case "3"://建廃
                    case "2"://産廃（積替）
                    case "5"://紙マニフェスト
                    default:
                        count = this.Get_Search_TMPE
                                (string.Empty, string.Empty,
                                    this.form.ListRegistKbn.ToString(),
                                    this.form.HaikiKbnCD,
                                    this.form.FirstManifestKbn.ToString(),
                                    this.form.PATTERN_NAME.Text.ToString(),
                                    this.header.KYOTEN_CD.Text.ToString(),
                                    this.form.HST_GYOUSHA_NAME.Text.ToString(),
                                    this.form.HST_GENBA_NAME.Text.ToString(),
                                    "false",
                                    this.form.cantxt_HaisyutugyoshaCD.Text.ToString(),
                                    this.form.cantxt_HaisyutugenbaCD.Text.ToString(),
                                    this.form.cantxt_UnpangyoshaCD.Text.ToString(),
                                    this.form.HST_UNBAN_JUTAKU_NAME.Text.ToString(),
                                    this.form.cantxt_ShobungyoshaCD.Text.ToString(),
                                    this.form.HST_SHOUBU_JUTAKU_NAME.Text.ToString(),
                                    this.form.cantxt_ShobunGenbaCD.Text.ToString(),
                                    this.form.HST_SHOUBU_JIGYOU_NAME.Text.ToString()
                                    );

                        MOutputPatternSelect = this.selectQuery;
                        if (String.IsNullOrEmpty(MOutputPatternSelect))
                        {
                            this.form.customDataGridView1.Enabled = false;
                        }
                        else
                        {
                            this.form.customDataGridView1.Enabled = true;
                            this.Set_Search_TMPE();
                        }

                        break;

                    case "4"://電子
                        count = this.Get_Search_DPR18
                            (string.Empty, string.Empty,
                                this.form.ListRegistKbn.ToString(),
                                this.form.HaikiKbnCD.ToString(),
                                this.form.FirstManifestKbn.ToString(),
                                this.form.PATTERN_NAME.Text.ToString(),
                                this.header.KYOTEN_CD.Text.ToString(),
                                this.form.HST_GYOUSHA_NAME.Text.ToString(),
                                this.form.HST_GENBA_NAME.Text.ToString(),
                                "false",
                                this.form.cantxt_HaisyutugyoshaCD.Text.ToString(),
                                this.form.cantxt_HaisyutugenbaCD.Text.ToString(),
                                this.form.cantxt_UnpangyoshaCD.Text.ToString(),
                                this.form.HST_UNBAN_JUTAKU_NAME.Text.ToString(),
                                this.form.cantxt_ShobungyoshaCD.Text.ToString(),
                                this.form.HST_SHOUBU_JUTAKU_NAME.Text.ToString(),
                                this.form.cantxt_ShobunGenbaCD.Text.ToString(),
                                this.form.HST_SHOUBU_JIGYOU_NAME.Text.ToString()
                                );

                        MOutputPatternSelect = this.selectQuery;
                        if (String.IsNullOrEmpty(MOutputPatternSelect))
                        {
                            this.form.customDataGridView1.Enabled = false;
                        }
                        else
                        {
                            this.form.customDataGridView1.Enabled = true;
                            this.Set_Search_DPR18();
                        }

                        break;

                    //default:
                    //    return count;
                }
                this.form.customDataGridView1.Focus();


            }
            catch (Exception ex)
            {
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
                count = -1;
            }

            LogUtility.DebugMethodEnd();
            //取得件数
            return count;
        }

        /// <summary>
        /// マニフェストパターン　データ取得
        /// </summary>
        public int Get_Search_TMPE
            (String SYSTEM_ID, String SEQ, String LIST_REGIST_KBN,
             String HAIKI_KBN_CD, String FIRST_MANIFEST_KBN, String PATTERN_NAME,
             String KYOTEN_CD, 
             String HST_GYOUSHA_NAME, String HST_GENBA_NAME, String DELETE_FLG,
             String HST_GYOUSHA_CD, String HST_GENBA_CD,
             String HST_UNBAN_JUTAKU_CD,String HST_UNBAN_JUTAKU_NAME,
             String HST_SHOUBU_JUTAKU_CD, String HST_SHOUBU_JUTAKU_NAME,
             String HST_SHOUBU_JIGYOU_CD, String HST_SHOUBU_JIGYOU_NAME
            )
        {
            LogUtility.DebugMethodStart
                (SYSTEM_ID, SEQ, LIST_REGIST_KBN,
                 HAIKI_KBN_CD, FIRST_MANIFEST_KBN, PATTERN_NAME,
                 KYOTEN_CD, 
                 HST_GYOUSHA_NAME, HST_GENBA_NAME, DELETE_FLG,
                 HST_GYOUSHA_CD,HST_GENBA_CD,
                 HST_UNBAN_JUTAKU_CD, HST_UNBAN_JUTAKU_NAME,
                 HST_SHOUBU_JUTAKU_CD,  HST_SHOUBU_JUTAKU_NAME,
                 HST_SHOUBU_JIGYOU_CD,  HST_SHOUBU_JIGYOU_NAME
                 );

            //SQL文格納StringBuilder
            var sql = new StringBuilder();

            #region SELECT句
            
            sql.Append(" SELECT DISTINCT T_MANIFEST_PT_ENTRY.SYSTEM_ID ");
            sql.Append("      , T_MANIFEST_PT_ENTRY.SEQ ");
            sql.Append("      , T_MANIFEST_PT_ENTRY.LIST_REGIST_KBN");
            sql.Append("      , T_MANIFEST_PT_ENTRY.HAIKI_KBN_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.FIRST_MANIFEST_KBN");
            sql.Append("      , T_MANIFEST_PT_ENTRY.PATTERN_NAME ");
            sql.Append("      , T_MANIFEST_PT_ENTRY.PATTERN_FURIGANA");
            sql.Append("      , T_MANIFEST_PT_ENTRY.USE_DEFAULT_KBN ");
            sql.Append("      , T_MANIFEST_PT_ENTRY.KYOTEN_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.TORIHIKISAKI_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.JIZEN_NUMBER");
            sql.Append("      , T_MANIFEST_PT_ENTRY.JIZEN_DATE");
            sql.Append("      , T_MANIFEST_PT_ENTRY.KOUFU_DATE");
            sql.Append("      , T_MANIFEST_PT_ENTRY.KOUFU_KBN");
            sql.Append("      , T_MANIFEST_PT_ENTRY.MANIFEST_ID");
            sql.Append("      , T_MANIFEST_PT_ENTRY.SEIRI_ID");
            sql.Append("      , T_MANIFEST_PT_ENTRY.KOUFU_TANTOUSHA");
            sql.Append("      , T_MANIFEST_PT_ENTRY.KOUFU_TANTOUSHA_SHOZOKU");
            sql.Append("      , T_MANIFEST_PT_ENTRY.HST_GYOUSHA_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.HST_GYOUSHA_NAME");
            sql.Append("      , T_MANIFEST_PT_ENTRY.HST_GYOUSHA_POST");
            sql.Append("      , T_MANIFEST_PT_ENTRY.HST_GYOUSHA_TEL");
            sql.Append("      , T_MANIFEST_PT_ENTRY.HST_GYOUSHA_ADDRESS");
            sql.Append("      , T_MANIFEST_PT_ENTRY.HST_GENBA_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.HST_GENBA_NAME");
            sql.Append("      , T_MANIFEST_PT_ENTRY.HST_GENBA_POST");
            sql.Append("      , T_MANIFEST_PT_ENTRY.HST_GENBA_TEL");
            sql.Append("      , T_MANIFEST_PT_ENTRY.HST_GENBA_ADDRESS");
            sql.Append("      , T_MANIFEST_PT_ENTRY.BIKOU");
            sql.Append("      , T_MANIFEST_PT_ENTRY.KONGOU_SHURUI_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.HAIKI_SUU");
            sql.Append("      , T_MANIFEST_PT_ENTRY.HAIKI_UNIT_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.TOTAL_SUU");
            sql.Append("      , T_MANIFEST_PT_ENTRY.TOTAL_KANSAN_SUU");
            sql.Append("      , T_MANIFEST_PT_ENTRY.TOTAL_GENNYOU_SUU");
            sql.Append("      , T_MANIFEST_PT_ENTRY.CHUUKAN_HAIKI_KBN");
            sql.Append("      , T_MANIFEST_PT_ENTRY.CHUUKAN_HAIKI");
            sql.Append("      , T_MANIFEST_PT_ENTRY.LAST_SBN_YOTEI_KBN");
            sql.Append("      , T_MANIFEST_PT_ENTRY.LAST_SBN_YOTEI_GYOUSHA_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.LAST_SBN_YOTEI_GENBA_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.LAST_SBN_YOTEI_GENBA_NAME");
            sql.Append("      , T_MANIFEST_PT_ENTRY.LAST_SBN_YOTEI_GENBA_POST");
            sql.Append("      , T_MANIFEST_PT_ENTRY.LAST_SBN_YOTEI_GENBA_TEL");
            sql.Append("      , T_MANIFEST_PT_ENTRY.LAST_SBN_YOTEI_GENBA_ADDRESS");
            sql.Append("      , T_MANIFEST_PT_ENTRY.SBN_GYOUSHA_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.SBN_GYOUSHA_NAME");
            sql.Append("      , T_MANIFEST_PT_ENTRY.SBN_GYOUSHA_POST");
            sql.Append("      , T_MANIFEST_PT_ENTRY.SBN_GYOUSHA_TEL");
            sql.Append("      , T_MANIFEST_PT_ENTRY.SBN_GYOUSHA_ADDRESS");
            sql.Append("      , T_MANIFEST_PT_ENTRY.TMH_GYOUSHA_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.TMH_GYOUSHA_NAME");
            sql.Append("      , T_MANIFEST_PT_ENTRY.TMH_GENBA_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.TMH_GENBA_NAME");
            sql.Append("      , T_MANIFEST_PT_ENTRY.TMH_GENBA_POST");
            sql.Append("      , T_MANIFEST_PT_ENTRY.TMH_GENBA_TEL");
            sql.Append("      , T_MANIFEST_PT_ENTRY.TMH_GENBA_ADDRESS");
            sql.Append("      , T_MANIFEST_PT_ENTRY.YUUKA_KBN");
            sql.Append("      , T_MANIFEST_PT_ENTRY.YUUKA_SUU");
            sql.Append("      , T_MANIFEST_PT_ENTRY.YUUKA_UNIT_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.SBN_JYURYOUSHA_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.SBN_JYURYOUSHA_NAME");
            sql.Append("      , T_MANIFEST_PT_ENTRY.SBN_JYURYOU_TANTOU_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.SBN_JYURYOU_TANTOU_NAME");
            sql.Append("      , T_MANIFEST_PT_ENTRY.SBN_JYURYOU_DATE");
            sql.Append("      , T_MANIFEST_PT_ENTRY.SBN_JYUTAKUSHA_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.SBN_JYUTAKUSHA_NAME");
            sql.Append("      , T_MANIFEST_PT_ENTRY.SBN_TANTOU_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.SBN_TANTOU_NAME");
            sql.Append("      , T_MANIFEST_PT_ENTRY.LAST_SBN_GYOUSHA_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.LAST_SBN_GENBA_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.LAST_SBN_GENBA_NAME");
            sql.Append("      , T_MANIFEST_PT_ENTRY.LAST_SBN_GENBA_POST");
            sql.Append("      , T_MANIFEST_PT_ENTRY.LAST_SBN_GENBA_TEL");
            sql.Append("      , T_MANIFEST_PT_ENTRY.LAST_SBN_GENBA_ADDRESS");
            sql.Append("      , T_MANIFEST_PT_ENTRY.LAST_SBN_GENBA_NUMBER");
            sql.Append("      , T_MANIFEST_PT_ENTRY.LAST_SBN_CHECK_NAME");
            sql.Append("      , T_MANIFEST_PT_ENTRY.CHECK_B1");
            sql.Append("      , T_MANIFEST_PT_ENTRY.CHECK_B2");
            sql.Append("      , T_MANIFEST_PT_ENTRY.CHECK_B4");
            sql.Append("      , T_MANIFEST_PT_ENTRY.CHECK_B6");
            sql.Append("      , T_MANIFEST_PT_ENTRY.CHECK_D");
            sql.Append("      , T_MANIFEST_PT_ENTRY.CHECK_E");
            sql.Append("      , T_MANIFEST_PT_ENTRY.RENKEI_DENSHU_KBN_CD");
            sql.Append("      , T_MANIFEST_PT_ENTRY.RENKEI_SYSTEM_ID");
            sql.Append("      , T_MANIFEST_PT_ENTRY.RENKEI_MEISAI_SYSTEM_ID");
            sql.Append("      , T_MANIFEST_PT_ENTRY.DELETE_FLG ");

            //TODO:排他制御の修正
            sql.Append("      , CAST(T_MANIFEST_PT_ENTRY.TIME_STAMP AS int ) AS TIME_STAMP ");         

            //sql.Append(this.selectQuery);
             String MOutputPatternSelect = this.selectQuery;
             if (String.IsNullOrEmpty(MOutputPatternSelect))
             {
             }
             else
             {
                 sql.Append(", ");
                 sql.Append(MOutputPatternSelect);
             }

            #endregion

            #region FROM句
            //マニフェストパターン
            sql.Append(" FROM ( ");
            sql.Append(" SELECT TMPE_COL.SYSTEM_ID ");
            sql.Append("      , TMPE_COL.SEQ ");
            sql.Append("      , TMPE_COL.LIST_REGIST_KBN ");
            sql.Append("      , TMPE_COL.HAIKI_KBN_CD ");
            sql.Append("      , CASE TMPE_COL.HAIKI_KBN_CD WHEN 1 THEN '直行' WHEN 2 THEN '建廃' WHEN 3 THEN '積替' ELSE '' END AS HAIKI_KBN_NAME ");
            sql.Append("      , TMPE_COL.FIRST_MANIFEST_KBN ");
            sql.Append("      , CASE TMPE_COL.FIRST_MANIFEST_KBN WHEN 'false' THEN '一次' WHEN 'true' THEN '二次' ELSE '' END AS FIRST_MANIFEST_KBN_NAME ");
            sql.Append("      , TMPE_COL.PATTERN_NAME ");
            sql.Append("      , TMPE_COL.PATTERN_FURIGANA ");
            sql.Append("      , TMPE_COL.USE_DEFAULT_KBN ");
            sql.Append("      , TMPE_COL.KYOTEN_CD ");
            sql.Append("      , TMPE_COL.TORIHIKISAKI_CD ");
            sql.Append("      , TMPE_COL.JIZEN_NUMBER ");
            sql.Append("      , TMPE_COL.JIZEN_DATE ");
            sql.Append("      , TMPE_COL.KOUFU_DATE ");
            sql.Append("      , TMPE_COL.KOUFU_KBN ");
            sql.Append("      , TMPE_COL.MANIFEST_ID ");
            sql.Append("      , TMPE_COL.SEIRI_ID ");
            sql.Append("      , TMPE_COL.KOUFU_TANTOUSHA ");
            sql.Append("      , TMPE_COL.KOUFU_TANTOUSHA_SHOZOKU ");
            sql.Append("      , TMPE_COL.HST_GYOUSHA_CD ");
            sql.Append("      , RTRIM(SUBSTRING(TMPE_COL.HST_GYOUSHA_NAME,1,40)) + SUBSTRING(TMPE_COL.HST_GYOUSHA_NAME,41,40) AS HST_GYOUSHA_NAME ");
            sql.Append("      , TMPE_COL.HST_GYOUSHA_POST ");
            sql.Append("      , TMPE_COL.HST_GYOUSHA_TEL ");
            sql.Append("      , TMPE_COL.HST_GYOUSHA_ADDRESS ");
            sql.Append("      , TMPE_COL.HST_GENBA_CD ");
            sql.Append("      , RTRIM(SUBSTRING(TMPE_COL.HST_GENBA_NAME,1,40)) + SUBSTRING(TMPE_COL.HST_GENBA_NAME,41,40) AS HST_GENBA_NAME ");
            sql.Append("      , TMPE_COL.HST_GENBA_POST ");
            sql.Append("      , TMPE_COL.HST_GENBA_TEL ");
            sql.Append("      , TMPE_COL.HST_GENBA_ADDRESS ");
            sql.Append("      , TMPE_COL.BIKOU ");
            sql.Append("      , TMPE_COL.KONGOU_SHURUI_CD ");
            sql.Append("      , TMPE_COL.HAIKI_SUU ");
            sql.Append("      , TMPE_COL.HAIKI_UNIT_CD ");
            sql.Append("      , TMPE_COL.TOTAL_SUU ");
            sql.Append("      , TMPE_COL.TOTAL_KANSAN_SUU ");
            sql.Append("      , TMPE_COL.TOTAL_GENNYOU_SUU ");
            sql.Append("      , TMPE_COL.CHUUKAN_HAIKI_KBN ");
            sql.Append("      , TMPE_COL.CHUUKAN_HAIKI ");
            sql.Append("      , TMPE_COL.LAST_SBN_YOTEI_KBN ");
            sql.Append("      , TMPE_COL.LAST_SBN_YOTEI_GYOUSHA_CD ");
            sql.Append("      , TMPE_COL.LAST_SBN_YOTEI_GENBA_CD ");
            sql.Append("      , TMPE_COL.LAST_SBN_YOTEI_GENBA_NAME ");
            sql.Append("      , TMPE_COL.LAST_SBN_YOTEI_GENBA_POST ");
            sql.Append("      , TMPE_COL.LAST_SBN_YOTEI_GENBA_TEL ");
            sql.Append("      , TMPE_COL.LAST_SBN_YOTEI_GENBA_ADDRESS ");
            sql.Append("      , TMPE_COL.SBN_GYOUSHA_CD ");
            sql.Append("      , TMPE_COL.SBN_GYOUSHA_NAME ");
            sql.Append("      , TMPE_COL.SBN_GYOUSHA_POST ");
            sql.Append("      , TMPE_COL.SBN_GYOUSHA_TEL ");
            sql.Append("      , TMPE_COL.SBN_GYOUSHA_ADDRESS ");
            sql.Append("      , TMPE_COL.TMH_GYOUSHA_CD ");
            sql.Append("      , TMPE_COL.TMH_GYOUSHA_NAME ");
            sql.Append("      , TMPE_COL.TMH_GENBA_CD ");
            sql.Append("      , TMPE_COL.TMH_GENBA_NAME ");
            sql.Append("      , TMPE_COL.TMH_GENBA_POST ");
            sql.Append("      , TMPE_COL.TMH_GENBA_TEL ");
            sql.Append("      , TMPE_COL.TMH_GENBA_ADDRESS ");
            sql.Append("      , TMPE_COL.YUUKA_KBN ");
            sql.Append("      , TMPE_COL.YUUKA_SUU ");
            sql.Append("      , TMPE_COL.YUUKA_UNIT_CD ");
            sql.Append("      , TMPE_COL.SBN_JYURYOUSHA_CD ");
            sql.Append("      , TMPE_COL.SBN_JYURYOUSHA_NAME ");
            sql.Append("      , TMPE_COL.SBN_JYURYOU_TANTOU_CD ");
            sql.Append("      , TMPE_COL.SBN_JYURYOU_TANTOU_NAME ");
            sql.Append("      , TMPE_COL.SBN_JYURYOU_DATE ");
            sql.Append("      , TMPE_COL.SBN_JYUTAKUSHA_CD ");
            sql.Append("      , TMPE_COL.SBN_JYUTAKUSHA_NAME ");
            sql.Append("      , TMPE_COL.SBN_TANTOU_CD ");
            sql.Append("      , TMPE_COL.SBN_TANTOU_NAME ");
            sql.Append("      , TMPE_COL.LAST_SBN_GYOUSHA_CD ");
            sql.Append("      , TMPE_COL.LAST_SBN_GENBA_CD ");
            sql.Append("      , TMPE_COL.LAST_SBN_GENBA_NAME ");
            sql.Append("      , TMPE_COL.LAST_SBN_GENBA_POST ");
            sql.Append("      , TMPE_COL.LAST_SBN_GENBA_TEL ");
            sql.Append("      , TMPE_COL.LAST_SBN_GENBA_ADDRESS ");
            sql.Append("      , TMPE_COL.LAST_SBN_GENBA_NUMBER ");
            sql.Append("      , TMPE_COL.LAST_SBN_CHECK_NAME ");
            sql.Append("      , TMPE_COL.CHECK_B1 ");
            sql.Append("      , TMPE_COL.CHECK_B2 ");
            sql.Append("      , TMPE_COL.CHECK_B4 ");
            sql.Append("      , TMPE_COL.CHECK_B6 ");
            sql.Append("      , TMPE_COL.CHECK_D ");
            sql.Append("      , TMPE_COL.CHECK_E ");
            sql.Append("      , TMPE_COL.RENKEI_DENSHU_KBN_CD ");
            sql.Append("      , TMPE_COL.RENKEI_SYSTEM_ID ");
            sql.Append("      , TMPE_COL.RENKEI_MEISAI_SYSTEM_ID ");
            sql.Append("      , TMPE_COL.CREATE_USER ");
            sql.Append("      , TMPE_COL.CREATE_DATE ");
            sql.Append("      , TMPE_COL.CREATE_PC ");
            sql.Append("      , TMPE_COL.UPDATE_USER ");
            sql.Append("      , TMPE_COL.UPDATE_DATE ");
            sql.Append("      , TMPE_COL.UPDATE_PC ");
            sql.Append("      , TMPE_COL.DELETE_FLG ");
            sql.Append("      , TMPE_COL.TIME_STAMP ");
            sql.Append("   FROM T_MANIFEST_PT_ENTRY TMPE_COL WITH(NOLOCK) ");
            sql.Append(" ) AS T_MANIFEST_PT_ENTRY ");

            //拠点
            sql.Append(" LEFT OUTER JOIN M_KYOTEN WITH(NOLOCK) ");
            sql.Append("   ON T_MANIFEST_PT_ENTRY.KYOTEN_CD = M_KYOTEN.KYOTEN_CD ");

            //取引先
            sql.Append(" LEFT OUTER JOIN M_TORIHIKISAKI WITH(NOLOCK) ");
            sql.Append("   ON T_MANIFEST_PT_ENTRY.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD ");

            #region 区間1

            //区間1：マニフェストパターン運搬
            sql.Append(" LEFT OUTER JOIN ( ");
            sql.Append(" SELECT SYSTEM_ID ");
            sql.Append(" , SEQ ");
            sql.Append(" , UPN_ROUTE_NO ");
            sql.Append(" , UPN_GYOUSHA_CD ");
            sql.Append(" , UPN_GYOUSHA_NAME ");
            sql.Append(" , UPN_GYOUSHA_POST ");
            sql.Append(" , UPN_GYOUSHA_TEL ");
            sql.Append(" , UPN_GYOUSHA_ADDRESS ");
            sql.Append(" , UPN_HOUHOU_CD ");
            sql.Append(" , SHASHU_CD ");
            sql.Append(" , SHARYOU_CD ");
            sql.Append(" , TMH_KBN ");
            sql.Append(" , CASE TMH_KBN WHEN 1 THEN '有' WHEN 2 THEN '無' ELSE '' END AS TMH_KBN_NAME ");
            sql.Append(" , UPN_SAKI_KBN ");
            sql.Append(" , CASE UPN_SAKI_KBN WHEN 1 THEN '処分施設' WHEN 2 THEN '積替保管' ELSE '' END AS UPN_SAKI_KBN_NAME ");
            sql.Append(" , UPN_SAKI_GYOUSHA_CD ");
            sql.Append(" , UPN_SAKI_GENBA_CD ");
            sql.Append(" , UPN_SAKI_GENBA_NAME ");
            sql.Append(" , UPN_SAKI_GENBA_POST ");
            sql.Append(" , UPN_SAKI_GENBA_TEL ");
            sql.Append(" , UPN_SAKI_GENBA_ADDRESS ");
            sql.Append(" , UPN_JYUTAKUSHA_CD ");
            sql.Append(" , UPN_JYUTAKUSHA_NAME ");
            sql.Append(" , UNTENSHA_CD ");
            sql.Append(" , UNTENSHA_NAME ");
            sql.Append(" , UPN_END_DATE ");
            sql.Append(" , YUUKA_SUU ");
            sql.Append(" , YUUKA_UNIT_CD ");
            sql.Append(" FROM T_MANIFEST_PT_UPN WITH(NOLOCK) ");
            sql.Append(" WHERE UPN_ROUTE_NO = 1 ");
            sql.Append(" ) T_MANIFEST_PT_UPN1 ");
            sql.Append("   ON T_MANIFEST_PT_ENTRY.SYSTEM_ID = T_MANIFEST_PT_UPN1.SYSTEM_ID ");
            sql.Append("  AND T_MANIFEST_PT_ENTRY.SEQ = T_MANIFEST_PT_UPN1.SEQ ");

            //区間1：運搬方法
            sql.Append(" LEFT OUTER JOIN M_UNPAN_HOUHOU AS M_UNPAN_HOUHOU1 WITH(NOLOCK) ");
            sql.Append("   ON T_MANIFEST_PT_UPN1.UPN_HOUHOU_CD = M_UNPAN_HOUHOU1.UNPAN_HOUHOU_CD ");

            //区間1：車種
            sql.Append(" LEFT OUTER JOIN M_SHASHU AS M_SHASHU1 WITH(NOLOCK) ");
            sql.Append("   ON T_MANIFEST_PT_UPN1.SHASHU_CD = M_SHASHU1.SHASHU_CD ");

            //区間1：車輛
            sql.Append(" LEFT OUTER JOIN M_SHARYOU AS M_SHARYOU1 WITH(NOLOCK) ");
            sql.Append("   ON T_MANIFEST_PT_UPN1.UPN_GYOUSHA_CD = M_SHARYOU1.GYOUSHA_CD ");
            sql.Append("  AND T_MANIFEST_PT_UPN1.SHARYOU_CD = M_SHARYOU1.SHARYOU_CD ");

            #endregion

            #region 区間2

            //区間2：マニフェストパターン運搬
            sql.Append(" LEFT OUTER JOIN ( ");
            sql.Append(" SELECT SYSTEM_ID ");
            sql.Append(" , SEQ ");
            sql.Append(" , UPN_ROUTE_NO ");
            sql.Append(" , UPN_GYOUSHA_CD ");
            sql.Append(" , UPN_GYOUSHA_NAME ");
            sql.Append(" , UPN_GYOUSHA_POST ");
            sql.Append(" , UPN_GYOUSHA_TEL ");
            sql.Append(" , UPN_GYOUSHA_ADDRESS ");
            sql.Append(" , UPN_HOUHOU_CD ");
            sql.Append(" , SHASHU_CD ");
            sql.Append(" , SHARYOU_CD ");
            sql.Append(" , TMH_KBN ");
            sql.Append(" , CASE TMH_KBN WHEN 1 THEN '有' WHEN 2 THEN '無' END AS TMH_KBN_NAME ");
            sql.Append(" , UPN_SAKI_KBN ");
            sql.Append(" , CASE UPN_SAKI_KBN WHEN 1 THEN '処分施設' WHEN 2 THEN '積替保管' ELSE '' END AS UPN_SAKI_KBN_NAME ");
            sql.Append(" , UPN_SAKI_GYOUSHA_CD ");
            sql.Append(" , UPN_SAKI_GENBA_CD ");
            sql.Append(" , UPN_SAKI_GENBA_NAME ");
            sql.Append(" , UPN_SAKI_GENBA_POST ");
            sql.Append(" , UPN_SAKI_GENBA_TEL ");
            sql.Append(" , UPN_SAKI_GENBA_ADDRESS ");
            sql.Append(" , UPN_JYUTAKUSHA_CD ");
            sql.Append(" , UPN_JYUTAKUSHA_NAME ");
            sql.Append(" , UNTENSHA_CD ");
            sql.Append(" , UNTENSHA_NAME ");
            sql.Append(" , UPN_END_DATE ");
            sql.Append(" , YUUKA_SUU ");
            sql.Append(" , YUUKA_UNIT_CD ");
            sql.Append(" FROM T_MANIFEST_PT_UPN WITH(NOLOCK) ");
            sql.Append(" WHERE UPN_ROUTE_NO = 2 ");
            sql.Append(" ) T_MANIFEST_PT_UPN2 ");
            sql.Append("   ON T_MANIFEST_PT_ENTRY.SYSTEM_ID = T_MANIFEST_PT_UPN2.SYSTEM_ID ");
            sql.Append("  AND T_MANIFEST_PT_ENTRY.SEQ = T_MANIFEST_PT_UPN2.SEQ ");

            //区間2：運搬方法
            sql.Append(" LEFT OUTER JOIN M_UNPAN_HOUHOU AS M_UNPAN_HOUHOU2 WITH(NOLOCK) ");
            sql.Append("   ON T_MANIFEST_PT_UPN2.UPN_HOUHOU_CD = M_UNPAN_HOUHOU2.UNPAN_HOUHOU_CD ");

            //区間2：車種
            sql.Append(" LEFT OUTER JOIN M_SHASHU AS M_SHASHU2 WITH(NOLOCK) ");
            sql.Append("   ON T_MANIFEST_PT_UPN2.SHASHU_CD = M_SHASHU2.SHASHU_CD ");

            //区間2：車輛
            sql.Append(" LEFT OUTER JOIN M_SHARYOU AS M_SHARYOU2 WITH(NOLOCK) ");
            sql.Append("   ON T_MANIFEST_PT_UPN2.UPN_GYOUSHA_CD = M_SHARYOU2.GYOUSHA_CD ");
            sql.Append("  AND T_MANIFEST_PT_UPN2.SHARYOU_CD = M_SHARYOU2.SHARYOU_CD ");
            #endregion

            #region 区間3

            //区間3：マニフェストパターン運搬
            sql.Append(" LEFT OUTER JOIN ( ");
            sql.Append(" SELECT SYSTEM_ID ");
            sql.Append(" , SEQ ");
            sql.Append(" , UPN_ROUTE_NO ");
            sql.Append(" , UPN_GYOUSHA_CD ");
            sql.Append(" , UPN_GYOUSHA_NAME ");
            sql.Append(" , UPN_GYOUSHA_POST ");
            sql.Append(" , UPN_GYOUSHA_TEL ");
            sql.Append(" , UPN_GYOUSHA_ADDRESS ");
            sql.Append(" , UPN_HOUHOU_CD ");
            sql.Append(" , SHASHU_CD ");
            sql.Append(" , SHARYOU_CD ");
            sql.Append(" , TMH_KBN ");
            sql.Append(" , CASE TMH_KBN WHEN 1 THEN '有' WHEN 2 THEN '無' END AS TMH_KBN_NAME ");
            sql.Append(" , UPN_SAKI_KBN ");
            sql.Append(" , CASE UPN_SAKI_KBN WHEN 1 THEN '処分施設' WHEN 2 THEN '積替保管' ELSE '' END AS UPN_SAKI_KBN_NAME ");
            sql.Append(" , UPN_SAKI_GYOUSHA_CD ");
            sql.Append(" , UPN_SAKI_GENBA_CD ");
            sql.Append(" , UPN_SAKI_GENBA_NAME ");
            sql.Append(" , UPN_SAKI_GENBA_POST ");
            sql.Append(" , UPN_SAKI_GENBA_TEL ");
            sql.Append(" , UPN_SAKI_GENBA_ADDRESS ");
            sql.Append(" , UPN_JYUTAKUSHA_CD ");
            sql.Append(" , UPN_JYUTAKUSHA_NAME ");
            sql.Append(" , UNTENSHA_CD ");
            sql.Append(" , UNTENSHA_NAME ");
            sql.Append(" , UPN_END_DATE ");
            sql.Append(" , YUUKA_SUU ");
            sql.Append(" , YUUKA_UNIT_CD ");
            sql.Append(" FROM T_MANIFEST_PT_UPN WITH(NOLOCK) ");
            sql.Append(" WHERE UPN_ROUTE_NO = 3 ");
            sql.Append(" ) T_MANIFEST_PT_UPN3 ");
            sql.Append("   ON T_MANIFEST_PT_ENTRY.SYSTEM_ID = T_MANIFEST_PT_UPN3.SYSTEM_ID ");
            sql.Append("  AND T_MANIFEST_PT_ENTRY.SEQ = T_MANIFEST_PT_UPN3.SEQ ");

            //区間3：運搬方法
            sql.Append(" LEFT OUTER JOIN M_UNPAN_HOUHOU AS M_UNPAN_HOUHOU3 WITH(NOLOCK) ");
            sql.Append("   ON T_MANIFEST_PT_UPN3.UPN_HOUHOU_CD = M_UNPAN_HOUHOU3.UNPAN_HOUHOU_CD ");

            //区間3：車種
            sql.Append(" LEFT OUTER JOIN M_SHASHU AS M_SHASHU3 WITH(NOLOCK) ");
            sql.Append("   ON T_MANIFEST_PT_UPN3.SHASHU_CD = M_SHASHU3.SHASHU_CD ");

            //区間3：車輛
            sql.Append(" LEFT OUTER JOIN M_SHARYOU AS M_SHARYOU3 WITH(NOLOCK) ");
            sql.Append("   ON T_MANIFEST_PT_UPN3.UPN_GYOUSHA_CD = M_SHARYOU3.GYOUSHA_CD ");
            sql.Append("  AND T_MANIFEST_PT_UPN3.SHARYOU_CD = M_SHARYOU3.SHARYOU_CD ");

            #endregion

            #region マニフェストパターン詳細

            //マニフェストパターン詳細
            sql.Append(" LEFT OUTER JOIN T_MANIFEST_PT_DETAIL WITH(NOLOCK) ");
            sql.Append("   ON T_MANIFEST_PT_ENTRY.SYSTEM_ID = T_MANIFEST_PT_DETAIL.SYSTEM_ID ");
            sql.Append("  AND T_MANIFEST_PT_ENTRY.SEQ = T_MANIFEST_PT_DETAIL.SEQ ");

            //廃棄物種類
            sql.Append(" LEFT OUTER JOIN ( ");
            sql.Append("     SELECT MHS_COL.HAIKI_KBN_CD ");
            sql.Append("          , MHS_COL.HAIKI_SHURUI_CD ");
            sql.Append("          , MHS_COL.HAIKI_SHURUI_NAME_RYAKU AS HAIKI_SHURUI_NAME ");
            sql.Append("       FROM M_HAIKI_SHURUI AS MHS_COL WITH(NOLOCK) ");
            sql.Append(" ) AS M_HAIKI_SHURUI ");
            sql.Append("   ON T_MANIFEST_PT_ENTRY.HAIKI_KBN_CD = M_HAIKI_SHURUI.HAIKI_KBN_CD ");
            sql.Append("  AND T_MANIFEST_PT_DETAIL.HAIKI_SHURUI_CD = M_HAIKI_SHURUI.HAIKI_SHURUI_CD ");

            //廃棄物名称
            sql.Append(" LEFT OUTER JOIN ( ");
            sql.Append("     SELECT MHN_COL.HAIKI_NAME_CD ");
            sql.Append("          , MHN_COL.HAIKI_NAME_RYAKU AS HAIKI_NAME ");
            sql.Append("       FROM M_HAIKI_NAME AS MHN_COL WITH(NOLOCK) ");
            sql.Append(" ) AS M_HAIKI_NAME ");
            sql.Append("   ON T_MANIFEST_PT_DETAIL.HAIKI_NAME_CD = M_HAIKI_NAME.HAIKI_NAME_CD ");

            //荷姿
            sql.Append(" LEFT OUTER JOIN ( ");
            sql.Append("     SELECT MN_COL.NISUGATA_CD ");
            sql.Append("          , MN_COL.NISUGATA_NAME_RYAKU AS NISUGATA_NAME ");
            sql.Append("       FROM M_NISUGATA AS MN_COL WITH(NOLOCK) ");
            sql.Append(" ) AS M_NISUGATA ");
            sql.Append("   ON T_MANIFEST_PT_DETAIL.NISUGATA_CD = M_NISUGATA.NISUGATA_CD ");

            //単位
            sql.Append(" LEFT OUTER JOIN ( ");
            sql.Append("     SELECT MU_COL.UNIT_CD ");
            sql.Append("          , MU_COL.UNIT_NAME_RYAKU AS UNIT_NAME ");
            sql.Append("       FROM M_UNIT AS MU_COL WITH(NOLOCK) ");
            sql.Append(" ) AS M_UNIT ");
            sql.Append("   ON T_MANIFEST_PT_DETAIL.HAIKI_UNIT_CD = M_UNIT.UNIT_CD ");

            //処分方法
            sql.Append(" LEFT OUTER JOIN ( ");
            sql.Append("     SELECT MSH_COL.SHOBUN_HOUHOU_CD ");
            sql.Append("          , MSH_COL.SHOBUN_HOUHOU_NAME_RYAKU AS SHOBUN_HOUHOU_NAME ");
            sql.Append("       FROM M_SHOBUN_HOUHOU AS MSH_COL WITH(NOLOCK) ");
            sql.Append(" ) AS M_SHOBUN_HOUHOU ");
            sql.Append("   ON T_MANIFEST_PT_DETAIL.SBN_HOUHOU_CD = M_SHOBUN_HOUHOU.SHOBUN_HOUHOU_CD ");

            sql.Append(" LEFT OUTER JOIN M_GYOUSHA");
            sql.Append("   ON T_MANIFEST_PT_DETAIL.LAST_SBN_GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ");

            sql.Append(" LEFT OUTER JOIN M_GENBA");
            sql.Append("   ON T_MANIFEST_PT_DETAIL.LAST_SBN_GYOUSHA_CD = M_GENBA.GYOUSHA_CD ");
            sql.Append("   AND T_MANIFEST_PT_DETAIL.LAST_SBN_GENBA_CD = M_GENBA.GENBA_CD ");

            // 20140529 syunrei No.730 マニフェストパターン一覧 start
            sql.Append(" LEFT OUTER JOIN T_MANIFEST_PT_PRT PRT");
            sql.Append("   ON (T_MANIFEST_PT_ENTRY.SYSTEM_ID=PRT.SYSTEM_ID AND T_MANIFEST_PT_ENTRY.SEQ=PRT.SEQ) ");
            // 20140612 satoda No.4720 Start
            sql.Append(" LEFT OUTER JOIN ");
            sql.Append(" (SELECT DISTINCT ");
            sql.Append(" DETAIL_PRT_1.SYSTEM_ID,");
            sql.Append(" DETAIL_PRT_1.SEQ,");
            sql.Append(" CASE ISNULL(DETAIL_PRT_2.SYSTEM_ID,'') ");
            sql.Append(" WHEN '' THEN DETAIL_PRT_1.HAIKI_SHURUI_NAME ELSE '混合廃棄物' END AS HAIKI_SHURUI_NAME ");
            sql.Append(" FROM ");
            sql.Append(" T_MANIFEST_PT_DETAIL_PRT AS DETAIL_PRT_1 ");
            sql.Append(" LEFT JOIN ");
            sql.Append("   (SELECT SYSTEM_ID,SEQ ");
            sql.Append("   FROM T_MANIFEST_PT_DETAIL_PRT ");
            sql.Append("   GROUP BY SYSTEM_ID,SEQ ");
            sql.Append("   HAVING COUNT(*) > 1) AS DETAIL_PRT_2 ");
            sql.Append(" ON DETAIL_PRT_1.SYSTEM_ID = DETAIL_PRT_2.SYSTEM_ID ");
            sql.Append(" AND DETAIL_PRT_1.SEQ = DETAIL_PRT_2.SEQ) T_MANIFEST_PT_DETAIL_PRT ");
            // 20140612 satoda No.4720 End
            sql.Append("   ON(T_MANIFEST_PT_DETAIL_PRT.SYSTEM_ID=PRT.SYSTEM_ID AND T_MANIFEST_PT_DETAIL_PRT.SEQ=PRT.SEQ) ");
            // 20140529 syunrei No.730 マニフェストパターン一覧 end

            #endregion

            #endregion

            #region WHERE句
            // 20140529 syunrei No.730 マニフェストパターン一覧 start
            var sqlWhere = new StringBuilder();
            //deleteフラグ
            sqlWhere.AppendFormat(" WHERE T_MANIFEST_PT_ENTRY.DELETE_FLG = '{0}'", DELETE_FLG);

            //一括登録区分
            if (LIST_REGIST_KBN != string.Empty)
            {
                sqlWhere.AppendFormat("   AND T_MANIFEST_PT_ENTRY.LIST_REGIST_KBN = '{0}'", LIST_REGIST_KBN);
            }
            //廃棄物区分CD
            if (HAIKI_KBN_CD != string.Empty)
            {
                if (HAIKI_KBN_CD == "5")
                {
                   
                    sqlWhere.AppendFormat("   AND T_MANIFEST_PT_ENTRY.HAIKI_KBN_CD IN (1,2,3) ");
                }
                else
                {
                    sqlWhere.AppendFormat("   AND T_MANIFEST_PT_ENTRY.HAIKI_KBN_CD = {0}", HAIKI_KBN_CD);
                }
            }


            //一次マニフェスト区分
            if (FIRST_MANIFEST_KBN != string.Empty)
            {
                sqlWhere.AppendFormat("   AND T_MANIFEST_PT_ENTRY.FIRST_MANIFEST_KBN = '{0}'", FIRST_MANIFEST_KBN);
            }

            //パターン名
            if (PATTERN_NAME != string.Empty)
            {
                string sqlPatternName = SqlCreateUtility.CounterplanEscapeSequence(PATTERN_NAME);
                sqlWhere.AppendFormat("   AND T_MANIFEST_PT_ENTRY.PATTERN_FURIGANA LIKE N'%{0}%'", sqlPatternName);
            }

            //拠点       

            if (KYOTEN_CD != string.Empty && KYOTEN_CD != "99")
            {
                sqlWhere.AppendFormat("   AND T_MANIFEST_PT_ENTRY.KYOTEN_CD = {0}", KYOTEN_CD);
            }

            //排出事業者名
            if (HST_GYOUSHA_NAME != string.Empty)
            {
                string sqlHstGyoushaName = SqlCreateUtility.CounterplanEscapeSequence(HST_GYOUSHA_NAME);
                sqlWhere.AppendFormat("   AND T_MANIFEST_PT_ENTRY.HST_GYOUSHA_NAME LIKE N'%{0}%'", sqlHstGyoushaName);
            }

            //排出事業場名
            if (HST_GENBA_NAME != string.Empty)
            {
                string sqlHstGenbaName = SqlCreateUtility.CounterplanEscapeSequence(HST_GENBA_NAME);
                sqlWhere.AppendFormat("   AND T_MANIFEST_PT_ENTRY.HST_GENBA_NAME LIKE N'%{0}%'", sqlHstGenbaName);
            }

            //排出事業者
            if (HST_GYOUSHA_CD != string.Empty)
            {
                sqlWhere.AppendFormat("   AND T_MANIFEST_PT_ENTRY.HST_GYOUSHA_CD LIKE N'%{0}%'", HST_GYOUSHA_CD);
            }

            //排出事業場
            if (HST_GENBA_CD != string.Empty)
            {
                sqlWhere.AppendFormat("   AND T_MANIFEST_PT_ENTRY.HST_GENBA_CD LIKE N'%{0}%'", HST_GENBA_CD);
            }

            //運搬受託者
            if (HST_UNBAN_JUTAKU_CD != string.Empty)
            {               
                sqlWhere.AppendFormat("   AND T_MANIFEST_PT_UPN1.UPN_GYOUSHA_CD LIKE N'%{0}%'", HST_UNBAN_JUTAKU_CD);
            }

            if (HST_UNBAN_JUTAKU_NAME != string.Empty)
            {
                string sqlHstUnbanJutakuName = SqlCreateUtility.CounterplanEscapeSequence(HST_UNBAN_JUTAKU_NAME);
                sqlWhere.AppendFormat("   AND T_MANIFEST_PT_UPN1.UPN_GYOUSHA_NAME LIKE N'%{0}%'", sqlHstUnbanJutakuName);
            }

            //処分受託者
            if (HST_SHOUBU_JUTAKU_CD != string.Empty)
            {
                sqlWhere.AppendFormat("   AND T_MANIFEST_PT_ENTRY.SBN_GYOUSHA_CD LIKE N'%{0}%'", HST_SHOUBU_JUTAKU_CD);
            }

            if (HST_SHOUBU_JUTAKU_NAME != string.Empty)
            {
                string sqlHstShoubuJutakuName = SqlCreateUtility.CounterplanEscapeSequence(HST_SHOUBU_JUTAKU_NAME);
                sqlWhere.AppendFormat("   AND T_MANIFEST_PT_ENTRY.SBN_GYOUSHA_NAME LIKE N'%{0}%'", sqlHstShoubuJutakuName);
            }

            //処分事業場⇒「運搬先の事業場　名称」と同じ
            if (HST_SHOUBU_JIGYOU_CD != string.Empty)
            {
                sqlWhere.AppendFormat("   AND T_MANIFEST_PT_UPN1.UPN_SAKI_GENBA_CD LIKE N'%{0}%'", HST_SHOUBU_JIGYOU_CD);
            }

            if (HST_SHOUBU_JIGYOU_NAME != string.Empty)
            {
                string sqlHstShoubuJigyouName = SqlCreateUtility.CounterplanEscapeSequence(HST_SHOUBU_JIGYOU_NAME);
                sqlWhere.AppendFormat("   AND T_MANIFEST_PT_UPN1.UPN_SAKI_GENBA_NAME LIKE N'%{0}%'", sqlHstShoubuJigyouName);
            }

            if (!string.IsNullOrEmpty(sqlWhere.ToString()))
            {
                //検索条件を追加する
                sql.Append(sqlWhere.ToString());
            }

            //sql.Append(" WHERE T_MANIFEST_PT_ENTRY.DELETE_FLG = '" + DELETE_FLG + "'");

            ////一括登録区分
            //if (LIST_REGIST_KBN != string.Empty)
            //{
            //    sql.Append("   AND T_MANIFEST_PT_ENTRY.LIST_REGIST_KBN = '" + LIST_REGIST_KBN + "'");
            //}

            ////廃棄物区分CD
            //if (HAIKI_KBN_CD != string.Empty)
            //{
            //    //switch (HAIKI_KBN_CD)
            //    //{
            //    //    case "1"://産廃（直行）
            //    //        sql.Append("   AND T_MANIFEST_PT_ENTRY.HAIKI_KBN_CD = 1 ");
            //    //        break;

            //    //    case "2"://産廃（積替）
            //    //        sql.Append("   AND T_MANIFEST_PT_ENTRY.HAIKI_KBN_CD = 3 ");
            //    //        break;

            //    //    case "3"://建廃
            //    //        sql.Append("   AND T_MANIFEST_PT_ENTRY.HAIKI_KBN_CD = 2 ");
            //    //        break;

            //    //    case "5"://紙マニフェスト
            //    //        sql.Append("   AND T_MANIFEST_PT_ENTRY.HAIKI_KBN_CD IN (1,2,3) ");
            //    //        break;

            //    //    default:
            //    //        break;
            //    //}

            //    if (HAIKI_KBN_CD == "5")
            //    {
            //        sql.Append("   AND T_MANIFEST_PT_ENTRY.HAIKI_KBN_CD IN (1,2,3) ");
            //    }
            //    else
            //    {
            //        sql.Append("   AND T_MANIFEST_PT_ENTRY.HAIKI_KBN_CD = '" + HAIKI_KBN_CD + "'");
            //    }

            //}

            ////一次マニフェスト区分
            //if (FIRST_MANIFEST_KBN != string.Empty)
            //{
            //    sql.Append("   AND T_MANIFEST_PT_ENTRY.FIRST_MANIFEST_KBN = '" + FIRST_MANIFEST_KBN + "'");
            //}

            ////パターン名
            //if (PATTERN_NAME != string.Empty)
            //{
            //    sql.Append("   AND T_MANIFEST_PT_ENTRY.PATTERN_NAME LIKE N'%" + PATTERN_NAME + "%'");
            //}

            ////拠点
            ////if (KYOTEN_CD != string.Empty)
            ////{
            ////    sql.Append("   AND T_MANIFEST_PT_ENTRY.KYOTEN_CD = " + KYOTEN_CD);
            ////}

            //if (KYOTEN_CD != string.Empty && KYOTEN_CD != "99")
            //{
            //    sql.Append("   AND T_MANIFEST_PT_ENTRY.KYOTEN_CD = " + KYOTEN_CD);
            //}

            ////排出事業者名
            //if (HST_GYOUSHA_NAME != string.Empty)
            //{
            //    sql.Append("   AND T_MANIFEST_PT_ENTRY.HST_GYOUSHA_NAME LIKE N'%" + HST_GYOUSHA_NAME + "%'");
            //}

            ////排出事業場名
            //if (HST_GENBA_NAME != string.Empty)
            //{
            //    sql.Append("   AND T_MANIFEST_PT_ENTRY.HST_GENBA_NAME LIKE N'%" + HST_GENBA_NAME + "%'");
            //}


            // 20140529 syunrei No.730 マニフェストパターン一覧 end

            #endregion

            #region ORDERBY句

            if (!string.IsNullOrEmpty(orderByQuery))
            {
                sql.Append(" ORDER BY ");
                sql.Append(this.orderByQuery);
            }

            #endregion

            this.createSql = sql.ToString();
            sql.Append(string.Empty);

            this.SearchResult = dao_GetResult.getdateforstringsql(this.createSql);
            int count = this.SearchResult.Rows.Count;
            // 20140529 syunrei No.730 マニフェストパターン一覧 start
            //// 列固定対応　非表示列を削除
            //foreach (var s in LogicClass.RemoveColumnsName)
            //{
            //    if (this.SearchResult.Columns.OfType<DataColumn>().Any(col => col.ColumnName == s))
            //    {
            //        this.SearchResult.Columns.Remove(s);
            //    }
            //}
            // 20140529 syunrei No.730 マニフェストパターン一覧 end
            LogUtility.DebugMethodEnd(count);

            return count;
        }

        /// <summary>
        /// マニフェストパターン　画面表示
        /// </summary>
        public void Set_Search_TMPE()
        {
            LogUtility.DebugMethodStart();

            //データ表示
            this.form.customDataGridView1.DataSource = null;

            //foreach (var s in LogicClass.RemoveColumnsName.Concat(LogicClass.removeColumnsPhysical))
            //{
            //    if (this.SearchResult.Columns.OfType<DataColumn>().Any(col => col.ColumnName == s))
            //    {
            //        this.SearchResult.Columns.Remove(s);
            //    }
            //}

            this.form.ShowData();

            // 20140618 syunrei EV004876_マニフェストパターン一覧のアラート件数が効いていない start
            if (this.form.customDataGridView1.RowCount > 0 && this.form.customDataGridView1 != null)
            {
                // 20140604 syunrei No.730 マニフェストパターン一覧 start
                //this.form.customDataGridView1.Columns["SYSTEM_ID"].Visible = false;
                //this.form.customDataGridView1.Columns["SEQ"].Visible = false;
                ////TODO:排他制御の修正
                //this.form.customDataGridView1.Columns["TIME_STAMP"].Visible = false;
               
                //非表示列を隠す
                this.SetColumnsVisible(this.form.HaikiKbnCD, false);
                // 20140604 syunrei No.730 マニフェストパターン一覧 end
            }
            // 20140618 syunrei EV004876_マニフェストパターン一覧のアラート件数が効いていない end

            //読込データ件数
            this.header.ReadDataNumber.Text = this.SearchResult.Rows.Count.ToString();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 電子マニフェストパターン　データ取得
        /// </summary>
        public int Get_Search_DPR18
            (String SYSTEM_ID, String SEQ, String LIST_REGIST_KBN,
             String HAIKI_KBN_CD, String FIRST_MANIFEST_KBN, String PATTERN_NAME,
             String KYOTEN_CD, 
             String HST_GYOUSHA_NAME, String HST_GENBA_NAME, String DELETE_FLG,
             String HST_GYOUSHA_CD, String HST_GENBA_CD,
             String HST_UNBAN_JUTAKU_CD, String HST_UNBAN_JUTAKU_NAME,
             String HST_SHOUBU_JUTAKU_CD, String HST_SHOUBU_JUTAKU_NAME,
             String HST_SHOUBU_JIGYOU_CD, String HST_SHOUBU_JIGYOU_NAME
            )
        {
            LogUtility.DebugMethodStart
                (SYSTEM_ID, SEQ, LIST_REGIST_KBN,
                 HAIKI_KBN_CD, FIRST_MANIFEST_KBN, PATTERN_NAME,
                 KYOTEN_CD, 
                 HST_GYOUSHA_NAME, HST_GENBA_NAME, DELETE_FLG,
                 HST_GYOUSHA_CD, HST_GENBA_CD,
                 HST_UNBAN_JUTAKU_CD, HST_UNBAN_JUTAKU_NAME,
                 HST_SHOUBU_JUTAKU_CD, HST_SHOUBU_JUTAKU_NAME,
                 HST_SHOUBU_JIGYOU_CD, HST_SHOUBU_JIGYOU_NAME
                 );

            //SQL文格納StringBuilder
            var sql = new StringBuilder();

            #region SELECT句

            sql.Append(" SELECT DISTINCT DT_PT_R18.SYSTEM_ID ");
            sql.Append("      , DT_PT_R18.SEQ ");
            sql.Append("      , DT_PT_R18.PATTERN_NAME ");
            sql.Append("      , DT_PT_R18.PATTERN_FURIGANA ");
            sql.Append("      , DT_PT_R18.USE_DEFAULT_KBN ");//155789
            sql.Append("      , DT_PT_R18.MANIFEST_ID ");
            sql.Append("      , DT_PT_R18.MANIFEST_KBN ");
            sql.Append("      , DT_PT_R18.SHOUNIN_FLAG ");
            sql.Append("      , DT_PT_R18.HIKIWATASHI_DATE ");
            sql.Append("      , DT_PT_R18.UPN_ENDREP_FLAG ");
            sql.Append("      , DT_PT_R18.SBN_ENDREP_FLAG ");
            sql.Append("      , DT_PT_R18.LAST_SBN_ENDREP_FLAG ");
            sql.Append("      , DT_PT_R18.KAKIN_DATE ");
            sql.Append("      , DT_PT_R18.REGI_DATE ");
            sql.Append("      , DT_PT_R18.UPN_SBN_REP_LIMIT_DATE ");
            sql.Append("      , DT_PT_R18.LAST_SBN_REP_LIMIT_DATE ");
            sql.Append("      , DT_PT_R18.RESV_LIMIT_DATE ");
            sql.Append("      , DT_PT_R18.SBN_ENDREP_KBN ");
            sql.Append("      , DT_PT_R18.HST_SHA_EDI_MEMBER_ID ");
            sql.Append("      , DT_PT_R18.HST_SHA_NAME ");
            sql.Append("      , DT_PT_R18.HST_SHA_POST ");
            sql.Append("      , DT_PT_R18.HST_SHA_ADDRESS1 ");
            sql.Append("      , DT_PT_R18.HST_SHA_ADDRESS2 ");
            sql.Append("      , DT_PT_R18.HST_SHA_ADDRESS3 ");
            sql.Append("      , DT_PT_R18.HST_SHA_ADDRESS4 ");
            sql.Append("      , DT_PT_R18.HST_SHA_TEL ");
            sql.Append("      , DT_PT_R18.HST_SHA_FAX ");
            sql.Append("      , DT_PT_R18.HST_JOU_NAME ");
            sql.Append("      , DT_PT_R18.HST_JOU_POST_NO ");
            sql.Append("      , DT_PT_R18.HST_JOU_ADDRESS1 ");
            sql.Append("      , DT_PT_R18.HST_JOU_ADDRESS2 ");
            sql.Append("      , DT_PT_R18.HST_JOU_ADDRESS3 ");
            sql.Append("      , DT_PT_R18.HST_JOU_ADDRESS4 ");
            sql.Append("      , DT_PT_R18.HST_JOU_TEL ");
            sql.Append("      , DT_PT_R18.REGI_TAN ");
            sql.Append("      , DT_PT_R18.HIKIWATASHI_TAN_NAME ");
            sql.Append("      , DT_PT_R18.HAIKI_DAI_CODE ");
            sql.Append("      , DT_PT_R18.HAIKI_CHU_CODE ");
            sql.Append("      , DT_PT_R18.HAIKI_SHO_CODE ");
            sql.Append("      , DT_PT_R18.HAIKI_SAI_CODE ");
            sql.Append("      , DT_PT_R18.HAIKI_BUNRUI ");
            sql.Append("      , DT_PT_R18.HAIKI_SHURUI ");
            sql.Append("      , DT_PT_R18.HAIKI_NAME ");
            sql.Append("      , DT_PT_R18.HAIKI_SUU ");
            sql.Append("      , DT_PT_R18.HAIKI_UNIT_CODE ");
            sql.Append("      , DT_PT_R18.SUU_KAKUTEI_CODE ");
            sql.Append("      , DT_PT_R18.HAIKI_KAKUTEI_SUU ");
            sql.Append("      , DT_PT_R18.HAIKI_KAKUTEI_UNIT_CODE ");
            sql.Append("      , DT_PT_R18.NISUGATA_CODE ");
            sql.Append("      , DT_PT_R18.NISUGATA_NAME ");
            sql.Append("      , DT_PT_R18.NISUGATA_SUU ");
            sql.Append("      , DT_PT_R18.SBN_SHA_MEMBER_ID ");
            sql.Append("      , DT_PT_R18.SBN_SHA_NAME ");
            sql.Append("      , DT_PT_R18.SBN_SHA_POST ");
            sql.Append("      , DT_PT_R18.SBN_SHA_ADDRESS1 ");
            sql.Append("      , DT_PT_R18.SBN_SHA_ADDRESS2 ");
            sql.Append("      , DT_PT_R18.SBN_SHA_ADDRESS3 ");
            sql.Append("      , DT_PT_R18.SBN_SHA_ADDRESS4 ");
            sql.Append("      , DT_PT_R18.SBN_SHA_TEL ");
            sql.Append("      , DT_PT_R18.SBN_SHA_FAX ");
            sql.Append("      , DT_PT_R18.SBN_SHA_KYOKA_ID ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_MEMBER_ID ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_NAME ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_POST ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_ADDRESS1 ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_ADDRESS2 ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_ADDRESS3 ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_ADDRESS4 ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_TEL ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_FAX ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_KYOKA_ID ");
            sql.Append("      , DT_PT_R18.SBN_WAY_CODE ");
            sql.Append("      , DT_PT_R18.SBN_WAY_NAME ");
            sql.Append("      , DT_PT_R18.SBN_SHOUNIN_FLAG ");
            sql.Append("      , DT_PT_R18.SBN_END_DATE ");
            sql.Append("      , DT_PT_R18.HAIKI_IN_DATE ");
            sql.Append("      , DT_PT_R18.RECEPT_SUU ");
            sql.Append("      , DT_PT_R18.RECEPT_UNIT_CODE ");
            sql.Append("      , DT_PT_R18.UPN_TAN_NAME ");
            sql.Append("      , DT_PT_R18.CAR_NO ");
            sql.Append("      , DT_PT_R18.REP_TAN_NAME ");
            sql.Append("      , DT_PT_R18.SBN_TAN_NAME ");
            sql.Append("      , DT_PT_R18.SBN_END_REP_DATE ");
            sql.Append("      , DT_PT_R18.SBN_REP_BIKOU ");
            sql.Append("      , DT_PT_R18.KENGEN_CODE ");
            sql.Append("      , DT_PT_R18.LAST_SBN_JOU_KISAI_FLAG ");
            sql.Append("      , DT_PT_R18.FIRST_MANIFEST_FLAG ");
            sql.Append("      , DT_PT_R18.LAST_SBN_END_DATE ");
            sql.Append("      , DT_PT_R18.LAST_SBN_END_REP_DATE ");
            sql.Append("      , DT_PT_R18.SHUSEI_DATE ");
            sql.Append("      , DT_PT_R18.CANCEL_FLAG ");
            sql.Append("      , DT_PT_R18.CANCEL_DATE ");
            sql.Append("      , DT_PT_R18.LAST_UPDATE_DATE ");
            sql.Append("      , DT_PT_R18.YUUGAI_CNT ");
            sql.Append("      , DT_PT_R18.UPN_ROUTE_CNT ");
            sql.Append("      , DT_PT_R18.LAST_SBN_PLAN_CNT ");
            sql.Append("      , DT_PT_R18.LAST_SBN_CNT ");
            sql.Append("      , DT_PT_R18.RENRAKU_CNT ");
            sql.Append("      , DT_PT_R18.BIKOU_CNT ");
            sql.Append("      , DT_PT_R18.FIRST_MANIFEST_CNT ");
            sql.Append("      , DT_PT_R18.HST_GYOUSHA_CD ");
            sql.Append("      , DT_PT_R18.HST_GENBA_CD ");
            sql.Append("      , DT_PT_R18.SBN_GYOUSHA_CD ");
            sql.Append("      , DT_PT_R18.SBN_GENBA_CD ");
            sql.Append("      , DT_PT_R18.NO_REP_SBN_EDI_MEMBER_ID ");
            sql.Append("      , DT_PT_R18.HAIKI_NAME_CD ");
            sql.Append("      , DT_PT_R18.SBN_HOUHOU_CD ");
            sql.Append("      , DT_PT_R18.HOUKOKU_TANTOUSHA_CD ");
            sql.Append("      , DT_PT_R18.SBN_TANTOUSHA_CD ");
            sql.Append("      , DT_PT_R18.UPN_TANTOUSHA_CD ");
            sql.Append("      , DT_PT_R18.SHARYOU_CD ");
            sql.Append("      , DT_PT_R18.KANSAN_SUU ");
            sql.Append("      , DT_PT_R18.CREATE_USER ");
            sql.Append("      , DT_PT_R18.CREATE_DATE ");
            sql.Append("      , DT_PT_R18.CREATE_PC ");
            sql.Append("      , DT_PT_R18.UPDATE_USER ");
            sql.Append("      , DT_PT_R18.UPDATE_DATE ");
            sql.Append("      , DT_PT_R18.UPDATE_PC ");
            sql.Append("      , DT_PT_R18.DELETE_FLG ");

            //TODO:排他制御の修正
            sql.Append("      , CAST(DT_PT_R18.TIME_STAMP AS int ) AS TIME_STAMP ");

            String MOutputPatternSelect = this.selectQuery;
            if (String.IsNullOrEmpty(MOutputPatternSelect))
            {
            }
            else
            {
                sql.Append(", ");
                sql.Append(MOutputPatternSelect);
            }
            #endregion

            #region FROM句
            //電子マニフェストパターン
            sql.Append(" FROM ( ");
            sql.Append(" SELECT 4 AS HAIKI_KBN_CD ");
            sql.Append("      , '電子' AS HAIKI_KBN_NAME ");
            sql.Append("      , DT_PT_R18.SYSTEM_ID ");
            sql.Append("      , DT_PT_R18.SEQ ");
            sql.Append("      , DT_PT_R18.PATTERN_NAME ");
            sql.Append("      , DT_PT_R18.PATTERN_FURIGANA ");
            sql.Append("      , DT_PT_R18.USE_DEFAULT_KBN ");//155789
            sql.Append("      , DT_PT_R18.MANIFEST_ID ");
            sql.Append("      , DT_PT_R18.MANIFEST_KBN ");
            sql.Append("      , DT_PT_R18.SHOUNIN_FLAG ");
            sql.Append("      , DT_PT_R18.HIKIWATASHI_DATE ");
            sql.Append("      , CASE WHEN (DT_PT_R18.HIKIWATASHI_DATE IS NULL OR DT_PT_R18.HIKIWATASHI_DATE = '') THEN NULL ELSE CONVERT(DATETIME,DT_PT_R18.HIKIWATASHI_DATE) END AS HIKIWATASHI_DATE_CNV ");
            sql.Append("      , DT_PT_R18.UPN_ENDREP_FLAG ");
            sql.Append("      , DT_PT_R18.SBN_ENDREP_FLAG ");
            sql.Append("      , DT_PT_R18.LAST_SBN_ENDREP_FLAG ");
            sql.Append("      , DT_PT_R18.KAKIN_DATE ");
            sql.Append("      , DT_PT_R18.REGI_DATE ");
            sql.Append("      , DT_PT_R18.UPN_SBN_REP_LIMIT_DATE ");
            sql.Append("      , DT_PT_R18.LAST_SBN_REP_LIMIT_DATE ");
            sql.Append("      , DT_PT_R18.RESV_LIMIT_DATE ");
            sql.Append("      , DT_PT_R18.SBN_ENDREP_KBN ");
            sql.Append("      , DT_PT_R18.HST_SHA_EDI_MEMBER_ID ");
            sql.Append("      , DT_PT_R18.HST_SHA_NAME ");
            sql.Append("      , DT_PT_R18.HST_SHA_POST ");
            sql.Append("      , DT_PT_R18.HST_SHA_ADDRESS1 ");
            sql.Append("      , DT_PT_R18.HST_SHA_ADDRESS2 ");
            sql.Append("      , DT_PT_R18.HST_SHA_ADDRESS3 ");
            sql.Append("      , DT_PT_R18.HST_SHA_ADDRESS4 ");
            sql.Append("      , DT_PT_R18.HST_SHA_ADDRESS1 + DT_PT_R18.HST_SHA_ADDRESS2 + DT_PT_R18.HST_SHA_ADDRESS3 + DT_PT_R18.HST_SHA_ADDRESS4 AS HST_SHA_ADDRESS ");
            sql.Append("      , DT_PT_R18.HST_SHA_TEL ");
            sql.Append("      , DT_PT_R18.HST_SHA_FAX ");
            sql.Append("      , DT_PT_R18.HST_JOU_NAME ");
            sql.Append("      , DT_PT_R18.HST_JOU_POST_NO ");
            sql.Append("      , DT_PT_R18.HST_JOU_ADDRESS1 ");
            sql.Append("      , DT_PT_R18.HST_JOU_ADDRESS2 ");
            sql.Append("      , DT_PT_R18.HST_JOU_ADDRESS3 ");
            sql.Append("      , DT_PT_R18.HST_JOU_ADDRESS4 ");
            sql.Append("      , DT_PT_R18.HST_JOU_ADDRESS1 + DT_PT_R18.HST_JOU_ADDRESS2 + DT_PT_R18.HST_JOU_ADDRESS3 + DT_PT_R18.HST_JOU_ADDRESS4 AS HST_JOU_ADDRESS ");
            sql.Append("      , DT_PT_R18.HST_JOU_TEL ");
            sql.Append("      , DT_PT_R18.REGI_TAN ");
            sql.Append("      , DT_PT_R18.HIKIWATASHI_TAN_NAME ");
            sql.Append("      , DT_PT_R18.HAIKI_DAI_CODE ");
            sql.Append("      , DT_PT_R18.HAIKI_CHU_CODE ");
            sql.Append("      , DT_PT_R18.HAIKI_SHO_CODE ");
            sql.Append("      , DT_PT_R18.HAIKI_DAI_CODE + DT_PT_R18.HAIKI_CHU_CODE + DT_PT_R18.HAIKI_SHO_CODE AS HAIKI_CODE ");
            sql.Append("      , DT_PT_R18.HAIKI_SAI_CODE ");
            sql.Append("      , DT_PT_R18.HAIKI_BUNRUI ");
            sql.Append("      , DT_PT_R18.HAIKI_SHURUI ");
            sql.Append("      , DT_PT_R18.HAIKI_NAME ");
            sql.Append("      , DT_PT_R18.HAIKI_SUU ");
            sql.Append("      , DT_PT_R18.HAIKI_UNIT_CODE ");
            sql.Append("      , DT_PT_R18.SUU_KAKUTEI_CODE ");
            sql.Append("      , DT_PT_R18.HAIKI_KAKUTEI_SUU ");
            sql.Append("      , DT_PT_R18.HAIKI_KAKUTEI_UNIT_CODE ");
            sql.Append("      , DT_PT_R18.NISUGATA_CODE ");
            sql.Append("      , DT_PT_R18.NISUGATA_NAME ");
            sql.Append("      , DT_PT_R18.NISUGATA_SUU ");
            sql.Append("      , DT_PT_R18.SBN_SHA_MEMBER_ID ");
            sql.Append("      , DT_PT_R18.SBN_SHA_NAME ");
            sql.Append("      , DT_PT_R18.SBN_SHA_NAME AS SBN_GYOUSHA_NAME ");
            sql.Append("      , DT_PT_R18.SBN_SHA_POST ");
            sql.Append("      , DT_PT_R18.SBN_SHA_ADDRESS1 ");
            sql.Append("      , DT_PT_R18.SBN_SHA_ADDRESS2 ");
            sql.Append("      , DT_PT_R18.SBN_SHA_ADDRESS3 ");
            sql.Append("      , DT_PT_R18.SBN_SHA_ADDRESS4 ");
            sql.Append("      , DT_PT_R18.SBN_SHA_ADDRESS1 + DT_PT_R18.SBN_SHA_ADDRESS2 + DT_PT_R18.SBN_SHA_ADDRESS3 + DT_PT_R18.SBN_SHA_ADDRESS4 AS SBN_SHA_ADDRESS ");
            sql.Append("      , DT_PT_R18.SBN_SHA_TEL ");
            sql.Append("      , DT_PT_R18.SBN_SHA_FAX ");
            sql.Append("      , DT_PT_R18.SBN_SHA_KYOKA_ID ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_MEMBER_ID ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_NAME ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_POST ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_ADDRESS1 ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_ADDRESS2 ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_ADDRESS3 ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_ADDRESS4 ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_ADDRESS1 + DT_PT_R18.SAI_SBN_SHA_ADDRESS2 + DT_PT_R18.SAI_SBN_SHA_ADDRESS3 + DT_PT_R18.SAI_SBN_SHA_ADDRESS4 AS SAI_SBN_SHA_ADDRESS ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_TEL ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_FAX ");
            sql.Append("      , DT_PT_R18.SAI_SBN_SHA_KYOKA_ID ");
            sql.Append("      , DT_PT_R18.SBN_WAY_CODE ");
            sql.Append("      , DT_PT_R18.SBN_WAY_NAME ");
            sql.Append("      , DT_PT_R18.SBN_SHOUNIN_FLAG ");
            sql.Append("      , DT_PT_R18.SBN_END_DATE ");
            sql.Append("      , CASE WHEN (DT_PT_R18.SBN_END_DATE IS NULL OR DT_PT_R18.SBN_END_DATE = '') THEN NULL ELSE CONVERT(DATETIME,DT_PT_R18.SBN_END_DATE) END AS SBN_END_DATE_CNV ");
            sql.Append("      , DT_PT_R18.HAIKI_IN_DATE ");
            sql.Append("      , CASE WHEN (DT_PT_R18.HAIKI_IN_DATE IS NULL OR DT_PT_R18.HAIKI_IN_DATE = '') THEN NULL ELSE CONVERT(DATETIME,DT_PT_R18.HAIKI_IN_DATE) END AS HAIKI_IN_DATE_CNV ");
            sql.Append("      , DT_PT_R18.RECEPT_SUU ");
            sql.Append("      , DT_PT_R18.RECEPT_UNIT_CODE ");
            sql.Append("      , DT_PT_R18.UPN_TAN_NAME ");
            sql.Append("      , DT_PT_R18.CAR_NO ");
            sql.Append("      , DT_PT_R18.REP_TAN_NAME ");
            sql.Append("      , DT_PT_R18.SBN_TAN_NAME ");
            sql.Append("      , DT_PT_R18.SBN_END_REP_DATE ");
            sql.Append("      , DT_PT_R18.SBN_REP_BIKOU ");
            sql.Append("      , DT_PT_R18.KENGEN_CODE ");
            sql.Append("      , DT_PT_R18.LAST_SBN_JOU_KISAI_FLAG ");
            sql.Append("      , CASE DT_PT_R18.LAST_SBN_JOU_KISAI_FLAG WHEN '0' THEN 1 WHEN '1' THEN 2 END AS LAST_SBN_YOTEI_KBN ");
            sql.Append("      , DT_PT_R18.FIRST_MANIFEST_FLAG ");
            sql.Append("      , CASE WHEN DT_PT_R18.FIRST_MANIFEST_FLAG IS NULL THEN 0 ELSE 1 END AS FIRST_MANIFEST_KBN ");
            sql.Append("      , CASE WHEN DT_PT_R18.FIRST_MANIFEST_FLAG IS NULL THEN '一次' ELSE '二次' END AS FIRST_MANIFEST_KBN_NAME ");
            sql.Append("      , DT_PT_R18.LAST_SBN_END_DATE ");
            sql.Append("      , CASE WHEN (DT_PT_R18.LAST_SBN_END_DATE IS NULL OR DT_PT_R18.LAST_SBN_END_DATE = '') THEN NULL ELSE CONVERT(DATETIME,DT_PT_R18.LAST_SBN_END_DATE) END AS LAST_SBN_END_DATE_CNV ");
            sql.Append("      , DT_PT_R18.LAST_SBN_END_REP_DATE ");
            sql.Append("      , DT_PT_R18.SHUSEI_DATE ");
            sql.Append("      , DT_PT_R18.CANCEL_FLAG ");
            sql.Append("      , DT_PT_R18.CANCEL_DATE ");
            sql.Append("      , DT_PT_R18.LAST_UPDATE_DATE ");
            sql.Append("      , DT_PT_R18.YUUGAI_CNT ");
            sql.Append("      , DT_PT_R18.UPN_ROUTE_CNT ");
            sql.Append("      , DT_PT_R18.LAST_SBN_PLAN_CNT ");
            sql.Append("      , DT_PT_R18.LAST_SBN_CNT ");
            sql.Append("      , DT_PT_R18.RENRAKU_CNT ");
            sql.Append("      , DT_PT_R18.BIKOU_CNT ");
            sql.Append("      , DT_PT_R18.FIRST_MANIFEST_CNT ");
            sql.Append("      , DT_PT_R18.HST_GYOUSHA_CD ");
            sql.Append("      , DT_PT_R18.HST_GENBA_CD ");
            sql.Append("      , DT_PT_R18.SBN_GYOUSHA_CD ");
            sql.Append("      , DT_PT_R18.SBN_GYOUSHA_CD AS SBN_SHA_CODE");
            sql.Append("      , DT_PT_R18.SBN_GENBA_CD ");
            sql.Append("      , DT_PT_R18.NO_REP_SBN_EDI_MEMBER_ID ");
            sql.Append("      , DT_PT_R18.HAIKI_NAME_CD ");
            sql.Append("      , DT_PT_R18.SBN_HOUHOU_CD ");
            sql.Append("      , DT_PT_R18.HOUKOKU_TANTOUSHA_CD ");
            sql.Append("      , DT_PT_R18.SBN_TANTOUSHA_CD ");
            sql.Append("      , DT_PT_R18.UPN_TANTOUSHA_CD ");
            sql.Append("      , DT_PT_R18.SHARYOU_CD ");
            sql.Append("      , DT_PT_R18.KANSAN_SUU ");
            sql.Append("      , DT_PT_R18.CREATE_USER ");
            sql.Append("      , DT_PT_R18.CREATE_DATE ");
            sql.Append("      , DT_PT_R18.CREATE_PC ");
            sql.Append("      , DT_PT_R18.UPDATE_USER ");
            sql.Append("      , DT_PT_R18.UPDATE_DATE ");
            sql.Append("      , DT_PT_R18.UPDATE_PC ");
            sql.Append("      , DT_PT_R18.DELETE_FLG ");
            sql.Append("      , DT_PT_R18.TIME_STAMP ");
            sql.Append("      , CAST(DT_PT_R18.TIME_STAMP AS int ) AS TIME_STAMP_CNV ");
            sql.Append("   FROM DT_PT_R18 WITH(NOLOCK) ");
            sql.Append(" ) AS DT_PT_R18 ");

            sql.Append("   LEFT OUTER JOIN DT_PT_R02 AS DT_PT_R02_1 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R02_1.SYSTEM_ID  ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R02_1.SEQ ");
            sql.Append("    AND DT_PT_R02_1.REC_SEQ = 1 ");

            sql.Append("   LEFT OUTER JOIN DT_PT_R02 AS DT_PT_R02_2 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R02_2.SYSTEM_ID  ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R02_2.SEQ ");
            sql.Append("    AND DT_PT_R02_2.REC_SEQ = 2 ");

            sql.Append("   LEFT OUTER JOIN DT_PT_R02 AS DT_PT_R02_3 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R02_3.SYSTEM_ID  ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R02_3.SEQ ");
            sql.Append("    AND DT_PT_R02_3.REC_SEQ = 3 ");

            sql.Append("   LEFT OUTER JOIN DT_PT_R02 AS DT_PT_R02_4 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R02_4.SYSTEM_ID  ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R02_4.SEQ ");
            sql.Append("    AND DT_PT_R02_4.REC_SEQ = 4 ");

            sql.Append("   LEFT OUTER JOIN DT_PT_R02 AS DT_PT_R02_5 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R02_5.SYSTEM_ID  ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R02_5.SEQ ");
            sql.Append("    AND DT_PT_R02_5.REC_SEQ = 5 ");

            sql.Append("   LEFT OUTER JOIN DT_PT_R02 AS DT_PT_R02_6 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R02_6.SYSTEM_ID  ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R02_6.SEQ ");
            sql.Append("    AND DT_PT_R02_6.REC_SEQ = 6 ");

            sql.Append("   LEFT OUTER JOIN ( ");
            sql.Append("     SELECT DT_PT_R04.SYSTEM_ID  ");
            sql.Append("          , DT_PT_R04.SEQ ");
            sql.Append("          , DT_PT_R04.LAST_SBN_GYOUSHA_CD ");
            sql.Append("          , DT_PT_R04.LAST_SBN_GENBA_CD ");
            sql.Append("          , DT_PT_R04.LAST_SBN_JOU_NAME ");
            sql.Append("          , DT_PT_R04.LAST_SBN_JOU_POST ");
            sql.Append("          , DT_PT_R04.LAST_SBN_JOU_TEL ");
            sql.Append("          , DT_PT_R04.LAST_SBN_JOU_ADDRESS1 + DT_PT_R04.LAST_SBN_JOU_ADDRESS2 + DT_PT_R04.LAST_SBN_JOU_ADDRESS3 + DT_PT_R04.LAST_SBN_JOU_ADDRESS4 AS LAST_SBN_JOU_ADDRESS ");
            sql.Append("       FROM DT_PT_R04 WITH(NOLOCK) ");
            sql.Append("      WHERE DT_PT_R04.REC_SEQ = 1 ");
            sql.Append("     )DT_PT_R04_1 ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R04_1.SYSTEM_ID  ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R04_1.SEQ ");

            sql.Append("   LEFT OUTER JOIN DT_PT_R05 AS DT_PT_R05_1 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R05_1.SYSTEM_ID  ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R05_1.SEQ ");
            sql.Append("    AND DT_PT_R05_1.RENRAKU_ID_NO = 1 ");

            sql.Append("   LEFT OUTER JOIN DT_PT_R05 AS DT_PT_R05_2 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R05_2.SYSTEM_ID  ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R05_2.SEQ ");
            sql.Append("    AND DT_PT_R05_2.RENRAKU_ID_NO = 2 ");

            sql.Append("   LEFT OUTER JOIN DT_PT_R05 AS DT_PT_R05_3 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R05_3.SYSTEM_ID  ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R05_3.SEQ ");
            sql.Append("    AND DT_PT_R05_3.RENRAKU_ID_NO = 3 ");

            sql.Append("   LEFT OUTER JOIN DT_PT_R06 AS DT_PT_R06_1 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R06_1.SYSTEM_ID  ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R06_1.SEQ ");
            sql.Append("    AND DT_PT_R06_1.BIKOU_NO = 1 ");

            sql.Append("   LEFT OUTER JOIN DT_PT_R06 AS DT_PT_R06_2 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R06_2.SYSTEM_ID  ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R06_2.SEQ ");
            sql.Append("    AND DT_PT_R06_2.BIKOU_NO = 2 ");

            sql.Append("   LEFT OUTER JOIN DT_PT_R06 AS DT_PT_R06_3 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R06_3.SYSTEM_ID  ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R06_3.SEQ ");
            sql.Append("    AND DT_PT_R06_3.BIKOU_NO = 3 ");

            sql.Append("   LEFT OUTER JOIN DT_PT_R06 AS DT_PT_R06_4 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R06_4.SYSTEM_ID  ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R06_4.SEQ ");
            sql.Append("    AND DT_PT_R06_4.BIKOU_NO = 4 ");

            sql.Append("   LEFT OUTER JOIN DT_PT_R06 AS DT_PT_R06_5 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R06_5.SYSTEM_ID  ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R06_5.SEQ ");
            sql.Append("    AND DT_PT_R06_5.BIKOU_NO = 5 ");

            //sql.Append("   LEFT OUTER JOIN DT_PT_R06 AS DT_PT_R06_6 WITH(NOLOCK) ");
            //sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R06_6.SYSTEM_ID  ");
            //sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R06_6.SEQ ");
            //sql.Append("    AND DT_PT_R06_6.BIKOU_NO = 6 ");

            sql.Append("   LEFT OUTER JOIN DT_PT_R13 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R13.SYSTEM_ID  ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R13.SEQ ");
            sql.Append("    AND DT_PT_R13.REC_SEQ = 1 ");

            sql.Append("   LEFT OUTER JOIN M_DENSHI_HAIKI_SHURUI WITH(NOLOCK) ");
            sql.Append("     ON (DT_PT_R18.HAIKI_DAI_CODE + DT_PT_R18.HAIKI_CHU_CODE + DT_PT_R18.HAIKI_SHO_CODE) = M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_CD ");

            sql.Append("   LEFT OUTER JOIN M_DENSHI_HAIKI_NAME WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R18.HST_SHA_EDI_MEMBER_ID = M_DENSHI_HAIKI_NAME.EDI_MEMBER_ID ");
            sql.Append("    AND DT_PT_R18.HAIKI_NAME_CD = M_DENSHI_HAIKI_NAME.HAIKI_NAME_CD ");

            sql.Append("   LEFT OUTER JOIN M_UNIT WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R18.HAIKI_UNIT_CODE = M_UNIT.UNIT_CD ");

            sql.Append("   LEFT OUTER JOIN M_SHOBUN_HOUHOU WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R18.SBN_HOUHOU_CD = M_SHOBUN_HOUHOU.SHOBUN_HOUHOU_CD ");

            #region 区間1

            //区間1：電子マニフェストパターン運搬
            sql.Append(" LEFT OUTER JOIN ( ");
            sql.Append("     SELECT DT_PT_R19.SYSTEM_ID ");
            sql.Append("          , DT_PT_R19.SEQ ");
            sql.Append("          , DT_PT_R19.UPN_GYOUSHA_CD ");
            sql.Append("          , DT_PT_R19.UPN_GYOUSHA_CD AS UPN_SHA_CD");
            sql.Append("          , DT_PT_R19.UPN_SHA_NAME ");
            sql.Append("          , DT_PT_R19.UPN_SHA_NAME AS UPN_GYOUSHA_NAME");
            sql.Append("          , DT_PT_R19.UPN_SHA_POST ");
            sql.Append("          , DT_PT_R19.UPN_SHA_TEL ");
            sql.Append("          , DT_PT_R19.UPN_SHA_ADDRESS1 + DT_PT_R19.UPN_SHA_ADDRESS2 + DT_PT_R19.UPN_SHA_ADDRESS3 + DT_PT_R19.UPN_SHA_ADDRESS4 AS UPN_SHA_ADDRESS ");
            sql.Append("          , DT_PT_R19.UPN_WAY_CODE ");
            sql.Append("          , DT_PT_R19.SHARYOU_CD ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_KBN ");
            sql.Append("          , CASE DT_PT_R19.UPNSAKI_JOU_KBN WHEN 1 THEN '積替・保管施設' WHEN 2 THEN '処分（中間）' WHEN 3 THEN '処分（最終）' WHEN 4 THEN '処分（中間＋最終）' END AS UPNSAKI_JOU_KBN_NAME ");
            sql.Append("          , DT_PT_R19.UPNSAKI_GYOUSHA_CD ");
            sql.Append("          , DT_PT_R19.UPNSAKI_GENBA_CD ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_NAME ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_POST ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_TEL ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_ADDRESS1 + DT_PT_R19.UPNSAKI_JOU_ADDRESS2 + DT_PT_R19.UPNSAKI_JOU_ADDRESS3 + DT_PT_R19.UPNSAKI_JOU_ADDRESS4 AS UPNSAKI_JOU_ADDRESS ");
            sql.Append("          , DT_PT_R19.UPN_TANTOUSHA_CD ");
            sql.Append("          , DT_PT_R19.UPN_TAN_NAME ");
            sql.Append("          , CASE WHEN (DT_PT_R19.UPN_END_DATE IS NULL OR DT_PT_R19.UPN_END_DATE = '') THEN NULL ELSE CONVERT(DATETIME,DT_PT_R19.UPN_END_DATE) END AS UPN_END_DATE ");
            sql.Append("       FROM DT_PT_R19 WITH(NOLOCK) ");
            sql.Append("      WHERE DT_PT_R19.UPN_ROUTE_NO = 1 ");
            sql.Append("     ) AS DT_PT_R19_1 ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R19_1.SYSTEM_ID ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R19_1.SEQ ");

            //区間1：運搬方法
            sql.Append("   LEFT OUTER JOIN M_UNPAN_HOUHOU AS M_UNPAN_HOUHOU_1 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R19_1.UPN_WAY_CODE = M_UNPAN_HOUHOU_1.UNPAN_HOUHOU_CD ");

            //区間1：車輛
            sql.Append("   LEFT OUTER JOIN M_SHARYOU AS M_SHARYOU_1 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R19_1.UPN_GYOUSHA_CD = M_SHARYOU_1.GYOUSHA_CD ");
            sql.Append("    AND DT_PT_R19_1.SHARYOU_CD = M_SHARYOU_1.SHARYOU_CD ");

            #endregion

            #region 区間2

            //区間2：電子マニフェストパターン運搬
            sql.Append(" LEFT OUTER JOIN ( ");
            sql.Append("     SELECT DT_PT_R19.SYSTEM_ID ");
            sql.Append("          , DT_PT_R19.SEQ ");
            sql.Append("          , DT_PT_R19.UPN_GYOUSHA_CD ");
            sql.Append("          , DT_PT_R19.UPN_GYOUSHA_CD AS UPN_SHA_CD");
            sql.Append("          , DT_PT_R19.UPN_SHA_NAME ");
            sql.Append("          , DT_PT_R19.UPN_SHA_NAME AS UPN_GYOUSHA_NAME");
            sql.Append("          , DT_PT_R19.UPN_SHA_POST ");
            sql.Append("          , DT_PT_R19.UPN_SHA_TEL ");
            sql.Append("          , DT_PT_R19.UPN_SHA_ADDRESS1 + DT_PT_R19.UPN_SHA_ADDRESS2 + DT_PT_R19.UPN_SHA_ADDRESS3 + DT_PT_R19.UPN_SHA_ADDRESS4 AS UPN_SHA_ADDRESS ");
            sql.Append("          , DT_PT_R19.UPN_WAY_CODE ");
            sql.Append("          , DT_PT_R19.SHARYOU_CD ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_KBN ");
            sql.Append("          , CASE DT_PT_R19.UPNSAKI_JOU_KBN WHEN 1 THEN '積替・保管施設' WHEN 2 THEN '処分（中間）' WHEN 3 THEN '処分（最終）' WHEN 4 THEN '処分（中間＋最終）' END AS UPNSAKI_JOU_KBN_NAME ");
            sql.Append("          , DT_PT_R19.UPNSAKI_GYOUSHA_CD ");
            sql.Append("          , DT_PT_R19.UPNSAKI_GENBA_CD ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_NAME ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_POST ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_TEL ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_ADDRESS1 + DT_PT_R19.UPNSAKI_JOU_ADDRESS2 + DT_PT_R19.UPNSAKI_JOU_ADDRESS3 + DT_PT_R19.UPNSAKI_JOU_ADDRESS4 AS UPNSAKI_JOU_ADDRESS ");
            sql.Append("          , DT_PT_R19.UPN_TANTOUSHA_CD ");
            sql.Append("          , DT_PT_R19.UPN_TAN_NAME ");
            sql.Append("          , CASE WHEN (DT_PT_R19.UPN_END_DATE IS NULL OR DT_PT_R19.UPN_END_DATE = '') THEN NULL ELSE CONVERT(DATETIME,DT_PT_R19.UPN_END_DATE) END AS UPN_END_DATE ");
            sql.Append("       FROM DT_PT_R19 WITH(NOLOCK) ");
            sql.Append("      WHERE DT_PT_R19.UPN_ROUTE_NO = 2 ");
            sql.Append("     ) AS DT_PT_R19_2 ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R19_2.SYSTEM_ID ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R19_2.SEQ ");

            //区間2：運搬方法
            sql.Append("   LEFT OUTER JOIN M_UNPAN_HOUHOU AS M_UNPAN_HOUHOU_2 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R19_2.UPN_WAY_CODE = M_UNPAN_HOUHOU_2.UNPAN_HOUHOU_CD ");

            //区間2：車輛
            sql.Append("   LEFT OUTER JOIN M_SHARYOU AS M_SHARYOU_2 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R19_2.UPN_GYOUSHA_CD = M_SHARYOU_2.GYOUSHA_CD ");
            sql.Append("    AND DT_PT_R19_2.SHARYOU_CD = M_SHARYOU_2.SHARYOU_CD ");

            #endregion

            #region 区間3

            //区間3：電子マニフェストパターン運搬
            sql.Append("  ");
            sql.Append(" LEFT OUTER JOIN ( ");
            sql.Append("     SELECT DT_PT_R19.SYSTEM_ID ");
            sql.Append("          , DT_PT_R19.SEQ ");
            sql.Append("          , DT_PT_R19.UPN_GYOUSHA_CD ");
            sql.Append("          , DT_PT_R19.UPN_GYOUSHA_CD AS UPN_SHA_CD");
            sql.Append("          , DT_PT_R19.UPN_SHA_NAME ");
            sql.Append("          , DT_PT_R19.UPN_SHA_NAME AS UPN_GYOUSHA_NAME");
            sql.Append("          , DT_PT_R19.UPN_SHA_POST ");
            sql.Append("          , DT_PT_R19.UPN_SHA_TEL ");
            sql.Append("          , DT_PT_R19.UPN_SHA_ADDRESS1 + DT_PT_R19.UPN_SHA_ADDRESS2 + DT_PT_R19.UPN_SHA_ADDRESS3 + DT_PT_R19.UPN_SHA_ADDRESS4 AS UPN_SHA_ADDRESS ");
            sql.Append("          , DT_PT_R19.UPN_WAY_CODE ");
            sql.Append("          , DT_PT_R19.SHARYOU_CD ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_KBN ");
            sql.Append("          , CASE DT_PT_R19.UPNSAKI_JOU_KBN WHEN 1 THEN '積替・保管施設' WHEN 2 THEN '処分（中間）' WHEN 3 THEN '処分（最終）' WHEN 4 THEN '処分（中間＋最終）' END AS UPNSAKI_JOU_KBN_NAME ");
            sql.Append("          , DT_PT_R19.UPNSAKI_GYOUSHA_CD ");
            sql.Append("          , DT_PT_R19.UPNSAKI_GENBA_CD ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_NAME ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_POST ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_TEL ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_ADDRESS1 + DT_PT_R19.UPNSAKI_JOU_ADDRESS2 + DT_PT_R19.UPNSAKI_JOU_ADDRESS3 + DT_PT_R19.UPNSAKI_JOU_ADDRESS4 AS UPNSAKI_JOU_ADDRESS ");
            sql.Append("          , DT_PT_R19.UPN_TANTOUSHA_CD ");
            sql.Append("          , DT_PT_R19.UPN_TAN_NAME ");
            sql.Append("          , CASE WHEN (DT_PT_R19.UPN_END_DATE IS NULL OR DT_PT_R19.UPN_END_DATE = '') THEN NULL ELSE CONVERT(DATETIME,DT_PT_R19.UPN_END_DATE) END AS UPN_END_DATE ");
            sql.Append("       FROM DT_PT_R19 WITH(NOLOCK) ");
            sql.Append("      WHERE DT_PT_R19.UPN_ROUTE_NO = 3 ");
            sql.Append(" ) AS DT_PT_R19_3 ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R19_3.SYSTEM_ID ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R19_3.SEQ ");

            //区間3：運搬方法
            sql.Append("   LEFT OUTER JOIN M_UNPAN_HOUHOU AS M_UNPAN_HOUHOU_3 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R19_3.UPN_WAY_CODE = M_UNPAN_HOUHOU_3.UNPAN_HOUHOU_CD ");

            //区間3：車輛
            sql.Append("   LEFT OUTER JOIN M_SHARYOU AS M_SHARYOU_3 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R19_3.UPN_GYOUSHA_CD = M_SHARYOU_3.GYOUSHA_CD ");
            sql.Append("    AND DT_PT_R19_3.SHARYOU_CD = M_SHARYOU_3.SHARYOU_CD ");

            #endregion

            #region 区間4

            //区間4：電子マニフェストパターン運搬
            sql.Append(" LEFT OUTER JOIN ( ");
            sql.Append("     SELECT DT_PT_R19.SYSTEM_ID ");
            sql.Append("          , DT_PT_R19.SEQ ");
            sql.Append("          , DT_PT_R19.UPN_GYOUSHA_CD ");
            sql.Append("          , DT_PT_R19.UPN_GYOUSHA_CD AS UPN_SHA_CD");
            sql.Append("          , DT_PT_R19.UPN_SHA_NAME ");
            sql.Append("          , DT_PT_R19.UPN_SHA_NAME AS UPN_GYOUSHA_NAME");
            sql.Append("          , DT_PT_R19.UPN_SHA_POST ");
            sql.Append("          , DT_PT_R19.UPN_SHA_TEL ");
            sql.Append("          , DT_PT_R19.UPN_SHA_ADDRESS1 + DT_PT_R19.UPN_SHA_ADDRESS2 + DT_PT_R19.UPN_SHA_ADDRESS3 + DT_PT_R19.UPN_SHA_ADDRESS4 AS UPN_SHA_ADDRESS ");
            sql.Append("          , DT_PT_R19.UPN_WAY_CODE ");
            sql.Append("          , DT_PT_R19.SHARYOU_CD ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_KBN ");
            sql.Append("          , CASE DT_PT_R19.UPNSAKI_JOU_KBN WHEN 1 THEN '積替・保管施設' WHEN 2 THEN '処分（中間）' WHEN 3 THEN '処分（最終）' WHEN 4 THEN '処分（中間＋最終）' END AS UPNSAKI_JOU_KBN_NAME ");
            sql.Append("          , DT_PT_R19.UPNSAKI_GYOUSHA_CD ");
            sql.Append("          , DT_PT_R19.UPNSAKI_GENBA_CD ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_NAME ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_POST ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_TEL ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_ADDRESS1 + DT_PT_R19.UPNSAKI_JOU_ADDRESS2 + DT_PT_R19.UPNSAKI_JOU_ADDRESS3 + DT_PT_R19.UPNSAKI_JOU_ADDRESS4 AS UPNSAKI_JOU_ADDRESS ");
            sql.Append("          , DT_PT_R19.UPN_TANTOUSHA_CD ");
            sql.Append("          , DT_PT_R19.UPN_TAN_NAME ");
            sql.Append("          , CASE WHEN (DT_PT_R19.UPN_END_DATE IS NULL OR DT_PT_R19.UPN_END_DATE = '') THEN NULL ELSE CONVERT(DATETIME,DT_PT_R19.UPN_END_DATE) END AS UPN_END_DATE ");
            sql.Append("       FROM DT_PT_R19 WITH(NOLOCK) ");
            sql.Append("      WHERE DT_PT_R19.UPN_ROUTE_NO = 4 ");
            sql.Append(" ) AS DT_PT_R19_4 ");

            //区間4：運搬方法
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R19_4.SYSTEM_ID ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R19_4.SEQ ");
            sql.Append("   LEFT OUTER JOIN M_UNPAN_HOUHOU AS M_UNPAN_HOUHOU_4 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R19_4.UPN_WAY_CODE = M_UNPAN_HOUHOU_4.UNPAN_HOUHOU_CD ");

            //区間4：車輛
            sql.Append("   LEFT OUTER JOIN M_SHARYOU AS M_SHARYOU_4 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R19_4.UPN_GYOUSHA_CD = M_SHARYOU_4.GYOUSHA_CD ");
            sql.Append("    AND DT_PT_R19_4.SHARYOU_CD = M_SHARYOU_4.SHARYOU_CD ");

            #endregion

            #region 区間5

            //区間5：電子マニフェストパターン運搬
            sql.Append(" LEFT OUTER JOIN ( ");
            sql.Append("     SELECT DT_PT_R19.SYSTEM_ID ");
            sql.Append("          , DT_PT_R19.SEQ ");
            sql.Append("          , DT_PT_R19.UPN_GYOUSHA_CD ");
            sql.Append("          , DT_PT_R19.UPN_GYOUSHA_CD AS UPN_SHA_CD");
            sql.Append("          , DT_PT_R19.UPN_SHA_NAME ");
            sql.Append("          , DT_PT_R19.UPN_SHA_NAME AS UPN_GYOUSHA_NAME");
            sql.Append("          , DT_PT_R19.UPN_SHA_POST ");
            sql.Append("          , DT_PT_R19.UPN_SHA_TEL ");
            sql.Append("          , DT_PT_R19.UPN_SHA_ADDRESS1 + DT_PT_R19.UPN_SHA_ADDRESS2 + DT_PT_R19.UPN_SHA_ADDRESS3 + DT_PT_R19.UPN_SHA_ADDRESS4 AS UPN_SHA_ADDRESS ");
            sql.Append("          , DT_PT_R19.UPN_WAY_CODE ");
            sql.Append("          , DT_PT_R19.SHARYOU_CD ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_KBN ");
            sql.Append("          , CASE DT_PT_R19.UPNSAKI_JOU_KBN WHEN 1 THEN '積替・保管施設' WHEN 2 THEN '処分（中間）' WHEN 3 THEN '処分（最終）' WHEN 4 THEN '処分（中間＋最終）' END AS UPNSAKI_JOU_KBN_NAME ");
            sql.Append("          , DT_PT_R19.UPNSAKI_GYOUSHA_CD ");
            sql.Append("          , DT_PT_R19.UPNSAKI_GENBA_CD ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_NAME ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_POST ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_TEL ");
            sql.Append("          , DT_PT_R19.UPNSAKI_JOU_ADDRESS1 + DT_PT_R19.UPNSAKI_JOU_ADDRESS2 + DT_PT_R19.UPNSAKI_JOU_ADDRESS3 + DT_PT_R19.UPNSAKI_JOU_ADDRESS4 AS UPNSAKI_JOU_ADDRESS ");
            sql.Append("          , DT_PT_R19.UPN_TANTOUSHA_CD ");
            sql.Append("          , DT_PT_R19.UPN_TAN_NAME ");
            sql.Append("          , CASE WHEN (DT_PT_R19.UPN_END_DATE IS NULL OR DT_PT_R19.UPN_END_DATE = '') THEN NULL ELSE CONVERT(DATETIME,DT_PT_R19.UPN_END_DATE) END AS UPN_END_DATE ");
            sql.Append("       FROM DT_PT_R19 WITH(NOLOCK) ");
            sql.Append("      WHERE DT_PT_R19.UPN_ROUTE_NO = 5 ");
            sql.Append(" ) AS DT_PT_R19_5 ");
            sql.Append("     ON DT_PT_R18.SYSTEM_ID = DT_PT_R19_5.SYSTEM_ID ");
            sql.Append("    AND DT_PT_R18.SEQ = DT_PT_R19_5.SEQ ");

            //区間5：運搬方法
            sql.Append("   LEFT OUTER JOIN M_UNPAN_HOUHOU AS M_UNPAN_HOUHOU_5 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R19_5.UPN_WAY_CODE = M_UNPAN_HOUHOU_5.UNPAN_HOUHOU_CD ");

            //区間5：車輛
            sql.Append("   LEFT OUTER JOIN M_SHARYOU AS M_SHARYOU_5 WITH(NOLOCK) ");
            sql.Append("     ON DT_PT_R19_5.UPN_GYOUSHA_CD = M_SHARYOU_5.GYOUSHA_CD ");
            sql.Append("    AND DT_PT_R19_5.SHARYOU_CD = M_SHARYOU_5.SHARYOU_CD ");

            #endregion

            // 20140529 syunrei No.730 マニフェストパターン一覧 start
            sql.Append(" LEFT OUTER JOIN T_MANIFEST_PT_PRT PRT");
            sql.Append("   ON (DT_PT_R18.SYSTEM_ID=PRT.SYSTEM_ID AND DT_PT_R18.SEQ=PRT.SEQ) ");
            sql.Append(" LEFT OUTER JOIN T_MANIFEST_PT_DETAIL_PRT");
            sql.Append("   ON(T_MANIFEST_PT_DETAIL_PRT.SYSTEM_ID=PRT.SYSTEM_ID AND T_MANIFEST_PT_DETAIL_PRT.SEQ=PRT.SEQ) ");
            // 20140529 syunrei No.730 マニフェストパターン一覧 end

            #endregion

            #region WHERE句

            sql.Append("  WHERE DT_PT_R18.DELETE_FLG = '" + DELETE_FLG + "'");

            //一括登録区分
            if (LIST_REGIST_KBN != string.Empty)
            {

            }

            //廃棄物区分CD
            if (HAIKI_KBN_CD != string.Empty)
            {
                
            }

            //一次マニフェスト区分
            if (FIRST_MANIFEST_KBN != string.Empty)
            {
                switch (FIRST_MANIFEST_KBN)
                {
                    case "false"://一次
                        sql.Append("   AND DT_PT_R18.FIRST_MANIFEST_FLAG IS NULL ");
                        break;

                    case "true"://二次
                        sql.Append("   AND DT_PT_R18.FIRST_MANIFEST_FLAG IS NOT NULL ");
                        break;
                }

            }


            // 20140529 syunrei No.730 マニフェストパターン一覧 start
            var sqlWhere = new StringBuilder();

            //パターン名
            if (PATTERN_NAME != string.Empty)
            {
                string sqlPatternName = SqlCreateUtility.CounterplanEscapeSequence(PATTERN_NAME);
                sqlWhere.AppendFormat("   AND DT_PT_R18.PATTERN_FURIGANA LIKE  N'%{0}%'", sqlPatternName);
            }

            //排出事業者名
            if (HST_GYOUSHA_NAME != string.Empty)
            {
                string sqlHstGyoushaName = SqlCreateUtility.CounterplanEscapeSequence(HST_GYOUSHA_NAME);
                sqlWhere.AppendFormat("   AND DT_PT_R18.HST_SHA_NAME LIKE N'%{0}%'", sqlHstGyoushaName);
            }

            //排出事業場名
            if (HST_GENBA_NAME != string.Empty)
            {
                string sqlHstGenbaName = SqlCreateUtility.CounterplanEscapeSequence(HST_GENBA_NAME);
                sqlWhere.AppendFormat("   AND DT_PT_R18.HST_JOU_NAME LIKE N'%{0}%'", sqlHstGenbaName);
            }

            //排出事業者
            if (HST_GYOUSHA_CD != string.Empty)
            {
                sqlWhere.AppendFormat("   AND DT_PT_R18.HST_GYOUSHA_CD LIKE N'%{0}%'", HST_GYOUSHA_CD);
            }

            //排出事業場
            if (HST_GENBA_CD != string.Empty)
            {
                sqlWhere.AppendFormat("   AND DT_PT_R18.HST_GENBA_CD LIKE N'%{0}%'", HST_GENBA_CD);
            }

            //運搬受託者
            if (HST_UNBAN_JUTAKU_CD != string.Empty)
            {
                sqlWhere.AppendFormat("   AND DT_PT_R19_1.UPN_SHA_CD LIKE N'%{0}%'", HST_UNBAN_JUTAKU_CD);
            }

            if (HST_UNBAN_JUTAKU_NAME != string.Empty)
            {
                string sqlHsUnbanJutakuName = SqlCreateUtility.CounterplanEscapeSequence(HST_UNBAN_JUTAKU_NAME);
                sqlWhere.AppendFormat("   AND DT_PT_R19_1.UPN_SHA_NAME LIKE N'%{0}%'", sqlHsUnbanJutakuName);
            }

            //処分受託者
            if (HST_SHOUBU_JUTAKU_CD != string.Empty)
            {
                sqlWhere.AppendFormat("   AND DT_PT_R18.SBN_GYOUSHA_CD LIKE N'%{0}%'", HST_SHOUBU_JUTAKU_CD);
            }

            if (HST_SHOUBU_JUTAKU_NAME != string.Empty)
            {
                string sqlHsShoubuJutakuName = SqlCreateUtility.CounterplanEscapeSequence(HST_SHOUBU_JUTAKU_NAME);
                sqlWhere.AppendFormat("   AND DT_PT_R18.SBN_GYOUSHA_NAME LIKE N'%{0}%'", sqlHsShoubuJutakuName);
            }

            //処分事業場⇒「運搬先の事業場　名称」と同じ
            if (HST_SHOUBU_JIGYOU_CD != string.Empty)
            {
                sqlWhere.AppendFormat("   AND DT_PT_R19_1.UPNSAKI_GENBA_CD LIKE N'%{0}%'", HST_SHOUBU_JIGYOU_CD);
            }

            if (HST_SHOUBU_JIGYOU_NAME != string.Empty)
            {
                string sqlHsShoubuJigyouName = SqlCreateUtility.CounterplanEscapeSequence(HST_SHOUBU_JIGYOU_NAME);
                sqlWhere.AppendFormat("   AND DT_PT_R19_1.UPNSAKI_JOU_NAME LIKE N'%{0}%'", sqlHsShoubuJigyouName);
            }

            if (!string.IsNullOrEmpty(sqlWhere.ToString()))
            {
                //検索条件を追加する
                sql.Append(sqlWhere.ToString());
            }

            ////パターン名
            //if (PATTERN_NAME != string.Empty)
            //{
            //    sql.Append("   AND DT_PT_R18.PATTERN_NAME LIKE N'%" + PATTERN_NAME + "%'");
            //}

            ////拠点
            //if (KYOTEN_CD != string.Empty)
            //{

            //}

            ////排出事業者名
            //if (HST_GYOUSHA_NAME != string.Empty)
            //{
            //    sql.Append("   AND DT_PT_R18.HST_SHA_NAME LIKE N'%" + HST_GYOUSHA_NAME + "%'");
            //}

            ////排出事業場名
            //if (HST_GENBA_NAME != string.Empty)
            //{
            //    sql.Append("   AND DT_PT_R18.HST_JOU_NAME LIKE N'%" + HST_GENBA_NAME + "%'");
            //}

            // 20140529 syunrei No.730 マニフェストパターン一覧 end

            #endregion

            #region ORDERBY句

            if (!string.IsNullOrEmpty(orderByQuery))
            {
                sql.Append(" ORDER BY ");
                sql.Append(this.orderByQuery);
            }

            #endregion

            this.createSql = sql.ToString();
            sql.Append("");

            this.SearchResult = dao_GetResult.getdateforstringsql(this.createSql);
            int count = this.SearchResult.Rows.Count;

            LogUtility.DebugMethodEnd(count);

            return count;
        }

        /// <summary>
        /// 電子マニフェストパターン　画面表示
        /// </summary>
        public void Set_Search_DPR18()
        {
            LogUtility.DebugMethodStart();

            //データ表示
            this.form.customDataGridView1.DataSource = null;

            //foreach (var s in LogicClass.RemoveColumnsName.Concat(LogicClass.removeColumnsPhysicalDenshi))
            //{
            //    if (this.SearchResult.Columns.OfType<DataColumn>().Any(col => col.ColumnName == s))
            //    {
            //        this.SearchResult.Columns.Remove(s);
            //    }
            //}

            this.form.ShowData();

             // 20140618 syunrei EV004876_マニフェストパターン一覧のアラート件数が効いていない start
            if (this.form.customDataGridView1.RowCount > 0 && this.form.customDataGridView1 != null)
            {
                // 20140604 syunrei No.730 マニフェストパターン一覧 start
                //this.form.customDataGridView1.Columns["SYSTEM_ID"].Visible = false;
                //this.form.customDataGridView1.Columns["SEQ"].Visible = false;
                ////TODO:排他制御の修正
                //this.form.customDataGridView1.Columns["TIME_STAMP"].Visible = false;

                //非表示列を隠す
                this.SetColumnsVisible(this.form.HaikiKbnCD, false);
                // 20140604 syunrei No.730 マニフェストパターン一覧 end
            }
            // 20140618 syunrei EV004876_マニフェストパターン一覧のアラート件数が効いていない end

            //読込データ件数
            this.header.ReadDataNumber.Text = this.SearchResult.Rows.Count.ToString();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面クリア
        /// </summary>
        public bool ClearScreen(String Kbn)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(Kbn);
                switch (Kbn)
                {
                    case "Initial"://初期表示
                        //タイトル
                        this.header.lb_title.Text = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_PATTERN_ICHIRAN);

                        ////廃棄物区分CD
                        //switch (this.form.HaikiKbnCD)
                        //{
                        //    case "1"://産廃（直行）
                        //        //一括入力
                        //        if (this.form.ListRegistKbn == "true")
                        //        {
                        //            this.form.crbHaikiKbnCd1.Enabled = true;
                        //            this.form.crbHaikiKbnCd2.Enabled = true;
                        //            this.form.crbHaikiKbnCd3.Enabled = true;
                        //            this.form.crbHaikiKbnCd4.Enabled = false;
                        //            this.form.crbHaikiKbnCd5.Enabled = true;
                        //        }
                        //        //単票入力
                        //        else if (this.form.ListRegistKbn == "false")
                        //        {
                        //            this.form.crbHaikiKbnCd1.Enabled = true;
                        //            this.form.crbHaikiKbnCd2.Enabled = false;
                        //            this.form.crbHaikiKbnCd3.Enabled = false;
                        //            this.form.crbHaikiKbnCd4.Enabled = false;
                        //            this.form.crbHaikiKbnCd5.Enabled = false;
                        //        }
                        //        this.form.HaikiKbnCD = "1";
                        //        break;

                        //    case "2"://建廃
                        //        //一括入力
                        //        if (this.form.ListRegistKbn == "true")
                        //        {
                        //            this.form.crbHaikiKbnCd1.Enabled = true;
                        //            this.form.crbHaikiKbnCd2.Enabled = true;
                        //            this.form.crbHaikiKbnCd3.Enabled = true;
                        //            this.form.crbHaikiKbnCd4.Enabled = false;
                        //            this.form.crbHaikiKbnCd5.Enabled = true;
                        //        }
                        //        //単票入力
                        //        else if (this.form.ListRegistKbn == "false")
                        //        {
                        //            this.form.crbHaikiKbnCd1.Enabled = false;
                        //            this.form.crbHaikiKbnCd2.Enabled = true;
                        //            this.form.crbHaikiKbnCd3.Enabled = false;
                        //            this.form.crbHaikiKbnCd4.Enabled = false;
                        //            this.form.crbHaikiKbnCd5.Enabled = false;
                        //        }
                        //        this.form.HaikiKbnCD = "3";
                        //        break;

                        //    case "3"://産廃（積替）
                        //        //一括入力
                        //        if (this.form.ListRegistKbn == "true")
                        //        {
                        //            this.form.crbHaikiKbnCd1.Enabled = true;
                        //            this.form.crbHaikiKbnCd2.Enabled = true;
                        //            this.form.crbHaikiKbnCd3.Enabled = true;
                        //            this.form.crbHaikiKbnCd4.Enabled = false;
                        //            this.form.crbHaikiKbnCd5.Enabled = true;
                        //        }
                        //        //単票入力
                        //        else if (this.form.ListRegistKbn == "false")
                        //        {
                        //            this.form.crbHaikiKbnCd1.Enabled = false;
                        //            this.form.crbHaikiKbnCd2.Enabled = false;
                        //            this.form.crbHaikiKbnCd3.Enabled = true;
                        //            this.form.crbHaikiKbnCd4.Enabled = false;
                        //            this.form.crbHaikiKbnCd5.Enabled = false;
                        //        }
                        //        this.form.HaikiKbnCD = "2";
                        //        break;

                        //    case "4"://電子
                        //        this.form.crbHaikiKbnCd1.Enabled = false;
                        //        this.form.crbHaikiKbnCd2.Enabled = false;
                        //        this.form.crbHaikiKbnCd3.Enabled = false;
                        //        this.form.crbHaikiKbnCd4.Enabled = true;
                        //        this.form.crbHaikiKbnCd5.Enabled = false;
                        //        this.form.HaikiKbnCD = "4";
                        //        break;

                        //    default://未指定
                        //        this.form.crbHaikiKbnCd1.Enabled = true;
                        //        this.form.crbHaikiKbnCd2.Enabled = true;
                        //        this.form.crbHaikiKbnCd3.Enabled = true;
                        //        this.form.crbHaikiKbnCd4.Enabled = false;
                        //        this.form.crbHaikiKbnCd5.Enabled = true;
                        //        this.form.HaikiKbnCD = "5";
                        //        break;
                        //}

                        //検索条件
                        this.form.searchString.Text = string.Empty;

                        //並び順ソートヘッダー
                        this.form.customSortHeader1.ClearCustomSortSetting();

                        //ヒント
                        this.footer.lb_hint.Text = string.Empty;

                        //処理No（ESC）
                        this.footer.txb_process.Text = "1";

                        break;

                    case "ClsSearchCondition"://検索条件をクリア
                        //拠点
                        Properties.Settings.Default.KYOTEN_CD = string.Empty;
                        Properties.Settings.Default.KYOTEN_NAME = string.Empty;

                        //アラート件数
                        this.header.NumberAlert = this.header.InitialNumberAlert;

                        ////廃棄物区分CD
                        //switch (this.form.HaikiKbnCD)
                        //{
                        //    case "1"://産廃（直行）
                        //        //一括入力
                        //        if (this.form.ListRegistKbn == "true")
                        //        {
                        //            this.form.HaikiKbnCD = "5";
                        //        }
                        //        //単票入力
                        //        else if (this.form.ListRegistKbn == "false")
                        //        {
                        //            this.form.HaikiKbnCD = "1";
                        //        }
                        //        break;

                        //    case "2"://建廃
                        //        //一括入力
                        //        if (this.form.ListRegistKbn == "true")
                        //        {
                        //            this.form.HaikiKbnCD = "5";
                        //        }
                        //        //単票入力
                        //        else if (this.form.ListRegistKbn == "false")
                        //        {
                        //            this.form.HaikiKbnCD = "3";
                        //        }
                        //        break;

                        //    case "3"://産廃（積替）
                        //        //一括入力
                        //        if (this.form.ListRegistKbn == "true")
                        //        {
                        //            this.form.HaikiKbnCD = "5";
                        //        }
                        //        //単票入力
                        //        else if (this.form.ListRegistKbn == "false")
                        //        {
                        //            this.form.HaikiKbnCD = "2";
                        //        }
                        //        break;

                        //    case "4"://電子
                        //        this.form.HaikiKbnCD = "4";
                        //        break;

                        //    default://未指定
                        //        this.form.HaikiKbnCD = "5";
                        //        break;
                        //}

                        ////一次二次区分
                        //this.form.FirstManifestKbn = "false";
                        //Properties.Settings.Default.FIRST_MANIFEST_KBN = "1";

                        //パターン名
                        Properties.Settings.Default.PATTERN_NAME = string.Empty;

                        //排出事業者名
                        Properties.Settings.Default.HST_GYOUSHA_NAME = string.Empty;

                        //排出事業場名
                        Properties.Settings.Default.HST_GENBA_NAME = string.Empty;

                        //並び順ソートヘッダー
                        this.form.customSortHeader1.ClearCustomSortSetting();

                        //一覧
                        this.SearchResult.Clear();

                        // 20140529 syunrei No.730 マニフェストパターン一覧 start

                        //排出事業者CD
                        this.form.cantxt_HaisyutugyoshaCD.Text= string.Empty;
                        this.form.HST_GYOUSHA_NAME.Text = string.Empty;
                        this.form.ctxt_HaisyutugyoshaName.Text = string.Empty;
                        //排出事業場CD
                        this.form.cantxt_HaisyutugenbaCD.Text= string.Empty;
                        this.form.HST_GENBA_NAME.Text = string.Empty;
                        this.form.ctxt_HaisyutugenbaName.Text = string.Empty;

                        //運搬受託者
                        this.form.cantxt_UnpangyoshaCD.Text= string.Empty;                        
                        //運搬受託者名
                        this.form.HST_UNBAN_JUTAKU_NAME.Text= string.Empty;
                        this.form.ctxt_UnpangyoshaName.Text = string.Empty;

                        //処分受託者
                        this.form.cantxt_ShobungyoshaCD.Text= string.Empty;                     
                        //処分受託者名
                        this.form.HST_SHOUBU_JUTAKU_NAME.Text = string.Empty;
                        this.form.ctxt_ShobungyoshaName.Text = string.Empty;
                        //処分事業場
                        this.form.cantxt_ShobunGenbaCD.Text= string.Empty;
                        //処分事業場名
                        this.form.HST_SHOUBU_JIGYOU_NAME.Text= string.Empty;
                        this.form.ctxt_ShobunGenbaName.Text = string.Empty;

                        // 20140529 syunrei No.730 マニフェストパターン一覧 end

                        break;

                    default:
                        break;
                }

                ////拠点
                //this.header.KYOTEN_CD.Text = Properties.Settings.Default.KYOTEN_CD;
                //this.header.KYOTEN_NAME.Text = Properties.Settings.Default.KYOTEN_NAME;

                //読込データ件数
                this.header.ReadDataNumber.Text = "0";

                ////一次二次区分
                //this.form.FirstManifestKbn = Properties.Settings.Default.FIRST_MANIFEST_KBN;

                //アラート件数
                this.header.AlertNumber.Text = this.header.NumberAlert.ToString();

                //パターン名
                this.form.PATTERN_NAME.Text = Properties.Settings.Default.PATTERN_NAME;

                //排出事業者名
                this.form.HST_GYOUSHA_NAME.Text = Properties.Settings.Default.HST_GYOUSHA_NAME;

                //排出事業場名
                this.form.HST_GENBA_NAME.Text = Properties.Settings.Default.HST_GENBA_NAME;

                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearScreen", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 更新
        /// </summary>
        [Transaction]
        public void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart();

            try
            {

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

        /// <summary>
        /// 処理No フォーカス移動
        /// </summary>
        public void SetFocusTxbProcess()
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.footer.txb_process.Focus();
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

        /// <summary>
        /// 処理No ボタン選択
        /// </summary>
        public void SelectButton()
        {
            LogUtility.DebugMethodStart();

            try
            {
                switch (this.footer.txb_process.Text)
                {
                    case "1"://【1】パターン一覧
                        this.footer.bt_process1.PerformClick();
                        break;

                    case "2"://【2】検索条件設定
                        this.footer.bt_process2.PerformClick();
                        break;
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
        }

        /// <summary>
        /// 適用
        /// </summary>
        public void SelectData()
        {
            LogUtility.DebugMethodStart();
            // 20140609 syunrei EV004332  start   
            string messContent ="";
            // 20140609 syunrei EV004332  end

            try
            {
                int i = this.form.customDataGridView1.SelectedCells[0].RowIndex;

                if ("4".Equals(this.form.HaikiKbnCD))
                {
                    // 電子マニ
                }
                else
                {
                    // 紙マニ
                    //適用期間をチェック
                    //if (this.mlogic.ChkTekiyouKikan(
                    //        (Int64)this.form.customDataGridView1.Rows[i].Cells["SYSTEM_ID"].Value, 
                    //        (Int32)this.form.customDataGridView1.Rows[i].Cells["SEQ"].Value,
                    //        !(this.form.HaikiKbnCD == "4"),ref messContent ))
                    //{
                    //    Message.MessageBoxUtility.MessageBoxShow("E171",messContent);//<Message id="E157" kubun="3">適用期間外の業者が含まれています。</Message>
                    //    return;
                    //}
                }

                this.form.ParamOut_SysID = this.form.customDataGridView1.Rows[i].Cells["SYSTEM_ID"].Value.ToString();
                //2013-11-06 Upd ogawamut IT No.133、No.134、No.135
                this.form.ParamOut_Seq = this.form.customDataGridView1.Rows[i].Cells["SEQ"].Value.ToString();

                //閉じるボタン
                this.footer.bt_func12.PerformClick();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SelectData", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// セッティングファイルへ保存
        /// </summary>
        public void SaveProperties()
        {
            LogUtility.DebugMethodStart();

            //拠点
            Properties.Settings.Default.KYOTEN_CD = this.header.KYOTEN_CD.Text;
            Properties.Settings.Default.KYOTEN_NAME = this.header.KYOTEN_NAME.Text;

            ////一次二次区分
            //switch (this.form.FirstManifestKbn)
            //{
            //    case "1":
            //    case "2":
            //        Properties.Settings.Default.FIRST_MANIFEST_KBN = this.form.FirstManifestKbn;
            //        break;

            //    default:
            //        Properties.Settings.Default.FIRST_MANIFEST_KBN = string.Empty;
            //        break;
            //}

            //パターン名
            Properties.Settings.Default.PATTERN_NAME = this.form.PATTERN_NAME.Text;

            //排出事業者名
            Properties.Settings.Default.HST_GYOUSHA_NAME = this.form.HST_GYOUSHA_NAME.Text;

            //排出事業場名
            Properties.Settings.Default.HST_GENBA_NAME = this.form.HST_GENBA_NAME.Text;

            Properties.Settings.Default.Save();

            LogUtility.DebugMethodEnd();
        }

        // 20140529 syunrei No.730 マニフェストパターン一覧 start
        internal void ChangeTitle()
        {
            string kbn;
            string firstOrSecond;
            string title ;

            //区分
            switch (this.form.HaikiKbnCD)
            {
                case "1"://産廃（直行）
                    kbn = "産廃マニフェスト(直行)";
                    break;
                case "2"://建廃
                    kbn = "建廃マニフェスト";
                    break;
                case "3"://産廃（積替）
                    kbn = "産廃マニフェスト(積替)";
                    break;
                case "4"://電子
                    kbn = "電子マニフェスト";
                    break;
                default: //5:全て と 空欄
                    kbn = "マニフェスト一括入力";
                    break;
            }

            //一次か二次か
            switch (this.form.FirstManifestKbn)
            {
                case "1"://1次
                case "false":
                    firstOrSecond = "一次";
                    break;
                case "2"://2次
                case "true":
                    firstOrSecond = "二次";
                    break;
                default: //空欄
                    firstOrSecond = "";
                    break;
            }

            //画面名
            title = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_PATTERN_ICHIRAN);

            //タイトルラベル変更
            this.header.lb_title.Text = kbn + firstOrSecond + title;
        }

        /// <summary>
        /// 非表示列を隠す
        /// </summary>
        public void SetColumnsVisible(string haikiKbn,bool blValue)
        {
            //共通項目
            this.form.customDataGridView1.Columns["SYSTEM_ID"].Visible = false;
            this.form.customDataGridView1.Columns["SEQ"].Visible = false;
            //TODO:排他制御の修正
            this.form.customDataGridView1.Columns["TIME_STAMP"].Visible = false;
            //廃棄区分より、非表示列を隠す
            switch (haikiKbn)
            {
                case "4"://電子

                    this.form.customDataGridView1.Columns["PATTERN_NAME"].Visible = blValue;
                    this.form.customDataGridView1.Columns["PATTERN_FURIGANA"].Visible = blValue;
                    this.form.customDataGridView1.Columns["MANIFEST_ID"].Visible = blValue;
                    this.form.customDataGridView1.Columns["MANIFEST_KBN"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SHOUNIN_FLAG"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HIKIWATASHI_DATE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["UPN_ENDREP_FLAG"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_ENDREP_FLAG"].Visible = blValue;
                    this.form.customDataGridView1.Columns["LAST_SBN_ENDREP_FLAG"].Visible = blValue;
                    this.form.customDataGridView1.Columns["KAKIN_DATE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["REGI_DATE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["UPN_SBN_REP_LIMIT_DATE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["LAST_SBN_REP_LIMIT_DATE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["RESV_LIMIT_DATE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_ENDREP_KBN"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HST_SHA_EDI_MEMBER_ID"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HST_SHA_NAME"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HST_SHA_POST"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HST_SHA_ADDRESS1"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HST_SHA_ADDRESS2"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HST_SHA_ADDRESS3"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HST_SHA_ADDRESS4"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HST_SHA_TEL"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HST_SHA_FAX"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HST_JOU_NAME"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HST_JOU_POST_NO"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HST_JOU_ADDRESS1"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HST_JOU_ADDRESS2"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HST_JOU_ADDRESS3"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HST_JOU_ADDRESS4"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HST_JOU_TEL"].Visible = blValue;
                    this.form.customDataGridView1.Columns["REGI_TAN"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HIKIWATASHI_TAN_NAME"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HAIKI_DAI_CODE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HAIKI_CHU_CODE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HAIKI_SHO_CODE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HAIKI_SAI_CODE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HAIKI_BUNRUI"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HAIKI_SHURUI"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HAIKI_NAME"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HAIKI_SUU"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HAIKI_UNIT_CODE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SUU_KAKUTEI_CODE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HAIKI_KAKUTEI_SUU"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HAIKI_KAKUTEI_UNIT_CODE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["NISUGATA_CODE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["NISUGATA_NAME"].Visible = blValue;
                    this.form.customDataGridView1.Columns["NISUGATA_SUU"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_SHA_MEMBER_ID"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_SHA_NAME"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_SHA_POST"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_SHA_ADDRESS1"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_SHA_ADDRESS2"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_SHA_ADDRESS3"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_SHA_ADDRESS4"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_SHA_TEL"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_SHA_FAX"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_SHA_KYOKA_ID"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SAI_SBN_SHA_MEMBER_ID"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SAI_SBN_SHA_NAME"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SAI_SBN_SHA_POST"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SAI_SBN_SHA_ADDRESS1"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SAI_SBN_SHA_ADDRESS2"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SAI_SBN_SHA_ADDRESS3"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SAI_SBN_SHA_ADDRESS4"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SAI_SBN_SHA_TEL"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SAI_SBN_SHA_FAX"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SAI_SBN_SHA_KYOKA_ID"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_WAY_CODE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_WAY_NAME"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_SHOUNIN_FLAG"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_END_DATE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HAIKI_IN_DATE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["RECEPT_SUU"].Visible = blValue;
                    this.form.customDataGridView1.Columns["RECEPT_UNIT_CODE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["UPN_TAN_NAME"].Visible = blValue;
                    this.form.customDataGridView1.Columns["CAR_NO"].Visible = blValue;
                    this.form.customDataGridView1.Columns["REP_TAN_NAME"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_TAN_NAME"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_END_REP_DATE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_REP_BIKOU"].Visible = blValue;
                    this.form.customDataGridView1.Columns["KENGEN_CODE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["LAST_SBN_JOU_KISAI_FLAG"].Visible = blValue;
                    this.form.customDataGridView1.Columns["FIRST_MANIFEST_FLAG"].Visible = blValue;
                    this.form.customDataGridView1.Columns["LAST_SBN_END_DATE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["LAST_SBN_END_REP_DATE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SHUSEI_DATE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["CANCEL_FLAG"].Visible = blValue;
                    this.form.customDataGridView1.Columns["CANCEL_DATE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["LAST_UPDATE_DATE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["YUUGAI_CNT"].Visible = blValue;
                    this.form.customDataGridView1.Columns["UPN_ROUTE_CNT"].Visible = blValue;
                    this.form.customDataGridView1.Columns["LAST_SBN_PLAN_CNT"].Visible = blValue;
                    this.form.customDataGridView1.Columns["LAST_SBN_CNT"].Visible = blValue;
                    this.form.customDataGridView1.Columns["RENRAKU_CNT"].Visible = blValue;
                    this.form.customDataGridView1.Columns["BIKOU_CNT"].Visible = blValue;
                    this.form.customDataGridView1.Columns["FIRST_MANIFEST_CNT"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HST_GYOUSHA_CD"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HST_GENBA_CD"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_GYOUSHA_CD"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_GENBA_CD"].Visible = blValue;
                    this.form.customDataGridView1.Columns["NO_REP_SBN_EDI_MEMBER_ID"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HAIKI_NAME_CD"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_HOUHOU_CD"].Visible = blValue;
                    this.form.customDataGridView1.Columns["HOUKOKU_TANTOUSHA_CD"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SBN_TANTOUSHA_CD"].Visible = blValue;
                    this.form.customDataGridView1.Columns["UPN_TANTOUSHA_CD"].Visible = blValue;
                    this.form.customDataGridView1.Columns["SHARYOU_CD"].Visible = blValue;
                    this.form.customDataGridView1.Columns["KANSAN_SUU"].Visible = blValue;
                    this.form.customDataGridView1.Columns["CREATE_USER"].Visible = blValue;
                    this.form.customDataGridView1.Columns["CREATE_DATE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["CREATE_PC"].Visible = blValue;
                    this.form.customDataGridView1.Columns["UPDATE_USER"].Visible = blValue;
                    this.form.customDataGridView1.Columns["UPDATE_DATE"].Visible = blValue;
                    this.form.customDataGridView1.Columns["UPDATE_PC"].Visible = blValue;
                    this.form.customDataGridView1.Columns["DELETE_FLG"].Visible = blValue;
                    this.form.customDataGridView1.Columns["USE_DEFAULT_KBN"].Visible = blValue;//155789                    
                    break;

                case "1"://産廃（直行）
                case "2"://建廃
                case "3"://産廃（積替）
                case "5":
                default:
                     this.form.customDataGridView1.Columns["LIST_REGIST_KBN"].Visible = blValue;
                     this.form.customDataGridView1.Columns["HAIKI_KBN_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["FIRST_MANIFEST_KBN"].Visible = blValue;
                     this.form.customDataGridView1.Columns["PATTERN_NAME"].Visible = blValue;
                     this.form.customDataGridView1.Columns["PATTERN_FURIGANA"].Visible = blValue;
                     this.form.customDataGridView1.Columns["USE_DEFAULT_KBN"].Visible = blValue;
                     this.form.customDataGridView1.Columns["KYOTEN_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["TORIHIKISAKI_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["JIZEN_NUMBER"].Visible = blValue;
                     this.form.customDataGridView1.Columns["JIZEN_DATE"].Visible = blValue;
                     this.form.customDataGridView1.Columns["KOUFU_DATE"].Visible = blValue;
                     this.form.customDataGridView1.Columns["KOUFU_KBN"].Visible = blValue;
                     this.form.customDataGridView1.Columns["MANIFEST_ID"].Visible = blValue;
                     this.form.customDataGridView1.Columns["SEIRI_ID"].Visible = blValue;
                     this.form.customDataGridView1.Columns["KOUFU_TANTOUSHA"].Visible = blValue;
                     this.form.customDataGridView1.Columns["KOUFU_TANTOUSHA_SHOZOKU"].Visible = blValue;
                     this.form.customDataGridView1.Columns["HST_GYOUSHA_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["HST_GYOUSHA_NAME"].Visible = blValue;
                     this.form.customDataGridView1.Columns["HST_GYOUSHA_POST"].Visible = blValue;
                     this.form.customDataGridView1.Columns["HST_GYOUSHA_TEL"].Visible = blValue;
                     this.form.customDataGridView1.Columns["HST_GYOUSHA_ADDRESS"].Visible = blValue;
                     this.form.customDataGridView1.Columns["HST_GENBA_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["HST_GENBA_NAME"].Visible = blValue;
                     this.form.customDataGridView1.Columns["HST_GENBA_POST"].Visible = blValue;
                     this.form.customDataGridView1.Columns["HST_GENBA_TEL"].Visible = blValue;
                     this.form.customDataGridView1.Columns["HST_GENBA_ADDRESS"].Visible = blValue;
                     this.form.customDataGridView1.Columns["BIKOU"].Visible = blValue;
                     this.form.customDataGridView1.Columns["KONGOU_SHURUI_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["HAIKI_SUU"].Visible = blValue;
                     this.form.customDataGridView1.Columns["HAIKI_UNIT_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["TOTAL_SUU"].Visible = blValue;
                     this.form.customDataGridView1.Columns["TOTAL_KANSAN_SUU"].Visible = blValue;
                     this.form.customDataGridView1.Columns["TOTAL_GENNYOU_SUU"].Visible = blValue;
                     this.form.customDataGridView1.Columns["CHUUKAN_HAIKI_KBN"].Visible = blValue;
                     this.form.customDataGridView1.Columns["CHUUKAN_HAIKI"].Visible = blValue;
                     this.form.customDataGridView1.Columns["LAST_SBN_YOTEI_KBN"].Visible = blValue;
                     this.form.customDataGridView1.Columns["LAST_SBN_YOTEI_GYOUSHA_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["LAST_SBN_YOTEI_GENBA_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["LAST_SBN_YOTEI_GENBA_NAME"].Visible = blValue;
                     this.form.customDataGridView1.Columns["LAST_SBN_YOTEI_GENBA_POST"].Visible = blValue;
                     this.form.customDataGridView1.Columns["LAST_SBN_YOTEI_GENBA_TEL"].Visible = blValue;
                     this.form.customDataGridView1.Columns["LAST_SBN_YOTEI_GENBA_ADDRESS"].Visible = blValue;
                     this.form.customDataGridView1.Columns["SBN_GYOUSHA_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["SBN_GYOUSHA_NAME"].Visible = blValue;
                     this.form.customDataGridView1.Columns["SBN_GYOUSHA_POST"].Visible = blValue;
                     this.form.customDataGridView1.Columns["SBN_GYOUSHA_TEL"].Visible = blValue;
                     this.form.customDataGridView1.Columns["SBN_GYOUSHA_ADDRESS"].Visible = blValue;
                     this.form.customDataGridView1.Columns["TMH_GYOUSHA_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["TMH_GYOUSHA_NAME"].Visible = blValue;
                     this.form.customDataGridView1.Columns["TMH_GENBA_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["TMH_GENBA_NAME"].Visible = blValue;
                     this.form.customDataGridView1.Columns["TMH_GENBA_POST"].Visible = blValue;
                     this.form.customDataGridView1.Columns["TMH_GENBA_TEL"].Visible = blValue;
                     this.form.customDataGridView1.Columns["TMH_GENBA_ADDRESS"].Visible = blValue;
                     this.form.customDataGridView1.Columns["YUUKA_KBN"].Visible = blValue;
                     this.form.customDataGridView1.Columns["YUUKA_SUU"].Visible = blValue;
                     this.form.customDataGridView1.Columns["YUUKA_UNIT_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["SBN_JYURYOUSHA_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["SBN_JYURYOUSHA_NAME"].Visible = blValue;
                     this.form.customDataGridView1.Columns["SBN_JYURYOU_TANTOU_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["SBN_JYURYOU_TANTOU_NAME"].Visible = blValue;
                     this.form.customDataGridView1.Columns["SBN_JYURYOU_DATE"].Visible = blValue;
                     this.form.customDataGridView1.Columns["SBN_JYUTAKUSHA_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["SBN_JYUTAKUSHA_NAME"].Visible = blValue;
                     this.form.customDataGridView1.Columns["SBN_TANTOU_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["SBN_TANTOU_NAME"].Visible = blValue;
                     this.form.customDataGridView1.Columns["LAST_SBN_GYOUSHA_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["LAST_SBN_GENBA_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["LAST_SBN_GENBA_NAME"].Visible = blValue;
                     this.form.customDataGridView1.Columns["LAST_SBN_GENBA_POST"].Visible = blValue;
                     this.form.customDataGridView1.Columns["LAST_SBN_GENBA_TEL"].Visible = blValue;
                     this.form.customDataGridView1.Columns["LAST_SBN_GENBA_ADDRESS"].Visible = blValue;
                     this.form.customDataGridView1.Columns["LAST_SBN_GENBA_NUMBER"].Visible = blValue;
                     this.form.customDataGridView1.Columns["LAST_SBN_CHECK_NAME"].Visible = blValue;
                     this.form.customDataGridView1.Columns["CHECK_B1"].Visible = blValue;
                     this.form.customDataGridView1.Columns["CHECK_B2"].Visible = blValue;
                     this.form.customDataGridView1.Columns["CHECK_B4"].Visible = blValue;
                     this.form.customDataGridView1.Columns["CHECK_B6"].Visible = blValue;
                     this.form.customDataGridView1.Columns["CHECK_D"].Visible = blValue;
                     this.form.customDataGridView1.Columns["CHECK_E"].Visible = blValue;
                     this.form.customDataGridView1.Columns["RENKEI_DENSHU_KBN_CD"].Visible = blValue;
                     this.form.customDataGridView1.Columns["RENKEI_SYSTEM_ID"].Visible = blValue;
                     this.form.customDataGridView1.Columns["RENKEI_MEISAI_SYSTEM_ID"].Visible = blValue;
                     this.form.customDataGridView1.Columns["DELETE_FLG"].Visible = blValue;

                    break;
            }
        }

        /// <summary>
        /// マニフェストテーブル（T_MANIFEST_PT_ENTRY）規定値を取得する
        /// </summary>
        private DataTable GetUseDefaultKbn(int iKYOTEN_CD, int iHAIKI_KBN_CD, int iFIRST_MANIFEST_KBN)
        {
            LogUtility.DebugMethodStart(iKYOTEN_CD,iHAIKI_KBN_CD,iFIRST_MANIFEST_KBN);

            DataTable iUseDefaultKbn = new DataTable();

            string tempSql="";
           
            //SQL文格納StringBuilder
            var sql = new StringBuilder();

            sql.Append(" SELECT T_MANIFEST_PT_ENTRY.PATTERN_NAME ");

            sql.Append("      , T_MANIFEST_PT_ENTRY.SYSTEM_ID ");

            sql.Append("      , T_MANIFEST_PT_ENTRY.SEQ  ");

            sql.Append("      , CAST(T_MANIFEST_PT_ENTRY.TIME_STAMP AS int ) AS TIME_STAMP ");   

            sql.Append(" FROM  T_MANIFEST_PT_ENTRY ");

            sql.Append(" WHERE T_MANIFEST_PT_ENTRY.DELETE_FLG = 0");
            sql.Append(" AND T_MANIFEST_PT_ENTRY.KYOTEN_CD = " + iKYOTEN_CD + "");
            sql.Append(" AND T_MANIFEST_PT_ENTRY.HAIKI_KBN_CD = " + iHAIKI_KBN_CD + "");  
            sql.Append(" AND T_MANIFEST_PT_ENTRY.FIRST_MANIFEST_KBN = " + iFIRST_MANIFEST_KBN + "");
            sql.Append(" AND T_MANIFEST_PT_ENTRY.USE_DEFAULT_KBN = 1");
  
            tempSql = this.createSql;
            this.createSql = sql.ToString();
            sql.Append(string.Empty);          

            this.SearchResult = dao_GetResult.getdateforstringsql(this.createSql);
            int count = this.SearchResult.Rows.Count;
            if (count > 0)
            {
                iUseDefaultKbn = this.SearchResult;
            }
            this.createSql = tempSql;
            LogUtility.DebugMethodEnd(iKYOTEN_CD, iHAIKI_KBN_CD, iFIRST_MANIFEST_KBN);
            return iUseDefaultKbn;
        }
        /// <summary>
        /// 挿入
        /// </summary>
        private void Insert(int systemId, int seq,bool iValue)
        {
            //マニフェスト
            this.InsertT_MANIFEST_PT_ENTRY_Data(systemId, seq, iValue);
            
            //マニ明細
            this.InsertT_MANIFEST_PT_DETAIL_Data(systemId, seq);


            //マニ収集運搬
            this.InsertT_MANIFEST_PT_UPN_Data(systemId, seq);  

            //マニ印字
            this.InsertT_MANIFEST_PT_PRT_Data(systemId, seq);

            //マニ印字明細
            this.InsertT_MANIFEST_PT_DETAIL_PRT_Data(systemId, seq);

            //マニ印字_建廃_形状
            this.InsertT_MANIFEST_PT_KP_KEIJYOU_Data(systemId, seq);

            //マニ印字_建廃_荷姿
            this.InsertT_MANIFEST_PT_KP_NISUGATA_Data(systemId, seq);       

            //マニ印字_建廃_処分方法
            this.InsertT_MANIFEST_PT_KP_SBN_HOUHOU_Data(systemId, seq);
          
        }
        /// <summary>
        /// ロジック削除
        /// </summary>
        private void Delete(int systemId, int sqe,string TimeStamp)
        {
            try
            {
                int count = 0;

                //紙マニフェスト
                List<T_MANIFEST_PT_ENTRY> TmpList = new List<T_MANIFEST_PT_ENTRY>();
                T_MANIFEST_PT_ENTRY tmpe;

                tmpe = new T_MANIFEST_PT_ENTRY();
                tmpe.SYSTEM_ID = systemId;
                tmpe.SEQ = sqe;

                DataBinderLogic<T_MANIFEST_PT_ENTRY> WHO = new DataBinderLogic<T_MANIFEST_PT_ENTRY>(tmpe);
                WHO.SetSystemProperty(tmpe, true);

                tmpe.DELETE_FLG = true;
                //TODO:排他制御の修正
                Int32 data2 = Int32.Parse(TimeStamp);
                tmpe.TIME_STAMP = ConvertStrByte.In32ToByteArray(data2);

                TmpList.Add(tmpe);

                this.TmpeList = TmpList;

                if (TmpeList != null && TmpeList.Count() > 0)
                {
                    foreach (T_MANIFEST_PT_ENTRY tp in TmpeList)
                    {
                        count = dao_SetTMPE.Update(tp);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
            }

        }
        /// <summary>
        /// T_MANIFEST_PT_ENTRYデータを作る
        /// </summary>
        private void InsertT_MANIFEST_PT_ENTRY_Data(int systemId, int seq, bool iValue)
        {
            T_MANIFEST_PT_ENTRY tmpe_PT_ENTRY = dao_SetTMPE.GetDataForEntity(systemId, seq);
            if (tmpe_PT_ENTRY != null)
            {
                tmpe_PT_ENTRY.SEQ = seq + 1;
                tmpe_PT_ENTRY.USE_DEFAULT_KBN = iValue;

                tmpe_PT_ENTRY.DELETE_FLG = false;

                var WHO = new DataBinderLogic<T_MANIFEST_PT_ENTRY>(tmpe_PT_ENTRY);
                WHO.SetSystemProperty(tmpe_PT_ENTRY, false);

                dao_SetTMPE.Insert(tmpe_PT_ENTRY);
            }

        }
        /// <summary>
        /// T_MANIFEST_PT_DETAILデータを作る
        /// </summary>
        private void InsertT_MANIFEST_PT_DETAIL_Data(int systemId, int seq)
        {
            T_MANIFEST_PT_DETAIL[] SearchResult = PtDetailDao.GetDataForEntity(systemId, seq);

            //マニ明細
            if (SearchResult != null && SearchResult.Count() > 0)
            {
                foreach (T_MANIFEST_PT_DETAIL detail in SearchResult)
                {
                    detail.SEQ = seq + 1;
                    //TODO:排他制御の修正
                    var WHO = new DataBinderLogic<T_MANIFEST_PT_DETAIL>(detail);
                    WHO.SetSystemProperty(detail, false);
                    PtDetailDao.Insert(detail);
                }
            }
        }
        /// <summary>
        /// T_MANIFEST_PT_UPNデータを作る
        /// </summary>
        private void InsertT_MANIFEST_PT_UPN_Data(int systemId, int seq)
        {
            T_MANIFEST_PT_UPN[] SearchResult = PtUpnDao.GetDataForEntity(systemId, seq);

            if (SearchResult != null && SearchResult.Count() > 0)
            {
                foreach (T_MANIFEST_PT_UPN upn in SearchResult)
                {
                    upn.SEQ = seq + 1;
                    //TODO:排他制御の修正
                    var WHO = new DataBinderLogic<T_MANIFEST_PT_UPN>(upn);
                    WHO.SetSystemProperty(upn, false);
                    PtUpnDao.Insert(upn);
                }
            }
        }
        /// <summary>
        /// T_MANIFEST_PT_PRTデータを作る
        /// </summary>
        private void InsertT_MANIFEST_PT_PRT_Data(int systemId, int seq)
        {

            T_MANIFEST_PT_PRT[] SearchResult = this.PtPrtDao.GetDataForEntity(systemId, seq);

            if (SearchResult != null && SearchResult.Count() > 0)
            {
                foreach (T_MANIFEST_PT_PRT prt in SearchResult)
                {
                    prt.SEQ = seq + 1;
                    //TODO:排他制御の修正
                    var WHO = new DataBinderLogic<T_MANIFEST_PT_PRT>(prt);
                    WHO.SetSystemProperty(prt, false);
                    this.PtPrtDao.Insert(prt);
                }
            }
        }
        /// <summary>
        /// T_MANIFEST_PT_DETAIL_PRTデータを作る
        /// </summary>
        private void InsertT_MANIFEST_PT_DETAIL_PRT_Data(int systemId, int seq)
        {
            T_MANIFEST_PT_DETAIL_PRT[] SearchResult = PtDetailPrtDao.GetDataForEntity(systemId, seq);

            if (SearchResult != null && SearchResult.Count() > 0)
            {
                foreach (T_MANIFEST_PT_DETAIL_PRT detailprt in SearchResult)
                {
                    detailprt.SEQ = seq + 1;
                    //TODO:排他制御の修正
                    var WHO = new DataBinderLogic<T_MANIFEST_PT_DETAIL_PRT>(detailprt);
                    WHO.SetSystemProperty(detailprt, false);
                    PtDetailPrtDao.Insert(detailprt);
                }
            }
        }
        /// <summary>
        /// T_MANIFEST_PT_KP_KEIJYOUデータを作る
        /// </summary>
        private void InsertT_MANIFEST_PT_KP_KEIJYOU_Data(int systemId, int seq)
        {
            T_MANIFEST_PT_KP_KEIJYOU[] SearchResult = PtKeijyouDao.GetDataForEntity(systemId, seq);

            if (SearchResult != null && SearchResult.Count() > 0)
            {
                foreach (T_MANIFEST_PT_KP_KEIJYOU keijyou in SearchResult)
                {
                    keijyou.SEQ = seq + 1;
                    //TODO:排他制御の修正
                    var WHO = new DataBinderLogic<T_MANIFEST_PT_KP_KEIJYOU>(keijyou);
                    WHO.SetSystemProperty(keijyou, false);
                    PtKeijyouDao.Insert(keijyou);
                }
            }
        }
        /// <summary>
        /// T_MANIFEST_PT_KP_NISUGATAデータを作る
        /// </summary>
        private void InsertT_MANIFEST_PT_KP_NISUGATA_Data(int systemId, int seq)
        {
            T_MANIFEST_PT_KP_NISUGATA[] SearchResult = PtNisugataDao.GetDataForEntity(systemId, seq);

            if (SearchResult != null && SearchResult.Count() > 0)
            {
                foreach (T_MANIFEST_PT_KP_NISUGATA niugata in SearchResult)
                {
                    niugata.SEQ = seq + 1;
                    //TODO:排他制御の修正
                    var WHO = new DataBinderLogic<T_MANIFEST_PT_KP_NISUGATA>(niugata);
                    WHO.SetSystemProperty(niugata, false);
                    PtNisugataDao.Insert(niugata);
                }
            }
        }
        /// <summary>
        /// T_MANIFEST_PT_KP_SBN_HOUHOUデータを作る
        /// </summary>
        private void InsertT_MANIFEST_PT_KP_SBN_HOUHOU_Data(int systemId, int seq)
        {

            T_MANIFEST_PT_KP_SBN_HOUHOU[] SearchResult = PtHouhouDao.GetDataForEntity(systemId, seq);

            if (SearchResult != null && SearchResult.Count() > 0)
            {
                foreach (T_MANIFEST_PT_KP_SBN_HOUHOU houhou in SearchResult)
                {
                    houhou.SEQ = seq + 1;
                    //TODO:排他制御の修正
                    var WHO = new DataBinderLogic<T_MANIFEST_PT_KP_SBN_HOUHOU>(houhou);
                    WHO.SetSystemProperty(houhou, false);
                    PtHouhouDao.Insert(houhou);
                }
            }
        }
        /// <summary>
        /// ディフォルト値登録
        /// </summary>
        public void RegistUseDefaultKbn()
        {
            LogUtility.DebugMethodStart();
            try
            {
                using (Transaction tran = new Transaction())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                    // 20140604 syunrei No.730 マニフェストパターン一覧 start
                    //選択されている明細行号
                    int iRowIndex = this.form.customDataGridView1.SelectedCells[0].RowIndex;
                    //拠点
                    int iKyoten = 0;
                    //廃棄区分
                    int iHaikiKbn = 0;
                    //マニフェスト区分
                    int iManiKbn = 0;
                    //明細に選択された行のsystemId
                    int iSystemId = 0;
                    //明細に選択された行のsqe
                    int iSqe = 0;
                    //メッセージ表示用のパターン名
                    string iPatternName = string.Empty;
                    //格納用のテーブルに存在しているデータ
                    DataTable dt = new DataTable();
                    //明細に選択された行のTimeStamp 排他用
                    string iTimeStamp = string.Empty;
                    //拠点                
                    if (this.form.customDataGridView1.Rows[iRowIndex].Cells["KYOTEN_CD"].Value != null)
                    {
                        iKyoten = Convert.ToInt32(this.form.customDataGridView1.Rows[iRowIndex].Cells["KYOTEN_CD"].Value);
                    }

                    //廃棄区分
                    if (this.form.customDataGridView1.Rows[iRowIndex].Cells["HAIKI_KBN_CD"].Value != null)
                    {

                        iHaikiKbn = Convert.ToInt32(this.form.customDataGridView1.Rows[iRowIndex].Cells["HAIKI_KBN_CD"].Value);
                    }

                    //マニフェスト区分
                    if (this.form.customDataGridView1.Rows[iRowIndex].Cells["FIRST_MANIFEST_KBN"].Value != null)
                    {
                        iManiKbn = Convert.ToInt32(this.form.customDataGridView1.Rows[iRowIndex].Cells["FIRST_MANIFEST_KBN"].Value);
                    }

                    //マニフェストテーブルのSYSTEM_ID
                    if (this.form.customDataGridView1.Rows[iRowIndex].Cells["SYSTEM_ID"].Value != null)
                    {
                        iSystemId = Convert.ToInt32(this.form.customDataGridView1.Rows[iRowIndex].Cells["SYSTEM_ID"].Value);
                    }
                    //マニフェストテーブルのSEQ
                    if (this.form.customDataGridView1.Rows[iRowIndex].Cells["SEQ"].Value != null)
                    {
                        iSqe = Convert.ToInt32(this.form.customDataGridView1.Rows[iRowIndex].Cells["SEQ"].Value);
                    }
                    //マニフェストテーブルのTIME_STAMP
                    iTimeStamp = this.form.customDataGridView1.Rows[iRowIndex].Cells["TIME_STAMP"].Value.ToString();

                    SqlBoolean use_game = false;
                    use_game = String.IsNullOrEmpty(this.form.customDataGridView1.Rows[iRowIndex].Cells["USE_DEFAULT_KBN"].Value.ToString()) ? SqlBoolean.Null : SqlBoolean.Parse(this.form.customDataGridView1.Rows[iRowIndex].Cells["USE_DEFAULT_KBN"].Value.ToString());
                    //・選択されている明細に対応する｢マニフェストパターン」TableのUSE_DEFAULT_KBN＝ 1の場合
                    if (use_game == true)
                    {
                        //「登録が完了しました。」Messageを表示する。
                        msgLogic.MessageBoxShow("I001", "登録");
                        if (this.Search() == -1) { return; }
                       
                    }
                    //・選択されている明細に対応する｢マニフェストパターン」TableのUSE_DEFAULT_KBN≠ 1の場合
                    else 
                    {
                        //｢マニフェストパターン」TableのKYOTEN_CD、HAIKI_KBN_CD、FIRST_MANIFEST_KBNの値と等しい、
                        dt = this.GetUseDefaultKbn(iKyoten, iHaikiKbn, iManiKbn);
                        if (dt.Rows.Count > 0)
                        {
                            //パターン名称
                            iPatternName = "「"+dt.Rows[0]["PATTERN_NAME"].ToString()+"」";
                            //「(そのRecordのPATTERN_NAME）が規定値として登録されています。規定値を更新しますか？」確認Message表示								
                            if (msgLogic.MessageBoxShow("C067", iPatternName) == DialogResult.Yes)
                            {
                                //「はい」を選択した場合　→　そのRecordのUSE_DEFAULT_KBNを0にし、      
                                for (int i = 0; i < dt.Rows.Count; i++ )
                                {
                                    //マニフェストテーブルのSYSTEM_ID
                                    int iSystemIdDt = Convert.ToInt32(dt.Rows[i]["SYSTEM_ID"]);

                                    //マニフェストテーブルのSEQ
                                    int iSqeDt = Convert.ToInt32(dt.Rows[i]["SEQ"]);

                                    //マニフェストテーブルのTIME_STAMP
                                    string iTimeStampDt = dt.Rows[i]["TIME_STAMP"].ToString();

                                    //選択されている明細に対応する｢マニフェストパターン」TableのUSE_DEFAULT_KBNを1にする。
                                    //挿入
                                    this.Insert(iSystemIdDt, iSqeDt, false);

                                    //⇒ロジック削除
                                    this.Delete(iSystemIdDt, iSqeDt, iTimeStampDt);
                                }

                                //更新 SQE   
                                //挿入
                                this.Insert(iSystemId, iSqe, true);
                                //⇒ロジック削除
                                this.Delete(iSystemId, iSqe, iTimeStamp);

                                //その後、「登録が完了しました」Messageを表示する。　
                                msgLogic.MessageBoxShow("I001", "登録");
                                if (this.Search() == -1) { return; }
                            }
                            else
                            {
                                return;
                            }
                        }
                        //b. 上記a.以外の場合
                        else
                        {
                            if (msgLogic.MessageBoxShow("C036") == DialogResult.Yes)
                            {
                                //挿入
                                this.Insert(iSystemId, iSqe, true);
                                //⇒ロジック削除
                                this.Delete(iSystemId, iSqe, iTimeStamp);
                                //その後、「登録が完了しました」Messageを表示する。　
                                msgLogic.MessageBoxShow("I001", "登録");
                                if (this.Search() == -1) { return; }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }

                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistUseDefaultKbn", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }              
            }
            LogUtility.DebugMethodEnd();
        }

        // 20140529 syunrei No.730 マニフェストパターン一覧 end

        #region MOD NHU 20211005 #155786
        internal void DeleteUseDefaultKbn()
        {
            try
            {
               
                //明細に選択された行のsystemId
                int iSystemId = 0;
                //明細に選択された行のsqe
                int iSqe = 0;
                string iTimeStamp = string.Empty;
                DataGridViewRow iRow = null;
                SqlBoolean use_game = false;
                foreach(DataGridViewRow row in this.form.customDataGridView1.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }
                    use_game = String.IsNullOrEmpty(row.Cells["USE_DEFAULT_KBN"].Value.ToString()) ? SqlBoolean.Null : SqlBoolean.Parse(row.Cells["USE_DEFAULT_KBN"].Value.ToString());
                    if (use_game)
                    {
                        iRow = row;
                    }
                }
                if (iRow == null)
                {
                    return;
                }
                //マニフェストテーブルのSYSTEM_ID
                if (iRow.Cells["SYSTEM_ID"].Value != null)
                {
                    iSystemId = Convert.ToInt32(iRow.Cells["SYSTEM_ID"].Value);
                }
                //マニフェストテーブルのSEQ
                if (iRow.Cells["SEQ"].Value != null)
                {
                    iSqe = Convert.ToInt32(iRow.Cells["SEQ"].Value);
                }
                //マニフェストテーブルのTIME_STAMP
                iTimeStamp = iRow.Cells["TIME_STAMP"].Value.ToString();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                using (Transaction tran = new Transaction())
                {          
                    switch (this.form.HaikiKbnCD)
                    {
                        case "1"://産廃（直行）
                        case "2"://建廃
                        case "3"://産廃（積替）
                        default:
                            //挿入
                            this.Insert(iSystemId, iSqe, false);

                            //⇒ロジック削除
                            this.Delete(iSystemId, iSqe, iTimeStamp);
                            break;
                        case "4"://電子
                            DT_PT_R18 ptR18 = new DT_PT_R18();
                            ptR18.SYSTEM_ID = iSystemId;
                            ptR18.SEQ = iSqe;
                            ptR18.USE_DEFAULT_KBN = false;

                            var WHO2 = new DataBinderLogic<DT_PT_R18>(ptR18);
                            WHO2.SetSystemProperty(ptR18, false);
                            //挿入
                            this.dao_SetDPR18.UpdateMod(ptR18);
                            break;
                   
                    }
                    tran.Commit();
                }
                //「登録が完了しました。」Messageを表示する。
                msgLogic.MessageBoxShow("I001", "解除");
                if (this.Search() == -1) { return; }
            }
            catch (Exception ee)
            {
                LogUtility.Error("DeleteUseDefaultKbn", ee);
                throw ee;
            }
        }
        #endregion

        #region MOD NHU 20211005 #155789
        public void RegistDenshiUseDefaultKbn()
        {
            LogUtility.DebugMethodStart();
            try
            {
                using (Transaction tran = new Transaction())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                    // 20140604 syunrei No.730 マニフェストパターン一覧 start
                    //選択されている明細行号
                    int iRowIndex = this.form.customDataGridView1.SelectedCells[0].RowIndex;
                    //マニフェスト区分
                    string iManiKbn = string.Empty;
                    //明細に選択された行のsystemId
                    int iSystemId = 0;
                    //明細に選択された行のsqe
                    int iSqe = 0;
                    //メッセージ表示用のパターン名
                    string iPatternName = string.Empty;
                    //格納用のテーブルに存在しているデータ
                    DataTable dt = new DataTable();                  

                    //マニフェスト区分
                    if (this.form.customDataGridView1.Rows[iRowIndex].Cells["FIRST_MANIFEST_FLAG"].Value != null)
                    {
                        iManiKbn = this.form.customDataGridView1.Rows[iRowIndex].Cells["FIRST_MANIFEST_FLAG"].Value.ToString();
                    }
                    //マニフェストテーブルのSYSTEM_ID
                    if (this.form.customDataGridView1.Rows[iRowIndex].Cells["SYSTEM_ID"].Value != null)
                    {
                        iSystemId = Convert.ToInt32(this.form.customDataGridView1.Rows[iRowIndex].Cells["SYSTEM_ID"].Value);
                    }
                    //マニフェストテーブルのSEQ
                    if (this.form.customDataGridView1.Rows[iRowIndex].Cells["SEQ"].Value != null)
                    {
                        iSqe = Convert.ToInt32(this.form.customDataGridView1.Rows[iRowIndex].Cells["SEQ"].Value);
                    }

                    SqlBoolean use_game = false;
                    use_game = String.IsNullOrEmpty(this.form.customDataGridView1.Rows[iRowIndex].Cells["USE_DEFAULT_KBN"].Value.ToString()) ? SqlBoolean.Null : SqlBoolean.Parse(this.form.customDataGridView1.Rows[iRowIndex].Cells["USE_DEFAULT_KBN"].Value.ToString());
                    //・選択されている明細に対応する｢マニフェストパターン」TableのUSE_DEFAULT_KBN＝ 1の場合
                    if (use_game == true)
                    {
                        //「登録が完了しました。」Messageを表示する。
                        msgLogic.MessageBoxShow("I001", "登録");
                        if (this.Search() == -1) { return; }

                    }
                    //・選択されている明細に対応する｢マニフェストパターン」TableのUSE_DEFAULT_KBN≠ 1の場合
                    else
                    {
                        //｢マニフェストパターン」TableのKYOTEN_CD、HAIKI_KBN_CD、FIRST_MANIFEST_KBNの値と等しい、
                        dt = this.GetDenshiUseDefaultKbn(iManiKbn);
                        if (dt.Rows.Count > 0)
                        {
                            DT_PT_R18 ptR18 = new DT_PT_R18();
                            //パターン名称
                            iPatternName = "「" + dt.Rows[0]["PATTERN_NAME"].ToString() + "」";
                            //「(そのRecordのPATTERN_NAME）が規定値として登録されています。規定値を更新しますか？」確認Message表示								
                            if (msgLogic.MessageBoxShow("C067", iPatternName) == DialogResult.Yes)
                            {
                                //「はい」を選択した場合　→　そのRecordのUSE_DEFAULT_KBNを0にし、      
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    ptR18 = new DT_PT_R18();
                                    //マニフェストテーブルのSYSTEM_ID
                                    ptR18 .SYSTEM_ID = Convert.ToInt64(dt.Rows[i]["SYSTEM_ID"]);

                                    //マニフェストテーブルのSEQ
                                    ptR18.SEQ = Convert.ToInt32(dt.Rows[i]["SEQ"]);

                                    ptR18.USE_DEFAULT_KBN = false;

                                    var WHO = new DataBinderLogic<DT_PT_R18>(ptR18);
                                    WHO.SetSystemProperty(ptR18, false);
                                    //選択されている明細に対応する｢マニフェストパターン」TableのUSE_DEFAULT_KBNを1にする。
                                    //挿入
                                    this.dao_SetDPR18.UpdateMod(ptR18);
                                }

                                //更新 SQE   
                                ptR18 = new DT_PT_R18();
                                ptR18.SYSTEM_ID = iSystemId;
                                ptR18.SEQ = iSqe;
                                ptR18.USE_DEFAULT_KBN = true;

                                var WHO2 = new DataBinderLogic<DT_PT_R18>(ptR18);
                                WHO2.SetSystemProperty(ptR18, false);
                                //挿入
                                this.dao_SetDPR18.UpdateMod(ptR18);

                                //その後、「登録が完了しました」Messageを表示する。　
                                msgLogic.MessageBoxShow("I001", "登録");
                                if (this.Search() == -1) { return; }
                            }
                            else
                            {
                                return;
                            }
                        }
                        //b. 上記a.以外の場合
                        else
                        {
                            if (msgLogic.MessageBoxShow("C036") == DialogResult.Yes)
                            {
                                //挿入
                                DT_PT_R18 ptR18 = new DT_PT_R18();
                                ptR18.SYSTEM_ID = iSystemId;
                                ptR18.SEQ = iSqe;
                                ptR18.USE_DEFAULT_KBN = true;

                                var WHO2 = new DataBinderLogic<DT_PT_R18>(ptR18);
                                WHO2.SetSystemProperty(ptR18, false);
                                //挿入
                                this.dao_SetDPR18.UpdateMod(ptR18);
                                //その後、「登録が完了しました」Messageを表示する。　
                                msgLogic.MessageBoxShow("I001", "登録");
                                if (this.Search() == -1) { return; }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }

                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistUseDefaultKbn", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// マニフェストテーブル（T_MANIFEST_PT_ENTRY）規定値を取得する
        /// </summary>
        private DataTable GetDenshiUseDefaultKbn(string iFIRST_MANIFEST_KBN)
        {
            LogUtility.DebugMethodStart(iFIRST_MANIFEST_KBN);

            DataTable iUseDefaultKbn = new DataTable();

            string tempSql = "";

            //SQL文格納StringBuilder
            var sql = new StringBuilder();

            sql.Append(" SELECT DT_PT_R18.PATTERN_NAME ");

            sql.Append("      , DT_PT_R18.SYSTEM_ID ");

            sql.Append("      , DT_PT_R18.SEQ  ");

            sql.Append("      , CAST(DT_PT_R18.TIME_STAMP AS int ) AS TIME_STAMP ");

            sql.Append(" FROM  DT_PT_R18 ");

            sql.Append(" WHERE DT_PT_R18.DELETE_FLG = 0");
            if (string.IsNullOrEmpty(iFIRST_MANIFEST_KBN))
            {
                sql.Append(" AND DT_PT_R18.FIRST_MANIFEST_FLAG IS NULL ");
            }
            else
            {
                sql.Append(" AND DT_PT_R18.FIRST_MANIFEST_FLAG IS NOT NULL ");
            }
            sql.Append(" AND DT_PT_R18.USE_DEFAULT_KBN = 1");

            tempSql = this.createSql;
            this.createSql = sql.ToString();
            sql.Append(string.Empty);

            this.SearchResult = dao_GetResult.getdateforstringsql(this.createSql);
            int count = this.SearchResult.Rows.Count;
            if (count > 0)
            {
                iUseDefaultKbn = this.SearchResult;
            }
            this.createSql = tempSql;
            LogUtility.DebugMethodEnd(iFIRST_MANIFEST_KBN);
            return iUseDefaultKbn;
        }
       
        #endregion
    }
}
