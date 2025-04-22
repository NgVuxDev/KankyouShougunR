// $Id: TorihikisakiHoshuLogic.cs 52661 2015-06-18 01:47:06Z minhhoang@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
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
using TorihikisakiHoshu.APP;
using TorihikisakiHoshu.Const;
using TorihikisakiHoshu.Validator;
using Seasar.Framework.Exceptions;

using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox.GeoCodingAPI;
using r_framework.Configuration;
using System.Data.SqlTypes;
using r_framework.Dto;

namespace TorihikisakiHoshu.Logic
{
    /// <summary>
    /// 事業者入力のビジネスロジック
    /// </summary>
    public class 
        TorihikisakiHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "TorihikisakiHoshu.Setting.ButtonSetting.xml";

        private readonly string ButtonInfoXmlPath2 = "TorihikisakiHoshu.Setting.ButtonSetting2.xml";

        private readonly string GET_INPUTCD_DATA_NYUUKINSAKI_SQL = "TorihikisakiHoshu.Sql.GetInputCddataNyuukinsakiSql.sql";

        private readonly string GET_INPUTCD_DATA_SYUKKINSAKI_SQL = "TorihikisakiHoshu.Sql.GetInputCddataSyukkinsakiSql.sql";

        private readonly string GET_INPUTCD_DATA_TORIHIKISAKI_SQL = "TorihikisakiHoshu.Sql.GetInputCddataTorihikisakiSql.sql";

        private readonly string GET_ICHIRAN_GYOUSHA_DATA_SQL = "TorihikisakiHoshu.Sql.GetIchiranGyoushaDataSql.sql";

        private readonly string UPDATE_NYUUKINSAKI_SQL = "TorihikisakiHoshu.Sql.UpdateNyuukinsakiSql.sql";

        private readonly string UPDATE_SYUKKINSAKI_SQL = "TorihikisakiHoshu.Sql.UpdateSyukkinsakiSql.sql";

        private readonly string GET_TEKIYOUBEGIN_SQL = "TorihikisakiHoshu.Sql.GetTeikiyouBeginDateSql.sql";

        private readonly string GET_TEKIYOUEND_SQL = "TorihikisakiHoshu.Sql.GetTeikiyouEndDateSql.sql";

        private readonly string CHECK_DELETE_TORIHIKISAKI_SQL = "TorihikisakiHoshu.Sql.CheckDeleteTorihikisakiSql.sql";

        /// <summary>
        /// 事業者入力Form
        /// </summary>
        private TorihikisakiHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_TORIHIKISAKI_SHIHARAI entitysTORIHIKISAKI_SHIHARAI;

        private M_TORIHIKISAKI_SEIKYUU entitysTORIHIKISAKI_SEIKYUU;

        private M_KYOTEN entitysM_KYOTEN;

        private M_BUSHO entitysM_BUSHO;

        private M_SHAIN entitysM_SHAIN;

        private M_SHUUKEI_KOUMOKU entitysM_SHUUKEI_KOUMOKU;

        private M_GYOUSHU entitysM_GYOUSHU;

        private M_TODOUFUKEN entitysM_TODOUFUKEN;

        private M_NYUUKINSAKI entitysM_NYUUKINSAKI;

        private M_SYUKKINSAKI entitysM_SYUKKINSAKI;

        internal M_SYS_INFO entitysM_SYS_INFO;

        private M_BANK entitysM_BANK;

        private M_BANK_SHITEN entitysM_BANK_SHITEN;

        private M_NYUUSHUKKIN_KBN entitysM_NYUUSHUKKIN_KBN;

        private M_GYOUSHA[] entitysM_GYOUSHA;

        private M_GENBA[] entitysM_GENBA;

        private M_HIKIAI_GYOUSHA[] entitysM_HIKIAI_GYOUSHA;

        private M_HIKIAI_GENBA[] entitysM_HIKIAI_GENBA;

        /// <summary>
        /// 加入者番号
        /// </summary>

        private IM_TORIHIKISAKIDao daoTORIHIKISAKI;

        private IM_TORIHIKISAKI_SHIHARAIDao daoTORIHIKISAKI_SHIHARAI;

        private IM_TORIHIKISAKI_SEIKYUUDao daoTORIHIKISAKI_SEIKYUU;

        private IM_NYUUKINSAKIDao daoNYUKINSAKI;

        private IM_SYUKKINSAKIDao daoSYUKINSAKI;

        private IM_KYOTENDao daoIM_KYOTEN;

        private IM_BUSHODao daoIM_BUSHO;

        private IM_SHAINDao daoIM_SHAIN;

        private IM_SHUUKEI_KOUMOKUDao daoIM_SHUUKEI_KOUMOKU;

        private IM_GYOUSHADao daoIM_GYOUSHA;

        private IM_GYOUSHUDao daoIM_GYOUSHU;

        private IM_TODOUFUKENDao daoIM_TODOUFUKEN;

        private IM_NYUUKINSAKIDao daoIM_NYUUKINSAKI;

        private IM_SYUKKINSAKIDao daoIM_SYUKKINSAKI;

        private IM_SYS_INFODao daoIM_SYS_INFO;

        private IM_BANKDao daoIM_BANK;

        private IM_BANK_SHITENDao daoIM_BANK_SHITEN;

        private IM_NYUUSHUKKIN_KBNDao daoIM_NYUUSHUKKIN_KBN;

        private IS_ZIP_CODEDao daoIS_ZIP_CODE;

        private IM_GENBADao daoIM_GENBA;

        private IM_CORP_INFODao daoIM_CORP_INFO;

        private IM_HIKIAI_GYOUSHADao daoIM_HIKIAI_GYOUSHA;

        private IM_HIKIAI_GENBADao daoIM_HIKIAI_GENBA;

        private int rowCntGyousha;

        /// <summary>
        /// タブコントロール制御用
        /// </summary>
        private TabPageManager _tabPageManager = null;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_TORIHIKISAKI SearchString { get; set; }

        /// <summary>
        /// 取引先CD
        /// </summary>
        public string TorihikisakiCd { get; set; }

        /// <summary>
        /// 電子申請フラグ
        /// </summary>
        public bool denshiShinseiFlg { get; set; }

        /// <summary>
        /// 登録・更新・削除処理の成否
        /// </summary>
        public bool isRegist { get; set; }

        /// <summary>
        /// 検索結果(業者一覧)
        /// </summary>
        public DataTable SearchResultGyousha { get; set; }

        /// <summary>
        ///　エラー判定フラグ
        /// </summary>
        public bool isError { get; set; }

        internal M_TORIHIKISAKI entitysTORIHIKISAKI { get; private set; }

        /// <summary>
        /// 承認済申請一覧から呼び出されたかどうかのフラグを取得・設定します
        /// </summary>
        internal bool IsFromShouninzumiDenshiShinseiIchiran { get; set; }

        /// <summary>
        /// 本登録の元となる引合取引先エンティティを取得・設定します
        /// </summary>
        internal M_HIKIAI_TORIHIKISAKI LoadHikiaiTorihikisaki { get; private set; }

        internal M_HIKIAI_TORIHIKISAKI_SEIKYUU LoadHikiaiTorihikisakiSeikyuu { get; private set; }

        internal M_HIKIAI_TORIHIKISAKI_SHIHARAI LoadHikiaiTorihikisakiShiharai { get; private set; }

        /// <summary>
        /// 本登録の元となる仮登録取引先エンティティを取得・設定します
        /// </summary>
        internal M_KARI_TORIHIKISAKI LoadKariTorihikisaki { get; private set; }

        internal M_KARI_TORIHIKISAKI_SEIKYUU LoadKariTorihikisakiSeikyuu { get; private set; }

        internal M_KARI_TORIHIKISAKI_SHIHARAI LoadKariTorihikisakiShiharai { get; private set; }

        public String Zandaka_U;
        public String Zandaka_K;

        //Begin: LANDUONG - 20220209 - refs#160050
        internal bool denshiSeikyusho, denshiSeikyuRaku;
        internal ControlUtility controlUtil;
        //End: LANDUONG - 20220209 - refs#160050

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public TorihikisakiHoshuLogic(TorihikisakiHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.entitysTORIHIKISAKI = new M_TORIHIKISAKI();
            this.entitysTORIHIKISAKI_SHIHARAI = new M_TORIHIKISAKI_SHIHARAI();
            this.entitysTORIHIKISAKI_SEIKYUU = new M_TORIHIKISAKI_SEIKYUU();
            this.entitysM_KYOTEN = new M_KYOTEN();
            this.entitysM_BUSHO = new M_BUSHO();
            this.entitysM_SHAIN = new M_SHAIN();
            this.entitysM_SHUUKEI_KOUMOKU = new M_SHUUKEI_KOUMOKU();
            this.entitysM_GYOUSHU = new M_GYOUSHU();
            this.entitysM_TODOUFUKEN = new M_TODOUFUKEN();
            this.entitysM_NYUUKINSAKI = new M_NYUUKINSAKI();
            this.entitysM_SYUKKINSAKI = new M_SYUKKINSAKI();
            this.entitysM_SYS_INFO = new M_SYS_INFO();
            this.entitysM_BANK = new M_BANK();
            this.entitysM_BANK_SHITEN = new M_BANK_SHITEN();
            this.entitysM_NYUUSHUKKIN_KBN = new M_NYUUSHUKKIN_KBN();

            this.daoTORIHIKISAKI = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.daoTORIHIKISAKI_SHIHARAI = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
            this.daoTORIHIKISAKI_SEIKYUU = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
            this.daoNYUKINSAKI = DaoInitUtility.GetComponent<IM_NYUUKINSAKIDao>();
            this.daoSYUKINSAKI = DaoInitUtility.GetComponent<IM_SYUKKINSAKIDao>();
            this.daoIM_KYOTEN = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.daoIM_BUSHO = DaoInitUtility.GetComponent<IM_BUSHODao>();
            this.daoIM_SHAIN = DaoInitUtility.GetComponent<IM_SHAINDao>();
            this.daoIM_SHUUKEI_KOUMOKU = DaoInitUtility.GetComponent<IM_SHUUKEI_KOUMOKUDao>();
            this.daoIM_GYOUSHA = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.daoIM_GYOUSHU = DaoInitUtility.GetComponent<IM_GYOUSHUDao>();
            this.daoIM_TODOUFUKEN = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            this.daoIM_NYUUKINSAKI = DaoInitUtility.GetComponent<IM_NYUUKINSAKIDao>();
            this.daoIM_SYUKKINSAKI = DaoInitUtility.GetComponent<IM_SYUKKINSAKIDao>();
            this.daoIM_SYS_INFO = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.daoIM_BANK = DaoInitUtility.GetComponent<IM_BANKDao>();
            this.daoIM_BANK_SHITEN = DaoInitUtility.GetComponent<IM_BANK_SHITENDao>();
            this.daoIM_NYUUSHUKKIN_KBN = DaoInitUtility.GetComponent<IM_NYUUSHUKKIN_KBNDao>();
            this.daoIS_ZIP_CODE = DaoInitUtility.GetComponent<IS_ZIP_CODEDao>();

            this.daoIM_GENBA = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.daoIM_CORP_INFO = DaoInitUtility.GetComponent<IM_CORP_INFODao>();

            this.daoIM_HIKIAI_GYOUSHA = DaoInitUtility.GetComponent<IM_HIKIAI_GYOUSHADao>();
            this.daoIM_HIKIAI_GENBA = DaoInitUtility.GetComponent<IM_HIKIAI_GENBADao>();

            _tabPageManager = new TabPageManager(this.form.tabData);

            //Begin: LANDUONG - 20220209 - refs#160050
            // 電子請求オプ
            denshiSeikyusho = r_framework.Configuration.AppConfig.AppOptions.IsElectronicInvoice();
            //電子請求楽楽明細オプ
            denshiSeikyuRaku = r_framework.Configuration.AppConfig.AppOptions.IsRakurakuMeisai();
            // Control Utility
            this.controlUtil = new ControlUtility();
            //Begin: LANDUONG - 20220209 - refs#160050

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
                this.ButtonInit();

                // MAPBOX連携が無効な場合
                this._tabPageManager.ChangeTabPageVisible(8, (AppConfig.AppOptions.IsMAPBOX()) ? true : false);

                // イベントの初期化処理
                this.EventInit();

                //Begin: LANDUONG - 20220209 - refs#160050
                this.SetDensiSeikyushoAndRakurakuVisible();
                this.ChangeOutputDensiSeikyushoAndRakurakuKbn();
                //End: LANDUONG - 20220209 - refs#160050

                // 処理モード別画面初期化
                bool catchErr = this.ModeInit(windowType, parentForm);
                if (catchErr)
                {
                    return true;
                }
                this.allControl = this.form.allControl;

                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                // 画面起動時に電子請求書で追加するコントロール・項目の表示/非表示を切り替える
                //this.setDensiSeikyushoVisible();
                // 20160429 koukoukon v2.1_電子請求書 #16612 end

                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
                this.SetInxsSeikyushoVisible();
                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

                this.setOnlineBankVisible();

                this.setPaymentOnlineBankVisible();//162933

                // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
                this.SetInxsShiharaiMesaishoVisible();
                // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 画面項目初期化処理モード【新規】
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public void WindowInitNew(BusinessBaseForm parentForm)
        {
            if (string.IsNullOrEmpty(this.TorihikisakiCd))
            {
                //【新規】モードで初期化
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
        /// 画面項目初期化【修正】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitUpdate(BusinessBaseForm parentForm)
        {
            try
            {
                // 全コントロールを操作可能とする
                this.AllControlLock(false);

                // 検索結果を画面に設定
                this.SetWindowData();
                this.WindowInitNewMode(parentForm);
                this.SetDataForWindow();

                // 修正モード固有UI設定
                this.form.TORIHIKISAKI_CD.Enabled = false;   // 取引先CD
                this.form.SAIBAN_BUTTON.Enabled = false;    // 採番ボタン

                // functionボタン
                parentForm.bt_func2.Enabled = true;     // 新規
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func7.Enabled = true;     // 一覧
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = true;    // 取消
                parentForm.bt_func12.Enabled = true;    // 閉じる

                // 業者分類タブ初期化
                bool catchErr = this.ManiCheckOffCheck();
                if (catchErr)
                {
                    return true;
                }

                // 初期フォーカス
                this.form.NYUUKINSAKI_KBN.Focus();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInitUpdate", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("WindowInitUpdate", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
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
            this.WindowInitNewMode(parentForm);
            this.SetDataForWindow();

            // 全コントロールを操作不可とする
            this.AllControlLock(true);

            // functionボタン
            parentForm.bt_func2.Enabled = true;     // 新規
            parentForm.bt_func3.Enabled = true;     // 修正
            parentForm.bt_func7.Enabled = true;     // 一覧
            parentForm.bt_func9.Enabled = true;     // 登録
            parentForm.bt_func11.Enabled = false;   // 取消
            parentForm.bt_func12.Enabled = true;    // 閉じる
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
                this.WindowInitNewMode(parentForm);
                this.SetDataForWindow();

                // 全コントロールを操作不可とする
                this.AllControlLock(true);

                // functionボタン
                parentForm.bt_func2.Enabled = true;     // 新規
                parentForm.bt_func3.Enabled = true;    // 修正
                parentForm.bt_func7.Enabled = true;     // 一覧
                parentForm.bt_func9.Enabled = false;    // 登録
                parentForm.bt_func11.Enabled = false;   // 取消
                parentForm.bt_func12.Enabled = true;    // 閉じる

                // 業者分類タブ初期化
                //this.ManiCheckOffCheck();

                // 初期フォーカス
                this.form.NYUUKINSAKI_KBN.Focus();
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
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("WindowInitReference", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
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
                this.AllControlLock(false);

                //// フォーカスをセットする
                //this.form.NYUUKINSAKI_KBN.Focus();

                // 最新のSYS_INFOを取得
                M_SYS_INFO[] sysInfo = this.daoIM_SYS_INFO.GetAllData();
                if (sysInfo != null && sysInfo.Length > 0)
                {
                    this.entitysM_SYS_INFO = sysInfo[0];
                }
                else
                {
                    this.entitysM_SYS_INFO = null;
                }

                // ヘッダー項目
                DetailedHeaderForm header = (DetailedHeaderForm)parentForm.headerForm;
                header.CreateDate.Text = string.Empty;
                header.CreateUser.Text = string.Empty;
                header.LastUpdateDate.Text = string.Empty;
                header.LastUpdateUser.Text = string.Empty;

                // 共通項目
                this.form.NYUUKINSAKI_KBN.Text = "1";
                this.form.TORIHIKISAKI_KYOTEN_CD.Text = "99";
                this.entitysM_KYOTEN = this.daoIM_KYOTEN.GetDataByCd("99");
                this.form.KYOTEN_NAME_RYAKU.Text = string.Empty;
                if (this.entitysM_KYOTEN != null)
                {
                    this.form.KYOTEN_NAME_RYAKU.Text = this.entitysM_KYOTEN.KYOTEN_NAME_RYAKU;
                }
                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                this.form.TORIHIKISAKI_FURIGANA.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME1.Text = string.Empty;
                this.form.TORIHIKISAKI_KEISHOU1.Text = this.entitysM_SYS_INFO.TORIHIKISAKI_KEISHOU1;
                this.form.TORIHIKISAKI_NAME2.Text = string.Empty;
                this.form.TORIHIKISAKI_KEISHOU2.Text = this.entitysM_SYS_INFO.TORIHIKISAKI_KEISHOU2;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                this.form.TORIHIKISAKI_TEL.Text = string.Empty;
                this.form.TORIHIKISAKI_FAX.Text = string.Empty;
                this.form.EIGYOU_TANTOU_BUSHO_CD.Text = string.Empty;
                this.form.EIGYOU_TANTOU_BUSHO_NAME.Text = string.Empty;
                this.form.EIGYOU_TANTOU_CD.Text = string.Empty;
                this.form.EIGYOU_TANTOU_NAME.Text = string.Empty;
                this.form.TEKIYOU_BEGIN.Value = parentForm.sysDate;
                this.form.TEKIYOU_END.Value = null;
                this.form.CHUUSHI_RIYUU1.Text = string.Empty;
                this.form.CHUUSHI_RIYUU2.Text = string.Empty;

                // 基本情報タブ
                this.form.TORIHIKISAKI_POST.Text = string.Empty;
                this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text = string.Empty;
                this.form.TORIHIKISAKI_TODOUFUKEN_NAME.Text = string.Empty;
                this.form.TORIHIKISAKI_ADDRESS1.Text = string.Empty;
                this.form.TORIHIKISAKI_ADDRESS2.Text = string.Empty;
                this.form.BUSHO.Text = string.Empty;
                this.form.TANTOUSHA.Text = string.Empty;
                this.form.SHUUKEI_ITEM_CD.Text = string.Empty;
                this.form.SHUUKEI_KOUMOKU_NAME.Text = string.Empty;
                this.form.GYOUSHU_CD.Text = string.Empty;
                this.form.GYOUSHU_NAME.Text = string.Empty;
                this.form.DAIHYOU_PRINT_KBN.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                {
                    this.form.DAIHYOU_PRINT_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_DAIHYOU_PRINT_KBN.ToString();
                }
                this.form.BIKOU1.Text = string.Empty;
                this.form.BIKOU2.Text = string.Empty;
                this.form.BIKOU3.Text = string.Empty;
                this.form.BIKOU4.Text = string.Empty;
                this.form.SHOKUCHI_KBN.Checked = false;

                //請求情報1タブ
                this.form.TORIHIKI_KBN.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_TORIHIKI_KBN.IsNull)
                {
                    this.form.TORIHIKI_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_TORIHIKI_KBN.ToString();
                }
                this.form.TAX_HASUU_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_TAX_HASUU_CD.IsNull)
                {
                    this.form.TAX_HASUU_CD.Text = this.entitysM_SYS_INFO.SEIKYUU_TAX_HASUU_CD.ToString();
                }
                this.form.KINGAKU_HASUU_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KINGAKU_HASUU_CD.IsNull)
                {
                    this.form.KINGAKU_HASUU_CD.Text = this.entitysM_SYS_INFO.SEIKYUU_KINGAKU_HASUU_CD.ToString();
                }
                this.form.ZEI_KEISAN_KBN_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_ZEI_KEISAN_KBN_CD.IsNull)
                {
                    //if (this.entitysM_SYS_INFO.SYS_ZEI_KEISAN_KBN_USE_KBN.ToString() == "1")
                    //{
                    //    this.form.ZEI_KEISAN_KBN_CD.Text = "1";
                    //}
                    //else if (this.entitysM_SYS_INFO.SYS_ZEI_KEISAN_KBN_USE_KBN.ToString() == "2")
                    //{
                    //    this.form.ZEI_KEISAN_KBN_CD.Text = "3";
                    //}
                    // 税区分利用形態は後で考慮する
                    this.form.ZEI_KEISAN_KBN_CD.Text = this.entitysM_SYS_INFO.SEIKYUU_ZEI_KEISAN_KBN_CD.ToString();
                }
                this.form.ZEI_KBN_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_ZEI_KBN_CD.IsNull)
                {
                    this.form.ZEI_KBN_CD.Text = this.entitysM_SYS_INFO.SEIKYUU_ZEI_KBN_CD.ToString();
                }

                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                // 出力区分
                this.form.OUTPUT_KBN.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_OUTPUT_KBN.IsNull)
                {
                    this.form.OUTPUT_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_OUTPUT_KBN.ToString();
                }
                // 出力区分変更後処理                
                this.ChangeOutputKbn();
                //160026 S
                this.form.KAISHUU_BETSU_KBN.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_BETSU_KBN.IsNull)
                {
                    this.form.KAISHUU_BETSU_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_BETSU_KBN.Value.ToString();
                }
                this.form.KAISHUU_BETSU_KBN_TextChanged(null, null);
                this.form.KAISHUU_BETSU_NICHIGO.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_BETSU_NICHIGO.IsNull)
                {
                    this.form.KAISHUU_BETSU_NICHIGO.Text = this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_BETSU_NICHIGO.Value.ToString();
                }
                //160026 E
                // 20160429 koukoukon v2.1_電子請求書 #16612 end

                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
                this.form.INXS_SEIKYUU_KBN.Text = "2";
                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

                // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
                this.form.INXS_SHIHARAI_KBN.Text = "2";
                // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

                // 振込銀行
                this.form.FURIKOMI_BANK_CD.Text = string.Empty;
                this.form.FURIKOMI_BANK_NAME.Text = string.Empty;
                this.form.FURIKOMI_BANK_SHITEN_CD.Text = string.Empty;
                this.form.KOUZA_SHURUI.Text = string.Empty;
                this.form.KOUZA_NO.Text = string.Empty;
                this.form.KOUZA_NAME.Text = string.Empty;
                this.form.FURIKOMI_BANK_SHITEN_NAME.Text = string.Empty;

                this.form.FURIKOMI_BANK_CD_2.Text = string.Empty;
                this.form.FURIKOMI_BANK_NAME_2.Text = string.Empty;
                this.form.FURIKOMI_BANK_SHITEN_CD_2.Text = string.Empty;
                this.form.KOUZA_SHURUI_2.Text = string.Empty;
                this.form.KOUZA_NO_2.Text = string.Empty;
                this.form.KOUZA_NAME_2.Text = string.Empty;
                this.form.FURIKOMI_BANK_SHITEN_NAME_2.Text = string.Empty;

                this.form.FURIKOMI_BANK_CD_3.Text = string.Empty;
                this.form.FURIKOMI_BANK_NAME_3.Text = string.Empty;
                this.form.FURIKOMI_BANK_SHITEN_CD_3.Text = string.Empty;
                this.form.KOUZA_SHURUI_3.Text = string.Empty;
                this.form.KOUZA_NO_3.Text = string.Empty;
                this.form.KOUZA_NAME_3.Text = string.Empty;
                this.form.FURIKOMI_BANK_SHITEN_NAME_3.Text = string.Empty;

                this.form.LAST_TORIHIKI_DATE.Text = string.Empty;

                // 請求情報2タブ
                this.form.SEIKYUU_JOUHOU1.Text = string.Empty;
                this.form.SEIKYUU_JOUHOU2.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_NAME1.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_KEISHOU1.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_NAME2.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_KEISHOU2.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_POST.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_ADDRESS1.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_ADDRESS2.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_BUSHO.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_TANTOU.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_TEL.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_FAX.Text = string.Empty;
                this.form.NYUUKINSAKI_CD.Enabled = false;
                this.form.NYUUKINSAKI_CD.Text = string.Empty;
                this.form.NYUUKINSAKI_NAME1.Text = string.Empty;
                this.form.NYUUKINSAKI_NAME2.Text = string.Empty;
                this.form.SEIKYUU_TANTOU.Text = string.Empty;
                this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;
                this.form.FURIKOMI_NAME1.Text = string.Empty;
                this.form.FURIKOMI_NAME2.Text = string.Empty;
                //初期値をセットする。
                //20150617 #3747 hoanghm start
                //if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                //if copy mod then don not set default value to FURIKOMI_BANK
                //if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG && string.IsNullOrEmpty(this.TorihikisakiCd))
                {
                    this.SetDefaultValueFromJisyaJyouhouToFURIKOMI_BANK();
                }
                //20150617 #3747 hoanghm end

                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                {
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_DAIHYOU_PRINT_KBN.ToString();
                }
                this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_PRINT_KBN.ToString();
                }
                this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_CD.IsNull)
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_CD.ToString()));
                }
                this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                M_KYOTEN seikyuuKyoten = this.daoIM_KYOTEN.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                if (seikyuuKyoten != null)
                {
                    this.form.SEIKYUU_KYOTEN_NAME.Text = seikyuuKyoten.KYOTEN_NAME_RYAKU;
                }
                bool catchErr = this.ChangeTorihikiKbn(false);
                if (catchErr)
                {
                    return true;
                }
                catchErr = this.ChangeSeikyuuKyotenPrintKbn();
                if (catchErr)
                {
                    return true;
                }

                // 支払情報1タブ
                this.form.SHIHARAI_TORIHIKI_KBN_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_TORIHIKI_KBN.IsNull)
                {
                    this.form.SHIHARAI_TORIHIKI_KBN_CD.Text = this.entitysM_SYS_INFO.SHIHARAI_TORIHIKI_KBN.ToString();
                }
                this.form.SHIHARAI_TAX_HASUU_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_TAX_HASUU_CD.IsNull)
                {
                    this.form.SHIHARAI_TAX_HASUU_CD.Text = this.entitysM_SYS_INFO.SHIHARAI_TAX_HASUU_CD.ToString();
                }
                this.form.SHIHARAI_KINGAKU_HASUU_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_KINGAKU_HASUU_CD.IsNull)
                {
                    this.form.SHIHARAI_KINGAKU_HASUU_CD.Text = this.entitysM_SYS_INFO.SHIHARAI_KINGAKU_HASUU_CD.ToString();
                }
                this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_ZEI_KEISAN_KBN_CD.IsNull)
                {
                    //if (this.entitysM_SYS_INFO.SYS_ZEI_KEISAN_KBN_USE_KBN.ToString() == "1")
                    //{
                    //    this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = "1";
                    //}
                    //else if (this.entitysM_SYS_INFO.SYS_ZEI_KEISAN_KBN_USE_KBN.ToString() == "2")
                    //{
                    //    this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = "3";
                    //}
                    // 税区分利用形態は後で考慮する
                    this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = this.entitysM_SYS_INFO.SHIHARAI_ZEI_KEISAN_KBN_CD.ToString();
                }
                this.form.SHIHARAI_ZEI_KBN_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_ZEI_KBN_CD.IsNull)
                {
                    this.form.SHIHARAI_ZEI_KBN_CD.Text = this.entitysM_SYS_INFO.SHIHARAI_ZEI_KBN_CD.ToString();
                }
                this.form.LAST_TORIHIKI_DATE_SHIHARAI.Text = string.Empty;
                //160026 S
                this.form.SHIHARAI_BETSU_KBN.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_KAISHUU_BETSU_KBN.IsNull)
                {
                    this.form.SHIHARAI_BETSU_KBN.Text = this.entitysM_SYS_INFO.SHIHARAI_KAISHUU_BETSU_KBN.Value.ToString();
                }
                this.form.SHIHARAI_BETSU_KBN_TextChanged(null, null);
                this.form.SHIHARAI_BETSU_NICHIGO.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_KAISHUU_BETSU_NICHIGO.IsNull)
                {
                    this.form.SHIHARAI_BETSU_NICHIGO.Text = this.entitysM_SYS_INFO.SHIHARAI_KAISHUU_BETSU_NICHIGO.Value.ToString();
                }
                //160026 E
                this.form.TOUROKU_NO.Text = string.Empty;

                // 支払情報2タブ
                this.form.SHIHARAI_JOUHOU1.Text = string.Empty;
                this.form.SHIHARAI_JOUHOU2.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_NAME1.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_KEISHOU1.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_NAME2.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_KEISHOU2.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_POST.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_ADDRESS1.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_ADDRESS2.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_BUSHO.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_TANTOU.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_TEL.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_FAX.Text = string.Empty;
                this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_PRINT_KBN.ToString();
                }
                this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_CD.IsNull)
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_CD.ToString()));
                }
                this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                M_KYOTEN shiharaiKyoten = this.daoIM_KYOTEN.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                if (shiharaiKyoten != null)
                {
                    this.form.SHIHARAI_KYOTEN_NAME.Text = shiharaiKyoten.KYOTEN_NAME_RYAKU;
                }
                //160026 S
                this.form.FURIKOMI_EXPORT_KBN.Text = "2";
                this.form.FURIKOMI_EXPORT_KBN_TextChanged(null, null);
                this.form.TEI_SUU_RYOU_KBN.Text = "1";
                this.form.FURI_KOMI_MOTO_BANK_CD.Text = string.Empty;
                this.form.FURI_KOMI_MOTO_BANK_NAME.Text = string.Empty;
                this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text = string.Empty;
                this.form.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = string.Empty;
                this.form.FURI_KOMI_MOTO_SHURUI.Text = string.Empty;
                this.form.FURI_KOMI_MOTO_NO.Text = string.Empty;
                this.form.FURI_KOMI_MOTO_NAME.Text = string.Empty;
                this.SetDefaultValueFromFURIKOMI_BANK_MOTO();
                //160026 E
                catchErr = this.ChangeSiharaiTorihikiKbn();
                if (catchErr)
                {
                    return true;
                }
                catchErr = this.ChangeShiharaiKyotenPrintKbn();
                if (catchErr)
                {
                    return true;
                }

                // 取引先分類タブ
                this.form.MANI_HENSOUSAKI_KBN.Checked = false;
                this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_NAME1.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_NAME2.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_POST.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_ADDRESS1.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_ADDRESS2.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_BUSHO.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_TANTOU.Text = string.Empty;

                this.form.TORIHIKISAKI_CD.Enabled = true;   // 取引先CD
                this.form.SAIBAN_BUTTON.Enabled = true;    // 採番ボタン

                // mapbox項目
                this.form.TorihikisakiLatitude.Text = string.Empty;
                this.form.TorihikisakiLongitude.Text = string.Empty;
                this.form.LocationInfoUpdateName.Text = string.Empty;
                this.form.LocationInfoUpdateDate.Text = string.Empty;

                // functionボタン
                parentForm.bt_func2.Enabled = true;     // 新規
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func7.Enabled = true;     // 一覧
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = true;    // 取消
                parentForm.bt_func12.Enabled = true;    // 閉じる

                // 業者分類タブ初期化
                catchErr = this.ManiCheckOffCheck();
                if (catchErr)
                {
                    return true;
                }

                this.form.GYOUSHA_ICHIRAN.DataSource = null;
                this.form.GYOUSHA_ICHIRAN.Rows.Clear();

                // 初期フォーカス
                this.form.NYUUKINSAKI_KBN.Focus();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInitNewMode", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitNewMode", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 処理モード別画面初期化処理
        /// </summary>
        /// <param name="windowType"></param>
        public bool ModeInit(WINDOW_TYPE windowType, BusinessBaseForm parentForm)
        {
            try
            {
                if (this.IsFromShouninzumiDenshiShinseiIchiran)
                {
                    // 電子申請から呼び出されたときは、コードを移動する
                    if (!String.IsNullOrEmpty(this.form.ShinseiTorihikisakiCd))
                    {
                        this.TorihikisakiCd = this.form.ShinseiTorihikisakiCd;
                    }
                    else
                    {
                        this.TorihikisakiCd = this.form.ShinseiHikiaiTorihikisakiCd;
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
                        break;

                    // 【削除】モード
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        this.WindowInitDelete(parentForm);
                        break;

                    // 【参照】モード
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        catchErr = this.WindowInitReference(parentForm);
                        break;

                    // デフォルトは【新規】モード
                    default:
                        this.WindowInitNew(parentForm);
                        break;
                }
                if (catchErr)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("ModeInit", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        /// <summary>
        /// 画面項目初期化【複写】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        private void WindowInitNewCopyMode(BusinessBaseForm parentForm)
        {
            // 画面を新規状態で初期化
            this.WindowInitNewMode(parentForm);

            // 検索結果を画面に設定
            this.SetWindowData();
            this.WindowInitNewMode(parentForm);
            this.SetDataForWindow();

            // functionボタン
            parentForm.bt_func2.Enabled = true;     // 新規
            parentForm.bt_func3.Enabled = false;    // 修正
            parentForm.bt_func7.Enabled = true;     // 一覧
            parentForm.bt_func9.Enabled = true;     // 登録
            parentForm.bt_func11.Enabled = true;    // 取消
            parentForm.bt_func12.Enabled = true;    // 閉じる

            //複写モード時
            //取引先コードのコピーはなし
            this.form.TORIHIKISAKI_CD.Text = string.Empty;

            // 承認済申請一覧から遷移時は引合取引先の適用開始・終了日を優先
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

            //最終取引日時
            this.form.LAST_TORIHIKI_DATE.Text = string.Empty;
            this.form.LAST_TORIHIKI_DATE_SHIHARAI.Text = string.Empty;

            // 発行先コード
            if (!this.IsFromShouninzumiDenshiShinseiIchiran)
            {
                this.form.HAKKOUSAKI_CD.Text = string.Empty;
            }

            // 複写モード時はmapbox項目のコピーなし
            this.form.TorihikisakiLatitude.Text = string.Empty;
            this.form.TorihikisakiLongitude.Text = string.Empty;
            this.form.LocationInfoUpdateName.Text = string.Empty;
            this.form.LocationInfoUpdateDate.Text = string.Empty;

            // 業者分類タブ初期化
            bool catchErr = this.ManiCheckOffCheck();
            if (catchErr)
            {
                throw new Exception("");
            }

            // 初期フォーカス
            this.form.NYUUKINSAKI_KBN.Focus();
        }

        /// <summary>
        /// データを取得し、画面に設定
        /// </summary>
        private void SetWindowData()
        {
            if (false == this.IsFromShouninzumiDenshiShinseiIchiran)
            {
                // 通常の取得処理
                entitysTORIHIKISAKI = daoTORIHIKISAKI.GetDataByCd(this.TorihikisakiCd);
                entitysTORIHIKISAKI_SEIKYUU = daoTORIHIKISAKI_SEIKYUU.GetDataByCd(this.TorihikisakiCd);
                entitysTORIHIKISAKI_SHIHARAI = daoTORIHIKISAKI_SHIHARAI.GetDataByCd(this.TorihikisakiCd);
            }
            else
            {
                if (null == this.form.ShinseiHikiaiTorihikisakiCd || String.IsNullOrEmpty(this.form.ShinseiHikiaiTorihikisakiCd))
                {
                    // 修正申請なので仮取引先を取得
                    this.SearchKariTorihikisaki();
                    this.SearchKariTorihikisakiSeikyuu();
                    this.SearchKariTorihikisakiShiharai();

                    // 取引先エンティティにコピー
                    this.CopyKariTorihikisakiEntityToTorihikisakiEntity();
                    this.CopyKariTorihikisakiSeikyuuEntityToTorihikisakiSeikyuuEntity();
                    this.CopyKariTorihikisakiShiharaiEntityToTorihikisakiShiharaiEntity();
                }
                else
                {
                    // 新規申請なので引合取引先を取得
                    this.SearchHikiaiTorihikisaki();
                    this.SearchHikiaiTorihikisakiSeikyuu();
                    this.SearchHikiaiTorihikisakiShiharai();

                    // 取引先エンティティにコピー
                    this.CopyHikiaiTorihikisakiEntityToTorihikisakiEntity();
                    this.CopyHikiaiTorihikisakiSeikyuuEntityToTorihikisakiSeikyuuEntity();
                    this.CopyHikiaiTorihikisakiShiharaiEntityToTorihikisakiShiharaiEntity();
                }
            }
        }

        /// <summary>
        /// データをDBから取得
        /// </summary>
        public void SetDataForWindow()
        {
            // ヘッダー項目
            DetailedHeaderForm header = (DetailedHeaderForm)((BusinessBaseForm)this.form.ParentForm).headerForm;
            header.CreateDate.Text = this.entitysTORIHIKISAKI.CREATE_DATE.ToString();
            header.CreateUser.Text = this.entitysTORIHIKISAKI.CREATE_USER;
            header.LastUpdateDate.Text = this.entitysTORIHIKISAKI.UPDATE_DATE.ToString();
            header.LastUpdateUser.Text = this.entitysTORIHIKISAKI.UPDATE_USER;

            // 共通情報
            if (!this.entitysTORIHIKISAKI.NYUUKINSAKI_KBN.IsNull)
            {
                this.form.NYUUKINSAKI_KBN.Text = this.entitysTORIHIKISAKI.NYUUKINSAKI_KBN.ToString();
            }
            if (!this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD.IsNull)
            {
                this.entitysM_KYOTEN = this.daoIM_KYOTEN.GetDataByCd(this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD.ToString());
                this.form.TORIHIKISAKI_KYOTEN_CD.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD.Value.ToString("00");
                if (this.entitysM_KYOTEN != null)
                {
                    this.form.KYOTEN_NAME_RYAKU.Text = this.entitysM_KYOTEN.KYOTEN_NAME_RYAKU;
                }
            }
            this.form.TORIHIKISAKI_CD.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_CD;
            this.form.TORIHIKISAKI_NAME1.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME1;
            this.form.TORIHIKISAKI_FURIGANA.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_FURIGANA;
            this.form.TORIHIKISAKI_NAME2.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME2;
            this.form.TORIHIKISAKI_KEISHOU1.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU1;
            this.form.TORIHIKISAKI_KEISHOU2.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU2;
            this.form.TORIHIKISAKI_NAME_RYAKU.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU;
            this.form.TORIHIKISAKI_TEL.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_TEL;
            this.form.TORIHIKISAKI_FAX.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_FAX;
            this.form.TORIHIKISAKI_KEISHOU1.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU1;
            this.form.TORIHIKISAKI_KEISHOU2.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU2;
            this.form.EIGYOU_TANTOU_BUSHO_CD.Text = this.entitysTORIHIKISAKI.EIGYOU_TANTOU_BUSHO_CD;
            if (!string.IsNullOrEmpty(this.entitysTORIHIKISAKI.EIGYOU_TANTOU_BUSHO_CD))
            {
                this.entitysM_BUSHO = this.daoIM_BUSHO.GetDataByCd(this.entitysTORIHIKISAKI.EIGYOU_TANTOU_BUSHO_CD);
                if (entitysM_BUSHO != null)
                {
                    this.form.EIGYOU_TANTOU_BUSHO_NAME.Text = this.entitysM_BUSHO.BUSHO_NAME_RYAKU;
                }
            }
            this.form.EIGYOU_TANTOU_CD.Text = this.entitysTORIHIKISAKI.EIGYOU_TANTOU_CD;
            if (!string.IsNullOrEmpty(this.entitysTORIHIKISAKI.EIGYOU_TANTOU_CD))
            {
                this.entitysM_SHAIN = this.daoIM_SHAIN.GetDataByCd(this.entitysTORIHIKISAKI.EIGYOU_TANTOU_CD);
                if (entitysM_SHAIN != null)
                {
                    this.form.EIGYOU_TANTOU_NAME.Text = this.entitysM_SHAIN.SHAIN_NAME_RYAKU;
                }
            }
            if (!this.entitysTORIHIKISAKI.TEKIYOU_BEGIN.IsNull)
            {
                this.form.TEKIYOU_BEGIN.Value = (DateTime)this.entitysTORIHIKISAKI.TEKIYOU_BEGIN;
            }
            if (!this.entitysTORIHIKISAKI.TEKIYOU_END.IsNull)
            {
                this.form.TEKIYOU_END.Value = (DateTime)this.entitysTORIHIKISAKI.TEKIYOU_END;
            }
            this.form.CHUUSHI_RIYUU1.Text = this.entitysTORIHIKISAKI.CHUUSHI_RIYUU1;
            this.form.CHUUSHI_RIYUU2.Text = this.entitysTORIHIKISAKI.CHUUSHI_RIYUU2;

            // 20150818 TIME_STAMP対応 画面のTIME_STAMPテキストボックス削除 Start
            //// タイムスタンプ（排他制御に必要）
            //if (null != this.entitysTORIHIKISAKI.TIME_STAMP)
            //{
            //    this.form.TIME_STAMP.Text = ConvertStrByte.ByteToString(this.entitysTORIHIKISAKI.TIME_STAMP);
            //}
            // 20150818 TIME_STAMP対応 画面のTIME_STAMPテキストボックス削除 End

            // 基本情報タブ
            this.form.TORIHIKISAKI_POST.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_POST;
            if (!this.entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD.IsNull)
            {
                this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text = string.Format("{0:D" + this.form.TORIHIKISAKI_TODOUFUKEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD.ToString()));
                this.entitysM_TODOUFUKEN = this.daoIM_TODOUFUKEN.GetDataByCd(this.entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD.ToString());
                if (this.entitysM_TODOUFUKEN != null)
                {
                    this.form.TORIHIKISAKI_TODOUFUKEN_NAME.Text = this.entitysM_TODOUFUKEN.TODOUFUKEN_NAME;
                }
            }
            this.form.TORIHIKISAKI_ADDRESS1.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS1;
            this.form.TORIHIKISAKI_ADDRESS2.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS2;
            this.form.BUSHO.Text = this.entitysTORIHIKISAKI.BUSHO;
            this.form.TANTOUSHA.Text = this.entitysTORIHIKISAKI.TANTOUSHA;
            this.form.SHUUKEI_ITEM_CD.Text = this.entitysTORIHIKISAKI.SHUUKEI_ITEM_CD;
            if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI.SHUUKEI_ITEM_CD))
            {
                this.entitysM_SHUUKEI_KOUMOKU = this.daoIM_SHUUKEI_KOUMOKU.GetDataByCd(this.entitysTORIHIKISAKI.SHUUKEI_ITEM_CD);
                if (this.entitysM_SHUUKEI_KOUMOKU != null)
                {
                    this.form.SHUUKEI_KOUMOKU_NAME.Text = this.entitysM_SHUUKEI_KOUMOKU.SHUUKEI_KOUMOKU_NAME_RYAKU;
                }
            }
            this.form.GYOUSHU_CD.Text = this.entitysTORIHIKISAKI.GYOUSHU_CD;
            if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI.GYOUSHU_CD))
            {
                this.entitysM_GYOUSHU = this.daoIM_GYOUSHU.GetDataByCd(this.entitysTORIHIKISAKI.GYOUSHU_CD);
                if (this.entitysM_GYOUSHU != null)
                {
                    this.form.GYOUSHU_NAME.Text = this.entitysM_GYOUSHU.GYOUSHU_NAME_RYAKU;
                }
            }
            this.form.DAIHYOU_PRINT_KBN.Text = string.Empty;
            if (!this.entitysTORIHIKISAKI.DAIHYOU_PRINT_KBN.IsNull)
            {
                this.form.DAIHYOU_PRINT_KBN.Text = this.entitysTORIHIKISAKI.DAIHYOU_PRINT_KBN.ToString();
            }
            this.form.BIKOU1.Text = this.entitysTORIHIKISAKI.BIKOU1;
            this.form.BIKOU2.Text = this.entitysTORIHIKISAKI.BIKOU2;
            this.form.BIKOU3.Text = this.entitysTORIHIKISAKI.BIKOU3;
            this.form.BIKOU4.Text = this.entitysTORIHIKISAKI.BIKOU4;
            this.form.SHOKUCHI_KBN.Checked = (bool)this.entitysTORIHIKISAKI.SHOKUCHI_KBN;

            //請求情報1タブ
            if (this.entitysTORIHIKISAKI_SEIKYUU != null)
            {
                if (!this.entitysTORIHIKISAKI_SEIKYUU.TORIHIKI_KBN_CD.IsNull)
                {
                    this.form.TORIHIKI_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.TORIHIKI_KBN_CD.ToString();
                }
                else
                {
                    this.form.TORIHIKI_KBN.Text = "";
                }
                bool catchErr = this.ChangeTorihikiKbn(false);
                if (catchErr)
                {
                    throw new Exception("");
                }

                if (this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI1.IsNull)
                {
                    this.form.SHIMEBI1.Text = "";
                    this.form.SHIMEBI2.Enabled = false;
                    this.form.SHIMEBI3.Enabled = false;
                }
                else
                {
                    this.form.SHIMEBI1.Text = this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI1.ToString();
                    this.form.SHIMEBI2.Enabled = true;
                }

                if (this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI2.IsNull)
                {
                    this.form.SHIMEBI2.Text = "";
                    this.form.SHIMEBI3.Enabled = false;
                }
                else
                {
                    this.form.SHIMEBI2.Text = this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI2.ToString();
                    this.form.SHIMEBI3.Enabled = true;
                }

                if (this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI3.IsNull)
                {
                    this.form.SHIMEBI3.Text = "";
                }
                else
                {
                    this.form.SHIMEBI3.Text = this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI3.ToString();
                }

                if (!this.entitysTORIHIKISAKI_SEIKYUU.HICCHAKUBI.IsNull)
                {
                    this.form.HICCHAKUBI.Text = this.entitysTORIHIKISAKI_SEIKYUU.HICCHAKUBI.ToString();
                }
                else
                {
                    this.form.HICCHAKUBI.Text = "";
                }
                //#160026 S
                if (!this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_BETSU_KBN.IsNull)
                {
                    this.form.KAISHUU_BETSU_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_BETSU_KBN.ToString();
                }
                else
                {
                    this.form.KAISHUU_BETSU_KBN.Text = "";
                }
                this.form.KAISHUU_BETSU_KBN_TextChanged(null, null);
                if (!this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_BETSU_NICHIGO.IsNull)
                {
                    this.form.KAISHUU_BETSU_NICHIGO.Text = this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_BETSU_NICHIGO.ToString();
                }
                else
                {
                    this.form.KAISHUU_BETSU_NICHIGO.Text = "";
                }
                //#160026 E

                if (!this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_MONTH.IsNull)
                {
                    this.form.KAISHUU_MONTH.Text = this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_MONTH.ToString();
                }
                else
                {
                    this.form.KAISHUU_MONTH.Text = "";
                }

                if (!this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_DAY.IsNull)
                {
                    this.form.KAISHUU_DAY.Text = this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_DAY.ToString();
                }
                else
                {
                    this.form.KAISHUU_DAY.Text = "";
                }

                if (!this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_HOUHOU.IsNull)
                {
                    this.form.KAISHUU_HOUHOU.Text = string.Format("{0:D" + this.form.KAISHUU_HOUHOU.CharactersNumber + "}", Int16.Parse(this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_HOUHOU.ToString()));
                    this.entitysM_NYUUSHUKKIN_KBN = this.daoIM_NYUUSHUKKIN_KBN.GetDataByCd((int)this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_HOUHOU);
                    if (this.entitysM_NYUUSHUKKIN_KBN != null)
                    {
                        this.form.KAISHUU_HOUHOU_NAME.Text = this.entitysM_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME_RYAKU;
                    }
                }
                else
                {
                    this.form.KAISHUU_HOUHOU.Text = "";
                    this.form.KAISHUU_HOUHOU_NAME.Text = "";
                }

                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG && !string.IsNullOrEmpty(this.TorihikisakiCd))
                {
                    this.form.KAISHI_URIKAKE_ZANDAKA.Text = "0";
                }
                else if (!this.entitysTORIHIKISAKI_SEIKYUU.KAISHI_URIKAKE_ZANDAKA.IsNull)
                {
                    this.SetZandakaFormat(this.entitysTORIHIKISAKI_SEIKYUU.KAISHI_URIKAKE_ZANDAKA.ToString(), this.form.KAISHI_URIKAKE_ZANDAKA);
                }
                Zandaka_U = this.form.KAISHI_URIKAKE_ZANDAKA.Text;

                if (!this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_KBN.IsNull)
                {
                    this.form.SHOSHIKI_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_KBN.ToString();
                }
                else
                {
                    this.form.SHOSHIKI_KBN.Text = "";
                }

                if (!this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_MEISAI_KBN.IsNull)
                {
                    this.form.SHOSHIKI_MEISAI_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_MEISAI_KBN.ToString();
                }
                else
                {
                    this.form.SHOSHIKI_MEISAI_KBN.Text = "";
                }

                if (!this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_GENBA_KBN.IsNull)
                {
                    this.form.SHOSHIKI_GENBA_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_GENBA_KBN.ToString();
                }
                else
                {
                    this.form.SHOSHIKI_GENBA_KBN.Text = "";
                }

                if (!this.entitysTORIHIKISAKI_SEIKYUU.TAX_HASUU_CD.IsNull)
                {
                    this.form.TAX_HASUU_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.TAX_HASUU_CD.ToString();
                }
                else
                {
                    this.form.TAX_HASUU_CD.Text = "";
                }
                if (!this.entitysTORIHIKISAKI_SEIKYUU.KINGAKU_HASUU_CD.IsNull)
                {
                    this.form.KINGAKU_HASUU_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.KINGAKU_HASUU_CD.ToString();
                }
                else
                {
                    this.form.KINGAKU_HASUU_CD.Text = "";
                }
                if (!this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KEITAI_KBN.IsNull)
                {
                    this.form.SEIKYUU_KEITAI_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KEITAI_KBN.ToString();
                }
                else
                {
                    this.form.SEIKYUU_KEITAI_KBN.Text = "";
                }
                if (!this.entitysTORIHIKISAKI_SEIKYUU.NYUUKIN_MEISAI_KBN.IsNull)
                {
                    this.form.NYUUKIN_MEISAI_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.NYUUKIN_MEISAI_KBN.ToString();
                }
                else
                {
                    this.form.NYUUKIN_MEISAI_KBN.Text = "";
                }
                if (!this.entitysTORIHIKISAKI_SEIKYUU.YOUSHI_KBN.IsNull)
                {
                    this.form.YOUSHI_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.YOUSHI_KBN.ToString();
                }
                else
                {
                    this.form.YOUSHI_KBN.Text = "";
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                // 出力区分
                if (!this.entitysTORIHIKISAKI_SEIKYUU.OUTPUT_KBN.IsNull)
                {
                    this.form.OUTPUT_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.OUTPUT_KBN.ToString();
                }
                else
                {
                    this.form.OUTPUT_KBN.Text = "";
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 end

                //Begin: LANDUONG - 20220209 - refs#160050        
                this.ChangeOutputKbn();
                if (this.form.OUTPUT_KBN.Text.Equals("2"))
                {
                    // 発行先コード
                    this.form.HAKKOUSAKI_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.HAKKOUSAKI_CD;
                    // 楽楽顧客コード
                    this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;
                }
                else if (this.form.OUTPUT_KBN.Text.Equals("3"))
                {
                    // 発行先コード                    
                    this.form.HAKKOUSAKI_CD.Text = string.Empty;
                    // 楽楽顧客コード
                    if(this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG && !string.IsNullOrEmpty(this.TorihikisakiCd))
                    {
                        this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;
                    }
                    else
                    {
                        this.form.RAKURAKU_CUSTOMER_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.RAKURAKU_CUSTOMER_CD;
                    }                    
                }
                else
                {
                    // 発行先コード
                    this.form.HAKKOUSAKI_CD.Text = string.Empty;
                    // 楽楽顧客コード
                    this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;
                }
                //End: LANDUONG - 20220209 - refs#160050

                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
                if (!this.entitysTORIHIKISAKI_SEIKYUU.INXS_SEIKYUU_KBN.IsNull)
                {
                    this.form.INXS_SEIKYUU_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.INXS_SEIKYUU_KBN.ToString();
                }
                else
                {
                    this.form.INXS_SEIKYUU_KBN.Text = string.Empty;
                }
                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

                if (!this.entitysTORIHIKISAKI_SEIKYUU.ZEI_KEISAN_KBN_CD.IsNull)
                {
                    this.form.ZEI_KEISAN_KBN_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.ZEI_KEISAN_KBN_CD.ToString();
                }
                else
                {
                    this.form.ZEI_KEISAN_KBN_CD.Text = "";
                }
                if (!this.entitysTORIHIKISAKI_SEIKYUU.ZEI_KBN_CD.IsNull)
                {
                    this.form.ZEI_KBN_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.ZEI_KBN_CD.ToString();
                }
                else
                {
                    this.form.ZEI_KBN_CD.Text = "";
                }

                // 請求情報2タブ
                this.form.SEIKYUU_JOUHOU1.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_JOUHOU1;
                this.form.SEIKYUU_JOUHOU2.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_JOUHOU2;
                this.form.SEIKYUU_SOUFU_NAME1.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_NAME1;
                this.form.SEIKYUU_SOUFU_KEISHOU1.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_KEISHOU1;
                this.form.SEIKYUU_SOUFU_NAME2.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_NAME2;
                this.form.SEIKYUU_SOUFU_KEISHOU2.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_KEISHOU2;
                this.form.SEIKYUU_SOUFU_POST.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_POST;
                this.form.SEIKYUU_SOUFU_ADDRESS1.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_ADDRESS1;
                this.form.SEIKYUU_SOUFU_ADDRESS2.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_ADDRESS2;
                this.form.SEIKYUU_SOUFU_BUSHO.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_BUSHO;
                this.form.SEIKYUU_SOUFU_TANTOU.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_TANTOU;
                this.form.SEIKYUU_SOUFU_TEL.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_TEL;
                this.form.SEIKYUU_SOUFU_FAX.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_FAX;
                if (this.entitysTORIHIKISAKI.NYUUKINSAKI_KBN.ToString().Equals("1"))
                {
                    this.form.NYUUKINSAKI_CD.Enabled = false;
                }
                this.form.NYUUKINSAKI_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.NYUUKINSAKI_CD;
                if (!string.IsNullOrEmpty(this.entitysTORIHIKISAKI_SEIKYUU.NYUUKINSAKI_CD))
                {
                    this.entitysM_NYUUKINSAKI = this.daoNYUKINSAKI.GetDataByCd(this.entitysTORIHIKISAKI_SEIKYUU.NYUUKINSAKI_CD);
                    if (this.entitysM_NYUUKINSAKI != null)
                    {
                        this.form.NYUUKINSAKI_NAME1.Text = this.entitysM_NYUUKINSAKI.NYUUKINSAKI_NAME1;
                        this.form.NYUUKINSAKI_NAME2.Text = this.entitysM_NYUUKINSAKI.NYUUKINSAKI_NAME2;
                    }
                }
                if (!string.IsNullOrEmpty(this.entitysTORIHIKISAKI_SHIHARAI.SYUUKINSAKI_CD))
                {
                    this.entitysM_SYUKKINSAKI = this.daoSYUKINSAKI.GetDataByCd(this.entitysTORIHIKISAKI_SHIHARAI.SYUUKINSAKI_CD);
                }

                this.form.SEIKYUU_TANTOU.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_TANTOU;
                this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;
                if (!this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                {
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_DAIHYOU_PRINT_KBN.ToString();
                }
                this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                if (!this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_PRINT_KBN.ToString();
                }
                catchErr = this.ChangeSeikyuuKyotenPrintKbn();
                if (catchErr)
                {
                    throw new Exception(""); ;
                }

                this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                if (!this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_CD.IsNull)
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_CD.ToString()));
                    M_KYOTEN kyo = this.daoIM_KYOTEN.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                    if (kyo != null)
                    {
                        this.form.SEIKYUU_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                    }
                }
                this.form.FURIKOMI_NAME1.Text = entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_NAME1;
                this.form.FURIKOMI_NAME2.Text = entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_NAME2;

                //振込銀行
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD))
                {
                    this.form.FURIKOMI_BANK_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD;
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD))
                {
                    this.entitysM_BANK = this.daoIM_BANK.GetDataByCd(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD);
                    if (this.entitysM_BANK != null)
                    {
                        this.form.FURIKOMI_BANK_NAME.Text = this.entitysM_BANK.BANK_NAME_RYAKU;
                    }
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD))
                {
                    this.form.FURIKOMI_BANK_SHITEN_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD;
                    this.form.KOUZA_SHURUI.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI;
                    this.form.KOUZA_NO.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO;
                    //this.form.KOUZA_NAME.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NAME;
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD) && !string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD))
                {
                    M_BANK_SHITEN bankShiten = new M_BANK_SHITEN();
                    bankShiten.BANK_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD;
                    bankShiten.BANK_SHITEN_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD;
                    bankShiten.KOUZA_SHURUI = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI;
                    bankShiten.KOUZA_NO = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO;
                    this.entitysM_BANK_SHITEN = this.daoIM_BANK_SHITEN.GetDataByCd(bankShiten);
                    if (this.entitysM_BANK_SHITEN != null)
                    {
                        this.form.FURIKOMI_BANK_SHITEN_NAME.Text = this.entitysM_BANK_SHITEN.BANK_SHIETN_NAME_RYAKU;
                        this.form.KOUZA_NAME.Text = this.entitysM_BANK_SHITEN.KOUZA_NAME;
                    }
                }
                if (!this.entitysTORIHIKISAKI_SEIKYUU.LAST_TORIHIKI_DATE.IsNull)
                {
                    this.form.LAST_TORIHIKI_DATE.Text = string.Format("yyyy/MM/dd HH:mm:ss", this.entitysTORIHIKISAKI_SEIKYUU.LAST_TORIHIKI_DATE);
                }

                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD))
                {
                    this.form.FURIKOMI_BANK_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD;
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD))
                {
                    this.entitysM_BANK = this.daoIM_BANK.GetDataByCd(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD);
                    if (this.entitysM_BANK != null)
                    {
                        this.form.FURIKOMI_BANK_NAME.Text = this.entitysM_BANK.BANK_NAME_RYAKU;
                    }
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD))
                {
                    this.form.FURIKOMI_BANK_SHITEN_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD;
                    this.form.KOUZA_SHURUI.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI;
                    this.form.KOUZA_NO.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO;
                    //this.form.KOUZA_NAME.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NAME;
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD) && !string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD))
                {
                    M_BANK_SHITEN bankShiten = new M_BANK_SHITEN();
                    bankShiten.BANK_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD;
                    bankShiten.BANK_SHITEN_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD;
                    bankShiten.KOUZA_SHURUI = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI;
                    bankShiten.KOUZA_NO = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO;
                    this.entitysM_BANK_SHITEN = this.daoIM_BANK_SHITEN.GetDataByCd(bankShiten);
                    if (this.entitysM_BANK_SHITEN != null)
                    {
                        this.form.FURIKOMI_BANK_SHITEN_NAME.Text = this.entitysM_BANK_SHITEN.BANK_SHIETN_NAME_RYAKU;
                        this.form.KOUZA_NAME.Text = this.entitysM_BANK_SHITEN.KOUZA_NAME;
                    }
                }

                //振込銀行2
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2))
                {
                    this.form.FURIKOMI_BANK_CD_2.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2;
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2))
                {
                    this.entitysM_BANK = this.daoIM_BANK.GetDataByCd(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2);
                    if (this.entitysM_BANK != null)
                    {
                        this.form.FURIKOMI_BANK_NAME_2.Text = this.entitysM_BANK.BANK_NAME_RYAKU;
                    }
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_2))
                {
                    this.form.FURIKOMI_BANK_SHITEN_CD_2.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_2;
                    this.form.KOUZA_SHURUI_2.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI_2;
                    this.form.KOUZA_NO_2.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO_2;
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2) && !string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_2))
                {
                    M_BANK_SHITEN bankShiten = new M_BANK_SHITEN();
                    bankShiten.BANK_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2;
                    bankShiten.BANK_SHITEN_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_2;
                    bankShiten.KOUZA_SHURUI = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI_2;
                    bankShiten.KOUZA_NO = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO_2;
                    this.entitysM_BANK_SHITEN = this.daoIM_BANK_SHITEN.GetDataByCd(bankShiten);
                    if (this.entitysM_BANK_SHITEN != null)
                    {
                        this.form.FURIKOMI_BANK_SHITEN_NAME_2.Text = this.entitysM_BANK_SHITEN.BANK_SHIETN_NAME_RYAKU;
                        this.form.KOUZA_NAME_2.Text = this.entitysM_BANK_SHITEN.KOUZA_NAME;
                    }
                }

                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2))
                {
                    this.form.FURIKOMI_BANK_CD_2.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2;
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2))
                {
                    this.entitysM_BANK = this.daoIM_BANK.GetDataByCd(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2);
                    if (this.entitysM_BANK != null)
                    {
                        this.form.FURIKOMI_BANK_NAME_2.Text = this.entitysM_BANK.BANK_NAME_RYAKU;
                    }
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_2))
                {
                    this.form.FURIKOMI_BANK_SHITEN_CD_2.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_2;
                    this.form.KOUZA_SHURUI_2.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI_2;
                    this.form.KOUZA_NO_2.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO_2;
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2) && !string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_2))
                {
                    M_BANK_SHITEN bankShiten = new M_BANK_SHITEN();
                    bankShiten.BANK_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2;
                    bankShiten.BANK_SHITEN_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_2;
                    bankShiten.KOUZA_SHURUI = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI_2;
                    bankShiten.KOUZA_NO = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO_2;
                    this.entitysM_BANK_SHITEN = this.daoIM_BANK_SHITEN.GetDataByCd(bankShiten);
                    if (this.entitysM_BANK_SHITEN != null)
                    {
                        this.form.FURIKOMI_BANK_SHITEN_NAME_2.Text = this.entitysM_BANK_SHITEN.BANK_SHIETN_NAME_RYAKU;
                        this.form.KOUZA_NAME_2.Text = this.entitysM_BANK_SHITEN.KOUZA_NAME;
                    }
                }

                //振込銀行3
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3))
                {
                    this.form.FURIKOMI_BANK_CD_3.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3;
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3))
                {
                    this.entitysM_BANK = this.daoIM_BANK.GetDataByCd(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3);
                    if (this.entitysM_BANK != null)
                    {
                        this.form.FURIKOMI_BANK_NAME_3.Text = this.entitysM_BANK.BANK_NAME_RYAKU;
                    }
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_3))
                {
                    this.form.FURIKOMI_BANK_SHITEN_CD_3.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_3;
                    this.form.KOUZA_SHURUI_3.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI_3;
                    this.form.KOUZA_NO_3.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO_3;
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3) && !string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_3))
                {
                    M_BANK_SHITEN bankShiten = new M_BANK_SHITEN();
                    bankShiten.BANK_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3;
                    bankShiten.BANK_SHITEN_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_3;
                    bankShiten.KOUZA_SHURUI = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI_3;
                    bankShiten.KOUZA_NO = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO_3;
                    this.entitysM_BANK_SHITEN = this.daoIM_BANK_SHITEN.GetDataByCd(bankShiten);
                    if (this.entitysM_BANK_SHITEN != null)
                    {
                        this.form.FURIKOMI_BANK_SHITEN_NAME_3.Text = this.entitysM_BANK_SHITEN.BANK_SHIETN_NAME_RYAKU;
                        this.form.KOUZA_NAME_3.Text = this.entitysM_BANK_SHITEN.KOUZA_NAME;
                    }
                }

                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3))
                {
                    this.form.FURIKOMI_BANK_CD_3.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3;
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3))
                {
                    this.entitysM_BANK = this.daoIM_BANK.GetDataByCd(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3);
                    if (this.entitysM_BANK != null)
                    {
                        this.form.FURIKOMI_BANK_NAME_3.Text = this.entitysM_BANK.BANK_NAME_RYAKU;
                    }
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_3))
                {
                    this.form.FURIKOMI_BANK_SHITEN_CD_3.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_3;
                    this.form.KOUZA_SHURUI_3.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI_3;
                    this.form.KOUZA_NO_3.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO_3;
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3) && !string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_3))
                {
                    M_BANK_SHITEN bankShiten = new M_BANK_SHITEN();
                    bankShiten.BANK_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3;
                    bankShiten.BANK_SHITEN_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_3;
                    bankShiten.KOUZA_SHURUI = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI_3;
                    bankShiten.KOUZA_NO = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO_3;
                    this.entitysM_BANK_SHITEN = this.daoIM_BANK_SHITEN.GetDataByCd(bankShiten);
                    if (this.entitysM_BANK_SHITEN != null)
                    {
                        this.form.FURIKOMI_BANK_SHITEN_NAME_3.Text = this.entitysM_BANK_SHITEN.BANK_SHIETN_NAME_RYAKU;
                        this.form.KOUZA_NAME_3.Text = this.entitysM_BANK_SHITEN.KOUZA_NAME;
                    }
                }

            }

            //支払情報1タブ
            if (this.entitysTORIHIKISAKI_SHIHARAI != null)
            {
                if (!this.entitysTORIHIKISAKI_SHIHARAI.TORIHIKI_KBN_CD.IsNull)
                {
                    this.form.SHIHARAI_TORIHIKI_KBN_CD.Text = this.entitysTORIHIKISAKI_SHIHARAI.TORIHIKI_KBN_CD.ToString();
                }
                else
                {
                    this.form.SHIHARAI_TORIHIKI_KBN_CD.Text = "";
                }
                bool catchErr = this.ChangeSiharaiTorihikiKbn();
                if (catchErr)
                {
                    throw new Exception("");
                }

                if (this.entitysTORIHIKISAKI_SHIHARAI.SHIMEBI1.IsNull)
                {
                    this.form.SHIHARAI_SHIMEBI1.Text = "";
                    this.form.SHIHARAI_SHIMEBI2.Enabled = false;
                    this.form.SHIHARAI_SHIMEBI3.Enabled = false;
                }
                else
                {
                    this.form.SHIHARAI_SHIMEBI1.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIMEBI1.ToString();
                }

                if (this.entitysTORIHIKISAKI_SHIHARAI.SHIMEBI2.IsNull)
                {
                    this.form.SHIHARAI_SHIMEBI2.Text = "";
                    this.form.SHIHARAI_SHIMEBI3.Enabled = false;
                }
                else
                {
                    this.form.SHIHARAI_SHIMEBI2.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIMEBI2.ToString();
                }

                if (this.entitysTORIHIKISAKI_SHIHARAI.SHIMEBI3.IsNull)
                {
                    this.form.SHIHARAI_SHIMEBI3.Text = "";
                }
                else
                {
                    this.form.SHIHARAI_SHIMEBI3.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIMEBI3.ToString();
                }
                //160026 S
                if (!this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_BETSU_KBN.IsNull)
                {
                    this.form.SHIHARAI_BETSU_KBN.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_BETSU_KBN.ToString();
                }
                else
                {
                    this.form.SHIHARAI_BETSU_KBN.Text = "";
                }
                this.form.SHIHARAI_BETSU_KBN_TextChanged(null, null);
                if (!this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_BETSU_NICHIGO.IsNull)
                {
                    this.form.SHIHARAI_BETSU_NICHIGO.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_BETSU_NICHIGO.ToString();
                }
                else
                {
                    this.form.SHIHARAI_BETSU_NICHIGO.Text = "";
                }
                //160026 E

                if (!this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_MONTH.IsNull)
                {
                    this.form.SHIHARAI_MONTH.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_MONTH.ToString();
                }
                else
                {
                    this.form.SHIHARAI_MONTH.Text = "";
                }
                if (!this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_DAY.IsNull)
                {
                    this.form.SHIHARAI_DAY.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_DAY.ToString();
                }
                else
                {
                    this.form.SHIHARAI_DAY.Text = "";
                }
                if (!this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_HOUHOU.IsNull)
                {
                    this.form.SHIHARAI_HOUHOU.Text = string.Format("{0:D" + this.form.SHIHARAI_HOUHOU.CharactersNumber + "}", Int16.Parse(this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_HOUHOU.ToString()));
                    this.entitysM_NYUUSHUKKIN_KBN = this.daoIM_NYUUSHUKKIN_KBN.GetDataByCd((int)this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_HOUHOU);
                    if (this.entitysM_NYUUSHUKKIN_KBN != null)
                    {
                        this.form.SHIHARAI_HOUHOU_NAME.Text = this.entitysM_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME_RYAKU;
                    }
                }
                else
                {
                    this.form.SHIHARAI_HOUHOU.Text = "";
                    this.form.SHIHARAI_HOUHOU_NAME.Text = "";
                }
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG && !string.IsNullOrEmpty(this.TorihikisakiCd))
                {
                    this.form.KAISHI_KAIKAKE_ZANDAKA.Text = "0";
                }
                else if (!this.entitysTORIHIKISAKI_SHIHARAI.KAISHI_KAIKAKE_ZANDAKA.IsNull)
                {
                    this.SetZandakaFormat(this.entitysTORIHIKISAKI_SHIHARAI.KAISHI_KAIKAKE_ZANDAKA.ToString(), this.form.KAISHI_KAIKAKE_ZANDAKA);
                }
                Zandaka_K = this.form.KAISHI_KAIKAKE_ZANDAKA.Text;

                if (!this.entitysTORIHIKISAKI_SHIHARAI.SHOSHIKI_KBN.IsNull)
                {
                    this.form.SHIHARAI_SHOSHIKI_KBN.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHOSHIKI_KBN.ToString();
                }
                else
                {
                    this.form.SHIHARAI_SHOSHIKI_KBN.Text = "";
                }
                if (!this.entitysTORIHIKISAKI_SHIHARAI.SHOSHIKI_MEISAI_KBN.IsNull)
                {
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHOSHIKI_MEISAI_KBN.ToString();
                }
                else
                {
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text = "";
                }
                if (!this.entitysTORIHIKISAKI_SHIHARAI.SHOSHIKI_GENBA_KBN.IsNull)
                {
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHOSHIKI_GENBA_KBN.ToString();
                }
                else
                {
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Text = "";
                }
                if (!this.entitysTORIHIKISAKI_SHIHARAI.TAX_HASUU_CD.IsNull)
                {
                    this.form.SHIHARAI_TAX_HASUU_CD.Text = this.entitysTORIHIKISAKI_SHIHARAI.TAX_HASUU_CD.ToString();
                }
                else
                {
                    this.form.SHIHARAI_TAX_HASUU_CD.Text = "";
                }
                if (!this.entitysTORIHIKISAKI_SHIHARAI.KINGAKU_HASUU_CD.IsNull)
                {
                    this.form.SHIHARAI_KINGAKU_HASUU_CD.Text = this.entitysTORIHIKISAKI_SHIHARAI.KINGAKU_HASUU_CD.ToString();
                }
                else
                {
                    this.form.SHIHARAI_KINGAKU_HASUU_CD.Text = "";
                }
                if (!this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_KEITAI_KBN.IsNull)
                {
                    this.form.SHIHARAI_KEITAI_KBN.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_KEITAI_KBN.ToString();
                }
                else
                {
                    this.form.SHIHARAI_KEITAI_KBN.Text = "";
                }
                if (!this.entitysTORIHIKISAKI_SHIHARAI.SHUKKIN_MEISAI_KBN.IsNull)
                {
                    this.form.SHUKKIN_MEISAI_KBN.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHUKKIN_MEISAI_KBN.ToString();
                }
                else
                {
                    this.form.SHUKKIN_MEISAI_KBN.Text = "";
                }
                if (!this.entitysTORIHIKISAKI_SHIHARAI.YOUSHI_KBN.IsNull)
                {
                    this.form.SHIHARAI_YOUSHI_KBN.Text = this.entitysTORIHIKISAKI_SHIHARAI.YOUSHI_KBN.ToString();
                }
                else
                {
                    this.form.SHIHARAI_YOUSHI_KBN.Text = "";
                }

                // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
                if (!this.entitysTORIHIKISAKI_SHIHARAI.INXS_SHIHARAI_KBN.IsNull)
                {
                    this.form.INXS_SHIHARAI_KBN.Text = this.entitysTORIHIKISAKI_SHIHARAI.INXS_SHIHARAI_KBN.ToString();
                }
                else
                {
                    this.form.INXS_SHIHARAI_KBN.Text = string.Empty;
                }
                // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

                if (!this.entitysTORIHIKISAKI_SHIHARAI.ZEI_KEISAN_KBN_CD.IsNull)
                {
                    this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = this.entitysTORIHIKISAKI_SHIHARAI.ZEI_KEISAN_KBN_CD.ToString();
                }
                else
                {
                    this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = "";
                }
                if (!this.entitysTORIHIKISAKI_SHIHARAI.ZEI_KBN_CD.IsNull)
                {
                    this.form.SHIHARAI_ZEI_KBN_CD.Text = this.entitysTORIHIKISAKI_SHIHARAI.ZEI_KBN_CD.ToString();
                }
                else
                {
                    this.form.SHIHARAI_ZEI_KBN_CD.Text = "";
                }
                if (!this.entitysTORIHIKISAKI_SHIHARAI.LAST_TORIHIKI_DATE.IsNull)
                {
                    this.form.LAST_TORIHIKI_DATE_SHIHARAI.Text = string.Format("yyyy/MM/dd HH:mm:ss", this.entitysTORIHIKISAKI_SHIHARAI.LAST_TORIHIKI_DATE);
                }
                this.form.TOUROKU_NO.Text = this.entitysTORIHIKISAKI_SHIHARAI.TOUROKU_NO;

                // 支払情報2タブ
                this.form.SHIHARAI_JOUHOU1.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_JOUHOU1;
                this.form.SHIHARAI_JOUHOU2.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_JOUHOU2;
                this.form.SHIHARAI_SOUFU_NAME1.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_NAME1;
                this.form.SHIHARAI_SOUFU_KEISHOU1.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_KEISHOU1;
                this.form.SHIHARAI_SOUFU_NAME2.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_NAME2;
                this.form.SHIHARAI_SOUFU_KEISHOU2.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_KEISHOU2;
                this.form.SHIHARAI_SOUFU_POST.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_POST;
                this.form.SHIHARAI_SOUFU_ADDRESS1.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_ADDRESS1;
                this.form.SHIHARAI_SOUFU_ADDRESS2.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_ADDRESS2;
                this.form.SHIHARAI_SOUFU_BUSHO.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_BUSHO;
                this.form.SHIHARAI_SOUFU_TANTOU.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_TANTOU;
                this.form.SHIHARAI_SOUFU_TEL.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_TEL;
                this.form.SHIHARAI_SOUFU_FAX.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_FAX;
                this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                if (!this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_PRINT_KBN.ToString();
                }
                //160026 S
                //振込先銀行
                if (!this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_EXPORT_KBN.IsNull)
                {
                    this.form.FURIKOMI_EXPORT_KBN.Text = this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_EXPORT_KBN.Value.ToString();
                }
                this.form.FURIKOMI_EXPORT_KBN_TextChanged(null, null);
                this.form.FURIKOMI_SAKI_BANK_CD.Text = this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_CD;
                this.form.FURIKOMI_SAKI_BANK_NAME.Text = this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_NAME;
                this.form.FURIKOMI_SAKI_BANK_SHITEN_CD.Text = this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_SHITEN_CD;
                this.form.FURIKOMI_SAKI_BANK_SHITEN_NAME.Text = this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_SHITEN_NAME;
                if (!this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.IsNull)
                {
                    this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Text = this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Value.ToString();
                    this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI.Text = this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_SHURUI;
                }
                this.form.FURIKOMI_SAKI_BANK_KOUZA_NO.Text = this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_NO;
                this.form.FURIKOMI_SAKI_BANK_KOUZA_NAME.Text = this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_NAME;
                if (!this.entitysTORIHIKISAKI_SHIHARAI.TEI_SUU_RYOU_KBN.IsNull)
                {
                    this.form.TEI_SUU_RYOU_KBN.Text = this.entitysTORIHIKISAKI_SHIHARAI.TEI_SUU_RYOU_KBN.Value.ToString();
                }
                //振込元銀行                
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_CD))
                {
                    this.form.FURI_KOMI_MOTO_BANK_CD.Text = this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_CD;
                    this.entitysM_BANK = this.daoIM_BANK.GetDataByCd(this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_CD);
                    if (this.entitysM_BANK != null)
                    {
                        this.form.FURI_KOMI_MOTO_BANK_NAME.Text = this.entitysM_BANK.BANK_NAME_RYAKU;
                    }
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_SHITTEN_CD))
                {
                    this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text = this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_SHITTEN_CD;
                    this.form.FURI_KOMI_MOTO_SHURUI.Text = this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_SHURUI;
                    this.form.FURI_KOMI_MOTO_NO.Text = this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_NO;
                }
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_CD) && !string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_SHITTEN_CD))
                {
                    M_BANK_SHITEN bankShiten = new M_BANK_SHITEN();
                    bankShiten.BANK_CD = this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_CD;
                    bankShiten.BANK_SHITEN_CD = this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_SHITTEN_CD;
                    bankShiten.KOUZA_SHURUI = this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_SHURUI;
                    bankShiten.KOUZA_NO = this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_NO;
                    this.entitysM_BANK_SHITEN = this.daoIM_BANK_SHITEN.GetDataByCd(bankShiten);
                    if (this.entitysM_BANK_SHITEN != null)
                    {
                        this.form.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = this.entitysM_BANK_SHITEN.BANK_SHIETN_NAME_RYAKU;
                        this.form.FURI_KOMI_MOTO_NAME.Text = this.entitysM_BANK_SHITEN.KOUZA_NAME;
                    }
                }
                //160026 E
                catchErr = this.ChangeShiharaiKyotenPrintKbn();
                if (catchErr)
                {
                    throw new Exception("");
                }
                this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                if (!this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_CD.IsNull)
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_CD.ToString()));
                    M_KYOTEN kyo = this.daoIM_KYOTEN.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                    if (kyo != null)
                    {
                        this.form.SHIHARAI_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                    }
                }
            }

            // 取引先分類タブ
            this.form.MANI_HENSOUSAKI_KBN.Checked = (bool)this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KBN;
            if (this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.IsNull)
            {
                this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "";
            }
            else
            {
                this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = Convert.ToString(this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Value);
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
                this.form.MANI_HENSOUSAKI_NAME1.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_NAME1;
                this.form.MANI_HENSOUSAKI_NAME2.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_NAME2;
                this.form.MANI_HENSOUSAKI_KEISHOU1.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU1;
                this.form.MANI_HENSOUSAKI_KEISHOU2.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU2;
                this.form.MANI_HENSOUSAKI_POST.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_POST;
                this.form.MANI_HENSOUSAKI_ADDRESS1.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_ADDRESS1;
                this.form.MANI_HENSOUSAKI_ADDRESS2.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_ADDRESS2;
                this.form.MANI_HENSOUSAKI_BUSHO.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_BUSHO;
                this.form.MANI_HENSOUSAKI_TANTOU.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_TANTOU;
            }

            // mapbox項目
            this.form.TorihikisakiLatitude.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_LATITUDE;
            this.form.TorihikisakiLongitude.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_LONGITUDE;
            this.form.LocationInfoUpdateName.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_LOCATION_INFO_UPDATE_NAME;
            if (!this.entitysTORIHIKISAKI.TORIHIKISAKI_LOCATION_INFO_UPDATE_DATE.IsNull)
            {
                this.form.LocationInfoUpdateDate.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_LOCATION_INFO_UPDATE_DATE.ToString();
            }


            this.rowCntGyousha = this.SearchGyousha();

            if (this.rowCntGyousha > 0)
            {
                this.SetIchiranGyousha();
            }
        }

        /// <summary>
        /// 全コントロール制御
        /// </summary>
        /// <param name="isBool">true:操作不可、false:操作可</param>
        private void AllControlLock(bool isBool)
        {
            // 共通項目
            this.form.NYUUKINSAKI_KBN.ReadOnly = isBool;
            this.form.NYUUKINSAKI_KBN_1.Enabled = !isBool;
            this.form.NYUUKINSAKI_KBN_2.Enabled = !isBool;
            this.form.TORIHIKISAKI_KYOTEN_CD.Enabled = !isBool;
            this.form.KYOTEN_SEARCH_BUTTON.Enabled = !isBool;
            this.form.TORIHIKISAKI_CD.Enabled = !isBool;
            this.form.SAIBAN_BUTTON.Enabled = !isBool;
            this.form.TORIHIKISAKI_FURIGANA.ReadOnly = isBool;
            this.form.TORIHIKISAKI_NAME1.ReadOnly = isBool;
            this.form.TORIHIKISAKI_KEISHOU1.Enabled = !isBool;
            this.form.TORIHIKISAKI_NAME2.ReadOnly = isBool;
            this.form.TORIHIKISAKI_KEISHOU2.Enabled = !isBool;
            this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = isBool;
            this.form.TORIHIKISAKI_TEL.ReadOnly = isBool;
            this.form.TORIHIKISAKI_FAX.ReadOnly = isBool;
            this.form.EIGYOU_TANTOU_BUSHO_CD.Enabled = !isBool;
            this.form.ENGYOU_TANTOUBUSHO_SEARCH_BUTTON.Enabled = !isBool;
            this.form.EIGYOU_TANTOU_CD.Enabled = !isBool;
            this.form.EIGYOU_TANTOU_SEARCH_BUTTON.Enabled = !isBool;
            this.form.TEKIYOU_BEGIN.Enabled = !isBool;
            this.form.TEKIYOU_END.Enabled = !isBool;
            this.form.CHUUSHI_RIYUU1.ReadOnly = isBool;
            this.form.CHUUSHI_RIYUU2.ReadOnly = isBool;

            //基本情報タブ
            this.form.TORIHIKISAKI_POST.Enabled = !isBool;
            this.form.TORIHIKISAKI_ADDRESS_SEARCH.Enabled = !isBool;
            this.form.TORIHIKISAKI_TODOUFUKEN_CD.Enabled = !isBool;
            this.form.TORIHIKISAKI_POST_SERACH.Enabled = !isBool;
            this.form.TORIHIKISAKI_ADDRESS1.ReadOnly = isBool;
            this.form.TORIHIKISAKI_ADDRESS2.ReadOnly = isBool;
            this.form.BUSHO.ReadOnly = isBool;
            this.form.TANTOUSHA.ReadOnly = isBool;
            this.form.SHUUKEI_ITEM_CD.Enabled = !isBool;
            this.form.SHUUKEI_KOUMOKU_SEARCH.Enabled = !isBool;
            this.form.GYOUSHU_CD.Enabled = !isBool;
            this.form.GYOUSHU_SEARCH.Enabled = !isBool;
            this.form.BIKOU1.ReadOnly = isBool;
            this.form.BIKOU2.ReadOnly = isBool;
            this.form.BIKOU3.ReadOnly = isBool;
            this.form.BIKOU4.ReadOnly = isBool;
            this.form.SHOKUCHI_KBN.Enabled = !isBool;
            this.form.TODOUFUKEN_SEARCH_BUTTON.Enabled = !isBool;
            this.form.DAIHYOU_PRINT_KBN.ReadOnly = isBool;
            this.form.DAIHYOU_PRINT_KBN_1.Enabled = !isBool;
            this.form.DAIHYOU_PRINT_KBN_2.Enabled = !isBool;

            //請求情報1タブ
            this.form.TORIHIKI_KBN.ReadOnly = isBool;
            this.form.TORIHIKI_KBN_1.Enabled = !isBool;
            this.form.TORIHIKI_KBN_2.Enabled = !isBool;
            this.form.SHIMEBI1.Enabled = !isBool;
            this.form.SHIMEBI2.Enabled = !isBool;
            this.form.SHIMEBI3.Enabled = !isBool;
            this.form.HICCHAKUBI.ReadOnly = isBool;
            this.form.KAISHUU_MONTH.ReadOnly = isBool;
            this.form.KAISHUU_MONTH_1.Enabled = !isBool;
            this.form.KAISHUU_MONTH_2.Enabled = !isBool;
            this.form.KAISHUU_MONTH_3.Enabled = !isBool;
            this.form.KAISHUU_MONTH_4.Enabled = !isBool;
            this.form.KAISHUU_MONTH_5.Enabled = !isBool;
            this.form.KAISHUU_MONTH_6.Enabled = !isBool;
            this.form.KAISHUU_MONTH_7.Enabled = !isBool;
            this.form.KAISHUU_DAY.ReadOnly = isBool;
            this.form.KAISHUU_HOUHOU.Enabled = !isBool;
            this.form.KAISHUU_HOUHOU_SEARCH.Enabled = !isBool;
            this.form.KAISHI_URIKAKE_ZANDAKA.ReadOnly = isBool;
            //160026 S
            this.form.KAISHUU_BETSU_KBN.ReadOnly = isBool;
            this.form.KAISHUU_BETSU_KBN_1.Enabled = !isBool;
            this.form.KAISHUU_BETSU_KBN_2.Enabled = !isBool;
            this.form.KAISHUU_BETSU_NICHIGO.Enabled = !isBool;
            //160026 E

            this.form.SHOSHIKI_KBN.ReadOnly = isBool;
            this.form.SHOSHIKI_KBN_1.Enabled = !isBool;
            this.form.SHOSHIKI_KBN_2.Enabled = !isBool;
            this.form.SHOSHIKI_KBN_3.Enabled = !isBool;
            this.form.SHOSHIKI_MEISAI_KBN.ReadOnly = isBool;
            this.form.SHOSHIKI_MEISAI_KBN_1.Enabled = !isBool;
            this.form.SHOSHIKI_MEISAI_KBN_2.Enabled = !isBool;
            this.form.SHOSHIKI_MEISAI_KBN_3.Enabled = !isBool;
            this.form.SHOSHIKI_GENBA_KBN.ReadOnly = isBool;
            this.form.SHOSHIKI_GENBA_KBN_1.Enabled = !isBool;
            this.form.SHOSHIKI_GENBA_KBN_2.Enabled = !isBool;
            this.form.TAX_HASUU_CD.ReadOnly = isBool;
            this.form.TAX_HASUU_CD_1.Enabled = !isBool;
            this.form.TAX_HASUU_CD_2.Enabled = !isBool;
            this.form.TAX_HASUU_CD_3.Enabled = !isBool;
            this.form.KINGAKU_HASUU_CD.ReadOnly = isBool;
            this.form.KINGAKU_HASUU_CD_1.Enabled = !isBool;
            this.form.KINGAKU_HASUU_CD_2.Enabled = !isBool;
            this.form.KINGAKU_HASUU_CD_3.Enabled = !isBool;
            this.form.SEIKYUU_KEITAI_KBN.ReadOnly = isBool;
            this.form.SEIKYUU_KEITAI_KBN_1.Enabled = !isBool;
            this.form.SEIKYUU_KEITAI_KBN_2.Enabled = !isBool;
            this.form.NYUUKIN_MEISAI_KBN.ReadOnly = isBool;
            this.form.NYUUKIN_MEISAI_KBN_1.Enabled = !isBool;
            this.form.NYUUKIN_MEISAI_KBN_2.Enabled = !isBool;
            this.form.YOUSHI_KBN.ReadOnly = isBool;
            this.form.YOUSHI_KBN_1.Enabled = !isBool;
            this.form.YOUSHI_KBN_2.Enabled = !isBool;
            this.form.YOUSHI_KBN_3.Enabled = !isBool;

            //Begin: LANDUONG - 20220209 - refs#160050        
            this.form.OUTPUT_KBN.ReadOnly = isBool;
            this.form.OUTPUT_KBN_1.Enabled = !isBool;
            this.form.OUTPUT_KBN_2.Enabled = !isBool;
            this.form.OUTPUT_KBN_3.Enabled = !isBool;
            this.form.HAKKOUSAKI_CD.ReadOnly = isBool;
            this.form.RAKURAKU_CUSTOMER_CD.ReadOnly = isBool;
            this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = !isBool;
            //End: LANDUONG - 20220209 - refs#160050

            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
            if (SystemProperty.Shain.InxsTantouFlg)
            {
                this.form.INXS_SEIKYUU_KBN.ReadOnly = isBool;
                this.form.INXS_SEIKYUU_KBN_1.Enabled = !isBool;
                this.form.INXS_SEIKYUU_KBN_2.Enabled = !isBool;
            }
            else
            {
                this.form.INXS_SEIKYUU_KBN.ReadOnly = false;
                this.form.INXS_SEIKYUU_KBN_1.Enabled = false;
                this.form.INXS_SEIKYUU_KBN_2.Enabled = false;
            }
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
            if (SystemProperty.Shain.InxsTantouFlg)
            {
                this.form.INXS_SHIHARAI_KBN.ReadOnly = isBool;
                this.form.INXS_SHIHARAI_KBN_1.Enabled = !isBool;
                this.form.INXS_SHIHARAI_KBN_2.Enabled = !isBool;
            }
            else
            {
                this.form.INXS_SHIHARAI_KBN.ReadOnly = false;
                this.form.INXS_SHIHARAI_KBN_1.Enabled = false;
                this.form.INXS_SHIHARAI_KBN_2.Enabled = false;
            }
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

            this.form.ZEI_KEISAN_KBN_CD.ReadOnly = isBool;
            this.form.ZEI_KEISAN_KBN_CD_1.Enabled = !isBool;
            this.form.ZEI_KEISAN_KBN_CD_2.Enabled = !isBool;
            this.form.ZEI_KEISAN_KBN_CD_3.Enabled = !isBool;
            this.form.ZEI_KBN_CD.ReadOnly = isBool;
            this.form.ZEI_KBN_CD_1.Enabled = !isBool;
            this.form.ZEI_KBN_CD_2.Enabled = !isBool;
            this.form.ZEI_KBN_CD_3.Enabled = !isBool;

            // 請求情報2タブ
            this.form.SEIKYUU_JOUHOU1.ReadOnly = isBool;
            this.form.SEIKYUU_JOUHOU2.ReadOnly = isBool;
            this.form.SEIKYUU_SOUFU_NAME1.ReadOnly = isBool;
            this.form.SEIKYUU_SOUFU_KEISHOU1.Enabled = !isBool;
            this.form.SEIKYUU_SOUFU_NAME2.ReadOnly = isBool;
            this.form.SEIKYUU_SOUFU_KEISHOU2.Enabled = !isBool;
            this.form.SEIKYUU_SOUFU_POST.Enabled = !isBool;
            this.form.SEIKYUU_JUSHO_SEARCH.Enabled = !isBool;
            this.form.SEIKYUU_POST_SEARCH.Enabled = !isBool;
            this.form.SEIKYUU_SOUFU_ADDRESS1.ReadOnly = isBool;
            this.form.SEIKYUU_SOUFU_ADDRESS2.ReadOnly = isBool;
            this.form.SEIKYUU_SOUFU_BUSHO.ReadOnly = isBool;
            this.form.SEIKYUU_SOUFU_TANTOU.ReadOnly = isBool;
            this.form.SEIKYUU_SOUFU_TEL.ReadOnly = isBool;
            this.form.SEIKYUU_SOUFU_FAX.ReadOnly = isBool;
            this.form.NYUUKINSAKI_CD.Enabled = !isBool;
            this.form.NYUUKINSAKI_SEARCH.Enabled = !isBool;
            this.form.SEIKYUU_TANTOU.ReadOnly = isBool;
            this.form.SEIKYUU_DAIHYOU_PRINT_KBN.ReadOnly = isBool;
            this.form.SEIKYUU_DAIHYOU_PRINT_KBN_1.Enabled = !isBool;
            this.form.SEIKYUU_DAIHYOU_PRINT_KBN_2.Enabled = !isBool;
            this.form.TORIHIKISAKI_COPY_SEIKYU_BUTTON.Enabled = !isBool;
            this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled = !isBool;
            this.form.SEIKYUU_KYOTEN_PRINT_KBN_1.Enabled = !isBool;
            this.form.SEIKYUU_KYOTEN_PRINT_KBN_2.Enabled = !isBool;
            this.form.SEIKYUU_KYOTEN_CD.Enabled = !isBool;
            this.form.SEIKYUU_KYOTEN_CD_SEARCH.Enabled = !isBool;
            this.form.FURIKOMI_NAME1.ReadOnly = isBool;
            this.form.FURIKOMI_NAME2.ReadOnly = isBool;

            // 振込銀行タブ
            this.form.FURIKOMI_BANK_CD.Enabled = !isBool;
            this.form.FURIKOMI_BANK_SEARCH.Enabled = !isBool;
            this.form.FURIKOMI_BANK_SHITEN_CD.Enabled = !isBool;
            this.form.FURIKOMI_BANK_SHITEN_SEARCH.Enabled = !isBool;
            this.form.FURIKOMI_BANK_CD_2.Enabled = !isBool;
            this.form.FURIKOMI_BANK_SEARCH_2.Enabled = !isBool;
            this.form.FURIKOMI_BANK_SHITEN_CD_2.Enabled = !isBool;
            this.form.FURIKOMI_BANK_SHITEN_SEARCH_2.Enabled = !isBool;
            this.form.FURIKOMI_BANK_CD_3.Enabled = !isBool;
            this.form.FURIKOMI_BANK_SEARCH_3.Enabled = !isBool;
            this.form.FURIKOMI_BANK_SHITEN_CD_3.Enabled = !isBool;
            this.form.FURIKOMI_BANK_SHITEN_SEARCH_3.Enabled = !isBool;

            //支払情報1タブ
            this.form.SHIHARAI_TORIHIKI_KBN_CD.ReadOnly = isBool;
            this.form.SHIHARAI_TORIHIKI_KBN_CD_1.Enabled = !isBool;
            this.form.SHIHARAI_TORIHIKI_KBN_CD_2.Enabled = !isBool;
            this.form.SHIHARAI_SHIMEBI1.Enabled = !isBool;
            this.form.SHIHARAI_SHIMEBI2.Enabled = !isBool;
            this.form.SHIHARAI_SHIMEBI3.Enabled = !isBool;
            //160026 S
            this.form.SHIHARAI_BETSU_KBN.ReadOnly = isBool;
            this.form.SHIHARAI_BETSU_KBN_1.Enabled = !isBool;
            this.form.SHIHARAI_BETSU_KBN_2.Enabled = !isBool;
            this.form.SHIHARAI_BETSU_NICHIGO.Enabled = !isBool;
            //160026 E
            this.form.SHIHARAI_MONTH.ReadOnly = isBool;
            this.form.SHIHARAI_MONTH_1.Enabled = !isBool;
            this.form.SHIHARAI_MONTH_2.Enabled = !isBool;
            this.form.SHIHARAI_MONTH_3.Enabled = !isBool;
            this.form.SHIHARAI_MONTH_4.Enabled = !isBool;
            this.form.SHIHARAI_MONTH_5.Enabled = !isBool;
            this.form.SHIHARAI_MONTH_6.Enabled = !isBool;
            this.form.SHIHARAI_MONTH_7.Enabled = !isBool;
            this.form.SHIHARAI_DAY.ReadOnly = isBool;
            this.form.SHIHARAI_HOUHOU.Enabled = !isBool;
            this.form.SHIHARAI_HOUHOU_SEARCH.Enabled = !isBool;
            this.form.KAISHI_KAIKAKE_ZANDAKA.ReadOnly = isBool;
            this.form.SHIHARAI_SHOSHIKI_KBN.ReadOnly = isBool;
            this.form.SHIHARAI_SHOSHIKI_KBN_1.Enabled = !isBool;
            this.form.SHIHARAI_SHOSHIKI_KBN_2.Enabled = !isBool;
            this.form.SHIHARAI_SHOSHIKI_KBN_3.Enabled = !isBool;
            this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.ReadOnly = isBool;
            this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_1.Enabled = !isBool;
            this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_2.Enabled = !isBool;
            this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_3.Enabled = !isBool;
            this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.ReadOnly = isBool;
            this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_1.Enabled = !isBool;
            this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_2.Enabled = !isBool;
            this.form.SHIHARAI_TAX_HASUU_CD.ReadOnly = isBool;
            this.form.SHIHARAI_TAX_HASUU_CD_1.Enabled = !isBool;
            this.form.SHIHARAI_TAX_HASUU_CD_2.Enabled = !isBool;
            this.form.SHIHARAI_TAX_HASUU_CD_3.Enabled = !isBool;
            this.form.SHIHARAI_KINGAKU_HASUU_CD.ReadOnly = isBool;
            this.form.SHIHARAI_KINGAKU_HASUU_CD_1.Enabled = !isBool;
            this.form.SHIHARAI_KINGAKU_HASUU_CD_2.Enabled = !isBool;
            this.form.SHIHARAI_KINGAKU_HASUU_CD_3.Enabled = !isBool;
            this.form.SHIHARAI_KEITAI_KBN.ReadOnly = isBool;
            this.form.SHIHARAI_KEITAI_KBN_1.Enabled = !isBool;
            this.form.SHIHARAI_KEITAI_KBN_2.Enabled = !isBool;
            this.form.SHUKKIN_MEISAI_KBN.ReadOnly = isBool;
            this.form.SHUKKIN_MEISAI_KBN_1.Enabled = !isBool;
            this.form.SHUKKIN_MEISAI_KBN_2.Enabled = !isBool;
            this.form.SHIHARAI_YOUSHI_KBN.ReadOnly = isBool;
            this.form.SHIHARAI_YOUSHI_KBN_1.Enabled = !isBool;
            this.form.SHIHARAI_YOUSHI_KBN_2.Enabled = !isBool;
            this.form.SHIHARAI_YOUSHI_KBN_3.Enabled = !isBool;
            this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.ReadOnly = isBool;
            this.form.SHIHARAI_ZEI_KEISAN_KBN_CD_1.Enabled = !isBool;
            this.form.SHIHARAI_ZEI_KEISAN_KBN_CD_2.Enabled = !isBool;
            this.form.SHIHARAI_ZEI_KEISAN_KBN_CD_3.Enabled = !isBool;
            this.form.SHIHARAI_ZEI_KBN_CD.ReadOnly = isBool;
            this.form.SHIHARAI_ZEI_KBN_CD_1.Enabled = !isBool;
            this.form.SHIHARAI_ZEI_KBN_CD_2.Enabled = !isBool;
            this.form.SHIHARAI_ZEI_KBN_CD_3.Enabled = !isBool;
            this.form.TOUROKU_NO.Enabled = !isBool;

            // 支払情報2タブ
            this.form.SHIHARAI_JOUHOU1.ReadOnly = isBool;
            this.form.SHIHARAI_JOUHOU2.ReadOnly = isBool;
            this.form.SHIHARAI_SOUFU_NAME1.ReadOnly = isBool;
            this.form.SHIHARAI_SOUFU_KEISHOU1.Enabled = !isBool;
            this.form.SHIHARAI_SOUFU_NAME2.ReadOnly = isBool;
            this.form.SHIHARAI_SOUFU_KEISHOU2.Enabled = !isBool;
            this.form.SHIHARAI_SOUFU_POST.Enabled = !isBool;
            this.form.SHIHARAI_SOUFU_ADDRESS_SEARCH.Enabled = !isBool;
            this.form.SHIHARAI_SOUFU_POST_SEARCH.Enabled = !isBool;
            this.form.SHIHARAI_SOUFU_ADDRESS1.ReadOnly = isBool;
            this.form.SHIHARAI_SOUFU_ADDRESS2.ReadOnly = isBool;
            this.form.SHIHARAI_SOUFU_BUSHO.ReadOnly = isBool;
            this.form.SHIHARAI_SOUFU_TANTOU.ReadOnly = isBool;
            this.form.SHIHARAI_SOUFU_TEL.ReadOnly = isBool;
            this.form.SHIHARAI_SOUFU_FAX.ReadOnly = isBool;
            this.form.TORIHIKISAKI_COPY_SIHARAI_BUTTON.Enabled = !isBool;
            this.form.TORIHIKISAKI_COPY_MANI_BUTTON.Enabled = !isBool;
            this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled = !isBool;
            this.form.SHIHARAI_KYOTEN_PRINT_KBN_1.Enabled = !isBool;
            this.form.SHIHARAI_KYOTEN_PRINT_KBN_2.Enabled = !isBool;
            this.form.SHIHARAI_KYOTEN_CD.Enabled = !isBool;
            this.form.SHIHARAI_KYOTEN_CD_SEARCH.Enabled = !isBool;
            //160026 S
            this.form.FURIKOMI_EXPORT_KBN.Enabled = !isBool;
            this.form.FURIKOMI_EXPORT_KBN_1.Enabled = !isBool;
            this.form.FURIKOMI_EXPORT_KBN_2.Enabled = !isBool;
            this.form.FURIKOMI_SAKI_BANK_CD.ReadOnly = isBool;
            this.form.FURIKOMI_SAKI_BANK_NAME.ReadOnly = isBool;
            this.form.FURIKOMI_SAKI_BANK_SHITEN_CD.ReadOnly = isBool;
            this.form.FURIKOMI_SAKI_BANK_SHITEN_NAME.ReadOnly = isBool;
            this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.ReadOnly = isBool;
            this.form.FURIKOMI_SAKI_BANK_KOUZA_NO.ReadOnly = isBool;
            this.form.FURIKOMI_SAKI_BANK_KOUZA_NAME.ReadOnly = isBool;
            this.form.TEI_SUU_RYOU_KBN.Enabled = !isBool;
            this.form.TEI_SUU_RYOU_KBN_1.Enabled = !isBool;
            this.form.TEI_SUU_RYOU_KBN_2.Enabled = !isBool;
            this.form.FURI_KOMI_MOTO_BANK_CD.ReadOnly = isBool;
            this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.ReadOnly = isBool;
            this.form.FURI_KOMI_MOTO_BANK_POPUP.Enabled = !isBool;
            this.form.FURI_KOMI_MOTO_BANK_SHITTEN_POPUP.Enabled = !isBool;
            //160026 E
            // 取引先分類タブ
            this.form.MANI_HENSOUSAKI_KBN.Enabled = !isBool;
            this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Enabled = !isBool;
            this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Enabled = !isBool;
            this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Enabled = !isBool;
            this.form.MANI_HENSOUSAKI_NAME1.ReadOnly = isBool;
            this.form.MANI_HENSOUSAKI_NAME2.ReadOnly = isBool;
            this.form.MANI_HENSOUSAKI_KEISHOU1.Enabled = !isBool;
            this.form.MANI_HENSOUSAKI_KEISHOU2.Enabled = !isBool;
            this.form.MANI_HENSOUSAKI_POST.ReadOnly = isBool;
            this.form.MANI_HENSOUSAKI_ADDRESS_SEARCH.Enabled = !isBool;
            this.form.MANI_HENSOUSAKI_POST_SEARCH.Enabled = !isBool;
            this.form.MANI_HENSOUSAKI_ADDRESS1.ReadOnly = isBool;
            this.form.MANI_HENSOUSAKI_ADDRESS2.ReadOnly = isBool;
            this.form.MANI_HENSOUSAKI_BUSHO.ReadOnly = isBool;
            this.form.MANI_HENSOUSAKI_TANTOU.ReadOnly = isBool;

            // 地図連携情報タブ
            this.form.TorihikisakiLatitude.Enabled = !isBool;
            this.form.TorihikisakiLongitude.Enabled = !isBool;
            this.form.bt_map_open.Enabled = !isBool;
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            int count = 1;

            //count = table.Rows.Count;

            return count;
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete)
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 最新のSYS_INFOを取得
                M_SYS_INFO[] sysInfo = this.daoIM_SYS_INFO.GetAllData();
                if (sysInfo != null && sysInfo.Length > 0)
                {
                    this.entitysM_SYS_INFO = sysInfo[0];
                }
                else
                {
                    this.entitysM_SYS_INFO = null;
                }

                // 取引先マスタ
                // 20151014 委託契約書入力(M001)システムエラー発生関連チケット Start
                //          TIME_STAMP設定部分修正方法更新
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG ||
                    this.entitysTORIHIKISAKI == null ||
                    this.entitysTORIHIKISAKI.TIME_STAMP == null || this.entitysTORIHIKISAKI.TIME_STAMP.Length != 8)
                {
                    this.entitysTORIHIKISAKI = new M_TORIHIKISAKI();
                }
                else
                {
                    this.entitysTORIHIKISAKI = new M_TORIHIKISAKI() { TIME_STAMP = this.entitysTORIHIKISAKI.TIME_STAMP };
                }
                // 20151014 委託契約書入力(M001)システムエラー発生関連チケット End
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_CD);
                this.form.TORIHIKISAKI_KYOTEN_CD.Text = "99"; // 強制的に99:全社を登録
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_KYOTEN_CD);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_NAME1);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_NAME2);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_NAME_RYAKU);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_FURIGANA);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_TEL);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_FAX);
                this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU1 = this.form.TORIHIKISAKI_KEISHOU1.Text;
                this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU2 = this.form.TORIHIKISAKI_KEISHOU2.Text;
                this.entitysTORIHIKISAKI.EIGYOU_TANTOU_BUSHO_CD = this.form.EIGYOU_TANTOU_BUSHO_CD.Text;
                this.entitysTORIHIKISAKI.SetValue(this.form.EIGYOU_TANTOU_CD);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_POST);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_TODOUFUKEN_CD);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_ADDRESS1);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_ADDRESS2);
                this.entitysTORIHIKISAKI.SetValue(this.form.CHUUSHI_RIYUU1);
                this.entitysTORIHIKISAKI.SetValue(this.form.CHUUSHI_RIYUU2);
                this.entitysTORIHIKISAKI.SetValue(this.form.BUSHO);
                this.entitysTORIHIKISAKI.SetValue(this.form.TANTOUSHA);
                this.entitysTORIHIKISAKI.SetValue(this.form.SHUUKEI_ITEM_CD);
                this.entitysTORIHIKISAKI.SetValue(this.form.GYOUSHU_CD);
                this.entitysTORIHIKISAKI.SetValue(this.form.DAIHYOU_PRINT_KBN);
                this.entitysTORIHIKISAKI.SetValue(this.form.BIKOU1);
                this.entitysTORIHIKISAKI.SetValue(this.form.BIKOU2);
                this.entitysTORIHIKISAKI.SetValue(this.form.BIKOU3);
                this.entitysTORIHIKISAKI.SetValue(this.form.BIKOU4);
                this.entitysTORIHIKISAKI.SetValue(this.form.NYUUKINSAKI_KBN);
                this.entitysTORIHIKISAKI.SetValue(this.form.MANI_HENSOUSAKI_KBN);
                this.entitysTORIHIKISAKI.SetValue(this.form.SHOKUCHI_KBN);
                this.entitysTORIHIKISAKI.SetValue(this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN);
                if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked)
                {
                    this.entitysTORIHIKISAKI.SetValue(this.form.MANI_HENSOUSAKI_NAME1);
                    this.entitysTORIHIKISAKI.SetValue(this.form.MANI_HENSOUSAKI_NAME2);
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU1 = this.form.MANI_HENSOUSAKI_KEISHOU1.Text;
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU2 = this.form.MANI_HENSOUSAKI_KEISHOU2.Text;
                    this.entitysTORIHIKISAKI.SetValue(this.form.MANI_HENSOUSAKI_POST);
                    this.entitysTORIHIKISAKI.SetValue(this.form.MANI_HENSOUSAKI_ADDRESS1);
                    this.entitysTORIHIKISAKI.SetValue(this.form.MANI_HENSOUSAKI_ADDRESS2);
                    this.entitysTORIHIKISAKI.SetValue(this.form.MANI_HENSOUSAKI_BUSHO);
                    this.entitysTORIHIKISAKI.SetValue(this.form.MANI_HENSOUSAKI_TANTOU);
                }
                else
                {
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_NAME1 = this.form.TORIHIKISAKI_NAME1.Text;
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_NAME2 = this.form.TORIHIKISAKI_NAME2.Text;
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU1 = this.form.TORIHIKISAKI_KEISHOU1.Text;
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU2 = this.form.TORIHIKISAKI_KEISHOU2.Text;
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_POST = this.form.TORIHIKISAKI_POST.Text;
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_ADDRESS1 = this.form.TORIHIKISAKI_TODOUFUKEN_NAME.Text + this.form.TORIHIKISAKI_ADDRESS1.Text;
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_ADDRESS2 = this.form.TORIHIKISAKI_ADDRESS2.Text;
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_BUSHO = this.form.BUSHO.Text;
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_TANTOU = this.form.TANTOUSHA.Text;
                }
                // VUNGUYEN 20150525 #1294 START
                DateTime dttemp;
                if (DateTime.TryParse(this.form.TEKIYOU_BEGIN.Text, out dttemp))
                {
                    this.entitysTORIHIKISAKI.TEKIYOU_BEGIN = dttemp;
                }
                else
                {
                    this.entitysTORIHIKISAKI.TEKIYOU_BEGIN = SqlDateTime.Null;
                }
                if (DateTime.TryParse(this.form.TEKIYOU_END.Text, out dttemp))
                {
                    this.entitysTORIHIKISAKI.TEKIYOU_END = dttemp;
                }
                else
                {
                    this.entitysTORIHIKISAKI.TEKIYOU_END = SqlDateTime.Null;
                }

                // 取引先 請求情報マスタ
                // 20151014 委託契約書入力(M001)システムエラー発生関連チケット Start
                //          TIME_STAMP設定部分修正方法更新
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG ||
                    this.entitysTORIHIKISAKI_SEIKYUU == null ||
                    this.entitysTORIHIKISAKI_SEIKYUU.TIME_STAMP == null || this.entitysTORIHIKISAKI_SEIKYUU.TIME_STAMP.Length != 8)
                {
                    this.entitysTORIHIKISAKI_SEIKYUU = new M_TORIHIKISAKI_SEIKYUU();
                }
                else
                {
                    this.entitysTORIHIKISAKI_SEIKYUU = new M_TORIHIKISAKI_SEIKYUU() { TIME_STAMP = this.entitysTORIHIKISAKI_SEIKYUU.TIME_STAMP };
                }
                // 20151014 委託契約書入力(M001)システムエラー発生関連チケット End

                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.TORIHIKISAKI_CD);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.TORIHIKI_KBN);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SHIMEBI1);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SHIMEBI2);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SHIMEBI3);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.HICCHAKUBI);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KAISHUU_MONTH);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KAISHUU_DAY);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KAISHUU_HOUHOU);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_JOUHOU1);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_JOUHOU2);
                //160026 S
                if (!string.IsNullOrWhiteSpace(this.form.KAISHUU_BETSU_KBN.Text))
                {
                    this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_BETSU_KBN = Convert.ToInt16(this.form.KAISHUU_BETSU_KBN.Text);
                }
                if (!string.IsNullOrWhiteSpace(this.form.KAISHUU_BETSU_NICHIGO.Text))
                {
                    this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_BETSU_NICHIGO = Convert.ToInt16(this.form.KAISHUU_BETSU_NICHIGO.Text);
                }
                //160026 E

                if (string.IsNullOrWhiteSpace(this.form.KAISHI_URIKAKE_ZANDAKA.Text))
                {
                    this.entitysTORIHIKISAKI_SEIKYUU.KAISHI_URIKAKE_ZANDAKA = 0;
                }
                else
                {
                    this.entitysTORIHIKISAKI_SEIKYUU.KAISHI_URIKAKE_ZANDAKA = Decimal.Parse(this.form.KAISHI_URIKAKE_ZANDAKA.Text, NumberStyles.AllowThousands | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowLeadingSign);
                }
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SHOSHIKI_KBN);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SHOSHIKI_MEISAI_KBN);
                if (this.form.SHOSHIKI_GENBA_KBN.Enabled)
                {
                    this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SHOSHIKI_GENBA_KBN);
                }
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_KEITAI_KBN);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.NYUUKIN_MEISAI_KBN);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.YOUSHI_KBN);
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.OUTPUT_KBN);
                // 20160429 koukoukon v2.1_電子請求書 #16612 end

                //Begin: LANDUONG - 20220209 - refs#160050
                if(this.form.TORIHIKI_KBN.Text.Equals("2"))
                {
                    if (this.form.OUTPUT_KBN.Text.Equals("3"))
                    {
                        this.entitysTORIHIKISAKI_SEIKYUU.HAKKOUSAKI_CD = string.Empty;
                        this.entitysTORIHIKISAKI_SEIKYUU.RAKURAKU_CUSTOMER_CD = this.form.RAKURAKU_CUSTOMER_CD.Text;
                    }
                    else if (this.form.OUTPUT_KBN.Text.Equals("2"))
                    {
                        this.entitysTORIHIKISAKI_SEIKYUU.HAKKOUSAKI_CD = this.form.HAKKOUSAKI_CD.Text;
                        this.entitysTORIHIKISAKI_SEIKYUU.RAKURAKU_CUSTOMER_CD = string.Empty;
                    }
                    else
                    {
                        this.entitysTORIHIKISAKI_SEIKYUU.HAKKOUSAKI_CD = string.Empty;
                        this.entitysTORIHIKISAKI_SEIKYUU.RAKURAKU_CUSTOMER_CD = string.Empty;
                    }
                }
                else
                {
                    this.entitysTORIHIKISAKI_SEIKYUU.HAKKOUSAKI_CD = string.Empty;
                    this.entitysTORIHIKISAKI_SEIKYUU.RAKURAKU_CUSTOMER_CD = string.Empty;
                    this.entitysTORIHIKISAKI_SEIKYUU.OUTPUT_KBN = 0;
                }
                //End: LANDUONG - 20220209 - refs#160050

                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.INXS_SEIKYUU_KBN);
                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.ZEI_KEISAN_KBN_CD);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.ZEI_KBN_CD);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.TAX_HASUU_CD);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KINGAKU_HASUU_CD);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_SOUFU_NAME1);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_SOUFU_NAME2);
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_KEISHOU1 = this.form.SEIKYUU_SOUFU_KEISHOU1.Text;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_KEISHOU2 = this.form.SEIKYUU_SOUFU_KEISHOU2.Text;
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_SOUFU_POST);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_SOUFU_ADDRESS1);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_SOUFU_ADDRESS2);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_SOUFU_BUSHO);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_SOUFU_TANTOU);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_SOUFU_TEL);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_SOUFU_FAX);
                if (this.form.NYUUKINSAKI_KBN.Text.Equals("1"))
                {
                    this.entitysTORIHIKISAKI_SEIKYUU.NYUUKINSAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                }
                else
                {
                    this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.NYUUKINSAKI_CD);
                }
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_TANTOU);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_DAIHYOU_PRINT_KBN);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_KYOTEN_PRINT_KBN);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_KYOTEN_CD);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.FURIKOMI_NAME1);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.FURIKOMI_NAME2);

                //振込銀行
                this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD = this.form.FURIKOMI_BANK_CD.Text;
                this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD = this.form.FURIKOMI_BANK_SHITEN_CD.Text;
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KOUZA_SHURUI);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KOUZA_NO);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KOUZA_NAME);

                this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2 = this.form.FURIKOMI_BANK_CD_2.Text;
                this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_2 = this.form.FURIKOMI_BANK_SHITEN_CD_2.Text;
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KOUZA_SHURUI_2);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KOUZA_NO_2);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KOUZA_NAME_2);

                this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3 = this.form.FURIKOMI_BANK_CD_3.Text;
                this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_3 = this.form.FURIKOMI_BANK_SHITEN_CD_3.Text;
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KOUZA_SHURUI_3);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KOUZA_NO_3);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KOUZA_NAME_3);

                // 取引先 支払情報マスタ
                // 20151014 委託契約書入力(M001)システムエラー発生関連チケット Start
                //          TIME_STAMP設定部分修正方法更新
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG ||
                    this.entitysTORIHIKISAKI_SHIHARAI == null ||
                    this.entitysTORIHIKISAKI_SHIHARAI.TIME_STAMP == null || this.entitysTORIHIKISAKI_SHIHARAI.TIME_STAMP.Length != 8)
                {
                    this.entitysTORIHIKISAKI_SHIHARAI = new M_TORIHIKISAKI_SHIHARAI();
                }
                else
                {
                    this.entitysTORIHIKISAKI_SHIHARAI = new M_TORIHIKISAKI_SHIHARAI() { TIME_STAMP = this.entitysTORIHIKISAKI_SHIHARAI.TIME_STAMP };
                }
                // 20151014 委託契約書入力(M001)システムエラー発生関連チケット End

                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.TORIHIKISAKI_CD);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_TORIHIKI_KBN_CD);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SHIMEBI1);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SHIMEBI2);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SHIMEBI3);
                //160026 S
                if (!string.IsNullOrWhiteSpace(this.form.SHIHARAI_BETSU_KBN.Text))
                {
                    this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_BETSU_KBN = Convert.ToInt16(this.form.SHIHARAI_BETSU_KBN.Text);
                }
                if (!string.IsNullOrWhiteSpace(this.form.SHIHARAI_BETSU_NICHIGO.Text))
                {
                    this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_BETSU_NICHIGO = Convert.ToInt16(this.form.SHIHARAI_BETSU_NICHIGO.Text);
                }
                //160026 E

                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_MONTH);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_DAY);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_HOUHOU);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_JOUHOU1);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_JOUHOU2);
                if (string.IsNullOrWhiteSpace(this.form.KAISHI_KAIKAKE_ZANDAKA.Text))
                {
                    this.entitysTORIHIKISAKI_SHIHARAI.KAISHI_KAIKAKE_ZANDAKA = 0;
                }
                else
                {
                    this.entitysTORIHIKISAKI_SHIHARAI.KAISHI_KAIKAKE_ZANDAKA = Decimal.Parse(this.form.KAISHI_KAIKAKE_ZANDAKA.Text, NumberStyles.AllowThousands | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowLeadingSign);
                }
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SHOSHIKI_KBN);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN);
                if (this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Enabled)
                {
                    this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SHOSHIKI_GENBA_KBN);
                }
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_KEITAI_KBN);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHUKKIN_MEISAI_KBN);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_YOUSHI_KBN);

                // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.INXS_SHIHARAI_KBN);
                // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

                if (!string.IsNullOrEmpty(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text))
                {
                    this.entitysTORIHIKISAKI_SHIHARAI.ZEI_KEISAN_KBN_CD = Int16.Parse(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text);
                }
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_ZEI_KBN_CD);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_TAX_HASUU_CD);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_KINGAKU_HASUU_CD);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SOUFU_NAME1);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SOUFU_NAME2);
                this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_KEISHOU1 = this.form.SHIHARAI_SOUFU_KEISHOU1.Text;
                this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_KEISHOU2 = this.form.SHIHARAI_SOUFU_KEISHOU2.Text;
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SOUFU_POST);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SOUFU_ADDRESS1);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SOUFU_ADDRESS2);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SOUFU_BUSHO);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SOUFU_TANTOU);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SOUFU_TEL);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SOUFU_FAX);
                this.entitysTORIHIKISAKI_SHIHARAI.SYUUKINSAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_KYOTEN_PRINT_KBN);
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_KYOTEN_CD);
                //160026 S
                if (!string.IsNullOrEmpty(this.form.FURIKOMI_EXPORT_KBN.Text))
                {
                    this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_EXPORT_KBN = Int16.Parse(this.form.FURIKOMI_EXPORT_KBN.Text);
                }
                this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_CD = this.form.FURIKOMI_SAKI_BANK_CD.Text;
                this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_NAME = this.form.FURIKOMI_SAKI_BANK_NAME.Text;
                this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_SHITEN_CD = this.form.FURIKOMI_SAKI_BANK_SHITEN_CD.Text;
                this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_SHITEN_NAME = this.form.FURIKOMI_SAKI_BANK_SHITEN_NAME.Text;
                if (!string.IsNullOrEmpty(this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Text))
                {
                    this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD = Int16.Parse(this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Text);
                    this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_SHURUI = this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI.Text;
                }
                this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_NO = this.form.FURIKOMI_SAKI_BANK_KOUZA_NO.Text;
                this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_NAME = this.form.FURIKOMI_SAKI_BANK_KOUZA_NAME.Text;
                if (!string.IsNullOrEmpty(this.form.TEI_SUU_RYOU_KBN.Text))
                {
                    this.entitysTORIHIKISAKI_SHIHARAI.TEI_SUU_RYOU_KBN = Int16.Parse(this.form.TEI_SUU_RYOU_KBN.Text);
                }
                this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_CD = this.form.FURI_KOMI_MOTO_BANK_CD.Text;
                this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_SHITTEN_CD = this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text;
                this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_SHURUI = this.form.FURI_KOMI_MOTO_SHURUI.Text;
                this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_NO = this.form.FURI_KOMI_MOTO_NO.Text;
                this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_NAME = this.form.FURI_KOMI_MOTO_NAME.Text;
                //160026 E
                this.entitysTORIHIKISAKI_SHIHARAI.SetValue(this.form.TOUROKU_NO);

                // 20150818 TIME_STAMP対応 入金先・出金先の更新は単独でSQL文実行ので、TIME_STAMP設定は不要
                if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    //入金先マスタ
                    this.entitysM_NYUUKINSAKI = new M_NYUUKINSAKI();
                    this.entitysM_NYUUKINSAKI.NYUUKINSAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                    this.entitysM_NYUUKINSAKI.NYUUKINSAKI_NAME1 = this.form.TORIHIKISAKI_NAME1.Text;
                    this.entitysM_NYUUKINSAKI.NYUUKINSAKI_NAME2 = this.form.TORIHIKISAKI_NAME2.Text;
                    this.entitysM_NYUUKINSAKI.NYUUKINSAKI_NAME_RYAKU = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
                    this.entitysM_NYUUKINSAKI.NYUUKINSAKI_FURIGANA = this.form.TORIHIKISAKI_FURIGANA.Text;
                    this.entitysM_NYUUKINSAKI.NYUUKINSAKI_TEL = this.form.TORIHIKISAKI_TEL.Text;
                    this.entitysM_NYUUKINSAKI.NYUUKINSAKI_FAX = this.form.TORIHIKISAKI_FAX.Text;
                    this.entitysM_NYUUKINSAKI.NYUUKINSAKI_POST = this.form.TORIHIKISAKI_POST.Text;
                    if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text))
                    {
                        this.entitysM_NYUUKINSAKI.NYUUKINSAKI_TODOUFUKEN_CD = Int16.Parse(this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text);
                    }
                    this.entitysM_NYUUKINSAKI.NYUUKINSAKI_ADDRESS1 = this.form.TORIHIKISAKI_ADDRESS1.Text;
                    this.entitysM_NYUUKINSAKI.NYUUKINSAKI_ADDRESS2 = this.form.TORIHIKISAKI_ADDRESS2.Text;
                    if (this.entitysM_SYS_INFO != null)
                    {
                        this.entitysM_NYUUKINSAKI.TORIKOMI_KBN = this.entitysM_SYS_INFO.NYUUKIN_TORIKOMI_KBN;
                    }
                    else
                    {
                        this.entitysM_NYUUKINSAKI.TORIKOMI_KBN = 0;
                    }
                }

                if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    //出金先マスタ
                    this.entitysM_SYUKKINSAKI = new M_SYUKKINSAKI();
                    this.entitysM_SYUKKINSAKI.SYUKKINSAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                    this.entitysM_SYUKKINSAKI.SYUKKINSAKI_NAME1 = this.form.TORIHIKISAKI_NAME1.Text;
                    this.entitysM_SYUKKINSAKI.SYUKKINSAKI_NAME2 = this.form.TORIHIKISAKI_NAME2.Text;
                    this.entitysM_SYUKKINSAKI.SYUKKINSAKI_NAME_RYAKU = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
                    this.entitysM_SYUKKINSAKI.SYUKKINSAKI_FURIGANA = this.form.TORIHIKISAKI_FURIGANA.Text;
                    this.entitysM_SYUKKINSAKI.SYUKKINSAKI_TEL = this.form.TORIHIKISAKI_TEL.Text;
                    this.entitysM_SYUKKINSAKI.SYUKKINSAKI_FAX = this.form.TORIHIKISAKI_FAX.Text;
                    this.entitysM_SYUKKINSAKI.SYUKKINSAKI_POST = this.form.TORIHIKISAKI_POST.Text;
                    if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text))
                    {
                        this.entitysM_SYUKKINSAKI.SYUKKINSAKI_TODOUFUKEN_CD = Int16.Parse(this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text);
                    }
                    this.entitysM_SYUKKINSAKI.SYUKKINSAKI_ADDRESS1 = this.form.TORIHIKISAKI_ADDRESS1.Text;
                    this.entitysM_SYUKKINSAKI.SYUKKINSAKI_ADDRESS2 = this.form.TORIHIKISAKI_ADDRESS2.Text;
                    if (this.entitysM_SYS_INFO != null)
                    {
                        this.entitysM_SYUKKINSAKI.TORIKOMI_KBN = 1;
                    }
                    else
                    {
                        this.entitysM_SYUKKINSAKI.TORIKOMI_KBN = 0;
                    }
                }

                // mapbox情報
                this.entitysTORIHIKISAKI.TORIHIKISAKI_LATITUDE = this.form.TorihikisakiLatitude.Text;
                this.entitysTORIHIKISAKI.TORIHIKISAKI_LONGITUDE = this.form.TorihikisakiLongitude.Text;
                if (string.IsNullOrEmpty(this.form.TorihikisakiLatitude.Text) &&
                    string.IsNullOrEmpty(this.form.TorihikisakiLatitude.Text) &&
                    string.IsNullOrEmpty(this.form.LocationInfoUpdateName.Text) &&
                    string.IsNullOrEmpty(this.form.LocationInfoUpdateDate.Text))
                {
                    // 未入力の状態から入力なし、という扱いなのでこの場合のみ更新なし
                }
                else
                {
                    this.entitysTORIHIKISAKI.TORIHIKISAKI_LOCATION_INFO_UPDATE_NAME = SystemProperty.UserName;
                    this.entitysTORIHIKISAKI.TORIHIKISAKI_LOCATION_INFO_UPDATE_DATE = this.sysDate();
                }

                // 更新者情報設定
                var dataBinderLogicTorihikisaki = new DataBinderLogic<M_TORIHIKISAKI>(this.entitysTORIHIKISAKI);
                dataBinderLogicTorihikisaki.SetSystemProperty(this.entitysTORIHIKISAKI, false);
                MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), this.entitysTORIHIKISAKI);
                var dataBinderLogicTorihikisakiSeikyuu = new DataBinderLogic<M_TORIHIKISAKI_SEIKYUU>(this.entitysTORIHIKISAKI_SEIKYUU);
                dataBinderLogicTorihikisakiSeikyuu.SetSystemProperty(this.entitysTORIHIKISAKI_SEIKYUU, false);
                MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), this.entitysTORIHIKISAKI_SEIKYUU);
                var dataBinderLogicTorihikisakiShiharai = new DataBinderLogic<M_TORIHIKISAKI_SHIHARAI>(this.entitysTORIHIKISAKI_SHIHARAI);
                dataBinderLogicTorihikisakiShiharai.SetSystemProperty(this.entitysTORIHIKISAKI_SHIHARAI, false);
                MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), this.entitysTORIHIKISAKI_SHIHARAI);
                var dataBinderLogicNyuukinsaki = new DataBinderLogic<M_NYUUKINSAKI>(this.entitysM_NYUUKINSAKI);
                dataBinderLogicNyuukinsaki.SetSystemProperty(this.entitysM_NYUUKINSAKI, false);
                MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), this.entitysM_NYUUKINSAKI);
                var dataBinderLogicSyukkinsaki = new DataBinderLogic<M_SYUKKINSAKI>(this.entitysM_SYUKKINSAKI);
                dataBinderLogicSyukkinsaki.SetSystemProperty(this.entitysM_SYUKKINSAKI, false);
                MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), this.entitysM_SYUKKINSAKI);

                #region 取引先CDで紐付いた業者と現場の拠点CDを更新(将来的に不要)

                var sql = "SELECT * FROM {0} WHERE {0}.TORIHIKISAKI_CD = '{1}' AND {0}.KYOTEN_CD != {2};";

                // 業者マスタ（最終更新情報等は更新しない）
                var dt = this.daoIM_GYOUSHA.GetDateForStringSql(string.Format(sql, "M_GYOUSHA",
                    this.entitysTORIHIKISAKI.TORIHIKISAKI_CD, this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD));
                this.entitysM_GYOUSHA = EntityUtility.DataTableToEntity<M_GYOUSHA>(dt);
                for (int i = 0; i < this.entitysM_GYOUSHA.Length; i++)
                {
                    this.entitysM_GYOUSHA[i].KYOTEN_CD = this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD;
                    // 20150818 TIME_STAMP個別対応 Start
                    this.entitysM_GYOUSHA[i].TIME_STAMP = Convert.IsDBNull(dt.Rows[i]["TIME_STAMP"]) ? null : (byte[])dt.Rows[i]["TIME_STAMP"];
                    // 20150818 TIME_STAMP個別対応 End
                }

                // 現場マスタ（最終更新情報等は更新しない）
                dt = this.daoIM_GENBA.GetDateForStringSql(string.Format(sql, "M_GENBA",
                    this.entitysTORIHIKISAKI.TORIHIKISAKI_CD, this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD));
                this.entitysM_GENBA = EntityUtility.DataTableToEntity<M_GENBA>(dt);
                for (int i = 0; i < this.entitysM_GENBA.Length; i++)
                {
                    this.entitysM_GENBA[i].KYOTEN_CD = this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD;
                    // 20150818 TIME_STAMP個別対応 Start
                    this.entitysM_GENBA[i].TIME_STAMP = Convert.IsDBNull(dt.Rows[i]["TIME_STAMP"]) ? null : (byte[])dt.Rows[i]["TIME_STAMP"];
                    // 20150818 TIME_STAMP個別対応 End
                }

                #endregion

                #region 取引先CDで紐付いた引合業者と引合現場の拠点CDを更新

                var hikiaiSql = "SELECT * FROM {0} WHERE {0}.TORIHIKISAKI_CD = '{1}' AND {0}.KYOTEN_CD != {2} AND {0}.HIKIAI_TORIHIKISAKI_USE_FLG = 0;";

                // 引合業者マスタ（最終更新情報等は更新しない）
                dt = this.daoIM_HIKIAI_GYOUSHA.GetDateForStringSql(string.Format(hikiaiSql, "M_HIKIAI_GYOUSHA",
                    this.entitysTORIHIKISAKI.TORIHIKISAKI_CD, this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD));
                this.entitysM_HIKIAI_GYOUSHA = EntityUtility.DataTableToEntity<M_HIKIAI_GYOUSHA>(dt);
                for (int i = 0; i < this.entitysM_HIKIAI_GYOUSHA.Length; i++)
                {
                    this.entitysM_HIKIAI_GYOUSHA[i].KYOTEN_CD = this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD;
                    // 20150818 TIME_STAMP個別対応 Start
                    this.entitysM_HIKIAI_GYOUSHA[i].TIME_STAMP = Convert.IsDBNull(dt.Rows[i]["TIME_STAMP"]) ? null : (byte[])dt.Rows[i]["TIME_STAMP"];
                    // 20150818 TIME_STAMP個別対応 End
                }

                // 引合現場マスタ（最終更新情報等は更新しない）
                dt = this.daoIM_HIKIAI_GENBA.GetDateForStringSql(string.Format(hikiaiSql, "M_HIKIAI_GENBA",
                    this.entitysTORIHIKISAKI.TORIHIKISAKI_CD, this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD));
                this.entitysM_HIKIAI_GENBA = EntityUtility.DataTableToEntity<M_HIKIAI_GENBA>(dt);
                for (int i = 0; i < this.entitysM_HIKIAI_GENBA.Length; i++)
                {
                    this.entitysM_HIKIAI_GENBA[i].KYOTEN_CD = this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD;
                    // 20150818 TIME_STAMP個別対応 Start
                    this.entitysM_HIKIAI_GENBA[i].TIME_STAMP = Convert.IsDBNull(dt.Rows[i]["TIME_STAMP"]) ? null : (byte[])dt.Rows[i]["TIME_STAMP"];
                    // 20150818 TIME_STAMP個別対応 End
                }

                #endregion

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CreateEntity", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// ゼロパディング処理
        /// </summary>
        /// <param name="inputData">ゼロパディングしたい文字列</param>
        /// <returns>ゼロパディングされた文字列</returns>
        public string ZeroPadding(string inputData)
        {
            if (!(Regex.Match(inputData, "^[a-zA-Z0-9]+$")).Success)
            {
                return inputData;
            }

            PropertyInfo pi = this.form.TORIHIKISAKI_CD.GetType().GetProperty(TorihikisakiHoshuConstans.CHARACTERS_NUMBER);
            Decimal charNumber = (Decimal)pi.GetValue(this.form.TORIHIKISAKI_CD, null);

            string padData = inputData.PadLeft((int)charNumber, '0');

            return padData;
        }

        /// <summary>
        /// 取引先CD重複チェック
        /// </summary>
        /// <param name="zeroPadCd">CD</param>
        /// <param name="isRegister">登録中か判断します</param>
        /// <returns></returns>
        public TorihikisakiHoshuConstans.TorihikisakiCdLeaveResult DupliCheckTorihikisakiCd(string zeroPadCd, bool isRegister)
        {
            LogUtility.DebugMethodStart(zeroPadCd, isRegister);

            // 取引先マスタ検索
            M_TORIHIKISAKI torihikisakiSearchSetting = new M_TORIHIKISAKI();
            torihikisakiSearchSetting.TORIHIKISAKI_CD = zeroPadCd;
            DataTable torihikiTable =
                this.daoTORIHIKISAKI.GetDataBySqlFile(GET_INPUTCD_DATA_TORIHIKISAKI_SQL, torihikisakiSearchSetting);

            // 入金先マスタ検索
            M_NYUUKINSAKI nyuukinSearch = new M_NYUUKINSAKI();
            nyuukinSearch.NYUUKINSAKI_CD = zeroPadCd;
            DataTable nyuukinTable =
                this.daoNYUKINSAKI.GetDataBySqlFile(GET_INPUTCD_DATA_NYUUKINSAKI_SQL, nyuukinSearch);

            // 出金先マスタ検索
            M_SYUKKINSAKI syukkinSearch = new M_SYUKKINSAKI();
            syukkinSearch.SYUKKINSAKI_CD = zeroPadCd;
            DataTable syukkinTable =
                this.daoSYUKINSAKI.GetDataBySqlFile(GET_INPUTCD_DATA_SYUKKINSAKI_SQL, syukkinSearch);

            // 重複チェック
            TorihikisakiHoshuValidator vali = new TorihikisakiHoshuValidator();
            DialogResult resultDialog = new DialogResult();
            bool resultDupli = vali.TorihikisakiCDValidator(torihikiTable, nyuukinTable, syukkinTable, isRegister, out resultDialog);

            TorihikisakiHoshuConstans.TorihikisakiCdLeaveResult ViewUpdateWindow = 0;

            // 重複チェックの結果と、ポップアップの結果で動作を変える
            if (!resultDupli && resultDialog == DialogResult.OK)
            {
                ViewUpdateWindow = TorihikisakiHoshuConstans.TorihikisakiCdLeaveResult.FALSE_OFF;
            }
            else if (!resultDupli && resultDialog == DialogResult.Yes)
            {
                ViewUpdateWindow = TorihikisakiHoshuConstans.TorihikisakiCdLeaveResult.FALSE_ON;
            }
            else if (!resultDupli && resultDialog == DialogResult.No)
            {
                ViewUpdateWindow = TorihikisakiHoshuConstans.TorihikisakiCdLeaveResult.FALSE_OFF;
            }
            else if (!resultDupli)
            {
                ViewUpdateWindow = TorihikisakiHoshuConstans.TorihikisakiCdLeaveResult.FALSE_NONE;
            }
            else
            {
                ViewUpdateWindow = TorihikisakiHoshuConstans.TorihikisakiCdLeaveResult.TURE_NONE;
            }

            LogUtility.DebugMethodEnd();

            return ViewUpdateWindow;
        }

        #region 取引先CD重複チェック(戻り値：bool)

        /// <summary>
        /// 取引先CD重複チェック
        /// </summary>
        /// <returns>true:重複、false:オリジナル</returns>
        public bool IsDupliTorihikisakiCd(string zeroPadCd, out bool catchErr)
        {
            try
            {
                bool returnVal = false;
                catchErr = false;

                // 取引先マスタ検索
                M_TORIHIKISAKI torihikisakiSearchSetting = new M_TORIHIKISAKI();
                torihikisakiSearchSetting.TORIHIKISAKI_CD = zeroPadCd;
                DataTable torihikiTable =
                    this.daoTORIHIKISAKI.GetDataBySqlFile(GET_INPUTCD_DATA_TORIHIKISAKI_SQL, torihikisakiSearchSetting);

                // 入金先マスタ検索
                M_NYUUKINSAKI nyuukinSearch = new M_NYUUKINSAKI();
                nyuukinSearch.NYUUKINSAKI_CD = zeroPadCd;
                DataTable nyuukinTable =
                    this.daoNYUKINSAKI.GetDataBySqlFile(GET_INPUTCD_DATA_NYUUKINSAKI_SQL, nyuukinSearch);

                // 出金先マスタ検索
                M_SYUKKINSAKI syukkinSearch = new M_SYUKKINSAKI();
                syukkinSearch.SYUKKINSAKI_CD = zeroPadCd;
                DataTable syukkinTable =
                    this.daoSYUKINSAKI.GetDataBySqlFile(GET_INPUTCD_DATA_SYUKKINSAKI_SQL, syukkinSearch);

                // 重複チェック
                /**
                 * 取引先で使用されておらず、入金先で使用されている場合
                 * または、取引先で使用されておらず、出金先で使用されている場合
                 * または、取引先で使用されている場合
                 * (TorihikisakiHoshuValidator#TorihikisakiCDValidatorを流用)
                 */
                if ((torihikiTable.Rows.Count == 0 && torihikiTable.Rows.Count > 0)
                    || (torihikiTable.Rows.Count == 0 && nyuukinTable.Rows.Count > 0)
                    || (torihikiTable.Rows.Count > 0))
                {
                    returnVal = true;
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("IsDupliTorihikisakiCd", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        /// <summary>
        /// 取消処理
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool Cancel(BusinessBaseForm parentForm)
        {
            try
            {
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    // 新規モードの場合は空画面を表示する
                    this.WindowInitNewMode(parentForm);
                }
                else
                {
                    // 取引先入力画面表示時の取引先CDで再検索・再表示
                    this.SetWindowData();
                    this.WindowInitNewMode(parentForm);
                    this.SetDataForWindow();

                    // 業者分類タブ初期化
                    bool catchErr = this.ManiCheckOffCheck();
                    if (catchErr)
                    {
                        return true;
                    }

                    this.form.TORIHIKISAKI_CD.Enabled = false;   // 取引先CD
                    this.form.SAIBAN_BUTTON.Enabled = false;    // 採番ボタン
                }
                return false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("Cancel", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
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
            LogUtility.DebugMethodStart(errorFlag);

            var msgLogic = new MessageBoxShowLogic();

            try
            {
                // 入金先区分が他社の場合、入金先CDを必須とする
                if (this.form.NYUUKINSAKI_KBN.Text.Equals("2") && string.IsNullOrEmpty(this.form.NYUUKINSAKI_CD.Text))
                {
                    //取引先区分が現金の場合のみ必須とする
                    if (this.form.TORIHIKI_KBN.Text.Equals("2") && string.IsNullOrEmpty(this.form.TORIHIKI_KBN.Text))
                    {
                        msgLogic.MessageBoxShow("E001", TorihikisakiHoshu.Properties.Resources.NYUUKINSAKI_CD);
                        errorFlag = true;
                    }
                }

                if (errorFlag)
                {
                    this.isRegist = false;
                    return;
                }

                this.entitysTORIHIKISAKI.DELETE_FLG = false;
                this.daoTORIHIKISAKI.Insert(entitysTORIHIKISAKI);
                this.daoTORIHIKISAKI_SEIKYUU.Insert(entitysTORIHIKISAKI_SEIKYUU);
                this.daoTORIHIKISAKI_SHIHARAI.Insert(entitysTORIHIKISAKI_SHIHARAI);
                this.entitysM_NYUUKINSAKI.DELETE_FLG = false;
                this.daoIM_NYUUKINSAKI.Insert(entitysM_NYUUKINSAKI);
                this.entitysM_SYUKKINSAKI.DELETE_FLG = false;
                this.daoIM_SYUKKINSAKI.Insert(entitysM_SYUKKINSAKI);

                this.isRegist = true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart();

            var msgLogic = new MessageBoxShowLogic();

            try
            {
                if (errorFlag)
                {
                    this.isRegist = false;
                    return;
                }

                this.entitysTORIHIKISAKI.DELETE_FLG = false;
                this.daoTORIHIKISAKI.Update(entitysTORIHIKISAKI);
                this.daoTORIHIKISAKI_SEIKYUU.Update(entitysTORIHIKISAKI_SEIKYUU);
                this.daoTORIHIKISAKI_SHIHARAI.Update(entitysTORIHIKISAKI_SHIHARAI);

                // 自動作成された入金先を更新する
                // ※但し、更新する部分は、自動作成した情報部分のみ
                this.entitysM_NYUUKINSAKI.DELETE_FLG = this.entitysTORIHIKISAKI.DELETE_FLG;
                this.daoIM_NYUUKINSAKI.GetDataBySqlFile(UPDATE_NYUUKINSAKI_SQL, this.entitysM_NYUUKINSAKI);

                // 自動作成された出金先を更新する
                // ※但し、更新する部分は、自動作成した情報部分のみ
                this.entitysM_SYUKKINSAKI.DELETE_FLG = this.entitysTORIHIKISAKI.DELETE_FLG;
                this.daoIM_SYUKKINSAKI.GetDataBySqlFile(UPDATE_SYUKKINSAKI_SQL, this.entitysM_SYUKKINSAKI);

                #region 取引先CDで紐付いた業者と現場の更新(将来的に不要)

                // 業者
                foreach (var gyousha in this.entitysM_GYOUSHA)
                {
                    this.daoIM_GYOUSHA.Update(gyousha);
                }

                // 現場
                foreach (var genba in this.entitysM_GENBA)
                {
                    this.daoIM_GENBA.Update(genba);
                }

                #endregion

                #region 取引先CDで紐付いた引合業者と引合現場の更新

                // 引合業者
                foreach (var hikiaiGyousha in this.entitysM_HIKIAI_GYOUSHA)
                {
                    this.daoIM_HIKIAI_GYOUSHA.Update(hikiaiGyousha);
                }

                // 引合現場
                foreach (var hikiaiGenba in this.entitysM_HIKIAI_GENBA)
                {
                    this.daoIM_HIKIAI_GENBA.Update(hikiaiGenba);
                }

                #endregion

                this.isRegist = true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Update", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Update", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Update", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        public virtual void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            var msgLogic = new MessageBoxShowLogic();

            try
            {
                // 引合データの使用有無を確認
                if (ContainsTorihikisaki())
                {
                    return;
                }

                // 他マスタの使用有無を確認
                if (!CheckDelete())
                {
                    return;
                }

                this.entitysTORIHIKISAKI.DELETE_FLG = true;

                this.daoTORIHIKISAKI.Update(this.entitysTORIHIKISAKI);

                // 自動作成された入金先を更新する
                // ※但し、更新する部分は、自動作成した情報部分のみ
                if (this.entitysM_NYUUKINSAKI != null)
                {
                    this.entitysM_NYUUKINSAKI.TORIKOMI_KBN = System.Data.SqlTypes.SqlInt16.Null;
                    this.entitysM_NYUUKINSAKI.DELETE_FLG = this.entitysTORIHIKISAKI.DELETE_FLG;
                    this.daoIM_NYUUKINSAKI.Update(this.entitysM_NYUUKINSAKI);
                }

                // 自動作成された出金先を更新する
                // ※但し、更新する部分は、自動作成した情報部分のみ
                if (this.entitysM_SYUKKINSAKI != null)
                {
                    this.entitysM_SYUKKINSAKI.TORIKOMI_KBN = System.Data.SqlTypes.SqlInt16.Null;
                    this.entitysM_SYUKKINSAKI.DELETE_FLG = this.entitysTORIHIKISAKI.DELETE_FLG;
                    this.daoIM_SYUKKINSAKI.Update(this.entitysM_SYUKKINSAKI);
                }

                this.isRegist = true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Equals/GetHashCode/ToString

        //<summary>
        //クラスが等しいかどうか判定
        //</summary>
        //<param name="other"></param>
        //<returns></returns>
        public override bool Equals(object other)
        {
            //objがnullか、型が違うときは、等価でない
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            TorihikisakiHoshuLogic localLogic = other as TorihikisakiHoshuLogic;
            return localLogic == null ? false : true;
        }

        //<summary>
        //ハッシュコード取得
        //</summary>
        //<returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        // <summary>
        //該当するオブジェクトを文字列形式で取得
        //</summary>
        //<returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        //<summary>
        //ボタン初期化処理
        //</summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.ParentForm;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        //<summary>
        //イベントの初期化処理
        //</summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //登録(F9)イベント生成
            if (denshiShinseiFlg)
            {
                //「F9:申請」となる場合のイベント生成

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
            }
            else
            {
                //新規(F2)イベント生成
                parentForm.bt_func2.Click += new EventHandler(this.form.CreateMode);

                //修正ボタン(F3)イベント生成
                parentForm.bt_func3.Click += new EventHandler(this.form.UpdateMode);

                //削除(F4)イベント生成
                //parentForm.bt_func4.Click += new EventHandler(this.form.UpdateMode);

                //一覧(F7)イベント生成
                parentForm.bt_func7.Click += new EventHandler(this.form.ShowIchiran);

                this.form.C_Regist(parentForm.bt_func9);
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                //取消し(F11)イベント生成
                parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
            }
            //取引先CD変更後処理
            this.form.TORIHIKISAKI_CD.Validated += new EventHandler(this.form.TorihikisakiCdValidated);

            //採番ボタン押下処理
            this.form.SAIBAN_BUTTON.Click += new EventHandler(this.form.SaibanButtonClick);

            //請求タブの取引先コピーボタン押下処理
            this.form.TORIHIKISAKI_COPY_SEIKYU_BUTTON.Click += new EventHandler(this.form.CopySeikyuButtonClick);

            //支払タブの取引先コピーボタン押下処理
            this.form.TORIHIKISAKI_COPY_SIHARAI_BUTTON.Click += new EventHandler(this.form.CopySiharaiButtonClick);

            //分類タブの取引先コピーボタン押下処理
            this.form.TORIHIKISAKI_COPY_MANI_BUTTON.Click += new EventHandler(this.form.CopyManiButtonClick);

            //営業担当部署CD変更後処理
            this.form.EIGYOU_TANTOU_BUSHO_CD.Validated += new EventHandler(this.form.EigyouTantouBushoCdValidated);

            //営業担当者CD変更後処理
            this.form.EIGYOU_TANTOU_CD.Validated += new EventHandler(this.form.EigyouTantouCdValidated);

            //振込銀行CD変更後処理
            this.form.FURIKOMI_BANK_CD.Validated += new EventHandler(this.form.FurikomiBankCdValidated);

            //振込銀行CD2変更後処理
            this.form.FURIKOMI_BANK_CD_2.Validated += new EventHandler(this.form.FurikomiBankCd2Validated);

            //振込銀行CD3変更後処理
            this.form.FURIKOMI_BANK_CD_3.Validated += new EventHandler(this.form.FurikomiBankCd3Validated);
            //振込元銀行
            this.form.FURI_KOMI_MOTO_BANK_CD.Validated += new EventHandler(this.form.FurikomiMotoBankCdValidated);//160026


            // 振込銀行支店CDバリデート処理
            this.form.FURIKOMI_BANK_SHITEN_CD.Validating += new CancelEventHandler(this.form.FURIKOMI_BANK_SHITEN_CD_Validating);

            // 振込銀行支店CD2バリデート処理
            this.form.FURIKOMI_BANK_SHITEN_CD_2.Validating += new CancelEventHandler(this.form.FURIKOMI_BANK_SHITEN_CD_2_Validating);

            // 振込銀行支店CD3バリデート処理
            this.form.FURIKOMI_BANK_SHITEN_CD_3.Validating += new CancelEventHandler(this.form.FURIKOMI_BANK_SHITEN_CD_3_Validating);

            // 振込元支店バリデート処理
            this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Validating += new CancelEventHandler(this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD_Validating);//160026
            this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Validated += new EventHandler(this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD_Validated);//160026

            // VUNGUYEN 20150525 #1294 START
            // 適用終了のダブルクリックイベント
            this.form.TEKIYOU_END.MouseDoubleClick += new MouseEventHandler(TEKIYOU_END_MouseDoubleClick);
            // VUNGUYEN 20150525 #1294 END

            this.form.NYUUKINSAKI_KBN.TextChanged += new EventHandler(this.form.NYUUKINSAKI_KBN_TextChanged);

            // mapbox連携
            // 地図表示処理
            this.form.bt_map_open.Click += new EventHandler(this.OpenMap);

            LogUtility.DebugMethodEnd();
        }

        //<summary>
        //ボタン設定の読込
        //</summary>
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

        //<summary>
        //検索条件初期化
        //</summary>
        private void ClearCondition()
        {
            LogUtility.DebugMethodStart();

            this.TorihikisakiCd = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先CD採番処理
        /// </summary>
        /// <returns>最大ID+1</returns>
        public bool Saiban()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 取引先マスタと入金先マスタと出金先マスタのCDの最大値+1を取得
                TorihikisakiMasterAccess torihikisakiMasterAccess = new TorihikisakiMasterAccess(new CustomTextBox(), new object[] { }, new object[] { });
                int keyTorihikisaki = -1;

                var keyTorihikisakiSsaibanFlag = torihikisakiMasterAccess.IsOverCDLimit(out keyTorihikisaki);

                if (keyTorihikisakiSsaibanFlag)
                {
                    // 採番エラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E041");
                    this.form.TORIHIKISAKI_CD.Text = "";
                }

                if (keyTorihikisaki < 1)
                {
                    // 採番エラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E041");
                    this.form.TORIHIKISAKI_CD.Text = "";
                }
                else
                {
                    // ゼロパディング後、テキストへ設定
                    this.form.TORIHIKISAKI_CD.Text = String.Format("{0:D" + this.form.TORIHIKISAKI_CD.MaxLength + "}", keyTorihikisaki);
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Saiban", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        //<summary>
        //取引先の情報を請求にコピーする
        //</summary>
        public bool CopyToSeikyu()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.SEIKYUU_SOUFU_NAME1.Text = this.form.TORIHIKISAKI_NAME1.Text;
                this.form.SEIKYUU_SOUFU_NAME2.Text = this.form.TORIHIKISAKI_NAME2.Text;
                this.form.SEIKYUU_SOUFU_KEISHOU1.Text = this.form.TORIHIKISAKI_KEISHOU1.Text;
                this.form.SEIKYUU_SOUFU_KEISHOU2.Text = this.form.TORIHIKISAKI_KEISHOU2.Text;
                this.form.SEIKYUU_SOUFU_POST.Text = this.form.TORIHIKISAKI_POST.Text;
                this.form.SEIKYUU_SOUFU_ADDRESS1.Text = this.form.TORIHIKISAKI_TODOUFUKEN_NAME.Text + this.form.TORIHIKISAKI_ADDRESS1.Text;
                this.form.SEIKYUU_SOUFU_ADDRESS2.Text = this.form.TORIHIKISAKI_ADDRESS2.Text;
                this.form.SEIKYUU_SOUFU_BUSHO.Text = this.form.BUSHO.Text;
                this.form.SEIKYUU_SOUFU_TANTOU.Text = this.form.TANTOUSHA.Text;
                this.form.SEIKYUU_SOUFU_TEL.Text = this.form.TORIHIKISAKI_TEL.Text;
                this.form.SEIKYUU_SOUFU_FAX.Text = this.form.TORIHIKISAKI_FAX.Text;

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CopyToSeikyu", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        //<summary>
        //取引先の情報を支払いのコピーする
        //</summary>
        public bool CopyToSiharai()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.SHIHARAI_SOUFU_NAME1.Text = this.form.TORIHIKISAKI_NAME1.Text;
                this.form.SHIHARAI_SOUFU_NAME2.Text = this.form.TORIHIKISAKI_NAME2.Text;
                this.form.SHIHARAI_SOUFU_KEISHOU1.Text = this.form.TORIHIKISAKI_KEISHOU1.Text;
                this.form.SHIHARAI_SOUFU_KEISHOU2.Text = this.form.TORIHIKISAKI_KEISHOU2.Text;
                this.form.SHIHARAI_SOUFU_POST.Text = this.form.TORIHIKISAKI_POST.Text;
                this.form.SHIHARAI_SOUFU_ADDRESS1.Text = this.form.TORIHIKISAKI_TODOUFUKEN_NAME.Text + this.form.TORIHIKISAKI_ADDRESS1.Text;
                this.form.SHIHARAI_SOUFU_ADDRESS2.Text = this.form.TORIHIKISAKI_ADDRESS2.Text;
                this.form.SHIHARAI_SOUFU_BUSHO.Text = this.form.BUSHO.Text;
                this.form.SHIHARAI_SOUFU_TANTOU.Text = this.form.TANTOUSHA.Text;
                this.form.SHIHARAI_SOUFU_TEL.Text = this.form.TORIHIKISAKI_TEL.Text;
                this.form.SHIHARAI_SOUFU_FAX.Text = this.form.TORIHIKISAKI_FAX.Text;

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CopyToSiharai", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        //<summary>
        //取引先の情報を分類にコピーする
        //</summary>
        public bool CopyToMani()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.MANI_HENSOUSAKI_NAME1.Text = this.form.TORIHIKISAKI_NAME1.Text;
                this.form.MANI_HENSOUSAKI_NAME2.Text = this.form.TORIHIKISAKI_NAME2.Text;
                this.form.MANI_HENSOUSAKI_KEISHOU1.Text = this.form.TORIHIKISAKI_KEISHOU1.Text;
                this.form.MANI_HENSOUSAKI_KEISHOU2.Text = this.form.TORIHIKISAKI_KEISHOU2.Text;
                this.form.MANI_HENSOUSAKI_POST.Text = this.form.TORIHIKISAKI_POST.Text;
                this.form.MANI_HENSOUSAKI_ADDRESS1.Text = this.form.TORIHIKISAKI_TODOUFUKEN_NAME.Text + this.form.TORIHIKISAKI_ADDRESS1.Text;
                this.form.MANI_HENSOUSAKI_ADDRESS2.Text = this.form.TORIHIKISAKI_ADDRESS2.Text;
                this.form.MANI_HENSOUSAKI_BUSHO.Text = this.form.BUSHO.Text;
                this.form.MANI_HENSOUSAKI_TANTOU.Text = this.form.TANTOUSHA.Text;

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CopyToMani", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
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
                LogUtility.Error("CheckTextBoxLength", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                res = false;
                return res;
            }
            finally
            {
                LogUtility.DebugMethodEnd(res);
            }
        }

        /// <summary>
        /// 営業担当部署CD変更後処理
        /// </summary>
        public bool EigyouTantouBushoCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.EIGYOU_TANTOU_BUSHO_CD.Text))
                {
                    this.form.EIGYOU_TANTOU_BUSHO_CD.Text = string.Empty;
                    this.form.EIGYOU_TANTOU_BUSHO_NAME.Text = string.Empty;
                    this.form.EIGYOU_TANTOU_CD.Text = string.Empty;
                    this.form.EIGYOU_TANTOU_NAME.Text = string.Empty;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EigyouTantouBushoCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 営業担当者CD変更後処理
        /// </summary>
        public bool EigyouTantouCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOU_CD.Text))
                {
                    M_SHAIN prm = new M_SHAIN();
                    prm.SHAIN_CD = this.form.EIGYOU_TANTOU_CD.Text;
                    prm.EIGYOU_TANTOU_KBN = true;
                    if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOU_BUSHO_CD.Text))
                    {
                        prm.BUSHO_CD = this.form.EIGYOU_TANTOU_BUSHO_CD.Text;
                    }
                    M_SHAIN[] data = this.daoIM_SHAIN.GetAllValidData(prm);
                    if (data != null && data.Length > 0)
                    {
                        this.entitysM_SHAIN = data[0];
                        this.form.EIGYOU_TANTOU_BUSHO_CD.Text = this.entitysM_SHAIN.BUSHO_CD;
                        this.entitysM_BUSHO = this.daoIM_BUSHO.GetDataByCd(this.entitysM_SHAIN.BUSHO_CD);
                        if (this.entitysM_BUSHO != null)
                        {
                            this.form.EIGYOU_TANTOU_BUSHO_NAME.Text = this.entitysM_BUSHO.BUSHO_NAME_RYAKU;
                        }

                        this.form.EIGYOU_TANTOU_CD.Text = this.entitysM_SHAIN.SHAIN_CD;
                        this.form.EIGYOU_TANTOU_NAME.Text = this.entitysM_SHAIN.SHAIN_NAME_RYAKU;
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "営業担当者");
                        this.isError = true;
                        ret = false;
                    }
                }
                else
                {
                    this.form.EIGYOU_TANTOU_CD.Text = string.Empty;
                    this.form.EIGYOU_TANTOU_NAME.Text = string.Empty;
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("EigyouTantouCdValidated", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EigyouTantouCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// <summary>
        /// 請求書拠点の値チェック
        /// </summary>
        public bool SeikyuuKyotenCdValidated(out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart();

                catchErr = false;
                bool ret = true;
                this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                if (!string.IsNullOrEmpty(this.form.SEIKYUU_KYOTEN_CD.Text))
                {
                    M_KYOTEN kyo = this.daoIM_KYOTEN.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
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

                LogUtility.DebugMethodEnd(ret, catchErr);
                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("SeikyuuKyotenCdValidated", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false, catchErr);
                return false;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("SeikyuuKyotenCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false, catchErr);
                return false;
            }
        }

        /// <summary>
        /// 支払書拠点の値チェック
        /// </summary>
        public bool ShiharaiKyotenCdValidated(out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart();

                catchErr = false;
                bool ret = true;
                this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                if (!string.IsNullOrEmpty(this.form.SHIHARAI_KYOTEN_CD.Text))
                {
                    M_KYOTEN kyo = this.daoIM_KYOTEN.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
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

                LogUtility.DebugMethodEnd(ret, catchErr);
                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("ShiharaiKyotenCdValidated", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false, catchErr);
                return false;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("ShiharaiKyotenCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false, catchErr);
                return false;
            }
        }

        /// <summary>
        /// 振込銀行CD変更後処理
        /// </summary>
        public bool FurikomiBankCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.FURIKOMI_BANK_CD.Text))
                {
                    this.form.FURIKOMI_BANK_CD.Text = string.Empty;
                    this.form.FURIKOMI_BANK_NAME.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_CD.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_NAME.Text = string.Empty;
                    this.form.KOUZA_SHURUI.Text = string.Empty;
                    this.form.KOUZA_NO.Text = string.Empty;
                    this.form.KOUZA_NAME.Text = string.Empty;
                }

                this.form.previousBankShitenCd = this.form.FURIKOMI_BANK_SHITEN_CD.Text;

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("FurikomiBankCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 振込銀行CD2変更後処理
        /// </summary>
        public bool FurikomiBankCd2Validated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.FURIKOMI_BANK_CD_2.Text))
                {
                    this.form.FURIKOMI_BANK_CD_2.Text = string.Empty;
                    this.form.FURIKOMI_BANK_NAME_2.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_CD_2.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_NAME_2.Text = string.Empty;
                    this.form.KOUZA_SHURUI_2.Text = string.Empty;
                    this.form.KOUZA_NO_2.Text = string.Empty;
                    this.form.KOUZA_NAME_2.Text = string.Empty;
                }

                this.form.previousBankShitenCd_2 = this.form.FURIKOMI_BANK_SHITEN_CD_2.Text;

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("FurikomiBankCd2Validated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 振込銀行CD3変更後処理
        /// </summary>
        public bool FurikomiBankCd3Validated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.FURIKOMI_BANK_CD_3.Text))
                {
                    this.form.FURIKOMI_BANK_CD_3.Text = string.Empty;
                    this.form.FURIKOMI_BANK_NAME_3.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_CD_3.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_NAME_3.Text = string.Empty;
                    this.form.KOUZA_SHURUI_3.Text = string.Empty;
                    this.form.KOUZA_NO_3.Text = string.Empty;
                    this.form.KOUZA_NAME_3.Text = string.Empty;
                }

                this.form.previousBankShitenCd_3 = this.form.FURIKOMI_BANK_SHITEN_CD_3.Text;

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("FurikomiBankCd3Validated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }


        /// <summary>
        /// 入金先CD変更後処理
        /// </summary>
        public bool NyuukinsakiCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!string.IsNullOrEmpty(this.form.NYUUKINSAKI_CD.Text))
                {
                    this.entitysM_NYUUKINSAKI = this.daoIM_NYUUKINSAKI.GetDataByCd(this.form.NYUUKINSAKI_CD.Text);
                    if (this.entitysM_NYUUKINSAKI != null)
                    {
                        this.form.NYUUKINSAKI_NAME1.Text = this.entitysM_NYUUKINSAKI.NYUUKINSAKI_NAME1;
                        this.form.NYUUKINSAKI_NAME2.Text = this.entitysM_NYUUKINSAKI.NYUUKINSAKI_NAME2;
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("NyuukinsakiCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 取引区分変更後処理
        /// </summary>
        /// <param name="isTextChanged">true:calling TextChangedEvent, false:other process</param>
        public bool ChangeTorihikiKbn(bool isTextChanged)
        {
            try
            {
                LogUtility.DebugMethodStart(isTextChanged);

                //1:現金　2:掛け
                if (this.form.TORIHIKI_KBN.Text.Equals("1"))
                {
                    this.form.SHIMEBI1.SelectedIndex = -1;
                    this.form.SHIMEBI2.SelectedIndex = -1;
                    this.form.SHIMEBI3.SelectedIndex = -1;
                    this.form.SHIMEBI1.Enabled = false;
                    this.form.SHIMEBI2.Enabled = false;
                    this.form.SHIMEBI3.Enabled = false;
                    this.form.HICCHAKUBI.Text = string.Empty;
                    this.form.HICCHAKUBI.Enabled = false;
                    //160026 S
                    this.form.KAISHUU_BETSU_KBN.Text = string.Empty;
                    this.form.KAISHUU_BETSU_KBN.Enabled = false;
                    this.form.KAISHUU_BETSU_KBN_1.Enabled = false;
                    this.form.KAISHUU_BETSU_KBN_2.Enabled = false;
                    this.form.KAISHUU_BETSU_NICHIGO.Text = string.Empty;
                    this.form.KAISHUU_BETSU_NICHIGO.Enabled = false;
                    //160026 E

                    this.form.KAISHUU_MONTH.Text = string.Empty;
                    this.form.KAISHUU_MONTH.Enabled = false;
                    this.form.KAISHUU_MONTH_1.Enabled = false;
                    this.form.KAISHUU_MONTH_2.Enabled = false;
                    this.form.KAISHUU_MONTH_3.Enabled = false;
                    this.form.KAISHUU_MONTH_4.Enabled = false;
                    this.form.KAISHUU_MONTH_5.Enabled = false;
                    this.form.KAISHUU_MONTH_6.Enabled = false;
                    this.form.KAISHUU_MONTH_7.Enabled = false;
                    this.form.KAISHUU_DAY.Text = string.Empty;
                    this.form.KAISHUU_DAY.Enabled = false;
                    this.form.KAISHUU_HOUHOU.Text = string.Empty;
                    this.form.KAISHUU_HOUHOU.Enabled = false;
                    this.form.KAISHUU_HOUHOU_SEARCH.Enabled = false;
                    this.form.KAISHUU_HOUHOU_NAME.Text = string.Empty;
                    this.form.KAISHI_URIKAKE_ZANDAKA.Text = string.Empty;
                    this.form.KAISHI_URIKAKE_ZANDAKA.Enabled = false;
                    this.form.SHOSHIKI_KBN.Text = string.Empty;
                    this.form.SHOSHIKI_KBN.Enabled = false;
                    this.form.SHOSHIKI_KBN_1.Enabled = false;
                    this.form.SHOSHIKI_KBN_2.Enabled = false;
                    this.form.SHOSHIKI_KBN_3.Enabled = false;
                    this.form.SHOSHIKI_MEISAI_KBN.Text = string.Empty;
                    this.form.SHOSHIKI_MEISAI_KBN.Enabled = false;
                    this.form.SHOSHIKI_MEISAI_KBN_1.Enabled = false;
                    this.form.SHOSHIKI_MEISAI_KBN_2.Enabled = false;
                    this.form.SHOSHIKI_MEISAI_KBN_3.Enabled = false;
                    this.form.SHOSHIKI_GENBA_KBN.Text = string.Empty;
                    this.form.SHOSHIKI_GENBA_KBN.Enabled = false;
                    this.form.SHOSHIKI_GENBA_KBN_1.Enabled = false;
                    this.form.SHOSHIKI_GENBA_KBN_2.Enabled = false;
                    this.form.SEIKYUU_KEITAI_KBN.Text = string.Empty;
                    this.form.SEIKYUU_KEITAI_KBN.Enabled = false;
                    this.form.SEIKYUU_KEITAI_KBN_1.Enabled = false;
                    this.form.SEIKYUU_KEITAI_KBN_2.Enabled = false;
                    this.form.NYUUKIN_MEISAI_KBN.Text = string.Empty;
                    this.form.NYUUKIN_MEISAI_KBN.Enabled = false;
                    this.form.NYUUKIN_MEISAI_KBN_1.Enabled = false;
                    this.form.NYUUKIN_MEISAI_KBN_2.Enabled = false;
                    this.form.YOUSHI_KBN.Text = string.Empty;
                    this.form.YOUSHI_KBN.Enabled = false;
                    this.form.YOUSHI_KBN_1.Enabled = false;
                    this.form.YOUSHI_KBN_2.Enabled = false;
                    this.form.YOUSHI_KBN_3.Enabled = false;

                    #region LANDUONG - 20211227
                    this.form.pl_SEIKYUU_OUTPUT_KBN.Enabled = false;
                    this.form.OUTPUT_KBN.Text = string.Empty;
                    this.form.HAKKOUSAKI_CD.Text = string.Empty;
                    this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;
                    #endregion LANDUONG - 20211227                    

                    // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
                    this.form.INXS_SEIKYUU_KBN.Text = "2";
                    this.form.INXS_SEIKYUU_KBN.Enabled = false;
                    this.form.INXS_SEIKYUU_KBN_1.Enabled = false;
                    this.form.INXS_SEIKYUU_KBN_2.Enabled = false;
                    // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

                    this.form.ZEI_KEISAN_KBN_CD_2.Enabled = false;
                    if (TorihikisakiHoshuConstans.ZEI_KEISAN_KBN_SEIKYUU.ToString().Equals(this.form.ZEI_KEISAN_KBN_CD.Text))
                    {
                        this.form.ZEI_KEISAN_KBN_CD.Text = TorihikisakiHoshuConstans.ZEI_KEISAN_KBN_DENPYOU.ToString();
                    }

                    this.form.FURIKOMI_BANK_CD.Enabled = false;
                    this.form.FURIKOMI_BANK_CD.Text = string.Empty;
                    this.form.FURIKOMI_BANK_NAME.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SEARCH.Enabled = false;
                    this.form.FURIKOMI_BANK_SHITEN_CD.Enabled = false;
                    this.form.FURIKOMI_BANK_SHITEN_CD.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_NAME.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_SEARCH.Enabled = false;
                    this.form.KOUZA_SHURUI.Text = string.Empty;
                    this.form.KOUZA_NO.Text = string.Empty;
                    this.form.KOUZA_NAME.Text = string.Empty;
                    this.form.FURIKOMI_BANK_CD_2.Enabled = false;
                    this.form.FURIKOMI_BANK_CD_2.Text = string.Empty;
                    this.form.FURIKOMI_BANK_NAME_2.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SEARCH_2.Enabled = false;
                    this.form.FURIKOMI_BANK_SHITEN_CD_2.Enabled = false;
                    this.form.FURIKOMI_BANK_SHITEN_CD_2.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_NAME_2.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_SEARCH_2.Enabled = false;
                    this.form.KOUZA_SHURUI_2.Text = string.Empty;
                    this.form.KOUZA_NO_2.Text = string.Empty;
                    this.form.KOUZA_NAME_2.Text = string.Empty;
                    this.form.FURIKOMI_BANK_CD_3.Enabled = false;
                    this.form.FURIKOMI_BANK_CD_3.Text = string.Empty;
                    this.form.FURIKOMI_BANK_NAME_3.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SEARCH_3.Enabled = false;
                    this.form.FURIKOMI_BANK_SHITEN_CD_3.Enabled = false;
                    this.form.FURIKOMI_BANK_SHITEN_CD_3.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_NAME_3.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_SEARCH_3.Enabled = false;
                    this.form.KOUZA_SHURUI_3.Text = string.Empty;
                    this.form.KOUZA_NO_3.Text = string.Empty;
                    this.form.KOUZA_NAME_3.Text = string.Empty;
                    this.form.SEIKYUU_JOUHOU1.Enabled = false;
                    this.form.SEIKYUU_JOUHOU1.Text = string.Empty;
                    this.form.SEIKYUU_JOUHOU2.Enabled = false;
                    this.form.SEIKYUU_JOUHOU2.Text = string.Empty;
                    this.form.TORIHIKISAKI_COPY_SEIKYU_BUTTON.Enabled = false;
                    this.form.SEIKYUU_SOUFU_NAME1.Enabled = false;
                    this.form.SEIKYUU_SOUFU_NAME1.Text = string.Empty;
                    this.form.SEIKYUU_SOUFU_KEISHOU1.Enabled = false;
                    this.form.SEIKYUU_SOUFU_KEISHOU1.Text = string.Empty;
                    this.form.SEIKYUU_SOUFU_NAME2.Enabled = false;
                    this.form.SEIKYUU_SOUFU_NAME2.Text = string.Empty;
                    this.form.SEIKYUU_SOUFU_KEISHOU2.Enabled = false;
                    this.form.SEIKYUU_SOUFU_KEISHOU2.Text = string.Empty;
                    this.form.SEIKYUU_SOUFU_POST.Enabled = false;
                    this.form.SEIKYUU_SOUFU_POST.Text = string.Empty;
                    this.form.SEIKYUU_JUSHO_SEARCH.Enabled = false;
                    this.form.SEIKYUU_SOUFU_ADDRESS1.Enabled = false;
                    this.form.SEIKYUU_SOUFU_ADDRESS1.Text = string.Empty;
                    this.form.SEIKYUU_SOUFU_ADDRESS2.Enabled = false;
                    this.form.SEIKYUU_SOUFU_ADDRESS2.Text = string.Empty;
                    this.form.SEIKYUU_POST_SEARCH.Enabled = false;
                    this.form.SEIKYUU_SOUFU_BUSHO.Enabled = false;
                    this.form.SEIKYUU_SOUFU_BUSHO.Text = string.Empty;
                    this.form.SEIKYUU_SOUFU_TANTOU.Enabled = false;
                    this.form.SEIKYUU_SOUFU_TANTOU.Text = string.Empty;
                    this.form.SEIKYUU_SOUFU_TEL.Enabled = false;
                    this.form.SEIKYUU_SOUFU_TEL.Text = string.Empty;
                    this.form.SEIKYUU_SOUFU_FAX.Enabled = false;
                    this.form.SEIKYUU_SOUFU_FAX.Text = string.Empty;
                    this.form.NYUUKINSAKI_CD.Enabled = false;
                    this.form.NYUUKINSAKI_CD.Text = string.Empty;
                    this.form.NYUUKINSAKI_NAME1.Text = string.Empty;
                    this.form.NYUUKINSAKI_NAME2.Text = string.Empty;
                    this.form.NYUUKINSAKI_SEARCH.Enabled = false;
                    this.form.SEIKYUU_TANTOU.Enabled = false;
                    this.form.SEIKYUU_TANTOU.Text = string.Empty;
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Enabled = false;
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN_1.Enabled = false;
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN_2.Enabled = false;
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled = false;
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN_1.Enabled = false;
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN_2.Enabled = false;
                    this.form.SEIKYUU_KYOTEN_CD.Enabled = false;
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_NAME.Enabled = false;
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_CD_SEARCH.Enabled = false;
                    this.form.NYUUKINSAKI_KBN.Text = "1";
                    this.form.NYUUKINSAKI_KBN.Enabled = false;
                    this.form.NYUUKINSAKI_KBN_1.Enabled = false;
                    this.form.NYUUKINSAKI_KBN_2.Enabled = false;
                    this.form.FURIKOMI_NAME1.Enabled = false;
                    this.form.FURIKOMI_NAME1.Text = string.Empty;
                    this.form.FURIKOMI_NAME2.Enabled = false;
                    this.form.FURIKOMI_NAME2.Text = string.Empty;
                }
                else
                {
                    this.form.NYUUKINSAKI_KBN.Enabled = true;
                    this.form.NYUUKINSAKI_KBN_1.Enabled = true;
                    this.form.NYUUKINSAKI_KBN_2.Enabled = true;
                    this.form.SHIMEBI1.Enabled = true;
                    this.form.SHIMEBI2.Enabled = true;
                    this.form.SHIMEBI3.Enabled = true;
                    this.form.SHIMEBI1.SelectedIndex = -1;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_SHIMEBI1.IsNull)
                    {
                        this.form.SHIMEBI1.SelectedItem = this.entitysM_SYS_INFO.SEIKYUU_SHIMEBI1.ToString();
                    }
                    else
                    {
                        this.form.SHIMEBI2.Enabled = false;
                        this.form.SHIMEBI3.Enabled = false;
                    }
                    this.form.SHIMEBI2.SelectedIndex = -1;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_SHIMEBI2.IsNull)
                    {
                        this.form.SHIMEBI2.SelectedItem = this.entitysM_SYS_INFO.SEIKYUU_SHIMEBI2.ToString();
                    }
                    else
                    {
                        this.form.SHIMEBI3.Enabled = false;
                    }
                    this.form.SHIMEBI3.SelectedIndex = -1;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_SHIMEBI3.IsNull)
                    {
                        this.form.SHIMEBI3.SelectedItem = this.entitysM_SYS_INFO.SEIKYUU_SHIMEBI3.ToString();
                    }
                    this.form.HICCHAKUBI.Enabled = true;
                    this.form.HICCHAKUBI.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_HICCHAKUBI.IsNull)
                    {
                        this.form.HICCHAKUBI.Text = this.entitysM_SYS_INFO.SEIKYUU_HICCHAKUBI.ToString();
                    }
                    //160026 S
                    //this.form.KAISHUU_MONTH.Enabled = true;
                    //this.form.KAISHUU_MONTH_1.Enabled = true;
                    //this.form.KAISHUU_MONTH_2.Enabled = true;
                    //this.form.KAISHUU_MONTH_3.Enabled = true;
                    //this.form.KAISHUU_MONTH_4.Enabled = true;
                    //this.form.KAISHUU_MONTH_5.Enabled = true;
                    //this.form.KAISHUU_MONTH_6.Enabled = true;
                    //this.form.KAISHUU_MONTH_7.Enabled = true;
                    //this.form.KAISHUU_MONTH.Text = string.Empty;
                    //if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_MONTH.IsNull)
                    //{
                    //    this.form.KAISHUU_MONTH.Text = this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_MONTH.ToString();
                    //}    
                    this.form.KAISHUU_BETSU_KBN.Enabled = true;
                    this.form.KAISHUU_BETSU_KBN_1.Enabled = true;
                    this.form.KAISHUU_BETSU_KBN_2.Enabled = true;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_BETSU_KBN.IsNull)
                    {
                        this.form.KAISHUU_BETSU_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_BETSU_KBN.ToString();
                    }
                    //160026 E
                    //DAT #162931 S
                    //this.form.KAISHUU_DAY.Enabled = true;
                    //this.form.KAISHUU_DAY.Text = string.Empty;
                    //if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_DAY.IsNull)
                    //{
                    //    this.form.KAISHUU_DAY.Text = this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_DAY.ToString();
                    //}
                    //DAT #162931 E
                    this.form.KAISHUU_HOUHOU.Enabled = true;
                    this.form.KAISHUU_HOUHOU_SEARCH.Enabled = true;
                    this.form.KAISHUU_HOUHOU.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_HOUHOU.IsNull)
                    {
                        this.form.KAISHUU_HOUHOU.Text = string.Format("{0:D" + this.form.KAISHUU_HOUHOU.CharactersNumber + "}", Int16.Parse(this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_HOUHOU.ToString()));
                    }
                    this.form.KAISHUU_HOUHOU_NAME.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_HOUHOU.IsNull)
                    {
                        this.entitysM_NYUUSHUKKIN_KBN = this.daoIM_NYUUSHUKKIN_KBN.GetDataByCd((int)this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_HOUHOU);
                        if (this.entitysM_NYUUSHUKKIN_KBN != null)
                        {
                            this.form.KAISHUU_HOUHOU_NAME.Text = this.entitysM_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME_RYAKU;
                        }
                    }
                    this.form.KAISHI_URIKAKE_ZANDAKA.Enabled = true;
                    this.form.KAISHI_URIKAKE_ZANDAKA.Text = "0";
                    this.form.SHOSHIKI_KBN.Enabled = true;
                    this.form.SHOSHIKI_KBN_1.Enabled = true;
                    this.form.SHOSHIKI_KBN_2.Enabled = true;
                    this.form.SHOSHIKI_KBN_3.Enabled = true;
                    this.form.SHOSHIKI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_SHOSHIKI_KBN.IsNull)
                    {
                        this.form.SHOSHIKI_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_SHOSHIKI_KBN.ToString();
                    }
                    if (this.form.SHOSHIKI_KBN.Text == "3")
                    {
                        this.form.SHOSHIKI_GENBA_KBN.Enabled = true;
                        this.form.SHOSHIKI_GENBA_KBN_1.Enabled = true;
                        this.form.SHOSHIKI_GENBA_KBN_2.Enabled = true;
                        if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_SHOSHIKI_GENBA_KBN.IsNull)
                        {
                            this.form.SHOSHIKI_GENBA_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_SHOSHIKI_GENBA_KBN.ToString();
                        }
                    }
                    else
                    {
                        this.form.SHOSHIKI_GENBA_KBN.Text = "";
                        this.form.SHOSHIKI_GENBA_KBN.Enabled = false;
                        this.form.SHOSHIKI_GENBA_KBN_1.Enabled = false;
                        this.form.SHOSHIKI_GENBA_KBN_2.Enabled = false;
                    }
                    this.form.SHOSHIKI_MEISAI_KBN.Enabled = true;
                    this.form.SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.SHOSHIKI_MEISAI_KBN_2.Enabled = true;
                    this.form.SHOSHIKI_MEISAI_KBN_3.Enabled = true;
                    this.form.SHOSHIKI_MEISAI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_SHOSHIKI_MEISAI_KBN.IsNull)
                    {
                        this.form.SHOSHIKI_MEISAI_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_SHOSHIKI_MEISAI_KBN.ToString();
                    }
                    this.form.SEIKYUU_KEITAI_KBN.Enabled = true;
                    this.form.SEIKYUU_KEITAI_KBN_1.Enabled = true;
                    this.form.SEIKYUU_KEITAI_KBN_2.Enabled = true;
                    this.form.SEIKYUU_KEITAI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KEITAI_KBN.IsNull)
                    {
                        this.form.SEIKYUU_KEITAI_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_KEITAI_KBN.ToString();
                    }
                    this.form.NYUUKIN_MEISAI_KBN.Enabled = true;
                    this.form.NYUUKIN_MEISAI_KBN_1.Enabled = true;
                    this.form.NYUUKIN_MEISAI_KBN_2.Enabled = true;
                    this.form.NYUUKIN_MEISAI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_NYUUKIN_MEISAI_KBN.IsNull)
                    {
                        this.form.NYUUKIN_MEISAI_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_NYUUKIN_MEISAI_KBN.ToString();
                    }
                    this.form.YOUSHI_KBN.Enabled = true;
                    this.form.YOUSHI_KBN_1.Enabled = true;
                    this.form.YOUSHI_KBN_2.Enabled = true;
                    this.form.YOUSHI_KBN_3.Enabled = true;
                    this.form.YOUSHI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_YOUSHI_KBN.IsNull)
                    {
                        this.form.YOUSHI_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_YOUSHI_KBN.ToString();
                    }
                    
                    //Begin: LANDUONG - 20220209 - refs#160050
                    this.form.pl_SEIKYUU_OUTPUT_KBN.Enabled = true;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_OUTPUT_KBN.IsNull)
                    {
                        this.form.OUTPUT_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_OUTPUT_KBN.ToString();
                    }
                    //End: LANDUONG - 20220209 - refs#160050

                    // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
                    if (SystemProperty.Shain.InxsTantouFlg)
                    {
                        this.form.INXS_SEIKYUU_KBN.Enabled = true;
                        this.form.INXS_SEIKYUU_KBN_1.Enabled = true;
                        this.form.INXS_SEIKYUU_KBN_2.Enabled = true;
                    }
                    else
                    {
                        this.form.INXS_SEIKYUU_KBN.Enabled = false;
                        this.form.INXS_SEIKYUU_KBN_1.Enabled = false;
                        this.form.INXS_SEIKYUU_KBN_2.Enabled = false;
                    }
                    this.form.INXS_SEIKYUU_KBN.Text = "2";
                    // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

                    //var characterLimitListIndex = Array.IndexOf(this.form.ZEI_KEISAN_KBN_CD.CharacterLimitList, '2');
                    //if (characterLimitListIndex < 0)
                    //{
                    //    List<char> list = new List<char>(this.form.ZEI_KEISAN_KBN_CD.CharacterLimitList);
                    //    list.Insert(0, '2');
                    //    this.form.ZEI_KEISAN_KBN_CD.CharacterLimitList = list.ToArray();
                    //}
                    this.form.ZEI_KEISAN_KBN_CD_2.Enabled = true;

                    this.form.FURIKOMI_BANK_CD.Enabled = true;
                    this.form.FURIKOMI_BANK_SEARCH.Enabled = true;
                    this.form.FURIKOMI_BANK_SHITEN_CD.Enabled = true;
                    this.form.FURIKOMI_BANK_SHITEN_SEARCH.Enabled = true;
                    this.form.FURIKOMI_BANK_CD_2.Enabled = true;
                    this.form.FURIKOMI_BANK_SEARCH_2.Enabled = true;
                    this.form.FURIKOMI_BANK_SHITEN_CD_2.Enabled = true;
                    this.form.FURIKOMI_BANK_SHITEN_SEARCH_2.Enabled = true;
                    this.form.FURIKOMI_BANK_CD_3.Enabled = true;
                    this.form.FURIKOMI_BANK_SEARCH_3.Enabled = true;
                    this.form.FURIKOMI_BANK_SHITEN_CD_3.Enabled = true;
                    this.form.FURIKOMI_BANK_SHITEN_SEARCH_3.Enabled = true;
                    this.form.SEIKYUU_JOUHOU1.Enabled = true;
                    this.form.SEIKYUU_JOUHOU2.Enabled = true;
                    this.form.TORIHIKISAKI_COPY_SEIKYU_BUTTON.Enabled = true;
                    this.form.SEIKYUU_SOUFU_NAME1.Enabled = true;
                    this.form.SEIKYUU_SOUFU_KEISHOU1.Enabled = true;
                    this.form.SEIKYUU_SOUFU_NAME2.Enabled = true;
                    this.form.SEIKYUU_SOUFU_KEISHOU2.Enabled = true;
                    this.form.SEIKYUU_SOUFU_POST.Enabled = true;
                    this.form.SEIKYUU_JUSHO_SEARCH.Enabled = true;
                    this.form.SEIKYUU_SOUFU_ADDRESS1.Enabled = true;
                    this.form.SEIKYUU_SOUFU_ADDRESS2.Enabled = true;
                    this.form.SEIKYUU_POST_SEARCH.Enabled = true;
                    this.form.SEIKYUU_SOUFU_BUSHO.Enabled = true;
                    this.form.SEIKYUU_SOUFU_TANTOU.Enabled = true;
                    this.form.SEIKYUU_SOUFU_TEL.Enabled = true;
                    this.form.SEIKYUU_SOUFU_FAX.Enabled = true;
                    if (this.form.NYUUKINSAKI_KBN.Text.Equals("2"))
                    {
                        this.form.NYUUKINSAKI_CD.Enabled = true;
                        this.form.NYUUKINSAKI_SEARCH.Enabled = true;
                    }
                    else
                    {
                        this.form.NYUUKINSAKI_CD.Enabled = false;
                        this.form.NYUUKINSAKI_CD.Text = string.Empty;
                        this.form.NYUUKINSAKI_NAME1.Text = string.Empty;
                        this.form.NYUUKINSAKI_NAME2.Text = string.Empty;
                        this.form.NYUUKINSAKI_SEARCH.Enabled = false;
                    }
                    this.form.SEIKYUU_TANTOU.Enabled = true;

                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Enabled = true;
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = "1";
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_DAIHYOU_PRINT_KBN.ToString();
                    }
                    if (this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text == "1")
                    {
                        this.form.SEIKYUU_DAIHYOU_PRINT_KBN_1.Checked = true;
                    }
                    else
                    {
                        this.form.SEIKYUU_DAIHYOU_PRINT_KBN_2.Checked = true;
                    }
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN_1.Enabled = true;
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN_2.Enabled = true;

                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled = true;
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = "1";
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_PRINT_KBN.ToString();
                    }
                    if (this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text == "1")
                    {
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN_1.Checked = true;
                    }
                    else
                    {
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN_2.Checked = true;
                    }
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN_1.Enabled = true;
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN_2.Enabled = true;

                    this.form.SEIKYUU_KYOTEN_CD.Enabled = true;
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_CD.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_CD.ToString()));
                    }

                    this.form.SEIKYUU_KYOTEN_NAME.Enabled = true;
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    M_KYOTEN seikyuuKyoten = this.daoIM_KYOTEN.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                    if (seikyuuKyoten != null)
                    {
                        this.form.SEIKYUU_KYOTEN_NAME.Text = seikyuuKyoten.KYOTEN_NAME_RYAKU;
                    }

                    this.form.SEIKYUU_KYOTEN_CD_SEARCH.Enabled = true;

                    this.form.FURIKOMI_NAME1.Enabled = true;
                    this.form.FURIKOMI_NAME2.Enabled = true;

                    bool catchErr = this.ChangeSeikyuuKyotenPrintKbn();
                    if (catchErr)
                    {
                        return true;
                    }

                    // [2.掛け]に変更した場合、以下を行う
                    // 請求書式明細区分の制限処理
                    // 請求税区分の制限処理
                    catchErr = this.LimitSeikyuuShoshikiMeisaiKbn();
                    if (catchErr)
                    {
                        return true;
                    }
                    catchErr = this.LimitSeikyuuZeiKbn();
                    if (catchErr)
                    {
                        return true;
                    }

                    if (isTextChanged)
                    {
                        // TextChagngedイベントからの呼び出し。(またはユーザ操作による呼び出し)
                        this.SetDefaultValueFromJisyaJyouhouToFURIKOMI_BANK();
                    }
                    else
                    {
                        // 画面初期化時の処理
                        //20150617 #3747 hoanghm start
                        //if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                        //if copy mod then don not set default value to FURIKOMI_BANK
                        if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG && string.IsNullOrEmpty(this.TorihikisakiCd))
                        {
                            //tab請求情報2の振込先銀行に初期値をセットする。
                            this.SetDefaultValueFromJisyaJyouhouToFURIKOMI_BANK();
                        }
                        //20150617 #3747 hoanghm end
                        else
                        {
                            this.form.FURIKOMI_BANK_CD.Text = string.Empty;
                            this.form.FURIKOMI_BANK_NAME.Text = string.Empty;
                            this.form.FURIKOMI_BANK_SHITEN_CD.Text = string.Empty;
                            this.form.FURIKOMI_BANK_SHITEN_NAME.Text = string.Empty;
                            this.form.KOUZA_SHURUI.Text = string.Empty;
                            this.form.KOUZA_NO.Text = string.Empty;
                            this.form.KOUZA_NAME.Text = string.Empty;
                            this.form.FURIKOMI_BANK_CD_2.Text = string.Empty;
                            this.form.FURIKOMI_BANK_NAME_2.Text = string.Empty;
                            this.form.FURIKOMI_BANK_SHITEN_CD_2.Text = string.Empty;
                            this.form.FURIKOMI_BANK_SHITEN_NAME_2.Text = string.Empty;
                            this.form.KOUZA_SHURUI_2.Text = string.Empty;
                            this.form.KOUZA_NO_2.Text = string.Empty;
                            this.form.KOUZA_NAME_2.Text = string.Empty;
                            this.form.FURIKOMI_BANK_CD_3.Text = string.Empty;
                            this.form.FURIKOMI_BANK_NAME_3.Text = string.Empty;
                            this.form.FURIKOMI_BANK_SHITEN_CD_3.Text = string.Empty;
                            this.form.FURIKOMI_BANK_SHITEN_NAME_3.Text = string.Empty;
                            this.form.KOUZA_SHURUI_3.Text = string.Empty;
                            this.form.KOUZA_NO_3.Text = string.Empty;
                            this.form.KOUZA_NAME_3.Text = string.Empty;
                        }
                    }

                    // 項目値を変更するとフォーカスが移動してしまうので戻す
                    this.form.TORIHIKI_KBN.Focus();
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("ChangeTorihikiKbn", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeTorihikiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 支払取引区分変更後処理
        /// </summary>
        public bool ChangeSiharaiTorihikiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.SHIHARAI_TORIHIKI_KBN_CD.Text.Equals("1"))
                {
                    this.form.SHIHARAI_SHIMEBI1.SelectedIndex = -1;
                    this.form.SHIHARAI_SHIMEBI2.SelectedIndex = -1;
                    this.form.SHIHARAI_SHIMEBI3.SelectedIndex = -1;
                    this.form.SHIHARAI_SHIMEBI1.Enabled = false;
                    this.form.SHIHARAI_SHIMEBI2.Enabled = false;
                    this.form.SHIHARAI_SHIMEBI3.Enabled = false;
                    //160026 S
                    this.form.SHIHARAI_BETSU_KBN.Text = string.Empty;
                    this.form.SHIHARAI_BETSU_KBN.Enabled = false;
                    this.form.SHIHARAI_BETSU_KBN_1.Enabled = false;
                    this.form.SHIHARAI_BETSU_KBN_2.Enabled = false;
                    this.form.SHIHARAI_BETSU_NICHIGO.Text = string.Empty;
                    this.form.SHIHARAI_BETSU_NICHIGO.Enabled = false;
                    //160026 E
                    this.form.SHIHARAI_MONTH.Text = string.Empty;
                    this.form.SHIHARAI_MONTH.Enabled = false;
                    this.form.SHIHARAI_MONTH_1.Enabled = false;
                    this.form.SHIHARAI_MONTH_2.Enabled = false;
                    this.form.SHIHARAI_MONTH_3.Enabled = false;
                    this.form.SHIHARAI_MONTH_4.Enabled = false;
                    this.form.SHIHARAI_MONTH_5.Enabled = false;
                    this.form.SHIHARAI_MONTH_6.Enabled = false;
                    this.form.SHIHARAI_MONTH_7.Enabled = false;
                    this.form.SHIHARAI_DAY.Text = string.Empty;
                    this.form.SHIHARAI_DAY.Enabled = false;
                    this.form.SHIHARAI_HOUHOU.Text = string.Empty;
                    this.form.SHIHARAI_HOUHOU.Enabled = false;
                    this.form.SHIHARAI_HOUHOU_SEARCH.Enabled = false;
                    this.form.SHIHARAI_HOUHOU_NAME.Text = string.Empty;
                    this.form.KAISHI_KAIKAKE_ZANDAKA.Text = string.Empty;
                    this.form.KAISHI_KAIKAKE_ZANDAKA.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_KBN.Text = string.Empty;
                    this.form.SHIHARAI_SHOSHIKI_KBN.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_KBN_1.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_KBN_2.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_KBN_3.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text = string.Empty;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_1.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_2.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_3.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Text = string.Empty;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_1.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_2.Enabled = false;
                    this.form.SHIHARAI_KEITAI_KBN.Text = string.Empty;
                    this.form.SHIHARAI_KEITAI_KBN.Enabled = false;
                    this.form.SHIHARAI_KEITAI_KBN_1.Enabled = false;
                    this.form.SHIHARAI_KEITAI_KBN_2.Enabled = false;
                    this.form.SHUKKIN_MEISAI_KBN.Text = string.Empty;
                    this.form.SHUKKIN_MEISAI_KBN.Enabled = false;
                    this.form.SHUKKIN_MEISAI_KBN_1.Enabled = false;
                    this.form.SHUKKIN_MEISAI_KBN_2.Enabled = false;
                    this.form.SHIHARAI_YOUSHI_KBN.Text = string.Empty;
                    this.form.SHIHARAI_YOUSHI_KBN.Enabled = false;
                    this.form.SHIHARAI_YOUSHI_KBN_1.Enabled = false;
                    this.form.SHIHARAI_YOUSHI_KBN_2.Enabled = false;
                    this.form.SHIHARAI_YOUSHI_KBN_3.Enabled = false;

                    // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
                    this.form.INXS_SHIHARAI_KBN.Text = "2";
                    this.form.INXS_SHIHARAI_KBN.Enabled = false;
                    this.form.INXS_SHIHARAI_KBN_1.Enabled = false;
                    this.form.INXS_SHIHARAI_KBN_2.Enabled = false;
                    // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

                    this.form.SHIHARAI_ZEI_KEISAN_KBN_CD_2.Enabled = false;
                    if (TorihikisakiHoshuConstans.ZEI_KEISAN_KBN_SEIKYUU.ToString().Equals(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text))
                    {
                        this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = TorihikisakiHoshuConstans.ZEI_KEISAN_KBN_DENPYOU.ToString();
                    }

                    this.form.SHIHARAI_JOUHOU1.Enabled = false;
                    this.form.SHIHARAI_JOUHOU1.Text = string.Empty;
                    this.form.SHIHARAI_JOUHOU2.Enabled = false;
                    this.form.SHIHARAI_JOUHOU2.Text = string.Empty;
                    this.form.TORIHIKISAKI_COPY_SIHARAI_BUTTON.Enabled = false;
                    this.form.SHIHARAI_SOUFU_NAME1.Enabled = false;
                    this.form.SHIHARAI_SOUFU_NAME1.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_KEISHOU1.Enabled = false;
                    this.form.SHIHARAI_SOUFU_KEISHOU1.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_NAME2.Enabled = false;
                    this.form.SHIHARAI_SOUFU_NAME2.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_KEISHOU2.Enabled = false;
                    this.form.SHIHARAI_SOUFU_KEISHOU2.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_POST.Enabled = false;
                    this.form.SHIHARAI_SOUFU_POST.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_ADDRESS_SEARCH.Enabled = false;
                    this.form.SHIHARAI_SOUFU_ADDRESS1.Enabled = false;
                    this.form.SHIHARAI_SOUFU_ADDRESS1.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_ADDRESS2.Enabled = false;
                    this.form.SHIHARAI_SOUFU_ADDRESS2.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_POST_SEARCH.Enabled = false;
                    this.form.SHIHARAI_SOUFU_BUSHO.Enabled = false;
                    this.form.SHIHARAI_SOUFU_BUSHO.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_TANTOU.Enabled = false;
                    this.form.SHIHARAI_SOUFU_TANTOU.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_TEL.Enabled = false;
                    this.form.SHIHARAI_SOUFU_TEL.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_FAX.Enabled = false;
                    this.form.SHIHARAI_SOUFU_FAX.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled = false;
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN_1.Enabled = false;
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN_2.Enabled = false;
                    this.form.SHIHARAI_KYOTEN_CD.Enabled = false;
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_NAME.Enabled = false;
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_CD_SEARCH.Enabled = false;
                    //160026 S
                    this.form.FURIKOMI_EXPORT_KBN.Enabled = false;
                    this.form.FURIKOMI_EXPORT_KBN.Text = string.Empty;
                    this.form.FURIKOMI_EXPORT_KBN_1.Enabled = false;
                    this.form.FURIKOMI_EXPORT_KBN_2.Enabled = false;

                    this.form.FURIKOMI_SAKI_BANK_CD.Enabled = false;
                    this.form.FURIKOMI_SAKI_BANK_NAME.Enabled = false;
                    this.form.FURIKOMI_SAKI_BANK_SHITEN_CD.Enabled = false;
                    this.form.FURIKOMI_SAKI_BANK_SHITEN_NAME.Enabled = false;
                    this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Enabled = false;
                    this.form.FURIKOMI_SAKI_BANK_KOUZA_NO.Enabled = false;
                    this.form.FURIKOMI_SAKI_BANK_KOUZA_NAME.Enabled = false;
                    this.form.FURIKOMI_SAKI_BANK_CD.Text = string.Empty;
                    this.form.FURIKOMI_SAKI_BANK_NAME.Text = string.Empty;
                    this.form.FURIKOMI_SAKI_BANK_SHITEN_CD.Text = string.Empty;
                    this.form.FURIKOMI_SAKI_BANK_SHITEN_NAME.Text = string.Empty;
                    this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Text = string.Empty;
                    this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI.Text = string.Empty;
                    this.form.FURIKOMI_SAKI_BANK_KOUZA_NO.Text = string.Empty;
                    this.form.FURIKOMI_SAKI_BANK_KOUZA_NAME.Text = string.Empty;
                    this.form.TEI_SUU_RYOU_KBN.Enabled = false;
                    this.form.TEI_SUU_RYOU_KBN.Text = string.Empty;
                    this.form.TEI_SUU_RYOU_KBN_1.Enabled = false;
                    this.form.TEI_SUU_RYOU_KBN_2.Enabled = false;

                    this.form.FURI_KOMI_MOTO_BANK_CD.Enabled = false;
                    this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Enabled = false;
                    this.form.FURI_KOMI_MOTO_BANK_SHITTEN_POPUP.Enabled = false;
                    this.form.FURI_KOMI_MOTO_BANK_POPUP.Enabled = false;
                    this.form.FURI_KOMI_MOTO_BANK_CD.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_BANK_NAME.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_SHURUI.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_NO.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_NAME.Text = string.Empty;
                    //160026 E
                }
                else
                {
                    this.form.SHIHARAI_SHIMEBI1.Enabled = true;
                    this.form.SHIHARAI_SHIMEBI2.Enabled = true;
                    this.form.SHIHARAI_SHIMEBI3.Enabled = true;
                    this.form.SHIHARAI_SHIMEBI1.SelectedIndex = -1;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_SHIMEBI1.IsNull)
                    {
                        this.form.SHIHARAI_SHIMEBI1.SelectedItem = this.entitysM_SYS_INFO.SHIHARAI_SHIMEBI1.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_SHIMEBI2.Enabled = false;
                        this.form.SHIHARAI_SHIMEBI3.Enabled = false;
                    }
                    this.form.SHIHARAI_SHIMEBI2.SelectedIndex = -1;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_SHIMEBI2.IsNull)
                    {
                        this.form.SHIHARAI_SHIMEBI2.SelectedItem = this.entitysM_SYS_INFO.SHIHARAI_SHIMEBI2.ToString();
                        this.form.SHIHARAI_SHIMEBI3.Enabled = true;
                    }
                    else
                    {
                        this.form.SHIHARAI_SHIMEBI3.Enabled = false;
                    }
                    this.form.SHIHARAI_SHIMEBI3.SelectedIndex = -1;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_SHIMEBI3.IsNull)
                    {
                        this.form.SHIHARAI_SHIMEBI3.SelectedItem = this.entitysM_SYS_INFO.SHIHARAI_SHIMEBI3.ToString();
                    }
                    //160026 S
                    this.form.SHIHARAI_BETSU_KBN.Text = string.Empty;
                    this.form.SHIHARAI_BETSU_KBN.Enabled = true;
                    this.form.SHIHARAI_BETSU_KBN_1.Enabled = true;
                    this.form.SHIHARAI_BETSU_KBN_2.Enabled = true;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_KAISHUU_BETSU_KBN.IsNull)
                    {
                        this.form.SHIHARAI_BETSU_KBN.Text = this.entitysM_SYS_INFO.SHIHARAI_KAISHUU_BETSU_KBN.ToString();
                    }
                    //this.form.SHIHARAI_MONTH.Enabled = true;
                    //this.form.SHIHARAI_MONTH_1.Enabled = true;
                    //this.form.SHIHARAI_MONTH_2.Enabled = true;
                    //this.form.SHIHARAI_MONTH_3.Enabled = true;
                    //this.form.SHIHARAI_MONTH_4.Enabled = true;
                    //this.form.SHIHARAI_MONTH_5.Enabled = true;
                    //this.form.SHIHARAI_MONTH_6.Enabled = true;
                    //this.form.SHIHARAI_MONTH_7.Enabled = true;
                    //this.form.SHIHARAI_MONTH.Text = string.Empty;
                    //if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_MONTH.IsNull)
                    //{
                    //    this.form.SHIHARAI_MONTH.Text = this.entitysM_SYS_INFO.SHIHARAI_MONTH.ToString();
                    //}
                    //160026 E
                    //DAT #162931 S
                    //this.form.SHIHARAI_DAY.Enabled = true;
                    //this.form.SHIHARAI_DAY.Text = string.Empty;
                    //if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_DAY.IsNull)
                    //{
                    //    this.form.SHIHARAI_DAY.Text = this.entitysM_SYS_INFO.SHIHARAI_DAY.ToString();
                    //}
                    //DAT #162931 E
                    this.form.SHIHARAI_HOUHOU.Enabled = true;
                    this.form.SHIHARAI_HOUHOU_SEARCH.Enabled = true;
                    this.form.SHIHARAI_HOUHOU.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_HOUHOU.IsNull)
                    {
                        this.form.SHIHARAI_HOUHOU.Text = string.Format("{0:D" + this.form.SHIHARAI_HOUHOU.CharactersNumber + "}", Int16.Parse(this.entitysM_SYS_INFO.SHIHARAI_HOUHOU.ToString()));
                    }
                    this.form.SHIHARAI_HOUHOU_NAME.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_HOUHOU.IsNull)
                    {
                        this.entitysM_NYUUSHUKKIN_KBN = this.daoIM_NYUUSHUKKIN_KBN.GetDataByCd((int)this.entitysM_SYS_INFO.SHIHARAI_HOUHOU);
                        if (this.entitysM_NYUUSHUKKIN_KBN != null)
                        {
                            this.form.SHIHARAI_HOUHOU_NAME.Text = this.entitysM_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME_RYAKU;
                        }
                    }
                    this.form.KAISHI_KAIKAKE_ZANDAKA.Enabled = true;
                    this.form.KAISHI_KAIKAKE_ZANDAKA.Text = "0";
                    this.form.SHIHARAI_SHOSHIKI_KBN.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_KBN_1.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_KBN_2.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_KBN_3.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_SHOSHIKI_KBN.IsNull)
                    {
                        this.form.SHIHARAI_SHOSHIKI_KBN.Text = this.entitysM_SYS_INFO.SHIHARAI_SHOSHIKI_KBN.ToString();
                    }
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_2.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_3.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_SHOSHIKI_MEISAI_KBN.IsNull)
                    {
                        this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text = this.entitysM_SYS_INFO.SHIHARAI_SHOSHIKI_MEISAI_KBN.ToString();
                    }
                    if (this.form.SHIHARAI_SHOSHIKI_KBN.Text == "3")
                    {
                        this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Enabled = true;
                        this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_1.Enabled = true;
                        this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_2.Enabled = true;
                        if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_SHOSHIKI_GENBA_KBN.IsNull)
                        {
                            this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Text = this.entitysM_SYS_INFO.SHIHARAI_SHOSHIKI_GENBA_KBN.ToString();
                        }
                    }
                    else
                    {
                        this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Text = "";
                        this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Enabled = false;
                        this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_1.Enabled = false;
                        this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_2.Enabled = false;
                    }

                    this.form.SHIHARAI_KEITAI_KBN.Enabled = true;
                    this.form.SHIHARAI_KEITAI_KBN_1.Enabled = true;
                    this.form.SHIHARAI_KEITAI_KBN_2.Enabled = true;
                    this.form.SHIHARAI_KEITAI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_KEITAI_KBN.IsNull)
                    {
                        this.form.SHIHARAI_KEITAI_KBN.Text = this.entitysM_SYS_INFO.SHIHARAI_KEITAI_KBN.ToString();
                    }
                    this.form.SHUKKIN_MEISAI_KBN.Enabled = true;
                    this.form.SHUKKIN_MEISAI_KBN_1.Enabled = true;
                    this.form.SHUKKIN_MEISAI_KBN_2.Enabled = true;
                    this.form.SHUKKIN_MEISAI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_SHUKKIN_MEISAI_KBN.IsNull)
                    {
                        this.form.SHUKKIN_MEISAI_KBN.Text = this.entitysM_SYS_INFO.SHIHARAI_SHUKKIN_MEISAI_KBN.ToString();
                    }
                    this.form.SHIHARAI_YOUSHI_KBN.Enabled = true;
                    this.form.SHIHARAI_YOUSHI_KBN_1.Enabled = true;
                    this.form.SHIHARAI_YOUSHI_KBN_2.Enabled = true;
                    this.form.SHIHARAI_YOUSHI_KBN_3.Enabled = true;
                    this.form.SHIHARAI_YOUSHI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_YOUSHI_KBN.IsNull)
                    {
                        this.form.SHIHARAI_YOUSHI_KBN.Text = this.entitysM_SYS_INFO.SHIHARAI_YOUSHI_KBN.ToString();
                    }
                    //var characterLimitListIndex = Array.IndexOf(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.CharacterLimitList, '2');
                    //if (characterLimitListIndex < 0)
                    //{
                    //    List<char> list = new List<char>(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.CharacterLimitList);
                    //    list.Insert(0, '2');
                    //    this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.CharacterLimitList = list.ToArray();
                    //}
                    this.form.SHIHARAI_ZEI_KEISAN_KBN_CD_2.Enabled = true;

                    // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
                    if (SystemProperty.Shain.InxsTantouFlg)
                    {
                        this.form.INXS_SHIHARAI_KBN.Enabled = true;
                        this.form.INXS_SHIHARAI_KBN_1.Enabled = true;
                        this.form.INXS_SHIHARAI_KBN_2.Enabled = true;
                    }
                    else
                    {
                        this.form.INXS_SHIHARAI_KBN.Enabled = false;
                        this.form.INXS_SHIHARAI_KBN_1.Enabled = false;
                        this.form.INXS_SHIHARAI_KBN_2.Enabled = false;
                    }
                    this.form.INXS_SHIHARAI_KBN.Text = "2";
                    // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

                    this.form.SHIHARAI_JOUHOU1.Enabled = true;
                    this.form.SHIHARAI_JOUHOU2.Enabled = true;
                    this.form.TORIHIKISAKI_COPY_SIHARAI_BUTTON.Enabled = true;
                    this.form.SHIHARAI_SOUFU_NAME1.Enabled = true;
                    this.form.SHIHARAI_SOUFU_KEISHOU1.Enabled = true;
                    this.form.SHIHARAI_SOUFU_NAME2.Enabled = true;
                    this.form.SHIHARAI_SOUFU_KEISHOU2.Enabled = true;
                    this.form.SHIHARAI_SOUFU_POST.Enabled = true;
                    this.form.SHIHARAI_SOUFU_ADDRESS_SEARCH.Enabled = true;
                    this.form.SHIHARAI_SOUFU_ADDRESS1.Enabled = true;
                    this.form.SHIHARAI_SOUFU_ADDRESS2.Enabled = true;
                    this.form.SHIHARAI_SOUFU_POST_SEARCH.Enabled = true;
                    this.form.SHIHARAI_SOUFU_BUSHO.Enabled = true;
                    this.form.SHIHARAI_SOUFU_TANTOU.Enabled = true;
                    this.form.SHIHARAI_SOUFU_TEL.Enabled = true;
                    this.form.SHIHARAI_SOUFU_FAX.Enabled = true;

                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled = true;
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = "1";
                    if (!this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_PRINT_KBN.ToString();
                    }
                    if (this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text == "1")
                    {
                        this.form.SHIHARAI_KYOTEN_PRINT_KBN_1.Checked = true;
                    }
                    else
                    {
                        this.form.SHIHARAI_KYOTEN_PRINT_KBN_2.Checked = true;
                    }
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN_1.Enabled = true;
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN_2.Enabled = true;
                    //160026 S
                    //#162933 S
                    this.setPaymentOnlineBankVisible();
                    //{
                    //    this.form.FURIKOMI_EXPORT_KBN.Enabled = true;
                    //    this.form.FURIKOMI_EXPORT_KBN.Text = "2";
                    //    this.form.FURIKOMI_EXPORT_KBN_2.Checked = true;
                    //    this.form.FURIKOMI_EXPORT_KBN_1.Enabled = true;
                    //    this.form.FURIKOMI_EXPORT_KBN_2.Enabled = true;
                    //}
                    //#162933 E

                    this.form.FURIKOMI_EXPORT_KBN_TextChanged(null, null);
                    //160026 E
                    this.form.SHIHARAI_KYOTEN_CD.Enabled = true;
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_CD.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_CD.ToString()));
                    }

                    this.form.SHIHARAI_KYOTEN_NAME.Enabled = true;
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    M_KYOTEN shiharaiKyoten = this.daoIM_KYOTEN.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                    if (shiharaiKyoten != null)
                    {
                        this.form.SHIHARAI_KYOTEN_NAME.Text = shiharaiKyoten.KYOTEN_NAME_RYAKU;
                    }

                    this.form.SHIHARAI_KYOTEN_CD_SEARCH.Enabled = true;

                    bool catchErr = this.ChangeShiharaiKyotenPrintKbn();
                    if (catchErr)
                    {
                        return true;
                    }

                    // [2.掛け]に変更した場合、以下を行う
                    // 支払書式明細区分の制限処理
                    // 支払税区分の制限処理
                    catchErr = this.LimitShiharaiShoshikiMeisaiKbn();
                    if (catchErr)
                    {
                        return true;
                    }
                    catchErr = this.LimitShiharaiZeiKbn();
                    if (catchErr)
                    {
                        return true;
                    }

                    // 項目値を変更するとフォーカスが移動してしまうので戻す
                    this.form.SHIHARAI_TORIHIKI_KBN_CD.Focus();
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("ChangeSiharaiTorihikiKbn", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeSiharaiTorihikiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 残高項目のフォーマット処理
        /// </summary>
        public bool SetZandakaFormat(string zan, CustomNumericTextBox2 target)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(zan))
                {
                    return false;
                }

                // マイナスが先頭以外に付与されていたら削除する
                var minus = zan.StartsWith("-");
                zan = (minus ? "-" : string.Empty) + zan.Replace("-", string.Empty).Replace(",", string.Empty);

                try
                {
                    target.Text = string.Format("{0:#,##0}", Decimal.Parse(zan));
                }
                catch
                {
                    target.Text = string.Empty;
                }
                return false;
            }
            catch (Exception ex)
            {
                target.Text = string.Empty;
                LogUtility.Error("SetZandakaFormat", ex);
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
                if (this.form.MANI_HENSOUSAKI_KBN.Checked)
                {
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
                        this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = true;
                        this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = true;
                        this.form.MANI_HENSOUSAKI_BUSHO.Enabled = true;
                        this.form.MANI_HENSOUSAKI_TANTOU.Enabled = true;
                        this.form.MANI_HENSOUSAKI_ADDRESS_SEARCH.Enabled = true;
                        this.form.MANI_HENSOUSAKI_POST_SEARCH.Enabled = true;
                        this.form.TORIHIKISAKI_COPY_MANI_BUTTON.Enabled = true;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(this.TorihikisakiCd))
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
                    this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = false;
                    this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = false;
                    this.form.MANI_HENSOUSAKI_BUSHO.Enabled = false;
                    this.form.MANI_HENSOUSAKI_TANTOU.Enabled = false;
                    this.form.MANI_HENSOUSAKI_ADDRESS_SEARCH.Enabled = false;
                    this.form.MANI_HENSOUSAKI_POST_SEARCH.Enabled = false;
                    this.form.TORIHIKISAKI_COPY_MANI_BUTTON.Enabled = false;
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
                LogUtility.DebugMethodEnd();
                return true;
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

                r_framework.FormManager.FormManager.OpenFormWithAuth("M214", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DENSHU_KBN.TORIHIKISAKI);

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 銀行情報設定処理
        /// </summary>
        public void SetBankInfo()
        {
            if (string.IsNullOrWhiteSpace(this.form.FURIKOMI_BANK_CD.Text) && !string.IsNullOrWhiteSpace(this.form.FURIKOMI_BANK_SHITEN_CD.Text))
            {
                M_BANK_SHITEN searchParams = new M_BANK_SHITEN();
                searchParams.BANK_SHITEN_CD = this.form.FURIKOMI_BANK_SHITEN_CD.Text.PadLeft((int)this.form.FURIKOMI_BANK_SHITEN_CD.CharactersNumber, '0');
                M_BANK_SHITEN[] bankShiten = this.daoIM_BANK_SHITEN.GetAllValidData(searchParams);
                if (bankShiten != null && bankShiten.Length == 1)
                {
                    M_BANK bank = this.daoIM_BANK.GetDataByCd(bankShiten[0].BANK_CD);
                    if (bank != null)
                    {
                        this.form.FURIKOMI_BANK_CD.Text = bank.BANK_CD;
                        this.form.FURIKOMI_BANK_NAME.Text = bank.BANK_NAME_RYAKU;
                    }
                }
            }
        }

        /// <summary>
        /// 業者一覧検索
        /// </summary>
        public bool TorihikiStopIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.rowCntGyousha = this.SearchGyousha();
                if (this.rowCntGyousha == 0)
                {
                    this.form.GYOUSHA_ICHIRAN.DataSource = null;
                    this.form.GYOUSHA_ICHIRAN.Rows.Clear();
                    return false;
                }

                this.SetIchiranGyousha();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TorihikiStopIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// データ取得処理(業者)
        /// </summary>
        /// <returns></returns>
        public int SearchGyousha()
        {
            LogUtility.DebugMethodStart();

            this.SearchResultGyousha = new DataTable();

            string torihikisakiCd = this.entitysTORIHIKISAKI.TORIHIKISAKI_CD;
            if (string.IsNullOrWhiteSpace(torihikisakiCd))
            {
                return 0;
            }

            M_GYOUSHA condition = new M_GYOUSHA();
            condition.TORIHIKISAKI_CD = torihikisakiCd;

            if (this.IsFromShouninzumiDenshiShinseiIchiran)
            {
                // 何もしない(本登録時は引合業者の情報は表示しない)
            }
            else
            {
                this.SearchResultGyousha = this.daoIM_GYOUSHA.GetDataBySqlFile(this.GET_ICHIRAN_GYOUSHA_DATA_SQL, condition);
            }

            int count = this.SearchResultGyousha.Rows.Count;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// 検索結果を業者一覧に設定
        /// </summary>
        internal void SetIchiranGyousha()
        {
            this.form.GYOUSHA_ICHIRAN.IsBrowsePurpose = false;
            var table = this.SearchResultGyousha;
            table.BeginLoadData();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].ReadOnly = false;
            }
            this.form.GYOUSHA_ICHIRAN.DataSource = table;
            this.form.GYOUSHA_ICHIRAN.IsBrowsePurpose = true;
        }

        /// <summary>
        /// 現場入力画面の呼び出し
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="newFlg"></param>
        internal bool ShowGyoushaWindow()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //選択行からキー項目を取得する
                string cd1 = string.Empty;
                foreach (Row row in this.form.GYOUSHA_ICHIRAN.Rows)
                {
                    if (row.Selected)
                    {
                        cd1 = row.Cells["GYOUSHA_CD"].Value.ToString();
                        break;
                    }
                }

                //現場入力画面を表示する
                if (r_framework.Authority.Manager.CheckAuthority("M215", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    r_framework.FormManager.FormManager.OpenFormWithAuth("M215", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, cd1);
                }
                else if (r_framework.Authority.Manager.CheckAuthority("M215", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    r_framework.FormManager.FormManager.OpenFormWithAuth("M215", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, cd1);
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E158", "修正");
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowGyoushaWindow", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 請求書式明細区分の制限処理
        /// </summary>
        internal bool LimitSeikyuuShoshikiMeisaiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (TorihikisakiHoshuConstans.SHOSHIKI_KBN_GYOUSHA.ToString().Equals(this.form.SHOSHIKI_KBN.Text))
                {
                    // 書式区分：業者別
                    // 入力制限
                    //this.form.SHOSHIKI_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_NONE,
                    //        TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_GENBA };
                    //this.form.SEIKYUU_KEITAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU };
                    //this.form.NYUUKIN_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI };
                    this.form.SEIKYUU_KEITAI_KBN.Text = TorihikisakiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU.ToString();
                    this.form.NYUUKIN_MEISAI_KBN.Text = TorihikisakiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI.ToString();
                    // チェック制限
                    this.form.SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.SHOSHIKI_MEISAI_KBN_2.Enabled = false;
                    this.form.SHOSHIKI_MEISAI_KBN_3.Enabled = true;
                    this.form.SEIKYUU_KEITAI_KBN_2.Enabled = false;
                    this.form.NYUUKIN_MEISAI_KBN_1.Enabled = false;

                    if (TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_GYOUSHA.ToString().Equals(this.form.SHOSHIKI_MEISAI_KBN.Text))
                    {
                        this.form.SHOSHIKI_MEISAI_KBN.Text = TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_NONE.ToString();
                    }

                    this.form.SHOSHIKI_GENBA_KBN.Text = string.Empty;
                    this.form.SHOSHIKI_GENBA_KBN.Enabled = false;
                    this.form.SHOSHIKI_GENBA_KBN_1.Enabled = false;
                    this.form.SHOSHIKI_GENBA_KBN_2.Enabled = false;
                }
                else if (TorihikisakiHoshuConstans.SHOSHIKI_KBN_GENBA.ToString().Equals(this.form.SHOSHIKI_KBN.Text))
                {
                    // 書式区分：現場別
                    // 入力制限
                    //this.form.SHOSHIKI_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_NONE };
                    //this.form.SEIKYUU_KEITAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU };
                    //this.form.NYUUKIN_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI };
                    this.form.SEIKYUU_KEITAI_KBN.Text = TorihikisakiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU.ToString();
                    this.form.NYUUKIN_MEISAI_KBN.Text = TorihikisakiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI.ToString();
                    // チェック制限
                    this.form.SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.SHOSHIKI_MEISAI_KBN_2.Enabled = false;
                    this.form.SHOSHIKI_MEISAI_KBN_3.Enabled = false;
                    this.form.SEIKYUU_KEITAI_KBN_2.Enabled = false;
                    this.form.NYUUKIN_MEISAI_KBN_1.Enabled = false;

                    if (TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_GYOUSHA.ToString().Equals(this.form.SHOSHIKI_MEISAI_KBN.Text)
                        || TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_GENBA.ToString().Equals(this.form.SHOSHIKI_MEISAI_KBN.Text))
                    {
                        this.form.SHOSHIKI_MEISAI_KBN.Text = TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_NONE.ToString();
                    }

                    this.form.SHOSHIKI_GENBA_KBN.Enabled = true;
                    this.form.SHOSHIKI_GENBA_KBN_1.Enabled = true;
                    this.form.SHOSHIKI_GENBA_KBN_2.Enabled = true;
                    if (string.IsNullOrEmpty(this.form.SHOSHIKI_GENBA_KBN.Text))
                    {
                        this.form.SHOSHIKI_GENBA_KBN.Text = "1";
                    }
                }
                else
                {
                    // その他
                    //// 入力制限
                    //this.form.SHOSHIKI_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_NONE,
                    //        TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_GYOUSHA,
                    //        TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_GENBA };
                    //this.form.SEIKYUU_KEITAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU,
                    //        TorihikisakiHoshuConstans.SEIKYU_KEITAI_KBN_KURIKOSI};
                    //this.form.NYUUKIN_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HYOUJI,
                    //        TorihikisakiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI};
                    // チェック制限
                    this.form.SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.SHOSHIKI_MEISAI_KBN_2.Enabled = true;
                    this.form.SHOSHIKI_MEISAI_KBN_3.Enabled = true;
                    this.form.SEIKYUU_KEITAI_KBN_2.Enabled = true;
                    this.form.NYUUKIN_MEISAI_KBN_1.Enabled = true;
                    this.form.SHOSHIKI_GENBA_KBN.Text = string.Empty;
                    this.form.SHOSHIKI_GENBA_KBN.Enabled = false;
                    this.form.SHOSHIKI_GENBA_KBN_1.Enabled = false;
                    this.form.SHOSHIKI_GENBA_KBN_2.Enabled = false;
                }

                // 許容されていない入力の場合、テキストをクリアする
                TorihikisakiHoshuLogic.ClearText_NotAllowedInput(this.form.SHOSHIKI_MEISAI_KBN);

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LimitSeikyuuShoshikiMeisaiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 支払書式明細区分の制限処理
        /// </summary>
        internal bool LimitShiharaiShoshikiMeisaiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (TorihikisakiHoshuConstans.SHOSHIKI_KBN_GYOUSHA.ToString().Equals(this.form.SHIHARAI_SHOSHIKI_KBN.Text))
                {
                    // 書式区分：業者別
                    // 入力制限
                    //this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_NONE,
                    //        TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_GENBA };
                    //this.form.SHIHARAI_KEITAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU };
                    //this.form.SHUKKIN_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI };
                    this.form.SHIHARAI_KEITAI_KBN.Text = TorihikisakiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU.ToString();
                    this.form.SHUKKIN_MEISAI_KBN.Text = TorihikisakiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI.ToString();
                    // チェック制限
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_2.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_3.Enabled = true;
                    this.form.SHIHARAI_KEITAI_KBN_2.Enabled = false;
                    this.form.SHUKKIN_MEISAI_KBN_1.Enabled = false;

                    if (TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_GYOUSHA.ToString().Equals(this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text))
                    {
                        this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text = TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_NONE.ToString();
                    }
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Text = string.Empty;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_1.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_2.Enabled = false;
                }
                else if (TorihikisakiHoshuConstans.SHOSHIKI_KBN_GENBA.ToString().Equals(this.form.SHIHARAI_SHOSHIKI_KBN.Text))
                {
                    // 書式区分：現場別
                    // 入力制限
                    //this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_NONE };
                    //this.form.SHIHARAI_KEITAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU };
                    //this.form.SHUKKIN_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI };
                    this.form.SHIHARAI_KEITAI_KBN.Text = TorihikisakiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU.ToString();
                    this.form.SHUKKIN_MEISAI_KBN.Text = TorihikisakiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI.ToString();
                    // チェック制限
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_2.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_3.Enabled = false;
                    this.form.SHIHARAI_KEITAI_KBN_2.Enabled = false;
                    this.form.SHUKKIN_MEISAI_KBN_1.Enabled = false;

                    if (TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_GYOUSHA.ToString().Equals(this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text)
                        || TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_GENBA.ToString().Equals(this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text))
                    {
                        this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text = TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_NONE.ToString();
                    }
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_1.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_2.Enabled = true;
                    if (string.IsNullOrEmpty(this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Text))
                    {
                        this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Text = "1";
                    }
                }
                else
                {
                    // その他
                    // 入力制限
                    //this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_NONE,
                    //        TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_GYOUSHA,
                    //        TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_GENBA };
                    //this.form.SHIHARAI_KEITAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU,
                    //        TorihikisakiHoshuConstans.SEIKYU_KEITAI_KBN_KURIKOSI};
                    //this.form.SHUKKIN_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HYOUJI,
                    //        TorihikisakiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI};
                    // チェック制限
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_2.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_3.Enabled = true;
                    this.form.SHIHARAI_KEITAI_KBN_2.Enabled = true;
                    this.form.SHUKKIN_MEISAI_KBN_1.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Text = string.Empty;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_1.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_2.Enabled = false;
                }

                // 許容されていない入力の場合、テキストをクリアする
                TorihikisakiHoshuLogic.ClearText_NotAllowedInput(this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN);

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LimitShiharaiShoshikiMeisaiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 請求税区分の制限処理
        /// </summary>
        internal bool LimitSeikyuuZeiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (TorihikisakiHoshuConstans.ZEI_KEISAN_KBN_DENPYOU.ToString().Equals(this.form.ZEI_KEISAN_KBN_CD.Text) ||
                    TorihikisakiHoshuConstans.ZEI_KEISAN_KBN_SEIKYUU.ToString().Equals(this.form.ZEI_KEISAN_KBN_CD.Text))
                {
                    // 税計算区分：伝票毎、請求毎
                    //// 入力制限
                    //this.form.ZEI_KBN_CD.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.ZEI_KBN_SOTO,
                    //        TorihikisakiHoshuConstans.ZEI_KBN_NONE };
                    // チェック制限
                    if (this.form.ZEI_KBN_CD_2.Checked)
                    {
                        this.form.ZEI_KBN_CD_1.Checked = true;
                    }
                    this.form.ZEI_KBN_CD_1.Enabled = true;
                    this.form.ZEI_KBN_CD_2.Enabled = false;
                    this.form.ZEI_KBN_CD_3.Enabled = true;
                }
                else
                {
                    // その他
                    //// 入力制限
                    //this.form.ZEI_KBN_CD.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.ZEI_KBN_SOTO,
                    //        TorihikisakiHoshuConstans.ZEI_KBN_UCHI,
                    //        TorihikisakiHoshuConstans.ZEI_KBN_NONE };
                    // チェック制限
                    this.form.ZEI_KBN_CD_1.Enabled = true;
                    this.form.ZEI_KBN_CD_2.Enabled = true;
                    this.form.ZEI_KBN_CD_3.Enabled = true;
                }

                // 許容されていない入力の場合、テキストをクリアする
                TorihikisakiHoshuLogic.ClearText_NotAllowedInput(this.form.ZEI_KBN_CD);

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LimitSeikyuuZeiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 支払税区分の制限処理
        /// </summary>
        internal bool LimitShiharaiZeiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (TorihikisakiHoshuConstans.ZEI_KEISAN_KBN_DENPYOU.ToString().Equals(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text) ||
                    TorihikisakiHoshuConstans.ZEI_KEISAN_KBN_SEIKYUU.ToString().Equals(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text))
                {
                    // 税計算区分：伝票毎、請求毎
                    //// 入力制限
                    //this.form.SHIHARAI_ZEI_KBN_CD.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.ZEI_KBN_SOTO,
                    //        TorihikisakiHoshuConstans.ZEI_KBN_NONE };
                    // チェック制限
                    if (this.form.SHIHARAI_ZEI_KBN_CD_2.Checked)
                    {
                        this.form.SHIHARAI_ZEI_KBN_CD_1.Checked = true;
                    }
                    this.form.SHIHARAI_ZEI_KBN_CD_1.Enabled = true;
                    this.form.SHIHARAI_ZEI_KBN_CD_2.Enabled = false;
                    this.form.SHIHARAI_ZEI_KBN_CD_3.Enabled = true;
                }
                else
                {
                    // その他
                    //// 入力制限
                    //this.form.SHIHARAI_ZEI_KBN_CD.CharacterLimitList =
                    //    new char[] {
                    //        TorihikisakiHoshuConstans.ZEI_KBN_SOTO,
                    //        TorihikisakiHoshuConstans.ZEI_KBN_UCHI,
                    //        TorihikisakiHoshuConstans.ZEI_KBN_NONE };
                    // チェック制限
                    this.form.SHIHARAI_ZEI_KBN_CD_1.Enabled = true;
                    this.form.SHIHARAI_ZEI_KBN_CD_2.Enabled = true;
                    this.form.SHIHARAI_ZEI_KBN_CD_3.Enabled = true;
                }

                // 許容されていない入力の場合、テキストをクリアする
                TorihikisakiHoshuLogic.ClearText_NotAllowedInput(this.form.SHIHARAI_ZEI_KBN_CD);

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LimitShiharaiZeiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 許容されていない入力の場合、テキストをクリアする
        /// </summary>
        /// <param name="textBox">数値入力テキストボックス</param>
        /// <param name="limitList">制限文字リスト</param>
        internal static void ClearText_NotAllowedInput(CustomNumericTextBox2 textBox)
        {
            if (textBox.LinkedRadioButtonArray.Length != 0)
            {
                var allowed = false;

                // ラジオボタンリンク処理
                ControlUtility controlUtil = new ControlUtility();
                controlUtil.ControlCollection = textBox.FindForm().Controls;
                foreach (var radioButtonName in textBox.LinkedRadioButtonArray)
                {
                    var radioButton = controlUtil.GetSettingField(radioButtonName) as CustomRadioButton;
                    if (radioButton != null && radioButton.Enabled)
                    {
                        allowed = true;
                        break;
                    }
                }

                // 許容外の場合、テキストをクリアする
                if (!allowed)
                {
                    //textBox.Text = string.Empty;
                    textBox.Text = TorihikisakiHoshuConstans.SHOSHIKI_MEISAI_KBN_NONE.ToString();
                }
            }
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
                    if (!this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_CD.IsNull && !string.IsNullOrWhiteSpace(this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_CD.ToString()) && this.form.SEIKYUU_KYOTEN_CD.Text.Equals(string.Empty))
                    {
                        //システム設定データの読み込み
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_CD.ToString()));
                        bool catchErr = false;
                        this.SeikyuuKyotenCdValidated(out catchErr);
                        if (catchErr)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_CD.Enabled = false;
                    this.form.SEIKYUU_KYOTEN_CD_SEARCH.Enabled = false;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeSeikyuuKyotenPrintKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
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
                    if (!this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_CD.IsNull && !string.IsNullOrWhiteSpace(this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_CD.ToString()) && this.form.SHIHARAI_KYOTEN_CD.Text.Equals(string.Empty))
                    {
                        //システム設定値の読み取り
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_CD.ToString()));
                        bool catchErr = false;
                        this.ShiharaiKyotenCdValidated(out catchErr);
                        if (catchErr)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_CD.Enabled = false;
                    this.form.SHIHARAI_KYOTEN_CD_SEARCH.Enabled = false;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeShiharaiKyotenPrintKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
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
                this.form.EIGYOU_TANTOU_BUSHO_NAME.Text = string.Empty;
                if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOU_BUSHO_CD.Text))
                {
                    M_BUSHO search = new M_BUSHO();
                    search.BUSHO_CD = this.form.EIGYOU_TANTOU_BUSHO_CD.Text;
                    M_BUSHO[] busho = this.daoIM_BUSHO.GetAllValidData(search);
                    if (busho != null && busho.Length > 0 && !busho[0].BUSHO_CD.Equals("999"))
                    {
                        this.form.EIGYOU_TANTOU_BUSHO_CD.Text = busho[0].BUSHO_CD.ToString();
                        this.form.EIGYOU_TANTOU_BUSHO_NAME.Text = busho[0].BUSHO_NAME_RYAKU.ToString();
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
                    this.form.EIGYOU_TANTOU_BUSHO_NAME.Text = string.Empty;
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("BushoCdValidated", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("BushoCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// <summary>
        /// 自社情報入力－会計情報タブに登録されている銀行と銀行支店を
        /// 請求情報2タブの振込銀行、支店の初期値としてセット
        /// </summary>
        public void SetDefaultValueFromJisyaJyouhouToFURIKOMI_BANK()
        {
            var jisyadata = this.daoIM_CORP_INFO.GetAllData();
            foreach (var info in jisyadata)
            {
                this.form.FURIKOMI_BANK_CD.Text = info.BANK_CD;
                if (this.form.FURIKOMI_BANK_CD.Text != "")
                {
                    var bank = this.daoIM_BANK.GetDataByCd(this.form.FURIKOMI_BANK_CD.Text);
                    if (bank != null)
                    {
                        this.form.FURIKOMI_BANK_NAME.Text = bank.BANK_NAME_RYAKU;
                    }
                }

                this.form.FURIKOMI_BANK_SHITEN_CD.Text = info.BANK_SHITEN_CD;
                // 取引先マスタと自社情報マスタの銀行支店名が同一となるようにする。
                this.form.KOUZA_SHURUI.Text = info.KOUZA_SHURUI;
                this.form.KOUZA_NO.Text = info.KOUZA_NO;
                if (this.form.FURIKOMI_BANK_SHITEN_CD.Text != "")
                {
                    var bankshiten = this.daoIM_BANK_SHITEN.GetDateForStringSql("SELECT * FROM M_BANK_SHITEN " +
                                                                                " where BANK_CD =  " + this.form.FURIKOMI_BANK_CD.Text +
                                                                                " and BANK_SHITEN_CD = " + this.form.FURIKOMI_BANK_SHITEN_CD.Text +
                                                                                " and KOUZA_SHURUI = " + "'" + this.form.KOUZA_SHURUI.Text + "'" +
                                                                                " and KOUZA_NO = " + this.form.KOUZA_NO.Text);
                    if (bankshiten != null)
                    {
                        foreach (DataRow dr in bankshiten.Rows)
                        {
                            Console.Write(dr["BANK_SHIETN_NAME_RYAKU"].ToString());

                            this.form.FURIKOMI_BANK_SHITEN_NAME.Text = dr["BANK_SHIETN_NAME_RYAKU"].ToString();
                            break;
                        }
                    }
                }
                this.form.KOUZA_NAME.Text = info.KOUZA_NAME;

                //振込銀行2
                this.form.FURIKOMI_BANK_CD_2.Text = info.BANK_CD_2;
                if (this.form.FURIKOMI_BANK_CD_2.Text != "")
                {
                    var bank = this.daoIM_BANK.GetDataByCd(this.form.FURIKOMI_BANK_CD_2.Text);
                    if (bank != null)
                    {
                        this.form.FURIKOMI_BANK_NAME_2.Text = bank.BANK_NAME_RYAKU;
                    }
                }

                this.form.FURIKOMI_BANK_SHITEN_CD_2.Text = info.BANK_SHITEN_CD_2;
                // 取引先マスタと自社情報マスタの銀行支店名が同一となるようにする。
                this.form.KOUZA_SHURUI_2.Text = info.KOUZA_SHURUI_2;
                this.form.KOUZA_NO_2.Text = info.KOUZA_NO_2;
                if (this.form.FURIKOMI_BANK_SHITEN_CD_2.Text != "")
                {
                    var bankshiten = this.daoIM_BANK_SHITEN.GetDateForStringSql("SELECT * FROM M_BANK_SHITEN " +
                                                                                " where BANK_CD =  " + this.form.FURIKOMI_BANK_CD_2.Text +
                                                                                " and BANK_SHITEN_CD = " + this.form.FURIKOMI_BANK_SHITEN_CD_2.Text +
                                                                                " and KOUZA_SHURUI = " + "'" + this.form.KOUZA_SHURUI_2.Text + "'" +
                                                                                " and KOUZA_NO = " + this.form.KOUZA_NO_2.Text);
                    if (bankshiten != null)
                    {
                        foreach (DataRow dr in bankshiten.Rows)
                        {
                            Console.Write(dr["BANK_SHIETN_NAME_RYAKU"].ToString());

                            this.form.FURIKOMI_BANK_SHITEN_NAME_2.Text = dr["BANK_SHIETN_NAME_RYAKU"].ToString();
                            break;
                        }
                    }
                }
                this.form.KOUZA_NAME_2.Text = info.KOUZA_NAME_2;

                //振込銀行3
                this.form.FURIKOMI_BANK_CD_3.Text = info.BANK_CD_3;
                if (this.form.FURIKOMI_BANK_CD_3.Text != "")
                {
                    var bank = this.daoIM_BANK.GetDataByCd(this.form.FURIKOMI_BANK_CD_3.Text);
                    if (bank != null)
                    {
                        this.form.FURIKOMI_BANK_NAME_3.Text = bank.BANK_NAME_RYAKU;
                    }
                }

                this.form.FURIKOMI_BANK_SHITEN_CD_3.Text = info.BANK_SHITEN_CD_3;
                // 取引先マスタと自社情報マスタの銀行支店名3が同一となるようにする。
                this.form.KOUZA_SHURUI_3.Text = info.KOUZA_SHURUI_3;
                this.form.KOUZA_NO_3.Text = info.KOUZA_NO_3;
                if (this.form.FURIKOMI_BANK_SHITEN_CD_3.Text != "")
                {
                    var bankshiten = this.daoIM_BANK_SHITEN.GetDateForStringSql("SELECT * FROM M_BANK_SHITEN " +
                                                                                " where BANK_CD =  " + this.form.FURIKOMI_BANK_CD_3.Text +
                                                                                " and BANK_SHITEN_CD = " + this.form.FURIKOMI_BANK_SHITEN_CD_3.Text +
                                                                                " and KOUZA_SHURUI = " + "'" + this.form.KOUZA_SHURUI_3.Text + "'" +
                                                                                " and KOUZA_NO = " + this.form.KOUZA_NO_3.Text);
                    if (bankshiten != null)
                    {
                        foreach (DataRow dr in bankshiten.Rows)
                        {
                            Console.Write(dr["BANK_SHIETN_NAME_RYAKU"].ToString());

                            this.form.FURIKOMI_BANK_SHITEN_NAME_3.Text = dr["BANK_SHIETN_NAME_RYAKU"].ToString();
                            break;
                        }
                    }
                }
                this.form.KOUZA_NAME_3.Text = info.KOUZA_NAME_3;
            }
        }

        /// <summary>
        /// 銀行支店リストを取得します
        /// </summary>
        /// <param name="bankCd">銀行CD</param>
        /// <param name="bankShitenCd">銀行支店CD</param>
        /// <returns>銀行支店リスト</returns>
        internal List<M_BANK_SHITEN> GetBankShiten(string bankCd, string bankShitenCd, out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart(bankCd, bankShitenCd);

                catchErr = false;
                var bankShitenList = this.daoIM_BANK_SHITEN.GetAllValidData(new M_BANK_SHITEN() { BANK_CD = bankCd, BANK_SHITEN_CD = bankShitenCd }).ToList();

                LogUtility.DebugMethodEnd(bankShitenList, catchErr);

                return bankShitenList;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                var bankShitenList = new List<M_BANK_SHITEN>();
                LogUtility.Error("GetBankShiten", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(bankShitenList, catchErr);
                return bankShitenList;
            }
            catch (Exception ex)
            {
                catchErr = true;
                var bankShitenList = new List<M_BANK_SHITEN>();
                LogUtility.Error("GetBankShiten", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(bankShitenList, catchErr);
                return bankShitenList;
            }
        }

        /// <summary>
        /// 引合で取引先データの使用有無を確認
        /// </summary>
        /// <returns></returns>
        private bool ContainsTorihikisaki()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            var hikiaiSql = "SELECT * FROM {0} WHERE {0}.TORIHIKISAKI_CD = '{1}' AND {0}.HIKIAI_TORIHIKISAKI_USE_FLG = 0 AND {0}.DELETE_FLG = 0;";
            var torihikisakiCD = this.entitysTORIHIKISAKI.TORIHIKISAKI_CD;

            // 引合業者マスタでの使用有無を確認
            var dt = this.daoIM_HIKIAI_GYOUSHA.GetDateForStringSql(string.Format(hikiaiSql, "M_HIKIAI_GYOUSHA", torihikisakiCD));
            if (0 < dt.Rows.Count)
            {
                msgLogic.MessageBoxShow("E086", "取引先", "引合業者", "削除");
                return true;
            }

            // 引合現場マスタでの使用有無を確認
            dt = this.daoIM_HIKIAI_GENBA.GetDateForStringSql(string.Format(hikiaiSql, "M_HIKIAI_GENBA", torihikisakiCD));
            if (0 < dt.Rows.Count)
            {
                msgLogic.MessageBoxShow("E086", "取引先", "引合現場", "削除");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 仮取引先エンティティを取得します
        /// </summary>
        /// <returns>取得した件数</returns>
        private int SearchKariTorihikisaki()
        {
            LogUtility.DebugMethodStart();

            var ret = 0;
            var dao = DaoInitUtility.GetComponent<IM_KARI_TORIHIKISAKIDao>();
            var entityList = dao.GetKariTorihikisakiList(new M_KARI_TORIHIKISAKI() { TORIHIKISAKI_CD = this.TorihikisakiCd, DELETE_FLG = false });
            if (0 != entityList.Count())
            {
                this.LoadKariTorihikisaki = entityList.FirstOrDefault();
                ret = 1;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        private int SearchKariTorihikisakiSeikyuu()
        {
            LogUtility.DebugMethodStart();

            var ret = 0;
            var dao = DaoInitUtility.GetComponent<IM_KARI_TORIHIKISAKI_SEIKYUUDao>();
            var entityList = dao.GetKariTorihikisakiSeikyuuList(new M_KARI_TORIHIKISAKI_SEIKYUU() { TORIHIKISAKI_CD = this.TorihikisakiCd });
            if (0 != entityList.Count())
            {
                this.LoadKariTorihikisakiSeikyuu = entityList.FirstOrDefault();
                ret = 1;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        private int SearchKariTorihikisakiShiharai()
        {
            LogUtility.DebugMethodStart();

            var ret = 0;
            var dao = DaoInitUtility.GetComponent<IM_KARI_TORIHIKISAKI_SHIHARAIDao>();
            var entityList = dao.GetKariTorihikisakiShiharaiList(new M_KARI_TORIHIKISAKI_SHIHARAI() { TORIHIKISAKI_CD = this.TorihikisakiCd });
            if (0 != entityList.Count())
            {
                this.LoadKariTorihikisakiShiharai = entityList.FirstOrDefault();
                ret = 1;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 引合取引先エンティティを取得します
        /// </summary>
        /// <returns>取得した件数</returns>
        private int SearchHikiaiTorihikisaki()
        {
            LogUtility.DebugMethodStart();

            var ret = 0;
            var dao = DaoInitUtility.GetComponent<IM_HIKIAI_TORIHIKISAKIDao>();
            var entityList = dao.GetHikiaiTorihikisakiList(new M_HIKIAI_TORIHIKISAKI() { TORIHIKISAKI_CD = this.TorihikisakiCd, DELETE_FLG = false });
            if (0 != entityList.Count())
            {
                this.LoadHikiaiTorihikisaki = entityList.FirstOrDefault();
                ret = 1;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        private int SearchHikiaiTorihikisakiSeikyuu()
        {
            LogUtility.DebugMethodStart();

            var ret = 0;
            var dao = DaoInitUtility.GetComponent<IM_HIKIAI_TORIHIKISAKI_SEIKYUUDao>();
            var entityList = dao.GetHikiaiTorihikisakiSeikyuuList(new M_HIKIAI_TORIHIKISAKI_SEIKYUU() { TORIHIKISAKI_CD = this.TorihikisakiCd });
            if (0 != entityList.Count())
            {
                this.LoadHikiaiTorihikisakiSeikyuu = entityList.FirstOrDefault();
                ret = 1;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        private int SearchHikiaiTorihikisakiShiharai()
        {
            LogUtility.DebugMethodStart();

            var ret = 0;
            var dao = DaoInitUtility.GetComponent<IM_HIKIAI_TORIHIKISAKI_SHIHARAIDao>();
            var entityList = dao.GetHikiaiTorihikisakiShiharaiList(new M_HIKIAI_TORIHIKISAKI_SHIHARAI() { TORIHIKISAKI_CD = this.TorihikisakiCd });
            if (0 != entityList.Count())
            {
                this.LoadHikiaiTorihikisakiShiharai = entityList.FirstOrDefault();
                ret = 1;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        private void CopyKariTorihikisakiEntityToTorihikisakiEntity()
        {
            LogUtility.DebugMethodStart();

            this.entitysTORIHIKISAKI = new M_TORIHIKISAKI();
            this.entitysTORIHIKISAKI.TORIHIKISAKI_CD = this.LoadKariTorihikisaki.TORIHIKISAKI_CD;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD = this.LoadKariTorihikisaki.TORIHIKISAKI_KYOTEN_CD;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME1 = this.LoadKariTorihikisaki.TORIHIKISAKI_NAME1;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME2 = this.LoadKariTorihikisaki.TORIHIKISAKI_NAME2;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU = this.LoadKariTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_FURIGANA = this.LoadKariTorihikisaki.TORIHIKISAKI_FURIGANA;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_TEL = this.LoadKariTorihikisaki.TORIHIKISAKI_TEL;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_FAX = this.LoadKariTorihikisaki.TORIHIKISAKI_FAX;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU1 = this.LoadKariTorihikisaki.TORIHIKISAKI_KEISHOU1;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU2 = this.LoadKariTorihikisaki.TORIHIKISAKI_KEISHOU2;
            this.entitysTORIHIKISAKI.EIGYOU_TANTOU_BUSHO_CD = this.LoadKariTorihikisaki.EIGYOU_TANTOU_BUSHO_CD;
            this.entitysTORIHIKISAKI.EIGYOU_TANTOU_CD = this.LoadKariTorihikisaki.EIGYOU_TANTOU_CD;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_POST = this.LoadKariTorihikisaki.TORIHIKISAKI_POST;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD = this.LoadKariTorihikisaki.TORIHIKISAKI_TODOUFUKEN_CD;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS1 = this.LoadKariTorihikisaki.TORIHIKISAKI_ADDRESS1;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS2 = this.LoadKariTorihikisaki.TORIHIKISAKI_ADDRESS2;
            this.entitysTORIHIKISAKI.TORIHIKI_JOUKYOU = this.LoadKariTorihikisaki.TORIHIKI_JOUKYOU;
            this.entitysTORIHIKISAKI.CHUUSHI_RIYUU1 = this.LoadKariTorihikisaki.CHUUSHI_RIYUU1;
            this.entitysTORIHIKISAKI.CHUUSHI_RIYUU2 = this.LoadKariTorihikisaki.CHUUSHI_RIYUU2;
            this.entitysTORIHIKISAKI.BUSHO = this.LoadKariTorihikisaki.BUSHO;
            this.entitysTORIHIKISAKI.TANTOUSHA = this.LoadKariTorihikisaki.TANTOUSHA;
            this.entitysTORIHIKISAKI.SHUUKEI_ITEM_CD = this.LoadKariTorihikisaki.SHUUKEI_ITEM_CD;
            this.entitysTORIHIKISAKI.GYOUSHU_CD = this.LoadKariTorihikisaki.GYOUSHU_CD;
            this.entitysTORIHIKISAKI.BIKOU1 = this.LoadKariTorihikisaki.BIKOU1;
            this.entitysTORIHIKISAKI.BIKOU2 = this.LoadKariTorihikisaki.BIKOU2;
            this.entitysTORIHIKISAKI.BIKOU3 = this.LoadKariTorihikisaki.BIKOU3;
            this.entitysTORIHIKISAKI.BIKOU4 = this.LoadKariTorihikisaki.BIKOU4;
            this.entitysTORIHIKISAKI.NYUUKINSAKI_KBN = this.LoadKariTorihikisaki.NYUUKINSAKI_KBN;
            this.entitysTORIHIKISAKI.DAIHYOU_PRINT_KBN = this.LoadKariTorihikisaki.DAIHYOU_PRINT_KBN;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KBN = this.LoadKariTorihikisaki.MANI_HENSOUSAKI_KBN;
            this.entitysTORIHIKISAKI.SHOKUCHI_KBN = this.LoadKariTorihikisaki.SHOKUCHI_KBN;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = this.LoadKariTorihikisaki.MANI_HENSOUSAKI_THIS_ADDRESS_KBN;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_NAME1 = this.LoadKariTorihikisaki.MANI_HENSOUSAKI_NAME1;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_NAME2 = this.LoadKariTorihikisaki.MANI_HENSOUSAKI_NAME2;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU1 = this.LoadKariTorihikisaki.MANI_HENSOUSAKI_KEISHOU1;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU2 = this.LoadKariTorihikisaki.MANI_HENSOUSAKI_KEISHOU2;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_POST = this.LoadKariTorihikisaki.MANI_HENSOUSAKI_POST;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_ADDRESS1 = this.LoadKariTorihikisaki.MANI_HENSOUSAKI_ADDRESS1;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_ADDRESS2 = this.LoadKariTorihikisaki.MANI_HENSOUSAKI_ADDRESS2;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_BUSHO = this.LoadKariTorihikisaki.MANI_HENSOUSAKI_BUSHO;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_TANTOU = this.LoadKariTorihikisaki.MANI_HENSOUSAKI_TANTOU;
            this.entitysTORIHIKISAKI.TEKIYOU_BEGIN = this.LoadKariTorihikisaki.TEKIYOU_BEGIN;
            this.entitysTORIHIKISAKI.SEARCH_TEKIYOU_BEGIN = this.LoadKariTorihikisaki.SEARCH_TEKIYOU_BEGIN;
            this.entitysTORIHIKISAKI.TEKIYOU_END = this.LoadKariTorihikisaki.TEKIYOU_END;
            this.entitysTORIHIKISAKI.SEARCH_TEKIYOU_END = this.LoadKariTorihikisaki.SEARCH_TEKIYOU_END;
            this.entitysTORIHIKISAKI.DELETE_FLG = this.LoadKariTorihikisaki.DELETE_FLG;

            LogUtility.DebugMethodEnd();
        }

        private void CopyKariTorihikisakiSeikyuuEntityToTorihikisakiSeikyuuEntity()
        {
            LogUtility.DebugMethodStart();

            this.entitysTORIHIKISAKI_SEIKYUU = new M_TORIHIKISAKI_SEIKYUU();
            this.entitysTORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD = this.LoadKariTorihikisakiSeikyuu.TORIHIKISAKI_CD;
            this.entitysTORIHIKISAKI_SEIKYUU.TORIHIKI_KBN_CD = this.LoadKariTorihikisakiSeikyuu.TORIHIKI_KBN_CD;
            this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI1 = this.LoadKariTorihikisakiSeikyuu.SHIMEBI1;
            this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI2 = this.LoadKariTorihikisakiSeikyuu.SHIMEBI2;
            this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI3 = this.LoadKariTorihikisakiSeikyuu.SHIMEBI3;
            this.entitysTORIHIKISAKI_SEIKYUU.HICCHAKUBI = this.LoadKariTorihikisakiSeikyuu.HICCHAKUBI;
            this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_MONTH = this.LoadKariTorihikisakiSeikyuu.KAISHUU_MONTH;
            this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_DAY = this.LoadKariTorihikisakiSeikyuu.KAISHUU_DAY;
            this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_HOUHOU = this.LoadKariTorihikisakiSeikyuu.KAISHUU_HOUHOU;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_JOUHOU1 = this.LoadKariTorihikisakiSeikyuu.SEIKYUU_JOUHOU1;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_JOUHOU2 = this.LoadKariTorihikisakiSeikyuu.SEIKYUU_JOUHOU2;
            this.entitysTORIHIKISAKI_SEIKYUU.KAISHI_URIKAKE_ZANDAKA = this.LoadKariTorihikisakiSeikyuu.KAISHI_URIKAKE_ZANDAKA;
            this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_KBN = this.LoadKariTorihikisakiSeikyuu.SHOSHIKI_KBN;
            if (this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_KBN == 3)
            {
                this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_GENBA_KBN = 1;
            }
            this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_MEISAI_KBN = this.LoadKariTorihikisakiSeikyuu.SHOSHIKI_MEISAI_KBN;
            this.entitysTORIHIKISAKI_SEIKYUU.LAST_TORIHIKI_DATE = this.LoadKariTorihikisakiSeikyuu.LAST_TORIHIKI_DATE;
            this.entitysTORIHIKISAKI_SEIKYUU.SEARCH_LAST_TORIHIKI_DATE = this.LoadKariTorihikisakiSeikyuu.SEARCH_LAST_TORIHIKI_DATE;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KEITAI_KBN = this.LoadKariTorihikisakiSeikyuu.SEIKYUU_KEITAI_KBN;
            this.entitysTORIHIKISAKI_SEIKYUU.NYUUKIN_MEISAI_KBN = this.LoadKariTorihikisakiSeikyuu.NYUUKIN_MEISAI_KBN;
            this.entitysTORIHIKISAKI_SEIKYUU.YOUSHI_KBN = this.LoadKariTorihikisakiSeikyuu.YOUSHI_KBN;
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            this.entitysTORIHIKISAKI_SEIKYUU.OUTPUT_KBN = this.LoadKariTorihikisakiSeikyuu.OUTPUT_KBN;
            this.entitysTORIHIKISAKI_SEIKYUU.HAKKOUSAKI_CD = this.LoadKariTorihikisakiSeikyuu.HAKKOUSAKI_CD;
            // 20160429 koukoukon v2.1_電子請求書 #16612 end

            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
            this.entitysTORIHIKISAKI_SEIKYUU.INXS_SEIKYUU_KBN = 2;
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

            this.entitysTORIHIKISAKI_SEIKYUU.ZEI_KEISAN_KBN_CD = this.LoadKariTorihikisakiSeikyuu.ZEI_KEISAN_KBN_CD;
            this.entitysTORIHIKISAKI_SEIKYUU.ZEI_KBN_CD = this.LoadKariTorihikisakiSeikyuu.ZEI_KBN_CD;
            this.entitysTORIHIKISAKI_SEIKYUU.TAX_HASUU_CD = this.LoadKariTorihikisakiSeikyuu.TAX_HASUU_CD;
            this.entitysTORIHIKISAKI_SEIKYUU.KINGAKU_HASUU_CD = this.LoadKariTorihikisakiSeikyuu.KINGAKU_HASUU_CD;
            this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD = this.LoadKariTorihikisakiSeikyuu.FURIKOMI_BANK_CD;
            this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD = this.LoadKariTorihikisakiSeikyuu.FURIKOMI_BANK_SHITEN_CD;
            this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI = this.LoadKariTorihikisakiSeikyuu.KOUZA_SHURUI;
            this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO = this.LoadKariTorihikisakiSeikyuu.KOUZA_NO;
            this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NAME = this.LoadKariTorihikisakiSeikyuu.KOUZA_NAME;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_NAME1 = this.LoadKariTorihikisakiSeikyuu.SEIKYUU_SOUFU_NAME1;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_NAME2 = this.LoadKariTorihikisakiSeikyuu.SEIKYUU_SOUFU_NAME2;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_KEISHOU1 = this.LoadKariTorihikisakiSeikyuu.SEIKYUU_SOUFU_KEISHOU1;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_KEISHOU2 = this.LoadKariTorihikisakiSeikyuu.SEIKYUU_SOUFU_KEISHOU2;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_POST = this.LoadKariTorihikisakiSeikyuu.SEIKYUU_SOUFU_POST;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_ADDRESS1 = this.LoadKariTorihikisakiSeikyuu.SEIKYUU_SOUFU_ADDRESS1;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_ADDRESS2 = this.LoadKariTorihikisakiSeikyuu.SEIKYUU_SOUFU_ADDRESS2;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_BUSHO = this.LoadKariTorihikisakiSeikyuu.SEIKYUU_SOUFU_BUSHO;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_TANTOU = this.LoadKariTorihikisakiSeikyuu.SEIKYUU_SOUFU_TANTOU;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_TEL = this.LoadKariTorihikisakiSeikyuu.SEIKYUU_SOUFU_TEL;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_FAX = this.LoadKariTorihikisakiSeikyuu.SEIKYUU_SOUFU_FAX;
            this.entitysTORIHIKISAKI_SEIKYUU.NYUUKINSAKI_CD = this.LoadKariTorihikisakiSeikyuu.NYUUKINSAKI_CD;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_TANTOU = this.LoadKariTorihikisakiSeikyuu.SEIKYUU_TANTOU;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_DAIHYOU_PRINT_KBN = this.LoadKariTorihikisakiSeikyuu.SEIKYUU_DAIHYOU_PRINT_KBN;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_PRINT_KBN = this.LoadKariTorihikisakiSeikyuu.SEIKYUU_KYOTEN_PRINT_KBN;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_CD = this.LoadKariTorihikisakiSeikyuu.SEIKYUU_KYOTEN_CD;

            LogUtility.DebugMethodEnd();
        }

        private void CopyKariTorihikisakiShiharaiEntityToTorihikisakiShiharaiEntity()
        {
            LogUtility.DebugMethodStart();

            this.entitysTORIHIKISAKI_SHIHARAI = new M_TORIHIKISAKI_SHIHARAI();
            this.entitysTORIHIKISAKI_SHIHARAI.TORIHIKISAKI_CD = this.LoadKariTorihikisakiShiharai.TORIHIKISAKI_CD;
            this.entitysTORIHIKISAKI_SHIHARAI.TORIHIKI_KBN_CD = this.LoadKariTorihikisakiShiharai.TORIHIKI_KBN_CD;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIMEBI1 = this.LoadKariTorihikisakiShiharai.SHIMEBI1;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIMEBI2 = this.LoadKariTorihikisakiShiharai.SHIMEBI2;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIMEBI3 = this.LoadKariTorihikisakiShiharai.SHIMEBI3;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_MONTH = this.LoadKariTorihikisakiShiharai.SHIHARAI_MONTH;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_DAY = this.LoadKariTorihikisakiShiharai.SHIHARAI_DAY;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_HOUHOU = this.LoadKariTorihikisakiShiharai.SHIHARAI_HOUHOU;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_JOUHOU1 = this.LoadKariTorihikisakiShiharai.SHIHARAI_JOUHOU1;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_JOUHOU2 = this.LoadKariTorihikisakiShiharai.SHIHARAI_JOUHOU2;
            this.entitysTORIHIKISAKI_SHIHARAI.KAISHI_KAIKAKE_ZANDAKA = this.LoadKariTorihikisakiShiharai.KAISHI_KAIKAKE_ZANDAKA;
            this.entitysTORIHIKISAKI_SHIHARAI.SHOSHIKI_KBN = this.LoadKariTorihikisakiShiharai.SHOSHIKI_KBN;
            this.entitysTORIHIKISAKI_SHIHARAI.SHOSHIKI_MEISAI_KBN = this.LoadKariTorihikisakiShiharai.SHOSHIKI_MEISAI_KBN;
            if (this.entitysTORIHIKISAKI_SHIHARAI.SHOSHIKI_KBN == 3)
            {
                this.entitysTORIHIKISAKI_SHIHARAI.SHOSHIKI_GENBA_KBN = 1;
            }
            this.entitysTORIHIKISAKI_SHIHARAI.LAST_TORIHIKI_DATE = this.LoadKariTorihikisakiShiharai.LAST_TORIHIKI_DATE;
            this.entitysTORIHIKISAKI_SHIHARAI.SEARCH_LAST_TORIHIKI_DATE = this.LoadKariTorihikisakiShiharai.SEARCH_LAST_TORIHIKI_DATE;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_KEITAI_KBN = this.LoadKariTorihikisakiShiharai.SHIHARAI_KEITAI_KBN;
            this.entitysTORIHIKISAKI_SHIHARAI.SHUKKIN_MEISAI_KBN = this.LoadKariTorihikisakiShiharai.SHUKKIN_MEISAI_KBN;
            this.entitysTORIHIKISAKI_SHIHARAI.YOUSHI_KBN = this.LoadKariTorihikisakiShiharai.YOUSHI_KBN;
            this.entitysTORIHIKISAKI_SHIHARAI.ZEI_KEISAN_KBN_CD = this.LoadKariTorihikisakiShiharai.ZEI_KEISAN_KBN_CD;
            this.entitysTORIHIKISAKI_SHIHARAI.ZEI_KBN_CD = this.LoadKariTorihikisakiShiharai.ZEI_KBN_CD;
            this.entitysTORIHIKISAKI_SHIHARAI.TAX_HASUU_CD = this.LoadKariTorihikisakiShiharai.TAX_HASUU_CD;
            this.entitysTORIHIKISAKI_SHIHARAI.KINGAKU_HASUU_CD = this.LoadKariTorihikisakiShiharai.KINGAKU_HASUU_CD;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_NAME1 = this.LoadKariTorihikisakiShiharai.SHIHARAI_SOUFU_NAME1;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_NAME2 = this.LoadKariTorihikisakiShiharai.SHIHARAI_SOUFU_NAME2;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_KEISHOU1 = this.LoadKariTorihikisakiShiharai.SHIHARAI_SOUFU_KEISHOU1;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_KEISHOU2 = this.LoadKariTorihikisakiShiharai.SHIHARAI_SOUFU_KEISHOU2;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_POST = this.LoadKariTorihikisakiShiharai.SHIHARAI_SOUFU_POST;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_ADDRESS1 = this.LoadKariTorihikisakiShiharai.SHIHARAI_SOUFU_ADDRESS1;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_ADDRESS2 = this.LoadKariTorihikisakiShiharai.SHIHARAI_SOUFU_ADDRESS2;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_BUSHO = this.LoadKariTorihikisakiShiharai.SHIHARAI_SOUFU_BUSHO;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_TANTOU = this.LoadKariTorihikisakiShiharai.SHIHARAI_SOUFU_TANTOU;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_TEL = this.LoadKariTorihikisakiShiharai.SHIHARAI_SOUFU_TEL;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_FAX = this.LoadKariTorihikisakiShiharai.SHIHARAI_SOUFU_FAX;
            this.entitysTORIHIKISAKI_SHIHARAI.SYUUKINSAKI_CD = this.LoadKariTorihikisakiShiharai.SYUUKINSAKI_CD;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_PRINT_KBN = this.LoadKariTorihikisakiShiharai.SHIHARAI_KYOTEN_PRINT_KBN;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_CD = this.LoadKariTorihikisakiShiharai.SHIHARAI_KYOTEN_CD;

            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
            this.entitysTORIHIKISAKI_SHIHARAI.INXS_SHIHARAI_KBN = 2;
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

            LogUtility.DebugMethodEnd();
        }

        private void CopyHikiaiTorihikisakiEntityToTorihikisakiEntity()
        {
            LogUtility.DebugMethodStart();

            this.entitysTORIHIKISAKI = new M_TORIHIKISAKI();
            this.entitysTORIHIKISAKI.TORIHIKISAKI_CD = this.LoadHikiaiTorihikisaki.TORIHIKISAKI_CD;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD = this.LoadHikiaiTorihikisaki.TORIHIKISAKI_KYOTEN_CD;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME1 = this.LoadHikiaiTorihikisaki.TORIHIKISAKI_NAME1;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME2 = this.LoadHikiaiTorihikisaki.TORIHIKISAKI_NAME2;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU = this.LoadHikiaiTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_FURIGANA = this.LoadHikiaiTorihikisaki.TORIHIKISAKI_FURIGANA;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_TEL = this.LoadHikiaiTorihikisaki.TORIHIKISAKI_TEL;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_FAX = this.LoadHikiaiTorihikisaki.TORIHIKISAKI_FAX;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU1 = this.LoadHikiaiTorihikisaki.TORIHIKISAKI_KEISHOU1;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU2 = this.LoadHikiaiTorihikisaki.TORIHIKISAKI_KEISHOU2;
            this.entitysTORIHIKISAKI.EIGYOU_TANTOU_BUSHO_CD = this.LoadHikiaiTorihikisaki.EIGYOU_TANTOU_BUSHO_CD;
            this.entitysTORIHIKISAKI.EIGYOU_TANTOU_CD = this.LoadHikiaiTorihikisaki.EIGYOU_TANTOU_CD;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_POST = this.LoadHikiaiTorihikisaki.TORIHIKISAKI_POST;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD = this.LoadHikiaiTorihikisaki.TORIHIKISAKI_TODOUFUKEN_CD;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS1 = this.LoadHikiaiTorihikisaki.TORIHIKISAKI_ADDRESS1;
            this.entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS2 = this.LoadHikiaiTorihikisaki.TORIHIKISAKI_ADDRESS2;
            this.entitysTORIHIKISAKI.TORIHIKI_JOUKYOU = this.LoadHikiaiTorihikisaki.TORIHIKI_JOUKYOU;
            this.entitysTORIHIKISAKI.CHUUSHI_RIYUU1 = this.LoadHikiaiTorihikisaki.CHUUSHI_RIYUU1;
            this.entitysTORIHIKISAKI.CHUUSHI_RIYUU2 = this.LoadHikiaiTorihikisaki.CHUUSHI_RIYUU2;
            this.entitysTORIHIKISAKI.BUSHO = this.LoadHikiaiTorihikisaki.BUSHO;
            this.entitysTORIHIKISAKI.TANTOUSHA = this.LoadHikiaiTorihikisaki.TANTOUSHA;
            this.entitysTORIHIKISAKI.SHUUKEI_ITEM_CD = this.LoadHikiaiTorihikisaki.SHUUKEI_ITEM_CD;
            this.entitysTORIHIKISAKI.GYOUSHU_CD = this.LoadHikiaiTorihikisaki.GYOUSHU_CD;
            this.entitysTORIHIKISAKI.BIKOU1 = this.LoadHikiaiTorihikisaki.BIKOU1;
            this.entitysTORIHIKISAKI.BIKOU2 = this.LoadHikiaiTorihikisaki.BIKOU2;
            this.entitysTORIHIKISAKI.BIKOU3 = this.LoadHikiaiTorihikisaki.BIKOU3;
            this.entitysTORIHIKISAKI.BIKOU4 = this.LoadHikiaiTorihikisaki.BIKOU4;
            this.entitysTORIHIKISAKI.NYUUKINSAKI_KBN = this.LoadHikiaiTorihikisaki.NYUUKINSAKI_KBN;
            this.entitysTORIHIKISAKI.DAIHYOU_PRINT_KBN = this.LoadHikiaiTorihikisaki.DAIHYOU_PRINT_KBN;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KBN = this.LoadHikiaiTorihikisaki.MANI_HENSOUSAKI_KBN;
            this.entitysTORIHIKISAKI.SHOKUCHI_KBN = this.LoadHikiaiTorihikisaki.SHOKUCHI_KBN;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = this.LoadHikiaiTorihikisaki.MANI_HENSOUSAKI_THIS_ADDRESS_KBN;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_NAME1 = this.LoadHikiaiTorihikisaki.MANI_HENSOUSAKI_NAME1;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_NAME2 = this.LoadHikiaiTorihikisaki.MANI_HENSOUSAKI_NAME2;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU1 = this.LoadHikiaiTorihikisaki.MANI_HENSOUSAKI_KEISHOU1;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU2 = this.LoadHikiaiTorihikisaki.MANI_HENSOUSAKI_KEISHOU2;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_POST = this.LoadHikiaiTorihikisaki.MANI_HENSOUSAKI_POST;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_ADDRESS1 = this.LoadHikiaiTorihikisaki.MANI_HENSOUSAKI_ADDRESS1;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_ADDRESS2 = this.LoadHikiaiTorihikisaki.MANI_HENSOUSAKI_ADDRESS2;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_BUSHO = this.LoadHikiaiTorihikisaki.MANI_HENSOUSAKI_BUSHO;
            this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_TANTOU = this.LoadHikiaiTorihikisaki.MANI_HENSOUSAKI_TANTOU;
            this.entitysTORIHIKISAKI.TEKIYOU_BEGIN = this.LoadHikiaiTorihikisaki.TEKIYOU_BEGIN;
            this.entitysTORIHIKISAKI.SEARCH_TEKIYOU_BEGIN = this.LoadHikiaiTorihikisaki.SEARCH_TEKIYOU_BEGIN;
            this.entitysTORIHIKISAKI.TEKIYOU_END = this.LoadHikiaiTorihikisaki.TEKIYOU_END;
            this.entitysTORIHIKISAKI.SEARCH_TEKIYOU_END = this.LoadHikiaiTorihikisaki.SEARCH_TEKIYOU_END;
            this.entitysTORIHIKISAKI.DELETE_FLG = this.LoadHikiaiTorihikisaki.DELETE_FLG;

            LogUtility.DebugMethodEnd();
        }

        private void CopyHikiaiTorihikisakiSeikyuuEntityToTorihikisakiSeikyuuEntity()
        {
            LogUtility.DebugMethodStart();

            this.entitysTORIHIKISAKI_SEIKYUU = new M_TORIHIKISAKI_SEIKYUU();
            this.entitysTORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD = this.LoadHikiaiTorihikisakiSeikyuu.TORIHIKISAKI_CD;
            this.entitysTORIHIKISAKI_SEIKYUU.TORIHIKI_KBN_CD = this.LoadHikiaiTorihikisakiSeikyuu.TORIHIKI_KBN_CD;
            this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI1 = this.LoadHikiaiTorihikisakiSeikyuu.SHIMEBI1;
            this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI2 = this.LoadHikiaiTorihikisakiSeikyuu.SHIMEBI2;
            this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI3 = this.LoadHikiaiTorihikisakiSeikyuu.SHIMEBI3;
            this.entitysTORIHIKISAKI_SEIKYUU.HICCHAKUBI = this.LoadHikiaiTorihikisakiSeikyuu.HICCHAKUBI;
            //160026 S
            this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_BETSU_KBN = this.LoadHikiaiTorihikisakiSeikyuu.KAISHUU_BETSU_KBN;
            this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_BETSU_NICHIGO = this.LoadHikiaiTorihikisakiSeikyuu.KAISHUU_BETSU_NICHIGO;
            //160026 E
            this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_MONTH = this.LoadHikiaiTorihikisakiSeikyuu.KAISHUU_MONTH;
            this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_DAY = this.LoadHikiaiTorihikisakiSeikyuu.KAISHUU_DAY;
            this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_HOUHOU = this.LoadHikiaiTorihikisakiSeikyuu.KAISHUU_HOUHOU;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_JOUHOU1 = this.LoadHikiaiTorihikisakiSeikyuu.SEIKYUU_JOUHOU1;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_JOUHOU2 = this.LoadHikiaiTorihikisakiSeikyuu.SEIKYUU_JOUHOU2;
            this.entitysTORIHIKISAKI_SEIKYUU.KAISHI_URIKAKE_ZANDAKA = this.LoadHikiaiTorihikisakiSeikyuu.KAISHI_URIKAKE_ZANDAKA;
            this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_KBN = this.LoadHikiaiTorihikisakiSeikyuu.SHOSHIKI_KBN;
            this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_MEISAI_KBN = this.LoadHikiaiTorihikisakiSeikyuu.SHOSHIKI_MEISAI_KBN;
            if (this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_KBN == 3)
            {
                this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_GENBA_KBN = 1;
            }
            this.entitysTORIHIKISAKI_SEIKYUU.LAST_TORIHIKI_DATE = this.LoadHikiaiTorihikisakiSeikyuu.LAST_TORIHIKI_DATE;
            this.entitysTORIHIKISAKI_SEIKYUU.SEARCH_LAST_TORIHIKI_DATE = this.LoadHikiaiTorihikisakiSeikyuu.SEARCH_LAST_TORIHIKI_DATE;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KEITAI_KBN = this.LoadHikiaiTorihikisakiSeikyuu.SEIKYUU_KEITAI_KBN;
            this.entitysTORIHIKISAKI_SEIKYUU.NYUUKIN_MEISAI_KBN = this.LoadHikiaiTorihikisakiSeikyuu.NYUUKIN_MEISAI_KBN;
            this.entitysTORIHIKISAKI_SEIKYUU.YOUSHI_KBN = this.LoadHikiaiTorihikisakiSeikyuu.YOUSHI_KBN;
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            this.entitysTORIHIKISAKI_SEIKYUU.OUTPUT_KBN = this.LoadHikiaiTorihikisakiSeikyuu.OUTPUT_KBN;
            this.entitysTORIHIKISAKI_SEIKYUU.HAKKOUSAKI_CD = this.LoadHikiaiTorihikisakiSeikyuu.HAKKOUSAKI_CD;
            // 20160429 koukoukon v2.1_電子請求書 #16612 end

            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
            this.entitysTORIHIKISAKI_SEIKYUU.INXS_SEIKYUU_KBN = 2;
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

            this.entitysTORIHIKISAKI_SEIKYUU.ZEI_KEISAN_KBN_CD = this.LoadHikiaiTorihikisakiSeikyuu.ZEI_KEISAN_KBN_CD;
            this.entitysTORIHIKISAKI_SEIKYUU.ZEI_KBN_CD = this.LoadHikiaiTorihikisakiSeikyuu.ZEI_KBN_CD;
            this.entitysTORIHIKISAKI_SEIKYUU.TAX_HASUU_CD = this.LoadHikiaiTorihikisakiSeikyuu.TAX_HASUU_CD;
            this.entitysTORIHIKISAKI_SEIKYUU.KINGAKU_HASUU_CD = this.LoadHikiaiTorihikisakiSeikyuu.KINGAKU_HASUU_CD;
            this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD = this.LoadHikiaiTorihikisakiSeikyuu.FURIKOMI_BANK_CD;
            this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD = this.LoadHikiaiTorihikisakiSeikyuu.FURIKOMI_BANK_SHITEN_CD;
            this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI = this.LoadHikiaiTorihikisakiSeikyuu.KOUZA_SHURUI;
            this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO = this.LoadHikiaiTorihikisakiSeikyuu.KOUZA_NO;
            this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NAME = this.LoadHikiaiTorihikisakiSeikyuu.KOUZA_NAME;
            this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2 = this.LoadHikiaiTorihikisakiSeikyuu.FURIKOMI_BANK_CD_2;
            this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_2 = this.LoadHikiaiTorihikisakiSeikyuu.FURIKOMI_BANK_SHITEN_CD_2;
            this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI_2 = this.LoadHikiaiTorihikisakiSeikyuu.KOUZA_SHURUI_2;
            this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO_2 = this.LoadHikiaiTorihikisakiSeikyuu.KOUZA_NO_2;
            this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NAME_2 = this.LoadHikiaiTorihikisakiSeikyuu.KOUZA_NAME_2;
            this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3 = this.LoadHikiaiTorihikisakiSeikyuu.FURIKOMI_BANK_CD_3;
            this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_3 = this.LoadHikiaiTorihikisakiSeikyuu.FURIKOMI_BANK_SHITEN_CD_3;
            this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI_3 = this.LoadHikiaiTorihikisakiSeikyuu.KOUZA_SHURUI_3;
            this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO_3 = this.LoadHikiaiTorihikisakiSeikyuu.KOUZA_NO_3;
            this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NAME_3 = this.LoadHikiaiTorihikisakiSeikyuu.KOUZA_NAME_3;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_NAME1 = this.LoadHikiaiTorihikisakiSeikyuu.SEIKYUU_SOUFU_NAME1;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_NAME2 = this.LoadHikiaiTorihikisakiSeikyuu.SEIKYUU_SOUFU_NAME2;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_KEISHOU1 = this.LoadHikiaiTorihikisakiSeikyuu.SEIKYUU_SOUFU_KEISHOU1;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_KEISHOU2 = this.LoadHikiaiTorihikisakiSeikyuu.SEIKYUU_SOUFU_KEISHOU2;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_POST = this.LoadHikiaiTorihikisakiSeikyuu.SEIKYUU_SOUFU_POST;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_ADDRESS1 = this.LoadHikiaiTorihikisakiSeikyuu.SEIKYUU_SOUFU_ADDRESS1;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_ADDRESS2 = this.LoadHikiaiTorihikisakiSeikyuu.SEIKYUU_SOUFU_ADDRESS2;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_BUSHO = this.LoadHikiaiTorihikisakiSeikyuu.SEIKYUU_SOUFU_BUSHO;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_TANTOU = this.LoadHikiaiTorihikisakiSeikyuu.SEIKYUU_SOUFU_TANTOU;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_TEL = this.LoadHikiaiTorihikisakiSeikyuu.SEIKYUU_SOUFU_TEL;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_FAX = this.LoadHikiaiTorihikisakiSeikyuu.SEIKYUU_SOUFU_FAX;
            this.entitysTORIHIKISAKI_SEIKYUU.NYUUKINSAKI_CD = this.LoadHikiaiTorihikisakiSeikyuu.NYUUKINSAKI_CD;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_TANTOU = this.LoadHikiaiTorihikisakiSeikyuu.SEIKYUU_TANTOU;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_DAIHYOU_PRINT_KBN = this.LoadHikiaiTorihikisakiSeikyuu.SEIKYUU_DAIHYOU_PRINT_KBN;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_PRINT_KBN = this.LoadHikiaiTorihikisakiSeikyuu.SEIKYUU_KYOTEN_PRINT_KBN;
            this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_CD = this.LoadHikiaiTorihikisakiSeikyuu.SEIKYUU_KYOTEN_CD;
            this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_NAME1 = this.LoadHikiaiTorihikisakiSeikyuu.FURIKOMI_NAME1;
            this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_NAME2 = this.LoadHikiaiTorihikisakiSeikyuu.FURIKOMI_NAME2;
            LogUtility.DebugMethodEnd();
        }

        private void CopyHikiaiTorihikisakiShiharaiEntityToTorihikisakiShiharaiEntity()
        {
            LogUtility.DebugMethodStart();

            this.entitysTORIHIKISAKI_SHIHARAI = new M_TORIHIKISAKI_SHIHARAI();
            this.entitysTORIHIKISAKI_SHIHARAI.TORIHIKISAKI_CD = this.LoadHikiaiTorihikisakiShiharai.TORIHIKISAKI_CD;
            this.entitysTORIHIKISAKI_SHIHARAI.TORIHIKI_KBN_CD = this.LoadHikiaiTorihikisakiShiharai.TORIHIKI_KBN_CD;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIMEBI1 = this.LoadHikiaiTorihikisakiShiharai.SHIMEBI1;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIMEBI2 = this.LoadHikiaiTorihikisakiShiharai.SHIMEBI2;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIMEBI3 = this.LoadHikiaiTorihikisakiShiharai.SHIMEBI3;
            //160026 S
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_BETSU_KBN = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_BETSU_KBN;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_BETSU_NICHIGO = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_BETSU_NICHIGO;
            //160026 E
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_MONTH = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_MONTH;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_DAY = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_DAY;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_HOUHOU = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_HOUHOU;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_JOUHOU1 = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_JOUHOU1;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_JOUHOU2 = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_JOUHOU2;
            this.entitysTORIHIKISAKI_SHIHARAI.KAISHI_KAIKAKE_ZANDAKA = this.LoadHikiaiTorihikisakiShiharai.KAISHI_KAIKAKE_ZANDAKA;
            this.entitysTORIHIKISAKI_SHIHARAI.SHOSHIKI_KBN = this.LoadHikiaiTorihikisakiShiharai.SHOSHIKI_KBN;
            this.entitysTORIHIKISAKI_SHIHARAI.SHOSHIKI_MEISAI_KBN = this.LoadHikiaiTorihikisakiShiharai.SHOSHIKI_MEISAI_KBN;
            if (this.entitysTORIHIKISAKI_SHIHARAI.SHOSHIKI_KBN == 3)
            {
                this.entitysTORIHIKISAKI_SHIHARAI.SHOSHIKI_GENBA_KBN = 1;
            }
            this.entitysTORIHIKISAKI_SHIHARAI.LAST_TORIHIKI_DATE = this.LoadHikiaiTorihikisakiShiharai.LAST_TORIHIKI_DATE;
            this.entitysTORIHIKISAKI_SHIHARAI.SEARCH_LAST_TORIHIKI_DATE = this.LoadHikiaiTorihikisakiShiharai.SEARCH_LAST_TORIHIKI_DATE;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_KEITAI_KBN = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_KEITAI_KBN;
            this.entitysTORIHIKISAKI_SHIHARAI.SHUKKIN_MEISAI_KBN = this.LoadHikiaiTorihikisakiShiharai.SHUKKIN_MEISAI_KBN;
            this.entitysTORIHIKISAKI_SHIHARAI.YOUSHI_KBN = this.LoadHikiaiTorihikisakiShiharai.YOUSHI_KBN;
            this.entitysTORIHIKISAKI_SHIHARAI.ZEI_KEISAN_KBN_CD = this.LoadHikiaiTorihikisakiShiharai.ZEI_KEISAN_KBN_CD;
            this.entitysTORIHIKISAKI_SHIHARAI.ZEI_KBN_CD = this.LoadHikiaiTorihikisakiShiharai.ZEI_KBN_CD;
            this.entitysTORIHIKISAKI_SHIHARAI.TAX_HASUU_CD = this.LoadHikiaiTorihikisakiShiharai.TAX_HASUU_CD;
            this.entitysTORIHIKISAKI_SHIHARAI.KINGAKU_HASUU_CD = this.LoadHikiaiTorihikisakiShiharai.KINGAKU_HASUU_CD;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_NAME1 = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_SOUFU_NAME1;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_NAME2 = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_SOUFU_NAME2;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_KEISHOU1 = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_SOUFU_KEISHOU1;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_KEISHOU2 = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_SOUFU_KEISHOU2;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_POST = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_SOUFU_POST;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_ADDRESS1 = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_SOUFU_ADDRESS1;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_ADDRESS2 = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_SOUFU_ADDRESS2;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_BUSHO = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_SOUFU_BUSHO;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_TANTOU = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_SOUFU_TANTOU;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_TEL = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_SOUFU_TEL;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_FAX = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_SOUFU_FAX;
            this.entitysTORIHIKISAKI_SHIHARAI.SYUUKINSAKI_CD = this.LoadHikiaiTorihikisakiShiharai.SYUUKINSAKI_CD;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_PRINT_KBN = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_KYOTEN_PRINT_KBN;
            this.entitysTORIHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_CD = this.LoadHikiaiTorihikisakiShiharai.SHIHARAI_KYOTEN_CD;
            //160026 S
            this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_EXPORT_KBN = this.LoadHikiaiTorihikisakiShiharai.FURIKOMI_EXPORT_KBN;
            this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_CD = this.LoadHikiaiTorihikisakiShiharai.FURIKOMI_SAKI_BANK_CD;
            this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_NAME = this.LoadHikiaiTorihikisakiShiharai.FURIKOMI_SAKI_BANK_NAME;
            this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_SHITEN_CD = this.LoadHikiaiTorihikisakiShiharai.FURIKOMI_SAKI_BANK_SHITEN_CD;
            this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_SHITEN_NAME = this.LoadHikiaiTorihikisakiShiharai.FURIKOMI_SAKI_BANK_SHITEN_NAME;
            this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD = this.LoadHikiaiTorihikisakiShiharai.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD;
            this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_SHURUI = this.LoadHikiaiTorihikisakiShiharai.FURIKOMI_SAKI_BANK_KOUZA_SHURUI;
            this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_NO = this.LoadHikiaiTorihikisakiShiharai.FURIKOMI_SAKI_BANK_KOUZA_NO;
            this.entitysTORIHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_NAME = this.LoadHikiaiTorihikisakiShiharai.FURIKOMI_SAKI_BANK_KOUZA_NAME;
            this.entitysTORIHIKISAKI_SHIHARAI.TEI_SUU_RYOU_KBN = this.LoadHikiaiTorihikisakiShiharai.TEI_SUU_RYOU_KBN;
            this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_CD = this.LoadHikiaiTorihikisakiShiharai.FURI_KOMI_MOTO_BANK_CD;
            this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_SHITTEN_CD = this.LoadHikiaiTorihikisakiShiharai.FURI_KOMI_MOTO_BANK_SHITTEN_CD;
            this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_SHURUI = this.LoadHikiaiTorihikisakiShiharai.FURI_KOMI_MOTO_SHURUI;
            this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_NO = this.LoadHikiaiTorihikisakiShiharai.FURI_KOMI_MOTO_NO;
            this.entitysTORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_NAME = this.LoadHikiaiTorihikisakiShiharai.FURI_KOMI_MOTO_NAME;
            //160026 E
            this.entitysTORIHIKISAKI_SHIHARAI.TOUROKU_NO = string.Empty;
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
            this.entitysTORIHIKISAKI_SHIHARAI.INXS_SHIHARAI_KBN = 2;
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

            LogUtility.DebugMethodEnd();
        }

        internal bool UpdateHikiaiTorihikisaki(M_HIKIAI_TORIHIKISAKI entity)
        {
            try
            {
                var dao = DaoInitUtility.GetComponent<IM_HIKIAI_TORIHIKISAKIDao>();
                dao.Update(entity);
                return false;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("UpdateHikiaiTorihikisaki", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("UpdateHikiaiTorihikisaki", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdateHikiaiTorihikisaki", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// 20141217 Houkakou 「取引先入力」の日付チェックを追加する　start

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

                M_TORIHIKISAKI data = new M_TORIHIKISAKI();
                data.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                // 業者、現場の適用開始日<取引先の適用開始日
                DataTable begin = this.daoTORIHIKISAKI.GetDataBySqlFile(GET_TEKIYOUBEGIN_SQL, data);
                DateTime date;
                if (begin != null && begin.Rows.Count > 0)
                {
                    if (DateTime.TryParse(Convert.ToString(begin.Rows[0][0]), out date) && date.CompareTo(date_from) < 0)
                    {
                        msgLogic.MessageBoxShow("E255", "適用開始日", "取引先", "業者及び現場", "後", "以前");
                        this.form.TEKIYOU_BEGIN.Focus();
                        return true;
                    }
                }

                // 業者、現場の適用終了日>取引先の適用終了日
                DataTable end = this.daoTORIHIKISAKI.GetDataBySqlFile(GET_TEKIYOUEND_SQL, data);
                if (end != null && begin.Rows.Count > 0)
                {
                    if (DateTime.TryParse(Convert.ToString(end.Rows[0][0]), out date) && date.CompareTo(date_to) > 0)
                    {
                        msgLogic.MessageBoxShow("E255", "適用終了日", "取引先", "業者及び現場", "前", "以降");
                        this.form.TEKIYOU_END.Focus();
                        return true;
                    }
                }

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.TEKIYOU_BEGIN.BackColor = Constans.ERROR_COLOR;
                    this.form.TEKIYOU_END.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "適用期間From", "適用期間To" };
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

        /// 20141217 Houkakou 「取引先入力」の日付チェックを追加する　end

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

            string SeikyuMSG = "請求締処理、月次処理、入金消込処理が行われています。\r\n売掛開始残高の変更は行えません。";
            string SeisanMSG = "支払明細締処理、月次処理が行われています。\r\n買掛開始残高の変更は行えません。";

            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (string.IsNullOrEmpty(this.form.TEKIYOU_BEGIN.Text))
                {
                    var result = msgLogic.MessageBoxShow("E001", "適用開始日");
                    this.form.TEKIYOU_BEGIN.BackColor = Constans.ERROR_COLOR;
                    return true;
                }

                if (WINDOW_TYPE.DELETE_WINDOW_FLAG != this.form.WindowType)
                {
                    //check 請求書書式１ SHOSHIKI_KBN
                    string sMsg = string.Empty;
                    if (this.form.SHOSHIKI_KBN.Text == "1")
                    {
                        if (string.IsNullOrEmpty(this.form.SEIKYUU_SOUFU_NAME1.Text))
                        {
                            sMsg += Const.TorihikisakiHoshuConstans.MSG_CONF_A;
                            sMsg += "\n";
                        }
                        //check SEIKYUU_SOUFU_ADDRESS1
                        if (string.IsNullOrEmpty(this.form.SEIKYUU_SOUFU_ADDRESS1.Text))
                        {
                            sMsg += Const.TorihikisakiHoshuConstans.MSG_CONF_B;
                            sMsg += "\n";
                        }
                    }

                    //check 支払明細書書式１ SHIHARAI_SHOSHIKI_KBN
                    if (this.form.SHIHARAI_SHOSHIKI_KBN.Text == "1")
                    {
                        if (string.IsNullOrEmpty(this.form.SHIHARAI_SOUFU_NAME1.Text))
                        {
                            sMsg += Const.TorihikisakiHoshuConstans.MSG_CONF_C;
                            sMsg += "\n";
                        }
                        //check SHIHARAI_SOUFU_ADDRESS1
                        if (string.IsNullOrEmpty(this.form.SHIHARAI_SOUFU_ADDRESS1.Text))
                        {
                            sMsg += Const.TorihikisakiHoshuConstans.MSG_CONF_D;
                            sMsg += "\n";
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

                if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    //呼び出し時と登録時の売掛残高が異なる場合、締め・入金消込・月次が行われてるか確認
                    if (!(Zandaka_U.Equals(this.form.KAISHI_URIKAKE_ZANDAKA.Text)))
                    {
                        DataTable dtTableSeiK;
                        dtTableSeiK = this.daoTORIHIKISAKI.GetDateForStringSql("SELECT TOP 1 * FROM T_SEIKYUU_DENPYOU WHERE TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text.ToString() + "' AND DELETE_FLG = 0");
                        if (dtTableSeiK != null && dtTableSeiK.Rows.Count > 0)
                        {
                            msgLogic.MessageBoxShowError(SeikyuMSG);
                            dtTableSeiK.Clear();
                            return true;
                        }

                        dtTableSeiK = this.daoTORIHIKISAKI.GetDateForStringSql("SELECT TOP 1 * FROM T_NYUUKIN_KESHIKOMI WHERE TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text.ToString() + "' AND DELETE_FLG = 0");
                        if (dtTableSeiK != null && dtTableSeiK.Rows.Count > 0)
                        {
                            msgLogic.MessageBoxShowError(SeikyuMSG);
                            dtTableSeiK.Clear();
                            return true;
                        }

                        dtTableSeiK = this.daoTORIHIKISAKI.GetDateForStringSql("SELECT TOP 1 * FROM (SELECT * FROM T_MONTHLY_LOCK_UR WHERE TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text.ToString() + "' AND DELETE_FLG = 0 UNION ALL SELECT * FROM T_MONTHLY_LOCK_SH WHERE TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text.ToString() + "' AND DELETE_FLG = 0) AS A");
                        if (dtTableSeiK != null && dtTableSeiK.Rows.Count > 0)
                        {
                            msgLogic.MessageBoxShowError(SeikyuMSG);
                            dtTableSeiK.Clear();
                            return true;
                        }
                    }

                    //呼び出し時と登録時の買掛残高が異なる場合、締め・月次が行われてるか確認
                    if (!(Zandaka_K.Equals(this.form.KAISHI_KAIKAKE_ZANDAKA.Text)))
                    {
                        DataTable dtTableSeiS;
                        dtTableSeiS = this.daoTORIHIKISAKI.GetDateForStringSql("SELECT TOP 1 * FROM T_SEISAN_DENPYOU WHERE TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text.ToString() + "' AND DELETE_FLG = 0");
                        if (dtTableSeiS != null && dtTableSeiS.Rows.Count > 0)
                        {
                            msgLogic.MessageBoxShowError(SeisanMSG);
                            dtTableSeiS.Clear();
                            return true;
                        }

                        dtTableSeiS = this.daoTORIHIKISAKI.GetDateForStringSql("SELECT TOP 1 * FROM (SELECT * FROM T_MONTHLY_LOCK_UR WHERE TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text.ToString() + "' AND DELETE_FLG = 0 UNION ALL SELECT * FROM T_MONTHLY_LOCK_SH WHERE TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text.ToString() + "' AND DELETE_FLG = 0) AS A");
                        if (dtTableSeiS != null && dtTableSeiS.Rows.Count > 0)
                        {
                            msgLogic.MessageBoxShowError(SeisanMSG);
                            dtTableSeiS.Clear();
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
                return true;
            }
        }

        // VUNGUYEN 20150525 #1294 END

        /// <summary>
        /// 返送先情報区分変更後処理
        /// </summary>
        public bool ChangeManiHensousakiAddKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text.Equals("1"))
                {
                    this.form.MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
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
                    this.form.MANI_HENSOUSAKI_ADDRESS_SEARCH.Enabled = false;
                    this.form.MANI_HENSOUSAKI_POST_SEARCH.Enabled = false;
                    this.form.TORIHIKISAKI_COPY_MANI_BUTTON.Enabled = false;
                }
                else
                {
                    this.form.MANI_HENSOUSAKI_NAME1.Enabled = true;
                    this.form.MANI_HENSOUSAKI_NAME2.Enabled = true;
                    this.form.MANI_HENSOUSAKI_KEISHOU1.Enabled = true;
                    this.form.MANI_HENSOUSAKI_KEISHOU2.Enabled = true;
                    this.form.MANI_HENSOUSAKI_POST.Enabled = true;
                    this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = true;
                    this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = true;
                    this.form.MANI_HENSOUSAKI_BUSHO.Enabled = true;
                    this.form.MANI_HENSOUSAKI_TANTOU.Enabled = true;
                    this.form.MANI_HENSOUSAKI_ADDRESS_SEARCH.Enabled = true;
                    this.form.MANI_HENSOUSAKI_POST_SEARCH.Enabled = true;
                    this.form.TORIHIKISAKI_COPY_MANI_BUTTON.Enabled = true;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeManiHensousakiAddKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 削除できるかどうかチェックする
        /// </summary>
        private bool CheckDelete()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            var torihikisakiCd = this.form.TORIHIKISAKI_CD.Text;

            if (!string.IsNullOrEmpty(torihikisakiCd))
            {
                var data = new M_TORIHIKISAKI();
                data.TORIHIKISAKI_CD = torihikisakiCd;

                DataTable dtTable = this.daoTORIHIKISAKI.GetDataBySqlFile(this.CHECK_DELETE_TORIHIKISAKI_SQL, data);
                if (dtTable != null && dtTable.Rows.Count > 0)
                {
                    string strName = string.Empty;

                    foreach (DataRow dr in dtTable.Rows)
                    {
                        strName += Environment.NewLine + dr["NAME"].ToString();
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E258", "取引先", "取引先CD", strName);

                    ret = false;
                }
                else
                {
                    ret = true;
                }
            }

            LogUtility.DebugMethodEnd();
            return ret;
        }

        // 20160429 koukoukon v2.1_電子請求書 #16612 start
        /// <summary>
        /// 出力区分変更後処理
        /// </summary>
        public bool ChangeOutputKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //Begin: LANDUONG - 20220209 - refs#160050
                if (this.form.TORIHIKI_KBN.Text.Equals("2"))
                {                    
                    //1．紙　2．電子CSV　の名称を変更  3．楽楽明細CSV　を追加
                    if (this.form.OUTPUT_KBN.Text.Equals("1"))
                    {
                        this.form.HAKKOUSAKI_CD.Text = string.Empty;
                        this.form.HAKKOUSAKI_CD.Enabled = false;
                        this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;
                        this.form.RAKURAKU_CUSTOMER_CD.Enabled = false;
                        this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = false;
                    }
                    else if (this.form.OUTPUT_KBN.Text.Equals("2"))
                    {
                        this.form.HAKKOUSAKI_CD.Text = string.Empty;
                        this.form.HAKKOUSAKI_CD.Enabled = true;
                        this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;
                        this.form.RAKURAKU_CUSTOMER_CD.Enabled = false;
                        this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = false;
                    }
                    else if (this.form.OUTPUT_KBN.Text.Equals("3"))
                    {
                        this.form.HAKKOUSAKI_CD.Text = string.Empty;
                        this.form.HAKKOUSAKI_CD.Enabled = false;
                        this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;
                        this.form.RAKURAKU_CUSTOMER_CD.Enabled = true;
                        this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = true;
                        this.form.RAKURAKU_CUSTOMER_CD.BackColor = Constans.NOMAL_COLOR;

                        // 楽楽顧客コードの採番ボタン
                        if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.RAKURAKU_CODE_NUMBERING_KBN.IsNull
                            && this.entitysM_SYS_INFO.RAKURAKU_CODE_NUMBERING_KBN == 1)
                        {
                            this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = false;
                        }
                        else if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.RAKURAKU_CODE_NUMBERING_KBN.IsNull
                            && this.entitysM_SYS_INFO.RAKURAKU_CODE_NUMBERING_KBN == 2)
                        {
                            this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = true;
                        }

                    }
                }
                else
                {
                    this.form.OUTPUT_KBN.Text = string.Empty;
                    this.form.HAKKOUSAKI_CD.Enabled = false;
                    this.form.RAKURAKU_CUSTOMER_CD.Enabled = false;
                    this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = false;
                }
                //End: LANDUONG - 20220209 - refs#160050

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeOutputKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
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
                this.form.labelOutputKbn.Visible = densiVisible;
                this.form.pl_SEIKYUU_OUTPUT_KBN.Visible = densiVisible;
                this.form.labelHakkosaki.Visible = densiVisible;
                this.form.HAKKOUSAKI_CD.Visible = densiVisible;

                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
                this.form.labelInxsSeikyuuKbn.Location = new System.Drawing.Point(this.form.labelInxsSeikyuuKbn.Location.X, this.form.labelInxsSeikyuuKbn.Location.Y - 44);
                this.form.panelInxsSeikyuuKbn.Location = new System.Drawing.Point(this.form.panelInxsSeikyuuKbn.Location.X, this.form.panelInxsSeikyuuKbn.Location.Y - 44);
                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

                this.form.label214.Location = new System.Drawing.Point(this.form.label214.Location.X, this.form.label214.Location.Y - 44);
                this.form.panel11.Location = new System.Drawing.Point(this.form.panel11.Location.X, this.form.panel11.Location.Y - 44);
                this.form.label215.Location = new System.Drawing.Point(this.form.label215.Location.X, this.form.label215.Location.Y - 44);
                this.form.ZEI_KBN_CD.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD.Location.X, this.form.ZEI_KBN_CD.Location.Y - 44);
                this.form.ZEI_KBN_CD_1.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD_1.Location.X, this.form.ZEI_KBN_CD_1.Location.Y - 44);
                this.form.ZEI_KBN_CD_2.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD_2.Location.X, this.form.ZEI_KBN_CD_2.Location.Y - 44);
                this.form.ZEI_KBN_CD_3.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD_3.Location.X, this.form.ZEI_KBN_CD_3.Location.Y - 44);

            }
            return densiVisible;
        }
        // 20160429 koukoukon v2.1_電子請求書 #16612 end

        /// <summary>
        /// 画面起動時にオプション有無を確認し、オンラインバンク連携で追加するコントロール・項目の表示/非表示を切り替える
        /// </summary>
        /// <returns></returns>
        private bool setOnlineBankVisible()
        {
            bool onlineBankVisible = r_framework.Configuration.AppConfig.AppOptions.IsOnlinebank();
            if (!onlineBankVisible)
            {
                this.form.FURIKOMI_NAME1.Visible = onlineBankVisible;
                this.form.FURIKOMI_NAME2.Visible = onlineBankVisible;
                this.form.label15.Visible = onlineBankVisible;
                this.form.label16.Visible = onlineBankVisible;
            }
            return onlineBankVisible;
        }

        //#162933 S
        public bool setPaymentOnlineBankVisible()
        {
            bool paymentOnlineBankVisible = r_framework.Configuration.AppConfig.AppOptions.IsPaymentOnlinebank();
            if (!paymentOnlineBankVisible)
            {
                this.form.FURIKOMI_EXPORT_KBN.Clear();
                this.form.FURIKOMI_EXPORT_KBN_1.Checked = false;
                this.form.FURIKOMI_EXPORT_KBN_2.Checked = false;
                this.form.FURIKOMI_EXPORT_KBN.Enabled = paymentOnlineBankVisible;
                this.form.FURIKOMI_EXPORT_KBN_1.Enabled = paymentOnlineBankVisible;
                this.form.FURIKOMI_EXPORT_KBN_2.Enabled = paymentOnlineBankVisible;
            }
            return paymentOnlineBankVisible;
        }
        //#162933 E

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
                string lat = string.Empty;
                string lon = string.Empty;

                // 緯度経度の入力チェック
                if (this.CheckLocation())
                {
                    return;
                }

                if (!string.IsNullOrEmpty(this.form.TorihikisakiLatitude.Text) && !string.IsNullOrEmpty(this.form.TorihikisakiLongitude.Text))
                {
                    // 緯度経度入力済み
                    if (this.form.errmessage.MessageBoxShowConfirm("地図を表示します。よろしいですか？") == DialogResult.No)
                    {
                        return;
                    }
                    // 入力済みの値を利用する
                    lat = this.form.TorihikisakiLatitude.Text;
                    lon = this.form.TorihikisakiLongitude.Text;
                }
                else if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text) && !string.IsNullOrEmpty(this.form.TORIHIKISAKI_ADDRESS1.Text))
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
                    string address = this.form.TORIHIKISAKI_TODOUFUKEN_NAME.Text + this.form.TORIHIKISAKI_ADDRESS1.Text + this.form.TORIHIKISAKI_ADDRESS2.Text;
                    if (!apiLogic.HttpGET<GeoCodingAPI>(address, out result))
                    {
                        // APIでエラー発生
                        return;
                    }
                    foreach (Feature feature in result.features)
                    {
                        // APIで取得した値を利用する
                        this.form.TorihikisakiLatitude.Text = feature.geometry.coordinates[1];
                        this.form.TorihikisakiLongitude.Text = feature.geometry.coordinates[0];
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
                gljsLogic.mapbox_HTML_Open(dtos, WINDOW_ID.M_TORIHIKISAKI);
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
                dto.dataShurui = "0";
                dto.torihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
                dto.torihikisakiName = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
                dto.gyoushaCd = string.Empty;
                dto.gyoushaName = string.Empty;
                dto.genbaCd = string.Empty;
                dto.genbaName = string.Empty;
                dto.address = this.form.TORIHIKISAKI_TODOUFUKEN_NAME.Text + this.form.TORIHIKISAKI_ADDRESS1.Text + this.form.TORIHIKISAKI_ADDRESS2.Text;
                dto.latitude = this.form.TorihikisakiLatitude.Text;
                dto.longitude = this.form.TorihikisakiLongitude.Text;
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

            // 数値以外が入力されていたらアラート

            // 緯度のチェック
            if (!string.IsNullOrEmpty(this.form.TorihikisakiLatitude.Text))
            {
                try
                {
                    Convert.ToDouble(this.form.TorihikisakiLatitude.Text);
                }
                catch (Exception ex)
                {
                    this.form.errmessage.MessageBoxShowWarn("緯度の入力が正しくありません。");
                    this.form.TorihikisakiLatitude.Focus();
                    return true;
                }
            }

            // 経度のチェック
            if (!string.IsNullOrEmpty(this.form.TorihikisakiLongitude.Text))
            {
                try
                {
                    Convert.ToDouble(this.form.TorihikisakiLongitude.Text);
                }
                catch (Exception ex)
                {
                    this.form.errmessage.MessageBoxShowWarn("経度の入力が正しくありません。");
                    this.form.TorihikisakiLongitude.Focus();
                    return true;
                }
            }

            // 緯度だけ入力されていない
            if (string.IsNullOrEmpty(this.form.TorihikisakiLatitude.Text) &&
               !string.IsNullOrEmpty(this.form.TorihikisakiLongitude.Text))
            {
                this.form.errmessage.MessageBoxShowWarn("緯度を入力してください。");
                this.form.TorihikisakiLatitude.Focus();
                return true;
            }

            // 経度だけ入力されていない
            if (string.IsNullOrEmpty(this.form.TorihikisakiLongitude.Text) &&
               !string.IsNullOrEmpty(this.form.TorihikisakiLatitude.Text))
            {
                this.form.errmessage.MessageBoxShowWarn("経度を入力してください。");
                this.form.TorihikisakiLongitude.Focus();
                return true;
            }

            return ret;
        }

        #endregion

        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
        #region 将軍-INXS 請求書アップロード

        /// <summary>
        /// Set visible INXS請求区分
        /// </summary>
        /// <returns></returns>
        private bool SetInxsSeikyushoVisible()
        {

            // densiVisible true場合表示false場合非表示
            bool inxsSeikyuushoVisible = r_framework.Configuration.AppConfig.AppOptions.IsInxsSeikyuusho();

            if (!inxsSeikyuushoVisible)
            {
                this.form.labelInxsSeikyuuKbn.Visible = inxsSeikyuushoVisible;
                this.form.panelInxsSeikyuuKbn.Visible = inxsSeikyuushoVisible;
                this.form.label214.Location = new System.Drawing.Point(this.form.label214.Location.X, this.form.label214.Location.Y - 22);
                this.form.panel11.Location = new System.Drawing.Point(this.form.panel11.Location.X, this.form.panel11.Location.Y - 22);
                this.form.label215.Location = new System.Drawing.Point(this.form.label215.Location.X, this.form.label215.Location.Y - 22);
                this.form.ZEI_KBN_CD.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD.Location.X, this.form.ZEI_KBN_CD.Location.Y - 22);
                this.form.ZEI_KBN_CD_1.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD_1.Location.X, this.form.ZEI_KBN_CD_1.Location.Y - 22);
                this.form.ZEI_KBN_CD_2.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD_2.Location.X, this.form.ZEI_KBN_CD_2.Location.Y - 22);
                this.form.ZEI_KBN_CD_3.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD_3.Location.X, this.form.ZEI_KBN_CD_3.Location.Y - 22);

            }
            return inxsSeikyuushoVisible;
        }

        #endregion
        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
        /// <summary>
        /// Set visible INXS支払区分
        /// </summary>
        /// <returns></returns>
        private bool SetInxsShiharaiMesaishoVisible()
        {

            // densiVisible true場合表示false場合非表示
            bool inxsShiharaiMesaishoVisible = r_framework.Configuration.AppConfig.AppOptions.IsInxsShiharai();

            if (!inxsShiharaiMesaishoVisible)
            {
                this.form.labelInxsShiharaiKbn.Visible = inxsShiharaiMesaishoVisible;
                this.form.panelInxsShiharaiKbn.Visible = inxsShiharaiMesaishoVisible;

                this.form.label417.Location = new System.Drawing.Point(this.form.label417.Location.X, this.form.label417.Location.Y - 22);
                this.form.panel13.Location = new System.Drawing.Point(this.form.panel13.Location.X, this.form.panel13.Location.Y - 22);

                this.form.label418.Location = new System.Drawing.Point(this.form.label418.Location.X, this.form.label418.Location.Y - 22);
                this.form.panel18.Location = new System.Drawing.Point(this.form.panel18.Location.X, this.form.panel18.Location.Y - 22);

                this.form.label419.Location = new System.Drawing.Point(this.form.label419.Location.X, this.form.label419.Location.Y - 22);
                this.form.LAST_TORIHIKI_DATE_SHIHARAI.Location = new System.Drawing.Point(this.form.LAST_TORIHIKI_DATE_SHIHARAI.Location.X, this.form.LAST_TORIHIKI_DATE_SHIHARAI.Location.Y - 22);

                this.form.label46.Location = new System.Drawing.Point(this.form.label46.Location.X, this.form.label46.Location.Y - 22);
                this.form.TOUROKU_NO.Location = new System.Drawing.Point(this.form.TOUROKU_NO.Location.X, this.form.TOUROKU_NO.Location.Y - 22);

            }
            return inxsShiharaiMesaishoVisible;
        }
        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end


        // Begin: LANDUONG - 20220209 - refs#160050
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

        internal void CheckRakurakuCodeInput()
        {
            this.form.RAKURAKU_CUSTOMER_CD.RegistCheckMethod.Clear();
        }

        internal string GetBeforeRakurakuCode()
        {
            string ret = string.Empty;

            if (this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                if (this.entitysTORIHIKISAKI_SEIKYUU != null && !string.IsNullOrEmpty(this.entitysTORIHIKISAKI_SEIKYUU.RAKURAKU_CUSTOMER_CD))
                {
                    ret = this.entitysTORIHIKISAKI_SEIKYUU.RAKURAKU_CUSTOMER_CD;
                }
            }

            return ret;
        }

        private void SetDensiSeikyushoAndRakurakuVisible()
        {
            if (!denshiSeikyusho && !denshiSeikyuRaku)
            {
                this.form.labelOutputKbn.Visible = false;
                this.form.pl_SEIKYUU_OUTPUT_KBN.Visible = false;
                this.form.labelHakkosaki.Visible = false;
                this.form.HAKKOUSAKI_CD.Visible = false;
                this.form.label28.Visible = false;
                this.form.RAKURAKU_CUSTOMER_CD.Visible = false;
                this.form.RAKURAKU_SAIBAN_BUTTON.Visible = false;

                this.form.labelInxsSeikyuuKbn.Location = new System.Drawing.Point(this.form.labelInxsSeikyuuKbn.Location.X, this.form.labelInxsSeikyuuKbn.Location.Y - 94);
                this.form.panelInxsSeikyuuKbn.Location = new System.Drawing.Point(this.form.panelInxsSeikyuuKbn.Location.X, this.form.panelInxsSeikyuuKbn.Location.Y - 94);
                this.form.label214.Location = new System.Drawing.Point(this.form.label214.Location.X, this.form.label214.Location.Y - 93);
                this.form.panel11.Location = new System.Drawing.Point(this.form.panel11.Location.X, this.form.panel11.Location.Y - 94);
                this.form.label215.Location = new System.Drawing.Point(this.form.label215.Location.X, this.form.label215.Location.Y - 93);
                this.form.ZEI_KBN_CD.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD.Location.X, this.form.ZEI_KBN_CD.Location.Y - 94);
                this.form.ZEI_KBN_CD_1.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD_1.Location.X, this.form.ZEI_KBN_CD_1.Location.Y - 94);
                this.form.ZEI_KBN_CD_2.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD_2.Location.X, this.form.ZEI_KBN_CD_2.Location.Y - 94);
                this.form.ZEI_KBN_CD_3.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD_3.Location.X, this.form.ZEI_KBN_CD_3.Location.Y - 94);
            }
            else if (denshiSeikyusho && denshiSeikyuRaku)
            {
                this.form.labelOutputKbn.Visible = true;
                this.form.pl_SEIKYUU_OUTPUT_KBN.Visible = true;
                this.form.labelHakkosaki.Visible = true;
                this.form.HAKKOUSAKI_CD.Visible = true;
                this.form.label28.Visible = true;
                this.form.RAKURAKU_CUSTOMER_CD.Visible = true;
                this.form.RAKURAKU_SAIBAN_BUTTON.Visible = true;
            }
            else if (denshiSeikyusho && !denshiSeikyuRaku)
            {
                this.form.labelOutputKbn.Visible = true;
                this.form.pl_SEIKYUU_OUTPUT_KBN.Visible = true;
                this.form.labelHakkosaki.Visible = true;
                this.form.HAKKOUSAKI_CD.Visible = true;
                this.form.label28.Visible = false;
                this.form.RAKURAKU_CUSTOMER_CD.Visible = false;
                this.form.RAKURAKU_SAIBAN_BUTTON.Visible = false;

                this.form.labelInxsSeikyuuKbn.Location = new System.Drawing.Point(this.form.labelInxsSeikyuuKbn.Location.X, this.form.labelInxsSeikyuuKbn.Location.Y - 22);
                this.form.panelInxsSeikyuuKbn.Location = new System.Drawing.Point(this.form.panelInxsSeikyuuKbn.Location.X, this.form.panelInxsSeikyuuKbn.Location.Y - 22);
                this.form.label214.Location = new System.Drawing.Point(this.form.label214.Location.X, this.form.label214.Location.Y - 22);
                this.form.panel11.Location = new System.Drawing.Point(this.form.panel11.Location.X, this.form.panel11.Location.Y - 22);
                this.form.label215.Location = new System.Drawing.Point(this.form.label215.Location.X, this.form.label215.Location.Y - 22);
                this.form.ZEI_KBN_CD.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD.Location.X, this.form.ZEI_KBN_CD.Location.Y - 22);
                this.form.ZEI_KBN_CD_1.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD_1.Location.X, this.form.ZEI_KBN_CD_1.Location.Y - 22);
                this.form.ZEI_KBN_CD_2.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD_2.Location.X, this.form.ZEI_KBN_CD_2.Location.Y - 22);
                this.form.ZEI_KBN_CD_3.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD_3.Location.X, this.form.ZEI_KBN_CD_3.Location.Y - 22);
            }
            else if (!denshiSeikyusho && denshiSeikyuRaku)
            {
                this.form.labelOutputKbn.Visible = true;
                this.form.pl_SEIKYUU_OUTPUT_KBN.Visible = true;
                this.form.labelHakkosaki.Visible = false;
                this.form.HAKKOUSAKI_CD.Visible = false;
                this.form.label28.Visible = true;
                this.form.RAKURAKU_CUSTOMER_CD.Visible = true;
                this.form.RAKURAKU_SAIBAN_BUTTON.Visible = true;

                this.form.label28.Location = new System.Drawing.Point(this.form.label28.Location.X, this.form.labelHakkosaki.Location.Y);
                this.form.RAKURAKU_CUSTOMER_CD.Location = new System.Drawing.Point(this.form.RAKURAKU_CUSTOMER_CD.Location.X, this.form.labelHakkosaki.Location.Y);
                this.form.RAKURAKU_SAIBAN_BUTTON.Location = new System.Drawing.Point(this.form.RAKURAKU_SAIBAN_BUTTON.Location.X, this.form.labelHakkosaki.Location.Y - 1);
                this.form.labelInxsSeikyuuKbn.Location = new System.Drawing.Point(this.form.labelInxsSeikyuuKbn.Location.X, this.form.labelInxsSeikyuuKbn.Location.Y - 22);
                this.form.panelInxsSeikyuuKbn.Location = new System.Drawing.Point(this.form.panelInxsSeikyuuKbn.Location.X, this.form.panelInxsSeikyuuKbn.Location.Y - 22);
                this.form.label214.Location = new System.Drawing.Point(this.form.label214.Location.X, this.form.label214.Location.Y - 22);
                this.form.panel11.Location = new System.Drawing.Point(this.form.panel11.Location.X, this.form.panel11.Location.Y - 22);
                this.form.label215.Location = new System.Drawing.Point(this.form.label215.Location.X, this.form.label215.Location.Y - 22);
                this.form.ZEI_KBN_CD.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD.Location.X, this.form.ZEI_KBN_CD.Location.Y - 22);
                this.form.ZEI_KBN_CD_1.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD_1.Location.X, this.form.ZEI_KBN_CD_1.Location.Y - 22);
                this.form.ZEI_KBN_CD_2.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD_2.Location.X, this.form.ZEI_KBN_CD_2.Location.Y - 22);
                this.form.ZEI_KBN_CD_3.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD_3.Location.X, this.form.ZEI_KBN_CD_3.Location.Y - 22);
            }

            return;
        }

        public bool ChangeOutputDensiSeikyushoAndRakurakuKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG || form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    return false;
                }

                if (!this.form.TORIHIKI_KBN.Text.Equals("1"))
                {
                    if (denshiSeikyusho && denshiSeikyuRaku)
                    {
                        this.form.OUTPUT_KBN.Enabled = true;
                        this.form.OUTPUT_KBN_1.Enabled = true;
                        this.form.OUTPUT_KBN_2.Enabled = true;
                        this.form.OUTPUT_KBN_3.Enabled = true;
                    }
                    else if (denshiSeikyusho && !denshiSeikyuRaku)
                    {
                        this.form.OUTPUT_KBN.Enabled = true;
                        this.form.OUTPUT_KBN_1.Enabled = true;
                        this.form.OUTPUT_KBN_2.Enabled = true;
                        this.form.OUTPUT_KBN_3.Enabled = false;
                    }
                    else if (!denshiSeikyusho && denshiSeikyuRaku)
                    {
                        this.form.OUTPUT_KBN.Enabled = true;
                        this.form.OUTPUT_KBN_1.Enabled = true;
                        this.form.OUTPUT_KBN_2.Enabled = false;
                        this.form.OUTPUT_KBN_3.Enabled = true;
                    }

                    //1．紙　2．電子CSV　の名称を変更  3．楽楽明細CSV　を追加
                    if (this.form.OUTPUT_KBN.Text.Equals("1"))
                    {
                        this.form.HAKKOUSAKI_CD.Enabled = false;
                        this.form.RAKURAKU_CUSTOMER_CD.Enabled = false;
                        this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = false;
                    }
                    else if (this.form.OUTPUT_KBN.Text.Equals("2"))
                    {
                        this.form.HAKKOUSAKI_CD.Enabled = true;
                        this.form.RAKURAKU_CUSTOMER_CD.Enabled = false;
                        this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = false;
                    }
                    else if (this.form.OUTPUT_KBN.Text.Equals("3"))
                    {
                        this.form.HAKKOUSAKI_CD.Enabled = false;
                        this.form.RAKURAKU_CUSTOMER_CD.Enabled = true;
                        this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = true;
                    }
                    else
                    {
                        this.form.HAKKOUSAKI_CD.Enabled = false;
                        this.form.RAKURAKU_CUSTOMER_CD.Enabled = false;
                        this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = false;
                    }
                }
                else
                {
                    this.form.OUTPUT_KBN.Text = string.Empty;
                    this.form.OUTPUT_KBN.Enabled = false;
                    this.form.OUTPUT_KBN_1.Enabled = false;
                    this.form.OUTPUT_KBN_2.Enabled = false;
                    this.form.OUTPUT_KBN_3.Enabled = false;
                    this.form.HAKKOUSAKI_CD.Enabled = false;
                    this.form.RAKURAKU_CUSTOMER_CD.Enabled = false;
                    this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = false;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeOutputKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }
        // End: LANDUONG - 20220209 - refs#160050

        //160026 S
        /// <summary>
        /// 振込元銀行変更後処理
        /// </summary>
        public bool FurikomiMotoBankCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.FURI_KOMI_MOTO_BANK_CD.Text))
                {
                    this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_SHURUI.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_NO.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_NAME.Text = string.Empty;
                }

                this.form.previousBankShitenMotoCd = this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text;

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("FurikomiMotoBankCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }
        public void SetDefaultValueFromFURIKOMI_BANK_MOTO()
        {
            var jisyadata = this.daoIM_CORP_INFO.GetAllData();
            foreach (var info in jisyadata)
            {
                this.form.FURI_KOMI_MOTO_BANK_CD.Text = info.FURIKOMI_MOTO_BANK_CD;
                if (this.form.FURI_KOMI_MOTO_BANK_CD.Text != "")
                {
                    var bank = this.daoIM_BANK.GetDataByCd(this.form.FURI_KOMI_MOTO_BANK_CD.Text);
                    if (bank != null)
                    {
                        this.form.FURI_KOMI_MOTO_BANK_NAME.Text = bank.BANK_NAME_RYAKU;
                    }
                }

                this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text = info.FURIKOMI_MOTO_BANK_SHITEN_CD;
                // 取引先マスタと自社情報マスタの銀行支店名が同一となるようにする。
                this.form.FURI_KOMI_MOTO_SHURUI.Text = info.FURIKOMI_MOTO_KOUZA_SHURUI;
                this.form.FURI_KOMI_MOTO_NO.Text = info.FURIKOMI_MOTO_KOUZA_NO;
                if (this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text != "")
                {
                    var bankshiten = this.daoIM_BANK_SHITEN.GetDateForStringSql("SELECT * FROM M_BANK_SHITEN " +
                                                                                " where BANK_CD =  " + this.form.FURI_KOMI_MOTO_BANK_CD.Text +
                                                                                " and BANK_SHITEN_CD = " + this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text +
                                                                                " and KOUZA_SHURUI = " + "'" + this.form.FURI_KOMI_MOTO_SHURUI.Text + "'" +
                                                                                " and KOUZA_NO = " + this.form.FURI_KOMI_MOTO_NO.Text);
                    if (bankshiten != null)
                    {
                        foreach (DataRow dr in bankshiten.Rows)
                        {
                            Console.Write(dr["BANK_SHIETN_NAME_RYAKU"].ToString());

                            this.form.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = dr["BANK_SHIETN_NAME_RYAKU"].ToString();
                            break;
                        }
                    }
                }
                this.form.FURI_KOMI_MOTO_NAME.Text = info.FURIKOMI_MOTO_KOUZA_NAME;
            }
        }
        //160026 E
    }
}