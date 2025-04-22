using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.KokyakuKarute.APP;
using Shougun.Core.Common.KokyakuKarute.Const;
using Shougun.Core.Common.KokyakuKarute.DAO;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.Common.KokyakuKarute.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class G173Logic : IBuisinessLogic
    {
        #region 定数

        //ボタン定義ファイル
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Common.KokyakuKarute.Setting.ButtonSetting.xml";

        // セル結合Index
        private readonly int dateIndex_Start = 9;
        private readonly int dateIndex_End = 15;
        private readonly int keiyakuKbnIndex_Start = 16;
        private readonly int keiyakuKbnIndex_End = 17;
        private readonly int syuukeiIndex_Start = 21;
        private readonly int syuukeiIndex_End = 22;

        // 月極情報取得用SQL
        private readonly string GET_TSUKI_HINMEI_DATA_SQL = "Shougun.Core.Common.KokyakuKarute.Sql.GetTsukiHinmeiDataSql.sql";

        #endregion

        #region 変数

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private r_framework.Dao.IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        internal M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// 受付DAO
        /// </summary>
        private IT_UketsukeDao UketsukeDao;

        /// <summary>
        /// 受付(収集)明細DAO
        /// </summary>
        private IT_UketsukeSSDetailDao UketsukeSSDetailDao;

        /// <summary>
        /// 受付(出荷)明細DAO
        /// </summary>
        private IT_UketsukeSKDetailDao UketsukeSKDetailDao;

        /// <summary>
        /// 受付(持込)明細DAO
        /// </summary>
        private IT_UketsukeMKDetailDao UketsukeMKDetailDao;

        /// <summary>
        /// 受付クレームDAO
        /// </summary>
        private IT_UketsukeCMDao UketsukeCMDao;

        /// <summary>
        /// 計量DAO
        /// </summary>
        private IT_KeiryouDao KeiryouDao;

        /// <summary>
        /// 計量明細DAO
        /// </summary>
        private IT_KeiryouDetailDao KeiryouDetailDao;

        /// <summary>
        /// 受入DAO
        /// </summary>
        private IT_UkeireDao UkeireDao;

        /// <summary>
        /// 受入明細DAO
        /// </summary>
        private IT_UkeireDetailDao UkeireDetailDao;

        /// <summary>
        /// 出荷DAO
        /// </summary>
        private IT_ShukkaDao ShukkaDao;

        /// <summary>
        /// 出荷明細DAO
        /// </summary>
        private IT_ShukkaDetailDao ShukkaDetailDao;

        /// <summary>
        /// 売上/支払DAO
        /// </summary>
        public IT_UrShDao UrShDao;

        /// <summary>
        /// 売上/支払明細DAO
        /// </summary>
        private IT_UrShDetailDao UrShDetailDao;

        /// <summary>
        /// 請求明細DAO
        /// </summary>
        private IT_SeikyuuDetailDao SeikyuuDetailDao;

        /// <summary>
        /// 清算明細DAO
        /// </summary>
        private IT_SeisanDetailDao SeisanDetailDao;

        /// <summary>
        /// 入金DAO
        /// </summary>
        public IT_NyuukinDao NyuukinDao;

        /// <summary>
        /// 入金明細DAO
        /// </summary>
        private IT_NyuukinDetailDao NyuukinDetailDao;

        /// <summary>
        /// 出金DAO
        /// </summary>
        private IT_ShukkinDao ShukkinDao;

        /// <summary>
        /// 出金明細DAO
        /// </summary>
        private IT_ShukkinDetailDao ShukkinDetailDao;

        /// <summary>
        /// 取引先DAO
        /// </summary>
        private IM_TorihikisakiDao TorihikisakiDao;

        /// <summary>
        /// 業者DAO
        /// </summary>
        private IM_GyoushaDao GyoushaInfoDao;

        /// <summary>
        /// 現場DAO
        /// </summary>
        private IM_GenbaDao GenbaInfoDao;

        /// <summary>
        /// 個別品名単価DAO
        /// </summary>
        private IM_KobetsuHinmeiTankaDao KobetsuHinmeiTankaDao;

        /// <summary>
        /// 取引先請求DAO
        /// </summary>
        private IM_TorihikisakiSeikyuuDao TorihikisakiSeikyuuDao;

        /// <summary>
        /// 取引先支払DAO
        /// </summary>
        private IM_TorihikisakiShiharaiDao TorihikisakiShiharaiDao;

        /// <summary>
        /// 委託契約DAO
        /// </summary>
        private IM_ItakuKeiyakuDao ItakuKeiyakuDao;

        /// <summary>
        /// 顧客カルテ汎用Dao
        /// </summary>
        private IM_KokyakuKaruteDao kokyakuKaruteDao;

        /// <summary>
        /// 拠点Dao
        /// </summary>
        private IM_KYOTENDao kyotenDao;

        /// <summary>
        /// 都道府県Dao
        /// </summary>
        private IM_TODOUFUKENDao todoufukenDao;

        /// <summary>
        /// Form
        /// </summary>
        private G173Form form;

        /// <summary>
        /// Header
        /// </summary>
        public G173HeaderForm header;

        /// <summary>
        /// タブコントロール制御用
        /// </summary>
        TabPageManager _tabPageManager = null;

        /// <summary>
        /// 取引先のDao
        /// </summary>
        private IM_TORIHIKISAKIDao daoTorisaki;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao daoGyousha;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao daoGenba;

        /// <summary>
        /// メニュー権限のDao
        /// </summary>
        internal IM_MENU_AUTHDao daoAuth;

        internal ContenaDao daoContena;
        internal SuuryouKanriDAOClass suuryouKanriDao;
        #endregion

        #region プロパティ

        /// <summary>
        /// 受付検索結果
        /// </summary>
        internal DataTable UketsukeSearchResult { get; set; }

        /// <summary>
        /// 受付(収集)明細検索結果
        /// </summary>
        internal DataTable UketsukeSSDetailSearchResult { get; set; }

        /// <summary>
        /// 受付(出荷)明細検索結果
        /// </summary>
        internal DataTable UketsukeSKDetailSearchResult { get; set; }

        /// <summary>
        /// 受付(持込)明細検索結果
        /// </summary>
        internal DataTable UketsukeMKDetailSearchResult { get; set; }

        /// <summary>
        /// 受付(物販)明細検索結果
        /// </summary>
        internal DataTable UketsukeBPDetailSearchResult { get; set; }

        /// <summary>
        /// 受付クレーム検索結果
        /// </summary>
        internal DataTable UketsukeCMSearchResult { get; set; }

        /// <summary>
        /// 計量検索結果
        /// </summary>
        internal DataTable KeiryouSearchResult { get; set; }

        /// <summary>
        /// 計量明細検索結果
        /// </summary>
        internal DataTable KeiryouDetailSearchResult { get; set; }

        /// <summary>
        /// 受入検索結果
        /// </summary>
        internal DataTable UkeireSearchResult { get; set; }

        /// <summary>
        /// 受入明細検索結果
        /// </summary>
        internal DataTable UkeireDetailSearchResult { get; set; }

        /// <summary>
        /// 出荷検索結果
        /// </summary>
        internal DataTable ShukkaSearchResult { get; set; }

        /// <summary>
        /// 出荷明細検索結果
        /// </summary>
        internal DataTable ShukkaDetailSearchResult { get; set; }

        /// <summary>
        /// 売上/支払検索結果
        /// </summary>
        internal DataTable UrShSearchResult { get; set; }

        /// <summary>
        /// 売上/支払明細検索結果
        /// </summary>
        internal DataTable UrShDetailSearchResult { get; set; }

        /// <summary>
        /// 代納検索結果
        /// </summary>
        internal DataTable DainouSearchResult { get; set; }

        /// <summary>
        /// 代納明細検索結果
        /// </summary>
        internal DataTable DainouDetailSearchResult { get; set; }

        /// <summary>
        /// 入金検索結果
        /// </summary>
        internal DataTable NyuukinSearchResult { get; set; }

        /// <summary>
        /// 入金明細検索結果
        /// </summary>
        internal DataTable NyuukinDetailSearchResult { get; set; }

        /// <summary>
        /// 出金検索結果
        /// </summary>
        internal DataTable ShukkinSearchResult { get; set; }

        /// <summary>
        /// 出金明細検索結果
        /// </summary>
        internal DataTable ShukkinDetailSearchResult { get; set; }

        /// <summary>
        /// 検収検索結果
        /// </summary>
        internal DataTable ShukkaKenshuuSearchResult { get; set; }

        /// <summary>
        /// 検収検索結果
        /// </summary>
        internal DataTable ShukkaKenshuuDetailSearchResult { get; set; }

        /// <summary>
        /// 取引先検索結果
        /// </summary>
        internal M_TORIHIKISAKI TorihikisakiSearchResult { get; set; }
        internal DataTable TorihikisakiHeaderSearchResult { get; set; }
        internal DataTable TorihikisakiKihonSearchResult { get; set; }
        internal DataTable TorihikisakiSeikyuuSearchResult { get; set; }
        internal DataTable TorihikisakiShiharaiSearchResult { get; set; }
        internal DataTable TorihikisakiGyoushaIchiranSearchResult { get; set; }

        /// <summary>
        ///業者検索結果
        /// </summary>
        internal DataTable GyoushaSearchResult { get; set; }
        internal DataTable GyoushaInfoSearchResult { get; set; }
        internal DataTable GyoushaGenbaIchiranSearchResult { get; set; }

        /// <summary>
        /// 現場マスタ検索結果
        /// </summary>
        internal DataTable GenbaSearchResult { get; set; }
        internal DataTable GenbaInfoSearchResult { get; set; }

        /// <summary>
        /// 個別品名単価検索結果
        /// </summary>
        internal DataTable KobetsuHinmeiTankaUriageSearchResult { get; set; }
        internal DataTable KobetsuHinmeiTankaShiharaiSearchResult { get; set; }

        /// <summary>
        /// 委託契約（業者）検索結果
        /// </summary>
        internal DataTable GyoushaItakuKeiyakuSearchResult { get; set; }

        /// <summary>
        /// 委託契約（現場）検索結果
        /// </summary>
        internal DataTable GenbaItakuKeiyakuSearchResult { get; set; }

        /// <summary>
        /// コンテナ
        /// </summary>
        internal DataTable ContenaSearchResult { get; set; }

        /// <summary>
        /// 受付検索されたかフラグ
        /// </summary>
        internal bool UketsukeFlg { get; set; }

        /// <summary>
        /// 受付クレーム検索されたかフラグ
        /// </summary>
        internal bool UketsukeCMFlg { get; set; }

        /// <summary>
        /// 計量検索されたかフラグ
        /// </summary>
        internal bool KeiryouFlg { get; set; }

        /// <summary>
        /// 受入検索されたかフラグ
        /// </summary>
        internal bool UkeireFlg { get; set; }

        /// <summary>
        /// 出荷検索されたかフラグ
        /// </summary>
        internal bool ShukkaFlg { get; set; }

        // <summary>
        /// 売上/支払検索されたかフラグ
        /// </summary>
        internal bool UrShFlg { get; set; }

        // <summary>
        /// 代納検索されたかフラグ
        /// </summary>
        internal bool DainouFlg { get; set; }

        // <summary>
        /// 入金検索されたかフラグ
        /// </summary>
        internal bool NyuukinFlg { get; set; }

        // <summary>
        /// 出金検索されたかフラグ
        /// </summary>
        internal bool ShukkinFlg { get; set; }

        /// <summary>
        /// 検収検索されたかフラグ
        /// </summary>
        internal bool ShukkaKenshuuFlg { get; set; }

        // <summary>
        /// 取引先検索されたかフラグ
        /// </summary>
        internal bool TorihikisakiFlg { get; set; }

        // <summary>
        /// 業者検索されたかフラグ
        /// </summary>
        internal bool GyoushaFlg { get; set; }

        // <summary>
        /// 現場検索されたかフラグ
        /// </summary>
        internal bool GenbaFlg { get; set; }

        /// <summary>
        /// コンテナ検索されたかフラグ
        /// </summary>
        internal bool ContenaFlg { get; set; }

        /// <summary>
        /// 取引先CD
        /// </summary>
        internal string TorihikisakiCD { get; set; }

        /// <summary>
        /// 業者CD
        /// </summary>
        internal string GyoushaCD { get; set; }

        /// <summary>
        /// 現場CD
        /// </summary>
        internal string GenbaCD { get; set; }

        /// <summary>
        /// 拠点CD
        /// </summary>
        internal string KyotenCD { get; set; }

        /// <summary>
        /// From date
        /// </summary>
        internal string FromDate { get; set; }

        /// <summary>
        /// To date
        /// </summary>
        internal string ToDate { get; set; }

        /// <summary>
        /// 現場検索条件
        /// </summary>
        internal M_GENBA GenbaSearchString { get; set; }

        /// <summary>
        /// 個別品名単価検索条件
        /// </summary>
        internal M_KOBETSU_HINMEI_TANKA KobetsuHinmeiTankaSearchString { get; set; }

        internal ContenaDTO SearchString { get; set; }

        internal List<M_MENU_AUTH> _listAuth = new List<M_MENU_AUTH>();
        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public G173Logic(G173Form targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            //DAOのインスタンス
            this.sysInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SYS_INFODao>();
            this.TorihikisakiDao = DaoInitUtility.GetComponent<IM_TorihikisakiDao>();
            this.KobetsuHinmeiTankaDao = DaoInitUtility.GetComponent<IM_KobetsuHinmeiTankaDao>();
            this.TorihikisakiSeikyuuDao = DaoInitUtility.GetComponent<IM_TorihikisakiSeikyuuDao>();
            this.TorihikisakiShiharaiDao = DaoInitUtility.GetComponent<IM_TorihikisakiShiharaiDao>();
            this.GyoushaInfoDao = DaoInitUtility.GetComponent<IM_GyoushaDao>();
            this.GenbaInfoDao = DaoInitUtility.GetComponent<IM_GenbaDao>();
            this.UketsukeDao = DaoInitUtility.GetComponent<IT_UketsukeDao>();
            this.UketsukeSSDetailDao = DaoInitUtility.GetComponent<IT_UketsukeSSDetailDao>();
            this.UketsukeSKDetailDao = DaoInitUtility.GetComponent<IT_UketsukeSKDetailDao>();
            this.UketsukeMKDetailDao = DaoInitUtility.GetComponent<IT_UketsukeMKDetailDao>();
            this.UketsukeCMDao = DaoInitUtility.GetComponent<IT_UketsukeCMDao>();
            this.UrShDao = DaoInitUtility.GetComponent<IT_UrShDao>();
            this.UrShDetailDao = DaoInitUtility.GetComponent<IT_UrShDetailDao>();
            this.SeikyuuDetailDao = DaoInitUtility.GetComponent<IT_SeikyuuDetailDao>();
            this.SeisanDetailDao = DaoInitUtility.GetComponent<IT_SeisanDetailDao>();
            this.NyuukinDao = DaoInitUtility.GetComponent<IT_NyuukinDao>();
            this.NyuukinDetailDao = DaoInitUtility.GetComponent<IT_NyuukinDetailDao>();
            this.ShukkinDao = DaoInitUtility.GetComponent<IT_ShukkinDao>();
            this.ShukkinDetailDao = DaoInitUtility.GetComponent<IT_ShukkinDetailDao>();
            this.KeiryouDao = DaoInitUtility.GetComponent<IT_KeiryouDao>();
            this.KeiryouDetailDao = DaoInitUtility.GetComponent<IT_KeiryouDetailDao>();
            this.UkeireDao = DaoInitUtility.GetComponent<IT_UkeireDao>();
            this.UkeireDetailDao = DaoInitUtility.GetComponent<IT_UkeireDetailDao>();
            this.ShukkaDao = DaoInitUtility.GetComponent<IT_ShukkaDao>();
            this.ShukkaDetailDao = DaoInitUtility.GetComponent<IT_ShukkaDetailDao>();
            this.ItakuKeiyakuDao = DaoInitUtility.GetComponent<IM_ItakuKeiyakuDao>();
            this.kokyakuKaruteDao = DaoInitUtility.GetComponent<IM_KokyakuKaruteDao>();
            this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.todoufukenDao = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            this._tabPageManager = new TabPageManager(this.form.TabControl_Genba);
            this.daoTorisaki = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.daoGyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.daoGenba = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.daoAuth = DaoInitUtility.GetComponent<IM_MENU_AUTHDao>();
            this.daoContena = DaoInitUtility.GetComponent<ContenaDao>();
            this.suuryouKanriDao = DaoInitUtility.GetComponent<SuuryouKanriDAOClass>();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 初期化

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            LogUtility.DebugMethodStart();

            // システム情報を取得
            this.GetSysInfoInit();

            //Header Init
            this.HeaderInit();

            if (this.sysInfoEntity.GYOUSHA_TORIHIKI_CHUUSHI.IsNull)
            {
                this.form.TORIHIKI_STOP.Text = string.Empty;
                this.form.GYOUSHA_TORIHIKI_STOP.Text = string.Empty;
            }
            else
            {
                this.form.TORIHIKI_STOP.Text = this.sysInfoEntity.GYOUSHA_TORIHIKI_CHUUSHI.ToString();
                this.form.GYOUSHA_TORIHIKI_STOP.Text = this.sysInfoEntity.GYOUSHA_TORIHIKI_CHUUSHI.ToString();
            }

            // 受付明細のフォーマット設定
            this.form.Uketsuke_Meisai.Columns["UKETUKE_MEISAI_SHURYOU"].DefaultCellStyle.Format = "#,##0.###";
            this.form.Uketsuke_Meisai.Columns["UKETUKE_MEISAI_TANKA"].DefaultCellStyle.Format = "#,##0.###";

            // 売上／支払明細のフォーマット設定
            this.form.UriageShiharai_Meisai.Columns["URIAGESIHARAI_MEISAI_SHURYOU"].DefaultCellStyle.Format = "#,##0.###";
            this.form.UriageShiharai_Meisai.Columns["URIAGESIHARAI_MEISAI_TANKA"].DefaultCellStyle.Format = "#,##0.###";

            //数量のフォーマットを設定
            //this.form.Uketsuke_Meisai.Columns["UKETUKE_MEISAI_SHURYOU"].DefaultCellStyle.Format = this.ChgDBNullToValue(sysInfoEntity.SYS_SUURYOU_FORMAT, string.Empty).ToString();
            this.form.Genba_TsukiHinmei_Ichiran.Columns["TSUKIHINMEI_TANKA"].DefaultCellStyle.Format = "#,##0.###";
            this.form.Genba_TsukiHinmei_Ichiran.Columns["CHOUKA_LIMIT_AMOUNT"].DefaultCellStyle.Format = "#,##0.###";

            // ボタンのテキストを初期化
            this.ButtonInit();

            // イベントの初期化処理
            this.EventInit();

            // ヘッダーテキスト配置位置を変更
            this.form.Genba_TsukiHinmei_Ichiran.Columns["TSUKIHINMEI_HINMEI"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.form.Genba_TsukiHinmei_Ichiran.Columns["TSUKIHINMEI_DENPYOU_KBN"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.form.Genba_TsukiHinmei_Ichiran.Columns["TSUKIHINMEI_TANI"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.form.Genba_TsukiHinmei_Ichiran.Columns["TSUKIHINMEI_TANKA"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.form.Genba_TsukiHinmei_Ichiran.Columns["CHOUKA_LIMIT_AMOUNT"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.form.Genba_TsukiHinmei_Ichiran.Columns["CHOUKA_HINMEI_NAME"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // システム設定の設定に従ってA票～E票タブを非表示にする。
            this.ChangeTabAtoE();

            // 検索フラグ初期化
            this.FlgInit();

            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            // 画面起動時に電子請求書で追加するコントロール・項目の表示/非表示を切り替える
            this.setDensiSeikyushoVisible();
            // 20160429 koukoukon v2.1_電子請求書 #16612 end

            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
            this.SetInxsSeikyushoVisible();
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
            this.SetInxsShiharaiMesaishoVisible();
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

            this.setOnlineBankVisible();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
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
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            // 伝票ボタン(F1)イベント生成
            parentForm.bt_func1.Click += new EventHandler(this.form.Tab_Denpyou_Click);

            //取引先ボタン(F2)イベント生成
            parentForm.bt_func2.Click += new EventHandler(this.form.Tab_TorihikisakiMaster_Click);

            //業者ボタン(F3)イベント生成
            parentForm.bt_func3.Click += new EventHandler(this.form.Tab_GyoushaMaster_Click);

            //現場(F4)イベント生成
            parentForm.bt_func4.Click += new EventHandler(this.form.Tab_GenbaMaster_Click);

            //単価ボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.Tab_Tanka_Click);

            //条件ｸﾘｱボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.Conditions_Clear_Click);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.UketsukeSearch);

            //複写ボタン(F9)イベント生成
            parentForm.bt_func9.Click += new EventHandler(this.form.Copy_Click);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            //データ移動(1)イベント生成
            parentForm.bt_process1.Click += new EventHandler(this.form.MoveData_Click);

            //日付Toイベント生成
            this.header.DATE_TO.MouseDoubleClick += new MouseEventHandler(DATE_TO_MouseDoubleClick);

            //受入出荷画面サイズ選択取得
            HearerSysInfoInit();

            LogUtility.DebugMethodEnd();
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

        /// <summary>
        /// 検索フラグ初期化
        /// </summary>
        internal void FlgInit()
        {
            LogUtility.DebugMethodStart();
            //受付クレーム
            this.UketsukeCMFlg = false;
            //受付
            this.UketsukeFlg = false;
            //売上支払
            this.UrShFlg = false;
            //代納
            this.DainouFlg = false;
            //入金
            this.NyuukinFlg = false;
            //出金
            this.ShukkinFlg = false;
            //計量
            this.KeiryouFlg = false;
            //受入
            this.UkeireFlg = false;
            //出荷
            this.ShukkaFlg = false;
            //検収
            this.ShukkaKenshuuFlg = false;
            //取引先
            this.TorihikisakiFlg = false;
            //業者
            this.GyoushaFlg = false;
            //現場
            this.GenbaFlg = false;
            //コンテナ
            this.ContenaFlg = false;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 各タブーの一覧、明細を初期化
        /// </summary>
        internal void MeisaiInit()
        {
            LogUtility.DebugMethodStart();
            //受付
            if (null != this.UketsukeSearchResult)
            {
                this.UketsukeSearchResult.Clear();
            }
            //受付（収集）明細
            if (null != this.UketsukeSSDetailSearchResult)
            {
                this.UketsukeSSDetailSearchResult.Clear();
            }
            //受付（出荷）明細
            if (null != this.UketsukeSKDetailSearchResult)
            {
                this.UketsukeSKDetailSearchResult.Clear();
            }
            //受付（持込）明細
            if (null != this.UketsukeMKDetailSearchResult)
            {
                this.UketsukeMKDetailSearchResult.Clear();
            }
            //受付（物販）明細
            if (null != this.UketsukeBPDetailSearchResult)
            {
                this.UketsukeBPDetailSearchResult.Clear();
            }

            //受付クレーム
            if (null != this.UketsukeCMSearchResult)
            {
                this.UketsukeCMSearchResult.Clear();
            }
            //受付クレーム明細
            this.UketsukeCMMeisaiCrear();

            //計量
            if (null != this.KeiryouSearchResult)
            {
                this.KeiryouSearchResult.Clear();
            }
            //計量明細
            if (null != this.KeiryouDetailSearchResult)
            {
                this.KeiryouDetailSearchResult.Clear();
            }
            //受入
            if (null != this.UkeireSearchResult)
            {
                this.UkeireSearchResult.Clear();
            }
            //受入明細
            if (null != this.UkeireDetailSearchResult)
            {
                this.UkeireDetailSearchResult.Clear();
            }
            //出荷
            if (null != this.ShukkaSearchResult)
            {
                this.ShukkaSearchResult.Clear();
            }
            //出荷明細
            if (null != this.ShukkaDetailSearchResult)
            {
                this.ShukkaDetailSearchResult.Clear();
            }
            //売上支払
            if (null != this.UrShSearchResult)
            {
                this.UrShSearchResult.Clear();
            }
            //売上支払明細
            if (null != this.UrShDetailSearchResult)
            {
                this.UrShDetailSearchResult.Clear();
            }
            //代納
            if (null != this.DainouSearchResult)
            {
                this.DainouSearchResult.Clear();
            }
            //代納明細
            if (null != this.DainouDetailSearchResult)
            {
                this.DainouDetailSearchResult.Clear();
            }
            //入金
            if (null != this.NyuukinSearchResult)
            {
                this.NyuukinSearchResult.Clear();
            }
            //入金明細
            if (null != this.NyuukinDetailSearchResult)
            {
                this.NyuukinDetailSearchResult.Clear();
            }
            //出金
            if (null != this.ShukkinSearchResult)
            {
                this.ShukkinSearchResult.Clear();
            }
            //出金明細
            if (null != this.ShukkinDetailSearchResult)
            {
                this.ShukkinDetailSearchResult.Clear();
            }
            //検収
            if (null != this.ShukkaKenshuuSearchResult)
            {
                this.ShukkaKenshuuSearchResult.Clear();
            }
            //検収明細
            if (null != this.ShukkaKenshuuDetailSearchResult)
            {
                this.ShukkaKenshuuDetailSearchResult.Clear();
            }
            //コンテナ
            if (this.ContenaSearchResult != null)
            {
                this.ContenaSearchResult.Clear();
            }
            //取引先ヘッダ情報
            if (null != this.TorihikisakiHeaderSearchResult)
            {
                this.TorihikisakiHeaderSearchResult.Clear();
            }
            //取引先基本情報
            if (null != this.TorihikisakiKihonSearchResult)
            {
                this.TorihikisakiKihonSearchResult.Clear();
            }
            //取引先請求情報
            if (null != this.TorihikisakiSeikyuuSearchResult)
            {
                this.TorihikisakiSeikyuuSearchResult.Clear();
            }
            //取引先支払情報
            if (null != this.TorihikisakiShiharaiSearchResult)
            {
                this.TorihikisakiShiharaiSearchResult.Clear();
            }
            //取引先業者一覧
            if (null != this.TorihikisakiGyoushaIchiranSearchResult)
            {
                this.TorihikisakiGyoushaIchiranSearchResult.Clear();
            }
            //業者の基本、請求、支払情報
            if (null != this.GyoushaInfoSearchResult)
            {
                this.GyoushaInfoSearchResult.Clear();
            }
            //業者現場一覧
            if (null != this.GyoushaGenbaIchiranSearchResult)
            {
                this.GyoushaGenbaIchiranSearchResult.Clear();
            }
            //現場の基本、請求、支払情報
            if (null != this.GenbaInfoSearchResult)
            {
                this.GenbaInfoSearchResult.Clear();
            }
            if (this.form.tabControl1.TabPages.ContainsKey("tabPage_torihikisaki"))
            {
                //取引先ヘッダ情報
                this.TorihikisakiHeaderCrear();
                //取引先基本情報
                this.TorihikisakiKihonCrear();
                //取引先請求情報1
                this.TorihikisakiSeikyuuCrear();
                //取引先請求情報2
                this.TorihikisakiSeikyuuCrear2();
                //取引先支払情報1
                this.TorihikisakiShiharaiCrear();
                //取引先支払情報2
                this.TorihikisakiShiharaiCrear2();
                //取引先分類情報
                this.TorihikisakiBunruiCrear();
                //取引先業者一覧
                this.TorihikisakiGyoushaIchiranCrear();
            }
            if (this.form.tabControl1.TabPages.ContainsKey("tabPage_gyousha"))
            {
                //業者ヘッダ情報
                this.GyoushaHeaderCrear();
                //業者基本情報
                this.GyoushaKihonCrear();
                //業者請求情報
                this.GyoushaSeikyuuCrear();
                //業者支払情報
                this.GyoushaShiharaiCrear();
                //業者現場一覧
                this.GyoushaGenbaIchiranCrear();
                //業者分類情報
                this.GyoushaBunruiCrear();
            }
            //業者・委託契約
            if (null != this.GyoushaItakuKeiyakuSearchResult)
            {
                this.GyoushaItakuKeiyakuSearchResult.Clear();
            }
            if (this.form.tabControl1.TabPages.ContainsKey("tabPage_genba"))
            {
                //現場ヘッダ情報
                this.GenbaHeaderCrear();
                //現場基本情報
                this.GenbaKihonCrear();
                //現場請求情報
                this.GenbaSeikyuuCrear();
                //現場支払情報
                this.GenbaShiharaiCrear();
                //現場分類情報
                this.GenbaBunruiCrear();
            }
            //現場・委託契約
            if (null != this.GenbaItakuKeiyakuSearchResult)
            {
                this.GenbaItakuKeiyakuSearchResult.Clear();
            }
            //単価
            if (null != this.KobetsuHinmeiTankaUriageSearchResult)
            {
                this.KobetsuHinmeiTankaUriageSearchResult.Clear();
            }
            if (null != this.KobetsuHinmeiTankaShiharaiSearchResult)
            {
                this.KobetsuHinmeiTankaShiharaiSearchResult.Clear();
            }
            // 現場A票返送先～E票返送先情報
            this.GenbaManiHensousakiCrear();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 検索条件設定

        /// <summary>
        /// 検索条件:現場の設定
        /// </summary>
        public bool SetGenbaSearchString()
        {
            LogUtility.DebugMethodStart();

            M_GENBA GenbaEntity = new M_GENBA();

            //取引先CD
            string trihikisaki_cd = this.form.TORIHIKISAKI_CD.Text;
            //業者CD
            string gyousha_cd = this.form.GYOUSHA_CD.Text;
            //現場CD
            string genba_cd = this.form.GENBA_CD.Text;

            //現場
            GenbaEntity.TORIHIKISAKI_CD = trihikisaki_cd;
            GenbaEntity.GYOUSHA_CD = gyousha_cd;
            GenbaEntity.GENBA_CD = genba_cd;
            this.GenbaSearchString = GenbaEntity;

            LogUtility.DebugMethodEnd();
            return true;
        }

        /// <summary>
        /// 検索条件:エンティティ毎にの設定
        /// </summary>
        /// <param name="entityName"></param>
        public bool SetSearchString(string entityName)
        {
            LogUtility.DebugMethodStart(entityName);
            //引数のエンティティ名を大文字にする
            string ENTITY_NAME = "";
            if (null != entityName && !"".Equals(entityName) && !DBNull.Value.Equals(entityName))
            {
                ENTITY_NAME = entityName.ToUpper();
            }
            //取引先CD
            string trihikisaki_cd = this.TorihikisakiCD;
            //業者CD
            string gyousha_cd = this.GyoushaCD;
            //現場CD
            string genba_cd = this.GenbaCD;
            if ("M_KOBETSU_HINMEI_TANKA".Equals(ENTITY_NAME))//個別品名単価
            {
                M_KOBETSU_HINMEI_TANKA Entity = new M_KOBETSU_HINMEI_TANKA();
                //取引先CD
                if (null != trihikisaki_cd && !"".Equals(trihikisaki_cd) && !DBNull.Value.Equals(trihikisaki_cd))
                {
                    Entity.TORIHIKISAKI_CD = trihikisaki_cd;
                }
                //業者CD
                if (null != gyousha_cd && !"".Equals(gyousha_cd) && !DBNull.Value.Equals(gyousha_cd))
                {
                    Entity.GYOUSHA_CD = gyousha_cd;
                }
                //現場CD
                if (null != genba_cd && !"".Equals(genba_cd) && !DBNull.Value.Equals(genba_cd))
                {
                    Entity.GENBA_CD = genba_cd;
                }

                //条件セット
                this.KobetsuHinmeiTankaSearchString = Entity;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion

        #region ボタン設定

        /// <summary>
        /// フッタの[F2取引先],[F3業者],[F4現場]を利用可にする
        /// </summary>
        internal void F2F3F4_Enabled()
        {
            LogUtility.DebugMethodStart();
            //取引先CD
            string trihikisaki_cd = this.form.TORIHIKISAKI_CD.Text;
            //業者CD
            string gyousha_cd = this.form.GYOUSHA_CD.Text;
            //現場CD
            string genba_cd = this.form.GENBA_CD.Text;
            //親フォーム
            var parentForm = (BusinessBaseForm)this.form.Parent;
            //取引先
            if (null != trihikisaki_cd && !"".Equals(trihikisaki_cd) && !DBNull.Value.Equals(trihikisaki_cd))
            {
                parentForm.bt_func2.Enabled = true;
            }
            //業者
            if (null != gyousha_cd && !"".Equals(gyousha_cd) && !DBNull.Value.Equals(gyousha_cd))
            {
                parentForm.bt_func3.Enabled = true;
            }
            //現場
            if (null != genba_cd && !"".Equals(genba_cd) && !DBNull.Value.Equals(genba_cd))
            {
                parentForm.bt_func4.Enabled = true;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 検索条件マスタ検索

        /// <summary>
        /// 取引先マスタ検索
        /// </summary>
        internal int TorihikisakiSearch()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //取引先CD
                string torihikisaki_cd = this.form.TORIHIKISAKI_CD.Text;

                //取引先検索結果
                this.TorihikisakiSearchResult = this.TorihikisakiDao.GetTorihikisakiData(torihikisaki_cd);
                //
                if (this.TorihikisakiSearchResult == null)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        /// <summary>
        /// 業者マスタ検索
        /// </summary>
        internal int GyoushaSearch()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //業者CD
                string gyousha_cd = this.form.GYOUSHA_CD.Text;

                //業者検索結果

                this.GyoushaSearchResult = this.GyoushaInfoDao.GetDataBySqlFile2(gyousha_cd);
                //

                if (this.GyoushaSearchResult == null || this.GyoushaSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        /// <summary>
        /// 現場マスタ検索
        /// </summary>
        internal int GenbaSearch()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //現場検索の条件設定
                this.SetGenbaSearchString();

                //現場検索結果
                this.GenbaSearchResult = this.GenbaInfoDao.GetDataBySqlFile2(this.GenbaSearchString);
                //
                if (this.GenbaSearchResult == null || this.GenbaSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        #endregion

        #region 検索：伝票タブ

        #region [伝票タブ]受付検索

        /// <summary>
        /// 受付検索
        /// </summary>
        internal int UketsukeSearch()
        {
            LogUtility.DebugMethodStart();
            //取引先CD
            string torihikisaki_cd = this.TorihikisakiCD;
            //業者CD
            string gyousha_cd = this.GyoushaCD;
            //現場CD
            string genba_cd = this.GenbaCD;
            //拠点CD
            string kyoten_cd = this.KyotenCD;
            //From date
            string from_date = this.FromDate;
            //To date
            string to_date = this.ToDate;
            try
            {
                //受付検索結果
                this.UketsukeSearchResult = this.UketsukeDao.GetDataBySqlFile(torihikisaki_cd, gyousha_cd, genba_cd, kyoten_cd, from_date, to_date);
                //
                if (this.UketsukeSearchResult == null || this.UketsukeSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //検索フラグ
            this.UketsukeFlg = true;

            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        /// <summary>
        /// 受付(収集)明細検索
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        internal int UketsukeSSDetailSearch(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //受付(収集)明細検索結果
                this.UketsukeSSDetailSearchResult = this.UketsukeSSDetailDao.GetDataBySqlFile(systemId, seq);
                //
                if (this.UketsukeSSDetailSearchResult == null || this.UketsukeSSDetailSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        /// <summary>
        /// 受付(出荷)明細検索
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        internal int UketsukeSKDetailSearch(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //受付(出荷)明細検索結果
                this.UketsukeSKDetailSearchResult = this.UketsukeSKDetailDao.GetDataBySqlFile(systemId, seq);
                //
                if (this.UketsukeSKDetailSearchResult == null || this.UketsukeSKDetailSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        /// <summary>
        /// 受付(持込)明細検索
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        internal int UketsukeMKDetailSearch(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //受付(持込)明細検索結果
                this.UketsukeMKDetailSearchResult = this.UketsukeMKDetailDao.GetDataBySqlFile(systemId, seq);
                //
                if (this.UketsukeMKDetailSearchResult == null || this.UketsukeMKDetailSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        #endregion

        #region [伝票タブ]受付(クレーム)検索

        /// <summary>
        /// 受付クレーム検索
        /// </summary>
        internal int UketsukeCMSearch()
        {
            LogUtility.DebugMethodStart();
            //取引先CD
            string torihikisaki_cd = this.TorihikisakiCD;
            //業者CD
            string gyousha_cd = this.GyoushaCD;
            //現場CD
            string genba_cd = this.GenbaCD;
            //拠点CD
            string kyoten_cd = this.KyotenCD;
            //From date
            string from_date = this.FromDate;
            //To date
            string to_date = this.ToDate;
            try
            {
                //受付クレーム検索結果
                this.UketsukeCMSearchResult = this.UketsukeCMDao.GetDataBySqlFile(torihikisaki_cd, gyousha_cd, genba_cd, kyoten_cd, from_date, to_date);
                //
                if (this.UketsukeCMSearchResult == null || this.UketsukeCMSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //検索フラグ
            this.UketsukeCMFlg = true;

            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        #endregion

        #region [伝票タブ]計量検索

        /// <summary>
        /// 計量検索
        /// </summary>
        internal int KeiryouSearch()
        {
            LogUtility.DebugMethodStart();
            //取引先CD
            string torihikisaki_cd = this.TorihikisakiCD;
            //業者CD
            string gyousha_cd = this.GyoushaCD;
            //現場CD
            string genba_cd = this.GenbaCD;
            try
            {
                //計量検索結果
                this.KeiryouSearchResult = this.KeiryouDao.GetDataBySqlFile(torihikisaki_cd, gyousha_cd, genba_cd);
                //
                if (this.KeiryouSearchResult == null || this.KeiryouSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //検索フラグ
            this.KeiryouFlg = true;

            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 計量明細検索
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        internal int KeiryouDetailSearch(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //計量明細検索結果
                this.KeiryouDetailSearchResult = this.KeiryouDetailDao.GetDataBySqlFile(systemId, seq);
                //
                if (this.KeiryouDetailSearchResult == null || this.KeiryouDetailSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        #endregion

        #region [伝票タブ]受入検索

        /// <summary>
        /// 受入検索
        /// </summary>
        internal int UkeireSearch()
        {
            LogUtility.DebugMethodStart();
            //取引先CD
            string torihikisaki_cd = this.TorihikisakiCD;
            //業者CD
            string gyousha_cd = this.GyoushaCD;
            //現場CDz
            string genba_cd = this.GenbaCD;
            //拠点CD
            string kyoten_cd = this.KyotenCD;
            //From date
            string from_date = this.FromDate;
            //To date
            string to_date = this.ToDate;
            try
            {
                //受入検索結果
                this.UkeireSearchResult = this.UkeireDao.GetDataBySqlFile(torihikisaki_cd, gyousha_cd, genba_cd, kyoten_cd, from_date, to_date);
                //
                if (this.UkeireSearchResult == null || this.UkeireSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                else
                {
                    // 請求明細存在チェック
                    for (int index = 0; index < this.UkeireSearchResult.Rows.Count; index++)
                    {
                        // システムID
                        long denpyouSystemId = this.UkeireSearchResult.Rows[index].Field<long>("SYSTEM_ID");
                        // 伝票番号
                        int denpyouSeq = this.UkeireSearchResult.Rows[index].Field<int>("SEQ");
                        // 状況JYOUKYOUの属性変更
                        for (int i = 0; i < this.UkeireSearchResult.Columns.Count; i++)
                        {
                            if (this.UkeireSearchResult.Columns[i].ColumnName == "JYOUKYOU_SEIKYUU"
                                || this.UkeireSearchResult.Columns[i].ColumnName == "JYOUKYOU_SEISAN"
                                || this.UkeireSearchResult.Columns[i].ColumnName == "JYOUKYOU_SALES_ZAIKO")
                            {
                                this.UkeireSearchResult.Columns[i].ReadOnly = false;
                                this.UkeireSearchResult.Columns[i].MaxLength = 10;
                            }
                        }
                        // 伝票種類CD(受入)
                        int denpyouShuruiCd = 1;

                        // 状況JYOUKYOU_SEIKYUUの値
                        if (this.SeikyuuDetailSearch2(denpyouSystemId, denpyouSeq, denpyouShuruiCd) == 0)
                        {
                            this.UkeireSearchResult.Rows[index]["JYOUKYOU_SEIKYUU"] = "未締";
                        }
                        else
                        {
                            this.UkeireSearchResult.Rows[index].SetField("JYOUKYOU_SEIKYUU", "締済");
                        }

                        // 状況JYOUKYOU_SEISANの値
                        if (this.SeisanDetailSearch(denpyouSystemId, denpyouSeq, denpyouShuruiCd) == 0)
                        {
                            this.UkeireSearchResult.Rows[index]["JYOUKYOU_SEISAN"] = "未締";
                        }
                        else
                        {
                            this.UkeireSearchResult.Rows[index].SetField("JYOUKYOU_SEISAN", "締済");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //検索フラグ
            this.UkeireFlg = true;

            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 受入明細検索
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        internal int UkeireDetailSearch(long systemId, int seq)
        {
            //LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //受入明細検索結果
                this.UkeireDetailSearchResult = this.UkeireDetailDao.GetDataBySqlFile(systemId, seq);
                //
                if (this.UkeireDetailSearchResult == null || this.UkeireDetailSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //LogUtility.DebugMethodEnd(1);
            return 1;
        }

        #endregion

        #region [伝票タブ]出荷検索

        /// <summary>
        /// 出荷検索
        /// </summary>
        internal int ShukkaSearch()
        {
            LogUtility.DebugMethodStart();
            //取引先CD
            string torihikisaki_cd = this.TorihikisakiCD;
            //業者CD
            string gyousha_cd = this.GyoushaCD;
            //現場CD
            string genba_cd = this.GenbaCD;
            //拠点CD
            string kyoten_cd = this.KyotenCD;
            //From date
            string from_date = this.FromDate;
            //To date
            string to_date = this.ToDate;
            try
            {
                //出荷検索結果
                this.ShukkaSearchResult = this.ShukkaDao.GetDataBySqlFile(torihikisaki_cd, gyousha_cd, genba_cd, kyoten_cd, from_date, to_date);
                //
                if (this.ShukkaSearchResult == null || this.ShukkaSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                else
                {
                    // 請求明細存在チェック
                    for (int index = 0; index < this.ShukkaSearchResult.Rows.Count; index++)
                    {
                        // システムID
                        long denpyouSystemId = this.ShukkaSearchResult.Rows[index].Field<long>("SYSTEM_ID");
                        // 伝票番号
                        int denpyouSeq = this.ShukkaSearchResult.Rows[index].Field<int>("SEQ");
                        // 状況JYOUKYOUの属性変更
                        for (int i = 0; i < this.ShukkaSearchResult.Columns.Count; i++)
                        {
                            if (this.ShukkaSearchResult.Columns[i].ColumnName == "JYOUKYOU_SEIKYUU"
                                || this.ShukkaSearchResult.Columns[i].ColumnName == "JYOUKYOU_SEISAN"
                                || this.ShukkaSearchResult.Columns[i].ColumnName == "JYOUKYOU_SALES_ZAIKO")
                            {
                                this.ShukkaSearchResult.Columns[i].ReadOnly = false;
                                this.ShukkaSearchResult.Columns[i].MaxLength = 10;
                            }
                        }
                        // 伝票種類CD(出荷)
                        int denpyouShuruiCd = 2;

                        // 状況JYOUKYOU_SEIKYUUの値
                        if (this.SeikyuuDetailSearch2(denpyouSystemId, denpyouSeq, denpyouShuruiCd) == 0)
                        {
                            this.ShukkaSearchResult.Rows[index]["JYOUKYOU_SEIKYUU"] = "未締";
                        }
                        else
                        {
                            this.ShukkaSearchResult.Rows[index].SetField("JYOUKYOU_SEIKYUU", "締済");
                        }

                        // 状況JYOUKYOU_SEISANの値
                        if (this.SeisanDetailSearch(denpyouSystemId, denpyouSeq, denpyouShuruiCd) == 0)
                        {
                            this.ShukkaSearchResult.Rows[index]["JYOUKYOU_SEISAN"] = "未締";
                        }
                        else
                        {
                            this.ShukkaSearchResult.Rows[index].SetField("JYOUKYOU_SEISAN", "締済");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //検索フラグ
            this.ShukkaFlg = true;

            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 出荷明細検索
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        internal int ShukkaDetailSearch(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //出荷明細検索結果
                this.ShukkaDetailSearchResult = this.ShukkaDetailDao.GetDataBySqlFile(systemId, seq);
                //
                if (this.ShukkaDetailSearchResult == null || this.ShukkaDetailSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        #endregion

        #region [伝票タブ]売上/支払検索

        /// <summary>
        /// 売上/支払検索
        /// </summary>
        internal int UrShSearch()
        {
            LogUtility.DebugMethodStart();

            //取引先CD
            string torihikisaki_cd = this.TorihikisakiCD;
            //業者CD
            string gyousha_cd = this.GyoushaCD;
            //現場CD
            string genba_cd = this.GenbaCD;
            //拠点CD
            string kyoten_cd = this.KyotenCD;
            //From date
            string from_date = this.FromDate;
            //To date
            string to_date = this.ToDate;
            try
            {
                //売上/支払検索結果
                this.UrShSearchResult = this.UrShDao.GetDataBySqlFile(torihikisaki_cd, gyousha_cd, genba_cd, kyoten_cd, from_date, to_date);
                //
                if (this.UrShSearchResult == null || this.UrShSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                else
                {
                    //請求明細存在チェック
                    for (int index = 0; index < this.UrShSearchResult.Rows.Count; index++)
                    {
                        //システムID
                        long denpyouSystemId = this.UrShSearchResult.Rows[index].Field<long>("SYSTEM_ID");
                        //伝票枝番
                        int denpyouSeq = this.UrShSearchResult.Rows[index].Field<int>("SEQ");
                        //状況JYOUKYOUの属性変更
                        for (int i = 0; i < this.UrShSearchResult.Columns.Count; i++)
                        {
                            if (this.UrShSearchResult.Columns[i].ColumnName == "JYOUKYOU_SEIKYUU"
                                || this.UrShSearchResult.Columns[i].ColumnName == "JYOUKYOU_SEISAN"
                                || this.UrShSearchResult.Columns[i].ColumnName == "JYOUKYOU_SALES_ZAIKO")
                            {
                                this.UrShSearchResult.Columns[i].ReadOnly = false;
                                this.UrShSearchResult.Columns[i].MaxLength = 10;
                            }
                        }
                        //伝票種類CD(売上支払)
                        int denpyouShuruiCd = 3;

                        //状況JYOUKYOU_SEIKYUUの値
                        if (this.SeikyuuDetailSearch2(denpyouSystemId, denpyouSeq, denpyouShuruiCd) == 0)
                        {
                            this.UrShSearchResult.Rows[index]["JYOUKYOU_SEIKYUU"] = "未締";
                        }
                        else
                        {
                            this.UrShSearchResult.Rows[index].SetField("JYOUKYOU_SEIKYUU", "締済");
                        }
                        //状況JYOUKYOU_SEISANの値
                        if (this.SeisanDetailSearch(denpyouSystemId, denpyouSeq, denpyouShuruiCd) == 0)
                        {
                            this.UrShSearchResult.Rows[index]["JYOUKYOU_SEISAN"] = "未締";
                        }
                        else
                        {
                            this.UrShSearchResult.Rows[index].SetField("JYOUKYOU_SEISAN", "締済");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //検索フラグ
            this.UrShFlg = true;

            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 売上/支払明細検索
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        internal int UrShDetailSearch(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //売上/支払明細検索結果
                this.UrShDetailSearchResult = this.UrShDetailDao.GetDataBySqlFile(systemId, seq);
                //
                if (this.UrShDetailSearchResult == null || this.UrShDetailSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                else
                {
                    //請求明細存在チェック
                    for (int index = 0; index < this.UrShDetailSearchResult.Rows.Count; index++)
                    {
                        //システムID
                        long denpyouSystemId = systemId;
                        //伝票枝番
                        int denpyouSeq = seq;
                        //明細システムID
                        long detailSystemId = UrShDetailSearchResult.Rows[index].Field<long>("DETAIL_SYSTEM_ID");
                        //伝票番号
                        long denpyouNumber = 0;
                        if (!this.UrShDetailSearchResult.Rows[index].IsNull("UR_SH_NUMBER"))
                        {
                            denpyouNumber = UrShDetailSearchResult.Rows[index].Field<long>("UR_SH_NUMBER");
                        }
                        //状況JYOUKYOUの属性変更
                        for (int i = 0; i < this.UrShDetailSearchResult.Columns.Count; i++)
                        {
                            if (this.UrShDetailSearchResult.Columns[i].ColumnName == "JYOUKYOU")
                            {
                                this.UrShDetailSearchResult.Columns[i].ReadOnly = false;
                                this.UrShDetailSearchResult.Columns[i].MaxLength = 10;
                            }
                        }
                        //伝票種類CD(売上支払)
                        int denpyouShuruiCd = 3;

                        //状況JYOUKYOUの値
                        if (this.SeikyuuDetailSearch(denpyouSystemId, denpyouSeq, detailSystemId, denpyouNumber, denpyouShuruiCd) == 0)
                        {
                            this.UrShDetailSearchResult.Rows[index]["JYOUKYOU"] = "未締";
                        }
                        else
                        {
                            this.UrShDetailSearchResult.Rows[index].SetField("JYOUKYOU", "締済");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        /// <summary>
        /// 請求明細存在チェック
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">枝番</param>
        /// <param name="detailSystemId">明細システムID</param>
        /// <param name="urShNumber">伝票番号</param>
        /// <param name="denpyouShuruiCd">伝票種類CD</param>
        internal int SeikyuuDetailSearch(long systemId, int seq, long detailSystemId, long denpyouNumber, int denpyouShuruiCd)
        {
            LogUtility.DebugMethodStart(systemId, seq, detailSystemId, denpyouNumber, denpyouShuruiCd);

            //請求明細検索結果
            int cnt = this.SeikyuuDetailDao.GetDataBySqlFile(systemId, seq, detailSystemId, denpyouNumber, denpyouShuruiCd);
            //
            if (cnt == 0)
            {
                LogUtility.DebugMethodEnd(0);
                return 0;
            }

            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 請求明細存在チェック
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">枝番</param>
        /// <param name="denpyouShuruiCd">伝票種類CD</param>
        internal int SeikyuuDetailSearch2(long systemId, int seq, int denpyouShuruiCd)
        {
            LogUtility.DebugMethodStart(systemId, seq, denpyouShuruiCd);

            //請求明細検索結果
            int cnt = this.SeikyuuDetailDao.GetDataBySqlFile2(systemId, seq, denpyouShuruiCd);
            //
            if (cnt == 0)
            {
                LogUtility.DebugMethodEnd(0);
                return 0;
            }

            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 清算明細存在チェック
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">枝番</param>
        /// <param name="denpyouShuruiCd">伝票種類CD</param>
        internal int SeisanDetailSearch(long systemId, int seq, int denpyouShuruiCd)
        {
            LogUtility.DebugMethodStart(systemId, seq, denpyouShuruiCd);

            //清算明細検索結果
            int cnt = this.SeisanDetailDao.GetDataBySqlFile(systemId, seq, denpyouShuruiCd);
            //
            if (cnt == 0)
            {
                LogUtility.DebugMethodEnd(0);
                return 0;
            }

            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        #endregion

        #region [伝票タブ]代納検索

        /// <summary>
        /// 売上/支払検索
        /// </summary>
        internal int DainouSearch()
        {
            LogUtility.DebugMethodStart();

            //取引先CD
            string torihikisaki_cd = this.TorihikisakiCD;
            //業者CD
            string gyousha_cd = this.GyoushaCD;
            //現場CD
            string genba_cd = this.GenbaCD;
            //拠点CD
            string kyoten_cd = this.KyotenCD;
            //From date
            string from_date = this.FromDate;
            //To date
            string to_date = this.ToDate;
            try
            {
                //売上/支払検索結果
                this.DainouSearchResult = this.UrShDao.GetDainouDataBySqlFile(torihikisaki_cd, gyousha_cd, genba_cd, kyoten_cd, from_date, to_date);
                //
                if (this.DainouSearchResult == null || this.DainouSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //検索フラグ
            this.DainouFlg = true;

            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 代納明細検索
        /// </summary>
        /// <param name="systemId">受入システムID</param>
        /// <param name="seq">受入SEQ</param>
        /// /// <param name="systemId">出荷システムID</param>
        /// <param name="seq">出荷SEQ</param>
        internal int DainouDetailSearch(long systemIdUkeire, int seqUkeire, long systemIdShukka, int seqShukka)
        {
            LogUtility.DebugMethodStart(systemIdUkeire, seqUkeire, systemIdShukka, seqShukka);
            try
            {
                //売上/支払明細検索結果
                this.DainouDetailSearchResult = this.UrShDetailDao.GetDainouDataBySqlFile(systemIdUkeire, seqUkeire, systemIdShukka, seqShukka);
                //
                if (this.DainouDetailSearchResult == null || this.DainouDetailSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                else
                {
                    this.DainouDetailSearchResult.Columns["ROW_NO"].ReadOnly = false;
                    for (int i = 0; i < DainouDetailSearchResult.Rows.Count; i++)
                    {
                        DainouDetailSearchResult.Rows[i]["ROW_NO"] = i + 1;
                    }
                    this.DainouDetailSearchResult.Columns["ROW_NO"].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        #endregion

        #region [伝票タブ]入金検索

        /// <summary>
        /// 入金検索
        /// </summary>
        internal int NyuukinSearch()
        {
            LogUtility.DebugMethodStart();

            //取引先CD
            string torihikisaki_cd = this.TorihikisakiCD;
            //拠点CD
            string kyoten_cd = this.KyotenCD;
            //From date
            string from_date = this.FromDate;
            //To date
            string to_date = this.ToDate;
            try
            {
                //入金検索結果
                this.NyuukinSearchResult = this.NyuukinDao.GetDataBySqlFile(torihikisaki_cd, kyoten_cd, from_date, to_date);
                //
                if (this.NyuukinSearchResult == null || this.NyuukinSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                else
                {
                    // 請求明細存在チェック
                    for (int index = 0; index < this.NyuukinSearchResult.Rows.Count; index++)
                    {
                        // システムID
                        long denpyouSystemId = this.NyuukinSearchResult.Rows[index].Field<long>("SYSTEM_ID");
                        // 伝票番号
                        int denpyouSeq = this.NyuukinSearchResult.Rows[index].Field<int>("SEQ");
                        // 状況JYOUKYOUの属性変更
                        for (int i = 0; i < this.NyuukinSearchResult.Columns.Count; i++)
                        {
                            if (this.NyuukinSearchResult.Columns[i].ColumnName == "JYOUKYOU_SEIKYUU"
                                || this.NyuukinSearchResult.Columns[i].ColumnName == "JYOUKYOU_SEISAN"
                                || this.NyuukinSearchResult.Columns[i].ColumnName == "JYOUKYOU_SALES_ZAIKO")
                            {
                                this.NyuukinSearchResult.Columns[i].ReadOnly = false;
                                this.NyuukinSearchResult.Columns[i].MaxLength = 10;
                            }
                        }
                        // 伝票種類CD(受入)
                        int denpyouShuruiCd = 10;

                        // 状況JYOUKYOU_SEIKYUUの値
                        if (this.SeikyuuDetailSearch2(denpyouSystemId, denpyouSeq, denpyouShuruiCd) == 0)
                        {
                            this.NyuukinSearchResult.Rows[index]["JYOUKYOU_SEIKYUU"] = "未締";
                        }
                        else
                        {
                            this.NyuukinSearchResult.Rows[index].SetField("JYOUKYOU_SEIKYUU", "締済");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //検索フラグ
            this.NyuukinFlg = true;

            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        /// <summary>
        /// 入金明細検索
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        internal int NyuukinDetailSearch(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //入金明細検索結果
                this.NyuukinDetailSearchResult = this.NyuukinDetailDao.GetDataBySqlFile(systemId, seq);
                //
                if (this.NyuukinDetailSearchResult == null || this.NyuukinDetailSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        #endregion

        #region [伝票タブ]出金検索

        /// <summary>
        /// 出金検索
        /// </summary>
        internal int ShukkinSearch()
        {
            LogUtility.DebugMethodStart();

            //取引先CD
            string torihikisaki_cd = this.TorihikisakiCD;
            //拠点CD
            string kyoten_cd = this.KyotenCD;
            //From date
            string from_date = this.FromDate;
            //To date
            string to_date = this.ToDate;
            try
            {
                //出金検索結果
                this.ShukkinSearchResult = this.ShukkinDao.GetDataBySqlFile(torihikisaki_cd, kyoten_cd, from_date, to_date);
                //
                if (this.ShukkinSearchResult == null || this.ShukkinSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                else
                {
                    // 請求明細存在チェック
                    for (int index = 0; index < this.ShukkinSearchResult.Rows.Count; index++)
                    {
                        // システムID
                        long denpyouSystemId = this.ShukkinSearchResult.Rows[index].Field<long>("SYSTEM_ID");
                        // 伝票番号
                        int denpyouSeq = this.ShukkinSearchResult.Rows[index].Field<int>("SEQ");
                        // 状況JYOUKYOUの属性変更
                        for (int i = 0; i < this.ShukkinSearchResult.Columns.Count; i++)
                        {
                            if (this.ShukkinSearchResult.Columns[i].ColumnName == "JYOUKYOU_SEIKYUU"
                                || this.ShukkinSearchResult.Columns[i].ColumnName == "JYOUKYOU_SEISAN"
                                || this.ShukkinSearchResult.Columns[i].ColumnName == "JYOUKYOU_SALES_ZAIKO")
                            {
                                this.ShukkinSearchResult.Columns[i].ReadOnly = false;
                                this.ShukkinSearchResult.Columns[i].MaxLength = 10;
                            }
                        }
                        // 伝票種類CD(受入)
                        int denpyouShuruiCd = 20;

                        // 状況JYOUKYOU_SEISANの値
                        if (this.SeisanDetailSearch(denpyouSystemId, denpyouSeq, denpyouShuruiCd) == 0)
                        {
                            this.ShukkinSearchResult.Rows[index]["JYOUKYOU_SEISAN"] = "未締";
                        }
                        else
                        {
                            this.ShukkinSearchResult.Rows[index].SetField("JYOUKYOU_SEISAN", "締済");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //検索フラグ
            this.ShukkinFlg = true;

            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        /// <summary>
        /// 出金明細検索
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        internal int ShukkinDetailSearch(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //出金明細検索結果
                this.ShukkinDetailSearchResult = this.ShukkinDetailDao.GetDataBySqlFile(systemId, seq);
                //
                if (this.ShukkinDetailSearchResult == null || this.ShukkinDetailSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        #endregion

        #region [伝票タブ]検収検索

        /// <summary>
        /// 出荷検収検索
        /// </summary>
        internal int ShukkaKenshuuDataSearch()
        {
            LogUtility.DebugMethodStart();
            //取引先CD
            string torihikisaki_cd = this.TorihikisakiCD;
            //業者CD
            string gyousha_cd = this.GyoushaCD;
            //現場CD
            string genba_cd = this.GenbaCD;
            //拠点CD
            string kyoten_cd = this.KyotenCD;
            //From date
            string from_date = this.FromDate;
            //To date
            string to_date = this.ToDate;
            try
            {
                //出荷検収検索結果
                this.ShukkaKenshuuSearchResult = this.ShukkaDao.GetDataBySqlFileKenshuuData(torihikisaki_cd, gyousha_cd, genba_cd, kyoten_cd, from_date, to_date);

                if (this.ShukkaKenshuuSearchResult == null || this.ShukkaKenshuuSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                else
                {
                    // 請求明細存在チェック
                    for (int index = 0; index < this.ShukkaKenshuuSearchResult.Rows.Count; index++)
                    {
                        // システムID
                        long denpyouSystemId = this.ShukkaKenshuuSearchResult.Rows[index].Field<long>("SYSTEM_ID");
                        // 伝票番号
                        int denpyouSeq = this.ShukkaKenshuuSearchResult.Rows[index].Field<int>("SEQ");
                        // 状況JYOUKYOUの属性変更
                        for (int i = 0; i < this.ShukkaKenshuuSearchResult.Columns.Count; i++)
                        {
                            if (this.ShukkaKenshuuSearchResult.Columns[i].ColumnName == "JYOUKYOU_SEIKYUU"
                                || this.ShukkaKenshuuSearchResult.Columns[i].ColumnName == "JYOUKYOU_SEISAN"
                                || this.ShukkaKenshuuSearchResult.Columns[i].ColumnName == "JYOUKYOU_SALES_ZAIKO")
                            {
                                this.ShukkaKenshuuSearchResult.Columns[i].ReadOnly = false;
                                this.ShukkaKenshuuSearchResult.Columns[i].MaxLength = 10;
                            }
                        }
                        // 伝票種類CD(出荷)
                        int denpyouShuruiCd = 2;

                        // 状況JYOUKYOU_SEIKYUUの値
                        if (this.SeikyuuDetailSearch2(denpyouSystemId, denpyouSeq, denpyouShuruiCd) == 0)
                        {
                            this.ShukkaKenshuuSearchResult.Rows[index]["JYOUKYOU_SEIKYUU"] = "未締";
                        }
                        else
                        {
                            this.ShukkaKenshuuSearchResult.Rows[index].SetField("JYOUKYOU_SEIKYUU", "締済");
                        }

                        // 状況JYOUKYOU_SEISANの値
                        if (this.SeisanDetailSearch(denpyouSystemId, denpyouSeq, denpyouShuruiCd) == 0)
                        {
                            this.ShukkaKenshuuSearchResult.Rows[index]["JYOUKYOU_SEISAN"] = "未締";
                        }
                        else
                        {
                            this.ShukkaKenshuuSearchResult.Rows[index].SetField("JYOUKYOU_SEISAN", "締済");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //検索フラグ
            this.ShukkaKenshuuFlg = true;

            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 検収検索
        /// </summary>
        internal int ShukkaKenshuuDetailSearch(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);

            try
            {
                //出荷検収明細検索結果
                this.ShukkaKenshuuDetailSearchResult = this.ShukkaDetailDao.GetShukkaKenshuuData(systemId, seq);

                if (this.ShukkaKenshuuDetailSearchResult == null || this.ShukkaKenshuuDetailSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        #endregion

        #endregion

        #region 検索：単価タブ

        /// <summary>
        /// 個別品名単価検索
        /// </summary>
        /// <param name="denPyouKbn">1=売上、2=支払</param>
        internal int KobetsuHinmeiTankaSearch(int denPyouKbnCd)
        {
            LogUtility.DebugMethodStart(denPyouKbnCd);

            try
            {
                if (false == SetSearchString("M_KOBETSU_HINMEI_TANKA"))
                {
                    LogUtility.DebugMethodEnd(-1);
                    return -1;
                }

                //個別品名単価検索結果
                if (denPyouKbnCd == 1)//売上
                {
                    this.KobetsuHinmeiTankaUriageSearchResult = this.KobetsuHinmeiTankaDao.GetDataBySqlFile(this.KobetsuHinmeiTankaSearchString, denPyouKbnCd);
                }
                else
                {//支払
                    this.KobetsuHinmeiTankaShiharaiSearchResult = this.KobetsuHinmeiTankaDao.GetDataBySqlFile(this.KobetsuHinmeiTankaSearchString, denPyouKbnCd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        #endregion

        #region 検索：取引先タブ

        #region [取引先タブ]ヘッダ検索

        /// <summary>
        /// 取引先ヘッダ情報検索
        /// </summary>
        /// <returns></returns>
        internal int TorihikisakiHeaderSearch()
        {
            LogUtility.DebugMethodStart();
            //取引先CD
            string torihikisaki_cd = this.TorihikisakiCD;
            try
            {
                //取引先検索結果
                this.TorihikisakiHeaderSearchResult = this.TorihikisakiDao.GetTorihikisakiHeaderData(torihikisaki_cd);

                if (this.TorihikisakiHeaderSearchResult == null || this.TorihikisakiHeaderSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //検索フラグ
            this.TorihikisakiFlg = true;

            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        #endregion

        #region [取引先タブ]基本情報検索

        /// <summary>
        /// 取引先基本情報検索
        /// </summary>
        internal int TorihikisakiKihonSearch()
        {
            LogUtility.DebugMethodStart();
            //取引先CD
            string torihikisaki_cd = this.TorihikisakiCD;
            try
            {
                //取引先検索結果
                this.TorihikisakiKihonSearchResult = this.TorihikisakiDao.GetTorihikisakiKihonData(torihikisaki_cd);

                if (this.TorihikisakiKihonSearchResult == null || this.TorihikisakiKihonSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //検索フラグ
            this.TorihikisakiFlg = true;

            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        #endregion

        #region [取引先タブ]請求情報検索

        /// <summary>
        /// 取引先請求情報検索
        /// </summary>
        internal int TorihikisakiSeikyuuSearch()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //取引先請求情報 検索結果
                string torihikisaki_cd = this.TorihikisakiCD; //取引先CD

                this.TorihikisakiSeikyuuSearchResult = this.TorihikisakiSeikyuuDao.GetDataBySqlFile(torihikisaki_cd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        #endregion

        #region [取引先タブ]支払情報検索

        /// <summary>
        /// 取引先支払情報検索
        /// </summary>
        internal int TorihikisakiShiharaiSearch()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //取引先支払情報 検索結果
                string torihikisaki_cd = this.TorihikisakiCD; //取引先CD

                this.TorihikisakiShiharaiSearchResult = this.TorihikisakiShiharaiDao.GetDataBySqlFile(torihikisaki_cd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        #endregion

        #region [取引先タブ]業者一覧検索

        /// <summary>
        /// データ取得処理(業者)
        /// </summary>
        /// <returns></returns>
        public int TorihikisakiGyoushaIchiranSearch()
        {
            LogUtility.DebugMethodStart();

            int count;
            try
            {
                //取引先業者一覧 検索結果
                string torihikisaki_cd = this.TorihikisakiCD; //取引先CD

                M_GYOUSHA condition = new M_GYOUSHA();
                condition.TORIHIKISAKI_CD = torihikisaki_cd;
                if (!string.IsNullOrWhiteSpace(this.form.TORIHIKI_STOP.Text))
                {
                    condition.TORIHIKI_JOUKYOU = Int16.Parse(this.form.TORIHIKI_STOP.Text);
                }

                this.TorihikisakiGyoushaIchiranSearchResult = this.GyoushaInfoDao.GetDataBySqlFile3(condition);

                count = this.TorihikisakiGyoushaIchiranSearchResult.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(count);
            return count;
        }

        #endregion

        #endregion

        #region 検索：業者タブ

        /// <summary>
        /// 業者情報検索
        /// </summary>
        internal int GyoushaInfoSearch()
        {
            LogUtility.DebugMethodStart();

            //業者情報 検索結果
            string torihikisaki_cd = this.TorihikisakiCD; //取引先CD
            string gyousha_cd = this.GyoushaCD;           //業者CD
            try
            {
                this.GyoushaInfoSearchResult = this.GyoushaInfoDao.GetDataBySqlFile(torihikisaki_cd, gyousha_cd);

                if (this.GyoushaInfoSearchResult == null || this.GyoushaInfoSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //検索フラグ
            this.GyoushaFlg = true;

            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        /// <summary>
        /// 業者現場一覧検索
        /// </summary>
        internal int GyoushaGenbaIchiranSearch()
        {
            LogUtility.DebugMethodStart();

            int count;
            try
            {
                string gyoushaCd = this.GyoushaCD;

                M_GENBA condition = new M_GENBA();
                condition.GYOUSHA_CD = gyoushaCd;
                if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_TORIHIKI_STOP.Text))
                {
                    condition.TORIHIKI_JOUKYOU = Int16.Parse(this.form.GYOUSHA_TORIHIKI_STOP.Text);
                }

                this.GyoushaGenbaIchiranSearchResult = this.GenbaInfoDao.GetDataBySqlFile3(condition);

                count = this.GyoushaGenbaIchiranSearchResult.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);
            return count;
        }

        #endregion

        #region 検索：現場タブ

        /// <summary>
        /// 現場情報検索
        /// </summary>
        internal int GenbaInfoSearch()
        {
            LogUtility.DebugMethodStart();

            //業者情報 検索結果
            string torihikisaki_cd = this.TorihikisakiCD; //取引先CD
            string gyousha_cd = this.GyoushaCD;           //業者CD
            string genba_cd = this.GenbaCD;               //現場CD
            try
            {
                this.GenbaInfoSearchResult = this.GenbaInfoDao.GetDataBySqlFile(torihikisaki_cd, gyousha_cd, genba_cd);

                if (this.GenbaInfoSearchResult == null || this.GenbaInfoSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //検索フラグ
            this.GenbaFlg = true;

            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        #endregion

        #region 検索：委託契約(業者,現場)

        /// <summary>
        /// 委託契約(業者,現場)情報検索
        /// </summary>
        /// <param name="flg">flg=1の場合、業者の委託；flg=2の場合、現場の委託</param>
        internal int ItakuKeiyakuSearch(int flg)
        {
            LogUtility.DebugMethodStart();

            //委託契約(業者)検索結果
            string gyousha_cd = this.GyoushaCD;   //業者CD
            string genba_cd = this.GenbaCD;       //現場CD
            try
            {
                //委託契約(業者)
                if (flg == 1)
                {
                    this.GyoushaItakuKeiyakuSearchResult = this.ItakuKeiyakuDao.GetDataBySqlFile(gyousha_cd, null);
                    //
                    if (this.GyoushaItakuKeiyakuSearchResult == null || this.GyoushaItakuKeiyakuSearchResult.Rows.Count == 0)
                    {
                        LogUtility.DebugMethodEnd(0);
                        return 0;
                    }
                }
                //委託契約(現場)
                if (flg == 2)
                {
                    this.GenbaItakuKeiyakuSearchResult = this.ItakuKeiyakuDao.GetDataBySqlFile(gyousha_cd, genba_cd);
                    //
                    if (this.GenbaItakuKeiyakuSearchResult == null || this.GenbaItakuKeiyakuSearchResult.Rows.Count == 0)
                    {
                        LogUtility.DebugMethodEnd(0);
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        #endregion

        #region 検索条件の項目値セット

        /// <summary>
        /// 条件の項目値セット
        /// </summary>
        internal void TorihikisakiSet()
        {
            LogUtility.DebugMethodStart();
            //取引先
            if (null != this.TorihikisakiSearchResult.TORIHIKISAKI_CD)
            {
                this.form.TORIHIKISAKI_CD.Text = this.TorihikisakiSearchResult.TORIHIKISAKI_CD;
            }
            //取引先略名
            if (null != this.TorihikisakiSearchResult.TORIHIKISAKI_NAME_RYAKU)
            {
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = this.TorihikisakiSearchResult.TORIHIKISAKI_NAME_RYAKU;
            }
            //住所１
            if (null != this.TorihikisakiSearchResult.TORIHIKISAKI_ADDRESS1)
            {
                this.form.TORIHIKISAKI_ADDRESS1.Text = this.TorihikisakiSearchResult.TORIHIKISAKI_ADDRESS1;
            }
            //住所２
            if (null != this.TorihikisakiSearchResult.TORIHIKISAKI_ADDRESS2)
            {
                this.form.TORIHIKISAKI_ADDRESS2.Text = this.TorihikisakiSearchResult.TORIHIKISAKI_ADDRESS2;
            }
            //電話番号
            if (null != this.TorihikisakiSearchResult.TORIHIKISAKI_TEL)
            {
                this.form.TORIHIKISAKI_TEL.Text = this.TorihikisakiSearchResult.TORIHIKISAKI_TEL;
            }
            //都道府県
            if (!this.TorihikisakiSearchResult.TORIHIKISAKI_TODOUFUKEN_CD.IsNull)
            {
                M_TODOUFUKEN entity = new M_TODOUFUKEN();
                entity = todoufukenDao.GetDataByCd(this.TorihikisakiSearchResult.TORIHIKISAKI_TODOUFUKEN_CD.Value.ToString());
                if (entity != null)
                {
                    this.form.TORIHIKISAKI_TODOUFUKEN.Text = entity.TODOUFUKEN_NAME_RYAKU;
                }
            }
            else
            {
                this.form.TORIHIKISAKI_TODOUFUKEN.Text = string.Empty;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 条件の業者項目値セット
        /// </summary>
        internal void GyoushaSet()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //取引先CD
                string trihikisaki_cd = this.form.TORIHIKISAKI_CD.Text;

                //業者CD
                if (!this.GyoushaSearchResult.Rows[0].IsNull("GYOUSHA_CD"))
                {
                    this.form.GYOUSHA_CD.Text = this.GyoushaSearchResult.Rows[0].Field<string>("GYOUSHA_CD");
                }

                //業者名
                if (!this.GyoushaSearchResult.Rows[0].IsNull("GYOUSHA_NAME_RYAKU"))
                {
                    this.form.GYOUSHA_NAME_RYAKU.Text = this.GyoushaSearchResult.Rows[0].Field<string>("GYOUSHA_NAME_RYAKU");
                }
                //業者住所1
                if (!this.GyoushaSearchResult.Rows[0].IsNull("GYOUSHA_ADDRESS1"))
                {
                    this.form.GYOUSHA_ADDRESS1.Text = this.GyoushaSearchResult.Rows[0].Field<string>("GYOUSHA_ADDRESS1");
                }
                //業者住所2
                if (!this.GyoushaSearchResult.Rows[0].IsNull("GYOUSHA_ADDRESS2"))
                {
                    this.form.GYOUSHA_ADDRESS2.Text = this.GyoushaSearchResult.Rows[0].Field<string>("GYOUSHA_ADDRESS2");
                }
                //業者電話
                if (!this.GyoushaSearchResult.Rows[0].IsNull("GYOUSHA_TEL"))
                {
                    this.form.GYOUSHA_TEL.Text = this.GyoushaSearchResult.Rows[0].Field<string>("GYOUSHA_TEL");
                }
                //業者都道府県
                if (!this.GyoushaSearchResult.Rows[0].IsNull("GYOUSHA_TODOUFUKEN_NAME_RYAKU"))
                {
                    this.form.GYOUSHA_TODOUFUKEN.Text = this.GyoushaSearchResult.Rows[0].Field<string>("GYOUSHA_TODOUFUKEN_NAME_RYAKU");
                }
                else
                {
                    this.form.GYOUSHA_TODOUFUKEN.Text = string.Empty;
                }

                // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(一覧タイプ) STR
                //取引先
                //if (null == trihikisaki_cd || "".Equals(trihikisaki_cd) || DBNull.Value.Equals(trihikisaki_cd))
                //{
                //    //取引先CD
                //    if (!this.GyoushaSearchResult.Rows[0].IsNull("TORIHIKISAKI_CD"))
                //    {
                //        this.form.TORIHIKISAKI_CD.Text = this.GyoushaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_CD");
                //    }

                //    //取引先名
                //    if (!this.GyoushaSearchResult.Rows[0].IsNull("TORIHIKISAKI_NAME_RYAKU"))
                //    {
                //        this.form.TORIHIKISAKI_NAME_RYAKU.Text = this.GyoushaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_NAME_RYAKU");
                //    }
                //    //取引先住所1
                //    if (!this.GyoushaSearchResult.Rows[0].IsNull("TORIHIKISAKI_ADDRESS1"))
                //    {
                //        this.form.TORIHIKISAKI_ADDRESS1.Text = this.GyoushaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_ADDRESS1");
                //    }
                //    //取引先住所2
                //    if (!this.GyoushaSearchResult.Rows[0].IsNull("TORIHIKISAKI_ADDRESS2"))
                //    {
                //        this.form.TORIHIKISAKI_ADDRESS2.Text = this.GyoushaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_ADDRESS2");
                //    }
                //    //取引先電話
                //    if (!this.GyoushaSearchResult.Rows[0].IsNull("TORIHIKISAKI_TEL"))
                //    {
                //        this.form.TORIHIKISAKI_TEL.Text = this.GyoushaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_TEL");
                //    }
                //}
                // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(一覧タイプ) END
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 条件の現場,業者、取引先項目値セット
        /// </summary>
        internal void GenbaSet()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //取引先CD
                string trihikisaki_cd = this.form.TORIHIKISAKI_CD.Text;
                //業者CD
                string gyousha_cd = this.form.GYOUSHA_CD.Text;

                //現場名
                if (!this.GenbaSearchResult.Rows[0].IsNull("GENBA_NAME_RYAKU"))
                {
                    this.form.GENBA_NAME_RYAKU.Text = this.GenbaSearchResult.Rows[0].Field<string>("GENBA_NAME_RYAKU");
                }
                //現場住所1
                if (!this.GenbaSearchResult.Rows[0].IsNull("GENBA_ADDRESS1"))
                {
                    this.form.GENBA_ADDRESS1.Text = this.GenbaSearchResult.Rows[0].Field<string>("GENBA_ADDRESS1");
                }
                //現場住所2
                if (!this.GenbaSearchResult.Rows[0].IsNull("GENBA_ADDRESS2"))
                {
                    this.form.GENBA_ADDRESS2.Text = this.GenbaSearchResult.Rows[0].Field<string>("GENBA_ADDRESS2");
                }
                //現場電話
                if (!this.GenbaSearchResult.Rows[0].IsNull("GENBA_TEL"))
                {
                    this.form.GENBA_TEL.Text = this.GenbaSearchResult.Rows[0].Field<string>("GENBA_TEL");
                }
                //現場都道府県
                if (!this.GenbaSearchResult.Rows[0].IsNull("GENBA_TODOUFUKEN_NAME_RYAKU"))
                {
                    this.form.GENBA_TODOUFUKEN.Text = this.GenbaSearchResult.Rows[0].Field<string>("GENBA_TODOUFUKEN_NAME_RYAKU");
                }
                else
                {
                    this.form.GENBA_TODOUFUKEN.Text = string.Empty;
                }
                //業者
                //if (null == gyousha_cd || "".Equals(gyousha_cd) || DBNull.Value.Equals(gyousha_cd))
                //{
                //業者CD
                if (!this.GenbaSearchResult.Rows[0].IsNull("GYOUSHA_CD"))
                {
                    this.form.GYOUSHA_CD.Text = this.GenbaSearchResult.Rows[0].Field<string>("GYOUSHA_CD").ToUpper();
                }

                //業者名
                if (!this.GenbaSearchResult.Rows[0].IsNull("GYOUSHA_NAME_RYAKU"))
                {
                    this.form.GYOUSHA_NAME_RYAKU.Text = this.GenbaSearchResult.Rows[0].Field<string>("GYOUSHA_NAME_RYAKU");
                }
                //業者住所1
                if (!this.GenbaSearchResult.Rows[0].IsNull("GYOUSHA_ADDRESS1"))
                {
                    this.form.GYOUSHA_ADDRESS1.Text = this.GenbaSearchResult.Rows[0].Field<string>("GYOUSHA_ADDRESS1");
                }
                //業者住所2
                if (!this.GenbaSearchResult.Rows[0].IsNull("GYOUSHA_ADDRESS2"))
                {
                    this.form.GYOUSHA_ADDRESS2.Text = this.GenbaSearchResult.Rows[0].Field<string>("GYOUSHA_ADDRESS2");
                }
                //業者電話
                if (!this.GenbaSearchResult.Rows[0].IsNull("GYOUSHA_TEL"))
                {
                    this.form.GYOUSHA_TEL.Text = this.GenbaSearchResult.Rows[0].Field<string>("GYOUSHA_TEL");
                }
                //業者都道府県
                if (!this.GenbaSearchResult.Rows[0].IsNull("GYOUSHA_TODOUFUKEN_NAME_RYAKU"))
                {
                    this.form.GYOUSHA_TODOUFUKEN.Text = this.GenbaSearchResult.Rows[0].Field<string>("GYOUSHA_TODOUFUKEN_NAME_RYAKU");
                }
                else
                {
                    this.form.GYOUSHA_TODOUFUKEN.Text = string.Empty;
                }

                // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(一覧タイプ) STR
                //}
                ////取引先
                //if (null == trihikisaki_cd || "".Equals(trihikisaki_cd) || DBNull.Value.Equals(trihikisaki_cd))
                //{
                ////取引先CD
                //if (!string.IsNullOrEmpty(Convert.ToString(this.GenbaSearchResult.Rows[0]["TORIHIKISAKI_CD"])))
                //{
                //    this.form.TORIHIKISAKI_CD.Text = this.GenbaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_CD").ToUpper();
                //}

                ////取引先名
                //if (!this.GenbaSearchResult.Rows[0].IsNull("TORIHIKISAKI_NAME_RYAKU"))
                //{
                //    this.form.TORIHIKISAKI_NAME_RYAKU.Text = this.GenbaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_NAME_RYAKU");
                //}
                ////取引先住所1
                //if (!this.GenbaSearchResult.Rows[0].IsNull("TORIHIKISAKI_ADDRESS1"))
                //{
                //    this.form.TORIHIKISAKI_ADDRESS1.Text = this.GenbaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_ADDRESS1");
                //}
                ////取引先住所2
                //if (!this.GenbaSearchResult.Rows[0].IsNull("TORIHIKISAKI_ADDRESS2"))
                //{
                //    this.form.TORIHIKISAKI_ADDRESS2.Text = this.GenbaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_ADDRESS2");
                //}
                ////取引先電話
                //if (!this.GenbaSearchResult.Rows[0].IsNull("TORIHIKISAKI_TEL"))
                //{
                //    this.form.TORIHIKISAKI_TEL.Text = this.GenbaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_TEL");
                //}
                //}
                // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(一覧タイプ) END
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 検索条件の項目値クリア

        /// <summary>
        /// 条件の取引先項目値クリア
        /// </summary>
        internal void TorihikisakiCrear()
        {
            LogUtility.DebugMethodStart();

            this.form.TORIHIKISAKI_CD.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.form.TORIHIKISAKI_ADDRESS1.Text = string.Empty;
            this.form.TORIHIKISAKI_ADDRESS2.Text = string.Empty;
            this.form.TORIHIKISAKI_TEL.Text = string.Empty;
            this.form.TORIHIKISAKI_TODOUFUKEN.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 条件の業者項目値クリア
        /// </summary>
        internal void GyoushaClear()
        {
            LogUtility.DebugMethodStart();

            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.form.GYOUSHA_ADDRESS1.Text = string.Empty;
            this.form.GYOUSHA_ADDRESS2.Text = string.Empty;
            this.form.GYOUSHA_TEL.Text = string.Empty;
            this.form.GYOUSHA_TODOUFUKEN.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 条件の現場項目値クリア
        /// </summary>
        internal void GenbaClear()
        {
            LogUtility.DebugMethodStart();

            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_NAME_RYAKU.Text = string.Empty;
            this.form.GENBA_ADDRESS1.Text = string.Empty;
            this.form.GENBA_ADDRESS2.Text = string.Empty;
            this.form.GENBA_TEL.Text = string.Empty;
            this.form.GENBA_TODOUFUKEN.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region タブ初期化

        #region 初期化：伝票タブ

        /// <summary>
        /// 受付クレームの内容の初期化
        /// </summary>
        internal void UketsukeCMMeisaiCrear()
        {
            LogUtility.DebugMethodStart();
            this.form.UKETSUKE_CM_NAIYOU1.Text = string.Empty;
            this.form.UKETSUKE_CM_NAIYOU2.Text = string.Empty;
            this.form.UKETSUKE_CM_NAIYOU3.Text = string.Empty;
            this.form.UKETSUKE_CM_NAIYOU4.Text = string.Empty;
            this.form.UKETSUKE_CM_NAIYOU5.Text = string.Empty;
            this.form.UKETSUKE_CM_NAIYOU6.Text = string.Empty;
            this.form.UKETSUKE_CM_NAIYOU7.Text = string.Empty;
            this.form.UKETSUKE_CM_NAIYOU8.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 初期化：取引先タブ

        /// <summary>
        /// 取引先ヘッダ情報の初期化
        /// </summary>
        internal void TorihikisakiHeaderCrear()
        {
            LogUtility.DebugMethodStart();

            // 入金先
            this.form.TORIHIKISAKI_NYUUKINSAKI_KBN.Text = string.Empty;
            // 拠点
            this.form.TORIHIKISAKI_KYOTEN_CD.Text = string.Empty;
            this.form.KYOTEN_NAME_RYAKU.Text = string.Empty;
            // 取引先CD
            this.form.TORIHIKISAKI_CD_HEADER.Text = string.Empty;
            // フリガナ
            this.form.TORIHIKISAKI_FURIGANA.Text = string.Empty;
            // 取引先名1
            this.form.TORIHIKISAKI_NAME1.Text = string.Empty;
            // 取引先敬称1
            this.form.TORIHIKISAKI_KEISHOU1.Text = string.Empty;
            // 取引先名2
            this.form.TORIHIKISAKI_NAME2.Text = string.Empty;
            // 取引先敬称2
            this.form.TORIHIKISAKI_KEISHOU2.Text = string.Empty;
            // 取引先略称
            this.form.TORIHIKISAKI_NAME_RYAKU_HEADER.Text = string.Empty;
            // 電話番号
            this.form.TORIHIKISAKI_TEL_HEADER.Text = string.Empty;
            // FAX番号
            this.form.TORIHIKISAKI_FAX.Text = string.Empty;
            // 営業担当部署
            this.form.EIGYOU_TANTOU_BUSHO_CD.Text = string.Empty;
            this.form.EIGYOU_TANTOU_BUSHO_NAME.Text = string.Empty;
            // 営業担当者
            this.form.EIGYOU_TANTOU_CD.Text = string.Empty;
            this.form.EIGYOU_TANTOU_NAME.Text = string.Empty;
            // 適用期間（開始日）
            this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Text = string.Empty;
            // 適用期間（終了日）
            this.form.TORIHIKISAKI_TEKIYOU_END.Text = string.Empty;
            // 中止理由1
            this.form.CHUUSHI_RIYUU1.Text = string.Empty;
            // 中止理由2
            this.form.CHUUSHI_RIYUU2.Text = string.Empty;
            // 最終更新
            this.form.TORIHIKISAKI_LastUpdateUser.Text = string.Empty;
            this.form.TORIHIKISAKI_LastUpdateDate.Text = string.Empty;
            // 初回登録
            this.form.TORIHIKISAKI_CreateUser.Text = string.Empty;
            this.form.TORIHIKISAKI_CreateDate.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先基本情報の初期化
        /// </summary>
        internal void TorihikisakiKihonCrear()
        {
            LogUtility.DebugMethodStart();

            //郵便番号
            this.form.TORIHIKISAKI_POST.Text = string.Empty;
            //都道府県
            this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text = string.Empty;
            this.form.TORIHIKISAKI_TODOUFUKEN_NAME.Text = string.Empty;
            //住所
            this.form.TORIHIKISAKI_ADDRESS1_KIHON.Text = string.Empty;
            this.form.TORIHIKISAKI_ADDRESS2_KIHON.Text = string.Empty;
            //部署
            this.form.BUSHO.Text = string.Empty;
            //担当者
            this.form.TANTOUSHA.Text = string.Empty;
            //集計項目
            this.form.SHUUKEI_ITEM_CD.Text = string.Empty;
            this.form.SHUUKEI_KOUMOKU_NAME.Text = string.Empty;
            //業種
            this.form.GYOUSHU_CD.Text = string.Empty;
            this.form.GYOUSHU_NAME.Text = string.Empty;
            //代表者を印字
            this.form.DAIHYOU_PRINT_KBN.Text = string.Empty;
            //備考
            this.form.BIKOU1.Text = string.Empty;
            this.form.BIKOU2.Text = string.Empty;
            this.form.BIKOU3.Text = string.Empty;
            this.form.BIKOU4.Text = string.Empty;
            //諸口
            this.form.SHOKUCHI_KBN.Checked = false;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先請求情報1の初期化
        /// </summary>
        internal void TorihikisakiSeikyuuCrear()
        {
            LogUtility.DebugMethodStart();

            //取引区分
            this.form.TORIHIKI_KBN.Text = string.Empty;
            //締日
            this.form.SHIMEBI1.Text = string.Empty;
            this.form.SHIMEBI2.Text = string.Empty;
            this.form.SHIMEBI3.Text = string.Empty;
            //必着日
            this.form.HICCHAKUBI.Text = string.Empty;
            //160026 S
            this.form.KAISHUU_BETSU_KBN.Text = string.Empty;
            this.form.KAISHUU_BETSU_NICHIGO.Text = string.Empty;
            //160026 E

            //回収月
            this.form.KAISHUU_MONTH.Text = string.Empty;
            //回収日
            this.form.KAISHUU_DAY.Text = string.Empty;
            //回収方法
            this.form.KAISHUU_HOUHOU.Text = string.Empty;
            this.form.KAISHUU_HOUHOU_NAME.Text = string.Empty;
            //開始売掛残高
            this.form.KAISHI_URIKAKE_ZANDAKA.Text = string.Empty;
            //書式1
            this.form.SHOSHIKI_KBN.Text = string.Empty;
            //書式2
            this.form.SHOSHIKI_MEISAI_KBN.Text = string.Empty;
            //書式3
            this.form.SHOSHIKI_GENBA_KBN.Text = string.Empty;
            //消費税端数
            this.form.TAX_HASUU_CD.Text = string.Empty;
            //金額端数
            this.form.KINGAKU_HASUU_CD.Text = string.Empty;
            //請求形態
            this.form.SEIKYUU_KEITAI_KBN.Text = string.Empty;
            //入金明細
            this.form.NYUUKIN_MEISAI_KBN.Text = string.Empty;
            //用紙区分
            this.form.YOUSHI_KBN.Text = string.Empty;
            //税区分1
            this.form.ZEI_KEISAN_KBN_CD.Text = string.Empty;
            //税区分2
            this.form.ZEI_KBN_CD.Text = string.Empty;

            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            //出力区分
            this.form.OUTPUT_KBN.Text = string.Empty;
            //発行先コード
            this.form.HAKKOUSAKI_CD.Text = string.Empty;
            // 20160429 koukoukon v2.1_電子請求書 #16612 end

            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
            this.form.INXS_SEIKYUU_KBN.Text = string.Empty;
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先請求情報2の初期化
        /// </summary>
        internal void TorihikisakiSeikyuuCrear2()
        {
            LogUtility.DebugMethodStart();

            //振込銀行
            this.form.FURIKOMI_BANK_CD.Text = string.Empty;
            this.form.FURIKOMI_BANK_NAME.Text = string.Empty;
            //支店
            this.form.FURIKOMI_BANK_SHITEN_CD.Text = string.Empty;
            this.form.FURIKOMI_BANK_SHITEN_NAME.Text = string.Empty;
            //口座種類
            this.form.KOUZA_SHURUI.Text = string.Empty;
            //口座番号
            this.form.KOUZA_NO.Text = string.Empty;
            //口座名
            this.form.KOUZA_NAME.Text = string.Empty;
            //最終取引日時
            this.form.LAST_TORIHIKI_DATE.Text = string.Empty;
            //請求情報
            this.form.SEIKYUU_JOUHOU1.Text = string.Empty;
            this.form.SEIKYUU_JOUHOU2.Text = string.Empty;
            //請求書送付先
            this.form.SEIKYUU_SOUFU_NAME1.Text = string.Empty;
            this.form.SEIKYUU_SOUFU_KEISHOU1.Text = string.Empty;
            this.form.SEIKYUU_SOUFU_NAME2.Text = string.Empty;
            this.form.SEIKYUU_SOUFU_KEISHOU2.Text = string.Empty;
            //送付先郵便番号
            this.form.SEIKYUU_SOUFU_POST.Text = string.Empty;
            //送付先住所
            this.form.SEIKYUU_SOUFU_ADDRESS1.Text = string.Empty;
            this.form.SEIKYUU_SOUFU_ADDRESS2.Text = string.Empty;
            //送付先部署
            this.form.SEIKYUU_SOUFU_BUSHO.Text = string.Empty;
            //送付先TEL
            this.form.SEIKYUU_SOUFU_TEL.Text = string.Empty;
            //送付先FAX
            this.form.SEIKYUU_SOUFU_FAX.Text = string.Empty;
            //入金先
            this.form.NYUUKINSAKI_CD.Text = string.Empty;
            this.form.NYUUKINSAKI_NAME1.Text = string.Empty;
            this.form.NYUUKINSAKI_NAME2.Text = string.Empty;
            //送付先担当者
            this.form.SEIKYUU_SOUFU_TANTOU.Text = string.Empty;
            //請求担当者
            this.form.SEIKYUU_TANTOU.Text = string.Empty;
            //代表印字
            this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;
            //拠点印字
            this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
            //請求書拠点
            this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
            this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
            //振込人名
            this.form.FURIKOMI_NAME1.Text = string.Empty;
            this.form.FURIKOMI_NAME2.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先支払情報1の初期化
        /// </summary>
        internal void TorihikisakiShiharaiCrear()
        {
            LogUtility.DebugMethodStart();

            //取引区分
            this.form.SHIHARAI_TORIHIKI_KBN_CD.Text = string.Empty;
            //締日
            this.form.SHIHARAI_SHIMEBI1.Text = string.Empty;
            this.form.SHIHARAI_SHIMEBI2.Text = string.Empty;
            this.form.SHIHARAI_SHIMEBI3.Text = string.Empty;
            //160026 S
            this.form.SHIHARAI_BETSU_KBN.Text = string.Empty;
            this.form.SHIHARAI_BETSU_NICHIGO.Text = string.Empty;
            //160026 E

            //支払月
            this.form.SHIHARAI_MONTH.Text = string.Empty;
            //支払日
            this.form.SHIHARAI_DAY.Text = string.Empty;
            //支払方法
            this.form.SHIHARAI_HOUHOU.Text = string.Empty;
            this.form.SHIHARAI_HOUHOU_NAME.Text = string.Empty;
            //開始買掛残高
            this.form.KAISHI_KAIKAKE_ZANDAKA.Text = string.Empty;
            //書式1
            this.form.SHIHARAI_SHOSHIKI_KBN.Text = string.Empty;
            //書式2
            this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text = string.Empty;
            //書式3
            this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Text = string.Empty;
            //消費税端数
            this.form.SHIHARAI_TAX_HASUU_CD.Text = string.Empty;
            //金額端数
            this.form.SHIHARAI_KINGAKU_HASUU_CD.Text = string.Empty;
            //支払形態
            this.form.SHIHARAI_KEITAI_KBN.Text = string.Empty;
            //出金明細
            this.form.SHUKKIN_MEISAI_KBN.Text = string.Empty;
            //用紙区分
            this.form.SHIHARAI_YOUSHI_KBN.Text = string.Empty;
            //税区分1
            this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = string.Empty;
            //税区分2
            this.form.SHIHARAI_ZEI_KBN_CD.Text = string.Empty;
            //最終取引日時
            this.form.LAST_TORIHIKI_DATE_SHIHARAI.Text = string.Empty;

            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
            this.form.INXS_SHIHARAI_KBN.Text = string.Empty;
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先支払情報2の初期化
        /// </summary>
        internal void TorihikisakiShiharaiCrear2()
        {
            LogUtility.DebugMethodStart();

            //支払情報
            this.form.SHIHARAI_JOUHOU1.Text = string.Empty;
            this.form.SHIHARAI_JOUHOU2.Text = string.Empty;
            //支払明細書送付先
            this.form.SHIHARAI_SOUFU_NAME1.Text = string.Empty;
            this.form.SHIHARAI_SOUFU_KEISHOU1.Text = string.Empty;
            this.form.SHIHARAI_SOUFU_NAME2.Text = string.Empty;
            this.form.SHIHARAI_SOUFU_KEISHOU2.Text = string.Empty;
            //送付先郵便番号
            this.form.SHIHARAI_SOUFU_POST.Text = string.Empty;
            //送付先住所
            this.form.SHIHARAI_SOUFU_ADDRESS1.Text = string.Empty;
            this.form.SHIHARAI_SOUFU_ADDRESS2.Text = string.Empty;
            //送付先部署
            this.form.SHIHARAI_SOUFU_BUSHO.Text = string.Empty;
            //送付先担当者
            this.form.SHIHARAI_SOUFU_TANTOU.Text = string.Empty;
            //送付先電話番号
            this.form.SHIHARAI_SOUFU_TEL.Text = string.Empty;
            //送付先FAX番号
            this.form.SHIHARAI_SOUFU_FAX.Text = string.Empty;
            //拠点名を印字
            this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
            //支払書拠点
            this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
            this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
            //160026 S
            this.form.FURIKOMI_EXPORT_KBN.Text = string.Empty;
            this.form.FURIKOMI_SAKI_BANK_CD.Text = string.Empty;
            this.form.FURIKOMI_SAKI_BANK_NAME.Text = string.Empty;
            this.form.FURIKOMI_SAKI_BANK_SHITEN_CD.Text = string.Empty;
            this.form.FURIKOMI_SAKI_BANK_SHITEN_NAME.Text = string.Empty;
            this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Text = string.Empty;
            this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI.Text = string.Empty;
            this.form.FURIKOMI_SAKI_BANK_KOUZA_NO.Text = string.Empty;
            this.form.FURIKOMI_SAKI_BANK_KOUZA_NAME.Text = string.Empty;
            this.form.TEI_SUU_RYOU_KBN.Text = string.Empty;
            this.form.FURI_KOMI_MOTO_BANK_CD.Text = string.Empty;
            this.form.FURI_KOMI_MOTO_BANK_NAME.Text = string.Empty;
            this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text = string.Empty;
            this.form.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = string.Empty;
            this.form.FURI_KOMI_MOTO_SHURUI.Text = string.Empty;
            this.form.FURI_KOMI_MOTO_NO.Text = string.Empty;
            this.form.FURI_KOMI_MOTO_NAME.Text = string.Empty;
            //160026 E
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先分類の初期化
        /// </summary>
        internal void TorihikisakiBunruiCrear()
        {
            LogUtility.DebugMethodStart();

            //マニフェスト返送先区分
            this.form.MANI_HENSOUSAKI_KBN.Checked = false;
            this.form.TORIHIKISAKI_MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "1";
            this.form.TORIHIKISAKI_MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = true;
            this.form.TORIHIKISAKI_MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = false;
            //マニフェスト返送先
            this.form.MANI_HENSOUSAKI_NAME1.Text = string.Empty;
            this.form.MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
            this.form.MANI_HENSOUSAKI_NAME2.Text = string.Empty;
            this.form.MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
            //マニフェスト返送先郵便番号
            this.form.MANI_HENSOUSAKI_POST.Text = string.Empty;
            //マニフェスト返送先住所
            this.form.MANI_HENSOUSAKI_ADDRESS1.Text = string.Empty;
            this.form.MANI_HENSOUSAKI_ADDRESS2.Text = string.Empty;
            //マニフェスト返送先部署
            this.form.MANI_HENSOUSAKI_BUSHO.Text = string.Empty;
            //マニフェスト返送先担当者
            this.form.MANI_HENSOUSAKI_TANTOU.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先業者一覧の初期化
        /// </summary>
        internal void TorihikisakiGyoushaIchiranCrear()
        {
            LogUtility.DebugMethodStart();

            if (this.sysInfoEntity.GYOUSHA_TORIHIKI_CHUUSHI.IsNull)
            {
                this.form.TORIHIKI_STOP.Text = string.Empty;
            }
            else
            {
                this.form.TORIHIKI_STOP.Text = this.sysInfoEntity.GYOUSHA_TORIHIKI_CHUUSHI.ToString();
            }
            this.form.GYOUSHA_ICHIRAN.DataSource = null;
            this.form.GYOUSHA_ICHIRAN.Rows.Clear();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 初期化：業者タブ

        /// <summary>
        /// 業者ヘッダ情報の初期化
        /// </summary>
        internal void GyoushaHeaderCrear()
        {
            LogUtility.DebugMethodStart();

            //取引先有無区分
            this.form.TORIHIKISAKI_UMU_KBN.Text = string.Empty;
            //業者区分
            this.form.Gyousha_KBN_1.Checked = false;
            this.form.Gyousha_KBN_2.Checked = false;
            this.form.Gyousha_KBN_3.Checked = false;
            //取引先CD
            this.form.GYOUSHA_TORIHIKISAKI_CD.Text = string.Empty;
            //取引先名
            this.form.GYOUSHA_TORIHIKISAKI_NAME1.Text = string.Empty;
            this.form.GYOUSHA_TORIHIKISAKI_NAME2.Text = string.Empty;
            //拠点CD
            this.form.KYOTEN_CD.Text = string.Empty;
            //拠点名
            this.form.KYOTEN_NAME.Text = string.Empty;
            //業者CD
            this.form.GYOUSHA_GYOUSHA_CD.Text = string.Empty;
            //フリガナ
            this.form.GYOUSHA_FURIGANA.Text = string.Empty;
            //業者名1
            this.form.GYOUSHA_NAME1.Text = string.Empty;
            //業者敬称1
            this.form.GYOUSHA_KEISHOU1.Text = string.Empty;
            //業者名2
            this.form.GYOUSHA_NAME2.Text = string.Empty;
            //業者敬称2
            this.form.GYOUSHA_KEISHOU2.Text = string.Empty;
            //業者略称
            this.form.GYOUSHA_GYOUSHA_NAME_RYAKU.Text = string.Empty;
            //業者電話番号
            this.form.GYOUSHA_GYOUSHA_TEL.Text = string.Empty;
            //業者携帯番号
            this.form.GYOUSHA_KEITAI_TEL.Text = string.Empty;
            //業者FAX番号
            this.form.GYOUSHA_FAX.Text = string.Empty;
            //業者営業担当部署CD
            this.form.GYOUSHA_EIGYOU_TANTOU_BUSHO_CD.Text = string.Empty;
            //業者営業担当部署名
            this.form.BUSHO_NAME.Text = string.Empty;
            //業者営業担当者CD
            this.form.GYOUSHA_EIGYOU_TANTOU_CD.Text = string.Empty;
            //業者営業担当者名
            this.form.SHAIN_NAME.Text = string.Empty;
            //適用開始日
            this.form.GYOUSHA_TEKIYOU_BEGIN.Text = string.Empty;
            //適用終了日
            this.form.GYOUSHA_TEKIYOU_END.Text = string.Empty;
            //中止理由
            this.form.GYOUSHA_CHUUSHI_RIYUU1.Text = string.Empty;
            this.form.GYOUSHA_CHUUSHI_RIYUU2.Text = string.Empty;
            //諸口区分
            this.form.GYOUSHA_SHOKUCHI_KBN.Checked = false;
            //自社区分
            this.form.JISHA_KBN.Checked = false;
            // 最終更新
            this.form.GYOUSHA_LastUpdateUser.Text = string.Empty;
            this.form.GYOUSHA_LastUpdateDate.Text = string.Empty;
            // 初回登録
            this.form.GYOUSHA_CreateUser.Text = string.Empty;
            this.form.GYOUSHA_CreateDate.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者基本情報の初期化
        /// </summary>
        internal void GyoushaKihonCrear()
        {
            LogUtility.DebugMethodStart();

            //郵便番号
            this.form.GYOUSHA_KIHON_POST_NO.Text = string.Empty;
            //都道府県
            this.form.GYOUSHA_KIHON_TODOUFUKEN_CD.Text = string.Empty;
            this.form.GYOUSHA_KIHON_TODOUFUKEN_NAME.Text = string.Empty;
            //住所
            this.form.GYOUSHA_KIHON_ADDRESS1.Text = string.Empty;
            this.form.GYOUSHA_KIHON_ADDRESS2.Text = string.Empty;
            //地域
            this.form.GYOUSHA_KIHON_CHIIKI_CD.Text = string.Empty;
            this.form.GYOUSHA_KIHON_CHIIKI_NAME.Text = string.Empty;
            //部署
            this.form.GYOUSHA_KIHON_BUSHO_NAME.Text = string.Empty;
            //担当者
            this.form.GYOUSHA_KIHON_TANTOUSHA_NAME.Text = string.Empty;
            //代表者
            this.form.GYOUSHA_DAIHYOU.Text = string.Empty;
            //集計項目
            this.form.GYOUSHA_KIHON_SHUKEIITEM_CD.Text = string.Empty;
            this.form.GYOUSHA_KIHON_SHUKEIITEM_NAME.Text = string.Empty;
            //業種
            this.form.GYOUSHA_KIHON_GYOUSHU_CD.Text = string.Empty;
            this.form.GYOUSHA_KIHON_GYOUSHU_NAME.Text = string.Empty;
            //備考
            this.form.GYOUSHA_KIHON_BIKOU1.Text = string.Empty;
            this.form.GYOUSHA_KIHON_BIKOU2.Text = string.Empty;
            this.form.GYOUSHA_KIHON_BIKOU3.Text = string.Empty;
            this.form.GYOUSHA_KIHON_BIKOU4.Text = string.Empty;

            //20250321
            this.form.URIAGE_GURUPU_CD.Text = string.Empty;
            this.form.URIAGE_GURUPU_NAME.Text = string.Empty;
            this.form.SHIHARAI_GURUPU_CD.Text = string.Empty;
            this.form.SHIHARAI_GURUPU_NAME.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者請求情報の初期化
        /// </summary>
        internal void GyoushaSeikyuuCrear()
        {
            LogUtility.DebugMethodStart();

            //請求書送付先
            this.form.GYOUSHA_SEIKYU_SOUFUSAKI1.Text = string.Empty;
            this.form.GYOUSHA_SEIKYU_SOUFUSAKI2.Text = string.Empty;
            this.form.GYOUSHA_SEIKYUU_SOUFU_KEISHOU1.Text = string.Empty;
            this.form.GYOUSHA_SEIKYUU_SOUFU_KEISHOU2.Text = string.Empty;
            //請求送付先郵便番号
            this.form.GYOUSHA_SEIKYU_SOUFUSAKI_POST_NO.Text = string.Empty;
            //請求送付先住所
            this.form.GYOUSHA_SEIKYU_SOUFUSAKI_ADDRS1.Text = string.Empty;
            this.form.GYOUSHA_SEIKYU_SOUFUSAKI_ADDRS2.Text = string.Empty;
            //請求送付先部署
            this.form.GYOUSHA_SEIKYU_SOUFUSAKI_BUSHO_NAME.Text = string.Empty;
            //請求送付先担当者
            this.form.GYOUSHA_SEIKYU_SOUFUSAKI_TANTOUSHA_NAME.Text = string.Empty;
            //請求送付先TEL
            this.form.GYOUSHA_SEIKYU_SOUFUSAKI_TEL.Text = string.Empty;
            //請求送付先FAX
            this.form.GYOUSHA_SEIKYU_SOUFUSAKI_FAX.Text = string.Empty;
            //請求担当者
            this.form.GYOUSHA_SEIKYU_SEIKYU_TANTOUSHA_NAME.Text = string.Empty;
            //代表印字
            this.form.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;
            //拠点印字
            this.form.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
            //請求書拠点
            this.form.GYOUSHA_SEIKYUU_KYOTEN_CD.Text = string.Empty;
            this.form.GYOUSHA_SEIKYUU_KYOTEN_NAME.Text = string.Empty;
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            //発行先コード
            this.form.GYOUSHA_HAKKOUSAKI_CD.Text = string.Empty;
            // 20160429 koukoukon v2.1_電子請求書 #16612 end

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者支払情報の初期化
        /// </summary>
        internal void GyoushaShiharaiCrear()
        {
            LogUtility.DebugMethodStart();

            //支払書送付先
            this.form.GYOUSHA_SHIHARAI_SOUFUSAKI1.Text = string.Empty;
            this.form.GYOUSHA_SHIHARAI_SOUFUSAKI2.Text = string.Empty;
            //支払送付先郵便番号
            this.form.GYOUSHA_SHIHARAI_SOUFUSAKI_POST_NO.Text = string.Empty;
            //支払送付先住所
            this.form.GYOUSHA_SHIHARAI_SOUFUSAKI_ADDRS1.Text = string.Empty;
            this.form.GYOUSHA_SHIHARAI_SOUFUSAKI_ADDRS2.Text = string.Empty;
            //支払送付先部署
            this.form.GYOUSHA_SHIHARAI_SOUFUSAKI_BUSHO_NAME.Text = string.Empty;
            //支払送付先担当者
            this.form.GYOUSHA_SHIHARAI_SOUFUSAKI_TANTOUSHA_NAME.Text = string.Empty;
            //支払送付先TEL
            this.form.GYOUSHA_SHIHARAI_SOUFUSAKI_TEL.Text = string.Empty;
            //支払送付先FAX
            this.form.GYOUSHA_SHIHARAI_SOUFUSAKI_FAX.Text = string.Empty;
            //拠点印字
            this.form.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
            //支払書拠点
            this.form.GYOUSHA_SHIHARAI_KYOTEN_CD.Text = string.Empty;
            this.form.GYOUSHA_SHIHARAI_KYOTEN_NAME.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者現場一覧の初期化
        /// </summary>
        internal void GyoushaGenbaIchiranCrear()
        {
            LogUtility.DebugMethodStart();

            if (this.sysInfoEntity.GYOUSHA_TORIHIKI_CHUUSHI.IsNull)
            {
                this.form.GYOUSHA_TORIHIKI_STOP.Text = string.Empty;
            }
            else
            {
                this.form.GYOUSHA_TORIHIKI_STOP.Text = this.sysInfoEntity.GYOUSHA_TORIHIKI_CHUUSHI.ToString();
            }
            this.form.GENBA_ICHIRAN.DataSource = null;
            this.form.GENBA_ICHIRAN.Rows.Clear();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者分類情報の初期化
        /// </summary>
        internal void GyoushaBunruiCrear()
        {
            LogUtility.DebugMethodStart();

            //排出事業者
            this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = false;
            //運搬受託者
            this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = false;
            //処分受託者
            this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = false;
            //マニフェスト返送先
            this.form.GYOUSHA_MANI_HENSOUSAKI_KBN.Checked = false;
            //返送先
            this.form.GYOUSHA_MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "1";
            this.form.GYOUSHA_MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = true;
            this.form.GYOUSHA_MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = false;
            this.form.GYOUSHA_MANI_HENSOUSAKI_NAME1.Text = string.Empty;
            this.form.GYOUSHA_MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
            this.form.GYOUSHA_MANI_HENSOUSAKI_NAME2.Text = string.Empty;
            this.form.GYOUSHA_MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
            //郵便番号
            this.form.GYOUSHA_MANI_HENSOUSAKI_POST.Text = string.Empty;
            //住所
            this.form.GYOUSHA_MANI_HENSOUSAKI_ADDRESS1.Text = string.Empty;
            this.form.GYOUSHA_MANI_HENSOUSAKI_ADDRESS2.Text = string.Empty;
            //部署
            this.form.GYOUSHA_MANI_HENSOUSAKI_BUSHO.Text = string.Empty;
            //担当者
            this.form.GYOUSHA_MANI_HENSOUSAKI_TANTOU.Text = string.Empty;
            // 運搬報告書提出先
            this.form.GYOUSHA_UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = string.Empty;
            this.form.GYOUSHA_UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 初期化：現場タブ

        /// <summary>
        /// 現場ヘッダ情報の初期化
        /// </summary>
        internal void GenbaHeaderCrear()
        {
            LogUtility.DebugMethodStart();

            this.form.TORIHIKISAKI_MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "1";
            this.form.TORIHIKISAKI_MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = true;
            this.form.TORIHIKISAKI_MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = false;
            //業者区分
            this.form.GyoushaKbnUkeire.Checked = false;
            this.form.GyoushaKbnShukka.Checked = false;
            this.form.GyoushaKbnMani.Checked = false;
            //業者コード
            this.form.GyoushaCode.Text = string.Empty;
            //業者名
            this.form.GyoushaName1.Text = string.Empty;
            this.form.GyoushaName2.Text = string.Empty;
            //取引先コード
            this.form.TorihikisakiCode.Text = string.Empty;
            //取引先名
            this.form.TorihikisakiName1.Text = string.Empty;
            this.form.TorihikisakiName2.Text = string.Empty;
            //拠点コード
            this.form.KyotenCode.Text = string.Empty;
            //拠点名
            this.form.KyotenName.Text = string.Empty;
            //現場コード
            this.form.GenbaCode.Text = string.Empty;
            //フリガナ
            this.form.GenbaFurigana.Text = string.Empty;
            //現場名
            this.form.GenbaName1.Text = string.Empty;
            this.form.GenbaKeishou1.Text = string.Empty;
            this.form.GenbaName2.Text = string.Empty;
            this.form.GenbaKeishou2.Text = string.Empty;
            //現場名略
            this.form.GenbaNameRyaku.Text = string.Empty;
            //現場電話番号
            this.form.GenbaTel.Text = string.Empty;
            //現場携帯番号
            this.form.GenbaKeitaiTel.Text = string.Empty;
            //現場FAX番号
            this.form.GenbaFax.Text = string.Empty;
            //営業担当部署コード
            this.form.EigyouTantouBushoCode.Text = string.Empty;
            //営業担当部署名
            this.form.EigyouTantouBushoName.Text = string.Empty;
            //営業担当者コード
            this.form.EigyouCode.Text = string.Empty;
            //営業担当者名
            this.form.EigyouName.Text = string.Empty;
            //適用開始
            this.form.TekiyouBegin.Text = string.Empty;
            //適用終了
            this.form.TekiyouEnd.Text = string.Empty;
            //中止理由
            this.form.ChuusiRiyuu1.Text = string.Empty;
            this.form.ChuusiRiyuu2.Text = string.Empty;
            //諸口区分
            this.form.ShokuchiKbn.Checked = false;
            //電マニ照会区分
            //this.form.DenManiShoukaiKbn.Checked = false;
            //検収有
            this.form.KENSHU_YOUHI.Checked = false;
            //自社区分
            this.form.JishaKbn.Checked = false;
            // 最終更新
            this.form.GENBA_LastUpdateUser.Text = string.Empty;
            this.form.GENBA_LastUpdateDate.Text = string.Empty;
            // 初回登録
            this.form.GENBA_CreateUser.Text = string.Empty;
            this.form.GENBA_CreateDate.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場基本情報の初期化
        /// </summary>
        internal void GenbaKihonCrear()
        {
            LogUtility.DebugMethodStart();

            //郵便番号
            this.form.GENBA_KIHON_POST_NO.Text = string.Empty;
            //都道府県
            this.form.GENBA_KIHON_TODOUFUKEN_CD.Text = string.Empty;
            this.form.GENBA_KIHON_TODOUFUKEN_NAME.Text = string.Empty;
            //住所
            this.form.GENBA_KIHON_ADDRS1.Text = string.Empty;
            this.form.GENBA_KIHON_ADDRS2.Text = string.Empty;
            //地域
            this.form.GENBA_KIHON_CHIIKI_CD.Text = string.Empty;
            this.form.GENBA_KIHON_CHIIKI_NAME.Text = string.Empty;
            //部署
            this.form.GENBA_KIHON_BUSHO_NAME.Text = string.Empty;
            //担当者
            this.form.GENBA_KIHON_TANTOUSHA_NAME.Text = string.Empty;
            //交付担当者
            this.form.GENBA_KIHON_KOUFU_TANTOUSHA_NAME.Text = string.Empty;
            //集計項目
            this.form.GENBA_KIHON_SHUKEIITEM_CD.Text = string.Empty;
            this.form.GENBA_KIHON_SHUKEIITEM_NAME.Text = string.Empty;
            //業種
            this.form.GENBA_KIHON_GYOUSHU_CD.Text = string.Empty;
            this.form.GENBA_KIHON_GYOUSHU_NAME.Text = string.Empty;
            //備考
            this.form.GENBA_KIHON_BIKOU1.Text = string.Empty;
            this.form.GENBA_KIHON_BIKOU2.Text = string.Empty;
            this.form.GENBA_KIHON_BIKOU3.Text = string.Empty;
            this.form.GENBA_KIHON_BIKOU4.Text = string.Empty;
            //運転者指示事項
            this.form.GENBA_UNTENSHA_SHIJI_JIKOU1.Text = string.Empty;
            this.form.GENBA_UNTENSHA_SHIJI_JIKOU2.Text = string.Empty;
            this.form.GENBA_UNTENSHA_SHIJI_JIKOU3.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場請求情報の初期化
        /// </summary>
        internal void GenbaSeikyuuCrear()
        {
            LogUtility.DebugMethodStart();

            //請求書送付先
            this.form.GENBA_SEIKYU_SOUFUSAKI1.Text = string.Empty;
            this.form.GENBA_SEIKYU_SOUFUSAKI2.Text = string.Empty;
            this.form.GENBA_SEIKYUU_SOUFU_KEISHOU1.Text = string.Empty;
            this.form.GENBA_SEIKYUU_SOUFU_KEISHOU2.Text = string.Empty;
            //請求送付先郵便番号
            this.form.GENBA_SEIKYU_SOUFUSAKI_POST_NO.Text = string.Empty;
            //請求送付先住所
            this.form.GENBA_SEIKYU_SOUFUSAKI_ADDRS1.Text = string.Empty;
            this.form.GENBA_SEIKYU_SOUFUSAKI_ADDRS2.Text = string.Empty;
            //請求送付先部署
            this.form.GENBA_SEIKYU_SOUFUSAKI_BUSHO_NAME.Text = string.Empty;
            //請求送付先担当者
            this.form.GENBA_SEIKYU_SOUFUSAKI_TANTOUSHA_NAME.Text = string.Empty;
            //請求送付先TEL
            this.form.GENBA_SEIKYU_SOUFUSAKI_TEL.Text = string.Empty;
            //請求送付先FAX
            this.form.GENBA_SEIKYU_SOUFUSAKI_FAX.Text = string.Empty;
            //請求担当者
            this.form.GENBA_SEIKYU_TANTOUSHA_NAME.Text = string.Empty;
            //代表印字
            this.form.SeikyuuDaihyouPrintKbn.Text = string.Empty;
            //拠点印字
            this.form.SeikyuuKyotenPrintKbn.Text = string.Empty;
            //拠点名
            this.form.SeikyuuKyotenCode.Text = string.Empty;
            this.form.SeikyuuKyotenName.Text = string.Empty;
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            //発行先コード
            this.form.GENBA_HAKKOUSAKI_CD.Text = string.Empty;
            // 20160429 koukoukon v2.1_電子請求書 #16612 end

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場支払情報の初期化
        /// </summary>
        internal void GenbaShiharaiCrear()
        {
            LogUtility.DebugMethodStart();

            //支払書送付先
            this.form.GENBA_SHIHARAI_SOUFUSAKI1.Text = string.Empty;
            this.form.GENBA_SHIHARAI_SOUFUSAKI2.Text = string.Empty;
            //支払送付先郵便番号
            this.form.GENBA_SHIHARAI_SOUFUSAKI_POST_NO.Text = string.Empty;
            //支払送付先住所
            this.form.GENBA_SHIHARAI_SOUFUSAKI_ADDRS1.Text = string.Empty;
            this.form.GENBA_SHIHARAI_SOUFUSAKI_ADDRS2.Text = string.Empty;
            //支払送付先部署
            this.form.GENBA_SHIHARAI_SOUFUSAKI_BUSHO_NAME.Text = string.Empty;
            //支払送付先担当者
            this.form.GENBA_SHIHARAI_SOUFUSAKI_TANTOUSHA_NAME.Text = string.Empty;
            //支払送付先TEL
            this.form.GENBA_SHIHARAI_SOUFUSAKI_TEL.Text = string.Empty;
            //支払送付先FAX
            this.form.GENBA_SHIHARAI_SOUFUSAKI_FAX.Text = string.Empty;
            //拠点印字
            this.form.ShiharaiKyotenPrintKbn.Text = string.Empty;
            //拠点名
            this.form.ShiharaiKyotenCode.Text = string.Empty;
            this.form.ShiharaiKyotenName.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場分類情報の初期化
        /// </summary>
        internal void GenbaBunruiCrear()
        {
            LogUtility.DebugMethodStart();

            //排出事業場
            this.form.HaishutsuKbn.Checked = false;
            //積み替え保管
            this.form.TsumikaeHokanKbn.Checked = false;
            //処分事業場
            this.form.ShobunJigyoujouKbn.Checked = false;
            //最終処分場
            this.form.SaishuuShobunjouKbn.Checked = false;
            //マニ返送先
            this.form.ManiHensousakiKbn.Checked = false;
            //マニ種類
            this.form.ManifestShuruiCode.Text = string.Empty;
            this.form.ManifestShuruiName.Text = string.Empty;
            //マニ手配
            this.form.ManifestTehaiCode.Text = string.Empty;
            this.form.ManifestTehaiName.Text = string.Empty;
            //処分先
            this.form.ShobunsakiCode.Text = string.Empty;
            //返送先
            this.form.GENBA_MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = string.Empty;
            this.form.GENBA_MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = false;
            this.form.GENBA_MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = false;
            //返送先名
            this.form.ManiHensousakiName1.Text = string.Empty;
            this.form.ManiHensousakiKeishou1.Text = string.Empty;
            this.form.ManiHensousakiName2.Text = string.Empty;
            this.form.ManiHensousakiKeishou2.Text = string.Empty;
            //返送先郵便番号
            this.form.ManiHensousakiPost.Text = string.Empty;
            //返送先住所
            this.form.ManiHensousakiAddress1.Text = string.Empty;
            this.form.ManiHensousakiAddress2.Text = string.Empty;
            //返送先部署
            this.form.ManiHensousakiBusho.Text = string.Empty;
            //返送先担当
            this.form.ManiHensousakiTantou.Text = string.Empty;
            // 運搬報告書提出先
            this.form.GENBA_UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = string.Empty;
            this.form.GENBA_UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場A票返送先～E票返送先情報の初期化
        /// </summary>
        internal void GenbaManiHensousakiCrear()
        {
            LogUtility.DebugMethodStart();
            #region A票
            if (this._tabPageManager.IsVisible(8))
            {
                this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = string.Empty;
                this.form.HensousakiKbn_AHyo.Text = string.Empty;
                this.form.HensousakiKbn1_AHyo.Checked = false;
                this.form.HensousakiKbn2_AHyo.Checked = false;
                this.form.HensousakiKbn3_AHyo.Checked = false;
                this.form.ManiHensousakiTorihikisakiCode_AHyo.Text = string.Empty;
                this.form.ManiHensousakiTorihikisakiName_AHyo.Text = string.Empty;
                this.form.ManiHensousakiGenbaCode_AHyo.Text = string.Empty;
                this.form.ManiHensousakiGyoushaName_AHyo.Text = string.Empty;
                this.form.ManiHensousakiGyoushaCode_AHyo.Text = string.Empty;
                this.form.ManiHensousakiGenbaName_AHyo.Text = string.Empty;
                this.form.MANIFEST_USE_AHyo.Text = string.Empty;

            }
            #endregion

            #region B1票
            if (this._tabPageManager.IsVisible(9))
            {
                this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = string.Empty;
                this.form.HensousakiKbn_B1Hyo.Text = string.Empty;
                this.form.HensousakiKbn1_B1Hyo.Checked = false;
                this.form.HensousakiKbn2_B1Hyo.Checked = false;
                this.form.HensousakiKbn3_B1Hyo.Checked = false;
                this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Text = string.Empty;
                this.form.ManiHensousakiTorihikisakiName_B1Hyo.Text = string.Empty;
                this.form.ManiHensousakiGenbaCode_B1Hyo.Text = string.Empty;
                this.form.ManiHensousakiGyoushaName_B1Hyo.Text = string.Empty;
                this.form.ManiHensousakiGyoushaCode_B1Hyo.Text = string.Empty;
                this.form.ManiHensousakiGenbaName_B1Hyo.Text = string.Empty;
                this.form.MANIFEST_USE_B1Hyo.Text = string.Empty;
            }
            #endregion

            #region B2票
            if (this._tabPageManager.IsVisible(10))
            {
                this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = string.Empty;
                this.form.HensousakiKbn_B2Hyo.Text = string.Empty;
                this.form.HensousakiKbn1_B2Hyo.Checked = false;
                this.form.HensousakiKbn2_B2Hyo.Checked = false;
                this.form.HensousakiKbn3_B2Hyo.Checked = false;
                this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Text = string.Empty;
                this.form.ManiHensousakiTorihikisakiName_B2Hyo.Text = string.Empty;
                this.form.ManiHensousakiGenbaCode_B2Hyo.Text = string.Empty;
                this.form.ManiHensousakiGyoushaName_B2Hyo.Text = string.Empty;
                this.form.ManiHensousakiGyoushaCode_B2Hyo.Text = string.Empty;
                this.form.ManiHensousakiGenbaName_B2Hyo.Text = string.Empty;
                this.form.MANIFEST_USE_B2Hyo.Text = string.Empty;
            }
            #endregion

            #region B4票
            if (this._tabPageManager.IsVisible(11))
            {
                this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = string.Empty;
                this.form.HensousakiKbn_B4Hyo.Text = string.Empty;
                this.form.HensousakiKbn1_B4Hyo.Checked = false;
                this.form.HensousakiKbn2_B4Hyo.Checked = false;
                this.form.HensousakiKbn3_B4Hyo.Checked = false;
                this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Text = string.Empty;
                this.form.ManiHensousakiTorihikisakiName_B4Hyo.Text = string.Empty;
                this.form.ManiHensousakiGenbaCode_B4Hyo.Text = string.Empty;
                this.form.ManiHensousakiGyoushaName_B4Hyo.Text = string.Empty;
                this.form.ManiHensousakiGyoushaCode_B4Hyo.Text = string.Empty;
                this.form.ManiHensousakiGenbaName_B4Hyo.Text = string.Empty;
                this.form.MANIFEST_USE_B4Hyo.Text = string.Empty;
            }
            #endregion

            #region B6票
            if (this._tabPageManager.IsVisible(12))
            {
                this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = string.Empty;
                this.form.HensousakiKbn_B6Hyo.Text = string.Empty;
                this.form.HensousakiKbn1_B6Hyo.Checked = false;
                this.form.HensousakiKbn2_B6Hyo.Checked = false;
                this.form.HensousakiKbn3_B6Hyo.Checked = false;
                this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Text = string.Empty;
                this.form.ManiHensousakiTorihikisakiName_B6Hyo.Text = string.Empty;
                this.form.ManiHensousakiGenbaCode_B6Hyo.Text = string.Empty;
                this.form.ManiHensousakiGyoushaName_B6Hyo.Text = string.Empty;
                this.form.ManiHensousakiGyoushaCode_B6Hyo.Text = string.Empty;
                this.form.ManiHensousakiGenbaName_B6Hyo.Text = string.Empty;
                this.form.MANIFEST_USE_B6Hyo.Text = string.Empty;
            }
            #endregion

            #region C1票
            if (this._tabPageManager.IsVisible(13))
            {
                this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = string.Empty;
                this.form.HensousakiKbn_C1Hyo.Text = string.Empty;
                this.form.HensousakiKbn1_C1Hyo.Checked = false;
                this.form.HensousakiKbn2_C1Hyo.Checked = false;
                this.form.HensousakiKbn3_C1Hyo.Checked = false;
                this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Text = string.Empty;
                this.form.ManiHensousakiTorihikisakiName_C1Hyo.Text = string.Empty;
                this.form.ManiHensousakiGenbaCode_C1Hyo.Text = string.Empty;
                this.form.ManiHensousakiGyoushaName_C1Hyo.Text = string.Empty;
                this.form.ManiHensousakiGyoushaCode_C1Hyo.Text = string.Empty;
                this.form.ManiHensousakiGenbaName_C1Hyo.Text = string.Empty;
                this.form.MANIFEST_USE_C1Hyo.Text = string.Empty;
            }
            #endregion

            #region C2票
            if (this._tabPageManager.IsVisible(14))
            {
                this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = string.Empty;
                this.form.HensousakiKbn_C2Hyo.Text = string.Empty;
                this.form.HensousakiKbn1_C2Hyo.Checked = false;
                this.form.HensousakiKbn2_C2Hyo.Checked = false;
                this.form.HensousakiKbn3_C2Hyo.Checked = false;
                this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Text = string.Empty;
                this.form.ManiHensousakiTorihikisakiName_C2Hyo.Text = string.Empty;
                this.form.ManiHensousakiGenbaCode_C2Hyo.Text = string.Empty;
                this.form.ManiHensousakiGyoushaName_C2Hyo.Text = string.Empty;
                this.form.ManiHensousakiGyoushaCode_C2Hyo.Text = string.Empty;
                this.form.ManiHensousakiGenbaName_C2Hyo.Text = string.Empty;
                this.form.MANIFEST_USE_C2Hyo.Text = string.Empty;
            }
            #endregion

            #region D票
            if (this._tabPageManager.IsVisible(15))
            {
                this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = string.Empty;
                this.form.HensousakiKbn_DHyo.Text = string.Empty;
                this.form.HensousakiKbn1_DHyo.Checked = false;
                this.form.HensousakiKbn2_DHyo.Checked = false;
                this.form.HensousakiKbn3_DHyo.Checked = false;
                this.form.ManiHensousakiTorihikisakiCode_DHyo.Text = string.Empty;
                this.form.ManiHensousakiTorihikisakiName_DHyo.Text = string.Empty;
                this.form.ManiHensousakiGenbaCode_DHyo.Text = string.Empty;
                this.form.ManiHensousakiGyoushaName_DHyo.Text = string.Empty;
                this.form.ManiHensousakiGyoushaCode_DHyo.Text = string.Empty;
                this.form.ManiHensousakiGenbaName_DHyo.Text = string.Empty;
                this.form.MANIFEST_USE_DHyo.Text = string.Empty;
            }
            #endregion

            #region E票
            if (this._tabPageManager.IsVisible(16))
            {
                this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = string.Empty;
                this.form.HensousakiKbn_EHyo.Text = string.Empty;
                this.form.HensousakiKbn1_EHyo.Checked = false;
                this.form.HensousakiKbn2_EHyo.Checked = false;
                this.form.HensousakiKbn3_EHyo.Checked = false;
                this.form.ManiHensousakiTorihikisakiCode_EHyo.Text = string.Empty;
                this.form.ManiHensousakiTorihikisakiName_EHyo.Text = string.Empty;
                this.form.ManiHensousakiGenbaCode_EHyo.Text = string.Empty;
                this.form.ManiHensousakiGyoushaName_EHyo.Text = string.Empty;
                this.form.ManiHensousakiGyoushaCode_EHyo.Text = string.Empty;
                this.form.ManiHensousakiGenbaName_EHyo.Text = string.Empty;
                this.form.MANIFEST_USE_EHyo.Text = string.Empty;
            }
            #endregion
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #endregion

        #region 表示：取引先タブ

        /// <summary>
        /// 取引先のヘッダ情報の表示
        /// </summary>
        internal void TorihikisakiHeaderHyouji()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (null != this.TorihikisakiHeaderSearchResult && this.TorihikisakiHeaderSearchResult.Rows.Count > 0)
                {
                    // 入金先
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("TORIHIKISAKI_NYUUKINSAKI_KBN"))
                    {
                        this.form.TORIHIKISAKI_NYUUKINSAKI_KBN.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<Int16>("TORIHIKISAKI_NYUUKINSAKI_KBN").ToString();
                    }
                    // 拠点
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("TORIHIKISAKI_KYOTEN_CD"))
                    {
                        this.form.TORIHIKISAKI_KYOTEN_CD.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<Int16>("TORIHIKISAKI_KYOTEN_CD").ToString();
                        this.form.KYOTEN_NAME_RYAKU.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<string>("KYOTEN_NAME_RYAKU");
                    }
                    // 取引先CD
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("TORIHIKISAKI_CD"))
                    {
                        this.form.TORIHIKISAKI_CD_HEADER.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<string>("TORIHIKISAKI_CD").ToString();
                    }
                    // フリガナ
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("TORIHIKISAKI_FURIGANA"))
                    {
                        this.form.TORIHIKISAKI_FURIGANA.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<string>("TORIHIKISAKI_FURIGANA").ToString();
                    }
                    // 取引先名1
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("TORIHIKISAKI_NAME1"))
                    {
                        this.form.TORIHIKISAKI_NAME1.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<string>("TORIHIKISAKI_NAME1").ToString();
                    }
                    // 取引先敬称1
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("TORIHIKISAKI_KEISHOU1"))
                    {
                        this.form.TORIHIKISAKI_KEISHOU1.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<string>("TORIHIKISAKI_KEISHOU1").ToString();
                    }
                    // 取引先名2
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("TORIHIKISAKI_NAME2"))
                    {
                        this.form.TORIHIKISAKI_NAME2.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<string>("TORIHIKISAKI_NAME2").ToString();
                    }
                    // 取引先敬称2
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("TORIHIKISAKI_KEISHOU2"))
                    {
                        this.form.TORIHIKISAKI_KEISHOU2.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<string>("TORIHIKISAKI_KEISHOU2").ToString();
                    }
                    // 取引先略称
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("TORIHIKISAKI_NAME_RYAKU"))
                    {
                        this.form.TORIHIKISAKI_NAME_RYAKU_HEADER.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<string>("TORIHIKISAKI_NAME_RYAKU").ToString();
                    }
                    // 電話番号
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("TORIHIKISAKI_TEL"))
                    {
                        this.form.TORIHIKISAKI_TEL_HEADER.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<string>("TORIHIKISAKI_TEL").ToString();
                    }
                    // FAX番号
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("TORIHIKISAKI_FAX"))
                    {
                        this.form.TORIHIKISAKI_FAX.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<string>("TORIHIKISAKI_FAX").ToString();
                    }
                    // 営業担当部署
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("EIGYOU_TANTOU_BUSHO_CD"))
                    {
                        this.form.EIGYOU_TANTOU_BUSHO_CD.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<string>("EIGYOU_TANTOU_BUSHO_CD").ToString();
                        this.form.EIGYOU_TANTOU_BUSHO_NAME.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<string>("EIGYOU_TANTOU_BUSHO_NAME");
                    }
                    // 営業担当者
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("EIGYOU_TANTOU_CD"))
                    {
                        this.form.EIGYOU_TANTOU_CD.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<string>("EIGYOU_TANTOU_CD").ToString();
                        this.form.EIGYOU_TANTOU_NAME.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<string>("EIGYOU_TANTOU_NAME");
                    }
                    // 適用期間（開始日）
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("TORIHIKISAKI_TEKIYOU_BEGIN"))
                    {
                        this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<DateTime>("TORIHIKISAKI_TEKIYOU_BEGIN").ToLongDateString();
                    }
                    // 適用期間（終了日）
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("TORIHIKISAKI_TEKIYOU_END"))
                    {
                        this.form.TORIHIKISAKI_TEKIYOU_END.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<DateTime>("TORIHIKISAKI_TEKIYOU_END").ToLongDateString();
                    }
                    // 中止理由1
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("CHUUSHI_RIYUU1"))
                    {
                        this.form.CHUUSHI_RIYUU1.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<string>("CHUUSHI_RIYUU1").ToString();
                    }
                    // 中止理由2
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("CHUUSHI_RIYUU2"))
                    {
                        this.form.CHUUSHI_RIYUU2.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<string>("CHUUSHI_RIYUU2").ToString();
                    }
                    // 最終更新ユーザ
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("UPDATE_USER"))
                    {
                        this.form.TORIHIKISAKI_LastUpdateUser.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<string>("UPDATE_USER").ToString();
                    }
                    // 最終更新日時
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("UPDATE_DATE"))
                    {
                        this.form.TORIHIKISAKI_LastUpdateDate.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<DateTime>("UPDATE_DATE").ToString();
                    }
                    // 初回登録ユーザ
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("CREATE_USER"))
                    {
                        this.form.TORIHIKISAKI_CreateUser.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<string>("CREATE_USER").ToString();
                    }
                    // 初回登録日時
                    if (!this.TorihikisakiHeaderSearchResult.Rows[0].IsNull("CREATE_DATE"))
                    {
                        this.form.TORIHIKISAKI_CreateDate.Text = this.TorihikisakiHeaderSearchResult.Rows[0].Field<DateTime>("CREATE_DATE").ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先の基本情報の表示
        /// </summary>
        internal void TorihikisakiKihonHyouji()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (null != this.TorihikisakiKihonSearchResult && this.TorihikisakiKihonSearchResult.Rows.Count > 0)
                {
                    //郵便番号
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("TORIHIKISAKI_POST"))
                    {
                        this.form.TORIHIKISAKI_POST.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("TORIHIKISAKI_POST");
                    }
                    //都道府県
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("TODOUFUKEN_CD"))
                    {
                        this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<Int16>("TODOUFUKEN_CD").ToString();
                        this.form.TORIHIKISAKI_TODOUFUKEN_NAME.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("TODOUFUKEN_NAME");
                    }
                    //住所1
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("TORIHIKISAKI_ADDRESS1"))
                    {
                        this.form.TORIHIKISAKI_ADDRESS1_KIHON.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("TORIHIKISAKI_ADDRESS1");
                    }
                    //住所2
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("TORIHIKISAKI_ADDRESS2"))
                    {
                        this.form.TORIHIKISAKI_ADDRESS2_KIHON.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("TORIHIKISAKI_ADDRESS2");
                    }
                    //部署
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("BUSHO"))
                    {
                        this.form.BUSHO.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("BUSHO");
                    }
                    //担当者
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("TANTOUSHA"))
                    {
                        this.form.TANTOUSHA.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("TANTOUSHA");
                    }
                    //集計項目
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("SHUUKEI_ITEM_CD"))
                    {
                        this.form.SHUUKEI_ITEM_CD.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("SHUUKEI_ITEM_CD");
                        this.form.SHUUKEI_KOUMOKU_NAME.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("SHUUKEI_ITEM_NAME_RYAKU");
                    }
                    //業種
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("GYOUSHU_CD"))
                    {
                        this.form.GYOUSHU_CD.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("GYOUSHU_CD");
                        this.form.GYOUSHU_NAME.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("GYOUSHU_NAME_RYAKU");
                    }
                    //代表者を印字
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("DAIHYOU_PRINT_KBN"))
                    {
                        this.form.DAIHYOU_PRINT_KBN.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<Int16>("DAIHYOU_PRINT_KBN").ToString();
                    }
                    //備考
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("BIKOU1"))
                    {
                        this.form.BIKOU1.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("BIKOU1");
                    }
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("BIKOU2"))
                    {
                        this.form.BIKOU2.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("BIKOU2");
                    }
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("BIKOU3"))
                    {
                        this.form.BIKOU3.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("BIKOU3");
                    }
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("BIKOU4"))
                    {
                        this.form.BIKOU4.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("BIKOU4");
                    }
                    //諸口
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("SHOKUCHI_KBN"))
                    {
                        this.form.SHOKUCHI_KBN.Checked = this.TorihikisakiKihonSearchResult.Rows[0].Field<Boolean>("SHOKUCHI_KBN");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先の請求情報1の表示
        /// </summary>
        internal void TorihikisakiSeikyuuHyouji()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //取引区分
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("TORIHIKI_KBN_CD"))
                {
                    this.form.TORIHIKI_KBN.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("TORIHIKI_KBN_CD").ToString();
                }
                //締日
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SHIMEBI1"))
                {
                    this.form.SHIMEBI1.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("SHIMEBI1").ToString();
                }
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SHIMEBI2"))
                {
                    this.form.SHIMEBI2.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("SHIMEBI2").ToString();
                }
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SHIMEBI3"))
                {
                    this.form.SHIMEBI3.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("SHIMEBI3").ToString();
                }
                //必着日
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("HICCHAKUBI"))
                {
                    this.form.HICCHAKUBI.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("HICCHAKUBI").ToString();
                }
                //160026 S
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("KAISHUU_BETSU_KBN"))
                {
                    this.form.KAISHUU_BETSU_KBN.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("KAISHUU_BETSU_KBN").ToString();
                }
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("KAISHUU_BETSU_NICHIGO"))
                {
                    this.form.KAISHUU_BETSU_NICHIGO.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("KAISHUU_BETSU_NICHIGO").ToString();
                }
                //160026 E

                //回収月
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("KAISHUU_MONTH"))
                {
                    this.form.KAISHUU_MONTH.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("KAISHUU_MONTH").ToString();
                }
                //回収日
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("KAISHUU_DAY"))
                {
                    this.form.KAISHUU_DAY.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("KAISHUU_DAY").ToString();
                }
                //回収方法
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("KAISHUU_HOUHOU_KBN"))
                {
                    this.form.KAISHUU_HOUHOU.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("KAISHUU_HOUHOU_KBN").ToString();
                    this.form.KAISHUU_HOUHOU_NAME.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("NYUUSHUKKIN_KBN_NAME");
                }
                //開始売掛残高
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("KAISHI_URIKAKE_ZANDAKA"))
                {
                    this.form.KAISHI_URIKAKE_ZANDAKA.Text = textFormat(1, this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<decimal>("KAISHI_URIKAKE_ZANDAKA").ToString());
                }
                //書式1
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SHOSHIKI_KBN"))
                {
                    this.form.SHOSHIKI_KBN.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("SHOSHIKI_KBN").ToString();
                }
                //書式2
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SHOSHIKI_MEISAI_KBN"))
                {
                    this.form.SHOSHIKI_MEISAI_KBN.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("SHOSHIKI_MEISAI_KBN").ToString();
                }
                //書式3
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SHOSHIKI_GENBA_KBN"))
                {
                    this.form.SHOSHIKI_GENBA_KBN.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("SHOSHIKI_GENBA_KBN").ToString();
                }
                //消費税端数
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("TAX_HASUU_CD"))
                {
                    this.form.TAX_HASUU_CD.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("TAX_HASUU_CD").ToString();
                }
                //金額端数
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("KINGAKU_HASUU_CD"))
                {
                    this.form.KINGAKU_HASUU_CD.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("KINGAKU_HASUU_CD").ToString();
                }
                //請求形態
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SEIKYUU_KEITAI_KBN"))
                {
                    this.form.SEIKYUU_KEITAI_KBN.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("SEIKYUU_KEITAI_KBN").ToString();
                }
                //入金明細
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("NYUUKIN_MEISAI_KBN"))
                {
                    this.form.NYUUKIN_MEISAI_KBN.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("NYUUKIN_MEISAI_KBN").ToString();
                }
                //用紙区分
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("YOUSHI_KBN"))
                {
                    this.form.YOUSHI_KBN.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("YOUSHI_KBN").ToString();
                }
                //税区分1
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("ZEI_KEISAN_KBN_CD"))
                {
                    this.form.ZEI_KEISAN_KBN_CD.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("ZEI_KEISAN_KBN_CD").ToString();
                }
                //税区分2
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("ZEI_KBN_CD"))
                {
                    this.form.ZEI_KBN_CD.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("ZEI_KBN_CD").ToString();
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                //出力区分
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("OUTPUT_KBN"))
                {
                    this.form.OUTPUT_KBN.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("OUTPUT_KBN").ToString();
                }
                //発行先コード
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("HAKKOUSAKI_CD"))
                {
                    this.form.HAKKOUSAKI_CD.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("HAKKOUSAKI_CD");
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 end

                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("INXS_SEIKYUU_KBN"))
                {
                    this.form.INXS_SEIKYUU_KBN.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("INXS_SEIKYUU_KBN").ToString();
                }
                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先の請求情報2の表示
        /// </summary>
        internal void TorihikisakiSeikyuuHyouji2()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //振込銀行
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("FURIKOMI_BANK_CD"))
                {
                    this.form.FURIKOMI_BANK_CD.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("FURIKOMI_BANK_CD");
                    this.form.FURIKOMI_BANK_NAME.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("BANK_NAME_RYAKU");
                }
                //支店
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("FURIKOMI_BANK_SHITEN_CD"))
                {
                    this.form.FURIKOMI_BANK_SHITEN_CD.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("FURIKOMI_BANK_SHITEN_CD");
                    this.form.FURIKOMI_BANK_SHITEN_NAME.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("BANK_SHIETN_NAME_RYAKU");
                }
                //口座種類
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("KOUZA_SHURUI"))
                {
                    this.form.KOUZA_SHURUI.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("KOUZA_SHURUI");
                }
                //口座番号
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("KOUZA_NO"))
                {
                    this.form.KOUZA_NO.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("KOUZA_NO").ToString();
                }
                //口座名
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("KOUZA_NAME"))
                {
                    this.form.KOUZA_NAME.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("KOUZA_NAME");
                }
                //最終取引日時
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("LAST_TORIHIKI_DATE"))
                {
                    this.form.LAST_TORIHIKI_DATE.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<DateTime>("LAST_TORIHIKI_DATE").ToString();
                }
                //請求情報
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SEIKYUU_JOUHOU1"))
                {
                    this.form.SEIKYUU_JOUHOU1.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("SEIKYUU_JOUHOU1");
                }
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SEIKYUU_JOUHOU2"))
                {
                    this.form.SEIKYUU_JOUHOU2.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("SEIKYUU_JOUHOU2");
                }
                //請求書送付先
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_NAME1"))
                {
                    this.form.SEIKYUU_SOUFU_NAME1.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_NAME1");
                }
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_KEISHOU1"))
                {
                    this.form.SEIKYUU_SOUFU_KEISHOU1.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_KEISHOU1");
                }
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_NAME2"))
                {
                    this.form.SEIKYUU_SOUFU_NAME2.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_NAME2");
                }
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_KEISHOU2"))
                {
                    this.form.SEIKYUU_SOUFU_KEISHOU2.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_KEISHOU2");
                }
                //送付先郵便番号
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_POST"))
                {
                    this.form.SEIKYUU_SOUFU_POST.Text = textFormat(3, this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_POST"));
                }
                //送付先住所
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_ADDRESS1"))
                {
                    this.form.SEIKYUU_SOUFU_ADDRESS1.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_ADDRESS1");
                }
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_ADDRESS2"))
                {
                    this.form.SEIKYUU_SOUFU_ADDRESS2.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_ADDRESS2");
                }
                //送付先部署
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_BUSHO"))
                {
                    this.form.SEIKYUU_SOUFU_BUSHO.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_BUSHO");
                }
                //送付先TEL
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_TEL"))
                {
                    this.form.SEIKYUU_SOUFU_TEL.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_TEL");
                }
                //送付先FAX
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_FAX"))
                {
                    this.form.SEIKYUU_SOUFU_FAX.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_FAX");
                }
                //入金先
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("NYUUKINSAKI_CD"))
                {
                    this.form.NYUUKINSAKI_CD.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("NYUUKINSAKI_CD");
                    this.form.NYUUKINSAKI_NAME1.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("NYUUKINSAKI_NAME1");
                    this.form.NYUUKINSAKI_NAME2.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("NYUUKINSAKI_NAME2");
                }
                //送付先担当者
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_TANTOU"))
                {
                    this.form.SEIKYUU_SOUFU_TANTOU.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_TANTOU");
                }
                //請求担当者
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SEIKYUU_TANTOU"))
                {
                    this.form.SEIKYUU_TANTOU.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("SEIKYUU_TANTOU");
                }
                //代表印字
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SEIKYUU_DAIHYOU_PRINT_KBN"))
                {
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("SEIKYUU_DAIHYOU_PRINT_KBN").ToString();
                }
                //拠点印字
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SEIKYUU_KYOTEN_PRINT_KBN"))
                {
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("SEIKYUU_KYOTEN_PRINT_KBN").ToString();
                }
                //請求書拠点
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("SEIKYUU_KYOTEN_CD"))
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<Int16>("SEIKYUU_KYOTEN_CD").ToString();
                    this.form.SEIKYUU_KYOTEN_NAME.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("KYOTEN_NAME_RYAKU");
                }
                //振込人名
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("FURIKOMI_NAME1"))
                {
                    this.form.FURIKOMI_NAME1.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("FURIKOMI_NAME1").ToString();
                }
                if (!this.TorihikisakiSeikyuuSearchResult.Rows[0].IsNull("FURIKOMI_NAME2"))
                {
                    this.form.FURIKOMI_NAME2.Text = this.TorihikisakiSeikyuuSearchResult.Rows[0].Field<string>("FURIKOMI_NAME2").ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先の支払情報1の表示
        /// </summary>
        internal void TorihikisakiShiharaiHyouji()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //取引区分
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("TORIHIKI_KBN_CD"))
                {
                    this.form.SHIHARAI_TORIHIKI_KBN_CD.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("TORIHIKI_KBN_CD").ToString();
                }
                //締日
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIMEBI1"))
                {
                    this.form.SHIHARAI_SHIMEBI1.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("SHIMEBI1").ToString();
                }
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIMEBI2"))
                {
                    this.form.SHIHARAI_SHIMEBI2.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("SHIMEBI2").ToString();
                }
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIMEBI3"))
                {
                    this.form.SHIHARAI_SHIMEBI3.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("SHIMEBI3").ToString();
                }
                //160026 S
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_BETSU_KBN"))
                {
                    this.form.SHIHARAI_BETSU_KBN.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("SHIHARAI_BETSU_KBN").ToString();
                }
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_BETSU_NICHIGO"))
                {
                    this.form.SHIHARAI_BETSU_NICHIGO.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("SHIHARAI_BETSU_NICHIGO").ToString();
                }
                //160026 E
                //支払月
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_MONTH"))
                {
                    this.form.SHIHARAI_MONTH.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("SHIHARAI_MONTH").ToString();
                }
                //支払日
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_DAY"))
                {
                    this.form.SHIHARAI_DAY.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("SHIHARAI_DAY").ToString();
                }
                //支払方法
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_HOUHOU_KBN"))
                {
                    this.form.SHIHARAI_HOUHOU.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("SHIHARAI_HOUHOU_KBN").ToString();
                    this.form.SHIHARAI_HOUHOU_NAME.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<string>("NYUUSHUKKIN_KBN_NAME");
                }
                //開始買掛残高
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("KAISHI_KAIKAKE_ZANDAKA"))
                {
                    this.form.KAISHI_KAIKAKE_ZANDAKA.Text = textFormat(1, this.TorihikisakiShiharaiSearchResult.Rows[0].Field<decimal>("KAISHI_KAIKAKE_ZANDAKA").ToString());
                }
                //書式1
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHOSHIKI_KBN"))
                {
                    this.form.SHIHARAI_SHOSHIKI_KBN.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("SHOSHIKI_KBN").ToString();
                }
                //書式2
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHOSHIKI_MEISAI_KBN"))
                {
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("SHOSHIKI_MEISAI_KBN").ToString();
                }
                //書式3
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHOSHIKI_GENBA_KBN"))
                {
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("SHOSHIKI_GENBA_KBN").ToString();
                }
                //消費税端数
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("TAX_HASUU_CD"))
                {
                    this.form.SHIHARAI_TAX_HASUU_CD.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("TAX_HASUU_CD").ToString();
                }
                //金額端数
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("KINGAKU_HASUU_CD"))
                {
                    this.form.SHIHARAI_KINGAKU_HASUU_CD.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("KINGAKU_HASUU_CD").ToString();
                }
                //支払形態
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_KEITAI_KBN"))
                {
                    this.form.SHIHARAI_KEITAI_KBN.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("SHIHARAI_KEITAI_KBN").ToString();
                }
                //出金明細
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHUKKIN_MEISAI_KBN"))
                {
                    this.form.SHUKKIN_MEISAI_KBN.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("SHUKKIN_MEISAI_KBN").ToString();
                }
                //用紙区分
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("YOUSHI_KBN"))
                {
                    this.form.SHIHARAI_YOUSHI_KBN.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("YOUSHI_KBN").ToString();
                }
                //税区分1
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("ZEI_KEISAN_KBN_CD"))
                {
                    this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("ZEI_KEISAN_KBN_CD").ToString();
                }
                //税区分2
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("ZEI_KBN_CD"))
                {
                    this.form.SHIHARAI_ZEI_KBN_CD.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("ZEI_KBN_CD").ToString();
                }
                //最終取引日時
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("LAST_TORIHIKI_DATE"))
                {
                    this.form.LAST_TORIHIKI_DATE_SHIHARAI.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<DateTime>("LAST_TORIHIKI_DATE").ToString();
                }

                // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("INXS_SHIHARAI_KBN"))
                {
                    this.form.INXS_SHIHARAI_KBN.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("INXS_SHIHARAI_KBN").ToString();
                }
                // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// 取引先の支払情報2の表示
        /// </summary>
        internal void TorihikisakiShiharaiHyouji2()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //支払情報
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_JOUHOU1"))
                {
                    this.form.SHIHARAI_JOUHOU1.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<string>("SHIHARAI_JOUHOU1");
                }
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_JOUHOU2"))
                {
                    this.form.SHIHARAI_JOUHOU2.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<string>("SHIHARAI_JOUHOU2");
                }
                //支払明細書送付先
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_NAME1"))
                {
                    this.form.SHIHARAI_SOUFU_NAME1.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_NAME1");
                }
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_KEISHOU1"))
                {
                    this.form.SHIHARAI_SOUFU_KEISHOU1.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_KEISHOU1");
                }
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_NAME2"))
                {
                    this.form.SHIHARAI_SOUFU_NAME2.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_NAME2");
                }
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_KEISHOU2"))
                {
                    this.form.SHIHARAI_SOUFU_KEISHOU2.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_KEISHOU2");
                }
                //送付先郵便番号
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_POST"))
                {
                    this.form.SHIHARAI_SOUFU_POST.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_POST");
                }
                //送付先住所
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_ADDRESS1"))
                {
                    this.form.SHIHARAI_SOUFU_ADDRESS1.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_ADDRESS1");
                }
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_ADDRESS2"))
                {
                    this.form.SHIHARAI_SOUFU_ADDRESS2.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_ADDRESS2");
                }
                //送付先部署
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_BUSHO"))
                {
                    this.form.SHIHARAI_SOUFU_BUSHO.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_BUSHO");
                }
                //送付先担当者
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_TANTOU"))
                {
                    this.form.SHIHARAI_SOUFU_TANTOU.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_TANTOU");
                }
                //送付先電話番号
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_TEL"))
                {
                    this.form.SHIHARAI_SOUFU_TEL.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_TEL");
                }
                //送付先FAX番号
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_FAX"))
                {
                    this.form.SHIHARAI_SOUFU_FAX.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_FAX");
                }
                //拠点名を印字
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_KYOTEN_PRINT_KBN"))
                {
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("SHIHARAI_KYOTEN_PRINT_KBN").ToString();
                }
                //支払書拠点
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("SHIHARAI_KYOTEN_CD"))
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<Int16>("SHIHARAI_KYOTEN_CD").ToString();
                    this.form.SHIHARAI_KYOTEN_NAME.Text = this.TorihikisakiShiharaiSearchResult.Rows[0].Field<string>("KYOTEN_NAME_RYAKU");
                }
                //160026 S
                //振込先銀行
                this.form.FURIKOMI_EXPORT_KBN.Text = this.TorihikisakiShiharaiSearchResult.Rows[0]["FURIKOMI_EXPORT_KBN"].ConvertToString(string.Empty);
                this.form.FURIKOMI_SAKI_BANK_CD.Text = this.TorihikisakiShiharaiSearchResult.Rows[0]["FURIKOMI_SAKI_BANK_CD"].ConvertToString(string.Empty);
                this.form.FURIKOMI_SAKI_BANK_NAME.Text = this.TorihikisakiShiharaiSearchResult.Rows[0]["FURIKOMI_SAKI_BANK_NAME"].ConvertToString(string.Empty);
                this.form.FURIKOMI_SAKI_BANK_SHITEN_CD.Text = this.TorihikisakiShiharaiSearchResult.Rows[0]["FURIKOMI_SAKI_BANK_SHITEN_CD"].ConvertToString(string.Empty);
                this.form.FURIKOMI_SAKI_BANK_SHITEN_NAME.Text = this.TorihikisakiShiharaiSearchResult.Rows[0]["FURIKOMI_SAKI_BANK_SHITEN_NAME"].ConvertToString(string.Empty);
                this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Text = this.TorihikisakiShiharaiSearchResult.Rows[0]["FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD"].ConvertToString(string.Empty);
                this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI.Text = this.TorihikisakiShiharaiSearchResult.Rows[0]["FURIKOMI_SAKI_BANK_KOUZA_SHURUI"].ConvertToString(string.Empty);
                this.form.FURIKOMI_SAKI_BANK_KOUZA_NO.Text = this.TorihikisakiShiharaiSearchResult.Rows[0]["FURIKOMI_SAKI_BANK_KOUZA_NO"].ConvertToString(string.Empty);
                this.form.FURIKOMI_SAKI_BANK_KOUZA_NAME.Text = this.TorihikisakiShiharaiSearchResult.Rows[0]["FURIKOMI_SAKI_BANK_KOUZA_NAME"].ConvertToString(string.Empty);
                this.form.TEI_SUU_RYOU_KBN.Text = this.TorihikisakiShiharaiSearchResult.Rows[0]["TEI_SUU_RYOU_KBN"].ConvertToString(string.Empty);
                //振込元銀行                
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("FURI_KOMI_MOTO_BANK_CD"))
                {
                    this.form.FURI_KOMI_MOTO_BANK_CD.Text = this.TorihikisakiShiharaiSearchResult.Rows[0]["FURI_KOMI_MOTO_BANK_CD"].ConvertToString(string.Empty);
                    M_BANK entitysM_BANK = DaoInitUtility.GetComponent<IM_BANKDao>().GetDataByCd(this.form.FURI_KOMI_MOTO_BANK_CD.Text);
                    if (entitysM_BANK != null)
                    {
                        this.form.FURI_KOMI_MOTO_BANK_NAME.Text = entitysM_BANK.BANK_NAME_RYAKU;
                    }
                }
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("FURI_KOMI_MOTO_BANK_SHITTEN_CD"))
                {
                    this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text = this.TorihikisakiShiharaiSearchResult.Rows[0]["FURI_KOMI_MOTO_BANK_SHITTEN_CD"].ConvertToString(string.Empty);
                    this.form.FURI_KOMI_MOTO_SHURUI.Text = this.TorihikisakiShiharaiSearchResult.Rows[0]["FURI_KOMI_MOTO_SHURUI"].ConvertToString(string.Empty);
                    this.form.FURI_KOMI_MOTO_NO.Text = this.TorihikisakiShiharaiSearchResult.Rows[0]["FURI_KOMI_MOTO_NO"].ConvertToString(string.Empty);
                }
                if (!this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("FURI_KOMI_MOTO_BANK_CD") && !this.TorihikisakiShiharaiSearchResult.Rows[0].IsNull("FURI_KOMI_MOTO_BANK_SHITTEN_CD"))
                {
                    M_BANK_SHITEN bankShiten = new M_BANK_SHITEN();
                    bankShiten.BANK_CD = this.TorihikisakiShiharaiSearchResult.Rows[0]["FURI_KOMI_MOTO_BANK_CD"].ConvertToString(string.Empty);
                    bankShiten.BANK_SHITEN_CD = this.TorihikisakiShiharaiSearchResult.Rows[0]["FURI_KOMI_MOTO_BANK_SHITTEN_CD"].ConvertToString(string.Empty);
                    bankShiten.KOUZA_SHURUI = this.TorihikisakiShiharaiSearchResult.Rows[0]["FURI_KOMI_MOTO_SHURUI"].ConvertToString(string.Empty);
                    bankShiten.KOUZA_NO = this.TorihikisakiShiharaiSearchResult.Rows[0]["FURI_KOMI_MOTO_NO"].ConvertToString(string.Empty);
                    M_BANK_SHITEN entitysM_BANK_SHITEN = DaoInitUtility.GetComponent<IM_BANK_SHITENDao>().GetDataByCd(bankShiten);
                    if (entitysM_BANK_SHITEN != null)
                    {
                        this.form.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = entitysM_BANK_SHITEN.BANK_SHIETN_NAME_RYAKU;
                        this.form.FURI_KOMI_MOTO_NAME.Text = entitysM_BANK_SHITEN.KOUZA_NAME;
                    }
                }
                //160026 E
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// 取引先分類の表示
        /// </summary>
        internal void TorihikisakiBunruiHyouji()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //マニフェスト返送先区分
                if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_KBN"))
                {
                    this.form.MANI_HENSOUSAKI_KBN.Checked = this.TorihikisakiKihonSearchResult.Rows[0].Field<Boolean>("MANI_HENSOUSAKI_KBN");
                }
                // 返送先情報
                if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_THIS_ADDRESS_KBN"))
                {
                    this.form.TORIHIKISAKI_MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = Convert.ToString(this.TorihikisakiKihonSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_THIS_ADDRESS_KBN"));
                    if (this.form.TORIHIKISAKI_MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text == "1")
                    {
                        this.form.TORIHIKISAKI_MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = true;
                    }
                    else if (this.form.TORIHIKISAKI_MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text == "2")
                    {
                        this.form.TORIHIKISAKI_MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                    }
                }
                if ("2".Equals(this.form.TORIHIKISAKI_MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text))
                {
                    //マニフェスト返送先
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_NAME1"))
                    {
                        this.form.MANI_HENSOUSAKI_NAME1.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_NAME1");
                    }
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_KEISHOU1"))
                    {
                        this.form.MANI_HENSOUSAKI_KEISHOU1.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_KEISHOU1");
                    }
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_NAME2"))
                    {
                        this.form.MANI_HENSOUSAKI_NAME2.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_NAME2");
                    }
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_KEISHOU2"))
                    {
                        this.form.MANI_HENSOUSAKI_KEISHOU2.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_KEISHOU2");
                    }
                    //マニフェスト返送先郵便番号
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_POST"))
                    {
                        this.form.MANI_HENSOUSAKI_POST.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_POST");
                    }
                    //マニフェスト返送先住所
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_ADDRESS1"))
                    {
                        this.form.MANI_HENSOUSAKI_ADDRESS1.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_ADDRESS1");
                    }
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_ADDRESS2"))
                    {
                        this.form.MANI_HENSOUSAKI_ADDRESS2.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_ADDRESS2");
                    }
                    //マニフェスト返送先部署
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_BUSHO"))
                    {
                        this.form.MANI_HENSOUSAKI_BUSHO.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_BUSHO");
                    }
                    //マニフェスト返送先担当者
                    if (!this.TorihikisakiKihonSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_TANTOU"))
                    {
                        this.form.MANI_HENSOUSAKI_TANTOU.Text = this.TorihikisakiKihonSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TANTOU");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索結果を業者一覧に設定
        /// </summary>
        internal void TorihikisakiGyoushaIchiranHyouji()
        {
            var table = this.TorihikisakiGyoushaIchiranSearchResult;

            if (null == table) { return; }

            table.BeginLoadData();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].ReadOnly = false;
            }
            this.form.GYOUSHA_ICHIRAN.IsBrowsePurpose = false;
            this.form.GYOUSHA_ICHIRAN.DataSource = table;
            this.form.GYOUSHA_ICHIRAN.IsBrowsePurpose = true;
        }

        #endregion

        #region 表示：業者タブ

        /// <summary>
        /// 業者のヘッダ情報の表示
        /// </summary>
        internal void GyoushaHeaderHyouji()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (null != this.GyoushaInfoSearchResult && this.GyoushaInfoSearchResult.Rows.Count > 0)
                {
                    //取引先有無区分
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("TORIHIKISAKI_UMU_KBN"))
                    {
                        this.form.TORIHIKISAKI_UMU_KBN.Text = this.GyoushaInfoSearchResult.Rows[0].Field<Int16>("TORIHIKISAKI_UMU_KBN").ToString();
                    }
                    //業者区分(受入)
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("GYOUSHAKBN_UKEIRE"))
                    {
                        this.form.Gyousha_KBN_1.Checked = this.GyoushaInfoSearchResult.Rows[0].Field<Boolean>("GYOUSHAKBN_UKEIRE");
                    }
                    //業者区分(出荷)
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("GYOUSHAKBN_SHUKKA"))
                    {
                        this.form.Gyousha_KBN_2.Checked = this.GyoushaInfoSearchResult.Rows[0].Field<Boolean>("GYOUSHAKBN_SHUKKA");
                    }
                    //業者区分(マニ記載業者)
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("GYOUSHAKBN_MANI"))
                    {
                        this.form.Gyousha_KBN_3.Checked = this.GyoushaInfoSearchResult.Rows[0].Field<Boolean>("GYOUSHAKBN_MANI");
                    }
                    //取引先
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("TORIHIKISAKI_CD"))
                    {
                        this.form.GYOUSHA_TORIHIKISAKI_CD.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("TORIHIKISAKI_CD");
                        this.form.GYOUSHA_TORIHIKISAKI_NAME1.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("TORIHIKISAKI_NAME1");
                        this.form.GYOUSHA_TORIHIKISAKI_NAME2.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("TORIHIKISAKI_NAME2");
                    }
                    //取引先適用開始日
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("TORIHIKISAKI_TEKIYOU_BEGIN"))
                    {
                        this.form.GYO_TORIHIKISAKI_TEKIYOU_BEGIN.Text = this.GyoushaInfoSearchResult.Rows[0].Field<DateTime>("TORIHIKISAKI_TEKIYOU_BEGIN").ToLongDateString();
                    }
                    //取引先適用終了日
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("TORIHIKISAKI_TEKIYOU_END"))
                    {
                        this.form.GYO_TORIHIKISAKI_TEKIYOU_END.Text = this.GyoushaInfoSearchResult.Rows[0].Field<DateTime>("TORIHIKISAKI_TEKIYOU_END").ToLongDateString();
                    }
                    //拠点
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("KYOTEN_CD"))
                    {
                        this.form.KYOTEN_CD.Text = this.GyoushaInfoSearchResult.Rows[0].Field<Int16>("KYOTEN_CD").ToString();
                        this.form.KYOTEN_NAME.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("KYOTEN_NAME");
                    }
                    //業者CD
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("GYOUSHA_CD"))
                    {
                        this.form.GYOUSHA_GYOUSHA_CD.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("GYOUSHA_CD");
                    }
                    //フリガナ
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("GYOUSHA_FURIGANA"))
                    {
                        this.form.GYOUSHA_FURIGANA.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("GYOUSHA_FURIGANA");
                    }
                    //業者名1
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("GYOUSHA_NAME1"))
                    {
                        this.form.GYOUSHA_NAME1.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("GYOUSHA_NAME1");
                    }
                    //業者敬称1
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("GYOUSHA_KEISHOU1"))
                    {
                        this.form.GYOUSHA_KEISHOU1.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("GYOUSHA_KEISHOU1");
                    }
                    //業者名2
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("GYOUSHA_NAME2"))
                    {
                        this.form.GYOUSHA_NAME2.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("GYOUSHA_NAME2");
                    }
                    //業者敬称2
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("GYOUSHA_KEISHOU2"))
                    {
                        this.form.GYOUSHA_KEISHOU2.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("GYOUSHA_KEISHOU2");
                    }
                    //業者略称
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("GYOUSHA_NAME_RYAKU"))
                    {
                        this.form.GYOUSHA_GYOUSHA_NAME_RYAKU.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("GYOUSHA_NAME_RYAKU");
                    }
                    //業者電話番号
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("GYOUSHA_TEL"))
                    {
                        this.form.GYOUSHA_GYOUSHA_TEL.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("GYOUSHA_TEL");
                    }
                    //業者携帯番号
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("GYOUSHA_KEITAI_TEL"))
                    {
                        this.form.GYOUSHA_KEITAI_TEL.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("GYOUSHA_KEITAI_TEL");
                    }
                    //業者FAX番号
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("GYOUSHA_FAX"))
                    {
                        this.form.GYOUSHA_FAX.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("GYOUSHA_FAX");
                    }
                    //業者営業担当部署
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("EIGYOU_TANTOU_BUSHO_CD"))
                    {
                        this.form.GYOUSHA_EIGYOU_TANTOU_BUSHO_CD.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("EIGYOU_TANTOU_BUSHO_CD");
                        this.form.BUSHO_NAME.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("BUSHO_NAME");
                    }
                    //業者営業担当者
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("EIGYOU_TANTOU_CD"))
                    {
                        this.form.GYOUSHA_EIGYOU_TANTOU_CD.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("EIGYOU_TANTOU_CD");
                        this.form.SHAIN_NAME.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SHAIN_NAME");
                    }
                    //適用開始日
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("TEKIYOU_BEGIN"))
                    {
                        this.form.GYOUSHA_TEKIYOU_BEGIN.Text = this.GyoushaInfoSearchResult.Rows[0].Field<DateTime>("TEKIYOU_BEGIN").ToLongDateString();
                    }
                    //適用終了日
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("TEKIYOU_END"))
                    {
                        this.form.GYOUSHA_TEKIYOU_END.Text = this.GyoushaInfoSearchResult.Rows[0].Field<DateTime>("TEKIYOU_END").ToLongDateString();
                    }
                    //中止理由
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("CHUUSHI_RIYUU1"))
                    {
                        this.form.GYOUSHA_CHUUSHI_RIYUU1.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("CHUUSHI_RIYUU1");
                    }
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("CHUUSHI_RIYUU2"))
                    {
                        this.form.GYOUSHA_CHUUSHI_RIYUU2.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("CHUUSHI_RIYUU2");
                    }
                    //諸口区分
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SHOKUCHI_KBN"))
                    {
                        this.form.GYOUSHA_SHOKUCHI_KBN.Checked = this.GyoushaInfoSearchResult.Rows[0].Field<Boolean>("SHOKUCHI_KBN");
                    }
                    //自社区分
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("JISHA_KBN"))
                    {
                        this.form.JISHA_KBN.Checked = this.GyoushaInfoSearchResult.Rows[0].Field<Boolean>("JISHA_KBN");
                    }
                    // 最終更新ユーザ
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("UPDATE_USER"))
                    {
                        this.form.GYOUSHA_LastUpdateUser.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("UPDATE_USER").ToString();
                    }
                    // 最終更新日時
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("UPDATE_DATE"))
                    {
                        this.form.GYOUSHA_LastUpdateDate.Text = this.GyoushaInfoSearchResult.Rows[0].Field<DateTime>("UPDATE_DATE").ToString();
                    }
                    // 初回登録ユーザ
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("CREATE_USER"))
                    {
                        this.form.GYOUSHA_CreateUser.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("CREATE_USER").ToString();
                    }
                    // 初回登録日時
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("CREATE_DATE"))
                    {
                        this.form.GYOUSHA_CreateDate.Text = this.GyoushaInfoSearchResult.Rows[0].Field<DateTime>("CREATE_DATE").ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者の基本情報の表示
        /// </summary>
        internal void GyoushaKihonHyouji()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (null != this.GyoushaInfoSearchResult && this.GyoushaInfoSearchResult.Rows.Count > 0)
                {
                    //郵便番号
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("GYOUSHA_POST"))
                    {
                        this.form.GYOUSHA_KIHON_POST_NO.Text = textFormat(3, this.GyoushaInfoSearchResult.Rows[0].Field<string>("GYOUSHA_POST"));
                    }
                    //都道府県
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("GYOUSHA_TODOUFUKEN_CD"))
                    {
                        this.form.GYOUSHA_KIHON_TODOUFUKEN_CD.Text = this.GyoushaInfoSearchResult.Rows[0].Field<Int16>("GYOUSHA_TODOUFUKEN_CD").ToString();
                        this.form.GYOUSHA_KIHON_TODOUFUKEN_NAME.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("TODOUFUKEN_NAME");
                    }
                    //住所
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("GYOUSHA_ADDRESS1"))
                    {
                        this.form.GYOUSHA_KIHON_ADDRESS1.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("GYOUSHA_ADDRESS1");
                    }
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("GYOUSHA_ADDRESS2"))
                    {
                        this.form.GYOUSHA_KIHON_ADDRESS2.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("GYOUSHA_ADDRESS2");
                    }
                    //地域
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("CHIIKI_CD"))
                    {
                        this.form.GYOUSHA_KIHON_CHIIKI_CD.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("CHIIKI_CD");
                        this.form.GYOUSHA_KIHON_CHIIKI_NAME.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("CHIIKI_NAME");
                    }
                    // 運搬報告書提出先
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD"))
                    {
                        this.form.GYOUSHA_UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD");
                    }
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME_RYAKU"))
                    {
                        this.form.GYOUSHA_UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME_RYAKU");
                    }
                    //部署
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("BUSHO"))
                    {
                        this.form.GYOUSHA_KIHON_BUSHO_NAME.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("BUSHO");
                    }
                    //担当者
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("TANTOUSHA"))
                    {
                        this.form.GYOUSHA_KIHON_TANTOUSHA_NAME.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("TANTOUSHA");
                    }
                    //代表者
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("GYOUSHA_DAIHYOU"))
                    {
                        this.form.GYOUSHA_DAIHYOU.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("GYOUSHA_DAIHYOU");
                    }
                    //集計項目
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SHUUKEI_ITEM_CD"))
                    {
                        this.form.GYOUSHA_KIHON_SHUKEIITEM_CD.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SHUUKEI_ITEM_CD");
                        this.form.GYOUSHA_KIHON_SHUKEIITEM_NAME.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SHUUKEI_KOUMOKU_NAME_RYAKU");
                    }
                    //業種
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("GYOUSHU_CD"))
                    {
                        this.form.GYOUSHA_KIHON_GYOUSHU_CD.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("GYOUSHU_CD");
                        this.form.GYOUSHA_KIHON_GYOUSHU_NAME.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("GYOUSHU_NAME_RYAKU");
                    }
                    //備考
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("BIKOU1"))
                    {
                        this.form.GYOUSHA_KIHON_BIKOU1.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("BIKOU1");
                    }
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("BIKOU2"))
                    {
                        this.form.GYOUSHA_KIHON_BIKOU2.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("BIKOU2");
                    }
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("BIKOU3"))
                    {
                        this.form.GYOUSHA_KIHON_BIKOU3.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("BIKOU3");
                    }
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("BIKOU4"))
                    {
                        this.form.GYOUSHA_KIHON_BIKOU4.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("BIKOU4");
                    }

                    //20250321
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("URIAGE_GURUPU_CD"))
                    {
                        this.form.URIAGE_GURUPU_CD.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("URIAGE_GURUPU_CD");
                    }
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("URIAGE_GURUPU_NAME"))
                    {
                        this.form.URIAGE_GURUPU_NAME.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("URIAGE_GURUPU_NAME");
                    }
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SHIHARAI_GURUPU_CD"))
                    {
                        this.form.SHIHARAI_GURUPU_CD.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_GURUPU_CD");
                    }
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SHIHARAI_GURUPU_NAME"))
                    {
                        this.form.SHIHARAI_GURUPU_NAME.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_GURUPU_NAME");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者の請求情報の表示
        /// </summary>
        internal void GyoushaSeikyuuHyouji()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //請求書送付先
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_NAME1"))
                {
                    this.form.GYOUSHA_SEIKYU_SOUFUSAKI1.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_NAME1");
                }
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_NAME2"))
                {
                    this.form.GYOUSHA_SEIKYU_SOUFUSAKI2.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_NAME2");
                }
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_KEISHOU1"))
                {
                    this.form.GYOUSHA_SEIKYUU_SOUFU_KEISHOU1.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_KEISHOU1");
                }
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_KEISHOU2"))
                {
                    this.form.GYOUSHA_SEIKYUU_SOUFU_KEISHOU2.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_KEISHOU2");
                }
                //請求送付先郵便番号
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_POST"))
                {
                    this.form.GYOUSHA_SEIKYU_SOUFUSAKI_POST_NO.Text = textFormat(3, this.GyoushaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_POST"));
                }
                //請求送付先住所
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_ADDRESS1"))
                {
                    this.form.GYOUSHA_SEIKYU_SOUFUSAKI_ADDRS1.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_ADDRESS1");
                }
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_ADDRESS2"))
                {
                    this.form.GYOUSHA_SEIKYU_SOUFUSAKI_ADDRS2.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_ADDRESS2");
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                //発行先コード
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SEIKYUU_HAKKOUSAKI_CD"))
                {
                    this.form.GYOUSHA_HAKKOUSAKI_CD.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_HAKKOUSAKI_CD");
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                //請求送付先部署
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_BUSHO"))
                {
                    this.form.GYOUSHA_SEIKYU_SOUFUSAKI_BUSHO_NAME.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_BUSHO");
                }
                //請求送付先担当者
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_TANTOU"))
                {
                    this.form.GYOUSHA_SEIKYU_SOUFUSAKI_TANTOUSHA_NAME.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_TANTOU");
                }
                //請求送付先TEL
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_TEL"))
                {
                    this.form.GYOUSHA_SEIKYU_SOUFUSAKI_TEL.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_TEL");
                }
                //請求送付先FAX
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_FAX"))
                {
                    this.form.GYOUSHA_SEIKYU_SOUFUSAKI_FAX.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_FAX");
                }
                //請求担当者
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SEIKYUU_TANTOU"))
                {
                    this.form.GYOUSHA_SEIKYU_SEIKYU_TANTOUSHA_NAME.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_TANTOU");
                }
                //代表印字
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SEIKYUU_DAIHYOU_PRINT_KBN"))
                {
                    this.form.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.GyoushaInfoSearchResult.Rows[0].Field<Int16>("SEIKYUU_DAIHYOU_PRINT_KBN").ToString();
                }
                //拠点印字
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SEIKYUU_KYOTEN_PRINT_KBN"))
                {
                    this.form.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN.Text = this.GyoushaInfoSearchResult.Rows[0].Field<Int16>("SEIKYUU_KYOTEN_PRINT_KBN").ToString();
                }
                //請求書拠点
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SEIKYUU_KYOTEN_CD"))
                {
                    this.form.GYOUSHA_SEIKYUU_KYOTEN_CD.Text = this.GyoushaInfoSearchResult.Rows[0].Field<Int16>("SEIKYUU_KYOTEN_CD").ToString();
                    this.form.GYOUSHA_SEIKYUU_KYOTEN_NAME.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_KYOTEN_NAME");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者の支払情報の表示
        /// </summary>
        internal void GyoushaShiharaiHyouji()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //支払書送付先
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_NAME1"))
                {
                    this.form.GYOUSHA_SHIHARAI_SOUFUSAKI1.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_NAME1");
                }
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_NAME2"))
                {
                    this.form.GYOUSHA_SHIHARAI_SOUFUSAKI2.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_NAME2");
                }
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_KEISHOU1"))
                {
                    this.form.GYOUSHA_SHIHARAI_SOUFU_KEISHOU1.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_KEISHOU1");
                }
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_KEISHOU2"))
                {
                    this.form.GYOUSHA_SHIHARAI_SOUFU_KEISHOU2.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_KEISHOU2");
                }
                //支払送付先郵便番号
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_POST"))
                {
                    this.form.GYOUSHA_SHIHARAI_SOUFUSAKI_POST_NO.Text = textFormat(3, this.GyoushaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_POST"));
                }
                //支払送付先住所
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_ADDRESS1"))
                {
                    this.form.GYOUSHA_SHIHARAI_SOUFUSAKI_ADDRS1.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_ADDRESS1");
                }
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_ADDRESS2"))
                {
                    this.form.GYOUSHA_SHIHARAI_SOUFUSAKI_ADDRS2.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_ADDRESS2");
                }
                //支払送付先部署
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_BUSHO"))
                {
                    this.form.GYOUSHA_SHIHARAI_SOUFUSAKI_BUSHO_NAME.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_BUSHO");
                }
                //支払送付先担当者
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_TANTOU"))
                {
                    this.form.GYOUSHA_SHIHARAI_SOUFUSAKI_TANTOUSHA_NAME.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_TANTOU");
                }
                //支払送付先TEL
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_TEL"))
                {
                    this.form.GYOUSHA_SHIHARAI_SOUFUSAKI_TEL.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_TEL");
                }
                //支払送付先FAX
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_FAX"))
                {
                    this.form.GYOUSHA_SHIHARAI_SOUFUSAKI_FAX.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_FAX");
                }
                //拠点印字
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SHIHARAI_KYOTEN_PRINT_KBN"))
                {
                    this.form.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN.Text = this.GyoushaInfoSearchResult.Rows[0].Field<Int16>("SHIHARAI_KYOTEN_PRINT_KBN").ToString();
                }
                //支払書拠点
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SHIHARAI_KYOTEN_CD"))
                {
                    this.form.GYOUSHA_SHIHARAI_KYOTEN_CD.Text = this.GyoushaInfoSearchResult.Rows[0].Field<Int16>("SHIHARAI_KYOTEN_CD").ToString();
                    this.form.GYOUSHA_SHIHARAI_KYOTEN_NAME.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_KYOTEN_NAME");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者の現場一覧の表示
        /// </summary>
        internal void GyoushaGenbaIchiranHyouji()
        {
            var table = this.GyoushaGenbaIchiranSearchResult;

            if (null == table) { return; }

            table.BeginLoadData();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].ReadOnly = false;
            }

            this.form.GENBA_ICHIRAN.IsBrowsePurpose = false;
            this.form.GENBA_ICHIRAN.DataSource = table;
            this.form.GENBA_ICHIRAN.IsBrowsePurpose = true;
        }

        /// <summary>
        /// 業者の分類情報の表示
        /// </summary>
        internal void GyoushaBunruiHyouji()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //排出事業者
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("HAISHUTSU_NIZUMI_GYOUSHA_KBN"))
                {
                    this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = this.GyoushaInfoSearchResult.Rows[0].Field<Boolean>("HAISHUTSU_NIZUMI_GYOUSHA_KBN");
                }
                //運搬受託者
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("UNPAN_JUTAKUSHA_KAISHA_KBN"))
                {
                    this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = this.GyoushaInfoSearchResult.Rows[0].Field<Boolean>("UNPAN_JUTAKUSHA_KAISHA_KBN");
                }
                //処分受託者
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("SHOBUN_NIOROSHI_GYOUSHA_KBN"))
                {
                    this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = this.GyoushaInfoSearchResult.Rows[0].Field<Boolean>("SHOBUN_NIOROSHI_GYOUSHA_KBN");
                }
                //マニフェスト返送先
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_KBN"))
                {
                    this.form.GYOUSHA_MANI_HENSOUSAKI_KBN.Checked = this.GyoushaInfoSearchResult.Rows[0].Field<Boolean>("MANI_HENSOUSAKI_KBN");
                }
                // 返送先情報
                if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_THIS_ADDRESS_KBN"))
                {
                    this.form.GYOUSHA_MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = Convert.ToString(this.GyoushaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_THIS_ADDRESS_KBN"));
                }
                if ("2".Equals(this.form.GYOUSHA_MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text))
                {
                    //返送先
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_NAME1"))
                    {
                        this.form.GYOUSHA_MANI_HENSOUSAKI_NAME1.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_NAME1");
                    }
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_KEISHOU1"))
                    {
                        this.form.GYOUSHA_MANI_HENSOUSAKI_KEISHOU1.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_KEISHOU1");
                    }
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_NAME2"))
                    {
                        this.form.GYOUSHA_MANI_HENSOUSAKI_NAME2.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_NAME2");
                    }
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_KEISHOU2"))
                    {
                        this.form.GYOUSHA_MANI_HENSOUSAKI_KEISHOU2.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_KEISHOU2");
                    }
                    //郵便番号
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_POST"))
                    {
                        this.form.GYOUSHA_MANI_HENSOUSAKI_POST.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_POST");
                    }
                    //住所
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_ADDRESS1"))
                    {
                        this.form.GYOUSHA_MANI_HENSOUSAKI_ADDRESS1.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_ADDRESS1");
                    }
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_ADDRESS2"))
                    {
                        this.form.GYOUSHA_MANI_HENSOUSAKI_ADDRESS2.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_ADDRESS2");
                    }
                    //部署
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_BUSHO"))
                    {
                        this.form.GYOUSHA_MANI_HENSOUSAKI_BUSHO.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_BUSHO");
                    }
                    //担当者
                    if (!this.GyoushaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_TANTOU"))
                    {
                        this.form.GYOUSHA_MANI_HENSOUSAKI_TANTOU.Text = this.GyoushaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TANTOU");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 表示：現場タブ

        /// <summary>
        /// 現場のヘッダ情報の表示
        /// </summary>
        internal void GenbaHeaderHyouji()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (null != this.GenbaInfoSearchResult && this.GenbaInfoSearchResult.Rows.Count > 0)
                {
                    //業者区分(受入)
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GYOUSHAKBN_UKEIRE"))
                    {
                        this.form.GyoushaKbnUkeire.Checked = this.GenbaInfoSearchResult.Rows[0].Field<Boolean>("GYOUSHAKBN_UKEIRE");
                    }
                    //業者区分(出荷)
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GYOUSHAKBN_SHUKKA"))
                    {
                        this.form.GyoushaKbnShukka.Checked = this.GenbaInfoSearchResult.Rows[0].Field<Boolean>("GYOUSHAKBN_SHUKKA");
                    }
                    //業者区分(マニ記載業者)
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GYOUSHAKBN_MANI"))
                    {
                        this.form.GyoushaKbnMani.Checked = this.GenbaInfoSearchResult.Rows[0].Field<Boolean>("GYOUSHAKBN_MANI");
                    }
                    //業者
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GYOUSHA_CD"))
                    {
                        this.form.GyoushaCode.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("GYOUSHA_CD");
                        this.form.GyoushaName1.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("GYOUSHA_NAME1");
                        this.form.GyoushaName2.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("GYOUSHA_NAME2");
                        //業者適用開始日
                        if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GYOUSHA_TEKIYOU_BEGIN"))
                        {
                            this.form.GEN_GYOUSHA_TEKIYOU_BEGIN.Text = this.GenbaInfoSearchResult.Rows[0].Field<DateTime>("GYOUSHA_TEKIYOU_BEGIN").ToLongDateString();
                        }
                        //業者適用終了日
                        if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GYOUSHA_TEKIYOU_END"))
                        {
                            this.form.GEN_GYOUSHA_TEKIYOU_END.Text = this.GenbaInfoSearchResult.Rows[0].Field<DateTime>("GYOUSHA_TEKIYOU_END").ToLongDateString();
                        }
                    }
                    //取引先
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("TORIHIKISAKI_CD"))
                    {
                        this.form.TorihikisakiCode.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("TORIHIKISAKI_CD");
                        this.form.TorihikisakiName1.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("TORIHIKISAKI_NAME1");
                        this.form.TorihikisakiName2.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("TORIHIKISAKI_NAME2");
                        //取引先適用開始日
                        if (!this.GenbaInfoSearchResult.Rows[0].IsNull("TORIHIKISAKI_TEKIYOU_BEGIN"))
                        {
                            this.form.GEN_TORIHIKISAKI_TEKIYOU_BEGIN.Text = this.GenbaInfoSearchResult.Rows[0].Field<DateTime>("TORIHIKISAKI_TEKIYOU_BEGIN").ToLongDateString();
                        }
                        //取引先適用終了日
                        if (!this.GenbaInfoSearchResult.Rows[0].IsNull("TORIHIKISAKI_TEKIYOU_END"))
                        {
                            this.form.GEN_TORIHIKISAKI_TEKIYOU_END.Text = this.GenbaInfoSearchResult.Rows[0].Field<DateTime>("TORIHIKISAKI_TEKIYOU_END").ToLongDateString();
                        }
                    }
                    //拠点
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("KYOTEN_CD"))
                    {
                        this.form.KyotenCode.Text = this.GenbaInfoSearchResult.Rows[0].Field<Int16>("KYOTEN_CD").ToString();
                        this.form.KyotenName.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("KYOTEN_NAME");
                    }
                    //現場コード
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GENBA_CD"))
                    {
                        this.form.GenbaCode.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("GENBA_CD");
                    }
                    //フリガナ
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GENBA_FURIGANA"))
                    {
                        this.form.GenbaFurigana.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("GENBA_FURIGANA");
                    }
                    //現場名
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GENBA_NAME1"))
                    {
                        this.form.GenbaName1.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("GENBA_NAME1");
                    }
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GENBA_KEISHOU1"))
                    {
                        this.form.GenbaKeishou1.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("GENBA_KEISHOU1");
                    }
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GENBA_NAME2"))
                    {
                        this.form.GenbaName2.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("GENBA_NAME2");
                    }
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GENBA_KEISHOU2"))
                    {
                        this.form.GenbaKeishou2.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("GENBA_KEISHOU2");
                    }
                    //現場名略
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GENBA_NAME_RYAKU"))
                    {
                        this.form.GenbaNameRyaku.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("GENBA_NAME_RYAKU");
                    }
                    //現場電話番号
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GENBA_TEL"))
                    {
                        this.form.GenbaTel.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("GENBA_TEL");
                    }
                    //現場携帯番号
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GENBA_KEITAI_TEL"))
                    {
                        this.form.GenbaKeitaiTel.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("GENBA_KEITAI_TEL");
                    }
                    //現場FAX番号
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GENBA_FAX"))
                    {
                        this.form.GenbaFax.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("GENBA_FAX");
                    }
                    //営業担当部署
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("EIGYOU_TANTOU_BUSHO_CD"))
                    {
                        this.form.EigyouTantouBushoCode.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("EIGYOU_TANTOU_BUSHO_CD");
                        this.form.EigyouTantouBushoName.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("BUSHO_NAME");
                    }
                    //営業担当者
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("EIGYOU_TANTOU_CD"))
                    {
                        this.form.EigyouCode.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("EIGYOU_TANTOU_CD");
                        this.form.EigyouName.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SHAIN_NAME");
                    }
                    //適用開始
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("TEKIYOU_BEGIN"))
                    {
                        this.form.TekiyouBegin.Text = this.GenbaInfoSearchResult.Rows[0].Field<DateTime>("TEKIYOU_BEGIN").ToLongDateString();
                    }
                    //適用終了
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("TEKIYOU_END"))
                    {
                        this.form.TekiyouEnd.Text = this.GenbaInfoSearchResult.Rows[0].Field<DateTime>("TEKIYOU_END").ToLongDateString();
                    }
                    //中止理由
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("CHUUSHI_RIYUU1"))
                    {
                        this.form.ChuusiRiyuu1.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("CHUUSHI_RIYUU1");
                    }
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("CHUUSHI_RIYUU2"))
                    {
                        this.form.ChuusiRiyuu2.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("CHUUSHI_RIYUU2");
                    }
                    //諸口区分
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SHOKUCHI_KBN"))
                    {
                        this.form.ShokuchiKbn.Checked = this.GenbaInfoSearchResult.Rows[0].Field<Boolean>("SHOKUCHI_KBN");
                    }
                    //電マニ照会区分
                    //if (!this.GenbaInfoSearchResult.Rows[0].IsNull("DEN_MANI_SHOUKAI_KBN"))
                    //{
                    //    this.form.DenManiShoukaiKbn.Checked = this.GenbaInfoSearchResult.Rows[0].Field<Boolean>("DEN_MANI_SHOUKAI_KBN");
                    //}
                    //検収有
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("KENSHU_YOUHI"))
                    {
                        this.form.KENSHU_YOUHI.Checked = this.GenbaInfoSearchResult.Rows[0].Field<Boolean>("KENSHU_YOUHI");
                    }
                    //自社区分
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("JISHA_KBN"))
                    {
                        this.form.JishaKbn.Checked = this.GenbaInfoSearchResult.Rows[0].Field<Boolean>("JISHA_KBN");
                    }
                    // 最終更新ユーザ
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("UPDATE_USER"))
                    {
                        this.form.GENBA_LastUpdateUser.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("UPDATE_USER").ToString();
                    }
                    // 最終更新日時
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("UPDATE_DATE"))
                    {
                        this.form.GENBA_LastUpdateDate.Text = this.GenbaInfoSearchResult.Rows[0].Field<DateTime>("UPDATE_DATE").ToString();
                    }
                    // 初回登録ユーザ
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("CREATE_USER"))
                    {
                        this.form.GENBA_CreateUser.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("CREATE_USER").ToString();
                    }
                    // 初回登録日時
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("CREATE_DATE"))
                    {
                        this.form.GENBA_CreateDate.Text = this.GenbaInfoSearchResult.Rows[0].Field<DateTime>("CREATE_DATE").ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場の基本情報の表示
        /// </summary>
        internal void GenbaKihonHyouji()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (null != this.GenbaInfoSearchResult && this.GenbaInfoSearchResult.Rows.Count > 0)
                {
                    //郵便番号
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GENBA_POST"))
                    {
                        this.form.GENBA_KIHON_POST_NO.Text = textFormat(3, this.GenbaInfoSearchResult.Rows[0].Field<string>("GENBA_POST"));
                    }
                    //都道府県
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GENBA_TODOUFUKEN_CD"))
                    {
                        this.form.GENBA_KIHON_TODOUFUKEN_CD.Text = this.GenbaInfoSearchResult.Rows[0].Field<Int16>("GENBA_TODOUFUKEN_CD").ToString();
                        this.form.GENBA_KIHON_TODOUFUKEN_NAME.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("TODOUFUKEN_NAME");
                    }
                    //住所
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GENBA_ADDRESS1"))
                    {
                        this.form.GENBA_KIHON_ADDRS1.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("GENBA_ADDRESS1");
                    }
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GENBA_ADDRESS2"))
                    {
                        this.form.GENBA_KIHON_ADDRS2.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("GENBA_ADDRESS2");
                    }
                    //地域
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("CHIIKI_CD"))
                    {
                        this.form.GENBA_KIHON_CHIIKI_CD.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("CHIIKI_CD");
                        this.form.GENBA_KIHON_CHIIKI_NAME.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("CHIIKI_NAME");
                    }
                    // 運搬報告書提出先
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD"))
                    {
                        this.form.GENBA_UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD");
                    }
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME_RYAKU"))
                    {
                        this.form.GENBA_UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME_RYAKU");
                    }
                    //部署
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("BUSHO"))
                    {
                        this.form.GENBA_KIHON_BUSHO_NAME.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("BUSHO");
                    }
                    //担当者
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("TANTOUSHA"))
                    {
                        this.form.GENBA_KIHON_TANTOUSHA_NAME.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("TANTOUSHA");
                    }
                    //交付担当者
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("KOUFU_TANTOUSHA"))
                    {
                        this.form.GENBA_KIHON_KOUFU_TANTOUSHA_NAME.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("KOUFU_TANTOUSHA");
                    }
                    //集計項目
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SHUUKEI_ITEM_CD"))
                    {
                        this.form.GENBA_KIHON_SHUKEIITEM_CD.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SHUUKEI_ITEM_CD");
                        this.form.GENBA_KIHON_SHUKEIITEM_NAME.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SHUUKEI_KOUMOKU_NAME_RYAKU");
                    }
                    //業種
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("GYOUSHU_CD"))
                    {
                        this.form.GENBA_KIHON_GYOUSHU_CD.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("GYOUSHU_CD");
                        this.form.GENBA_KIHON_GYOUSHU_NAME.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("GYOUSHU_NAME_RYAKU");
                    }
                    //備考
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("BIKOU1"))
                    {
                        this.form.GENBA_KIHON_BIKOU1.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("BIKOU1");
                    }
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("BIKOU2"))
                    {
                        this.form.GENBA_KIHON_BIKOU2.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("BIKOU2");
                    }
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("BIKOU3"))
                    {
                        this.form.GENBA_KIHON_BIKOU3.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("BIKOU3");
                    }
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("BIKOU4"))
                    {
                        this.form.GENBA_KIHON_BIKOU4.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("BIKOU4");
                    }
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("UNTENSHA_SHIJI_JIKOU1"))
                    {
                        this.form.GENBA_UNTENSHA_SHIJI_JIKOU1.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("UNTENSHA_SHIJI_JIKOU1");
                    }
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("UNTENSHA_SHIJI_JIKOU2"))
                    {
                        this.form.GENBA_UNTENSHA_SHIJI_JIKOU2.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("UNTENSHA_SHIJI_JIKOU2");
                    }
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("UNTENSHA_SHIJI_JIKOU3"))
                    {
                        this.form.GENBA_UNTENSHA_SHIJI_JIKOU3.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("UNTENSHA_SHIJI_JIKOU3");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場の請求情報の表示
        /// </summary>
        internal void GenbaSeikyuuHyouji()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //請求書送付先
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_NAME1"))
                {
                    this.form.GENBA_SEIKYU_SOUFUSAKI1.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_NAME1");
                }
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_NAME2"))
                {
                    this.form.GENBA_SEIKYU_SOUFUSAKI2.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_NAME2");
                }
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_KEISHOU1"))
                {
                    this.form.GENBA_SEIKYUU_SOUFU_KEISHOU1.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_KEISHOU1");
                }
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_KEISHOU2"))
                {
                    this.form.GENBA_SEIKYUU_SOUFU_KEISHOU2.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_KEISHOU2");
                }
                //請求送付先郵便番号
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_POST"))
                {
                    this.form.GENBA_SEIKYU_SOUFUSAKI_POST_NO.Text = textFormat(3, this.GenbaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_POST"));
                }
                //請求送付先住所
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_ADDRESS1"))
                {
                    this.form.GENBA_SEIKYU_SOUFUSAKI_ADDRS1.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_ADDRESS1");
                }
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_ADDRESS2"))
                {
                    this.form.GENBA_SEIKYU_SOUFUSAKI_ADDRS2.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_ADDRESS2");
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                //発行先コード
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SEIKYUU_HAKKOUSAKI_CD"))
                {
                    this.form.GENBA_HAKKOUSAKI_CD.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_HAKKOUSAKI_CD");
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                //請求送付先部署
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_BUSHO"))
                {
                    this.form.GENBA_SEIKYU_SOUFUSAKI_BUSHO_NAME.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_BUSHO");
                }
                //請求送付先担当者
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_TANTOU"))
                {
                    this.form.GENBA_SEIKYU_SOUFUSAKI_TANTOUSHA_NAME.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_TANTOU");
                }
                //請求送付先TEL
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_TEL"))
                {
                    this.form.GENBA_SEIKYU_SOUFUSAKI_TEL.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_TEL");
                }
                //請求送付先FAX
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SEIKYUU_SOUFU_FAX"))
                {
                    this.form.GENBA_SEIKYU_SOUFUSAKI_FAX.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_SOUFU_FAX");
                }
                //請求担当者
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SEIKYUU_TANTOU"))
                {
                    this.form.GENBA_SEIKYU_TANTOUSHA_NAME.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_TANTOU");
                }
                //代表印字
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SEIKYUU_DAIHYOU_PRINT_KBN"))
                {
                    this.form.SeikyuuDaihyouPrintKbn.Text = this.GenbaInfoSearchResult.Rows[0].Field<Int16>("SEIKYUU_DAIHYOU_PRINT_KBN").ToString();
                }
                //拠点印字
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SEIKYUU_KYOTEN_PRINT_KBN"))
                {
                    this.form.SeikyuuKyotenPrintKbn.Text = this.GenbaInfoSearchResult.Rows[0].Field<Int16>("SEIKYUU_KYOTEN_PRINT_KBN").ToString();
                }
                //拠点名
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SEIKYUU_KYOTEN_CD"))
                {
                    this.form.SeikyuuKyotenCode.Text = this.GenbaInfoSearchResult.Rows[0].Field<Int16>("SEIKYUU_KYOTEN_CD").ToString();
                    this.form.SeikyuuKyotenName.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SEIKYUU_KYOTEN_NAME");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場の支払情報の表示
        /// </summary>
        internal void GenbaShiharaiHyouji()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //支払書送付先
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_NAME1"))
                {
                    this.form.GENBA_SHIHARAI_SOUFUSAKI1.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_NAME1");
                }
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_NAME2"))
                {
                    this.form.GENBA_SHIHARAI_SOUFUSAKI2.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_NAME2");
                }
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_KEISHOU1"))
                {
                    this.form.GENBA_SHIHARAI_SOUFU_KEISHOU1.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_KEISHOU1");
                }
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_KEISHOU2"))
                {
                    this.form.GENBA_SHIHARAI_SOUFU_KEISHOU2.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_KEISHOU2");
                }
                //支払送付先郵便番号
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_POST"))
                {
                    this.form.GENBA_SHIHARAI_SOUFUSAKI_POST_NO.Text = textFormat(3, this.GenbaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_POST"));
                }
                //支払送付先住所
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_ADDRESS1"))
                {
                    this.form.GENBA_SHIHARAI_SOUFUSAKI_ADDRS1.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_ADDRESS1");
                }
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_ADDRESS2"))
                {
                    this.form.GENBA_SHIHARAI_SOUFUSAKI_ADDRS2.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_ADDRESS2");
                }
                //支払送付先部署
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_BUSHO"))
                {
                    this.form.GENBA_SHIHARAI_SOUFUSAKI_BUSHO_NAME.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_BUSHO");
                }
                //支払送付先担当者
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_TANTOU"))
                {
                    this.form.GENBA_SHIHARAI_SOUFUSAKI_TANTOUSHA_NAME.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_TANTOU");
                }
                //支払送付先TEL
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_TEL"))
                {
                    this.form.GENBA_SHIHARAI_SOUFUSAKI_TEL.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_TEL");
                }
                //支払送付先FAX
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SHIHARAI_SOUFU_FAX"))
                {
                    this.form.GENBA_SHIHARAI_SOUFUSAKI_FAX.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_SOUFU_FAX");
                }
                //拠点印字
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SHIHARAI_KYOTEN_PRINT_KBN"))
                {
                    this.form.ShiharaiKyotenPrintKbn.Text = this.GenbaInfoSearchResult.Rows[0].Field<Int16>("SHIHARAI_KYOTEN_PRINT_KBN").ToString();
                }
                //拠点名
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SHIHARAI_KYOTEN_CD"))
                {
                    this.form.ShiharaiKyotenCode.Text = this.GenbaInfoSearchResult.Rows[0].Field<Int16>("SHIHARAI_KYOTEN_CD").ToString();
                    this.form.ShiharaiKyotenName.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SHIHARAI_KYOTEN_NAME");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場の分類情報の表示
        /// </summary>
        internal void GenbaBunruiHyouji()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //排出事業場
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("HAISHUTSU_NIZUMI_GENBA_KBN"))
                {
                    this.form.HaishutsuKbn.Checked = this.GenbaInfoSearchResult.Rows[0].Field<Boolean>("HAISHUTSU_NIZUMI_GENBA_KBN");
                }
                //積み替え保管
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("TSUMIKAEHOKAN_KBN"))
                {
                    this.form.TsumikaeHokanKbn.Checked = this.GenbaInfoSearchResult.Rows[0].Field<Boolean>("TSUMIKAEHOKAN_KBN");
                }
                //処分事業場
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SHOBUN_NIOROSHI_GENBA_KBN"))
                {
                    this.form.ShobunJigyoujouKbn.Checked = this.GenbaInfoSearchResult.Rows[0].Field<Boolean>("SHOBUN_NIOROSHI_GENBA_KBN");
                }
                //最終処分場
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SAISHUU_SHOBUNJOU_KBN"))
                {
                    this.form.SaishuuShobunjouKbn.Checked = this.GenbaInfoSearchResult.Rows[0].Field<Boolean>("SAISHUU_SHOBUNJOU_KBN");
                }
                //マニ返送先
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_KBN"))
                {
                    this.form.ManiHensousakiKbn.Checked = this.GenbaInfoSearchResult.Rows[0].Field<Boolean>("MANI_HENSOUSAKI_KBN");
                }
                //マニ種類
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANIFEST_SHURUI_CD"))
                {
                    this.form.ManifestShuruiCode.Text = this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANIFEST_SHURUI_CD").ToString();
                    this.form.ManifestShuruiName.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANIFEST_SHURUI_NAME");
                }
                //マニ手配
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANIFEST_TEHAI_CD"))
                {
                    this.form.ManifestTehaiCode.Text = this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANIFEST_TEHAI_CD").ToString();
                    this.form.ManifestTehaiName.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANIFEST_TEHAI_NAME");
                }
                //処分先
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("SHOBUNSAKI_NO"))
                {
                    this.form.ShobunsakiCode.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("SHOBUNSAKI_NO");
                }
                //返送先情報
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_THIS_ADDRESS_KBN"))
                {
                    this.form.GENBA_MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = Convert.ToString(this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_THIS_ADDRESS_KBN"));
                }
                if ("2".Equals(this.form.GENBA_MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text))
                {
                    //返送先名
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_NAME1"))
                    {
                        this.form.ManiHensousakiName1.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_NAME1");
                    }
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_KEISHOU1"))
                    {
                        this.form.ManiHensousakiKeishou1.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_KEISHOU1");
                    }
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_NAME2"))
                    {
                        this.form.ManiHensousakiName2.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_NAME2");
                    }
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_KEISHOU2"))
                    {
                        this.form.ManiHensousakiKeishou2.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_KEISHOU2");
                    }
                    //返送先郵便番号
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_POST"))
                    {
                        this.form.ManiHensousakiPost.Text = textFormat(3, this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_POST"));
                    }
                    //返送先住所
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_ADDRESS1"))
                    {
                        this.form.ManiHensousakiAddress1.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_ADDRESS1");
                    }
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_ADDRESS2"))
                    {
                        this.form.ManiHensousakiAddress2.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_ADDRESS2");
                    }
                    //返送先部署
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_BUSHO"))
                    {
                        this.form.ManiHensousakiBusho.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_BUSHO");
                    }
                    //返送先担当
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_TANTOU"))
                    {
                        this.form.ManiHensousakiTantou.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TANTOU");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 各種メソッド

        /// <summary>
        /// フォーマット
        /// </summary>
        /// <param name="flg">1=金額、2=電話番号、3=郵便番号</param>
        /// <param name="text"></param>
        internal string textFormat(int flg, object text)
        {
            LogUtility.DebugMethodStart(flg, text);
            string strFormat = "";

            if (null != text)
            {
                strFormat = text.ToString().Trim();
                decimal num;
                switch (flg)
                {
                    case 1:
                        //金額
                        if (null != strFormat && !"".Equals(strFormat))
                        {
                            num = decimal.Parse(strFormat);
                            strFormat = string.Format("{0:N0}", num);
                        }
                        break;

                    case 2:
                        //電話番号
                        //  電話番号及びFAX番号にハイフンを自動で入れる処理を廃止しました
                        break;

                    case 3:
                        //郵便番号
                        if (null != strFormat && !"".Equals(strFormat))
                        {
                            int index = strFormat.IndexOf('-');
                            if (index <= 0)
                            {
                                strFormat = strFormat = strFormat.Substring(0, 3) + "-" + strFormat.Substring(3);
                            }
                        }
                        break;
                }
            }
            LogUtility.DebugMethodEnd(strFormat);

            return strFormat;
        }

        #region システム情報を取得

        /// <summary>
        ///  システム情報を取得
        /// </summary>
        internal void GetSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.sysInfoEntity = sysInfo[0];
                }
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

        #endregion

        #region DBNull値を指定値に変換

        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">チェック対象</param>
        /// <param name="value">設定値</param>
        /// <returns>object</returns>
        private object ChgDBNullToValue(object obj, object value)
        {
            try
            {
                //LogUtility.DebugMethodStart(obj, value);
                if (obj is DBNull)
                {
                    return value;
                }
                else if (obj.GetType().Namespace.Equals("System.Data.SqlTypes"))
                {
                    INullable objChk = (INullable)obj;
                    if (objChk.IsNull)
                        return value;
                    else
                        return obj;
                }
                else
                {
                    return obj;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        /// <summary>
        /// 定期品名情報を取得
        /// </summary>
        internal void GenbaTeikiHinmei_Select()
        {
            LogUtility.DebugMethodStart();

            //業者CD、現場CD,いずれか空欄の場合は検索せず
            if ((null != this.form.GYOUSHA_CD.Text && !"".Equals(this.form.GYOUSHA_CD.Text) && !DBNull.Value.Equals(this.form.GYOUSHA_CD.Text))
                && (null != this.form.GENBA_CD.Text && !"".Equals(this.form.GENBA_CD.Text) && !DBNull.Value.Equals(this.form.GENBA_CD.Text)))
            {
                // 一旦初期化
                this.form.SHIKUCHOUSON_CD.Text = string.Empty;
                this.form.SHIKUCHOUSON_NAME.Text = string.Empty;

                // 業者現場に紐付く定期品名情報を取得
                var table = this.kokyakuKaruteDao.GetTeikiHinmeiData(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text);
                this.form.Genba_TeikiHinmei_Ichiran.IsBrowsePurpose = false;
                this.form.Genba_TeikiHinmei_Ichiran.DataSource = table;
                this.form.Genba_TeikiHinmei_Ichiran.IsBrowsePurpose = true;

                if (table.Rows.Count != 0)
                {
                    // 市区町村CDをセット(市区町村は基本情報に紐付くものなので常に唯一)
                    this.form.SHIKUCHOUSON_CD.Text = table.Rows[0]["SHIKUCHOUSON_CD"].ToString();
                    this.form.SHIKUCHOUSON_NAME.Text = table.Rows[0]["SHIKUCHOUSON_NAME_RYAKU"].ToString();
                }
            }

            LogUtility.DebugMethodEnd(1);

            return;
        }

        /// <summary>
        /// 月極情報を取得
        /// </summary>
        internal void GenbaTsukiHinmei_Select()
        {
            LogUtility.DebugMethodStart();

            //業者CD、現場CD,いずれか空欄の場合は検索せず
            if ((null != this.form.GYOUSHA_CD.Text && !"".Equals(this.form.GYOUSHA_CD.Text) && !DBNull.Value.Equals(this.form.GYOUSHA_CD.Text))
                && (null != this.form.GENBA_CD.Text && !"".Equals(this.form.GENBA_CD.Text) && !DBNull.Value.Equals(this.form.GENBA_CD.Text)))
            {
                // 業者現場に紐付く月極情報を取得
                var table = this.kokyakuKaruteDao.GetTsukiHinmeiData(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text);
                this.form.Genba_TsukiHinmei_Ichiran.IsBrowsePurpose = false;
                this.form.Genba_TsukiHinmei_Ichiran.DataSource = table;
                this.form.Genba_TsukiHinmei_Ichiran.IsBrowsePurpose = true;
            }

            LogUtility.DebugMethodEnd(1);

            return;
        }

        #endregion

        #region A票～E票の情報を取得
        /// <summary>
        /// A票～E票の情報を取得
        /// </summary>
        internal void SetAToEWindowsData()
        {
            LogUtility.DebugMethodStart();

            #region A票
            if (this._tabPageManager.IsVisible(8))
            {
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_USE_A")
                    && this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_USE_A") == 2)
                {
                    this.form.MANIFEST_USE_AHyo.Text = "2";
                    this.form.MANIFEST_USE_2_AHyo.Checked = true;
                    if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Checked = true;
                    }
                }
                else
                {
                    this.form.MANIFEST_USE_AHyo.Text = "1";
                    this.form.MANIFEST_USE_1_AHyo.Checked = true;
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_PLACE_KBN_A")
                        && this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_PLACE_KBN_A") == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Checked = true;
                        if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_TORIHIKISAKI_CD_A")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_A")))
                        {
                            this.form.HensousakiKbn_AHyo.Text = "1";
                            this.form.HensousakiKbn1_AHyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_AHyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_A");
                            this.form.ManiHensousakiTorihikisakiName_AHyo.Text = GetTorihikisaki(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_A")).TORIHIKISAKI_NAME_RYAKU;
                        }
                        else if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_GENBA_CD_A")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_A")))
                        {
                            this.form.HensousakiKbn_AHyo.Text = "3";
                            this.form.HensousakiKbn3_AHyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_A");
                            this.form.ManiHensousakiGyoushaName_AHyo.Text = GetGyousha(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_A")).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_AHyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_A");
                            this.form.ManiHensousakiGenbaName_AHyo.Text = GetGenba(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_A"), this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_A")).GENBA_NAME_RYAKU;
                        }
                        else if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_GYOUSHA_CD_A")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_A")))
                        {
                            this.form.HensousakiKbn_AHyo.Text = "2";
                            this.form.HensousakiKbn2_AHyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_A");
                            this.form.ManiHensousakiGyoushaName_AHyo.Text = GetGyousha(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_A")).GYOUSHA_NAME_RYAKU;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_AHyo.Text = "1";
                                this.form.HensousakiKbn1_AHyo.Checked = true;
                            }
                            else
                            {
                                this.form.HensousakiKbn_AHyo.Text = string.Empty;
                                this.form.HensousakiKbn1_AHyo.Checked = false;
                                this.form.HensousakiKbn2_AHyo.Checked = false;
                                this.form.HensousakiKbn3_AHyo.Checked = false;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_AHyo.Text = string.Empty;
                        }
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Checked = true;
                        this.form.HensousakiKbn_AHyo.Text = "1";
                        this.form.HensousakiKbn1_AHyo.Checked = true;
                    }
                }
            }
            #endregion

            #region B1票
            if (this._tabPageManager.IsVisible(9))
            {
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_USE_B1")
                    && this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_USE_B1") == 2)
                {
                    this.form.MANIFEST_USE_B1Hyo.Text = "2";
                    this.form.MANIFEST_USE_2_B1Hyo.Checked = true;
                    if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Checked = true;
                    }
                }
                else
                {
                    this.form.MANIFEST_USE_B1Hyo.Text = "1";
                    this.form.MANIFEST_USE_1_B1Hyo.Checked = true;
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_PLACE_KBN_B1")
                        && this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_PLACE_KBN_B1") == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Checked = true;
                        if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1")))
                        {
                            this.form.HensousakiKbn_B1Hyo.Text = "1";
                            this.form.HensousakiKbn1_B1Hyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1");
                            this.form.ManiHensousakiTorihikisakiName_B1Hyo.Text = GetTorihikisaki(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1")).TORIHIKISAKI_NAME_RYAKU;
                        }
                        else if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_GENBA_CD_B1")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_B1")))
                        {
                            this.form.HensousakiKbn_B1Hyo.Text = "3";
                            this.form.HensousakiKbn3_B1Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B1");
                            this.form.ManiHensousakiGyoushaName_B1Hyo.Text = GetGyousha(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B1")).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_B1");
                            this.form.ManiHensousakiGenbaName_B1Hyo.Text = GetGenba(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B1"), this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_B1")).GENBA_NAME_RYAKU;
                        }
                        else if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_GYOUSHA_CD_B1")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B1")))
                        {
                            this.form.HensousakiKbn_B1Hyo.Text = "2";
                            this.form.HensousakiKbn2_B1Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B1");
                            this.form.ManiHensousakiGyoushaName_B1Hyo.Text = GetGyousha(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B1")).GYOUSHA_NAME_RYAKU;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_B1Hyo.Text = "1";
                                this.form.HensousakiKbn1_B1Hyo.Checked = true;
                            }
                            else
                            {
                                this.form.HensousakiKbn_B1Hyo.Text = string.Empty;
                                this.form.HensousakiKbn1_B1Hyo.Checked = false;
                                this.form.HensousakiKbn2_B1Hyo.Checked = false;
                                this.form.HensousakiKbn3_B1Hyo.Checked = false;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_B1Hyo.Text = string.Empty;
                        }
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Checked = true;
                        this.form.HensousakiKbn_B1Hyo.Text = "1";
                        this.form.HensousakiKbn1_B1Hyo.Checked = true;
                    }
                }
            }
            #endregion

            #region B2票
            if (this._tabPageManager.IsVisible(10))
            {
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_USE_B2")
                    && this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_USE_B2") == 2)
                {
                    this.form.MANIFEST_USE_B2Hyo.Text = "2";
                    this.form.MANIFEST_USE_2_B2Hyo.Checked = true;
                    if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Checked = true;
                    }
                }
                else
                {
                    this.form.MANIFEST_USE_B2Hyo.Text = "1";
                    this.form.MANIFEST_USE_1_B2Hyo.Checked = true;
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_PLACE_KBN_B2")
                        && this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_PLACE_KBN_B2") == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Checked = true;
                        if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2")))
                        {
                            this.form.HensousakiKbn_B2Hyo.Text = "1";
                            this.form.HensousakiKbn1_B2Hyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2");
                            this.form.ManiHensousakiTorihikisakiName_B2Hyo.Text = GetTorihikisaki(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2")).TORIHIKISAKI_NAME_RYAKU;
                        }
                        else if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_GENBA_CD_B2")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_B2")))
                        {
                            this.form.HensousakiKbn_B2Hyo.Text = "3";
                            this.form.HensousakiKbn3_B2Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B2");
                            this.form.ManiHensousakiGyoushaName_B2Hyo.Text = GetGyousha(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B2")).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_B2");
                            this.form.ManiHensousakiGenbaName_B2Hyo.Text = GetGenba(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B2"), this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_B2")).GENBA_NAME_RYAKU;
                        }
                        else if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_GYOUSHA_CD_B2")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B2")))
                        {
                            this.form.HensousakiKbn_B2Hyo.Text = "2";
                            this.form.HensousakiKbn2_B2Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B2");
                            this.form.ManiHensousakiGyoushaName_B2Hyo.Text = GetGyousha(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B2")).GYOUSHA_NAME_RYAKU;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_B2Hyo.Text = "1";
                                this.form.HensousakiKbn1_B2Hyo.Checked = true;
                            }
                            else
                            {
                                this.form.HensousakiKbn_B2Hyo.Text = string.Empty;
                                this.form.HensousakiKbn1_B2Hyo.Checked = false;
                                this.form.HensousakiKbn2_B2Hyo.Checked = false;
                                this.form.HensousakiKbn3_B2Hyo.Checked = false;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_B2Hyo.Text = string.Empty;
                        }
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Checked = true;
                        this.form.HensousakiKbn_B2Hyo.Text = "1";
                        this.form.HensousakiKbn1_B2Hyo.Checked = true;
                    }
                }
            }
            #endregion

            #region B4票
            if (this._tabPageManager.IsVisible(11))
            {
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_USE_B4")
                    && this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_USE_B4") == 2)
                {
                    this.form.MANIFEST_USE_B4Hyo.Text = "2";
                    this.form.MANIFEST_USE_2_B4Hyo.Checked = true;
                    if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Checked = true;
                    }
                }
                else
                {
                    this.form.MANIFEST_USE_B4Hyo.Text = "1";
                    this.form.MANIFEST_USE_1_B4Hyo.Checked = true;
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_PLACE_KBN_B4")
                        && this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_PLACE_KBN_B4") == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Checked = true;
                        if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4")))
                        {
                            this.form.HensousakiKbn_B4Hyo.Text = "1";
                            this.form.HensousakiKbn1_B4Hyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4");
                            this.form.ManiHensousakiTorihikisakiName_B4Hyo.Text = GetTorihikisaki(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4")).TORIHIKISAKI_NAME_RYAKU;
                        }
                        else if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_GENBA_CD_B4")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_B4")))
                        {
                            this.form.HensousakiKbn_B4Hyo.Text = "3";
                            this.form.HensousakiKbn3_B4Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B4");
                            this.form.ManiHensousakiGyoushaName_B4Hyo.Text = GetGyousha(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B4")).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_B4");
                            this.form.ManiHensousakiGenbaName_B4Hyo.Text = GetGenba(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B4"), this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_B4")).GENBA_NAME_RYAKU;
                        }
                        else if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_GYOUSHA_CD_B4")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B4")))
                        {
                            this.form.HensousakiKbn_B4Hyo.Text = "2";
                            this.form.HensousakiKbn2_B4Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B4");
                            this.form.ManiHensousakiGyoushaName_B4Hyo.Text = GetGyousha(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B4")).GYOUSHA_NAME_RYAKU;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_B4Hyo.Text = "1";
                                this.form.HensousakiKbn1_B4Hyo.Checked = true;
                            }
                            else
                            {
                                this.form.HensousakiKbn_B4Hyo.Text = string.Empty;
                                this.form.HensousakiKbn1_B4Hyo.Checked = false;
                                this.form.HensousakiKbn2_B4Hyo.Checked = false;
                                this.form.HensousakiKbn3_B4Hyo.Checked = false;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_B4Hyo.Text = string.Empty;
                        }
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Checked = true;
                        this.form.HensousakiKbn_B4Hyo.Text = "1";
                        this.form.HensousakiKbn1_B4Hyo.Checked = true;
                    }
                }
            }
            #endregion

            #region B6票
            if (this._tabPageManager.IsVisible(12))
            {
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_USE_B6")
                    && this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_USE_B6") == 2)
                {
                    this.form.MANIFEST_USE_B6Hyo.Text = "2";
                    this.form.MANIFEST_USE_2_B6Hyo.Checked = true;
                    if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Checked = true;
                    }
                }
                else
                {
                    this.form.MANIFEST_USE_B6Hyo.Text = "1";
                    this.form.MANIFEST_USE_1_B6Hyo.Checked = true;
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_PLACE_KBN_B6")
                        && this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_PLACE_KBN_B6") == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Checked = true;
                        if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6")))
                        {
                            this.form.HensousakiKbn_B6Hyo.Text = "1";
                            this.form.HensousakiKbn1_B6Hyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6");
                            this.form.ManiHensousakiTorihikisakiName_B6Hyo.Text = GetTorihikisaki(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6")).TORIHIKISAKI_NAME_RYAKU;
                        }
                        else if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_GENBA_CD_B6")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_B6")))
                        {
                            this.form.HensousakiKbn_B6Hyo.Text = "3";
                            this.form.HensousakiKbn3_B6Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B6");
                            this.form.ManiHensousakiGyoushaName_B6Hyo.Text = GetGyousha(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B6")).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_B6");
                            this.form.ManiHensousakiGenbaName_B6Hyo.Text = GetGenba(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B6"), this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_B6")).GENBA_NAME_RYAKU;
                        }
                        else if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_GYOUSHA_CD_B6")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B6")))
                        {
                            this.form.HensousakiKbn_B6Hyo.Text = "2";
                            this.form.HensousakiKbn2_B6Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B6");
                            this.form.ManiHensousakiGyoushaName_B6Hyo.Text = GetGyousha(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_B6")).GYOUSHA_NAME_RYAKU;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_B6Hyo.Text = "1";
                                this.form.HensousakiKbn1_B6Hyo.Checked = true;
                            }
                            else
                            {
                                this.form.HensousakiKbn_B6Hyo.Text = string.Empty;
                                this.form.HensousakiKbn1_B6Hyo.Checked = false;
                                this.form.HensousakiKbn2_B6Hyo.Checked = false;
                                this.form.HensousakiKbn3_B6Hyo.Checked = false;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_B6Hyo.Text = string.Empty;
                        }
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Checked = true;
                        this.form.HensousakiKbn_B6Hyo.Text = "1";
                        this.form.HensousakiKbn1_B6Hyo.Checked = true;
                    }
                }
            }
            #endregion

            #region C1票
            if (this._tabPageManager.IsVisible(13))
            {
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_USE_C1")
                    && this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_USE_C1") == 2)
                {
                    this.form.MANIFEST_USE_C1Hyo.Text = "2";
                    this.form.MANIFEST_USE_2_C1Hyo.Checked = true;
                    if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Checked = true;
                    }
                }
                else
                {
                    this.form.MANIFEST_USE_C1Hyo.Text = "1";
                    this.form.MANIFEST_USE_1_C1Hyo.Checked = true;
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_PLACE_KBN_C1")
                        && this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_PLACE_KBN_C1") == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Checked = true;
                        if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1")))
                        {
                            this.form.HensousakiKbn_C1Hyo.Text = "1";
                            this.form.HensousakiKbn1_C1Hyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1");
                            this.form.ManiHensousakiTorihikisakiName_C1Hyo.Text = GetTorihikisaki(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1")).TORIHIKISAKI_NAME_RYAKU;
                        }
                        else if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_GENBA_CD_C1")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_C1")))
                        {
                            this.form.HensousakiKbn_C1Hyo.Text = "3";
                            this.form.HensousakiKbn3_C1Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_C1");
                            this.form.ManiHensousakiGyoushaName_C1Hyo.Text = GetGyousha(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_C1")).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_C1");
                            this.form.ManiHensousakiGenbaName_C1Hyo.Text = GetGenba(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_C1"), this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_C1")).GENBA_NAME_RYAKU;
                        }
                        else if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_GYOUSHA_CD_C1")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_C1")))
                        {
                            this.form.HensousakiKbn_C1Hyo.Text = "2";
                            this.form.HensousakiKbn2_C1Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_C1");
                            this.form.ManiHensousakiGyoushaName_C1Hyo.Text = GetGyousha(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_C1")).GYOUSHA_NAME_RYAKU;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_C1Hyo.Text = "1";
                                this.form.HensousakiKbn1_C1Hyo.Checked = true;
                            }
                            else
                            {
                                this.form.HensousakiKbn_C1Hyo.Text = string.Empty;
                                this.form.HensousakiKbn1_C1Hyo.Checked = false;
                                this.form.HensousakiKbn2_C1Hyo.Checked = false;
                                this.form.HensousakiKbn3_C1Hyo.Checked = false;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_C1Hyo.Text = string.Empty;
                        }
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Checked = true;
                        this.form.HensousakiKbn_C1Hyo.Text = "1";
                        this.form.HensousakiKbn1_C1Hyo.Checked = true;
                    }
                }
            }
            #endregion

            #region C2票
            if (this._tabPageManager.IsVisible(14))
            {
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_USE_C2")
                    && this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_USE_C2") == 2)
                {
                    this.form.MANIFEST_USE_C2Hyo.Text = "2";
                    this.form.MANIFEST_USE_2_C2Hyo.Checked = true;
                    if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Checked = true;
                    }
                }
                else
                {
                    this.form.MANIFEST_USE_C2Hyo.Text = "1";
                    this.form.MANIFEST_USE_1_C2Hyo.Checked = true;
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_PLACE_KBN_C2")
                        && this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_PLACE_KBN_C2") == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Checked = true;
                        if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2")))
                        {
                            this.form.HensousakiKbn_C2Hyo.Text = "1";
                            this.form.HensousakiKbn1_C2Hyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2");
                            this.form.ManiHensousakiTorihikisakiName_C2Hyo.Text = GetTorihikisaki(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2")).TORIHIKISAKI_NAME_RYAKU;
                        }
                        else if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_GENBA_CD_C2")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_C2")))
                        {
                            this.form.HensousakiKbn_C2Hyo.Text = "3";
                            this.form.HensousakiKbn3_C2Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_C2");
                            this.form.ManiHensousakiGyoushaName_C2Hyo.Text = GetGyousha(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_C2")).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_C2");
                            this.form.ManiHensousakiGenbaName_C2Hyo.Text = GetGenba(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_C2"), this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_C2")).GENBA_NAME_RYAKU;
                        }
                        else if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_GYOUSHA_CD_C2")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_C2")))
                        {
                            this.form.HensousakiKbn_C2Hyo.Text = "2";
                            this.form.HensousakiKbn2_C2Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_C2");
                            this.form.ManiHensousakiGyoushaName_C2Hyo.Text = GetGyousha(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_C2")).GYOUSHA_NAME_RYAKU;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_C2Hyo.Text = "1";
                                this.form.HensousakiKbn1_C2Hyo.Checked = true;
                            }
                            else
                            {
                                this.form.HensousakiKbn_C2Hyo.Text = string.Empty;
                                this.form.HensousakiKbn1_C2Hyo.Checked = false;
                                this.form.HensousakiKbn2_C2Hyo.Checked = false;
                                this.form.HensousakiKbn3_C2Hyo.Checked = false;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_C2Hyo.Text = string.Empty;
                        }
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Checked = true;
                        this.form.HensousakiKbn_C2Hyo.Text = "1";
                        this.form.HensousakiKbn1_C2Hyo.Checked = true;
                    }
                }
            }
            #endregion

            #region D票
            if (this._tabPageManager.IsVisible(15))
            {
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_USE_D")
                    && this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_USE_D") == 2)
                {
                    this.form.MANIFEST_USE_DHyo.Text = "2";
                    this.form.MANIFEST_USE_2_DHyo.Checked = true;
                    if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Checked = true;
                    }
                }
                else
                {
                    this.form.MANIFEST_USE_DHyo.Text = "1";
                    this.form.MANIFEST_USE_1_DHyo.Checked = true;
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_PLACE_KBN_D")
                        && this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_PLACE_KBN_D") == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Checked = true;
                        if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_TORIHIKISAKI_CD_D")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_D")))
                        {
                            this.form.HensousakiKbn_DHyo.Text = "1";
                            this.form.HensousakiKbn1_DHyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_DHyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_D");
                            this.form.ManiHensousakiTorihikisakiName_DHyo.Text = GetTorihikisaki(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_D")).TORIHIKISAKI_NAME_RYAKU;
                        }
                        else if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_GENBA_CD_D")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_D")))
                        {
                            this.form.HensousakiKbn_DHyo.Text = "3";
                            this.form.HensousakiKbn3_DHyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_D");
                            this.form.ManiHensousakiGyoushaName_DHyo.Text = GetGyousha(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_D")).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_DHyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_D");
                            this.form.ManiHensousakiGenbaName_DHyo.Text = GetGenba(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_D"), this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_D")).GENBA_NAME_RYAKU;
                        }
                        else if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_GYOUSHA_CD_D")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_D")))
                        {
                            this.form.HensousakiKbn_DHyo.Text = "2";
                            this.form.HensousakiKbn2_DHyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_D");
                            this.form.ManiHensousakiGyoushaName_DHyo.Text = GetGyousha(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_D")).GYOUSHA_NAME_RYAKU;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_DHyo.Text = "1";
                                this.form.HensousakiKbn1_DHyo.Checked = true;
                            }
                            else
                            {
                                this.form.HensousakiKbn_DHyo.Text = string.Empty;
                                this.form.HensousakiKbn1_DHyo.Checked = false;
                                this.form.HensousakiKbn2_DHyo.Checked = false;
                                this.form.HensousakiKbn3_DHyo.Checked = false;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_DHyo.Text = string.Empty;
                        }
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Checked = true;
                        this.form.HensousakiKbn_DHyo.Text = "1";
                        this.form.HensousakiKbn1_DHyo.Checked = true;
                    }
                }
            }
            #endregion

            #region E票
            if (this._tabPageManager.IsVisible(16))
            {
                if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_USE_E")
                    && this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_USE_E") == 2)
                {
                    this.form.MANIFEST_USE_EHyo.Text = "2";
                    this.form.MANIFEST_USE_2_EHyo.Checked = true;
                    if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Checked = true;
                    }
                }
                else
                {
                    this.form.MANIFEST_USE_EHyo.Text = "1";
                    this.form.MANIFEST_USE_1_EHyo.Checked = true;
                    if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_PLACE_KBN_E")
                        && this.GenbaInfoSearchResult.Rows[0].Field<Int16>("MANI_HENSOUSAKI_PLACE_KBN_E") == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Checked = true;
                        if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_TORIHIKISAKI_CD_E")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_E")))
                        {
                            this.form.HensousakiKbn_EHyo.Text = "1";
                            this.form.HensousakiKbn1_EHyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_EHyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_E");
                            this.form.ManiHensousakiTorihikisakiName_EHyo.Text = GetTorihikisaki(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_TORIHIKISAKI_CD_E")).TORIHIKISAKI_NAME_RYAKU;
                        }
                        else if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_GENBA_CD_E")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_E")))
                        {
                            this.form.HensousakiKbn_EHyo.Text = "3";
                            this.form.HensousakiKbn3_EHyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_E");
                            this.form.ManiHensousakiGyoushaName_EHyo.Text = GetGyousha(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_E")).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_EHyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_E");
                            this.form.ManiHensousakiGenbaName_EHyo.Text = GetGenba(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_E"), this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GENBA_CD_E")).GENBA_NAME_RYAKU;
                        }
                        else if (!this.GenbaInfoSearchResult.Rows[0].IsNull("MANI_HENSOUSAKI_GYOUSHA_CD_E")
                            && !string.IsNullOrEmpty(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_E")))
                        {
                            this.form.HensousakiKbn_EHyo.Text = "2";
                            this.form.HensousakiKbn2_EHyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Text = this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_E");
                            this.form.ManiHensousakiGyoushaName_EHyo.Text = GetGyousha(this.GenbaInfoSearchResult.Rows[0].Field<string>("MANI_HENSOUSAKI_GYOUSHA_CD_E")).GYOUSHA_NAME_RYAKU;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_EHyo.Text = "1";
                                this.form.HensousakiKbn1_EHyo.Checked = true;
                            }
                            else
                            {
                                this.form.HensousakiKbn_EHyo.Text = string.Empty;
                                this.form.HensousakiKbn1_EHyo.Checked = false;
                                this.form.HensousakiKbn2_EHyo.Checked = false;
                                this.form.HensousakiKbn3_EHyo.Checked = false;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_EHyo.Text = string.Empty;
                        }
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Checked = true;
                        this.form.HensousakiKbn_EHyo.Text = "1";
                        this.form.HensousakiKbn1_EHyo.Checked = true;
                    }
                }
            }
            #endregion

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region A票～E票タブ表示制御
        /// <summary>
        /// A票～E票タブ表示制御
        /// </summary>
        internal void ChangeTabAtoE()
        {
            LogUtility.DebugMethodStart();

            //A票
            this._tabPageManager.ChangeTabPageVisible(8, (this.sysInfoEntity.MANIFEST_USE_A == 1) ? true : false);

            //B1票
            this._tabPageManager.ChangeTabPageVisible(9, (this.sysInfoEntity.MANIFEST_USE_B1 == 1) ? true : false);

            //B2票
            this._tabPageManager.ChangeTabPageVisible(10, (this.sysInfoEntity.MANIFEST_USE_B2 == 1) ? true : false);

            //B4票
            this._tabPageManager.ChangeTabPageVisible(11, (this.sysInfoEntity.MANIFEST_USE_B4 == 1) ? true : false);

            //B6票
            this._tabPageManager.ChangeTabPageVisible(12, (this.sysInfoEntity.MANIFEST_USE_B6 == 1) ? true : false);

            //C1票
            this._tabPageManager.ChangeTabPageVisible(13, (this.sysInfoEntity.MANIFEST_USE_C1 == 1) ? true : false);

            //C2票
            this._tabPageManager.ChangeTabPageVisible(14, (this.sysInfoEntity.MANIFEST_USE_C2 == 1) ? true : false);

            //D票
            this._tabPageManager.ChangeTabPageVisible(15, (this.sysInfoEntity.MANIFEST_USE_D == 1) ? true : false);

            //E票
            this._tabPageManager.ChangeTabPageVisible(16, (this.sysInfoEntity.MANIFEST_USE_E == 1) ? true : false);

            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public M_TORIHIKISAKI GetTorihikisaki(string cd)
        {
            LogUtility.DebugMethodStart(cd);

            M_TORIHIKISAKI entity = new M_TORIHIKISAKI();
            entity.TORIHIKISAKI_CD = cd;
            entity.ISNOT_NEED_DELETE_FLG = true;

            M_TORIHIKISAKI[] result = this.daoTorisaki.GetAllValidData(entity);
            if (result != null && result.Length > 0)
            {
                entity = result[0];
            }

            LogUtility.DebugMethodEnd(entity);
            return entity;
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public M_GYOUSHA GetGyousha(string cd)
        {
            LogUtility.DebugMethodStart(cd);

            M_GYOUSHA entity = new M_GYOUSHA();
            entity.GYOUSHA_CD = cd;
            entity.ISNOT_NEED_DELETE_FLG = true;

            M_GYOUSHA[] result = this.daoGyousha.GetAllValidData(entity);
            if (result != null && result.Length > 0)
            {
                entity = result[0];
            }

            LogUtility.DebugMethodEnd(entity);
            return entity;
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public M_GENBA GetGenba(string gyoushaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            M_GENBA entity = new M_GENBA();
            entity.GYOUSHA_CD = gyoushaCd;
            entity.GENBA_CD = genbaCd;
            entity.ISNOT_NEED_DELETE_FLG = true;

            M_GENBA[] result = this.daoGenba.GetAllValidData(entity);
            if (result != null && result.Length > 0)
            {
                entity = result[0];
            }

            LogUtility.DebugMethodEnd(entity);
            return entity;
        }
        #endregion

        #region Equals/GetHashCode/ToString

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

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

        /// <summary>
        /// 取引先と拠点の関係をチェックします
        /// </summary>
        /// <param name="headerKyotenCd">拠点CD</param>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns>チェック結果</returns>
        internal bool CheckTorihikisakiKyoten(string headerKyotenCd, string torihikisakiCd)
        {
            //取引先が空だったらReturn
            if (string.Empty == torihikisakiCd)
            {
                return true;
            }

            // 拠点をチェック
            if (String.IsNullOrEmpty(headerKyotenCd))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E146");

                return false;
            }

            var torihikisaki = this.TorihikisakiDao.GetTorihikisakiData(torihikisakiCd);
            if (null == torihikisaki)
            {
                return false;
            }

            var kyotenCd = (int)torihikisaki.TORIHIKISAKI_KYOTEN_CD;
            if (99 != kyotenCd && Convert.ToInt16(headerKyotenCd) != kyotenCd)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 取引先と拠点の関係をチェックします
        /// </summary>
        /// <param name="headerKyotenCd">拠点CD</param>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns>チェック結果</returns>
        internal bool CheckGyosyaKyoten(string headerKyotenCd, string torihikisakiCd, string gyousyaCd)
        {
            //業者が空だったらReturn
            if (string.Empty == gyousyaCd)
            {
                return true;
            }

            // 拠点をチェック
            if (String.IsNullOrEmpty(headerKyotenCd))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E146");

                return false;
            }

            var dt = this.GyoushaInfoDao.GetDataBySqlFileForMoveData(torihikisakiCd, gyousyaCd);
            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }

            foreach (DataRow row in dt.Rows)
            {
                if (!string.IsNullOrEmpty(row["KYOTEN_CD"].ToString()))
                {
                    int kyotenCd = int.Parse(row["KYOTEN_CD"].ToString());
                    if (99 != kyotenCd && Convert.ToInt16(headerKyotenCd) != kyotenCd)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 検索条件をクリアする
        /// </summary>
        internal void searchConfirionsClear()
        {
            // 検索条件をクリアする
            this.form.TORIHIKISAKI_CD.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.form.TORIHIKISAKI_ADDRESS1.Text = string.Empty;
            this.form.TORIHIKISAKI_ADDRESS2.Text = string.Empty;
            this.form.TORIHIKISAKI_TEL.Text = string.Empty;
            this.form.TORIHIKISAKI_TODOUFUKEN.Text = string.Empty;

            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.form.GYOUSHA_ADDRESS1.Text = string.Empty;
            this.form.GYOUSHA_ADDRESS2.Text = string.Empty;
            this.form.GYOUSHA_TEL.Text = string.Empty;
            this.form.GYOUSHA_TODOUFUKEN.Text = string.Empty;

            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_NAME_RYAKU.Text = string.Empty;
            this.form.GENBA_ADDRESS1.Text = string.Empty;
            this.form.GENBA_ADDRESS2.Text = string.Empty;
            this.form.GENBA_TEL.Text = string.Empty;
            this.form.GENBA_TODOUFUKEN.Text = string.Empty;
        }

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            this.header.DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.header.DATE_TO.BackColor = Constans.NOMAL_COLOR;

            DateTime from = Convert.ToDateTime(this.header.DATE_FROM.Text);
            DateTime to = Convert.ToDateTime(this.header.DATE_TO.Text);
            //if from date > to date then return error
            if (from.CompareTo(to) > 0)
            {
                this.header.DATE_FROM.BackColor = Constans.ERROR_COLOR;
                this.header.DATE_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { this.header.label3.Text.Replace("※", "") + "From", this.header.label3.Text.Replace("※", "") + "To" };
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.header.DATE_FROM.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Header init
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            BusinessBaseForm form = (BusinessBaseForm)this.form.Parent;
            this.header = (G173HeaderForm)form.headerForm;

            this.header.DATE_FROM.Value = form.sysDate.AddMonths(-6);
            this.header.DATE_TO.Value = form.sysDate;

            XMLAccessor fileAccess = new XMLAccessor();
            CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();

            this.header.KYOTEN_CD.Text = configProfile.ItemSetVal1.PadLeft(2, '0');

            if (!string.IsNullOrEmpty(this.header.KYOTEN_CD.Text))
            {
                M_KYOTEN data = new M_KYOTEN();
                data = kyotenDao.GetDataByCd(Convert.ToInt16(this.header.KYOTEN_CD.Text).ToString());
                if (data != null)
                {
                    this.header.KYOTEN_NAME_RYAKU.Text = data.KYOTEN_NAME_RYAKU;
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 必須チェックエラーフォーカス処理
        /// </summary>
        /// <returns></returns>
        internal void SetErrorFocus()
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
            foreach (Control control in this.header.allControl)
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

        /// <summary>
        /// DATE_TO MouseDoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.header.DATE_FROM;
            var ToTextBox = this.header.DATE_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }

        // 20160429 koukoukon v2.1_電子請求書 #16612 start
        /// <summary>
        /// 画面起動時に電子請求書で追加するコントロール・項目の表示/非表示を切り替える
        /// </summary>
        private bool setDensiSeikyushoVisible()
        {
            // densiVisible true場合表示false場合非表示
            bool densiVisible = r_framework.Configuration.AppConfig.AppOptions.IsElectronicInvoice();

            if (!densiVisible)
            {
                //取引先
                this.form.labelOutputKbn.Visible = densiVisible;
                this.form.PanelOutputKbn.Visible = densiVisible;
                this.form.labelHakkosaki.Visible = densiVisible;
                this.form.HAKKOUSAKI_CD.Visible = densiVisible;

                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
                this.form.labelInxsSeikyuukbn.Location = new System.Drawing.Point(this.form.labelInxsSeikyuukbn.Location.X, this.form.labelInxsSeikyuukbn.Location.Y - 44);
                this.form.panelInxsSeikyuuKbn.Location = new System.Drawing.Point(this.form.panelInxsSeikyuuKbn.Location.X, this.form.panelInxsSeikyuuKbn.Location.Y - 44);
                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

                this.form.label214.Location = new System.Drawing.Point(this.form.label214.Location.X, this.form.label214.Location.Y - 44);
                this.form.panel11.Location = new System.Drawing.Point(this.form.panel11.Location.X, this.form.panel11.Location.Y - 44);
                this.form.label215.Location = new System.Drawing.Point(this.form.label215.Location.X, this.form.label215.Location.Y - 44);
                this.form.ZEI_KBN_CD.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD.Location.X, this.form.ZEI_KBN_CD.Location.Y - 44);
                this.form.ZEI_KBN_CD_1.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD_1.Location.X, this.form.ZEI_KBN_CD_1.Location.Y - 44);
                this.form.ZEI_KBN_CD_2.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD_2.Location.X, this.form.ZEI_KBN_CD_2.Location.Y - 44);
                this.form.ZEI_KBN_CD_3.Location = new System.Drawing.Point(this.form.ZEI_KBN_CD_3.Location.X, this.form.ZEI_KBN_CD_3.Location.Y - 44);
                //業者
                this.form.labelGyoushaDensiSeikyuuSho.Visible = densiVisible;
                this.form.labelGyoushaHakkousaki.Visible = densiVisible;
                this.form.GYOUSHA_HAKKOUSAKI_CD.Visible = densiVisible;
                //現場
                this.form.labelGenbaDensiSeikyuuSho.Visible = densiVisible;
                this.form.labelGenbaHakkousaki.Visible = densiVisible;
                this.form.GENBA_HAKKOUSAKI_CD.Visible = densiVisible;

            }
            return densiVisible;
        }
        // 20160429 koukoukon v2.1_電子請求書 #16612 end

        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
        /// <summary>
        /// Set visible INXS請求書 control
        /// </summary>
        /// <returns></returns>
        private bool SetInxsSeikyushoVisible()
        {

            // densiVisible true場合表示false場合非表示
            bool inxsSeikyuushoVisible = r_framework.Configuration.AppConfig.AppOptions.IsInxsSeikyuusho();

            if (!inxsSeikyuushoVisible)
            {
                this.form.labelInxsSeikyuukbn.Visible = inxsSeikyuushoVisible;
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
        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
        /// <summary>
        /// Set visible INXS支払明細書 control
        /// </summary>
        /// <returns></returns>
        private bool SetInxsShiharaiMesaishoVisible()
        {

            // densiVisible true場合表示false場合非表示
            bool inxsShiharaishoVisible = r_framework.Configuration.AppConfig.AppOptions.IsInxsShiharai();

            if (!inxsShiharaishoVisible)
            {
                this.form.labelInxsShiharaikbn.Visible = inxsShiharaishoVisible;
                this.form.panelInxsShiharaiKbn.Visible = inxsShiharaishoVisible;

                this.form.label417.Location = new System.Drawing.Point(this.form.label417.Location.X, this.form.label417.Location.Y - 22);
                this.form.panel13.Location = new System.Drawing.Point(this.form.panel13.Location.X, this.form.panel13.Location.Y - 22);
                this.form.label418.Location = new System.Drawing.Point(this.form.label418.Location.X, this.form.label418.Location.Y - 22);
                this.form.panel18.Location = new System.Drawing.Point(this.form.panel18.Location.X, this.form.panel18.Location.Y - 22);
                this.form.label419.Location = new System.Drawing.Point(this.form.label419.Location.X, this.form.label419.Location.Y - 22);
                this.form.LAST_TORIHIKI_DATE_SHIHARAI.Location = new System.Drawing.Point(this.form.LAST_TORIHIKI_DATE_SHIHARAI.Location.X, this.form.LAST_TORIHIKI_DATE_SHIHARAI.Location.Y - 22);
            }
            return inxsShiharaishoVisible;
        }
        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

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
                this.form.label104.Visible = onlineBankVisible;
                this.form.label58.Visible = onlineBankVisible;
            }
            return onlineBankVisible;
        }

        /// <summary>
        /// adjustColumnSize
        /// </summary>
        /// <param name="dgv"></param>
        internal void adjustColumnSize(CustomDataGridView dgv)
        {
            //var dgv = this.form.customDataGridView1;
            if (dgv == null || dgv.ColumnCount == 0)
            {
                return;
            }

            if (dgv.RowCount == 0 || dgv.DataSource == null || ((DataTable)dgv.DataSource).Rows.Count == 0)
            {
                return;
            }
            //dgv.SuspendLayout();
            // TIME_STAMP列はバイナリなのでDataGridViewImageColumnとなり、AutoResizeColumnsメソッドでエラーとなってしまう
            // そのため、列名が"TIME_STAMP"でDataGridViewImageColumn以外をリサイズ対象とする
            // また、入力項目についてはリサイズを行わない(入力項目は初期状態ブランクの場合、幅が小さくなってしまため)
            // ※画面によってはCheckBoxも影響を受けてしまうため、返却日入力用にDgvCustomDataTimeColumnだけリサイズしないようにしている。
            foreach (DataGridViewColumn c in dgv.Columns)
            {
                //if (c.Visible && !(c is DataGridViewImageColumn) && !c.Name.Equals("TIME_STAMP")
                //    && (c.ReadOnly || c.GetType() != typeof(DgvCustomDataTimeColumn)))
                {
                    dgv.AutoResizeColumn(c.Index, DataGridViewAutoSizeColumnMode.DisplayedCells);
                }
            }
            //dgv.ResumeLayout();

            //if (IsFocusDGV)
            //{
            //    // 先頭セルをカレントセルに設定
            //    var firstDisplayColumnIndex = (from DataGridViewColumn c in dgv.Columns where c.Visible orderby c.DisplayIndex select c.Index).First();
            //    dgv.CurrentCell = dgv[firstDisplayColumnIndex, 0];

            //    dgv.Focus();
            //}

            //this.form.AdjustColumnSizeComplete();


        }

        private string[] arrAuth = new string[] {
            "M213", // 取引先マスタ
            "M215", // 業者マスタ
            "M217", // 現場マスタ
            "M212", // 個別品名単価マスタ
            "G015", // 収集受付入力
            "G020", // クレーム受付入力
            "G721", // 受入入力
            "G051", // 受入入力
            "G722", // 出荷入力
            "G053", // 出荷入力
            "G159", // 検収入力
            "G054", // 売上/支払入力
            "G161", // 代納入力
            "G619", // 入金入力(取引先)
            "G459", // 入金入力(入金先)
            "G090", // 出金入力
            "G041", // 設置コンテナ一覧
        };

        internal void GetUserAuthRead()
        {
            this._listAuth = new List<M_MENU_AUTH>();
            M_MENU_AUTH au = new M_MENU_AUTH();
            au.SHAIN_CD = r_framework.Dto.SystemProperty.Shain.CD;
            au.BUSHO_CD = r_framework.Dto.SystemProperty.Shain.BushoCD;
            au.AUTH_READ = true;
            foreach (var item in arrAuth)
            {
                au.FORM_ID = item;
                var auth = this.daoAuth.GetAllValidData(au);
                if (auth.Length > 0) this._listAuth.Add(auth[0]);
            }
        }

        internal bool IsUserFullAuth()
        {
            bool result = false;
            M_MENU_AUTH au = new M_MENU_AUTH();
            au.SHAIN_CD = r_framework.Dto.SystemProperty.Shain.CD;
            au.BUSHO_CD = r_framework.Dto.SystemProperty.Shain.BushoCD;
            var authData = this.daoAuth.GetAllValidData(au);
            if (authData.Length <= 0)
            {
                M_MENU_AUTH auth = new M_MENU_AUTH();
                foreach (var item in arrAuth)
                {
                    auth = new M_MENU_AUTH();
                    auth.SHAIN_CD = r_framework.Dto.SystemProperty.Shain.CD;
                    auth.BUSHO_CD = r_framework.Dto.SystemProperty.Shain.BushoCD;
                    auth.FORM_ID = item;
                    this._listAuth.Add(auth);
                }
                result = true;
            }

            return result;
        }

        internal void SetEnabledAuthRead()
        {
            var indexTabControl1 = this.form.tabControl1.SelectedIndex;//DAT 20220505 #162925
            var indexDenpyou = this.form.TabControl_Denpyou.SelectedIndex;//DAT 20220505 #162925
            try { this.form.tabControl1.TabPages.Clear(); } catch { }
            try { this.form.TabControl_Denpyou.TabPages.Clear(); } catch { }
            try { this.form.tabControl2.TabPages.Clear(); } catch { }

            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.bt_func2.Enabled = false;
            parentForm.bt_func3.Enabled = false;
            parentForm.bt_func4.Enabled = false;
            parentForm.bt_func5.Enabled = false;

            this.AddTabDenpyouAll();
            if (this.form.TabControl_Denpyou.TabPages.Count > 0)
            {
                this.form.tabControl1.TabPages.Insert(0, this.form.TabPage_Denpyou);
                this.form.TabControl_Denpyou.SelectedIndex = indexDenpyou;
            }

            if (this.AddTabDenpyou(this.form.tabControl1, this.form.tabPage_torihikisaki, "M213")) parentForm.bt_func2.Enabled = true;
            if (this.AddTabDenpyou(this.form.tabControl1, this.form.tabPage_gyousha, "M215")) parentForm.bt_func3.Enabled = true;
            if (this.AddTabDenpyou(this.form.tabControl1, this.form.tabPage_genba, "M217")) parentForm.bt_func4.Enabled = true;
            if (this.AddTabDenpyou(this.form.tabControl1, this.form.tabPage_tanka, "M212")) parentForm.bt_func5.Enabled = true;
            if (this.form.tabControl1.TabPages.Count > 0) this.form.tabControl1.SelectedIndex = indexTabControl1;
        }

        private void AddTabDenpyouAll()
        {
            this.AddTabDenpyou(this.form.TabControl_Denpyou, this.form.Denpyou_TabPage_Uketsuke, "G015"); // 収集受付入力
            this.AddTabDenpyou(this.form.TabControl_Denpyou, this.form.Denpyou_TabPage_Uketsuke_Kuremu, "G020"); // 受付（クレーム）
            this.AddTabDenpyou(this.form.TabControl_Denpyou, this.form.Denpyou_TabPage_Ukeire, "G721", "G051"); // 受入入力
            this.AddTabDenpyou(this.form.TabControl_Denpyou, this.form.Denpyou_TabPage_Shukka, "G722", "G053"); // 出荷入力
            this.AddTabDenpyou(this.form.TabControl_Denpyou, this.form.Denpyou_TabPage_ShukkaKenshuu, "G722", "G053"); // 検収入力
            this.AddTabDenpyou(this.form.TabControl_Denpyou, this.form.Denpyou_TabPage_UriageShiharai, "G054"); // 売上/支払入力
            this.AddTabDenpyou(this.form.TabControl_Denpyou, this.form.Denpyou_TabPage_Dainou, "G161"); // 代納入力
            this.AddTabDenpyou(this.form.TabControl_Denpyou, this.form.Denpyou_TabPage_Nyuukin, "G619"); // 入金入力
            this.AddTabDenpyou(this.form.TabControl_Denpyou, this.form.Denpyou_TabPage_Shukkin, "G090"); // 出金入力
            this.AddTabDenpyou(this.form.TabControl_Denpyou, this.form.Denpyou_TabPage_Contena, "G041"); // 設置コンテナ一覧
        }

        internal void AddTabComeBack()
        {
            try { this.form.tabControl1.TabPages.Clear(); } catch { }
            try { this.form.TabControl_Denpyou.TabPages.Clear(); } catch { }
            try { this.form.tabControl2.TabPages.Clear(); } catch { }

            this.AddTabDenpyouAll();
            if (this.form.TabControl_Denpyou.TabPages.Count > 0) this.form.tabControl1.TabPages.Insert(0, this.form.TabPage_Denpyou);
            this.AddTabDenpyou(this.form.tabControl1, this.form.tabPage_torihikisaki, "M213");
            this.AddTabDenpyou(this.form.tabControl1, this.form.tabPage_gyousha, "M215");
            this.AddTabDenpyou(this.form.tabControl1, this.form.tabPage_genba, "M217");
            this.AddTabDenpyou(this.form.tabControl1, this.form.tabPage_tanka, "M212");
        }

        internal bool AddTabDenpyou(TabControl tabControl, TabPage page, string formId)
        {
            if (this._listAuth.Any(x => x.FORM_ID == formId))
            {
                try { tabControl.TabPages.Add(page); } catch { }
                return true;
            }
            else
            {
                this.form.tabControl2.TabPages.Add(page);
            }
            return false;
        }

        internal bool AddTabDenpyou(TabControl tabControl, TabPage page, string formId1, string formId2)
        {
            var sizeType = this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE;
            if (sizeType == 1 && this._listAuth.Any(x => x.FORM_ID == formId1))
            {
                try { tabControl.TabPages.Add(page); } catch { }
                return true;
            }
            else if (sizeType == 2 && this._listAuth.Any(x => x.FORM_ID == formId2))
            {
                try { tabControl.TabPages.Add(page); } catch { }
                return true;
            }
            else
            {
                this.form.tabControl2.TabPages.Add(page);
            }
            return false;
        }

        // 検索結果のカラム名(数量管理)
        private string[] columnNames = { "CONTENA_SHURUI_CD", "CONTENA_SHURUI_NAME_RYAKU", "CONTENA_CD", "CONTENA_NAME_RYAKU", "GYOUSHA_CD", "GYOUSHA_NAME_RYAKU"
                                       , "GENBA_CD", "GENBA_NAME_RYAKU", "EIGYOU_TANTOU_CD", "SHAIN_NAME_RYAKU", "SECCHI_DATE", "DAYSCOUNT", "GRAPH", "DAISUU"};

        // 検索結果のカラム名(個体管理)
        private string[] columnNamesForKotaiknri = { "SecchiChouhuku", "CONTENA_SHURUI_CD", "CONTENA_SHURUI_NAME_RYAKU", "CONTENA_CD", "CONTENA_NAME_RYAKU", "GYOUSHA_CD", "GYOUSHA_NAME_RYAKU"
                                                    , "GENBA_CD", "GENBA_NAME_RYAKU", "EIGYOU_TANTOU_CD", "SHAIN_NAME_RYAKU", "SECCHI_DATE", "DAYSCOUNT", "GRAPH"};

        // 検索結果のタイプ名(検索結果のカラム名に対応)
        private string[] columnTyepes = { "System.String","System.String","System.String","System.String","System.String","System.String"
                                        ,"System.String","System.String","System.String","System.String","System.DateTime","System.Int16","System.Double","System.Int16"};

        private string[] columnTyepesForKotaiKanri = { "System.String","System.String","System.String","System.String","System.String","System.String","System.String"
                                        ,"System.String","System.String","System.String","System.String","System.DateTime","System.Int32","System.Double"};

        private string COLUMN_NAME_CONTENA_SHURUI_CD = "CONTENA_SHURUI_CD";
        private string COLUMN_NAME_CONTENA_SHURUI_NAME_RYAKU = "CONTENA_SHURUI_NAME_RYAKU";
        private string COLUMN_NAME_CONTENA_CD = "CONTENA_CD";
        private string COLUMN_NAME_CONTENA_NAME_RYAKU = "CONTENA_NAME_RYAKU";
        private string COLUMN_NAME_GYOUSHA_CD = "GYOUSHA_CD";
        private string COLUMN_NAME_GYOUSHA_NAME_RYAKU = "GYOUSHA_NAME_RYAKU";
        private string COLUMN_NAME_GENBA_CD = "GENBA_CD";
        private string COLUMN_NAME_GENBA_NAME_RYAKU = "GENBA_NAME_RYAKU";
        private string COLUMN_NAME_EIGYOU_TANTOU_CD = "EIGYOU_TANTOU_CD";
        private string COLUMN_NAME_SHAIN_NAME_RYAKU = "SHAIN_NAME_RYAKU";
        private string COLUMN_NAME_SECCHI_DATE = "SECCHI_DATE";
        private string COLUMN_NAME_DAYSCOUNT = "DAYSCOUNT";
        private string COLUMN_NAME_GRAPH = "GRAPH";
        private string COLUMN_NAME_DAISUU = "DAISUU";
        private string COLUMN_NAME_SECCHICHOUUHUKU = "SecchiChouhuku";

        // 重複設置カラムに表示する文字列
        private string CHOUHUKU_SECCHI_VALUE = "○";

        internal void ChangeLabelAndLayout()
        {
            int kontenaKanriHouhou = this.sysInfoEntity.CONTENA_KANRI_HOUHOU.IsNull ? KokyakuKaruteConstans.CONTENA_KANRI_HOUHOU_SUURYOU : (int)this.sysInfoEntity.CONTENA_KANRI_HOUHOU;
            if (kontenaKanriHouhou == KokyakuKaruteConstans.CONTENA_KANRI_HOUHOU_KOTAI)
            {
                // 個体管理
                this.header.label3.Text = "設置日※";
                // 明細行の調整
                this.form.Contena_Denpyou.Columns[COLUMN_NAME_CONTENA_NAME_RYAKU].Visible = true;
                this.form.Contena_Denpyou.Columns[COLUMN_NAME_DAISUU].Visible = false;
                this.form.Contena_Denpyou.Columns[COLUMN_NAME_SECCHICHOUUHUKU].Visible = true;
                // 初期表示時に表示内容を全てみせたいため、コンテナ種類名を非表示にする
                this.form.Contena_Denpyou.Columns[COLUMN_NAME_CONTENA_SHURUI_NAME_RYAKU].Visible = false;

                this.form.Contena_Denpyou.Columns[COLUMN_NAME_SECCHI_DATE].HeaderText = "設置日";
                this.form.Contena_Denpyou.Columns[COLUMN_NAME_SECCHI_DATE].Width = 80;
                this.form.Contena_Denpyou.Columns[COLUMN_NAME_DAYSCOUNT].HeaderText = "経過日数";
                this.form.Contena_Denpyou.Columns[COLUMN_NAME_DAYSCOUNT].Width = 70;
            }
            else
            {
                this.header.label3.Text = "最終更新日※";
                // 数量管理
                // デザイナのほうは数量管理用のデザインになっているためここでは何もしない
            }
        }

        internal void setLoadPage()
        {
            // グラフ（ｎ日迄）
            this.form.Contena_Denpyou.Columns["graph"].HeaderText = "グラフ（" + this.sysInfoEntity.CONTENA_MAX_SET_KEIKA_DATE + "日まで）";
        }

        public void SetSearchString()
        {
            try
            {
                LogUtility.DebugMethodStart();
                ContenaDTO searchCondition = new ContenaDTO();

                // アラート件数

                // 拠点
                if (!string.IsNullOrEmpty(this.header.KYOTEN_CD.Text))
                {
                    searchCondition.KYOTEN_CD = this.header.KYOTEN_CD.Text;
                }

                // 取引先
                if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    searchCondition.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                }

                // 業者
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    searchCondition.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                }

                // 現場
                if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    searchCondition.GENBA_CD = this.form.GENBA_CD.Text;
                }

                // 設置日
                if (this.header.DATE_FROM.Value != null)
                {
                    searchCondition.SECCHI_DATE_FROM = this.header.DATE_FROM.Value.ToString();
                }

                // 設置日
                if (this.header.DATE_TO.Value != null)
                {
                    searchCondition.SECCHI_DATE_TO = this.header.DATE_TO.Value.ToString();
                }

                // グラフ（ｎ日迄）
                if (!this.sysInfoEntity.CONTENA_MAX_SET_KEIKA_DATE.IsNull)
                {
                    searchCondition.SYS_DAYS_COUNT = this.sysInfoEntity.CONTENA_MAX_SET_KEIKA_DATE;
                }
                else
                {
                    searchCondition.SYS_DAYS_COUNT = 999;
                }

                this.SearchString = searchCondition;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSearchString", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        internal int ContenaSearch()
        {
            LogUtility.DebugMethodStart();

            // 検索条件を設定する
            SetSearchString();

            // 検索結果を取得する
            int kontenaKanriHouhou = this.sysInfoEntity.CONTENA_KANRI_HOUHOU.IsNull ? KokyakuKaruteConstans.CONTENA_KANRI_HOUHOU_SUURYOU : (int)this.sysInfoEntity.CONTENA_KANRI_HOUHOU;
            if (kontenaKanriHouhou == KokyakuKaruteConstans.CONTENA_KANRI_HOUHOU_SUURYOU)
            {
                if (this.ContenaSearchResult != null) this.ContenaSearchResult.Clear();

                DataTable displayData = new DataTable();
                for (int i = 0; i < this.columnNames.Length; i++) displayData.Columns.Add(columnNames[i].ToString(), System.Type.GetType(columnTyepes[i]));

                var queryResult = this.suuryouKanriDao.GetIchiranDataSqlForSuuryouKanri(this.SearchString);
                // 数量計算
                // その現場にいくつコンテナが設定されていて、何日動きが無いかを計算
                var groupList = queryResult.Select(s => new { s.CONTENA_SHURUI_CD, s.GYOUSHA_CD, s.GENBA_CD }).GroupBy(g => new { g.CONTENA_SHURUI_CD, g.GYOUSHA_CD, g.GENBA_CD });

                foreach (var tempGroup in groupList)
                {
                    var tempResultData = queryResult.Where(w => w.CONTENA_SHURUI_CD.Equals(tempGroup.Key.CONTENA_SHURUI_CD) && w.GYOUSHA_CD.Equals(tempGroup.Key.GYOUSHA_CD) && w.GENBA_CD.Equals(tempGroup.Key.GENBA_CD) && w.DAYSCOUNT >= 0).OrderBy(o => o.SECCHI_DATE);
                    short daisuuCntTotal = 0;
                    foreach (var calcTarget in tempResultData)
                    {
                        if (calcTarget.CONTENA_SET_KBN == KokyakuKaruteConstans.CONTENA_SET_KBN_SECCHI)
                        {
                            // 加算
                            daisuuCntTotal += (short)calcTarget.DAISUU_CNT;
                        }
                        else if (calcTarget.CONTENA_SET_KBN == KokyakuKaruteConstans.CONTENA_SET_KBN_HIKIAGE)
                        {
                            // 減算
                            daisuuCntTotal -= calcTarget.DAISUU_CNT;
                        }
                    }

                    // 最後の要素を画面に表示する
                    var lastData = tempResultData.LastOrDefault();
                    // 引揚のほうが多い場合、現場にはコンテナは設定されていないはずなので画面には表示しない
                    if (daisuuCntTotal != 0 && lastData != null
                        && lastData.DAYSCOUNT >= this.SearchString.ELAPSED_DAYS)
                    {
                        DataRow row = displayData.NewRow();
                        row[COLUMN_NAME_CONTENA_SHURUI_CD] = lastData.CONTENA_SHURUI_CD;
                        row[COLUMN_NAME_CONTENA_SHURUI_NAME_RYAKU] = lastData.CONTENA_SHURUI_NAME_RYAKU;
                        row[COLUMN_NAME_CONTENA_CD] = lastData.CONTENA_CD;
                        row[COLUMN_NAME_CONTENA_NAME_RYAKU] = lastData.CONTENA_NAME_RYAKU;
                        row[COLUMN_NAME_GYOUSHA_CD] = lastData.GYOUSHA_CD;
                        row[COLUMN_NAME_GYOUSHA_NAME_RYAKU] = lastData.GYOUSHA_NAME_RYAKU;
                        row[COLUMN_NAME_GENBA_CD] = lastData.GENBA_CD;
                        row[COLUMN_NAME_GENBA_NAME_RYAKU] = lastData.GENBA_NAME_RYAKU;
                        row[COLUMN_NAME_EIGYOU_TANTOU_CD] = lastData.EIGYOU_TANTOU_CD;
                        row[COLUMN_NAME_SHAIN_NAME_RYAKU] = lastData.SHAIN_NAME_RYAKU;
                        row[COLUMN_NAME_SECCHI_DATE] = lastData.SECCHI_DATE;
                        row[COLUMN_NAME_DAYSCOUNT] = lastData.DAYSCOUNT;
                        row[COLUMN_NAME_GRAPH] = lastData.GRAPH;
                        row[COLUMN_NAME_DAISUU] = daisuuCntTotal;
                        displayData.Rows.Add(row);
                    }
                }

                this.ContenaSearchResult = displayData;
            }
            else
            {
                // 個体管理

                // 実績データ(収集受付、受入、売上支払)から取得
                var resulutList = this.daoContena.GetIchiranJissekiDataSql(this.SearchString);
                if (resulutList != null && resulutList.Count > 0)
                {
                    // 引揚がされていないデータを抽出
                    var genbas = resulutList.AsEnumerable().Select(s => new { s.CONTENA_SHURUI_CD, s.CONTENA_CD, s.GYOUSHA_CD, s.GENBA_CD })
                        .GroupBy(g => new { g.CONTENA_SHURUI_CD, g.CONTENA_CD, g.GYOUSHA_CD, g.GENBA_CD });

                    foreach (var genba in genbas)
                    {
                        var rows = resulutList.AsEnumerable()
                            .Where(w => w.CONTENA_SHURUI_CD.Equals(genba.Key.CONTENA_SHURUI_CD)
                                && w.CONTENA_CD.Equals(genba.Key.CONTENA_CD)
                                && w.GYOUSHA_CD.Equals(genba.Key.GYOUSHA_CD)
                                && w.GENBA_CD.Equals(genba.Key.GENBA_CD)
                                && w.CONTENA_SET_KBN == 2).ToArray();

                        foreach (var row in rows)
                        {
                            var secchiRow = resulutList.AsEnumerable().Where(w => w.SECCHI_DATE != null
                                && Convert.ToDateTime(w.SECCHI_DATE) <= (Convert.ToDateTime(row.SECCHI_DATE))
                                && w.CONTENA_SHURUI_CD.Equals(genba.Key.CONTENA_SHURUI_CD)
                                && w.CONTENA_CD.Equals(genba.Key.CONTENA_CD)
                                && w.GYOUSHA_CD.Equals(genba.Key.GYOUSHA_CD.ToString())
                                && w.GENBA_CD.Equals(genba.Key.GENBA_CD)
                                && w.CONTENA_SET_KBN == 1).OrderByDescending(o => o.SECCHI_DATE).ToArray();

                            // 設置 -> 引揚の操作のセットがあった場合はリストから除外
                            if (secchiRow != null && secchiRow.Count() > 0)
                            {
                                // 設置分
                                resulutList.Remove(secchiRow.First());
                            }

                            // 引揚分を除外
                            resulutList.Remove(row);
                        }

                    }
                }

                DataTable displayData = new DataTable();
                for (int i = 0; i < this.columnNamesForKotaiknri.Length; i++) displayData.Columns.Add(columnNamesForKotaiknri[i].ToString(), System.Type.GetType(columnTyepesForKotaiKanri[i]));

                foreach (SearchResultContena data in resulutList)
                {
                    DataRow row = displayData.NewRow();
                    row[COLUMN_NAME_SECCHICHOUUHUKU] = data.SecchiChouhuku;
                    row[COLUMN_NAME_CONTENA_SHURUI_CD] = data.CONTENA_SHURUI_CD;
                    row[COLUMN_NAME_CONTENA_SHURUI_NAME_RYAKU] = data.CONTENA_SHURUI_NAME_RYAKU;
                    row[COLUMN_NAME_CONTENA_CD] = data.CONTENA_CD;
                    row[COLUMN_NAME_CONTENA_NAME_RYAKU] = data.CONTENA_NAME_RYAKU;
                    row[COLUMN_NAME_GYOUSHA_CD] = data.GYOUSHA_CD;
                    row[COLUMN_NAME_GYOUSHA_NAME_RYAKU] = data.GYOUSHA_NAME_RYAKU;
                    row[COLUMN_NAME_GENBA_CD] = data.GENBA_CD;
                    row[COLUMN_NAME_GENBA_NAME_RYAKU] = data.GENBA_NAME_RYAKU;
                    row[COLUMN_NAME_EIGYOU_TANTOU_CD] = data.EIGYOU_TANTOU_CD;
                    row[COLUMN_NAME_SHAIN_NAME_RYAKU] = data.SHAIN_NAME_RYAKU;
                    row[COLUMN_NAME_SECCHI_DATE] = data.SECCHI_DATE;
                    row[COLUMN_NAME_DAYSCOUNT] = data.DAYSCOUNT;
                    row[COLUMN_NAME_GRAPH] = data.GRAPH;
                    displayData.Rows.Add(row);
                }

                this.ContenaSearchResult = displayData;
                this.ContenaSearchResult.DefaultView.RowFilter = string.Format("{0} >= '{1}'", COLUMN_NAME_DAYSCOUNT, this.SearchString.ELAPSED_DAYS);
                // [F11]フィルタ機能でRowFilterが上書かれるため、その対策
                this.ContenaSearchResult = this.ContenaSearchResult.DefaultView.ToTable();

                // 重複するコンテナが存在したら、画面上わかりやすいように重複設置カラムに記号を設定
                // 取得した状態のDataTableは読み取り専用になっているため、ReadOnlyを一度はずす
                foreach (DataColumn col in this.ContenaSearchResult.Columns) col.ReadOnly = false;

                foreach (DataRow row in this.ContenaSearchResult.Rows)
                {
                    var filterRow = this.ContenaSearchResult.AsEnumerable().Where(w => w[COLUMN_NAME_CONTENA_SHURUI_CD].ToString().Equals(row[COLUMN_NAME_CONTENA_SHURUI_CD].ToString()) && w[COLUMN_NAME_CONTENA_CD].ToString().Equals(row[COLUMN_NAME_CONTENA_CD].ToString()));
                    if (filterRow != null && filterRow.Count() > 1)
                    {
                        foreach (DataRow tempRow in filterRow) tempRow[COLUMN_NAME_SECCHICHOUUHUKU] = this.CHOUHUKU_SECCHI_VALUE;
                    }
                }

                // ReadOnlyがTrueの状態だと、画面に表示したときにCell色の制御がおかしくなるため、
                // ReadOnlyをFalseに戻す
                foreach (DataColumn col in this.ContenaSearchResult.Columns) col.ReadOnly = true;
            }

            //検索フラグ
            this.ContenaFlg = true;

            LogUtility.DebugMethodEnd(1);

            return 1;
        }
    }
}