// $Id: LogicClass.cs 56672 2015-07-24 08:28:40Z y-hosokawa@takumi-sys.co.jp $
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
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.Report;
using Shougun.Core.SalesPayment.DenpyouHakou;
using Shougun.Core.SalesPayment.DenpyouHakou.Const;
using Shougun.Function.ShougunCSCommon.Const;
using Shougun.Function.ShougunCSCommon.Dto;
using Shougun.Function.ShougunCSCommon.Utility;
using SystemSetteiHoshu.Logic;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.ContenaShitei.Const;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Dto;
using r_framework.FormManager;
using Shougun.Core.SalesPayment.DenpyouHakou.Report;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou_invoice.Report;

namespace Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Setting.ButtonSetting.xml";

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

        /// <summary>確定区分</summary>
        internal const string CELL_NAME_KAKUTEI_KBN = "KAKUTEI_KBN";

        /// <summary>売上/支払日</summary>
        internal const string CELL_NAME_URIAGESHIHARAI_DATE = "URIAGESHIHARAI_DATE";

        /// <summary>状況</summary>
        internal const string CELL_NAME_JOUKYOU = "JOUKYOU";

        /// <summary>品名CD</summary>
        internal const string CELL_NAME_HINMEI_CD = "HINMEI_CD";

        /// <summary>品名</summary>
        internal const string CELL_NAME_HINMEI_NAME = "HINMEI_NAME";

        /// <summary>伝票区分</summary>
        internal const string CELL_NAME_DENPYOU_KBN_CD = "DENPYOU_KBN_CD";

        /// <summary>伝票区分名</summary>
        internal const string CELL_NAME_DENPYOU_KBN_NAME = "DENPYOU_KBN_NAME";

        /// <summary>数量</summary>
        internal const string CELL_NAME_SUURYOU = "SUURYOU";

        /// <summary>単位CD</summary>
        internal const string CELL_NAME_UNIT_CD = "UNIT_CD";

        /// <summary>単位名</summary>
        internal const string CELL_NAME_UNIT_NAME_RYAKU = "UNIT_NAME_RYAKU";

        /// <summary>単価</summary>
        internal const string CELL_NAME_TANKA = "TANKA";

        /// <summary>金額</summary>
        internal const string CELL_NAME_KINGAKU = "KINGAKU";

        /// <summary>備考</summary>
        internal const string CELL_NAME_MEISAI_BIKOU = "MEISAI_BIKOU";

        /// <summary>マニフェスト番号</summary>
        internal const string CELL_NAME_MANIFEST_ID = "MANIFEST_ID";

        /// <summary>システムID</summary>
        internal const string CELL_NAME_SYSTEM_ID = "SYSTEM_ID";

        /// <summary>明細システムID</summary>
        internal const string CELL_NAME_DETAIL_SYSTEM_ID = "DETAIL_SYSTEM_ID";

        /// <summary>消費税率</summary>
        internal const string CELL_NAME_SHOUHIZEI_RATE = "CELL_SHOUHIZEI_RATE";

        /// <summary>税区分CD</summary>
        internal const string CELL_NAME_HINMEI_ZEI_KBN_CD = "HINMEI_ZEI_KBN_CD";

        /// <summary>受付（収集）テーブル区分</summary>
        internal const int UKETSUKE_SS_KBN = 1;

        /// <summary>受付（出荷）テーブル区分</summary>
        internal const int UKETSUKE_SK_KBN = 2;

        /// <summary>受付（物販）テーブル区分</summary>
        internal const int UKETSUKE_BP_KBN = 3;

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
        internal UIHeaderForm headerForm;

        /// <summary>
        /// 支払入力専用DBアクセッサー
        /// </summary>
        internal Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Accessor.DBAccessor accessor;

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
        /// T_UR_SH_ENTRY用DataBinder
        /// </summary>
        internal DataBinderLogic<T_UR_SH_ENTRY> urshEntryDataBinder;

        /// <summary>
        /// T_UR_SH_DETAIL用DataBinder
        /// </summary>
        internal DataBinderLogic<T_UR_SH_DETAIL> urshDetailDataBinder;

        /// <summary>
        /// 伝票区分全件
        /// </summary>
        Dictionary<short, M_DENPYOU_KBN> denpyouKbnDictionary = new Dictionary<short, M_DENPYOU_KBN>();

        /// <summary>
        /// 単位区分全件
        /// </summary>
        Dictionary<short, M_UNIT> unitDictionary = new Dictionary<short, M_UNIT>();

        /// <summary>
        /// 車輌CDマスタ存在チェック実行フラグ
        /// </summary>
        bool isSharyouMasterCheck = true;

        /// <summary>
        /// UIFormの入力コントロール名一覧
        /// </summary>
        private string[] inputUiFormControlNames = { "ENTRY_NUMBER",
                                                       "RENBAN",
                                                       "KAKUTEI_KBN",
                                                       "UKETSUKE_NUMBER",
                                                       "KEIRYOU_NUMBER", "TAIRYUU_BIKOU",
                                                       "DENPYOU_BIKOU",
                                                       "nextButton", "previousButton", 
                                                       "DENPYOU_DATE", "URIAGE_DATE", "SHIHARAI_DATE",
                                                       "SHARYOU_CD",
                                                       "NYUURYOKU_TANTOUSHA_CD",
                                                       "NIZUMI_GYOUSHA_CD","NIZUMI_GENBA_CD",
                                                       "TORIHIKISAKI_CD", "TORIHIKISAKI_SEARCH_BUTTON",
                                                       "SHASHU_CD", "UNPAN_GYOUSHA_CD",
                                                       "UNPAN_GYOUSHA_SEARCH_BUTTON",
                                                       "GYOUSHA_CD", "GENBA_SEARCH_BUTTON",
                                                       "UNTENSHA_CD", "UNTEN_KBN",
                                                       "NINZUU_CNT", "GENBA_CD",
                                                       "KEITAI_KBN_CD",
                                                       "NYUURYOKU_TANTOUSHA_NAME",
                                                       "customPopupOpenButton1",
                                                       "customPopupOpenButton3",
                                                       "customPopupOpenButton4",
                                                       "NIOROSHI_GYOUSHA_CD", "NIOROSHI_GYOUSHA_SEARCH_BUTTON", "NYUURYOKU_TANTOUSHA_SEARCH_BUTTON", "DAIKAN_KBN", "NIOROSHI_GENBA_CD", "customPopupOpenButton2", "SAISHUU_SHOBUNJOU_KBN", "MANIFEST_SHURUI_CD", "CONTENA_SOUSA_CD", "MANIFEST_TEHAI_CD", "EIGYOU_TANTOUSHA_CD", "SHUUKEI_KOUMOKU_CD", "ukeireNyuuryokuDetail1", "EIGYOU_TANTOU_KBN", //PhuocLoc 2020/12/01 #136221
                                                       "GYOUSHA_SEARCH_BUTTON", "NIZUMI_GYOUSHA_SEARCH_BUTTON", "NIZUMI_GENBA_SEARCH_BUTTON", "NIOROSHI_GENBA_SEARCH_BUTTON",
                                                       "TORIHIKISAKI_NAME_RYAKU", "GYOUSHA_NAME_RYAKU", "GENBA_NAME_RYAKU", "NIZUMI_GYOUSHA_NAME", "NIZUMI_GENBA_NAME","NIOROSHI_GYOUSHA_NAME","NIOROSHI_GENBA_NAME","UNPAN_GYOUSHA_NAME",
                                                       "URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON","SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON","DENPYOU_RIREKI_BTN"};

        /// <summary>
        /// UIFormの入力コントロール名一覧（参照用）
        /// </summary>
        private string[] refUiFormControlNames = { "ENTRY_NUMBER",
                                                       "RENBAN",
                                                       "KAKUTEI_KBN",
                                                       "UKETSUKE_NUMBER",
                                                       "KEIRYOU_NUMBER", "TAIRYUU_BIKOU",
                                                       "DENPYOU_BIKOU",
                                                       "DENPYOU_DATE", "URIAGE_DATE", "SHIHARAI_DATE",
                                                       "SHARYOU_CD",
                                                       "NYUURYOKU_TANTOUSHA_CD",
                                                       "NIZUMI_GYOUSHA_CD","NIZUMI_GENBA_CD",
                                                       "TORIHIKISAKI_CD", "TORIHIKISAKI_SEARCH_BUTTON",
                                                       "SHASHU_CD", "UNPAN_GYOUSHA_CD",
                                                       "UNPAN_GYOUSHA_SEARCH_BUTTON",
                                                       "GYOUSHA_CD", "GENBA_SEARCH_BUTTON",
                                                       "UNTENSHA_CD", "UNTEN_KBN",
                                                       "NINZUU_CNT", "GENBA_CD",
                                                       "KEITAI_KBN_CD",
                                                       "NYUURYOKU_TANTOUSHA_NAME",
                                                       "customPopupOpenButton1",
                                                       "customPopupOpenButton3",
                                                       "customPopupOpenButton4",
                                                       "NIOROSHI_GYOUSHA_CD", "NIOROSHI_GYOUSHA_SEARCH_BUTTON", "NYUURYOKU_TANTOUSHA_SEARCH_BUTTON", "DAIKAN_KBN", "NIOROSHI_GENBA_CD", "customPopupOpenButton2", "SAISHUU_SHOBUNJOU_KBN", "MANIFEST_SHURUI_CD", "CONTENA_SOUSA_CD", "MANIFEST_TEHAI_CD", "EIGYOU_TANTOUSHA_CD", "SHUUKEI_KOUMOKU_CD", "ukeireNyuuryokuDetail1", "EIGYOU_TANTOU_KBN", //PhuocLoc 2020/12/01 #136221
                                                       "GYOUSHA_SEARCH_BUTTON", "NIZUMI_GYOUSHA_SEARCH_BUTTON", "NIZUMI_GENBA_SEARCH_BUTTON", "NIOROSHI_GENBA_SEARCH_BUTTON",
                                                       "TORIHIKISAKI_NAME_RYAKU", "GYOUSHA_NAME_RYAKU", "GENBA_NAME_RYAKU", "NIZUMI_GYOUSHA_NAME", "NIZUMI_GENBA_NAME","NIOROSHI_GYOUSHA_NAME","NIOROSHI_GENBA_NAME","UNPAN_GYOUSHA_NAME",
                                                       "URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON","SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON","GYOUSHA_SEARCH_BUTTON","NIZUMI_GYOUSHA_SEARCH_BUTTON","NIZUMI_GENBA_SEARCH_BUTTON","NIOROSHI_GENBA_SEARCH_BUTTON"};

        // No.3822-->
        /// <summary>
        /// タブストップ用
        /// </summary>
        private string[] tabUiFormControlNames = 
            {   "KYOTEN_CD","NYUURYOKU_TANTOUSHA_CD","ENTRY_NUMBER",
                "RENBAN","KAKUTEI_KBN","UKETSUKE_NUMBER", "KEIRYOU_NUMBER","DENPYOU_DATE",
                "URIAGE_DATE","SHIHARAI_DATE","GYOUSHA_CD", "GYOUSHA_NAME_RYAKU", 
                "GENBA_CD", "GENBA_NAME_RYAKU", "TORIHIKISAKI_CD","TORIHIKISAKI_NAME_RYAKU",
                "NIZUMI_GYOUSHA_CD","NIZUMI_GYOUSHA_NAME", "NIZUMI_GENBA_CD","NIZUMI_GENBA_NAME",
                "NIOROSHI_GYOUSHA_CD", "NIOROSHI_GYOUSHA_NAME", "NIOROSHI_GENBA_CD","NIOROSHI_GENBA_NAME",
                "UNPAN_GYOUSHA_CD","UNPAN_GYOUSHA_NAME","SHASHU_CD",
                "SHASHU_NAME","SHARYOU_CD","SHARYOU_NAME_RYAKU","UNTENSHA_CD","NINZUU_CNT",
                "KEITAI_KBN_CD", "MANIFEST_SHURUI_CD", "MANIFEST_TEHAI_CD","EIGYOU_TANTOUSHA_CD", "SHUUKEI_KOUMOKU_CD", //PhuocLoc 2020/12/01 #136221
                "DENPYOU_BIKOU","TAIRYUU_BIKOU", 
            };
        private string[] tabDetailControlNames =
            {   
                "HINMEI_CD", "HINMEI_NAME","SUURYOU","UNIT_CD","TANKA",
                "KINGAKU","MANIFEST_ID","MEISAI_BIKOU",
            };
        // No.3822<--

        /// <summary>
        /// HeaderFormの入力コントロール名一覧
        /// </summary>
        private string[] inputHeaderControlNames = { "KYOTEN_CD"};

        /// <summary>
        /// Detailの入力コントロール名一覧
        /// </summary>
        private string[] inputDetailControlNames = { "ROW_NO", "KAKUTEI_KBN", "JOUKYOU", "URIAGESHIHARAI_DATE", "HINMEI_CD", "HINMEI_NAME", "DENPYOU_KBN_CD", "DENPYOU_KBN_NAME", "SUURYOU", "UNIT_CD", "UNIT_NAME_RYAKU", "TANKA", "KINGAKU", "MEISAI_BIKOU", "DETAIL_SYSTEM_ID", "MANIFEST_ID" };

        /// <summary>
        /// 入出金更新用DTO
        /// </summary>
        private NyuuShukkinDTOClass nyuuShukkinDto = new NyuuShukkinDTOClass();

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

        /// <summary>
        /// 収集受付入力エンティティ（更新用に保持）
        /// </summary>
        internal T_UKETSUKE_SS_ENTRY tUketsukeSsEntry;

        /// <summary>
        /// 出荷受付入力エンティティ（更新用に保持）
        /// </summary>
        private T_UKETSUKE_SK_ENTRY tUketsukeSkEntry;

        // 20140512 kayo No.679 計量番号連携 start
        /// <summary>
        /// 持込受付入力エンティティ（更新用に保持）
        /// </summary>
        private T_UKETSUKE_MK_ENTRY tUketsukeMkEntry;
        // 20140512 kayo No.679 計量番号連携 end

        // No.3822-->
        private System.Collections.Specialized.StringCollection DenpyouCtrl = new System.Collections.Specialized.StringCollection();
        private System.Collections.Specialized.StringCollection DetailCtrl = new System.Collections.Specialized.StringCollection();
        // No.3822<--

        /// 20141014 teikyou 「売上・支払入力画面」の休動Checkを追加する　start
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
        /// 20141014 teikyou 「売上・支払入力画面」の休動Checkを追加する　end        

        // 4935_8 売上支払入力 jyokou 20150505 str
        internal bool isRegistered;
        // 4935_8 売上支払入力 jyokou 20150505 end
        // 20151021 katen #13337 品名手入力に関する機能修正 start
        internal bool hasShow = false;
        // 20151021 katen #13337 品名手入力に関する機能修正 end

        /// <summary>
        /// モバイル連携DAO
        /// </summary>
        private IT_MOBISYO_RTDao mobisyoRtDao;
        private GET_SYSDATEDao dao;
        internal IM_SYS_INFODao sysInfoDao;

        // MAILAN #158993 START
        internal bool isTankaMessageShown = false;
        internal bool isCheckTankaFromChild = false;
        // MAILAN #158993 END

        //仕切書の明細合計金額表示用フラグ
        //品名内税or明細毎内税(品名税なし)の明細があれば、合計金額をブランクで表示
        internal int SHIKIRISHO_UR_UTIZEI = 0;
        internal int SHIKIRISHO_SH_UTIZEI = 0;
       
        #endregion

        #region 初期化処理
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

            // Accessor
            this.accessor = new Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Accessor.DBAccessor();
            this.commonAccesser = new Shougun.Core.Common.BusinessCommon.DBAccessor();

            // Utility
            this.controlUtil = new ControlUtility();

            /// 20141014 teikyou 「売上・支払入力画面」の休動Checkを追加する　start
            this.workclosedsharyouDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_SHARYOUDao>();
            this.workcloseduntenshaDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_UNTENSHADao>();
            this.workclosedhannyuusakiDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_HANNYUUSAKIDao>();
            /// 20141014 teikyou 「売上・支払入力画面」の休動Checkを追加する　end
            this.mobisyoRtDao = DaoInitUtility.GetComponent<IT_MOBISYO_RTDao>();

            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();

            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

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
                headerForm.logic = this;    // No.3822

                this.ChangeEnabledForInputControl(false);
                bool catchErr = false;

                // 月次処理中・月次締済み、請求・精算締済みチェックを行い締済みの場合は参照モードに切り替え
                if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                    || this.form.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
                {
                    DateTime getsujiShoriCheckDate = this.dto.entryEntity.DENPYOU_DATE.Value;
                    GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
                    // 月次処理中チェック
                    if (getsujiShoriCheckLogic.CheckGetsujiShoriChu(getsujiShoriCheckDate))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        string messageArg = string.Empty;
                        // メッセージ生成
                        if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                        {
                            messageArg = "修正";
                        }
                        else if (this.form.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
                        {
                            messageArg = "削除";
                        }
                        msgLogic.MessageBoxShow("E224", messageArg);

                        this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        this.form.HeaderFormInit();
                    }
                    // 月次処理ロックチェック
                    else if (getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(getsujiShoriCheckDate.Year.ToString()), short.Parse(getsujiShoriCheckDate.Month.ToString())))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        string messageArg = string.Empty;
                        // メッセージ生成
                        if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                        {
                            messageArg = "修正";
                        }
                        else if (this.form.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
                        {
                            messageArg = "削除";
                        }
                        msgLogic.MessageBoxShow("E222", messageArg);

                        this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        this.form.HeaderFormInit();
                    }
                    else if (CheckAllShimeStatus(out catchErr) && !catchErr)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        string messageArg = string.Empty;
                        // メッセージ生成
                        if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                        {
                            messageArg = "修正";
                        }
                        else if (this.form.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
                        {
                            messageArg = "削除";
                        }
                        msgLogic.MessageBoxShow("I011", messageArg);

                        this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        this.form.HeaderFormInit();
                    }
                    else if (catchErr)
                    {
                        return true;
                    }
                }

                // DisplayInitでTemplateを書き換えているので、データをセットする前に実行すること
                this.DisplayInit();

                // タブオーダーデータ取得
                GetStatus();   // No.3822

                this.EntryDataInit();

                foreach (var row in this.form.mrwDetail.Rows)
                {
                    // 単価と金額の活性制御
                    catchErr = this.form.SetIchranReadOnly(row.Index);
                    if (catchErr)
                    {
                        return true;
                    }
                }

                catchErr = this.NumberingRowNo();
                if (catchErr)
                {
                    return true;
                }

                Control[] copy = new Control[this.form.allControl.GetLength(0) + 1];

                this.form.allControl.CopyTo(copy, 0);
                copy[this.form.allControl.GetLength(0)] = this.headerForm.KYOTEN_CD;
                this.form.allControl = copy;


                if (this.form.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG) ||
                    this.form.WindowType.Equals(WINDOW_TYPE.REFERENCE_WINDOW_FLAG))
                {
                    // 削除モード時には全コントロールをReadOnlyにする
                    this.ChangeEnabledForInputControl(true);
                    this.form.SHARYOU_NAME_RYAKU.Enabled = false;
                }
                catchErr = false;
                if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    // 売上消費税率設定
                    catchErr = this.SetUriageShouhizeiRate();
                    if (catchErr)
                    {
                        return true;
                    }
                    catchErr = this.SetShiharaiShouhizeiRate();
                    if (catchErr)
                    {
                        return true;
                    }

                    // 新規で受付番号がセットされている場合
                    if (this.form.UketukeNumber > -1)
                    {
                        this.form.UKETSUKE_NUMBER.Text = this.form.UketukeNumber.ToString();
                        catchErr = this.GetUketsukeNumber();
                        if (catchErr)
                        {
                            return true;
                        }
                    }
                    // 20140512 kayo No.679 計量番号連携 start
                    else if (this.form.KeiryouNumber > -1)
                    {
                        this.form.KEIRYOU_NUMBER.Text = this.form.KeiryouNumber.ToString();
                        catchErr = this.GetKeiryouNumber();
                        if (catchErr)
                        {
                            return true;
                        }
                        this.form.KeiryouNumber = -1;   // 一回呼んだら初期化しておく
                    }
                    // 20140512 kayo No.679 計量番号連携 end
                }

                // 伝票発行ポップアップDTO初期化
                this.form.denpyouHakouPopUpDTO = new DenpyouHakou.ParameterDTOClass();

                setSearchButtonInfo();

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
                    else
                    {
                        // デザインが初期化されないのでここで初期化
                        this.ChangeShokuchiSharyouDesign(false);
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

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInit", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("WindowInit", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        // No.3822-->
        public bool LocalDataInit()
        {
            try
            {
                this.tmpTorihikisakiCd = string.Empty;
                this.tmpGyousyaCd = string.Empty;
                this.tmpNizumiGyoushaCd = string.Empty;
                this.tmpNizumiGenbaCd = string.Empty;
                this.tmpNioroshiGyoushaCd = string.Empty;
                this.tmpNioroshiGenbaCd = string.Empty;
                this.tmpUnpanGyoushaCd = string.Empty;
                this.tmpGenbaCd = string.Empty;
                this.tmpKeitaiKbnCd = string.Empty;
                this.tmpUntenshaCd = string.Empty;
                this.sharyouCd = string.Empty;
                this.unpanGyousha = string.Empty;
                this.form.SHARYOU_CD.BackColor = SystemColors.Window;    // No.3822 色が残るので戻す
                this.dto.contenaResultList = new List<T_CONTENA_RESULT>();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LocalDataInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }
        // No.3822<--

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

                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        this.footerForm.bt_func2.Enabled = true;    // No.2818
                        this.footerForm.bt_func3.Enabled = false;
                        this.footerForm.bt_func4.Enabled = false;
                        this.footerForm.bt_func6.Enabled = true;
                        this.footerForm.bt_func7.Enabled = true;
                        this.footerForm.bt_func8.Enabled = false;
                        this.footerForm.bt_func9.Enabled = true;
                        this.footerForm.bt_func10.Enabled = true;
                        this.footerForm.bt_func11.Enabled = true;
                        this.footerForm.bt_func12.Enabled = true;
                        // 4935_8 売上支払入力 jyokou 20150505 str
                        //this.footerForm.bt_process1.Enabled = false;
                        this.footerForm.bt_process1.Enabled = true;
                        // 4935_8 売上支払入力 jyokou 20150505 end
                        this.footerForm.bt_process2.Enabled = true;
                        this.footerForm.bt_process3.Enabled = true;
                        //this.footerForm.bt_process4.Enabled = true;
                        this.footerForm.bt_process5.Enabled = false;
                        break;
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 修正モードの場合、登録後に初めて活性化するボタンがあるので制御
                        if (registeredFlag)
                        {
                            // 登録後処理
                            this.footerForm.bt_func2.Enabled = true;
                            this.footerForm.bt_func3.Enabled = true;
                            this.footerForm.bt_func4.Enabled = false;
                            this.footerForm.bt_func6.Enabled = false;
                            this.footerForm.bt_func7.Enabled = true;
                            this.footerForm.bt_func8.Enabled = false;
                            this.footerForm.bt_func9.Enabled = false;
                            this.footerForm.bt_func10.Enabled = false;
                            this.footerForm.bt_func11.Enabled = false;
                            this.footerForm.bt_func12.Enabled = true;
                            this.footerForm.bt_process1.Enabled = true;
                            this.footerForm.bt_process2.Enabled = true;
                            this.footerForm.bt_process3.Enabled = false;
                            //this.footerForm.bt_process4.Enabled = true;
                            this.footerForm.bt_process5.Enabled = false;
                        }
                        else
                        {
                            this.footerForm.bt_func2.Enabled = true; // No.2818
                            this.footerForm.bt_func3.Enabled = false;
                            this.footerForm.bt_func4.Enabled = false;
                            this.footerForm.bt_func6.Enabled = true;
                            this.footerForm.bt_func7.Enabled = true;
                            this.footerForm.bt_func8.Enabled = false;
                            this.footerForm.bt_func9.Enabled = true;
                            this.footerForm.bt_func10.Enabled = true;
                            this.footerForm.bt_func11.Enabled = true;
                            this.footerForm.bt_func12.Enabled = true;
                            this.footerForm.bt_process1.Enabled = true;
                            this.footerForm.bt_process2.Enabled = true;
                            this.footerForm.bt_process3.Enabled = true;
                            //this.footerForm.bt_process4.Enabled = true;
                            this.footerForm.bt_process5.Enabled = false;
                        }
                        break;
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        this.footerForm.bt_func2.Enabled = false;
                        this.footerForm.bt_func3.Enabled = false;
                        this.footerForm.bt_func4.Enabled = false;
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
                        //this.footerForm.bt_process4.Enabled = false;
                        this.footerForm.bt_process5.Enabled = false;
                        break;
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        this.footerForm.bt_func1.Enabled = false;
                        this.footerForm.bt_func2.Enabled = true; // No.2818
                        this.footerForm.bt_func3.Enabled = false;
                        this.footerForm.bt_func4.Enabled = false;
                        this.footerForm.bt_func5.Enabled = false;
                        this.footerForm.bt_func6.Enabled = true;
                        this.footerForm.bt_func7.Enabled = true;
                        this.footerForm.bt_func8.Enabled = false;
                        this.footerForm.bt_func9.Enabled = false;
                        this.footerForm.bt_func10.Enabled = false;
                        this.footerForm.bt_func11.Enabled = false;
                        this.footerForm.bt_func12.Enabled = true;
                        this.footerForm.bt_process1.Enabled = true;
                        this.footerForm.bt_process2.Enabled = false;
                        this.footerForm.bt_process3.Enabled = false;
                        //this.footerForm.bt_process4.Enabled = false;
                        this.footerForm.bt_process5.Enabled = false;
                        break;
                    default:
                        break;
                }

                // 非活性ボタンの名称が表示されるためクリア
                //foreach (var button in buttonSetting)
                //{
                //    var cont = controlUtil.FindControl(footerForm, button.ButtonName);
                //    if (!cont.Enabled) cont.Text = string.Empty;
                //}

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }


        /// <summary>
        /// イベント初期化処理
        /// </summary>
        internal bool EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 新規ボタン(F2)イベント
                footerForm.bt_func2.Click += new EventHandler(this.form.ChangeNewWindow);

                // 修正ボタン(F3)イベント
                footerForm.bt_func3.Click += new EventHandler(this.form.ChangeUpdateWindow);

                //受付伝票(F4)イベント
                footerForm.bt_func4.Click += new EventHandler(this.form.UketsukeDenpyo);

                //コンテナ(F6)イベント
                footerForm.bt_func6.Click += new EventHandler(this.form.ContenaWindow);

                // 一覧ボタン(F7)イベント
                footerForm.bt_func7.Click += new EventHandler(this.form.ShowDenpyouIchiran);

                // 登録ボタン(F9)イベント
                footerForm.bt_func9.Click += new EventHandler(this.form.Regist);
                footerForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                // 行挿入ボタン(F10)イベント
                footerForm.bt_func10.Click += new EventHandler(this.form.AddRow);

                // 行挿入ボタン(F11)イベント
                footerForm.bt_func11.Click += new EventHandler(this.form.RemoveRow);

                // 閉じるボタン(F12)イベント生成
                footerForm.bt_func12.Click += new EventHandler(this.form.FormClose);

                //プロセスボタンイベント生成
                footerForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);
                footerForm.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);
                footerForm.bt_process3.Click += new EventHandler(this.form.bt_process3_Click);
                //footerForm.bt_process4.Click += new EventHandler(this.form.bt_process4_Click);

                // コントロールのイベント
                this.form.TORIHIKISAKI_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.GYOUSHA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.GENBA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.NIOROSHI_GYOUSHA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.NIOROSHI_GENBA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.UNPAN_GYOUSHA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.SHARYOU_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);

                // HeaderForm
                // 拠点CDイベント
                //this.headerForm.KYOTEN_CD.Validated += new EventHandler(this.form.KYOTEN_CD_OnValidated);

                //20151026 hoanghm #13404 start
                // 全てのコントロールのEnterイベントに追加
                foreach (Control ctrl in this.form.Controls)
                {
                    ctrl.Enter -= new EventHandler(this.GetControlEnter);
                    ctrl.Enter += new EventHandler(this.GetControlEnter);

                    ctrl.LostFocus -= new EventHandler(this.CheckImeStatus);
                    ctrl.LostFocus += new EventHandler(this.CheckImeStatus);
                }
                foreach (Control ctrl in this.headerForm.Controls)
                {
                    ctrl.Enter -= new EventHandler(this.GetControlEnter);
                    ctrl.Enter += new EventHandler(this.GetControlEnter);
                }
                //20151026 hoanghm #13404 end

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

		#region 20151026 hoanghm #13404
        /// <summary>
        /// 全コントロールのEnterイベントで必ず通る
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetControlEnter(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;

            if ((ctrl is TextBox || ctrl is GrapeCity.Win.MultiRow.GcMultiRow))
            {
                this.form.beforbeforControlName = this.form.beforControlName;
                this.form.beforControlName = ctrl.Name;
            }
        }
		#endregion

        #region IME制御
        private void CheckImeStatus(object sender, EventArgs e)
        {
            if (this.form.ParentBaseForm.imeStatus.IsConversion)
            {
                Control ctrl = (Control)sender;
                this.form.ParentBaseForm.imeStatus.ReleaseImeMode(ctrl.Handle);
                this.form.imeStatus.ChengeIsConversion(false);
            }
            else
            {
                return;
            }
        }
        #endregion

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

            this.InitShouhizeiRatePopupSetting();

            this.form.Enabled = true;
            this.headerForm.Enabled = true;
            this.form.ENTRY_NUMBER.ReadOnly = false;
            this.form.RENBAN.ReadOnly = false;
            this.form.UKETSUKE_NUMBER.ReadOnly = false;

            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    break;

                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    this.form.ENTRY_NUMBER.ReadOnly = true;
                    this.form.RENBAN.ReadOnly = true;
                    this.form.UKETSUKE_NUMBER.ReadOnly = true;
                    break;

                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    //this.form.Enabled = false;
                    //this.headerForm.Enabled = false;
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
                this.form.NYUURYOKU_TANTOUSHA_CD.RegistCheckMethod = null;
                this.form.URIAGE_DATE.RegistCheckMethod = null;
                this.form.SHIHARAI_DATE.RegistCheckMethod = null;
                this.form.KAKUTEI_KBN.RegistCheckMethod = null;
                this.form.URIAGE_SHOUHIZEI_RATE_VALUE.ReadOnly = true;
                this.form.URIAGE_SHOUHIZEI_RATE_VALUE.RegistCheckMethod = null;
                this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.ReadOnly = true;
                this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.RegistCheckMethod = null;

                // Detail
                this.form.mrwDetail.SuspendLayout();

                foreach (var o in this.form.mrwDetail.Rows)
                {
                    var obj2 = controlUtil.FindControl(o.Cells.ToArray(), new string[] { CELL_NAME_HINMEI_CD, CELL_NAME_HINMEI_NAME, CELL_NAME_HINMEI_NAME, CELL_NAME_SUURYOU, CELL_NAME_UNIT_CD, CELL_NAME_KINGAKU, CELL_NAME_URIAGESHIHARAI_DATE });
                    foreach (var target in obj2)
                    {
                        PropertyUtility.SetValue(target, "RegistCheckMethod", null);
                    }
                }

                this.form.mrwDetail.ResumeLayout();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RequiredSettingInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 必須チェックの設定を動的に生成
        /// </summary>
        internal bool SetRequiredSetting()
        {
            try
            {
                // 設定
                SelectCheckDto existCheck = new SelectCheckDto();
                existCheck.CheckMethodName = "必須チェック";
                Collection<SelectCheckDto> existChecks = new Collection<SelectCheckDto>();
                existChecks.Add(existCheck);

                // 売上日付、支払日付は動的に必須チェックが変わるため、初期カラーに戻す
                // もし、画面独自に色の制御をしていたら以下の処理も変更すること。
                this.form.NYUURYOKU_TANTOUSHA_CD.RegistCheckMethod = existChecks;
                this.form.URIAGE_DATE.IsInputErrorOccured = false;
                this.form.URIAGE_DATE.UpdateBackColor();
                this.form.SHIHARAI_DATE.IsInputErrorOccured = false;
                this.form.SHIHARAI_DATE.UpdateBackColor();

                this.form.KAKUTEI_KBN.RegistCheckMethod = existChecks;
                short kakuteiKbn = 0;
                if (!string.IsNullOrEmpty(this.form.KAKUTEI_KBN.Text))
                {
                    short.TryParse(this.form.KAKUTEI_KBN.Text, out kakuteiKbn);
                }
                if (this.form.URIAGE_DATE.Visible)
                {
                    if (kakuteiKbn == SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI)
                    {
                        // 伝票毎締めの場合
                        if (GetRowsDenpyouKbnCdMixed() != SHIHARAI_ONLY)
                        {
                            // 明細行に、伝票区分が「支払」以外の行も存在する場合
                            this.form.URIAGE_DATE.RegistCheckMethod = existChecks;
                            // 必須チェックのため一時的にReadOnlyをはずす
                            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.ReadOnly = false;
                            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.RegistCheckMethod = existChecks;
                        }
                    }
                }
                if (this.form.SHIHARAI_DATE.Visible)
                {
                    if (kakuteiKbn == SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI)
                    {
                        // 伝票毎締めの場合
                        if (GetRowsDenpyouKbnCdMixed() != URIAGE_ONLY)
                        {
                            // 明細行に、伝票区分が「売上」以外の行も存在する場合
                            this.form.SHIHARAI_DATE.RegistCheckMethod = existChecks;
                            // 必須チェックのため一時的にReadOnlyをはずす
                            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.ReadOnly = false;
                            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.RegistCheckMethod = existChecks;
                        }
                    }
                }

                this.form.mrwDetail.SuspendLayout();

                foreach (var o in this.form.mrwDetail.Rows)
                {
                    string[] registCheckTarget;
                    if (this.dto.sysInfoEntity.UR_SH_KAKUTEI_USE_KBN == SalesPaymentConstans.UR_SH_KAKUTEI_USE_KBN_YES)
                    {
                        // 売上支払日付は動的に必須チェックが変わるため、初期カラーに戻す
                        // もし、画面独自に色の制御をしていたら以下の処理も変更すること。
                        var urShDate = o.Cells[CELL_NAME_URIAGESHIHARAI_DATE];
                        if (urShDate != null && urShDate.Visible)
                        {
                            var cell = urShDate as ICustomAutoChangeBackColor;
                            cell.IsInputErrorOccured = false;
                            cell.UpdateBackColor();
                        }

                        var kakuteiKbnCell = o.Cells[CELL_NAME_KAKUTEI_KBN];
                        if (kakuteiKbnCell != null && kakuteiKbnCell.Value != null && (bool)kakuteiKbnCell.Value)
                        {
                            registCheckTarget = new string[] { CELL_NAME_HINMEI_CD, CELL_NAME_HINMEI_NAME, CELL_NAME_HINMEI_NAME, CELL_NAME_SUURYOU, CELL_NAME_UNIT_CD, CELL_NAME_KINGAKU, CELL_NAME_URIAGESHIHARAI_DATE };
                        }
                        else
                        {
                            registCheckTarget = new string[] { CELL_NAME_HINMEI_CD, CELL_NAME_HINMEI_NAME, CELL_NAME_HINMEI_NAME, CELL_NAME_SUURYOU, CELL_NAME_UNIT_CD, CELL_NAME_KINGAKU };
                        }
                    }
                    else
                    {
                        registCheckTarget = new string[] { CELL_NAME_HINMEI_CD, CELL_NAME_HINMEI_NAME, CELL_NAME_HINMEI_NAME, CELL_NAME_SUURYOU, CELL_NAME_UNIT_CD, CELL_NAME_KINGAKU, CELL_NAME_URIAGESHIHARAI_DATE };
                    }

                    var obj2 = controlUtil.FindControl(o.Cells.ToArray(), registCheckTarget);
                    foreach (var target in obj2)
                    {
                        var visible = target.GetType().GetProperty("Visible");
                        if (visible != null && (bool)visible.GetValue(target, null))
                        {
                            PropertyUtility.SetValue(target, "RegistCheckMethod", existChecks);
                        }
                    }
                }

                this.form.mrwDetail.ResumeLayout();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetRequiredSetting", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 画面のT_UR_SH_ENTRY部分の初期化処理
        /// </summary>
        internal void EntryDataInit()
        {
            // DBには無い値などの設定
            denpyouKbnDictionary.Clear();
            unitDictionary.Clear();

            var denpyous = this.accessor.GetAllDenpyouKbn();
            var units = this.accessor.GetAllUnit();

            foreach (var denpyou in denpyous)
            {
                denpyouKbnDictionary.Add((short)denpyou.DENPYOU_KBN_CD, denpyou);
            }

            foreach (var unit in units)
            {
                unitDictionary.Add((short)unit.UNIT_CD, unit);
            }

            SqlInt32 renbanValue = -1;
            // 画面毎に設定が異なるコントロールの初期化(コピペしやすいようにするため)
            // 受付番号
            this.form.ENTRY_NUMBER.DBFieldsName = "UR_SH_NUMBER";
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
            if (!this.form.isRegisted)
            {
                this.form.DENPYOU_DATE.Value = this.footerForm.sysDate;
                this.form.URIAGE_DATE.Value = null;
                this.form.SHIHARAI_DATE.Value = null;
            }

            long systemId = -1;
            int seq = -1;
            if (!this.dto.entryEntity.SYSTEM_ID.IsNull) systemId = (long)this.dto.entryEntity.SYSTEM_ID;
            if (!this.dto.entryEntity.SEQ.IsNull) seq = (int)this.dto.entryEntity.SEQ;

            // 締処理状況判定用データ取得
            DataTable seikyuuData = this.accessor.GetSeikyuMeisaiData(systemId, seq, -1, this.dto.entryEntity.TORIHIKISAKI_CD);
            DataTable seisanData = this.accessor.GetSeisanMeisaiData(systemId, seq, -1, this.dto.entryEntity.TORIHIKISAKI_CD);

            // システム設定の確定利用区分と確定単位区分による初期表示
            if (this.dto.sysInfoEntity.UR_SH_KAKUTEI_USE_KBN == SalesPaymentConstans.UR_SH_KAKUTEI_USE_KBN_YES)
            {
                if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_DENPYOU)
                {
                    // 確定フラグ
                    this.form.KAKUTEI_KBN_LABEL.Visible = true;
                    this.form.KAKUTEI_KBN.Visible = true;
                    this.form.KAKUTEI_KBN_NAME.Visible = true;

                    // 売上日付
                    this.form.URIAGE_DATE_LABEL.Visible = true;
                    this.form.URIAGE_DATE.Visible = true;

                    // 売上消費税
                    this.form.URIAGE_SHOUHIZEI_RATE_LABEL.Visible = true;
                    this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Visible = true;
                    this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = true;

                    // 支払日付
                    this.form.SHIHARAI_DATE_LABEL.Visible = true;
                    this.form.SHIHARAI_DATE.Visible = true;

                    // 支払消費税
                    this.form.SHIHARAI_SHOUHIZEI_RATE_LABEL.Visible = true;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Visible = true;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = true;

                    // 売上締処理状況
                    this.form.SHIMESHORI_JOUKYOU_URIAGE_LABEL.Visible = true;
                    this.form.SHIMESHORI_JOUKYOU_URIAGE.Visible = true;

                    // 支払締処理状況
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI_LABEL.Visible = true;
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Visible = true;

                    // 明細
                    this.ChangePropertyForGC(new string[] { "clmHeaderKakuteiKbn", "clmHeaderJoukyou", "clmHeaderUrshDate" }, new string[] { "KAKUTEI_KBN", "JOUKYOU", "URIAGESHIHARAI_DATE" }, "Visible", false);
                }
                else
                {
                    // 確定フラグ
                    this.form.KAKUTEI_KBN_LABEL.Visible = false;
                    this.form.KAKUTEI_KBN.Visible = false;
                    this.form.KAKUTEI_KBN_NAME.Visible = false;

                    // 売上日付
                    this.form.URIAGE_DATE_LABEL.Visible = false;
                    this.form.URIAGE_DATE.Visible = false;

                    // 売上消費税
                    this.form.URIAGE_SHOUHIZEI_RATE_LABEL.Visible = false;
                    this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Visible = false;
                    this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = false;

                    // 支払日付
                    this.form.SHIHARAI_DATE_LABEL.Visible = false;
                    this.form.SHIHARAI_DATE.Visible = false;

                    // 支払消費税
                    this.form.SHIHARAI_SHOUHIZEI_RATE_LABEL.Visible = false;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Visible = false;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = false;

                    // 売上締処理状況
                    this.form.SHIMESHORI_JOUKYOU_URIAGE_LABEL.Visible = false;
                    this.form.SHIMESHORI_JOUKYOU_URIAGE.Visible = false;

                    // 支払締処理状況
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI_LABEL.Visible = false;
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Visible = false;

                    // 明細
                    this.ChangePropertyForGC(new string[] { "clmHeaderKakuteiKbn", "clmHeaderJoukyou", "clmHeaderUrshDate" }, new string[] { "KAKUTEI_KBN", "JOUKYOU", "URIAGESHIHARAI_DATE" }, "Visible", true);
                }
            }
            else if (this.dto.sysInfoEntity.UR_SH_KAKUTEI_USE_KBN == SalesPaymentConstans.UR_SH_KAKUTEI_USE_KBN_NO)
            {
                // 確定フラグ
                this.form.KAKUTEI_KBN_LABEL.Visible = false;
                this.form.KAKUTEI_KBN.Visible = false;
                this.form.KAKUTEI_KBN_NAME.Visible = false;

                if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_DENPYOU)
                {
                    // 売上日付
                    this.form.URIAGE_DATE_LABEL.Visible = true;
                    this.form.URIAGE_DATE.Visible = true;

                    // 売上消費税
                    this.form.URIAGE_SHOUHIZEI_RATE_LABEL.Visible = true;
                    this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Visible = true;
                    this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = true;

                    // 支払日付
                    this.form.SHIHARAI_DATE_LABEL.Visible = true;
                    this.form.SHIHARAI_DATE.Visible = true;

                    // 支払消費税
                    this.form.SHIHARAI_SHOUHIZEI_RATE_LABEL.Visible = true;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Visible = true;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = true;

                    // 売上締処理状況
                    this.form.SHIMESHORI_JOUKYOU_URIAGE_LABEL.Visible = true;
                    this.form.SHIMESHORI_JOUKYOU_URIAGE.Visible = true;

                    // 支払締処理状況
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI_LABEL.Visible = true;
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Visible = true;

                    // 明細
                    this.ChangePropertyForGC(new string[] { "clmHeaderKakuteiKbn", "clmHeaderJoukyou", "clmHeaderUrshDate" }, new string[] { "KAKUTEI_KBN", "JOUKYOU", "URIAGESHIHARAI_DATE" }, "Visible", false);
                }
                else
                {
                    // 売上日付
                    this.form.URIAGE_DATE_LABEL.Visible = false;
                    this.form.URIAGE_DATE.Visible = false;

                    // 売上消費税
                    this.form.URIAGE_SHOUHIZEI_RATE_LABEL.Visible = false;
                    this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Visible = false;
                    this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = false;

                    // 支払日付
                    this.form.SHIHARAI_DATE_LABEL.Visible = false;
                    this.form.SHIHARAI_DATE.Visible = false;

                    // 支払消費税
                    this.form.SHIHARAI_SHOUHIZEI_RATE_LABEL.Visible = false;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Visible = false;
                    this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.Visible = false;

                    // 売上締処理状況
                    this.form.SHIMESHORI_JOUKYOU_URIAGE_LABEL.Visible = false;
                    this.form.SHIMESHORI_JOUKYOU_URIAGE.Visible = false;

                    // 支払締処理状況
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI_LABEL.Visible = false;
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Visible = false;

                    // 明細
                    this.ChangePropertyForGC(new string[] { "clmHeaderKakuteiKbn", "clmHeaderJoukyou", "clmHeaderUrshDate" }, new string[] { "KAKUTEI_KBN", "JOUKYOU", "URIAGESHIHARAI_DATE" }, "Visible", true);

                    // 明細の確定区分のみ改めて非表示
                    this.form.mrwDetail.SuspendLayout();
                    var newTemplate = this.form.mrwDetail.Template;
                    var obj1 = controlUtil.FindControl(newTemplate.ColumnHeaders[0].Cells.ToArray(), new string[] { "clmHeaderKakuteiKbn" });
                    foreach (var o in obj1)
                    {
                        PropertyUtility.SetValue(o, "Value", string.Empty);
                    }
                    var obj2 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { "KAKUTEI_KBN" });
                    foreach (var o in obj2)
                    {
                        PropertyUtility.SetValue(o, "Visible", false);
                    }
                    this.form.mrwDetail.Template = newTemplate;
                    this.form.mrwDetail.ResumeLayout();
                }
            }

            // 数値の表示形式初期化
            CalcValueFormatSettingInit();

            // マニ登録形態区分
            if (this.dto.sysInfoEntity.SYS_MANI_KEITAI_KBN == SalesPaymentConstans.SYS_MANI_KEITAI_KBN_DENPYOU)
            {
                this.ChangePropertyForGC(new string[] { "clmHeaderManifestId" }, new string[] { "MANIFEST_ID" }, "Visible", false);
            }
            else
            {
                this.ChangePropertyForGC(new string[] { "clmHeaderManifestId" }, new string[] { "MANIFEST_ID" }, "Visible", true);
            }

            //入力中のデータを初期化する。
            Clear();

            // 形態区分の初期値設定
            SqlInt16 KeitaiKbnCd = this.accessor.GetKeitaiKbnCd(SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI);
            if (KeitaiKbnCd > 0)
            {
                this.form.KEITAI_KBN_CD.Text = KeitaiKbnCd.ToString();
                this.form.KEITAI_KBN_NAME_RYAKU.Text = this.accessor.GetKeitaiKbnNameRyaku(KeitaiKbnCd);
            }
            else
            {
                this.form.KEITAI_KBN_CD.Text = string.Empty;
                this.form.KEITAI_KBN_NAME_RYAKU.Text = string.Empty;
            }

            // 領収書番号の初期化
            if (this.dto.sysInfoEntity.SYS_RECEIPT_RENBAN_HOUHOU_KBN == 1)
            {
                this.form.RECEIPT_NUMBER_LABEL.Text = "領収書番号(日連番)";
            }
            else
            {
                this.form.RECEIPT_NUMBER_LABEL.Text = "領収書番号(年連番)";
            }

            // モードによる制御
            if (this.IsRequireData())
            {
                // ヘッダー Start
                // 拠点
                if (!this.dto.entryEntity.KYOTEN_CD.IsNull)
                {
                    headerForm.KYOTEN_CD.Text = string.Format("{0:00}", (int)this.dto.entryEntity.KYOTEN_CD);
                }
                if (!string.IsNullOrEmpty(this.dto.kyotenEntity.KYOTEN_NAME_RYAKU))
                {
                    headerForm.KYOTEN_NAME_RYAKU.Text = this.dto.kyotenEntity.KYOTEN_NAME_RYAKU.ToString();
                }
                // ヘッダー End

                // 詳細 Start
                this.form.ENTRY_NUMBER.Text = this.dto.entryEntity.UR_SH_NUMBER.ToString();
                // 連番
                if (!renbanValue.IsNull)
                {
                    this.form.RENBAN.Text = renbanValue.ToString();
                }
                else
                {
                    this.form.RENBAN.Text = "";
                }
                // 確定区分
                if (this.dto.sysInfoEntity.UR_SH_KAKUTEI_USE_KBN == SalesPaymentConstans.UR_SH_KAKUTEI_USE_KBN_YES)
                {
                    // 新規でも複写時は確定区分のセットが必要
                    if (!WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType) || (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType) && this.form.UrShNumber != -1) && !this.dto.entryEntity.KAKUTEI_KBN.IsNull)
                    {
                        this.form.KAKUTEI_KBN.Text = this.dto.entryEntity.KAKUTEI_KBN.ToString();
                    }
                }

                // 確定区分
                if (this.dto.sysInfoEntity.UR_SH_KAKUTEI_USE_KBN == SalesPaymentConstans.UR_SH_KAKUTEI_USE_KBN_YES)
                {
                    if (this.dto.entryEntity.KAKUTEI_KBN == SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI)
                    {
                        // 確定名
                        this.form.KAKUTEI_KBN_NAME.Text = SalesPaymentConstans.GetKakuteiKbnName(SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI);
                    }
                    else if (this.dto.entryEntity.KAKUTEI_KBN == SalesPaymentConstans.KAKUTEI_KBN_MIKAKUTEI)
                    {
                        // 確定名
                        this.form.KAKUTEI_KBN_NAME.Text = SalesPaymentConstans.GetKakuteiKbnName(SalesPaymentConstans.KAKUTEI_KBN_MIKAKUTEI);
                    }
                }

                // 受付番号
                if (!WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType))
                {
                    if (!this.dto.entryEntity.UKETSUKE_NUMBER.IsNull)
                    {
                        this.form.UKETSUKE_NUMBER.Text = this.dto.entryEntity.UKETSUKE_NUMBER.ToString();
                    }
                }
                // 20140512 kayo No.679 計量番号連携 start
                // TODO
                // 計量番号
                //                if (!this.dto.entryEntity.KEIRYOU_NUMBER.IsNull)
                //                {
                //                    this.form.KEIRYOU_NUMBER.Text = this.dto.entryEntity.KEIRYOU_NUMBER.ToString();
                //               }
                // 20140512 kayo No.679 計量番号連携 end
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
                        this.form.NYUURYOKU_TANTOUSHA_CD.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_CD.ToString();
                        this.form.NYUURYOKU_TANTOUSHA_NAME.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME_RYAKU.ToString();
                        strNyuryokuTantousyaName = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME.ToString();    // No.3279
                    }
                    else
                    {
                        this.form.NYUURYOKU_TANTOUSHA_CD.Text = string.Empty;
                        this.form.NYUURYOKU_TANTOUSHA_NAME.Text = string.Empty;
                        strNyuryokuTantousyaName = string.Empty;  // No.3279
                    }
                }
                else
                {
                    this.form.NYUURYOKU_TANTOUSHA_CD.Text = this.dto.entryEntity.NYUURYOKU_TANTOUSHA_CD;
                    this.form.NYUURYOKU_TANTOUSHA_NAME.Text = this.dto.entryEntity.NYUURYOKU_TANTOUSHA_NAME;
                    strNyuryokuTantousyaName = this.dto.entryEntity.NYUURYOKU_TANTOUSHA_NAME;
                }

                // 売上日付
                if (!this.dto.entryEntity.URIAGE_DATE.IsNull
                    && !string.IsNullOrEmpty(this.dto.entryEntity.URIAGE_DATE.ToString()))
                {
                    this.form.URIAGE_DATE.Value = (DateTime)this.dto.entryEntity.URIAGE_DATE;
                }
                else
                {
                    this.form.URIAGE_DATE.Value = string.Empty;
                }

                // 支払日付
                if (!this.dto.entryEntity.SHIHARAI_DATE.IsNull
                    && !string.IsNullOrEmpty(this.dto.entryEntity.SHIHARAI_DATE.ToString()))
                {
                    this.form.SHIHARAI_DATE.Value = (DateTime)this.dto.entryEntity.SHIHARAI_DATE;
                }
                else
                {
                    this.form.SHIHARAI_DATE.Value = string.Empty;
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

                // 車輌
                if (!string.IsNullOrEmpty(this.dto.entryEntity.SHARYOU_CD))
                {
                    this.form.SHARYOU_CD.Text = this.dto.entryEntity.SHARYOU_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.SHARYOU_NAME))
                {
                    this.form.SHARYOU_NAME_RYAKU.Text = this.dto.entryEntity.SHARYOU_NAME.ToString();
                }

                // 取引先
                if (!string.IsNullOrEmpty(this.dto.entryEntity.TORIHIKISAKI_CD))
                {
                    this.form.TORIHIKISAKI_CD.Text = this.dto.entryEntity.TORIHIKISAKI_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.TORIHIKISAKI_NAME))
                {
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = this.dto.entryEntity.TORIHIKISAKI_NAME.ToString();
                }
                // 車種
                if (!string.IsNullOrEmpty(this.dto.entryEntity.SHASHU_CD))
                {
                    this.form.SHASHU_CD.Text = this.dto.entryEntity.SHASHU_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.SHASHU_NAME))
                {
                    this.form.SHASHU_NAME.Text = this.dto.entryEntity.SHASHU_NAME.ToString();
                }
                // 売上締日
                if (!this.dto.torihikisakiSeikyuuEntity.SHIMEBI1.IsNull)
                {
                    this.form.SEIKYUU_SHIMEBI1.Text = this.dto.torihikisakiSeikyuuEntity.SHIMEBI1.ToString();
                }
                if (!this.dto.torihikisakiSeikyuuEntity.SHIMEBI2.IsNull)
                {
                    this.form.SEIKYUU_SHIMEBI2.Text = this.dto.torihikisakiSeikyuuEntity.SHIMEBI2.ToString();
                }
                if (!this.dto.torihikisakiSeikyuuEntity.SHIMEBI3.IsNull)
                {
                    this.form.SEIKYUU_SHIMEBI3.Text = this.dto.torihikisakiSeikyuuEntity.SHIMEBI3.ToString();
                }
                // 支払締日
                if (!this.dto.torihikisakiShiharaiEntity.SHIMEBI1.IsNull)
                {
                    this.form.SHIHARAI_SHIMEBI1.Text = this.dto.torihikisakiShiharaiEntity.SHIMEBI1.ToString();
                }
                if (!this.dto.torihikisakiShiharaiEntity.SHIMEBI2.IsNull)
                {
                    this.form.SHIHARAI_SHIMEBI2.Text = this.dto.torihikisakiShiharaiEntity.SHIMEBI2.ToString();
                }
                if (!this.dto.torihikisakiShiharaiEntity.SHIMEBI3.IsNull)
                {
                    this.form.SHIHARAI_SHIMEBI3.Text = this.dto.torihikisakiShiharaiEntity.SHIMEBI3.ToString();
                }
                // 運搬業者
                if (!string.IsNullOrEmpty(this.dto.entryEntity.UNPAN_GYOUSHA_CD))
                {
                    this.form.UNPAN_GYOUSHA_CD.Text = this.dto.entryEntity.UNPAN_GYOUSHA_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.UNPAN_GYOUSHA_NAME))
                {
                    this.form.UNPAN_GYOUSHA_NAME.Text = this.dto.entryEntity.UNPAN_GYOUSHA_NAME.ToString();
                }
                // 業者
                if (!string.IsNullOrEmpty(this.dto.entryEntity.GYOUSHA_CD))
                {
                    this.form.GYOUSHA_CD.Text = this.dto.entryEntity.GYOUSHA_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.GYOUSHA_NAME))
                {
                    this.form.GYOUSHA_NAME_RYAKU.Text = this.dto.entryEntity.GYOUSHA_NAME.ToString();
                }
                // 運転者
                if (!string.IsNullOrEmpty(this.dto.entryEntity.UNTENSHA_CD))
                {
                    this.form.UNTENSHA_CD.Text = this.dto.entryEntity.UNTENSHA_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.UNTENSHA_NAME))
                {
                    this.form.UNTENSHA_NAME.Text = this.dto.entryEntity.UNTENSHA_NAME.ToString();
                }
                // 人数
                if (!this.dto.entryEntity.NINZUU_CNT.IsNull)
                {
                    this.form.NINZUU_CNT.Text = this.dto.entryEntity.NINZUU_CNT.ToString();

                }
                // 現場
                if (!string.IsNullOrEmpty(this.dto.entryEntity.GENBA_CD))
                {
                    this.form.GENBA_CD.Text = this.dto.entryEntity.GENBA_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.GENBA_NAME))
                {
                    this.form.GENBA_NAME_RYAKU.Text = this.dto.entryEntity.GENBA_NAME.ToString();
                }
                // 形態区分
                if (!this.dto.entryEntity.KEITAI_KBN_CD.IsNull)
                {
                    this.form.KEITAI_KBN_CD.Text = string.Format("{0:00}", (int)this.dto.entryEntity.KEITAI_KBN_CD);
                }
                // 形態区分名
                if (!string.IsNullOrEmpty(this.dto.keitaiKbnEntity.KEITAI_KBN_NAME_RYAKU))
                {
                    this.form.KEITAI_KBN_NAME_RYAKU.Text = this.dto.keitaiKbnEntity.KEITAI_KBN_NAME_RYAKU;
                }
                // コンテナ
                if (!this.dto.contenaEntity.CONTENA_JOUKYOU_CD.IsNull)
                {
                    this.form.CONTENA_SOUSA_CD.Text = string.Format("{0:00}", (int)this.dto.contenaEntity.CONTENA_JOUKYOU_CD);
                }
                if (!string.IsNullOrEmpty(this.dto.contenaEntity.CONTENA_JOUKYOU_NAME_RYAKU))
                {
                    this.form.CONTENA_JOUKYOU_NAME_RYAKU.Text = this.dto.contenaEntity.CONTENA_JOUKYOU_NAME_RYAKU.ToString();
                }

                // 荷積業者
                if (!string.IsNullOrEmpty(this.dto.entryEntity.NIZUMI_GYOUSHA_CD))
                {
                    this.form.NIZUMI_GYOUSHA_CD.Text = this.dto.entryEntity.NIZUMI_GYOUSHA_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.NIZUMI_GYOUSHA_NAME))
                {
                    this.form.NIZUMI_GYOUSHA_NAME.Text = this.dto.entryEntity.NIZUMI_GYOUSHA_NAME.ToString();
                }

                // 荷積現場
                if (!string.IsNullOrEmpty(this.dto.entryEntity.NIZUMI_GENBA_CD))
                {
                    this.form.NIZUMI_GENBA_CD.Text = this.dto.entryEntity.NIZUMI_GENBA_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.NIZUMI_GENBA_NAME))
                {
                    this.form.NIZUMI_GENBA_NAME.Text = this.dto.entryEntity.NIZUMI_GENBA_NAME.ToString();
                }


                // 荷降業者
                if (!string.IsNullOrEmpty(this.dto.entryEntity.NIOROSHI_GYOUSHA_CD))
                {
                    this.form.NIOROSHI_GYOUSHA_CD.Text = this.dto.entryEntity.NIOROSHI_GYOUSHA_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.NIOROSHI_GYOUSHA_NAME))
                {
                    this.form.NIOROSHI_GYOUSHA_NAME.Text = this.dto.entryEntity.NIOROSHI_GYOUSHA_NAME.ToString();
                }

                // 荷降現場
                if (!string.IsNullOrEmpty(this.dto.entryEntity.NIOROSHI_GENBA_CD))
                {
                    this.form.NIOROSHI_GENBA_CD.Text = this.dto.entryEntity.NIOROSHI_GENBA_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.NIOROSHI_GENBA_NAME))
                {
                    this.form.NIOROSHI_GENBA_NAME.Text = this.dto.entryEntity.NIOROSHI_GENBA_NAME.ToString();
                }
                // マニフェスト種類
                if (!this.dto.entryEntity.MANIFEST_SHURUI_CD.IsNull)
                {
                    this.form.MANIFEST_SHURUI_CD.Text = string.Format("{0:00}", (int)this.dto.entryEntity.MANIFEST_SHURUI_CD);
                }
                if (!string.IsNullOrEmpty(this.dto.manifestShuruiEntity.MANIFEST_SHURUI_NAME_RYAKU))
                {
                    this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = this.dto.manifestShuruiEntity.MANIFEST_SHURUI_NAME_RYAKU.ToString();
                }
                // マニフェスト手配
                if (!this.dto.entryEntity.MANIFEST_TEHAI_CD.IsNull)
                {
                    this.form.MANIFEST_TEHAI_CD.Text = string.Format("{0:00}", (int)this.dto.entryEntity.MANIFEST_TEHAI_CD);
                }
                if (!string.IsNullOrEmpty(this.dto.manifestTehaiEntity.MANIFEST_TEHAI_NAME_RYAKU))
                {
                    this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = this.dto.manifestTehaiEntity.MANIFEST_TEHAI_NAME_RYAKU.ToString();
                }
                // 営業担当者
                if (!string.IsNullOrEmpty(this.dto.entryEntity.EIGYOU_TANTOUSHA_CD))
                {
                    this.form.EIGYOU_TANTOUSHA_CD.Text = this.dto.entryEntity.EIGYOU_TANTOUSHA_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.EIGYOU_TANTOUSHA_NAME))
                {
                    this.form.EIGYOU_TANTOUSHA_NAME.Text = this.dto.entryEntity.EIGYOU_TANTOUSHA_NAME.ToString();
                }
                //PhuocLoc 2020/12/01 #136221 -Start
                // 集計項目
                if (!string.IsNullOrEmpty(this.dto.entryEntity.MOD_SHUUKEI_KOUMOKU_CD))
                {
                    this.form.SHUUKEI_KOUMOKU_CD.Text = this.dto.entryEntity.MOD_SHUUKEI_KOUMOKU_CD.ToString();
                }
                if (!string.IsNullOrEmpty(this.dto.entryEntity.MOD_SHUUKEI_KOUMOKU_NAME))
                {
                    this.form.SHUUKEI_KOUMOKU_NAME.Text = this.dto.entryEntity.MOD_SHUUKEI_KOUMOKU_NAME.ToString();
                }
                //PhuocLoc 2020/12/01 #136221 -End
                // 伝票備考
                if (!string.IsNullOrEmpty(this.dto.entryEntity.DENPYOU_BIKOU))
                {
                    this.form.DENPYOU_BIKOU.Text = this.dto.entryEntity.DENPYOU_BIKOU.ToString();
                }

                // 締処理状況(売上)
                // 締処理状況(売上)
                if (seikyuuData != null && 0 < seikyuuData.Rows.Count)
                {
                    if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType))
                    {
                        this.form.SHIMESHORI_JOUKYOU_URIAGE.Text = SalesPaymentConstans.MISHIME;
                    }
                    else
                    {
                        this.form.SHIMESHORI_JOUKYOU_URIAGE.Text = SalesPaymentConstans.SHIMEZUMI;
                    }
                }
                else
                {
                    this.form.SHIMESHORI_JOUKYOU_URIAGE.Text = SalesPaymentConstans.MISHIME;
                }

                // 締処理状況(支払)
                if (seisanData != null && 0 < seisanData.Rows.Count)
                {
                    if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType))
                    {
                        this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Text = SalesPaymentConstans.MISHIME;
                    }
                    else
                    {
                        this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Text = SalesPaymentConstans.SHIMEZUMI;
                    }
                }
                else
                {
                    this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Text = SalesPaymentConstans.MISHIME;
                }

                // 領収書番号(日連番)
                if (!this.dto.entryEntity.RECEIPT_NUMBER.IsNull)
                {
                    this.form.RECEIPT_NUMBER_DAY.Text = this.dto.entryEntity.RECEIPT_NUMBER.ToString();

                }
                // 領収書番号(年連番)
                if (!this.dto.entryEntity.RECEIPT_NUMBER_YEAR.IsNull)
                {
                    this.form.RECEIPT_NUMBER_YEAR.Text = this.dto.entryEntity.RECEIPT_NUMBER_YEAR.ToString();
                }
                // 領収書番号(表示用)
                if (this.dto.sysInfoEntity.SYS_RECEIPT_RENBAN_HOUHOU_KBN == 1)
                {
                    this.form.RECEIPT_NUMBER.Text = this.form.RECEIPT_NUMBER_DAY.Text;
                }
                else
                {
                    this.form.RECEIPT_NUMBER.Text = this.form.RECEIPT_NUMBER_YEAR.Text;
                }

                // 画面に表示されない品名別金額を算出
                decimal hinmeiUriageKingakuTotal = 0;
                decimal hinmeiShiharaiKingakuTotal = 0;

                if (!this.dto.entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL.IsNull)
                {
                    hinmeiUriageKingakuTotal = (decimal)this.dto.entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL;
                }
                if (!this.dto.entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL.IsNull)
                {
                    hinmeiShiharaiKingakuTotal = (decimal)this.dto.entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL;
                }

                // 差引額計算用
                decimal uriageKingakuTotal = 0;
                decimal shiharaiKingakuTotal = 0;

                // 売上金額合計
                if (!this.dto.entryEntity.URIAGE_AMOUNT_TOTAL.IsNull)
                {
                    // 差額計算用
                    uriageKingakuTotal =
                        (decimal)this.dto.entryEntity.URIAGE_AMOUNT_TOTAL + hinmeiUriageKingakuTotal;
                }
                // 支払金額合計
                if (!this.dto.entryEntity.SHIHARAI_AMOUNT_TOTAL.IsNull)
                {
                    this.form.SHIHARAI_KINGAKU_TOTAL.Text =
                        CommonCalc.DecimalFormat((decimal)this.dto.entryEntity.SHIHARAI_AMOUNT_TOTAL + hinmeiShiharaiKingakuTotal);
                    // 差額計算用
                    shiharaiKingakuTotal =
                        (decimal)this.dto.entryEntity.SHIHARAI_AMOUNT_TOTAL + hinmeiShiharaiKingakuTotal;
                }

                // 月極一括作成区分
                this.form.TSUKI_CREATE_KBN.Text = !this.dto.entryEntity.TSUKI_CREATE_KBN.IsNull ? this.dto.entryEntity.TSUKI_CREATE_KBN.ToString() : string.Empty;

                // 排他制御用
                this.form.TIME_STAMP.Text = ConvertStrByte.ByteToString(this.dto.entryEntity.TIME_STAMP);

                //2次
                //取引区分(売)
                if (this.dto.entryEntity.URIAGE_TORIHIKI_KBN_CD == 1)
                {
                    //1.現金
                    this.form.txtUri.Text = "現金";
                }
                else if (this.dto.entryEntity.URIAGE_TORIHIKI_KBN_CD == 2)
                {
                    //2.掛け
                    this.form.txtUri.Text = "掛け";
                }
                else
                {
                    this.form.txtUri.Text = "";
                }
                //取引区分(支)
                if (this.dto.entryEntity.SHIHARAI_TORIHIKI_KBN_CD == 1)
                {
                    //1.現金
                    this.form.txtShi.Text = "現金";
                }
                else if (this.dto.entryEntity.SHIHARAI_TORIHIKI_KBN_CD == 2)
                {
                    //2.掛け
                    this.form.txtShi.Text = "掛け";
                }
                else
                {
                    this.form.txtShi.Text = "";
                }
                //設置台数・引揚台数
                this.form.txtSecchi.Text = "";
                this.form.txtHikiage.Text = "";
                SqlInt16 sumSecchi = 0;
                SqlInt16 sumHikiage = 0;
                foreach (T_CONTENA_RESULT entity in this.dto.contenaResultList)
                {
                    if (entity.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI
                        && entity.DENSHU_KBN_CD == SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI
                        && entity.DELETE_FLG.Equals(SqlBoolean.False))
                    {
                        //設置
                        if (!entity.DAISUU_CNT.Equals(SqlInt16.Null))
                        {
                            sumSecchi += entity.DAISUU_CNT;
                        }
                    }
                    else if (entity.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE
                             && entity.DENSHU_KBN_CD == SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI
                             && entity.DELETE_FLG.Equals(SqlBoolean.False))
                    {
                        //引揚
                        if (!entity.DAISUU_CNT.Equals(SqlInt16.Null))
                        {
                            sumHikiage += entity.DAISUU_CNT;
                        }
                    }

                }
                this.form.txtSecchi.Text = sumSecchi.ToString();
                this.form.txtHikiage.Text = sumHikiage.ToString();

                // 明細 Start
                // テンプレートをいじる処理は、データ設定前に実行
                this.ExecuteAlignmentForDetail();

                this.form.mrwDetail.BeginEdit(false);
                this.form.mrwDetail.Rows.Clear();
                // CreateDataTableForEntityがMultiRowを動的に作成しないため、ここでEntity分行数を追加する
                // Entity数Rowを作ると最終行が無いので、Etity + 1でループさせる
                for (int i = 1; i < this.dto.detailEntity.Length + 1; i++)
                {
                    this.form.mrwDetail.Rows.Add();
                }
                var dataBinder = new DataBinderLogic<T_UR_SH_DETAIL>(this.dto.detailEntity);
                dataBinder.CreateDataTableForEntity(this.form.mrwDetail, this.dto.detailEntity);

                // MultiRowへ設定
                int k = 0;
                foreach (var row in this.form.mrwDetail.Rows)
                {
                    if (k < dto.detailEntity.Length)
                    {
                        short denpyouCd = 0;
                        ICustomControl denpyouCdCell = (ICustomControl)row.Cells[CELL_NAME_DENPYOU_KBN_CD];
                        if (short.TryParse(denpyouCdCell.GetResultText(), out denpyouCd)
                            && denpyouKbnDictionary.ContainsKey(denpyouCd))
                        {
                            row.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = denpyouKbnDictionary[denpyouCd].DENPYOU_KBN_NAME_RYAKU;
                        }

                        short unitCd = 0;
                        ICustomControl unitCdCell = (ICustomControl)row.Cells[CELL_NAME_UNIT_CD];
                        if (short.TryParse(unitCdCell.GetResultText(), out unitCd)
                            && unitDictionary.ContainsKey(unitCd))
                        {
                            row.Cells[CELL_NAME_UNIT_NAME_RYAKU].Value = unitDictionary[unitCd].UNIT_NAME_RYAKU;
                        }

                        // マニフェスト.交付番号
                        if (!WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType))
                        {
                            if (row.Cells[CELL_NAME_SYSTEM_ID].Value != null
                                 && row.Cells[CELL_NAME_DETAIL_SYSTEM_ID].Value != null)
                            {
                                string whereStrForMani = "RENKEI_DENSHU_KBN_CD = " + SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI + " AND RENKEI_SYSTEM_ID = " + row.Cells[CELL_NAME_SYSTEM_ID].Value + " AND RENKEI_MEISAI_SYSTEM_ID = " + row.Cells[CELL_NAME_DETAIL_SYSTEM_ID].Value;
                                var manifestEntry = this.dto.manifestEntrys.Select(whereStrForMani);
                                if (manifestEntry != null && 0 < manifestEntry.Length)
                                {
                                    // 一件しか取れないはずなので、最初の要素を取得
                                    row.Cells[CELL_NAME_MANIFEST_ID].Value = manifestEntry[0][CELL_NAME_MANIFEST_ID];
                                }
                            }
                        }

                        if (k < this.dto.detailEntity.Length)
                        {
                            T_UR_SH_DETAIL ditail = this.dto.detailEntity[k];
                            // 金額
                            decimal kintaku = 0;
                            decimal hinmeiKingaku = 0;
                            decimal.TryParse(Convert.ToString(ditail.KINGAKU), out kintaku);
                            decimal.TryParse(Convert.ToString(ditail.HINMEI_KINGAKU), out hinmeiKingaku);
                            row.Cells[CELL_NAME_KINGAKU].Value = kintaku + hinmeiKingaku;

                            // 確定区分
                            if (ditail.KAKUTEI_KBN == SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI)
                            {
                                row.Cells[CELL_NAME_KAKUTEI_KBN].Value = true;
                            }
                            else
                            {
                                row.Cells[CELL_NAME_KAKUTEI_KBN].Value = false;
                            }
                        }

                        // 締処理状況設定
                        string whereStr = string.Empty;
                        whereStr = "DETAIL_SYSTEM_ID = " + row.Cells[CELL_NAME_DETAIL_SYSTEM_ID].Value;
                        DataRow[] detail = null;

                        if (SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE == denpyouCd)
                        {
                            detail = seikyuuData.Select(whereStr);
                            if (detail != null && 0 < detail.Length)
                            {
                                row.Cells[CELL_NAME_JOUKYOU].Value = SalesPaymentConstans.SHIMEZUMI_DETAIL;
                            }
                            else
                            {
                                row.Cells[CELL_NAME_JOUKYOU].Value = SalesPaymentConstans.MISHIME_DETAIL;
                            }
                        }
                        else if (SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI == denpyouCd)
                        {
                            detail = seisanData.Select(whereStr);
                            if (detail != null && 0 < detail.Length)
                            {
                                row.Cells[CELL_NAME_JOUKYOU].Value = SalesPaymentConstans.SHIMEZUMI_DETAIL;
                            }
                            else
                            {
                                row.Cells[CELL_NAME_JOUKYOU].Value = SalesPaymentConstans.MISHIME_DETAIL;
                            }
                        }
                    }


                    k++;
                }



                //MultiRow設定時にCellValidated処理が動いて合計金額が再計算されるため、TextBoxの反映は最後にする。
                this.form.URIAGE_AMOUNT_TOTAL.Text = CommonCalc.DecimalFormat(uriageKingakuTotal);
                this.form.SHIHARAI_KINGAKU_TOTAL.Text = CommonCalc.DecimalFormat(shiharaiKingakuTotal);

                // 差額
                // TODO：[システム設定].受入差引基準がどのテーブルにあるか確認
                if (this.dto.sysInfoEntity.UR_SH_CALC_BASE_KBN == SalesPaymentConstans.UR_SH_CALC_BASE_KBN_URIAGE)
                {
                    this.form.SAGAKU.Text = CommonCalc.DecimalFormat(uriageKingakuTotal - shiharaiKingakuTotal);
                }
                else if (this.dto.sysInfoEntity.UR_SH_CALC_BASE_KBN == SalesPaymentConstans.UR_SH_CALC_BASE_KBN_SHIHARAI)
                {
                    this.form.SAGAKU.Text = CommonCalc.DecimalFormat(shiharaiKingakuTotal - uriageKingakuTotal);
                }
                // 明細 End

            }
            else
            {
                // No.4089-->
                this.form.KAKUTEI_KBN.Text = this.dto.sysInfoEntity.UR_SH_KAKUTEI_FLAG.ToString();
                this.form.KAKUTEI_KBN_NAME.Text = SalesPaymentConstans.GetKakuteiKbnName(Int16.Parse(this.form.KAKUTEI_KBN.Text));
                // No.4089<--
            }

            if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType) && this.form.UrShNumber != -1)
            {

                // 複写モード（新規モード、受入番号あり）の初期化処理  
                //受入番号
                this.form.ENTRY_NUMBER.Text = "";
                //日付連番
                this.form.RENBAN.Text = "";
                // 日付系初期値設定
                if (!this.form.isRegisted)
                {
                    // 継続入力じゃないときだけ初期化
                    this.form.DENPYOU_DATE.Value = this.footerForm.sysDate;
                    this.form.URIAGE_DATE.Value = this.footerForm.sysDate;
                    this.form.SHIHARAI_DATE.Value = this.footerForm.sysDate;
                }
                
                // コンテナ
                this.form.CONTENA_SOUSA_CD.Text = string.Empty;
                this.form.CONTENA_JOUKYOU_NAME_RYAKU.Text = string.Empty;
                //設置
                this.form.txtSecchi.Text = string.Empty;
                //引揚
                this.form.txtHikiage.Text = string.Empty;
                if (this.dto != null)
                {
                    this.dto.contenaResultList = new List<T_CONTENA_RESULT>();
                    this.dto.contenaReserveList = new List<T_CONTENA_RESERVE>();
                }
                
                //取引先チェック及びＤＴＯセット
                // 20160120 chenzz 12114の不具合一覧についての修正(販売管理(入力)no.31) start
                string torihikisakiNmae = this.dto.entryEntity.TORIHIKISAKI_NAME;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiNmae;
                // 20160120 chenzz 12114の不具合一覧についての修正(販売管理(入力)no.31) end
                // 領収書番号
                this.form.RECEIPT_NUMBER.Text = string.Empty;
                this.form.RECEIPT_NUMBER_DAY.Text = string.Empty;
                this.form.RECEIPT_NUMBER_YEAR.Text = string.Empty;
            }


            this.form.mrwDetail.EndEdit();
            this.form.mrwDetail.NotifyCurrentCellDirty(false);

            if (this.form.mrwDetail.Rows[0].Cells[CELL_NAME_URIAGESHIHARAI_DATE].Visible == true &&
                this.form.mrwDetail.Rows[0].Cells[CELL_NAME_URIAGESHIHARAI_DATE].ReadOnly == false)
            {
                this.form.mrwDetail.CurrentCell = this.form.mrwDetail.Rows[0].Cells[CELL_NAME_URIAGESHIHARAI_DATE];
            }
            else
            {
                this.form.mrwDetail.CurrentCell = this.form.mrwDetail.Rows[0].Cells[CELL_NAME_MEISAI_BIKOU];
            }

            //M_SYS_INFO mSysInfo = this.accessor.GetSysInfo();
            //// 計量票出力区分
            //if (!mSysInfo.UKEIRE_KEIRYOU_PRIRNT_KBN.IsNull)
            //{
            //    this.form.KEIRYOU_PRIRNT_KBN_VALUE.Text = mSysInfo.UKEIRE_KEIRYOU_PRIRNT_KBN.ToString();
            //}

            //ThangNguyen [Add] 20150826 #10907 Start
            this.CheckTorihikisakiShokuchi();
            this.CheckGyoushaShokuchi();
            this.CheckGenbaShokuchi();
            this.CheckNizumiGyoushaShokuchi();
            this.CheckNizumiGenbaShokuchi();
            this.CheckNioroshiGyoushaShokuchi();
            this.CheckNioroshiGenbaShokuchi();
            this.CheckUpanGyoushaShokuchi();
            //ThangNguyen [Add] 20150826 #10907 End
            if (WINDOW_TYPE.REFERENCE_WINDOW_FLAG.Equals(this.form.WindowType)
                || WINDOW_TYPE.DELETE_WINDOW_FLAG.Equals(this.form.WindowType))
            {
                this.form.DENPYOU_RIREKI_BTN.Enabled = false;
            }
        }

        /// <summary>
        /// Entity作成と登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        /// <returns>true:成功, false:失敗</returns>
        public bool CreateEntityAndUpdateTables(bool errorFlag)
        {
            try
            {
                var uketsukeExist = false;
                this.form.RegistErrorFlag = false;
                if (!string.IsNullOrEmpty(this.form.UKETSUKE_NUMBER.Text))
                {
                    var dtUketsuke = this.accessor.GetUketsukeSS(this.form.UKETSUKE_NUMBER.Text);
                    if (dtUketsuke.Rows.Count > 0)
                    {
                        if (null != this.tUketsukeSsEntry)
                        {
                            // 収集受付の更新前にデータが重複していないかチェックを行う
                            var systemId = this.tUketsukeSsEntry.SYSTEM_ID.ToString();
                            var checkEntity = this.accessor.GetUketsukeSsEntry(systemId);
                            if (checkEntity == null || (this.tUketsukeSsEntry.SEQ != checkEntity.SEQ))
                            {
                                // 重複していた場合は登録を行わない
                                uketsukeExist = true;
                            }
                        }
                    }
                    else
                    {
                        dtUketsuke = this.accessor.GetUketsukeSK(this.form.UKETSUKE_NUMBER.Text);
                        if (dtUketsuke.Rows.Count > 0)
                        {
                            if (null != this.tUketsukeSkEntry)
                            {
                                // 出荷受付の更新前にデータが重複していないかチェックを行う
                                var systemId = this.tUketsukeSkEntry.SYSTEM_ID.ToString();
                                var checkEntity = this.accessor.GetUketsukeSkEntry(systemId);
                                if (checkEntity == null || (this.tUketsukeSkEntry.SEQ != checkEntity.SEQ))
                                {
                                    // 重複していた場合は登録を行わない
                                    uketsukeExist = true;
                                }
                            }
                        }
                    }
                }

                if (uketsukeExist == false)
                {
                    // 排他制御する
                    using (Transaction tran = new Transaction())
                    {
                        switch (this.form.WindowType)
                        {
                            case WINDOW_TYPE.NEW_WINDOW_FLAG:
                                this.CreateEntity();
                                // 入出金系
                                this.CreateNyuuShukkinEntity();
                                this.Regist(errorFlag);

                                // キャッシャ連動「1.する」の場合
                                if (this.form.denpyouHakouPopUpDTO.Kyasya == CommonConst.CASHER_LINK_KBN_USE)
                                {
                                    // キャッシャ情報送信
                                    this.sendCasher();
                                }
                                break;

                            case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                                this.CreateEntity();
                                this.Update(errorFlag);
                                break;

                            case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                                this.CreateEntity();
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
                    msgLogic.MessageBoxShowError("該当の受付データに変更があります。\n再度入力し直してください。");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityAndUpdateTables", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    // TODO: 排他エラー用のエラーメッセージをDBに定義すること
                    //string errorMessage = "登録に失敗しました。\n\r他のユーザからデータを更新された可能性があります。\n\r再度登録を実行してください。繰り返しエラーが発生する場合は一度画面を閉じて再度情報を入力しなおしてください。";
                    //MessageBox.Show(errorMessage, MESSAGE_KUBUN.ERROR.ToKubunString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.form.RegistErrorFlag = true;
                    msgLogic.MessageBoxShow("E080");

                    return false;
                }
                else if (ex is SQLRuntimeException)
                {
                    // SQLエラー用メッセージを出力
                    this.form.RegistErrorFlag = true;

                    var causeNo = ((SqlException)ex.InnerException).Number;
                    // 一意エラーの場合
                    if (causeNo == 2627)
                    {
                        msgLogic.MessageBoxShow("E080", "");
                        return false;
                    }

                    msgLogic.MessageBoxShow("E093", "");
                    return false;
                }
                else
                {
                    this.form.RegistErrorFlag = true;
                    msgLogic.MessageBoxShow("E245", "");
                    return false;
                }
            }

            return true;

        }


        /// <summary>
        /// 論理削除処理
        /// </summary>
        public virtual void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            this.dto.entryEntity.DELETE_FLG = true;
            /// 20141118 Houkakou 「更新日、登録日の見直し」　start
            this.dto.entryEntity.UPDATE_DATE = this.beforDto.entryEntity.UPDATE_DATE;
            this.dto.entryEntity.UPDATE_PC = this.beforDto.entryEntity.UPDATE_PC;
            this.dto.entryEntity.UPDATE_USER = this.beforDto.entryEntity.UPDATE_USER;
            /// 20141118 Houkakou 「更新日、登録日の見直し」　end
            this.accessor.UpdateUrshEntry(this.dto.entryEntity);

            /// 20141118 Houkakou 「更新日、登録日の見直し」　start
            this.dto.entryEntity.DELETE_FLG = true;
            this.dto.entryEntity.SEQ = this.dto.entryEntity.SEQ + 1;
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            //this.dto.entryEntity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
            this.dto.entryEntity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            this.dto.entryEntity.UPDATE_PC = SystemInformation.ComputerName;
            this.dto.entryEntity.UPDATE_USER = SystemProperty.UserName;
            this.accessor.InsertUrshEntry(this.dto.entryEntity);

            for (int row = 0; row < this.dto.detailEntity.Length; row++)
            {
                this.dto.detailEntity[row].SEQ = this.dto.entryEntity.SEQ;
            }
            this.accessor.InsertUrshDetail(this.dto.detailEntity);
            //2次
            // コンテナ稼動予約の台数計算フラグを更新
            foreach (var targetEntity in this.dto.contenaResultList)
            {
                targetEntity.SEQ = this.dto.entryEntity.SEQ;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //targetEntity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                targetEntity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                targetEntity.UPDATE_PC = SystemInformation.ComputerName;
                targetEntity.UPDATE_USER = SystemProperty.UserName;
            }
            this.accessor.InsertContenaResult(this.dto.contenaResultList);
            this.accessor.UpdateContenaResult(this.beforDto.contenaResultList);
            /// 20141118 Houkakou 「更新日、登録日の見直し」　end

            // 受付番号がある場合は、受付のデータ更新
            if (!this.dto.entryEntity.UKETSUKE_NUMBER.IsNull)
            {
                var dtUketsuke = this.accessor.GetUketsukeSS(this.dto.entryEntity.UKETSUKE_NUMBER.ToString());
                if (dtUketsuke.Rows.Count > 0)
                {
                    // 複数の伝票に対して連携されている場合、伝票の更新は行わない。
                    var dtUketsukeRenkeiData = this.accessor.GetUketsukeSSRenke(this.dto.entryEntity.UKETSUKE_NUMBER.ToString(), this.dto.entryEntity.UR_SH_NUMBER.ToString());
                    if (dtUketsukeRenkeiData == null || dtUketsukeRenkeiData.Rows.Count < 1)
                    {
                        var systemId = dtUketsuke.Rows[0]["SYSTEM_ID"].ToString();
                        var seq = dtUketsuke.Rows[0]["SEQ"].ToString();
                        this.tUketsukeSsEntry = this.accessor.GetUketsukeSsEntry(systemId, seq);

                        if (null != this.tUketsukeSsEntry)
                        {
                            if (!String.IsNullOrEmpty(this.tUketsukeSsEntry.SHARYOU_CD) && !String.IsNullOrEmpty(this.tUketsukeSsEntry.UNTENSHA_CD))
                            {
                                this.UpdateHaishaJokyoSs(SalesPaymentConstans.HAISHA_JOKYO_CD_HAISHA, SalesPaymentConstans.HAISHA_JOKYO_NAME_HAISHA);
                            }
                            else
                            {
                                this.UpdateHaishaJokyoSs(SalesPaymentConstans.HAISHA_JOKYO_CD_JUCHU, SalesPaymentConstans.HAISHA_JOKYO_NAME_JUCHU);
                            }
                        }
                    }
                }
                else
                {
                    dtUketsuke = this.accessor.GetUketsukeSK(this.dto.entryEntity.UKETSUKE_NUMBER.ToString());
                    if (dtUketsuke.Rows.Count > 0)
                    {
                        // 複数の伝票に対して連携されている場合、伝票の更新は行わない。
                        var dtUketsukeRenkeiData = this.accessor.GetUketsukeSKRenkei(this.dto.entryEntity.UKETSUKE_NUMBER.ToString(), this.dto.entryEntity.UR_SH_NUMBER.ToString());
                        if (dtUketsukeRenkeiData == null || dtUketsukeRenkeiData.Rows.Count < 1)
                        {

                            var systemId = dtUketsuke.Rows[0]["SYSTEM_ID"].ToString();
                            var seq = dtUketsuke.Rows[0]["SEQ"].ToString();
                            this.tUketsukeSkEntry = this.accessor.GetUketsukeSkEntry(systemId, seq);

                            if (null != this.tUketsukeSkEntry)
                            {
                                if (!String.IsNullOrEmpty(this.tUketsukeSkEntry.SHARYOU_CD) && !String.IsNullOrEmpty(this.tUketsukeSkEntry.UNTENSHA_CD))
                                {
                                    this.UpdateHaishaJokyoSk(SalesPaymentConstans.HAISHA_JOKYO_CD_HAISHA, SalesPaymentConstans.HAISHA_JOKYO_NAME_HAISHA);
                                }
                                else
                                {
                                    this.UpdateHaishaJokyoSk(SalesPaymentConstans.HAISHA_JOKYO_CD_JUCHU, SalesPaymentConstans.HAISHA_JOKYO_NAME_JUCHU);
                                }
                            }
                        }
                    }
                    else
                    {
						//持込受付の更新 refs #158909
                        dtUketsuke = this.accessor.GetUketsukeMK(this.dto.entryEntity.UKETSUKE_NUMBER.ToString());
                        if (dtUketsuke.Rows.Count > 0)
                        {
                            var systemId = dtUketsuke.Rows[0]["SYSTEM_ID"].ToString();
                            var seq = dtUketsuke.Rows[0]["SEQ"].ToString();
                            this.tUketsukeMkEntry = this.accessor.GetUketsukeMkEntry(systemId, seq);

                            if (null != this.tUketsukeMkEntry)
                            {
                                this.UpdateYoyakuJokyo(SalesPaymentConstans.YOYAKU_JOKYO_CD_YOYAKU_KANRYOU, SalesPaymentConstans.YOYAKU_JOKYO_YOYAKU_KANRYOU_NAME);
                            }
                        }
                    }
                }
            }

            // 実績売上支払確定により作成された売上支払伝票を削除する場合は、定期実績明細データの売上支払番号をクリアする
            T_TEIKI_JISSEKI_DETAIL[] arrayTjd = this.accessor.GetTeikiJissekiDetail(this.dto.entryEntity.UR_SH_NUMBER.ToString());
            if (arrayTjd != null && arrayTjd.Length > 0)
            {
                foreach (T_TEIKI_JISSEKI_DETAIL tjd in arrayTjd)
                {
                    tjd.UR_SH_NUMBER = SqlInt64.Null;
                    tjd.KAKUTEI_FLG = false;
                    this.accessor.UpdateTeikiJissekiDetail(tjd);
                }
            }

            LogUtility.DebugMethodStart();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            this.accessor.InsertUrshEntry(this.dto.entryEntity);
            this.accessor.InsertUrshDetail(this.dto.detailEntity);
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
            if (ConstClass.RYOSYUSYO_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Ryousyusyou))
            {
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
            }

            // S_NUMBER_RECEIPT_YEARの更新
            if (ConstClass.RYOSYUSYO_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Ryousyusyou))
            {
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
            }

            // 入出金系の更新
            // Insertメソッド内で更新に必要なキーのチェックをしているので、ここでのチェックは必要なし
            this.accessor.InsertNyuukinSumEntry(this.nyuuShukkinDto.nyuukinSumEntry);
            this.accessor.InsertNyuukinSumDetails(this.nyuuShukkinDto.nyuukinSumDetails);
            this.accessor.InsertNyuukinEntry(this.nyuuShukkinDto.nyuukinEntry);
            this.accessor.InsertNyuukinDetails(this.nyuuShukkinDto.nyuukinDetials);
            this.accessor.InsertShukkinEntry(this.nyuuShukkinDto.shukkinEntry);
            this.accessor.InsertShukkinDetails(this.nyuuShukkinDto.shukkinDetails);

            //2次
            // コンテナ稼動予約の台数計算フラグを更新
            this.accessor.UpdateContenaReserve(this.dto.contenaReserveList);

            this.accessor.InsertContenaResult(this.dto.contenaResultList);

            // 入力内容に基づいてコンテナマスタの更新
            if (this.dto.contenaResultList.Count > 0)
            {
                this.dto.contenaMasterList = new List<M_CONTENA>();
                UpdateContenaMaster();
                if (this.dto.contenaMasterList.Count > 0)
                {
                    this.accessor.UpdateContenaMaster(this.dto.contenaMasterList);
                }
            }

            if (null != this.tUketsukeSsEntry)
            {
                // 収集受付を紐付け時は収集受付を更新
                this.UpdateHaishaJokyoSs(SalesPaymentConstans.HAISHA_JOKYO_CD_KEIJO, SalesPaymentConstans.HAISHA_JOKYO_NAME_KEIJO);
            }
            else if (null != this.tUketsukeSkEntry)
            {
                // 出荷受付を紐付け時は出荷受付を更新
                this.UpdateHaishaJokyoSk(SalesPaymentConstans.HAISHA_JOKYO_CD_KEIJO, SalesPaymentConstans.HAISHA_JOKYO_NAME_KEIJO);
            }
            else if (null != this.tUketsukeMkEntry)
            {
                // 持込受付を紐付け時は持込受付を更新 refs #158909
                this.UpdateYoyakuJokyo(SalesPaymentConstans.YOYAKU_JOKYO_CD_KEIJOU, SalesPaymentConstans.YOYAKU_JOKYO_KEIJOU_NAME);
            }

            LogUtility.DebugMethodEnd(errorFlag);
        }
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            // TODO: 途中でエラーが発生してロールバックがされるか確認
            this.accessor.InsertUrshEntry(this.dto.entryEntity);
            this.accessor.UpdateUrshEntry(this.beforDto.entryEntity);
            this.accessor.InsertUrshDetail(this.dto.detailEntity);
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
            if (ConstClass.RYOSYUSYO_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Ryousyusyou))
            {
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
            }

            // S_NUMBER_RECEIPT_YEARの更新
            if (ConstClass.RYOSYUSYO_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Ryousyusyou))
            {
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
            }

            //2次
            // コンテナ稼動予約の台数計算フラグを更新
            this.accessor.UpdateContenaReserve(this.dto.contenaReserveList);

            this.accessor.InsertContenaResult(this.dto.contenaResultList);
            this.accessor.UpdateContenaResult(this.beforDto.contenaResultList);
            // 入力内容に基づいてコンテナマスタの更新
            if (this.dto.contenaResultList.Count > 0)
            {
                this.dto.contenaMasterList = new List<M_CONTENA>();
                UpdateContenaMaster();
                if (this.dto.contenaMasterList.Count > 0)
                {
                    this.accessor.UpdateContenaMaster(this.dto.contenaMasterList);
                }
            }
            // 収集受付の更新
            var beforeUketsukeNumber = this.beforDto.entryEntity.UKETSUKE_NUMBER;
            var uketsukeNumber = this.dto.entryEntity.UKETSUKE_NUMBER;

            if (beforeUketsukeNumber.IsNull && !uketsukeNumber.IsNull)
            {
                // 更新後だけ受付番号がセットされている場合は、更新後データに紐付けられている受付データの配車状況を更新する
                if (null != this.tUketsukeSsEntry)
                {
                    this.UpdateHaishaJokyoSs(SalesPaymentConstans.HAISHA_JOKYO_CD_KEIJO, SalesPaymentConstans.HAISHA_JOKYO_NAME_KEIJO);
                }
                else if (null != this.tUketsukeSkEntry)
                {
                    this.UpdateHaishaJokyoSk(SalesPaymentConstans.HAISHA_JOKYO_CD_KEIJO, SalesPaymentConstans.HAISHA_JOKYO_NAME_KEIJO);
                }
                else if (null != this.tUketsukeMkEntry)
                {
					//持込受付の更新 refs #158909
                    this.UpdateYoyakuJokyo(SalesPaymentConstans.YOYAKU_JOKYO_CD_KEIJOU, SalesPaymentConstans.YOYAKU_JOKYO_KEIJOU_NAME);
                }
            }
            else if (!beforeUketsukeNumber.IsNull && uketsukeNumber.IsNull)
            {
                // 更新前だけ受付番号がセットされている場合は、更新前データに紐付けられている受付の配車状況を更新する
                // 更新前データに紐付けられている受付データは取得してから更新する
                var dtUketsuke = this.accessor.GetUketsukeSS(beforeUketsukeNumber.ToString());
                if (dtUketsuke.Rows.Count > 0)
                {
                    var systemId = dtUketsuke.Rows[0]["SYSTEM_ID"].ToString();
                    var seq = dtUketsuke.Rows[0]["SEQ"].ToString();
                    this.tUketsukeSsEntry = this.accessor.GetUketsukeSsEntry(systemId, seq);

                    if (null != this.tUketsukeSsEntry)
                    {
                        if (!String.IsNullOrEmpty(this.tUketsukeSsEntry.SHARYOU_CD) && !String.IsNullOrEmpty(this.tUketsukeSsEntry.UNTENSHA_CD))
                        {
                            this.UpdateHaishaJokyoSs(SalesPaymentConstans.HAISHA_JOKYO_CD_HAISHA, SalesPaymentConstans.HAISHA_JOKYO_NAME_HAISHA);
                        }
                        else
                        {
                            this.UpdateHaishaJokyoSs(SalesPaymentConstans.HAISHA_JOKYO_CD_JUCHU, SalesPaymentConstans.HAISHA_JOKYO_NAME_JUCHU);
                        }
                    }
                }
                else
                {
                    dtUketsuke = this.accessor.GetUketsukeSK(beforeUketsukeNumber.ToString());
                    if (dtUketsuke.Rows.Count > 0)
                    {
                        var systemId = dtUketsuke.Rows[0]["SYSTEM_ID"].ToString();
                        var seq = dtUketsuke.Rows[0]["SEQ"].ToString();
                        this.tUketsukeSkEntry = this.accessor.GetUketsukeSkEntry(systemId, seq);

                        if (null != this.tUketsukeSkEntry)
                        {
                            if (!String.IsNullOrEmpty(this.tUketsukeSkEntry.SHARYOU_CD) && !String.IsNullOrEmpty(this.tUketsukeSkEntry.UNTENSHA_CD))
                            {
                                this.UpdateHaishaJokyoSk(SalesPaymentConstans.HAISHA_JOKYO_CD_HAISHA, SalesPaymentConstans.HAISHA_JOKYO_NAME_HAISHA);
                            }
                            else
                            {
                                this.UpdateHaishaJokyoSk(SalesPaymentConstans.HAISHA_JOKYO_CD_JUCHU, SalesPaymentConstans.HAISHA_JOKYO_NAME_JUCHU);
                            }
                        }
                    }
                    else
                    {
						//持込受付の更新 refs #158909
                        dtUketsuke = this.accessor.GetUketsukeMK(beforeUketsukeNumber.ToString());
                        if (dtUketsuke.Rows.Count > 0)
                        {
                            var systemId = dtUketsuke.Rows[0]["SYSTEM_ID"].ToString();
                            var seq = dtUketsuke.Rows[0]["SEQ"].ToString();
                            this.tUketsukeMkEntry = this.accessor.GetUketsukeMkEntry(systemId, seq);

                            if (null != this.tUketsukeMkEntry)
                            {
                                this.UpdateYoyakuJokyo(SalesPaymentConstans.YOYAKU_JOKYO_CD_YOYAKU_KANRYOU, SalesPaymentConstans.YOYAKU_JOKYO_YOYAKU_KANRYOU_NAME);
                            }
                        }
                    }
                }
            }
            else if (!beforeUketsukeNumber.IsNull && !uketsukeNumber.IsNull && beforeUketsukeNumber != uketsukeNumber)
            {
                // 両方の受付番号がセットされている場合は、両方のデータに紐付けられている受付データの配車状況を更新する
                // 先に更新後データに紐付けられている受付の配車状況を更新
                if (null != this.tUketsukeSsEntry)
                {
                    this.UpdateHaishaJokyoSs(SalesPaymentConstans.HAISHA_JOKYO_CD_KEIJO, SalesPaymentConstans.HAISHA_JOKYO_NAME_KEIJO);
                }
                else if (null != this.tUketsukeSkEntry)
                {
                    this.UpdateHaishaJokyoSk(SalesPaymentConstans.HAISHA_JOKYO_CD_KEIJO, SalesPaymentConstans.HAISHA_JOKYO_NAME_KEIJO);
                }
                // 更新前データに紐付けられている受付データは取得してから更新する
                var dtUketsuke = this.accessor.GetUketsukeSS(beforeUketsukeNumber.ToString());
                if (dtUketsuke.Rows.Count > 0)
                {
                    var systemId = dtUketsuke.Rows[0]["SYSTEM_ID"].ToString();
                    var seq = dtUketsuke.Rows[0]["SEQ"].ToString();
                    this.tUketsukeSsEntry = this.accessor.GetUketsukeSsEntry(systemId, seq);

                    if (null != this.tUketsukeSsEntry)
                    {
                        if (!String.IsNullOrEmpty(this.tUketsukeSsEntry.SHARYOU_CD) && !String.IsNullOrEmpty(this.tUketsukeSsEntry.UNTENSHA_CD))
                        {
                            this.UpdateHaishaJokyoSs(SalesPaymentConstans.HAISHA_JOKYO_CD_HAISHA, SalesPaymentConstans.HAISHA_JOKYO_NAME_HAISHA);
                        }
                        else
                        {
                            this.UpdateHaishaJokyoSs(SalesPaymentConstans.HAISHA_JOKYO_CD_JUCHU, SalesPaymentConstans.HAISHA_JOKYO_NAME_JUCHU);
                        }
                    }
                }
                else
                {
                    dtUketsuke = this.accessor.GetUketsukeSK(beforeUketsukeNumber.ToString());
                    if (dtUketsuke.Rows.Count > 0)
                    {
                        var systemId = dtUketsuke.Rows[0]["SYSTEM_ID"].ToString();
                        var seq = dtUketsuke.Rows[0]["SEQ"].ToString();
                        this.tUketsukeSkEntry = this.accessor.GetUketsukeSkEntry(systemId, seq);

                        if (null != this.tUketsukeSkEntry)
                        {
                            if (!String.IsNullOrEmpty(this.tUketsukeSkEntry.SHARYOU_CD) && !String.IsNullOrEmpty(this.tUketsukeSkEntry.UNTENSHA_CD))
                            {
                                this.UpdateHaishaJokyoSk(SalesPaymentConstans.HAISHA_JOKYO_CD_HAISHA, SalesPaymentConstans.HAISHA_JOKYO_NAME_HAISHA);
                            }
                            else
                            {
                                this.UpdateHaishaJokyoSk(SalesPaymentConstans.HAISHA_JOKYO_CD_JUCHU, SalesPaymentConstans.HAISHA_JOKYO_NAME_JUCHU);
                            }
                        }
                    }
                }
            }

            LogUtility.DebugMethodEnd(errorFlag);
        }

        /// <summary>
        /// 明細の金額、重量計算
        /// 金額、重量計算をまとめて処理します
        /// </summary>
        internal bool CalcDetail()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 合計系金額計算
                bool catchErr = this.CalcTotalValues();
                if (catchErr)
                {
                    return true;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcDetail", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 合計系の計算
        /// </summary>
        internal bool CalcTotalValues()
        {
            try
            {
                LogUtility.DebugMethodStart();

                decimal uriageKingakuTotal = 0;
                decimal shiharaiKingakuTotal = 0;
                foreach (Row dr in this.form.mrwDetail.Rows)
                {
                    decimal kingaku = 0;

                    decimal.TryParse(Convert.ToString(dr.Cells[CELL_NAME_KINGAKU].EditedFormattedValue), out kingaku);

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
                this.form.URIAGE_AMOUNT_TOTAL.Text = uriageKingakuTotal.ToString();
                this.form.SHIHARAI_KINGAKU_TOTAL.Text = shiharaiKingakuTotal.ToString();

                // 差額計算
                this.CalcSagaku();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcTotalValues", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 差額計算
        /// </summary>
        internal void CalcSagaku()
        {
            LogUtility.DebugMethodStart();

            decimal uriageTotal = 0;
            decimal shiharaiTotal = 0;

            decimal.TryParse(Convert.ToString(this.form.URIAGE_AMOUNT_TOTAL.Text), out uriageTotal);
            decimal.TryParse(Convert.ToString(this.form.SHIHARAI_KINGAKU_TOTAL.Text), out shiharaiTotal);

            // TODO: 売上/支払差額基準の参照があっているか確認
            if (this.dto.sysInfoEntity.UR_SH_CALC_BASE_KBN == SalesPaymentConstans.UR_SH_CALC_BASE_KBN_URIAGE)
            {
                this.form.SAGAKU.Text = Convert.ToString(uriageTotal - shiharaiTotal);
            }
            else if (this.dto.sysInfoEntity.UR_SH_CALC_BASE_KBN == SalesPaymentConstans.UR_SH_CALC_BASE_KBN_SHIHARAI)
            {
                this.form.SAGAKU.Text = Convert.ToString(shiharaiTotal - uriageTotal);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 品名入力値チェック
        /// </summary>
        /// <returns>true: 問題なし, false:問題あり</returns>
        internal bool ValidateHinmeiName(out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart();
                bool returnVal = false;
                catchErr = false;

                Row targetRow = this.form.mrwDetail.CurrentRow;

                if (targetRow == null)
                {
                    return returnVal;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value)))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_NAME].EditedFormattedValue)))
                    {
                        CellPosition pos = this.form.mrwDetail.CurrentCellPosition;
                        this.form.mrwDetail.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, CELL_NAME_HINMEI_NAME);

                        var cell = targetRow.Cells[CELL_NAME_HINMEI_NAME] as ICustomAutoChangeBackColor;
                        cell.IsInputErrorOccured = true;
                        cell.UpdateBackColor();

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E012", "品名");
                        return returnVal;
                    }
                }

                returnVal = true;
                LogUtility.DebugMethodEnd(returnVal, catchErr);
                return returnVal;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("ValidateHinmeiName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true, catchErr);
                return true;
            }
        }

        /// <summary>
        /// 品名コードより品名再取得
        /// </summary>
        /// <param name="hinmeiCd"></param>
        /// <returns>品名略称</returns>
        //internal string SearchHinmei(string hinmeiCd)
        //{
        //    LogUtility.DebugMethodStart(hinmeiCd);

        //    string returnValue = string.Empty;
        //    M_HINMEI hinmei = this.accessor.GetHinmeiDataByCd(hinmeiCd);
        //    if (!string.IsNullOrEmpty(hinmei.HINMEI_NAME_RYAKU))
        //    {
        //        returnValue = hinmei.HINMEI_NAME_RYAKU;
        //    }

        //    LogUtility.DebugMethodEnd();
        //    return returnValue;
        //}

        /// <summary>
        /// 単位CD検索&設定
        /// </summary>
        /// <param name="hinmeiChangedFlg">品名CDが更新された後の処理かどうか</param>
        internal bool SearchAndCalcForUnit(bool hinmeiChangedFlg, Row targetRow)
        {
            try
            {
                LogUtility.DebugMethodStart(hinmeiChangedFlg, targetRow);

                if (targetRow == null)
                {
                    return false;
                }

                // 単価前回値取得
                var oldTanka = targetRow.Cells[CELL_NAME_TANKA].Value == null ? string.Empty : targetRow.Cells[CELL_NAME_TANKA].Value.ToString();

                M_UNIT targetUnit = null;

                if (hinmeiChangedFlg)
                {
                    // 品名CD更新時
                    if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value)))
                    {
                        return false;
                    }

                    M_HINMEI hinmei = this.accessor.GetHinmeiDataByCd(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());

                    if (hinmei == null || string.IsNullOrEmpty(hinmei.HINMEI_CD))
                    {
                        return false;
                    }

                    if (targetRow.Cells[CELL_NAME_UNIT_CD].Value == null
                        || string.IsNullOrEmpty(targetRow.Cells[CELL_NAME_UNIT_CD].Value.ToString()))
                    {
                        M_UNIT[] units = null;
                        short UnitCd = 0;
                        if (short.TryParse(hinmei.UNIT_CD.ToString(), out UnitCd))
                            units = this.accessor.GetUnit(UnitCd);

                        if (units == null)
                        {
                            return false;
                        }
                        else
                        {
                            targetUnit = units[0];
                        }

                        if (string.IsNullOrEmpty(targetUnit.UNIT_NAME))
                        {
                            return false;
                        }

                        targetRow.Cells[CELL_NAME_UNIT_CD].Value = targetUnit.UNIT_CD.ToString();
                        targetRow.Cells[CELL_NAME_UNIT_NAME_RYAKU].Value = targetUnit.UNIT_NAME_RYAKU.ToString();
                    }
                }
                else
                {
                    // 単位CD更新時
                }

                short denpyouKbn = 0;
                short unitCd = 0;
                if (!short.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value), out denpyouKbn)
                    || !short.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_UNIT_CD].Value), out unitCd))
                {
                    return false;
                }

                /**
                 * 単価設定
                 */
                var updateTanka = string.Empty; // MAILAN #158993 START
                var kobetsuhinmeiTanka = this.commonAccesser.GetKobetsuhinmeiTanka(
                    (short)SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI,
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
                        (short)SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI,
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
                        updateTanka = kihonHinmeiTanka.TANKA.Value.ToString(); // MAILAN #158993 START
                    }
                    else
                    {
                        updateTanka = string.Empty; // MAILAN #158993 START
                    }
                }
                else
                {
                    updateTanka = kobetsuhinmeiTanka.TANKA.Value.ToString(); // MAILAN #158993 START
                }

                // MAILAN #158993 START
                if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    decimal oldTankaValue = -1;
                    decimal updateTankaValue = -1;
                    if (oldTanka != null && !string.IsNullOrEmpty(oldTanka.ToString()))
                    {
                        oldTankaValue = decimal.Parse(oldTanka.ToString());
                    }
                    if (updateTanka != null && !string.IsNullOrEmpty(updateTanka.ToString()))
                    {
                        updateTankaValue = decimal.Parse(updateTanka.ToString());
                    }

                    if (!oldTankaValue.Equals(updateTankaValue))
                    {
                        if (!this.isTankaMessageShown)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            if (msgLogic.MessageBoxShow("C127") == DialogResult.Yes)
                            {
                                targetRow.Cells[CELL_NAME_TANKA].Value = updateTanka;
                            }
                            else
                            {
                                this.ResetTankaCheck();
                                return true;
                            }
                            this.isTankaMessageShown = true;
                        }
                        else
                        {
                            targetRow.Cells[CELL_NAME_TANKA].Value = updateTanka;
                        }
                    }
                }
                // MAILAN #158993 END
                else // ban chuan
                {
                    targetRow.Cells[CELL_NAME_TANKA].Value = updateTanka.ToString();
                }

                /**
                 * 金額設定
                 */
                var newTanka = targetRow.Cells[CELL_NAME_TANKA].Value == null ? string.Empty : targetRow.Cells[CELL_NAME_TANKA].Value.ToString();

                // 単価に変更があった場合のみ再計算
                if (!oldTanka.Equals(newTanka))
                {
                    bool catchErr = this.CalcDetaiKingaku(targetRow);
                    if (catchErr)
                    {
                        return true;
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchAndCalcForUnit", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchAndCalcForUnit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 明細金額計算
        /// </summary>
        internal void CalcDetaiKingaku()
        {
            LogUtility.DebugMethodStart();

            Row targetRow = this.form.mrwDetail.CurrentRow;

            if (targetRow == null)
            {
                return;
            }

            decimal suuryou = 0;
            decimal tanka = 0;
            short kingakuHasuuCd = 0;

            decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_SUURYOU].EditedFormattedValue), out suuryou);
            decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_TANKA].EditedFormattedValue), out tanka);
            short.TryParse(Convert.ToString(this.dto.torihikisakiSeikyuuEntity.KINGAKU_HASUU_CD), out kingakuHasuuCd);

            targetRow.Cells[CELL_NAME_KINGAKU].Value = CommonCalc.FractionCalc((decimal)suuryou * tanka, kingakuHasuuCd);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先の締日から明細.売上支払日を変更する
        /// UIFormから使用する場合はこちらを呼び出してください
        /// </summary>
        //internal void ChangeNearSeikyuDateForAllRow()
        //{
        //    LogUtility.DebugMethodStart();

        //    foreach (var targetRow in this.form.mrwDetail.Rows)
        //    {
        //        if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value)))
        //        {
        //            targetRow.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value = this.GetNearSeikyuDateForDetail(targetRow);
        //        }
        //    }
        //    LogUtility.DebugMethodEnd();
        //}

        /// <summary>
        /// 取引先の締日から明細.売上支払日を変更する
        /// mrwDetailから使用する場合はこちらから呼び出してください
        /// </summary>
        //internal void ChangeNearSeikyuDateForSelectedRow()
        //{
        //    LogUtility.DebugMethodStart();

        //    Row targetRow = this.form.mrwDetail.CurrentRow;

        //    if (targetRow == null)
        //    {
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value)))
        //    {
        //        return;
        //    }

        //    this.form.mrwDetail.BeginEdit(false);
        //    targetRow.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value = this.GetNearSeikyuDateForDetail(targetRow);
        //    this.form.mrwDetail.EndEdit();
        //    this.form.mrwDetail.NotifyCurrentCellDirty(false);
        //    LogUtility.DebugMethodEnd();
        //}

        /// <summary>
        /// 明細.売上支払日から近くの締日からを請求日(支払日)計算する
        /// </summary>
        /// <param name="targetRow">明細行</param>
        /// <returns>請求日または支払日</returns>
        //private DateTime GetNearSeikyuDateForDetail(Row targetRow)
        //{
        //    LogUtility.DebugMethodStart(targetRow);
        //    DateTime returnVal = (DateTime)targetRow.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value;

        //    if (targetRow == null)
        //    {
        //        return returnVal;
        //    }

        //    // 締日取得
        //    List<short> shimebiList = new List<short>();
        //    short denpyouKbnCd = 0;
        //    short.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value), out denpyouKbnCd);
        //    if (denpyouKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
        //    {
        //        // 請求先
        //        if (this.dto.torihikisakiSeikyuuEntity == null)
        //        {
        //            return returnVal;
        //        }

        //        if (0 < this.dto.torihikisakiSeikyuuEntity.SHIMEBI1) shimebiList.Add((short)this.dto.torihikisakiSeikyuuEntity.SHIMEBI1);
        //        if (0 < this.dto.torihikisakiSeikyuuEntity.SHIMEBI2) shimebiList.Add((short)this.dto.torihikisakiSeikyuuEntity.SHIMEBI2);
        //        if (0 < this.dto.torihikisakiSeikyuuEntity.SHIMEBI3) shimebiList.Add((short)this.dto.torihikisakiSeikyuuEntity.SHIMEBI3);
        //    }
        //    else if (denpyouKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI)
        //    {
        //        // 支払先
        //        if (this.dto.torihikisakiShiharaiEntity == null)
        //        {
        //            return returnVal;
        //        }

        //        if (0 < this.dto.torihikisakiShiharaiEntity.SHIMEBI1) shimebiList.Add((short)this.dto.torihikisakiShiharaiEntity.SHIMEBI1);
        //        if (0 < this.dto.torihikisakiShiharaiEntity.SHIMEBI2) shimebiList.Add((short)this.dto.torihikisakiShiharaiEntity.SHIMEBI2);
        //        if (0 < this.dto.torihikisakiShiharaiEntity.SHIMEBI3) shimebiList.Add((short)this.dto.torihikisakiShiharaiEntity.SHIMEBI3);
        //    }
        //    if (shimebiList.Count < 1)
        //    {
        //        return returnVal;
        //    }

        //    if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value)))
        //    {
        //        var seikyuuUtil = new SeiKyuuUtility();
        //        returnVal = (seikyuuUtil.GetNearSeikyuDate((DateTime)targetRow.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value, shimebiList.ToArray())).Date;
        //    }

        //    LogUtility.DebugMethodEnd();
        //    return returnVal;

        //}

        /// <summary>
        /// Logic内で定義されているEntityすべての最新情報を取得する
        /// </summary>
        /// <returns>true:正常値、false:エラー発生</returns>
        public bool GetAllEntityData(out bool catchErr)
        {
            try
            {
                catchErr = false;
                // 更新前データを保持しておく
                this.beforDto = new DTOClass();

                // 画面のモードに依存しないデータの取得
                this.dto.sysInfoEntity = CommonShogunData.SYS_INFO;

                // TODO: CommonShogunDataのCreateメソッドをちゃんとログイン時に呼んでいるか確認

                if (!this.IsRequireData())
                {
                    return true;
                }

                // 受入/支払入力
                var entrys = accessor.GetUrshEntry(this.form.UrShNumber, this.form.SEQ);
                if (entrys == null || entrys.Length < 1)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    return false;
                }
                else
                {
                    this.dto.entryEntity = entrys[0];
                }

                // 受入明細
                var ditails = accessor.GetUrshDetail(this.dto.entryEntity.SYSTEM_ID, this.dto.entryEntity.SEQ);
                if (ditails == null || ditails.Length < 1)
                {
                    this.dto.detailEntity = new T_UR_SH_DETAIL[] { new T_UR_SH_DETAIL() };
                }
                else
                {
                    this.dto.detailEntity = ditails;
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

                // コンテナ状況
                this.dto.contenaEntity = new M_CONTENA_JOUKYOU();
                if (!this.dto.entryEntity.CONTENA_SOUSA_CD.IsNull)
                {
                    var contenaJoukyou = this.accessor.GetContenaJoukyou((short)this.dto.entryEntity.CONTENA_SOUSA_CD);
                    if (contenaJoukyou != null)
                    {
                        this.dto.contenaEntity = contenaJoukyou;
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

                // マニフェスト種類
                this.dto.manifestShuruiEntity = new M_MANIFEST_SHURUI();
                this.dto.manifestEntrys = this.accessor.GetManifestEntry(this.dto.detailEntity);
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

                //2次
                //コンテナ稼働実績
                var contenaResultEntity = this.accessor.GetContena(this.dto.entryEntity.SYSTEM_ID.ToString());
                this.dto.contenaResultList = new List<T_CONTENA_RESULT>();
                if (contenaResultEntity != null)
                {
                    foreach (T_CONTENA_RESULT entity in contenaResultEntity)
                    {
                        this.dto.contenaResultList.Add(entity);
                    }
                }

                this.beforDto = this.dto.Clone();
                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("GetAllEntityData", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("GetAllEntityData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 売上日付を近々の締日に変更
        /// </summary>
        //internal void ChangeSeikyuuShimeDate()
        //{
        //    if (this.dto.torihikisakiSeikyuuEntity == null)
        //    {
        //        return;
        //    }

        //    List<short> shimebiList = new List<short>();
        //    if (0 < this.dto.torihikisakiSeikyuuEntity.SHIMEBI1) shimebiList.Add((short)this.dto.torihikisakiSeikyuuEntity.SHIMEBI1);
        //    if (0 < this.dto.torihikisakiSeikyuuEntity.SHIMEBI2) shimebiList.Add((short)this.dto.torihikisakiSeikyuuEntity.SHIMEBI2);
        //    if (0 < this.dto.torihikisakiSeikyuuEntity.SHIMEBI3) shimebiList.Add((short)this.dto.torihikisakiSeikyuuEntity.SHIMEBI3);

        //    if (shimebiList.Count < 1)
        //    {
        //        return;
        //    }

        //    if (!string.IsNullOrEmpty(Convert.ToString(this.form.URIAGE_DATE.Value)))
        //    {
        //        var seikyuuUtil = new SeiKyuuUtility();
        //        this.form.URIAGE_DATE.Text = (seikyuuUtil.GetNearSeikyuDate((DateTime)this.form.URIAGE_DATE.Value, shimebiList.ToArray())).Date.ToString();
        //    }
        //}

        /// <summary>
        /// 支払日付を近々の締日に変更
        /// </summary>
        //internal void ChangeShiharaiShimeDate()
        //{
        //    if (this.dto.torihikisakiShiharaiEntity == null)
        //    {
        //        return;
        //    }

        //    List<short> shimebiList = new List<short>();
        //    if (0 < this.dto.torihikisakiShiharaiEntity.SHIMEBI1) shimebiList.Add((short)this.dto.torihikisakiShiharaiEntity.SHIMEBI1);
        //    if (0 < this.dto.torihikisakiShiharaiEntity.SHIMEBI2) shimebiList.Add((short)this.dto.torihikisakiShiharaiEntity.SHIMEBI2);
        //    if (0 < this.dto.torihikisakiShiharaiEntity.SHIMEBI3) shimebiList.Add((short)this.dto.torihikisakiShiharaiEntity.SHIMEBI3);

        //    if (shimebiList.Count < 1)
        //    {
        //        return;
        //    }

        //    if (!string.IsNullOrEmpty(Convert.ToString(this.form.SHIHARAI_DATE.Value)))
        //    {
        //        var seikyuuUtil = new SeiKyuuUtility();
        //        this.form.SHIHARAI_DATE.Text = (seikyuuUtil.GetNearSeikyuDate((DateTime)this.form.SHIHARAI_DATE.Value, shimebiList.ToArray())).Date.ToString();
        //    }
        //}

        /// <summary>
        /// ヘッダーの拠点CDの存在チェック
        /// </summary>
        internal bool CheckKyotenCd()
        {
            try
            {
                // 初期化
                this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;

                if (string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
                {
                    this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
                    return false;
                }

                short kyoteCd = -1;
                if (!short.TryParse(string.Format("{0:#,0}", this.headerForm.KYOTEN_CD.Text), out kyoteCd))
                {
                    return false;
                }

                var kyotens = this.accessor.GetDataByCodeForKyoten(kyoteCd);

                // 存在チェック
                if (kyotens == null || kyotens.Length < 1)
                {
                    this.headerForm.KYOTEN_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "拠点");
                    this.headerForm.KYOTEN_CD.Focus();
                    return false;
                }
                else
                {
                    // キーが１つなので複数はヒットしないはず
                    M_KYOTEN kyoten = kyotens[0];
                    this.headerForm.KYOTEN_NAME_RYAKU.Text = kyoten.KYOTEN_NAME_RYAKU.ToString();
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckKyotenCd", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        private string nizumiGyoushaHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 荷積業者CDの存在チェック
        /// </summary>
        public virtual bool CheckNizumiGyoushaCd()
        {
            LogUtility.DebugMethodStart();
            bool ret = false;
            bool isError = false;

            var msgLogic = new MessageBoxShowLogic();
            var inputNioroshiGyoushaCd = this.form.NIZUMI_GYOUSHA_CD.Text;

            // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
            if (String.IsNullOrEmpty(inputNioroshiGyoushaCd) || !this.tmpNizumiGyoushaCd.Equals(inputNioroshiGyoushaCd)
                || string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_NAME.Text))
            {
                // 初期化
                this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
                this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = true;
                this.form.NIZUMI_GYOUSHA_NAME.Tag = string.Empty;
                this.form.NIZUMI_GYOUSHA_NAME.TabStop = false;
                if (!this.tmpNizumiGyoushaCd.Equals(inputNioroshiGyoushaCd))
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
                }
                else
                {
                    bool catchErr = false;
                    var gyousha = this.accessor.GetGyousha(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                    if (gyousha != null)
                    {
                        // PKは1つなので複数ヒットしない
                        // 20151026 BUNN #12040 STR
                        if (gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                        // 20151026 BUNN #12040 END
                        {
                            // 荷積業者名
                            this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                            if (gyousha.SHOKUCHI_KBN.IsTrue)
                            {
                                this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                                this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = false;
                                //this.form.NIZUMI_GYOUSHA_NAME.TabStop = true;
                                this.form.NIZUMI_GYOUSHA_NAME.TabStop = GetTabStop("NIZUMI_GYOUSHA_NAME");    // No.3822
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
                                            && (genbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue || genbaEntity.TSUMIKAEHOKAN_KBN.IsTrue))
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
                                            this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME1;
                                            this.form.NIZUMI_GENBA_NAME.ReadOnly = false;
                                            this.form.NIZUMI_GENBA_NAME.TabStop = GetTabStop("NIZUMI_GENBA_NAME");    // No.3822
                                            this.form.NIZUMI_GENBA_NAME.Tag = this.nizumiGenbaHintText;
                                            this.form.NIZUMI_GENBA_NAME.Focus();
                                        }
                                        else
                                        {
                                            this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            // エラーメッセージ
                            msgLogic.MessageBoxShow("E058", "");
                            this.form.NIZUMI_GYOUSHA_CD.Focus();
                            isError = true;
                            ret = false;
                        }
                    }
                    else
                    {
                        msgLogic.MessageBoxShow("E020", "荷積業者");
                        this.form.NIZUMI_GYOUSHA_CD.Focus();
                        isError = true;
                        ret = false;
                    }
                }
            }
            LogUtility.DebugMethodEnd();

            return ret;
        }

        private string nizumiGenbaHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 荷積現場CDの存在チェック
        /// </summary>
        public virtual bool CheckNizumiGenbaCd()
        {
            LogUtility.DebugMethodStart();

            bool ret = false;
            bool isError = false;

            var msgLogic = new MessageBoxShowLogic();
            var inputNIZUMIGenbaCd = this.form.NIZUMI_GENBA_CD.Text;

            // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
            if ((String.IsNullOrEmpty(inputNIZUMIGenbaCd) || !this.tmpNizumiGenbaCd.Equals(inputNIZUMIGenbaCd))
                || this.form.isFromSearchButton || string.IsNullOrEmpty(this.form.NIZUMI_GENBA_NAME.Text))
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
                }
                else
                {
                    if (string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
                    {
                        this.form.isInputError = true;
                        msgLogic.MessageBoxShow("E051", "荷積業者");
                        this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                        this.form.NIZUMI_GENBA_CD.Focus();
                        isError = true;
                        ret = false;
                        return ret;
                    }

                    //var genbaEntityList = this.accessor.GetGenba(this.form.NIZUMI_GENBA_CD.Text);
                    var genbaEntityList = this.accessor.GetGenbaList(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.NIZUMI_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                    M_GENBA genba = new M_GENBA();

                    if (genbaEntityList == null || genbaEntityList.Length < 1)
                    {
                        this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E020", "現場");
                        this.form.NIZUMI_GENBA_CD.Focus();
                        isError = true;
                    }
                    else
                    {
                        //genba = this.accessor.GetGenba(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.NIZUMI_GENBA_CD.Text);
                        genba = genbaEntityList[0];
                        // 荷積業者名入力チェック
                        if (string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
                        {
                            // エラーメッセージ
                            this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                            msgLogic.MessageBoxShow("E051", "荷積業者");
                            this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                            this.form.NIZUMI_GENBA_CD.Focus();
                            isError = true;
                        }
                        // 荷積業者と荷積現場の関連チェック
                        else if (genba == null)
                        {
                            // 一致するデータがないのでエラー
                            this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                            msgLogic.MessageBoxShow("E062", "荷積業者");
                            this.form.NIZUMI_GENBA_CD.Focus();
                            isError = true;
                        }
                        else
                        {
                            bool catchErr = false;
                            // 業者設定
                            var gyousha = this.accessor.GetGyousha(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                            if (catchErr)
                            {
                                throw new Exception("");
                            }
                            if (gyousha != null)
                            {
                                // PKは1つなので複数ヒットしない
                                // 20151026 BUNN #12040 STR
                                if (gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                                // 20151026 BUNN #12040 END
                                {
                                    this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                                    // 荷卸業者名
                                    this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                                    if (gyousha.SHOKUCHI_KBN.IsTrue)
                                    {
                                        this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                                        this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = false;
                                        this.form.NIZUMI_GYOUSHA_NAME.TabStop = GetTabStop("NIZUMI_GYOUSHA_NAME");
                                        this.form.NIZUMI_GYOUSHA_NAME.Tag = this.nizumiGyoushaHintText;
                                    }
                                }
                            }

                            // 事業場区分、現場区分チェック
                            // 20151026 BUNN #12040 STR
                            if (genba.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue || genba.TSUMIKAEHOKAN_KBN.IsTrue)
                            // 20151026 BUNN #12040 END
                            {
                                this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;

                                // 諸口区分チェック
                                if (genba.SHOKUCHI_KBN.IsTrue)
                                {
                                    // 荷積現場名編集可
                                    this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME1;
                                    this.form.NIZUMI_GENBA_NAME.ReadOnly = false;
                                    //this.form.NIZUMI_GENBA_NAME.TabStop = true;
                                    this.form.NIZUMI_GENBA_NAME.TabStop = GetTabStop("NIZUMI_GENBA_NAME");    // No.3822
                                    this.form.NIZUMI_GENBA_NAME.Tag = this.nizumiGenbaHintText;
                                    this.form.NIZUMI_GENBA_NAME.Focus();
                                }

                                if (this.form.oldShokuchiKbn)
                                {
                                    ret = true;
                                }
                            }
                            else
                            {
                                // 一致するデータがないのでエラー
                                this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                                msgLogic.MessageBoxShow("E058", "");
                                this.form.NIZUMI_GENBA_CD.Focus();
                                isError = true;
                            }
                        }
                    }
                }
            }
            LogUtility.DebugMethodEnd();

            return ret;
        }

        private string nioroshiGyoushaHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 荷降業者チェック
        /// </summary>
        internal bool CheckNioroshiGyoushaCd()
        {
            LogUtility.DebugMethodStart();

            bool ret = false;
            bool isError = false;

            var msgLogic = new MessageBoxShowLogic();
            var inputNioroshiGyoushaCd = this.form.NIOROSHI_GYOUSHA_CD.Text;

            // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
            if (String.IsNullOrEmpty(inputNioroshiGyoushaCd) || !this.tmpNioroshiGyoushaCd.Equals(inputNioroshiGyoushaCd)
                 || string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_NAME.Text))
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
                }
                else
                {
                    bool catchErr = false;
                    var gyousha = this.accessor.GetGyousha(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                    if (gyousha != null)
                    {
                        // PKは1つなので複数ヒットしない
                        // 20151026 BUNN #12040 STR
                        if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue || gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue)
                        // 20151026 BUNN #12040 END
                        {
                            // 荷卸業者名
                            this.form.NIOROSHI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                            if (gyousha.SHOKUCHI_KBN.IsTrue)
                            {
                                this.form.NIOROSHI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                                this.form.NIOROSHI_GYOUSHA_NAME.ReadOnly = false;
                                //this.form.NIOROSHI_GYOUSHA_NAME.TabStop = true;
                                this.form.NIOROSHI_GYOUSHA_NAME.TabStop = GetTabStop("NIOROSHI_GYOUSHA_NAME");    // No.3822
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
                                            && (genbaEntity.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || genbaEntity.SAISHUU_SHOBUNJOU_KBN.IsTrue || genbaEntity.TSUMIKAEHOKAN_KBN.IsTrue))
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
                                            this.form.NIOROSHI_GENBA_NAME.TabStop = GetTabStop("NIOROSHI_GENBA_NAME");    // No.3822
                                            this.form.NIOROSHI_GENBA_NAME.Tag = this.nioroshiGenbaHintText;
                                            this.form.NIOROSHI_GENBA_NAME.Text = genba.GENBA_NAME1;
                                            this.form.NIOROSHI_GENBA_NAME.Focus();
                                        }
                                        else
                                        {
                                            this.form.NIOROSHI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            // エラーメッセージ
                            msgLogic.MessageBoxShow("E058", "");
                            this.form.NIOROSHI_GYOUSHA_CD.Focus();
                            isError = true;
                        }
                    }
                    else
                    {
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
                        bool catchErr = false;
                        foreach (Row r in this.form.mrwDetail.Rows.Where(r => !r.IsNewRow).ToList())
                        {
                            catchErr = this.SearchAndCalcForUnit(false, r);
                            if (catchErr)
                            {
                                return false; // MAILAN #158993 START
                            }
                        }
                        this.ResetTankaCheck(); // MAILAN #158993 START

                        // 合計金額の再計算
                        if (!this.CalcTotalValues())
                        {
                            return false;
                        }
                    }
                }
            }
            LogUtility.DebugMethodEnd();

            return ret;
        }

        private string nioroshiGenbaHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 荷降現場CDの存在チェック
        /// </summary>
        internal bool CheckNioroshiGenbaCd()
        {
            LogUtility.DebugMethodStart();

            bool ret = false;
            bool isError = false;

            var msgLogic = new MessageBoxShowLogic();
            var inputNioroshiGenbaCd = this.form.NIOROSHI_GENBA_CD.Text;

            // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
            if ((String.IsNullOrEmpty(inputNioroshiGenbaCd) || !this.tmpNioroshiGenbaCd.Equals(inputNioroshiGenbaCd))
                || this.form.isFromSearchButton || string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_NAME.Text))
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
                }
                else
                {
                    if (string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
                    {
                        this.form.isInputError = true;
                        msgLogic.MessageBoxShow("E051", "荷降業者");
                        this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                        this.form.NIOROSHI_GENBA_CD.Focus();
                        isError = true;
                        ret = false;
                        return ret;
                    }

                    //var genbaEntityList = this.accessor.GetGenba(this.form.NIOROSHI_GENBA_CD.Text);
                    var genbaEntityList = this.accessor.GetGenbaList(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.NIOROSHI_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                    M_GENBA genba = new M_GENBA();

                    if (genbaEntityList == null || genbaEntityList.Length < 1)
                    {
                        this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E020", "現場");
                        this.form.NIOROSHI_GENBA_CD.Focus();
                        isError = true;
                    }
                    else
                    {
                        //genba = this.accessor.GetGenba(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.NIOROSHI_GENBA_CD.Text);
                        genba = genbaEntityList[0];
                        // 荷卸業者名入力チェック
                        if (string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
                        {
                            // エラーメッセージ
                            this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                            msgLogic.MessageBoxShow("E051", "荷降業者");
                            this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                            this.form.NIOROSHI_GENBA_CD.Focus();
                            isError = true;
                        }
                        // 荷降業者と荷降現場の関連チェック
                        else if (genba == null)
                        {
                            // 一致するデータがないのでエラー
                            this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                            msgLogic.MessageBoxShow("E062", "荷降業者");
                            this.form.NIOROSHI_GENBA_CD.Focus();
                            isError = true;
                        }
                        else
                        {
                            bool catchErr = false;
                            // 業者設定
                            var gyousha = this.accessor.GetGyousha(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                            if (catchErr)
                            {
                                throw new Exception("");
                            }
                            if (gyousha != null)
                            {
                                // PKは1つなので複数ヒットしない
                                // 20151026 BUNN #12040 STR
                                if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue || gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue)
                                // 20151026 BUNN #12040 END
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
                                }
                            }

                            // 積み替え保管区分、処分事業場区分、最終処分場区分、荷積降現場区分チェック
                            // 20151026 BUNN #12040 STR
                            if (genba.TSUMIKAEHOKAN_KBN.IsTrue || genba.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || genba.SAISHUU_SHOBUNJOU_KBN.IsTrue)
                            // 20151026 BUNN #12040 END
                            {
                                this.form.NIOROSHI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;

                                // 諸口区分チェック
                                if (genba.SHOKUCHI_KBN.IsTrue)
                                {
                                    // 荷卸現場名編集可
                                    this.form.NIOROSHI_GENBA_NAME.Text = genba.GENBA_NAME1;
                                    this.form.NIOROSHI_GENBA_NAME.ReadOnly = false;
                                    //this.form.NIOROSHI_GENBA_NAME.TabStop = true;
                                    this.form.NIOROSHI_GENBA_NAME.TabStop = GetTabStop("NIOROSHI_GENBA_NAME");    // No.3822
                                    this.form.NIOROSHI_GENBA_NAME.Tag = this.nioroshiGenbaHintText;
                                    this.form.NIOROSHI_GENBA_NAME.Focus();
                                }

                                if (this.form.oldShokuchiKbn)
                                {
                                    ret = true;
                                }
                            }
                            else
                            {
                                // 一致するデータがないのでエラー
                                this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                                msgLogic.MessageBoxShow("E058", "");
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
                        bool catchErr = false;
                        foreach (Row r in this.form.mrwDetail.Rows.Where(r => !r.IsNewRow).ToList())
                        {
                            catchErr = this.SearchAndCalcForUnit(false, r);
                            if (catchErr)
                            {
                                return false; // MAILAN #158993 START
                            }
                        }
                        this.ResetTankaCheck(); // MAILAN #158993 START

                        // 合計金額の再計算
                        if (!this.CalcTotalValues())
                        {
                            return false;
                        }
                    }
                }
            }

            LogUtility.DebugMethodEnd();

            return ret;
        }

        private string unpanGyoushaHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 運搬業者CDの存在チェック
        /// </summary>
        internal bool CheckUnpanGyoushaCd()        
        {
            LogUtility.DebugMethodStart();

            bool ret = false;
            bool isError = false;

            var msgLogic = new MessageBoxShowLogic();
            var inputUnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
            //20150806 hoanghm edit #10666
            bool isErrorGyousha = false;
            //20150806 hoanghm end edit #10666

            // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
            if ((String.IsNullOrEmpty(inputUnpanGyoushaCd) || !this.tmpUnpanGyoushaCd.Equals(inputUnpanGyoushaCd)) || this.form.isFromSearchButton || this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured)
            {
                // 初期化
                this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_NAME.ReadOnly = true;
                this.form.UNPAN_GYOUSHA_NAME.TabStop = false;
                this.form.UNPAN_GYOUSHA_NAME.Tag = string.Empty;

                bool catchErr = false;
                var gyousha = this.accessor.GetGyousha(this.form.UNPAN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr)
                {
                    throw new Exception("");
                }
                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    if (gyousha != null)
                    {
                        // 20151026 BUNN #12040 STR
                        if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                        // 20151026 BUNN #12040 END
                        {
                            M_SHARYOU[] sharyouEntitys = null;
                            sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null, SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()));

                            this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                            this.form.SHARYOU_CD.AutoChangeBackColorEnabled = true;

                            if (sharyouEntitys == null)
                            {
                                if (!this.form.oldSharyouShokuchiKbn)
                                {
                                    // 車輌・車種をクリア
                                    this.form.SHARYOU_CD.Text = string.Empty;
                                    this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
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
                            }

                            this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                            // 諸口区分チェック
                            if (gyousha.SHOKUCHI_KBN.IsTrue)
                            {
                                // 運搬業者名編集可
                                this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                                this.form.UNPAN_GYOUSHA_NAME.ReadOnly = false;
                                //this.form.UNPAN_GYOUSHA_NAME.TabStop = true;
                                this.form.UNPAN_GYOUSHA_NAME.TabStop = GetTabStop("UNPAN_GYOUSHA_NAME");    // No.3822
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
                            //20150806 hoanghm edit #10666
                            //msgLogic.MessageBoxShow("E020", "業者");
                            //this.form.UNPAN_GYOUSHA_CD.Focus();

                            //this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                            //isError = true;
                            isErrorGyousha = true;
                            //20150806 hoanghm end edit #10666
                        }
                    }
                    //20150806 hoanghm edit #10666
                    else
                    {
                        isErrorGyousha = true;
                    }
                    //20150806 hoanghm end edit #10666
                    if (isErrorGyousha)
                    {
                        msgLogic.MessageBoxShow("E020", "業者");
                        this.form.UNPAN_GYOUSHA_CD.Focus();

                        this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
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
                    }
                }

                if (!isError)
                {
                    if (!this.tmpUnpanGyoushaCd.Equals(inputUnpanGyoushaCd))
                    {
                        // 明細行すべての単価を再設定
                        catchErr = false;
                        foreach (Row r in this.form.mrwDetail.Rows.Where(r => !r.IsNewRow).ToList())
                        {
                            catchErr = this.SearchAndCalcForUnit(false, r);
                            if (catchErr)
                            {
                                return false; // MAILAN #158993 START
                            }
                        }
                        this.ResetTankaCheck(); // MAILAN #158993 START

                        // 合計金額の再計算
                        if (!this.CalcTotalValues())
                        {
                            return false;
                        }
                    }
                }
            }
            LogUtility.DebugMethodEnd();

            return ret;
        }

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
                strNyuryokuTantousyaName = string.Empty;  // No.3279

                if (string.IsNullOrEmpty(this.form.NYUURYOKU_TANTOUSHA_CD.Text))
                {
                    // 入力担当者CDがなければ既にエラーが表示されているはずなので何もしない
                    return false;
                }

                var shainEntity = this.accessor.GetShain(this.form.NYUURYOKU_TANTOUSHA_CD.Text);
                if (shainEntity == null)
                {
                    return false;
                }
                else if (shainEntity.NYUURYOKU_TANTOU_KBN.IsFalse)
                {
                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E058", "");
                    this.form.NYUURYOKU_TANTOUSHA_CD.Focus();
                    return false;
                }
                else
                {
                    this.form.NYUURYOKU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                    strNyuryokuTantousyaName = shainEntity.SHAIN_NAME;    // No.3279
                }
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckNyuuryokuTantousha", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNyuuryokuTantousha", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        private string torihikisakiHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 取引先チェック
        /// </summary>
        internal bool CheckTorihikisaki()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            bool isError = false;

            var msgLogic = new MessageBoxShowLogic();
            var inputTorihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
            var oldTorihikisakiCd = this.tmpTorihikisakiCd;

            // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
            //if ((String.IsNullOrEmpty(inputTorihikisakiCd) || !this.tmpTorihikisakiCd.Equals(inputTorihikisakiCd)) || this.form.isFromSearchButton)   // No.4095
            if ((String.IsNullOrEmpty(inputTorihikisakiCd) || !this.tmpTorihikisakiCd.Equals(inputTorihikisakiCd) ||
                (this.tmpTorihikisakiCd.Equals(inputTorihikisakiCd) && string.IsNullOrEmpty(this.form.TORIHIKISAKI_NAME_RYAKU.Text)))     // No.4095(ID変更無い場合でもNAMEが空の場合はチェックするように変更)
                || this.form.isFromSearchButton)
            {
                //　初期化
                //this.tmpTorihikisakiCd = string.Empty;
                this.form.isInputError = false;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
                this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
                this.form.TORIHIKISAKI_NAME_RYAKU.Tag = string.Empty;
                this.form.SEIKYUU_SHIMEBI1.Text = string.Empty;
                this.form.SEIKYUU_SHIMEBI2.Text = string.Empty;
                this.form.SEIKYUU_SHIMEBI3.Text = string.Empty;
                this.form.SHIHARAI_SHIMEBI1.Text = string.Empty;
                this.form.SHIHARAI_SHIMEBI2.Text = string.Empty;
                this.form.SHIHARAI_SHIMEBI3.Text = string.Empty;
                this.form.txtUri.Text = string.Empty;
                this.form.txtShi.Text = string.Empty;

                if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    bool catchErr = false;
                    var torihikisakiEntity = this.accessor.GetTorihikisaki(inputTorihikisakiCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                    if (catchErr)
                    {
                        throw new Exception("");
                    } 
                    if (null == torihikisakiEntity)
                    {
                        msgLogic.MessageBoxShow("E020", "取引先");
                        this.form.isInputError = true;
                        this.form.TORIHIKISAKI_CD.Focus();
                        isError = true;
                        ret = false;
                    }
                    else
                    {
                        bool isCheckTori = CheckTorihikisakiAndKyotenCd(torihikisakiEntity, this.form.TORIHIKISAKI_CD.Text, out catchErr);
                        if (catchErr) { throw new Exception(""); }
                        if (isCheckTori)
                        {
                            // 取引先の拠点と入力された拠点コードの関連チェックOK
                            this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                            this.tmpTorihikisakiCd = torihikisakiEntity.TORIHIKISAKI_CD;
                        }
                        else
                        {
                            this.form.TORIHIKISAKI_CD.Focus();
                            this.form.isInputError = true;
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
                            //this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = true;
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

                        // 請求締日チェック
                        this.CheckSeikyuuShimebi();

                        // 支払い締日チェック
                        this.CheckShiharaiShimebi();

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
                        this.setEigyou_Tantousha(this.form.GENBA_CD.Text, this.form.GYOUSHA_CD.Text, this.form.TORIHIKISAKI_CD.Text);
                        this.SetShuukeiKomoku(this.form.GENBA_CD.Text, this.form.GYOUSHA_CD.Text, this.form.TORIHIKISAKI_CD.Text); //PhuocLoc 2020/12/01 #136221
                    }

                    if (!oldTorihikisakiCd.Equals(inputTorihikisakiCd) && !this.isCheckTankaFromChild) // MAILAN #158993 START
                    {
                        // 明細行すべての単価を再設定
                        bool catchErr = false;
                        foreach (Row r in this.form.mrwDetail.Rows.Where(r => !r.IsNewRow).ToList())
                        {
                            catchErr = this.SearchAndCalcForUnit(false, r);
                            if (catchErr)
                            {
                                this.form.isInputError = true;
                                return false; // MAILAN #158993 START
                            }
                        }
                        this.ResetTankaCheck(); // MAILAN #158993 START

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

            LogUtility.DebugMethodEnd();

            return ret;
        }

        /// <summary>
        /// 取引先の拠点コードと入力された拠点コードの関連チェック
        /// </summary>
        /// <param name="torihikisakiEntity">取引先エンティティ</param>
        /// <param name="TorihikisakiCd">取引先CD</param>
        /// <returns>True：チェックOK False：チェックNG</returns>
        internal bool CheckTorihikisakiAndKyotenCd(M_TORIHIKISAKI torihikisakiEntity, string TorihikisakiCd, out bool catchErr)
        {
            catchErr = false;
            try
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
                    torihikisakiEntity = this.accessor.GetTorihikisaki(TorihikisakiCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                    if (catchErr)
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
                        returnVal = true;   // No.2865
                    }
                }
                else
                {
                    returnVal = true;
                    return returnVal;
                }

                return returnVal;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckTorihikisakiAndKyotenCd", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTorihikisakiAndKyotenCd", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// <summary>
        /// 請求締日チェック
        /// </summary>
        internal void CheckSeikyuuShimebi()
        {
            LogUtility.DebugMethodStart();
            if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                this.form.SEIKYUU_SHIMEBI1.Text = "";
                this.form.SEIKYUU_SHIMEBI2.Text = "";
                this.form.SEIKYUU_SHIMEBI3.Text = "";
                return;
            }

            var torihikisakiSeikyuuEntity = this.accessor.GetTorihikisakiSeikyuu(this.form.TORIHIKISAKI_CD.Text);
            if (torihikisakiSeikyuuEntity != null)
            {
                // 締日1
                if (!torihikisakiSeikyuuEntity.SHIMEBI1.IsNull)
                {
                    this.form.SEIKYUU_SHIMEBI1.Text = torihikisakiSeikyuuEntity.SHIMEBI1.ToString();
                }

                // 締日2
                if (!torihikisakiSeikyuuEntity.SHIMEBI2.IsNull)
                {
                    this.form.SEIKYUU_SHIMEBI2.Text = torihikisakiSeikyuuEntity.SHIMEBI2.ToString();
                }

                // 締日3
                if (!torihikisakiSeikyuuEntity.SHIMEBI3.IsNull)
                {
                    this.form.SEIKYUU_SHIMEBI3.Text = torihikisakiSeikyuuEntity.SHIMEBI3.ToString();
                }
            }

            // DBアクセスしないようここで設定
            this.dto.torihikisakiSeikyuuEntity = torihikisakiSeikyuuEntity;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 支払締日チェック
        /// </summary>
        internal void CheckShiharaiShimebi()
        {
            LogUtility.DebugMethodStart();
            if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                this.form.SHIHARAI_SHIMEBI1.Text = "";
                this.form.SHIHARAI_SHIMEBI2.Text = "";
                this.form.SHIHARAI_SHIMEBI3.Text = "";
                return;
            }

            var torihikisakiShiharaiEntity = this.accessor.GetTorihikisakiShiharai(this.form.TORIHIKISAKI_CD.Text);
            if (torihikisakiShiharaiEntity != null)
            {
                // 締日1
                if (!torihikisakiShiharaiEntity.SHIMEBI1.IsNull)
                {
                    this.form.SHIHARAI_SHIMEBI1.Text = torihikisakiShiharaiEntity.SHIMEBI1.ToString();
                }

                // 締日2
                if (!torihikisakiShiharaiEntity.SHIMEBI2.IsNull)
                {
                    this.form.SHIHARAI_SHIMEBI2.Text = torihikisakiShiharaiEntity.SHIMEBI2.ToString();
                }

                // 締日3
                if (!torihikisakiShiharaiEntity.SHIMEBI3.IsNull)
                {
                    this.form.SHIHARAI_SHIMEBI3.Text = torihikisakiShiharaiEntity.SHIMEBI3.ToString();
                }
            }

            // DBアクセスをなくすためここで設定
            this.dto.torihikisakiShiharaiEntity = torihikisakiShiharaiEntity;

            LogUtility.DebugMethodEnd();
        }

        private string gyoushaHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 業者チェック
        /// </summary>
        internal bool CheckGyousha()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            bool isError = false;

            var msgLogic = new MessageBoxShowLogic();
            var inputGyoushaCd = this.form.GYOUSHA_CD.Text;

            int rowindex = 0;
            int cellindex = 0;
            bool isChageCurrentCell = false;

            // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
            //if ((String.IsNullOrEmpty(inputGyoushaCd) || !this.tmpGyousyaCd.Equals(inputGyoushaCd)) || this.form.isFromSearchButton)     // No.4095
            if ((String.IsNullOrEmpty(inputGyoushaCd) || !this.tmpGyousyaCd.Equals(inputGyoushaCd) ||
                (this.tmpGyousyaCd.Equals(inputGyoushaCd) && string.IsNullOrEmpty(this.form.GYOUSHA_NAME_RYAKU.Text)))     // No.4095(ID変更無い場合でもNAMEが空の場合はチェックするように変更)
                || this.form.isFromSearchButton)
            {
                // 初期化
                //this.tmpGyousyaCd = string.Empty;
                this.form.isInputError = false;
                this.form.GYOUSHA_NAME_RYAKU.Text = String.Empty;
                this.form.GYOUSHA_NAME_RYAKU.ReadOnly = true;
                this.form.GYOUSHA_NAME_RYAKU.Tag = String.Empty;
                this.form.GYOUSHA_NAME_RYAKU.TabStop = false;
                this.form.GENBA_CD.Text = String.Empty;
                this.form.GENBA_NAME_RYAKU.Text = String.Empty;
                this.form.GENBA_NAME_RYAKU.ReadOnly = true;
                this.form.GENBA_NAME_RYAKU.Tag = String.Empty;
                this.form.GENBA_NAME_RYAKU.TabStop = false;

                if (String.IsNullOrEmpty(inputGyoushaCd))
                {
                    // 同時に現場コードもクリア
                    this.form.GENBA_CD.Text = String.Empty;
                    this.form.GENBA_NAME_RYAKU.Text = String.Empty;
                    this.form.GENBA_NAME_RYAKU.ReadOnly = true;
                    this.form.GENBA_NAME_RYAKU.Tag = String.Empty;
                    this.form.GENBA_NAME_RYAKU.TabStop = false;
                    strGenbaName = string.Empty;   // No.3279

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
                else
                {
                    bool catchErr = false;
                    var gyoushaEntity = this.accessor.GetGyousha(inputGyoushaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                    if (null == gyoushaEntity)
                    {
                        // エラーメッセージ
                        msgLogic.MessageBoxShow("E020", "業者");
                        this.form.GYOUSHA_CD.Focus();
                        this.form.isInputError = true;
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
                            this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME1;
                            this.form.GYOUSHA_NAME_RYAKU.ReadOnly = false;
                            //this.form.GYOUSHA_NAME_RYAKU.TabStop = true;
                            this.form.GYOUSHA_NAME_RYAKU.TabStop = GetTabStop("GYOUSHA_NAME_RYAKU");    // No.3822
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
                        var torihikisakiEntity = this.accessor.GetTorihikisaki(gyoushaEntity.TORIHIKISAKI_CD, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                        if (catchErr)
                        {
                            throw new Exception("");
                        }
                        if (null != torihikisakiEntity)
                        {
                            this.form.TORIHIKISAKI_CD.Text = gyoushaEntity.TORIHIKISAKI_CD;
                            this.isCheckTankaFromChild = true; // MAILAN #158993 START
                            // 取引先チェック呼び出し
                            ret = this.CheckTorihikisaki();
                        }
                        else
                        {
                            this.form.TORIHIKISAKI_CD.Text = string.Empty;
                            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                            this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
                            this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
                            this.form.TORIHIKISAKI_NAME_RYAKU.Tag = string.Empty;
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
                                    // 現場チェック呼び出し
                                    ret = this.CheckGenba();
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
                            this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME1;
                            this.form.GYOUSHA_NAME_RYAKU.ReadOnly = false;
                            //this.form.GYOUSHA_NAME_RYAKU.TabStop = true;
                            this.form.GYOUSHA_NAME_RYAKU.TabStop = GetTabStop("GYOUSHA_NAME_RYAKU");    // No.3822
                            this.form.GYOUSHA_NAME_RYAKU.Tag = this.gyoushaHintText;
                            this.form.GYOUSHA_NAME_RYAKU.Focus();
                            this.form.isSetShokuchiForcus = true;
                        }
                    }
                }

                if (!isError)
                {
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if (!this.tmpGyousyaCd.Equals(inputGyoushaCd) && this.form.validateFlag)
                    {
                        bool flag = false;
                        foreach (Row row in this.form.mrwDetail.Rows)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_HINMEI_CD].Value)))
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!this.hasShow && this.form.mrwDetail.Rows.Count > 1 && flag)
                        {
                            // currentCellが単価再読み込みや、再計算の対象だった場合、
                            // ポップアップが上がった後にCurrentCellがEditModeになってしまう問題の対策。
                            if (this.form.mrwDetail.CurrentCell != null
                                && (this.form.mrwDetail.CurrentCell.Name.Equals(LogicClass.CELL_NAME_TANKA)
                                || this.form.mrwDetail.CurrentCell.Name.Equals(LogicClass.CELL_NAME_KINGAKU)))
                            {
                                rowindex = this.form.mrwDetail.CurrentRow.Index;
                                cellindex = this.form.mrwDetail.CurrentCell.CellIndex;
                                this.form.mrwDetail.CurrentCell = null;
                                isChageCurrentCell = true;
                            }

                            msgLogic = new MessageBoxShowLogic();
                            DialogResult dr = msgLogic.MessageBoxShow("C097", "業者");
                            bool catchErr = false;
                            if (dr == DialogResult.OK || dr == DialogResult.Yes)
                            {
                                foreach (Row r in this.form.mrwDetail.Rows.Where(r => !r.IsNewRow).ToList())
                                {
                                    this.GetHinmeiForPop(r, out catchErr);
                                    if (catchErr)
                                    {
                                        throw new Exception("");
                                    }
                                }
                            }
                        }
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end

                    if (!this.tmpGyousyaCd.Equals(inputGyoushaCd))
                    {
                        // 営業担当者の設定
                        this.setEigyou_Tantousha(this.form.GENBA_CD.Text, this.form.GYOUSHA_CD.Text, this.form.TORIHIKISAKI_CD.Text);
                        this.SetShuukeiKomoku(this.form.GENBA_CD.Text, this.form.GYOUSHA_CD.Text, this.form.TORIHIKISAKI_CD.Text); //PhuocLoc 2020/12/01 #136221
                    }

                    if (!this.tmpGyousyaCd.Equals(inputGyoushaCd))
                    {
                        // 明細行すべての単価を再設定
                        bool catchErr = false;
                        foreach (Row r in this.form.mrwDetail.Rows.Where(r => !r.IsNewRow).ToList())
                        {
                            catchErr = this.SearchAndCalcForUnit(false, r);
                            if (catchErr)
                            {
                                return false; // MAILAN #158993 START
                            }
                        }
                        this.ResetTankaCheck(); // MAILAN #158993 START

                        // 合計金額の再計算
                        if (!this.CalcTotalValues())
                        {
                            return false;
                        }
                    }

                    //ThangNguyen [Add] 20150818 #11065 Start
                    if (this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly == false && this.form.GYOUSHA_CD.Text != "" && this.form.GYOUSHA_NAME_RYAKU.Text != "")
                    {
                        //this.form.TORIHIKISAKI_NAME_RYAKU.BackColor = SystemColors.Window;
                        //this.form.GYOUSHA_CD.Focus();
                    }
                    //ThangNguyen [Add] 20150818 #11065 End
                }
            }
            else
            {
                ret = false;
            }

            if (isChageCurrentCell)
            {
                this.form.mrwDetail.CurrentCell = this.form.mrwDetail.Rows[rowindex].Cells[cellindex];
            }

            LogUtility.DebugMethodEnd();

            return ret;

        }

        private string genbaHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 現場チェック
        /// </summary>
        internal bool CheckGenba()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            bool isError = false;

            var msgLogic = new MessageBoxShowLogic();
            var inputGyoushaCd = this.form.GYOUSHA_CD.Text;
            var inputGenbaCd = this.form.GENBA_CD.Text;
            bool isContinue = false;
            bool catchErr = false;
            var gyoushaEntity = this.accessor.GetGyousha(this.form.GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);

            int rowindex = 0;
            int cellindex = 0;
            bool isChageCurrentCell = false;

            if (catchErr)
            {
                throw new Exception("");
            }
            // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
            if (this.form.isInputError || (String.IsNullOrEmpty(inputGenbaCd) || !this.tmpGenbaCd.Equals(inputGenbaCd)) || this.form.isFromSearchButton)
            {
                // 初期化
                //this.tmpGenbaCd = string.Empty;
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
                }
                else
                {
                    if (string.IsNullOrEmpty(inputGyoushaCd))
                    {
                        this.form.isInputError = true;
                        msgLogic.MessageBoxShow("E051", "業者");
                        this.form.GENBA_CD.Text = string.Empty;
                        this.form.GENBA_CD.Focus();
                        isError = true;
                        ret = false;
                        return ret;
                    }

                    //var genbaEntityList = this.accessor.GetGenba(inputGenbaCd);
                    var genbaEntityList = this.accessor.GetGenbaList(inputGyoushaCd, inputGenbaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                    if (genbaEntityList == null || genbaEntityList.Length < 1)
                    {
                        // エラーメッセージ
                        this.form.isInputError = true;
                        // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                        //msgLogic.MessageBoxShow("E011", "現場マスタ");
                        msgLogic.MessageBoxShow("E020", "現場");
                        // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                        this.form.GENBA_CD.Focus();
                        isError = true;
                        ret = false;
                    }
                    else
                    {
                        M_GENBA genba = new M_GENBA();
                        //foreach (M_GENBA genbaEntity in genbaEntityList)
                        //{
                        //    if (this.form.GYOUSHA_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
                        //    {
                        //        isContinue = true;
                        //        genba = genbaEntity;
                        //        this.form.GENBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
                        //        strGenbaName = genbaEntity.GENBA_NAME1 + genbaEntity.GENBA_NAME2;   // No.3279
                        //        break;
                        //    }
                        //}
                        //if (!isContinue)
                        //{
                        //    // 一致するものがないのでエラー
                        //    this.form.GENBA_CD.IsInputErrorOccured = true;
                        //    msgLogic.MessageBoxShow("E062", "業者");
                        //    this.form.GENBA_CD.Focus();
                        //    ret = false;
                        //}
                        //else if (null == gyoushaEntity)
                        //{
                        //    ret = false;
                        //}

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
                            //this.form.GYOUSHA_NAME_RYAKU.TabStop = true;
                            this.form.GYOUSHA_NAME_RYAKU.TabStop = GetTabStop("GYOUSHA_NAME_RYAKU");    // No.3822
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
                            //torihikisakiEntity = this.accessor.GetTorihikisaki(genba.TORIHIKISAKI_CD);

                            r_framework.Dao.IM_TORIHIKISAKIDao torihikisakiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKIDao>();
                            var keyEntity = new M_TORIHIKISAKI();
                            keyEntity.TORIHIKISAKI_CD = genba.TORIHIKISAKI_CD;
                            torihikisakiEntity = torihikisakiDao.GetAllValidData(keyEntity).FirstOrDefault();

                            if (torihikisakiEntity != null)
                            {
                                // 取引先設定
                                this.form.TORIHIKISAKI_CD.Text = torihikisakiEntity.TORIHIKISAKI_CD;
                                pressedEnterOrTab = false;
                                this.isCheckTankaFromChild = true; // MAILAN #158993 START
                                ret = this.CheckTorihikisaki();
                            }
                        }
                        else
                        {
                            this.form.TORIHIKISAKI_CD.Text = string.Empty;
                            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                            this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
                            this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
                            this.form.TORIHIKISAKI_NAME_RYAKU.Tag = string.Empty;
                        }

                        // TODO: 【2次】営業担当者チェックの呼び出し

                        // 現場：諸口区分チェック
                        this.form.isSetShokuchiForcus = false;
                        if (genba.SHOKUCHI_KBN.IsTrue)
                        {
                            // 現場名編集可
                            this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME1;
                            this.form.GENBA_NAME_RYAKU.ReadOnly = false;
                            //this.form.GENBA_NAME_RYAKU.TabStop = true;
                            this.form.GENBA_NAME_RYAKU.TabStop = GetTabStop("GENBA_NAME_RYAKU");    // No.3822
                            this.form.GENBA_NAME_RYAKU.Tag = genbaHintText;
                            this.form.GENBA_NAME_RYAKU.Focus();
                            this.form.isSetShokuchiForcus = true;
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
                            var manifestShuruiEntity = this.accessor.GetManifestShurui(genba.MANIFEST_SHURUI_CD);
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
                            var manifestTehaiEntity = this.accessor.GetManifestTehai(genba.MANIFEST_TEHAI_CD);
                            if (manifestTehaiEntity != null && !string.IsNullOrEmpty(manifestTehaiEntity.MANIFEST_TEHAI_NAME_RYAKU))
                            {
                                this.form.MANIFEST_TEHAI_CD.Text = Convert.ToString(genba.MANIFEST_TEHAI_CD);
                                this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = manifestTehaiEntity.MANIFEST_TEHAI_NAME_RYAKU;
                            }
                        }
                    }
                }

                if (!isError)
                {
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if (!this.tmpGenbaCd.Equals(inputGenbaCd) && this.form.validateFlag)
                    {
                        bool flag = false;
                        foreach (Row row in this.form.mrwDetail.Rows)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_HINMEI_CD].Value)))
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (this.form.mrwDetail.Rows.Count > 1 && flag)
                        {
                            // currentCellが単価再読み込みや、再計算の対象だった場合、
                            // ポップアップが上がった後にCurrentCellがEditModeになってしまう問題の対策。
                            if (this.form.mrwDetail.CurrentCell != null
                                && (this.form.mrwDetail.CurrentCell.Name.Equals(LogicClass.CELL_NAME_TANKA)
                                || this.form.mrwDetail.CurrentCell.Name.Equals(LogicClass.CELL_NAME_KINGAKU)))
                            {
                                rowindex = this.form.mrwDetail.CurrentRow.Index;
                                cellindex = this.form.mrwDetail.CurrentCell.CellIndex;
                                this.form.mrwDetail.CurrentCell = null;
                                isChageCurrentCell = true;
                            }

                            msgLogic = new MessageBoxShowLogic();
                            DialogResult dr = msgLogic.MessageBoxShow("C097", "現場");
                            if (dr == DialogResult.OK || dr == DialogResult.Yes)
                            {
                                foreach (Row r in this.form.mrwDetail.Rows.Where(r => !r.IsNewRow).ToList())
                                {
                                    this.GetHinmeiForPop(r, out catchErr);
                                    if (catchErr)
                                    {
                                        throw new Exception("");
                                    }
                                }
                            }
                        }
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                    if (!this.tmpGenbaCd.Equals(inputGenbaCd))
                    {
                        // 営業担当者の設定
                        this.setEigyou_Tantousha(this.form.GENBA_CD.Text, this.form.GYOUSHA_CD.Text, this.form.TORIHIKISAKI_CD.Text);
                        this.SetShuukeiKomoku(this.form.GENBA_CD.Text, this.form.GYOUSHA_CD.Text, this.form.TORIHIKISAKI_CD.Text); //PhuocLoc 2020/12/01 #136221
                    }

                    if (!this.tmpGenbaCd.Equals(inputGenbaCd))
                    {
                        // 明細行すべての単価を再設定
                        catchErr = false;
                        foreach (Row r in this.form.mrwDetail.Rows.Where(r => !r.IsNewRow).ToList())
                        {
                            catchErr = this.SearchAndCalcForUnit(false, r);
                            if (catchErr)
                            {
                                return false;
                            }
                        }
                        this.ResetTankaCheck(); // MAILAN #158993 START

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
                this.form.mrwDetail.CurrentCell = this.form.mrwDetail.Rows[rowindex].Cells[cellindex];
            }

            LogUtility.DebugMethodEnd();

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

                //参照モード、削除モードの場合は処理を行わない
                if (this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG ||
                    this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    return false;
                }

                // 初期化
                this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.EIGYOU_TANTOUSHA_CD.Text))
                {
                    // 営業担当者CDがなければ既にエラーが表示されているので何もしない。
                    return false;
                }

                var shainEntity = this.accessor.GetShain(this.form.EIGYOU_TANTOUSHA_CD.Text, true);
                if (shainEntity == null)
                {
                    return false;
                }
                else if (shainEntity.EIGYOU_TANTOU_KBN.Equals(SqlBoolean.False))
                {
                    // エラーメッセージ
                    this.form.EIGYOU_TANTOUSHA_CD.IsInputErrorOccured = true;
                    this.form.EIGYOU_TANTOUSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "営業担当者");
                    this.form.EIGYOU_TANTOUSHA_CD.Focus();
                }
                else
                {
                    this.form.EIGYOU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckEigyouTantousha", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckEigyouTantousha", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        internal string tmpTorihikisakiCd = string.Empty;
        internal string tmpGyousyaCd = string.Empty;
        internal string tmpNizumiGyoushaCd = string.Empty;
        internal string tmpNizumiGenbaCd = string.Empty;
        internal string tmpNioroshiGyoushaCd = string.Empty;
        internal string tmpNioroshiGenbaCd = string.Empty;
        private string tmpUnpanGyoushaCd = string.Empty;
        internal string tmpGenbaCd = string.Empty;  // No.3587
        internal string tmpKeitaiKbnCd = string.Empty;
        internal string tmpUntenshaCd = string.Empty;
        private string sharyouCd = string.Empty;
        private string shaShuCd = string.Empty;
        private string unpanGyousha = string.Empty;
        private Color sharyouCdBackColor = Color.FromArgb(255, 235, 160);
        private Color sharyouCdBackColorBlue = Color.FromArgb(0, 255, 255);
        private string sharyouHinttext = "全角10桁以内で入力してください";
        internal string tmpDenpyouDate = string.Empty;


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
        internal void GyousyaCdSet()
        {
            tmpGyousyaCd = this.form.GYOUSHA_CD.Text;
        }

        // No.3587-->
        /// <summary>
        /// 現場CD初期セット
        /// </summary>
        internal void GenbaCdSet()
        {
            tmpGenbaCd = this.form.GENBA_CD.Text;
        }
        // No.3587<--

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
        /// 車輌CD初期セット
        /// </summary>
        internal void ShayouCdSet()
        {
            sharyouCd = this.form.SHARYOU_CD.Text;
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
        /// 運搬業者CD初期セット
        /// </summary>
        internal void UnpanGyoushaCdSet()
        {
            tmpUnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 運転者チェック
        /// </summary>
        internal bool CheckUntensha()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //参照モード、削除モードの場合は処理を行わない
                if (this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG ||
                    this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    return false;
                }

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                // 又は、前回値が入力エラーだった場合
                if ((String.IsNullOrEmpty(this.form.UNTENSHA_CD.Text) || !this.tmpUntenshaCd.Equals(this.form.UNTENSHA_CD.Text)) || this.form.UNTENSHA_CD.IsInputErrorOccured)
                {
                    // 初期化
                    this.form.UNTENSHA_NAME.Text = string.Empty;


                    if (string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
                    {
                        // 運転者CDがなければ既にエラーが表示されているので何もしない。
                        return false;
                    }

                    var shainEntity = this.accessor.GetShain(this.form.UNTENSHA_CD.Text);
                    if (shainEntity == null)
                    {
                        // エラーメッセージ                        
                        this.form.UNTENSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "社員");
                        this.form.UNTENSHA_CD.Focus();
                        this.tmpUntenshaCd = string.Empty;
                        this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                        return false;
                    }
                    else if (shainEntity.UNTEN_KBN.Equals(SqlBoolean.False))
                    {
                        // エラーメッセージ                        
                        this.form.UNTENSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "運転者");
                        this.tmpUntenshaCd = string.Empty;
                        this.form.UNTENSHA_CD.Focus();
                        this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    }
                    else
                    {
                        this.form.UNTENSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckUntensha", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUntensha", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 車種Cd初期セット
        /// </summary>
        internal void ShashuCdSet()
        {
            shaShuCd = this.form.SHASHU_CD.Text;
        }

        /// <summary>
        /// 伝票日付初期セット
        /// </summary>
        internal void DenpyouDateSet()
        {
            tmpDenpyouDate = this.form.DENPYOU_DATE.Text;
        }

        /// <summary>
        /// 車輌チェック
        /// </summary>
        internal bool CheckSharyou()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //参照モード、削除モードの場合は処理を行わない
                if (this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG ||
                    this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    return false;
                }

                M_SHARYOU[] sharyouEntitys = null;

                // 何もしないとポップアップが起動されてしまう可能性があるため
                // 変更されたかチェックする
                if (sharyouCd.Equals(this.form.SHARYOU_CD.Text))
                {
                    // 複数ヒットするCDを入力→ポップアップで何もしない→一度ポップアップを閉じて再度ポップアップからデータを選択
                    // したときに色が戻らない問題の対策のため、存在チェックだけは実施する。
                    sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null, SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()));
                    if (sharyouEntitys != null && sharyouEntitys.Length == 1)
                    {
                        // 一意に識別できる場合は色を戻す
                        this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                        this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
                        this.form.oldSharyouShokuchiKbn = false;
                        this.form.SHARYOU_NAME_RYAKU.Tag = string.Empty;
                        this.form.SHARYOU_NAME_RYAKU.TabStop = false;
                        this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                        this.form.SHARYOU_CD.AutoChangeBackColorEnabled = true;
                    }
                    return false;
                }

                // 初期化
                this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                if (string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                    this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
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
                    return false;
                }

                sharyouCd = this.form.SHARYOU_CD.Text;
                unpanGyousha = this.form.UNPAN_GYOUSHA_CD.Text;

                sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null, SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()));

                bool catchErr = false;
                // マスタ存在チェック
                if (sharyouEntitys == null || sharyouEntitys.Length < 1)
                {
                    // 車輌名を編集可
                    this.ChangeShokuchiSharyouDesign(true);
                    // マスタに存在しない場合、ユーザに車輌名を自由入力させる
                    if (string.IsNullOrEmpty(this.form.SHARYOU_NAME_RYAKU.Text) || this.form.SHARYOU_CD.Text != this.dto.entryEntity.SHARYOU_CD)
                    {
                        this.form.SHARYOU_NAME_RYAKU.Text = ZeroSuppress(this.form.SHARYOU_CD, out catchErr);
                        if (catchErr)
                        {
                            throw new Exception("");
                        }
                    }

                    this.MoveToNextControlForShokuchikbnCheck(this.form.SHARYOU_CD);

                    if (!this.form.isSelectingSharyouCd)
                    {
                        this.form.isSelectingSharyouCd = true;
                        return false;
                    }

                    return false;
                }
                else
                {
                    this.form.oldSharyouShokuchiKbn = false;
                }

                // ポップアップから戻ってきたときに運搬業者名が無いため取得
                M_GYOUSHA unpanGyousya = this.accessor.GetGyousha(this.form.UNPAN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr)
                {
                    return true;
                }
                if (unpanGyousya != null)
                {
                    this.form.UNPAN_GYOUSHA_NAME.Text = unpanGyousya.GYOUSHA_NAME_RYAKU;
                }

                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_NAME.Text))
                {
                    M_SHARYOU sharyou = new M_SHARYOU();

                    // 運搬業者チェック
                    bool isCheck = false;
                    foreach (M_SHARYOU sharyouEntity in sharyouEntitys)
                    {
                        if (sharyouEntity.GYOUSHA_CD.Equals(this.form.UNPAN_GYOUSHA_CD.Text))
                        {
                            isCheck = true;
                            sharyou = sharyouEntity;
                            // 諸口区分チェック
                            if (unpanGyousya != null)
                            {
                                if (unpanGyousya.SHOKUCHI_KBN.IsTrue)
                                {
                                    // 運搬業者名編集可
                                    this.form.UNPAN_GYOUSHA_NAME.ReadOnly = false;
                                    //this.form.UNPAN_GYOUSHA_NAME.TabStop = true;
                                    this.form.UNPAN_GYOUSHA_NAME.TabStop = GetTabStop("UNPAN_GYOUSHA_NAME");    // No.3822
                                    this.form.UNPAN_GYOUSHA_NAME.Tag = this.unpanGyoushaHintText;
                                }
                            }
                            break;
                        }
                    }

                    if (isCheck)
                    {
                        // 車輌データセット
                        SetSharyou(sharyou);
                        return false;
                    }
                    else
                    {
                        // エラーメッセージ
                        this.form.SHARYOU_CD.AutoChangeBackColorEnabled = false;
                        this.form.SHARYOU_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E062", "運搬業者");
                        this.form.SHARYOU_CD.Focus();
                        return false;
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
                        //this.form.SHARYOU_NAME_RYAKU.TabStop = true;
                        this.form.SHARYOU_NAME_RYAKU.TabStop = GetTabStop("SHARYOU_NAME_RYAKU");    // No.3822
                        this.form.SHARYOU_NAME_RYAKU.Tag = this.sharyouHinttext;
                        // 自由入力可能であるため車輌名の色を変更
                        this.form.SHARYOU_CD.AutoChangeBackColorEnabled = false;
                        this.form.SHARYOU_CD.BackColor = sharyouCdBackColorBlue;

                        if (!this.form.isSelectingSharyouCd)
                        {
                            sharyouCd = string.Empty;
                            unpanGyousha = string.Empty;

                            this.form.isSelectingSharyouCd = true;
                            this.form.SHARYOU_CD.Focus();

                            this.form.FocusOutErrorFlag = true;

                            // この時は車輌CDを検索条件に含める
                            this.PopUpConditionsSharyouSwitch(true);

                            // 検索ポップアップ起動
                            var result = CustomControlExtLogic.PopUp(this.form.SHARYOU_CD);
                            this.PopUpConditionsSharyouSwitch(false);

                            // PopUpでF12押下された場合
                            //（戻り値でF12が押下されたか判断できない為、運搬業者の有無で判断）
                            if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                            {
                                // マスタに存在しない場合、ユーザに車輌名を自由入力させる
                                this.ChangeShokuchiSharyouDesign(true);
                                if (string.IsNullOrEmpty(this.form.SHARYOU_NAME_RYAKU.Text) || this.form.SHARYOU_CD.Text != this.dto.entryEntity.SHARYOU_CD)
                                {
                                    this.form.SHARYOU_NAME_RYAKU.Text = ZeroSuppress(this.form.SHARYOU_CD, out catchErr);
                                    if (catchErr)
                                    {
                                        throw new Exception("");
                                    }
                                }
                            }

                            this.form.FocusOutErrorFlag = false;
                            return false;
                        }
                        else
                        {
                            // ポップアアップから戻ってきて車輌名へ遷移した場合
                            // マスタに存在しない場合、ユーザに車輌名を自由入力させる
                            this.ChangeShokuchiSharyouDesign(true);
                            this.form.SHARYOU_NAME_RYAKU.Text = ZeroSuppress(this.form.SHARYOU_CD, out catchErr);
                            if (catchErr)
                            {
                                throw new Exception("");
                            }
                        }

                    }
                    else
                    {
                        // 一意レコード
                        // 車輌データセット
                        SetSharyou(sharyouEntitys[0]);
                    }
                }
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckSharyou", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("CheckSharyou", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                LogUtility.DebugMethodEnd();
                return true;
            }
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
        private void SetSharyou(M_SHARYOU sharyouEntity)
        {
            this.form.SHARYOU_NAME_RYAKU.Text = sharyouEntity.SHARYOU_NAME_RYAKU;
            this.form.UNTENSHA_CD.Text = sharyouEntity.SHAIN_CD;
            this.form.SHASHU_CD.Text = sharyouEntity.SHASYU_CD;
            this.form.UNPAN_GYOUSHA_CD.Text = sharyouEntity.GYOUSHA_CD;

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

            this.CheckUnpanGyoushaCd();
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

            foreach (DataRow r in dt.Rows)
            {
                if (r["DENSHU_KBN_CD"] != null
                    && (r["DENSHU_KBN_CD"].ToString().Equals(SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI.ToString())
                        || r["DENSHU_KBN_CD"].ToString().Equals(SalesPaymentConstans.DENSHU_KBN_CD_KYOTU.ToString()))
                    )
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
            try
            {
                LogUtility.DebugMethodStart();

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text) || !this.tmpKeitaiKbnCd.Equals(this.form.KEITAI_KBN_CD.Text)))
                {
                    // 初期化
                    this.form.KEITAI_KBN_NAME_RYAKU.Text = string.Empty;

                    if (string.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text))
                    {
                        return false;
                    }

                    short keitaiKbnCd;

                    if (!short.TryParse(this.form.KEITAI_KBN_CD.Text, out keitaiKbnCd))
                    {
                        return false;
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
                        return false;
                    }

                    var denshuKbnCd = (DENSHU_KBN)Enum.ToObject(typeof(DENSHU_KBN), (int)kakuteiKbn.DENSHU_KBN_CD);

                    switch (denshuKbnCd)
                    {
                        case DENSHU_KBN.URIAGE_SHIHARAI:
                        case DENSHU_KBN.KYOUTSUU:
                            this.form.KEITAI_KBN_NAME_RYAKU.Text = kakuteiKbn.KEITAI_KBN_NAME_RYAKU;
                            break;

                        default:
                            // エラーメッセージ
                            this.form.KEITAI_KBN_CD.IsInputErrorOccured = true;
                            this.form.KEITAI_KBN_CD.BackColor = Constans.ERROR_COLOR;
                            this.form.KEITAI_KBN_CD.ForeColor = Constans.ERROR_COLOR_FORE;
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "形態区分");
                            this.form.KEITAI_KBN_CD.Focus();
                            tmpKeitaiKbnCd = string.Empty;
                            break;
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckKeitaiKbn", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckKeitaiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// 確定区分チェック
        /// 入力CDから名称を表示する処理も実施
        /// </summary>
        internal bool CheckKakuteiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //参照モード、削除モードの場合は処理を行わない
                if (this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG ||
                    this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    return false;
                }

                if (string.IsNullOrEmpty(this.form.KAKUTEI_KBN.Text))
                {
                    this.form.KAKUTEI_KBN_NAME.Text = string.Empty;
                    return false;
                }

                short kakuteiKbn = 0;
                short.TryParse(this.form.KAKUTEI_KBN.Text, out kakuteiKbn);

                switch (kakuteiKbn)
                {
                    case SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI:
                    case SalesPaymentConstans.KAKUTEI_KBN_MIKAKUTEI:
                        this.form.KAKUTEI_KBN_NAME.Text = SalesPaymentConstans.GetKakuteiKbnName(kakuteiKbn);
                        break;

                    default:
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E058");
                        this.form.KAKUTEI_KBN.Focus();
                        break;

                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckKakuteiKbn", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckKakuteiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 明細に新規行を追加
        /// </summary>
        internal bool AddNewRow()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if ((Row)this.form.mrwDetail.CurrentRow != null)
                {
                    this.form.mrwDetail.EndEdit();

                    int iSaveRowIndex = this.form.mrwDetail.CurrentRow.Index;
                    this.form.mrwDetail.Rows.Insert(this.form.mrwDetail.CurrentRow.Index);
                    this.form.mrwDetail.ClearSelection();

                    this.form.mrwDetail.AddSelection(iSaveRowIndex);

                    this.form.mrwDetail.NotifyCurrentCellDirty(false);
                    // 行番号採番
                    bool catchErr = this.NumberingRowNo();
                    if (catchErr)
                    {
                        return true;
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("AddNewRow", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 明細のカレント行を削除
        /// </summary>
        internal bool RemoveSelectedRow()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if ((Row)this.form.mrwDetail.CurrentRow != null)
                {
                    this.form.mrwDetail.EndEdit();
                    Row selectedRows = (Row)this.form.mrwDetail.CurrentRow;
                    if (!selectedRows.IsNewRow)
                    {
                        // 行削除
                        int iSaveIndex = this.form.mrwDetail.CurrentRow.Index;
                        this.form.mrwDetail.Rows.Remove(selectedRows);
                        this.form.mrwDetail.ClearSelection();
                        this.form.mrwDetail.AddSelection(iSaveIndex);
                    }

                    this.form.mrwDetail.NotifyCurrentCellDirty(false);
                    // 行番号採番
                    bool catchErr = this.NumberingRowNo();
                    if (catchErr)
                    {
                        return true;
                    }
                    this.form.mrwDetail.ResumeLayout();

                    // 計算
                    this.CalcDetail();
                }
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("RemoveSelectedRow", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 伝票日付チェック
        /// </summary>
        internal bool CheckDenpyouDate()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var inputDenpyouDate = this.form.DENPYOU_DATE.Text;

                // 伝票日付が空じゃないかつ変更があった場合
                if (!string.IsNullOrEmpty(inputDenpyouDate) && !this.tmpDenpyouDate.Equals(inputDenpyouDate))
                {
                    // 明細行すべての単価を再設定
                    bool catchErr = false;
                    foreach (Row r in this.form.mrwDetail.Rows.Where(r => !r.IsNewRow).ToList())
                    {
                        catchErr = this.SearchAndCalcForUnit(false, r);
                        if (catchErr)
                        {
                            return true;
                        }
                    }
                    this.ResetTankaCheck(); // MAILAN #158993 START

                    // 合計金額の再計算
                    if (!this.CalcTotalValues())
                    {
                        return false;
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDenpyouDate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 帳票(領収書)出力
        /// </summary>
        internal bool Print()
        {
            try
            {
                LogUtility.DebugMethodStart();

                DataTable reportData = CreateReportData();

                // G335\Templateにxmlを配置している
                // 現在表示されている一覧をレポート情報として生成
                ReportInfoR339 reportInfo = new ReportInfoR339(this.form.WindowId, reportData);
                reportInfo.CreateReport();
                reportInfo.Title = "領収書";

                // 印刷ポップアップ表示
                FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo, "R339");
                //reportPopup.ShowDialog(); // No.1143
                // 印刷設定の取得
                //reportPopup.SetPrintSetting(SalesPaymentConstans.RYOUSYUUSHO);
                // 印刷アプリ初期動作(直印刷)
                reportPopup.PrintInitAction = 1;
                // 印刷実行
                reportPopup.PrintXPS(true,true);        // No.1143
                reportPopup.Dispose();
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("Print", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                LogUtility.DebugMethodEnd();
                return true;
            }
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
            reportTable.Columns.Add("KYOTEN_ADDRESS2");                 // No.3710
            reportTable.Columns["KYOTEN_ADDRESS2"].ReadOnly = false;    // No.3710
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
                bool catchErr = false;
                TorihikisakiEntity = accessor.GetTorihikisaki(this.form.TORIHIKISAKI_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr, false);
                if (catchErr)
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
                    if (this.dto.entryEntity.TORIHIKISAKI_NAME.Length > 20)
                    {
                        //20150921 hoanghm #12520 start
                        //row["GYOUSHA_NAME1"] = this.dto.entryEntity.TORIHIKISAKI_NAME.Substring(0, 20);
                        //row["GYOUSHA_NAME2"] = this.dto.entryEntity.TORIHIKISAKI_NAME.Substring(20);                         
                        string gyoushaName1 = string.Empty;
                        string gyoushaName2 = string.Empty;
                        ReportUtility.SubString(this.dto.entryEntity.TORIHIKISAKI_NAME, 40, ref gyoushaName1, ref gyoushaName2);
                        row["GYOUSHA_NAME1"] = gyoushaName1;
                        row["GYOUSHA_NAME2"] = gyoushaName2;
                        //20150921 hoanghm #12520 end
                    }
                    else
                    {
                        row["GYOUSHA_NAME1"] = this.dto.entryEntity.TORIHIKISAKI_NAME;
                    }
                }
                else
                {
                    row["GYOUSHA_NAME1"] = TorihikisakiEntity.TORIHIKISAKI_NAME1;
                    row["GYOUSHA_NAME2"] = TorihikisakiEntity.TORIHIKISAKI_NAME2;
                }
            }
            row["KEISHOU1"] = this.form.denpyouHakouPopUpDTO.Keisyou_1;
            row["KEISHOU2"] = this.form.denpyouHakouPopUpDTO.Keisyou_2;
            row["DENPYOU_DATE"] = this.form.DENPYOU_DATE.Text;
            if (this.dto.sysInfoEntity.SYS_RECEIPT_RENBAN_HOUHOU_KBN == 1)
            {
                // 日連番
                row["RECEIPT_NUMBER"] = this.dto.entryEntity.RECEIPT_NUMBER;
            }
            else
            {
                // 年連番
                row["RECEIPT_NUMBER"] = this.dto.entryEntity.RECEIPT_NUMBER_YEAR;
            }
            row["KINGAKU_TOTAL"] = this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Rorihiki;

            row["TADASHIGAKI"] = this.form.denpyouHakouPopUpDTO.Tadasi_Kaki;
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
                var shouhizeiRate = this.accessor.GetShouhizeiRate(((DateTime)this.dto.entryEntity.URIAGE_DATE).Date);
                zeiritu = (decimal)(shouhizeiRate.SHOUHIZEI_RATE);
            }
            row["SYOUHIZEI_RITU"] = string.Format("{0:0%}", zeiritu);
            row["SYOUHIZEI"] = this.form.denpyouHakouPopUpDTO.R_KAZEI_SHOUHIZEI;

            if (this.form.denpyouHakouPopUpDTO.R_KAZEI_KINGAKU == "0")
            {
                row["ZEINUKI_KINGAKU"] = "";
                row["SYOUHIZEI"] = "";
            }

            row["DENPYOU_NUMBER"] = "売上/支払番号No." + this.dto.entryEntity.UR_SH_NUMBER;

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

        enum DENPYO_SHIKIRISHO_KIND
        {
            SEIKYUU = 1,
            SHIHARAI,
            SOUSAI
        }

        const string DEF_HAKKOU_KBN_KOBETSU = "1";
        const string DEF_HAKKOU_KBN_SOUSAI = "2";
        const string DEF_HAKKOU_KBN_ALL = "3";

        string strTorihikisakiName = "";
        string strTorihikisakiName2 = "";
        string strTorihikisakiKeishou = "";
        string strTorihikisakiKeishou2 = "";
        string strNyuryokuTantousyaName = "";
        string strGenbaName = "";

        M_KYOTEN entKyotenInfo;

        /// <summary>
        /// 仕切書印刷処理
        /// </summary>
        /// <returns></returns>
        internal bool PrintShikirisyo()
        {
            try
            {
                LogUtility.DebugMethodStart();
                DataTable reportTable = new DataTable();

                DataRow rowHeader;
                rowHeader = reportTable.NewRow();

                //明細のデータに伝票区分が売上/支払のデータが存在するか確認する。
                bool bExistDenpyoKbnUriage = false;
                bool bExistDenpyoKbnShiharai = false;

                foreach (Row dr in this.form.mrwDetail.Rows)
                {
                    if (!dr.IsNewRow)
                    {
                        string strDenpyouKbn = dr[CELL_NAME_DENPYOU_KBN_CD].Value.ToString();
                        if (strDenpyouKbn == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE_STR)
                        {
                            bExistDenpyoKbnUriage = true;
                        }
                        else if (strDenpyouKbn == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI_STR)
                        {
                            bExistDenpyoKbnShiharai = true;
                        }

                        if (bExistDenpyoKbnUriage && bExistDenpyoKbnShiharai)
                        {
                            break;
                        }
                    }
                }

                if (bExistDenpyoKbnShiharai || bExistDenpyoKbnUriage)
                {
                    bool catchErr = false;
                    //DBアクセスを無駄に行わないように先に固定の情報は取得する。
                    M_TORIHIKISAKI entTorihikisaki = accessor.GetTorihikisaki(this.form.TORIHIKISAKI_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr, false);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                    if (entTorihikisaki != null)
                    {
                        // 諸口区分チェック
                        if (entTorihikisaki.SHOKUCHI_KBN.IsTrue)
                        {
                            //ThangNguyen [Update] 2015086 #12394 Start
                            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
                            byte[] byteArray = encoding.GetBytes(this.form.TORIHIKISAKI_NAME_RYAKU.Text);
                            if (byteArray.Length > 40)
                            {
                                strTorihikisakiName = encoding.GetString(byteArray, 0, 40);
                                strTorihikisakiName2 = this.form.TORIHIKISAKI_NAME_RYAKU.Text.Replace(strTorihikisakiName, "");
                            }
                            else
                            {
                                strTorihikisakiName = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
                            }
                            //ThangNguyen [Update] 2015086 #12394 End
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(entTorihikisaki.TORIHIKISAKI_NAME1))
                            {
                                strTorihikisakiName = entTorihikisaki.TORIHIKISAKI_NAME1;
                                strTorihikisakiName2 = entTorihikisaki.TORIHIKISAKI_NAME2;
                            }
                            else
                            {
                                strTorihikisakiName = "";
                            }
                        }
                        if (!string.IsNullOrEmpty(entTorihikisaki.TORIHIKISAKI_KEISHOU1))
                        {
                            strTorihikisakiKeishou = entTorihikisaki.TORIHIKISAKI_KEISHOU1;
                        }
                        else
                        {
                            strTorihikisakiKeishou = "";
                        }
                        if (!string.IsNullOrEmpty(entTorihikisaki.TORIHIKISAKI_KEISHOU2))
                        {
                            strTorihikisakiKeishou2 = entTorihikisaki.TORIHIKISAKI_KEISHOU2;
                        }
                        else
                        {
                            strTorihikisakiKeishou2 = "";
                        }
                    }

                    M_SHAIN entShain = accessor.GetShain(this.form.NYUURYOKU_TANTOUSHA_CD.Text);
                    if (entShain != null)
                    {
                        if (!string.IsNullOrEmpty(entShain.SHAIN_NAME))
                        {
                            strNyuryokuTantousyaName = entShain.SHAIN_NAME;
                        }
                        else
                        {
                            strNyuryokuTantousyaName = "";
                        }

                    }

                    catchErr = false;
                    M_GENBA entGenba = accessor.GetGenba(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr, false);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                    if (entGenba != null)
                    {
                        // 諸口区分チェック
                        if (entGenba.SHOKUCHI_KBN.IsTrue)
                        {
                            strGenbaName = this.form.GENBA_NAME_RYAKU.Text;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(entGenba.GENBA_NAME1))
                            {
                                strGenbaName = entGenba.GENBA_NAME1;
                            }
                            else
                            {
                                strGenbaName = "";
                            }
                        }
                    }

                    short kyotenCd;
                    if (short.TryParse(this.headerForm.KYOTEN_CD.Text, out kyotenCd))
                    {
                        M_KYOTEN[] kyotens = accessor.GetAllDataByCodeForKyoten(kyotenCd);
                        if (kyotens != null && kyotens.Count() > 0)
                        {
                            // 拠点CDで絞り込んだら一件しか取れないはず
                            entKyotenInfo = kyotens[0];
                        }
                    }

                    string strSeikyuHakkouKbn = this.form.denpyouHakouPopUpDTO.Seikyu_Hakou_Kbn;
                    string strShiharaiHakkouKbn = this.form.denpyouHakouPopUpDTO.Shiharai_Hakou_Kbn;
                    string strHakkouKbn = this.form.denpyouHakouPopUpDTO.Hakou_Kbn;
                    //請求の伝票発行区分を確認する。
                    if (bExistDenpyoKbnUriage && strSeikyuHakkouKbn == "1")
                    {
                        if (strHakkouKbn != DEF_HAKKOU_KBN_SOUSAI)
                        {
                            PrintShikirisyoByType(DENPYO_SHIKIRISHO_KIND.SEIKYUU);
                        }
                    }

                    //支払の伝票発行区分を確認する。
                    if (bExistDenpyoKbnShiharai && strShiharaiHakkouKbn == "1")
                    {
                        if (strHakkouKbn != DEF_HAKKOU_KBN_SOUSAI)
                        {
                            PrintShikirisyoByType(DENPYO_SHIKIRISHO_KIND.SHIHARAI);
                        }
                    }

                    if (strSeikyuHakkouKbn == "1" || strShiharaiHakkouKbn == "1")
                    {
                        if (strHakkouKbn != DEF_HAKKOU_KBN_KOBETSU)
                        {
                            PrintShikirisyoByType(DENPYO_SHIKIRISHO_KIND.SOUSAI);
                        }
                    }
                }
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("PrintShikirisyo", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("PrintShikirisyo", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 指定した種類の仕切書を印刷する。
        /// </summary>
        /// <returns></returns>
        private void PrintShikirisyoByType(DENPYO_SHIKIRISHO_KIND type)
        {

            DataTable dtHeader = CreateHeaderTable(type);
            DataTable dtDetail = CreateDetailTable(type);
            DataTable dtFooter = CreateFooterTable(type);

            switch (type)
            {
                case DENPYO_SHIKIRISHO_KIND.SEIKYUU:
                case DENPYO_SHIKIRISHO_KIND.SHIHARAI:
                    ReportInfoR765 reportInfo_invoice = new ReportInfoR765(this.form.WindowId);
                    reportInfo_invoice.DataTableList.Add("Header", dtHeader);
                    reportInfo_invoice.DataTableList.Add("Detail", dtDetail);
                    reportInfo_invoice.DataTableList.Add("Footer", dtFooter);
                    reportInfo_invoice.Create(@".\Template\R765-Form.xml", "LAYOUT1", new DataTable());
                    reportInfo_invoice.Title = "仕切書";
                    FormReportPrintPopup popup_invoice = new FormReportPrintPopup(reportInfo_invoice, "R765", WINDOW_ID.T_URIAGE_SHIHARAI);
                    // 印刷アプリ初期動作(直印刷)
                    popup_invoice.PrintInitAction = 1;
                    // 印刷実行
                    popup_invoice.PrintXPS(true, true);
                    break;
                case DENPYO_SHIKIRISHO_KIND.SOUSAI:
                    ReportInfoR338 reportInfo = new ReportInfoR338(this.form.WindowId);
                    reportInfo.DataTableList.Add("Header", dtHeader);
                    reportInfo.DataTableList.Add("Detail", dtDetail);
                    reportInfo.DataTableList.Add("Footer", dtFooter);
                    reportInfo.Create(@".\Template\R338-Form.xml", "LAYOUT1", new DataTable());
                    reportInfo.Title = "仕切書";
                    FormReportPrintPopup popup = new FormReportPrintPopup(reportInfo, "R338", WINDOW_ID.T_URIAGE_SHIHARAI);
                    // 印刷アプリ初期動作(直印刷)
                    popup.PrintInitAction = 1;
                    // 印刷実行
                    popup.PrintXPS(true, true);
                    break;
            }
        }

        /// <summary>
        /// 仕切書ヘッダー部データ受渡用文字列作成
        /// </summary>
        /// <returns>ヘッダー部データ受渡用文字列</returns>
        private DataTable CreateHeaderTable(DENPYO_SHIKIRISHO_KIND Type)
        {
            DataTable dtHeader = new DataTable();
            DataRow rowTmp;
            dtHeader.TableName = "Header";

            // タイトル名
            dtHeader.Columns.Add("TITLE");
            // 担当名
            dtHeader.Columns.Add("TANTOU");
            // お取引先CD
            dtHeader.Columns.Add("TORIHIKISAKICD");
            // お取引先名
            dtHeader.Columns.Add("TORIHIKISAKIMEI");
            // お取引先名2
            dtHeader.Columns.Add("TORIHIKISAKIMEI2");
            // お取引先名敬称
            dtHeader.Columns.Add("TORIHIKISAKIKEISHOU");
            // お取引先名敬称2
            dtHeader.Columns.Add("TORIHIKISAKIKEISHOU2");
            // 伝票No
            dtHeader.Columns.Add("DENPYOUNO");
            // 乗員
            dtHeader.Columns.Add("JYOUIN");
            // 車番
            dtHeader.Columns.Add("SHABAN");
            // 車輌CD
            dtHeader.Columns.Add("SHARYOUCD");  // No.3837
            // 伝票日付
            dtHeader.Columns.Add("DENPYOUDATE");

            dtHeader.Columns.Add("BARCODE");
            dtHeader.Columns.Add("QRCODE");
            dtHeader.Columns.Add("DENPYOU_SHURUI");

            rowTmp = dtHeader.NewRow();


            System.Text.StringBuilder sBuilder;

            sBuilder = new StringBuilder();

            //タイトル名
            string iCalcBaseKbn = this.dto.sysInfoEntity.UR_SH_CALC_BASE_KBN.ToString();
            if (Type == DENPYO_SHIKIRISHO_KIND.SEIKYUU)
            {
                sBuilder.Append(this.dto.sysInfoEntity.UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE1);
                sBuilder.Append(",");
                sBuilder.Append(this.dto.sysInfoEntity.UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE2);
                sBuilder.Append(",");
                sBuilder.Append(this.dto.sysInfoEntity.UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE3);
            }
            else if (Type == DENPYO_SHIKIRISHO_KIND.SHIHARAI)
            {
                sBuilder.Append(this.dto.sysInfoEntity.UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE1);
                sBuilder.Append(",");
                sBuilder.Append(this.dto.sysInfoEntity.UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE2);
                sBuilder.Append(",");
                sBuilder.Append(this.dto.sysInfoEntity.UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE3);
            }
            else
            {
                if (iCalcBaseKbn == "1")
                {
                    sBuilder.Append(this.dto.sysInfoEntity.UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE1);
                    sBuilder.Append(",");
                    sBuilder.Append(this.dto.sysInfoEntity.UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE2);
                    sBuilder.Append(",");
                    sBuilder.Append(this.dto.sysInfoEntity.UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE3);
                }
                else
                {
                    sBuilder.Append(this.dto.sysInfoEntity.UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE1);
                    sBuilder.Append(",");
                    sBuilder.Append(this.dto.sysInfoEntity.UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE2);
                    sBuilder.Append(",");
                    sBuilder.Append(this.dto.sysInfoEntity.UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE3);
                }
            }

            rowTmp["TITLE"] = sBuilder.ToString();

            // 担当名
            rowTmp["TANTOU"] = strNyuryokuTantousyaName;

            //お取引先CD
            rowTmp["TORIHIKISAKICD"] = this.form.TORIHIKISAKI_CD.Text;

            //お取引先名
            rowTmp["TORIHIKISAKIMEI"] = strTorihikisakiName;

            //お取引先名2
            rowTmp["TORIHIKISAKIMEI2"] = strTorihikisakiName2;

            //お取引先名敬称
            rowTmp["TORIHIKISAKIKEISHOU"] = strTorihikisakiKeishou;

            //お取引先名敬称2
            rowTmp["TORIHIKISAKIKEISHOU2"] = strTorihikisakiKeishou2;

            //伝票No
            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                // 新規
                rowTmp["DENPYOUNO"] = this.dto.entryEntity.UR_SH_NUMBER;
            else
                // 新規以外
                rowTmp["DENPYOUNO"] = this.form.ENTRY_NUMBER.Text;

            //乗員
            rowTmp["JYOUIN"] = this.form.NINZUU_CNT.Text;

            //車番
            rowTmp["SHABAN"] = this.form.SHARYOU_NAME_RYAKU.Text;

            //車輌CD
            rowTmp["SHARYOUCD"] = this.form.SHARYOU_CD.Text;    // No.3837

            //伝票日付
            DateTime Date = (DateTime)this.form.DENPYOU_DATE.Value;
            rowTmp["DENPYOUDATE"] = Date.ToString("yyyy/MM/dd");

            rowTmp["BARCODE"] = "*3" + rowTmp["DENPYOUNO"].ToString().PadLeft(7, '0') + "*";
            rowTmp["QRCODE"] = "3" + rowTmp["DENPYOUNO"].ToString().PadLeft(7, '0');
            rowTmp["DENPYOU_SHURUI"] = "3";

            dtHeader.Rows.Add(rowTmp);

            return dtHeader;
        }

        /// <summary>
        /// 仕切書明細部データ受渡用文字列作成
        /// </summary>
        /// <returns>仕切書明細部データ受渡用文字列</returns>
        private DataTable CreateDetailTable(DENPYO_SHIKIRISHO_KIND Type)
        {

            // 売上税計算区分CD
            int seikyuZeikeisanKbn = 0;
            int.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Zeikeisan_Kbn, out seikyuZeikeisanKbn);
            // 売上税区分CD
            int uriageZeiKbnCd = 0;
            int.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn, out uriageZeiKbnCd);
            // 支払税計算区分CD
            int shiharaiZeiKeisanKbnCd = 0;
            int.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Zeikeisan_Kbn, out shiharaiZeiKeisanKbnCd);
            // 支払税区分CD
            int shiharaiZeiKbnCd = 0;
            int.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn, out shiharaiZeiKbnCd);
            SHIKIRISHO_UR_UTIZEI = 0;
            SHIKIRISHO_SH_UTIZEI = 0;

            DataTable dtDetail = new DataTable();
            DataRow rowTmp;
            dtDetail.TableName = "Detail";
            // No
            dtDetail.Columns.Add("NUMBER");
            // 総重量
            dtDetail.Columns.Add("SOU_JYUURYOU");
            // 空車重量
            dtDetail.Columns.Add("KUUSHA_JYUURYOU");
            // 調整
            dtDetail.Columns.Add("CHOUSEI");
            // 容器引
            dtDetail.Columns.Add("YOUKIBIKI");
            // 正味
            dtDetail.Columns.Add("SHOUMI");
            // 数量
            dtDetail.Columns.Add("SUURYOU");
            // 数量単位名
            dtDetail.Columns.Add("FHN_SUURYOU_TANI");
            // 品名CD
            dtDetail.Columns.Add("FHN_HINMEICD");
            // 品名
            dtDetail.Columns.Add("HINMEI");
            // 単価
            dtDetail.Columns.Add("TANKA");
            // 金額
            dtDetail.Columns.Add("KINGAKU");

            foreach (Row dr in this.form.mrwDetail.Rows)
            {
                if (dr.IsNewRow || string.IsNullOrEmpty((string)dr.Cells["ROW_NO"].Value.ToString()))
                {
                    continue;
                }

                rowTmp = dtDetail.NewRow();

                //No
                rowTmp["NUMBER"] = dr.Cells[CELL_NAME_ROW_NO].Value.ToString();

                //総重量
                rowTmp["SOU_JYUURYOU"] = "";

                //空車重量
                rowTmp["KUUSHA_JYUURYOU"] = "";

                //調整
                rowTmp["CHOUSEI"] = "";

                //容器引
                rowTmp["YOUKIBIKI"] = "";

                //正味
                rowTmp["SHOUMI"] = "";

                //数量
                rowTmp["SUURYOU"] = dr.Cells[CELL_NAME_SUURYOU].DisplayText;

                //数量単位名
                rowTmp["FHN_SUURYOU_TANI"] = dr.Cells[CELL_NAME_UNIT_NAME_RYAKU].Value.ToString();

                //品名CD
                rowTmp["FHN_HINMEICD"] = dr.Cells[CELL_NAME_HINMEI_CD].Value.ToString();

                //品名
                // 20151021 katen #13337 品名手入力に関する機能修正 start
                rowTmp["HINMEI"] = dr.Cells[CELL_NAME_HINMEI_NAME].Value.ToString();
                //if (!string.IsNullOrEmpty(dr.Cells[CELL_NAME_HINMEI_CD].Value.ToString()))
                //{
                //    M_HINMEI entHinmei = this.accessor.GetHinmeiDataByCd(dr.Cells[CELL_NAME_HINMEI_CD].Value.ToString());
                //    string strHinmeiName = entHinmei.HINMEI_NAME;
                //    rowTmp["HINMEI"] = strHinmeiName;
                //}
                //else
                //{
                //    rowTmp["HINMEI"] = "";
                //}
                // 20151021 katen #13337 品名手入力に関する機能修正 end

                //単価と金額
                string strUrShCalcBaseKbn = this.dto.sysInfoEntity.UR_SH_CALC_BASE_KBN.ToString();  // 差引基準を取得
                int iDenpyoKbnCd = int.Parse(dr.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString());  // 伝票区分を取得
                if (Type == DENPYO_SHIKIRISHO_KIND.SEIKYUU)
                {
                    // 請求仕切書の場合
                    if (iDenpyoKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
                    {
                        // 伝票区分=1(売上)
                        rowTmp["TANKA"] = dr.Cells[CELL_NAME_TANKA].DisplayText;
                        rowTmp["KINGAKU"] = dr.Cells[CELL_NAME_KINGAKU].DisplayText;
                        int temp;
                        if (int.TryParse(Convert.ToString(dr.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value), out temp))
                        {
                            //品名内税
                            if (dr.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Equals(CommonConst.ZEI_KBN_UCHI.ToString()))
                            {
                                SHIKIRISHO_UR_UTIZEI = 1;
                            }
                        }
                        else
                        {
                            //品名税なし、明細毎内税
                            if (seikyuZeikeisanKbn.Equals(CommonConst.ZEI_KEISAN_KBN_MEISAI) && uriageZeiKbnCd.Equals(CommonConst.ZEI_KBN_UCHI))
                            {
                                SHIKIRISHO_UR_UTIZEI = 1;
                            }
                        }
                    }
                    else if (iDenpyoKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI)
                    {
                        // 伝票区分=2(支払)
                        rowTmp["TANKA"] = "";
                        rowTmp["KINGAKU"] = "";
                    }
                }
                else if (Type == DENPYO_SHIKIRISHO_KIND.SHIHARAI)
                {
                    // 支払仕切書の場合
                    if (iDenpyoKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
                    {
                        // 伝票区分=1(売上)
                        rowTmp["TANKA"] = "";
                        rowTmp["KINGAKU"] = "";
                    }
                    else if (iDenpyoKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI)
                    {
                        // 伝票区分=2(支払)
                        rowTmp["TANKA"] = dr.Cells[CELL_NAME_TANKA].DisplayText;
                        rowTmp["KINGAKU"] = dr.Cells[CELL_NAME_KINGAKU].DisplayText;
                        int temp;
                        if (int.TryParse(Convert.ToString(dr.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value), out temp))
                        {
                            //品名内税
                            if (dr.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Equals(CommonConst.ZEI_KBN_UCHI.ToString()))
                            {
                                SHIKIRISHO_SH_UTIZEI = 1;
                            }
                        }
                        else
                        {
                            //品名税なし、明細毎内税
                            if (shiharaiZeiKeisanKbnCd.Equals(CommonConst.ZEI_KEISAN_KBN_MEISAI) && shiharaiZeiKbnCd.Equals(CommonConst.ZEI_KBN_UCHI))
                            {
                                SHIKIRISHO_SH_UTIZEI = 1;
                            }
                        }
                    }
                }
                else if (Type == DENPYO_SHIKIRISHO_KIND.SOUSAI)
                {
                    //相殺仕切書の場合
                    if (strUrShCalcBaseKbn == "1")
                    {
                        // 差引基準=1(売上)

                        if (iDenpyoKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
                        {
                            // 伝票区分=1(売上)
                            rowTmp["TANKA"] = dr.Cells[CELL_NAME_TANKA].DisplayText;
                            rowTmp["KINGAKU"] = dr.Cells[CELL_NAME_KINGAKU].DisplayText;
                        }
                        else if (iDenpyoKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI)
                        {
                            // 伝票区分=2(支払)
                            rowTmp["TANKA"] = dr.Cells[CELL_NAME_TANKA].DisplayText;
                            decimal decKingaku = 0;
                            decimal.TryParse(dr.Cells[CELL_NAME_KINGAKU].Value.ToString(), out decKingaku);
                            rowTmp["KINGAKU"] = CommonCalc.DecimalFormat(decKingaku * -1);
                        }
                    }
                    else if (strUrShCalcBaseKbn == "2")
                    {
                        // 差引基準=2(支払)

                        if (iDenpyoKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
                        {
                            // 伝票区分=1(売上)
                            rowTmp["TANKA"] = dr.Cells[CELL_NAME_TANKA].DisplayText;
                            decimal decKingaku = 0;
                            decimal.TryParse(dr.Cells[CELL_NAME_KINGAKU].Value.ToString(), out decKingaku);
                            rowTmp["KINGAKU"] = CommonCalc.DecimalFormat(decKingaku * -1);
                        }
                        else if (iDenpyoKbnCd == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI)
                        {
                            // 伝票区分=2(支払)
                            rowTmp["TANKA"] = dr.Cells[CELL_NAME_TANKA].DisplayText;
                            rowTmp["KINGAKU"] = dr.Cells[CELL_NAME_KINGAKU].DisplayText;
                        }
                    }
                }

                dtDetail.Rows.Add(rowTmp);

            }

            //データがない場合でも5行分データがないと出力されないため、空行を作成する。
            int iNumEmpLine = dtDetail.Rows.Count % 5;
            if (iNumEmpLine > 0)
            {
                for (int ii = 0; ii < 5 - iNumEmpLine; ii++)
                {
                    rowTmp = dtDetail.NewRow();
                    rowTmp["NUMBER"] = "";
                    rowTmp["SOU_JYUURYOU"] = "";
                    rowTmp["KUUSHA_JYUURYOU"] = "";
                    rowTmp["CHOUSEI"] = "";
                    rowTmp["YOUKIBIKI"] = "";
                    rowTmp["SHOUMI"] = "";
                    rowTmp["SUURYOU"] = "";
                    rowTmp["FHN_SUURYOU_TANI"] = "";
                    rowTmp["FHN_HINMEICD"] = "";
                    rowTmp["HINMEI"] = "";
                    rowTmp["TANKA"] = "";
                    rowTmp["KINGAKU"] = "";

                    dtDetail.Rows.Add(rowTmp);
                }
            }

            return dtDetail;
        }

        /// <summary>
        /// 仕切書フッター部データ受渡用文字列作成
        /// </summary>
        /// <returns>仕切書フッター部データ受渡用文字列</returns>
        private DataTable CreateFooterTable(DENPYO_SHIKIRISHO_KIND Type)
        {
            DataTable dtFooter = new DataTable();
            DataRow rowTmp;
            dtFooter.TableName = "Footer";

            // 現場名
            dtFooter.Columns.Add("GENBA");
            // 正味合計
            dtFooter.Columns.Add("SHOUMI_GOUKEI");
            // 合計金額
            dtFooter.Columns.Add("GOUKEI_KINGAKU");
            // 備考
            dtFooter.Columns.Add("BIKOU");

            // 上段の請求・支払いラベル
            dtFooter.Columns.Add("SEIKYUU_SHIHARAI1");
            // 上段の請求・前回残高
            dtFooter.Columns.Add("UP_ZENKAI_ZANDAKA");
            // 上段の請求・伝票額（税抜）
            dtFooter.Columns.Add("UP_DENPYOUGAKU");
            // 上段の請求・消費税
            dtFooter.Columns.Add("UP_SHOUHIZEI");
            // 上段の請求・合計（税込）
            dtFooter.Columns.Add("UP_GOUKEI_ZEIKOMI");
            // 上段の請求・御精算額
            dtFooter.Columns.Add("UP_SEISANGAKU");
            // 上段の請求・差引残高
            dtFooter.Columns.Add("UP_SASHIHIKIZANDAKA");
            // 下段の請求・支払いラベル
            dtFooter.Columns.Add("SEIKYUU_SHIHARAI2");
            // 下段の請求・前回残高
            dtFooter.Columns.Add("DOWN_ZENKAI_ZANDAKA");
            // 下段の請求・伝票額（税抜）
            dtFooter.Columns.Add("DOWN_DENPYOUGAKU");
            // 下段の請求・消費税
            dtFooter.Columns.Add("DOWN_SHOUHIZEI");
            // 下段の請求・合計（税込）
            dtFooter.Columns.Add("DOWN_GOUKEI_ZEIKOMI");
            // 下段の請求・御精算額
            dtFooter.Columns.Add("DOWN_SEISANGAKU");
            // 下段の請求・差引残高
            dtFooter.Columns.Add("DOWN_SASHIHIKIZANDAKA");

            // 計量情報計量証明項目1
            dtFooter.Columns.Add("KEIRYOU_JYOUHOU1");
            // 計量情報計量証明項目2
            dtFooter.Columns.Add("KEIRYOU_JYOUHOU2");
            // 計量情報計量証明項目3
            dtFooter.Columns.Add("KEIRYOU_JYOUHOU3");

            // 会社名
            dtFooter.Columns.Add("CORP_RYAKU_NAME");
            // 拠点
            dtFooter.Columns.Add("KYOTEN_NAME");
            // 拠点郵便番号
            dtFooter.Columns.Add("KYOTEN_POST");    // No.3048
            // 拠点住所1
            dtFooter.Columns.Add("KYOTEN_ADDRESS1");
            // 拠点住所2
            dtFooter.Columns.Add("KYOTEN_ADDRESS2");
            // 拠点電話
            dtFooter.Columns.Add("KYOTEN_TEL");
            // 拠点FAX
            dtFooter.Columns.Add("KYOTEN_FAX");

            // 相殺後金額
            dtFooter.Columns.Add("SOUSAI_KINGAKU");

            //登録番号
            dtFooter.Columns.Add("TOUROKU_NO");
            //相殺ラベル①
            dtFooter.Columns.Add("SOUSAI_LBL1");
            //相殺ラベル②
            dtFooter.Columns.Add("SOUSAI_LBL2");
            //課税ラベル
            dtFooter.Columns.Add("KAZEI_LBL");
            //取引先ラベル
            dtFooter.Columns.Add("TORIHIKI_LBL");

            rowTmp = dtFooter.NewRow();

            //現場
            rowTmp["GENBA"] = strGenbaName;

            //正味合計
            rowTmp["SHOUMI_GOUKEI"] = "";

            //合計金額
            rowTmp["GOUKEI_KINGAKU"] = string.Empty;
            if (Type == DENPYO_SHIKIRISHO_KIND.SEIKYUU)
            {
                if (SHIKIRISHO_UR_UTIZEI == 0)
                {
                    rowTmp["GOUKEI_KINGAKU"] = this.form.URIAGE_AMOUNT_TOTAL.Text;
                }
            }
            else if (Type == DENPYO_SHIKIRISHO_KIND.SHIHARAI)
            {
                if (SHIKIRISHO_SH_UTIZEI == 0)
                {
                    rowTmp["GOUKEI_KINGAKU"] = this.form.SHIHARAI_KINGAKU_TOTAL.Text;
                }
            }

            //備考
            rowTmp["BIKOU"] = this.form.DENPYOU_BIKOU.Text;    // No.2613
            //rowTmp["BIKOU"] = InsertReturn(this.form.DENPYOU_BIKOU.Text, 5);    // No.2613-->No.3837により改行不要

            string strPrefixForSeikyu = "";
            string strPrefixForShiharai = "";
            string strLabelKeyForSeikyu = "";
            string strLabelKeyForShiharai = "";
            string strLabelSousaiKbn = "";

            decimal decTopSeisangaku = 0;
            decimal decBottomSeisangaku = 0;
            string strUrShCalcBaseKbn = this.dto.sysInfoEntity.UR_SH_CALC_BASE_KBN.ToString();
            if (strUrShCalcBaseKbn == "1")
            {
                strPrefixForSeikyu = "UP";
                strPrefixForShiharai = "DOWN";
                strLabelKeyForSeikyu = "SEIKYUU_SHIHARAI1";
                strLabelKeyForShiharai = "SEIKYUU_SHIHARAI2";
            }
            else
            {
                strPrefixForSeikyu = "DOWN";
                strPrefixForShiharai = "UP";
                strLabelKeyForShiharai = "SEIKYUU_SHIHARAI1";
                strLabelKeyForSeikyu = "SEIKYUU_SHIHARAI2";
            }
            if (Type == DENPYO_SHIKIRISHO_KIND.SOUSAI)
            {
                #region 相殺
                /*********請求*********/
                {
                    //ラベル
                    rowTmp[strLabelKeyForSeikyu] = "請求";

                    //前回残高
                    rowTmp[strPrefixForSeikyu + "_ZENKAI_ZANDAKA"] = this.form.denpyouHakouPopUpDTO.Seikyu_Zenkai_Zentaka;

                    //伝票額(税抜)
                    rowTmp[strPrefixForSeikyu + "_DENPYOUGAKU"] = this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Kingaku;

                    //消費税
                    rowTmp[strPrefixForSeikyu + "_SHOUHIZEI"] = this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Zeigaku;

                    //合計(税込)
                    rowTmp[strPrefixForSeikyu + "_GOUKEI_ZEIKOMI"] = this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Rorihiki;

                    //御清算額
                    decimal decSousaiKingaku = 0;
                    decimal decNyushukkinKingaku = 0;
                    decimal decKonkai_Torihiki = 0;
                    decimal decZenkai_Zentaka = 0;
                    int isSeikyu_Seisan_Kbn = 1;

                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Sousatu_Kingaku, out decSousaiKingaku);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Nyusyu_Kingaku, out decNyushukkinKingaku);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Rorihiki, out decKonkai_Torihiki);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Zenkai_Zentaka, out decZenkai_Zentaka);
                    int.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Seisan_Kbn, out isSeikyu_Seisan_Kbn);
                    string strGoseisangaku = CommonCalc.DecimalFormat(decSousaiKingaku + decNyushukkinKingaku);
                    rowTmp[strPrefixForSeikyu + "_SEISANGAKU"] = strGoseisangaku;

                    string baseKbn = this.dto.sysInfoEntity.UR_SH_CALC_BASE_KBN.ToString();
                    // 支払精算区分「1.する」の場合
                    if (isSeikyu_Seisan_Kbn == 1)
                    {
                        if (baseKbn == "1")
                        {
                            decBottomSeisangaku = (decZenkai_Zentaka + decKonkai_Torihiki) - (decSousaiKingaku + decNyushukkinKingaku);
                        }
                        else
                        {
                            decTopSeisangaku = (decZenkai_Zentaka + decKonkai_Torihiki) - (decSousaiKingaku + decNyushukkinKingaku);
                        }
                    }
                    else
                    {
                        if (baseKbn == "1")
                        {
                            decTopSeisangaku = decKonkai_Torihiki - (decSousaiKingaku + decNyushukkinKingaku);
                        }
                        else
                        {
                            decBottomSeisangaku = decKonkai_Torihiki - (decSousaiKingaku + decNyushukkinKingaku);
                        }
                    }

                    //差引残高
                    rowTmp[strPrefixForSeikyu + "_SASHIHIKIZANDAKA"] = this.form.denpyouHakouPopUpDTO.Seikyu_Sagaku_Zentaka;
                }

                /*********支払*********/
                {
                    //ラベル
                    rowTmp[strLabelKeyForShiharai] = "支払";

                    //前回残高
                    rowTmp[strPrefixForShiharai + "_ZENKAI_ZANDAKA"] = this.form.denpyouHakouPopUpDTO.Shiharai_Zenkai_Zentaka;

                    //伝票額(税抜)
                    rowTmp[strPrefixForShiharai + "_DENPYOUGAKU"] = this.form.denpyouHakouPopUpDTO.Shiharai_Konkai_Kingaku;

                    //消費税
                    rowTmp[strPrefixForShiharai + "_SHOUHIZEI"] = this.form.denpyouHakouPopUpDTO.Shiharai_Konkai_Zeigaku;

                    //合計(税込)
                    rowTmp[strPrefixForShiharai + "_GOUKEI_ZEIKOMI"] = this.form.denpyouHakouPopUpDTO.Shiharai_Konkai_Rorihiki;

                    //御清算額
                    decimal decSousaiKingaku = 0;
                    decimal decNyushukkinKingaku = 0;
                    decimal decKonkai_Torihiki = 0;
                    decimal decZenkai_Zentaka = 0;
                    int isShiharai_Seisan_Kbn = 1;

                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Sousatu_Kingaku, out decSousaiKingaku);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Nyusyu_Kingaku, out decNyushukkinKingaku);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Konkai_Rorihiki, out decKonkai_Torihiki);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Zenkai_Zentaka, out decZenkai_Zentaka);
                    int.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Seisan_Kbn, out isShiharai_Seisan_Kbn);
                    string strGoseisangaku = CommonCalc.DecimalFormat(decSousaiKingaku + decNyushukkinKingaku);
                    rowTmp[strPrefixForShiharai + "_SEISANGAKU"] = strGoseisangaku;

                    string baseKbn = this.dto.sysInfoEntity.UR_SH_CALC_BASE_KBN.ToString();

                    // 支払精算区分「1.する」の場合
                    if (isShiharai_Seisan_Kbn == 1)
                    {
                        if (baseKbn == "1")
                        {
                            decBottomSeisangaku = (decZenkai_Zentaka + decKonkai_Torihiki) - (decSousaiKingaku + decNyushukkinKingaku);
                        }
                        else
                        {
                            decTopSeisangaku = (decZenkai_Zentaka + decKonkai_Torihiki) - (decSousaiKingaku + decNyushukkinKingaku);
                        }
                    }
                    else
                    {
                        if (baseKbn == "1")
                        {
                            decBottomSeisangaku = decKonkai_Torihiki - (decSousaiKingaku + decNyushukkinKingaku);
                        }
                        else
                        {
                            decTopSeisangaku = decKonkai_Torihiki - (decSousaiKingaku + decNyushukkinKingaku);
                        }
                    }

                    //差引残高
                    rowTmp[strPrefixForShiharai + "_SASHIHIKIZANDAKA"] = this.form.denpyouHakouPopUpDTO.Shiharai_Sagaku_Zentaka;
                }
                #endregion 相殺
            }
            else
            {
                #region 売上or支払
                strPrefixForSeikyu = "UP";
                strPrefixForShiharai = "DOWN";
                strLabelKeyForSeikyu = "SEIKYUU_SHIHARAI1";
                strLabelKeyForShiharai = "SEIKYUU_SHIHARAI2";
                /*********上段表示*********/
                if (Type == DENPYO_SHIKIRISHO_KIND.SEIKYUU)
                {
                    #region 仕切書(売上)
                    //ラベル
                    rowTmp[strLabelKeyForSeikyu] = "請求";
                    //前回残高
                    rowTmp[strPrefixForSeikyu + "_ZENKAI_ZANDAKA"] = this.form.denpyouHakouPopUpDTO.Seikyu_Zenkai_Zentaka;
                    //伝票額(税抜)
                    rowTmp[strPrefixForSeikyu + "_DENPYOUGAKU"] = this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Kingaku;
                    //消費税
                    rowTmp[strPrefixForSeikyu + "_SHOUHIZEI"] = this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Zeigaku;
                    //合計(税込)
                    rowTmp[strPrefixForSeikyu + "_GOUKEI_ZEIKOMI"] = this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Rorihiki;

                    //御清算額
                    decimal decSousaiKingaku = 0;
                    decimal decNyushukkinKingaku = 0;
                    decimal decKonkai_Torihiki = 0;
                    decimal decZenkai_Zentaka = 0;
                    string strGoseisangaku = string.Empty;
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Sousatu_Kingaku, out decSousaiKingaku);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Nyusyu_Kingaku, out decNyushukkinKingaku);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Konkai_Rorihiki, out decKonkai_Torihiki);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Zenkai_Zentaka, out decZenkai_Zentaka);
                    
                    //相殺する
                    if (this.form.denpyouHakouPopUpDTO.Sosatu == "1")
                    {
                        rowTmp["SOUSAI_LBL1"] = "相殺金額";
                        rowTmp[strPrefixForSeikyu + "_SEISANGAKU"] = decSousaiKingaku;

                        rowTmp["SOUSAI_LBL2"] = "御請求額";
                        strGoseisangaku = CommonCalc.DecimalFormat(decZenkai_Zentaka + decKonkai_Torihiki - decSousaiKingaku - decNyushukkinKingaku);
                        rowTmp[strPrefixForSeikyu + "_SASHIHIKIZANDAKA"] = strGoseisangaku;
                    }
                    else
                    {
                        rowTmp["SOUSAI_LBL1"] = "御請求額";
                        rowTmp[strPrefixForSeikyu + "_SEISANGAKU"] = decKonkai_Torihiki;
                        rowTmp["SOUSAI_LBL2"] = "差引残高";
                        strGoseisangaku = CommonCalc.DecimalFormat(decZenkai_Zentaka + decKonkai_Torihiki - decNyushukkinKingaku);
                        rowTmp[strPrefixForSeikyu + "_SASHIHIKIZANDAKA"] = strGoseisangaku;
                    }

                    /*********下段表示*********/
                    rowTmp[strLabelKeyForShiharai] = "内訳";
                    rowTmp["KAZEI_LBL"] = string.Format("{0:0%}", Decimal.Parse(this.form.denpyouHakouPopUpDTO.Seikyu_Syohizei_Ritu)) + "対象";
                    if (this.form.denpyouHakouPopUpDTO.R_KAZEI_KINGAKU != "0")
                    {
                        //課税金額
                        rowTmp[strPrefixForShiharai + "_DENPYOUGAKU"] = this.form.denpyouHakouPopUpDTO.R_KAZEI_KINGAKU;
                        //課税消費税
                        rowTmp[strPrefixForShiharai + "_SHOUHIZEI"] = this.form.denpyouHakouPopUpDTO.R_KAZEI_SHOUHIZEI;
                    }
                    if (this.form.denpyouHakouPopUpDTO.R_HIKAZEI_KINGAKU != "0")
                    {
                        //非課税額
                        rowTmp[strPrefixForShiharai + "_SEISANGAKU"] = this.form.denpyouHakouPopUpDTO.R_HIKAZEI_KINGAKU;
                        //非課税消費税
                        rowTmp[strPrefixForShiharai + "_SASHIHIKIZANDAKA"] = string.Empty;
                    }
                    //取引先
                    rowTmp["TORIHIKI_LBL"] = "取引先";

                    #endregion 仕切書(売上)

                }
                if (Type == DENPYO_SHIKIRISHO_KIND.SHIHARAI)
                {
                    #region 仕切書(支払)
                    /*********上段表示*********/
                    //ラベル
                    rowTmp[strLabelKeyForSeikyu] = "支払";
                    //前回残高
                    rowTmp[strPrefixForSeikyu + "_ZENKAI_ZANDAKA"] = this.form.denpyouHakouPopUpDTO.Shiharai_Zenkai_Zentaka;
                    //伝票額(税抜)
                    rowTmp[strPrefixForSeikyu + "_DENPYOUGAKU"] = this.form.denpyouHakouPopUpDTO.Shiharai_Konkai_Kingaku;
                    //消費税
                    rowTmp[strPrefixForSeikyu + "_SHOUHIZEI"] = this.form.denpyouHakouPopUpDTO.Shiharai_Konkai_Zeigaku;
                    //合計(税込)
                    rowTmp[strPrefixForSeikyu + "_GOUKEI_ZEIKOMI"] = this.form.denpyouHakouPopUpDTO.Shiharai_Konkai_Rorihiki;

                    //御清算額
                    decimal decSousaiKingaku = 0;
                    decimal decNyushukkinKingaku = 0;
                    decimal decKonkai_Torihiki = 0;
                    decimal decZenkai_Zentaka = 0;
                    string strGoseisangaku = string.Empty;

                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Sousatu_Kingaku, out decSousaiKingaku);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Nyusyu_Kingaku, out decNyushukkinKingaku);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Konkai_Rorihiki, out decKonkai_Torihiki);
                    decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Zenkai_Zentaka, out decZenkai_Zentaka);

                    //相殺する
                    if (this.form.denpyouHakouPopUpDTO.Sosatu == "1")
                    {
                        rowTmp["SOUSAI_LBL1"] = "相殺金額";
                        rowTmp[strPrefixForSeikyu + "_SEISANGAKU"] = decSousaiKingaku;

                        rowTmp["SOUSAI_LBL2"] = "御精算額";
                        strGoseisangaku = CommonCalc.DecimalFormat(decZenkai_Zentaka + decKonkai_Torihiki - decSousaiKingaku - decNyushukkinKingaku);
                        rowTmp[strPrefixForSeikyu + "_SASHIHIKIZANDAKA"] = strGoseisangaku;
                    }
                    else
                    {
                        rowTmp["SOUSAI_LBL1"] = "御精算額";
                        rowTmp[strPrefixForSeikyu + "_SEISANGAKU"] = decKonkai_Torihiki;
                        rowTmp["SOUSAI_LBL2"] = "差引残高";
                        strGoseisangaku = CommonCalc.DecimalFormat(decZenkai_Zentaka + decKonkai_Torihiki - decNyushukkinKingaku);
                        rowTmp[strPrefixForSeikyu + "_SASHIHIKIZANDAKA"] = strGoseisangaku;
                    }

                    /*********下段表示*********/
                    rowTmp[strLabelKeyForShiharai] = "内訳";
                    rowTmp["KAZEI_LBL"] = string.Format("{0:0%}", Decimal.Parse(this.form.denpyouHakouPopUpDTO.Shiharai_Syohizei_Ritu)) + "対象";
                    if (this.form.denpyouHakouPopUpDTO.R_KAZEI_KINGAKU_SHIHARAI != "0")
                    {
                        //課税金額
                        rowTmp[strPrefixForShiharai + "_DENPYOUGAKU"] = this.form.denpyouHakouPopUpDTO.R_KAZEI_KINGAKU_SHIHARAI;
                        //課税消費税
                        rowTmp[strPrefixForShiharai + "_SHOUHIZEI"] = this.form.denpyouHakouPopUpDTO.R_KAZEI_SHOUHIZEI_SHIHARAI;
                    }
                    if (this.form.denpyouHakouPopUpDTO.R_HIKAZEI_KINGAKU_SHIHARAI != "0")
                    {
                        //非課税額
                        rowTmp[strPrefixForShiharai + "_SEISANGAKU"] = this.form.denpyouHakouPopUpDTO.R_HIKAZEI_KINGAKU_SHIHARAI;
                        //非課税消費税
                        rowTmp[strPrefixForShiharai + "_SASHIHIKIZANDAKA"] = string.Empty;
                    }
                    //取引先
                    rowTmp["TORIHIKI_LBL"] = "取引先";
                    // 取引先支払情報
                    var torihikisakiShiharaiEntity = this.accessor.GetTorihikisakiShiharai(this.form.TORIHIKISAKI_CD.Text);
                    if (torihikisakiShiharaiEntity != null)
                    {
                        if (!string.IsNullOrEmpty(torihikisakiShiharaiEntity.TOUROKU_NO))
                        {
                            rowTmp["TORIHIKI_LBL"] = "取引先(登録番号：" + torihikisakiShiharaiEntity.TOUROKU_NO + ")";
                        }
                    }

                    #endregion 仕切書(支払)
                }
                #endregion 売上or支払
            }

            //会社名
            rowTmp["CORP_RYAKU_NAME"] = "";
            M_CORP_INFO entCorpInfo = CommonShogunData.CORP_INFO;
            if (entCorpInfo != null)
            {
                if (!string.IsNullOrEmpty(entCorpInfo.CORP_NAME))
                {
                    rowTmp["CORP_RYAKU_NAME"] = entCorpInfo.CORP_NAME;
                }
                if (Type == DENPYO_SHIKIRISHO_KIND.SEIKYUU)
                {
                    if (!string.IsNullOrEmpty(entCorpInfo.TOUROKU_NO))
                    {
                        rowTmp["TOUROKU_NO"] = entCorpInfo.TOUROKU_NO;
                    }
                }
            }

            rowTmp["KYOTEN_NAME"] = "";
            rowTmp["KYOTEN_POST"] = ""; // No.3048
            rowTmp["KYOTEN_ADDRESS1"] = "";
            rowTmp["KYOTEN_ADDRESS2"] = "";
            rowTmp["KYOTEN_TEL"] = "";
            rowTmp["KYOTEN_FAX"] = "";
            rowTmp["KEIRYOU_JYOUHOU1"] = "";
            rowTmp["KEIRYOU_JYOUHOU2"] = "";
            rowTmp["KEIRYOU_JYOUHOU3"] = "";

            if (entKyotenInfo != null)
            {
                //拠点
                if (!string.IsNullOrEmpty(entKyotenInfo.KYOTEN_NAME))
                {
                    rowTmp["KYOTEN_NAME"] = entKyotenInfo.KYOTEN_NAME;
                }

                //No.3048-->
                //拠点FAX
                if (!string.IsNullOrEmpty(entKyotenInfo.KYOTEN_POST))
                {
                    rowTmp["KYOTEN_POST"] = entKyotenInfo.KYOTEN_POST;
                }
                // No.3048<--

                //拠点住所1
                if (!string.IsNullOrEmpty(entKyotenInfo.KYOTEN_ADDRESS1))
                {
                    rowTmp["KYOTEN_ADDRESS1"] = entKyotenInfo.KYOTEN_ADDRESS1;
                }

                //拠点住所2
                if (!string.IsNullOrEmpty(entKyotenInfo.KYOTEN_ADDRESS2))
                {

                    rowTmp["KYOTEN_ADDRESS2"] = entKyotenInfo.KYOTEN_ADDRESS2;
                }

                //拠点電話
                if (!string.IsNullOrEmpty(entKyotenInfo.KYOTEN_TEL))
                {
                    rowTmp["KYOTEN_TEL"] = entKyotenInfo.KYOTEN_TEL;
                }

                //拠点FAX
                if (!string.IsNullOrEmpty(entKyotenInfo.KYOTEN_FAX))
                {
                    rowTmp["KYOTEN_FAX"] = entKyotenInfo.KYOTEN_FAX;
                }

                //計量情報計量証明項目1
                if (!string.IsNullOrEmpty(entKyotenInfo.KEIRYOU_SHOUMEI_1))
                {
                    rowTmp["KEIRYOU_JYOUHOU1"] = entKyotenInfo.KEIRYOU_SHOUMEI_1;
                }

                //計量情報計量証明項目2
                if (!string.IsNullOrEmpty(entKyotenInfo.KEIRYOU_SHOUMEI_2))
                {
                    rowTmp["KEIRYOU_JYOUHOU2"] = entKyotenInfo.KEIRYOU_SHOUMEI_2;
                }

                //計量情報計量証明項目3
                if (!string.IsNullOrEmpty(entKyotenInfo.KEIRYOU_SHOUMEI_3))
                {
                    rowTmp["KEIRYOU_JYOUHOU3"] = entKyotenInfo.KEIRYOU_SHOUMEI_3;
                }
            }

            //相殺後金額
            if (Type == DENPYO_SHIKIRISHO_KIND.SOUSAI && ConstClass.SOUSATU_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Sosatu))
            {
                decimal decSousaigoKingaku = decTopSeisangaku - decBottomSeisangaku;

                string baseKbn = this.dto.sysInfoEntity.UR_SH_CALC_BASE_KBN.ToString();
                if (baseKbn == "1")
                {
                    if (decSousaigoKingaku == 0)
                    {
                        strLabelSousaiKbn = "金額";
                    }
                    else if (decSousaigoKingaku.ToString().Contains("-"))
                    {
                        strLabelSousaiKbn = "支払金額";
                    }
                    else
                    {
                        strLabelSousaiKbn = "請求金額";
                    }
                }
                else
                {
                    if (decSousaigoKingaku == 0)
                    {
                        strLabelSousaiKbn = "金額";
                    }
                    else if (decSousaigoKingaku.ToString().Contains("-"))
                    {
                        strLabelSousaiKbn = "請求金額";
                    }
                    else
                    {
                        strLabelSousaiKbn = "支払金額";
                    }
                }

                string strSousaigoKingaku = CommonCalc.DecimalFormat(decSousaigoKingaku);
                strSousaigoKingaku = strSousaigoKingaku.Replace("-", "");
                rowTmp["SOUSAI_KINGAKU"] = "相殺後" + strLabelSousaiKbn + ":" + strSousaigoKingaku;
            }
            else
            {
                rowTmp["SOUSAI_KINGAKU"] = "";
            }

            dtFooter.Rows.Add(rowTmp);

            return dtFooter;
        }
        /// <summary>
        /// コンテナ指定画面表示
        /// </summary>
        internal bool OpenContena()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 編集不可
                string mode = "2";
                switch (this.form.WindowType)
                {
                    // 新規・修正モードの場合
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 編集可
                        mode = "1";
                        break;
                }

                // コンテナ指定画面(G184)をモーダル表示
                var assembly = Assembly.LoadFrom("ContenaShitei.dll");
                string denpyouDate = this.form.DENPYOU_DATE.Value == null ? null : this.form.DENPYOU_DATE.Value.ToString();
                var callForm1 = (SuperForm)assembly.CreateInstance(
                        "Shougun.Core.Common.ContenaShitei.UIForm",
                        false,
                        BindingFlags.CreateInstance,
                        null,
                        new object[] { mode, "1", this.dto.contenaResultList, denpyouDate, this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text },
                        null,
                        null
                      );
                var callHeader = (HeaderBaseForm)assembly.CreateInstance(
                        "Shougun.Core.Common.ContenaShitei.UIHeader",
                        false,
                        BindingFlags.CreateInstance,
                        null,
                        null,
                        null,
                        null
                );
                if (callForm1.IsDisposed)
                {
                    return false;
                }
                var businessForm = new BasePopForm(callForm1, callHeader);
                var ret = businessForm.ShowDialog();

                //戻り値
                Type baseObj = assembly.GetType("Shougun.Core.Common.ContenaShitei.UIForm");
                PropertyInfo val = baseObj.GetProperty("RetCntRetList");
                if (val != null)
                {
                    List<T_CONTENA_RESULT> tmpList = (List<T_CONTENA_RESULT>)val.GetValue(callForm1, null);
                    if (tmpList != null)
                    {
                        this.dto.contenaResultList = tmpList;
                    }
                }
                businessForm.Dispose();

                //設置／引揚の表示変更
                this.form.txtSecchi.Text = string.Empty;
                this.form.txtHikiage.Text = string.Empty;
                if (this.dto.contenaResultList == null)
                {
                    return false;
                }

                SqlInt16 sumSecchi = 0;     // No.3342
                SqlInt16 sumHikiage = 0;    // No.3342
                foreach (T_CONTENA_RESULT entity in this.dto.contenaResultList)
                {
                    //伝種区分（売上支払）を設定
                    entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;

                    if (entity.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI
                            && entity.DENSHU_KBN_CD == SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI
                            && entity.DELETE_FLG.Equals(SqlBoolean.False))
                    {
                        //設置
                        if (!entity.DAISUU_CNT.Equals(SqlInt16.Null))
                        {
                            //this.form.txtSecchi.Text = entity.DAISUU_CNT.ToString();     // No.3342
                            sumSecchi += entity.DAISUU_CNT;     // No.3342
                        }
                    }
                    else if (entity.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE
                             && entity.DENSHU_KBN_CD == SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI
                             && entity.DELETE_FLG.Equals(SqlBoolean.False))
                    {
                        //引揚
                        if (!entity.DAISUU_CNT.Equals(SqlInt16.Null))
                        {
                            //this.form.txtHikiage.Text = entity.DAISUU_CNT.ToString();     // No.3342
                            sumHikiage += entity.DAISUU_CNT;     // No.3342
                        }
                    }
                }
                this.form.txtSecchi.Text = sumSecchi.ToString();    // No.3342
                this.form.txtHikiage.Text = sumHikiage.ToString();    // No.3342

                //コンテナ操作CDの設定
                SetContenaInfo();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("OpenContena", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }
        /// <summary>
        /// 運賃入力画面表示
        /// </summary>
        internal bool OpenUnchinNyuuryoku(object sender, System.EventArgs e)
        {
            try
            {
                // 4935_8 売上支払入力 jyokou 20150505 str
                LogUtility.DebugMethodStart(sender, e);

                // 運賃入力画面(G153)をモーダル表示
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    this.form.Regist(sender, e);
                    if (isRegistered)
                    {
                        FormManager.OpenFormModal("G153", WINDOW_TYPE.NEW_WINDOW_FLAG, this.dto.entryEntity.UR_SH_NUMBER, SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI);
                        isRegistered = false;
                    }
                }
                else
                {
                    T_UR_SH_ENTRY entry = this.CreateUnchiDateEntity();
                    FormManager.OpenFormModal("G153", WINDOW_TYPE.UPDATE_WINDOW_FLAG, this.dto.entryEntity.UR_SH_NUMBER, SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI, entry);
                }
                //// 運賃入力画面(G153)をモーダル表示
                //r_framework.FormManager.FormManager.OpenFormModal("G153", WINDOW_TYPE.UPDATE_WINDOW_FLAG, SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI, this.dto.entryEntity.SYSTEM_ID);
                // 4935_8 売上支払入力 jyokou 20150505 end

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("OpenUnchinNyuuryoku", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 運賃入力画面データ移行
        /// </summary>
        internal T_UR_SH_ENTRY CreateUnchiDateEntity()
        {
            LogUtility.DebugMethodStart();
            T_UR_SH_ENTRY entry = new T_UR_SH_ENTRY();

            //拠点CD
            if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
            {
                entry.KYOTEN_CD = SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text);
            }
            //伝票日付
            if (this.form.DENPYOU_DATE.Value != null)
            {
                entry.DENPYOU_DATE = ((DateTime)this.form.DENPYOU_DATE.Value).Date;
            }
            // 運搬業者CD
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                entry.UNPAN_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_NAME.Text))
            {
                entry.UNPAN_GYOUSHA_NAME = this.form.UNPAN_GYOUSHA_NAME.Text;
            }
            // 車輌CD
            if (!string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
            {
                entry.SHARYOU_CD = this.form.SHARYOU_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHARYOU_NAME_RYAKU.Text))
            {
                entry.SHARYOU_NAME = this.form.SHARYOU_NAME_RYAKU.Text;
            }
            // 車種CD
            if (!string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
            {
                entry.SHASHU_CD = this.form.SHASHU_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHASHU_NAME.Text))
            {
                entry.SHASHU_NAME = this.form.SHASHU_NAME.Text;
            }
            // 運転者CD
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
            {
                entry.UNTENSHA_CD = this.form.UNTENSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_NAME.Text))
            {
                entry.UNTENSHA_NAME = this.form.UNTENSHA_NAME.Text;
            }
            // 形態区分CD
            if (!string.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text))
            {
                entry.KEITAI_KBN_CD = SqlInt16.Parse(this.form.KEITAI_KBN_CD.Text);
            }
            // 荷積業者CD
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
            {
                entry.NIZUMI_GYOUSHA_CD = this.form.NIZUMI_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_NAME.Text))
            {
                entry.NIZUMI_GYOUSHA_NAME = this.form.NIZUMI_GYOUSHA_NAME.Text;
            }
            // 荷積現場CD
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text))
            {
                entry.NIZUMI_GENBA_CD = this.form.NIZUMI_GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GENBA_NAME.Text))
            {
                entry.NIZUMI_GENBA_NAME = this.form.NIZUMI_GENBA_NAME.Text;
            }

            // 荷降業者CD
            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
            {
                entry.NIOROSHI_GYOUSHA_CD = this.form.NIOROSHI_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_NAME.Text))
            {
                entry.NIOROSHI_GYOUSHA_NAME = this.form.NIOROSHI_GYOUSHA_NAME.Text;
            }
            // 荷降現場CD
            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
            {
                entry.NIOROSHI_GENBA_CD = this.form.NIOROSHI_GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_NAME.Text))
            {
                entry.NIOROSHI_GENBA_NAME = this.form.NIOROSHI_GENBA_NAME.Text;
            }

            // 業者CD
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                entry.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_NAME_RYAKU.Text))
            {
                entry.GYOUSHA_NAME = this.form.GYOUSHA_NAME_RYAKU.Text;
            }
            // 現場CD
            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                entry.GENBA_CD = this.form.GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GENBA_NAME_RYAKU.Text))
            {
                entry.GENBA_NAME = this.form.GENBA_NAME_RYAKU.Text;
            }

            LogUtility.DebugMethodEnd();
            return entry;
        }

        /// <summary>
        /// 品名コードの存在チェック（伝種区分が売上/支払、または共通のみ可）
        /// </summary>
        /// <param name="targetRow"></param>
        /// <returns>true: 入力された品名コードが存在する, false: 入力された品名コードが存在しない</returns>
        internal bool CheckHinmeiCd(Row targetRow)
        {
            try
            {
                LogUtility.DebugMethodStart();
                bool returnVal = false;

                if ((targetRow.Cells["HINMEI_CD"].Value == null) || (string.IsNullOrEmpty(targetRow.Cells["HINMEI_CD"].Value.ToString())))
                {
                    // 品名コードの入力がない場合
                    return returnVal;
                }

                M_HINMEI hinmei = this.accessor.GetHinmeiDataByCd(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());
                if (hinmei == null || string.IsNullOrEmpty(hinmei.HINMEI_CD))
                {
                    // 品名コードがマスタに存在しない場合
                    // ただし、部品で存在チェックが行われるため、実際にここを通ることはない
                    return returnVal;
                }
                else
                {
                    // 品名コードがマスタに存在する場合
                    if ((hinmei.DENSHU_KBN_CD != SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI
                        && hinmei.DENSHU_KBN_CD != SalesPaymentConstans.DENSHU_KBN_CD_KYOTU))
                    {
                        // 入力された品名コードに紐づく伝種区分が売上/支払、共通以外の場合はエラーメッセージ表示
                        targetRow.Cells[CELL_NAME_HINMEI_CD].Value = null;
                        targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = null;
                        targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value = null;
                        targetRow.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = null;
                        targetRow.Cells[CELL_NAME_UNIT_CD].Value = null;
                        targetRow.Cells[CELL_NAME_UNIT_NAME_RYAKU].Value = null;
                        targetRow.Cells[CELL_NAME_SUURYOU].ReadOnly = false;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E058", "");
                        return returnVal;
                    }
                }

                returnVal = true;
                LogUtility.DebugMethodEnd();
                return returnVal;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckHinmeiCd", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHinmeiCd", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        // 20151021 katen #13337 品名手入力に関する機能修正 start
        internal bool GetHinmei(Row targetRow, out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart();
                bool returnVal = false;
                catchErr = false;

                if ((targetRow.Cells[CELL_NAME_HINMEI_CD].Value == null) || (string.IsNullOrEmpty(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString())))
                {
                    // 品名コードの入力がない場合
                    return returnVal;
                }

                M_KOBETSU_HINMEI_TANKA kobetsuHinmeiTanka = this.accessor.GetKobetsuHinmeiTankaDataByCd(this.form.TORIHIKISAKI_CD.Text, this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString(), this.form.DENPYOU_DATE.Text);
                if (kobetsuHinmeiTanka != null)
                {
                    // 個別品名が存在するか検索する。
                    M_KOBETSU_HINMEI kobetsuHinmei = this.accessor.GetKobetsuHinmeiDataByCd(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString(), 1);

                    if (kobetsuHinmei != null)
                    {
                        targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = kobetsuHinmei.SEIKYUU_HINMEI_NAME;
                    }
                    else
                    {
                        M_HINMEI[] hinmeis = this.accessor.GetAllValidHinmeiData(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());

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
                else
                {
                    M_SYS_INFO mSysInfo = this.accessor.GetSysInfo();

                    // 個別品名が存在するか検索する。
                    M_KOBETSU_HINMEI kobetsuHinmei = this.accessor.GetKobetsuHinmeiDataByCd(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString(), 1);

                    if (kobetsuHinmei != null)
                    {
                        targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = kobetsuHinmei.SEIKYUU_HINMEI_NAME;

                        // 品名検索抽出区分 「1.個別品名単価」の場合
                        if (mSysInfo != null && mSysInfo.HINMEI_SEARCH_CHUSYUTSU_JOKEN.Value == 1)
                        {
                            // 警告メッセージを表示する。
                            this.form.errmessage.MessageBoxShowWarn("個別単価（契約単価）が未登録の品名ＣＤをセットしました。");
                        }
                    }
                    else
                    {
                        M_HINMEI[] hinmeis = this.accessor.GetAllValidHinmeiData(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());

                        if (hinmeis != null && hinmeis.Count() > 0)
                        {
                            targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = hinmeis[0].HINMEI_NAME;

                            // 品名検索抽出区分 「1.個別品名単価」の場合
                            if (mSysInfo != null && mSysInfo.HINMEI_SEARCH_CHUSYUTSU_JOKEN.Value == 1)
                            {
                                // 警告メッセージを表示する。
                                this.form.errmessage.MessageBoxShowWarn("個別単価（契約単価）が未登録の品名ＣＤをセットしました。");
                            }
                        }
                        else
                        {
                            returnVal = true;
                        }
                    }
                }
                LogUtility.DebugMethodEnd(returnVal,catchErr);
                return returnVal;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("GetHinmei", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true, catchErr);
                return true;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("GetHinmei", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true, catchErr);
                return true;
            }
        }

        internal bool GetHinmeiForPop(Row targetRow,out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart();
                bool returnVal = false;
                catchErr = false;

                if ((targetRow.Cells[CELL_NAME_HINMEI_CD].Value == null) || (string.IsNullOrEmpty(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString())))
                {
                    // 品名コードの入力がない場合
                    return returnVal;
                }

                M_KOBETSU_HINMEI kobetsuHinmeis = this.accessor.GetKobetsuHinmeiDataByCd(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString(), SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI);
                if (kobetsuHinmeis != null)
                {
                    targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = kobetsuHinmeis.SEIKYUU_HINMEI_NAME;
                }
                else
                {
                    M_HINMEI[] hinmeis = this.accessor.GetAllValidHinmeiData(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());

                    if (hinmeis != null && hinmeis.Count() > 0)
                    {
                        targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = hinmeis[0].HINMEI_NAME;
                    }
                }
                LogUtility.DebugMethodEnd(returnVal,catchErr);
                return returnVal;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("GetHinmeiForPop", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true, catchErr);
                return true;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("GetHinmeiForPop", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true, catchErr);
                return true;
            }
        }
        // 20151021 katen #13337 品名手入力に関する機能修正 end

        /// <summary>
        /// コンテナ関連情報設定
        /// </summary>
        internal void SetContenaInfo()
        {
            LogUtility.DebugMethodStart();

            // 設置台数
            int settiDaisu = 0;
            if (!string.IsNullOrEmpty(this.form.txtSecchi.Text))
                settiDaisu = int.Parse(this.form.txtSecchi.Text);
            // 引揚台数
            int hikiageDaisu = 0;
            if (!string.IsNullOrEmpty(this.form.txtHikiage.Text))
                hikiageDaisu = int.Parse(this.form.txtHikiage.Text);

            // コンテナCDをセット
            if (settiDaisu > 0 && hikiageDaisu > 0)
            {
                this.form.CONTENA_SOUSA_CD.Text = "1";
            }
            else if (settiDaisu > 0 && hikiageDaisu == 0)
            {
                this.form.CONTENA_SOUSA_CD.Text = "2";
            }
            else if (settiDaisu == 0 && hikiageDaisu > 0)
            {
                this.form.CONTENA_SOUSA_CD.Text = "3";
            }
            else
            {
                this.form.CONTENA_SOUSA_CD.Text = string.Empty;
            }

            // コンテナ名をセット
            this.form.CONTENA_JOUKYOU_NAME_RYAKU.Text = string.Empty;
            M_CONTENA_JOUKYOU contenaJoukyou = new M_CONTENA_JOUKYOU();
            if (!string.IsNullOrEmpty(this.form.CONTENA_SOUSA_CD.Text))
                contenaJoukyou = this.accessor.GetContenaJoukyou(short.Parse(this.form.CONTENA_SOUSA_CD.Text));
            if (contenaJoukyou != null)
                this.form.CONTENA_JOUKYOU_NAME_RYAKU.Text = contenaJoukyou.CONTENA_JOUKYOU_NAME_RYAKU;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 数値の表示形式初期化（システム設定に基づき）
        /// </summary>
        internal void CalcValueFormatSettingInit()
        {
            LogUtility.DebugMethodStart();

            this.form.mrwDetail.SuspendLayout();
            var newTemplate = this.form.mrwDetail.Template;

            // 単価
            var obj1 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_TANKA });
            int TankaFormatCd = (int)this.dto.sysInfoEntity.SYS_TANKA_FORMAT_CD;
            foreach (var o in obj1)
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
            var obj2 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_SUURYOU });
            int SuuryouFormatCd = (int)this.dto.sysInfoEntity.SYS_SUURYOU_FORMAT_CD;
            foreach (var o in obj2)
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

            this.form.mrwDetail.Template = newTemplate;
            this.form.mrwDetail.ResumeLayout();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// コンテナマスタ更新
        /// </summary>
        public void UpdateContenaMaster()
        {
            LogUtility.DebugMethodStart();

            foreach (T_CONTENA_RESULT contenaRes in this.dto.contenaResultList)
            {

                M_CONTENA contenaMtr = this.accessor.GetContenaMaster(contenaRes.CONTENA_SHURUI_CD, contenaRes.CONTENA_CD);
                if (contenaMtr != null)
                {
                    // 設置日、引揚日をチェック
                    if ((!contenaMtr.SECCHI_DATE.IsNull
                        && contenaMtr.SECCHI_DATE.Value.Date > SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()).Value.Date)
                        || (!contenaMtr.HIKIAGE_DATE.IsNull
                        && contenaMtr.HIKIAGE_DATE.Value.Date > SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()).Value.Date))
                    {
                        // 設置日、引揚日が作業日より新しい場合は何もしない。
                        continue;
                    }

                    // 画面の入力内容をコンテナマスタに反映させる
                    if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                    {
                        contenaMtr.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    }
                    if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                    {
                        contenaMtr.GENBA_CD = this.form.GENBA_CD.Text;
                    }
                    contenaMtr.SHARYOU_CD = string.Empty;
                    if (contenaRes.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI)
                    {
                        // 設置の場合
                        if (!string.IsNullOrEmpty(this.form.DENPYOU_DATE.Text))
                        {
                            contenaMtr.SECCHI_DATE = SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString());
                            contenaMtr.HIKIAGE_DATE = SqlDateTime.Null;
                        }
                        contenaMtr.JOUKYOU_KBN = ConstCls.CONTENA_JOUKYOU_KBN_SECCHI;
                    }
                    else if (contenaRes.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE)
                    {
                        // 引揚の場合
                        contenaMtr.HIKIAGE_DATE = SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString());
                        contenaMtr.JOUKYOU_KBN = ConstCls.CONTENA_JOUKYOU_KBN_HIKIAGE;
                    }
                    // 自動設定項目
                    string createUser = contenaMtr.CREATE_USER;
                    SqlDateTime createDate = contenaMtr.CREATE_DATE;
                    string createPC = contenaMtr.CREATE_PC;
                    var dataBinderUkeireEntry = new DataBinderLogic<M_CONTENA>(contenaMtr);
                    dataBinderUkeireEntry.SetSystemProperty(contenaMtr, false);
                    // Create情報は前の状態を引き継ぐ
                    contenaMtr.CREATE_USER = createUser;
                    contenaMtr.CREATE_DATE = createDate;
                    contenaMtr.CREATE_PC = createPC;
                    // 最終更新者
                    //contenaMtr.UPDATE_USER = this.form.NYUURYOKU_TANTOUSHA_NAME.Text;

                    this.dto.contenaMasterList.Add(contenaMtr);
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ユーティリティ
        /// <summary>
        /// コントロールから登録用のEntityを作成する
        /// </summary>
        public virtual void CreateEntity()
        {
            LogUtility.DebugMethodStart();

            if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG) ||
                this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                this.dto.entryEntity = new T_UR_SH_ENTRY();
                this.dto.numberDay = new S_NUMBER_DAY();
                this.dto.numberYear = new S_NUMBER_YEAR();
                this.dto.numberReceipt = new S_NUMBER_RECEIPT();
                this.dto.numberReceiptYear = new S_NUMBER_RECEIPT_YEAR();
            }

            // T_UR_SH_ENTRYの設定
            // 日連番取得
            S_NUMBER_DAY[] numberDays = null;
            DateTime denpyouDate = this.footerForm.sysDate;  // 伝票日付
            short kyotenCd = -1;    // 拠点CD
            short.TryParse(this.headerForm.KYOTEN_CD.Text.ToString(), out kyotenCd);
            if (DateTime.TryParse(this.form.DENPYOU_DATE.Value.ToString(), out denpyouDate)
                && -1 < kyotenCd)
            {
                numberDays = this.accessor.GetNumberDay(denpyouDate.Date, SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI, kyotenCd);
            }

            // TODO: 年連番取得(S_NUMBER_YEARテーブルから情報取得 + 年度の生成処理を追加)
            S_NUMBER_YEAR[] numberYeas = null;
            SqlInt32 numberedYear = CorpInfoUtility.GetCurrentYear(denpyouDate.Date, (short)CommonShogunData.CORP_INFO.KISHU_MONTH);
            if (-1 < kyotenCd)
            {
                numberYeas = this.accessor.GetNumberYear(numberedYear, SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI, kyotenCd);
            }

            // モードに依存する処理
            byte[] numberDayTimeStamp = null;
            byte[] numberYearTimeStamp = null;
            int timeStampCount = 0;

            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:

                    // SYSTEM_IDの採番
                    SqlInt64 systemId = this.accessor.createSystemIdForUrsh();
                    this.dto.entryEntity.SYSTEM_ID = systemId;

                    // 受入番号の採番
                    this.dto.entryEntity.UR_SH_NUMBER = this.accessor.createUrshNumber();

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

                    //2次
                    foreach (T_CONTENA_RESULT entity in this.dto.contenaResultList)
                    {
                        // systemidとseqは入力テーブルと同じ内容をセットする
                        entity.SYSTEM_ID = this.dto.entryEntity.SYSTEM_ID;
                        entity.SEQ = this.dto.entryEntity.SEQ;
                        // 伝種区分セット
                        entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;
                        // 自動設定
                        var dataBinderContenaResult = new DataBinderLogic<T_CONTENA_RESULT>(entity);
                        dataBinderContenaResult.SetSystemProperty(entity, false);
                    }

                    //設置台数・引揚台数
                    this.dto.contenaReserveList = new List<T_CONTENA_RESERVE>();
                    if (!string.IsNullOrEmpty(this.form.UKETSUKE_NUMBER.Text))
                    {
                        DataTable dt = this.accessor.GetUketsukeSS(this.form.UKETSUKE_NUMBER.Text);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            var contenaReserveEntity = this.accessor.GetContenaReserve(dt.Rows[0]["SYSTEM_ID"].ToString(), dt.Rows[0]["SEQ"].ToString());
                            if (contenaReserveEntity != null && contenaReserveEntity.Length > 0)
                            {
                                this.dto.contenaReserveList = new List<T_CONTENA_RESERVE>();
                                foreach (T_CONTENA_RESERVE entity in contenaReserveEntity)
                                {
                                    this.dto.contenaReserveList.Add(entity);
                                }
                            }
                        }
                    }

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

                    this.dto.entryEntity.UR_SH_NUMBER = SqlInt64.Parse(this.form.ENTRY_NUMBER.Text);
                    this.dto.entryEntity.SYSTEM_ID = this.beforDto.entryEntity.SYSTEM_ID;     // 更新されないはず
                    this.dto.entryEntity.SEQ = this.beforDto.entryEntity.SEQ + 1;
                    this.dto.entryEntity.DELETE_FLG = false;
                    // 更新前伝票は論理削除
                    this.beforDto.entryEntity.DELETE_FLG = true;
                    //2次
                    foreach (T_CONTENA_RESULT entity in this.beforDto.contenaResultList)
                    {
                        entity.DELETE_FLG = true;
                        // 自動設定
                        var dataBinderContenaResult = new DataBinderLogic<T_CONTENA_RESULT>(entity);
                        dataBinderContenaResult.SetSystemProperty(entity, false);
                    }

                    // 月極一括作成区分
                    if (!string.IsNullOrEmpty(this.form.TSUKI_CREATE_KBN.Text))
                    {
                        this.dto.entryEntity.TSUKI_CREATE_KBN = Convert.ToBoolean(this.form.TSUKI_CREATE_KBN.Text);
                    }

                    // 排他制御用
                    this.beforDto.entryEntity.TIME_STAMP = ConvertStrByte.StringToByte(this.form.TIME_STAMP.Text);
                    //2次
                    foreach (T_CONTENA_RESULT entity in this.dto.contenaResultList)
                    {
                        // systemidとseqは入力テーブルと同じ内容をセットする
                        entity.SYSTEM_ID = this.dto.entryEntity.SYSTEM_ID;
                        entity.SEQ = this.dto.entryEntity.SEQ;
                        // 伝種区分セット
                        entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;
                        // 自動設定
                        var dataBinderContenaResult = new DataBinderLogic<T_CONTENA_RESULT>(entity);
                        dataBinderContenaResult.SetSystemProperty(entity, false);
                        entity.CREATE_DATE = this.beforDto.entryEntity.CREATE_DATE;
                        entity.CREATE_PC = this.beforDto.entryEntity.CREATE_PC;
                        entity.CREATE_USER = this.beforDto.entryEntity.CREATE_USER;
                    }

                    //設置台数・引揚台数
                    this.dto.contenaReserveList = new List<T_CONTENA_RESERVE>();
                    if (!string.IsNullOrEmpty(this.form.UKETSUKE_NUMBER.Text))
                    {
                        DataTable data = this.accessor.GetUketsukeSS(this.form.UKETSUKE_NUMBER.Text);
                        if (data != null && data.Rows.Count > 0)
                        {
                            var contenaReserveEntity = this.accessor.GetContenaReserve(data.Rows[0]["SYSTEM_ID"].ToString(), data.Rows[0]["SEQ"].ToString());
                            if (contenaReserveEntity != null && contenaReserveEntity.Length > 0)
                            {
                                this.dto.contenaReserveList = new List<T_CONTENA_RESERVE>();
                                foreach (T_CONTENA_RESERVE entity in contenaReserveEntity)
                                {
                                    this.dto.contenaReserveList.Add(entity);
                                }
                            }
                        }
                    }

                    break;

                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    this.dto.entryEntity.DELETE_FLG = true;
                    this.dto.entryEntity.TIME_STAMP = ConvertStrByte.StringToByte(this.form.TIME_STAMP.Text);
                    /// 20141118 Houkakou 「更新日、登録日の見直し」　start
                    //2次
                    foreach (T_CONTENA_RESULT entity in this.beforDto.contenaResultList)
                    {
                        entity.DELETE_FLG = true;
                        // 自動設定
                        var dataBinderContenaResult = new DataBinderLogic<T_CONTENA_RESULT>(entity);
                        dataBinderContenaResult.SetSystemProperty(entity, false);
                    }
                    //2次
                    foreach (T_CONTENA_RESULT entity in this.dto.contenaResultList)
                    {
                        // systemidとseqは入力テーブルと同じ内容をセットする
                        entity.SYSTEM_ID = this.dto.entryEntity.SYSTEM_ID;
                        entity.SEQ = this.dto.entryEntity.SEQ;
                        // 伝種区分セット
                        entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;
                        // 自動設定
                        //var dataBinderContenaResult = new DataBinderLogic<T_CONTENA_RESULT>(entity);
                        //dataBinderContenaResult.SetSystemProperty(entity, false);
                        entity.DELETE_FLG = true;
                        entity.CREATE_DATE = this.beforDto.entryEntity.CREATE_DATE;
                        entity.CREATE_PC = this.beforDto.entryEntity.CREATE_PC;
                        entity.CREATE_USER = this.beforDto.entryEntity.CREATE_USER;
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                        //entity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                        entity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                        entity.UPDATE_PC = SystemInformation.ComputerName;
                        entity.UPDATE_USER = SystemProperty.UserName;
                    }
                    /// 20141118 Houkakou 「更新日、登録日の見直し」　end
                    break;

                default:
                    break;

            }

            if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
            {
                this.dto.entryEntity.KYOTEN_CD = SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text);
            }

            if (!string.IsNullOrEmpty(this.form.KAKUTEI_KBN.Text))
            {
                this.dto.entryEntity.KAKUTEI_KBN = SqlInt16.Parse(this.form.KAKUTEI_KBN.Text);
            }
            else
            {
                this.dto.entryEntity.KAKUTEI_KBN = SalesPaymentConstans.KAKUTEI_KBN_MIKAKUTEI;
            }

            if (this.form.DENPYOU_DATE.Value != null)
            {
                this.dto.entryEntity.DENPYOU_DATE = ((DateTime)this.form.DENPYOU_DATE.Value).Date;
            }

            if (this.form.URIAGE_DATE.Value != null)
            {
                this.dto.entryEntity.URIAGE_DATE = ((DateTime)this.form.URIAGE_DATE.Value).Date;
            }

            if (this.form.SHIHARAI_DATE.Value != null)
            {
                this.dto.entryEntity.SHIHARAI_DATE = ((DateTime)this.form.SHIHARAI_DATE.Value).Date;
            }
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                this.dto.entryEntity.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_NAME_RYAKU.Text))
            {
                this.dto.entryEntity.TORIHIKISAKI_NAME = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
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
            if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOUSHA_CD.Text))
            {
                this.dto.entryEntity.EIGYOU_TANTOUSHA_CD = this.form.EIGYOU_TANTOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOUSHA_NAME.Text))
            {
                this.dto.entryEntity.EIGYOU_TANTOUSHA_NAME = this.form.EIGYOU_TANTOUSHA_NAME.Text;
            }
            //PhuocLoc 2020/12/01 #136221 -Start
            if (!string.IsNullOrEmpty(this.form.SHUUKEI_KOUMOKU_CD.Text))
            {
                this.dto.entryEntity.MOD_SHUUKEI_KOUMOKU_CD = this.form.SHUUKEI_KOUMOKU_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHUUKEI_KOUMOKU_NAME.Text))
            {
                this.dto.entryEntity.MOD_SHUUKEI_KOUMOKU_NAME = this.form.SHUUKEI_KOUMOKU_NAME.Text;
            }
            //PhuocLoc 2020/12/01 #136221 -End
            if (!string.IsNullOrEmpty(this.form.NYUURYOKU_TANTOUSHA_CD.Text))
            {
                this.dto.entryEntity.NYUURYOKU_TANTOUSHA_CD = this.form.NYUURYOKU_TANTOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NYUURYOKU_TANTOUSHA_NAME.Text))
            {
                this.dto.entryEntity.NYUURYOKU_TANTOUSHA_NAME = this.form.NYUURYOKU_TANTOUSHA_NAME.Text;
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
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                this.dto.entryEntity.UNPAN_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_NAME.Text))
            {
                this.dto.entryEntity.UNPAN_GYOUSHA_NAME = this.form.UNPAN_GYOUSHA_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
            {
                this.dto.entryEntity.UNTENSHA_CD = this.form.UNTENSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_NAME.Text))
            {
                this.dto.entryEntity.UNTENSHA_NAME = this.form.UNTENSHA_NAME.Text;
            }
            if (!string.IsNullOrEmpty(this.form.NINZUU_CNT.Text))
            {
                this.dto.entryEntity.NINZUU_CNT = SqlInt16.Parse(this.form.NINZUU_CNT.Text);
            }
            if (!string.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text))
            {
                this.dto.entryEntity.KEITAI_KBN_CD = SqlInt16.Parse(this.form.KEITAI_KBN_CD.Text);
            }
            if (!string.IsNullOrEmpty(this.form.CONTENA_SOUSA_CD.Text))
            {
                this.dto.entryEntity.CONTENA_SOUSA_CD = SqlInt16.Parse(this.form.CONTENA_SOUSA_CD.Text);
            }
            if (!string.IsNullOrEmpty(this.form.MANIFEST_SHURUI_CD.Text))
            {
                this.dto.entryEntity.MANIFEST_SHURUI_CD = SqlInt16.Parse(this.form.MANIFEST_SHURUI_CD.Text);
            }
            if (!string.IsNullOrEmpty(this.form.MANIFEST_TEHAI_CD.Text))
            {
                this.dto.entryEntity.MANIFEST_TEHAI_CD = SqlInt16.Parse(this.form.MANIFEST_TEHAI_CD.Text);
            }
            if (!string.IsNullOrEmpty(this.form.DENPYOU_BIKOU.Text))
            {
                this.dto.entryEntity.DENPYOU_BIKOU = this.form.DENPYOU_BIKOU.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UKETSUKE_NUMBER.Text))
            {
                this.dto.entryEntity.UKETSUKE_NUMBER = SqlInt64.Parse(this.form.UKETSUKE_NUMBER.Text);
            }
            if (!string.IsNullOrEmpty(this.form.RECEIPT_NUMBER_DAY.Text))
            {
                this.dto.entryEntity.RECEIPT_NUMBER = SqlInt32.Parse(this.form.RECEIPT_NUMBER_DAY.Text);
            }
            if (!string.IsNullOrEmpty(this.form.RECEIPT_NUMBER_YEAR.Text))
            {
                this.dto.entryEntity.RECEIPT_NUMBER_YEAR = SqlInt32.Parse(this.form.RECEIPT_NUMBER_YEAR.Text);
            }

            // 確定フラグのデフォ値を設定
            if (this.dto.sysInfoEntity.UR_SH_KAKUTEI_USE_KBN == SalesPaymentConstans.UR_SH_KAKUTEI_USE_KBN_NO)
            {
                // EntryはClearメソッドでデフォ値を設定しているためここでは設定しない

                // Detail
                foreach (Row row in this.form.mrwDetail.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }

                    row.Cells[CELL_NAME_KAKUTEI_KBN].Value = true;
                }
            }

            /**
             * 確定フラグの制御
             * 
             * ■システム設定の確定条件:伝票単位の場合
             * 　Detailの確定フラグ：Entryの確定フラグをセット
             * 　Detailの売上/支払日付：Entryの売上 or 支払日付をセット
             * 
             * ■システム設定の確定条件：明細単位の場合
             * 　Entryの確定フラグ：Detailの確定フラグに1つでも未確定があったら未確定にする
             * 　　　　　　　　　　 上記以外は確定でセット
             * 　Entryの売上日付：Detailの伝票区分：売上の中で、日付が一番古い日付
             * 　Entryの支払日付：Detailの伝票区分：支払の中で日付が一番古い日付
             */
            if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_DENPYOU)
            {
                // 伝票単位
                foreach (Row row in this.form.mrwDetail.Rows)
                {
                    if (row.IsNewRow || string.IsNullOrEmpty((string)row.Cells["ROW_NO"].Value.ToString()))
                    {
                        continue;
                    }

                    if (!this.dto.entryEntity.KAKUTEI_KBN.IsNull)
                    {
                        row.Cells[CELL_NAME_KAKUTEI_KBN].Value = (bool)(this.dto.entryEntity.KAKUTEI_KBN == SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI);
                    }
                    else
                    {
                        row.Cells[CELL_NAME_KAKUTEI_KBN].Value = false;
                    }

                    if (row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value != null
                        && !string.IsNullOrEmpty(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString()))
                    {
                        if (SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE_STR.Equals(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString())
                            && !this.dto.entryEntity.URIAGE_DATE.IsNull)
                        {
                            row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value = (DateTime)this.dto.entryEntity.URIAGE_DATE;
                            // 売上消費税率
                            // 直接指定されていればそちらを参照する
                            if (!string.IsNullOrEmpty(this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text))
                            {
                                row.Cells[CELL_NAME_SHOUHIZEI_RATE].Value = this.ToDecimalForUriageShouhizeiRate();
                            }
                            else
                            {
                                var shouhizeiRate = this.accessor.GetShouhizeiRate(((DateTime)this.dto.entryEntity.URIAGE_DATE).Date);
                                row.Cells[CELL_NAME_SHOUHIZEI_RATE].Value = shouhizeiRate.SHOUHIZEI_RATE;
                            }
                        }
                        else if (SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI_STR.Equals(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString())
                            && !this.dto.entryEntity.SHIHARAI_DATE.IsNull)
                        {
                            row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value = (DateTime)this.dto.entryEntity.SHIHARAI_DATE;
                            // 支払消費税率
                            if (!string.IsNullOrEmpty(this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text))
                            {
                                row.Cells[CELL_NAME_SHOUHIZEI_RATE].Value = this.ToDecimalForShiharaiShouhizeiRate();
                            }
                            else
                            {
                                var shouhizeiRate = this.accessor.GetShouhizeiRate(((DateTime)this.dto.entryEntity.URIAGE_DATE).Date);
                                row.Cells[CELL_NAME_SHOUHIZEI_RATE].Value = shouhizeiRate.SHOUHIZEI_RATE;
                            }
                        }
                    }
                }
            }
            else
            {
                // 明細単位
                short tempKakuteiKbn = SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI;
                SqlDateTime tempUriageDate = SqlDateTime.Null;
                SqlDateTime tempShiharaiDate = SqlDateTime.Null;
                decimal tempUriageShouhizeiRate = 0;
                decimal tempShiharaiShouhizeiRate = 0;
                foreach (Row row in this.form.mrwDetail.Rows)
                {
                    if (row.IsNewRow || string.IsNullOrEmpty((string)row.Cells["ROW_NO"].Value.ToString()))
                    {
                        continue;
                    }

                    if (row.Cells[CELL_NAME_KAKUTEI_KBN].Value == null
                        || !(bool)row.Cells[CELL_NAME_KAKUTEI_KBN].Value)
                    {
                        tempKakuteiKbn = SalesPaymentConstans.KAKUTEI_KBN_MIKAKUTEI;
                    }

                    DateTime tempUrShDate;
                    if (row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value != null
                        && DateTime.TryParse(row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value.ToString(), out tempUrShDate)
                        && (row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value != null
                            && !string.IsNullOrEmpty(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString())))
                    {
                        if (SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE_STR.Equals(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString()))
                        {
                            if (tempUriageDate.IsNull)
                            {
                                tempUriageDate = tempUrShDate.Date;
                            }
                            // 一番最後の日付かチェック
                            else if (tempUriageDate < tempUrShDate.Date)
                            {
                                tempUriageDate = tempUrShDate.Date;
                            }
                        }
                        else if (SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI_STR.Equals(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString()))
                        {
                            if (tempShiharaiDate.IsNull)
                            {
                                tempShiharaiDate = tempUrShDate.Date;
                            }
                            // 一番最後の日付かチェック
                            else if (tempShiharaiDate < tempUrShDate.Date)
                            {
                                tempShiharaiDate = tempUrShDate.Date;
                            }
                        }
                    }
                }

                this.dto.entryEntity.KAKUTEI_KBN = tempKakuteiKbn;
                this.dto.entryEntity.URIAGE_DATE = tempUriageDate;
                // 念のため画面にもセット
                if (!tempUriageDate.IsNull)
                {
                    this.form.URIAGE_DATE.Value = tempUriageDate;
                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(((DateTime)tempUriageDate).Date);
                    if (shouhizeiEntity != null
                        && !shouhizeiEntity.SHOUHIZEI_RATE.IsNull)
                    {
                        tempUriageShouhizeiRate = (decimal)shouhizeiEntity.SHOUHIZEI_RATE;
                    }
                }
                else
                {
                    this.form.URIAGE_DATE.Value = string.Empty;
                }

                this.dto.entryEntity.SHIHARAI_DATE = tempShiharaiDate;
                // 念のため画面にもセット
                if (!tempShiharaiDate.IsNull)
                {
                    this.form.SHIHARAI_DATE.Value = tempShiharaiDate;
                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(((DateTime)tempShiharaiDate).Date);
                    if (shouhizeiEntity != null
                        && !shouhizeiEntity.SHOUHIZEI_RATE.IsNull)
                    {
                        tempShiharaiShouhizeiRate = (decimal)shouhizeiEntity.SHOUHIZEI_RATE;
                    }
                }
                else
                {
                    this.form.SHIHARAI_DATE.Value = string.Empty;
                }

                // 消費税率
                this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE = tempUriageShouhizeiRate;
                this.dto.entryEntity.SHIHARAI_SHOUHIZEI_RATE = tempShiharaiShouhizeiRate;
                this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text = tempUriageShouhizeiRate.ToString();
                this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text = tempShiharaiShouhizeiRate.ToString();
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
                if (this.form.URIAGE_DATE.Value != null
                    && DateTime.TryParse(this.form.URIAGE_DATE.Value.ToString(), out uriageDate))
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
                if (this.form.SHIHARAI_DATE.Value != null
                    && DateTime.TryParse(this.form.SHIHARAI_DATE.Value.ToString(), out shiharaiDate))
                {
                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(shiharaiDate.Date);
                    if (shouhizeiEntity != null
                        && 0 < shouhizeiEntity.SHOUHIZEI_RATE)
                    {
                        this.dto.entryEntity.SHIHARAI_SHOUHIZEI_RATE = shouhizeiEntity.SHOUHIZEI_RATE;
                    }
                }
            }

            /**
             * 伝票発行画面にて取得したデータ
             */
            // 売上税計算区分CD
            int seikyuZeikeisanKbn = 0;
            if (int.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Zeikeisan_Kbn, out seikyuZeikeisanKbn))
            {
                this.dto.entryEntity.URIAGE_ZEI_KEISAN_KBN_CD = (SqlInt16)seikyuZeikeisanKbn;
            }
            // 売上税区分CD
            int uriageZeiKbnCd = 0;
            if (int.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn, out uriageZeiKbnCd))
                this.dto.entryEntity.URIAGE_ZEI_KBN_CD = (SqlInt16)uriageZeiKbnCd;
            // 売上取引区分CD
            int uriageTorihikiKbnCd = 0;
            if (int.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Rohiki_Kbn, out uriageTorihikiKbnCd))
            {
                this.dto.entryEntity.URIAGE_TORIHIKI_KBN_CD = (SqlInt16)uriageTorihikiKbnCd;
            }
            // 支払税計算区分CD
            int shiharaiZeiKeisanKbnCd = 0;
            if (int.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Zeikeisan_Kbn, out shiharaiZeiKeisanKbnCd))
            {
                this.dto.entryEntity.SHIHARAI_ZEI_KEISAN_KBN_CD = (SqlInt16)shiharaiZeiKeisanKbnCd;
            }
            // 支払税区分CD
            int shiharaiZeiKbnCd = 0;
            if (int.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn, out shiharaiZeiKbnCd))
            {
                this.dto.entryEntity.SHIHARAI_ZEI_KBN_CD = (SqlInt16)shiharaiZeiKbnCd;
            }
            // 支払取引区分CD
            int ShiharaiTorihikiKbnCd = 0;
            if (int.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Rohiki_Kbn, out ShiharaiTorihikiKbnCd))
            {
                this.dto.entryEntity.SHIHARAI_TORIHIKI_KBN_CD = (SqlInt16)ShiharaiTorihikiKbnCd;
            }

            // 代納フラッグ
            this.dto.entryEntity.DAINOU_FLG = false;

            var dataBinderUrshEntry = new DataBinderLogic<T_UR_SH_ENTRY>(this.dto.entryEntity);
            dataBinderUrshEntry.SetSystemProperty(this.dto.entryEntity, false);

            /// 20141118 Houkakou 「更新日、登録日の見直し」　start
            // 修正、削除モードの場合、Create情報は前の伝票のデータを引き継ぐ
            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    this.dto.entryEntity.CREATE_USER = this.beforDto.entryEntity.CREATE_USER;
                    this.dto.entryEntity.CREATE_DATE = this.beforDto.entryEntity.CREATE_DATE;
                    this.dto.entryEntity.CREATE_PC = this.beforDto.entryEntity.CREATE_PC;
                    break;

                default:
                    break;
            }
            /// 20141118 Houkakou 「更新日、登録日の見直し」　end

            // 最終更新者
            //this.dto.entryEntity.UPDATE_USER = this.form.NYUURYOKU_TANTOUSHA_NAME.Text;

            // 更新前伝票
            /// 20141118 Houkakou 「更新日、登録日の見直し」　start
            //var dataBinderBeforUrshEntry = new DataBinderLogic<T_UR_SH_ENTRY>(this.beforDto.entryEntity);
            //dataBinderUrshEntry.SetSystemProperty(this.beforDto.entryEntity, true);
            /// 20141118 Houkakou 「更新日、登録日の見直し」　end

            // Detail
            List<T_UR_SH_DETAIL> urshDetailEntitys = new List<T_UR_SH_DETAIL>();

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

            foreach (Row dr in this.form.mrwDetail.Rows)
            {
                if (dr.IsNewRow || string.IsNullOrEmpty((string)dr.Cells["ROW_NO"].Value.ToString()))
                {
                    continue;
                }

                T_UR_SH_DETAIL temp = new T_UR_SH_DETAIL();

                // モードに依存する処理
                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // 新規の場合は、既にEntryで採番しているので、それに+1する
                        detailSysId = this.accessor.createSystemIdForUrsh();
                        temp.DETAIL_SYSTEM_ID = detailSysId;

                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:

                        // DETAIL_SYSTEM_IDの採番
                        if (dr.Cells["DETAIL_SYSTEM_ID"].Value == null
                            || string.IsNullOrEmpty(dr.Cells["DETAIL_SYSTEM_ID"].Value.ToString()))
                        {
                            // 修正モードでT_UR_SH_DETAILが初めて登録されるパターンも張るはずなので、
                            // Detailが無ければ新たに採番(更新モードの場合、ここで初めて採番する)
                            detailSysId = this.accessor.createSystemIdForUrsh();
                        }
                        else
                        {
                            // 既に登録されていればそのまま使う
                            detailSysId = SqlInt64.Parse(dr.Cells["DETAIL_SYSTEM_ID"].Value.ToString());
                        }

                        temp.DETAIL_SYSTEM_ID = detailSysId;
                        break;

                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        /// 20141118 Houkakou 「更新日、登録日の見直し」　start
                        // DETAIL_SYSTEM_IDの採番
                        if (dr.Cells["DETAIL_SYSTEM_ID"].Value == null
                            || string.IsNullOrEmpty(dr.Cells["DETAIL_SYSTEM_ID"].Value.ToString()))
                        {
                            // 修正モードでT_UR_SH_DETAILが初めて登録されるパターンも張るはずなので、
                            // Detailが無ければ新たに採番(更新モードの場合、ここで初めて採番する)
                            detailSysId = this.accessor.createSystemIdForUrsh();
                        }
                        else
                        {
                            // 既に登録されていればそのまま使う
                            detailSysId = SqlInt64.Parse(dr.Cells["DETAIL_SYSTEM_ID"].Value.ToString());
                        }

                        temp.DETAIL_SYSTEM_ID = detailSysId;
                        /// 20141118 Houkakou 「更新日、登録日の見直し」　end
                        break;

                    default:
                        break;

                }

                temp.SYSTEM_ID = this.dto.entryEntity.SYSTEM_ID.Value;
                temp.SEQ = this.dto.entryEntity.SEQ;
                temp.UR_SH_NUMBER = this.dto.entryEntity.UR_SH_NUMBER;
                if (!string.IsNullOrEmpty(dr.Cells["ROW_NO"].Value.ToString()))
                {
                    temp.ROW_NO = SqlInt16.Parse(dr.Cells["ROW_NO"].Value.ToString());
                }

                if (this.dto.sysInfoEntity.UR_SH_KAKUTEI_USE_KBN == SalesPaymentConstans.UR_SH_KAKUTEI_USE_KBN_YES)
                {
                    if (dr.Cells["KAKUTEI_KBN"].Value != null && !string.IsNullOrEmpty(dr.Cells["KAKUTEI_KBN"].Value.ToString())
                        && Convert.ToBoolean(dr.Cells["KAKUTEI_KBN"].Value.ToString()) == true)
                    {
                        temp.KAKUTEI_KBN = SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI;
                    }
                    else
                    {
                        temp.KAKUTEI_KBN = SalesPaymentConstans.KAKUTEI_KBN_MIKAKUTEI;
                    }
                }

                else
                {
                    temp.KAKUTEI_KBN = SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI;
                }
                if (dr.Cells["URIAGESHIHARAI_DATE"].Value != null
                    && !string.IsNullOrEmpty(dr.Cells["URIAGESHIHARAI_DATE"].Value.ToString()))
                {
                    temp.URIAGESHIHARAI_DATE = (DateTime)dr.Cells["URIAGESHIHARAI_DATE"].Value;
                }

                if (dr.Cells["DENPYOU_KBN_CD"].Value != null && !string.IsNullOrEmpty(dr.Cells["DENPYOU_KBN_CD"].Value.ToString()))
                {
                    temp.DENPYOU_KBN_CD = SqlInt16.Parse(dr.Cells["DENPYOU_KBN_CD"].Value.ToString());
                }
                if (dr.Cells["HINMEI_CD"].Value != null)
                {
                    temp.HINMEI_CD = dr.Cells["HINMEI_CD"].Value.ToString();
                }
                if (dr.Cells["HINMEI_NAME"].Value != null)
                {
                    temp.HINMEI_NAME = dr.Cells["HINMEI_NAME"].Value.ToString();
                }
                if (dr.Cells["SUURYOU"].Value != null && !string.IsNullOrEmpty(dr.Cells["SUURYOU"].Value.ToString()))
                {
                    temp.SUURYOU = SqlDecimal.Parse(dr.Cells["SUURYOU"].Value.ToString());
                }
                if (dr.Cells["UNIT_CD"].Value != null && !string.IsNullOrEmpty(dr.Cells["UNIT_CD"].Value.ToString()))
                {
                    temp.UNIT_CD = SqlInt16.Parse(dr.Cells["UNIT_CD"].Value.ToString());
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
                if (dr.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value != null
                    && short.TryParse(dr.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value.ToString(), out hinmeiZeiKbnCd))
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
                decimal.TryParse(dr.Cells["KINGAKU"].Value.ToString(), out meisaiKingaku);

                temp.TAX_SOTO = 0;          // 消費税外税初期値
                temp.TAX_UCHI = 0;          // 消費税内税初期値
                temp.HINMEI_TAX_SOTO = 0;   // 品名別消費税外税初期値
                temp.HINMEI_TAX_UCHI = 0;   // 品名別消費税内税初期値

                decimal detailShouhizeiRate = 0;
                if (!temp.URIAGESHIHARAI_DATE.IsNull)
                {
                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(((DateTime)temp.URIAGESHIHARAI_DATE).Date);
                    if (shouhizeiEntity != null
                        && 0 < shouhizeiEntity.SHOUHIZEI_RATE)
                    {
                        detailShouhizeiRate = (decimal)shouhizeiEntity.SHOUHIZEI_RATE;
                    }
                }

                // もし消費税率が設定されていればそちらを優先して使う
                decimal tempShouhizeiRate = 0;
                if (dr.Cells[CELL_NAME_SHOUHIZEI_RATE].Value != null
                    && !string.IsNullOrEmpty(dr.Cells[CELL_NAME_SHOUHIZEI_RATE].Value.ToString())
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
                        // TODO: 明細毎消費税合計は品名.税区分CDがある場合はそれを使って計算するかどうか
                        // 設計Tへ確認

                        switch (temp.HINMEI_ZEI_KBN_CD.ToString())
                        {
                            case ConstClass.ZEI_KBN_1:
                                // 品名別消費税外税
                                temp.HINMEI_TAX_SOTO =
                                    CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        (int)this.dto.torihikisakiSeikyuuEntity.TAX_HASUU_CD);
                                HinmeiUrTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)this.dto.torihikisakiSeikyuuEntity.TAX_HASUU_CD,
                                        temp.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            case ConstClass.ZEI_KBN_2:
                                // 品名別消費税内税
                                temp.HINMEI_TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                temp.HINMEI_TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)temp.HINMEI_TAX_UCHI, (int)this.dto.torihikisakiSeikyuuEntity.TAX_HASUU_CD);
                                HinmeiUrTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)this.dto.torihikisakiSeikyuuEntity.TAX_HASUU_CD,
                                        temp.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
                        // publicにしてもらい、そこを参照すること
                        switch (this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn)
                        {
                            case ConstClass.ZEI_KBN_1:
                                UrTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)this.dto.torihikisakiSeikyuuEntity.TAX_HASUU_CD,
                                        this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn);
                                // 消費税外
                                temp.TAX_SOTO
                                    = CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        (int)this.dto.torihikisakiSeikyuuEntity.TAX_HASUU_CD);

                                break;

                            case ConstClass.ZEI_KBN_2:
                                UrTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)this.dto.torihikisakiSeikyuuEntity.TAX_HASUU_CD,
                                        this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn);
                                // 消費税内
                                temp.TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                temp.TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)temp.TAX_UCHI, (int)this.dto.torihikisakiSeikyuuEntity.TAX_HASUU_CD);
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
                        // TODO:明細毎消費税合計は 品名.税区分CDがある場合はそれを使って計算するかどうか
                        // 設計Tへ確認

                        switch (temp.HINMEI_ZEI_KBN_CD.ToString())
                        {
                            case ConstClass.ZEI_KBN_1:
                                // 品名別消費税外税
                                temp.HINMEI_TAX_SOTO =
                                    CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        (int)this.dto.torihikisakiShiharaiEntity.TAX_HASUU_CD);
                                HinmeiShTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)this.dto.torihikisakiShiharaiEntity.TAX_HASUU_CD,
                                        temp.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            case ConstClass.ZEI_KBN_2:
                                // 品名別消費税内税
                                temp.HINMEI_TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                temp.HINMEI_TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)temp.HINMEI_TAX_UCHI, (int)this.dto.torihikisakiShiharaiEntity.TAX_HASUU_CD);
                                HinmeiShTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)this.dto.torihikisakiShiharaiEntity.TAX_HASUU_CD,
                                        temp.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
                        // publicにしてもらい、そこを参照すること
                        switch (this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn)
                        {
                            case ConstClass.ZEI_KBN_1:
                                ShTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)this.dto.torihikisakiShiharaiEntity.TAX_HASUU_CD,
                                        this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn);
                                // 消費税外
                                temp.TAX_SOTO =
                                    CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        (int)this.dto.torihikisakiShiharaiEntity.TAX_HASUU_CD);

                                break;

                            case ConstClass.ZEI_KBN_2:
                                ShTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)this.dto.torihikisakiShiharaiEntity.TAX_HASUU_CD,
                                        this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn);
                                // 消費税内
                                temp.TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                temp.TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)temp.TAX_UCHI, (int)this.dto.torihikisakiShiharaiEntity.TAX_HASUU_CD);
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
                // TODO: 2次
                temp.NISUGATA_SUURYOU = 0;
                // TODO: 2次
                temp.NISUGATA_UNIT_CD = 0;

                var dbLogic = new DataBinderLogic<T_UR_SH_DETAIL>(temp);
                dbLogic.SetSystemProperty(temp, false);

                urshDetailEntitys.Add(temp);
            }

            this.dto.detailEntity = new T_UR_SH_DETAIL[urshDetailEntitys.Count];
            this.dto.detailEntity = urshDetailEntitys.ToArray<T_UR_SH_DETAIL>();

            // 明細の集計結果系
            // 品名別売上金額合計
            this.dto.entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL = HimeiUrKingakuTotal;
            // entityの値を使って計算するため、処理の最後に計算
            decimal uriageTotal = 0;
            decimal.TryParse(this.form.URIAGE_AMOUNT_TOTAL.Text, out uriageTotal);
            this.dto.entryEntity.URIAGE_AMOUNT_TOTAL = uriageTotal - this.dto.entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL.Value;

            /**
             * 売上の税金系計算
             */
            // 売上伝票毎消費税外税、品名別売上消費税外税合計
            // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
            // publicにしてもらい、そこを参照すること
            if (ConstClass.ZEI_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn))
            {
                this.dto.entryEntity.URIAGE_TAX_SOTO
                    = CommonCalc.FractionCalc(
                        (decimal)(this.dto.entryEntity.URIAGE_AMOUNT_TOTAL * this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE),
                        (int)this.dto.torihikisakiSeikyuuEntity.TAX_HASUU_CD);
            }
            else
            {
                this.dto.entryEntity.URIAGE_TAX_SOTO = 0;
            }

            this.dto.entryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL
                = CommonCalc.FractionCalc(
                    HinmeiUrTaxSotoTotal,
                    (int)this.dto.torihikisakiSeikyuuEntity.TAX_HASUU_CD);

            // 売上伝票毎消費税内税、品名別売上消費税内税合計
            // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
            // publicにしてもらい、そこを参照すること
            if (ConstClass.ZEI_KBN_2.Equals(this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn))
            {
                // 金額計算
                this.dto.entryEntity.URIAGE_TAX_UCHI
                    = (this.dto.entryEntity.URIAGE_AMOUNT_TOTAL
                        - (this.dto.entryEntity.URIAGE_AMOUNT_TOTAL / (this.dto.entryEntity.URIAGE_SHOUHIZEI_RATE + 1)));
                // 端数処理
                this.dto.entryEntity.URIAGE_TAX_UCHI
                    = CommonCalc.FractionCalc(
                        (decimal)this.dto.entryEntity.URIAGE_TAX_UCHI,
                        (int)this.dto.torihikisakiSeikyuuEntity.TAX_HASUU_CD);
            }
            else
            {
                this.dto.entryEntity.URIAGE_TAX_UCHI = 0;
            }

            // 金額計算
            this.dto.entryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL = HinmeiUrTaxUchiTotal;

            // 端数処理
            this.dto.entryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL
                = CommonCalc.FractionCalc(
                    (decimal)this.dto.entryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL,
                    (int)this.dto.torihikisakiSeikyuuEntity.TAX_HASUU_CD);

            // 売上伝票毎消費税外税合計
            this.dto.entryEntity.URIAGE_TAX_SOTO_TOTAL = UrTaxSotoTotal;

            // 売上伝票毎消費税内税合計
            this.dto.entryEntity.URIAGE_TAX_UCHI_TOTAL = UrTaxUchiTotal;

            // 品名別支払金額合計
            this.dto.entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL = HimeiShKingakuTotal;
            decimal shiharaiTotal = 0;
            decimal.TryParse(this.form.SHIHARAI_KINGAKU_TOTAL.Text, out shiharaiTotal);
            this.dto.entryEntity.SHIHARAI_AMOUNT_TOTAL = shiharaiTotal - this.dto.entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL.Value;

            /**
             * 支払の税金系計算
             */
            // 支払伝票毎消費税外税、品名別支払消費税外税合計
            // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
            // publicにしてもらい、そこを参照すること
            if (ConstClass.ZEI_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn))
            {
                this.dto.entryEntity.SHIHARAI_TAX_SOTO
                    = CommonCalc.FractionCalc(
                        (decimal)this.dto.entryEntity.SHIHARAI_AMOUNT_TOTAL * (decimal)this.dto.entryEntity.SHIHARAI_SHOUHIZEI_RATE,
                        (int)this.dto.torihikisakiShiharaiEntity.TAX_HASUU_CD);
            }
            else
            {
                this.dto.entryEntity.SHIHARAI_TAX_SOTO = 0;
            }

            this.dto.entryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL
                = CommonCalc.FractionCalc(
                    HinmeiShTaxSotoTotal,
                    (int)this.dto.torihikisakiShiharaiEntity.TAX_HASUU_CD);

            // 支払伝票毎消費税内税、品名別支払消費税内税合計
            if (ConstClass.ZEI_KBN_2.Equals(this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn))
            {
                // 金額計算
                this.dto.entryEntity.SHIHARAI_TAX_UCHI
                    = this.dto.entryEntity.SHIHARAI_AMOUNT_TOTAL
                        - (this.dto.entryEntity.SHIHARAI_AMOUNT_TOTAL / (this.dto.entryEntity.SHIHARAI_SHOUHIZEI_RATE + 1));
                // 端数処理
                this.dto.entryEntity.SHIHARAI_TAX_UCHI
                    = CommonCalc.FractionCalc(
                        (decimal)this.dto.entryEntity.SHIHARAI_TAX_UCHI,
                        (int)this.dto.torihikisakiShiharaiEntity.TAX_HASUU_CD);
            }
            else
            {
                this.dto.entryEntity.SHIHARAI_TAX_UCHI = 0;
            }

            // 金額計算
            this.dto.entryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = HinmeiShTaxUchiTotal;
            // 端数処理
            this.dto.entryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL
                = CommonCalc.FractionCalc(
                    (decimal)this.dto.entryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL,
                    (int)this.dto.torihikisakiShiharaiEntity.TAX_HASUU_CD);

            // 支払明細毎消費税外税合計
            this.dto.entryEntity.SHIHARAI_TAX_SOTO_TOTAL = ShTaxSotoTotal;

            // 支払明細毎消費税内税合計
            this.dto.entryEntity.SHIHARAI_TAX_UCHI_TOTAL = ShTaxUchiTotal;

            // S_NUMBER_YEAR
            this.dto.numberYear.NUMBERED_YEAR = numberedYear;
            this.dto.numberYear.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;
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
            this.dto.numberDay.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;
            this.dto.numberDay.KYOTEN_CD = kyotenCd;
            this.dto.numberDay.CURRENT_NUMBER = this.dto.entryEntity.DATE_NUMBER;
            this.dto.numberDay.DELETE_FLG = false;
            if (numberDayTimeStamp != null)
            {
                this.dto.numberDay.TIME_STAMP = numberDayTimeStamp;
            }
            var dataBinderNumberDay = new DataBinderLogic<S_NUMBER_DAY>(this.dto.numberDay);
            dataBinderNumberDay.SetSystemProperty(this.dto.numberDay, false);

            //T_CONTENA_RESULT
            foreach (T_CONTENA_RESULT entity in this.dto.contenaResultList)
            {
                //SEQ
                //entity.SEQ += 1;

                // 更新前伝票
                /// 20141118 Houkakou 「更新日、登録日の見直し」　start
                //var dataBinderContenaResult = new DataBinderLogic<T_CONTENA_RESULT>(entity);
                //dataBinderContenaResult.SetSystemProperty(entity, true);

                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //entity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                entity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                entity.UPDATE_PC = SystemInformation.ComputerName;
                entity.UPDATE_USER = SystemProperty.UserName;
                /// 20141118 Houkakou 「更新日、登録日の見直し」　end
            }

            // S_NUMBER_RECEIPTの更新

            // 領収証番号採番
            if (ConstClass.RYOSYUSYO_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Ryousyusyou))
            {
                DateTime hiduke = Convert.ToDateTime(this.form.DENPYOU_DATE.Value);

                /* 日連番 */
                var numberReceipt = this.accessor.GetNumberReceipt(hiduke, kyotenCd);
                if (numberReceipt == null)
                {
                    this.dto.numberReceipt.CURRENT_NUMBER = 1;
                    this.dto.entryEntity.RECEIPT_NUMBER = 1;
                }
                else
                {
                    int numberReceiptCurrentNumber = -1;
                    int.TryParse(Convert.ToString(numberReceipt.CURRENT_NUMBER), out numberReceiptCurrentNumber);
                    this.dto.numberReceipt.CURRENT_NUMBER = numberReceiptCurrentNumber + 1;
                    this.dto.numberReceipt.TIME_STAMP = numberReceipt.TIME_STAMP;
                    this.dto.entryEntity.RECEIPT_NUMBER = numberReceiptCurrentNumber + 1;
                }

                this.dto.numberReceipt.NUMBERED_DAY = hiduke.Date;
                this.dto.numberReceipt.KYOTEN_CD = kyotenCd;
                this.dto.numberReceipt.DELETE_FLG = false;
                var dataBinderNumberReceipt = new DataBinderLogic<S_NUMBER_RECEIPT>(this.dto.numberReceipt);
                dataBinderNumberReceipt.SetSystemProperty(this.dto.numberReceipt, false);

                /* 年連番 */
                var numberReceiptYear = this.accessor.GetNumberReceiptYear(hiduke, kyotenCd);
                if (numberReceiptYear == null)
                {
                    this.dto.numberReceiptYear.CURRENT_NUMBER = 1;
                    this.dto.entryEntity.RECEIPT_NUMBER_YEAR = 1;
                }
                else
                {
                    int numberReceiptYearCurrentNumber = -1;
                    int.TryParse(Convert.ToString(numberReceiptYear.CURRENT_NUMBER), out numberReceiptYearCurrentNumber);
                    this.dto.numberReceiptYear.CURRENT_NUMBER = numberReceiptYearCurrentNumber + 1;
                    this.dto.numberReceiptYear.TIME_STAMP = numberReceiptYear.TIME_STAMP;
                    this.dto.entryEntity.RECEIPT_NUMBER_YEAR = numberReceiptYearCurrentNumber + 1;
                }

                this.dto.numberReceiptYear.NUMBERED_YEAR = (SqlInt16)hiduke.Year;
                this.dto.numberReceiptYear.KYOTEN_CD = kyotenCd;
                this.dto.numberReceiptYear.DELETE_FLG = false;
                var dataBinderNumberReceiptYear = new DataBinderLogic<S_NUMBER_RECEIPT_YEAR>(this.dto.numberReceiptYear);
                dataBinderNumberReceiptYear.SetSystemProperty(this.dto.numberReceiptYear, false);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金、出金伝票作成
        /// </summary>
        internal void CreateNyuuShukkinEntity()
        {
            // 入金
            /**
             * 取引区分：掛け
             * 精算区分：どちらでも
             */
            if ((ConstClass.SEISAN_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Seikyu_Seisan_Kbn)
                && ConstClass.TORIHIKI_KBN_2.Equals(this.form.denpyouHakouPopUpDTO.Seikyu_Rohiki_Kbn))
                || (ConstClass.SEISAN_KBN_2.Equals(this.form.denpyouHakouPopUpDTO.Seikyu_Seisan_Kbn)
                && ConstClass.TORIHIKI_KBN_2.Equals(this.form.denpyouHakouPopUpDTO.Seikyu_Rohiki_Kbn)
                && ConstClass.SOUSATU_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Sosatu))
                )
            {
                /**
                 * 入金一括入力 + 入金入力
                 */
                T_NYUUKIN_SUM_ENTRY nyuukinSumEntry = new T_NYUUKIN_SUM_ENTRY();
                List<T_NYUUKIN_SUM_DETAIL> nyuukinSumDetailList = new List<T_NYUUKIN_SUM_DETAIL>();
                T_NYUUKIN_ENTRY nyuukinEntry = new T_NYUUKIN_ENTRY();
                List<T_NYUUKIN_DETAIL> nyuukinDetaiList = new List<T_NYUUKIN_DETAIL>();

                // 入金系以外から登録された場合に立てる
                nyuukinSumEntry.SEISAN_SOUSAI_CREATE_KBN = true;

                // SYSTEM_ID採番
                nyuukinSumEntry.SYSTEM_ID = this.commonAccesser.createSystemId(SalesPaymentConstans.DENSHU_KBN_CD_NYUUKIN);
                nyuukinSumEntry.SEQ = 1;
                nyuukinSumEntry.NYUUKIN_NUMBER = this.commonAccesser.createDenshuNumber(SalesPaymentConstans.DENSHU_KBN_CD_NYUUKIN);
                nyuukinEntry.SYSTEM_ID = this.commonAccesser.createSystemId(SalesPaymentConstans.DENSHU_KBN_CD_NYUUKIN);
                nyuukinEntry.SEQ = 1;
                short kyotenCd = 0;
                if (short.TryParse(this.headerForm.KYOTEN_CD.Text, out kyotenCd))
                {
                    nyuukinEntry.KYOTEN_CD = kyotenCd;
                    nyuukinSumEntry.KYOTEN_CD = kyotenCd;
                }

                // 入金番号採番
                nyuukinEntry.NYUUKIN_NUMBER = nyuukinSumEntry.NYUUKIN_NUMBER;
                nyuukinEntry.NYUUKIN_SUM_SYSTEM_ID = nyuukinSumEntry.SYSTEM_ID;
                nyuukinEntry.DENPYOU_DATE = ((DateTime)this.form.DENPYOU_DATE.Value).Date;
                nyuukinSumEntry.DENPYOU_DATE = ((DateTime)this.form.DENPYOU_DATE.Value).Date;
                nyuukinEntry.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                if (this.dto.torihikisakiSeikyuuEntity != null)
                {
                    nyuukinSumEntry.NYUUKINSAKI_CD = this.dto.torihikisakiSeikyuuEntity.NYUUKINSAKI_CD;

                    nyuukinEntry.NYUUKINSAKI_CD = this.dto.torihikisakiSeikyuuEntity.NYUUKINSAKI_CD;
                }
                nyuukinSumEntry.EIGYOU_TANTOUSHA_CD = this.form.EIGYOU_TANTOUSHA_CD.Text;
                nyuukinEntry.EIGYOU_TANTOUSHA_CD = this.form.EIGYOU_TANTOUSHA_CD.Text;
                // 入金額合計は明細を作成した後に計算する
                nyuukinSumEntry.CHOUSEI_AMOUNT_TOTAL = 0;
                nyuukinEntry.CHOUSEI_AMOUNT_TOTAL = 0;
                nyuukinSumEntry.KARIUKEKIN_WARIATE_TOTAL = 0;
                nyuukinSumEntry.DELETE_FLG = false;
                nyuukinEntry.DELETE_FLG = false;

                // 入金一括入力
                var dataBinderNyuukinSumEntry = new DataBinderLogic<T_NYUUKIN_SUM_ENTRY>(nyuukinSumEntry);
                dataBinderNyuukinSumEntry.SetSystemProperty(nyuukinSumEntry, false);

                // 入金入力
                var dataBinderNyuukinEntry = new DataBinderLogic<T_NYUUKIN_ENTRY>(nyuukinEntry);
                dataBinderNyuukinEntry.SetSystemProperty(nyuukinEntry, false);

                /**
                 * 入金一括明細 + 入金明細
                 */
                short rowCount = 1;

                // 相殺明細
                decimal seikyuuSousaiKingaku = 0;
                if (decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Sousatu_Kingaku, out seikyuuSousaiKingaku)
                    && seikyuuSousaiKingaku != 0)
                {
                    T_NYUUKIN_SUM_DETAIL nyuukinSumDetailForSousai = new T_NYUUKIN_SUM_DETAIL();
                    nyuukinSumDetailForSousai.DETAIL_SYSTEM_ID = this.commonAccesser.createSystemId(SalesPaymentConstans.DENSHU_KBN_CD_NYUUKIN);
                    nyuukinSumDetailForSousai.SYSTEM_ID = nyuukinSumEntry.SYSTEM_ID;
                    nyuukinSumDetailForSousai.SEQ = nyuukinSumEntry.SEQ;
                    nyuukinSumDetailForSousai.ROW_NUMBER = (SqlInt16)rowCount;
                    nyuukinSumDetailForSousai.NYUUSHUKKIN_KBN_CD = SalesPaymentConstans.NYUUSHUKKIN_KBN_CD_SOUSAI;
                    nyuukinSumDetailForSousai.KINGAKU = seikyuuSousaiKingaku;
                    var dataBinderNyuukinSumSousai = new DataBinderLogic<T_NYUUKIN_SUM_DETAIL>(nyuukinSumDetailForSousai);
                    dataBinderNyuukinSumSousai.SetSystemProperty(nyuukinSumDetailForSousai, false);

                    T_NYUUKIN_DETAIL nyuukinDetailForSeikyuuSousai = new T_NYUUKIN_DETAIL();
                    nyuukinDetailForSeikyuuSousai.SYSTEM_ID = nyuukinEntry.SYSTEM_ID;
                    nyuukinDetailForSeikyuuSousai.SEQ = nyuukinEntry.SEQ;
                    nyuukinDetailForSeikyuuSousai.DETAIL_SYSTEM_ID = this.commonAccesser.createSystemId(SalesPaymentConstans.DENSHU_KBN_CD_NYUUKIN);
                    nyuukinDetailForSeikyuuSousai.ROW_NUMBER = (SqlInt16)rowCount;
                    nyuukinDetailForSeikyuuSousai.NYUUSHUKKIN_KBN_CD = SalesPaymentConstans.NYUUSHUKKIN_KBN_CD_SOUSAI;
                    nyuukinDetailForSeikyuuSousai.KINGAKU = seikyuuSousaiKingaku;

                    var dataBinderNyuukinSousai = new DataBinderLogic<T_NYUUKIN_DETAIL>(nyuukinDetailForSeikyuuSousai);
                    dataBinderNyuukinSousai.SetSystemProperty(nyuukinDetailForSeikyuuSousai, false);

                    nyuukinSumDetailList.Add(nyuukinSumDetailForSousai);
                    nyuukinDetaiList.Add(nyuukinDetailForSeikyuuSousai);
                    rowCount++;
                }

                // 現金明細
                decimal seikyuuNyuukingaku = 0;
                if (decimal.TryParse(this.form.denpyouHakouPopUpDTO.Seikyu_Nyusyu_Kingaku, out seikyuuNyuukingaku)
                    && seikyuuNyuukingaku != 0)
                {
                    T_NYUUKIN_SUM_DETAIL nyuukinSumDetailForSeikyuuKingaku = new T_NYUUKIN_SUM_DETAIL();
                    nyuukinSumDetailForSeikyuuKingaku.DETAIL_SYSTEM_ID = this.commonAccesser.createSystemId(SalesPaymentConstans.DENSHU_KBN_CD_NYUUKIN);
                    nyuukinSumDetailForSeikyuuKingaku.SYSTEM_ID = nyuukinSumEntry.SYSTEM_ID;
                    nyuukinSumDetailForSeikyuuKingaku.SEQ = nyuukinSumEntry.SEQ;
                    nyuukinSumDetailForSeikyuuKingaku.ROW_NUMBER = (SqlInt16)rowCount;
                    nyuukinSumDetailForSeikyuuKingaku.NYUUSHUKKIN_KBN_CD = SalesPaymentConstans.NYUUSHUKKIN_KBN_CD_GENKIN;
                    nyuukinSumDetailForSeikyuuKingaku.KINGAKU = seikyuuNyuukingaku;
                    var dataBinderNyuukinSumSousai = new DataBinderLogic<T_NYUUKIN_SUM_DETAIL>(nyuukinSumDetailForSeikyuuKingaku);
                    dataBinderNyuukinSumSousai.SetSystemProperty(nyuukinSumDetailForSeikyuuKingaku, false);

                    T_NYUUKIN_DETAIL nyuukinDetailForSeikyuuKingaku = new T_NYUUKIN_DETAIL();
                    nyuukinDetailForSeikyuuKingaku.SYSTEM_ID = nyuukinEntry.SYSTEM_ID;
                    nyuukinDetailForSeikyuuKingaku.SEQ = nyuukinEntry.SEQ;
                    nyuukinDetailForSeikyuuKingaku.DETAIL_SYSTEM_ID = this.commonAccesser.createSystemId(SalesPaymentConstans.DENSHU_KBN_CD_NYUUKIN);
                    nyuukinDetailForSeikyuuKingaku.ROW_NUMBER = (SqlInt16)rowCount;
                    nyuukinDetailForSeikyuuKingaku.NYUUSHUKKIN_KBN_CD = SalesPaymentConstans.NYUUSHUKKIN_KBN_CD_GENKIN;
                    nyuukinDetailForSeikyuuKingaku.KINGAKU = seikyuuNyuukingaku;

                    var dataBinderNyuukinKingaku = new DataBinderLogic<T_NYUUKIN_DETAIL>(nyuukinDetailForSeikyuuKingaku);
                    dataBinderNyuukinKingaku.SetSystemProperty(nyuukinDetailForSeikyuuKingaku, false);

                    nyuukinSumDetailList.Add(nyuukinSumDetailForSeikyuuKingaku);
                    nyuukinDetaiList.Add(nyuukinDetailForSeikyuuKingaku);
                }

                // TODO: 入金額計算
                nyuukinSumEntry.NYUUKIN_AMOUNT_TOTAL = seikyuuNyuukingaku;
                nyuukinEntry.NYUUKIN_AMOUNT_TOTAL = seikyuuNyuukingaku;
                nyuukinSumEntry.CHOUSEI_AMOUNT_TOTAL = seikyuuSousaiKingaku;
                nyuukinEntry.CHOUSEI_AMOUNT_TOTAL = seikyuuSousaiKingaku;

                // セット
                this.nyuuShukkinDto.nyuukinSumEntry = nyuukinSumEntry;
                this.nyuuShukkinDto.nyuukinEntry = nyuukinEntry;
                this.nyuuShukkinDto.nyuukinSumDetails = nyuukinSumDetailList;
                this.nyuuShukkinDto.nyuukinDetials = nyuukinDetaiList;
            }

            // 出金
            /**
             * 取引区分：掛け
             * 精算区分：どちらでも
             */
            if ((ConstClass.SEISAN_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Shiharai_Seisan_Kbn)
                && ConstClass.TORIHIKI_KBN_2.Equals(this.form.denpyouHakouPopUpDTO.Shiharai_Rohiki_Kbn))
                || (ConstClass.SEISAN_KBN_2.Equals(this.form.denpyouHakouPopUpDTO.Seikyu_Seisan_Kbn)
                && ConstClass.TORIHIKI_KBN_2.Equals(this.form.denpyouHakouPopUpDTO.Seikyu_Rohiki_Kbn)
                && ConstClass.SOUSATU_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Sosatu))
                )
            {
                /**
                 * 出金入力
                 */

                T_SHUKKIN_ENTRY shukkinEntry = new T_SHUKKIN_ENTRY();
                List<T_SHUKKIN_DETAIL> shukkinDetaiList = new List<T_SHUKKIN_DETAIL>();

                // 出金系以外から登録された場合に立てる
                shukkinEntry.SEISAN_SOUSAI_CREATE_KBN = true;

                // SYSTEM_ID採番
                shukkinEntry.SYSTEM_ID = this.commonAccesser.createSystemId(SalesPaymentConstans.DENSHU_KBN_CD_SHUKKIN);
                shukkinEntry.SEQ = 1;
                short kyotenCd = 0;
                if (short.TryParse(this.headerForm.KYOTEN_CD.Text, out kyotenCd))
                {
                    shukkinEntry.KYOTEN_CD = kyotenCd;
                }

                // 出金番号採番
                shukkinEntry.SHUKKIN_NUMBER = this.commonAccesser.createDenshuNumber(SalesPaymentConstans.DENSHU_KBN_CD_SHUKKIN);
                shukkinEntry.DENPYOU_DATE = ((DateTime)this.form.DENPYOU_DATE.Value).Date;
                shukkinEntry.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                shukkinEntry.EIGYOU_TANTOUSHA_CD = this.form.EIGYOU_TANTOUSHA_CD.Text;
                // 出金額合計は明細を作成した後に計算
                shukkinEntry.CHOUSEI_AMOUNT_TOTAL = 0;
                shukkinEntry.DELETE_FLG = false;

                var dataBinderNyuukinEntry = new DataBinderLogic<T_SHUKKIN_ENTRY>(shukkinEntry);
                dataBinderNyuukinEntry.SetSystemProperty(shukkinEntry, false);

                /**
                 * 出金明細
                 */
                short rowCount = 1;

                // 相殺明細
                decimal shiharaiSousaiKingaku = 0;
                if (decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Sousatu_Kingaku, out shiharaiSousaiKingaku)
                    && shiharaiSousaiKingaku != 0)
                {
                    T_SHUKKIN_DETAIL shukkinDetailForShiharaiSousai = new T_SHUKKIN_DETAIL();
                    shukkinDetailForShiharaiSousai.SYSTEM_ID = shukkinEntry.SYSTEM_ID;
                    shukkinDetailForShiharaiSousai.SEQ = shukkinEntry.SEQ;
                    shukkinDetailForShiharaiSousai.DETAIL_SYSTEM_ID = this.commonAccesser.createSystemId(SalesPaymentConstans.DENSHU_KBN_CD_SHUKKIN);
                    shukkinDetailForShiharaiSousai.ROW_NUMBER = (SqlInt16)rowCount;
                    shukkinDetailForShiharaiSousai.NYUUSHUKKIN_KBN_CD = SalesPaymentConstans.NYUUSHUKKIN_KBN_CD_SOUSAI;
                    shukkinDetailForShiharaiSousai.KINGAKU = shiharaiSousaiKingaku;

                    var dataBinderShiharaiSousai = new DataBinderLogic<T_SHUKKIN_DETAIL>(shukkinDetailForShiharaiSousai);
                    dataBinderShiharaiSousai.SetSystemProperty(shukkinDetailForShiharaiSousai, false);

                    shukkinDetaiList.Add(shukkinDetailForShiharaiSousai);
                    rowCount++;
                }

                // 現金明細
                decimal shiharaiShukkingaku = 0;
                if (decimal.TryParse(this.form.denpyouHakouPopUpDTO.Shiharai_Nyusyu_Kingaku, out shiharaiShukkingaku)
                    && shiharaiShukkingaku != 0)
                {
                    T_SHUKKIN_DETAIL shukkinDetailForShiharaiKingaku = new T_SHUKKIN_DETAIL();
                    shukkinDetailForShiharaiKingaku.SYSTEM_ID = shukkinEntry.SYSTEM_ID;
                    shukkinDetailForShiharaiKingaku.SEQ = shukkinEntry.SEQ;
                    shukkinDetailForShiharaiKingaku.DETAIL_SYSTEM_ID = this.commonAccesser.createSystemId(SalesPaymentConstans.DENSHU_KBN_CD_SHUKKIN);
                    shukkinDetailForShiharaiKingaku.ROW_NUMBER = (SqlInt16)rowCount;
                    shukkinDetailForShiharaiKingaku.NYUUSHUKKIN_KBN_CD = SalesPaymentConstans.NYUUSHUKKIN_KBN_CD_GENKIN;
                    shukkinDetailForShiharaiKingaku.KINGAKU = shiharaiShukkingaku;

                    var dataBinderShukkinKingaku = new DataBinderLogic<T_SHUKKIN_DETAIL>(shukkinDetailForShiharaiKingaku);
                    dataBinderShukkinKingaku.SetSystemProperty(shukkinDetailForShiharaiKingaku, false);

                    shukkinDetaiList.Add(shukkinDetailForShiharaiKingaku);
                }

                // 入金額計算
                shukkinEntry.SHUKKIN_AMOUNT_TOTAL = shiharaiShukkingaku;
                shukkinEntry.CHOUSEI_AMOUNT_TOTAL = shiharaiSousaiKingaku;

                // セット
                this.nyuuShukkinDto.shukkinEntry = shukkinEntry;
                this.nyuuShukkinDto.shukkinDetails = shukkinDetaiList;
            }

        }

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
                && this.form.UrShNumber != -1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 明細の制御(システム設定情報用)
        /// </summary>
        /// <param name="headerNames">非表示にするカラム名一覧</param>
        /// <param name="cellNames">非表示にするセル名一覧</param>
        /// <param name="visibleFlag">各カラム、セルのVisibleに設定するbool</param>
        private void ChangePropertyForGC(string[] headerNames, string[] cellNames, string propertyName, bool visibleFlag)
        {
            this.form.mrwDetail.SuspendLayout();

            var newTemplate = this.form.mrwDetail.Template;

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

            this.form.mrwDetail.Template = newTemplate;

            this.form.mrwDetail.ResumeLayout();
        }

        /// <summary>
        /// Detail必須チェック
        /// Datailが一行以上入力されているかチェックする
        /// </summary>
        /// <returns>true: 一件以上入力されている, false: 一件も入力されていない</returns>
        internal bool CheckRequiredDataForDeital(out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart();

                catchErr = false;
                bool returnVal = false;
                foreach (var row in this.form.mrwDetail.Rows)
                {
                    if (row == null) continue;
                    if (row.IsNewRow) continue;

                    returnVal = true;
                    break;
                }

                LogUtility.DebugMethodEnd(returnVal, catchErr);
                return returnVal;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("CheckRequiredDataForDeital", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false, catchErr);
                return false;
            }
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

                var property = control.GetType().GetProperty("Enabled");

                if (property != null)
                {
                    property.SetValue(control, !isLock, null);
                }
            }

            // Detailのコントロールを制御
            foreach (Row row in this.form.mrwDetail.Rows)
            {
                foreach (var detaiControlName in inputDetailControlNames)
                {
                    row.Cells[detaiControlName].Enabled = !isLock;
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

            this.form.mrwDetail.SuspendLayout();
            var newTemplate = this.form.mrwDetail.Template;

            newTemplate.Width = 937;
            // 初期化
            // 確
            var kakuteiHeader = newTemplate.ColumnHeaders[0].Cells["clmHeaderKakuteiKbn"];
            kakuteiHeader.Size = new Size(25, 20);
            kakuteiHeader.Location = new Point(47, 0);
            var kakuteiCell = newTemplate.Row.Cells["KAKUTEI_KBN"];
            kakuteiCell.Size = new Size(25, 21);
            kakuteiCell.Location = new Point(47, 0);

            // 状況
            var joukyouHeader = newTemplate.ColumnHeaders[0].Cells["clmHeaderJoukyou"];
            joukyouHeader.Size = new Size(35, 20);
            joukyouHeader.Location = new Point(72, 0);
            var joukyouCell = newTemplate.Row.Cells["JOUKYOU"];
            joukyouCell.Size = new Size(35, 21);
            joukyouCell.Location = new Point(72, 0);

            // 売上/支払日
            var urShiHeader = newTemplate.ColumnHeaders[0].Cells["clmHeaderUrshDate"];
            urShiHeader.Size = new Size(82, 20);
            urShiHeader.Location = new Point(107, 0);
            var urshDateCell = newTemplate.Row.Cells["URIAGESHIHARAI_DATE"];
            urshDateCell.Size = new Size(82, 21);
            urshDateCell.Location = new Point(107, 0);

            // 品名CD
            var meisaiHinmeiCdHeader = newTemplate.ColumnHeaders[0].Cells["clmHeaderHinmeiCd"];
            meisaiHinmeiCdHeader.Size = new Size(51, 20);
            meisaiHinmeiCdHeader.Location = new Point(189, 0);
            var hinmeiCdCell = newTemplate.Row.Cells["HINMEI_CD"];
            hinmeiCdCell.Size = new Size(51, 21);
            hinmeiCdCell.Location = new Point(189, 0);

            // 品名
            var meisaiHinmeiHeader = newTemplate.ColumnHeaders[0].Cells["clmHeaderHinmeiName"];
            meisaiHinmeiHeader.Size = new Size(116, 20);
            meisaiHinmeiHeader.Location = new Point(240, 0);
            var hinmeiNameCell = newTemplate.Row.Cells["HINMEI_NAME"];
            hinmeiNameCell.Size = new Size(116, 21);
            hinmeiNameCell.Location = new Point(240, 0);

            // 伝票
            var meisaiDenpyoHeader = newTemplate.ColumnHeaders[0].Cells["clmHeaderDenpyouKbn"];
            meisaiDenpyoHeader.Size = new Size(59, 20);
            meisaiDenpyoHeader.Location = new Point(356, 0);
            var DenpyoKbnCdCell = newTemplate.Row.Cells["DENPYOU_KBN_CD"];
            DenpyoKbnCdCell.Size = new Size(1, 21);
            DenpyoKbnCdCell.Location = new Point(356, 0);
            var DenpyoKbnCell = newTemplate.Row.Cells["DENPYOU_KBN_NAME"];
            DenpyoKbnCell.Size = new Size(59, 21);
            DenpyoKbnCell.Location = new Point(356, 0);

            // 数量
            var meisaiSuryouHeader = newTemplate.ColumnHeaders[0].Cells["clmHeaderSuryou"];
            meisaiSuryouHeader.Size = new Size(80, 20);
            meisaiSuryouHeader.Location = new Point(415, 0);
            var SuryouCell = newTemplate.Row.Cells["SUURYOU"];
            SuryouCell.Size = new Size(80, 21);
            SuryouCell.Location = new Point(415, 0);

            // 単位
            var meisaiUnitHeader = newTemplate.ColumnHeaders[0].Cells["clmHeaderUnit"];
            meisaiUnitHeader.Size = new Size(66, 20);
            meisaiUnitHeader.Location = new Point(495, 0);
            var unitNameCell = newTemplate.Row.Cells["UNIT_NAME_RYAKU"];
            unitNameCell.Size = new Size(43, 21);
            unitNameCell.Location = new Point(518, 0);
            var unitCdCell = newTemplate.Row.Cells["UNIT_CD"];
            unitCdCell.Size = new Size(23, 21);
            unitCdCell.Location = new Point(495, 0);

            // 単価
            var meisaiTankaHeader = newTemplate.ColumnHeaders[0].Cells["clmHeaderTanka"];
            meisaiTankaHeader.Size = new Size(85, 20);
            meisaiTankaHeader.Location = new Point(561, 0);
            var tankaCell = newTemplate.Row.Cells["TANKA"];
            tankaCell.Size = new Size(85, 21);
            tankaCell.Location = new Point(561, 0);

            // 金額
            var meisaiKingakuHeader = newTemplate.ColumnHeaders[0].Cells["clmHeaderKingaku"];
            meisaiKingakuHeader.Size = new Size(80, 20);
            meisaiKingakuHeader.Location = new Point(646, 0);
            var kingakuCell = newTemplate.Row.Cells["KINGAKU"];
            kingakuCell.Size = new Size(80, 21);
            kingakuCell.Location = new Point(646, 0);

            // 明細備考
            var meisaiBikouHeader = newTemplate.ColumnHeaders[0].Cells["clmHeaderMeisaiBikou"];
            meisaiBikouHeader.Size = new Size(128, 20);
            meisaiBikouHeader.Location = new Point(726, 0);
            var meisaiBikouCell = newTemplate.Row.Cells["MEISAI_BIKOU"];
            meisaiBikouCell.Size = new Size(128, 21);
            meisaiBikouCell.Location = new Point(726, 0);

            var manifestIdHeader = newTemplate.ColumnHeaders[0].Cells["clmHeaderManifestId"];
            manifestIdHeader.Size = new Size(83, 20);
            manifestIdHeader.Location = new Point(854, 0);
            var manifestIdCell = newTemplate.Row.Cells["MANIFEST_ID"];
            manifestIdHeader.Size = new Size(83, 21);
            manifestIdHeader.Location = new Point(854, 0);

            // 位置調整
            if (!kakuteiHeader.Visible
                && !joukyouHeader.Visible
                && !urShiHeader.Visible)
            {
                //非表示になった項目分の横幅を求める。
                int iRemoveWidth = kakuteiHeader.Width + joukyouHeader.Width + urShiHeader.Width;
                // 品名CD
                meisaiHinmeiCdHeader.Location = new Point(meisaiHinmeiCdHeader.Location.X - iRemoveWidth, meisaiHinmeiCdHeader.Location.Y);
                hinmeiCdCell.Location = new Point(hinmeiCdCell.Location.X - iRemoveWidth, hinmeiCdCell.Location.Y);

                int iHalfOfRemoveWidth = iRemoveWidth / 2;
                // 品名
                meisaiHinmeiHeader.Location = new Point(meisaiHinmeiHeader.Location.X - iRemoveWidth, meisaiHinmeiHeader.Location.Y);
                meisaiHinmeiHeader.Size = new Size(meisaiHinmeiHeader.Width + iHalfOfRemoveWidth, meisaiHinmeiHeader.Height);
                hinmeiNameCell.Location = new Point(hinmeiNameCell.Location.X - iRemoveWidth, hinmeiNameCell.Location.Y);
                hinmeiNameCell.Size = new Size(hinmeiNameCell.Width + iHalfOfRemoveWidth, hinmeiNameCell.Height);

                //伝票
                meisaiDenpyoHeader.Location = new Point(meisaiDenpyoHeader.Location.X - iHalfOfRemoveWidth, meisaiDenpyoHeader.Location.Y);
                DenpyoKbnCdCell.Location = new Point(DenpyoKbnCdCell.Location.X - iHalfOfRemoveWidth, DenpyoKbnCdCell.Location.Y);
                DenpyoKbnCell.Location = new Point(DenpyoKbnCell.Location.X - iHalfOfRemoveWidth, DenpyoKbnCell.Location.Y);

                //数量
                meisaiSuryouHeader.Location = new Point(meisaiSuryouHeader.Location.X - iHalfOfRemoveWidth, meisaiSuryouHeader.Location.Y);
                SuryouCell.Location = new Point(SuryouCell.Location.X - iHalfOfRemoveWidth, SuryouCell.Location.Y);

                //// 単位
                meisaiUnitHeader.Location = new Point(meisaiUnitHeader.Location.X - iHalfOfRemoveWidth, meisaiUnitHeader.Location.Y);
                unitCdCell.Location = new Point(unitCdCell.Location.X - iHalfOfRemoveWidth, unitCdCell.Location.Y);
                unitNameCell.Location = new Point(unitNameCell.Location.X - iHalfOfRemoveWidth, unitNameCell.Location.Y);

                //// 単価
                meisaiTankaHeader.Location = new Point(meisaiTankaHeader.Location.X - iHalfOfRemoveWidth, meisaiTankaHeader.Location.Y);
                tankaCell.Location = new Point(tankaCell.Location.X - iHalfOfRemoveWidth, tankaCell.Location.Y);

                //// 金額
                meisaiKingakuHeader.Location = new Point(meisaiKingakuHeader.Location.X - iHalfOfRemoveWidth, meisaiKingakuHeader.Location.Y);
                kingakuCell.Location = new Point(kingakuCell.Location.X - iHalfOfRemoveWidth, kingakuCell.Location.Y);

                // 明細備考
                meisaiBikouHeader.Location = new Point(meisaiBikouHeader.Location.X - iHalfOfRemoveWidth, meisaiBikouHeader.Location.Y);
                meisaiBikouHeader.Size = new Size(meisaiBikouHeader.Width + iHalfOfRemoveWidth, meisaiBikouHeader.Height);
                meisaiBikouCell.Location = new Point(meisaiBikouCell.Location.X - iHalfOfRemoveWidth, meisaiBikouCell.Location.Y);
                meisaiBikouCell.Size = new Size(meisaiBikouCell.Width + iHalfOfRemoveWidth, meisaiBikouCell.Height);
            }

            if (manifestIdHeader.Visible == false)
            {
                newTemplate.Width -= manifestIdHeader.Width;
            }

            this.form.mrwDetail.Template = newTemplate;

            this.form.mrwDetail.ResumeLayout();

            LogUtility.DebugMethodEnd();
        }


        /// <summary>
        /// 指定した売上支払番号のデータが存在するか返す
        /// </summary>
        /// <param name="urShNumber">受入番号</param>
        /// <returns>true:存在する, false:存在しない</returns>
        internal bool IsExistUrShData(long urShNumber)
        {
            LogUtility.DebugMethodStart();

            bool returnVal = false;

            if (0 <= urShNumber)
            {
                var urShEntrys = this.accessor.GetUrshEntry(urShNumber, this.form.SEQ);
                if (urShEntrys != null
                    && 0 < urShEntrys.Length)
                {
                    returnVal = true;
                }
            }
            else if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                returnVal = true;
            }

            LogUtility.DebugMethodEnd();
            return returnVal;
        }

        /// <summary>
        /// MultiRowのデータに対しROW_NOを採番します
        /// </summary>
        public bool NumberingRowNo()
        {
            try
            {
                if (!this.form.notEditingOperationFlg)
                {
                    this.form.mrwDetail.BeginEdit(false);
                }

                foreach (Row dr in this.form.mrwDetail.Rows)
                {
                    dr.Cells[CELL_NAME_ROW_NO].Value = dr.Index + 1;
                }

                if (!this.form.notEditingOperationFlg)
                {
                    this.form.mrwDetail.EndEdit();
                    this.form.mrwDetail.NotifyCurrentCellDirty(false);
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("NumberingRowNo", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 明細金額計算
        /// </summary>
        internal bool CalcDetaiKingaku(Row targetRow)
        {
            /* 登録実行時に金額計算のチェック(CheckDetailKingakuメソッド)が実行されます。 　　         */
            /* チェックの計算方法は本メソッドと同じため、修正する際はチェック処理も修正してください。 */
            try
            {
                LogUtility.DebugMethodStart(targetRow);

                if (targetRow == null)
                {
                    return false;
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
                        short.TryParse(Convert.ToString(this.dto.torihikisakiSeikyuuEntity.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                    }
                    else if (targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString().Equals("2"))
                    {
                        short.TryParse(Convert.ToString(this.dto.torihikisakiShiharaiEntity.KINGAKU_HASUU_CD), out kingakuHasuuCd);
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

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcDetaiKingaku", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 消費税率を売上支払日から取得して設定(明細用)
        /// </summary>
        /// <param name="targetRow"></param>
        internal bool SetShouhizeiRateForDetail(Row targetRow)
        {
            try
            {
                LogUtility.DebugMethodStart(targetRow);
                if (targetRow == null)
                {
                    return false;
                }

                DateTime uriageShiharaiDate = this.footerForm.sysDate;
                if (targetRow[CELL_NAME_URIAGESHIHARAI_DATE].Value != null
                    && DateTime.TryParse(targetRow[CELL_NAME_URIAGESHIHARAI_DATE].Value.ToString(), out uriageShiharaiDate))
                {
                    var shouhizeiRate = this.accessor.GetShouhizeiRate(uriageShiharaiDate.Date);
                    if (shouhizeiRate != null && !shouhizeiRate.SHOUHIZEI_RATE.IsNull)
                    {
                        targetRow[CELL_NAME_SHOUHIZEI_RATE].Value = shouhizeiRate.SHOUHIZEI_RATE;
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetShouhizeiRateForDetail", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShouhizeiRateForDetail", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 伝票区分設定
        /// 明細の品名から伝票区分を設定する
        /// </summary>
        internal bool SetDenpyouKbn(out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart();

                catchErr = false;
                Row targetRow = this.form.mrwDetail.CurrentRow;

                if (targetRow == null)
                {
                    return true;
                }

                // 初期化
                targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value = string.Empty;
                targetRow.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = string.Empty;

                if (targetRow.Cells[CELL_NAME_HINMEI_CD].Value == null
                    || string.IsNullOrEmpty(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString()))
                {
                    return true;
                }

                var targetHimei = this.accessor.GetHinmeiDataByCd(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());

                if (targetHimei == null || string.IsNullOrEmpty(targetHimei.HINMEI_CD))
                {
                    // 存在しない品名が選択されている場合
                    return true;
                }

                switch (targetHimei.DENPYOU_KBN_CD.ToString())
                {
                    case SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE_STR:
                    case SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI_STR:
                        targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value = (short)targetHimei.DENPYOU_KBN_CD;
                        targetRow.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = denpyouKbnDictionary[(short)targetHimei.DENPYOU_KBN_CD].DENPYOU_KBN_NAME_RYAKU;
                        break;

                    default:
                        // ポップアップを打ち上げ、ユーザに選択してもらう
                        CellPosition pos = this.form.mrwDetail.CurrentCellPosition;
                        CustomControlExtLogic.PopUp((ICustomControl)this.form.mrwDetail.Rows[pos.RowIndex].Cells[CELL_NAME_DENPYOU_KBN_CD]);

                        var denpyouKbnCd = targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value;
                        if (denpyouKbnCd == null
                            || string.IsNullOrEmpty(denpyouKbnCd.ToString()))
                        {
                            // ポップアップでキャンセルが押された
                            // ※ポップアップで何を押されたか判断できないので、CDの存在チェックで対応
                            targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = string.Empty;
                            targetRow.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = string.Empty;

                            //ポップアップキャンセルフラグをTrueにする。
                            this.form.bCancelDenpyoPopup = true;

                            return false;
                        }

                        break;
                }

                LogUtility.DebugMethodEnd(true, catchErr);

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("SetDenpyouKbn", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false, catchErr);
                return false;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("SetDenpyouKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false, catchErr);
                return false;
            }
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
                    return false;
                }

                var value = PropertyUtility.GetTextOrValue(sender);
                if (!string.IsNullOrEmpty(value))
                {
                    PropertyUtility.SetTextOrValue(sender, FormatUtility.ToAmountValue(value));
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ToAmountValue", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
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
                return;
            }

            var value = PropertyUtility.GetTextOrValue(this.form.mrwDetail[e.RowIndex, e.CellIndex]);
            if (!string.IsNullOrEmpty(value))
            {
                this.form.mrwDetail.SetValue(e.RowIndex, e.CellIndex, FormatUtility.ToAmountValue(value));
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
        /// 伝票発行ポップアップ用連携オブジェクトを生成する
        /// </summary>
        /// <returns></returns>
        internal ParameterDTOClass createParameterDTOClass()
        {
            // 一度画面で選択されている場合を考慮し、formのParameterDTOClassで初期化
            ParameterDTOClass returnVal = this.form.denpyouHakouPopUpDTO;

            // 新規、修正共通で設定
            var uriageDate = this.getUriageDateForDenpyouHakou();
            var shiharaiDate = this.getShiharaiDateForDenpyouHakou();
            if (uriageDate != null)
            {
                returnVal.Uriage_Date = (uriageDate.Value.Date).ToString();
            }
            if (shiharaiDate != null)
            {
                returnVal.Shiharai_Date = (shiharaiDate.Value.Date).ToString();
            }
            returnVal.Torihikisaki_Cd = this.form.TORIHIKISAKI_CD.Text.ToString();
            returnVal.Gyousha_Cd = this.form.GYOUSHA_CD.Text.ToString();
            returnVal.Uriage_Amount_Total = this.form.URIAGE_AMOUNT_TOTAL.Text.ToString();
            returnVal.Shiharai_Amount_Total = this.form.SHIHARAI_KINGAKU_TOTAL.Text.ToString();
            List<MeiseiDTOClass> meisaiDtoList = this.createMeisaiDtoList();
            returnVal.Tenpyo_Cnt = meisaiDtoList;

            // 滞留区分フラグを初期化
            this.form.denpyouHakouPopUpDTO.Tairyuu_Kbn = false;

            this.form.denpyouHakouPopUpDTO.Kakute_Kbn = this.form.KAKUTEI_KBN.Text.Replace("0", "");

            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    // 新規作成
                    break;

                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    // DBの情報を復元
                    this.form.denpyouHakouPopUpDTO.System_Id = this.dto.entryEntity.SYSTEM_ID.ToString();
                    break;

                default:
                    break;
            }

            // 消費税率をセット
            if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_DENPYOU)
            {
                if (!string.IsNullOrEmpty(this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text))
                {
                    this.form.denpyouHakouPopUpDTO.Uriage_Shouhizei_Rate = this.ToDecimalForUriageShouhizeiRate().ToString();
                }
                if (!string.IsNullOrEmpty(this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text))
                {
                    this.form.denpyouHakouPopUpDTO.Shiharai_Shouhizei_Rate = this.ToDecimalForShiharaiShouhizeiRate().ToString();
                }
            }
            else
            {
                // 明細単位
                SqlDateTime tempUriageDate = SqlDateTime.Null;
                SqlDateTime tempShiharaiDate = SqlDateTime.Null;
                foreach (Row row in this.form.mrwDetail.Rows)
                {
                    if (row.IsNewRow || string.IsNullOrEmpty((string)row.Cells["ROW_NO"].Value.ToString()))
                    {
                        continue;
                    }

                    DateTime tempUrShDate;
                    if (row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value != null
                        && DateTime.TryParse(row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value.ToString(), out tempUrShDate)
                        && (row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value != null
                            && !string.IsNullOrEmpty(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString())))
                    {
                        if (SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE_STR.Equals(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString()))
                        {
                            if (tempUriageDate.IsNull)
                            {
                                tempUriageDate = tempUrShDate.Date;
                            }
                            // 一番最後の日付かチェック
                            else if (tempUriageDate < tempUrShDate.Date)
                            {
                                tempUriageDate = tempUrShDate.Date;
                            }
                        }
                        else if (SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI_STR.Equals(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString()))
                        {
                            if (tempShiharaiDate.IsNull)
                            {
                                tempShiharaiDate = tempUrShDate.Date;
                            }
                            // 一番最後の日付かチェック
                            else if (tempShiharaiDate < tempUrShDate.Date)
                            {
                                tempShiharaiDate = tempUrShDate.Date;
                            }
                        }
                    }
                }

                // 消費税セット
                if (!tempUriageDate.IsNull)
                {
                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(((DateTime)tempUriageDate).Date);
                    if (shouhizeiEntity != null
                        && !shouhizeiEntity.SHOUHIZEI_RATE.IsNull)
                    {
                        this.form.denpyouHakouPopUpDTO.Uriage_Shouhizei_Rate = shouhizeiEntity.SHOUHIZEI_RATE.ToString();
                    }
                }
                if (!tempShiharaiDate.IsNull)
                {
                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(((DateTime)tempShiharaiDate).Date);
                    if (shouhizeiEntity != null
                        && !shouhizeiEntity.SHOUHIZEI_RATE.IsNull)
                    {
                        this.form.denpyouHakouPopUpDTO.Shiharai_Shouhizei_Rate = shouhizeiEntity.SHOUHIZEI_RATE.ToString();
                    }
                }
            }

            #region 月次処理 - 月次ロック用

            returnVal.DenpyouDate = string.Empty;
            returnVal.BeforeDenpyouDate = string.Empty;

            // 伝票日付
            returnVal.DenpyouDate = this.form.DENPYOU_DATE.Value.ToString();

            if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                // 画面表示時の伝票日付
                returnVal.BeforeDenpyouDate = this.beforDto.entryEntity.DENPYOU_DATE.ToString();
            }

            #endregion


            return returnVal;
        }

        /// <summary>
        /// 売上日付取得(伝票発行ポップアップ用)
        /// 明細毎に日付が設定される場合、明細行の中でもっとも古い日付を取得する
        /// </summary>
        /// <returns>取得できない場合はnullを返す</returns>
        private DateTime? getUriageDateForDenpyouHakou()
        {
            // Detailの日付をチェック
            if (this.dto.sysInfoEntity.UKEIRE_KAKUTEI_USE_KBN == SalesPaymentConstans.UKEIRE_KAKUTEI_USE_KBN_YES)
            {
                if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_MEISAI)
                {
                    // もっとも古い日付を検索
                    DateTime? targetDateTime = null;
                    foreach (Row row in this.form.mrwDetail.Rows)
                    {
                        if (row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value == null)
                        {
                            continue;
                        }
                        else if (!SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE_STR.Equals(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString()))
                        {
                            continue;
                        }

                        if (targetDateTime == null)
                        {
                            if (row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value != null
                                && !string.IsNullOrEmpty(row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value.ToString())
                                && row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Visible)
                            {
                                targetDateTime = (DateTime)row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value;
                                continue;
                            }
                        }
                        else
                        {
                            // 比較
                            if (row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value != null
                                && !string.IsNullOrEmpty(row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value.ToString())
                                && row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Visible)
                            {
                                var tempDateTime = (DateTime)row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value;
                                if (tempDateTime < targetDateTime)
                                {
                                    // 日付の古いほうをセット
                                    targetDateTime = tempDateTime;
                                }
                            }
                        }
                    }

                    return targetDateTime;
                }
            }

            // Entryの日付をチェック
            if (this.form.URIAGE_DATE.Value != null
                && this.form.URIAGE_DATE.Visible)
            {
                return (DateTime)this.form.URIAGE_DATE.Value;
            }
            return null;
        }

        /// <summary>
        /// 支払日付取得(伝票発行ポップアップ用)
        /// 明細毎に日付が設定される場合、明細行の中でもっとも古い日付を取得する
        /// </summary>
        /// <returns>取得できない場合はnullを返す</returns>
        private DateTime? getShiharaiDateForDenpyouHakou()
        {
            // Detailの日付をチェック
            if (this.dto.sysInfoEntity.UKEIRE_KAKUTEI_USE_KBN == SalesPaymentConstans.UKEIRE_KAKUTEI_USE_KBN_YES)
            {
                if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_MEISAI)
                {
                    // もっとも古い日付を検索
                    DateTime? targetDateTime = null;
                    foreach (Row row in this.form.mrwDetail.Rows)
                    {
                        if (row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value == null)
                        {
                            continue;
                        }
                        else if (!SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI_STR.Equals(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value.ToString()))
                        {
                            continue;
                        }

                        if (targetDateTime == null)
                        {
                            if (row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value != null
                                && !string.IsNullOrEmpty(row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value.ToString())
                                && row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Visible)
                            {
                                targetDateTime = (DateTime)row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value;
                                continue;
                            }
                        }
                        else
                        {
                            // 比較
                            if (row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value != null
                                && !string.IsNullOrEmpty(row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value.ToString())
                                && row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Visible)
                            {
                                var tempDateTime = (DateTime)row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value;
                                if (tempDateTime < targetDateTime)
                                {
                                    // 日付の古いほうをセット
                                    targetDateTime = tempDateTime;
                                }
                            }
                        }
                    }

                    return targetDateTime;
                }
            }

            // Entryの日付をチェック
            if (this.form.SHIHARAI_DATE.Value != null
                && this.form.SHIHARAI_DATE.Visible)
            {
                return (DateTime)this.form.SHIHARAI_DATE.Value;
            }
            return null;
        }

        /// <summary>
        /// 伝票明細リスト生成処理
        /// </summary>
        /// <returns></returns>
        private List<MeiseiDTOClass> createMeisaiDtoList()
        {
            List<MeiseiDTOClass> returnVal = new List<MeiseiDTOClass>();
            foreach (Row row in this.form.mrwDetail.Rows)
            {
                if (row.IsNewRow != true)
                {
                    MeiseiDTOClass meisaiDtoClass = new MeiseiDTOClass();
                    // 確定区分
                    if (this.dto.sysInfoEntity.UKEIRE_KAKUTEI_USE_KBN == SalesPaymentConstans.UKEIRE_KAKUTEI_USE_KBN_YES)
                    {
                        if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_DENPYOU)
                        {
                            meisaiDtoClass.Kakutei_Kbn = this.form.KAKUTEI_KBN.Text;
                        }
                        else if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_DENPYOU)
                        {
                            meisaiDtoClass.Kakutei_Kbn = Convert.ToString(row.Cells[CELL_NAME_KAKUTEI_KBN].Value);
                        }
                    }
                    meisaiDtoClass.Uriageshiharai_Kbn = Convert.ToString(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value);
                    meisaiDtoClass.Hinmei_Cd = Convert.ToString(row.Cells[CELL_NAME_HINMEI_CD].Value);
                    meisaiDtoClass.Kingaku = Convert.ToString(row.Cells[CELL_NAME_KINGAKU].Value);
                    int temp;
                    if (int.TryParse(Convert.ToString(row.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value), out temp))
                    {
                        meisaiDtoClass.ZeiKbn = Convert.ToString(row.Cells[CELL_NAME_HINMEI_ZEI_KBN_CD].Value);
                    }
                    else
                    {
                        meisaiDtoClass.ZeiKbn = string.Empty;
                    }
                    returnVal.Add(meisaiDtoClass);
                }
            }
            return returnVal;
        }

        /// <summary>
        /// 項目クリア処理
        /// </summary>
        /// <returns></returns>
        internal void Clear()
        {
            // ヘッダー Start
            // 拠点
            headerForm.KYOTEN_CD.Text = string.Empty;
            headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
            const string KYOTEN_CD = "拠点CD";
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            this.headerForm.KYOTEN_CD.Text = this.GetUserProfileValue(userProfile, KYOTEN_CD);
            if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text.ToString()))
            {
                this.headerForm.KYOTEN_CD.Text = this.headerForm.KYOTEN_CD.Text.ToString().PadLeft(this.headerForm.KYOTEN_CD.MaxLength, '0');
                bool catchErr = CheckKyotenCd();
                if (catchErr)
                {
                    throw new Exception("");
                }
            }
            // ヘッダー End

            // 詳細 Start
            this.form.ENTRY_NUMBER.Text = string.Empty;
            // 連番
            this.form.RENBAN.Text = string.Empty;
            // 確定区分は1（確定）
            this.form.KAKUTEI_KBN.Text = SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI.ToString();
            this.form.KAKUTEI_KBN_NAME.Text = SalesPaymentConstans.GetKakuteiKbnName(SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI);
            // 受付番号
            this.form.UKETSUKE_NUMBER.Text = string.Empty;
            this.ClearTUketsukeSsEntry();
            this.ClearTUketsukeSkEntry();

            if (!this.form.isRegisted)
            {
                // 継続入力以外のときだけ初期化
                // 売上日付
                this.form.URIAGE_DATE.Value = this.footerForm.sysDate;
                // 支払日付
                this.form.SHIHARAI_DATE.Value = this.footerForm.sysDate;
            }

            // 消費税率
            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text = string.Empty;
            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text = string.Empty;

            // 入力担当者
            if (CommonShogunData.LOGIN_USER_INFO != null
                && !string.IsNullOrEmpty(CommonShogunData.LOGIN_USER_INFO.SHAIN_CD)
                && CommonShogunData.LOGIN_USER_INFO.NYUURYOKU_TANTOU_KBN)
            {
                this.form.NYUURYOKU_TANTOUSHA_CD.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_CD.ToString();
                this.form.NYUURYOKU_TANTOUSHA_NAME.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME_RYAKU.ToString();
                strNyuryokuTantousyaName = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME.ToString();  // No.3279
            }
            else
            {
                this.form.NYUURYOKU_TANTOUSHA_CD.Text = string.Empty;
                this.form.NYUURYOKU_TANTOUSHA_NAME.Text = string.Empty;
                strNyuryokuTantousyaName = string.Empty;  // No.3279
                this.form.NYUURYOKU_TANTOUSHA_NAME.ReadOnly = true;
            }

            // 車輌
            this.form.SHARYOU_CD.Text = string.Empty;
            this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
            this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
            this.form.SHARYOU_NAME_RYAKU.TabStop = false;
            this.form.SHARYOU_NAME_RYAKU.Tag = string.Empty;

            // 取引先
            this.form.TORIHIKISAKI_CD.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
            this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
            this.form.TORIHIKISAKI_NAME_RYAKU.Tag = string.Empty;

            // 車種
            this.form.SHASHU_CD.Text = string.Empty;
            this.form.SHASHU_NAME.Text = string.Empty;
            this.form.SHASHU_NAME.TabStop = false;
            this.form.SHASHU_NAME.ReadOnly = true;

            // 売上締日
            this.form.SEIKYUU_SHIMEBI1.Text = string.Empty;
            this.form.SEIKYUU_SHIMEBI2.Text = string.Empty;
            this.form.SEIKYUU_SHIMEBI3.Text = string.Empty;
            // 支払締日
            this.form.SHIHARAI_SHIMEBI1.Text = string.Empty;
            this.form.SHIHARAI_SHIMEBI2.Text = string.Empty;
            this.form.SHIHARAI_SHIMEBI3.Text = string.Empty;
            // 運搬業者
            this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
            this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
            this.form.UNPAN_GYOUSHA_NAME.ReadOnly = true;
            this.form.UNPAN_GYOUSHA_NAME.Tag = string.Empty;
            // 業者
            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.ReadOnly = true;
            this.form.GYOUSHA_NAME_RYAKU.Tag = string.Empty;
            // 運転者
            this.form.UNTENSHA_CD.Text = string.Empty;
            this.form.UNTENSHA_NAME.Text = string.Empty;
            this.form.UNTENSHA_NAME.ReadOnly = true;
            // 人数
            this.form.NINZUU_CNT.Text = string.Empty;
            // 現場
            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_NAME_RYAKU.Text = string.Empty;
            this.form.GENBA_NAME_RYAKU.ReadOnly = true;
            this.form.GENBA_NAME_RYAKU.Tag = string.Empty;
            // 形態区分
            this.form.KEITAI_KBN_CD.Text = string.Empty;
            this.form.KEITAI_KBN_NAME_RYAKU.Text = string.Empty;
            // コンテナ
            this.form.CONTENA_SOUSA_CD.Text = string.Empty;
            this.form.CONTENA_JOUKYOU_NAME_RYAKU.Text = string.Empty;
            // 荷積業者
            this.form.NIZUMI_GYOUSHA_CD.Text = string.Empty;
            this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
            this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = true;
            this.form.NIZUMI_GYOUSHA_NAME.TabStop = false;
            this.form.NIZUMI_GYOUSHA_NAME.Tag = string.Empty;
            // 荷積現場
            this.form.NIZUMI_GENBA_CD.Text = string.Empty;
            this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
            this.form.NIZUMI_GENBA_NAME.ReadOnly = true;
            this.form.NIZUMI_GENBA_NAME.TabStop = false;
            this.form.NIZUMI_GENBA_NAME.Tag = string.Empty;
            // 荷降業者
            this.form.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
            this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
            this.form.NIOROSHI_GYOUSHA_NAME.ReadOnly = true;
            this.form.NIOROSHI_GYOUSHA_NAME.TabStop = false;
            this.form.NIOROSHI_GYOUSHA_NAME.Tag = string.Empty;
            // 荷降現場
            this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
            this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
            this.form.NIOROSHI_GENBA_NAME.ReadOnly = true;
            this.form.NIOROSHI_GENBA_NAME.Tag = string.Empty;

            // No.3815-->
            if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType))
            {   // 新規の場合
                // 荷積業者
                const string NIZUMI_GYOUSHA_CD = "荷積業者CD";
                this.form.NIZUMI_GYOUSHA_CD.Text = this.GetUserProfileValue(userProfile, NIZUMI_GYOUSHA_CD);
                if (!string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text.ToString()))
                {
                    this.form.NIZUMI_GYOUSHA_CD.Text = this.form.NIZUMI_GYOUSHA_CD.Text.ToString().PadLeft(this.form.NIZUMI_GYOUSHA_CD.MaxLength, '0');
                    CheckNizumiGyoushaCd();
                }
                // 荷積現場
                const string NIZUMI_GENBA_CD = "荷積現場CD";
                this.form.NIZUMI_GENBA_CD.Text = this.GetUserProfileValue(userProfile, NIZUMI_GENBA_CD);
                if (!string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text.ToString()))
                {
                    this.form.NIZUMI_GENBA_CD.Text = this.form.NIZUMI_GENBA_CD.Text.ToString().PadLeft(this.form.NIZUMI_GENBA_CD.MaxLength, '0');
                    CheckNizumiGenbaCd();
                }
                // 荷降業者
                const string NIOROSHI_GYOUSHA_CD = "荷降業者CD";
                this.form.NIOROSHI_GYOUSHA_CD.Text = this.GetUserProfileValue(userProfile, NIOROSHI_GYOUSHA_CD);
                if (!string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text.ToString()))
                {
                    this.form.NIOROSHI_GYOUSHA_CD.Text = this.form.NIOROSHI_GYOUSHA_CD.Text.ToString().PadLeft(this.form.NIOROSHI_GYOUSHA_CD.MaxLength, '0');
                    CheckNioroshiGyoushaCd();
                }
                // 荷降現場
                const string NIOROSHI_GENBA_CD = "荷降現場CD";
                this.form.NIOROSHI_GENBA_CD.Text = this.GetUserProfileValue(userProfile, NIOROSHI_GENBA_CD);
                if (!string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text.ToString()))
                {
                    this.form.NIOROSHI_GENBA_CD.Text = this.form.NIOROSHI_GENBA_CD.Text.ToString().PadLeft(this.form.NIOROSHI_GENBA_CD.MaxLength, '0');
                    CheckNioroshiGenbaCd();
                }
            }
            // No.3815<--

            // マニフェスト種類
            this.form.MANIFEST_SHURUI_CD.Text = string.Empty;
            this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = string.Empty;
            // マニフェスト手配
            this.form.MANIFEST_TEHAI_CD.Text = string.Empty;
            this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = string.Empty;
            // 営業担当者
            this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
            this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
            this.form.EIGYOU_TANTOUSHA_NAME.ReadOnly = true;
            //PhuocLoc 2020/12/01 #136221 -Start
            // 集計項目者
            this.form.SHUUKEI_KOUMOKU_CD.Text = string.Empty;
            this.form.SHUUKEI_KOUMOKU_NAME.Text = string.Empty;
            this.form.SHUUKEI_KOUMOKU_NAME.ReadOnly = true;
            //PhuocLoc 2020/12/01 #136221 -End
            // 伝票備考
            this.form.DENPYOU_BIKOU.Text = string.Empty;
            // 締処理状況(売上)
            this.form.SHIMESHORI_JOUKYOU_URIAGE.Text = string.Empty;
            // 締処理状況(支払)
            this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Text = string.Empty;
            // 領収書番号
            this.form.RECEIPT_NUMBER.Text = string.Empty;
            this.form.RECEIPT_NUMBER_DAY.Text = string.Empty;
            this.form.RECEIPT_NUMBER_YEAR.Text = string.Empty;
            // 売上金額合計
            this.form.URIAGE_AMOUNT_TOTAL.Text = string.Empty;
            // 支払金額合計
            this.form.SHIHARAI_KINGAKU_TOTAL.Text = string.Empty;
            // 差額
            this.form.SAGAKU.Text = string.Empty;
            //2次
            //取引区分（売）
            this.form.txtUri.Text = string.Empty;
            //取引区分（支）
            this.form.txtShi.Text = string.Empty;
            //コンテナ設置
            this.form.txtSecchi.Text = string.Empty;
            //コンテナ引揚
            this.form.txtHikiage.Text = string.Empty;
            // 詳細 End

            // 明細 Start
            // テンプレートをいじる処理は、データ設定前に実行
            this.ExecuteAlignmentForDetail();
            this.form.mrwDetail.BeginEdit(false);
            this.form.mrwDetail.Rows.Clear();
            this.form.mrwDetail.EndEdit();
            this.form.mrwDetail.NotifyCurrentCellDirty(false);
        }

        /// <summary>
        /// 取引区分チェック
        /// </summary>
        internal void CheckTorihikiKBN()
        {
            LogUtility.DebugMethodStart();

            string seikyuuKBN;
            string shiharaiKBN;

            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    seikyuuKBN = this.accessor.GetTrihikisakiKBN_Seikyuu(this.form.TORIHIKISAKI_CD.Text);
                    if (seikyuuKBN == "1")
                    {
                        //1.現金
                        this.form.txtUri.Text = "現金";
                    }
                    else if (seikyuuKBN == "2")
                    {
                        //2.掛け
                        this.form.txtUri.Text = "掛け";
                    }
                    else
                    {
                        this.form.txtUri.Text = "";
                    }

                    shiharaiKBN = this.accessor.GetTrihikisakiKBN_Shiharai(this.form.TORIHIKISAKI_CD.Text);
                    if (shiharaiKBN == "1")
                    {
                        //1.現金
                        this.form.txtShi.Text = "現金";
                    }
                    else if (shiharaiKBN == "2")
                    {
                        //2.掛け
                        this.form.txtShi.Text = "掛け";
                    }
                    else
                    {
                        this.form.txtShi.Text = "";
                    }
                    break;

                default:
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 受付番号からデータ取得
        /// </summary>
        internal bool GetUketsukeNumber()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.UKETSUKE_NUMBER.Text))
                {
                    return false;
                }

                // 受付（収集）からデータ取得
                DataTable dt = this.accessor.GetUketsukeSS(this.form.UKETSUKE_NUMBER.Text);
                if (dt.Rows.Count == 0)
                {
                    // データがなかったら受付（出荷）からデータ取得
                    dt = this.accessor.GetUketsukeSK(this.form.UKETSUKE_NUMBER.Text);
                    if (dt.Rows.Count == 0)
                    {
                        dt = this.accessor.GetUketsukeMK(this.form.UKETSUKE_NUMBER.Text);
                        //Check 受付（持込）データの予約状況 refs #158909 start
                        if (dt != null && dt.Rows.Count != 0)
                        {
                            var yoyakuJokyoCd = dt.Rows[0]["YOYAKU_JOKYO_CD"].ToString();
                            if (!SalesPaymentConstans.YOYAKU_JOKYO_CD_YOYAKU_KANRYOU.Equals(yoyakuJokyoCd))
                            {
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShow("E101", "伝票", "売上");
                                this.form.UKETSUKE_NUMBER.Text = "";
                                this.form.UKETSUKE_NUMBER.Focus();
                                return true;
                            }
                            // 登録時に予約状況を変更するためにエンティティを保存
                            var systemId = dt.Rows[0]["SYSTEM_ID"].ToString();
                            var seq = dt.Rows[0]["SEQ"].ToString();
                            this.tUketsukeMkEntry = this.accessor.GetUketsukeMkEntry(systemId, seq);
                        }
                        //Check 受付（持込）データの予約状況 end
                        if (dt.Rows.Count == 0)
                        {
                            // データなし

                            // メッセージ表示
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E045");

                            // 入力受付番号クリア
                            this.form.UKETSUKE_NUMBER.Text = ""; // No.3163

                            //フォーカスを受付番号にする
                            this.form.UKETSUKE_NUMBER.Focus();

                            // 処理終了
                            return false;
                        }
                    }
                    else if (!isOpenedScreenHaishaJokyoCd(dt))
                    {
                        showErrorDenpyou();

                        return false;
                    }
                    else
                    {
                        // 登録時に配車状況を変更するために出荷受付入力エンティティを保存
                        var systemId = dt.Rows[0]["SYSTEM_ID"].ToString();
                        var seq = dt.Rows[0]["SEQ"].ToString();
                        this.tUketsukeSkEntry = this.accessor.GetUketsukeSkEntry(systemId, seq);
                    }
                }
                else if (!isOpenedScreenHaishaJokyoCd(dt))
                {
                    showErrorDenpyou();

                    return false;
                }
                else
                {
                    // 登録時に配車状況を変更するために収集受付入力エンティティを保存
                    var systemId = dt.Rows[0]["SYSTEM_ID"].ToString();
                    var seq = dt.Rows[0]["SEQ"].ToString();
                    this.tUketsukeSsEntry = this.accessor.GetUketsukeSsEntry(systemId, seq);
                }
                //伝票日付、売上日付、支払日付
                if (!string.IsNullOrEmpty(dt.Rows[0]["SAGYOU_DATE"].ToString()))
                {
                    this.form.DENPYOU_DATE.Text = dt.Rows[0]["SAGYOU_DATE"].ToString();
                    this.form.URIAGE_DATE.Text = dt.Rows[0]["SAGYOU_DATE"].ToString();
                    this.form.SHIHARAI_DATE.Text = dt.Rows[0]["SAGYOU_DATE"].ToString();

                    // 消費税率の設定
                    DateTime uriageDate = this.footerForm.sysDate.Date;
                    if (DateTime.TryParse(this.form.URIAGE_DATE.Text, out uriageDate))
                    {
                        var shouhizeiRate = this.accessor.GetShouhizeiRate(uriageDate);
                        if (!shouhizeiRate.SHOUHIZEI_RATE.IsNull)
                        {
                            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text = shouhizeiRate.SHOUHIZEI_RATE.ToString();
                        }
                    }

                    DateTime shiharaiDate = this.footerForm.sysDate.Date;
                    if (DateTime.TryParse(this.form.SHIHARAI_DATE.Text, out shiharaiDate))
                    {
                        var shiharaiShouhizeiRate = this.accessor.GetShouhizeiRate(shiharaiDate);
                        if (!shiharaiShouhizeiRate.SHOUHIZEI_RATE.IsNull)
                        {
                            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text = shiharaiShouhizeiRate.SHOUHIZEI_RATE.ToString();
                        }
                    }
                }
                //取引先CD
                if (string.IsNullOrEmpty(dt.Rows[0]["TORIHIKISAKI_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["TORIHIKISAKI_NAME_RYAKU"].ToString()))
                {
                    this.form.TORIHIKISAKI_CD.Text = string.Empty;
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                }
                else
                {
                    this.form.TORIHIKISAKI_CD.Text = dt.Rows[0]["TORIHIKISAKI_CD"].ToString();
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = dt.Rows[0]["TORIHIKISAKI_NAME_RYAKU"].ToString();
                }
                //業者CD
                if (string.IsNullOrEmpty(dt.Rows[0]["GYOUSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString()))
                {
                    this.form.GYOUSHA_CD.Text = string.Empty;
                    this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                }
                else
                {
                    this.form.GYOUSHA_CD.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                    this.form.GYOUSHA_NAME_RYAKU.Text = dt.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString();
                }
                //現場CD
                if (string.IsNullOrEmpty(dt.Rows[0]["GENBA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["GENBA_NAME_RYAKU"].ToString()))
                {
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                }
                else
                {
                    this.form.GENBA_CD.Text = dt.Rows[0]["GENBA_CD"].ToString();
                    this.form.GENBA_NAME_RYAKU.Text = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();
                }
                //運搬業者CD
                if (string.IsNullOrEmpty(dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["UNPAN_GYOUSHA_NAME_RYAKU"].ToString()))
                {
                    this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
                    this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                }
                else
                {
                    this.form.UNPAN_GYOUSHA_CD.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                    this.form.UNPAN_GYOUSHA_NAME.Text = dt.Rows[0]["UNPAN_GYOUSHA_NAME_RYAKU"].ToString();
                }
                //荷積業者CD
                if (string.IsNullOrEmpty(dt.Rows[0]["NIZUMI_GYOUSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["NIZUMI_GYOUSHA_NAME_RYAKU"].ToString()))
                {
                    this.form.NIZUMI_GYOUSHA_CD.Text = string.Empty;
                    this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
                }
                else
                {
                    this.form.NIZUMI_GYOUSHA_CD.Text = dt.Rows[0]["NIZUMI_GYOUSHA_CD"].ToString();
                    this.form.NIZUMI_GYOUSHA_NAME.Text = dt.Rows[0]["NIZUMI_GYOUSHA_NAME_RYAKU"].ToString();
                }
                //荷積現場CD
                if (string.IsNullOrEmpty(dt.Rows[0]["NIZUMI_GENBA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["NIZUMI_GENBA_NAME_RYAKU"].ToString()))
                {
                    this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                    this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                }
                else
                {
                    this.form.NIZUMI_GENBA_CD.Text = dt.Rows[0]["NIZUMI_GENBA_CD"].ToString();
                    this.form.NIZUMI_GENBA_NAME.Text = dt.Rows[0]["NIZUMI_GENBA_NAME_RYAKU"].ToString();
                }
                //荷降業者CD
                if (string.IsNullOrEmpty(dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["NIOROSHI_GYOUSHA_NAME_RYAKU"].ToString()))
                {
                    this.form.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
                    this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                }
                else
                {
                    this.form.NIOROSHI_GYOUSHA_CD.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                    this.form.NIOROSHI_GYOUSHA_NAME.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_NAME_RYAKU"].ToString();
                }
                //荷降現場CD
                if (string.IsNullOrEmpty(dt.Rows[0]["NIOROSHI_GENBA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["NIOROSHI_GENBA_NAME_RYAKU"].ToString()))
                {
                    this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                    this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                }
                else
                {
                    this.form.NIOROSHI_GENBA_CD.Text = dt.Rows[0]["NIOROSHI_GENBA_CD"].ToString();
                    this.form.NIOROSHI_GENBA_NAME.Text = dt.Rows[0]["NIOROSHI_GENBA_NAME_RYAKU"].ToString();
                }
                //営業担当者CD
                if (string.IsNullOrEmpty(dt.Rows[0]["EIGYOU_TANTOUSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["EIGYOU_TANTOUSHA_NAME_RYAKU"].ToString()))
                {
                    this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
                    this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
                }
                else
                {
                    this.form.EIGYOU_TANTOUSHA_CD.Text = dt.Rows[0]["EIGYOU_TANTOUSHA_CD"].ToString();
                    this.form.EIGYOU_TANTOUSHA_NAME.Text = dt.Rows[0]["EIGYOU_TANTOUSHA_NAME_RYAKU"].ToString();
                }
                this.SetShuukeiKomoku(this.form.GENBA_CD.Text, this.form.GYOUSHA_CD.Text, this.form.TORIHIKISAKI_CD.Text); //PhuocLoc 2020/12/01 #136221
                //車輌CD
                if (string.IsNullOrEmpty(dt.Rows[0]["SHARYOU_CD"].ToString()))
                {
                    //コードが無しの場合は未入力状態
                    this.form.SHARYOU_CD.Text = "";
                    this.form.SHARYOU_NAME_RYAKU.Text = "";
                }
                else if (string.IsNullOrEmpty(dt.Rows[0]["SHARYOU_NAME_RYAKU"].ToString()))
                {
                    //コードありで名称無しの場合は車輌名入力可能状態
                    this.form.SHARYOU_CD.Text = dt.Rows[0]["SHARYOU_CD"].ToString();
                    this.form.SHARYOU_NAME_RYAKU.Text = "";
                    this.CheckSharyouUketsuke_NASHI(dt);
                }
                else
                {
                    this.form.SHARYOU_CD.Text = dt.Rows[0]["SHARYOU_CD"].ToString();
                    this.form.SHARYOU_NAME_RYAKU.Text = dt.Rows[0]["SHARYOU_NAME_RYAKU"].ToString();
                }
                //車種CD
                if (string.IsNullOrEmpty(dt.Rows[0]["SHASHU_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["SHASHU_NAME_RYAKU"].ToString()))
                {
                    this.form.SHASHU_CD.Text = string.Empty;
                    this.form.SHASHU_NAME.Text = string.Empty;
                }
                else
                {
                    this.form.SHASHU_CD.Text = dt.Rows[0]["SHASHU_CD"].ToString();
                    this.form.SHASHU_NAME.Text = dt.Rows[0]["SHASHU_NAME_RYAKU"].ToString();
                }
                //運転者CD
                if (string.IsNullOrEmpty(dt.Rows[0]["UNTENSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["UNTENSHA_NAME_RYAKU"].ToString()))
                {
                    this.form.UNTENSHA_CD.Text = string.Empty;
                    this.form.UNTENSHA_NAME.Text = string.Empty;
                }
                else
                {
                    this.form.UNTENSHA_CD.Text = dt.Rows[0]["UNTENSHA_CD"].ToString();
                    this.form.UNTENSHA_NAME.Text = dt.Rows[0]["UNTENSHA_NAME_RYAKU"].ToString();
                }
                //マニ種類CD
                if (string.IsNullOrEmpty(dt.Rows[0]["MANIFEST_SHURUI_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["MANIFEST_SHURUI_NAME_RYAKU"].ToString()))
                {
                    this.form.MANIFEST_SHURUI_CD.Text = string.Empty;
                    this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = string.Empty;
                }
                else
                {
                    this.form.MANIFEST_SHURUI_CD.Text = dt.Rows[0]["MANIFEST_SHURUI_CD"].ToString();
                    this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = dt.Rows[0]["MANIFEST_SHURUI_NAME_RYAKU"].ToString();
                }
                //マニ手配CD
                if (string.IsNullOrEmpty(dt.Rows[0]["MANIFEST_TEHAI_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["MANIFEST_TEHAI_NAME_RYAKU"].ToString()))
                {
                    this.form.MANIFEST_TEHAI_CD.Text = string.Empty;
                    this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = string.Empty;
                }
                else
                {
                    this.form.MANIFEST_TEHAI_CD.Text = dt.Rows[0]["MANIFEST_TEHAI_CD"].ToString();
                    this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = dt.Rows[0]["MANIFEST_TEHAI_NAME_RYAKU"].ToString();
                }
                //コンテナCD
                if (string.IsNullOrEmpty(dt.Rows[0]["CONTENA_SOUSA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["CONTENA_JOUKYOU_NAME_RYAKU"].ToString()))
                {
                    this.form.CONTENA_SOUSA_CD.Text = string.Empty;
                    this.form.CONTENA_JOUKYOU_NAME_RYAKU.Text = string.Empty;
                }
                else
                {
                    this.form.CONTENA_SOUSA_CD.Text = dt.Rows[0]["CONTENA_SOUSA_CD"].ToString();
                    this.form.CONTENA_JOUKYOU_NAME_RYAKU.Text = dt.Rows[0]["CONTENA_JOUKYOU_NAME_RYAKU"].ToString();
                }

                //設置台数・引揚台数
                var contenaReserveEntity = this.accessor.GetContenaReserve(dt.Rows[0]["SYSTEM_ID"].ToString(), dt.Rows[0]["SEQ"].ToString());
                if (contenaReserveEntity != null)
                {
                    foreach (T_CONTENA_RESERVE entity in contenaReserveEntity)
                    {
                        this.dto.contenaReserveList.Add(entity);
                    }
                }
                this.form.txtSecchi.Text = "";
                this.form.txtHikiage.Text = "";
                SqlInt32 sumSecchi = 0;
                SqlInt32 sumHikiage = 0;
                List<T_CONTENA_RESULT> tmpList = new List<T_CONTENA_RESULT>();
                foreach (T_CONTENA_RESERVE entity in this.dto.contenaReserveList)
                {
                    if (entity.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI
                        && entity.DELETE_FLG.Equals(SqlBoolean.False))
                    {
                        //設置
                        if (!entity.DAISUU_CNT.Equals(SqlInt32.Null))
                        {
                            sumSecchi += entity.DAISUU_CNT;
                        }
                    }
                    else if (entity.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE
                             && entity.DELETE_FLG.Equals(SqlBoolean.False))
                    {
                        //引揚
                        if (!entity.DAISUU_CNT.Equals(SqlInt32.Null))
                        {
                            sumHikiage += entity.DAISUU_CNT;
                        }
                    }
                    //コンテナ稼動予定をコンテナ稼動実績に移行
                    T_CONTENA_RESULT resultEntity = new T_CONTENA_RESULT();
                    if (entity != null)
                    {
                        resultEntity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;
                        resultEntity.SYSTEM_ID = entity.SYSTEM_ID;
                        resultEntity.SEQ = entity.SEQ;
                        resultEntity.CONTENA_SET_KBN = entity.CONTENA_SET_KBN;
                        resultEntity.CONTENA_SHURUI_CD = entity.CONTENA_SHURUI_CD;
                        resultEntity.CONTENA_CD = entity.CONTENA_CD;
                        resultEntity.DAISUU_CNT = (SqlInt16)entity.DAISUU_CNT;
                        resultEntity.CREATE_USER = entity.CREATE_USER;
                        resultEntity.CREATE_DATE = entity.CREATE_DATE;
                        resultEntity.CREATE_PC = entity.CREATE_PC;
                        resultEntity.UPDATE_USER = entity.UPDATE_USER;
                        resultEntity.UPDATE_DATE = entity.UPDATE_DATE;
                        resultEntity.UPDATE_PC = entity.UPDATE_PC;
                        resultEntity.DELETE_FLG = entity.DELETE_FLG;
                        resultEntity.TIME_STAMP = entity.TIME_STAMP;
                        tmpList.Add(resultEntity);
                    }
                }
                this.dto.contenaResultList = tmpList;
                this.form.txtSecchi.Text = sumSecchi.ToString();
                this.form.txtHikiage.Text = sumHikiage.ToString();

                int maxRows = this.form.mrwDetail.Rows.Count;
                int dataCount = 0;
                for (int i = (maxRows - 1); i < (maxRows - 1) + dt.Rows.Count; i++)
                {
                    // DETAIL_SYSTEM_IDが0（SQLでnullは0としている）の場合は明細なし
                    if (int.Parse(dt.Rows[dataCount]["DETAIL_SYSTEM_ID"].ToString()) > 0)
                    {
                        // 行追加
                        this.form.mrwDetail.Rows.Add();

                        this.form.mrwDetail.Rows[i]["ROW_NO"].Value = dt.Rows[dataCount]["DETAIL_ROW_NO"].ToString();
                        //品名CD
                        if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_HINMEI_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_HINMEI_NAME_RYAKU"].ToString()))
                        {
                            this.form.mrwDetail.Rows[i]["HINMEI_CD"].Value = "";
                            this.form.mrwDetail.Rows[i]["HINMEI_NAME"].Value = "";
                            this.form.mrwDetail.Rows[i][CELL_NAME_HINMEI_ZEI_KBN_CD].Value = "";
                        }
                        else
                        {
                            this.form.mrwDetail.Rows[i]["HINMEI_CD"].Value = dt.Rows[dataCount]["DETAIL_HINMEI_CD"];
                            this.form.mrwDetail.Rows[i]["HINMEI_NAME"].Value = dt.Rows[dataCount]["DETAIL_HINMEI_NAME_RYAKU"];
                            this.form.mrwDetail.Rows[i][CELL_NAME_HINMEI_ZEI_KBN_CD].Value = dt.Rows[dataCount]["DETAIL_HINMEI_ZEI_KBN_CD"];
                        }
                        //伝票区分CD
                        if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_DENPYOU_KBN_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_DENPYOU_KBN_NAME_RYAKU"].ToString()))
                        {
                            this.form.mrwDetail.Rows[i]["DENPYOU_KBN_CD"].Value = "";
                            this.form.mrwDetail.Rows[i]["DENPYOU_KBN_NAME"].Value = "";
                        }
                        else
                        {
                            this.form.mrwDetail.Rows[i]["DENPYOU_KBN_CD"].Value = dt.Rows[dataCount]["DETAIL_DENPYOU_KBN_CD"];
                            this.form.mrwDetail.Rows[i]["DENPYOU_KBN_NAME"].Value = dt.Rows[dataCount]["DETAIL_DENPYOU_KBN_NAME_RYAKU"];
                        }
                        //数量
                        if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_SUURYOU"].ToString()))
                        {
                            this.form.mrwDetail.Rows[i]["SUURYOU"].Value = "";
                        }
                        else
                        {
                            this.form.mrwDetail.Rows[i]["SUURYOU"].Value = dt.Rows[dataCount]["DETAIL_SUURYOU"];
                        }
                        //単位CD
                        if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_UNIT_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_UNIT_NAME_RYAKU"].ToString()))
                        {
                            this.form.mrwDetail.Rows[i]["UNIT_CD"].Value = "";
                            this.form.mrwDetail.Rows[i]["UNIT_NAME_RYAKU"].Value = "";
                        }
                        else
                        {
                            //string tempUnit = String.Format("{0:D3}", int.Parse(dt.Rows[dataCount]["DETAIL_UNIT_CD"].ToString()));    // No.2715
                            //this.form.mrwDetail.Rows[i]["UNIT_CD"].Value = tempUnit;    // No.2715
                            this.form.mrwDetail.Rows[i]["UNIT_CD"].Value = dt.Rows[dataCount]["DETAIL_UNIT_CD"].ToString();    // No.2715
                            this.form.mrwDetail.Rows[i]["UNIT_NAME_RYAKU"].Value = dt.Rows[dataCount]["DETAIL_UNIT_NAME_RYAKU"];
                        }
                        //単価
                        if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_TANKA"].ToString()))
                        {
                            this.form.mrwDetail.Rows[i]["TANKA"].Value = "";
                        }
                        else
                        {
                            this.form.mrwDetail.Rows[i]["TANKA"].Value = dt.Rows[dataCount]["DETAIL_TANKA"];
                        }
                        //金額
                        if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_KINGAKU"].ToString()))
                        {
                            this.form.mrwDetail.Rows[i]["KINGAKU"].Value = "";
                        }
                        else
                        {
                            this.form.mrwDetail.Rows[i]["KINGAKU"].Value = dt.Rows[dataCount]["DETAIL_KINGAKU"];
                        }
                        //明細備考
                        if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_MEISAI_BIKOU"].ToString()))
                        {
                            this.form.mrwDetail.Rows[i]["MEISAI_BIKOU"].Value = "";
                        }
                        else
                        {
                            this.form.mrwDetail.Rows[i]["MEISAI_BIKOU"].Value = dt.Rows[dataCount]["DETAIL_MEISAI_BIKOU"];
                        }

                        // 個別品名単価読み込み
                        if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_TANKA"].ToString()))
                        {
                            this.SearchAndCalcForUnit(false, this.form.mrwDetail.Rows[i]);
                        }
                    }
                    dataCount += 1;
                }
                this.ResetTankaCheck(); // MAILAN #158993 START

                bool catchErr = false;
                // 行番号採番
                catchErr = this.NumberingRowNo();
                if (catchErr)
                {
                    return true;
                }

                // 設定後処理
                SettingsAfterDisplayData(dt);

                foreach (Row row in this.form.mrwDetail.Rows)
                {
                    this.form.SetIchranReadOnly(row.Index);
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("GetUketsukeNumber", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("GetUketsukeNumber", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 配車状況CDの画面遷移可・不可を判定
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private bool isOpenedScreenHaishaJokyoCd(DataTable dt)
        {
            var haishaJokyoCd = dt.Rows[0]["HAISHA_JOKYO_CD"].ToString();

            // 配車状況が「1:受注」「2:配車」「3:計上」「5:回収なし」以外は遷移できない
            if (SalesPaymentConstans.HAISHA_JOKYO_CD_CANCEL.Equals(haishaJokyoCd))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 伝票に関するエラーメッセージを表示
        /// </summary>
        private void showErrorDenpyou()
        {
            // メッセージ表示
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            msgLogic.MessageBoxShow("E101", "伝票", "売上");

            // 入力受付番号クリア
            this.form.UKETSUKE_NUMBER.Text = "";

            //フォーカスを受付番号にする
            this.form.UKETSUKE_NUMBER.Focus();
        }

        /// <summary>
        /// 番号入力データ表示後処理
        /// </summary>
        internal void SettingsAfterDisplayData(DataTable dt)
        {
            LogUtility.DebugMethodStart();

            // 諸口区分チェック
            if (dt.Rows[0]["TORIHIKISAKI_SHOKUCHI_KBN"].ToString().Equals("1"))
            {
                // 取引先名編集可
                this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = false;
                //this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = true;
                this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = GetTabStop("TORIHIKISAKI_NAME_RYAKU");    // No.3822
            }
            if (dt.Rows[0]["NIZUMI_GYOUSHA_SHOKUCHI_KBN"].ToString().Equals("1"))
            {
                this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = false;
                //this.form.NIZUMI_GYOUSHA_NAME.TabStop = true;
                this.form.NIZUMI_GYOUSHA_NAME.TabStop = GetTabStop("NIZUMI_GYOUSHA_NAME");    // No.3822
            }
            if (dt.Rows[0]["NIZUMI_GENBA_SHOKUCHI_KBN"].ToString().Equals("1"))
            {
                this.form.NIZUMI_GENBA_NAME.ReadOnly = false;
                //this.form.NIZUMI_GENBA_NAME.TabStop = true;
                this.form.NIZUMI_GENBA_NAME.TabStop = GetTabStop("NIZUMI_GENBA_NAME");    // No.3822
            }
            if (dt.Rows[0]["NIOROSHI_GYOUSHA_SHOKUCHI_KBN"].ToString().Equals("1"))
            {
                this.form.NIOROSHI_GYOUSHA_NAME.ReadOnly = false;
                //this.form.NIOROSHI_GYOUSHA_NAME.TabStop = true;
                this.form.NIOROSHI_GYOUSHA_NAME.TabStop = GetTabStop("NIOROSHI_GYOUSHA_NAME");    // No.3822
            }
            if (dt.Rows[0]["NIOROSHI_GENBA_SHOKUCHI_KBN"].ToString().Equals("1"))
            {
                this.form.NIOROSHI_GENBA_NAME.ReadOnly = false;
                //this.form.NIOROSHI_GENBA_NAME.TabStop = true;
                this.form.NIOROSHI_GENBA_NAME.TabStop = GetTabStop("NIOROSHI_GENBA_NAME");    // No.3822
            }
            if (dt.Rows[0]["UNPAN_GYOUSHA_SHOKUCHI_KBN"].ToString().Equals("1"))
            {
                this.form.UNPAN_GYOUSHA_NAME.ReadOnly = false;
                //this.form.UNPAN_GYOUSHA_NAME.TabStop = true;
                this.form.UNPAN_GYOUSHA_NAME.TabStop = GetTabStop("UNPAN_GYOUSHA_NAME");    // No.3822
            }
            if (dt.Rows[0]["GYOUSHA_SHOKUCHI_KBN"].ToString().Equals("1"))
            {
                // 業者名編集可
                this.form.GYOUSHA_NAME_RYAKU.ReadOnly = false;
                //this.form.GYOUSHA_NAME_RYAKU.TabStop = true;
                this.form.GYOUSHA_NAME_RYAKU.TabStop = GetTabStop("GYOUSHA_NAME_RYAKU");    // No.3822
            }
            if (dt.Rows[0]["GENBA_SHOKUCHI_KBN"].ToString().Equals("1"))
            {
                // 現場名編集可
                this.form.GENBA_NAME_RYAKU.ReadOnly = false;
                //this.form.GENBA_NAME_RYAKU.TabStop = true;
                this.form.GENBA_NAME_RYAKU.TabStop = GetTabStop("GENBA_NAME_RYAKU");    // No.3822
            }
            // 取引先セット時
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                //エンティティを更新するため請求支払のチェックはそのまま呼ぶ

                // 請求締日チェック
                this.CheckSeikyuuShimebi();

                // 支払い締日チェック
                this.CheckShiharaiShimebi();

                //取引区分チェック
                this.CheckTorihikiKBN();
            }

            // 合計系金額計算
            bool catchErr = this.CalcTotalValues();
            if (catchErr)
            {
                throw new Exception("");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌名を編集可にする
        /// </summary>
        /// <param name="dt"></param>
        internal void CheckSharyouUketsuke_NASHI(DataTable dt)
        {
            // 車輌名を編集可
            this.form.SHARYOU_NAME_RYAKU.ReadOnly = false;
            // 自由入力可能であるため車輌名の色を変更
            this.form.SHARYOU_CD.AutoChangeBackColorEnabled = false;
            this.form.SHARYOU_CD.BackColor = sharyouCdBackColor;
            // マスタに存在しない場合、ユーザに車輌名を自由入力させる
            bool catchErr = false;
            this.form.SHARYOU_NAME_RYAKU.Text = ZeroSuppress(this.form.SHARYOU_CD, out catchErr);
            if (catchErr)
            {
                throw new Exception("");
            }
            // No.3822-->
            // 車輌名のタブストップ変更
            this.form.SHARYOU_NAME_RYAKU.TabStop = GetTabStop("SHARYOU_NAME_RYAKU");
            // No.3822<--
        }

        /// <summary>
        /// 背景色自動設定モード切り替え
        /// </summary>
        internal bool ChangeAutoChangeBackColorEnabled()
        {
            try
            {
                this.headerForm.KYOTEN_CD.IsInputErrorOccured = false;
                this.form.ENTRY_NUMBER.IsInputErrorOccured = false;
                this.form.KAKUTEI_KBN.IsInputErrorOccured = false;
                this.form.UKETSUKE_NUMBER.IsInputErrorOccured = false;
                this.form.TORIHIKISAKI_CD.IsInputErrorOccured = false;
                this.form.GYOUSHA_CD.IsInputErrorOccured = false;
                this.form.GENBA_CD.IsInputErrorOccured = false;
                this.form.NIZUMI_GYOUSHA_CD.IsInputErrorOccured = false;
                this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = false;
                this.form.NIOROSHI_GYOUSHA_CD.IsInputErrorOccured = false;
                this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = false;
                this.form.DENPYOU_BIKOU.IsInputErrorOccured = false;
                this.form.SHARYOU_CD.IsInputErrorOccured = false;
                this.form.SHASHU_CD.IsInputErrorOccured = false;
                this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = false;
                this.form.UNTENSHA_CD.IsInputErrorOccured = false;
                this.form.NINZUU_CNT.IsInputErrorOccured = false;
                this.form.KEITAI_KBN_CD.IsInputErrorOccured = false;
                this.form.CONTENA_SOUSA_CD.IsInputErrorOccured = false;
                this.form.MANIFEST_SHURUI_CD.IsInputErrorOccured = false;
                this.form.MANIFEST_TEHAI_CD.IsInputErrorOccured = false;
                this.form.EIGYOU_TANTOUSHA_CD.IsInputErrorOccured = false;
                this.form.SHUUKEI_KOUMOKU_CD.IsInputErrorOccured = false; //PhuocLoc 2020/12/01 #136221
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeAutoChangeBackColorEnabled", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
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
        /// 検索ボタンの設定をする
        /// ポップアップの設定だけをセッティング
        /// </summary>
        internal void setSearchButtonInfo()
        {
            // 各CDのフォーカスアウト処理を通すため、検索ポップアップから戻ってきたら各CDへフォーカスする
            this.form.TORIHIKISAKI_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToTorihikisakiCd";
            //this.form.GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToGyoushaCd";
            //this.form.GENBA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToGenbaCd";
            //this.form.NIZUMI_GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToNizumiGyoushaCd";
            //this.form.NIZUMI_GENBA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToNizumiGenbaCd";
            //this.form.NIOROSHI_GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToNioroshiGyoushaCd";
            //this.form.NIOROSHI_GENBA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToNioroshiGenbaCd";
            this.form.UNPAN_GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToUnpanGyoushaCd";
            this.form.NYUURYOKU_TANTOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToNyuuryokuTantoushaCd";
        }

        /// <summary>
        /// 検索ボタンの設定をする
        /// ポップアップの設定だけをセッティング
        /// </summary>
        internal bool MoveErrorFocus()
        {
            try
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
                //DateTimePickerに現在エラーが発生しているかどうかを調べる方法がないため、
                //独自でチェックする。
                if (this.form.DENPYOU_DATE.Value == null)
                {
                    if (target != null)
                    {
                        if (this.form.DENPYOU_DATE.TabIndex < target.TabIndex)
                        {
                            target = this.form.DENPYOU_DATE;
                        }
                    }
                    else
                    {
                        target = this.form.DENPYOU_DATE;
                    }
                }
                else if (this.form.URIAGE_DATE.Value == null)
                {
                    if (target != null)
                    {
                        if (this.form.URIAGE_DATE.TabIndex < target.TabIndex)
                        {
                            target = this.form.URIAGE_DATE;
                        }
                    }
                    else
                    {
                        target = this.form.URIAGE_DATE;
                    }
                }
                else if (this.form.SHIHARAI_DATE.Value == null)
                {
                    if (target != null)
                    {
                        if (this.form.SHIHARAI_DATE.TabIndex < target.TabIndex)
                        {
                            target = this.form.SHIHARAI_DATE;
                        }
                    }
                    else
                    {
                        target = this.form.SHIHARAI_DATE;
                    }
                }

                //明細項目の必須チェックエラーにカーソルを合わせる。
                if (target == null)
                {
                    this.form.mrwDetail.SuspendLayout();

                    bool bFound = false;

                    if (this.form.mrwDetail.Rows.Count > 1)
                    {

                        foreach (var row in this.form.mrwDetail.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                if (row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Visible == true
                                    && (row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value == null
                                    || string.IsNullOrEmpty(row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Value.ToString())))
                                {
                                    this.form.mrwDetail.CurrentCell = row.Cells[CELL_NAME_URIAGESHIHARAI_DATE];
                                    this.form.mrwDetail.BeginEdit(true);
                                    bFound = true;
                                }
                                else
                                {
                                    string[] strColNameArray = new string[] {   CELL_NAME_HINMEI_CD,
                                                                            CELL_NAME_HINMEI_NAME,
                                                                            CELL_NAME_SUURYOU,
                                                                            CELL_NAME_UNIT_CD,
                                                                            CELL_NAME_KINGAKU };
                                    foreach (string strColName in strColNameArray)
                                    {
                                        if (row.Cells[strColName] is ICustomTextBox)
                                        {
                                            if (string.IsNullOrEmpty(Convert.ToString(row.Cells[strColName].Value)))
                                            {
                                                //((ICustomTextBox)row.Cells[strColName]).IsInputErrorOccured = true;
                                                this.form.mrwDetail.CurrentCell = row.Cells[strColName];
                                                this.form.mrwDetail.BeginEdit(true);
                                                bFound = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            //エラーの項目を見つけた場合はループ処理を終了する。
                            if (bFound)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        var row = this.form.mrwDetail.Rows[0];
                        if (row.Cells[CELL_NAME_URIAGESHIHARAI_DATE].Visible == true)
                        {
                            this.form.mrwDetail.CurrentCell = row.Cells[CELL_NAME_URIAGESHIHARAI_DATE];
                            this.form.mrwDetail.BeginEdit(true);
                            bFound = true;
                        }
                        else
                        {
                            this.form.mrwDetail.CurrentCell = row.Cells[CELL_NAME_HINMEI_CD];
                            this.form.mrwDetail.BeginEdit(true);
                        }

                    }

                }
                else
                {
                    target.Focus();
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("MoveErrorFocus", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 営業担当者の表示（現場マスタ、業者マスタ、取引先マスタを元に）
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <param name="gyoushaCd"></param>
        /// <param name="torihikisakiCd"></param>
        internal void setEigyou_Tantousha(string genbaCd, string gyoushaCd, string torihikisakiCd)
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
                    // 現場CD入力あり
                    bool catchErr = false;
                    genbaEntity = this.accessor.GetGenba(gyoushaCd, genbaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
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

            bool catchErr = false;
            gyoushaEntity = this.accessor.GetGyousha(gyoushaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
            if (catchErr)
            {
                throw new Exception("");
            }
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
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "業者");
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
                bool catchErr = false;
                torihikisakiEntity = this.accessor.GetTorihikisaki(torihikisakiCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr)
                {
                    throw new Exception("");
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
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "取引先");
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
        /// 指定された売上／支払番号の次に大きい番号を取得
        /// </summary>
        /// <param name="UrshNumber"></param>
        /// <returns></returns>
        internal long GetNextUrshNumber(long UrshNumber,out bool catchErr)
        {
            try
            {
                catchErr = false;
                // No.3341-->
                string KyotenCD = this.headerForm.KYOTEN_CD.Text;
                long returnValue = this.accessor.GetNextUrshNumber(UrshNumber, KyotenCD);
                if (returnValue == 0)
                {
                    returnValue = this.accessor.GetNextUrshNumber(0, KyotenCD);
                    if (returnValue == UrshNumber)
                    {
                        returnValue = 0;
                    }
                }
                // No.3341<--
                return returnValue;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("GetNextUrshNumber", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return -1;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("GetNextUrshNumber", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return -1;
            }
        }

        /// <summary>
        /// 指定された売上／支払番号の次に小さい番号を取得
        /// </summary>
        /// <param name="UrshNumber"></param>
        /// <returns></returns>
        internal long GetPreUrshNumber(long UrshNumber,out bool catchErr)
        {
            try
            {
                catchErr = false;
                // No.3341-->
                string KyotenCD = this.headerForm.KYOTEN_CD.Text;
                long returnValue = this.accessor.GetPreUrshNumber(UrshNumber, KyotenCD);
                if (returnValue == 0)
                {
                    long max = this.accessor.GetMaxUrshNumber();
                    returnValue = this.accessor.GetPreUrshNumber(max + 1, KyotenCD);
                    if (returnValue == UrshNumber)
                    {
                        returnValue = 0;
                    }
                }
                // No.3341<--
                return returnValue;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("GetPreUrshNumber", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return -1;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("GetPreUrshNumber", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return -1;
            }
        }

        // No.1767
        /// <summary>
        /// 指定された売上／支払番号の次に小さい番号を取得
        /// </summary>
        /// <returns></returns>
        internal long GetMaxUrshNumber(out bool catchErr)
        {
            try
            {
                catchErr = false;
                long returnValue = this.accessor.GetMaxUrshNumber();
                return returnValue;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("GetMaxUrshNumber", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return -1;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("GetMaxUrshNumber", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return -1;
            }
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
            short.TryParse(Convert.ToString(this.form.mrwDetail.Rows[0].Cells[CELL_NAME_DENPYOU_KBN_CD].Value), out denpyouKbnCd);
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
            foreach (var row in this.form.mrwDetail.Rows)
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
        /// 入力情報から確定伝票かどうかを判断
        /// </summary>
        /// <returns>true : 確定、false : 未確定</returns>
        internal bool IsKakuteiDenpyou()
        {
            bool returnValue = false;

            /**
             * 確定フラグの制御
             * 
             * ■システム設定の確定条件:伝票単位の場合
             * 　Detailの確定フラグ：Entryの確定フラグをチェック
             * 
             * ■システム設定の確定条件：明細単位の場合
             * 　Entryの確定フラグ：Detailの確定フラグに1つでも未確定があったら未確定
             * 　　　　　　　　　　 上記以外は確定
             */
            if (this.dto.sysInfoEntity.SYS_KAKUTEI__TANNI_KBN == SalesPaymentConstans.SYS_KAKUTEI_TANNI_KBN_DENPYOU)
            {
                returnValue = SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI.ToString().Equals(this.form.KAKUTEI_KBN.Text);

            }
            else
            {
                // 明細単位
                foreach (Row row in this.form.mrwDetail.Rows)
                {
                    if (row.IsNewRow || string.IsNullOrEmpty((string)row.Cells["ROW_NO"].Value.ToString()))
                    {
                        continue;
                    }

                    if (row.Cells[CELL_NAME_KAKUTEI_KBN].Value == null
                        || !(bool)row.Cells[CELL_NAME_KAKUTEI_KBN].Value)
                    {
                        returnValue = false;
                        break;
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// ゼロサプレス処理
        /// </summary>
        /// <param name="source">入力コントロール</param>
        /// <returns>ゼロサプレス後の文字列</returns>
        private string ZeroSuppress(object source, out bool catchErr)
        {
            try
            {
                string result = string.Empty;
                catchErr = false;

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
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("ZeroSuppress", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return "";
            }
        }

        /// <summary>
        /// 売上日付を基に売上消費税率を設定
        /// </summary>
        internal bool SetUriageShouhizeiRate()
        {
            try
            {
                DateTime uriageDate = this.footerForm.sysDate.Date;
                if (DateTime.TryParse(this.form.URIAGE_DATE.Text, out uriageDate))
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
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetUriageShouhizeiRate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 支払日付を基に売上消費税率を設定
        /// </summary>
        internal bool SetShiharaiShouhizeiRate()
        {
            try
            {
                DateTime shiharaiDate = this.footerForm.sysDate.Date;
                if (DateTime.TryParse(this.form.SHIHARAI_DATE.Text, out shiharaiDate))
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
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShiharaiShouhizeiRate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 売上、支払消費税率のポップアップ設定初期化
        /// </summary>
        internal void InitShouhizeiRatePopupSetting()
        {
            /**
             * 売上消費税率テキストボックスの設定
             */
            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupWindowId = WINDOW_ID.M_SHOUHIZEI;
            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupWindowName = "マスタ共通ポップアップ";
            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupGetMasterField = "SHOUHIZEI_RATE";
            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupSetFormField = "URIAGE_SHOUHIZEI_RATE_VALUE";
            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupDataHeaderTitle = new string[] { "消費税率" };

            // 表示情報作成
            var shouhizeiRates = this.accessor.GetAllShouhizeiRate();
            var dt = EntityUtility.EntityToDataTable(shouhizeiRates);


            var displayShouhizei = new DataTable();
            foreach (var col in this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()))
            {
                displayShouhizei.Columns.Add(dt.Columns[col].ColumnName, dt.Columns[col].DataType);

            }

            foreach (DataRow row in dt.Rows)
            {
                displayShouhizei.Rows.Add(displayShouhizei.Columns.OfType<DataColumn>().Select(s => row[s.ColumnName]).ToArray());
            }

            displayShouhizei.TableName = "消費税率";
            this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupDataSource = displayShouhizei;

            /**
             * ポップアップの設定
             */
            this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupGetMasterField = this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupGetMasterField;
            this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupSetFormField = this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupSetFormField;
            this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupWindowId = this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupWindowId;
            this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupWindowName = this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupWindowName;
            this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupDataHeaderTitle = this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupDataHeaderTitle;
            this.form.URIAGE_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupDataSource = this.form.URIAGE_SHOUHIZEI_RATE_VALUE.PopupDataSource;

            /**
             * 支払消費税率テキストボックスの設定
             */
            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupWindowId = WINDOW_ID.M_SHOUHIZEI;
            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupWindowName = "マスタ共通ポップアップ";
            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupGetMasterField = "SHOUHIZEI_RATE";
            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupSetFormField = "SHIHARAI_SHOUHIZEI_RATE_VALUE";
            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupDataHeaderTitle = new string[] { "消費税率" }; ;
            // 売上消費税率と同様のマスタを参照するためデータソースは売上消費税のを流用
            this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupDataSource = displayShouhizei;

            /**
             * ポップアップの設定
             */
            this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupGetMasterField = this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupGetMasterField;
            this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupSetFormField = this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupSetFormField;
            this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupWindowId = this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupWindowId;
            this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupWindowName = this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupWindowName;
            this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupDataHeaderTitle = this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupDataHeaderTitle;
            this.form.SHIHARAI_SHOUHIZEI_RATE_SEARCH_BUTTON.PopupDataSource = this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.PopupDataSource;
        }

        /// <summary>
        /// UIFormの売上消費税率をパーセント表記で取得する
        /// </summary>
        /// <returns>パーセント表示の売上消費税率</returns>
        internal string ToPercentForUriageShouhizeiRate(out bool catchErr)
        {
            try
            {
                catchErr = false;
                string returnVal = string.Empty;

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
                catchErr = true;
                LogUtility.Error("ToPercentForUriageShouhizeiRate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return "";
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
            try
            {
                catchErr = false;
                string returnVal = string.Empty;

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
                catchErr = true;
                LogUtility.Error("ToPercentForShiharaiShouhizeiRate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return "";
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
        #endregion

        #region 締状況チェック処理
        /// <summary>
        /// 締状況チェック処理
        /// 請求明細、精算明細、在庫明細を確認して、対象の伝票に締済のデータが存在するか確認する。
        /// </summary>
        internal bool CheckAllShimeStatus(out bool catchErr)
        {
            try
            {
                bool retval = false;
                catchErr = false;

                long systemId = -1;
                int seq = -1;

                if (!this.dto.entryEntity.SYSTEM_ID.IsNull) systemId = (long)this.dto.entryEntity.SYSTEM_ID;
                if (!this.dto.entryEntity.SEQ.IsNull) seq = (int)this.dto.entryEntity.SEQ;
                if (systemId != -1 && seq != -1)
                {
                    // 締処理状況判定用データ取得
                    DataTable seikyuuData = this.accessor.GetSeikyuMeisaiData(systemId, seq, -1, this.dto.entryEntity.TORIHIKISAKI_CD);
                    DataTable seisanData = this.accessor.GetSeisanMeisaiData(systemId, seq, -1, this.dto.entryEntity.TORIHIKISAKI_CD);

                    // 締処理状況(請求明細)
                    if (seikyuuData != null && 0 < seikyuuData.Rows.Count)
                    {
                        retval = true;
                    }

                    // 締処理状況(精算明細)
                    if (retval == false && seisanData != null && 0 < seisanData.Rows.Count)
                    {
                        retval = true;
                    }
                }

                return retval;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckAllShimeStatus", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = true;
                return true;
            }
        }
        #endregion 締状況チェック処理

        #region 配車状況更新処理（収集）
        /// <summary>
        /// 収集受付入力の配車状況を更新します
        /// </summary>
        /// <param name="haishaJokyoCd">配車状況CD</param>
        /// <param name="haishaJokyoName">配車状況</param>
        private void UpdateHaishaJokyoSs(string haishaJokyoCd, string haishaJokyoName)
        {
            LogUtility.DebugMethodStart(haishaJokyoCd, haishaJokyoName);

            if (Int16.Parse(SalesPaymentConstans.HAISHA_JOKYO_CD_CANCEL) != this.tUketsukeSsEntry.HAISHA_JOKYO_CD)
            {
                // 配車状況が変更されているときだけ更新する
                var newEntryEntity = this.CreateTUketsukeSsEntry(this.tUketsukeSsEntry);
                newEntryEntity.HAISHA_JOKYO_CD = Int16.Parse(haishaJokyoCd);
                newEntryEntity.HAISHA_JOKYO_NAME = haishaJokyoName;
                newEntryEntity.CONTENA_SOUSA_CD = new T_UKETSUKE_SS_ENTRY().CONTENA_SOUSA_CD;
                this.accessor.InsertUketsukeSsEntry(newEntryEntity);

                // もとのエンティティを削除する
                this.DeleteTUketsukeSsEntry(this.tUketsukeSsEntry);

                // 子の収集受付詳細を更新する
                var tUketsukeSsDetailList = this.accessor.GetUketsukeSsDetail(this.tUketsukeSsEntry.SYSTEM_ID.ToString(), this.tUketsukeSsEntry.SEQ.ToString());
                foreach (var tUketsukeSsDetail in tUketsukeSsDetailList)
                {
                    var newDetailEntity = this.CreateTUketsukeSsDetail(newEntryEntity, tUketsukeSsDetail);
                    this.accessor.InsertUketsukeSsDetail(newDetailEntity);
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 渡された収集受付入力エンティティをコピーして新しいエンティティを作成します
        /// </summary>
        /// <param name="entity">元になるエンティティ</param>
        /// <returns>作成したエンティティ</returns>
        private T_UKETSUKE_SS_ENTRY CreateTUketsukeSsEntry(T_UKETSUKE_SS_ENTRY entity)
        {
            LogUtility.DebugMethodStart(entity);

            // 配車状況を変更したエンティティを作成
            var newEntity = new T_UKETSUKE_SS_ENTRY();
            Shougun.Core.Common.BusinessCommon.Utility.MasterUtility.CopyProperties(entity, newEntity);
            var dbLogic = new DataBinderLogic<T_UKETSUKE_SS_ENTRY>(newEntity);
            dbLogic.SetSystemProperty(newEntity, false);
            newEntity.SEQ = entity.SEQ + 1;
            newEntity.CREATE_USER = entity.CREATE_USER;
            newEntity.CREATE_DATE = entity.CREATE_DATE;
            newEntity.CREATE_PC = entity.CREATE_PC;
            LogUtility.DebugMethodEnd(newEntity);

            return newEntity;
        }

        /// <summary>
        /// 渡された収集受付入力エンティティを削除します
        /// </summary>
        /// <param name="entity">削除するエンティティ</param>
        private void DeleteTUketsukeSsEntry(T_UKETSUKE_SS_ENTRY entity)
        {
            LogUtility.DebugMethodStart(entity);

            var dbLogic = new DataBinderLogic<T_UKETSUKE_SS_ENTRY>(entity);
            dbLogic.SetSystemProperty(entity, true);
            entity.DELETE_FLG = true;
            this.accessor.UpdateUketsukeSsEntry(entity);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 渡された収集受付詳細エンティティをコピーして新しいエンティティを作成します
        /// </summary>
        /// <param name="entryEntity">親になる収集受付入力エンティティ</param>
        /// <param name="detailEntity">元になる収集受付詳細エンティティ</param>
        /// <returns>作成したエンティティ</returns>
        private T_UKETSUKE_SS_DETAIL CreateTUketsukeSsDetail(T_UKETSUKE_SS_ENTRY entryEntity, T_UKETSUKE_SS_DETAIL detailEntity)
        {
            LogUtility.DebugMethodStart(entryEntity, detailEntity);

            var newEntity = new T_UKETSUKE_SS_DETAIL();
            Shougun.Core.Common.BusinessCommon.Utility.MasterUtility.CopyProperties(detailEntity, newEntity);
            var dbLogic = new DataBinderLogic<T_UKETSUKE_SS_DETAIL>(newEntity);
            dbLogic.SetSystemProperty(newEntity, false);
            newEntity.SEQ = entryEntity.SEQ; // SEQだけ変更される
            newEntity.CREATE_USER = entryEntity.CREATE_USER;
            newEntity.CREATE_DATE = entryEntity.CREATE_DATE;
            newEntity.CREATE_PC = entryEntity.CREATE_PC;
            LogUtility.DebugMethodEnd(newEntity);

            return newEntity;
        }

        /// <summary>
        /// 取得済みの収集受付詳細エンティティをクリアします
        /// </summary>
        internal void ClearTUketsukeSsEntry()
        {
            this.tUketsukeSsEntry = null;
        }
        #endregion

        #region 配車状況更新処理（出荷）
        /// <summary>
        /// 出荷受付入力の配車状況を更新します
        /// </summary>
        /// <param name="haishaJokyoCd">配車状況CD</param>
        /// <param name="haishaJokyoName">配車状況</param>
        private void UpdateHaishaJokyoSk(string haishaJokyoCd, string haishaJokyoName)
        {
            LogUtility.DebugMethodStart(haishaJokyoCd, haishaJokyoName);

            if (Int16.Parse(SalesPaymentConstans.HAISHA_JOKYO_CD_CANCEL) != this.tUketsukeSkEntry.HAISHA_JOKYO_CD
                                           && Int16.Parse(haishaJokyoCd) != this.tUketsukeSkEntry.HAISHA_JOKYO_CD)
            {
                // 配車状況が変更されているときだけ更新する
                var newEntryEntity = this.CreateTUketsukeSkEntry(this.tUketsukeSkEntry);
                newEntryEntity.HAISHA_JOKYO_CD = Int16.Parse(haishaJokyoCd);
                newEntryEntity.HAISHA_JOKYO_NAME = haishaJokyoName;
                this.accessor.InsertUketsukeSkEntry(newEntryEntity);

                // もとのエンティティを削除する
                this.DeleteTUketsukeSkEntry(this.tUketsukeSkEntry);

                // 子の収集受付詳細を更新する
                var tUketsukeSkDetailList = this.accessor.GetUketsukeSkDetail(this.tUketsukeSkEntry.SYSTEM_ID.ToString(), this.tUketsukeSkEntry.SEQ.ToString());
                foreach (var tUketsukeSkDetail in tUketsukeSkDetailList)
                {
                    var newDetailEntity = this.CreateTUketsukeSkDetail(newEntryEntity, tUketsukeSkDetail);
                    this.accessor.InsertUketsukeSkDetail(newDetailEntity);
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 渡された出荷受付入力エンティティをコピーして新しいエンティティを作成します
        /// </summary>
        /// <param name="entity">元になるエンティティ</param>
        /// <returns>作成したエンティティ</returns>
        private T_UKETSUKE_SK_ENTRY CreateTUketsukeSkEntry(T_UKETSUKE_SK_ENTRY entity)
        {
            LogUtility.DebugMethodStart(entity);

            // 配車状況を変更したエンティティを作成
            var newEntity = new T_UKETSUKE_SK_ENTRY();
            Shougun.Core.Common.BusinessCommon.Utility.MasterUtility.CopyProperties(entity, newEntity);
            var dbLogic = new DataBinderLogic<T_UKETSUKE_SK_ENTRY>(newEntity);
            dbLogic.SetSystemProperty(newEntity, false);
            newEntity.SEQ = entity.SEQ + 1;
            newEntity.CREATE_USER = entity.CREATE_USER;
            newEntity.CREATE_DATE = entity.CREATE_DATE;
            newEntity.CREATE_PC = entity.CREATE_PC;
            LogUtility.DebugMethodEnd(newEntity);

            return newEntity;
        }

        /// <summary>
        /// 渡された出荷受付入力エンティティを削除します
        /// </summary>
        /// <param name="entity">削除するエンティティ</param>
        private void DeleteTUketsukeSkEntry(T_UKETSUKE_SK_ENTRY entity)
        {
            LogUtility.DebugMethodStart(entity);

            var dbLogic = new DataBinderLogic<T_UKETSUKE_SK_ENTRY>(entity);
            dbLogic.SetSystemProperty(entity, true);
            entity.DELETE_FLG = true;
            this.accessor.UpdateUketsukeSkEntry(entity);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 渡された出荷受付詳細エンティティをコピーして新しいエンティティを作成します
        /// </summary>
        /// <param name="entryEntity">親になる出荷受付入力エンティティ</param>
        /// <param name="detailEntity">元になる出荷受付詳細エンティティ</param>
        /// <returns>作成したエンティティ</returns>
        private T_UKETSUKE_SK_DETAIL CreateTUketsukeSkDetail(T_UKETSUKE_SK_ENTRY entryEntity, T_UKETSUKE_SK_DETAIL detailEntity)
        {
            LogUtility.DebugMethodStart(entryEntity, detailEntity);

            var newEntity = new T_UKETSUKE_SK_DETAIL();
            Shougun.Core.Common.BusinessCommon.Utility.MasterUtility.CopyProperties(detailEntity, newEntity);
            var dbLogic = new DataBinderLogic<T_UKETSUKE_SK_DETAIL>(newEntity);
            dbLogic.SetSystemProperty(newEntity, false);
            newEntity.SEQ = entryEntity.SEQ;
            newEntity.CREATE_USER = entryEntity.CREATE_USER;
            newEntity.CREATE_DATE = entryEntity.CREATE_DATE;
            newEntity.CREATE_PC = entryEntity.CREATE_PC;
            LogUtility.DebugMethodEnd(newEntity);

            return newEntity;
        }

        /// <summary>
        /// 取得済みの出荷受付詳細エンティティをクリアします
        /// </summary>
        internal void ClearTUketsukeSkEntry()
        {
            this.tUketsukeSkEntry = null;
        }
        #endregion

        // No.2613-->
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
        // No.2613<--

        // 20140512 kayo No.679 計量番号連携 start
        /// <summary>
        /// 計量番号からデータ取得
        /// </summary>
        internal bool GetKeiryouNumber()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.KEIRYOU_NUMBER.Text))
                {
                    return false;
                }

                bool catchErr = false;
                DataTable dt = this.accessor.GetKeiryou(this.form.KEIRYOU_NUMBER.Text);
                if (dt.Rows.Count > 0)
                {
                    //伝票日付、売上日付、支払日付
                    if (!string.IsNullOrEmpty(dt.Rows[0]["DENPYOU_DATE"].ToString()))
                    {
                        this.form.DENPYOU_DATE.Text = dt.Rows[0]["DENPYOU_DATE"].ToString();
                        this.form.URIAGE_DATE.Text = dt.Rows[0]["DENPYOU_DATE"].ToString();
                        this.form.SHIHARAI_DATE.Text = dt.Rows[0]["DENPYOU_DATE"].ToString();

                        // 消費税率の設定
                        DateTime uriageDate = this.footerForm.sysDate.Date;
                        if (DateTime.TryParse(this.form.URIAGE_DATE.Text, out uriageDate))
                        {
                            var shouhizeiRate = this.accessor.GetShouhizeiRate(uriageDate);
                            if (!shouhizeiRate.SHOUHIZEI_RATE.IsNull)
                            {
                                this.form.URIAGE_SHOUHIZEI_RATE_VALUE.Text = shouhizeiRate.SHOUHIZEI_RATE.ToString();
                            }
                        }

                        DateTime shiharaiDate = this.footerForm.sysDate.Date;
                        if (DateTime.TryParse(this.form.SHIHARAI_DATE.Text, out shiharaiDate))
                        {
                            var shiharaiShouhizeiRate = this.accessor.GetShouhizeiRate(shiharaiDate);
                            if (!shiharaiShouhizeiRate.SHOUHIZEI_RATE.IsNull)
                            {
                                this.form.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text = shiharaiShouhizeiRate.SHOUHIZEI_RATE.ToString();
                            }
                        }
                    }
                    //取引先CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["TORIHIKISAKI_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["TORIHIKISAKI_NAME_RYAKU"].ToString()))
                    {
                        this.form.TORIHIKISAKI_CD.Text = string.Empty;
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_CD.Text = dt.Rows[0]["TORIHIKISAKI_CD"].ToString();
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = dt.Rows[0]["TORIHIKISAKI_NAME_RYAKU"].ToString();
                    }
                    //業者CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["GYOUSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString()))
                    {
                        this.form.GYOUSHA_CD.Text = string.Empty;
                        this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                    }
                    else
                    {
                        this.form.GYOUSHA_CD.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                        this.form.GYOUSHA_NAME_RYAKU.Text = dt.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString();
                    }
                    //現場CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["GENBA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["GENBA_NAME_RYAKU"].ToString()))
                    {
                        this.form.GENBA_CD.Text = string.Empty;
                        this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                        strGenbaName = string.Empty;   // No.3279
                    }
                    else
                    {
                        this.form.GENBA_CD.Text = dt.Rows[0]["GENBA_CD"].ToString();
                        this.form.GENBA_NAME_RYAKU.Text = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();

                        M_GENBA entGenba = accessor.GetGenba(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                        if (catchErr)
                        {
                            return true;
                        }
                        if (entGenba != null)
                        {
                            // 諸口区分チェック
                            if (entGenba.SHOKUCHI_KBN.IsTrue)
                            {
                                strGenbaName = this.form.GENBA_NAME_RYAKU.Text;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(entGenba.GENBA_NAME1))
                                {
                                    strGenbaName = entGenba.GENBA_NAME1 + entGenba.GENBA_NAME2;
                                }
                                else
                                {
                                    strGenbaName = "";
                                }
                            }
                        }

                    }
                    //運搬業者CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["UNPAN_GYOUSHA_NAME_RYAKU"].ToString()))
                    {
                        this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
                        this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                    }
                    else
                    {
                        this.form.UNPAN_GYOUSHA_CD.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                        this.form.UNPAN_GYOUSHA_NAME.Text = dt.Rows[0]["UNPAN_GYOUSHA_NAME_RYAKU"].ToString();
                    }
                    //荷積業者CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["NIZUMI_GYOUSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["NIZUMI_GYOUSHA_NAME_RYAKU"].ToString()))
                    {
                        this.form.NIZUMI_GYOUSHA_CD.Text = string.Empty;
                        this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
                    }
                    else
                    {
                        this.form.NIZUMI_GYOUSHA_CD.Text = dt.Rows[0]["NIZUMI_GYOUSHA_CD"].ToString();
                        this.form.NIZUMI_GYOUSHA_NAME.Text = dt.Rows[0]["NIZUMI_GYOUSHA_NAME_RYAKU"].ToString();
                    }
                    //荷積現場CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["NIZUMI_GENBA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["NIZUMI_GENBA_NAME_RYAKU"].ToString()))
                    {
                        this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                        this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                    }
                    else
                    {
                        this.form.NIZUMI_GENBA_CD.Text = dt.Rows[0]["NIZUMI_GENBA_CD"].ToString();
                        this.form.NIZUMI_GENBA_NAME.Text = dt.Rows[0]["NIZUMI_GENBA_NAME_RYAKU"].ToString();
                    }
                    //荷降業者CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["NIOROSHI_GYOUSHA_NAME_RYAKU"].ToString()))
                    {
                        this.form.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                    }
                    else
                    {
                        this.form.NIOROSHI_GYOUSHA_CD.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_NAME_RYAKU"].ToString();
                    }
                    //荷降現場CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["NIOROSHI_GENBA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["NIOROSHI_GENBA_NAME_RYAKU"].ToString()))
                    {
                        this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                        this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                    }
                    else
                    {
                        this.form.NIOROSHI_GENBA_CD.Text = dt.Rows[0]["NIOROSHI_GENBA_CD"].ToString();
                        this.form.NIOROSHI_GENBA_NAME.Text = dt.Rows[0]["NIOROSHI_GENBA_NAME_RYAKU"].ToString();
                    }
                    //営業担当者CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["EIGYOU_TANTOUSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["EIGYOU_TANTOUSHA_NAME_RYAKU"].ToString()))
                    {
                        this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
                        this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
                    }
                    else
                    {
                        this.form.EIGYOU_TANTOUSHA_CD.Text = dt.Rows[0]["EIGYOU_TANTOUSHA_CD"].ToString();
                        this.form.EIGYOU_TANTOUSHA_NAME.Text = dt.Rows[0]["EIGYOU_TANTOUSHA_NAME_RYAKU"].ToString();
                    }
                    //車輌CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["SHARYOU_CD"].ToString()))
                    {
                        //コードが無しの場合は未入力状態
                        this.form.SHARYOU_CD.Text = "";
                        this.form.SHARYOU_NAME_RYAKU.Text = "";
                    }
                    else if (string.IsNullOrEmpty(dt.Rows[0]["SHARYOU_NAME_RYAKU"].ToString()))
                    {
                        //コードありで名称無しの場合は車輌名入力可能状態
                        this.form.SHARYOU_CD.Text = dt.Rows[0]["SHARYOU_CD"].ToString();
                        this.form.SHARYOU_NAME_RYAKU.Text = "";
                        this.CheckSharyouUketsuke_NASHI(dt);
                    }
                    else
                    {
                        this.form.SHARYOU_CD.Text = dt.Rows[0]["SHARYOU_CD"].ToString();
                        this.form.SHARYOU_NAME_RYAKU.Text = dt.Rows[0]["SHARYOU_NAME_RYAKU"].ToString();
                    }
                    //車種CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["SHASHU_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["SHASHU_NAME_RYAKU"].ToString()))
                    {
                        this.form.SHASHU_CD.Text = string.Empty;
                        this.form.SHASHU_NAME.Text = string.Empty;
                    }
                    else
                    {
                        this.form.SHASHU_CD.Text = dt.Rows[0]["SHASHU_CD"].ToString();
                        this.form.SHASHU_NAME.Text = dt.Rows[0]["SHASHU_NAME_RYAKU"].ToString();
                    }
                    //運転者CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["UNTENSHA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["UNTENSHA_NAME_RYAKU"].ToString()))
                    {
                        this.form.UNTENSHA_CD.Text = string.Empty;
                        this.form.UNTENSHA_NAME.Text = string.Empty;
                    }
                    else
                    {
                        this.form.UNTENSHA_CD.Text = dt.Rows[0]["UNTENSHA_CD"].ToString();
                        this.form.UNTENSHA_NAME.Text = dt.Rows[0]["UNTENSHA_NAME_RYAKU"].ToString();
                    }
                    //マニ種類CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["MANIFEST_SHURUI_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["MANIFEST_SHURUI_NAME_RYAKU"].ToString()))
                    {
                        this.form.MANIFEST_SHURUI_CD.Text = string.Empty;
                        this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = string.Empty;
                    }
                    else
                    {
                        this.form.MANIFEST_SHURUI_CD.Text = dt.Rows[0]["MANIFEST_SHURUI_CD"].ToString();
                        this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = dt.Rows[0]["MANIFEST_SHURUI_NAME_RYAKU"].ToString();
                    }
                    //コンテナCD
                    if (string.IsNullOrEmpty(dt.Rows[0]["CONTENA_SOUSA_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["CONTENA_JOUKYOU_NAME_RYAKU"].ToString()))
                    {
                        this.form.CONTENA_SOUSA_CD.Text = string.Empty;
                        this.form.CONTENA_JOUKYOU_NAME_RYAKU.Text = string.Empty;
                    }
                    else
                    {
                        this.form.CONTENA_SOUSA_CD.Text = dt.Rows[0]["CONTENA_SOUSA_CD"].ToString();
                        this.form.CONTENA_JOUKYOU_NAME_RYAKU.Text = dt.Rows[0]["CONTENA_JOUKYOU_NAME_RYAKU"].ToString();
                    }
                    //形態区分
                    if (string.IsNullOrEmpty(dt.Rows[0]["KEITAI_KBN_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["KEITAI_KBN_NAME_RYAKU"].ToString()))
                    {
                        this.form.KEITAI_KBN_CD.Text = string.Empty;
                        this.form.KEITAI_KBN_NAME_RYAKU.Text = string.Empty;
                    }
                    else
                    {
                        this.form.KEITAI_KBN_CD.Text = dt.Rows[0]["KEITAI_KBN_CD"].ToString();
                        this.form.KEITAI_KBN_NAME_RYAKU.Text = dt.Rows[0]["KEITAI_KBN_NAME_RYAKU"].ToString();
                    }
                    //マニ手配CD
                    if (string.IsNullOrEmpty(dt.Rows[0]["MANIFEST_TEHAI_CD"].ToString()) || string.IsNullOrEmpty(dt.Rows[0]["MANIFEST_TEHAI_NAME_RYAKU"].ToString()))
                    {
                        this.form.MANIFEST_TEHAI_CD.Text = string.Empty;
                        this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = string.Empty;
                    }
                    else
                    {
                        this.form.MANIFEST_TEHAI_CD.Text = dt.Rows[0]["MANIFEST_TEHAI_CD"].ToString();
                        this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = dt.Rows[0]["MANIFEST_TEHAI_NAME_RYAKU"].ToString();
                    }
                    //受付番号
                    if (string.IsNullOrEmpty(dt.Rows[0]["UKETSUKE_NUMBER"].ToString()))
                    {
                        this.form.UKETSUKE_NUMBER.Text = string.Empty;
                    }
                    else
                    {
                        this.form.UKETSUKE_NUMBER.Text = dt.Rows[0]["UKETSUKE_NUMBER"].ToString();
                    }
                    //伝票備考
                    if (string.IsNullOrEmpty(dt.Rows[0]["DENPYOU_BIKOU"].ToString()))
                    {
                        this.form.DENPYOU_BIKOU.Text = string.Empty;
                    }
                    else
                    {
                        this.form.DENPYOU_BIKOU.Text = dt.Rows[0]["DENPYOU_BIKOU"].ToString();
                    }

                    //設置台数・引揚台数
                    var contenaReserveEntity = this.accessor.GetContenaReserve(dt.Rows[0]["SYSTEM_ID"].ToString(), dt.Rows[0]["SEQ"].ToString());
                    if (contenaReserveEntity != null)
                    {
                        foreach (T_CONTENA_RESERVE entity in contenaReserveEntity)
                        {
                            this.dto.contenaReserveList.Add(entity);
                        }
                    }
                    this.form.txtSecchi.Text = "";
                    this.form.txtHikiage.Text = "";
                    SqlInt32 sumSecchi = 0;
                    SqlInt32 sumHikiage = 0;
                    List<T_CONTENA_RESULT> tmpList = new List<T_CONTENA_RESULT>();
                    foreach (T_CONTENA_RESERVE entity in this.dto.contenaReserveList)
                    {
                        if (entity.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI
                            && entity.DELETE_FLG.Equals(SqlBoolean.False))
                        {
                            //設置
                            if (!entity.DAISUU_CNT.Equals(SqlInt32.Null))
                            {
                                sumSecchi += entity.DAISUU_CNT;
                            }
                        }
                        else if (entity.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE
                                 && entity.DELETE_FLG.Equals(SqlBoolean.False))
                        {
                            //引揚
                            if (!entity.DAISUU_CNT.Equals(SqlInt32.Null))
                            {
                                sumHikiage += entity.DAISUU_CNT;
                            }
                        }
                    }
                    this.form.txtSecchi.Text = sumSecchi.ToString();
                    this.form.txtHikiage.Text = sumHikiage.ToString();

                    int maxRows = this.form.mrwDetail.Rows.Count;
                    int dataCount = 0;
                    for (int i = (maxRows - 1); i < (maxRows - 1) + dt.Rows.Count; i++)
                    {
                        // DETAIL_SYSTEM_IDが0（SQLでnullは0としている）の場合は明細なし
                        if (int.Parse(dt.Rows[dataCount]["DETAIL_SYSTEM_ID"].ToString()) > 0)
                        {
                            // 行追加
                            this.form.mrwDetail.Rows.Add();

                            this.form.mrwDetail.Rows[i]["ROW_NO"].Value = dt.Rows[dataCount]["DETAIL_ROW_NO"].ToString();
                            //伝票区分CD
                            if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_DENPYOU_KBN_CD"].ToString())
                                || string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_DENPYOU_KBN_NAME_RYAKU"].ToString()))
                            {
                                this.form.mrwDetail.Rows[i]["DENPYOU_KBN_CD"].Value = "";
                                this.form.mrwDetail.Rows[i]["DENPYOU_KBN_NAME"].Value = "";
                            }
                            else
                            {
                                this.form.mrwDetail.Rows[i]["DENPYOU_KBN_CD"].Value = dt.Rows[dataCount]["DETAIL_DENPYOU_KBN_CD"];
                                this.form.mrwDetail.Rows[i]["DENPYOU_KBN_NAME"].Value = dt.Rows[dataCount]["DETAIL_DENPYOU_KBN_NAME_RYAKU"];
                            }
                            //品名CD
                            if (string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_HINMEI_CD"].ToString())
                                || string.IsNullOrEmpty(dt.Rows[dataCount]["DETAIL_HINMEI_NAME_RYAKU"].ToString()))
                            {
                                this.form.mrwDetail.Rows[i]["HINMEI_CD"].Value = "";
                                this.form.mrwDetail.Rows[i]["HINMEI_NAME"].Value = "";
                            }
                            else
                            {
                                this.form.mrwDetail.Rows[i]["HINMEI_CD"].Value = dt.Rows[dataCount]["DETAIL_HINMEI_CD"];
                                this.form.mrwDetail.Rows[i]["HINMEI_NAME"].Value = dt.Rows[dataCount]["DETAIL_HINMEI_NAME_RYAKU"];
                            }
                            //明細備考
                            if (string.IsNullOrEmpty(dt.Rows[dataCount]["MEISAI_BIKOU"].ToString()))
                            {
                                this.form.mrwDetail.Rows[i]["MEISAI_BIKOU"].Value = "";
                            }
                            else
                            {
                                this.form.mrwDetail.Rows[i]["MEISAI_BIKOU"].Value = dt.Rows[dataCount]["MEISAI_BIKOU"].ToString();
                            }
                        }

                        dataCount += 1;
                    }

                    // 行番号採番
                    catchErr = this.NumberingRowNo();
                    if (catchErr)
                    {
                        return true;
                    }

                    // 設定後処理
                    SettingsAfterDisplayData(dt);
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("GetKeiryouNumber", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("GetKeiryouNumber", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                LogUtility.DebugMethodEnd();
                return true;
            }
        }
        // 20140512 kayo No.679 計量番号連携 end

        /// <summary>
        /// 明細欄の品名をセットします
        /// </summary>
        /// <param name="row">現在のセルを含む行（CurrentRow）</param>
        internal bool setHinmeiName(Row row)
        {
            try
            {
                if (row == null)
                {
                    return false;
                }
                if ((this.CheckHinmeiCd(row)))    // 品名コードの存在チェック（伝種区分が受入、または共通）
                {
                    M_HINMEI hinmei = this.accessor.GetHinmeiDataByCd(row.Cells[CELL_NAME_HINMEI_CD].Value.ToString());

                    // 入力された品名コードが存在するとき
                    if (row.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value != null)
                    {
                        if (string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value.ToString()))
                        {
                            // 品名が空の場合再セット
                            //row.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value = this.SearchHinmei(row.Cells["HINMEI_CD"].Value.ToString());
                            bool catchErr = false;
                            this.GetHinmeiForPop(row, out catchErr);
                            if (catchErr)
                            {
                                return true;
                            }
                            row.Cells[LogicClass.CELL_NAME_HINMEI_ZEI_KBN_CD].Value = hinmei != null ? hinmei.ZEI_KBN_CD.ToString() : string.Empty;
                        }
                    }
                    else
                    {
                        // 品名が空の場合再セット
                        //row.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value = this.SearchHinmei(row.Cells["HINMEI_CD"].Value.ToString());
                        bool catchErr = false;
                        this.GetHinmeiForPop(row, out catchErr);
                        if (catchErr)
                        {
                            return true;
                        }
                        row.Cells[LogicClass.CELL_NAME_HINMEI_ZEI_KBN_CD].Value = hinmei != null ? hinmei.ZEI_KBN_CD.ToString() : string.Empty;
                    }
                }
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("setHinmeiName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("setHinmeiName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

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
                    CheckTorihikisaki();
                    this.form.GYOUSHA_CD.Text = this.form.moveData_gyousyaCd;
                    CheckGyousha();
                    this.form.GENBA_CD.Text = this.form.moveData_genbaCd;
                    CheckGenba();
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    this.hasShow = false;
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                }
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetMoveData", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetMoveData", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        // No.3822-->
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

                        //if (readOnlyValue == false && textProperty != null)
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
                var row = this.form.mrwDetail.Template.Row;
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

                        //if (readOnlyValue == false && textProperty != null)
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
            if (Properties.Settings.Default.DenpyouCtrl != null && Properties.Settings.Default.DenpyouCtrl.Count > 0)
            {
                if (DenpyouCtrl.Count > 0)
                {
                    DenpyouCtrl.Clear();
                }
                for (var i = 0; i < Properties.Settings.Default.DenpyouCtrl.Count; i++)
                {
                    DenpyouCtrl.Add(Properties.Settings.Default.DenpyouCtrl[i]);
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
						//20151026 hoanghm #13404 start
						//control.TabStop = true;
                        Type type  = control.GetType();
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
						//20151026 hoanghm #13404 end
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
            if (Properties.Settings.Default.DetailCtrl != null && Properties.Settings.Default.DetailCtrl.Count > 0)
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
                var row = this.form.mrwDetail.Template.Row;
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
                            return false;
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
                                return false;
                            }
                        }
                    }
                }

                // 最後までみつからなかった場合
                // 詳細で最初を探す
                GrapeCity.Win.MultiRow.Cell gcontrol = nextDetailContorl(true);
                if (gcontrol != null)
                {
                    gcontrol.Selected = true;
                    if (gcontrol.GcMultiRow != null)
                    {
                        gcontrol.GcMultiRow.Focus();
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTopControlFocus", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 次のタブストップのコントロールにフォーカス移動
        /// </summary>
        /// <param name="foward"></param>
        public bool GotoNextControl(bool foward)
        {
            try
            {
                Control control = NextFormControl(foward);
                if (control != null)
                {
                    control.Focus();
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GotoNextControl", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
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
                    autochange = (ICustomAutoChangeBackColor)controlUtil.FindControl(this.form, controlName);
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
            GrapeCity.Win.MultiRow.Cell gcontrol = nextDetailContorl(foward);
            if (gcontrol != null)
            {
                gcontrol.Selected = true;
                return this.form.mrwDetail;
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
        public GrapeCity.Win.MultiRow.Cell nextDetailContorl(bool foward)
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
                var tmprow = this.form.mrwDetail.Template.Row;
                GrapeCity.Win.MultiRow.Cell tmpcell = tmprow.Cells[controlName];
                if (tmpcell != null)
                {
                    if (tmpcell.TabStop == true && tmpcell.Visible == true && tmpcell.ReadOnly == false)    // テンプレートのタブストップで判断
                    {
                        var currentrow = this.form.mrwDetail.Rows[0];
                        if (foward == false)
                        {   // 最後の場合、最後の行の最後のセル
                            var last = this.form.mrwDetail.RowCount - 1;
                            currentrow = this.form.mrwDetail.Rows[last];
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
        // No.3822<--

        /// <summary>
        /// 現在入力されている情報から諸口状態の車輌CDかチェック
        /// 諸口状態だった場合は、車輌CD、車輌名のデザインを諸口状態用に変更する
        /// </summary>
        internal void CheckShokuchiSharyou()
        {
            var sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null, SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()));
            if (sharyouEntitys == null || sharyouEntitys.Length < 1)
            {
                this.ChangeShokuchiSharyouDesign(true);
            }
            else
            {
                this.ChangeShokuchiSharyouDesign(false);
            }
        }

        /// <summary>
        /// 車輌CD、車輌名を諸口状態のデザインへ変更する
        /// </summary>
        internal void ChangeShokuchiSharyouDesign(bool isShokuchi)
        {
            if (isShokuchi)
            {
                this.form.oldSharyouShokuchiKbn = true;
                this.form.SHARYOU_NAME_RYAKU.ReadOnly = false;
                this.form.SHARYOU_NAME_RYAKU.TabStop = GetTabStop("SHARYOU_NAME_RYAKU");
                this.form.SHARYOU_NAME_RYAKU.Tag = this.sharyouHinttext;
                // 自由入力可能であるため車輌名の色を変更
                this.form.SHARYOU_CD.AutoChangeBackColorEnabled = false;
                this.form.SHARYOU_CD.BackColor = sharyouCdBackColor;
            }
            else
            {
                // デザインが初期化されないのでここで初期化
                this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
                this.form.SHARYOU_NAME_RYAKU.Tag = string.Empty;
                this.form.SHARYOU_NAME_RYAKU.TabStop = false;
                this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                this.form.SHARYOU_CD.AutoChangeBackColorEnabled = true;
                this.form.oldSharyouShokuchiKbn = false;
            }
        }

        /// <summary>
        /// コンテナ情報が変更されているかチェック
        /// </summary>
        /// <returns>true:変更有, false:変更無</returns>
        internal bool IsChengeContenaData()
        {
            bool returnVal = false;

            // 修正前の要素がなければ変更有と判断
            if ((this.beforDto.contenaResultList == null
                || this.beforDto.contenaResultList.Count < 1)
                && (this.dto.contenaResultList == null
                || this.dto.contenaResultList.Count < 1))
            {
                // 両方空なら変更無と判断
                return false;
            }
            else if ((this.beforDto.contenaResultList == null || this.beforDto.contenaResultList.Count < 1)
                || (this.dto.contenaResultList == null || this.dto.contenaResultList.Count < 1))
            {
                // どちらか一方が空だった場合、以降の処理で不都合があるため、
                // このタイミングで変更有と判断
                return true;
            }

            // 要素数が同じ場合にチェック
            if (this.beforDto.contenaResultList.Count == this.dto.contenaResultList.Count)
            {
                foreach (var contenaData in this.dto.contenaResultList)
                {
                    bool isChengeData = true;

                    foreach (var beforeData in this.beforDto.contenaResultList)
                    {
                        if (beforeData.CONTENA_SHURUI_CD.Equals(contenaData.CONTENA_SHURUI_CD)
                            && beforeData.CONTENA_CD.Equals(contenaData.CONTENA_CD)
                            && beforeData.CONTENA_SET_KBN == contenaData.CONTENA_SET_KBN)
                        {
                            isChengeData = false;
                            break;
                        }
                    }

                    if (isChengeData)
                    {
                        // 変更を検知
                        returnVal = true;
                        break;
                    }
                }

                // コンテナ情報が変更されていなくても、日付が変わってたら変更有
                if (!this.beforDto.entryEntity.DENPYOU_DATE.IsNull
                    && this.form.DENPYOU_DATE.Value != null
                    && this.beforDto.entryEntity.DENPYOU_DATE.Value.Date != ((DateTime)this.form.DENPYOU_DATE.Value).Date)
                {
                    return true;
                }
            }
            else
            {
                // 要素数が変わっている場合は変更有
                returnVal = true;
            }

            return returnVal;
        }

        /// <summary>
        /// 変更前と変更後のコンテナデータを比較して、削除されているコンテナデータのリストを返す
        /// </summary>
        /// <returns>true:変更有, false:変更無</returns>
        internal List<T_CONTENA_RESULT> getDeleteContenaData()
        {
            List<T_CONTENA_RESULT> returnVal = new List<T_CONTENA_RESULT>();

            // 修正前の要素がなければ変更有と判断
            if ((this.beforDto.contenaResultList == null
                || this.beforDto.contenaResultList.Count < 1)
                && (this.dto.contenaResultList == null
                || this.dto.contenaResultList.Count < 1))
            {
                // 両方空なら変更無と判断
                return returnVal;
            }
            else if ((this.beforDto.contenaResultList == null || this.beforDto.contenaResultList.Count < 1)
                || (this.dto.contenaResultList == null || this.dto.contenaResultList.Count < 1))
            {
                // どちらか一方が空だった場合、以降の処理で不都合があるため、
                // このタイミングで変更有と判断
                return returnVal;
            }

            // 要素数が同じ場合にチェック
            foreach (var beforeData in this.beforDto.contenaResultList)
            {
                bool isDeleteDeta = true;

                foreach (var contenaData in this.dto.contenaResultList)
                {
                    if (beforeData.CONTENA_SHURUI_CD.Equals(contenaData.CONTENA_SHURUI_CD)
                        && beforeData.CONTENA_CD.Equals(contenaData.CONTENA_CD)
                        && beforeData.CONTENA_SET_KBN == contenaData.CONTENA_SET_KBN)
                    {
                        isDeleteDeta = false;
                        break;
                    }
                }

                if (isDeleteDeta)
                {
                    returnVal.Add(beforeData);
                }
            }

            return returnVal;
        }

        /// <summary>
        /// 変更前と変更後のコンテナデータを比較して、動きのないコンテナデータのリストを返す
        /// </summary>
        /// <returns></returns>
        internal List<T_CONTENA_RESULT> GetStayContenaData()
        {
            List<T_CONTENA_RESULT> returnVal = new List<T_CONTENA_RESULT>();

            // 修正前の要素がなければ画面で指定しているすべてが追加
            if ((this.beforDto.contenaResultList == null
                || this.beforDto.contenaResultList.Count < 1))
            {
                // 両方空なら変更無と判断
                return returnVal;
            }

            foreach (var contena in this.dto.contenaResultList)
            {
                foreach (var beforeContena in this.beforDto.contenaResultList)
                {
                    if (beforeContena.CONTENA_SHURUI_CD.Equals(contena.CONTENA_SHURUI_CD)
                        && beforeContena.CONTENA_CD.Equals(contena.CONTENA_CD)
                        && beforeContena.CONTENA_SET_KBN == contena.CONTENA_SET_KBN)
                    {
                        returnVal.Add(contena);
                    }
                }
            }

            return returnVal;
        }

        // 20141030 koukouei 委託契約チェック start
        #region 委託契約書チェック
        /// <summary>
        /// 委託契約書チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckItakukeiyaku()
        {
            var msgLogic = new MessageBoxShowLogic();
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
                GcCustomMultiRow gridDetail = this.form.mrwDetail;
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
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckItakukeiyaku", ex2);
                msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckItakukeiyaku", ex);
                msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion
        // 20141030 koukouei 委託契約チェック end
        /// 20141112 Houkakou 「売上/支払入力」の締済期間チェックの追加　start
        #region 請求日付チェック
        /// <summary>
        /// 請求日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool SeikyuuDateCheck()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                ShimeCheckLogic CheckShimeDate = new ShimeCheckLogic();
                List<ReturnDate> returnDate = new List<ReturnDate>();
                List<CheckDate> checkDate = new List<CheckDate>();
                ReturnDate rd = new ReturnDate();
                CheckDate cd = new CheckDate();

                bool bDenpyouKbnCheck = false;

                var denpyouKbnSelect = (from temp in this.form.mrwDetail.Rows
                                        where Convert.ToString(temp.Cells["DENPYOU_KBN_NAME"].Value) == "売上"
                                        select temp).ToArray();

                bDenpyouKbnCheck = denpyouKbnSelect != null && denpyouKbnSelect.Length > 0;

                if (bDenpyouKbnCheck == false)
                {
                    return true;
                }

                //nullチェック
                if (string.IsNullOrEmpty(this.form.URIAGE_DATE.Text))
                {
                    return true;
                }
                if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    return true;
                }

                string strSeikyuuDate = this.form.URIAGE_DATE.Text;
                DateTime seikyuudate = Convert.ToDateTime(strSeikyuuDate);

                cd.CHECK_DATE = seikyuudate;
                cd.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                cd.KYOTEN_CD = this.headerForm.KYOTEN_CD.Text;
                checkDate.Add(cd);
                returnDate = CheckShimeDate.GetNearShimeDate(checkDate, 1);

                if (returnDate.Count == 0)
                {
                    return true;
                }
                else if (returnDate.Count == 1)
                {
                    //例外日付が含まれる
                    if (returnDate[0].dtDATE == SqlDateTime.MinValue.Value)
                    {
                        msgLogic.MessageBoxShow("E214");
                        return false;
                    }
                    else
                    {
                        if (msgLogic.MessageBoxShow("C084", returnDate[0].dtDATE.ToString("yyyy/MM/dd"), "請求") == DialogResult.Yes)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //例外日付が含まれる
                    foreach (ReturnDate rdDate in returnDate)
                    {
                        if (rdDate.dtDATE == SqlDateTime.MinValue.Value)
                        {
                            msgLogic.MessageBoxShow("E214");
                            return false;
                        }
                    }
                    if (msgLogic.MessageBoxShow("C085", "請求") == DialogResult.Yes)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SeikyuuDateCheck", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SeikyuuDateCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion

        #region 精算日付チェック
        /// <summary>
        /// 精算日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool SeisanDateCheck()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                ShimeCheckLogic CheckShimeDate = new ShimeCheckLogic();
                List<ReturnDate> returnDate = new List<ReturnDate>();
                List<CheckDate> checkDate = new List<CheckDate>();
                ReturnDate rd = new ReturnDate();
                CheckDate cd = new CheckDate();

                bool bDenpyouKbnCheck = false;

                var denpyouKbnSelect = (from temp in this.form.mrwDetail.Rows
                                        where Convert.ToString(temp.Cells["DENPYOU_KBN_NAME"].Value) == "支払"
                                        select temp).ToArray();

                bDenpyouKbnCheck = denpyouKbnSelect != null && denpyouKbnSelect.Length > 0;

                if (bDenpyouKbnCheck == false)
                {
                    return true;
                }

                //nullチェック
                if (string.IsNullOrEmpty(this.form.SHIHARAI_DATE.Text))
                {
                    return true;
                }
                if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    return true;
                }

                string strShiharaiDate = this.form.SHIHARAI_DATE.Text;
                DateTime shiharaidate = Convert.ToDateTime(strShiharaiDate);

                cd.CHECK_DATE = shiharaidate;
                cd.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                cd.KYOTEN_CD = this.headerForm.KYOTEN_CD.Text;
                checkDate.Add(cd);
                returnDate = CheckShimeDate.GetNearShimeDate(checkDate, 2);

                if (returnDate.Count == 0)
                {
                    return true;
                }
                else if (returnDate.Count == 1)
                {
                    //例外日付が含まれる
                    if (returnDate[0].dtDATE == SqlDateTime.MinValue.Value)
                    {
                        msgLogic.MessageBoxShow("E214");
                        return false;
                    }
                    else
                    {
                        if (msgLogic.MessageBoxShow("C084", returnDate[0].dtDATE.ToString("yyyy/MM/dd"), "支払") == DialogResult.Yes)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //例外日付が含まれる
                    foreach (ReturnDate rdDate in returnDate)
                    {
                        if (rdDate.dtDATE == SqlDateTime.MinValue.Value)
                        {
                            msgLogic.MessageBoxShow("E214");
                            return false;
                        }
                    }
                    if (msgLogic.MessageBoxShow("C085", "支払") == DialogResult.Yes)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SeisanDateCheck", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SeisanDateCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion
        /// 20141112 Houkakou 「売上/支払入力」の締済期間チェックの追加　end

        /// 20141014 teikyou 「売上・支払入力画面」の休動Checkを追加する　start
        #region 車輌休動チェック
        internal bool SharyouDateCheck(bool ShowMsg = true)
        {
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
                //作業日取得
                workclosedsharyouEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);

                M_WORK_CLOSED_SHARYOU[] workclosedsharyouList = workclosedsharyouDao.GetAllValidData(workclosedsharyouEntry);

                //取得テータ
                if (workclosedsharyouList.Count() >= 1)
                {
                    if (ShowMsg)
                    {
                        this.form.SHARYOU_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E206", "車輌", "伝票日付：" + workclosedsharyouEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    }
                    return false;
                }

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SharyouDateCheck", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SharyouDateCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }
        #endregion

        #region 運転者休動チェック
        internal bool UntenshaDateCheck()
        {
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
                    msgLogic.MessageBoxShow("E206", "運転者", "伝票日付：" + workcloseduntenshaEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    return false;
                }

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("UntenshaDateCheck", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UntenshaDateCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }
        #endregion

        #region 搬入先休動チェック
        internal bool HannyuusakiDateCheck(bool ShowMsg = true)
        {
            try
            {
                string inputNioroshiGyoushaCd = this.form.NIOROSHI_GYOUSHA_CD.Text;
                string inputNioroshiGenbaCd = this.form.NIOROSHI_GENBA_CD.Text;
                string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Text);

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
                    if (ShowMsg)
                    {
                        this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E206", "荷降現場", "伝票日付：" + workclosedhannyuusakiEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    }
                    return false;
                }

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("HannyuusakiDateCheck", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HannyuusakiDateCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }
        #endregion

        #region 受付番号チェック
        internal bool UketukeBangoCheck(out bool catchErr)
        {
            try
            {
                catchErr = false;
                string inputUnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
                string inputSharyouCd = this.form.SHARYOU_CD.Text;
                string inputUntenshaCd = this.form.UNTENSHA_CD.Text;
                string inputNioroshiGyoushaCd = this.form.NIOROSHI_GYOUSHA_CD.Text;
                string inputNioroshiGenbaCd = this.form.NIOROSHI_GENBA_CD.Text;
                string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Text);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return true;
                }

                // 車輌休動
                M_WORK_CLOSED_SHARYOU workclosedsharyouEntry = new M_WORK_CLOSED_SHARYOU();
                //運搬業者CD
                workclosedsharyouEntry.GYOUSHA_CD = inputUnpanGyoushaCd;
                //車輌CD取得
                workclosedsharyouEntry.SHARYOU_CD = inputSharyouCd;
                //伝票日付取得
                workclosedsharyouEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);
                M_WORK_CLOSED_SHARYOU[] workclosedsharyouList = workclosedsharyouDao.GetAllValidData(workclosedsharyouEntry);

                // 運転者休動
                M_WORK_CLOSED_UNTENSHA workcloseduntenshaEntry = new M_WORK_CLOSED_UNTENSHA();
                //運転者CD取得
                workcloseduntenshaEntry.SHAIN_CD = inputUntenshaCd;
                //作業日取得
                workcloseduntenshaEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);
                M_WORK_CLOSED_UNTENSHA[] workcloseduntenshaList = workcloseduntenshaDao.GetAllValidData(workcloseduntenshaEntry);

                // 搬入先休動
                M_WORK_CLOSED_HANNYUUSAKI workclosedhannyuusakiEntry = new M_WORK_CLOSED_HANNYUUSAKI();
                //荷降業者CD取得
                workclosedhannyuusakiEntry.GYOUSHA_CD = inputNioroshiGyoushaCd;
                //荷降現場CD取得
                workclosedhannyuusakiEntry.GENBA_CD = inputNioroshiGenbaCd;
                //作業日取得
                workclosedhannyuusakiEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);
                M_WORK_CLOSED_HANNYUUSAKI[] workclosedhannyuusakiList = workclosedhannyuusakiDao.GetAllValidData(workclosedhannyuusakiEntry);

                //取得テータ
                if (workclosedsharyouList.Count() >= 1)
                {
                    this.form.SHARYOU_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E208", "受付番号", ":" + this.form.UKETSUKE_NUMBER.Text, "車輌",
                                                    "伝票日付：" + workclosedsharyouEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    this.form.SHARYOU_CD.Focus();
                    return false;
                }
                else if (workcloseduntenshaList.Count() >= 1)
                {                    
                    msgLogic.MessageBoxShow("E208", "受付番号", ":" + this.form.UKETSUKE_NUMBER.Text, "運転者",
                                                    "伝票日付：" + workcloseduntenshaEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    this.form.UNTENSHA_CD.Focus();
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    return false;
                }
                else if (workclosedhannyuusakiList.Count() >= 1)
                {
                    this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E208", "受付番号", ":" + this.form.UKETSUKE_NUMBER.Text, "荷降現場" + this.form.NIOROSHI_GENBA_CD.Text,
                                                    "伝票日付：" + workclosedhannyuusakiEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    this.form.NIOROSHI_GENBA_CD.Focus();
                    return false;
                }

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("UketukeBangoCheck", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                this.form.NIOROSHI_GENBA_CD.Focus();
                return true;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("UketukeBangoCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                this.form.NIOROSHI_GENBA_CD.Focus();
                return true;
            }
        }
        #endregion

        #region 計量番号チェック
        internal bool KeiryouBangoCheck()
        {
            string inputUnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
            string inputSharyouCd = this.form.SHARYOU_CD.Text;
            string inputUntenshaCd = this.form.UNTENSHA_CD.Text;
            string inputNioroshiGyoushaCd = this.form.NIOROSHI_GYOUSHA_CD.Text;
            string inputNioroshiGenbaCd = this.form.NIOROSHI_GENBA_CD.Text;
            string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Text);

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (String.IsNullOrEmpty(inputSagyouDate))
            {
                return true;
            }

            // 車輌休動
            M_WORK_CLOSED_SHARYOU workclosedsharyouEntry = new M_WORK_CLOSED_SHARYOU();
            //運搬業者CD
            workclosedsharyouEntry.GYOUSHA_CD = inputUnpanGyoushaCd;
            //車輌CD取得
            workclosedsharyouEntry.SHARYOU_CD = inputSharyouCd;
            //伝票日付取得
            workclosedsharyouEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);
            M_WORK_CLOSED_SHARYOU[] workclosedsharyouList = workclosedsharyouDao.GetAllValidData(workclosedsharyouEntry);

            // 運転者休動
            M_WORK_CLOSED_UNTENSHA workcloseduntenshaEntry = new M_WORK_CLOSED_UNTENSHA();
            //運転者CD取得
            workcloseduntenshaEntry.SHAIN_CD = inputUntenshaCd;
            //作業日取得
            workcloseduntenshaEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);
            M_WORK_CLOSED_UNTENSHA[] workcloseduntenshaList = workcloseduntenshaDao.GetAllValidData(workcloseduntenshaEntry);

            // 搬入先休動
            M_WORK_CLOSED_HANNYUUSAKI workclosedhannyuusakiEntry = new M_WORK_CLOSED_HANNYUUSAKI();
            //荷降業者CD取得
            workclosedhannyuusakiEntry.GYOUSHA_CD = inputNioroshiGyoushaCd;
            //荷降現場CD取得
            workclosedhannyuusakiEntry.GENBA_CD = inputNioroshiGenbaCd;
            //作業日取得
            workclosedhannyuusakiEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);
            M_WORK_CLOSED_HANNYUUSAKI[] workclosedhannyuusakiList = workclosedhannyuusakiDao.GetAllValidData(workclosedhannyuusakiEntry);

            //取得テータ
            if (workclosedsharyouList.Count() >= 1)
            {
                this.form.SHARYOU_CD.IsInputErrorOccured = true;
                msgLogic.MessageBoxShow("E208", "受付番号", ":" + this.form.UKETSUKE_NUMBER.Text, "車輌",
                                                "伝票日付：" + workclosedsharyouEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                this.form.SHARYOU_CD.Focus();
                return false;
            }
            else if (workcloseduntenshaList.Count() >= 1)
            {                
                msgLogic.MessageBoxShow("E208", "受付番号", ":" + this.form.UKETSUKE_NUMBER.Text, "運転者",
                                                "伝票日付：" + workcloseduntenshaEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                this.form.UNTENSHA_CD.Focus();
                this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                return false;
            }
            else if (workclosedhannyuusakiList.Count() >= 1)
            {
                this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                msgLogic.MessageBoxShow("E208", "受付番号", ":" + this.form.UKETSUKE_NUMBER.Text, "荷降現場",
                                                "伝票日付：" + workclosedhannyuusakiEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                this.form.NIOROSHI_GENBA_CD.Focus();
                return false;
            }

            return true;
        }
        #endregion
        /// 20141014 teikyou 「売上・支払入力画面」の休動Checkを追加する　end

        #region 現金取引チェック
        /// <summary>
        /// 現金取引チェック
        /// </summary>
        /// <returns>
        /// true  = 取引区分の売上支払のどちらかが現金 AND 確定フラグが2：未確定以外の場合
        /// false = 取引区分の売上支払のどちらかが現金 AND 確定フラグが2：未確定の場合
        /// </returns>
        internal bool GenkinTorihikiCheck(out bool catchErr)
        {
            try
            {
                catchErr = false;
                var uriageTorihikiKbn = this.form.txtUri.Text;
                var shiharaiTorihikiKbn = this.form.txtShi.Text;
                short kakuteiFlg = 0;
                if (!string.IsNullOrEmpty(this.form.KAKUTEI_KBN.Text))
                    short.TryParse(this.form.KAKUTEI_KBN.Text, out kakuteiFlg);

                var genkin = SalesPaymentConstans.STR_TORIHIKI_KBN_1;
                var uriageRowCount = 0;
                var siharaiRowCount = 0;
                var denpyouKbnCuloumnIndex = this.form.mrwDetail.Columns["DENPYOU_KBN_CD"].Index;
                var ren = true;

                // 売上
                if (uriageTorihikiKbn == genkin)
                {
                    // 明細の売上行数
                    uriageRowCount = this.form.mrwDetail.Rows.Cast<GrapeCity.Win.MultiRow.Row>().ToList().
                                        Where(r => r.Cells[denpyouKbnCuloumnIndex].Value != null).ToList().
                                        Where(r => r.Cells[denpyouKbnCuloumnIndex].Value.ToString() == "1").Count();
                }

                // 支払
                if (shiharaiTorihikiKbn == genkin)
                {
                    // 明細の支払行数
                    siharaiRowCount = this.form.mrwDetail.Rows.Cast<GrapeCity.Win.MultiRow.Row>().ToList().
                                        Where(r => r.Cells[denpyouKbnCuloumnIndex].Value != null).ToList().
                                        Where(r => r.Cells[denpyouKbnCuloumnIndex].Value.ToString() == "2").Count();
                }

                // 確定フラグが2：未確定の場合
                if ((uriageRowCount != 0 || siharaiRowCount != 0) && (kakuteiFlg == SalesPaymentConstans.KAKUTEI_KBN_MIKAKUTEI))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E236");
                    ren = false;
                }

                return ren;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("GenkinTorihikiCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }
        #endregion

        #region キャッシャ連動
        /// <summary>
        /// キャッシャ情報送信
        /// </summary>
        private void sendCasher()
        {
            // 売上金額算出※現金の場合のみ
            decimal uriKin = 0;
            if (this.dto.entryEntity.URIAGE_TORIHIKI_KBN_CD == CommonConst.TORIHIKI_KBN_GENKIN)
            {
                // 金額
                decimal kin = (this.dto.entryEntity.URIAGE_AMOUNT_TOTAL.Value + this.dto.entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL.Value);

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
                decimal kin = (this.dto.entryEntity.SHIHARAI_AMOUNT_TOTAL.Value + this.dto.entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL.Value);

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
                casherDto.DENPYOU_NUMBER = this.dto.entryEntity.UR_SH_NUMBER.Value;
                casherDto.KINGAKU = kingaku;
                casherDto.BIKOU = (string.IsNullOrEmpty(this.dto.entryEntity.DENPYOU_BIKOU) ? string.Empty : this.dto.entryEntity.DENPYOU_BIKOU);
                casherDto.DENSHU_KBN_CD = CommonConst.DENSHU_KBN_UR_SH;
                casherDto.KYOTEN_CD = this.dto.entryEntity.KYOTEN_CD.Value;

                // キャッシャ共通処理に情報セット
                var casherAccessor = new CasherAccessor();
                casherAccessor.setCasherData(casherDto);
            }
        }

        #endregion キャッシャ連動

        //ThangNguyen [Add] 20150826 #10907 Start
        private void CheckTorihikisakiShokuchi()
        {
            bool catchErr = false;
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                var torihikisakiEntity = this.accessor.GetTorihikisaki(this.form.TORIHIKISAKI_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr)
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
                bool catchErr = false;
                var gyoushaEntity = this.accessor.GetGyousha(this.form.GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr)
                {
                    throw new Exception("");
                }
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
                bool catchErr = false;
                var genbaEntity = this.accessor.GetGenba(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr)
                {
                    throw new Exception("");
                }
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

        private void CheckNizumiGyoushaShokuchi()
        { 
            if (!string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
            {
                bool catchErr = false;
                var nizumiGyousha = this.accessor.GetGyousha(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr)
                {
                    throw new Exception("");
                }
                if (nizumiGyousha != null)
                {
                    // 20151104 BUNN #12040 STR
                    if (nizumiGyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue || nizumiGyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                    // 20151104 BUNN #12040 END

                    {
                        this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = !(bool)nizumiGyousha.SHOKUCHI_KBN;
                        this.form.NIZUMI_GYOUSHA_NAME.Tag = (bool)nizumiGyousha.SHOKUCHI_KBN ? this.nizumiGyoushaHintText : string.Empty;
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
                    // 事業場区分、現場区分チェック
                    // 20151104 BUNN #12040 STR
                    if (genba.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue || genba.TSUMIKAEHOKAN_KBN.IsTrue)
                    // 20151104 BUNN #12040 END
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

        private void CheckNioroshiGyoushaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
            {
                bool catchErr = false;
                var gyousha = this.accessor.GetGyousha(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr)
                {
                    throw new Exception("");
                }
                if (gyousha != null)
                {
                    // 20151104 BUNN #12040 STR
                    if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue || gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue)
                    // 20151104 BUNN #12040 END
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
                    // 20151104 BUNN #12040 STR
                    if (genba.TSUMIKAEHOKAN_KBN.IsTrue || genba.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || genba.SAISHUU_SHOBUNJOU_KBN.IsTrue)
                    // 20151104 BUNN #12040 END
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

        private void CheckUpanGyoushaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                bool catchErr = false;
                var gyousha = this.accessor.GetGyousha(this.form.UNPAN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr)
                {
                    throw new Exception("");
                }
                if (gyousha != null)
                {
                    // 20151104 BUNN #12040 STR
                    if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                    // 20151104 BUNN #12040 END
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
        //ThangNguyen [Add] 20150826 #10907 End
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            System.Data.DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        /// <summary>
        /// 更新用受付入力データ取得
        /// </summary>
        /// <remarks>
        /// GetUketsukeNumberメソッドを参考に、受付データ保持部分のみ抽出
        /// </remarks>
        /// <returns></returns>
        internal bool GetUketsukeData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.UKETSUKE_NUMBER.Text))
                {
                    return false;
                }

                // 受付（収集）からデータ取得
                DataTable dt = this.accessor.GetUketsukeSS(this.form.UKETSUKE_NUMBER.Text);
                if (dt.Rows.Count == 0)
                {
                    // データがなかったら受付（出荷）からデータ取得
                    dt = this.accessor.GetUketsukeSK(this.form.UKETSUKE_NUMBER.Text);
                    if (dt.Rows.Count == 0)
                    {
                        dt = this.accessor.GetUketsukeMK(this.form.UKETSUKE_NUMBER.Text);
                        if (dt.Rows.Count == 0)
                        {
                            // 処理終了
                            return false;
                        }
                    }
                    else if (!isOpenedScreenHaishaJokyoCd(dt))
                    {
                        return false;
                    }
                    else
                    {
                        // 登録時に配車状況を変更するために出荷受付入力エンティティを保存
                        var systemId = dt.Rows[0]["SYSTEM_ID"].ToString();
                        var seq = dt.Rows[0]["SEQ"].ToString();
                        this.tUketsukeSkEntry = this.accessor.GetUketsukeSkEntry(systemId, seq);
                    }
                }
                else if (!isOpenedScreenHaishaJokyoCd(dt))
                {
                    return false;
                }
                else
                {
                    // 登録時に配車状況を変更するために収集受付入力エンティティを保存
                    var systemId = dt.Rows[0]["SYSTEM_ID"].ToString();
                    var seq = dt.Rows[0]["SEQ"].ToString();
                    this.tUketsukeSsEntry = this.accessor.GetUketsukeSsEntry(systemId, seq);
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("GetUketsukeNumber", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("GetUketsukeNumber", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        #region 税区分、税計算区分、取引区分をセット

        /// <summary>
        /// 税区分、税計算区分、取引区分をセット
        /// </summary>
        public bool zeiKbnChanged()
        {
            if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType && this.dto.entryEntity != null)
            {
                this.form.denpyouHakouPopUpDTO.Seikyu_Zeikeisan_Kbn = Convert.ToString(this.dto.entryEntity.URIAGE_ZEI_KEISAN_KBN_CD);
                this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn = Convert.ToString(this.dto.entryEntity.URIAGE_ZEI_KBN_CD);
                this.form.denpyouHakouPopUpDTO.Seikyu_Rohiki_Kbn = Convert.ToString(this.dto.entryEntity.URIAGE_TORIHIKI_KBN_CD);
                this.form.denpyouHakouPopUpDTO.Shiharai_Zeikeisan_Kbn = Convert.ToString(this.dto.entryEntity.SHIHARAI_ZEI_KEISAN_KBN_CD);
                this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn = Convert.ToString(this.dto.entryEntity.SHIHARAI_ZEI_KBN_CD);
                this.form.denpyouHakouPopUpDTO.Shiharai_Rohiki_Kbn = Convert.ToString(this.dto.entryEntity.SHIHARAI_TORIHIKI_KBN_CD);

                if (!this.form.TORIHIKISAKI_CD.Text.Equals(this.dto.entryEntity.TORIHIKISAKI_CD.ToString()))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    DialogResult dr = msgLogic.MessageBoxShow("C105", "取引先CD", "税計算区分", "税区分", "取引区分");
                    if (dr == DialogResult.OK || dr == DialogResult.Yes)
                    {
                        // 取引先請求情報
                        var torihikisakiSeikyuuEntity = this.accessor.GetTorihikisakiSeikyuu(this.form.TORIHIKISAKI_CD.Text);

                        if (torihikisakiSeikyuuEntity != null)
                        {
                            this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn = torihikisakiSeikyuuEntity.ZEI_KBN_CD.ToString();
                            this.form.denpyouHakouPopUpDTO.Seikyu_Zeikeisan_Kbn = torihikisakiSeikyuuEntity.ZEI_KEISAN_KBN_CD.ToString();
                            this.form.denpyouHakouPopUpDTO.Seikyu_Rohiki_Kbn = torihikisakiSeikyuuEntity.TORIHIKI_KBN_CD.ToString();
                        }
                        // 取引先支払情報
                        var torihikisakiShiharaiEntity = this.accessor.GetTorihikisakiShiharai(this.form.TORIHIKISAKI_CD.Text);

                        if (torihikisakiShiharaiEntity != null)
                        {
                            this.form.denpyouHakouPopUpDTO.Shiharai_Zei_Kbn = torihikisakiShiharaiEntity.ZEI_KBN_CD.ToString();
                            this.form.denpyouHakouPopUpDTO.Shiharai_Zeikeisan_Kbn = torihikisakiShiharaiEntity.ZEI_KEISAN_KBN_CD.ToString();
                            this.form.denpyouHakouPopUpDTO.Shiharai_Rohiki_Kbn = torihikisakiShiharaiEntity.TORIHIKI_KBN_CD.ToString();
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return true;
        }

        #endregion

        #region 連携チェック
        internal bool RenkeiCheck(string uketsukeNum)
        {
            try
            {
                if (string.IsNullOrEmpty(uketsukeNum))
                {
                    return true;
                }

                DataTable dt = this.mobisyoRtDao.GetRenkeiData("1", uketsukeNum);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["UKETSUKE_KBN"].ToString().Equals("1"))
                    {

                        this.form.errmessage.MessageBoxShow("E262", "現在回収中", "完了後、実績取込にて、売上/支払データを作成");
                    }
                    else if (dt.Rows[0]["UKETSUKE_KBN"].ToString().Equals("2"))
                    {
                        this.form.errmessage.MessageBoxShow("E262", "現在運搬中", "完了後、実績取込にて、売上/支払データを作成");
                    }
                    return false;
                }

                // ロジこんぱす連携済みであるかをチェックする。
                string selectStr;
                selectStr = "SELECT DISTINCT LLS.* FROM T_LOGI_LINK_STATUS LLS "
                    + "LEFT JOIN T_LOGI_DELIVERY_DETAIL LDD on LDD.SYSTEM_ID = LLS.SYSTEM_ID and LDD.DELETE_FLG = 0";
                selectStr += " WHERE LDD.DENPYOU_ATTR != 3"  // 3：定期以外
                    + " and LDD.REF_DENPYOU_NO = " + uketsukeNum
                    + " and LLS.LINK_STATUS <> 3"  // 「3：受信済」以外
                    + " and LLS.DELETE_FLG = 0";

                // データ取得
                dt = this.dao.GetDateForStringSql(selectStr);
                // 連携済みの場合はアラートを表示する。
                if (dt.Rows.Count > 0)
                {
                    this.form.errmessage.MessageBoxShow("E261", "ロジこんぱす連携中", "呼出し");
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("RenkeiCheck", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RenkeiCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }

            return true;
        }
        #endregion

        #region 車輌登録
        /// <summary>
        /// 
        /// </summary>
        internal void SharyouTouroku()
        {
            if (this.IsInputSharyouRequest())
            {
                return;
            }
            var sharyouEntity = this.accessor.GetSharyouByCd(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text);
            if (sharyouEntity != null)
            {
                this.form.errmessage.MessageBoxShowError("入力された車輌CDはすでに登録されています。\n運搬業者CD：" + this.form.UNPAN_GYOUSHA_CD.Text + "、車輌CD：" + this.form.SHARYOU_CD.Text);
                return;
            }
            else
            {
                M_SHARYOU createEntity = new M_SHARYOU();
                createEntity.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                createEntity.SHARYOU_CD = this.form.SHARYOU_CD.Text;
                createEntity.SHARYOU_NAME = this.form.SHARYOU_NAME_RYAKU.Text;
                createEntity.SHARYOU_NAME_RYAKU = this.form.SHARYOU_NAME_RYAKU.Text;
                createEntity.SHASYU_CD = this.form.SHASHU_CD.Text;
                createEntity.SHAIN_CD = this.form.UNTENSHA_CD.Text;
                createEntity.HAISHA_WARIATE = false;
                createEntity.KEIRYOU_GYOUSHA_SET_KBN = false;
                if (this.form.GYOUSHA_CD.Text.Equals(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    createEntity.KEIRYOU_GYOUSHA_SET_KBN = true;
                }
                var dataBinderSharyouEntry = new DataBinderLogic<M_SHARYOU>(createEntity);
                dataBinderSharyouEntry.SetSystemProperty(createEntity, false);

                using (Transaction tran = new Transaction())
                {
                    this.accessor.InsertSharyou(createEntity);
                    // コミット
                    tran.Commit();
                }
                this.form.errmessage.MessageBoxShowInformation("登録が完了しました");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsInputSharyouRequest()
        {
            bool isOk = false;
            StringBuilder sb = new StringBuilder();
            this.form.UNPAN_GYOUSHA_CD.BackColor = Constans.NOMAL_COLOR;
            this.form.SHARYOU_CD.BackColor = Constans.NOMAL_COLOR;
            this.form.SHARYOU_NAME_RYAKU.BackColor = Constans.NOMAL_COLOR;

            if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                sb.AppendLine("運搬業者CDは必須項目です。入力してください。");
                this.form.UNPAN_GYOUSHA_CD.BackColor = Constans.ERROR_COLOR;
            }
            if (string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
            {
                sb.AppendLine("車輌CDは必須項目です。入力してください。");
                this.form.SHARYOU_CD.BackColor = Constans.ERROR_COLOR;
            }
            if (string.IsNullOrEmpty(this.form.SHARYOU_NAME_RYAKU.Text))
            {
                sb.AppendLine("車輌名は必須項目です。入力してください。");
                this.form.SHARYOU_NAME_RYAKU.BackColor = Constans.ERROR_COLOR;
            }

            if (!string.IsNullOrEmpty(sb.ToString()))
            {
                this.form.errmessage.MessageBoxShowError(sb.ToString());
                if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    this.form.UNPAN_GYOUSHA_CD.Focus();
                }
                else if (string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    this.form.SHARYOU_CD.Focus();
                }
                else if (string.IsNullOrEmpty(this.form.SHARYOU_NAME_RYAKU.Text))
                {
                    this.form.SHARYOU_NAME_RYAKU.Focus();
                }
                isOk = true;
            }
            return isOk;
        }
        #endregion

        //PhuocLoc 2020/12/01 #136221 -Start
        /// <summary>
        /// 集計項目の表示（現場マスタ、業者マスタ、取引先マスタを元に）
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <param name="gyoushaCd"></param>
        /// <param name="torihikisakiCd"></param>
        internal void SetShuukeiKomoku(string genbaCd, string gyoushaCd, string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(genbaCd, gyoushaCd, torihikisakiCd);
            M_GENBA genbaEntity = new M_GENBA();
            M_SHUUKEI_KOUMOKU shuukeiKoumokuEntity = new M_SHUUKEI_KOUMOKU();
            string shuukeiKomokuCd = null;
            if (!string.IsNullOrEmpty(gyoushaCd))
            {
                // 業者CD入力あり
                if (!string.IsNullOrEmpty(genbaCd))
                {
                    bool catchErr = true;
                    var retData = accessor.GetGenba(gyoushaCd, genbaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                    // 現場CD入力あり
                    genbaEntity = retData;
                    if (genbaEntity != null)
                    {
                        // コードに対応する現場マスタが存在する
                        shuukeiKomokuCd = genbaEntity.SHUUKEI_ITEM_CD;
                        if (!string.IsNullOrEmpty(shuukeiKomokuCd))
                        {
                            // 現場マスタに集計項目の設定がある場合
                            shuukeiKoumokuEntity = this.accessor.GetShuukeiKomoku(shuukeiKomokuCd);
                            if (shuukeiKoumokuEntity != null)
                            {
                                // 現場CDで取得した現場マスタの集計項目コードで、集計項目マスタを取得できた場合
                                if (!string.IsNullOrEmpty(shuukeiKoumokuEntity.SHUUKEI_KOUMOKU_NAME_RYAKU))
                                {
                                    // 取得した集計項目マスタの集計項目名略が設定されている場合
                                    this.form.SHUUKEI_KOUMOKU_CD.Text = shuukeiKoumokuEntity.SHUUKEI_KOUMOKU_CD;
                                    this.form.SHUUKEI_KOUMOKU_NAME.Text = shuukeiKoumokuEntity.SHUUKEI_KOUMOKU_NAME_RYAKU;
                                }
                                else
                                {
                                    // 取得した社員マスタの社員名略が設定されていない場合
                                    GetShuukeiKomokuOfGyousha(gyoushaCd, torihikisakiCd);
                                }
                            }
                            else
                            {
                                // 現場CDで取得した現場マスタの営業担当者コードで、社員マスタを取得できない場合
                                GetShuukeiKomokuOfGyousha(gyoushaCd, torihikisakiCd);
                            }
                        }
                        else
                        {
                            // 現場マスタに営業担当者の設定がない場合
                            GetShuukeiKomokuOfGyousha(gyoushaCd, torihikisakiCd);
                        }
                    }
                }
                else
                {
                    // 現場CD入力なし
                    GetShuukeiKomokuOfGyousha(gyoushaCd, torihikisakiCd);
                }
            }
            else
            {
                // 業者CD入力なし
                GetShuukeiKomokuOfTorihikisaki(torihikisakiCd);
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者マスタの集計項目コードからの集計項目取得(業者CD入力あり、業者マスタに存在することが前提)
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="torihikisakiCd"></param>
        private void GetShuukeiKomokuOfGyousha(string gyoushaCd, string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, torihikisakiCd);
            M_GYOUSHA gyoushaEntity = new M_GYOUSHA();
            M_SHUUKEI_KOUMOKU shuukeiKoumokuEntity = new M_SHUUKEI_KOUMOKU();
            string shuukeiKomokuCd = null;
            bool catchErr = true;
            var retData = this.accessor.GetGyousha(gyoushaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
            if (catchErr)
            {
                throw new Exception("");
            }
            gyoushaEntity = retData;
            if (gyoushaEntity != null)
            {
                // コードに対応する業者マスタが存在する
                shuukeiKomokuCd = gyoushaEntity.SHUUKEI_ITEM_CD;
                if (!string.IsNullOrEmpty(shuukeiKomokuCd))
                {
                    // 業者マスタに集計項目の設定がある場合
                    shuukeiKoumokuEntity = this.accessor.GetShuukeiKomoku(shuukeiKomokuCd);
                    if (shuukeiKoumokuEntity != null)
                    {
                        // 業者CDで取得した業者マスタの集計項目コードで、集計項目マスタを取得できた場合
                        if (!string.IsNullOrEmpty(shuukeiKoumokuEntity.SHUUKEI_KOUMOKU_NAME_RYAKU))
                        {
                            // 取得した社員マスタの集計項目名略が設定されている場合
                            this.form.SHUUKEI_KOUMOKU_CD.Text = shuukeiKoumokuEntity.SHUUKEI_KOUMOKU_CD;
                            this.form.SHUUKEI_KOUMOKU_NAME.Text = shuukeiKoumokuEntity.SHUUKEI_KOUMOKU_NAME_RYAKU;
                        }
                        else
                        {
                            // 取得した集計項目マスタの集計項目名略が設定されていない場合
                            GetShuukeiKomokuOfTorihikisaki(torihikisakiCd);
                        }
                    }
                    else
                    {
                        // 業者CDで取得した業者マスタの集計項目コードで、集計項目マスタを取得できない場合
                        GetShuukeiKomokuOfTorihikisaki(torihikisakiCd);
                    }
                }
                else
                {
                    // 業者マスタに集計項目の設定がない場合
                    GetShuukeiKomokuOfTorihikisaki(torihikisakiCd);
                }
            }
            else
            {
                return;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先マスタの集計項目コードからの集計項目取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        private void GetShuukeiKomokuOfTorihikisaki(string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(torihikisakiCd);
            M_TORIHIKISAKI torihikisakiEntity = new M_TORIHIKISAKI();
            M_SHUUKEI_KOUMOKU shuukeiKoumokuEntity = new M_SHUUKEI_KOUMOKU();
            string shuukeiKomokuCd = null;
            if (!string.IsNullOrEmpty(torihikisakiCd))
            {
                // 取引先CD入力あり
                bool catchErr = true;
                torihikisakiEntity = this.accessor.GetTorihikisaki(torihikisakiCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out catchErr);
                if (catchErr)
                {
                    return;
                }
                if (torihikisakiEntity != null)
                {
                    // コードに対応する取引先マスタが存在する
                    shuukeiKomokuCd = torihikisakiEntity.SHUUKEI_ITEM_CD;
                    if (!string.IsNullOrEmpty(shuukeiKomokuCd))
                    {
                        // 取引先マスタに集計項目の設定がある場合
                        shuukeiKoumokuEntity = this.accessor.GetShuukeiKomoku(shuukeiKomokuCd);
                        if (shuukeiKoumokuEntity != null)
                        {
                            // 取引先CDで取得した取引先マスタの集計項目コードで、集計項目マスタを取得できた場合
                            if (!string.IsNullOrEmpty(shuukeiKoumokuEntity.SHUUKEI_KOUMOKU_NAME_RYAKU))
                            {
                                // 取得した集計項目マスタの集計項目名略が設定されている場合
                                this.form.SHUUKEI_KOUMOKU_CD.Text = shuukeiKoumokuEntity.SHUUKEI_KOUMOKU_CD;
                                this.form.SHUUKEI_KOUMOKU_NAME.Text = shuukeiKoumokuEntity.SHUUKEI_KOUMOKU_NAME_RYAKU;
                            }
                            else
                            {
                                // 取得した集計項目マスタの集計項目名略が設定されていない場合
                                this.form.SHUUKEI_KOUMOKU_CD.Text = string.Empty;
                                this.form.SHUUKEI_KOUMOKU_NAME.Text = string.Empty;
                            }
                        }
                        else
                        {
                            // 取引先CDで取得した取引先マスタの集計項目コードで、集計項目マスタを取得できない場合
                            this.form.SHUUKEI_KOUMOKU_CD.Text = string.Empty;
                            this.form.SHUUKEI_KOUMOKU_NAME.Text = string.Empty;
                        }
                    }
                    else
                    {
                        // 取引先マスタに集計項目の設定がない場合
                        this.form.SHUUKEI_KOUMOKU_CD.Text = string.Empty;
                        this.form.SHUUKEI_KOUMOKU_NAME.Text = string.Empty;
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                // 取引先CD入力なし
                this.form.SHUUKEI_KOUMOKU_CD.Text = string.Empty;
                this.form.SHUUKEI_KOUMOKU_NAME.Text = string.Empty;
            }
            LogUtility.DebugMethodEnd();
        }
        //PhuocLoc 2020/12/01 #136221 -End

        #region 予約状況更新処理 refs #158909

        /// <summary>
        /// 持込受付入力の予約状況を更新します
        /// </summary>
        /// <param name="yoyakuJokyoCd">予約状況CD</param>
        /// <param name="yoyakuJokyoName">予約状況</param>
        private void UpdateYoyakuJokyo(string yoyakuJokyoCd, string yoyakuJokyoName)
        {
            LogUtility.DebugMethodStart(yoyakuJokyoCd, yoyakuJokyoName);

            // 予約状況が変更されているときだけ更新する
            var newEntryEntity = this.CreateTUketsukeMkEntry(this.tUketsukeMkEntry);
            newEntryEntity.YOYAKU_JOKYO_CD = Int16.Parse(yoyakuJokyoCd);
            newEntryEntity.YOYAKU_JOKYO_NAME = yoyakuJokyoName;
            this.accessor.InsertUketsukeMkEntry(newEntryEntity);

            // もとのエンティティを削除する
            this.DeleteTUketsukeMkEntry(this.tUketsukeMkEntry);

            // 子の持込受付詳細を更新する
            var tUketsukeMkDetailList = this.accessor.GetUketsukeMkDetail(this.tUketsukeMkEntry.SYSTEM_ID.ToString(), this.tUketsukeMkEntry.SEQ.ToString());
            foreach (var tUketsukeMkDetail in tUketsukeMkDetailList)
            {
                var newDetailEntity = this.CreateTUketsukeMkDetail(newEntryEntity, tUketsukeMkDetail);
                this.accessor.InsertUketsukeMkDetail(newDetailEntity);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 渡された持込受付入力エンティティをコピーして新しいエンティティを作成します
        /// </summary>
        /// <param name="entity">元になるエンティティ</param>
        /// <returns>作成したエンティティ</returns>
        private T_UKETSUKE_MK_ENTRY CreateTUketsukeMkEntry(T_UKETSUKE_MK_ENTRY entity)
        {
            LogUtility.DebugMethodStart(entity);

            // 予約状況を変更したエンティティを作成
            var newEntity = new T_UKETSUKE_MK_ENTRY();
            MasterUtility.CopyProperties(entity, newEntity);
            var dbLogic = new DataBinderLogic<T_UKETSUKE_MK_ENTRY>(newEntity);
            dbLogic.SetSystemProperty(newEntity, false);
            newEntity.SEQ = entity.SEQ + 1;
            newEntity.CREATE_USER = entity.CREATE_USER;
            newEntity.CREATE_DATE = entity.CREATE_DATE;
            newEntity.CREATE_PC = entity.CREATE_PC;

            LogUtility.DebugMethodEnd(newEntity);

            return newEntity;
        }

        /// <summary>
        /// 渡された持込受付入力エンティティを削除します
        /// </summary>
        /// <param name="entity">削除するエンティティ</param>
        private void DeleteTUketsukeMkEntry(T_UKETSUKE_MK_ENTRY entity)
        {
            LogUtility.DebugMethodStart(entity);

            var dbLogic = new DataBinderLogic<T_UKETSUKE_MK_ENTRY>(entity);
            dbLogic.SetSystemProperty(entity, true);
            entity.DELETE_FLG = true;
            this.accessor.UpdateUketsukeMkEntry(entity);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 渡された持込受付詳細エンティティをコピーして新しいエンティティを作成します
        /// </summary>
        /// <param name="entryEntity">親になる持込受付入力エンティティ</param>
        /// <param name="detailEntity">元になる持込受付詳細エンティティ</param>
        /// <returns>作成したエンティティ</returns>
        private T_UKETSUKE_MK_DETAIL CreateTUketsukeMkDetail(T_UKETSUKE_MK_ENTRY entryEntity, T_UKETSUKE_MK_DETAIL detailEntity)
        {
            LogUtility.DebugMethodStart(entryEntity, detailEntity);

            var newEntity = new T_UKETSUKE_MK_DETAIL();
            MasterUtility.CopyProperties(detailEntity, newEntity);
            var dbLogic = new DataBinderLogic<T_UKETSUKE_MK_DETAIL>(newEntity);
            dbLogic.SetSystemProperty(newEntity, false);
            newEntity.SEQ = entryEntity.SEQ;
            newEntity.CREATE_USER = entryEntity.CREATE_USER;
            newEntity.CREATE_DATE = entryEntity.CREATE_DATE;
            newEntity.CREATE_PC = entryEntity.CREATE_PC;

            LogUtility.DebugMethodEnd(newEntity);

            return newEntity;
        }

        /// <summary>
        /// 取得済みの持込受付詳細エンティティをクリアします
        /// </summary>
        internal void ClearTUketsukeMkEntry()
        {
            this.tUketsukeMkEntry = null;
        }
        #endregion

        /// <summary>
        /// CalDenpyouRireki
        /// </summary>
        internal void CalDenpyouRireki()
        {
            LogUtility.DebugMethodStart();
            DenpyouRirekiDTOClass dto = new DenpyouRirekiDTOClass();
            dto.TorihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
            dto.GyoushaCd = this.form.GYOUSHA_CD.Text;
            dto.GenbaCd = this.form.GENBA_CD.Text;
            dto.UnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
            dto.SharyouCd = this.form.SHARYOU_CD.Text;
            dto.SharyouName = this.form.SHARYOU_NAME_RYAKU.Text;
            dto.FormId = "G054";
            var result = FormManager.OpenFormModal("G761", dto);
            if (!string.IsNullOrEmpty(Convert.ToString(dto.CopyMode)))
            {
                this.SetDataFromDenpyouRireki(dto);
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// SetDataFromDenpyouRireki
        /// </summary>
        /// <param name="dto"></param>
        internal void SetDataFromDenpyouRireki(DenpyouRirekiDTOClass dto)
        {
            LogUtility.DebugMethodStart(dto);
            bool Err = false;
            var entrys = accessor.GetUrshEntry(Convert.ToInt64(dto.DenpyouNumber), this.form.SEQ);
            T_UR_SH_ENTRY entry = null;
            T_UR_SH_DETAIL[] detail = null;
            long systemId = -1;
            int seq = -1;


            if (entrys.Length <= 0)
            {
                return;
            }
            entry = entrys[0];
            // 締処理状況判定用データ取得
            DataTable seikyuuData = this.accessor.GetSeikyuMeisaiData(systemId, seq, -1, entry.TORIHIKISAKI_CD);
            DataTable seisanData = this.accessor.GetSeisanMeisaiData(systemId, seq, -1, entry.TORIHIKISAKI_CD);

            if (!entry.SYSTEM_ID.IsNull) systemId = (long)entry.SYSTEM_ID;
            if (!entry.SEQ.IsNull) seq = (int)entry.SEQ;

            if (dto.CopyMode == "1")
            {
                this.form.DENPYOU_DATE.Value = this.footerForm.sysDate;
                this.form.URIAGE_DATE.Value = this.footerForm.sysDate;
                this.form.SHIHARAI_DATE.Value = this.footerForm.sysDate;

                //受付番号
                this.form.UKETSUKE_NUMBER.Text = string.Empty;

                //取引先
                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
                this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
                this.form.TORIHIKISAKI_NAME_RYAKU.Tag = string.Empty;
                this.form.SEIKYUU_SHIMEBI1.Text = string.Empty;
                this.form.SEIKYUU_SHIMEBI2.Text = string.Empty;
                this.form.SEIKYUU_SHIMEBI3.Text = string.Empty;
                this.form.SHIHARAI_SHIMEBI1.Text = string.Empty;
                this.form.SHIHARAI_SHIMEBI2.Text = string.Empty;
                this.form.SHIHARAI_SHIMEBI3.Text = string.Empty;
                this.form.txtUri.Text = string.Empty;
                this.form.txtShi.Text = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(entry.TORIHIKISAKI_CD)))
                {
                    var torihikisakiEntity = this.accessor.GetTorihikisaki(entry.TORIHIKISAKI_CD, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out Err);
                    if (torihikisakiEntity != null)
                    {
                        this.form.TORIHIKISAKI_CD.Text = entry.TORIHIKISAKI_CD;
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                        this.tmpTorihikisakiCd = torihikisakiEntity.TORIHIKISAKI_CD;

                        // 諸口区分チェック
                        if (torihikisakiEntity.SHOKUCHI_KBN.IsTrue)
                        {
                            // 取引先名編集可
                            this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME1;
                            this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = false;
                            this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = GetTabStop("TORIHIKISAKI_NAME_RYAKU");
                            this.form.TORIHIKISAKI_NAME_RYAKU.Tag = this.torihikisakiHintText;
                        }

                        // 請求締日チェック
                        this.CheckSeikyuuShimebi();

                        // 支払い締日チェック
                        this.CheckShiharaiShimebi();

                        //取引区分チェック
                        this.CheckTorihikiKBN();
                    }

                }

                //業者
                this.form.GYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_NAME_RYAKU.Text = String.Empty;
                this.form.GYOUSHA_NAME_RYAKU.ReadOnly = true;
                this.form.GYOUSHA_NAME_RYAKU.Tag = String.Empty;
                this.form.GYOUSHA_NAME_RYAKU.TabStop = false;

                this.form.GENBA_CD.Text = String.Empty;
                this.form.GENBA_NAME_RYAKU.Text = String.Empty;
                this.form.GENBA_NAME_RYAKU.ReadOnly = true;
                this.form.GENBA_NAME_RYAKU.Tag = String.Empty;
                this.form.GENBA_NAME_RYAKU.TabStop = false;
                strGenbaName = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(entry.GYOUSHA_CD)))
                {
                    var gyoushaEntity = this.accessor.GetGyousha(entry.GYOUSHA_CD, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out Err);

                    if (gyoushaEntity != null)
                    {
                        this.form.GYOUSHA_CD.Text = entry.GYOUSHA_CD;
                        this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;

                        // 諸口区分チェック
                        if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                        {
                            // 業者名編集可
                            this.form.GYOUSHA_NAME_RYAKU.ReadOnly = false;
                            this.form.GYOUSHA_NAME_RYAKU.TabStop = GetTabStop("GYOUSHA_NAME_RYAKU");
                            this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME1;
                            this.form.GYOUSHA_NAME_RYAKU.Tag = this.gyoushaHintText;
                        }
                    }
                }

                //現場
                if (!string.IsNullOrEmpty(Convert.ToString(entry.GENBA_CD)))
                {
                    var genbaEntityList = this.accessor.GetGenbaList(entry.GYOUSHA_CD, entry.GENBA_CD, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                    if (genbaEntityList.Length > 0)
                    {
                        M_GENBA genba = new M_GENBA();
                        genba = genbaEntityList[0];
                        this.form.GENBA_CD.Text = entry.GENBA_CD;
                        this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;
                        strGenbaName = genba.GENBA_NAME1 + genba.GENBA_NAME2;

                        // 現場：諸口区分チェック
                        if (genba.SHOKUCHI_KBN.IsTrue)
                        {
                            // 現場名編集可
                            this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME1;
                            this.form.GENBA_NAME_RYAKU.ReadOnly = false;
                            this.form.GENBA_NAME_RYAKU.TabStop = GetTabStop("GENBA_NAME_RYAKU");    // No.3822
                            this.form.GENBA_NAME_RYAKU.Tag = genbaHintText;
                        }

                        this.form.MANIFEST_SHURUI_CD.Text = string.Empty;
                        this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = string.Empty;

                        if (!genba.MANIFEST_SHURUI_CD.IsNull)
                        {
                            var manifestShuruiEntity = this.accessor.GetManifestShurui(genba.MANIFEST_SHURUI_CD);
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
                            var manifestTehaiEntity = this.accessor.GetManifestTehai(genba.MANIFEST_TEHAI_CD);
                            if (manifestTehaiEntity != null && !string.IsNullOrEmpty(manifestTehaiEntity.MANIFEST_TEHAI_NAME_RYAKU))
                            {
                                this.form.MANIFEST_TEHAI_CD.Text = Convert.ToString(genba.MANIFEST_TEHAI_CD);
                                this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = manifestTehaiEntity.MANIFEST_TEHAI_NAME_RYAKU;
                            }
                        }
                    }
                }

                //伝票備考
                this.form.DENPYOU_BIKOU.Text = entry.DENPYOU_BIKOU;

                //確定区分
                this.form.KAKUTEI_KBN.Text = this.dto.sysInfoEntity.UKEIRE_KAKUTEI_FLAG.ToString();
                this.form.KAKUTEI_KBN_NAME.Text = SalesPaymentConstans.GetKakuteiKbnName(Int16.Parse(this.form.KAKUTEI_KBN.Text));

                //入力担当者
                if (CommonShogunData.LOGIN_USER_INFO != null
                && !string.IsNullOrEmpty(CommonShogunData.LOGIN_USER_INFO.SHAIN_CD)
                && CommonShogunData.LOGIN_USER_INFO.NYUURYOKU_TANTOU_KBN)
                {
                    this.form.NYUURYOKU_TANTOUSHA_CD.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_CD.ToString();
                    this.form.NYUURYOKU_TANTOUSHA_NAME.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME_RYAKU.ToString();
                    strNyuryokuTantousyaName = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME.ToString();
                }
                else
                {
                    this.form.NYUURYOKU_TANTOUSHA_CD.Text = string.Empty;
                    this.form.NYUURYOKU_TANTOUSHA_NAME.Text = string.Empty;
                    strNyuryokuTantousyaName = string.Empty;
                    this.form.NYUURYOKU_TANTOUSHA_NAME.ReadOnly = true;
                }

                //運搬業者
                this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_NAME.ReadOnly = true;
                this.form.UNPAN_GYOUSHA_NAME.TabStop = false;
                this.form.UNPAN_GYOUSHA_NAME.Tag = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(entry.UNPAN_GYOUSHA_CD)))
                {
                    var gyousha = this.accessor.GetGyousha(entry.UNPAN_GYOUSHA_CD, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out Err);
                    if (gyousha != null)
                    {
                        if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                        {
                            this.form.UNPAN_GYOUSHA_CD.Text = gyousha.GYOUSHA_CD;
                            this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                            // 諸口区分チェック
                            if (gyousha.SHOKUCHI_KBN.IsTrue)
                            {
                                // 運搬業者名編集可
                                this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                                this.form.UNPAN_GYOUSHA_NAME.ReadOnly = false;
                                this.form.UNPAN_GYOUSHA_NAME.TabStop = GetTabStop("UNPAN_GYOUSHA_NAME");
                                this.form.UNPAN_GYOUSHA_NAME.Tag = this.unpanGyoushaHintText;
                            }
                        }
                    }
                }

                //車輌
                this.form.SHARYOU_CD.Text = entry.SHARYOU_CD;
                this.form.SHARYOU_NAME_RYAKU.Text = entry.SHARYOU_NAME;
                this.form.SHARYOU_CD.AutoChangeBackColorEnabled = true;
                this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                this.form.oldSharyouShokuchiKbn = false;
                bool retCheck = false;
                if (!string.IsNullOrEmpty(Convert.ToString(entry.SHARYOU_CD)))
                {
                    retCheck = this.SharyouDateCheck(false);
                    if (!retCheck)
                    {
                        this.form.SHARYOU_CD.Text = string.Empty;
                        this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                    }
                    else
                    {
                        sharyouCd = this.form.SHARYOU_CD.Text;
                        unpanGyousha = this.form.UNPAN_GYOUSHA_CD.Text;
                        var sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null, SqlDateTime.Parse(this.form.DENPYOU_DATE.Value.ToString()));

                        // マスタ存在チェック
                        if (sharyouEntitys == null || sharyouEntitys.Length < 1)
                        {
                            // 車輌名を編集可
                            this.ChangeShokuchiSharyouDesign(true);
                        }
                    }
                }

                //形態区分
                SqlInt16 KeitaiKbnCd = this.accessor.GetKeitaiKbnCd(SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI);
                if (KeitaiKbnCd > 0)
                {
                    this.form.KEITAI_KBN_CD.Text = KeitaiKbnCd.ToString();
                    this.form.KEITAI_KBN_NAME_RYAKU.Text = this.accessor.GetKeitaiKbnNameRyaku(KeitaiKbnCd);
                }
                else
                {
                    this.form.KEITAI_KBN_CD.Text = string.Empty;
                    this.form.KEITAI_KBN_NAME_RYAKU.Text = string.Empty;
                }

                //人数
                this.form.NINZUU_CNT.Text = string.Empty;
                if (!entry.NINZUU_CNT.IsNull)
                {
                    this.form.NINZUU_CNT.Text = entry.NINZUU_CNT.ToString();
                }

                //tab 請求支払
                this.SetUriageShouhizeiRate();
                this.SetShiharaiShouhizeiRate();

                //締状況(売上)	
                this.form.SHIMESHORI_JOUKYOU_URIAGE.Text = string.Empty;

                //締状況(支払)
                this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Text = string.Empty;

                //領収書番号(日連番)
                this.form.RECEIPT_NUMBER.Text = string.Empty;

                //荷降業者
                this.form.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
                this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                this.form.NIOROSHI_GYOUSHA_NAME.ReadOnly = true;
                this.form.NIOROSHI_GYOUSHA_NAME.Tag = string.Empty;
                this.form.NIOROSHI_GYOUSHA_NAME.TabStop = false;
                if (!string.IsNullOrEmpty(Convert.ToString(entry.NIOROSHI_GYOUSHA_CD)))
                {
                    var Nioroshigyousha = this.accessor.GetGyousha(entry.NIOROSHI_GYOUSHA_CD, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out Err);
                    if (Nioroshigyousha != null)
                    {
                        if (Nioroshigyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue || Nioroshigyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue)
                        {
                            this.form.NIOROSHI_GYOUSHA_CD.Text = Nioroshigyousha.GYOUSHA_CD;
                            // 荷卸業者名
                            this.form.NIOROSHI_GYOUSHA_NAME.Text = Nioroshigyousha.GYOUSHA_NAME_RYAKU;

                            if (Nioroshigyousha.SHOKUCHI_KBN.IsTrue)
                            {
                                this.form.NIOROSHI_GYOUSHA_NAME.Text = Nioroshigyousha.GYOUSHA_NAME1;
                                this.form.NIOROSHI_GYOUSHA_NAME.ReadOnly = false;
                                this.form.NIOROSHI_GYOUSHA_NAME.TabStop = true;
                                this.form.NIOROSHI_GYOUSHA_NAME.Tag = this.nioroshiGyoushaHintText;
                            }
                        }
                    }
                }

                //荷降現場
                this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                this.form.NIOROSHI_GENBA_NAME.ReadOnly = true;
                this.form.NIOROSHI_GENBA_NAME.TabStop = false;
                this.form.NIOROSHI_GENBA_NAME.Tag = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(entry.NIOROSHI_GENBA_CD)))
                {
                    retCheck = this.HannyuusakiDateCheck(false);
                    if (retCheck)
                    {
                        var genbaEntityList = this.accessor.GetGenbaList(entry.NIOROSHI_GYOUSHA_CD, entry.NIOROSHI_GENBA_CD, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                        M_GENBA genba = new M_GENBA();
                        if (genbaEntityList.Length > 0)
                        {
                            genba = genbaEntityList[0];
                            if (genba.TSUMIKAEHOKAN_KBN.IsTrue || genba.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || genba.SAISHUU_SHOBUNJOU_KBN.IsTrue)
                            {
                                this.form.NIOROSHI_GENBA_CD.Text = genba.GENBA_CD;
                                this.form.NIOROSHI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;

                                // 諸口区分チェック
                                if (genba.SHOKUCHI_KBN.IsTrue)
                                {
                                    // 荷卸現場名編集可
                                    this.form.NIOROSHI_GENBA_NAME.Text = genba.GENBA_NAME1;
                                    this.form.NIOROSHI_GENBA_NAME.ReadOnly = false;
                                    this.form.NIOROSHI_GENBA_NAME.TabStop = true;
                                    this.form.NIOROSHI_GENBA_NAME.Tag = this.nioroshiGenbaHintText;
                                }
                            }
                        }
                    }
                }


                //荷積業者
                this.form.NIZUMI_GYOUSHA_CD.Text = string.Empty;
                this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
                this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = true;
                this.form.NIZUMI_GYOUSHA_NAME.Tag = string.Empty;
                this.form.NIZUMI_GYOUSHA_NAME.TabStop = false;
                if (!string.IsNullOrEmpty(Convert.ToString(entry.NIZUMI_GYOUSHA_CD)))
                {
                    var Nizumigyousha = this.accessor.GetGyousha(entry.NIZUMI_GYOUSHA_CD, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date, out Err);
                    if (Nizumigyousha != null)
                    {
                        if (Nizumigyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue || Nizumigyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                        {
                            this.form.NIZUMI_GYOUSHA_CD.Text = Nizumigyousha.GYOUSHA_CD;
                            this.form.NIZUMI_GYOUSHA_NAME.Text = Nizumigyousha.GYOUSHA_NAME_RYAKU;

                            if (Nizumigyousha.SHOKUCHI_KBN.IsTrue)
                            {
                                this.form.NIZUMI_GYOUSHA_NAME.Text = Nizumigyousha.GYOUSHA_NAME1;
                                this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = false;
                                this.form.NIZUMI_GYOUSHA_NAME.TabStop = true;
                                this.form.NIZUMI_GYOUSHA_NAME.Tag = this.nizumiGyoushaHintText;
                            }
                        }
                    }
                }

                //荷積現場
                this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                this.form.NIZUMI_GENBA_NAME.ReadOnly = true;
                this.form.NIZUMI_GENBA_NAME.TabStop = false;
                this.form.NIZUMI_GENBA_NAME.Tag = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(entry.NIZUMI_GENBA_CD)))
                {
                    var genbaEntityList = this.accessor.GetGenbaList(entry.NIZUMI_GYOUSHA_CD, entry.NIZUMI_GENBA_CD, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                    M_GENBA genba = new M_GENBA();
                    if (genbaEntityList.Length > 0)
                    {
                        genba = genbaEntityList[0];
                        if (genba.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue || genba.TSUMIKAEHOKAN_KBN.IsTrue)
                        {
                            this.form.NIZUMI_GENBA_CD.Text = genba.GENBA_CD;
                            this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;

                            // 諸口区分チェック
                            if (genba.SHOKUCHI_KBN.IsTrue)
                            {
                                this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME1;
                                this.form.NIZUMI_GENBA_NAME.ReadOnly = false;
                                this.form.NIZUMI_GENBA_NAME.TabStop = true;
                                this.form.NIZUMI_GENBA_NAME.Tag = this.nizumiGenbaHintText;
                            }
                        }
                    }
                }

                //営業担当者
                this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
                this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(entry.EIGYOU_TANTOUSHA_CD)))
                {
                    var shainEntity = this.accessor.GetShain(entry.EIGYOU_TANTOUSHA_CD);
                    if (shainEntity != null)
                    {
                        this.form.EIGYOU_TANTOUSHA_CD.Text = shainEntity.SHAIN_CD;
                        this.form.EIGYOU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                    }
                }

                //集計項目
                this.form.SHUUKEI_KOUMOKU_CD.Text = string.Empty;
                this.form.SHUUKEI_KOUMOKU_NAME.Text = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(entry.MOD_SHUUKEI_KOUMOKU_CD)))
                {
                    var shuukeiKoumokuEntity = this.accessor.GetShuukeiKomoku(entry.MOD_SHUUKEI_KOUMOKU_CD);
                    if (shuukeiKoumokuEntity != null)
                    {
                        this.form.SHUUKEI_KOUMOKU_CD.Text = shuukeiKoumokuEntity.SHUUKEI_KOUMOKU_CD;
                        this.form.SHUUKEI_KOUMOKU_NAME.Text = shuukeiKoumokuEntity.SHUUKEI_KOUMOKU_NAME_RYAKU;
                    }
                }

                //車種
                this.form.SHASHU_CD.Text = string.Empty;
                this.form.SHASHU_NAME.Text = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(entry.SHASHU_CD)))
                {
                    var shashu = this.accessor.GetShashu(entry.SHASHU_CD);
                    if (shashu != null)
                    {
                        this.form.SHASHU_CD.Text = shashu.SHASHU_CD;
                        this.form.SHASHU_NAME.Text = shashu.SHASHU_NAME_RYAKU;
                    }
                }

                //運転者
                this.form.UNTENSHA_CD.Text = string.Empty;
                this.form.UNTENSHA_NAME.Text = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(entry.UNTENSHA_CD)))
                {
                    var shainEntity = this.accessor.GetShain(entry.UNTENSHA_CD);
                    if (shainEntity != null)
                    {
                        this.form.UNTENSHA_CD.Text = shainEntity.SHAIN_CD;
                        this.form.UNTENSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                    }
                }

                // コンテナ
                this.form.CONTENA_SOUSA_CD.Text = string.Empty;
                this.form.CONTENA_JOUKYOU_NAME_RYAKU.Text = string.Empty;

                this.dto.contenaEntity = new M_CONTENA_JOUKYOU();
                if (!entry.CONTENA_SOUSA_CD.IsNull)
                {
                    var contenaJoukyou = this.accessor.GetContenaJoukyou((short)entry.CONTENA_SOUSA_CD);
                    if (contenaJoukyou != null)
                    {
                        this.dto.contenaEntity = contenaJoukyou;
                        if (!this.dto.contenaEntity.CONTENA_JOUKYOU_CD.IsNull)
                        {
                            this.form.CONTENA_SOUSA_CD.Text = string.Format("{0:00}", (int)this.dto.contenaEntity.CONTENA_JOUKYOU_CD);
                        }
                        if (!string.IsNullOrEmpty(this.dto.contenaEntity.CONTENA_JOUKYOU_NAME_RYAKU))
                        {
                            this.form.CONTENA_JOUKYOU_NAME_RYAKU.Text = this.dto.contenaEntity.CONTENA_JOUKYOU_NAME_RYAKU.ToString();
                        }
                    }
                }

                // 2次
                // コンテナ稼働実績
                this.dto.contenaResultList = new List<T_CONTENA_RESULT>();
                var contenaResultEntity = this.accessor.GetContena(entry.SYSTEM_ID.ToString());
                if (contenaResultEntity != null)
                {
                    foreach (T_CONTENA_RESULT entity in contenaResultEntity)
                    {
                        this.dto.contenaResultList.Add(entity);
                    }
                }

                this.form.txtSecchi.Text = "";
                this.form.txtHikiage.Text = "";
                SqlInt16 sumSecchi = 0;
                SqlInt16 sumHikiage = 0;
                foreach (T_CONTENA_RESULT entity in this.dto.contenaResultList)
                {
                    if (entity.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI
                        && entity.DENSHU_KBN_CD == SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI
                        && entity.DELETE_FLG.Equals(SqlBoolean.False))
                    {
                        //設置
                        if (!entity.DAISUU_CNT.Equals(SqlInt16.Null))
                        {
                            sumSecchi += entity.DAISUU_CNT;
                        }
                    }
                    else if (entity.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE
                             && entity.DENSHU_KBN_CD == SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI
                             && entity.DELETE_FLG.Equals(SqlBoolean.False))
                    {
                        //引揚
                        if (!entity.DAISUU_CNT.Equals(SqlInt16.Null))
                        {
                            sumHikiage += entity.DAISUU_CNT;
                        }
                    }
                }
                this.form.txtSecchi.Text = sumSecchi.ToString();
                this.form.txtHikiage.Text = sumHikiage.ToString();

                //get tanka
                var list = this.form.mrwDetail.Rows.Where(r => !r.IsNewRow).ToList();
                foreach (Row dr in list)
                {
                    this.SearchAndCalcForUnit(false, dr);
                    this.form.SetIchranReadOnly(dr.Index);
                }

                this.CalcTotalValues();

            }
            else if (dto.CopyMode == "2")
            {
                //get tanka
                var dlg = this.form.errmessage.MessageBoxShowConfirm("単価を読み直しますか？");

                detail = accessor.GetUrshDetail(entry.SYSTEM_ID, entry.SEQ);

                this.form.mrwDetail.BeginEdit(false);
                this.form.mrwDetail.Rows.Clear();
                // CreateDataTableForEntityがMultiRowを動的に作成しないため、ここでEntity分行数を追加する
                //// Entity数Rowを作ると最終行が無いので、Entity + 1でループさせる
                for (int i = 1; i < detail.Length + 1; i++)
                {
                    this.form.mrwDetail.Rows.Add();
                }
                var dataBinder = new DataBinderLogic<T_UR_SH_DETAIL>(detail);
                dataBinder.CreateDataTableForEntity(this.form.mrwDetail, detail);

                int k = 0;
                foreach (var row in this.form.mrwDetail.Rows)
                {
                    if (k < detail.Length)
                    {
                        short denpyouCd = 0;
                        ICustomControl denpyouCdCell = (ICustomControl)row.Cells[CELL_NAME_DENPYOU_KBN_CD];
                        if (short.TryParse(denpyouCdCell.GetResultText(), out denpyouCd)
                            && denpyouKbnDictionary.ContainsKey(denpyouCd))
                        {
                            row.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = denpyouKbnDictionary[denpyouCd].DENPYOU_KBN_NAME_RYAKU;
                        }

                        short unitCd = 0;
                        ICustomControl unitCdCell = (ICustomControl)row.Cells[CELL_NAME_UNIT_CD];
                        if (short.TryParse(unitCdCell.GetResultText(), out unitCd)
                            && unitDictionary.ContainsKey(unitCd))
                        {
                            row.Cells[CELL_NAME_UNIT_NAME_RYAKU].Value = unitDictionary[unitCd].UNIT_NAME_RYAKU;
                        }

                        T_UR_SH_DETAIL ditail = detail[k];
                        // 金額
                        decimal kintaku = 0;
                        decimal hinmeiKingaku = 0;
                        decimal.TryParse(Convert.ToString(ditail.KINGAKU), out kintaku);
                        decimal.TryParse(Convert.ToString(ditail.HINMEI_KINGAKU), out hinmeiKingaku);
                        row.Cells[CELL_NAME_KINGAKU].Value = kintaku + hinmeiKingaku;

                        // 確定区分
                        if (ditail.KAKUTEI_KBN == SalesPaymentConstans.KAKUTEI_KBN_KAKUTEI)
                        {
                            row.Cells[CELL_NAME_KAKUTEI_KBN].Value = true;
                        }
                        else
                        {
                            row.Cells[CELL_NAME_KAKUTEI_KBN].Value = false;
                        }

                        // 締処理状況設定
                        string whereStr = string.Empty;
                        whereStr = "DETAIL_SYSTEM_ID = " + row.Cells[CELL_NAME_DETAIL_SYSTEM_ID].Value;
                        DataRow[] dtRow = null;

                        if (SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE == denpyouCd)
                        {
                            dtRow = seikyuuData.Select(whereStr);
                            if (dtRow != null && 0 < dtRow.Length)
                            {
                                row.Cells[CELL_NAME_JOUKYOU].Value = SalesPaymentConstans.SHIMEZUMI_DETAIL;
                            }
                            else
                            {
                                row.Cells[CELL_NAME_JOUKYOU].Value = SalesPaymentConstans.MISHIME_DETAIL;
                            }
                        }
                        else if (SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI == denpyouCd)
                        {
                            dtRow = seisanData.Select(whereStr);
                            if (dtRow != null && 0 < dtRow.Length)
                            {
                                row.Cells[CELL_NAME_JOUKYOU].Value = SalesPaymentConstans.SHIMEZUMI_DETAIL;
                            }
                            else
                            {
                                row.Cells[CELL_NAME_JOUKYOU].Value = SalesPaymentConstans.MISHIME_DETAIL;
                            }
                        }
                    }

                    row.Cells[CELL_NAME_MANIFEST_ID].Value = string.Empty;
                    row.Cells[CELL_NAME_SUURYOU].Value = null;
                    row.Cells[CELL_NAME_DETAIL_SYSTEM_ID].Value = null;
                    row.Cells[CELL_NAME_KINGAKU].Value = null;
                    this.form.SetIchranReadOnly(row.Index);
                    k++;
                }

                this.form.mrwDetail.EndEdit();
                this.form.mrwDetail.NotifyCurrentCellDirty(false);
                SelectionActions.MoveToFirstCell.Execute(this.form.mrwDetail);

                //締処理状況(売上)
                this.form.SHIMESHORI_JOUKYOU_URIAGE.Text = string.Empty;

                //締処理状況(支払)
                this.form.SHIMESHORI_JOUKYOU_SHIHARAI.Text = string.Empty;

                //領収書番号(日連番)
                this.form.RECEIPT_NUMBER.Text = string.Empty;

                //売上金額合計
                this.form.URIAGE_AMOUNT_TOTAL.Text = "0";

                //支払金額合計
                this.form.SHIHARAI_KINGAKU_TOTAL.Text = "0";

                //差引額
                this.form.SAGAKU.Text = "0";

                //get tanka
                if (dlg == DialogResult.Yes)
                {
                    var list = this.form.mrwDetail.Rows.Where(r => !r.IsNewRow).ToList();
                    foreach (Row dr in list)
                    {
                        this.SearchAndCalcForUnit(false, dr);
                        this.form.SetIchranReadOnly(dr.Index);
                    }
                }

                this.CalcTotalValues();
            }

            LogUtility.DebugMethodEnd();
        }

        // MAILAN #158993 START
        internal void ResetTankaCheck()
        {
            this.isTankaMessageShown = false;
            this.isCheckTankaFromChild = false;
        }
        // MAILAN #158993 END
    }
}
