using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.BusinessManagement.Const.Common;
using Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DTO;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Xml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using GrapeCity.Win.MultiRow;

namespace Shougun.Core.BusinessManagement.MitsumoriNyuryoku
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// Form
        /// </summary>
        private MitsumoriNyuryokuForm form;

        /// <summary>
        /// フッター
        /// </summary>
        private BusinessBaseForm footerForm;

        /// <summary>
        /// ヘッダー
        /// </summary>
        internal HeaderForm headerForm;

        /// <summary>
        /// DBアクセッサー
        /// </summary>
        internal Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Accessor.DBAccessor accessor;

        /// <summary>
        /// DTO
        /// </summary>
        internal DTOClass dto;

        /// <summary>
        /// IM_SYS_INFODao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        //20250411
        private IM_BIKO_PATAN_NYURYOKUDao bikoPatanDao;

        //20250415
        private IM_BIKO_SENTAKUSHI_NYURYOKUDao bikoSentaDao;

        //20250414
        private IM_BIKO_UCHIWAKE_NYURYOKUDao bikoUchiwakeDao;

        /// <summary>
        /// IM_SYS_INFO
        /// </summary>
        private M_SYS_INFO mSysInfo = new M_SYS_INFO();

        //20250411
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// 画面表示時点DTO
        /// </summary>
        private DTOClass beforDto;

        /// <summary>
        /// ControlUtility
        /// </summary>
        internal ControlUtility controlUtil;

        /// <summary>
        /// コピーDTO
        /// </summary>
        internal T_MITSUMORI_DETAIL copyDetail;

        /// <summary>
        /// BusinessCommonのDBAccesser
        /// </summary>
        private Shougun.Core.Common.BusinessCommon.DBAccessor commonAccesser;

        /// <summary>
        /// CDや名称を再取得するか判断します
        /// </summary>
        internal bool isReacquisition = true;

        //20250414
        private string beforeBikoCd = string.Empty;

        private string beforeBikoName = string.Empty;

        /// <summary>
        /// UIFormの入力コントロール名一覧
        /// </summary>
        //private string[] inputUiFormControlNames = { "txt_Bikou5", "txt_Bikou2", "txt_Bikou1", "txt_Bikou4", "txt_Bikou3", "txt_MitsumoriNaiyo4", "txt_MitsumoriNaiyo2", "txt_MitsumoriNaiyo3", "txt_MitsumoriNaiyo1", "txt_Kenmei", "btn_GenbaSearch", "chk_GenbaInji", "txtGenbaName", "txt_GenbaCD", "btn_GyousyaSearch", "txt_GyoushaName", "txt_GyoushaCD", "chk_GyushaInji", "txt_TorihikisakiMei", "btn_TorihikisakieSearch", "txt_TorihikisakiCD", "chk_TorihikisakiInji", "txt_InjiKyoten2CD", "txt_InjiKyoten1CD", "btn_Tsugi", "btn_Mae", "txt_MitsumoriNumber", "txt_ShainNameRyaku", "txt_SeikyuuZeiKbnCD", "txt_ShainCD", "dtp_MitsumoriDate", "chk_HikiaiGenbaFlg", "chk_HikiaiGyoushaFlg", "chk_HikiaiTorihikisakiFlg", "txt_ShiharaiZeiKbnCD", "txt_ShouhizeiRate", "groupBox3", "rdo_Hikazei", "rdo_Uchizei", "rdo_Sotozei", "txt_ZeiKbnCD", "groupBox2", "rdo_Meisai", "rdo_Denpyo", "txt_ZeiKeisanKbnCD", "txt_SystemID", "txt_SeqNo", "tbc_MitsumoriMeisai", "lb_TorihikisakiKeishou", "lb_GyoushaKeishou", "lb_GenbaKeishou", "groupBox4", "rdo_InjiNashi", "rdo_InjiAri", "txt_MitsumoriInjiDate", "groupBox1", "rdo_sichu", "rdo_Jyuchu", "rdo_Shinkou", "txt_JykyoFlg", "tab_Gyousha", "tap_Torihikisaki", "txt_TorihikisakiTel", "txt_TorihikisakiTantousya", "txt_TorihikisakiAddress2", "txt_TorihikisakiAddress1", "btn_TorihikisakiPostSearch", "btn_TorihikisakiAddressSearch", "txt_TorihikisakiPost", "tap_Gyousya", "txt_GyousyaKeitaiTel", "txt_GyousyaTel", "txt_GyousyaTantousya", "txt_GyousyaAddress2", "txt_GyousyaAddress1", "btn_GyousyaPostSearch", "btn_GyousyaAddressSearch", "txt_GyousyaPost", "tap_Genba", "txt_GenbaKeitaiTel", "txt_GenbaTel", "txt_GenbaTantousya", "txt_GenbaAddress2", "txt_GenbaAddress1", "btn_GenbaPostSearch", "btn_GenbaAddressSearch", "txt_GenbaPost", "tapBiko", "txt_ShanaiBikou", "txt_Torihikisaki_shokuchi_kbn", "txt_Gyousha_shokuchi_kbn", "txt_Genba_shokuchi_kbn", "dtp_ShinkouDate", "dtp_SichuDate", "dtp_JuchuDate", "label11", "txt_BushouNameInji", "rdo_BS_InjiAri", "rdo_BS_InjiNashi" };
        //20250417
        private string[] inputUiFormControlNames = { "txt_Bikou5", "txt_Bikou2", "txt_Bikou1", "txt_Bikou4", "txt_Bikou3", "txt_MitsumoriNaiyo5", "txt_MitsumoriNaiyo4", "txt_MitsumoriNaiyo2", "txt_MitsumoriNaiyo3", "txt_MitsumoriNaiyo1", "txt_Kenmei", "txt_Kenmei2", "btn_GenbaSearch", "chk_GenbaInji", "txtGenbaName", "txt_GenbaCD", "btn_GyousyaSearch", "txt_GyoushaName", "txt_GyoushaCD", "chk_GyushaInji", "txt_TorihikisakiMei", "btn_TorihikisakieSearch", "txt_TorihikisakiCD", "chk_TorihikisakiInji", "txt_InjiKyoten2CD", "txt_InjiKyoten1CD", "btn_Tsugi", "btn_Mae", "txt_MitsumoriNumber", "txt_ShainNameRyaku", "txt_SeikyuuZeiKbnCD", "txt_ShainCD", "dtp_MitsumoriDate", "chk_HikiaiGenbaFlg", "chk_HikiaiGyoushaFlg", "chk_HikiaiTorihikisakiFlg", "txt_ShiharaiZeiKbnCD", "txt_ShouhizeiRate", "groupBox3", "rdo_Hikazei", "rdo_Uchizei", "rdo_Sotozei", "txt_ZeiKbnCD", "groupBox2", "rdo_Meisai", "rdo_Denpyo", "txt_ZeiKeisanKbnCD", "txt_SystemID", "txt_SeqNo", "tbc_MitsumoriMeisai", "lb_TorihikisakiKeishou", "lb_GyoushaKeishou", "lb_GenbaKeishou", "groupBox4", "rdo_InjiNashi", "rdo_InjiAri", "txt_MitsumoriInjiDate", "groupBox1", "chk_sichu", "chk_Jyuchu", "rdo_Shinkou", "txt_JykyoFlg", "tab_Gyousha", "tap_Torihikisaki", "txt_TorihikisakiTel", "txt_TorihikisakiTantousya", "txt_TorihikisakiAddress2", "txt_TorihikisakiAddress1", "btn_TorihikisakiPostSearch", "btn_TorihikisakiAddressSearch", "txt_TorihikisakiPost", "tap_Gyousya", "txt_GyousyaKeitaiTel", "txt_GyousyaTel", "txt_GyousyaTantousya", "txt_GyousyaAddress2", "txt_GyousyaAddress1", "btn_GyousyaPostSearch", "btn_GyousyaAddressSearch", "txt_GyousyaPost", "tap_Genba", "txt_GenbaKeitaiTel", "txt_GenbaTel", "txt_GenbaTantousya", "txt_GenbaAddress2", "txt_GenbaAddress1", "btn_GenbaPostSearch", "btn_GenbaAddressSearch", "txt_GenbaPost", "tapBiko", "txt_ShanaiBikou", "txt_Torihikisaki_shokuchi_kbn", "txt_Gyousha_shokuchi_kbn", "txt_Genba_shokuchi_kbn", "dtp_ShinkouDate", "dtp_SichuDate", "dtp_JuchuDate", "label11", "txt_BushouNameInji", "rdo_BS_InjiAri", "rdo_BS_InjiNashi" };

        /// <summary>
        /// [参照モード用] UIFormの入力コントロール名一覧
        /// </summary>
        //private string[] inputUiFormControlNames_Reference = { "txt_Bikou5", "txt_Bikou2", "txt_Bikou1", "txt_Bikou4", "txt_Bikou3", "txt_MitsumoriNaiyo4", "txt_MitsumoriNaiyo2", "txt_MitsumoriNaiyo3", "txt_MitsumoriNaiyo1", "txt_Kenmei", "btn_GenbaSearch", "chk_GenbaInji", "txtGenbaName", "txt_GenbaCD", "btn_GyousyaSearch", "txt_GyoushaName", "txt_GyoushaCD", "chk_GyushaInji", "txt_TorihikisakiMei", "btn_TorihikisakieSearch", "txt_TorihikisakiCD", "chk_TorihikisakiInji", "txt_InjiKyoten2CD", "txt_InjiKyoten1CD", "txt_MitsumoriNumber", "txt_ShainNameRyaku", "txt_SeikyuuZeiKbnCD", "txt_ShainCD", "dtp_MitsumoriDate", "chk_HikiaiGenbaFlg", "chk_HikiaiGyoushaFlg", "chk_HikiaiTorihikisakiFlg", "txt_ShiharaiZeiKbnCD", "txt_ShouhizeiRate", "rdo_Hikazei", "rdo_Uchizei", "rdo_Sotozei", "txt_ZeiKbnCD", "rdo_Meisai", "rdo_Denpyo", "txt_ZeiKeisanKbnCD", "txt_SystemID", "txt_SeqNo", "tbc_MitsumoriMeisai", "lb_TorihikisakiKeishou", "lb_GyoushaKeishou", "lb_GenbaKeishou", "rdo_InjiNashi", "rdo_InjiAri", "txt_MitsumoriInjiDate", "rdo_sichu", "rdo_Jyuchu", "rdo_Shinkou", "txt_JykyoFlg", "txt_TorihikisakiTel", "txt_TorihikisakiTantousya", "txt_TorihikisakiAddress2", "txt_TorihikisakiAddress1", "btn_TorihikisakiPostSearch", "btn_TorihikisakiAddressSearch", "txt_TorihikisakiPost", "txt_GyousyaKeitaiTel", "txt_GyousyaTel", "txt_GyousyaTantousya", "txt_GyousyaAddress2", "txt_GyousyaAddress1", "btn_GyousyaPostSearch", "btn_GyousyaAddressSearch", "txt_GyousyaPost", "txt_GenbaKeitaiTel", "txt_GenbaTel", "txt_GenbaTantousya", "txt_GenbaAddress2", "txt_GenbaAddress1", "btn_GenbaPostSearch", "btn_GenbaAddressSearch", "txt_GenbaPost", "txt_ShanaiBikou", "txt_Torihikisaki_shokuchi_kbn", "txt_Gyousha_shokuchi_kbn", "txt_Genba_shokuchi_kbn", "dtp_ShinkouDate", "dtp_SichuDate", "dtp_JuchuDate", "txt_BushouNameInji", "rdo_BS_InjiAri", "rdo_BS_InjiNashi" };
        private string[] inputUiFormControlNames_Reference = { "txt_Bikou5", "txt_Bikou2", "txt_Bikou1", "txt_Bikou4", "txt_Bikou3", "txt_MitsumoriNaiyo5", "txt_MitsumoriNaiyo4", "txt_MitsumoriNaiyo2", "txt_MitsumoriNaiyo3", "txt_MitsumoriNaiyo1", "txt_Kenmei", "txt_Kenmei2", "btn_GenbaSearch", "chk_GenbaInji", "txtGenbaName", "txt_GenbaCD", "btn_GyousyaSearch", "txt_GyoushaName", "txt_GyoushaCD", "chk_GyushaInji", "txt_TorihikisakiMei", "btn_TorihikisakieSearch", "txt_TorihikisakiCD", "chk_TorihikisakiInji", "txt_InjiKyoten2CD", "txt_InjiKyoten1CD", "txt_MitsumoriNumber", "txt_ShainNameRyaku", "txt_SeikyuuZeiKbnCD", "txt_ShainCD", "dtp_MitsumoriDate", "chk_HikiaiGenbaFlg", "chk_HikiaiGyoushaFlg", "chk_HikiaiTorihikisakiFlg", "txt_ShiharaiZeiKbnCD", "txt_ShouhizeiRate", "rdo_Hikazei", "rdo_Uchizei", "rdo_Sotozei", "txt_ZeiKbnCD", "rdo_Meisai", "rdo_Denpyo", "txt_ZeiKeisanKbnCD", "txt_SystemID", "txt_SeqNo", "tbc_MitsumoriMeisai", "lb_TorihikisakiKeishou", "lb_GyoushaKeishou", "lb_GenbaKeishou", "rdo_InjiNashi", "rdo_InjiAri", "txt_MitsumoriInjiDate", "chk_sichu", "chk_Jyuchu", "rdo_Shinkou", "txt_JykyoFlg", "txt_TorihikisakiTel", "txt_TorihikisakiTantousya", "txt_TorihikisakiAddress2", "txt_TorihikisakiAddress1", "btn_TorihikisakiPostSearch", "btn_TorihikisakiAddressSearch", "txt_TorihikisakiPost", "txt_GyousyaKeitaiTel", "txt_GyousyaTel", "txt_GyousyaTantousya", "txt_GyousyaAddress2", "txt_GyousyaAddress1", "btn_GyousyaPostSearch", "btn_GyousyaAddressSearch", "txt_GyousyaPost", "txt_GenbaKeitaiTel", "txt_GenbaTel", "txt_GenbaTantousya", "txt_GenbaAddress2", "txt_GenbaAddress1", "btn_GenbaPostSearch", "btn_GenbaAddressSearch", "txt_GenbaPost", "txt_ShanaiBikou", "txt_Torihikisaki_shokuchi_kbn", "txt_Gyousha_shokuchi_kbn", "txt_Genba_shokuchi_kbn", "dtp_ShinkouDate", "dtp_SichuDate", "dtp_JuchuDate", "txt_BushouNameInji", "rdo_BS_InjiAri", "rdo_BS_InjiNashi" };

        /// <summary>
        /// HeaderFormの入力コントロール名一覧
        /// </summary>
        private string[] inputHeaderControlNames = { "KYOTEN_CD" };

        // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
        /// <summary>
        /// ヘッダ部
        /// </summary>
        // 20250417
        private string[] headerReportItemName = { "MITSUMORI_NUMBER", "MITSUMORI_DATE", "TORIHIKISAKI_NAME1", "TORIHIKISAKI_NAME2", "GYOUSHA_NAME1", "GYOUSHA_NAME2", "GENBA_NAME1", "GENBA_NAME2", "KENMEI", "KENMEI_2", "MITSUMORI_KOUMOKU1", "MITSUMORI_1", "MITSUMORI_KOUMOKU2", "MITSUMORI_2", "MITSUMORI_KOUMOKU3", "MITSUMORI_3", "MITSUMORI_KOUMOKU4", "MITSUMORI_4", "MITSUMORI_KOUMOKU5", "MITSUMORI_5", "CORP_NAME", "CORP_DAIHYOU", "KYOTEN_NAME_1", "KYOTEN_POST_1", "KYOTEN_ADDRESS1_1", "KYOTEN_ADDRESS2_1", "KYOTEN_TEL_1", "KYOTEN_FAXL_1", "KYOTEN_NAME_2", "KYOTEN_POST_2", "KYOTEN_ADDRESS1_2", "KYOTEN_ADDRESS2_2", "KYOTEN_TEL_2", "KYOTEN_FAXL_2", "BUSHO_NAME_LABEL", "BUSHO_NAME", "EIGYO_TANTOUSHA_NAME", "GOUKEI_KINGAKU", "ZEI_KBN_CD" };

        /// <summary>
        /// 明細部
        /// </summary>
        private string[] detailReportItemName = { "DENPYOU_NUMBER", "HINMEI_NAME", "SUURYOU", "UNIT_NAME", "TANKA", "HINMEI_KINGAKU", "HINMEI_ZEI_KBN_CD", "HINMEI_TAX_SOTO", "MEISAI_BIKOU", "DENPYOU_KBN_CD", "DENPYOU_KBN" };

        /// <summary>
        /// フッだ部
        /// </summary>
        private string[] footerReportItemName = { "KINGAKU_TOTAL", "TAX_SOTO", "GOUKEI_KINGAKU_TOTAL", "BIKOU_1", "BIKOU_2", "BIKOU_3", "BIKOU_4", "BIKOU_5" };

        // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end

        /// <summary>
        /// 伝票区分全件
        /// </summary>
        private Dictionary<short, M_DENPYOU_KBN> denpyouKbnDictionary = new Dictionary<short, M_DENPYOU_KBN>();

        /// <summary>
        /// 単位区分全件
        /// </summary>
        private Dictionary<short, M_UNIT> unitDictionary = new Dictionary<short, M_UNIT>();

        /// <summary>
        /// 税区分CD
        /// </summary>
        public ZEI_KBN_CD ZEI_KBN { get; set; }

        /// <summary>
        /// 税区分CD
        /// </summary>
        public enum ZEI_KBN_CD
        {
            // 外税
            SOTOZEI = 1,

            // 内税
            UCHIZEI = 2,

            // 非課税
            HIGAZEI = 3
        }

        /// <summary>
        /// 消費税率
        /// </summary>
        internal decimal taxRate;

        //
        // 概要:
        //     画面のタイプ
        public TORIHIKISAKI TORIHIKISAKI_WindowType { get; set; }

        //
        // 概要:
        //     画面のタイプ
        public HIKIAI_TORIHIKISAKI HIKIAI_TORIHIKISAKI_WindowType { get; set; }

        //
        // 概要:
        //     画面のタイプ
        public HIKIAI_GYOUSHA HIKIAI_GYOUSHA_WindowType { get; set; }

        //
        // 概要:
        //     画面のタイプ
        public HIKIAI_GENBA HIKIAI_GENBA_WindowType { get; set; }

        // 概要:
        //     取引先登録区分
        public enum TORIHIKISAKI
        {
            NONE = 0,

            //
            // 概要:
            //     新規
            NEW_WINDOW_FLAG = 1,

            //
            // 概要:
            //     修正
            UPDATE_WINDOW_FLAG = 4,
        }

        // 概要:
        //     引合取引先登録区分
        public enum HIKIAI_TORIHIKISAKI
        {
            NONE = 0,

            //
            // 概要:
            //     新規
            NEW_WINDOW_FLAG = 1,

            //
            // 概要:
            //     参照
            REFERENCE_WINDOW_FLAG = 2,

            //
            // 概要:
            //     修正
            UPDATE_WINDOW_FLAG = 4,
        }

        // 概要:
        //     引合業者登録区分
        public enum HIKIAI_GYOUSHA
        {
            NONE = 0,

            //
            // 概要:
            //     新規
            NEW_WINDOW_FLAG = 1,

            //
            // 概要:
            //     参照
            REFERENCE_WINDOW_FLAG = 2,

            //
            // 概要:
            //     修正
            UPDATE_WINDOW_FLAG = 4,
        }

        // 概要:
        //     引合現場登録区分
        public enum HIKIAI_GENBA
        {
            NONE = 0,

            //
            // 概要:
            //     新規
            NEW_WINDOW_FLAG = 1,

            //
            // 概要:
            //     参照
            REFERENCE_WINDOW_FLAG = 2,

            //
            // 概要:
            //     修正
            UPDATE_WINDOW_FLAG = 4,
        }

        /// <summary>
        /// 取引先が諸口の時、名称が変更されたかを示します
        /// </summary>
        internal bool torihikisakiNameChangedFlg = false;

        /// <summary>
        /// 業者が諸口の時、名称が変更されたかを示します
        /// </summary>
        internal bool gyoushaNameChangedFlg = false;

        /// <summary>
        /// 現場が諸口の時、名称が変更されたかを示します
        /// </summary>
        internal bool genbaNameChangedFlg = false;

        /// <summary>
        /// 取引先名称の前回値を格納します
        /// </summary>
        internal string beforeTorihikisakiName;

        /// <summary>
        /// 業者名称の前回値を格納します
        /// </summary>
        internal string beforeGyoushaName;

        /// <summary>
        /// 現場名称の前回値を格納します
        /// </summary>
        internal string beforeGenbaName;

        /// <summary>
        /// フォーカス遷移するか判断するフラグ
        /// </summary>
        internal bool isTransitionFocus = true;

        /// <summary>
        /// 取引先と拠点が関連チェックがエラーか判断するフラグ
        /// </summary>
        internal bool torihikisakiAndKyotenErrorFlg = false;

        /// <summary>
        /// 変更前の品名CDを格納します
        /// </summary>
        internal string befHinmeiCd { get; set; }

        /// <summary>
        /// 現場対したの取引先CD
        /// </summary>
        internal string genbaTorihiksiakiCd { get; set; }

        #endregion フィールド

        /// <summary>
        /// 業者のエラーか現場のエラーか判断する区分
        /// ０：正常
        /// １：業者エラー
        /// ２：現場エラー
        /// </summary>
        internal int gyousyaGenbaErrorKbn = 0;

        #region 初期化処理

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(MitsumoriNyuryokuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            // フォーム
            this.form = targetForm;

            // dto
            this.dto = new DTOClass();

            this.beforDto = new DTOClass();

            // Accessor
            this.accessor = new Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Accessor.DBAccessor();

            // commonAccesser
            this.commonAccesser = new Shougun.Core.Common.BusinessCommon.DBAccessor();

            // IM_SYS_INFODao
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            //20250411
            this.bikoPatanDao = DaoInitUtility.GetComponent<IM_BIKO_PATAN_NYURYOKUDao>();

            //20250415
            this.bikoSentaDao = DaoInitUtility.GetComponent<IM_BIKO_SENTAKUSHI_NYURYOKUDao>();

            //20250414
            this.bikoUchiwakeDao = DaoInitUtility.GetComponent<IM_BIKO_UCHIWAKE_NYURYOKUDao>();

            // Utility
            this.controlUtil = new ControlUtility();

            LogUtility.DebugMethodEnd();
        }

        #endregion コンストラクタ

        #region 必須チェックの設定を初期化します

        /// <summary>
        /// 必須チェックの設定を初期化します
        /// </summary>
        internal bool RequiredSettingInit()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
                // Entry
                this.headerForm.KYOTEN_CD.RegistCheckMethod = null;
                this.form.dtp_MitsumoriDate.RegistCheckMethod = null;
                this.form.txt_TorihikisakiCD.RegistCheckMethod = null;
                this.form.txt_ZeiKeisanKbnCD.RegistCheckMethod = null;
                this.form.txt_ZeiKbnCD.RegistCheckMethod = null;
                this.form.txt_MitsumoriInjiDate.RegistCheckMethod = null;
                // 201400708 syunrei ＃947　№13　　start
                this.form.txt_BushouNameInji.RegistCheckMethod = null;
                // 201400708 syunrei ＃947　№13　　end
                this.form.txt_JykyoFlg.RegistCheckMethod = null;

                this.form.dtp_ShinkouDate.RegistCheckMethod = null;
                this.form.dtp_JuchuDate.RegistCheckMethod = null;
                this.form.dtp_SichuDate.RegistCheckMethod = null;

                // Detail

                // ページ分データ作成
                for (int page = 0; page < this.form.tbc_MitsumoriMeisai.TabPages.Count; page++)
                {
                    // CustomDataGridViewを取得する
                    CustomDataGridView cdgv = ((CustomDataGridView)this.form.tbc_MitsumoriMeisai.TabPages[page].Controls["control"].Controls["CustomDataGridView"]);

                    foreach (DataGridViewRow o in cdgv.Rows)
                    {
                        // 品名
                        var hinmeiName = (DgvCustomTextBoxCell)o.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME];

                        hinmeiName.RegistCheckMethod = null;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RequiredSettingInit", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion 必須チェックの設定を初期化します

        #region 必須チェックの背景色を戻す

        /// <summary>
        /// 必須チェックの背景色を戻す
        /// </summary>
        internal void RequiredSettingBackColorClear()
        {
            LogUtility.DebugMethodStart();

            // Entry
            this.headerForm.KYOTEN_CD.IsInputErrorOccured = false;
            this.form.txt_ZeiKeisanKbnCD.IsInputErrorOccured = false;
            this.form.txt_ZeiKbnCD.IsInputErrorOccured = false;
            this.form.txt_MitsumoriInjiDate.IsInputErrorOccured = false;
            // 201400708 syunrei ＃947　№13　　start
            this.form.txt_BushouNameInji.IsInputErrorOccured = false;
            // 201400708 syunrei ＃947　№13　　end

            this.form.txt_JykyoFlg.IsInputErrorOccured = false;
            this.form.txt_GyoushaCD.IsInputErrorOccured = false;

            // Detail
            // ページ分データ作成
            for (int page = 0; page < this.form.tbc_MitsumoriMeisai.TabPages.Count; page++)
            {
                // CustomDataGridViewを取得する
                CustomDataGridView cdgv = ((CustomDataGridView)this.form.tbc_MitsumoriMeisai.TabPages[page].Controls["control"].Controls["CustomDataGridView"]);

                foreach (DataGridViewRow o in cdgv.Rows)
                {
                    // 品名
                    var hinmeiName = (DgvCustomTextBoxCell)o.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME];

                    hinmeiName.IsInputErrorOccured = false;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion 必須チェックの背景色を戻す

        #region 必須チェックの設定を動的に生成

        /// <summary>
        /// 必須チェックの設定を動的に生成
        /// </summary>
        /// <param name="tairyuuKbn">滞留登録かどうか</param>
        internal bool SetRequiredSetting()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
                // 初期化
                if (!this.RequiredSettingInit())
                {
                    ret = false;
                    return ret;
                }

                // 設定
                SelectCheckDto existCheck = new SelectCheckDto();
                existCheck.CheckMethodName = "必須チェック";
                Collection<SelectCheckDto> excitChecks = new Collection<SelectCheckDto>();
                excitChecks.Add(existCheck);

                // 登録
                this.headerForm.KYOTEN_CD.RegistCheckMethod = excitChecks;
                this.form.dtp_MitsumoriDate.RegistCheckMethod = excitChecks;
                this.form.txt_TorihikisakiCD.RegistCheckMethod = excitChecks;
                this.form.txt_ZeiKeisanKbnCD.RegistCheckMethod = excitChecks;
                this.form.txt_ZeiKbnCD.RegistCheckMethod = excitChecks;
                this.form.txt_MitsumoriInjiDate.RegistCheckMethod = excitChecks;

                if (this.form.chk_Jyuchu.Checked)
                {
                    this.form.dtp_JuchuDate.RegistCheckMethod = excitChecks;
                }
                if (this.form.chk_sichu.Checked)
                {
                    this.form.dtp_SichuDate.RegistCheckMethod = excitChecks;
                }

                // ページ分データ作成
                for (int page = 0; page < this.form.tbc_MitsumoriMeisai.TabPages.Count; page++)
                {
                    // CustomDataGridViewを取得する
                    CustomDataGridView cdgv = ((CustomDataGridView)this.form.tbc_MitsumoriMeisai.TabPages[page].Controls["control"].Controls["CustomDataGridView"]);

                    foreach (DataGridViewRow o in cdgv.Rows)
                    {
                        // 品名
                        var hinmeiName = (DgvCustomTextBoxCell)o.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME];
                        // 伝票区分
                        var denpyouKbnName = (DgvCustomTextBoxCell)o.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME];
                    }
                }

                #region 明細必須チェック

                string errorStr = string.Empty;
                MessageUtility MessageUtil = new MessageUtility();
                bool hinmeiCdFlg = false;
                bool hinmeiNameFlg = false;
                bool denpyouFlg = false;
                bool unitFlg = false;
                bool hasError = false;

                // ページ分データ作成 明細必須チェック
                for (int page = 0; page < this.form.tbc_MitsumoriMeisai.TabPages.Count; page++)
                {
                    // CustomDataGridViewを取得する
                    CustomDataGridView cdgv = ((CustomDataGridView)this.form.tbc_MitsumoriMeisai.TabPages[page].Controls["control"].Controls["CustomDataGridView"]);

                    foreach (DataGridViewRow row in cdgv.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value == null
                                || row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value == "")
                            {
                                hinmeiCdFlg = true;
                                (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD] as DgvCustomTextBoxCell).IsInputErrorOccured = true;
                                if (!hasError)
                                {
                                    cdgv.Focus();
                                    row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Selected = true;
                                    hasError = true;
                                }
                            }
                            else
                            {
                                (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD] as DgvCustomTextBoxCell).IsInputErrorOccured = false;
                            }

                            if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME].Value == null
                                || row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME].Value == "")
                            {
                                hinmeiNameFlg = true;
                                (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME] as DgvCustomTextBoxCell).IsInputErrorOccured = true;
                                if (!hasError)
                                {
                                    cdgv.Focus();
                                    row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME].Selected = true;
                                    hasError = true;
                                }
                            }
                            else
                            {
                                (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME] as DgvCustomTextBoxCell).IsInputErrorOccured = false;
                            }

                            if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME].Value == null
                                || row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME].Value == "")
                            {
                                denpyouFlg = true;
                                (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME] as DgvCustomTextBoxCell).IsInputErrorOccured = true;
                                if (!hasError)
                                {
                                    cdgv.Focus();
                                    row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME].Selected = true;
                                    hasError = true;
                                }
                            }
                            else
                            {
                                (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME] as DgvCustomTextBoxCell).IsInputErrorOccured = false;
                            }
                            // 数量が入力されている場合
                            if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SUURYOU].Value != null
                                && row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SUURYOU].Value != "")
                            {
                                if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD].Value == null
                                    || row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD].Value == "")
                                {
                                    unitFlg = true;
                                    (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD] as DgvCustomNumericTextBox2Cell).IsInputErrorOccured = true;
                                    if (!hasError)
                                    {
                                        cdgv.Focus();
                                        row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD].Selected = true;
                                        hasError = true;
                                    }
                                }
                                else
                                {
                                    (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD] as DgvCustomNumericTextBox2Cell).IsInputErrorOccured = false;
                                }
                            }
                        }
                    }
                }

                if (hinmeiCdFlg)
                {
                    errorStr += String.Format(MessageUtil.GetMessage("E001").MESSAGE, "品名CD") + Environment.NewLine;
                }
                if (hinmeiNameFlg)
                {
                    errorStr += String.Format(MessageUtil.GetMessage("E001").MESSAGE, "品名") + Environment.NewLine;
                }
                if (denpyouFlg)
                {
                    errorStr += String.Format(MessageUtil.GetMessage("E001").MESSAGE, "伝票区分") + Environment.NewLine;
                }
                if (unitFlg)
                {
                    errorStr += String.Format(MessageUtil.GetMessage("E001").MESSAGE, "単位") + Environment.NewLine;
                }

                if (!String.IsNullOrEmpty(errorStr))
                {
                    new MessageBoxShowLogic().MessageBoxShowError(errorStr);
                    return false;
                }

                #endregion 明細必須チェック
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetRequiredSetting", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion 必須チェックの設定を動的に生成

        #region 明細必須チェックの設定を動的に生成

        /// <summary>
        /// 必須チェックの設定を動的に生成
        /// </summary>
        /// <param name="tairyuuKbn">滞留登録かどうか</param>
        internal bool SetMeisaiRequiredSetting(out bool catchErr)
        {
            LogUtility.DebugMethodStart();

            bool err = true;
            String MsgPage = string.Empty;
            String MsgRows = string.Empty;
            catchErr = false;
            try
            {
                // ページ分データ作成
                for (int page = 0; page < this.form.tbc_MitsumoriMeisai.TabPages.Count; page++)
                {
                    // CustomDataGridViewを取得する
                    CustomDataGridView cdgv = ((CustomDataGridView)this.form.tbc_MitsumoriMeisai.TabPages[page].Controls["control"].Controls["CustomDataGridView"]);

                    MsgRows = string.Empty;

                    foreach (DataGridViewRow o in cdgv.Rows)
                    {
                        if (o.Index == cdgv.Rows.Count - 1)
                        {
                            break;
                        }
                        // 品名
                        var hinmeiName = (DgvCustomTextBoxCell)o.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME];

                        // 小計取得
                        var shoukeiFlg = (DgvCustomTextBoxCell)o.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SHOUKEI_FLG];

                        if (shoukeiFlg.Value != null && shoukeiFlg.Value.ToString() == "True")
                        {
                        }
                        else
                        {
                            // 201400708 syunrei EV002428_内税の計算を修正する。　start

                            //DataGridViewComboBoxCell cbc = (DataGridViewComboBoxCell)o.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD];
                            //if (cbc.Value == null || cbc.Value.ToString().Equals("0") || string.IsNullOrEmpty(cbc.Value.ToString()))
                            //{
                            //    if (string.IsNullOrEmpty(MsgRows))
                            //    {
                            //        MsgRows = MsgRows + (o.Index + 1);
                            //    }
                            //    else
                            //    {
                            //        MsgRows = MsgRows + "," + (o.Index + 1);
                            //    }
                            //}
                            // 201400708 syunrei EV002428_内税の計算を修正する。　end
                            if (hinmeiName.Value == null)
                            {
                                hinmeiName.IsInputErrorOccured = true;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(MsgRows))
                    {
                        MsgPage = MsgPage + (page + 1).ToString() + "ページ[" + MsgRows + "]\n";

                        err = false;
                    }
                }

                if (!err)
                {
                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E068", "税区分", MsgPage);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetMeisaiRequiredSetting", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E245", "");
                err = false;
                catchErr = true;
            }

            LogUtility.DebugMethodEnd(err, catchErr);

            return err;
        }

        #endregion 明細必須チェックの設定を動的に生成

        #region Detail入力された明細行が1行もありません。

        /// <summary>
        /// Detail必須チェック
        /// 入力された明細行が1行もありません。
        /// </summary>
        /// <returns>true: 一件以上入力されている, false: 一件も入力されていない</returns>
        internal bool CheckRequiredDataForDeitalEmpty(out bool catchErr)
        {
            LogUtility.DebugMethodStart();

            bool returnVal = true;
            catchErr = false;
            try
            {
                // ページ分データ作成
                for (int page = 0; page < this.form.tbc_MitsumoriMeisai.TabPages.Count; page++)
                {
                    // CustomDataGridViewを取得する
                    CustomDataGridView cdgv = ((CustomDataGridView)this.form.tbc_MitsumoriMeisai.TabPages[page].Controls["control"].Controls["CustomDataGridView"]);

                    if (cdgv.Rows.Count == 1)
                    {
                        returnVal = false;

                        return returnVal;

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckRequiredDataForDeitalEmpty", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E245", "");
                returnVal = false;
                catchErr = true;
            }

            LogUtility.DebugMethodEnd(returnVal, catchErr);
            return returnVal;
        }

        #endregion Detail入力された明細行が1行もありません。

        #region Detail空ページはページ間に残せません

        /// <summary>
        /// Detail必須チェック
        /// 空ページはページ間に残せません
        /// </summary>
        /// <returns>true: 一件以上入力されている, false: 一件も入力されていない</returns>
        internal bool CheckRequiredDataForDeital(out bool catchErr)
        {
            LogUtility.DebugMethodStart();

            bool returnVal = true;
            catchErr = false;
            try
            {
                // Detail
                String pageStatus = string.Empty;

                int selectedTab = 0;
                //int selectedTab = int.Parse(this.form.tbc_MitsumoriMeisai.SelectedTab.Name.Split('e')[1]) - 1;
                // ページ分データ作成
                for (int page = this.form.tbc_MitsumoriMeisai.TabPages.Count - 1; page > 0; page--)
                {
                    // CustomDataGridViewを取得する
                    CustomDataGridView cdgv = ((CustomDataGridView)this.form.tbc_MitsumoriMeisai.TabPages[page].Controls["control"].Controls["CustomDataGridView"]);

                    if (cdgv.Rows.Count > 1)
                    {
                        selectedTab = page;

                        break;
                    }
                }

                // ページ分データ作成
                for (int page = selectedTab; page > 0; page--)
                {
                    // CustomDataGridViewを取得する
                    CustomDataGridView cdgv = ((CustomDataGridView)this.form.tbc_MitsumoriMeisai.TabPages[page].Controls["control"].Controls["CustomDataGridView"]);

                    if (cdgv.Rows.Count == 1)
                    {
                        returnVal = false;

                        return returnVal;

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckRequiredDataForDeital", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                returnVal = false;
                catchErr = true;
            }

            LogUtility.DebugMethodEnd(returnVal, catchErr);
            return returnVal;
        }

        #endregion Detail空ページはページ間に残せません

        #region 画面表示設定

        /// <summary>
        /// 画面表示設定
        /// </summary>
        internal void WindowsItemsSet()
        {
            LogUtility.DebugMethodStart();

            // 画面項目初期化
            this.WindowsItemsInit();

            // モードによる制御
            if (this.IsRequireData())
            {
                // 見積設定
                this.WindowsSetEntrtyData();

                // 見積明細を設定
                this.WindowsSetDetailData();

                //取引先、業者、現場ロストフォーカスイベント
                bool catchErr = false;
                this.Torihikisaki_GyoushaCD_GenbaCD_LostFocus(this.dto.entryEntity.TORIHIKISAKI_CD, this.dto.entryEntity.GYOUSHA_CD, this.dto.entryEntity.GENBA_CD, out catchErr);
                if (catchErr) { throw new Exception(""); }

                //取引先、業者、現場タブ画面表示設定
                if (!this.Torihikisaki_GyoushaCD_GenbaCD_SetTab(false)) { throw new Exception(""); }
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion 画面表示設定

        #region CustomDataGridViewのrowに対しDENPYOU_NUMBERを採番します

        /// <summary>
        /// CustomDataGridViewのrowに対しDENPYOU_NUMBERを採番します
        /// </summary>
        public bool CreateDenpyoNumber()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            try
            {
                // ロード場合、処理しない
                if (this.form.nowLoding)
                {
                    return ret;
                }

                this.form.CreateDenpyoNumbering = true;

                // Detail
                // 伝票番号
                int denNo = 0;
                int rowNo = 0;

                // ページ分データ作成
                for (int page = 0; page < this.form.tbc_MitsumoriMeisai.TabPages.Count; page++)
                {
                    // CustomDataGridView(この行を変更しないで！！ 自動でタグのCustomDataGridViewを設定する
                    CustomDataGridView cdgv = ((CustomDataGridView)this.form.tbc_MitsumoriMeisai.TabPages[page].Controls["control"].Controls["CustomDataGridView"]);

                    // 画面データ
                    for (int i = 0; i < cdgv.Rows.Count - 1; i++)
                    {
                        DataGridViewRow row = cdgv.Rows[i];

                        // 小計行は伝票番号を入れない
                        var shoukeiFlg = (DgvCustomTextBoxCell)row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SHOUKEI_FLG];

                        // 行番号設定
                        rowNo = rowNo + 1;
                        row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_ROW_NO].Value = rowNo;

                        // 伝票番号設定（小計の場合、計算しない）
                        if (shoukeiFlg.Value != null && shoukeiFlg.Value.ToString() == "True")
                        {
                            row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_NO].Value = String.Empty;
                        }
                        else
                        {
                            denNo = denNo + 1;

                            row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_NO].Value = denNo;
                        }
                    }
                }

                this.form.CreateDenpyoNumbering = false;

                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateDenpyoNumber", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion CustomDataGridViewのrowに対しDENPYOU_NUMBERを採番します

        #region 画面情報の初期化処理

        /// <summary>
        /// 画面情報の初期化処理
        /// </summary>
        public bool WindowInit()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
                // SystemInfo取得
                this.mSysInfo = this.GetSysInfo();

                // footerForm、headerFormの定義
                footerForm = (BusinessBaseForm)this.form.Parent;
                headerForm = (HeaderForm)footerForm.headerForm;

                // 活性制御
                if (!this.ChangeEnabledForInputControl(false))
                {
                    throw new Exception("");
                }

                // 画面表示設定　
                this.WindowsItemsSet();

                // 削除、参照場合、活性制御
                if (this.form.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG) || this.form.WindowType.Equals(WINDOW_TYPE.REFERENCE_WINDOW_FLAG))
                {
                    // 削除モード時には全コントロールをReadOnlyにする
                    if (!this.ChangeEnabledForInputControl(true))
                    {
                        throw new Exception("");
                    }
                }

                // 201400704 syunrei EV002427_見積入力の税計算区分がシステム設定の税区分締処理利用形態を参照していない　start
                //システム設定入力の税区分/締処理利用形態の登録内容に応じて、見積入力の税計算区分で選択されていない方を非活性にする。
                // 201400708 syunrei EV002428_内税の計算を修正する。　start
                if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    // 201400708 syunrei EV002428_内税の計算を修正する。　end
                    if (!this.InitSaiZeiKbnCtrl()) { throw new Exception(""); }
                }
                else
                {
                    //伝票毎のとき、内税が利用不可
                    if (this.dto.entryEntity.ZEI_KEISAN_KBN_CD.ToString().Equals("1"))
                    {
                        this.form.rdo_Uchizei.Enabled = false;
                        //内税のテキストをクリアする
                        if (this.dto.entryEntity.ZEI_KBN_CD.ToString().Equals("2"))
                        {
                            this.form.txt_ZeiKbnCD.Text = "";
                        }
                    }
                }
                // 201400704 syunrei EV002427_見積入力の税計算区分がシステム設定の税区分締処理利用形態を参照していない　end

                // 201400708 syunrei ＃947　№11　　start
                if (!this.SetMeisaiZeiKbnCtr(false)) { throw new Exception(""); }
                //・見積入力画面内、状況項目内、進行中項目は削除する。
                this.DeleteJoukyo();
                // 201400708 syunrei ＃947　№11　　end

                // 20140717 syunrei EV005320_前次ボタンにて伝票を呼び出すと伝票番号にフォーカスが当たっていてフォーカスアウトすると再度同じ伝票を読み込んでしまう。　start
                if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    this.form.txt_MitsumoriNumber.Enabled = false;
                    this.headerForm.KYOTEN_CD.Focus();
                }
                else
                {
                    this.form.txt_MitsumoriNumber.Enabled = true;
                }
                // 20140717 syunrei EV005320_前次ボタンにて伝票を呼び出すと伝票番号にフォーカスが当たっていてフォーカスアウトすると再度同じ伝票を読み込んでしまう。　end

                // 見積書種類ポップアップ初期表示処理
                this.MitsumoriSyuruiPopUpDataInit();
                this.form.beforTorihikisakiHikiai = this.form.txt_Torihikisaki_hikiai_flg.Text;
                this.form.beforGyoushaHikiai = this.form.txt_Gyousha_hikiai_flg.Text;
                this.form.beforeGenbaHikiai = this.form.txt_Genba_hikiai_flg.Text;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("WindowInit", ex);
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    if (ex is SQLRuntimeException)
                    {
                        msgLogic.MessageBoxShow("E093", "");
                    }
                    else
                    {
                        msgLogic.MessageBoxShow("E245", "");
                    }
                }
                ret = false;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }

        #endregion 画面情報の初期化処理

        #region ボタンの初期化

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        public bool ButtonInit(bool registeredFlag = false)
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
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
                        this.footerForm.bt_func1.Enabled = false;
                        this.footerForm.bt_func2.Enabled = true;
                        this.footerForm.bt_func3.Enabled = false;
                        this.footerForm.bt_func4.Enabled = false;
                        this.footerForm.bt_func5.Enabled = true;
                        this.footerForm.bt_func7.Enabled = true;
                        this.footerForm.bt_func8.Enabled = false;
                        this.footerForm.bt_func9.Enabled = true;
                        this.footerForm.bt_func10.Enabled = true;
                        this.footerForm.bt_func11.Enabled = true;
                        this.footerForm.bt_func12.Enabled = true;
                        this.footerForm.bt_process1.Enabled = true;
                        this.footerForm.bt_process2.Enabled = true;

                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 修正モードの場合、登録後に初めて活性化するボタンがあるので制御
                        if (registeredFlag)
                        {
                            this.footerForm.bt_func1.Enabled = false;
                            this.footerForm.bt_func2.Enabled = true;
                            this.footerForm.bt_func3.Enabled = true;
                            this.footerForm.bt_func5.Enabled = true;
                            this.footerForm.bt_func8.Enabled = true;
                            this.footerForm.bt_func9.Enabled = false;
                            this.footerForm.bt_func10.Enabled = false;
                            this.footerForm.bt_func11.Enabled = false;
                            this.footerForm.bt_process1.Enabled = false;
                            this.footerForm.bt_process2.Enabled = false;
                            //this.footerForm.bt_process3.Enabled = false;
                            this.footerForm.bt_process4.Enabled = false;
                            this.footerForm.bt_process5.Enabled = false;
                        }
                        else
                        {
                            this.footerForm.bt_func1.Enabled = false;
                            this.footerForm.bt_func2.Enabled = true;
                            this.footerForm.bt_func3.Enabled = true;
                            this.footerForm.bt_func5.Enabled = true;
                            this.footerForm.bt_func8.Enabled = true;
                            this.footerForm.bt_func9.Enabled = true;
                            this.footerForm.bt_func10.Enabled = true;
                            this.footerForm.bt_func11.Enabled = true;
                            this.footerForm.bt_process1.Enabled = true;
                            this.footerForm.bt_process2.Enabled = true;
                            //this.footerForm.bt_process3.Enabled = true;
                            this.footerForm.bt_process4.Enabled = true;
                            this.footerForm.bt_process5.Enabled = true;
                        }

                        break;

                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        // 削除モードの場合、
                        this.footerForm.bt_func1.Enabled = false;
                        this.footerForm.bt_func2.Enabled = true;
                        this.footerForm.bt_func3.Enabled = true;
                        this.footerForm.bt_func4.Enabled = false;
                        this.footerForm.bt_func5.Enabled = false;
                        this.footerForm.bt_func7.Enabled = true;
                        this.footerForm.bt_func8.Enabled = true;
                        this.footerForm.bt_func9.Enabled = true;
                        this.footerForm.bt_func10.Enabled = false;
                        this.footerForm.bt_func11.Enabled = false;
                        this.footerForm.bt_func12.Enabled = true;
                        this.footerForm.bt_process1.Enabled = false;
                        this.footerForm.bt_process2.Enabled = false;
                        //this.footerForm.bt_process3.Enabled = false;
                        this.footerForm.bt_process4.Enabled = false;
                        this.footerForm.bt_process5.Enabled = false;
                        break;

                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        // 参照モードの場合、
                        this.footerForm.bt_func1.Enabled = false;
                        this.footerForm.bt_func2.Enabled = true;
                        this.footerForm.bt_func3.Enabled = true;
                        this.footerForm.bt_func4.Enabled = false;
                        this.footerForm.bt_func5.Enabled = true;
                        this.footerForm.bt_func7.Enabled = true;
                        this.footerForm.bt_func8.Enabled = true;
                        this.footerForm.bt_func9.Enabled = false;
                        this.footerForm.bt_func10.Enabled = false;
                        this.footerForm.bt_func11.Enabled = false;
                        this.footerForm.bt_func12.Enabled = true;
                        this.footerForm.bt_process1.Enabled = false;
                        this.footerForm.bt_process2.Enabled = false;
                        this.footerForm.bt_process4.Enabled = false;
                        this.footerForm.bt_process5.Enabled = false;
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }

        #region イベントの初期化処理

        public bool EventInit()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
                #region ボタン押下イベント

                // 前へボタン押下イベント生成
                this.form.btn_Mae.Click += new EventHandler(this.form.PreMitsumoriData);

                // 次へボタン押下イベント生成
                this.form.btn_Tsugi.Click += new EventHandler(this.form.NextMitsumoriData);

                // 引合登録ボタンイベント生成
                //this.form.C_Regist(footerForm.bt_func1);
                //this.footerForm.bt_func1.Click += new EventHandler(this.form.RegistHikiaiData);
                //footerForm.bt_func1.ProcessKbn = PROCESS_KBN.NEW;

                // 新規ボタンイベント生成
                this.footerForm.bt_func2.Click += new EventHandler(this.form.ChangeNewWindow);

                // 修正ボタンイベント生成
                this.footerForm.bt_func3.Click += new EventHandler(this.form.ChangeUpdateWindow);

                // プレビューボタン押下イベント生成
                this.footerForm.bt_func5.Click += new EventHandler(this.form.ShowPreview);

                // 一覧ボタン押下イベント
                this.footerForm.bt_func7.Click += new EventHandler(this.form.ShowMitsumoriForm);

                // 複写ボタン押下イベント
                this.footerForm.bt_func8.Click += new EventHandler(this.form.CopyData);

                // 登録ボタン押下イベント
                this.footerForm.bt_func9.Click += new EventHandler(this.form.RegistMistumori);

                // 行挿入(F10)ボタンイベント生成
                this.footerForm.bt_func10.Click += new EventHandler(this.form.DataRowAdd);

                // 行削除(F11)ボタンイベント生成
                this.footerForm.bt_func11.Click += new EventHandler(this.form.DataRowDelete);

                //閉じるボタン(F12)イベント生成
                this.footerForm.bt_func12.Click += new EventHandler(this.form.FormClose);

                // 行コピーイベント生成
                this.footerForm.bt_process1.Click += new EventHandler(this.form.RowCopy);

                // 行添付イベント生成
                this.footerForm.bt_process2.Click += new EventHandler(this.form.RowPast);

                // 小計イベント生成
                //this.footerForm.bt_process3.Click += new EventHandler(this.form.SubTotal);

                // ページ追加イベント生成
                this.footerForm.bt_process4.Click += new EventHandler(this.form.PageAdd);

                // ページ削除イベント生成
                this.footerForm.bt_process5.Click += new EventHandler(this.form.PageDelete);

                // コントロールのイベント
                this.form.txt_TorihikisakiCD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.txt_GyoushaCD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.txt_GenbaCD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);

                #endregion ボタン押下イベント
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd();
            return ret;
        }

        #endregion イベントの初期化処理

        #region ボタン情報の設定

        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();

            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        #endregion ボタン情報の設定

        #region 画面項目初期表示処理

        /// <summary>
        /// 画面項目初期表示処理
        /// </summary>
        public void WindowsItemsInit()
        {
            LogUtility.DebugMethodStart();

            // 伝票区分一覧取得
            denpyouKbnDictionary.Clear();
            M_DENPYOU_KBN[] denpyous = this.accessor.GetAllDenpyouKbn();
            foreach (var denpyou in denpyous)
            {
                denpyouKbnDictionary.Add((short)denpyou.DENPYOU_KBN_CD, denpyou);
            }

            // 単位一覧取得
            unitDictionary.Clear();
            M_UNIT[] units = this.accessor.GetAllUnit();
            foreach (var unit in units)
            {
                unitDictionary.Add((short)unit.UNIT_CD, unit);
            }

            // ヘッダー Start
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();

            // 拠点
            headerForm.KYOTEN_CD.Text = string.Empty;
            headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
            const string KYOTEN_CD = "拠点CD";
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

            // ヘッダー End
            IM_SYS_INFODao daoIM_SYS_INFO = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            M_SYS_INFO[] sysInfo = daoIM_SYS_INFO.GetAllData();

            // 見積項目セット
            this.form.lb_MistumoriKoumoku1.Text = this.dto.sysInfoEntity.MITSUMORI_KOUMOKU1;
            this.form.lb_MistumoriKoumoku2.Text = this.dto.sysInfoEntity.MITSUMORI_KOUMOKU2;
            this.form.lb_MistumoriKoumoku3.Text = this.dto.sysInfoEntity.MITSUMORI_KOUMOKU3;
            this.form.lb_MistumoriKoumoku4.Text = this.dto.sysInfoEntity.MITSUMORI_KOUMOKU4;

            //20250416
            this.form.lb_MistumoriKoumoku5.Text = this.dto.sysInfoEntity.MITSUMORI_KOUMOKU5;

            // 見積項目内用設置
            this.form.txt_MitsumoriNaiyo1.Text = this.dto.sysInfoEntity.MITSUMORI_NAIYOU1;
            this.form.txt_MitsumoriNaiyo2.Text = this.dto.sysInfoEntity.MITSUMORI_NAIYOU2;
            this.form.txt_MitsumoriNaiyo3.Text = this.dto.sysInfoEntity.MITSUMORI_NAIYOU3;
            this.form.txt_MitsumoriNaiyo4.Text = this.dto.sysInfoEntity.MITSUMORI_NAIYOU4;

            //20250416
            this.form.txt_MitsumoriNaiyo5.Text = this.dto.sysInfoEntity.MITSUMORI_NAIYOU5;

            // 状況
            this.form.dtp_JuchuDate.Text = string.Empty;
            this.form.dtp_SichuDate.Text = string.Empty;
            this.form.dtp_JuchuDate.IsInputErrorOccured = false;
            this.form.dtp_SichuDate.IsInputErrorOccured = false;
            this.form.chk_Jyuchu.Checked = false;
            this.form.chk_sichu.Checked = false;

            // 新規モードの場合
            if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                // システム設定にしたがって税区分、税計算区分設定
                if (sysInfo != null && sysInfo.Length > 0 && !sysInfo[0].MITSUMORI_ZEIKEISAN_KBN_CD.IsNull)
                {
                    this.form.txt_ZeiKeisanKbnCD.Text = sysInfo[0].MITSUMORI_ZEIKEISAN_KBN_CD.ToString();
                }
                if (sysInfo != null && sysInfo.Length > 0 && !sysInfo[0].MITSUMORI_ZEI_KBN_CD.IsNull)
                {
                    this.form.txt_ZeiKbnCD.Text = sysInfo[0].MITSUMORI_ZEI_KBN_CD.ToString();
                }
            }
            else
            {
                // 税区分、税計算区分クリア
                this.form.txt_ZeiKeisanKbnCD.Text = "1";
                this.form.txt_ZeiKbnCD.Text = "1";
            }

            this.form.txt_MitsumoriInjiDate.Text = "1";
            // 201400708 syunrei ＃947　№13　　start
            if (!this.dto.sysInfoEntity.BUSHO_NAME_PRINT.IsNull)
            {
                this.form.txt_BushouNameInji.Text = this.dto.sysInfoEntity.BUSHO_NAME_PRINT.ToString();
            }
            else
            {
                this.form.txt_BushouNameInji.Text = "1";
            }
            // 201400708 syunrei ＃947　№13　　end

            #region 20250411

            //this.form.txt_Bikou1.Text = this.dto.sysInfoEntity.MITSUMORI_BIKOU1;
            //this.form.txt_Bikou2.Text = this.dto.sysInfoEntity.MITSUMORI_BIKOU2;
            //this.form.txt_Bikou3.Text = this.dto.sysInfoEntity.MITSUMORI_BIKOU3;
            //this.form.txt_Bikou4.Text = this.dto.sysInfoEntity.MITSUMORI_BIKOU4;
            //this.form.txt_Bikou5.Text = this.dto.sysInfoEntity.MITSUMORI_BIKOU5;

            #endregion 20250411

            // 件名を設置
            this.form.txt_Kenmei.Text = this.dto.sysInfoEntity.MITSUMORI_SUBJECT_DEFAULT;

            //20250416
            this.form.txt_Kenmei2.Text = this.dto.sysInfoEntity.MITSUMORI_SUBJECT_DEFAULT_1;

            // 営業担当者クリア
            this.form.txt_ShainCD.Text = string.Empty;
            this.form.txt_ShainNameRyaku.Text = string.Empty;

            if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                // 見積番号クリア
                this.form.txt_MitsumoriNumber.Text = string.Empty;
            }

            // 見積書種類表示
            var parentForm = (BusinessBaseForm)this.form.Parent;
            this.form.dtp_MitsumoriDate.Value = parentForm.sysDate;
            // 20140704 ria EV002428 内税の計算を修正する。 start
            // 消費税を取得
            this.getShohizeData();
            // 20140704 ria EV002428 内税の計算を修正する。 end

            // 見積書種類CDによってCD及び名称を設定します
            this.ChangeMitsumoriSyuruiName();

            // 新規モードの場合
            if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                // 印字拠点情報システム設定にしたがってデフォルト値設定
                if (sysInfo != null && sysInfo.Length > 0 && !sysInfo[0].MITSUMORI_INJI_KYOTEN_CD1.IsNull)
                {
                    this.form.txt_InjiKyoten1CD.Text = string.Format("{0:D2}", Int16.Parse(sysInfo[0].MITSUMORI_INJI_KYOTEN_CD1.ToString()));
                }
                if (!string.IsNullOrEmpty(this.form.txt_InjiKyoten1CD.Text))
                {
                    this.form.txt_Kyoten1NameRyaku.Text = getKyotenName(this.form.txt_InjiKyoten1CD.Text);
                }
                if (sysInfo != null && sysInfo.Length > 0 && !sysInfo[0].MITSUMORI_INJI_KYOTEN_CD2.IsNull)
                {
                    this.form.txt_InjiKyoten2CD.Text = string.Format("{0:D2}", Int16.Parse(sysInfo[0].MITSUMORI_INJI_KYOTEN_CD2.ToString()));
                }
                if (!string.IsNullOrEmpty(this.form.txt_InjiKyoten2CD.Text))
                {
                    this.form.txt_Kyoten2NameRyaku.Text = getKyotenName(this.form.txt_InjiKyoten2CD.Text);
                }
            }
            else
            {
                // 拠点情報クリア
                this.form.txt_InjiKyoten1CD.Text = string.Empty;
                this.form.txt_Kyoten1NameRyaku.Text = string.Empty;
                this.form.txt_InjiKyoten2CD.Text = string.Empty;
                this.form.txt_Kyoten2NameRyaku.Text = string.Empty;
            }

            // 取引先クリア
            this.form.chk_TorihikisakiInji.Checked = false;
            this.form.txt_TorihikisakiCD.Text = string.Empty;
            this.form.txt_TorihikisakiMei.Text = string.Empty;
            this.form.txt_TorihikisakiMei.ReadOnly = true;
            this.form.txt_TorihikisakiMei.TabStop = false;
            this.form.txt_TorihikisakiFurigana.Text = string.Empty;
            this.beforeTorihikisakiName = string.Empty;
            this.form.txt_Torihikisaki_hikiai_flg.Text = string.Empty;
            this.form.txt_Torihikisaki_shokuchi_kbn.Text = string.Empty;
            // 業者クリア
            this.form.chk_GyushaInji.Checked = false;
            this.form.txt_GyoushaCD.Text = string.Empty;
            this.form.txt_GyoushaName.Text = string.Empty;
            this.form.txt_GyoushaName.ReadOnly = true;
            this.form.txt_GyoushaName.TabStop = false;
            this.form.txt_GyoushaFurigana.Text = string.Empty;
            this.beforeGyoushaName = string.Empty;
            this.form.txt_Gyousha_hikiai_flg.Text = string.Empty;
            this.form.txt_Gyousha_shokuchi_kbn.Text = string.Empty;
            // 現場クリア
            this.form.chk_GenbaInji.Checked = false;
            this.form.txt_GenbaCD.Text = string.Empty;
            this.form.txtGenbaName.Text = string.Empty;
            this.form.txtGenbaName.ReadOnly = true;
            this.form.txtGenbaName.TabStop = false;
            this.form.txtGenbaFurigana.Text = string.Empty;
            this.beforeGenbaName = string.Empty;
            this.form.txt_Genba_hikiai_flg.Text = string.Empty;
            this.form.txt_Genba_shokuchi_kbn.Text = string.Empty;
            // 敬称クリア
            this.form.lb_TorihikisakiKeishou.Text = string.Empty;
            this.form.lb_GyoushaKeishou.Text = string.Empty;
            this.form.lb_GenbaKeishou.Text = string.Empty;

            // 金額系クリア
            this.form.txt_KingakuTotal.Text = string.Empty;
            this.form.txt_TaxSoto.Text = string.Empty;
            this.form.txt_KazeiTaisyoGaku.Text = string.Empty;
            this.form.txt_GoukeiKingakuTotal.Text = string.Empty;
            this.form.txt_UchiTotal.Text = string.Empty;

            this.form.beforMitsumoriNumber = string.Empty;
            this.form.beforTorihikisakiCD = string.Empty;
            this.form.beforGyousaCD = string.Empty;
            this.form.beforeGenbaCD = string.Empty;

            // タブコントロール内クリア
            this.initTorihikisakiTab();
            this.initGyoushaTab();
            this.initGenbaTab();
            this.form.txt_ShanaiBikou.Text = string.Empty;

            // 状況クリア
            this.form.dtp_JuchuDate.Value = null;
            this.form.dtp_SichuDate.Value = null;

            if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                // 取引先タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Torihikisaki.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                //業者タブの項目を使用不可にする
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Gyousya.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                // 現場タブを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Genba.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }
            }

            //20250414
            this.form.BIKO_KBN_CD.Text = string.Empty;
            this.form.BIKO_NAME_RYAKU.Text = string.Empty;
            this.form.Ichiran.Rows.Clear();

            // 引合取引先チェックボックスを未選択にする
            this.form.chk_HikiaiTorihikisakiFlg.Checked = false;

            // 引合業者チェックを未選択にする
            this.form.chk_HikiaiGyoushaFlg.Checked = false;

            // 引合現場チェックボックスを未選択
            this.form.chk_HikiaiGenbaFlg.Checked = false;

            // tabPage初期化
            if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                this.form.tbc_MitsumoriMeisai.TabPages.Clear();

                // ページを追加
                this.form.PageAdd();
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion 画面項目初期表示処理

        /// <summary>
        /// ヘッダ情報設定
        /// </summary>
        /// <param name="windowType"></param>
        public void setHeaderInfo(WINDOW_TYPE windowType)
        {
            LogUtility.DebugMethodStart(windowType);

            this.headerForm.windowTypeLabel.BackColor = WINDOW_TYPEExt.ToLabelColor(WINDOW_TYPE.NONE);
            this.headerForm.windowTypeLabel.ForeColor = WINDOW_TYPEExt.ToLabelForeColor(WINDOW_TYPE.NONE);

            this.headerForm.windowTypeLabel.Text = WINDOW_TYPEExt.ToTypeString(windowType);
            this.headerForm.windowTypeLabel.BackColor = WINDOW_TYPEExt.ToLabelColor(windowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// Logic内で定義されているEntityすべての最新情報を取得する
        /// </summary>
        /// <returns>true:正常値、false:エラー発生</returns>
        public bool GetAllEntityData(out bool catchErr)
        {
            LogUtility.DebugMethodStart();
            catchErr = false;
            bool ret = true;
            try
            {
                // 更新前データを保持しておく
                this.beforDto = new DTOClass();
                // 初期化
                this.dto = new DTOClass();

                // 画面のモードに依存しないデータの取得
                this.dto.sysInfoEntity = CommonShogunData.SYS_INFO;

                // 見積日付
                if (this.form.dtp_MitsumoriDate.Value != null && !string.IsNullOrEmpty((this.form.dtp_MitsumoriDate.Value).ToString().Trim()))
                {
                    // 消費税率取得
                    this.taxRate = this.GetShouhizeiRate(this.form.dtp_MitsumoriDate.Value);
                }

                if (!this.IsRequireData())
                {
                    return ret;
                }

                // 見積入力
                var entrys = accessor.GetMitsumoriEntry(this.form.mitsumoriNumber);
                if (entrys == null || entrys.Length < 1)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");

                    if (!this.beforDto.entryEntity.MITSUMORI_NUMBER.IsNull)
                    {
                        this.form.txt_MitsumoriNumber.Text = this.beforDto.entryEntity.MITSUMORI_NUMBER.ToString();
                    }

                    ret = false;
                    return ret;
                }
                else
                {
                    this.dto.entryEntity = entrys[0];
                }

                // 見積明細
                var details = accessor.GetMitsumoriDetail(this.dto.entryEntity.SYSTEM_ID, this.dto.entryEntity.SEQ);
                if (details == null || details.Length < 1)
                {
                    this.dto.detailEntity = new T_MITSUMORI_DETAIL[] { new T_MITSUMORI_DETAIL() };
                }
                else
                {
                    this.dto.detailEntity = details;
                }

                //20250416
                var details_2 = accessor.GetBikoDetail(this.dto.entryEntity.SYSTEM_ID, this.dto.entryEntity.SEQ);
                if (details_2 == null || details_2.Length < 1)
                {
                    this.dto.detailEntity_2 = new T_MITSUMORI_DETAIL_2[] { new T_MITSUMORI_DETAIL_2() };
                }
                else
                {
                    this.dto.detailEntity_2 = details_2;
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

                // すべてのデータを保存
                this.beforDto = this.dto.Clone();
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetAllEntityData", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        /// <summary>
        /// ヘッダーの拠点CDの存在チェック
        /// </summary>
        internal void CheckKyotenCd()
        {
            LogUtility.DebugMethodStart();

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
                this.headerForm.KYOTEN_NAME_RYAKU.Text = kyoten.KYOTEN_NAME_RYAKU.ToString();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 指定した見積番号のデータが存在するか返す
        /// </summary>
        /// <param name="mitsumoriNumber">見積番号</param>
        /// <returns>true:存在する, false:存在しない</returns>
        internal bool IsExistMitsumoriData(long mitsumoriNumber)
        {
            LogUtility.DebugMethodStart(mitsumoriNumber);

            bool returnVal = false;
            try
            {
                if (0 <= mitsumoriNumber)
                {
                    var ukeireEntrys = this.accessor.GetMitsumoriEntry(mitsumoriNumber);
                    if (ukeireEntrys != null
                        && 0 < ukeireEntrys.Length)
                    {
                        returnVal = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsExistMitsumoriData", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                returnVal = false;
            }
            LogUtility.DebugMethodEnd();
            return returnVal;
        }

        /// <summary>
        /// 見積入力データを取得処理
        /// </summary>
        private void WindowsSetEntrtyData()
        {
            LogUtility.DebugMethodStart();
            try
            {
                // ヘッダー Start
                // 拠点
                if (!this.dto.entryEntity.KYOTEN_CD.IsNull)
                {
                    headerForm.KYOTEN_CD.Text = this.dto.entryEntity.KYOTEN_CD.ToString().PadLeft(2, '0');
                    if (!string.IsNullOrEmpty(this.dto.kyotenEntity.KYOTEN_NAME_RYAKU))
                    {
                        headerForm.KYOTEN_NAME_RYAKU.Text = this.dto.kyotenEntity.KYOTEN_NAME_RYAKU.ToString();
                    }
                }
                // ヘッダー End

                // 営業担当者情報設定
                if (this.dto.entryEntity.SHAIN_CD != null && !string.IsNullOrEmpty(this.dto.entryEntity.SHAIN_CD.ToString()))
                {
                    form.txt_ShainCD.Text = this.dto.entryEntity.SHAIN_CD.ToString();
                }
                if (this.dto.entryEntity.SHAIN_NAME != null && !string.IsNullOrEmpty(this.dto.entryEntity.SHAIN_NAME.ToString()))
                {
                    form.txt_ShainNameRyaku.Text = this.dto.entryEntity.SHAIN_NAME.ToString();
                }

                // 見積日付設定
                if (this.dto.entryEntity.MITSUMORI_DATE != null)
                {
                    this.form.dtp_MitsumoriDate.Text = this.dto.entryEntity.MITSUMORI_DATE;
                }

                // 拠点情報設定
                if (!this.dto.entryEntity.INJI_KYOTEN1_CD.IsNull)
                {
                    form.txt_InjiKyoten1CD.Text = this.dto.entryEntity.INJI_KYOTEN1_CD.ToString().PadLeft(2, '0');
                }
                if (!this.dto.entryEntity.INJI_KYOTEN1_CD.IsNull)
                {
                    this.form.txt_Kyoten1NameRyaku.Text = getKyotenName(this.form.txt_InjiKyoten1CD.Text);
                }
                if (!this.dto.entryEntity.INJI_KYOTEN2_CD.IsNull)
                {
                    form.txt_InjiKyoten2CD.Text = this.dto.entryEntity.INJI_KYOTEN2_CD.ToString().PadLeft(2, '0');
                }
                if (!this.dto.entryEntity.INJI_KYOTEN2_CD.IsNull)
                {
                    this.form.txt_Kyoten2NameRyaku.Text = getKyotenName(this.form.txt_InjiKyoten2CD.Text);
                }

                // 取引先設定
                if (this.dto.entryEntity.TORIHIKISAKI_CD != null && !string.IsNullOrEmpty(this.dto.entryEntity.TORIHIKISAKI_CD.ToString()))
                {
                    this.form.txt_TorihikisakiCD.Text = this.dto.entryEntity.TORIHIKISAKI_CD.ToString();

                    this.form.beforTorihikisakiCD = this.dto.entryEntity.TORIHIKISAKI_CD.ToString();
                }
                if (!this.dto.entryEntity.TORIHIKISAKI_INJI.IsNull && (!this.dto.entryEntity.TORIHIKISAKI_INJI.IsNull && (Boolean)this.dto.entryEntity.TORIHIKISAKI_INJI))
                {
                    this.form.chk_TorihikisakiInji.Checked = true;
                }
                if (!this.dto.entryEntity.TORIHIKISAKI_INJI.IsNull && (!this.dto.entryEntity.HIKIAI_TORIHIKISAKI_FLG.IsNull && (Boolean)this.dto.entryEntity.HIKIAI_TORIHIKISAKI_FLG))
                {
                    this.form.chk_HikiaiTorihikisakiFlg.Checked = true;
                }

                // 請求区分
                if (this.dto.entryEntity.TORIHIKISAKI_CD != null && !string.IsNullOrEmpty(this.dto.entryEntity.TORIHIKISAKI_CD.ToString()))
                {
                    //this.getSeikyuData(this.dto.Entry.TORIHIKISAKI_CD.ToString());
                }

                // 支払取引先税区分
                if (this.dto.entryEntity.TORIHIKISAKI_CD != null && !string.IsNullOrEmpty(this.dto.entryEntity.TORIHIKISAKI_CD.ToString()))
                {
                    //this.getShiharaiData(this.dto.Entry.TORIHIKISAKI_CD.ToString());
                }

                if (this.dto.entryEntity.TORIHIKISAKI_CD != null && !string.IsNullOrEmpty(this.dto.entryEntity.TORIHIKISAKI_CD.ToString()))
                {
                    this.form.txt_TorihikisakiMei.Text = this.dto.entryEntity.TORIHIKISAKI_NAME.ToString();
                }

                // 業者設定
                if (!this.dto.entryEntity.GYOUSHA_INJI.IsNull && ((Boolean)this.dto.entryEntity.GYOUSHA_INJI))
                {
                    this.form.chk_GyushaInji.Checked = true;
                }

                if (!this.dto.entryEntity.GYOUSHA_INJI.IsNull && (Boolean)this.dto.entryEntity.HIKIAI_GENBA_FLG)
                {
                    this.form.chk_HikiaiGyoushaFlg.Checked = true;
                }
                if (this.dto.entryEntity.GYOUSHA_CD != null && !string.IsNullOrEmpty(this.dto.entryEntity.GYOUSHA_CD.ToString()))
                {
                    this.form.txt_GyoushaCD.Text = this.dto.entryEntity.GYOUSHA_CD.ToString();
                    this.form.beforGyousaCD = this.dto.entryEntity.GYOUSHA_CD.ToString();
                }
                if (this.dto.entryEntity.GYOUSHA_NAME != null && !string.IsNullOrEmpty(this.dto.entryEntity.GYOUSHA_NAME.ToString()))
                {
                    this.form.txt_GyoushaName.Text = this.dto.entryEntity.GYOUSHA_NAME.ToString();
                }

                // 現場設定
                if (!this.dto.entryEntity.GENBA_INJI.IsNull && ((Boolean)this.dto.entryEntity.GENBA_INJI))
                {
                    this.form.chk_GenbaInji.Checked = true;
                }
                if (!this.dto.entryEntity.HIKIAI_GENBA_FLG.IsNull && ((Boolean)this.dto.entryEntity.HIKIAI_GENBA_FLG))
                {
                    this.form.chk_HikiaiGenbaFlg.Checked = true;
                }
                if (this.dto.entryEntity.GENBA_CD != null && !string.IsNullOrEmpty(this.dto.entryEntity.GENBA_CD.ToString()))
                {
                    this.form.txt_GenbaCD.Text = this.dto.entryEntity.GENBA_CD.ToString();
                    this.form.beforeGenbaCD = this.dto.entryEntity.GENBA_CD.ToString();
                }
                if (this.dto.entryEntity.GENBA_NAME != null && !string.IsNullOrEmpty(this.dto.entryEntity.GENBA_NAME.ToString()))
                {
                    this.form.txtGenbaName.Text = this.dto.entryEntity.GENBA_NAME.ToString();
                }

                // 見積番号
                if (!this.dto.entryEntity.MITSUMORI_NUMBER.IsNull && !string.IsNullOrEmpty(this.dto.entryEntity.MITSUMORI_NUMBER.ToString()))
                {
                    this.form.txt_MitsumoriNumber.Text = this.dto.entryEntity.MITSUMORI_NUMBER.ToString();
                }

                // 件名
                if (this.dto.entryEntity.KENMEI != null && !string.IsNullOrEmpty(this.dto.entryEntity.KENMEI.ToString()))
                {
                    this.form.txt_Kenmei.Text = this.dto.entryEntity.KENMEI.ToString();
                }
                else
                {
                    this.form.txt_Kenmei.Text = string.Empty;
                }

                //20250415
                if (this.dto.entryEntity.KENMEI_2 != null && !string.IsNullOrEmpty(this.dto.entryEntity.KENMEI_2.ToString()))
                {
                    this.form.txt_Kenmei2.Text = this.dto.entryEntity.KENMEI_2.ToString();
                }
                else
                {
                    this.form.txt_Kenmei2.Text = string.Empty;
                }

                // 敬称(取引先、業者、現場)
                if (this.dto.entryEntity.TORIHIKISAKI_KEISHOU != null && !string.IsNullOrEmpty(this.dto.entryEntity.TORIHIKISAKI_KEISHOU.ToString()))
                {
                    this.form.lb_TorihikisakiKeishou.Text = this.dto.entryEntity.TORIHIKISAKI_KEISHOU.ToString();
                }
                if (this.dto.entryEntity.GYOUSHA_KEISHOU != null && !string.IsNullOrEmpty(this.dto.entryEntity.GYOUSHA_KEISHOU.ToString()))
                {
                    this.form.lb_GyoushaKeishou.Text = this.dto.entryEntity.GYOUSHA_KEISHOU.ToString();
                }
                if (this.dto.entryEntity.GENBA_KEISHOU != null && !string.IsNullOrEmpty(this.dto.entryEntity.GENBA_KEISHOU.ToString()))
                {
                    this.form.lb_GenbaKeishou.Text = this.dto.entryEntity.GENBA_KEISHOU.ToString();
                }

                // 見積項目１～４
                if (this.dto.entryEntity.MITSUMORI_1 != null && !string.IsNullOrEmpty(this.dto.entryEntity.MITSUMORI_1.ToString()))
                {
                    this.form.txt_MitsumoriNaiyo1.Text = this.dto.entryEntity.MITSUMORI_1.ToString();
                }
                else
                {
                    this.form.txt_MitsumoriNaiyo1.Text = string.Empty;
                }
                if (this.dto.entryEntity.MITSUMORI_2 != null && !string.IsNullOrEmpty(this.dto.entryEntity.MITSUMORI_2.ToString()))
                {
                    this.form.txt_MitsumoriNaiyo2.Text = this.dto.entryEntity.MITSUMORI_2.ToString();
                }
                else
                {
                    this.form.txt_MitsumoriNaiyo2.Text = string.Empty;
                }
                if (this.dto.entryEntity.MITSUMORI_3 != null && !string.IsNullOrEmpty(this.dto.entryEntity.MITSUMORI_3.ToString()))
                {
                    this.form.txt_MitsumoriNaiyo3.Text = this.dto.entryEntity.MITSUMORI_3.ToString();
                }
                else
                {
                    this.form.txt_MitsumoriNaiyo3.Text = string.Empty;
                }
                if (this.dto.entryEntity.MITSUMORI_4 != null && !string.IsNullOrEmpty(this.dto.entryEntity.MITSUMORI_4.ToString()))
                {
                    this.form.txt_MitsumoriNaiyo4.Text = this.dto.entryEntity.MITSUMORI_4.ToString();
                }
                else
                {
                    this.form.txt_MitsumoriNaiyo4.Text = string.Empty;
                }

                //20250416
                if (this.dto.entryEntity.MITSUMORI_5 != null && !string.IsNullOrEmpty(this.dto.entryEntity.MITSUMORI_5.ToString()))
                {
                    this.form.txt_MitsumoriNaiyo5.Text = this.dto.entryEntity.MITSUMORI_5.ToString();
                }
                else
                {
                    this.form.txt_MitsumoriNaiyo5.Text = string.Empty;
                }

                // 取引先フラグ
                if (!this.dto.entryEntity.HIKIAI_TORIHIKISAKI_FLG.IsNull && (Boolean)this.dto.entryEntity.HIKIAI_TORIHIKISAKI_FLG)
                {
                    this.form.txt_Torihikisaki_hikiai_flg.Text = "1";
                }
                else if (!this.dto.entryEntity.HIKIAI_TORIHIKISAKI_FLG.IsNull && !(Boolean)this.dto.entryEntity.HIKIAI_TORIHIKISAKI_FLG)
                {
                    this.form.txt_Torihikisaki_hikiai_flg.Text = "0";
                }

                // 業者フラグ
                if (!this.dto.entryEntity.HIKIAI_GYOUSHA_FLG.IsNull && (Boolean)this.dto.entryEntity.HIKIAI_GYOUSHA_FLG)
                {
                    this.form.txt_Gyousha_hikiai_flg.Text = "1";
                }
                else if (!this.dto.entryEntity.HIKIAI_GYOUSHA_FLG.IsNull && !(Boolean)this.dto.entryEntity.HIKIAI_GYOUSHA_FLG)
                {
                    this.form.txt_Gyousha_hikiai_flg.Text = "0";
                }

                // 現場フラグ
                if (!this.dto.entryEntity.HIKIAI_GENBA_FLG.IsNull && (Boolean)this.dto.entryEntity.HIKIAI_GENBA_FLG)
                {
                    this.form.txt_Genba_hikiai_flg.Text = "1";
                }
                else if (!this.dto.entryEntity.HIKIAI_GENBA_FLG.IsNull && !(Boolean)this.dto.entryEntity.HIKIAI_GENBA_FLG)
                {
                    this.form.txt_Genba_hikiai_flg.Text = "0";
                }

                // 進行中日付
                if (this.dto.entryEntity.SINKOU_DATE != null)
                {
                    this.form.dtp_ShinkouDate.Value = this.dto.entryEntity.SINKOU_DATE;
                }
                // 受注日付
                if (this.dto.entryEntity.JUCHU_DATE != null && !this.form.copyDataFlg)
                {
                    this.form.dtp_JuchuDate.Value = this.dto.entryEntity.JUCHU_DATE;
                }
                // 失注日付
                if (this.dto.entryEntity.SICHU_DATE != null && !this.form.copyDataFlg)
                {
                    this.form.dtp_SichuDate.Value = this.dto.entryEntity.SICHU_DATE.ToString();
                }
                // 社内備考タブ
                if (this.dto.entryEntity.SHANAI_BIKOU != null && !string.IsNullOrEmpty(this.dto.entryEntity.SHANAI_BIKOU.ToString()))
                {
                    this.form.txt_ShanaiBikou.Text = this.dto.entryEntity.SHANAI_BIKOU.ToString();
                }

                // 状況チェックボックス
                if (!this.dto.entryEntity.JOKYO_FLG.IsNull && !string.IsNullOrEmpty(this.dto.entryEntity.JOKYO_FLG.ToString())
                     && !this.form.copyDataFlg)
                {
                    this.form.txt_JykyoFlg.Text = this.dto.entryEntity.JOKYO_FLG.ToString();
                }

                // 税計算区分チェックボックス
                if (!this.dto.entryEntity.ZEI_KEISAN_KBN_CD.IsNull && !string.IsNullOrEmpty(this.dto.entryEntity.ZEI_KEISAN_KBN_CD.ToString()))
                {
                    this.form.txt_ZeiKeisanKbnCD.Text = this.dto.entryEntity.ZEI_KEISAN_KBN_CD.ToString();
                }

                // 税区分チェックボックス
                if (!this.dto.entryEntity.ZEI_KBN_CD.IsNull && !string.IsNullOrEmpty(this.dto.entryEntity.ZEI_KBN_CD.ToString()))
                {
                    this.form.txt_ZeiKbnCD.Text = this.dto.entryEntity.ZEI_KBN_CD.ToString();
                }

                //20250414
                if (!this.dto.entryEntity.BIKO_KBN_CD.IsNull && !string.IsNullOrEmpty(this.dto.entryEntity.BIKO_KBN_CD.ToString()))
                {
                    this.form.BIKO_KBN_CD.Text = this.dto.entryEntity.BIKO_KBN_CD.ToString();

                    //20250418
                    this.beforeBikoCd = this.dto.entryEntity.BIKO_KBN_CD.ToString();
                }

                if (!this.dto.entryEntity.BIKO_NAME_RYAKU.IsNull && !string.IsNullOrEmpty(this.dto.entryEntity.BIKO_NAME_RYAKU.ToString()))
                {
                    this.form.BIKO_NAME_RYAKU.Text = this.dto.entryEntity.BIKO_NAME_RYAKU.ToString();

                    //20250418
                    this.beforeBikoName = this.dto.entryEntity.BIKO_NAME_RYAKU.ToString();
                }

                // 見積書種類表示
                this.form.formParem.cyohyoType = int.Parse(this.dto.entryEntity.MITSUMORI_SHOSHIKI_KBN.ToString());

                // 見積書種類CDによって見積書種類名称を設定
                this.ChangeMitsumoriSyuruiName();

                if (this.form.formParem.cyohyoType == MitumorisyoConst.MITUMOTISYO_TANKA_YOKO)
                {
                    this.form.MITSUMORISYURUI_NAME.Text = MitumorisyoConst.MITUMOTISYO_TANKA_YOKO_NAME;
                }
                else if (this.form.formParem.cyohyoType == MitumorisyoConst.MITUMOTISYO_TANKA_TATE)
                {
                    this.form.MITSUMORISYURUI_NAME.Text = MitumorisyoConst.MITUMOTISYO_TANKA_TATE_NAME;
                }
                else if (this.form.formParem.cyohyoType == MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO)
                {
                    this.form.MITSUMORISYURUI_NAME.Text = MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO_NAME;
                }
                else if (this.form.formParem.cyohyoType == MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE)
                {
                    this.form.MITSUMORISYURUI_NAME.Text = MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE_NAME;
                }
                else
                {
                    this.form.MITSUMORISYURUI_NAME.Text = string.Empty;
                }

                // 見積印字チェックボックス
                if (!this.dto.entryEntity.MITSUMORI_INJI_DATE.IsNull && this.dto.entryEntity.MITSUMORI_INJI_DATE)
                {
                    this.form.txt_MitsumoriInjiDate.Text = "1";
                }
                else
                {
                    this.form.txt_MitsumoriInjiDate.Text = "2";
                }
                // 部署名印字ラジオボタン
                if (!this.dto.entryEntity.BUSHO_NAME_INJI.IsNull)
                {
                    if (this.dto.entryEntity.BUSHO_NAME_INJI)
                    {
                        this.form.txt_BushouNameInji.Text = "1";
                    }
                    else
                    {
                        this.form.txt_BushouNameInji.Text = "2";
                    }
                }
                else
                {
                    if (!this.dto.sysInfoEntity.BUSHO_NAME_PRINT.IsNull)
                    {
                        this.form.txt_BushouNameInji.Text = this.dto.sysInfoEntity.BUSHO_NAME_PRINT.ToString();
                    }
                    else
                    {
                        this.form.txt_BushouNameInji.Text = "1";
                    }
                }

                // 金額計
                if (!this.dto.entryEntity.KINGAKU_TOTAL.IsNull && !string.IsNullOrEmpty(this.dto.entryEntity.KINGAKU_TOTAL.ToString()))
                {
                    this.form.txt_KingakuTotal.Text = CommonCalc.DecimalFormat(decimal.Parse(this.dto.entryEntity.KINGAKU_TOTAL.ToString().Replace(",", string.Empty)));
                }
                // 消費税
                if (!this.dto.entryEntity.ZEI_KEISAN_KBN_CD.IsNull && !string.IsNullOrEmpty(this.dto.entryEntity.ZEI_KEISAN_KBN_CD.ToString()))
                {
                    if (this.dto.entryEntity.ZEI_KEISAN_KBN_CD.ToString() == "1")
                    {
                        if (!this.dto.entryEntity.TAX_SOTO.IsNull)
                        {
                            this.form.txt_TaxSoto.Text = CommonCalc.DecimalFormat(decimal.Parse(this.dto.entryEntity.TAX_SOTO.ToString().Replace(",", string.Empty)));
                        }
                    }
                    else
                    {
                        if (!this.dto.entryEntity.TAX_SOTO_TOTAL.IsNull)
                        {
                            this.form.txt_TaxSoto.Text = CommonCalc.DecimalFormat(decimal.Parse(this.dto.entryEntity.TAX_SOTO_TOTAL.ToString().Replace(",", string.Empty)));
                        }
                    }
                }

                // 2014007011 chinchisi EV002428_内税の計算を修正する。　start (バグです)
                // 課税対象額
                if (!this.dto.entryEntity.ZEI_KEISAN_KBN_CD.IsNull && !string.IsNullOrEmpty(this.dto.entryEntity.ZEI_KEISAN_KBN_CD.ToString()))
                {
                    if (this.dto.entryEntity.ZEI_KEISAN_KBN_CD.ToString() == "1")
                    {
                        if (!this.dto.entryEntity.TAX_UCHI.IsNull)
                        {
                            //this.form.txt_KazeiTaisyoGaku.Text = CommonCalc.DecimalFormat(decimal.Parse(this.dto.entryEntity.TAX_UCHI.ToString().Replace(",", string.Empty)));
                            this.form.txt_UchiTotal.Text = CommonCalc.DecimalFormat(decimal.Parse(this.dto.entryEntity.TAX_UCHI.ToString().Replace(",", string.Empty)));
                        }
                        if (!this.dto.entryEntity.KINGAKU_TOTAL.IsNull)
                        {
                            this.form.txt_KazeiTaisyoGaku.Text = CommonCalc.DecimalFormat(decimal.Parse(this.dto.entryEntity.KINGAKU_TOTAL.ToString().Replace(",", string.Empty)) - decimal.Parse(this.dto.entryEntity.TAX_UCHI.ToString().Replace(",", string.Empty)));
                        }
                    }
                    else
                    {
                        if (!this.dto.entryEntity.TAX_UCHI_TOTAL.IsNull)
                        {
                            //this.form.txt_KazeiTaisyoGaku.Text = CommonCalc.DecimalFormat(decimal.Parse(this.dto.entryEntity.TAX_UCHI_TOTAL.ToString().Replace(",", string.Empty)));
                            this.form.txt_UchiTotal.Text = CommonCalc.DecimalFormat(decimal.Parse(this.dto.entryEntity.TAX_UCHI.ToString().Replace(",", string.Empty)));
                        }
                        if (!this.dto.entryEntity.KINGAKU_TOTAL.IsNull)
                        {
                            this.form.txt_KazeiTaisyoGaku.Text = CommonCalc.DecimalFormat(decimal.Parse(this.dto.entryEntity.KINGAKU_TOTAL.ToString().Replace(",", string.Empty)) - decimal.Parse(this.dto.entryEntity.TAX_UCHI.ToString().Replace(",", string.Empty)));
                        }
                    }
                }
                // 2014007011 chinchisi EV002428_内税の計算を修正する。　end

                // 総合計
                if (!this.dto.entryEntity.GOUKEI_KINGAKU_TOTAL.IsNull && !string.IsNullOrEmpty(this.dto.entryEntity.GOUKEI_KINGAKU_TOTAL.ToString()))
                {
                    this.form.txt_GoukeiKingakuTotal.Text = CommonCalc.DecimalFormat(decimal.Parse(this.dto.entryEntity.GOUKEI_KINGAKU_TOTAL.ToString().Replace(",", string.Empty)));
                }

                #region 20250411

                // 備考１～５設定
                //if ((this.dto.entryEntity.BIKOU_1 != null && !string.IsNullOrEmpty(this.dto.entryEntity.BIKOU_1.ToString())))
                //{
                //    this.form.txt_Bikou1.Text = this.dto.entryEntity.BIKOU_1.ToString();
                //}
                //else
                //{
                //    this.form.txt_Bikou1.Text = string.Empty;
                //}
                //if ((this.dto.entryEntity.BIKOU_2 != null && !string.IsNullOrEmpty(this.dto.entryEntity.BIKOU_2.ToString())))
                //{
                //    this.form.txt_Bikou2.Text = this.dto.entryEntity.BIKOU_2.ToString();
                //}
                //else
                //{
                //    this.form.txt_Bikou2.Text = string.Empty;
                //}
                //if ((this.dto.entryEntity.BIKOU_3 != null && !string.IsNullOrEmpty(this.dto.entryEntity.BIKOU_3.ToString())))
                //{
                //    this.form.txt_Bikou3.Text = this.dto.entryEntity.BIKOU_3.ToString();
                //}
                //else
                //{
                //    this.form.txt_Bikou3.Text = string.Empty;
                //}
                //if ((this.dto.entryEntity.BIKOU_4 != null && !string.IsNullOrEmpty(this.dto.entryEntity.BIKOU_4.ToString())))
                //{
                //    this.form.txt_Bikou4.Text = this.dto.entryEntity.BIKOU_4.ToString();
                //}
                //else
                //{
                //    this.form.txt_Bikou4.Text = string.Empty;
                //}
                //if ((this.dto.entryEntity.BIKOU_5 != null && !string.IsNullOrEmpty(this.dto.entryEntity.BIKOU_5.ToString())))
                //{
                //    this.form.txt_Bikou5.Text = this.dto.entryEntity.BIKOU_5.ToString();
                //}
                //else
                //{
                //    this.form.txt_Bikou5.Text = string.Empty;
                //}

                #endregion 20250411

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
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 見積明細入力データ取得処理
        /// </summary>
        private void WindowsSetDetailData()
        {
            LogUtility.DebugMethodStart();

            try
            {
                // CellValueChangedが発生を行わないため
                this.form.meisaiNowLoding = true;

                // tabPageをすべてクリア
                this.form.tbc_MitsumoriMeisai.TabPages.Clear();

                // 新規ページ追加処理
                SqlInt16 totalPageNo = 0;

                // rowを取得
                SqlInt16 intRow = 0;

                // 総ページ番号を取得
                totalPageNo = this.dto.entryEntity.PEGE_TOTAL;
                for (int pageNo = 0; pageNo < totalPageNo.Value; pageNo++)
                {
                    // ページを追加
                    this.form.PageAdd();

                    // CustomDataGridViewを取得
                    CustomDataGridView cdgv = ((CustomDataGridView)this.form.tbc_MitsumoriMeisai.TabPages[pageNo].Controls["control"].Controls["CustomDataGridView"]);

                    //((System.ComponentModel.ISupportInitialize)(cdgv)).BeginInit();
                    //cdgv.SuspendLayout();

                    // ページ行初期化
                    intRow = 0;

                    // CreateDataTableForEntityがMultiRowを動的に作成しないため、ここでEntity分行数を追加する
                    // Entity数Rowを作ると最終行が無いので、Etity + 1でループさせる
                    for (int row = 0; row < this.dto.detailEntity.Length; row++)
                    {
                        // ページ番号を取得
                        SqlInt16 pageNumber = this.dto.detailEntity[row].PAGE_NUMBER;

                        if (pageNumber == SqlInt16.Parse((pageNo + 1).ToString()))
                        {
                            // 行を追加
                            cdgv.Rows.Add();

                            // システムID
                            cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_SYS_ID].Value = this.dto.detailEntity[row].DETAIL_SYSTEM_ID;

                            // 行番号
                            cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_ROW_NO].Value = this.dto.detailEntity[row].ROW_NO;

                            // 伝票番号
                            if (!this.dto.detailEntity[row].DENPYOU_NUMBER.IsNull)
                            {
                                cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_NO].Value = this.dto.detailEntity[row].DENPYOU_NUMBER;
                            }
                            else
                            {
                                cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_NO].Value = string.Empty;
                            }

                            // 小計ﾌﾗｸﾞ
                            if (!this.dto.detailEntity[row].SHOUKEI_FLG.IsNull && this.dto.detailEntity[row].SHOUKEI_FLG)
                            {
                                cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SHOUKEI_FLG].Value = this.dto.detailEntity[row].SHOUKEI_FLG;

                                //　小計行を入力禁止
                                this.SubTotalRowReadOnly(intRow.Value);
                            }
                            else
                            {
                                cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SHOUKEI_FLG].Value = "False";
                            }
                            // 品名CD
                            if (!string.IsNullOrEmpty(this.dto.detailEntity[row].HINMEI_CD))
                            {
                                cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value = this.dto.detailEntity[row].HINMEI_CD;
                            }
                            else
                            {
                                cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value = string.Empty;
                            }
                            // 品名
                            if (!string.IsNullOrEmpty(this.dto.detailEntity[row].HINMEI_NAME))
                            {
                                cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME].Value = this.dto.detailEntity[row].HINMEI_NAME;
                            }

                            // 単位CD
                            if (!this.dto.detailEntity[row].UNIT_CD.IsNull)
                            {
                                cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD].Value = this.dto.detailEntity[row].UNIT_CD.ToString();
                                // 単位名
                                cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_NAME].Value = this.GetUnitName((short)this.dto.detailEntity[row].UNIT_CD);
                            }

                            // 数量
                            if (!this.dto.detailEntity[row].SUURYOU.IsNull)
                            {
                                cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SUURYOU].Value = decimal.Parse(this.dto.detailEntity[row].SUURYOU.ToString().Replace(",", string.Empty)).ToString(SystemProperty.Format.Suuryou);
                            }

                            // 単価
                            if (!this.dto.detailEntity[row].TANKA.IsNull)
                            {
                                cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value = decimal.Parse(this.dto.detailEntity[row].TANKA.ToString().Replace(",", string.Empty)).ToString(SystemProperty.Format.Tanka);
                            }

                            // 小計ﾌﾗｸﾞ
                            if (!this.dto.detailEntity[row].SHOUKEI_FLG.IsNull && this.dto.detailEntity[row].SHOUKEI_FLG)
                            {
                                // 20140716 syunrei EV005272_ページ６に小計を挿入し登録後、修正モードにて開くとシステムエラー(発生時間18時47分)　start
                                // 金額
                                //cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value = CommonCalc.DecimalFormat(decimal.Parse(this.dto.detailEntity[row].KINGAKU.ToString().Replace(",", string.Empty)));
                                if (!this.dto.detailEntity[row].KINGAKU.IsNull)
                                {
                                    cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value = CommonCalc.DecimalFormat(decimal.Parse(this.dto.detailEntity[row].KINGAKU.ToString().Replace(",", string.Empty)));
                                }
                                // 20140716 syunrei EV005272_ページ６に小計を挿入し登録後、修正モードにて開くとシステムエラー(発生時間18時47分)　end
                                // 外税
                                if (!this.dto.detailEntity[row].TAX_SOTO.IsNull)
                                {
                                    cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat(decimal.Parse(this.dto.detailEntity[row].TAX_SOTO.ToString().Replace(",", string.Empty)));
                                }
                                // 内税
                                if (!this.dto.detailEntity[row].TAX_UCHI.IsNull)
                                {
                                    cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value = CommonCalc.DecimalFormat(decimal.Parse(this.dto.detailEntity[row].TAX_UCHI.ToString().Replace(",", string.Empty)));
                                }
                            }
                            else
                            {
                                // 画面の税区分
                                DataGridViewComboBoxCell cbc = (DataGridViewComboBoxCell)cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD];
                                // 税区分設定
                                SqlInt16 zeikbn = 0;

                                zeikbn = this.dto.detailEntity[row].HINMEI_ZEI_KBN_CD;

                                zeikbn = zeikbn.IsNull ? 0 : zeikbn;

                                cbc.Value = "0".Equals(zeikbn.Value.ToString()) ? String.Empty : cbc.Items[int.Parse(zeikbn.Value.ToString()) - 1];

                                // 品名税区分
                                SqlInt16 hinmeiZeikbn = 0;
                                if (string.IsNullOrEmpty(this.dto.detailEntity[row].HINMEI_CD))
                                {
                                    hinmeiZeikbn = SqlInt16.Null;
                                }
                                else
                                {
                                    hinmeiZeikbn = this.accessor.GetHinmeiDataByCd(this.dto.detailEntity[row].HINMEI_CD.ToString()).ZEI_KBN_CD;
                                }
                                hinmeiZeikbn = hinmeiZeikbn.IsNull ? 0 : hinmeiZeikbn;

                                this.dto.detailEntity[row].KINGAKU = this.dto.detailEntity[row].KINGAKU.IsNull ? 0 : this.dto.detailEntity[row].KINGAKU;
                                this.dto.detailEntity[row].HINMEI_KINGAKU = this.dto.detailEntity[row].HINMEI_KINGAKU.IsNull ? 0 : this.dto.detailEntity[row].HINMEI_KINGAKU;

                                // 品名金額もしくは金額、値が登録されている方を採用する
                                if (this.dto.detailEntity[row].HINMEI_KINGAKU.Value != 0)
                                {
                                    // 金額
                                    cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value = CommonCalc.DecimalFormat(decimal.Parse(this.dto.detailEntity[row].HINMEI_KINGAKU.ToString().Replace(",", string.Empty)));

                                    // 外税
                                    if (!this.dto.detailEntity[row].HINMEI_TAX_SOTO.IsNull)
                                    {
                                        cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat(decimal.Parse(this.dto.detailEntity[row].HINMEI_TAX_SOTO.ToString().Replace(",", string.Empty)));
                                    }
                                    // 内税
                                    if (!this.dto.detailEntity[row].HINMEI_TAX_UCHI.IsNull)
                                    {
                                        cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value = CommonCalc.DecimalFormat(decimal.Parse(this.dto.detailEntity[row].HINMEI_TAX_UCHI.ToString().Replace(",", string.Empty)));
                                    }
                                }
                                else
                                {
                                    // 金額
                                    cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value = CommonCalc.DecimalFormat(decimal.Parse(this.dto.detailEntity[row].KINGAKU.ToString().Replace(",", string.Empty)));

                                    // 外税
                                    if (!this.dto.detailEntity[row].TAX_SOTO.IsNull)
                                    {
                                        cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat(decimal.Parse(this.dto.detailEntity[row].TAX_SOTO.ToString().Replace(",", string.Empty)));
                                    }
                                    // 内税
                                    if (!this.dto.detailEntity[row].TAX_UCHI.IsNull)
                                    {
                                        cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value = CommonCalc.DecimalFormat(decimal.Parse(this.dto.detailEntity[row].TAX_UCHI.ToString().Replace(",", string.Empty)));
                                    }
                                }
                            }

                            // 明細備考
                            cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_MEISAI_BIKOU].Value = this.dto.detailEntity[row].MEISAI_BIKOU;
                            // 明細摘要
                            cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_MEISAI_TEKIYOU].Value = this.dto.detailEntity[row].MEISAI_TEKIYO;
                            // 伝票区分CD
                            if (!this.dto.detailEntity[row].DENPYOU_KBN_CD.IsNull)
                            {
                                cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value = this.dto.detailEntity[row].DENPYOU_KBN_CD;
                                // 伝票区分名
                                cdgv.Rows[intRow.Value].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME].Value = this.GetDataByCodeForDenpyouKbnName((short)this.dto.detailEntity[row].DENPYOU_KBN_CD);
                            }

                            // 加算
                            intRow = intRow + 1;
                        }
                    }

                    //this.Controls.Add(cdgv);

                    //((System.ComponentModel.ISupportInitialize)(cdgv)).EndInit();
                    //cdgv.ResumeLayout(false);
                }

                //20250416
                for (int row = 0; row < this.dto.detailEntity_2.Length; row++)
                {
                    var bdgv = this.form.Ichiran;

                    bdgv.Rows.Add();

                    if (!string.IsNullOrEmpty(this.dto.detailEntity_2[row].BIKO_CD))
                    {
                        bdgv.Rows[row].Cells[MitumorisyoConst.BIKO_COLUMN_NAME_BIKO_CD].Value = this.dto.detailEntity_2[row].BIKO_CD;
                    }
                    else
                    {
                        bdgv.Rows[row].Cells[MitumorisyoConst.BIKO_COLUMN_NAME_BIKO_CD].Value = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(this.dto.detailEntity_2[row].BIKO_NOTE))
                    {
                        bdgv.Rows[row].Cells[MitumorisyoConst.BIKO_COLUMN_NAME_BIKO_NOTE].Value = this.dto.detailEntity_2[row].BIKO_NOTE;
                    }
                    else
                    {
                        bdgv.Rows[row].Cells[MitumorisyoConst.BIKO_COLUMN_NAME_BIKO_NOTE].Value = this.dto.detailEntity_2[row].BIKO_NOTE;
                    }
                }

                // CellValueChangedが発生を行わないため
                this.form.meisaiNowLoding = false;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            finally
            {
                // CellValueChangedが発生を行わないため
                this.form.meisaiNowLoding = false;
                LogUtility.DebugMethodEnd();
            }
        }

        #region 見積日付ロストフォーカスイベント

        /// <summary>
        ///  見積日付ロストフォーカスイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dtp_MitsumoriDate_LostFocus(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            getShohizeData();

            LogUtility.DebugMethodEnd();
        }

        #endregion 見積日付ロストフォーカスイベント

        #region 見積日付テキストチェンジイベント

        /// <summary>
        /// 見積日付テキストチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dtp_MitsumoriDate_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 消費税を取得
            getShohizeData();

            LogUtility.DebugMethodEnd();
        }

        #endregion 見積日付テキストチェンジイベント

        #region 消費税取得

        /// <summary>
        /// 消費税取得
        /// </summary>
        private void getShohizeData()
        {
            LogUtility.DebugMethodStart();

            // 消費税率取得
            this.taxRate = this.GetShouhizeiRate(this.form.dtp_MitsumoriDate.Value);

            LogUtility.DebugMethodEnd();
        }

        #endregion 消費税取得

        #endregion ボタンの初期化

        #endregion 初期化処理

        #region 業務処理

        #region Entity作成と登録処理

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
                // CreateEntityとそれぞれの更新処理でDB更新が発生するため、UIFormから
                // 排他制御する
                using (Transaction tran = new Transaction())
                {
                    // 取引先マスタ検索
                    bool catchErr = false;
                    var isLostCheck = this.Torihikisaki_GyoushaCD_GenbaCD_LostFocus(this.form.txt_TorihikisakiCD.Text, this.form.txt_GyoushaCD.Text, this.form.txt_GenbaCD.Text, out catchErr);
                    if (catchErr || !isLostCheck)
                    {
                        return false;
                    }
                    // №5235 登録時のシステムエラー回避のため
                    //// 引合取引先、引合業者、引合現場登録処理
                    //if (!this.RegistTorihikisakiGyoushaGenba(errorFlag))
                    //{
                    //    return false;
                    //}
                    // №5235 登録時のシステムエラー回避のため

                    // 見積登録処理
                    switch (this.form.WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            this.CreateEntity(taiyuuKbn);
                            this.Regist(errorFlag);
                            break;

                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            this.CreateEntity(taiyuuKbn);
                            this.Update(errorFlag);
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

                    // メッセージ出力
                    // ※トランザクション処理中にメッセージボックスを表示しない
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    switch (this.form.WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            msgLogic.MessageBoxShow("I001", "登録");
                            break;

                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            msgLogic.MessageBoxShow("I001", "更新");
                            break;

                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            msgLogic.MessageBoxShow("I001", "削除");
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityAndUpdateTables", ex);

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

        #endregion Entity作成と登録処理

        #region コントロールから登録用のEntityを作成する

        /// <summary>
        /// コントロールから登録用のEntityを作成する
        /// </summary>
        public virtual void CreateEntity(bool tairyuuKbnFlag)
        {
            LogUtility.DebugMethodStart(tairyuuKbnFlag);

            // MisumorilEntityを作成
            this.CreateMitsumoriEntity(tairyuuKbnFlag);

            // DetailEntityを作成
            this.CreateMitsumoriDetailEntity();

            //20250416
            this.CreateBikoDetailEntity();

            LogUtility.DebugMethodEnd();
        }

        #endregion コントロールから登録用のEntityを作成する

        #region コントロールから登録用のMitsumoriEntityを作成する

        /// <summary>
        /// コントロールから登録用のMitsumoriEntityを作成する
        /// </summary>
        public virtual void CreateMitsumoriEntity(bool tairyuuKbnFlag)
        {
            LogUtility.DebugMethodStart(tairyuuKbnFlag);

            if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
            {
                this.dto.entryEntity = new T_MITSUMORI_ENTRY();
            }

            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:

                    // SYSTEM_IDの採番
                    SqlInt64 systemId = 0;
                    if (tairyuuKbnFlag)
                    {
                        //見積番号
                        if (!string.IsNullOrEmpty(this.form.txt_MitsumoriNumber.Text))
                        {
                            this.dto.entryEntity.MITSUMORI_NUMBER = SqlInt64.Parse(this.form.txt_MitsumoriNumber.Text);
                        }
                    }
                    else
                    {
                        // SYSTEM_IDの採番
                        systemId = this.accessor.createSystemIdForMitsumori();
                        this.dto.entryEntity.SYSTEM_ID = systemId;

                        // 見積番号の採番
                        this.dto.entryEntity.MITSUMORI_NUMBER = this.accessor.createMitsumoriNumber();

                        this.form.txt_MitsumoriNumber.Text = this.dto.entryEntity.MITSUMORI_NUMBER.ToString();
                    }

                    // 枝番
                    this.dto.entryEntity.SEQ = 1;
                    this.dto.entryEntity.DELETE_FLG = false;

                    break;

                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    // 画面表示時にSYSTEM_IDを取得しているため採番は割愛
                    // システムID
                    this.dto.entryEntity.SYSTEM_ID = this.beforDto.entryEntity.SYSTEM_ID;
                    // 枝番
                    this.dto.entryEntity.SEQ = this.beforDto.entryEntity.SEQ + 1;

                    // 見積番号
                    this.dto.entryEntity.MITSUMORI_NUMBER = this.beforDto.entryEntity.MITSUMORI_NUMBER;

                    this.dto.entryEntity.DELETE_FLG = false;
                    // 更新前伝票は論理削除
                    this.beforDto.entryEntity.DELETE_FLG = true;

                    break;

                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    this.dto.entryEntity.DELETE_FLG = true;
                    this.dto.entryEntity.TIME_STAMP = this.beforDto.entryEntity.TIME_STAMP;
                    break;

                default:
                    break;
            }

            // 拠点CD
            if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
            {
                this.dto.entryEntity.KYOTEN_CD = SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text);
            }

            // 見積書式区分
            if (!string.IsNullOrEmpty(this.form.formParem.cyohyoType.ToString()))
            {
                this.dto.entryEntity.MITSUMORI_SHOSHIKI_KBN = SqlInt16.Parse(this.form.formParem.cyohyoType.ToString());
            }

            // 総ページ数
            if (this.form.tbc_MitsumoriMeisai.TabPages.Count > 0)
            {
                this.dto.entryEntity.PEGE_TOTAL = SqlInt16.Parse((this.form.tbc_MitsumoriMeisai.TabPages.Count.ToString()));
            }

            //状況チェックボックス
            if (!string.IsNullOrEmpty(this.form.txt_JykyoFlg.Text))
            {
                this.dto.entryEntity.JOKYO_FLG = SqlInt16.Parse(this.form.txt_JykyoFlg.Text);
            }
            else
            {
                this.dto.entryEntity.JOKYO_FLG = SqlInt16.Null;
            }

            // 見積日付
            if (this.form.dtp_MitsumoriDate.Value != null && !string.IsNullOrEmpty((this.form.dtp_MitsumoriDate.Value).ToString().Trim()))
            {
                this.dto.entryEntity.MITSUMORI_DATE = this.form.dtp_MitsumoriDate.Value.ToString();
            }
            // 印字拠点１
            if (!string.IsNullOrEmpty(this.form.txt_InjiKyoten1CD.Text))
            {
                this.dto.entryEntity.INJI_KYOTEN1_CD = SqlInt16.Parse(this.form.txt_InjiKyoten1CD.Text);
            }
            else
            {
                this.dto.entryEntity.INJI_KYOTEN1_CD = SqlInt16.Null;
            }
            // 印字拠点２
            if (!string.IsNullOrEmpty(this.form.txt_InjiKyoten2CD.Text))
            {
                this.dto.entryEntity.INJI_KYOTEN2_CD = SqlInt16.Parse(this.form.txt_InjiKyoten2CD.Text);
            }
            else
            {
                this.dto.entryEntity.INJI_KYOTEN2_CD = SqlInt16.Null;
            }
            // 引合取引先ﾌﾗｸﾞ
            if (!string.IsNullOrEmpty(this.form.txt_Torihikisaki_hikiai_flg.Text))
            {
                this.dto.entryEntity.HIKIAI_TORIHIKISAKI_FLG = SqlBoolean.Parse(this.form.txt_Torihikisaki_hikiai_flg.Text);
            }
            else
            {
                this.dto.entryEntity.HIKIAI_TORIHIKISAKI_FLG = false;
            }

            // 取引先CD
            switch (HIKIAI_TORIHIKISAKI_WindowType)
            {
                // №5235 システムエラー回避のため
                //case HIKIAI_TORIHIKISAKI.NEW_WINDOW_FLAG:
                //
                //    this.dto.entryEntity.TORIHIKISAKI_CD = this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_CD;
                //    break;
                // №5235 システムエラー回避のため
                default:
                    if (!string.IsNullOrEmpty(this.form.txt_TorihikisakiCD.Text))
                    {
                        this.dto.entryEntity.TORIHIKISAKI_CD = this.form.txt_TorihikisakiCD.Text;
                    }
                    break;
            }

            // 取引先名
            if (!string.IsNullOrEmpty(this.form.txt_TorihikisakiMei.Text))
            {
                this.dto.entryEntity.TORIHIKISAKI_NAME = this.form.txt_TorihikisakiMei.Text;
            }
            // 取引先印字ﾌﾗｸﾞ
            this.dto.entryEntity.TORIHIKISAKI_INJI = this.form.chk_TorihikisakiInji.Checked;

            //引合業者ﾌﾗｸﾞ
            if (!string.IsNullOrEmpty(this.form.txt_Gyousha_hikiai_flg.Text))
            {
                this.dto.entryEntity.HIKIAI_GYOUSHA_FLG = SqlBoolean.Parse(this.form.txt_Gyousha_hikiai_flg.Text);
            }
            else
            {
                this.dto.entryEntity.HIKIAI_GYOUSHA_FLG = false;
            }

            //業者CD
            switch (HIKIAI_GYOUSHA_WindowType)
            {
                // №5235 システムエラー回避のため
                //case HIKIAI_GYOUSHA.NEW_WINDOW_FLAG:
                //    this.dto.entryEntity.GYOUSHA_CD = this.dto.hikiaiGyoushaEntry.GYOUSHA_CD;
                //    break;
                // №5235 システムエラー回避のため
                default:
                    if (!string.IsNullOrEmpty(this.form.txt_GyoushaCD.Text))
                    {
                        this.dto.entryEntity.GYOUSHA_CD = this.form.txt_GyoushaCD.Text;
                    }
                    else
                    {
                        this.dto.entryEntity.GYOUSHA_CD = string.Empty;
                    }
                    break;
            }

            // 業者名
            if (!string.IsNullOrEmpty(this.form.txt_GyoushaName.Text))
            {
                this.dto.entryEntity.GYOUSHA_NAME = this.form.txt_GyoushaName.Text;
            }
            else
            {
                this.dto.entryEntity.GYOUSHA_NAME = string.Empty;
            }
            // 業者印字ﾌﾗｸﾞ
            this.dto.entryEntity.GYOUSHA_INJI = this.form.chk_GyushaInji.Checked;

            // 引合現場ﾌﾗｸﾞ
            if (!string.IsNullOrEmpty(this.form.txt_Genba_hikiai_flg.Text))
            {
                this.dto.entryEntity.HIKIAI_GENBA_FLG = SqlBoolean.Parse(this.form.txt_Genba_hikiai_flg.Text);
            }
            else
            {
                this.dto.entryEntity.HIKIAI_GENBA_FLG = false;
            }

            // 現場CD
            switch (HIKIAI_GENBA_WindowType)
            {
                // №5235 システムエラー回避のため
                //case HIKIAI_GENBA.NEW_WINDOW_FLAG:
                //    this.dto.entryEntity.GENBA_CD = this.dto.hikiaiGenbaEntry.GENBA_CD;
                //    break;
                // №5235 システムエラー回避のため
                default:

                    if (!string.IsNullOrEmpty(this.form.txt_GenbaCD.Text))
                    {
                        this.dto.entryEntity.GENBA_CD = this.form.txt_GenbaCD.Text;
                    }
                    else
                    {
                        this.dto.entryEntity.GENBA_CD = string.Empty;
                    }

                    break;
            }

            // 現場名
            if (!string.IsNullOrEmpty(this.form.txtGenbaName.Text))
            {
                this.dto.entryEntity.GENBA_NAME = this.form.txtGenbaName.Text;
            }
            else
            {
                this.dto.entryEntity.GENBA_NAME = string.Empty;
            }
            // 現場印字ﾌﾗｸﾞ
            this.dto.entryEntity.GENBA_INJI = this.form.chk_GenbaInji.Checked;

            // 営業者CD
            if (!string.IsNullOrEmpty(this.form.txt_ShainCD.Text))
            {
                this.dto.entryEntity.SHAIN_CD = this.form.txt_ShainCD.Text;
            }
            else
            {
                this.dto.entryEntity.SHAIN_CD = string.Empty;
            }
            // 営業者名
            if (!string.IsNullOrEmpty(this.form.txt_ShainNameRyaku.Text))
            {
                this.dto.entryEntity.SHAIN_NAME = this.form.txt_ShainNameRyaku.Text;
            }
            else
            {
                this.dto.entryEntity.SHAIN_NAME = string.Empty;
            }
            // 取引先敬称
            this.dto.entryEntity.TORIHIKISAKI_KEISHOU = this.form.lb_TorihikisakiKeishou.Text;
            // 業者敬称
            this.dto.entryEntity.GYOUSHA_KEISHOU = this.form.lb_GyoushaKeishou.Text;
            // 現場敬称
            this.dto.entryEntity.GENBA_KEISHOU = this.form.lb_GenbaKeishou.Text;
            // 件名
            if (!string.IsNullOrEmpty(this.form.txt_Kenmei.Text))
            {
                this.dto.entryEntity.KENMEI = this.form.txt_Kenmei.Text;
            }
            else
            {
                this.dto.entryEntity.KENMEI = string.Empty;
            }

            //20250415
            if (!string.IsNullOrEmpty(this.form.txt_Kenmei2.Text))
            {
                this.dto.entryEntity.KENMEI_2 = this.form.txt_Kenmei2.Text;
            }
            else
            {
                this.dto.entryEntity.KENMEI_2 = string.Empty;
            }

            // 見積項目1
            if (!string.IsNullOrEmpty(this.form.txt_MitsumoriNaiyo1.Text))
            {
                this.dto.entryEntity.MITSUMORI_1 = this.form.txt_MitsumoriNaiyo1.Text;
            }
            else
            {
                this.dto.entryEntity.MITSUMORI_1 = string.Empty;
            }
            // 見積項目2
            if (!string.IsNullOrEmpty(this.form.txt_MitsumoriNaiyo2.Text))
            {
                this.dto.entryEntity.MITSUMORI_2 = this.form.txt_MitsumoriNaiyo2.Text;
            }
            else
            {
                this.dto.entryEntity.MITSUMORI_2 = string.Empty;
            }
            // 見積項目3
            if (!string.IsNullOrEmpty(this.form.txt_MitsumoriNaiyo3.Text))
            {
                this.dto.entryEntity.MITSUMORI_3 = this.form.txt_MitsumoriNaiyo3.Text;
            }
            else
            {
                this.dto.entryEntity.MITSUMORI_3 = string.Empty;
            }
            // 見積項目4
            if (!string.IsNullOrEmpty(this.form.txt_MitsumoriNaiyo4.Text))
            {
                this.dto.entryEntity.MITSUMORI_4 = this.form.txt_MitsumoriNaiyo4.Text;
            }
            else
            {
                this.dto.entryEntity.MITSUMORI_4 = string.Empty;
            }
            //20250416
            // 見積項目5
            if (!string.IsNullOrEmpty(this.form.txt_MitsumoriNaiyo5.Text))
            {
                this.dto.entryEntity.MITSUMORI_5 = this.form.txt_MitsumoriNaiyo5.Text;
            }
            else
            {
                this.dto.entryEntity.MITSUMORI_5 = string.Empty;
            }

            #region 20250411

            // 備考1
            //if (!string.IsNullOrEmpty(this.form.txt_Bikou1.Text))
            //{
            //    this.dto.entryEntity.BIKOU_1 = this.form.txt_Bikou1.Text;
            //}
            //else
            //{
            //    this.dto.entryEntity.BIKOU_1 = string.Empty;
            //}
            //// 備考2
            //if (!string.IsNullOrEmpty(this.form.txt_Bikou2.Text))
            //{
            //    this.dto.entryEntity.BIKOU_2 = this.form.txt_Bikou2.Text;
            //}
            //else
            //{
            //    this.dto.entryEntity.BIKOU_2 = string.Empty;
            //}
            //// 備考3
            //if (!string.IsNullOrEmpty(this.form.txt_Bikou3.Text))
            //{
            //    this.dto.entryEntity.BIKOU_3 = this.form.txt_Bikou3.Text;
            //}
            //else
            //{
            //    this.dto.entryEntity.BIKOU_3 = string.Empty;
            //}
            //// 備考4
            //if (!string.IsNullOrEmpty(this.form.txt_Bikou4.Text))
            //{
            //    this.dto.entryEntity.BIKOU_4 = this.form.txt_Bikou4.Text;
            //}
            //else
            //{
            //    this.dto.entryEntity.BIKOU_4 = string.Empty;
            //}
            //// 備考5
            //if (!string.IsNullOrEmpty(this.form.txt_Bikou5.Text))
            //{
            //    this.dto.entryEntity.BIKOU_5 = this.form.txt_Bikou5.Text;
            //}
            //else
            //{
            //    this.dto.entryEntity.BIKOU_5 = string.Empty;
            //}

            #endregion 20250411

            // 見積日付印字
            if (!string.IsNullOrEmpty(this.form.txt_MitsumoriInjiDate.Text))
            {
                if (this.form.txt_MitsumoriInjiDate.Text.Equals("1"))
                {
                    this.dto.entryEntity.MITSUMORI_INJI_DATE = SqlBoolean.Parse(this.form.txt_MitsumoriInjiDate.Text);
                }
                else
                {
                    // falseを設定
                    this.dto.entryEntity.MITSUMORI_INJI_DATE = SqlBoolean.Parse("0");
                }
            }
            // 部署名印字
            if (!string.IsNullOrEmpty(this.form.txt_BushouNameInji.Text))
            {
                if (this.form.txt_BushouNameInji.Text.Equals("1"))
                {
                    this.dto.entryEntity.BUSHO_NAME_INJI = SqlBoolean.Parse(this.form.txt_BushouNameInji.Text);
                }
                else
                {
                    // falseを設定
                    this.dto.entryEntity.BUSHO_NAME_INJI = SqlBoolean.Parse("0");
                }
            }

            // 進行中日付
            if (this.form.dtp_ShinkouDate.Value != null && !string.IsNullOrEmpty((this.form.dtp_ShinkouDate.Value).ToString().Trim()))
            {
                this.dto.entryEntity.SINKOU_DATE = this.form.dtp_ShinkouDate.Value.ToString();
            }
            else
            {
                this.dto.entryEntity.SINKOU_DATE = null;
            }
            // 受注日付
            if (this.form.dtp_JuchuDate.Value != null && !string.IsNullOrEmpty((this.form.dtp_JuchuDate.Value).ToString().Trim()))
            {
                this.dto.entryEntity.JUCHU_DATE = this.form.dtp_JuchuDate.Value.ToString();
            }
            else
            {
                this.dto.entryEntity.JUCHU_DATE = null;
            }
            // 失注日付
            if (this.form.dtp_SichuDate.Value != null && !string.IsNullOrEmpty((this.form.dtp_SichuDate.Value).ToString().Trim()))
            {
                this.dto.entryEntity.SICHU_DATE = this.form.dtp_SichuDate.Value.ToString();
            }
            else
            {
                this.dto.entryEntity.SICHU_DATE = null;
            }
            // 社内備考
            if (!string.IsNullOrEmpty(this.form.txt_ShanaiBikou.Text))
            {
                this.dto.entryEntity.SHANAI_BIKOU = this.form.txt_ShanaiBikou.Text;
            }
            else
            {
                this.dto.entryEntity.SHANAI_BIKOU = string.Empty;
            }
            // 税計算区分CD
            if (!string.IsNullOrEmpty(this.form.txt_ZeiKeisanKbnCD.Text))
            {
                this.dto.entryEntity.ZEI_KEISAN_KBN_CD = SqlInt16.Parse(this.form.txt_ZeiKeisanKbnCD.Text);
            }
            // 税区分CD
            if (!string.IsNullOrEmpty(this.form.txt_ZeiKbnCD.Text))
            {
                this.dto.entryEntity.ZEI_KBN_CD = SqlInt16.Parse(this.form.txt_ZeiKbnCD.Text);
            }

            // 金額計
            if (!string.IsNullOrEmpty(this.form.txt_KingakuTotal.Text))
            {
                this.dto.entryEntity.KINGAKU_TOTAL = SqlDecimal.Parse(this.form.txt_KingakuTotal.Text.Replace(",", string.Empty));
            }

            // 消費税率
            this.dto.entryEntity.SHOUHIZEI_RATE = this.taxRate;

            //  伝票毎消費税外税
            if (!string.IsNullOrEmpty(this.form.txt_ZeiKeisanKbnCD.Text) && SqlInt16.Parse(this.form.txt_ZeiKeisanKbnCD.Text).Value == 1)
            {
                if (!string.IsNullOrEmpty(this.form.txt_TaxSoto.Text))
                {
                    this.dto.entryEntity.TAX_SOTO = SqlDecimal.Parse(this.form.txt_TaxSoto.Text.Replace(",", string.Empty));
                }
                else
                {
                    this.dto.entryEntity.TAX_SOTO = 0;
                }
                if (!string.IsNullOrEmpty(this.form.txt_UchiTotal.Text))
                {
                    this.dto.entryEntity.TAX_UCHI = SqlDecimal.Parse(this.form.txt_UchiTotal.Text.Replace(",", string.Empty));
                }
                else
                {
                    this.dto.entryEntity.TAX_UCHI = 0;
                }

                this.dto.entryEntity.TAX_SOTO_TOTAL = 0;
                this.dto.entryEntity.TAX_UCHI_TOTAL = 0;
            }
            else if (!string.IsNullOrEmpty(this.form.txt_ZeiKeisanKbnCD.Text) && SqlInt16.Parse(this.form.txt_ZeiKeisanKbnCD.Text) == 2)
            {
                this.dto.entryEntity.TAX_SOTO = 0;
                this.dto.entryEntity.TAX_UCHI = 0;

                if (!string.IsNullOrEmpty(this.form.txt_TaxSoto.Text))
                {
                    this.dto.entryEntity.TAX_SOTO_TOTAL = SqlDecimal.Parse(this.form.txt_TaxSoto.Text.Replace(",", string.Empty));
                }
                else
                {
                    this.dto.entryEntity.TAX_SOTO_TOTAL = 0;
                }
                if (!string.IsNullOrEmpty(this.form.txt_UchiTotal.Text))
                {
                    this.dto.entryEntity.TAX_UCHI_TOTAL = SqlDecimal.Parse(this.form.txt_UchiTotal.Text.Replace(",", string.Empty));
                }
                else
                {
                    this.dto.entryEntity.TAX_UCHI_TOTAL = 0;
                }
            }

            //20250414
            //備考パターン
            if (!string.IsNullOrEmpty(this.form.BIKO_KBN_CD.Text))
            {
                this.dto.entryEntity.BIKO_KBN_CD = this.form.BIKO_KBN_CD.Text;
            }
            else
            {
                this.dto.entryEntity.BIKO_KBN_CD = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.form.BIKO_NAME_RYAKU.Text))
            {
                this.dto.entryEntity.BIKO_NAME_RYAKU = this.form.BIKO_NAME_RYAKU.Text;
            }
            else
            {
                this.dto.entryEntity.BIKO_NAME_RYAKU = string.Empty;
            }

            // 合計金額
            if (!string.IsNullOrEmpty(this.form.txt_GoukeiKingakuTotal.Text))
            {
                this.dto.entryEntity.GOUKEI_KINGAKU_TOTAL = SqlDecimal.Parse(this.form.txt_GoukeiKingakuTotal.Text.Replace(",", string.Empty));
            }

            // 共通処理
            var dataBinderEntry = new DataBinderLogic<T_MITSUMORI_ENTRY>(this.dto.entryEntity);
            dataBinderEntry.SetSystemProperty(this.dto.entryEntity, false);

            switch (this.form.WindowType)
            {
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    // 更新、または削除の場合、前回の作成情報を更新しない
                    this.dto.entryEntity.CREATE_DATE = this.beforDto.entryEntity.CREATE_DATE;
                    this.dto.entryEntity.CREATE_USER = this.beforDto.entryEntity.CREATE_USER;
                    this.dto.entryEntity.CREATE_PC = this.beforDto.entryEntity.CREATE_PC;
                    break;

                default:
                    break;
            }

            if (tairyuuKbnFlag)
            {
            }
            else
            {
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
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion コントロールから登録用のMitsumoriEntityを作成する

        #region コントロールから登録用のCreateDetailEntityを作成する

        /// <summary>
        /// コントロールから登録用のCreateDetailEntityを作成する
        /// </summary>
        public virtual void CreateMitsumoriDetailEntity()
        {
            LogUtility.DebugMethodStart();

            // Detail
            List<T_MITSUMORI_DETAIL> mitsumoriDetailEntitys = new List<T_MITSUMORI_DETAIL>();

            // ページ分データ作成
            for (int page = 0; page < this.form.tbc_MitsumoriMeisai.TabPages.Count; page++)
            {
                SqlInt16 intPage = 0;
                // ページ
                intPage = SqlInt16.Parse((page + 1).ToString());

                // 初期化
                CustomDataGridView cdgv = ((CustomDataGridView)this.form.tbc_MitsumoriMeisai.TabPages[page].Controls["control"].Controls["CustomDataGridView"]);

                // 画面データ
                for (int i = 0; i < cdgv.Rows.Count - 1; i++)
                {
                    // システムID
                    SqlInt32 detailSysId = -1;

                    DataGridViewRow row = cdgv.Rows[i];

                    T_MITSUMORI_DETAIL temp = new T_MITSUMORI_DETAIL();

                    // モードに依存する処理
                    switch (this.form.WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            // 新規の場合は、既にEntryで採番しているので、それに+1する
                            detailSysId = this.accessor.createSystemIdForMitsumori();
                            temp.DETAIL_SYSTEM_ID = detailSysId;

                            break;

                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:

                            // DETAIL_SYSTEM_IDの採番
                            if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_SYS_ID].Value == null
                                || string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_SYS_ID].Value.ToString()))
                            {
                                // 修正モードでT_MITSUMORI_DETAILが初めて登録されるパターンも張るはずなので、
                                // Detailが無ければ新たに採番(更新モードの場合、ここで初めて採番する)
                                detailSysId = this.accessor.createSystemIdForMitsumori();
                            }
                            else
                            {
                                // 既に登録されていればそのまま使う
                                detailSysId = SqlInt32.Parse(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_SYS_ID].Value.ToString());
                            }

                            temp.DETAIL_SYSTEM_ID = detailSysId;
                            break;

                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                            break;

                        default:
                            break;
                    }

                    // SYSTEM_ID
                    if (!this.dto.entryEntity.SYSTEM_ID.IsNull)
                    {
                        temp.SYSTEM_ID = SqlInt64.Parse(this.dto.entryEntity.SYSTEM_ID.Value.ToString());
                    }

                    // 枝番
                    if (!this.dto.entryEntity.SEQ.IsNull)
                    {
                        temp.SEQ = SqlInt32.Parse(this.dto.entryEntity.SEQ.Value.ToString());
                    }

                    // 明細システムID
                    if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_SYS_ID].Value != null)
                    {
                        temp.DETAIL_SYSTEM_ID = SqlInt64.Parse(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_SYS_ID].Value.ToString());
                    }

                    // 伝票番号
                    if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_NO].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_NO].Value.ToString()))
                    {
                        temp.DENPYOU_NUMBER = SqlInt64.Parse(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_NO].Value.ToString());
                    }

                    // ページ番号
                    if (!string.IsNullOrEmpty(intPage.ToString()))
                    {
                        temp.PAGE_NUMBER = intPage;
                    }

                    // 行番号
                    if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_ROW_NO].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_ROW_NO].Value.ToString()))
                    {
                        temp.ROW_NO = SqlInt16.Parse(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_ROW_NO].Value.ToString());
                    }

                    // 小計ﾌﾗｸﾞ
                    if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SHOUKEI_FLG].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SHOUKEI_FLG].Value.ToString()))
                    {
                        temp.SHOUKEI_FLG = SqlBoolean.Parse(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SHOUKEI_FLG].Value.ToString());
                    }
                    else
                    {
                        temp.SHOUKEI_FLG = false;
                    }

                    // 伝票区分CD
                    if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value.ToString()))
                    {
                        temp.DENPYOU_KBN_CD = SqlInt16.Parse(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value.ToString());
                    }

                    // 品名CD
                    if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value.ToString()))
                    {
                        temp.HINMEI_CD = row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value.ToString();
                    }
                    else
                    {
                        temp.HINMEI_CD = null;
                    }
                    // 品名
                    if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME].Value.ToString()))
                    {
                        // 小計ﾌﾗｸﾞ
                        if (!temp.SHOUKEI_FLG)
                        {
                            temp.HINMEI_NAME = row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME].Value.ToString();
                        }
                    }

                    // 数量
                    if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SUURYOU].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SUURYOU].Value.ToString()))
                    {
                        temp.SUURYOU = SqlDecimal.Parse(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SUURYOU].Value.ToString().Replace(",", ""));
                    }
                    else
                    {
                        temp.SUURYOU = SqlDecimal.Null;
                    }

                    // 単位CD
                    if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD].Value.ToString()))
                    {
                        temp.UNIT_CD = SqlInt16.Parse(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD].Value.ToString());
                    }
                    else
                    {
                        temp.UNIT_CD = SqlInt16.Null;
                    }

                    // 単価
                    if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value.ToString()))
                    {
                        temp.TANKA = SqlDecimal.Parse(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value.ToString().Replace(",", ""));
                    }
                    else
                    {
                        temp.TANKA = SqlDecimal.Null;
                    }

                    // 小計ﾌﾗｸﾞ
                    if (!temp.SHOUKEI_FLG)
                    {
                        // 画面の税区分
                        DataGridViewComboBoxCell cbc = (DataGridViewComboBoxCell)cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD];

                        // 比較用
                        SqlInt16 zeikbn = 0;

                        // 品名別税区分CD
                        if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString()))
                        {
                            zeikbn = SqlInt16.Parse(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1));
                        }
                        else
                        {
                            zeikbn = SqlInt16.Null;
                        }
                        zeikbn = zeikbn.IsNull ? 0 : zeikbn;

                        // 品名税区分
                        SqlInt16 hinmeiZeikbn = 0;
                        if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value == null || string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value.ToString()))
                        {
                            hinmeiZeikbn = SqlInt16.Null;
                        }
                        else
                        {
                            hinmeiZeikbn = this.accessor.GetHinmeiDataByCd(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value.ToString()).ZEI_KBN_CD;
                        }

                        hinmeiZeikbn = hinmeiZeikbn.IsNull ? 0 : hinmeiZeikbn;

                        // 金額
                        temp.KINGAKU = 0;
                        // 消費税外税
                        temp.TAX_SOTO = 0;
                        // 消費税内税
                        temp.TAX_UCHI = 0;

                        // 品名別金額
                        if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value.ToString()))
                        {
                            temp.HINMEI_KINGAKU = SqlDecimal.Parse(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value.ToString().Replace(",", ""));
                        }
                        else
                        {
                            temp.HINMEI_KINGAKU = SqlDecimal.Null;
                        }

                        // 品名別消費税外税
                        if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value.ToString()))
                        {
                            temp.HINMEI_TAX_SOTO = SqlDecimal.Parse(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value.ToString().Replace(",", ""));
                        }
                        else
                        {
                            temp.HINMEI_TAX_SOTO = SqlDecimal.Null;
                        }
                        // 品名別消費税内税
                        if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value.ToString()))
                        {
                            temp.HINMEI_TAX_UCHI = SqlDecimal.Parse(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value.ToString().Replace(",", ""));
                        }
                        else
                        {
                            temp.HINMEI_TAX_UCHI = SqlDecimal.Null;
                        }
                    }
                    else
                    {
                        // 品名金額
                        temp.HINMEI_KINGAKU = 0;
                        // 品名外税
                        temp.HINMEI_TAX_SOTO = 0;
                        // 品名内税
                        temp.HINMEI_TAX_UCHI = 0;

                        // 金額
                        if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value.ToString()))
                        {
                            temp.KINGAKU = SqlDecimal.Parse(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value.ToString().Replace(",", ""));
                        }
                        else
                        {
                            temp.KINGAKU = SqlDecimal.Null;
                        }

                        // 消費税外税
                        if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value.ToString()))
                        {
                            temp.TAX_SOTO = SqlDecimal.Parse(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value.ToString().Replace(",", ""));
                        }
                        else
                        {
                            temp.TAX_SOTO = SqlDecimal.Null;
                        }
                        // 消費税内税
                        if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value.ToString()))
                        {
                            temp.TAX_UCHI = SqlDecimal.Parse(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value.ToString().Replace(",", ""));
                        }
                        else
                        {
                            temp.TAX_UCHI = SqlDecimal.Null;
                        }
                    }

                    // 品名別税区分CD
                    if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString()))
                    {
                        temp.HINMEI_ZEI_KBN_CD = SqlInt16.Parse(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1));
                    }
                    else
                    {
                        temp.HINMEI_ZEI_KBN_CD = SqlInt16.Null;
                    }

                    // 明細備考
                    if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_MEISAI_BIKOU].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_MEISAI_BIKOU].Value.ToString()))
                    {
                        temp.MEISAI_BIKOU = row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_MEISAI_BIKOU].Value.ToString();
                    }
                    else
                    {
                        temp.MEISAI_BIKOU = null;
                    }
                    // 明細摘要
                    if (row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_MEISAI_TEKIYOU].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_MEISAI_TEKIYOU].Value.ToString()))
                    {
                        temp.MEISAI_TEKIYO = row.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_MEISAI_TEKIYOU].Value.ToString();
                    }
                    else
                    {
                        temp.MEISAI_TEKIYO = null;
                    }
                    // 共通処理
                    var dbLogic = new DataBinderLogic<T_MITSUMORI_DETAIL>(temp);
                    dbLogic.SetSystemProperty(temp, false);

                    mitsumoriDetailEntitys.Add(temp);
                }

                this.dto.detailEntity = new T_MITSUMORI_DETAIL[mitsumoriDetailEntitys.Count];
                this.dto.detailEntity = mitsumoriDetailEntitys.ToArray<T_MITSUMORI_DETAIL>();
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion コントロールから登録用のCreateDetailEntityを作成する

        #region 登録処理

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            this.accessor.InsertMitsumoriEntry(this.dto.entryEntity);
            this.accessor.InsertMitsumoriDetail(this.dto.detailEntity);

            //20250416
            this.accessor.InsertBikoDetail(this.dto.detailEntity_2);

            LogUtility.DebugMethodEnd();
        }

        #endregion 登録処理

        #region 検索処理

        /// <summary>
        /// 検索処理
        /// </summary>
        public int Search()
        {
            throw new NotImplementedException();
        }

        #endregion 検索処理

        #region 更新処理

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            this.accessor.InsertMitsumoriEntry(this.dto.entryEntity);
            this.accessor.UpdateMitsumoriEntry(this.beforDto.entryEntity);
            this.accessor.InsertMitsumoriDetail(this.dto.detailEntity);

            //20250416
            this.accessor.InsertBikoDetail(this.dto.detailEntity_2);

            LogUtility.DebugMethodEnd();
        }

        #endregion 更新処理

        #region 明細の制御(システム設定情報用)

        /// <summary>
        /// 明細の制御(システム設定情報用)
        /// </summary>
        /// <param name="headerNames">非表示にするカラム名一覧</param>
        /// <param name="cellNames">非表示にするセル名一覧</param>
        /// <param name="visibleFlag">各カラム、セルのVisibleに設定するbool</param>
        private void ChangePropertyForGC(string[] headerNames, string[] cellNames, string propertyName, bool visibleFlag)
        {
            //this.form.gcMultiRow1.SuspendLayout();

            //var newTemplate = this.form.gcMultiRow1.Template;

            //if (headerNames != null && 0 < headerNames.Length)
            //{
            //    var obj1 = controlUtil.FindControl(newTemplate.ColumnHeaders[0].Cells.ToArray(), headerNames);
            //    foreach (var o in obj1)
            //    {
            //        PropertyUtility.SetValue(o, propertyName, visibleFlag);
            //    }
            //}

            //if (cellNames != null && 0 < cellNames.Length)
            //{
            //    var obj2 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), cellNames);
            //    foreach (var o in obj2)
            //    {
            //        PropertyUtility.SetValue(o, propertyName, visibleFlag);
            //    }
            //}

            //this.form.gcMultiRow1.Template = newTemplate;

            //this.form.gcMultiRow1.ResumeLayout();
        }

        #endregion 明細の制御(システム設定情報用)

        #region 指定した受入番号のデータが存在するか返す

        /// <summary>
        /// 指定した受入番号のデータが存在するか返す
        /// </summary>
        /// <param name="ukeireNumber">受入番号</param>
        /// <returns>true:存在する, false:存在しない</returns>
        internal bool IsExistUkeireData(long ukeireNumber)
        {
            LogUtility.DebugMethodStart();

            bool returnVal = false;

            //if (0 <= ukeireNumber)
            //{
            //    var ukeireEntrys = this.accessor.GetKeiryouEntry(ukeireNumber);
            //    if (ukeireEntrys != null
            //        && 0 < ukeireEntrys.Length)
            //    {
            //        returnVal = true;
            //    }
            //}

            LogUtility.DebugMethodEnd();
            return returnVal;
        }

        #endregion 指定した受入番号のデータが存在するか返す

        #region 金額値用フォーマット

        /// <summary>
        /// 金額値用フォーマット
        /// </summary>
        /// <param name="sender"></param>
        internal void ToAmountValue(object sender)
        {
            LogUtility.DebugMethodStart(sender);

            if (sender == null)
            {
                return;
            }

            //var value = PropertyUtility.GetTextOrValue(sender);
            //if (!string.IsNullOrEmpty(value))
            //{
            //    PropertyUtility.SetTextOrValue(sender, FormatUtility.ToAmountValue(value));
            //}

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 重量値、金額、数量用フォーマット
        /// </summary>
        /// <param name="value"></param>
        /// <param name="formatStr"></param>
        /// <returns></returns>
        public string ToAmountValue(string value, string formatStr)
        {
            LogUtility.DebugMethodStart(value, formatStr);

            // 初期化
            string returnValue = "";

            // 変換対象がNULLもしくはEmptyだった場合はブランクを返す
            if (false == string.IsNullOrEmpty(value))
            {
                decimal num = -1;
                if (decimal.TryParse(value, out num))
                {
                    // 指定されたフォーマット書式に従い変換
                    returnValue = num.ToString(formatStr);
                }
            }

            LogUtility.DebugMethodEnd(returnValue);
            return returnValue;
        }

        #endregion 金額値用フォーマット

        #region 削除チェック

        /// <summary>
        /// 削除チェック
        /// Datailが一行以上入力されているかチェックする
        /// </summary>
        /// <returns>true: 一件以上入力されている, false: 一件も入力されていない</returns>
        internal bool CheckDeletePageCheck()
        {
            LogUtility.DebugMethodStart();

            bool returnVal = true;

            // Detail
            String pageStatus = string.Empty;

            int selectedTab = int.Parse(this.form.tbc_MitsumoriMeisai.SelectedTab.Name.Split('e')[1]);

            // ページ分データ作成
            for (int page = selectedTab; page < this.form.tbc_MitsumoriMeisai.TabPages.Count; page++)
            {
                // CustomDataGridViewを取得する
                CustomDataGridView cdgv = ((CustomDataGridView)this.form.tbc_MitsumoriMeisai.TabPages[page].Controls["control"].Controls["CustomDataGridView"]);

                if (cdgv.Rows.Count > 1)
                {
                    returnVal = false;

                    return returnVal;
                }
            }

            LogUtility.DebugMethodEnd();
            return returnVal;
        }

        #endregion 削除チェック

        #region ユーザ入力コントロールの活性制御

        /// <summary>
        /// ユーザ入力コントロールの活性制御
        /// </summary>
        /// <param name="isLock">ロック状態に設定するbool</param>
        internal bool ChangeEnabledForInputControl(bool isLock)
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
                // UIFormのコントロールを制御
                List<string> formControlNameList = new List<string>();

                if (this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                {
                    // 参照モード時は前・次ボタンを押下可能にするため分ける
                    formControlNameList.AddRange(inputUiFormControlNames_Reference);
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

                    var property = control.GetType().GetProperty("ReadOnly");

                    if (property != null)
                    {
                        property.SetValue(control, isLock, null);
                    }

                    property = control.GetType().GetProperty("Enabled");

                    if (property != null)
                    {
                        if (controlName.ToString().Equals("tbc_MitsumoriMeisai") &&
                            this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                        {
                            // 参照モード時は見積明細のタブが変更できなくなるので中のコントロールを操作不可にする
                            foreach (System.Windows.Forms.Control ctl in this.form.tbc_MitsumoriMeisai.Controls)
                            {
                                if (ctl is System.Windows.Forms.Label)
                                {
                                    continue;
                                }

                                if (ctl is System.Windows.Forms.TabPage)
                                {
                                    // Tabコントロールの場合、更にTab+1階層下のコントロールを取得
                                    foreach (System.Windows.Forms.Control ct in ctl.Controls)
                                    {
                                        foreach (System.Windows.Forms.Control c in ct.Controls)
                                        {
                                            if (c is System.Windows.Forms.Label)
                                            {
                                                continue;
                                            }

                                            // DataGridViewの場合、Enabledを操作するとスクロールできなくなるのを回避するためのフラグ
                                            bool isSetReadOnlyForDVG = false;

                                            var ctl_property = c.GetType().GetProperty("ReadOnly");
                                            if (ctl_property != null)
                                            {
                                                ctl_property.SetValue(c, isLock, null);
                                                if (c is r_framework.CustomControl.CustomDataGridView)
                                                {
                                                    isSetReadOnlyForDVG = true;
                                                }
                                            }

                                            ctl_property = c.GetType().GetProperty("Enabled");
                                            if (ctl_property != null && !isSetReadOnlyForDVG)
                                            {
                                                ctl_property.SetValue(c, !isLock, null);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    var ctl_property = ctl.GetType().GetProperty("ReadOnly");
                                    if (ctl_property != null)
                                    {
                                        ctl_property.SetValue(ctl, isLock, null);
                                    }

                                    ctl_property = ctl.GetType().GetProperty("Enabled");
                                    if (ctl_property != null)
                                    {
                                        ctl_property.SetValue(ctl, !isLock, null);
                                    }
                                }
                            }
                        }
                        else
                        {
                            property.SetValue(control, !isLock, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeEnabledForInputControl", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }

        #endregion ユーザ入力コントロールの活性制御

        #region 単位名を取得

        /// <summary>
        /// 単位を取得
        /// </summary>
        internal string GetUnitName(short unit)
        {
            String rtnUnitName = null;
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
                rtnUnitName = units[0].UNIT_NAME_RYAKU;
            }

            return rtnUnitName;
        }

        #endregion 単位名を取得

        #region 伝票区分名を取得

        /// <summary>
        /// 伝票区分を取得
        /// </summary>
        internal string GetDataByCodeForDenpyouKbnName(short denpyouKbnCd)
        {
            String rtnDenpyouKbnName = null;
            var denpyouKbnCds = this.accessor.GetDataByCodeForDenpyouKbn(denpyouKbnCd);

            // 存在チェック
            if (denpyouKbnCds == null)
            {
                // 存在しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "伝票区分");
            }
            else
            {
                // 伝票区分を取得
                rtnDenpyouKbnName = denpyouKbnCds.DENPYOU_KBN_NAME_RYAKU;
            }

            return rtnDenpyouKbnName;
        }

        #endregion 伝票区分名を取得

        #region 論理削除処理

        /// <summary>
        /// 論理削除処理
        /// </summary>
        public virtual void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            this.dto.entryEntity.DELETE_FLG = true;
            this.accessor.UpdateMitsumoriEntry(this.dto.entryEntity);

            // 20141204 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
            this.dto.entryEntity.SEQ += 1;
            this.accessor.InsertMitsumoriEntry(this.dto.entryEntity);
            foreach (T_MITSUMORI_DETAIL data in this.dto.detailEntity)
            {
                data.SEQ = this.dto.entryEntity.SEQ;
            }
            this.accessor.InsertMitsumoriDetail(this.dto.detailEntity);
            // 20141204 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します end

            //20250417
            foreach (T_MITSUMORI_DETAIL_2 data2 in this.dto.detailEntity_2)
            {
                data2.SEQ = this.dto.entryEntity.SEQ;
            }
            this.accessor.InsertBikoDetail(this.dto.detailEntity_2);

            LogUtility.DebugMethodStart();
        }

        #endregion 論理削除処理

        #region 物理削除処理

        /// <summary>
        /// 物理削除処理
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion 物理削除処理

        #region プレビュー

        /// <summary>
        /// プレビュー
        /// </summary>
        internal bool Print()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
                // 再設定
                this.CreateEntity(true);

                // 画面表示名称（種類)
                string layoutName = string.Empty;

                // プロジェクトID
                string projectId = string.Empty;

                // 帳票宣言
                ReportInfoR425_R508_R547_R548 reportInfo = new ReportInfoR425_R508_R547_R548(WINDOW_ID.R_MITSUMORISYO);

                //　会社情報取得
                this.dto.corpEntity = this.accessor.GetCorpInfo(this.dto.sysInfoEntity.SYS_ID);

                // 部署名
                if (!string.IsNullOrEmpty(this.dto.entryEntity.SHAIN_CD))
                {
                    this.dto.bushoEntity = new M_BUSHO();

                    var shainEntity = this.accessor.GetShain(this.form.txt_ShainCD.Text);

                    if (shainEntity != null)
                    {
                        var busho = this.accessor.GetDataByCodeForBusho(shainEntity.BUSHO_CD);
                        if (busho != null && 0 < busho.Length)
                        {
                            this.dto.bushoEntity = busho[0];
                        }
                    }
                }

                //　印字拠点1情報
                this.dto.kyotenEntity1 = new M_KYOTEN();
                if (!this.dto.entryEntity.INJI_KYOTEN1_CD.IsNull)
                {
                    this.dto.kyotenEntity1 = this.getKyotenData(this.dto.entryEntity.INJI_KYOTEN1_CD.ToString().PadLeft(2, '0'));
                }

                //　印字拠点2情報
                this.dto.kyotenEntity2 = new M_KYOTEN();
                if (!this.dto.entryEntity.INJI_KYOTEN2_CD.IsNull)
                {
                    this.dto.kyotenEntity2 = this.getKyotenData(this.dto.entryEntity.INJI_KYOTEN2_CD.ToString().PadLeft(2, '0'));
                }

                // 画面種類
                switch (this.form.formParem.cyohyoType)
                {
                    case MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE: // 見積書（縦）
                        reportInfo.OutputType = ReportInfoR425_R508_R547_R548.OutputTypeDef.KingakuMitsumoriV;
                        layoutName = "LAYOUT1";
                        projectId = "R425";

                        break;

                    case MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO: // 見積書（横）
                        reportInfo.OutputType = ReportInfoR425_R508_R547_R548.OutputTypeDef.KingakuMitsumoriH;
                        layoutName = "LAYOUT2";
                        projectId = "R508";

                        break;

                    case MitumorisyoConst.MITUMOTISYO_TANKA_TATE:   // 単価見積書（縦）
                        reportInfo.OutputType = ReportInfoR425_R508_R547_R548.OutputTypeDef.TankaMitsumoriV;
                        layoutName = "LAYOUT3";
                        projectId = "R547";

                        break;

                    case MitumorisyoConst.MITUMOTISYO_TANKA_YOKO:   // 単価見積書（横）
                        reportInfo.OutputType = ReportInfoR425_R508_R547_R548.OutputTypeDef.TankaMitsumoriH;
                        layoutName = "LAYOUT4";
                        projectId = "R548";

                        break;
                }

                // 引渡すデータ
                DataTable reportData = new DataTable();

                // ページ分データ作成
                for (int page = 1; page <= this.form.tbc_MitsumoriMeisai.TabPages.Count; page++)
                {
                    DataRow reportDataRow = reportData.NewRow();

                    // ページ宣言
                    reportInfo.DataTablePageList[page.ToString()] = new Dictionary<string, DataTable>();

                    // ヘッダ
                    reportInfo.DataTablePageList[page.ToString()]["Header"] = this.getPageHeader(this.dto, page);

                    // 明細
                    reportInfo.DataTablePageList[page.ToString()]["Detail"] = this.getPageDetail(page);

                    // フッタ
                    reportInfo.DataTablePageList[page.ToString()]["Footer"] = this.getPageFooter(page);
                }

                // Sample
                //reportInfo.CreateSampleData();

                // 現在表示されている一覧をレポート情報として生成
                reportInfo.Create(@".\Template\R425_R508_R547_R548-Form.xml", layoutName, new DataTable());
                if (this.form.formParem.cyohyoType == MitumorisyoConst.MITUMOTISYO_TANKA_YOKO)
                {
                    reportInfo.Title = MitumorisyoConst.MITUMOTISYO_TANKA_YOKO_NAME;
                }
                else if (this.form.formParem.cyohyoType == MitumorisyoConst.MITUMOTISYO_TANKA_TATE)
                {
                    reportInfo.Title = MitumorisyoConst.MITUMOTISYO_TANKA_TATE_NAME;
                }
                else if (this.form.formParem.cyohyoType == MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO)
                {
                    reportInfo.Title = MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO_NAME;
                }
                else if (this.form.formParem.cyohyoType == MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE)
                {
                    reportInfo.Title = MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE_NAME;
                }
                else
                {
                    reportInfo.Title = "見積書";
                }

                using (FormReport reportPopup = new FormReport(reportInfo, projectId, WINDOW_ID.R_MITSUMORISYO))
                {
                    // 見積書種類表示

                    if (this.form.formParem.cyohyoType == MitumorisyoConst.MITUMOTISYO_TANKA_YOKO)
                    {
                        reportPopup.Caption = MitumorisyoConst.MITUMOTISYO_TANKA_YOKO_NAME;
                    }
                    else if (this.form.formParem.cyohyoType == MitumorisyoConst.MITUMOTISYO_TANKA_TATE)
                    {
                        reportPopup.Caption = MitumorisyoConst.MITUMOTISYO_TANKA_TATE_NAME;
                    }
                    else if (this.form.formParem.cyohyoType == MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO)
                    {
                        reportPopup.Caption = MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO_NAME;
                    }
                    else if (this.form.formParem.cyohyoType == MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE)
                    {
                        reportPopup.Caption = MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE_NAME;
                    }
                    else
                    {
                        reportPopup.Caption = string.Empty;
                    }

                    // 印刷設定の取得
                    //reportPopup.SetPrintSetting(MitumorisyoConst.MITSUMORISHO);

                    // 印刷アプリ初期動作(プレビュー)
                    reportPopup.PrintInitAction = 2;

                    //reportPopup.ShowDialog();
                    reportPopup.PrintXPS();
                    reportPopup.Dispose();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Print", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }

        #endregion プレビュー

        #region ヘッダ（１ページ）

        /// <summary>
        /// ヘッダ（１ページ）
        /// </summary>
        private DataTable getPageHeader(DTOClass dto, int pageNo)
        {
            LogUtility.DebugMethodStart(dto, pageNo);

            // Header部
            DataTable dataTableHeader = new DataTable();

            // ヘッダ部
            for (int iHeader = 0; iHeader < headerReportItemName.Length; iHeader++)
            {
                dataTableHeader.Columns.Add(headerReportItemName[iHeader], typeof(string));
            }

            DataRow dataTableHeaderRow = dataTableHeader.NewRow();

            String torihikisakiName = String.Empty;
            String torihikisakiName1 = String.Empty;
            String torihikisakiName2 = String.Empty;
            String torihikisakiKeishou = String.Empty;

            String gyoushaName = String.Empty;
            String gyoushaName1 = String.Empty;
            String gyoushaName2 = String.Empty;
            String gyoushaKeishou = String.Empty;

            String genbaName = String.Empty;
            String genbaName1 = String.Empty;
            String genbaName2 = String.Empty;
            String genbaKeishou = String.Empty;

            for (int iHeader = 0; iHeader < headerReportItemName.Length; iHeader++)
            {
                object value;
                PropertyUtility.GetValue(this.dto.entryEntity, headerReportItemName[iHeader], out value);

                value = value == null ? "" : value;
                value = "Null".Equals(value.ToString()) ? "" : value;

                if (!this.dto.entryEntity.TORIHIKISAKI_INJI.IsNull && (!this.dto.entryEntity.TORIHIKISAKI_INJI.IsNull && (Boolean)this.dto.entryEntity.TORIHIKISAKI_INJI))
                {
                    // 取引先名1(＋取引先敬称1)
                    if (headerReportItemName[iHeader].Equals("TORIHIKISAKI_NAME1"))
                    {
                        // 引合フラグ　＝０　且つ 諸口区分 ＝ True
                        if ((this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("0")) && this.form.txt_Torihikisaki_shokuchi_kbn.Text.Equals("True"))
                        {
                            // 画面値を出力
                            PropertyUtility.GetValue(this.dto.entryEntity, "TORIHIKISAKI_NAME", out value);
                            value = value == null ? "" : value;
                            torihikisakiName1 = value.ToString();

                            PropertyUtility.GetValue(this.dto.entryEntity, "TORIHIKISAKI_KEISHOU", out value);
                            value = value == null ? "" : value;
                            torihikisakiKeishou = value.ToString();

                            if (!string.IsNullOrEmpty(torihikisakiName1 + torihikisakiName2))
                            {
                                torihikisakiName = torihikisakiName1 + torihikisakiName2 + "　" + torihikisakiKeishou;
                            }

                            value = torihikisakiName;
                        }
                        else if ((this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("0")) && this.form.txt_Torihikisaki_shokuchi_kbn.Text.Equals("False"))
                        {
                            // 画面値を出力
                            PropertyUtility.GetValue(this.dto.torihikisakiEntry, "TORIHIKISAKI_NAME1", out value);

                            value = value == null ? "" : value;

                            torihikisakiName1 = value.ToString();

                            PropertyUtility.GetValue(this.dto.torihikisakiEntry, "TORIHIKISAKI_NAME2", out value);
                            value = value == null ? "" : value;

                            torihikisakiName2 = value.ToString();

                            PropertyUtility.GetValue(this.dto.entryEntity, "TORIHIKISAKI_KEISHOU", out value);
                            value = value == null ? "" : value;
                            torihikisakiKeishou = value.ToString();

                            // if (!string.IsNullOrEmpty(torihikisakiName1 + torihikisakiName2))
                            if (!string.IsNullOrEmpty(torihikisakiName1) && !string.IsNullOrEmpty(torihikisakiName2))
                            {
                                torihikisakiName = torihikisakiName1;
                            }
                            else if (!string.IsNullOrEmpty(torihikisakiName1) && string.IsNullOrEmpty(torihikisakiName2))
                            {
                                torihikisakiName = torihikisakiName1 + "　" + torihikisakiKeishou;
                            }
                            value = torihikisakiName;
                        }
                        else if (this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("1"))
                        {
                            // 画面値を出力
                            PropertyUtility.GetValue(this.dto.hikiaiTorihikisakiEntry, "TORIHIKISAKI_NAME1", out value);

                            value = value == null ? "" : value;

                            torihikisakiName1 = value.ToString();

                            PropertyUtility.GetValue(this.dto.hikiaiTorihikisakiEntry, "TORIHIKISAKI_NAME2", out value);
                            value = value == null ? "" : value;

                            torihikisakiName2 = value.ToString();

                            PropertyUtility.GetValue(this.dto.entryEntity, "TORIHIKISAKI_KEISHOU", out value);
                            value = value == null ? "" : value;
                            torihikisakiKeishou = value.ToString();

                            if (!string.IsNullOrEmpty(torihikisakiName1) && !string.IsNullOrEmpty(torihikisakiName2))
                            {
                                torihikisakiName = torihikisakiName1;
                            }
                            else if (!string.IsNullOrEmpty(torihikisakiName1) && string.IsNullOrEmpty(torihikisakiName2))
                            {
                                torihikisakiName = torihikisakiName1 + "　" + torihikisakiKeishou;
                            }
                            value = torihikisakiName;
                        }
                    }
                    else if (headerReportItemName[iHeader].Equals("TORIHIKISAKI_NAME2"))
                    {
                        // 引合フラグ　＝０　且つ 諸口区分 ＝ True
                        if ((this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("0")) && this.form.txt_Torihikisaki_shokuchi_kbn.Text.Equals("True"))
                        {
                            // 取得しない

                            value = String.Empty;
                        }
                        else if ((this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("0")) && this.form.txt_Torihikisaki_shokuchi_kbn.Text.Equals("False"))
                        {
                            PropertyUtility.GetValue(this.dto.torihikisakiEntry, "TORIHIKISAKI_NAME2", out value);

                            value = value == null ? "" : value;

                            torihikisakiName2 = value.ToString();

                            PropertyUtility.GetValue(this.dto.entryEntity, "TORIHIKISAKI_KEISHOU", out value);
                            value = value == null ? "" : value;
                            torihikisakiKeishou = value.ToString();

                            if (!string.IsNullOrEmpty(torihikisakiName1) && !string.IsNullOrEmpty(torihikisakiName2))
                            {
                                torihikisakiName = torihikisakiName2 + "　" + torihikisakiKeishou;
                            }
                            else if (!string.IsNullOrEmpty(torihikisakiName1) && string.IsNullOrEmpty(torihikisakiName2))
                            {
                                torihikisakiName = string.Empty;
                            }
                            value = torihikisakiName;
                        }
                        else if (this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("1"))
                        {
                            PropertyUtility.GetValue(this.dto.hikiaiTorihikisakiEntry, "TORIHIKISAKI_NAME2", out value);

                            value = value == null ? "" : value;

                            torihikisakiName2 = value.ToString();

                            PropertyUtility.GetValue(this.dto.entryEntity, "TORIHIKISAKI_KEISHOU", out value);
                            value = value == null ? "" : value;
                            torihikisakiKeishou = value.ToString();

                            if (!string.IsNullOrEmpty(torihikisakiName1) && !string.IsNullOrEmpty(torihikisakiName2))
                            {
                                torihikisakiName = torihikisakiName2 + "　" + torihikisakiKeishou;
                            }
                            else if (!string.IsNullOrEmpty(torihikisakiName1) && string.IsNullOrEmpty(torihikisakiName2))
                            {
                                torihikisakiName = string.Empty;
                            }
                            value = torihikisakiName;
                        }
                    }
                }

                // 業者名1(＋業者敬称1)
                if (!this.dto.entryEntity.GENBA_INJI.IsNull && (!this.dto.entryEntity.GENBA_INJI.IsNull && (Boolean)this.dto.entryEntity.GYOUSHA_INJI))
                {
                    if (headerReportItemName[iHeader].Equals("GYOUSHA_NAME1"))
                    {
                        // 引合フラグ　＝０　且つ 諸口区分 ＝ True
                        if ((this.form.txt_Gyousha_hikiai_flg.Text.Equals("0")) && (this.form.txt_Gyousha_shokuchi_kbn.Text.Equals("True") || this.form.txt_Gyousha_shokuchi_kbn.Text.Equals("1")))
                        {
                            // 画面値を出力
                            PropertyUtility.GetValue(this.dto.entryEntity, "GYOUSHA_NAME", out value);
                            value = value == null ? "" : value;
                            gyoushaName1 = value.ToString();

                            PropertyUtility.GetValue(this.dto.entryEntity, "GYOUSHA_KEISHOU", out value);
                            value = value == null ? "" : value;
                            gyoushaKeishou = value.ToString();

                            if (!string.IsNullOrEmpty(gyoushaName1))
                            {
                                gyoushaName = gyoushaName1 + "　" + gyoushaKeishou;
                            }

                            value = gyoushaName;
                        }
                        else if ((this.form.txt_Gyousha_hikiai_flg.Text.Equals("0")) && (this.form.txt_Gyousha_shokuchi_kbn.Text.Equals("False") || this.form.txt_Gyousha_shokuchi_kbn.Text.Equals("0")))
                        {
                            // 画面値を出力
                            PropertyUtility.GetValue(this.dto.gyoushaEntry, "GYOUSHA_NAME1", out value);

                            value = value == null ? "" : value;

                            gyoushaName1 = value.ToString();

                            PropertyUtility.GetValue(this.dto.gyoushaEntry, "GYOUSHA_NAME2", out value);
                            value = value == null ? "" : value;

                            gyoushaName2 = value.ToString();

                            PropertyUtility.GetValue(this.dto.entryEntity, "GYOUSHA_KEISHOU", out value);
                            value = value == null ? "" : value;
                            gyoushaKeishou = value.ToString();

                            if (!string.IsNullOrEmpty(gyoushaName1) && !string.IsNullOrEmpty(gyoushaName2))
                            {
                                gyoushaName = gyoushaName1;
                            }
                            else if (!string.IsNullOrEmpty(gyoushaName1) && string.IsNullOrEmpty(gyoushaName2))
                            {
                                gyoushaName = gyoushaName1 + "　" + gyoushaKeishou;
                            }
                            value = gyoushaName;
                        }
                        else if (this.form.txt_Gyousha_hikiai_flg.Text.Equals("1") || this.form.txt_Gyousha_hikiai_flg.Text.Equals("True"))
                        {
                            // 画面値を出力
                            PropertyUtility.GetValue(this.dto.hikiaiGyoushaEntry, "GYOUSHA_NAME1", out value);

                            value = value == null ? "" : value;

                            gyoushaName1 = value.ToString();

                            PropertyUtility.GetValue(this.dto.hikiaiGyoushaEntry, "GYOUSHA_NAME2", out value);
                            value = value == null ? "" : value;

                            gyoushaName2 = value.ToString();

                            PropertyUtility.GetValue(this.dto.entryEntity, "GYOUSHA_KEISHOU", out value);
                            value = value == null ? "" : value;
                            gyoushaKeishou = value.ToString();

                            if (!string.IsNullOrEmpty(gyoushaName1) && !string.IsNullOrEmpty(gyoushaName2))
                            {
                                gyoushaName = gyoushaName1;
                            }
                            else if (!string.IsNullOrEmpty(gyoushaName1) && string.IsNullOrEmpty(gyoushaName2))
                            {
                                gyoushaName = gyoushaName1 + "　" + gyoushaKeishou;
                            }
                            value = gyoushaName;
                        }
                    }
                    else if (headerReportItemName[iHeader].Equals("GYOUSHA_NAME2"))
                    {
                        // 引合フラグ　＝０　且つ 諸口区分 ＝ True
                        if ((this.form.txt_Gyousha_hikiai_flg.Text.Equals("0")) && this.form.txt_Gyousha_shokuchi_kbn.Text.Equals("True"))
                        {
                            // 取得しない

                            value = String.Empty; ;
                        }
                        else if ((this.form.txt_Gyousha_hikiai_flg.Text.Equals("0")) && this.form.txt_Gyousha_shokuchi_kbn.Text.Equals("False"))
                        {
                            // 画面値を出力
                            PropertyUtility.GetValue(this.dto.gyoushaEntry, "GYOUSHA_NAME1", out value);

                            value = value == null ? "" : value;

                            gyoushaName1 = value.ToString();

                            PropertyUtility.GetValue(this.dto.gyoushaEntry, "GYOUSHA_NAME2", out value);
                            value = value == null ? "" : value;

                            gyoushaName2 = value.ToString();

                            PropertyUtility.GetValue(this.dto.entryEntity, "GYOUSHA_KEISHOU", out value);
                            value = value == null ? "" : value;
                            gyoushaKeishou = value.ToString();

                            if (!string.IsNullOrEmpty(gyoushaName1) && !string.IsNullOrEmpty(gyoushaName2))
                            {
                                gyoushaName = gyoushaName2 + "　" + gyoushaKeishou; ;
                            }
                            else if (!string.IsNullOrEmpty(gyoushaName1) && string.IsNullOrEmpty(gyoushaName2))
                            {
                                gyoushaName = string.Empty;
                            }
                            value = gyoushaName;
                        }
                        else if (this.form.txt_Gyousha_hikiai_flg.Text.Equals("1") || this.form.txt_Gyousha_hikiai_flg.Text.Equals("True"))
                        {
                            // 画面値を出力
                            PropertyUtility.GetValue(this.dto.hikiaiGyoushaEntry, "GYOUSHA_NAME1", out value);

                            value = value == null ? "" : value;

                            gyoushaName1 = value.ToString();

                            PropertyUtility.GetValue(this.dto.hikiaiGyoushaEntry, "GYOUSHA_NAME2", out value);
                            value = value == null ? "" : value;

                            gyoushaName2 = value.ToString();

                            PropertyUtility.GetValue(this.dto.entryEntity, "GYOUSHA_KEISHOU", out value);
                            value = value == null ? "" : value;
                            gyoushaKeishou = value.ToString();

                            if (!string.IsNullOrEmpty(gyoushaName1) && !string.IsNullOrEmpty(gyoushaName2))
                            {
                                gyoushaName = gyoushaName2 + "　" + gyoushaKeishou; ;
                            }
                            else if (!string.IsNullOrEmpty(gyoushaName1) && string.IsNullOrEmpty(gyoushaName2))
                            {
                                gyoushaName = string.Empty;
                            }
                            value = gyoushaName;
                        }
                    }
                }

                // 現場
                if (!this.dto.entryEntity.GENBA_INJI.IsNull && (!this.dto.entryEntity.GENBA_INJI.IsNull && (Boolean)this.dto.entryEntity.GENBA_INJI))
                {
                    if (headerReportItemName[iHeader].Equals("GENBA_NAME1"))
                    {
                        // 引合フラグ　＝０　且つ 諸口区分 ＝ True
                        if ((this.form.txt_Genba_hikiai_flg.Text.Equals("0")) && this.form.txt_Genba_shokuchi_kbn.Text.Equals("True"))
                        {
                            // 画面値を出力
                            PropertyUtility.GetValue(this.dto.entryEntity, "GENBA_NAME", out value);
                            value = value == null ? "" : value;
                            genbaName1 = value.ToString();

                            PropertyUtility.GetValue(this.dto.entryEntity, "GENBA_KEISHOU", out value);
                            value = value == null ? "" : value;
                            genbaKeishou = value.ToString();

                            if (!string.IsNullOrEmpty(genbaName1 + genbaName2))
                            {
                                genbaName = genbaName1 + genbaName2 + "　" + genbaKeishou;
                            }

                            if (!string.IsNullOrWhiteSpace(genbaName))
                            {
                                value = genbaName;
                            }
                        }
                        else if ((this.form.txt_Genba_hikiai_flg.Text.Equals("0")) && this.form.txt_Genba_shokuchi_kbn.Text.Equals("False"))
                        {
                            // 画面値を出力
                            PropertyUtility.GetValue(this.dto.genbaEntry, "GENBA_NAME1", out value);

                            value = value == null ? "" : value;

                            genbaName1 = value.ToString();

                            PropertyUtility.GetValue(this.dto.genbaEntry, "GENBA_NAME2", out value);
                            value = value == null ? "" : value;

                            genbaName2 = value.ToString();

                            PropertyUtility.GetValue(this.dto.entryEntity, "GENBA_KEISHOU", out value);
                            value = value == null ? "" : value;
                            genbaKeishou = value.ToString();

                            if (!string.IsNullOrEmpty(genbaName1) && !string.IsNullOrEmpty(genbaName2))
                            {
                                genbaName = genbaName1;
                            }
                            else if (!string.IsNullOrEmpty(genbaName1) && string.IsNullOrEmpty(genbaName2))
                            {
                                genbaName = genbaName1 + "　" + genbaKeishou;
                            }
                            value = genbaName;
                        }
                        else if (this.form.txt_Genba_hikiai_flg.Text.Equals("1"))
                        {
                            // 画面値を出力
                            PropertyUtility.GetValue(this.dto.hikiaiGenbaEntry, "GENBA_NAME1", out value);

                            value = value == null ? "" : value;

                            genbaName1 = value.ToString();

                            PropertyUtility.GetValue(this.dto.hikiaiGenbaEntry, "GENBA_NAME2", out value);
                            value = value == null ? "" : value;

                            genbaName2 = value.ToString();

                            PropertyUtility.GetValue(this.dto.entryEntity, "GENBA_KEISHOU", out value);
                            value = value == null ? "" : value;
                            genbaKeishou = value.ToString();

                            if (!string.IsNullOrEmpty(genbaName1) && !string.IsNullOrEmpty(genbaName2))
                            {
                                genbaName = genbaName1;
                            }
                            else if (!string.IsNullOrEmpty(genbaName1) && string.IsNullOrEmpty(genbaName2))
                            {
                                genbaName = genbaName1 + "　" + genbaKeishou;
                            }
                            value = genbaName;
                        }
                    }
                    else if (headerReportItemName[iHeader].Equals("GENBA_NAME2"))
                    {
                        // 引合フラグ　＝０　且つ 諸口区分 ＝ True
                        if ((this.form.txt_Genba_hikiai_flg.Text.Equals("0")) && this.form.txt_Genba_shokuchi_kbn.Text.Equals("True"))
                        {
                            // 取得しない

                            value = String.Empty; ;
                        }
                        else if ((this.form.txt_Genba_hikiai_flg.Text.Equals("0")) && this.form.txt_Genba_shokuchi_kbn.Text.Equals("False"))
                        {
                            // 画面値を出力
                            PropertyUtility.GetValue(this.dto.genbaEntry, "GENBA_NAME1", out value);

                            value = value == null ? "" : value;

                            genbaName1 = value.ToString();

                            PropertyUtility.GetValue(this.dto.genbaEntry, "GENBA_NAME2", out value);
                            value = value == null ? "" : value;

                            genbaName2 = value.ToString();

                            PropertyUtility.GetValue(this.dto.entryEntity, "GENBA_KEISHOU", out value);
                            value = value == null ? "" : value;
                            genbaKeishou = value.ToString();

                            if (!string.IsNullOrEmpty(genbaName1) && !string.IsNullOrEmpty(genbaName2))
                            {
                                genbaName = genbaName2 + "　" + genbaKeishou;
                            }
                            else if (!string.IsNullOrEmpty(genbaName1) && string.IsNullOrEmpty(genbaName2))
                            {
                                genbaName = string.Empty;
                            }
                            value = genbaName;
                        }
                        else if (this.form.txt_Genba_hikiai_flg.Text.Equals("1"))
                        {
                            // 画面値を出力
                            PropertyUtility.GetValue(this.dto.hikiaiGenbaEntry, "GENBA_NAME1", out value);

                            value = value == null ? "" : value;

                            genbaName1 = value.ToString();

                            PropertyUtility.GetValue(this.dto.hikiaiGenbaEntry, "GENBA_NAME2", out value);
                            value = value == null ? "" : value;

                            genbaName2 = value.ToString();

                            PropertyUtility.GetValue(this.dto.entryEntity, "GENBA_KEISHOU", out value);
                            value = value == null ? "" : value;
                            genbaKeishou = value.ToString();

                            if (!string.IsNullOrEmpty(genbaName1) && !string.IsNullOrEmpty(genbaName2))
                            {
                                genbaName = genbaName2 + "　" + genbaKeishou;
                            }
                            else if (!string.IsNullOrEmpty(genbaName1) && string.IsNullOrEmpty(genbaName2))
                            {
                                genbaName = string.Empty;
                            }
                            value = genbaName;
                        }
                    }
                }

                // 201400708 syunrei ＃947　№13　　start
                if (this.form.txt_BushouNameInji.Text.Equals("1") && this.form.rdo_BS_InjiAri.Checked)
                {
                    // 部署名
                    if (headerReportItemName[iHeader].Equals("BUSHO_NAME"))
                    {
                        value = this.dto.bushoEntity.BUSHO_NAME == null ? "" : this.dto.bushoEntity.BUSHO_NAME;
                    }
                    //部署名が空の場合、部署名ラベルも表示しない
                    if (this.dto.bushoEntity.BUSHO_NAME != null
                        && !this.dto.bushoEntity.BUSHO_NAME.Equals(string.Empty))
                    {
                        if (headerReportItemName[iHeader].Equals("BUSHO_NAME_LABEL"))
                        {
                            value = "部署";
                        }
                    }
                    else
                    {
                        if (headerReportItemName[iHeader].Equals("BUSHO_NAME_LABEL"))
                        {
                            value = "";
                        }
                    }
                }

                //// 部署名
                //if (headerReportItemName[iHeader].Equals("BUSHO_NAME"))
                //{
                //    value = this.dto.bushoEntity.BUSHO_NAME == null ? "" : this.dto.bushoEntity.BUSHO_NAME;

                //}
                // 201400708 syunrei ＃947　№13　　end
                // 営業担当者名
                if (headerReportItemName[iHeader].Equals("EIGYO_TANTOUSHA_NAME"))
                {
                    PropertyUtility.GetValue(this.dto.entryEntity, "SHAIN_NAME", out value);
                    value = value == null ? "" : value;
                }
                // 合計金額
                if (headerReportItemName[iHeader].Equals("GOUKEI_KINGAKU"))
                {
                    PropertyUtility.GetValue(this.dto.entryEntity, "GOUKEI_KINGAKU_TOTAL", out value);
                    if (value == null || "Null".Equals(value.ToString()) || "null".Equals(value.ToString()))
                    {
                        value = String.Empty;
                    }
                    else
                    {
                        value = CommonCalc.DecimalFormat(decimal.Parse(value.ToString().Replace(",", string.Empty)));
                    }
                }

                // 見積日付印字
                if (headerReportItemName[iHeader].Equals("MITSUMORI_DATE"))
                {
                    object valueInji;
                    PropertyUtility.GetValue(this.dto.entryEntity, "MITSUMORI_INJI_DATE", out valueInji);
                    if (valueInji == null || "Null".Equals(valueInji.ToString()))
                    {
                        valueInji = String.Empty;
                    }
                    if (value == null || "Null".Equals(value.ToString()))
                    {
                        value = String.Empty;
                    }

                    if ("True".Equals(valueInji.ToString()))
                    {
                        DateTime dt;
                        string strdate = string.Empty;
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
                    else
                    {
                        value = string.Empty;
                    }
                }

                // 見積項目セット
                if (headerReportItemName[iHeader].Equals("MITSUMORI_KOUMOKU1"))
                {
                    value = this.dto.sysInfoEntity.MITSUMORI_KOUMOKU1 == null ? "" : this.dto.sysInfoEntity.MITSUMORI_KOUMOKU1;
                }
                else if (headerReportItemName[iHeader].Equals("MITSUMORI_KOUMOKU2"))
                {
                    value = this.dto.sysInfoEntity.MITSUMORI_KOUMOKU2 == null ? "" : this.dto.sysInfoEntity.MITSUMORI_KOUMOKU2;
                }
                else if (headerReportItemName[iHeader].Equals("MITSUMORI_KOUMOKU3"))
                {
                    value = this.dto.sysInfoEntity.MITSUMORI_KOUMOKU3 == null ? "" : this.dto.sysInfoEntity.MITSUMORI_KOUMOKU3;
                }
                else if (headerReportItemName[iHeader].Equals("MITSUMORI_KOUMOKU4"))
                {
                    value = this.dto.sysInfoEntity.MITSUMORI_KOUMOKU4 == null ? "" : this.dto.sysInfoEntity.MITSUMORI_KOUMOKU4;
                }
                else if (headerReportItemName[iHeader].Equals("MITSUMORI_KOUMOKU5"))
                {
                    value = this.dto.sysInfoEntity.MITSUMORI_KOUMOKU5 == null ? "" : this.dto.sysInfoEntity.MITSUMORI_KOUMOKU5;
                }

                // 会社名
                if (headerReportItemName[iHeader].Equals("CORP_NAME"))
                {
                    value = this.dto.corpEntity.CORP_NAME == null ? "" : this.dto.corpEntity.CORP_NAME;
                }
                // 代表名
                if (headerReportItemName[iHeader].Equals("CORP_DAIHYOU") && this.getDaihyouPrintKbn(this.form.txt_TorihikisakiCD.Text))
                {
                    value = this.dto.corpEntity.CORP_DAIHYOU == null ? "" : this.dto.corpEntity.CORP_DAIHYOU;
                }

                // 印字拠点名1
                if (headerReportItemName[iHeader].Equals("KYOTEN_NAME_1"))
                {
                    value = this.dto.kyotenEntity1.KYOTEN_NAME_RYAKU == null ? "" : this.dto.kyotenEntity1.KYOTEN_NAME;
                }
                // 印字拠点郵便番号1
                if (headerReportItemName[iHeader].Equals("KYOTEN_POST_1"))
                {
                    value = this.dto.kyotenEntity1.KYOTEN_POST == null ? "" : "〒" + this.dto.kyotenEntity1.KYOTEN_POST;
                }

                // 印字拠点住所1_1
                if (headerReportItemName[iHeader].Equals("KYOTEN_ADDRESS1_1"))
                {
                    value = this.dto.kyotenEntity1.KYOTEN_ADDRESS1 == null ? "" : this.dto.kyotenEntity1.KYOTEN_ADDRESS1;
                }

                // 印字拠点住所2_1
                if (headerReportItemName[iHeader].Equals("KYOTEN_ADDRESS2_1"))
                {
                    value = this.dto.kyotenEntity1.KYOTEN_ADDRESS2 == null ? "" : this.dto.kyotenEntity1.KYOTEN_ADDRESS2;
                }

                // 印字拠点TEL1
                if (headerReportItemName[iHeader].Equals("KYOTEN_TEL_1"))
                {
                    value = this.dto.kyotenEntity1.KYOTEN_TEL == null ? "" : "TEL " + this.dto.kyotenEntity1.KYOTEN_TEL;
                }

                // 印字拠点FAX2
                if (headerReportItemName[iHeader].Equals("KYOTEN_FAXL_1"))
                {
                    value = this.dto.kyotenEntity1.KYOTEN_FAX == null ? "" : "FAX " + this.dto.kyotenEntity1.KYOTEN_FAX;
                }

                // 印字拠点名2
                if (headerReportItemName[iHeader].Equals("KYOTEN_NAME_2"))
                {
                    value = this.dto.kyotenEntity2.KYOTEN_NAME_RYAKU == null ? "" : this.dto.kyotenEntity2.KYOTEN_NAME;
                }
                // 印字拠点郵便番号2
                if (headerReportItemName[iHeader].Equals("KYOTEN_POST_2"))
                {
                    value = this.dto.kyotenEntity2.KYOTEN_POST == null ? "" : "〒" + this.dto.kyotenEntity2.KYOTEN_POST;
                }

                // 印字拠点住所2_2
                if (headerReportItemName[iHeader].Equals("KYOTEN_ADDRESS1_2"))
                {
                    value = this.dto.kyotenEntity2.KYOTEN_ADDRESS1 == null ? "" : this.dto.kyotenEntity2.KYOTEN_ADDRESS1;
                }

                // 印字拠点住所2_2
                if (headerReportItemName[iHeader].Equals("KYOTEN_ADDRESS2_2"))
                {
                    value = this.dto.kyotenEntity2.KYOTEN_ADDRESS2 == null ? "" : this.dto.kyotenEntity2.KYOTEN_ADDRESS2;
                }

                // 印字拠点TEL2
                if (headerReportItemName[iHeader].Equals("KYOTEN_TEL_2"))
                {
                    value = this.dto.kyotenEntity2.KYOTEN_TEL == null ? "" : "TEL " + this.dto.kyotenEntity2.KYOTEN_TEL;
                }

                // 印字拠点FAX2
                if (headerReportItemName[iHeader].Equals("KYOTEN_FAXL_2"))
                {
                    value = this.dto.kyotenEntity2.KYOTEN_FAX == null ? "" : "FAX " + this.dto.kyotenEntity2.KYOTEN_FAX;
                }

                // 帳票データに設定
                dataTableHeaderRow[iHeader] = value;
            }

            dataTableHeader.Rows.Add(dataTableHeaderRow);

            LogUtility.DebugMethodEnd();

            return dataTableHeader;
        }

        #endregion ヘッダ（１ページ）

        #region 明細（１ページ）

        /// <summary>
        /// 明細（１ページ）
        /// </summary>
        private DataTable getPageDetail(int pageNo)
        {
            LogUtility.DebugMethodStart(pageNo);

            // Detail部
            DataTable dataTableDetail = new DataTable();
            // 明細部
            for (int iDetail = 0; iDetail < detailReportItemName.Length; iDetail++)
            {
                dataTableDetail.Columns.Add(detailReportItemName[iDetail], typeof(string));
            }

            // 数量フォーマット取得
            string suuryouFormatStr = this.mSysInfo.SYS_SUURYOU_FORMAT;
            // 単価フォーマット取得
            string tankaFormatStr = this.mSysInfo.SYS_TANKA_FORMAT;

            // 総ページ
            SqlInt16 totalPageNo = 0;

            // rowを取得
            SqlInt16 intRow = 0;

            DataRow dataTableDetailRow;

            // CreateDataTableForEntityがMultiRowを動的に作成しないため、ここでEntity分行数を追加する
            // Entity数Rowを作ると最終行が無いので、Etity + 1でループさせる
            for (int row = 0; row < this.dto.detailEntity.Length; row++)
            {
                // ページ番号を取得
                SqlInt16 pageNumber = this.dto.detailEntity[row].PAGE_NUMBER;

                // 小計行存在フラグ
                bool subTotal = false;

                if (pageNumber == SqlInt16.Parse((pageNo).ToString()))
                {
                    dataTableDetailRow = dataTableDetail.NewRow();

                    for (int iDetail = 0; iDetail < detailReportItemName.Length; iDetail++)
                    {
                        object value;
                        PropertyUtility.GetValue(this.dto.detailEntity[row], detailReportItemName[iDetail], out value);

                        if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                        {
                            value = String.Empty;
                        }

                        // 画面種類
                        switch (this.form.formParem.cyohyoType)
                        {
                            case MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE: // 見積書（縦）
                            case MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO: // 見積書（横）
                                if (detailReportItemName[iDetail].Equals("DENPYOU_KBN"))
                                {
                                    PropertyUtility.GetValue(this.dto.detailEntity[row], "DENPYOU_KBN_CD", out value);

                                    if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                                    {
                                        value = String.Empty;
                                    }
                                    else
                                    {
                                        value = denpyouKbnDictionary[short.Parse(value.ToString())].DENPYOU_KBN_NAME_RYAKU;
                                    }
                                }

                                if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                                {
                                    value = String.Empty;
                                }

                                if (detailReportItemName[iDetail].Equals("TAX"))
                                {
                                    PropertyUtility.GetValue(this.dto.detailEntity[row], "TAX_SOTO", out value);

                                    if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                                    {
                                        value = String.Empty;
                                    }
                                }

                                break;

                            case MitumorisyoConst.MITUMOTISYO_TANKA_TATE:   // 単価見積書（縦）
                            case MitumorisyoConst.MITUMOTISYO_TANKA_YOKO:   // 単価見積書（横）
                                if (detailReportItemName[iDetail].Equals("DENPYOU_KBN_CD"))
                                {
                                    if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                                    {
                                        value = String.Empty;
                                    }
                                    else
                                    {
                                        value = denpyouKbnDictionary[short.Parse(value.ToString())].DENPYOU_KBN_NAME_RYAKU;
                                    }
                                }

                                if (detailReportItemName[iDetail].Equals("KINGAKU"))
                                {
                                    if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                                    {
                                        value = String.Empty;
                                    }
                                    else
                                    {
                                        value = CommonCalc.DecimalFormat(decimal.Parse(value.ToString().Replace(",", string.Empty)));
                                    }

                                    if (detailReportItemName[iDetail].Equals("TAX"))
                                    {
                                        PropertyUtility.GetValue(this.dto.detailEntity[row], "TAX_SOTO", out value);

                                        if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                                        {
                                            value = String.Empty;
                                        }
                                        else
                                        {
                                            value = CommonCalc.DecimalFormat(decimal.Parse(value.ToString().Replace(",", string.Empty)));
                                        }
                                    }
                                }

                                break;
                        }

                        if (detailReportItemName[iDetail].Equals("HINMEI_ZEI_KBN_CD"))
                        {
                            if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                            {
                                value = String.Empty;
                            }
                            else
                            {
                                // 税区分
                                switch (value.ToString())
                                {
                                    case "1": // 外税

                                        value = "外税";
                                        break;

                                    case "2": // 内税

                                        value = "内税";
                                        break;

                                    case "3": // 非課税

                                        value = "税抜";
                                        break;

                                    default:
                                        value = string.Empty;
                                        break;
                                }
                            }
                        }

                        if (detailReportItemName[iDetail].Equals("UNIT_NAME"))
                        {
                            PropertyUtility.GetValue(this.dto.detailEntity[row], "UNIT_CD", out value);

                            if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                            {
                                value = String.Empty;
                            }
                            else
                            {
                                value = this.GetUnitName(short.Parse(value.ToString()));
                            }
                        }

                        if (detailReportItemName[iDetail].Equals("SUURYOU"))
                        {
                            if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                            {
                                value = String.Empty;
                            }
                            else
                            {
                                value = this.ToAmountValue(value.ToString().Replace(",", string.Empty), suuryouFormatStr);
                            }
                        }
                        if (detailReportItemName[iDetail].Equals("TANKA"))
                        {
                            if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                            {
                                value = String.Empty;
                            }
                            else
                            {
                                value = this.ToAmountValue(value.ToString().Replace(",", string.Empty), tankaFormatStr);
                            }
                        }

                        // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                        // 明細金額
                        if (detailReportItemName[iDetail].Equals("HINMEI_KINGAKU"))
                        {
                            if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                            {
                                value = String.Empty;
                            }
                            else
                            {
                                value = CommonCalc.DecimalFormat(decimal.Parse(value.ToString().Replace(",", string.Empty)));
                            }
                        }

                        // 消費税
                        if (detailReportItemName[iDetail].Equals("HINMEI_TAX_SOTO"))
                        {
                            if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                            {
                                value = String.Empty;
                            }
                            else
                            {
                                value = CommonCalc.DecimalFormat(decimal.Parse(value.ToString().Replace(",", string.Empty)));
                            }
                        }
                        // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end

                        subTotal = false;
                        if (detailReportItemName[iDetail].Equals("DENPYOU_NUMBER"))
                        {
                            PropertyUtility.GetValue(this.dto.detailEntity[row], detailReportItemName[iDetail], out value);
                            if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                            {
                                subTotal = true;
                                break;
                            }
                        }

                        dataTableDetailRow[iDetail] = value;
                    }

                    // 小計行を外す
                    if (!subTotal)
                    {
                        dataTableDetail.Rows.Add(dataTableDetailRow);
                    }

                    ////subTotal = false;
                    //if (detailReportItemName[iDetail].Equals("DENPYOU_NUMBER"))
                    //{
                    //    PropertyUtility.GetValue(this.dto.detailEntity[row], detailReportItemName[iDetail], out value);

                    //    if (value == null || "Null".Equals(value.ToString()))
                    //    {
                    //        value = String.Empty;
                    //    }

                    //    subTotal = true;

                    //}

                    // 小計行を表示対応
                    //if (detailReportItemName[iDetail].Equals("HINMEI_NAME"))
                    //{
                    //    PropertyUtility.GetValue(this.dto.detailEntity[row], detailReportItemName[iDetail], out value);
                    //    if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                    //    {
                    //        value = MitumorisyoConst.SUB_TOTAL;

                    //        subTotal = true;

                    //    }
                    //}

                    //dataTableDetailRow[iDetail] = value;
                }
            }

            LogUtility.DebugMethodEnd();

            return dataTableDetail;
        }

        #endregion 明細（１ページ）

        #region フッタ（１ページ）

        /// <summary>
        /// フッタ（１ページ）
        /// </summary>
        private DataTable getPageFooter(int pageNo)
        {
            LogUtility.DebugMethodStart(pageNo);

            // Footer部
            DataTable dataTableFooter = new DataTable();

            // フッタ部
            for (int iFooder = 0; iFooder < footerReportItemName.Length; iFooder++)
            {
                dataTableFooter.Columns.Add(footerReportItemName[iFooder], typeof(string));
            }

            DataRow dataTableFooterRow = dataTableFooter.NewRow();

            for (int iFooder = 0; iFooder < footerReportItemName.Length; iFooder++)
            {
                object value;
                PropertyUtility.GetValue(this.dto.entryEntity, footerReportItemName[iFooder], out value);
                if (value == null)
                {
                    value = String.Empty;
                }

                // 画面種類
                switch (this.form.formParem.cyohyoType)
                {
                    case MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE: // 見積書（縦）
                    case MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO: // 見積書（横）

                        if (footerReportItemName[iFooder].Equals("PRICE_PROPER"))
                        {
                            value = this.form.txt_KazeiTaisyoGaku.Text;

                            if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                            {
                                value = String.Empty;
                            }
                            else
                            {
                                value = CommonCalc.DecimalFormat(decimal.Parse(value.ToString().Replace(",", string.Empty)));
                            }
                        }

                        if (this.form.formParem.cyohyoType == MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE || this.form.formParem.cyohyoType == MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO)
                        {
                            if (footerReportItemName[iFooder].Equals("KINGAKU_TOTAL"))
                            {
                                value = this.form.txt_KingakuTotal.Text;

                                if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                                {
                                    value = String.Empty;
                                }
                                else
                                {
                                    value = CommonCalc.DecimalFormat(decimal.Parse(value.ToString().Replace(",", string.Empty)));
                                }
                            }

                            // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                            if (footerReportItemName[iFooder].Equals("TAX_SOTO"))
                            {
                                value = this.form.txt_TaxSoto.Text;

                                if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                                {
                                    value = String.Empty;
                                }
                                else
                                {
                                    value = CommonCalc.DecimalFormat(decimal.Parse(value.ToString().Replace(",", string.Empty)));
                                }
                            }
                            // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end

                            if (footerReportItemName[iFooder].Equals("GOUKEI_KINGAKU_TOTAL"))
                            {
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 start
                                value = this.form.txt_GoukeiKingakuTotal.Text;
                                // 201407011 chinchisi [環境将軍R 標準版 - 開発 #947]_№18 end

                                if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                                {
                                    value = String.Empty;
                                }
                                else
                                {
                                    value = CommonCalc.DecimalFormat(decimal.Parse(value.ToString().Replace(",", string.Empty)));
                                }
                            }
                        }
                        else
                        {
                            if (footerReportItemName[iFooder].Equals("KINGAKU_TOTAL"))
                            {
                                if (footerReportItemName[iFooder].Equals("TAX_SOTO"))
                                {
                                    value = this.form.txt_TaxSoto.Text;

                                    if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                                    {
                                        value = String.Empty;
                                    }
                                    else
                                    {
                                        value = CommonCalc.DecimalFormat(decimal.Parse(value.ToString().Replace(",", string.Empty)));
                                    }
                                }
                            }
                            if (footerReportItemName[iFooder].Equals("GOUKEI_KINGAKU_TOTAL"))
                            {
                                value = this.form.txt_TaxSoto.Text;

                                if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                                {
                                    value = String.Empty;
                                }
                                else
                                {
                                    value = CommonCalc.DecimalFormat(decimal.Parse(value.ToString().Replace(",", string.Empty)));
                                }
                            }
                        }

                        break;

                    case MitumorisyoConst.MITUMOTISYO_TANKA_TATE:   // 単価見積書（縦）
                    case MitumorisyoConst.MITUMOTISYO_TANKA_YOKO:   // 単価見積書（横）

                        if (footerReportItemName[iFooder].Equals("PRICE_PROPER"))
                        {
                            value = this.form.txt_KazeiTaisyoGaku.Text;

                            if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                            {
                                value = String.Empty;
                            }
                            else
                            {
                                value = CommonCalc.DecimalFormat(decimal.Parse(value.ToString().Replace(",", string.Empty)));
                            }
                        }

                        if (this.form.formParem.cyohyoType == MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE || this.form.formParem.cyohyoType == MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO)
                        {
                            if (footerReportItemName[iFooder].Equals("KINGAKU_TOTAL"))
                            {
                                value = this.form.txt_KingakuTotal.Text;

                                if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                                {
                                    value = String.Empty;
                                }
                                else
                                {
                                    value = CommonCalc.DecimalFormat(decimal.Parse(value.ToString().Replace(",", string.Empty)));
                                }
                            }

                            if (footerReportItemName[iFooder].Equals("TAX_SOTO"))
                            {
                                value = this.form.txt_TaxSoto.Text;

                                if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                                {
                                    value = String.Empty;
                                }
                                else
                                {
                                    value = CommonCalc.DecimalFormat(decimal.Parse(value.ToString().Replace(",", string.Empty)));
                                }
                            }

                            if (footerReportItemName[iFooder].Equals("GOUKEI_KINGAKU_TOTAL"))
                            {
                                value = this.form.txt_KingakuTotal.Text;

                                if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                                {
                                    value = String.Empty;
                                }
                                else
                                {
                                    value = CommonCalc.DecimalFormat(decimal.Parse(value.ToString().Replace(",", string.Empty)));
                                }
                            }
                        }
                        else
                        {
                            if (footerReportItemName[iFooder].Equals("KINGAKU_TOTAL"))
                            {
                                if (footerReportItemName[iFooder].Equals("TAX_SOTO"))
                                {
                                    value = this.form.txt_TaxSoto.Text;

                                    if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                                    {
                                        value = String.Empty;
                                    }
                                    else
                                    {
                                        value = CommonCalc.DecimalFormat(decimal.Parse(value.ToString().Replace(",", string.Empty)));
                                    }
                                }
                            }
                            if (footerReportItemName[iFooder].Equals("GOUKEI_KINGAKU_TOTAL"))
                            {
                                value = this.form.txt_TaxSoto.Text;

                                if (value == null || "Null".Equals(value.ToString()) || string.IsNullOrEmpty(value.ToString()))
                                {
                                    value = String.Empty;
                                }
                                else
                                {
                                    value = CommonCalc.DecimalFormat(decimal.Parse(value.ToString().Replace(",", string.Empty)));
                                }
                            }
                        }

                        break;
                }

                dataTableFooterRow[iFooder] = value;
            }

            dataTableFooter.Rows.Add(dataTableFooterRow);

            LogUtility.DebugMethodEnd();

            return dataTableFooter;
        }

        #endregion フッタ（１ページ）

        #region 行コピーイベント

        /// <summary>
        /// 行コピーイベント
        /// </summary>
        /// <param name="rowNO"></param>
        public virtual bool RowCopy(int rowNO)
        {
            LogUtility.DebugMethodStart(rowNO);
            bool ret = true;
            try
            {
                // DetailEntityを作成
                this.CreateMitsumoriDetailEntity();

                if (this.dto.detailEntity.Length > 0)
                {
                    // 選択したページをコピー
                    for (int i = 0; i < this.dto.detailEntity.Length; i++)
                    {
                        // コピー条件（ページ、行、集計フラグ＝false）
                        if ((this.form.tbc_MitsumoriMeisai.SelectedTab.TabIndex == this.dto.detailEntity[i].PAGE_NUMBER.Value - 1) && (this.dto.detailEntity[i].ROW_NO.Value == rowNO))
                        {
                            if (!(this.dto.detailEntity[i].SHOUKEI_FLG.Value))
                            {
                                copyDetail = this.dto.MitsumoriDetailClone(this.dto.detailEntity[i]);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    // 判定用なのでnull設定する。
                    copyDetail = null;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RowCopy", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            LogUtility.DebugMethodEnd();

            return ret;
        }

        #endregion 行コピーイベント

        #region 行ペーストイベント

        /// <summary>
        /// 行ペーストイベント
        /// </summary>
        /// <param name="rowNO"></param>
        public virtual bool RowPast(int rowNO)
        {
            LogUtility.DebugMethodStart(rowNO);
            bool ret = true;
            try
            {
                if (copyDetail != null)
                {
                    if (!this.copyDetail.SHOUKEI_FLG.IsNull)
                    {
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SHOUKEI_FLG].Value = this.copyDetail.SHOUKEI_FLG;
                    }
                    if (!string.IsNullOrEmpty(this.copyDetail.HINMEI_CD))
                    {
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value = this.copyDetail.HINMEI_CD;
                    }
                    else
                    {
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(this.copyDetail.HINMEI_NAME))
                    {
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME].Value = this.copyDetail.HINMEI_NAME;
                    }
                    else
                    {
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME].Value = string.Empty;
                    }
                    // 税区分
                    // -----------------
                    DataGridViewComboBoxCell cbc = (DataGridViewComboBoxCell)this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD];

                    SqlInt16 zeikbn = 0;

                    zeikbn = this.copyDetail.HINMEI_ZEI_KBN_CD;

                    zeikbn = zeikbn.IsNull ? 0 : zeikbn;

                    cbc.Value = "0".Equals(zeikbn.Value.ToString()) ? String.Empty : cbc.Items[int.Parse(zeikbn.Value.ToString()) - 1];

                    // -----------------

                    if (!this.copyDetail.SUURYOU.IsNull)
                    {
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SUURYOU].Value = decimal.Parse(this.copyDetail.SUURYOU.ToString().Replace(",", string.Empty));
                    }
                    else
                    {
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SUURYOU].Value = string.Empty;
                    }
                    if (!this.copyDetail.UNIT_CD.IsNull)
                    {
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD].Value = this.copyDetail.UNIT_CD.ToString();
                        // 単位名
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_NAME].Value = this.GetUnitName((short)this.copyDetail.UNIT_CD);
                    }
                    else
                    {
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD].Value = string.Empty;
                        // 単位名
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_NAME].Value = string.Empty;
                    }

                    if (!this.copyDetail.TANKA.IsNull)
                    {
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value = decimal.Parse(this.copyDetail.TANKA.ToString().Replace(",", string.Empty));
                    }
                    else
                    {
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value = string.Empty;
                    }

                    // 画面の税区分
                    SqlInt16 zeikbn1 = cbc.Value == null ? 0 : SqlInt16.Parse(cbc.RowIndex.ToString());

                    // 品名税区分
                    SqlInt16 hinmeiZeikbn = 0;
                    if (string.IsNullOrEmpty(this.copyDetail.HINMEI_CD))
                    {
                        hinmeiZeikbn = SqlInt16.Null;
                    }
                    else
                    {
                        hinmeiZeikbn = this.accessor.GetHinmeiDataByCd(this.copyDetail.HINMEI_CD.ToString()).ZEI_KBN_CD;
                    }
                    hinmeiZeikbn = hinmeiZeikbn.IsNull ? 0 : hinmeiZeikbn;

                    // 品名マスタと見積明細の税区分比較
                    if (hinmeiZeikbn.Value == zeikbn.Value)
                    {
                        if (!this.copyDetail.HINMEI_KINGAKU.IsNull)
                        {
                            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value = CommonCalc.DecimalFormat(decimal.Parse(this.copyDetail.HINMEI_KINGAKU.ToString().Replace(",", string.Empty)));
                        }
                        else
                        {
                            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value = string.Empty;
                        }

                        if (!this.copyDetail.HINMEI_TAX_SOTO.IsNull)
                        {
                            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat(decimal.Parse(this.copyDetail.HINMEI_TAX_SOTO.ToString().Replace(",", string.Empty)));
                        }
                        else
                        {
                            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = string.Empty;
                        }
                        if (!this.copyDetail.HINMEI_TAX_UCHI.IsNull)
                        {
                            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value = CommonCalc.DecimalFormat(decimal.Parse(this.copyDetail.HINMEI_TAX_UCHI.ToString().Replace(",", string.Empty)));
                        }
                        else
                        {
                            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value = string.Empty;
                        }
                    }
                    else
                    {
                        if (!this.copyDetail.KINGAKU.IsNull)
                        {
                            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value = CommonCalc.DecimalFormat(decimal.Parse(this.copyDetail.KINGAKU.ToString().Replace(",", string.Empty)));
                        }
                        else
                        {
                            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value = string.Empty;
                        }
                        if (!this.copyDetail.TAX_SOTO.IsNull)
                        {
                            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat(decimal.Parse(this.copyDetail.TAX_SOTO.ToString().Replace(",", string.Empty)));
                        }
                        else
                        {
                            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = string.Empty;
                        }

                        if (!this.copyDetail.TAX_UCHI.IsNull)
                        {
                            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value = CommonCalc.DecimalFormat(decimal.Parse(this.copyDetail.TAX_UCHI.ToString().Replace(",", string.Empty)));
                        }
                        else
                        {
                            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value = string.Empty;
                        }
                    }

                    if (!string.IsNullOrEmpty(this.copyDetail.MEISAI_BIKOU))
                    {
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_MEISAI_BIKOU].Value = this.copyDetail.MEISAI_BIKOU;
                    }
                    else
                    {
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_MEISAI_BIKOU].Value = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(this.copyDetail.MEISAI_TEKIYO))
                    {
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_MEISAI_TEKIYOU].Value = this.copyDetail.MEISAI_TEKIYO;
                    }
                    else
                    {
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_MEISAI_TEKIYOU].Value = string.Empty;
                    }
                    if (!this.copyDetail.DENPYOU_KBN_CD.IsNull)
                    {
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value = this.copyDetail.DENPYOU_KBN_CD;
                        // 伝票区分名
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME].Value = this.GetDataByCodeForDenpyouKbnName((short)this.copyDetail.DENPYOU_KBN_CD);
                    }
                    else
                    {
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value = string.Empty;
                        // 伝票区分名
                        this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME].Value = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RowPast", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }

            LogUtility.DebugMethodEnd();
            return ret;
        }

        #endregion 行ペーストイベント

        #region 小計イベント

        /// <summary>
        /// 小計イベント
        /// </summary>
        /// <param name="rowNO"></param>
        public virtual bool SubTotal(int rowNO)
        {
            LogUtility.DebugMethodStart(rowNO);
            bool ret = true;
            try
            {
                // DetailEntityを作成
                this.CreateMitsumoriDetailEntity();

                //　小計行を入力禁止
                this.SubTotalRowReadOnly(rowNO);

                // 小計の計算
                if (!this.Calculate(string.Empty))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SubTotal", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }

        #endregion 小計イベント

        #region 小計行を入力禁止

        /// <summary>
        /// 小計イベント
        /// </summary>
        /// <param name="rowNO"></param>
        public virtual void SubTotalRowReadOnly(int rowNO)
        {
            LogUtility.DebugMethodStart(rowNO);

            // [伝票区分CD]に0(=ブランク)をセット。
            if (!this.dto.detailEntity[rowNO].DENPYOU_KBN_CD.IsNull)
            {
                this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value = String.Empty;
            }

            // 変更不可
            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_NO].ReadOnly = true;
            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_SYS_ID].ReadOnly = true;
            // 明細システムIDを削除
            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_SYS_ID].Value = null;

            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SHOUKEI_FLG].ReadOnly = true;
            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].ReadOnly = true;
            // 品名(小計)必須項目外す
            ((DgvCustomAlphaNumTextBoxCell)this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD]).FocusOutCheckMethod = null;
            ((DgvCustomAlphaNumTextBoxCell)this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD]).PopupWindowId = WINDOW_ID.MAIN_MENU;
            ((DgvCustomAlphaNumTextBoxCell)this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD]).PopupWindowName = null;

            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME].ReadOnly = true;
            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SUURYOU].ReadOnly = true;
            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD].ReadOnly = true;
            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_NAME].ReadOnly = true;
            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].ReadOnly = true;
            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].ReadOnly = true;
            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].ReadOnly = true;
            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].ReadOnly = true;
            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].ReadOnly = true;
            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_MEISAI_BIKOU].ReadOnly = true;
            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_MEISAI_TEKIYOU].ReadOnly = true;
            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].ReadOnly = true;
            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME].ReadOnly = true;

            //　小計
            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME].Value = MitumorisyoConst.SUB_TOTAL;
            // [小計フラグ]に1(=小計行)をセット。)
            this.form.CustomDataGridView.Rows[rowNO].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SHOUKEI_FLG].Value = "True";

            //// 小計の計算
            //this.Calculate(string.Empty);

            LogUtility.DebugMethodEnd();
        }

        #endregion 小計行を入力禁止

        #region 小計の計算

        /// <summary>
        /// 小計の計算
        /// </summary>
        internal bool Calculate(String cellname)
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
                short kingakuHasuuCd = 0;
                // 20140715 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                short taxHasuuCd = 0;
                // 20140715 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end

                decimal pageKingakuTotal = 0;
                decimal pageSotoTotal = 0;
                decimal pageUchiTotal = 0;
                decimal pageNotExistsHinmeiZeiKingakuTotal = 0;

                M_HINMEI targetHimei = new M_HINMEI();

                decimal sotoZeiToal = 0;
                decimal uchiZeiToal = 0;

                // ページ
                for (int page = 0; page < this.form.tbc_MitsumoriMeisai.TabPages.Count; page++)
                {
                    // 税計算区分CD (2.明細毎)
                    if (this.form.txt_ZeiKeisanKbnCD.Text.Equals("2"))
                    {
                        sotoZeiToal = 0;
                        uchiZeiToal = 0;
                    }

                    decimal kingakuTotal = 0;

                    decimal notExistHinmeiZeiKingakuTotal = 0;

                    // 画面小計表示用
                    decimal subKingakuTotal = 0;
                    decimal subSotoTotal = 0;
                    decimal subUchiTotal = 0;

                    // 小計フラグ
                    SqlBoolean blnSub = false;

                    // CustomDataGridViewを取得する
                    CustomDataGridView cdgv = ((CustomDataGridView)this.form.tbc_MitsumoriMeisai.TabPages[page].Controls["control"].Controls["CustomDataGridView"]);

                    // 明細に対して、計算を行う
                    for (int i = 0; i < cdgv.Rows.Count - 1; i++)
                    {
                        DataGridViewRow dr = cdgv.Rows[i];

                        targetHimei = new M_HINMEI();

                        if (cdgv[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, i].Value != null)
                        {
                            targetHimei = this.accessor.GetHinmeiDataByCd(cdgv[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, i].Value.ToString());
                        }

                        // 端数取得
                        kingakuHasuuCd = CalcHasuu(dr);

                        // 20140715 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                        // 消費税端数取得
                        taxHasuuCd = TaxCalcHasuu(dr);
                        // 20140715 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end

                        // 小計行計算処理

                        blnSub = false;

                        // [小計フラグ]を取得する。
                        if (cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SHOUKEI_FLG].Value != null)
                        {
                            blnSub = SqlBoolean.Parse(cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SHOUKEI_FLG].Value.ToString());
                        }

                        // 金額
                        decimal kingaku = 0;
                        // 数量
                        decimal suuryou = 0;
                        // 単価
                        decimal tanka = 0;
                        // 外税
                        decimal sotoZei = 0;
                        // 内税
                        decimal uchiZei = 0;
                        // 品名税区分が設定されていない金額
                        decimal notExistHinmeiZeKingaku = 0;

                        decimal.TryParse(Convert.ToString(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value), out kingaku);
                        decimal.TryParse(Convert.ToString(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SUURYOU].Value), out suuryou);
                        decimal.TryParse(Convert.ToString(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value), out tanka);
                        decimal.TryParse(Convert.ToString(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value), out sotoZei);
                        decimal.TryParse(Convert.ToString(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value), out uchiZei);

                        // 20140710 chinchisi EV002428 内税の計算を修正する。 start
                        // マスタに品名の税区分　設定かとか
                        bool zei_kbn_type = true;
                        if (dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value == null || (string.IsNullOrEmpty(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString())) || (dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value != null && !string.IsNullOrEmpty(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString()) && !((dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1")) || dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("2") || dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("3"))))
                        {
                            zei_kbn_type = false;

                            if (cellname != MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU)
                            {
                                notExistHinmeiZeKingaku = CommonCalc.FractionCalc((decimal)suuryou * tanka, kingakuHasuuCd);
                                if (notExistHinmeiZeKingaku.ToString().Length > 9)
                                {
                                    notExistHinmeiZeKingaku = Convert.ToDecimal(notExistHinmeiZeKingaku.ToString().Substring(0, 9));
                                }
                            }
                            else
                            {
                                notExistHinmeiZeKingaku = kingaku;
                            }

                            String denpyouKbn = Convert.ToString(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value);
                            if (!string.IsNullOrEmpty(denpyouKbn) && denpyouKbn != this.ChgDBNullToValue(this.dto.sysInfoEntity.UR_SH_CALC_BASE_KBN, string.Empty).ToString())
                            {
                                notExistHinmeiZeKingaku = notExistHinmeiZeKingaku * -1;
                            }
                        }

                        // 明細行計算する
                        if (!blnSub)
                        {
                            // 伝票区分を取得
                            String denpyouKbn = Convert.ToString(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value);

                            // 税計算区分CD (1.伝票毎)
                            if (this.form.txt_ZeiKeisanKbnCD.Text.Equals("1"))
                            {
                                // 201400708 syunrei ＃947　№11　　start
                                // 非課税に設定
                                //DataGridViewComboBoxCell cbc = (DataGridViewComboBoxCell)dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD];
                                //// 201400704 syunrei EV002427_見積入力の税計算区分がシステム設定の税区分締処理利用形態を参照していない　start
                                ////cbc.Value = cbc.Items[2];
                                //cbc.Value = string.Empty;
                                //// 201400704 syunrei EV002427_見積入力の税計算区分がシステム設定の税区分締処理利用形態を参照していない　end
                                // 201400708 syunrei ＃947　№11　　end
                                // ＜売上支払情報差引基準≠[伝票区分]の場合＞
                                // [金額] × －１ の計算を行う。
                                if (!string.IsNullOrEmpty(denpyouKbn) && denpyouKbn != this.ChgDBNullToValue(this.dto.sysInfoEntity.UR_SH_CALC_BASE_KBN, string.Empty).ToString())
                                {
                                    // denpyouKbnによる計算はEntryで行い、Detailでは行わないため明細では常に+値で計算
                                    //if (kingaku > 0)
                                    //{
                                    //    kingaku = kingaku * -1;
                                    //}

                                    if (cellname != MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value != null
                                        && !String.IsNullOrEmpty(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value.ToString()))
                                    {
                                        //kingaku = CommonCalc.FractionCalc((decimal)suuryou * tanka * -1, kingakuHasuuCd);
                                        kingaku = CommonCalc.FractionCalc((decimal)suuryou * tanka, kingakuHasuuCd);
                                        if (kingaku.ToString().Length > 9)
                                        {
                                            kingaku = Convert.ToDecimal(kingaku.ToString().Substring(0, 9));
                                        }
                                    }
                                    sotoZei = 0;
                                    uchiZei = 0;
                                    decimal clm_TaxSoto = 0;
                                    if ((dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value != null && !string.IsNullOrEmpty(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString())))
                                    {
                                        if (dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("2"))
                                        {
                                            // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                            uchiZei = CommonCalc.FractionCalc(kingaku - (kingaku / (this.taxRate + 1)), taxHasuuCd);
                                            // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                            clm_TaxSoto = uchiZei;
                                        }
                                        else if (dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1"))
                                        {
                                            // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                            clm_TaxSoto = CommonCalc.FractionCalc(kingaku * this.taxRate, taxHasuuCd);
                                            // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                        }
                                    }

                                    // 税区分が外税の場合
                                    if (this.form.txt_ZeiKbnCD.Text.Equals("1"))
                                    {
                                        if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1"))
                                        {
                                            // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                            sotoZei = CommonCalc.FractionCalc(kingaku * this.taxRate, taxHasuuCd);
                                            // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                        }

                                        // 品名税区分が設定されていなければ消費税を表示しない
                                        if (targetHimei.ZEI_KBN_CD.IsNull || targetHimei.ZEI_KBN_CD.ToString() == string.Empty)
                                        {
                                            cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = string.Empty;
                                        }
                                        else
                                        {
                                            cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)clm_TaxSoto);
                                        }
                                    }
                                    else if (this.form.txt_ZeiKbnCD.Text.Equals("3"))
                                    {
                                        if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1"))
                                        {
                                            // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                            sotoZei = CommonCalc.FractionCalc(kingaku * this.taxRate, taxHasuuCd);
                                            // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                        }

                                        // 品名税区分が設定されていなければ消費税を表示しない
                                        if (targetHimei.ZEI_KBN_CD.IsNull || targetHimei.ZEI_KBN_CD.ToString() == string.Empty)
                                        {
                                            cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = string.Empty;
                                        }
                                        else
                                        {
                                            cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)clm_TaxSoto);
                                        }
                                    }

                                    if (cellname != MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU)
                                    {
                                        cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value = CommonCalc.DecimalFormat((decimal)kingaku);
                                    }
                                }
                                else
                                {
                                    if (cellname != MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value != null
                                        && !String.IsNullOrEmpty(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value.ToString()))
                                    {
                                        kingaku = CommonCalc.FractionCalc((decimal)suuryou * tanka, kingakuHasuuCd);
                                        if (kingaku.ToString().Length > 9)
                                        {
                                            kingaku = Convert.ToDecimal(kingaku.ToString().Substring(0, 9));
                                        }
                                    }
                                    sotoZei = 0;
                                    uchiZei = 0;
                                    decimal clm_TaxSoto = 0;
                                    if ((dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value != null && !string.IsNullOrEmpty(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString())))
                                    {
                                        if (dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("2"))
                                        {
                                            // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                            uchiZei = CommonCalc.FractionCalc(kingaku - (kingaku / (this.taxRate + 1)), taxHasuuCd);
                                            // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                            clm_TaxSoto = uchiZei;
                                        }
                                        else if (dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1"))
                                        {
                                            // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                            clm_TaxSoto = CommonCalc.FractionCalc(kingaku * this.taxRate, taxHasuuCd);
                                            // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                        }
                                    }
                                    // 税区分が外税の場合
                                    if (this.form.txt_ZeiKbnCD.Text.Equals("1"))
                                    {
                                        if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1"))
                                        {
                                            // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                            sotoZei = CommonCalc.FractionCalc(kingaku * this.taxRate, taxHasuuCd);
                                            // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                        }

                                        // 品名税区分が設定されていなければ消費税を表示しない
                                        if (targetHimei.ZEI_KBN_CD.IsNull || targetHimei.ZEI_KBN_CD.ToString() == string.Empty)
                                        {
                                            cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = string.Empty;
                                        }
                                        else
                                        {
                                            cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)clm_TaxSoto);
                                        }
                                    }
                                    else if (this.form.txt_ZeiKbnCD.Text.Equals("3"))
                                    {
                                        if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1"))
                                        {
                                            // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                            sotoZei = CommonCalc.FractionCalc(kingaku * this.taxRate, taxHasuuCd);
                                            // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                        }

                                        // 品名税区分が設定されていなければ消費税を表示しない
                                        if (targetHimei.ZEI_KBN_CD.IsNull || targetHimei.ZEI_KBN_CD.ToString() == string.Empty)
                                        {
                                            cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = string.Empty;
                                        }
                                        else
                                        {
                                            cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)clm_TaxSoto);
                                        }
                                    }

                                    if (cellname != MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU)
                                    {
                                        cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value = CommonCalc.DecimalFormat((decimal)kingaku);
                                    }
                                }
                            }
                            // 税計算区分CD (2.明細毎)
                            else if (this.form.txt_ZeiKeisanKbnCD.Text.Equals("2"))
                            {
                                // 20140709 ria EV002428 内税の計算を修正する。 start
                                //// 税区分CD(品名)　■外税
                                //if ((dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value != null && !string.IsNullOrEmpty(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString()) && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1")))
                                //{
                                // 税区分が外税の場合
                                if (this.form.txt_ZeiKbnCD.Text.Equals("1"))
                                {
                                    // 20140709 ria EV002428 内税の計算を修正する。 end

                                    // ＜売上支払情報差引基準≠[伝票区分]の場合＞
                                    // [金額] × －１ の計算を行う。
                                    if (!string.IsNullOrEmpty(denpyouKbn) && denpyouKbn != this.ChgDBNullToValue(this.dto.sysInfoEntity.UR_SH_CALC_BASE_KBN, string.Empty).ToString())
                                    {
                                        // denpyouKbnによる計算はEntryで行い、Detailでは行わないため明細では常に+値で計算
                                        //if (kingaku > 0)
                                        //{
                                        //    kingaku = kingaku * -1;
                                        //}
                                        sotoZei = 0;
                                        uchiZei = 0;

                                        switch (cellname)
                                        {
                                            case MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU:

                                                // 外税、内税再計算
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                if (!zei_kbn_type || (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1")))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                                    sotoZei = CommonCalc.FractionCalc(kingaku * this.taxRate, taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                }
                                                else if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("2"))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                                    uchiZei = CommonCalc.FractionCalc(kingaku - (kingaku / (this.taxRate + 1)), taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                }
                                                else
                                                {
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)0);
                                                }
                                                break;

                                            default:

                                                // denpyouKbnによる計算はEntryで行い、Detailでは行わないため明細では常に+値で計算
                                                //kingaku = CommonCalc.FractionCalc((decimal)suuryou * tanka * -1, kingakuHasuuCd);
                                                if (dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value != null
                                                    && !String.IsNullOrEmpty(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value.ToString()))
                                                {
                                                    kingaku = CommonCalc.FractionCalc((decimal)suuryou * tanka, kingakuHasuuCd);
                                                    if (kingaku.ToString().Length > 9)
                                                    {
                                                        kingaku = Convert.ToDecimal(kingaku.ToString().Substring(0, 9));
                                                    }
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value = CommonCalc.DecimalFormat((decimal)kingaku);
                                                }

                                                //sotoZei = kingaku * this.taxRate;
                                                //uchiZei = 0;
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                if (!zei_kbn_type || (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1")))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                                    sotoZei = CommonCalc.FractionCalc(kingaku * this.taxRate, taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                }
                                                else if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("2"))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                                    uchiZei = CommonCalc.FractionCalc(kingaku - (kingaku / (this.taxRate + 1)), taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                }
                                                else
                                                {
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)0);
                                                }
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        sotoZei = 0;
                                        uchiZei = 0;

                                        switch (cellname)
                                        {
                                            case MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU:

                                                // 外税、内税再計算
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                if (!zei_kbn_type || (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1")))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                                    sotoZei = CommonCalc.FractionCalc(kingaku * this.taxRate, taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                }
                                                else if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("2"))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                                    uchiZei = CommonCalc.FractionCalc(kingaku - (kingaku / (this.taxRate + 1)), taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                }
                                                else
                                                {
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)0);
                                                }
                                                break;

                                            default:

                                                if (dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value != null
                                                    && !String.IsNullOrEmpty(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value.ToString()))
                                                {
                                                    kingaku = CommonCalc.FractionCalc((decimal)suuryou * tanka, kingakuHasuuCd);
                                                    if (kingaku.ToString().Length > 9)
                                                    {
                                                        kingaku = Convert.ToDecimal(kingaku.ToString().Substring(0, 9));
                                                    }
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value = CommonCalc.DecimalFormat((decimal)kingaku);
                                                }

                                                //sotoZei = kingaku * this.taxRate;
                                                //uchiZei = 0;
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                if (!zei_kbn_type || (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1")))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                                    sotoZei = CommonCalc.FractionCalc(kingaku * this.taxRate, taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                }
                                                else if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("2"))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                                    uchiZei = CommonCalc.FractionCalc(kingaku - (kingaku / (this.taxRate + 1)), taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                }
                                                else
                                                {
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)0);
                                                }
                                                break;
                                        }
                                    }
                                }
                                // 20140709 ria EV002428 内税の計算を修正する。 start
                                //// 税区分CD(品名) ■内税
                                //else if ((dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value != null && !string.IsNullOrEmpty(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString()) && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("2")))
                                //{
                                // 税区分が内税の場合
                                else if (this.form.txt_ZeiKbnCD.Text.Equals("2"))
                                {
                                    // 20140709 ria EV002428 内税の計算を修正する。 end

                                    // ＜売上支払情報差引基準≠[伝票区分]の場合＞
                                    // [金額] × －１ の計算を行う。
                                    if (!string.IsNullOrEmpty(denpyouKbn) && denpyouKbn != this.ChgDBNullToValue(this.dto.sysInfoEntity.UR_SH_CALC_BASE_KBN, string.Empty).ToString())
                                    {
                                        // denpyouKbnによる計算はEntryで行い、Detailでは行わないため明細では常に+値で計算
                                        //if (kingaku > 0)
                                        //{
                                        //    kingaku = kingaku * -1;
                                        //}
                                        //sotoZei = 0;
                                        //uchiZei = CommonCalc.FractionCalc(kingaku - (kingaku / (this.taxRate + 1)), kingakuHasuuCd);
                                        sotoZei = 0;
                                        uchiZei = 0;

                                        switch (cellname)
                                        {
                                            case MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU:

                                                // 外税、内税再計算
                                                // 20140704 ria EV002428 内税の計算を修正する。 start
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                // 20140704 ria EV002428 内税の計算を修正する。 end
                                                if (!zei_kbn_type || (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("2")))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                                    uchiZei = CommonCalc.FractionCalc(kingaku - (kingaku / (this.taxRate + 1)), taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                }
                                                else if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1"))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                                    sotoZei = CommonCalc.FractionCalc(kingaku * this.taxRate, taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                }
                                                else
                                                {
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)0);
                                                }
                                                break;

                                            default:

                                                // denpyouKbnによる計算はEntryで行い、Detailでは行わないため明細では常に+値で計算
                                                //kingaku = CommonCalc.FractionCalc((decimal)suuryou * tanka * -1, kingakuHasuuCd);

                                                if (dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value != null
                                                    && !String.IsNullOrEmpty(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value.ToString()))
                                                {
                                                    kingaku = CommonCalc.FractionCalc((decimal)suuryou * tanka, kingakuHasuuCd);
                                                    if (kingaku.ToString().Length > 9)
                                                    {
                                                        kingaku = Convert.ToDecimal(kingaku.ToString().Substring(0, 9));
                                                    }
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value = CommonCalc.DecimalFormat((decimal)kingaku);
                                                }
                                                //sotoZei = 0;
                                                //uchiZei = CommonCalc.FractionCalc(kingaku - (kingaku / (this.taxRate + 1)), kingakuHasuuCd);
                                                // 20140704 ria EV002428 内税の計算を修正する。 start
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                // 20140704 ria EV002428 内税の計算を修正する。 end
                                                if (!zei_kbn_type || (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("2")))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                                    uchiZei = CommonCalc.FractionCalc(kingaku - (kingaku / (this.taxRate + 1)), taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                }
                                                else if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1"))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 start
                                                    sotoZei = CommonCalc.FractionCalc(kingaku * this.taxRate, taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                }
                                                else
                                                {
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)0);
                                                }
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        //sotoZei = 0;
                                        //uchiZei = CommonCalc.FractionCalc(kingaku - (kingaku / (this.taxRate + 1)), kingakuHasuuCd);
                                        sotoZei = 0;
                                        uchiZei = 0;

                                        switch (cellname)
                                        {
                                            case MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU:

                                                // 外税、内税再計算
                                                // 20140704 ria EV002428 内税の計算を修正する。 start
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                // 20140704 ria EV002428 内税の計算を修正する。 end
                                                if (!zei_kbn_type || (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("2")))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。start
                                                    uchiZei = CommonCalc.FractionCalc(kingaku - (kingaku / (this.taxRate + 1)), taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                }
                                                else if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1"))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。start
                                                    sotoZei = CommonCalc.FractionCalc(kingaku * this.taxRate, taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                }
                                                else
                                                {
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)0);
                                                }
                                                break;

                                            default:

                                                if (dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value != null
                                                    && !String.IsNullOrEmpty(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value.ToString()))
                                                {
                                                    kingaku = CommonCalc.FractionCalc((decimal)suuryou * tanka, kingakuHasuuCd);
                                                    if (kingaku.ToString().Length > 9)
                                                    {
                                                        kingaku = Convert.ToDecimal(kingaku.ToString().Substring(0, 9));
                                                    }
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value = CommonCalc.DecimalFormat((decimal)kingaku);
                                                }
                                                //sotoZei = 0;
                                                //uchiZei = CommonCalc.FractionCalc(kingaku - (kingaku / (this.taxRate + 1)), kingakuHasuuCd);
                                                // 20140704 ria EV002428 内税の計算を修正する。 start
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                // 20140704 ria EV002428 内税の計算を修正する。 end
                                                if (!zei_kbn_type || (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("2")))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。start
                                                    uchiZei = CommonCalc.FractionCalc(kingaku - (kingaku / (this.taxRate + 1)), taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                }
                                                else if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1"))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。start
                                                    sotoZei = CommonCalc.FractionCalc(kingaku * this.taxRate, taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                }
                                                else
                                                {
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)0);
                                                }
                                                break;
                                        }
                                    }
                                }
                                // 20140709 ria EV002428 内税の計算を修正する。 start
                                //// 税区分CD(品名)　■非課税
                                //else if ((dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value != null && !string.IsNullOrEmpty(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString()) && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("3")))
                                //{
                                // 税区分が内税の場合
                                else if (this.form.txt_ZeiKbnCD.Text.Equals("3"))
                                {
                                    // 20140709 ria EV002428 内税の計算を修正する。 end

                                    // ＜売上支払情報差引基準≠[伝票区分]の場合＞
                                    // [金額] × －１ の計算を行う。
                                    if (!string.IsNullOrEmpty(denpyouKbn) && denpyouKbn != this.ChgDBNullToValue(this.dto.sysInfoEntity.UR_SH_CALC_BASE_KBN, string.Empty).ToString())
                                    {
                                        // denpyouKbnによる計算はEntryで行い、Detailでは行わないため明細では常に+値で計算
                                        //if (kingaku > 0)
                                        //{
                                        //    kingaku = kingaku * -1;
                                        //}
                                        sotoZei = 0;
                                        uchiZei = 0;

                                        switch (cellname)
                                        {
                                            case MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU:

                                                // 外税、内税再計算
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1"))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。start
                                                    sotoZei = CommonCalc.FractionCalc(kingaku * this.taxRate, taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                }
                                                else if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("2"))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。start
                                                    uchiZei = CommonCalc.FractionCalc(kingaku - (kingaku / (this.taxRate + 1)), taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                }
                                                else
                                                {
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)0);
                                                }
                                                break;

                                            default:

                                                // denpyouKbnによる計算はEntryで行い、Detailでは行わないため明細では常に+値で計算
                                                //kingaku = CommonCalc.FractionCalc((decimal)suuryou * tanka * -1, kingakuHasuuCd);
                                                if (dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value != null
                                                    && !String.IsNullOrEmpty(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value.ToString()))
                                                {
                                                    kingaku = CommonCalc.FractionCalc((decimal)suuryou * tanka, kingakuHasuuCd);
                                                    if (kingaku.ToString().Length > 9)
                                                    {
                                                        kingaku = Convert.ToDecimal(kingaku.ToString().Substring(0, 9));
                                                    }
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value = CommonCalc.DecimalFormat((decimal)kingaku);
                                                }
                                                //sotoZei = 0;
                                                //uchiZei = 0;
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1"))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。start
                                                    sotoZei = CommonCalc.FractionCalc(kingaku * this.taxRate, taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                }
                                                else if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("2"))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。start
                                                    uchiZei = CommonCalc.FractionCalc(kingaku - (kingaku / (this.taxRate + 1)), taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                }
                                                else
                                                {
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)0);
                                                }
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        sotoZei = 0;
                                        uchiZei = 0;

                                        switch (cellname)
                                        {
                                            case MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU:

                                                // 外税、内税再計算
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1"))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。start
                                                    sotoZei = CommonCalc.FractionCalc(kingaku * this.taxRate, taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                }
                                                else if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("2"))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。start
                                                    uchiZei = CommonCalc.FractionCalc(kingaku - (kingaku / (this.taxRate + 1)), taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                }
                                                else
                                                {
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)0);
                                                }
                                                break;

                                            default:

                                                if (dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value != null
                                                    && !String.IsNullOrEmpty(dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value.ToString()))
                                                {
                                                    kingaku = CommonCalc.FractionCalc((decimal)suuryou * tanka, kingakuHasuuCd);
                                                    if (kingaku.ToString().Length > 9)
                                                    {
                                                        kingaku = Convert.ToDecimal(kingaku.ToString().Substring(0, 9));
                                                    }
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value = CommonCalc.DecimalFormat((decimal)kingaku);
                                                }

                                                //sotoZei = 0;
                                                //uchiZei = 0;
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                //cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_UCHI].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("1"))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。start
                                                    sotoZei = CommonCalc.FractionCalc(kingaku * this.taxRate, taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)sotoZei);
                                                }
                                                else if (zei_kbn_type && dr.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD].Value.ToString().Substring(0, 1).Equals("2"))
                                                {
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。start
                                                    uchiZei = CommonCalc.FractionCalc(kingaku - (kingaku / (this.taxRate + 1)), taxHasuuCd);
                                                    // 20140716 EV005256_消費税端数と金額端数を四捨五入にて計算している。  EV005258_明細の消費税と伝票の消費税の合計額が合わない。 end
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)uchiZei);
                                                }
                                                else
                                                {
                                                    cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)0);
                                                }
                                                break;
                                        }
                                    }
                                }
                            }

                            // 明細には+値を常に表示させるためここで補正する
                            if (!string.IsNullOrEmpty(denpyouKbn) && denpyouKbn != this.ChgDBNullToValue(this.dto.sysInfoEntity.UR_SH_CALC_BASE_KBN, string.Empty).ToString())
                            {
                                kingaku = kingaku * -1;
                                sotoZei = sotoZei * -1;
                                uchiZei = uchiZei * -1;
                            }
                        }

                        // 小計行は計算する
                        //if (!blnSub)
                        //{
                        // 金額計
                        kingakuTotal += kingaku;
                        sotoZeiToal += sotoZei;
                        uchiZeiToal += uchiZei;

                        notExistHinmeiZeiKingakuTotal += notExistHinmeiZeKingaku;

                        //小計(金額)
                        subKingakuTotal += kingaku;

                        //小計(外税)
                        subSotoTotal += sotoZei;

                        //小計(内税)
                        subUchiTotal += uchiZei;
                        // }

                        /*if (blnSub)
                        {
                            // 画面出力
                            cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU].Value = CommonCalc.DecimalFormat((decimal)subKingakuTotal);
                            cdgv.Rows[i].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO].Value = CommonCalc.DecimalFormat((decimal)subSotoTotal);

                            // 初期化
                            subKingakuTotal = 0;
                            subSotoTotal = 0;
                            subUchiTotal = 0;
                        }*/
                    }

                    // 総金額合計
                    pageKingakuTotal += kingakuTotal;
                    // 総消費税（内税）
                    pageSotoTotal += sotoZeiToal;
                    // 総消費税（外税）
                    pageUchiTotal += uchiZeiToal;
                    // 総金額（品名税区分が無い金額の合計）
                    pageNotExistsHinmeiZeiKingakuTotal += notExistHinmeiZeiKingakuTotal;
                }

                // 税計算区分CD (1.伝票毎)
                if (this.form.txt_ZeiKeisanKbnCD.Text.Equals("1"))
                {
                    // 税区分が外税の場合
                    if (this.form.txt_ZeiKbnCD.Text.Equals("1"))
                    {
                        // 金額計
                        this.form.txt_KingakuTotal.Text = CommonCalc.DecimalFormat(pageKingakuTotal);
                        // 消費税（外税）
                        //this.form.txt_TaxSoto.Text = CommonCalc.DecimalFormat(pageKingakuTotal * this.taxRate);
                        this.form.txt_TaxSoto.Text = CommonCalc.DecimalFormat(CommonCalc.FractionCalc(pageNotExistsHinmeiZeiKingakuTotal * this.taxRate, taxHasuuCd) + sotoZeiToal + uchiZeiToal);
                        // 課税対象金額
                        this.form.txt_KazeiTaisyoGaku.Text = CommonCalc.DecimalFormat(pageKingakuTotal - uchiZeiToal);
                        // 内税総額
                        this.form.txt_UchiTotal.Text = CommonCalc.DecimalFormat(uchiZeiToal);
                        // 総合計金額
                        this.form.txt_GoukeiKingakuTotal.Text = CommonCalc.DecimalFormat(pageKingakuTotal + CommonCalc.FractionCalc(pageNotExistsHinmeiZeiKingakuTotal * this.taxRate, taxHasuuCd) + sotoZeiToal);
                    }
                    // 税区分が内税の場合
                    else if (this.form.txt_ZeiKbnCD.Text.Equals("2"))
                    {
                        // 金額計
                        this.form.txt_KingakuTotal.Text = CommonCalc.DecimalFormat(pageKingakuTotal);
                        // 消費税（外税）
                        this.form.txt_TaxSoto.Text = CommonCalc.DecimalFormat(CommonCalc.FractionCalc(pageKingakuTotal - (pageKingakuTotal / (this.taxRate + 1)), kingakuHasuuCd));
                        // 課税対象金額
                        this.form.txt_KazeiTaisyoGaku.Text =
                            CommonCalc.DecimalFormat(pageKingakuTotal - CommonCalc.FractionCalc(pageKingakuTotal - (pageKingakuTotal / (this.taxRate + 1)), kingakuHasuuCd));
                        // 内税総額
                        this.form.txt_UchiTotal.Text = CommonCalc.DecimalFormat(CommonCalc.FractionCalc(pageKingakuTotal - (pageKingakuTotal / (this.taxRate + 1)), kingakuHasuuCd));
                        // 総合計金額
                        this.form.txt_GoukeiKingakuTotal.Text = CommonCalc.DecimalFormat(pageKingakuTotal);
                    }
                    // 税区分が非課税の場合
                    else if (this.form.txt_ZeiKbnCD.Text.Equals("3"))
                    {
                        // 金額計
                        this.form.txt_KingakuTotal.Text = CommonCalc.DecimalFormat(pageKingakuTotal);
                        // 消費税（外税）
                        this.form.txt_TaxSoto.Text = CommonCalc.DecimalFormat(sotoZeiToal + uchiZeiToal);
                        // 課税対象金額
                        this.form.txt_KazeiTaisyoGaku.Text = CommonCalc.DecimalFormat(pageKingakuTotal - uchiZeiToal);
                        // 内税総額
                        this.form.txt_UchiTotal.Text = CommonCalc.DecimalFormat(uchiZeiToal);
                        // 総合計金額
                        this.form.txt_GoukeiKingakuTotal.Text = CommonCalc.DecimalFormat(pageKingakuTotal + sotoZeiToal);
                    }
                }
                // 税計算区分CD (2.明細毎)
                else if (this.form.txt_ZeiKeisanKbnCD.Text.Equals("2"))
                {
                    // 金額計
                    this.form.txt_KingakuTotal.Text = CommonCalc.DecimalFormat(pageKingakuTotal);
                    // 消費税（外税）
                    this.form.txt_TaxSoto.Text = CommonCalc.DecimalFormat(pageSotoTotal + pageUchiTotal);
                    // 課税対象金額
                    this.form.txt_KazeiTaisyoGaku.Text = CommonCalc.DecimalFormat(pageKingakuTotal - pageUchiTotal);
                    // 内税総額
                    this.form.txt_UchiTotal.Text = CommonCalc.DecimalFormat(pageUchiTotal);
                    // 総合計金額
                    this.form.txt_GoukeiKingakuTotal.Text = CommonCalc.DecimalFormat(pageKingakuTotal + pageSotoTotal);
                }
                // 20140710 chinchisi EV002428 内税の計算を修正する。 end
            }
            catch (Exception ex)
            {
                LogUtility.Error("Calculate", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }

            LogUtility.DebugMethodEnd();
            return ret;
        }

        #endregion 小計の計算

        #region 品名チェック

        /// <summary>
        /// 品名チェック
        /// </summary>
        /// <param name="intRowNo"></param>
        /// <param name="selected"></param>
        /// <param name="msgYes"></param>
        internal bool CheckHinmeiCd(int intRowNo, int selected, bool msgYes, out bool catchErr)
        {
            LogUtility.DebugMethodStart(intRowNo, selected, msgYes);
            catchErr = false;
            try
            {
                // 初期化
                if (this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo].Value == null
                    || string.IsNullOrEmpty(this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo].Value.ToString()))
                {
                    // 20140714 ria EV002760 品名ＣＤをクリアしてもそれに紐づく項目が残ってしまう start
                    this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME, intRowNo].Value = String.Empty;
                    DataGridViewComboBoxCell cbc = (DataGridViewComboBoxCell)this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD, intRowNo];
                    cbc.Value = String.Empty;
                    this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD, intRowNo].Value = string.Empty;
                    this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_NAME, intRowNo].Value = string.Empty;
                    this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA, intRowNo].Value = string.Empty;
                    this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD, intRowNo].Value = string.Empty;
                    this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME, intRowNo].Value = string.Empty;
                    // 20140714 ria EV002760 品名ＣＤをクリアしてもそれに紐づく項目が残ってしまう end

                    return true;
                }

                M_HINMEI targetHimei = new M_HINMEI();

                if (this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo].Value != null)
                {
                    targetHimei = this.accessor.GetHinmeiDataByCd(this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo].Value.ToString());
                }

                if (this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo].Value != null
                    && (string.IsNullOrEmpty(targetHimei.HINMEI_CD)))
                {
                    // 存在しない品名が選択されている場合

                    if (selected == 1)
                    {
                        return false;
                    }
                    else
                    {
                        this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME, intRowNo].Value = String.Empty;
                        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo], true);
                        return false;
                    }
                }

                if (this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME, intRowNo].Value == null
                    || string.IsNullOrEmpty(Convert.ToString(this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME, intRowNo].Value)))
                {
                    // 20140717 syunrei EV005319_見積書を出力した時、品名が略称で表示されてしまうため、品名CDを入力した時にセットする品名は正式名称で表示する。　start
                    //this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME, intRowNo].Value = targetHimei.HINMEI_NAME_RYAKU;
                    this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME, intRowNo].Value = targetHimei.HINMEI_NAME;
                    // 20140717 syunrei EV005319_見積書を出力した時、品名が略称で表示されてしまうため、品名CDを入力した時にセットする品名は正式名称で表示する。　end

                    if (targetHimei.DENPYOU_KBN_CD != 9)
                    {
                        this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD, intRowNo].Value = targetHimei.DENPYOU_KBN_CD;
                    }

                    short UnitCd = 0;
                    if (short.TryParse(targetHimei.UNIT_CD.ToString(), out UnitCd))
                    {
                        this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD, intRowNo].Value = targetHimei.UNIT_CD;
                    }

                    // 伝票区分&税区分設定
                    this.SetDenpyouKbn(intRowNo);
                    this.SetZeiKbn(intRowNo);

                    // 単位設定
                    this.SetUnit(intRowNo);

                    // 単価設定
                    this.CalcTanka(this.form.CustomDataGridView.CurrentRow);

                    this.befHinmeiCd = this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo].Value.ToString();
                    return true;
                }
                else
                {
                    if (selected == 1)
                    {
                        // 一致するものがないのでエラー
                        bool isSkip = false;

                        if (!string.IsNullOrEmpty(this.befHinmeiCd))
                        {
                            // 前回値が空の場合は確認アラートは不要
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            var result = msgLogic.MessageBoxShow("C045", "品名CD", "品名、単位、単価、税区分を上書き");
                            if (result != DialogResult.Yes)
                            {
                                isSkip = true;
                            }
                        }

                        if (!isSkip)
                        {
                            // 20140717 syunrei EV005319_見積書を出力した時、品名が略称で表示されてしまうため、品名CDを入力した時にセットする品名は正式名称で表示する。　start
                            //this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME, intRowNo].Value = targetHimei.HINMEI_NAME_RYAKU;
                            this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME, intRowNo].Value = targetHimei.HINMEI_NAME;
                            // 20140717 syunrei EV005319_見積書を出力した時、品名が略称で表示されてしまうため、品名CDを入力した時にセットする品名は正式名称で表示する。　end

                            if (targetHimei.DENPYOU_KBN_CD != 9)
                            {
                                this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD, intRowNo].Value = targetHimei.DENPYOU_KBN_CD;
                            }

                            short UnitCd = 0;
                            if (short.TryParse(targetHimei.UNIT_CD.ToString(), out UnitCd))
                            {
                                this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD, intRowNo].Value = targetHimei.UNIT_CD;
                            }

                            // 伝票区分&税区分設定
                            this.SetDenpyouKbn(intRowNo);
                            this.SetZeiKbn(intRowNo);
                            // 単位設定
                            this.SetUnit(intRowNo);
                            // 単価設定
                            this.CalcTanka(this.form.CustomDataGridView.CurrentRow);
                        }
                        else
                        {
                            // 伝票区分のみ再設定
                            this.SetDenpyouKbn(intRowNo);
                        }
                    }
                    else
                    {
                        if (msgYes)
                        {
                            // 単価設定
                            this.CalcTanka(this.form.CustomDataGridView.CurrentRow);
                            // 20140718 katen No.5323 品名「000002.金属くず」をセットすると税区分を非課税にセットしても消費税を計上してしまう start‏
                            //// 税区分設定
                            //this.SetZeiKbn(intRowNo);
                            // 20140718 katen No.5323 品名「000002.金属くず」をセットすると税区分を非課税にセットしても消費税を計上してしまう start‏
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHinmeiCd", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
                return false;
            }
            LogUtility.DebugMethodEnd(true, catchErr);

            this.befHinmeiCd = this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo].Value.ToString();
            return true;
        }

        #endregion 品名チェック

        #region 品名CDチェック

        /// <summary>
        /// 品名CDチェック
        /// </summary>
        /// <param name="intRowNo"></param>
        /// <param name="selected"></param>
        /// <param name="msgYes"></param>
        internal bool CheckHinmeiCd(int intRowNo)
        {
            LogUtility.DebugMethodStart(intRowNo);
            try
            {
                // 初期化
                if (this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo].Value == null
                    || string.IsNullOrEmpty(this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo].Value.ToString()))
                {
                    this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME, intRowNo].Value = string.Empty;
                    DataGridViewComboBoxCell cbc = (DataGridViewComboBoxCell)this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD, intRowNo];
                    cbc.Value = String.Empty;
                    this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD, intRowNo].Value = string.Empty;
                    this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_NAME, intRowNo].Value = string.Empty;
                    this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA, intRowNo].Value = string.Empty;
                    this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD, intRowNo].Value = string.Empty;
                    this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME, intRowNo].Value = string.Empty;
                    return true;
                }

                this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo].Value =
                    this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo].Value.ToString().PadLeft(6, '0').ToUpper();

                M_HINMEI hinmei = new M_HINMEI();
                if (this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo].Value != null)
                {
                    hinmei = this.accessor.GetHinmeiDataByCd(Convert.ToString(this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo].Value));
                    if (hinmei != null)
                    {
                        this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME, intRowNo].Value = hinmei.HINMEI_NAME;
                    }
                }

                if (this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo].Value != null && string.IsNullOrEmpty(hinmei.HINMEI_CD))
                {
                    // 存在しない品名が選択されている場合

                    this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME, intRowNo].Value = String.Empty;
                    ControlUtility.SetInputErrorOccuredForDgvCell(this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo], true);

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "品名");

                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHinmeiCd", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                return false;
            }
            LogUtility.DebugMethodEnd();

            return true;
        }

        #endregion 品名CDチェック

        #region 品名チェック

        /// <summary>
        /// 品名チェック
        /// </summary>
        /// <param name="intRowNo"></param>
        /// <param name="selected"></param>
        /// <param name="msgYes"></param>
        internal bool CheckHinmeiName(int intRowNo)
        {
            LogUtility.DebugMethodStart(intRowNo);

            try
            {
                if (this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo].Value != null)
                {
                    // 存在しない品名が選択されている場合

                    if (this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME, intRowNo].Value == null || string.IsNullOrEmpty(this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME, intRowNo].Value.ToString().Trim()))
                    {
                        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME, intRowNo], true);

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E012", "品名");

                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHinmeiName", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            LogUtility.DebugMethodEnd();

            return true;
        }

        #endregion 品名チェック

        #region 全品名チェック

        /// <summary>
        /// 全品名チェック
        /// </summary>
        internal void CheckAllHinmeiCd(out bool catchErr)
        {
            LogUtility.DebugMethodStart();
            catchErr = false;

            try
            {
                // ページ
                for (int page = 0; page < this.form.tbc_MitsumoriMeisai.TabPages.Count; page++)
                {
                    // CustomDataGridViewを取得する
                    CustomDataGridView cdgv = ((CustomDataGridView)this.form.tbc_MitsumoriMeisai.TabPages[page].Controls["control"].Controls["CustomDataGridView"]);
                    //データグリットの情報を処理中のpage情報でセットする
                    this.form.CustomDataGridView = (CustomDataGridView)this.form.tbc_MitsumoriMeisai.TabPages[page].Controls["control"].Controls["CustomDataGridView"];

                    // 明細に対して、計算を行う
                    for (int i = 0; i < cdgv.Rows.Count - 1; i++)
                    {
                        CheckHinmeiCd(i, 2, false, out catchErr);
                        if (catchErr) { return; }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckAllHinmeiCd", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion 全品名チェック

        #region 伝票区分設定

        /// <summary>
        /// 伝票区分設定
        /// 明細の品名から伝票区分を設定する
        /// </summary>
        internal void SetDenpyouKbn(int intRowNo)
        {
            LogUtility.DebugMethodStart(intRowNo);

            // 伝票区分
            var targetHimei = this.accessor.GetHinmeiDataByCd(this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo].Value.ToString());
            short denpyouKbn = targetHimei.DENPYOU_KBN_CD.IsNull ? (short)0 : (short)targetHimei.DENPYOU_KBN_CD;

            // ●9(共通)の場合、データなしに設定
            if (denpyouKbn == 9)
            {
                // DBに存在しない伝票区分が設定されていた場合
                return;
            }

            // 初期化
            this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD, intRowNo].Value = string.Empty;
            this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME, intRowNo].Value = string.Empty;

            if (!denpyouKbnDictionary.ContainsKey(denpyouKbn))
            {
                // DBに存在しない伝票区分が設定されていた場合
                return;
            }

            // 伝票区分を設定
            this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD, intRowNo].Value = denpyouKbn;
            this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME, intRowNo].Value = denpyouKbnDictionary[denpyouKbn].DENPYOU_KBN_NAME_RYAKU;

            LogUtility.DebugMethodEnd();
        }

        #endregion 伝票区分設定

        #region 税区分設定

        /// <summary>
        /// 税区分設定
        /// 明細の品名から税区分を設定する
        /// </summary>
        internal void SetZeiKbn(int intRowNo)
        {
            LogUtility.DebugMethodStart(intRowNo);

            // 品名ＣＤが入力されていない明細行の場合
            if (this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo].Value == null)
            {
                return;
            }

            var targetHimei = this.accessor.GetHinmeiDataByCd(this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD, intRowNo].Value.ToString());

            // 伝票区分
            short denpyouKbn = targetHimei.DENPYOU_KBN_CD.IsNull ? (short)0 : (short)targetHimei.DENPYOU_KBN_CD;
            if (!denpyouKbnDictionary.ContainsKey(denpyouKbn))
            {
                // DBに存在しない伝票区分が設定されていた場合
                return;
            }

            // №2428 D-Sato
            //// ●9(共通)の場合、データなしに設定
            //if (denpyouKbn == 9)
            //{
            //    // DBに存在しない伝票区分が設定されていた場合
            //    return;
            //}
            // №2428 D-Sato

            // 税区分を設定
            String zeiKbnCd = targetHimei.ZEI_KBN_CD.IsNull ? "0" : targetHimei.ZEI_KBN_CD.ToString();
            this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD, intRowNo].Value = zeiKbnCd;
            if (zeiKbnCd.Equals("1") || zeiKbnCd.Equals("2") || zeiKbnCd.Equals("3"))
            {
                DataGridViewComboBoxCell cbc = (DataGridViewComboBoxCell)this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD, intRowNo];
                cbc.Value = cbc.Items[int.Parse(zeiKbnCd) - 1];
            }
            else
            {
                DataGridViewComboBoxCell cbc = (DataGridViewComboBoxCell)this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD, intRowNo];
                cbc.Value = String.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion 税区分設定

        #region 伝票区分チェック

        /// <summary>
        /// 伝票区分チェック
        /// </summary>
        internal bool checkDenpyouKbn(DataGridViewCellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart();

            // ●伝票区分(空白の場合）
            if (this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD, e.RowIndex].Value == null ||
                string.IsNullOrEmpty(this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD, e.RowIndex].Value.ToString()))
            {
                this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME, e.RowIndex].Value = String.Empty;

                return true;
            }

            // 画面入力伝票区分
            short denpyouKbn = string.IsNullOrEmpty(this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD, e.RowIndex].Value.ToString()) ? (short)0 : short.Parse(this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD, e.RowIndex].Value.ToString());

            // ●マスタにない場合
            if (!denpyouKbnDictionary.ContainsKey(denpyouKbn))
            {
                ControlUtility.SetInputErrorOccuredForDgvCell(this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD, e.RowIndex], true);
                this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME, e.RowIndex].Value = String.Empty;

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "伝票区分");

                return false;
            }

            // ●9(共通)の場合、データなしに設定
            if (denpyouKbn == 9)
            {
                ControlUtility.SetInputErrorOccuredForDgvCell(this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD, e.RowIndex], true);

                this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME, e.RowIndex].Value = String.Empty;

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "伝票区分");

                return false;
            }

            // 伝票区分を設定
            if (denpyouKbn > 0)
            {
                this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD, e.RowIndex].Value = denpyouKbn;
                this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME, e.RowIndex].Value = denpyouKbnDictionary[denpyouKbn].DENPYOU_KBN_NAME_RYAKU;
            }

            LogUtility.DebugMethodEnd(true);

            return true;
        }

        #endregion 伝票区分チェック

        #region 単位設定

        /// <summary>
        /// 単位設定
        /// </summary>
        internal void SetUnit(int intRowNo)
        {
            if (this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD, intRowNo].Value != null)
            {
                if (!string.IsNullOrEmpty(this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD, intRowNo].Value.ToString()))
                {
                    M_UNIT targetUnit = unitDictionary[short.Parse(this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD, intRowNo].Value.ToString())];
                    this.form.CustomDataGridView[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_NAME, intRowNo].Value = targetUnit.UNIT_NAME_RYAKU;
                }
            }
        }

        #endregion 単位設定

        #region 単価設定

        /// <summary>
        /// 単価設定
        /// </summary>
        internal void CalcTanka(DataGridViewRow targetRow)
        {
            LogUtility.DebugMethodStart(targetRow);

            if (targetRow == null)
            {
                return;
            }
            if (String.IsNullOrEmpty(Convert.ToString(targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value)))
            {
                return;
            }
            if (String.IsNullOrEmpty(Convert.ToString(targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value)))
            {
                return;
            }
            if (String.IsNullOrEmpty(Convert.ToString(targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD].Value)))
            {
                return;
            }
            // 単価
            decimal tanka = 0;

            String torihikisakiCD = string.Empty;
            String gyoushaC = string.Empty;
            String genbaCD = string.Empty;

            torihikisakiCD = this.form.txt_TorihikisakiCD.Text;
            gyoushaC = this.form.txt_GyoushaCD.Text;
            genbaCD = this.form.txt_GenbaCD.Text;

            //TODO ---------------------------------------------------------
            // 暫定対応（業者、現場をnull)設定　※パターンがないため
            gyoushaC = string.Empty;
            genbaCD = string.Empty;
            // -------------------------------------------------------------
            // 個別品名単価から取得
            var kobetsuhinmeiTanka = this.commonAccesser.GetKobetsuhinmeiTanka(
                (short)int.Parse(targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value.ToString()),
                torihikisakiCD,
                gyoushaC,
                genbaCD,
                "",
                "",
                "",
                Convert.ToString(targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value),
                (short)int.Parse(targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD].Value.ToString())
                );

            // 個別品名単価から情報が取れない場合は基本品名単価の検索
            if (kobetsuhinmeiTanka == null)
            {
                var kihonHinmeiTanka = this.commonAccesser.GetKihonHinmeitanka(
                   (short)int.Parse(targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value.ToString()),
                    "",
                    "",
                    "",
                    Convert.ToString(targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value),
                    (short)int.Parse(targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD].Value.ToString())
                    );
                if (kihonHinmeiTanka != null)
                {
                    decimal.TryParse(Convert.ToString(kihonHinmeiTanka.TANKA.Value), out tanka);
                }
            }
            else
            {
                decimal.TryParse(Convert.ToString(kobetsuhinmeiTanka.TANKA.Value), out tanka);
            }

            // 算定した[単価]が「0」の場合、かつ設定されていた[単価]が0以外の場合、単価設定は行わない。
            decimal tankaTmp;
            decimal.TryParse(targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].FormattedValue.ToString(), out tankaTmp);
            //if (tanka == 0 && tankaTmp > 0)
            //{
            //    return;
            //}
            // 単価を設定
            targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA].Value = tanka;
        }

        #endregion 単価設定

        #region 端数を取得

        /// <summary>
        /// 端数を取得
        /// </summary>
        internal short CalcHasuu(DataGridViewRow targetRow)
        {
            LogUtility.DebugMethodStart(targetRow);

            if (targetRow == null)
            {
                return 0;
            }

            // 取引先が未入力或いは伝票区分が未入力である場合
            if (targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value == null)
            {
                return 0;
            }

            short kingakuHasuuCd = 0;

            // 画面の取引先が顧客(既存)の場合
            if (this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("0"))
            {
                // 伝票区分により、端数を設定
                switch (Convert.ToString(targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value))
                {
                    case "1":
                        // 取引先_請求情報マスタ
                        if (this.dto.torihikisakiSeikyuuEntity != null)
                        {
                            short.TryParse(Convert.ToString(this.dto.torihikisakiSeikyuuEntity.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                        }
                        break;

                    case "2":
                        // 取引先_支払マスタ
                        if (this.dto.torihikisakiShiharaiEntity != null)
                        {
                            short.TryParse(Convert.ToString(this.dto.torihikisakiShiharaiEntity.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                        }
                        break;

                    default:
                        break;
                }
            }
            else if (this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("1"))
            {
                // 引合取引先
                // 伝票区分により、端数を設定
                switch (Convert.ToString(targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value))
                {
                    case "1":
                        // 引合取引先_請求情報マスタ
                        if (this.dto.hikiaiTorihikisakiSeikyuuEntity != null)
                        {
                            short.TryParse(Convert.ToString(this.dto.hikiaiTorihikisakiSeikyuuEntity.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                        }
                        break;

                    case "2":
                        // 引合取引先_支払マスタ
                        if (this.dto.hikiaiTorihikisakiShiharaiEntity != null)
                        {
                            short.TryParse(Convert.ToString(this.dto.hikiaiTorihikisakiShiharaiEntity.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                        }
                        break;

                    default:
                        break;
                }
            }
            else
            {
                // その他の場合
                if (this.dto.sysInfoEntity != null)
                {
                    switch (Convert.ToString(targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value))
                    {
                        case "1":
                            short.TryParse(Convert.ToString(this.dto.sysInfoEntity.SEIKYUU_KINGAKU_HASUU_CD), out kingakuHasuuCd);
                            break;

                        case "2":
                            short.TryParse(Convert.ToString(this.dto.sysInfoEntity.SHIHARAI_KINGAKU_HASUU_CD), out kingakuHasuuCd);
                            break;

                        default:
                            break;
                    }
                }
            }

            return kingakuHasuuCd;
        }

        #endregion 端数を取得

        #region 消費税端数を取得

        /// <summary>
        /// 消費税端数を取得
        /// </summary>
        internal short TaxCalcHasuu(DataGridViewRow targetRow)
        {
            LogUtility.DebugMethodStart(targetRow);

            if (targetRow == null)
            {
                return 0;
            }

            // 取引先が未入力或いは伝票区分が未入力である場合
            if (targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value == null)
            {
                return 0;
            }

            short taxHasuuCd = 0;

            // 画面の取引先が顧客(既存)の場合
            if (this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("0"))
            {
                // 伝票区分により、端数を設定
                switch (Convert.ToString(targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value))
                {
                    case "1":
                        // 取引先_請求情報マスタ
                        if (this.dto.torihikisakiSeikyuuEntity != null)
                        {
                            short.TryParse(Convert.ToString(this.dto.torihikisakiSeikyuuEntity.TAX_HASUU_CD), out taxHasuuCd);
                        }
                        break;

                    case "2":
                        // 取引先_支払マスタ
                        if (this.dto.torihikisakiShiharaiEntity != null)
                        {
                            short.TryParse(Convert.ToString(this.dto.torihikisakiShiharaiEntity.TAX_HASUU_CD), out taxHasuuCd);
                        }
                        break;

                    default:
                        break;
                }
            }
            else if (this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("1"))
            {
                // 引合取引先の場合
                // 伝票区分により、端数を設定
                switch (Convert.ToString(targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value))
                {
                    case "1":
                        // 引合取引先_請求情報マスタ
                        if (this.dto.hikiaiTorihikisakiSeikyuuEntity != null)
                        {
                            short.TryParse(Convert.ToString(this.dto.hikiaiTorihikisakiSeikyuuEntity.TAX_HASUU_CD), out taxHasuuCd);
                        }
                        break;

                    case "2":
                        // 引合取引先_支払マスタ
                        if (this.dto.hikiaiTorihikisakiShiharaiEntity != null)
                        {
                            short.TryParse(Convert.ToString(this.dto.hikiaiTorihikisakiShiharaiEntity.TAX_HASUU_CD), out taxHasuuCd);
                        }
                        break;

                    default:
                        break;
                }
            }
            else
            {
                // その他の場合
                if (this.dto.sysInfoEntity != null)
                {
                    switch (Convert.ToString(targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value))
                    {
                        case "1":
                            short.TryParse(Convert.ToString(this.dto.sysInfoEntity.SEIKYUU_TAX_HASUU_CD), out taxHasuuCd);
                            break;

                        case "2":
                            short.TryParse(Convert.ToString(this.dto.sysInfoEntity.SHIHARAI_TAX_HASUU_CD), out taxHasuuCd);
                            break;

                        default:
                            break;
                    }
                }
            }

            return taxHasuuCd;
        }

        #endregion 消費税端数を取得

        #region 消費税率を取得

        /// <summary>
        /// 消費税率を取得
        /// </summary>
        /// <param name="tekiyouDate">適用日付</param>
        /// <returns>消費税率</returns>
        public decimal GetShouhizeiRate(object tekiyouDate)
        {
            if (tekiyouDate == null)
            {
                return 0;
            }

            DateTime resulttekiyoudate = new DateTime();
            if (!DateTime.TryParse(tekiyouDate.ToString(), out resulttekiyoudate))
            {
                return 0;
            }

            DateTime datemin = (DateTime)SqlDateTime.MinValue;
            if (resulttekiyoudate < datemin)
            {
                return 0;
            }

            // SQL文作成(冗長にならないためsqlファイルで管理しない)
            DataTable dt = new DataTable();
            string selectStr = "SELECT * FROM M_SHOUHIZEI";
            string whereStr = " WHERE DELETE_FLG = 0";

            StringBuilder sb = new StringBuilder();
            sb.Append(" AND");
            sb.Append(" (");
            sb.Append("  (");
            sb.Append("  TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '" + (DateTime)tekiyouDate + "', 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, '" + (DateTime)tekiyouDate + "', 111), 120) <= TEKIYOU_END");
            sb.Append("  )");
            sb.Append("  OR (");
            sb.Append("  TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '" + (DateTime)tekiyouDate + "', 111), 120) AND TEKIYOU_END IS NULL");
            sb.Append("     )");
            sb.Append(" )");

            whereStr = whereStr + sb.ToString();

            dt = this.accessor.gyoushaDao.GetDateForStringSql(selectStr + whereStr);

            if (dt == null || dt.Rows.Count < 1)
            {
                return 0;
            }

            return (decimal)this.ChgDBNullToValue(dt.Rows[0]["SHOUHIZEI_RATE"], 0);
        }

        #endregion 消費税率を取得

        #region 税区分CDより税区分名を取得

        /// <summary>
        /// 税区分CDより税区分名を取得
        /// </summary>
        /// <param name="zeiKbnCD">税区分CD</param>
        /// <returns>税区分名</returns>
        private string GetZeiKbn(string zeiKbnCD)
        {
            switch (zeiKbnCD)
            {
                case "1":
                    return "外税";

                case "2":
                    return "内税";

                case "3":
                    return "非課税";

                default:
                    return string.Empty;
            }
        }

        #endregion 税区分CDより税区分名を取得

        #region 取引先、業者、現場ロストフォーカスイベント

        /// <summary>
        /// 取引先、業者、現場ロストフォーカスイベント
        /// </summary>
        /// <param name="torihikisakiCode"></param>
        /// <param name="gyoushaCD"></param>
        /// <param name="genbaCD"></param>
        public bool Torihikisaki_GyoushaCD_GenbaCD_LostFocus(string torihikisakiCode, string gyoushaCD, string genbaCD, out bool catchErr)
        {
            LogUtility.DebugMethodStart(torihikisakiCode, gyoushaCD, genbaCD);
            catchErr = false;
            try
            {
                // 取引先ロストフォーカスイベント
                if (string.IsNullOrEmpty(torihikisakiCode))
                {
                    // 初期化
                    this.initTorihikisakiTab();

                    this.form.txt_Torihikisaki_hikiai_flg.Text = String.Empty;
                    this.form.txt_Torihikisaki_shokuchi_kbn.Text = String.Empty;
                    // 引合業者チェックを外す
                    this.form.chk_HikiaiTorihikisakiFlg.Checked = false;

                    // 取引先タブのコントロールを使用不可
                    foreach (System.Windows.Forms.Control ctl in this.form.tap_Torihikisaki.Controls)
                    {
                        if (ctl is System.Windows.Forms.Label)
                        {
                            continue;
                        }
                        ctl.Enabled = false;
                    }

                    // 処理モード
                    HIKIAI_TORIHIKISAKI_WindowType = HIKIAI_TORIHIKISAKI.NONE;
                }

                // 業者ロストフォーカスイベント
                if (string.IsNullOrEmpty(gyoushaCD))
                {
                    // 初期化
                    this.initGyoushaTab();

                    this.form.txt_Gyousha_hikiai_flg.Text = String.Empty;
                    this.form.txt_Gyousha_shokuchi_kbn.Text = String.Empty;
                    // 引合業者チェックを外す
                    this.form.chk_HikiaiGyoushaFlg.Checked = false;

                    // 業者タブのコントロールを使用不可
                    foreach (System.Windows.Forms.Control ctl in this.form.tap_Gyousya.Controls)
                    {
                        if (ctl is System.Windows.Forms.Label)
                        {
                            continue;
                        }
                        ctl.Enabled = false;
                    }

                    // 処理モード
                    HIKIAI_GYOUSHA_WindowType = HIKIAI_GYOUSHA.NONE;
                }

                // 現場ロストフォーカスイベント
                if (string.IsNullOrEmpty(genbaCD))
                {
                    // 初期化
                    this.initGenbaTab();

                    this.form.txt_Genba_hikiai_flg.Text = String.Empty;
                    this.form.txt_Genba_shokuchi_kbn.Text = String.Empty;
                    // 引合現場チェックを外す
                    this.form.chk_HikiaiGenbaFlg.Checked = false;

                    // 現場タブのコントロールを使用不可
                    foreach (System.Windows.Forms.Control ctl in this.form.tap_Genba.Controls)
                    {
                        if (ctl is System.Windows.Forms.Label)
                        {
                            continue;
                        }
                        ctl.Enabled = false;
                    }
                    // 処理モード
                    HIKIAI_GENBA_WindowType = HIKIAI_GENBA.NONE;
                }

                // 取引先●
                if (!string.IsNullOrEmpty(torihikisakiCode) && (this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("0") || string.IsNullOrEmpty(this.form.txt_Torihikisaki_hikiai_flg.Text)))
                {
                    // 取引先マスタ検索
                    if (!this.initTorihikisaki(torihikisakiCode))
                    {
                        return false;
                    }
                }
                else if (!string.IsNullOrEmpty(torihikisakiCode) && (this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("1")))
                {
                    // 引合取引先マスタ検索
                    if (!this.initHikiaiTorihikisaki(torihikisakiCode))
                    {
                        return false;
                    }
                }

                // 業者●
                if (!string.IsNullOrEmpty(gyoushaCD) && (this.form.txt_Gyousha_hikiai_flg.Text.Equals("0") || string.IsNullOrEmpty(this.form.txt_Gyousha_hikiai_flg.Text) || (this.form.txt_Gyousha_hikiai_flg.Text.Equals("False"))))
                {
                    // 業者マスタ検索
                    if (!this.initGyousha(gyoushaCD))
                    {
                        return false;
                    }
                }
                else if (!string.IsNullOrEmpty(gyoushaCD) && (this.form.txt_Gyousha_hikiai_flg.Text.Equals("1")) || (this.form.txt_Gyousha_hikiai_flg.Text.Equals("True")))
                {
                    // 引合業者マスタ検索
                    if (!this.initHikiaiGyousha(gyoushaCD))
                    {
                        return false;
                    }
                }

                // 現場●
                if (!string.IsNullOrEmpty(genbaCD) && (this.form.txt_Genba_hikiai_flg.Text.Equals("0") || string.IsNullOrEmpty(this.form.txt_Genba_hikiai_flg.Text)))
                {
                    // 現場マスタ検索
                    if (!this.initGenba(gyoushaCD, genbaCD))
                    {
                        return false;
                    }
                }
                else if (!string.IsNullOrEmpty(genbaCD) && (this.form.txt_Genba_hikiai_flg.Text.Equals("1")))
                {
                    // 引合現場マスタ検索
                    this.initHikiaiGenba(gyoushaCD, genbaCD);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Torihikisaki_GyoushaCD_GenbaCD_LostFocus", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            LogUtility.DebugMethodEnd(true, catchErr);

            return true;
        }

        #endregion 取引先、業者、現場ロストフォーカスイベント



        #region 取引先ロストフォーカスイベント

        /// <summary>
        /// 取引先ロストフォーカスイベント
        /// </summary>
        /// <param name="torihikisakiCode"></param>
        public bool TorihikisakiLostFocus(string torihikisakiCode)
        {
            LogUtility.DebugMethodStart(torihikisakiCode);

            // 取引先ロストフォーカスイベント
            if (string.IsNullOrEmpty(torihikisakiCode))
            {
                // 初期化
                this.initTorihikisakiTab();

                this.form.txt_Torihikisaki_hikiai_flg.Text = String.Empty;
                this.form.txt_Torihikisaki_shokuchi_kbn.Text = String.Empty;
                // 引合業者チェックを外す
                this.form.chk_HikiaiTorihikisakiFlg.Checked = false;

                // 取引先タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Torihikisaki.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                // 処理モード
                HIKIAI_TORIHIKISAKI_WindowType = HIKIAI_TORIHIKISAKI.NONE;

                return true;
            }

            // 既存を表示するか引合を表示するか切り替えます
            if (!torihikisakiDisplaySwitching())
            {
                // 既存マスタでも引合マスタでもヒットしなかった場合
                // 処理モード
                HIKIAI_TORIHIKISAKI_WindowType = HIKIAI_TORIHIKISAKI.NONE;
                // 再設定
                this.form.txt_Torihikisaki_hikiai_flg.Text = String.Empty;
                this.form.txt_Torihikisaki_shokuchi_kbn.Text = String.Empty;

                // 取引先タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Torihikisaki.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                this.form.txt_TorihikisakiCD.IsInputErrorOccured = true;
                this.form.txt_TorihikisakiCD.Focus();
                this.form.beforTorihikisakiCD = this.form.txt_TorihikisakiCD.Text;
            }
            else
            {
                // 既存表示の場合
                if (!this.form.hikiaiDisplaySwitchingFlg)
                {
                    // 取引先マスタ取得
                    if (!this.seachTorihikisaki(torihikisakiCode))
                    {
                        return false;
                    }
                }
                // 引合表示の場合
                else if (this.form.hikiaiDisplaySwitchingFlg)
                {
                    // 引合取引先マスタ取得
                    if (!this.seachHikiaiTorihikisaki(torihikisakiCode))
                    {
                        return false;
                    }
                }
            }

            LogUtility.DebugMethodEnd();
            return true;
        }

        #endregion 取引先ロストフォーカスイベント

        #region 業者ロストフォーカスイベント

        /// <summary>
        /// 業者ロストフォーカスイベント
        /// </summary>
        /// <param name="gyoushaCD"></param>
        public bool GyoushaCD_LostFocus(string gyoushaCD)
        {
            LogUtility.DebugMethodStart(gyoushaCD);

            // 業者ロストフォーカスイベント
            if (string.IsNullOrEmpty(gyoushaCD))
            {
                // 初期化
                this.initGyoushaTab();

                this.form.txt_Gyousha_hikiai_flg.Text = String.Empty;
                this.form.txt_Gyousha_shokuchi_kbn.Text = String.Empty;
                // 引合業者チェックを外す
                this.form.chk_HikiaiGyoushaFlg.Checked = false;

                // 業者タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Gyousya.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                // 処理モード
                HIKIAI_GYOUSHA_WindowType = HIKIAI_GYOUSHA.NONE;

                this.form.beforGyousaCD = this.form.txt_GyoushaCD.Text;

                if (!this.gyoushaNameChangedFlg)
                {
                    this.form.txt_GyoushaName.Text = string.Empty;
                }

                // 初期化
                this.dto.gyoushaEntry = null;
                this.dto.hikiaiGyoushaEntry = null;
                this.beforDto.hikiaiGyoushaEntry = null;

                // 初期化
                this.initGenbaTab();

                this.form.txt_Genba_hikiai_flg.Text = String.Empty;
                this.form.txt_Genba_shokuchi_kbn.Text = String.Empty;
                // 引合現場チェックを外す
                this.form.chk_HikiaiGenbaFlg.Checked = false;

                //現場タブ画面表示設定
                this.initGenbaTab();

                // 現場タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Genba.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }
                // 処理モード
                HIKIAI_GENBA_WindowType = HIKIAI_GENBA.NONE;

                this.form.txt_GenbaCD.Text = string.Empty;
                this.form.txtGenbaName.Text = string.Empty;
                this.form.txtGenbaName.ReadOnly = true;
                this.form.txtGenbaName.TabStop = false;

                // 初期化
                this.dto.genbaEntry = null;
                this.dto.hikiaiGenbaEntry = null;
                beforDto.hikiaiGenbaEntry = null;

                return true;
            }

            // 既存を表示するか引合を表示するか切り替えます
            if (!gyoushaDisplaySwitching())
            {
                // 処理モード
                HIKIAI_GYOUSHA_WindowType = HIKIAI_GYOUSHA.NONE;
                // 再設定
                this.form.txt_Gyousha_hikiai_flg.Text = String.Empty;
                this.form.txt_Gyousha_shokuchi_kbn.Text = String.Empty;

                // 業者タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Gyousya.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                this.form.txt_GyoushaCD.IsInputErrorOccured = true;
                this.form.txt_GyoushaCD.Focus();
            }
            else
            {
                // 既存表示の場合
                if (!this.form.hikiaiDisplaySwitchingFlg)
                {
                    // 業者マスタ取得
                    if (!this.seachGyousha(gyoushaCD))
                    {
                        return false;
                    }
                }
                // 引合表示の場合
                else if (this.form.hikiaiDisplaySwitchingFlg)
                {
                    // 引合業者マスタ取得
                    if (!this.seachHikiaiGyousha(gyoushaCD))
                    {
                        return false;
                    }
                }
            }

            LogUtility.DebugMethodEnd();

            return true;
        }

        #endregion 業者ロストフォーカスイベント

        #region 現場ロストフォーカスイベント

        /// <summary>
        /// 現場ロストフォーカスイベント
        /// </summary>
        /// <param name="gyoushaCD"></param>
        /// <param name="genbaCD"></param>
        public bool GenbaCD_LostFocus(string gyoushaCD, string genbaCD)
        {
            LogUtility.DebugMethodStart(genbaCD, genbaCD);

            // 初期化
            this.gyousyaGenbaErrorKbn = 0;

            // 現場ロストフォーカスイベント
            if (string.IsNullOrEmpty(genbaCD))
            {
                // 初期化
                this.initGenbaTab();

                this.form.txt_Genba_hikiai_flg.Text = String.Empty;
                this.form.txt_Genba_shokuchi_kbn.Text = String.Empty;

                // 引合現場チェックを外す
                this.form.chk_HikiaiGenbaFlg.Checked = false;

                // 現場タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Genba.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }
                // 処理モード
                HIKIAI_GENBA_WindowType = HIKIAI_GENBA.NONE;

                // 初期化
                this.dto.genbaEntry = null;
                this.dto.hikiaiGenbaEntry = null;
                beforDto.hikiaiGenbaEntry = null;

                return true;
            }

            if (!string.IsNullOrEmpty(this.form.txt_GenbaCD.Text) && string.IsNullOrEmpty(this.form.txt_GyoushaCD.Text))
            {
                // 初期化
                this.initGenbaTab();

                this.form.txt_Genba_hikiai_flg.Text = String.Empty;
                this.form.txt_Genba_shokuchi_kbn.Text = String.Empty;
                // 引合現場チェックを外す
                this.form.chk_HikiaiGenbaFlg.Checked = false;

                // 現場タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Genba.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }
                // 処理モード
                HIKIAI_GENBA_WindowType = HIKIAI_GENBA.NONE;

                // 初期化
                this.dto.genbaEntry = null;

                // 初期化
                this.dto.hikiaiGenbaEntry = null;
                beforDto.hikiaiGenbaEntry = null;

                this.form.txt_GyoushaCD.IsInputErrorOccured = true;

                // エラー区分＝１：業者エラー
                this.gyousyaGenbaErrorKbn = 1;

                this.form.txt_GyoushaCD.Focus();

                return false;
            }

            // 既存を表示するか引合を表示するか切り替えます
            if (!genbaDisplaySwitching())
            {
                // 既存マスタでも引合マスタでもヒットしなかった場合
                // 処理モード
                HIKIAI_GENBA_WindowType = HIKIAI_GENBA.NONE;

                // 再設定
                this.form.txt_Genba_hikiai_flg.Text = String.Empty;
                this.form.txt_Genba_shokuchi_kbn.Text = String.Empty;

                // 現場タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Genba.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                this.form.txt_GenbaCD.IsInputErrorOccured = true;

                // エラー区分＝２：現場エラー
                this.gyousyaGenbaErrorKbn = 2;

                this.form.txt_GenbaCD.Focus();
            }
            else
            {
                // 既存表示の場合
                if (!this.form.hikiaiDisplaySwitchingFlg)
                {
                    // 現場マスタ取得
                    if (!this.seachGenba(gyoushaCD, genbaCD))
                    {
                        return false;
                    }
                }
                // 引合表示の場合
                else if (this.form.hikiaiDisplaySwitchingFlg)
                {
                    // 引合現場マスタ取得
                    if (!this.seachHikiaiGenba(gyoushaCD, genbaCD))
                    {
                        return false;
                    }
                }
            }

            LogUtility.DebugMethodEnd();

            return true;
        }

        #endregion 現場ロストフォーカスイベント

        #region 取引先、業者、現場タブ画面表示設定

        /// <summary>
        /// 取引先、業者、現場タブ画面表示設定
        /// </summary>
        /// <param name="torihikisakiCode"></param>
        public bool Torihikisaki_GyoushaCD_GenbaCD_SetTab(bool NameBasedChangeFlg)
        {
            LogUtility.DebugMethodStart(NameBasedChangeFlg);
            bool ret = true;
            try
            {
                // 取引先タブ画面表示設定
                this.setDataTorihikisakiTab(NameBasedChangeFlg);

                // 業者タブ画面表示設定
                this.setDataGyoushaTab(NameBasedChangeFlg);

                // 現場タブ画面表示設定
                this.setDataGenbaTab(NameBasedChangeFlg);
            }
            catch (Exception ex)
            {
                LogUtility.Error("Torihikisaki_GyoushaCD_GenbaCD_SetTab", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }

            LogUtility.DebugMethodEnd();
            return ret;
        }

        #endregion 取引先、業者、現場タブ画面表示設定

        #region 取引先マスタ検索

        /// <summary>
        /// 取引先マスタ、取引先請求、取引先支払検索
        /// </summary>
        /// <param name="torihikisakiCode"></param>
        public bool seachTorihikisaki(string torihikisakiCode)
        {
            LogUtility.DebugMethodStart(torihikisakiCode);

            // 初期化
            this.dto.torihikisakiEntry = null;
            this.dto.torihikisakiSeikyuuEntity = null;
            this.dto.torihikisakiShiharaiEntity = null;

            // 処理しない
            if (string.IsNullOrEmpty(torihikisakiCode))
            {
                return true;
            }

            torihikisakiCode = torihikisakiCode.PadLeft(6, '0');

            // 取引先マスタ取得
            SqlDateTime tekiyouDate = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
            DateTime date;
            if (!string.IsNullOrWhiteSpace(this.form.dtp_MitsumoriDate.Text) && DateTime.TryParse(this.form.dtp_MitsumoriDate.Text, out date))
            {
                tekiyouDate = date;
            }
            this.dto.torihikisakiEntry = this.accessor.GetTorihikisaki(torihikisakiCode, tekiyouDate);

            if (this.dto.torihikisakiEntry == null)
            {
                // 処理モード
                HIKIAI_TORIHIKISAKI_WindowType = HIKIAI_TORIHIKISAKI.NONE;
                // 再設定
                this.form.txt_Torihikisaki_hikiai_flg.Text = String.Empty;
                this.form.txt_Torihikisaki_shokuchi_kbn.Text = String.Empty;

                // 取引先タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Torihikisaki.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "取引先");
                this.form.isInputError = true;
                this.form.txt_TorihikisakiCD.IsInputErrorOccured = true;
                this.form.txt_TorihikisakiCD.Focus();
                this.form.beforTorihikisakiCD = this.form.txt_TorihikisakiCD.Text;

                return false;
            }

            // windowType設定
            if (!this.dto.torihikisakiEntry.SHOKUCHI_KBN.IsNull && (Boolean)this.dto.torihikisakiEntry.SHOKUCHI_KBN)
            {
                // 処理モード
                HIKIAI_TORIHIKISAKI_WindowType = HIKIAI_TORIHIKISAKI.NEW_WINDOW_FLAG;

                // 再設定
                this.form.txt_Torihikisaki_hikiai_flg.Text = "0";
                this.form.txt_Torihikisaki_shokuchi_kbn.Text = "True";
            }
            else if (!this.dto.torihikisakiEntry.SHOKUCHI_KBN.IsNull && !(Boolean)this.dto.torihikisakiEntry.SHOKUCHI_KBN)
            {
                // 処理モード
                HIKIAI_TORIHIKISAKI_WindowType = HIKIAI_TORIHIKISAKI.REFERENCE_WINDOW_FLAG;

                // 再設定
                this.form.txt_Torihikisaki_hikiai_flg.Text = "0";
                this.form.txt_Torihikisaki_shokuchi_kbn.Text = "False";
            }

            // 取引先請求
            this.dto.torihikisakiSeikyuuEntity = this.accessor.GetTorihikisakiSeikyuu(torihikisakiCode);

            // 取引先支払
            this.dto.torihikisakiShiharaiEntity = this.accessor.GetTorihikisakiShiharai(torihikisakiCode);

            LogUtility.DebugMethodEnd();

            return true;
        }

        #endregion 取引先マスタ検索

        #region 引合取引先マスタ検索

        /// <summary>
        /// 引合取引先マスタ検索
        /// </summary>
        /// <param name="hikiaiTorihikisakiCode"></param>
        public bool seachHikiaiTorihikisaki(string hikiaiTorihikisakiCode)
        {
            LogUtility.DebugMethodStart(hikiaiTorihikisakiCode);

            // 初期化
            this.dto.hikiaiTorihikisakiEntry = null;
            this.beforDto.hikiaiTorihikisakiEntry = null;

            // 処理しない
            if (string.IsNullOrEmpty(hikiaiTorihikisakiCode))
            {
                return true;
            }

            hikiaiTorihikisakiCode = hikiaiTorihikisakiCode.PadLeft(6, '0');

            // 引合取引先マスタ検索
            this.dto.hikiaiTorihikisakiEntry = this.accessor.GetHikiaiTorihikisakiEntry(hikiaiTorihikisakiCode);

            // 更新前データを保持
            this.beforDto.hikiaiTorihikisakiEntry = this.dto.hikiaiTorihikisakiEntry;

            if (this.dto.hikiaiTorihikisakiEntry == null)
            {
                // 処理モード
                HIKIAI_TORIHIKISAKI_WindowType = HIKIAI_TORIHIKISAKI.NONE;
                // 再設定
                this.form.txt_Torihikisaki_hikiai_flg.Text = String.Empty;
                this.form.txt_Torihikisaki_shokuchi_kbn.Text = String.Empty;

                // 取引先タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Torihikisaki.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "取引先");
                this.form.isInputError = true;
                this.form.txt_TorihikisakiCD.IsInputErrorOccured = true;
                this.form.txt_TorihikisakiCD.Focus();
                this.form.beforTorihikisakiCD = this.form.txt_TorihikisakiCD.Text;

                return false;
            }

            // 処理モード
            HIKIAI_TORIHIKISAKI_WindowType = HIKIAI_TORIHIKISAKI.UPDATE_WINDOW_FLAG;

            // windowType設定
            if (!this.dto.hikiaiTorihikisakiEntry.SHOKUCHI_KBN.IsNull)
            {
                // 再設定
                this.form.txt_Torihikisaki_hikiai_flg.Text = "1";
                this.form.txt_Torihikisaki_shokuchi_kbn.Text = ((Boolean)this.dto.hikiaiTorihikisakiEntry.SHOKUCHI_KBN).ToString();
            }
            else
            {
                // 再設定
                this.form.txt_Torihikisaki_hikiai_flg.Text = "1";
                this.form.txt_Torihikisaki_shokuchi_kbn.Text = String.Empty;
            }

            this.dto.hikiaiTorihikisakiSeikyuuEntity = this.accessor.GetHikiaiTorihikisakiSeikyuuEntry(hikiaiTorihikisakiCode);
            this.dto.hikiaiTorihikisakiShiharaiEntity = this.accessor.GetHikiaiTorihikisakiShiharaiEntry(hikiaiTorihikisakiCode);

            LogUtility.DebugMethodEnd(true);

            return true;
        }

        #endregion 引合取引先マスタ検索

        #region 業者マスタ検索

        /// <summary>
        /// 業者マスタ
        /// </summary>
        /// <param name="gyoushaCode"></param>
        public bool seachGyousha(string gyoushaCode)
        {
            LogUtility.DebugMethodStart(gyoushaCode);

            // 初期化
            this.dto.gyoushaEntry = null;

            // 処理しない
            if (string.IsNullOrEmpty(gyoushaCode))
            {
                return true;
            }

            gyoushaCode = gyoushaCode.PadLeft(6, '0');

            // 業者マスタ取得
            SqlDateTime tekiyouDate = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
            DateTime date;
            if (!string.IsNullOrWhiteSpace(this.form.dtp_MitsumoriDate.Text) && DateTime.TryParse(this.form.dtp_MitsumoriDate.Text, out date))
            {
                tekiyouDate = date;
            }
            this.dto.gyoushaEntry = this.accessor.GetGyousha(gyoushaCode, tekiyouDate);

            if (this.dto.gyoushaEntry == null)
            {
                // 処理モード
                HIKIAI_GYOUSHA_WindowType = HIKIAI_GYOUSHA.NONE;
                // 再設定
                this.form.txt_Gyousha_hikiai_flg.Text = String.Empty;
                this.form.txt_Gyousha_shokuchi_kbn.Text = String.Empty;

                // 業者タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Gyousya.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                this.form.txt_GyoushaCD.IsInputErrorOccured = true;
                //MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                //msgLogic.MessageBoxShow("E020", "業者");
                this.form.isInputError = true;
                this.form.txt_GyoushaCD.Focus();

                return false;
            }
            this.form.txt_GyoushaCD.IsInputErrorOccured = false;

            // windowType設定
            if (!this.dto.gyoushaEntry.SHOKUCHI_KBN.IsNull && (Boolean)this.dto.gyoushaEntry.SHOKUCHI_KBN)
            {
                // 処理モード
                HIKIAI_GYOUSHA_WindowType = HIKIAI_GYOUSHA.NEW_WINDOW_FLAG;

                // 再設定
                this.form.txt_Gyousha_hikiai_flg.Text = "0";
                this.form.txt_Gyousha_shokuchi_kbn.Text = "True";
            }
            else if (!this.dto.gyoushaEntry.SHOKUCHI_KBN.IsNull && !(Boolean)this.dto.gyoushaEntry.SHOKUCHI_KBN)
            {
                // 処理モード
                HIKIAI_GYOUSHA_WindowType = HIKIAI_GYOUSHA.REFERENCE_WINDOW_FLAG;

                // 再設定
                this.form.txt_Gyousha_hikiai_flg.Text = "0";
                this.form.txt_Gyousha_shokuchi_kbn.Text = "False";
            }

            LogUtility.DebugMethodEnd();

            return true;
        }

        #endregion 業者マスタ検索

        #region 引合業者マスタ検索

        /// <summary>
        /// 引合業者マスタ検索
        /// </summary>
        /// <param name="gyoushaCode"></param>
        public bool seachHikiaiGyousha(string gyoushaCode)
        {
            LogUtility.DebugMethodStart(gyoushaCode);

            // 初期化
            this.dto.hikiaiGyoushaEntry = null;
            this.beforDto.hikiaiGyoushaEntry = null;

            // 処理しない
            if (string.IsNullOrEmpty(gyoushaCode))
            {
                // 処理モード
                HIKIAI_GYOUSHA_WindowType = HIKIAI_GYOUSHA.NONE;
                // 再設定
                this.form.txt_Gyousha_hikiai_flg.Text = String.Empty;
                this.form.txt_Gyousha_shokuchi_kbn.Text = String.Empty;

                // 業者タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Gyousya.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "業者");
                this.form.isInputError = true;
                this.form.txt_GyoushaCD.IsInputErrorOccured = true;
                this.form.txt_GyoushaCD.Focus();

                return false;
            }

            gyoushaCode = gyoushaCode.PadLeft(6, '0');

            // 引合業者マスタ検索
            this.dto.hikiaiGyoushaEntry = this.accessor.GetHikiaiGyoushaEntry(gyoushaCode);

            // 更新前データを保持
            this.beforDto.hikiaiGyoushaEntry = this.dto.hikiaiGyoushaEntry;

            if (this.dto.hikiaiGyoushaEntry == null)
            {
                // 処理モード
                HIKIAI_GYOUSHA_WindowType = HIKIAI_GYOUSHA.NONE;
                // 再設定
                this.form.txt_Gyousha_hikiai_flg.Text = String.Empty;
                this.form.txt_Gyousha_shokuchi_kbn.Text = String.Empty;

                // 業者タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Gyousya.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                return false;
            }

            // 処理モード
            HIKIAI_GYOUSHA_WindowType = HIKIAI_GYOUSHA.UPDATE_WINDOW_FLAG;

            // windowType設定
            if (!this.dto.hikiaiGyoushaEntry.SHOKUCHI_KBN.IsNull)
            {
                // 再設定
                this.form.txt_Gyousha_hikiai_flg.Text = "1";
                this.form.txt_Gyousha_shokuchi_kbn.Text = ((Boolean)this.dto.hikiaiGyoushaEntry.SHOKUCHI_KBN).ToString();
            }
            else
            {
                // 再設定
                this.form.txt_Gyousha_hikiai_flg.Text = "1";
                this.form.txt_Gyousha_shokuchi_kbn.Text = String.Empty;
            }

            LogUtility.DebugMethodEnd();

            return true;
        }

        #endregion 引合業者マスタ検索

        #region 現場マスタ検索

        /// <summary>
        /// 現場マスタ
        /// </summary>
        /// <param name="gyoushaCD"></param>
        /// <param name="genbaCD"></param>
        public bool seachGenba(string gyoushaCD, string genbaCD)
        {
            LogUtility.DebugMethodStart(gyoushaCD, genbaCD);

            // 初期化
            this.dto.genbaEntry = null;

            gyoushaCD = gyoushaCD.PadLeft(6, '0');
            genbaCD = genbaCD.PadLeft(6, '0');

            // 処理しない
            if (string.IsNullOrEmpty(gyoushaCD))
            {
                return true;
            }
            // 処理しない
            if (string.IsNullOrEmpty(genbaCD))
            {
                return true;
            }

            // 現場マスタ取得
            SqlDateTime tekiyouDate = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
            DateTime date;
            if (!string.IsNullOrWhiteSpace(this.form.dtp_MitsumoriDate.Text) && DateTime.TryParse(this.form.dtp_MitsumoriDate.Text, out date))
            {
                tekiyouDate = date;
            }
            this.dto.genbaEntry = this.accessor.GetGenba(gyoushaCD, genbaCD, tekiyouDate);

            if (this.dto.genbaEntry == null)
            {
                // 処理モード
                HIKIAI_GENBA_WindowType = HIKIAI_GENBA.NONE;

                // 再設定
                this.form.txt_Genba_hikiai_flg.Text = String.Empty;
                this.form.txt_Genba_shokuchi_kbn.Text = String.Empty;

                // 現場タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Genba.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                this.form.txt_GenbaCD.IsInputErrorOccured = true;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "現場");
                this.form.isInputError = true;
                this.form.txt_GenbaCD.Focus();

                return false;
            }
            this.form.txt_GenbaCD.IsInputErrorOccured = false;

            // windowType設定
            if (!this.dto.genbaEntry.SHOKUCHI_KBN.IsNull && (Boolean)this.dto.genbaEntry.SHOKUCHI_KBN)
            {
                // 処理モード
                HIKIAI_GENBA_WindowType = HIKIAI_GENBA.NEW_WINDOW_FLAG;

                // 再設定
                this.form.txt_Genba_hikiai_flg.Text = "0";
                this.form.txt_Genba_shokuchi_kbn.Text = "True";
            }
            else if (!this.dto.genbaEntry.SHOKUCHI_KBN.IsNull && !(Boolean)this.dto.genbaEntry.SHOKUCHI_KBN)
            {
                // 処理モード
                HIKIAI_GENBA_WindowType = HIKIAI_GENBA.REFERENCE_WINDOW_FLAG;

                // 再設定
                this.form.txt_Genba_hikiai_flg.Text = "0";
                this.form.txt_Genba_shokuchi_kbn.Text = "False";
            }

            LogUtility.DebugMethodEnd();

            return true;
        }

        #endregion 現場マスタ検索

        #region 引合現場マスタ検索

        /// <summary>
        /// 引合現場マスタ検索
        /// </summary>
        /// <param name="gyoushaCD"></param>
        /// <param name="genbaCD"></param>
        public bool seachHikiaiGenba(string gyoushaCD, string genbaCD)
        {
            LogUtility.DebugMethodStart(gyoushaCD, genbaCD);

            // 初期化
            this.dto.hikiaiGenbaEntry = null;
            beforDto.hikiaiGenbaEntry = null;

            // 処理しない
            if (string.IsNullOrEmpty(gyoushaCD))
            {
                return true;
            }
            // 処理しない
            if (string.IsNullOrEmpty(genbaCD))
            {
                return true;
            }

            gyoushaCD = gyoushaCD.PadLeft(6, '0');
            genbaCD = genbaCD.PadLeft(6, '0');

            // 引合現場マスタ検索
            SqlDateTime tekiyouDate = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
            DateTime date;
            if (!string.IsNullOrWhiteSpace(this.form.dtp_MitsumoriDate.Text) && DateTime.TryParse(this.form.dtp_MitsumoriDate.Text, out date))
            {
                tekiyouDate = date;
            }
            this.dto.hikiaiGenbaEntry = this.accessor.GetHikiaiGenbaEntry(gyoushaCD, genbaCD, tekiyouDate, this.form.ManualInputGenbaFlg ? string.Empty : this.form.txt_Gyousha_hikiai_flg.Text);

            // 更新前データを保持
            beforDto.hikiaiGenbaEntry = this.dto.hikiaiGenbaEntry;

            if (this.dto.hikiaiGenbaEntry == null)
            {
                // 処理モード
                HIKIAI_GENBA_WindowType = HIKIAI_GENBA.NONE;

                // 再設定
                this.form.txt_Genba_hikiai_flg.Text = String.Empty;
                this.form.txt_Genba_shokuchi_kbn.Text = String.Empty;

                // 現場タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Genba.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "現場");
                this.form.isInputError = true;
                this.form.txt_GenbaCD.IsInputErrorOccured = true;
                this.form.txt_GenbaCD.Focus();
                this.form.beforeGenbaCD = this.form.txt_GenbaCD.Text;

                return false;
            }

            // 処理モード
            HIKIAI_GENBA_WindowType = HIKIAI_GENBA.UPDATE_WINDOW_FLAG;

            // windowType設定
            if (!this.dto.hikiaiGenbaEntry.SHOKUCHI_KBN.IsNull)
            {
                // 再設定
                this.form.txt_Genba_hikiai_flg.Text = "1";
                this.form.txt_Genba_shokuchi_kbn.Text = ((Boolean)this.dto.hikiaiGenbaEntry.SHOKUCHI_KBN).ToString();
            }
            else
            {
                // 再設定
                this.form.txt_Genba_hikiai_flg.Text = "1";
                this.form.txt_Genba_shokuchi_kbn.Text = String.Empty;
            }

            LogUtility.DebugMethodEnd();

            return true;
        }

        #endregion 引合現場マスタ検索

        #region 取引先タブ画面表示設定

        /// <summary>
        /// 取引先タブ画面表示設定
        /// </summary>
        public void setDataTorihikisakiTab(bool NameBasedChangeFlg)
        {
            LogUtility.DebugMethodStart();

            this.isReacquisition = false;

            if (NameBasedChangeFlg)
            {
                // 名称を再取得するか判定
                if (this.dto.torihikisakiEntry != null && this.dto.torihikisakiEntry.TORIHIKISAKI_CD != null)
                {
                    // CDが変更された場合かつ諸口で名称が変更されていない場合
                    if (this.form.beforeValue != this.dto.torihikisakiEntry.TORIHIKISAKI_CD)
                    {
                        this.isReacquisition = true;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    //else if (this.form.txt_TorihikisakiMei.Text == this.dto.torihikisakiEntry.TORIHIKISAKI_NAME_RYAKU)
                    else if ((this.dto.torihikisakiEntry.SHOKUCHI_KBN.IsTrue && this.form.txt_TorihikisakiMei.Text == this.dto.torihikisakiEntry.TORIHIKISAKI_NAME1) || ((!this.dto.torihikisakiEntry.SHOKUCHI_KBN.IsTrue) && this.form.txt_TorihikisakiMei.Text == this.dto.torihikisakiEntry.TORIHIKISAKI_NAME_RYAKU))
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                    {
                        this.isReacquisition = true;
                    }
                }
                else if (this.dto.hikiaiTorihikisakiEntry != null && this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_CD != null)
                {
                    // CDが変更された場合かつ諸口で名称が変更されていない場合
                    if (this.form.beforeValue != this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_CD)
                    {
                        this.isReacquisition = true;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    //else if (this.form.txt_TorihikisakiMei.Text == this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_NAME_RYAKU)
                    else if ((this.dto.hikiaiTorihikisakiEntry.SHOKUCHI_KBN.IsTrue && this.form.txt_TorihikisakiMei.Text == this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_NAME1) || ((!this.dto.hikiaiTorihikisakiEntry.SHOKUCHI_KBN.IsTrue) && this.form.txt_TorihikisakiMei.Text == this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_NAME_RYAKU))
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                    {
                        this.isReacquisition = true;
                    }
                }
            }

            // 引合取引先
            switch (HIKIAI_TORIHIKISAKI_WindowType)
            {
                case HIKIAI_TORIHIKISAKI.NEW_WINDOW_FLAG:

                    if (this.dto.torihikisakiEntry != null)
                    {
                        // 取引先名称設定
                        if (isReacquisition)
                        {
                            // 20151021 katen #13337 品名手入力に関する機能修正 start
                            if (this.dto.torihikisakiEntry.SHOKUCHI_KBN.IsTrue)
                            {
                                this.form.txt_TorihikisakiMei.Text = this.dto.torihikisakiEntry.TORIHIKISAKI_NAME1;
                            }
                            else
                            {
                                this.form.txt_TorihikisakiMei.Text = this.dto.torihikisakiEntry.TORIHIKISAKI_NAME_RYAKU;
                            }
                            // 20151021 katen #13337 品名手入力に関する機能修正 end
                            this.form.txt_TorihikisakiFurigana.Text = this.dto.torihikisakiEntry.TORIHIKISAKI_FURIGANA;
                        }

                        // 取引先タブに情報をセット
                        this.form.txt_TorihikisakiPost.Text = this.dto.torihikisakiEntry.TORIHIKISAKI_POST;
                        this.form.txt_TorihikisakiAddress1.Text = this.dto.torihikisakiEntry.TORIHIKISAKI_ADDRESS1;
                        this.form.txt_TorihikisakiAddress2.Text = this.dto.torihikisakiEntry.TORIHIKISAKI_ADDRESS2;
                        this.form.txt_TorihikisakiTantousya.Text = this.dto.torihikisakiEntry.TANTOUSHA;
                        this.form.txt_TorihikisakiTel.Text = this.dto.torihikisakiEntry.TORIHIKISAKI_TEL;
                    }
                    else if (this.dto.hikiaiTorihikisakiEntry != null)
                    {
                        // 取引先名称設定
                        if (isReacquisition)
                        {
                            // 20151021 katen #13337 品名手入力に関する機能修正 start
                            if (this.dto.hikiaiTorihikisakiEntry.SHOKUCHI_KBN.IsTrue)
                            {
                                this.form.txt_TorihikisakiMei.Text = this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_NAME1;
                            }
                            else
                            {
                                this.form.txt_TorihikisakiMei.Text = this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_NAME_RYAKU;
                            }
                            // 20151021 katen #13337 品名手入力に関する機能修正 end
                            this.form.txt_TorihikisakiFurigana.Text = this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_FURIGANA;
                        }

                        // 取引先タブに情報をセット
                        this.form.txt_TorihikisakiPost.Text = this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_POST;
                        this.form.txt_TorihikisakiAddress1.Text = this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_ADDRESS1;
                        this.form.txt_TorihikisakiAddress2.Text = this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_ADDRESS2;
                        this.form.txt_TorihikisakiTantousya.Text = this.dto.hikiaiTorihikisakiEntry.TANTOUSHA;
                        this.form.txt_TorihikisakiTel.Text = this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_TEL;
                    }
                    else
                    {
                        // 初期化
                        initTorihikisakiTab();
                    }

                    break;

                case HIKIAI_TORIHIKISAKI.UPDATE_WINDOW_FLAG:

                    if (this.dto.hikiaiTorihikisakiEntry != null)
                    {
                        // 取引先名称設定
                        if (isReacquisition)
                        {
                            // 20151021 katen #13337 品名手入力に関する機能修正 start
                            if (this.dto.hikiaiTorihikisakiEntry.SHOKUCHI_KBN.IsTrue)
                            {
                                this.form.txt_TorihikisakiMei.Text = this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_NAME1;
                            }
                            else
                            {
                                this.form.txt_TorihikisakiMei.Text = this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_NAME_RYAKU;
                            }
                            // 20151021 katen #13337 品名手入力に関する機能修正 end
                            this.form.txt_TorihikisakiFurigana.Text = this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_FURIGANA;
                        }
                        // 取引先タブに情報をセット
                        this.form.txt_TorihikisakiPost.Text = this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_POST;
                        this.form.txt_TorihikisakiAddress1.Text = this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_ADDRESS1;
                        this.form.txt_TorihikisakiAddress2.Text = this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_ADDRESS2;
                        this.form.txt_TorihikisakiTantousya.Text = this.dto.hikiaiTorihikisakiEntry.TANTOUSHA;
                        this.form.txt_TorihikisakiTel.Text = this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_TEL;
                    }
                    else
                    {
                        // 初期化
                        initTorihikisakiTab();
                    }

                    break;

                case HIKIAI_TORIHIKISAKI.REFERENCE_WINDOW_FLAG:

                    if (this.dto.torihikisakiEntry != null)
                    {
                        this.form.txt_Torihikisaki_hikiai_flg.Text = "0";

                        // CDが変更された場合
                        if (this.form.beforeValue != this.dto.torihikisakiEntry.TORIHIKISAKI_CD)
                        {
                            // 取引先名称設定
                            // 20151021 katen #13337 品名手入力に関する機能修正 start
                            if (this.dto.torihikisakiEntry.SHOKUCHI_KBN.IsTrue)
                            {
                                this.form.txt_TorihikisakiMei.Text = this.dto.torihikisakiEntry.TORIHIKISAKI_NAME1;
                            }
                            else
                            {
                                this.form.txt_TorihikisakiMei.Text = this.dto.torihikisakiEntry.TORIHIKISAKI_NAME_RYAKU;
                            }
                            // 20151021 katen #13337 品名手入力に関する機能修正 end
                            this.form.txt_TorihikisakiFurigana.Text = this.dto.torihikisakiEntry.TORIHIKISAKI_FURIGANA;
                        }

                        // 取引先タブに情報をセット
                        this.form.txt_TorihikisakiPost.Text = this.dto.torihikisakiEntry.TORIHIKISAKI_POST;
                        this.form.txt_TorihikisakiAddress1.Text = this.dto.torihikisakiEntry.TORIHIKISAKI_ADDRESS1;
                        this.form.txt_TorihikisakiAddress2.Text = this.dto.torihikisakiEntry.TORIHIKISAKI_ADDRESS2;
                        this.form.txt_TorihikisakiTantousya.Text = this.dto.torihikisakiEntry.TANTOUSHA;
                        this.form.txt_TorihikisakiTel.Text = this.dto.torihikisakiEntry.TORIHIKISAKI_TEL;
                    }
                    else
                    {
                        // 初期化
                        initTorihikisakiTab();
                    }

                    break;

                default:
                    // 初期化
                    initTorihikisakiTab();
                    break;
            }

            // 引合取引先
            switch (HIKIAI_TORIHIKISAKI_WindowType)
            {
                case HIKIAI_TORIHIKISAKI.NEW_WINDOW_FLAG:
                case HIKIAI_TORIHIKISAKI.UPDATE_WINDOW_FLAG:

                    // 引合取引先チェックボックスを選択
                    this.form.chk_HikiaiTorihikisakiFlg.Checked = true;

                    // 取引先タブのコントロールを使用可
                    foreach (System.Windows.Forms.Control ctl in this.form.tap_Torihikisaki.Controls)
                    {
                        if (ctl is System.Windows.Forms.Label)
                        {
                            continue;
                        }
                        ctl.Enabled = true;
                    }
                    this.form.txt_TorihikisakiMei.ReadOnly = false;
                    this.form.txt_TorihikisakiMei.TabStop = true;

                    break;

                case HIKIAI_TORIHIKISAKI.REFERENCE_WINDOW_FLAG:

                    // 引合取引先チェックボックスを未選択
                    this.form.chk_HikiaiTorihikisakiFlg.Checked = false;

                    // 取引先タブのコントロールを使用不可
                    foreach (System.Windows.Forms.Control ctl in this.form.tap_Torihikisaki.Controls)
                    {
                        if (ctl is System.Windows.Forms.Label)
                        {
                            continue;
                        }
                        ctl.Enabled = false;
                    }
                    this.form.txt_TorihikisakiMei.ReadOnly = true;
                    this.form.txt_TorihikisakiMei.TabStop = false;
                    break;

                default:
                    break;
            }

            if (isTransitionFocus)
            {
                // Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                this.MoveToNextControlForShokuchikbnCheck(this.form.txt_TorihikisakiCD);
            }

            // 請求情報取得
            this.setSeikyuZeiKbnCD(this.form.txt_TorihikisakiCD.Text);

            // 支払取引先区分をセット
            this.setShiharaiZeiKbn(this.form.txt_TorihikisakiCD.Text);

            LogUtility.DebugMethodEnd();
        }

        #endregion 取引先タブ画面表示設定

        #region 業者タブ画面表示設定

        /// <summary>
        /// 業者タブ画面表示設定
        /// </summary>
        public void setDataGyoushaTab(bool NameBasedChangeFlg)
        {
            LogUtility.DebugMethodStart();

            this.isReacquisition = false;

            if (NameBasedChangeFlg)
            {
                // 名称を再取得するか判定
                if (this.dto.gyoushaEntry != null && this.dto.gyoushaEntry.GYOUSHA_CD != null)
                {
                    // CDが変更された場合かつ諸口で名称が変更されていない場合
                    if (this.form.beforeValue != this.dto.gyoushaEntry.GYOUSHA_CD)
                    {
                        this.isReacquisition = true;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    //else if (this.form.txt_GyoushaName.Text == this.dto.gyoushaEntry.GYOUSHA_NAME_RYAKU)
                    else if ((this.dto.gyoushaEntry.SHOKUCHI_KBN.IsTrue && this.form.txt_GyoushaName.Text == this.dto.gyoushaEntry.GYOUSHA_NAME1) || ((!this.dto.gyoushaEntry.SHOKUCHI_KBN.IsTrue) && this.form.txt_GyoushaName.Text == this.dto.gyoushaEntry.GYOUSHA_NAME_RYAKU))
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                    {
                        this.isReacquisition = true;
                    }
                }
                else if (this.dto.hikiaiGyoushaEntry != null && this.dto.hikiaiGyoushaEntry.GYOUSHA_CD != null)
                {
                    // CDが変更された場合かつ諸口で名称が変更されていない場合
                    if (this.form.beforeValue != this.dto.hikiaiGyoushaEntry.GYOUSHA_CD)
                    {
                        this.isReacquisition = true;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    //else if (this.form.txt_GyoushaName.Text == this.dto.hikiaiGyoushaEntry.GYOUSHA_NAME_RYAKU)
                    else if ((this.dto.hikiaiGyoushaEntry.SHOKUCHI_KBN.IsTrue && this.form.txt_GyoushaName.Text == this.dto.hikiaiGyoushaEntry.GYOUSHA_NAME1) || ((!this.dto.hikiaiGyoushaEntry.SHOKUCHI_KBN.IsTrue) && this.form.txt_GyoushaName.Text == this.dto.hikiaiGyoushaEntry.GYOUSHA_NAME_RYAKU))
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                    {
                        this.isReacquisition = true;
                    }
                }
            }

            // 引合業者
            switch (HIKIAI_GYOUSHA_WindowType)
            {
                case HIKIAI_GYOUSHA.NEW_WINDOW_FLAG:

                    if (this.dto.gyoushaEntry != null)
                    {
                        // 業者名称設定
                        if (isReacquisition)
                        {
                            this.form.txt_GyoushaCD.Text = this.dto.gyoushaEntry.GYOUSHA_CD;
                            if (!this.gyoushaNameChangedFlg)
                            {
                                // 20151021 katen #13337 品名手入力に関する機能修正 start
                                if (this.dto.gyoushaEntry.SHOKUCHI_KBN.IsTrue)
                                {
                                    this.form.txt_GyoushaName.Text = this.dto.gyoushaEntry.GYOUSHA_NAME1;
                                }
                                else
                                {
                                    this.form.txt_GyoushaName.Text = this.dto.gyoushaEntry.GYOUSHA_NAME_RYAKU;
                                }
                                // 20151021 katen #13337 品名手入力に関する機能修正 end
                                this.form.txt_GyoushaFurigana.Text = this.dto.gyoushaEntry.GYOUSHA_FURIGANA;
                            }
                        }

                        // 業者タブに情報をセット
                        this.form.txt_GyousyaPost.Text = this.dto.gyoushaEntry.GYOUSHA_POST;
                        this.form.txt_GyousyaAddress1.Text = this.dto.gyoushaEntry.GYOUSHA_ADDRESS1;
                        this.form.txt_GyousyaAddress2.Text = this.dto.gyoushaEntry.GYOUSHA_ADDRESS2;
                        this.form.txt_GyousyaTantousya.Text = this.dto.gyoushaEntry.TANTOUSHA;
                        this.form.txt_GyousyaTel.Text = this.dto.gyoushaEntry.GYOUSHA_TEL;
                        this.form.txt_GyousyaKeitaiTel.Text = this.dto.gyoushaEntry.GYOUSHA_KEITAI_TEL;
                    }
                    else if (this.dto.hikiaiGyoushaEntry != null)
                    {
                        // 業者名称設定
                        if (isReacquisition)
                        {
                            this.form.txt_GyoushaCD.Text = this.dto.hikiaiGyoushaEntry.GYOUSHA_CD;
                            if (!this.gyoushaNameChangedFlg)
                            {
                                // 20151021 katen #13337 品名手入力に関する機能修正 start
                                if (this.dto.hikiaiGyoushaEntry.SHOKUCHI_KBN.IsTrue)
                                {
                                    this.form.txt_GyoushaName.Text = this.dto.hikiaiGyoushaEntry.GYOUSHA_NAME1;
                                }
                                else
                                {
                                    this.form.txt_GyoushaName.Text = this.dto.hikiaiGyoushaEntry.GYOUSHA_NAME_RYAKU;
                                }
                                // 20151021 katen #13337 品名手入力に関する機能修正 end
                                this.form.txt_GyoushaFurigana.Text = this.dto.hikiaiGyoushaEntry.GYOUSHA_FURIGANA;
                            }
                        }

                        // 業者タブに情報をセット
                        this.form.txt_GyousyaPost.Text = this.dto.hikiaiGyoushaEntry.GYOUSHA_POST;
                        this.form.txt_GyousyaAddress1.Text = this.dto.hikiaiGyoushaEntry.GYOUSHA_ADDRESS1;
                        this.form.txt_GyousyaAddress2.Text = this.dto.hikiaiGyoushaEntry.GYOUSHA_ADDRESS2;
                        this.form.txt_GyousyaTantousya.Text = this.dto.hikiaiGyoushaEntry.TANTOUSHA;
                        this.form.txt_GyousyaTel.Text = this.dto.hikiaiGyoushaEntry.GYOUSHA_TEL;
                        this.form.txt_GyousyaKeitaiTel.Text = this.dto.hikiaiGyoushaEntry.GYOUSHA_KEITAI_TEL;
                    }
                    else
                    {
                        //初期化
                        initGyoushaTab();
                    }
                    break;

                case HIKIAI_GYOUSHA.UPDATE_WINDOW_FLAG:

                    if (this.dto.hikiaiGyoushaEntry != null)
                    {
                        // 業者名称設定
                        if (isReacquisition)
                        {
                            this.form.txt_GyoushaCD.Text = this.dto.hikiaiGyoushaEntry.GYOUSHA_CD;
                            if (!this.gyoushaNameChangedFlg)
                            {
                                // 20151021 katen #13337 品名手入力に関する機能修正 start
                                if (this.dto.hikiaiGyoushaEntry.SHOKUCHI_KBN.IsTrue)
                                {
                                    this.form.txt_GyoushaName.Text = this.dto.hikiaiGyoushaEntry.GYOUSHA_NAME1;
                                }
                                else
                                {
                                    this.form.txt_GyoushaName.Text = this.dto.hikiaiGyoushaEntry.GYOUSHA_NAME_RYAKU;
                                }
                                // 20151021 katen #13337 品名手入力に関する機能修正 end
                                this.form.txt_GyoushaFurigana.Text = this.dto.hikiaiGyoushaEntry.GYOUSHA_FURIGANA;
                            }
                        }

                        // 業者タブに情報をセット
                        this.form.txt_GyousyaPost.Text = this.dto.hikiaiGyoushaEntry.GYOUSHA_POST;
                        this.form.txt_GyousyaAddress1.Text = this.dto.hikiaiGyoushaEntry.GYOUSHA_ADDRESS1;
                        this.form.txt_GyousyaAddress2.Text = this.dto.hikiaiGyoushaEntry.GYOUSHA_ADDRESS2;
                        this.form.txt_GyousyaTantousya.Text = this.dto.hikiaiGyoushaEntry.TANTOUSHA;
                        this.form.txt_GyousyaTel.Text = this.dto.hikiaiGyoushaEntry.GYOUSHA_TEL;
                        this.form.txt_GyousyaKeitaiTel.Text = this.dto.hikiaiGyoushaEntry.GYOUSHA_KEITAI_TEL;
                    }
                    else
                    {
                        //初期化
                        initGyoushaTab();
                    }

                    break;

                case HIKIAI_GYOUSHA.REFERENCE_WINDOW_FLAG:

                    if (this.dto.gyoushaEntry != null)
                    {
                        // 業者フラグ
                        this.form.txt_Gyousha_hikiai_flg.Text = "0";

                        // CDが変更された場合かつ諸口で名称が変更されていない場合
                        if (this.form.beforeValue != this.dto.gyoushaEntry.GYOUSHA_CD)
                        {
                            // 業者名称設定
                            this.form.txt_GyoushaCD.Text = this.dto.gyoushaEntry.GYOUSHA_CD;
                            // 20151021 katen #13337 品名手入力に関する機能修正 start
                            if (this.dto.gyoushaEntry.SHOKUCHI_KBN.IsTrue)
                            {
                                this.form.txt_GyoushaName.Text = this.dto.gyoushaEntry.GYOUSHA_NAME1;
                            }
                            else
                            {
                                this.form.txt_GyoushaName.Text = this.dto.gyoushaEntry.GYOUSHA_NAME_RYAKU;
                            }
                            // 20151021 katen #13337 品名手入力に関する機能修正 end
                            this.form.txt_GyoushaFurigana.Text = this.dto.gyoushaEntry.GYOUSHA_FURIGANA;
                        }

                        // 業者タブに情報をセット
                        this.form.txt_GyousyaPost.Text = this.dto.gyoushaEntry.GYOUSHA_POST;
                        this.form.txt_GyousyaAddress1.Text = this.dto.gyoushaEntry.GYOUSHA_ADDRESS1;
                        this.form.txt_GyousyaAddress2.Text = this.dto.gyoushaEntry.GYOUSHA_ADDRESS2;
                        this.form.txt_GyousyaTantousya.Text = this.dto.gyoushaEntry.TANTOUSHA;
                        this.form.txt_GyousyaTel.Text = this.dto.gyoushaEntry.GYOUSHA_TEL;
                        this.form.txt_GyousyaKeitaiTel.Text = this.dto.gyoushaEntry.GYOUSHA_KEITAI_TEL;
                    }
                    else
                    {
                        //初期化
                        initGyoushaTab();
                    }

                    break;

                default:
                    //初期化
                    initGyoushaTab();
                    break;
            }

            // 引合取引先
            switch (HIKIAI_GYOUSHA_WindowType)
            {
                case HIKIAI_GYOUSHA.NEW_WINDOW_FLAG:
                case HIKIAI_GYOUSHA.UPDATE_WINDOW_FLAG:

                    // 引合業者チェックボックスを選択
                    this.form.chk_HikiaiGyoushaFlg.Checked = true;

                    // 業者タブのコントロールを使用可
                    foreach (System.Windows.Forms.Control ctl in this.form.tap_Gyousya.Controls)
                    {
                        if (ctl is System.Windows.Forms.Label)
                        {
                            continue;
                        }
                        ctl.Enabled = true;
                    }
                    this.form.txt_GyoushaName.ReadOnly = false;
                    this.form.txt_GyoushaName.TabStop = true;

                    break;

                case HIKIAI_GYOUSHA.REFERENCE_WINDOW_FLAG:

                    // 引合業者チェックボックスを未選択
                    this.form.chk_HikiaiGyoushaFlg.Checked = false;

                    // 業者タブのコントロールを使用不可
                    foreach (System.Windows.Forms.Control ctl in this.form.tap_Gyousya.Controls)
                    {
                        if (ctl is System.Windows.Forms.Label)
                        {
                            continue;
                        }
                        ctl.Enabled = false;
                    }

                    this.form.txt_GyoushaName.ReadOnly = true;
                    this.form.txt_GyoushaName.TabStop = false;
                    break;

                default:
                    break;
            }

            // 取引先拠点チェックがエラーだったらフォーカスを移動しない
            if (!this.torihikisakiAndKyotenErrorFlg)
            {
                // Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                this.MoveToNextControlForShokuchikbnCheck(this.form.txt_GyoushaCD);
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion 業者タブ画面表示設定

        #region 現場タブ画面表示設定

        /// <summary>
        /// 現場タブ画面表示設定
        /// </summary>
        public void setDataGenbaTab(bool NameBasedChangeFlg)
        {
            LogUtility.DebugMethodStart();

            this.isReacquisition = false;

            if (NameBasedChangeFlg)
            {
                // 名称を再取得するか判定
                if (this.dto.genbaEntry != null && this.dto.genbaEntry.GENBA_CD != null)
                {
                    // CDが変更された場合かつ諸口で名称が変更されていない場合
                    if (this.form.beforeValue != this.dto.genbaEntry.GENBA_CD)
                    {
                        this.isReacquisition = true;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    //else if (this.form.txtGenbaName.Text == this.dto.genbaEntry.GENBA_NAME_RYAKU)
                    else if ((this.dto.genbaEntry.SHOKUCHI_KBN.IsTrue && this.form.txtGenbaName.Text == this.dto.genbaEntry.GENBA_NAME1) || ((!this.dto.genbaEntry.SHOKUCHI_KBN.IsTrue) && this.form.txtGenbaName.Text == this.dto.genbaEntry.GENBA_NAME_RYAKU))
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                    {
                        this.isReacquisition = true;
                    }
                }
            }

            // 引合現場
            switch (HIKIAI_GENBA_WindowType)
            {
                case HIKIAI_GENBA.NEW_WINDOW_FLAG:

                    if (this.dto.genbaEntry != null)
                    {
                        // 現場名称設定
                        if (isReacquisition)
                        {
                            this.form.txt_GenbaCD.Text = this.dto.genbaEntry.GENBA_CD;
                            // 20151021 katen #13337 品名手入力に関する機能修正 start
                            if (this.dto.genbaEntry.SHOKUCHI_KBN.IsTrue)
                            {
                                this.form.txtGenbaName.Text = this.dto.genbaEntry.GENBA_NAME1;
                            }
                            else
                            {
                                this.form.txtGenbaName.Text = this.dto.genbaEntry.GENBA_NAME_RYAKU;
                            }
                            // 20151021 katen #13337 品名手入力に関する機能修正 end
                            this.form.txtGenbaFurigana.Text = this.dto.genbaEntry.GENBA_FURIGANA;
                        }

                        // 現場タブに情報をセット
                        this.form.txt_GenbaPost.Text = this.dto.genbaEntry.GENBA_POST;
                        this.form.txt_GenbaAddress1.Text = this.dto.genbaEntry.GENBA_ADDRESS1;
                        this.form.txt_GenbaAddress2.Text = this.dto.genbaEntry.GENBA_ADDRESS2;
                        this.form.txt_GenbaTantousya.Text = this.dto.genbaEntry.TANTOUSHA;
                        this.form.txt_GenbaTel.Text = this.dto.genbaEntry.GENBA_TEL;
                        this.form.txt_GenbaKeitaiTel.Text = this.dto.genbaEntry.GENBA_KEITAI_TEL;
                    }
                    else
                    {
                        // 初期化
                        initGenbaTab();
                    };

                    break;

                case HIKIAI_GENBA.UPDATE_WINDOW_FLAG:

                    if (this.dto.hikiaiGenbaEntry != null)
                    {
                        // 現場名称設定
                        if (isReacquisition)
                        {
                            this.form.txt_GenbaCD.Text = this.dto.hikiaiGenbaEntry.GENBA_CD;
                            // 20151021 katen #13337 品名手入力に関する機能修正 start
                            if (this.dto.hikiaiGenbaEntry.SHOKUCHI_KBN.IsTrue)
                            {
                                this.form.txtGenbaName.Text = this.dto.hikiaiGenbaEntry.GENBA_NAME1;
                            }
                            else
                            {
                                this.form.txtGenbaName.Text = this.dto.hikiaiGenbaEntry.GENBA_NAME_RYAKU;
                            }
                            // 20151021 katen #13337 品名手入力に関する機能修正 end
                            this.form.txtGenbaFurigana.Text = this.dto.hikiaiGenbaEntry.GENBA_FURIGANA;
                        }

                        // 現場タブに情報をセット
                        this.form.txt_GenbaPost.Text = this.dto.hikiaiGenbaEntry.GENBA_POST;
                        this.form.txt_GenbaAddress1.Text = this.dto.hikiaiGenbaEntry.GENBA_ADDRESS1;
                        this.form.txt_GenbaAddress2.Text = this.dto.hikiaiGenbaEntry.GENBA_ADDRESS2;
                        this.form.txt_GenbaTantousya.Text = this.dto.hikiaiGenbaEntry.TANTOUSHA;
                        this.form.txt_GenbaTel.Text = this.dto.hikiaiGenbaEntry.GENBA_TEL;
                        this.form.txt_GenbaKeitaiTel.Text = this.dto.hikiaiGenbaEntry.GENBA_KEITAI_TEL;
                    }
                    else
                    {
                        // 初期化
                        initGenbaTab();
                    }

                    break;

                case HIKIAI_GENBA.REFERENCE_WINDOW_FLAG:

                    if (this.dto.genbaEntry != null)
                    {
                        // CDが変更された場合かつ諸口で名称が変更されていない場合
                        if (this.form.beforeValue != this.dto.genbaEntry.GENBA_CD)
                        {
                            // 現場名称設定
                            this.form.txt_GenbaCD.Text = this.dto.genbaEntry.GENBA_CD;
                            // 20151021 katen #13337 品名手入力に関する機能修正 start
                            if (this.dto.genbaEntry.SHOKUCHI_KBN.IsTrue)
                            {
                                this.form.txtGenbaName.Text = this.dto.genbaEntry.GENBA_NAME1;
                            }
                            else
                            {
                                this.form.txtGenbaName.Text = this.dto.genbaEntry.GENBA_NAME_RYAKU;
                            }
                            // 20151021 katen #13337 品名手入力に関する機能修正 end
                            this.form.txtGenbaFurigana.Text = this.dto.genbaEntry.GENBA_FURIGANA;
                        }

                        // 現場タブに情報をセット
                        this.form.txt_GenbaPost.Text = this.dto.genbaEntry.GENBA_POST;
                        this.form.txt_GenbaAddress1.Text = this.dto.genbaEntry.GENBA_ADDRESS1;
                        this.form.txt_GenbaAddress2.Text = this.dto.genbaEntry.GENBA_ADDRESS2;
                        this.form.txt_GenbaTantousya.Text = this.dto.genbaEntry.TANTOUSHA;
                        this.form.txt_GenbaTel.Text = this.dto.genbaEntry.GENBA_TEL;
                        this.form.txt_GenbaKeitaiTel.Text = this.dto.genbaEntry.GENBA_KEITAI_TEL;
                    }
                    else
                    {
                        // 初期化
                        initGenbaTab();
                    }
                    break;

                default:
                    // 初期化
                    initGenbaTab();
                    break;
            }

            // 現場
            switch (HIKIAI_GENBA_WindowType)
            {
                case HIKIAI_GENBA.NEW_WINDOW_FLAG:
                case HIKIAI_GENBA.UPDATE_WINDOW_FLAG:

                    // 引合現場チェックボックスを選択
                    this.form.chk_HikiaiGenbaFlg.Checked = true;

                    // 現場タブのコントロールを使用可
                    foreach (System.Windows.Forms.Control ctl in this.form.tap_Genba.Controls)
                    {
                        if (ctl is System.Windows.Forms.Label)
                        {
                            continue;
                        }
                        ctl.Enabled = true;
                    }
                    this.form.txtGenbaName.ReadOnly = false;
                    this.form.txtGenbaName.TabStop = true;

                    break;

                case HIKIAI_GENBA.REFERENCE_WINDOW_FLAG:

                    // 引合現場チェックボックスを未選択
                    this.form.chk_HikiaiGenbaFlg.Checked = false;

                    // 現場タブのコントロールを使用不可
                    foreach (System.Windows.Forms.Control ctl in this.form.tap_Genba.Controls)
                    {
                        if (ctl is System.Windows.Forms.Label)
                        {
                            continue;
                        }
                        ctl.Enabled = false;
                    }
                    this.form.txtGenbaName.ReadOnly = true;
                    this.form.txtGenbaName.TabStop = false;

                    break;

                default:
                    break;
            }

            // Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
            this.MoveToNextControlForShokuchikbnCheck(this.form.txt_GenbaCD);

            LogUtility.DebugMethodEnd();
        }

        #endregion 現場タブ画面表示設定

        #region 請求税区分設定

        /// <summary>
        /// 請求税区分設定
        /// </summary>
        private void setSeikyuZeiKbnCD(string torihikisakiCode)
        {
            LogUtility.DebugMethodStart(torihikisakiCode);

            if (this.dto.torihikisakiSeikyuuEntity == null)
            {
                this.form.txt_SeikyuuZeiKbnCD.Text = "0";
            }
            else
            {
                this.form.txt_SeikyuuZeiKbnCD.Text = this.dto.torihikisakiSeikyuuEntity.ZEI_KBN_CD.ToString();
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion 請求税区分設定

        #region 支払税区分設定

        /// <summary>
        /// 支払税区分設定
        /// </summary>
        private void setShiharaiZeiKbn(string torihikisakiCode)
        {
            LogUtility.DebugMethodStart(torihikisakiCode);

            if (this.dto.torihikisakiShiharaiEntity == null)
            {
                this.form.txt_ShiharaiZeiKbnCD.Text = "0";
            }
            else
            {
                this.form.txt_ShiharaiZeiKbnCD.Text = this.dto.torihikisakiShiharaiEntity.ZEI_KBN_CD.ToString();
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion 支払税区分設定

        #region 引合必須チェックの設定を初期化します

        /// <summary>
        /// 引合必須チェックの設定を初期化します
        /// </summary>
        internal bool HIkiaiRequiredSettingInit()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
                // 初期化
                if (!this.RequiredSettingInit())
                {
                    ret = false;
                    return ret;
                }

                // 引合以外の必須を外す
                this.RequiredSettingBackColorClear();

                // Entry
                this.form.txt_TorihikisakiCD.RegistCheckMethod = null;
                this.form.txt_GyoushaCD.RegistCheckMethod = null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HIkiaiRequiredSettingInit", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }

        #endregion 引合必須チェックの設定を初期化します

        #region 引合必須チェックの設定を動的に生成

        /// <summary>
        /// 引合必須チェックの設定を動的に生成
        /// </summary>
        internal void HikiaiSetRequiredSetting()
        {
            LogUtility.DebugMethodStart();

            // 初期化
            if (!this.HIkiaiRequiredSettingInit())
            {
                return;
            }

            // 設定
            SelectCheckDto existCheck = new SelectCheckDto();
            existCheck.CheckMethodName = "必須チェック";
            Collection<SelectCheckDto> excitChecks = new Collection<SelectCheckDto>();
            excitChecks.Add(existCheck);

            // 登録
            if (!string.IsNullOrEmpty(this.form.txt_GenbaCD.Text) && string.IsNullOrEmpty(this.form.txt_GyoushaCD.Text))
            {
                this.form.txt_GyoushaCD.RegistCheckMethod = excitChecks;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion 引合必須チェックの設定を動的に生成

        #region 引合チェック

        /// <summary>
        /// 引合チェック
        /// </summary>
        internal bool HikiaiCheck(out bool catchErr)
        {
            LogUtility.DebugMethodStart();
            catchErr = false;
            bool ret = false;
            try
            {
                if (

                  // 引合-引合-引合
                  (
                     this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("1")
                  && string.IsNullOrEmpty(this.form.txt_Gyousha_hikiai_flg.Text)
                  && string.IsNullOrEmpty(this.form.txt_Genba_hikiai_flg.Text)
                  )
                  ||
                  // 引合-引合-引合
                  (
                     this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("1")
                  && (this.form.txt_Gyousha_hikiai_flg.Text.Equals("1") || this.form.txt_Gyousha_hikiai_flg.Text.Equals("True"))
                  && string.IsNullOrEmpty(this.form.txt_Genba_hikiai_flg.Text)
                  )
                  ||
                  // 引合-引合-引合
                  (
                     this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("1")
                  && (this.form.txt_Gyousha_hikiai_flg.Text.Equals("1") || this.form.txt_Gyousha_hikiai_flg.Text.Equals("True"))
                  && this.form.txt_Genba_hikiai_flg.Text.Equals("1")
                  )
                  ||
                  // 引合-引合-引合
                  (
                     this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("1")
                  && ((this.form.txt_Gyousha_hikiai_flg.Text.Equals("0") || this.form.txt_Gyousha_hikiai_flg.Text.Equals("True")) && this.form.txt_Gyousha_shokuchi_kbn.Text.Equals("True"))
                  && string.IsNullOrEmpty(this.form.txt_Genba_hikiai_flg.Text)
                  )
                  ||
                  // 引合-引合-引合
                  (
                     this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("1")
                  && ((this.form.txt_Gyousha_hikiai_flg.Text.Equals("0") || this.form.txt_Gyousha_hikiai_flg.Text.Equals("True")) && this.form.txt_Gyousha_shokuchi_kbn.Text.Equals("True"))
                  && ((this.form.txt_Genba_hikiai_flg.Text.Equals("0") || this.form.txt_Genba_hikiai_flg.Text.Equals("True")) && this.form.txt_Genba_shokuchi_kbn.Text.Equals("True"))
                  )
                  ||
                  // 顧客-引合-引合
                  (
                     this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("0")
                  && string.IsNullOrEmpty(this.form.txt_Gyousha_hikiai_flg.Text)
                  && string.IsNullOrEmpty(this.form.txt_Genba_hikiai_flg.Text)
                  )
                   ||
                  (
                     this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("0")
                  && (this.form.txt_Gyousha_hikiai_flg.Text.Equals("1") || this.form.txt_Gyousha_hikiai_flg.Text.Equals("True"))
                  && string.IsNullOrEmpty(this.form.txt_Genba_hikiai_flg.Text)
                  )
                   ||
                  (
                     this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("0")
                   && (this.form.txt_Gyousha_hikiai_flg.Text.Equals("1") || this.form.txt_Gyousha_hikiai_flg.Text.Equals("True"))
                  && this.form.txt_Genba_hikiai_flg.Text.Equals("1")
                  )
                   ||
                  // 顧客-顧客-引合
                  (
                     this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("0")
                   && (this.form.txt_Gyousha_hikiai_flg.Text.Equals("0") || this.form.txt_Gyousha_hikiai_flg.Text.Equals("False"))
                  && string.IsNullOrEmpty(this.form.txt_Genba_hikiai_flg.Text)
                  )
                   ||
                  (
                     this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("0")
                   && (this.form.txt_Gyousha_hikiai_flg.Text.Equals("0") || this.form.txt_Gyousha_hikiai_flg.Text.Equals("False"))
                  && this.form.txt_Genba_hikiai_flg.Text.Equals("1")
                  )
                   ||
                  // 顧客-顧客-顧客
                  (
                     this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("0")
                   && (this.form.txt_Gyousha_hikiai_flg.Text.Equals("0") || this.form.txt_Gyousha_hikiai_flg.Text.Equals("False"))
                  && this.form.txt_Genba_hikiai_flg.Text.Equals("0")
                 )
                )
                {
                    // 正常
                    ret = true;
                }
                else
                {
                    // エラーメッセージが登録されていないので、一時的にほかのメッセージIDを使用
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E081");

                    // 異常
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("HikiaiCheck", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(true, catchErr);
            }
            // 正常
            return ret;
        }

        #endregion 引合チェック

        #region HikiaiEntity作成と引合登録処理

        /// <summary>
        /// HikiaiEntity作成と引合登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        /// <returns>true:成功, false:失敗</returns>
        public bool CreateHikiaiEntityAndUpdateTables(bool errorFlag)
        {
            try
            {
                // CreateEntityとそれぞれの更新処理でDB更新が発生するため、UIFormから
                // 排他制御する
                using (Transaction tran = new Transaction())
                {
                    // 引合取引先、引合業者、引合現場登録処理
                    if (!this.RegistTorihikisakiGyoushaGenba(errorFlag))
                    {
                        return false;
                    }

                    //--------------------------------------------------------
                    // コミット
                    tran.Commit();

                    // メッセージ出力
                    // ※トランザクション処理中にメッセージボックスを表示しない
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                    if (HIKIAI_TORIHIKISAKI_WindowType.Equals(HIKIAI_TORIHIKISAKI.NEW_WINDOW_FLAG) || HIKIAI_GYOUSHA_WindowType.Equals(HIKIAI_GYOUSHA.NEW_WINDOW_FLAG) || HIKIAI_GENBA_WindowType.Equals(HIKIAI_GENBA.NEW_WINDOW_FLAG))
                    {
                        msgLogic.MessageBoxShow("I001", "登録");
                    }
                    else if (HIKIAI_TORIHIKISAKI_WindowType.Equals(HIKIAI_TORIHIKISAKI.UPDATE_WINDOW_FLAG) || HIKIAI_GYOUSHA_WindowType.Equals(HIKIAI_GYOUSHA.UPDATE_WINDOW_FLAG) || HIKIAI_GENBA_WindowType.Equals(HIKIAI_GENBA.UPDATE_WINDOW_FLAG))
                    {
                        msgLogic.MessageBoxShow("I001", "更新");
                    }
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

        #endregion HikiaiEntity作成と引合登録処理

        #region 引合取引先、引合業者、引合現場登録処理

        /// <summary>
        /// 引合取引先、引合業者、引合現場登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        /// <returns>true:成功, false:失敗</returns>
        public bool RegistTorihikisakiGyoushaGenba(bool errorFlag)
        {
            try
            {
                // 引合取引先
                switch (HIKIAI_TORIHIKISAKI_WindowType)
                {
                    case HIKIAI_TORIHIKISAKI.NEW_WINDOW_FLAG:

                        if (!this.CreateHikiaiTorihikisakiEntity())
                        {
                            return false;
                        }

                        this.HikiaiTorihikisakiRegist(errorFlag);

                        // 登録結果の再設定
                        this.form.txt_Torihikisaki_hikiai_flg.Text = "1";
                        // 変更後のコード表示
                        this.form.txt_TorihikisakiCD.Text = this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_CD;
                        this.form.beforTorihikisakiCD = this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_CD;

                        // 登録結果の再設定
                        this.form.txt_Torihikisaki_hikiai_flg.Text = "1";

                        break;

                    case HIKIAI_TORIHIKISAKI.UPDATE_WINDOW_FLAG:
                        this.CreateHikiaiTorihikisakiEntity();
                        this.HikiaiTorihikisakiUpdate(errorFlag);
                        break;

                    case HIKIAI_TORIHIKISAKI.REFERENCE_WINDOW_FLAG:
                        break;

                    default:
                        break;
                }

                // 引合業者
                switch (HIKIAI_GYOUSHA_WindowType)
                {
                    case HIKIAI_GYOUSHA.NEW_WINDOW_FLAG:

                        if (!this.CreateGyoushaEntity())
                        {
                            return false;
                        }

                        this.HikiaiGyoushaRegist(errorFlag);
                        // 登録結果の再設定
                        this.form.txt_Gyousha_hikiai_flg.Text = "1";
                        // 変更後のコード表示
                        this.form.txt_GyoushaCD.Text = this.dto.hikiaiGyoushaEntry.GYOUSHA_CD;

                        break;

                    case HIKIAI_GYOUSHA.UPDATE_WINDOW_FLAG:
                        this.CreateGyoushaEntity();
                        this.HikiaiGyoushaUpdate(errorFlag);
                        break;

                    case HIKIAI_GYOUSHA.REFERENCE_WINDOW_FLAG:
                        break;

                    default:
                        break;
                }

                // 引合現場
                switch (HIKIAI_GENBA_WindowType)
                {
                    case HIKIAI_GENBA.NEW_WINDOW_FLAG:

                        if (!this.CreateGenbaEntity())
                        {
                            return false;
                        }

                        this.HikiaiGenbaRegist(errorFlag);

                        // 登録結果の再設定
                        this.form.txt_Genba_hikiai_flg.Text = "1";
                        // 変更後のコード表示
                        this.form.txt_GenbaCD.Text = this.dto.hikiaiGenbaEntry.GENBA_CD;

                        break;

                    case HIKIAI_GENBA.UPDATE_WINDOW_FLAG:
                        this.CreateGenbaEntity();
                        this.HikiaiGebbaUpdate(errorFlag);

                        break;

                    case HIKIAI_GENBA.REFERENCE_WINDOW_FLAG:
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E080", "");
                    errorFlag = false;
                    return false;
                }
                else if (ex is SQLRuntimeException)
                {
                    // SQLエラー用メッセージを出力
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E093", "");
                    errorFlag = false;
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        #endregion 引合取引先、引合業者、引合現場登録処理

        #region 引合取引先登録処理

        /// <summary>
        /// 引合取引先登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void HikiaiTorihikisakiRegist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            // 引合取引先
            switch (HIKIAI_TORIHIKISAKI_WindowType)
            {
                case HIKIAI_TORIHIKISAKI.NEW_WINDOW_FLAG:
                    this.accessor.InsertHikiaiTorihikisakiEntry(this.dto.hikiaiTorihikisakiEntry);

                    break;

                case HIKIAI_TORIHIKISAKI.UPDATE_WINDOW_FLAG:
                    this.accessor.UpdateHikiaiTorihikisakiEntry(this.dto.hikiaiTorihikisakiEntry);

                    break;

                case HIKIAI_TORIHIKISAKI.REFERENCE_WINDOW_FLAG:
                    break;

                default:
                    break;
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion 引合取引先登録処理

        #region 引合業者登録処理

        /// <summary>
        /// 引合業者登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void HikiaiGyoushaRegist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            // 引合業者
            switch (HIKIAI_GYOUSHA_WindowType)
            {
                case HIKIAI_GYOUSHA.NEW_WINDOW_FLAG:
                    this.accessor.InsertHikiaiGyoushaEntry(this.dto.hikiaiGyoushaEntry);

                    break;

                case HIKIAI_GYOUSHA.UPDATE_WINDOW_FLAG:
                    this.accessor.UpdateHikiaiGyoushaEntry(this.dto.hikiaiGyoushaEntry);

                    break;

                case HIKIAI_GYOUSHA.REFERENCE_WINDOW_FLAG:
                    break;

                default:
                    break;
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion 引合業者登録処理

        #region 引合現場登録処理

        /// <summary>
        /// 引合現場登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void HikiaiGenbaRegist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            // 引合現場
            switch (HIKIAI_GENBA_WindowType)
            {
                case HIKIAI_GENBA.NEW_WINDOW_FLAG:
                    this.accessor.InsertHikiaiGenbaEntry(this.dto.hikiaiGenbaEntry);

                    break;

                case HIKIAI_GENBA.UPDATE_WINDOW_FLAG:
                    this.accessor.UpdateHikiaiGenbaEntry(this.dto.hikiaiGenbaEntry);

                    break;

                case HIKIAI_GENBA.REFERENCE_WINDOW_FLAG:
                    break;

                default:
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion 引合現場登録処理

        #region 引合取引先更新処理

        /// <summary>
        /// 引合取引先更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void HikiaiTorihikisakiUpdate(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            this.accessor.UpdateHikiaiTorihikisakiEntry(this.dto.hikiaiTorihikisakiEntry);

            LogUtility.DebugMethodEnd();
        }

        #endregion 引合取引先更新処理

        #region 引合業者更新処理

        /// <summary>
        /// 引合業者更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void HikiaiGyoushaUpdate(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            this.accessor.UpdateHikiaiGyoushaEntry(this.dto.hikiaiGyoushaEntry);

            LogUtility.DebugMethodEnd();
        }

        #endregion 引合業者更新処理

        #region 引合現場更新処理

        /// <summary>
        /// 引合現場更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void HikiaiGebbaUpdate(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            this.accessor.UpdateHikiaiGenbaEntry(this.dto.hikiaiGenbaEntry);

            LogUtility.DebugMethodEnd();
        }

        #endregion 引合現場更新処理

        #region 引合取引先情報作成

        /// <summary>
        /// 引合取引先情報作成
        /// </summary>
        /// <returns></returns>
        private bool CreateHikiaiTorihikisakiEntity()
        {
            LogUtility.DebugMethodStart();

            switch (HIKIAI_TORIHIKISAKI_WindowType)
            {
                case HIKIAI_TORIHIKISAKI.NEW_WINDOW_FLAG:

                    // 初期化
                    this.dto.hikiaiTorihikisakiEntry = new M_HIKIAI_TORIHIKISAKI();

                    // 取引先CDの最大値を取得
                    string maxtTorihikisakiCD = accessor.hikiaiTorihikisakiDao.GetMaxTorihikisakiCode(this.dto.hikiaiTorihikisakiEntry);

                    if ("000000".Equals(maxtTorihikisakiCD))
                    {
                        // 取引先CDの空き番号を取得
                        maxtTorihikisakiCD = accessor.hikiaiTorihikisakiDao.GetUselessTorihikisakiCode(this.dto.hikiaiTorihikisakiEntry);
                    }

                    // 取引先CDを設定
                    this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_CD = maxtTorihikisakiCD.PadLeft(6, '0');

                    break;

                case HIKIAI_TORIHIKISAKI.UPDATE_WINDOW_FLAG:
                    // 取引先CD
                    this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_CD = this.beforDto.hikiaiTorihikisakiEntry.TORIHIKISAKI_CD;
                    break;

                default:
                    break;
            }

            // 取引先名
            if (!string.IsNullOrEmpty(this.form.txt_TorihikisakiMei.Text))
            {
                this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_NAME_RYAKU = this.form.txt_TorihikisakiMei.Text.ToString();
                this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_NAME1 = this.form.txt_TorihikisakiMei.Text.ToString();
            }

            // 取引先ふりがな
            if (!string.IsNullOrEmpty(this.form.txt_TorihikisakiFurigana.Text))
            {
                this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_FURIGANA = this.form.txt_TorihikisakiFurigana.Text.ToString();
            }
            // 郵便番号
            if (!string.IsNullOrEmpty(this.form.txt_TorihikisakiPost.Text))
            {
                this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_POST = this.form.txt_TorihikisakiPost.Text;
            }
            // 住所
            if (!string.IsNullOrEmpty(this.form.txt_TorihikisakiAddress1.Text))
            {
                this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_ADDRESS1 = this.form.txt_TorihikisakiAddress1.Text;
            }
            if (!string.IsNullOrEmpty(this.form.txt_TorihikisakiAddress2.Text))
            {
                this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_ADDRESS2 = this.form.txt_TorihikisakiAddress2.Text;
            }
            // 担当者
            if (!string.IsNullOrEmpty(this.form.txt_TorihikisakiTantousya.Text))
            {
                this.dto.hikiaiTorihikisakiEntry.TANTOUSHA = this.form.txt_TorihikisakiTantousya.Text;
            }
            // 電話番号
            if (!string.IsNullOrEmpty(this.form.txt_TorihikisakiTel.Text))
            {
                this.dto.hikiaiTorihikisakiEntry.TORIHIKISAKI_TEL = this.form.txt_TorihikisakiTel.Text;
            }

            // 諸口区分
            this.dto.hikiaiTorihikisakiEntry.SHOKUCHI_KBN = false;
            //if (!string.IsNullOrEmpty(this.form.txt_Torihikisaki_shokuchi_kbn.Text) && this.form.txt_Torihikisaki_shokuchi_kbn.Text.Equals("False"))
            //{
            //    this.dto.hikiaiTorihikisakiEntry.SHOKUCHI_KBN = false;

            //}
            //else if (!string.IsNullOrEmpty(this.form.txt_Torihikisaki_shokuchi_kbn.Text) && this.form.txt_Torihikisaki_shokuchi_kbn.Text.Equals("True"))
            //{
            //    this.dto.hikiaiTorihikisakiEntry.SHOKUCHI_KBN = true;

            //}

            // 削除フラグ
            this.dto.hikiaiTorihikisakiEntry.DELETE_FLG = false;

            // 引合取引先フラグ(選択に設定）
            this.form.chk_HikiaiTorihikisakiFlg.Checked = true; ;

            // 20140717 katen No.5299 見積入力から引合取引先登録後、引合取引先を修正モードで開くと見積印字設定の代表者を印字区分がブランクになってしまっている start‏
            IM_SYS_INFODao daoIM_SYS_INFO = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            M_SYS_INFO[] sysInfo = daoIM_SYS_INFO.GetAllData();
            if (sysInfo != null && sysInfo.Length > 0 && !sysInfo[0].SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
            {
                this.dto.hikiaiTorihikisakiEntry.DAIHYOU_PRINT_KBN = sysInfo[0].SEIKYUU_DAIHYOU_PRINT_KBN;
            }
            // 20140717 katen No.5299 見積入力から引合取引先登録後、引合取引先を修正モードで開くと見積印字設定の代表者を印字区分がブランクになってしまっている end‏

            // 共通処理
            var dataBinderEntry = new DataBinderLogic<M_HIKIAI_TORIHIKISAKI>(this.dto.hikiaiTorihikisakiEntry);
            dataBinderEntry.SetSystemProperty(this.dto.hikiaiTorihikisakiEntry, false);

            switch (HIKIAI_TORIHIKISAKI_WindowType)
            {
                case HIKIAI_TORIHIKISAKI.UPDATE_WINDOW_FLAG:
                    // 更新の場合、前回の作成情報を更新しない
                    this.dto.hikiaiTorihikisakiEntry.CREATE_DATE = this.beforDto.hikiaiTorihikisakiEntry.CREATE_DATE;
                    this.dto.hikiaiTorihikisakiEntry.CREATE_USER = this.beforDto.hikiaiTorihikisakiEntry.CREATE_USER;
                    this.dto.hikiaiTorihikisakiEntry.CREATE_PC = this.beforDto.hikiaiTorihikisakiEntry.CREATE_PC;
                    break;

                default:
                    break;
            }

            LogUtility.DebugMethodEnd(this.dto.hikiaiTorihikisakiEntry);

            return true;
        }

        #endregion 引合取引先情報作成

        #region 引合業者登録情報作成

        /// <summary>
        /// 引合業者登録情報設定
        /// </summary>
        /// <returns></returns>
        private bool CreateGyoushaEntity()
        {
            LogUtility.DebugMethodStart();

            // 業者
            switch (HIKIAI_GYOUSHA_WindowType)
            {
                case HIKIAI_GYOUSHA.NEW_WINDOW_FLAG:

                    // 初期化
                    this.dto.hikiaiGyoushaEntry = new M_HIKIAI_GYOUSHA();

                    // 業者CDの最大値を取得
                    string maxtGyoushaCD = accessor.hikiaiGyoushaDao.GetMaxGyoushaCode(this.dto.hikiaiGyoushaEntry);

                    if ("000000".Equals(maxtGyoushaCD))
                    {
                        // 業者CDの空き番号を取得
                        maxtGyoushaCD = accessor.hikiaiGyoushaDao.GetUselessGyoushaCode(this.dto.hikiaiGyoushaEntry);
                    }

                    // 業者CDを設定
                    this.dto.hikiaiGyoushaEntry.GYOUSHA_CD = maxtGyoushaCD.PadLeft(6, '0');

                    break;

                case HIKIAI_GYOUSHA.UPDATE_WINDOW_FLAG:
                    // 業者CD
                    this.dto.hikiaiGyoushaEntry.GYOUSHA_CD = this.beforDto.hikiaiGyoushaEntry.GYOUSHA_CD;
                    break;

                default:
                    break;
            }

            // 業者名称
            if (!string.IsNullOrEmpty(this.form.txt_GyoushaName.Text))
            {
                this.dto.hikiaiGyoushaEntry.GYOUSHA_NAME_RYAKU = this.form.txt_GyoushaName.Text.ToString();
                this.dto.hikiaiGyoushaEntry.GYOUSHA_NAME1 = this.form.txt_GyoushaName.Text.ToString();
            }
            // 業者ふりがな
            if (!string.IsNullOrEmpty(this.form.txt_GyoushaFurigana.Text))
            {
                this.dto.hikiaiGyoushaEntry.GYOUSHA_FURIGANA = this.form.txt_GyoushaFurigana.Text.ToString();
            }

            // 郵便番号
            if (!string.IsNullOrEmpty(this.form.txt_GyousyaPost.Text))
            {
                this.dto.hikiaiGyoushaEntry.GYOUSHA_POST = this.form.txt_GyousyaPost.Text.ToString();
            }
            // 住所
            if (!string.IsNullOrEmpty(this.form.txt_GyousyaAddress1.Text))
            {
                this.dto.hikiaiGyoushaEntry.GYOUSHA_ADDRESS1 = this.form.txt_GyousyaAddress1.Text.ToString();
            }
            if (!string.IsNullOrEmpty(this.form.txt_GyousyaAddress2.Text))
            {
                this.dto.hikiaiGyoushaEntry.GYOUSHA_ADDRESS2 = this.form.txt_GyousyaAddress2.Text.ToString();
            }
            // 担当者
            if (!string.IsNullOrEmpty(this.form.txt_GyousyaTantousya.Text))
            {
                this.dto.hikiaiGyoushaEntry.TANTOUSHA = this.form.txt_GyousyaTantousya.Text.ToString();
            }
            // 電話番号
            if (!string.IsNullOrEmpty(this.form.txt_GyousyaTel.Text))
            {
                this.dto.hikiaiGyoushaEntry.GYOUSHA_TEL = this.form.txt_GyousyaTel.Text.ToString();
            }
            // 携帯電話
            if (!string.IsNullOrEmpty(this.form.txt_GyousyaKeitaiTel.Text))
            {
                this.dto.hikiaiGyoushaEntry.GYOUSHA_KEITAI_TEL = this.form.txt_GyousyaKeitaiTel.Text.ToString();
            }

            // 諸口区分
            this.dto.hikiaiGyoushaEntry.SHOKUCHI_KBN = false;
            //if (!string.IsNullOrEmpty(this.form.txt_Gyousha_shokuchi_kbn.Text) && this.form.txt_Gyousha_shokuchi_kbn.Text.Equals("False"))
            //{
            //    this.dto.hikiaiGyoushaEntry.SHOKUCHI_KBN = false;

            //}
            //else if (!string.IsNullOrEmpty(this.form.txt_Gyousha_shokuchi_kbn.Text) && this.form.txt_Gyousha_shokuchi_kbn.Text.Equals("True"))
            //{
            //    this.dto.hikiaiGyoushaEntry.SHOKUCHI_KBN = true;

            //}

            // 削除フラグ
            this.dto.hikiaiGyoushaEntry.DELETE_FLG = false;

            // 引合業者フラグ
            this.dto.hikiaiGyoushaEntry.HIKIAI_TORIHIKISAKI_USE_FLG = this.form.chk_HikiaiTorihikisakiFlg.Checked;

            // 共通処理
            var dataBinderEntry = new DataBinderLogic<M_HIKIAI_GYOUSHA>(this.dto.hikiaiGyoushaEntry);
            dataBinderEntry.SetSystemProperty(this.dto.hikiaiGyoushaEntry, false);
            switch (HIKIAI_GYOUSHA_WindowType)
            {
                case HIKIAI_GYOUSHA.UPDATE_WINDOW_FLAG:
                    // 更新の場合、前回の作成情報を更新しない
                    this.dto.hikiaiGyoushaEntry.CREATE_DATE = this.beforDto.hikiaiGyoushaEntry.CREATE_DATE;
                    this.dto.hikiaiGyoushaEntry.CREATE_USER = this.beforDto.hikiaiGyoushaEntry.CREATE_USER;
                    this.dto.hikiaiGyoushaEntry.CREATE_PC = this.beforDto.hikiaiGyoushaEntry.CREATE_PC;
                    break;

                default:
                    break;
            }
            LogUtility.DebugMethodEnd();

            return true;
        }

        #endregion 引合業者登録情報作成

        #region 引合現場登録情報作成

        /// <summary>
        /// 引合現場情登録報作成
        /// </summary>
        /// <returns></returns>
        private bool CreateGenbaEntity()
        {
            LogUtility.DebugMethodStart();

            switch (HIKIAI_GENBA_WindowType)
            {
                case HIKIAI_GENBA.NEW_WINDOW_FLAG:

                    // 初期化
                    this.dto.hikiaiGenbaEntry = new M_HIKIAI_GENBA();

                    // 現場CDの最大値を取得
                    string maxtGenbaCD = accessor.hikiaiGenbaDao.GetMaxGenbaCode(this.dto.hikiaiGenbaEntry);

                    if ("000000".Equals(maxtGenbaCD))
                    {
                        // 現場CDの空き番号を取得
                        maxtGenbaCD = accessor.hikiaiGenbaDao.GetUselessGenbaCode(this.dto.hikiaiGenbaEntry);
                    }

                    // 現場CDを設定
                    this.dto.hikiaiGenbaEntry.GENBA_CD = maxtGenbaCD.PadLeft(6, '0');

                    break;

                case HIKIAI_GENBA.UPDATE_WINDOW_FLAG:
                    // 現場CD
                    this.dto.hikiaiGenbaEntry.GENBA_CD = beforDto.hikiaiGenbaEntry.GENBA_CD;
                    break;

                default:
                    break;
            }

            // 業者コード
            if (!string.IsNullOrEmpty(this.form.txt_GyoushaCD.Text))
            {
                this.dto.hikiaiGenbaEntry.GYOUSHA_CD = this.form.txt_GyoushaCD.Text.ToString();
            }

            // 現場名
            if (!string.IsNullOrEmpty(this.form.txtGenbaName.Text))
            {
                this.dto.hikiaiGenbaEntry.GENBA_NAME_RYAKU = this.form.txtGenbaName.Text.ToString();
                this.dto.hikiaiGenbaEntry.GENBA_NAME1 = this.form.txtGenbaName.Text.ToString();
            }
            // 現場ふりがな
            if (!string.IsNullOrEmpty(this.form.txtGenbaFurigana.Text))
            {
                this.dto.hikiaiGenbaEntry.GENBA_FURIGANA = this.form.txtGenbaFurigana.Text.ToString();
            }

            // 郵便番号
            if (!string.IsNullOrEmpty(this.form.txt_GenbaPost.Text))
            {
                this.dto.hikiaiGenbaEntry.GENBA_POST = this.form.txt_GenbaPost.Text.ToString();
            }
            // 住所
            if (!string.IsNullOrEmpty(this.form.txt_GenbaAddress1.Text))
            {
                this.dto.hikiaiGenbaEntry.GENBA_ADDRESS1 = this.form.txt_GenbaAddress1.Text.ToString();
            }
            if (!string.IsNullOrEmpty(this.form.txt_GenbaAddress1.Text))
            {
                this.dto.hikiaiGenbaEntry.GENBA_ADDRESS2 = this.form.txt_GenbaAddress2.Text.ToString();
            }
            // 担当者
            if (!string.IsNullOrEmpty(this.form.txt_GenbaTantousya.Text))
            {
                this.dto.hikiaiGenbaEntry.TANTOUSHA = this.form.txt_GenbaTantousya.Text.ToString();
            }
            // 電話番号
            if (!string.IsNullOrEmpty(this.form.txt_GenbaTel.Text))
            {
                this.dto.hikiaiGenbaEntry.GENBA_TEL = this.form.txt_GenbaTel.Text.ToString();
            }
            // 携帯電話
            if (!string.IsNullOrEmpty(this.form.txt_GyousyaKeitaiTel.Text))
            {
                this.dto.hikiaiGenbaEntry.GENBA_KEITAI_TEL = this.form.txt_GyousyaKeitaiTel.Text.ToString();
            }

            // 諸口区分
            this.dto.hikiaiGenbaEntry.SHOKUCHI_KBN = false;
            //if (!string.IsNullOrEmpty(this.form.txt_Genba_shokuchi_kbn.Text) && this.form.txt_Genba_shokuchi_kbn.Text.Equals("False"))
            //{
            //    this.dto.hikiaiGenbaEntry.SHOKUCHI_KBN = false;

            //}
            //else if (!string.IsNullOrEmpty(this.form.txt_Genba_shokuchi_kbn.Text) && this.form.txt_Genba_shokuchi_kbn.Text.Equals("True"))
            //{
            //    this.dto.hikiaiGenbaEntry.SHOKUCHI_KBN = true;

            //}

            // 削除フラグ
            this.dto.hikiaiGenbaEntry.DELETE_FLG = false;

            // 引合取引先フラグ
            this.dto.hikiaiGenbaEntry.HIKIAI_TORIHIKISAKI_USE_FLG = this.form.chk_HikiaiTorihikisakiFlg.Checked;

            // 引合業者フラグ
            this.dto.hikiaiGenbaEntry.HIKIAI_GYOUSHA_USE_FLG = this.form.chk_HikiaiGyoushaFlg.Checked;

            // 共通処理
            var dataBinderEntry = new DataBinderLogic<M_HIKIAI_GENBA>(this.dto.hikiaiGenbaEntry);
            dataBinderEntry.SetSystemProperty(this.dto.hikiaiGenbaEntry, false);
            switch (HIKIAI_GENBA_WindowType)
            {
                case HIKIAI_GENBA.UPDATE_WINDOW_FLAG:
                    // 更新の場合、前回の作成情報を更新しない
                    this.dto.hikiaiGenbaEntry.CREATE_DATE = this.beforDto.hikiaiGenbaEntry.CREATE_DATE;
                    this.dto.hikiaiGenbaEntry.CREATE_USER = this.beforDto.hikiaiGenbaEntry.CREATE_USER;
                    this.dto.hikiaiGenbaEntry.CREATE_PC = this.beforDto.hikiaiGenbaEntry.CREATE_PC;
                    break;

                default:
                    break;
            }
            LogUtility.DebugMethodEnd();

            return true;
        }

        #endregion 引合現場登録情報作成

        #region 取引先タブをクリア

        /// <summary>
        /// 取引先タブをクリア
        /// </summary>
        public void initTorihikisakiTab()
        {
            LogUtility.DebugMethodStart();

            // 取引先名称クリア
            this.form.txt_TorihikisakiMei.Text = string.Empty;
            this.form.txt_TorihikisakiMei.ReadOnly = true;
            this.form.txt_TorihikisakiMei.TabStop = false;
            this.form.txt_TorihikisakiFurigana.Text = string.Empty;

            // 取引先タブをクリア
            this.form.txt_TorihikisakiPost.Text = string.Empty;
            this.form.txt_TorihikisakiAddress1.Text = string.Empty;
            this.form.txt_TorihikisakiAddress2.Text = string.Empty;
            this.form.txt_TorihikisakiTantousya.Text = string.Empty;
            this.form.txt_TorihikisakiTel.Text = string.Empty;

            this.form.txt_TorihikisakiPost.ReadOnly = false;
            this.form.txt_TorihikisakiAddress1.ReadOnly = false;
            this.form.txt_TorihikisakiAddress2.ReadOnly = false;
            this.form.txt_TorihikisakiTantousya.ReadOnly = false;
            this.form.txt_TorihikisakiTel.ReadOnly = false;

            LogUtility.DebugMethodEnd();
        }

        #endregion 取引先タブをクリア

        #region 業者タブをクリア

        /// <summary>
        /// 業者タブをクリア
        /// </summary>
        public void initGyoushaTab()
        {
            LogUtility.DebugMethodStart();

            // 業者名称
            this.form.txt_GyoushaName.Text = string.Empty;
            this.form.txt_GyoushaName.ReadOnly = true;
            this.form.txt_GyoushaName.TabStop = false;
            this.form.txt_GyoushaFurigana.Text = string.Empty;

            // 現場
            this.form.txt_GenbaCD.Text = string.Empty;
            this.initGenbaTab();

            // 業者タブの設定をクリア
            this.form.txt_GyousyaPost.Text = string.Empty;
            this.form.txt_GyousyaAddress1.Text = string.Empty;
            this.form.txt_GyousyaAddress2.Text = string.Empty;
            this.form.txt_GyousyaTantousya.Text = string.Empty;
            this.form.txt_GyousyaTel.Text = string.Empty;
            this.form.txt_GyousyaKeitaiTel.Text = string.Empty;

            this.form.txt_GyousyaPost.ReadOnly = false;
            this.form.txt_GyousyaAddress1.ReadOnly = false;
            this.form.txt_GyousyaAddress2.ReadOnly = false;
            this.form.txt_GyousyaTantousya.ReadOnly = false;
            this.form.txt_GyousyaTel.ReadOnly = false;
            this.form.txt_GyousyaKeitaiTel.ReadOnly = false;

            LogUtility.DebugMethodEnd();
        }

        #endregion 業者タブをクリア

        #region 現場タブをクリア

        /// <summary>
        /// 現場タブをクリア
        /// </summary>
        public void initGenbaTab()
        {
            LogUtility.DebugMethodStart();

            //現場名称
            this.form.txtGenbaName.Text = string.Empty;
            this.form.txtGenbaName.ReadOnly = true;
            this.form.txtGenbaName.TabStop = false;
            this.form.txtGenbaFurigana.Text = string.Empty;

            this.form.txt_GenbaPost.Text = string.Empty;
            this.form.txt_GenbaAddress1.Text = string.Empty;
            this.form.txt_GenbaAddress2.Text = string.Empty;
            this.form.txt_GenbaTantousya.Text = string.Empty;
            this.form.txt_GenbaTel.Text = string.Empty;
            this.form.txt_GenbaKeitaiTel.Text = string.Empty;

            this.form.txt_GenbaPost.ReadOnly = false;
            this.form.txt_GenbaAddress1.ReadOnly = false;
            this.form.txt_GenbaAddress2.ReadOnly = false;
            this.form.txt_GenbaTantousya.ReadOnly = false;
            this.form.txt_GenbaTel.ReadOnly = false;
            this.form.txt_GenbaKeitaiTel.ReadOnly = false;

            LogUtility.DebugMethodEnd();
        }

        #endregion 現場タブをクリア

        #region 拠点名称取得

        /// <summary>
        /// 拠点名称取得
        /// </summary>
        /// <param name="kyotenEntity"></param>
        private string getKyotenName(string kyotenCode)
        {
            LogUtility.DebugMethodStart(kyotenCode);

            string kyotenName = string.Empty;

            M_KYOTEN kyotenEntity = new M_KYOTEN();
            kyotenEntity.KYOTEN_CD = Int16.Parse(kyotenCode);

            // 拠点マスタ取得
            var kyotens = this.accessor.GetDataByCodeForKyoten((short)kyotenEntity.KYOTEN_CD);
            if (kyotens != null && 0 < kyotens.Length)
            {
                this.dto.kyotenEntity = kyotens[0];

                LogUtility.DebugMethodEnd();
                return this.dto.kyotenEntity.KYOTEN_NAME_RYAKU;
            }
            else
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "拠点");
                //this.form.txt_Kyoten1NameRyaku.Text = "";
                //this.form.txt_InjiKyoten1CD.Focus();

                LogUtility.DebugMethodEnd();

                return kyotenName;
            }
        }

        #endregion 拠点名称取得

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
            if (kyotens != null && 0 < kyotens.Length && kyotens[0].DELETE_FLG.IsFalse)
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

        #endregion 拠点データ取得処理

        #region 共通で使用する計算処理クラス

        /// <summary>
        /// 共通で使用する計算処理クラス
        /// </summary>
        public static class CommonCalc
        {
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
            /// 端数処理桁用Enum
            /// </summary>
            private enum hasuKetaType : short
            {
                NONE = 1,       // 1の位
                ONEPOINT,       // 小数第一位
                TOWPOINT,       // 小数第二位
                THREEPOINT,     // 小数第三位
                FOUR,           // 小数第四位
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
                decimal sign = 1;
                if (kingaku < 0)
                {
                    sign = -1;
                }

                kingaku = Math.Abs(kingaku);

                switch ((fractionType)calcCD)
                {
                    case fractionType.CEILING:
                        returnVal = kingaku < 0 ? Math.Floor(kingaku) : Math.Ceiling(kingaku);
                        break;

                    case fractionType.FLOOR:
                        returnVal = kingaku < 0 ? Math.Truncate(kingaku) : Math.Floor(kingaku);
                        break;

                    case fractionType.ROUND:
                        returnVal = Math.Round(kingaku, 0, MidpointRounding.AwayFromZero);
                        break;

                    default:
                        // 何もしない
                        returnVal = kingaku;
                        break;
                }

                returnVal = returnVal * sign;

                return returnVal;
            }

            /// <summary>
            /// 金額の共通フォーマットメソッド
            /// 単価などM_SYS_INFO等にフォーマットが設定されている
            /// ものについては使用しないでください
            /// </summary>
            /// <param name="num"></param>
            /// <returns></returns>
            public static string DecimalFormat(decimal num)
            {
                string format = "#,##0";
                return string.Format("{0:" + format + "}", num);
            }
        }

        #endregion 共通で使用する計算処理クラス

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

        #endregion Equals/GetHashCode/ToString

        #region 前の番号を取得

        /// <summary>
        /// 前の受付番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="numberValue">受付番号</param>
        /// <returns>前の受付番号</returns>
        internal String GetPreviousNumber(String tableName, String fieldName, String numberValue, out bool catchErr)
        {
            string returnVal = string.Empty;
            DataTable dt = new DataTable();
            catchErr = false;
            try
            {
                string selectStr;
                if (String.IsNullOrEmpty(numberValue))
                {
                    selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 AND KYOTEN_CD = " + this.headerForm.KYOTEN_CD.Text;
                    // データ取得
                    dt = this.accessor.mitsumoriEntryDao.GetDateForStringSql(selectStr);
                    returnVal = Convert.ToString(dt.Rows[0]["MAX_NUMBER"]);
                    return returnVal;
                }

                // SQL文作成(冗長にならないためsqlファイルで管理しない)

                selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                selectStr += " WHERE " + fieldName + " < " + long.Parse(numberValue);
                selectStr += "   AND DELETE_FLG = 0 AND KYOTEN_CD = " + this.headerForm.KYOTEN_CD.Text;

                // データ取得
                dt = this.accessor.mitsumoriEntryDao.GetDateForStringSql(selectStr);
                if (Convert.ToString(dt.Rows[0]["MAX_NUMBER"]) == "")
                {
                    selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 AND KYOTEN_CD = " + this.headerForm.KYOTEN_CD.Text;
                    // データ取得
                    dt = this.accessor.mitsumoriEntryDao.GetDateForStringSql(selectStr);
                }

                // MAX_NUMBERをセット
                returnVal = Convert.ToString(dt.Rows[0]["MAX_NUMBER"]);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetPreviousNumber", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }

            return returnVal;
        }

        #endregion 前の番号を取得

        #region 次の番号を取得

        /// <summary>
        /// 次の受付番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="numberValue">受付番号</param>
        /// <returns>次の受付番号</returns>
        internal String GetNextNumber(String tableName, String fieldName, String numberValue, out bool catchErr)
        {
            String returnVal = string.Empty;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(tableName, fieldName, numberValue);
                DataTable dt = new DataTable();
                string selectStr;
                if (String.IsNullOrEmpty(numberValue))
                {
                    selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 AND KYOTEN_CD = " + this.headerForm.KYOTEN_CD.Text;
                    // データ取得
                    dt = this.accessor.mitsumoriEntryDao.GetDateForStringSql(selectStr);
                    returnVal = Convert.ToString(dt.Rows[0]["MIN_NUMBER"]);
                    return returnVal;
                }

                // SQL文作成(冗長にならないためsqlファイルで管理しない)
                selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                selectStr += " WHERE " + fieldName + " > " + long.Parse(numberValue);
                selectStr += "   AND DELETE_FLG = 0 AND KYOTEN_CD = " + this.headerForm.KYOTEN_CD.Text;

                // データ取得
                dt = this.accessor.mitsumoriEntryDao.GetDateForStringSql(selectStr);
                if (Convert.ToString(dt.Rows[0]["MIN_NUMBER"]) == "")
                {
                    selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 AND KYOTEN_CD = " + this.headerForm.KYOTEN_CD.Text;
                    // データ取得
                    dt = this.accessor.mitsumoriEntryDao.GetDateForStringSql(selectStr);
                }

                // MAX_UKETSUKE_NUMBERをセット
                returnVal = Convert.ToString(dt.Rows[0]["MIN_NUMBER"]);

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetNextNumber", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
            return returnVal;
        }

        #endregion 次の番号を取得

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
                case "btn_TorihikisakieSearch":
                    textName = "txt_TorihikisakiCD";
                    break;

                case "btn_GyousyaSearch":
                    textName = "txt_GyoushaCD";
                    break;

                case "btn_GenbaSearch":
                    textName = "txt_GenbaCD";
                    break;

                default:
                    break;
            }
            return textName;
        }

        #endregion ポップアップボタン名により、テキスト名を取得

        /// <summary>
        /// 入力担当者チェック
        /// </summary>
        internal bool CheckNyuuryokuTantousha()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
                // 初期化
                this.form.txt_ShainNameRyaku.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.txt_ShainCD.Text))
                {
                    // 入力担当者CDがなければ既にエラーが表示されているはずなので何もしない
                    return ret;
                }

                var shainEntity = this.accessor.GetShain(this.form.txt_ShainCD.Text);
                if (shainEntity == null)
                {
                    return ret;
                }

                if (shainEntity.EIGYOU_TANTOU_KBN.IsFalse)
                {
                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E058", "");
                    this.form.txt_ShainCD.Focus();
                }
                else
                {
                    this.form.txt_ShainNameRyaku.Text = shainEntity.SHAIN_NAME_RYAKU;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNyuuryokuTantousha", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }

        #endregion 業務処理

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

        #region Utility

        /// <summary>
        /// WINDOWTYPEからデータ取得が必要かどうか判断します
        /// </summary>
        /// <returns>True:データ取得が必要, Flase:データ取得が不必要</returns>
        private bool IsRequireData()
        {
            if (WINDOW_TYPE.UPDATE_WINDOW_FLAG.Equals(this.form.WindowType)
                || WINDOW_TYPE.DELETE_WINDOW_FLAG.Equals(this.form.WindowType)
                || WINDOW_TYPE.REFERENCE_WINDOW_FLAG.Equals(this.form.WindowType)
                )
            {
                return true;
            }

            return false;
        }

        #endregion Utility

        #region ユーザー定義情報取得処理

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

        #endregion ユーザー定義情報取得処理

        /// <summary>
        /// true,falseの数字変換
        /// </summary>
        public enum BooleanToInt
        {
            FALSE = 0,
            TRUE = 1
        };

        #region DBNull値を指定値に変換

        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">チェック対象</param>
        /// <param name="value">設定値</param>
        /// <returns>object</returns>
        private object ChgDBNullToValue(object obj, object value)
        {
            if (obj is DBNull)
            {
                return value;
            }
            else
            {
                return obj;
            }
        }

        #endregion DBNull値を指定値に変換

        /// <summary>
        /// データ移動処理
        /// </summary>
        internal bool SetMoveData()
        {
            bool ret = true;
            try
            {
                if (this.form.moveData_flg)
                {
                    this.form.txt_TorihikisakiCD.Text = this.form.moveData_torihikisakiCd;
                    this.form.txt_TorihikisakiCD_Validating(null, null);

                    this.form.txt_GyoushaCD.Text = this.form.moveData_gyousyaCd;
                    this.form.txt_GyoushaCD_Validating(null, null);

                    this.form.txt_GenbaCD.Text = this.form.moveData_genbaCd;
                    this.form.txt_GenbaCD_Validating(null, null);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetMoveData", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        // 201400704 syunrei EV002427_見積入力の税計算区分がシステム設定の税区分締処理利用形態を参照していない　start
        /// <summary>
        /// システム設定入力の税区分によって、画面の税区分制御
        /// </summary>
        private SqlInt16 GetZeiKbn()
        {
            //システム設定入力の税区分情報取得
            M_SYS_INFO mSysInfo = new Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Accessor.DBAccessor().GetSysInfo();
            SqlInt16 sysZeiKbn = SqlInt16.Null;
            return sysZeiKbn = mSysInfo.MITSUMORI_ZEIKEISAN_KBN_CD;
        }

        /// <summary>
        /// 伝票毎時に明細に対して、明細税区分を制御
        /// </summary>
        public bool SetZeiKbnCtr(bool ctrl)
        {
            bool ret = true;
            try
            {
                //伝票毎時に明細に対して、明細税区分を制御
                if (this.form.CustomDataGridView.Columns.Count > 0)
                {
                    DataGridViewColumn dc = this.form.CustomDataGridView.Columns[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD];
                    dc.ReadOnly = ctrl;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetZeiKbnCtr", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 初期の場合、明細税区分を制御
        /// </summary>
        public bool InitSaiZeiKbnCtrl()
        {
            bool ret = false;
            try
            {
                SqlInt16 sysZeiKbn = this.GetZeiKbn();
                //税区分/締処理利用形態が伝票毎
                if (sysZeiKbn == 1)
                {
                    this.form.txt_ZeiKeisanKbnCD.Text = "1";
                    //伝票毎の場合、明細税区分が利用不可
                    if (!this.SetZeiKbnCtr(true)) { return ret; }
                }
                else
                {
                    this.form.txt_ZeiKeisanKbnCD.Text = "2";
                    //伝票毎以外の場合、明細税区分が利用可
                    if (!this.SetZeiKbnCtr(false)) { return ret; }
                }
                if (!this.SetGamenZeiKbn(this.form.txt_ZeiKeisanKbnCD.Text)) { return ret; }
                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("InitSaiZeiKbnCtrl", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 「税計算区分※」に「2.明細毎」を選択した場合　「税区分※」は使用不可、入力不可にする。
        /// </summary>
        public bool SetGamenZeiKbn(string zeiKSKBN)
        {
            bool ret = true;
            try
            {
                if (zeiKSKBN.Equals("2"))
                {
                    //　税計算区分＝2.明細毎　の場合 2.内税	表示	入力可

                    this.form.rdo_Uchizei.Enabled = true;
                    //画面の税区分が１、2、３選択できるだけ
                    this.form.txt_ZeiKbnCD.CharacterLimitList = new char[] {
                    '1',
                    '2',
                    '3',
                    '\0'};
                }
                else
                {
                    // 税区分が２の場合は１にセットする
                    if (this.form.txt_ZeiKbnCD.Text == "2")
                    {
                        this.form.txt_ZeiKbnCD.Text = "1";
                    }

                    //　税計算区分＝1.伝票毎　の場合 2.内税	表示	入力不可
                    this.form.rdo_Uchizei.Enabled = false;
                    //画面の税区分が１、３選択できるだけ
                    this.form.txt_ZeiKbnCD.CharacterLimitList = new char[] {
                    '1',
                    '3',
                    '\0'};
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGamenZeiKbn", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            return ret;
        }

        // 201400704 syunrei EV002427_見積入力の税計算区分がシステム設定の税区分締処理利用形態を参照していない　end

        // 201400708 syunrei ＃947　№11　　start
        /// <summary>
        /// 明細の税区分が表示しない
        /// </summary>
        public bool SetMeisaiZeiKbnCtr(bool bl)
        {
            bool ret = true;
            try
            {
                // ページ分データ作成
                for (int page = 0; page < this.form.tbc_MitsumoriMeisai.TabPages.Count; page++)
                {
                    // CustomDataGridViewを取得する
                    CustomDataGridView cdgv = ((CustomDataGridView)this.form.tbc_MitsumoriMeisai.TabPages[page].Controls["control"].Controls["CustomDataGridView"]);
                    //明細の税区分が表示しない
                    DataGridViewColumn dgc = cdgv.Columns[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD];
                    dgc.Visible = bl;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetMeisaiZeiKbnCtr", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            return ret;
        }

        // 201400708 syunrei ＃947　№11　　end

        // 201400708 syunrei ＃947　№15　　start
        /// <summary>
        /// ・見積入力画面内、状況項目内、進行中項目は削除する。
        /// </summary>
        private void DeleteJoukyo()
        {
            //進行中表示しない
            this.form.rdo_Shinkou.Visible = false;
            this.form.dtp_ShinkouDate.Visible = false;
            //テキスト入力範囲設定
            this.form.txt_JykyoFlg.CharacterLimitList = new char[] {
            // 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 start
                    //'2',
                    //'3',
                    //'\0'};
                    '1',
                    '2',
                    '\0'};
            // 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 end
        }

        // 201400708 syunrei ＃947　№15　　end

        // 20140711 ria No.947 営業管理機能改修 start
        //状況の入力値をチェックする
        internal bool CheckJokyoCd(out bool catchErr)
        {
            LogUtility.DebugMethodStart();
            bool returnVal = true;
            catchErr = false;
            try
            {
                // 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 start
                //if (this.form.txt_JykyoFlg.Text != "2" && this.form.txt_JykyoFlg.Text != "3")
                if (this.form.txt_JykyoFlg.Text != "1"
                    && this.form.txt_JykyoFlg.Text != "2"
                    && this.form.txt_JykyoFlg.Text != string.Empty)
                // 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 end
                {
                    returnVal = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckJokyoCd", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E245", "");
                returnVal = false;
                catchErr = true;
            }
            LogUtility.DebugMethodEnd(returnVal, catchErr);
            return returnVal;
        }

        // 20140711 ria No.947 営業管理機能改修 end

        /// <summary>
        /// 伝票区分設定（明細の品名から伝票区分を設定する）
        /// </summary>
        /// <returns></returns>
        internal bool SetDenpyouKbn()
        {
            LogUtility.DebugMethodStart();

            try
            {
                // DataGridViewRowを取得する
                DataGridViewRow targetRow = this.form.CustomDataGridView.CurrentRow;
                // 取得できなければ終了
                if (targetRow == null)
                {
                    return true;
                }

                // 品名CDの入力が無ければ終了
                if (targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value == null
                    || string.IsNullOrEmpty(targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value.ToString()))
                {
                    return true;
                }

                // 品名CDから品名マスタを取得
                M_HINMEI[] hinmeis = this.accessor.GetAllValidHinmeiData(targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value.ToString());
                if (hinmeis == null || hinmeis.Count() < 1)
                {
                    // 存在しない品名が選択されている場合は終了
                    return true;
                }
                var targetHimei = hinmeis[0];

                if (targetHimei.DENPYOU_KBN_CD.ToString().Equals(MitumorisyoConst.DENPYOU_KBN_CD_KYOTU))
                {
                    if (string.IsNullOrEmpty(targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value.ToString()))
                    {
                        // 初期化
                        targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value = string.Empty;
                        targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME].Value = string.Empty;

                        // 品名マスタの伝票区分が「共通」の場合、ポップアップを表示し、ユーザーに伝票区分を選択してもらう
                        ICustomControl targetCtl = (ICustomControl)targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD];
                        CustomControlExtLogic.PopUp(targetCtl);

                        var denpyouKbnCd = targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value;
                        if (denpyouKbnCd == null
                            || string.IsNullOrEmpty(denpyouKbnCd.ToString()))
                        {
                            // 伝票区分がセットされない場合（ポップアップがキャンセルされた場合など）は、伝票区分名を初期化、戻り値はFalse
                            targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME].Value = string.Empty;
                            return false;
                        }
                    }
                }
                else
                {
                    // 品名マスタの伝票区分が「共通」以外の場合、そのまま反映する
                    targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value = targetHimei.DENPYOU_KBN_CD;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDenpyouKbn", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                return false;
            }
            LogUtility.DebugMethodStart();

            return true;
        }

        // 20140717 syunrei EV005312_最初に業者CDをセットしても、最初に現場CDをセットしても紐づく取引先CDがセットされない。　start

        #region 取引先CD・業者CD・現場CDの関連チェック処理

        /// <summary>
        /// 取引先チェック
        /// </summary>
        internal bool CheckTorihikisaki(out bool catchErr)
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            catchErr = false;
            var msgLogic = new MessageBoxShowLogic();
            try
            {
                var inputTorihikisakiCd = this.form.txt_TorihikisakiCD.Text;

                // 取引先CDの空チェック
                if (!string.IsNullOrEmpty(inputTorihikisakiCd))
                {
                    // 既存を表示するか引合を表示するか切り替えます
                    if (torihikisakiDisplaySwitching())
                    {
                        // 既存表示の場合
                        if (!this.form.hikiaiDisplaySwitchingFlg)
                        {
                            // 既存
                            SqlDateTime tekiyouDate = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
                            DateTime date;
                            if (!string.IsNullOrWhiteSpace(this.form.dtp_MitsumoriDate.Text) && DateTime.TryParse(this.form.dtp_MitsumoriDate.Text, out date))
                            {
                                tekiyouDate = date;
                            }
                            var torihikisakiEntity = this.accessor.GetTorihikisaki(inputTorihikisakiCd, tekiyouDate);
                            if (null != torihikisakiEntity)
                            {
                                bool toriCheck = CheckTorihikisakiAndKyotenCd(null, torihikisakiEntity, this.form.txt_TorihikisakiCD.Text, out catchErr);
                                if (catchErr) { return false; }
                                if (toriCheck)
                                {
                                    if (!torihikisakiNameChangedFlg)
                                    {
                                        // 20151021 katen #13337 品名手入力に関する機能修正 start
                                        // 取引先の拠点と入力された拠点コードの関連チェックOK
                                        if (torihikisakiEntity.SHOKUCHI_KBN.IsTrue)
                                        {
                                            this.form.txt_TorihikisakiMei.Text = torihikisakiEntity.TORIHIKISAKI_NAME1;
                                        }
                                        else
                                        {
                                            this.form.txt_TorihikisakiMei.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                                        }
                                        // 20151021 katen #13337 品名手入力に関する機能修正 end
                                    }
                                    this.form.txt_TorihikisakiMei.ReadOnly = (bool)!torihikisakiEntity.SHOKUCHI_KBN;
                                }
                                else
                                {
                                    ret = false;
                                }
                            }
                            else
                            {
                                ret = false;
                            }
                        }
                        // 引合表示の場合
                        else if (this.form.hikiaiDisplaySwitchingFlg)
                        {
                            // 引合
                            var torihikisakiEntity_hiki = this.accessor.GetHikiaiTorihikisakiEntry(inputTorihikisakiCd);
                            if (null != torihikisakiEntity_hiki)
                            {
                                bool hikiToriCheck = CheckTorihikisakiAndKyotenCd(torihikisakiEntity_hiki, null, this.form.txt_TorihikisakiCD.Text, out catchErr);
                                if (catchErr) { return false; }
                                if (hikiToriCheck)
                                {
                                    if (!torihikisakiNameChangedFlg)
                                    {
                                        // 20151021 katen #13337 品名手入力に関する機能修正 start
                                        // 取引先の拠点と入力された拠点コードの関連チェックOK
                                        if (torihikisakiEntity_hiki.SHOKUCHI_KBN.IsTrue)
                                        {
                                            this.form.txt_TorihikisakiMei.Text = torihikisakiEntity_hiki.TORIHIKISAKI_NAME1;
                                        }
                                        else
                                        {
                                            this.form.txt_TorihikisakiMei.Text = torihikisakiEntity_hiki.TORIHIKISAKI_NAME_RYAKU;
                                        }
                                        // 20151021 katen #13337 品名手入力に関する機能修正 end
                                    }
                                    this.form.txt_TorihikisakiMei.ReadOnly = false;
                                }
                                else
                                {
                                    ret = false;
                                }
                            }
                            else
                            {
                                ret = false;
                            }
                        }
                    }
                    else
                    {
                        // 既存でも引合でもヒットしなかった場合エラー
                        this.form.txt_TorihikisakiCD.Focus();
                        ret = false;
                    }
                }
                else
                {
                    if (!torihikisakiNameChangedFlg)
                    {
                        // 取引先CDが空の場合、関連項目クリア
                        this.form.txt_TorihikisakiMei.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTorihikisaki", ex);
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        /// <summary>
        /// 業者チェック
        /// </summary>
        internal bool CheckGyousha(out bool catchErr)
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            catchErr = false;
            var msgLogic = new MessageBoxShowLogic();
            try
            {
                var inputGyoushaCd = this.form.txt_GyoushaCD.Text;

                if (String.IsNullOrEmpty(inputGyoushaCd))
                {
                    // 同時に現場コードもクリア
                    this.form.txt_GyoushaName.Text = String.Empty;
                }
                else
                {
                    // 既存を表示するか引合を表示するか切り替えます
                    if (gyoushaDisplaySwitching())
                    {
                        // 既存表示の場合
                        if (!this.form.hikiaiDisplaySwitchingFlg)
                        {
                            SqlDateTime tekiyouDate = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
                            DateTime date;
                            if (!string.IsNullOrWhiteSpace(this.form.dtp_MitsumoriDate.Text) && DateTime.TryParse(this.form.dtp_MitsumoriDate.Text, out date))
                            {
                                tekiyouDate = date;
                            }
                            var gyoushaEntity = this.accessor.GetGyousha(inputGyoushaCd, tekiyouDate);

                            // 20151021 katen #13337 品名手入力に関する機能修正 start
                            if (null != gyoushaEntity)
                            {
                                // 業者名
                                if (!this.gyoushaNameChangedFlg)
                                {
                                    if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                                    {
                                        this.form.txt_GyoushaName.Text = gyoushaEntity.GYOUSHA_NAME1;
                                    }
                                    else
                                    {
                                        this.form.txt_GyoushaName.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                                    }
                                }
                                this.form.txt_GyoushaName.ReadOnly = (bool)!gyoushaEntity.SHOKUCHI_KBN;
                                // 20151021 katen #13337 品名手入力に関する機能修正 end

                                // 既存取引先を取得
                                var torihikisakiEntity = this.accessor.GetTorihikisaki(gyoushaEntity.TORIHIKISAKI_CD, tekiyouDate);
                                if (this.form.beforGyousaCD != this.form.txt_GyoushaCD.Text)
                                {
                                    if (null != torihikisakiEntity)
                                    {
                                        this.form.txt_TorihikisakiCD.Text = gyoushaEntity.TORIHIKISAKI_CD;
                                        // 現場から入力されたときは既に取引先チェック行われているため、
                                        // 一度エラーが出たら同じエラーを出さないようにする
                                        if (!this.torihikisakiAndKyotenErrorFlg)
                                        {
                                            // 取引先チェック呼び出し
                                            this.isTransitionFocus = false;
                                            ret = this.form.txt_TorihikisakiCD_AfterUpdate(out catchErr);
                                            if (catchErr) { return ret; }
                                            this.isTransitionFocus = true;
                                        }
                                        this.form.txt_Torihikisaki_shokuchi_kbn.Text = torihikisakiEntity.SHOKUCHI_KBN.ToString();
                                    }
                                    else
                                    {
                                        this.form.txt_TorihikisakiCD.Text = string.Empty;
                                        this.initTorihikisakiAndControl();
                                    }
                                }
                            }
                        }
                        // 引合表示の場合
                        else if (this.form.hikiaiDisplaySwitchingFlg)
                        {
                            var gyoushaEntity_hiki = this.accessor.GetHikiaiGyoushaEntry(inputGyoushaCd);

                            // 業者名
                            // 20151021 katen #13337 品名手入力に関する機能修正 start
                            if (!this.gyoushaNameChangedFlg)
                            {
                                if (gyoushaEntity_hiki.SHOKUCHI_KBN.IsTrue)
                                {
                                    this.form.txt_GyoushaName.Text = gyoushaEntity_hiki.GYOUSHA_NAME1;
                                }
                                else
                                {
                                    this.form.txt_GyoushaName.Text = gyoushaEntity_hiki.GYOUSHA_NAME_RYAKU;
                                }
                            }
                            // 20151021 katen #13337 品名手入力に関する機能修正 end
                            this.form.txt_GyoushaName.ReadOnly = false;
                            if (!gyoushaEntity_hiki.HIKIAI_TORIHIKISAKI_USE_FLG.IsNull)
                            {
                                this.form.txt_Torihikisaki_hikiai_flg.Text = gyoushaEntity_hiki.HIKIAI_TORIHIKISAKI_USE_FLG.Value ? "1" : "0";
                                if (gyoushaEntity_hiki.HIKIAI_TORIHIKISAKI_USE_FLG.Value)
                                {
                                    // 引合取引先を取得
                                    var torihikisakiEntity = this.accessor.GetHikiaiTorihikisakiEntry(gyoushaEntity_hiki.TORIHIKISAKI_CD);
                                    if (this.form.beforGyousaCD != this.form.txt_GyoushaCD.Text)
                                    {
                                        if (null != torihikisakiEntity)
                                        {
                                            this.form.txt_TorihikisakiCD.Text = gyoushaEntity_hiki.TORIHIKISAKI_CD;

                                            // 現場から入力されたときは既に取引先チェック行われているため、
                                            // 一度エラーが出たら同じエラーを出さないようにする
                                            if (!this.torihikisakiAndKyotenErrorFlg)
                                            {
                                                // 取引先チェック呼び出し
                                                this.isTransitionFocus = false;
                                                ret = this.form.txt_TorihikisakiCD_AfterUpdate(out catchErr);
                                                if (catchErr) { return ret; }
                                                this.isTransitionFocus = true;
                                            }

                                            this.form.txt_Torihikisaki_shokuchi_kbn.Text = torihikisakiEntity.SHOKUCHI_KBN.ToString();
                                        }
                                        else
                                        {
                                            this.form.txt_TorihikisakiCD.Text = string.Empty;
                                            this.initTorihikisakiAndControl();
                                        }
                                    }
                                }
                                else
                                {
                                    // 既存取引先を取得
                                    SqlDateTime tekiyouDate = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
                                    DateTime date;
                                    if (!string.IsNullOrWhiteSpace(this.form.dtp_MitsumoriDate.Text) && DateTime.TryParse(this.form.dtp_MitsumoriDate.Text, out date))
                                    {
                                        tekiyouDate = date;
                                    }
                                    var torihikisakiEntity = this.accessor.GetTorihikisaki(gyoushaEntity_hiki.TORIHIKISAKI_CD, tekiyouDate);
                                    if (this.form.beforGyousaCD != this.form.txt_GyoushaCD.Text)
                                    {
                                        if (null != torihikisakiEntity)
                                        {
                                            this.form.txt_TorihikisakiCD.Text = gyoushaEntity_hiki.TORIHIKISAKI_CD;

                                            // 現場から入力されたときは既に取引先チェック行われているため、
                                            // 一度エラーが出たら同じエラーを出さないようにする
                                            if (!this.torihikisakiAndKyotenErrorFlg)
                                            {
                                                // 取引先チェック呼び出し
                                                this.isTransitionFocus = false;
                                                ret = this.form.txt_TorihikisakiCD_AfterUpdate(out catchErr);
                                                if (catchErr) { return ret; }
                                                this.isTransitionFocus = true;
                                            }
                                            this.form.txt_Torihikisaki_shokuchi_kbn.Text = torihikisakiEntity.SHOKUCHI_KBN.ToString();
                                        }
                                        else
                                        {
                                            this.form.txt_TorihikisakiCD.Text = string.Empty;
                                            this.initTorihikisakiAndControl();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // 既存でも引合でもヒットしなかった場合エラー
                        this.form.txt_GyoushaCD.Focus();
                        ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGyousha", ex);
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        /// <summary>
        /// 現場チェック
        /// </summary>
        internal bool CheckGenba(out bool catchErr)
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            catchErr = false;
            var msgLogic = new MessageBoxShowLogic();

            try
            {
                var inputGenbaCd = this.form.txt_GenbaCD.Text;
                var inputGyoshaCd = this.form.txt_GyoushaCD.Text;

                SqlDateTime tekiyouDate = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
                DateTime date;
                if (!string.IsNullOrWhiteSpace(this.form.dtp_MitsumoriDate.Text) && DateTime.TryParse(this.form.dtp_MitsumoriDate.Text, out date))
                {
                    tekiyouDate = date;
                }
                var gyoushaEntity = this.accessor.GetGyousha(this.form.txt_GyoushaCD.Text, tekiyouDate);

                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
                this.genbaTorihiksiakiCd = string.Empty;
                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END

                if (String.IsNullOrEmpty(inputGenbaCd))
                {
                    this.form.txtGenbaName.Text = string.Empty;
                }
                else
                {
                    // 既存を表示するか引合を表示するか切り替えます
                    if (!genbaDisplaySwitching())
                    {
                        ret = false;
                    }
                    else
                    {
                        // 既存表示の場合
                        if (!this.form.hikiaiDisplaySwitchingFlg)
                        {
                            var genbaEntityList = this.accessor.GetGenba(inputGenbaCd, tekiyouDate);
                            if (genbaEntityList == null)
                            {
                                this.form.isInputError = true;
                                return false;
                            }

                            //既存
                            M_GENBA genba = new M_GENBA();
                            foreach (M_GENBA genbaEntity in genbaEntityList)
                            {
                                if (this.form.txt_GyoushaCD.Text.Equals(genbaEntity.GYOUSHA_CD))
                                {
                                    genba = genbaEntity;
                                    if (!this.genbaNameChangedFlg)
                                    {
                                        // 20151021 katen #13337 品名手入力に関する機能修正 start
                                        if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                                        {
                                            this.form.txtGenbaName.Text = genbaEntity.GENBA_NAME1;
                                        }
                                        else
                                        {
                                            this.form.txtGenbaName.Text = genbaEntity.GENBA_NAME_RYAKU;
                                        }
                                        // 20151021 katen #13337 品名手入力に関する機能修正 end
                                    }
                                    this.form.txtGenbaName.ReadOnly = (bool)!genbaEntity.SHOKUCHI_KBN;
                                    break;
                                }
                            }

                            //業者と取引先を取得
                            this.form.txt_Gyousha_hikiai_flg.Text = "0";
                            this.form.txt_Torihikisaki_hikiai_flg.Text = "0";
                            this.CheckGyousha(out catchErr);
                            if (catchErr) { return ret; }

                            // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
                            this.genbaTorihiksiakiCd = genba.TORIHIKISAKI_CD;
                            // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END

                            ret = false;
                        }
                        // 引合表示の場合
                        else if (this.form.hikiaiDisplaySwitchingFlg)
                        {
                            var genbaEntity_hiki = this.accessor.GetHikiaiGenbaEntry(inputGyoshaCd, inputGenbaCd, tekiyouDate, this.form.ManualInputGenbaFlg ? string.Empty : this.form.txt_Gyousha_hikiai_flg.Text);

                            //引合
                            this.form.txt_Gyousha_hikiai_flg.Text = genbaEntity_hiki.HIKIAI_GYOUSHA_USE_FLG.Value ? "1" : "0";
                            if (!genbaEntity_hiki.HIKIAI_TORIHIKISAKI_USE_FLG.IsNull)
                            {
                                this.form.txt_Torihikisaki_hikiai_flg.Text = genbaEntity_hiki.HIKIAI_TORIHIKISAKI_USE_FLG.Value ? "1" : "0";
                            }
                            if (!string.IsNullOrEmpty(genbaEntity_hiki.GENBA_CD))
                            {
                                if (!this.genbaNameChangedFlg)
                                {
                                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                                    if (genbaEntity_hiki.SHOKUCHI_KBN.IsTrue)
                                    {
                                        this.form.txtGenbaName.Text = genbaEntity_hiki.GENBA_NAME1;
                                    }
                                    else
                                    {
                                        this.form.txtGenbaName.Text = genbaEntity_hiki.GENBA_NAME_RYAKU;
                                    }
                                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                                }
                                this.form.txtGenbaName.ReadOnly = false;
                            }
                            this.CheckGyousha(out catchErr);
                            if (catchErr) { return ret; }

                            // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
                            this.genbaTorihiksiakiCd = genbaEntity_hiki.TORIHIKISAKI_CD;
                            // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END

                            ret = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenba", ex);
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
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
        internal bool CheckTorihikisakiAndKyotenCd(M_HIKIAI_TORIHIKISAKI torihikisakiEntity_hiki, M_TORIHIKISAKI torihikisakiEntity, string TorihikisakiCd, out bool catchErr)
        {
            bool returnVal = false;
            catchErr = false;
            try
            {
                //引合
                if (this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("1"))
                {
                    if (string.IsNullOrEmpty(TorihikisakiCd))
                    {
                        // 取引先の入力がない場合はチェック対象外
                        returnVal = true;
                        return returnVal;
                    }

                    if (torihikisakiEntity_hiki == null)
                    {
                        // 取引先マスタを引数の取引先CDで取得しなおす
                        torihikisakiEntity_hiki = this.accessor.GetHikiaiTorihikisakiEntry(TorihikisakiCd);

                        // 取引先の入力がない場合はチェック対象外
                        returnVal = true;
                        return returnVal;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
                        {
                            if (SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text) == torihikisakiEntity_hiki.TORIHIKISAKI_KYOTEN_CD
                                || torihikisakiEntity_hiki.TORIHIKISAKI_KYOTEN_CD.ToString().Equals("99"))
                            {
                                // 入力画面の拠点コードと取引先の拠点コードが等しいか、取引先の拠点コードが99（全社)の場合
                                returnVal = true;
                            }
                            else
                            {
                                // 入力画面の拠点コードと取引先の拠点コードが等しくない場合
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShow("E146");
                                this.torihikisakiAndKyotenErrorFlg = true;
                                this.form.txt_TorihikisakiCD.Focus();
                            }
                        }
                        else
                        {   // 拠点が指定されていない場合
                            returnVal = true;   // No.2865
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(TorihikisakiCd))
                    {
                        // 取引先の入力がない場合はチェック対象外
                        returnVal = true;
                        return returnVal;
                    }

                    if (torihikisakiEntity == null)
                    {
                        // 取引先マスタを引数の取引先CDで取得しなおす
                        SqlDateTime tekiyouDate = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
                        DateTime date;
                        if (!string.IsNullOrWhiteSpace(this.form.dtp_MitsumoriDate.Text) && DateTime.TryParse(this.form.dtp_MitsumoriDate.Text, out date))
                        {
                            tekiyouDate = date;
                        }
                        torihikisakiEntity = this.accessor.GetTorihikisaki(TorihikisakiCd, tekiyouDate);

                        // 取引先の入力がない場合はチェック対象外
                        returnVal = true;
                        return returnVal;
                    }
                    else
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
                                this.torihikisakiAndKyotenErrorFlg = true;
                                this.form.txt_TorihikisakiCD.Focus();
                            }
                        }
                        else
                        {   // 拠点が指定されていない場合
                            returnVal = true;   // No.2865
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTorihikisakiAndKyotenCd", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
                returnVal = false;
            }

            return returnVal;
        }

        #endregion 取引先CD・業者CD・現場CDの関連チェック処理

        // 20140717 syunrei EV005312_最初に業者CDをセットしても、最初に現場CDをセットしても紐づく取引先CDがセットされない。　end

        #region 既存を表示するか引合を表示するか切り替えメソッド

        /// <summary>
        /// 取引先に既存を表示するか引合を表示するか切り替えます
        /// </summary>
        /// <returns>true = 正常  false = エラー</returns>
        internal bool torihikisakiDisplaySwitching()
        {
            bool ret = true;
            var inputTorihikisakiCd = this.form.txt_TorihikisakiCD.Text;

            // 初期値：既存表示に設定
            this.form.hikiaiDisplaySwitchingFlg = false;

            // 手入力の場合はマスタの存在チェックで判断する
            if (this.form.ManualInputFlg)
            {
                // 既存取引先チェック
                SqlDateTime tekiyouDate = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
                DateTime date;
                if (!string.IsNullOrWhiteSpace(this.form.dtp_MitsumoriDate.Text) && DateTime.TryParse(this.form.dtp_MitsumoriDate.Text, out date))
                {
                    tekiyouDate = date;
                }
                var torihikisakiEntity = this.accessor.GetTorihikisaki(inputTorihikisakiCd, tekiyouDate);
                if (null == torihikisakiEntity || this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("1"))
                {
                    // 引合取引先チェック
                    var torihikisakiEntity_hiki = this.accessor.GetHikiaiTorihikisakiEntry(inputTorihikisakiCd);
                    if (null == torihikisakiEntity_hiki)
                    {
                        // 表示切替フラグを既存表示に設定
                        this.form.hikiaiDisplaySwitchingFlg = false;
                        this.form.txt_TorihikisakiCD.Focus();
                        ret = false;
                    }
                    else
                    {
                        // 引合を表示
                        this.form.hikiaiDisplaySwitchingFlg = true;
                    }
                }
                else
                {
                    // 既存を表示
                    this.form.hikiaiDisplaySwitchingFlg = false;
                }
            }
            // ポップアップ入力の場合は引合フラグで判断する
            else if (!this.form.ManualInputFlg)
            {
                if (this.form.txt_Gyousha_hikiai_flg.Text == "1" || this.form.txt_Genba_hikiai_flg.Text == "1")
                {
                    this.form.txt_Torihikisaki_hikiai_flg.Text = "1";
                }

                // 入力が既存だった場合
                if (this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("0"))
                {
                    // 既存を表示
                    this.form.hikiaiDisplaySwitchingFlg = false;
                }
                // 入力が引合だった場合
                else if (this.form.txt_Torihikisaki_hikiai_flg.Text.Equals("1"))
                {
                    // 引合を表示
                    this.form.hikiaiDisplaySwitchingFlg = true;
                }
                else
                {
                    ret = false;
                }
            }
            return ret;
        }

        /// <summary>
        /// 業者に既存を表示するか引合を表示するか切り替えます
        /// </summary>
        /// <returns>true = 正常  false = エラー</returns>
        internal bool gyoushaDisplaySwitching()
        {
            bool ret = true;
            var inputGyoushaCd = this.form.txt_GyoushaCD.Text;

            // 初期値：既存表示に設定
            this.form.hikiaiDisplaySwitchingFlg = false;

            // 手入力の場合はマスタの存在チェックで判断する
            if (this.form.ManualInputFlg)
            {
                // 既存業者チェック
                SqlDateTime tekiyouDate = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
                DateTime date;
                if (!string.IsNullOrWhiteSpace(this.form.dtp_MitsumoriDate.Text) && DateTime.TryParse(this.form.dtp_MitsumoriDate.Text, out date))
                {
                    tekiyouDate = date;
                }
                var gyoushaEntity = this.accessor.GetGyousha(inputGyoushaCd, tekiyouDate);
                if (null == gyoushaEntity || this.form.txt_Gyousha_hikiai_flg.Text.Equals("1"))
                {
                    // 引合業者チェック
                    var gyoushaEntity_hiki = this.accessor.GetHikiaiGyoushaEntry(inputGyoushaCd);
                    if (null == gyoushaEntity_hiki)
                    {
                        // 表示切替フラグを既存表示に設定
                        this.form.hikiaiDisplaySwitchingFlg = false;
                        this.form.txt_GyoushaCD.Focus();
                        ret = false;
                    }
                    else
                    {
                        // 引合を表示
                        this.form.hikiaiDisplaySwitchingFlg = true;
                    }
                }
                else
                {
                    // 既存を表示
                    this.form.hikiaiDisplaySwitchingFlg = false;
                }
            }
            // ポップアップ入力の場合は引合フラグで判断する
            else if (!this.form.ManualInputFlg)
            {
                // 入力が既存だった場合
                if (this.form.txt_Gyousha_hikiai_flg.Text.Equals("0"))
                {
                    // 既存を表示
                    this.form.hikiaiDisplaySwitchingFlg = false;
                }
                // 入力が引合だった場合
                else if (this.form.txt_Gyousha_hikiai_flg.Text.Equals("1"))
                {
                    // 引合を表示
                    this.form.hikiaiDisplaySwitchingFlg = true;
                }
            }
            return ret;
        }

        /// <summary>
        /// 現場に既存を表示するか引合を表示するか切り替えます
        /// </summary>
        /// <returns>true = 正常  false = エラー</returns>
        internal bool genbaDisplaySwitching()
        {
            bool ret = true;
            var inputGyoushaCd = this.form.txt_GyoushaCD.Text;
            var inputGenbaCd = this.form.txt_GenbaCD.Text;

            // 初期値：既存表示に設定
            this.form.hikiaiDisplaySwitchingFlg = false;

            // 手入力の場合はマスタの存在チェックで判断する
            if (this.form.ManualInputFlg)
            {
                // 既存現場チェック
                SqlDateTime tekiyouDate = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
                DateTime date;
                if (!string.IsNullOrWhiteSpace(this.form.dtp_MitsumoriDate.Text) && DateTime.TryParse(this.form.dtp_MitsumoriDate.Text, out date))
                {
                    tekiyouDate = date;
                }
                var genbaEntity = this.accessor.GetGenba(inputGyoushaCd, inputGenbaCd, tekiyouDate);
                if (null == genbaEntity || this.form.txt_Gyousha_hikiai_flg.Text.Equals("1"))
                {
                    // 引合現場チェック
                    var genbaEntity_hiki = this.accessor.GetHikiaiGenbaEntry(inputGyoushaCd, inputGenbaCd, tekiyouDate, this.form.ManualInputGenbaFlg ? string.Empty : this.form.txt_Gyousha_hikiai_flg.Text);
                    if (null == genbaEntity_hiki)
                    {
                        // 表示切替フラグを既存表示に設定
                        this.form.hikiaiDisplaySwitchingFlg = false;
                        // 業者CDを入力してくださいのエラー
                        this.gyousyaGenbaErrorKbn = 1;
                        ret = false;
                    }
                    else
                    {
                        // 引合を表示
                        this.form.hikiaiDisplaySwitchingFlg = true;
                    }
                }
                else
                {
                    // 既存を表示
                    this.form.hikiaiDisplaySwitchingFlg = false;
                }
            }
            // ポップアップ入力の場合は引合フラグで判断する
            else if (!this.form.ManualInputFlg)
            {
                // 入力が既存だった場合
                if (this.form.txt_Genba_hikiai_flg.Text.Equals("0"))
                {
                    // 既存を表示
                    this.form.hikiaiDisplaySwitchingFlg = false;
                }
                // 入力が引合だった場合
                else if (this.form.txt_Genba_hikiai_flg.Text.Equals("1"))
                {
                    // 引合を表示
                    this.form.hikiaiDisplaySwitchingFlg = true;

                    this.form.txt_Gyousha_hikiai_flg.Text = "1";
                    this.form.txt_Torihikisaki_hikiai_flg.Text = "1";
                }
            }
            return ret;
        }

        #endregion 既存を表示するか引合を表示するか切り替えメソッド

        /// <summary>
        /// 見積書種類チェック
        /// </summary>
        internal bool MitsumoriSyuruiCheck()
        {
            bool ret = true;
            try
            {
                // 見積書種類CDをformParem.cyohyoTypeに変換
                this.form.formParem.cyohyoType = this.ConvertCyohyoType(this.form.MITSUMORISYURUI_CD.Text);

                // 全てのタブに対して変更をかける
                for (int page = 0; page < this.form.tbc_MitsumoriMeisai.TabPages.Count; page++)
                {
                    // 明細欄のプロパティを変更
                    this.ChangeThePropertiesOfTheDetail(page);
                }

                // 見積書種類CDによって見積書種類名称を設定
                this.ChangeMitsumoriSyuruiName();
            }
            catch (Exception ex)
            {
                LogUtility.Error("MitsumoriSyuruiCheck", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 見積書種類CDをformParem.cyohyoTypeに変換
        /// </summary>
        /// <param name="mitsumoriSyuruiCd">見積書CD</param>
        /// <returns>見積書CD   →   formParem.cyohyoType
        ///                １   →   １１
        ///                ２   →   １２
        ///                ３   →   ２１
        ///                ４   →   ２２
        /// </returns>
        internal int ConvertCyohyoType(string mitsumoriSyuruiCd)
        {
            // 初期化
            int ret = 0;

            if (!string.IsNullOrEmpty(mitsumoriSyuruiCd))
            {
                if (this.form.formParem != null)
                {
                    // 見積書種類名称の設定
                    switch (int.Parse(mitsumoriSyuruiCd))
                    {
                        case MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE_INDEX: // 見積書（縦）
                            ret = MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE;
                            break;

                        case MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO_INDEX: // 見積書（横）
                            ret = MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO;
                            break;

                        case MitumorisyoConst.MITUMOTISYO_TANKA_TATE_INDEX:   // 単価見積書（縦）
                            ret = MitumorisyoConst.MITUMOTISYO_TANKA_TATE;
                            break;

                        case MitumorisyoConst.MITUMOTISYO_TANKA_YOKO_INDEX:   // 単価見積書（横）
                            ret = MitumorisyoConst.MITUMOTISYO_TANKA_YOKO;
                            break;

                        default:
                            break;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 明細欄のプロパティを変更します
        /// </summary>
        /// <param name="tabIndex">対象タブのインデックス</param>
        internal void ChangeThePropertiesOfTheDetail(int tabIndex)
        {
            if (this.form.formParem != null)
            {
                CustomDataGridView cdgv = ((CustomDataGridView)this.form.tbc_MitsumoriMeisai.TabPages[tabIndex].Controls["control"].Controls["CustomDataGridView"]);
                string bikou = "clm_MeisaiBikou1";

                // 見積書種類
                switch (this.form.formParem.cyohyoType)
                {
                    // 見積書（縦）
                    case MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE:
                        // 備考カラムの表示幅を変更
                        cdgv.Columns[bikou].Width = 100;
                        break;

                    // 見積書（横）
                    case MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO:
                        // 備考カラムの表示幅を変更
                        cdgv.Columns[bikou].Width = 200;
                        break;

                    // 単価見積書（縦）
                    case MitumorisyoConst.MITUMOTISYO_TANKA_TATE:
                        // 備考カラムの表示幅を変更
                        cdgv.Columns[bikou].Width = 100;
                        break;

                    // 単価見積書（横）
                    case MitumorisyoConst.MITUMOTISYO_TANKA_YOKO:
                        // 備考カラムの表示幅を変更
                        cdgv.Columns[bikou].Width = 200;
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 見積書種類CDによって見積書種類名称を設定します
        /// </summary>
        internal void ChangeMitsumoriSyuruiName()
        {
            if (this.form.formParem != null)
            {
                // 見積書種類名称の設定
                switch (this.form.formParem.cyohyoType)
                {
                    case MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE: // 見積書（縦）
                        this.form.MITSUMORISYURUI_NAME.Text = MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE_NAME;
                        this.form.MITSUMORISYURUI_CD.Text = MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE_INDEX.ToString();
                        break;

                    case MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO: // 見積書（横）
                        this.form.MITSUMORISYURUI_NAME.Text = MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO_NAME;
                        this.form.MITSUMORISYURUI_CD.Text = MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO_INDEX.ToString();
                        break;

                    case MitumorisyoConst.MITUMOTISYO_TANKA_TATE:   // 単価見積書（縦）
                        this.form.MITSUMORISYURUI_NAME.Text = MitumorisyoConst.MITUMOTISYO_TANKA_TATE_NAME;
                        this.form.MITSUMORISYURUI_CD.Text = MitumorisyoConst.MITUMOTISYO_TANKA_TATE_INDEX.ToString();
                        break;

                    case MitumorisyoConst.MITUMOTISYO_TANKA_YOKO:   // 単価見積書（横）
                        this.form.MITSUMORISYURUI_NAME.Text = MitumorisyoConst.MITUMOTISYO_TANKA_YOKO_NAME;
                        this.form.MITSUMORISYURUI_CD.Text = MitumorisyoConst.MITUMOTISYO_TANKA_YOKO_INDEX.ToString();
                        break;

                    default:
                        this.form.MITSUMORISYURUI_NAME.Text = string.Empty;
                        break;
                }
            }
        }

        #region 見積書種類 ポップアップ初期化

        /// <summary>
        /// 見積書種類 ポップアップ初期化
        /// </summary>
        public void MitsumoriSyuruiPopUpDataInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 表示用データ設定
                DataTable mitsumoriSyuruiDataTable = new DataTable();

                // データ（列）
                mitsumoriSyuruiDataTable.Columns.Add("MITSUMORISYURUI_CD", Type.GetType("System.String"));
                mitsumoriSyuruiDataTable.Columns.Add("MITSUMORISYURUI_NAME", Type.GetType("System.String"));
                // データ（行）
                mitsumoriSyuruiDataTable.Rows.Add(MitumorisyoConst.MITUMOTISYO_TANKA_YOKO_INDEX.ToString(), MitumorisyoConst.MITUMOTISYO_TANKA_YOKO_NAME);
                mitsumoriSyuruiDataTable.Rows.Add(MitumorisyoConst.MITUMOTISYO_TANKA_TATE_INDEX.ToString(), MitumorisyoConst.MITUMOTISYO_TANKA_TATE_NAME);
                mitsumoriSyuruiDataTable.Rows.Add(MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO_INDEX.ToString(), MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO_NAME);
                mitsumoriSyuruiDataTable.Rows.Add(MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE_INDEX.ToString(), MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE_NAME);

                // TableNameを設定すれば、ポップアップのタイトルになる
                mitsumoriSyuruiDataTable.TableName = "見積書種類選択";

                // 列名とデータソース設定
                this.form.MITSUMORISYURUI_CD.PopupDataHeaderTitle = new string[] { "見積書種類CD", "見積書種類名" };
                this.form.MITSUMORISYURUI_CD.PopupDataSource = mitsumoriSyuruiDataTable;
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

        #endregion 見積書種類 ポップアップ初期化

        /// <summary>
        /// SYS_INFOを取得する
        /// </summary>
        /// <returns></returns>
        public M_SYS_INFO GetSysInfo()
        {
            M_SYS_INFO[] returnEntity = sysInfoDao.GetAllData();
            return returnEntity[0];
        }

        /// <summary>
        /// 取引先もしくは引合取引先の代表印字区分を取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns>
        /// 代表印字区分
        /// true = 印字する  false = 印字しない
        /// </returns>
        private bool getDaihyouPrintKbn(string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(torihikisakiCd);

            // 取引先引合フラグ取得
            var torihikisakiHikiaiFlg = this.form.txt_Torihikisaki_hikiai_flg.Text;
            bool isDaihyouPrint = true;

            // 引合取引先
            if (torihikisakiHikiaiFlg == "1")
            {
                var hikiaiTorihikisakiEntry = new M_HIKIAI_TORIHIKISAKI();

                hikiaiTorihikisakiEntry = this.accessor.GetHikiaiTorihikisakiEntry(torihikisakiCd);
                if (hikiaiTorihikisakiEntry != null)
                {
                    isDaihyouPrint = hikiaiTorihikisakiEntry.DAIHYOU_PRINT_KBN == 1 ? true : false;
                }
            }
            // 既存取引先
            else if (torihikisakiHikiaiFlg == "0")
            {
                var torihikisakiEntry = new M_TORIHIKISAKI();

                SqlDateTime tekiyouDate = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
                DateTime date;
                if (!string.IsNullOrWhiteSpace(this.form.dtp_MitsumoriDate.Text) && DateTime.TryParse(this.form.dtp_MitsumoriDate.Text, out date))
                {
                    tekiyouDate = date;
                }
                torihikisakiEntry = this.accessor.GetTorihikisaki(torihikisakiCd, tekiyouDate);
                if (torihikisakiEntry != null)
                {
                    isDaihyouPrint = torihikisakiEntry.DAIHYOU_PRINT_KBN == 1 ? true : false;
                }
            }
            LogUtility.DebugMethodEnd(isDaihyouPrint);
            return isDaihyouPrint;
        }

        // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
        /// <summary>
        /// 現場より取引先情報を設定する
        /// </summary>
        internal void SetTorihiksiakiInfoByGenba(out bool catchErr)
        {
            //if (string.IsNullOrEmpty(this.genbaTorihiksiakiCd)) { return; }
            catchErr = false;
            try
            {
                SqlDateTime tekiyouDate = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
                DateTime date;
                if (!string.IsNullOrWhiteSpace(this.form.dtp_MitsumoriDate.Text) && DateTime.TryParse(this.form.dtp_MitsumoriDate.Text, out date))
                {
                    tekiyouDate = date;
                }
                var torihikisakiEntity = this.accessor.GetTorihikisaki(this.genbaTorihiksiakiCd, tekiyouDate);
                var hikiaitorihikisakiEntity = this.accessor.GetHikiaiTorihikisakiEntry(this.genbaTorihiksiakiCd);
                if (this.form.beforGyousaCD != this.form.txt_GyoushaCD.Text || this.form.beforeGenbaCD != this.form.txt_GenbaCD.Text
                    || this.form.beforeGenbaHikiai != this.form.txt_Genba_hikiai_flg.Text)
                {
                    if (null != torihikisakiEntity || null != hikiaitorihikisakiEntity)
                    {
                        this.form.txt_TorihikisakiCD.Text = this.genbaTorihiksiakiCd;
                        // 現場から入力されたときは既に取引先チェック行われているため、
                        // 一度エラーが出たら同じエラーを出さないようにする
                        if (!this.torihikisakiAndKyotenErrorFlg)
                        {
                            // 取引先チェック呼び出し
                            this.isTransitionFocus = false;
                            this.form.txt_TorihikisakiCD_AfterUpdate(out catchErr);
                            if (catchErr) { return; }
                            this.isTransitionFocus = true;
                        }
                        this.form.txt_Torihikisaki_shokuchi_kbn.Text = torihikisakiEntity == null ? hikiaitorihikisakiEntity.SHOKUCHI_KBN.ToString() : torihikisakiEntity.SHOKUCHI_KBN.ToString();
                    }
                }

                if (this.form.txt_TorihikisakiCD.Text != this.genbaTorihiksiakiCd && this.form.beforeGenbaHikiai == "1")
                {
                    this.form.txt_TorihikisakiCD.Text = this.genbaTorihiksiakiCd;
                    this.form.txt_Torihikisaki_hikiai_flg.Text = "1";

                    if (!this.torihikisakiAndKyotenErrorFlg)
                    {
                        // 取引先チェック呼び出し
                        this.isTransitionFocus = false;
                        this.form.txt_TorihikisakiCD_AfterUpdate(out catchErr);
                        if (catchErr) { return; }
                        this.isTransitionFocus = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTorihiksiakiInfoByGenba", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
        }

        // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END

        #region 初期取引先

        /// <summary>
        /// 初期取引先
        /// </summary>
        /// <param name="torihikisakiCode"></param>
        public bool initTorihikisaki(string torihikisakiCode)
        {
            LogUtility.DebugMethodStart(torihikisakiCode);

            // 初期化
            this.dto.torihikisakiEntry = null;
            this.dto.torihikisakiSeikyuuEntity = null;
            this.dto.torihikisakiShiharaiEntity = null;

            // 処理しない
            if (string.IsNullOrEmpty(torihikisakiCode))
            {
                return true;
            }

            torihikisakiCode = torihikisakiCode.PadLeft(6, '0');

            // 取引先マスタ取得
            this.dto.torihikisakiEntry = this.accessor.torihikisakiDao.GetDataByCd(torihikisakiCode);
            if (this.dto.torihikisakiEntry == null)
            {
                // 処理モード
                HIKIAI_TORIHIKISAKI_WindowType = HIKIAI_TORIHIKISAKI.NONE;
                // 再設定
                this.form.txt_Torihikisaki_hikiai_flg.Text = String.Empty;
                this.form.txt_Torihikisaki_shokuchi_kbn.Text = String.Empty;

                // 取引先タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Torihikisaki.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "取引先");
                this.form.isInputError = true;
                this.form.txt_TorihikisakiCD.IsInputErrorOccured = true;
                this.form.txt_TorihikisakiCD.Focus();
                this.form.beforTorihikisakiCD = this.form.txt_TorihikisakiCD.Text;

                return false;
            }

            // windowType設定
            if (!this.dto.torihikisakiEntry.SHOKUCHI_KBN.IsNull && (Boolean)this.dto.torihikisakiEntry.SHOKUCHI_KBN)
            {
                // 処理モード
                HIKIAI_TORIHIKISAKI_WindowType = HIKIAI_TORIHIKISAKI.NEW_WINDOW_FLAG;

                // 再設定
                this.form.txt_Torihikisaki_hikiai_flg.Text = "0";
                this.form.txt_Torihikisaki_shokuchi_kbn.Text = "True";
            }
            else if (!this.dto.torihikisakiEntry.SHOKUCHI_KBN.IsNull && !(Boolean)this.dto.torihikisakiEntry.SHOKUCHI_KBN)
            {
                // 処理モード
                HIKIAI_TORIHIKISAKI_WindowType = HIKIAI_TORIHIKISAKI.REFERENCE_WINDOW_FLAG;

                // 再設定
                this.form.txt_Torihikisaki_hikiai_flg.Text = "0";
                this.form.txt_Torihikisaki_shokuchi_kbn.Text = "False";
            }

            // 取引先請求
            this.dto.torihikisakiSeikyuuEntity = this.accessor.GetTorihikisakiSeikyuu(torihikisakiCode);

            // 取引先支払
            this.dto.torihikisakiShiharaiEntity = this.accessor.GetTorihikisakiShiharai(torihikisakiCode);

            LogUtility.DebugMethodEnd();

            return true;
        }

        #endregion 初期取引先

        #region 初期引合取引先

        /// <summary>
        /// 初期引合取引先
        /// </summary>
        /// <param name="hikiaiTorihikisakiCode"></param>
        public bool initHikiaiTorihikisaki(string hikiaiTorihikisakiCode)
        {
            LogUtility.DebugMethodStart(hikiaiTorihikisakiCode);

            // 初期化
            this.dto.hikiaiTorihikisakiEntry = null;
            this.beforDto.hikiaiTorihikisakiEntry = null;

            // 処理しない
            if (string.IsNullOrEmpty(hikiaiTorihikisakiCode))
            {
                return true;
            }

            hikiaiTorihikisakiCode = hikiaiTorihikisakiCode.PadLeft(6, '0');

            // 引合取引先マスタ検索
            this.dto.hikiaiTorihikisakiEntry = this.accessor.hikiaiTorihikisakiDao.GetDataByCd(hikiaiTorihikisakiCode);

            // 更新前データを保持
            this.beforDto.hikiaiTorihikisakiEntry = this.dto.hikiaiTorihikisakiEntry;

            if (this.dto.hikiaiTorihikisakiEntry == null)
            {
                // 処理モード
                HIKIAI_TORIHIKISAKI_WindowType = HIKIAI_TORIHIKISAKI.NONE;
                // 再設定
                this.form.txt_Torihikisaki_hikiai_flg.Text = String.Empty;
                this.form.txt_Torihikisaki_shokuchi_kbn.Text = String.Empty;

                // 取引先タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Torihikisaki.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "取引先");
                this.form.isInputError = true;
                this.form.txt_TorihikisakiCD.IsInputErrorOccured = true;
                this.form.txt_TorihikisakiCD.Focus();
                this.form.beforTorihikisakiCD = this.form.txt_TorihikisakiCD.Text;

                return false;
            }

            // 処理モード
            HIKIAI_TORIHIKISAKI_WindowType = HIKIAI_TORIHIKISAKI.UPDATE_WINDOW_FLAG;

            // windowType設定
            if (!this.dto.hikiaiTorihikisakiEntry.SHOKUCHI_KBN.IsNull)
            {
                // 再設定
                this.form.txt_Torihikisaki_hikiai_flg.Text = "1";
                this.form.txt_Torihikisaki_shokuchi_kbn.Text = ((Boolean)this.dto.hikiaiTorihikisakiEntry.SHOKUCHI_KBN).ToString();
            }
            else
            {
                // 再設定
                this.form.txt_Torihikisaki_hikiai_flg.Text = "1";
                this.form.txt_Torihikisaki_shokuchi_kbn.Text = String.Empty;
            }

            this.dto.hikiaiTorihikisakiSeikyuuEntity = this.accessor.GetHikiaiTorihikisakiSeikyuuEntry(hikiaiTorihikisakiCode);
            this.dto.hikiaiTorihikisakiShiharaiEntity = this.accessor.GetHikiaiTorihikisakiShiharaiEntry(hikiaiTorihikisakiCode);

            LogUtility.DebugMethodEnd(true);

            return true;
        }

        /// <summary>
        /// 取引先初期化
        /// </summary>
        private void initTorihikisakiAndControl()
        {
            this.initTorihikisakiTab();
            this.form.txt_Torihikisaki_hikiai_flg.Text = String.Empty;
            this.form.txt_Torihikisaki_shokuchi_kbn.Text = String.Empty;
            // 引合業者チェックを外す
            this.form.chk_HikiaiTorihikisakiFlg.Checked = false;

            // 取引先タブのコントロールを使用不可
            foreach (System.Windows.Forms.Control ctl in this.form.tap_Torihikisaki.Controls)
            {
                if (ctl is System.Windows.Forms.Label)
                {
                    continue;
                }
                ctl.Enabled = false;
            }

            // 処理モード
            HIKIAI_TORIHIKISAKI_WindowType = HIKIAI_TORIHIKISAKI.NONE;
        }

        #endregion 初期引合取引先

        #region 初期業者

        /// <summary>
        /// 初期業者
        /// </summary>
        /// <param name="gyoushaCode"></param>
        public bool initGyousha(string gyoushaCode)
        {
            LogUtility.DebugMethodStart(gyoushaCode);

            // 初期化
            this.dto.gyoushaEntry = null;

            // 処理しない
            if (string.IsNullOrEmpty(gyoushaCode))
            {
                return true;
            }

            gyoushaCode = gyoushaCode.PadLeft(6, '0');

            // 業者マスタ取得
            this.dto.gyoushaEntry = this.accessor.gyoushaDao.GetDataByCd(gyoushaCode);

            if (this.dto.gyoushaEntry == null)
            {
                // 処理モード
                HIKIAI_GYOUSHA_WindowType = HIKIAI_GYOUSHA.NONE;
                // 再設定
                this.form.txt_Gyousha_hikiai_flg.Text = String.Empty;
                this.form.txt_Gyousha_shokuchi_kbn.Text = String.Empty;

                // 業者タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Gyousya.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                this.form.txt_GyoushaCD.IsInputErrorOccured = true;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "業者");
                this.form.isInputError = true;
                this.form.txt_GyoushaCD.Focus();

                return false;
            }
            this.form.txt_GyoushaCD.IsInputErrorOccured = false;

            // windowType設定
            if (!this.dto.gyoushaEntry.SHOKUCHI_KBN.IsNull && (Boolean)this.dto.gyoushaEntry.SHOKUCHI_KBN)
            {
                // 処理モード
                HIKIAI_GYOUSHA_WindowType = HIKIAI_GYOUSHA.NEW_WINDOW_FLAG;

                // 再設定
                this.form.txt_Gyousha_hikiai_flg.Text = "0";
                this.form.txt_Gyousha_shokuchi_kbn.Text = "True";
            }
            else if (!this.dto.gyoushaEntry.SHOKUCHI_KBN.IsNull && !(Boolean)this.dto.gyoushaEntry.SHOKUCHI_KBN)
            {
                // 処理モード
                HIKIAI_GYOUSHA_WindowType = HIKIAI_GYOUSHA.REFERENCE_WINDOW_FLAG;

                // 再設定
                this.form.txt_Gyousha_hikiai_flg.Text = "0";
                this.form.txt_Gyousha_shokuchi_kbn.Text = "False";
            }

            LogUtility.DebugMethodEnd();

            return true;
        }

        #endregion 初期業者

        #region 初期引合業者

        /// <summary>
        /// 初期引合業者
        /// </summary>
        /// <param name="gyoushaCode"></param>
        public bool initHikiaiGyousha(string gyoushaCode)
        {
            LogUtility.DebugMethodStart(gyoushaCode);

            // 初期化
            this.dto.hikiaiGyoushaEntry = null;
            this.beforDto.hikiaiGyoushaEntry = null;

            // 処理しない
            if (string.IsNullOrEmpty(gyoushaCode))
            {
                // 処理モード
                HIKIAI_GYOUSHA_WindowType = HIKIAI_GYOUSHA.NONE;
                // 再設定
                this.form.txt_Gyousha_hikiai_flg.Text = String.Empty;
                this.form.txt_Gyousha_shokuchi_kbn.Text = String.Empty;

                // 業者タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Gyousya.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "業者");
                this.form.isInputError = true;
                this.form.txt_GyoushaCD.IsInputErrorOccured = true;
                this.form.txt_GyoushaCD.Focus();

                return false;
            }

            gyoushaCode = gyoushaCode.PadLeft(6, '0');

            // 引合業者マスタ検索
            this.dto.hikiaiGyoushaEntry = this.accessor.hikiaiGyoushaDao.GetDataByCd(gyoushaCode);

            // 更新前データを保持
            this.beforDto.hikiaiGyoushaEntry = this.dto.hikiaiGyoushaEntry;

            if (this.dto.hikiaiGyoushaEntry == null)
            {
                // 処理モード
                HIKIAI_GYOUSHA_WindowType = HIKIAI_GYOUSHA.NONE;
                // 再設定
                this.form.txt_Gyousha_hikiai_flg.Text = String.Empty;
                this.form.txt_Gyousha_shokuchi_kbn.Text = String.Empty;

                // 業者タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Gyousya.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                return false;
            }

            // 処理モード
            HIKIAI_GYOUSHA_WindowType = HIKIAI_GYOUSHA.UPDATE_WINDOW_FLAG;

            // windowType設定
            if (!this.dto.hikiaiGyoushaEntry.SHOKUCHI_KBN.IsNull)
            {
                // 再設定
                this.form.txt_Gyousha_hikiai_flg.Text = "1";
                this.form.txt_Gyousha_shokuchi_kbn.Text = ((Boolean)this.dto.hikiaiGyoushaEntry.SHOKUCHI_KBN).ToString();
            }
            else
            {
                // 再設定
                this.form.txt_Gyousha_hikiai_flg.Text = "1";
                this.form.txt_Gyousha_shokuchi_kbn.Text = String.Empty;
            }

            LogUtility.DebugMethodEnd();

            return true;
        }

        #endregion 初期引合業者

        #region 初期現場

        /// <summary>
        /// 初期現場
        /// </summary>
        /// <param name="gyoushaCD"></param>
        /// <param name="genbaCD"></param>
        public bool initGenba(string gyoushaCD, string genbaCD)
        {
            LogUtility.DebugMethodStart(gyoushaCD, genbaCD);

            // 初期化
            this.dto.genbaEntry = null;

            gyoushaCD = gyoushaCD.PadLeft(6, '0');
            genbaCD = genbaCD.PadLeft(6, '0');

            // 処理しない
            if (string.IsNullOrEmpty(gyoushaCD))
            {
                return true;
            }
            // 処理しない
            if (string.IsNullOrEmpty(genbaCD))
            {
                return true;
            }

            // 現場マスタ取得
            M_GENBA data = new M_GENBA();
            data.GYOUSHA_CD = gyoushaCD;
            data.GENBA_CD = genbaCD;
            this.dto.genbaEntry = this.accessor.genbaDao.GetDataByCd(data);

            if (this.dto.genbaEntry == null)
            {
                // 処理モード
                HIKIAI_GENBA_WindowType = HIKIAI_GENBA.NONE;

                // 再設定
                this.form.txt_Genba_hikiai_flg.Text = String.Empty;
                this.form.txt_Genba_shokuchi_kbn.Text = String.Empty;

                // 現場タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Genba.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                this.form.txt_GenbaCD.IsInputErrorOccured = true;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "現場");
                this.form.isInputError = true;
                this.form.txt_GenbaCD.Focus();

                return false;
            }
            this.form.txt_GenbaCD.IsInputErrorOccured = false;

            // windowType設定
            if (!this.dto.genbaEntry.SHOKUCHI_KBN.IsNull && (Boolean)this.dto.genbaEntry.SHOKUCHI_KBN)
            {
                // 処理モード
                HIKIAI_GENBA_WindowType = HIKIAI_GENBA.NEW_WINDOW_FLAG;

                // 再設定
                this.form.txt_Genba_hikiai_flg.Text = "0";
                this.form.txt_Genba_shokuchi_kbn.Text = "True";
            }
            else if (!this.dto.genbaEntry.SHOKUCHI_KBN.IsNull && !(Boolean)this.dto.genbaEntry.SHOKUCHI_KBN)
            {
                // 処理モード
                HIKIAI_GENBA_WindowType = HIKIAI_GENBA.REFERENCE_WINDOW_FLAG;

                // 再設定
                this.form.txt_Genba_hikiai_flg.Text = "0";
                this.form.txt_Genba_shokuchi_kbn.Text = "False";
            }

            LogUtility.DebugMethodEnd();

            return true;
        }

        #endregion 初期現場

        #region 初期引合現場

        /// <summary>
        /// 初期引合現場
        /// </summary>
        /// <param name="gyoushaCD"></param>
        /// <param name="genbaCD"></param>
        public bool initHikiaiGenba(string gyoushaCD, string genbaCD)
        {
            LogUtility.DebugMethodStart(gyoushaCD, genbaCD);

            // 初期化
            this.dto.hikiaiGenbaEntry = null;
            beforDto.hikiaiGenbaEntry = null;

            // 処理しない
            if (string.IsNullOrEmpty(gyoushaCD))
            {
                return true;
            }
            // 処理しない
            if (string.IsNullOrEmpty(genbaCD))
            {
                return true;
            }

            gyoushaCD = gyoushaCD.PadLeft(6, '0');
            genbaCD = genbaCD.PadLeft(6, '0');

            // 引合現場マスタ検索
            M_HIKIAI_GENBA data = new M_HIKIAI_GENBA();
            data.GYOUSHA_CD = gyoushaCD;
            data.GENBA_CD = genbaCD;
            this.dto.hikiaiGenbaEntry = this.accessor.hikiaiGenbaDao.GetDataByCd(data);

            // 更新前データを保持
            beforDto.hikiaiGenbaEntry = this.dto.hikiaiGenbaEntry;

            if (this.dto.hikiaiGenbaEntry == null)
            {
                // 処理モード
                HIKIAI_GENBA_WindowType = HIKIAI_GENBA.NONE;

                // 再設定
                this.form.txt_Genba_hikiai_flg.Text = String.Empty;
                this.form.txt_Genba_shokuchi_kbn.Text = String.Empty;

                // 現場タブのコントロールを使用不可
                foreach (System.Windows.Forms.Control ctl in this.form.tap_Genba.Controls)
                {
                    if (ctl is System.Windows.Forms.Label)
                    {
                        continue;
                    }
                    ctl.Enabled = false;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "現場");
                this.form.isInputError = true;
                this.form.txt_GenbaCD.IsInputErrorOccured = true;
                this.form.txt_GenbaCD.Focus();
                this.form.beforeGenbaCD = this.form.txt_GenbaCD.Text;

                return false;
            }

            // 処理モード
            HIKIAI_GENBA_WindowType = HIKIAI_GENBA.UPDATE_WINDOW_FLAG;

            // windowType設定
            if (!this.dto.hikiaiGenbaEntry.SHOKUCHI_KBN.IsNull)
            {
                // 再設定
                this.form.txt_Genba_hikiai_flg.Text = "1";
                this.form.txt_Genba_shokuchi_kbn.Text = ((Boolean)this.dto.hikiaiGenbaEntry.SHOKUCHI_KBN).ToString();
            }
            else
            {
                // 再設定
                this.form.txt_Genba_hikiai_flg.Text = "1";
                this.form.txt_Genba_shokuchi_kbn.Text = String.Empty;
            }

            LogUtility.DebugMethodEnd();

            return true;
        }

        #endregion 初期引合現場

        /// <summary>
        /// 引合取引先の取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="catchErr"></param>
        /// <returns></returns>
        internal M_HIKIAI_TORIHIKISAKI GetHikiaiTorihikisakiEntry(string torihikisakiCd, out bool catchErr)
        {
            M_HIKIAI_TORIHIKISAKI ret = null;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(torihikisakiCd);
                ret = this.accessor.GetHikiaiTorihikisakiEntry(torihikisakiCd);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetHikiaiTorihikisakiEntry", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        /// <summary>
        /// 取引先取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="mitsumoriDate"></param>
        /// <param name="catchErr"></param>
        /// <returns></returns>
        internal M_TORIHIKISAKI GetTorihikisaki(string torihikisakiCd, SqlDateTime mitsumoriDate, out bool catchErr)
        {
            M_TORIHIKISAKI ret = null;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(torihikisakiCd, mitsumoriDate);
                ret = this.accessor.GetTorihikisaki(torihikisakiCd, mitsumoriDate);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisaki", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        //20250411
        public bool BikoKbnCdValidating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                string bikoCd = this.form.BIKO_KBN_CD.Text;

                if (!beforeBikoCd.Equals(bikoCd))
                {
                    DialogResult result = msgLogic.MessageBoxShowConfirm("備考パターンを変更します。備考CDは上書きされますがよろしいですか。", MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.No)
                    {
                        this.form.BIKO_KBN_CD.Text = beforeBikoCd;
                        this.form.BIKO_NAME_RYAKU.Text = beforeBikoName;

                        LogUtility.DebugMethodEnd(true);
                        return true;
                    }

                    if (!string.IsNullOrEmpty(bikoCd))
                    {
                        M_BIKO_UCHIWAKE_NYURYOKU r = new M_BIKO_UCHIWAKE_NYURYOKU();
                        r.BIKO_KBN_CD = this.form.BIKO_KBN_CD.Text;
                        M_BIKO_UCHIWAKE_NYURYOKU[] biko = this.bikoUchiwakeDao.GetAllValidData(r);

                        if (biko != null && biko.Length > 0)
                        {
                            this.form.BIKO_NAME_RYAKU.Text = biko[0].BIKO_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.BIKO_NAME_RYAKU.Text = string.Empty;
                            this.form.BIKO_KBN_CD.IsInputErrorOccured = true;
                            this.form.BIKO_KBN_CD.UpdateBackColor(false);
                            msgLogic.MessageBoxShowError("備考パターンCDマスタに存在しないコードが入力されました。");
                            e.Cancel = true;
                            this.form.Ichiran.Rows.Clear();
                            return true;
                        }
                    }
                    else
                    {
                        this.form.BIKO_KBN_CD.IsInputErrorOccured = false;
                        this.form.BIKO_KBN_CD.UpdateBackColor(false);
                        this.form.BIKO_NAME_RYAKU.Text = string.Empty;
                        this.form.Ichiran.Rows.Clear();
                    }
                }
                else
                {
                    this.form.BIKO_NAME_RYAKU.Text = beforeBikoName;
                }

                this.beforeBikoCd = bikoCd;
                this.beforeBikoName = this.form.BIKO_NAME_RYAKU.Text;

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("BikoKBNCDValidating ", ex);
                this.errmessage.MessageBoxShow("E245");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        public bool BikoKBNCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var ichiran = this.form.Ichiran;

                if (!string.IsNullOrEmpty(this.form.BIKO_KBN_CD.Text))
                {
                    ichiran.Rows.Clear();

                    M_BIKO_UCHIWAKE_NYURYOKU r = new M_BIKO_UCHIWAKE_NYURYOKU();
                    r.BIKO_KBN_CD = this.form.BIKO_KBN_CD.Text;
                    M_BIKO_UCHIWAKE_NYURYOKU[] bikoUchi = this.bikoUchiwakeDao.GetAllValidData(r);

                    if (bikoUchi != null && bikoUchi.Length > 0)
                    {
                        foreach (var biko in bikoUchi)
                        {
                            int index = ichiran.Rows.Add();
                            ichiran.Rows[index].Cells["BIKO_CD"].Value = biko.BIKO_CD;
                            ichiran.Rows[index].Cells["BIKO_NOTE"].Value = biko.BIKO_NOTE;
                        }

                        ichiran.CurrentCell = ichiran.Rows[0].Cells["BIKO_CD"];
                    }
                    else
                    {
                        ichiran.Rows.Clear();
                    }
                }
                else
                {
                    ichiran.Rows.Clear();
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("BikoKBNCdValidated ", ex);
                this.errmessage.MessageBoxShow("E245");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        //public bool DgvBikoCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);

        //        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        //        var cell = this.form.dgvBiko.CurrentCell;

        //        if (cell.FormattedValue != null && !string.IsNullOrEmpty(cell.FormattedValue.ToString()))
        //        {
        //            M_BIKO_SENTAKUSHI_NYURYOKU r = new M_BIKO_SENTAKUSHI_NYURYOKU();
        //            r.BIKO_CD = cell.FormattedValue.ToString();
        //            M_BIKO_SENTAKUSHI_NYURYOKU[] bikoSenta = this.bikoSentaDao.GetAllValidData(r);

        //            if (bikoSenta != null && bikoSenta.Length > 0)
        //            {
        //                this.form.dgvBiko.Rows[cell.RowIndex].Cells["BIKO_NOTE"].Value = bikoSenta[0].BIKO_NOTE;
        //            }
        //            else
        //            {
        //                this.form.dgvBiko.Rows[cell.RowIndex].Cells["BIKO_NOTE"].Value = string.Empty;
        //                msgLogic.MessageBoxShowError("");
        //                e.Cancel = true;
        //                return true;
        //            }
        //        }
        //        else
        //        {
        //            this.form.dgvBiko.Rows[cell.RowIndex].Cells["BIKO_NOTE"].Value = string.Empty;
        //        }

        //        LogUtility.DebugMethodEnd(false);
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error("DgvBikoValidating ", ex);
        //        this.errmessage.MessageBoxShow("E245");
        //        LogUtility.DebugMethodEnd(true);
        //        return true;
        //    }
        //}

        public virtual void CreateBikoDetailEntity()
        {
            LogUtility.DebugMethodStart();

            List<T_MITSUMORI_DETAIL_2> bikoDetailEntitys = new List<T_MITSUMORI_DETAIL_2>();

            for (int i = 0; i < this.form.Ichiran.RowCount - 1; i++)
            {
                SqlInt32 detailSysId = -1;

                Row row = this.form.Ichiran.Rows[i];

                T_MITSUMORI_DETAIL_2 entity = new T_MITSUMORI_DETAIL_2();

                // モードに依存する処理
                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // 新規の場合は、既にEntryで採番しているので、それに+1する
                        detailSysId = this.accessor.createSystemIdForMitsumori();
                        entity.DETAIL_SYSTEM_ID = detailSysId;

                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:

                        // DETAIL_SYSTEM_IDの採番
                        if (row.Cells[MitumorisyoConst.BIKO_COLUMN_SYS_ID].Value == null
                            || string.IsNullOrEmpty(row.Cells[MitumorisyoConst.BIKO_COLUMN_SYS_ID].Value.ToString()))
                        {
                            // 修正モードでT_MITSUMORI_DETAILが初めて登録されるパターンも張るはずなので、
                            // Detailが無ければ新たに採番(更新モードの場合、ここで初めて採番する)
                            detailSysId = this.accessor.createSystemIdForMitsumori();
                        }
                        else
                        {
                            // 既に登録されていればそのまま使う
                            detailSysId = SqlInt32.Parse(row.Cells[MitumorisyoConst.BIKO_COLUMN_SYS_ID].Value.ToString());
                        }

                        entity.DETAIL_SYSTEM_ID = detailSysId;
                        break;

                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        break;

                    default:
                        break;
                }

                // SYSTEM_ID
                if (!this.dto.entryEntity.SYSTEM_ID.IsNull)
                {
                    entity.SYSTEM_ID = SqlInt64.Parse(this.dto.entryEntity.SYSTEM_ID.Value.ToString());
                }

                // 枝番
                if (!this.dto.entryEntity.SEQ.IsNull)
                {
                    entity.SEQ = SqlInt32.Parse(this.dto.entryEntity.SEQ.Value.ToString());
                }

                // 明細システムID
                if (row.Cells[MitumorisyoConst.BIKO_COLUMN_SYS_ID].Value != null)
                {
                    entity.DETAIL_SYSTEM_ID = SqlInt64.Parse(row.Cells[MitumorisyoConst.BIKO_COLUMN_SYS_ID].Value.ToString());
                }

                if (row.Cells[MitumorisyoConst.BIKO_COLUMN_NAME_BIKO_CD].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.BIKO_COLUMN_NAME_BIKO_CD].Value.ToString()))
                {
                    entity.BIKO_CD = row.Cells[MitumorisyoConst.BIKO_COLUMN_NAME_BIKO_CD].Value.ToString();
                }
                else
                {
                    entity.BIKO_CD = null;
                }

                if (row.Cells[MitumorisyoConst.BIKO_COLUMN_NAME_BIKO_NOTE].Value != null && !string.IsNullOrEmpty(row.Cells[MitumorisyoConst.BIKO_COLUMN_NAME_BIKO_NOTE].Value.ToString()))
                {
                    entity.BIKO_NOTE = row.Cells[MitumorisyoConst.BIKO_COLUMN_NAME_BIKO_NOTE].Value.ToString();
                }
                else
                {
                    entity.BIKO_NOTE = null;
                }

                var dbLogic = new DataBinderLogic<T_MITSUMORI_DETAIL_2>(entity);
                dbLogic.SetSystemProperty(entity, false);

                bikoDetailEntitys.Add(entity);
            }

            this.dto.detailEntity_2 = new T_MITSUMORI_DETAIL_2[bikoDetailEntitys.Count];
            this.dto.detailEntity_2 = bikoDetailEntitys.ToArray<T_MITSUMORI_DETAIL_2>();

            LogUtility.DebugMethodEnd();
        }

        //20250421
        public bool Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                //20250409
                if (e.CellName.Equals("BIKO_CD"))
                {
                    if (e.FormattedValue != null && !string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        M_BIKO_SENTAKUSHI_NYURYOKU c = new M_BIKO_SENTAKUSHI_NYURYOKU();
                        c.BIKO_CD = e.FormattedValue.ToString().PadLeft(3, '0');
                        M_BIKO_SENTAKUSHI_NYURYOKU[] bikoSenta = this.bikoSentaDao.GetAllValidData(c);

                        if (bikoSenta != null && bikoSenta.Length > 0)
                        {
                            this.form.Ichiran[e.RowIndex, e.CellIndex + 1].Value = bikoSenta[0].BIKO_NOTE;
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E020", string.Empty);
                            e.Cancel = true;
                            if (this.form.Ichiran.EditingControl != null)
                            {
                                ((TextBoxEditingControl)this.form.Ichiran.EditingControl).SelectAll();
                            }
                        }
                    }
                    else
                    {
                        this.form.Ichiran[e.RowIndex, e.CellIndex + 1].Value = string.Empty;
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellValidating", ex);
                this.form.errmessage.MessageBoxShow("E245");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }
    }
}