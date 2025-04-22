using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.SalesPayment.DenpyouHakou;
using Shougun.Core.SalesPayment.DenpyouHakou.Const;
using Shougun.Core.Scale.Keiryou;
using Shougun.Function.ShougunCSCommon.Const;
using Shougun.Function.ShougunCSCommon.Dto;
using Shougun.Function.ShougunCSCommon.Utility;
using Shougun.Core.FileUpload.FileUploadCommon;
using Shougun.Core.FileUpload.FileUploadCommon.Logic;
using Shougun.Core.FileUpload.FileUploadCommon.Utility;
using Shougun.Core.SalesPayment.DenpyouHakou.Report;

namespace Shougun.Core.Scale.KeiryouNyuuryoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicClass : IBuisinessLogic
    {
        #region フィールド

        #region 定数
        /// <summary>
        /// ボタン設定ファイルのパス
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Scale.KeiryouNyuuryoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// 日連番更新区分
        /// </summary>
        private Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN hiRenbanRegistKbn { get; set; }

        /// <summary>
        /// 年連番更新区分
        /// </summary>
        private Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN nenRenbanRegistKbn { get; set; }

        /// <summary>
        /// ROW_NO
        /// </summary>
        internal const string CELL_NAME_ROW_NO = "ROW_NO";

        /// <summary>品名CD</summary>
        internal const string CELL_NAME_HINMEI_CD = "HINMEI_CD";

        /// <summary>品名</summary>
        internal const string CELL_NAME_HINMEI_NAME = "HINMEI_NAME";

        /// <summary>正味重量</summary>
        internal const string CELL_NAME_NET_JYUURYOU = "NET_JYUURYOU";

        /// <summary>伝票区分CD</summary>
        internal const string CELL_NAME_DENPYOU_KBN_CD = "DENPYOU_KBN_CD";

        /// <summary>伝票区分名</summary>
        internal const string CELL_NAME_DENPYOU_KBN_NAME = "DENPYOU_KBN_NAME";

        /// <summary>金額</summary>
        internal const string CELL_NAME_KINGAKU = "KINGAKU";

        /// <summary>調整重量</summary>
        internal const string CELL_NAME_CHOUSEI_JYUURYOU = "CHOUSEI_JYUURYOU";

        /// <summary>調整(%)</summary>
        internal const string CELL_NAME_CHOUSEI_PERCENT = "CHOUSEI_PERCENT";

        /// <summary>容器kg</summary>
        internal const string CELL_NAME_YOUKI_JYUURYOU = "YOUKI_JYUURYOU";

        /// <summary>総重量</summary>
        internal const string CELL_NAME_STAK_JYUURYOU = "STACK_JYUURYOU";

        /// <summary>空車重量</summary>
        internal const string CELL_NAME_EMPTY_JYUURYOU = "EMPTY_JYUURYOU";

        /// <summary>容器数量</summary>
        internal const string CELL_NAME_YOUKI_SUURYOU = "YOUKI_SUURYOU";

        /// <summary>容器CD</summary>
        internal const string CELL_NAME_YOUKI_CD = "YOUKI_CD";

        /// <summary>容器名</summary>
        internal const string CELL_NAME_YOUKI_NAME_RYAKU = "YOUKI_NAME_RYAKU";

        /// <summary>システムID</summary>
        internal const string CELL_NAME_SYSTEM_ID = "SYSTEM_ID";

        /// <summary>明細システムID</summary>
        internal const string CELL_NAME_DETAIL_SYSTEM_ID = "DETAIL_SYSTEM_ID";

        /// <summary>単位CD</summary>
        internal const string CELL_NAME_UNIT_CD = "UNIT_CD";

        /// <summary>単位名</summary>
        internal const string CELL_NAME_UNIT_NAME_RYAKU = "UNIT_NAME_RYAKU";

        /// <summary>数量</summary>
        internal const string CELL_NAME_SUURYOU = "SUURYOU";

        /// <summary>単価</summary>
        internal const string CELL_NAME_TANKA = "TANKA";

        /// <summary>計量時間</summary>
        internal const string CELL_NAME_KEIRYOU_TIME = "KEIRYOU_TIME";

        /// <summary>明細備考</summary>
        internal const string CELL_NAME_MEISAI_BIKOU = "MEISAI_BIKOU";

        /// <summary>消費税率</summary>
        internal const string CELL_NAME_SHOUHIZEI_RATE = "CELL_SHOUHIZEI_RATE";

        /// <summary>税区分CD</summary>
        internal const string CELL_NAME_HINMEI_ZEI_KBN_CD = "HINMEI_ZEI_KBN_CD";

        /// <summary>システムID</summary>
        internal const string CELL_NAME_DENPYOU_SYSTEM_ID = "DENPYOU_SYSTEM_ID";

        /// <summary>明細システムID</summary>
        internal const string CELL_NAME_JISSEKI_SEQ = "SEQ";

        /// <summary>数量割合</summary>
        internal const string CELL_NAME_SUURYOU_WARIAI = "SUURYOU_WARIAI";

        /// <summary>
        /// 明細行に売上と支払が混在している場合
        /// </summary>
        internal const int URIAGE_SHIHARAI_MIXED = 0;

        /// <summary>
        /// 明細行に売上のみある場合
        /// </summary>
        internal const int URIAGE_ONLY = 1;

        /// <summary>
        /// 明細行に支払のみある場合
        /// </summary>
        internal const int SHIHARAI_ONLY = 2;

        //仕切書種類
        enum DENPYO_SHIKIRISHO_KIND
        {
            SEIKYUU = 1,
            SHIHARAI,
            SOUSAI
        }

        //伝票発行区分
        const string DEF_HAKKOU_KBN_KOBETSU = "1";
        const string DEF_HAKKOU_KBN_SOUSAI = "2";
        const string DEF_HAKKOU_KBN_ALL = "3";

        /// <summary>
        /// 端数処理種別
        /// </summary>
        private enum fractionType : int
        {
            CEILING = 1,	// 切り上げ
            FLOOR,		// 切り捨て
            ROUND,		// 四捨五入
        }

        /// <summary>
        /// システム単価書式コード
        /// </summary>
        private enum SysTankaFormatCd : int
        {
            BLANK = 1,      // 1の位がゼロなら空白表示
            NONE,           // 1の位がゼロならゼロ表示
            ONEPOINT,       // 小数点第１位まで表示
            TWOPOINT,       // 小数点第２位まで表示
            THREEPOINT,     // 小数点第３位まで表示
        }

        /// <summary>
        /// システム数量書式コード
        /// </summary>
        private enum SysSuuryouFormatCd : int
        {
            BLANK = 1,      // 1の位がゼロなら空白表示
            NONE,           // 1の位がゼロならゼロ表示
            ONEPOINT,       // 小数点第１位まで表示
            TWOPOINT,       // 小数点第２位まで表示
            THREEPOINT,     // 小数点第３位まで表示
        }

        private System.Collections.Specialized.StringCollection DenpyouCtrl = new System.Collections.Specialized.StringCollection();
        private System.Collections.Specialized.StringCollection DetailCtrl = new System.Collections.Specialized.StringCollection();

        /// <summary>
        /// Detail項目で最初のセルフォーカス位置のセル名
        /// </summary>
        private string firstIndexDetailCellName = "HINMEI_CD";

        /// <summary>
        /// 滞留登録された計量伝票に設定する画面区分(新規)
        /// </summary>
        private static readonly WINDOW_TYPE tairyuuWindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

        #endregion

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// フッター
        /// </summary>
        public BusinessBaseForm footerForm;

        /// <summary>
        /// ヘッダー
        /// </summary>
        public UIHeaderForm headerForm;

        /// <summary>
        /// 計量入力専用DBアクセッサー
        /// </summary>
        internal Shougun.Scale.KeiryouNyuuryoku.Accessor.DBAccessor accessor;

        /// <summary>
        /// BusinessCommonのDBAccesser
        /// </summary>
        private Shougun.Core.Common.BusinessCommon.DBAccessor commonAccesser;

        /// <summary>
        /// DTO
        /// </summary>
        internal DTOClass dto;

        /// <summary>
        /// 画面表示時点DTO
        /// </summary>
        internal DTOClass beforDto;

        /// <summary>
        /// ControlUtility
        /// </summary>
        internal ControlUtility controlUtil;

        /// <summary>
        /// 画面上に表示するメッセージボックスを
        /// メッセージIDから検索し表示する処理
        /// </summary>
        public MessageBoxShowLogic msgLogic;

        /// <summary>
        /// 取得した取引先エンティティを保持する
        /// </summary>
        private List<M_TORIHIKISAKI> torihikisakiList = new List<M_TORIHIKISAKI>();

        /// <summary>
        /// 取得した業者エンティティを保持する
        /// </summary>
        private List<M_GYOUSHA> gyoushaList = new List<M_GYOUSHA>();

        /// <summary>
        /// 取得した現場エンティティを保持する
        /// </summary>
        private List<M_GENBA> genbaList = new List<M_GENBA>();

        /// <summary>
        /// 取得したマニフェスト種類エンティティを保持する
        /// </summary>
        private List<M_MANIFEST_SHURUI> manifestShuruiList = new List<M_MANIFEST_SHURUI>();

        /// <summary>
        /// 取得したマニフェスト手配エンティティを保持する
        /// </summary>
        private List<M_MANIFEST_TEHAI> manifestTehaiList = new List<M_MANIFEST_TEHAI>();

        /// <summary>
        /// 伝票区分全件
        /// </summary>
        Dictionary<short, M_DENPYOU_KBN> denpyouKbnDictionary = new Dictionary<short, M_DENPYOU_KBN>();

        /// <summary>
        /// 容器全件
        /// </summary>
        Dictionary<string, M_YOUKI> youkiDictionary = new Dictionary<string, M_YOUKI>();

        /// <summary>
        /// 単位区分全件
        /// </summary>
        Dictionary<short, M_UNIT> unitDictionary = new Dictionary<short, M_UNIT>();

        /// <summary>
        /// 品名全件
        /// </summary>
        Dictionary<string, M_HINMEI> hinmeiDictionary = new Dictionary<string, M_HINMEI>();

        /// <summary>
        /// タブコントロール制御用
        /// </summary>
        internal TabPageManager _tabPageManager = null;

        /// <summary>
        /// 車輌CDマスタ存在チェック実行フラグ
        /// </summary>
        bool isSharyouMasterCheck = true;

        /// <summary>
        /// サブファンクションから呼ばれたか判断するフラグ
        /// </summary>
        internal bool isSubFunctionCall = true;

        /// <summary>
        /// UIFormの入力コントロール名一覧
        /// </summary>
        private string[] inputUiFormControlNames =
            {   "NYUURYOKU_TANTOUSHA_CD", "NYUURYOKU_TANTOUSHA_SEARCH_BUTTON",
                "ENTRY_NUMBER", "nextButton", "previousButton", "RENBAN", 
                "DENPYOU_DATE", "KEIRYOU_KBN_CD", "GYOUSHA_CD", "GHOUSYA_SEARCH_BUTTON", 
                "GENBA_CD", "GENBA_SEARCH_BUTTON", "UNPAN_GYOUSHA_CD", "UNPAN_GYOUSHA_SEARCH_BUTTON", 
                "SHASHU_CD", "SHASHU_SEARCH_BUTTON", "SHARYOU_CD", "SHARYOU_SEARCH_BUTTON", 
                "UNTENSHA_CD", "UNTENSHA_SEARCH_BUTTON", "EMPTY_JYUURYOU", "DENPYOU_BIKOU", "TAIRYUU_BIKOU",
                "MANIFEST_HAIKI_KBN_CD", "HST_GYOUSHA_CD", "HST_GENBA_CD", "SBN_GYOUSHA_CD",
                "SBN_GENBA_CD", "LAST_SBN_GYOUSHA_CD", "LAST_SBN_GENBA_CD", "EIGYOU_TANTOUSHA_CD",
                "NIOROSHI_GYOUSHA_CD", "NIOROSHI_GENBA_CD", "NIZUMI_GYOUSHA_CD", "NIZUMI_GENBA_CD",
                "DAIKAN_KBN", "KEITAI_KBN_CD", "MANIFEST_SHURUI_CD", "MANIFEST_TEHAI_CD",
                "TORIHIKISAKI_CD", "TORIHIKISAKI_SEARCH_BUTTON",
                "SEIKYUU_TORIHIKI_KBN_CD", "SEIKYUU_ZEI_KEISAN_KBN_CD", "SEIKYUU_ZEI_KBN_CD",
                "SHIHARAI_TORIHIKI_KBN_CD", "SHIHARAI_ZEI_KEISAN_KBN_CD", "SHIHARAI_ZEI_KBN_CD",
                "CASHEIR_RENDOU_KBN_CD", "rb_CASHEIR_RENDOU_KBN_1", "rb_CASHEIR_RENDOU_KBN_2",
                "RECEIPT_KBN_CD", "rb_RECEIPT_KBN_1", "rb_RECEIPT_KBN_2", 
                "RECEIPT_KEISHOU", "RECEIPT_TADASHIGAKI","STACK_JYUURYOU", "STACK_KEIRYOU_TIME",
                "EMPTY_JYUURYOU", "EMPTY_KEIRYOU_TIME",
                "SAGYOU_DATE","SAGYOU_HOUR","SAGYOU_MINUTE","SAGYOUSHA_CD","SAGYOU_BIKOU"
            };

        /// <summary>
        /// UIFormの入力コントロール名一覧（参照用）
        /// </summary>
        private string[] refUiFormControlNames =
            {   "NYUURYOKU_TANTOUSHA_CD", "NYUURYOKU_TANTOUSHA_SEARCH_BUTTON",
                "ENTRY_NUMBER", "nextButton", "previousButton", "RENBAN", 
                "DENPYOU_DATE", "KEIRYOU_KBN_CD", "GYOUSHA_CD", "GHOUSYA_SEARCH_BUTTON", 
                "GENBA_CD", "GENBA_SEARCH_BUTTON", "UNPAN_GYOUSHA_CD", "UNPAN_GYOUSHA_SEARCH_BUTTON", 
                "SHASHU_CD", "SHASHU_SEARCH_BUTTON", "SHARYOU_CD", "SHARYOU_SEARCH_BUTTON", 
                "UNTENSHA_CD", "UNTENSHA_SEARCH_BUTTON",  "EMPTY_JYUURYOU", "DENPYOU_BIKOU", "TAIRYUU_BIKOU",
                "MANIFEST_HAIKI_KBN_CD", "HST_GYOUSHA_CD", "HST_GENBA_CD",
                "SBN_GYOUSHA_CD", "SBN_GENBA_CD", "LAST_SBN_GYOUSHA_CD", "LAST_SBN_GENBA_CD",
                "EIGYOU_TANTOUSHA_CD", "NIOROSHI_GYOUSHA_CD", "NIOROSHI_GENBA_CD",
                "NIZUMI_GYOUSHA_CD", "NIZUMI_GENBA_CD", "DAIKAN_KBN", "KEITAI_KBN_CD",
                "MANIFEST_SHURUI_CD", "MANIFEST_TEHAI_CD", "TORIHIKISAKI_CD", "TORIHIKISAKI_SEARCH_BUTTON",
                "SEIKYUU_TORIHIKI_KBN_CD", "SEIKYUU_ZEI_KEISAN_KBN_CD", "SEIKYUU_ZEI_KBN_CD",
                "SHIHARAI_TORIHIKI_KBN_CD", "SHIHARAI_ZEI_KEISAN_KBN_CD", "SHIHARAI_ZEI_KBN_CD",
                "CASHEIR_RENDOU_KBN_CD", "rb_CASHEIR_RENDOU_KBN_1", "rb_CASHEIR_RENDOU_KBN_2",
                "RECEIPT_KBN_CD", "rb_RECEIPT_KBN_1", "rb_RECEIPT_KBN_2", 
                "RECEIPT_KEISHOU", "RECEIPT_TADASHIGAKI", "STACK_JYUURYOU", "STACK_KEIRYOU_TIME",
                "EMPTY_JYUURYOU", "EMPTY_KEIRYOU_TIME",
                "SAGYOU_DATE","SAGYOU_HOUR","SAGYOU_MINUTE","SAGYOUSHA_CD","SAGYOU_BIKOU"
            };

        /// <summary>
        /// タブストップ用
        /// </summary>
        private string[] tabUiFormControlNames =
            {   "KYOTEN_CD", "NYUURYOKU_TANTOUSHA_CD", "ENTRY_NUMBER",
                "RENBAN", "DENPYOU_DATE", "KEIRYOU_KBN_CD", "UNPAN_GYOUSHA_CD", "UNPAN_GYOUSHA_NAME",
                "SHASHU_CD","SHASHU_NAME", "UNTENSHA_CD", "SHARYOU_CD", "SHARYOU_NAME_RYAKU","GYOUSHA_CD",
                "GYOUSHA_NAME_RYAKU", "GENBA_CD", "GENBA_NAME_RYAKU", "STACK_JYUURYOU", "STACK_KEIRYOU_TIME",
                "EMPTY_JYUURYOU", "EMPTY_KEIRYOU_TIME", "DENPYOU_BIKOU", "TAIRYUU_BIKOU",
                "SAGYOU_DATE","SAGYOU_HOUR","SAGYOU_MINUTE","SAGYOUSHA_CD","SAGYOU_BIKOU",
                "MANIFEST_HAIKI_KBN_CD", "HST_GYOUSHA_CD", "HST_GENBA_CD","SBN_GYOUSHA_CD", "SBN_GENBA_CD",
                "LAST_SBN_GYOUSHA_CD", "LAST_SBN_GENBA_CD", "EIGYOU_TANTOUSHA_CD",
                "NIOROSHI_GYOUSHA_CD", "NIOROSHI_GYOUSHA_NAME", "NIOROSHI_GENBA_CD", "NIOROSHI_GENBA_NAME",
                "NIZUMI_GYOUSHA_CD", "NIZUMI_GYOUSHA_NAME", "NIZUMI_GENBA_CD", "NIZUMI_GENBA_NAME",
                "DAIKAN_KBN", "KEITAI_KBN_CD", "MANIFEST_SHURUI_CD", "MANIFEST_TEHAI_CD",
                "TORIHIKISAKI_CD", "TORIHIKISAKI_NAME_RYAKU",
                "SEIKYUU_TORIHIKI_KBN_CD", "SEIKYUU_ZEI_KEISAN_KBN_CD", "SEIKYUU_ZEI_KBN_CD",
                "SHIHARAI_TORIHIKI_KBN_CD", "SHIHARAI_ZEI_KEISAN_KBN_CD", "SHIHARAI_ZEI_KBN_CD", "CASHEIR_RENDOU_KBN_CD",
                "RECEIPT_KBN_CD", "RECEIPT_KEISHOU_1", "RECEIPT_KEISHOU_2", "RECEIPT_TADASHIGAKI"
            };
        private string[] tabDetailControlNames =
            {   "HINMEI_CD", "HINMEI_NAME", "KEIRYOU_TIME", "STACK_JYUURYOU", "EMPTY_JYUURYOU",
                "CHOUSEI_JYUURYOU", "CHOUSEI_PERCENT", "YOUKI_CD", "YOUKI_NAME_RYAKU", "YOUKI_SUURYOU", "YOUKI_JYUURYOU",
                "SUURYOU", "UNIT_CD", "TANKA", "KINGAKU", "MEISAI_BIKOU",
            };

        /// <summary>
        /// HeaderFormの入力コントロール名一覧
        /// </summary>
        private string[] inputHeaderControlNames =
            {   "KYOTEN_CD", "KYOTEN_NAME_RYAKU", "KEIZOKU_NYUURYOKU_VALUE", "KEIZOKU_NYUURYOKU_ON", "KEIZOKU_NYUURYOKU_OFF",
                "PRINT_VALUE", "PRINT_ON", "PRINT_OFF", "CreateUser", "CreateDate", "LastUpdateUser", "LastUpdateDate" 
            };

        /// <summary>
        /// Detailの入力コントロール名一覧
        /// </summary>
        private string[] inputDetailControlNames = new string[] 
            {   "ROW_NO", "HINMEI_CD", "HINMEI_NAME", "NET_JYUURYOU", "SUURYOU", "KEIRYOU_TIME", "UNIT_CD", 
                "STACK_JYUURYOU", "EMPTY_JYUURYOU", "TANKA", "KINGAKU", 
                "CHOUSEI_JYUURYOU", "CHOUSEI_PERCENT", "YOUKI_CD", "YOUKI_NAME_RYAKU", "YOUKI_SUURYOU", "YOUKI_JYUURYOU",
                "MEISAI_BIKOU", "DETAIL_SYSTEM_ID"
            };

        /// <summary>
        /// 受入実績DETAILの入力コントロール名一覧
        /// </summary>
        private string[] inputJissekiControlNames = new string[] 
            {   "ROW_NO", "HINMEI_CD", "HINMEI_NAME", "SUURYOU_WARIAI", "DENPYOU_SYSTEM_ID", "SEQ"
            };

        /// <summary>
        /// 登録処理中フラグを取得・設定します
        /// </summary>
        internal bool IsRegist { get; set; }

        /// <summary>
        /// 数量処理FLG(伝票上部の項目で、値を変更しても、数量が自動で修正されないことため)
        /// </summary>
        internal bool IsSuuryouKesannFlg { get; set; }

        #region 休動処理
        // HACK 休動処理実装済みだが計量将軍では未使用
        /// <summary>
        /// 車輌休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_SHARYOUDao workclosedsharyouDao;

        /// <summary>
        /// 運転者休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_UNTENSHADao workcloseduntenshaDao;

        /// <summary>
        /// 搬入先休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_HANNYUUSAKIDao workclosedhannyuusakiDao;
        #endregion

        /// <summary>滞留区分(Regist, Update時にのみ使用)</summary>
        internal bool TaiyuuKbn { get; set; }

        internal bool isRegistered = false;
        private GET_SYSDATEDao dao;
        internal bool hasShow = false;

        /// <summary>
        /// モバイル連携DAO
        /// </summary>
        private IT_MOBISYO_RTDao mobisyoRtDao;

        internal string beforeCd = string.Empty;
        internal string popupCd = string.Empty;
        internal string popupCd2 = string.Empty;
        internal bool isInputError = false;

        PopupSearchSendParamDto dto1 = new PopupSearchSendParamDto();
        PopupSearchSendParamDto dto2 = new PopupSearchSendParamDto();
        PopupSearchSendParamDto dto3 = new PopupSearchSendParamDto();
        PopupSearchSendParamDto dto4 = new PopupSearchSendParamDto();
        PopupSearchSendParamDto dto5 = new PopupSearchSendParamDto();
        JoinMethodDto dto6 = new JoinMethodDto();
        SearchConditionsDto dto7 = new SearchConditionsDto();
        SearchConditionsDto dto8 = new SearchConditionsDto();
        SearchConditionsDto dto9 = new SearchConditionsDto();
        internal bool isClose = false;
        #endregion

        private IM_FILE_LINK_UKEIRE_JISSEKI_ENTRYDao fileLinkUJEDao;

        /// <summary>
        /// ファイルアップロード処理クラス
        /// </summary>
        private FileUploadLogic uploadLogic;

        private FILE_DATADAO fileDataDao;

        /// <summary>
        /// エラー表示用品名CD
        /// </summary>
        internal string ErrHinmeiCD;

        /// <summary>
        /// 取引先_請求情報マスタ
        /// </summary>
        private IM_TORIHIKISAKI_SEIKYUUDao TorihikisakiSeikyuDao;

        //伝票区分名（売上）
        public const string TENPYO_KBN_UR = "売上";
        //伝票区分１（売上）
        public const string TENPYO_KBN_1 = "1";
        //税区分（外税）
        public const string ZEI_KBN_1 = "1";
        //税区分（内税）
        public const string ZEI_KBN_2 = "2";
        //税区分（非課税）
        public const string ZEI_KBN_3 = "3";
        //税計算区分（伝票毎）
        public const string ZEIKEISAN_KBN_1 = "1";
        //税計算区分（請求毎）
        public const string ZEIKEISAN_KBN_2 = "2";
        //税計算区分（明細毎）
        public const string ZEIKEISAN_KBN_3 = "3";
        //金額0
        public const string KIGAKU_0 = "0";

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            // フォーム
            this.form = targetForm;

            // dto
            this.dto = new DTOClass();

            //DBサーバ日付を取得するため作成したDao
            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();

            // Accessor
            this.accessor = new Shougun.Scale.KeiryouNyuuryoku.Accessor.DBAccessor();
            this.commonAccesser = new Shougun.Core.Common.BusinessCommon.DBAccessor();

            // Utility
            this.controlUtil = new ControlUtility();

            this.TaiyuuKbn = false;

            #region 休動処理
            // HACK 休動処理実装済みだが計量将軍では未使用
            this.workclosedsharyouDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_SHARYOUDao>();
            this.workcloseduntenshaDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_UNTENSHADao>();
            this.workclosedhannyuusakiDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_HANNYUUSAKIDao>();
            #endregion
            this.mobisyoRtDao = DaoInitUtility.GetComponent<IT_MOBISYO_RTDao>();

            this.msgLogic = new MessageBoxShowLogic();

            if (this.form.selectDenshuKbnCd == DENSHU_KBN.NONE)
            {
                this.form.selectDenshuKbnCd = DENSHU_KBN.UKEIRE;
            }

            dto1 = new PopupSearchSendParamDto();
            dto1.And_Or = CONDITION_OPERATOR.AND;
            dto1.KeyName = "GYOUSHAKBN_UKEIRE";
            dto1.Value = "TRUE";
            dto5 = new PopupSearchSendParamDto();
            dto5.And_Or = CONDITION_OPERATOR.AND;
            dto5.KeyName = "UNPAN_JUTAKUSHA_KAISHA_KBN";
            dto5.Value = "TRUE";
            dto2 = new PopupSearchSendParamDto();
            dto2.And_Or = CONDITION_OPERATOR.AND;
            dto2.Control = "DENPYOU_DATE";
            dto2.KeyName = "TEKIYOU_BEGIN";

            dto3 = new PopupSearchSendParamDto();
            dto3.And_Or = CONDITION_OPERATOR.AND;
            dto3.Control = "GYOUSHA_CD";
            dto3.KeyName = "GYOUSHA_CD";
            dto4 = new PopupSearchSendParamDto();
            dto4.And_Or = CONDITION_OPERATOR.AND;
            dto4.KeyName = "M_GYOUSHA.GYOUSHAKBN_UKEIRE";
            dto4.Value = "TRUE";

            dto6 = new JoinMethodDto();
            dto6.Join = JOIN_METHOD.WHERE;
            dto6.LeftKeyColumn = "DENSHU_KBN_CD";
            dto6.LeftTable = "M_HINMEI";
            dto6.SearchCondition = new Collection<SearchConditionsDto>();

            dto7 = new SearchConditionsDto();
            dto7.And_Or = CONDITION_OPERATOR.AND;
            dto7.Condition = JUGGMENT_CONDITION.EQUALS;
            dto7.LeftColumn = "DENSHU_KBN_CD";
            dto7.Value = "1";
            dto7.ValueColumnType = DB_TYPE.SMALLINT;

            dto8 = new SearchConditionsDto();
            dto8.And_Or = CONDITION_OPERATOR.AND;
            dto8.Condition = JUGGMENT_CONDITION.EQUALS;
            dto8.LeftColumn = "DENSHU_KBN_CD";
            dto8.Value = "2";
            dto8.ValueColumnType = DB_TYPE.SMALLINT;

            dto9 = new SearchConditionsDto();
            dto9.And_Or = CONDITION_OPERATOR.OR;
            dto9.Condition = JUGGMENT_CONDITION.EQUALS;
            dto9.LeftColumn = "DENSHU_KBN_CD";
            dto9.Value = "9";
            dto9.ValueColumnType = DB_TYPE.SMALLINT;

            this.form.ismobile_mode = r_framework.Configuration.AppConfig.AppOptions.IsMobile();

            _tabPageManager = new TabPageManager(this.form.DETAIL_TAB);

            this.form.isfile_upload = r_framework.Configuration.AppConfig.AppOptions.IsFileUpload();

            this.fileLinkUJEDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_FILE_LINK_UKEIRE_JISSEKI_ENTRYDao>();

            this.fileDataDao = FileConnectionUtility.GetComponent<FILE_DATADAO>(); ;

            this.uploadLogic = new FileUploadLogic();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // LogicClassで初期化が必要な場合はここに記載
                this.nenRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.NONE;
                this.hiRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.NONE;

                footerForm = (BusinessBaseForm)this.form.Parent;
                headerForm = (UIHeaderForm)footerForm.headerForm;
                headerForm.logic = this;

                this.ChangeEnabledForInputControl(false);

                // DisplayInitでTemplateを書き換えているので、データをセットする前に実行すること
                // 滞留伝票の場合、WindowTypeが変更され、DisplayInitメソッドに影響があるため、このタイミングで実行する
                this.DisplayInit();

                // タブオーダーデータ取得
                GetStatus();

                if (this.dto.entryEntity.TAIRYUU_KBN)
                {
                    this.form.TairyuuNewFlg = true;
                    if (!this.dto.entryEntity.KEIRYOU_NUMBER.IsNull)
                    {
                        this.form.KeiryouNumber = long.Parse(this.dto.entryEntity.KEIRYOU_NUMBER.ToString());
                    }
                    // 滞留一覧から削除で開かれた場合は、モードを変更しない
                    if (HadChangedWindowTypeTairyuu(this.form.WindowType))
                    {
                        this.form.WindowType = tairyuuWindowType;
                    }
                    this.form.HeaderFormInit();
                }
                else
                {
                    this.form.TairyuuNewFlg = false;
                }

                this.SetUkeireShukkaKbn();

                this.EntryDataInit();

                foreach (var row in this.form.gcMultiRow1.Rows)
                {
                    // 単価と金額の活性制御
                    this.form.SetIchranReadOnly(row.Index);
                }

                if (!this.NumberingRowNo())
                {
                    return false;
                }

                if (this.form.ismobile_mode)
                {
                    if (!this.NumberingRowNo2())
                    {
                        return false;
                    }
                }

                if (this.form.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG)
                    || this.form.WindowType.Equals(WINDOW_TYPE.REFERENCE_WINDOW_FLAG))
                {
                    // 削除モード時には全コントロールをReadOnlyにする
                    this.ChangeEnabledForInputControl(true);
                    this.form.SHARYOU_NAME_RYAKU.Enabled = false;
                }
                if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    // 売上消費税率設定
                    if (!this.SetUriageShouhizeiRate())
                    {
                        return false;
                    }

                    // 支払消費税率設定
                    if (!this.SetShiharaiShouhizeiRate())
                    {
                        return false;
                    }

                }

                // 検索ボタン設定
                this.SetSearchButtonInfo();

                // 継続入力初期化
                if (string.IsNullOrEmpty(this.headerForm.KEIZOKU_NYUURYOKU_VALUE.Text))
                {
                    // 初期値設定
                    // 空以外の時には前の情報を引き継ぐ仕様
                    this.headerForm.KEIZOKU_NYUURYOKU_VALUE.Text = SalesPaymentConstans.KEIZOKU_NYUURYOKU_OFF;
                }

                // 車輌選択ポップアップ選択中フラグ初期化
                this.form.isSelectingSharyouCd = false;

                // Entryデータがある場合は車輌項目のデザインを初期化
                if (!WINDOW_TYPE.REFERENCE_WINDOW_FLAG.Equals(this.form.WindowType))
                {
                    if (this.dto.entryEntity != null
                        && !this.dto.entryEntity.SYSTEM_ID.IsNull
                        && !this.dto.entryEntity.SYSTEM_ID.IsNull
                        && !string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                    {
                        this.CheckShokuchiSharyou();
                    }
                }

                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        headerForm.windowTypeLabel.Text = "新規";
                        headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Aqua;
                        headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
                        break;
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        headerForm.windowTypeLabel.Text = "修正";
                        headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Yellow;
                        headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
                        break;
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        headerForm.windowTypeLabel.Text = "参照";
                        headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Orange;
                        headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
                        break;
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        headerForm.windowTypeLabel.Text = "削除";
                        headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Red;
                        headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.White;
                        break;
                }

                this.form.DENPYOU_DATE.IsInputErrorOccured = false;
                this.form.DENPYOU_DATE.BackColor = Constans.NOMAL_COLOR;
                this.form.IsDataLoading = false;

                this.getDefaultList();

                // TODO デザイナ上からMISHIYOU_TABPAGEタブを削除したい
                this.form.DETAIL_TAB.TabPages.Remove(this.form.MISHIYOU_TABPAGE);
                if (!this.form.ismobile_mode)
                {
                    this.form.DETAIL_TAB.TabPages.Remove(this.form.KEIRYOU_JISSEKI_TABPAGE);
                }

                if (!this.form.isfile_upload)
                {
                    this.form.JISSEKI_TAB.TabPages.Remove(this.form.TENPU_FILE_PAGE);
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        /// <param name="registeredFlag">登録後処理かどうか。デフォルト：false</param>
        public bool ButtonInit(bool registeredFlag = false)
        {
            try
            {
                LogUtility.DebugMethodStart(registeredFlag);
                var buttonSetting = this.CreateButtonInfo();
                ButtonControlUtility.SetButtonInfo(buttonSetting, footerForm, this.form.WindowType);

                // 初期化
                foreach (var button in buttonSetting)
                {
                    var cont = controlUtil.FindControl(footerForm, button.ButtonName);
                    if (!string.IsNullOrEmpty(cont.Text)) cont.Enabled = true;
                }

                //D3,D4(計量将軍+マニ(L))のみ、サブファンクション３を表示
                if (!("D3".Equals(AppConfig.Series) || "D4".Equals(AppConfig.Series)))
                {
                    this.footerForm.bt_process3.Text = "";
                }

                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 修正モードの場合、登録後に初めて活性化するボタンがあるので制御
                        if (registeredFlag)
                        {
                            // 登録後処理
                            this.footerForm.bt_func1.Enabled = true;
                            this.footerForm.bt_func2.Enabled = true;
                            this.footerForm.bt_func3.Enabled = true;
                            this.footerForm.bt_func4.Enabled = true;
                            this.footerForm.bt_func5.Enabled = false;
                            this.footerForm.bt_func6.Enabled = false;
                            if (string.IsNullOrEmpty(this.form.SHARYOU_EMPTY_JYUURYOU.Text) || this.form.selectDenshuKbnCd == DENSHU_KBN.SHUKKA)
                            {
                                this.footerForm.bt_func8.Enabled = false;
                            }
                            else
                            {
                                this.footerForm.bt_func8.Enabled = true;
                            }
                            this.footerForm.bt_func9.Enabled = false;
                            this.footerForm.bt_func10.Enabled = false;
                            this.footerForm.bt_func11.Enabled = false;
                            this.footerForm.bt_process1.Enabled = false;
                            this.footerForm.bt_process2.Enabled = false;
                            if ("D3".Equals(AppConfig.Series) || "D4".Equals(AppConfig.Series))
                            {
                                this.footerForm.bt_process3.Enabled = true;
                            }
                            else
                            {
                                this.footerForm.bt_process3.Enabled = false;
                            }
                            this.footerForm.bt_process4.Enabled = true;
                            this.footerForm.bt_process5.Enabled = false;
                        }
                        else
                        {
                            this.footerForm.bt_func1.Enabled = true;
                            this.footerForm.bt_func2.Enabled = true;
                            this.footerForm.bt_func3.Enabled = false;
                            this.footerForm.bt_func4.Enabled = true;
                            this.footerForm.bt_func5.Enabled = true;
                            this.footerForm.bt_func6.Enabled = false;
                            if (string.IsNullOrEmpty(this.form.SHARYOU_EMPTY_JYUURYOU.Text) || this.form.selectDenshuKbnCd == DENSHU_KBN.SHUKKA)
                            {
                                this.footerForm.bt_func8.Enabled = false;
                            }
                            else
                            {
                                this.footerForm.bt_func8.Enabled = true;
                            }
                            this.footerForm.bt_func9.Enabled = true;
                            this.footerForm.bt_func10.Enabled = true;
                            this.footerForm.bt_func11.Enabled = true;
                            this.footerForm.bt_process1.Enabled = false;
                            this.footerForm.bt_process2.Enabled = false;
                            if ("D3".Equals(AppConfig.Series) || "D4".Equals(AppConfig.Series))
                            {
                                this.footerForm.bt_process3.Enabled = true;
                            }
                            else
                            {
                                this.footerForm.bt_process3.Enabled = false;
                            }
                            this.footerForm.bt_process4.Enabled = true;
                            this.footerForm.bt_process5.Enabled = false;
                        }
                        break;

                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        if (!string.IsNullOrEmpty(this.form.ENTRY_NUMBER.Text))
                        {
                            this.footerForm.bt_func6.Enabled = false;
                        }
                        else
                        {
                            this.footerForm.bt_func6.Enabled = true;
                        }
                        if (string.IsNullOrEmpty(this.form.SHARYOU_EMPTY_JYUURYOU.Text) || this.form.selectDenshuKbnCd == DENSHU_KBN.SHUKKA)
                        {
                            this.footerForm.bt_func8.Enabled = false;
                        }
                        else
                        {
                            this.footerForm.bt_func8.Enabled = true;
                        }
                        this.footerForm.bt_process1.Enabled = false;
                        this.footerForm.bt_process2.Enabled = false;
                        this.footerForm.bt_process3.Enabled = false;
                        this.footerForm.bt_process4.Enabled = true;
                        this.footerForm.bt_process5.Enabled = false;
                        break;
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        this.footerForm.bt_func1.Enabled = false;
                        this.footerForm.bt_func2.Enabled = true;
                        this.footerForm.bt_func3.Enabled = false;
                        this.footerForm.bt_func4.Enabled = false;
                        this.footerForm.bt_func5.Enabled = false;
                        this.footerForm.bt_func6.Enabled = false;
                        this.footerForm.bt_func7.Enabled = true;
                        this.footerForm.bt_func8.Enabled = false;
                        this.footerForm.bt_func9.Enabled = false;
                        this.footerForm.bt_func10.Enabled = false;
                        this.footerForm.bt_func11.Enabled = false;
                        this.footerForm.bt_func12.Enabled = true;
                        this.footerForm.bt_process1.Enabled = false;
                        this.footerForm.bt_process2.Enabled = false;
                        this.footerForm.bt_process3.Enabled = false;
                        this.footerForm.bt_process4.Enabled = true;
                        this.footerForm.bt_process5.Enabled = false;
                        break;
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        this.footerForm.bt_func1.Enabled = false;
                        this.footerForm.bt_func2.Enabled = false;
                        this.footerForm.bt_func3.Enabled = false;
                        this.footerForm.bt_func4.Enabled = false;
                        this.footerForm.bt_func5.Enabled = false;
                        this.footerForm.bt_func6.Enabled = false;
                        this.footerForm.bt_func7.Enabled = true;
                        this.footerForm.bt_func8.Enabled = false;
                        this.footerForm.bt_func9.Enabled = true;
                        this.footerForm.bt_func10.Enabled = false;
                        this.footerForm.bt_func11.Enabled = false;
                        this.footerForm.bt_func12.Enabled = true;
                        this.footerForm.bt_process1.Enabled = false;
                        this.footerForm.bt_process2.Enabled = false;
                        this.footerForm.bt_process3.Enabled = false;
                        this.footerForm.bt_process4.Enabled = false;
                        this.footerForm.bt_process5.Enabled = false;
                        break;
                    default:
                        break;
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ButtonInit", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// イベント初期化処理
        /// </summary>
        public bool EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ボタン(F1)イベント
                footerForm.bt_func1.Click += new EventHandler(this.form.Weight_Click);

                // 新規ボタン(F2)イベント
                footerForm.bt_func2.Click += new EventHandler(this.form.ChangeNewWindow);

                // 修正ボタン(F3)イベント
                footerForm.bt_func3.Click += new EventHandler(this.form.ChangeUpdateWindow);

                // 品名検索ボタン(F4)イベント
                footerForm.bt_func4.Click += new EventHandler(this.form.SearchHinmei);

                // 滞留ボタン(F5)イベント
                this.form.C_Regist(footerForm.bt_func5);
                footerForm.bt_func5.Click += new EventHandler(this.form.TairyuuRegist);
                footerForm.bt_func5.ProcessKbn = PROCESS_KBN.NEW;

                // 切替ボタン(F6)イベント
                footerForm.bt_func6.Click += new EventHandler(this.form.ChangeTabPage);

                // 一覧ボタン(F7)イベント
                footerForm.bt_func7.Click += new EventHandler(this.form.ShowDenpyouIchiran);

                // 車輌空車取込イベント
                footerForm.bt_func8.Click += new EventHandler(this.form.SharyouKuushaJyuuryouTorikomi);

                // 登録ボタン(F9)イベント
                this.form.C_Regist(footerForm.bt_func9);
                footerForm.bt_func9.Click += new EventHandler(this.form.Regist);
                footerForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                // 行挿入ボタン(F10)イベント
                footerForm.bt_func10.Click += new EventHandler(this.form.AddRow);

                // 行挿入ボタン(F11)イベント
                footerForm.bt_func11.Click += new EventHandler(this.form.RemoveRow);

                // 閉じるボタン(F12)イベント生成
                footerForm.bt_func12.Click += new EventHandler(this.form.FormClose);

                // プロセスボタンイベント生成
                footerForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);
                footerForm.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);
                footerForm.bt_process3.Click += new EventHandler(this.form.bt_process3_Click);
                footerForm.bt_process4.Click += new EventHandler(this.form.bt_process4_Click);
                footerForm.bt_process5.Click += new EventHandler(this.form.bt_process5_Click);

                // コントロールのイベント
                this.form.TORIHIKISAKI_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.GYOUSHA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.GENBA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.NIZUMI_GYOUSHA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.NIZUMI_GENBA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.NIOROSHI_GYOUSHA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.NIOROSHI_GENBA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.UNPAN_GYOUSHA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.SHARYOU_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.SHARYOU_CD.TextChanged += new EventHandler(this.SHARYOU_CD_TextChanged);

                // 全てのコントロールのEnterイベントに追加
                foreach (Control ctrl in this.form.Controls)
                {
                    ctrl.Enter -= new EventHandler(this.GetControlEnter);
                    ctrl.Enter += new EventHandler(this.GetControlEnter);
                }
                foreach (Control ctrl in this.headerForm.Controls)
                {
                    ctrl.Enter -= new EventHandler(this.GetControlEnter);
                    ctrl.Enter += new EventHandler(this.GetControlEnter);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }


        /// <summary>
        /// 全コントロールのEnterイベントで必ず通る
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetControlEnter(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;

            if (ctrl is TextBox || ctrl is GrapeCity.Win.MultiRow.GcMultiRow || ctrl is TabControl)
            {
                this.form.beforbeforControlName = this.form.beforControlName;
                this.form.beforControlName = ctrl.Name;
            }
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// 表示制御
        /// </summary>
        private void DisplayInit()
        {
            this.form.KEITAI_KBN_CD.PopupDataHeaderTitle = new string[] { "形態区分CD", "形態区分名" };
            this.form.KEITAI_KBN_CD.PopupDataSource = this.CreateKeitaiKbnPopupDataSource();

            this.form.ENTRY_NUMBER.ReadOnly = false;

            this.ChangePropertyForGC(null, new string[] { "STACK_JYUURYOU", "EMPTY_JYUURYOU" }, "ReadOnly", false);

            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    // 重量系
                    this.ChangePropertyForGC(null, new string[] { "CHOUSEI_JYUURYOU", "CHOUSEI_PERCENT" }, "ReadOnly", true);
                    break;

                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    // 重量系
                    this.form.ENTRY_NUMBER.ReadOnly = true;
                    break;

                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    // 削除モードの場合、
                    // すべてをReadOnlyにしたいので初期化の最後に実施
                    break;

                default:
                    break;
            }

        }

        /// <summary>
        /// 必須チェックの設定を初期化します
        /// </summary>
        internal bool RequiredSettingInit()
        {
            try
            {
                // Entry
                this.headerForm.KYOTEN_CD.RegistCheckMethod = null;
                this.form.NYUURYOKU_TANTOUSHA_CD.RegistCheckMethod = null;
                this.form.TORIHIKISAKI_CD.RegistCheckMethod = null;
                this.form.GYOUSHA_CD.RegistCheckMethod = null;
                this.form.DENPYOU_DATE.RegistCheckMethod = null;
                this.form.SEIKYUU_TORIHIKI_KBN_CD.RegistCheckMethod = null;
                this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.RegistCheckMethod = null;
                this.form.SEIKYUU_ZEI_KBN_CD.RegistCheckMethod = null;
                this.form.SHIHARAI_TORIHIKI_KBN_CD.RegistCheckMethod = null;
                this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.RegistCheckMethod = null;
                this.form.SHIHARAI_ZEI_KBN_CD.RegistCheckMethod = null;
                this.form.RECEIPT_KBN_CD.RegistCheckMethod = null;
                this.form.CASHEIR_RENDOU_KBN_CD.RegistCheckMethod = null;

                // Detail
                this.form.gcMultiRow1.SuspendLayout();

                foreach (var o in this.form.gcMultiRow1.Rows)
                {
                    var obj2 = controlUtil.FindControl(o.Cells.ToArray(), new string[] { CELL_NAME_HINMEI_CD, CELL_NAME_HINMEI_NAME, CELL_NAME_SUURYOU, CELL_NAME_UNIT_CD, CELL_NAME_KINGAKU });
                    foreach (var target in obj2)
                    {
                        PropertyUtility.SetValue(target, "RegistCheckMethod", null);
                    }
                }

                this.form.gcMultiRow1.ResumeLayout();

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("RequiredSettingInit", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RequiredSettingInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 必須チェックの設定を動的に生成
        /// </summary>
        /// <param name="tairyuuKbn">滞留登録かどうか</param>
        internal bool SetRequiredSetting(bool tairyuuKbn)
        {
            try
            {
                // 初期化
                if (!this.RequiredSettingInit())
                {
                    return false;
                }

                // 設定
                SelectCheckDto existCheck = new SelectCheckDto();
                existCheck.CheckMethodName = "必須チェック";
                Collection<SelectCheckDto> excitChecks = new Collection<SelectCheckDto>();
                excitChecks.Add(existCheck);

                if (tairyuuKbn)
                {
                    // 滞留登録
                    this.form.NYUURYOKU_TANTOUSHA_CD.RegistCheckMethod = excitChecks;
                    this.form.DENPYOU_DATE.RegistCheckMethod = excitChecks;
                    this.headerForm.KYOTEN_CD.RegistCheckMethod = excitChecks;
                }
                else
                {
                    // 登録
                    this.headerForm.KYOTEN_CD.RegistCheckMethod = excitChecks;
                    this.form.NYUURYOKU_TANTOUSHA_CD.RegistCheckMethod = excitChecks;
                    this.form.GYOUSHA_CD.RegistCheckMethod = excitChecks;
                    this.form.DENPYOU_DATE.RegistCheckMethod = excitChecks;
                    this.form.SEIKYUU_TORIHIKI_KBN_CD.RegistCheckMethod = excitChecks;
                    this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.RegistCheckMethod = excitChecks;
                    this.form.SEIKYUU_ZEI_KBN_CD.RegistCheckMethod = excitChecks;
                    this.form.SHIHARAI_TORIHIKI_KBN_CD.RegistCheckMethod = excitChecks;
                    this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.RegistCheckMethod = excitChecks;
                    this.form.SHIHARAI_ZEI_KBN_CD.RegistCheckMethod = excitChecks;
                    if (this.form.RECEIPT_KBN_CD.Enabled)
                    {
                        this.form.RECEIPT_KBN_CD.RegistCheckMethod = excitChecks;
                    }
                    if (this.form.CASHEIR_RENDOU_KBN_CD.Enabled)
                    {
                        this.form.CASHEIR_RENDOU_KBN_CD.RegistCheckMethod = excitChecks;
                    }

                    this.form.gcMultiRow1.SuspendLayout();

                    foreach (var o in this.form.gcMultiRow1.Rows)
                    {
                        string[] registCheckTarget = new string[] { CELL_NAME_HINMEI_CD, CELL_NAME_HINMEI_NAME, CELL_NAME_SUURYOU, CELL_NAME_UNIT_CD, CELL_NAME_KINGAKU };
                        var obj2 = controlUtil.FindControl(o.Cells.ToArray(), registCheckTarget);
                        foreach (var target in obj2)
                        {
                            var visible = target.GetType().GetProperty("Visible");
                            if (visible != null && (bool)visible.GetValue(target, null))
                            {
                                PropertyUtility.SetValue(target, "RegistCheckMethod", excitChecks);
                            }
                        }
                    }

                    this.form.gcMultiRow1.ResumeLayout();
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetRequiredSetting", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetRequiredSetting", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }

        }

        /// <summary>
        /// 画面のT_KEIRYOU_ENTRY部分の初期化処理
        /// </summary>
        internal void EntryDataInit()
        {
            LogUtility.DebugMethodStart();

            // DBには無い値などの設定
            denpyouKbnDictionary.Clear();
            youkiDictionary.Clear();
            unitDictionary.Clear();
            hinmeiDictionary.Clear();

            var denpyous = this.accessor.GetAllDenpyouKbn();
            var youkis = this.accessor.GetAllYouki();
            var units = this.accessor.GetAllUnit();
            var hinmeis = this.accessor.GetAllHinmei();

            foreach (var denpyou in denpyous)
            {
                denpyouKbnDictionary.Add((short)denpyou.DENPYOU_KBN_CD, denpyou);
            }

            foreach (var youki in youkis)
            {
                youkiDictionary.Add(youki.YOUKI_CD, youki);
            }

            foreach (var unit in units)
            {
                unitDictionary.Add((short)unit.UNIT_CD, unit);
            }

            foreach (var hinmei in hinmeis)
            {
                hinmeiDictionary.Add(hinmei.HINMEI_CD, hinmei);
            }

            SqlInt32 renbanValue = -1;
            // 画面毎に設定が異なるコントロールの初期化(コピペしやすいようにするため)
            // 計量番号
            this.form.ENTRY_NUMBER.DBFieldsName = "KEIRYOU_NUMBER";
            this.form.ENTRY_NUMBER.ItemDefinedTypes = DB_TYPE.BIGINT.ToTypeString();

            // 連番ラベル、連番
            if (this.dto.sysInfoEntity.SYS_RENBAN_HOUHOU_KBN == SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBN_HIRENBAN)
            {
                this.form.RENBAN_LABEL.Text = SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBNExt.ToTypeString(SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBN.HIRENBAN);
                this.form.RENBAN.DBFieldsName = "DATE_NUMBER";
                this.form.RENBAN.ItemDefinedTypes = DB_TYPE.INT.ToTypeString();
                if (!this.dto.entryEntity.DATE_NUMBER.IsNull)
                {
                    this.form.RENBAN.Text = this.dto.entryEntity.DATE_NUMBER.ToString();
                }
                else
                {
                    this.form.RENBAN.Text = "";
                }
                renbanValue = this.dto.entryEntity.DATE_NUMBER;
            }
            else if (this.dto.sysInfoEntity.SYS_RENBAN_HOUHOU_KBN == SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBN_NENRENBAN)
            {
                this.form.RENBAN_LABEL.Text = SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBNExt.ToTypeString(SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBN.NENRENBAN);
                this.form.RENBAN.DBFieldsName = "YEAR_NUMBER";
                this.form.RENBAN.ItemDefinedTypes = DB_TYPE.INT.ToTypeString();
                if (!this.dto.entryEntity.YEAR_NUMBER.IsNull)
                {
                    this.form.RENBAN.Text = this.dto.entryEntity.YEAR_NUMBER.ToString();
                }
                else
                {
                    this.form.RENBAN.Text = "";
                }
                renbanValue = this.dto.entryEntity.YEAR_NUMBER;
            }

            // 日付系初期値設定
            if (!SalesPaymentConstans.KEIZOKU_NYUURYOKU_ON.Equals(this.headerForm.KEIZOKU_NYUURYOKU_VALUE.Text))
            {
                this.form.DENPYOU_DATE.Value = this.footerForm.sysDate;
            }

            long systemId = -1;
            int seq = -1;

            if (!this.dto.entryEntity.SYSTEM_ID.IsNull) systemId = (long)this.dto.entryEntity.SYSTEM_ID;
            if (!this.dto.entryEntity.SEQ.IsNull) seq = (int)this.dto.entryEntity.SEQ;

            // 調整値の表示形式初期化
            CalcValueFormatSettingInit();

            this.ClearEntryData();

            // モードによる制御
            if (this.IsRequireData())
            {
                // ヘッダー Start
                // 拠点
                if (!this.dto.entryEntity.KYOTEN_CD.IsNull)
                {
                    headerForm.KYOTEN_CD.Text = this.dto.entryEntity.KYOTEN_CD.ToString().PadLeft(headerForm.KYOTEN_CD.MaxLength, '0');
                }
                headerForm.KYOTEN_NAME_RYAKU.Text = this.dto.kyotenEntity.KYOTEN_NAME_RYAKU;

                // 登録者情報
                if (!string.IsNullOrEmpty(this.dto.entryEntity.CREATE_USER))
                {
                    headerForm.CreateUser.Text = this.dto.entryEntity.CREATE_USER;
                }
                if (!this.dto.entryEntity.CREATE_DATE.IsNull
                    && !string.IsNullOrEmpty(this.dto.entryEntity.CREATE_DATE.ToString()))
                {
                    headerForm.CreateDate.Text = this.dto.entryEntity.CREATE_DATE.ToString();
                }

                // 更新者情報
                if (!string.IsNullOrEmpty(this.dto.entryEntity.UPDATE_USER))
                {
                    headerForm.LastUpdateUser.Text = this.dto.entryEntity.UPDATE_USER;
                }

                if (this.dto.entryEntity.KIHON_KEIRYOU.IsNull)
                {
                    this.headerForm.KIHON_KEIRYOU.Text = "1";
                    this.form.selectDenshuKbnCd = DENSHU_KBN.UKEIRE;
                }
                else
                {
                    this.headerForm.KIHON_KEIRYOU.Text = Convert.ToString(this.dto.entryEntity.KIHON_KEIRYOU.Value);
                }
                if (this.headerForm.KIHON_KEIRYOU.Text == "1")
                {
                    this.form.selectDenshuKbnCd = DENSHU_KBN.UKEIRE;
                }
                else
                {
                    this.form.selectDenshuKbnCd = DENSHU_KBN.SHUKKA;
                }
                this.SetUkeireShukkaKbn();
                // ヘッダー End

                // 詳細 Start
                // 計量番号
                this.form.ENTRY_NUMBER.Text = this.dto.entryEntity.KEIRYOU_NUMBER.ToString();
                if (this.dto.entryEntity.KEIRYOU_KBN.IsNull)
                {
                    this.form.KEIRYOU_KBN_CD.Text = string.Empty;
                }
                else
                {
                    this.form.KEIRYOU_KBN_CD.Text = this.dto.entryEntity.KEIRYOU_KBN.Value.ToString();
                }
                // 連番
                if (!renbanValue.IsNull)
                {
                    this.form.RENBAN.Text = renbanValue.ToString();
                }
                else
                {
                    this.form.RENBAN.Text = "";
                }
                // 伝票日付
                if (!this.dto.entryEntity.DENPYOU_DATE.IsNull
                    && !string.IsNullOrEmpty(this.dto.entryEntity.DENPYOU_DATE.ToString()))
                {
                    this.form.DENPYOU_DATE.Text = this.dto.entryEntity.DENPYOU_DATE.ToString();
                }
                // 入力担当者
                if (WINDOW_TYPE.NEW_WINDOW_FLAG == this.form.WindowType || WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType)
                {
                    if (CommonShogunData.LOGIN_USER_INFO != null
                        && !string.IsNullOrEmpty(CommonShogunData.LOGIN_USER_INFO.SHAIN_CD)
                        && CommonShogunData.LOGIN_USER_INFO.NYUURYOKU_TANTOU_KBN)
                    {
                        this.form.NYUURYOKU_TANTOUSHA_CD.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_CD;
                        this.form.NYUURYOKU_TANTOUSHA_NAME.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME_RYAKU;
                        strNyuryokuTantousyaName = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME;
                    }
                    else
                    {
                        this.form.NYUURYOKU_TANTOUSHA_CD.Text = string.Empty;
                        this.form.NYUURYOKU_TANTOUSHA_NAME.Text = string.Empty;
                        strNyuryokuTantousyaName = string.Empty;
                    }
                }
                else
                {
                    this.form.NYUURYOKU_TANTOUSHA_CD.Text = this.dto.entryEntity.NYUURYOKU_TANTOUSHA_CD;
                    this.form.NYUURYOKU_TANTOUSHA_NAME.Text = this.dto.entryEntity.NYUURYOKU_TANTOUSHA_NAME;
                    strNyuryokuTantousyaName = this.dto.entryEntity.NYUURYOKU_TANTOUSHA_NAME;
                }
                // 売上消費税
                if (!this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE.IsNull)
                {
                    this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text = this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE.ToString();
                }
                // 支払消費税
                if (!this.dto.entryEntity.SHIHARAI_SHOUHIZEI_RATE.IsNull)
                {
                    this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text = this.dto.entryEntity.SHIHARAI_SHOUHIZEI_RATE.ToString();
                }
                // 運搬業者
                this.form.UNPAN_GYOUSHA_CD.Text = this.dto.entryEntity.UNPAN_GYOUSHA_CD;
                this.form.UNPAN_GYOUSHA_NAME.Text = this.dto.entryEntity.UNPAN_GYOUSHA_NAME;
                // 車輌
                this.form.SHARYOU_CD.Text = this.dto.entryEntity.SHARYOU_CD;
                this.form.SHARYOU_NAME_RYAKU.Text = this.dto.entryEntity.SHARYOU_NAME;
                // 車種
                this.form.SHASHU_CD.Text = this.dto.entryEntity.SHASHU_CD;
                this.form.SHASHU_NAME.Text = this.dto.entryEntity.SHASHU_NAME;
                // 運転者
                this.form.UNTENSHA_CD.Text = this.dto.entryEntity.UNTENSHA_CD;
                this.form.UNTENSHA_NAME.Text = this.dto.entryEntity.UNTENSHA_NAME;
                var sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null, SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()), this.headerForm.KIHON_KEIRYOU.Text);
                if (sharyouEntitys != null && sharyouEntitys.Length > 0 && !sharyouEntitys[0].KUUSHA_JYURYO.IsNull)
                {
                    this.form.SHARYOU_EMPTY_JYUURYOU.Text = sharyouEntitys[0].KUUSHA_JYURYO.ToString();
                }

                // 業者
                this.form.GYOUSHA_CD.Text = this.dto.entryEntity.GYOUSHA_CD;
                this.form.GYOUSHA_NAME_RYAKU.Text = this.dto.entryEntity.GYOUSHA_NAME;
                // 現場
                this.form.GENBA_CD.Text = this.dto.entryEntity.GENBA_CD;
                this.form.GENBA_NAME_RYAKU.Text = this.dto.entryEntity.GENBA_NAME;
                strGenbaName = "";//クリア
                bool catchErr = true;
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) && !string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    // 印刷用現場名
                    var retData = this.accessor.GetGenba(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                    if (!catchErr)
                    {
                        throw new Exception("");
                    }
                    var genbaEntity = retData;
                    if (genbaEntity != null)
                    {
                        strGenbaName = genbaEntity.GENBA_NAME1 + genbaEntity.GENBA_NAME2;
                    }
                }
                // 伝票備考
                this.form.DENPYOU_BIKOU.Text = this.dto.entryEntity.DENPYOU_BIKOU;
                // 滞留備考
                this.form.TAIRYUU_BIKOU.Text = this.dto.entryEntity.TAIRYUU_BIKOU;

                // 総重量
                if (!this.dto.entryEntity.STACK_JYUURYOU.IsNull)
                {
                    this.form.STACK_JYUURYOU.Text = this.dto.entryEntity.STACK_JYUURYOU.Value.ToString();
                }
                if (!this.dto.entryEntity.STACK_KEIRYOU_TIME.IsNull)
                {
                    this.form.STACK_KEIRYOU_TIME.Text = this.dto.entryEntity.STACK_KEIRYOU_TIME.Value.ToString("HH:mm");
                }
                // 空車重量
                if (!this.dto.entryEntity.EMPTY_JYUURYOU.IsNull)
                {
                    this.form.EMPTY_JYUURYOU.Text = this.dto.entryEntity.EMPTY_JYUURYOU.Value.ToString();
                }
                if (!this.dto.entryEntity.EMPTY_KEIRYOU_TIME.IsNull)
                {
                    this.form.EMPTY_KEIRYOU_TIME.Text = this.dto.entryEntity.EMPTY_KEIRYOU_TIME.Value.ToString("HH:mm");
                }
                // マニフェストタブ
                // 廃棄物区分
                if (!this.dto.entryEntity.HAIKI_KBN_CD.IsNull)
                {
                    this.form.MANIFEST_HAIKI_KBN_CD.Text = this.dto.entryEntity.HAIKI_KBN_CD.ToString();
                    if (this.form.MANIFEST_HAIKI_KBN_CD.Text == "1")
                    {
                        this.form.MANIFEST_HAIKI_KBN_NAME.Text = "産廃";
                    }
                    else if (this.form.MANIFEST_HAIKI_KBN_CD.Text == "2")
                    {
                        this.form.MANIFEST_HAIKI_KBN_NAME.Text = "建廃";
                    }
                    else if (this.form.MANIFEST_HAIKI_KBN_CD.Text == "3")
                    {
                        this.form.MANIFEST_HAIKI_KBN_NAME.Text = "積替";
                    }
                    else
                    {
                        this.form.MANIFEST_HAIKI_KBN_NAME.Text = "";
                    }
                }
                else
                {
                    this.form.MANIFEST_HAIKI_KBN_CD.Text = string.Empty;
                    this.form.MANIFEST_HAIKI_KBN_NAME.Text = string.Empty;
                }
                catchErr = true;
                M_GYOUSHA gyousha = new M_GYOUSHA();
                M_GENBA genba = new M_GENBA();
                // 排出事業者
                if (!string.IsNullOrEmpty(this.dto.entryEntity.HST_GYOUSHA_CD))
                {
                    this.form.HST_GYOUSHA_CD.Text = this.dto.entryEntity.HST_GYOUSHA_CD;
                    gyousha = this.accessor.GetGyousha(this.form.HST_GYOUSHA_CD.Text, out catchErr);
                    if (!catchErr)
                    {
                        throw new Exception("");
                    }
                    if (gyousha != null)
                    {
                        this.form.HST_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                }
                // 排出事業場
                if (!string.IsNullOrEmpty(this.dto.entryEntity.HST_GENBA_CD))
                {
                    this.form.HST_GENBA_CD.Text = this.dto.entryEntity.HST_GENBA_CD;
                    genba = this.accessor.GetGenba(this.form.HST_GYOUSHA_CD.Text, this.form.HST_GENBA_CD.Text, out catchErr);
                    if (!catchErr)
                    {
                        throw new Exception("");
                    }
                    if (genba != null)
                    {
                        this.form.HST_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                    }
                }
                // 処分業者
                if (!string.IsNullOrEmpty(this.dto.entryEntity.SBN_GYOUSHA_CD))
                {
                    this.form.SBN_GYOUSHA_CD.Text = this.dto.entryEntity.SBN_GYOUSHA_CD;
                    gyousha = this.accessor.GetGyousha(this.form.SBN_GYOUSHA_CD.Text, out catchErr);
                    if (!catchErr)
                    {
                        throw new Exception("");
                    }
                    if (gyousha != null)
                    {
                        this.form.SBN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                }
                // 処分事業場
                if (!string.IsNullOrEmpty(this.dto.entryEntity.SBN_GENBA_CD))
                {
                    this.form.SBN_GENBA_CD.Text = this.dto.entryEntity.SBN_GENBA_CD;
                    genba = this.accessor.GetGenba(this.form.SBN_GYOUSHA_CD.Text, this.form.SBN_GENBA_CD.Text, out catchErr);
                    if (!catchErr)
                    {
                        throw new Exception("");
                    }
                    if (genba != null)
                    {
                        this.form.SBN_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                    }
                }
                // 最終処分業者
                if (!string.IsNullOrEmpty(this.dto.entryEntity.LAST_SBN_GYOUSHA_CD))
                {
                    this.form.LAST_SBN_GYOUSHA_CD.Text = this.dto.entryEntity.LAST_SBN_GYOUSHA_CD;
                    gyousha = this.accessor.GetGyousha(this.form.LAST_SBN_GYOUSHA_CD.Text, out catchErr);
                    if (!catchErr)
                    {
                        throw new Exception("");
                    }
                    if (gyousha != null)
                    {
                        this.form.LAST_SBN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                }
                // 最終処分場
                if (!string.IsNullOrEmpty(this.dto.entryEntity.LAST_SBN_GENBA_CD))
                {
                    this.form.LAST_SBN_GENBA_CD.Text = this.dto.entryEntity.LAST_SBN_GENBA_CD;
                    genba = this.accessor.GetGenba(this.form.LAST_SBN_GYOUSHA_CD.Text, this.form.LAST_SBN_GENBA_CD.Text, out catchErr);
                    if (!catchErr)
                    {
                        throw new Exception("");
                    }
                    if (genba != null)
                    {
                        this.form.LAST_SBN_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                    }
                }

                // その他タブ
                // 営業担当者
                this.form.EIGYOU_TANTOUSHA_CD.Text = this.dto.entryEntity.EIGYOU_TANTOUSHA_CD;
                this.form.EIGYOU_TANTOUSHA_NAME.Text = this.dto.entryEntity.EIGYOU_TANTOUSHA_NAME;
                // 荷降業者
                this.form.NIOROSHI_GYOUSHA_CD.Text = this.dto.entryEntity.NIOROSHI_GYOUSHA_CD;
                this.form.NIOROSHI_GYOUSHA_NAME.Text = this.dto.entryEntity.NIOROSHI_GYOUSHA_NAME;
                // 荷卸現場
                this.form.NIOROSHI_GENBA_CD.Text = this.dto.entryEntity.NIOROSHI_GENBA_CD;
                this.form.NIOROSHI_GENBA_NAME.Text = this.dto.entryEntity.NIOROSHI_GENBA_NAME;
                // 荷積業者
                this.form.NIZUMI_GYOUSHA_CD.Text = this.dto.entryEntity.NIZUMI_GYOUSHA_CD;
                this.form.NIZUMI_GYOUSHA_NAME.Text = this.dto.entryEntity.NIZUMI_GYOUSHA_NAME;
                // 荷積現場
                this.form.NIZUMI_GENBA_CD.Text = this.dto.entryEntity.NIZUMI_GENBA_CD;
                this.form.NIZUMI_GENBA_NAME.Text = this.dto.entryEntity.NIZUMI_GENBA_NAME;
                // 台貫
                if (!this.dto.entryEntity.DAIKAN_KBN.IsNull)
                {
                    this.form.DAIKAN_KBN.Text = this.dto.entryEntity.DAIKAN_KBN.ToString();
                }
                else
                {
                    this.form.DAIKAN_KBN.Text = string.Empty;
                }
                this.form.DAIKAN_KBN_NAME.Text = SalesPaymentConstans.DAIKAN_KBNExt.ToTypeString(SalesPaymentConstans.DAIKAN_KBNExt.ToDaikanKbn(this.form.DAIKAN_KBN.Text));
                // 形態区分
                if (!this.dto.entryEntity.KEITAI_KBN_CD.IsNull)
                {
                    this.form.KEITAI_KBN_CD.Text = this.dto.entryEntity.KEITAI_KBN_CD.ToString().PadLeft(this.form.KEITAI_KBN_CD.MaxLength, '0');
                }
                else
                {
                    this.form.KEITAI_KBN_CD.Text = string.Empty;
                }
                // 形態区分名
                this.form.KEITAI_KBN_NAME_RYAKU.Text = this.dto.keitaiKbnEntity.KEITAI_KBN_NAME_RYAKU;
                // マニフェスト種類
                if (!this.dto.entryEntity.MANIFEST_SHURUI_CD.IsNull)
                {
                    this.form.MANIFEST_SHURUI_CD.Text = this.dto.entryEntity.MANIFEST_SHURUI_CD.ToString().PadLeft(this.form.MANIFEST_SHURUI_CD.MaxLength, '0');
                }
                else
                {
                    this.form.MANIFEST_SHURUI_CD.Text = string.Empty;
                }
                this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = this.dto.manifestShuruiEntity.MANIFEST_SHURUI_NAME_RYAKU;
                // マニフェスト手配
                if (!this.dto.entryEntity.MANIFEST_TEHAI_CD.IsNull)
                {
                    this.form.MANIFEST_TEHAI_CD.Text = this.dto.entryEntity.MANIFEST_TEHAI_CD.ToString().PadLeft(this.form.MANIFEST_TEHAI_CD.MaxLength, '0');
                }
                if (!string.IsNullOrEmpty(this.dto.manifestTehaiEntity.MANIFEST_TEHAI_NAME_RYAKU))
                {
                    this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = this.dto.manifestTehaiEntity.MANIFEST_TEHAI_NAME_RYAKU.ToString();
                }

                // 伝票発行タブ
                // 取引先
                this.form.TORIHIKISAKI_CD.Text = this.dto.entryEntity.TORIHIKISAKI_CD;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = this.dto.entryEntity.TORIHIKISAKI_NAME;

                if (this.dto.entryEntity.URIAGE_TORIHIKI_KBN_CD.IsNull)
                {
                    this.form.SEIKYUU_TORIHIKI_KBN_CD.Text = string.Empty;
                }
                else
                {
                    this.form.SEIKYUU_TORIHIKI_KBN_CD.Text = this.dto.entryEntity.URIAGE_TORIHIKI_KBN_CD.ToString();
                }
                if (this.dto.entryEntity.URIAGE_ZEI_KEISAN_KBN_CD.IsNull)
                {
                    this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text = string.Empty;
                }
                else
                {
                    this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text = this.dto.entryEntity.URIAGE_ZEI_KEISAN_KBN_CD.ToString();
                }
                if (this.dto.entryEntity.URIAGE_ZEI_KBN_CD.IsNull)
                {
                    this.form.SEIKYUU_ZEI_KBN_CD.Text = string.Empty;
                }
                else
                {
                    this.form.SEIKYUU_ZEI_KBN_CD.Text = this.dto.entryEntity.URIAGE_ZEI_KBN_CD.ToString();
                }

                if (this.dto.entryEntity.SHIHARAI_TORIHIKI_KBN_CD.IsNull)
                {
                    this.form.SHIHARAI_TORIHIKI_KBN_CD.Text = string.Empty;
                }
                else
                {
                    this.form.SHIHARAI_TORIHIKI_KBN_CD.Text = this.dto.entryEntity.SHIHARAI_TORIHIKI_KBN_CD.ToString();
                }
                if (this.dto.entryEntity.SHIHARAI_ZEI_KEISAN_KBN_CD.IsNull)
                {
                    this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = string.Empty;
                }
                else
                {
                    this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = this.dto.entryEntity.SHIHARAI_ZEI_KEISAN_KBN_CD.ToString();
                }
                if (this.dto.entryEntity.SHIHARAI_ZEI_KBN_CD.IsNull)
                {
                    this.form.SHIHARAI_ZEI_KBN_CD.Text = string.Empty;
                }
                else
                {
                    this.form.SHIHARAI_ZEI_KBN_CD.Text = this.dto.entryEntity.SHIHARAI_ZEI_KBN_CD.ToString();
                }
                this.SettingsAfterDisplayData();
                // 排他制御用
                this.form.ENTRY_TIME_STAMP.Text = ConvertStrByte.ByteToString(this.dto.entryEntity.TIME_STAMP);
                // 詳細 End


                // 明細 Start
                // テンプレートをいじる処理は、データ設定前に実行
                this.ExecuteAlignmentForDetail();

                this.form.gcMultiRow1.BeginEdit(false);
                this.form.gcMultiRow1.Rows.Clear();
                // CreateDataTableForEntityがMultiRowを動的に作成しないため、ここでEntity分行数を追加する
                // Entity数Rowを作ると最終行が無いので、Entity + 1でループさせる
                for (int i = 1; i < this.dto.detailEntity.Length + 1; i++)
                {
                    this.form.gcMultiRow1.Rows.Add();
                }
                var dataBinder = new DataBinderLogic<T_KEIRYOU_DETAIL>(this.dto.detailEntity);
                dataBinder.CreateDataTableForEntity(this.form.gcMultiRow1, this.dto.detailEntity);

                // MultiRowへ設定
                int k = 0;
                foreach (var row in this.form.gcMultiRow1.Rows)
                {
                    short denpyouCd = 0;
                    ICustomControl denpyouCdCell = (ICustomControl)row.Cells[CELL_NAME_DENPYOU_KBN_CD];
                    if (short.TryParse(denpyouCdCell.GetResultText(), out denpyouCd)
                        && denpyouKbnDictionary.ContainsKey(denpyouCd))
                    {
                        row.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = denpyouKbnDictionary[denpyouCd].DENPYOU_KBN_NAME_RYAKU;
                    }

                    ICustomControl youkiCdCell = (ICustomControl)row.Cells[CELL_NAME_YOUKI_CD];
                    if (!string.IsNullOrEmpty(youkiCdCell.GetResultText())
                        && youkiDictionary.ContainsKey(youkiCdCell.GetResultText()))
                    {
                        row.Cells[CELL_NAME_YOUKI_NAME_RYAKU].Value = youkiDictionary[youkiCdCell.GetResultText()].YOUKI_NAME_RYAKU;
                    }

                    short unitCd = 0;
                    ICustomControl unitCdCell = (ICustomControl)row.Cells[CELL_NAME_UNIT_CD];
                    if (short.TryParse(unitCdCell.GetResultText(), out unitCd)
                        && unitDictionary.ContainsKey(unitCd))
                    {
                        row.Cells[CELL_NAME_UNIT_NAME_RYAKU].Value = unitDictionary[unitCd].UNIT_NAME_RYAKU;
                    }

                    if (k < this.dto.detailEntity.Length)
                    {
                        T_KEIRYOU_DETAIL detail = this.dto.detailEntity[k];
                        // 金額
                        decimal kintaku = 0;
                        decimal hinmeiKingaku = 0;
                        decimal.TryParse(Convert.ToString(detail.KINGAKU), out kintaku);
                        decimal.TryParse(Convert.ToString(detail.HINMEI_KINGAKU), out hinmeiKingaku);
                        row.Cells[CELL_NAME_KINGAKU].Value = kintaku + hinmeiKingaku;

                    }

                    // 単位kgの品名数量設定
                    if (!SetHinmeiSuuryou(LogicClass.CELL_NAME_UNIT_CD, row, false))
                    {
                        throw new Exception("");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_STAK_JYUURYOU].Value))
                        && !string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value)))
                    {
                        row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].ReadOnly = false;
                        row.Cells[CELL_NAME_CHOUSEI_PERCENT].ReadOnly = false;
                    }
                    else
                    {
                        row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].ReadOnly = true;
                        row.Cells[CELL_NAME_CHOUSEI_PERCENT].ReadOnly = true;
                    }
                    row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].UpdateBackColor(false);
                    row.Cells[CELL_NAME_CHOUSEI_PERCENT].UpdateBackColor(false);

                    if (k < this.dto.detailEntity.Length && !this.dto.detailEntity[k].KEIRYOU_TIME.IsNull)
                    {
                        row.Cells[CELL_NAME_KEIRYOU_TIME].Value = this.dto.detailEntity[k].KEIRYOU_TIME.Value.ToString("HH:mm");
                    }
                    else
                    {
                        row.Cells[CELL_NAME_KEIRYOU_TIME].Value = string.Empty;
                    }

                    if (!row.Cells[CELL_NAME_TANKA].Visible)
                    {
                        row.Cells[CELL_NAME_TANKA].Value = string.Empty;
                    }
                    if (!row.Cells[CELL_NAME_KINGAKU].Visible)
                    {
                        row.Cells[CELL_NAME_KINGAKU].Value = string.Empty;
                    }

                    k++;
                }

                this.form.gcMultiRow1.EndEdit();
                this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
                SelectionActions.MoveToFirstCell.Execute(this.form.gcMultiRow1);

                // 明細 End
                if (this.form.ismobile_mode && this.form.selectDenshuKbnCd == DENSHU_KBN.UKEIRE)
                {
                    //受入実績 start
                    //作業日付
                    if (!this.dto.JentryEntity.SAGYOU_DATE.IsNull
                        && !string.IsNullOrEmpty(this.dto.JentryEntity.SAGYOU_DATE.ToString()))
                    {
                        this.form.SAGYOU_DATE.Text = this.dto.JentryEntity.SAGYOU_DATE.ToString();
                    }
                    //時刻
                    if (!string.IsNullOrEmpty(this.dto.JentryEntity.SAGYOU_TIME))
                    {
                        this.form.SAGYOU_HOUR.Text = (DateTime.Parse(this.dto.JentryEntity.SAGYOU_TIME.ToString()).Hour).ToString();
                        this.form.SAGYOU_MINUTE.Text = (DateTime.Parse(this.dto.JentryEntity.SAGYOU_TIME.ToString()).Minute).ToString();
                    }
                    //作業者
                    this.form.SAGYOUSHA_CD.Text = this.dto.JentryEntity.SAGYOUSHA_CD;
                    this.form.SAGYOUSHA_NAME.Text = this.dto.JentryEntity.SAGYOUSHA_NAME;
                    //作業時備考
                    this.form.SAGYOU_BIKOU.Text = this.dto.JentryEntity.SAGYOU_BIKOU;
                    // 排他制御用
                    if (!this.dto.JentryEntity.DENPYOU_SYSTEM_ID.IsNull)
                    {
                        this.form.JISSEKI_TIME_STAMP.Text = ConvertStrByte.ByteToString(this.dto.JentryEntity.TIME_STAMP);
                    }

                    //受入実績明細
                    this.form.gcMultiRow2.BeginEdit(false);
                    this.form.gcMultiRow2.Rows.Clear();
                    // CreateDataTableForEntityがMultiRowを動的に作成しないため、ここでEntity分行数を追加する
                    // Entity数Rowを作ると最終行が無いので、Entity + 1でループさせる
                    for (int i = 1; i < this.dto.JdetailEntity.Length + 1; i++)
                    {
                        this.form.gcMultiRow2.Rows.Add();
                    }
                    var dataBinder2 = new DataBinderLogic<T_UKEIRE_JISSEKI_DETAIL>(this.dto.JdetailEntity);
                    dataBinder2.CreateDataTableForEntity(this.form.gcMultiRow2, this.dto.JdetailEntity);

                    foreach (var row in this.form.gcMultiRow2.Rows)
                    {
                        ICustomControl hinmeiCdCell = (ICustomControl)row.Cells[CELL_NAME_HINMEI_CD];
                        if (!string.IsNullOrEmpty(hinmeiCdCell.GetResultText())
                            && hinmeiDictionary.ContainsKey(hinmeiCdCell.GetResultText()))
                        {
                            row.Cells[CELL_NAME_HINMEI_NAME].Value = hinmeiDictionary[hinmeiCdCell.GetResultText()].HINMEI_NAME_RYAKU;
                        }
                    }

                    this.CalcTotalValues2();

                    this.form.gcMultiRow2.EndEdit();
                    this.form.gcMultiRow2.NotifyCurrentCellDirty(false);
                    SelectionActions.MoveToFirstCell.Execute(this.form.gcMultiRow2);

                    // ファイルアップロードオプションがONの場合
                    if (this.form.isfile_upload)
                    {
                        // 添付ファイルタブに設定する。
                        if (this.dto.fileDataList.Count() > 0)
                        {
                            this.form.dgvTenpuFileDetail.Rows.Clear();

                            for (int i = 0; i < this.dto.fileDataList.Count(); i++)
                            {
                                if (this.dto.fileDataList[i].FILE_ID.IsNull)
                                {
                                    continue;
                                }

                                this.form.dgvTenpuFileDetail.Rows.Add();
                                this.form.dgvTenpuFileDetail.Rows[i].Cells["TENPU_FILE_NAME"].Value = this.dto.fileDataList[i].FILE_PATH;
                                this.form.dgvTenpuFileDetail.Rows[i].Cells["HIDDEN_FILEID"].Value = this.dto.fileDataList[i].FILE_ID;
                            }
                        }
                    }
                }

            }
            else
            {
                // 新規モードの初期化処理
                this.ChangePropertyForGC(new string[] { "" },
                    new string[] { "CHOUSEI_JYUURYOU", "CHOUSEI_PERCENT" },
                    "Readonly", true);

            }

            if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType) && this.form.KeiryouNumber != -1 && !this.form.TairyuuNewFlg || this.form.isCopy)
            {
                // 複写モード（新規モード、計量番号あり）の初期化処理
                // 計量番号
                this.form.ENTRY_NUMBER.Text = "";
                // 日付連番
                this.form.RENBAN.Text = "";
                // 日付系初期値設定
                this.form.DENPYOU_DATE.Value = this.footerForm.sysDate;
                // 取引先チェック及びＤＴＯセット
                string torihikisakiNmae = this.dto.entryEntity.TORIHIKISAKI_NAME;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiNmae;
            }

            this.CheckTorihikisakiShokuchi();
            this.CheckGyoushaShokuchi();
            this.CheckGenbaShokuchi();
            this.CheckNioroshiGyoushaShokuchi();
            this.CheckNioroshiGenbaShokuchi();
            this.CheckNizumiGyoushaShokuchi();
            this.CheckNizumiGenbaShokuchi();
            this.CheckUpanGyoushaShokuchi();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 重量取込処理
        /// </summary>
        public bool SetJyuuryou(bool WeightDisplaySwitch)
        {
            // 取込ファイルの作成or更新を行う。
            var jyuryoTorikomiUtil = new JyuryoTorikomiUtility();
            jyuryoTorikomiUtil.MakeTorikomiFile();

            if (this.headerForm.KIHON_KEIRYOU.Text == "1")
            {
                return SetJyuuryouForUkeire(WeightDisplaySwitch);
            }
            else
            {
                return SetJyuuryouForShukka(WeightDisplaySwitch);
            }
        }
        /// <summary>
        /// 重量取込処理
        /// </summary>
        public bool SetJyuuryouForUkeire(bool WeightDisplaySwitch)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 重量取込処理中にグローバル変数が変わる可能性があるのでローカル変数に待避
                string beforCtrlName = this.form.beforControlName;

                if ((!this.headerForm.label1.Visible && WeightDisplaySwitch)
                    || string.IsNullOrEmpty(this.headerForm.label1.Text))
                {
                    return true;
                }

                if (this.form.gcMultiRow1 == null
                    || this.form.gcMultiRow1.Rows == null
                    || this.form.gcMultiRow1.RowCount < 1)
                {
                    return true;
                }

                string emptyJyuuryouOfPreviousRow = string.Empty;
                this.form.gcMultiRow1.Focus();
                this.form.gcMultiRow1.BeginEdit(false);

                bool catchErr = true;
                if (string.IsNullOrEmpty(Convert.ToString(this.form.gcMultiRow1.Rows[0].Cells["STACK_JYUURYOU"].Value)))
                {
                    this.form.STACK_JYUURYOU.Text = this.headerForm.label1.Text.Replace(",", "");
                    this.form.STACK_KEIRYOU_TIME.Text = this.GetDate(out catchErr);
                }

                // フォーカス位置を保持
                var seveRowFocus = 0;
                var seveCellFocus = 0;
                foreach (var cell in this.form.gcMultiRow1.SelectedCells)
                {
                    seveRowFocus = cell.RowIndex;
                    seveCellFocus = cell.CellIndex;

                    if (this.form.gcMultiRow1[seveRowFocus, seveCellFocus].Visible && this.form.gcMultiRow1[seveRowFocus, seveCellFocus].Selectable)
                    {
                        break;
                    }
                }

                if (this.form == null || this.form.IsDisposed || this.form.gcMultiRow1 == null)
                {
                    // [F1]と[F9]を同時に押したとき、既にformが消えているのに以降の処理が実行されるため
                    // formの生存確認を実行。
                    return true;
                }

                //フォーカスを品名CDにいったん退避する
                this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(this.form.gcMultiRow1.CurrentRow.Index, CELL_NAME_HINMEI_CD);

                if (this.form.KeizokuKeiryouFlg)
                {
                    Row row = null;

                    // 滞留登録一覧で継続計量を設定して修正モードで開いた場合
                    // 継続計量でも、初回の一度しかここを通さない

                    // 最終行を取得
                    for (int i = 0; i < this.form.gcMultiRow1.RowCount; i++)
                    {
                        row = this.form.gcMultiRow1.Rows[i];

                        // Rowsの後ろからチェック
                        if (row.IsNewRow)
                        {
                            this.form.gcMultiRow1.Rows.Add();
                            // indexがずれるので再取得
                            row = this.form.gcMultiRow1.Rows[i];
                        }

                        // 最後の空車重量がない場合
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_STAK_JYUURYOU].Value))
                            && string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value)))
                        {
                            row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value = this.headerForm.label1.Text.Replace(",", "");
                            row.Cells[CELL_NAME_KEIRYOU_TIME].Value = this.GetDate(out catchErr);
                            this.ChangeTenyuuryoku(row, false);
                            break;
                        }
                        else
                        {
                            // 総重・空車・正味のない行の場合
                            if (string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_STAK_JYUURYOU].Value))
                                && string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value))
                                && string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_NET_JYUURYOU].Value))
                                )
                            {
                                // 全て空白の場合はそのままその行に登録
                            }
                            else
                            {
                                this.form.gcMultiRow1.Rows.Add();
                                row = this.form.gcMultiRow1.Rows[i + 1];    // indexがずれるので再取得
                            }

                            row.Cells[CELL_NAME_STAK_JYUURYOU].Value = this.headerForm.label1.Text.Replace(",", "");
                            break;
                        }
                    }

                    if (row != null)
                    {
                        Row targetRow = this.form.gcMultiRow1.Rows[row.Index];

                        this.CalcStackOrEmptyJyuuryou(targetRow);
                        if (!this.CalcDetaiKingaku(row))
                        {
                            ret = false;
                            return ret;
                        }
                    }

                    this.form.KeizokuKeiryouFlg = true;

                }
                else
                {
                    for (int i = 0; i < this.form.gcMultiRow1.RowCount; i++)
                    {
                        Row row = this.form.gcMultiRow1.Rows[i];

                        if (row.IsNewRow)
                        {
                            this.form.gcMultiRow1.Rows.Add();
                            // indexがずれるので再取得
                            row = this.form.gcMultiRow1.Rows[i];
                        }

                        // 総重量、空車重量のどちらにも値有り
                        if (!string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_EMPTY_JYUURYOU].Value)))
                        {
                            // 次行の総重量にセットするための変数
                            emptyJyuuryouOfPreviousRow = row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value.ToString();
                        }

                        // RowNo再振り
                        if (!this.NumberingRowNo())
                        {
                            return false;
                        }

                        // 総重量、空車重量の両方が無い場合、
                        if (string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_STAK_JYUURYOU].Value))
                            && string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_EMPTY_JYUURYOU].Value)))
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_NET_JYUURYOU].Value)))
                            {
                                continue;
                            }

                            if (string.IsNullOrEmpty(emptyJyuuryouOfPreviousRow))
                            {
                                // 重量値取り込み
                                row.Cells[CELL_NAME_STAK_JYUURYOU].Value = this.headerForm.label1.Text.Replace(",", "");
                            }
                            else
                            {
                                // 重量値取り込み
                                row.Cells[CELL_NAME_STAK_JYUURYOU].Value = emptyJyuuryouOfPreviousRow;
                                row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value = this.headerForm.label1.Text.Replace(",", "");
                                row.Cells[CELL_NAME_KEIRYOU_TIME].Value = this.GetDate(out catchErr);
                                this.ChangeTenyuuryoku(row, false);
                            }
                            // 正味重量、金額計算
                            Row targetRow = this.form.gcMultiRow1.Rows[row.Index];
                            this.CalcStackOrEmptyJyuuryou(targetRow);
                            if (!this.CalcDetaiKingaku(row))
                            {
                                ret = false;
                                return ret;
                            }
                            break;
                        }

                        // 総重量のみ値有り
                        if (!string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_STAK_JYUURYOU].Value))
                            && string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_EMPTY_JYUURYOU].Value)))
                        {
                            row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value = this.headerForm.label1.Text.Replace(",", "");
                            row.Cells[CELL_NAME_KEIRYOU_TIME].Value = this.GetDate(out catchErr);

                            // 正味重量、金額計算
                            Row targetRow = this.form.gcMultiRow1.Rows[row.Index];
                            this.CalcStackOrEmptyJyuuryou(targetRow);
                            if (!this.CalcDetaiKingaku(row))
                            {
                                ret = false;
                                return ret;
                            }
                            this.ChangeTenyuuryoku(row, false);
                            break;
                        }

                        // 空車重量のみ値有り
                        if (string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_STAK_JYUURYOU].Value))
                            && !string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_EMPTY_JYUURYOU].Value)))
                        {
                            continue;
                        }
                    }
                }

                this.form.gcMultiRow1.EndEdit();
                this.form.gcMultiRow1.NotifyCurrentCellDirty(false);

                if (beforCtrlName == this.form.gcMultiRow1.Name)
                {
                    // 次の項目へフォーカスを移動
                    if (this.form.gcMultiRow1[seveRowFocus, seveCellFocus].Visible)
                    {
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(seveRowFocus, seveCellFocus);
                    }
                }
                else if (this.form.Contains(this.form.Controls[beforCtrlName]))
                {
                    // Formの処理前のコントロールにフォーカスを戻す
                    if (!string.IsNullOrEmpty(beforCtrlName))
                    {
                        this.form.Controls[beforCtrlName].Focus();
                    }
                }
                else if (this.headerForm.Contains(this.headerForm.Controls[beforCtrlName]))
                {
                    // HeaderFormの処理前のコントロールにフォーカスを戻す
                    if (!string.IsNullOrEmpty(beforCtrlName))
                    {
                        this.headerForm.Controls[beforCtrlName].Focus();
                    }
                }

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetJyuuryou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetJyuuryou", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 重量取込処理
        /// </summary>
        public bool SetJyuuryouForShukka(bool WeightDisplaySwitch)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 重量取込処理中にグローバル変数が変わる可能性があるのでローカル変数に待避
                string beforCtrlName = this.form.beforControlName;

                if ((!this.headerForm.label1.Visible && WeightDisplaySwitch)
                    || string.IsNullOrEmpty(this.headerForm.label1.Text))
                {
                    return true;
                }

                if (this.form.gcMultiRow1 == null
                    || this.form.gcMultiRow1.Rows == null
                    || this.form.gcMultiRow1.RowCount < 1)
                {
                    return true;
                }

                string emptyJyuuryouOfPreviousRow = string.Empty;
                this.form.gcMultiRow1.Focus();
                this.form.gcMultiRow1.BeginEdit(false);

                bool catchErr = true;
                if (string.IsNullOrEmpty(Convert.ToString(this.form.gcMultiRow1.Rows[0].Cells[CELL_NAME_EMPTY_JYUURYOU].Value))
                    && string.IsNullOrEmpty(this.form.EMPTY_JYUURYOU.Text))
                {
                    this.form.EMPTY_JYUURYOU.Text = this.headerForm.label1.Text.Replace(",", "");
                    this.form.EMPTY_KEIRYOU_TIME.Text = this.GetDate(out catchErr);
                }
                else if (string.IsNullOrEmpty(Convert.ToString(this.form.gcMultiRow1.Rows[0].Cells[CELL_NAME_EMPTY_JYUURYOU].Value)))
                {
                    this.form.EMPTY_KEIRYOU_TIME.Text = this.GetDate(out catchErr);
                }

                // フォーカス位置を保持
                var seveRowFocus = 0;
                var seveCellFocus = 0;
                foreach (var cell in this.form.gcMultiRow1.SelectedCells)
                {
                    seveRowFocus = cell.RowIndex;
                    seveCellFocus = cell.CellIndex;

                    if (this.form.gcMultiRow1[seveRowFocus, seveCellFocus].Visible && this.form.gcMultiRow1[seveRowFocus, seveCellFocus].Selectable)
                    {
                        break;
                    }
                }

                if (this.form == null || this.form.IsDisposed || this.form.gcMultiRow1 == null)
                {
                    // [F1]と[F9]を同時に押したとき、既にformが消えているのに以降の処理が実行されるため
                    // formの生存確認を実行。
                    return true;
                }

                // フォーカスを品名CDにいったん退避する
                this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(this.form.gcMultiRow1.CurrentRow.Index, CELL_NAME_HINMEI_CD);

                if (this.form.KeizokuKeiryouFlg)
                {
                    Row row = null;

                    // 最終行を取得
                    for (int i = 0; i < this.form.gcMultiRow1.RowCount; i++)
                    {
                        row = this.form.gcMultiRow1.Rows[i];

                        // Rowsの後ろからチェック
                        if (row.IsNewRow)
                        {
                            this.form.gcMultiRow1.Rows.Add();
                            // indexがずれるので再取得
                            row = this.form.gcMultiRow1.Rows[i];
                        }

                        // 最後の空車重量がない場合
                        if (string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_STAK_JYUURYOU].Value))
                            && !string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value)))
                        {
                            row.Cells[CELL_NAME_STAK_JYUURYOU].Value = this.headerForm.label1.Text.Replace(",", "");
                            row.Cells[CELL_NAME_KEIRYOU_TIME].Value = this.GetDate(out catchErr);
                            this.ChangeTenyuuryoku(row, false);
                            break;
                        }
                        else
                        {
                            // 総重・空車・正味・割振のない行の場合
                            if (string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_STAK_JYUURYOU].Value))
                                && string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value))
                                && string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_NET_JYUURYOU].Value))
                                )
                            {
                                // 全て空白の場合はそのままその行に登録
                            }
                            else
                            {
                                this.form.gcMultiRow1.Rows.Add();
                                row = this.form.gcMultiRow1.Rows[i + 1];    // indexがずれるので再取得
                            }

                            row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value = this.headerForm.label1.Text.Replace(",", "");
                            break;
                        }
                    }

                    if (row != null)
                    {
                        //this.form.gcMultiRow1.CurrentCellPosition = new CellPosition(row.Index, CELL_NAME_MEISAI_BIKOU);
                        Row targetRow = this.form.gcMultiRow1.Rows[row.Index];
                        this.CalcStackOrEmptyJyuuryou(targetRow);
                        if (!this.CalcDetaiKingaku(row))
                        {
                            throw new Exception("");
                        }
                    }

                    this.form.KeizokuKeiryouFlg = true;

                }
                else
                {
                    for (int i = 0; i < this.form.gcMultiRow1.RowCount; i++)
                    {
                        Row row = this.form.gcMultiRow1.Rows[i];

                        if (row.IsNewRow)
                        {
                            this.form.gcMultiRow1.Rows.Add();
                            // indexがずれるので再取得
                            row = this.form.gcMultiRow1.Rows[i];
                        }

                        // 総重量、空車重量のどちらにも値有り
                        if (!string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_STAK_JYUURYOU].Value)))
                        {
                            // 次行の総重量にセットするための変数
                            emptyJyuuryouOfPreviousRow = row.Cells[CELL_NAME_STAK_JYUURYOU].Value.ToString();
                        }

                        // RowNo再振り
                        if (!this.NumberingRowNo())
                        {
                            return false;
                        }

                        // 総重量、空車重量の両方が無い場合、
                        if (string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_STAK_JYUURYOU].Value))
                            && string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_EMPTY_JYUURYOU].Value)))
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_NET_JYUURYOU].Value)))
                            {
                                continue;
                            }

                            if (string.IsNullOrEmpty(emptyJyuuryouOfPreviousRow))
                            {
                                // 重量値取り込み
                                row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value = this.headerForm.label1.Text.Replace(",", "");
                            }
                            else
                            {
                                // 重量値取り込み
                                row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value = emptyJyuuryouOfPreviousRow;
                                row.Cells[CELL_NAME_STAK_JYUURYOU].Value = this.headerForm.label1.Text.Replace(",", "");
                                row.Cells[CELL_NAME_KEIRYOU_TIME].Value = this.GetDate(out catchErr);
                                this.ChangeTenyuuryoku(row, false);
                            }
                            // 正味重量、金額計算
                            Row targetRow = this.form.gcMultiRow1.Rows[row.Index];
                            this.CalcStackOrEmptyJyuuryou(targetRow);
                            if (!this.CalcDetaiKingaku(row))
                            {
                                throw new Exception("");
                            }
                            break;
                        }

                        // 総重量のみ値有り
                        if (string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_STAK_JYUURYOU].Value))
                            && !string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_EMPTY_JYUURYOU].Value)))
                        {
                            row.Cells[CELL_NAME_STAK_JYUURYOU].Value = this.headerForm.label1.Text.Replace(",", "");
                            row.Cells[CELL_NAME_KEIRYOU_TIME].Value = this.GetDate(out catchErr);
                            this.ChangeTenyuuryoku(row, false);
                            // 正味重量、金額計算
                            Row targetRow = this.form.gcMultiRow1.Rows[row.Index];
                            this.CalcStackOrEmptyJyuuryou(targetRow);
                            if (!this.CalcDetaiKingaku(row))
                            {
                                throw new Exception("");
                            }
                            break;
                        }

                        // 空車重量のみ値有り
                        if (!string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_STAK_JYUURYOU].Value))
                            && string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_EMPTY_JYUURYOU].Value)))
                        {
                            continue;
                        }
                    }
                }

                this.form.gcMultiRow1.EndEdit();
                this.form.gcMultiRow1.NotifyCurrentCellDirty(false);

                if (beforCtrlName == this.form.gcMultiRow1.Name)
                {
                    if (this.form.gcMultiRow1[seveRowFocus, seveCellFocus].Visible)
                    {
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(seveRowFocus, seveCellFocus);
                    }
                }
                else if (this.form.Contains(this.form.Controls[beforCtrlName]))
                {
                    // Formの処理前のコントロールにフォーカスを戻す
                    if (!string.IsNullOrEmpty(beforCtrlName))
                    {
                        this.form.Controls[beforCtrlName].Focus();
                    }
                }
                else if (this.headerForm.Contains(this.headerForm.Controls[beforCtrlName]))
                {
                    // HeaderFormの処理前のコントロールにフォーカスを戻す
                    if (!string.IsNullOrEmpty(beforCtrlName))
                    {
                        this.headerForm.Controls[beforCtrlName].Focus();
                    }
                }
                ret = true;

            }
            catch (SQLRuntimeException ex1)
            {
                if (!string.IsNullOrEmpty(ex1.Message))
                {
                    LogUtility.Error("SetJyuuryou", ex1);
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                ret = false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetJyuuryou", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 数値の表示形式初期化（システム設定に基づき）
        /// </summary>
        internal void CalcValueFormatSettingInit()
        {
            LogUtility.DebugMethodStart();

            this.form.gcMultiRow1.SuspendLayout();
            var newTemplate = this.form.gcMultiRow1.Template;
            string FormatSettingValue;

            // 調整割合(%)
            var obj3 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_CHOUSEI_PERCENT });
            FormatSettingValue = SetFormat((int)this.dto.sysInfoEntity.KEIRYOU_CHOUSEI_WARIAI_HASU_CD,
                (short)this.dto.sysInfoEntity.KEIRYOU_CHOUSEI_WARIAI_HASU_KETA);
            foreach (var o in obj3)
            {
                PropertyUtility.SetValue(o, "FormatSetting", "カスタム");
                PropertyUtility.SetValue(o, "CustomFormatSetting", FormatSettingValue);
            }

            // 調整値
            var obj4 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_CHOUSEI_JYUURYOU });
            FormatSettingValue = SetFormat((int)this.dto.sysInfoEntity.KEIRYOU_CHOUSEI_HASU_CD,
                (short)this.dto.sysInfoEntity.KEIRYOU_CHOUSEI_HASU_KETA);
            foreach (var o in obj4)
            {
                PropertyUtility.SetValue(o, "FormatSetting", "カスタム");
                PropertyUtility.SetValue(o, "CustomFormatSetting", FormatSettingValue);
            }

            // 単価
            var obj5 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_TANKA });
            int TankaFormatCd = (int)this.dto.sysInfoEntity.SYS_TANKA_FORMAT_CD;
            foreach (var o in obj5)
            {
                if ((SysTankaFormatCd)TankaFormatCd == SysTankaFormatCd.BLANK || (SysTankaFormatCd)TankaFormatCd == SysTankaFormatCd.NONE)
                {
                    PropertyUtility.SetValue(o, "CharacterLimitList", new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '-' });
                }
                else
                {
                    PropertyUtility.SetValue(o, "CharacterLimitList", new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '.', '-' });
                }
            }

            // 数量
            var obj6 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_SUURYOU });
            int SuuryouFormatCd = (int)this.dto.sysInfoEntity.SYS_SUURYOU_FORMAT_CD;
            foreach (var o in obj6)
            {
                if ((SysSuuryouFormatCd)SuuryouFormatCd == SysSuuryouFormatCd.BLANK || (SysSuuryouFormatCd)SuuryouFormatCd == SysSuuryouFormatCd.NONE)
                {
                    PropertyUtility.SetValue(o, "CharacterLimitList", new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '-' });
                }
                else
                {
                    PropertyUtility.SetValue(o, "CharacterLimitList", new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '.', '-' });
                }
            }

            this.form.gcMultiRow1.Template = newTemplate;
            this.form.gcMultiRow1.ResumeLayout();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 小数点以下の端数の表示形式を編集
        /// </summary>
        /// <param name="calcCD"></param>
        /// <param name="hasuKeta"></param>
        /// <returns></returns>
        internal string SetFormat(int calcCD, short hasuKeta)
        {
            LogUtility.DebugMethodStart(calcCD, hasuKeta);
            string returnValue = "#,##0";
            int hasuKetaExe = hasuKeta - 2;

            if (hasuKetaExe > 0
                && ((fractionType)calcCD == fractionType.CEILING || (fractionType)calcCD == fractionType.FLOOR || (fractionType)calcCD == fractionType.ROUND))
            {
                returnValue = returnValue + ".";
                if (hasuKetaExe > 1)
                {
                    for (int i = 1; i <= hasuKetaExe - 1; ++i)
                    {
                        returnValue = returnValue + "0";
                    }
                }
                returnValue = returnValue + "0";  // No.3443
            }

            LogUtility.DebugMethodEnd();
            return returnValue;
        }
        #endregion

        #region 業務処理

        /// <summary>
        /// Entity作成と登録処理
        /// </summary>
        /// <param name="taiyuuKbn">滞留登録区分</param>
        /// <param name="errorFlag"></param>
        /// <returns>true:成功, false:失敗</returns>
        public bool CreateEntityAndUpdateTables(bool taiyuuKbn, bool errorFlag, out bool catchErr)
        {
            catchErr = true;
            try
            {
                var keiryouExist = false;

                if (keiryouExist == false)
                {
                    this.TaiyuuKbn = taiyuuKbn;

                    // 削除モード時はモードを変更すると削除できなくなるのでモードを変更しない
                    if (WINDOW_TYPE.DELETE_WINDOW_FLAG != this.form.WindowType && this.form.TairyuuNewFlg == true)
                    {
                        // 滞留一覧からの新規データはUPDATEに戻す
                        this.form.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    }

                    // CreateEntityとそれぞれの更新処理でDB更新が発生するため、UIFormから
                    // 排他制御する
                    using (Transaction tran = new Transaction())
                    {
                        switch (this.form.WindowType)
                        {
                            case WINDOW_TYPE.NEW_WINDOW_FLAG:
                                // 計量系
                                this.CreateEntity(taiyuuKbn);
                                this.Regist(errorFlag);

                                // キャッシャ連動「1.する」の場合
                                if (this.form.CASHEIR_RENDOU_KBN_CD.Text == CommonConst.CASHER_LINK_KBN_USE)
                                {
                                    // キャッシャ情報送信
                                    this.SendCasher();
                                }
                                break;

                            case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                                this.CreateEntity(taiyuuKbn);
                                this.Update(errorFlag);

                                // キャッシャ連動「1.する」かつ滞留登録の場合
                                if (this.form.TairyuuNewFlg && this.form.CASHEIR_RENDOU_KBN_CD.Text == CommonConst.CASHER_LINK_KBN_USE)
                                {
                                    // キャッシャ情報送信
                                    this.SendCasher();
                                }
                                break;

                            case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                                this.CreateEntity(taiyuuKbn);
                                this.LogicalDelete();
                                break;

                            default:
                                break;
                        }
                        // コミット
                        tran.Commit();
                    }
                }
                else
                {
                    // 重複している場合はエラー表示を行う
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShowError("該当の計量データに変更があります。\n再度入力し直してください。");
                    return false;
                }

                return true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("CreateEntityAndUpdateTables", ex1);
                this.msgLogic.MessageBoxShow("E080", "");
                catchErr = false;
                return false;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CreateEntityAndUpdateTables", ex1);

                var causeNo = ((SqlException)ex1.InnerException).Number;

                // 一意エラーの場合
                if (causeNo == 2627)
                {
                    msgLogic.MessageBoxShow("E080", "");
                    catchErr = false;
                    return false;
                }

                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityAndUpdateTables", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                return false;
            }

        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        public virtual void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            this.dto.entryEntity.DELETE_FLG = true;
            this.dto.entryEntity.UPDATE_DATE = this.beforDto.entryEntity.UPDATE_DATE;
            this.dto.entryEntity.UPDATE_PC = this.beforDto.entryEntity.UPDATE_PC;
            this.dto.entryEntity.UPDATE_USER = this.beforDto.entryEntity.UPDATE_USER;
            this.accessor.UpdateKeiryouEntry(this.dto.entryEntity);

            this.dto.entryEntity.DELETE_FLG = true;
            this.dto.entryEntity.SEQ = this.dto.entryEntity.SEQ + 1;
            this.dto.entryEntity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
            this.dto.entryEntity.UPDATE_PC = SystemInformation.ComputerName;
            this.dto.entryEntity.UPDATE_USER = SystemProperty.UserName;
            this.accessor.InsertKeiryouEntry(this.dto.entryEntity);

            for (int row = 0; row < this.dto.detailEntity.Length; row++)
            {
                this.dto.detailEntity[row].SEQ = this.dto.entryEntity.SEQ;
            }
            this.accessor.InsertKeiryouDetail(this.dto.detailEntity);

            //受入実績は、[受入]のみ
            if (this.form.ismobile_mode && this.form.selectDenshuKbnCd == DENSHU_KBN.UKEIRE)
            {
                if (this.beforDto.JdetailEntity.Length > 0)
                {
                    this.dto.JentryEntity.TIME_STAMP = ConvertStrByte.StringToByte(this.form.JISSEKI_TIME_STAMP.Text);
                    this.dto.JentryEntity.SEQ = this.dto.JentryEntity.SEQ;
                    this.dto.JentryEntity.DELETE_FLG = true;
                    this.dto.JentryEntity.UPDATE_DATE = this.beforDto.JentryEntity.UPDATE_DATE;
                    this.dto.JentryEntity.UPDATE_PC = this.beforDto.JentryEntity.UPDATE_PC;
                    this.dto.JentryEntity.UPDATE_USER = this.beforDto.JentryEntity.UPDATE_USER;
                    this.accessor.UpdateUkeireJissekiEntry(this.dto.JentryEntity);

                    this.dto.JentryEntity.SEQ = this.dto.JentryEntity.SEQ + 1;
                    this.dto.JentryEntity.DELETE_FLG = true;
                    this.dto.JentryEntity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    this.dto.JentryEntity.UPDATE_PC = SystemInformation.ComputerName;
                    this.dto.JentryEntity.UPDATE_USER = SystemProperty.UserName;
                    this.accessor.InsertUkeireJissekiEntry(this.dto.JentryEntity);

                    for (int row = 0; row < this.dto.JdetailEntity.Length; row++)
                    {
                        this.dto.JdetailEntity[row].SEQ = this.dto.JentryEntity.SEQ;
                    }
                    this.accessor.InsertUkeireJissekiDetail(this.dto.JdetailEntity);

                    if (this.form.isfile_upload)
                    {
                        // ファイルデータとファイル連携データを削除する。
                        var list = this.fileLinkUJEDao.GetDataByCd(short.Parse(this.beforDto.JdetailEntity[0].DENPYOU_SHURUI.ToString())
                                                                , this.beforDto.JdetailEntity[0].DENPYOU_SYSTEM_ID.ToString());
                        if (list != null && 0 < list.Count)
                        {
                            // ファイルデータを物理削除する。
                            var fileIdList = list.Select(n => n.FILE_ID.Value).ToList();
                            this.uploadLogic.DeleteFileData(fileIdList);

                            // ファイル連携データを削除する。
                            string sql = string.Format("DELETE FROM M_FILE_LINK_UKEIRE_JISSEKI_ENTRY WHERE DENPYOU_SHURUI = {0} AND DENPYOU_SYSTEM_ID = {1}"
                                , this.beforDto.JdetailEntity[0].DENPYOU_SHURUI.ToString(), this.beforDto.JdetailEntity[0].DENPYOU_SYSTEM_ID.ToString());
                            this.fileLinkUJEDao.GetDateForStringSql(sql);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        /// <param name="taiyuuKbn"></param>
        public virtual void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            this.accessor.InsertKeiryouEntry(this.dto.entryEntity);
            this.accessor.InsertKeiryouDetail(this.dto.detailEntity);

            //受入実績は、[受入]かつ、明細ありの時だけ作成
            if (this.form.ismobile_mode && this.form.selectDenshuKbnCd == DENSHU_KBN.UKEIRE)
            {
                if (this.dto.JdetailEntity.Length > 0)
                {
                    this.accessor.InsertUkeireJissekiEntry(this.dto.JentryEntity);
                    this.accessor.InsertUkeireJissekiDetail(this.dto.JdetailEntity);
                }
            }
            if (this.hiRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT))
            {
                this.accessor.InsertNumberDay(this.dto.numberDay);
            }
            else if (this.hiRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE))
            {
                this.accessor.UpdateNumberDay(this.dto.numberDay);
            }
            if (this.nenRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT))
            {
                this.accessor.InsertNumberYear(this.dto.numberYear);
            }
            else if (this.nenRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE))
            {
                this.accessor.UpdateNumberYear(this.dto.numberYear);
            }

            // S_NUMBER_RECEIPTの更新
            if (this.dto.numberReceipt != null
                && this.dto.numberReceipt.TIME_STAMP != null)
            {
                this.accessor.UpdateNumberReceipt(this.dto.numberReceipt);
            }
            else if (this.dto.numberReceipt != null
                && this.dto.numberReceipt.TIME_STAMP == null)
            {
                this.accessor.InsertNumberReceipt(this.dto.numberReceipt);
            }

            // S_NUMBER_RECEIPT_YEARの更新
            if (this.dto.numberReceiptYear != null
                && this.dto.numberReceiptYear.TIME_STAMP != null)
            {
                this.accessor.UpdateNumberReceiptYear(this.dto.numberReceiptYear);
            }
            else if (this.dto.numberReceiptYear != null
                && this.dto.numberReceiptYear.TIME_STAMP == null)
            {
                this.accessor.InsertNumberReceiptYear(this.dto.numberReceiptYear);
            }

            LogUtility.DebugMethodEnd(errorFlag);
        }

        /// <summary>
        /// 検索(使用しない)
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        /// <param name="taiyuuKbn"></param>
        public virtual void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            this.accessor.InsertKeiryouEntry(this.dto.entryEntity);
            this.accessor.UpdateKeiryouEntry(this.beforDto.entryEntity);
            this.accessor.InsertKeiryouDetail(this.dto.detailEntity);

            //受入実績は、[受入]で作成
            if (this.form.ismobile_mode && this.form.selectDenshuKbnCd == DENSHU_KBN.UKEIRE)
            {
                //既存明細データなし/登録する明細データ無し
                if (this.beforDto.JdetailEntity.Length <= 0 && this.dto.JdetailEntity.Length <= 0)
                {
                    //受入実績は作成しない
                }
                //既存明細データあり/登録する明細データ無し
                else if (this.beforDto.JdetailEntity.Length > 0 && this.dto.JdetailEntity.Length <= 0)
                {
                    //論理削除
                    this.dto.JentryEntity.TIME_STAMP = ConvertStrByte.StringToByte(this.form.JISSEKI_TIME_STAMP.Text);
                    this.dto.JentryEntity.SEQ = this.dto.JentryEntity.SEQ;
                    this.dto.JentryEntity.DELETE_FLG = true;
                    this.dto.JentryEntity.UPDATE_DATE = this.beforDto.JentryEntity.UPDATE_DATE;
                    this.dto.JentryEntity.UPDATE_PC = this.beforDto.JentryEntity.UPDATE_PC;
                    this.dto.JentryEntity.UPDATE_USER = this.beforDto.JentryEntity.UPDATE_USER;
                    this.accessor.UpdateUkeireJissekiEntry(this.dto.JentryEntity);

                    this.dto.JentryEntity.SEQ = this.dto.JentryEntity.SEQ + 1;
                    this.dto.JentryEntity.DELETE_FLG = true;
                    this.dto.JentryEntity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    this.dto.JentryEntity.UPDATE_PC = SystemInformation.ComputerName;
                    this.dto.JentryEntity.UPDATE_USER = SystemProperty.UserName;
                    this.accessor.InsertUkeireJissekiEntry(this.dto.JentryEntity);

                    for (int row = 0; row < this.beforDto.JdetailEntity.Length; row++)
                    {
                        this.beforDto.JdetailEntity[row].SEQ = this.dto.JentryEntity.SEQ;
                    }
                    this.accessor.InsertUkeireJissekiDetail(this.beforDto.JdetailEntity);
                }
                else
                {
                    //登録するデータあり
                    if (this.beforDto.JdetailEntity.Length > 0)
                    {
                        //既存明細データあり、伝票をDELETE_FLGを立てて更新する
                        this.beforDto.JentryEntity.DELETE_FLG = true;
                        this.accessor.UpdateUkeireJissekiEntry(this.beforDto.JentryEntity);
                    }
                    this.accessor.InsertUkeireJissekiEntry(this.dto.JentryEntity);
                    this.accessor.InsertUkeireJissekiDetail(this.dto.JdetailEntity);
                }
            }

            if (this.hiRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT))
            {
                this.accessor.InsertNumberDay(this.dto.numberDay);
            }
            else if (this.hiRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE))
            {
                this.accessor.UpdateNumberDay(this.dto.numberDay);
            }
            if (this.nenRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT))
            {
                this.accessor.InsertNumberYear(this.dto.numberYear);
            }
            else if (this.nenRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE))
            {
                this.accessor.UpdateNumberYear(this.dto.numberYear);
            }

            // S_NUMBER_RECEIPTの更新
            if (this.dto.numberReceipt != null
                && this.dto.numberReceipt.TIME_STAMP != null)
            {
                this.accessor.UpdateNumberReceipt(this.dto.numberReceipt);
            }
            else if (this.dto.numberReceipt != null
                && this.dto.numberReceipt.TIME_STAMP == null)
            {
                this.accessor.InsertNumberReceipt(this.dto.numberReceipt);
            }

            // S_NUMBER_RECEIPT_YEARの更新
            if (this.dto.numberReceiptYear != null
                && this.dto.numberReceiptYear.TIME_STAMP != null)
            {
                this.accessor.UpdateNumberReceiptYear(this.dto.numberReceiptYear);
            }
            else if (this.dto.numberReceiptYear != null
                && this.dto.numberReceiptYear.TIME_STAMP == null)
            {
                this.accessor.InsertNumberReceiptYear(this.dto.numberReceiptYear);
            }
            LogUtility.DebugMethodEnd(errorFlag);
        }

        /// <summary>
        /// MultiRowのデータに対しROW_NOを採番します
        /// </summary>
        public bool NumberingRowNo()
        {
            bool ret = false;
            try
            {

                if (!this.form.notEditingOperationFlg)
                {
                    this.form.gcMultiRow1.BeginEdit(false);
                }

                foreach (Row dr in this.form.gcMultiRow1.Rows)
                {
                    dr.Cells[CELL_NAME_ROW_NO].Value = dr.Index + 1;
                }

                if (!this.form.notEditingOperationFlg)
                {
                    this.form.gcMultiRow1.EndEdit();
                    this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
                }
                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("NumberingRowNo", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("NumberingRowNo", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 総重量、空車重量の状態によって、調整の入力制限を変更する
        /// </summary>
        internal bool ChangeChouseiInputStatus()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                var targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return true;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value))
                    && !string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value)))
                {
                    this.ChangeTenyuuryoku(targetRow, false);
                }
                else
                {
                    this.ChangeTenyuuryoku(targetRow, true);
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChangeChouseiInputStatus", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeChouseiInputStatus", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 手入力変更処理
        /// </summary>
        /// <param name="tenyuuryokuFlag">ture: 手入力可, false:手入力不可</param>
        internal void ChangeTenyuuryoku(Row targetRow, bool isReadOnly)
        {
            LogUtility.DebugMethodStart(targetRow, isReadOnly);

            if (targetRow == null)
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            this.form.gcMultiRow1.BeginEdit(false);

            /**
             * 手入力可能とする
             */
            targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].ReadOnly = isReadOnly;
            targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].ReadOnly = isReadOnly;

            targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].UpdateBackColor(false);
            targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].UpdateBackColor(false);

            this.form.gcMultiRow1.EndEdit();
            this.form.gcMultiRow1.NotifyCurrentCellDirty(false);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 明細の金額、重量計算
        /// 金額、重量計算をまとめて処理します
        /// </summary>
        internal bool CalcDetail()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 調整kg(片方だけ計算すればいいはず)
                if (!this.CalcChouseiJyuuryou())
                {
                    return false;
                }

                // 容器重量
                if (!this.CalcYoukiJyuuryou())
                {
                    return false;
                }

                // 合計系金額計算
                if (!this.CalcTotalValues())
                {
                    return false;
                }

                ret = true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("CalcDetail", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 総重量または空車重量計算
        /// </summary>
        internal void CalcStackOrEmptyJyuuryou()
        {
            LogUtility.DebugMethodStart();

            Row targetRow = this.form.gcMultiRow1.CurrentRow;

            if (targetRow == null)
            {
                return;
            }

            this.form.gcMultiRow1.BeginEdit(false);

            decimal stackJyuuryou = 0;
            decimal emptyJyuuryou = 0;
            decimal chouseiJyuuryou = 0;
            decimal youkiJyuuryou = 0;

            decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stackJyuuryou);
            decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou);
            decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value), out chouseiJyuuryou);
            decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou);

            // 総重量・空車重量のどちらか片方でも入力されていなければ
            // 正味重量に値をセットしない
            if (targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value != null
                && targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value != null)
            {
                targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = stackJyuuryou - emptyJyuuryou - chouseiJyuuryou - youkiJyuuryou;
            }
            else
            {
                targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = null;
            }

            if (Convert.ToString(targetRow.Cells[CELL_NAME_UNIT_CD].Value) == "3")
            {
                targetRow.Cells[CELL_NAME_SUURYOU].Value = targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value;
            }

            this.form.gcMultiRow1.EndEdit();
            this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 総重量または空車重量計算
        /// </summary>
        internal void CalcStackOrEmptyJyuuryou(Row targetRow)
        {
            LogUtility.DebugMethodStart();

            if (targetRow == null)
            {
                return;
            }

            this.form.gcMultiRow1.BeginEdit(false);

            decimal stackJyuuryou = 0;
            decimal emptyJyuuryou = 0;
            decimal chouseiJyuuryou = 0;
            decimal youkiJyuuryou = 0;

            decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stackJyuuryou);
            decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou);
            decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value), out chouseiJyuuryou);
            decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou);

            // 総重量・空車重量のどちらか片方でも入力されていなければ
            // 正味重量に値をセットしない
            if (targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value != null
                && targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value != null)
            {
                targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = stackJyuuryou - emptyJyuuryou - chouseiJyuuryou - youkiJyuuryou;
            }
            else
            {
                targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = null;
            }

            targetRow.Cells[CELL_NAME_SUURYOU].ReadOnly = false;

            if (Convert.ToString(targetRow.Cells[CELL_NAME_UNIT_CD].Value) == "3")
            {
                targetRow.Cells[CELL_NAME_SUURYOU].Value = targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value;
                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_SUURYOU].Value)))
                {
                    // 単位：Kgの場合は読み取り専用
                    targetRow.Cells[CELL_NAME_SUURYOU].ReadOnly = true;
                }
            }
            else
            {
                if (Convert.ToString(targetRow.Cells[CELL_NAME_UNIT_CD].Value) == "1"
                    && targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value != null)
                {
                    decimal ton = (stackJyuuryou - emptyJyuuryou - chouseiJyuuryou - youkiJyuuryou) / 1000;
                    // 単位tの場合は正味重量/1000＝数量とする
                    targetRow.Cells[CELL_NAME_SUURYOU].Value = ton;
                }
            }

            // ReadOnlyを変更するとBackColorが変わらない場合がある
            var cell = targetRow.Cells[CELL_NAME_SUURYOU] as ICustomAutoChangeBackColor;
            cell.UpdateBackColor();

            this.form.gcMultiRow1.EndEdit();
            this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 合計系の計算
        /// </summary>
        internal bool CalcTotalValues()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                decimal netTotal = 0;
                decimal uriageKingakuTotal = 0;
                decimal shiharaiKingakuTotal = 0;
                foreach (Row dr in this.form.gcMultiRow1.Rows)
                {
                    decimal kingaku = 0;
                    decimal netJyuuryou = 0;

                    decimal.TryParse(Convert.ToString(dr.Cells[CELL_NAME_KINGAKU].EditedFormattedValue), out kingaku);
                    decimal.TryParse(Convert.ToString(dr.Cells[CELL_NAME_NET_JYUURYOU].Value), out netJyuuryou);

                    // 正味重量計算
                    netTotal += netJyuuryou;

                    // 売上金額合計、支払金額合計計算
                    switch (Convert.ToString(dr.Cells[CELL_NAME_DENPYOU_KBN_CD].Value))
                    {
                        case SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE_STR:
                            uriageKingakuTotal += kingaku;
                            break;

                        case SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI_STR:
                            shiharaiKingakuTotal += kingaku;
                            break;

                        default:
                            break;
                    }
                }
                this.form.NET_TOTAL.Text = netTotal.ToString("N");
                CustomTextBoxLogic customTextBoxLogic = new CustomTextBoxLogic(this.form.NET_TOTAL);
                customTextBoxLogic.Format(this.form.NET_TOTAL);
                this.form.URIAGE_KINGAKU_TOTAL.Text = uriageKingakuTotal.ToString();
                this.form.SHIHARAI_KINGAKU_TOTAL.Text = shiharaiKingakuTotal.ToString();

                // 差額計算
                this.CalcSagaku();

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcTotalValues", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcTotalValues", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 差額計算
        /// </summary>
        internal void CalcSagaku()
        {
            LogUtility.DebugMethodStart();

            decimal uriageTotal = 0;
            decimal shiharaiTotal = 0;

            decimal.TryParse(Convert.ToString(this.form.URIAGE_KINGAKU_TOTAL.Text), out uriageTotal);
            decimal.TryParse(Convert.ToString(this.form.SHIHARAI_KINGAKU_TOTAL.Text), out shiharaiTotal);

            if (this.dto.sysInfoEntity.UKEIRE_CALC_BASE_KBN == SalesPaymentConstans.UKEIRE_CALC_BASE_KBN_URIAGE)
            {
                this.form.SAGAKU.Text = Convert.ToString(uriageTotal - shiharaiTotal);
            }
            else if (this.dto.sysInfoEntity.UKEIRE_CALC_BASE_KBN == SalesPaymentConstans.UKEIRE_CALC_BASE_KBN_SHIHARAI)
            {
                this.form.SAGAKU.Text = Convert.ToString(shiharaiTotal - uriageTotal);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 調整Kg更新後の計算
        /// </summary>
        internal bool CalcChouseiJyuuryou()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return true;
                }

                this.form.gcMultiRow1.BeginEdit(false);
                var criterionNet = this.GetCriterionNetForCurrent();    // 基準正味
                if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value)))
                {
                    // 紐付くデータの削除
                    targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value = string.Empty;
                    if (criterionNet != null)
                    {
                        targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = criterionNet;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value))
                        && !string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value)))
                    {
                        decimal netJyuuryou = 0;
                        decimal youkiJyuuryou = 0;
                        var netTryPaseResult = decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value), out netJyuuryou);
                        var youkiTryPaseResult = decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou);
                        if (!netTryPaseResult && !youkiTryPaseResult)
                        {
                            this.form.gcMultiRow1.EndEdit();
                            this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
                            return true;
                        }

                        targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = (netJyuuryou - youkiJyuuryou);
                    }
                }
                else
                {
                    decimal chouseiJyuuryou = 0;  // 調整kg
                    decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value), out chouseiJyuuryou);

                    // 調整%計算
                    decimal youkiJyuuryou = 0;    // 容器重量
                    decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou);
                    decimal chouseiPercent = 0;
                    if (decimal.TryParse(Convert.ToString((chouseiJyuuryou / (criterionNet - youkiJyuuryou)) * 100), out chouseiPercent))
                    {
                        chouseiPercent = (CommonCalc.FractionCalc((decimal)(chouseiJyuuryou / (criterionNet - youkiJyuuryou)) * 100, (int)this.dto.sysInfoEntity.KEIRYOU_CHOUSEI_WARIAI_HASU_CD, (short)this.dto.sysInfoEntity.KEIRYOU_CHOUSEI_WARIAI_HASU_KETA));
                        targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value = chouseiPercent;

                        // 正味重量計算
                        targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = criterionNet - chouseiJyuuryou - youkiJyuuryou;
                    }

                }

                this.form.gcMultiRow1.EndEdit();
                this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcChouseiJyuuryou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcChouseiJyuuryou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 調整%更新後の計算
        /// </summary>
        internal bool CalcChouseiPercent()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return true;
                }

                this.form.gcMultiRow1.BeginEdit(false);

                var criterionNet = this.GetCriterionNetForCurrent();    // 基準正味
                if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value)))
                {
                    // 紐付くデータの削除
                    targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value = string.Empty;
                    if (criterionNet != null)
                    {
                        targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = criterionNet;
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value))
                        && !string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value)))
                    {
                        decimal netJyuuryou = 0;
                        decimal youkiJyuuryou = 0;
                        decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value), out netJyuuryou);
                        decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou);

                        targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = (netJyuuryou - youkiJyuuryou);
                    }
                }
                else
                {
                    decimal youkiJyuuryou = 0;    // 容器重量
                    decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou);
                    decimal chouseiPercent = 0;  // 調整%
                    decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value), out chouseiPercent);
                    decimal criterionNetCalcResult = (decimal)((criterionNet - youkiJyuuryou) * (chouseiPercent / 100));
                    criterionNetCalcResult = CommonCalc.FractionCalc(criterionNetCalcResult, (int)this.dto.sysInfoEntity.KEIRYOU_CHOUSEI_HASU_CD, (short)this.dto.sysInfoEntity.KEIRYOU_CHOUSEI_HASU_KETA);

                    targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value = criterionNetCalcResult;

                    // 正味重量計算
                    targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = (decimal)criterionNet - criterionNetCalcResult - (decimal)youkiJyuuryou;

                }
                this.form.gcMultiRow1.EndEdit();
                this.form.gcMultiRow1.NotifyCurrentCellDirty(false);

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcChouseiPercent", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcChouseiPercent", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 容器欄初期化
        /// </summary>
        internal bool InitYoukiItem()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();
                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return true;
                }

                string youkiCd = Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_CD].Value);
                if (string.IsNullOrEmpty(youkiCd))
                {
                    targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value = string.Empty;
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("InitYoukiItem", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("InitYoukiItem", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 容器数量更新後計算
        /// </summary>
        internal bool CalcYoukiSuuryou()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();
                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return true; ;
                }

                M_YOUKI youki = this.accessor.GetYouki((Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_CD].Value)));

                // 容器数量用処理
                decimal youkiJyuryou = 0;     // 容器重量(容器)
                decimal youkiSuuryou = 0;     // 容器数量(計量明細)

                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_SUURYOU].Value), out youkiSuuryou);

                if (!decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_SUURYOU].Value), out youkiSuuryou))
                {
                    targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value = null;
                }
                else if (youki != null)
                {
                    decimal tempJyuryou = 0;
                    decimal.TryParse(Convert.ToString(youki.YOUKI_JYURYO), out tempJyuryou);
                    youkiJyuryou = tempJyuryou;

                    // 容器重量設定(計量明細)
                    // 容器重量を計算
                    decimal youkiJyuuryouForCell = youkiJyuryou * youkiSuuryou;   // 正味重量の計算があるため変数に設定
                    targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value = youkiJyuuryouForCell;
                }

                if (!this.CalcYoukiJyuuryou())
                {
                    return false;
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcYoukiSuuryou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcYoukiSuuryou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 容器重量更新後計算
        /// </summary>
        internal bool CalcYoukiJyuuryou()
        {
            bool ret = false;

            try
            {
                LogUtility.DebugMethodStart();
                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return true;
                }

                // 容器重量用処理
                decimal youkiJyuryou = 0;     // 容器重量(容器)
                decimal stakJyuuryou = 0;
                decimal emptyJyuuryou = 0;
                decimal netJyuuryou = 0;

                if (
                    !decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuryou)
                    && !decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stakJyuuryou)
                    && !decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou)
                    && !decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value), out netJyuuryou)
                    )
                {
                    return true;
                }

                if (0 <= stakJyuuryou && 0 <= emptyJyuuryou)
                {
                    var criterionNet = this.GetCriterionNetForCurrent();    // 基準正味
                    decimal chouseiJyuuryou = 0;
                    decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value), out chouseiJyuuryou);
                    // 総重量・空車重量のどちらか片方でも入力されていなければ
                    // 正味重量に値をセットしない
                    if (targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value != null
                        && targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value != null)
                    {
                        targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = criterionNet - chouseiJyuuryou - youkiJyuryou;
                    }
                    else if (targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value != null)
                    {
                        targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = criterionNet - chouseiJyuuryou - youkiJyuryou;
                    }
                    else
                    {
                        targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = null;
                    }
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcYoukiJyuuryou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcYoukiJyuuryou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 調整kg入力値チェック
        /// </summary>
        /// <returns>true: 問題なし, false:問題あり</returns>
        internal bool ValidateChouseiJyuuryou(out bool catchErr)
        {
            bool returnVal = false;
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart();

                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return returnVal;
                }

                decimal stackJyuuryou = 0;
                decimal emptyJyuuryou = 0;
                decimal chouseiJyuuryou = 0;

                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value), out chouseiJyuuryou))
                {
                    if (chouseiJyuuryou == 0)
                    {
                        CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, CELL_NAME_CHOUSEI_JYUURYOU);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", "1以上");
                        return returnVal;
                    }
                }
                else if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value)))
                {
                    // Null or 空は許容しているのでスルー
                    returnVal = true;
                    return returnVal;
                }

                decimal youkiJyuuryou = 0;
                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou);
                /**
                 * 総重量-空車重量の値　又は　割振Kgが入力されている場合
                 */
                if ((decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stackJyuuryou)
                    && decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou)))
                {
                    // 全部値がある場合にだけチェック
                    if (0 == (stackJyuuryou - emptyJyuuryou - youkiJyuuryou))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E082", "総重量、空車重量、容器重量");
                        targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value = string.Empty;
                        return returnVal;
                    }
                    else if ((stackJyuuryou - emptyJyuuryou - youkiJyuuryou) <= chouseiJyuuryou)
                    {
                        CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, CELL_NAME_CHOUSEI_JYUURYOU);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", (stackJyuuryou - emptyJyuuryou - youkiJyuuryou).ToString() + "未満");
                        return returnVal;
                    }
                }
                returnVal = true;

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ValidateChouseiJyuuryou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                returnVal = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateChouseiJyuuryou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                returnVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }

        /// <summary>
        /// 調整(%)入力値チェック
        /// </summary>
        /// <returns>true: 問題なし, false:問題あり</returns>
        internal bool ValidateChouseiPercent(out bool catchErr)
        {
            catchErr = true;
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart();

                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return returnVal;
                }

                if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value)))
                {
                    return true;
                }

                decimal stackJyuuryou = 0;
                decimal emptyJyuuryou = 0;
                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stackJyuuryou)
                    && decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou))
                {
                    if (0 == (stackJyuuryou - emptyJyuuryou))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E082", "総重量、空車重量");
                        targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value = string.Empty;
                        return returnVal;
                    }
                }

                decimal chouseiPercent = 0;

                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value), out chouseiPercent))
                {
                    if (100 <= chouseiPercent)
                    {
                        CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, CELL_NAME_CHOUSEI_PERCENT);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", "100未満");
                        return returnVal;
                    }

                    if (chouseiPercent == 0)
                    {
                        CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, CELL_NAME_CHOUSEI_PERCENT);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", "1以上");
                        return returnVal;
                    }
                }

                returnVal = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ValidateChouseiPercent", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateChouseiPercent", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }

        /// <summary>
        /// 品名入力値チェック
        /// </summary>
        /// <returns>true: 問題なし, false:問題あり</returns>
        internal bool ValidateHinmeiName(out bool catchErr)
        {
            catchErr = true;
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();


                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return returnVal;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value)))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_NAME].EditedFormattedValue)))
                    {
                        CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, CELL_NAME_HINMEI_NAME);

                        var cell = targetRow.Cells[CELL_NAME_HINMEI_NAME] as ICustomAutoChangeBackColor;
                        cell.IsInputErrorOccured = true;
                        cell.UpdateBackColor();

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E012", "品名");
                        return returnVal;
                    }
                }

                returnVal = true;

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ValidateHinmeiName", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateHinmeiName", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }

        /// <summary>
        /// 重量値のフォーマットチェック
        /// </summary>
        /// <param name="targetRow">対象のRow</param>
        /// <param name="cellName">対象のCell名</param>
        /// <returns></returns>
        internal bool ValidateJyuryouFormat(Row targetRow, string cellName, out bool catchErr)
        {
            bool returnVal = false;
            catchErr = true;
            try
            {
                if (targetRow == null)
                {
                    return returnVal;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[cellName].EditedFormattedValue)))
                {
                    decimal tempStackJyuuryou = 0;
                    if (!decimal.TryParse(targetRow.Cells[cellName].EditedFormattedValue.ToString(), out tempStackJyuuryou))
                    {
                        CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                        this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, cellName);

                        var cell = targetRow.Cells[cellName] as ICustomAutoChangeBackColor;
                        cell.IsInputErrorOccured = true;
                        cell.UpdateBackColor();

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E084", targetRow.Cells[cellName].EditedFormattedValue.ToString());
                        return returnVal;
                    }
                }

                returnVal = true;

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ValidateJyuryouFormat", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateJyuryouFormat", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;

        }

        /// <summary>
        /// 単位CD検索&設定
        /// </summary>
        /// <param name="hinmeiChangedFlg">品名CDが更新された後の処理かどうか</param>
        internal bool SearchAndCalcForUnit(bool hinmeiChangedFlg, Row targetRow)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(hinmeiChangedFlg, targetRow);

                if (targetRow == null)
                {
                    return true;
                }

                // 単価前回値取得
                var oldTanka = targetRow.Cells[CELL_NAME_TANKA].Value == null ? string.Empty : targetRow.Cells[CELL_NAME_TANKA].Value.ToString();

                M_UNIT targetUnit = null;

                if (hinmeiChangedFlg)
                {
                    M_HINMEI[] hinmeis = null;
                    // 品名CD更新時
                    if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value)))
                    {
                        return true;
                    }
                    else
                    {
                        hinmeis = this.accessor.GetAllValidHinmeiData(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value));
                    }

                    if (hinmeis == null || hinmeis.Count() < 1)
                    {
                        return true;
                    }

                    M_HINMEI hinmei = hinmeis[0];

                    M_UNIT[] units = null;
                    short UnitCd = 0;
                    if (short.TryParse(hinmei.UNIT_CD.ToString(), out UnitCd))
                        units = this.accessor.GetUnit(UnitCd);

                    if (units == null)
                    {
                        return true;
                    }
                    else
                    {
                        targetUnit = units[0];
                    }

                    if (string.IsNullOrEmpty(targetUnit.UNIT_NAME))
                    {
                        return true;
                    }

                    targetRow.Cells[CELL_NAME_UNIT_CD].Value = targetUnit.UNIT_CD.ToString();
                    targetRow.Cells[CELL_NAME_UNIT_NAME_RYAKU].Value = targetUnit.UNIT_NAME_RYAKU;
                }
                else
                {
                    // 単位CD更新時
                }

                /**
                 * 数量設定
                 */
                if (!this.CalcSuuryou(targetRow))     // 正味重量が変更になるタイミングあるため数量計算をメソッド化
                {
                    return false;
                }

                short denpyouKbn = 0;
                short unitCd = 0;
                if (!short.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value), out denpyouKbn)
                    || !short.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_UNIT_CD].Value), out unitCd))
                {
                    return true;
                }

                if (targetRow.Cells[CELL_NAME_TANKA].Visible)
                {
                    /**
                     * 単価設定
                     */
                    var kobetsuhinmeiTanka = this.commonAccesser.GetKobetsuhinmeiTanka(
                        (short)this.form.selectDenshuKbnCd,
                        Convert.ToInt16(targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value),
                        this.form.TORIHIKISAKI_CD.Text,
                        this.form.GYOUSHA_CD.Text,
                        this.form.GENBA_CD.Text,
                        this.form.UNPAN_GYOUSHA_CD.Text,
                        this.form.NIOROSHI_GYOUSHA_CD.Text,
                        this.form.NIOROSHI_GENBA_CD.Text,
                        Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value),
                        Convert.ToInt16(targetRow.Cells[CELL_NAME_UNIT_CD].Value),
                        this.form.DENPYOU_DATE.Text
                        );

                    // 個別品名単価から情報が取れない場合は基本品名単価の検索
                    if (kobetsuhinmeiTanka == null)
                    {
                        var kihonHinmeiTanka = this.commonAccesser.GetKihonHinmeitanka(
                            (short)this.form.selectDenshuKbnCd,
                            Convert.ToInt16(targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value),
                            this.form.UNPAN_GYOUSHA_CD.Text,
                            this.form.NIOROSHI_GYOUSHA_CD.Text,
                            this.form.NIOROSHI_GENBA_CD.Text,
                            Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value),
                            Convert.ToInt16(targetRow.Cells[CELL_NAME_UNIT_CD].Value),
                            this.form.DENPYOU_DATE.Text
                            );
                        if (kihonHinmeiTanka != null)
                        {
                            targetRow.Cells[CELL_NAME_TANKA].Value = kihonHinmeiTanka.TANKA.Value;
                        }
                        else
                        {
                            targetRow.Cells[CELL_NAME_TANKA].Value = string.Empty;
                        }
                    }
                    else
                    {
                        targetRow.Cells[CELL_NAME_TANKA].Value = kobetsuhinmeiTanka.TANKA.Value;
                    }
                }
                else
                {
                    targetRow.Cells[CELL_NAME_TANKA].Value = string.Empty;
                    targetRow.Cells[CELL_NAME_KINGAKU].Value = string.Empty;
                }

                /**
                 * 金額設定
                 */
                var newTanka = targetRow.Cells[CELL_NAME_TANKA].Value == null ? string.Empty : targetRow.Cells[CELL_NAME_TANKA].Value.ToString();

                // 単価に変更があった場合のみ再計算
                if (!oldTanka.Equals(newTanka))
                {
                    if (!this.CalcDetaiKingaku(targetRow))
                    {
                        return false;
                    }
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SearchAndCalcForUnit", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchAndCalcForUnit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 数量計算
        /// </summary>
        internal bool CalcSuuryou(Row targetRow)
        {
            bool ret = false;

            try
            {
                LogUtility.DebugMethodStart(targetRow);

                if (targetRow == null)
                {
                    return true;
                }

                if (targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value == null
                        || string.IsNullOrEmpty(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value.ToString())
                        || targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value == null
                        || string.IsNullOrEmpty(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value.ToString()))
                {
                    return true;
                }

                /**
                 * 数量設定
                 */
                if (string.Compare(SalesPaymentConstans.UNIT_CD_KG,
                    Convert.ToString(targetRow.Cells[CELL_NAME_UNIT_CD].Value), true) == 0)
                {
                    if (!this.IsRegist && !this.IsSuuryouKesannFlg)
                    {
                        targetRow.Cells[CELL_NAME_SUURYOU].Value = targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value;
                    }
                }
                // No.2275
                else if (string.Compare(SalesPaymentConstans.UNIT_CD_TON,
                    Convert.ToString(targetRow.Cells[CELL_NAME_UNIT_CD].Value), true) == 0)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value)))
                    {
                        if (!this.IsRegist && !this.IsSuuryouKesannFlg)
                        {
                            decimal kg = Convert.ToDecimal(targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value);
                            decimal ton = kg / 1000;
                            targetRow.Cells[CELL_NAME_SUURYOU].Value = ton;
                        }
                    }
                }
                targetRow.Cells[CELL_NAME_SUURYOU].UpdateBackColor(false);

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcSuuryou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcSuuryou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 明細金額計算
        /// </summary>
        internal bool CalcDetaiKingaku(Row targetRow)
        {
            /* 登録実行時に金額計算のチェック(CheckDetailKingakuメソッド)が実行されます。 　　         */
            /* チェックの計算方法は本メソッドと同じため、修正する際はチェック処理も修正してください。 */
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(targetRow);

                if (targetRow == null)
                {
                    return true;
                }

                if (this.form.IsDataLoading)
                {
                    return true;
                }

                if (!targetRow.Cells[CELL_NAME_KINGAKU].Visible)
                {
                    targetRow.Cells[CELL_NAME_KINGAKU].Value = string.Empty;
                    return true;
                }

                decimal suuryou = 0;
                decimal tanka = 0;
                // 金額端数の初期値は四捨五入としておく
                short kingakuHasuuCd = 3;

                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_SUURYOU].FormattedValue), out suuryou);
                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_TANKA].FormattedValue), out tanka);

                // 金額端数取得
                if (targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value != null)
                {
                    if (targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString().Equals("1"))
                    {
                        if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                        {
                            short.TryParse(Convert.ToString(this.dto.torihikisakiSeikyuuEntity.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                        }
                        else
                        {
                            short.TryParse(Convert.ToString(this.dto.sysInfoEntity.SEIKYUU_KINGAKU_HASUU_CD), out kingakuHasuuCd);
                        }
                    }
                    else if (targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString().Equals("2"))
                    {
                        if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                        {
                            short.TryParse(Convert.ToString(this.dto.torihikisakiShiharaiEntity.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                        }
                        else
                        {
                            short.TryParse(Convert.ToString(this.dto.sysInfoEntity.SHIHARAI_KINGAKU_HASUU_CD), out kingakuHasuuCd);
                        }
                    }
                }

                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_SUURYOU].FormattedValue), out suuryou)
                    && decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_TANKA].FormattedValue), out tanka))
                {
                    decimal kingaku = CommonCalc.FractionCalc(suuryou * tanka, kingakuHasuuCd);

                    /* 桁が10桁以上になる場合は9桁で表示する。ただし、結果としては違算なので、登録時金額チェックではこの処理は行わずエラーとしている */
                    if (kingaku.ToString().Length > 9)
                    {
                        kingaku = Convert.ToDecimal(kingaku.ToString().Substring(0, 9));
                    }

                    targetRow.Cells[CELL_NAME_KINGAKU].Value = kingaku;
                }
                else
                {
                    targetRow.Cells[CELL_NAME_KINGAKU].Value = null;
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcDetaiKingaku", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcDetaiKingaku", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;

        }

        /// <summary>
        /// Logic内で定義されているEntityすべての最新情報を取得する
        /// </summary>
        /// <returns>true:正常値、false:エラー発生</returns>
        public bool GetAllEntityData(out bool catchErr)
        {
            catchErr = true;
            try
            {
                if (this.form.ismobile_mode)
                {
                    _tabPageManager.ChangeTabPageVisible(1, true);
                }
                // 更新前データを保持しておく
                this.beforDto = new DTOClass();

                // 画面のモードに依存しないデータの取得
                this.dto.sysInfoEntity = CommonShogunData.SYS_INFO;

                // システム設定が設定されていない場合を想定しデフォルト値を設定
                this.SetSysInfoDefaultValue();

                if (!this.IsRequireData())
                {
                    return true;
                }

                // 計量入力
                var entrys = accessor.GetKeiryouEntry(this.form.KeiryouNumber, this.form.SEQ);
                if (entrys == null || entrys.Length < 1)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    return false;
                }
                else
                {
                    this.form.IsDataLoading = true;

                    this.dto.entryEntity = entrys[0];
                }

                // 計量明細
                var details = accessor.GetKeiryouDetail(this.dto.entryEntity.SYSTEM_ID, this.dto.entryEntity.SEQ);
                if (details == null || details.Length < 1)
                {
                    this.dto.detailEntity = new T_KEIRYOU_DETAIL[] { };
                }
                else
                {
                    this.dto.detailEntity = details;
                }


                if (this.form.ismobile_mode)
                {
                    // 受入実績
                    var entrysJ = accessor.GetUkeireJissekiEntry(1, this.dto.entryEntity.SYSTEM_ID);
                    if (entrysJ == null || entrysJ.Length < 1)
                    {
                        this.dto.JentryEntity = new T_UKEIRE_JISSEKI_ENTRY();
                        this.dto.JdetailEntity = new T_UKEIRE_JISSEKI_DETAIL[] { };
                        this.dto.fileLinkUJEList = new M_FILE_LINK_UKEIRE_JISSEKI_ENTRY[] { new M_FILE_LINK_UKEIRE_JISSEKI_ENTRY() };
                        this.dto.fileDataList = new T_FILE_DATA[] { new T_FILE_DATA() };
                    }
                    else
                    {
                        this.dto.JentryEntity = entrysJ[0];

                        // 受入実績明細
                        var detailJ = accessor.GetUkeireJissekiDetail(1, this.dto.entryEntity.SYSTEM_ID, this.dto.JentryEntity.SEQ);
                        if (detailJ == null || detailJ.Length < 1)
                        {
                            this.dto.JdetailEntity = new T_UKEIRE_JISSEKI_DETAIL[] { };
                        }
                        else
                        {
                            this.dto.JdetailEntity = detailJ;
                        }

                        // ファイルアプロードオプションがONの場合
                        if (this.form.isfile_upload)
                        {
                            // ファイルアップロード用DB接続を確立
                            if (!this.uploadLogic.CanConnectDB())
                            {
                                this.msgLogic.MessageBoxShowError("ファイルアップロード用DBに接続できませんでした。\n接続情報を確認してください。");

                                // 処理を行わない
                                return false;
                            }

                            // 添付ファイルデータを取得する。
                            this.GetFileData();
                        }
                    }
                }

                // 取引先請求
                this.dto.torihikisakiSeikyuuEntity = new M_TORIHIKISAKI_SEIKYUU();
                var torihikisakiSeikyuu = this.accessor.GetTorihikisakiSeikyuu(this.dto.entryEntity.TORIHIKISAKI_CD);
                if (torihikisakiSeikyuu != null)
                {
                    this.dto.torihikisakiSeikyuuEntity = torihikisakiSeikyuu;
                }

                // 取引先支払
                this.dto.torihikisakiShiharaiEntity = new M_TORIHIKISAKI_SHIHARAI();
                var torhikisakiShiharai = this.accessor.GetTorihikisakiShiharai(this.dto.entryEntity.TORIHIKISAKI_CD);
                if (torhikisakiShiharai != null)
                {
                    this.dto.torihikisakiShiharaiEntity = torhikisakiShiharai;
                }

                // 形態区分
                this.dto.keitaiKbnEntity = new M_KEITAI_KBN();
                if (!this.dto.entryEntity.KEITAI_KBN_CD.IsNull)
                {
                    var keitaiKbn = this.accessor.GetkeitaiKbn((short)this.dto.entryEntity.KEITAI_KBN_CD, true);
                    if (keitaiKbn != null)
                    {
                        this.dto.keitaiKbnEntity = keitaiKbn;
                    }
                }

                // 拠点
                this.dto.kyotenEntity = new M_KYOTEN();
                if (!this.dto.entryEntity.KYOTEN_CD.IsNull)
                {
                    var kyotens = this.accessor.GetAllDataByCodeForKyoten((short)this.dto.entryEntity.KYOTEN_CD);
                    if (kyotens != null && 0 < kyotens.Length)
                    {
                        this.dto.kyotenEntity = kyotens[0];
                    }
                }

                // マニフェスト
                this.dto.manifestEntrys = this.accessor.GetManifestEntry(this.dto.detailEntity);

                // マニフェスト種類
                this.dto.manifestShuruiEntity = new M_MANIFEST_SHURUI();
                if (!this.dto.entryEntity.MANIFEST_SHURUI_CD.IsNull)
                {
                    var manifestShurui = this.accessor.GetManifestShurui(this.dto.entryEntity.MANIFEST_SHURUI_CD);
                    if (manifestShurui != null)
                    {
                        this.dto.manifestShuruiEntity = manifestShurui;
                    }
                }
                // マニフェスト手配
                this.dto.manifestTehaiEntity = new M_MANIFEST_TEHAI();
                if (!this.dto.entryEntity.MANIFEST_TEHAI_CD.IsNull)
                {
                    var manifestTehai = this.accessor.GetManifestTehai(this.dto.entryEntity.MANIFEST_TEHAI_CD);
                    if (manifestTehai != null)
                    {
                        this.dto.manifestTehaiEntity = manifestTehai;
                    }
                }

                this.beforDto = this.dto.Clone();
                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetAllEntityData", ex1);
                msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetAllEntityData", ex);
                msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// ヘッダーの拠点CDの存在チェック
        /// </summary>
        internal void CheckKyotenCd()
        {
            // 初期化
            this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;

            if (string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
            {
                this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
                return;
            }

            short kyoteCd = -1;
            if (!short.TryParse(string.Format("{0:#,0}", this.headerForm.KYOTEN_CD.Text), out kyoteCd))
            {
                return;
            }

            var kyotens = this.accessor.GetDataByCodeForKyoten(kyoteCd);

            // 存在チェック
            if (kyotens == null || kyotens.Length < 1)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "拠点");
                this.headerForm.KYOTEN_CD.Focus();
                return;
            }
            else
            {
                // キーが１つなので複数はヒットしないはず
                M_KYOTEN kyoten = kyotens[0];
                this.headerForm.KYOTEN_NAME_RYAKU.Text = kyoten.KYOTEN_NAME_RYAKU;
            }
        }

        #region 排出事業者のチェック
        private string hstGyoushaHintText = "全角20文字/半角40文字以内で入力してください";

        /// <summary>
        /// 排出事業者チェック
        /// </summary>
        internal bool CheckHstGyoushaCd(out bool catchErr)
        {
            catchErr = true;
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                var msgLogic = new MessageBoxShowLogic();
                var inputHstGyoushaCd = this.form.HST_GYOUSHA_CD.Text;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(inputHstGyoushaCd)
                    || !this.tmpHstGyoushaCd.Equals(inputHstGyoushaCd))
                    || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.HST_GYOUSHA_NAME.Text = string.Empty;
                    if (!this.tmpHstGyoushaCd.Equals(inputHstGyoushaCd))
                    {
                        this.form.HST_GENBA_CD.Text = string.Empty;
                        this.form.HST_GENBA_NAME.Text = string.Empty;
                    }

                    if (string.IsNullOrEmpty(this.form.HST_GYOUSHA_CD.Text))
                    {
                        this.form.HST_GENBA_CD.Text = string.Empty;
                        this.form.HST_GENBA_NAME.Text = string.Empty;
                    }
                    else
                    {
                        catchErr = true;
                        var retData = this.accessor.GetGyousha(this.form.HST_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                        if (!catchErr)
                        {
                            ret = false;
                            return ret;
                        }
                        var gyousha = retData;
                        if (gyousha != null)
                        {
                            // PKは1つなので複数ヒットしない
                            if (gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue && (gyousha.GYOUSHAKBN_MANI.IsTrue))
                            {
                                // 排出事業者名
                                this.form.HST_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                                // 入力済の排出事業場との関連チェック
                                bool isContinue = false;
                                M_GENBA genba = new M_GENBA();
                                if (!string.IsNullOrEmpty(this.form.HST_GENBA_CD.Text))
                                {
                                    var genbaEntityList = this.accessor.GetGenbaByGyousha(this.form.HST_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                                    if (genbaEntityList != null || genbaEntityList.Length >= 1)
                                    {
                                        foreach (M_GENBA genbaEntity in genbaEntityList)
                                        {
                                            if (this.form.HST_GENBA_CD.Text.Equals(genbaEntity.GENBA_CD)
                                                && (genbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue))
                                            {
                                                isContinue = true;
                                                genba = genbaEntity;
                                                ret = true;
                                                break;
                                            }
                                        }
                                        if (!isContinue)
                                        {
                                            // 一致するものがないので、入力されている現場CDを消す
                                            this.form.HST_GENBA_CD.Text = string.Empty;
                                            this.form.HST_GENBA_NAME.Text = string.Empty;
                                        }
                                        else
                                        {
                                            this.form.HST_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                this.form.isInputError = true;
                                // エラーメッセージ
                                msgLogic.MessageBoxShow("E020", "排出事業者");
                                this.form.HST_GYOUSHA_CD.Focus();
                            }
                        }
                        else
                        {
                            this.form.isInputError = true;
                            msgLogic.MessageBoxShow("E020", "排出事業者");
                            this.form.HST_GYOUSHA_CD.Focus();
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckHstGyoushaCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHstGyoushaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }
        #endregion

        #region 排出事業場のチェック
        private string hstGenbaHintText = "全角20文字/半角40文字以内で入力してください";

        /// <summary>
        /// 排出事業場CDの存在チェック
        /// </summary>
        internal bool CheckHstGenbaCd(out bool catchErr)
        {
            catchErr = true;
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                var msgLogic = new MessageBoxShowLogic();
                var inputHstGenbaCd = this.form.HST_GENBA_CD.Text;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(inputHstGenbaCd)
                    || !this.tmpHstGenbaCd.Equals(inputHstGenbaCd))
                    || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.HST_GENBA_NAME.Text = string.Empty;

                    if (string.IsNullOrEmpty(this.form.HST_GENBA_CD.Text))
                    {
                        this.form.HST_GENBA_NAME.Text = string.Empty;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(this.form.HST_GYOUSHA_CD.Text))
                        {
                            msgLogic.MessageBoxShow("E051", "排出事業場");
                            this.form.HST_GENBA_CD.Text = string.Empty;
                            this.form.HST_GENBA_CD.Focus();
                            ret = false;
                            return ret;
                        }

                        var genbaEntityList = this.accessor.GetGenbaList(this.form.HST_GYOUSHA_CD.Text, this.form.HST_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                        M_GENBA genba = new M_GENBA();

                        if (genbaEntityList == null || genbaEntityList.Length < 1)
                        {
                            this.form.isInputError = true;
                            msgLogic.MessageBoxShow("E020", "排出事業場");
                            this.form.HST_GENBA_CD.Focus();
                        }
                        else
                        {
                            genba = genbaEntityList[0];
                            // 排出事業者名入力チェック
                            if (string.IsNullOrEmpty(this.form.HST_GYOUSHA_CD.Text))
                            {
                                // エラーメッセージ
                                this.form.isInputError = true;
                                msgLogic.MessageBoxShow("E020", "排出事業場");
                                this.form.HST_GENBA_CD.Text = string.Empty;
                                this.form.HST_GENBA_CD.Focus();
                            }
                            // 排出事業者と排出事業場の関連チェック
                            else if (genba == null)
                            {
                                // 一致するデータがないのでエラー
                                this.form.isInputError = true;
                                msgLogic.MessageBoxShow("E020", "排出事業場");
                                this.form.HST_GENBA_CD.Focus();
                            }
                            else
                            {
                                catchErr = true;
                                var retData = this.accessor.GetGyousha(this.form.HST_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                                if (!catchErr)
                                {
                                    ret = false;
                                    return ret;
                                }
                                // 業者設定
                                var gyousha = retData;
                                if (gyousha != null)
                                {
                                    // PKは1つなので複数ヒットしない
                                    if (gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue && (gyousha.GYOUSHAKBN_MANI.IsTrue))
                                    {
                                        // 排出事業者名
                                        this.form.HST_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                                    }
                                }

                                // 事業場区分、現場区分チェック
                                if (genba.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue)
                                {
                                    this.form.HST_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                                }
                                else
                                {
                                    // 一致するデータがないのでエラー
                                    this.form.isInputError = true;
                                    msgLogic.MessageBoxShow("E020", "排出事業場");
                                    this.form.HST_GENBA_CD.Focus();
                                }
                            }
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckHstGenbaCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHstGenbaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }
        #endregion

        #region 処分業者のチェック
        private string sbnGyoushaHintText = "全角20文字/半角40文字以内で入力してください";

        /// <summary>
        /// 処分業者チェック
        /// </summary>
        internal bool CheckSbnGyoushaCd(out bool catchErr)
        {
            catchErr = true;
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                var msgLogic = new MessageBoxShowLogic();
                var inputSbnGyoushaCd = this.form.SBN_GYOUSHA_CD.Text;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(inputSbnGyoushaCd)
                    || !this.tmpSbnGyoushaCd.Equals(inputSbnGyoushaCd)))
                {
                    // 初期化
                    this.form.SBN_GYOUSHA_NAME.Text = string.Empty;
                    if (!this.tmpSbnGyoushaCd.Equals(inputSbnGyoushaCd))
                    {
                        this.form.SBN_GENBA_CD.Text = string.Empty;
                        this.form.SBN_GENBA_NAME.Text = string.Empty;
                    }

                    if (string.IsNullOrEmpty(this.form.SBN_GYOUSHA_CD.Text))
                    {
                        this.form.SBN_GENBA_CD.Text = string.Empty;
                        this.form.SBN_GENBA_NAME.Text = string.Empty;
                    }
                    else
                    {
                        catchErr = true;
                        var retData = this.accessor.GetGyousha(this.form.SBN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                        if (!catchErr)
                        {
                            ret = false;
                            return ret;
                        }
                        var gyousha = retData;
                        if (gyousha != null)
                        {
                            // PKは1つなので複数ヒットしない
                            if (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue && (gyousha.GYOUSHAKBN_MANI.IsTrue))
                            {
                                // 処分業者名
                                this.form.SBN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                                // 入力済の処分事業場との関連チェック
                                bool isContinue = false;
                                M_GENBA genba = new M_GENBA();
                                if (!string.IsNullOrEmpty(this.form.SBN_GENBA_CD.Text))
                                {
                                    var genbaEntityList = this.accessor.GetGenbaByGyousha(this.form.SBN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                                    if (genbaEntityList != null || genbaEntityList.Length >= 1)
                                    {
                                        foreach (M_GENBA genbaEntity in genbaEntityList)
                                        {
                                            if (this.form.SBN_GENBA_CD.Text.Equals(genbaEntity.GENBA_CD)
                                                && (genbaEntity.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue
                                                    || genbaEntity.SAISHUU_SHOBUNJOU_KBN.IsTrue))
                                            {
                                                isContinue = true;
                                                genba = genbaEntity;
                                                ret = true;
                                                break;
                                            }
                                        }
                                        if (!isContinue)
                                        {
                                            // 一致するものがないので、入力されている現場CDを消す
                                            this.form.SBN_GENBA_CD.Text = string.Empty;
                                            this.form.SBN_GENBA_NAME.Text = string.Empty;
                                        }
                                        else
                                        {
                                            // 一致する現場CDがあれば、現場名を再設定する
                                            this.form.SBN_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                this.form.isInputError = true;
                                // エラーメッセージ
                                msgLogic.MessageBoxShow("E020", "処分業者");
                                this.form.SBN_GYOUSHA_CD.Focus();
                            }
                        }
                        else
                        {
                            this.form.isInputError = true;
                            msgLogic.MessageBoxShow("E020", "処分業者");
                            this.form.SBN_GYOUSHA_CD.Focus();
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckSbnGyoushaCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckSbnGyoushaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }
        #endregion

        #region 処分事業場のチェック
        private string sbnGenbaHintText = "全角20文字/半角40文字以内で入力してください";

        /// <summary>
        /// 処分事業場CDの存在チェック
        /// </summary>
        internal bool CheckSbnGenbaCd(out bool catchErr)
        {
            catchErr = true;
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                var msgLogic = new MessageBoxShowLogic();
                var inputSbnGenbaCd = this.form.SBN_GENBA_CD.Text;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(inputSbnGenbaCd)
                    || !this.tmpSbnGenbaCd.Equals(inputSbnGenbaCd))
                    || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.SBN_GENBA_NAME.Text = string.Empty;

                    if (string.IsNullOrEmpty(this.form.SBN_GENBA_CD.Text))
                    {
                        this.form.SBN_GENBA_NAME.Text = string.Empty;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(this.form.SBN_GYOUSHA_CD.Text))
                        {
                            msgLogic.MessageBoxShow("E051", "処分事業場");
                            this.form.SBN_GENBA_CD.Text = string.Empty;
                            this.form.SBN_GENBA_CD.Focus();
                            ret = false;
                            return ret;
                        }

                        var genbaEntityList = this.accessor.GetGenbaList(this.form.SBN_GYOUSHA_CD.Text, this.form.SBN_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                        M_GENBA genba = new M_GENBA();

                        if (genbaEntityList == null || genbaEntityList.Length < 1)
                        {
                            this.form.isInputError = true;
                            msgLogic.MessageBoxShow("E020", "処分事業場");
                            this.form.SBN_GENBA_CD.Focus();
                        }
                        else
                        {
                            genba = genbaEntityList[0];
                            // 処分業者名入力チェック
                            if (string.IsNullOrEmpty(this.form.SBN_GYOUSHA_CD.Text))
                            {
                                // エラーメッセージ
                                this.form.isInputError = true;
                                msgLogic.MessageBoxShow("E020", "処分事業場");
                                this.form.SBN_GENBA_CD.Text = string.Empty;
                                this.form.SBN_GENBA_CD.Focus();
                            }
                            // 処分業者と処分事業場の関連チェック
                            else if (genba == null)
                            {
                                // 一致するデータがないのでエラー
                                this.form.isInputError = true;
                                msgLogic.MessageBoxShow("E020", "処分事業場");
                                this.form.SBN_GENBA_CD.Focus();
                            }
                            else
                            {
                                catchErr = true;
                                var retData = this.accessor.GetGyousha(this.form.SBN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                                if (!catchErr)
                                {
                                    ret = false;
                                    return ret;
                                }
                                // 業者設定
                                var gyousha = retData;
                                if (gyousha != null)
                                {
                                    // PKは1つなので複数ヒットしない
                                    if (gyousha.GYOUSHAKBN_UKEIRE.IsTrue && (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                                    {
                                        this.form.SBN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                                        // 処分業者名
                                        this.form.SBN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                                    }
                                }

                                // 事業場区分、現場区分チェック
                                if (genba.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || genba.SAISHUU_SHOBUNJOU_KBN.IsTrue || genba.TSUMIKAEHOKAN_KBN.IsTrue)
                                {
                                    this.form.SBN_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                                }
                                else
                                {
                                    // 一致するデータがないのでエラー
                                    this.form.isInputError = true;
                                    msgLogic.MessageBoxShow("E020", "処分事業場");
                                    this.form.SBN_GENBA_CD.Focus();
                                }
                            }
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckSbnGenbaCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckSbnGenbaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }
        #endregion

        #region 最終処分業者のチェック
        private string lastSbnGyoushaHintText = "全角20文字/半角40文字以内で入力してください";

        /// <summary>
        /// 最終処分業者チェック
        /// </summary>
        internal bool CheckLastSbnGyoushaCd(out bool catchErr)
        {
            catchErr = true;
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                var msgLogic = new MessageBoxShowLogic();
                var inputLastSbnGyoushaCd = this.form.LAST_SBN_GYOUSHA_CD.Text;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(inputLastSbnGyoushaCd)
                    || !this.tmpLastSbnGyoushaCd.Equals(inputLastSbnGyoushaCd))
                    || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.LAST_SBN_GYOUSHA_NAME.Text = string.Empty;
                    if (!this.tmpLastSbnGyoushaCd.Equals(inputLastSbnGyoushaCd))
                    {
                        this.form.LAST_SBN_GENBA_CD.Text = string.Empty;
                        this.form.LAST_SBN_GENBA_NAME.Text = string.Empty;
                    }

                    if (string.IsNullOrEmpty(this.form.LAST_SBN_GYOUSHA_CD.Text))
                    {
                        this.form.LAST_SBN_GENBA_CD.Text = string.Empty;
                        this.form.LAST_SBN_GENBA_NAME.Text = string.Empty;
                    }
                    else
                    {
                        catchErr = true;
                        var retData = this.accessor.GetGyousha(this.form.LAST_SBN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                        if (!catchErr)
                        {
                            ret = false;
                            return ret;
                        }
                        var gyousha = retData;
                        if (gyousha != null)
                        {
                            // PKは1つなので複数ヒットしない
                            if (gyousha.GYOUSHAKBN_MANI.IsTrue || (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue))
                            {
                                // 最終処分業者名
                                this.form.LAST_SBN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                                // 入力済の最終処分場との関連チェック
                                bool isContinue = false;
                                M_GENBA genba = new M_GENBA();
                                if (!string.IsNullOrEmpty(this.form.LAST_SBN_GENBA_CD.Text))
                                {
                                    var genbaEntityList = this.accessor.GetGenbaByGyousha(this.form.LAST_SBN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                                    if (genbaEntityList != null || genbaEntityList.Length >= 1)
                                    {
                                        foreach (M_GENBA genbaEntity in genbaEntityList)
                                        {
                                            if (this.form.LAST_SBN_GENBA_CD.Text.Equals(genbaEntity.GENBA_CD)
                                                && (genbaEntity.SAISHUU_SHOBUNJOU_KBN.IsTrue))
                                            {
                                                isContinue = true;
                                                genba = genbaEntity;
                                                ret = true;
                                                break;
                                            }
                                        }
                                        if (!isContinue)
                                        {
                                            // 一致するものがないので、入力されている現場CDを消す
                                            this.form.LAST_SBN_GENBA_CD.Text = string.Empty;
                                            this.form.LAST_SBN_GENBA_NAME.Text = string.Empty;
                                        }
                                        else
                                        {
                                            // 一致する現場CDがあれば、現場名を再設定する
                                            this.form.LAST_SBN_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                this.form.isInputError = true;
                                // エラーメッセージ
                                msgLogic.MessageBoxShow("E020", "最終処分業者");
                                this.form.LAST_SBN_GYOUSHA_CD.Focus();
                            }
                        }
                        else
                        {
                            this.form.isInputError = true;
                            msgLogic.MessageBoxShow("E020", "最終処分業者");
                            this.form.LAST_SBN_GYOUSHA_CD.Focus();
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckLastSbnGyoushaCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckLastSbnGyoushaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }
        #endregion

        #region 最終処分場のチェック
        private string lastSbnGenbaHintText = "全角20文字/半角40文字以内で入力してください";

        /// <summary>
        /// 最終処分場CDの存在チェック
        /// </summary>
        internal bool CheckLastSbnGenbaCd(out bool catchErr)
        {
            catchErr = true;
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                var msgLogic = new MessageBoxShowLogic();
                var inputLastSbnGenbaCd = this.form.LAST_SBN_GENBA_CD.Text;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(inputLastSbnGenbaCd)
                    || !this.tmpLastSbnGenbaCd.Equals(inputLastSbnGenbaCd))
                    || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.LAST_SBN_GENBA_NAME.Text = string.Empty;

                    if (string.IsNullOrEmpty(this.form.LAST_SBN_GENBA_CD.Text))
                    {
                        this.form.LAST_SBN_GENBA_NAME.Text = string.Empty;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(this.form.LAST_SBN_GYOUSHA_CD.Text))
                        {
                            msgLogic.MessageBoxShow("E051", "最終処分場");
                            this.form.LAST_SBN_GENBA_CD.Text = string.Empty;
                            this.form.LAST_SBN_GENBA_CD.Focus();
                            ret = false;
                            return ret;
                        }

                        var genbaEntityList = this.accessor.GetGenbaList(this.form.LAST_SBN_GYOUSHA_CD.Text, this.form.LAST_SBN_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                        M_GENBA genba = new M_GENBA();

                        if (genbaEntityList == null || genbaEntityList.Length < 1)
                        {
                            this.form.isInputError = true;
                            msgLogic.MessageBoxShow("E020", "最終処分場");
                            this.form.LAST_SBN_GENBA_CD.Focus();
                        }
                        else
                        {
                            genba = genbaEntityList[0];
                            // 最終処分業者名入力チェック
                            if (string.IsNullOrEmpty(this.form.LAST_SBN_GYOUSHA_CD.Text))
                            {
                                // エラーメッセージ
                                this.form.isInputError = true;
                                msgLogic.MessageBoxShow("E020", "最終処分場");
                                this.form.LAST_SBN_GENBA_CD.Text = string.Empty;
                                this.form.LAST_SBN_GENBA_CD.Focus();
                            }
                            // 最終処分業者と最終処分場の関連チェック
                            else if (genba == null)
                            {
                                // 一致するデータがないのでエラー
                                this.form.isInputError = true;
                                msgLogic.MessageBoxShow("E020", "最終処分場");
                                this.form.LAST_SBN_GENBA_CD.Focus();
                            }
                            else
                            {
                                catchErr = true;
                                var retData = this.accessor.GetGyousha(this.form.LAST_SBN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                                if (!catchErr)
                                {
                                    ret = false;
                                    return ret;
                                }
                                // 業者設定
                                var gyousha = retData;
                                if (gyousha != null)
                                {
                                    // PKは1つなので複数ヒットしない
                                    if (gyousha.GYOUSHAKBN_MANI.IsTrue && (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue))
                                    {
                                        // 最終処分業者名
                                        this.form.LAST_SBN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                                    }
                                }

                                // 事業場区分、現場区分チェック
                                if (genba.SAISHUU_SHOBUNJOU_KBN.IsTrue)
                                {
                                    this.form.LAST_SBN_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                                }
                                else
                                {
                                    // 一致するデータがないのでエラー
                                    this.form.isInputError = true;
                                    msgLogic.MessageBoxShow("E020", "最終処分場");
                                    this.form.LAST_SBN_GENBA_CD.Focus();
                                }
                            }
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckLastSbnGenbaCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckLastSbnGenbaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }
        #endregion

        #region 荷降業者チェック
        private string nioroshiGyoushaHintText = "全角20文字/半角40文字以内で入力してください";

        /// <summary>
        /// 荷降業者チェック
        /// </summary>
        internal bool CheckNioroshiGyoushaCd(out bool catchErr)
        {
            catchErr = true;
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                bool isError = false;

                var msgLogic = new MessageBoxShowLogic();
                var inputNioroshiGyoushaCd = this.form.NIOROSHI_GYOUSHA_CD.Text;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(inputNioroshiGyoushaCd) || !this.tmpNioroshiGyoushaCd.Equals(inputNioroshiGyoushaCd) ||
                      (this.tmpNioroshiGyoushaCd.Equals(inputNioroshiGyoushaCd) && string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_NAME.Text)))
                      || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                    this.form.NIOROSHI_GYOUSHA_NAME.ReadOnly = true;
                    this.form.NIOROSHI_GYOUSHA_NAME.Tag = string.Empty;
                    this.form.NIOROSHI_GYOUSHA_NAME.TabStop = false;
                    if (!this.tmpNioroshiGyoushaCd.Equals(inputNioroshiGyoushaCd))
                    {
                        this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                        this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                        this.form.NIOROSHI_GENBA_NAME.ReadOnly = true;
                        this.form.NIOROSHI_GENBA_NAME.TabStop = false;
                        this.form.NIOROSHI_GENBA_NAME.Tag = string.Empty;
                    }

                    if (string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
                    {
                        this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                        this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                        this.form.NIOROSHI_GENBA_NAME.ReadOnly = true;
                        this.form.NIOROSHI_GENBA_NAME.TabStop = false;
                        this.form.NIOROSHI_GENBA_NAME.Tag = string.Empty;

                        if (!this.form.oldShokuchiKbn || this.form.keyEventArgs.Shift)
                        {
                            // フレームワーク側の再フォーカス処理を行わない
                            ret = false;
                        }
                        else
                        {
                            // フレームワーク側の再フォーカス処理を行う
                            ret = true;
                        }
                        /*ThangNguyen 20200219 #134056, #134060 Start*/
                        if (this.headerForm.KIHON_KEIRYOU.Text.Equals("1"))
                        {
                            this.form.SBN_GYOUSHA_CD.Text = string.Empty;
                            this.form.SBN_GYOUSHA_NAME.Text = string.Empty;
                            this.form.SBN_GENBA_CD.Text = string.Empty;
                            this.form.SBN_GENBA_NAME.Text = string.Empty;
                        }
                        /*ThangNguyen 20200219 #134056, #134060 End*/
                    }
                    else
                    {
                        catchErr = true;
                        var retData = this.accessor.GetGyousha(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                        if (!catchErr)
                        {
                            ret = false;
                            return ret;
                        }
                        var gyousha = retData;
                        if (gyousha != null)
                        {
                            // PKは1つなので複数ヒットしない
                            if (gyousha.GYOUSHAKBN_UKEIRE.IsTrue && (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                            {
                                // 荷卸業者名
                                this.form.NIOROSHI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                                if (gyousha.SHOKUCHI_KBN.IsTrue)
                                {
                                    this.form.NIOROSHI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                                    this.form.NIOROSHI_GYOUSHA_NAME.ReadOnly = false;
                                    this.form.NIOROSHI_GYOUSHA_NAME.TabStop = GetTabStop("NIOROSHI_GYOUSHA_NAME");
                                    this.form.NIOROSHI_GYOUSHA_NAME.Tag = this.nioroshiGyoushaHintText;
                                    this.form.NIOROSHI_GYOUSHA_NAME.Focus();
                                }
                                else
                                {
                                    if (this.form.oldShokuchiKbn)
                                    {
                                        ret = true;
                                    }
                                }
                                /*ThangNguyen 20200219 #134056, #134060 Start*/
                                if (this.headerForm.KIHON_KEIRYOU.Text.Equals("1"))
                                {
                                    this.SetSbnGyoushaByGyousha(this.form.NIOROSHI_GYOUSHA_CD.Text);
                                }
                                /*ThangNguyen 20200219 #134056, #134060 End*/
                                // 入力済の荷降現場との関連チェック
                                bool isContinue = false;
                                M_GENBA genba = new M_GENBA();
                                if (!string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
                                {
                                    var genbaEntityList = this.accessor.GetGenbaByGyousha(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                                    if (genbaEntityList != null || genbaEntityList.Length >= 1)
                                    {
                                        foreach (M_GENBA genbaEntity in genbaEntityList)
                                        {
                                            if (this.form.NIOROSHI_GENBA_CD.Text.Equals(genbaEntity.GENBA_CD)
                                                && (genbaEntity.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue
                                                    || genbaEntity.SAISHUU_SHOBUNJOU_KBN.IsTrue
                                                    || genbaEntity.TSUMIKAEHOKAN_KBN.IsTrue))
                                            {
                                                isContinue = true;
                                                genba = genbaEntity;
                                                ret = true;
                                                break;
                                            }
                                        }
                                        if (!isContinue)
                                        {
                                            // 一致するものがないので、入力されている現場CDを消す
                                            this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                                            this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                                        }
                                        else
                                        {
                                            // 一致する現場CDがあれば、現場名を再設定する
                                            if (genba.SHOKUCHI_KBN.IsTrue)
                                            {
                                                this.form.NIOROSHI_GENBA_NAME.ReadOnly = false;
                                                this.form.NIOROSHI_GENBA_NAME.TabStop = GetTabStop("NIOROSHI_GENBA_NAME");
                                                this.form.NIOROSHI_GENBA_NAME.Tag = this.nioroshiGenbaHintText;
                                                this.form.NIOROSHI_GENBA_NAME.Text = genba.GENBA_NAME1;
                                                this.form.NIOROSHI_GENBA_NAME.Focus();
                                            }
                                            else
                                            {
                                                this.form.NIOROSHI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                                            }
                                            /*ThangNguyen 20200219 #134056, #134060 Start*/
                                            if (this.headerForm.KIHON_KEIRYOU.Text.Equals("1"))
                                            {
                                                this.SetSbnGenbaByGenba(this.form.SBN_GYOUSHA_CD.Text, this.form.NIOROSHI_GENBA_CD.Text);
                                            }
                                            /*ThangNguyen 20200219 #134056, #134060 End*/
                                        }
                                    }
                                }
                            }
                            else
                            {
                                this.form.isInputError = true;
                                // エラーメッセージ
                                msgLogic.MessageBoxShow("E020", "荷降業者");
                                this.form.NIOROSHI_GYOUSHA_CD.Focus();
                                isError = true;
                            }
                        }
                        else
                        {
                            this.form.isInputError = true;
                            msgLogic.MessageBoxShow("E020", "荷降業者");
                            this.form.NIOROSHI_GYOUSHA_CD.Focus();
                            isError = true;
                        }
                    }

                    if (!isError)
                    {
                        if (!this.tmpNioroshiGyoushaCd.Equals(inputNioroshiGyoushaCd))
                        {
                            // 明細行すべての単価を再設定
                            var list = this.form.gcMultiRow1.Rows.Where(r => !r.IsNewRow).ToList();

                            foreach (Row dr in list)
                            {
                                if (!this.SearchAndCalcForUnit(false, dr))
                                {
                                    return false;
                                }
                            }

                            // 合計金額の再計算
                            if (!this.CalcTotalValues())
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNioroshiGyoushaCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNioroshiGyoushaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }
        #endregion

        #region 荷降現場チェック
        private string nioroshiGenbaHintText = "全角20文字/半角40文字以内で入力してください";

        /// <summary>
        /// 荷降現場CDの存在チェック
        /// </summary>
        internal bool CheckNioroshiGenbaCd(out bool catchErr)
        {
            catchErr = true;
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                bool isError = false;

                var msgLogic = new MessageBoxShowLogic();
                var inputNioroshiGenbaCd = this.form.NIOROSHI_GENBA_CD.Text;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(inputNioroshiGenbaCd) || !this.tmpNioroshiGenbaCd.Equals(inputNioroshiGenbaCd)) ||
                    (this.tmpNioroshiGenbaCd.Equals(inputNioroshiGenbaCd) && string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_NAME.Text))
                    || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                    this.form.NIOROSHI_GENBA_NAME.ReadOnly = true;
                    this.form.NIOROSHI_GENBA_NAME.TabStop = false;
                    this.form.NIOROSHI_GENBA_NAME.Tag = string.Empty;

                    if (string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
                    {
                        this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                        if (this.form.oldShokuchiKbn)
                        {
                            ret = true;
                        }
                        /*ThangNguyen 20200219 #134056, #134060 Start*/
                        if (this.headerForm.KIHON_KEIRYOU.Text.Equals("1"))
                        {
                            this.form.SBN_GENBA_CD.Text = string.Empty;
                            this.form.SBN_GENBA_NAME.Text = string.Empty;
                        }
                        /*ThangNguyen 20200219 #134056, #134060 End*/
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
                        {
                            msgLogic.MessageBoxShow("E051", "荷降業者");
                            this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                            this.form.NIOROSHI_GENBA_CD.Focus();
                            isError = true;
                            ret = false;
                            return ret;
                        }

                        var genbaEntityList = this.accessor.GetGenbaList(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.NIOROSHI_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                        M_GENBA genba = new M_GENBA();

                        if (genbaEntityList == null || genbaEntityList.Length < 1)
                        {
                            this.form.isInputError = true;
                            msgLogic.MessageBoxShow("E020", "荷降現場");
                            this.form.NIOROSHI_GENBA_CD.Focus();
                            isError = true;
                        }
                        else
                        {
                            genba = genbaEntityList[0];
                            // 荷卸業者名入力チェック
                            if (string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
                            {
                                // エラーメッセージ
                                this.form.isInputError = true;
                                msgLogic.MessageBoxShow("E020", "荷降現場");
                                this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                                this.form.NIOROSHI_GENBA_CD.Focus();
                                isError = true;
                            }
                            // 荷降業者と荷降現場の関連チェック
                            else if (genba == null)
                            {
                                // 一致するデータがないのでエラー
                                this.form.isInputError = true;
                                msgLogic.MessageBoxShow("E020", "荷降現場");
                                this.form.NIOROSHI_GENBA_CD.Focus();
                                isError = true;
                            }
                            else
                            {
                                catchErr = true;
                                var retData = this.accessor.GetGyousha(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                                if (!catchErr)
                                {
                                    ret = false;
                                    return ret;
                                }
                                // 業者設定
                                var gyousha = retData;
                                if (gyousha != null)
                                {
                                    // PKは1つなので複数ヒットしない
                                    if (gyousha.GYOUSHAKBN_UKEIRE.IsTrue && (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                                    {
                                        this.form.NIOROSHI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                                        // 荷卸業者名
                                        this.form.NIOROSHI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                                        if (gyousha.SHOKUCHI_KBN.IsTrue)
                                        {
                                            this.form.NIOROSHI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                                            this.form.NIOROSHI_GYOUSHA_NAME.ReadOnly = false;
                                            this.form.NIOROSHI_GYOUSHA_NAME.TabStop = GetTabStop("NIOROSHI_GYOUSHA_NAME");
                                            this.form.NIOROSHI_GYOUSHA_NAME.Tag = this.nioroshiGyoushaHintText;
                                        }
                                        /*ThangNguyen 20200219 #134056, #134060 Start*/
                                        if (this.headerForm.KIHON_KEIRYOU.Text.Equals("1"))
                                        {
                                            this.SetSbnGyoushaByGyousha(this.form.NIOROSHI_GYOUSHA_CD.Text);
                                        }
                                        /*ThangNguyen 20200219 #134056, #134060 End*/
                                    }
                                }

                                // 事業場区分、現場区分チェック
                                if (genba.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || genba.SAISHUU_SHOBUNJOU_KBN.IsTrue || genba.TSUMIKAEHOKAN_KBN.IsTrue)
                                {
                                    this.form.NIOROSHI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;

                                    // 諸口区分チェック
                                    if (genba.SHOKUCHI_KBN.IsTrue)
                                    {
                                        // 荷卸現場名編集可
                                        this.form.NIOROSHI_GENBA_NAME.Text = genba.GENBA_NAME1;
                                        this.form.NIOROSHI_GENBA_NAME.ReadOnly = false;
                                        this.form.NIOROSHI_GENBA_NAME.TabStop = GetTabStop("NIOROSHI_GENBA_NAME");
                                        this.form.NIOROSHI_GENBA_NAME.Tag = this.nioroshiGenbaHintText;
                                        this.form.NIOROSHI_GENBA_NAME.Focus();
                                    }

                                    if (this.form.oldShokuchiKbn)
                                    {
                                        ret = true;
                                    }
                                    /*ThangNguyen 20200219 #134056, #134060 Start*/
                                    if (this.headerForm.KIHON_KEIRYOU.Text.Equals("1"))
                                    {
                                        this.SetSbnGenbaByGenba(this.form.SBN_GYOUSHA_CD.Text, this.form.NIOROSHI_GENBA_CD.Text);
                                    }
                                    /*ThangNguyen 20200219 #134056, #134060 End*/
                                }
                                else
                                {
                                    // 一致するデータがないのでエラー
                                    this.form.isInputError = true;
                                    msgLogic.MessageBoxShow("E020", "荷降現場");
                                    this.form.NIOROSHI_GENBA_CD.Focus();
                                    isError = true;
                                }
                            }
                        }
                    }

                    if (!isError)
                    {
                        if (!this.tmpNioroshiGenbaCd.Equals(inputNioroshiGenbaCd))
                        {
                            // 明細行すべての単価を再設定
                            var list = this.form.gcMultiRow1.Rows.Where(r => !r.IsNewRow).ToList();

                            foreach (Row dr in list)
                            {
                                if (!this.SearchAndCalcForUnit(false, dr))
                                {
                                    return false;
                                }
                            }

                            // 合計金額の再計算
                            if (!this.CalcTotalValues())
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNioroshiGenbaCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNioroshiGenbaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }
        #endregion

        #region 荷積業者チェック
        private string nizumiGyoushaHintText = "全角20文字/半角40文字以内で入力してください";

        /// <summary>
        /// 荷積業者チェック
        /// </summary>
        internal bool CheckNizumiGyoushaCd(out bool catchErr)
        {
            catchErr = true;
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                bool isError = false;

                var msgLogic = new MessageBoxShowLogic();
                var inputNizumiGyoushaCd = this.form.NIZUMI_GYOUSHA_CD.Text;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(inputNizumiGyoushaCd) || !this.tmpNizumiGyoushaCd.Equals(inputNizumiGyoushaCd) ||
                      (this.tmpNizumiGyoushaCd.Equals(inputNizumiGyoushaCd) && string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_NAME.Text)))
                      || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
                    this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = true;
                    this.form.NIZUMI_GYOUSHA_NAME.Tag = string.Empty;
                    this.form.NIZUMI_GYOUSHA_NAME.TabStop = false;
                    if (!this.tmpNizumiGyoushaCd.Equals(inputNizumiGyoushaCd))
                    {
                        this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                        this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                        this.form.NIZUMI_GENBA_NAME.ReadOnly = true;
                        this.form.NIZUMI_GENBA_NAME.TabStop = false;
                        this.form.NIZUMI_GENBA_NAME.Tag = string.Empty;
                    }

                    if (string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
                    {
                        this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                        this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                        this.form.NIZUMI_GENBA_NAME.ReadOnly = true;
                        this.form.NIZUMI_GENBA_NAME.TabStop = false;
                        this.form.NIZUMI_GENBA_NAME.Tag = string.Empty;

                        if (!this.form.oldShokuchiKbn || this.form.keyEventArgs.Shift)
                        {
                            // フレームワーク側の再フォーカス処理を行わない
                            ret = false;
                        }
                        else
                        {
                            // フレームワーク側の再フォーカス処理を行う
                            ret = true;
                        }
                        /*ThangNguyen 20200219 #134056, #134060 Start*/
                        if (this.headerForm.KIHON_KEIRYOU.Text.Equals("2"))
                        {
                            this.form.HST_GYOUSHA_CD.Text = string.Empty;
                            this.form.HST_GYOUSHA_NAME.Text = string.Empty;
                            this.form.HST_GENBA_CD.Text = string.Empty;
                            this.form.HST_GENBA_NAME.Text = string.Empty;
                        }
                        /*ThangNguyen 20200219 #134056, #134060 End*/
                    }
                    else
                    {
                        catchErr = true;
                        var retData = this.accessor.GetGyousha(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                        if (!catchErr)
                        {
                            ret = false;
                            return ret;
                        }
                        var gyousha = retData;
                        if (gyousha != null)
                        {
                            // PKは1つなので複数ヒットしない
                            if (gyousha.GYOUSHAKBN_SHUKKA.IsTrue && (gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                            {
                                // 荷積業者名
                                this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                                if (gyousha.SHOKUCHI_KBN.IsTrue)
                                {
                                    this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                                    this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = false;
                                    this.form.NIZUMI_GYOUSHA_NAME.TabStop = GetTabStop("NIZUMI_GYOUSHA_NAME");
                                    this.form.NIZUMI_GYOUSHA_NAME.Tag = this.nizumiGyoushaHintText;
                                    this.form.NIZUMI_GYOUSHA_NAME.Focus();
                                }
                                else
                                {
                                    if (this.form.oldShokuchiKbn)
                                    {
                                        ret = true;
                                    }
                                }
                                /*ThangNguyen 20200219 #134056, #134060 Start*/
                                if (this.headerForm.KIHON_KEIRYOU.Text.Equals("2"))
                                {
                                    this.SetHstGyoushaByGyousha(this.form.NIZUMI_GYOUSHA_CD.Text);
                                }
                                /*ThangNguyen 20200219 #134056, #134060 End*/
                                // 入力済の荷積現場との関連チェック
                                bool isContinue = false;
                                M_GENBA genba = new M_GENBA();
                                if (!string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text))
                                {
                                    var genbaEntityList = this.accessor.GetGenbaByGyousha(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                                    if (genbaEntityList != null || genbaEntityList.Length >= 1)
                                    {
                                        foreach (M_GENBA genbaEntity in genbaEntityList)
                                        {
                                            if (this.form.NIZUMI_GENBA_CD.Text.Equals(genbaEntity.GENBA_CD)
                                                && (genbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue))
                                            {
                                                isContinue = true;
                                                genba = genbaEntity;
                                                ret = true;
                                                break;
                                            }
                                        }
                                        if (!isContinue)
                                        {
                                            // 一致するものがないので、入力されている現場CDを消す
                                            this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                                            this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                                        }
                                        else
                                        {
                                            // 一致する現場CDがあれば、現場名を再設定する
                                            if (genba.SHOKUCHI_KBN.IsTrue)
                                            {
                                                this.form.NIZUMI_GENBA_NAME.ReadOnly = false;
                                                this.form.NIZUMI_GENBA_NAME.TabStop = GetTabStop("NIZUMI_GENBA_NAME");    // No.3822
                                                this.form.NIZUMI_GENBA_NAME.Tag = this.nizumiGenbaHintText;
                                                this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME1;
                                                this.form.NIZUMI_GENBA_NAME.Focus();
                                            }
                                            else
                                            {
                                                this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                                            }
                                            /*ThangNguyen 20200219 #134056, #134060 Start*/
                                            if (this.headerForm.KIHON_KEIRYOU.Text.Equals("2"))
                                            {
                                                this.SetHstGenbaByGenba(this.form.HST_GYOUSHA_CD.Text, this.form.NIZUMI_GENBA_CD.Text);
                                            }
                                            /*ThangNguyen 20200219 #134056, #134060 End*/
                                        }
                                    }
                                }
                            }
                            else
                            {
                                this.form.isInputError = true;
                                // エラーメッセージ
                                msgLogic.MessageBoxShow("E020", "荷積業者");
                                this.form.NIZUMI_GYOUSHA_CD.Focus();
                                isError = true;
                            }
                        }
                        else
                        {
                            this.form.isInputError = true;
                            msgLogic.MessageBoxShow("E020", "荷積業者");
                            this.form.NIZUMI_GYOUSHA_CD.Focus();
                            isError = true;
                        }
                    }

                    if (!isError)
                    {
                        if (!this.tmpNizumiGyoushaCd.Equals(inputNizumiGyoushaCd))
                        {
                            // 明細行すべての単価を再設定
                            var list = this.form.gcMultiRow1.Rows.Where(r => !r.IsNewRow).ToList();

                            foreach (Row dr in list)
                            {
                                if (!this.SearchAndCalcForUnit(false, dr))
                                {
                                    return false;
                                }
                            }

                            // 合計金額の再計算
                            if (!this.CalcTotalValues())
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNizumiGyoushaCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNizumiGyoushaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }
        #endregion

        #region 荷積現場チェック
        private string nizumiGenbaHintText = "全角20文字/半角40文字以内で入力してください";

        /// <summary>
        /// 荷積現場CDの存在チェック
        /// </summary>
        internal bool CheckNizumiGenbaCd(out bool catchErr)
        {
            catchErr = true;
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();


                bool isError = false;

                var msgLogic = new MessageBoxShowLogic();
                var inputNizumiGenbaCd = this.form.NIZUMI_GENBA_CD.Text;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(inputNizumiGenbaCd) || !this.tmpNizumiGenbaCd.Equals(inputNizumiGenbaCd)) ||
                    (this.tmpNizumiGenbaCd.Equals(inputNizumiGenbaCd) && string.IsNullOrEmpty(this.form.NIZUMI_GENBA_NAME.Text))
                    || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                    this.form.NIZUMI_GENBA_NAME.ReadOnly = true;
                    this.form.NIZUMI_GENBA_NAME.TabStop = false;
                    this.form.NIZUMI_GENBA_NAME.Tag = string.Empty;

                    if (string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text))
                    {
                        this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                        if (this.form.oldShokuchiKbn)
                        {
                            ret = true;
                        }
                        /*ThangNguyen 20200219 #134056, #134060 Start*/
                        if (this.headerForm.KIHON_KEIRYOU.Text.Equals("2"))
                        {
                            this.form.HST_GENBA_CD.Text = string.Empty;
                            this.form.HST_GENBA_NAME.Text = string.Empty;
                        }
                        /*ThangNguyen 20200219 #134056, #134060 End*/
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
                        {
                            msgLogic.MessageBoxShow("E051", "荷積業者");
                            this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                            this.form.NIZUMI_GENBA_CD.Focus();
                            isError = true;
                            ret = false;
                            return ret;
                        }

                        var genbaEntityList = this.accessor.GetGenbaList(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.NIZUMI_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                        M_GENBA genba = new M_GENBA();

                        if (genbaEntityList == null || genbaEntityList.Length < 1)
                        {
                            this.form.isInputError = true;
                            msgLogic.MessageBoxShow("E020", "荷積現場");
                            this.form.NIZUMI_GENBA_CD.Focus();
                            isError = true;
                        }
                        else
                        {
                            genba = genbaEntityList[0];
                            // 荷積業者名入力チェック
                            if (string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
                            {
                                // エラーメッセージ
                                this.form.isInputError = true;
                                msgLogic.MessageBoxShow("E020", "荷積現場");
                                this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                                this.form.NIZUMI_GENBA_CD.Focus();
                                isError = true;
                            }
                            // 荷積業者と荷積現場の関連チェック
                            else if (genba == null)
                            {
                                // 一致するデータがないのでエラー
                                this.form.isInputError = true;
                                msgLogic.MessageBoxShow("E020", "荷積現場");
                                this.form.NIZUMI_GENBA_CD.Focus();
                                isError = true;
                            }
                            else
                            {
                                catchErr = true;
                                var retData = this.accessor.GetGyousha(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                                if (!catchErr)
                                {
                                    ret = false;
                                    return ret;
                                }
                                // 業者設定
                                var gyousha = retData;
                                if (gyousha != null)
                                {
                                    // PKは1つなので複数ヒットしない
                                    if (gyousha.GYOUSHAKBN_SHUKKA.IsTrue && (gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                                    {
                                        this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                                        // 荷積業者名
                                        this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                                        if (gyousha.SHOKUCHI_KBN.IsTrue)
                                        {
                                            this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                                            this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = false;
                                            this.form.NIZUMI_GYOUSHA_NAME.TabStop = GetTabStop("NIZUMI_GYOUSHA_NAME");
                                            this.form.NIZUMI_GYOUSHA_NAME.Tag = this.nizumiGyoushaHintText;
                                        }
                                        /*ThangNguyen 20200219 #134056, #134060 Start*/
                                        if (this.headerForm.KIHON_KEIRYOU.Text.Equals("2"))
                                        {
                                            this.SetHstGyoushaByGyousha(this.form.NIZUMI_GYOUSHA_CD.Text);
                                        }
                                        /*ThangNguyen 20200219 #134056, #134060 End*/
                                    }
                                }

                                // 事業場区分、現場区分チェック
                                if (genba.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue || genba.TSUMIKAEHOKAN_KBN)
                                {
                                    this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;

                                    // 諸口区分チェック
                                    if (genba.SHOKUCHI_KBN.IsTrue)
                                    {
                                        // 荷卸現場名編集可
                                        this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME1;
                                        this.form.NIZUMI_GENBA_NAME.ReadOnly = false;
                                        this.form.NIZUMI_GENBA_NAME.TabStop = GetTabStop("NIZUMI_GENBA_NAME");
                                        this.form.NIZUMI_GENBA_NAME.Tag = this.nizumiGenbaHintText;
                                        this.form.NIZUMI_GENBA_NAME.Focus();
                                    }

                                    if (this.form.oldShokuchiKbn)
                                    {
                                        ret = true;
                                    }
                                    /*ThangNguyen 20200219 #134056, #134060 Start*/
                                    if (this.headerForm.KIHON_KEIRYOU.Text.Equals("2"))
                                    {
                                        this.SetHstGenbaByGenba(this.form.HST_GYOUSHA_CD.Text, this.form.NIZUMI_GENBA_CD.Text);
                                    }
                                    /*ThangNguyen 20200219 #134056, #134060 End*/
                                }
                                else
                                {
                                    // 一致するデータがないのでエラー
                                    this.form.isInputError = true;
                                    msgLogic.MessageBoxShow("E020", "荷積現場");
                                    this.form.NIZUMI_GENBA_CD.Focus();
                                    isError = true;
                                }
                            }
                        }
                    }

                    if (!isError)
                    {
                        if (!this.tmpNizumiGenbaCd.Equals(inputNizumiGenbaCd))
                        {
                            // 明細行すべての単価を再設定
                            var list = this.form.gcMultiRow1.Rows.Where(r => !r.IsNewRow).ToList();

                            foreach (Row dr in list)
                            {
                                if (!this.SearchAndCalcForUnit(false, dr))
                                {
                                    return false;
                                }
                            }

                            // 合計金額の再計算
                            if (!this.CalcTotalValues())
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNizumiGenbaCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNizumiGenbaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }
        #endregion

        #region 運搬業者チェック
        private string unpanGyoushaHintText = "全角20文字/半角40文字以内で入力してください";

        /// <summary>
        /// 運搬業者CDの存在チェック
        /// </summary>
        internal bool CheckUnpanGyoushaCd(out bool catchErr, bool tairyuuFlg = false)
        {
            catchErr = true;
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(catchErr, tairyuuFlg);

                bool isError = false;

                var msgLogic = new MessageBoxShowLogic();
                var inputUnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(inputUnpanGyoushaCd) || !this.tmpUnpanGyoushaCd.Equals(inputUnpanGyoushaCd)) || this.form.isFromSearchButton || this.form.isInputError)
                {
                    // 初期化
                    this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                    this.form.UNPAN_GYOUSHA_NAME.ReadOnly = true;
                    this.form.UNPAN_GYOUSHA_NAME.TabStop = false;
                    this.form.UNPAN_GYOUSHA_NAME.Tag = string.Empty;

                    catchErr = true;
                    var retData = this.accessor.GetGyousha(this.form.UNPAN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                    if (!catchErr)
                    {
                        return false;
                    }
                    var gyousha = retData;
                    if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                    {
                        if (gyousha != null)
                        {
                            if ((this.headerForm.KIHON_KEIRYOU.Text == "1" && gyousha.GYOUSHAKBN_UKEIRE.IsTrue
                                || this.headerForm.KIHON_KEIRYOU.Text == "2" && gyousha.GYOUSHAKBN_SHUKKA.IsTrue)
                                && gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                            {
                                M_SHARYOU[] sharyouEntitys = null;
                                sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null, SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()), this.headerForm.KIHON_KEIRYOU.Text);

                                this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                                this.form.SHARYOU_CD.AutoChangeBackColorEnabled = true;

                                this.form.SHARYOU_EMPTY_JYUURYOU.Text = string.Empty;
                                if (sharyouEntitys == null)
                                {
                                    if (!this.form.oldSharyouShokuchiKbn)
                                    {
                                        // 車輌・車種をクリア
                                        this.form.SHARYOU_CD.Text = string.Empty;
                                        this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                                        //this.form.EMPTY_JYUURYOU.Text = string.Empty;
                                    }
                                    else
                                    {
                                        // 車輌名を編集可
                                        this.form.SHARYOU_CD.AutoChangeBackColorEnabled = false;
                                        this.form.SHARYOU_CD.BackColor = sharyouCdBackColor;
                                        this.form.SHARYOU_NAME_RYAKU.ReadOnly = false;
                                    }
                                }
                                else if (sharyouEntitys != null)
                                {
                                    var sharyouEntity = sharyouEntitys[0];
                                    this.form.SHARYOU_CD.Text = sharyouEntity.SHARYOU_CD;
                                    this.form.oldSharyouShokuchiKbn = false;
                                    this.form.SHARYOU_NAME_RYAKU.Text = sharyouEntity.SHARYOU_NAME_RYAKU;
                                    this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
                                    if (!sharyouEntity.KUUSHA_JYURYO.IsNull)
                                    {
                                        this.form.SHARYOU_EMPTY_JYUURYOU.Text = sharyouEntity.KUUSHA_JYURYO.ToString();
                                    }

                                    if (!tairyuuFlg && this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG
                                        && !string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text)
                                        && !string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text)
                                        && !string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                                    {
                                        Int64 number = this.GetTairyuuNumberBySharyou();
                                        if (number != -1 && (this.form.gcMultiRow1.Rows.Count == 0 || this.form.gcMultiRow1.Rows[0].IsNewRow))
                                        {
                                            var dia = this.msgLogic.MessageBoxShowConfirm("この車輌は、滞留状態の伝票がすでに登録されています。滞留伝票を読込ますか？");
                                            tairyuuFlg = true;
                                            if (dia == DialogResult.Yes)
                                            {
                                                this.ShowDenpyou(number);
                                                return true;
                                            }
                                        }
                                    }
                                    if (this.headerForm.KIHON_KEIRYOU.Text == "2" && !sharyouEntity.KUUSHA_JYURYO.IsNull && this.form.gcMultiRow1.Rows.Count > 0
                                        && string.IsNullOrEmpty(Convert.ToString(this.form.gcMultiRow1[0, CELL_NAME_EMPTY_JYUURYOU].Value)))
                                    {
                                        this.form.EMPTY_JYUURYOU.Text = sharyouEntity.KUUSHA_JYURYO.ToString();

                                        this.form.EMPTY_KEIRYOU_TIME.Text = this.GetDate(out catchErr);
                                        if (this.form.gcMultiRow1.Rows[0].IsNewRow)
                                        {
                                            this.form.gcMultiRow1.Rows.Add();
                                            this.NumberingRowNo();
                                        }
                                        this.form.gcMultiRow1[0, CELL_NAME_EMPTY_JYUURYOU].Value = sharyouEntity.KUUSHA_JYURYO.ToString();

                                        if (!string.IsNullOrEmpty(Convert.ToString(this.form.gcMultiRow1[0, CELL_NAME_STAK_JYUURYOU].Value))
                                            && string.IsNullOrEmpty(Convert.ToString(this.form.gcMultiRow1[0, CELL_NAME_KEIRYOU_TIME].Value)))
                                        {
                                            this.form.gcMultiRow1[0, CELL_NAME_KEIRYOU_TIME].Value = this.GetDate(out catchErr);
                                        }
                                    }

                                    // 運転者情報セット
                                    var untensha = this.accessor.GetShain(sharyouEntity.SHAIN_CD);
                                    if (untensha != null)
                                    {
                                        this.form.UNTENSHA_CD.Text = untensha.SHAIN_CD;
                                        this.form.UNTENSHA_NAME.Text = untensha.SHAIN_NAME_RYAKU;
                                    }
                                    else
                                    {
                                        this.form.UNTENSHA_CD.Text = string.Empty;
                                        this.form.UNTENSHA_NAME.Text = string.Empty;
                                    }

                                    // 車輌情報セット
                                    var shashuEntity = this.accessor.GetShashu(sharyouEntity.SHASYU_CD);
                                    if (shashuEntity != null)
                                    {
                                        this.form.SHASHU_CD.Text = shashuEntity.SHASHU_CD;
                                        this.form.SHASHU_NAME.Text = shashuEntity.SHASHU_NAME_RYAKU;
                                    }
                                    else
                                    {
                                        this.form.SHASHU_CD.Text = string.Empty;
                                        this.form.SHASHU_NAME.Text = string.Empty;
                                    }

                                    if (sharyouEntity.KEIRYOU_GYOUSHA_SET_KBN.IsTrue && this.form.GYOUSHA_CD.Text != sharyouEntity.GYOUSHA_CD)
                                    {
                                        this.tmpGyoushaCd = this.form.GYOUSHA_CD.Text;
                                        this.form.GYOUSHA_CD.Text = sharyouEntity.GYOUSHA_CD;
                                        // 業者名セット
                                        catchErr = true;
                                        ret = this.CheckGyousha(out catchErr);
                                        if (!catchErr)
                                        {
                                            throw new Exception("");
                                        }
                                    }
                                }

                                this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                                // 諸口区分チェック
                                if (gyousha.SHOKUCHI_KBN.IsTrue)
                                {
                                    // 運搬業者名編集可
                                    this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                                    this.form.UNPAN_GYOUSHA_NAME.ReadOnly = false;
                                    this.form.UNPAN_GYOUSHA_NAME.TabStop = GetTabStop("UNPAN_GYOUSHA_NAME");
                                    this.form.UNPAN_GYOUSHA_NAME.Tag = this.unpanGyoushaHintText;
                                    this.form.UNPAN_GYOUSHA_NAME.Focus();
                                }
                                else
                                {
                                    if (this.form.oldShokuchiKbn)
                                    {
                                        ret = true;
                                    }
                                }
                            }
                            else
                            {
                                this.form.isInputError = true;
                                msgLogic.MessageBoxShow("E020", "業者");
                                this.form.UNPAN_GYOUSHA_CD.Focus();
                                isError = true;
                            }
                        }
                        else
                        {
                            this.form.isInputError = true;
                            msgLogic.MessageBoxShow("E020", "業者");
                            this.form.UNPAN_GYOUSHA_CD.Focus();
                            isError = true;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.tmpUnpanGyoushaCd) && !this.form.oldSharyouShokuchiKbn)
                        {
                            this.form.SHARYOU_CD.Text = string.Empty;
                            this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                            this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
                            this.form.SHARYOU_EMPTY_JYUURYOU.Text = string.Empty;
                        }
                    }

                    if (!isError)
                    {
                        if (!this.tmpUnpanGyoushaCd.Equals(inputUnpanGyoushaCd))
                        {
                            // 明細行すべての単価を再設定
                            var list = this.form.gcMultiRow1.Rows.Where(r => !r.IsNewRow).ToList();

                            foreach (Row dr in list)
                            {
                                if (!this.SearchAndCalcForUnit(false, dr))
                                {
                                    return false;
                                }
                            }

                            // 合計金額の再計算
                            if (!this.CalcTotalValues())
                            {
                                return false;
                            }
                        }
                    }
                }

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckUnpanGyoushaCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUnpanGyoushaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }
        #endregion

        /// <summary>
        /// 入力担当者チェック
        /// </summary>
        internal bool CheckNyuuryokuTantousha()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.NYUURYOKU_TANTOUSHA_NAME.Text = string.Empty;
                strNyuryokuTantousyaName = string.Empty;

                if (string.IsNullOrEmpty(this.form.NYUURYOKU_TANTOUSHA_CD.Text))
                {
                    // 入力担当者CDがなければ既にエラーが表示されているはずなので何もしない
                    return true;
                }

                var shainEntity = this.accessor.GetShain(this.form.NYUURYOKU_TANTOUSHA_CD.Text, true);
                if (shainEntity == null)
                {
                    return true;
                }
                else if (shainEntity.NYUURYOKU_TANTOU_KBN.IsFalse)
                {
                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E058", "");
                    this.form.NYUURYOKU_TANTOUSHA_CD.Focus();
                    return true;
                }
                else
                {
                    this.form.NYUURYOKU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                    strNyuryokuTantousyaName = shainEntity.SHAIN_NAME;    // No.3279
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNyuuryokuTantousha", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNyuuryokuTantousha", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        private string torihikisakiHintText = "全角20文字/半角40文字以内で入力してください";

        /// <summary>
        /// 取引先チェック
        /// </summary>
        internal bool CheckTorihikisaki(out bool catchErr)
        {
            catchErr = true;
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                bool isError = false;

                var msgLogic = new MessageBoxShowLogic();
                var inputTorihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
                var oldTorihikisakiCd = this.tmpTorihikisakiCd;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if (this.form.isInputError || (String.IsNullOrEmpty(inputTorihikisakiCd) || !this.tmpTorihikisakiCd.Equals(inputTorihikisakiCd) ||
                    (this.tmpTorihikisakiCd.Equals(inputTorihikisakiCd) && string.IsNullOrEmpty(this.form.TORIHIKISAKI_NAME_RYAKU.Text)))
                    || this.form.isFromSearchButton)
                {
                    //　初期化
                    this.form.isInputError = false;
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                    this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
                    this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
                    this.form.TORIHIKISAKI_NAME_RYAKU.Tag = string.Empty;

                    if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                    {
                        catchErr = true;
                        var torihikisakiEntity = this.accessor.GetTorihikisaki(inputTorihikisakiCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                        if (!catchErr)
                        {
                            return false;
                        }
                        if (null == torihikisakiEntity)
                        {
                            this.form.isInputError = true;
                            msgLogic.MessageBoxShow("E020", "取引先");
                            this.form.TORIHIKISAKI_CD.Focus();
                            isError = true;
                            ret = false;
                        }
                        else
                        {
                            if (CheckTorihikisakiAndKyotenCd(torihikisakiEntity, this.form.TORIHIKISAKI_CD.Text))
                            {
                                // 取引先の拠点と入力された拠点コードの関連チェックOK
                                this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                                this.tmpTorihikisakiCd = torihikisakiEntity.TORIHIKISAKI_CD;
                            }
                            else
                            {
                                this.form.isInputError = true;
                                this.form.TORIHIKISAKI_CD.Focus();
                                isError = true;
                                ret = false;
                            }
                        }

                        if (ret)
                        {
                            // 取引先名
                            this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                            this.tmpTorihikisakiCd = torihikisakiEntity.TORIHIKISAKI_CD;

                            // 諸口区分チェック
                            if (torihikisakiEntity.SHOKUCHI_KBN.IsTrue)
                            {
                                // 取引先名編集可
                                this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME1;
                                this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = false;
                                this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = GetTabStop("TORIHIKISAKI_NAME_RYAKU");    // No.3822
                                this.form.TORIHIKISAKI_NAME_RYAKU.Tag = this.torihikisakiHintText;
                                this.form.TORIHIKISAKI_NAME_RYAKU.Focus();

                                ret = false;
                            }
                            else
                            {
                                if (!this.form.oldShokuchiKbn)
                                {
                                    ret = false;
                                }
                            }

                            //取引区分チェック
                            this.CheckTorihikiKBN();
                        }
                    }
                    else
                    {
                        // 関連項目クリア
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;

                        if (!this.form.oldShokuchiKbn || this.form.keyEventArgs.Shift)
                        {
                            // フレームワーク側の再フォーカス処理を行わない
                            ret = false;
                        }
                        else
                        {
                            // フレームワーク側の再フォーカス処理を行う
                            ret = true;
                        }
                    }

                    if (!isError)
                    {
                        if (!oldTorihikisakiCd.Equals(inputTorihikisakiCd))
                        {
                            // 営業担当者の設定
                            this.SetEigyouTantousha(this.form.GENBA_CD.Text, this.form.GYOUSHA_CD.Text, this.form.TORIHIKISAKI_CD.Text);
                        }

                        if (!oldTorihikisakiCd.Equals(inputTorihikisakiCd))
                        {
                            // 明細行すべての単価を再設定
                            var list = this.form.gcMultiRow1.Rows.Where(r => !r.IsNewRow).ToList();

                            foreach (Row dr in list)
                            {
                                if (!this.SearchAndCalcForUnit(false, dr))
                                {
                                    return false;
                                }
                            }

                            // 合計金額の再計算
                            if (!this.CalcTotalValues())
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    ret = false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckTorihikisaki", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                this.form.isInputError = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTorihikisaki", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                this.form.isInputError = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;

        }

        /// <summary>
        /// 取引先の拠点コードと入力された拠点コードの関連チェック
        /// </summary>
        /// <param name="torihikisakiEntity">取引先エンティティ</param>
        /// <param name="TorihikisakiCd">取引先CD</param>
        /// <returns>True：チェックOK False：チェックNG</returns>
        internal bool CheckTorihikisakiAndKyotenCd(M_TORIHIKISAKI torihikisakiEntity, string TorihikisakiCd)
        {
            bool returnVal = false;

            if (string.IsNullOrEmpty(TorihikisakiCd))
            {
                // 取引先の入力がない場合はチェック対象外
                returnVal = true;
                return returnVal;
            }

            if (torihikisakiEntity == null)
            {
                // 取引先マスタを引数の取引先CDで取得しなおす
                bool catchErr = true;
                torihikisakiEntity = this.accessor.GetTorihikisaki(TorihikisakiCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
            }

            if (torihikisakiEntity != null)
            {
                if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
                {
                    if (SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text) == torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD
                        || torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString().Equals(SalesPaymentConstans.KYOTEN_ZENSHA))
                    {
                        // 入力画面の拠点コードと取引先の拠点コードが等しいか、取引先の拠点コードが99（全社)の場合
                        returnVal = true;
                    }
                    else
                    {
                        // 入力画面の拠点コードと取引先の拠点コードが等しくない場合
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E146");
                        this.form.TORIHIKISAKI_CD.Focus();
                    }
                }
                else
                {   // 拠点が指定されていない場合
                    returnVal = true;
                }
            }
            else
            {
                returnVal = true;
                return returnVal;
            }

            return returnVal;
        }

        /// <summary>
        /// 取引区分チェック
        /// </summary>
        internal void CheckTorihikiKBN()
        {
            LogUtility.DebugMethodStart();

            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    var seikyuu = this.accessor.GetTorihikisakiSeikyuu(this.form.TORIHIKISAKI_CD.Text);
                    if (seikyuu.TORIHIKI_KBN_CD == 1)
                    {
                        // 1.現金
                        this.form.SEIKYUU_TORIHIKI_KBN_CD.Text = "1";
                        this.form.SEIKYUU_TORIHIKI_KBN_NAME.Text = "現金";
                    }
                    else if (seikyuu.TORIHIKI_KBN_CD == 2)
                    {
                        // 2.掛け
                        this.form.SEIKYUU_TORIHIKI_KBN_CD.Text = "2";
                        this.form.SEIKYUU_TORIHIKI_KBN_NAME.Text = "掛け";
                    }
                    else
                    {
                        this.form.SEIKYUU_TORIHIKI_KBN_CD.Text = "";
                        this.form.SEIKYUU_TORIHIKI_KBN_NAME.Text = "";
                    }

                    if (seikyuu.ZEI_KEISAN_KBN_CD == 1)
                    {
                        // 1.伝票毎
                        this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text = "1";
                        this.form.SEIKYUU_ZEI_KEISAN_KBN_NAME.Text = "伝票毎";
                    }
                    else if (seikyuu.ZEI_KEISAN_KBN_CD == 2)
                    {
                        // 2.請求毎
                        this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text = "2";
                        this.form.SEIKYUU_ZEI_KEISAN_KBN_NAME.Text = "請求毎";
                    }
                    else if (seikyuu.ZEI_KEISAN_KBN_CD == 3)
                    {
                        // 3.明細毎
                        this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text = "3";
                        this.form.SEIKYUU_ZEI_KEISAN_KBN_NAME.Text = "明細毎";
                    }
                    else
                    {
                        this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text = "";
                        this.form.SEIKYUU_ZEI_KEISAN_KBN_NAME.Text = "";
                    }

                    if (seikyuu.ZEI_KBN_CD == 1)
                    {
                        // 1.外税
                        this.form.SEIKYUU_ZEI_KBN_CD.Text = "1";
                        this.form.SEIKYUU_ZEI_KBN_NAME.Text = "外税";
                    }
                    else if (seikyuu.ZEI_KBN_CD == 2)
                    {
                        // 2.内税
                        this.form.SEIKYUU_ZEI_KBN_CD.Text = "2";
                        this.form.SEIKYUU_ZEI_KBN_NAME.Text = "内税";
                    }
                    else if (seikyuu.ZEI_KBN_CD == 3)
                    {
                        // 3.非課税
                        this.form.SEIKYUU_ZEI_KBN_CD.Text = "3";
                        this.form.SEIKYUU_ZEI_KBN_NAME.Text = "非課税";
                    }
                    else
                    {
                        this.form.SEIKYUU_ZEI_KBN_CD.Text = "";
                        this.form.SEIKYUU_ZEI_KBN_NAME.Text = "";
                    }

                    bool catchErr = false;
                    var torihiki = this.accessor.GetTorihikisaki(this.form.TORIHIKISAKI_CD.Text, out catchErr);
                    if (torihiki != null)
                    {
                        this.form.RECEIPT_KEISHOU_1.Text = torihiki.TORIHIKISAKI_KEISHOU1;
                        this.form.RECEIPT_KEISHOU_2.Text = torihiki.TORIHIKISAKI_KEISHOU2;
                    }
                    else
                    {
                        this.form.RECEIPT_KEISHOU_1.Text = string.Empty;
                        this.form.RECEIPT_KEISHOU_2.Text = string.Empty;
                    }
                    this.form.RECEIPT_KEISHOU_1.Enabled = false;
                    this.form.RECEIPT_KEISHOU_2.Enabled = false;
                    if (this.form.SEIKYUU_TORIHIKI_KBN_CD.Text == "1")
                    {
                        this.form.RECEIPT_KBN_CD.Enabled = true;
                        this.form.rb_RECEIPT_KBN_1.Enabled = true;
                        this.form.rb_RECEIPT_KBN_2.Enabled = true;
                        if (this.form.SEIKYUU_TORIHIKI_KBN_CD.Text == "1")
                        {
                            this.form.RECEIPT_KEISHOU_1.Enabled = true;
                            this.form.RECEIPT_KEISHOU_2.Enabled = true;
                        }
                    }
                    else
                    {
                        this.form.RECEIPT_KBN_CD.Enabled = false;
                        this.form.rb_RECEIPT_KBN_1.Enabled = false;
                        this.form.rb_RECEIPT_KBN_2.Enabled = false;
                    }

                    var shiharai = this.accessor.GetTorihikisakiShiharai(this.form.TORIHIKISAKI_CD.Text);
                    if (shiharai.TORIHIKI_KBN_CD == 1)
                    {
                        // 1.現金
                        this.form.SHIHARAI_TORIHIKI_KBN_CD.Text = "1";
                        this.form.SHIHARAI_TORIHIKI_KBN_NAME.Text = "現金";
                    }
                    else if (shiharai.TORIHIKI_KBN_CD == 2)
                    {
                        // 2.掛け
                        this.form.SHIHARAI_TORIHIKI_KBN_CD.Text = "2";
                        this.form.SHIHARAI_TORIHIKI_KBN_NAME.Text = "掛け";
                    }
                    else
                    {
                        this.form.SHIHARAI_TORIHIKI_KBN_CD.Text = "";
                        this.form.SHIHARAI_TORIHIKI_KBN_NAME.Text = "";
                    }


                    if (shiharai.ZEI_KEISAN_KBN_CD == 1)
                    {
                        // 1.伝票毎
                        this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = "1";
                        this.form.SHIHARAI_ZEI_KEISAN_KBN_NAME.Text = "伝票毎";
                    }
                    else if (shiharai.ZEI_KEISAN_KBN_CD == 2)
                    {
                        // 2.精算毎
                        this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = "2";
                        this.form.SHIHARAI_ZEI_KEISAN_KBN_NAME.Text = "精算毎";
                    }
                    else if (shiharai.ZEI_KEISAN_KBN_CD == 3)
                    {
                        // 3.明細毎
                        this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = "3";
                        this.form.SHIHARAI_ZEI_KEISAN_KBN_NAME.Text = "明細毎";
                    }
                    else
                    {
                        this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = "";
                        this.form.SHIHARAI_ZEI_KEISAN_KBN_NAME.Text = "";
                    }

                    if (shiharai.ZEI_KBN_CD == 1)
                    {
                        // 1.外税
                        this.form.SHIHARAI_ZEI_KBN_CD.Text = "1";
                        this.form.SHIHARAI_ZEI_KBN_NAME.Text = "外税";
                    }
                    else if (shiharai.ZEI_KBN_CD == 2)
                    {
                        // 2.内税
                        this.form.SHIHARAI_ZEI_KBN_CD.Text = "2";
                        this.form.SHIHARAI_ZEI_KBN_NAME.Text = "内税";
                    }
                    else if (shiharai.ZEI_KBN_CD == 3)
                    {
                        // 3.非課税
                        this.form.SHIHARAI_ZEI_KBN_CD.Text = "3";
                        this.form.SHIHARAI_ZEI_KBN_NAME.Text = "非課税";
                    }
                    else
                    {
                        this.form.SHIHARAI_ZEI_KBN_CD.Text = "";
                        this.form.SHIHARAI_ZEI_KBN_NAME.Text = "";
                    }
                    break;

                default:
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        private string gyoushaHintText = "全角20文字/半角40文字以内で入力してください";

        /// <summary>
        /// 業者チェック
        /// </summary>
        internal bool CheckGyousha(out bool catchErr)
        {
            catchErr = true;
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                bool isError = false;

                var msgLogic = new MessageBoxShowLogic();
                var inputGyoushaCd = this.form.GYOUSHA_CD.Text;

                int rowindex = 0;
                int cellindex = 0;
                bool isChageCurrentCell = false;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if (this.form.isInputError || (String.IsNullOrEmpty(inputGyoushaCd) || !this.tmpGyoushaCd.Equals(inputGyoushaCd) ||
                    (this.tmpGyoushaCd.Equals(inputGyoushaCd) && string.IsNullOrEmpty(this.form.GYOUSHA_NAME_RYAKU.Text)))
                    || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.isInputError = false;
                    this.form.GYOUSHA_NAME_RYAKU.Text = String.Empty;
                    this.form.GYOUSHA_NAME_RYAKU.ReadOnly = true;
                    this.form.GYOUSHA_NAME_RYAKU.Tag = String.Empty;
                    this.form.GYOUSHA_NAME_RYAKU.TabStop = false;
                    if (!this.tmpGyoushaCd.Equals(inputGyoushaCd))
                    {
                        this.form.GENBA_CD.Text = String.Empty;
                        this.form.GENBA_NAME_RYAKU.Text = String.Empty;
                        this.form.GENBA_NAME_RYAKU.ReadOnly = true;
                        this.form.GENBA_NAME_RYAKU.Tag = String.Empty;
                        this.form.GENBA_NAME_RYAKU.TabStop = false;
                    }

                    if (String.IsNullOrEmpty(inputGyoushaCd))
                    {
                        // 同時に現場コードもクリア
                        this.form.GENBA_CD.Text = String.Empty;
                        this.form.GENBA_NAME_RYAKU.Text = String.Empty;
                        this.form.GENBA_NAME_RYAKU.ReadOnly = true;
                        this.form.GENBA_NAME_RYAKU.Tag = String.Empty;
                        this.form.GENBA_NAME_RYAKU.TabStop = false;
                        strGenbaName = string.Empty;

                        if (!this.form.oldShokuchiKbn || this.form.keyEventArgs.Shift)
                        {
                            // フレームワーク側の再フォーカス処理を行わない
                            ret = false;
                        }
                        else
                        {
                            // フレームワーク側の再フォーカス処理を行う
                            ret = true;
                        }
                        /*ThangNguyen 20200219 #134056, #134060 Start*/
                        if (this.headerForm.KIHON_KEIRYOU.Text.Equals("2"))
                        {
                            this.form.SBN_GYOUSHA_CD.Text = string.Empty;
                            this.form.SBN_GYOUSHA_NAME.Text = string.Empty;
                            this.form.SBN_GENBA_CD.Text = string.Empty;
                            this.form.SBN_GENBA_NAME.Text = string.Empty;
                        }
                        /*ThangNguyen 20200219 #134056, #134060 End*/
                    }
                    else
                    {
                        catchErr = true;
                        var retData = this.accessor.GetGyousha(inputGyoushaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                        if (!catchErr)
                        {
                            return false;
                        }
                        var gyoushaEntity = retData;
                        if (null == gyoushaEntity)
                        {
                            this.form.isInputError = true;
                            // エラーメッセージ
                            msgLogic.MessageBoxShow("E020", "業者");
                            this.form.GYOUSHA_CD.Focus();
                            isError = true;
                            ret = false;
                        }
                        else if (this.headerForm.KIHON_KEIRYOU.Text == "1" && !gyoushaEntity.GYOUSHAKBN_UKEIRE.IsTrue
                                 || this.headerForm.KIHON_KEIRYOU.Text == "2" && !gyoushaEntity.GYOUSHAKBN_SHUKKA.IsTrue)
                        {
                            this.form.isInputError = true;
                            // エラーメッセージ
                            msgLogic.MessageBoxShow("E020", "業者");
                            this.form.GYOUSHA_CD.Focus();
                            isError = true;
                            ret = false;
                        }
                        else
                        {
                            // 業者名
                            this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;

                            // 諸口区分チェック
                            if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                            {
                                // 業者名編集可
                                this.form.GYOUSHA_NAME_RYAKU.ReadOnly = false;
                                this.form.GYOUSHA_NAME_RYAKU.TabStop = GetTabStop("GYOUSHA_NAME_RYAKU");
                                this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME1;
                                this.form.GYOUSHA_NAME_RYAKU.Tag = this.gyoushaHintText;
                                this.form.GYOUSHA_NAME_RYAKU.Focus();

                                ret = false;
                            }
                            else
                            {
                                if (!this.form.oldShokuchiKbn)
                                {
                                    ret = false;
                                }

                            }

                            // 取引先を取得
                            catchErr = true;
                            var torihikisakiEntity = this.accessor.GetTorihikisaki(gyoushaEntity.TORIHIKISAKI_CD, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                            if (!catchErr)
                            {
                                return false;
                            }

                            if (null != torihikisakiEntity)
                            {
                                this.form.TORIHIKISAKI_CD.Text = gyoushaEntity.TORIHIKISAKI_CD;
                                // 取引先チェック呼び出し
                                catchErr = true;
                                ret = this.CheckTorihikisaki(out catchErr);
                                if (!catchErr)
                                {
                                    return false;
                                }
                            }

                            if (true == ret)
                            {
                                // 現場が入力されていれば現場との関連チェック
                                var genbaCd = this.form.GENBA_CD.Text;
                                if (!String.IsNullOrEmpty(genbaCd))
                                {
                                    var genbaEntityList = this.accessor.GetGenbaByGyousha(inputGyoushaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                                    var genbaEntity = genbaEntityList.Where(g => g.GENBA_CD == genbaCd).FirstOrDefault();
                                    if (null != genbaEntity)
                                    {
                                        catchErr = true;
                                        ret = this.CheckGenba(out catchErr);
                                        if (!catchErr)
                                        {
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        // 一致するものがなければ、入力されている現場を消す
                                        this.form.GENBA_CD.Text = String.Empty;
                                        this.form.GENBA_NAME_RYAKU.Text = String.Empty;
                                    }
                                }
                            }
                            // 諸口区分チェック
                            this.form.isSetShokuchiForcus = false;
                            if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                            {
                                // 現場を再設定
                                this.form.GYOUSHA_NAME_RYAKU.ReadOnly = false;
                                this.form.GYOUSHA_NAME_RYAKU.TabStop = GetTabStop("GYOUSHA_NAME_RYAKU");
                                this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME1;
                                this.form.GYOUSHA_NAME_RYAKU.Tag = this.gyoushaHintText;
                                this.form.GYOUSHA_NAME_RYAKU.Focus();
                                this.form.isSetShokuchiForcus = true;
                            }
                            /*ThangNguyen 20200219 #134056, #134060 Start*/
                            if (this.headerForm.KIHON_KEIRYOU.Text.Equals("2"))
                            {
                                this.SetSbnGyoushaByGyousha(this.form.GYOUSHA_CD.Text);
                            }
                            else
                            {
                                this.SetHstGyoushaByGyousha(this.form.GYOUSHA_CD.Text);
                            }
                            /*ThangNguyen 20200219 #134056, #134060 End*/
                        }
                    }

                    if (!isError)
                    {
                        if (!this.beforeGyoushaCd.Equals(inputGyoushaCd) && this.form.validateFlag)
                        {
                            bool flag = false;
                            foreach (Row row in this.form.gcMultiRow1.Rows)
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_HINMEI_CD].Value)))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (!this.hasShow && this.form.gcMultiRow1.Rows.Count > 1 && flag)
                            {
                                // currentCellが単価再読み込みや、再計算の対象だった場合、
                                // ポップアップが上がった後にCurrentCellがEditModeになってしまう問題の対策。
                                if (this.form.gcMultiRow1.CurrentCell != null
                                    && (this.form.gcMultiRow1.CurrentCell.Name.Equals(LogicClass.CELL_NAME_TANKA)
                                    || this.form.gcMultiRow1.CurrentCell.Name.Equals(LogicClass.CELL_NAME_KINGAKU)))
                                {
                                    rowindex = this.form.gcMultiRow1.CurrentRow.Index;
                                    cellindex = this.form.gcMultiRow1.CurrentCell.CellIndex;
                                    this.form.gcMultiRow1.CurrentCell = null;
                                    isChageCurrentCell = true;
                                }

                                msgLogic = new MessageBoxShowLogic();
                                DialogResult dr = msgLogic.MessageBoxShow("C097", "業者");
                                if (dr == DialogResult.OK || dr == DialogResult.Yes)
                                {
                                    this.form.gcMultiRow1.Rows.Where(r => !r.IsNewRow).ToList().ForEach(r => this.GetHinmeiForPop(r));
                                }
                            }
                        }

                        if (!this.tmpGyoushaCd.Equals(inputGyoushaCd))
                        {
                            // 営業担当者の設定
                            this.SetEigyouTantousha(this.form.GENBA_CD.Text, this.form.GYOUSHA_CD.Text, this.form.TORIHIKISAKI_CD.Text);
                        }

                        if (!this.tmpGyoushaCd.Equals(inputGyoushaCd))
                        {
                            // 明細行すべての単価を再設定
                            var list = this.form.gcMultiRow1.Rows.Where(r => !r.IsNewRow).ToList();

                            foreach (Row dr in list)
                            {
                                if (!this.SearchAndCalcForUnit(false, dr))
                                {
                                    return false;
                                }
                            }

                            // 合計金額の再計算
                            if (!this.CalcTotalValues())
                            {
                                return false;
                            }
                        }

                        if (this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly == false && this.form.GYOUSHA_CD.Text != "" && this.form.GYOUSHA_NAME_RYAKU.Text != "")
                        {
                        }
                    }
                }
                else
                {
                    ret = false;
                }

                if (isChageCurrentCell)
                {
                    this.form.gcMultiRow1.CurrentCell = this.form.gcMultiRow1.Rows[rowindex].Cells[cellindex];
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckGyousha", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                this.form.isInputError = true;
                catchErr = false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("CheckGyousha", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                this.form.isInputError = true;
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;

        }

        private string genbaHintText = "全角20文字/半角40文字以内で入力してください";

        /// <summary>
        /// 現場チェック
        /// </summary>
        internal bool CheckGenba(out bool catchErr)
        {
            bool ret = true;
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart();


                bool isError = false;

                var msgLogic = new MessageBoxShowLogic();
                var inputGenbaCd = this.form.GENBA_CD.Text;
                var inputGyoushaCd = this.form.GYOUSHA_CD.Text;
                bool isContinue = false;

                int rowindex = 0;
                int cellindex = 0;
                bool isChageCurrentCell = false;

                catchErr = true;
                var retData = this.accessor.GetGyousha(this.form.GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (!catchErr)
                {
                    ret = false;
                    return ret;
                }
                var gyoushaEntity = retData;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(inputGenbaCd) || !this.tmpGenbaCd.Equals(inputGenbaCd))
                    || this.form.isFromSearchButton || this.form.isInputError)
                {
                    // 初期化
                    this.form.isInputError = false;
                    this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                    this.form.GENBA_NAME_RYAKU.ReadOnly = true;
                    this.form.GENBA_NAME_RYAKU.Tag = string.Empty;
                    this.form.GENBA_NAME_RYAKU.TabStop = false;

                    if (String.IsNullOrEmpty(inputGenbaCd))
                    {
                        if (!this.form.oldShokuchiKbn || this.form.keyEventArgs.Shift)
                        {
                            // フレームワーク側の再フォーカス処理を行わない
                            ret = false;
                        }
                        else
                        {
                            // フレームワーク側の再フォーカス処理を行う
                            ret = true;
                        }
                        /*ThangNguyen 20200219 #134056, #134060 Start*/
                        if (this.headerForm.KIHON_KEIRYOU.Text.Equals("2"))
                        {
                            this.form.HST_GENBA_CD.Text = string.Empty;
                            this.form.HST_GENBA_NAME.Text = string.Empty;
                        }
                        /*ThangNguyen 20200219 #134056, #134060 End*/
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(inputGyoushaCd))
                        {
                            msgLogic.MessageBoxShow("E051", "業者");
                            this.form.GENBA_CD.Text = string.Empty;
                            this.form.GENBA_CD.Focus();
                            isError = true;
                            ret = false;
                            return ret;
                        }
                        var genbaEntityList = this.accessor.GetGenbaList(inputGyoushaCd, inputGenbaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                        if (genbaEntityList == null || genbaEntityList.Length < 1)
                        {
                            // エラーメッセージ
                            this.form.isInputError = true;
                            msgLogic.MessageBoxShow("E020", "現場");
                            this.form.GENBA_CD.Focus();
                            isError = true;
                            ret = false;
                        }
                        else
                        {
                            M_GENBA genba = new M_GENBA();

                            genba = genbaEntityList[0];
                            this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;
                            strGenbaName = genba.GENBA_NAME1 + genba.GENBA_NAME2;

                            if (null == gyoushaEntity)
                            {
                                ret = false;
                            }
                            // 業者の諸口区分チェック
                            else if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                            {
                                // 業者名編集可
                                this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME1;
                                this.form.GYOUSHA_NAME_RYAKU.ReadOnly = false;
                                this.form.GYOUSHA_NAME_RYAKU.TabStop = GetTabStop("GYOUSHA_NAME_RYAKU");
                                this.form.GYOUSHA_NAME_RYAKU.Tag = this.gyoushaHintText;
                            }
                            else
                            {
                                this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                            }

                            // 取引先を取得
                            M_TORIHIKISAKI torihikisakiEntity = null;
                            if (!string.IsNullOrEmpty(genba.TORIHIKISAKI_CD))
                            {

                                r_framework.Dao.IM_TORIHIKISAKIDao torihikisakiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKIDao>();
                                var keyEntity = new M_TORIHIKISAKI();
                                keyEntity.TORIHIKISAKI_CD = genba.TORIHIKISAKI_CD;
                                torihikisakiEntity = torihikisakiDao.GetAllValidData(keyEntity).FirstOrDefault();

                                if (torihikisakiEntity != null)
                                {
                                    // 取引先設定
                                    this.form.TORIHIKISAKI_CD.Text = torihikisakiEntity.TORIHIKISAKI_CD;
                                    pressedEnterOrTab = false;
                                    catchErr = true;
                                    ret = this.CheckTorihikisaki(out catchErr);
                                    if (!catchErr)
                                    {
                                        ret = false;
                                        return ret;
                                    }
                                }
                            }

                            // TODO: 【2次】営業担当者チェックの呼び出し

                            // 現場：諸口区分チェック
                            if (genba.SHOKUCHI_KBN.IsTrue)
                            {
                                // 現場名編集可
                                this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME1;
                                this.form.GENBA_NAME_RYAKU.ReadOnly = false;
                                this.form.GENBA_NAME_RYAKU.TabStop = GetTabStop("GENBA_NAME_RYAKU");
                                this.form.GENBA_NAME_RYAKU.Tag = genbaHintText;
                                this.form.GENBA_NAME_RYAKU.Focus();
                            }

                            //// Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                            if (ret)
                                this.MoveToNextControlForShokuchikbnCheck(this.form.GENBA_CD);

                            ret = false;

                            // マニ種類の自動表示
                            // 初期化
                            this.form.MANIFEST_SHURUI_CD.Text = string.Empty;
                            this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = string.Empty;

                            if (!genba.MANIFEST_SHURUI_CD.IsNull)
                            {
                                var manifestShuruiEntity = this.manifestShuruiList.Where(t => t.MANIFEST_SHURUI_CD.ToString() == genba.MANIFEST_SHURUI_CD.ToString()).FirstOrDefault();
                                if (manifestShuruiEntity != null && !string.IsNullOrEmpty(manifestShuruiEntity.MANIFEST_SHURUI_NAME_RYAKU))
                                {
                                    this.form.MANIFEST_SHURUI_CD.Text = Convert.ToString(genba.MANIFEST_SHURUI_CD);
                                    this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = manifestShuruiEntity.MANIFEST_SHURUI_NAME_RYAKU;
                                }
                            }

                            // マニ手配の自動表示
                            // 初期化
                            this.form.MANIFEST_TEHAI_CD.Text = string.Empty;
                            this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = string.Empty;

                            if (!genba.MANIFEST_TEHAI_CD.IsNull)
                            {
                                var manifestTehaiEntity = this.manifestTehaiList.Where(t => t.MANIFEST_TEHAI_CD.ToString() == genba.MANIFEST_TEHAI_CD.ToString()).FirstOrDefault();
                                if (manifestTehaiEntity != null && !string.IsNullOrEmpty(manifestTehaiEntity.MANIFEST_TEHAI_NAME_RYAKU))
                                {
                                    this.form.MANIFEST_TEHAI_CD.Text = Convert.ToString(genba.MANIFEST_TEHAI_CD);
                                    this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = manifestTehaiEntity.MANIFEST_TEHAI_NAME_RYAKU;
                                }
                            }
                            /*ThangNguyen 20200219 #134056, #134060 Start*/
                            if (this.headerForm.KIHON_KEIRYOU.Text.Equals("2"))
                            {
                                this.SetSbnGyoushaByGyousha(this.form.GYOUSHA_CD.Text);
                                this.SetSbnGenbaByGenba(this.form.SBN_GYOUSHA_CD.Text, this.form.GENBA_CD.Text);
                            }
                            else
                            {
                                this.SetHstGyoushaByGyousha(this.form.GYOUSHA_CD.Text);
                                this.SetHstGenbaByGenba(this.form.HST_GYOUSHA_CD.Text, this.form.GENBA_CD.Text);
                            }
                            /*ThangNguyen 20200219 #134056, #134060 End*/
                        }
                    }

                    if (!isError)
                    {
                        if (!this.tmpGenbaCd.Equals(inputGenbaCd) && this.form.validateFlag)
                        {
                            bool flag = false;
                            foreach (Row row in this.form.gcMultiRow1.Rows)
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_HINMEI_CD].Value)))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (this.form.gcMultiRow1.Rows.Count > 1 && flag)
                            {
                                // currentCellが単価再読み込みや、再計算の対象だった場合、
                                // ポップアップが上がった後にCurrentCellがEditModeになってしまう問題の対策。
                                if (this.form.gcMultiRow1.CurrentCell != null
                                    && (this.form.gcMultiRow1.CurrentCell.Name.Equals(LogicClass.CELL_NAME_TANKA)
                                    || this.form.gcMultiRow1.CurrentCell.Name.Equals(LogicClass.CELL_NAME_KINGAKU)))
                                {
                                    rowindex = this.form.gcMultiRow1.CurrentRow.Index;
                                    cellindex = this.form.gcMultiRow1.CurrentCell.CellIndex;
                                    this.form.gcMultiRow1.CurrentCell = null;
                                    isChageCurrentCell = true;
                                }

                                msgLogic = new MessageBoxShowLogic();
                                DialogResult dr = msgLogic.MessageBoxShow("C097", "現場");
                                if (dr == DialogResult.OK || dr == DialogResult.Yes)
                                {
                                    this.form.gcMultiRow1.Rows.Where(r => !r.IsNewRow).ToList().ForEach(r => this.GetHinmeiForPop(r));
                                }
                                this.hasShow = true;
                            }
                        }
                        if (!this.tmpGenbaCd.Equals(inputGenbaCd))
                        {
                            // 営業担当者の設定
                            this.SetEigyouTantousha(this.form.GENBA_CD.Text, this.form.GYOUSHA_CD.Text, this.form.TORIHIKISAKI_CD.Text);
                        }

                        if (!this.tmpGenbaCd.Equals(inputGenbaCd))
                        {
                            // 明細行すべての単価を再設定
                            var list = this.form.gcMultiRow1.Rows.Where(r => !r.IsNewRow).ToList();

                            foreach (Row dr in list)
                            {
                                if (!this.SearchAndCalcForUnit(false, dr))
                                {
                                    return false;
                                }
                            }

                            // 合計金額の再計算
                            if (!this.CalcTotalValues())
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    ret = false;
                }

                if (isChageCurrentCell)
                {
                    this.form.gcMultiRow1.CurrentCell = this.form.gcMultiRow1.Rows[rowindex].Cells[cellindex];
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckGenba", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                this.form.isInputError = true;
                catchErr = false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("CheckGenba", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                    this.form.isInputError = true;
                }
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }

        /// <summary>
        /// 営業担当者チェック
        /// </summary>
        internal bool CheckEigyouTantousha()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.EIGYOU_TANTOUSHA_CD.Text))
                {
                    // 営業担当者CDがなければ既にエラーが表示されているので何もしない。
                    return true;
                }

                var shainEntity = this.accessor.GetShain(this.form.EIGYOU_TANTOUSHA_CD.Text, true);
                if (shainEntity == null)
                {
                    return true;
                }
                else if (shainEntity.EIGYOU_TANTOU_KBN.Equals(SqlBoolean.False))
                {
                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "営業担当者");
                    this.form.EIGYOU_TANTOUSHA_CD.Focus();
                }
                else
                {
                    this.form.EIGYOU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckEigyouTantousha", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckEigyouTantousha", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        internal string tmpTorihikisakiCd = string.Empty;
        internal string tmpGyoushaCd = string.Empty;
        internal string tmpGenbaCd = string.Empty;
        internal string tmpHstGyoushaCd = string.Empty;
        internal string tmpHstGenbaCd = string.Empty;
        internal string tmpUpnGyoushaCd = string.Empty;
        internal string tmpSbnGyoushaCd = string.Empty;
        internal string tmpSbnGenbaCd = string.Empty;
        internal string tmpLastSbnGyoushaCd = string.Empty;
        internal string tmpLastSbnGenbaCd = string.Empty;
        internal string tmpNioroshiGyoushaCd = string.Empty;
        internal string tmpNioroshiGenbaCd = string.Empty;
        internal string tmpNizumiGyoushaCd = string.Empty;
        internal string tmpNizumiGenbaCd = string.Empty;
        internal string tmpUnpanGyoushaCd = string.Empty;
        internal string tmpKeitaiKbnCd = string.Empty;
        internal string tmpUntenshaCd = string.Empty;
        private string sharyouCd = string.Empty;
        private string shaShuCd = string.Empty;
        private string unpanGyousha = string.Empty;
        private Color sharyouCdBackColor = Color.FromArgb(255, 235, 160);
        private Color sharyouCdBackColorBlue = Color.FromArgb(0, 255, 255);
        internal string searchSendParamKeyNameForSharyouCd = "key002";
        private string sharyouHinttext = "全角10文字/半角20文字以内で入力してください";
        internal string tmpDenpyouDate = string.Empty;
        internal string beforeGyoushaCd = string.Empty;

        /// <summary>
        /// 取引先CD初期セット
        /// </summary>
        internal void TorihikisakiCdSet()
        {
            tmpTorihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
        }

        /// <summary>
        /// 業者CD初期セット
        /// </summary>
        internal void EnterGyoushaCdSet()
        {
            beforeGyoushaCd = this.form.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 業者CD初期セット
        /// </summary>
        internal void GyoushaCdSet()
        {
            tmpGyoushaCd = this.form.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 排出事業者CD初期セット
        /// </summary>
        internal void HstGyoushaCdSet()
        {
            tmpHstGyoushaCd = this.form.HST_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 排出事業場CD初期セット
        /// </summary>
        internal void HstGenbaCdSet()
        {
            tmpHstGenbaCd = this.form.HST_GENBA_CD.Text;
        }

        /// <summary>
        /// 処分業者CD初期セット
        /// </summary>
        internal void SbnGyoushaCdSet()
        {
            tmpSbnGyoushaCd = this.form.SBN_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 処分事業場CD初期セット
        /// </summary>
        internal void SbnGenbaCdSet()
        {
            tmpSbnGenbaCd = this.form.SBN_GENBA_CD.Text;
        }

        /// <summary>
        /// 最終処分業者CD初期セット
        /// </summary>
        internal void LastSbnGyoushaCdSet()
        {
            tmpLastSbnGyoushaCd = this.form.LAST_SBN_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 最終処分事業場CD初期セット
        /// </summary>
        internal void LastSbnGenbaCdSet()
        {
            tmpLastSbnGenbaCd = this.form.LAST_SBN_GENBA_CD.Text;
        }

        /// <summary>
        /// 荷降業者CD初期セット
        /// </summary>
        internal void NioroshiGyoushaCdSet()
        {
            tmpNioroshiGyoushaCd = this.form.NIOROSHI_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 荷降現場CD初期セット
        /// </summary>
        internal void NioroshiGenbaCdSet()
        {
            tmpNioroshiGenbaCd = this.form.NIOROSHI_GENBA_CD.Text;
        }

        /// <summary>
        /// 荷積業者CD初期セット
        /// </summary>
        internal void NizumiGyoushaCdSet()
        {
            tmpNizumiGyoushaCd = this.form.NIZUMI_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 荷積現場CD初期セット
        /// </summary>
        internal void NizumiGenbaCdSet()
        {
            tmpNizumiGenbaCd = this.form.NIZUMI_GENBA_CD.Text;
        }

        /// <summary>
        /// 運搬業者CD初期セット
        /// </summary>
        internal void UnpanGyoushaCdSet()
        {
            tmpUnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 現場CD初期セット
        /// </summary>
        internal void GenbaCdSet()
        {
            tmpGenbaCd = this.form.GENBA_CD.Text;
        }

        /// <summary>
        /// 車輌CD初期セット
        /// </summary>
        internal void ShayouCdSet()
        {
            sharyouCd = this.form.SHARYOU_CD.Text;
        }

        /// <summary>
        /// 車種Cd初期セット
        /// </summary>
        internal void ShashuCdSet()
        {
            shaShuCd = this.form.SHASHU_CD.Text;
        }

        /// <summary>
        /// 運転者CD初期セット
        /// </summary>
        internal void UntenshaCdSet()
        {
            tmpUntenshaCd = this.form.UNTENSHA_CD.Text;
        }

        /// <summary>
        /// 形態区分CD初期セット
        /// </summary>
        internal void KeitaiKbnCdSet()
        {
            tmpKeitaiKbnCd = this.form.KEITAI_KBN_CD.Text;
        }

        /// <summary>
        /// 伝票日付初期セット
        /// </summary>
        internal void DenpyouDateSet()
        {
            tmpDenpyouDate = this.form.DENPYOU_DATE.Text;
        }

        public void SHARYOU_CD_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
            {
                this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                this.form.SHARYOU_EMPTY_JYUURYOU.Text = string.Empty;
                this.form.isSelectingSharyouCd = false;
                this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
                return;
            }
        }

        /// <summary>
        /// 車輌チェック
        /// </summary>
        internal bool CheckSharyou(bool tairyuuFlg = false)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(tairyuuFlg);

                M_SHARYOU[] sharyouEntitys = null;

                // 何もしないとポップアップが起動されてしまう可能性があるため
                // 変更されたかチェックする
                if (sharyouCd.Equals(this.form.SHARYOU_CD.Text))
                {
                    // 複数ヒットするCDを入力→ポップアップで何もしない→一度ポップアップを閉じて再度ポップアップからデータを選択
                    // したときに色が戻らない問題の対策のため、存在チェックだけは実施する。
                    sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null, SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()), this.headerForm.KIHON_KEIRYOU.Text);
                    if (sharyouEntitys != null && sharyouEntitys.Length == 1)
                    {
                        // 一意に識別できる場合は色を戻す
                        this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                        this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
                        this.form.oldSharyouShokuchiKbn = false;
                        this.form.SHARYOU_NAME_RYAKU.Tag = string.Empty;
                        this.form.SHARYOU_NAME_RYAKU.TabStop = false;
                        this.form.SHARYOU_CD.AutoChangeBackColorEnabled = true;
                    }
                    return true;
                }

                // 初期化
                this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                if (string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                    this.form.SHARYOU_EMPTY_JYUURYOU.Text = string.Empty;
                }
                this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
                this.form.oldSharyouShokuchiKbn = false;
                this.form.SHARYOU_NAME_RYAKU.Tag = string.Empty;
                this.form.SHARYOU_NAME_RYAKU.TabStop = false;
                this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                this.form.SHARYOU_CD.AutoChangeBackColorEnabled = true;

                if (string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    sharyouCd = string.Empty;
                    this.form.isSelectingSharyouCd = false;
                    return true;
                }

                this.sharyouCd = this.form.SHARYOU_CD.Text;
                this.unpanGyousha = this.form.UNPAN_GYOUSHA_CD.Text;

                //sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null, SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()), this.headerForm.KIHON_KEIRYOU.Text);
                sharyouEntitys = this.accessor.GetSharyouMod(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null);
                //this.form.EMPTY_JYUURYOU.Text = string.Empty;
                bool catchErr = true;
                this.form.SHARYOU_EMPTY_JYUURYOU.Text = string.Empty;

                // マスタ存在チェック
                if (sharyouEntitys == null || sharyouEntitys.Length < 1)
                {
                    // 車輌名を編集可
                    this.ChangeShokuchiSharyouDesign();
                    // マスタに存在しない場合、ユーザに車輌名を自由入力させる
                    if (string.IsNullOrEmpty(this.form.SHARYOU_NAME_RYAKU.Text) || this.form.SHARYOU_CD.Text != this.dto.entryEntity.SHARYOU_CD)
                    {
                        this.form.SHARYOU_CD.Text = this.form.SHARYOU_CD.Text.PadLeft(6, '0');
                        this.form.SHARYOU_NAME_RYAKU.Text = ZeroSuppress(this.form.SHARYOU_CD);
                    }
                    this.MoveToNextControlForShokuchikbnCheck(this.form.SHARYOU_CD);

                    //if (!this.form.isSelectingSharyouCd)
                    //{
                    //    this.form.isSelectingSharyouCd = true;
                    //    return true;
                    //}
                    return true;
                }
                else
                {
                    this.form.oldSharyouShokuchiKbn = false;
                }

                catchErr = true;
                var retData = this.accessor.GetGyousha(this.form.UNPAN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                // ポップアップから戻ってきたときに運搬業者名が無いため取得
                M_GYOUSHA unpanGyousha = retData;
                if (unpanGyousha != null)
                {
                    this.form.UNPAN_GYOUSHA_NAME.Text = unpanGyousha.GYOUSHA_NAME_RYAKU;
                }

                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_NAME.Text))
                {
                    M_SHARYOU sharyou = new M_SHARYOU();

                    // 運搬業者チェック
                    if (sharyouEntitys.Length == 1)
                    {
                        bool isCheck = false;
                        foreach (M_SHARYOU sharyouEntity in sharyouEntitys)
                        {
                            if (sharyouEntity.GYOUSHA_CD.Equals(this.form.UNPAN_GYOUSHA_CD.Text))
                            {
                                isCheck = true;
                                sharyou = sharyouEntity;
                                // 諸口区分チェック
                                if (unpanGyousha != null)
                                {
                                    if (unpanGyousha.SHOKUCHI_KBN.IsTrue)
                                    {
                                        // 運搬業者名編集可
                                        this.form.UNPAN_GYOUSHA_NAME.ReadOnly = false;
                                        this.form.UNPAN_GYOUSHA_NAME.TabStop = GetTabStop("UNPAN_GYOUSHA_NAME");
                                        this.form.UNPAN_GYOUSHA_NAME.Tag = this.unpanGyoushaHintText;
                                    }
                                }
                                break;
                            }
                        }

                        if (isCheck)
                        {
                            // 車輌データセット
                            SetSharyou(sharyou, tairyuuFlg);
                            return true;
                        }
                        else
                        {
                            // エラーメッセージ
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E062", "運搬業者");
                            this.form.SHARYOU_CD.Focus();
                            return true;
                        }
                    }
                    else if (sharyouEntitys.Length > 1)
                    {
                        // 複数レコード
                        // 車輌名を編集可
                        this.form.oldSharyouShokuchiKbn = true;
                        this.form.SHARYOU_NAME_RYAKU.ReadOnly = false;
                        this.form.SHARYOU_NAME_RYAKU.TabStop = GetTabStop("SHARYOU_NAME_RYAKU");
                        this.form.SHARYOU_NAME_RYAKU.Tag = this.sharyouHinttext;
                        // 自由入力可能であるため車輌名の色を変更
                        this.form.SHARYOU_CD.AutoChangeBackColorEnabled = false;
                        this.form.SHARYOU_CD.BackColor = sharyouCdBackColorBlue;

                        sharyouCd = string.Empty;
                        this.unpanGyousha = string.Empty;
                        this.form.isSelectingSharyouCd = true;
                        this.form.SHARYOU_CD.Focus();

                        this.form.FocusOutErrorFlag = true;

                        // この時は車輌CDを検索条件に含める
                        this.PopUpConditionsSharyouSwitch(true);

                        // 検索ポップアップ起動
                        CustomControlExtLogic.PopUp(this.form.SHARYOU_CD);
                        this.PopUpConditionsSharyouSwitch(false);

                        // PopUpでF12押下された場合
                        //（戻り値でF12が押下されたか判断できない為、運搬業者の有無で判断）
                        // マスタに存在しない場合、ユーザに車輌名を自由入力させる
                        this.ChangeShokuchiSharyouDesign();
                        if (string.IsNullOrEmpty(this.form.SHARYOU_NAME_RYAKU.Text) || this.form.SHARYOU_CD.Text != this.dto.entryEntity.SHARYOU_CD)
                        {
                            //NHU MOD 20180508 #104593 S
                            this.form.SHARYOU_CD.Text = this.form.SHARYOU_CD.Text.PadLeft(6, '0');
                            //NHU MOD 20180508 #104593 E
                            this.form.SHARYOU_NAME_RYAKU.Text = ZeroSuppress(this.form.SHARYOU_CD);
                        }

                        this.form.FocusOutErrorFlag = false;
                        return true;
                    }
                }
                else
                {
                    if (sharyouEntitys.Length > 1)
                    {
                        // 複数レコード
                        // 車輌名を編集可
                        this.form.oldSharyouShokuchiKbn = true;
                        this.form.SHARYOU_NAME_RYAKU.ReadOnly = false;
                        this.form.SHARYOU_NAME_RYAKU.TabStop = GetTabStop("SHARYOU_NAME_RYAKU");
                        this.form.SHARYOU_NAME_RYAKU.Tag = this.sharyouHinttext;
                        // 自由入力可能であるため車輌名の色を変更
                        this.form.SHARYOU_CD.AutoChangeBackColorEnabled = false;
                        this.form.SHARYOU_CD.BackColor = sharyouCdBackColorBlue;

                        if (!this.form.isSelectingSharyouCd)
                        {
                            this.sharyouCd = string.Empty;
                            this.unpanGyousha = string.Empty;
                            this.form.isSelectingSharyouCd = true;
                            this.form.SHARYOU_CD.Focus();

                            this.form.FocusOutErrorFlag = true;

                            // この時は車輌CDを検索条件に含める
                            this.PopUpConditionsSharyouSwitch(true);

                            // 検索ポップアップ起動
                            CustomControlExtLogic.PopUp(this.form.SHARYOU_CD);
                            this.PopUpConditionsSharyouSwitch(false);

                            // PopUpでF12押下された場合
                            //（戻り値でF12が押下されたか判断できない為、運搬業者の有無で判断）
                            if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                            {
                                // マスタに存在しない場合、ユーザに車輌名を自由入力させる
                                this.ChangeShokuchiSharyouDesign();
                                if (string.IsNullOrEmpty(this.form.SHARYOU_NAME_RYAKU.Text) || this.form.SHARYOU_CD.Text != this.dto.entryEntity.SHARYOU_CD)
                                {
                                    this.form.SHARYOU_CD.Text = this.form.SHARYOU_CD.Text.PadLeft(6, '0');
                                    this.form.SHARYOU_NAME_RYAKU.Text = ZeroSuppress(this.form.SHARYOU_CD);
                                }
                            }

                            this.form.FocusOutErrorFlag = false;
                            return true;
                        }
                        else
                        {
                            // ポップアアップから戻ってきて車輌名へ遷移した場合
                            // マスタに存在しない場合、ユーザに車輌名を自由入力させる
                            this.ChangeShokuchiSharyouDesign();
                            this.form.SHARYOU_CD.Text = this.form.SHARYOU_CD.Text.PadLeft(6, '0');
                            this.form.SHARYOU_NAME_RYAKU.Text = ZeroSuppress(this.form.SHARYOU_CD);
                        }
                    }
                    else
                    {
                        // 一意レコード
                        // 車輌データセット
                        SetSharyou(sharyouEntitys[0], tairyuuFlg);
                    }
                }

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckSharyou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckSharyou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 車輌PopUpの検索条件に車輌CDを含めるかを引数によって設定します
        /// </summary>
        /// <param name="isPopupConditionsSharyouCD"></param>
        internal void PopUpConditionsSharyouSwitch(bool isPopupConditionsSharyouCD)
        {
            PopupSearchSendParamDto sharyouParam = new PopupSearchSendParamDto();
            sharyouParam.And_Or = CONDITION_OPERATOR.AND;
            sharyouParam.Control = "SHARYOU_CD";
            sharyouParam.KeyName = "key002";

            if (isPopupConditionsSharyouCD)
            {
                if (!this.form.SHARYOU_CD.PopupSearchSendParams.Contains(sharyouParam))
                {
                    this.form.SHARYOU_CD.PopupSearchSendParams.Add(sharyouParam);
                }
            }
            else
            {
                var paramsCount = this.form.SHARYOU_CD.PopupSearchSendParams.Count;
                for (int i = 0; i < paramsCount; i++)
                {
                    if (this.form.SHARYOU_CD.PopupSearchSendParams[i].Control == "SHARYOU_CD" &&
                        this.form.SHARYOU_CD.PopupSearchSendParams[i].KeyName == "key002")
                    {
                        this.form.SHARYOU_CD.PopupSearchSendParams.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// 車輌情報をセット
        /// </summary>
        /// <param name="sharyouEntity"></param>
        private bool SetSharyou(M_SHARYOU sharyouEntity, bool tairyuuFlg)
        {
            this.form.SHARYOU_CD.Text = sharyouEntity.SHARYOU_CD;
            this.sharyouCd = sharyouEntity.SHARYOU_CD;

            this.form.SHARYOU_NAME_RYAKU.Text = sharyouEntity.SHARYOU_NAME_RYAKU;
            this.form.UNTENSHA_CD.Text = sharyouEntity.SHAIN_CD;
            this.form.SHASHU_CD.Text = sharyouEntity.SHASYU_CD;
            this.form.UNPAN_GYOUSHA_CD.Text = sharyouEntity.GYOUSHA_CD;

            bool catchErr = true;
            this.form.SHARYOU_EMPTY_JYUURYOU.Text = string.Empty;
            if (!sharyouEntity.KUUSHA_JYURYO.IsNull)
            {
                this.form.SHARYOU_EMPTY_JYUURYOU.Text = sharyouEntity.KUUSHA_JYURYO.ToString();
            }

            if (!tairyuuFlg && this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG
                && !string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text)
                && !string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text)
                && !string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
            {
                Int64 number = this.GetTairyuuNumberBySharyou();
                if (number != -1 && (this.form.gcMultiRow1.Rows.Count == 0 || this.form.gcMultiRow1.Rows[0].IsNewRow))
                {
                    var dia = this.msgLogic.MessageBoxShowConfirm("この車輌は、滞留状態の伝票がすでに登録されています。滞留伝票を読込ますか？");
                    tairyuuFlg = true;
                    if (dia == DialogResult.Yes)
                    {
                        this.ShowDenpyou(number);
                        return true;
                    }
                }
            }
            if (this.headerForm.KIHON_KEIRYOU.Text == "2" && !sharyouEntity.KUUSHA_JYURYO.IsNull && this.form.gcMultiRow1.Rows.Count > 0
                && string.IsNullOrEmpty(Convert.ToString(this.form.gcMultiRow1[0, CELL_NAME_EMPTY_JYUURYOU].Value)))
            {
                this.form.EMPTY_JYUURYOU.Text = sharyouEntity.KUUSHA_JYURYO.ToString();
                this.form.EMPTY_KEIRYOU_TIME.Text = this.GetDate(out catchErr);
                if (this.form.gcMultiRow1.Rows[0].IsNewRow)
                {
                    this.form.gcMultiRow1.Rows.Add();
                    this.NumberingRowNo();
                }
                this.form.gcMultiRow1[0, CELL_NAME_EMPTY_JYUURYOU].Value = sharyouEntity.KUUSHA_JYURYO.ToString();
                if (!string.IsNullOrEmpty(Convert.ToString(this.form.gcMultiRow1[0, CELL_NAME_STAK_JYUURYOU].Value))
                    && string.IsNullOrEmpty(Convert.ToString(this.form.gcMultiRow1[0, CELL_NAME_KEIRYOU_TIME].Value)))
                {
                    this.form.gcMultiRow1[0, CELL_NAME_KEIRYOU_TIME].Value = this.GetDate(out catchErr);
                }
            }

            // 運転者情報セット
            var untensha = this.accessor.GetShain(sharyouEntity.SHAIN_CD);
            if (untensha != null)
            {
                this.form.UNTENSHA_NAME.Text = untensha.SHAIN_NAME_RYAKU;
            }
            else
            {
                this.form.UNTENSHA_NAME.Text = string.Empty;
            }

            //車種情報セット
            var shashu = this.accessor.GetShashu(sharyouEntity.SHASYU_CD);
            if (shashu != null)
            {
                this.form.SHASHU_CD.Text = shashu.SHASHU_CD;
                this.form.SHASHU_NAME.Text = shashu.SHASHU_NAME_RYAKU;
            }
            else
            {
                this.form.SHASHU_CD.Text = string.Empty;
                this.form.SHASHU_NAME.Text = string.Empty;
            }

            this.MoveToNextControlForShokuchikbnCheck(this.form.SHARYOU_CD);

            // 運搬業者名セット
            catchErr = true;
            bool retCheck = this.CheckUnpanGyoushaCd(out catchErr, tairyuuFlg);
            if (!catchErr)
            {
                throw new Exception("");
            }
            if (sharyouEntity.KEIRYOU_GYOUSHA_SET_KBN.IsTrue && this.form.GYOUSHA_CD.Text != sharyouEntity.GYOUSHA_CD)
            {
                this.form.GYOUSHA_CD.Text = sharyouEntity.GYOUSHA_CD;
                // 業者名セット
                catchErr = true;
                retCheck = this.CheckGyousha(out catchErr);
                if (!catchErr)
                {
                    throw new Exception("");
                }
            }
            if (!retCheck)
            {
                return false;
            }
            this.form.isSelectingSharyouCd = false;

            return true;
        }

        /// <summary>
        /// 形態区分選択ポップアップ用DataSource生成
        /// デザイナのプロパティ設定からでは絞り込み条件が作れないため、
        /// DataSourceを渡す方法でポップアップを表示する。
        /// </summary>
        /// <returns></returns>
        internal DataTable CreateKeitaiKbnPopupDataSource()
        {
            var allKeitaiKbn = DaoInitUtility.GetComponent<IM_KEITAI_KBNDao>().GetAllValidData(new M_KEITAI_KBN());
            var dt = EntityUtility.EntityToDataTable(allKeitaiKbn);

            if (dt.Rows.Count == 0)
            {
                return dt;
            }

            var sortedDt = new DataTable();
            sortedDt.Columns.Add(dt.Columns["KEITAI_KBN_CD"].ColumnName, dt.Columns["KEITAI_KBN_CD"].DataType);
            sortedDt.Columns.Add(dt.Columns["KEITAI_KBN_NAME_RYAKU"].ColumnName, dt.Columns["KEITAI_KBN_NAME_RYAKU"].DataType);

            string denshuKbn = "1";
            if (this.form.selectDenshuKbnCd == DENSHU_KBN.UKEIRE)
            {
                denshuKbn = "1";
            }
            else
            {
                denshuKbn = "2";
            }
            foreach (DataRow r in dt.Rows)
            {
                if (Convert.ToString(r["DENSHU_KBN_CD"]) == denshuKbn
                        || Convert.ToString(r["DENSHU_KBN_CD"]) == "9")
                {
                    sortedDt.Rows.Add(sortedDt.Columns.OfType<DataColumn>().Select(s => r[s.ColumnName]).ToArray());
                }
            }

            return sortedDt;
        }

        /// <summary>
        /// 形態区分チェック処理
        /// </summary>
        internal bool CheckKeitaiKbn()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text) || !this.tmpKeitaiKbnCd.Equals(this.form.KEITAI_KBN_CD.Text)) || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.KEITAI_KBN_NAME_RYAKU.Text = string.Empty;

                    if (string.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text))
                    {
                        return true;
                    }

                    short keitaiKbnCd;

                    if (!short.TryParse(this.form.KEITAI_KBN_CD.Text, out keitaiKbnCd))
                    {
                        return true;
                    }

                    M_KEITAI_KBN kakuteiKbn = this.accessor.GetkeitaiKbn(keitaiKbnCd);
                    if (kakuteiKbn == null)
                    {
                        // エラーメッセージ
                        this.form.KEITAI_KBN_CD.IsInputErrorOccured = true;
                        this.form.KEITAI_KBN_CD.BackColor = Constans.ERROR_COLOR;
                        this.form.KEITAI_KBN_CD.ForeColor = Constans.ERROR_COLOR_FORE;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "形態区分");
                        this.form.KEITAI_KBN_CD.Focus();
                        tmpKeitaiKbnCd = string.Empty;
                        return true; ;
                    }

                    var denshuKbnCd = (DENSHU_KBN)Enum.ToObject(typeof(DENSHU_KBN), (int)kakuteiKbn.DENSHU_KBN_CD);

                    if (denshuKbnCd == this.form.selectDenshuKbnCd || denshuKbnCd == DENSHU_KBN.KYOUTSUU)
                    {
                        this.form.KEITAI_KBN_NAME_RYAKU.Text = kakuteiKbn.KEITAI_KBN_NAME_RYAKU;
                    }
                    else
                    {
                        // エラーメッセージ
                        this.form.KEITAI_KBN_CD.IsInputErrorOccured = true;
                        this.form.KEITAI_KBN_CD.BackColor = Constans.ERROR_COLOR;
                        this.form.KEITAI_KBN_CD.ForeColor = Constans.ERROR_COLOR_FORE;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "形態区分");
                        this.form.KEITAI_KBN_CD.Focus();
                        tmpKeitaiKbnCd = string.Empty;
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckKeitaiKbn", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 台貫区分チェック
        /// </summary>
        internal bool CheckDaikanKbn()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.DAIKAN_KBN_NAME.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.DAIKAN_KBN.Text))
                {
                    return true;
                }

                string daikanKbnName = SalesPaymentConstans.DAIKAN_KBNExt.ToTypeString(SalesPaymentConstans.DAIKAN_KBNExt.ToDaikanKbn(this.form.DAIKAN_KBN.Text));
                if (string.IsNullOrEmpty(daikanKbnName))
                {
                    // エラーメッセージ
                    this.form.DAIKAN_KBN.IsInputErrorOccured = true;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E058", "");
                    this.form.DAIKAN_KBN.Focus();
                }
                else
                {
                    this.form.DAIKAN_KBN_NAME.Text = daikanKbnName;
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDaikanKbn", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 伝票日付チェック
        /// </summary>
        internal bool CheckDenpyouDate()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                var inputDenpyouDate = string.Empty;
                DateTime date = DateTime.Now;
                if (DateTime.TryParse(this.form.DENPYOU_DATE.Text, out date))
                {
                    inputDenpyouDate = date.ToString("yyyy/MM/dd");
                }

                // 伝票日付が空じゃないかつ変更があった場合
                if (!string.IsNullOrEmpty(inputDenpyouDate) && !this.tmpDenpyouDate.Equals(inputDenpyouDate))
                {
                    // 明細行すべての単価を再設定
                    var list = this.form.gcMultiRow1.Rows.Where(r => !r.IsNewRow).ToList();

                    foreach (Row dr in list)
                    {
                        if (!this.SearchAndCalcForUnit(false, dr))
                        {
                            return false;
                        }
                    }

                    // 合計金額の再計算
                    if (!this.CalcTotalValues())
                    {
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDenpyouDate", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 帳票(領収書)出力
        /// </summary>
        internal void Print()
        {
            LogUtility.DebugMethodStart();

            this.denpyouHakkou();
            DataTable reportData = CreateReportData();

            // G335\Templateにxmlを配置している
            // 現在表示されている一覧をレポート情報として生成
            ReportInfoR339 reportInfo = new ReportInfoR339(this.form.WindowId, reportData);
            reportInfo.CreateReport();
            reportInfo.Title = "領収書";

            // 印刷ポップアップ表示
            FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo, "R339");
            // 印刷アプリ初期動作(直印刷)
            reportPopup.PrintInitAction = 1;
            // 印刷実行
            reportPopup.PrintXPS(true, true);
            reportPopup.Dispose();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票出力用データ作成
        /// </summary>
        /// <returns>帳票用DataTable</returns>
        internal DataTable CreateReportData()
        {
            LogUtility.DebugMethodStart();
            DataTable reportTable = new DataTable();

            // Colum定義
            reportTable.Columns.Add("GYOUSHA_CD");
            reportTable.Columns["GYOUSHA_CD"].ReadOnly = false;
            reportTable.Columns.Add("GYOUSHA_NAME1");
            reportTable.Columns["GYOUSHA_NAME1"].ReadOnly = false;
            reportTable.Columns.Add("KEISHOU1");
            reportTable.Columns["KEISHOU1"].ReadOnly = false;
            reportTable.Columns.Add("GYOUSHA_NAME2");
            reportTable.Columns["GYOUSHA_NAME2"].ReadOnly = false;
            reportTable.Columns.Add("KEISHOU2");
            reportTable.Columns["KEISHOU2"].ReadOnly = false;
            reportTable.Columns.Add("DENPYOU_DATE");
            reportTable.Columns["DENPYOU_DATE"].ReadOnly = false;
            reportTable.Columns.Add("RECEIPT_NUMBER");
            reportTable.Columns["RECEIPT_NUMBER"].ReadOnly = false;
            reportTable.Columns.Add("KINGAKU_TOTAL");
            reportTable.Columns["KINGAKU_TOTAL"].ReadOnly = false;
            reportTable.Columns.Add("TADASHIGAKI");
            reportTable.Columns["TADASHIGAKI"].ReadOnly = false;
            reportTable.Columns.Add("CORP_RYAKU_NAME");
            reportTable.Columns["CORP_RYAKU_NAME"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_NAME");
            reportTable.Columns["KYOTEN_NAME"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_POST");
            reportTable.Columns["KYOTEN_POST"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_ADDRESS1");
            reportTable.Columns["KYOTEN_ADDRESS1"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_ADDRESS2");
            reportTable.Columns["KYOTEN_ADDRESS2"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_TEL");
            reportTable.Columns["KYOTEN_TEL"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_FAX");
            reportTable.Columns["KYOTEN_FAX"].ReadOnly = false;
            reportTable.Columns.Add("ZEINUKI_KINGAKU");
            reportTable.Columns["ZEINUKI_KINGAKU"].ReadOnly = false;
            reportTable.Columns.Add("SYOUHIZEI_RITU");
            reportTable.Columns["SYOUHIZEI_RITU"].ReadOnly = false;
            reportTable.Columns.Add("SYOUHIZEI");
            reportTable.Columns["SYOUHIZEI"].ReadOnly = false;
            reportTable.Columns.Add("DENPYOU_NUMBER");
            reportTable.Columns["DENPYOU_NUMBER"].ReadOnly = false;
            //小計②
            reportTable.Columns.Add("HIKAZEI_ZEINUKI_KINGAKU");
            reportTable.Columns["HIKAZEI_ZEINUKI_KINGAKU"].ReadOnly = false;
            reportTable.Columns.Add("HIKAZEI_SYOUHIZEI_RITU");
            reportTable.Columns["HIKAZEI_SYOUHIZEI_RITU"].ReadOnly = false;
            reportTable.Columns.Add("HIKAZEI_SYOUHIZEI");
            reportTable.Columns["HIKAZEI_SYOUHIZEI"].ReadOnly = false;
            //登録番号
            reportTable.Columns.Add("TOUROKU_NO");
            reportTable.Columns["TOUROKU_NO"].ReadOnly = false;

            // 取引先マスタ検索
            M_TORIHIKISAKI TorihikisakiEntity = new M_TORIHIKISAKI();
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                bool catchErr = true;
                TorihikisakiEntity = accessor.GetTorihikisaki(this.form.TORIHIKISAKI_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr, false);
                if (!catchErr)
                {
                    throw new Exception("");
                }
            }

            // 拠点マスタ検索
            short kyoteCd = -1;
            if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
            {
                short.TryParse(this.headerForm.KYOTEN_CD.Text, out kyoteCd);
            }
            M_KYOTEN[] kyotenEntitys = this.accessor.GetAllDataByCodeForKyoten(kyoteCd);

            // データセット
            DataRow row = reportTable.NewRow();
            row["GYOUSHA_CD"] = this.form.TORIHIKISAKI_CD.Text;
            if (TorihikisakiEntity != null)
            {
                // 諸口区分チェック
                if (TorihikisakiEntity.SHOKUCHI_KBN.IsTrue)
                {
                    if (this.dto.entryEntity.TORIHIKISAKI_NAME != null)
                    {
                        if (this.dto.entryEntity.TORIHIKISAKI_NAME.Length > 20)
                        {
                            string gyoushaName1 = string.Empty;
                            string gyoushaName2 = string.Empty;
                            ReportUtility.SubString(this.dto.entryEntity.TORIHIKISAKI_NAME, 40, ref gyoushaName1, ref gyoushaName2);
                            row["GYOUSHA_NAME1"] = gyoushaName1;
                            row["GYOUSHA_NAME2"] = gyoushaName2;
                        }
                        else
                        {
                            row["GYOUSHA_NAME1"] = this.dto.entryEntity.TORIHIKISAKI_NAME;
                        }
                    }
                }
                else
                {
                    row["GYOUSHA_NAME1"] = TorihikisakiEntity.TORIHIKISAKI_NAME1;
                    row["GYOUSHA_NAME2"] = TorihikisakiEntity.TORIHIKISAKI_NAME2;
                }
            }
            row["KEISHOU1"] = this.form.RECEIPT_KEISHOU_1.Text;
            row["KEISHOU2"] = this.form.RECEIPT_KEISHOU_2.Text;
            row["DENPYOU_DATE"] = this.form.DENPYOU_DATE.Text;
            if (this.dto.sysInfoEntity.SYS_RECEIPT_RENBAN_HOUHOU_KBN == 1)
            {
                row["RECEIPT_NUMBER"] = this.dto.entryEntity.RECEIPT_NUMBER;
            }
            else
            {
                row["RECEIPT_NUMBER"] = this.dto.entryEntity.RECEIPT_NUMBER_YEAR;
            }
            row["KINGAKU_TOTAL"] = this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Rorihiki;
            row["TADASHIGAKI"] = this.form.RECEIPT_TADASHIGAKI.Text;
            row["CORP_RYAKU_NAME"] = "";
            M_CORP_INFO entCorpInfo = CommonShogunData.CORP_INFO;
            if (entCorpInfo != null)
            {
                if (!string.IsNullOrEmpty(entCorpInfo.CORP_NAME))
                {
                    row["CORP_RYAKU_NAME"] = entCorpInfo.CORP_NAME;
                }
                //登録番号
                if (!string.IsNullOrEmpty(entCorpInfo.TOUROKU_NO))
                {
                    row["TOUROKU_NO"] = "登録番号：" + entCorpInfo.TOUROKU_NO;
                }
            }
            if (kyotenEntitys != null && kyotenEntitys.Length > 0)
            {
                row["KYOTEN_NAME"] = kyotenEntitys[0].KYOTEN_NAME;
                row["KYOTEN_POST"] = kyotenEntitys[0].KYOTEN_POST;
                row["KYOTEN_ADDRESS1"] = kyotenEntitys[0].KYOTEN_ADDRESS1;
                row["KYOTEN_ADDRESS2"] = kyotenEntitys[0].KYOTEN_ADDRESS2;  // No.3710
                row["KYOTEN_TEL"] = kyotenEntitys[0].KYOTEN_TEL;
                row["KYOTEN_FAX"] = kyotenEntitys[0].KYOTEN_FAX;
            }

            row["ZEINUKI_KINGAKU"] = this.form.denpyouHakouPopUpDTO.R_KAZEI_KINGAKU;
            decimal zeiritu;
            if (!string.IsNullOrEmpty(this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text))
            {
                zeiritu = this.ToDecimalForUriageShouhizeiRate();
            }
            else
            {
                var shouhizeiRate = this.accessor.GetShouhizeiRate(((DateTime)this.dto.entryEntity.DENPYOU_DATE).Date);
                zeiritu = (decimal)(shouhizeiRate.SHOUHIZEI_RATE);
            }
            row["SYOUHIZEI_RITU"] = string.Format("{0:0%}", zeiritu);
            row["SYOUHIZEI"] = this.form.denpyouHakouPopUpDTO.R_KAZEI_SHOUHIZEI;

            if (this.form.denpyouHakouPopUpDTO.R_KAZEI_KINGAKU == "0")
            {
                row["ZEINUKI_KINGAKU"] = "";
                row["SYOUHIZEI"] = "";
            }

            row["DENPYOU_NUMBER"] = "計量番号No." + this.dto.entryEntity.KEIRYOU_NUMBER;

            //小計②
            row["HIKAZEI_SYOUHIZEI_RITU"] = "非課税";
            if (this.form.denpyouHakouPopUpDTO.R_HIKAZEI_KINGAKU != "0")
            {
                row["HIKAZEI_ZEINUKI_KINGAKU"] = this.form.denpyouHakouPopUpDTO.R_HIKAZEI_KINGAKU;
                row["HIKAZEI_SYOUHIZEI"] = 0;
            }

            reportTable.Rows.Add(row);

            LogUtility.DebugMethodEnd();
            return reportTable;
        }

        string strTorihikisakiName = "";
        string strTorihikisakiName2 = "";
        string strTorihikisakiKeishou = "";
        string strTorihikisakiKeishou2 = "";
        string strNyuryokuTantousyaName = "";
        /// <summary>
        /// 印刷用現場名
        /// </summary>
        string strGenbaName = "";

        M_KYOTEN entKyotenInfo;

        //---計量票追加---------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 帳票(計量票)出力
        /// </summary>
        internal bool PrintKeiryouhyou()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // サブファンクションから呼ばれたときだけ確認のポップアップを上げる
                if (isSubFunctionCall)
                {
                    DialogResult ret = msgLogic.MessageBoxShow("C047", "計量票");
                    if (ret == DialogResult.No)
                    {
                        return true; ;
                    }
                }

                ReportInfoR354_R549_R550_R680_R681 reportInfo = new ReportInfoR354_R549_R550_R680_R681(this.form.WindowId);
                reportInfo.sysDate = this.footerForm.sysDate;
                if (!string.IsNullOrEmpty(Convert.ToString(this.form.DENPYOU_DATE.Value)))
                {
                    reportInfo.DenpyouDate = Convert.ToDateTime(this.form.DENPYOU_DATE.Value);
                }
                else
                {
                    reportInfo.DenpyouDate = Convert.ToDateTime(this.footerForm.sysDate);
                }
                string layoutName = string.Empty;
                string projectId = string.Empty;

                //システム設定[M_SYS_INFO].計量情報計量票レイアウト区分[KEIRYOU_LAYOUT_KBN]
                //システム設定[M_SYS_INFO].計量情報計量票品数区分[KEIRYOU_GOODS_KBN]
                if (this.dto.sysInfoEntity.KEIRYOU_LAYOUT_KBN.IsNull)
                {
                    return true; ;
                }
                switch ((int)this.dto.sysInfoEntity.KEIRYOU_LAYOUT_KBN)
                {
                    case 1:
                        // A4縦
                        reportInfo.OutputType = ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.Normal;
                        layoutName = "LAYOUT1";
                        projectId = "R354";
                        break;
                    case 2:
                        // A4横
                        if (this.dto.sysInfoEntity.KEIRYOU_GOODS_KBN == 1)
                        {
                            // 単品目
                            if (this.dto.sysInfoEntity.KEIRYOU_TORIHIKISAKI_DISP_KBN == 1)
                            {
                                reportInfo.OutputType = ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.SingleH;
                                layoutName = "LAYOUT3";
                                projectId = "R550";
                            }
                            // 単品目 取引先なし
                            else if (this.dto.sysInfoEntity.KEIRYOU_TORIHIKISAKI_DISP_KBN == 2)
                            {
                                reportInfo.OutputType = ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.SingleH_NoTorihikisaki;
                                layoutName = "LAYOUT5";
                                projectId = "R680";
                            }
                        }
                        else if (this.dto.sysInfoEntity.KEIRYOU_GOODS_KBN == 2)
                        {
                            // 複数品目
                            if (this.dto.sysInfoEntity.KEIRYOU_TORIHIKISAKI_DISP_KBN == 1)
                            {
                                reportInfo.OutputType = ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.MultiH;
                                layoutName = "LAYOUT2";
                                projectId = "R549";
                            }
                            // 複数品目 取引先なし
                            else if (this.dto.sysInfoEntity.KEIRYOU_TORIHIKISAKI_DISP_KBN == 2)
                            {
                                reportInfo.OutputType = ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.MultiH_NoTorihikisaki;
                                layoutName = "LAYOUT4";
                                projectId = "R681";
                            }
                        }
                        else
                        {
                            // なければなにもできないので終了
                            return true; ;
                        }
                        break;

                    default:
                        // なければなにもできないので終了
                        return true; ;
                }

                // データセット
                reportInfo = CreateKeiryouReport(reportInfo);
                reportInfo.Create(@".\Template\R354_R549_R550_R680_R681-Form.xml", layoutName, new DataTable());
                reportInfo.Title = "計量票";

                FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo, projectId);

                // 印刷アプリ初期動作(直印刷)
                reportPopup.PrintInitAction = 1;

                // 印刷実行
                reportPopup.PrintXPS(true, true);
                reportPopup.Dispose();

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("PrintKeiryouhyou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("PrintKeiryouhyou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        private ReportInfoR354_R549_R550_R680_R681 CreateKeiryouReport(ReportInfoR354_R549_R550_R680_R681 reportInfo)
        {
            // 取引先マスタ検索
            M_TORIHIKISAKI toriEntity = new M_TORIHIKISAKI();
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                //DBアクセスを無駄に行わないように先に固定の情報は取得する。
                bool catchErr = true;
                toriEntity = this.accessor.GetTorihikisaki(this.form.TORIHIKISAKI_CD.Text, out catchErr);
                if (!catchErr)
                {
                    throw new Exception("");
                }
            }
            if (toriEntity == null)
            {
                toriEntity = new M_TORIHIKISAKI();
            }

            // 業者マスタ検索
            M_GYOUSHA gyousEntity = new M_GYOUSHA();
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                bool catchErr = true;
                var retData = this.accessor.GetGyousha(this.form.GYOUSHA_CD.Text, out catchErr);
                if (!catchErr)
                {
                    throw new Exception("");
                }
                gyousEntity = retData;
            }
            if (gyousEntity == null)
            {
                gyousEntity = new M_GYOUSHA();
            }

            // 現場マスタ検索
            M_GENBA genbaEntity = new M_GENBA();
            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text) && !string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                bool catchErr = true;
                var retData = accessor.GetGenba(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, out catchErr);
                if (!catchErr)
                {
                    throw new Exception("");
                }
                genbaEntity = retData;
            }
            if (genbaEntity == null)
            {
                genbaEntity = new M_GENBA();
            }

            // 拠点マスタ検索(拠点マスタ.拠点CD＝0の拠点マスタ)
            M_KYOTEN kyotenEntitys = null;
            short kyotenCd;
            if (short.TryParse(this.headerForm.KYOTEN_CD.Text, out kyotenCd))
            {
                M_KYOTEN[] kyotens = accessor.GetAllDataByCodeForKyoten(kyotenCd);
                if (kyotens != null && kyotens.Count() > 0)
                {
                    // 拠点CDで絞り込んだら一件しか取れないはず
                    kyotenEntitys = kyotens[0];
                }
            }

            // 調整合計
            decimal chosei = 0;     //画面．明細．調整Kgの合計
            decimal youki = 0;      //画面．明細．容器重量の合計
            foreach (Row row in this.form.gcMultiRow1.Rows)
            {
                if (row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value != null)
                {
                    decimal choseiJyuuryou = 0;
                    if (decimal.TryParse(row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].FormattedValue.ToString(), out choseiJyuuryou))
                    {
                        chosei += choseiJyuuryou;
                    }
                }
                if (row.Cells[CELL_NAME_YOUKI_JYUURYOU].Value != null)
                {
                    decimal youkiJyuuryou = 0;
                    if (decimal.TryParse(row.Cells[CELL_NAME_YOUKI_JYUURYOU].FormattedValue.ToString(), out youkiJyuuryou))
                    {
                        youki += youkiJyuuryou;
                    }
                }
            }

            // データセット
            DataRow rowTmp;
            // Header部
            DataTable dataTableTmpH;
            dataTableTmpH = new DataTable();
            dataTableTmpH.TableName = "Header";
            // Detail部
            DataTable dataTableTmpD;
            dataTableTmpD = new DataTable();
            dataTableTmpD.TableName = "Detail";
            // Footer部
            DataTable dataTableTmpF;
            dataTableTmpF = new DataTable();
            dataTableTmpF.TableName = "Footer";

            DateTime Date = this.footerForm.sysDate;
            if (this.form.DENPYOU_DATE.Value != null)
            {
                Date = (DateTime)this.form.DENPYOU_DATE.Value;
            }

            switch (reportInfo.OutputType)
            {
                case ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.Normal:    // A4 縦三つ切り

                    #region - A4 縦三つ切り -

                    // 計量入力から出力する場合は取引先欄に業者情報を出力する。
                    reportInfo.DispTypeForNormal = ReportInfoR354_R549_R550_R680_R681.DispTypeForNormalDef.Gyousha;

                    // Header部

                    //タイトル名
                    dataTableTmpH.Columns.Add("KEIRYOU_HYOU_TITLE");
                    //担当名
                    dataTableTmpH.Columns.Add("TANTOU");
                    //業者CD
                    dataTableTmpH.Columns.Add("GYOUSHA_CD");
                    //業者名
                    dataTableTmpH.Columns.Add("GYOUSHA_NAME");
                    //業者名敬称
                    dataTableTmpH.Columns.Add("GYOUSHA_KEISYOU");
                    //伝票No
                    dataTableTmpH.Columns.Add("DENPYOU_NUMBER");
                    //乗員
                    dataTableTmpH.Columns.Add("JYOUIN");
                    //車番
                    dataTableTmpH.Columns.Add("SHABAN");
                    //伝票日付
                    dataTableTmpH.Columns.Add("DENPYOU_DATE");
                    //総重量計量時間
                    dataTableTmpH.Columns.Add("STACK_KEIRYOU_TIME");
                    //空車重量計量時間
                    dataTableTmpH.Columns.Add("EMPTY_KEIRYOU_TIME");

                    rowTmp = dataTableTmpH.NewRow();

                    rowTmp["KEIRYOU_HYOU_TITLE"] = this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_1 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_2 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_3;

                    rowTmp["TANTOU"] = strNyuryokuTantousyaName;

                    // 業者CD
                    rowTmp["GYOUSHA_CD"] = this.form.GYOUSHA_CD.Text;
                    // 業者マスタ．業者名1
                    rowTmp["GYOUSHA_NAME"] = string.Empty;
                    if (gyousEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の業者
                        rowTmp["GYOUSHA_NAME"] = this.form.GYOUSHA_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (gyousEntity.GYOUSHA_NAME1 != null)
                        {
                            // No.2996-->
                            if (gyousEntity.GYOUSHA_NAME2 != null)
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1 + "\n" + gyousEntity.GYOUSHA_NAME2;
                            }
                            else
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1;
                            }
                            // No.2996<--
                        }
                    }
                    // 業者マスタ．業者敬称1
                    rowTmp["GYOUSHA_KEISYOU"] = string.Empty;
                    if (gyousEntity.GYOUSHA_KEISHOU1 != null)
                    {
                        if (gyousEntity.GYOUSHA_KEISHOU2 != null)
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1 + "\n" + gyousEntity.GYOUSHA_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1;
                        }
                    }

                    // 伝票番号
                    if (this.dto.entryEntity != null && !this.dto.entryEntity.KEIRYOU_NUMBER.IsNull
                        && !this.dto.entryEntity.KEIRYOU_NUMBER.ToString().Equals(this.form.ENTRY_NUMBER.Text))
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.dto.entryEntity.KEIRYOU_NUMBER.ToString();
                    }
                    else
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.form.ENTRY_NUMBER.Text;
                    }

                    rowTmp["SHABAN"] = this.form.SHARYOU_NAME_RYAKU.Text;
                    if (this.form.DENPYOU_DATE.Value != null)
                    {
                        rowTmp["DENPYOU_DATE"] = Date.ToString("yyyy/MM/dd");
                    }

                    rowTmp["STACK_KEIRYOU_TIME"] = this.form.STACK_KEIRYOU_TIME.Text;
                    rowTmp["EMPTY_KEIRYOU_TIME"] = this.form.EMPTY_KEIRYOU_TIME.Text;
                    dataTableTmpH.Rows.Add(rowTmp);

                    // Detail部
                    dataTableTmpD.Columns.Add("ROW_NO");
                    dataTableTmpD.Columns.Add("STACK_JYUURYOU");
                    dataTableTmpD.Columns.Add("EMPTY_JYUURYOU");
                    dataTableTmpD.Columns.Add("NET_CHOUSEI");
                    dataTableTmpD.Columns.Add("YOUKI_JYUURYOU");
                    dataTableTmpD.Columns.Add("NET_JYUURYOU");
                    dataTableTmpD.Columns.Add("HINMEI_CD");
                    dataTableTmpD.Columns.Add("HINMEI_NAME");
                    dataTableTmpD.Columns.Add("KEIRYOU_TIME");

                    foreach (Row row in this.form.gcMultiRow1.Rows)
                    {
                        // 未確定行は無視
                        if (row.IsNewRow || string.IsNullOrEmpty(Convert.ToString(row.Cells["ROW_NO"].Value)))
                        {
                            continue;
                        }

                        rowTmp = dataTableTmpD.NewRow();

                        //No
                        rowTmp["ROW_NO"] = row.Cells[CELL_NAME_ROW_NO].Value;
                        //総重量
                        rowTmp["STACK_JYUURYOU"] = row.Cells[CELL_NAME_STAK_JYUURYOU].DisplayText;
                        //空車重量
                        rowTmp["EMPTY_JYUURYOU"] = row.Cells[CELL_NAME_EMPTY_JYUURYOU].DisplayText;
                        //調整
                        rowTmp["NET_CHOUSEI"] = row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].DisplayText;
                        //容器引
                        rowTmp["YOUKI_JYUURYOU"] = row.Cells[CELL_NAME_YOUKI_JYUURYOU].DisplayText;
                        //正味
                        rowTmp["NET_JYUURYOU"] = row.Cells[CELL_NAME_NET_JYUURYOU].DisplayText;
                        //品名CD
                        rowTmp["HINMEI_CD"] = row.Cells[CELL_NAME_HINMEI_CD].DisplayText;
                        // 品名マスタ．品名
                        rowTmp["HINMEI_NAME"] = row.Cells[CELL_NAME_HINMEI_NAME].DisplayText;
                        // 計量時間
                        rowTmp["KEIRYOU_TIME"] = row.Cells[CELL_NAME_KEIRYOU_TIME].DisplayText;

                        dataTableTmpD.Rows.Add(rowTmp);
                    }

                    // Footer部
                    dataTableTmpF.Columns.Add("GENBA_CD");
                    dataTableTmpF.Columns.Add("GENBA_NAME");
                    dataTableTmpF.Columns.Add("NET_JYUURYOU_TOTAL");
                    dataTableTmpF.Columns.Add("DENPYOU_BIKOU");
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU1");
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU2");
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU3");
                    dataTableTmpF.Columns.Add("CORP_RYAKU_NAME");
                    dataTableTmpF.Columns.Add("KYOTEN_NAME");
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS1");
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS2");
                    dataTableTmpF.Columns.Add("KYOTEN_TEL");
                    dataTableTmpF.Columns.Add("KYOTEN_FAX");

                    rowTmp = dataTableTmpF.NewRow();

                    //現場CD
                    rowTmp["GENBA_CD"] = this.form.GENBA_CD.Text;
                    //現場名
                    rowTmp["GENBA_NAME"] = string.Empty;
                    if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の現場
                        rowTmp["GENBA_NAME"] = this.form.GENBA_NAME_RYAKU.Text;
                    }
                    else
                    {
                        // 諸口以外
                        if (genbaEntity.GENBA_NAME1 != null)
                        {
                            if (genbaEntity.GENBA_NAME2 != null)
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1 + genbaEntity.GENBA_NAME2;
                            }
                            else
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1;
                            }
                        }
                    }
                    //正味合計
                    if (!string.IsNullOrEmpty(this.form.NET_TOTAL.Text) && decimal.Parse(this.form.NET_TOTAL.Text) > 0)//if does not exits detail then set blank
                    {
                        rowTmp["NET_JYUURYOU_TOTAL"] = this.form.NET_TOTAL.Text;
                    }
                    //備考
                    rowTmp["DENPYOU_BIKOU"] = this.form.DENPYOU_BIKOU.Text;

                    //初期化
                    rowTmp["KEIRYOU_JYOUHOU1"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU2"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU3"] = string.Empty;
                    rowTmp["CORP_RYAKU_NAME"] = string.Empty;
                    rowTmp["KYOTEN_NAME"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS1"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS2"] = string.Empty;
                    rowTmp["KYOTEN_TEL"] = string.Empty;
                    rowTmp["KYOTEN_FAX"] = string.Empty;

                    // 会社名
                    if (CommonShogunData.CORP_INFO.CORP_NAME != null)
                        rowTmp["CORP_RYAKU_NAME"] = CommonShogunData.CORP_INFO.CORP_NAME;
                    // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点名
                    if (kyotenEntitys != null)
                    {
                        if (kyotenEntitys.KYOTEN_NAME != null)
                            rowTmp["KYOTEN_NAME"] = kyotenEntitys.KYOTEN_NAME;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所1
                        if (kyotenEntitys.KYOTEN_ADDRESS1 != null)
                            rowTmp["KYOTEN_ADDRESS1"] = kyotenEntitys.KYOTEN_ADDRESS1;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所2
                        if (kyotenEntitys.KYOTEN_ADDRESS2 != null)
                            rowTmp["KYOTEN_ADDRESS2"] = kyotenEntitys.KYOTEN_ADDRESS2;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点TEL
                        if (kyotenEntitys.KYOTEN_TEL != null)
                            rowTmp["KYOTEN_TEL"] = kyotenEntitys.KYOTEN_TEL;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点FAX
                        if (kyotenEntitys.KYOTEN_FAX != null)
                            rowTmp["KYOTEN_FAX"] = kyotenEntitys.KYOTEN_FAX;
                        //計量情報計量証明項目1
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_1 != null)
                            rowTmp["KEIRYOU_JYOUHOU1"] = kyotenEntitys.KEIRYOU_SHOUMEI_1;
                        //計量情報計量証明項目2
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_2 != null)
                            rowTmp["KEIRYOU_JYOUHOU2"] = kyotenEntitys.KEIRYOU_SHOUMEI_2;
                        //計量情報計量証明項目3
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_3 != null)
                            rowTmp["KEIRYOU_JYOUHOU3"] = kyotenEntitys.KEIRYOU_SHOUMEI_3;
                    }
                    dataTableTmpF.Rows.Add(rowTmp);

                    #endregion - A4 縦三つ切り -

                    break;

                case ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.MultiH:   // 三つ切り 複数品目

                    #region - 三つ切り 複数品目 -

                    // Header部

                    // 計量証明書タイトル
                    dataTableTmpH.Columns.Add("KEIRYOU_HYOU_TITLE");
                    // 伝票日付
                    dataTableTmpH.Columns.Add("DENPYOU_DATE");
                    // 伝票番号
                    dataTableTmpH.Columns.Add("DENPYOU_NUMBER");
                    // 取引先CD
                    dataTableTmpH.Columns.Add("TORIHIKISAKI_CD");
                    // 取引先名
                    dataTableTmpH.Columns.Add("TORIHIKISAKI_NAME");
                    // 取引先敬称
                    dataTableTmpH.Columns.Add("TORIHIKISAKI_KEISYOU");
                    // 業者CD
                    dataTableTmpH.Columns.Add("GYOUSHA_CD");
                    // 業者名
                    dataTableTmpH.Columns.Add("GYOUSHA_NAME");
                    // 業者敬称
                    dataTableTmpH.Columns.Add("GYOUSHA_KEISYOU");
                    // 現場CD
                    dataTableTmpH.Columns.Add("GENBA_CD");
                    // 現場名
                    dataTableTmpH.Columns.Add("GENBA_NAME");
                    // 現場敬称
                    dataTableTmpH.Columns.Add("GENBA_KEISYOU");
                    // 車輌
                    dataTableTmpH.Columns.Add("SHARYOU");
                    // 総重量
                    dataTableTmpH.Columns.Add("STACK_JYUURYOU");
                    // 空車重量
                    dataTableTmpH.Columns.Add("EMPTY_JYUURYOU");
                    //総重量計量時間
                    dataTableTmpH.Columns.Add("STACK_KEIRYOU_TIME");
                    //空車重量計量時間
                    dataTableTmpH.Columns.Add("EMPTY_KEIRYOU_TIME");
                    // バーコード
                    dataTableTmpH.Columns.Add("BARCODE");

                    rowTmp = dataTableTmpH.NewRow();

                    // 計量票タイトル
                    rowTmp["KEIRYOU_HYOU_TITLE"] = this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_1 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_2 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_3;
                    // 伝票日付
                    if (this.form.DENPYOU_DATE.Value != null)
                    {
                        rowTmp["DENPYOU_DATE"] = Date.ToString("yyyy/MM/dd");
                    }
                    // 伝票番号
                    if (this.dto.entryEntity != null && !this.dto.entryEntity.KEIRYOU_NUMBER.IsNull
                        && !this.dto.entryEntity.KEIRYOU_NUMBER.ToString().Equals(this.form.ENTRY_NUMBER.Text))
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.dto.entryEntity.KEIRYOU_NUMBER.ToString();
                    }
                    else
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.form.ENTRY_NUMBER.Text;
                    }

                    // 取引先CD
                    rowTmp["TORIHIKISAKI_CD"] = this.form.TORIHIKISAKI_CD.Text;
                    // 取引先マスタ．取引先名1
                    rowTmp["TORIHIKISAKI_NAME"] = string.Empty;
                    if (toriEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の取引先
                        rowTmp["TORIHIKISAKI_NAME"] = this.form.TORIHIKISAKI_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (toriEntity.TORIHIKISAKI_NAME1 != null)
                        {
                            // No.2996-->
                            if (toriEntity.TORIHIKISAKI_NAME2 != null)
                            {
                                rowTmp["TORIHIKISAKI_NAME"] = toriEntity.TORIHIKISAKI_NAME1 + "\n" + toriEntity.TORIHIKISAKI_NAME2;
                            }
                            else
                            {
                                rowTmp["TORIHIKISAKI_NAME"] = toriEntity.TORIHIKISAKI_NAME1;
                            }
                            // No.2996<--
                        }
                    }
                    // 取引先マスタ．取引先敬称1
                    rowTmp["TORIHIKISAKI_KEISYOU"] = string.Empty;
                    if (toriEntity.TORIHIKISAKI_KEISHOU1 != null)
                    {
                        if (toriEntity.TORIHIKISAKI_KEISHOU2 != null)
                        {
                            rowTmp["TORIHIKISAKI_KEISYOU"] = toriEntity.TORIHIKISAKI_KEISHOU1 + "\n" + toriEntity.TORIHIKISAKI_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["TORIHIKISAKI_KEISYOU"] = toriEntity.TORIHIKISAKI_KEISHOU1;
                        }
                    }
                    // 業者CD
                    rowTmp["GYOUSHA_CD"] = this.form.GYOUSHA_CD.Text;
                    // 業者マスタ．業者名1
                    rowTmp["GYOUSHA_NAME"] = string.Empty;
                    if (gyousEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の業者
                        rowTmp["GYOUSHA_NAME"] = this.form.GYOUSHA_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (gyousEntity.GYOUSHA_NAME1 != null)
                        {
                            if (gyousEntity.GYOUSHA_NAME2 != null)
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1 + "\n" + gyousEntity.GYOUSHA_NAME2;
                            }
                            else
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1;
                            }
                        }
                    }
                    // 業者マスタ．業者敬称1
                    rowTmp["GYOUSHA_KEISYOU"] = string.Empty;
                    if (gyousEntity.GYOUSHA_KEISHOU1 != null)
                    {
                        if (gyousEntity.GYOUSHA_KEISHOU2 != null)
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1 + "\n" + gyousEntity.GYOUSHA_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1;
                        }
                    }
                    // 現場CD
                    rowTmp["GENBA_CD"] = this.form.GENBA_CD.Text;
                    // 現場マスタ．現場名1
                    rowTmp["GENBA_NAME"] = string.Empty;
                    if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の現場
                        rowTmp["GENBA_NAME"] = this.form.GENBA_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (genbaEntity.GENBA_NAME1 != null)
                        {
                            if (genbaEntity.GENBA_NAME2 != null)
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1 + "\n" + genbaEntity.GENBA_NAME2;
                            }
                            else
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1;
                            }
                        }
                    }

                    // 現場マスタ．現場敬称1
                    rowTmp["GENBA_KEISYOU"] = string.Empty;
                    if (genbaEntity.GENBA_KEISHOU1 != null)
                    {
                        if (genbaEntity.GENBA_KEISHOU2 != null)
                        {
                            rowTmp["GENBA_KEISYOU"] = genbaEntity.GENBA_KEISHOU1 + "\n" + genbaEntity.GENBA_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["GENBA_KEISYOU"] = genbaEntity.GENBA_KEISHOU1;
                        }
                    }

                    // 車輌
                    rowTmp["SHARYOU"] = this.form.SHARYOU_NAME_RYAKU.Text;

                    // 総重量、空車重量(画面に表示されている重量値系はフォーマット済みなので、そのまま使用する)
                    rowTmp["STACK_JYUURYOU"] = this.form.STACK_JYUURYOU.Text;
                    rowTmp["EMPTY_JYUURYOU"] = this.form.EMPTY_JYUURYOU.Text;

                    rowTmp["STACK_KEIRYOU_TIME"] = this.form.STACK_KEIRYOU_TIME.Text;
                    rowTmp["EMPTY_KEIRYOU_TIME"] = this.form.EMPTY_KEIRYOU_TIME.Text;

                    //バーコード（02＋計量番号＋差額）
                    rowTmp["BARCODE"] = "02"
                        + this.form.ENTRY_NUMBER.Text.PadLeft(4, '0')
                        + Convert.ToString(this.form.SAGAKU.Text.ToString()).Replace(",", "").Replace("-", "").PadLeft(6, '0');

                    dataTableTmpH.Rows.Add(rowTmp);

                    // Detail部

                    // 品名CD
                    dataTableTmpD.Columns.Add("HINMEI_CD");
                    // 品名
                    dataTableTmpD.Columns.Add("HINMEI_NAME");
                    // 調整
                    dataTableTmpD.Columns.Add("NET_CHOUSEI");
                    // 容器引
                    dataTableTmpD.Columns.Add("YOUKI_JYUURYOU");
                    // 正味
                    dataTableTmpD.Columns.Add("NET_JYUURYOU");
                    // 計量時間
                    dataTableTmpD.Columns.Add("KEIRYOU_TIME");

                    foreach (Row row in this.form.gcMultiRow1.Rows)
                    {
                        // 未確定行は無視
                        if (row.IsNewRow || string.IsNullOrEmpty(Convert.ToString(row.Cells["ROW_NO"].Value)))
                        {
                            continue;
                        }

                        rowTmp = dataTableTmpD.NewRow();

                        rowTmp["HINMEI_CD"] = row.Cells[CELL_NAME_HINMEI_CD].Value;
                        // 品名マスタ．品名
                        rowTmp["HINMEI_NAME"] = row.Cells[CELL_NAME_HINMEI_NAME].DisplayText;

                        rowTmp["NET_CHOUSEI"] = row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].DisplayText;
                        rowTmp["YOUKI_JYUURYOU"] = row.Cells[CELL_NAME_YOUKI_JYUURYOU].DisplayText;
                        rowTmp["NET_JYUURYOU"] = row.Cells[CELL_NAME_NET_JYUURYOU].DisplayText;
                        // 計量時間
                        rowTmp["KEIRYOU_TIME"] = row.Cells[CELL_NAME_KEIRYOU_TIME].DisplayText;

                        dataTableTmpD.Rows.Add(rowTmp);
                    }

                    // Footer部

                    // 調整合計
                    dataTableTmpF.Columns.Add("NET_CHOSEI_TOTAL");
                    // 容器引合計
                    dataTableTmpF.Columns.Add("YOUKI_JYUURYOU_TOTAL");
                    // 正味合計
                    dataTableTmpF.Columns.Add("NET_JYUURYOU_TOTAL");
                    // 伝票備考
                    dataTableTmpF.Columns.Add("DENPYOU_BIKOU");
                    // 計量情報計量証明項目1
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU1");
                    // 計量情報計量証明項目2
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU2");
                    // 計量情報計量証明項目3
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU3");
                    // 会社名
                    dataTableTmpF.Columns.Add("CORP_RYAKU_NAME");
                    // 拠点
                    dataTableTmpF.Columns.Add("KYOTEN_NAME");
                    // 拠点住所1
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS1");
                    // 拠点住所2
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS2");
                    // 拠点電話
                    dataTableTmpF.Columns.Add("KYOTEN_TEL");
                    // 拠点FAX
                    dataTableTmpF.Columns.Add("KYOTEN_FAX");

                    rowTmp = dataTableTmpF.NewRow();

                    // 調整合計
                    if (chosei > 0)
                    {
                        rowTmp["NET_CHOSEI_TOTAL"] = chosei.ToString(this.dto.sysInfoEntity.SYS_JYURYOU_FORMAT);
                    }
                    // 容器引合計
                    if (youki > 0)
                    {
                        rowTmp["YOUKI_JYUURYOU_TOTAL"] = youki.ToString(this.dto.sysInfoEntity.SYS_JYURYOU_FORMAT);
                    }
                    // 正味合計
                    if (!string.IsNullOrEmpty(this.form.NET_TOTAL.Text) && decimal.Parse(this.form.NET_TOTAL.Text) > 0)
                    {
                        rowTmp["NET_JYUURYOU_TOTAL"] = this.form.NET_TOTAL.Text;
                    }
                    // 伝票備考
                    rowTmp["DENPYOU_BIKOU"] = this.form.DENPYOU_BIKOU.Text;

                    //初期化
                    rowTmp["KEIRYOU_JYOUHOU1"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU2"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU3"] = string.Empty;
                    rowTmp["CORP_RYAKU_NAME"] = string.Empty;
                    rowTmp["KYOTEN_NAME"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS1"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS2"] = string.Empty;
                    rowTmp["KYOTEN_TEL"] = string.Empty;
                    rowTmp["KYOTEN_FAX"] = string.Empty;
                    // 会社名
                    if (CommonShogunData.CORP_INFO.CORP_NAME != null)
                        rowTmp["CORP_RYAKU_NAME"] = CommonShogunData.CORP_INFO.CORP_NAME;
                    // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点名
                    if (kyotenEntitys != null)
                    {
                        if (kyotenEntitys.KYOTEN_NAME != null)
                            rowTmp["KYOTEN_NAME"] = kyotenEntitys.KYOTEN_NAME;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所1
                        if (kyotenEntitys.KYOTEN_ADDRESS1 != null)
                            rowTmp["KYOTEN_ADDRESS1"] = kyotenEntitys.KYOTEN_ADDRESS1;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所2
                        if (kyotenEntitys.KYOTEN_ADDRESS2 != null)
                            rowTmp["KYOTEN_ADDRESS2"] = kyotenEntitys.KYOTEN_ADDRESS2;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点TEL
                        if (kyotenEntitys.KYOTEN_TEL != null)
                            rowTmp["KYOTEN_TEL"] = kyotenEntitys.KYOTEN_TEL;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点FAX
                        if (kyotenEntitys.KYOTEN_FAX != null)
                            rowTmp["KYOTEN_FAX"] = kyotenEntitys.KYOTEN_FAX;
                        //計量情報計量証明項目1
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_1 != null)
                            rowTmp["KEIRYOU_JYOUHOU1"] = kyotenEntitys.KEIRYOU_SHOUMEI_1;
                        //計量情報計量証明項目2
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_2 != null)
                            rowTmp["KEIRYOU_JYOUHOU2"] = kyotenEntitys.KEIRYOU_SHOUMEI_2;
                        //計量情報計量証明項目3
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_3 != null)
                            rowTmp["KEIRYOU_JYOUHOU3"] = kyotenEntitys.KEIRYOU_SHOUMEI_3;
                    }

                    dataTableTmpF.Rows.Add(rowTmp);

                    #endregion - 三つ切り 複数品目 -

                    break;

                case ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.SingleH:   // 三つ切り 単品目

                    #region - 三つ切り 単品目 -

                    // Header部

                    // 計量証明書タイトル
                    dataTableTmpH.Columns.Add("KEIRYOU_HYOU_TITLE");
                    // 伝票日付
                    dataTableTmpH.Columns.Add("DENPYOU_DATE");
                    // 伝票番号
                    dataTableTmpH.Columns.Add("DENPYOU_NUMBER");
                    // 取引先CD
                    dataTableTmpH.Columns.Add("TORIHIKISAKI_CD");
                    // 取引先名
                    dataTableTmpH.Columns.Add("TORIHIKISAKI_NAME");
                    // 取引先敬称
                    dataTableTmpH.Columns.Add("TORIHIKISAKI_KEISYOU");
                    // 業者CD
                    dataTableTmpH.Columns.Add("GYOUSHA_CD");
                    // 業者名
                    dataTableTmpH.Columns.Add("GYOUSHA_NAME");
                    // 業者敬称
                    dataTableTmpH.Columns.Add("GYOUSHA_KEISYOU");
                    // 現場CD
                    dataTableTmpH.Columns.Add("GENBA_CD");
                    // 現場名
                    dataTableTmpH.Columns.Add("GENBA_NAME");
                    // 現場敬称
                    dataTableTmpH.Columns.Add("GENBA_KEISYOU");
                    // 車輌
                    dataTableTmpH.Columns.Add("SHARYOU");
                    // バーコード
                    dataTableTmpH.Columns.Add("BARCODE");

                    rowTmp = dataTableTmpH.NewRow();

                    // 計量票タイトル
                    rowTmp["KEIRYOU_HYOU_TITLE"] = this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_1 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_2 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_3;
                    // 伝票日付
                    if (this.form.DENPYOU_DATE.Value != null)
                    {
                        rowTmp["DENPYOU_DATE"] = Date.ToString("yyyy/MM/dd");
                    }
                    // 伝票番号
                    if (this.dto.entryEntity != null && !this.dto.entryEntity.KEIRYOU_NUMBER.IsNull
                        && !this.dto.entryEntity.KEIRYOU_NUMBER.ToString().Equals(this.form.ENTRY_NUMBER.Text))
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.dto.entryEntity.KEIRYOU_NUMBER.ToString();
                    }
                    else
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.form.ENTRY_NUMBER.Text;
                    }

                    // 取引先CD
                    rowTmp["TORIHIKISAKI_CD"] = this.form.TORIHIKISAKI_CD.Text;
                    // 取引先マスタ．取引先名1
                    rowTmp["TORIHIKISAKI_NAME"] = string.Empty;
                    if (toriEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の取引先
                        rowTmp["TORIHIKISAKI_NAME"] = this.form.TORIHIKISAKI_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (toriEntity.TORIHIKISAKI_NAME1 != null)
                        {
                            if (toriEntity.TORIHIKISAKI_NAME2 != null)
                            {
                                rowTmp["TORIHIKISAKI_NAME"] = toriEntity.TORIHIKISAKI_NAME1 + "\n" + toriEntity.TORIHIKISAKI_NAME2;
                            }
                            else
                            {
                                rowTmp["TORIHIKISAKI_NAME"] = toriEntity.TORIHIKISAKI_NAME1;
                            }
                        }
                    }
                    // 取引先マスタ．取引先敬称1
                    rowTmp["TORIHIKISAKI_KEISYOU"] = string.Empty;
                    if (toriEntity.TORIHIKISAKI_KEISHOU1 != null)
                    {
                        if (toriEntity.TORIHIKISAKI_KEISHOU2 != null)
                        {
                            rowTmp["TORIHIKISAKI_KEISYOU"] = toriEntity.TORIHIKISAKI_KEISHOU1 + "\n" + toriEntity.TORIHIKISAKI_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["TORIHIKISAKI_KEISYOU"] = toriEntity.TORIHIKISAKI_KEISHOU1;
                        }
                    }
                    // 業者CD
                    rowTmp["GYOUSHA_CD"] = this.form.GYOUSHA_CD.Text;
                    // 業者マスタ．業者名1
                    rowTmp["GYOUSHA_NAME"] = string.Empty;
                    if (gyousEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の業者
                        rowTmp["GYOUSHA_NAME"] = this.form.GYOUSHA_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (gyousEntity.GYOUSHA_NAME1 != null)
                        {
                            if (gyousEntity.GYOUSHA_NAME2 != null)
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1 + "\n" + gyousEntity.GYOUSHA_NAME2;
                            }
                            else
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1;
                            }
                        }
                    }
                    // 業者マスタ．業者敬称1
                    rowTmp["GYOUSHA_KEISYOU"] = string.Empty;
                    if (gyousEntity.GYOUSHA_KEISHOU1 != null)
                    {
                        if (gyousEntity.GYOUSHA_KEISHOU2 != null)
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1 + "\n" + gyousEntity.GYOUSHA_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1;
                        }
                    }
                    // 現場CD
                    rowTmp["GENBA_CD"] = this.form.GENBA_CD.Text;
                    // 現場マスタ．現場名1
                    rowTmp["GENBA_NAME"] = string.Empty;
                    if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の現場
                        rowTmp["GENBA_NAME"] = this.form.GENBA_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (genbaEntity.GENBA_NAME1 != null)
                        {
                            if (genbaEntity.GENBA_NAME2 != null)
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1 + "\n" + genbaEntity.GENBA_NAME2;
                            }
                            else
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1;
                            }
                        }
                    }

                    // 現場マスタ．現場敬称1
                    rowTmp["GENBA_KEISYOU"] = string.Empty;
                    if (genbaEntity.GENBA_KEISHOU1 != null)
                    {
                        if (genbaEntity.GENBA_KEISHOU2 != null)
                        {
                            rowTmp["GENBA_KEISYOU"] = genbaEntity.GENBA_KEISHOU1 + "\n" + genbaEntity.GENBA_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["GENBA_KEISYOU"] = genbaEntity.GENBA_KEISHOU1;
                        }
                    }

                    // 車輌
                    rowTmp["SHARYOU"] = this.form.SHARYOU_NAME_RYAKU.Text;

                    //バーコード（02＋計量番号＋差額）
                    rowTmp["BARCODE"] = "02"
                            + this.form.ENTRY_NUMBER.Text.PadLeft(4, '0')
                            + Convert.ToString(this.form.SAGAKU.Text.ToString()).Replace(",", "").Replace("-", "").PadLeft(6, '0');

                    dataTableTmpH.Rows.Add(rowTmp);

                    // Detail部
                    // 品名CD
                    dataTableTmpD.Columns.Add("HINMEI_CD");
                    // 品名
                    dataTableTmpD.Columns.Add("HINMEI_NAME");
                    // 総重量
                    dataTableTmpD.Columns.Add("STACK_JYUURYOU");
                    // 空車重量
                    dataTableTmpD.Columns.Add("EMPTY_JYUURYOU");
                    // 調整
                    dataTableTmpD.Columns.Add("NET_CHOUSEI");
                    // 容器引
                    dataTableTmpD.Columns.Add("YOUKI_JYUURYOU");
                    // 正味
                    dataTableTmpD.Columns.Add("NET_JYUURYOU");
                    //総重量計量時間
                    dataTableTmpD.Columns.Add("STACK_KEIRYOU_TIME");
                    //空車重量計量時間
                    dataTableTmpD.Columns.Add("EMPTY_KEIRYOU_TIME");
                    // 計量時間
                    dataTableTmpD.Columns.Add("NET_JYUURYOU_TIME");

                    rowTmp = dataTableTmpD.NewRow();

                    // 未確定行は無視
                    rowTmp["HINMEI_CD"] = string.Empty;
                    rowTmp["HINMEI_NAME"] = string.Empty;
                    rowTmp["STACK_JYUURYOU"] = string.Empty;
                    rowTmp["EMPTY_JYUURYOU"] = string.Empty;
                    rowTmp["STACK_KEIRYOU_TIME"] = string.Empty;
                    rowTmp["EMPTY_KEIRYOU_TIME"] = string.Empty;
                    rowTmp["NET_JYUURYOU_TIME"] = string.Empty;
                    if (this.form.gcMultiRow1.Rows.Count > 1)
                    {
                        // 品名CD
                        rowTmp["HINMEI_CD"] = this.form.gcMultiRow1.Rows[0].Cells[CELL_NAME_HINMEI_CD].Value;
                        rowTmp["HINMEI_NAME"] = this.form.gcMultiRow1.Rows[0].Cells[CELL_NAME_HINMEI_NAME].Value;
                        // 総重量(画面に表示されている重量値系はフォーマット済みなので、そのまま使用する)
                        rowTmp["STACK_JYUURYOU"] = this.form.STACK_JYUURYOU.Text;
                        // 空車重量(画面に表示されている重量値系はフォーマット済みなので、そのまま使用する)
                        rowTmp["EMPTY_JYUURYOU"] = this.form.EMPTY_JYUURYOU.Text;
                        // 総重量計量時間
                        rowTmp["STACK_KEIRYOU_TIME"] = this.form.STACK_KEIRYOU_TIME.Text;
                        // 空車重量計量時間
                        rowTmp["EMPTY_KEIRYOU_TIME"] = this.form.EMPTY_KEIRYOU_TIME.Text;
                        // 正味重量計量時間
                        rowTmp["NET_JYUURYOU_TIME"] = this.form.gcMultiRow1.Rows[0].Cells[CELL_NAME_KEIRYOU_TIME].Value;
                    }

                    // 調整
                    if (chosei > 0)
                    {
                        rowTmp["NET_CHOUSEI"] = chosei.ToString(this.dto.sysInfoEntity.SYS_JYURYOU_FORMAT);
                    }
                    // 容器引
                    if (youki > 0)
                    {
                        rowTmp["YOUKI_JYUURYOU"] = youki.ToString(this.dto.sysInfoEntity.SYS_JYURYOU_FORMAT);
                    }

                    // 正味
                    if (!string.IsNullOrEmpty(this.form.NET_TOTAL.Text) && decimal.Parse(this.form.NET_TOTAL.Text) > 0)
                    {
                        rowTmp["NET_JYUURYOU"] = this.form.gcMultiRow1.Rows[0].Cells[CELL_NAME_NET_JYUURYOU].DisplayText;
                    }

                    dataTableTmpD.Rows.Add(rowTmp);

                    // Footer部

                    // 伝票備考
                    dataTableTmpF.Columns.Add("DENPYOU_BIKOU");
                    // 計量情報計量証明項目1
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU1");
                    // 計量情報計量証明項目2
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU2");
                    // 計量情報計量証明項目3
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU3");
                    // 会社名
                    dataTableTmpF.Columns.Add("CORP_RYAKU_NAME");
                    // 拠点
                    dataTableTmpF.Columns.Add("KYOTEN_NAME");
                    // 拠点住所1
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS1");
                    // 拠点住所2
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS2");
                    // 拠点電話
                    dataTableTmpF.Columns.Add("KYOTEN_TEL");
                    // 拠点FAX
                    dataTableTmpF.Columns.Add("KYOTEN_FAX");

                    rowTmp = dataTableTmpF.NewRow();

                    //初期化
                    rowTmp["KEIRYOU_JYOUHOU1"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU2"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU3"] = string.Empty;
                    rowTmp["DENPYOU_BIKOU"] = string.Empty;
                    rowTmp["CORP_RYAKU_NAME"] = string.Empty;
                    rowTmp["KYOTEN_NAME"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS1"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS2"] = string.Empty;
                    rowTmp["KYOTEN_TEL"] = string.Empty;
                    rowTmp["KYOTEN_FAX"] = string.Empty;

                    //伝票備考
                    rowTmp["DENPYOU_BIKOU"] = this.form.DENPYOU_BIKOU.Text;

                    // 会社名
                    if (CommonShogunData.CORP_INFO.CORP_NAME != null)
                        rowTmp["CORP_RYAKU_NAME"] = CommonShogunData.CORP_INFO.CORP_NAME;
                    // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点名
                    if (kyotenEntitys != null)
                    {
                        if (kyotenEntitys.KYOTEN_NAME != null)
                            rowTmp["KYOTEN_NAME"] = kyotenEntitys.KYOTEN_NAME;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所1
                        if (kyotenEntitys.KYOTEN_ADDRESS1 != null)
                            rowTmp["KYOTEN_ADDRESS1"] = kyotenEntitys.KYOTEN_ADDRESS1;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所2
                        if (kyotenEntitys.KYOTEN_ADDRESS2 != null)
                            rowTmp["KYOTEN_ADDRESS2"] = kyotenEntitys.KYOTEN_ADDRESS2;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点TEL
                        if (kyotenEntitys.KYOTEN_TEL != null)
                            rowTmp["KYOTEN_TEL"] = kyotenEntitys.KYOTEN_TEL;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点FAX
                        if (kyotenEntitys.KYOTEN_FAX != null)
                            rowTmp["KYOTEN_FAX"] = kyotenEntitys.KYOTEN_FAX;
                        //計量情報計量証明項目1
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_1 != null)
                            rowTmp["KEIRYOU_JYOUHOU1"] = kyotenEntitys.KEIRYOU_SHOUMEI_1;
                        //計量情報計量証明項目2
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_2 != null)
                            rowTmp["KEIRYOU_JYOUHOU2"] = kyotenEntitys.KEIRYOU_SHOUMEI_2;
                        //計量情報計量証明項目3
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_3 != null)
                            rowTmp["KEIRYOU_JYOUHOU3"] = kyotenEntitys.KEIRYOU_SHOUMEI_3;
                    }

                    dataTableTmpF.Rows.Add(rowTmp);

                    #endregion - 三つ切り 単品目 -

                    break;
                case ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.MultiH_NoTorihikisaki:   // 三つ切り 複数品目 取引先無し

                    #region - 三つ切り 複数品目 取引先無し -

                    // Header部

                    // 計量証明書タイトル
                    dataTableTmpH.Columns.Add("KEIRYOU_HYOU_TITLE");
                    // 伝票日付
                    dataTableTmpH.Columns.Add("DENPYOU_DATE");
                    // 伝票番号
                    dataTableTmpH.Columns.Add("DENPYOU_NUMBER");
                    // 業者CD
                    dataTableTmpH.Columns.Add("GYOUSHA_CD");
                    // 業者名
                    dataTableTmpH.Columns.Add("GYOUSHA_NAME");
                    // 業者敬称
                    dataTableTmpH.Columns.Add("GYOUSHA_KEISYOU");
                    // 現場CD
                    dataTableTmpH.Columns.Add("GENBA_CD");
                    // 現場名
                    dataTableTmpH.Columns.Add("GENBA_NAME");
                    // 現場敬称
                    dataTableTmpH.Columns.Add("GENBA_KEISYOU");
                    // 車輌
                    dataTableTmpH.Columns.Add("SHARYOU");
                    // 総重量
                    dataTableTmpH.Columns.Add("STACK_JYUURYOU");
                    // 空車重量
                    dataTableTmpH.Columns.Add("EMPTY_JYUURYOU");
                    //総重量計量時間
                    dataTableTmpH.Columns.Add("STACK_KEIRYOU_TIME");
                    //空車重量計量時間
                    dataTableTmpH.Columns.Add("EMPTY_KEIRYOU_TIME");
                    // バーコード
                    dataTableTmpH.Columns.Add("BARCODE");

                    rowTmp = dataTableTmpH.NewRow();

                    // 計量票タイトル
                    rowTmp["KEIRYOU_HYOU_TITLE"] = this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_1 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_2 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_3;
                    // 伝票日付
                    if (this.form.DENPYOU_DATE.Value != null)
                    {
                        rowTmp["DENPYOU_DATE"] = Date.ToString("yyyy/MM/dd");
                    }
                    // 伝票番号
                    if (this.dto.entryEntity != null && !this.dto.entryEntity.KEIRYOU_NUMBER.IsNull
                        && !this.dto.entryEntity.KEIRYOU_NUMBER.ToString().Equals(this.form.ENTRY_NUMBER.ToString()))
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.dto.entryEntity.KEIRYOU_NUMBER.ToString();
                    }
                    else
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.form.ENTRY_NUMBER.Text;
                    }

                    // 業者CD
                    rowTmp["GYOUSHA_CD"] = this.form.GYOUSHA_CD.Text;
                    // 業者マスタ．業者名1
                    rowTmp["GYOUSHA_NAME"] = string.Empty;
                    if (gyousEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の業者
                        rowTmp["GYOUSHA_NAME"] = this.form.GYOUSHA_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (gyousEntity.GYOUSHA_NAME1 != null)
                        {
                            if (gyousEntity.GYOUSHA_NAME2 != null)
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1 + "\n" + gyousEntity.GYOUSHA_NAME2;
                            }
                            else
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1;
                            }
                        }
                    }
                    // 業者マスタ．業者敬称1
                    rowTmp["GYOUSHA_KEISYOU"] = string.Empty;
                    if (gyousEntity.GYOUSHA_KEISHOU1 != null)
                    {
                        if (gyousEntity.GYOUSHA_KEISHOU2 != null)
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1 + "\n" + gyousEntity.GYOUSHA_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1;
                        }
                    }
                    // 現場CD
                    rowTmp["GENBA_CD"] = this.form.GENBA_CD.Text;
                    // 現場マスタ．現場名1
                    rowTmp["GENBA_NAME"] = string.Empty;
                    if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の現場
                        rowTmp["GENBA_NAME"] = this.form.GENBA_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (genbaEntity.GENBA_NAME1 != null)
                        {
                            if (genbaEntity.GENBA_NAME2 != null)
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1 + "\n" + genbaEntity.GENBA_NAME2;
                            }
                            else
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1;
                            }
                        }
                    }

                    // 現場マスタ．現場敬称1
                    rowTmp["GENBA_KEISYOU"] = string.Empty;
                    if (genbaEntity.GENBA_KEISHOU1 != null)
                    {
                        if (genbaEntity.GENBA_KEISHOU2 != null)
                        {
                            rowTmp["GENBA_KEISYOU"] = genbaEntity.GENBA_KEISHOU1 + "\n" + genbaEntity.GENBA_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["GENBA_KEISYOU"] = genbaEntity.GENBA_KEISHOU1;
                        }
                    }

                    // 車輌
                    rowTmp["SHARYOU"] = this.form.SHARYOU_NAME_RYAKU.Text;

                    // 総重量、空車重量(画面に表示されている重量値系はフォーマット済みなので、そのまま使用する)
                    rowTmp["STACK_JYUURYOU"] = this.form.STACK_JYUURYOU.Text;
                    rowTmp["EMPTY_JYUURYOU"] = this.form.EMPTY_JYUURYOU.Text;

                    rowTmp["STACK_KEIRYOU_TIME"] = this.form.STACK_KEIRYOU_TIME.Text;
                    rowTmp["EMPTY_KEIRYOU_TIME"] = this.form.EMPTY_KEIRYOU_TIME.Text;

                    //バーコード（02＋計量番号＋差額）
                    rowTmp["BARCODE"] = "02"
                            + this.form.ENTRY_NUMBER.Text.PadLeft(4, '0')
                            + Convert.ToString(this.form.SAGAKU.Text.ToString()).Replace(",", "").Replace("-", "").PadLeft(6, '0');

                    dataTableTmpH.Rows.Add(rowTmp);

                    // Detail部

                    // 品名CD
                    dataTableTmpD.Columns.Add("HINMEI_CD");
                    // 品名
                    dataTableTmpD.Columns.Add("HINMEI_NAME");
                    // 調整
                    dataTableTmpD.Columns.Add("NET_CHOUSEI");
                    // 容器引
                    dataTableTmpD.Columns.Add("YOUKI_JYUURYOU");
                    // 正味
                    dataTableTmpD.Columns.Add("NET_JYUURYOU");
                    // 計量時間
                    dataTableTmpD.Columns.Add("KEIRYOU_TIME");

                    foreach (Row row in this.form.gcMultiRow1.Rows)
                    {
                        // 未確定行は無視
                        if (row.IsNewRow || string.IsNullOrEmpty((string)row.Cells["ROW_NO"].Value.ToString()))
                        {
                            continue;
                        }

                        rowTmp = dataTableTmpD.NewRow();

                        rowTmp["HINMEI_CD"] = row.Cells[CELL_NAME_HINMEI_CD].Value;
                        // 品名マスタ．品名
                        rowTmp["HINMEI_NAME"] = row.Cells[CELL_NAME_HINMEI_NAME].DisplayText;

                        rowTmp["NET_CHOUSEI"] = row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].DisplayText;
                        rowTmp["YOUKI_JYUURYOU"] = row.Cells[CELL_NAME_YOUKI_JYUURYOU].DisplayText;
                        rowTmp["NET_JYUURYOU"] = row.Cells[CELL_NAME_NET_JYUURYOU].DisplayText;
                        // 計量時間
                        rowTmp["KEIRYOU_TIME"] = row.Cells[CELL_NAME_KEIRYOU_TIME].DisplayText;

                        dataTableTmpD.Rows.Add(rowTmp);
                    }

                    // Footer部

                    // 調整合計
                    dataTableTmpF.Columns.Add("NET_CHOSEI_TOTAL");
                    // 容器引合計
                    dataTableTmpF.Columns.Add("YOUKI_JYUURYOU_TOTAL");
                    // 正味合計
                    dataTableTmpF.Columns.Add("NET_JYUURYOU_TOTAL");
                    // 伝票備考
                    dataTableTmpF.Columns.Add("DENPYOU_BIKOU");
                    // 計量情報計量証明項目1
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU1");
                    // 計量情報計量証明項目2
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU2");
                    // 計量情報計量証明項目3
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU3");
                    // 会社名
                    dataTableTmpF.Columns.Add("CORP_RYAKU_NAME");
                    // 拠点
                    dataTableTmpF.Columns.Add("KYOTEN_NAME");
                    // 拠点住所1
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS1");
                    // 拠点住所2
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS2");
                    // 拠点電話
                    dataTableTmpF.Columns.Add("KYOTEN_TEL");
                    // 拠点FAX
                    dataTableTmpF.Columns.Add("KYOTEN_FAX");

                    rowTmp = dataTableTmpF.NewRow();

                    // 調整合計
                    if (chosei > 0)
                    {
                        rowTmp["NET_CHOSEI_TOTAL"] = chosei.ToString(this.dto.sysInfoEntity.SYS_JYURYOU_FORMAT);
                    }
                    // 容器引合計
                    if (youki > 0)
                    {
                        rowTmp["YOUKI_JYUURYOU_TOTAL"] = youki.ToString(this.dto.sysInfoEntity.SYS_JYURYOU_FORMAT);
                    }
                    // 正味合計
                    if (!string.IsNullOrEmpty(this.form.NET_TOTAL.Text) && decimal.Parse(this.form.NET_TOTAL.Text) > 0)
                    {
                        rowTmp["NET_JYUURYOU_TOTAL"] = this.form.NET_TOTAL.Text;
                    }
                    // 伝票備考
                    rowTmp["DENPYOU_BIKOU"] = this.form.DENPYOU_BIKOU.Text;

                    //初期化
                    rowTmp["KEIRYOU_JYOUHOU1"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU2"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU3"] = string.Empty;
                    rowTmp["CORP_RYAKU_NAME"] = string.Empty;
                    rowTmp["KYOTEN_NAME"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS1"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS2"] = string.Empty;
                    rowTmp["KYOTEN_TEL"] = string.Empty;
                    rowTmp["KYOTEN_FAX"] = string.Empty;
                    // 会社名
                    if (CommonShogunData.CORP_INFO.CORP_NAME != null)
                        rowTmp["CORP_RYAKU_NAME"] = CommonShogunData.CORP_INFO.CORP_NAME;
                    // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点名
                    if (kyotenEntitys != null)
                    {
                        if (kyotenEntitys.KYOTEN_NAME != null)
                            rowTmp["KYOTEN_NAME"] = kyotenEntitys.KYOTEN_NAME;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所1
                        if (kyotenEntitys.KYOTEN_ADDRESS1 != null)
                            rowTmp["KYOTEN_ADDRESS1"] = kyotenEntitys.KYOTEN_ADDRESS1;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所2
                        if (kyotenEntitys.KYOTEN_ADDRESS2 != null)
                            rowTmp["KYOTEN_ADDRESS2"] = kyotenEntitys.KYOTEN_ADDRESS2;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点TEL
                        if (kyotenEntitys.KYOTEN_TEL != null)
                            rowTmp["KYOTEN_TEL"] = kyotenEntitys.KYOTEN_TEL;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点FAX
                        if (kyotenEntitys.KYOTEN_FAX != null)
                            rowTmp["KYOTEN_FAX"] = kyotenEntitys.KYOTEN_FAX;
                        //計量情報計量証明項目1
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_1 != null)
                            rowTmp["KEIRYOU_JYOUHOU1"] = kyotenEntitys.KEIRYOU_SHOUMEI_1;
                        //計量情報計量証明項目2
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_2 != null)
                            rowTmp["KEIRYOU_JYOUHOU2"] = kyotenEntitys.KEIRYOU_SHOUMEI_2;
                        //計量情報計量証明項目3
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_3 != null)
                            rowTmp["KEIRYOU_JYOUHOU3"] = kyotenEntitys.KEIRYOU_SHOUMEI_3;
                    }

                    dataTableTmpF.Rows.Add(rowTmp);

                    #endregion - 三つ切り 複数品目 取引先無し -

                    break;

                case ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.SingleH_NoTorihikisaki:   // 三つ切り 単品目 取引先無し

                    #region - 三つ切り 単品目 取引先無し -

                    // Header部

                    // 計量証明書タイトル
                    dataTableTmpH.Columns.Add("KEIRYOU_HYOU_TITLE");
                    // 伝票日付
                    dataTableTmpH.Columns.Add("DENPYOU_DATE");
                    // 伝票番号
                    dataTableTmpH.Columns.Add("DENPYOU_NUMBER");
                    // 業者CD
                    dataTableTmpH.Columns.Add("GYOUSHA_CD");
                    // 業者名
                    dataTableTmpH.Columns.Add("GYOUSHA_NAME");
                    // 業者敬称
                    dataTableTmpH.Columns.Add("GYOUSHA_KEISYOU");
                    // 現場CD
                    dataTableTmpH.Columns.Add("GENBA_CD");
                    // 現場名
                    dataTableTmpH.Columns.Add("GENBA_NAME");
                    // 現場敬称
                    dataTableTmpH.Columns.Add("GENBA_KEISYOU");
                    // 車輌
                    dataTableTmpH.Columns.Add("SHARYOU");
                    // バーコード
                    dataTableTmpH.Columns.Add("BARCODE");

                    rowTmp = dataTableTmpH.NewRow();

                    // 計量票タイトル
                    rowTmp["KEIRYOU_HYOU_TITLE"] = this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_1 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_2 + ","
                                                + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_3;
                    // 伝票日付
                    if (this.form.DENPYOU_DATE.Value != null)
                    {
                        rowTmp["DENPYOU_DATE"] = Date.ToString("yyyy/MM/dd");
                    }
                    // 伝票番号
                    if (this.dto.entryEntity != null && !this.dto.entryEntity.KEIRYOU_NUMBER.IsNull
                        && !this.dto.entryEntity.KEIRYOU_NUMBER.ToString().Equals(this.form.ENTRY_NUMBER.ToString()))
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.dto.entryEntity.KEIRYOU_NUMBER.ToString();
                    }
                    else
                    {
                        rowTmp["DENPYOU_NUMBER"] = this.form.ENTRY_NUMBER.Text;
                    }

                    // 業者CD
                    rowTmp["GYOUSHA_CD"] = this.form.GYOUSHA_CD.Text;
                    // 業者マスタ．業者名1
                    rowTmp["GYOUSHA_NAME"] = string.Empty;
                    if (gyousEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の業者
                        rowTmp["GYOUSHA_NAME"] = this.form.GYOUSHA_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (gyousEntity.GYOUSHA_NAME1 != null)
                        {
                            if (gyousEntity.GYOUSHA_NAME2 != null)
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1 + "\n" + gyousEntity.GYOUSHA_NAME2;
                            }
                            else
                            {
                                rowTmp["GYOUSHA_NAME"] = gyousEntity.GYOUSHA_NAME1;
                            }
                        }
                    }
                    // 業者マスタ．業者敬称1
                    rowTmp["GYOUSHA_KEISYOU"] = string.Empty;
                    if (gyousEntity.GYOUSHA_KEISHOU1 != null)
                    {
                        if (gyousEntity.GYOUSHA_KEISHOU2 != null)
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1 + "\n" + gyousEntity.GYOUSHA_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["GYOUSHA_KEISYOU"] = gyousEntity.GYOUSHA_KEISHOU1;
                        }
                    }
                    // 現場CD
                    rowTmp["GENBA_CD"] = this.form.GENBA_CD.Text;
                    // 現場マスタ．現場名1
                    rowTmp["GENBA_NAME"] = string.Empty;
                    if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        // 諸口の現場
                        rowTmp["GENBA_NAME"] = this.form.GENBA_NAME_RYAKU.Text + "\n ";
                    }
                    else
                    {
                        // 諸口以外
                        if (genbaEntity.GENBA_NAME1 != null)
                        {
                            if (genbaEntity.GENBA_NAME2 != null)
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1 + "\n" + genbaEntity.GENBA_NAME2;
                            }
                            else
                            {
                                rowTmp["GENBA_NAME"] = genbaEntity.GENBA_NAME1;
                            }
                        }
                    }

                    // 現場マスタ．現場敬称1
                    rowTmp["GENBA_KEISYOU"] = string.Empty;
                    if (genbaEntity.GENBA_KEISHOU1 != null)
                    {
                        if (genbaEntity.GENBA_KEISHOU2 != null)
                        {
                            rowTmp["GENBA_KEISYOU"] = genbaEntity.GENBA_KEISHOU1 + "\n" + genbaEntity.GENBA_KEISHOU2;
                        }
                        else
                        {
                            rowTmp["GENBA_KEISYOU"] = genbaEntity.GENBA_KEISHOU1;
                        }
                    }

                    // 車輌
                    rowTmp["SHARYOU"] = this.form.SHARYOU_NAME_RYAKU.Text;

                    //バーコード（02＋計量番号＋差額）
                    rowTmp["BARCODE"] = "02"
                            + this.form.ENTRY_NUMBER.Text.PadLeft(4, '0')
                            + Convert.ToString(this.form.SAGAKU.Text.ToString()).Replace(",", "").Replace("-", "").PadLeft(6, '0');

                    dataTableTmpH.Rows.Add(rowTmp);

                    // Detail部
                    // 品名CD
                    dataTableTmpD.Columns.Add("HINMEI_CD");
                    // 品名
                    dataTableTmpD.Columns.Add("HINMEI_NAME");
                    // 総重量
                    dataTableTmpD.Columns.Add("STACK_JYUURYOU");
                    // 空車重量
                    dataTableTmpD.Columns.Add("EMPTY_JYUURYOU");
                    // 調整
                    dataTableTmpD.Columns.Add("NET_CHOUSEI");
                    // 容器引
                    dataTableTmpD.Columns.Add("YOUKI_JYUURYOU");
                    // 正味
                    dataTableTmpD.Columns.Add("NET_JYUURYOU");
                    //総重量計量時間
                    dataTableTmpD.Columns.Add("STACK_KEIRYOU_TIME");
                    //空車重量計量時間
                    dataTableTmpD.Columns.Add("EMPTY_KEIRYOU_TIME");
                    // 計量時間
                    dataTableTmpD.Columns.Add("NET_JYUURYOU_TIME");

                    rowTmp = dataTableTmpD.NewRow();

                    // 未確定行は無視
                    rowTmp["HINMEI_CD"] = string.Empty;
                    rowTmp["HINMEI_NAME"] = string.Empty;
                    rowTmp["STACK_JYUURYOU"] = string.Empty;
                    rowTmp["EMPTY_JYUURYOU"] = string.Empty;
                    rowTmp["STACK_KEIRYOU_TIME"] = string.Empty;
                    rowTmp["EMPTY_KEIRYOU_TIME"] = string.Empty;
                    rowTmp["NET_JYUURYOU_TIME"] = string.Empty;
                    if (this.form.gcMultiRow1.Rows.Count > 1)
                    {
                        // 品名CD
                        rowTmp["HINMEI_CD"] = this.form.gcMultiRow1.Rows[0].Cells[CELL_NAME_HINMEI_CD].Value;
                        rowTmp["HINMEI_NAME"] = this.form.gcMultiRow1.Rows[0].Cells[CELL_NAME_HINMEI_NAME].Value;
                        // 総重量(画面に表示されている重量値系はフォーマット済みなので、そのまま使用する)
                        rowTmp["STACK_JYUURYOU"] = this.form.STACK_JYUURYOU.Text;
                        // 空車重量(画面に表示されている重量値系はフォーマット済みなので、そのまま使用する)
                        rowTmp["EMPTY_JYUURYOU"] = this.form.EMPTY_JYUURYOU.Text;
                        // 総重量計量時間
                        rowTmp["STACK_KEIRYOU_TIME"] = this.form.STACK_KEIRYOU_TIME.Text;
                        // 空車重量計量時間
                        rowTmp["EMPTY_KEIRYOU_TIME"] = this.form.EMPTY_KEIRYOU_TIME.Text;
                        // 正味重量計量時間
                        rowTmp["NET_JYUURYOU_TIME"] = this.form.gcMultiRow1.Rows[0].Cells[CELL_NAME_KEIRYOU_TIME].Value;
                    }

                    // 調整
                    if (chosei > 0)
                    {
                        rowTmp["NET_CHOUSEI"] = chosei.ToString(this.dto.sysInfoEntity.SYS_JYURYOU_FORMAT);
                    }
                    // 容器引
                    if (youki > 0)
                    {
                        rowTmp["YOUKI_JYUURYOU"] = youki.ToString(this.dto.sysInfoEntity.SYS_JYURYOU_FORMAT);
                    }

                    // 正味
                    if (!string.IsNullOrEmpty(this.form.NET_TOTAL.Text) && decimal.Parse(this.form.NET_TOTAL.Text) > 0)
                    {
                        rowTmp["NET_JYUURYOU"] = this.form.gcMultiRow1.Rows[0].Cells[CELL_NAME_NET_JYUURYOU].DisplayText;
                    }

                    dataTableTmpD.Rows.Add(rowTmp);

                    // Footer部

                    // 伝票備考
                    dataTableTmpF.Columns.Add("DENPYOU_BIKOU");
                    // 計量情報計量証明項目1
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU1");
                    // 計量情報計量証明項目2
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU2");
                    // 計量情報計量証明項目3
                    dataTableTmpF.Columns.Add("KEIRYOU_JYOUHOU3");
                    // 会社名
                    dataTableTmpF.Columns.Add("CORP_RYAKU_NAME");
                    // 拠点
                    dataTableTmpF.Columns.Add("KYOTEN_NAME");
                    // 拠点住所1
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS1");
                    // 拠点住所2
                    dataTableTmpF.Columns.Add("KYOTEN_ADDRESS2");
                    // 拠点電話
                    dataTableTmpF.Columns.Add("KYOTEN_TEL");
                    // 拠点FAX
                    dataTableTmpF.Columns.Add("KYOTEN_FAX");

                    rowTmp = dataTableTmpF.NewRow();

                    //初期化
                    rowTmp["KEIRYOU_JYOUHOU1"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU2"] = string.Empty;
                    rowTmp["KEIRYOU_JYOUHOU3"] = string.Empty;
                    rowTmp["DENPYOU_BIKOU"] = string.Empty;
                    rowTmp["CORP_RYAKU_NAME"] = string.Empty;
                    rowTmp["KYOTEN_NAME"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS1"] = string.Empty;
                    rowTmp["KYOTEN_ADDRESS2"] = string.Empty;
                    rowTmp["KYOTEN_TEL"] = string.Empty;
                    rowTmp["KYOTEN_FAX"] = string.Empty;

                    //伝票備考
                    rowTmp["DENPYOU_BIKOU"] = this.form.DENPYOU_BIKOU.Text;

                    // 会社名
                    if (CommonShogunData.CORP_INFO.CORP_NAME != null)
                        rowTmp["CORP_RYAKU_NAME"] = CommonShogunData.CORP_INFO.CORP_NAME;
                    // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点名
                    if (kyotenEntitys != null)
                    {
                        if (kyotenEntitys.KYOTEN_NAME != null)
                            rowTmp["KYOTEN_NAME"] = kyotenEntitys.KYOTEN_NAME;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所1
                        if (kyotenEntitys.KYOTEN_ADDRESS1 != null)
                            rowTmp["KYOTEN_ADDRESS1"] = kyotenEntitys.KYOTEN_ADDRESS1;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点住所2
                        if (kyotenEntitys.KYOTEN_ADDRESS2 != null)
                            rowTmp["KYOTEN_ADDRESS2"] = kyotenEntitys.KYOTEN_ADDRESS2;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点TEL
                        if (kyotenEntitys.KYOTEN_TEL != null)
                            rowTmp["KYOTEN_TEL"] = kyotenEntitys.KYOTEN_TEL;
                        // 拠点マスタ.拠点CD＝0の拠点マスタ.拠点FAX
                        if (kyotenEntitys.KYOTEN_FAX != null)
                            rowTmp["KYOTEN_FAX"] = kyotenEntitys.KYOTEN_FAX;
                        //計量情報計量証明項目1
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_1 != null)
                            rowTmp["KEIRYOU_JYOUHOU1"] = kyotenEntitys.KEIRYOU_SHOUMEI_1;
                        //計量情報計量証明項目2
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_2 != null)
                            rowTmp["KEIRYOU_JYOUHOU2"] = kyotenEntitys.KEIRYOU_SHOUMEI_2;
                        //計量情報計量証明項目3
                        if (kyotenEntitys.KEIRYOU_SHOUMEI_3 != null)
                            rowTmp["KEIRYOU_JYOUHOU3"] = kyotenEntitys.KEIRYOU_SHOUMEI_3;
                    }

                    dataTableTmpF.Rows.Add(rowTmp);

                    #endregion - 三つ切り 単品目 取引先無し -

                    break;
            }

            // Header部
            reportInfo.DataTableList.Add("Header", dataTableTmpH);
            // Detail部
            reportInfo.DataTableList.Add("Detail", dataTableTmpD);
            // Footer部
            reportInfo.DataTableList.Add("Footer", dataTableTmpF);

            return reportInfo;

        }
        //---計量票追加---------------------------------------------------------------------------------------------------------------------------


        /// <summary>

        /// 明細に新規行を追加
        /// </summary>
        internal void AddNewRow()
        {
            LogUtility.DebugMethodStart();

            if ((Row)this.form.gcMultiRow1.CurrentRow != null)
            {
                this.form.gcMultiRow1.EndEdit();
                Row selectedRows = (Row)this.form.gcMultiRow1.CurrentRow;

                int iSaveRowIndex = this.form.gcMultiRow1.CurrentRow.Index;
                this.form.gcMultiRow1.Rows.Insert(this.form.gcMultiRow1.CurrentRow.Index);
                this.form.gcMultiRow1.ClearSelection();

                this.form.gcMultiRow1.AddSelection(iSaveRowIndex);

                this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
                // 行番号採番
                if (!this.NumberingRowNo())
                {
                    return;
                }
            }

            LogUtility.DebugMethodStart();
        }

        /// <summary>
        /// 明細のカレント行を削除
        /// </summary>
        internal bool RemoveSelectedRow()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if ((Row)this.form.gcMultiRow1.CurrentRow != null)
                {
                    this.form.gcMultiRow1.EndEdit();
                    Row selectedRows = (Row)this.form.gcMultiRow1.CurrentRow;
                    if (!selectedRows.IsNewRow)
                    {
                        // 行削除の後に現在のCellのフォーカスアウトチェックが走ってしまうので、FocusOutCheckMethodを削除
                        var currentCell = this.form.gcMultiRow1.CurrentCell as ICustomControl;
                        if (currentCell != null)
                        {
                            currentCell.FocusOutCheckMethod = null;
                        }

                        // 行削除
                        int iSaveIndex = this.form.gcMultiRow1.CurrentRow.Index;
                        this.form.gcMultiRow1.Rows.Remove(selectedRows);
                        this.form.gcMultiRow1.ClearSelection();
                    }
                    this.form.gcMultiRow1.EndEdit();
                    this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
                    // 行番号採番
                    if (!this.NumberingRowNo())
                    {
                        ret = false;
                        return ret;
                    }
                    this.form.gcMultiRow1.ResumeLayout();

                    // 計算
                    if (!this.CalcDetail())
                    {
                        ret = false;
                        return ret;
                    }

                }

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("RemoveSelectedRow", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("RemoveSelectedRow", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 伝票区分設定
        /// 明細の品名から伝票区分を設定する
        /// </summary>
        internal bool SetDenpyouKbn()
        {
            LogUtility.DebugMethodStart();

            Row targetRow = this.form.gcMultiRow1.CurrentRow;

            if (targetRow == null)
            {
                return true;
            }

            // 初期化
            targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value = string.Empty;
            targetRow.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = string.Empty;

            if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value)))
            {
                return true;
            }

            M_HINMEI[] hinmeis = this.accessor.GetAllValidHinmeiData(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());

            if (hinmeis == null || hinmeis.Count() < 1)
            {
                // 存在しない品名が選択されている場合
                return true;
            }
            var targetHimei = hinmeis[0];

            switch (targetHimei.DENPYOU_KBN_CD.ToString())
            {
                case SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE_STR:
                case SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI_STR:
                    targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value = (short)targetHimei.DENPYOU_KBN_CD;
                    targetRow.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = denpyouKbnDictionary[(short)targetHimei.DENPYOU_KBN_CD].DENPYOU_KBN_NAME_RYAKU;
                    break;

                default:
                    // ポップアップを打ち上げ、ユーザに選択してもらう
                    CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                    CustomControlExtLogic.PopUp((ICustomControl)this.form.gcMultiRow1.Rows[pos.RowIndex].Cells[CELL_NAME_DENPYOU_KBN_CD]);

                    var denpyouKbnCd = targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value;
                    if (string.IsNullOrEmpty(Convert.ToString(denpyouKbnCd)))
                    {
                        // ポップアップでキャンセルが押された
                        // ※ポップアップで何を押されたか判断できないので、CDの存在チェックで対応
                        targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = string.Empty;
                        targetRow.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = string.Empty;
                        targetRow.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value = string.Empty;

                        //ポップアップキャンセルフラグをTrueにする。
                        this.form.bCancelDenpyoPopup = true;

                        return false;
                    }

                    break;
            }

            LogUtility.DebugMethodStart();

            return true;
        }

        /// <summary>
        /// 運転者チェック
        /// </summary>
        internal bool CheckUntensha()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                //参照モード、削除モードの場合は処理を行わない
                if (this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG ||
                    this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    return true;
                }

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(this.form.UNTENSHA_CD.Text) || !this.tmpUntenshaCd.Equals(this.form.UNTENSHA_CD.Text)) || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.UNTENSHA_NAME.Text = string.Empty;

                    if (string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
                    {
                        // 運転者CDがなければ既にエラーが表示されているので何もしない。
                        return true;
                    }

                    var shainEntity = this.accessor.GetShain(this.form.UNTENSHA_CD.Text);
                    if (shainEntity == null)
                    {
                        // エラーメッセージ
                        this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                        this.form.UNTENSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "社員");
                        this.form.UNTENSHA_CD.Focus();
                        this.tmpUntenshaCd = string.Empty;
                        return true;
                    }
                    else if (shainEntity.UNTEN_KBN.Equals(SqlBoolean.False))
                    {
                        // エラーメッセージ
                        this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                        this.form.UNTENSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "運転者");
                        this.form.UNTENSHA_CD.Focus();
                        this.tmpUntenshaCd = string.Empty;
                        return true;
                    }
                    else
                    {
                        this.form.UNTENSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                    }
                }

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckUntensha", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUntensha", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        internal bool GetHinmei(Row targetRow, out bool catchErr)
        {
            catchErr = true;
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value)))
                {
                    // 品名コードの入力がない場合
                    return returnVal;
                }

                M_HINMEI[] hinmeis = this.accessor.GetAllValidHinmeiData(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());
                if (hinmeis != null && hinmeis.Count() > 0)
                {
                    targetRow.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value = hinmeis[0].ZEI_KBN_CD;
                }

                M_KOBETSU_HINMEI kobetsuHinmeis = this.accessor.GetKobetsuHinmeiDataByCd(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString(), Convert.ToInt16(this.form.selectDenshuKbnCd));
                if (kobetsuHinmeis != null)
                {
                    targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = kobetsuHinmeis.SEIKYUU_HINMEI_NAME;
                }
                else
                {
                    if (hinmeis != null && hinmeis.Count() > 0)
                    {
                        targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = hinmeis[0].HINMEI_NAME;
                    }
                    else
                    {
                        returnVal = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetHinmei", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetHinmei", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }

        internal bool GetHinmeiForPop(Row targetRow)
        {
            LogUtility.DebugMethodStart();
            bool returnVal = false;

            if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value)))
            {
                // 品名コードの入力がない場合
                return returnVal;
            }

            M_HINMEI[] hinmeis = this.accessor.GetAllValidHinmeiData(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());
            if (hinmeis != null && hinmeis.Count() > 0)
            {
                targetRow.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value = hinmeis[0].ZEI_KBN_CD;
            }

            M_KOBETSU_HINMEI kobetsuHinmeis = this.accessor.GetKobetsuHinmeiDataByCd(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString(), SalesPaymentConstans.DENSHU_KBN_CD_UKEIRE);
            if (kobetsuHinmeis != null)
            {
                targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = kobetsuHinmeis.SEIKYUU_HINMEI_NAME;
            }
            else
            {
                if (hinmeis != null && hinmeis.Count() > 0)
                {
                    targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = hinmeis[0].HINMEI_NAME;
                }
            }
            LogUtility.DebugMethodEnd();
            return returnVal;
        }

        /// <summary>
        /// 品名コードの存在チェック（伝種区分が受入/出荷、または共通のみ可）
        /// </summary>
        /// <param name="targetRow"></param>
        /// <returns>true: 入力された品名コードが存在する, false: 入力された品名コードが存在しない</returns>
        internal bool CheckHinmeiCd(Row targetRow, out bool catchErr)
        {
            catchErr = true;
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value)))
                {
                    // 品名コードの入力がない場合
                    return returnVal;
                }

                M_HINMEI[] hinmeis = this.accessor.GetAllValidHinmeiData(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());
                if (hinmeis == null || hinmeis.Count() < 1)
                {
                    // 品名コードがマスタに存在しない場合
                    // ただし、部品で存在チェックが行われるため、実際にここを通ることはない
                    return returnVal;
                }
                else
                {
                    M_HINMEI hinmei = hinmeis[0];
                    // 品名コードがマスタに存在する場合
                    if ((hinmei.DENSHU_KBN_CD != Convert.ToInt16(this.form.selectDenshuKbnCd)
                        && hinmei.DENSHU_KBN_CD != Convert.ToInt16(DENSHU_KBN.KYOUTSUU)))
                    {
                        // 入力された品名コードに紐づく伝種区分が受入/出荷、共通以外の場合はエラーメッセージ表示
                        targetRow.Cells[CELL_NAME_HINMEI_CD].Value = null;
                        targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = null;
                        targetRow.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value = null;
                        targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value = null;
                        targetRow.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = null;
                        targetRow.Cells[CELL_NAME_UNIT_CD].Value = null;
                        targetRow.Cells[CELL_NAME_UNIT_NAME_RYAKU].Value = null;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E058", "");
                        return returnVal;
                    }
                }

                returnVal = true;

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckHinmeiCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHinmeiCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
            return returnVal;
        }

        #endregion

        #region Utility
        /// <summary>
        /// WINDOWTYPEからデータ取得が必要かどうか判断します
        /// </summary>
        /// <returns>True:データ取得が必要, Flase:データ取得が不必要</returns>
        private bool IsRequireData()
        {
            if (WINDOW_TYPE.DELETE_WINDOW_FLAG.Equals(this.form.WindowType)
                || WINDOW_TYPE.UPDATE_WINDOW_FLAG.Equals(this.form.WindowType)
                || WINDOW_TYPE.REFERENCE_WINDOW_FLAG.Equals(this.form.WindowType))
            {
                return true;
            }
            if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType)
                && this.form.KeiryouNumber != -1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// コントロールから登録用のEntityを作成する
        /// </summary>
        public virtual void CreateEntity(bool tairyuuKbnFlag)
        {
            LogUtility.DebugMethodStart(tairyuuKbnFlag);

            this.dto.numberReceipt = new S_NUMBER_RECEIPT();
            this.dto.numberReceiptYear = new S_NUMBER_RECEIPT_YEAR();

            if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG) ||
                this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                this.dto.entryEntity = new T_KEIRYOU_ENTRY();
                this.dto.JentryEntity = new T_UKEIRE_JISSEKI_ENTRY();
                this.dto.numberDay = new S_NUMBER_DAY();
                this.dto.numberYear = new S_NUMBER_YEAR();
            }

            // T_KEIRYOU_ENTRYの設定
            // 日連番取得
            S_NUMBER_DAY[] numberDays = null;
            DateTime denpyouDate = Convert.ToDateTime(this.form.DENPYOU_DATE.Value);

            short kyotenCd = -1;    // 拠点CD
            short.TryParse(this.headerForm.KYOTEN_CD.Text, out kyotenCd);
            if (DateTime.TryParse(this.form.DENPYOU_DATE.Value.ToString(), out denpyouDate)
                && -1 < kyotenCd)
            {
                numberDays = this.accessor.GetNumberDay(denpyouDate.Date, SalesPaymentConstans.DENSHU_KBN_CD_KEIRYOU, kyotenCd);
            }

            // 年連番取得(S_NUMBER_YEARテーブルから情報取得 + 年度の生成処理を追加)
            S_NUMBER_YEAR[] numberYeas = null;
            SqlInt32 numberedYear = CorpInfoUtility.GetCurrentYear(denpyouDate.Date, (short)CommonShogunData.CORP_INFO.KISHU_MONTH);
            if (-1 < kyotenCd)
            {
                numberYeas = this.accessor.GetNumberYear(numberedYear, SalesPaymentConstans.DENSHU_KBN_CD_KEIRYOU, kyotenCd);
            }

            // S_NUMBER_RECEIPTの更新

            // 領収証番号採番
            if (ConstClass.RYOSYUSYO_KBN_1.Equals(this.form.RECEIPT_KBN_CD.Text) && this.form.RECEIPT_KBN_CD.Enabled)
            {
                /* 日連番 */
                var numberReceipt = this.accessor.GetNumberReceipt(denpyouDate, kyotenCd);
                if (numberReceipt == null)
                {
                    this.dto.numberReceipt.CURRENT_NUMBER = 1;
                }
                else
                {
                    int numberReceiptCurrentNumber = -1;
                    int.TryParse(Convert.ToString(numberReceipt.CURRENT_NUMBER), out numberReceiptCurrentNumber);
                    this.dto.numberReceipt.CURRENT_NUMBER = numberReceiptCurrentNumber + 1;
                    this.dto.numberReceipt.TIME_STAMP = numberReceipt.TIME_STAMP;
                }

                this.dto.numberReceipt.NUMBERED_DAY = denpyouDate.Date;
                this.dto.numberReceipt.KYOTEN_CD = kyotenCd;
                this.dto.numberReceipt.DELETE_FLG = false;
                var dataBinderNumberReceipt = new DataBinderLogic<S_NUMBER_RECEIPT>(this.dto.numberReceipt);
                dataBinderNumberReceipt.SetSystemProperty(this.dto.numberReceipt, false);

                /* 年連番 */
                var numberReceiptYear = this.accessor.GetNumberReceiptYear(denpyouDate, kyotenCd);
                if (numberReceiptYear == null)
                {
                    this.dto.numberReceiptYear.CURRENT_NUMBER = 1;
                }
                else
                {
                    int numberReceiptYearCurrentNumber = -1;
                    int.TryParse(Convert.ToString(numberReceiptYear.CURRENT_NUMBER), out numberReceiptYearCurrentNumber);
                    this.dto.numberReceiptYear.CURRENT_NUMBER = numberReceiptYearCurrentNumber + 1;
                    this.dto.numberReceiptYear.TIME_STAMP = numberReceiptYear.TIME_STAMP;
                }

                this.dto.numberReceiptYear.NUMBERED_YEAR = (SqlInt16)denpyouDate.Year;
                this.dto.numberReceiptYear.KYOTEN_CD = kyotenCd;
                this.dto.numberReceiptYear.DELETE_FLG = false;
                var dataBinderNumberReceiptYear = new DataBinderLogic<S_NUMBER_RECEIPT_YEAR>(this.dto.numberReceiptYear);
                dataBinderNumberReceiptYear.SetSystemProperty(this.dto.numberReceiptYear, false);
            }

            // モードに依存する処理
            byte[] numberDayTimeStamp = null;
            byte[] numberYearTimeStamp = null;
            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    // SYSTEM_IDの採番
                    SqlInt64 systemId = this.accessor.CreateSystemIdForKeiryou();
                    this.dto.entryEntity.SYSTEM_ID = systemId;

                    // 計量番号の採番
                    this.dto.entryEntity.KEIRYOU_NUMBER = this.accessor.CreateKeiryouNumber();

                    // 日連番
                    if (numberDays == null || numberDays.Length < 1)
                    {
                        this.hiRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT;   // DB更新時に使用
                        this.dto.entryEntity.DATE_NUMBER = 1;
                    }
                    else
                    {
                        this.hiRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE;   // DB更新時に使用
                        this.dto.entryEntity.DATE_NUMBER = numberDays[0].CURRENT_NUMBER + 1;
                        numberDayTimeStamp = numberDays[0].TIME_STAMP;
                    }

                    // 年連番
                    if (numberYeas == null || numberYeas.Length < 1)
                    {
                        this.nenRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT;   // DB更新時に使用
                        this.dto.entryEntity.YEAR_NUMBER = 1;
                    }
                    else
                    {
                        this.nenRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE;   // DB更新時に使用
                        this.dto.entryEntity.YEAR_NUMBER = numberYeas[0].CURRENT_NUMBER + 1;
                        numberYearTimeStamp = numberYeas[0].TIME_STAMP;
                    }

                    this.dto.entryEntity.SEQ = 1;
                    this.dto.entryEntity.DELETE_FLG = false;

                    break;

                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    // 画面表示時にSYSTEM_IDを取得しているため採番は割愛

                    // 日連番
                    short beforeKotenCd = -1;
                    short.TryParse(beforDto.entryEntity.KYOTEN_CD.ToString(), out beforeKotenCd);
                    if ((beforeKotenCd != kyotenCd
                        || beforDto.entryEntity.KYOTEN_CD != kyotenCd)
                        || !beforDto.entryEntity.DENPYOU_DATE.Equals((SqlDateTime)denpyouDate))
                    {
                        if (numberDays == null || numberDays.Length < 1)
                        {
                            this.hiRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT;   // DB更新時に使用
                            this.dto.entryEntity.DATE_NUMBER = 1;
                        }
                        else
                        {
                            this.hiRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE;   // DB更新時に使用
                            this.dto.entryEntity.DATE_NUMBER = numberDays[0].CURRENT_NUMBER + 1;
                            numberDayTimeStamp = numberDays[0].TIME_STAMP;
                        }
                    }
                    else
                    {
                        this.hiRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.NONE;   // DB更新時に使用
                        this.dto.entryEntity.DATE_NUMBER = this.beforDto.entryEntity.DATE_NUMBER;
                        numberDayTimeStamp = numberDays[0].TIME_STAMP;
                    }
                    // 年連番
                    SqlInt32 beforNumberedYear = CorpInfoUtility.GetCurrentYear((DateTime)beforDto.entryEntity.DENPYOU_DATE, (short)CommonShogunData.CORP_INFO.KISHU_MONTH);
                    if ((beforeKotenCd != kyotenCd
                        || beforDto.entryEntity.KYOTEN_CD != kyotenCd)
                        || (numberYeas == null || numberYeas.Length < 1 || beforNumberedYear.Value != numberYeas[0].NUMBERED_YEAR.Value))
                    {
                        if (numberYeas == null || numberYeas.Length < 1)
                        {
                            this.nenRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT;   // DB更新時に使用
                            this.dto.entryEntity.YEAR_NUMBER = 1;
                        }
                        else
                        {
                            this.nenRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE;   // DB更新時に使用
                            this.dto.entryEntity.YEAR_NUMBER = numberYeas[0].CURRENT_NUMBER + 1;
                            numberYearTimeStamp = numberYeas[0].TIME_STAMP;
                        }
                    }
                    else
                    {
                        this.nenRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.NONE;   // DB更新時に使用
                        this.dto.entryEntity.YEAR_NUMBER = this.beforDto.entryEntity.YEAR_NUMBER;
                        numberYearTimeStamp = numberYeas[0].TIME_STAMP;
                    }

                    this.dto.entryEntity.KEIRYOU_NUMBER = SqlInt64.Parse(this.form.ENTRY_NUMBER.Text);
                    this.dto.entryEntity.SYSTEM_ID = this.beforDto.entryEntity.SYSTEM_ID; // 更新されないはず
                    this.dto.entryEntity.SEQ = this.beforDto.entryEntity.SEQ + 1;
                    this.dto.entryEntity.DELETE_FLG = false;
                    // 更新前伝票は論理削除
                    this.beforDto.entryEntity.DELETE_FLG = true;
                    // 排他制御用
                    this.beforDto.entryEntity.TIME_STAMP = ConvertStrByte.StringToByte(this.form.ENTRY_TIME_STAMP.Text);

                    break;

                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    this.dto.entryEntity.DELETE_FLG = true;
                    this.dto.entryEntity.TIME_STAMP = ConvertStrByte.StringToByte(this.form.ENTRY_TIME_STAMP.Text);
                    break;

            }

            this.dto.torihikisakiSeikyuuEntity = this.accessor.GetTorihikisakiSeikyuu(this.form.TORIHIKISAKI_CD.Text);
            if (this.dto.torihikisakiSeikyuuEntity == null)
            {
                this.dto.torihikisakiSeikyuuEntity = new M_TORIHIKISAKI_SEIKYUU();
            }

            this.dto.torihikisakiShiharaiEntity = this.accessor.GetTorihikisakiShiharai(this.form.TORIHIKISAKI_CD.Text);
            if (this.dto.torihikisakiShiharaiEntity == null)
            {
                this.dto.torihikisakiShiharaiEntity = new M_TORIHIKISAKI_SHIHARAI();
            }

            this.form.KeiryouSysId = this.dto.entryEntity.SYSTEM_ID.Value;
            // 滞留区分
            // 削除モード時は書き換えない
            if (WINDOW_TYPE.DELETE_WINDOW_FLAG != this.form.WindowType)
            {
                this.dto.entryEntity.TAIRYUU_KBN = tairyuuKbnFlag;
            }

            if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
            {
                this.dto.entryEntity.KYOTEN_CD = SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text);
            }
            if (!string.IsNullOrEmpty(this.headerForm.KIHON_KEIRYOU.Text))
            {
                this.dto.entryEntity.KIHON_KEIRYOU = SqlInt16.Parse(this.headerForm.KIHON_KEIRYOU.Text);
            }

            if (!string.IsNullOrEmpty(this.form.NYUURYOKU_TANTOUSHA_CD.Text))
            {
                this.dto.entryEntity.NYUURYOKU_TANTOUSHA_CD = this.form.NYUURYOKU_TANTOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NYUURYOKU_TANTOUSHA_NAME.Text))
            {
                this.dto.entryEntity.NYUURYOKU_TANTOUSHA_NAME = this.form.NYUURYOKU_TANTOUSHA_NAME.Text;
            }
            if (this.form.DENPYOU_DATE.Value != null)
            {
                this.dto.entryEntity.DENPYOU_DATE = ((DateTime)this.form.DENPYOU_DATE.Value).Date;
            }
            if (!string.IsNullOrEmpty(this.form.KEIRYOU_KBN_CD.Text))
            {
                this.dto.entryEntity.KEIRYOU_KBN = SqlInt16.Parse(this.form.KEIRYOU_KBN_CD.Text);
            }

            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                this.dto.entryEntity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_NAME_RYAKU.Text))
            {
                this.dto.entryEntity.GYOUSHA_NAME = this.form.GYOUSHA_NAME_RYAKU.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                this.dto.entryEntity.GENBA_CD = this.form.GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GENBA_NAME_RYAKU.Text))
            {
                this.dto.entryEntity.GENBA_NAME = this.form.GENBA_NAME_RYAKU.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                this.dto.entryEntity.UNPAN_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_NAME.Text))
            {
                this.dto.entryEntity.UNPAN_GYOUSHA_NAME = this.form.UNPAN_GYOUSHA_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
            {
                this.dto.entryEntity.SHARYOU_CD = this.form.SHARYOU_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHARYOU_NAME_RYAKU.Text))
            {
                this.dto.entryEntity.SHARYOU_NAME = this.form.SHARYOU_NAME_RYAKU.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
            {
                this.dto.entryEntity.SHASHU_CD = this.form.SHASHU_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHASHU_NAME.Text))
            {
                this.dto.entryEntity.SHASHU_NAME = this.form.SHASHU_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
            {
                this.dto.entryEntity.UNTENSHA_CD = this.form.UNTENSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_NAME.Text))
            {
                this.dto.entryEntity.UNTENSHA_NAME = this.form.UNTENSHA_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.STACK_JYUURYOU.Text))
            {
                this.dto.entryEntity.STACK_JYUURYOU = Convert.ToDecimal(this.form.STACK_JYUURYOU.Text);
            }
            if (!string.IsNullOrEmpty(this.form.STACK_KEIRYOU_TIME.Text))
            {
                this.dto.entryEntity.STACK_KEIRYOU_TIME = SqlDateTime.Parse(this.form.STACK_KEIRYOU_TIME.Text);
            }
            if (!string.IsNullOrEmpty(this.form.EMPTY_JYUURYOU.Text))
            {
                this.dto.entryEntity.EMPTY_JYUURYOU = Convert.ToDecimal(this.form.EMPTY_JYUURYOU.Text);
            }
            if (!string.IsNullOrEmpty(this.form.EMPTY_KEIRYOU_TIME.Text))
            {
                this.dto.entryEntity.EMPTY_KEIRYOU_TIME = SqlDateTime.Parse(this.form.EMPTY_KEIRYOU_TIME.Text);
            }
            if (!string.IsNullOrEmpty(this.form.DENPYOU_BIKOU.Text))
            {
                this.dto.entryEntity.DENPYOU_BIKOU = this.form.DENPYOU_BIKOU.Text;
            }
            if (!string.IsNullOrEmpty(this.form.TAIRYUU_BIKOU.Text))
            {
                this.dto.entryEntity.TAIRYUU_BIKOU = this.form.TAIRYUU_BIKOU.Text;
            }

            // マニフェスト
            if (!string.IsNullOrEmpty(this.form.MANIFEST_HAIKI_KBN_CD.Text))
            {
                this.dto.entryEntity.HAIKI_KBN_CD = SqlInt16.Parse(this.form.MANIFEST_HAIKI_KBN_CD.Text);
            }
            if (!string.IsNullOrEmpty(this.form.HST_GYOUSHA_CD.Text))
            {
                this.dto.entryEntity.HST_GYOUSHA_CD = this.form.HST_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.HST_GENBA_CD.Text))
            {
                this.dto.entryEntity.HST_GENBA_CD = this.form.HST_GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SBN_GYOUSHA_CD.Text))
            {
                this.dto.entryEntity.SBN_GYOUSHA_CD = this.form.SBN_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SBN_GENBA_CD.Text))
            {
                this.dto.entryEntity.SBN_GENBA_CD = this.form.SBN_GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.LAST_SBN_GYOUSHA_CD.Text))
            {
                this.dto.entryEntity.LAST_SBN_GYOUSHA_CD = this.form.LAST_SBN_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.LAST_SBN_GENBA_CD.Text))
            {
                this.dto.entryEntity.LAST_SBN_GENBA_CD = this.form.LAST_SBN_GENBA_CD.Text;
            }

            // その他
            if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOUSHA_CD.Text))
            {
                this.dto.entryEntity.EIGYOU_TANTOUSHA_CD = this.form.EIGYOU_TANTOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOUSHA_NAME.Text))
            {
                this.dto.entryEntity.EIGYOU_TANTOUSHA_NAME = this.form.EIGYOU_TANTOUSHA_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
            {
                this.dto.entryEntity.NIOROSHI_GYOUSHA_CD = this.form.NIOROSHI_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_NAME.Text))
            {
                this.dto.entryEntity.NIOROSHI_GYOUSHA_NAME = this.form.NIOROSHI_GYOUSHA_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
            {
                this.dto.entryEntity.NIOROSHI_GENBA_CD = this.form.NIOROSHI_GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_NAME.Text))
            {
                this.dto.entryEntity.NIOROSHI_GENBA_NAME = this.form.NIOROSHI_GENBA_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
            {
                this.dto.entryEntity.NIZUMI_GYOUSHA_CD = this.form.NIZUMI_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_NAME.Text))
            {
                this.dto.entryEntity.NIZUMI_GYOUSHA_NAME = this.form.NIZUMI_GYOUSHA_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text))
            {
                this.dto.entryEntity.NIZUMI_GENBA_CD = this.form.NIZUMI_GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GENBA_NAME.Text))
            {
                this.dto.entryEntity.NIZUMI_GENBA_NAME = this.form.NIZUMI_GENBA_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text))
            {
                this.dto.entryEntity.KEITAI_KBN_CD = SqlInt16.Parse(this.form.KEITAI_KBN_CD.Text);
            }
            if (!string.IsNullOrEmpty(this.form.DAIKAN_KBN.Text))
            {
                this.dto.entryEntity.DAIKAN_KBN = SqlInt16.Parse(this.form.DAIKAN_KBN.Text);
            }
            if (!string.IsNullOrEmpty(this.form.MANIFEST_SHURUI_CD.Text))
            {
                this.dto.entryEntity.MANIFEST_SHURUI_CD = SqlInt16.Parse(this.form.MANIFEST_SHURUI_CD.Text);
            }
            if (!string.IsNullOrEmpty(this.form.MANIFEST_TEHAI_CD.Text))
            {
                this.dto.entryEntity.MANIFEST_TEHAI_CD = SqlInt16.Parse(this.form.MANIFEST_TEHAI_CD.Text);
            }

            // 伝票発行
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                this.dto.entryEntity.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_NAME_RYAKU.Text))
            {
                this.dto.entryEntity.TORIHIKISAKI_NAME = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text))
            {
                this.dto.entryEntity.URIAGE_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text);
            }
            if (!string.IsNullOrEmpty(this.form.SEIKYUU_ZEI_KBN_CD.Text))
            {
                this.dto.entryEntity.URIAGE_ZEI_KBN_CD = SqlInt16.Parse(this.form.SEIKYUU_ZEI_KBN_CD.Text);
            }
            if (!string.IsNullOrEmpty(this.form.SEIKYUU_TORIHIKI_KBN_CD.Text))
            {
                this.dto.entryEntity.URIAGE_TORIHIKI_KBN_CD = SqlInt16.Parse(this.form.SEIKYUU_TORIHIKI_KBN_CD.Text);
            }
            if (!string.IsNullOrEmpty(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text))
            {
                this.dto.entryEntity.SHIHARAI_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text);
            }
            if (!string.IsNullOrEmpty(this.form.SHIHARAI_ZEI_KBN_CD.Text))
            {
                this.dto.entryEntity.SHIHARAI_ZEI_KBN_CD = SqlInt16.Parse(this.form.SHIHARAI_ZEI_KBN_CD.Text);
            }
            if (!string.IsNullOrEmpty(this.form.SEIKYUU_TORIHIKI_KBN_CD.Text))
            {
                this.dto.entryEntity.SHIHARAI_TORIHIKI_KBN_CD = SqlInt16.Parse(this.form.SHIHARAI_TORIHIKI_KBN_CD.Text);
            }

            if (!this.dto.numberReceipt.CURRENT_NUMBER.IsNull)
            {
                this.dto.entryEntity.RECEIPT_NUMBER = this.dto.numberReceipt.CURRENT_NUMBER;
            }
            else if (this.beforDto.entryEntity != null && !this.beforDto.entryEntity.RECEIPT_NUMBER.IsNull)
            {
                // 修正モードで表示、かつ領収書を発行しない場合
                this.dto.entryEntity.RECEIPT_NUMBER = this.beforDto.entryEntity.RECEIPT_NUMBER;
            }
            if (!this.dto.numberReceiptYear.CURRENT_NUMBER.IsNull)
            {
                this.dto.entryEntity.RECEIPT_NUMBER_YEAR = this.dto.numberReceiptYear.CURRENT_NUMBER;
            }
            else if (this.beforDto.entryEntity != null && !this.beforDto.entryEntity.RECEIPT_NUMBER_YEAR.IsNull)
            {
                // 修正モードで表示、かつ領収書を発行しない場合
                this.dto.entryEntity.RECEIPT_NUMBER_YEAR = this.beforDto.entryEntity.RECEIPT_NUMBER_YEAR;
            }

            if (!string.IsNullOrEmpty(this.form.NET_TOTAL.Text))
            {
                this.dto.entryEntity.NET_TOTAL = Convert.ToDecimal(this.form.NET_TOTAL.Text);
            }

            // 売上消費税
            this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE = 0;

            if (!string.IsNullOrEmpty(this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text))
            {
                this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE = this.ToDecimalForUriageShouhizeiRate();
            }
            else
            {
                DateTime uriageDate;
                if (this.form.DENPYOU_DATE.Value != null
                    && DateTime.TryParse(this.form.DENPYOU_DATE.Value.ToString(), out uriageDate))
                {
                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(uriageDate.Date);
                    if (shouhizeiEntity != null
                        && 0 < shouhizeiEntity.SHOUHIZEI_RATE)
                    {
                        this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE = shouhizeiEntity.SHOUHIZEI_RATE;
                    }
                }
            }

            // 支払消費税
            this.dto.entryEntity.SHIHARAI_SHOUHIZEI_RATE = 0;

            if (!string.IsNullOrEmpty(this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text))
            {
                this.dto.entryEntity.SHIHARAI_SHOUHIZEI_RATE = this.ToDecimalForShiharaiShouhizeiRate();
            }
            else
            {
                DateTime shiharaiDate;
                if (this.form.DENPYOU_DATE.Value != null
                    && DateTime.TryParse(this.form.DENPYOU_DATE.Value.ToString(), out shiharaiDate))
                {
                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(shiharaiDate.Date);
                    if (shouhizeiEntity != null
                        && 0 < shouhizeiEntity.SHOUHIZEI_RATE)
                    {
                        this.dto.entryEntity.SHIHARAI_SHOUHIZEI_RATE = shouhizeiEntity.SHOUHIZEI_RATE;
                    }
                }
            }

            var dataBinderKeiryouEntry = new DataBinderLogic<T_KEIRYOU_ENTRY>(this.dto.entryEntity);
            dataBinderKeiryouEntry.SetSystemProperty(this.dto.entryEntity, false);

            // 修正、削除モードの場合、Create情報は前の伝票のデータを引き継ぐ
            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    this.dto.entryEntity.CREATE_USER = this.form.NYUURYOKU_TANTOUSHA_NAME.Text;
                    break;

                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    this.dto.entryEntity.CREATE_USER = this.beforDto.entryEntity.CREATE_USER;
                    this.dto.entryEntity.CREATE_DATE = this.beforDto.entryEntity.CREATE_DATE;
                    this.dto.entryEntity.CREATE_PC = this.beforDto.entryEntity.CREATE_PC;
                    break;

                default:
                    break;
            }

            // 最終更新者
            this.dto.entryEntity.UPDATE_USER = this.form.NYUURYOKU_TANTOUSHA_NAME.Text;

            // Detail
            List<T_KEIRYOU_DETAIL> keiryouDetailEntitys = new List<T_KEIRYOU_DETAIL>();

            SqlInt64 detailSysId = -1;
            decimal HimeiUrKingakuTotal = 0;
            decimal HimeiShKingakuTotal = 0;
            decimal HinmeiUrTaxSotoTotal = 0;
            decimal HinmeiShTaxSotoTotal = 0;
            decimal HinmeiUrTaxUchiTotal = 0;
            decimal HinmeiShTaxUchiTotal = 0;
            decimal UrTaxSotoTotal = 0;
            decimal ShTaxSotoTotal = 0;
            decimal UrTaxUchiTotal = 0;
            decimal ShTaxUchiTotal = 0;
            int TaxHasuuCdSeikyuu = -1;
            int TaxHasuuCdShiharai = -1;
            SqlInt16 SEIKYUU_TAX_HASUU_CD = SqlInt16.Null;
            SqlInt16 SHIHARAI_TAX_HASUU_CD = SqlInt16.Null;

            // 取引先CDが無い場合、取引先請求 or 取引先支払の税区分CDがなく、端数計算処理で落ちてしまうため、
            // ここで税計算区分CDを設定
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                SEIKYUU_TAX_HASUU_CD = this.dto.torihikisakiSeikyuuEntity.TAX_HASUU_CD;
                SHIHARAI_TAX_HASUU_CD = this.dto.torihikisakiShiharaiEntity.TAX_HASUU_CD;
            }
            else
            {
                SEIKYUU_TAX_HASUU_CD = this.dto.sysInfoEntity.SEIKYUU_TAX_HASUU_CD;
                SHIHARAI_TAX_HASUU_CD = this.dto.sysInfoEntity.SHIHARAI_TAX_HASUU_CD;
            }
            if (!SEIKYUU_TAX_HASUU_CD.IsNull)
            {
                TaxHasuuCdSeikyuu = SEIKYUU_TAX_HASUU_CD.Value;
            }
            if (!SHIHARAI_TAX_HASUU_CD.IsNull)
            {
                TaxHasuuCdShiharai = SHIHARAI_TAX_HASUU_CD.Value;
            }

            foreach (Row dr in this.form.gcMultiRow1.Rows)
            {
                if (dr.IsNewRow || string.IsNullOrEmpty(Convert.ToString(dr.Cells["ROW_NO"].Value)))
                {
                    continue;
                }

                T_KEIRYOU_DETAIL temp = new T_KEIRYOU_DETAIL();

                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // 新規の場合は、既にEntryで採番しているので、それに+1する
                        detailSysId = this.accessor.CreateSystemIdForKeiryou();
                        temp.DETAIL_SYSTEM_ID = detailSysId;
                        break;
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        // DETAIL_SYSTEM_IDの採番
                        if (string.IsNullOrEmpty(Convert.ToString(dr.Cells["DETAIL_SYSTEM_ID"].Value)))
                        {
                            // 修正モードでT_UKEIRE_DETAILが初めて登録されるパターンも張るはずなので、
                            // Detailが無ければ新たに採番(更新モードの場合、ここで初めて採番する)
                            detailSysId = this.accessor.CreateSystemIdForKeiryou();
                        }
                        else
                        {
                            // 既に登録されていればそのまま使う
                            detailSysId = SqlInt64.Parse(dr.Cells["DETAIL_SYSTEM_ID"].Value.ToString());
                        }
                        temp.DETAIL_SYSTEM_ID = detailSysId;
                        break;
                    default:
                        break;
                }

                temp.SYSTEM_ID = this.dto.entryEntity.SYSTEM_ID.Value;
                temp.SEQ = this.dto.entryEntity.SEQ;
                temp.KEIRYOU_NUMBER = this.dto.entryEntity.KEIRYOU_NUMBER;
                if (!string.IsNullOrEmpty(Convert.ToString(dr.Cells["ROW_NO"].Value)))
                {
                    temp.ROW_NO = SqlInt16.Parse(dr.Cells["ROW_NO"].Value.ToString());
                }

                temp.DENPYOU_DATE = this.dto.entryEntity.DENPYOU_DATE;
                decimal tempStackJyuryou = 0;
                if (decimal.TryParse(Convert.ToString(dr.Cells["STACK_JYUURYOU"].Value), out tempStackJyuryou))
                {
                    temp.STACK_JYUURYOU = tempStackJyuryou;
                }

                decimal tempEmptyJyuuryou = 0;
                if (decimal.TryParse(Convert.ToString(dr.Cells["EMPTY_JYUURYOU"].Value), out tempEmptyJyuuryou))
                {
                    temp.EMPTY_JYUURYOU = tempEmptyJyuuryou;
                }

                decimal tempChouseiJyuuryou = 0;
                if (decimal.TryParse(Convert.ToString(dr.Cells["CHOUSEI_JYUURYOU"].Value), out tempChouseiJyuuryou))
                {
                    temp.CHOUSEI_JYUURYOU = tempChouseiJyuuryou;
                }

                decimal tempChouseiPercent = 0;
                if (decimal.TryParse(Convert.ToString(dr.Cells["CHOUSEI_PERCENT"].Value), out tempChouseiPercent))
                {
                    temp.CHOUSEI_PERCENT = tempChouseiPercent;
                }
                if (dr.Cells["YOUKI_CD"].Value != null)
                {
                    temp.YOUKI_CD = dr.Cells["YOUKI_CD"].Value.ToString();
                }

                decimal tempYoukiSuuryou = 0;
                if (decimal.TryParse(Convert.ToString(dr.Cells["YOUKI_SUURYOU"].Value), out tempYoukiSuuryou))
                {
                    temp.YOUKI_SUURYOU = tempYoukiSuuryou;
                }

                decimal tempYoukiJyuuryou = 0;
                if (decimal.TryParse(Convert.ToString(dr.Cells["YOUKI_JYUURYOU"].Value), out tempYoukiJyuuryou))
                {
                    temp.YOUKI_JYUURYOU = tempYoukiJyuuryou;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr.Cells["DENPYOU_KBN_CD"].Value)))
                {
                    temp.DENPYOU_KBN_CD = SqlInt16.Parse(Convert.ToString(dr.Cells["DENPYOU_KBN_CD"].Value));
                }
                if (dr.Cells["HINMEI_CD"].Value != null)
                {
                    temp.HINMEI_CD = Convert.ToString(dr.Cells["HINMEI_CD"].Value);
                }
                if (dr.Cells["HINMEI_NAME"].Value != null)
                {
                    temp.HINMEI_NAME = Convert.ToString(dr.Cells["HINMEI_NAME"].Value);
                }

                decimal tempNetJyuuryou = 0;
                if (decimal.TryParse(Convert.ToString(dr.Cells["NET_JYUURYOU"].Value), out tempNetJyuuryou))
                {
                    temp.NET_JYUURYOU = tempNetJyuuryou;
                }

                decimal tempSuuryou = 0;
                if (decimal.TryParse(Convert.ToString(dr.Cells["SUURYOU"].Value), out tempSuuryou))
                {
                    temp.SUURYOU = tempSuuryou;
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dr.Cells["UNIT_CD"].Value)))
                {
                    temp.UNIT_CD = SqlInt16.Parse(Convert.ToString(dr.Cells["UNIT_CD"].Value));
                }
                decimal tanka = 0;
                if (dr.Cells["TANKA"].Value != null)
                {
                    if (decimal.TryParse(dr.Cells["TANKA"].Value.ToString(), out tanka))
                    {
                        temp.TANKA = tanka;
                    }
                    else
                    {
                        temp.TANKA = SqlDecimal.Null;
                    }
                }
                else
                {
                    temp.TANKA = SqlDecimal.Null;
                }

                // 明細で選択された品名の情報を取得
                short hinmeiZeiKbnCd = 0;
                if (short.TryParse(Convert.ToString(dr.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value), out hinmeiZeiKbnCd))
                {
                    temp.HINMEI_ZEI_KBN_CD = hinmeiZeiKbnCd;
                }

                if (temp.HINMEI_ZEI_KBN_CD.IsNull || temp.HINMEI_ZEI_KBN_CD == 0)
                {
                    if (dr.Cells["KINGAKU"].Value != null)
                    {
                        decimal kingaku = 0;
                        decimal.TryParse(dr.Cells["KINGAKU"].Value.ToString(), out kingaku);
                        temp.KINGAKU = kingaku;
                    }
                }
                else
                {
                    temp.KINGAKU = 0;
                }

                if (temp.HINMEI_ZEI_KBN_CD != 0 && !temp.HINMEI_ZEI_KBN_CD.IsNull)
                {
                    if (dr.Cells["KINGAKU"].Value != null)
                    {
                        decimal hinmeiKingaku = 0;
                        decimal.TryParse(dr.Cells["KINGAKU"].Value.ToString(), out hinmeiKingaku);
                        temp.HINMEI_KINGAKU = hinmeiKingaku;

                        if (dr.Cells["DENPYOU_KBN_CD"].Value != null
                            && SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE == temp.DENPYOU_KBN_CD)
                        {
                            HimeiUrKingakuTotal += hinmeiKingaku;
                        }
                        else if (dr.Cells["DENPYOU_KBN_CD"].Value != null
                            && SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI == temp.DENPYOU_KBN_CD)
                        {
                            HimeiShKingakuTotal += hinmeiKingaku;
                        }
                    }
                }
                else
                {
                    temp.HINMEI_KINGAKU = 0;
                }

                // 明細毎消費税合計を計算
                // この時点で明細.品名のデータは検索済みなので、品名データ取得処理はしない

                decimal meisaiKingaku = 0;
                decimal.TryParse(Convert.ToString(dr.Cells["KINGAKU"].Value), out meisaiKingaku);

                temp.TAX_SOTO = 0;          // 消費税外税初期値
                temp.TAX_UCHI = 0;          // 消費税内税初期値
                temp.HINMEI_TAX_SOTO = 0;   // 品名別消費税外税初期値
                temp.HINMEI_TAX_UCHI = 0;   // 品名別消費税内税初期値

                decimal detailShouhizeiRate = 0;
                if (!this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE.IsNull)
                {
                    detailShouhizeiRate = this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE.Value;
                }

                // もし消費税率が設定されていればそちらを優先して使う
                decimal tempShouhizeiRate = 0;
                if (!string.IsNullOrEmpty(Convert.ToString(dr.Cells[CELL_NAME_SHOUHIZEI_RATE].Value))
                    && decimal.TryParse(dr.Cells[CELL_NAME_SHOUHIZEI_RATE].Value.ToString(), out tempShouhizeiRate))
                {
                    detailShouhizeiRate = tempShouhizeiRate;
                }

                if (dr.Cells["DENPYOU_KBN_CD"].Value != null
                            && SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE == temp.DENPYOU_KBN_CD)
                {
                    if (!temp.HINMEI_ZEI_KBN_CD.IsNull
                        && temp.HINMEI_ZEI_KBN_CD != 0)
                    {
                        switch (temp.HINMEI_ZEI_KBN_CD.ToString())
                        {
                            case ConstClass.ZEI_KBN_1:
                                // 品名別消費税外税
                                temp.HINMEI_TAX_SOTO =
                                    CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        TaxHasuuCdSeikyuu);
                                HinmeiUrTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        TaxHasuuCdSeikyuu,
                                        temp.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            case ConstClass.ZEI_KBN_2:
                                // 品名別消費税内税
                                temp.HINMEI_TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                temp.HINMEI_TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)temp.HINMEI_TAX_UCHI, TaxHasuuCdSeikyuu);
                                HinmeiUrTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        TaxHasuuCdSeikyuu,
                                        temp.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (this.form.SEIKYUU_ZEI_KBN_CD.Text)
                        {
                            case ConstClass.ZEI_KBN_1:
                                UrTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        TaxHasuuCdSeikyuu,
                                        this.form.SEIKYUU_ZEI_KBN_CD.Text);
                                // 消費税外
                                temp.TAX_SOTO
                                    = CommonCalc.FractionCalc(
                                        meisaiKingaku * (decimal)detailShouhizeiRate,
                                        TaxHasuuCdSeikyuu);

                                break;

                            case ConstClass.ZEI_KBN_2:
                                UrTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        TaxHasuuCdSeikyuu,
                                        this.form.SEIKYUU_ZEI_KBN_CD.Text);
                                // 消費税内
                                temp.TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                temp.TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)temp.TAX_UCHI, TaxHasuuCdSeikyuu);
                                break;

                            default:
                                break;
                        }
                    }
                }
                else if (dr.Cells["DENPYOU_KBN_CD"].Value != null
                            && SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI == temp.DENPYOU_KBN_CD)
                {
                    if (!temp.HINMEI_ZEI_KBN_CD.IsNull
                        && temp.HINMEI_ZEI_KBN_CD != 0)
                    {
                        switch (temp.HINMEI_ZEI_KBN_CD.ToString())
                        {
                            case ConstClass.ZEI_KBN_1:
                                // 品名別消費税外税
                                temp.HINMEI_TAX_SOTO =
                                    CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        TaxHasuuCdShiharai);
                                HinmeiShTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        TaxHasuuCdShiharai,
                                        temp.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            case ConstClass.ZEI_KBN_2:
                                // 品名別消費税内税
                                temp.HINMEI_TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                temp.HINMEI_TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)temp.HINMEI_TAX_UCHI, TaxHasuuCdShiharai);
                                HinmeiShTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        TaxHasuuCdShiharai,
                                        temp.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (this.form.SHIHARAI_ZEI_KBN_CD.Text)
                        {
                            case ConstClass.ZEI_KBN_1:
                                ShTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        TaxHasuuCdShiharai,
                                        this.form.SHIHARAI_ZEI_KBN_CD.Text);
                                // 消費税外
                                temp.TAX_SOTO =
                                    CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        TaxHasuuCdShiharai);

                                break;

                            case ConstClass.ZEI_KBN_2:
                                ShTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        TaxHasuuCdShiharai,
                                        this.form.SHIHARAI_ZEI_KBN_CD.Text);
                                // 消費税内
                                temp.TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                temp.TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)temp.TAX_UCHI, TaxHasuuCdShiharai);
                                break;

                            default:
                                break;
                        }
                    }
                }

                if (dr.Cells["MEISAI_BIKOU"].Value != null)
                {
                    temp.MEISAI_BIKOU = dr.Cells["MEISAI_BIKOU"].Value.ToString();
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dr.Cells["KEIRYOU_TIME"].Value)))
                {
                    temp.KEIRYOU_TIME = Convert.ToDateTime(dr.Cells["KEIRYOU_TIME"].Value);
                }

                var dbLogic = new DataBinderLogic<T_KEIRYOU_DETAIL>(temp);
                dbLogic.SetSystemProperty(temp, false);

                keiryouDetailEntitys.Add(temp);
            }

            this.dto.detailEntity = new T_KEIRYOU_DETAIL[keiryouDetailEntitys.Count];
            this.dto.detailEntity = keiryouDetailEntitys.ToArray<T_KEIRYOU_DETAIL>();

            // 明細の集計結果系
            // 品名別売上金額合計
            this.dto.entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL = HimeiUrKingakuTotal;
            // entityの値を使って計算するため、処理の最後に計算
            decimal uriageTotal = 0;
            decimal.TryParse(this.form.URIAGE_KINGAKU_TOTAL.Text, out uriageTotal);
            this.dto.entryEntity.URIAGE_KINGAKU_TOTAL = uriageTotal - this.dto.entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL.Value;

            /**
             * 売上の税金系計算
             */
            // 売上伝票毎消費税外税、品名別売上消費税外税合計
            if (ConstClass.ZEI_KBN_1.Equals(this.form.SEIKYUU_ZEI_KBN_CD.Text))
            {
                this.dto.entryEntity.URIAGE_TAX_SOTO
                    = CommonCalc.FractionCalc(
                        (decimal)(this.dto.entryEntity.URIAGE_KINGAKU_TOTAL * this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE),
                        TaxHasuuCdSeikyuu);
            }
            else
            {
                this.dto.entryEntity.URIAGE_TAX_SOTO = 0;
            }

            if (!SEIKYUU_TAX_HASUU_CD.IsNull)
                this.dto.entryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL
                    = CommonCalc.FractionCalc(
                        HinmeiUrTaxSotoTotal,
                        TaxHasuuCdSeikyuu);

            // 売上伝票毎消費税内税、品名別売上消費税内税合計
            if (ConstClass.ZEI_KBN_2.Equals(this.form.SEIKYUU_ZEI_KBN_CD.Text))
            {
                // 金額計算
                this.dto.entryEntity.URIAGE_TAX_UCHI
                    = (this.dto.entryEntity.URIAGE_KINGAKU_TOTAL
                        - (this.dto.entryEntity.URIAGE_KINGAKU_TOTAL / (this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE + 1)));
                // 端数処理
                this.dto.entryEntity.URIAGE_TAX_UCHI
                    = CommonCalc.FractionCalc(
                        (decimal)this.dto.entryEntity.URIAGE_TAX_UCHI,
                        TaxHasuuCdSeikyuu);
            }
            else
            {
                this.dto.entryEntity.URIAGE_TAX_UCHI = 0;
            }

            // 金額計算
            this.dto.entryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL = HinmeiUrTaxUchiTotal;

            // 端数処理
            if (!SEIKYUU_TAX_HASUU_CD.IsNull)
                this.dto.entryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL
                    = CommonCalc.FractionCalc(
                        (decimal)this.dto.entryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL,
                        TaxHasuuCdSeikyuu);

            // 売上伝票毎消費税外税合計
            this.dto.entryEntity.URIAGE_TAX_SOTO_TOTAL = UrTaxSotoTotal;

            // 売上伝票毎消費税内税合計
            this.dto.entryEntity.URIAGE_TAX_UCHI_TOTAL = UrTaxUchiTotal;

            // 品名別支払金額合計
            this.dto.entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL = HimeiShKingakuTotal;
            decimal shiharaiTotal = 0;
            decimal.TryParse(this.form.SHIHARAI_KINGAKU_TOTAL.Text, out shiharaiTotal);
            this.dto.entryEntity.SHIHARAI_KINGAKU_TOTAL = shiharaiTotal - this.dto.entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL.Value;

            /**
             * 支払の税金系計算
             */
            // 支払伝票毎消費税外税、品名別支払消費税外税合計
            if (ConstClass.ZEI_KBN_1.Equals(this.form.SHIHARAI_ZEI_KBN_CD.Text))
            {
                this.dto.entryEntity.SHIHARAI_TAX_SOTO
                    = CommonCalc.FractionCalc(
                        (decimal)this.dto.entryEntity.SHIHARAI_KINGAKU_TOTAL * (decimal)this.dto.entryEntity.SHIHARAI_SHOUHIZEI_RATE,
                        TaxHasuuCdShiharai);
            }
            else
            {
                this.dto.entryEntity.SHIHARAI_TAX_SOTO = 0;
            }

            if (!SHIHARAI_TAX_HASUU_CD.IsNull)
                this.dto.entryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL
                    = CommonCalc.FractionCalc(
                        HinmeiShTaxSotoTotal,
                        TaxHasuuCdShiharai);

            // 支払伝票毎消費税内税、品名別支払消費税内税合計
            if (ConstClass.ZEI_KBN_2.Equals(this.form.SHIHARAI_ZEI_KBN_CD.Text))
            {
                // 金額計算
                this.dto.entryEntity.SHIHARAI_TAX_UCHI
                    = this.dto.entryEntity.SHIHARAI_KINGAKU_TOTAL
                        - (this.dto.entryEntity.SHIHARAI_KINGAKU_TOTAL / (this.dto.entryEntity.SHIHARAI_SHOUHIZEI_RATE + 1));
                // 端数処理
                this.dto.entryEntity.SHIHARAI_TAX_UCHI
                    = CommonCalc.FractionCalc(
                        (decimal)this.dto.entryEntity.SHIHARAI_TAX_UCHI,
                        TaxHasuuCdShiharai);
            }
            else
            {
                this.dto.entryEntity.SHIHARAI_TAX_UCHI = 0;
            }

            // 金額計算
            this.dto.entryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = HinmeiShTaxUchiTotal;
            // 端数処理
            if (!SHIHARAI_TAX_HASUU_CD.IsNull)
                this.dto.entryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL
                    = CommonCalc.FractionCalc(
                        (decimal)this.dto.entryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL,
                        TaxHasuuCdShiharai);

            // 支払明細毎消費税外税合計
            this.dto.entryEntity.SHIHARAI_TAX_SOTO_TOTAL = ShTaxSotoTotal;

            // 支払明細毎消費税内税合計
            this.dto.entryEntity.SHIHARAI_TAX_UCHI_TOTAL = ShTaxUchiTotal;

            //受入実績
            if (this.form.ismobile_mode)
            {
                int SEQInc = 0; //登録する明細がある時だけ、取得したSEQをインクリメントする
                foreach (Row dr in this.form.gcMultiRow2.Rows)
                {
                    if (dr.IsNewRow || string.IsNullOrEmpty(Convert.ToString(dr.Cells["ROW_NO"].Value)))
                    {
                        continue;
                    }
                    SEQInc = 1;
                }
                if (WINDOW_TYPE.DELETE_WINDOW_FLAG.Equals(this.form.WindowType))
                {
                    SEQInc = 0;
                }

                this.dto.JentryEntity.DENPYOU_SHURUI = 1;   //計量
                this.dto.JentryEntity.DENPYOU_SYSTEM_ID = this.dto.entryEntity.SYSTEM_ID;
                this.dto.JentryEntity.SEQ = this.accessor.OldSEQForUkeireJisseki(1, this.dto.entryEntity.SYSTEM_ID);    //無ければ0で持ってくる
                this.dto.JentryEntity.SEQ = this.dto.JentryEntity.SEQ + SEQInc;
                if (this.form.SAGYOU_DATE.Value != null)
                {
                    this.dto.JentryEntity.SAGYOU_DATE = ((DateTime)this.form.SAGYOU_DATE.Value).Date;
                }
                if (!(string.IsNullOrEmpty(this.form.SAGYOU_HOUR.Text) && string.IsNullOrEmpty(this.form.SAGYOU_MINUTE.Text)))
                {
                    this.dto.JentryEntity.SAGYOU_TIME = this.form.SAGYOU_HOUR.Text + ":" + this.form.SAGYOU_MINUTE.Text;
                }
                if (!string.IsNullOrEmpty(this.form.SAGYOUSHA_CD.Text))
                {
                    this.dto.JentryEntity.SAGYOUSHA_CD = this.form.SAGYOUSHA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.SAGYOUSHA_NAME.Text))
                {
                    this.dto.JentryEntity.SAGYOUSHA_NAME = this.form.SAGYOUSHA_NAME.Text;
                }
                if (!string.IsNullOrEmpty(this.form.SAGYOU_BIKOU.Text))
                {
                    this.dto.JentryEntity.SAGYOU_BIKOU = this.form.SAGYOU_BIKOU.Text;
                }
                if (this.beforDto.JentryEntity != null && !this.beforDto.JentryEntity.SEQ.IsNull)
                {
                    this.beforDto.JentryEntity.TIME_STAMP = ConvertStrByte.StringToByte(this.form.JISSEKI_TIME_STAMP.Text);
                }
                var dataBinderUkeireJEntry = new DataBinderLogic<T_UKEIRE_JISSEKI_ENTRY>(this.dto.JentryEntity);
                dataBinderUkeireJEntry.SetSystemProperty(this.dto.JentryEntity, false);

                this.dto.JentryEntity.DELETE_FLG = false;
                if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    this.dto.JentryEntity.UPDATE_USER = this.form.NYUURYOKU_TANTOUSHA_NAME.Text;
                }
                else if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG) && (this.beforDto.JdetailEntity.Length == 0))
                {
                    this.dto.JentryEntity.UPDATE_USER = this.form.NYUURYOKU_TANTOUSHA_NAME.Text;
                }
                else
                {
                    this.dto.JentryEntity.CREATE_USER = this.beforDto.JentryEntity.CREATE_USER;
                    this.dto.JentryEntity.CREATE_DATE = this.beforDto.JentryEntity.CREATE_DATE;
                    this.dto.JentryEntity.CREATE_PC = this.beforDto.JentryEntity.CREATE_PC;
                }

                List<T_UKEIRE_JISSEKI_DETAIL> UkeireJissekiEntitys = new List<T_UKEIRE_JISSEKI_DETAIL>();

                foreach (Row dr in this.form.gcMultiRow2.Rows)
                {
                    if (dr.IsNewRow || string.IsNullOrEmpty(Convert.ToString(dr.Cells["ROW_NO"].Value)))
                    {
                        continue;
                    }

                    T_UKEIRE_JISSEKI_DETAIL temp = new T_UKEIRE_JISSEKI_DETAIL();
                    temp.DENPYOU_SHURUI = 1;    //計量
                    temp.DENPYOU_SYSTEM_ID = this.dto.entryEntity.SYSTEM_ID.Value;
                    temp.SEQ = this.dto.JentryEntity.SEQ;

                    SqlInt64 jDetailSystemId = 0;
                    switch (this.form.WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            // 新規の場合は、既にEntryで採番しているので、それに+1する
                            jDetailSystemId = this.accessor.CreateSystemIdForUkeireJisseki();
                            temp.DETAIL_SYSTEM_ID = jDetailSystemId;
                            break;
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            // DETAIL_SYSTEM_IDの採番
                            if (dr.Cells["DETAIL_SYSTEM_ID"].Value == null
                                || string.IsNullOrEmpty(dr.Cells["DETAIL_SYSTEM_ID"].Value.ToString()))
                            {
                                // 修正モードでT_UKEIRE_DETAILが初めて登録されるパターンも張るはずなので、
                                // Detailが無ければ新たに採番(更新モードの場合、ここで初めて採番する)
                                jDetailSystemId = this.accessor.CreateSystemIdForUkeireJisseki();
                            }
                            else
                            {
                                // 既に登録されていればそのまま使う
                                jDetailSystemId = SqlInt64.Parse(dr.Cells["DETAIL_SYSTEM_ID"].Value.ToString());
                            }
                            temp.DETAIL_SYSTEM_ID = jDetailSystemId;
                            break;
                        default:
                            break;
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr.Cells["ROW_NO"].Value)))
                    {
                        temp.DETAIL_SEQ = SqlInt16.Parse(dr.Cells["ROW_NO"].Value.ToString());
                    }
                    if (dr.Cells["HINMEI_CD"].Value != null)
                    {
                        temp.HINMEI_CD = Convert.ToString(dr.Cells["HINMEI_CD"].Value);
                    }
                    Int16 tempSuuryou = 0;
                    if (Int16.TryParse(Convert.ToString(dr.Cells["SUURYOU_WARIAI"].Value), out tempSuuryou))
                    {
                        temp.SUURYOU_WARIAI = tempSuuryou;
                    }
                    var dbLogic = new DataBinderLogic<T_UKEIRE_JISSEKI_DETAIL>(temp);
                    dbLogic.SetSystemProperty(temp, false);

                    UkeireJissekiEntitys.Add(temp);
                }

                this.dto.JdetailEntity = new T_UKEIRE_JISSEKI_DETAIL[UkeireJissekiEntitys.Count];
                this.dto.JdetailEntity = UkeireJissekiEntitys.ToArray<T_UKEIRE_JISSEKI_DETAIL>();
            }
            ////////////////////////////////////

            // S_NUMBER_YEAR
            this.dto.numberYear.NUMBERED_YEAR = numberedYear;
            this.dto.numberYear.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_KEIRYOU;
            this.dto.numberYear.KYOTEN_CD = kyotenCd;
            this.dto.numberYear.CURRENT_NUMBER = this.dto.entryEntity.YEAR_NUMBER;
            this.dto.numberYear.DELETE_FLG = false;
            if (numberYearTimeStamp != null)
            {
                this.dto.numberYear.TIME_STAMP = numberYearTimeStamp;
            }
            var dataBinderNumberYear = new DataBinderLogic<S_NUMBER_YEAR>(this.dto.numberYear);
            dataBinderNumberYear.SetSystemProperty(this.dto.numberYear, false);

            // S_NUMBER_DAY
            this.dto.numberDay.NUMBERED_DAY = denpyouDate.Date;
            this.dto.numberDay.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_KEIRYOU;
            this.dto.numberDay.KYOTEN_CD = kyotenCd;
            this.dto.numberDay.CURRENT_NUMBER = this.dto.entryEntity.DATE_NUMBER;
            this.dto.numberDay.DELETE_FLG = false;
            if (numberDayTimeStamp != null)
            {
                this.dto.numberDay.TIME_STAMP = numberDayTimeStamp;
            }
            var dataBinderNumberDay = new DataBinderLogic<S_NUMBER_DAY>(this.dto.numberDay);
            dataBinderNumberDay.SetSystemProperty(this.dto.numberDay, false);

            LogUtility.DebugMethodEnd(tairyuuKbnFlag);
        }

        #region Equals/GetHashCode/ToString
        /// <summary>
        /// クラスが等しいかどうか判定
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            //objがnullか、型が違うときは、等価でない
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            LogicClass localLogic = other as LogicClass;
            return localLogic == null ? false : true;
        }

        /// <summary>
        /// ハッシュコード取得
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 該当するオブジェクトを文字列形式で取得
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

        /// <summary>
        /// 明細の制御(システム設定情報用)
        /// </summary>
        /// <param name="headerNames">非表示にするカラム名一覧</param>
        /// <param name="cellNames">非表示にするセル名一覧</param>
        /// <param name="visibleFlag">各カラム、セルのVisibleに設定するbool</param>
        private void ChangePropertyForGC(string[] headerNames, string[] cellNames, string propertyName, bool visibleFlag)
        {
            this.form.gcMultiRow1.SuspendLayout();

            var newTemplate = this.form.gcMultiRow1.Template;

            if (headerNames != null && 0 < headerNames.Length)
            {
                var obj1 = controlUtil.FindControl(newTemplate.ColumnHeaders[0].Cells.ToArray(), headerNames);
                foreach (var o in obj1)
                {
                    PropertyUtility.SetValue(o, propertyName, visibleFlag);
                }
            }

            if (cellNames != null && 0 < cellNames.Length)
            {
                var obj2 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), cellNames);
                foreach (var o in obj2)
                {
                    PropertyUtility.SetValue(o, propertyName, visibleFlag);
                }
            }

            this.form.gcMultiRow1.Template = newTemplate;

            this.form.gcMultiRow1.ResumeLayout();
        }

        /// <summary>
        /// 基準正味を取得
        /// 総重量-空車重量をreturn
        /// </summary>
        /// <returns></returns>
        private decimal? GetCriterionNetForCurrent()
        {
            LogUtility.DebugMethodStart();

            Row targetRow = this.form.gcMultiRow1.CurrentRow;

            decimal stakJyuuryou = 0;
            decimal emptyJyuuryou = 0;
            var resultStackJyuuryouTryPase = decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stakJyuuryou);
            var resultEmptyJyuuryouTrypase = decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou);
            if (resultStackJyuuryouTryPase && resultEmptyJyuuryouTrypase)
            {
                return stakJyuuryou - emptyJyuuryou;
            }

            LogUtility.DebugMethodEnd();
            return null;
        }

        /// <summary>
        /// Detail必須チェック
        /// Datailが一行以上入力されているかチェックする
        /// </summary>
        /// <returns>true: 一件以上入力されている, false: 一件も入力されていない</returns>
        internal bool CheckRequiredDataForDeital(out bool catchErr)
        {
            catchErr = true;
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();


                foreach (var row in this.form.gcMultiRow1.Rows)
                {
                    if (row == null) continue;
                    if (row.IsNewRow) continue;

                    returnVal = true;
                    break;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckRequiredDataForDeital", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                returnVal = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckRequiredDataForDeital", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                returnVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }

        /// <summary>
        /// ユーザ入力コントロールの活性制御
        /// </summary>
        /// <param name="isLock">ロック状態に設定するbool</param>
        internal void ChangeEnabledForInputControl(bool isLock)
        {
            LogUtility.DebugMethodStart();

            // UIFormのコントロールを制御
            List<string> formControlNameList = new List<string>();
            if (this.form.WindowType.Equals(WINDOW_TYPE.REFERENCE_WINDOW_FLAG))
            {
                // 画面モードが参照用の場合
                formControlNameList.AddRange(refUiFormControlNames);
            }
            else
            {
                formControlNameList.AddRange(inputUiFormControlNames);
            }
            formControlNameList.AddRange(inputHeaderControlNames);
            foreach (var controlName in formControlNameList)
            {
                Control control = controlUtil.FindControl(this.form, controlName);

                if (control == null)
                {
                    // headerを検索
                    control = controlUtil.FindControl(this.headerForm, controlName);
                }

                if (control == null)
                {
                    continue;
                }

                var enabledProperty = control.GetType().GetProperty("Enabled");
                var readOnlyProperty = control.GetType().GetProperty("ReadOnly");

                if (enabledProperty != null)
                {
                    bool readOnlyValue = false;
                    if (readOnlyProperty != null)
                    {
                        readOnlyValue = (bool)readOnlyProperty.GetValue(control, null);
                    }
                    // 車輌CD等、ReadOnlyが動的に変わる箇所の対策としてReadOnlyを判定する
                    if (!readOnlyValue)
                    {
                        enabledProperty.SetValue(control, !isLock, null);
                    }
                }
            }

            // Detailのコントロールを制御
            foreach (Row row in this.form.gcMultiRow1.Rows)
            {
                foreach (var detaiControlName in inputDetailControlNames)
                {
                    row.Cells[detaiControlName].Enabled = !isLock;
                }
            }

            if (this.form.ismobile_mode)
            {
                // Jissekiのコントロールを制御
                foreach (Row row in this.form.gcMultiRow2.Rows)
                {
                    foreach (var detaiControlName in inputJissekiControlNames)
                    {
                        row.Cells[detaiControlName].Enabled = !isLock;
                    }
                }

                // 添付ファイルタブのコントロールを制御
                if (this.form.isfile_upload)
                {
                    this.form.TENPU_FILE_UPDATE_BTN.Enabled = !isLock;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 明細のレイアウト調整
        /// 非表示にしたコントロールが空白で表示されるため調整する
        /// </summary>
        internal void ExecuteAlignmentForDetail()
        {
            LogUtility.DebugMethodStart();

            this.form.gcMultiRow1.SuspendLayout();

            bool tankaVisible = true;
            bool chouseiVisible = true;
            bool youkiVisible = true;

            this.form.gcMultiRow1.SuspendLayout();
            var newTemplate = this.form.gcMultiRow1.Template;

            var stackHedader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCell4"];
            var stackCell = newTemplate.Row.Cells["STACK_JYUURYOU"];

            var emptyHedader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCell7"];
            var emptyCell = newTemplate.Row.Cells["EMPTY_JYUURYOU"];

            var tankaHedader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCell18"];
            var tankaCell = newTemplate.Row.Cells["TANKA"];

            var kingakuHedader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCell20"];
            var kingakuCell = newTemplate.Row.Cells["KINGAKU"];

            var chouseiJHedader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCell10"];
            var chouseiJCell = newTemplate.Row.Cells["CHOUSEI_JYUURYOU"];

            var chouseiPHedader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCell11"];
            var chouseiPCell = newTemplate.Row.Cells["CHOUSEI_PERCENT"];

            var youkiHedader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCell12"];
            var youkiCCell = newTemplate.Row.Cells["YOUKI_CD"];
            var youkiNCell = newTemplate.Row.Cells["YOUKI_NAME_RYAKU"];

            var youkiSHedader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCell13"];
            var youkiSCell = newTemplate.Row.Cells["YOUKI_SUURYOU"];
            var youkiJCell = newTemplate.Row.Cells["YOUKI_JYUURYOU"];

            var blankHedader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCell24"];
            var blankCell = newTemplate.Row.Cells["BLANK"];

            var bikouHedader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCell2"];
            var bikouCell = newTemplate.Row.Cells["MEISAI_BIKOU"];

            if (this.dto.sysInfoEntity.KEIRYOU_TANKA_KINGAKU_USE_KBN != 1)
            {
                tankaVisible = false;
            }
            if (this.dto.sysInfoEntity.KEIRYOU_CHOUSEI_USE_KBN != 1)
            {
                chouseiVisible = false;
            }
            if (this.dto.sysInfoEntity.KEIRYOU_YOUKI_USE_KBN != 1)
            {
                youkiVisible = false;
            }
            int x = stackHedader.Location.X + stackHedader.Width;

            if (!youkiVisible)
            {
                youkiHedader.Location = new Point(0, 0);
                youkiCCell.Location = new Point(0, 0);
                youkiNCell.Location = new Point(0, 0);
                youkiHedader.Visible = false;
                youkiCCell.Visible = false;
                youkiNCell.Visible = false;

                youkiSHedader.Location = new Point(0, 20);
                youkiSCell.Location = new Point(0, 21);
                youkiJCell.Location = new Point(0, 21);
                youkiSHedader.Visible = false;
                youkiSCell.Visible = false;
                youkiJCell.Visible = false;
            }
            else
            {
                youkiHedader.Location = new Point(x, 0);
                youkiCCell.Location = new Point(x, 0);
                youkiNCell.Location = new Point(x + youkiCCell.Width, 0);

                youkiSHedader.Location = new Point(x, 20);
                youkiSCell.Location = new Point(x, 21);
                youkiJCell.Location = new Point(x + youkiSCell.Width, 21);
                x = x + youkiHedader.Width;
            }

            if (!chouseiVisible)
            {
                chouseiJHedader.Location = new Point(0, 0);
                chouseiJCell.Location = new Point(0, 0);
                chouseiJHedader.Visible = false;
                chouseiJCell.Visible = false;

                chouseiPHedader.Location = new Point(0, 20);
                chouseiPCell.Location = new Point(0, 21);
                chouseiPHedader.Visible = false;
                chouseiPCell.Visible = false;
            }
            else
            {
                chouseiJHedader.Location = new Point(x, 0);
                chouseiJCell.Location = new Point(x, 0);

                chouseiPHedader.Location = new Point(x, 20);
                chouseiPCell.Location = new Point(x, 21);
                x = x + chouseiJHedader.Width;
            }

            if (!tankaVisible)
            {
                tankaHedader.Location = new Point(0, 0);
                tankaCell.Location = new Point(0, 0);
                tankaHedader.Visible = false;
                tankaCell.Visible = false;

                kingakuHedader.Location = new Point(0, 20);
                kingakuCell.Location = new Point(0, 21);
                kingakuHedader.Visible = false;
                kingakuCell.Visible = false;
            }
            else
            {
                tankaHedader.Location = new Point(x, 0);
                tankaCell.Location = new Point(x, 0);

                kingakuHedader.Location = new Point(x, 20);
                kingakuCell.Location = new Point(x, 21);
                x = x + tankaHedader.Width;
            }

            blankHedader.Location = new Point(x, 0);
            blankCell.Location = new Point(x, 0);
            bikouHedader.Location = new Point(x, 20);
            bikouCell.Location = new Point(x, 21);

            this.form.gcMultiRow1.Template.Width = blankHedader.Location.X + blankHedader.Width;
            this.form.gcMultiRow1.ResumeLayout();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 指定した計量番号のデータが存在するか返す
        /// </summary>
        /// <param name="keiryouNumber">計量番号</param>
        /// <returns>true:存在する, false:存在しない</returns>
        internal bool IsExistKeiryouData(long keiryouNumber, out bool catchErr)
        {
            catchErr = true;
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();



                if (0 <= keiryouNumber)
                {
                    var keiryouEntrys = this.accessor.GetKeiryouEntry(keiryouNumber, this.form.SEQ);
                    if (keiryouEntrys != null
                        && 0 < keiryouEntrys.Length)
                    {
                        returnVal = true;
                    }
                }
                else if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    returnVal = true;
                }

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("IsExistKeiryouData", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsExistKeiryouData", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
            return returnVal;
        }

        /// <summary>
        /// 滞留登録された計量伝票用の権限チェックを行う
        /// </summary>
        /// <param name="keiryouNumber">計量番号</param>
        /// <param name="seq">SEQ</param>
        /// <returns>true:権限有, false:権限無</returns>
        internal bool HasAuthorityTairyuu(long keiryouNumber, string seq, out bool catchErr)
        {
            bool ret = true;
            catchErr = true;
            try
            {
                // 計量入力
                var entrys = accessor.GetKeiryouEntry(keiryouNumber, seq);
                if (entrys == null || entrys.Length < 1)
                {
                    // 対象伝票が無い場合、権限有(未チェック)とみなす。
                    return true;
                }

                if (!entrys[0].TAIRYUU_KBN)
                {
                    // 滞留登録されていなければ、権限有(未チェック)とみなす。
                    return true;
                }

                // 滞留登録された計量伝票用にWindowTypeが変更対象か判定(削除モード以外は新規モードに変更するため)
                if (HadChangedWindowTypeTairyuu(this.form.WindowType))
                {
                    // 滞留登録された計量伝票用の権限チェック
                    return r_framework.Authority.Manager.CheckAuthority("G672", tairyuuWindowType, false);
                }

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("HasAuthorityTairyuu", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HasAuthorityTairyuu", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }

        /// <summary>
        /// 滞留登録された計量伝票用に画面区分の変更有無を判定
        /// </summary>
        /// <param name="windowType"></param>
        /// <returns></returns>
        private bool HadChangedWindowTypeTairyuu(WINDOW_TYPE windowType)
        {
            // 滞留一覧から削除で開かれた場合は、モードを変更しない
            return WINDOW_TYPE.DELETE_WINDOW_FLAG != windowType && WINDOW_TYPE.REFERENCE_WINDOW_FLAG != windowType;
        }

        /// <summary>
        /// 重量値、金額値用フォーマット
        /// </summary>
        /// <param name="sender"></param>
        internal bool ToAmountValue(object sender)
        {
            try
            {
                LogUtility.DebugMethodStart(sender);

                if (sender == null)
                {
                    return true;
                }

                var value = PropertyUtility.GetTextOrValue(sender);
                if (!string.IsNullOrEmpty(value))
                {
                    PropertyUtility.SetTextOrValue(sender, FormatUtility.ToAmountValue(value));
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ToAmountValue", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ToAmountValue", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 重量値、金額値用フォーマット(Detial用)
        /// </summary>
        /// <param name="sender"></param>
        internal void ToAmountValueForDetail(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (sender == null)
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            var value = PropertyUtility.GetTextOrValue(this.form.gcMultiRow1[e.RowIndex, e.CellIndex]);
            if (!string.IsNullOrEmpty(value))
            {
                this.form.gcMultiRow1.SetValue(e.RowIndex, e.CellIndex, FormatUtility.ToAmountValue(value));
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 売上明細毎消費税を計算する(外、内税両方)
        /// </summary>
        /// <param name="hinmei">明細.品名</param>
        /// <param name="kingaku">明細.金額</param>
        /// <param name="zeiKbn">伝票発行画面.請求税区分</param>
        /// <returns></returns>
        private decimal CalcTaxForUriageDetial(decimal kingaku, decimal uriageShouhizeiRate, int hasuuCd, string zeiKbn)
        {
            decimal returnVal = 0;

            // TODO: 税区分はConstクラスの値で判定
            switch (zeiKbn)
            {
                // 一般的な税区分を使用
                case "1":
                    returnVal = CommonCalc.FractionCalc((kingaku * uriageShouhizeiRate), hasuuCd);
                    break;

                case "2":
                    returnVal = kingaku - (kingaku / (uriageShouhizeiRate + 1));
                    // 端数処理
                    returnVal
                        = CommonCalc.FractionCalc(returnVal, hasuuCd);
                    break;

                default:
                    break;
            }

            return returnVal;
        }

        /// <summary>
        /// 項目クリア処理
        /// </summary>
        /// <returns></returns>
        internal void ClearEntryData()
        {
            // ヘッダー Start
            // 拠点
            headerForm.KYOTEN_CD.Text = string.Empty;
            headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
            const string KYOTEN_CD = "拠点CD";
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            this.headerForm.KYOTEN_CD.Text = this.GetUserProfileValue(userProfile, KYOTEN_CD);
            if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
            {
                this.headerForm.KYOTEN_CD.Text = this.headerForm.KYOTEN_CD.Text.PadLeft(this.headerForm.KYOTEN_CD.MaxLength, '0');
                CheckKyotenCd();
            }

            // 登録者情報
            headerForm.CreateUser.Text = string.Empty;
            headerForm.CreateDate.Text = string.Empty;

            // 更新者情報
            headerForm.LastUpdateUser.Text = string.Empty;
            headerForm.LastUpdateDate.Text = string.Empty;
            // ヘッダー End

            // 詳細 Start
            this.form.ENTRY_NUMBER.Text = string.Empty;
            // 連番
            this.form.RENBAN.Text = string.Empty;

            // 計量区分
            SqlInt16 KeiryouKbnCd = 0;
            if (this.headerForm.KIHON_KEIRYOU.Text == "1")
            {
                KeiryouKbnCd = this.dto.sysInfoEntity.KEIRYOU_UKEIRE_KEIRYOU_KBN_CD;
            }
            else
            {
                KeiryouKbnCd = this.dto.sysInfoEntity.KEIRYOU_SHUKKA_KEIRYOU_KBN_CD;
            }
            if (!KeiryouKbnCd.IsNull)
            {
                this.form.KEIRYOU_KBN_CD.Text = KeiryouKbnCd.ToString();
            }
            else
            {
                this.form.KEIRYOU_KBN_CD.Text = string.Empty;
            }

            // 消費税率
            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text = string.Empty;
            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text = string.Empty;

            // 入力担当者
            if (CommonShogunData.LOGIN_USER_INFO != null
                && !string.IsNullOrEmpty(CommonShogunData.LOGIN_USER_INFO.SHAIN_CD)
                && CommonShogunData.LOGIN_USER_INFO.NYUURYOKU_TANTOU_KBN)
            {
                this.form.NYUURYOKU_TANTOUSHA_CD.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_CD;
                this.form.NYUURYOKU_TANTOUSHA_NAME.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME_RYAKU;
                strNyuryokuTantousyaName = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME;
            }
            else
            {
                this.form.NYUURYOKU_TANTOUSHA_CD.Text = string.Empty;
                this.form.NYUURYOKU_TANTOUSHA_NAME.Text = string.Empty;
                strNyuryokuTantousyaName = string.Empty;
                this.form.NYUURYOKU_TANTOUSHA_NAME.ReadOnly = true;
            }
            // 運搬業者
            this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
            this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
            this.form.UNPAN_GYOUSHA_NAME.ReadOnly = true;
            this.form.UNPAN_GYOUSHA_NAME.Tag = string.Empty;
            // 車輌
            this.form.SHARYOU_CD.Text = string.Empty;
            this.form.SHARYOU_CD.BackColor = SystemColors.Window;
            this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
            this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
            this.form.SHARYOU_NAME_RYAKU.Tag = string.Empty;
            this.form.STACK_JYUURYOU.Text = string.Empty;
            this.form.EMPTY_JYUURYOU.Text = string.Empty;
            this.form.SHARYOU_EMPTY_JYUURYOU.Text = string.Empty;
            // 車種
            this.form.SHASHU_CD.Text = string.Empty;
            this.form.SHASHU_NAME.Text = string.Empty;
            this.form.SHASHU_NAME.ReadOnly = true;
            // 運転者
            this.form.UNTENSHA_CD.Text = string.Empty;
            this.form.UNTENSHA_NAME.Text = string.Empty;
            this.form.UNTENSHA_NAME.ReadOnly = true;
            // 業者
            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.ReadOnly = true;
            this.form.GYOUSHA_NAME_RYAKU.Tag = string.Empty;
            // 現場
            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_NAME_RYAKU.Text = string.Empty;
            this.form.GENBA_NAME_RYAKU.ReadOnly = true;
            this.form.GENBA_NAME_RYAKU.Tag = string.Empty;
            // 伝票備考
            this.form.DENPYOU_BIKOU.Text = string.Empty;
            // 滞留備考
            this.form.TAIRYUU_BIKOU.Text = string.Empty;
            // 総重量
            this.form.STACK_JYUURYOU.Text = string.Empty;
            this.form.STACK_KEIRYOU_TIME.Text = string.Empty;
            // 空車重量
            this.form.EMPTY_JYUURYOU.Text = string.Empty;
            this.form.EMPTY_KEIRYOU_TIME.Text = string.Empty;

            // 廃棄物区分
            if (!this.dto.sysInfoEntity.KEIRYOU_MANIFEST_HAIKI_KBN_CD.IsNull)
            {
                this.form.MANIFEST_HAIKI_KBN_CD.Text = this.dto.sysInfoEntity.KEIRYOU_MANIFEST_HAIKI_KBN_CD.ToString();
                if (this.form.MANIFEST_HAIKI_KBN_CD.Text == "1")
                {
                    this.form.MANIFEST_HAIKI_KBN_NAME.Text = "産廃";
                }
                else if (this.form.MANIFEST_HAIKI_KBN_CD.Text == "2")
                {
                    this.form.MANIFEST_HAIKI_KBN_NAME.Text = "建廃";
                }
                else if (this.form.MANIFEST_HAIKI_KBN_CD.Text == "3")
                {
                    this.form.MANIFEST_HAIKI_KBN_NAME.Text = "積替";
                }
                else
                {
                    this.form.MANIFEST_HAIKI_KBN_NAME.Text = string.Empty;
                }
            }
            else
            {
                this.form.MANIFEST_HAIKI_KBN_CD.Text = string.Empty;
                this.form.MANIFEST_HAIKI_KBN_NAME.Text = string.Empty;
            }
            // 排出事業者
            this.form.HST_GYOUSHA_CD.Text = string.Empty;
            this.form.HST_GYOUSHA_NAME.Text = string.Empty;
            this.form.HST_GYOUSHA_NAME.ReadOnly = true;
            this.form.HST_GYOUSHA_NAME.Tag = " ";   //ヒントテキスト不要のためスペースを詰める
            // 排出事業場
            this.form.HST_GENBA_CD.Text = string.Empty;
            this.form.HST_GENBA_NAME.Text = string.Empty;
            this.form.HST_GENBA_NAME.ReadOnly = true;
            this.form.HST_GENBA_NAME.Tag = " ";   //ヒントテキスト不要のためスペースを詰める
            // 処分業者
            this.form.SBN_GYOUSHA_CD.Text = string.Empty;
            this.form.SBN_GYOUSHA_NAME.Text = string.Empty;
            this.form.SBN_GYOUSHA_NAME.ReadOnly = true;
            this.form.SBN_GYOUSHA_NAME.Tag = " ";   //ヒントテキスト不要のためスペースを詰める
            // 処分事業場
            this.form.SBN_GENBA_CD.Text = string.Empty;
            this.form.SBN_GENBA_NAME.Text = string.Empty;
            this.form.SBN_GENBA_NAME.ReadOnly = true;
            this.form.SBN_GENBA_NAME.Tag = " ";   //ヒントテキスト不要のためスペースを詰める
            // 最終処分業者
            this.form.LAST_SBN_GYOUSHA_CD.Text = string.Empty;
            this.form.LAST_SBN_GYOUSHA_NAME.Text = string.Empty;
            this.form.LAST_SBN_GYOUSHA_NAME.ReadOnly = true;
            this.form.LAST_SBN_GYOUSHA_NAME.Tag = " ";   //ヒントテキスト不要のためスペースを詰める
            // 最終処分場
            this.form.LAST_SBN_GENBA_CD.Text = string.Empty;
            this.form.LAST_SBN_GENBA_NAME.Text = string.Empty;
            this.form.LAST_SBN_GENBA_NAME.ReadOnly = true;
            this.form.LAST_SBN_GENBA_NAME.Tag = " ";   //ヒントテキスト不要のためスペースを詰める
            // 営業担当者
            this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
            this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
            this.form.EIGYOU_TANTOUSHA_NAME.ReadOnly = true;
            // 荷降業者
            this.form.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
            this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
            this.form.NIOROSHI_GYOUSHA_NAME.ReadOnly = true;
            this.form.NIOROSHI_GYOUSHA_NAME.Tag = " ";   //ヒントテキスト不要のためスペースを詰める
            // 荷降現場
            this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
            this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
            this.form.NIOROSHI_GENBA_NAME.ReadOnly = true;
            this.form.NIOROSHI_GENBA_NAME.Tag = " ";   //ヒントテキスト不要のためスペースを詰める
            // 荷積業者
            this.form.NIZUMI_GYOUSHA_CD.Text = string.Empty;
            this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
            this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = true;
            this.form.NIZUMI_GYOUSHA_NAME.Tag = " ";   //ヒントテキスト不要のためスペースを詰める
            // 荷積現場
            this.form.NIZUMI_GENBA_CD.Text = string.Empty;
            this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
            this.form.NIZUMI_GENBA_NAME.ReadOnly = true;
            this.form.NIZUMI_GENBA_NAME.Tag = " ";   //ヒントテキスト不要のためスペースを詰める

            if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType) && this.form.TairyuuNewFlg == false)
            {   // 滞留以外の新規の場合
                if (this.headerForm.KIHON_KEIRYOU.Text == "1")
                {
                    // 荷降業者
                    const string NIOROSHI_GYOUSHA_CD = "荷降業者CD";
                    this.form.NIOROSHI_GYOUSHA_CD.Text = this.GetUserProfileValue(userProfile, NIOROSHI_GYOUSHA_CD);
                    if (!string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
                    {
                        this.form.NIOROSHI_GYOUSHA_CD.Text = this.form.NIOROSHI_GYOUSHA_CD.Text.PadLeft(this.form.NIOROSHI_GYOUSHA_CD.MaxLength, '0');
                        bool catchErr = true;
                        CheckNioroshiGyoushaCd(out catchErr);
                        if (!catchErr)
                        {
                            throw new Exception("");
                        }
                    }
                    // 荷降現場
                    const string NIOROSHI_GENBA_CD = "荷降現場CD";
                    this.form.NIOROSHI_GENBA_CD.Text = this.GetUserProfileValue(userProfile, NIOROSHI_GENBA_CD);
                    if (!string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
                    {
                        this.form.NIOROSHI_GENBA_CD.Text = this.form.NIOROSHI_GENBA_CD.Text.PadLeft(this.form.NIOROSHI_GENBA_CD.MaxLength, '0');
                        bool catchErr = true;
                        CheckNioroshiGenbaCd(out catchErr);
                        if (!catchErr)
                        {
                            throw new Exception("");
                        }
                    }
                }
                else if (this.headerForm.KIHON_KEIRYOU.Text == "2")
                {
                    // 荷積業者
                    const string NIZUMI_GYOUSHA_CD = "荷積業者CD";
                    this.form.NIZUMI_GYOUSHA_CD.Text = this.GetUserProfileValue(userProfile, NIZUMI_GYOUSHA_CD);
                    if (!string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
                    {
                        this.form.NIZUMI_GYOUSHA_CD.Text = this.form.NIZUMI_GYOUSHA_CD.Text.PadLeft(this.form.NIZUMI_GYOUSHA_CD.MaxLength, '0');
                        bool catchErr = true;
                        CheckNizumiGyoushaCd(out catchErr);
                        if (!catchErr)
                        {
                            throw new Exception("");
                        }
                    }
                    // 荷積現場
                    const string NIZUMI_GENBA_CD = "荷積現場CD";
                    this.form.NIZUMI_GENBA_CD.Text = this.GetUserProfileValue(userProfile, NIZUMI_GENBA_CD);
                    if (!string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text))
                    {
                        this.form.NIZUMI_GENBA_CD.Text = this.form.NIZUMI_GENBA_CD.Text.PadLeft(this.form.NIZUMI_GENBA_CD.MaxLength, '0');
                        bool catchErr = true;
                        CheckNizumiGenbaCd(out catchErr);
                        if (!catchErr)
                        {
                            throw new Exception("");
                        }
                    }
                }
            }

            // 台貫
            this.form.DAIKAN_KBN.Text = SalesPaymentConstans.DAIKAN_KBN_JISHA;
            this.form.DAIKAN_KBN_NAME.Text = SalesPaymentConstans.DAIKAN_KBNExt.ToTypeString(SalesPaymentConstans.DAIKAN_KBNExt.ToDaikanKbn(this.form.DAIKAN_KBN.Text));
            // 形態区分
            SqlInt16 KeitaiKbnCd = 0;
            if (this.headerForm.KIHON_KEIRYOU.Text == "1")
            {
                KeitaiKbnCd = this.dto.sysInfoEntity.KEIRYOU_UKEIRE_KEITAI_KBN_CD;
            }
            else
            {
                KeitaiKbnCd = this.dto.sysInfoEntity.KEIRYOU_SHUKKA_KEITAI_KBN_CD;
            }
            if (!KeitaiKbnCd.IsNull)
            {
                this.form.KEITAI_KBN_CD.Text = KeitaiKbnCd.ToString();
                this.form.KEITAI_KBN_NAME_RYAKU.Text = this.accessor.GetKeitaiKbnNameRyaku(KeitaiKbnCd);
            }
            else
            {
                this.form.KEITAI_KBN_CD.Text = string.Empty;
                this.form.KEITAI_KBN_NAME_RYAKU.Text = string.Empty;
            }
            // マニフェスト種類
            this.form.MANIFEST_SHURUI_CD.Text = string.Empty;
            this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = string.Empty;
            // マニフェスト手配
            this.form.MANIFEST_TEHAI_CD.Text = string.Empty;
            this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = string.Empty;

            // 取引先
            this.form.TORIHIKISAKI_CD.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
            this.form.TORIHIKISAKI_NAME_RYAKU.Tag = " ";   //ヒントテキスト不要のためスペースを詰める
            // 請求:取引区分
            this.form.SEIKYUU_TORIHIKI_KBN_CD.Text = this.dto.sysInfoEntity.KEIRYOU_SEIKYUU_TORIHIKI_KBN_CD.ToString();
            // 請求:税計算区分
            this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text = this.dto.sysInfoEntity.KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD.ToString();
            // 請求:税区分
            this.form.SEIKYUU_ZEI_KBN_CD.Text = this.dto.sysInfoEntity.KEIRYOU_SEIKYUU_ZEI_KBN_CD.ToString();
            // 支払:取引区分
            this.form.SHIHARAI_TORIHIKI_KBN_CD.Text = this.dto.sysInfoEntity.KEIRYOU_SHIHARAI_TORIHIKI_KBN_CD.ToString();
            // 支払:税計算区分
            this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = this.dto.sysInfoEntity.KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD.ToString();
            // 支払:税区分
            this.form.SHIHARAI_ZEI_KBN_CD.Text = this.dto.sysInfoEntity.KEIRYOU_SHIHARAI_ZEI_KBN_CD.ToString();
            // キャッシャー連動
            this.form.CASHEIR_RENDOU_KBN_CD.Text = this.GetUserProfileValue(userProfile, "キャッシャ連動");
            // 領収書発行
            this.form.RECEIPT_KBN_CD.Text = this.GetUserProfileValue(userProfile, "領収書");
            if (string.IsNullOrEmpty(this.form.RECEIPT_KBN_CD.Text))
            {
                this.form.RECEIPT_KBN_CD.Text = "2";
            }
            // 領収書敬称
            this.form.RECEIPT_KEISHOU_1.Text = string.Empty;
            this.form.RECEIPT_KEISHOU_2.Text = string.Empty;
            // 領収書但し書き
            this.form.RECEIPT_TADASHIGAKI.Text = string.Empty;
            if (this.form.RECEIPT_KBN_CD.Text == "1" && this.form.SEIKYUU_TORIHIKI_KBN_CD.Text == "1")
            {
                this.form.RECEIPT_KEISHOU_1.Enabled = true;
                this.form.RECEIPT_KEISHOU_2.Enabled = true;
                this.form.RECEIPT_TADASHIGAKI.Enabled = true;

                string tadasigaki = Properties.Settings.Default.tadasigaki;
                if (!string.IsNullOrEmpty(tadasigaki))
                {
                    if (string.IsNullOrEmpty(this.form.RECEIPT_TADASHIGAKI.Text))
                    {   // データが設定されていない場合のみ前回値入れる
                        this.form.RECEIPT_TADASHIGAKI.Text = tadasigaki;
                    }
                }

            }
            else
            {
                this.form.RECEIPT_KEISHOU_1.Enabled = false;
                this.form.RECEIPT_KEISHOU_2.Enabled = false;
                this.form.RECEIPT_TADASHIGAKI.Enabled = false;
            }

            // 正味合計
            this.form.NET_TOTAL.Text = string.Empty;
            // 売上金額合計
            this.form.URIAGE_KINGAKU_TOTAL.Text = string.Empty;
            // 支払金額合計
            this.form.SHIHARAI_KINGAKU_TOTAL.Text = string.Empty;
            // 差額
            this.form.SAGAKU.Text = string.Empty;
            // 詳細 End

            // 明細 Start
            // テンプレートをいじる処理は、データ設定前に実行
            this.ExecuteAlignmentForDetail();
            this.form.gcMultiRow1.BeginEdit(false);
            this.form.gcMultiRow1.Rows.Clear();
            this.form.gcMultiRow1.EndEdit();
            this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
            // 明細 End

            //受入実績
            if (this.form.ismobile_mode)
            {
                //作業日時
                this.form.SAGYOU_DATE.Text = string.Empty;
                this.form.SAGYOU_HOUR.Text = string.Empty;
                this.form.SAGYOU_MINUTE.Text = string.Empty;
                //作業者
                this.form.SAGYOUSHA_CD.Text = string.Empty;
                this.form.SAGYOUSHA_NAME.Text = string.Empty;
                //作業時備考
                this.form.SAGYOU_BIKOU.Text = string.Empty;

                // 明細 Start
                this.form.SUURYOU_WARIAI_GOUKEI.Text = string.Empty;
                this.form.gcMultiRow2.BeginEdit(false);
                this.form.gcMultiRow2.Rows.Clear();
                this.form.gcMultiRow2.EndEdit();
                this.form.gcMultiRow2.NotifyCurrentCellDirty(false);
                // 明細 End

                // 添付ファイルタブ
                if (this.form.isfile_upload)
                {
                    this.form.dgvTenpuFileDetail.Rows.Clear();
                }
            }
        }

        /// <summary>
        /// 検索ボタンの設定をする
        /// デザインのマージ対策
        /// レイアウトの調整をするとぬめぬめ動くと思われるので、
        /// ポップアップの設定だけをセッティング
        /// </summary>
        internal void SetSearchButtonInfo()
        {
            // 車輌
            this.form.SHARYOU_SEARCH_BUTTON.PopupGetMasterField = this.form.SHARYOU_CD.PopupGetMasterField;
            this.form.SHARYOU_SEARCH_BUTTON.PopupSetFormField = this.form.SHARYOU_CD.PopupSetFormField;
            this.form.SHARYOU_SEARCH_BUTTON.PopupMultiSelect = this.form.SHARYOU_CD.PopupMultiSelect;
            this.form.SHARYOU_SEARCH_BUTTON.PopupSearchSendParams = this.form.SHARYOU_CD.PopupSearchSendParams;
            this.form.SHARYOU_SEARCH_BUTTON.PopupWindowId = this.form.SHARYOU_CD.PopupWindowId;
            this.form.SHARYOU_SEARCH_BUTTON.PopupWindowName = this.form.SHARYOU_CD.PopupWindowName;
            this.form.SHARYOU_SEARCH_BUTTON.popupWindowSetting = this.form.SHARYOU_CD.popupWindowSetting;

            // 車種
            this.form.SHASHU_SEARCH_BUTTON.PopupGetMasterField = this.form.SHASHU_CD.PopupGetMasterField;
            this.form.SHASHU_SEARCH_BUTTON.PopupSetFormField = this.form.SHASHU_CD.PopupSetFormField;
            this.form.SHASHU_SEARCH_BUTTON.PopupMultiSelect = this.form.SHASHU_CD.PopupMultiSelect;
            this.form.SHASHU_SEARCH_BUTTON.PopupSearchSendParams = this.form.SHASHU_CD.PopupSearchSendParams;
            this.form.SHASHU_SEARCH_BUTTON.PopupWindowId = this.form.SHASHU_CD.PopupWindowId;
            this.form.SHASHU_SEARCH_BUTTON.PopupWindowName = this.form.SHASHU_CD.PopupWindowName;
            this.form.SHASHU_SEARCH_BUTTON.popupWindowSetting = this.form.SHASHU_CD.popupWindowSetting;

            // 運転者
            this.form.UNTENSHA_SEARCH_BUTTON.PopupGetMasterField = this.form.UNTENSHA_CD.PopupGetMasterField;
            this.form.UNTENSHA_SEARCH_BUTTON.PopupSetFormField = this.form.UNTENSHA_CD.PopupSetFormField;
            this.form.UNTENSHA_SEARCH_BUTTON.PopupMultiSelect = this.form.UNTENSHA_CD.PopupMultiSelect;
            this.form.UNTENSHA_SEARCH_BUTTON.PopupSearchSendParams = this.form.UNTENSHA_CD.PopupSearchSendParams;
            this.form.UNTENSHA_SEARCH_BUTTON.PopupWindowId = this.form.UNTENSHA_CD.PopupWindowId;
            this.form.UNTENSHA_SEARCH_BUTTON.PopupWindowName = this.form.UNTENSHA_CD.PopupWindowName;
            this.form.UNTENSHA_SEARCH_BUTTON.popupWindowSetting = this.form.UNTENSHA_CD.popupWindowSetting;

            // 各CDのフォーカスアウト処理を通すため、検索ポップアップから戻ってきたら各CDへフォーカスする
            this.form.TORIHIKISAKI_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToTorihikisakiCd";
            // TODO: 業者、現場の検索ボタン名がおかしいため後で修正
            this.form.GHOUSYA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToGyoushaCd";
            this.form.GENBA_SEARCH_BUTTON.PopupBeforeExecuteMethod = "GenbaPopupBefore";
            this.form.GENBA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToGenbaCd";
            //this.form.SHARYOU_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToSharyouCd";
            this.form.SHASHU_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToShashuCd";
            this.form.UNPAN_GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToUnpanGyoushaCd";
            this.form.UNTENSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToUntenshaCd";
        }

        /// <summary>
        /// M_SYS_INFOでデフォルト値を設定
        /// 当該画面で必須かつ、Nullのものに値を設定する
        /// </summary>
        private void SetSysInfoDefaultValue()
        {
            // システム連番方法区分
            if (this.dto.sysInfoEntity.SYS_RENBAN_HOUHOU_KBN.IsNull)
            {
                this.dto.sysInfoEntity.SYS_RENBAN_HOUHOU_KBN = 1;
            }

            // 受入情報差引基準
            if (this.dto.sysInfoEntity.UKEIRE_CALC_BASE_KBN.IsNull)
            {
                this.dto.sysInfoEntity.UKEIRE_CALC_BASE_KBN = 1;
            }

            // 計量：基本計量
            if (this.dto.sysInfoEntity.KEIRYOU_KIHON_KEIRYOU.IsNull)
            {
                this.dto.sysInfoEntity.KEIRYOU_KIHON_KEIRYOU = 1;
            }

            // 出荷情報差引基準
            if (this.dto.sysInfoEntity.SHUKKA_CALC_BASE_KBN.IsNull)
            {
                this.dto.sysInfoEntity.SHUKKA_CALC_BASE_KBN = 1;
            }

            // 計量：調整値端数CD
            if (this.dto.sysInfoEntity.KEIRYOU_CHOUSEI_HASU_CD.IsNull)
            {
                this.dto.sysInfoEntity.KEIRYOU_CHOUSEI_HASU_CD = 1;
            }

            // 計量：調整値端数処理桁
            if (this.dto.sysInfoEntity.KEIRYOU_CHOUSEI_HASU_KETA.IsNull)
            {
                this.dto.sysInfoEntity.KEIRYOU_CHOUSEI_HASU_KETA = 1;
            }

            // 計量：調整割合端数CD
            if (this.dto.sysInfoEntity.KEIRYOU_CHOUSEI_WARIAI_HASU_CD.IsNull)
            {
                this.dto.sysInfoEntity.KEIRYOU_CHOUSEI_WARIAI_HASU_CD = 1;
            }

            // 計量：調整割合端数処理桁
            if (this.dto.sysInfoEntity.KEIRYOU_CHOUSEI_WARIAI_HASU_KETA.IsNull)
            {
                this.dto.sysInfoEntity.KEIRYOU_CHOUSEI_WARIAI_HASU_KETA = 1;
            }
        }

        /// <summary>EnterかTabボタンが押下されたかどうかの判定フラグ</summary>
        internal bool pressedEnterOrTab = false;

        /// <summary>
        /// 諸口区分用プレビューキーダウンイベント
        /// 諸口区分が存在する取引先、業者、現場で使用する
        /// ※例外として車輌でも使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PreviewKeyDownForShokuchikbnCheck(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Tab:
                    this.pressedEnterOrTab = true;
                    break;

                default:
                    this.pressedEnterOrTab = false;
                    break;
            }
        }

        /// <summary>
        /// 諸口区分用フォーカス移動処理
        /// </summary>
        /// <param name="control"></param>
        private void MoveToNextControlForShokuchikbnCheck(ICustomControl control)
        {
            if (this.pressedEnterOrTab)
            {
                var isPressShift = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
                this.form.SelectNextControl((Control)control, !isPressShift, true, true, true);
            }

            // マウス操作を考慮するためpressedEnterOrTabを初期化
            pressedEnterOrTab = false;
        }

        /// <summary>
        /// 調整kg, 調整%の入力制御
        /// </summary>
        internal bool ChangeInputStatusForChousei()
        {
            try
            {
                var row = this.form.gcMultiRow1.CurrentRow;

                if (row == null)
                {
                    return true;
                }

                bool isReadOnlyForStackJyuuryou = false;
                if (
                    (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value)))
                    || (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_CHOUSEI_PERCENT].Value)))
                    )
                {
                    isReadOnlyForStackJyuuryou = true;
                    isReadOnlyForStackJyuuryou = true;
                }

                row.Cells[CELL_NAME_STAK_JYUURYOU].ReadOnly = isReadOnlyForStackJyuuryou;
                row.Cells[CELL_NAME_EMPTY_JYUURYOU].ReadOnly = isReadOnlyForStackJyuuryou;

                row.Cells[CELL_NAME_STAK_JYUURYOU].UpdateBackColor(false);
                row.Cells[CELL_NAME_EMPTY_JYUURYOU].UpdateBackColor(false);

                // 調整のReadOnlyにデフォルト値が設定されるためここで新たに設定する
                bool isReadOnlyForChousei = true;
                if (
                    (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_STAK_JYUURYOU].Value)))
                    || (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_STAK_JYUURYOU].Value)))
                    )
                {
                    isReadOnlyForChousei = false;
                }

                row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].ReadOnly = isReadOnlyForChousei;
                row.Cells[CELL_NAME_CHOUSEI_PERCENT].ReadOnly = isReadOnlyForChousei;

                row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].UpdateBackColor(false);
                row.Cells[CELL_NAME_CHOUSEI_PERCENT].UpdateBackColor(false);

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeInputStatusForChousei", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 営業担当者の表示（現場マスタ、業者マスタ、取引先マスタを元に）
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <param name="gyoushaCd"></param>
        /// <param name="torihikisakiCd"></param>
        internal void SetEigyouTantousha(string genbaCd, string gyoushaCd, string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(genbaCd, gyoushaCd, torihikisakiCd);

            M_GENBA genbaEntity = new M_GENBA();
            M_SHAIN shainEntity = new M_SHAIN();
            string eigyouTantouCd = null;

            if (!string.IsNullOrEmpty(gyoushaCd))
            {
                // 業者CD入力あり
                if (!string.IsNullOrEmpty(genbaCd))
                {
                    bool catchErr = true;
                    var retData = accessor.GetGenba(gyoushaCd, genbaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                    if (!catchErr)
                    {
                        throw new Exception("");
                    }
                    // 現場CD入力あり
                    genbaEntity = retData;
                    if (genbaEntity != null)
                    {
                        // コードに対応する現場マスタが存在する
                        eigyouTantouCd = genbaEntity.EIGYOU_TANTOU_CD;
                        if (!string.IsNullOrEmpty(eigyouTantouCd))
                        {
                            // 現場マスタに営業担当者の設定がある場合
                            shainEntity = this.accessor.GetShain(eigyouTantouCd);
                            if (shainEntity != null)
                            {
                                // 現場CDで取得した現場マスタの営業担当者コードで、社員マスタを取得できた場合
                                if (!string.IsNullOrEmpty(shainEntity.SHAIN_NAME_RYAKU))
                                {
                                    // 取得した社員マスタの社員名略が設定されている場合
                                    this.form.EIGYOU_TANTOUSHA_CD.Text = shainEntity.SHAIN_CD;
                                    this.form.EIGYOU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                                }
                                else
                                {
                                    // 取得した社員マスタの社員名略が設定されていない場合
                                    GetEigyou_TantoushaOfGyousha(gyoushaCd, torihikisakiCd);
                                }
                            }
                            else
                            {
                                // 現場CDで取得した現場マスタの営業担当者コードで、社員マスタを取得できない場合
                                GetEigyou_TantoushaOfGyousha(gyoushaCd, torihikisakiCd);
                            }
                        }
                        else
                        {
                            // 現場マスタに営業担当者の設定がない場合
                            GetEigyou_TantoushaOfGyousha(gyoushaCd, torihikisakiCd);
                        }
                    }
                }
                else
                {
                    // 現場CD入力なし
                    GetEigyou_TantoushaOfGyousha(gyoushaCd, torihikisakiCd);
                }
            }
            else
            {
                // 業者CD入力なし
                GetEigyou_TantoushaOfTorihikisaki(torihikisakiCd);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者マスタの営業担当者コードからの営業担当者取得(業者CD入力あり、業者マスタに存在することが前提)
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="torihikisakiCd"></param>
        private void GetEigyou_TantoushaOfGyousha(string gyoushaCd, string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, torihikisakiCd);

            M_GYOUSHA gyoushaEntity = new M_GYOUSHA();
            M_SHAIN shainEntity = new M_SHAIN();
            string eigyouTantouCd = null;

            bool catchErr = true;
            var retData = this.accessor.GetGyousha(gyoushaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
            if (!catchErr)
            {
                throw new Exception("");
            }
            gyoushaEntity = retData;
            if (gyoushaEntity != null)
            {
                // コードに対応する業者マスタが存在する
                eigyouTantouCd = gyoushaEntity.EIGYOU_TANTOU_CD;
                if (!string.IsNullOrEmpty(eigyouTantouCd))
                {
                    // 業者マスタに営業担当者の設定がある場合
                    shainEntity = this.accessor.GetShain(eigyouTantouCd);
                    if (shainEntity != null)
                    {
                        // 業者CDで取得した業者マスタの営業担当者コードで、社員マスタを取得できた場合
                        if (!string.IsNullOrEmpty(shainEntity.SHAIN_NAME_RYAKU))
                        {
                            // 取得した社員マスタの社員名略が設定されている場合
                            this.form.EIGYOU_TANTOUSHA_CD.Text = shainEntity.SHAIN_CD;
                            this.form.EIGYOU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                        }
                        else
                        {
                            // 取得した社員マスタの社員名略が設定されていない場合
                            GetEigyou_TantoushaOfTorihikisaki(torihikisakiCd);
                        }
                    }
                    else
                    {
                        // 業者CDで取得した業者マスタの営業担当者コードで、社員マスタを取得できない場合
                        GetEigyou_TantoushaOfTorihikisaki(torihikisakiCd);
                    }
                }
                else
                {
                    // 業者マスタに営業担当者の設定がない場合
                    GetEigyou_TantoushaOfTorihikisaki(torihikisakiCd);
                }
            }
            else
            {
                // コードに対応する業者マスタが存在しない
                // ただし、マスタ存在チェックはこの前になされているので、ここを通ることはない
                return;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先マスタの営業担当者コードからの営業担当者取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        private void GetEigyou_TantoushaOfTorihikisaki(string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(torihikisakiCd);

            M_TORIHIKISAKI torihikisakiEntity = new M_TORIHIKISAKI();
            M_SHAIN shainEntity = new M_SHAIN();
            string eigyouTantouCd = null;

            if (!string.IsNullOrEmpty(torihikisakiCd))
            {
                // 取引先CD入力あり
                bool catchErr = true;
                torihikisakiEntity = this.accessor.GetTorihikisaki(torihikisakiCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (torihikisakiEntity != null)
                {
                    // コードに対応する取引先マスタが存在する
                    eigyouTantouCd = torihikisakiEntity.EIGYOU_TANTOU_CD;
                    if (!string.IsNullOrEmpty(eigyouTantouCd))
                    {
                        // 取引先マスタに営業担当者の設定がある場合
                        shainEntity = this.accessor.GetShain(eigyouTantouCd);
                        if (shainEntity != null)
                        {
                            // 取引先CDで取得した取引先マスタの営業担当者コードで、社員マスタを取得できた場合
                            if (!string.IsNullOrEmpty(shainEntity.SHAIN_NAME_RYAKU))
                            {
                                // 取得した社員マスタの社員名略が設定されている場合
                                this.form.EIGYOU_TANTOUSHA_CD.Text = shainEntity.SHAIN_CD;
                                this.form.EIGYOU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                            }
                            else
                            {
                                // 取得した社員マスタの社員名略が設定されていない場合
                                this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
                                this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
                            }
                        }
                        else
                        {
                            // 取引先CDで取得した取引先マスタの営業担当者コードで、社員マスタを取得できない場合
                            this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
                            this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
                        }
                    }
                    else
                    {
                        // 取引先マスタに営業担当者の設定がない場合
                        this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
                        this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
                    }
                }
                else
                {
                    // コードに対応する取引先マスタが存在しない
                    // ただし、マスタ存在チェックはこの前になされているので、ここを通ることはない
                    return;
                }
            }
            else
            {
                // 取引先CD入力なし
                this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
                this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
            }

            LogUtility.DebugMethodEnd();
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
        /// 指定された計量番号の次に大きい番号を取得
        /// </summary>
        /// <param name="KeiryouNumber"></param>
        /// <returns></returns>
        internal long GetNextKeiryouNumber(long KeiryouNumber)
        {
            // No.3341-->
            string KyotenCD = this.headerForm.KYOTEN_CD.Text;
            long returnValue = this.accessor.GetNextKeiryouNumber(KeiryouNumber, KyotenCD);
            if (returnValue == 0)
            {
                returnValue = this.accessor.GetNextKeiryouNumber(0, KyotenCD);
                if (returnValue == KeiryouNumber)
                {
                    returnValue = 0;
                }
            }
            // No.3341<--
            return returnValue;
        }

        /// <summary>
        /// 指定された計量番号の次に小さい番号を取得
        /// </summary>
        /// <param name="KeiryouNumber"></param>
        /// <returns></returns>
        internal long GetPreKeiryouNumber(long KeiryouNumber)
        {
            string KyotenCD = this.headerForm.KYOTEN_CD.Text;
            long returnValue = this.accessor.GetPreKeiryouNumber(KeiryouNumber, KyotenCD);
            if (returnValue == 0)
            {
                long max = this.accessor.GetMaxKeiryouNumber();
                returnValue = this.accessor.GetPreKeiryouNumber(max + 1, KyotenCD);
                if (returnValue == KeiryouNumber)
                {
                    returnValue = 0;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 指定された計量番号の次に小さい番号を取得
        /// </summary>
        /// <returns></returns>
        internal long GetMaxKeiryouNumber()
        {
            long returnValue = this.accessor.GetMaxKeiryouNumber();
            return returnValue;
        }

        /// <summary>
        /// 明細行に入力されている伝票区分の状況を取得
        /// </summary>
        /// <returns>0:売上と支払が混在 1:売上のみ 2:支払のみ</returns>
        internal int GetRowsDenpyouKbnCdMixed()
        {
            LogUtility.DebugMethodStart();

            int returnValue = URIAGE_SHIHARAI_MIXED;
            int currentRowdenKbn = URIAGE_SHIHARAI_MIXED;
            short denpyouKbnCd = 0;

            // 最初の明細行の状態を取得
            short.TryParse(Convert.ToString(this.form.gcMultiRow1.Rows[0].Cells[CELL_NAME_DENPYOU_KBN_CD].Value), out denpyouKbnCd);
            switch (denpyouKbnCd)
            {
                case SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE:
                    returnValue = URIAGE_ONLY;
                    break;
                case SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI:
                    returnValue = SHIHARAI_ONLY;
                    break;
            }

            // 各明細行の伝票区分を参照
            foreach (var row in this.form.gcMultiRow1.Rows)
            {
                short.TryParse(Convert.ToString(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value), out denpyouKbnCd);
                switch (denpyouKbnCd)
                {
                    case SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE:
                        currentRowdenKbn = URIAGE_ONLY;
                        break;
                    case SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI:
                        currentRowdenKbn = SHIHARAI_ONLY;
                        break;
                }
                // 最初の行と違う伝票区分が存在すれば、その時点で抜ける
                if (currentRowdenKbn != returnValue)
                {
                    returnValue = URIAGE_SHIHARAI_MIXED;
                    break;
                }
            }

            LogUtility.DebugMethodEnd();
            return returnValue;
        }

        /// <summary>
        /// ゼロサプレス処理
        /// </summary>
        /// <param name="source">入力コントロール</param>
        /// <returns>ゼロサプレス後の文字列</returns>
        private string ZeroSuppress(object source)
        {
            string result = string.Empty;

            // 該当コントロールの最大桁数を取得
            object obj;
            decimal charactersNumber;
            string text = PropertyUtility.GetTextOrValue(source);
            if (!PropertyUtility.GetValue(source, Constans.CHARACTERS_NUMBER, out obj))
                // 最大桁数が取得できない場合はそのまま
                return text;

            charactersNumber = (decimal)obj;
            if (charactersNumber == 0 || source == null || string.IsNullOrEmpty(text))
                // 最大桁数が0または入力値が空の場合はそのまま
                return text;

            var strCharactersUmber = text;
            if (strCharactersUmber.Contains("."))
                // 小数点を含む場合はそのまま
                return text;

            // ゼロサプレスした値を返す
            StringBuilder sb = new StringBuilder((int)charactersNumber);
            string format = sb.Append('#', (int)charactersNumber).ToString();
            long val = 0;
            if (long.TryParse(text, out val))
                result = val == 0 ? "0" : val.ToString(format);
            else
                // 入力値が数値ではない場合はそのまま
                result = text;

            return result;
        }

        /// <summary>
        /// 明細行の重量項目取得（計量票などの出力用）
        /// </summary>
        /// <param name="JuryoOption">0：総重量、1：空車重量</param>
        /// <returns>該当重量項目の値</returns>
        private string GetJuryoCol(int JuryoOption)
        {
            LogUtility.DebugMethodStart(JuryoOption);

            string returnVal = string.Empty;
            switch (JuryoOption)
            {
                case 0:
                    // 総重量取得（明細行のうち最初の行）
                    foreach (var row in this.form.gcMultiRow1.Rows)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_STAK_JYUURYOU].FormattedValue)))
                        {
                            returnVal = row.Cells[CELL_NAME_STAK_JYUURYOU].FormattedValue.ToString();
                            break;
                        }
                    }
                    break;
                case 1:
                    // 空車重量取得（明細行のうち最後の行）
                    foreach (var row in this.form.gcMultiRow1.Rows.Reverse())
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_EMPTY_JYUURYOU].FormattedValue)))
                        {
                            returnVal = row.Cells[CELL_NAME_EMPTY_JYUURYOU].FormattedValue.ToString();
                            break;
                        }
                    }
                    break;
            }

            LogUtility.DebugMethodEnd();
            return returnVal;
        }

        /// <summary>
        /// 伝票日付を基に売上消費税率を設定
        /// </summary>
        internal bool SetUriageShouhizeiRate()
        {
            try
            {
                DateTime uriageDate = this.footerForm.sysDate.Date;
                if (DateTime.TryParse(this.form.DENPYOU_DATE.Text, out uriageDate))
                {
                    var shouhizeiRate = this.accessor.GetShouhizeiRate(uriageDate);
                    if (shouhizeiRate != null && !shouhizeiRate.SHOUHIZEI_RATE.IsNull)
                    {
                        this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text = shouhizeiRate.SHOUHIZEI_RATE.ToString();
                    }
                }
                else
                {
                    this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text = string.Empty;
                }

                return true;

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetUriageShouhizeiRate", ex1);
                msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetUriageShouhizeiRate", ex);
                msgLogic.MessageBoxShow("E245", "");
                return false;
            }

        }

        /// <summary>
        /// 伝票日付を基に売上消費税率を設定
        /// </summary>
        internal bool SetShiharaiShouhizeiRate()
        {
            try
            {
                DateTime shiharaiDate = this.footerForm.sysDate.Date;
                if (DateTime.TryParse(this.form.DENPYOU_DATE.Text, out shiharaiDate))
                {
                    var shouhizeiRate = this.accessor.GetShouhizeiRate(shiharaiDate);
                    if (shouhizeiRate != null && !shouhizeiRate.SHOUHIZEI_RATE.IsNull)
                    {
                        this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text = shouhizeiRate.SHOUHIZEI_RATE.ToString();
                    }
                }
                else
                {
                    this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text = string.Empty;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShiharaiShouhizeiRate", ex);
                msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }
        #endregion

        /// <summary>
        /// UIFormの売上消費税率をパーセント表記で取得する
        /// </summary>
        /// <returns>パーセント表示の売上消費税率</returns>
        internal string ToPercentForUriageShouhizeiRate(out bool catchErr)
        {
            string returnVal = string.Empty;
            catchErr = true;

            try
            {
                if (!string.IsNullOrEmpty(this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text))
                {
                    decimal shouhizeiRate = 0;
                    if (!this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text.Contains("%")
                        && decimal.TryParse(this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text, out shouhizeiRate))
                    {
                        returnVal = shouhizeiRate.ToString("P");
                    }
                    else if (this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text.Contains("%"))
                    {
                        // 既に%表記ならそのまま返す
                        returnVal = this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text;
                    }
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ToPercentForUriageShouhizeiRate", ex);
                msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                return returnVal;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
        }

        /// <summary>
        /// UIFormの売上消費税率を小数点表記で取得する
        /// </summary>
        /// <returns>小数点表記の売上消費税率(DBへ格納できる値)</returns>
        internal decimal ToDecimalForUriageShouhizeiRate()
        {
            decimal returnVal = 0;

            if (!string.IsNullOrEmpty(this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text))
            {
                string tempUriageShouhizeiRate = string.Empty;

                if (!this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text.Contains("%"))
                {
                    tempUriageShouhizeiRate = this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text;
                }
                else
                {
                    tempUriageShouhizeiRate = this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text.Replace("%", "");
                }

                decimal shouhizeiRate = 0;
                if (decimal.TryParse(tempUriageShouhizeiRate, out shouhizeiRate))
                {
                    returnVal = shouhizeiRate / 100m;
                }
            }

            return returnVal;
        }

        /// <summary>
        /// UIFormの支払消費税率をパーセント表記で取得する
        /// </summary>
        /// <returns>パーセント表示の売上消費税率</returns>
        internal string ToPercentForShiharaiShouhizeiRate(out bool catchErr)
        {
            string returnVal = string.Empty;
            catchErr = true;
            try
            {
                if (!string.IsNullOrEmpty(this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text))
                {
                    decimal shouhizeiRate = 0;
                    if (!this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text.Contains("%")
                        && decimal.TryParse(this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text, out shouhizeiRate))
                    {
                        returnVal = shouhizeiRate.ToString("P");
                    }
                    else if (this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text.Contains("%"))
                    {
                        // 既に%表記ならそのまま返す
                        returnVal = this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text;
                    }
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ToPercentForShiharaiShouhizeiRate", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                return returnVal;
            }
        }

        /// <summary>
        /// UIFormの支払消費税率を小数点表記で取得する
        /// </summary>
        /// <returns>小数点表記の売上消費税率(DBへ格納できる値)</returns>
        internal decimal ToDecimalForShiharaiShouhizeiRate()
        {
            decimal returnVal = 0;

            if (!string.IsNullOrEmpty(this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text))
            {
                string tempUriageShouhizeiRate = string.Empty;

                if (!this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text.Contains("%"))
                {
                    tempUriageShouhizeiRate = this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text;
                }
                else
                {
                    tempUriageShouhizeiRate = this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text.Replace("%", "");
                }

                decimal shouhizeiRate = 0;
                if (decimal.TryParse(tempUriageShouhizeiRate, out shouhizeiRate))
                {
                    returnVal = shouhizeiRate / 100m;
                }
            }

            return returnVal;
        }

        /// <summary>文字列の指定位置に改行挿入する</summary>
        internal string InsertReturn(string str, int num)
        {
            string retstr = "";
            if (false == string.IsNullOrEmpty(str))
            {
                string s = str;
                int numberOfs = s.Count();
                while (numberOfs > num)
                {
                    retstr = retstr + s.Substring(0, num) + "\n";
                    s = s.Substring(num, numberOfs - num);
                    numberOfs -= num;
                }
                if (numberOfs > 0)
                {
                    retstr = retstr + s;
                }
            }
            return retstr;
        }

        /// <summary>
        /// 明細欄の品名をセットします
        /// </summary>
        /// <param name="row">現在のセルを含む行（CurrentRow）</param>
        internal bool SetHinmeiName(Row row)
        {
            try
            {
                if (row == null)
                {
                    return true;
                }
                bool catchErr = true;
                bool retChousei = this.CheckHinmeiCd(row, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (retChousei)    // 品名コードの存在チェック（伝種区分が受入/出荷、または共通）
                {
                    // 入力された品名コードが存在するとき
                    if (row.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value != null)
                    {
                        if (string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value.ToString()))
                        {
                            // 品名が空の場合再セット
                            this.GetHinmei(row, out catchErr);
                            if (!catchErr)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        // 品名が空の場合再セット
                        this.GetHinmei(row, out catchErr);
                        if (!catchErr)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetHinmeiName", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHinmeiName", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        #region 単位kg-品名数量制御処理

        /// <summary>
        /// 単位kg-品名数量制御処理
        /// </summary>
        /// <param name="cellName">対象Cell名称</param>
        /// <param name="row">対象行</param>
        /// <param name="isKingakuNotCalc">金額が行わないかのフラグ</param>
        public bool SetHinmeiSuuryou(string cellName, Row row, bool isKingakuNotCalc)
        {
            try
            {
                /*
                 * 以下のセルが変更された場合は正味重量、品名数量、マニ数量の同期をとる
                 *    総重量
                 *    空車重量
                 *    割振重量(kg)
                 *    割振(%)
                 *    調整重量(kg)
                 *    調整(%)
                 *    容器CD
                 *    容器数量
                 *    容器重量(kg)
                 *    品名CD
                 *    単位CD
                 */
                if (cellName.Equals(LogicClass.CELL_NAME_STAK_JYUURYOU)
                    || cellName.Equals(LogicClass.CELL_NAME_EMPTY_JYUURYOU)
                    || cellName.Equals(LogicClass.CELL_NAME_CHOUSEI_JYUURYOU)
                    || cellName.Equals(LogicClass.CELL_NAME_CHOUSEI_PERCENT)
                    || cellName.Equals(LogicClass.CELL_NAME_YOUKI_CD)
                    || cellName.Equals(LogicClass.CELL_NAME_YOUKI_SUURYOU)
                    || cellName.Equals(LogicClass.CELL_NAME_YOUKI_JYUURYOU)
                    || cellName.Equals(LogicClass.CELL_NAME_HINMEI_CD)
                    || cellName.Equals(LogicClass.CELL_NAME_UNIT_CD))
                {
                    object jyuuryou = row.Cells[LogicClass.CELL_NAME_NET_JYUURYOU].Value;
                    object unitcd = row.Cells[LogicClass.CELL_NAME_UNIT_CD].Value;

                    decimal value = 0;
                    if (jyuuryou != null && decimal.TryParse(Convert.ToString(jyuuryou), out value))
                    {
                        // 正味重量あり
                        if ("3".Equals(unitcd))
                        {
                            // 正味重量＝品名数量とする
                            row.Cells[LogicClass.CELL_NAME_SUURYOU].Value = value;
                        }
                        else
                        {
                            // 単位kg以外の場合は、品名数量変更可
                            row.Cells[LogicClass.CELL_NAME_SUURYOU].ReadOnly = false;
                        }

                        if (unitcd != null && (unitcd.ToString().Equals("1") || unitcd.ToString().Equals("3")))
                        {
                            // 正味重量ありかつ単位がkg,tの場合は品名数量変更不可
                            row.Cells[LogicClass.CELL_NAME_SUURYOU].ReadOnly = true;
                        }
                        else
                        {
                            // その他の場合は品名数量変更可
                            row.Cells[LogicClass.CELL_NAME_SUURYOU].ReadOnly = false;
                        }
                    }
                    else
                    {
                        // 正味重量なしの場合は品名数量手入力可（単位は何でもOK)
                        row.Cells[LogicClass.CELL_NAME_SUURYOU].ReadOnly = false;
                    }
                    row.Cells[LogicClass.CELL_NAME_SUURYOU].UpdateBackColor(false);

                    // 金額の再計算を行う
                    if (!isKingakuNotCalc)
                    {
                        if (!this.CalcDetaiKingaku(row))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHinmeiSuuryou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }

        }

        #endregion

        #region タブオーダー設定
        /// <summary>タブオーダー伝票データ格納</summary>
        internal void TabDataSetDenpyou()
        {
            try
            {
                int count = 0;
                if (DenpyouCtrl.Count > 0)
                {
                    DenpyouCtrl.Clear();
                }

                // UIFormのコントロールを制御
                List<string> formControlNameList = new List<string>();
                formControlNameList.AddRange(tabUiFormControlNames);
                foreach (var controlName in formControlNameList)
                {
                    Control control = controlUtil.FindControl(this.form, controlName);

                    if (control == null)
                    {
                        // headerを検索
                        control = controlUtil.FindControl(this.headerForm, controlName);
                    }

                    if (control == null)
                    {
                        continue;
                    }

                    var enabledProperty = control.GetType().GetProperty("Enabled");
                    var readOnlyProperty = control.GetType().GetProperty("ReadOnly");
                    var tabStopProperty = control.GetType().GetProperty("TabStop");
                    var tabOrderProperty = control.GetType().GetProperty("TabIndex");
                    var textProperty = control.GetType().GetProperty("DisplayItemName");

                    if (enabledProperty != null)
                    {
                        bool readOnlyValue = false;
                        if (readOnlyProperty != null)
                        {
                            readOnlyValue = (bool)readOnlyProperty.GetValue(control, null);
                        }

                        if (textProperty != null)   // ReadOnlyはチェックしないようにする
                        {
                            string text = (string)textProperty.GetValue(control, null);
                            bool tabStopValue = (bool)tabStopProperty.GetValue(control, null);
                            int tabOrderValue = (int)tabOrderProperty.GetValue(control, null);
                            if (!string.IsNullOrEmpty(text))
                            {
                                string liststring = string.Format("{0}:{1}:{2}:{3}", controlName, text, tabStopValue.ToString(), tabOrderValue.ToString());
                                DenpyouCtrl.Add(liststring);   // 有効なコントロール名のリスト作成
                                count++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
        }

        /// <summary>タブオーダー詳細データ格納</summary>
        internal void TabDataSetDetail()
        {
            try
            {
                int count = 0;
                if (DetailCtrl.Count > 0)
                {
                    DetailCtrl.Clear();
                }

                // UIFormのコントロールを制御
                List<string> formControlNameList = new List<string>();
                formControlNameList.AddRange(tabDetailControlNames);
                var row = this.form.gcMultiRow1.Template.Row;
                foreach (var controlName in formControlNameList)
                {
                    GrapeCity.Win.MultiRow.Cell control = row.Cells[controlName];
                    if (control == null)
                    {
                        continue;
                    }

                    var enabledProperty = control.GetType().GetProperty("Enabled");
                    var readOnlyProperty = control.GetType().GetProperty("ReadOnly");
                    var tabStopProperty = control.GetType().GetProperty("TabStop");
                    var tabOrderProperty = control.GetType().GetProperty("TabIndex");
                    var textProperty = control.GetType().GetProperty("DisplayItemName");

                    if (enabledProperty != null)
                    {
                        bool readOnlyValue = false;
                        if (readOnlyProperty != null)
                        {
                            readOnlyValue = (bool)readOnlyProperty.GetValue(control, null);
                        }

                        if (textProperty != null)   // ReadOnlyはチェックしないようにする
                        {
                            string text = (string)textProperty.GetValue(control, null);
                            bool tabStopValue = (bool)tabStopProperty.GetValue(control, null);
                            int tabOrderValue = (int)tabOrderProperty.GetValue(control, null);
                            if (!string.IsNullOrEmpty(text))
                            {
                                string liststring = string.Format("{0}:{1}:{2}:{3}", controlName, text, tabStopValue.ToString(), tabOrderValue.ToString());
                                DetailCtrl.Add(liststring);   // 有効なコントロール名のリスト作成
                                count++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
        }

        /// <summary>
        /// ステータス取得
        /// </summary>
        public void GetStatus()
        {
            bool dataUpdate = false;

            // タブオーダー伝票データ取得
            if (Properties.Settings.Default.ApDenpyouCtrl != null && Properties.Settings.Default.ApDenpyouCtrl.Count > 0)
            {
                if (DenpyouCtrl.Count > 0)
                {
                    DenpyouCtrl.Clear();
                }
                for (var i = 0; i < Properties.Settings.Default.ApDenpyouCtrl.Count; i++)
                {
                    DenpyouCtrl.Add(Properties.Settings.Default.ApDenpyouCtrl[i]);
                }

                // 設定に従いタブストップを変更
                for (var i = 0; i < DenpyouCtrl.Count; i++)
                {
                    // string分解
                    string str = DenpyouCtrl[i];
                    int ctpos = str.IndexOf(':');
                    string controlName = str.Substring(0, ctpos);
                    int nmpos = str.IndexOf(':', ctpos + 1);
                    int tspos = str.IndexOf(':', nmpos + 1);
                    string tbstop = str.Substring(nmpos + 1, tspos - nmpos - 1);

                    Control control = controlUtil.FindControl(this.form, controlName);
                    if (control == null)
                    {
                        // headerを検索
                        control = controlUtil.FindControl(this.headerForm, controlName);
                    }
                    if (control == null)
                    {
                        continue;
                    }
                    if (tbstop.Equals("True"))
                    {
                        Type type = control.GetType();
                        if (type.Name == "CustomTextBox" || type.BaseType.Name == "CustomTextBox")
                        {
                            if (!((CustomTextBox)control).ReadOnly)
                            {
                                control.TabStop = true;
                            }
                            else
                            {
                                control.TabStop = false;
                            }
                        }
                        else
                        {
                            control.TabStop = true;
                        }
                    }
                    else
                    {
                        control.TabStop = false;
                    }
                }
            }
            else
            {   // データが存在しない場合作成
                TabDataSetDenpyou();
                dataUpdate = true;
            }

            // タブオーダー詳細データ取得
            if (Properties.Settings.Default.DetailCtrl != null && Properties.Settings.Default.DetailCtrl.Count > 0 && CheckFocusStackEmptyJuuryou())
            {
                if (DetailCtrl.Count > 0)
                {
                    DetailCtrl.Clear();
                }
                for (var i = 0; i < Properties.Settings.Default.DetailCtrl.Count; i++)
                {
                    DetailCtrl.Add(Properties.Settings.Default.DetailCtrl[i]);
                }

                // 設定に従いタブストップを変更
                bool isSeted = false;
                var row = this.form.gcMultiRow1.Template.Row;
                for (var i = 0; i < DetailCtrl.Count; i++)
                {
                    // string分解
                    string str = DetailCtrl[i];
                    int ctpos = str.IndexOf(':');
                    string controlName = str.Substring(0, ctpos);
                    int nmpos = str.IndexOf(':', ctpos + 1);
                    int tspos = str.IndexOf(':', nmpos + 1);
                    string tbstop = str.Substring(nmpos + 1, tspos - nmpos - 1);

                    GrapeCity.Win.MultiRow.Cell control = row.Cells[controlName];
                    if (control == null)
                    {
                        continue;
                    }

                    if (tbstop.Equals("True"))
                    {
                        control.TabStop = true;
                        if (!isSeted)
                        {
                            this.firstIndexDetailCellName = controlName;
                            isSeted = true;
                        }
                    }
                    else
                    {
                        control.TabStop = false;
                    }
                }
            }
            else
            {   // データが存在しない場合作成
                TabDataSetDetail();
                dataUpdate = true;
            }

            if (dataUpdate == true)
            {
                //データ保存
                SetStatus();
            }
        }

        /// <summary>
        /// ステータス保存
        /// </summary>
        public void SetStatus()
        {
            // タブオーダー伝票データ格納
            if (DenpyouCtrl.Count > 0 && DenpyouCtrl != Properties.Settings.Default.DenpyouCtrl)
            {
                if (Properties.Settings.Default.DenpyouCtrl == null)
                {
                    Properties.Settings.Default.DenpyouCtrl = new System.Collections.Specialized.StringCollection();
                }
                if (Properties.Settings.Default.DenpyouCtrl != null)
                {
                    if (Properties.Settings.Default.DenpyouCtrl.Count > 0)
                    {
                        Properties.Settings.Default.DenpyouCtrl.Clear();
                    }
                    for (var i = 0; i < DenpyouCtrl.Count; i++)
                    {
                        Properties.Settings.Default.DenpyouCtrl.Add(DenpyouCtrl[i]);
                    }
                }
            }

            // タブオーダー詳細データ格納
            if (DetailCtrl.Count > 0 && DetailCtrl != Properties.Settings.Default.DetailCtrl)
            {
                if (Properties.Settings.Default.DetailCtrl == null)
                {
                    Properties.Settings.Default.DetailCtrl = new System.Collections.Specialized.StringCollection();
                }
                if (Properties.Settings.Default.DetailCtrl != null)
                {
                    if (Properties.Settings.Default.DetailCtrl.Count > 0)
                    {
                        Properties.Settings.Default.DetailCtrl.Clear();
                    }
                    for (var i = 0; i < DetailCtrl.Count; i++)
                    {
                        Properties.Settings.Default.DetailCtrl.Add(DetailCtrl[i]);
                    }
                }
            }

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 初期時フォーカス設定
        /// </summary>
        public bool SetTopControlFocus()
        {
            try
            {
                List<string> formControlNameList = new List<string>();
                formControlNameList.AddRange(tabUiFormControlNames);
                foreach (var controlName in formControlNameList)
                {
                    Control control = controlUtil.FindControl(this.headerForm, controlName);
                    ICustomAutoChangeBackColor autochange = (ICustomAutoChangeBackColor)controlUtil.FindControl(this.headerForm, controlName);
                    if (control != null)
                    {
                        if (control.TabStop == true && control.Visible == true && autochange.ReadOnly == false)
                        {
                            control.Focus();
                            return true;
                        }
                    }
                    else
                    {
                        control = controlUtil.FindControl(this.form, controlName);
                        autochange = (ICustomAutoChangeBackColor)controlUtil.FindControl(this.form, controlName);
                        if (control != null)
                        {
                            if (control.TabStop == true && control.Visible == true && autochange.ReadOnly == false)
                            {
                                control.Focus();
                                return true;
                            }
                        }
                    }
                }

                // 最後までみつからなかった場合
                // 詳細で最初を探す
                GrapeCity.Win.MultiRow.Cell gcontrol = NextDetailControl(true);
                if (gcontrol != null)
                {
                    gcontrol.Selected = true;
                    if (gcontrol.GcMultiRow != null)
                    {
                        gcontrol.GcMultiRow.Focus();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTopControlFocus", ex);
                msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 次のタブストップのコントロールにフォーカス移動
        /// </summary>
        /// <param name="foward"></param>
        public void GotoNextControl(bool foward)
        {
            Control control = NextFormControl(foward);
            if (control != null)
            {
                control.Focus();
            }
        }

        /// <summary>
        /// 現在のコントロールの次のタブストップコントールを探す
        /// </summary>
        /// <param name="foward"></param>
        /// <returns></returns>
        public Control NextFormControl(bool foward)
        {
            Control control = null;
            ICustomAutoChangeBackColor autochange = null;
            bool startflg = false;
            List<string> formControlNameList = new List<string>();

            formControlNameList.AddRange(tabUiFormControlNames);
            if (foward == false)
            {
                formControlNameList.Reverse();
            }
            foreach (var controlName in formControlNameList)
            {
                control = controlUtil.FindControl(this.headerForm, controlName);
                autochange = (ICustomAutoChangeBackColor)controlUtil.FindControl(this.headerForm, controlName);
                if (control != null)
                {
                    if (startflg)
                    {
                        // 次のコントロール
                        if (control.TabStop == true && control.Visible == true && autochange.ReadOnly == false)
                        {
                            return control;
                        }
                    }
                    else if (this.headerForm.ActiveControl != null && this.headerForm.ActiveControl.Equals(control))
                    {   // 現在のactiveコントロ－ル
                        startflg = true;
                    }
                }
                else
                {
                    control = controlUtil.FindControl(this.form, controlName);
                    if (control is r_framework.CustomControl.CustomComboBox == false)
                    {
                        autochange = (ICustomAutoChangeBackColor)controlUtil.FindControl(this.form, controlName);
                    }
                    if (control != null)
                    {
                        if (startflg)
                        {
                            // 次のコントロール
                            if (control.TabStop == true && control.Visible == true && autochange.ReadOnly == false)
                            {
                                return control;
                            }
                        }
                        else if (this.form.ActiveControl != null && this.form.ActiveControl.Equals(control))
                        {   // 現在のactiveコントロ－ル
                            startflg = true;
                        }
                    }
                }
            }

            // 最後までみつからなかった場合
            // 詳細で最初を探す
            GrapeCity.Win.MultiRow.Cell gcontrol = NextDetailControl(foward);
            if (gcontrol != null)
            {
                if (this.form.gcMultiRow1.CurrentCellPosition.Equals(new GrapeCity.Win.MultiRow.CellPosition(gcontrol.RowIndex, gcontrol.Name)))
                {
                    // 既に対象のpositionの場合、セルが編集モードにならないので強制的に修正モードにする。
                    this.form.gcMultiRow1.BeginEdit(true);
                }
                else
                {
                    this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(gcontrol.RowIndex, gcontrol.Name);
                }
                if (gcontrol.GcMultiRow != null)
                {
                    gcontrol.GcMultiRow.Focus();
                }
                return null;
            }

            // 詳細でタブストップが無い場合最初から検索
            foreach (var controlName in formControlNameList)
            {
                control = controlUtil.FindControl(this.headerForm, controlName);
                autochange = (ICustomAutoChangeBackColor)controlUtil.FindControl(this.headerForm, controlName);
                if (control != null)
                {
                    if (control.TabStop == true && control.Visible == true && autochange.ReadOnly == false)
                    {
                        return control;
                    }
                }
                else
                {
                    control = controlUtil.FindControl(this.form, controlName);
                    autochange = (ICustomAutoChangeBackColor)controlUtil.FindControl(this.form, controlName);
                    if (control != null)
                    {
                        if (control.TabStop == true && control.Visible == true && autochange.ReadOnly == false)
                        {
                            return control;
                        }
                    }
                }
            }
            return control;
        }

        /// <summary>
        /// 詳細の最初のコントロールにフォーカス移動
        /// </summary>
        /// <param name="foward"></param>
        public Cell NextDetailControl(bool foward)
        {
            List<string> sformControlNameList = new List<string>();
            sformControlNameList.AddRange(tabDetailControlNames);
            if (foward == false)
            {
                sformControlNameList.Reverse();
            }

            GrapeCity.Win.MultiRow.Cell control = null;
            foreach (var controlName in sformControlNameList)
            {
                var tmprow = this.form.gcMultiRow1.Template.Row;
                GrapeCity.Win.MultiRow.Cell tmpcell = tmprow.Cells[controlName];
                if (tmpcell != null)
                {
                    if (tmpcell.TabStop == true && tmpcell.Visible == true && tmpcell.ReadOnly == false)    // テンプレートのタブストップで判断
                    {
                        var currentrow = this.form.gcMultiRow1.Rows[0];
                        if (foward == false)
                        {   // 最後の場合、最後の行の最後のセル
                            var last = this.form.gcMultiRow1.RowCount - 1;
                            currentrow = this.form.gcMultiRow1.Rows[last];
                        }
                        if (currentrow != null)
                        {
                            control = currentrow.Cells[controlName];
                        }
                        return control;
                    }
                }
            }
            return control;
        }

        /// <summary>
        /// タブストップ情報取得(詳細含まず)
        /// </summary>
        /// <returns></returns>
        private bool GetTabStop(string cname)
        {
            bool tabstop = false;
            for (var i = 0; i < DenpyouCtrl.Count; i++)
            {
                string str = DenpyouCtrl[i];
                int ctpos = str.IndexOf(':');
                string controlName = str.Substring(0, ctpos);

                if (cname.Equals(controlName))
                {
                    int nmpos = str.IndexOf(':', ctpos + 1);
                    int tspos = str.IndexOf(':', nmpos + 1);
                    string tbstop = str.Substring(nmpos + 1, tspos - nmpos - 1);

                    Control control = controlUtil.FindControl(this.form, controlName);
                    if (control == null)
                    {
                        control = controlUtil.FindControl(this.headerForm, controlName);
                    }
                    if (control == null)
                    {
                        continue;
                    }
                    if (tbstop.Equals("True"))
                    {
                        tabstop = true;
                    }
                    break;
                }
            }
            return tabstop;
        }

        #endregion タブオーダー設定

        /// <summary>
        /// データ移動処理
        /// </summary>
        internal bool SetMoveData()
        {
            try
            {
                if (this.form.moveData_flg)
                {
                    this.form.TORIHIKISAKI_CD.Text = this.form.moveData_torihikisakiCd;
                    bool catchErr = true;
                    this.CheckTorihikisaki(out catchErr);
                    if (!catchErr)
                    {
                        return false;
                    }

                    this.form.GYOUSHA_CD.Text = this.form.moveData_gyousyaCd;
                    catchErr = true;
                    this.CheckGyousha(out catchErr);
                    if (!catchErr)
                    {
                        return false;
                    }

                    this.form.GENBA_CD.Text = this.form.moveData_genbaCd;

                    catchErr = true;
                    this.CheckGenba(out catchErr);
                    if (!catchErr)
                    {
                        return false;
                    }

                    this.hasShow = false;
                }
                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetMoveData", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetMoveData", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                return false;
            }
        }

        /// <summary>
        /// 現在入力されている情報から諸口状態の車輌CDかチェック
        /// 諸口状態だった場合は、車輌CD、車輌名のデザインを諸口状態用に変更する
        /// </summary>
        internal void CheckShokuchiSharyou()
        {
            var sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null, SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()), this.headerForm.KIHON_KEIRYOU.Text);
            if (sharyouEntitys == null || sharyouEntitys.Length < 1)
            {
                this.ChangeShokuchiSharyouDesign();
            }
        }

        /// <summary>
        /// 車輌CD、車輌名を諸口状態のデザインへ変更する
        /// </summary>
        internal void ChangeShokuchiSharyouDesign()
        {
            this.form.oldSharyouShokuchiKbn = true;
            this.form.SHARYOU_NAME_RYAKU.ReadOnly = false;
            this.form.SHARYOU_NAME_RYAKU.TabStop = GetTabStop("SHARYOU_NAME_RYAKU");
            this.form.SHARYOU_NAME_RYAKU.Tag = this.sharyouHinttext;
            // 自由入力可能であるため車輌名の色を変更
            this.form.SHARYOU_CD.AutoChangeBackColorEnabled = false;
            this.form.SHARYOU_CD.BackColor = sharyouCdBackColor;
        }

        #region 委託契約書チェック
        /// <summary>
        /// 委託契約書チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckItakukeiyaku(out bool catchErr)
        {
            catchErr = true;
            try
            {
                M_SYS_INFO sysInfo = new M_SYS_INFO();
                IM_SYS_INFODao sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

                M_SYS_INFO[] sysInfos = sysInfoDao.GetAllData();
                if (sysInfos != null && sysInfos.Length > 0)
                {
                    sysInfo = sysInfos[0];
                }
                else
                {
                    return true;
                }

                CustomAlphaNumTextBox txtGyoushaCd = this.form.GYOUSHA_CD;
                CustomAlphaNumTextBox txtGenbaCd = this.form.GENBA_CD;
                CustomDateTimePicker txtSagyouDate = this.form.DENPYOU_DATE;
                GcCustomMultiRow gridDetail = this.form.gcMultiRow1;
                string CTL_NAME_DETAIL = "HINMEI_CD";
                string CTL_NAME_DETAIL_NAME = "HINMEI_NAME";

                //委託契約チェックDtoを取得
                ItakuCheckDTO checkDto = new ItakuCheckDTO();
                checkDto.MANIFEST_FLG = false;
                checkDto.GYOUSHA_CD = txtGyoushaCd.Text;
                checkDto.GENBA_CD = txtGenbaCd.Text;
                checkDto.SAGYOU_DATE = txtSagyouDate.Text;
                checkDto.LIST_HINMEI_HAIKISHURUI = new List<DetailDTO>();

                foreach (Row row in gridDetail.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }
                    DetailDTO detailDto = new DetailDTO();
                    detailDto.CD = Convert.ToString(row.Cells[CTL_NAME_DETAIL].Value);
                    detailDto.NAME = Convert.ToString(row.Cells[CTL_NAME_DETAIL_NAME].Value);
                    checkDto.LIST_HINMEI_HAIKISHURUI.Add(detailDto);
                }

                ItakuKeiyakuCheckLogic itakuLogic = new ItakuKeiyakuCheckLogic();
                bool isCheck = itakuLogic.IsCheckItakuKeiyaku(sysInfo, checkDto);
                //委託契約チェックを処理しない場合
                if (isCheck == false)
                {
                    return true;
                }

                //委託契約チェック
                ItakuErrorDTO error = itakuLogic.ItakuKeiyakuCheck(checkDto);

                //エラーなし
                if (error.ERROR_KBN == (short)ITAKU_ERROR_KBN.NONE)
                {
                    return true;
                }

                bool ret = itakuLogic.ShowError(error, sysInfo.ITAKU_KEIYAKU_ALERT_AUTH, checkDto.MANIFEST_FLG, txtGyoushaCd, txtGenbaCd, txtSagyouDate, gridDetail, CTL_NAME_DETAIL);
                return ret;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckItakukeiyaku", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckItakukeiyaku", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                return false;
            }
        }
        #endregion

        #region 休動対応箇所
        // HACK 休動処理実装済みだが計量将軍では未使用
        #region 車輌休動チェック
        internal bool SharyouDateCheck(out bool catchErr)
        {
            catchErr = true;
            try
            {
                string inputUnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
                string inputSharyouCd = this.form.SHARYOU_CD.Text;
                string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Text);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return true;
                }

                M_WORK_CLOSED_SHARYOU workclosedsharyouEntry = new M_WORK_CLOSED_SHARYOU();
                //運搬業者CD
                workclosedsharyouEntry.GYOUSHA_CD = inputUnpanGyoushaCd;
                //車輌CD取得
                workclosedsharyouEntry.SHARYOU_CD = inputSharyouCd;
                //伝票日付取得
                workclosedsharyouEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);

                M_WORK_CLOSED_SHARYOU[] workclosedsharyouList = workclosedsharyouDao.GetAllValidData(workclosedsharyouEntry);

                //取得テータ
                if (workclosedsharyouList.Count() >= 1)
                {
                    this.form.SHARYOU_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E206", "車輌", "伝票日付：" + workclosedsharyouEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    return false;
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SharyouDateCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SharyouDateCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                return false;
            }
        }
        #endregion

        #region 運転者休動チェック
        internal bool UntenshaDateCheck(out bool catchErr)
        {
            catchErr = true;
            try
            {
                string inputUntenshaCd = this.form.UNTENSHA_CD.Text;
                string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Text);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return true;
                }

                M_WORK_CLOSED_UNTENSHA workcloseduntenshaEntry = new M_WORK_CLOSED_UNTENSHA();
                //運転者CD取得
                workcloseduntenshaEntry.SHAIN_CD = inputUntenshaCd;
                //作業日取得
                workcloseduntenshaEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);

                M_WORK_CLOSED_UNTENSHA[] workcloseduntenshaList = workcloseduntenshaDao.GetAllValidData(workcloseduntenshaEntry);

                //取得テータ
                if (workcloseduntenshaList.Count() >= 1)
                {
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E206", "運転者", "伝票日付：" + workcloseduntenshaEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    return false;
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("UntenshaDateCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UntenshaDateCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                return false;
            }
        }
        #endregion

        #region 搬入先休動チェック
        internal bool HannyuusakiDateCheck(out bool catchErr)
        {
            catchErr = true;
            try
            {
                string inputNioroshiGyoushaCd = this.form.NIOROSHI_GYOUSHA_CD.Text;
                string inputNioroshiGenbaCd = this.form.NIOROSHI_GENBA_CD.Text;
                string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Text);
                string inputUntenshaCd = this.form.UNTENSHA_CD.Text;

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return true;
                }

                M_WORK_CLOSED_HANNYUUSAKI workclosedhannyuusakiEntry = new M_WORK_CLOSED_HANNYUUSAKI();
                //荷降業者CD取得
                workclosedhannyuusakiEntry.GYOUSHA_CD = inputNioroshiGyoushaCd;
                //荷降現場CD取得
                workclosedhannyuusakiEntry.GENBA_CD = inputNioroshiGenbaCd;
                //作業日取得
                workclosedhannyuusakiEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);

                M_WORK_CLOSED_HANNYUUSAKI[] workclosedhannyuusakiList = workclosedhannyuusakiDao.GetAllValidData(workclosedhannyuusakiEntry);

                //取得テータ
                if (workclosedhannyuusakiList.Count() >= 1)
                {
                    this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E206", "荷降現場", "伝票日付：" + workclosedhannyuusakiEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    return false;
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("HannyuusakiDateCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HannyuusakiDateCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                return false;
            }
        }
        #endregion
        #endregion

        #region 現金取引チェック
        /// <summary>
        /// 現金取引チェック
        /// </summary>
        /// <returns>
        /// true  = 取引区分の売上支払のどちらかが現金　確定
        /// false = 取引区分の売上支払のどちらかが現金　未確定
        /// </returns>
        internal bool GenkinTorihikiCheck(out bool catchErr)
        {
            catchErr = true;
            var ren = true;
            try
            {
                var uriageTorihikiKbn = this.form.SEIKYUU_TORIHIKI_KBN_NAME.Text;
                var shiharaiTorihikiKbn = this.form.SHIHARAI_TORIHIKI_KBN_NAME.Text;
                var genkin = SalesPaymentConstans.STR_TORIHIKI_KBN_1;
                var uriageRowCount = 0;
                var siharaiRowCount = 0;
                var denpyouKbnCuloumnIndex = this.form.gcMultiRow1.Columns["DENPYOU_KBN_CD"].Index;

                // 売上
                if (uriageTorihikiKbn == genkin)
                {
                    // 明細の売上行数
                    uriageRowCount = this.form.gcMultiRow1.Rows.Cast<GrapeCity.Win.MultiRow.Row>().ToList().
                                        Where(r => r.Cells[denpyouKbnCuloumnIndex].Value != null).ToList().
                                        Where(r => r.Cells[denpyouKbnCuloumnIndex].Value.ToString() == "1").Count();
                }

                // 支払
                if (shiharaiTorihikiKbn == genkin)
                {
                    // 明細の支払行数
                    siharaiRowCount = this.form.gcMultiRow1.Rows.Cast<GrapeCity.Win.MultiRow.Row>().ToList().
                                        Where(r => r.Cells[denpyouKbnCuloumnIndex].Value != null).ToList().
                                        Where(r => r.Cells[denpyouKbnCuloumnIndex].Value.ToString() == "2").Count();
                }

                return ren;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GenkinTorihikiCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GenkinTorihikiCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            return ren;
        }
        #endregion

        #region キャッシャ連動
        /// <summary>
        /// キャッシャ情報送信
        /// </summary>
        private void SendCasher()
        {
            // 売上金額算出※現金の場合のみ
            decimal uriKin = 0;
            if (this.dto.entryEntity.URIAGE_TORIHIKI_KBN_CD == CommonConst.TORIHIKI_KBN_GENKIN)
            {
                // 金額
                decimal kin = (this.dto.entryEntity.URIAGE_KINGAKU_TOTAL.Value + this.dto.entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL.Value);

                // 税
                // 税計算区分が伝票毎の場合は伝票毎消費税を用いる
                decimal tax = 0;
                if (this.dto.entryEntity.URIAGE_ZEI_KEISAN_KBN_CD == CommonConst.ZEI_KEISAN_KBN_DENPYOU)
                {
                    // 伝票毎消費税
                    tax = (this.dto.entryEntity.URIAGE_TAX_SOTO.Value + this.dto.entryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL.Value);
                }
                else
                {
                    // 明細毎消費税合計
                    tax = (this.dto.entryEntity.URIAGE_TAX_SOTO_TOTAL.Value + this.dto.entryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL.Value);
                }

                // 合計
                uriKin = (kin + tax);
            }

            // 支払金額算出※現金の場合のみ
            decimal shiKin = 0;
            if (this.dto.entryEntity.SHIHARAI_TORIHIKI_KBN_CD == CommonConst.TORIHIKI_KBN_GENKIN)
            {
                // 金額
                decimal kin = (this.dto.entryEntity.SHIHARAI_KINGAKU_TOTAL.Value + this.dto.entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL.Value);

                // 税
                // 税計算区分が伝票毎の場合は伝票毎消費税を用いる
                decimal tax = 0;
                if (this.dto.entryEntity.SHIHARAI_ZEI_KEISAN_KBN_CD == CommonConst.ZEI_KEISAN_KBN_DENPYOU)
                {
                    // 伝票毎消費税
                    tax = (this.dto.entryEntity.SHIHARAI_TAX_SOTO.Value + this.dto.entryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL.Value);
                }
                else
                {
                    // 明細毎消費税合計
                    tax = (this.dto.entryEntity.SHIHARAI_TAX_SOTO_TOTAL.Value + this.dto.entryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL.Value);
                }

                // 合計
                shiKin = (kin + tax);
            }

            // 差引０円の場合はキャッシャ情報の送信を行わない
            var kingaku = (uriKin - shiKin);
            if (kingaku != 0)
            {
                // キャッシャ用DTO生成
                var casherDto = new CasherDTOClass();
                casherDto.DENPYOU_DATE = this.dto.entryEntity.DENPYOU_DATE.Value;
                casherDto.NYUURYOKU_TANTOUSHA_CD = this.dto.entryEntity.NYUURYOKU_TANTOUSHA_CD;
                casherDto.DENPYOU_NUMBER = this.dto.entryEntity.KEIRYOU_NUMBER.Value;
                casherDto.KINGAKU = kingaku;
                casherDto.BIKOU = (string.IsNullOrEmpty(this.dto.entryEntity.DENPYOU_BIKOU) ? string.Empty : this.dto.entryEntity.DENPYOU_BIKOU);
                casherDto.DENSHU_KBN_CD = CommonConst.DENSHU_KBN_KEIRYOU;
                casherDto.KYOTEN_CD = this.dto.entryEntity.KYOTEN_CD.Value;

                // キャッシャ共通処理に情報セット
                var casherAccessor = new CasherAccessor();
                casherAccessor.setCasherData(casherDto);
            }
        }
        #endregion キャッシャ連動

        public void getListTorihikisakiDefault()
        {
            var keyEntity = new M_TORIHIKISAKI();
            r_framework.Dao.IM_TORIHIKISAKIDao torihikisakiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKIDao>();
            this.torihikisakiList = torihikisakiDao.GetAllValidData(keyEntity).ToList();
        }

        public void getListGyoushaDefault()
        {
            var keyEntity = new M_GYOUSHA();
            r_framework.Dao.IM_GYOUSHADao gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            this.gyoushaList = gyoushaDao.GetAllValidData(keyEntity).ToList();
        }

        public void getListGenbaDefault()
        {
            var keyEntity = new M_GENBA();
            r_framework.Dao.IM_GENBADao genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
            this.genbaList = genbaDao.GetAllValidData(keyEntity).ToList();
        }

        public void getListManifestShuruiDefault()
        {
            var keyEntity = new M_MANIFEST_SHURUI();
            var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_MANIFEST_SHURUIDao>();
            this.manifestShuruiList = dao.GetAllValidData(keyEntity).ToList();
        }

        public void getListManifestTeihaiDefault()
        {
            var keyEntity = new M_MANIFEST_TEHAI();
            var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_MANIFEST_TEHAIDao>();
            this.manifestTehaiList = dao.GetAllValidData(keyEntity).ToList();
        }

        public void getDefaultList()
        {
            getListManifestShuruiDefault();
            getListManifestTeihaiDefault();
        }

        private void CheckTorihikisakiShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                bool catchErr = true;
                var torihikisakiEntity = this.accessor.GetTorihikisaki(this.form.TORIHIKISAKI_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (!catchErr)
                {
                    throw new Exception("");
                }
                if (null != torihikisakiEntity)
                {
                    this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = !(bool)torihikisakiEntity.SHOKUCHI_KBN;
                    this.form.TORIHIKISAKI_NAME_RYAKU.Tag = (bool)torihikisakiEntity.SHOKUCHI_KBN ? this.torihikisakiHintText : string.Empty;
                    if (!this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly)
                    {
                        this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = GetTabStop("TORIHIKISAKI_NAME_RYAKU");
                    }
                }
            }
        }

        private void CheckGyoushaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                bool catchErr = true;
                var retData = this.accessor.GetGyousha(this.form.GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (!catchErr)
                {
                    throw new Exception("");
                }
                var gyoushaEntity = retData;
                if (null != gyoushaEntity)
                {
                    this.form.GYOUSHA_NAME_RYAKU.ReadOnly = !(bool)gyoushaEntity.SHOKUCHI_KBN;
                    this.form.GYOUSHA_NAME_RYAKU.Tag = (bool)gyoushaEntity.SHOKUCHI_KBN ? this.gyoushaHintText : string.Empty;
                    if (!this.form.GYOUSHA_NAME_RYAKU.ReadOnly)
                    {
                        this.form.GYOUSHA_NAME_RYAKU.TabStop = GetTabStop("GYOUSHA_NAME_RYAKU");
                    }
                }
            }
        }

        private void CheckGenbaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) && !string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                bool catchErr = true;
                var retData = this.accessor.GetGenba(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (!catchErr)
                {
                    throw new Exception("");
                }
                var genbaEntity = retData;
                if (null != genbaEntity)
                {
                    this.form.GENBA_NAME_RYAKU.ReadOnly = !(bool)genbaEntity.SHOKUCHI_KBN;
                    this.form.GENBA_NAME_RYAKU.Tag = (bool)genbaEntity.SHOKUCHI_KBN ? this.genbaHintText : string.Empty;
                    if (!this.form.GENBA_NAME_RYAKU.ReadOnly)
                    {
                        this.form.GENBA_NAME_RYAKU.TabStop = GetTabStop("GENBA_NAME_RYAKU");
                    }
                }
            }
        }

        private void CheckNioroshiGyoushaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
            {
                bool catchErr = true;
                var retDate = this.accessor.GetGyousha(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (!catchErr)
                {
                    throw new Exception("");
                }
                var gyousha = retDate;
                if (gyousha != null)
                {
                    if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue || gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue)
                    {
                        this.form.NIOROSHI_GYOUSHA_NAME.ReadOnly = !(bool)gyousha.SHOKUCHI_KBN;
                        this.form.NIOROSHI_GYOUSHA_NAME.Tag = (bool)gyousha.SHOKUCHI_KBN ? this.nioroshiGyoushaHintText : string.Empty;
                        if (!this.form.NIOROSHI_GYOUSHA_NAME.ReadOnly)
                        {
                            this.form.NIOROSHI_GYOUSHA_NAME.TabStop = GetTabStop("NIOROSHI_GYOUSHA_NAME");
                        }
                    }
                }
            }
        }

        private void CheckNioroshiGenbaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text) && !string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
            {
                var genbaEntityList = this.accessor.GetGenbaList(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.NIOROSHI_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                M_GENBA genba = new M_GENBA();

                if (genbaEntityList != null && genbaEntityList.Length > 0)
                {
                    genba = genbaEntityList[0];
                    if (genba.TSUMIKAEHOKAN_KBN.IsTrue || genba.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || genba.SAISHUU_SHOBUNJOU_KBN.IsTrue)
                    {
                        this.form.NIOROSHI_GENBA_NAME.ReadOnly = !(bool)genba.SHOKUCHI_KBN;
                        this.form.NIOROSHI_GENBA_NAME.Tag = (bool)genba.SHOKUCHI_KBN ? this.nioroshiGenbaHintText : string.Empty;
                        if (!this.form.NIOROSHI_GENBA_NAME.ReadOnly)
                        {
                            this.form.NIOROSHI_GENBA_NAME.TabStop = GetTabStop("NIOROSHI_GENBA_NAME");
                        }
                    }
                }
            }
        }

        private void CheckNizumiGyoushaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
            {
                bool catchErr = true;
                var retDate = this.accessor.GetGyousha(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (!catchErr)
                {
                    throw new Exception("");
                }
                var gyousha = retDate;
                if (gyousha != null)
                {
                    if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue || gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue)
                    {
                        this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = !(bool)gyousha.SHOKUCHI_KBN;
                        this.form.NIZUMI_GYOUSHA_NAME.Tag = (bool)gyousha.SHOKUCHI_KBN ? this.nizumiGyoushaHintText : string.Empty;
                        if (!this.form.NIZUMI_GYOUSHA_NAME.ReadOnly)
                        {
                            this.form.NIZUMI_GYOUSHA_NAME.TabStop = GetTabStop("NIZUMI_GYOUSHA_NAME");
                        }
                    }
                }
            }
        }

        private void CheckNizumiGenbaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text) && !string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text))
            {
                var genbaEntityList = this.accessor.GetGenbaList(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.NIZUMI_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                M_GENBA genba = new M_GENBA();

                if (genbaEntityList != null && genbaEntityList.Length > 0)
                {
                    genba = genbaEntityList[0];
                    if (genba.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue)
                    {
                        this.form.NIZUMI_GENBA_NAME.ReadOnly = !(bool)genba.SHOKUCHI_KBN;
                        this.form.NIZUMI_GENBA_NAME.Tag = (bool)genba.SHOKUCHI_KBN ? this.nizumiGenbaHintText : string.Empty;
                        if (!this.form.NIZUMI_GENBA_NAME.ReadOnly)
                        {
                            this.form.NIZUMI_GENBA_NAME.TabStop = GetTabStop("NIZUMI_GENBA_NAME");
                        }
                    }
                }
            }
        }

        private void CheckUpanGyoushaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                bool catchErr = true;
                var retData = this.accessor.GetGyousha(this.form.UNPAN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (!catchErr)
                {
                    throw new Exception("");
                }
                var gyousha = retData;
                if (gyousha != null)
                {
                    if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                    {
                        this.form.UNPAN_GYOUSHA_NAME.ReadOnly = !(bool)gyousha.SHOKUCHI_KBN;
                        this.form.UNPAN_GYOUSHA_NAME.Tag = (bool)gyousha.SHOKUCHI_KBN ? this.unpanGyoushaHintText : string.Empty;
                        if (!this.form.UNPAN_GYOUSHA_NAME.ReadOnly)
                        {
                            this.form.UNPAN_GYOUSHA_NAME.TabStop = GetTabStop("UNPAN_GYOUSHA_NAME");
                        }
                    }
                }
            }
        }

        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }

        #region 画面処理
        /// <summary>
        /// 受入／出荷の画面切替を実行する
        /// </summary>
        /// <param name="ukeireShukkaKbn">を表す文字列</param>
        internal void ChangeUkeireShukkaKbn(string ukeireShukkaKbn)
        {
            if (!string.IsNullOrEmpty(this.form.ENTRY_NUMBER.Text))
            {
                return;
            }

            if (this.form.ismobile_mode)
            {
                _tabPageManager.ChangeTabPageVisible(1, true);
            }

            // 基本計量セット
            this.headerForm.KIHON_KEIRYOU.Text = ukeireShukkaKbn;

            //伝種区分セット
            if (ukeireShukkaKbn == "1")
            {
                this.form.selectDenshuKbnCd = DENSHU_KBN.UKEIRE;
            }
            else
            {
                this.form.selectDenshuKbnCd = DENSHU_KBN.SHUKKA;
            }
            this.form.ChangeNewWindow(null, new EventArgs());
        }

        /// <summary>
        /// 受入／出荷の画面切替を実行する
        /// </summary>
        /// <param name="ukeireShukkaKbn">を表す文字列</param>
        internal void SetUkeireShukkaKbn()
        {
            Color backColorSelect = Color.FromArgb(0, 105, 51);
            Color backColorNotSelect = Color.FromArgb(0, 64, 0);
            Color foreColorSelect = Color.Cyan;
            Color foreColorNotSelect = Color.Silver;
            Color backCororLabel = Color.FromArgb(0, 105, 51);

            if (this.form.ismobile_mode)
            {
                _tabPageManager.ChangeTabPageVisible(1, true);
            }

            // 形態区分、計量区分初期セット
            if (this.form.selectDenshuKbnCd == DENSHU_KBN.UKEIRE)
            {
                this.headerForm.KIHON_KEIRYOU.Text = "1";
                if (!this.dto.sysInfoEntity.KEIRYOU_UKEIRE_KEITAI_KBN_CD.IsNull)
                {
                    this.form.KEITAI_KBN_CD.Text = this.dto.sysInfoEntity.KEIRYOU_UKEIRE_KEITAI_KBN_CD.ToString();
                }
                else
                {
                    this.form.KEITAI_KBN_CD.Text = string.Empty;
                }
                if (!this.dto.sysInfoEntity.KEIRYOU_UKEIRE_KEIRYOU_KBN_CD.IsNull)
                {
                    this.form.KEIRYOU_KBN_CD.Text = this.dto.sysInfoEntity.KEIRYOU_UKEIRE_KEIRYOU_KBN_CD.ToString();
                }
                else
                {
                    this.form.KEIRYOU_KBN_CD.Text = string.Empty;
                }
                dto1.KeyName = "GYOUSHAKBN_UKEIRE";
                dto4.KeyName = "M_GYOUSHA.GYOUSHAKBN_UKEIRE";
                if (this.form.ismobile_mode)
                {
                    _tabPageManager.ChangeTabPageVisible(1, true);
                }
            }
            else
            {
                this.headerForm.KIHON_KEIRYOU.Text = "2";
                if (!this.dto.sysInfoEntity.KEIRYOU_SHUKKA_KEITAI_KBN_CD.IsNull)
                {
                    this.form.KEITAI_KBN_CD.Text = this.dto.sysInfoEntity.KEIRYOU_SHUKKA_KEITAI_KBN_CD.ToString();
                }
                else
                {
                    this.form.KEITAI_KBN_CD.Text = string.Empty;
                }
                if (!this.dto.sysInfoEntity.KEIRYOU_SHUKKA_KEIRYOU_KBN_CD.IsNull)
                {
                    this.form.KEIRYOU_KBN_CD.Text = this.dto.sysInfoEntity.KEIRYOU_SHUKKA_KEIRYOU_KBN_CD.ToString();
                }
                else
                {
                    this.form.KEIRYOU_KBN_CD.Text = string.Empty;
                }
                dto1.KeyName = "GYOUSHAKBN_SHUKKA";
                dto4.KeyName = "M_GYOUSHA.GYOUSHAKBN_SHUKKA";
                if (this.form.ismobile_mode)
                {
                    _tabPageManager.ChangeTabPageVisible(1, false);
                }
            }
            this.form.KEITAI_KBN_CD.PopupDataSource = this.CreateKeitaiKbnPopupDataSource();
            this.form.KEITAI_KBN_CD.popupWindowSetting = new Collection<JoinMethodDto>();

            M_KEITAI_KBN kakuteiKbn = null;
            if (!this.dto.sysInfoEntity.KEIRYOU_UKEIRE_KEITAI_KBN_CD.IsNull)
            {
                kakuteiKbn = this.accessor.GetkeitaiKbn(this.dto.sysInfoEntity.KEIRYOU_UKEIRE_KEITAI_KBN_CD.Value);
            }
            if (kakuteiKbn != null)
            {
                this.form.KEITAI_KBN_NAME_RYAKU.Text = kakuteiKbn.KEITAI_KBN_NAME_RYAKU;
            }
            else
            {
                this.form.KEITAI_KBN_NAME_RYAKU.Text = string.Empty;
            }
            this.form.GYOUSHA_CD.PopupSearchSendParams.Clear();
            this.form.GYOUSHA_CD.PopupSearchSendParams.Add(dto1);
            this.form.GYOUSHA_CD.PopupSearchSendParams.Add(dto2);
            this.form.GENBA_CD.PopupSearchSendParams.Clear();
            this.form.GENBA_CD.PopupSearchSendParams.Add(dto2);
            this.form.GENBA_CD.PopupSearchSendParams.Add(dto3);
            this.form.GENBA_CD.PopupSearchSendParams.Add(dto4);

            this.form.GHOUSYA_SEARCH_BUTTON.PopupSearchSendParams.Clear();
            this.form.GHOUSYA_SEARCH_BUTTON.PopupSearchSendParams.Add(dto1);
            this.form.GHOUSYA_SEARCH_BUTTON.PopupSearchSendParams.Add(dto2);
            this.form.GENBA_SEARCH_BUTTON.PopupSearchSendParams.Clear();
            this.form.GENBA_SEARCH_BUTTON.PopupSearchSendParams.Add(dto2);
            this.form.GENBA_SEARCH_BUTTON.PopupSearchSendParams.Add(dto3);
            this.form.GENBA_SEARCH_BUTTON.PopupSearchSendParams.Add(dto4);

            this.form.UNPAN_GYOUSHA_CD.PopupSearchSendParams.Clear();
            this.form.UNPAN_GYOUSHA_CD.PopupSearchSendParams.Add(dto1);
            this.form.UNPAN_GYOUSHA_CD.PopupSearchSendParams.Add(dto2);
            this.form.UNPAN_GYOUSHA_CD.PopupSearchSendParams.Add(dto5);

            this.form.UNPAN_GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams.Clear();
            this.form.UNPAN_GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams.Add(dto1);
            this.form.UNPAN_GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams.Add(dto2);
            this.form.UNPAN_GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams.Add(dto5);

            foreach (var dto in this.form.SHARYOU_CD.PopupSearchSendParams)
            {
                if (dto.KeyName == "key005")
                {
                    if (this.form.selectDenshuKbnCd == DENSHU_KBN.UKEIRE)
                    {
                        dto.Value = "1";
                    }
                    else
                    {
                        dto.Value = "2";
                    }
                    break;
                }
            }
            foreach (var dto in this.form.SHARYOU_SEARCH_BUTTON.PopupSearchSendParams)
            {
                if (dto.KeyName == "key005")
                {
                    if (this.form.selectDenshuKbnCd == DENSHU_KBN.UKEIRE)
                    {
                        dto.Value = "1";
                    }
                    else
                    {
                        dto.Value = "2";
                    }
                    break;
                }
            }


            // 背景色切替
            if (this.form.selectDenshuKbnCd == DENSHU_KBN.UKEIRE)
            {
                this.headerForm.UKEIRE_LABEL.BackColor = backColorSelect;
                this.headerForm.UKEIRE_LABEL.ForeColor = foreColorSelect;
                this.headerForm.SHUKKA_LABEL.BackColor = backColorNotSelect;
                this.headerForm.SHUKKA_LABEL.ForeColor = foreColorNotSelect;
                backCororLabel = Color.FromArgb(0, 105, 51);
                this.SetHeader("受入", backCororLabel);
                this.SetBody(backCororLabel);
                var newTemplate = this.form.gcMultiRow1.Template;
                var cell = newTemplate.Row.Cells["HINMEI_CD"] as r_framework.CustomControl.GcCustomAlphaNumTextBoxCell;
                cell.popupWindowSetting = new Collection<JoinMethodDto>();
                dto6.SearchCondition = new Collection<SearchConditionsDto>();
                dto6.SearchCondition.Add(dto7);
                dto6.SearchCondition.Add(dto9);
                cell.popupWindowSetting.Add(dto6);

                if (this.form.ismobile_mode)
                {
                    var newTemplate2 = this.form.gcMultiRow2.Template;
                    var cell2 = newTemplate2.Row.Cells["HINMEI_CD"] as r_framework.CustomControl.GcCustomAlphaNumTextBoxCell;
                    cell2.popupWindowSetting = new Collection<JoinMethodDto>();
                    dto6.SearchCondition = new Collection<SearchConditionsDto>();
                    dto6.SearchCondition.Add(dto7);
                    dto6.SearchCondition.Add(dto9);
                    cell2.popupWindowSetting.Add(dto6);
                }

                this.form.NIOROSHI_GYOUSHA_CD.Enabled = true;
                this.form.NIOROSHI_GENBA_CD.Enabled = true;
                this.form.NIZUMI_GYOUSHA_CD.Text = string.Empty;
                this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
                this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                this.form.NIZUMI_GYOUSHA_CD.Enabled = false;
                this.form.NIZUMI_GENBA_CD.Enabled = false;
                this.footerForm.bt_func8.Enabled = true;
            }
            else
            {
                this.headerForm.UKEIRE_LABEL.BackColor = backColorNotSelect;
                this.headerForm.UKEIRE_LABEL.ForeColor = foreColorNotSelect;
                this.headerForm.SHUKKA_LABEL.BackColor = backColorSelect;
                this.headerForm.SHUKKA_LABEL.ForeColor = foreColorSelect;
                backCororLabel = Color.FromArgb(0, 51, 160);
                this.SetHeader("出荷", backCororLabel);
                this.SetBody(backCororLabel);
                var newTemplate = this.form.gcMultiRow1.Template;
                var cell = newTemplate.Row.Cells["HINMEI_CD"] as r_framework.CustomControl.GcCustomAlphaNumTextBoxCell;
                cell.popupWindowSetting = new Collection<JoinMethodDto>();
                dto6.SearchCondition = new Collection<SearchConditionsDto>();
                dto6.SearchCondition.Add(dto8);
                dto6.SearchCondition.Add(dto9);
                cell.popupWindowSetting.Add(dto6);
                this.form.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
                this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                this.form.NIOROSHI_GYOUSHA_CD.Enabled = false;
                this.form.NIOROSHI_GENBA_CD.Enabled = false;
                this.form.NIZUMI_GYOUSHA_CD.Enabled = true;
                this.form.NIZUMI_GENBA_CD.Enabled = true;
                this.footerForm.bt_func8.Enabled = false;
            }
        }

        /// <summary>
        /// ヘッダーフォームの設定
        /// タイトルラベル、ラベルの背景色を変更
        /// </summary>
        public void SetHeader(string strTitleName, Color BackColor)
        {
            LogUtility.DebugMethodStart(strTitleName, BackColor);
            this.headerForm.lb_title.BackColor = BackColor;
            this.headerForm.KYOTEN_LABEL.BackColor = BackColor;
            this.headerForm.KEIZOKU_NYUURYOKU_LABEL.BackColor = BackColor;
            this.headerForm.PRINT_KBN_LABLE.BackColor = BackColor;
            this.headerForm.lb_LastUpdate.BackColor = BackColor;
            this.headerForm.lb_Create.BackColor = BackColor;
            LogUtility.DebugMethodEnd(strTitleName, BackColor);
        }

        /// <summary>
        /// ラベルの背景色を変更
        /// </summary>
        /// <param name="BackColor">設定する色</param>
        public void SetBody(Color BackColor)
        {
            LogUtility.DebugMethodStart(BackColor);
            //伝票ヘッダー
            this.form.ENTRY_NUMBER_LABEL.BackColor = BackColor;
            this.form.RENBAN_LABEL.BackColor = BackColor;
            this.form.DENPYOU_DATE_LABEL.BackColor = BackColor;
            this.form.KEIRYOU_KBN_LABEL.BackColor = BackColor;
            this.form.NYUURYOKU_TANTOUSHA_LABEL.BackColor = BackColor;
            this.form.GYOUSHA_LABEL.BackColor = BackColor;
            this.form.GENBA_LABEL.BackColor = BackColor;
            this.form.UNPAN_GYOUSHA_LABEL.BackColor = BackColor;
            this.form.SHARYOU_LABEL.BackColor = BackColor;
            this.form.SHASHU_LABEL.BackColor = BackColor;
            this.form.UNTENSHA_LABEL.BackColor = BackColor;
            this.form.DENPYOU_BIKOU_LABEL.BackColor = BackColor;
            this.form.TAIRYUU_BIKOU_LABEL.BackColor = BackColor;
            this.form.STACK_JYUURYOU_LABEL.BackColor = BackColor;
            this.form.EMPTY_JYUURYOU_LABLE.BackColor = BackColor;
            //マニフェストタブ
            this.form.MANIFEST_HAIKI_KBN_LABEL.BackColor = BackColor;
            this.form.HST_GENBA_LABEL.BackColor = BackColor;
            this.form.HST_GYOUSHA_LABEL.BackColor = BackColor;
            this.form.SBN_GENBA_LABEL.BackColor = BackColor;
            this.form.SBN_GYOUSHA_LABEL.BackColor = BackColor;
            this.form.LAST_SBN_GENBA_LABEL.BackColor = BackColor;
            this.form.LAST_SBN_GYOUSHA_LABEL.BackColor = BackColor;
            //その他タブ
            this.form.EIGYOU_TANTOUSHA_LABEL.BackColor = BackColor;
            this.form.NIZUMI_GYOUSHA_LABEL.BackColor = BackColor;
            this.form.NIZUMI_GENBA_LABEL.BackColor = BackColor;
            this.form.NIOROSHI_GYOUSHA_LABEL.BackColor = BackColor;
            this.form.NIOROSHI_GENBA_LABEL.BackColor = BackColor;
            this.form.DAIKAN_KBN_LABEL.BackColor = BackColor;
            this.form.KEITAI_KBN_LABEL.BackColor = BackColor;
            this.form.MANIFEST_SHURUI_LABEL.BackColor = BackColor;
            this.form.MANIFEST_TEHAI_LABEL.BackColor = BackColor;
            //伝票発行タブ
            this.form.TORIHIKISAKI_LABEL.BackColor = BackColor;
            this.form.SEIKYUU_TORIHIKI_LABEL.BackColor = BackColor;
            this.form.SEIKYUU_TORIHIKI_KBN_LABEL.BackColor = BackColor;
            this.form.SEIKYUU_ZEI_KEISAN_KBN_LABEL.BackColor = BackColor;
            this.form.SEIKYUU_ZEI_KBN_LABEL.BackColor = BackColor;
            this.form.SHIHARAI_TORIHIKI_LABEL.BackColor = BackColor;
            this.form.SHIHARAI_TORIHIKI_KBN_LABEL.BackColor = BackColor;
            this.form.SHIHARAI_ZEI_KEISAN_KBN_LABEL.BackColor = BackColor;
            this.form.SHIHARAI_ZEI_KBN_LABEL.BackColor = BackColor;
            this.form.CASHEIR_RENDOU_KBN_LABEL.BackColor = BackColor;
            this.form.RECEIPT_KBN_LABEL.BackColor = BackColor;
            this.form.RECEIPT_KEISHOU_LABEL.BackColor = BackColor;
            this.form.RECEIPT_TADASHIGAKI_LABEL.BackColor = BackColor;
            //計量明細のラベル背景色
            this.ChangeBackColorGC(new string[] 
                { "cornerHeaderCell1", "columnHeaderCell5", "columnHeaderCell15", "columnHeaderCell17",
                  "columnHeaderCell23", "columnHeaderCell4", "columnHeaderCell7", "columnHeaderCell11",
                  "columnHeaderCell12", "columnHeaderCell24", "columnHeaderCell14", "columnHeaderCell19",
                  "columnHeaderCell16", "columnHeaderCell6", "columnHeaderCell18", "columnHeaderCell20",
                  "columnHeaderCell10", "columnHeaderCell13", "columnHeaderCell2"
                },
                BackColor);
            this.form.gcMultiRow1.ResumeLayout();
            LogUtility.DebugMethodEnd(BackColor);
        }

        /// <summary>
        /// 明細のヘッダー背景色変更
        /// </summary>
        /// <param name="headerNames">背景色を変更にするカラム名一覧</param>
        /// <param name="backColor">背景色</param>
        private void ChangeBackColorGC(string[] headerNames, Color backColor)
        {
            DataTable dt = new DataTable();
            foreach (var col in this.form.gcMultiRow1.Columns)
            {
                dt.Columns.Add(col.Name);
            }
            DataRow dr = dt.NewRow();
            foreach (var row in this.form.gcMultiRow1.Rows)
            {
                dr = dt.NewRow();
                foreach (var col in this.form.gcMultiRow1.Columns)
                {
                    dr[col.Name] = Convert.ToString(row.Cells[col.Name].Value);
                }
                dt.Rows.Add(dr);
            }
            this.form.gcMultiRow1.SuspendLayout();

            var newTemplate = this.form.gcMultiRow1.Template;

            if (headerNames != null && 0 < headerNames.Length)
            {
                var obj1 = controlUtil.FindControl(newTemplate.ColumnHeaders[0].Cells.ToArray(), headerNames);
                foreach (var o in obj1)
                {
                    PropertyUtility.SetBackColor(o, backColor);
                }
            }

            this.form.gcMultiRow1.Template = newTemplate;

            this.form.gcMultiRow1.ResumeLayout();

            for (int i = 0; i < this.form.gcMultiRow1.Rows.Count; i++)
            {
                dr = dt.NewRow();
                for (int j = 0; j < this.form.gcMultiRow1.Columns.Count; j++)
                {
                    this.form.gcMultiRow1[i, j].Value = dt.Rows[i][j];
                }
                dt.Rows.Add(dr);
            }
        }

        /// <summary>
        /// タブページ切替
        /// </summary>
        internal void ChangeTabPage()
        {
            try
            {
                LogUtility.DebugMethodStart();
                string ukeireShukkaKbn = this.headerForm.KIHON_KEIRYOU.Text == "1" ? "2" : "1";
                // 基本計量セット
                this.headerForm.KIHON_KEIRYOU.Text = ukeireShukkaKbn;

                //伝種区分セット
                if (ukeireShukkaKbn == "1")
                {
                    this.form.selectDenshuKbnCd = DENSHU_KBN.UKEIRE;
                }
                else
                {
                    this.form.selectDenshuKbnCd = DENSHU_KBN.SHUKKA;
                }
                this.form.ChangeNewWindow(null, new EventArgs());
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeTabPage", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 品名検索処理
        /// </summary>
        public void SearchHinmei()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.ismobile_mode && this.form.selectDenshuKbnCd == DENSHU_KBN.UKEIRE && this.form.DETAIL_TAB.SelectedIndex == 1)
                {
                    //明細行が無い場合は処理しない
                    if (this.form.gcMultiRow2.CurrentRow == null)
                    {
                        return;
                    }
                    this.form.gcMultiRow2.Focus();
                    var cell = this.form.gcMultiRow2.CurrentRow.Cells[CELL_NAME_HINMEI_CD];
                    cell.Selected = true;
                    this.form.gcMultiRow2.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(cell.RowIndex, cell.CellIndex);
                    this.form.gcMultiRow2.CurrentCell = this.form.gcMultiRow2.CurrentRow.Cells[CELL_NAME_HINMEI_CD];
                    CustomControlExtLogic.PopUp((ICustomControl)this.form.gcMultiRow2.CurrentRow.Cells[CELL_NAME_HINMEI_CD]);
                }
                else
                {
                    //明細行が無い場合は処理しない
                    if (this.form.gcMultiRow1.CurrentRow == null)
                    {
                        return;
                    }
                    this.form.DETAIL_TAB.SelectedIndex = 0;
                    this.form.gcMultiRow1.Focus();
                    var cell = this.form.gcMultiRow1.CurrentRow.Cells[CELL_NAME_HINMEI_CD];
                    cell.Selected = true;
                    this.form.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(cell.RowIndex, cell.CellIndex);
                    this.form.gcMultiRow1.CurrentCell = this.form.gcMultiRow1.CurrentRow.Cells[CELL_NAME_HINMEI_CD];
                    CustomControlExtLogic.PopUp((ICustomControl)this.form.gcMultiRow1.CurrentRow.Cells[CELL_NAME_HINMEI_CD]);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchHinmei", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return;
        }

        #endregion

        #region 時間入力チェック処理
        /// <summary>
        /// 時間入力チェック処理
        /// </summary>
        /// <returns>bool(OK:true NG:false)</returns>
        internal bool IsTimeChkOK(CustomAlphaNumTextBox ctrl)
        {
            bool result = true;
            try
            {
                LogUtility.DebugMethodStart(ctrl);
                string value = ctrl.Text;
                if (ctrl == null || string.IsNullOrEmpty(value))
                {
                    return result;
                }
                // 未入力の場合、処理中止
                if (string.IsNullOrEmpty(value))
                {
                    return result;
                }
                value = value.Replace(":", "");
                var reg = @"^(20|21|22|23|[0-1]\d)[0-5]\d$";
                result = Regex.IsMatch(value, reg);
                if (!result)
                {
                    msgLogic.MessageBoxShow("E084", value);
                }
                else
                {
                    ctrl.Text = value.Substring(0, 2) + ":" + value.Substring(2, 2);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
        }

        /// <summary>
        /// 時間入力チェック処理
        /// </summary>
        /// <returns>bool(OK:true NG:false)</returns>
        internal bool IsTimeChkOK(Cell ctrl)
        {
            bool result = true;
            try
            {
                LogUtility.DebugMethodStart(ctrl);
                string value = Convert.ToString(ctrl.Value);
                if (ctrl == null || string.IsNullOrEmpty(value))
                {
                    return result;
                }
                // 未入力の場合、処理中止
                if (string.IsNullOrEmpty(value))
                {
                    return result;
                }
                value = value.Replace(":", "");
                var reg = @"^(20|21|22|23|[0-1]\d)[0-5]\d$";
                result = Regex.IsMatch(value, reg);
                if (!result)
                {
                    msgLogic.MessageBoxShow("E084", value);
                }
                else
                {
                    ctrl.Value = value.Substring(0, 2) + ":" + value.Substring(2, 2);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
        }
        #endregion
        public void denpyouHakkou()
        {
            this.form.denpyouHakouPopUpDTO = new ParameterDTOClass();
            M_TORIHIKISAKI_SEIKYUU dataTorihikiSeikyu = new M_TORIHIKISAKI_SEIKYUU();
            this.TorihikisakiSeikyuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
            decimal r_KAZEI_KINGAKU = 0;
            decimal r_KAZEI_SHOUHIZEI = 0;
            decimal r_KAZEI_KINGAKU_SUM = 0;
            decimal r_HIKAZEI_KINGAKU = 0;
            this.form.denpyouHakouPopUpDTO.R_KAZEI_KINGAKU = "0";
            this.form.denpyouHakouPopUpDTO.R_KAZEI_SHOUHIZEI = "0";
            this.form.denpyouHakouPopUpDTO.R_HIKAZEI_KINGAKU = "0";
            this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Rorihiki = "0";
            int iSeikyuHasuCd = 0;

            // 請求分用の税率取得
            string seikyuShouhizeiRate = this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE.ToString();
            
            // 取引先情報取得
            dataTorihikiSeikyu = TorihikisakiSeikyuDao.GetDataByCd(this.form.TORIHIKISAKI_CD.Text.ToString());

            // 端数CD取得
            if (!dataTorihikiSeikyu.TAX_HASUU_CD.Equals(System.Data.SqlTypes.SqlInt16.Null))
            {
                iSeikyuHasuCd = dataTorihikiSeikyu.TAX_HASUU_CD.Value;
            }
 
            if (this.form.RECEIPT_KBN_CD.Text.ToString() == "1")
            {
                M_HINMEI[] hinmeis = null;
                string zeiKbn = string.Empty;       // 税区分
                Boolean hinmeiFlg = false;          // 品名税フラグ

                foreach (var row in this.form.gcMultiRow1.Rows)
                {
                    if (row.Cells["HINMEI_CD"].Value != null)
                    {
                        // 品名税取得
                        hinmeis = this.accessor.GetAllValidHinmeiData(row.Cells["HINMEI_CD"].Value.ToString());
                        M_HINMEI hinmei = hinmeis[0];
 
                        //TENPYO_KBN_1:売上
                        if (TENPYO_KBN_1.Equals(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString()))
                        {
                            if (hinmei.ZEI_KBN_CD > 0)
                            {
                                zeiKbn = hinmeis[0].ZEI_KBN_CD.ToString();   //品名税区分
                                hinmeiFlg = true;
                            }
                            else
                            {
                                zeiKbn = this.form.SEIKYUU_ZEI_KBN_CD.Text;  //伝票税区分
                            }

                            //外税
                            if (ZEI_KBN_1.Equals(zeiKbn))
                            {
                                if ((!hinmeiFlg) && (ZEIKEISAN_KBN_1.Equals(this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text)))
                                {
                                    //品名税区分なし、伝票毎外税
                                    //★F
                                    r_KAZEI_KINGAKU_SUM = r_KAZEI_KINGAKU_SUM + Convert.ToDecimal(row.Cells["KINGAKU"].Value.ToString());
                                }
                                else if ((!hinmeiFlg) && (ZEIKEISAN_KBN_2.Equals(this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text)))
                                {
                                    //品名税区分なし、請求毎外税
                                    //O
                                    r_KAZEI_KINGAKU = r_KAZEI_KINGAKU + Convert.ToDecimal(row.Cells["KINGAKU"].Value.ToString());
                                }
                                else
                                {
                                    //品名外税or明細毎外税
                                    //A/J
                                    r_KAZEI_KINGAKU = r_KAZEI_KINGAKU + Convert.ToDecimal(row.Cells["KINGAKU"].Value.ToString());
                                    //B/K
                                    r_KAZEI_SHOUHIZEI = r_KAZEI_SHOUHIZEI +
                                                    Convert.ToDecimal(SetComma(CalcZeiKbnShohiZei(zeiKbn,
                                                    Convert.ToDecimal(row.Cells["KINGAKU"].Value.ToString()),
                                                    Convert.ToDecimal(seikyuShouhizeiRate),
                                                    iSeikyuHasuCd).ToString()));
                                }
                            }

                            //内税
                            else if (ZEI_KBN_2.Equals(zeiKbn))
                            {
                                //★C/L
                                r_KAZEI_KINGAKU_SUM = r_KAZEI_KINGAKU_SUM + Convert.ToDecimal(row.Cells["KINGAKU"].Value.ToString()) -
                                                Convert.ToDecimal(SetComma(CalcZeiKbnShohiZei(zeiKbn,
                                                Convert.ToDecimal(row.Cells["KINGAKU"].Value.ToString()),
                                                Convert.ToDecimal(seikyuShouhizeiRate),
                                                iSeikyuHasuCd).ToString()));
                            }

                            //非課税
                            else if (ZEI_KBN_3.Equals(zeiKbn))
                            {
                                //E/I/N/R
                                r_HIKAZEI_KINGAKU = r_HIKAZEI_KINGAKU + Convert.ToDecimal(row.Cells["KINGAKU"].Value.ToString());
                            }

                        }
                    }
                }

                //	領収書_売上)課税金額 //A + J + (★C + F + L)
                this.form.denpyouHakouPopUpDTO.R_KAZEI_KINGAKU = SetComma(Convert.ToString(r_KAZEI_KINGAKU + r_KAZEI_KINGAKU_SUM));

                //	領収書_売上)課税消費税
                if (ZEIKEISAN_KBN_2.Equals(this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text))
                {
                    //税計算区分：請求毎 【ゼロ】固定
                    this.form.denpyouHakouPopUpDTO.R_KAZEI_SHOUHIZEI = "0";
                }
                else
                {
                    //税計算区分：請求毎以外 (★C + F + L) * 税率 + B + K
                    this.form.denpyouHakouPopUpDTO.R_KAZEI_SHOUHIZEI = SetComma(Convert.ToString(
                                                    Convert.ToDecimal(SetComma(CalcZeiKbnShohiZei("1",
                                                    Convert.ToDecimal(r_KAZEI_KINGAKU_SUM),
                                                    Convert.ToDecimal(seikyuShouhizeiRate),
                                                    iSeikyuHasuCd).ToString()))
                                                    + r_KAZEI_SHOUHIZEI));
                }
                //	領収書_売上)非課税金額 //E+I+N+R
                this.form.denpyouHakouPopUpDTO.R_HIKAZEI_KINGAKU = SetComma(Convert.ToString(r_HIKAZEI_KINGAKU));

                //  領収書_売上)合計
                this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Rorihiki = SetComma(Convert.ToString(
                                                                        Convert.ToDecimal(this.form.denpyouHakouPopUpDTO.R_HIKAZEI_KINGAKU) +
                                                                        Convert.ToDecimal(this.form.denpyouHakouPopUpDTO.R_KAZEI_SHOUHIZEI) +
                                                                        Convert.ToDecimal(this.form.denpyouHakouPopUpDTO.R_KAZEI_KINGAKU)
                                                                        ));
            }
        }
  
        public bool ShowSharyou()
        {
            bool catchErr = true;
            try
            {
                // 修正モードの権限チェック
                FormManager.OpenFormWithAuth("M207", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowSharyou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(catchErr);
            }
            return catchErr;
        }

        public bool ShowManifest()
        {
            bool catchErr = true;
            try
            {
                if (string.IsNullOrEmpty(this.form.MANIFEST_HAIKI_KBN_CD.Text))
                {
                    msgLogic.MessageBoxShow("E001", "廃棄物区分");
                    if (this.form.ismobile_mode && this.form.selectDenshuKbnCd == DENSHU_KBN.UKEIRE)
                    {
                        this.form.DETAIL_TAB.SelectedIndex = 2;
                    }
                    else
                    {
                        this.form.DETAIL_TAB.SelectedIndex = 1;
                    }
                    this.form.MANIFEST_HAIKI_KBN_CD.Focus();
                    return catchErr;
                }

                if (!string.IsNullOrEmpty(this.form.MANIFEST_SHURUI_CD.Text) && this.form.MANIFEST_SHURUI_CD.Text != "1")
                {
                    msgLogic.MessageBoxShow("E175");
                    if (this.form.ismobile_mode && this.form.selectDenshuKbnCd == DENSHU_KBN.UKEIRE)
                    {
                        this.form.DETAIL_TAB.SelectedIndex = 3;
                    }
                    else
                    {
                        this.form.DETAIL_TAB.SelectedIndex = 2;
                    }
                    this.form.MANIFEST_SHURUI_CD.Focus();
                    return catchErr;
                }
                string formId = string.Empty;
                switch (this.form.MANIFEST_HAIKI_KBN_CD.Text)
                {
                    case "1":
                        formId = "G119";
                        break;
                    case "2":
                        formId = "G121";
                        break;
                    case "3":
                        formId = "G120";
                        break;
                }

                Int16 maniFlag = 1;
                if (this.form.selectDenshuKbnCd == DENSHU_KBN.UKEIRE)
                {
                    maniFlag = 1;
                }
                else if (this.form.selectDenshuKbnCd == DENSHU_KBN.SHUKKA)
                {
                    maniFlag = 2;
                }
                // 新規モードの権限チェック
                if (Manager.CheckAuthority(formId, WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                {
                    if (!this.form.RegistDataProcess(null, null, true) || this.form.RegistErrorFlag || !this.isRegistered)
                    {
                        if (this.form.RegistErrorFlag)
                        {
                            this.form.SetControlFocus();
                        }
                        return catchErr;
                    }
                    FormManager.OpenFormWithAuth(formId, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, "140", Convert.ToString(this.form.KeiryouSysId), null, 1, null, null, null, maniFlag);
                    if (this.isClose)
                    {
                        this.isClose = false;
                        this.form.CloseTopForm();
                    }
                }
                else
                {
                    // 新規モードの権限なしのアラームを上げる
                    this.msgLogic.MessageBoxShow("E158", "新規");
                    return catchErr;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowManifest", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(catchErr);
            }
            return catchErr;
        }

        public bool GetTairyuuData()
        {
            bool catchErr = true;
            try
            {
                if (string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
                {
                    msgLogic.MessageBoxShow("E001", "拠点CD");
                    this.headerForm.KYOTEN_CD.Focus();
                    return catchErr;
                }
                TairyuuDTOClass dto = new TairyuuDTOClass();
                dto.kyotenCd = Int16.Parse(this.headerForm.KYOTEN_CD.Text);
                DataTable dt = this.accessor.GetTairyuuData(dto);
                this.form.tairyuuIchiran.DataSource = dt;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetTairyuuData", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTairyuuData", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(catchErr);
            }
            return catchErr;
        }

        public Int64 GetTairyuuNumberBySharyou()
        {
            Int64 number = -1;
            try
            {
                TairyuuDTOClass dto = new TairyuuDTOClass();
                dto.kyotenCd = Int16.Parse(this.headerForm.KYOTEN_CD.Text);
                dto.upnGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
                dto.sharyouCd = this.form.SHARYOU_CD.Text;
                DataTable dt = this.accessor.GetTairyuuData(dto);
                Int64 denpyou = -1;
                if (!string.IsNullOrEmpty(this.form.ENTRY_NUMBER.Text))
                {
                    denpyou = Convert.ToInt64(this.form.ENTRY_NUMBER.Text);
                }
                foreach (DataRow row in dt.Rows)
                {
                    number = Convert.ToInt64(row["DENPYOU_NUMBER"]);
                    if (denpyou != number)
                    {
                        break;
                    }
                    else
                    {
                        number = -1;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetTairyuuNumberBySharyou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                number = -1;
                return number;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTairyuuNumberBySharyou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                number = -1;
                return number;
            }
            finally
            {
                LogUtility.DebugMethodEnd(number);
            }
            return number;
        }

        public bool ShowDenpyou(Int64 number = -1)
        {
            bool catchErr = true;
            try
            {
                if (number == -1)
                {
                    var row = this.form.tairyuuIchiran.CurrentRow;
                    if (row == null)
                    {
                        return catchErr;
                    }
                    number = Convert.ToInt64(row.Cells["DENPYOU_NUMBER"].Value);
                }

                var tmpType = this.form.WindowType;
                // 入力されている受入番号の後の受入番号が取得できた場合
                this.form.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                this.form.KeiryouNumber = number;
                bool retDate = this.GetAllEntityData(out catchErr);
                if (!catchErr)
                {
                    return catchErr;
                }
                if (!retDate)
                {
                    // エラー発生時には値をクリアする
                    this.form.ChangeNewWindow(null, new EventArgs());
                    return catchErr;
                }

                catchErr = true;
                bool retExist = this.HasAuthorityTairyuu(this.form.KeiryouNumber, this.form.SEQ, out catchErr);
                if (!catchErr)
                {
                    return catchErr;
                }
                // 滞留登録された受入伝票用権限チェック
                if (!retExist)
                {
                    // 滞留登録された受入伝票かつ新規権限がない場合はアラートを表示して処理中断
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E158", "新規");
                    this.form.WindowType = tmpType;
                    this.form.HeaderFormInit();
                    return catchErr;
                }

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("G672", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // 修正権限がない場合
                    if (r_framework.Authority.Manager.CheckAuthority("G672", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        // 修正権限は無いが参照権限がある場合は参照モードで起動
                        this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        this.form.HeaderFormInit();
                    }
                    else
                    {
                        // どちらも無い場合はアラートを表示して処理中断
                        MessageBoxShowLogic msg = new MessageBoxShowLogic();
                        msg.MessageBoxShow("E158", "修正");
                        this.form.WindowType = tmpType;
                        this.form.HeaderFormInit();
                        return catchErr;
                    }
                }

                if (!this.WindowInit())
                {
                    return false;
                }

                if (!this.ButtonInit())
                {
                    return false;
                }

                // スクロールバーが下がる場合があるため、
                // 強制的にバーを先頭にする
                this.form.AutoScrollPosition = new Point(0, 0);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ShowDenpyou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowDenpyou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(catchErr);
            }
            return catchErr;
        }

        #region マニフェストタブ

        #region 排出業者
        public bool HST_GYOUSHA_CD_Validated()
        {
            bool catchErr = true;
            try
            {
                if (this.beforeCd == this.form.HST_GYOUSHA_CD.Text && !this.isInputError)
                {
                    return catchErr;
                }
                this.form.HST_GYOUSHA_NAME.Text = string.Empty;
                this.form.HST_GENBA_CD.Text = string.Empty;
                this.form.HST_GENBA_NAME.Text = string.Empty;
                if (string.IsNullOrEmpty(this.form.HST_GYOUSHA_CD.Text))
                {
                    return catchErr;
                }

                var gyousha = this.accessor.GetGyousha(this.form.HST_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, true, false, false, out catchErr);
                if (!catchErr)
                {
                    return catchErr;
                }
                if (gyousha == null)
                {
                    this.form.HST_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.msgLogic.MessageBoxShow("E020", "業者");
                    this.isInputError = true;
                    this.form.HST_GYOUSHA_CD.Focus();
                }
                else
                {
                    this.form.HST_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("HST_GYOUSHA_CD_Validated", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HST_GYOUSHA_CD_Validated", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(catchErr);
            }
            return catchErr;
        }
        #endregion

        #region 排出現場
        public bool HST_GENBA_CD_Validated()
        {
            bool catchErr = true;
            try
            {
                if (this.beforeCd == this.form.HST_GENBA_CD.Text && !this.isInputError)
                {
                    return catchErr;
                }
                this.form.HST_GENBA_NAME.Text = string.Empty;
                if (string.IsNullOrEmpty(this.form.HST_GENBA_CD.Text))
                {
                    return catchErr;
                }
                if (string.IsNullOrEmpty(this.form.HST_GYOUSHA_CD.Text))
                {
                    this.form.HST_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.msgLogic.MessageBoxShow("E051", "排出業者");
                    this.isInputError = true;
                    this.form.HST_GENBA_CD.Focus();
                    return catchErr;
                }

                var genba = this.accessor.GetGenba(this.form.HST_GYOUSHA_CD.Text, this.form.HST_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, true, false, false, out catchErr);
                if (!catchErr)
                {
                    return catchErr;
                }
                if (genba == null)
                {
                    this.form.HST_GENBA_CD.IsInputErrorOccured = true;
                    this.msgLogic.MessageBoxShow("E020", "現場");
                    this.isInputError = true;
                    this.form.HST_GENBA_CD.Focus();
                }
                else
                {
                    this.form.HST_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("HST_GENBA_CD_Validated", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HST_GENBA_CD_Validated", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(catchErr);
            }
            return catchErr;
        }
        #endregion

        #region 処分業者
        public bool SBN_GYOUSHA_CD_Validated()
        {
            bool catchErr = true;
            try
            {
                if (this.beforeCd == this.form.SBN_GYOUSHA_CD.Text && !this.isInputError)
                {
                    return catchErr;
                }
                this.form.SBN_GYOUSHA_NAME.Text = string.Empty;
                this.form.SBN_GENBA_CD.Text = string.Empty;
                this.form.SBN_GENBA_NAME.Text = string.Empty;
                if (string.IsNullOrEmpty(this.form.SBN_GYOUSHA_CD.Text))
                {
                    return catchErr;
                }

                var gyousha = this.accessor.GetGyousha(this.form.SBN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, false, true, false, out catchErr);
                if (!catchErr)
                {
                    return catchErr;
                }
                if (gyousha == null)
                {
                    this.form.SBN_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.msgLogic.MessageBoxShow("E020", "業者");
                    this.isInputError = true;
                    this.form.SBN_GYOUSHA_CD.Focus();
                }
                else
                {
                    this.form.SBN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SBN_GYOUSHA_CD_Validated", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SBN_GYOUSHA_CD_Validated", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(catchErr);
            }
            return catchErr;
        }
        #endregion

        #region 処分現場
        public bool SBN_GENBA_CD_Validated()
        {
            bool catchErr = true;
            try
            {
                if (this.beforeCd == this.form.SBN_GENBA_CD.Text && !this.isInputError)
                {
                    return catchErr;
                }
                this.form.SBN_GENBA_NAME.Text = string.Empty;
                if (string.IsNullOrEmpty(this.form.SBN_GENBA_CD.Text))
                {
                    return catchErr;
                }

                if (string.IsNullOrEmpty(this.form.SBN_GYOUSHA_CD.Text))
                {
                    this.form.SBN_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.msgLogic.MessageBoxShow("E051", "処分業者");
                    this.isInputError = true;
                    this.form.SBN_GENBA_CD.Focus();
                    return catchErr;
                }

                var genba = this.accessor.GetGenba(this.form.SBN_GYOUSHA_CD.Text, this.form.SBN_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, false, true, false, out catchErr);
                if (!catchErr)
                {
                    return catchErr;
                }
                if (genba == null)
                {
                    this.form.SBN_GENBA_CD.IsInputErrorOccured = true;
                    this.msgLogic.MessageBoxShow("E020", "現場");
                    this.isInputError = true;
                    this.form.SBN_GENBA_CD.Focus();
                }
                else
                {
                    this.form.SBN_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SBN_GENBA_CD_Validated", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SBN_GENBA_CD_Validated", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(catchErr);
            }
            return catchErr;
        }
        #endregion

        #region 最終処分業者
        public bool LAST_SBN_GYOUSHA_CD_Validated()
        {
            bool catchErr = true;
            try
            {
                if (this.beforeCd == this.form.LAST_SBN_GYOUSHA_CD.Text && !this.isInputError)
                {
                    return catchErr;
                }
                this.form.LAST_SBN_GYOUSHA_NAME.Text = string.Empty;
                this.form.LAST_SBN_GENBA_CD.Text = string.Empty;
                this.form.LAST_SBN_GENBA_NAME.Text = string.Empty;
                if (string.IsNullOrEmpty(this.form.LAST_SBN_GYOUSHA_CD.Text))
                {
                    return catchErr;
                }

                var gyousha = this.accessor.GetGyousha(this.form.LAST_SBN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, false, false, true, out catchErr);
                if (!catchErr)
                {
                    return catchErr;
                }
                if (gyousha == null)
                {
                    this.form.LAST_SBN_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.msgLogic.MessageBoxShow("E020", "業者");
                    this.isInputError = true;
                    this.form.LAST_SBN_GYOUSHA_CD.Focus();
                }
                else
                {
                    this.form.LAST_SBN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("LAST_SBN_GYOUSHA_CD_Validated", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LAST_SBN_GYOUSHA_CD_Validated", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(catchErr);
            }
            return catchErr;
        }
        #endregion

        #region 最終処分現場
        public bool LAST_SBN_GENBA_CD_Validated()
        {
            bool catchErr = true;
            try
            {
                if (this.beforeCd == this.form.LAST_SBN_GENBA_CD.Text && !this.isInputError)
                {
                    return catchErr;
                }
                this.form.LAST_SBN_GENBA_NAME.Text = string.Empty;
                if (string.IsNullOrEmpty(this.form.LAST_SBN_GENBA_CD.Text))
                {
                    return catchErr;
                }
                if (string.IsNullOrEmpty(this.form.LAST_SBN_GYOUSHA_CD.Text))
                {
                    this.form.LAST_SBN_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.msgLogic.MessageBoxShow("E051", "最終処分業者");
                    this.isInputError = true;
                    this.form.LAST_SBN_GENBA_CD.Focus();
                    return catchErr;
                }

                var genba = this.accessor.GetGenba(this.form.LAST_SBN_GYOUSHA_CD.Text, this.form.LAST_SBN_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, false, false, true, out catchErr);
                if (!catchErr)
                {
                    return catchErr;
                }
                if (genba == null)
                {
                    this.form.LAST_SBN_GENBA_CD.IsInputErrorOccured = true;
                    this.msgLogic.MessageBoxShow("E020", "現場");
                    this.isInputError = true;
                    this.form.LAST_SBN_GENBA_CD.Focus();
                }
                else
                {
                    this.form.LAST_SBN_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("LAST_SBN_GENBA_CD_Validated", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LAST_SBN_GENBA_CD_Validated", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(catchErr);
            }
            return catchErr;
        }
        #endregion
        #endregion

        public void SetHstGyoushaByGyousha(string gyoushaCd)
        {
            bool catchErr = false;
            M_GYOUSHA gyousha = this.accessor.GetGyousha(gyoushaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate, true, false, false, out catchErr);
            if (gyousha != null)
            {
                if (this.form.HST_GYOUSHA_CD.Text != gyousha.GYOUSHA_CD)
                {
                    this.form.HST_GENBA_CD.Text = string.Empty;
                    this.form.HST_GENBA_NAME.Text = string.Empty;
                }
                this.form.HST_GYOUSHA_CD.Text = gyousha.GYOUSHA_CD;
                this.form.HST_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
            }
            else
            {
                this.form.HST_GYOUSHA_CD.Text = string.Empty;
                this.form.HST_GYOUSHA_NAME.Text = string.Empty;
                this.form.HST_GENBA_CD.Text = string.Empty;
                this.form.HST_GENBA_NAME.Text = string.Empty;
            }
        }

        public void SetHstGenbaByGenba(string gyoushaCd, string genbaCd)
        {
            if (string.IsNullOrEmpty(gyoushaCd))
            {
                return;
            }
            bool catchErr = false;
            M_GENBA genba = this.accessor.GetGenba(gyoushaCd, genbaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate, true, false, false, out catchErr);
            if (genba != null)
            {
                this.form.HST_GENBA_CD.Text = genba.GENBA_CD;
                this.form.HST_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
            }
            else
            {
                this.form.HST_GENBA_CD.Text = string.Empty;
                this.form.HST_GENBA_NAME.Text = string.Empty;
            }
        }

        // 設定後処理
        public void SettingsAfterDisplayData()
        {
            if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG || this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
            {
                return;
            }
            bool catchErr = true;
            M_GYOUSHA gyousha = this.accessor.GetGyousha(this.form.GYOUSHA_CD.Text, out catchErr);
            if (!catchErr)
            {
                throw (new Exception(""));
            }
            if (gyousha != null && gyousha.SHOKUCHI_KBN.IsTrue)
            {
                this.form.GYOUSHA_NAME_RYAKU.ReadOnly = false;
            }
            gyousha = this.accessor.GetGyousha(this.form.UNPAN_GYOUSHA_CD.Text, out catchErr);
            if (!catchErr)
            {
                throw (new Exception(""));
            }
            if (gyousha != null && gyousha.SHOKUCHI_KBN.IsTrue)
            {
                this.form.UNPAN_GYOUSHA_NAME.ReadOnly = false;
            }
            M_GENBA genba = this.accessor.GetGenba(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, out catchErr);
            if (!catchErr)
            {
                throw (new Exception(""));
            }
            if (genba != null && genba.SHOKUCHI_KBN.IsTrue)
            {
                this.form.GENBA_NAME_RYAKU.ReadOnly = false;
            }
            M_TORIHIKISAKI torihikisaki = this.accessor.GetTorihikisaki(this.form.TORIHIKISAKI_CD.Text, out catchErr);
            if (!catchErr)
            {
                throw (new Exception(""));
            }
            if (torihikisaki != null)
            {
                if (torihikisaki.SHOKUCHI_KBN.IsTrue)
                {
                    this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = false;
                }
                this.form.RECEIPT_KEISHOU_1.Text = torihikisaki.TORIHIKISAKI_KEISHOU1;
                this.form.RECEIPT_KEISHOU_2.Text = torihikisaki.TORIHIKISAKI_KEISHOU2;
            }
        }

        public string GetDate(out bool catchErr)
        {
            string ret = string.Empty;
            catchErr = true;
            try
            {
                DateTime date = this.accessor.GetDate();
                ret = date.ToString("HH:mm");
                return ret;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetDate", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetDate", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                return ret;
            }
        }

        internal bool SetJyuuryouForRegist()
        {
            bool ret = true;
            try
            {
                string stackJyuuryou = string.Empty;
                string emptyJyuuryou = string.Empty;
                string time = string.Empty;

                string stackJyuuryou_U = string.Empty;
                string time_U = string.Empty;

                string emptyJyuuryou_S = string.Empty;
                string time_S = string.Empty;

                foreach (Row row in this.form.gcMultiRow1.Rows)
                {
                    if (row.Index.Equals(0) && !string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_STAK_JYUURYOU].Value)))
                    {
                        stackJyuuryou_U = Convert.ToString(row.Cells[CELL_NAME_STAK_JYUURYOU].Value);
                        time_U = Convert.ToString(row.Cells[CELL_NAME_KEIRYOU_TIME].Value);
                    }

                    if (row.Index.Equals(0) && !string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value)))
                    {
                        emptyJyuuryou_S = Convert.ToString(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value);
                        time_S = Convert.ToString(row.Cells[CELL_NAME_KEIRYOU_TIME].Value);
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_NET_JYUURYOU].Value)))
                    {
                        stackJyuuryou = Convert.ToString(row.Cells[CELL_NAME_STAK_JYUURYOU].Value);
                        emptyJyuuryou = Convert.ToString(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value);
                        time = Convert.ToString(row.Cells[CELL_NAME_KEIRYOU_TIME].Value);
                    }
                }
                if (this.headerForm.KIHON_KEIRYOU.Text == "1")
                {
                    if (string.IsNullOrEmpty(this.form.EMPTY_JYUURYOU.Text))
                    {
                        this.form.EMPTY_JYUURYOU.Text = emptyJyuuryou;
                        this.form.EMPTY_KEIRYOU_TIME.Text = time;
                    }

                    if (string.IsNullOrEmpty(this.form.STACK_JYUURYOU.Text))
                    {
                        this.form.STACK_JYUURYOU.Text = stackJyuuryou_U;
                        this.form.STACK_KEIRYOU_TIME.Text = time_U;
                    }

                }
                else
                {
                    if (string.IsNullOrEmpty(this.form.STACK_JYUURYOU.Text))
                    {
                        this.form.STACK_JYUURYOU.Text = stackJyuuryou;
                    }
                    if (string.IsNullOrEmpty(this.form.STACK_KEIRYOU_TIME.Text))
                    {
                        this.form.STACK_KEIRYOU_TIME.Text = time;
                    }

                    if (string.IsNullOrEmpty(this.form.EMPTY_JYUURYOU.Text))
                    {
                        this.form.EMPTY_JYUURYOU.Text = emptyJyuuryou_S;
                        this.form.EMPTY_KEIRYOU_TIME.Text = time_S;
                    }

                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetJyuuryouForRegist", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
                return ret;
            }
            return ret;
        }

        internal bool JyuuryouCheck(out bool chkFlg)
        {
            bool ret = true;
            chkFlg = false;
            try
            {
                if (this.headerForm.KIHON_KEIRYOU.Text == "1")
                {
                    string sharyouEmpty = this.form.EMPTY_JYUURYOU.Text;
                    string empty = string.Empty;
                    string time = string.Empty;
                    foreach (Row row in this.form.gcMultiRow1.Rows)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_NET_JYUURYOU].Value)))
                        {
                            empty = Convert.ToString(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value);
                        }
                    }
                    if (!string.IsNullOrEmpty(sharyouEmpty) && !string.IsNullOrEmpty(empty)
                        && decimal.Parse(sharyouEmpty) != decimal.Parse(empty))
                    {
                        var diaRet = msgLogic.MessageBoxShowConfirm("明細最終行の空車重量（明細）と 空車重量（伝票）の値が異なっています。\n登録してもよろしいでしょうか？");
                        if (diaRet != DialogResult.Yes)
                        {
                            ret = false;
                        }
                        else
                        {
                            chkFlg = true;
                        }
                    }
                }
                else
                {
                    string denpyou = this.form.STACK_JYUURYOU.Text;
                    string stack = string.Empty;
                    foreach (Row row in this.form.gcMultiRow1.Rows)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_NET_JYUURYOU].Value)))
                        {
                            stack = Convert.ToString(row.Cells[CELL_NAME_STAK_JYUURYOU].Value);
                        }
                    }
                    if (!string.IsNullOrEmpty(denpyou) && !string.IsNullOrEmpty(stack)
                        && decimal.Parse(denpyou) != decimal.Parse(stack))
                    {
                        var diaRet = msgLogic.MessageBoxShowConfirm("明細最終行の総重量（明細）と総重量（伝票）の値が異なっています。\n登録してもよろしいでしょうか？");
                        if (diaRet != DialogResult.Yes)
                        {
                            ret = false;
                        }
                        else
                        {
                            chkFlg = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("JyuuryouCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
                return ret;
            }
            return ret;
        }

        /// <summary>
        /// 計量時間
        /// </summary>
        internal bool SetKeiryouTime(Row targetRow)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(targetRow);

                if (targetRow == null)
                {
                    return ret;
                }

                if (this.form.IsDataLoading)
                {
                    return ret;
                }
                bool catchErr = true;
                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value))
                    && !string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value)))
                {
                    targetRow.Cells[CELL_NAME_KEIRYOU_TIME].Value = this.GetDate(out catchErr);
                    return catchErr;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetKeiryouTime", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetKeiryouTime", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;

        }

        /// <summary>
        /// ステータス保存
        /// </summary>
        public void SetStatus_tadasigaki()
        {
            Properties.Settings.Default.tadasigaki = this.form.RECEIPT_TADASHIGAKI.Text;     //但し書き
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 明細の総重量、空車重量 
        /// </summary>
        /// <returns></returns>
        private bool CheckFocusStackEmptyJuuryou()
        {
            bool isTabStop = false;

            if (Properties.Settings.Default.DetailCtrl != null && Properties.Settings.Default.DetailCtrl.Count > 0)
            {
                var listCheck = Properties.Settings.Default.DetailCtrl.Cast<string>().Where(x => x.Contains(CELL_NAME_STAK_JYUURYOU) || x.Contains(CELL_NAME_EMPTY_JYUURYOU)).ToList();

                var row = this.form.gcMultiRow1.Template.Row;
                for (var i = 0; i < listCheck.Count; i++)
                {
                    // string分解
                    string str = listCheck[i];
                    int ctpos = str.IndexOf(':');
                    string controlName = str.Substring(0, ctpos);
                    int nmpos = str.IndexOf(':', ctpos + 1);
                    int tspos = str.IndexOf(':', nmpos + 1);
                    string tbstop = str.Substring(nmpos + 1, tspos - nmpos - 1);

                    GrapeCity.Win.MultiRow.Cell control = row.Cells[controlName];
                    if (control == null)
                    {
                        continue;
                    }

                    if (tbstop.Equals("True"))
                    {
                        isTabStop = true;
                        break;
                    }
                }
            }

            return isTabStop;
        }

        /*ThangNguyen 20200219 #134056, #134060 Start*/
        /// <summary>
        /// 
        /// </summary>
        public void SetSbnGyoushaByGyousha(string gyoushaCd)
        {
            bool catchErr = false;
            M_GYOUSHA gyousha = this.accessor.GetGyousha(gyoushaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate, false, true, false, out catchErr);
            if (gyousha != null)
            {
                this.form.SBN_GYOUSHA_CD.Text = gyousha.GYOUSHA_CD;
                this.form.SBN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                this.form.SBN_GENBA_CD.Text = string.Empty;
                this.form.SBN_GENBA_NAME.Text = string.Empty;
            }
            else
            {
                this.form.SBN_GYOUSHA_CD.Text = string.Empty;
                this.form.SBN_GYOUSHA_NAME.Text = string.Empty;
                this.form.SBN_GENBA_CD.Text = string.Empty;
                this.form.SBN_GENBA_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetSbnGenbaByGenba(string gyoushaCd, string genbaCd)
        {
            if (string.IsNullOrEmpty(gyoushaCd))
            {
                return;
            }
            bool catchErr = false;
            M_GENBA genba = this.accessor.GetGenba(gyoushaCd, genbaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate, false, true, false, out catchErr);
            if (genba != null)
            {
                this.form.SBN_GENBA_CD.Text = genba.GENBA_CD;
                this.form.SBN_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
            }
            else
            {
                this.form.SBN_GENBA_CD.Text = string.Empty;
                this.form.SBN_GENBA_NAME.Text = string.Empty;
            }
        }
        /*ThangNguyen 20200219 #134056, #134060 End*/

        ///////////////////////////////////////////////////////
        //受入実績明細
        ///////////////////////////////////////////////////////

        /// <summary>
        /// 受入実績明細に新規行を追加
        /// </summary>
        internal void AddNewRow2()
        {
            LogUtility.DebugMethodStart();

            if ((Row)this.form.gcMultiRow2.CurrentRow != null)
            {
                this.form.gcMultiRow2.EndEdit();
                Row selectedRows = (Row)this.form.gcMultiRow2.CurrentRow;

                int iSaveRowIndex = this.form.gcMultiRow2.CurrentRow.Index;
                this.form.gcMultiRow2.Rows.Insert(this.form.gcMultiRow2.CurrentRow.Index);
                this.form.gcMultiRow2.ClearSelection();
                this.form.gcMultiRow2.AddSelection(iSaveRowIndex);
                this.form.gcMultiRow2.NotifyCurrentCellDirty(false);

                // 行番号採番
                if (!this.NumberingRowNo2())
                {
                    return;
                }
            }

            LogUtility.DebugMethodStart();
        }

        /// <summary>
        /// 受入実績明細のカレント行を削除
        /// </summary>
        internal bool RemoveSelectedRow2()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if ((Row)this.form.gcMultiRow2.CurrentRow != null)
                {
                    this.form.gcMultiRow2.EndEdit();
                    Row selectedRows = (Row)this.form.gcMultiRow2.CurrentRow;
                    if (!selectedRows.IsNewRow)
                    {
                        // 行削除の後に現在のCellのフォーカスアウトチェックが走ってしまうので、FocusOutCheckMethodを削除
                        var currentCell = this.form.gcMultiRow2.CurrentCell as ICustomControl;
                        if (currentCell != null)
                        {
                            currentCell.FocusOutCheckMethod = null;
                        }

                        // 行削除
                        int iSaveIndex = this.form.gcMultiRow2.CurrentRow.Index;
                        this.form.gcMultiRow2.Rows.Remove(selectedRows);
                        this.form.gcMultiRow2.ClearSelection();
                    }
                    this.form.gcMultiRow2.EndEdit();
                    this.form.gcMultiRow2.NotifyCurrentCellDirty(false);
                    // 行番号採番
                    if (!this.NumberingRowNo2())
                    {
                        ret = false;
                        return ret;
                    }
                    this.form.gcMultiRow2.ResumeLayout();

                    // 合計割合
                    if (!this.CalcTotalValues2())
                    {
                        ret = false;
                        return ret;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("RemoveSelectedRow2", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("RemoveSelectedRow2", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// MultiRowのデータに対しROW_NOを採番します
        /// </summary>
        public bool NumberingRowNo2()
        {
            bool ret = false;
            try
            {
                if (!this.form.notEditingOperationFlg)
                {
                    this.form.gcMultiRow2.BeginEdit(false);
                }

                foreach (Row dr in this.form.gcMultiRow2.Rows)
                {
                    dr.Cells[CELL_NAME_ROW_NO].Value = dr.Index + 1;
                }

                if (!this.form.notEditingOperationFlg)
                {
                    this.form.gcMultiRow2.EndEdit();
                    this.form.gcMultiRow2.NotifyCurrentCellDirty(false);
                }
                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("NumberingRowNo2", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("NumberingRowNo2", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 品名セット
        /// </summary>
        /// <param name="targetRow"></param>
        /// <param name="catchErr"></param>
        /// <returns></returns>
        internal bool GetHinmei2(Row targetRow, out bool catchErr)
        {
            catchErr = true;
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value)))
                {
                    // 品名コードの入力がない場合
                    return returnVal;
                }

                M_HINMEI[] hinmeis = this.accessor.GetAllValidHinmeiData(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());

                if (hinmeis != null && hinmeis.Count() > 0)
                {
                    targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = hinmeis[0].HINMEI_NAME_RYAKU;
                }
                else
                {
                    returnVal = true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetHinmei2", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetHinmei2", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }

        /// <summary>
        /// 品名表示
        /// </summary>
        /// <param name="targetRow"></param>
        /// <returns></returns>
        internal bool GetHinmeiForPop2(Row targetRow)
        {
            LogUtility.DebugMethodStart();
            bool returnVal = false;

            if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value)))
            {
                // 品名コードの入力がない場合
                return returnVal;
            }

            M_HINMEI[] hinmeis = this.accessor.GetAllValidHinmeiData(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());
            if (hinmeis != null && hinmeis.Count() > 0)
            {
                targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = hinmeis[0].HINMEI_NAME_RYAKU;
            }

            LogUtility.DebugMethodEnd();
            return returnVal;
        }

        /// <summary>
        /// 品名コードの存在チェック（伝種区分が受入/出荷、または共通のみ可）
        /// </summary>
        /// <param name="targetRow"></param>
        /// <returns>true: 入力された品名コードが存在する, false: 入力された品名コードが存在しない</returns>
        internal bool CheckHinmeiCd2(Row targetRow, out bool catchErr)
        {
            catchErr = true;
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value)))
                {
                    // 品名コードの入力がない場合
                    return returnVal;
                }

                M_HINMEI[] hinmeis = this.accessor.GetAllValidHinmeiData(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());
                if (hinmeis == null || hinmeis.Count() < 1)
                {
                    // 品名コードがマスタに存在しない場合
                    // ただし、部品で存在チェックが行われるため、実際にここを通ることはない
                    return returnVal;
                }
                else
                {
                    M_HINMEI hinmei = hinmeis[0];
                    // 品名コードがマスタに存在する場合
                    if ((hinmei.DENSHU_KBN_CD != Convert.ToInt16(this.form.selectDenshuKbnCd)
                        && hinmei.DENSHU_KBN_CD != Convert.ToInt16(DENSHU_KBN.KYOUTSUU)))
                    {
                        // 入力された品名コードに紐づく伝種区分が受入/出荷、共通以外の場合はエラーメッセージ表示
                        targetRow.Cells[CELL_NAME_HINMEI_CD].Value = null;
                        targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = null;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E058", "");
                        return returnVal;
                    }
                }
                returnVal = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckHinmeiCd2", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHinmeiCd2", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
            return returnVal;
        }

        /// <summary>
        /// 品名入力値チェック
        /// </summary>
        /// <returns>true: 問題なし, false:問題あり</returns>
        internal bool ValidateHinmeiName2(out bool catchErr)
        {
            catchErr = true;
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                Row targetRow = this.form.gcMultiRow2.CurrentRow;

                if (targetRow == null)
                {
                    return returnVal;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value)))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_NAME].EditedFormattedValue)))
                    {
                        CellPosition pos = this.form.gcMultiRow2.CurrentCellPosition;
                        this.form.gcMultiRow2.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, CELL_NAME_HINMEI_NAME);

                        var cell = targetRow.Cells[CELL_NAME_HINMEI_NAME] as ICustomAutoChangeBackColor;
                        cell.IsInputErrorOccured = true;
                        cell.UpdateBackColor();

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E012", "品名");
                        return returnVal;
                    }
                }
                returnVal = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ValidateHinmeiName2", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateHinmeiName2", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }

        /// <summary>
        /// 明細欄の品名をセットします
        /// </summary>
        /// <param name="row">現在のセルを含む行（CurrentRow）</param>
        internal bool SetHinmeiName2(Row row)
        {
            try
            {
                if (row == null)
                {
                    return true;
                }
                bool catchErr = true;
                bool retChousei = this.CheckHinmeiCd2(row, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (retChousei)    // 品名コードの存在チェック（伝種区分が受入/出荷、または共通）
                {
                    // 入力された品名コードが存在するとき
                    if (row.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value != null)
                    {
                        if (string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value.ToString()))
                        {
                            // 品名が空の場合再セット
                            this.GetHinmei2(row, out catchErr);
                            if (!catchErr)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        // 品名が空の場合再セット
                        this.GetHinmei2(row, out catchErr);
                        if (!catchErr)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetHinmeiName2", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHinmeiName2", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }
        /// <summary>
        /// 数量割合合計
        /// </summary>
        internal bool CalcTotalValues2()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                decimal netTotal = 0;
                foreach (Row dr in this.form.gcMultiRow2.Rows)
                {
                    decimal netSuuryouWariai = 0;
                    decimal.TryParse(Convert.ToString(dr.Cells[CELL_NAME_SUURYOU_WARIAI].Value), out netSuuryouWariai);
                    // 数量割合
                    netTotal += netSuuryouWariai;
                }
                this.form.SUURYOU_WARIAI_GOUKEI.Text = netTotal.ToString("N");
                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcTotalValues2", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcTotalValues2", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }
        /// <summary>
        /// 作業者チェック
        /// </summary>
        internal bool CheckSagyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.SAGYOUSHA_NAME.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.SAGYOUSHA_CD.Text))
                {
                    // 入力担当者CDがなければ既にエラーが表示されているはずなので何もしない
                    return true;
                }

                var shainEntity = this.accessor.GetShain(this.form.SAGYOUSHA_CD.Text, true);
                if (shainEntity == null)
                {
                    return true;
                }
                else if (shainEntity.SHOBUN_TANTOU_KBN.IsFalse && shainEntity.NYUURYOKU_TANTOU_KBN.IsFalse && shainEntity.UNTEN_KBN.IsFalse)
                {
                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E058", "");
                    this.form.SAGYOUSHA_CD.Focus();
                    return true;
                }
                else
                {
                    this.form.SAGYOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckSagyousha", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckSagyousha", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 添付ファイルデータを取得する。
        /// </summary>
        public void GetFileData()
        {
            if (this.dto.JentryEntity.DENPYOU_SHURUI.IsNull)
            {
                // DTOを初期化して終了
                this.dto.fileLinkUJEList = new M_FILE_LINK_UKEIRE_JISSEKI_ENTRY[] { new M_FILE_LINK_UKEIRE_JISSEKI_ENTRY() };
                this.dto.fileDataList = new T_FILE_DATA[] { new T_FILE_DATA() };
                return;
            }

            var entityList = this.fileLinkUJEDao.GetDataByCd(short.Parse(this.dto.JentryEntity.DENPYOU_SHURUI.ToString()), this.dto.JentryEntity.DENPYOU_SYSTEM_ID.ToString());
            if (entityList.Count > 0)
            {
                this.dto.fileLinkUJEList = entityList.ToArray();

                List<long> fileIds = new List<long>();
                foreach (M_FILE_LINK_UKEIRE_JISSEKI_ENTRY ent in this.dto.fileLinkUJEList)
                {
                    fileIds.Add(long.Parse(ent.FILE_ID.ToString()));
                }

                // ファイルデータを取得する。
                List<T_FILE_DATA> fileList = fileDataDao.GetDataByKeyList(fileIds);
                if (fileList.Count > 0)
                {
                    this.dto.fileDataList = fileList.ToArray();
                }
            }
            else
            {
                this.dto.fileLinkUJEList = new M_FILE_LINK_UKEIRE_JISSEKI_ENTRY[] { new M_FILE_LINK_UKEIRE_JISSEKI_ENTRY() };
                this.dto.fileDataList = new T_FILE_DATA[] { new T_FILE_DATA() };
            }
        }

        /// <summary>
        /// 閲覧イベント
        /// </summary>
        public void ExecEtsuran()
        {
            // カレント行のfileIdを取得する。
            var fileId = this.form.dgvTenpuFileDetail.Rows[this.form.dgvTenpuFileDetail.CurrentRow.Index].Cells["HIDDEN_FILEID"].Value;

            // プレビュー
            this.uploadLogic.Preview(long.Parse(fileId.ToString()));
        }

        /// <summary>
        /// カンマ編集
        /// </summary>
        /// <returns></returns>
        private string SetComma(string value)
        {
            if (value == null)
            {
                return "0";
            }
            else
            {
                return string.Format("{0:#,0}", Convert.ToDecimal(value));
            }
        }

        /// <summary>
        /// 指定された端数CDに従い、金額の端数処理を行う
        /// </summary>
        /// <param name="kingaku">端数処理対象金額</param>
        /// <param name="calcCD">端数CD</param>
        /// <returns name="decimal">端数処理後の金額</returns>
        public static decimal FractionCalc(decimal kingaku, int calcCD)
        {
            decimal returnVal = 0;		// 戻り値
            decimal sign = 1;           // 1(正) or -1(負)

            if (kingaku < 0)
                sign = -1;

            switch ((fractionType)calcCD)
            {
                case fractionType.CEILING:
                    returnVal = Math.Ceiling(Math.Abs(kingaku)) * sign;
                    break;
                case fractionType.FLOOR:
                    returnVal = Math.Floor(Math.Abs(kingaku)) * sign;
                    break;
                case fractionType.ROUND:
                    returnVal = Math.Round(Math.Abs(kingaku), 0, MidpointRounding.AwayFromZero) * sign;
                    break;
                default:
                    // 何もしない
                    returnVal = kingaku;
                    break;
            }

            return returnVal;
        }


        /// <summary>
        /// 消費税を税区分（外税、内税、非課税）別に計算
        /// </summary>
        /// <param name="zeiKbn">税区分</param>
        /// <param name="Kingaku">金額</param>
        /// <param name="ShouhizeiRate">消費税率</param>
        /// <param name="HasuCd">端数の処理方法</param>
        /// <returns>消費税額</returns>
        private decimal CalcZeiKbnShohiZei(string zeiKbn, decimal Kingaku, decimal ShouhizeiRate, int HasuCd)
        {
            decimal returnVal = 0;
            decimal dTax = 0;
            decimal dUTax = 0;
            if (ZEI_KBN_1.Equals(zeiKbn))
            {
                // 外税
                dTax = Kingaku * ShouhizeiRate;
                returnVal = FractionCalc(dTax, HasuCd);
            }
            else if (ZEI_KBN_2.Equals(zeiKbn))
            {
                // 内税
                dUTax = Kingaku;
                dUTax = dUTax - (dUTax / (ShouhizeiRate + 1));
                returnVal = FractionCalc(dUTax, HasuCd);
            }
            else if (ZEI_KBN_3.Equals(zeiKbn))
            {
                // 非課税
                returnVal = Convert.ToDecimal(KIGAKU_0);
            }
            return returnVal;
        }


        #region 領収書/仕切書チェック

        /// <summary>
        /// 領収書/仕切書チェック
        /// アラートに対して、
        /// 「はい」を選択した場合→以降のチェック処理を行わず、登録処理を続行する
        /// 「いいえ」を選択した場合→画面項目を変更し、登録処理を中断する
        /// </summary>
        internal bool Ryousyu_ShikiriCheck()
        {
            bool returnVal = false;
            DialogResult result = 0;

            string ErrUHinmeiCD = string.Empty;  //エラー品名用
            string zeiKbn = string.Empty;　      //品名税区分

            #region ０）品名(明細)の税区分データ取得
            //１）品名(明細)の税区分チェック用のデータ取得
            //　　品名の税区分 = 1:外税の場合アラート
            foreach (var row in this.form.gcMultiRow1.Rows)
            {
                M_HINMEI[] hinmeis;

                if (row.Cells["HINMEI_CD"].Value != null)
                {
                    hinmeis = this.accessor.GetAllValidHinmeiData(row.Cells["HINMEI_CD"].Value.ToString());
                    zeiKbn = hinmeis[0].ZEI_KBN_CD.ToString();

                    if (!string.IsNullOrEmpty(zeiKbn))
                    {
                        if (ZEI_KBN_1.Equals(zeiKbn))
                        {
                            ErrUHinmeiCD = ErrUHinmeiCD + row.Cells["HINMEI_CD"].Value.ToString() + "、";
                        }
                    }
                }
            }

            #endregion ０）品名(明細)の税区分データ取得

            #region １）品名(明細)の税区分チェック
            //１）品名(明細)の税区分チェック
            //　　品名の税区分 = 1:外税の場合アラート
            ErrHinmeiCD = string.Empty;
            ErrHinmeiCD = ErrUHinmeiCD;

            if (ErrHinmeiCD != "")
            {
                if ((this.form.RECEIPT_KBN_CD.Text == "1") && (form.RECEIPT_KBN_CD.Enabled))
                {
                    ErrHinmeiCD = ErrHinmeiCD.Substring(0, ErrHinmeiCD.Length - 1);
                    result = MessageBox.Show(string.Format("税区分に外税が登録されている品名は、\r適格請求書の要件を満たした{0}になりませんがよろしいでしょうか？\r（品名CD={1}）", "領収書", ErrHinmeiCD), "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        return returnVal;
                    }
                    else
                    {
                        this.form.RECEIPT_KBN_CD.Text = "2";
                        returnVal = true;
                        return returnVal;
                    }
                }
            }
            #endregion １）品名(明細)の税区分チェック

            #region ２）税計算区分のチェック
            //２）税計算区分のチェック
            //　　請求取引-税計算区分 =3の場合アラート
            if ((this.form.RECEIPT_KBN_CD.Text == "1") && (form.RECEIPT_KBN_CD.Enabled))
            {
                if (this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text == "3")
                {
                    result = MessageBox.Show(string.Format("税計算区分＝3.明細毎 は、\r適格請求書の要件を満たした{0}になりませんがよろしいでしょうか？", "領収書"), "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        return returnVal;
                    }
                    else
                    {
                        this.form.RECEIPT_KBN_CD.Text = "2";
                        returnVal = true;
                        return returnVal;
                    }
                }
            }
            #endregion ２）税計算区分のチェック

            #region ３）登録番号チェック

            if ((this.form.RECEIPT_KBN_CD.Text == "1") && (form.RECEIPT_KBN_CD.Enabled))
            {
                //自社情報入力ー登録番号が未設定の場合アラート 
                M_CORP_INFO entCorpInfo = CommonShogunData.CORP_INFO;

                if ((entCorpInfo != null) && (String.IsNullOrEmpty(entCorpInfo.TOUROKU_NO)))
                {
                    result = MessageBox.Show("登録番号が未入力です。\r登録番号が表示されませんがよろしいでしょうか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        return returnVal;
                    }
                    else
                    {
                        returnVal = true;
                        this.form.RECEIPT_KBN_CD.Text = "2";
                        return returnVal;
                    }
                }
            }
            #endregion ３）登録番号チェック

            return returnVal;
        }
        #endregion
    }
}