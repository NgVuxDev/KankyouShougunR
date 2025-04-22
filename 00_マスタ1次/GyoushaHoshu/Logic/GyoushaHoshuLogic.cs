// $Id: GyoushaHoshuLogic.cs 226 2013-07-08 09:49:12Z sanbongi $
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using GyoushaHoshu.APP;
using GyoushaHoshu.Const;
using GyoushaHoshu.Validator;
using MasterCommon.Logic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.MasterAccess;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;

using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox.GeoCodingAPI;
using r_framework.Configuration;
using System.Data.SqlTypes;
using r_framework.Dto;

namespace GyoushaHoshu.Logic
{
    /// <summary>
    /// 業者保守画面のビジネスロジック
    /// </summary>
    public class GyoushaHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "GyoushaHoshu.Setting.ButtonSetting.xml";

        private readonly string ButtonInfoXmlPath2 = "GyoushaHoshu.Setting.ButtonSetting2.xml";

        private readonly string GET_ICHIRAN_GENBA_DATA_SQL = "GyoushaHoshu.Sql.GetIchiranGenbaDataSql.sql";

        private readonly string GET_ICHIRAN_ITAKU_DATA_SQL = "GyoushaHoshu.Sql.GetIchiranItakudataSql.sql";

        private readonly string GET_CHIIKI_DATA_SQL = "GyoushaHoshu.Sql.GetChiikidataSql.sql";

        private readonly string GET_POPUP_DATA_SQL = "GyoushaHoshu.Sql.GetPopupdataSql.sql";

        private readonly string GET_TEKIYOUBEGIN_SQL = "GyoushaHoshu.Sql.GetTeikiyouBeginDateSql.sql";

        private readonly string GET_TEKIYOUEND_SQL = "GyoushaHoshu.Sql.GetTeikiyouEndDateSql.sql";

        //20250319
        private readonly string GET_URIAGE_GURUPU_DATA_SQL = "GyoushaHoshu.Sql.GetUriageGurupudataSql.sql";

        /// <summary>
        /// 業者保守画面Form
        /// </summary>
        private GyoushaHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_TORIHIKISAKI torihikisakiEntity;

        private M_KYOTEN kyotenEntity;

        private M_BUSHO bushoEntity;

        private M_SHAIN shainEntity;

        private M_TODOUFUKEN todoufukenEntity;

        private M_CHIIKI chiikiEntity;

        private M_SHUUKEI_KOUMOKU shuukeiEntity;

        private M_GYOUSHU gyoushuEntity;

        //20250319
        private M_GURUPU_NYURYOKU gurupuEntity;

        private M_GURUPU_NYURYOKU gurupuEntity1;

        internal M_SYS_INFO sysinfoEntity;

        private int rowCntGenba;

        private int rowCntItaku;

        // 20141208 ブン 運搬報告書提出先の地域エンティティを追加する start
        private M_CHIIKI upnHoukokushoTeishutsuChiikiEntity;
        // 20141208 ブン 運搬報告書提出先の地域エンティティを追加する end

        /// <summary>
        /// 業者のDao
        /// </summary>
        internal IM_GYOUSHADao daoGyousha;

        /// <summary>
        /// 営業担当者のDao
        /// </summary>
        private IM_EIGYOU_TANTOUSHADao daoEigyou;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao daoGenba;

        /// <summary>
        /// 委託のDao
        /// </summary>
        private IM_ITAKU_KEIYAKU_KIHONDao daoItaku;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// 取引先のDao
        /// </summary>
        private IM_TORIHIKISAKIDao daoTorisaki;

        /// <summary>
        /// 取引先請求のDao
        /// </summary>
        private IM_TORIHIKISAKI_SEIKYUUDao daoSeikyuu;

        /// <summary>
        /// 取引先支払のDao
        /// </summary>
        private IM_TORIHIKISAKI_SHIHARAIDao daoShiharai;

        /// <summary>
        /// 拠点のDao
        /// </summary>
        private IM_KYOTENDao daoKyoten;

        /// <summary>
        /// 部署のDao
        /// </summary>
        private IM_BUSHODao daoBusho;

        /// <summary>
        /// 社員のDao
        /// </summary>
        private IM_SHAINDao daoShain;

        /// <summary>
        /// 都道府県のDao
        /// </summary>
        private IM_TODOUFUKENDao daoTodoufuken;

        /// <summary>
        /// 地域のDao
        /// </summary>
        private IM_CHIIKIDao daoChiiki;

        /// <summary>
        /// 集計項目のDao
        /// </summary>
        private IM_SHUUKEI_KOUMOKUDao daoShuukei;

        /// <summary>
        /// 業種のDao
        /// </summary>
        private IM_GYOUSHUDao daoGyoushu;

        //20250319
        private IM_GURUPU_NYURYOKUDao daoGurupu;

        /// <summary>
        /// 引合業者のDao
        /// </summary>
        private IM_HIKIAI_GYOUSHADao daoHikiaiGyousha;

        /// <summary>
        /// 引合現場のDao
        /// </summary>
        private IM_HIKIAI_GENBADao daoHikiaiGenba;

        /// <summary>
        /// タブコントロール制御用
        /// </summary>
        private TabPageManager _tabPageManager = null;

        /// <summary>
        /// 委託契約書種類
        /// </summary>
        private Dictionary<string, string> ItakuKeiyakuShurui = new Dictionary<string, string>()
        {
            {"1", "収集・運搬委託契約"},
            {"2", "処分委託契約"},
            {"3", "収集・運搬/処分委託契約"}
        };

        /// <summary>
        /// 委託契約書ステータス
        /// </summary>
        private Dictionary<string, string> ItakuKeiyakuStatus = new Dictionary<string, string>()
        {
            {"1", "作成"},
            {"2", "送付"},
            {"3", "返送"},
            {"4", "完了"},
            {"5", "解約済"}
        };

        // 業者エントリ
        internal M_GYOUSHA gyousha = new M_GYOUSHA();

        // 取引先情報をコピーしないフラグ
        internal bool isNotTorihikisakiCopy = false;

        #endregion

        // Begin: LANDUONG - 20220214 - refs#160052
        internal bool denshiSeikyusho, denshiSeikyuRaku;        
        // End: LANDUONG - 20220214 - refs#160052

        #region プロパティ

        /// <summary>
        /// 登録・更新・削除処理の成否
        /// </summary>
        public bool isRegist { get; set; }

        /// <summary>
        /// 業者CD
        /// </summary>
        public string GyoushaCd { get; set; }

        /// <summary>
        /// 取引先CD
        /// </summary>
        public string TorihikisakiCd { get; set; }

        /// <summary>
        /// 連携フラグ
        /// </summary>
        public bool RenkeiFlg { get; set; }

        /// <summary>
        /// 電子申請フラグ
        /// </summary>
        public bool denshiShinseiFlg { get; set; }

        /// <summary>
        /// 自社区分チェック
        /// </summary>
        public bool FlgJishaKbn { get; set; }

        /// <summary>
        /// 排出事業者区分チェック
        /// </summary>
        public bool FlgHaishutsuJigyoushaKbn { get; set; }

        /// <summary>
        /// 運搬受託者区分チェック
        /// </summary>
        public bool FlgUnpanJutakushaKbn { get; set; }

        /// <summary>
        /// 処分受託者区分チェック
        /// </summary>
        public bool FlgShobunJutakushaKbn { get; set; }

        /// <summary>
        /// マニ返送先区分チェック
        /// </summary>
        public bool FlgManiHensousakiKbn { get; set; }

        /// <summary>
        /// 検索結果(現場一覧)
        /// </summary>
        public DataTable SearchResultGenba { get; set; }

        /// <summary>
        /// 検索結果(委託一覧)
        /// </summary>
        public DataTable SearchResultItaku { get; set; }

        /// <summary>
        ///　エラー判定フラグ
        /// </summary>
        public bool isError { get; set; }

        /// <summary>
        /// 現場マスタ追加フラグ
        /// </summary>
        public bool IsGenbaAdd { get; set; }

        internal M_GYOUSHA GyoushaEntity { get; set; }

        /// <summary>
        /// 現場マスタ（現場CD=000000）
        /// </summary>
        public M_GENBA genbaEntity;

        /// <summary>
        /// 承認済申請一覧から呼び出されたかどうかのフラグを取得・設定します
        /// </summary>
        internal bool IsFromShouninzumiDenshiShinseiIchiran { get; set; }

        /// <summary>
        /// 本登録の元となる引合業者エンティティを取得・設定します
        /// </summary>
        internal M_HIKIAI_GYOUSHA LoadHikiaiGyoushaEntity { get; private set; }

        /// <summary>
        /// 本登録の元となる仮登録現場エンティティを取得・設定します
        /// </summary>
        internal M_KARI_GYOUSHA LoadKariGyoushaEntity { get; private set; }

        /// <summary>
        /// 取引先有無チェック
        /// </summary>
        public string TorihikisakiUmKbn { get; set; }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public GyoushaHoshuLogic(GyoushaHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.daoGyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.daoEigyou = DaoInitUtility.GetComponent<IM_EIGYOU_TANTOUSHADao>();
            this.daoGenba = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.daoItaku = DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_KIHONDao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.daoBusho = DaoInitUtility.GetComponent<IM_BUSHODao>();
            this.daoChiiki = DaoInitUtility.GetComponent<IM_CHIIKIDao>();
            this.daoGyoushu = DaoInitUtility.GetComponent<IM_GYOUSHUDao>();
            this.daoKyoten = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.daoShain = DaoInitUtility.GetComponent<IM_SHAINDao>();
            this.daoShuukei = DaoInitUtility.GetComponent<IM_SHUUKEI_KOUMOKUDao>();
            this.daoTodoufuken = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            this.daoTorisaki = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.daoSeikyuu = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
            this.daoShiharai = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
            this.daoHikiaiGyousha = DaoInitUtility.GetComponent<IM_HIKIAI_GYOUSHADao>();
            this.daoHikiaiGenba = DaoInitUtility.GetComponent<IM_HIKIAI_GENBADao>();

            //20250319
            this.daoGurupu = DaoInitUtility.GetComponent<IM_GURUPU_NYURYOKUDao>();

            _tabPageManager = new TabPageManager(this.form.JOHOU);

            // Begin: LANDUONG - 20220214 - refs#160052
            // 電子請求オプ
            denshiSeikyusho = r_framework.Configuration.AppConfig.AppOptions.IsElectronicInvoice();
            //電子請求楽楽明細オプ
            denshiSeikyuRaku = r_framework.Configuration.AppConfig.AppOptions.IsRakurakuMeisai();            
            // End: LANDUONG - 20220214 - refs#160052

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BusinessBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit(parentForm);

                // MAPBOX連携が無効な場合
                this._tabPageManager.ChangeTabPageVisible(6, (AppConfig.AppOptions.IsMAPBOX()) ? true : false);

                // イベントの初期化
                this.EventInit(parentForm);

                // 処理モード別画面初期化
                this.ModeInit(windowType, parentForm);
                this.allControl = this.form.allControl;
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                // 画面起動時に電子請求書で追加するコントロール・項目の表示/非表示を切り替える
                //this.setDensiSeikyushoVisible();
                // 20160429 koukoukon v2.1_電子請求書 #16612 end

                // Begin: LANDUONG - 20220214 - refs#160052
                this.SetDensiSeikyushoAndRakurakuVisible();
                // End: LANDUONG - 20220214 - refs#160052

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("WindowInit", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 処理モード別画面初期化処理
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="parentForm"></param>
        public void ModeInit(WINDOW_TYPE windowType, BusinessBaseForm parentForm)
        {
            if (this.IsFromShouninzumiDenshiShinseiIchiran)
            {
                // 電子申請から呼び出されたときは、コードを移動する
                if (!String.IsNullOrEmpty(this.form.ShinseiGyoushaCd))
                {
                    this.GyoushaCd = this.form.ShinseiGyoushaCd;
                }
                else
                {
                    this.GyoushaCd = this.form.ShinseiHikiaiGyoushaCd;
                }
            }

            bool catchErr = false;
            switch (windowType)
            {
                // 【新規】モード
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    this.WindowInitNew(parentForm);
                    break;

                // 【修正】モード
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    catchErr = this.WindowInitUpdate(parentForm);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                    break;

                // 【削除】モード
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    this.WindowInitDelete(parentForm);
                    break;

                // 【参照】モード
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    catchErr = this.WindowInitReference(parentForm);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                    break;

                // デフォルトは【新規】モード
                default:
                    this.WindowInitNew(parentForm);
                    break;
            }
        }

        /// <summary>
        /// 画面項目初期化処理モード【新規】
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public void WindowInitNew(BusinessBaseForm parentForm)
        {
            if (string.IsNullOrEmpty(this.GyoushaCd))
            {
                this.GetSysInfo();

                // 【新規】モードで初期化
                bool catchErr = WindowInitNewMode(parentForm);
                if (catchErr)
                {
                    throw new Exception("");
                }
            }
            else
            {
                // 【複写】モードで初期化
                WindowInitNewCopyMode(parentForm);
            }
        }

        /// <summary>
        /// システム設定マスタ取得
        /// </summary>
        private void GetSysInfo()
        {
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            this.sysinfoEntity = sysInfo[0];
        }

        /// <summary>
        /// 画面項目初期化【新規】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitNewMode(BusinessBaseForm parentForm)
        {
            try
            {
                // 全コントロール操作可能とする
                this.AllControlLock(true);

                // BaseHeader部
                BusinessBaseForm findForm = (BusinessBaseForm)this.form.Parent.FindForm();
                DetailedHeaderForm header = (DetailedHeaderForm)findForm.headerForm;
                header.CreateDate.Text = string.Empty;
                header.CreateUser.Text = string.Empty;
                header.LastUpdateDate.Text = string.Empty;
                header.LastUpdateUser.Text = string.Empty;

                // 共通部
                //this.form.TORIHIKISAKI_UMU_KBN.Text = "1";
                this.form.Gyousha_KBN_1.Checked = (bool)this.sysinfoEntity.GYOUSHA_KBN_UKEIRE;
                this.form.Gyousha_KBN_2.Checked = (bool)this.sysinfoEntity.GYOUSHA_KBN_SHUKKA;
                this.form.Gyousha_KBN_3.Checked = (bool)this.sysinfoEntity.GYOUSHA_KBN_MANI;
                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME1.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME2.Text = string.Empty;
                this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                this.form.KYOTEN_CD.Text = string.Empty;
                this.form.KYOTEN_NAME.Text = string.Empty;
                this.form.GYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_NAME1.Text = string.Empty;
                this.form.GYOUSHA_NAME2.Text = string.Empty;
                this.form.GYOUSHA_FURIGANA.Text = string.Empty;
                this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.form.GYOUSHA_KEISHOU1.Text = this.sysinfoEntity.GYOUSHA_KEISHOU1;
                this.form.GYOUSHA_KEISHOU2.Text = this.sysinfoEntity.GYOUSHA_KEISHOU2;
                this.form.GYOUSHA_TEL.Text = string.Empty;
                this.form.GYOUSHA_KEITAI_TEL.Text = string.Empty;
                this.form.GYOUSHA_FAX.Text = string.Empty;
                this.form.EIGYOU_TANTOU_BUSHO_CD.Text = string.Empty;
                this.form.BUSHO_NAME.Text = string.Empty;
                this.form.EIGYOU_TANTOU_CD.Text = string.Empty;
                this.form.SHAIN_NAME.Text = string.Empty;
                this.form.TEKIYOU_BEGIN.Value = findForm.sysDate;
                this.form.TEKIYOU_END.Value = null;
                this.form.CHUUSHI_RIYUU1.Text = string.Empty;
                this.form.CHUUSHI_RIYUU2.Text = string.Empty;
                this.form.SHOKUCHI_KBN.Checked = false;
                this.form.TORIHIKISAKI_UMU_KBN.Text = "1";

                // 基本情報
                this.form.GYOUSHA_POST.Text = string.Empty;
                this.form.GYOUSHA_TODOUFUKEN_CD.Text = string.Empty;
                this.form.TODOUFUKEN_NAME.Text = string.Empty;
                this.form.GYOUSHA_ADDRESS1.Text = string.Empty;
                this.form.GYOUSHA_ADDRESS2.Text = string.Empty;
                this.form.CHIIKI_CD.Text = string.Empty;
                this.form.CHIIKI_NAME.Text = string.Empty;
                this.form.BUSHO.Text = string.Empty;
                this.form.TANTOUSHA.Text = string.Empty;
                this.form.GYOUSHA_DAIHYOU.Text = string.Empty;
                this.form.SHUUKEI_ITEM_CD.Text = string.Empty;
                this.form.SHUUKEI_ITEM_NAME.Text = string.Empty;
                this.form.GYOUSHU_CD.Text = string.Empty;
                this.form.GYOUSHU_NAME.Text = string.Empty;
                this.form.BIKOU1.Text = string.Empty;
                this.form.BIKOU2.Text = string.Empty;
                this.form.BIKOU3.Text = string.Empty;
                this.form.BIKOU4.Text = string.Empty;

                //20250320
                this.form.URIAGE_GURUPU_CD.Text = string.Empty;
                this.form.URIAGE_GURUPU_NAME.Text = string.Empty;
                this.form.SHIHARAI_GURUPU_CD.Text = string.Empty;
                this.form.SHIHARAI_GURUPU_NAME.Text = string.Empty;

                // 請求情報
                this.form.SEIKYUU_SOUFU_NAME1.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_NAME2.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_KEISHOU1.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_KEISHOU2.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_POST.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_ADDRESS1.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_ADDRESS2.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_BUSHO.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_TANTOU.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_TEL.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_FAX.Text = string.Empty;
                this.form.SEIKYUU_TANTOU.Text = string.Empty;
                this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;
                if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                {
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.sysinfoEntity.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.ToString();
                }
                this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN.ToString();
                }
                this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.IsNull)
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.ToString()));
                }
                this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                M_KYOTEN seikyuuKyoten = this.daoKyoten.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                if (seikyuuKyoten != null)
                {
                    this.form.SEIKYUU_KYOTEN_NAME.Text = seikyuuKyoten.KYOTEN_NAME_RYAKU;
                }
                this.ChangeSeikyuuKyotenPrintKbn();

                // Begin: LANDUONG - 20220214 - refs#160052
                this.form.HAKKOUSAKI_CD.Text = string.Empty;
                this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;                
                this.HakkousakuAndRakurakuCDCheck();
                // End: LANDUONG - 20220214 - refs#160052

                // 支払情報
                this.form.SHIHARAI_SOUFU_NAME1.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_NAME2.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_KEISHOU1.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_KEISHOU2.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_POST.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_ADDRESS1.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_ADDRESS2.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_BUSHO.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_TANTOU.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_TEL.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_FAX.Text = string.Empty;
                this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN.ToString();
                }
                this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.IsNull)
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.ToString()));
                }
                this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                M_KYOTEN shiharaiKyoten = this.daoKyoten.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                if (shiharaiKyoten != null)
                {
                    this.form.SHIHARAI_KYOTEN_NAME.Text = shiharaiKyoten.KYOTEN_NAME_RYAKU;
                }
                this.ChangeShiharaiKyotenPrintKbn();

                // 現場一覧
                this.form.GENBA_ICHIRAN.DataSource = null;
                this.form.GENBA_ICHIRAN.Rows.Clear();

                // 業者分類
                if (this.GyoushaEntity != null)
                {
                    // 自社区分のチェックが走る前にクリア
                    this.GyoushaEntity.GYOUSHA_CD = string.Empty;
                }
                this.form.JISHA_KBN.Checked = (bool)sysinfoEntity.GYOUSHA_JISHA_KBN;
                if (!sysinfoEntity.GYOUSHA_HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsNull)
                {
                    this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = (bool)sysinfoEntity.GYOUSHA_HAISHUTSU_NIZUMI_GYOUSHA_KBN;
                }

                if (!sysinfoEntity.GYOUSHA_UNPAN_JUTAKUSHA_KAISHA_KBN.IsNull)
                {
                    this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = (bool)sysinfoEntity.GYOUSHA_UNPAN_JUTAKUSHA_KAISHA_KBN;
                }

                if (!sysinfoEntity.GYOUSHA_SHOBUN_NIOROSHI_GYOUSHA_KBN.IsNull)
                {
                    this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = (bool)sysinfoEntity.GYOUSHA_SHOBUN_NIOROSHI_GYOUSHA_KBN;
                }

                if (!sysinfoEntity.GYOUSHA_MANI_HENSOUSAKI_KBN.IsNull)
                {
                    this.form.MANI_HENSOUSAKI_KBN.Checked = (bool)sysinfoEntity.GYOUSHA_MANI_HENSOUSAKI_KBN;
                }
                this.form.MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_NAME1.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_NAME2.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_POST.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_ADDRESS1.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_ADDRESS2.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_BUSHO.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_TANTOU.Text = string.Empty;
                if (!string.IsNullOrEmpty(this.TorihikisakiCd))
                {
                    this.GyoushaEntity = new M_GYOUSHA();
                    this.GyoushaEntity.TORIHIKISAKI_CD = this.TorihikisakiCd;
                    this.SearchTorihikisaki();
                }
                if (!string.IsNullOrEmpty(this.TorihikisakiCd) && this.torihikisakiEntity != null)
                {
                    this.form.MANI_HENSOUSAKI_KBN.Checked = true;
                    if (torihikisakiEntity.MANI_HENSOUSAKI_KBN.IsTrue)
                    {
                        if (!torihikisakiEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.IsNull)
                        {
                            this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = Convert.ToString(torihikisakiEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Value);

                            if ("1".Equals(this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text))
                            {
                                this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = true;
                            }
                            else
                            {
                                this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                            }
                        }
                        if ("2".Equals(this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text))
                        {
                            this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                            this.form.MANI_HENSOUSAKI_KEISHOU1.Text = torihikisakiEntity.MANI_HENSOUSAKI_KEISHOU1;
                            this.form.MANI_HENSOUSAKI_KEISHOU2.Text = torihikisakiEntity.MANI_HENSOUSAKI_KEISHOU2;
                            this.form.MANI_HENSOUSAKI_NAME1.Text = torihikisakiEntity.MANI_HENSOUSAKI_NAME1;
                            this.form.MANI_HENSOUSAKI_NAME2.Text = torihikisakiEntity.MANI_HENSOUSAKI_NAME2;
                            this.form.MANI_HENSOUSAKI_POST.Text = torihikisakiEntity.MANI_HENSOUSAKI_POST;
                            this.form.MANI_HENSOUSAKI_ADDRESS1.Text = torihikisakiEntity.MANI_HENSOUSAKI_ADDRESS1;
                            this.form.MANI_HENSOUSAKI_ADDRESS2.Text = torihikisakiEntity.MANI_HENSOUSAKI_ADDRESS2;
                            this.form.MANI_HENSOUSAKI_BUSHO.Text = torihikisakiEntity.MANI_HENSOUSAKI_BUSHO;
                            this.form.MANI_HENSOUSAKI_TANTOU.Text = torihikisakiEntity.MANI_HENSOUSAKI_TANTOU;
                        }
                    }
                    else
                    {
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "1";
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = true;
                        this.form.MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_NAME1.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_NAME2.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_POST.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_ADDRESS1.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_ADDRESS2.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_BUSHO.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_TANTOU.Text = string.Empty;
                    }
                }
                else
                {
                    if (!sysinfoEntity.GYOUSHA_MANI_HENSOUSAKI_KBN.IsNull)
                    {
                        this.form.MANI_HENSOUSAKI_KBN.Checked = (bool)sysinfoEntity.GYOUSHA_MANI_HENSOUSAKI_KBN;
                    }
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "2";
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                    this.form.MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_NAME1.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_NAME2.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_POST.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_ADDRESS1.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_ADDRESS2.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_BUSHO.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_TANTOU.Text = string.Empty;
                }
                //if (!this.form.Gyousha_KBN_3.Checked)
                //{
                //    //this.form.JISHA_KBN.Enabled = false;
                //    this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Enabled = false;
                //    this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Enabled = false;
                //    this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Enabled = false;
                //}

                // 20141208 ブン 運搬報告書提出先を追加する start
                this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = string.Empty;
                this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = string.Empty;
                // 20141208 ブン 運搬報告書提出先を追加する end

                // 委託契約情報
                this.form.ITAKU_ICHIRAN.DataSource = null;
                this.form.ITAKU_ICHIRAN.Rows.Clear();

                // mapbox項目
                this.form.GyoushaLatitude.Text = string.Empty;
                this.form.GyoushaLongitude.Text = string.Empty;
                this.form.LocationInfoUpdateName.Text = string.Empty;
                this.form.LocationInfoUpdateDate.Text = string.Empty;

                //set TORIHIKISAKI_UMU_KBN from system
                if (this.sysinfoEntity != null)
                {
                    if (!this.sysinfoEntity.TORIHIKISAKI_UMU_KBN.IsNull)
                    {
                        this.form.TORIHIKISAKI_UMU_KBN.Text = Convert.ToString(this.sysinfoEntity.TORIHIKISAKI_UMU_KBN);
                    }
                }

                if (!string.IsNullOrEmpty(this.TorihikisakiCd) && !isNotTorihikisakiCopy)
                {
                    //this.form.TORIHIKISAKI_UMU_KBN.Text = "1";
                    this.form.TORIHIKISAKI_CD.Text = this.TorihikisakiCd;
                    this.SearchsetTorihikisaki();
                    this.TorihikisakiCopy();
                }

                // functionボタン
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = true;    // 取消

                // 業者分類タブ初期化
                bool catchErr = this.ManiCheckOffCheck();

                this.GyoushaEntity = null;
                this.genbaEntity = null;

                this.form.GYOUSHA_CD.Focus();
                return catchErr;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitNewMode", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 画面項目初期化【複写】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        private void WindowInitNewCopyMode(BusinessBaseForm parentForm)
        {
            // 全コントロールを操作可能とする
            this.AllControlLock(true);

            // 検索結果を画面に設定
            this.SetWindowData();

            // functionボタン
            parentForm.bt_func3.Enabled = false;    // 修正
            parentForm.bt_func9.Enabled = true;     // 登録
            parentForm.bt_func11.Enabled = true;    // 取消

            //複写モード時は業者コードのコピーはなし
            this.GyoushaEntity.GYOUSHA_CD = string.Empty;
            this.form.GYOUSHA_CD.Text = string.Empty;
            this.GyoushaCd = string.Empty;

            // 複写モード時は、紐付く現場はないためクリア
            this.genbaEntity = null;

            // 承認済申請一覧から遷移時は引合業者の適用開始・終了日を優先
            if (!this.IsFromShouninzumiDenshiShinseiIchiran)
            {
                //適用開始日(当日日付)
                this.form.TEKIYOU_BEGIN.Value = parentForm.sysDate;
                //適用終了日
                this.form.TEKIYOU_END.Value = null;
            }
            // ヘッダー項目
            DetailedHeaderForm header = (DetailedHeaderForm)((BusinessBaseForm)this.form.ParentForm).headerForm;
            header.CreateDate.Text = string.Empty;
            header.CreateUser.Text = string.Empty;
            header.LastUpdateDate.Text = string.Empty;
            header.LastUpdateUser.Text = string.Empty;

            if (!this.IsFromShouninzumiDenshiShinseiIchiran)
            {
                // 電子申請の本登録の場合、GENBA_ICHIRANにデータが設定されているため初期化しない。
                //現場一覧クリア
                this.form.GENBA_ICHIRAN.DataSource = null;
                this.form.GENBA_ICHIRAN.Rows.Clear();
            }

            //委託契約者情報クリア
            this.form.ITAKU_ICHIRAN.DataSource = null;
            this.form.ITAKU_ICHIRAN.Rows.Clear();

            // 発行先コード
            if (!this.IsFromShouninzumiDenshiShinseiIchiran)
            {
                this.form.HAKKOUSAKI_CD.Text = string.Empty;
            }

            // 複写モード時はmapbox項目のコピーなし
            this.form.GyoushaLatitude.Text = string.Empty;
            this.form.GyoushaLongitude.Text = string.Empty;
            this.form.LocationInfoUpdateName.Text = string.Empty;
            this.form.LocationInfoUpdateDate.Text = string.Empty;

            // 業者分類タブ初期化
            bool catchErr = this.ManiCheckOffCheck();
            if (catchErr)
            {
                throw new Exception("");
            }
        }

        /// <summary>
        /// 画面項目初期化【修正】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitUpdate(BusinessBaseForm parentForm)
        {
            try
            {
                // 全コントロールを操作可能とする
                this.AllControlLock(true);

                // 検索結果を画面に設定
                this.SetWindowData();

                // 修正モード固有UI設定
                this.form.GYOUSHA_CD.Enabled = false;   // 業者CD
                this.form.bt_gyoushacd_saiban.Enabled = false;    // 採番ボタン

                // functionボタン
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = true;    // 取消

                // 業者分類タブ初期化
                bool catchErr = this.ManiCheckOffCheck();
                return catchErr;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInitUpdate", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitUpdate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 画面項目初期化【削除】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public void WindowInitDelete(BusinessBaseForm parentForm)
        {
            // 検索結果を画面に設定
            this.SetWindowData();

            // 削除モード固有UI設定
            this.AllControlLock(false);

            // functionボタン
            parentForm.bt_func3.Enabled = true;     // 修正
            parentForm.bt_func9.Enabled = true;     // 登録
            parentForm.bt_func11.Enabled = false;   // 取消
        }

        /// <summary>
        /// 画面項目初期化【参照】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitReference(BusinessBaseForm parentForm)
        {
            try
            {
                // 検索結果を画面に設定
                this.SetWindowData();

                // 削除モード固有UI設定
                this.AllControlLock(false);

                // functionボタン
                parentForm.bt_func3.Enabled = true;     // 修正
                parentForm.bt_func9.Enabled = false;     // 登録
                parentForm.bt_func11.Enabled = false;    // 取消
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInitReference", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitReference", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// データを取得し、画面に設定
        /// </summary>
        private void SetWindowData()
        {
            this.GyoushaEntity = null;
            this.LoadHikiaiGyoushaEntity = null;
            this.LoadKariGyoushaEntity = null;
            this.TorihikisakiUmKbn = "";

            // 各種データ取得
            if (false == this.IsFromShouninzumiDenshiShinseiIchiran)
            {
                this.SearchGenba(this.GyoushaCd, "000000");
                this.SearchGyousha();
            }
            else
            {
                if (null == this.form.ShinseiHikiaiGyoushaCd || String.IsNullOrEmpty(this.form.ShinseiHikiaiGyoushaCd))
                {
                    // 修正申請なので仮業者を取得
                    this.SearchKariGyousha();
                    // 業者エンティティにコピー
                    this.CopyKariGyoushaEntityToGyoushaEntity();
                }
                else
                {
                    // 新規申請なので引合業者を取得
                    this.SearchHikiaiGyousha();
                    // 業者エンティティにコピー
                    this.CopyHikiaiGyoushaEntityToGyoushaEntity();
                }
            }
            this.SearchTorihikisaki();
            this.SearchBusho();
            this.SearchChiiki();
            this.SearchGyoushu();
            this.SearchKyoten();
            this.SearchShain();
            this.SearchShuukeiItem();
            this.SearchTodoufuken();
            this.GetSysInfo();

            //20250319
            this.SearchUriageGurupu();

            //20250320
            this.SearchShiharaiGurupu();

            // 20141208 ブン 運搬報告書提出データを取得する start
            this.SearchUpnHoukokushoTeishutsuChiiki();
            // 20141208 ブン 運搬報告書提出データを取得する end

            // BaseHeader部
            BusinessBaseForm findForm = (BusinessBaseForm)this.form.Parent.FindForm();
            DetailedHeaderForm header = (DetailedHeaderForm)findForm.headerForm;
            header.CreateDate.Text = this.GyoushaEntity.CREATE_DATE.ToString();
            header.CreateUser.Text = this.GyoushaEntity.CREATE_USER;
            header.LastUpdateDate.Text = this.GyoushaEntity.UPDATE_DATE.ToString();
            header.LastUpdateUser.Text = this.GyoushaEntity.UPDATE_USER;

            // 共通部
            this.form.TORIHIKISAKI_UMU_KBN.Text = string.Empty; // 取引先有無区分が前回表示と同じだった場合入力許可となってしまうので一旦クリアしてイベント駆動するようにしておく
            this.form.TORIHIKISAKI_UMU_KBN.Text = (this.GyoushaEntity.TORIHIKISAKI_UMU_KBN.IsNull) ? "2" : this.GyoushaEntity.TORIHIKISAKI_UMU_KBN.ToString();
            TorihikisakiUmKbn = this.form.TORIHIKISAKI_UMU_KBN.Text;    //変更前の取引先有無を保管

            if (!this.GyoushaEntity.GYOUSHAKBN_UKEIRE.IsNull)
            {
                this.form.Gyousha_KBN_1.Checked = (bool)this.GyoushaEntity.GYOUSHAKBN_UKEIRE;
            }
            else
            {
                this.form.Gyousha_KBN_1.Checked = false;
            }
            if (!this.GyoushaEntity.GYOUSHAKBN_SHUKKA.IsNull)
            {
                this.form.Gyousha_KBN_2.Checked = (bool)this.GyoushaEntity.GYOUSHAKBN_SHUKKA;
            }
            else
            {
                this.form.Gyousha_KBN_2.Checked = false;
            }
            if (!this.GyoushaEntity.GYOUSHAKBN_MANI.IsNull)
            {
                // マニ記載のチェックを外す場合は各区分をクリアしておく
                // ※チェックを外す際にエラーとなる為
                var gyoushaKbnMani = this.GyoushaEntity.GYOUSHAKBN_MANI.Value;
                //if (!gyoushaKbnMani)
                //{
                //    this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = false;
                //    this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = false;
                //    this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = false;
                //    //this.form.JISHA_KBN.Checked = false;
                //}
                this.form.Gyousha_KBN_3.Checked = gyoushaKbnMani;
            }
            else
            {
                this.form.Gyousha_KBN_3.Checked = false;
            }
            this.form.TORIHIKISAKI_CD.Text = this.GyoushaEntity.TORIHIKISAKI_CD;
            if (this.torihikisakiEntity != null)
            {
                this.form.TORIHIKISAKI_NAME1.Text = this.torihikisakiEntity.TORIHIKISAKI_NAME1;
                this.form.TORIHIKISAKI_NAME2.Text = this.torihikisakiEntity.TORIHIKISAKI_NAME2;
                if (this.torihikisakiEntity.TEKIYOU_BEGIN.IsNull)
                {
                    this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                }
                else
                {
                    this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = this.torihikisakiEntity.TEKIYOU_BEGIN;
                }
                if (this.torihikisakiEntity.TEKIYOU_END.IsNull)
                {
                    this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                }
                else
                {
                    this.form.TORIHIKISAKI_TEKIYOU_END.Value = this.torihikisakiEntity.TEKIYOU_END;
                }
            }
            if (!this.GyoushaEntity.KYOTEN_CD.IsNull)
            {
                this.form.KYOTEN_CD.Text = this.GyoushaEntity.KYOTEN_CD.Value.ToString();
            }
            if (this.kyotenEntity != null)
            {
                this.form.KYOTEN_NAME.Text = this.kyotenEntity.KYOTEN_NAME_RYAKU;
            }
            this.form.GYOUSHA_CD.Text = this.GyoushaEntity.GYOUSHA_CD;  //TODO:ここのCDを引数で渡されるか？
            this.form.GYOUSHA_NAME1.Text = this.GyoushaEntity.GYOUSHA_NAME1;
            this.form.GYOUSHA_NAME2.Text = this.GyoushaEntity.GYOUSHA_NAME2;
            this.form.GYOUSHA_FURIGANA.Text = this.GyoushaEntity.GYOUSHA_FURIGANA;
            this.form.GYOUSHA_NAME_RYAKU.Text = this.GyoushaEntity.GYOUSHA_NAME_RYAKU;
            this.form.GYOUSHA_KEISHOU1.Text = this.GyoushaEntity.GYOUSHA_KEISHOU1;
            this.form.GYOUSHA_KEISHOU2.Text = this.GyoushaEntity.GYOUSHA_KEISHOU2;
            this.form.GYOUSHA_TEL.Text = this.GyoushaEntity.GYOUSHA_TEL;
            this.form.GYOUSHA_KEITAI_TEL.Text = this.GyoushaEntity.GYOUSHA_KEITAI_TEL;
            this.form.GYOUSHA_FAX.Text = this.GyoushaEntity.GYOUSHA_FAX;
            if (this.bushoEntity != null)
            {
                this.form.EIGYOU_TANTOU_BUSHO_CD.Text = this.GyoushaEntity.EIGYOU_TANTOU_BUSHO_CD;
                this.form.BUSHO_NAME.Text = this.bushoEntity.BUSHO_NAME_RYAKU;
            }
            if (this.shainEntity != null)
            {
                this.form.EIGYOU_TANTOU_CD.Text = this.GyoushaEntity.EIGYOU_TANTOU_CD;
                this.form.SHAIN_NAME.Text = this.shainEntity.SHAIN_NAME_RYAKU;
            }

            if (this.GyoushaEntity.TEKIYOU_BEGIN.IsNull)
            {
                this.form.TEKIYOU_BEGIN.Value = null;
            }
            else
            {
                this.form.TEKIYOU_BEGIN.Value = this.GyoushaEntity.TEKIYOU_BEGIN.Value;
            }

            if (this.GyoushaEntity.TEKIYOU_END.IsNull)
            {
                this.form.TEKIYOU_END.Value = null;
            }
            else
            {
                this.form.TEKIYOU_END.Value = this.GyoushaEntity.TEKIYOU_END.Value;
            }
            this.form.CHUUSHI_RIYUU1.Text = this.GyoushaEntity.CHUUSHI_RIYUU1;
            this.form.CHUUSHI_RIYUU2.Text = this.GyoushaEntity.CHUUSHI_RIYUU2;

            if (!this.GyoushaEntity.SHOKUCHI_KBN.IsNull)
            {
                this.form.SHOKUCHI_KBN.Checked = (bool)this.GyoushaEntity.SHOKUCHI_KBN;
            }
            else
            {
                this.form.SHOKUCHI_KBN.Checked = false;
            }

            // 基本情報
            this.form.GYOUSHA_POST.Text = this.GyoushaEntity.GYOUSHA_POST;
            if (!this.GyoushaEntity.GYOUSHA_TODOUFUKEN_CD.IsNull)
            {
                if (this.todoufukenEntity != null)
                {
                    this.form.GYOUSHA_TODOUFUKEN_CD.Text = this.GyoushaEntity.GYOUSHA_TODOUFUKEN_CD.ToString();
                    this.form.TODOUFUKEN_NAME.Text = this.todoufukenEntity.TODOUFUKEN_NAME;
                }
            }

            this.form.GYOUSHA_ADDRESS1.Text = this.GyoushaEntity.GYOUSHA_ADDRESS1;
            this.form.GYOUSHA_ADDRESS2.Text = this.GyoushaEntity.GYOUSHA_ADDRESS2;
            if (this.chiikiEntity != null)
            {
                this.form.CHIIKI_CD.Text = this.GyoushaEntity.CHIIKI_CD;
                this.form.CHIIKI_NAME.Text = this.chiikiEntity.CHIIKI_NAME_RYAKU;
            }

            this.form.BUSHO.Text = this.GyoushaEntity.BUSHO;
            this.form.TANTOUSHA.Text = this.GyoushaEntity.TANTOUSHA;
            this.form.GYOUSHA_DAIHYOU.Text = this.GyoushaEntity.GYOUSHA_DAIHYOU;
            if (this.shuukeiEntity != null)
            {
                this.form.SHUUKEI_ITEM_CD.Text = this.GyoushaEntity.SHUUKEI_ITEM_CD;
                this.form.SHUUKEI_ITEM_NAME.Text = this.shuukeiEntity.SHUUKEI_KOUMOKU_NAME_RYAKU;
            }
            if (this.gyoushuEntity != null)
            {
                this.form.GYOUSHU_CD.Text = this.GyoushaEntity.GYOUSHU_CD;
                this.form.GYOUSHU_NAME.Text = this.gyoushuEntity.GYOUSHU_NAME_RYAKU;
            }
            this.form.BIKOU1.Text = this.GyoushaEntity.BIKOU1;
            this.form.BIKOU2.Text = this.GyoushaEntity.BIKOU2;
            this.form.BIKOU3.Text = this.GyoushaEntity.BIKOU3;
            this.form.BIKOU4.Text = this.GyoushaEntity.BIKOU4;

            //20250320-21
            if (this.gurupuEntity != null)
            {
                this.form.URIAGE_GURUPU_CD.Text = this.GyoushaEntity.URIAGE_GURUPU_CD;
                this.form.URIAGE_GURUPU_NAME.Text = this.gurupuEntity.GURUPU_NAME;
            }
            if (this.gurupuEntity1 != null)
            {
                this.form.SHIHARAI_GURUPU_CD.Text = this.GyoushaEntity.SHIHARAI_GURUPU_CD;
                this.form.SHIHARAI_GURUPU_NAME.Text = this.gurupuEntity1.GURUPU_NAME;
            }

            if (this.form.TORIHIKISAKI_UMU_KBN.Text == "1")
            {
                // 請求情報
                this.form.SEIKYUU_SOUFU_NAME1.Text = this.GyoushaEntity.SEIKYUU_SOUFU_NAME1;
                this.form.SEIKYUU_SOUFU_NAME2.Text = this.GyoushaEntity.SEIKYUU_SOUFU_NAME2;
                this.form.SEIKYUU_SOUFU_KEISHOU1.Text = this.GyoushaEntity.SEIKYUU_SOUFU_KEISHOU1;
                this.form.SEIKYUU_SOUFU_KEISHOU2.Text = this.GyoushaEntity.SEIKYUU_SOUFU_KEISHOU2;
                this.form.SEIKYUU_SOUFU_POST.Text = this.GyoushaEntity.SEIKYUU_SOUFU_POST;
                this.form.SEIKYUU_SOUFU_ADDRESS1.Text = this.GyoushaEntity.SEIKYUU_SOUFU_ADDRESS1;
                this.form.SEIKYUU_SOUFU_ADDRESS2.Text = this.GyoushaEntity.SEIKYUU_SOUFU_ADDRESS2;
                this.form.SEIKYUU_SOUFU_BUSHO.Text = this.GyoushaEntity.SEIKYUU_SOUFU_BUSHO;
                this.form.SEIKYUU_SOUFU_TANTOU.Text = this.GyoushaEntity.SEIKYUU_SOUFU_TANTOU;
                this.form.SEIKYUU_SOUFU_TEL.Text = this.GyoushaEntity.SEIKYUU_SOUFU_TEL;
                this.form.SEIKYUU_SOUFU_FAX.Text = this.GyoushaEntity.SEIKYUU_SOUFU_FAX;
                this.form.SEIKYUU_TANTOU.Text = this.GyoushaEntity.SEIKYUU_TANTOU;
                this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;
                if (!this.GyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                {
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.GyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.Value.ToString();
                }
                this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                if (!this.GyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.GyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN.Value.ToString();
                }
                this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                if (!this.GyoushaEntity.SEIKYUU_KYOTEN_CD.IsNull)
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.GyoushaEntity.SEIKYUU_KYOTEN_CD.Value.ToString()));
                    M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                    if (kyo != null)
                    {
                        this.form.SEIKYUU_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                    }
                }

                // Begin: LANDUONG - 20220214 - refs#160052
                this.form.HAKKOUSAKI_CD.Text = this.GyoushaEntity.HAKKOUSAKI_CD;
                this.form.RAKURAKU_CUSTOMER_CD.Text = this.GyoushaEntity.RAKURAKU_CUSTOMER_CD;                
                HakkousakuAndRakurakuCDCheck();
                // End: LANDUONG - 20220214 - refs#160052

                Boolean isSeikyuuKake = true;
                if (this.torihikisakiEntity != null)
                {
                    M_TORIHIKISAKI_SEIKYUU seikyuuEntity = this.daoSeikyuu.GetDataByCd(this.torihikisakiEntity.TORIHIKISAKI_CD);

                    if (seikyuuEntity != null)
                    {
                        // 取引先区分が[1.現金]時には[請求情報タブ]内部を非活性
                        if (seikyuuEntity.TORIHIKI_KBN_CD == 1)
                        {
                            isSeikyuuKake = false;
                        }
                        else if (seikyuuEntity.TORIHIKI_KBN_CD == 2)
                        {
                            isSeikyuuKake = true;
                        }
                    }

                    //非活性⇔活性
                    if ((!isSeikyuuKake && this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled) || (isSeikyuuKake && !this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled))
                    {
                        this.ChangeTorihikisakiKbn(Const.GyoushaHoshuConstans.TorihikisakiKbnProcessType.Seikyuu, isSeikyuuKake);
                    }
                }

                // 支払情報
                this.form.SHIHARAI_SOUFU_NAME1.Text = this.GyoushaEntity.SHIHARAI_SOUFU_NAME1;
                this.form.SHIHARAI_SOUFU_NAME2.Text = this.GyoushaEntity.SHIHARAI_SOUFU_NAME2;
                this.form.SHIHARAI_SOUFU_KEISHOU1.Text = this.GyoushaEntity.SHIHARAI_SOUFU_KEISHOU1;
                this.form.SHIHARAI_SOUFU_KEISHOU2.Text = this.GyoushaEntity.SHIHARAI_SOUFU_KEISHOU2;
                this.form.SHIHARAI_SOUFU_POST.Text = this.GyoushaEntity.SHIHARAI_SOUFU_POST;
                this.form.SHIHARAI_SOUFU_ADDRESS1.Text = this.GyoushaEntity.SHIHARAI_SOUFU_ADDRESS1;
                this.form.SHIHARAI_SOUFU_ADDRESS2.Text = this.GyoushaEntity.SHIHARAI_SOUFU_ADDRESS2;
                this.form.SHIHARAI_SOUFU_BUSHO.Text = this.GyoushaEntity.SHIHARAI_SOUFU_BUSHO;
                this.form.SHIHARAI_SOUFU_TANTOU.Text = this.GyoushaEntity.SHIHARAI_SOUFU_TANTOU;
                this.form.SHIHARAI_SOUFU_TEL.Text = this.GyoushaEntity.SHIHARAI_SOUFU_TEL;
                this.form.SHIHARAI_SOUFU_FAX.Text = this.GyoushaEntity.SHIHARAI_SOUFU_FAX;
                this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                if (!this.GyoushaEntity.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.GyoushaEntity.SHIHARAI_KYOTEN_PRINT_KBN.Value.ToString();
                }
                this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                if (!this.GyoushaEntity.SHIHARAI_KYOTEN_CD.IsNull)
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.GyoushaEntity.SHIHARAI_KYOTEN_CD.Value.ToString()));
                    M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                    if (kyo != null)
                    {
                        this.form.SHIHARAI_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                    }
                }

                Boolean isShiharaiKake = true;
                if (this.torihikisakiEntity != null)
                {
                    M_TORIHIKISAKI_SHIHARAI shiharaiEntity = this.daoShiharai.GetDataByCd(this.torihikisakiEntity.TORIHIKISAKI_CD);

                    if (shiharaiEntity != null)
                    {
                        // 取引先区分が[1.現金]時には[請求情報タブ]内部を非活性
                        if (shiharaiEntity.TORIHIKI_KBN_CD == 1)
                        {
                            isShiharaiKake = false;
                        }
                        else if (shiharaiEntity.TORIHIKI_KBN_CD == 2)
                        {
                            isShiharaiKake = true;
                        }
                    }

                    //非活性⇔活性
                    if ((!isShiharaiKake && this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled) || (isShiharaiKake && !this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled))
                    {
                        this.ChangeTorihikisakiKbn(Const.GyoushaHoshuConstans.TorihikisakiKbnProcessType.Siharai, isShiharaiKake);
                    }
                }
            }

            this.rowCntGenba = this.SearchGenbaIchiran();

            if (this.rowCntGenba > 0)
            {
                this.SetIchiranGenba();
            }

            // 業者分類
            if (!this.GyoushaEntity.JISHA_KBN.IsNull)
            {
                this.form.JISHA_KBN.Checked = (bool)this.GyoushaEntity.JISHA_KBN;
            }
            else
            {
                //this.form.JISHA_KBN.Checked = false;
            }
            if (!this.GyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsNull)
            {
                this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = (bool)this.GyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN;
            }
            else
            {
                this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = false;
            }
            if (!this.GyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN.IsNull)
            {
                this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = (bool)this.GyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN;
            }
            else
            {
                this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = false;
            }
            if (!this.GyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsNull)
            {
                this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = (bool)this.GyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN;
            }
            else
            {
                this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = false;
            }
            if (!this.GyoushaEntity.MANI_HENSOUSAKI_KBN.IsNull)
            {
                this.form.MANI_HENSOUSAKI_KBN.Checked = (bool)this.GyoushaEntity.MANI_HENSOUSAKI_KBN;
            }
            else
            {
                this.form.MANI_HENSOUSAKI_KBN.Checked = false;
            }

            //if (!this.form.Gyousha_KBN_3.Checked)
            //{
            //    //this.form.JISHA_KBN.Enabled = false;
            //    this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Enabled = false;
            //    this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Enabled = false;
            //    this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Enabled = false;
            //}
            //else
            //{
            //    //this.form.JISHA_KBN.Enabled = true;
            //    this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Enabled = true;
            //    this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Enabled = true;
            //    this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Enabled = true;
            //}
            if (this.GyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.IsNull)
            {
                this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "";
            }
            else
            {
                this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = Convert.ToString(this.GyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Value);
                if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text == "1")
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = true;
                }
                else
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                }
            }
            if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked)
            {
                this.form.MANI_HENSOUSAKI_KEISHOU1.Text = this.GyoushaEntity.MANI_HENSOUSAKI_KEISHOU1;
                this.form.MANI_HENSOUSAKI_KEISHOU2.Text = this.GyoushaEntity.MANI_HENSOUSAKI_KEISHOU2;
                this.form.MANI_HENSOUSAKI_NAME1.Text = this.GyoushaEntity.MANI_HENSOUSAKI_NAME1;
                this.form.MANI_HENSOUSAKI_NAME2.Text = this.GyoushaEntity.MANI_HENSOUSAKI_NAME2;
                this.form.MANI_HENSOUSAKI_POST.Text = this.GyoushaEntity.MANI_HENSOUSAKI_POST;
                this.form.MANI_HENSOUSAKI_ADDRESS1.Text = this.GyoushaEntity.MANI_HENSOUSAKI_ADDRESS1;
                this.form.MANI_HENSOUSAKI_ADDRESS2.Text = this.GyoushaEntity.MANI_HENSOUSAKI_ADDRESS2;
                this.form.MANI_HENSOUSAKI_BUSHO.Text = this.GyoushaEntity.MANI_HENSOUSAKI_BUSHO;
                this.form.MANI_HENSOUSAKI_TANTOU.Text = this.GyoushaEntity.MANI_HENSOUSAKI_TANTOU;
            }
            else
            {
                this.form.MANI_HENSOUSAKI_KEISHOU1.Enabled = false;
                this.form.MANI_HENSOUSAKI_KEISHOU2.Enabled = false;
                this.form.MANI_HENSOUSAKI_NAME1.Enabled = false;
                this.form.MANI_HENSOUSAKI_NAME2.Enabled = false;
                this.form.MANI_HENSOUSAKI_POST.Enabled = false;
                this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = false;
                this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = false;
                this.form.MANI_HENSOUSAKI_BUSHO.Enabled = false;
                this.form.MANI_HENSOUSAKI_TANTOU.Enabled = false;
            }

            // 20141208 ブン 運搬報告書提出を追加する start
            this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = this.GyoushaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD;
            this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = (this.upnHoukokushoTeishutsuChiikiEntity == null) ? string.Empty : this.upnHoukokushoTeishutsuChiikiEntity.CHIIKI_NAME_RYAKU;
            // 20141208 ブン 運搬報告書提出を追加する end
            if (!this.GyoushaEntity.TASYA_EDI.IsNull)
            {
                this.form.TASYA_EDI.Text = Convert.ToString(this.GyoushaEntity.TASYA_EDI.Value);
            }
            else
            { 
                this.form.TASYA_EDI.Text = string.Empty; 
            }

            // 委託契約情報
            this.rowCntItaku = this.SearchItaku();
            if (this.rowCntItaku > 0)
            {
                this.SetIchiranItaku();
            }

            // mapbox項目
            this.form.GyoushaLatitude.Text = this.GyoushaEntity.GYOUSHA_LATITUDE;
            this.form.GyoushaLongitude.Text = this.GyoushaEntity.GYOUSHA_LONGITUDE;
            this.form.LocationInfoUpdateName.Text = this.GyoushaEntity.GYOUSHA_LOCATION_INFO_UPDATE_NAME;
            if (!this.GyoushaEntity.GYOUSHA_LOCATION_INFO_UPDATE_DATE.IsNull)
            {
                this.form.LocationInfoUpdateDate.Text = this.GyoushaEntity.GYOUSHA_LOCATION_INFO_UPDATE_DATE.ToString();
            }
        }

        #region 全コントロール制御メソッド

        /// <summary>
        /// 全コントロール制御
        /// </summary>
        /// <param name="isBool">true:操作可、false:操作不可</param>
        private void AllControlLock(bool isBool)
        {
            // 共通部
            this.form.TORIHIKISAKI_UMU_KBN.Enabled = isBool;
            this.form.Torihiki_ari.Enabled = isBool;
            this.form.Torihiki_nashi.Enabled = isBool;
            this.form.Gyousha_KBN_1.Enabled = isBool;
            this.form.Gyousha_KBN_2.Enabled = isBool;
            this.form.Gyousha_KBN_3.Enabled = isBool;

            this.form.TORIHIKISAKI_CD.Enabled = isBool;
            this.form.bt_torihikisaki_copy.Enabled = isBool;
            this.form.bt_torihikisaki_create.Enabled = isBool;
            this.form.BT_TORIHIKISAKI_REFERENCE.Enabled = isBool;
            this.form.bt_torihikisaki_search.Enabled = isBool;
            this.form.KYOTEN_CD.Enabled = isBool;
            this.form.GYOUSHU_CD.Enabled = isBool;
            this.form.bt_gyoushacd_saiban.Enabled = isBool;
            this.form.GYOUSHA_NAME1.Enabled = isBool;
            this.form.GYOUSHA_NAME2.Enabled = isBool;
            this.form.GYOUSHA_FURIGANA.Enabled = isBool;
            this.form.GYOUSHA_NAME_RYAKU.Enabled = isBool;
            this.form.GYOUSHA_KEISHOU1.Enabled = isBool;
            this.form.GYOUSHA_KEISHOU2.Enabled = isBool;
            this.form.GYOUSHA_TEL.Enabled = isBool;
            this.form.GYOUSHA_KEITAI_TEL.Enabled = isBool;
            this.form.GYOUSHA_FAX.Enabled = isBool;
            this.form.EIGYOU_TANTOU_BUSHO_CD.Enabled = isBool;
            this.form.EIGYOU_TANTOU_CD.Enabled = isBool;
            this.form.bt_tantousha_search.Enabled = isBool;
            this.form.bt_tantoubusho_search.Enabled = isBool;
            this.form.TEKIYOU_BEGIN.Enabled = isBool;
            this.form.TEKIYOU_END.Enabled = isBool;
            this.form.CHUUSHI_RIYUU1.Enabled = isBool;
            this.form.CHUUSHI_RIYUU2.Enabled = isBool;
            this.form.SHOKUCHI_KBN.Enabled = isBool;

            // 基本情報
            this.form.GYOUSHA_POST.Enabled = isBool;
            this.form.bt_address.Enabled = isBool;
            this.form.GYOUSHA_TODOUFUKEN_CD.Enabled = isBool;
            this.form.bt_todoufuken_search.Enabled = isBool;
            this.form.bt_post.Enabled = isBool;
            this.form.GYOUSHA_ADDRESS1.Enabled = isBool;
            this.form.GYOUSHA_ADDRESS2.Enabled = isBool;
            this.form.CHIIKI_CD.Enabled = isBool;
            this.form.bt_chiiki_search.Enabled = isBool;
            this.form.BUSHO.Enabled = isBool;
            this.form.TANTOUSHA.Enabled = isBool;
            this.form.GYOUSHA_DAIHYOU.Enabled = isBool;
            this.form.SHUUKEI_ITEM_CD.Enabled = isBool;
            this.form.bt_syuukeiitem_search.Enabled = isBool;
            this.form.GYOUSHA_CD.Enabled = isBool;
            this.form.bt_gyoushu_search.Enabled = isBool;
            this.form.BIKOU1.Enabled = isBool;
            this.form.BIKOU2.Enabled = isBool;
            this.form.BIKOU3.Enabled = isBool;
            this.form.BIKOU4.Enabled = isBool;

            //20250320
            this.form.URIAGE_GURUPU_CD.Enabled = isBool;
            this.form.bt_gurupu_search_1.Enabled = isBool;
            this.form.SHIHARAI_GURUPU_CD.Enabled = isBool;
            this.form.bt_gurupu_search_2.Enabled = isBool;

            // 請求情報
            this.form.SEIKYUU_SOUFU_NAME1.Enabled = isBool;
            this.form.SEIKYUU_SOUFU_NAME2.Enabled = isBool;
            this.form.SEIKYUU_SOUFU_KEISHOU1.Enabled = isBool;
            this.form.SEIKYUU_SOUFU_KEISHOU2.Enabled = isBool;
            this.form.SEIKYUU_SOUFU_POST.Enabled = isBool;
            this.form.bt_souhusaki_torihikisaki_copy.Enabled = isBool;
            this.form.bt_souhusaki_address.Enabled = isBool;
            this.form.bt_souhusaki_post.Enabled = isBool;
            this.form.SEIKYUU_SOUFU_ADDRESS1.Enabled = isBool;
            this.form.SEIKYUU_SOUFU_ADDRESS2.Enabled = isBool;
            this.form.SEIKYUU_SOUFU_BUSHO.Enabled = isBool;
            this.form.SEIKYUU_SOUFU_TANTOU.Enabled = isBool;
            this.form.SEIKYUU_SOUFU_TEL.Enabled = isBool;
            this.form.SEIKYUU_SOUFU_FAX.Enabled = isBool;
            this.form.SEIKYUU_TANTOU.Enabled = isBool;
            this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Enabled = isBool;
            this.form.SEIKYUU_DAIHYOU_PRINT_KBN_1.Enabled = isBool;
            this.form.SEIKYUU_DAIHYOU_PRINT_KBN_2.Enabled = isBool;
            this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled = isBool;
            this.form.SEIKYUU_KYOTEN_PRINT_KBN_1.Enabled = isBool;
            this.form.SEIKYUU_KYOTEN_PRINT_KBN_2.Enabled = isBool;
            this.form.SEIKYUU_KYOTEN_CD.Enabled = isBool;
            this.form.SEIKYUU_KYOTEN_CD_SEARCH.Enabled = isBool;
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            this.form.HAKKOUSAKI_CD.Enabled = isBool;
            // 20160429 koukoukon v2.1_電子請求書 #16612 end
            this.form.bt_seikyuu_torihikisaki_copy.Enabled = isBool;

            // Begin: LANDUONG - 20220214 - refs#160052
            this.form.RAKURAKU_CUSTOMER_CD.Enabled = isBool;
            this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = isBool;
            // Begin: LANDUONG - 20220214 - refs#160052

            // 支払情報
            this.form.SHIHARAI_SOUFU_NAME1.Enabled = isBool;
            this.form.SHIHARAI_SOUFU_NAME2.Enabled = isBool;
            this.form.SHIHARAI_SOUFU_KEISHOU1.Enabled = isBool;
            this.form.SHIHARAI_SOUFU_KEISHOU2.Enabled = isBool;
            this.form.SHIHARAI_SOUFU_POST.Enabled = isBool;
            this.form.bt_shiharai_souhusaki_torihikisaki_copy.Enabled = isBool;
            this.form.bt_shiharai_address.Enabled = isBool;
            this.form.bt_shiharai_post.Enabled = isBool;
            this.form.SHIHARAI_SOUFU_ADDRESS1.Enabled = isBool;
            this.form.SHIHARAI_SOUFU_ADDRESS2.Enabled = isBool;
            this.form.SHIHARAI_SOUFU_BUSHO.Enabled = isBool;
            this.form.SHIHARAI_SOUFU_TANTOU.Enabled = isBool;
            this.form.SHIHARAI_SOUFU_TEL.Enabled = isBool;
            this.form.SHIHARAI_SOUFU_FAX.Enabled = isBool;
            this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled = isBool;
            this.form.SHIHARAI_KYOTEN_PRINT_KBN_1.Enabled = isBool;
            this.form.SHIHARAI_KYOTEN_PRINT_KBN_2.Enabled = isBool;
            this.form.SHIHARAI_KYOTEN_CD.Enabled = isBool;
            this.form.SHIHARAI_KYOTEN_CD_SEARCH.Enabled = isBool;
            this.form.bt_shiharai_torihikisaki_copy.Enabled = isBool;

            // 業者分類
            this.form.JISHA_KBN.Enabled = isBool;
            this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Enabled = isBool;
            this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Enabled = isBool;
            this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Enabled = isBool;
            this.form.MANI_HENSOUSAKI_KBN.Enabled = isBool;
            this.form.MANI_HENSOUSAKI_KEISHOU1.Enabled = isBool;
            this.form.MANI_HENSOUSAKI_KEISHOU2.Enabled = isBool;
            this.form.MANI_HENSOUSAKI_NAME1.Enabled = isBool;
            this.form.MANI_HENSOUSAKI_NAME2.Enabled = isBool;
            this.form.MANI_HENSOUSAKI_POST.Enabled = isBool;
            this.form.bt_hensousaki_torihikisaki_copy.Enabled = isBool;
            this.form.bt_hensousaki_address.Enabled = isBool;
            this.form.bt_hensousaki_post.Enabled = isBool;
            this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = isBool;
            this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = isBool;
            this.form.MANI_HENSOUSAKI_BUSHO.Enabled = isBool;
            this.form.MANI_HENSOUSAKI_TANTOU.Enabled = isBool;
            this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Enabled = isBool;
            this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Enabled = isBool;
            this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Enabled = isBool;
            this.form.TASYA_EDI.Enabled = isBool;

            // 20141208 ブン 運搬報告書提出先を追加する start
            this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Enabled = isBool;
            this.form.bt_upn_houkokusho_teishutsu_chiiki_search.Enabled = isBool;
            // 20141208 ブン 運搬報告書提出先を追加する end

            // 委託契約情報
            this.form.ITAKU_ICHIRAN.Enabled = isBool;

            // 地図連携情報タブ
            this.form.GyoushaLatitude.Enabled = isBool;
            this.form.GyoushaLongitude.Enabled = isBool;
            this.form.bt_map_open.Enabled = isBool;
        }

        #endregion 全コントロール制御メソッド

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit(BusinessBaseForm parentForm)
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// サーチ
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            return 0;
        }

        /// <summary>
        /// 業者CDと現場CDで現場マスタを取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>件数</returns>
        public int SearchGenba(string gyoushaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            var ret = 0;
            this.genbaEntity = daoGenba.GetDataByCd(new M_GENBA() { GYOUSHA_CD = gyoushaCd, GENBA_CD = genbaCd });
            if (null != this.genbaEntity)
            {
                ret = 1;
            }

            LogUtility.DebugMethodEnd();

            return ret;
        }

        /// <summary>
        /// データ取得処理(業者)
        /// </summary>
        /// <returns></returns>
        public int SearchGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.GyoushaEntity = null;

                this.GyoushaEntity = daoGyousha.GetDataByCd(this.GyoushaCd);

                int count = this.GyoushaEntity == null ? 0 : 1;

                LogUtility.DebugMethodEnd(count);
                return count;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchGyousha", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGyousha", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
        }

        /// <summary>
        /// データ取得処理(取引先)
        /// </summary>
        /// <returns></returns>
        public int SearchTorihikisaki()
        {
            LogUtility.DebugMethodStart();

            this.torihikisakiEntity = null;

            this.torihikisakiEntity = daoTorisaki.GetDataByCd(this.GyoushaEntity.TORIHIKISAKI_CD);

            int count = this.torihikisakiEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(拠点)
        /// </summary>
        /// <returns></returns>
        public int SearchKyoten()
        {
            LogUtility.DebugMethodStart();

            this.kyotenEntity = null;

            if (this.torihikisakiEntity != null && !this.torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
            {
                this.kyotenEntity = daoKyoten.GetDataByCd(this.torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString());
            }

            int count = this.kyotenEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(部署)
        /// </summary>
        /// <returns></returns>
        public int SearchBusho()
        {
            LogUtility.DebugMethodStart();

            this.bushoEntity = null;

            this.bushoEntity = daoBusho.GetDataByCd(this.GyoushaEntity.EIGYOU_TANTOU_BUSHO_CD);

            int count = this.bushoEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(社員)
        /// </summary>
        /// <returns></returns>
        public int SearchShain()
        {
            LogUtility.DebugMethodStart();

            this.shainEntity = null;

            this.shainEntity = daoShain.GetDataByCd(this.GyoushaEntity.EIGYOU_TANTOU_CD);

            int count = this.shainEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(都道府県)
        /// </summary>
        /// <returns></returns>
        public int SearchTodoufuken()
        {
            LogUtility.DebugMethodStart();

            this.todoufukenEntity = null;

            if (!this.GyoushaEntity.GYOUSHA_TODOUFUKEN_CD.IsNull)
            {
                this.todoufukenEntity = daoTodoufuken.GetDataByCd(this.GyoushaEntity.GYOUSHA_TODOUFUKEN_CD.Value.ToString());
            }
            else
            {
                this.todoufukenEntity = null;
            }

            int count = this.todoufukenEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(地域)
        /// </summary>
        /// <returns></returns>
        public int SearchChiiki()
        {
            LogUtility.DebugMethodStart();

            this.chiikiEntity = null;

            this.chiikiEntity = daoChiiki.GetDataByCd(this.GyoushaEntity.CHIIKI_CD);

            int count = this.chiikiEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(集計項目)
        /// </summary>
        /// <returns></returns>
        public int SearchShuukeiItem()
        {
            LogUtility.DebugMethodStart();

            this.shuukeiEntity = null;

            this.shuukeiEntity = daoShuukei.GetDataByCd(this.GyoushaEntity.SHUUKEI_ITEM_CD);

            int count = this.shuukeiEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(業種)
        /// </summary>
        /// <returns></returns>
        public int SearchGyoushu()
        {
            LogUtility.DebugMethodStart();

            this.gyoushuEntity = null;

            this.gyoushuEntity = daoGyoushu.GetDataByCd(this.GyoushaEntity.GYOUSHU_CD);

            int count = this.gyoushuEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        //20250319
        public int SearchUriageGurupu()
        {
            LogUtility.DebugMethodStart();

            this.gurupuEntity = null;

            this.gurupuEntity = daoGurupu.GetDataByCdAndDencd(this.GyoushaEntity.URIAGE_GURUPU_CD, 1);

            int count = this.gurupuEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        //20250320
        public int SearchShiharaiGurupu()
        {
            LogUtility.DebugMethodStart();

            this.gurupuEntity1 = null;

            this.gurupuEntity1 = daoGurupu.GetDataByCdAndDencd(this.GyoushaEntity.SHIHARAI_GURUPU_CD, 2);

            int count = this.gurupuEntity1 == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(現場一覧)
        /// </summary>
        /// <returns></returns>
        public int SearchGenbaIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.SearchResultGenba = new DataTable();

                string gyoushaCd = this.GyoushaEntity.GYOUSHA_CD;
                if (string.IsNullOrWhiteSpace(gyoushaCd))
                {
                    return 0;
                }

                M_GENBA condition = new M_GENBA();
                condition.GYOUSHA_CD = gyoushaCd;

                if (this.IsFromShouninzumiDenshiShinseiIchiran)
                {
                    // 何もしない(本登録時は引合現場の情報は表示しない)
                }
                else
                {
                    this.SearchResultGenba = daoGenba.GetDataBySqlFile(this.GET_ICHIRAN_GENBA_DATA_SQL, condition);
                }

                int count = this.SearchResultGenba.Rows.Count;

                LogUtility.DebugMethodEnd(count);
                return count;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchGenbaIchiran", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGenbaIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
        }

        /// <summary>
        /// データ取得処理(引合現場一覧)
        /// </summary>
        /// <returns></returns>
        public int SearchHikiaiGenbaIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.SearchResultGenba = new DataTable();
                int count = 0;

                string gyoushaCd = this.GyoushaEntity.GYOUSHA_CD;
                if (string.IsNullOrWhiteSpace(gyoushaCd))
                {
                    // 業者CDがなければ中断
                    return count;
                }

                // 業者CDで引合現場を参照
                M_HIKIAI_GENBA[] hikiaiGenba = daoHikiaiGenba.GetHikiaiGenbaJisha(gyoushaCd);
                count = hikiaiGenba.Length;

                LogUtility.DebugMethodEnd(count);
                return count;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchHikiaiGenbaIchiran", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchHikiaiGenbaIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
        }

        /// <summary>
        /// データ取得処理(委託)
        /// </summary>
        /// <returns></returns>
        public int SearchItaku()
        {
            LogUtility.DebugMethodStart();

            this.SearchResultItaku = new DataTable();

            string gyoushaCd = this.GyoushaEntity.GYOUSHA_CD;
            if (string.IsNullOrWhiteSpace(gyoushaCd))
            {
                return 0;
            }

            M_ITAKU_KEIYAKU_KIHON condition = new M_ITAKU_KEIYAKU_KIHON();
            condition.HAISHUTSU_JIGYOUSHA_CD = gyoushaCd;

            this.SearchResultItaku = daoItaku.GetDataBySqlFile(this.GET_ICHIRAN_ITAKU_DATA_SQL, condition);

            int count = this.SearchResultItaku.Rows.Count;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    this.TorihikisakiCd = string.Empty;
                    this.RenkeiFlg = false;
                    // 取引先連携時に取引先情報コピーを防ぐためフラグを設定
                    this.isNotTorihikisakiCopy = true;

                    // 新規モードの場合は空画面を表示する
                    this.WindowInitNewMode(parentForm);

                    this.isNotTorihikisakiCopy = false;
                }
                else
                {
                    // 入金先入力画面表示時の入金先CDで再検索・再表示
                    this.SetWindowData();
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 業者CD重複チェック
        /// </summary>
        /// <param name="zeroPadCd">CD</param>
        /// <param name="isRegister">登録中か判断します</param>
        /// <returns></returns>
        public GyoushaHoshuConstans.GyoushaCdLeaveResult DupliCheckGyoushaCd(string zeroPadCd, bool isRegister)
        {
            LogUtility.DebugMethodStart(zeroPadCd, isRegister);

            // 業者マスタ検索
            M_GYOUSHA gyoushaEntity = this.daoGyousha.GetDataByCd(zeroPadCd);

            // 重複チェック
            GyoushaHoshuValidator vali = new GyoushaHoshuValidator();
            DialogResult resultDialog = new DialogResult();
            bool resultDupli = vali.GyoushaCDValidator(gyoushaEntity, isRegister, out resultDialog);

            GyoushaHoshuConstans.GyoushaCdLeaveResult ViewUpdateWindow = 0;

            // 重複チェックの結果と、ポップアップの結果で動作を変える
            if (!resultDupli && resultDialog == DialogResult.OK)
            {
                ViewUpdateWindow = GyoushaHoshuConstans.GyoushaCdLeaveResult.FALSE_OFF;
            }
            else if (!resultDupli && resultDialog == DialogResult.Yes)
            {
                ViewUpdateWindow = GyoushaHoshuConstans.GyoushaCdLeaveResult.FALSE_ON;
            }
            else if (!resultDupli && resultDialog == DialogResult.No)
            {
                ViewUpdateWindow = GyoushaHoshuConstans.GyoushaCdLeaveResult.FALSE_OFF;
            }
            else if (!resultDupli)
            {
                ViewUpdateWindow = GyoushaHoshuConstans.GyoushaCdLeaveResult.FALSE_NONE;
            }
            else
            {
                ViewUpdateWindow = GyoushaHoshuConstans.GyoushaCdLeaveResult.TURE_NONE;
            }

            LogUtility.DebugMethodEnd();

            return ViewUpdateWindow;
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        /// <param name="isDelete"></param>
        public bool CreateGyoushaEntity(bool isDelete)
        {
            try
            {
                LogUtility.DebugMethodStart();

                DateTime timeBegin = new DateTime();
                DateTime timeEnd = new DateTime();

                // 20151014 委託契約書入力(M001)システムエラー発生関連チケット Start
                //          TIME_STAMP設定部分修正方法更新
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG ||
                    this.GyoushaEntity == null ||
                    this.GyoushaEntity.TIME_STAMP == null || this.GyoushaEntity.TIME_STAMP.Length != 8)
                {
                    this.GyoushaEntity = new M_GYOUSHA();
                }
                else
                {
                    this.GyoushaEntity = new M_GYOUSHA() { TIME_STAMP = this.GyoushaEntity.TIME_STAMP };
                }
                // 20151014 委託契約書入力(M001)システムエラー発生関連チケット End
                // 現在の入力内容でEntity作成
                this.GyoushaEntity.BIKOU1 = this.form.BIKOU1.Text;
                this.GyoushaEntity.BIKOU2 = this.form.BIKOU2.Text;
                this.GyoushaEntity.BIKOU3 = this.form.BIKOU3.Text;
                this.GyoushaEntity.BIKOU4 = this.form.BIKOU4.Text;
                this.GyoushaEntity.BUSHO = this.form.BUSHO.Text;
                this.GyoushaEntity.CHIIKI_CD = this.form.CHIIKI_CD.Text;
                this.GyoushaEntity.CHUUSHI_RIYUU1 = this.form.CHUUSHI_RIYUU1.Text;
                this.GyoushaEntity.CHUUSHI_RIYUU2 = this.form.CHUUSHI_RIYUU2.Text;
                this.GyoushaEntity.EIGYOU_TANTOU_BUSHO_CD = this.form.EIGYOU_TANTOU_BUSHO_CD.Text;
                this.GyoushaEntity.EIGYOU_TANTOU_CD = this.form.EIGYOU_TANTOU_CD.Text;
                this.GyoushaEntity.GYOUSHA_DAIHYOU = this.form.GYOUSHA_DAIHYOU.Text;
                this.GyoushaEntity.GYOUSHA_ADDRESS1 = this.form.GYOUSHA_ADDRESS1.Text;
                this.GyoushaEntity.GYOUSHA_ADDRESS2 = this.form.GYOUSHA_ADDRESS2.Text;
                this.GyoushaEntity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                this.GyoushaEntity.GYOUSHA_FAX = this.form.GYOUSHA_FAX.Text;
                this.GyoushaEntity.GYOUSHA_FURIGANA = this.form.GYOUSHA_FURIGANA.Text;
                this.GyoushaEntity.GYOUSHA_KEISHOU1 = this.form.GYOUSHA_KEISHOU1.Text;
                this.GyoushaEntity.GYOUSHA_KEISHOU2 = this.form.GYOUSHA_KEISHOU2.Text;
                this.GyoushaEntity.GYOUSHA_KEITAI_TEL = this.form.GYOUSHA_KEITAI_TEL.Text;
                this.GyoushaEntity.GYOUSHA_NAME_RYAKU = this.form.GYOUSHA_NAME_RYAKU.Text;
                this.GyoushaEntity.GYOUSHA_NAME1 = this.form.GYOUSHA_NAME1.Text;
                this.GyoushaEntity.GYOUSHA_NAME2 = this.form.GYOUSHA_NAME2.Text;
                this.GyoushaEntity.GYOUSHA_POST = this.form.GYOUSHA_POST.Text;
                this.GyoushaEntity.GYOUSHA_TEL = this.form.GYOUSHA_TEL.Text;
                if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_TODOUFUKEN_CD.Text))
                {
                    this.GyoushaEntity.GYOUSHA_TODOUFUKEN_CD = Int16.Parse(this.form.GYOUSHA_TODOUFUKEN_CD.Text.ToString());
                }
                this.GyoushaEntity.GYOUSHAKBN_MANI = this.form.Gyousha_KBN_3.Checked;
                this.GyoushaEntity.GYOUSHAKBN_SHUKKA = this.form.Gyousha_KBN_2.Checked;
                this.GyoushaEntity.GYOUSHAKBN_UKEIRE = this.form.Gyousha_KBN_1.Checked;
                this.GyoushaEntity.GYOUSHU_CD = this.form.GYOUSHU_CD.Text;
                this.GyoushaEntity.JISHA_KBN = this.form.JISHA_KBN.Checked;
                this.form.KYOTEN_CD.Text = "99";                                   //強制的に99:全社を登録
                if (!string.IsNullOrWhiteSpace(this.form.KYOTEN_CD.Text))
                {
                    this.GyoushaEntity.KYOTEN_CD = Int16.Parse(this.form.KYOTEN_CD.Text);
                }

                if (this.RenkeiFlg && this.torihikisakiEntity != null)
                {
                    this.GyoushaEntity.MANI_HENSOUSAKI_KBN = this.form.MANI_HENSOUSAKI_KBN.Checked;
                    if (!string.IsNullOrWhiteSpace(this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text))
                    {
                        this.GyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = Int16.Parse(this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text);
                    }
                    else
                    {
                        this.GyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = SqlInt16.Null;
                    }
                    if (this.form.MANI_HENSOUSAKI_KBN.Checked)
                    {
                        if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked)
                        {
                            this.GyoushaEntity.MANI_HENSOUSAKI_NAME1 = this.torihikisakiEntity.MANI_HENSOUSAKI_NAME1;
                            this.GyoushaEntity.MANI_HENSOUSAKI_NAME2 = this.torihikisakiEntity.MANI_HENSOUSAKI_NAME2;
                            this.GyoushaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.torihikisakiEntity.MANI_HENSOUSAKI_KEISHOU1;
                            this.GyoushaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.torihikisakiEntity.MANI_HENSOUSAKI_KEISHOU2;
                            this.GyoushaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.torihikisakiEntity.MANI_HENSOUSAKI_ADDRESS1;
                            this.GyoushaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.torihikisakiEntity.MANI_HENSOUSAKI_ADDRESS2;
                            this.GyoushaEntity.MANI_HENSOUSAKI_POST = this.torihikisakiEntity.MANI_HENSOUSAKI_POST;
                            this.GyoushaEntity.MANI_HENSOUSAKI_BUSHO = this.torihikisakiEntity.MANI_HENSOUSAKI_BUSHO;
                            this.GyoushaEntity.MANI_HENSOUSAKI_TANTOU = this.torihikisakiEntity.MANI_HENSOUSAKI_TANTOU;
                        }
                        else
                        {
                            this.GyoushaEntity.MANI_HENSOUSAKI_NAME1 = this.torihikisakiEntity.TORIHIKISAKI_NAME1;
                            this.GyoushaEntity.MANI_HENSOUSAKI_NAME2 = this.torihikisakiEntity.TORIHIKISAKI_NAME2;
                            this.GyoushaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.torihikisakiEntity.TORIHIKISAKI_KEISHOU1;
                            this.GyoushaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.torihikisakiEntity.TORIHIKISAKI_KEISHOU2;
                            this.GyoushaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.torihikisakiEntity.TORIHIKISAKI_ADDRESS1;
                            this.GyoushaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.torihikisakiEntity.TORIHIKISAKI_ADDRESS2;
                            this.GyoushaEntity.MANI_HENSOUSAKI_POST = this.torihikisakiEntity.TORIHIKISAKI_POST;
                            this.GyoushaEntity.MANI_HENSOUSAKI_BUSHO = this.torihikisakiEntity.BUSHO;
                            this.GyoushaEntity.MANI_HENSOUSAKI_TANTOU = this.torihikisakiEntity.TANTOUSHA;
                        }
                    }
                    else
                    {
                        this.GyoushaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.form.MANI_HENSOUSAKI_ADDRESS1.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.form.MANI_HENSOUSAKI_ADDRESS2.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_BUSHO = this.form.MANI_HENSOUSAKI_BUSHO.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_KBN = this.form.MANI_HENSOUSAKI_KBN.Checked;
                        this.GyoushaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.form.MANI_HENSOUSAKI_KEISHOU1.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.form.MANI_HENSOUSAKI_KEISHOU2.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_NAME1 = this.form.MANI_HENSOUSAKI_NAME1.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_NAME2 = this.form.MANI_HENSOUSAKI_NAME2.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_POST = this.form.MANI_HENSOUSAKI_POST.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_TANTOU = this.form.MANI_HENSOUSAKI_TANTOU.Text;
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text))
                    {
                        this.GyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = Int16.Parse(this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text);
                    }
                    if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked)
                    {
                        this.GyoushaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.form.TODOUFUKEN_NAME.Text + this.form.GYOUSHA_ADDRESS1.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.form.GYOUSHA_ADDRESS2.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_BUSHO = this.form.BUSHO.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_KBN = this.form.MANI_HENSOUSAKI_KBN.Checked;
                        this.GyoushaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.form.GYOUSHA_KEISHOU1.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.form.GYOUSHA_KEISHOU2.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_NAME1 = this.form.GYOUSHA_NAME1.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_NAME2 = this.form.GYOUSHA_NAME2.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_POST = this.form.GYOUSHA_POST.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_TANTOU = this.form.TANTOUSHA.Text;
                    }
                    else
                    {
                        this.GyoushaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.form.MANI_HENSOUSAKI_ADDRESS1.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.form.MANI_HENSOUSAKI_ADDRESS2.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_BUSHO = this.form.MANI_HENSOUSAKI_BUSHO.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_KBN = this.form.MANI_HENSOUSAKI_KBN.Checked;
                        this.GyoushaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.form.MANI_HENSOUSAKI_KEISHOU1.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.form.MANI_HENSOUSAKI_KEISHOU2.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_NAME1 = this.form.MANI_HENSOUSAKI_NAME1.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_NAME2 = this.form.MANI_HENSOUSAKI_NAME2.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_POST = this.form.MANI_HENSOUSAKI_POST.Text;
                        this.GyoushaEntity.MANI_HENSOUSAKI_TANTOU = this.form.MANI_HENSOUSAKI_TANTOU.Text;
                    }
                }

                // 20141208 ブン 運搬報告書提出先を追加する start
                this.GyoushaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text;
                // 20141208 ブン 運搬報告書提出先を追加する end

                if (this._tabPageManager.IsVisible(1))
                {
                    this.GyoushaEntity.SEIKYUU_SOUFU_ADDRESS1 = this.form.SEIKYUU_SOUFU_ADDRESS1.Text;
                    this.GyoushaEntity.SEIKYUU_SOUFU_ADDRESS2 = this.form.SEIKYUU_SOUFU_ADDRESS2.Text;
                    this.GyoushaEntity.SEIKYUU_SOUFU_BUSHO = this.form.SEIKYUU_SOUFU_BUSHO.Text;
                    this.GyoushaEntity.SEIKYUU_SOUFU_FAX = this.form.SEIKYUU_SOUFU_FAX.Text;
                    this.GyoushaEntity.SEIKYUU_SOUFU_KEISHOU1 = this.form.SEIKYUU_SOUFU_KEISHOU1.Text;
                    this.GyoushaEntity.SEIKYUU_SOUFU_KEISHOU2 = this.form.SEIKYUU_SOUFU_KEISHOU2.Text;
                    this.GyoushaEntity.SEIKYUU_SOUFU_NAME1 = this.form.SEIKYUU_SOUFU_NAME1.Text;
                    this.GyoushaEntity.SEIKYUU_SOUFU_NAME2 = this.form.SEIKYUU_SOUFU_NAME2.Text;
                    this.GyoushaEntity.SEIKYUU_SOUFU_POST = this.form.SEIKYUU_SOUFU_POST.Text;
                    this.GyoushaEntity.SEIKYUU_SOUFU_TANTOU = this.form.SEIKYUU_SOUFU_TANTOU.Text;
                    this.GyoushaEntity.SEIKYUU_SOUFU_TEL = this.form.SEIKYUU_SOUFU_TEL.Text;
                    this.GyoushaEntity.SEIKYUU_TANTOU = this.form.SEIKYUU_TANTOU.Text;
                    if (this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text.Length > 0)
                    {
                        this.GyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN = Int16.Parse(this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text);
                    }
                    if (this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text.Length > 0)
                    {
                        this.GyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN = Int16.Parse(this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text);
                    }
                    if (this.form.SEIKYUU_KYOTEN_CD.Text.Length > 0)
                    {
                        this.GyoushaEntity.SEIKYUU_KYOTEN_CD = Int16.Parse(this.form.SEIKYUU_KYOTEN_CD.Text);
                    }

                    // Begin: LANDUONG - 20220214 - refs#160052                    
                    this.GyoushaEntity.HAKKOUSAKI_CD = this.form.HAKKOUSAKI_CD.Text;                    
                    this.GyoushaEntity.RAKURAKU_CUSTOMER_CD = this.form.RAKURAKU_CUSTOMER_CD.Text;
                    // End: LANDUONG - 20220214 - refs#160052
                }
                if (this._tabPageManager.IsVisible(2))
                {
                    this.GyoushaEntity.SHIHARAI_SOUFU_ADDRESS1 = this.form.SHIHARAI_SOUFU_ADDRESS1.Text;
                    this.GyoushaEntity.SHIHARAI_SOUFU_ADDRESS2 = this.form.SHIHARAI_SOUFU_ADDRESS2.Text;
                    this.GyoushaEntity.SHIHARAI_SOUFU_BUSHO = this.form.SHIHARAI_SOUFU_BUSHO.Text;
                    this.GyoushaEntity.SHIHARAI_SOUFU_FAX = this.form.SHIHARAI_SOUFU_FAX.Text;
                    this.GyoushaEntity.SHIHARAI_SOUFU_KEISHOU1 = this.form.SHIHARAI_SOUFU_KEISHOU1.Text;
                    this.GyoushaEntity.SHIHARAI_SOUFU_KEISHOU2 = this.form.SHIHARAI_SOUFU_KEISHOU2.Text;
                    this.GyoushaEntity.SHIHARAI_SOUFU_NAME1 = this.form.SHIHARAI_SOUFU_NAME1.Text;
                    this.GyoushaEntity.SHIHARAI_SOUFU_NAME2 = this.form.SHIHARAI_SOUFU_NAME2.Text;
                    this.GyoushaEntity.SHIHARAI_SOUFU_POST = this.form.SHIHARAI_SOUFU_POST.Text;
                    this.GyoushaEntity.SHIHARAI_SOUFU_TANTOU = this.form.SHIHARAI_SOUFU_TANTOU.Text;
                    this.GyoushaEntity.SHIHARAI_SOUFU_TEL = this.form.SHIHARAI_SOUFU_TEL.Text;
                    if (this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text.Length > 0)
                    {
                        this.GyoushaEntity.SHIHARAI_KYOTEN_PRINT_KBN = Int16.Parse(this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text);
                    }
                    if (this.form.SHIHARAI_KYOTEN_CD.Text.Length > 0)
                    {
                        this.GyoushaEntity.SHIHARAI_KYOTEN_CD = Int16.Parse(this.form.SHIHARAI_KYOTEN_CD.Text);
                    }
                }
                this.GyoushaEntity.SHOKUCHI_KBN = this.form.SHOKUCHI_KBN.Checked;
                this.GyoushaEntity.SHUUKEI_ITEM_CD = this.form.SHUUKEI_ITEM_CD.Text;
                this.GyoushaEntity.TANTOUSHA = this.form.TANTOUSHA.Text;
                if (this.form.TEKIYOU_BEGIN.Value != null)
                {
                    DateTime.TryParse(this.form.TEKIYOU_BEGIN.Value.ToString(), out timeBegin);
                    this.GyoushaEntity.TEKIYOU_BEGIN = timeBegin;
                }
                if (this.form.TEKIYOU_END.Value != null)
                {
                    DateTime.TryParse(this.form.TEKIYOU_END.Value.ToString(), out timeEnd);
                    this.GyoushaEntity.TEKIYOU_END = timeEnd;
                }
                this.GyoushaEntity.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                this.GyoushaEntity.TORIHIKISAKI_UMU_KBN = Int16.Parse(this.form.TORIHIKISAKI_UMU_KBN.Text);

                // 20151020 BUNN #12040 STR
                // 排出事業者/荷積業者区分
                if (this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked)
                {
                    this.GyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN = true;
                }
                else
                {
                    this.GyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN = false;
                }
                // 運搬受託者/運搬会社区分
                if (this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked)
                {
                    this.GyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN = true;
                }
                else
                {
                    this.GyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN = false;
                }
                // 処分受託者/荷降業者区分
                if (this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked)
                {
                    this.GyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN = true;
                }
                else
                {
                    this.GyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN = false;
                }
                // 20151020 BUNN #12040 END
                if (string.IsNullOrEmpty(this.form.TASYA_EDI.Text))
                {
                    if (this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked)
                    {
                        this.GyoushaEntity.TASYA_EDI = 2;
                    }
                    else
                    {
                        this.GyoushaEntity.TASYA_EDI = SqlInt16.Null;
                    }
                }
                else
                {
                    this.GyoushaEntity.TASYA_EDI = Int16.Parse(this.form.TASYA_EDI.Text);
                }

                // mapbox情報
                this.GyoushaEntity.GYOUSHA_LATITUDE = this.form.GyoushaLatitude.Text;
                this.GyoushaEntity.GYOUSHA_LONGITUDE = this.form.GyoushaLongitude.Text;
                if (string.IsNullOrEmpty(this.form.GyoushaLatitude.Text) &&
                    string.IsNullOrEmpty(this.form.GyoushaLongitude.Text) &&
                    string.IsNullOrEmpty(this.form.LocationInfoUpdateName.Text) &&
                    string.IsNullOrEmpty(this.form.LocationInfoUpdateDate.Text))
                {
                    // 未入力の状態から入力なし、という扱いなのでこの場合のみ更新なし
                }
                else
                {
                    this.GyoushaEntity.GYOUSHA_LOCATION_INFO_UPDATE_NAME = SystemProperty.UserName;
                    this.GyoushaEntity.GYOUSHA_LOCATION_INFO_UPDATE_DATE = this.sysDate();
                }

                //20250320
                this.GyoushaEntity.URIAGE_GURUPU_CD = this.form.URIAGE_GURUPU_CD.Text;
                this.GyoushaEntity.SHIHARAI_GURUPU_CD = this.form.SHIHARAI_GURUPU_CD.Text;

                // 更新者情報設定
                var dataBinderLogicNyuukin = new DataBinderLogic<r_framework.Entity.M_GYOUSHA>(this.GyoushaEntity);
                dataBinderLogicNyuukin.SetSystemProperty(this.GyoushaEntity, false);
                MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), this.GyoushaEntity);

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateGyoushaEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 現場エンティティを作成します
        /// </summary>
        internal bool CreateGenbaEntity()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.IsGenbaAdd)
                {
                    this.genbaEntity = new M_GENBA();
                }

                this.genbaEntity.GYOUSHA_CD = this.GyoushaEntity.GYOUSHA_CD;
                this.genbaEntity.GENBA_CD = "000000";
                this.genbaEntity.TORIHIKISAKI_CD = this.GyoushaEntity.TORIHIKISAKI_CD;
                this.genbaEntity.KYOTEN_CD = this.GyoushaEntity.KYOTEN_CD;
                this.genbaEntity.GENBA_NAME1 = this.GyoushaEntity.GYOUSHA_NAME1;
                this.genbaEntity.GENBA_NAME2 = this.GyoushaEntity.GYOUSHA_NAME2;
                this.genbaEntity.GENBA_NAME_RYAKU = this.GyoushaEntity.GYOUSHA_NAME_RYAKU;
                this.genbaEntity.GENBA_FURIGANA = this.GyoushaEntity.GYOUSHA_FURIGANA;
                this.genbaEntity.GENBA_TEL = this.GyoushaEntity.GYOUSHA_TEL;
                this.genbaEntity.GENBA_FAX = this.GyoushaEntity.GYOUSHA_FAX;
                this.genbaEntity.GENBA_KEITAI_TEL = this.GyoushaEntity.GYOUSHA_KEITAI_TEL;
                this.genbaEntity.GENBA_KEISHOU1 = this.GyoushaEntity.GYOUSHA_KEISHOU1;
                this.genbaEntity.GENBA_KEISHOU2 = this.GyoushaEntity.GYOUSHA_KEISHOU2;
                this.genbaEntity.EIGYOU_TANTOU_BUSHO_CD = this.GyoushaEntity.EIGYOU_TANTOU_BUSHO_CD;
                this.genbaEntity.EIGYOU_TANTOU_CD = this.GyoushaEntity.EIGYOU_TANTOU_CD;
                this.genbaEntity.GENBA_POST = this.GyoushaEntity.GYOUSHA_POST;
                this.genbaEntity.GENBA_TODOUFUKEN_CD = this.GyoushaEntity.GYOUSHA_TODOUFUKEN_CD;
                this.genbaEntity.GENBA_ADDRESS1 = this.GyoushaEntity.GYOUSHA_ADDRESS1;
                this.genbaEntity.GENBA_ADDRESS2 = this.GyoushaEntity.GYOUSHA_ADDRESS2;
                this.genbaEntity.TORIHIKI_JOUKYOU = this.GyoushaEntity.TORIHIKI_JOUKYOU;
                this.genbaEntity.CHUUSHI_RIYUU1 = this.GyoushaEntity.CHUUSHI_RIYUU1;
                this.genbaEntity.CHUUSHI_RIYUU2 = this.GyoushaEntity.CHUUSHI_RIYUU2;
                this.genbaEntity.BUSHO = this.GyoushaEntity.BUSHO;
                this.genbaEntity.TANTOUSHA = this.GyoushaEntity.TANTOUSHA;
                this.genbaEntity.SHUUKEI_ITEM_CD = this.GyoushaEntity.SHUUKEI_ITEM_CD;
                this.genbaEntity.GYOUSHU_CD = this.GyoushaEntity.GYOUSHU_CD;
                this.genbaEntity.CHIIKI_CD = this.GyoushaEntity.CHIIKI_CD;
                this.genbaEntity.BIKOU1 = this.GyoushaEntity.BIKOU1;
                this.genbaEntity.BIKOU2 = this.GyoushaEntity.BIKOU2;
                this.genbaEntity.BIKOU3 = this.GyoushaEntity.BIKOU3;
                this.genbaEntity.BIKOU4 = this.GyoushaEntity.BIKOU4;

                //20250320
                this.genbaEntity.URIAGE_GURUPU_CD = this.GyoushaEntity.URIAGE_GURUPU_CD;
                this.genbaEntity.SHIHARAI_GURUPU_CD = this.GyoushaEntity.SHIHARAI_GURUPU_CD;

                this.genbaEntity.SEIKYUU_SOUFU_NAME1 = this.GyoushaEntity.SEIKYUU_SOUFU_NAME1;
                this.genbaEntity.SEIKYUU_SOUFU_NAME2 = this.GyoushaEntity.SEIKYUU_SOUFU_NAME2;
                this.genbaEntity.SEIKYUU_SOUFU_KEISHOU1 = this.GyoushaEntity.SEIKYUU_SOUFU_KEISHOU1;
                this.genbaEntity.SEIKYUU_SOUFU_KEISHOU2 = this.GyoushaEntity.SEIKYUU_SOUFU_KEISHOU2;
                this.genbaEntity.SEIKYUU_SOUFU_POST = this.GyoushaEntity.SEIKYUU_SOUFU_POST;
                this.genbaEntity.SEIKYUU_SOUFU_ADDRESS1 = this.GyoushaEntity.SEIKYUU_SOUFU_ADDRESS1;
                this.genbaEntity.SEIKYUU_SOUFU_ADDRESS2 = this.GyoushaEntity.SEIKYUU_SOUFU_ADDRESS2;
                this.genbaEntity.SEIKYUU_SOUFU_BUSHO = this.GyoushaEntity.SEIKYUU_SOUFU_BUSHO;
                this.genbaEntity.SEIKYUU_SOUFU_TANTOU = this.GyoushaEntity.SEIKYUU_SOUFU_TANTOU;
                this.genbaEntity.SEIKYUU_SOUFU_TEL = this.GyoushaEntity.SEIKYUU_SOUFU_TEL;
                this.genbaEntity.SEIKYUU_SOUFU_FAX = this.GyoushaEntity.SEIKYUU_SOUFU_FAX;
                this.genbaEntity.SEIKYUU_TANTOU = this.GyoushaEntity.SEIKYUU_TANTOU;
                this.genbaEntity.SEIKYUU_DAIHYOU_PRINT_KBN = this.GyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN;
                this.genbaEntity.SEIKYUU_KYOTEN_PRINT_KBN = this.GyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN;
                this.genbaEntity.SEIKYUU_KYOTEN_CD = this.GyoushaEntity.SEIKYUU_KYOTEN_CD;
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                //this.genbaEntity.HAKKOUSAKI_CD = this.GyoushaEntity.HAKKOUSAKI_CD;
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                this.genbaEntity.SHIHARAI_SOUFU_NAME1 = this.GyoushaEntity.SHIHARAI_SOUFU_NAME1;
                this.genbaEntity.SHIHARAI_SOUFU_NAME2 = this.GyoushaEntity.SHIHARAI_SOUFU_NAME2;
                this.genbaEntity.SHIHARAI_SOUFU_KEISHOU1 = this.GyoushaEntity.SHIHARAI_SOUFU_KEISHOU1;
                this.genbaEntity.SHIHARAI_SOUFU_KEISHOU2 = this.GyoushaEntity.SHIHARAI_SOUFU_KEISHOU2;
                this.genbaEntity.SHIHARAI_SOUFU_POST = this.GyoushaEntity.SHIHARAI_SOUFU_POST;
                this.genbaEntity.SHIHARAI_SOUFU_ADDRESS1 = this.GyoushaEntity.SHIHARAI_SOUFU_ADDRESS1;
                this.genbaEntity.SHIHARAI_SOUFU_ADDRESS2 = this.GyoushaEntity.SHIHARAI_SOUFU_ADDRESS2;
                this.genbaEntity.SHIHARAI_SOUFU_BUSHO = this.GyoushaEntity.SHIHARAI_SOUFU_BUSHO;
                this.genbaEntity.SHIHARAI_SOUFU_TANTOU = this.GyoushaEntity.SHIHARAI_SOUFU_TANTOU;
                this.genbaEntity.SHIHARAI_SOUFU_TEL = this.GyoushaEntity.SHIHARAI_SOUFU_TEL;
                this.genbaEntity.SHIHARAI_SOUFU_FAX = this.GyoushaEntity.SHIHARAI_SOUFU_FAX;
                this.genbaEntity.SHIHARAI_KYOTEN_PRINT_KBN = this.GyoushaEntity.SHIHARAI_KYOTEN_PRINT_KBN;
                this.genbaEntity.SHIHARAI_KYOTEN_CD = this.GyoushaEntity.SHIHARAI_KYOTEN_CD;
                this.genbaEntity.JISHA_KBN = this.GyoushaEntity.JISHA_KBN;
                this.genbaEntity.SHOKUCHI_KBN = this.GyoushaEntity.SHOKUCHI_KBN;
                this.genbaEntity.ITAKU_KEIYAKU_USE_KBN = 1;

                if (this.IsGenbaAdd)
                {
                    this.genbaEntity.TSUMIKAEHOKAN_KBN = false;
                    this.genbaEntity.SAISHUU_SHOBUNJOU_KBN = false;
                    this.genbaEntity.MANI_HENSOUSAKI_KBN = this.GyoushaEntity.MANI_HENSOUSAKI_KBN;
                    this.genbaEntity.MANIFEST_SHURUI_CD = this.sysinfoEntity.GENBA_MANIFEST_SHURUI_CD;
                    this.genbaEntity.MANIFEST_TEHAI_CD = this.sysinfoEntity.GENBA_MANIFEST_TEHAI_CD;
                    this.genbaEntity.DEN_MANI_SHOUKAI_KBN = false;
                    this.genbaEntity.KENSHU_YOUHI = false;
                    // 20151020 BUNN #12040 STR
                    this.genbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN = this.GyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN;
                    this.genbaEntity.SHOBUN_NIOROSHI_GENBA_KBN = this.GyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN;
                    // 20151020 BUNN #12040 END
                }
                if (this.GyoushaEntity.MANI_HENSOUSAKI_KBN.IsTrue)
                {
                    if (this.GyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN == 1)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = 1;
                        this.genbaEntity.MANI_HENSOUSAKI_NAME1 = this.GyoushaEntity.GYOUSHA_NAME1;
                        this.genbaEntity.MANI_HENSOUSAKI_NAME2 = this.GyoushaEntity.GYOUSHA_NAME2;
                        this.genbaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.GyoushaEntity.GYOUSHA_KEISHOU1;
                        this.genbaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.GyoushaEntity.GYOUSHA_KEISHOU2;
                        this.genbaEntity.MANI_HENSOUSAKI_POST = this.GyoushaEntity.GYOUSHA_POST;
                        this.genbaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.GyoushaEntity.MANI_HENSOUSAKI_ADDRESS1;
                        this.genbaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.GyoushaEntity.GYOUSHA_ADDRESS2;
                        this.genbaEntity.MANI_HENSOUSAKI_BUSHO = this.GyoushaEntity.BUSHO;
                        this.genbaEntity.MANI_HENSOUSAKI_TANTOU = this.GyoushaEntity.TANTOUSHA;
                    }
                    else if (this.GyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN == 2)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = 2;
                        this.genbaEntity.MANI_HENSOUSAKI_NAME1 = this.GyoushaEntity.MANI_HENSOUSAKI_NAME1;
                        this.genbaEntity.MANI_HENSOUSAKI_NAME2 = this.GyoushaEntity.MANI_HENSOUSAKI_NAME2;
                        this.genbaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.GyoushaEntity.MANI_HENSOUSAKI_KEISHOU1;
                        this.genbaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.GyoushaEntity.MANI_HENSOUSAKI_KEISHOU2;
                        this.genbaEntity.MANI_HENSOUSAKI_POST = this.GyoushaEntity.MANI_HENSOUSAKI_POST;
                        this.genbaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.GyoushaEntity.MANI_HENSOUSAKI_ADDRESS1;
                        this.genbaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.GyoushaEntity.MANI_HENSOUSAKI_ADDRESS2;
                        this.genbaEntity.MANI_HENSOUSAKI_BUSHO = this.GyoushaEntity.MANI_HENSOUSAKI_BUSHO;
                        this.genbaEntity.MANI_HENSOUSAKI_TANTOU = this.GyoushaEntity.MANI_HENSOUSAKI_TANTOU;
                    }
                    else
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_NAME1 = string.Empty;
                        this.genbaEntity.MANI_HENSOUSAKI_NAME2 = string.Empty;
                        this.genbaEntity.MANI_HENSOUSAKI_KEISHOU1 = string.Empty;
                        this.genbaEntity.MANI_HENSOUSAKI_KEISHOU2 = string.Empty;
                        this.genbaEntity.MANI_HENSOUSAKI_POST = string.Empty;
                        this.genbaEntity.MANI_HENSOUSAKI_ADDRESS1 = string.Empty;
                        this.genbaEntity.MANI_HENSOUSAKI_ADDRESS2 = string.Empty;
                        this.genbaEntity.MANI_HENSOUSAKI_BUSHO = string.Empty;
                        this.genbaEntity.MANI_HENSOUSAKI_TANTOU = string.Empty;
                    }

                    if (this.IsGenbaAdd)
                    {
                        if (1 == this.sysinfoEntity.MANIFEST_USE_A)
                        {
                            this.genbaEntity.MANI_HENSOUSAKI_USE_A = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_A = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A = this.genbaEntity.GYOUSHA_CD;
                            this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_A = this.genbaEntity.GENBA_CD;
                        }
                        else
                        {
                            this.genbaEntity.MANI_HENSOUSAKI_USE_A = 2;
                            this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_A = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_A = null;
                        }

                        if (1 == this.sysinfoEntity.MANIFEST_USE_B1)
                        {
                            this.genbaEntity.MANI_HENSOUSAKI_USE_B1 = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B1 = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1 = this.genbaEntity.GYOUSHA_CD;
                            this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1 = this.genbaEntity.GENBA_CD;
                        }
                        else
                        {
                            this.genbaEntity.MANI_HENSOUSAKI_USE_B1 = 2;
                            this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B1 = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1 = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1 = null;
                        }

                        if (1 == this.sysinfoEntity.MANIFEST_USE_B2)
                        {
                            this.genbaEntity.MANI_HENSOUSAKI_USE_B2 = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B2 = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2 = this.genbaEntity.GYOUSHA_CD;
                            this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2 = this.genbaEntity.GENBA_CD;
                        }
                        else
                        {
                            this.genbaEntity.MANI_HENSOUSAKI_USE_B2 = 2;
                            this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B2 = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2 = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2 = null;
                        }

                        if (1 == this.sysinfoEntity.MANIFEST_USE_B4)
                        {
                            this.genbaEntity.MANI_HENSOUSAKI_USE_B4 = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B4 = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4 = this.genbaEntity.GYOUSHA_CD;
                            this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4 = this.genbaEntity.GENBA_CD;
                        }
                        else
                        {
                            this.genbaEntity.MANI_HENSOUSAKI_USE_B4 = 2;
                            this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B4 = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4 = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4 = null;
                        }

                        if (1 == this.sysinfoEntity.MANIFEST_USE_B6)
                        {
                            this.genbaEntity.MANI_HENSOUSAKI_USE_B6 = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B6 = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6 = this.genbaEntity.GYOUSHA_CD;
                            this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6 = this.genbaEntity.GENBA_CD;
                        }
                        else
                        {
                            this.genbaEntity.MANI_HENSOUSAKI_USE_B6 = 2;
                            this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B6 = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6 = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6 = null;
                        }

                        if (1 == this.sysinfoEntity.MANIFEST_USE_C1)
                        {
                            this.genbaEntity.MANI_HENSOUSAKI_USE_C1 = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C1 = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1 = this.genbaEntity.GYOUSHA_CD;
                            this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1 = this.genbaEntity.GENBA_CD;
                        }
                        else
                        {
                            this.genbaEntity.MANI_HENSOUSAKI_USE_C1 = 2;
                            this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C1 = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1 = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1 = null;
                        }

                        if (1 == this.sysinfoEntity.MANIFEST_USE_C2)
                        {
                            this.genbaEntity.MANI_HENSOUSAKI_USE_C2 = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C2 = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2 = this.genbaEntity.GYOUSHA_CD;
                            this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2 = this.genbaEntity.GENBA_CD;
                        }
                        else
                        {
                            this.genbaEntity.MANI_HENSOUSAKI_USE_C2 = 2;
                            this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C2 = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2 = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2 = null;
                        }

                        if (1 == this.sysinfoEntity.MANIFEST_USE_D)
                        {
                            this.genbaEntity.MANI_HENSOUSAKI_USE_D = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_D = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D = this.genbaEntity.GYOUSHA_CD;
                            this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_D = this.genbaEntity.GENBA_CD;
                        }
                        else
                        {
                            this.genbaEntity.MANI_HENSOUSAKI_USE_D = 2;
                            this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_D = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_D = null;
                        }

                        if (1 == this.sysinfoEntity.MANIFEST_USE_E)
                        {
                            this.genbaEntity.MANI_HENSOUSAKI_USE_E = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_E = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E = this.genbaEntity.GYOUSHA_CD;
                            this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_E = this.genbaEntity.GENBA_CD;
                        }
                        else
                        {
                            this.genbaEntity.MANI_HENSOUSAKI_USE_E = 2;
                            this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_E = 1;
                            this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E = null;
                            this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_E = null;
                        }
                    }
                }
                else
                {
                    this.genbaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = 1;
                    this.genbaEntity.MANI_HENSOUSAKI_NAME1 = this.GyoushaEntity.GYOUSHA_NAME1;
                    this.genbaEntity.MANI_HENSOUSAKI_NAME2 = this.GyoushaEntity.GYOUSHA_NAME2;
                    this.genbaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.GyoushaEntity.GYOUSHA_KEISHOU1;
                    this.genbaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.GyoushaEntity.GYOUSHA_KEISHOU2;
                    this.genbaEntity.MANI_HENSOUSAKI_POST = this.GyoushaEntity.GYOUSHA_POST;
                    this.genbaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.GyoushaEntity.GYOUSHA_ADDRESS1;
                    this.genbaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.GyoushaEntity.GYOUSHA_ADDRESS2;
                    this.genbaEntity.MANI_HENSOUSAKI_BUSHO = this.GyoushaEntity.BUSHO;
                    this.genbaEntity.MANI_HENSOUSAKI_TANTOU = this.GyoushaEntity.TANTOUSHA;

                    if (this.IsGenbaAdd)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_USE_A = 2;
                        this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_A = 2;
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = null;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A = null;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_A = null;
                        this.genbaEntity.MANI_HENSOUSAKI_USE_B1 = 2;
                        this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B1 = 2;
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = null;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1 = null;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1 = null;

                        this.genbaEntity.MANI_HENSOUSAKI_USE_B2 = 2;
                        this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B2 = 2;
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = null;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2 = null;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2 = null;

                        this.genbaEntity.MANI_HENSOUSAKI_USE_B4 = 2;
                        this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B4 = 2;
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 = null;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4 = null;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4 = null;

                        this.genbaEntity.MANI_HENSOUSAKI_USE_B6 = 2;
                        this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B6 = 2;
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 = null;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6 = null;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6 = null;

                        this.genbaEntity.MANI_HENSOUSAKI_USE_C1 = 2;
                        this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C1 = 2;
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = null;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1 = null;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1 = null;

                        this.genbaEntity.MANI_HENSOUSAKI_USE_C2 = 2;
                        this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C2 = 2;
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = null;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2 = null;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2 = null;

                        this.genbaEntity.MANI_HENSOUSAKI_USE_D = 2;
                        this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_D = 2;
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = null;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D = null;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_D = null;

                        this.genbaEntity.MANI_HENSOUSAKI_USE_E = 2;
                        this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_E = 2;
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = null;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E = null;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_E = null;
                    }
                }
                this.genbaEntity.TEKIYOU_BEGIN = this.GyoushaEntity.TEKIYOU_BEGIN;
                this.genbaEntity.TEKIYOU_END = this.GyoushaEntity.TEKIYOU_END;
                this.genbaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = this.GyoushaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD;

                // mapbox情報
                this.genbaEntity.GENBA_LATITUDE = this.GyoushaEntity.GYOUSHA_LATITUDE;
                this.genbaEntity.GENBA_LONGITUDE = this.GyoushaEntity.GYOUSHA_LONGITUDE;
                if (string.IsNullOrEmpty(this.form.GyoushaLatitude.Text) &&
                    string.IsNullOrEmpty(this.form.GyoushaLongitude.Text) &&
                    string.IsNullOrEmpty(this.form.LocationInfoUpdateName.Text) &&
                    string.IsNullOrEmpty(this.form.LocationInfoUpdateDate.Text))
                {
                    // 未入力の状態から入力なし、という扱いなのでこの場合のみ更新なし
                }
                else
                {
                    this.genbaEntity.GENBA_LOCATION_INFO_UPDATE_NAME = SystemProperty.UserName;
                    this.genbaEntity.GENBA_LOCATION_INFO_UPDATE_DATE = this.sysDate();
                }

                var dataBinderLogic = new DataBinderLogic<M_GENBA>(this.genbaEntity);
                dataBinderLogic.SetSystemProperty(this.genbaEntity, false);
                MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), this.genbaEntity);

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateGenbaEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// ゼロパディング処理
        /// </summary>
        /// <param name="inputData">ゼロパディングしたい文字列</param>
        /// <returns>ゼロパディングされた文字列</returns>
        public string ZeroPadding(string inputData, out bool catchErr)
        {
            try
            {
                catchErr = false;
                if (!(Regex.Match(inputData, "^[a-zA-Z0-9]+$")).Success)
                {
                    return inputData;
                }

                PropertyInfo pi = this.form.GYOUSHA_CD.GetType().GetProperty(GyoushaHoshuConstans.CHARACTERS_NUMBER);
                Decimal charNumber = (Decimal)pi.GetValue(this.form.GYOUSHA_CD, null);

                string padData = inputData.PadLeft((int)charNumber, '0');

                return padData;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("ZeroPadding", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return "";
            }
        }

        /// <summary>
        /// ゼロパディング処理(県用)
        /// </summary>
        /// <param name="inputData">ゼロパディングしたい文字列</param>
        /// <returns>ゼロパディングされた文字列</returns>
        public string ZeroPadding_Ken(string inputData)
        {
            if (!(Regex.Match(inputData, "^[a-zA-Z0-9]+$")).Success)
            {
                return inputData;
            }

            PropertyInfo pi = this.form.GYOUSHA_TODOUFUKEN_CD.GetType().GetProperty(GyoushaHoshuConstans.CHARACTERS_NUMBER);
            Decimal charNumber = (Decimal)pi.GetValue(this.form.GYOUSHA_TODOUFUKEN_CD, null);

            string padData = inputData.PadLeft((int)charNumber, '0');

            return padData;
        }

        /// <summary>
        /// 登録時ユーザーコードチェック処理
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public bool FormUserRegistCheck(object source, r_framework.Event.RegistCheckEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(source, e);

                MessageUtility msgUtil = new MessageUtility();
                if (e.multiRow == null)
                {
                    // 通常コントロールのチェック
                    Control ctrl = (Control)source;
                    ICustomControl customCtrl = source as ICustomControl;

                    // 業者区分のマニ記載者がチェック済みかつ
                    // 業者分類タブの排出事業者、運搬受託者、処分受託者全て未チェックの場合、エラー
                    if (ctrl != null
                        && customCtrl != null
                        && ctrl.Name.Equals("Gyousha_KBN_3")
                        && DB_FLAG.TRUE.ToString().Equals(customCtrl.GetResultText())
                        && !this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked
                        && !this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked
                        && !this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked)
                    {
                        string errorItem = string.Format("{0}、{1}、{2}のいづれか", this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Text, this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Text, this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Text);
                        e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, errorItem));
                    }
                }
                else
                {
                    // グリッドセル内のチェック
                }

                LogUtility.DebugMethodEnd(source, e, false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormUserRegistCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(source, e, true);
                return true;
            }
        }

        #region 登録/更新/削除

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void Regist(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart(errorFlag);

                if (errorFlag)
                {
                    return;
                }

                this.GyoushaEntity.DELETE_FLG = false;

                // 業者マスタ更新
                this.daoGyousha.Insert(this.GyoushaEntity);
                // 現場マスタ更新
                if (null != this.genbaEntity)
                {
                    this.daoGenba.Insert(this.genbaEntity);
                }

                this.isRegist = true;

                this.genbaEntity = null;

                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void Update(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (errorFlag)
                {
                    return;
                }

                this.GyoushaEntity.DELETE_FLG = false;

                // 入金先マスタ更新
                this.daoGyousha.Update(this.GyoushaEntity);
                // 現場マスタ更新
                if (this.IsGenbaAdd)
                {
                    if (null != this.genbaEntity)
                    {
                        this.daoGenba.Insert(this.genbaEntity);
                    }
                }
                else
                {
                    if (null != this.genbaEntity)
                    {
                        this.daoGenba.Update(this.genbaEntity);
                    }
                }

                // 初期化
                var gyousyaCd = this.form.GYOUSHA_CD.Text;
                var torihikisaki_Cd = this.form.TORIHIKISAKI_CD.Text;
                string kyotenCdSet = "NULL";
                string kyotenCdWhere = "IS NULL";
                StringBuilder sql = new StringBuilder();

                // 拠点CDの入力がない場合
                if (!string.IsNullOrEmpty(this.form.KYOTEN_CD.Text))
                {
                    kyotenCdSet = "'" + this.form.KYOTEN_CD.Text + "'";
                }
                // 拠点CDの入力がある場合
                else
                {
                    // 変更前の拠点CDがNULLじゃない場合
                    if (!this.gyousha.KYOTEN_CD.IsNull)
                    {
                        kyotenCdWhere = "= '" + this.gyousha.KYOTEN_CD.Value.ToString() + "'";
                    }
                }

                // 業者入力で登録した取引先・拠点を紐づく現場にも登録する
                sql.Append("UPDATE M_GENBA ");
                sql.Append("SET TORIHIKISAKI_CD = '" + torihikisaki_Cd + "', KYOTEN_CD = " + kyotenCdSet + " ");
                sql.Append("WHERE GYOUSHA_CD = '" + gyousyaCd + "' " + "AND KYOTEN_CD " + kyotenCdWhere + " ");
                this.daoGenba.GetDateForStringSql(sql.ToString());

                this.isRegist = true;

                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Update", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Update", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Update", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        public virtual void LogicalDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                /* CheckDeleteメソッドで行うので削除 */
                // 業者に紐付く現場のチェック
                //if (new GyoushaHoshuValidator().HasGenba(this.GyoushaEntity.GYOUSHA_CD))
                //{
                //    // {0}入力に該当する{1}が使用されています。<br>{0}を削除してから削除してください。
                //    msgLogic.MessageBoxShow("I016", new[] { "現場", "業者" });
                //    LogUtility.DebugMethodEnd();
                //    return;
                //}

                this.GyoushaEntity.DELETE_FLG = true;

                this.daoGyousha.Update(this.GyoushaEntity);

                this.isRegist = true;

                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// 削除できるかどうかチェックする
        /// </summary>
        /// <returns></returns>
        public bool CheckDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                var cd = this.form.GYOUSHA_CD.Text;

                if (!string.IsNullOrEmpty(cd))
                {
                    DataTable dtTable = DaoInitUtility.GetComponent<GyoushaHoshu.Dao.IDenshiShinseiEntryDao>().GetDataBySqlFileCheck(cd);
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        string strName = string.Empty;

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            strName += "\n" + dr["NAME"].ToString();
                        }

                        new MessageBoxShowLogic().MessageBoxShow("E258", "業者", "業者CD", strName);

                        ret = false;
                    }
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
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

            GyoushaHoshuLogic localLogic = other as GyoushaHoshuLogic;
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
        /// 検索結果を現場一覧に設定
        /// </summary>
        internal void SetIchiranGenba()
        {
            this.form.GENBA_ICHIRAN.IsBrowsePurpose = false;
            var table = this.SearchResultGenba;
            table.BeginLoadData();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].ReadOnly = false;
            }
            this.form.GENBA_ICHIRAN.DataSource = table;
            this.form.GENBA_ICHIRAN.IsBrowsePurpose = true;
        }

        /// <summary>
        /// 検索結果を委託一覧に設定
        /// </summary>
        internal void SetIchiranItaku()
        {
            this.form.ITAKU_ICHIRAN.IsBrowsePurpose = false;
            var table = this.SearchResultItaku;
            table.BeginLoadData();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].ReadOnly = false;
            }
            this.ChangeItakuStatus(table);
            this.form.ITAKU_ICHIRAN.DataSource = table;
            this.form.ITAKU_ICHIRAN.IsBrowsePurpose = true;
        }

        /// <summary>
        /// 委託計約種類、ステータス変換処理
        /// </summary>
        private void ChangeItakuStatus(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                // 委託契約種類変換
                string keyShurui = table.Rows[i][GyoushaHoshuConstans.ITAKU_SHURUI].ToString();
                string shurui = this.ItakuKeiyakuShurui[keyShurui];
                table.Rows[i][GyoushaHoshuConstans.ITAKU_SHURUI] = shurui;

                // 委託契約ステータス変換
                string keyStatus = table.Rows[i][GyoushaHoshuConstans.ITAKU_STATUS].ToString();
                string status = this.ItakuKeiyakuStatus[keyStatus];
                table.Rows[i][GyoushaHoshuConstans.ITAKU_STATUS] = status;
            }
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit(BusinessBaseForm parentForm)
        {
            if (denshiShinseiFlg)
            {
                //「F9:申請」となる場合のイベント生成

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
            }
            else
            {
                //新規ボタン(F2)イベント生成
                parentForm.bt_func2.Click += new EventHandler(this.form.CreateMode);

                //修正ボタン(F3)イベント生成
                parentForm.bt_func3.Click += new EventHandler(this.form.UpdateMode);

                //一覧ボタン(F7)イベント生成
                parentForm.bt_func7.Click += new EventHandler(this.form.ShowIchiran);

                //登録ボタン(F9)イベント生成
                this.form.C_Regist(parentForm.bt_func9);
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                //取消ボタン(F11)イベント生成
                parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

                // mapbox連携
                // 地図表示処理
                this.form.bt_map_open.Click += new EventHandler(this.OpenMap);
            }
            // SuperFormイベント処理
            this.form.UserRegistCheck += new SuperForm.UserRegistCheckHandler(this.form.FormUserRegistCheck);

            // VUNGUYEN 20150525 #1294 START
            // 適用終了のダブルクリックイベント
            this.form.TEKIYOU_END.MouseDoubleClick += new MouseEventHandler(TEKIYOU_END_MouseDoubleClick);
            // VUNGUYEN 20150525 #1294 END

            // Begin: LANDUONG - 20220214 - refs#160052
            this.form.RAKURAKU_SAIBAN_BUTTON.Click += new EventHandler(this.form.RAKURAKU_SAIBAN_BUTTON_Click);
            // End: LANDUONG - 20220214 - refs#160052
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            if (denshiShinseiFlg)
            {
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath2);
            }
            else
            {
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
        }

        /// <summary>
        /// 現場一覧検索
        /// </summary>
        public bool TorihikiStopIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.SearchResultGenba = new DataTable();

                this.rowCntGenba = this.SearchGenbaIchiran();
                if (this.rowCntGenba == 0)
                {
                    this.form.GENBA_ICHIRAN.DataSource = null;
                    this.form.GENBA_ICHIRAN.Rows.Clear();
                    return false;
                }

                this.SetIchiranGenba();

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("TorihikiStopIchiran", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TorihikiStopIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// タブ表示制御
        /// </summary>
        public bool TabDispControl()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.form.TORIHIKISAKI_UMU_KBN.Text))
                {
                    //使用不能にする
                    this.form.TORIHIKISAKI_CD.ReadOnly = true;
                    this.form.TORIHIKISAKI_CD.Enabled = false;
                    this.form.TORIHIKISAKI_CD.Text = string.Empty;

                    // Begin: LANDUONG - 20220214 - refs#160052
                    HakkousakuAndRakurakuCDCheck();
                    // End: LANDUONG - 20220214 - refs#160052

                    this.form.bt_torihikisaki_copy.Enabled = false;
                    this.form.bt_torihikisaki_search.Enabled = false;
                    _tabPageManager.ChangeTabPageVisible(1, false);
                    _tabPageManager.ChangeTabPageVisible(2, false);

                    return false;
                }

                if (int.Parse(this.form.TORIHIKISAKI_UMU_KBN.Text) == 1)
                {
                    // 使用可能にする
                    this.form.TORIHIKISAKI_CD.ReadOnly = false;
                    this.form.TORIHIKISAKI_CD.Enabled = true;
                    this.form.bt_torihikisaki_copy.Enabled = true;
                    this.form.bt_torihikisaki_search.Enabled = true;
                    _tabPageManager.ChangeTabPageVisible(1, true);
                    _tabPageManager.ChangeTabPageVisible(2, true);

                    // 請求情報
                    if (this.GyoushaEntity == null)
                    {
                        return false;
                    }

                    this.form.SEIKYUU_SOUFU_NAME1.Text = this.GyoushaEntity.SEIKYUU_SOUFU_NAME1;
                    this.form.SEIKYUU_SOUFU_NAME2.Text = this.GyoushaEntity.SEIKYUU_SOUFU_NAME2;
                    this.form.SEIKYUU_SOUFU_KEISHOU1.Text = this.GyoushaEntity.SEIKYUU_SOUFU_KEISHOU1;
                    this.form.SEIKYUU_SOUFU_KEISHOU2.Text = this.GyoushaEntity.SEIKYUU_SOUFU_KEISHOU2;
                    this.form.SEIKYUU_SOUFU_POST.Text = this.GyoushaEntity.SEIKYUU_SOUFU_POST;
                    this.form.SEIKYUU_SOUFU_ADDRESS1.Text = this.GyoushaEntity.SEIKYUU_SOUFU_ADDRESS1;
                    this.form.SEIKYUU_SOUFU_ADDRESS2.Text = this.GyoushaEntity.SEIKYUU_SOUFU_ADDRESS2;
                    this.form.SEIKYUU_SOUFU_BUSHO.Text = this.GyoushaEntity.SEIKYUU_SOUFU_BUSHO;
                    this.form.SEIKYUU_SOUFU_TANTOU.Text = this.GyoushaEntity.SEIKYUU_SOUFU_TANTOU;
                    this.form.SEIKYUU_SOUFU_TEL.Text = this.GyoushaEntity.SEIKYUU_SOUFU_TEL;
                    this.form.SEIKYUU_SOUFU_FAX.Text = this.GyoushaEntity.SEIKYUU_SOUFU_FAX;
                    this.form.SEIKYUU_TANTOU.Text = this.GyoushaEntity.SEIKYUU_TANTOU;
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;

                    // Begin: LANDUONG - 20220214 - refs#160052        
                    this.form.HAKKOUSAKI_CD.Text = this.GyoushaEntity.HAKKOUSAKI_CD;
                    this.form.RAKURAKU_CUSTOMER_CD.Text = this.GyoushaEntity.RAKURAKU_CUSTOMER_CD;                    
                    HakkousakuAndRakurakuCDCheck();
                    // End: LANDUONG - 20220214 - refs#160052

                    if (!this.GyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.GyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.Value.ToString();
                    }
                    else if (this.GyoushaEntity.TORIHIKISAKI_UMU_KBN == 2 && !this.sysinfoEntity.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.sysinfoEntity.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.Value.ToString();
                    }
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                    if (!this.GyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.GyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN.Value.ToString();
                    }
                    else if (this.GyoushaEntity.TORIHIKISAKI_UMU_KBN == 2 && !this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN.Value.ToString();
                    }
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    if (!this.GyoushaEntity.SEIKYUU_KYOTEN_CD.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.GyoushaEntity.SEIKYUU_KYOTEN_CD.Value.ToString()));
                        M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                        if (kyo != null)
                        {
                            this.form.SEIKYUU_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                        }
                    }
                    else if (this.GyoushaEntity.TORIHIKISAKI_UMU_KBN == 2 && !this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.Value.ToString()));
                        M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                        if (kyo != null)
                        {
                            this.form.SEIKYUU_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                        }
                    }

                    // 支払情報
                    this.form.SHIHARAI_SOUFU_NAME1.Text = this.GyoushaEntity.SHIHARAI_SOUFU_NAME1;
                    this.form.SHIHARAI_SOUFU_NAME2.Text = this.GyoushaEntity.SHIHARAI_SOUFU_NAME2;
                    this.form.SHIHARAI_SOUFU_KEISHOU1.Text = this.GyoushaEntity.GYOUSHA_KEISHOU1;
                    this.form.SHIHARAI_SOUFU_KEISHOU2.Text = this.GyoushaEntity.GYOUSHA_KEISHOU2;
                    this.form.SHIHARAI_SOUFU_POST.Text = this.GyoushaEntity.SHIHARAI_SOUFU_POST;
                    this.form.SHIHARAI_SOUFU_ADDRESS1.Text = this.GyoushaEntity.SHIHARAI_SOUFU_ADDRESS1;
                    this.form.SHIHARAI_SOUFU_ADDRESS2.Text = this.GyoushaEntity.SHIHARAI_SOUFU_ADDRESS2;
                    this.form.SHIHARAI_SOUFU_BUSHO.Text = this.GyoushaEntity.SHIHARAI_SOUFU_BUSHO;
                    this.form.SHIHARAI_SOUFU_TANTOU.Text = this.GyoushaEntity.SHIHARAI_SOUFU_TANTOU;
                    this.form.SHIHARAI_SOUFU_TEL.Text = this.GyoushaEntity.SHIHARAI_SOUFU_TEL;
                    this.form.SHIHARAI_SOUFU_FAX.Text = this.GyoushaEntity.SHIHARAI_SOUFU_FAX;
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                    if (!this.GyoushaEntity.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.GyoushaEntity.SHIHARAI_KYOTEN_PRINT_KBN.Value.ToString();
                    }
                    else if (this.GyoushaEntity.TORIHIKISAKI_UMU_KBN == 2 && !this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN.Value.ToString();
                    }
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    if (!this.GyoushaEntity.SHIHARAI_KYOTEN_CD.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.GyoushaEntity.SHIHARAI_KYOTEN_CD.Value.ToString()));
                        M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                        if (kyo != null)
                        {
                            this.form.SHIHARAI_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                        }
                    }
                    else if (this.GyoushaEntity.TORIHIKISAKI_UMU_KBN == 2 && !this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.Value.ToString()));
                        M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                        if (kyo != null)
                        {
                            this.form.SHIHARAI_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                        }
                    }
                }
                else
                {
                    //使用不能にする
                    this.form.TORIHIKISAKI_CD.ReadOnly = true;
                    this.form.TORIHIKISAKI_CD.Enabled = false;
                    this.form.TORIHIKISAKI_CD.Text = string.Empty;

                    // Begin: LANDUONG - 20220214 - refs#160052   
                    HakkousakuAndRakurakuCDCheck();
                    // End: LANDUONG - 20220214 - refs#160052   

                    this.form.TORIHIKISAKI_NAME1.Text = string.Empty;
                    this.form.TORIHIKISAKI_NAME2.Text = string.Empty;
                    this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                    this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                    this.form.KYOTEN_CD.Text = string.Empty;
                    this.form.KYOTEN_NAME.Text = string.Empty;
                    this.form.bt_torihikisaki_copy.Enabled = false;
                    this.form.bt_torihikisaki_search.Enabled = false;
                    _tabPageManager.ChangeTabPageVisible(1, false);
                    _tabPageManager.ChangeTabPageVisible(2, false);
                    this.form.TORIHIKISAKI_CD.IsInputErrorOccured = false;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TabDispControl", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// マニありチェックボックスのON/OFFチェック
        /// </summary>
        /// <returns></returns>
        public bool ManiCheckOffCheck()
        {
            try
            {
                if (FlgManiHensousakiKbn)
                {
                    //　使用可能
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Enabled = true;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Enabled = true;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Enabled = true;
                    if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked)
                    {
                        this.form.MANI_HENSOUSAKI_NAME1.Enabled = true;
                        this.form.MANI_HENSOUSAKI_NAME2.Enabled = true;
                        this.form.MANI_HENSOUSAKI_KEISHOU1.Enabled = true;
                        this.form.MANI_HENSOUSAKI_KEISHOU2.Enabled = true;
                        this.form.MANI_HENSOUSAKI_POST.Enabled = true;
                        this.form.bt_hensousaki_torihikisaki_copy.Enabled = true;
                        this.form.bt_hensousaki_address.Enabled = true;
                        this.form.bt_hensousaki_post.Enabled = true;
                        this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = true;
                        this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = true;
                        this.form.MANI_HENSOUSAKI_BUSHO.Enabled = true;
                        this.form.MANI_HENSOUSAKI_TANTOU.Enabled = true;
                    }
                }
                else
                {
                    // 使用不可
                    if (string.IsNullOrEmpty(this.GyoushaCd))
                    {
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "2";
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                    }
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Enabled = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Enabled = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Enabled = false;
                    this.form.MANI_HENSOUSAKI_NAME1.Enabled = false;
                    this.form.MANI_HENSOUSAKI_NAME2.Enabled = false;
                    this.form.MANI_HENSOUSAKI_KEISHOU1.Enabled = false;
                    this.form.MANI_HENSOUSAKI_KEISHOU2.Enabled = false;
                    this.form.MANI_HENSOUSAKI_POST.Enabled = false;
                    this.form.bt_hensousaki_torihikisaki_copy.Enabled = false;
                    this.form.bt_hensousaki_address.Enabled = false;
                    this.form.bt_hensousaki_post.Enabled = false;
                    this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = false;
                    this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = false;
                    this.form.MANI_HENSOUSAKI_BUSHO.Enabled = false;
                    this.form.MANI_HENSOUSAKI_TANTOU.Enabled = false;

                    // テキストクリア
                    this.form.MANI_HENSOUSAKI_NAME1.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_NAME2.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_POST.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_ADDRESS1.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_ADDRESS2.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_BUSHO.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_TANTOU.Text = string.Empty;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ManiCheckOffCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 業者と現場のマニ不整合チェック
        /// </summary>
        /// <returns></returns>
        public bool ManiCheckMsg(M_GENBA queryParam, out bool catchErr)
        {
            try
            {
                catchErr = false;
                M_GENBA[] result = this.daoGenba.GetAllValidData(queryParam);

                if (result != null && result.Length > 0)
                {
                    return false;
                }

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("ManiCheckMsg", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("ManiCheckMsg", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 取引先情報コピー処理
        /// </summary>
        public bool TorihikisakiCopy()
        {
            try
            {
                string inputCd = this.form.TORIHIKISAKI_CD.Text;
                if (string.IsNullOrWhiteSpace(inputCd))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    var result = msgLogic.MessageBoxShow("E012", "取引先CD");
                    return false;
                }
                else
                {
                    bool catchErr = false;
                    string zeroPadCd = this.ZeroPadding(inputCd, out catchErr);
                    if (catchErr)
                    {
                        return true;
                    }
                    this.TorihikisakiSetting(zeroPadCd);
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TorihikisakiCopy", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 取引先コピー＆セッティング
        /// </summary>
        /// <param name="inputCd">入力された取引先CD</param>
        private void TorihikisakiSetting(string inputCd)
        {
            M_TORIHIKISAKI torisakiEntity = this.daoTorisaki.GetDataByCd(inputCd);
            M_TORIHIKISAKI_SHIHARAI shiharaiEntity = this.daoShiharai.GetDataByCd(inputCd);
            M_TORIHIKISAKI_SEIKYUU seikyuuEntity = this.daoSeikyuu.GetDataByCd(inputCd);

            if (torisakiEntity != null)
            {
                // 共通部分
                if (!torisakiEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                {
                    this.form.KYOTEN_CD.Text = torisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString();                // 拠点CD
                    M_KYOTEN kyoten = this.daoKyoten.GetDataByCd(torisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString());
                    if (kyoten == null)
                    {
                        this.form.KYOTEN_CD.Text = string.Empty;
                    }
                    else
                    {
                        this.form.KYOTEN_NAME.Text = kyoten.KYOTEN_NAME_RYAKU;                                  // 拠点名
                    }
                }

                this.form.GYOUSHA_NAME1.Text = torisakiEntity.TORIHIKISAKI_NAME1;                               // 取引先名１
                this.form.GYOUSHA_NAME2.Text = torisakiEntity.TORIHIKISAKI_NAME2;                               // 取引先名２

                if (torisakiEntity.TEKIYOU_BEGIN.IsNull)
                {
                    this.form.TEKIYOU_BEGIN.Value = null;
                }
                else
                {
                    this.form.TEKIYOU_BEGIN.Value = torisakiEntity.TEKIYOU_BEGIN;
                }
                if (torisakiEntity.TEKIYOU_END.IsNull)
                {
                    this.form.TEKIYOU_END.Value = null;
                }
                else
                {
                    this.form.TEKIYOU_END.Value = torisakiEntity.TEKIYOU_END;
                }

                this.form.GYOUSHA_KEISHOU1.Text = torisakiEntity.TORIHIKISAKI_KEISHOU1;                         // 敬称１
                this.form.GYOUSHA_KEISHOU2.Text = torisakiEntity.TORIHIKISAKI_KEISHOU2;                         // 敬称２

                this.form.GYOUSHA_FURIGANA.Text = torisakiEntity.TORIHIKISAKI_FURIGANA;                         // フリガナ(不要な可能性有)
                this.form.GYOUSHA_NAME_RYAKU.Text = torisakiEntity.TORIHIKISAKI_NAME_RYAKU;                     // 略称(不要な可能性有)

                this.form.GYOUSHA_TEL.Text = torisakiEntity.TORIHIKISAKI_TEL;                                   // 電話
                this.form.GYOUSHA_FAX.Text = torisakiEntity.TORIHIKISAKI_FAX;                                   // FAX
                this.form.EIGYOU_TANTOU_BUSHO_CD.Text = torisakiEntity.EIGYOU_TANTOU_BUSHO_CD;                  // 営業担当部署CD
                M_BUSHO busho = this.daoBusho.GetDataByCd(torisakiEntity.EIGYOU_TANTOU_BUSHO_CD);
                if (busho == null)
                {
                    this.form.EIGYOU_TANTOU_BUSHO_CD.Text = string.Empty;
                }
                else
                {
                    this.form.BUSHO_NAME.Text = busho.BUSHO_NAME_RYAKU;                                         // 営業担当部署名
                }
                this.form.EIGYOU_TANTOU_CD.Text = torisakiEntity.EIGYOU_TANTOU_CD;                              // 営業担当者CD
                M_SHAIN shain = this.daoShain.GetDataByCd(torisakiEntity.EIGYOU_TANTOU_CD);
                if (shain == null)
                {
                    this.form.EIGYOU_TANTOU_CD.Text = string.Empty;
                }
                else
                {
                    this.form.SHAIN_NAME.Text = shain.SHAIN_NAME_RYAKU;                                         // 営業担当者名
                }

                // 適用開始日
                if (torisakiEntity.TEKIYOU_BEGIN.IsNull)
                {
                    this.form.TEKIYOU_BEGIN.Value = null;
                }
                else
                {
                    this.form.TEKIYOU_BEGIN.Value = torisakiEntity.TEKIYOU_BEGIN;
                }

                // 適用終了日
                if (torisakiEntity.TEKIYOU_END.IsNull)
                {
                    this.form.TEKIYOU_END.Value = null;
                }
                else
                {
                    this.form.TEKIYOU_END.Value = torisakiEntity.TEKIYOU_END;
                }
                this.form.CHUUSHI_RIYUU1.Text = torisakiEntity.CHUUSHI_RIYUU1;                              // 中止理由1
                this.form.CHUUSHI_RIYUU2.Text = torisakiEntity.CHUUSHI_RIYUU2;                              // 中止理由2
                this.form.SHOKUCHI_KBN.Checked = (bool)torisakiEntity.SHOKUCHI_KBN;                         // 諸口区分

                // 基本情報タブ
                this.form.GYOUSHA_POST.Text = torisakiEntity.TORIHIKISAKI_POST;                                 // 郵便番号

                if (!torisakiEntity.TORIHIKISAKI_TODOUFUKEN_CD.IsNull)
                {
                    this.form.GYOUSHA_TODOUFUKEN_CD.Text = this.ZeroPadding_Ken(torisakiEntity.TORIHIKISAKI_TODOUFUKEN_CD.ToString());    // 都道府県CD
                    M_TODOUFUKEN temp = this.daoTodoufuken.GetDataByCd(torisakiEntity.TORIHIKISAKI_TODOUFUKEN_CD.ToString());
                    if (temp == null)
                    {
                        this.form.GYOUSHA_TODOUFUKEN_CD.Text = string.Empty;
                    }
                    else
                    {
                        this.form.TODOUFUKEN_NAME.Text = temp.TODOUFUKEN_NAME;                                  // 都道府県名
                    }
                }

                this.form.GYOUSHA_ADDRESS1.Text = torisakiEntity.TORIHIKISAKI_ADDRESS1;                         // 住所１
                this.form.GYOUSHA_ADDRESS2.Text = torisakiEntity.TORIHIKISAKI_ADDRESS2;                         // 住所２

                // 地域の判定は関数に任せる
                if (this.ChechChiiki(true))
                {
                    this.form.CHIIKI_CD.Text = string.Empty;                                                    // 地域CD
                    this.form.CHIIKI_NAME.Text = string.Empty;                                                  // 地域名
                    this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = string.Empty;                           // 運搬報告書提出先地域CD
                    this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = string.Empty;                         // 運搬報告書提出先地域名
                }

                this.form.BUSHO.Text = torisakiEntity.BUSHO;                                                    // 部署
                this.form.TANTOUSHA.Text = torisakiEntity.TANTOUSHA;                                            // 担当者
                this.form.SHUUKEI_ITEM_CD.Text = torisakiEntity.SHUUKEI_ITEM_CD;                                // 集計項目CD
                M_SHUUKEI_KOUMOKU shuukei = this.daoShuukei.GetDataByCd(torisakiEntity.SHUUKEI_ITEM_CD);
                if (shuukei == null)
                {
                    this.form.SHUUKEI_ITEM_CD.Text = string.Empty;
                }
                else
                {
                    this.form.SHUUKEI_ITEM_NAME.Text = shuukei.SHUUKEI_KOUMOKU_NAME_RYAKU;                      // 集計項目名
                }
                this.form.GYOUSHU_CD.Text = torisakiEntity.GYOUSHU_CD;                                          // 業種CD
                M_GYOUSHU gyoushu = this.daoGyoushu.GetDataByCd(torisakiEntity.GYOUSHU_CD);
                if (gyoushu == null)
                {
                    this.form.GYOUSHU_CD.Text = string.Empty;
                }
                else
                {
                    this.form.GYOUSHU_NAME.Text = gyoushu.GYOUSHU_NAME_RYAKU;                                   // 業種名
                }
                this.form.BIKOU1.Text = torisakiEntity.BIKOU1;                                                  // 備考１
                this.form.BIKOU2.Text = torisakiEntity.BIKOU2;                                                  // 備考２
                this.form.BIKOU3.Text = torisakiEntity.BIKOU3;                                                  // 備考３
                this.form.BIKOU4.Text = torisakiEntity.BIKOU4;                                                  // 備考４

                //20250320
                this.form.URIAGE_GURUPU_CD.Text = torihikisakiEntity.URIAGE_GURUPU_CD;
                M_GURUPU_NYURYOKU gurupu = this.daoGurupu.GetDataByCd(gurupuEntity.GURUPU_CD);
                if (gurupu == null)
                {
                    this.form.URIAGE_GURUPU_CD.Text = string.Empty;
                }
                else
                {
                    this.form.URIAGE_GURUPU_NAME.Text = gurupu.GURUPU_NAME;
                }
                this.form.SHIHARAI_GURUPU_CD.Text = torihikisakiEntity.SHIHARAI_GURUPU_CD;
                M_GURUPU_NYURYOKU gurupu1 = this.daoGurupu.GetDataByCd(gurupuEntity.GURUPU_CD);
                if (gurupu1 == null)
                {
                    this.form.SHIHARAI_GURUPU_CD.Text = string.Empty;
                }
                else
                {
                    this.form.SHIHARAI_GURUPU_NAME.Text = gurupu.GURUPU_NAME;
                }

                // 業者分類タブ
                this.form.MANI_HENSOUSAKI_KBN.Checked = torisakiEntity.MANI_HENSOUSAKI_KBN.IsTrue;           // マニフェスト返送先
                if (torisakiEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN == 2)
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "2";
                }
                else if (torisakiEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN == 1)
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = true;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "1";
                }
                else
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = string.Empty;
                }
                if ("2".Equals(this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text))
                {
                    this.form.MANI_HENSOUSAKI_NAME1.Text = torisakiEntity.MANI_HENSOUSAKI_NAME1;
                    this.form.MANI_HENSOUSAKI_NAME2.Text = torisakiEntity.MANI_HENSOUSAKI_NAME2;
                    this.form.MANI_HENSOUSAKI_KEISHOU1.Text = torisakiEntity.MANI_HENSOUSAKI_KEISHOU1;
                    this.form.MANI_HENSOUSAKI_KEISHOU2.Text = torisakiEntity.MANI_HENSOUSAKI_KEISHOU2;
                    this.form.MANI_HENSOUSAKI_POST.Text = torisakiEntity.MANI_HENSOUSAKI_POST;
                    this.form.MANI_HENSOUSAKI_ADDRESS1.Text = torisakiEntity.MANI_HENSOUSAKI_ADDRESS1;
                    this.form.MANI_HENSOUSAKI_ADDRESS2.Text = torisakiEntity.MANI_HENSOUSAKI_ADDRESS2;
                    this.form.MANI_HENSOUSAKI_BUSHO.Text = torisakiEntity.MANI_HENSOUSAKI_BUSHO;
                    this.form.MANI_HENSOUSAKI_TANTOU.Text = torisakiEntity.MANI_HENSOUSAKI_TANTOU;
                }
                else
                {
                    this.form.MANI_HENSOUSAKI_NAME1.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_NAME2.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_POST.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_ADDRESS1.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_ADDRESS2.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_BUSHO.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_TANTOU.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_NAME1.Enabled = false;
                    this.form.MANI_HENSOUSAKI_NAME2.Enabled = false;
                    this.form.MANI_HENSOUSAKI_KEISHOU1.Enabled = false;
                    this.form.MANI_HENSOUSAKI_KEISHOU2.Enabled = false;
                    this.form.MANI_HENSOUSAKI_POST.Enabled = false;
                    this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = false;
                    this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = false;
                    this.form.MANI_HENSOUSAKI_BUSHO.Enabled = false;
                    this.form.MANI_HENSOUSAKI_TANTOU.Enabled = false;
                }


                this.form.GyoushaLatitude.Text = torisakiEntity.TORIHIKISAKI_LATITUDE;                          // 緯度
                this.form.GyoushaLongitude.Text = torisakiEntity.TORIHIKISAKI_LONGITUDE;                        // 経度

            }

            if (seikyuuEntity != null)
            {
                // 請求情報タブ
                this.form.SEIKYUU_SOUFU_NAME1.Text = seikyuuEntity.SEIKYUU_SOUFU_NAME1;                         // 請求書送付先1
                this.form.SEIKYUU_SOUFU_KEISHOU1.Text = seikyuuEntity.SEIKYUU_SOUFU_KEISHOU1;                   // 請求書送付先敬称1
                this.form.SEIKYUU_SOUFU_NAME2.Text = seikyuuEntity.SEIKYUU_SOUFU_NAME2;                         // 請求書送付先2
                this.form.SEIKYUU_SOUFU_KEISHOU2.Text = seikyuuEntity.SEIKYUU_SOUFU_KEISHOU2;                   // 請求書送付先敬称2
                this.form.SEIKYUU_SOUFU_POST.Text = seikyuuEntity.SEIKYUU_SOUFU_POST;                           // 送付先郵便番号
                this.form.SEIKYUU_SOUFU_ADDRESS1.Text = seikyuuEntity.SEIKYUU_SOUFU_ADDRESS1;                   // 送付先住所１
                this.form.SEIKYUU_SOUFU_ADDRESS2.Text = seikyuuEntity.SEIKYUU_SOUFU_ADDRESS2;                   // 送付先住所２
                this.form.SEIKYUU_SOUFU_BUSHO.Text = seikyuuEntity.SEIKYUU_SOUFU_BUSHO;                         // 送付先部署
                this.form.SEIKYUU_SOUFU_TANTOU.Text = seikyuuEntity.SEIKYUU_SOUFU_TANTOU;                       // 送付先担当者
                this.form.SEIKYUU_SOUFU_TEL.Text = seikyuuEntity.SEIKYUU_SOUFU_TEL;                             // 送付先電話番号
                this.form.SEIKYUU_SOUFU_FAX.Text = seikyuuEntity.SEIKYUU_SOUFU_FAX;                             // 送付先FAX番号
                this.form.SEIKYUU_TANTOU.Text = seikyuuEntity.SEIKYUU_TANTOU;

                // Begin: LANDUONG - 20220214 - refs#160052                                
                HakkousakuAndRakurakuCDCheck();                
                // End: LANDUONG - 20220214 - refs#160052

                if (!seikyuuEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                {
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = seikyuuEntity.SEIKYUU_DAIHYOU_PRINT_KBN.ToString();  // 代表取締役を印字
                }
                if (!seikyuuEntity.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = seikyuuEntity.SEIKYUU_KYOTEN_PRINT_KBN.ToString();    // 拠点名を印字
                }
                if (!seikyuuEntity.SEIKYUU_KYOTEN_CD.IsNull)
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = seikyuuEntity.SEIKYUU_KYOTEN_CD.ToString();                  // 請求書拠点
                    this.SeikyuuKyotenCdValidated();
                }
            }

            if (shiharaiEntity != null)
            {
                // 支払情報タブ
                this.form.SHIHARAI_SOUFU_NAME1.Text = shiharaiEntity.SHIHARAI_SOUFU_NAME1;		                // 支払明細書送付先1
                this.form.SHIHARAI_SOUFU_KEISHOU1.Text = shiharaiEntity.SHIHARAI_SOUFU_KEISHOU1;	            // 支払明細書送付先敬称1
                this.form.SHIHARAI_SOUFU_NAME2.Text = shiharaiEntity.SHIHARAI_SOUFU_NAME2;		                // 支払明細書送付先2
                this.form.SHIHARAI_SOUFU_KEISHOU2.Text = shiharaiEntity.SHIHARAI_SOUFU_KEISHOU2;	            // 支払明細書送付先敬称2
                this.form.SHIHARAI_SOUFU_POST.Text = shiharaiEntity.SHIHARAI_SOUFU_POST;		                // 送付先郵便番号
                this.form.SHIHARAI_SOUFU_ADDRESS1.Text = shiharaiEntity.SHIHARAI_SOUFU_ADDRESS1;	            // 送付先住所１
                this.form.SHIHARAI_SOUFU_ADDRESS2.Text = shiharaiEntity.SHIHARAI_SOUFU_ADDRESS2;	            // 送付先住所２
                this.form.SHIHARAI_SOUFU_BUSHO.Text = shiharaiEntity.SHIHARAI_SOUFU_BUSHO;		                // 送付先部署
                this.form.SHIHARAI_SOUFU_TANTOU.Text = shiharaiEntity.SHIHARAI_SOUFU_TANTOU;		            // 送付先担当者
                this.form.SHIHARAI_SOUFU_TEL.Text = shiharaiEntity.SHIHARAI_SOUFU_TEL;			                // 送付先電話番号
                this.form.SHIHARAI_SOUFU_FAX.Text = shiharaiEntity.SHIHARAI_SOUFU_FAX;			                // 送付先FAX番号
                if (!shiharaiEntity.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = shiharaiEntity.SHIHARAI_KYOTEN_PRINT_KBN.ToString(); //拠点名を印字
                }
                if (!shiharaiEntity.SHIHARAI_KYOTEN_CD.IsNull)
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = shiharaiEntity.SHIHARAI_KYOTEN_CD.ToString();               //支払書拠点
                    this.ShiharaiKyotenCdValidated();
                }
            }


        }

        /// <summary>
        /// 地域CD検索処理
        /// </summary>
        /// <returns></returns>
        private string GetChiikiCd(M_TORIHIKISAKI torisakiEntity, out string chiikiName)
        {
            string result = string.Empty;
            chiikiName = string.Empty;

            // ①M_TORIHIKISAKI.TORIHIKISAKI_POST, M_TORIHIKISAKI.TORIHIKISAKI_ADDRESS1が空の場合は空を設定し、処理を終了する。
            string tempPost = torisakiEntity.TORIHIKISAKI_POST;
            string tempAddress = torisakiEntity.TORIHIKISAKI_ADDRESS1;
            if (string.IsNullOrWhiteSpace(tempPost) && string.IsNullOrWhiteSpace(tempAddress))
            {
                return result;
            }

            // ②M_TORIHIKISAKI.TORIHIKISAKI_POSTで郵便辞書を検索し、都道府県と市町村を取得する。
            IS_ZIP_CODEDao daoPost = DaoInitUtility.GetComponent<IS_ZIP_CODEDao>();
            S_ZIP_CODE[] postNum = daoPost.GetDataByPost7LikeSearch(tempPost);
            if (postNum.Length <= 0)
            {
                return result;
            }

            string todoufukenBypost = postNum[0].TODOUFUKEN;
            string sityousonBypost = postNum[0].SIKUCHOUSON;

            M_CHIIKI condition = new M_CHIIKI();

            DataTable dtChiiki = new DataTable();

            // 市町村がＮＵＬＬ以外の場合、市町村で検索する
            if (!string.IsNullOrWhiteSpace(sityousonBypost))
            {
                // ③②の検索結果の市町村よりM_CHIIKI.CHIIKI_NAMEを検索し、
                //   結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                condition.CHIIKI_NAME = sityousonBypost;

                dtChiiki = daoChiiki.GetDataBySqlFile(GET_CHIIKI_DATA_SQL, condition);

                if (dtChiiki.Rows.Count == 1)
                {
                    chiikiName = dtChiiki.Rows[0]["CHIIKI_NAME"].ToString();
                    return dtChiiki.Rows[0]["CHIIKI_CD"].ToString();
                }
            }

            // 都道府県がＮＵＬＬ以外の場合、都道府県で検索する
            if (!string.IsNullOrWhiteSpace(todoufukenBypost))
            {
                // ④②の検索結果の都道府県よりM_CHIIKI.CHIIKI_NAMEを検索し、
                //   結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                condition.CHIIKI_NAME = todoufukenBypost;
                dtChiiki = daoChiiki.GetDataBySqlFile(GET_CHIIKI_DATA_SQL, condition);
                if (dtChiiki.Rows.Count == 1)
                {
                    chiikiName = dtChiiki.Rows[0]["CHIIKI_NAME"].ToString();
                    return dtChiiki.Rows[0]["CHIIKI_CD"].ToString();
                }
            }

            // ⑤M_TORIHIKISAKI.TORIHIKISAKI_ADDRESS1から郵便辞書を検索し、都道府県と市町村でグループ化を行う。
            postNum = daoPost.GetDataByJushoLikeSearch(tempAddress);

            // 郵便辞書マスタよりデータが取得できた場合、処理を行う
            if (postNum.Length >= 1)
            {
                string sityousonByaddress = postNum[0].SIKUCHOUSON + postNum[0].OTHER1;

                // 市町村がＮＵＬＬ以外の場合、市町村で検索する
                if (!string.IsNullOrWhiteSpace(postNum[0].SIKUCHOUSON))
                {
                    // ⑥⑤の検索結果が1件または複数件の場合、結果の市町村よりM_CHIIKI.CHIIKI_NAMEを検索し、
                    // 結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                    condition.CHIIKI_NAME = sityousonByaddress;
                    dtChiiki = daoChiiki.GetDataBySqlFile(GET_CHIIKI_DATA_SQL, condition);
                    if (dtChiiki.Rows.Count == 1)
                    {
                        chiikiName = dtChiiki.Rows[0]["CHIIKI_NAME"].ToString();
                        return dtChiiki.Rows[0]["CHIIKI_CD"].ToString();
                    }
                }
            }

            //都道府県＋市区町村＋OTHER1
            postNum = daoPost.GetDataByTodoufukenJushoLikeSearch(tempAddress);

            // 郵便辞書マスタよりデータが取得できない場合、処理を終了する
            if (postNum.Length <= 0)
            {
                return result;
            }

            string todoufukenByaddress = postNum[0].TODOUFUKEN + postNum[0].SIKUCHOUSON + postNum[0].OTHER1;
            // 都道府県がＮＵＬＬ以外の場合、都道府県で検索する
            if (!string.IsNullOrWhiteSpace(postNum[0].TODOUFUKEN))
            {
                // ⑦⑥の検索結果が0件の場合、結果の都道府県よりM_CHIIKI.CHIIKI_NAMEを検索し、
                // 結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                condition.CHIIKI_NAME = todoufukenByaddress;
                dtChiiki = daoChiiki.GetDataBySqlFile(GET_CHIIKI_DATA_SQL, condition);
                if (dtChiiki.Rows.Count == 1)
                {
                    chiikiName = dtChiiki.Rows[0]["CHIIKI_NAME"].ToString();
                    return dtChiiki.Rows[0]["CHIIKI_CD"].ToString();
                }
            }

            // ⑧⑥または⑦の結果が複数件ある場合、空を設定し、処理を終了する。
            // ⑨⑤の検索結果が0件の場合、空を設定し、処理を終了する。
            return result;
        }

        /// <summary>
        /// 業者CD採番処理
        /// </summary>
        /// <returns>最大CD+1</returns>
        public bool Saiban()
        {
            try
            {
                // 業者マスタのCDの最大値+1を取得
                GyoushaMasterAccess gyoushaMasterAccess = new GyoushaMasterAccess(new CustomTextBox(), new object[] { }, new object[] { });
                int keyGyousha = -1;

                var keyGyoushasaibanFlag = gyoushaMasterAccess.IsOverCDLimit(out keyGyousha);

                if (keyGyoushasaibanFlag || keyGyousha < 1)
                {
                    // 採番エラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E041");
                    this.form.GYOUSHA_CD.Text = "";
                }
                else
                {
                    // ゼロパディング後、テキストへ設定
                    this.form.GYOUSHA_CD.Text = String.Format("{0:D" + this.form.GYOUSHA_CD.MaxLength + "}", keyGyousha);
                }
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Saiban", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Saiban", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// ポップアップ用部署情報取得メソッド
        /// </summary>
        /// <param name="eigyouTantouCd"></param>
        public bool SetBushoData(string eigyouTantouCd)
        {
            try
            {
                bool ret = true;
                M_SHAIN condition = new M_SHAIN();
                condition.SHAIN_CD = eigyouTantouCd;
                if (!string.IsNullOrWhiteSpace(this.form.EIGYOU_TANTOU_BUSHO_CD.Text))
                {
                    condition.BUSHO_CD = this.form.EIGYOU_TANTOU_BUSHO_CD.Text;
                }
                DataTable dt = this.daoShain.GetShainDataSqlFile(GET_POPUP_DATA_SQL, condition);
                if (0 < dt.Rows.Count)
                {
                    this.form.EIGYOU_TANTOU_BUSHO_CD.Text = dt.Rows[0]["BUSHO_CD"].ToString();
                    this.form.BUSHO_NAME.Text = dt.Rows[0]["BUSHO_NAME"].ToString();
                    this.form.EIGYOU_TANTOU_CD.Text = dt.Rows[0]["SHAIN_CD"].ToString();
                    this.form.SHAIN_NAME.Text = dt.Rows[0]["SHAIN_NAME"].ToString();
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "営業担当者");
                    this.isError = true;
                    ret = false;
                }
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetBushoData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                this.isError = true;
                return false;
            }
        }

        /// <summary>
        /// 一覧画面表示処理
        /// </summary>
        internal bool ShowIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();

                r_framework.FormManager.FormManager.OpenFormWithAuth("M216", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DENSHU_KBN.GYOUSHA);

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 取引先入力表示処理
        /// </summary>
        internal bool ShowTorihikisakiCreate()
        {
            try
            {
                LogUtility.DebugMethodStart();

                r_framework.FormManager.FormManager.OpenFormWithAuth("M213", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG);

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowTorihikisakiCreate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 現場入力画面の呼び出し
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="newFlg"></param>
        internal bool ShowWindow(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType);

                //選択行からキー項目を取得する
                string cd1 = this.form.GYOUSHA_CD.Text.ToString();
                string cd2 = string.Empty;
                foreach (Row row in this.form.GENBA_ICHIRAN.Rows)
                {
                    if (row.Selected)
                    {
                        cd2 = row.Cells["GENBA_CD"].Value.ToString();
                        break;
                    }
                }

                //現場入力画面を表示する
                if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    // 権限チェック
                    // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
                    if (r_framework.Authority.Manager.CheckAuthority("M217", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        r_framework.FormManager.FormManager.OpenFormWithAuth("M217", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, windowType, cd1, cd2);
                    }
                    else if (r_framework.Authority.Manager.CheckAuthority("M217", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        r_framework.FormManager.FormManager.OpenFormWithAuth("M217", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, cd1, cd2);
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E158", "修正");
                    }
                }
                else
                {
                    // 1次FW ⇒ 2次FW
                    int iWindowType = (int)windowType;
                    r_framework.Const.WINDOW_TYPE newWindowType = (r_framework.Const.WINDOW_TYPE)iWindowType;

                    r_framework.FormManager.FormManager.OpenFormWithAuth("M217", newWindowType, windowType, cd1, cd2);
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowWindow", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 委託契約入力画面の呼び出し
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="newFlg"></param>
        internal bool ShowWindowItaku(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType);

                //選択行からキー項目を取得する
                string cd1 = string.Empty;
                foreach (Row row in this.form.ITAKU_ICHIRAN.Rows)
                {
                    if (row.Selected)
                    {
                        cd1 = row.Cells["ITAKU_SYSTEM_ID"].Value.ToString();
                        break;
                    }
                }

                //委託契約入力画面を表示する
                if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    // 権限チェック
                    // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
                    if (r_framework.Authority.Manager.CheckAuthority("M001", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        r_framework.FormManager.FormManager.OpenFormWithAuth("M001", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, windowType, cd1);
                    }
                    else if (r_framework.Authority.Manager.CheckAuthority("M001", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        r_framework.FormManager.FormManager.OpenFormWithAuth("M001", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, cd1);
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E158", "修正");
                    }
                }
                else
                {
                    // 1次FW ⇒ 2次FW
                    int iWindowType = (int)windowType;
                    r_framework.Const.WINDOW_TYPE newWindowType = (r_framework.Const.WINDOW_TYPE)iWindowType;

                    r_framework.FormManager.FormManager.OpenFormWithAuth("M001", newWindowType, windowType, cd1);
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowWindowItaku", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 請求情報の送付先情報を業者の情報でコピー
        /// </summary>
        public bool GyoushaInfoCopyFromSeikyuuInfo()
        {
            try
            {
                this.form.SEIKYUU_SOUFU_NAME1.Text = this.form.GYOUSHA_NAME1.Text;
                this.form.SEIKYUU_SOUFU_NAME2.Text = this.form.GYOUSHA_NAME2.Text;
                this.form.SEIKYUU_SOUFU_KEISHOU1.Text = this.form.GYOUSHA_KEISHOU1.Text;
                this.form.SEIKYUU_SOUFU_KEISHOU2.Text = this.form.GYOUSHA_KEISHOU2.Text;
                this.form.SEIKYUU_SOUFU_POST.Text = this.form.GYOUSHA_POST.Text;
                this.form.SEIKYUU_SOUFU_ADDRESS1.Text = this.form.TODOUFUKEN_NAME.Text + this.form.GYOUSHA_ADDRESS1.Text;
                this.form.SEIKYUU_SOUFU_ADDRESS2.Text = this.form.GYOUSHA_ADDRESS2.Text;
                this.form.SEIKYUU_SOUFU_BUSHO.Text = this.form.BUSHO.Text;
                this.form.SEIKYUU_SOUFU_TANTOU.Text = this.form.TANTOUSHA.Text;
                this.form.SEIKYUU_SOUFU_TEL.Text = this.form.GYOUSHA_TEL.Text;
                this.form.SEIKYUU_SOUFU_FAX.Text = this.form.GYOUSHA_FAX.Text;

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyoushaInfoCopyFromSeikyuuInfo", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 支払情報の送付先情報を業者の情報でコピー
        /// </summary>
        public bool GyoushaInfoCopyFromShiharaiInfo()
        {
            try
            {
                this.form.SHIHARAI_SOUFU_NAME1.Text = this.form.GYOUSHA_NAME1.Text;
                this.form.SHIHARAI_SOUFU_NAME2.Text = this.form.GYOUSHA_NAME2.Text;
                this.form.SHIHARAI_SOUFU_KEISHOU1.Text = this.form.GYOUSHA_KEISHOU1.Text;
                this.form.SHIHARAI_SOUFU_KEISHOU2.Text = this.form.GYOUSHA_KEISHOU2.Text;
                this.form.SHIHARAI_SOUFU_POST.Text = this.form.GYOUSHA_POST.Text;
                this.form.SHIHARAI_SOUFU_ADDRESS1.Text = this.form.TODOUFUKEN_NAME.Text + this.form.GYOUSHA_ADDRESS1.Text;
                this.form.SHIHARAI_SOUFU_ADDRESS2.Text = this.form.GYOUSHA_ADDRESS2.Text;
                this.form.SHIHARAI_SOUFU_BUSHO.Text = this.form.BUSHO.Text;
                this.form.SHIHARAI_SOUFU_TANTOU.Text = this.form.TANTOUSHA.Text;
                this.form.SHIHARAI_SOUFU_TEL.Text = this.form.GYOUSHA_TEL.Text;
                this.form.SHIHARAI_SOUFU_FAX.Text = this.form.GYOUSHA_FAX.Text;

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyoushaInfoCopyFromShiharaiInfo", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 業者分類の返送先情報を業者の情報でコピー
        /// </summary>
        public bool GyoushaInfoCopyFromGyoushaBunrui()
        {
            try
            {
                this.form.MANI_HENSOUSAKI_NAME1.Text = this.form.GYOUSHA_NAME1.Text;
                this.form.MANI_HENSOUSAKI_NAME2.Text = this.form.GYOUSHA_NAME2.Text;
                this.form.MANI_HENSOUSAKI_KEISHOU1.Text = this.form.GYOUSHA_KEISHOU1.Text;
                this.form.MANI_HENSOUSAKI_KEISHOU2.Text = this.form.GYOUSHA_KEISHOU2.Text;
                this.form.MANI_HENSOUSAKI_POST.Text = this.form.GYOUSHA_POST.Text;
                this.form.MANI_HENSOUSAKI_ADDRESS1.Text = this.form.TODOUFUKEN_NAME.Text + this.form.GYOUSHA_ADDRESS1.Text;
                this.form.MANI_HENSOUSAKI_ADDRESS2.Text = this.form.GYOUSHA_ADDRESS2.Text;
                this.form.MANI_HENSOUSAKI_BUSHO.Text = this.form.BUSHO.Text;
                this.form.MANI_HENSOUSAKI_TANTOU.Text = this.form.TANTOUSHA.Text;

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyoushaInfoCopyFromGyoushaBunrui", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// CustomTextBoxの入力文字数をチェックします
        /// </summary>
        /// <param name="txb">CustomTextBox</param>
        /// <returns>true:制限以内, false:制限超過</returns>
        public bool CheckTextBoxLength(CustomTextBox txb)
        {
            LogUtility.DebugMethodStart(txb);

            bool res = true;

            try
            {
                var mxLenStr = txb.CharactersNumber.ToString();
                if (mxLenStr.Contains(".") || string.IsNullOrEmpty(txb.Text))
                {
                    // 最大桁数指定がおかしい場合はチェックしない
                    return res;
                }

                int mxLen;
                if (!int.TryParse(mxLenStr, out mxLen) || mxLen < 1)
                {
                    // 最大桁数指定がおかしい場合はチェックしない
                    return res;
                }

                var enc = System.Text.Encoding.GetEncoding("Shift_JIS");
                var bytes = enc.GetBytes(txb.Text);
                var txLen = bytes.Length;
                res = !(mxLen < txLen);

                if (!res)
                {
                    // 制限超過の場合はアラートを表示してコントロールにフォーカス
                    var msl = new MessageBoxShowLogic();
                    msl.MessageBoxShow("E152", txb.DisplayItemName, (mxLen / 2).ToString());
                    txb.Select();
                }

                return res;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("CheckTextBoxLength", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(res);
            }
        }

        /// <summary>
        /// 地域判定処理
        /// </summary>
        /// <param name="isUnpanHoukokusyoChange">運搬報告書提出先CDを変更するかを示します</param>
        public bool ChechChiiki(bool isUnpanHoukokusyoChange)
        {
            try
            {
                // 共通地域チェックロジックで地域検索を実行する
                M_CHIIKI chiiki = MasterCommonLogic.SearchChiikiFromAddress(this.form.GYOUSHA_TODOUFUKEN_CD.Text, this.form.GYOUSHA_ADDRESS1.Text);
                if (chiiki != null)
                {
                    this.form.CHIIKI_CD.Text = chiiki.CHIIKI_CD;
                    this.form.CHIIKI_NAME.Text = chiiki.CHIIKI_NAME_RYAKU;
                }
                else
                {
                    this.form.CHIIKI_CD.Text = string.Empty;
                    this.form.CHIIKI_NAME.Text = string.Empty;
                }

                // 運搬報告書提出先
                if (isUnpanHoukokusyoChange)
                {
                    M_CHIIKI houkokuChiiki = MasterCommonLogic.SearchChiikiFromAddress(this.form.GYOUSHA_TODOUFUKEN_CD.Text, string.Empty);
                    if (houkokuChiiki != null)
                    {
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = houkokuChiiki.CHIIKI_CD;
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = houkokuChiiki.CHIIKI_NAME_RYAKU;
                    }
                    else
                    {
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = string.Empty;
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = string.Empty;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechChiiki", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// データ取得処理(拠点)
        /// </summary>
        /// <returns></returns>
        public int SearchsetTorihikisaki()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!string.IsNullOrEmpty(this.TorihikisakiCd)
                    && this.TorihikisakiCd.Equals(this.form.TORIHIKISAKI_CD.Text))
                {
                    this.RenkeiFlg = true;
                }
                else
                {
                    this.RenkeiFlg = false;
                }

                M_TORIHIKISAKI entity = null;
                M_TORIHIKISAKI queryParam = new M_TORIHIKISAKI();
                queryParam.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;

                _tabPageManager.ChangeTabPageVisible(1, true);
                _tabPageManager.ChangeTabPageVisible(2, true);

                M_TORIHIKISAKI_SHIHARAI shiharaiEntity = this.daoShiharai.GetDataByCd(queryParam.TORIHIKISAKI_CD);
                M_TORIHIKISAKI_SEIKYUU seikyuuEntity = this.daoSeikyuu.GetDataByCd(queryParam.TORIHIKISAKI_CD);

                M_TORIHIKISAKI[] result = this.daoTorisaki.GetAllValidData(queryParam);
                if (result != null && result.Length > 0)
                {
                    entity = result[0];
                }

                if (entity == null)
                {
                    this.form.TORIHIKISAKI_NAME1.Text = string.Empty;
                    this.form.TORIHIKISAKI_NAME2.Text = string.Empty;
                    this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                    this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;

                    this.form.KYOTEN_CD.Text = string.Empty;
                    this.form.KYOTEN_NAME.Text = string.Empty;

                    if (this.form.TORIHIKISAKI_CD.Text != "")
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "取引先");
                        this.isError = true;
                        //取引先に移動
                        this.form.TORIHIKISAKI_CD.Focus();
                    }

                    //非活性になっていた場合だけ活性化する。
                    if (!this.form.SEIKYUU_SOUFU_NAME1.Enabled)
                    {
                        this.ChangeTorihikisakiKbn(Const.GyoushaHoshuConstans.TorihikisakiKbnProcessType.Seikyuu, true);
                    }
                    if (!this.form.SHIHARAI_SOUFU_NAME1.Enabled)
                    {
                        this.ChangeTorihikisakiKbn(Const.GyoushaHoshuConstans.TorihikisakiKbnProcessType.Siharai, true);
                    }
                    return 0;
                }

                Boolean isSeikyuuKake = true;
                if (seikyuuEntity != null)
                {
                    // 取引先区分が[1.現金]時には[請求情報タブ]内部を非活性
                    if (seikyuuEntity.TORIHIKI_KBN_CD == 1)
                    {
                        isSeikyuuKake = false;
                    }
                    else if (seikyuuEntity.TORIHIKI_KBN_CD == 2)
                    {
                        isSeikyuuKake = true;
                    }
                }
                //非活性⇔活性
                if ((!isSeikyuuKake && this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled) || (isSeikyuuKake && !this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled))
                {
                    this.ChangeTorihikisakiKbn(Const.GyoushaHoshuConstans.TorihikisakiKbnProcessType.Seikyuu, isSeikyuuKake);
                }

                Boolean isShiharaiKake = false;
                if (shiharaiEntity != null)
                {
                    // 取引先区分が[1.現金]時には[支払情報タブ]内部を非活性
                    if (shiharaiEntity.TORIHIKI_KBN_CD == 1)
                    {
                        isShiharaiKake = false;
                    }
                    else if (shiharaiEntity.TORIHIKI_KBN_CD == 2)
                    {
                        isShiharaiKake = true;
                    }
                }
                //非活性⇔活性
                if ((!isShiharaiKake && this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled) || (isShiharaiKake && !this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled))
                {
                    this.ChangeTorihikisakiKbn(Const.GyoushaHoshuConstans.TorihikisakiKbnProcessType.Siharai, isShiharaiKake);
                }

                if (entity != null)
                {
                    this.form.TORIHIKISAKI_NAME1.Text = entity.TORIHIKISAKI_NAME1;
                    this.form.TORIHIKISAKI_NAME2.Text = entity.TORIHIKISAKI_NAME2;
                    if (entity.TEKIYOU_BEGIN.IsNull)
                    {
                        this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = entity.TEKIYOU_BEGIN;
                    }
                    if (entity.TEKIYOU_END.IsNull)
                    {
                        this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_TEKIYOU_END.Value = entity.TEKIYOU_END;
                    }

                    if (!entity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                    {
                        this.form.KYOTEN_CD.Text = entity.TORIHIKISAKI_KYOTEN_CD.ToString();
                    }

                    if ((!isSeikyuuKake && this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Enabled) || (isSeikyuuKake && !this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Enabled))
                    {
                        if (!seikyuuEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                        {
                            this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = seikyuuEntity.SEIKYUU_DAIHYOU_PRINT_KBN.ToString();  // 代表取締役
                        }
                    }
                }

                this.kyotenEntity = null;

                this.kyotenEntity = daoKyoten.GetDataByCd(this.form.KYOTEN_CD.Text);

                int count = this.kyotenEntity == null ? 0 : 1;

                //拠点名セットを行う
                if (count > 0)
                {
                    this.form.KYOTEN_NAME.Text = this.kyotenEntity.KYOTEN_NAME_RYAKU;
                }

                LogUtility.DebugMethodEnd(count);
                return count;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchsetTorihikisaki", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchsetTorihikisaki", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
        }

        /// <summary>
        /// 取引先区分に基づくコントロールの変更処理
        /// </summary>
        /// <param name="isKake"> 取引先区分が[1.現金]時に true</param>
        public void ChangeTorihikisakiKbn(Const.GyoushaHoshuConstans.TorihikisakiKbnProcessType torihikisakiKbnProcess, Boolean isKake)
        {
            if (torihikisakiKbnProcess == Const.GyoushaHoshuConstans.TorihikisakiKbnProcessType.Seikyuu)
            {
                this.ChangeSeikyuuControl(isKake);
            }
            else if (torihikisakiKbnProcess == Const.GyoushaHoshuConstans.TorihikisakiKbnProcessType.Siharai)
            {
                this.ChangeSiharaiControl(isKake);
            }
        }

        /// <summary>
        /// 取引先区分に基づくコントロールの変更処理(請求)
        /// </summary>
        /// <param name="isKake"> 取引先区分が[1.現金]時に true</param>
        private void ChangeSeikyuuControl(Boolean isKake)
        {
            this.form.SEIKYUU_SOUFU_NAME1.Text = string.Empty;      // 請求書送付先1
            this.form.SEIKYUU_SOUFU_NAME1.Enabled = isKake;

            this.form.SEIKYUU_SOUFU_KEISHOU1.Text = string.Empty;   // 請求書送付先敬称1
            this.form.SEIKYUU_SOUFU_KEISHOU1.Enabled = isKake;

            this.form.SEIKYUU_SOUFU_NAME2.Text = string.Empty;      // 請求書送付先2
            this.form.SEIKYUU_SOUFU_NAME2.Enabled = isKake;

            this.form.SEIKYUU_SOUFU_KEISHOU2.Text = string.Empty;   // 請求書送付先敬称2
            this.form.SEIKYUU_SOUFU_KEISHOU2.Enabled = isKake;

            this.form.SEIKYUU_SOUFU_POST.Text = string.Empty;       // 送付先郵便番号
            this.form.SEIKYUU_SOUFU_POST.Enabled = isKake;

            this.form.SEIKYUU_SOUFU_ADDRESS1.Text = string.Empty;   // 送付先住所１
            this.form.SEIKYUU_SOUFU_ADDRESS1.Enabled = isKake;

            this.form.SEIKYUU_SOUFU_ADDRESS2.Text = string.Empty;   // 送付先住所２
            this.form.SEIKYUU_SOUFU_ADDRESS2.Enabled = isKake;

            this.form.SEIKYUU_SOUFU_BUSHO.Text = string.Empty;      // 送付先部署
            this.form.SEIKYUU_SOUFU_BUSHO.Enabled = isKake;

            this.form.SEIKYUU_SOUFU_TANTOU.Text = string.Empty;     // 送付先担当者
            this.form.SEIKYUU_SOUFU_TANTOU.Enabled = isKake;

            this.form.SEIKYUU_SOUFU_TEL.Text = string.Empty;        // 送付先電話番号
            this.form.SEIKYUU_SOUFU_TEL.Enabled = isKake;

            this.form.SEIKYUU_SOUFU_FAX.Text = string.Empty;        // 送付先FAX番号
            this.form.SEIKYUU_SOUFU_FAX.Enabled = isKake;

            this.form.SEIKYUU_TANTOU.Text = string.Empty;           // 請求担当者
            this.form.SEIKYUU_TANTOU.Enabled = isKake;

            // 請求書代表印字区分
            if (!isKake)
            {
                this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;
            }
            else if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
            {
                this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.sysinfoEntity.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.ToString();
            }

            this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Enabled = isKake;
            this.form.SEIKYUU_DAIHYOU_PRINT_KBN_1.Enabled = isKake;
            this.form.SEIKYUU_DAIHYOU_PRINT_KBN_2.Enabled = isKake;

            // 請求書拠点印字区分
            if (!isKake)
            {
                this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
            }
            else if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
            {
                this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN.ToString();
            }
            this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled = isKake;
            this.form.SEIKYUU_KYOTEN_PRINT_KBN_1.Enabled = isKake;
            this.form.SEIKYUU_KYOTEN_PRINT_KBN_2.Enabled = isKake;

            // 請求書拠点CD
            if (!isKake)
            {
                this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
            }
            else if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.IsNull && isKake != this.form.SEIKYUU_KYOTEN_CD.Enabled && this.form.SEIKYUU_KYOTEN_CD.Text.Equals(string.Empty))
            {
                this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.ToString()));
            }

            M_KYOTEN seikyuuKyoten = this.daoKyoten.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
            if (this.form.SEIKYUU_KYOTEN_CD.Text != string.Empty)
            {
                if (seikyuuKyoten != null)
                {
                    this.form.SEIKYUU_KYOTEN_NAME.Text = seikyuuKyoten.KYOTEN_NAME_RYAKU;
                }
                else 
                {
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                }
            }

            this.ChangeSeikyuuKyotenPrintKbn();

            this.form.bt_souhusaki_torihikisaki_copy.Enabled = isKake;              // 請求書業者情報コピー
            this.form.bt_souhusaki_address.Enabled = isKake;                        // 請求書住所参照
            this.form.bt_souhusaki_post.Enabled = isKake;                           // 請求書郵便番号参照
        }

        /// <summary>
        /// 取引先区分に基づくコントロールの変更処理(支払)
        /// </summary>
        /// <param name="isKake"> 取引先区分が[1.現金]時に true</param>
        private void ChangeSiharaiControl(Boolean isKake)
        {
            this.form.SHIHARAI_SOUFU_NAME1.Text = string.Empty;         // 支払明細書送付先1
            this.form.SHIHARAI_SOUFU_NAME1.Enabled = isKake;

            this.form.SHIHARAI_SOUFU_KEISHOU1.Text = string.Empty;      // 支払明細書送付先敬称1
            this.form.SHIHARAI_SOUFU_KEISHOU1.Enabled = isKake;

            this.form.SHIHARAI_SOUFU_NAME2.Text = string.Empty;         // 支払明細書送付先2
            this.form.SHIHARAI_SOUFU_NAME2.Enabled = isKake;

            this.form.SHIHARAI_SOUFU_KEISHOU2.Text = string.Empty;      // 支払明細書送付先敬称2
            this.form.SHIHARAI_SOUFU_KEISHOU2.Enabled = isKake;

            this.form.SHIHARAI_SOUFU_POST.Text = string.Empty;          // 送付先郵便番号
            this.form.SHIHARAI_SOUFU_POST.Enabled = isKake;

            this.form.SHIHARAI_SOUFU_ADDRESS1.Text = string.Empty;      // 送付先住所１
            this.form.SHIHARAI_SOUFU_ADDRESS1.Enabled = isKake;

            this.form.SHIHARAI_SOUFU_ADDRESS2.Text = string.Empty;      // 送付先住所２
            this.form.SHIHARAI_SOUFU_ADDRESS2.Enabled = isKake;

            this.form.SHIHARAI_SOUFU_BUSHO.Text = string.Empty;         // 送付先部署
            this.form.SHIHARAI_SOUFU_BUSHO.Enabled = isKake;

            this.form.SHIHARAI_SOUFU_TANTOU.Text = string.Empty;        // 送付先担当者
            this.form.SHIHARAI_SOUFU_TANTOU.Enabled = isKake;

            this.form.SHIHARAI_SOUFU_TEL.Text = string.Empty;           // 送付先電話番号
            this.form.SHIHARAI_SOUFU_TEL.Enabled = isKake;

            this.form.SHIHARAI_SOUFU_FAX.Text = string.Empty;           // 送付先FAX番号
            this.form.SHIHARAI_SOUFU_FAX.Enabled = isKake;

            // 支払書拠点印字区分
            if (!isKake)
            {
                this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
            }
            else if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
            {
                this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN.ToString();
            }
            this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled = isKake;
            this.form.SHIHARAI_KYOTEN_PRINT_KBN_1.Enabled = isKake;
            this.form.SHIHARAI_KYOTEN_PRINT_KBN_2.Enabled = isKake;

            // 支払書拠点CD
            if (!isKake)
            {
                this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
            }
            else if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.IsNull && isKake != this.form.SHIHARAI_KYOTEN_CD.Enabled && this.form.SHIHARAI_KYOTEN_CD.Text.Equals(string.Empty))
            {
                this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.ToString()));
            }

            M_KYOTEN shiharaiKyoten = this.daoKyoten.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
            if (this.form.SHIHARAI_KYOTEN_CD.Text != string.Empty)
            {
                if (shiharaiKyoten != null)
                {
                    this.form.SHIHARAI_KYOTEN_NAME.Text = shiharaiKyoten.KYOTEN_NAME_RYAKU;
                }
                else 
                {
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                }

            }

            this.ChangeShiharaiKyotenPrintKbn();

            this.form.bt_shiharai_souhusaki_torihikisaki_copy.Enabled = isKake;     // 支払書業者情報コピー
            this.form.bt_shiharai_address.Enabled = isKake;                         // 支払書住所参照
            this.form.bt_shiharai_post.Enabled = isKake;                            // 支払書郵便番号参照
        }

        /// <summary>
        /// 請求拠点印字区分変更処理
        /// </summary>
        public bool ChangeSeikyuuKyotenPrintKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text.Equals("1"))
                {
                    this.form.SEIKYUU_KYOTEN_CD.Enabled = true;
                    this.form.SEIKYUU_KYOTEN_CD_SEARCH.Enabled = true;
                    if (!sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.IsNull && !string.IsNullOrWhiteSpace(sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.ToString()) && this.form.SEIKYUU_KYOTEN_CD.Text.Equals(string.Empty))
                    {
                        //システム設定データの読み込み
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.ToString()));
                        this.SeikyuuKyotenCdValidated();
                    }
                }
                else
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_CD.Enabled = false;
                    this.form.SEIKYUU_KYOTEN_CD_SEARCH.Enabled = false;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeSeikyuuKyotenPrintKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 支払拠点印字区分変更処理
        /// </summary>
        public bool ChangeShiharaiKyotenPrintKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text.Equals("1"))
                {
                    this.form.SHIHARAI_KYOTEN_CD.Enabled = true;
                    this.form.SHIHARAI_KYOTEN_CD_SEARCH.Enabled = true;
                    if (!sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.IsNull && !string.IsNullOrWhiteSpace(sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.ToString()) && this.form.SHIHARAI_KYOTEN_CD.Text.Equals(string.Empty))
                    {
                        //システム設定値の読み取り
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.ToString()));
                        this.ShiharaiKyotenCdValidated();
                    }
                }
                else
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_CD.Enabled = false;
                    this.form.SHIHARAI_KYOTEN_CD_SEARCH.Enabled = false;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeShiharaiKyotenPrintKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 請求書拠点の値チェック
        /// </summary>
        public bool SeikyuuKyotenCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                if (!string.IsNullOrEmpty(this.form.SEIKYUU_KYOTEN_CD.Text))
                {
                    M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                    if (kyo != null && kyo.KYOTEN_CD != 99 && !kyo.DELETE_FLG.IsTrue)
                    {
                        string padData = kyo.KYOTEN_CD.ToString().PadLeft((int)this.form.SEIKYUU_KYOTEN_CD.CharactersNumber, '0');
                        this.form.SEIKYUU_KYOTEN_CD.Text = padData;
                        this.form.SEIKYUU_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "拠点");
                        this.isError = true;
                        ret = false;
                    }
                }

                if (this.isError)
                {
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                this.isError = true;
                LogUtility.Error("SeikyuuKyotenCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 支払書拠点の値チェック
        /// </summary>
        public bool ShiharaiKyotenCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                if (!string.IsNullOrEmpty(this.form.SHIHARAI_KYOTEN_CD.Text))
                {
                    M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                    if (kyo != null && kyo.KYOTEN_CD != 99 && !kyo.DELETE_FLG.IsTrue)
                    {
                        string padData = kyo.KYOTEN_CD.ToString().PadLeft((int)this.form.SHIHARAI_KYOTEN_CD.CharactersNumber, '0');
                        this.form.SHIHARAI_KYOTEN_CD.Text = padData;
                        this.form.SHIHARAI_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "拠点");
                        this.isError = true;
                        ret = false;
                    }
                }

                if (this.isError)
                {
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                this.isError = true;
                LogUtility.Error("ShiharaiKyotenCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 営業拠点部署の値チェック
        /// </summary>
        public bool BushoCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                this.form.BUSHO_NAME.Text = string.Empty;
                if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOU_BUSHO_CD.Text))
                {
                    M_BUSHO search = new M_BUSHO();
                    search.BUSHO_CD = this.form.EIGYOU_TANTOU_BUSHO_CD.Text;
                    M_BUSHO[] busho = this.daoBusho.GetAllValidData(search);
                    if (busho != null && busho.Length > 0 && !busho[0].BUSHO_CD.Equals("999"))
                    {
                        this.form.EIGYOU_TANTOU_BUSHO_CD.Text = busho[0].BUSHO_CD.ToString();
                        this.form.BUSHO_NAME.Text = busho[0].BUSHO_NAME_RYAKU.ToString();
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "部署");
                        this.isError = true;
                        ret = false;
                    }
                }

                if (this.isError)
                {
                    this.form.EIGYOU_TANTOU_BUSHO_CD.SelectAll();
                    this.form.BUSHO_NAME.Text = string.Empty;
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                this.isError = true;
                LogUtility.Error("BushoCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 引合業者エンティティを取得します
        /// </summary>
        /// <returns>取得した件数</returns>
        private int SearchHikiaiGyousha()
        {
            LogUtility.DebugMethodStart();

            var ret = 0;
            var dao = DaoInitUtility.GetComponent<IM_HIKIAI_GYOUSHADao>();
            var entityList = dao.GetHikiaiGyoushaList(new M_HIKIAI_GYOUSHA() { GYOUSHA_CD = this.GyoushaCd, DELETE_FLG = false });
            if (0 != entityList.Count())
            {
                this.LoadHikiaiGyoushaEntity = entityList.FirstOrDefault();
                ret = 1;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 仮業者エンティティを取得します
        /// </summary>
        /// <returns>取得した件数</returns>
        private int SearchKariGyousha()
        {
            LogUtility.DebugMethodStart();

            var ret = 0;
            var dao = DaoInitUtility.GetComponent<IM_KARI_GYOUSHADao>();
            var entityList = dao.GetKariGyoushaList(new M_KARI_GYOUSHA() { GYOUSHA_CD = this.GyoushaCd, DELETE_FLG = false });
            if (0 != entityList.Count())
            {
                this.LoadKariGyoushaEntity = entityList.FirstOrDefault();
                ret = 1;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 引合業者エンティティから業者エンティティに内容をコピーします
        /// </summary>
        private void CopyHikiaiGyoushaEntityToGyoushaEntity()
        {
            LogUtility.DebugMethodStart();

            this.GyoushaEntity = new M_GYOUSHA();
            this.GyoushaEntity.BIKOU1 = this.LoadHikiaiGyoushaEntity.BIKOU1;
            this.GyoushaEntity.BIKOU2 = this.LoadHikiaiGyoushaEntity.BIKOU2;
            this.GyoushaEntity.BIKOU3 = this.LoadHikiaiGyoushaEntity.BIKOU3;
            this.GyoushaEntity.BIKOU4 = this.LoadHikiaiGyoushaEntity.BIKOU4;
            this.GyoushaEntity.BUSHO = this.LoadHikiaiGyoushaEntity.BUSHO;
            this.GyoushaEntity.CHIIKI_CD = this.LoadHikiaiGyoushaEntity.CHIIKI_CD;
            this.GyoushaEntity.CHUUSHI_RIYUU1 = this.LoadHikiaiGyoushaEntity.CHUUSHI_RIYUU1;
            this.GyoushaEntity.CHUUSHI_RIYUU2 = this.LoadHikiaiGyoushaEntity.CHUUSHI_RIYUU2;
            this.GyoushaEntity.CREATE_DATE = this.LoadHikiaiGyoushaEntity.CREATE_DATE;
            this.GyoushaEntity.CREATE_PC = this.LoadHikiaiGyoushaEntity.CREATE_PC;
            this.GyoushaEntity.CREATE_USER = this.LoadHikiaiGyoushaEntity.CREATE_USER;
            this.GyoushaEntity.DELETE_FLG = this.LoadHikiaiGyoushaEntity.DELETE_FLG;
            this.GyoushaEntity.EIGYOU_TANTOU_BUSHO_CD = this.LoadHikiaiGyoushaEntity.EIGYOU_TANTOU_BUSHO_CD;
            this.GyoushaEntity.EIGYOU_TANTOU_CD = this.LoadHikiaiGyoushaEntity.EIGYOU_TANTOU_CD;
            this.GyoushaEntity.GYOUSHA_ADDRESS1 = this.LoadHikiaiGyoushaEntity.GYOUSHA_ADDRESS1;
            this.GyoushaEntity.GYOUSHA_ADDRESS2 = this.LoadHikiaiGyoushaEntity.GYOUSHA_ADDRESS2;
            this.GyoushaEntity.GYOUSHA_CD = this.LoadHikiaiGyoushaEntity.GYOUSHA_CD;
            this.GyoushaEntity.GYOUSHA_DAIHYOU = this.LoadHikiaiGyoushaEntity.GYOUSHA_DAIHYOU;
            this.GyoushaEntity.GYOUSHA_FAX = this.LoadHikiaiGyoushaEntity.GYOUSHA_FAX;
            this.GyoushaEntity.GYOUSHA_FURIGANA = this.LoadHikiaiGyoushaEntity.GYOUSHA_FURIGANA;
            this.GyoushaEntity.GYOUSHA_KEISHOU1 = this.LoadHikiaiGyoushaEntity.GYOUSHA_KEISHOU1;
            this.GyoushaEntity.GYOUSHA_KEISHOU2 = this.LoadHikiaiGyoushaEntity.GYOUSHA_KEISHOU2;
            this.GyoushaEntity.GYOUSHA_KEITAI_TEL = this.LoadHikiaiGyoushaEntity.GYOUSHA_KEITAI_TEL;
            this.GyoushaEntity.GYOUSHA_NAME_RYAKU = this.LoadHikiaiGyoushaEntity.GYOUSHA_NAME_RYAKU;
            this.GyoushaEntity.GYOUSHA_NAME1 = this.LoadHikiaiGyoushaEntity.GYOUSHA_NAME1;
            this.GyoushaEntity.GYOUSHA_NAME2 = this.LoadHikiaiGyoushaEntity.GYOUSHA_NAME2;
            this.GyoushaEntity.GYOUSHA_POST = this.LoadHikiaiGyoushaEntity.GYOUSHA_POST;
            this.GyoushaEntity.GYOUSHA_TEL = this.LoadHikiaiGyoushaEntity.GYOUSHA_TEL;
            this.GyoushaEntity.GYOUSHA_TODOUFUKEN_CD = this.LoadHikiaiGyoushaEntity.GYOUSHA_TODOUFUKEN_CD;
            this.GyoushaEntity.GYOUSHAKBN_MANI = this.LoadHikiaiGyoushaEntity.GYOUSHAKBN_MANI;
            this.GyoushaEntity.GYOUSHAKBN_SHUKKA = this.LoadHikiaiGyoushaEntity.GYOUSHAKBN_SHUKKA;
            this.GyoushaEntity.GYOUSHAKBN_UKEIRE = this.LoadHikiaiGyoushaEntity.GYOUSHAKBN_UKEIRE;
            this.GyoushaEntity.GYOUSHU_CD = this.LoadHikiaiGyoushaEntity.GYOUSHU_CD;
            //20250320
            this.GyoushaEntity.URIAGE_GURUPU_CD = this.LoadHikiaiGyoushaEntity.URIAGE_GURUPU_CD;
            this.GyoushaEntity.SHIHARAI_GURUPU_CD = this.LoadHikiaiGyoushaEntity.SHIHARAI_GURUPU_CD;

            this.GyoushaEntity.JISHA_KBN = this.LoadHikiaiGyoushaEntity.JISHA_KBN;
            this.GyoushaEntity.KYOTEN_CD = this.LoadHikiaiGyoushaEntity.KYOTEN_CD;
            this.GyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = this.LoadHikiaiGyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN;
            this.GyoushaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.LoadHikiaiGyoushaEntity.MANI_HENSOUSAKI_ADDRESS1;
            this.GyoushaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.LoadHikiaiGyoushaEntity.MANI_HENSOUSAKI_ADDRESS2;
            this.GyoushaEntity.MANI_HENSOUSAKI_BUSHO = this.LoadHikiaiGyoushaEntity.MANI_HENSOUSAKI_BUSHO;
            this.GyoushaEntity.MANI_HENSOUSAKI_KBN = this.LoadHikiaiGyoushaEntity.MANI_HENSOUSAKI_KBN;
            this.GyoushaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.LoadHikiaiGyoushaEntity.MANI_HENSOUSAKI_KEISHOU1;
            this.GyoushaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.LoadHikiaiGyoushaEntity.MANI_HENSOUSAKI_KEISHOU2;
            this.GyoushaEntity.MANI_HENSOUSAKI_NAME1 = this.LoadHikiaiGyoushaEntity.MANI_HENSOUSAKI_NAME1;
            this.GyoushaEntity.MANI_HENSOUSAKI_NAME2 = this.LoadHikiaiGyoushaEntity.MANI_HENSOUSAKI_NAME2;
            this.GyoushaEntity.MANI_HENSOUSAKI_POST = this.LoadHikiaiGyoushaEntity.MANI_HENSOUSAKI_POST;
            this.GyoushaEntity.MANI_HENSOUSAKI_TANTOU = this.LoadHikiaiGyoushaEntity.MANI_HENSOUSAKI_TANTOU;
            this.GyoushaEntity.SEARCH_CREATE_DATE = this.LoadHikiaiGyoushaEntity.SEARCH_CREATE_DATE;
            this.GyoushaEntity.SEARCH_TEKIYOU_BEGIN = this.LoadHikiaiGyoushaEntity.SEARCH_TEKIYOU_BEGIN;
            this.GyoushaEntity.SEARCH_TEKIYOU_END = this.LoadHikiaiGyoushaEntity.SEARCH_TEKIYOU_END;
            this.GyoushaEntity.SEARCH_UPDATE_DATE = this.LoadHikiaiGyoushaEntity.SEARCH_UPDATE_DATE;
            this.GyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN = this.LoadHikiaiGyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN;
            this.GyoushaEntity.SEIKYUU_KYOTEN_CD = this.LoadHikiaiGyoushaEntity.SEIKYUU_KYOTEN_CD;
            this.GyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN = this.LoadHikiaiGyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN;
            this.GyoushaEntity.SEIKYUU_SOUFU_ADDRESS1 = this.LoadHikiaiGyoushaEntity.SEIKYUU_SOUFU_ADDRESS1;
            this.GyoushaEntity.SEIKYUU_SOUFU_ADDRESS2 = this.LoadHikiaiGyoushaEntity.SEIKYUU_SOUFU_ADDRESS2;
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            this.GyoushaEntity.HAKKOUSAKI_CD = this.LoadHikiaiGyoushaEntity.HAKKOUSAKI_CD;
            // 20160429 koukoukon v2.1_電子請求書 #16612 end
            this.GyoushaEntity.SEIKYUU_SOUFU_BUSHO = this.LoadHikiaiGyoushaEntity.SEIKYUU_SOUFU_BUSHO;
            this.GyoushaEntity.SEIKYUU_SOUFU_FAX = this.LoadHikiaiGyoushaEntity.SEIKYUU_SOUFU_FAX;
            this.GyoushaEntity.SEIKYUU_SOUFU_KEISHOU1 = this.LoadHikiaiGyoushaEntity.SEIKYUU_SOUFU_KEISHOU1;
            this.GyoushaEntity.SEIKYUU_SOUFU_KEISHOU2 = this.LoadHikiaiGyoushaEntity.SEIKYUU_SOUFU_KEISHOU2;
            this.GyoushaEntity.SEIKYUU_SOUFU_NAME1 = this.LoadHikiaiGyoushaEntity.SEIKYUU_SOUFU_NAME1;
            this.GyoushaEntity.SEIKYUU_SOUFU_NAME2 = this.LoadHikiaiGyoushaEntity.SEIKYUU_SOUFU_NAME2;
            this.GyoushaEntity.SEIKYUU_SOUFU_POST = this.LoadHikiaiGyoushaEntity.SEIKYUU_SOUFU_POST;
            this.GyoushaEntity.SEIKYUU_SOUFU_TANTOU = this.LoadHikiaiGyoushaEntity.SEIKYUU_SOUFU_TANTOU;
            this.GyoushaEntity.SEIKYUU_SOUFU_TEL = this.LoadHikiaiGyoushaEntity.SEIKYUU_SOUFU_TEL;
            this.GyoushaEntity.SEIKYUU_TANTOU = this.LoadHikiaiGyoushaEntity.SEIKYUU_TANTOU;
            this.GyoushaEntity.SHIHARAI_KYOTEN_CD = this.LoadHikiaiGyoushaEntity.SHIHARAI_KYOTEN_CD;
            this.GyoushaEntity.SHIHARAI_KYOTEN_PRINT_KBN = this.LoadHikiaiGyoushaEntity.SHIHARAI_KYOTEN_PRINT_KBN;
            this.GyoushaEntity.SHIHARAI_SOUFU_ADDRESS1 = this.LoadHikiaiGyoushaEntity.SHIHARAI_SOUFU_ADDRESS1;
            this.GyoushaEntity.SHIHARAI_SOUFU_ADDRESS2 = this.LoadHikiaiGyoushaEntity.SHIHARAI_SOUFU_ADDRESS2;
            this.GyoushaEntity.SHIHARAI_SOUFU_BUSHO = this.LoadHikiaiGyoushaEntity.SHIHARAI_SOUFU_BUSHO;
            this.GyoushaEntity.SHIHARAI_SOUFU_FAX = this.LoadHikiaiGyoushaEntity.SHIHARAI_SOUFU_FAX;
            this.GyoushaEntity.SHIHARAI_SOUFU_KEISHOU1 = this.LoadHikiaiGyoushaEntity.SHIHARAI_SOUFU_KEISHOU1;
            this.GyoushaEntity.SHIHARAI_SOUFU_KEISHOU2 = this.LoadHikiaiGyoushaEntity.SHIHARAI_SOUFU_KEISHOU2;
            this.GyoushaEntity.SHIHARAI_SOUFU_NAME1 = this.LoadHikiaiGyoushaEntity.SHIHARAI_SOUFU_NAME1;
            this.GyoushaEntity.SHIHARAI_SOUFU_NAME2 = this.LoadHikiaiGyoushaEntity.SHIHARAI_SOUFU_NAME2;
            this.GyoushaEntity.SHIHARAI_SOUFU_POST = this.LoadHikiaiGyoushaEntity.SHIHARAI_SOUFU_POST;
            this.GyoushaEntity.SHIHARAI_SOUFU_TANTOU = this.LoadHikiaiGyoushaEntity.SHIHARAI_SOUFU_TANTOU;
            this.GyoushaEntity.SHIHARAI_SOUFU_TEL = this.LoadHikiaiGyoushaEntity.SHIHARAI_SOUFU_TEL;
            this.GyoushaEntity.SHOKUCHI_KBN = this.LoadHikiaiGyoushaEntity.SHOKUCHI_KBN;
            this.GyoushaEntity.SHUUKEI_ITEM_CD = this.LoadHikiaiGyoushaEntity.SHUUKEI_ITEM_CD;
            this.GyoushaEntity.TANTOUSHA = this.LoadHikiaiGyoushaEntity.TANTOUSHA;
            this.GyoushaEntity.TEKIYOU_BEGIN = this.LoadHikiaiGyoushaEntity.TEKIYOU_BEGIN;
            this.GyoushaEntity.TEKIYOU_END = this.LoadHikiaiGyoushaEntity.TEKIYOU_END;
            this.GyoushaEntity.TORIHIKI_JOUKYOU = this.LoadHikiaiGyoushaEntity.TORIHIKI_JOUKYOU;
            this.GyoushaEntity.TORIHIKISAKI_CD = this.LoadHikiaiGyoushaEntity.TORIHIKISAKI_CD;
            this.GyoushaEntity.TORIHIKISAKI_UMU_KBN = this.LoadHikiaiGyoushaEntity.TORIHIKISAKI_UMU_KBN;
            this.GyoushaEntity.UPDATE_DATE = this.LoadHikiaiGyoushaEntity.UPDATE_DATE;
            this.GyoushaEntity.UPDATE_PC = this.LoadHikiaiGyoushaEntity.UPDATE_PC;
            this.GyoushaEntity.UPDATE_USER = this.LoadHikiaiGyoushaEntity.UPDATE_USER;
            this.GyoushaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = this.LoadHikiaiGyoushaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD;
            this.GyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN = this.LoadHikiaiGyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN;
            this.GyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN = this.LoadHikiaiGyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN;
            this.GyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN = this.LoadHikiaiGyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 仮業者エンティティから業者エンティティに内容をコピーします
        /// </summary>
        private void CopyKariGyoushaEntityToGyoushaEntity()
        {
            LogUtility.DebugMethodStart();

            this.GyoushaEntity = new M_GYOUSHA();
            this.GyoushaEntity.BIKOU1 = this.LoadKariGyoushaEntity.BIKOU1;
            this.GyoushaEntity.BIKOU2 = this.LoadKariGyoushaEntity.BIKOU2;
            this.GyoushaEntity.BIKOU3 = this.LoadKariGyoushaEntity.BIKOU3;
            this.GyoushaEntity.BIKOU4 = this.LoadKariGyoushaEntity.BIKOU4;
            this.GyoushaEntity.BUSHO = this.LoadKariGyoushaEntity.BUSHO;
            this.GyoushaEntity.CHIIKI_CD = this.LoadKariGyoushaEntity.CHIIKI_CD;
            this.GyoushaEntity.CHUUSHI_RIYUU1 = this.LoadKariGyoushaEntity.CHUUSHI_RIYUU1;
            this.GyoushaEntity.CHUUSHI_RIYUU2 = this.LoadKariGyoushaEntity.CHUUSHI_RIYUU2;
            this.GyoushaEntity.CREATE_DATE = this.LoadKariGyoushaEntity.CREATE_DATE;
            this.GyoushaEntity.CREATE_PC = this.LoadKariGyoushaEntity.CREATE_PC;
            this.GyoushaEntity.CREATE_USER = this.LoadKariGyoushaEntity.CREATE_USER;
            this.GyoushaEntity.DELETE_FLG = this.LoadKariGyoushaEntity.DELETE_FLG;
            this.GyoushaEntity.EIGYOU_TANTOU_BUSHO_CD = this.LoadKariGyoushaEntity.EIGYOU_TANTOU_BUSHO_CD;
            this.GyoushaEntity.EIGYOU_TANTOU_CD = this.LoadKariGyoushaEntity.EIGYOU_TANTOU_CD;
            this.GyoushaEntity.GYOUSHA_ADDRESS1 = this.LoadKariGyoushaEntity.GYOUSHA_ADDRESS1;
            this.GyoushaEntity.GYOUSHA_ADDRESS2 = this.LoadKariGyoushaEntity.GYOUSHA_ADDRESS2;
            this.GyoushaEntity.GYOUSHA_CD = this.LoadKariGyoushaEntity.GYOUSHA_CD;
            this.GyoushaEntity.GYOUSHA_DAIHYOU = this.LoadKariGyoushaEntity.GYOUSHA_DAIHYOU;
            this.GyoushaEntity.GYOUSHA_FAX = this.LoadKariGyoushaEntity.GYOUSHA_FAX;
            this.GyoushaEntity.GYOUSHA_FURIGANA = this.LoadKariGyoushaEntity.GYOUSHA_FURIGANA;
            this.GyoushaEntity.GYOUSHA_KEISHOU1 = this.LoadKariGyoushaEntity.GYOUSHA_KEISHOU1;
            this.GyoushaEntity.GYOUSHA_KEISHOU2 = this.LoadKariGyoushaEntity.GYOUSHA_KEISHOU2;
            this.GyoushaEntity.GYOUSHA_KEITAI_TEL = this.LoadKariGyoushaEntity.GYOUSHA_KEITAI_TEL;
            this.GyoushaEntity.GYOUSHA_NAME_RYAKU = this.LoadKariGyoushaEntity.GYOUSHA_NAME_RYAKU;
            this.GyoushaEntity.GYOUSHA_NAME1 = this.LoadKariGyoushaEntity.GYOUSHA_NAME1;
            this.GyoushaEntity.GYOUSHA_NAME2 = this.LoadKariGyoushaEntity.GYOUSHA_NAME2;
            this.GyoushaEntity.GYOUSHA_POST = this.LoadKariGyoushaEntity.GYOUSHA_POST;
            this.GyoushaEntity.GYOUSHA_TEL = this.LoadKariGyoushaEntity.GYOUSHA_TEL;
            this.GyoushaEntity.GYOUSHA_TODOUFUKEN_CD = this.LoadKariGyoushaEntity.GYOUSHA_TODOUFUKEN_CD;
            this.GyoushaEntity.GYOUSHAKBN_MANI = this.LoadKariGyoushaEntity.GYOUSHAKBN_MANI;
            this.GyoushaEntity.GYOUSHAKBN_SHUKKA = this.LoadKariGyoushaEntity.GYOUSHAKBN_SHUKKA;
            this.GyoushaEntity.GYOUSHAKBN_UKEIRE = this.LoadKariGyoushaEntity.GYOUSHAKBN_UKEIRE;
            this.GyoushaEntity.GYOUSHU_CD = this.LoadKariGyoushaEntity.GYOUSHU_CD;
            //20250320
            this.GyoushaEntity.URIAGE_GURUPU_CD = this.LoadKariGyoushaEntity.URIAGE_GURUPU_CD;
            this.GyoushaEntity.SHIHARAI_GURUPU_CD = this.LoadKariGyoushaEntity.SHIHARAI_GURUPU_CD;

            this.GyoushaEntity.JISHA_KBN = this.LoadKariGyoushaEntity.JISHA_KBN;
            this.GyoushaEntity.KYOTEN_CD = this.LoadKariGyoushaEntity.KYOTEN_CD;
            this.GyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = this.LoadKariGyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN;
            this.GyoushaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.LoadKariGyoushaEntity.MANI_HENSOUSAKI_ADDRESS1;
            this.GyoushaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.LoadKariGyoushaEntity.MANI_HENSOUSAKI_ADDRESS2;
            this.GyoushaEntity.MANI_HENSOUSAKI_BUSHO = this.LoadKariGyoushaEntity.MANI_HENSOUSAKI_BUSHO;
            this.GyoushaEntity.MANI_HENSOUSAKI_KBN = this.LoadKariGyoushaEntity.MANI_HENSOUSAKI_KBN;
            this.GyoushaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.LoadKariGyoushaEntity.MANI_HENSOUSAKI_KEISHOU1;
            this.GyoushaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.LoadKariGyoushaEntity.MANI_HENSOUSAKI_KEISHOU2;
            this.GyoushaEntity.MANI_HENSOUSAKI_NAME1 = this.LoadKariGyoushaEntity.MANI_HENSOUSAKI_NAME1;
            this.GyoushaEntity.MANI_HENSOUSAKI_NAME2 = this.LoadKariGyoushaEntity.MANI_HENSOUSAKI_NAME2;
            this.GyoushaEntity.MANI_HENSOUSAKI_POST = this.LoadKariGyoushaEntity.MANI_HENSOUSAKI_POST;
            this.GyoushaEntity.MANI_HENSOUSAKI_TANTOU = this.LoadKariGyoushaEntity.MANI_HENSOUSAKI_TANTOU;
            this.GyoushaEntity.SEARCH_CREATE_DATE = this.LoadKariGyoushaEntity.SEARCH_CREATE_DATE;
            this.GyoushaEntity.SEARCH_TEKIYOU_BEGIN = this.LoadKariGyoushaEntity.SEARCH_TEKIYOU_BEGIN;
            this.GyoushaEntity.SEARCH_TEKIYOU_END = this.LoadKariGyoushaEntity.SEARCH_TEKIYOU_END;
            this.GyoushaEntity.SEARCH_UPDATE_DATE = this.LoadKariGyoushaEntity.SEARCH_UPDATE_DATE;
            this.GyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN = this.LoadKariGyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN;
            this.GyoushaEntity.SEIKYUU_KYOTEN_CD = this.LoadKariGyoushaEntity.SEIKYUU_KYOTEN_CD;
            this.GyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN = this.LoadKariGyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN;
            this.GyoushaEntity.SEIKYUU_SOUFU_ADDRESS1 = this.LoadKariGyoushaEntity.SEIKYUU_SOUFU_ADDRESS1;
            this.GyoushaEntity.SEIKYUU_SOUFU_ADDRESS2 = this.LoadKariGyoushaEntity.SEIKYUU_SOUFU_ADDRESS2;
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            this.GyoushaEntity.HAKKOUSAKI_CD = this.LoadKariGyoushaEntity.HAKKOUSAKI_CD;
            // 20160429 koukoukon v2.1_電子請求書 #16612 end
            this.GyoushaEntity.SEIKYUU_SOUFU_BUSHO = this.LoadKariGyoushaEntity.SEIKYUU_SOUFU_BUSHO;
            this.GyoushaEntity.SEIKYUU_SOUFU_FAX = this.LoadKariGyoushaEntity.SEIKYUU_SOUFU_FAX;
            this.GyoushaEntity.SEIKYUU_SOUFU_KEISHOU1 = this.LoadKariGyoushaEntity.SEIKYUU_SOUFU_KEISHOU1;
            this.GyoushaEntity.SEIKYUU_SOUFU_KEISHOU2 = this.LoadKariGyoushaEntity.SEIKYUU_SOUFU_KEISHOU2;
            this.GyoushaEntity.SEIKYUU_SOUFU_NAME1 = this.LoadKariGyoushaEntity.SEIKYUU_SOUFU_NAME1;
            this.GyoushaEntity.SEIKYUU_SOUFU_NAME2 = this.LoadKariGyoushaEntity.SEIKYUU_SOUFU_NAME2;
            this.GyoushaEntity.SEIKYUU_SOUFU_POST = this.LoadKariGyoushaEntity.SEIKYUU_SOUFU_POST;
            this.GyoushaEntity.SEIKYUU_SOUFU_TANTOU = this.LoadKariGyoushaEntity.SEIKYUU_SOUFU_TANTOU;
            this.GyoushaEntity.SEIKYUU_SOUFU_TEL = this.LoadKariGyoushaEntity.SEIKYUU_SOUFU_TEL;
            this.GyoushaEntity.SEIKYUU_TANTOU = this.LoadKariGyoushaEntity.SEIKYUU_TANTOU;
            this.GyoushaEntity.SHIHARAI_KYOTEN_CD = this.LoadKariGyoushaEntity.SHIHARAI_KYOTEN_CD;
            this.GyoushaEntity.SHIHARAI_KYOTEN_PRINT_KBN = this.LoadKariGyoushaEntity.SHIHARAI_KYOTEN_PRINT_KBN;
            this.GyoushaEntity.SHIHARAI_SOUFU_ADDRESS1 = this.LoadKariGyoushaEntity.SHIHARAI_SOUFU_ADDRESS1;
            this.GyoushaEntity.SHIHARAI_SOUFU_ADDRESS2 = this.LoadKariGyoushaEntity.SHIHARAI_SOUFU_ADDRESS2;
            this.GyoushaEntity.SHIHARAI_SOUFU_BUSHO = this.LoadKariGyoushaEntity.SHIHARAI_SOUFU_BUSHO;
            this.GyoushaEntity.SHIHARAI_SOUFU_FAX = this.LoadKariGyoushaEntity.SHIHARAI_SOUFU_FAX;
            this.GyoushaEntity.SHIHARAI_SOUFU_KEISHOU1 = this.LoadKariGyoushaEntity.SHIHARAI_SOUFU_KEISHOU1;
            this.GyoushaEntity.SHIHARAI_SOUFU_KEISHOU2 = this.LoadKariGyoushaEntity.SHIHARAI_SOUFU_KEISHOU2;
            this.GyoushaEntity.SHIHARAI_SOUFU_NAME1 = this.LoadKariGyoushaEntity.SHIHARAI_SOUFU_NAME1;
            this.GyoushaEntity.SHIHARAI_SOUFU_NAME2 = this.LoadKariGyoushaEntity.SHIHARAI_SOUFU_NAME2;
            this.GyoushaEntity.SHIHARAI_SOUFU_POST = this.LoadKariGyoushaEntity.SHIHARAI_SOUFU_POST;
            this.GyoushaEntity.SHIHARAI_SOUFU_TANTOU = this.LoadKariGyoushaEntity.SHIHARAI_SOUFU_TANTOU;
            this.GyoushaEntity.SHIHARAI_SOUFU_TEL = this.LoadKariGyoushaEntity.SHIHARAI_SOUFU_TEL;
            this.GyoushaEntity.SHOKUCHI_KBN = this.LoadKariGyoushaEntity.SHOKUCHI_KBN;
            this.GyoushaEntity.SHUUKEI_ITEM_CD = this.LoadKariGyoushaEntity.SHUUKEI_ITEM_CD;
            this.GyoushaEntity.TANTOUSHA = this.LoadKariGyoushaEntity.TANTOUSHA;
            this.GyoushaEntity.TEKIYOU_BEGIN = this.LoadKariGyoushaEntity.TEKIYOU_BEGIN;
            this.GyoushaEntity.TEKIYOU_END = this.LoadKariGyoushaEntity.TEKIYOU_END;
            this.GyoushaEntity.TORIHIKI_JOUKYOU = this.LoadKariGyoushaEntity.TORIHIKI_JOUKYOU;
            this.GyoushaEntity.TORIHIKISAKI_CD = this.LoadKariGyoushaEntity.TORIHIKISAKI_CD;
            this.GyoushaEntity.TORIHIKISAKI_UMU_KBN = this.LoadKariGyoushaEntity.TORIHIKISAKI_UMU_KBN;
            this.GyoushaEntity.UPDATE_DATE = this.LoadKariGyoushaEntity.UPDATE_DATE;
            this.GyoushaEntity.UPDATE_PC = this.LoadKariGyoushaEntity.UPDATE_PC;
            this.GyoushaEntity.UPDATE_USER = this.LoadKariGyoushaEntity.UPDATE_USER;
            this.GyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN = this.LoadKariGyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN;
            this.GyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN = this.LoadKariGyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN;
            this.GyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN = this.LoadKariGyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN;

            LogUtility.DebugMethodEnd();
        }

        internal bool UpdateHikiaiGyousha(M_HIKIAI_GYOUSHA entity)
        {
            try
            {
                var dao = DaoInitUtility.GetComponent<IM_HIKIAI_GYOUSHADao>();
                dao.Update(entity);
                return false;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("UpdateHikiaiGyousha", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("UpdateHikiaiGyousha", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdateHikiaiGyousha", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// 20141217 Houkakou 「業者入力」の日付チェックを追加する　start

        #region 日付チェック

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                this.form.TEKIYOU_BEGIN.BackColor = Constans.NOMAL_COLOR;
                this.form.TEKIYOU_END.BackColor = Constans.NOMAL_COLOR;

                DateTime date_from = new DateTime(1, 1, 1);
                DateTime date_to = new DateTime(9999, 12, 31);
                if (!string.IsNullOrWhiteSpace(this.form.TEKIYOU_BEGIN.Text))
                {
                    DateTime.TryParse(this.form.TEKIYOU_BEGIN.Text, out date_from);
                }
                if (!string.IsNullOrWhiteSpace(this.form.TEKIYOU_END.Text))
                {
                    DateTime.TryParse(this.form.TEKIYOU_END.Text, out date_to);
                }

                DateTime torihiki_from = new DateTime(1, 1, 1);
                DateTime torihiki_to = new DateTime(9999, 12, 31);
                if (!string.IsNullOrWhiteSpace(this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Text))
                {
                    DateTime.TryParse(this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Text, out torihiki_from);
                }
                if (!string.IsNullOrWhiteSpace(this.form.TORIHIKISAKI_TEKIYOU_END.Text))
                {
                    DateTime.TryParse(this.form.TORIHIKISAKI_TEKIYOU_END.Text, out torihiki_to);
                }

                // 業者適用開始日 < 取引先適用開始日 場合
                if (date_from.CompareTo(torihiki_from) < 0)
                {
                    msgLogic.MessageBoxShow("E255", "適用開始日", "業者", "取引先", "前", "以降");
                    this.form.TEKIYOU_BEGIN.Focus();
                    return true;
                }

                // 取引先適用終了日 < 業者適用終了日 場合
                if (torihiki_to.CompareTo(date_to) < 0)
                {
                    msgLogic.MessageBoxShow("E255", "適用終了日", "業者", "取引先", "後", "以前");
                    this.form.TEKIYOU_END.Focus();
                    return true;
                }

                M_GYOUSHA data = new M_GYOUSHA();
                data.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                // 現場適用開始日<業者適用開始日
                DataTable begin = this.daoGyousha.GetDataBySqlFile(GET_TEKIYOUBEGIN_SQL, data);
                DateTime date;
                if (begin != null && begin.Rows.Count > 0)
                {
                    DateTime.TryParse(Convert.ToString(begin.Rows[0][0]), out date);
                    if (date.CompareTo(date_from) < 0)
                    {
                        msgLogic.MessageBoxShow("E255", "適用開始日", "業者", "現場", "後", "以前");
                        this.form.TEKIYOU_BEGIN.Focus();
                        return true;
                    }
                }

                // 現場適用終了日>業者適用終了日
                DataTable end = this.daoGyousha.GetDataBySqlFile(GET_TEKIYOUEND_SQL, data);
                if (end != null && end.Rows.Count > 0)
                {
                    DateTime.TryParse(Convert.ToString(end.Rows[0][0]), out date);
                    if (date.CompareTo(date_to) > 0)
                    {
                        msgLogic.MessageBoxShow("E255", "適用終了日", "業者", "現場", "前", "以降");
                        this.form.TEKIYOU_END.Focus();
                        return true;
                    }
                }

                // 業者適用開始日 > 業者適用終了日 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.TEKIYOU_BEGIN.BackColor = Constans.ERROR_COLOR;
                    this.form.TEKIYOU_END.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "適用開始", "適用終了" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.TEKIYOU_BEGIN.Focus();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DateCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        /// 20141217 Houkakou 「業者入力」の日付チェックを追加する　end

        // 20141208 ブン 運搬報告書提出先を追加する start
        /// <summary>
        /// 運搬報告書提出先を取得します
        /// </summary>
        /// <returns>取得した件数</returns>
        public bool SearchsetUpanHoukokushoTeishutsu()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = string.Empty;
                if (!string.IsNullOrEmpty(this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text))
                {
                    M_CHIIKI search = new M_CHIIKI();
                    search.CHIIKI_CD = this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text;
                    M_CHIIKI[] chiikiDto = this.daoChiiki.GetAllValidData(search);
                    if (chiikiDto != null && chiikiDto.Length > 0)
                    {
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = chiikiDto[0].CHIIKI_CD.ToString();
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = chiikiDto[0].CHIIKI_NAME_RYAKU.ToString();
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "地域");
                        this.isError = true;
                        ret = false;
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Focus();
                    }
                }
                else
                {
                    this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = string.Empty;
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                this.isError = true;
                LogUtility.Error("SearchsetUpanHoukokushoTeishutsu", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 運搬報告書提出先データ取得処理(地域)
        /// </summary>
        /// <returns></returns>
        public int SearchUpnHoukokushoTeishutsuChiiki()
        {
            LogUtility.DebugMethodStart();

            this.upnHoukokushoTeishutsuChiikiEntity = null;

            this.upnHoukokushoTeishutsuChiikiEntity = daoChiiki.GetDataByCd(this.GyoushaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD);

            int count = this.upnHoukokushoTeishutsuChiikiEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        // 20141208 ブン 運搬報告書提出先を追加する end

        // VUNGUYEN 20150525 #1294 START
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TEKIYOU_END_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.form.TEKIYOU_END.Text = this.form.TEKIYOU_BEGIN.Text;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 登録データをチェックする。
        /// </summary>
        public bool CheckRegistData()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (string.IsNullOrEmpty(this.form.TEKIYOU_BEGIN.Text))
                {
                    var result = msgLogic.MessageBoxShow("E001", "適用開始日");
                    this.form.TEKIYOU_BEGIN.BackColor = Constans.ERROR_COLOR;
                    return true;
                }
                if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text) && WINDOW_TYPE.DELETE_WINDOW_FLAG != this.form.WindowType)
                {
                    var TorihikisakiSeikyuu = daoSeikyuu.GetDataByCd(this.form.TORIHIKISAKI_CD.Text);
                    var TorihikisakiShiharai = daoShiharai.GetDataByCd(this.form.TORIHIKISAKI_CD.Text);

                    //check 請求書書式１ SHOSHIKI_KBN
                    string sMsg = string.Empty;
                    if (TorihikisakiSeikyuu != null)
                    {
                        if (!TorihikisakiSeikyuu.SHOSHIKI_KBN.IsNull)
                        {
                            if (TorihikisakiSeikyuu.SHOSHIKI_KBN.Value == 2)
                            {
                                if (string.IsNullOrEmpty(this.form.SEIKYUU_SOUFU_NAME1.Text))
                                {
                                    sMsg += Const.GyoushaHoshuConstans.MSG_CONF_B;
                                    sMsg += "\n";
                                }
                                //check SEIKYUU_SOUFU_ADDRESS1
                                if (string.IsNullOrEmpty(this.form.SEIKYUU_SOUFU_ADDRESS1.Text))
                                {
                                    sMsg += Const.GyoushaHoshuConstans.MSG_CONF_C;
                                    sMsg += "\n";
                                }
                            }
                        }
                    }

                    //check 支払明細書書式１ SHIHARAI_SHOSHIKI_KBN
                    if (TorihikisakiShiharai != null)
                    {
                        if (!TorihikisakiShiharai.SHOSHIKI_KBN.IsNull)
                        {
                            if (TorihikisakiShiharai.SHOSHIKI_KBN.Value == 2)
                            {
                                if (string.IsNullOrEmpty(this.form.SHIHARAI_SOUFU_NAME1.Text))
                                {
                                    sMsg += Const.GyoushaHoshuConstans.MSG_CONF_D;
                                    sMsg += "\n";
                                }
                                //check SHIHARAI_SOUFU_ADDRESS1
                                if (string.IsNullOrEmpty(this.form.SHIHARAI_SOUFU_ADDRESS1.Text))
                                {
                                    sMsg += Const.GyoushaHoshuConstans.MSG_CONF_E;
                                    sMsg += "\n";
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(sMsg))
                    {
                        var dlg = msgLogic.MessageBoxShowConfirm(sMsg);
                        if (dlg == DialogResult.No)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckRegistData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        // VUNGUYEN 20150525 #1294 END

        /// <summary>
        /// 返送先情報変更処理
        /// </summary>
        public bool ChangeManiHensousakiJyouhouKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text.Equals("1"))
                {
                    this.form.MANI_HENSOUSAKI_NAME1.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_NAME2.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_POST.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_ADDRESS1.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_ADDRESS2.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_BUSHO.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_TANTOU.Text = string.Empty;

                    this.form.MANI_HENSOUSAKI_NAME1.Enabled = false;
                    this.form.MANI_HENSOUSAKI_NAME2.Enabled = false;
                    this.form.MANI_HENSOUSAKI_KEISHOU1.Enabled = false;
                    this.form.MANI_HENSOUSAKI_KEISHOU2.Enabled = false;
                    this.form.MANI_HENSOUSAKI_POST.Enabled = false;
                    this.form.bt_hensousaki_torihikisaki_copy.Enabled = false;
                    this.form.bt_hensousaki_address.Enabled = false;
                    this.form.bt_hensousaki_post.Enabled = false;
                    this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = false;
                    this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = false;
                    this.form.MANI_HENSOUSAKI_BUSHO.Enabled = false;
                    this.form.MANI_HENSOUSAKI_TANTOU.Enabled = false;
                }
                else
                {
                    this.form.MANI_HENSOUSAKI_NAME1.Enabled = true;
                    this.form.MANI_HENSOUSAKI_NAME2.Enabled = true;
                    this.form.MANI_HENSOUSAKI_KEISHOU1.Enabled = true;
                    this.form.MANI_HENSOUSAKI_KEISHOU2.Enabled = true;
                    this.form.MANI_HENSOUSAKI_POST.Enabled = true;
                    this.form.bt_hensousaki_torihikisaki_copy.Enabled = true;
                    this.form.bt_hensousaki_address.Enabled = true;
                    this.form.bt_hensousaki_post.Enabled = true;
                    this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = true;
                    this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = true;
                    this.form.MANI_HENSOUSAKI_BUSHO.Enabled = true;
                    this.form.MANI_HENSOUSAKI_TANTOU.Enabled = true;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeManiHensousakiJyouhouKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 運搬報告書提出先変更処理
        /// </summary>
        public bool TeishutsuChiikiCdValidating(System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();
                if (string.IsNullOrEmpty(this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text))
                {
                    this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = string.Empty;
                    return false;
                }

                M_CHIIKI keyEntity = new M_CHIIKI();
                keyEntity.CHIIKI_CD = this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text;
                var data = this.daoChiiki.GetAllValidData(keyEntity).FirstOrDefault();
                if (data != null)
                {
                    this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = data.CHIIKI_NAME_RYAKU;
                }
                else
                {
                    this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = string.Empty;
                    this.form.errmessage.MessageBoxShow("E020", "地域");
                    e.Cancel = true;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeishutsuChiikiCdValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        // 20160429 koukoukon v2.1_電子請求書 #16612 start
        /// <summary>
        /// 発行先チェック処理
        /// </summary>
        public bool HakkousakuCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();


                // 取引先CDの値がブランクの場合
                if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    this.form.HAKKOUSAKI_CD.Enabled = false;
                    this.form.HAKKOUSAKI_CD.Text = string.Empty;
                }
                else
                {
                    M_TORIHIKISAKI_SEIKYUU queryParam = new M_TORIHIKISAKI_SEIKYUU();
                    queryParam.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                    M_TORIHIKISAKI_SEIKYUU seikyuuEntity = this.daoSeikyuu.GetDataByCd(queryParam.TORIHIKISAKI_CD);
                    // 取引区「1.現金」の取引先を入力した時、発行先コードは非活性となるようにする。
                    if (seikyuuEntity != null && seikyuuEntity.OUTPUT_KBN.ToString() == "2" && seikyuuEntity.TORIHIKI_KBN_CD.ToString() == "2")
                    {
                        // 取引先マスタの出力区分が「２．電子CSV」の場合
                        this.form.HAKKOUSAKI_CD.Enabled = true;
                    }
                    else
                    {
                        // 取引先マスタの出力区分が「２．電子CSV」以外の場合
                        this.form.HAKKOUSAKI_CD.Enabled = false;
                        this.form.HAKKOUSAKI_CD.Text = string.Empty;
                    }
                }
   
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HakkousakuCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 画面起動時に電子請求書で追加するコントロール・項目の表示/非表示を切り替える
        /// </summary>
        private bool setDensiSeikyushoVisible()
        {
            // densiVisible true場合表示false場合非表示
            bool densiVisible = r_framework.Configuration.AppConfig.AppOptions.IsElectronicInvoice();

            if (!densiVisible)
            {

                this.form.labelDensiSeikyuuSho.Visible= densiVisible;
                this.form.labelHakkosaki.Visible = densiVisible;
                this.form.HAKKOUSAKI_CD.Visible = densiVisible;
           
            }
            return densiVisible;
        }
        // 20160429 koukoukon v2.1_電子請求書 #16612 end

        /// <summary>
        /// 請求情報の送付先情報を取引先の情報でコピー
        /// </summary>
        public bool TorihikisakiInfoCopyFromSeikyuuInfo()
        {
            try
            {
                M_TORIHIKISAKI entitysTORIHIKISAKI = daoTorisaki.GetDataByCd(this.TorihikisakiCd);

                if (entitysTORIHIKISAKI == null)
                {
                    return true;
                }

                this.form.SEIKYUU_SOUFU_NAME1.Text = entitysTORIHIKISAKI.TORIHIKISAKI_NAME1;
                this.form.SEIKYUU_SOUFU_NAME2.Text = entitysTORIHIKISAKI.TORIHIKISAKI_NAME2;
                this.form.SEIKYUU_SOUFU_KEISHOU1.Text = entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU1;
                this.form.SEIKYUU_SOUFU_KEISHOU2.Text = entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU2;
                this.form.SEIKYUU_SOUFU_POST.Text = entitysTORIHIKISAKI.TORIHIKISAKI_POST;

                string todoufukenName = "";
                if (!entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD.IsNull)
                {
                    M_TODOUFUKEN entitysM_TODOUFUKEN = this.daoTodoufuken.GetDataByCd(entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD.ToString());
                    if (entitysM_TODOUFUKEN != null)
                    {
                        todoufukenName = entitysM_TODOUFUKEN.TODOUFUKEN_NAME;
                    }
                }

                this.form.SEIKYUU_SOUFU_ADDRESS1.Text = todoufukenName + entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS1;
                this.form.SEIKYUU_SOUFU_ADDRESS2.Text = entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS2;
                this.form.SEIKYUU_SOUFU_BUSHO.Text = entitysTORIHIKISAKI.BUSHO;
                this.form.SEIKYUU_SOUFU_TANTOU.Text = entitysTORIHIKISAKI.TANTOUSHA;
                this.form.SEIKYUU_SOUFU_TEL.Text = entitysTORIHIKISAKI.TORIHIKISAKI_TEL;
                this.form.SEIKYUU_SOUFU_FAX.Text = entitysTORIHIKISAKI.TORIHIKISAKI_FAX;

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TorihikisakiInfoCopyFromSeikyuuInfo", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 支払情報の送付先情報を取引先の情報でコピー
        /// </summary>
        public bool TorihikisakiInfoCopyFromShiharaiInfo()
        {
            try
            {
                M_TORIHIKISAKI entitysTORIHIKISAKI = daoTorisaki.GetDataByCd(this.TorihikisakiCd);

                if (entitysTORIHIKISAKI == null)
                {
                    return true;
                }

                this.form.SHIHARAI_SOUFU_NAME1.Text = entitysTORIHIKISAKI.TORIHIKISAKI_NAME1;
                this.form.SHIHARAI_SOUFU_NAME2.Text = entitysTORIHIKISAKI.TORIHIKISAKI_NAME2;
                this.form.SHIHARAI_SOUFU_KEISHOU1.Text = entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU1;
                this.form.SHIHARAI_SOUFU_KEISHOU2.Text = entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU2;
                this.form.SHIHARAI_SOUFU_POST.Text = entitysTORIHIKISAKI.TORIHIKISAKI_POST;

                string todoufukenName = "";
                if (!entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD.IsNull)
                {
                    M_TODOUFUKEN entitysM_TODOUFUKEN = this.daoTodoufuken.GetDataByCd(entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD.ToString());
                    if (entitysM_TODOUFUKEN != null)
                    {
                        todoufukenName = entitysM_TODOUFUKEN.TODOUFUKEN_NAME;
                    }
                }

                this.form.SHIHARAI_SOUFU_ADDRESS1.Text = todoufukenName + entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS1;
                this.form.SHIHARAI_SOUFU_ADDRESS2.Text = entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS2;
                this.form.SHIHARAI_SOUFU_BUSHO.Text = entitysTORIHIKISAKI.BUSHO;
                this.form.SHIHARAI_SOUFU_TANTOU.Text = entitysTORIHIKISAKI.TANTOUSHA;
                this.form.SHIHARAI_SOUFU_TEL.Text = entitysTORIHIKISAKI.TORIHIKISAKI_TEL;
                this.form.SHIHARAI_SOUFU_FAX.Text = entitysTORIHIKISAKI.TORIHIKISAKI_FAX;

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TorihikisakiInfoCopyFromShiharaiInfo", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// OpenGenbaForm
        /// </summary>
        /// <param name="GyoushaCd"></param>
        /// <param name="GenbaCd"></param>
        internal  void OpenGenbaForm(WINDOW_TYPE wType,string GyoushaCd, string GenbaCd)
        {
            LogUtility.DebugMethodStart(wType, GyoushaCd, GenbaCd);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (!r_framework.Authority.Manager.CheckAuthority("M217", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                if (!r_framework.Authority.Manager.CheckAuthority("M217", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    msgLogic.MessageBoxShow("E158", "修正");
                    return;
                }

                wType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
            }

            r_framework.FormManager.FormManager.OpenFormWithAuth("M217", wType, wType, GyoushaCd, GenbaCd);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// OpenTorihikisakiFormReference
        /// </summary>
        /// <param name="TorihikisakiCd"></param>
        internal void OpenTorihikisakiFormReference(string TorihikisakiCd)
        {
            LogUtility.DebugMethodStart(TorihikisakiCd);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            WINDOW_TYPE windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
            if (!r_framework.Authority.Manager.CheckAuthority("M213", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                msgLogic.MessageBoxShow("E158", "修正");
                return;
            }

            r_framework.FormManager.FormManager.OpenFormWithAuth("M213", windowType, windowType, TorihikisakiCd);
            LogUtility.DebugMethodEnd();
        }

        #region mapbox連携

        /// <summary>
        /// 地図表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OpenMap(object sender, EventArgs e)
        {
            try
            {
                // 緯度経度の入力チェック
                if (this.CheckLocation())
                {
                    return;
                }

                if (!string.IsNullOrEmpty(this.form.GyoushaLatitude.Text) && !string.IsNullOrEmpty(this.form.GyoushaLongitude.Text))
                {
                    // 緯度経度入力済み
                    if (this.form.errmessage.MessageBoxShowConfirm("地図を表示します。よろしいですか？") == DialogResult.No)
                    {
                        return;
                    }
                }
                else if (!string.IsNullOrEmpty(this.form.GYOUSHA_TODOUFUKEN_CD.Text) && !string.IsNullOrEmpty(this.form.GYOUSHA_ADDRESS1.Text))
                {
                    // 緯度経度未入力、都道府県、住所有り
                    if (this.form.errmessage.MessageBoxShowConfirm("入力されている住所情報にて地図を表示します。よろしいですか？\r\n注）住所での地図表示の場合、正しい位置にならない場合があります。") == DialogResult.No)
                    {
                        return;
                    }

                    // 住所から緯度経度を取得するAPI
                    // 取得した緯度経度は画面にセットせず、APIに投げるだけ
                    MapboxAPILogic apiLogic = new MapboxAPILogic();
                    GeoCodingAPI result = null;

                    // 都道府県+住所1+住所2で検索
                    string address = this.form.TODOUFUKEN_NAME.Text + this.form.GYOUSHA_ADDRESS1.Text + this.form.GYOUSHA_ADDRESS2.Text;
                    if (!apiLogic.HttpGET<GeoCodingAPI>(address, out result))
                    {
                        // APIでエラー発生
                        return;
                    }
                    foreach (Feature feature in result.features)
                    {
                        // APIで取得した値を利用する
                        this.form.GyoushaLatitude.Text = feature.geometry.coordinates[1];
                        this.form.GyoushaLongitude.Text = feature.geometry.coordinates[0];
                    }
                }
                else
                {
                    // 緯度経度、都道府県住所全て未入力
                    this.form.errmessage.MessageBoxShowInformation("緯度及び経度、もしくは、住所を入力してください。");
                    return;
                }

                MapboxGLJSLogic gljsLogic = new MapboxGLJSLogic();

                // 地図に渡すDTO作成
                List<mapDtoList> dtos = new List<mapDtoList>();
                dtos = this.createMapboxDto();
                if (dtos == null)
                {
                    return;
                }

                // 地図表示
                gljsLogic.mapbox_HTML_Open(dtos, WINDOW_ID.M_GYOUSHA);
            }
            catch (Exception ex)
            {
                LogUtility.Error("OpenMap", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
            }
        }


        /// <summary>
        /// 地図表示ロジックに渡すDtoを作成
        /// </summary>
        /// <returns></returns>
        private List<mapDtoList> createMapboxDto()
        {
            try
            {
                List<mapDtoList> dtoLists = new List<mapDtoList>();
                mapDtoList dtoList = new mapDtoList();
                dtoList.layerId = 1;

                List<mapDto> dtos = new List<mapDto>();
                mapDto dto = new mapDto();
                dto.id = 1;
                dto.layerNo = 1;
                dto.dataShurui = "1";
                dto.torihikisakiCd = string.Empty;
                dto.torihikisakiName = string.Empty;
                dto.gyoushaCd = this.form.GYOUSHA_CD.Text;
                dto.gyoushaName = this.form.GYOUSHA_NAME_RYAKU.Text;
                dto.genbaCd = string.Empty;
                dto.genbaName = string.Empty;
                dto.address = this.form.TODOUFUKEN_NAME.Text + this.form.GYOUSHA_ADDRESS1.Text + this.form.GYOUSHA_ADDRESS2.Text;
                dto.latitude = this.form.GyoushaLatitude.Text;
                dto.longitude = this.form.GyoushaLongitude.Text;
                dtos.Add(dto);
                dtoList.dtos = dtos;
                dtoLists.Add(dtoList);

                return dtoLists;
            }
            catch (Exception ex)
            {
                LogUtility.Error("createMapboxDto", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private SqlDateTime sysDate()
        {
            DateTime now = DateTime.Now;
            GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return SqlDateTime.Parse(now.ToString());
        }

        /// <summary>
        /// 緯度経度の入力チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckLocation()
        {
            bool ret = false;

            // 緯度のチェック
            if (!string.IsNullOrEmpty(this.form.GyoushaLatitude.Text))
            {
                try
                {
                    Convert.ToDouble(this.form.GyoushaLatitude.Text);
                }
                catch (Exception ex)
                {
                    this.form.errmessage.MessageBoxShowWarn("緯度の入力が正しくありません");
                    this.form.GyoushaLatitude.Focus();
                    return true;
                }
            }

            // 経度のチェック
            if (!string.IsNullOrEmpty(this.form.GyoushaLongitude.Text))
            {
                try
                {
                    Convert.ToDouble(this.form.GyoushaLongitude.Text);
                }
                catch (Exception ex)
                {
                    this.form.errmessage.MessageBoxShowWarn("経度の入力が正しくありません");
                    this.form.GyoushaLongitude.Focus();
                    return true;
                }
            }

            // 緯度だけ入力されていない
            if (string.IsNullOrEmpty(this.form.GyoushaLatitude.Text) &&
               !string.IsNullOrEmpty(this.form.GyoushaLongitude.Text))
            {
                this.form.errmessage.MessageBoxShowWarn("緯度を入力してください。");
                this.form.GyoushaLatitude.Focus();
                return true;
            }

            // 経度だけ入力されていない
            if (string.IsNullOrEmpty(this.form.GyoushaLongitude.Text) &&
               !string.IsNullOrEmpty(this.form.GyoushaLatitude.Text))
            {
                this.form.errmessage.MessageBoxShowWarn("経度を入力してください。");
                this.form.GyoushaLongitude.Focus();
                return true;
            }

            return ret;
        }

        #endregion

        #region LANDUONG - 20211130
        private void SetDensiSeikyushoAndRakurakuVisible()
        {
            if (!denshiSeikyusho && !denshiSeikyuRaku)
            {
                this.form.labelDensiSeikyuuSho.Visible = false;
                this.form.labelHakkosaki.Visible = false;
                this.form.HAKKOUSAKI_CD.Visible = false;
                this.form.label43.Visible = false;
                this.form.RAKURAKU_CUSTOMER_CD.Visible = false;
                this.form.RAKURAKU_SAIBAN_BUTTON.Visible = false;
            }
            else if (denshiSeikyusho && denshiSeikyuRaku)
            {
                this.form.labelDensiSeikyuuSho.Visible = true;
                this.form.labelHakkosaki.Visible = true;
                this.form.HAKKOUSAKI_CD.Visible = true;
                this.form.label43.Visible = true;
                this.form.RAKURAKU_CUSTOMER_CD.Visible = true;
                this.form.RAKURAKU_SAIBAN_BUTTON.Visible = true;
            }
            else if (denshiSeikyusho && !denshiSeikyuRaku)
            {
                this.form.labelDensiSeikyuuSho.Visible = true;
                this.form.labelHakkosaki.Visible = true;
                this.form.HAKKOUSAKI_CD.Visible = true;
                this.form.label43.Visible = false;
                this.form.RAKURAKU_CUSTOMER_CD.Visible = false;
                this.form.RAKURAKU_SAIBAN_BUTTON.Visible = false;
            }
            else if (!denshiSeikyusho && denshiSeikyuRaku)
            {
                this.form.labelDensiSeikyuuSho.Visible = true;
                this.form.labelHakkosaki.Visible = false;
                this.form.HAKKOUSAKI_CD.Visible = false;
                this.form.label43.Visible = true;
                this.form.RAKURAKU_CUSTOMER_CD.Visible = true;
                this.form.RAKURAKU_SAIBAN_BUTTON.Visible = true;

                this.form.label43.Location = new System.Drawing.Point(this.form.label43.Location.X, this.form.labelHakkosaki.Location.Y);
                this.form.RAKURAKU_CUSTOMER_CD.Location = new System.Drawing.Point(this.form.RAKURAKU_CUSTOMER_CD.Location.X, this.form.labelHakkosaki.Location.Y);
                this.form.RAKURAKU_SAIBAN_BUTTON.Location = new System.Drawing.Point(this.form.RAKURAKU_SAIBAN_BUTTON.Location.X, this.form.labelHakkosaki.Location.Y - 1);
            }

            return;
        }

        public bool HakkousakuAndRakurakuCDCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    this.form.HAKKOUSAKI_CD.Text = string.Empty;
                    this.form.HAKKOUSAKI_CD.Enabled = false;
                    this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;
                    this.form.RAKURAKU_CUSTOMER_CD.Enabled = false;
                    this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = false;
                }
                else
                {
                    M_TORIHIKISAKI_SEIKYUU queryParam = new M_TORIHIKISAKI_SEIKYUU();
                    queryParam.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                    M_TORIHIKISAKI_SEIKYUU seikyuuEntity = this.daoSeikyuu.GetDataByCd(queryParam.TORIHIKISAKI_CD);
                    if (seikyuuEntity != null)
                    {
                        if (seikyuuEntity.OUTPUT_KBN == 3)
                        {
                            this.form.HAKKOUSAKI_CD.Text = string.Empty;
                            this.form.HAKKOUSAKI_CD.Enabled = false;
                            this.form.RAKURAKU_CUSTOMER_CD.Enabled = true;
                            this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = true;

                            // 楽楽顧客コードの採番ボタン
                            if (this.sysinfoEntity != null && !this.sysinfoEntity.RAKURAKU_CODE_NUMBERING_KBN.IsNull
                                && this.sysinfoEntity.RAKURAKU_CODE_NUMBERING_KBN == 1)
                            {
                                this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = false;
                            }
                            else if (this.sysinfoEntity != null && !this.sysinfoEntity.RAKURAKU_CODE_NUMBERING_KBN.IsNull
                                && this.sysinfoEntity.RAKURAKU_CODE_NUMBERING_KBN == 2)
                            {
                                this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = true;
                            }

                        }
                        else if (seikyuuEntity.OUTPUT_KBN == 2)
                        {
                            this.form.HAKKOUSAKI_CD.Enabled = true;
                            this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;
                            this.form.RAKURAKU_CUSTOMER_CD.Enabled = false;
                            this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = false;
                        }
                        else
                        {
                            this.form.HAKKOUSAKI_CD.Text = string.Empty;
                            this.form.HAKKOUSAKI_CD.Enabled = false;
                            this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;
                            this.form.RAKURAKU_CUSTOMER_CD.Enabled = false;
                            this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = false;
                        }
                    }
                    else
                    {
                        this.form.HAKKOUSAKI_CD.Text = string.Empty;
                        this.form.HAKKOUSAKI_CD.Enabled = false;
                        this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;
                        this.form.RAKURAKU_CUSTOMER_CD.Enabled = false;
                        this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = false;
                    }
                }

                this.form.HAKKOUSAKI_CD.BackColor = Constans.NOMAL_COLOR;
                this.form.RAKURAKU_CUSTOMER_CD.BackColor = Constans.NOMAL_COLOR;

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HakkousakuCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        internal void SaibanRakurakuCode()
        {
            LogUtility.DebugMethodStart();
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            try
            {
                var code = MasterCommonLogic.IsOverRakurakuCodeLimit();
                if (code < 0)
                {
                    msgLogic.MessageBoxShow("E041");
                    this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;
                }

                this.form.RAKURAKU_CUSTOMER_CD.Text = code.ToString();

                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaibanRakurakuCode", ex);
                this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;
            }
        }

        internal string GetBeforeRakurakuCode()
        {
            string ret = string.Empty;

            if (this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                if (this.GyoushaEntity != null && !string.IsNullOrEmpty(this.GyoushaEntity.RAKURAKU_CUSTOMER_CD))
                {
                    ret = this.GyoushaEntity.RAKURAKU_CUSTOMER_CD;
                }
            }

            return ret;
        }
        #endregion LANDUONG - 20211130

        #region 取引先有無チェック
        /// <summary>
        /// 取引先有無チェック
        /// </summary>
        /// <returns></returns>
        internal bool TorihikisakiUmuCheck()
        {
            //有りから無しに変更された場合
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text) && !string.IsNullOrEmpty(TorihikisakiUmKbn) && !string.IsNullOrEmpty(this.form.TORIHIKISAKI_UMU_KBN.Text))
            {
                if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType && int.Parse(this.form.TORIHIKISAKI_UMU_KBN.Text) == 2)
                {
                    if (int.Parse(TorihikisakiUmKbn) != int.Parse(this.form.TORIHIKISAKI_UMU_KBN.Text))
                    {
                        var messageLogic = new MessageBoxShowLogic();
                        var dialogResult = DialogResult.No;
                        dialogResult = messageLogic.MessageBoxShow("C133");
                        if (DialogResult.Yes == dialogResult)
                        {
                        }
                        else
                        {
                            this.form.TORIHIKISAKI_UMU_KBN.Focus();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #endregion

        //20250319
        public bool UriageGurupuCdValidating(System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();
                if (string.IsNullOrEmpty(this.form.URIAGE_GURUPU_CD.Text))
                {
                    this.form.URIAGE_GURUPU_NAME.Text = string.Empty;
                    return false;
                }

                M_GURUPU_NYURYOKU gurupuEntity = new M_GURUPU_NYURYOKU();
                gurupuEntity.GURUPU_CD = this.form.URIAGE_GURUPU_CD.Text;
                var data = this.daoGurupu.GetAllValidDataUriage(gurupuEntity).FirstOrDefault();
                if (data != null)
                {
                    this.form.URIAGE_GURUPU_NAME.Text = data.GURUPU_NAME;
                }
                else
                {
                    this.form.URIAGE_GURUPU_NAME.Text = string.Empty;
                    this.form.errmessage.MessageBoxShow("E020", "");
                    e.Cancel = true;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UriageGurupuCdValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        public bool ShiharaiGurupuCdValidating(System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();
                if (string.IsNullOrEmpty(this.form.SHIHARAI_GURUPU_CD.Text))
                {
                    this.form.SHIHARAI_GURUPU_NAME.Text = string.Empty;
                    return false;
                }

                M_GURUPU_NYURYOKU gurupuEntity = new M_GURUPU_NYURYOKU();
                gurupuEntity.GURUPU_CD = this.form.SHIHARAI_GURUPU_CD.Text;
                var data = this.daoGurupu.GetAllValidDataShiharai(gurupuEntity).FirstOrDefault();
                if (data != null)
                {
                    this.form.SHIHARAI_GURUPU_NAME.Text = data.GURUPU_NAME;
                }
                else
                {
                    this.form.SHIHARAI_GURUPU_NAME.Text = string.Empty;
                    this.form.errmessage.MessageBoxShow("E020", "");
                    e.Cancel = true;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShiharaiGurupuCdValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }
    }
}