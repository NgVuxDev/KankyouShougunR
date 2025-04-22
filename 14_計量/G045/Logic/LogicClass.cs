using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Function.ShougunCSCommon.Utility;
using Shougun.Core.Scale.Keiryou.Accessor;
using Shougun.Core.Scale.Keiryou.Dao;
using Shougun.Core.Scale.Keiryou;
using Shougun.Core.Scale.Keiryou.Const;
using Shougun.Core.Scale.Keiryou.Dto;
using Shougun.Core.Scale.Keiryou.Utility;
using CommonChouhyouPopup.App;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Xml;

using System.Text;
using Shougun.Function.ShougunCSCommon.Const;
using r_framework.Dao;

namespace Shougun.Core.Scale.Keiryou
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Scale.Keiryou.Setting.ButtonSetting.xml";

        /// <summary>
        /// 日連番更新区分
        /// </summary>
        private KeiryouConstans.REGIST_KBN hiRenbanRegistKbn { get; set; }

        /// <summary>
        /// 年連番更新区分
        /// </summary>
        private KeiryouConstans.REGIST_KBN nenRenbanRegistKbn { get; set; }

        /// <summary>
        /// ROW_NO
        /// </summary>
        internal const string CELL_NAME_ROW_NO = "ROW_NO";

        /// <summary>総重量</summary>
        internal const string CELL_NAME_STAK_JYUURYOU = "STACK_JYUURYOU";

        /// <summary>空車重量</summary>
        internal const string CELL_NAME_EMPTY_JYUURYOU = "EMPTY_JYUURYOU";

        /// <summary>割振重量</summary>
        internal const string CELL_NAME_WARIFURI_JYUURYOU = "WARIFURI_JYUURYOU";

        /// <summary>割振(%)</summary>
        internal const string CELL_NAME_WARIFURI_PERCENT = "WARIFURI_PERCENT";

        /// <summary>調整重量</summary>
        internal const string CELL_NAME_CHOUSEI_JYUURYOU = "CHOUSEI_JYUURYOU";

        /// <summary>調整(%)</summary>
        internal const string CELL_NAME_CHOUSEI_PERCENT = "CHOUSEI_PERCENT";

        /// <summary>容器kg</summary>
        internal const string CELL_NAME_YOUKI_JYUURYOU = "YOUKI_JYUURYOU";

        /// <summary>容器数量</summary>
        internal const string CELL_NAME_YOUKI_SUURYOU = "YOUKI_SUURYOU";

        /// <summary>容器CD</summary>
        internal const string CELL_NAME_YOUKI_CD = "YOUKI_CD";

        /// <summary>容器名</summary>
        internal const string CELL_NAME_YOUKI_NAME_RYAKU = "YOUKI_NAME_RYAKU";

        /// <summary>品名CD</summary>
        internal const string CELL_NAME_HINMEI_CD = "HINMEI_CD";

        /// <summary>品名</summary>
        internal const string CELL_NAME_HINMEI_NAME = "HINMEI_NAME";

        /// <summary>伝票区分CD</summary>
        internal const string CELL_NAME_DENPYOU_KBN_CD = "DENPYOU_KBN_CD";

        /// <summary>伝票区分名</summary>
        internal const string CELL_NAME_DENPYOU_KBN_NAME = "DENPYOU_KBN_NAME";

        /// <summary>正味重量</summary>
        internal const string CELL_NAME_NET_JYUURYOU = "NET_JYUURYOU";

        /// <summary>荷姿数量</summary>
        internal const string CELL_NAME_NISUGATA_SUURYOU = "NISUGATA_SUURYOU";

        /// <summary>荷姿単位CD</summary>
        internal const string CELL_NAME_NISUGATA_UNIT_CD = "NISUGATA_UNIT_CD";

        /// <summary>荷姿単位名称</summary>
        internal const string CELL_NAME_NISUGATA_NAME_RYAKU = "NISUGATA_NAME_RYAKU";

        /// <summary>システムID</summary>
        internal const string CELL_NAME_SYSTEM_ID = "SYSTEM_ID";

        /// <summary>明細システムID</summary>
        internal const string CELL_NAME_DETAIL_SYSTEM_ID = "DETAIL_SYSTEM_ID";

        /// <summary>割振重量計算用のグルーピングNo</summary>
        internal const string CELL_NAME_WARIHURI_NO = "warihuriNo";

        /// <summary>warihuriNo内の行番</summary>
        internal const string CELL_NAME_WARIHURIROW_NO = "warihuriRowNo";

        /// <summary>締処理状況</summary>
        internal const string CELL_NAME_JOUKYOU = "JOUKYOU";

        /// <summary>マニフェスト番号</summary>
        internal const string CELL_NAME_MANIFEST_ID = "MANIFEST_ID";

        /// <summary>明細備考</summary>
        internal const string CELL_NAME_MEISAI_BIKOU = "MEISAI_BIKOU";

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
        /// DBアクセッサー
        /// </summary>
        private Shougun.Core.Scale.Keiryou.Accessor.DBAccessor accessor;

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// 画面表示時点DTO
        /// </summary>
        private DTOClass beforDto;

        /// <summary>
        /// ControlUtility
        /// </summary>
        internal ControlUtility controlUtil;

        /// <summary>
        /// 重量系の情報をまとめたリスト
        /// 明細行と重量系との関係が同期されないため
        /// このオブジェクトを使ってコントロールする
        /// </summary>
        internal List<List<JyuuryouDto>> jyuuryouDtoList = new List<List<JyuuryouDto>>();

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

        ///// <summary>
        ///// 荷姿単位全件
        ///// </summary>
        //Dictionary<string, M_NISUGATA> nisugataDictionary = new Dictionary<string, M_NISUGATA>();

        /// <summary>
        /// UIFormの入力コントロール名一覧
        /// </summary>
        private string[] inputUiFormControlNames = { "radbtnHanshutu", "radbtnHannyu", "NINZUU_CNT", "TORIHIKISAKI_NAME_RYAKU", "TORIHIKISAKI_CD", "SHARYOU_CD", "SHARYOU_NAME_RYAKU", "SHASHU_CD", "SHASHU_NAME", "GENBA_CD", "GENBA_NAME_RYAKU", "GYOUSHA_CD", "GYOUSHA_NAME_RYAKU", "UNPAN_GYOUSHA_CD", "UNPAN_GYOUSHA_NAME", "NIOROSHI_GENBA_CD", "NIOROSHI_GENBA_NAME", "NIOROSHI_GYOUSHA_NAME", "NIZUMI_GENBA_NAME", "EIGYOU_TANTOUSHA_CD", "EIGYOU_TANTOUSHA_NAME", "UNTENSHA_CD", "UNTENSHA_NAME", "RENBAN", "KEIRYOU_NUMBER", "DENPYOU_DATE", "NYUURYOKU_TANTOUSHA_CD", "NYUURYOKU_TANTOUSHA_NAME", "KEITAI_KBN_CD", "MANIFEST_SHURUI_CD", "CONTENA_SOUSA_CD", "DENPYOU_BIKOU", "TAIRYUU_BIKOU", "MANIFEST_TEHAI_CD", "nextButton", "previousButton", "TORIHIKISAKI_SEARCH_BUTTON", "GENBA_SEARCH_BUTTON", "UNPAN_GYOUSHA_SEARCH_BUTTON", "NIOROSHI_GYOUSHA_SEARCH_BUTTON", "NIOROSHI_GENBA_SEARCH_BUTTON", "NIZUMI_GENBA_SEARCH_BUTTON", "GYOUSHA_SEARCH_BUTTON", "NIOROSHI_GYOUSHA_CD", "NIZUMI_GYOUSHA_CD", "NIZUMI_GENBA_CD", "NIZUMI_GYOUSHA_SEARCH_BUTTON", "SAISHUU_SHOBUNJOU_KBN", "UNTEN_KBN", "NYUURYOKU_TANTOU_KBN", "NIZUMI_GYOUSHA_NAME", "KIHON_KEIRYOU", "UKETSUKE_NUMBER", "KEITAI_KBN_NAME_RYAKU", "MANIFEST_SHURUI_NAME_RYAKU", "CONTENA_JOUKYOU_NAME_RYAKU", "MANIFEST_TEHAI_NAME_RYAKU", "NYUURYOKU_TANTOUSHA_SEARCH_BUTTON", "EIGYOU_TANTOUSHA_SEARCH_BUTTON", "SHARYOU_SEARCH_BUTTON", "SHASHU_SEARCH_BUTTON", "UNTENSHA_SEARCH_BUTTON", "KEITAI_KBN_SEARCH_BUTTON", "MANIFEST_SHURUI_SEARCH_BUTTON", "MANIFEST_TEHAI_SEARCH_BUTTON", "CONTENA_SOUSA_SEARCH_BUTTON" };

        // No.3822-->
        /// <summary>
        /// タブストップ用
        /// </summary>
        private string[] tabUiFormControlNames = 
            {
                "KYOTEN_CD", "KIHON_KEIRYOU","NYUURYOKU_TANTOUSHA_CD", "KEIRYOU_NUMBER",
                "RENBAN", "UKETSUKE_NUMBER","DENPYOU_DATE", "UNPAN_GYOUSHA_CD", "UNPAN_GYOUSHA_NAME", 
                "SHASHU_CD", "SHARYOU_CD", "SHARYOU_NAME_RYAKU", "UNTENSHA_CD","NINZUU_CNT", 
                "GYOUSHA_CD","GYOUSHA_NAME_RYAKU","GENBA_CD", "GENBA_NAME_RYAKU","TORIHIKISAKI_CD",
                "TORIHIKISAKI_NAME_RYAKU", "NIZUMI_GYOUSHA_CD","NIZUMI_GYOUSHA_NAME", "NIZUMI_GENBA_CD","NIZUMI_GENBA_NAME",
                "NIOROSHI_GYOUSHA_CD", "NIOROSHI_GYOUSHA_NAME", "NIOROSHI_GENBA_CD", "NIOROSHI_GENBA_NAME","KEITAI_KBN_CD",
                "MANIFEST_SHURUI_CD","MANIFEST_TEHAI_CD","EIGYOU_TANTOUSHA_CD","DENPYOU_BIKOU", "TAIRYUU_BIKOU", 
            };
        private string[] tabDetailControlNames =
            {   "STACK_JYUURYOU","EMPTY_JYUURYOU","WARIFURI_JYUURYOU", "WARIFURI_PERCENT","CHOUSEI_JYUURYOU",
                "CHOUSEI_PERCENT", "YOUKI_NAME_RYAKU","YOUKI_CD","YOUKI_SUURYOU","YOUKI_JYUURYOU",
                "HINMEI_CD","NISUGATA_SUURYOU","NISUGATA_UNIT_CD","MEISAI_BIKOU","MANIFEST_ID",
            };
        // No.3822<--

        /// <summary>
        /// HeaderFormの入力コントロール名一覧
        /// </summary>
        private string[] inputHeaderControlNames = { "KYOTEN_CD"};

        /// <summary>
        /// Detailの入力コントロール名一覧
        /// </summary>
        private string[] inputDetailControlNames = { "ROW_NO", "EMPTY_JYUURYOU", "STACK_JYUURYOU", "WARIFURI_JYUURYOU", "WARIFURI_PERCENT", "CHOUSEI_JYUURYOU", "CHOUSEI_PERCENT", "YOUKI_NAME_RYAKU", "YOUKI_JYUURYOU", "NET_JYUURYOU", "MEISAI_BIKOU", "HINMEI_CD", "DENPYOU_KBN_NAME", "HINMEI_NAME", "YOUKI_CD", "YOUKI_SUURYOU", "MANIFEST_ID", "NISUGATA_SUURYOU", "NISUGATA_UNIT_CD", "NISUGATA_NAME_RYAKU", };


        /// <summary>
        /// ヘッダ部(R354)
        /// </summary>
        private string[] headerReportItemName_R354 = { "KEIRYOU_HYOU_TITLE", "TANTOU", "GYOUSHA_CD", "GYOUSHA_NAME", "GYOUSHA_KEISYOU", "DENPYOU_NUMBER", "JYOUIN", "SHABAN", "DENPYOU_DATE" };

        /// <summary>
        /// 明細部(R354)
        /// </summary>
        private string[] detailReportItemName_R354 = { "ROW_NO", "STACK_JYUURYOU", "EMPTY_JYUURYOU", "NET_CHOUSEI", "YOUKI_JYUURYOU", "NET_JYUURYOU", "HINMEI_CD", "HINMEI_NAME" };

        /// <summary>
        /// フッダー部(R354)
        /// </summary>
        private string[] footerReportItemName_R354 = { "DENPYOU_BIKOU", "GENBA_CD", "GENBA_NAME", "NET_JYUURYOU_TOTAL", "KEIRYOU_JYOUHOU1", "KEIRYOU_JYOUHOU2", "KEIRYOU_JYOUHOU3", "CORP_RYAKU_NAME", "KYOTEN_NAME", "KYOTEN_ADDRESS1", "KYOTEN_ADDRESS2", "KYOTEN_TEL", "KYOTEN_FAX" };



        /// <summary>
        /// ヘッダ部(R549)
        /// </summary>
        private string[] headerReportItemName_R549 = { "KEIRYOU_HYOU_TITLE", "DENPYOU_DATE", "DENPYOU_NUMBER", "TORIHIKISAKI_CD", "TORIHIKISAKI_NAME", "TORIHIKISAKI_KEISYOU", "GYOUSHA_CD", "GYOUSHA_NAME", "GYOUSHA_KEISYOU", "GENBA_CD", "GENBA_NAME", "SHARYOU", "STACK_JYUURYOU", "EMPTY_JYUURYOU", "GENBA_KEISYOU" };

        /// <summary>
        /// 明細部(R549)
        /// </summary>
        private string[] detailReportItemName_R549 = { "HINMEI_CD", "HINMEI_NAME", "NET_CHOUSEI", "YOUKI_JYUURYOU", "NET_JYUURYOU" };

        /// <summary>
        /// フッダー部(R549)
        /// </summary>
        private string[] footerReportItemName_R549 = { "DENPYOU_BIKOU", "NET_CHOSEI_TOTAL", "YOUKI_JYUURYOU_TOTAL", "NET_JYUURYOU_TOTAL", "CORP_RYAKU_NAME", "KYOTEN_NAME", "KYOTEN_ADDRESS1", "KYOTEN_ADDRESS2", "KYOTEN_TEL", "KYOTEN_FAX" };


        /// <summary>
        /// ヘッダ部(R550)
        /// </summary>
        private string[] headerReportItemName_R550 = { "KEIRYOU_HYOU_TITLE", "DENPYOU_DATE", "DENPYOU_NUMBER", "TORIHIKISAKI_CD", "TORIHIKISAKI_NAME", "TORIHIKISAKI_KEISYOU", "GYOUSHA_CD", "GYOUSHA_NAME", "GYOUSHA_KEISYOU", "GENBA_CD", "GENBA_NAME", "SHARYOU", "GENBA_KEISYOU" };

        /// <summary>
        /// 明細部(R550)
        /// </summary>
        private string[] detailReportItemName_R550 = { "HINMEI_CD", "HINMEI_NAME", "STACK_JYUURYOU", "EMPTY_JYUURYOU", "NET_CHOUSEI", "YOUKI_JYUURYOU", "NET_JYUURYOU", "DENPYOU_BIKOU" };

        /// <summary>
        /// フッダー部(R550)
        /// </summary>
        private string[] footerReportItemName_R550 = { "DENPYOU_BIKOU", "CORP_RYAKU_NAME", "KYOTEN_NAME", "KYOTEN_ADDRESS1", "KYOTEN_ADDRESS2", "KYOTEN_TEL", "KYOTEN_FAX" };

        /// <summary>
        /// 正式現場名ワーク
        /// </summary>
        private string workGenbaName = null;

        /// <summary>
        /// 正式入力担当者ワーク
        /// </summary>
        private string workTantou = null;

        /// <summary>
        /// 数値の表示形式を設定用(システム設定に基づき）
        /// </summary>
        private string TankaFormat;

        /// <summary>
        /// 端数処理種別
        /// </summary>
        private enum fractionType : int
        {
            CEILING = 1,	// 切り上げ
            FLOOR,		// 切り捨て
            ROUND,		// 四捨五入
        }

        // No.3822-->
        private System.Collections.Specialized.StringCollection DenpyouCtrl = new System.Collections.Specialized.StringCollection();
        private System.Collections.Specialized.StringCollection DetailCtrl = new System.Collections.Specialized.StringCollection();
        // No.3822<--

        /// 20141013 chinchisi 「計量入力画面」の休動Checkを追加する　start
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
        /// 20141013 chinchisi 「計量入力画面」の休動Checkを追加する　end

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();
        #endregion

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

            // Accessor
            this.accessor = new Shougun.Core.Scale.Keiryou.Accessor.DBAccessor();

            // Utility
            this.controlUtil = new ControlUtility();

            this.jyuuryouDtoList = new List<List<JyuuryouDto>>();

            /// 20141013 chinchisi 「計量入力画面」の休動Checkを追加する　start
            this.workclosedsharyouDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_SHARYOUDao>();
            this.workcloseduntenshaDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_UNTENSHADao>();
            this.workclosedhannyuusakiDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_HANNYUUSAKIDao>();
            /// 20141013 chinchisi 「計量入力画面」の休動Checkを追加する　end

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.ChangeEnabledForInputControl(false);

                // 数値の表示形式を設定を初期化(システム設定に基づき）
                TankaFormat = this.dto.sysInfoEntity.SYS_JYURYOU_FORMAT;

                // No.2334-->
                if (this.dto.entryEntity.TAIRYUU_KBN)
                {
                    this.form.TairyuuNewFlg = true;
                    if (!this.dto.entryEntity.KEIRYOU_NUMBER.IsNull)
                    {
                        this.form.KeiryouNumber = long.Parse(this.dto.entryEntity.KEIRYOU_NUMBER.ToString());
                    }
                    //this.form.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;   //ThangNguyen [Delete] 20150818 #11112
                    this.form.HeaderFormInit();
                }
                // No.2334<--

                // 初期化
                this.winClear();

                // タブオーダーデータ取得
                GetStatus();   // No.3822

                // DisplayInitでTemplateを書き換えているので、データをセットする前に実行すること
                this.DisplayInit();

                if (!this.EntryDataInit())
                {
                    ret = false;
                    return ret;
                }

                this.NumberingRowNo();

                if (this.form.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG) || this.form.WindowType.Equals(WINDOW_TYPE.REFERENCE_WINDOW_FLAG))
                {
                    // 削除モード、参照モード時には全コントロールをReadOnlyにする
                    this.ChangeEnabledForInputControl(true);
                }

                // 伝票発行ポップアップDTO初期化
                this.form.denpyouHakouPopUpDTO = new Shougun.Core.SalesPayment.DenpyouHakou.ParameterDTOClass();

                // 検索ボタン設定
                this.setSearchButtonInfo();
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
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        /// <param name="registeredFlag">登録後処理かどうか。デフォルト：false</param>
        public bool ButtonInit(bool registeredFlag = false)
        {
            bool ret = true;
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
                        this.footerForm.bt_func1.Enabled = true;
                        this.footerForm.bt_func2.Enabled = true;
                        this.footerForm.bt_func3.Enabled = false;
                        this.footerForm.bt_func4.Enabled = true;
                        this.footerForm.bt_func5.Enabled = true;
                        this.footerForm.bt_func7.Enabled = true;
                        this.footerForm.bt_func8.Enabled = false;
                        this.footerForm.bt_func9.Enabled = true; //No3500
                        this.footerForm.bt_func10.Enabled = true;
                        this.footerForm.bt_func11.Enabled = true;
                        this.footerForm.bt_func12.Enabled = true;
                        this.footerForm.bt_process1.Enabled = true;
                        this.footerForm.bt_process2.Enabled = true;
                        this.footerForm.bt_process3.Enabled = false;
                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 修正モードの場合、登録後に初めて活性化するボタンがあるので制御
                        if (registeredFlag)
                        {
                            // 登録後処理
                            this.footerForm.bt_func1.Enabled = false;
                            this.footerForm.bt_func2.Enabled = true;
                            this.footerForm.bt_func3.Enabled = true;
                            this.footerForm.bt_func5.Enabled = false;
                            this.footerForm.bt_func6.Enabled = false;
                            this.footerForm.bt_func8.Enabled = true;
                            this.footerForm.bt_func9.Enabled = false;
                            this.footerForm.bt_func10.Enabled = false;
                            this.footerForm.bt_func11.Enabled = false;
                            this.footerForm.bt_process1.Enabled = false;
                            this.footerForm.bt_process2.Enabled = false;
                            this.footerForm.bt_process3.Enabled = false;
                        }
                        else
                        {
                            this.footerForm.bt_func1.Enabled = true;
                            this.footerForm.bt_func2.Enabled = true;    //ThangNguyen [Add] 20150824 #11118
                            this.footerForm.bt_func3.Enabled = false;
                            this.footerForm.bt_func5.Enabled = true;
                            this.footerForm.bt_func6.Enabled = true;
                            this.footerForm.bt_func8.Enabled = true;
                            this.footerForm.bt_func9.Enabled = true;
                            this.footerForm.bt_func10.Enabled = true;
                            this.footerForm.bt_func11.Enabled = true;
                            this.footerForm.bt_process1.Enabled = true;
                            this.footerForm.bt_process2.Enabled = true;
                            this.footerForm.bt_process3.Enabled = false;

                        } break;
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        // 削除モードの場合、
                        this.footerForm.bt_func1.Enabled = false;
                        this.footerForm.bt_func2.Enabled = false;
                        this.footerForm.bt_func3.Enabled = false;
                        this.footerForm.bt_func4.Enabled = false;
                        this.footerForm.bt_func5.Enabled = false;
                        this.footerForm.bt_func7.Enabled = true;
                        this.footerForm.bt_func8.Enabled = false;
                        this.footerForm.bt_func9.Enabled = true;
                        this.footerForm.bt_func10.Enabled = false;
                        this.footerForm.bt_func11.Enabled = false;
                        this.footerForm.bt_func12.Enabled = true;
                        this.footerForm.bt_process1.Enabled = false;
                        this.footerForm.bt_process2.Enabled = false;
                        this.footerForm.bt_process3.Enabled = false;
                        break;
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        // 削除モードの場合、
                        this.footerForm.bt_func1.Enabled = false;
                        this.footerForm.bt_func2.Enabled = false;
                        this.footerForm.bt_func3.Enabled = false;
                        this.footerForm.bt_func4.Enabled = false;
                        this.footerForm.bt_func5.Enabled = false;
                        this.footerForm.bt_func7.Enabled = true;
                        this.footerForm.bt_func8.Enabled = false;
                        this.footerForm.bt_func9.Enabled = false;
                        this.footerForm.bt_func10.Enabled = false;
                        this.footerForm.bt_func11.Enabled = false;
                        this.footerForm.bt_func12.Enabled = true;
                        this.footerForm.bt_process1.Enabled = false;
                        this.footerForm.bt_process2.Enabled = false;
                        this.footerForm.bt_process3.Enabled = false;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// イベント初期化処理
        /// </summary>
        public bool EventInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 新規ボタン(F1)イベント
                footerForm.bt_func1.Click += new EventHandler(this.form.Weight_Click);

                // 新規ボタン(F2)イベント
                footerForm.bt_func2.Click += new EventHandler(this.form.ChangeNewWindow);

                // 修正ボタン(F3)イベント
                footerForm.bt_func3.Click += new EventHandler(this.form.ChangeUpdateWindow);

                // 受付伝票ボタン(F4)イベント
                this.form.C_Regist(footerForm.bt_func4);
                footerForm.bt_func4.Click += new EventHandler(this.form.ForwordUketukeDenpyo);
                footerForm.bt_func4.ProcessKbn = PROCESS_KBN.NEW;

                // 滞留ボタン(F5)イベント
                this.form.C_Regist(footerForm.bt_func5);
                footerForm.bt_func5.Click += new EventHandler(this.form.TairyuuRegist);
                footerForm.bt_func5.ProcessKbn = PROCESS_KBN.NEW;

                // 一覧ボタン(F7)イベント
                this.form.C_Regist(footerForm.bt_func7);
                footerForm.bt_func7.Click += new EventHandler(this.form.ShowDenpyouIchiran);
                footerForm.bt_func7.ProcessKbn = PROCESS_KBN.NEW;

                // 複写ボタン(F8)イベント
                this.form.C_Regist(footerForm.bt_func8);
                footerForm.bt_func8.Click += new EventHandler(this.form.Copy);
                footerForm.bt_func8.ProcessKbn = PROCESS_KBN.NEW;

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

                // HeaderForm
                // 拠点CDイベント
                this.headerForm.KYOTEN_CD.Validated += new EventHandler(this.form.KYOTEN_CD_OnValidated);

                // 計量票発行ボタンイベント
                this.form.C_Regist(footerForm.bt_process1);
                footerForm.bt_process1.Click += new EventHandler(this.form.KeiryouhyouHakkou);
                footerForm.bt_process1.ProcessKbn = PROCESS_KBN.NEW;

                // 手入力ボタンイベント
                this.form.C_Regist(footerForm.bt_process2);
                footerForm.bt_process2.Click += new EventHandler(this.form.Tenyuuryoku);
                footerForm.bt_process2.ProcessKbn = PROCESS_KBN.NEW;


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
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
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
        internal void DisplayInit()
        {
            this.form.RENBAN.ReadOnly = false;

            this.ChangePropertyForGC(null, new string[] { "STACK_JYUURYOU", "EMPTY_JYUURYOU", "NET_JYUURYOU", "WARIFURI_JYUURYOU", "WARIFURI_PERCENT", "CHOUSEI_JYUURYOU", "CHOUSEI_PERCENT" }, "ReadOnly", false);

            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    // 重量系
                    this.ChangePropertyForGC(null, new string[] { "WARIFURI_JYUURYOU", "WARIFURI_PERCENT", "CHOUSEI_JYUURYOU", "CHOUSEI_PERCENT", "NET_JYUURYOU" }, "ReadOnly", true);
                    //this.ChangePropertyForGC(null, new string[] { "NET_JYUURYOU" }, "ReadOnly", true);

                    //受付番号検索中
                    if (this.form.uketsukeNumberNowLoding)
                    {
                        // 入出区分
                        this.form.KIHON_KEIRYOU.ReadOnly = true;
                        this.form.KIHON_KEIRYOU.TabStop = false;
                        this.form.radbtnHannyu.Enabled = false;
                        this.form.radbtnHanshutu.Enabled = false;
                        this.form.UKETSUKE_NUMBER.ReadOnly = false;
                        //this.form.UKETSUKE_NUMBER.TabStop = true;
                        this.form.UKETSUKE_NUMBER.TabStop = GetTabStop("UKETSUKE_NUMBER");    // No.3822
                    }
                    else
                    {
                        // 入出区分
                        this.form.KIHON_KEIRYOU.ReadOnly = false;
                        //this.form.KIHON_KEIRYOU.TabStop = true;
                        this.form.KIHON_KEIRYOU.TabStop = GetTabStop("KIHON_KEIRYOU");    // No.3822
                        this.form.radbtnHannyu.Enabled = true;
                        this.form.radbtnHanshutu.Enabled = true;
                        this.form.UKETSUKE_NUMBER.ReadOnly = false;
                        //this.form.UKETSUKE_NUMBER.TabStop = true;
                        this.form.UKETSUKE_NUMBER.TabStop = GetTabStop("UKETSUKE_NUMBER");    // No.3822
                    }

                    break;

                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    // 重量系
                    // ボタン制御

                    // 入出力区分
                    this.form.KIHON_KEIRYOU.ReadOnly = true;
                    this.form.KIHON_KEIRYOU.TabStop = false;
                    this.form.radbtnHannyu.Enabled = false;
                    this.form.radbtnHanshutu.Enabled = false;

                    this.form.UKETSUKE_NUMBER.ReadOnly = true;
                    this.form.UKETSUKE_NUMBER.TabStop = false;

                    //this.ChangePropertyForGC(null, new string[] {  "STACK_JYUURYOU", "EMPTY_JYUURYOU", "NET_JYUURYOU" }, "ReadOnly", true);
                    this.ChangePropertyForGC(null, new string[] { "NET_JYUURYOU" }, "ReadOnly", true);

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
                this.form.KIHON_KEIRYOU.RegistCheckMethod = null;
                this.form.DENPYOU_DATE.RegistCheckMethod = null;
                this.form.NYUURYOKU_TANTOUSHA_CD.RegistCheckMethod = null;
                this.form.TORIHIKISAKI_CD.RegistCheckMethod = null;
                this.form.GYOUSHA_CD.RegistCheckMethod = null;

                // Detail
                this.form.gcMultiRow1.SuspendLayout();

                foreach (var o in this.form.gcMultiRow1.Rows)
                {
                    var obj2 = controlUtil.FindControl(o.Cells.ToArray(), new string[] { "HINMEI_CD" });
                    foreach (var target in obj2)
                    {
                        PropertyUtility.SetValue(target, "RegistCheckMethod", null);
                    }
                }

                this.form.gcMultiRow1.ResumeLayout();
            }
            catch (Exception ex)
            {
                LogUtility.Error("RequiredSettingInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
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
                    this.headerForm.KYOTEN_CD.RegistCheckMethod = excitChecks;
                    //this.form.KIHON_KEIRYOU.RegistCheckMethod = excitChecks;
                    this.form.DENPYOU_DATE.RegistCheckMethod = excitChecks;
                    this.form.NYUURYOKU_TANTOUSHA_CD.RegistCheckMethod = excitChecks;
                    //this.form.TORIHIKISAKI_CD.RegistCheckMethod = excitChecks;
                    //this.form.GYOUSHA_CD.RegistCheckMethod = excitChecks;
                }
                else
                {
                    // 登録
                    this.headerForm.KYOTEN_CD.RegistCheckMethod = excitChecks;
                    this.form.KIHON_KEIRYOU.RegistCheckMethod = excitChecks;
                    this.form.DENPYOU_DATE.RegistCheckMethod = excitChecks;
                    this.form.NYUURYOKU_TANTOUSHA_CD.RegistCheckMethod = excitChecks;
                    this.form.TORIHIKISAKI_CD.RegistCheckMethod = excitChecks;
                    this.form.GYOUSHA_CD.RegistCheckMethod = excitChecks;


                    this.form.gcMultiRow1.SuspendLayout();

                    foreach (var o in this.form.gcMultiRow1.Rows)
                    {
                        var obj2 = controlUtil.FindControl(o.Cells.ToArray(), new string[] { "HINMEI_CD" });
                        foreach (var target in obj2)
                        {
                            PropertyUtility.SetValue(target, "RegistCheckMethod", excitChecks);
                        }
                    }

                    this.form.gcMultiRow1.ResumeLayout();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetRequiredSetting", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 画面のT_KEIRYOU_ENTRY部分の初期化処理
        /// </summary>
        internal bool EntryDataInit()
        {
            var denpyous = this.accessor.GetAllDenpyouKbn();
            var youkis = this.accessor.GetAllYouki();
            var units = this.accessor.GetAllUnit();

            this.denpyouKbnDictionary = new Dictionary<short, M_DENPYOU_KBN>();
            foreach (var denpyou in denpyous)
            {
                this.denpyouKbnDictionary.Add((short)denpyou.DENPYOU_KBN_CD, denpyou);
            }

            this.youkiDictionary = new Dictionary<string, M_YOUKI>();
            foreach (var youki in youkis)
            {
                this.youkiDictionary.Add(youki.YOUKI_CD, youki);
            }

            this.unitDictionary = new Dictionary<short, M_UNIT>();
            foreach (var unit in units)
            {
                this.unitDictionary.Add((short)unit.UNIT_CD, unit);

            }

            SqlInt32 renbanValue = -1;
            // 画面毎に設定が異なるコントロールの初期化(コピペしやすいようにするため)
            // 受付番号
            this.form.KEIRYOU_NUMBER.DBFieldsName = "KEIRYOU_NUMBER";
            this.form.KEIRYOU_NUMBER.ItemDefinedTypes = DB_TYPE.BIGINT.ToTypeString();

            // 連番ラベル、連番
            if (this.dto.sysInfoEntity.SYS_RENBAN_HOUHOU_KBN == KeiryouConstans.SYS_RENBAN_HOUHOU_KBN_HIRENBAN)
            {
                this.form.RENBAN_LABEL.Text = KeiryouConstans.SYS_RENBAN_HOUHOU_KBNExt.ToTypeString(KeiryouConstans.SYS_RENBAN_HOUHOU_KBN.HIRENBAN);
                this.form.RENBAN.DBFieldsName = "DATE_NUMBER";
                this.form.RENBAN.ItemDefinedTypes = DB_TYPE.INT.ToTypeString();
                this.form.RENBAN.Text = this.dto.entryEntity.DATE_NUMBER.IsNull ? "" : this.dto.entryEntity.DATE_NUMBER.ToString();
                renbanValue = this.dto.entryEntity.DATE_NUMBER;
                renbanValue = renbanValue.IsNull ? SqlInt32.Null : renbanValue;
            }
            else if (this.dto.sysInfoEntity.SYS_RENBAN_HOUHOU_KBN == KeiryouConstans.SYS_RENBAN_HOUHOU_KBN_NENRENBAN)
            {
                this.form.RENBAN_LABEL.Text = KeiryouConstans.SYS_RENBAN_HOUHOU_KBNExt.ToTypeString(KeiryouConstans.SYS_RENBAN_HOUHOU_KBN.NENRENBAN);
                this.form.RENBAN.DBFieldsName = "YEAR_NUMBER";
                this.form.RENBAN.ItemDefinedTypes = DB_TYPE.INT.ToTypeString();
                this.form.RENBAN.Text = this.dto.entryEntity.YEAR_NUMBER.IsNull ? "" : this.dto.entryEntity.YEAR_NUMBER.ToString();

                renbanValue = this.dto.entryEntity.YEAR_NUMBER;
                renbanValue = renbanValue.IsNull ? SqlInt32.Null : renbanValue;
            }

            // 割振値、調整値の表示形式初期化
            CalcValueFormatSettingInit();

            // モードによる制御
            if (this.IsRequireData())
            {
                if (!this.SetEntryData(renbanValue))
                {
                    return false;
                }
            }
            else
            {
                //受付番号検索中
                if (this.form.uketsukeNumberNowLoding)
                {
                    if (!this.SetEntryData(renbanValue))
                    {
                        return false;
                    }
                }
                else
                {
                    // 新規

                }
            }
            return true;
        }

        // No.3443-->
        /// <summary>
        /// 数値の表示形式初期化（システム設定に基づき）
        /// </summary>
        internal void CalcValueFormatSettingInit()
        {
            LogUtility.DebugMethodStart();

            this.form.gcMultiRow1.SuspendLayout();
            var newTemplate = this.form.gcMultiRow1.Template;

            // No.3822-->
            var row = this.form.gcMultiRow1.Template.Row;
            GrapeCity.Win.MultiRow.Cell control1 = row.Cells["STACK_JYUURYOU"];   // 総重量
            GrapeCity.Win.MultiRow.Cell control2 = row.Cells["EMPTY_JYUURYOU"];   // 空車重量
            // No.3822<--

            if (SqlInt16.Parse(this.form.KIHON_KEIRYOU.Text) == 1)
            {   // --- 受入 ---
                // 割振割合(%)
                var obj1 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_WARIFURI_PERCENT });
                string FormatSettingValue = SetFormat((int)this.dto.sysInfoEntity.UKEIRE_WARIFURI_WARIAI_HASU_CD,
                    (short)this.dto.sysInfoEntity.UKEIRE_WARIFURI_WARIAI_HASU_KETA);
                foreach (var o in obj1)
                {
                    PropertyUtility.SetValue(o, "FormatSetting", "カスタム");
                    PropertyUtility.SetValue(o, "CustomFormatSetting", FormatSettingValue);
                }

                // 割振値
                var obj2 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_WARIFURI_JYUURYOU });
                FormatSettingValue = SetFormat((int)this.dto.sysInfoEntity.UKEIRE_WARIFURI_HASU_CD,
                    (short)this.dto.sysInfoEntity.UKEIRE_WARIFURI_HASU_KETA);
                foreach (var o in obj2)
                {
                    PropertyUtility.SetValue(o, "FormatSetting", "カスタム");
                    PropertyUtility.SetValue(o, "CustomFormatSetting", FormatSettingValue);
                }

                // 調整割合(%)
                var obj3 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_CHOUSEI_PERCENT });
                FormatSettingValue = SetFormat((int)this.dto.sysInfoEntity.UKEIRE_CHOUSEI_WARIAI_HASU_CD,
                    (short)this.dto.sysInfoEntity.UKEIRE_CHOUSEI_WARIAI_HASU_KETA);
                foreach (var o in obj3)
                {
                    PropertyUtility.SetValue(o, "FormatSetting", "カスタム");
                    PropertyUtility.SetValue(o, "CustomFormatSetting", FormatSettingValue);
                }

                // 調整値
                var obj4 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_CHOUSEI_JYUURYOU });
                FormatSettingValue = SetFormat((int)this.dto.sysInfoEntity.UKEIRE_CHOUSEI_HASU_CD,
                    (short)this.dto.sysInfoEntity.UKEIRE_CHOUSEI_HASU_KETA);
                foreach (var o in obj4)
                {
                    PropertyUtility.SetValue(o, "FormatSetting", "カスタム");
                    PropertyUtility.SetValue(o, "CustomFormatSetting", FormatSettingValue);
                }

                // No.3822-->
                /*if (control1.TabIndex > control2.TabIndex)
                {
                    var index = control1.TabIndex;
                    control1.TabIndex = control2.TabIndex;
                    control2.TabIndex = index;
                }*/
                // No.3822<--
            }
            else
            {   // --- 出荷 ---
                // 割振割合(%)
                var obj1 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_WARIFURI_PERCENT });
                string FormatSettingValue = SetFormat((int)this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_CD,
                    (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_KETA);
                foreach (var o in obj1)
                {
                    PropertyUtility.SetValue(o, "FormatSetting", "カスタム");
                    PropertyUtility.SetValue(o, "CustomFormatSetting", FormatSettingValue);
                }

                // 割振値
                var obj2 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_WARIFURI_JYUURYOU });
                FormatSettingValue = SetFormat((int)this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_CD,
                    (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_KETA);
                foreach (var o in obj2)
                {
                    PropertyUtility.SetValue(o, "FormatSetting", "カスタム");
                    PropertyUtility.SetValue(o, "CustomFormatSetting", FormatSettingValue);
                }

                // 調整割合(%)
                var obj3 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_CHOUSEI_PERCENT });
                FormatSettingValue = SetFormat((int)this.dto.sysInfoEntity.SHUKKA_CHOUSEI_WARIAI_HASU_CD,
                    (short)this.dto.sysInfoEntity.SHUKKA_CHOUSEI_WARIAI_HASU_KETA);
                foreach (var o in obj3)
                {
                    PropertyUtility.SetValue(o, "FormatSetting", "カスタム");
                    PropertyUtility.SetValue(o, "CustomFormatSetting", FormatSettingValue);
                }

                // 調整値
                var obj4 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), new string[] { CELL_NAME_CHOUSEI_JYUURYOU });
                FormatSettingValue = SetFormat((int)this.dto.sysInfoEntity.SHUKKA_CHOUSEI_HASU_CD,
                    (short)this.dto.sysInfoEntity.SHUKKA_CHOUSEI_HASU_KETA);
                foreach (var o in obj4)
                {
                    PropertyUtility.SetValue(o, "FormatSetting", "カスタム");
                    PropertyUtility.SetValue(o, "CustomFormatSetting", FormatSettingValue);
                }

                // No.3822-->
                /*if (control1.TabIndex < control2.TabIndex)
                {
                    var index = control1.TabIndex;
                    control1.TabIndex = control2.TabIndex;
                    control2.TabIndex = index;
                }*/
                // No.3822<--
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
                returnValue = returnValue + "0";
            }

            LogUtility.DebugMethodEnd();
            return returnValue;
        }
        // No.3443<--

        // No.3200-->
        /// <summary>
        /// 登録者情報＆更新者情報の表示
        /// 新規の場合は非表示
        /// </summary>
        /// <param name="windowType"></param>
        private void dispUserInfo(WINDOW_TYPE windowType)
        {
            bool isDisp;

            if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                isDisp = false;
            }
            else
            {
                isDisp = true;
            }
            // 登録者情報
            headerForm.label36.Visible = isDisp;
            headerForm.CreateUser.Visible = isDisp;
            headerForm.CreateDate.Visible = isDisp;
            // 更新者情報
            headerForm.label35.Visible = isDisp;
            headerForm.LastUpdateUser.Visible = isDisp;
            headerForm.LastUpdateDate.Visible = isDisp;
        }
        // No.3200<--

        /// <summary>
        /// 画面のクリア
        /// </summary>
        internal void winClear()
        {
            LogUtility.DebugMethodStart();

            // LogicClassで初期化が必要な場合はここに記載
            this.nenRenbanRegistKbn = Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.NONE;
            this.hiRenbanRegistKbn = Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.NONE;

            footerForm = (BusinessBaseForm)this.form.Parent;
            headerForm = (UIHeaderForm)footerForm.headerForm;
            headerForm.logic = this;    // No.3822

            M_HINMEI[] allHinmeis = this.accessor.GetAllValidHinmeiData();

            // DBには無い値などの設定
            this.denpyouKbnDictionary.Clear();
            this.youkiDictionary.Clear();
            this.unitDictionary.Clear();

            // 日付系初期値設定
            this.form.DENPYOU_DATE.Value = DateTime.Now;

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
                CheckKyotenCd();
            }

            // 登録者情報
            headerForm.CreateUser.Text = string.Empty;
            headerForm.CreateDate.Text = string.Empty;

            // 更新者情報
            headerForm.LastUpdateUser.Text = string.Empty;
            headerForm.LastUpdateDate.Text = string.Empty;

            // No.3200-->
            this.dispUserInfo(this.form.WindowType);
            // No.3200<--

            // ヘッダー End

            // 詳細 Start
            // 連番
            this.form.RENBAN.Text = string.Empty;
            // 受付番号
            if (!this.form.uketsukeNumberNowLoding)
            {
                this.form.UKETSUKE_NUMBER.Text = string.Empty;
                this.form.beforUketukeNumber = string.Empty;
            }
            // 計量番号
            this.form.KEIRYOU_NUMBER.Text = string.Empty;
            this.form.beforKeiryouNumber = string.Empty;
            // 入力担当者
            if (CommonShogunData.LOGIN_USER_INFO != null
                && !string.IsNullOrEmpty(CommonShogunData.LOGIN_USER_INFO.SHAIN_CD)
                && CommonShogunData.LOGIN_USER_INFO.NYUURYOKU_TANTOU_KBN)
            {
                this.form.NYUURYOKU_TANTOUSHA_CD.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_CD.ToString();
                this.form.NYUURYOKU_TANTOUSHA_NAME.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME_RYAKU.ToString();
                workTantou = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME.ToString();    // No.3279
            }
            else
            {
                this.form.NYUURYOKU_TANTOUSHA_CD.Text = string.Empty;
                this.form.NYUURYOKU_TANTOUSHA_NAME.Text = string.Empty;
                workTantou = string.Empty;  // No.3279
            }
            this.form.NYUURYOKU_TANTOUSHA_NAME.ReadOnly = true;

            // 入出区分
            this.form.KIHON_KEIRYOU.Text = "1";
            this.form.radbtnHannyu.Checked = true;
            this.nizumi_nioroshi(true);

            // 日付系初期値設定
            this.form.DENPYOU_DATE.Value = DateTime.Now;
            // 車輌
            this.form.SHARYOU_CD.Text = string.Empty;
            this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
            this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
            this.form.SHARYOU_NAME_RYAKU.TabStop = false;
            this.form.SHARYOU_CD.BackColor = SystemColors.Window;
            // 車輌重量
            this.form.KUUSHA_JYURYO.Text = string.Empty;        // No.4101
            // 取引先
            this.form.TORIHIKISAKI_CD.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
            this.form.beforTorihikisakiCD = string.Empty;
            // 車種
            this.form.SHASHU_CD.Text = string.Empty;
            this.form.SHASHU_NAME.Text = string.Empty;
            this.form.SHASHU_NAME.ReadOnly = true;
            // 運搬業者
            this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
            this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
            this.form.UNPAN_GYOUSHA_NAME.ReadOnly = true;
            this.form.beforUnpanGyoushaCD = string.Empty;
            // 業者
            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.ReadOnly = true;
            this.form.beforGyousaCD = string.Empty;
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
            this.form.beforeGenbaCD = string.Empty;
            // 形態区分
            this.form.KEITAI_KBN_CD.Text = string.Empty;
            this.form.KEITAI_KBN_NAME_RYAKU.Text = string.Empty;
            this.form.KEITAI_KBN_NAME_RYAKU.ReadOnly = true;
            // コンテナ
            this.form.CONTENA_SOUSA_CD.Text = string.Empty;
            this.form.CONTENA_JOUKYOU_NAME_RYAKU.Text = string.Empty;
            this.form.CONTENA_JOUKYOU_NAME_RYAKU.ReadOnly = true;
            // 荷降業者
            this.form.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
            this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
            this.form.NIOROSHI_GYOUSHA_NAME.ReadOnly = true;
            this.form.beforNizumiGenbaCD = string.Empty;
            // 荷卸現場
            this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
            this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
            this.form.NIOROSHI_GENBA_NAME.ReadOnly = true;
            this.form.beforNioroshiGenbaCD = string.Empty;
            // 荷積業者
            this.form.NIZUMI_GYOUSHA_CD.Text = string.Empty;
            this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
            this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = true;
            this.form.beforNioroshiGyoushaCD = string.Empty;
            // 荷積現場
            this.form.NIZUMI_GENBA_CD.Text = string.Empty;
            this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
            this.form.NIZUMI_GENBA_NAME.ReadOnly = true;
            this.form.beforNizumiGenbaCD = string.Empty;
            // マニフェスト種類
            this.form.MANIFEST_SHURUI_CD.Text = string.Empty;
            this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = string.Empty;
            this.form.MANIFEST_SHURUI_NAME_RYAKU.ReadOnly = true;
            // マニフェスト手配
            this.form.MANIFEST_TEHAI_CD.Text = string.Empty;
            this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = string.Empty;
            this.form.MANIFEST_TEHAI_NAME_RYAKU.ReadOnly = true;
            // 営業担当者
            this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
            this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
            this.form.EIGYOU_TANTOUSHA_NAME.ReadOnly = true;
            // 伝票備考
            this.form.DENPYOU_BIKOU.Text = string.Empty;
            // 滞留備考
            this.form.TAIRYUU_BIKOU.Text = string.Empty;

            //  正味合計
            this.form.NET_TOTAL.Text = string.Empty;

            // 詳細 End

            // 新規モードの初期化処理
            //this.ChangePropertyForGC(new string[] { "" },
            //    new string[] { "WARIFURI_JYUURYOU", "CHOUSEI_JYUURYOU", "WARIFURI_PERCENT", "CHOUSEI_PERCENT" },
            //    "Readonly", true);


            //// 明細 Start
            //// テンプレートをいじる処理は、データ設定前に実行
            //this.Tenyuuryoku(true);

            //this.ExecuteAlignmentForDetail();
            this.form.gcMultiRow1.BeginEdit(false);
            this.form.gcMultiRow1.Rows.Clear();
            this.form.gcMultiRow1.EndEdit();
            this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
            // 選択されたセル名称設定
            this.form.selectedCellName = CELL_NAME_STAK_JYUURYOU;
            // 明細 End

            // フッダー Start
            // フッダー End

            LogUtility.DebugMethodEnd();

        }
        /// <summary>
        /// 検索ボタンの設定をする
        /// デザインのマージ対策
        /// レイアウトの調整をするとぬめぬめ動くと思われるので、
        /// ポップアップの設定だけをセッティング
        /// </summary>
        internal void setSearchButtonInfo()
        {
            // 2013/11/15以降のバグ修正分のみを設定

            // 業者
            this.form.GYOUSHA_SEARCH_BUTTON.PopupGetMasterField = this.form.GYOUSHA_CD.PopupGetMasterField;
            this.form.GYOUSHA_SEARCH_BUTTON.PopupSetFormField = this.form.GYOUSHA_CD.PopupSetFormField;
            this.form.GYOUSHA_SEARCH_BUTTON.PopupMultiSelect = this.form.GYOUSHA_CD.PopupMultiSelect;
            this.form.GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams = this.form.GYOUSHA_CD.PopupSearchSendParams;
            this.form.GYOUSHA_SEARCH_BUTTON.PopupWindowId = this.form.GYOUSHA_CD.PopupWindowId;
            this.form.GYOUSHA_SEARCH_BUTTON.PopupWindowName = this.form.GYOUSHA_CD.PopupWindowName;
            this.form.GYOUSHA_SEARCH_BUTTON.popupWindowSetting = this.form.GYOUSHA_CD.popupWindowSetting;

            // 入力担当者
            this.form.NYUURYOKU_TANTOUSHA_SEARCH_BUTTON.PopupGetMasterField = this.form.NYUURYOKU_TANTOUSHA_CD.PopupGetMasterField;
            this.form.NYUURYOKU_TANTOUSHA_SEARCH_BUTTON.PopupSetFormField = this.form.NYUURYOKU_TANTOUSHA_CD.PopupSetFormField;
            this.form.NYUURYOKU_TANTOUSHA_SEARCH_BUTTON.PopupMultiSelect = this.form.NYUURYOKU_TANTOUSHA_CD.PopupMultiSelect;
            this.form.NYUURYOKU_TANTOUSHA_SEARCH_BUTTON.PopupSearchSendParams = this.form.NYUURYOKU_TANTOUSHA_CD.PopupSearchSendParams;
            this.form.NYUURYOKU_TANTOUSHA_SEARCH_BUTTON.PopupWindowId = this.form.NYUURYOKU_TANTOUSHA_CD.PopupWindowId;
            this.form.NYUURYOKU_TANTOUSHA_SEARCH_BUTTON.PopupWindowName = this.form.NYUURYOKU_TANTOUSHA_CD.PopupWindowName;
            this.form.NYUURYOKU_TANTOUSHA_SEARCH_BUTTON.popupWindowSetting = this.form.NYUURYOKU_TANTOUSHA_CD.popupWindowSetting;

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

            // 形態区分
            this.form.KEITAI_KBN_SEARCH_BUTTON.PopupGetMasterField = this.form.KEITAI_KBN_CD.PopupGetMasterField;
            this.form.KEITAI_KBN_SEARCH_BUTTON.PopupSetFormField = this.form.KEITAI_KBN_CD.PopupSetFormField;
            this.form.KEITAI_KBN_SEARCH_BUTTON.PopupMultiSelect = this.form.KEITAI_KBN_CD.PopupMultiSelect;
            this.form.KEITAI_KBN_SEARCH_BUTTON.PopupSearchSendParams = this.form.KEITAI_KBN_CD.PopupSearchSendParams;
            this.form.KEITAI_KBN_SEARCH_BUTTON.PopupWindowId = this.form.KEITAI_KBN_CD.PopupWindowId;
            this.form.KEITAI_KBN_SEARCH_BUTTON.PopupWindowName = this.form.KEITAI_KBN_CD.PopupWindowName;
            this.form.KEITAI_KBN_SEARCH_BUTTON.popupWindowSetting = this.form.KEITAI_KBN_CD.popupWindowSetting;

            // コンテナ
            this.form.CONTENA_SOUSA_SEARCH_BUTTON.PopupGetMasterField = this.form.CONTENA_SOUSA_CD.PopupGetMasterField;
            this.form.CONTENA_SOUSA_SEARCH_BUTTON.PopupSetFormField = this.form.CONTENA_SOUSA_CD.PopupSetFormField;
            this.form.CONTENA_SOUSA_SEARCH_BUTTON.PopupMultiSelect = this.form.CONTENA_SOUSA_CD.PopupMultiSelect;
            this.form.CONTENA_SOUSA_SEARCH_BUTTON.PopupSearchSendParams = this.form.CONTENA_SOUSA_CD.PopupSearchSendParams;
            this.form.CONTENA_SOUSA_SEARCH_BUTTON.PopupWindowId = this.form.CONTENA_SOUSA_CD.PopupWindowId;
            this.form.CONTENA_SOUSA_SEARCH_BUTTON.PopupWindowName = this.form.CONTENA_SOUSA_CD.PopupWindowName;
            this.form.CONTENA_SOUSA_SEARCH_BUTTON.popupWindowSetting = this.form.CONTENA_SOUSA_CD.popupWindowSetting;

            // マニ種類
            this.form.MANIFEST_SHURUI_SEARCH_BUTTON.PopupGetMasterField = this.form.MANIFEST_SHURUI_CD.PopupGetMasterField;
            this.form.MANIFEST_SHURUI_SEARCH_BUTTON.PopupSetFormField = this.form.MANIFEST_SHURUI_CD.PopupSetFormField;
            this.form.MANIFEST_SHURUI_SEARCH_BUTTON.PopupMultiSelect = this.form.MANIFEST_SHURUI_CD.PopupMultiSelect;
            this.form.MANIFEST_SHURUI_SEARCH_BUTTON.PopupSearchSendParams = this.form.MANIFEST_SHURUI_CD.PopupSearchSendParams;
            this.form.MANIFEST_SHURUI_SEARCH_BUTTON.PopupWindowId = this.form.MANIFEST_SHURUI_CD.PopupWindowId;
            this.form.MANIFEST_SHURUI_SEARCH_BUTTON.PopupWindowName = this.form.MANIFEST_SHURUI_CD.PopupWindowName;
            this.form.MANIFEST_SHURUI_SEARCH_BUTTON.popupWindowSetting = this.form.MANIFEST_SHURUI_CD.popupWindowSetting;

            // マニ手配
            this.form.MANIFEST_TEHAI_SEARCH_BUTTON.PopupGetMasterField = this.form.MANIFEST_TEHAI_CD.PopupGetMasterField;
            this.form.MANIFEST_TEHAI_SEARCH_BUTTON.PopupSetFormField = this.form.MANIFEST_TEHAI_CD.PopupSetFormField;
            this.form.MANIFEST_TEHAI_SEARCH_BUTTON.PopupMultiSelect = this.form.MANIFEST_TEHAI_CD.PopupMultiSelect;
            this.form.MANIFEST_TEHAI_SEARCH_BUTTON.PopupSearchSendParams = this.form.MANIFEST_TEHAI_CD.PopupSearchSendParams;
            this.form.MANIFEST_TEHAI_SEARCH_BUTTON.PopupWindowId = this.form.MANIFEST_TEHAI_CD.PopupWindowId;
            this.form.MANIFEST_TEHAI_SEARCH_BUTTON.PopupWindowName = this.form.MANIFEST_TEHAI_CD.PopupWindowName;
            this.form.MANIFEST_TEHAI_SEARCH_BUTTON.popupWindowSetting = this.form.MANIFEST_TEHAI_CD.popupWindowSetting;

            // 営業担当者
            this.form.EIGYOU_TANTOUSHA_SEARCH_BUTTON.PopupGetMasterField = this.form.EIGYOU_TANTOUSHA_CD.PopupGetMasterField;
            this.form.EIGYOU_TANTOUSHA_SEARCH_BUTTON.PopupSetFormField = this.form.EIGYOU_TANTOUSHA_CD.PopupSetFormField;
            this.form.EIGYOU_TANTOUSHA_SEARCH_BUTTON.PopupMultiSelect = this.form.EIGYOU_TANTOUSHA_CD.PopupMultiSelect;
            this.form.EIGYOU_TANTOUSHA_SEARCH_BUTTON.PopupSearchSendParams = this.form.EIGYOU_TANTOUSHA_CD.PopupSearchSendParams;
            this.form.EIGYOU_TANTOUSHA_SEARCH_BUTTON.PopupWindowId = this.form.EIGYOU_TANTOUSHA_CD.PopupWindowId;
            this.form.EIGYOU_TANTOUSHA_SEARCH_BUTTON.PopupWindowName = this.form.EIGYOU_TANTOUSHA_CD.PopupWindowName;
            this.form.EIGYOU_TANTOUSHA_SEARCH_BUTTON.popupWindowSetting = this.form.EIGYOU_TANTOUSHA_CD.popupWindowSetting;

            // 各CDのフォーカスアウト処理を通すため、検索ポップアップから戻ってきたら各CDへフォーカスする
            this.form.TORIHIKISAKI_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToTorihikisakiCd";
            // TODO: 業者、現場の検索ボタン名がおかしいため後で修正
            this.form.GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToGyoushaCd";
            this.form.GENBA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToGenbaCd";
            this.form.NIOROSHI_GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToNioroshiGyoushaCd";
            this.form.NIOROSHI_GENBA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToNioroshiGenbaCd";
            //this.form.NIZUMI_GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToNiZumiGyoushaCd";
            this.form.NIZUMI_GENBA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToNiZumiGenbaCd";
            this.form.EIGYOU_TANTOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToEigyouTantoushaCd";
            this.form.NYUURYOKU_TANTOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToNyuuryokuTantoushaCd";
            this.form.SHARYOU_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToSharyouCd";
            this.form.SHASHU_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToShashuCd";
            this.form.UNPAN_GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToUnpanGyoushaCd";
            this.form.UNTENSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToUntenshaCd";
            this.form.KEITAI_KBN_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToKeitaiKbnCd";
            // TODO: コンテナのコントロール名が全体的におかしいので後で修正
            this.form.CONTENA_SOUSA_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToContenaJoukyouCd";
            this.form.MANIFEST_SHURUI_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToManiShuruiCd";
            this.form.MANIFEST_TEHAI_SEARCH_BUTTON.PopupAfterExecuteMethod = "MoveToManiTehaiCd";

        }
        /// <summary>
        /// 画面のT_KEIRYOU_ENTRY部分の初期化処理
        /// </summary>
        internal bool SetEntryData(SqlInt32 renbanValue)
        {
            // ヘッダー Start
            // 拠点
            if (!this.dto.entryEntity.KYOTEN_CD.IsNull)
            {
                headerForm.KYOTEN_CD.Text = this.dto.entryEntity.KYOTEN_CD.ToString().PadLeft(2, '0');
            }
            if (!string.IsNullOrEmpty(this.dto.kyotenEntity.KYOTEN_NAME_RYAKU))
            {
                headerForm.KYOTEN_NAME_RYAKU.Text = this.dto.kyotenEntity.KYOTEN_NAME_RYAKU.ToString();
            }
            // ヘッダー End

            // 詳細 Start


            // 計量番号
            if (!this.dto.entryEntity.KEIRYOU_NUMBER.IsNull)
            {
                this.form.KEIRYOU_NUMBER.Text = this.dto.entryEntity.KEIRYOU_NUMBER.ToString();
                this.form.beforKeiryouNumber = this.dto.entryEntity.KEIRYOU_NUMBER.ToString();
            }
            // 連番
            if (renbanValue.IsNull)
            {
                this.form.RENBAN.Text = "";
            }
            else
            {
                this.form.RENBAN.Text = renbanValue.ToString();
            }
            // 入出区分
            if (!this.dto.entryEntity.KIHON_KEIRYOU.IsNull)
            {
                this.form.KIHON_KEIRYOU.Text = this.dto.entryEntity.KIHON_KEIRYOU.ToString();

                if (this.dto.entryEntity.KIHON_KEIRYOU == 1)
                {
                    this.form.radbtnHannyu.Checked = true;

                    this.nizumi_nioroshi(true);

                }
                else if (this.dto.entryEntity.KIHON_KEIRYOU == 2)
                {
                    this.form.radbtnHanshutu.Checked = true;

                    this.nizumi_nioroshi(false);
                }

            }
            // 伝票日付
            if (!this.dto.entryEntity.DENPYOU_DATE.IsNull
                && !string.IsNullOrEmpty(this.dto.entryEntity.DENPYOU_DATE.ToString()))
            {
                this.form.DENPYOU_DATE.Text = this.dto.entryEntity.DENPYOU_DATE.ToString();
            }
            // 入力担当者
            if (!string.IsNullOrEmpty(this.dto.entryEntity.NYUURYOKU_TANTOUSHA_CD))
            {
                this.form.NYUURYOKU_TANTOUSHA_CD.Text = this.dto.entryEntity.NYUURYOKU_TANTOUSHA_CD.ToString();
            }
            if (this.dto.entryEntity.NYUURYOKU_TANTOUSHA_NAME != null
                && !string.IsNullOrEmpty(this.dto.entryEntity.NYUURYOKU_TANTOUSHA_NAME.ToString()))
            {
                this.form.NYUURYOKU_TANTOUSHA_NAME.Text = this.dto.entryEntity.NYUURYOKU_TANTOUSHA_NAME.ToString();
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
            // 取引先
            if (!string.IsNullOrEmpty(this.dto.entryEntity.TORIHIKISAKI_CD))
            {
                this.form.TORIHIKISAKI_CD.Text = this.dto.entryEntity.TORIHIKISAKI_CD.ToString();
                this.form.beforTorihikisakiCD = this.dto.entryEntity.TORIHIKISAKI_CD.ToString();

                this.CheckTorihikisaki();


            }
            if (!string.IsNullOrEmpty(this.dto.entryEntity.TORIHIKISAKI_NAME))
            {
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = this.dto.entryEntity.TORIHIKISAKI_NAME.ToString();
            }

            // 車種
            if (!string.IsNullOrEmpty(this.dto.entryEntity.SHASHU_CD))
            {
                this.form.SHASHU_CD.Text = this.dto.entryEntity.SHASHU_CD.ToString().PadLeft(3, '0');
            }
            if (!string.IsNullOrEmpty(this.dto.entryEntity.SHASHU_NAME))
            {
                this.form.SHASHU_NAME.Text = this.dto.entryEntity.SHASHU_NAME.ToString();
            }
            // 運搬業者
            if (!string.IsNullOrEmpty(this.dto.entryEntity.UNPAN_GYOUSHA_CD))
            {
                this.form.UNPAN_GYOUSHA_CD.Text = this.dto.entryEntity.UNPAN_GYOUSHA_CD.ToString();
                this.form.beforUnpanGyoushaCD = this.dto.entryEntity.UNPAN_GYOUSHA_CD.ToString();
                this.CheckUnpanGyoushaCd();

            }
            if (!string.IsNullOrEmpty(this.dto.entryEntity.UNPAN_GYOUSHA_NAME))
            {
                this.form.UNPAN_GYOUSHA_NAME.Text = this.dto.entryEntity.UNPAN_GYOUSHA_NAME.ToString();
            }
            // 車輌
            if (!string.IsNullOrEmpty(this.dto.entryEntity.SHARYOU_CD))
            {
                this.form.SHARYOU_CD.Text = this.dto.entryEntity.SHARYOU_CD.ToString().PadLeft(6, '0');
            }
            if (!string.IsNullOrEmpty(this.dto.entryEntity.SHARYOU_NAME))
            {
                this.form.SHARYOU_NAME_RYAKU.Text = this.dto.entryEntity.SHARYOU_NAME.ToString();
            }
            // 業者
            if (!string.IsNullOrEmpty(this.dto.entryEntity.GYOUSHA_CD))
            {
                this.form.GYOUSHA_CD.Text = this.dto.entryEntity.GYOUSHA_CD.ToString();
                this.form.beforGyousaCD = this.dto.entryEntity.GYOUSHA_CD.ToString();
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
            // 現場
            if (!string.IsNullOrEmpty(this.dto.entryEntity.GENBA_CD))
            {
                this.form.GENBA_CD.Text = this.dto.entryEntity.GENBA_CD.ToString();
                this.form.beforeGenbaCD = this.dto.entryEntity.GENBA_CD.ToString();
                this.CheckGenba();
            }
            if (!string.IsNullOrEmpty(this.dto.entryEntity.GENBA_NAME))
            {
                this.form.GENBA_NAME_RYAKU.Text = this.dto.entryEntity.GENBA_NAME.ToString();
            }

            // No.4101-->
            this.form.KUUSHA_JYURYO.Text = string.Empty;
            M_SHARYOU[] sharyouEntitys = null;
            sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null);
            if (sharyouEntitys != null && sharyouEntitys.Length == 1)
            {
                if (!sharyouEntitys[0].KUUSHA_JYURYO.IsNull)
                {
                    this.form.KUUSHA_JYURYO.Text = sharyouEntitys[0].KUUSHA_JYURYO.ToString();
                }
            }
            // No.4101<--

            // 形態区分
            if (!this.dto.entryEntity.KEITAI_KBN_CD.IsNull)
            {
                this.form.KEITAI_KBN_CD.Text = this.dto.entryEntity.KEITAI_KBN_CD.ToString();
            }
            // 形態区分名
            if (!this.dto.entryEntity.KEITAI_KBN_CD.IsNull && !string.IsNullOrEmpty(this.dto.keitaiKbnEntity.KEITAI_KBN_NAME_RYAKU))
            {
                this.form.KEITAI_KBN_NAME_RYAKU.Text = this.dto.keitaiKbnEntity.KEITAI_KBN_NAME_RYAKU;
            }
            // 荷降業者
            if (!string.IsNullOrEmpty(this.dto.entryEntity.NIOROSHI_GYOUSHA_CD))
            {
                this.form.NIOROSHI_GYOUSHA_CD.Text = this.dto.entryEntity.NIOROSHI_GYOUSHA_CD.ToString();
                this.form.beforNioroshiGyoushaCD = this.dto.entryEntity.NIOROSHI_GYOUSHA_CD.ToString();

                if (this.dto.entryEntity.KIHON_KEIRYOU == 1)
                {
                    this.CheckNioroshiGyoushaCd();
                }
                else if (this.dto.entryEntity.KIHON_KEIRYOU == 2)
                {

                }



            }
            if (!string.IsNullOrEmpty(this.dto.entryEntity.NIOROSHI_GYOUSHA_NAME))
            {
                this.form.NIOROSHI_GYOUSHA_NAME.Text = this.dto.entryEntity.NIOROSHI_GYOUSHA_NAME.ToString();
            }

            // 荷卸現場
            if (!string.IsNullOrEmpty(this.dto.entryEntity.NIOROSHI_GENBA_CD))
            {
                this.form.NIOROSHI_GENBA_CD.Text = this.dto.entryEntity.NIOROSHI_GENBA_CD.ToString();
                this.form.beforNioroshiGenbaCD = this.dto.entryEntity.NIOROSHI_GENBA_CD.ToString();
                if (this.dto.entryEntity.KIHON_KEIRYOU == 1)
                {
                    this.ChechNioroshiGenbaCd();
                }
                else if (this.dto.entryEntity.KIHON_KEIRYOU == 2)
                {

                }

            }
            if (!string.IsNullOrEmpty(this.dto.entryEntity.NIOROSHI_GENBA_NAME))
            {
                this.form.NIOROSHI_GENBA_NAME.Text = this.dto.entryEntity.NIOROSHI_GENBA_NAME.ToString();
            }
            // 荷積業者
            if (!string.IsNullOrEmpty(this.dto.entryEntity.NIZUMI_GYOUSHA_CD))
            {
                this.form.NIZUMI_GYOUSHA_CD.Text = this.dto.entryEntity.NIZUMI_GYOUSHA_CD.ToString();
                this.form.beforNizumiGyoushaCD = this.dto.entryEntity.NIZUMI_GYOUSHA_CD.ToString();
                if (this.dto.entryEntity.KIHON_KEIRYOU == 1)
                {

                }
                else if (this.dto.entryEntity.KIHON_KEIRYOU == 2)
                {
                    this.CheckNizumiGyoushaCd();
                }


            }
            if (!string.IsNullOrEmpty(this.dto.entryEntity.NIZUMI_GYOUSHA_NAME))
            {
                this.form.NIZUMI_GYOUSHA_NAME.Text = this.dto.entryEntity.NIZUMI_GYOUSHA_NAME.ToString();
            }

            // 荷積現場
            if (!string.IsNullOrEmpty(this.dto.entryEntity.NIZUMI_GENBA_CD))
            {
                this.form.NIZUMI_GENBA_CD.Text = this.dto.entryEntity.NIZUMI_GENBA_CD.ToString();
                this.form.beforNizumiGenbaCD = this.dto.entryEntity.NIZUMI_GENBA_CD.ToString();
                if (this.dto.entryEntity.KIHON_KEIRYOU == 1)
                {

                }
                else if (this.dto.entryEntity.KIHON_KEIRYOU == 2)
                {
                    this.ChechNioroshiGenbaCd();
                }

            }
            if (!string.IsNullOrEmpty(this.dto.entryEntity.NIZUMI_GENBA_NAME))
            {
                this.form.NIZUMI_GENBA_NAME.Text = this.dto.entryEntity.NIZUMI_GENBA_NAME.ToString();
            }
            // マニフェスト種類
            if (!this.dto.entryEntity.MANIFEST_SHURUI_CD.IsNull)
            {
                if (this.dto.entryEntity.MANIFEST_SHURUI_CD.Value == 0)
                {
                    this.form.MANIFEST_SHURUI_CD.Text = string.Empty;
                }
                else
                {
                    this.form.MANIFEST_SHURUI_CD.Text = this.dto.entryEntity.MANIFEST_SHURUI_CD.ToString();
                }

                var manifestShuruiEntity = this.accessor.GetManifestShurui(this.dto.entryEntity.MANIFEST_SHURUI_CD);
                if (manifestShuruiEntity != null && !string.IsNullOrEmpty(manifestShuruiEntity.MANIFEST_SHURUI_NAME_RYAKU))
                {
                    this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = manifestShuruiEntity.MANIFEST_SHURUI_NAME_RYAKU.ToString();
                }
                else
                {
                    this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = string.Empty;
                }
            }

            // マニフェスト手配
            if (!this.dto.entryEntity.MANIFEST_TEHAI_CD.IsNull)
            {
                if (this.dto.entryEntity.MANIFEST_TEHAI_CD.Value == 0)
                {
                    this.form.MANIFEST_TEHAI_CD.Text = string.Empty;
                }
                else
                {
                    this.form.MANIFEST_TEHAI_CD.Text = this.dto.entryEntity.MANIFEST_TEHAI_CD.ToString();
                }

                var manifestTehaiEntity = this.accessor.GetManifestTehai(this.dto.entryEntity.MANIFEST_TEHAI_CD);
                if (manifestTehaiEntity != null && !string.IsNullOrEmpty(manifestTehaiEntity.MANIFEST_TEHAI_NAME_RYAKU))
                {
                    this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = manifestTehaiEntity.MANIFEST_TEHAI_NAME_RYAKU.ToString();
                }
                else
                {
                    this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = string.Empty;
                }
            }

            // 伝票備考
            if (!string.IsNullOrEmpty(this.dto.entryEntity.DENPYOU_BIKOU))
            {
                this.form.DENPYOU_BIKOU.Text = this.dto.entryEntity.DENPYOU_BIKOU.ToString();
            }

            // NET_TOTAL
            if (!this.dto.entryEntity.NET_TOTAL.IsNull)
            {
                this.form.NET_TOTAL.Text = this.dto.entryEntity.NET_TOTAL.ToString();

            }


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
            if (!this.dto.entryEntity.UPDATE_DATE.IsNull
                && !string.IsNullOrEmpty(this.dto.entryEntity.UPDATE_DATE.ToString()))
            {
                headerForm.LastUpdateDate.Text = this.dto.entryEntity.UPDATE_DATE.ToString();
            }



            // 詳細 End

            // 明細 Start

            this.form.gcMultiRow1.BeginEdit(false);
            EventRun(false);
            this.form.gcMultiRow1.Rows.Clear();

            //CreateDataTableForEntityがMultiRowを動的に作成しないため、ここでEntity分行数を追加する
            //Entity数Rowを作ると最終行が無いので、Etity + 1でループさせる
            for (int i = 1; i < this.dto.detailEntity.Length + 1; i++)
            {
                this.form.gcMultiRow1.Rows.Add();
            }
            var dataBinder = new DataBinderLogic<T_KEIRYOU_DETAIL>(this.dto.detailEntity);
            dataBinder.CreateDataTableForEntity(this.form.gcMultiRow1, this.dto.detailEntity);

            // 選択されたセルの色をクリアする
            this.form.gcMultiRow1.Rows[this.form.gcMultiRow1.RowCount - 1].Cells[CELL_NAME_STAK_JYUURYOU].Style.BackColor = System.Drawing.Color.Empty;

            // MultiRowへ設定
            int k = 0;
            foreach (var row in this.form.gcMultiRow1.Rows)
            {
                short denpyouCd = 0;
                ICustomControl denpyouCdCell = (ICustomControl)row.Cells[CELL_NAME_DENPYOU_KBN_CD];
                if (short.TryParse(denpyouCdCell.GetResultText(), out denpyouCd)
                    && this.denpyouKbnDictionary.ContainsKey(denpyouCd))
                {
                    row.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = this.denpyouKbnDictionary[denpyouCd].DENPYOU_KBN_NAME_RYAKU;
                }

                ICustomControl youkiCdCell = (ICustomControl)row.Cells[CELL_NAME_YOUKI_CD];
                if (!string.IsNullOrEmpty(youkiCdCell.GetResultText())
                    && this.youkiDictionary.ContainsKey(youkiCdCell.GetResultText()))
                {
                    row.Cells[CELL_NAME_YOUKI_NAME_RYAKU].Value = this.youkiDictionary[youkiCdCell.GetResultText()].YOUKI_NAME_RYAKU;
                }

                short unitCd = 0;
                ICustomControl unitCdCell = (ICustomControl)row.Cells[CELL_NAME_NISUGATA_UNIT_CD];
                if (short.TryParse(unitCdCell.GetResultText(), out unitCd)
                    && this.unitDictionary.ContainsKey(unitCd))
                {
                    row.Cells[CELL_NAME_NISUGATA_NAME_RYAKU].Value = this.unitDictionary[unitCd].UNIT_NAME_RYAKU;
                }

                // マニフェスト.交付番号
                if (row.Cells[CELL_NAME_SYSTEM_ID].Value != null
                     && row.Cells[CELL_NAME_DETAIL_SYSTEM_ID].Value != null)
                {
                    string whereStrForMani = "RENKEI_DENSHU_KBN_CD = " + (short)DENSHU_KBN.KEIRYOU + " AND RENKEI_SYSTEM_ID = " + row.Cells[CELL_NAME_SYSTEM_ID].Value + " AND RENKEI_MEISAI_SYSTEM_ID = " + row.Cells[CELL_NAME_DETAIL_SYSTEM_ID].Value;
                    var manifestEntry = this.dto.manifestEntrys.Select(whereStrForMani);
                    if (manifestEntry != null && 0 < manifestEntry.Length)
                    {
                        // 一件しか取れないはずなので、最初の要素を取得
                        row.Cells[CELL_NAME_MANIFEST_ID].Value = manifestEntry[0][CELL_NAME_MANIFEST_ID];
                    }
                }

                k++;
            }

            // 計算
            if (!this.CalcDetail())
            {
                return false;
            }




            // 重量値系同期のためのデータをセット
            this.SetJyuuryouDataToDtoList();

            // 重量リストを使って重量値の更新
            this.SetJyuuryouDataToMultiRow(true);

            EventRun(true);
            this.form.gcMultiRow1.EndEdit();
            this.form.gcMultiRow1.NotifyCurrentCellDirty(false);


            // 明細 End

            // フッダー Start
            // フッダー End
            return true;

        }

        #endregion

        #region 業務処理

        /// <summary>
        /// Entity作成と登録処理
        /// </summary>
        /// <param name="taiyuuKbn">滞留登録区分</param>
        /// <param name="errorFlag"></param>
        /// <returns>true:成功, false:失敗</returns>
        public bool CreateEntityAndUpdateTables(bool taiyuuKbn, bool errorFlag)
        {
            try
            {
                // No.3850-->
                if (this.form.TairyuuNewFlg == true)
                {
                    // 滞留一覧からの新規データはUPDATEに戻す
                    this.form.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                }
                // No.3350<--

                // CreateEntityとそれぞれの更新処理でDB更新が発生するため、UIFormから
                // 排他制御する
                using (Transaction tran = new Transaction())
                {
                    switch (this.form.WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            this.CreateEntity(taiyuuKbn, false);
                            this.Regist(errorFlag);
                            break;

                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            this.CreateEntity(taiyuuKbn, false);
                            this.Update(errorFlag);
                            break;

                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            this.CreateEntity(taiyuuKbn, false);
                            this.LogicalDelete();
                            break;

                        default:
                            break;
                    }

                    // コミット
                    tran.Commit();

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E080", "");
                    return false;
                }
                else if (ex is SQLRuntimeException)
                {
                    // SQLエラー用メッセージを出力
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E093", "");
                    return false;
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
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
            this.accessor.UpdateKeiryouEntry(this.dto.entryEntity);

            /// 20141117 Houkakou 「更新日、登録日の見直し」　start
            this.dto.entryEntity.DELETE_FLG = true;
            this.dto.entryEntity.SEQ = this.dto.entryEntity.SEQ + 1;
            this.accessor.InsertKeiryouEntry(this.dto.entryEntity);

            for (int row = 0; row < this.dto.detailEntity.Length; row++)
            {
                this.dto.detailEntity[row].SEQ = this.dto.entryEntity.SEQ;
            }
            this.accessor.InsertKeiryouDetail(this.dto.detailEntity);
            /// 20141117 Houkakou 「更新日、登録日の見直し」　end

            LogUtility.DebugMethodStart();
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
        public virtual void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            this.accessor.InsertKeiryouEntry(this.dto.entryEntity);
            this.accessor.InsertKeiryouDetail(this.dto.detailEntity);
            if (this.hiRenbanRegistKbn.Equals(Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.INSERT))
            {
                this.accessor.InsertNumberDay(this.dto.numberDay);
            }
            else if (this.hiRenbanRegistKbn.Equals(Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.UPDATE))
            {
                this.accessor.UpdateNumberDay(this.dto.numberDay);
            }
            if (this.nenRenbanRegistKbn.Equals(Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.INSERT))
            {
                this.accessor.InsertNumberYear(this.dto.numberYear);
            }
            else if (this.nenRenbanRegistKbn.Equals(Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.UPDATE))
            {
                this.accessor.UpdateNumberYear(this.dto.numberYear);
            }


            if (KeiryouConstans.RYOSYUSYO_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Ryousyusyou))
            {
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
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            // TODO: 途中でエラーが発生してロールバックがされるか確認
            this.accessor.InsertKeiryouEntry(this.dto.entryEntity);
            this.accessor.UpdateKeiryouEntry(this.beforDto.entryEntity);
            this.accessor.InsertKeiryouDetail(this.dto.detailEntity);
            if (this.hiRenbanRegistKbn.Equals(Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.INSERT))
            {
                this.accessor.InsertNumberDay(this.dto.numberDay);
            }
            else if (this.hiRenbanRegistKbn.Equals(Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.UPDATE))
            {
                this.accessor.UpdateNumberDay(this.dto.numberDay);
            }
            if (this.nenRenbanRegistKbn.Equals(Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.INSERT))
            {
                this.accessor.InsertNumberYear(this.dto.numberYear);
            }
            else if (this.nenRenbanRegistKbn.Equals(Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.UPDATE))
            {
                this.accessor.UpdateNumberYear(this.dto.numberYear);
            }

            // TODO: S_NUMBER_RECEIPTの更新


            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// MultiRowのデータに対しROW_NOを採番します
        /// </summary>
        public void NumberingRowNo()
        {
            this.form.gcMultiRow1.BeginEdit(false);
            foreach (Row dr in this.form.gcMultiRow1.Rows)
            {
                dr.Cells[CELL_NAME_ROW_NO].Value = dr.Index + 1;
            }
            this.form.gcMultiRow1.EndEdit();
            this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
        }

        /// <summary>
        /// 総重量、空車重量の状態によって、割振、調整の入力制限を変更する
        /// </summary>
        internal bool ChangeWarihuriAndChouseiInputStatus()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                var targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return ret;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value)))
                {
                    this.ChangeTenyuuryoku(targetRow, false);
                }
                else
                {
                    this.ChangeTenyuuryoku(targetRow, true);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeWarihuriAndChouseiInputStatus", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
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
        public void MoveToNextControlForShokuchikbnCheck(ICustomControl control)
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
                    (row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value != null
                        && !string.IsNullOrEmpty(row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value.ToString()))
                    || (row.Cells[CELL_NAME_WARIFURI_PERCENT].Value != null
                        && !string.IsNullOrEmpty(row.Cells[CELL_NAME_WARIFURI_PERCENT].Value.ToString()))
                    || (row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value != null
                        && !string.IsNullOrEmpty(row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value.ToString()))
                    || (row.Cells[CELL_NAME_CHOUSEI_PERCENT].Value != null
                        && !string.IsNullOrEmpty(row.Cells[CELL_NAME_CHOUSEI_PERCENT].Value.ToString()))
                    )
                {
                    isReadOnlyForStackJyuuryou = true;
                    isReadOnlyForStackJyuuryou = true;
                }


                // 割振、調整のReadOnlyにデフォルト値が設定されるためここで新たに設定する
                bool isReadOnlyForWarihuriAndChousei = true;
                if (
                    (row.Cells[CELL_NAME_STAK_JYUURYOU].Value != null
                        && !string.IsNullOrEmpty(row.Cells[CELL_NAME_STAK_JYUURYOU].Value.ToString()))
                    || (row.Cells[CELL_NAME_STAK_JYUURYOU].Value != null
                        && !string.IsNullOrEmpty(row.Cells[CELL_NAME_STAK_JYUURYOU].Value.ToString()))
                    )
                {
                    isReadOnlyForWarihuriAndChousei = false;

                    isReadOnlyForStackJyuuryou = false;
                    isReadOnlyForStackJyuuryou = false;
                }

                row.Cells[CELL_NAME_STAK_JYUURYOU].ReadOnly = isReadOnlyForStackJyuuryou;
                row.Cells[CELL_NAME_EMPTY_JYUURYOU].ReadOnly = isReadOnlyForStackJyuuryou;


                // 割振計算された行用
                int k = 0;
                if (int.TryParse(Convert.ToString(row.Cells[CELL_NAME_WARIHURIROW_NO].Value), out k)
                    && 0 < k)
                {
                    isReadOnlyForWarihuriAndChousei = false;
                }

                row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].ReadOnly = isReadOnlyForWarihuriAndChousei;
                row.Cells[CELL_NAME_CHOUSEI_PERCENT].ReadOnly = isReadOnlyForWarihuriAndChousei;
            }  
            catch (Exception ex)
            {
                LogUtility.Error("ChangeInputStatusForChousei", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 手入力変更処理
        /// </summary>
        internal void ChangeJyuuryouEnabled()
        {
            LogUtility.DebugMethodStart();

            var targetRow = this.form.gcMultiRow1.CurrentRow;

            if (targetRow == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value))
                || !string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value)))
            {
                this.ChangeTenyuuryoku(targetRow, true);
            }
            else
            {
                this.ChangeTenyuuryoku(targetRow, false);
            }

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 総重量、空車重量、正味重量の入力制限を変更不可に設定する
        /// </summary>
        internal void ChangeJyuuryouDisabled()
        {
            LogUtility.DebugMethodStart();

            var targetRow = this.form.gcMultiRow1.CurrentRow;

            this.ChangeTenyuuryoku(targetRow, true);

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 手入力変更処理
        /// </summary>
        /// <param name="targetRow"></param>
        /// <param name="isReadOnly">ture: 手入力可, false:手入力不可</param>
        internal void ChangeTenyuuryoku(Row targetRow, bool isReadOnly)
        {
            LogUtility.DebugMethodStart(targetRow, isReadOnly);
            this.form.gcMultiRow1.BeginEdit(false);

            if (targetRow == null)
            {
                return;
            }

            /**
             * 手入力可能とする
             */
            this.EventRun(false);


            targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].ReadOnly = isReadOnly;
            targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].UpdateBackColor(isReadOnly);
            targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value = string.Empty;

            targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].ReadOnly = isReadOnly;
            targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].UpdateBackColor(isReadOnly);
            targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].Value = string.Empty;

            targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].ReadOnly = isReadOnly;
            targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].UpdateBackColor(isReadOnly);
            targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value = string.Empty;

            targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].ReadOnly = isReadOnly;
            targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].UpdateBackColor(isReadOnly);
            targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value = string.Empty;


            this.EventRun(true);

            this.form.gcMultiRow1.EndEdit();
            this.form.gcMultiRow1.NotifyCurrentCellDirty(false);



            LogUtility.DebugMethodEnd();

        }
        /// <summary>
        /// 手入力
        /// </summary>
        /// <param name="isReadOnly"></param>
        internal void Tenyuuryoku(bool isReadOnly)
        {
            LogUtility.DebugMethodStart(isReadOnly);

            this.Tenyuuryoku(null, new string[] { "STACK_JYUURYOU", "EMPTY_JYUURYOU", "NET_JYUURYOU", "WARIFURI_JYUURYOU", "WARIFURI_PERCENT", "CHOUSEI_JYUURYOU", "CHOUSEI_PERCENT" }, "ReadOnly", isReadOnly);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 明細の金額、重量計算
        /// 金額、重量計算をまとめて処理します
        /// </summary>
        internal bool CalcDetail()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 割振計算
                if (!this.ExecuteWarifuri(true))
                {
                    ret = false;
                    return ret;
                }

                // 調整kg(片方だけ計算すればいいはず)
                if (!this.CalcChouseiJyuuryou())
                {
                    ret = false;
                    return ret;
                }

                // 容器数量(手入力した場合の重量が再計算されてしまう為、実行不可)
                //this.CalcYoukiSuuryou();

                // 容器重量
                if (!this.CalcYoukiJyuuryou())
                {
                    ret = false;
                    return ret;
                }

                // 合計系金額計算
                if (!this.CalcTotalValues())
                {
                    ret = false;
                    return ret;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcDetail", ex);
                this.errmessage.MessageBoxShow("E245", "");
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
        internal bool CalcStackOrEmptyJyuuryou()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return ret;
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

                targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = stackJyuuryou - emptyJyuuryou - chouseiJyuuryou - youkiJyuuryou;

                this.form.gcMultiRow1.EndEdit();
                this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcStackOrEmptyJyuuryou", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 合計系の計算
        /// </summary>
        internal bool CalcTotalValues()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                decimal netTotal = 0;
                foreach (Row dr in this.form.gcMultiRow1.Rows)
                {
                    decimal netJyuuryou = 0;

                    decimal.TryParse(Convert.ToString(dr.Cells[CELL_NAME_NET_JYUURYOU].Value), out netJyuuryou);

                    // 正味重量計算
                    netTotal += netJyuuryou;

                }

                this.form.NET_TOTAL.Text = netTotal.ToString("N");
                CustomTextBoxLogic customTextBoxLogic = new CustomTextBoxLogic(this.form.NET_TOTAL);
                customTextBoxLogic.Format(this.form.NET_TOTAL);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcTotalValues", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 調整Kg更新後の計算
        /// </summary>
        internal bool CalcChouseiJyuuryou()
        {
            try
            {
                LogUtility.DebugMethodStart();

                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return true;
                }

                this.form.gcMultiRow1.BeginEdit(false);
                decimal criterionNet = this.GetCriterionNetForCurrent();    // 基準正味
                if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value)))
                {
                    // 紐付くデータの削除
                    targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value = string.Empty;
                    targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = criterionNet;
                    if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value))
                        && !string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value)))
                    {
                        decimal netJyuuryou = 0;
                        decimal youkiJyuuryou = 0;
                        var netTryPaseResult = decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value), out netJyuuryou);
                        var youkiTryPaseResult = decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou);
                        if (!netTryPaseResult && !youkiTryPaseResult)
                        {
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
                    decimal chouseiPercent = 0;
                    if (decimal.TryParse(Convert.ToString((chouseiJyuuryou / criterionNet) * 100), out chouseiPercent))
                    {
                        if (SqlInt16.Parse(this.form.KIHON_KEIRYOU.Text) == 1)
                        {   // 受入
                            chouseiPercent = (Shougun.Core.Scale.Keiryou.Utility.CommonCalc.FractionCalc((decimal)(chouseiJyuuryou / criterionNet) * 100, (int)this.dto.sysInfoEntity.UKEIRE_CHOUSEI_WARIAI_HASU_CD, (short)this.dto.sysInfoEntity.UKEIRE_CHOUSEI_WARIAI_HASU_KETA));
                        }
                        else
                        {   // 出荷
                            chouseiPercent = (Shougun.Core.Scale.Keiryou.Utility.CommonCalc.FractionCalc((decimal)(chouseiJyuuryou / criterionNet) * 100, (int)this.dto.sysInfoEntity.SHUKKA_CHOUSEI_WARIAI_HASU_CD, (short)this.dto.sysInfoEntity.SHUKKA_CHOUSEI_WARIAI_HASU_KETA));
                        }
                        targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value = chouseiPercent;

                        // 正味重量計算
                        decimal youkiJyuuryou = 0;    // 容器重量
                        decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou);
                        targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = criterionNet - chouseiJyuuryou - youkiJyuuryou;
                    }
                }

                this.form.gcMultiRow1.EndEdit();
                this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcChouseiJyuuryou", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 調整%更新後の計算
        /// </summary>
        internal bool CalcChouseiPercent()
        {
            try
            {
                LogUtility.DebugMethodStart();

                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                this.form.gcMultiRow1.BeginEdit(false);

                var criterionNet = this.GetCriterionNetForCurrent();    // 基準正味
                if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value)))
                {
                    // 紐付くデータの削除
                    targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value = string.Empty;
                    targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = criterionNet;

                    if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value))
                        && !string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value)))
                    {
                        decimal netJyuuryou = 0;
                        decimal youkiJyuuryou = 0;
                        decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value), out netJyuuryou);
                        decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou);

                        targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = ((decimal)criterionNet - (decimal)youkiJyuuryou);
                    }
                }
                else
                {
                    decimal chouseiPercent = 0;  // 調整%
                    decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Value), out chouseiPercent);
                    decimal criterionNetCalcResult = (decimal)(criterionNet * (chouseiPercent / 100));
                    if (SqlInt16.Parse(this.form.KIHON_KEIRYOU.Text) == 1)
                    {   // 受入
                        criterionNetCalcResult = Shougun.Core.Scale.Keiryou.Utility.CommonCalc.FractionCalc(criterionNetCalcResult, (int)this.dto.sysInfoEntity.UKEIRE_CHOUSEI_HASU_CD, (short)this.dto.sysInfoEntity.UKEIRE_CHOUSEI_HASU_KETA);
                    }
                    else
                    {   // 出荷
                        criterionNetCalcResult = Shougun.Core.Scale.Keiryou.Utility.CommonCalc.FractionCalc(criterionNetCalcResult, (int)this.dto.sysInfoEntity.SHUKKA_CHOUSEI_HASU_CD, (short)this.dto.sysInfoEntity.SHUKKA_CHOUSEI_HASU_KETA);
                    }

                    targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value = criterionNetCalcResult;

                    // 正味重量計算
                    decimal youkiJyuuryou = 0;    // 容器重量
                    decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou);
                    targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = (decimal)criterionNet - criterionNetCalcResult - (decimal)youkiJyuuryou;
                }
                this.form.gcMultiRow1.EndEdit();
                this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcChouseiPercent", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 容器重量を計算
        /// 容器重量を手入力した場合は実行不可！
        /// </summary>
        internal bool CalcYoukiSuuryou()
        {
            try
            {
                LogUtility.DebugMethodStart();
                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                M_YOUKI youki = this.accessor.GetYouki((Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_CD].Value)));

                // 容器数量用処理
                decimal youkiJyuryou = 0;     // 容器重量(容器)
                decimal youkiSuuryou = 0;     // 容器数量(受入明細)

                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_SUURYOU].Value), out youkiSuuryou);

                if (youki != null)
                {
                    decimal tempJyuryou = 0;
                    decimal.TryParse(Convert.ToString(youki.YOUKI_JYURYO), out tempJyuryou);
                    youkiJyuryou = tempJyuryou;

                    // 容器重量設定(受入明細)
                    decimal youkiJyuuryouForCell = youkiJyuryou * youkiSuuryou;   // 正味重量の計算があるため変数に設定
                    targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value = youkiJyuuryouForCell;
                }

                if (!this.CalcYoukiJyuuryou())
                {
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcYoukiSuuryou", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcYoukiSuuryou", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 基準正味重量計算
        /// </summary>
        internal bool CalcYoukiJyuuryou()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return ret;
                }

                // 容器重量用処理
                decimal youkiJyuryou = 0;     // 容器重量(容器)
                decimal stakJyuuryou = 0;
                decimal emptyJyuuryou = 0;
                decimal warifuriJyuuryou = 0;



                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuryou);
                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stakJyuuryou);
                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou);
                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value), out warifuriJyuuryou);

                if (
                    !decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuryou)
                    && !decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stakJyuuryou)
                    && !decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou)
                    && !decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value), out warifuriJyuuryou)
                    )
                {
                    return ret;
                }

                if ((0 <= stakJyuuryou)
                    || 0 <= warifuriJyuuryou)
                {
                    decimal criterionNet = this.GetCriterionNetForCurrent();    // 基準正味
                    decimal chouseiJyuuryou = 0;
                    decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value), out chouseiJyuuryou);

                    if (criterionNet - chouseiJyuuryou - youkiJyuryou < 0)
                    {
                        targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = string.Empty;
                        return ret;
                    }

                    targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value = criterionNet - chouseiJyuuryou - youkiJyuryou;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcYoukiJyuuryou", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 割振計算処理
        /// </summary>
        /// <param name="isWarihuriJyuuryou">ture: 割振kg基点, false: 割振(%)基点</param>
        internal bool ExecuteWarifuri(bool isWarihuriJyuuryou)
        {
            /** 
             * warifuriNo   ：jyuuryouDtoListのindex
             * warifuriRowNo：jyuuryouDtoList内の1要素内のindex
             * **/

            try
            {
                LogUtility.DebugMethodStart(isWarihuriJyuuryou);

                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                int warifuriNo = -1;
                short warifuriRowNo = -1;

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_WARIHURI_NO].Value)))
                {
                    int.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIHURI_NO].Value), out warifuriNo);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_WARIHURIROW_NO].Value)))
                {
                    short.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIHURIROW_NO].Value), out warifuriRowNo);
                }

                // jyuuryouDtoListを初期化
                this.SetJyuuryouDataToDtoList();

                warifuriNo = -1;
                warifuriRowNo = -1;

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_WARIHURI_NO].Value)))
                {
                    int.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIHURI_NO].Value), out warifuriNo);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_WARIHURIROW_NO].Value)))
                {
                    short.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIHURIROW_NO].Value), out warifuriRowNo);
                }

                object checkTargetValue = null;

                if (isWarihuriJyuuryou)
                {
                    checkTargetValue = targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value;
                }
                else
                {
                    checkTargetValue = targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].Value;
                }

                if (warifuriNo < 0 || warifuriRowNo < 0)
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                if (checkTargetValue == null || string.IsNullOrEmpty(Convert.ToString(checkTargetValue)))
                {
                    // 削除時の処理

                    /**
                     * 割振が設定されていない場合は、「総重量」「空車重量」を編集可にする。
                     */
                    targetRow.Cells[CELL_NAME_STAK_JYUURYOU].ReadOnly = false;
                    targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].ReadOnly = false;

                    targetRow.Cells[CELL_NAME_STAK_JYUURYOU].UpdateBackColor(false);
                    targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].UpdateBackColor(false);

                    // 重量値リストの更新
                    // 対象のJyuuryouDtoより下を削除
                    if (warifuriNo < this.jyuuryouDtoList.Count)
                    {
                        var jyuuryouDtos = this.jyuuryouDtoList[warifuriNo];
                        int i = warifuriRowNo + 1;
                        while (i < jyuuryouDtos.Count)
                        {
                            jyuuryouDtos.RemoveAt(i);
                        }

                        if (0 < warifuriRowNo)
                        {
                            // 自分自身を削除
                            jyuuryouDtos.RemoveAt(warifuriRowNo);
                            // 再計算のため1つ上の割振を削除
                            jyuuryouDtos[warifuriRowNo - 1].warifuriJyuuryou = null;
                            jyuuryouDtos[warifuriRowNo - 1].warifuriPercent = null;
                        }
                        else
                        {
                            // 先頭行の場合は自分の割振kgと割振%を削除するだけ
                            jyuuryouDtos[warifuriRowNo].warifuriJyuuryou = null;
                            jyuuryouDtos[warifuriRowNo].warifuriPercent = null;
                        }
                    }

                    // 再計算処理
                    foreach (var jyuuryouDtoList in this.jyuuryouDtoList)
                    {
                        if (SqlInt16.Parse(this.form.KIHON_KEIRYOU.Text) == 1)
                        {   // 受入
                            JyuuryouDto.CalcJyuuryouDtoForAdd(
                                jyuuryouDtoList,
                                isWarihuriJyuuryou,
                                (short)this.dto.sysInfoEntity.UKEIRE_WARIFURI_HASU_CD,
                                (short)this.dto.sysInfoEntity.UKEIRE_WARIFURI_HASU_KETA,
                                (short)this.dto.sysInfoEntity.UKEIRE_WARIFURI_WARIAI_HASU_CD,
                                (short)this.dto.sysInfoEntity.UKEIRE_WARIFURI_WARIAI_HASU_KETA);
                        }
                        else
                        {   // 出荷
                            JyuuryouDto.CalcJyuuryouDtoForAdd(
                                jyuuryouDtoList,
                                isWarihuriJyuuryou,
                                (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_CD,
                                (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_KETA,
                                (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_CD,
                                (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_KETA);
                        }
                    }
                }
                else
                {

                    // 追加時の処理
                    decimal readOnly = 0;
                    decimal stackJyuuryou = 0;
                    decimal emptyJyuuryou = 0;
                    decimal warifuriJyuuryou = 0;
                    decimal warifuriPercent = 0;

                    JyuuryouDto jyuuryouDto = new JyuuryouDto();

                    if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stackJyuuryou))
                    {
                        jyuuryouDto.stackJyuuryou = stackJyuuryou;
                    }

                    if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou))
                    {
                        jyuuryouDto.emptyJyuuryou = emptyJyuuryou;
                    }

                    if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value), out warifuriJyuuryou))
                    {
                        jyuuryouDto.warifuriJyuuryou = warifuriJyuuryou;
                    }

                    if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].Value), out warifuriPercent))
                    {
                        jyuuryouDto.warifuriPercent = warifuriPercent;
                    }


                    //if (stackJyuuryou > 0 || emptyJyuuryou > 0)
                    //{

                    //    /**
                    //     * 割振が設定されている場合は、「総重量」「空車重量」を編集可にする。
                    //     */
                    //    targetRow.Cells[CELL_NAME_STACK_JYUURYOU].ReadOnly = false;
                    //    targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].ReadOnly = false;

                    //}
                    //else
                    //{

                    //    /**
                    //    * 割振が設定されている場合は、「総重量」「空車重量」を編集不可にする。
                    //    */
                    //    targetRow.Cells[CELL_NAME_STACK_JYUURYOU].ReadOnly = true;
                    //    targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].ReadOnly = true;

                    //}

                    /**
                    * 割振が設定されている場合は、「総重量」「空車重量」を編集不可にする。
                    */
                    targetRow.Cells[CELL_NAME_STAK_JYUURYOU].ReadOnly = true;
                    targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].ReadOnly = true;

                    // 重量リストの更新
                    this.AddJyuuryouDataList(
                        warifuriNo,
                        warifuriRowNo,
                        jyuuryouDto,
                        isWarihuriJyuuryou
                    );

                }

                // 重量リストを使って重量値の更新
                this.SetJyuuryouDataToMultiRow(false);

                // 調整kg, 容器重量は移動しないため、重量値を再計算
                this.SetJyuuryouDataToDtoList();
                foreach (var jyuuryouDtoList in this.jyuuryouDtoList)
                {
                    if (SqlInt16.Parse(this.form.KIHON_KEIRYOU.Text) == 1)
                    {   // 受入
                        JyuuryouDto.CalcJyuuryouDtoForAdd(
                            jyuuryouDtoList,
                            isWarihuriJyuuryou,
                            (short)this.dto.sysInfoEntity.UKEIRE_WARIFURI_HASU_CD,
                            (short)this.dto.sysInfoEntity.UKEIRE_WARIFURI_HASU_KETA,
                            (short)this.dto.sysInfoEntity.UKEIRE_WARIFURI_WARIAI_HASU_CD,
                            (short)this.dto.sysInfoEntity.UKEIRE_WARIFURI_WARIAI_HASU_KETA);
                    }
                    else
                    {   // 出荷
                        JyuuryouDto.CalcJyuuryouDtoForAdd(
                            jyuuryouDtoList,
                            isWarihuriJyuuryou,
                            (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_CD,
                            (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_HASU_KETA,
                            (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_CD,
                            (short)this.dto.sysInfoEntity.SHUKKA_WARIFURI_WARIAI_HASU_KETA);
                    }
                }
                this.SetJyuuryouDataToMultiRow(false);
                this.NumberingRowNo();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ExecuteWarifuri", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// イベント発生、非発生を設定
        /// <blnEvent>true: 発生, false:発生しない</returns>
        /// </summary>
        internal void EventRun(bool blnEvent)
        {
            LogUtility.DebugMethodStart(blnEvent);

            if (blnEvent)
            {
                this.form.gcMultiRow1.CellValidating += new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.form.gcMultiRow1_CellValidating);
                this.form.gcMultiRow1.CellValidated += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.form.gcMultiRow1_OnValidated);
                this.form.gcMultiRow1.CellEnter += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.form.gcMultiRow1_OnCellEnter);
                this.form.gcMultiRow1.RowEnter += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.form.gcMultiRow1_OnRowEnter);
                this.form.gcMultiRow1.RowLeave += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.form.gcMultiRow1_Leave);
            }
            else
            {
                this.form.gcMultiRow1.CellValidating -= new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.form.gcMultiRow1_CellValidating);
                this.form.gcMultiRow1.CellValidated -= new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.form.gcMultiRow1_OnValidated);
                this.form.gcMultiRow1.CellEnter -= new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.form.gcMultiRow1_OnCellEnter);
                this.form.gcMultiRow1.RowEnter -= new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.form.gcMultiRow1_OnRowEnter);
                this.form.gcMultiRow1.RowLeave -= new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.form.gcMultiRow1_Leave);
            }
            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// 割振重量入力値チェック
        /// </summary>
        /// <returns>true: 問題なし, false:問題あり</returns>
        internal bool ValidateWarifuriJyuuryou()
        {
            LogUtility.DebugMethodStart();

            bool returnVal = false;

            try
            {
                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return returnVal;
                }

                decimal stackJyuuryou = 0;
                decimal emptyJyuuryou = 0;
                decimal warihuriJyuuryou = 0;

                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].EditedFormattedValue), out warihuriJyuuryou))
                {
                    if (warihuriJyuuryou == 0)
                    {
                        ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU], true);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", "0以上");
                        return returnVal;
                    }
                }
                else if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].EditedFormattedValue)))
                {
                    // Null or 空は許容しているのでスルー
                    returnVal = true;
                    return returnVal;
                }

                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].EditedFormattedValue), out stackJyuuryou)
                    && decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].EditedFormattedValue), out emptyJyuuryou))
                {
                    // 全部値がある場合にだけチェック
                    if (0 == (stackJyuuryou - emptyJyuuryou))
                    {
                        ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU], true);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E082", "総重量、空車重量");
                        return returnVal;
                    }
                    else if ((stackJyuuryou - emptyJyuuryou) <= warihuriJyuuryou)
                    {
                        ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU], true);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", (stackJyuuryou - emptyJyuuryou).ToString() + "未満");
                        return returnVal;
                    }
                }

                ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU], false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateWarifuriJyuuryou", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return returnVal;
            }
            LogUtility.DebugMethodEnd();
            returnVal = true;
            return returnVal;

        }

        /// <summary>
        /// 割振%入力値チェック
        /// </summary>
        /// <returns>true: 問題なし, false:問題あり</returns>
        internal bool ValidateWarifuriPercent()
        {
            LogUtility.DebugMethodStart();
            bool returnVal = false;

            try
            {
                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return returnVal;
                }

                if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].EditedFormattedValue)))
                {
                    return true;
                }

                decimal stackJyuuryou = 0;
                decimal emptyJyuuryou = 0;

                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].EditedFormattedValue), out stackJyuuryou)
                    && decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].EditedFormattedValue), out emptyJyuuryou))
                {
                    if (0 == (stackJyuuryou - emptyJyuuryou))
                    {

                        ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_WARIFURI_PERCENT], true);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E082", "総重量、空車重量");
                        return returnVal;
                    }
                }

                decimal warifuriPercent = 0;

                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].EditedFormattedValue), out warifuriPercent))
                {
                    if (100 <= warifuriPercent)
                    {
                        ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_WARIFURI_PERCENT], true);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", "100未満");
                        return returnVal;
                    }

                    //if (warifuriPercent == 0)
                    //{
                    //    ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_WARIFURI_PERCENT], true);
                    //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    //    msgLogic.MessageBoxShow("E048", "0以上");
                    //    return returnVal;
                    //}
                }
                ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_WARIFURI_PERCENT], false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateWarifuriPercent", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return returnVal;
            }

            LogUtility.DebugMethodEnd();
            returnVal = true;
            return returnVal;
        }
        /// <summary>
        /// 総重量入力値チェック
        /// </summary>
        /// <returns>true: 問題なし, false:問題あり</returns>
        internal bool ValidateStackJyuuryou()
        {
            LogUtility.DebugMethodStart();
            bool returnVal = false;

            try
            {
                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return returnVal;
                }

                decimal stackJyuuryou = 0;
                decimal emptyJyuuryou = 0;
                decimal chouseiJyuuryou = 0;
                decimal warihuriJyuuryou = 0;

                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].EditedFormattedValue), out stackJyuuryou);
                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].EditedFormattedValue), out emptyJyuuryou);
                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].EditedFormattedValue), out chouseiJyuuryou);
                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].EditedFormattedValue), out warihuriJyuuryou);

                /**
                 * 総重量必須チェック
                 */
                if (stackJyuuryou == 0 && (chouseiJyuuryou > 0 || warihuriJyuuryou > 0) && emptyJyuuryou == 0)
                {

                    ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_STAK_JYUURYOU], true);
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E048", (emptyJyuuryou).ToString() + "以上");

                    return returnVal;
                }

                ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_STAK_JYUURYOU], false);

                if (!this.ValidateChouseiJyuuryou())
                {
                    EventRun(false);
                    targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Selected = true;
                    EventRun(true);

                    return returnVal;
                }
                ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU], false);

                if (!this.ValidateChouseiPercent())
                {
                    EventRun(false);
                    targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Selected = true;
                    EventRun(true);

                    return returnVal;
                }
                ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT], false);

                if (!this.ValidateWarifuriJyuuryou())
                {
                    EventRun(false);
                    targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].Selected = true;
                    EventRun(true);

                    return returnVal;
                }
                ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU], false);

                if (!this.ValidateWarifuriPercent())
                {

                    EventRun(false);
                    targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].Selected = true;
                    EventRun(true);

                    return returnVal;
                }
                ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_WARIFURI_PERCENT], false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateStackJyuuryou", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return returnVal;
            }

            LogUtility.DebugMethodEnd();
            returnVal = true;
            return returnVal;
        }

        /// <summary>
        /// 空車重量入力値チェック
        /// </summary>
        /// <returns>true: 問題なし, false:問題あり</returns>
        internal bool ValidateEmpyJyuuryou()
        {
            LogUtility.DebugMethodStart();
            bool returnVal = false;

            try
            {
                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return returnVal;
                }

                decimal readOnly = 0;
                decimal stackJyuuryou = 0;
                decimal emptyJyuuryou = 0;

                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].EditedFormattedValue), out stackJyuuryou);
                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].EditedFormattedValue), out emptyJyuuryou);


                // 処理しない
                if ((stackJyuuryou == 0 || emptyJyuuryou == 0) && readOnly == 0)
                {

                    return true;
                }

                ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU], false);

                if (!this.ValidateChouseiJyuuryou())
                {
                    EventRun(false);
                    targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Selected = true;
                    EventRun(true);

                    return returnVal;
                }
                ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU], false);

                if (!this.ValidateChouseiPercent())
                {
                    EventRun(false);
                    targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].Selected = true;
                    EventRun(true);

                    return returnVal;
                }
                ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT], false);

                if (!this.ValidateWarifuriJyuuryou())
                {
                    EventRun(false);
                    targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].Selected = true;
                    EventRun(true);

                    return returnVal;
                }
                ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU], false);

                if (!this.ValidateWarifuriPercent())
                {

                    EventRun(false);
                    targetRow.Cells[CELL_NAME_WARIFURI_PERCENT].Selected = true;
                    EventRun(true);

                    return returnVal;
                }

                ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_WARIFURI_PERCENT], false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateEmpyJyuuryou", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return returnVal;
            }

            LogUtility.DebugMethodEnd();
            returnVal = true;
            return returnVal;
        }

        /// <summary>
        /// 調整kg入力値チェック
        /// </summary>
        /// <returns>true: 問題なし, false:問題あり</returns>
        internal bool ValidateChouseiJyuuryou()
        {
            LogUtility.DebugMethodStart();
            bool returnVal = false;

            try
            {
                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return returnVal;
                }

                decimal stackJyuuryou = 0;
                decimal emptyJyuuryou = 0;
                decimal warihuriJyuuryou = 0;
                decimal chouseiJyuuryou = 0;

                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].EditedFormattedValue), out chouseiJyuuryou))
                {
                    if (chouseiJyuuryou == 0)
                    {
                        ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU], true);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", "0以上");
                        this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }
                }
                else if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].EditedFormattedValue)))
                {
                    // Null or 空は許容しているのでスルー
                    returnVal = true;
                    return returnVal;
                }

                /**
                 * 総重量-空車重量の値　又は　割振Kgが入力されている場合
                 */
                if ((decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stackJyuuryou)
                    && decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou)))
                {
                    // 全部値がある場合にだけチェック
                    if (0 == (stackJyuuryou - emptyJyuuryou))
                    {
                        ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU], true);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E082", "総重量、空車重量");
                        this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }
                    else if ((stackJyuuryou - emptyJyuuryou) <= chouseiJyuuryou)
                    {
                        ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU], true);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", (stackJyuuryou - emptyJyuuryou).ToString() + "未満");
                        targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Selected = true;
                        this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }
                }

                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].EditedFormattedValue), out warihuriJyuuryou))
                {
                    if (warihuriJyuuryou <= chouseiJyuuryou)
                    {

                        ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU], true);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", (warihuriJyuuryou.ToString() + "未満"));
                        this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }
                }
                ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_CHOUSEI_JYUURYOU], false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateChouseiJyuuryou", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return returnVal;
            }

            LogUtility.DebugMethodEnd();
            returnVal = true;
            return returnVal;
        }

        /// <summary>
        /// 調整(%)入力値チェック
        /// </summary>
        /// <returns>true: 問題なし, false:問題あり</returns>
        internal bool ValidateChouseiPercent()
        {
            LogUtility.DebugMethodStart();
            bool returnVal = false;

            try
            {
                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return returnVal;
                }

                if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].EditedFormattedValue)))
                {
                    return true;
                }

                decimal stackJyuuryou = 0;
                decimal emptyJyuuryou = 0;
                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].EditedFormattedValue), out stackJyuuryou)
                    && decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].EditedFormattedValue), out emptyJyuuryou))
                {
                    if (0 == (stackJyuuryou - emptyJyuuryou))
                    {
                        ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT], true);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E082", "総重量、空車重量");
                        this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }
                }

                decimal chouseiPercent = 0;

                if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT].EditedFormattedValue), out chouseiPercent))
                {
                    if (100 <= chouseiPercent)
                    {
                        ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT], true);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", "100未満");
                        this.SetJyuuryouDataToDtoList();
                        return returnVal;
                    }

                    //if (chouseiPercent == 0)
                    //{

                    //    ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT], true);
                    //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    //    msgLogic.MessageBoxShow("E048", "0以上");
                    //    this.SetJyuuryouDataToDtoList();
                    //    return returnVal;
                    //}
                }

                ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_CHOUSEI_PERCENT], false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateChouseiPercent", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return returnVal;
            }
            returnVal = true;
            LogUtility.DebugMethodEnd();
            return returnVal;
        }


        /// <summary>
        /// 品名入力値チェック
        /// </summary>
        /// <returns>true: 問題なし, false:問題あり</returns>
        internal bool ValidateHinmeiName()
        {
            LogUtility.DebugMethodStart();
            bool returnVal = false;

            try
            {
                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return returnVal;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].EditedFormattedValue)))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_NAME].EditedFormattedValue)))
                    {
                        ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_HINMEI_NAME], true);
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E012", "品名");
                        return returnVal;

                    }
                }
                ControlUtility.SetInputErrorOccuredForMultiRow(targetRow.Cells[CELL_NAME_HINMEI_NAME], false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateHinmeiName", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return returnVal;
            }

            returnVal = true;
            LogUtility.DebugMethodEnd();
            return returnVal;
        }

        /// <summary>
        /// 重量値のフォーマットチェック
        /// </summary>
        /// <param name="targetRow">対象のRow</param>
        /// <param name="cellName">対象のCell名</param>
        /// <returns></returns>
        internal bool ValidateJyuryouFormat(Row targetRow, string cellName)
        {
            bool returnVal = false;
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
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateJyuryouFormat", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return returnVal;
            }

            returnVal = true;
            return returnVal;
        }

        /// <summary>
        /// 品名コードより品名再取得
        /// </summary>
        /// <param name="hinmeiCd"></param>
        /// <returns>品名略称</returns>
        internal string SearchHinmei(string hinmeiCd, out bool catchErr)
        {
            LogUtility.DebugMethodStart(hinmeiCd);

            string returnValue = string.Empty;
            catchErr = true;
            try
            {
                M_HINMEI hinmei = this.accessor.GetHinmeiDataByCd(hinmeiCd);
                if (!string.IsNullOrEmpty(hinmei.HINMEI_NAME_RYAKU))
                {
                    returnValue = hinmei.HINMEI_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SearchHinmei", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchHinmei", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(returnValue, catchErr);
            return returnValue;
        }

        /// <summary>
        /// 品名コードより品名取得
        /// </summary>
        /// <param name="hinmeiCd"></param>
        /// <returns>品名</returns>
        internal string GetHinmei(string hinmeiCd)
        {
            LogUtility.DebugMethodStart(hinmeiCd);

            string returnValue = string.Empty;
            M_HINMEI hinmei = this.accessor.GetHinmeiDataByCd(hinmeiCd);
            if (!string.IsNullOrEmpty(hinmei.HINMEI_NAME))
            {
                returnValue = hinmei.HINMEI_NAME;
            }

            LogUtility.DebugMethodEnd();
            return returnValue;
        }

        /// <summary>
        /// 単位CD検索&設定
        /// </summary>
        /// <param name="hinmeiChangedFlg">品名CDが更新された後の処理かどうか</param>
        internal bool SearchAndCalcForUnit(bool hinmeiChangedFlg)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(hinmeiChangedFlg);

                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    return ret;
                }

                M_UNIT targetUnit = null;

                if (hinmeiChangedFlg)
                {
                    // 品名CD更新時
                    if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[CELL_NAME_HINMEI_CD].Value)))
                    {
                        return ret;
                    }

                    M_HINMEI hinmei = this.accessor.GetHinmeiDataByCd(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());

                    if (hinmei == null || string.IsNullOrEmpty(hinmei.HINMEI_CD))
                    {
                        return ret;
                    }

                    M_UNIT[] units = null;
                    short UnitCd = 0;
                    if (short.TryParse(hinmei.UNIT_CD.ToString(), out UnitCd))
                        units = this.accessor.GetUnit(UnitCd);

                    if (units == null)
                    {
                        return ret;
                    }
                    else
                    {
                        targetUnit = units[0];
                    }

                    if (string.IsNullOrEmpty(targetUnit.UNIT_NAME))
                    {
                        return ret;
                    }

                }
                else
                {
                    // 単位CD更新時
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SearchAndCalcForUnit", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchAndCalcForUnit", ex);
                this.errmessage.MessageBoxShow("E245", "");
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
        internal void CalcSuuryou(Row targetRow)
        {
            LogUtility.DebugMethodStart(targetRow);

            if (targetRow == null)
            {
                return;
            }

            /**
             * 数量設定
             */
            //if (string.Compare(KeiryouConstans.UNIT_NAME_KG,
            //    Convert.ToString(targetRow.Cells[CELL_NAME_NISUGATA_NAME_RYAKU].Value), true) == 0)
            //{
            //    targetRow.Cells[CELL_NAME_YOUKI_SUURYOU].Value = targetRow.Cells[CELL_NAME_NET_JYUURYOU].Value;
            //    targetRow.Cells[CELL_NAME_YOUKI_SUURYOU].ReadOnly = true;
            //}
            //else
            //{
            //    targetRow.Cells[CELL_NAME_YOUKI_SUURYOU].ReadOnly = false;
            //}

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ヘッダ情報設定
        /// </summary>
        /// <param name="windowType"></param>
        public bool setHeaderInfo(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType);

                this.headerForm.windowTypeLabel.Text = WINDOW_TYPEExt.ToTypeString(windowType);
                this.headerForm.windowTypeLabel.BackColor = WINDOW_TYPEExt.ToLabelColor(windowType);

                // No.3200-->
                this.dispUserInfo(windowType);
                // No.3200<--
            }
            catch (Exception ex)
            {
                LogUtility.Error("setHeaderInfo", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// Logic内で定義されているEntityすべての最新情報を取得する
        /// </summary>
        /// <returns>true:正常値、false:エラー発生</returns>
        public bool GetAllEntityData()
        {
            try
            {
                // 更新前データを保持しておく
                this.beforDto = new DTOClass();
                // 初期化
                this.dto = new DTOClass();

                // 画面のモードに依存しないデータの取得
                this.dto.sysInfoEntity = CommonShogunData.SYS_INFO;

                if (!this.IsRequireData())
                {
                    return true;
                }

                // 新規画面の場合、Entityを初期化
                if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    this.dto.entryEntity = new T_KEIRYOU_ENTRY();
                }

                // 受付番号検索
                if (this.form.blnUketsukeProgress)
                {
                    return true;
                }

                // 計量入力
                var entrys = accessor.GetKeiryouEntry(this.form.KeiryouNumber, this.form.SEQ);
                if (entrys == null || entrys.Length < 1)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    this.form.KEIRYOU_NUMBER.IsInputErrorOccured = true;
                    return false;
                }
                else
                {
                    this.dto.entryEntity = entrys[0];
                }

                // 計量明細
                var ditails = accessor.GetKeiryouDetail(this.dto.entryEntity.SYSTEM_ID, this.dto.entryEntity.SEQ);
                if (ditails == null || ditails.Length < 1)
                {
                    this.dto.detailEntity = new T_KEIRYOU_DETAIL[] { new T_KEIRYOU_DETAIL() };
                }
                else
                {
                    this.dto.detailEntity = ditails;
                }

                // 形態区分
                if (!this.dto.entryEntity.KEITAI_KBN_CD.IsNull)
                {
                    this.dto.keitaiKbnEntity = this.accessor.GetkeitaiKbn((short)this.dto.entryEntity.KEITAI_KBN_CD);

                }

                // 拠点
                if (!this.dto.entryEntity.KYOTEN_CD.IsNull)
                {
                    var kyotens = this.accessor.GetDataByCodeForKyoten((short)this.dto.entryEntity.KYOTEN_CD);
                    if (kyotens != null && 0 < kyotens.Length)
                    {
                        this.dto.kyotenEntity = kyotens[0];
                    }
                }

                // マニフェスト
                this.dto.manifestEntrys = this.accessor.GetManifestEntry(this.dto.detailEntity);
                // マニフェスト種類
                if (!this.dto.entryEntity.MANIFEST_SHURUI_CD.IsNull)
                {
                    this.dto.manifestShuruiEntity = this.accessor.GetManifestShurui(this.dto.entryEntity.MANIFEST_SHURUI_CD);
                }
                // マニフェスト手配
                if (!this.dto.entryEntity.MANIFEST_TEHAI_CD.IsNull)
                {
                    this.dto.manifestTehaiEntity = this.accessor.GetManifestTehai(this.dto.entryEntity.MANIFEST_TEHAI_CD);
                }

                this.beforDto = this.dto.Clone();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetAllEntityData", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetAllEntityData", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;

        }

        /// <summary>
        /// ヘッダーの拠点CDの存在チェック
        /// </summary>
        internal bool CheckKyotenCd()
        {
            bool ret = true;
            try
            {
                // 初期化
                this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;

                if (string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
                {
                    this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
                    return ret;
                }

                short kyoteCd = -1;
                if (!short.TryParse(string.Format("{0:#,0}", this.headerForm.KYOTEN_CD.Text), out kyoteCd))
                {
                    return ret;
                }

                var kyotens = this.accessor.GetDataByCodeForKyoten(kyoteCd);

                // 存在チェック
                if (kyotens == null || kyotens.Length < 1)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "拠点");
                    this.headerForm.KYOTEN_CD.IsInputErrorOccured = true;
                    this.headerForm.KYOTEN_CD.Focus();
                    return ret;
                }
                else
                {
                    // キーが１つなので複数はヒットしないはず
                    M_KYOTEN kyoten = kyotens[0];
                    this.headerForm.KYOTEN_NAME_RYAKU.Text = kyoten.KYOTEN_NAME_RYAKU.ToString();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckKyotenCd", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckKyotenCd", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        private string nizumiGyoushaHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 荷積業者チェック
        /// </summary>
        internal bool CheckNizumiGyoushaCd()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
                this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = true;
                this.form.NIZUMI_GYOUSHA_NAME.Tag = string.Empty;
                this.form.NIZUMI_GYOUSHA_NAME.TabStop = false;

                this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                this.form.NIZUMI_GENBA_NAME.ReadOnly = true;
                this.form.NIZUMI_GENBA_NAME.TabStop = false;

                //this.form.beforNizumiGyoushaCD = string.Empty;
                //this.form.beforNizumiGenbaCD = string.Empty;



                if (string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
                {
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                //var gyoushas = this.accessor.GetNisumiGyousya(this.form.NIZUMI_GYOUSHA_CD.Text);
                var gyousha = this.accessor.GetGyousha(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (gyousha == null)
                {
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                // PKは1つなので複数ヒットしない
                if (gyousha.GYOUSHAKBN_SHUKKA.IsTrue &&
                    gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue)
                {
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if (gyousha.SHOKUCHI_KBN.Equals(SqlBoolean.True))
                    {
                        this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end

                }
                else
                {
                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    this.form.NIZUMI_GYOUSHA_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E058", "");
                    this.form.NIZUMI_GYOUSHA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                if (gyousha.SHOKUCHI_KBN.Equals(SqlBoolean.True))
                {
                    this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = false;
                    //this.form.NIZUMI_GYOUSHA_NAME.TabStop = true;
                    this.form.NIZUMI_GYOUSHA_NAME.TabStop = GetTabStop("NIZUMI_GYOUSHA_NAME");    // No.3822
                    this.form.NIZUMI_GYOUSHA_NAME.Tag = this.nizumiGyoushaHintText;
                }
                // Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                this.MoveToNextControlForShokuchikbnCheck(this.form.NIZUMI_GYOUSHA_CD);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNizumiGyoushaCd", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNizumiGyoushaCd", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);

            return true;
        }

        private string nizumiGenbaHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 荷積現場CDの存在チェック
        /// </summary>
        internal bool ChechNizumiGenbaCd()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                this.form.NIZUMI_GENBA_NAME.ReadOnly = true;
                this.form.NIZUMI_GENBA_NAME.TabStop = false;
                this.form.NIZUMI_GENBA_NAME.Tag = string.Empty;

                if (string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text))
                {
                    this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                    return false;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                var genbaEntityList = this.accessor.GetGenba(this.form.NIZUMI_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (genbaEntityList == null || genbaEntityList.Length < 1)
                {
                    this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.form.NIZUMI_GENBA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                M_GENBA genba = new M_GENBA();
                if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_NAME_RYAKU.Text))
                {
                    // 荷積業者名入力チェック
                    if (string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_NAME.Text))
                    {
                        // エラーメッセージ
                        this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E051", "荷積業者");
                        this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                        this.form.NIZUMI_GENBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    genba = this.accessor.GetGenba(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.NIZUMI_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);

                    // 荷積業者チェック
                    if (genba == null)
                    {
                        // 一致するデータがないのでエラー
                        this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E062", "荷積業者");
                        this.form.NIZUMI_GENBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    // 事業場区分、荷積現場区分チェック
                    if (genba.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || genba.SAISHUU_SHOBUNJOU_KBN.IsTrue)
                    {
                        // 20151021 katen #13337 品名手入力に関する機能修正 start
                        if (genba.SHOKUCHI_KBN.IsTrue)
                        {
                            this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME1;
                        }
                        else
                        {
                            this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                        }
                        // 20151021 katen #13337 品名手入力に関する機能修正 end
                    }
                    else
                    {
                        // 一致するデータがないのでエラー
                        this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E058", "");
                        this.form.NIZUMI_GENBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                }
                else
                {
                    // 荷積業者名入力チェック
                    if (string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_NAME.Text))
                    {
                        // エラーメッセージ
                        this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E051", "荷積業者");
                        this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                        this.form.NIZUMI_GENBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    genba = this.accessor.GetGenba(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.NIZUMI_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);

                    // 取引先チェック(荷積業者はデータ取得時に指定しているので既にチェックしている)
                    //if (genba == null || !genba.TORIHIKISAKI_CD.Equals(this.form.TORIHIKISAKI_CD.Text))
                    //{
                    //    // 一致するデータがないのでエラー
                    //    this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                    //    msgLogic.MessageBoxShow("E062", "取引先、荷積業者");
                    //    this.form.NIZUMI_GENBA_CD.Focus();
                    //    return;
                    //}

                    // 事業場区分、現場区分チェック
                    if (genba != null && (genba.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || genba.SAISHUU_SHOBUNJOU_KBN.IsTrue))
                    {
                        // 20151021 katen #13337 品名手入力に関する機能修正 start
                        if (genba.SHOKUCHI_KBN.IsTrue)
                        {
                            this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME1;
                        }
                        else
                        {
                            this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                        }
                        // 20151021 katen #13337 品名手入力に関する機能修正 end
                    }
                    else
                    {
                        // 一致するデータがないのでエラー
                        this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E058", "");
                        this.form.NIZUMI_GENBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                }

                // 諸口区分チェック
                if (genba.SHOKUCHI_KBN.IsTrue)
                {
                    // 荷積現場名編集可
                    this.form.NIZUMI_GENBA_NAME.ReadOnly = false;
                    //this.form.NIZUMI_GENBA_NAME.TabStop = true;
                    this.form.NIZUMI_GENBA_NAME.TabStop = GetTabStop("NIZUMI_GENBA_NAME");    // No.3822
                    this.form.NIZUMI_GENBA_NAME.Tag = this.nizumiGenbaHintText;
                }
                else
                {
                    // 荷積現場名編集不可
                    this.form.NIZUMI_GENBA_NAME.ReadOnly = true;
                    this.form.NIZUMI_GENBA_NAME.TabStop = false;
                }
                // Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                this.MoveToNextControlForShokuchikbnCheck(this.form.NIZUMI_GENBA_CD);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChechNizumiGenbaCd", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechNizumiGenbaCd", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);

            return true;


        }

        private string nioroshiGyoushaHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 荷降業者チェック
        /// </summary>
        internal bool CheckNioroshiGyoushaCd()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                this.form.NIOROSHI_GYOUSHA_NAME.ReadOnly = true;
                this.form.NIOROSHI_GYOUSHA_NAME.Tag = string.Empty;
                this.form.NIOROSHI_GYOUSHA_NAME.TabStop = false;

                this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                this.form.NIOROSHI_GENBA_NAME.ReadOnly = true;
                this.form.NIOROSHI_GENBA_NAME.TabStop = false;

                //this.form.beforNioroshiGyoushaCD = string.Empty;
                //this.form.beforNioroshiGenbaCD = string.Empty;


                if (string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
                {
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                //var gyoushas = this.accessor.GetNisumiGyousya(this.form.NIOROSHI_GYOUSHA_CD.Text);
                var gyousha = this.accessor.GetGyousha(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (gyousha == null)
                {
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                // PKは1つなので複数ヒットしない
                if (gyousha.GYOUSHAKBN_UKEIRE.IsTrue && gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue)
                {
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if (gyousha.SHOKUCHI_KBN.Equals(SqlBoolean.True))
                    {
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end

                }
                else
                {
                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    this.form.NIOROSHI_GYOUSHA_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E058", "");
                    this.form.NIOROSHI_GYOUSHA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                if (gyousha.SHOKUCHI_KBN.Equals(SqlBoolean.True))
                {
                    this.form.NIOROSHI_GYOUSHA_NAME.ReadOnly = false;
                    //this.form.NIOROSHI_GYOUSHA_NAME.TabStop = true;
                    this.form.NIOROSHI_GYOUSHA_NAME.TabStop = GetTabStop("NIOROSHI_GYOUSHA_NAME");    // No.3822
                    this.form.NIOROSHI_GYOUSHA_NAME.Tag = this.nioroshiGyoushaHintText;
                }

                // Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                this.MoveToNextControlForShokuchikbnCheck(this.form.NIOROSHI_GYOUSHA_CD);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNioroshiGyoushaCd", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNioroshiGyoushaCd", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);

            return true;
        }

        private string nioroshiGenbaHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 荷降現場CDの存在チェック
        /// </summary>
        internal bool ChechNioroshiGenbaCd()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                this.form.NIOROSHI_GENBA_NAME.ReadOnly = true;
                this.form.NIOROSHI_GENBA_NAME.TabStop = false;
                this.form.NIOROSHI_GENBA_NAME.Tag = string.Empty;

                if (string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
                {
                    this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                var genbaEntityList = this.accessor.GetGenba(this.form.NIOROSHI_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (genbaEntityList == null || genbaEntityList.Length < 1)
                {
                    this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.form.NIOROSHI_GENBA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                M_GENBA genba = new M_GENBA();
                if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_NAME_RYAKU.Text))
                {
                    // 荷卸業者名入力チェック
                    if (string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_NAME.Text))
                    {
                        // エラーメッセージ
                        this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E051", "荷降業者");
                        this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                        this.form.NIOROSHI_GENBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    genba = this.accessor.GetGenba(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.NIOROSHI_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);

                    // 荷卸業者チェック
                    if (genba == null)
                    {
                        // 一致するデータがないのでエラー
                        this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E062", "荷降業者");
                        this.form.NIOROSHI_GENBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    // 事業場区分、荷卸現場区分チェック
                    if (genba.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || genba.SAISHUU_SHOBUNJOU_KBN.IsTrue)
                    {
                        // 20151021 katen #13337 品名手入力に関する機能修正 start
                        if (genba.SHOKUCHI_KBN.IsTrue)
                        {
                            this.form.NIOROSHI_GENBA_NAME.Text = genba.GENBA_NAME1;
                        }
                        else
                        {
                            this.form.NIOROSHI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                        }
                        // 20151021 katen #13337 品名手入力に関する機能修正 end
                    }
                    else
                    {
                        // 一致するデータがないのでエラー
                        this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E058", "");
                        this.form.NIOROSHI_GENBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                }
                else
                {
                    // 荷卸業者名入力チェック
                    if (string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_NAME.Text))
                    {
                        // エラーメッセージ
                        this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E051", "荷降業者");
                        this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                        this.form.NIOROSHI_GENBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    genba = this.accessor.GetGenba(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.NIOROSHI_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);

                    //// 取引先チェック(荷卸業者はデータ取得時に指定しているので既にチェックしている)
                    //if (genba == null || !genba.TORIHIKISAKI_CD.Equals(this.form.TORIHIKISAKI_CD.Text))
                    //{
                    //    // 一致するデータがないのでエラー
                    //    this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                    //    msgLogic.MessageBoxShow("E062", "取引先、荷降業者");
                    //    this.form.NIOROSHI_GENBA_CD.Focus();
                    //    return;
                    //}

                    // 事業場区分、現場区分チェック
                    if (genba != null && (genba.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || genba.SAISHUU_SHOBUNJOU_KBN.IsTrue))
                    {
                        // 20151021 katen #13337 品名手入力に関する機能修正 start
                        if (genba.SHOKUCHI_KBN.IsTrue)
                        {
                            this.form.NIOROSHI_GENBA_NAME.Text = genba.GENBA_NAME1;
                        }
                        else
                        {
                            this.form.NIOROSHI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                        }
                        // 20151021 katen #13337 品名手入力に関する機能修正 end
                    }
                    else
                    {
                        // 一致するデータがないのでエラー
                        this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E058", "");
                        this.form.NIOROSHI_GENBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                }

                // 諸口区分チェック
                if (genba.SHOKUCHI_KBN.IsTrue)
                {
                    // 荷卸現場名編集可
                    this.form.NIOROSHI_GENBA_NAME.ReadOnly = false;
                    //this.form.NIOROSHI_GENBA_NAME.TabStop = true;
                    this.form.NIOROSHI_GENBA_NAME.TabStop = GetTabStop("NIOROSHI_GENBA_NAME");    // No.3822
                    this.form.NIOROSHI_GENBA_NAME.Tag = this.nioroshiGenbaHintText;
                }
                else
                {
                    // 荷卸現場名編集不可
                    this.form.NIOROSHI_GENBA_NAME.ReadOnly = true;
                    this.form.NIOROSHI_GENBA_NAME.TabStop = false;
                }
                // Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                this.MoveToNextControlForShokuchikbnCheck(this.form.NIOROSHI_GENBA_CD);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChechNioroshiGenbaCd", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechNioroshiGenbaCd", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);

            return true;
        }

        private string unpanGyoushaHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 運搬業者CDの存在チェック
        /// </summary>
        internal bool CheckUnpanGyoushaCd()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_NAME.ReadOnly = true;
                this.form.UNPAN_GYOUSHA_NAME.TabStop = false;
                this.form.UNPAN_GYOUSHA_NAME.Tag = string.Empty;

                if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    return ret;
                }

                var gyousha = this.accessor.GetGyousha(this.form.UNPAN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (gyousha == null)
                {
                    return ret;
                }
                else if ("1".Equals(this.form.KIHON_KEIRYOU.Text) && gyousha.GYOUSHAKBN_UKEIRE.IsTrue &&
                    gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                {
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if (gyousha.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                }
                else if ("2".Equals(this.form.KIHON_KEIRYOU.Text) && gyousha.GYOUSHAKBN_SHUKKA.IsTrue &&
                    gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                {
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if (gyousha.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E058", "");
                    this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
                    this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.form.UNPAN_GYOUSHA_CD.Focus();
                    return ret;
                }

                // 諸口区分チェック
                if (gyousha.SHOKUCHI_KBN.IsTrue)
                {
                    // 運搬業者名編集可
                    this.form.UNPAN_GYOUSHA_NAME.ReadOnly = false;
                    //this.form.UNPAN_GYOUSHA_NAME.TabStop = true;
                    this.form.UNPAN_GYOUSHA_NAME.TabStop = GetTabStop("UNPAN_GYOUSHA_NAME");    // No.3822
                    this.form.UNPAN_GYOUSHA_NAME.Tag = this.unpanGyoushaHintText;
                }
                else
                {
                    // 運搬業者名編集不可
                    this.form.UNPAN_GYOUSHA_NAME.ReadOnly = true;
                    this.form.UNPAN_GYOUSHA_NAME.TabStop = false;
                }
                // Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                this.MoveToNextControlForShokuchikbnCheck(this.form.UNPAN_GYOUSHA_CD);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckUnpanGyoushaCd", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUnpanGyoushaCd", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 入力担当者チェック
        /// </summary>
        internal bool CheckNyuuryokuTantousha()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.NYUURYOKU_TANTOUSHA_NAME.Text = string.Empty;
                workTantou = string.Empty;  // No.3279

                if (string.IsNullOrEmpty(this.form.NYUURYOKU_TANTOUSHA_CD.Text))
                {
                    // 入力担当者CDがなければ既にエラーが表示されているはずなので何もしない
                    return ret;
                }

                var shainEntity = this.accessor.GetShain(this.form.NYUURYOKU_TANTOUSHA_CD.Text);
                if (shainEntity == null)
                {
                    return ret;
                }
                else if (shainEntity.NYUURYOKU_TANTOU_KBN.IsFalse)
                {
                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E058", "");
                    this.form.NYUURYOKU_TANTOUSHA_CD.Focus();
                }
                else
                {
                    this.form.NYUURYOKU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                    workTantou = shainEntity.SHAIN_NAME;    // No.3279
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNyuuryokuTantousha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNyuuryokuTantousha", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        private string torihikisakiHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 取引先チェック
        /// </summary>
        internal bool CheckTorihikisaki()
        {
            try
            {
                LogUtility.DebugMethodStart();


                //初期化
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
                this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
                this.form.TORIHIKISAKI_NAME_RYAKU.Tag = string.Empty;

                // No.2392-->
                //this.form.GYOUSHA_CD.Text = string.Empty;
                //this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                //this.form.GYOUSHA_NAME_RYAKU.ReadOnly = true;
                //this.form.GYOUSHA_NAME_RYAKU.TabStop = false;

                //this.form.GENBA_CD.Text = string.Empty;
                //this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                //this.form.GENBA_NAME_RYAKU.ReadOnly = true;
                //this.form.GENBA_NAME_RYAKU.TabStop = false;
                // No.2392 <--

                if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    return false;
                }

                var torihikisakiEntity = this.accessor.GetTorihikisaki(this.form.TORIHIKISAKI_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (torihikisakiEntity == null)
                {
                    // 存在しない
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    this.form.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E020", "取引先");
                    this.form.TORIHIKISAKI_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                else
                {
                    if (CheckTorihikisakiAndKyotenCd(torihikisakiEntity, this.form.TORIHIKISAKI_CD.Text))
                    {
                        // 取引先の拠点と入力された拠点コードの関連チェックOK
                        // 20151021 katen #13337 品名手入力に関する機能修正 start
                        if (torihikisakiEntity.SHOKUCHI_KBN.IsTrue)
                        {
                            this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME1;
                        }
                        else
                        {
                            this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                        }
                        // 20151021 katen #13337 品名手入力に関する機能修正 end
                    }
                    else
                    {
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                }

                // 諸口区分チェック
                if (torihikisakiEntity.SHOKUCHI_KBN.IsTrue)
                {
                    // 取引先名編集可
                    this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = false;
                    //this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = true;
                    this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = GetTabStop("TORIHIKISAKI_NAME_RYAKU");    // No.3822
                    this.form.TORIHIKISAKI_NAME_RYAKU.Tag = this.torihikisakiHintText;
                }
                else
                {
                    // 取引先名編集不可
                    this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
                    this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
                }

                // Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                this.MoveToNextControlForShokuchikbnCheck(this.form.TORIHIKISAKI_CD);

                // TODO: 【2次】営業担当者チェック
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckTorihikisaki", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTorihikisaki", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);

            return true;
        }

        private string gyoushaHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 取引先の拠点コードと入力された拠点コードの関連チェック
        /// </summary>
        /// <param name="torihikisakiEntity">取引先エンティティ</param>
        /// <param name="TorihikisakiCd">取引先CD</param>
        /// <returns>True：チェックOK False：チェックNG</returns>
        internal bool CheckTorihikisakiAndKyotenCd(M_TORIHIKISAKI torihikisakiEntity, string TorihikisakiCd)
        {
            bool returnVal = false;

            if (torihikisakiEntity == null)
            {
                // 取引先マスタを引数の取引先CDで取得しなおす
                torihikisakiEntity = this.accessor.GetTorihikisaki(TorihikisakiCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
            }

            if (torihikisakiEntity != null)
            {
                if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
                {
                    if (SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text) == torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD
                        || torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString().Equals("99"))
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
            }

            return returnVal;
        }

        /// <summary>
        /// 業者チェック
        /// </summary>
        internal bool CheckGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var torihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
                var torihikisakiName = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
                var gyoushaCd = this.form.GYOUSHA_CD.Text;
                var gyoushaName = this.form.GYOUSHA_NAME_RYAKU.Text;
                var genbaCd = this.form.GENBA_CD.Text;
                var genbaName = this.form.GENBA_NAME_RYAKU.Text;

                // 入力されてない場合
                if (String.IsNullOrEmpty(gyoushaCd))
                {
                    // 関連項目クリア
                    this.form.GYOUSHA_NAME_RYAKU.Text = String.Empty;
                    this.form.GENBA_CD.Text = String.Empty;
                    this.form.GENBA_NAME_RYAKU.Text = String.Empty;

                    this.form.beforeGenbaCD = string.Empty;

                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                // 業者を取得
                var gyousha = this.accessor.GetGyousha((this.form.GYOUSHA_CD.Text), this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (null == gyousha)
                {
                    // 業者名設定
                    this.form.GYOUSHA_NAME_RYAKU.Text = String.Empty;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "業者");

                    this.form.GYOUSHA_CD.Focus();
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                else if ("1".Equals(this.form.KIHON_KEIRYOU.Text) && gyousha.GYOUSHAKBN_UKEIRE == false)
                {
                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    this.form.GYOUSHA_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E058");
                    this.form.GYOUSHA_CD.Focus();

                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                else if ("2".Equals(this.form.KIHON_KEIRYOU.Text) && gyousha.GYOUSHAKBN_SHUKKA == false)
                {
                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    this.form.GYOUSHA_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E058");
                    this.form.GYOUSHA_CD.Focus();

                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                // 取引先を再設定
                // 取引先を取得
                var torihikisaki = this.accessor.GetTorihikisaki(gyousha.TORIHIKISAKI_CD, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (torihikisaki != null)
                {
                    this.form.TORIHIKISAKI_CD.Text = gyousha.TORIHIKISAKI_CD;
                    this.form.beforTorihikisakiCD = gyousha.TORIHIKISAKI_CD;

                    // 取引先と拠点の関係をチェック
                    if (!this.CheckTorihikisakiAndKyotenCd(null, gyousha.TORIHIKISAKI_CD))
                    {
                        //this.form.TORIHIKISAKI_CD.Focus();    // No.3822
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;

                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if ((bool)torihikisaki.SHOKUCHI_KBN)
                    {
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisaki.TORIHIKISAKI_NAME1;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                    this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = !(bool)torihikisaki.SHOKUCHI_KBN;
                    //this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = (bool)torihikisaki.SHOKUCHI_KBN;
                    this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = ((bool)torihikisaki.SHOKUCHI_KBN && GetTabStop("TORIHIKISAKI_NAME_RYAKU"));    // No.3822
                }

                // TODO: 【2次】営業担当者チェックの呼び出し

                // 業者を設定
                // 20151021 katen #13337 品名手入力に関する機能修正 start
                if ((bool)gyousha.SHOKUCHI_KBN)
                {
                    this.form.GYOUSHA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME1;
                }
                else
                {
                    this.form.GYOUSHA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
                // 20151021 katen #13337 品名手入力に関する機能修正 end
                if ((bool)gyousha.SHOKUCHI_KBN)
                {
                    this.form.GYOUSHA_NAME_RYAKU.ReadOnly = false;
                    //this.form.GYOUSHA_NAME_RYAKU.TabStop = true;
                    this.form.GYOUSHA_NAME_RYAKU.TabStop = GetTabStop("GYOUSHA_NAME_RYAKU");    // No.3822
                    this.form.GYOUSHA_NAME_RYAKU.Tag = this.gyoushaHintText;
                }
                else
                {
                    this.form.GYOUSHA_NAME_RYAKU.ReadOnly = true;
                    this.form.GYOUSHA_NAME_RYAKU.TabStop = false;
                    this.form.GYOUSHA_NAME_RYAKU.Tag = string.Empty;
                }

                // 現場を再設定
                // 現場を取得
                var genba = this.accessor.GetGenba(gyoushaCd, genbaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (null != genba)
                {
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if ((bool)genba.SHOKUCHI_KBN)
                    {
                        this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME1;
                    }
                    else
                    {
                        this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                    if ((bool)genba.SHOKUCHI_KBN)
                    {
                        this.form.GENBA_NAME_RYAKU.ReadOnly = false;
                        //this.form.GENBA_NAME_RYAKU.TabStop = true;
                        this.form.GENBA_NAME_RYAKU.TabStop = GetTabStop("GENBA_NAME_RYAKU");    // No.3822
                        this.form.GENBA_NAME_RYAKU.Tag = this.genbaHintText;
                    }
                    else
                    {
                        this.form.GENBA_NAME_RYAKU.ReadOnly = true;
                        this.form.GENBA_NAME_RYAKU.TabStop = false;
                        this.form.GENBA_NAME_RYAKU.Tag = string.Empty;
                    }
                }

                // Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                this.MoveToNextControlForShokuchikbnCheck(this.form.GYOUSHA_CD);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckGyousha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGyousha", ex);
                this.errmessage.MessageBoxShow("E245", "");
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        private string genbaHintText = "全角20桁以内で入力してください";

        /// <summary>
        /// 現場チェック
        /// </summary>
        internal bool CheckGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                this.form.GENBA_NAME_RYAKU.ReadOnly = true;
                this.form.GENBA_NAME_RYAKU.Tag = string.Empty;
                this.form.GENBA_NAME_RYAKU.TabStop = false;

                if (string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                var genbaEntityList = this.accessor.GetGenba(this.form.GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (genbaEntityList == null || genbaEntityList.Length < 1)
                {
                    this.form.GENBA_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.form.GENBA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                bool isContinue = false;
                M_GENBA genba = new M_GENBA();
                if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_NAME_RYAKU.Text))
                {
                    if (string.IsNullOrEmpty(this.form.GYOUSHA_NAME_RYAKU.Text))
                    {
                        // エラーメッセージ
                        this.form.GENBA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E051", "業者");
                        this.form.GENBA_CD.Text = string.Empty;
                        this.form.GENBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    foreach (M_GENBA genbaEntity in genbaEntityList)
                    {
                        if (this.form.GYOUSHA_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
                        {
                            isContinue = true;
                            genba = genbaEntity;
                            // 20151021 katen #13337 品名手入力に関する機能修正 start
                            if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                            {
                                this.form.GENBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME1;
                            }
                            else
                            {
                                this.form.GENBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
                            }
                            // 20151021 katen #13337 品名手入力に関する機能修正 end
                            workGenbaName = genbaEntity.GENBA_NAME1 + genbaEntity.GENBA_NAME2;   // No.3279
                            break;
                        }
                    }

                    if (!isContinue)
                    {
                        // 一致するものがないのでエラー
                        this.form.GENBA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E062", "業者");
                        this.form.GENBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(this.form.GYOUSHA_NAME_RYAKU.Text))
                    {
                        // エラーメッセージ
                        this.form.GENBA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E051", "業者");
                        this.form.GENBA_CD.Text = string.Empty;
                        this.form.GENBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    foreach (M_GENBA genbaEntity in genbaEntityList)
                    {
                        if (this.form.GYOUSHA_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
                        {
                            isContinue = true;
                            genba = genbaEntity;
                            // 20151021 katen #13337 品名手入力に関する機能修正 start
                            if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                            {
                                this.form.GENBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME1;
                            }
                            else
                            {
                                this.form.GENBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
                            }
                            // 20151021 katen #13337 品名手入力に関する機能修正 end
                            workGenbaName = genbaEntity.GENBA_NAME1 + genbaEntity.GENBA_NAME2;   // No.3279
                            break;
                        }
                    }

                    if (!isContinue)
                    {
                        // 一致するものがないのでエラー
                        this.form.GENBA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E062", "取引先、業者");
                        this.form.GENBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                }

                // TODO: 【2次】営業担当者チェックの呼び出し

                // 諸口区分チェック
                if (genba.SHOKUCHI_KBN.IsTrue)
                {
                    // 現場名編集可
                    this.form.GENBA_NAME_RYAKU.ReadOnly = false;
                    //this.form.GENBA_NAME_RYAKU.TabStop = true;
                    this.form.GENBA_NAME_RYAKU.TabStop = GetTabStop("GENBA_NAME_RYAKU");    // No.3822
                    this.form.GENBA_NAME_RYAKU.Tag = genbaHintText;
                }
                else
                {
                    // 現場名編集不可
                    this.form.GENBA_NAME_RYAKU.ReadOnly = true;
                    this.form.GENBA_NAME_RYAKU.TabStop = false;
                }

                // 取引先を再設定
                // 取引先を取得
                var torihikisaki = this.accessor.GetTorihikisaki(genba.TORIHIKISAKI_CD, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (torihikisaki != null)
                {
                    this.form.TORIHIKISAKI_CD.Text = genba.TORIHIKISAKI_CD;
                    this.form.beforTorihikisakiCD = genba.TORIHIKISAKI_CD;

                    // 取引先と拠点の関係をチェック
                    if (!this.CheckTorihikisakiAndKyotenCd(null, genba.TORIHIKISAKI_CD))
                    {
                        //this.form.TORIHIKISAKI_CD.Focus();    // No.3822
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;

                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if (torihikisaki.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisaki.TORIHIKISAKI_NAME1;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                    this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = !(bool)torihikisaki.SHOKUCHI_KBN;
                    this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = ((bool)torihikisaki.SHOKUCHI_KBN && GetTabStop("TORIHIKISAKI_NAME_RYAKU"));    // No.3822
                }

                // Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                this.MoveToNextControlForShokuchikbnCheck(this.form.GENBA_CD);

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
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckGenba", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenba", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);

            return true;
        }

        /// <summary>
        /// 営業担当者の表示（現場マスタ、業者マスタ、取引先マスタを元に）
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <param name="gyoushaCd"></param>
        /// <param name="torihikisakiCd"></param>
        internal bool setEigyou_Tantousha(string genbaCd, string gyoushaCd, string torihikisakiCd)
        {
            bool ret = true;
            try
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
                        genbaEntity = this.accessor.GetGenba(gyoushaCd, genbaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
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
                        else
                        {
                            // コードに対応する現場マスタが存在しない
                            // ただし、マスタ存在チェックはこの前になされているので、ここを通ることはない
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "現場");
                            return ret;
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
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("setEigyou_Tantousha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("setEigyou_Tantousha", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
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

            gyoushaEntity = this.accessor.GetGyousha(gyoushaCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
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
                torihikisakiEntity = this.accessor.GetTorihikisaki(torihikisakiCd, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
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
        /// 営業担当者チェック
        /// </summary>
        internal bool CheckEigyouTantousha()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.EIGYOU_TANTOUSHA_CD.Text))
                {
                    // 営業担当者CDがなければ既にエラーが表示されているので何もしない。
                    return ret;
                }

                var shainEntity = this.accessor.GetShain(this.form.EIGYOU_TANTOUSHA_CD.Text);
                if (shainEntity == null)
                {
                    return ret;
                }
                else if (shainEntity.EIGYOU_TANTOU_KBN.Equals(SqlBoolean.False))
                {
                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E058", "");
                    this.form.SHARYOU_CD.IsInputErrorOccured = true;
                    this.form.EIGYOU_TANTOUSHA_CD.Focus();
                }
                else
                {
                    this.form.EIGYOU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckEigyouTantousha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckEigyouTantousha", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        private string sharyouCd = string.Empty;
        private string unpanGyousha = string.Empty;
        private Color sharyouCdBackColor = Color.FromArgb(255, 235, 160);
        internal string searchSendParamKeyNameForSharyouCd = "key002";
        private string sharyouHinttext = "全角10桁以内で入力してください";

        // No.3312-->
        /// <summary>
        /// 車輌CD初期セット
        /// </summary>
        internal void ShayouCdSet()
        {
            sharyouCd = this.form.SHARYOU_CD.Text;
        }
        // No.3312<--

        /// <summary>
        /// 車輌チェック
        /// </summary>
        internal bool CheckSharyou()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                M_SHARYOU[] sharyouEntitys = null;

                // 何もしないとポップアップが起動されてしまう可能性があるため
                // 変更されたかチェックする
                if (sharyouCd.Equals(this.form.SHARYOU_CD.Text))
                {
                    // 複数ヒットするCDを入力→ポップアップで何もしない→一度ポップアップを閉じて再度ポップアップからデータを選択
                    // したときに色が戻らない問題の対策のため、存在チェックだけは実施する。
                    sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, this.form.SHASHU_CD.Text, null);
                    if (sharyouEntitys != null && sharyouEntitys.Length == 1)
                    {
                        // 一意に識別できる場合は色を戻す
                        this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                        this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
                        this.form.SHARYOU_NAME_RYAKU.Tag = string.Empty;
                        this.form.SHARYOU_NAME_RYAKU.TabStop = false;
                        this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                        this.form.SHARYOU_CD.AutoChangeBackColorEnabled = true;
                    }
                    return ret;
                }

                // 初期化
                this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                if (string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                    this.form.KUUSHA_JYURYO.Text = string.Empty;        // No.4101
                }
                this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
                this.form.SHARYOU_NAME_RYAKU.Tag = string.Empty;
                this.form.SHARYOU_NAME_RYAKU.TabStop = false;
                this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                this.form.SHARYOU_CD.AutoChangeBackColorEnabled = true;

                if (string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    sharyouCd = string.Empty;
                    this.form.isSelectingSharyouCd = false;
                    //this.form.beforUnpanGyoushaCD = string.Empty;
                    return ret;
                }

                sharyouCd = this.form.SHARYOU_CD.Text;
                unpanGyousha = this.form.UNPAN_GYOUSHA_CD.Text;

                //this.form.beforUnpanGyoushaCD = unpanGyousha;

                sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, this.form.SHASHU_CD.Text, null);

                // No.4101-->
                this.form.KUUSHA_JYURYO.Text = string.Empty;
                if (sharyouEntitys != null && sharyouEntitys.Length == 1)
                {
                    if (!sharyouEntitys[0].KUUSHA_JYURYO.IsNull)
                    {
                        this.form.KUUSHA_JYURYO.Text = sharyouEntitys[0].KUUSHA_JYURYO.ToString();
                    }
                }
                // No.4101<--

                // マスタ存在チェック
                if (sharyouEntitys == null || sharyouEntitys.Length < 1)
                {
                    // 車輌名を編集可
                    this.form.SHARYOU_NAME_RYAKU.ReadOnly = false;
                    //this.form.SHARYOU_NAME_RYAKU.TabStop = true;
                    this.form.SHARYOU_NAME_RYAKU.TabStop = GetTabStop("SHARYOU_NAME_RYAKU");    // No.3822;
                    this.form.SHARYOU_NAME_RYAKU.Tag = this.sharyouHinttext;
                    // 自由入力可能であるため車輌名の色を変更
                    this.form.SHARYOU_CD.AutoChangeBackColorEnabled = false;
                    this.form.SHARYOU_CD.BackColor = sharyouCdBackColor;
                    // マスタに存在しない場合、ユーザに車輌名を自由入力させる
                    if (string.IsNullOrEmpty(this.form.SHARYOU_NAME_RYAKU.Text) || this.form.SHARYOU_CD.Text != this.dto.entryEntity.SHARYOU_CD)
                        this.form.SHARYOU_NAME_RYAKU.Text = this.form.SHARYOU_CD.Text;
                    this.form.SHARYOU_NAME_RYAKU.Focus();

                    this.MoveToNextControlForShokuchikbnCheck(this.form.SHARYOU_CD);

                    if (!this.form.isSelectingSharyouCd)
                    {
                        this.form.isSelectingSharyouCd = true;
                        return ret;
                    }
                    return ret;
                }

                // ポップアップから戻ってきたときに運搬業者名が無いため取得
                M_GYOUSHA unpanGyousya = this.accessor.GetGyousha(this.form.UNPAN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
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
                            break;
                        }
                    }

                    if (isCheck)
                    {
                        // 車輌データセット
                        SetSharyou(sharyou);
                        return ret;
                    }
                    else
                    {
                        // エラーメッセージ
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E062", "運搬業者");
                        this.form.SHARYOU_CD.IsInputErrorOccured = true;
                        this.form.SHARYOU_CD.Focus();
                        return ret;
                    }
                }
                else
                {
                    if (sharyouEntitys.Length > 1)
                    {
                        // 複数レコード
                        // 車輌名を編集可
                        this.form.SHARYOU_NAME_RYAKU.ReadOnly = false;
                        //this.form.SHARYOU_NAME_RYAKU.TabStop = true;
                        this.form.SHARYOU_NAME_RYAKU.TabStop = GetTabStop("SHARYOU_NAME_RYAKU");    // No.3822
                        this.form.SHARYOU_NAME_RYAKU.Tag = this.sharyouHinttext;
                        // 自由入力可能であるため車輌名の色を変更
                        this.form.SHARYOU_CD.AutoChangeBackColorEnabled = false;
                        this.form.SHARYOU_CD.BackColor = sharyouCdBackColor;

                        if (!this.form.isSelectingSharyouCd)
                        {
                            sharyouCd = string.Empty;
                            unpanGyousha = string.Empty;
                            this.form.isSelectingSharyouCd = true;
                            this.form.SHARYOU_CD.Focus();

                            this.form.FocusOutErrorFlag = true;

                            // 検索ポップアップ起動
                            SendKeys.Send(" ");

                            this.form.FocusOutErrorFlag = false;
                            return ret;
                        }
                        else
                        {
                            // ポップアアップから戻ってきて車輌名へ遷移した場合
                            // マスタに存在しない場合、ユーザに車輌名を自由入力させる
                            this.form.SHARYOU_NAME_RYAKU.Text = this.form.SHARYOU_CD.Text;
                        }

                    }
                    else
                    {
                        // 一意レコード
                        // 車輌データセット
                        SetSharyou(sharyouEntitys[0]);
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckSharyou", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckSharyou", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 車輌情報をセット
        /// </summary>
        /// <param name="sharyouEntity"></param>
        private void SetSharyou(M_SHARYOU sharyouEntity)
        {
            this.form.SHARYOU_NAME_RYAKU.Text = sharyouEntity.SHARYOU_NAME_RYAKU;
            this.form.UNTENSHA_CD.Text = sharyouEntity.SHAIN_CD;
            this.form.UNPAN_GYOUSHA_CD.Text = sharyouEntity.GYOUSHA_CD;
            this.form.beforUnpanGyoushaCD = sharyouEntity.GYOUSHA_CD;

            // No.4101-->
            if (!sharyouEntity.KUUSHA_JYURYO.IsNull)
            {
                this.form.KUUSHA_JYURYO.Text = sharyouEntity.KUUSHA_JYURYO.ToString();
            }
            else
            {
                this.form.KUUSHA_JYURYO.Text = string.Empty;
            }
            // No.4101<--

            // 運転者情報セット
            var untensha = this.accessor.GetShain(sharyouEntity.SHAIN_CD);
            if (untensha != null)
            {
                this.form.UNTENSHA_NAME.Text = untensha.SHAIN_NAME_RYAKU;

                this.form.beforUnpanGyoushaCD = string.Empty;
                this.CheckUnpanGyoushaCd();
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
        }

        /// <summary>
        /// 帳票(領収書)出力
        /// </summary>
        internal void Print()
        {
            LogUtility.DebugMethodStart();

            DataTable reportData = CreateReportData();

            // G335\Templateにxmlを配置している
            // 現在表示されている一覧をレポート情報として生成
            ReportInfoBase reportInfo = new ReportInfoBase(reportData);
            reportInfo.Title = "領収書";
            reportInfo.Create(@".\Template\RyoushuushoReport_Meisai.xml", "RyoushuushoReport_Meisai", reportData);

            // 印刷ポップアップ表示
            FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo, "R339");

            // 印刷アプリ初期動作(ポップアップ)
            reportPopup.PrintInitAction = 3;

            //reportPopup.ShowDialog();
            reportPopup.PrintXPS();

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
            reportTable.Columns.Add("KYOTEN_NAME");
            reportTable.Columns["KYOTEN_NAME"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_POST");
            reportTable.Columns["KYOTEN_POST"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_ADDRESS1");
            reportTable.Columns["KYOTEN_ADDRESS1"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_TEL");
            reportTable.Columns["KYOTEN_TEL"].ReadOnly = false;
            reportTable.Columns.Add("KYOTEN_FAX");
            reportTable.Columns["KYOTEN_FAX"].ReadOnly = false;

            // 業者マスタ検索
            M_GYOUSHA gyousEntity = new M_GYOUSHA();
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                gyousEntity = this.accessor.GetGyousha(this.form.GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
            }

            // 拠点マスタ検索
            short kyoteCd = -1;
            if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
            {
                short.TryParse(this.headerForm.KYOTEN_CD.Text, out kyoteCd);
            }
            M_KYOTEN[] kyotenEntitys = this.accessor.GetDataByCodeForKyoten(kyoteCd);

            // データセット
            DataRow row = reportTable.NewRow();
            row["GYOUSHA_CD"] = this.form.GYOUSHA_CD.Text;
            row["GYOUSHA_NAME1"] = gyousEntity.GYOUSHA_NAME1;
            row["KEISHOU1"] = this.form.denpyouHakouPopUpDTO.Keisyou_1;
            row["GYOUSHA_NAME2"] = gyousEntity.GYOUSHA_NAME2;
            row["KEISHOU2"] = this.form.denpyouHakouPopUpDTO.Keisyou_2;
            row["DENPYOU_DATE"] = this.form.DENPYOU_DATE.Text;
            //row["RECEIPT_NUMBER"] = this.dto.entryEntity.RECEIPT_NUMBER;
            //if (ConstClass.ZEI_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Seikyu_Zei_Kbn))
            //{
            //    // 売上伝票毎消費税外税 加算
            //    decimal uriageKingakuTotal = 0;
            //    //decimal.TryParse(this.form.URIAGE_KINGAKU_TOTAL.Text, out uriageKingakuTotal);
            //    //row["KINGAKU_TOTAL"] = (uriageKingakuTotal + this.dto.entryEntity.URIAGE_TAX_SOTO);
            //}
            //else
            //{
            //    //row["KINGAKU_TOTAL"] = this.form.URIAGE_KINGAKU_TOTAL.Text;
            //}
            row["TADASHIGAKI"] = this.form.denpyouHakouPopUpDTO.Tadasi_Kaki;
            if (kyotenEntitys != null && kyotenEntitys.Length > 0)
            {
                row["KYOTEN_NAME"] = kyotenEntitys[0].KYOTEN_NAME;
                row["KYOTEN_POST"] = kyotenEntitys[0].KYOTEN_POST;
                row["KYOTEN_ADDRESS1"] = kyotenEntitys[0].KYOTEN_ADDRESS1;
                row["KYOTEN_TEL"] = kyotenEntitys[0].KYOTEN_TEL;
                row["KYOTEN_FAX"] = kyotenEntitys[0].KYOTEN_FAX;
            }

            reportTable.Rows.Add(row);

            LogUtility.DebugMethodEnd();
            return reportTable;
        }

        /// <summary>
        /// 明細に新規行を追加
        /// </summary>
        internal bool AddNewRow()
        {
            try
            {
                LogUtility.DebugMethodStart();
                if (this.form.gcMultiRow1.SelectedRows.Count < 0)
                {
                    // 選択されている場合のみ行追加
                    return true;
                }

                this.form.gcMultiRow1.BeginEdit(false);
                if (this.form.gcMultiRow1.CurrentRow.Index < this.form.gcMultiRow1.RowCount)
                {
                    this.form.gcMultiRow1.Rows.Insert(this.form.gcMultiRow1.CurrentRow.Index);
                }
                this.form.gcMultiRow1.EndEdit();
                this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
                // 行番号採番
                this.NumberingRowNo();
                // jyuuryouDtoを初期化
                this.SetJyuuryouDataToDtoList();
            }
            catch (Exception ex)
            {
                LogUtility.Error("AddNewRow", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodStart(true);
            return true;
        }

        /// <summary>
        /// 明細のカレント行を削除
        /// </summary>
        internal bool RemoveSelectedRow()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.gcMultiRow1.SelectedRows.Count < 1)
                {
                    // 選択されている場合のみ行追加
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                this.form.gcMultiRow1.BeginEdit(false);
                Row selectedRows = (Row)this.form.gcMultiRow1.CurrentRow;
                if (!selectedRows.IsNewRow)
                {
                    int warifuriNo = 0;
                    short warifuriRowNo = 0;

                    int.TryParse(Convert.ToString(selectedRows.Cells[CELL_NAME_WARIHURI_NO].Value), out warifuriNo);
                    short.TryParse(Convert.ToString(selectedRows.Cells[CELL_NAME_WARIHURIROW_NO].Value), out warifuriRowNo);

                    // 再計算のためデータを削除
                    if ((0 <= warifuriNo && 0 <= warifuriRowNo)
                        && warifuriNo < this.jyuuryouDtoList.Count
                        && warifuriRowNo < this.jyuuryouDtoList[warifuriNo].Count)
                    {
                        if (warifuriNo < this.jyuuryouDtoList.Count)
                        {
                            var jyuuryouDtos = this.jyuuryouDtoList[warifuriNo];
                            int i = warifuriRowNo + 1;
                            while (i < jyuuryouDtos.Count)
                            {
                                jyuuryouDtos[i].warifuriJyuuryou = null;
                                jyuuryouDtos[i].warifuriPercent = null;
                                i++;
                            }

                            if (0 < warifuriRowNo)
                            {
                                // 自分自身を削除
                                jyuuryouDtos.RemoveAt(warifuriRowNo);
                                // 再計算のため1つ上の割振を削除
                                jyuuryouDtos[warifuriRowNo - 1].warifuriJyuuryou = null;
                                jyuuryouDtos[warifuriRowNo - 1].warifuriPercent = null;
                            }
                            else
                            {
                                // 先頭行の場合は自分の割振kgと割振%を削除するだけ
                                //jyuuryouDtos[warifuriRowNo].warifuriJyuuryou = null;
                                //jyuuryouDtos[warifuriRowNo].warifuriPercent = null;

                                // 先頭行の場合はこの値を消す？
                                this.jyuuryouDtoList.RemoveAt(warifuriNo);
                            }
                        }

                    }



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
                    this.form.gcMultiRow1.AddSelection(iSaveIndex);

                    // 再計算処理
                    foreach (var jyuuryouDtoList in this.jyuuryouDtoList)
                    {
                        JyuuryouDto.CalcJyuuryouDtoForAdd(
                            jyuuryouDtoList,
                            true,
                            (short)this.dto.sysInfoEntity.UKEIRE_WARIFURI_HASU_CD,
                            (short)this.dto.sysInfoEntity.UKEIRE_WARIFURI_HASU_KETA,
                            (short)this.dto.sysInfoEntity.UKEIRE_WARIFURI_WARIAI_HASU_CD,
                            (short)this.dto.sysInfoEntity.UKEIRE_WARIFURI_WARIAI_HASU_KETA);
                    }

                    // 重量リストを使って重量値の更新
                    this.SetJyuuryouDataToMultiRow(false);
                }
                this.form.gcMultiRow1.EndEdit();
                this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
                // 行番号採番
                this.NumberingRowNo();
                this.form.gcMultiRow1.ResumeLayout();

                // 計算
                if (!this.CalcDetail())
                {
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RemoveSelectedRow", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 伝票区分設定
        /// 明細の品名から伝票区分を設定する
        /// </summary>
        internal bool SetDenpyouKbn(out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart();

                Row targetRow = this.form.gcMultiRow1.CurrentRow;

                if (targetRow == null)
                {
                    LogUtility.DebugMethodStart(true, catchErr);
                    return true;
                }

                // 初期化
                targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value = string.Empty;
                targetRow.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = string.Empty;

                if (targetRow.Cells[CELL_NAME_HINMEI_CD].EditedFormattedValue == null
                    || string.IsNullOrEmpty(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString()))
                {
                    LogUtility.DebugMethodStart(true, catchErr);
                    return true;
                }

                var targetHimei = this.accessor.GetHinmeiDataByCd(targetRow.Cells[CELL_NAME_HINMEI_CD].Value.ToString());

                if (targetHimei == null || string.IsNullOrEmpty(targetHimei.HINMEI_CD))
                {
                    // 存在しない品名が選択されている場合
                    LogUtility.DebugMethodStart(true, catchErr);
                    return true;
                }

                switch (targetHimei.DENPYOU_KBN_CD.ToString())
                {
                    case KeiryouConstans.DENPYOU_KBN_CD_URIAGE_STR:
                    case KeiryouConstans.DENPYOU_KBN_CD_SHIHARAI_STR:
                        targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value = (short)targetHimei.DENPYOU_KBN_CD;
                        targetRow.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = denpyouKbnDictionary[(short)targetHimei.DENPYOU_KBN_CD].DENPYOU_KBN_NAME_RYAKU;
                        break;

                    default:
                        // ポップアップを打ち上げ、ユーザに選択してもらう
                        CellPosition pos = this.form.gcMultiRow1.CurrentCellPosition;
                        CustomControlExtLogic.PopUp((ICustomControl)this.form.gcMultiRow1.Rows[pos.RowIndex].Cells[CELL_NAME_DENPYOU_KBN_CD]);

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

                            LogUtility.DebugMethodStart(false, catchErr);
                            return false;
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDenpyouKbn", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodStart(true, catchErr);

            return true;
        }

        #region 計量票発行
        /// <summary>
        /// 計量票発行
        /// </summary>
        internal bool KeiryouhyouHakkou()
        {
            bool retValue = true;
            try
            {
                LogUtility.DebugMethodStart();


                MessageBoxShowLogic msgLog = new MessageBoxShowLogic();
                DialogResult ret = msgLog.MessageBoxShow("C047", "計量票");

                if (ret == DialogResult.No)
                {
                    return retValue;
                }


                CommonShogunData.Create(SystemProperty.Shain.CD);

                // 画面のモードに依存しないデータの取得
                this.dto.sysInfoEntity = CommonShogunData.SYS_INFO;

                // 再設定
                this.CreateEntity(false, true);

                // 画面表示名称（種類)
                string layoutName = string.Empty;

                // 帳票宣言
                ReportInfoR354_R549_R550_R680_R681 reportInfo = new ReportInfoR354_R549_R550_R680_R681(WINDOW_ID.T_KEIRYO);
                reportInfo.logic = this;
                if (!string.IsNullOrEmpty(Convert.ToString(this.form.DENPYOU_DATE.Value)))
                {
                    reportInfo.DenpyouDate = Convert.ToDateTime(this.form.DENPYOU_DATE.Value);
                }
                else
                {
                    reportInfo.DenpyouDate = Convert.ToDateTime(this.footerForm.sysDate.Date);
                }

                //　会社情報取得
                this.dto.corpEntity = this.accessor.GetCorpInfo(this.dto.sysInfoEntity.SYS_ID);

                //　拠点情報
                if (!this.dto.entryEntity.KYOTEN_CD.IsNull)
                {
                    this.dto.kyotenEntity1 = this.getKyotenData(this.dto.entryEntity.KYOTEN_CD.ToString().PadLeft(2, '0'));
                }

                // this.dto.sysInfoEntity.KEIRYOU_LAYOUT_KBN = 3;


                if (this.dto.sysInfoEntity.KEIRYOU_LAYOUT_KBN.IsNull)
                {

                    // メッセージ出力
                    // ※{0}されていないため、{1}できません。
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E057", "M_SYS_INFOテーブルの計量情報計量票「レイアウト区分」が入力", "計量票発行は");

                    return retValue;
                }
                if (this.dto.sysInfoEntity.KEIRYOU_GOODS_KBN.IsNull)
                {

                    // メッセージ出力
                    // ※{0}されていないため、{1}できません。
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E057", "M_SYS_INFOテーブルの計量情報計量票「品数区分」が入力", "計量票発行は");

                    return retValue;
                }


                // 画面種類

                string projectId = string.Empty;

                // A4 縦三つ切り
                if (this.dto.sysInfoEntity.KEIRYOU_LAYOUT_KBN.Value == 1)
                {
                    reportInfo.OutputType = ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.Normal;
                    reportInfo.DispTypeForNormal = ReportInfoR354_R549_R550_R680_R681.DispTypeForNormalDef.Gyousha;
                    layoutName = "LAYOUT1";
                    projectId = "R354";

                    // ヘッダ
                    reportInfo.DataTableList["Header"] = this.getPageHeader(this.dto, headerReportItemName_R354);

                    // 明細
                    reportInfo.DataTableList["Detail"] = this.getPageDetail(detailReportItemName_R354);

                    // フッタ
                    reportInfo.DataTableList["Footer"] = this.getPageFooter(this.dto, footerReportItemName_R354);
                }
                // 三つ切り 複数品目
                else if (this.dto.sysInfoEntity.KEIRYOU_LAYOUT_KBN.Value == 2 && this.dto.sysInfoEntity.KEIRYOU_GOODS_KBN.Value == 2)
                {
                    reportInfo.OutputType = ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.MultiH;
                    layoutName = "LAYOUT2";
                    projectId = "R549";

                    // ヘッダ
                    reportInfo.DataTableList["Header"] = this.getPageHeader(this.dto, headerReportItemName_R549);

                    // 明細
                    reportInfo.DataTableList["Detail"] = this.getPageDetail(detailReportItemName_R549);

                    // フッタ
                    reportInfo.DataTableList["Footer"] = this.getPageFooter(this.dto, footerReportItemName_R549);
                }
                // 三つ切り 単品目
                else if (this.dto.sysInfoEntity.KEIRYOU_LAYOUT_KBN.Value == 2 && this.dto.sysInfoEntity.KEIRYOU_GOODS_KBN.Value == 1)
                {
                    reportInfo.OutputType = ReportInfoR354_R549_R550_R680_R681.OutputTypeDef.SingleH;
                    layoutName = "LAYOUT3";
                    projectId = "R550";

                    // ヘッダ
                    reportInfo.DataTableList["Header"] = this.getPageHeader(this.dto, headerReportItemName_R550);
                    // 明細
                    reportInfo.DataTableList["Detail"] = this.getPageDetail(detailReportItemName_R550);
                    // フッタ
                    reportInfo.DataTableList["Footer"] = this.getPageFooter(this.dto, footerReportItemName_R550);
                }
                else
                {
                    // メッセージ出力
                    // ※{0}の値が不正です。設定を見直してください。
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E082", "M_SYS_INFOテーブルの計量情報計量票「レイアウト区分」、「品数区分」");

                    return retValue;
                }


                // Sample
                //reportInfo.CreateSampleData();

                // 現在表示されている一覧をレポート情報として生成
                reportInfo.Create(@".\Template\R354_R549_R550_R680_R681-Form.xml", layoutName, new DataTable());
                reportInfo.Title = "計量票";

                using (FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo, projectId, WINDOW_ID.T_KEIRYO))
                {
                    //reportPopup.PrinterSettingInfo = new System.Drawing.Printing.PrinterSettings();
                    // 印刷設定の取得（計量票：7）
                    //reportPopup.SetPrintSetting(7);
                    // テスト用
                    //reportPopup.ShowDialog();

                    // 印刷アプリ初期動作(直印刷)
                    reportPopup.PrintInitAction = 1;

                    reportPopup.PrintXPS();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("KeiryouhyouHakkou", ex);
                this.errmessage.MessageBoxShow("E245", "");
                retValue = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(retValue);
            }
            return retValue;
        }
        #endregion

        #region ヘッダ（１ページ）
        /// <summary>
        /// ヘッダ（１ページ）
        /// </summary>
        private DataTable getPageHeader(DTOClass dto, string[] header)
        {
            LogUtility.DebugMethodStart(dto, header);

            // Header部
            DataTable dataTableHeader = new DataTable();

            // ヘッダ部
            for (int iHeader = 0; iHeader < header.Length; iHeader++)
            {
                dataTableHeader.Columns.Add(header[iHeader], typeof(string));

            }
            DataRow dataTableHeaderRow = dataTableHeader.NewRow();

            // 取引先マスタ検索
            M_TORIHIKISAKI toriEntity = new M_TORIHIKISAKI();
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                toriEntity = this.accessor.GetTorihikisaki(this.form.TORIHIKISAKI_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
            }
            // 業者マスタ検索
            M_GYOUSHA gyousEntity = new M_GYOUSHA();
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                gyousEntity = this.accessor.GetGyousha(this.form.GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
            }
            // 現場マスタ検索
            M_GENBA genbaEntity = new M_GENBA();
            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text) && !string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                genbaEntity = this.accessor.GetGenba(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
            }

            for (int iHeader = 0; iHeader < header.Length; iHeader++)
            {
                object value;
                PropertyUtility.GetValue(this.dto.entryEntity, header[iHeader], out value);
                if (value == null || "Null".Equals(value.ToString()))
                {
                    value = String.Empty;
                }

                // 総重量
                if (header[iHeader].Equals("STACK_JYUURYOU"))
                {
                    int InOut = 0;
                    if (!string.IsNullOrEmpty(this.form.KIHON_KEIRYOU.Text))
                    {
                        InOut = int.Parse(this.form.KIHON_KEIRYOU.Text);
                    }
                    string StakJyuuryou = GetJuryoCol(0, InOut);
                    value = StakJyuuryou;
                }


                // 総空車重量
                if (header[iHeader].Equals("EMPTY_JYUURYOU"))
                {
                    int InOut = 0;
                    if (!string.IsNullOrEmpty(this.form.KIHON_KEIRYOU.Text))
                    {
                        InOut = int.Parse(this.form.KIHON_KEIRYOU.Text);
                    }
                    string EmptyJyuuryou = GetJuryoCol(1, InOut);
                    value = EmptyJyuuryou;
                }

                // 伝票No.
                if (header[iHeader].Equals("DENPYOU_NUMBER"))
                {
                    PropertyUtility.GetValue(this.dto.entryEntity, "KEIRYOU_NUMBER", out value);
                    if (value == null || "Null".Equals(value.ToString()))
                    {
                        value = String.Empty;
                    }
                }

                // 乗員
                if (header[iHeader].Equals("JYOUIN"))
                {
                    PropertyUtility.GetValue(this.dto.entryEntity, "NINZUU_CNT", out value);
                    if (value == null || "Null".Equals(value.ToString()))
                    {
                        value = String.Empty;
                    }
                }

                // 車番
                if (header[iHeader].Equals("SHABAN"))
                {
                    PropertyUtility.GetValue(this.dto.entryEntity, "SHARYOU_NAME", out value);
                    if (value == null || "Null".Equals(value.ToString()))
                    {
                        value = String.Empty;
                    }
                }

                // 担当
                if (header[iHeader].Equals("TANTOU"))
                {
                    //PropertyUtility.GetValue(this.dto.entryEntity, "NYUURYOKU_TANTOUSHA_NAME", out value); // No.3279
                    value = workTantou; // No.3279
                    value = value == null ? "" : value.ToString();
                }
                // 車両
                if (header[iHeader].Equals("SHARYOU"))
                {
                    PropertyUtility.GetValue(this.dto.entryEntity, "SHARYOU_NAME", out value);
                    if (value == null || "Null".Equals(value.ToString()))
                    {
                        value = String.Empty;
                    }
                }
                // 計量証明書タイトル
                if (header[iHeader].Equals("KEIRYOU_HYOU_TITLE"))
                {

                    this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_1 = this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_1 == null ? string.Empty : this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_1;
                    this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_2 = this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_2 == null ? string.Empty : this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_2;
                    this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_3 = this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_3 == null ? string.Empty : this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_3;

                    value = this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_1 + "," + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_2 + "," + this.dto.sysInfoEntity.KEIRYOU_HYOU_TITLE_3;

                }


                // 伝票日付
                if (header[iHeader].Equals("DENPYOU_DATE"))
                {

                    DateTime dt;
                    string strdate = string.Empty;
                    PropertyUtility.GetValue(this.dto.entryEntity, "DENPYOU_DATE", out value);

                    if (DateTime.TryParse(value.ToString(), out dt))
                    {
                        strdate = dt.ToString("yyyy/MM/dd");
                    }

                    value = strdate;

                    if (value == null || "Null".Equals(value.ToString()))
                    {
                        value = String.Empty;
                    }
                }


                // 取引先CD
                if (header[iHeader].Equals("TORIHIKISAKI_CD"))
                {
                    if (toriEntity != null)
                    {
                        value = toriEntity.TORIHIKISAKI_CD;
                    }
                }

                // 取引先名
                if (header[iHeader].Equals("TORIHIKISAKI_NAME"))
                {
                    value = string.Empty;
                    if (toriEntity != null)
                    {
                        if (toriEntity.SHOKUCHI_KBN.IsTrue)
                        {
                            // 諸口の取引先
                            value = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
                        }
                        else
                        {
                            // 諸口以外
                            if (toriEntity.TORIHIKISAKI_NAME1 != null)
                            {
                                if (toriEntity.TORIHIKISAKI_NAME2 != null)
                                {
                                    value = toriEntity.TORIHIKISAKI_NAME1 + "\n" + toriEntity.TORIHIKISAKI_NAME2;
                                }
                                else
                                {
                                    value = toriEntity.TORIHIKISAKI_NAME1;
                                }
                            }
                        }
                    }
                }

                // 取引先敬称
                if (header[iHeader].Equals("TORIHIKISAKI_KEISYOU"))
                {
                    value = string.Empty;
                    if (toriEntity != null)
                    {
                        if (toriEntity.TORIHIKISAKI_KEISHOU1 != null)
                        {
                            if (toriEntity.TORIHIKISAKI_KEISHOU2 != null)
                            {
                                value = toriEntity.TORIHIKISAKI_KEISHOU1 + "\n" + toriEntity.TORIHIKISAKI_KEISHOU2;
                            }
                            else
                            {
                                value = toriEntity.TORIHIKISAKI_KEISHOU1;
                            }
                        }
                    }
                }

                // 業者CD
                if (header[iHeader].Equals("GYOUSHA_CD"))
                {
                    if (gyousEntity != null)
                    {
                        value = gyousEntity.GYOUSHA_CD;
                    }
                }

                // 業者名
                if (header[iHeader].Equals("GYOUSHA_NAME"))
                {
                    value = string.Empty;
                    if (gyousEntity != null)
                    {
                        if (gyousEntity.SHOKUCHI_KBN.IsTrue)
                        {
                            // 諸口の業者
                            value = this.form.GYOUSHA_NAME_RYAKU.Text;
                        }
                        else
                        {
                            // 諸口以外
                            if (gyousEntity.GYOUSHA_NAME1 != null)
                            {
                                if (gyousEntity.GYOUSHA_NAME2 != null)
                                {
                                    value = gyousEntity.GYOUSHA_NAME1 + "\n" + gyousEntity.GYOUSHA_NAME2;
                                }
                                else
                                {
                                    value = gyousEntity.GYOUSHA_NAME1;
                                }
                            }
                        }
                    }
                }

                // 業者敬称
                if (header[iHeader].Equals("GYOUSHA_KEISYOU"))
                {
                    value = string.Empty;
                    if (gyousEntity != null)
                    {
                        if (gyousEntity.GYOUSHA_KEISHOU1 != null)
                        {
                            if (gyousEntity.GYOUSHA_KEISHOU2 != null)
                            {
                                value = gyousEntity.GYOUSHA_KEISHOU1 + "\n" + gyousEntity.GYOUSHA_KEISHOU2;
                            }
                            else
                            {
                                value = gyousEntity.GYOUSHA_KEISHOU1;
                            }
                        }
                    }
                }

                // 現場CD
                if (header[iHeader].Equals("GENBA_CD"))
                {
                    if (genbaEntity != null)
                    {
                        value = genbaEntity.GENBA_CD;
                    }
                }

                // 現場名
                if (header[iHeader].Equals("GENBA_NAME"))
                {
                    if (genbaEntity != null)
                    {
                        this.dto.entryEntity.GENBA_NAME = string.Empty;

                        if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                        {
                            // 諸口の現場
                            value = this.form.GENBA_NAME_RYAKU.Text;
                        }
                        else
                        {
                            // 諸口以外
                            if (genbaEntity.GENBA_NAME1 != null)
                            {
                                if (genbaEntity.GENBA_NAME2 != null)
                                {
                                    value = genbaEntity.GENBA_NAME1 + "\n" + genbaEntity.GENBA_NAME2;
                                }
                                else
                                {
                                    value = genbaEntity.GENBA_NAME1;
                                }
                            }
                        }
                    }
                }

                //現場敬称
                if (header[iHeader].Equals("GENBA_KEISYOU"))
                {
                    if (genbaEntity != null)
                    {
                        value = string.Empty;
                        if (genbaEntity.GENBA_KEISHOU1 != null)
                        {
                            if (genbaEntity.GENBA_KEISHOU2 != null)
                            {
                                value = genbaEntity.GENBA_KEISHOU1 + "\n" + genbaEntity.GENBA_KEISHOU2;
                            }
                            else
                            {
                                value = genbaEntity.GENBA_KEISHOU1;
                            }
                        }
                    }
                }

                // 帳票データに設定
                dataTableHeaderRow[iHeader] = value;
            }

            dataTableHeader.Rows.Add(dataTableHeaderRow);

            LogUtility.DebugMethodEnd();

            return dataTableHeader;
        }
        #endregion

        #region 明細（１ページ）
        /// <summary>
        /// 明細（１ページ）
        /// </summary>
        private DataTable getPageDetail(string[] detail)
        {
            LogUtility.DebugMethodStart(detail);


            // Detail部
            DataTable dataTableDetail = new DataTable();
            // 明細部
            for (int iDetail = 0; iDetail < detail.Length; iDetail++)
            {
                dataTableDetail.Columns.Add(detail[iDetail], typeof(string));

            }

            // 総ページ
            SqlInt16 totalPageNo = 0;

            // rowを取得
            SqlInt16 intRow = 0;

            DataRow dataTableDetailRow;

            // CreateDataTableForEntityがMultiRowを動的に作成しないため、ここでEntity分行数を追加する
            // Entity数Rowを作ると最終行が無いので、Etity + 1でループさせる
            for (int row = 0; row < this.dto.detailEntity.Length; row++)
            {
                dataTableDetailRow = dataTableDetail.NewRow();

                for (int iDetail = 0; iDetail < detail.Length; iDetail++)
                {
                    object value;
                    PropertyUtility.GetValue(this.dto.detailEntity[row], detail[iDetail], out value);

                    if (value == null || "Null".Equals(value.ToString()) || "null".Equals(value.ToString()))
                    {
                        value = String.Empty;
                    }



                    // 総重量
                    if (detail[iDetail].Equals("STACK_JYUURYOU"))
                    {
                        if (this.dto.sysInfoEntity.KEIRYOU_LAYOUT_KBN.Value == 1)
                        {
                            // A4 縦三つ切り
                            PropertyUtility.GetValue(this.dto.detailEntity[row], "STACK_JYUURYOU", out value);
                            if (value == null || "Null".Equals(value.ToString()) || "null".Equals(value.ToString()))
                            {
                                value = String.Empty;
                            }
                            else
                            {
                                value = value.ToString();
                            }
                        }
                        else
                        {
                            int InOut = 0;
                            if (!string.IsNullOrEmpty(this.form.KIHON_KEIRYOU.Text))
                            {
                                InOut = int.Parse(this.form.KIHON_KEIRYOU.Text);
                            }
                            string StakJyuuryou = GetJuryoCol(0, InOut);
                            value = StakJyuuryou;
                        }
                    }

                    // 空車重量
                    if (detail[iDetail].Equals("EMPTY_JYUURYOU"))
                    {
                        if (this.dto.sysInfoEntity.KEIRYOU_LAYOUT_KBN.Value == 1)
                        {
                            // A4 縦三つ切り
                            PropertyUtility.GetValue(this.dto.detailEntity[row], "EMPTY_JYUURYOU", out value);
                            if (value == null || "Null".Equals(value.ToString()) || "null".Equals(value.ToString()))
                            {
                                value = String.Empty;
                            }
                            else
                            {
                                value = ((SqlDecimal)value).Value.ToString(TankaFormat);
                            }
                        }
                        else
                        {
                            int InOut = 0;
                            if (!string.IsNullOrEmpty(this.form.KIHON_KEIRYOU.Text))
                            {
                                InOut = int.Parse(this.form.KIHON_KEIRYOU.Text);
                            }
                            string EmptyJyuuryou = GetJuryoCol(1, InOut);
                            value = EmptyJyuuryou;
                        }
                    }
                    // 割振重量
                    if (detail[iDetail].Equals("WARIFURI_JYUURYOU"))
                    {
                        PropertyUtility.GetValue(this.dto.detailEntity[row], "WARIFURI_JYUURYOU", out value);

                        if (value == null || "Null".Equals(value.ToString()) || "null".Equals(value.ToString()))
                        {
                            value = String.Empty;
                        }
                        else
                        {
                            value = ((SqlDecimal)value).Value.ToString(TankaFormat);
                        }
                    }


                    // 調整
                    if (detail[iDetail].Equals("NET_CHOUSEI"))
                    {
                        PropertyUtility.GetValue(this.dto.detailEntity[row], "CHOUSEI_JYUURYOU", out value);

                        if (value == null || "Null".Equals(value.ToString()) || "null".Equals(value.ToString()))
                        {
                            value = String.Empty;
                        }
                        else
                        {
                            value = ((SqlDecimal)value).Value.ToString(TankaFormat);
                        }
                    }

                    // 容器重量
                    if (detail[iDetail].Equals("YOUKI_JYUURYOU"))
                    {
                        PropertyUtility.GetValue(this.dto.detailEntity[row], "YOUKI_JYUURYOU", out value);

                        if (value == null || "Null".Equals(value.ToString()) || "null".Equals(value.ToString()))
                        {
                            value = String.Empty;
                        }
                        else
                        {
                            value = ((SqlDecimal)value).Value.ToString(TankaFormat);
                        }
                    }


                    // 正味
                    if (detail[iDetail].Equals("NET_JYUURYOU"))
                    {
                        PropertyUtility.GetValue(this.dto.detailEntity[row], "NET_JYUURYOU", out value);

                        if (value == null || "Null".Equals(value.ToString()) || "null".Equals(value.ToString()))
                        {
                            value = String.Empty;
                        }
                        else
                        {
                            value = ((SqlDecimal)value).Value.ToString(TankaFormat);
                        }

                    }

                    // No.3278-->
                    // 品名
                    if (detail[iDetail].Equals("HINMEI_NAME"))
                    {
                        // 20151021 katen #13337 品名手入力に関する機能修正 start
                        PropertyUtility.GetValue(this.dto.detailEntity[row], "HINMEI_NAME", out value);

                        if (value == null || "Null".Equals(value.ToString()) || "null".Equals(value.ToString()))
                        {
                            value = String.Empty;
                        }
                        //PropertyUtility.GetValue(this.dto.detailEntity[row], "HINMEI_CD", out value);
                        //if (value == null || "Null".Equals(value.ToString()) || "null".Equals(value.ToString()))
                        //{
                        //    value = String.Empty;
                        //}
                        //else
                        //{
                        //    value = GetHinmei(value.ToString());
                        //}
                        // 20151021 katen #13337 品名手入力に関する機能修正 end
                    }
                    // No.3278<--

                    dataTableDetailRow[iDetail] = value;
                }

                dataTableDetail.Rows.Add(dataTableDetailRow);


            }

            LogUtility.DebugMethodEnd();

            return dataTableDetail;
        }

        #endregion

        #region フッタ（１ページ）
        /// <summary>
        /// フッタ（１ページ）
        /// </summary>
        private DataTable getPageFooter(DTOClass dto, string[] footer)
        {
            LogUtility.DebugMethodStart(dto, footer);

            // Footer部
            DataTable dataTableFooter = new DataTable();

            // フッタ部
            for (int iFooder = 0; iFooder < footer.Length; iFooder++)
            {
                dataTableFooter.Columns.Add(footer[iFooder], typeof(string));

            }

            DataRow dataTableFooterRow = dataTableFooter.NewRow();

            for (int iFooder = 0; iFooder < footer.Length; iFooder++)
            {
                object value;
                PropertyUtility.GetValue(this.dto.entryEntity, footer[iFooder], out value);
                if (value == null || "Null".Equals(value.ToString()))
                {
                    value = String.Empty;
                }


                // 正味合計
                if (footer[iFooder].Equals("NET_JYUURYOU_TOTAL"))
                {
                    PropertyUtility.GetValue(this.dto.entryEntity, "NET_TOTAL", out value);

                    if (value == null || "Null".Equals(value.ToString()))
                    {
                        value = String.Empty;
                    }
                    value = ((SqlDecimal)value).Value.ToString(TankaFormat);

                }

                // 備考
                if (footer[iFooder].Equals("DENPYOU_BIKOU"))
                {
                    PropertyUtility.GetValue(this.dto.entryEntity, "DENPYOU_BIKOU", out value);

                    if (value == null || "Null".Equals(value.ToString()))
                    {
                        value = String.Empty;
                    }
                    value = value.ToString();

                }

                // 計量情報計量証明項目1
                if (footer[iFooder].Equals("KEIRYOU_JYOUHOU1"))
                {
                    value = this.dto.kyotenEntity1.KEIRYOU_SHOUMEI_1;

                    if (value == null || "Null".Equals(value.ToString()))
                    {
                        value = String.Empty;
                    }
                }
                // 計量情報計量証明項目2
                if (footer[iFooder].Equals("KEIRYOU_JYOUHOU2"))
                {
                    value = this.dto.kyotenEntity1.KEIRYOU_SHOUMEI_2;

                    if (value == null || "Null".Equals(value.ToString()))
                    {
                        value = String.Empty;
                    }
                }
                // 計量情報計量証明項目3
                if (footer[iFooder].Equals("KEIRYOU_JYOUHOU3"))
                {
                    value = this.dto.kyotenEntity1.KEIRYOU_SHOUMEI_3;

                    if (value == null || "Null".Equals(value.ToString()))
                    {
                        value = String.Empty;
                    }
                }

                // 会社名
                if (footer[iFooder].Equals("CORP_RYAKU_NAME"))
                {
                    value = this.dto.corpEntity.CORP_NAME == null ? "" : this.dto.corpEntity.CORP_NAME;
                }

                // 拠点名
                if (footer[iFooder].Equals("KYOTEN_NAME"))
                {
                    value = this.dto.kyotenEntity1.KYOTEN_NAME == null ? "" : this.dto.kyotenEntity1.KYOTEN_NAME;
                    if (value == null || "Null".Equals(value.ToString()))
                    {
                        value = String.Empty;
                    }
                }

                // 拠点住所1
                if (footer[iFooder].Equals("KYOTEN_ADDRESS1"))
                {
                    value = this.dto.kyotenEntity1.KYOTEN_ADDRESS1 == null ? "" : this.dto.kyotenEntity1.KYOTEN_ADDRESS1;
                    if (value == null || "Null".Equals(value.ToString()))
                    {
                        value = String.Empty;
                    }
                }

                // 拠点住所2
                if (footer[iFooder].Equals("KYOTEN_ADDRESS2"))
                {
                    value = this.dto.kyotenEntity1.KYOTEN_ADDRESS2 == null ? "" : this.dto.kyotenEntity1.KYOTEN_ADDRESS2;
                    if (value == null || "Null".Equals(value.ToString()))
                    {
                        value = String.Empty;
                    }
                }

                // 拠点TEL
                if (footer[iFooder].Equals("KYOTEN_TEL"))
                {
                    value = this.dto.kyotenEntity1.KYOTEN_TEL == null ? "" : this.dto.kyotenEntity1.KYOTEN_TEL;
                    if (value == null || "Null".Equals(value.ToString()))
                    {
                        value = String.Empty;
                    }
                }

                // 拠点FAX
                if (footer[iFooder].Equals("KYOTEN_FAX"))
                {
                    value = this.dto.kyotenEntity1.KYOTEN_FAX == null ? "" : this.dto.kyotenEntity1.KYOTEN_FAX;
                    if (value == null || "Null".Equals(value.ToString()))
                    {
                        value = String.Empty;
                    }
                }

                // No.3279-->
                // 現場名
                if (footer[iFooder].Equals("GENBA_NAME"))
                {
                    if (this.form.GENBA_CD.Text != "" && this.form.GYOUSHA_CD.Text != "")
                    {
                        var genbaentity = this.accessor.GetGenba(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                        if (genbaentity != null)
                        {
                            if (genbaentity.SHOKUCHI_KBN.IsTrue)
                            {
                                // 諸口の現場
                                value = this.form.GENBA_NAME_RYAKU.Text;
                            }
                            else
                            {
                                value = workGenbaName == null ? "" : workGenbaName;
                            }
                        }
                    }
                }
                // No.3279<--

                dataTableFooterRow[iFooder] = value;
            }

            dataTableFooter.Rows.Add(dataTableFooterRow);

            LogUtility.DebugMethodEnd();

            return dataTableFooter;
        }
        #endregion

        #region 拠点データ取得処理
        /// <summary>
        /// 拠点データ取得処理
        /// </summary>
        /// <param name="kyotenEntity"></param>
        private M_KYOTEN getKyotenData(string kyotenCode)
        {
            LogUtility.DebugMethodStart(kyotenCode);

            M_KYOTEN kyotenEntity = new M_KYOTEN();

            kyotenEntity.KYOTEN_CD = Int16.Parse(kyotenCode);

            // 拠点マスタ取得
            var kyotens = this.accessor.GetDataByCodeForKyoten((short)kyotenEntity.KYOTEN_CD);
            if (kyotens != null && 0 < kyotens.Length)
            {
                this.dto.kyotenEntity = kyotens[0];

                LogUtility.DebugMethodEnd();
                return this.dto.kyotenEntity;
            }
            else
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "拠点");

                LogUtility.DebugMethodEnd();

                return null;
            }

        }

        #endregion

        #endregion

        #region Utility
        /// <summary>
        /// WINDOWTYPEからデータ取得が必要かどうか判断します
        /// </summary>
        /// <returns>True:データ取得が必要, Flase:データ取得が不必要</returns>
        private bool IsRequireData()
        {
            if (((WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType) && this.form.blnCopyProgress) // 複写
                || (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType) && this.form.nowLoding)   // 計量番号検索
                )
                || WINDOW_TYPE.UPDATE_WINDOW_FLAG.Equals(this.form.WindowType)
                || WINDOW_TYPE.DELETE_WINDOW_FLAG.Equals(this.form.WindowType)
                || WINDOW_TYPE.REFERENCE_WINDOW_FLAG.Equals(this.form.WindowType)
                )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// コントロールから登録用のEntityを作成する
        /// </summary>
        public virtual void CreateEntity(bool tairyuuKbnFlag, bool makeKb)
        {
            LogUtility.DebugMethodStart(tairyuuKbnFlag, makeKb);

            if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG) || tairyuuKbnFlag)
            {
                this.dto.entryEntity = new T_KEIRYOU_ENTRY();
            }

            // T_KEIRYOU_ENTRYの設定
            // 日連番取得
            S_NUMBER_DAY[] numberDays = null;
            DateTime denpyouDate = DateTime.Now;  // 伝票日付
            short kyotenCd = -1;    // 拠点CD
            short.TryParse(this.headerForm.KYOTEN_CD.Text.ToString(), out kyotenCd);
            if (DateTime.TryParse(this.form.DENPYOU_DATE.Value.ToString(), out denpyouDate)
                && -1 < kyotenCd)
            {
                numberDays = this.accessor.GetNumberDay(denpyouDate.Date, (short)DENSHU_KBN.KEIRYOU, kyotenCd);
            }

            // TODO: 年連番取得(S_NUMBER_YEARテーブルから情報取得 + 年度の生成処理を追加)
            S_NUMBER_YEAR[] numberYeas = null;
            SqlInt32 numberedYear = CorpInfoUtility.GetCurrentYear(denpyouDate.Date, (short)CommonShogunData.CORP_INFO.KISHU_MONTH);
            if (-1 < kyotenCd)
            {
                numberYeas = this.accessor.GetNumberYear(numberedYear, (short)DENSHU_KBN.KEIRYOU, kyotenCd);
            }


            // モードに依存する処理
            byte[] numberDayTimeStamp = null;
            byte[] numberYearTimeStamp = null;
            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    if (!makeKb)
                    {
                        // SYSTEM_IDの採番
                        SqlInt64 systemId = this.accessor.createSystemIdForKeiryou();
                        this.dto.entryEntity.SYSTEM_ID = systemId;

                        // 計量番号の採番
                        this.dto.entryEntity.KEIRYOU_NUMBER = this.accessor.createKeiryouNumber();

                        // 日連番
                        if (numberDays == null || numberDays.Length < 1)
                        {
                            this.hiRenbanRegistKbn = Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.INSERT;   // DB更新時に使用
                            this.dto.entryEntity.DATE_NUMBER = 1;
                        }
                        else
                        {
                            this.hiRenbanRegistKbn = Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.UPDATE;   // DB更新時に使用
                            this.dto.entryEntity.DATE_NUMBER = numberDays[0].CURRENT_NUMBER + 1;
                            numberDayTimeStamp = numberDays[0].TIME_STAMP;
                        }

                        // 年連番
                        if (numberYeas == null || numberYeas.Length < 1)
                        {
                            this.nenRenbanRegistKbn = Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.INSERT;   // DB更新時に使用
                            this.dto.entryEntity.YEAR_NUMBER = 1;
                        }
                        else
                        {
                            this.nenRenbanRegistKbn = Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.UPDATE;   // DB更新時に使用
                            this.dto.entryEntity.YEAR_NUMBER = numberYeas[0].CURRENT_NUMBER + 1;
                            numberYearTimeStamp = numberYeas[0].TIME_STAMP;
                        }

                        this.dto.entryEntity.SEQ = 1;
                        this.dto.entryEntity.DELETE_FLG = false;
                    }

                    break;

                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    // 画面表示時にSYSTEM_IDを取得しているため採番は割愛

                    // 日連番
                    short beforeKotenCd = -1;
                    short.TryParse(beforDto.entryEntity.KYOTEN_CD.ToString(), out beforeKotenCd);
                    if (beforeKotenCd != kyotenCd
                        || beforDto.entryEntity.KYOTEN_CD != kyotenCd)
                    {
                        if (numberDays == null || numberDays.Length < 1)
                        {
                            this.hiRenbanRegistKbn = Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.INSERT;   // DB更新時に使用
                            this.dto.entryEntity.DATE_NUMBER = 1;
                        }
                        else
                        {
                            this.hiRenbanRegistKbn = Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.UPDATE;   // DB更新時に使用
                            this.dto.entryEntity.DATE_NUMBER = numberDays[0].CURRENT_NUMBER + 1;
                            numberDayTimeStamp = numberDays[0].TIME_STAMP;
                        }
                    }
                    else
                    {
                        this.hiRenbanRegistKbn = Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.NONE;   // DB更新時に使用
                        this.dto.entryEntity.DATE_NUMBER = this.beforDto.entryEntity.DATE_NUMBER;
                        //numberDayTimeStamp = numberDays[0].TIME_STAMP;
                    }
                    // 年連番
                    if (beforeKotenCd != kyotenCd
                        || beforDto.entryEntity.KYOTEN_CD != kyotenCd)
                    {
                        if (numberYeas == null || numberYeas.Length < 1)
                        {
                            this.nenRenbanRegistKbn = Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.INSERT;   // DB更新時に使用
                            this.dto.entryEntity.YEAR_NUMBER = 1;
                        }
                        else
                        {
                            this.nenRenbanRegistKbn = Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.UPDATE;   // DB更新時に使用
                            this.dto.entryEntity.YEAR_NUMBER = numberYeas[0].CURRENT_NUMBER + 1;
                            numberYearTimeStamp = numberYeas[0].TIME_STAMP;
                        }
                    }
                    else
                    {
                        this.nenRenbanRegistKbn = Shougun.Core.Scale.Keiryou.Const.KeiryouConstans.REGIST_KBN.NONE;   // DB更新時に使用
                        this.dto.entryEntity.YEAR_NUMBER = this.beforDto.entryEntity.YEAR_NUMBER;
                        //numberYearTimeStamp = numberYeas[0].TIME_STAMP;
                    }

                    this.dto.entryEntity.SYSTEM_ID = this.beforDto.entryEntity.SYSTEM_ID;     // 更新されないはず
                    this.dto.entryEntity.SEQ = this.beforDto.entryEntity.SEQ + 1;
                    this.dto.entryEntity.DELETE_FLG = false;
                    // 更新前伝票は論理削除
                    this.beforDto.entryEntity.DELETE_FLG = true;

                    break;

                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    this.dto.entryEntity.DELETE_FLG = true;
                    break;

                default:
                    break;

            }
            if (tairyuuKbnFlag)
            {
                this.dto.entryEntity.TAIRYUU_KBN = SqlBoolean.True;
            }
            else
            {
                this.dto.entryEntity.TAIRYUU_KBN = SqlBoolean.False;
            }

            if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
            {
                this.dto.entryEntity.KYOTEN_CD = SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text);
            }

            if (!string.IsNullOrEmpty(this.form.KIHON_KEIRYOU.Text))
            {
                this.dto.entryEntity.KIHON_KEIRYOU = SqlInt16.Parse(this.form.KIHON_KEIRYOU.Text);
            }
            if (this.form.DENPYOU_DATE.Value != null)
            {
                this.dto.entryEntity.DENPYOU_DATE = ((DateTime)this.form.DENPYOU_DATE.Value).Date;
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
            if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOUSHA_CD.Text))
            {
                this.dto.entryEntity.EIGYOU_TANTOUSHA_CD = this.form.EIGYOU_TANTOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOUSHA_NAME.Text))
            {
                this.dto.entryEntity.EIGYOU_TANTOUSHA_NAME = this.form.EIGYOU_TANTOUSHA_NAME.Text;
            }
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
            if (!string.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text))
            {
                this.dto.entryEntity.KEITAI_KBN_CD = SqlInt16.Parse(this.form.KEITAI_KBN_CD.Text);
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
            // NET_TOTAL
            if (!string.IsNullOrEmpty(this.form.NET_TOTAL.Text))
            {
                this.dto.entryEntity.NET_TOTAL = SqlDecimal.Parse(this.form.NET_TOTAL.Text);

            }
            if (!string.IsNullOrEmpty(this.form.KEIRYOU_NUMBER.Text))
            {
                this.dto.entryEntity.KEIRYOU_NUMBER = SqlInt64.Parse(this.form.KEIRYOU_NUMBER.Text);
            }

            // 共有情報設定
            var dataBinderKeiryouEntry = new DataBinderLogic<T_KEIRYOU_ENTRY>(this.dto.entryEntity);
            dataBinderKeiryouEntry.SetSystemProperty(this.dto.entryEntity, false);

            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                /// 20141117 Houkakou 「更新日、登録日の見直し」　start
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                /// 20141117 Houkakou 「更新日、登録日の見直し」　end
                    // 更新の場合、前回の作成情報を更新しない
                    this.dto.entryEntity.CREATE_DATE = this.beforDto.entryEntity.CREATE_DATE;
                    this.dto.entryEntity.CREATE_USER = this.beforDto.entryEntity.CREATE_USER;
                    this.dto.entryEntity.CREATE_PC = this.beforDto.entryEntity.CREATE_PC;
                    break;
                default:
                    break;

            }

            // 最終更新者
            this.dto.entryEntity.UPDATE_USER = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME_RYAKU.ToString();

            // 更新前伝票
            var dataBinderBeforKeiryouEntry = new DataBinderLogic<T_KEIRYOU_ENTRY>(this.beforDto.entryEntity);
            dataBinderKeiryouEntry.SetSystemProperty(this.beforDto.entryEntity, true);


            // Detail
            List<T_KEIRYOU_DETAIL> keiryouDetailEntitys = new List<T_KEIRYOU_DETAIL>();

            SqlInt64 detailSysId = -1;
            foreach (Row dr in this.form.gcMultiRow1.Rows)
            {
                if (dr.IsNewRow || string.IsNullOrEmpty((string)dr.Cells["ROW_NO"].Value.ToString()))
                {
                    continue;
                }

                T_KEIRYOU_DETAIL temp = new T_KEIRYOU_DETAIL();

                // モードに依存する処理
                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // 新規の場合は、既にEntryで採番しているので、それに+1する
                        detailSysId = this.accessor.createSystemIdForKeiryou();
                        temp.DETAIL_SYSTEM_ID = detailSysId;

                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:

                        // DETAIL_SYSTEM_IDの採番
                        if (dr.Cells["DETAIL_SYSTEM_ID"].Value == null
                            || string.IsNullOrEmpty(dr.Cells["DETAIL_SYSTEM_ID"].Value.ToString()))
                        {
                            // 修正モードでT_KEIRYOU_DETAILが初めて登録されるパターンも張るはずなので、
                            // Detailが無ければ新たに採番(更新モードの場合、ここで初めて採番する)
                            detailSysId = this.accessor.createSystemIdForKeiryou();
                        }
                        else
                        {
                            // 既に登録されていればそのまま使う
                            detailSysId = SqlInt64.Parse(dr.Cells["DETAIL_SYSTEM_ID"].Value.ToString());
                        }

                        temp.DETAIL_SYSTEM_ID = detailSysId;
                        break;

                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        // DETAIL_SYSTEM_IDの採番
                        if (dr.Cells["DETAIL_SYSTEM_ID"].Value == null
                            || string.IsNullOrEmpty(dr.Cells["DETAIL_SYSTEM_ID"].Value.ToString()))
                        {
                            // 修正モードでT_KEIRYOU_DETAILが初めて登録されるパターンも張るはずなので、
                            // Detailが無ければ新たに採番(更新モードの場合、ここで初めて採番する)
                            detailSysId = this.accessor.createSystemIdForKeiryou();
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

                if (!this.dto.entryEntity.SYSTEM_ID.IsNull)
                {
                    temp.SYSTEM_ID = this.dto.entryEntity.SYSTEM_ID.Value;
                }
                if (!this.dto.entryEntity.SEQ.IsNull)
                {
                    temp.SEQ = this.dto.entryEntity.SEQ;
                }
                if (!this.dto.entryEntity.KEIRYOU_NUMBER.IsNull)
                {
                    temp.KEIRYOU_NUMBER = this.dto.entryEntity.KEIRYOU_NUMBER.Value;
                }


                if (!string.IsNullOrEmpty(dr.Cells["ROW_NO"].Value.ToString()))
                {
                    temp.ROW_NO = SqlInt16.Parse(dr.Cells["ROW_NO"].Value.ToString());
                }
                if (dr.Cells["STACK_JYUURYOU"].Value != null && !string.IsNullOrEmpty(dr.Cells["STACK_JYUURYOU"].Value.ToString()))
                {
                    temp.STACK_JYUURYOU = SqlDecimal.Parse(dr.Cells["STACK_JYUURYOU"].Value.ToString());
                }
                if (dr.Cells["EMPTY_JYUURYOU"].Value != null && !string.IsNullOrEmpty(dr.Cells["EMPTY_JYUURYOU"].Value.ToString()))
                {
                    temp.EMPTY_JYUURYOU = SqlDecimal.Parse(dr.Cells["EMPTY_JYUURYOU"].Value.ToString());
                }
                if (dr.Cells["CHOUSEI_JYUURYOU"].Value != null && !string.IsNullOrEmpty(dr.Cells["CHOUSEI_JYUURYOU"].Value.ToString()))
                {
                    temp.CHOUSEI_JYUURYOU = SqlDecimal.Parse(dr.Cells["CHOUSEI_JYUURYOU"].Value.ToString());
                }
                if (dr.Cells["CHOUSEI_PERCENT"].Value != null && !string.IsNullOrEmpty(dr.Cells["CHOUSEI_PERCENT"].Value.ToString()))
                {
                    temp.CHOUSEI_PERCENT = SqlDecimal.Parse(dr.Cells["CHOUSEI_PERCENT"].Value.ToString());
                }
                if (dr.Cells["YOUKI_CD"].Value != null && !string.IsNullOrEmpty(dr.Cells["YOUKI_CD"].Value.ToString()))
                {
                    temp.YOUKI_CD = dr.Cells["YOUKI_CD"].Value.ToString();
                }
                if (dr.Cells["YOUKI_SUURYOU"].Value != null && !string.IsNullOrEmpty(dr.Cells["YOUKI_SUURYOU"].Value.ToString()))
                {
                    temp.YOUKI_SUURYOU = SqlDecimal.Parse(dr.Cells["YOUKI_SUURYOU"].Value.ToString());
                }
                if (dr.Cells["YOUKI_JYUURYOU"].Value != null && !string.IsNullOrEmpty(dr.Cells["YOUKI_JYUURYOU"].Value.ToString()))
                {
                    temp.YOUKI_JYUURYOU = SqlDecimal.Parse(dr.Cells["YOUKI_JYUURYOU"].Value.ToString());
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
                if (dr.Cells["NET_JYUURYOU"].Value != null && !string.IsNullOrEmpty(dr.Cells["NET_JYUURYOU"].Value.ToString()))
                {
                    temp.NET_JYUURYOU = SqlDecimal.Parse(dr.Cells["NET_JYUURYOU"].Value.ToString());
                }

                // 明細で選択された品名の情報を取得
                M_HINMEI targetHinmei = null;
                if (!string.IsNullOrEmpty(temp.HINMEI_CD))
                {
                    var tempHinmei = this.accessor.GetHinmeiDataByCd(temp.HINMEI_CD);
                    if (tempHinmei != null && !string.IsNullOrEmpty(tempHinmei.HINMEI_CD))
                    {
                        targetHinmei = tempHinmei;
                    }
                }

                if (dr.Cells["MEISAI_BIKOU"].Value != null)
                {
                    temp.MEISAI_BIKOU = dr.Cells["MEISAI_BIKOU"].Value.ToString();
                }

                var dbLogic = new DataBinderLogic<T_KEIRYOU_DETAIL>(temp);
                dbLogic.SetSystemProperty(temp, false);

                keiryouDetailEntitys.Add(temp);
            }

            this.dto.detailEntity = new T_KEIRYOU_DETAIL[keiryouDetailEntitys.Count];
            this.dto.detailEntity = keiryouDetailEntitys.ToArray<T_KEIRYOU_DETAIL>();

            // S_NUMBER_YEAR
            this.dto.numberYear.NUMBERED_YEAR = numberedYear;
            this.dto.numberYear.DENSHU_KBN_CD = (short)DENSHU_KBN.KEIRYOU; ;
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
            this.dto.numberDay.DENSHU_KBN_CD = (short)DENSHU_KBN.KEIRYOU;
            this.dto.numberDay.KYOTEN_CD = kyotenCd;
            this.dto.numberDay.CURRENT_NUMBER = this.dto.entryEntity.DATE_NUMBER;
            this.dto.numberDay.DELETE_FLG = false;
            if (numberDayTimeStamp != null)
            {
                this.dto.numberDay.TIME_STAMP = numberDayTimeStamp;
            }
            var dataBinderNumberDay = new DataBinderLogic<S_NUMBER_DAY>(this.dto.numberDay);
            dataBinderNumberDay.SetSystemProperty(this.dto.numberDay, false);

            // S_NUMBER_RECEIPTの更新

            // 領収証番号採番
            if (KeiryouConstans.RYOSYUSYO_KBN_1.Equals(this.form.denpyouHakouPopUpDTO.Ryousyusyou))
            {
                DateTime hiduke = DateTime.Now;
                var numberReceipt = this.accessor.GetNumberReceipt(hiduke, kyotenCd);
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

                this.dto.numberReceipt.NUMBERED_DAY = hiduke.Date;
                this.dto.numberReceipt.KYOTEN_CD = kyotenCd;
                this.dto.numberReceipt.DELETE_FLG = false;
                var dataBinderNumberReceipt = new DataBinderLogic<S_NUMBER_RECEIPT>(this.dto.numberReceipt);
                dataBinderNumberReceipt.SetSystemProperty(this.dto.numberReceipt, false);
            }

            LogUtility.DebugMethodEnd();
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

            // テンプレートを取得
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
            // 設定したテンプレートをコピー
            this.form.gcMultiRow1.Template = newTemplate;

            this.form.gcMultiRow1.ResumeLayout();
        }

        /// <summary>
        /// 手入力
        /// </summary>
        /// <param name="headerNames">非表示にするカラム名一覧</param>
        /// <param name="cellNames">非表示にするセル名一覧</param>
        /// <param name="visibleFlag">各カラム、セルのVisibleに設定するbool</param>
        private void Tenyuuryoku(string[] headerNames, string[] cellNames, string propertyName, bool visibleFlag)
        {
            this.form.gcMultiRow1.SuspendLayout();
            this.form.gcMultiRow1.BeginEdit(false);

            // テンプレートを取得
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
            // 設定したテンプレートをコピー
            this.form.gcMultiRow1.Template = newTemplate;

            // ▽元のデータをコピーする
            this.form.gcMultiRow1.Rows.Clear();
            // CreateDataTableForEntityがMultiRowを動的に作成しないため、ここでEntity分行数を追加する
            // Entity数Rowを作ると最終行が無いので、Etity + 1でループさせる

            if (this.dto.detailEntity != null && this.dto.detailEntity.Length >= 1)
            {
                for (int i = 1; i < this.dto.detailEntity.Length + 1; i++)
                {
                    this.form.gcMultiRow1.Rows.Add();
                }
            }

            var dataBinder = new DataBinderLogic<T_KEIRYOU_DETAIL>(this.dto.detailEntity);
            dataBinder.CreateDataTableForEntity(this.form.gcMultiRow1, this.dto.detailEntity);
            // 選択されたセルの色をクリアする
            this.form.gcMultiRow1.Rows[this.form.gcMultiRow1.RowCount - 1].Cells[CELL_NAME_STAK_JYUURYOU].Style.BackColor = System.Drawing.Color.Empty;
            // △元のデータをコピーする

            this.form.gcMultiRow1.EndEdit();
            this.form.gcMultiRow1.ResumeLayout();


        }

        /// <summary>
        /// 指定した計量番号のデータが存在するか返す
        /// </summary>
        /// <param name="keireNumber">計量番号</param>
        /// <returns>true:存在する, false:存在しない</returns>
        internal bool IsExistKeiryouData(long keireNumber)
        {
            LogUtility.DebugMethodStart(keireNumber);

            bool returnVal = false;

            try
            {
                if (0 <= keireNumber)
                {
                    var ukeireEntrys = this.accessor.GetKeiryouEntry(keireNumber, this.form.SEQ);
                    if (ukeireEntrys != null
                        && 0 < ukeireEntrys.Length)
                    {
                        returnVal = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("IsExistKeiryouData", ex1);
                this.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsExistKeiryouData", ex);
                this.errmessage.MessageBoxShow("E245", "");
            }
            
            LogUtility.DebugMethodEnd();
            return returnVal;
        }

        /// <summary>
        /// 指定した受付番号のデータが存在するか返す
        /// </summary>
        /// <param name="ukeireNumber">計量番号</param>
        /// <returns>true:存在する, false:存在しない</returns>
        internal bool IsExistUketsukeData(long uketsukeNumber)
        {
            LogUtility.DebugMethodStart(uketsukeNumber);

            bool returnVal = false;

            try
            {
                if (0 <= uketsukeNumber)
                {
                    var ukeireEntrys = this.accessor.GetUketukeEntry(uketsukeNumber);
                    if (ukeireEntrys != null
                        && 0 < ukeireEntrys.Length)
                    {
                        returnVal = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("IsExistUketsukeData", ex1);
                this.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsExistUketsukeData", ex);
                this.errmessage.MessageBoxShow("E245", "");
            }

            LogUtility.DebugMethodEnd();
            return returnVal;
        }
        /// <summary>
        /// 受付入力データを計量入力データに入れ替える
        /// </summary>
        /// <param name="uketsukeNumber">受付番号</param>
        internal bool setUketsukeEntity(long uketsukeNumber)
        {
            try
            {
                LogUtility.DebugMethodStart(uketsukeNumber);

                // 受付入力
                var uketsukeEntitys = accessor.GetUketukeEntry(uketsukeNumber);
                if (uketsukeEntitys == null || uketsukeEntitys.Length < 1)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                else
                {
                    var haishaJokyoCd = uketsukeEntitys[0].HAISHA_JOKYO_CD.ToString();

                    // 配車状況が「1:受注」「2:配車」「3:計上」以外は遷移できない
                    if (SalesPaymentConstans.HAISHA_JOKYO_CD_CANCEL.Equals(haishaJokyoCd) || SalesPaymentConstans.HAISHA_JOKYO_CD_NASHI.Equals(haishaJokyoCd))
                    {
                        // メッセージ表示
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E101", "伝票", "売上");

                        // 入力受付番号クリア
                        this.form.UKETSUKE_NUMBER.Text = "";

                        //フォーカスを受付番号にする
                        this.form.UKETSUKE_NUMBER.Focus();

                        // 処理終了
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    this.dto.uketsukeEntity = uketsukeEntitys[0];
                    // 受付⇒計量
                    this.dto.entryEntity = this.dto.ukeireEntityClone();

                    // 出荷受付入力からの入れ替えの際には、荷積業者、荷積現場を入れ込む
                    T_UKETSUKE_SK_ENTRY[] uketsukeSKEntrys = accessor.GetUketsukeEntry(uketsukeNumber);
                    if (uketsukeSKEntrys != null && uketsukeSKEntrys.Length >= 1)
                    {
                        // 荷積業者、荷積現場を入れ込む
                        this.dto.entryEntity.NIZUMI_GYOUSHA_CD = this.dto.uketsukeEntity.NIOROSHI_GYOUSHA_CD;
                        this.dto.entryEntity.NIZUMI_GYOUSHA_NAME = this.dto.uketsukeEntity.NIOROSHI_GYOUSHA_NAME;
                        this.dto.entryEntity.NIZUMI_GENBA_CD = this.dto.uketsukeEntity.NIOROSHI_GENBA_CD;
                        this.dto.entryEntity.NIZUMI_GENBA_NAME = this.dto.uketsukeEntity.NIOROSHI_GENBA_NAME;

                        // 出荷受付入力には荷降業者、荷降現場はないため、初期化
                        this.dto.entryEntity.NIOROSHI_GYOUSHA_CD = null;
                        this.dto.entryEntity.NIOROSHI_GYOUSHA_NAME = null;
                        this.dto.entryEntity.NIOROSHI_GENBA_CD = null;
                        this.dto.entryEntity.NIOROSHI_GENBA_NAME = null;
                    }
                    else
                    {
                        this.dto.entryEntity.NIOROSHI_GYOUSHA_CD = this.dto.uketsukeEntity.NIOROSHI_GYOUSHA_CD;
                        this.dto.entryEntity.NIOROSHI_GYOUSHA_NAME = this.dto.uketsukeEntity.NIOROSHI_GYOUSHA_NAME;
                        this.dto.entryEntity.NIOROSHI_GENBA_CD = this.dto.uketsukeEntity.NIOROSHI_GENBA_CD;
                        this.dto.entryEntity.NIOROSHI_GENBA_NAME = this.dto.uketsukeEntity.NIOROSHI_GENBA_NAME;
                    }
                }

                // 受付明細
                T_UKETSUKE_SS_DETAIL[] uketsukeDetails = accessor.GetUketsukeDetail(uketsukeNumber);

                if (uketsukeDetails == null || uketsukeDetails.Length < 1)
                {
                    this.dto.uketsukeDetailEntity = new T_UKETSUKE_SS_DETAIL[] { new T_UKETSUKE_SS_DETAIL() };
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("setUketsukeEntity", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("setUketsukeEntity", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);

            return true;
        }

        /// <summary>
        /// 重量値、金額値用フォーマット
        /// </summary>
        /// <param name="sender"></param>
        internal bool ToAmountValue(object sender)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(sender);

                if (sender == null)
                {
                    return ret;
                }

                var value = PropertyUtility.GetTextOrValue(sender);
                if (!string.IsNullOrEmpty(value))
                {
                    PropertyUtility.SetTextOrValue(sender, FormatUtility.ToAmountValue(value));
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ToAmountValue", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 重量値フォーマット(Detial用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ToAmountValueForDetail(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (sender == null)
            {
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
        /// 基準正味を取得
        /// ①割振重量があれば割振重量をreturn
        /// ②割振重量がなければ総重量-空車重量をreturn
        /// </summary>
        /// <returns></returns>
        private decimal GetCriterionNetForCurrent()
        {
            LogUtility.DebugMethodStart();
            // 返却用
            decimal rtnValue = 0;

            Row targetRow = this.form.gcMultiRow1.CurrentRow;
            decimal warifuriJyuuryou = 0;
            if (decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value), out warifuriJyuuryou))
            {
                rtnValue = warifuriJyuuryou;
            }
            else
            {
                decimal stakJyuuryou = 0;
                decimal emptyJyuuryou = 0;
                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stakJyuuryou);
                decimal.TryParse(Convert.ToString(targetRow.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou);
                rtnValue = (decimal)stakJyuuryou - (decimal)emptyJyuuryou;
            }
            LogUtility.Debug("基準正味:" + rtnValue);
            LogUtility.DebugMethodEnd();

            return rtnValue;

        }

        /// <summary>
        /// 基準正味を取得
        /// ①割振重量があれば割振重量をreturn
        /// ②割振重量がなければ総重量-空車重量をreturn
        /// </summary>
        /// <returns></returns>
        private decimal GetCriterionNet(decimal warifuriJyuuryou, decimal stakJyuuryou, decimal emptyJyuuryou)
        {
            LogUtility.DebugMethodStart(warifuriJyuuryou, stakJyuuryou, emptyJyuuryou);
            // 返却用
            decimal rtnValue = 0;
            if (warifuriJyuuryou > 0)
            {
                rtnValue = warifuriJyuuryou;
            }
            else
            {
                rtnValue = stakJyuuryou - emptyJyuuryou;
            }
            LogUtility.Debug("基準正味:" + rtnValue);
            LogUtility.DebugMethodEnd();
            return rtnValue;
        }


        /// <summary>
        /// DetailからJyuuryouDtoリストへ値を設定
        /// </summary>
        internal void SetJyuuryouDataToDtoList()
        {
            /** 
             * warifuriNo   ：jyuuryouDtoListのindex
             * warifuriRowNo：jyuuryouDtoList内の1要素内のindex
             * **/

            LogUtility.DebugMethodStart();

            this.jyuuryouDtoList = new List<List<JyuuryouDto>>();
            if (this.form.gcMultiRow1.RowCount < 1)
            {
                return;
            }
            short i = -1;    // 行カウント用
            int warihuriNo = 0;     // 内部的に使う割振用No
            bool isValidJyuuryouList = false;
            List<JyuuryouDto> jyuuryouList = new List<JyuuryouDto>();

            this.form.gcMultiRow1.BeginEdit(false);
            for (int j = 0; j < this.form.gcMultiRow1.RowCount; j++)
            {
                Row row = this.form.gcMultiRow1.Rows[j];

                if (row.IsNewRow)
                {
                    // 新規行の前までの情報をセット
                    i = -1;
                    this.jyuuryouDtoList.Add(jyuuryouList);
                    warihuriNo++;
                    jyuuryouList = new List<JyuuryouDto>();
                    continue;
                }

                i++;

                // 必要なデータを数値に変換
                decimal stackJyuuryou = 0;
                decimal emptyJyuuryou = 0;
                decimal warihuriJyuuryou = 0;
                decimal warihuriPercent = 0;
                decimal chouseiJyuuryou = 0;
                decimal chouseiPercent = 0;
                decimal youkiJyuuryou = 0;
                decimal netJyuuryou = 0;

                JyuuryouDto jyuuryouDto = new JyuuryouDto();

                if (decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stackJyuuryou))
                {
                    isValidJyuuryouList = true;
                    jyuuryouDto.stackJyuuryou = stackJyuuryou;
                }
                if (decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou))
                {
                    isValidJyuuryouList = true;
                    jyuuryouDto.emptyJyuuryou = emptyJyuuryou;
                }
                if (decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value), out warihuriJyuuryou))
                {
                    jyuuryouDto.warifuriJyuuryou = warihuriJyuuryou;
                }
                if (decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_WARIFURI_PERCENT].Value), out warihuriPercent))
                {
                    jyuuryouDto.warifuriPercent = warihuriPercent;
                }
                if (decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value), out chouseiJyuuryou))
                {
                    jyuuryouDto.chouseiJyuuryou = chouseiJyuuryou;
                }
                if (decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_CHOUSEI_PERCENT].Value), out chouseiPercent))
                {
                    jyuuryouDto.chouseiPercent = chouseiPercent;
                }
                if (decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_YOUKI_JYUURYOU].Value), out youkiJyuuryou))
                {
                    jyuuryouDto.youkiJyuuryou = youkiJyuuryou;
                }
                if (decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_NET_JYUURYOU].Value), out netJyuuryou))
                {
                    jyuuryouDto.netJyuuryou = netJyuuryou;
                }

                row.Cells[CELL_NAME_WARIHURI_NO].Value = warihuriNo;
                row.Cells[CELL_NAME_WARIHURIROW_NO].Value = i;

                jyuuryouList.Add(jyuuryouDto);

                if (j + 1 < this.form.gcMultiRow1.RowCount
                    && !string.IsNullOrEmpty(Convert.ToString(this.form.gcMultiRow1.Rows[j + 1].Cells[CELL_NAME_STAK_JYUURYOU].Value)))
                {
                    // 総重量が入っている箇所からは割振が振りなおされると判断する
                    i = -1;
                    // ここまでの情報を1セットとして格納
                    this.jyuuryouDtoList.Add(jyuuryouList);
                    warihuriNo++;
                    jyuuryouList = new List<JyuuryouDto>();
                }
                else if (j + 1 == this.form.gcMultiRow1.RowCount)
                {
                    // 最終行もセット
                    this.jyuuryouDtoList.Add(jyuuryouList);
                }
            }

            if (!isValidJyuuryouList)
            {
                // 有効な総重量 or 空車重量がなかった場合
                this.jyuuryouDtoList = new List<List<JyuuryouDto>>();
            }

            this.form.gcMultiRow1.EndEdit();
            this.form.gcMultiRow1.NotifyCurrentCellDirty(false);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// JyuuryouDtoリストからMultiRowへ値を設定
        /// </summary>
        private void SetJyuuryouDataToMultiRow(bool dataLoad)
        {
            LogUtility.DebugMethodStart(dataLoad);

            if (this.jyuuryouDtoList == null
                || this.jyuuryouDtoList.Count < 1)
            {
                return;
            }

            if (this.form.gcMultiRow1.Rows.Count < 1)
            {
                return;
            }

            this.form.gcMultiRow1.BeginEdit(false);

            // MultiRowの重量値系を初期化
            foreach (var row in this.form.gcMultiRow1.Rows)
            {
                row.Cells[CELL_NAME_STAK_JYUURYOU].Value = null;
                row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value = null;
                row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value = null;
                row.Cells[CELL_NAME_WARIFURI_PERCENT].Value = null;
                row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value = null;
                row.Cells[CELL_NAME_CHOUSEI_PERCENT].Value = null;
                row.Cells[CELL_NAME_YOUKI_JYUURYOU].Value = null;
                row.Cells[CELL_NAME_NET_JYUURYOU].Value = null;
            }

            // 最初にMultiRowのサイズを増やしておく
            int l = 0;
            foreach (var countList in this.jyuuryouDtoList)
            {
                l += countList.Count;
            }

            while (this.form.gcMultiRow1.Rows.Count < l + 1)
            {
                this.form.gcMultiRow1.Rows.Add();
            }

            int i = 0;      // MultiRowのindex
            for (int j = 0; j < this.jyuuryouDtoList.Count; j++)
            {
                var jyuuryouDtos = this.jyuuryouDtoList[j];

                if (jyuuryouDtos == null
                    || jyuuryouDtos.Count < 1)
                {
                    continue;
                }

                if (this.form.gcMultiRow1.Rows.Count <= i)
                {
                    // MultiRowの配列が無くなったら終わり
                    break;
                }

                for (int k = 0; k < jyuuryouDtos.Count; k++)
                {
                    var jyuuryouDto = jyuuryouDtos[k];

                    if (jyuuryouDto == null)
                    {
                        continue;
                    }

                    if (this.form.gcMultiRow1.Rows.Count <= i)
                    {
                        // MultiRowの配列が無くなったら
                        // break;
                    }

                    // MultiRowへ設定
                    Row row = this.form.gcMultiRow1.Rows[i];
                    if (row == null)
                    {
                        continue;
                    }

                    row.Cells[CELL_NAME_STAK_JYUURYOU].Value = jyuuryouDto.stackJyuuryou;
                    row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value = jyuuryouDto.emptyJyuuryou;
                    row.Cells[CELL_NAME_NET_JYUURYOU].Value = jyuuryouDto.netJyuuryou;
                    row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value = jyuuryouDto.warifuriJyuuryou;
                    row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value = jyuuryouDto.chouseiJyuuryou;
                    row.Cells[CELL_NAME_YOUKI_JYUURYOU].Value = jyuuryouDto.youkiJyuuryou;
                    row.Cells[CELL_NAME_WARIFURI_PERCENT].Value = jyuuryouDto.warifuriPercent;
                    row.Cells[CELL_NAME_CHOUSEI_PERCENT].Value = jyuuryouDto.chouseiPercent;


                    // 基準正味
                    decimal warifuriJyuuryou = 0;
                    if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value)))
                    {
                        decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value), out warifuriJyuuryou);
                    }
                    decimal stackJyuuryou = 0;
                    if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_STAK_JYUURYOU].Value)))
                    {
                        decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_STAK_JYUURYOU].Value), out stackJyuuryou);
                    }
                    decimal emptyJyuuryou = 0;
                    if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value)))
                    {
                        decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value), out emptyJyuuryou);
                    }
                    decimal criterionNet = this.GetCriterionNet(warifuriJyuuryou, stackJyuuryou, emptyJyuuryou);

                    // No.2703-->
                    if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value)))
                    {
                        decimal chouseiJyuuryou = 0;  // 調整kg
                        decimal.TryParse(Convert.ToString(row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value), out chouseiJyuuryou);

                        // 調整%計算
                        decimal chouseiPercent = 0;
                        if (criterionNet != 0)
                        {
                            if (decimal.TryParse(Convert.ToString((chouseiJyuuryou / criterionNet) * 100), out chouseiPercent))
                            {
                                if (SqlInt16.Parse(this.form.KIHON_KEIRYOU.Text) == 1)
                                {   // 受入
                                    chouseiPercent = (Shougun.Core.Scale.Keiryou.Utility.CommonCalc.FractionCalc((decimal)(chouseiJyuuryou / criterionNet) * 100, (int)this.dto.sysInfoEntity.UKEIRE_CHOUSEI_WARIAI_HASU_CD, (short)this.dto.sysInfoEntity.UKEIRE_CHOUSEI_WARIAI_HASU_KETA));
                                }
                                else
                                {   // 出荷
                                    chouseiPercent = (Shougun.Core.Scale.Keiryou.Utility.CommonCalc.FractionCalc((decimal)(chouseiJyuuryou / criterionNet) * 100, (int)this.dto.sysInfoEntity.SHUKKA_CHOUSEI_WARIAI_HASU_CD, (short)this.dto.sysInfoEntity.SHUKKA_CHOUSEI_WARIAI_HASU_KETA));
                                }
                                row.Cells[CELL_NAME_CHOUSEI_PERCENT].Value = chouseiPercent;
                            }
                        }
                        else
                        {
                            row.Cells[CELL_NAME_CHOUSEI_PERCENT].Value = chouseiPercent;
                        }
                    }
                    // No.2703<--

                    row.Cells[CELL_NAME_WARIHURI_NO].Value = j;
                    row.Cells[CELL_NAME_WARIHURIROW_NO].Value = k;

                    // 割振kg,割振%が設定されている場合は入力可能にしておかないと変更できない
                    // TODO: 重量取込ボタンとかの状態によって入力可能状態を変更しなければいけないかも
                    if (row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value != null)
                    {
                        row.Cells[CELL_NAME_WARIFURI_JYUURYOU].ReadOnly = false;
                        row.Cells[CELL_NAME_WARIFURI_JYUURYOU].UpdateBackColor(false);


                        row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].ReadOnly = false;
                        row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].UpdateBackColor(false);

                    }
                    else
                    {

                        if (k > 0)
                        {
                            row.Cells[CELL_NAME_WARIFURI_JYUURYOU].ReadOnly = true;
                            row.Cells[CELL_NAME_WARIFURI_JYUURYOU].UpdateBackColor(true);


                            row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].ReadOnly = true;
                            row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].UpdateBackColor(true);

                        }


                    }



                    if (row.Cells[CELL_NAME_WARIFURI_PERCENT].Value != null)
                    {
                        row.Cells[CELL_NAME_WARIFURI_PERCENT].ReadOnly = false;
                        row.Cells[CELL_NAME_WARIFURI_PERCENT].UpdateBackColor(false);

                        row.Cells[CELL_NAME_CHOUSEI_PERCENT].ReadOnly = false;
                        row.Cells[CELL_NAME_CHOUSEI_PERCENT].UpdateBackColor(false);

                    }
                    else
                    {

                        if (k > 0)
                        {
                            row.Cells[CELL_NAME_WARIFURI_PERCENT].ReadOnly = true;
                            row.Cells[CELL_NAME_WARIFURI_PERCENT].UpdateBackColor(true);


                            row.Cells[CELL_NAME_CHOUSEI_PERCENT].ReadOnly = true;
                            row.Cells[CELL_NAME_CHOUSEI_PERCENT].UpdateBackColor(true);

                        }

                    }

                    // 総重量、空車重量の入力制限制御
                    // 割振が設定されている行だったら編集不可とする
                    bool isReadOnlyForStackJyuuryou = false;

                    // DBから取得する場合
                    if (dataLoad)
                    {
                        if (0 < k && (!row.Cells[CELL_NAME_WARIFURI_PERCENT].ReadOnly || !row.Cells[CELL_NAME_WARIFURI_JYUURYOU].ReadOnly))
                        {
                            isReadOnlyForStackJyuuryou = true;
                        }

                        row.Cells[CELL_NAME_STAK_JYUURYOU].ReadOnly = isReadOnlyForStackJyuuryou;
                        row.Cells[CELL_NAME_STAK_JYUURYOU].UpdateBackColor(isReadOnlyForStackJyuuryou);
                        row.Cells[CELL_NAME_EMPTY_JYUURYOU].ReadOnly = isReadOnlyForStackJyuuryou;
                        row.Cells[CELL_NAME_EMPTY_JYUURYOU].UpdateBackColor(isReadOnlyForStackJyuuryou);

                    }
                    else
                    {
                        if (
                            (row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value != null
                                && !string.IsNullOrEmpty(row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value.ToString()))
                            || (row.Cells[CELL_NAME_WARIFURI_PERCENT].Value != null
                                && !string.IsNullOrEmpty(row.Cells[CELL_NAME_WARIFURI_PERCENT].Value.ToString()))
                            || (row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value != null
                                && !string.IsNullOrEmpty(row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].Value.ToString()))
                            || (row.Cells[CELL_NAME_CHOUSEI_PERCENT].Value != null
                                && !string.IsNullOrEmpty(row.Cells[CELL_NAME_CHOUSEI_PERCENT].Value.ToString()))
                            )
                        {
                            isReadOnlyForStackJyuuryou = true;
                            isReadOnlyForStackJyuuryou = true;
                        }

                        row.Cells[CELL_NAME_STAK_JYUURYOU].ReadOnly = isReadOnlyForStackJyuuryou;
                        row.Cells[CELL_NAME_EMPTY_JYUURYOU].ReadOnly = isReadOnlyForStackJyuuryou;

                        row.Cells[CELL_NAME_STAK_JYUURYOU].UpdateBackColor(false);  // No.2076
                        row.Cells[CELL_NAME_EMPTY_JYUURYOU].UpdateBackColor(false);  // No.2076

                        // 割振、調整のReadOnlyにデフォルト値が設定されるためここで新たに設定する
                        bool isReadOnlyForWarihuriAndChousei = true;
                        if (
                            (row.Cells[CELL_NAME_STAK_JYUURYOU].Value != null
                                && !string.IsNullOrEmpty(row.Cells[CELL_NAME_STAK_JYUURYOU].Value.ToString()))
                            || (row.Cells[CELL_NAME_STAK_JYUURYOU].Value != null
                                && !string.IsNullOrEmpty(row.Cells[CELL_NAME_STAK_JYUURYOU].Value.ToString()))
                            )
                        {
                            isReadOnlyForWarihuriAndChousei = false;
                        }

                        // 割振計算された行用
                        if (0 < k)
                        {
                            isReadOnlyForWarihuriAndChousei = false;
                        }

                        row.Cells[CELL_NAME_WARIFURI_JYUURYOU].ReadOnly = isReadOnlyForWarihuriAndChousei;
                        row.Cells[CELL_NAME_WARIFURI_PERCENT].ReadOnly = isReadOnlyForWarihuriAndChousei;
                        row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].ReadOnly = isReadOnlyForWarihuriAndChousei;
                        row.Cells[CELL_NAME_CHOUSEI_PERCENT].ReadOnly = isReadOnlyForWarihuriAndChousei;

                        row.Cells[CELL_NAME_WARIFURI_JYUURYOU].UpdateBackColor(false);  // No.2076
                        row.Cells[CELL_NAME_WARIFURI_PERCENT].UpdateBackColor(false);  // No.2076
                        row.Cells[CELL_NAME_CHOUSEI_JYUURYOU].UpdateBackColor(false);  // No.2076
                        row.Cells[CELL_NAME_CHOUSEI_PERCENT].UpdateBackColor(false);  // No.2076


                    }


                    i++;
                }
            }

            bool isEmptyRow = true;
            Row[] cloneRows = new Row[this.form.gcMultiRow1.RowCount];
            this.form.gcMultiRow1.Rows.CopyTo(cloneRows, 0);
            foreach (var row in cloneRows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                // 初期化
                isEmptyRow = true;

                foreach (var cell in row.Cells)
                {
                    if (cell.Name.Equals(CELL_NAME_WARIHURI_NO)
                        || cell.Name.Equals(CELL_NAME_WARIHURIROW_NO)
                        || cell.Name.Equals(CELL_NAME_ROW_NO))
                    {
                        // nullになりえない or 非表示項目は判定から除外
                        continue;
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(cell.Value)))
                    {
                        isEmptyRow = false;
                    }
                }

                if (isEmptyRow)
                {
                    this.form.gcMultiRow1.Rows.RemoveAt(row.Index);
                }
            }

            this.form.gcMultiRow1.EndEdit();
            this.form.gcMultiRow1.NotifyCurrentCellDirty(false);

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 重量取込処理
        /// </summary>
        public bool SetJyuuryou()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (!this.headerForm.label1.Visible
                    || string.IsNullOrEmpty(this.headerForm.label1.Text))
                {
                    return ret;
                }

                if (this.form.gcMultiRow1 == null
                    || this.form.gcMultiRow1.Rows == null
                    || this.form.gcMultiRow1.RowCount < 1)
                {
                    return ret;
                }


                string stackJyuuryouOfPreviousRow = string.Empty;
                string emptyJyuuryouOfPreviousRow = string.Empty;

                this.form.gcMultiRow1.Focus();
                //this.form.gcMultiRow1.BeginEdit(false);

                if (this.form.KeizokuKeiryouFlg)
                {
                    Row row = null;

                    // 滞留登録一覧で継続計量を設定して修正モードで開いた場合
                    // 継続計量でも、初回の一度しかここを通さない

                    // 最終行を取得
                    for (int i = 0; i < this.form.gcMultiRow1.RowCount; i++)
                    {
                        row = this.form.gcMultiRow1.Rows[i];

                        if (row.Cells[CELL_NAME_STAK_JYUURYOU].ReadOnly
                                || row.Cells[CELL_NAME_EMPTY_JYUURYOU].ReadOnly)
                        {
                            // 入力できない場合、処理しない
                            continue;
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value))
                                || !string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_WARIFURI_PERCENT].Value)))
                        {
                            // 割り振りが入っている行は省く
                            continue;
                        }

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
                            row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value = this.headerForm.label1.Text;
                            this.form.gcMultiRow1.CurrentCellPosition = new CellPosition(row.Index, CELL_NAME_STAK_JYUURYOU);
                            this.form.gcMultiRow1.CurrentCellPosition = new CellPosition(row.Index, CELL_NAME_EMPTY_JYUURYOU);

                            break;
                        }
                        else
                        {
                            // 総重・空車・正味・割振のない行の場合
                            if (string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_STAK_JYUURYOU].Value))
                                && string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value))
                                && string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_WARIFURI_JYUURYOU].Value))
                                && string.IsNullOrEmpty(Convert.ToString(row.Cells[CELL_NAME_WARIFURI_PERCENT].Value))
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

                            row.Cells[CELL_NAME_STAK_JYUURYOU].Value = this.headerForm.label1.Text;
                            this.form.gcMultiRow1.CurrentCellPosition = new CellPosition(row.Index, CELL_NAME_EMPTY_JYUURYOU);
                            this.form.gcMultiRow1.CurrentCellPosition = new CellPosition(row.Index, CELL_NAME_STAK_JYUURYOU);



                            break;
                        }
                    }

                    if (row != null)
                    {

                        // 計算のため、選択Rowの位置を調整
                        this.form.gcMultiRow1.ClearSelection();
                        this.form.gcMultiRow1.AddSelection(row.Index);

                        if (!this.CalcStackOrEmptyJyuuryou())
                        {
                            ret = false;
                            return ret;
                        }
                    }

                    this.form.KeizokuKeiryouFlg = true;

                }
                else
                {


                    // 受入の場合
                    if (SqlInt16.Parse(this.form.KIHON_KEIRYOU.Text) == 1)
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

                            if (row.Cells[CELL_NAME_STAK_JYUURYOU].ReadOnly
                                    || row.Cells[CELL_NAME_EMPTY_JYUURYOU].ReadOnly)
                            {
                                // 入力できない場合、処理しない
                                continue;
                            }

                            // 重量値計算のためCurrentRowを重量値を取り込む行に変更
                            this.form.gcMultiRow1.ClearSelection();
                            this.form.gcMultiRow1.AddSelection(i);

                            // 割振りがある行は省く
                            if (!string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_WARIFURI_JYUURYOU].Value))
                                || !string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_WARIFURI_PERCENT].Value)))
                            {
                                continue;
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
                                    row.Cells[CELL_NAME_STAK_JYUURYOU].Value = this.headerForm.label1.Text;

                                    this.form.gcMultiRow1.CurrentCellPosition = new CellPosition(row.Index, CELL_NAME_EMPTY_JYUURYOU);
                                    this.form.gcMultiRow1.CurrentCellPosition = new CellPosition(row.Index, CELL_NAME_STAK_JYUURYOU);

                                }
                                else
                                {
                                    // 重量値取り込み
                                    row.Cells[CELL_NAME_STAK_JYUURYOU].Value = emptyJyuuryouOfPreviousRow;
                                    row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value = this.headerForm.label1.Text;


                                    this.form.gcMultiRow1.CurrentCellPosition = new CellPosition(row.Index, CELL_NAME_STAK_JYUURYOU);
                                    this.form.gcMultiRow1.CurrentCellPosition = new CellPosition(row.Index, CELL_NAME_EMPTY_JYUURYOU);
                                }


                                // 正味重量、金額計算
                                if (!this.CalcStackOrEmptyJyuuryou())
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
                                this.form.gcMultiRow1.CurrentCellPosition = new CellPosition(row.Index, CELL_NAME_EMPTY_JYUURYOU);

                                row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value = this.headerForm.label1.Text;
                                row.Cells[CELL_NAME_EMPTY_JYUURYOU].Selected = true;

                                this.form.gcMultiRow1.CurrentCellPosition = new CellPosition(row.Index, CELL_NAME_WARIFURI_JYUURYOU);
                                this.form.gcMultiRow1.CurrentCellPosition = new CellPosition(row.Index, CELL_NAME_EMPTY_JYUURYOU);

                                // 正味重量、金額計算
                                if (!this.CalcStackOrEmptyJyuuryou())
                                {
                                    ret = false;
                                    return ret;
                                }
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
                    // 出荷の場合
                    else if (SqlInt16.Parse(this.form.KIHON_KEIRYOU.Text) == 2)
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
                                // 次行の空車重量にセットするための変数
                                stackJyuuryouOfPreviousRow = row.Cells[CELL_NAME_STAK_JYUURYOU].Value.ToString();
                            }

                            if (row.Cells[CELL_NAME_STAK_JYUURYOU].ReadOnly
                                    || row.Cells[CELL_NAME_EMPTY_JYUURYOU].ReadOnly)
                            {
                                // 入力できない場合、処理しない
                                continue;
                            }

                            // 重量値計算のためCurrentRowを重量値を取り込む行に変更
                            this.form.gcMultiRow1.ClearSelection();
                            this.form.gcMultiRow1.AddSelection(i);

                            // 割振りがある行は省く
                            if (!string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_WARIFURI_JYUURYOU].Value))
                                || !string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_WARIFURI_PERCENT].Value)))
                            {
                                continue;
                            }

                            // 総重量、空車重量の両方が無い場合、
                            if (string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_STAK_JYUURYOU].Value))
                                && string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_EMPTY_JYUURYOU].Value)))
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_NET_JYUURYOU].Value)))
                                {
                                    continue;
                                }


                                if (string.IsNullOrEmpty(stackJyuuryouOfPreviousRow))
                                {
                                    // 重量値取り込み
                                    row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value = this.headerForm.label1.Text;

                                    this.form.gcMultiRow1.CurrentCellPosition = new CellPosition(row.Index, CELL_NAME_STAK_JYUURYOU);
                                    this.form.gcMultiRow1.CurrentCellPosition = new CellPosition(row.Index, CELL_NAME_EMPTY_JYUURYOU);
                                }
                                else
                                {
                                    // 重量値取り込み
                                    row.Cells[CELL_NAME_STAK_JYUURYOU].Value = this.headerForm.label1.Text;
                                    this.form.gcMultiRow1.CurrentCellPosition = new CellPosition(row.Index, CELL_NAME_EMPTY_JYUURYOU);
                                    this.form.gcMultiRow1.CurrentCellPosition = new CellPosition(row.Index, CELL_NAME_STAK_JYUURYOU);

                                    row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value = stackJyuuryouOfPreviousRow;
                                }


                                // 正味重量、金額計算
                                if (!this.CalcStackOrEmptyJyuuryou())
                                {
                                    ret = false;
                                    return ret;
                                }
                                break;
                            }

                            // 空車重量のみ値有り
                            if (string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_STAK_JYUURYOU].Value))
                                && !string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_EMPTY_JYUURYOU].Value)))
                            {
                                row.Cells[CELL_NAME_STAK_JYUURYOU].Value = this.headerForm.label1.Text;

                                this.form.gcMultiRow1.CurrentCellPosition = new CellPosition(row.Index, CELL_NAME_EMPTY_JYUURYOU);
                                this.form.gcMultiRow1.CurrentCellPosition = new CellPosition(row.Index, CELL_NAME_STAK_JYUURYOU);

                                // 正味重量、金額計算
                                if (!this.CalcStackOrEmptyJyuuryou())
                                {
                                    ret = false;
                                    return ret;
                                }
                                break;
                            }

                            // 総重量のみ値有り 
                            if (string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_STAK_JYUURYOU].Value))
                                && !string.IsNullOrEmpty(Convert.ToString(row[CELL_NAME_EMPTY_JYUURYOU].Value)))
                            {
                                continue;
                            }
                        }
                    }


                }

                //this.form.gcMultiRow1.EndEdit();
                //this.form.gcMultiRow1.NotifyCurrentCellDirty(false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetJyuuryou", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;

        }
        /// <summary>
        /// 重量値用リストから値を削除
        /// 引数の数値を元に配列内のデータを削除する
        /// </summary>
        /// <param name="warifuriNo">割振グルーピング番号(MultiRowに隠しコントロールとして設定しているやつ)</param>
        /// <param name="warifuriRowNo">割振グルーピング内の行番号(MultiRowに隠しコントロールとして設定しているやつ)</param>
        private void RemoveJyuuryouDataList(int warifuriNo, short warifuriRowNo)
        {
            /** 
             * warifuriNo   ：jyuuryouDtoListのindex
             * warifuriRowNo：jyuuryouDtoList内の1要素内のindex
             * **/


            LogUtility.DebugMethodStart(warifuriNo, warifuriRowNo);

            if (warifuriNo < 0 || warifuriRowNo < 0)
            {
                // 範囲外は弾く
                return;
            }
            if (warifuriNo <= this.jyuuryouDtoList.Count)
            {
                // 範囲外は弾く
                return;
            }

            var jyuuyouDtoList = this.jyuuryouDtoList[warifuriNo];
            if (warifuriRowNo <= jyuuyouDtoList.Count)
            {
                // 範囲外は弾く
                return;
            }

            jyuuyouDtoList.Remove(jyuuyouDtoList[warifuriRowNo]);

            // TODO: 総重量、空車重量、割振kgの再計算

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// JyuuryouDtoをthis.jyuuryouDtoListに追加します
        /// 指定されたwarifuriNoとwarifuriRowNoの後に追加します
        /// </summary>
        /// <param name="warifuriNo">割振グルーピング番号(MultiRowに隠しコントロールとして設定しているやつ)</param>
        /// <param name="warifuriRowNo">割振グルーピング内の行番号(MultiRowに隠しコントロールとして設定しているやつ)</param>
        /// <param name="targetJyuuryouDto"></param>
        /// <param name="jyuuryouTargetFlag">計算方法の対象を決定するフラグ。true: 割振重量, false: 割振(%)</param>
        private void AddJyuuryouDataList(int warifuriNo, short warifuriRowNo, JyuuryouDto targetJyuuryouDto,
            bool jyuuryouTargetFlag)
        {
            /** 
             * warifuriNo   ：jyuuryouDtoListのindex
             * warifuriRowNo：jyuuryouDtoList内の1要素内のindex
             * **/
            LogUtility.DebugMethodStart(warifuriNo, warifuriRowNo, targetJyuuryouDto, jyuuryouTargetFlag);

            if (targetJyuuryouDto == null)
            {
                return;
            }

            // warifuriNoが範囲外の場合は新規追加するのかどうか判定
            if (warifuriNo < 0 || warifuriRowNo < 0
                || this.jyuuryouDtoList.Count <= warifuriNo)
            {
                // 範囲外が指定されたら何もしない
                return;
            }

            var jyuuyouDtoList = this.jyuuryouDtoList[warifuriNo];
            if (jyuuyouDtoList.Count <= warifuriRowNo)
            {
                // 範囲外は弾く
                return;
            }

            jyuuyouDtoList[warifuriRowNo] = targetJyuuryouDto;

            // 修正箇所から同グループ内の行は再計算が必要なので削除する
            int i = warifuriRowNo + 1;
            while (i < jyuuyouDtoList.Count)
            {
                jyuuyouDtoList.RemoveAt(i);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// Detail必須チェック
        /// Datailが一行以上入力されているかチェックする
        /// </summary>
        /// <returns>true: 一件以上入力されている, false: 一件も入力されていない</returns>
        internal bool CheckRequiredDataForDeital()
        {
            LogUtility.DebugMethodStart();

            bool returnVal = false;
            foreach (var row in this.form.gcMultiRow1.Rows)
            {
                if (row == null) continue;
                if (row.IsNewRow) continue;

                returnVal = true;
                break;
            }

            LogUtility.DebugMethodEnd();
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
            formControlNameList.AddRange(inputUiFormControlNames);
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

                var property = control.GetType().GetProperty("ReadOnly");

                if (property != null)
                {
                    property.SetValue(control, isLock, null);

                }

                property = control.GetType().GetProperty("Enabled");

                if (property != null)
                {
                    property.SetValue(control, !isLock, null);
                }
            }

            // Detailのコントロールを制御
            foreach (Row row in this.form.gcMultiRow1.Rows)
            {
                foreach (var detaiControlName in inputDetailControlNames)
                {
                    row.Cells[detaiControlName].ReadOnly = isLock;
                    row.Cells[detaiControlName].Enabled = !isLock;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 伝票発行ポップアップ用連携オブジェクトを生成する
        /// </summary>
        /// <returns></returns>
        internal Shougun.Core.SalesPayment.DenpyouHakou.ParameterDTOClass createParameterDTOClass()
        {
            // 一度画面で選択されている場合を考慮し、formのParameterDTOClassで初期化
            Shougun.Core.SalesPayment.DenpyouHakou.ParameterDTOClass returnVal = this.form.denpyouHakouPopUpDTO;

            // 新規、修正共通で設定
            returnVal.Torihikisaki_Cd = this.form.TORIHIKISAKI_CD.Text.ToString();
            returnVal.Gyousha_Cd = this.form.GYOUSHA_CD.Text.ToString();
            List<Shougun.Core.SalesPayment.DenpyouHakou.MeiseiDTOClass> meisaiDtoList = this.createMeisaiDtoList();

            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    // 新規作成
                    break;

                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    // DBの情報を復元
                    break;

                default:
                    break;
            }

            return returnVal;
        }


        /// <summary>
        /// 伝票明細リスト生成処理
        /// </summary>
        /// <returns></returns>
        private List<Shougun.Core.SalesPayment.DenpyouHakou.MeiseiDTOClass> createMeisaiDtoList()
        {
            List<Shougun.Core.SalesPayment.DenpyouHakou.MeiseiDTOClass> returnVal = new List<Shougun.Core.SalesPayment.DenpyouHakou.MeiseiDTOClass>();
            foreach (Row row in this.form.gcMultiRow1.Rows)
            {
                Shougun.Core.SalesPayment.DenpyouHakou.MeiseiDTOClass meisaiDtoClass = new Shougun.Core.SalesPayment.DenpyouHakou.MeiseiDTOClass();

                meisaiDtoClass.Uriageshiharai_Kbn = Convert.ToString(row.Cells[CELL_NAME_DENPYOU_KBN_CD].Value);
                meisaiDtoClass.Hinmei_Cd = Convert.ToString(row.Cells[CELL_NAME_HINMEI_CD].Value);
                //meisaiDtoClass.Kingaku = Convert.ToString(row.Cells[CELL_NAME_KINGAKU].Value);
                returnVal.Add(meisaiDtoClass);
            }
            return returnVal;
        }
        /// <summary>
        /// 単位を取得
        /// </summary>
        internal string GetUnit(short unit)
        {
            String rtnUnit = null;
            var units = this.accessor.GetDataByCodeForUnit(unit);

            // 存在チェック
            if (units == null || units.Length < 1)
            {
                // 存在しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "単位");
            }
            else
            {
                // 単位を取得
                rtnUnit = units[0].UNIT_NAME;

            }

            return rtnUnit;
        }

        /// <summary>
        /// 荷積業者、荷積現場、荷降業者、荷降現場の入力制限
        /// </summary>
        internal bool nizumi_nioroshi(bool readOnly)
        {
            bool ret = true;
            try
            {
                this.form.beforNioroshiGenbaCD = string.Empty;
                this.form.beforNioroshiGyoushaCD = string.Empty;
                this.form.beforNizumiGenbaCD = string.Empty;
                this.form.beforNizumiGyoushaCD = string.Empty;

                if (readOnly)
                {
                    // 荷積
                    this.form.NIZUMI_GYOUSHA_CD.ReadOnly = readOnly;
                    //this.form.NIZUMI_GYOUSHA_CD.TabStop = !readOnly;
                    this.form.NIZUMI_GYOUSHA_CD.TabStop = (readOnly && GetTabStop("NIZUMI_GYOUSHA_CD"));    // No.3822
                    this.form.NIZUMI_GYOUSHA_CD.Text = string.Empty;
                    this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
                    this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = readOnly;
                    //this.form.NIZUMI_GYOUSHA_NAME.TabStop = !readOnly;
                    this.form.NIZUMI_GYOUSHA_NAME.TabStop = (!readOnly && GetTabStop("NIZUMI_GYOUSHA_NAME"));    // No.3822

                    this.form.NIZUMI_GENBA_CD.ReadOnly = readOnly;
                    //this.form.NIZUMI_GENBA_CD.TabStop = !readOnly;
                    this.form.NIZUMI_GENBA_CD.TabStop = (readOnly && GetTabStop("NIZUMI_GENBA_CD"));    // No.3822
                    this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                    this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                    this.form.NIZUMI_GENBA_NAME.ReadOnly = readOnly;
                    //this.form.NIZUMI_GENBA_NAME.TabStop = !readOnly;
                    this.form.NIZUMI_GENBA_NAME.TabStop = (!readOnly && GetTabStop("NIZUMI_GENBA_NAME"));    // No.3822
                    this.form.NIZUMI_GYOUSHA_SEARCH_BUTTON.Enabled = !readOnly;
                    this.form.NIZUMI_GENBA_SEARCH_BUTTON.Enabled = !readOnly;

                    // 荷降
                    this.form.NIOROSHI_GYOUSHA_CD.ReadOnly = !readOnly;
                    //this.form.NIOROSHI_GYOUSHA_CD.TabStop = readOnly;
                    this.form.NIOROSHI_GYOUSHA_CD.TabStop = (readOnly && GetTabStop("NIOROSHI_GYOUSHA_CD"));    // No.3822
                    this.form.NIOROSHI_GYOUSHA_NAME.ReadOnly = readOnly;
                    //this.form.NIOROSHI_GYOUSHA_NAME.TabStop = !readOnly;
                    this.form.NIOROSHI_GYOUSHA_NAME.TabStop = (!readOnly && GetTabStop("NIOROSHI_GYOUSHA_NAME"));    // No.3822


                    this.form.NIOROSHI_GENBA_CD.ReadOnly = !readOnly;
                    //this.form.NIOROSHI_GENBA_CD.TabStop = readOnly;
                    this.form.NIOROSHI_GENBA_CD.TabStop = (readOnly && GetTabStop("NIOROSHI_GENBA_CD"));    // No.3822
                    this.form.NIOROSHI_GENBA_NAME.ReadOnly = readOnly;
                    //this.form.NIOROSHI_GENBA_NAME.TabStop = !readOnly;
                    this.form.NIOROSHI_GENBA_NAME.TabStop = (!readOnly && GetTabStop("NIOROSHI_GENBA_NAME"));    // No.3822
                    this.form.NIOROSHI_GYOUSHA_SEARCH_BUTTON.Enabled = readOnly;
                    this.form.NIOROSHI_GENBA_SEARCH_BUTTON.Enabled = readOnly;

                }
                else
                {


                    // 荷積
                    this.form.NIZUMI_GYOUSHA_CD.ReadOnly = readOnly;
                    //this.form.NIZUMI_GYOUSHA_CD.TabStop = !readOnly;
                    this.form.NIZUMI_GYOUSHA_CD.TabStop = (!readOnly && GetTabStop("NIZUMI_GYOUSHA_CD"));    // No.3822
                    this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = !readOnly;
                    //this.form.NIZUMI_GYOUSHA_NAME.TabStop = readOnly;
                    this.form.NIZUMI_GYOUSHA_NAME.TabStop = (readOnly && GetTabStop("NIZUMI_GYOUSHA_NAME"));    // No.3822

                    this.form.NIZUMI_GENBA_CD.ReadOnly = readOnly;
                    //this.form.NIZUMI_GENBA_CD.TabStop = !readOnly;
                    this.form.NIZUMI_GENBA_CD.TabStop = (!readOnly && GetTabStop("NIZUMI_GENBA_CD"));    // No.3822
                    this.form.NIZUMI_GENBA_NAME.ReadOnly = !readOnly;
                    //this.form.NIZUMI_GENBA_NAME.TabStop = readOnly;
                    this.form.NIZUMI_GENBA_NAME.TabStop = (readOnly && GetTabStop("NIZUMI_GENBA_NAME"));    // No.3822
                    this.form.NIZUMI_GYOUSHA_SEARCH_BUTTON.Enabled = !readOnly;
                    this.form.NIZUMI_GENBA_SEARCH_BUTTON.Enabled = !readOnly;


                    // 荷降
                    this.form.NIOROSHI_GYOUSHA_CD.ReadOnly = !readOnly;
                    //this.form.NIOROSHI_GYOUSHA_CD.TabStop = readOnly;
                    this.form.NIOROSHI_GYOUSHA_CD.TabStop = (readOnly && GetTabStop("NIOROSHI_GYOUSHA_CD"));    // No.3822
                    this.form.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
                    this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                    this.form.NIOROSHI_GYOUSHA_NAME.ReadOnly = !readOnly;
                    //this.form.NIOROSHI_GYOUSHA_NAME.TabStop = readOnly;
                    this.form.NIOROSHI_GYOUSHA_NAME.TabStop = (readOnly && GetTabStop("NIOROSHI_GYOUSHA_NAME"));    // No.3822


                    this.form.NIOROSHI_GENBA_CD.ReadOnly = !readOnly;
                    //this.form.NIOROSHI_GENBA_CD.TabStop = readOnly;
                    this.form.NIOROSHI_GENBA_CD.TabStop = (readOnly && GetTabStop("NIOROSHI_GENBA_CD"));    // No.3822
                    this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                    this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                    this.form.NIOROSHI_GENBA_NAME.ReadOnly = !readOnly;
                    //this.form.NIOROSHI_GENBA_NAME.TabStop = readOnly;
                    this.form.NIOROSHI_GENBA_NAME.TabStop = (readOnly && GetTabStop("NIOROSHI_GENBA_NAME"));    // No.3822
                    this.form.NIOROSHI_GYOUSHA_SEARCH_BUTTON.Enabled = readOnly;
                    this.form.NIOROSHI_GENBA_SEARCH_BUTTON.Enabled = readOnly;
                }

                // 割振値、調整値の表示形式初期化
                CalcValueFormatSettingInit();       // No.3443
            }
            catch (Exception ex)
            {
                LogUtility.Error("nizumi_nioroshi", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        // No.2595-->
        /// <summary>
        /// 形態区分チェック
        /// </summary>
        internal bool CheckKeitaiKbn()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                //参照モード、削除モードの場合は処理を行わない
                if (this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG ||
                    this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    return ret;
                }

                // 初期化
                this.form.KEITAI_KBN_NAME_RYAKU.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text))
                {
                    // 形態区分CDがなければ既にエラーが表示されているので何もしない。
                    return ret;
                }

                var keitaiKbnEntity = this.accessor.GetkeitaiKbn(short.Parse(this.form.KEITAI_KBN_CD.Text));
                SqlInt16 denshu_kbn_cd;
                if (string.IsNullOrWhiteSpace(this.form.KIHON_KEIRYOU.Text))
                {
                    denshu_kbn_cd = KeiryouConstans.DENSHU_KBN_CD_KYOTU;
                }
                else
                {
                    denshu_kbn_cd = SqlInt16.Parse(this.form.KIHON_KEIRYOU.Text);
                }
                if (keitaiKbnEntity == null)
                {
                    // エラーメッセージ
                    this.form.KEITAI_KBN_CD.IsInputErrorOccured = true;
                    this.form.KEITAI_KBN_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "形態区分");
                    this.form.KEITAI_KBN_CD.Focus();
                    return ret;
                }
                else if ((keitaiKbnEntity.DENSHU_KBN_CD != denshu_kbn_cd
                    && keitaiKbnEntity.DENSHU_KBN_CD != KeiryouConstans.DENSHU_KBN_CD_KYOTU))
                {
                    // エラーメッセージ
                    this.form.KEITAI_KBN_CD.IsInputErrorOccured = true;
                    this.form.KEITAI_KBN_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "形態区分");
                    this.form.KEITAI_KBN_CD.Focus();
                    return ret;
                }
                else
                {
                    this.form.KEITAI_KBN_NAME_RYAKU.Text = keitaiKbnEntity.KEITAI_KBN_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckKeitaiKbn", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckKeitaiKbn", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        // No.2595<--

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
                    return ret;
                }

                // 初期化
                this.form.UNTENSHA_NAME.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
                {
                    // 運転者CDがなければ既にエラーが表示されているので何もしない。
                    return ret;
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
                    return ret;
                }
                else if (shainEntity.UNTEN_KBN.Equals(SqlBoolean.False))
                {
                    // エラーメッセージ
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    this.form.UNTENSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "運転者");
                    this.form.UNTENSHA_CD.Focus();
                    return ret;
                }
                else
                {
                    this.form.UNTENSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckUntensha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUntensha", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 品名コードの存在チェック（伝種区分が計量、または共通のみ可）
        /// </summary>
        /// <param name="targetRow"></param>
        /// <returns>true: 入力された品名コードが存在する, false: 入力された品名コードが存在しない</returns>
        internal bool CheckHinmeiCd(Row targetRow)
        {
            LogUtility.DebugMethodStart();
            bool returnVal = false;

            try
            {
                if ((targetRow.Cells[CELL_NAME_HINMEI_CD].Value == null))
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
                    // No.2595-->
                    SqlInt16 denshu_kbn_cd;
                    if (string.IsNullOrWhiteSpace(this.form.KIHON_KEIRYOU.Text))
                    {
                        denshu_kbn_cd = KeiryouConstans.DENSHU_KBN_CD_KYOTU;
                    }
                    else
                    {
                        denshu_kbn_cd = SqlInt16.Parse(this.form.KIHON_KEIRYOU.Text);
                    }
                    // No.2595<--
                    // 品名コードがマスタに存在する場合
                    if ((hinmei.DENSHU_KBN_CD != denshu_kbn_cd  // No.2595
                        && hinmei.DENSHU_KBN_CD != KeiryouConstans.DENSHU_KBN_CD_KYOTU))
                    {
                        // 入力された品名コードに紐づく伝種区分が計量、共通以外の場合はエラーメッセージ表示
                        targetRow.Cells[CELL_NAME_HINMEI_CD].Value = null;
                        targetRow.Cells[CELL_NAME_HINMEI_NAME].Value = null;
                        targetRow.Cells[CELL_NAME_DENPYOU_KBN_CD].Value = null;
                        targetRow.Cells[CELL_NAME_DENPYOU_KBN_NAME].Value = null;
                        targetRow.Cells[CELL_NAME_NISUGATA_UNIT_CD].Value = null;
                        targetRow.Cells[CELL_NAME_NISUGATA_NAME_RYAKU].Value = null;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E058", "");
                        return returnVal;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHinmeiCd", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return returnVal;
            }

            returnVal = true;
            LogUtility.DebugMethodEnd();
            return returnVal;
        }

        #region 前の計量番号を取得
        /// <summary>
        /// 前の受付番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="numberValue">受付番号</param>
        /// <returns>前の受付番号</returns>
        internal String GetPreviousNumber(String tableName, String fieldName, String numberValue, out Boolean catchErr)
        {
            catchErr = true;
            String returnVal = string.Empty;
            try
            {
                DataTable dt = new DataTable();
                string selectStr;
                if (String.IsNullOrEmpty(numberValue))
                {
                    selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 ";
                    // データ取得
                    dt = this.accessor.keiryouEntryDao.GetDateForStringSql(selectStr);
                    returnVal = Convert.ToString(dt.Rows[0]["MAX_NUMBER"]);
                    return returnVal;
                }

                // SQL文作成(冗長にならないためsqlファイルで管理しない)

                selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                selectStr += " WHERE " + fieldName + " < " + long.Parse(numberValue);
                selectStr += "   AND DELETE_FLG = 0 ";

                // データ取得
                dt = this.accessor.keiryouEntryDao.GetDateForStringSql(selectStr);
                if (Convert.ToString(dt.Rows[0]["MAX_NUMBER"]) == "")
                {
                    selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 ";
                    // データ取得
                    dt = this.accessor.keiryouEntryDao.GetDateForStringSql(selectStr);
                }

                // MAX_NUMBERをセット
                returnVal = Convert.ToString(dt.Rows[0]["MAX_NUMBER"]);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetPreviousNumber", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetPreviousNumber", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            return returnVal;
        }
        #endregion

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

        #region 次の計量番号を取得
        /// <summary>
        /// 次の受付番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="numberValue">受付番号</param>
        /// <returns>次の受付番号</returns>
        internal String GetNextNumber(String tableName, String fieldName, String numberValue, out Boolean catchErr)
        {
            catchErr = true;
            String returnVal = string.Empty;
            try
            {
                LogUtility.DebugMethodStart(tableName, fieldName, numberValue);
                DataTable dt = new DataTable();
                string selectStr;
                if (String.IsNullOrEmpty(numberValue))
                {
                    selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 ";
                    // データ取得
                    dt = this.accessor.keiryouEntryDao.GetDateForStringSql(selectStr);
                    returnVal = Convert.ToString(dt.Rows[0]["MIN_NUMBER"]);
                    return returnVal;
                }

                // SQL文作成(冗長にならないためsqlファイルで管理しない)
                selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                selectStr += " WHERE " + fieldName + " > " + long.Parse(numberValue);
                selectStr += "   AND DELETE_FLG = 0 ";

                // データ取得
                dt = this.accessor.keiryouEntryDao.GetDateForStringSql(selectStr);
                if (Convert.ToString(dt.Rows[0]["MIN_NUMBER"]) == "")
                {
                    selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 ";
                    // データ取得
                    dt = this.accessor.keiryouEntryDao.GetDateForStringSql(selectStr);
                }

                // MAX_UKETSUKE_NUMBERをセット
                returnVal = Convert.ToString(dt.Rows[0]["MIN_NUMBER"]);

                return returnVal;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetNextNumber", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetNextNumber", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
                return returnVal;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
        }
        #endregion

        #region ポップアップボタン名により、テキスト名を取得
        /// <summary>
        /// ポップアップボタン名により、テキスト名を取得
        /// </summary>
        /// <returns></returns>
        internal String GetTextName(String buttonName)
        {
            String textName = "";
            switch (buttonName)
            {
                case "TORIHIKISAKI_SEARCH_BUTTON":
                    textName = "TORIHIKISAKI_CD";
                    break;
                case "GYOUSHA_SEARCH_BUTTON":
                    textName = "GYOUSHA_CD";
                    break;
                case "GENBA_SEARCH_BUTTON":
                    textName = "GENBA_CD";
                    break;
                case "UNPAN_GYOUSHA_SEARCH_BUTTON":
                    textName = "UNPAN_GYOUSHA_CD";
                    break;
                case "NIZUMI_GYOUSHA_SEARCH_BUTTON":
                    textName = "NIZUMI_GYOUSHA_CD";
                    break;
                case "NIZUMI_GENBA_SEARCH_BUTTON":
                    textName = "NIZUMI_GENBA_CD";
                    break;
                case "NIOROSHI_GYOUSHA_SEARCH_BUTTON":
                    textName = "NIOROSHI_GYOUSHA_CD";
                    break;
                case "NIOROSHI_GENBA_SEARCH_BUTTON":
                    textName = "NIOROSHI_GENBA_CD";
                    break;
                default:
                    break;
            }
            return textName;
        }
        #endregion

        /// <summary>
        /// 明細行の重量項目取得（計量票などの出力用）
        /// </summary>
        /// <param name="JuryoOption">0：総重量、1：空車重量</param>
        /// <param name="In">1：受入、2：出荷</param>
        /// <returns>該当重量項目の値</returns>
        private string GetJuryoCol(int JuryoOption, int In)
        {
            LogUtility.DebugMethodStart(JuryoOption, In);

            string returnVal = string.Empty;
            switch (In)
            {
                case 1:
                    // 受入
                    switch (JuryoOption)
                    {
                        case 0:
                            // 総重量取得（明細行のうち最初の行）
                            foreach (var row in this.form.gcMultiRow1.Rows)
                                if (row.Cells[CELL_NAME_STAK_JYUURYOU].Value != null)
                                    if (!string.IsNullOrEmpty(row.Cells[CELL_NAME_STAK_JYUURYOU].Value.ToString()))
                                    {
                                        returnVal = (Convert.ToDecimal(row.Cells[CELL_NAME_STAK_JYUURYOU].Value)).ToString(TankaFormat);
                                        break;
                                    }
                            break;
                        case 1:
                            // 空車重量取得（明細行のうち最後の行）
                            foreach (var row in this.form.gcMultiRow1.Rows.Reverse())
                                if (row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value != null)
                                    if (!string.IsNullOrEmpty(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value.ToString()))
                                    {
                                        returnVal = (Convert.ToDecimal(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value)).ToString(TankaFormat);
                                        break;
                                    }
                            break;
                    }
                    break;

                case 2:
                    // 出荷
                    switch (JuryoOption)
                    {
                        case 0:
                            // 総重量取得（明細行のうち最後の行）
                            foreach (var row in this.form.gcMultiRow1.Rows.Reverse())
                                if (row.Cells[CELL_NAME_STAK_JYUURYOU].Value != null)
                                    if (!string.IsNullOrEmpty(row.Cells[CELL_NAME_STAK_JYUURYOU].Value.ToString()))
                                    {
                                        returnVal = (Convert.ToDecimal(row.Cells[CELL_NAME_STAK_JYUURYOU].Value)).ToString(TankaFormat);
                                        break;
                                    }
                            break;
                        case 1:
                            // 空車重量取得（明細行のうち最初の行）
                            foreach (var row in this.form.gcMultiRow1.Rows)
                                if (row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value != null)
                                    if (!string.IsNullOrEmpty(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value.ToString()))
                                    {
                                        returnVal = (Convert.ToDecimal(row.Cells[CELL_NAME_EMPTY_JYUURYOU].Value)).ToString(TankaFormat);
                                        break;
                                    }
                            break;
                    }
                    break;
            }

            LogUtility.DebugMethodEnd();
            return returnVal;
        }

        #region 必須チェックエラーフォーカス処理
        /// <summary>
        /// 必須チェックエラーフォーカス処理
        /// </summary>
        /// <returns></returns>
        internal bool SetErrorFocus()
        {
            bool ret = true;
            try
            {
                //必須チェックフォーカス処理
                bool errorFlg = false;
                if (this.form.RegistErrorFlag)
                {
                    var allControl = this.form.GetAllControl();
                    Control[] sortControl = allControl.OfType<Control>().OrderBy(s => s.TabIndex).ToArray();

                    var k = this.form.allControl.Where(c => c.Name == "KYOTEN_CD");

                    foreach (Control ctr in sortControl)
                    {
                        // TextBoxの場合
                        var textBox = ctr as ICustomTextBox;
                        if (textBox != null)
                        {
                            if (textBox.IsInputErrorOccured)
                            {
                                // エラー発生時はフォーカス移動を行う
                                ctr.Focus();
                                errorFlg = true;
                            }
                        }

                        // CustomDateTimePickerの場合
                        var CustomDateTimePicker = ctr as CustomDateTimePicker;

                        if (CustomDateTimePicker != null)
                        {
                            if (CustomDateTimePicker.IsInputErrorOccured)
                            {
                                // エラー発生時はフォーカス移動を行う
                                ctr.Focus();
                                errorFlg = true;
                            }
                        }

                        // GcCustomMultiRowの場合
                        var gcCustomMultiRow = ctr as GcCustomMultiRow;

                        // 明細内、必須チェック
                        if (gcCustomMultiRow != null && gcCustomMultiRow.Name.Equals(KeiryouConstans.CTR_DETAIL))
                        {
                            foreach (var row in gcCustomMultiRow.Rows)
                            {

                                if (row[KeiryouConstans.STACK_JYUURYOU].Style.BackColor == System.Drawing.Color.FromArgb(255, 100, 100))
                                {
                                    row[KeiryouConstans.STACK_JYUURYOU].Selected = true;
                                    errorFlg = true;

                                    break;
                                }
                                if (row[KeiryouConstans.EMPTY_JYUURYOU].Style.BackColor == System.Drawing.Color.FromArgb(255, 100, 100))
                                {
                                    row[KeiryouConstans.EMPTY_JYUURYOU].Selected = true;
                                    errorFlg = true;

                                    break;
                                }
                                if (this.ValueIsNullOrDBNullOrWhiteSpace(row[KeiryouConstans.HINMEI_CD].Value))
                                {
                                    row[KeiryouConstans.HINMEI_CD].Selected = true;
                                    errorFlg = true;

                                    break;
                                }
                            }
                        }

                        if (errorFlg == true)
                        {
                            // フォーカス移動が発生した時点で終了
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetErrorFocus", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }
        #endregion 必須チェックエラーフォーカス処理

        /// <summary>
        /// 値がNULLまたはDBNullまたは空白であるかどうかをチェックする。
        /// </summary>
        /// <param name="objValue">値</param>
        /// <returns>true:空白　false:空白でない</returns>
        private bool ValueIsNullOrDBNullOrWhiteSpace(object objValue)
        {
            return objValue == null || DBNull.Value.Equals(objValue) || string.IsNullOrWhiteSpace(objValue.ToString());
        }

        #endregion



        /// <summary>
        /// カレント行が割振行であるかないか判定
        /// </summary>
        /// <returns>True:カレント行が割振行, Flase:カレント行が割振行ではない</returns>
        internal bool judgeWarihuri()
        {
            Row selectedRow = (Row)this.form.gcMultiRow1.CurrentRow;
            if (selectedRow != null)
            {

                //新規行は""（空文字）、更新行はnullになっているので両方チェックする
                if (
                    (selectedRow["WARIFURI_PERCENT"].Value != null && selectedRow["WARIFURI_PERCENT"].Value != "") ||
                    (selectedRow["WARIFURI_JYUURYOU"].Value != null && selectedRow["WARIFURI_JYUURYOU"].Value != "")
                    )
                {
                    // 行挿入の性質が「選択行の上に行を挿入する」ため、warihuriRowNoが0のもについては行追加OKとする
                    if (selectedRow[CELL_NAME_WARIHURIROW_NO].Value != null
                        && 0 == Convert.ToInt16(selectedRow[CELL_NAME_WARIHURIROW_NO].Value.ToString()))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// データ移動処理
        /// </summary>
        internal bool SetMoveData()
        {

            if (this.form.moveData_flg)
            {
                this.form.TORIHIKISAKI_CD.Text = this.form.moveData_torihikisakiCd;
                if (CheckTorihikisaki())
                {
                    return false;
                }
                this.form.GYOUSHA_CD.Text = this.form.moveData_gyousyaCd;
                if (!CheckGyousha())
                {
                    return false;
                }
                this.form.GENBA_CD.Text = this.form.moveData_genbaCd;
                if (!CheckGenba())
                {
                    return false;
                }
                this.form.KIHON_KEIRYOU.Text = this.form.moveData_inOutKbn;
            }
            return true;
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
            bool ret = true;
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
                            return ret;
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
                                return ret;
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
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTopControlFocus", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
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
                control = (Control)controlUtil.FindControl(this.headerForm, controlName);
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
                    control = (Control)controlUtil.FindControl(this.form, controlName);
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
                return null;
            }

            // 詳細でタブストップが無い場合最初から検索
            foreach (var controlName in formControlNameList)
            {
                control = (Control)controlUtil.FindControl(this.headerForm, controlName);
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
                    control = (Control)controlUtil.FindControl(this.form, controlName);
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

            if (SqlInt16.Parse(this.form.KIHON_KEIRYOU.Text) == 2)
            {   // 出荷の場合総重量と空車重量を入れ替える
                if ( foward == true )
                {
                    string tmp = sformControlNameList[0];
                    sformControlNameList[0] = sformControlNameList[1];
                    sformControlNameList[1] = tmp;
                }
                else
                {
                    var num = sformControlNameList.Count-1;
                    string tmp = sformControlNameList[num];
                    sformControlNameList[num] = sformControlNameList[num-1];
                    sformControlNameList[num-1] = tmp;
                }
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
        public bool GetTabStop(string cname)
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

        /// 20141013 chinchisi 「計量入力画面」の休動Checkを追加する　start
        #region 車輌休動チェック
        internal bool SharyouDateCheck()
        {
            try
            {
                string inputUnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
                string inputSharyouCd = this.form.SHARYOU_CD.Text;
                string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Value);

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
                    this.form.SHARYOU_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E206", "車輌", "伝票日付：" + workclosedsharyouEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SharyouDateCheck", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SharyouDateCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }

            return true;
        }
        #endregion

        #region 運転者休動チェック
        internal bool UntenshaDateCheck()
        {
            try
            {
                string inputUntenshaCd = this.form.UNTENSHA_CD.Text;
                string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Value);

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
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("UntenshaDateCheck", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UntenshaDateCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }

            return true;
        }
        #endregion

        #region 搬入先休動チェック
        internal bool HannyuusakiDateCheck()
        {
            try
            {
                string inputNioroshiGyoushaCd = this.form.NIOROSHI_GYOUSHA_CD.Text;
                string inputNioroshiGenbaCd = this.form.NIOROSHI_GENBA_CD.Text;
                string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Value);

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
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("HannyuusakiDateCheck", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HannyuusakiDateCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }

            return true;
        }
        #endregion
        /// 20141013 chinchisi 「計量入力画面」の休動Checkを追加する　end
        
        /// 20141017 Houkakou 「計量入力画面」の休動Checkを追加する　start
        #region 受付番号チェック
        internal bool UketukeBangoCheck()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                string inputUnpanGyoushaCd = this.dto.entryEntity.UNPAN_GYOUSHA_CD;
                string inputSharyouCd = this.dto.entryEntity.SHARYOU_CD;
                string inputUntenshaCd = this.dto.entryEntity.UNTENSHA_CD;
                string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Text);
                string inputNioroshiGyoushaCd = this.dto.entryEntity.NIOROSHI_GYOUSHA_CD;
                string inputNioroshiGenbaCd = this.dto.entryEntity.NIOROSHI_GENBA_CD;

                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return true;
                }

                if (!string.IsNullOrEmpty(inputSharyouCd))
                {
                    // 車輌休動
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
                        msgLogic.MessageBoxShow("E208", "受付番号", ":" + this.form.UKETSUKE_NUMBER.Text, "車輌",
                                                        "伝票日付：" + workclosedsharyouEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                        this.form.SHARYOU_CD.Focus();
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(inputUntenshaCd))
                {
                    // 運転者休動
                    M_WORK_CLOSED_UNTENSHA workcloseduntenshaEntry = new M_WORK_CLOSED_UNTENSHA();
                    //運転者CD取得
                    workcloseduntenshaEntry.SHAIN_CD = inputUntenshaCd;
                    //作業日取得
                    workcloseduntenshaEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);
                    M_WORK_CLOSED_UNTENSHA[] workcloseduntenshaList = workcloseduntenshaDao.GetAllValidData(workcloseduntenshaEntry);
                    if (workcloseduntenshaList.Count() >= 1)
                    {
                        this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E208", "受付番号", ":" + this.form.UKETSUKE_NUMBER.Text, "運転者",
                                                        "伝票日付：" + workcloseduntenshaEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                        this.form.UNTENSHA_CD.Focus();
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(inputNioroshiGenbaCd))
                {
                    // 搬入先休動
                    M_WORK_CLOSED_HANNYUUSAKI workclosedhannyuusakiEntry = new M_WORK_CLOSED_HANNYUUSAKI();
                    //荷降業者CD取得
                    workclosedhannyuusakiEntry.GYOUSHA_CD = inputNioroshiGyoushaCd;
                    //荷降現場CD取得
                    workclosedhannyuusakiEntry.GENBA_CD = inputNioroshiGenbaCd;
                    //作業日取得
                    workclosedhannyuusakiEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);
                    M_WORK_CLOSED_HANNYUUSAKI[] workclosedhannyuusakiList = workclosedhannyuusakiDao.GetAllValidData(workclosedhannyuusakiEntry);

                    if (workclosedhannyuusakiList.Count() >= 1)
                    {
                        this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E208", "受付番号", ":" + this.form.UKETSUKE_NUMBER.Text, "荷降現場",
                                                        "伝票日付：" + workclosedhannyuusakiEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                        this.form.NIOROSHI_GENBA_CD.Focus();
                        return false;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("UketukeBangoCheck", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UketukeBangoCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }

            return true;
        }
        #endregion
        /// 20141017 Houkakou 「計量入力画面」の休動Checkを追加する　end

        // 20151021 katen #13337 品名手入力に関する機能修正 start
        internal bool torihikisaki_Pop()
        {
            bool ret = true;
            try
            {
                var torihikisakiEntity = this.accessor.GetTorihikisaki(this.form.TORIHIKISAKI_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (torihikisakiEntity != null)
                {
                    if (torihikisakiEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME1;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("torihikisaki_Pop", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("torihikisaki_Pop", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        internal bool gyousha_Pop()
        {
            bool ret = true;
            try
            {
                var gyoushaEntity = this.accessor.GetGyousha(this.form.GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (gyoushaEntity != null)
                {
                    if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("gyousha_Pop", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("gyousha_Pop", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        internal bool genba_Pop()
        {
            bool ret = true;
            try
            {
                var gyoushaEntity = this.accessor.GetGyousha(this.form.GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (gyoushaEntity != null)
                {
                    if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                    }
                }
                var genbaEntity = this.accessor.GetGenba(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (genbaEntity != null)
                {
                    if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.GENBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME1;
                    }
                    else
                    {
                        this.form.GENBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("genba_Pop", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("genba_Pop", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        internal bool upnGyousha_Pop()
        {
            bool ret = true;
            try
            {
                var gyoushaEntity = this.accessor.GetGyousha(this.form.UNPAN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (gyoushaEntity != null)
                {
                    if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.UNPAN_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.form.UNPAN_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("upnGyousha_Pop", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("upnGyousha_Pop", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        internal bool nioroshiGyousha_Pop()
        {
            bool ret = true;
            try
            {
                var gyoushaEntity = this.accessor.GetGyousha(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (gyoushaEntity != null)
                {
                    if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("nioroshiGyousha_Pop", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("nioroshiGyousha_Pop", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        internal bool nioroshiGenba_Pop()
        {
            bool ret = true;
            try
            {
                var gyoushaEntity = this.accessor.GetGyousha(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (gyoushaEntity != null)
                {
                    if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                    }
                }
                var genbaEntity = this.accessor.GetGenba(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.NIOROSHI_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (genbaEntity != null)
                {
                    if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.NIOROSHI_GENBA_NAME.Text = genbaEntity.GENBA_NAME1;
                    }
                    else
                    {
                        this.form.NIOROSHI_GENBA_NAME.Text = genbaEntity.GENBA_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("nioroshiGenba_Pop", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("nioroshiGenba_Pop", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        internal bool nizumiGyousha_Pop()
        {
            bool ret = true;
            try
            {
                var gyoushaEntity = this.accessor.GetGyousha(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (gyoushaEntity != null)
                {
                    if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.NIZUMI_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.form.NIZUMI_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("nizumiGyousha_Pop", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("nizumiGyousha_Pop", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        internal bool nizumiGenba_Pop()
        {
            bool ret = true;
            try
            {
                var gyoushaEntity = this.accessor.GetGyousha(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (gyoushaEntity != null)
                {
                    if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.NIZUMI_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.form.NIZUMI_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                    }
                }
                var genbaEntity = this.accessor.GetGenba(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.NIZUMI_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, this.footerForm.sysDate.Date);
                if (genbaEntity != null)
                {
                    if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.NIZUMI_GENBA_NAME.Text = genbaEntity.GENBA_NAME1;
                    }
                    else
                    {
                        this.form.NIZUMI_GENBA_NAME.Text = genbaEntity.GENBA_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("nizumiGenba_Pop", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("nizumiGenba_Pop", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }
        // 20151021 katen #13337 品名手入力に関する機能修正 end
    }
}
