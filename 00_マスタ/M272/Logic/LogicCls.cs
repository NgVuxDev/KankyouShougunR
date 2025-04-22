using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.Master.RiyouRirekiKanri.APP;
using Shougun.Core.Master.RiyouRirekiKanri.Dao;

namespace Shougun.Core.Master.RiyouRirekiKanri.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicCls : IBuisinessLogic, IDisposable
    {
        #region 内部変数

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.RiyouRirekiKanri.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// 現場CDチェック用のDao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// IM_KYOTENDao(拠点Dao)
        /// </summary>
        private IM_KYOTENDao kyotenDao;

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
        //private IT_UketsukeCMDao UketsukeCMDao;
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
        internal IT_UrShDao UrShDao;

        /// <summary>
        /// 売上/支払明細DAO
        /// </summary>
        private IT_UrShDetailDao UrShDetailDao;

        /// <summary>
        /// 請求明細DAO
        /// </summary>
        private IT_SeikyuuDetailDao SeikyuuDetailDao;

        /// <summary>
        /// 入金DAO
        /// </summary>
        internal IT_NyuukinDao NyuukinDao;

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

        //条件空のフラグ
        private bool JyoukenNullFlg = false;

        /// <summary>
        /// ControlUtility
        /// </summary>
        internal ControlUtility controlUtil;

        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
        internal BusinessBaseForm parentForm;

        // 20150922 katen #12048 「システム日付」の基準作成、適用 end

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
        //internal DataTable UketsukeCMSearchResult { get; set; }
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
        /// Dainou data for export
        /// </summary>
        internal DataTable DainouSearchExportResult { get; set; }

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
        /// 入金検索されたかフラグ
        /// </summary>
        internal bool NyuukinFlg { get; set; }

        // <summary>
        /// 出金検索されたかフラグ
        /// </summary>
        internal bool ShukkinFlg { get; set; }

        /// <summary>
        /// 拠点CD
        /// </summary>
        internal string KyotenCD { get; set; }

        /// <summary>
        /// ソート設定情報
        /// </summary>
        private SortSettingInfo sortSettingInfo = null;

        /// <summary>
        /// 紐付くデータグリッドビュー
        /// </summary>
        private CustomDataGridView linkedDataGridView = null;

        public List<CustomSortColumn> SortColumns { get; set; }
        public List<CustomSortColumn> ViewColumns { get; set; }

        internal M_SYS_INFO sysInfoEntity;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            // フォーム取得
            this.form = targetForm;

            // 共通Dao
            this.kyotenDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KYOTENDao>();
            // Utility
            this.controlUtil = new ControlUtility();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <returns></returns>
        internal bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end

                // フォームの初期化処理
                this.FormInit();

                // DTO初期化
                this.DaoInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 0件時の画面設定
                this.SetNoDataDisplay();

                // 入力フィールドの初期化
                this.SetInitValue();

                // 拠点初期値設定
                this.SetInitKyoten();

                // フォーカス設定
                this.form.HeaderForm.Select();
                this.form.HeaderForm.Focus();
                this.form.HeaderForm.HIDUKE_FROM.Select();
                this.form.HeaderForm.HIDUKE_FROM.Focus();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion

        #region フォームの初期化

        /// <summary>
        /// フォーム初期化処理
        /// </summary>
        /// <returns></returns>
        private void FormInit()
        {
            LogUtility.DebugMethodStart();

            // ベースタイトル設定
            var title = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId);

            // ウインドウタイトル設定
            var parentForm = this.form.ParentBaseForm;
            parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(title);

            //プロセスボタンを非表示設定
            //parentForm.ProcessButtonPanel.Visible = false;

            // ヘッダタイトル設定
            var headerForm = this.form.HeaderForm;
            headerForm.lb_title.Text = title;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        /// <returns></returns>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            parentForm.bt_func1.Enabled = true;
            parentForm.bt_func2.Enabled = false;
            parentForm.bt_func3.Enabled = false;
            parentForm.bt_func4.Enabled = false;
            parentForm.bt_func5.Enabled = false;
            parentForm.bt_func6.Enabled = true;
            parentForm.bt_func7.Enabled = true;
            parentForm.bt_func8.Enabled = true;
            parentForm.bt_func9.Enabled = false;
            parentForm.bt_func10.Enabled = true;
            parentForm.bt_func11.Enabled = false;
            parentForm.bt_func12.Enabled = true;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ボタン情報の設定

        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            ButtonSetting[] res = buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);

            LogUtility.DebugMethodStart(res);
            return res;
        }

        #endregion

        #region 拠点初期値設定

        #region 拠点設定

        /// <summary>
        /// 拠点初期値設定
        /// </summary>
        private void SetInitKyoten()
        {
            LogUtility.DebugMethodStart();

            // 拠点
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            this.form.HeaderForm.txtBox_KyotenCd.Text = this.GetUserProfileValue(userProfile, "拠点CD");
            if (!string.IsNullOrEmpty(this.form.HeaderForm.txtBox_KyotenCd.Text.ToString()))
            {
                this.form.HeaderForm.txtBox_KyotenCd.Text = this.form.HeaderForm.txtBox_KyotenCd.Text.ToString().PadLeft(this.form.HeaderForm.txtBox_KyotenCd.MaxLength, '0');
                this.CheckKyotenCd();
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

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

        #endregion

        #region ヘッダーの拠点CDの存在チェック

        /// <summary>
        /// ヘッダーの拠点CDの存在チェック
        /// </summary>
        internal void CheckKyotenCd()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 初期化
                this.form.HeaderForm.txtBox_KyotenNameRyaku.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.HeaderForm.txtBox_KyotenCd.Text))
                {
                    this.form.HeaderForm.txtBox_KyotenNameRyaku.Text = string.Empty;
                    return;
                }

                short kyoteCd = -1;
                if (!short.TryParse(this.form.HeaderForm.txtBox_KyotenCd.Text, out kyoteCd))
                {
                    return;
                }

                M_KYOTEN keyEntity = new M_KYOTEN();
                keyEntity.KYOTEN_CD = kyoteCd;
                var kyotens = this.kyotenDao.GetAllValidData(keyEntity);

                // 存在チェック
                if (kyotens == null || kyotens.Length < 1)
                {
                    //MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    //msgLogic.MessageBoxShow("E020", "拠点");
                    //this.headerForm.KYOTEN_CD.Focus();
                    return;
                }
                else
                {
                    // キーが１つなので複数はヒットしないはず
                    M_KYOTEN kyoten = kyotens[0];
                    this.form.HeaderForm.txtBox_KyotenNameRyaku.Text = kyoten.KYOTEN_NAME_RYAKU.ToString();
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

        #endregion

        #region イベントの初期化

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            // Functionボタンのイベント生成
            var parentForm = this.form.ParentBaseForm;
            parentForm.bt_func1.Click += new System.EventHandler(this.bt_func1_Click);            //参照
            parentForm.bt_func6.Click += new System.EventHandler(this.bt_func6_Click);            //CSV
            parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);            //検索条件クリア
            parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);            //検索
            parentForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);          //並べ替え
            parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);          //閉じる

            // 画面表示イベント
            parentForm.Shown += new EventHandler(UIForm_Shown);

            // 20141201 teikyou ダブルクリックを追加する　start
            this.form.HeaderForm.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDUKE_TO_MouseDoubleClick);
            // 20141201 teikyou ダブルクリックを追加する　end

            /// 20141203 Houkakou 「利用履歴」の日付チェックを追加する　start
            this.form.HeaderForm.HIDUKE_FROM.Leave += new System.EventHandler(HIDUKE_FROM_Leave);
            this.form.HeaderForm.HIDUKE_TO.Leave += new System.EventHandler(HIDUKE_TO_Leave);
            /// 20141203 Houkakou 「利用履歴」の日付チェックを追加する　end

            //受入出荷画面サイズ選択取得
            HearerSysInfoInit();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベント処理の削除を行う
        /// </summary>
        private void EventDelete()
        {
            LogUtility.DebugMethodStart();

            // Functionボタンのイベント解除
            var parentForm = this.form.ParentBaseForm;
            parentForm.bt_func1.Click -= new System.EventHandler(this.bt_func1_Click);            //参照
            parentForm.bt_func6.Click -= new System.EventHandler(this.bt_func6_Click);            //CSV
            parentForm.bt_func7.Click -= new System.EventHandler(this.bt_func7_Click);            //検索条件クリア
            parentForm.bt_func8.Click -= new System.EventHandler(this.bt_func8_Click);            //検索
            parentForm.bt_func10.Click -= new System.EventHandler(this.bt_func10_Click);          //並べ替え
            parentForm.bt_func12.Click -= new System.EventHandler(this.bt_func12_Click);          //閉じる

            // 画面表示イベント解除
            parentForm.Shown -= new EventHandler(UIForm_Shown);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region Daoの初期化

        /// <summary>
        /// Daoの初期化
        /// </summary>
        private void DaoInit()
        {
            LogUtility.DebugMethodStart();

            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            // 受付DAO
            this.UketsukeDao = DaoInitUtility.GetComponent<IT_UketsukeDao>();
            this.UketsukeSSDetailDao = DaoInitUtility.GetComponent<IT_UketsukeSSDetailDao>();
            this.UketsukeSKDetailDao = DaoInitUtility.GetComponent<IT_UketsukeSKDetailDao>();
            this.UketsukeMKDetailDao = DaoInitUtility.GetComponent<IT_UketsukeMKDetailDao>();
            // 計量DAO
            this.KeiryouDao = DaoInitUtility.GetComponent<IT_KeiryouDao>();
            this.KeiryouDetailDao = DaoInitUtility.GetComponent<IT_KeiryouDetailDao>();
            // 受入DAO
            this.UkeireDao = DaoInitUtility.GetComponent<IT_UkeireDao>();
            this.UkeireDetailDao = DaoInitUtility.GetComponent<IT_UkeireDetailDao>();
            // 出荷DAO
            this.ShukkaDao = DaoInitUtility.GetComponent<IT_ShukkaDao>();
            this.ShukkaDetailDao = DaoInitUtility.GetComponent<IT_ShukkaDetailDao>();
            // 売上支払DAO
            this.UrShDao = DaoInitUtility.GetComponent<IT_UrShDao>();
            this.UrShDetailDao = DaoInitUtility.GetComponent<IT_UrShDetailDao>();
            this.SeikyuuDetailDao = DaoInitUtility.GetComponent<IT_SeikyuuDetailDao>();
            // 入金DAO
            this.NyuukinDao = DaoInitUtility.GetComponent<IT_NyuukinDao>();
            this.NyuukinDetailDao = DaoInitUtility.GetComponent<IT_NyuukinDetailDao>();
            // 出金DAO
            this.ShukkinDao = DaoInitUtility.GetComponent<IT_ShukkinDao>();
            this.ShukkinDetailDao = DaoInitUtility.GetComponent<IT_ShukkinDetailDao>();

            LogUtility.DebugMethodEnd();
        }

        #endregion

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

        #region 入力コントロールの初期設定

        /// <summary>
        /// 入力コントロールへ初期値設定
        /// </summary>
        private void SetInitValue()
        {
            LogUtility.DebugMethodStart();

            // ヘッダクラス取得
            var headerForm = this.form.HeaderForm;

            // 拠点
            headerForm.txtBox_KyotenCd.Text = "";
            headerForm.txtBox_KyotenNameRyaku.Text = "";

            // 伝票日付
            // 20150922 katen #12048 「システム日付」の基準作成、適用 start
            //headerForm.HIDUKE_FROM.Value = DateTime.Now;
            //headerForm.HIDUKE_TO.Value = DateTime.Now;
            headerForm.HIDUKE_FROM.Value = this.parentForm.sysDate;
            headerForm.HIDUKE_TO.Value = this.parentForm.sysDate;
            // 20150922 katen #12048 「システム日付」の基準作成、適用 end

            // アラート件数
            M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode("0");
            if (sysInfo != null)
            {
                // システム情報からアラート件数を取得
                headerForm.alertNumber.Text = SetComma(sysInfo.ICHIRAN_ALERT_KENSUU.ToString());
            }

            // 読込データ件数
            headerForm.ReadDataNumber.Text = "0";

            // フォームクラス取得
            var form = this.form;

            // 伝票種類
            form.txtDenpyouKind.Text = "1";

            this.SortColumns = new List<CustomSortColumn>();
            sortSettingInfo = SortSettingHelper.LoadSortSettingInfo("UIForm.customSortHeader1");

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索条件クリア
        /// </summary>
        private void SearchConditionsClear()
        {
            // 検索条件をクリア
            this.form.CONDITION_ITEM1.Text = string.Empty;
            this.form.CONDITION_ITEM2.Text = string.Empty;
            this.form.CONDITION_ITEM3.Text = string.Empty;
            this.form.CONDITION_ITEM4.Text = string.Empty;
            this.form.CONDITION_ITEM5.Text = string.Empty;
            this.form.CONDITION_ITEM6.Text = string.Empty;

            this.form.CONDITION_VALUE1.Text = string.Empty;
            this.form.CONDITION_VALUE2.Text = string.Empty;
            this.form.CONDITION_VALUE3.Text = string.Empty;
            this.form.CONDITION_VALUE4.Text = string.Empty;
            this.form.CONDITION_VALUE5.Text = string.Empty;
            this.form.CONDITION_VALUE6.Text = string.Empty;
        }

        #endregion

        #region [Fキー押下]イベント処理

        /// <summary>
        /// F1 参照
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                switch (this.form.txtDenpyouKind.Text)
                {
                    case "1":
                        if (this.form.Uketsuke_Denpyou.Rows.Count > 0)
                        {
                            this.form.Uketsuke_Denpyou_OpenForm();
                        }
                        break;
                    //case "2":
                    //    if (this.form.Keiryou_Denpyou.Rows.Count > 0)
                    //    {
                    //        this.form.Keiryou_Denpyou_OpenForm();
                    //    }
                    //    break;
                    case "2":
                        if (this.form.Ukeire_Denpyou.Rows.Count > 0)
                        {
                            this.form.Ukeire_Denpyou_OpenForm();
                        }
                        break;

                    case "3":
                        if (this.form.Shukka_Denpyou.Rows.Count > 0)
                        {
                            this.form.Shukka_Denpyou_OpenForm();
                        }
                        break;

                    case "4":
                        if (this.form.UriageShiharai_Denpyou.Rows.Count > 0)
                        {
                            this.form.UriageShiharai_Denpyou_OpenForm();
                        }
                        break;

                    case "5":
                        //代納
                        if (this.form.MultiRow_DaiNouDenpyou.Rows.Count > 0)
                        {
                            this.form.Dainou_Denpyou_OpenForm();
                        }
                        break;

                    case "6":
                        if (this.form.Nyuukin_Denpyou.Rows.Count > 0)
                        {
                            this.form.Nyuukin_Denpyou_OpenForm();
                        }
                        break;

                    case "7":
                        if (this.form.Shukkin_Denpyou.Rows.Count > 0)
                        {
                            this.form.Shukkin_Denpyou_OpenForm();
                        }
                        break;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F6 CSV
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // エラーメッセージクラス
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                CustomDataGridView dgv = null;
                switch (this.form.txtDenpyouKind.Text)
                {
                    case "1":
                        dgv = this.form.Uketsuke_Denpyou;
                        break;
                    //case "2":
                    //    dgv = this.form.Keiryou_Denpyou;
                    //    break;
                    case "2":
                        dgv = this.form.Ukeire_Denpyou;
                        break;

                    case "3":
                        dgv = this.form.Shukka_Denpyou;
                        break;

                    case "4":
                        dgv = this.form.UriageShiharai_Denpyou;
                        break;

                    case "5":
                        //代納
                        dgv = this.form.dgvDainou_Denpyou;
                        break;

                    case "6":
                        dgv = this.form.Nyuukin_Denpyou;
                        break;

                    case "7":
                        dgv = this.form.Shukkin_Denpyou;
                        break;

                    default:
                        return;
                }
                // データが0件の場合
                if (dgv.Rows.Count < 1)
                {
                    msgLogic.MessageBoxShow("E044", "CSV出力");
                    return;
                }

                // CSV出力
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    csvExport.ConvertCustomDataGridViewToCsv(dgv, true, true, WINDOW_TITLEExt.ToTitleString(this.form.WindowId), this.form);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 検索条件クリア
                this.SearchConditionsClear();

                // ゼロ件表示
                this.SetNoDataDisplay();

                // ソート条件のクリア
                this.ClearCustomSortSetting();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                /// 20141203 Houkakou 「利用履歴」の日付チェックを追加する　start
                if (this.DateCheck())
                {
                    return;
                }
                /// 20141203 Houkakou 「利用履歴」の日付チェックを追加する　end

                this.GetTabControlDenpyouData();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現在選択しているタブ(TabControl_Denpyou)のデータを取得
        /// </summary>
        private void GetTabControlDenpyouData()
        {
            //タブIndex
            if (string.IsNullOrWhiteSpace(this.form.txtDenpyouKind.Text))
            {
                return;
            }
            int tabindex = int.Parse(this.form.txtDenpyouKind.Text);

            switch (tabindex)
            {
                case 1:
                    //受付
                    this.Denpyou_TabPage_Uketsuke_Click();
                    break;
                //case 2:
                //    //計量
                //    Denpyou_TabPage_Keiryou_Click();
                //    break;
                case 2:
                    //受入
                    Denpyou_TabPage_Ukeire_Click();
                    break;

                case 3:
                    //出荷
                    Denpyou_TabPage_Shukka_Click();
                    break;

                case 4:
                    //売上/支払
                    Denpyou_TabPage_UriageShiharai_Click();
                    break;

                case 5:
                    //代納
                    Denpyou_TabPage_Dainou_Click();
                    break;

                case 6:
                    //入金
                    Denpyou_TabPage_Nyuukin_Click();
                    break;

                case 7:
                    //出金
                    Denpyou_TabPage_Shukkin_Click();
                    break;
            }
            //並び順項目チェック
            if (string.IsNullOrEmpty(this.form.customSortHeader1.txboxSortSettingInfo.Text))
            {
                this.SortColumns = new List<CustomSortColumn>();
            }
        }

        #region 受付

        /// <summary>
        /// 受付タブー情報を取得
        /// </summary>
        internal void Denpyou_TabPage_Uketsuke_Click()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (!this.JyoukenNullFlg)
                {
                    //受付情報取得
                    if (this.UketsukeDate_Select() > 0)
                    {
                        //受付明細の初期表示
                        //受付区分
                        string uketsukeKbn = this.UketsukeSearchResult.Rows[0].Field<string>("UKETSUKE_KBN");
                        //システムID
                        long systemId = this.UketsukeSearchResult.Rows[0].Field<long>("SYSTEM_ID");
                        //SEQ
                        int seq = this.UketsukeSearchResult.Rows[0].Field<int>("SEQ");
                        //受付明細
                        this.UketsukeDetailDate_Select(uketsukeKbn, systemId, seq);
                    }
                    //並び順項目チェック
                    if (!string.IsNullOrEmpty(this.form.customSortHeader1.txboxSortSettingInfo.Text))
                    {
                        this.SortDataTable(this.UketsukeSearchResult);
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
        /// 受付タブ情報を取得
        /// </summary>
        private int UketsukeDate_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //すでに検索したら、再検索をしない
                //if (!this.logic.UketsukeFlg)
                //{
                //}
                // 検索
                if (this.UketsukeSearch() != 0)
                {
                    // 画面反映
                    if (!this.SetResultData(this.UketsukeSearchResult))
                    {
                        LogUtility.DebugMethodEnd(0);
                        return 0;
                    }
                }
                else
                {
                    // メッセージ表示後 → 画面クリアの順

                    // ゼロ件メッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001", "検索結果");

                    // ゼロ件表示
                    this.SetNoDataDisplay();

                    // 伝票欄の行を初期化
                    this.form.Uketsuke_Denpyou.DataSource = this.UketsukeSearchResult;

                    // 明細欄の行を全て削除
                    this.DetailLineRemove();

                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }

                var table = this.UketsukeSearchResult;
                //バインド
                table.BeginLoadData();
                //行の編集可
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.form.Uketsuke_Denpyou.DataSource = table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
            return 1;
        }

        /// <summary>
        /// 明細欄の行をすべて削除します
        /// </summary>
        internal void DetailLineRemove()
        {
            switch (this.form.txtDenpyouKind.Text)
            {
                // DataGridView
                // 受付
                case "1":
                    for (int index = this.form.Uketsuke_Meisai.Rows.Count; 0 < index; index--)
                    {
                        this.form.Uketsuke_Meisai.Rows.RemoveAt(0);
                    }
                    break;
                // 売上/支払
                case "4":
                    for (int index = this.form.UriageShiharai_Meisai.Rows.Count; 0 < index; index--)
                    {
                        this.form.UriageShiharai_Meisai.Rows.RemoveAt(0);
                    }
                    break;
                //代納
                case "5":
                    for (int index = this.form.MultiRow_DaiNouMeissai.Rows.Count; 0 < index; index--)
                    {
                        this.form.MultiRow_DaiNouMeissai.Rows.RemoveAt(0);
                    }
                    break;
                // 入金
                case "6":
                    for (int index = this.form.Nyuukin_Meisai.Rows.Count; 0 < index; index--)
                    {
                        this.form.Nyuukin_Meisai.Rows.RemoveAt(0);
                    }
                    break;
                // 出金
                case "7":
                    for (int index = this.form.Shukkin_Meisai.Rows.Count; 0 < index; index--)
                    {
                        this.form.Shukkin_Meisai.Rows.RemoveAt(0);
                    }
                    break;

                //  MultiRow
                //// 計量
                //case "2":
                //    for (int index = this.form.MultiRow_KeiryouMeisai.Rows.Count; 0 < index; index--)
                //    {
                //        this.form.MultiRow_KeiryouMeisai.Rows.RemoveAt(0);
                //    }
                //    break;
                // 受入
                case "2":
                    for (int index = this.form.MultiRow_UkeireMeisai.Rows.Count; 0 < index; index--)
                    {
                        this.form.MultiRow_UkeireMeisai.Rows.RemoveAt(0);
                    }
                    break;
                // 出荷
                case "3":
                    for (int index = this.form.MultiRow_ShukkaMeisai.Rows.Count; 0 < index; index--)
                    {
                        this.form.MultiRow_ShukkaMeisai.Rows.RemoveAt(0);
                    }
                    break;
            }
        }

        /// <summary>
        /// 受付検索
        /// </summary>
        internal int UketsukeSearch()
        {
            LogUtility.DebugMethodStart();

            string kyotenCD = this.form.HeaderForm.txtBox_KyotenCd.Text;
            string updateFrom = string.Empty;
            string updateTo = string.Empty;
            if (this.form.HeaderForm.HIDUKE_FROM.Value != null)
            {
                updateFrom = this.form.HeaderForm.HIDUKE_FROM.Value.ToString();
            }
            if (this.form.HeaderForm.HIDUKE_TO.Value != null)
            {
                updateTo = this.form.HeaderForm.HIDUKE_TO.Value.ToString();
            }

            string appendWhere = makeWere(this.form.Uketsuke_Denpyou);
            try
            {
                // 拠点CDが99の場合は全件検索
                if (kyotenCD == "99")
                {
                    kyotenCD = string.Empty;
                }

                //受付検索結果
                this.UketsukeSearchResult = this.UketsukeDao.GetDataBySqlFile(kyotenCD, updateFrom, updateTo, appendWhere);
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

        #region 受付明細情報を取得

        /// <summary>
        /// 受付明細情報を取得
        /// </summary>
        /// <param name="uketsukeKbn">受付区分</param>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        internal int UketsukeDetailDate_Select(string uketsukeKbn, long systemId, int seq)
        {
            LogUtility.DebugMethodStart(uketsukeKbn, systemId, seq);
            try
            {
                DataTable table;

                //DB検索 :受付明細
                switch (uketsukeKbn)
                {
                    case "収集":
                        //受付(収集)明細
                        if (this.UketsukeSSDetailSearch(systemId, seq) == 0)
                        {
                            this.form.Uketsuke_Meisai.DataSource = this.UketsukeSSDetailSearchResult;

                            LogUtility.DebugMethodEnd(0);
                            return 0;
                        }
                        //バインド
                        table = this.UketsukeSSDetailSearchResult;
                        table.BeginLoadData();
                        this.form.Uketsuke_Meisai.DataSource = table;

                        break;

                    case "出荷":
                        //受付(出荷)明細
                        if (this.UketsukeSKDetailSearch(systemId, seq) == 0)
                        {
                            this.form.Uketsuke_Meisai.DataSource = this.UketsukeSKDetailSearchResult;
                            LogUtility.DebugMethodEnd(0);
                            return 0;
                        }
                        //バインド
                        table = this.UketsukeSKDetailSearchResult;
                        table.BeginLoadData();
                        this.form.Uketsuke_Meisai.DataSource = table;

                        break;

                    case "持込":
                        //受付(持込)明細
                        if (this.UketsukeMKDetailSearch(systemId, seq) == 0)
                        {
                            this.form.Uketsuke_Meisai.DataSource = this.UketsukeMKDetailSearchResult;
                            LogUtility.DebugMethodEnd(0);
                            return 0;
                        }
                        //バインド
                        table = this.UketsukeMKDetailSearchResult;
                        table.BeginLoadData();
                        this.form.Uketsuke_Meisai.DataSource = table;

                        break;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("UketsukeDetailDate_Select", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UketsukeDetailDate_Select", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            LogUtility.DebugMethodEnd();

            return 1;
        }

        #endregion

        #region 受付(収集)明細検索

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

        #endregion

        #region 受付(出荷)明細検索

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

        #endregion

        #region 受付(持込)明細検索

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

        #endregion

        #region 計量

        ///// <summary>
        ///// 計量タブー情報を取得
        ///// </summary>
        //internal void Denpyou_TabPage_Keiryou_Click()
        //{
        //    LogUtility.DebugMethodStart();
        //    try
        //    {
        //        if (!this.JyoukenNullFlg)
        //        {
        //            //計量情報取得
        //            if (this.KeiryouDate_Select() > 0)
        //            {
        //                //計量明細の初期表示
        //                //システムID
        //                long systemId = this.KeiryouSearchResult.Rows[0].Field<long>("SYSTEM_ID");
        //                //SEQ
        //                int seq = this.KeiryouSearchResult.Rows[0].Field<int>("SEQ");
        //                //計量明細
        //                this.KeiryouDetailDate_Select(systemId, seq);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    LogUtility.DebugMethodEnd();
        //}

        ///// <summary>
        ///// 計量タブー情報を取得
        ///// </summary>
        //private int KeiryouDate_Select()
        //{
        //    LogUtility.DebugMethodStart();
        //    try
        //    {
        //        //すでに検索したら、再検索をしない
        //        //if (!this.logic.KeiryouFlg)
        //        //{
        //        //}
        //        // 検索
        //        if (this.KeiryouSearch() != 0)
        //        {
        //            // 画面反映
        //            if (!this.SetResultData(this.KeiryouSearchResult))
        //            {
        //                LogUtility.DebugMethodEnd(0);
        //                return 0;
        //            }
        //        }
        //        else
        //        {
        //            // メッセージ表示後 → 画面クリアの順

        //            // ゼロ件メッセージ表示
        //            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
        //            msgLogic.MessageBoxShow("C001", "検索結果");

        //            // ゼロ件表示
        //            this.SetNoDataDisplay();
        //            this.form.Keiryou_Denpyou.DataSource = this.KeiryouSearchResult;

        //            // 明細欄の行を全て削除
        //            this.DetailLineRemove();

        //            LogUtility.DebugMethodEnd(0);
        //            return 0;
        //        }
        //        //バインド
        //        var table = this.KeiryouSearchResult;

        //        table.BeginLoadData();
        //        //行の編集可
        //        for (int i = 0; i < table.Columns.Count; i++)
        //        {
        //            table.Columns[i].ReadOnly = false;
        //        }

        //        this.form.Keiryou_Denpyou.DataSource = table;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    LogUtility.DebugMethodEnd(1);

        //    return 1;
        //}

        ///// <summary>
        ///// 計量明細情報を取得
        ///// </summary>
        ///// <param name="systemId">システムID</param>
        ///// <param name="seq">SEQ</param>
        //internal int KeiryouDetailDate_Select(long systemId, int seq)
        //{
        //    LogUtility.DebugMethodStart(systemId, seq);
        //    try
        //    {
        //        //DB検索 :計量明細
        //        if (this.KeiryouDetailSearch(systemId, seq) == 0)
        //        {
        //            this.form.MultiRow_KeiryouMeisai.DataSource = this.KeiryouDetailSearchResult;
        //            LogUtility.DebugMethodEnd(0);
        //            return 0;
        //        }
        //        //バインド
        //        var table = this.KeiryouDetailSearchResult;

        //        table.BeginLoadData();

        //        this.form.MultiRow_KeiryouMeisai.DataSource = table;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    LogUtility.DebugMethodEnd(1);

        //    return 1;
        //}
        ///// <summary>
        ///// 計量検索
        ///// </summary>
        //internal int KeiryouSearch()
        //{
        //    LogUtility.DebugMethodStart();

        //    string kyotenCD = this.form.HeaderForm.txtBox_KyotenCd.Text;
        //    string updateFrom = string.Empty;
        //    string updateTo = string.Empty;
        //    if (this.form.HeaderForm.HIDUKE_FROM.Value != null)
        //    {
        //        updateFrom = this.form.HeaderForm.HIDUKE_FROM.Value.ToString();
        //    }
        //    if (this.form.HeaderForm.HIDUKE_TO.Value != null)
        //    {
        //        updateTo = this.form.HeaderForm.HIDUKE_TO.Value.ToString();
        //    }

        //    string appendWhere = makeWere(this.form.Keiryou_Denpyou);
        //    try
        //    {
        //        // 拠点CDが99の場合は全件検索
        //        if (kyotenCD == "99")
        //        {
        //            kyotenCD = string.Empty;
        //        }

        //        //計量検索結果
        //        this.KeiryouSearchResult = this.KeiryouDao.GetDataBySqlFile(kyotenCD, updateFrom, updateTo, appendWhere);
        //        //
        //        if (this.KeiryouSearchResult == null || this.KeiryouSearchResult.Rows.Count == 0)
        //        {
        //            LogUtility.DebugMethodEnd(0);
        //            return 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    //検索フラグ
        //    this.KeiryouFlg = true;

        //    LogUtility.DebugMethodEnd(1);

        //    return 1;
        //}
        ///// <summary>
        ///// 計量明細検索
        ///// </summary>
        ///// <param name="systemId">システムID</param>
        ///// <param name="seq">SEQ</param>
        //internal int KeiryouDetailSearch(long systemId, int seq)
        //{
        //    LogUtility.DebugMethodStart(systemId, seq);
        //    try
        //    {
        //        //計量明細検索結果
        //        this.KeiryouDetailSearchResult = this.KeiryouDetailDao.GetDataBySqlFile(systemId, seq);
        //        //
        //        if (this.KeiryouDetailSearchResult == null || this.KeiryouDetailSearchResult.Rows.Count == 0)
        //        {
        //            LogUtility.DebugMethodEnd(0);
        //            return 0;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    LogUtility.DebugMethodEnd(1);
        //    return 1;
        //}

        #endregion

        #region 受入

        /// <summary>
        /// 受入タブー情報を取得
        /// </summary>
        internal void Denpyou_TabPage_Ukeire_Click()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (!this.JyoukenNullFlg)
                {
                    //受入情報取得
                    if (this.UkeireDate_Select() > 0)
                    {
                        //受入明細の初期表示
                        //システムID
                        long systemId = this.UkeireSearchResult.Rows[0].Field<long>("SYSTEM_ID");
                        //SEQ
                        int seq = this.UkeireSearchResult.Rows[0].Field<int>("SEQ");
                        //受入明細
                        this.UkeireDetailDate_Select(systemId, seq);
                    }
                }
                //並び順項目チェック
                if (!string.IsNullOrEmpty(this.form.customSortHeader1.txboxSortSettingInfo.Text))
                {
                    this.SortDataTable(this.UkeireSearchResult);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 受入タブー情報を取得
        /// </summary>
        private int UkeireDate_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //すでに検索したら、再検索をしない
                //if (!this.UkeireFlg)
                //{
                //}
                // 検索
                if (this.UkeireSearch() != 0)
                {
                    // 画面反映
                    if (!this.SetResultData(this.UkeireSearchResult))
                    {
                        LogUtility.DebugMethodEnd(0);
                        return 0;
                    }
                }
                else
                {
                    // メッセージ表示後 → 画面クリアの順

                    // ゼロ件メッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001", "検索結果");

                    // ゼロ件表示
                    this.SetNoDataDisplay();
                    this.form.Ukeire_Denpyou.DataSource = this.UkeireSearchResult;

                    // 明細欄の行を全て削除
                    this.DetailLineRemove();

                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //バインド
                var table = this.UkeireSearchResult;

                table.BeginLoadData();
                //行の編集可
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.form.Ukeire_Denpyou.DataSource = table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 受入明細情報を取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        internal int UkeireDetailDate_Select(long systemId, int seq)
        {
            //LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //DB検索 :受入明細
                if (this.UkeireDetailSearch(systemId, seq) == 0)
                {
                    this.form.MultiRow_UkeireMeisai.DataSource = this.UkeireDetailSearchResult;

                    return 0;
                }
                //バインド
                var table = this.UkeireDetailSearchResult;

                table.BeginLoadData();
                this.form.MultiRow_UkeireMeisai.DataSource = table;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("UkeireDetailDate_Select", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UkeireDetailDate_Select", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            //LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 受入検索
        /// </summary>
        internal int UkeireSearch()
        {
            LogUtility.DebugMethodStart();

            string kyotenCD = this.form.HeaderForm.txtBox_KyotenCd.Text;
            string updateFrom = string.Empty;
            string updateTo = string.Empty;
            if (this.form.HeaderForm.HIDUKE_FROM.Value != null)
            {
                updateFrom = this.form.HeaderForm.HIDUKE_FROM.Value.ToString();
            }
            if (this.form.HeaderForm.HIDUKE_TO.Value != null)
            {
                updateTo = this.form.HeaderForm.HIDUKE_TO.Value.ToString();
            }

            string appendWhere = makeWere(this.form.Ukeire_Denpyou);
            try
            {
                // 拠点CDが99の場合は全件検索
                if (kyotenCD == "99")
                {
                    kyotenCD = string.Empty;
                }

                //受入検索結果
                this.UkeireSearchResult = this.UkeireDao.GetDataBySqlFile(kyotenCD, updateFrom, updateTo, appendWhere);
                //
                if (this.UkeireSearchResult == null || this.UkeireSearchResult.Rows.Count == 0)
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

        #region 出荷

        /// <summary>
        /// 出荷タブー情報を取得
        /// </summary>
        internal void Denpyou_TabPage_Shukka_Click()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (!this.JyoukenNullFlg)
                {
                    //出荷情報取得
                    if (this.ShukkaDate_Select() > 0)
                    {
                        //出荷明細の初期表示
                        //システムID
                        long systemId = this.ShukkaSearchResult.Rows[0].Field<long>("SYSTEM_ID");
                        //SEQ
                        int seq = this.ShukkaSearchResult.Rows[0].Field<int>("SEQ");
                        //出荷明細
                        this.ShukkaDetailDate_Select(systemId, seq);
                    }
                }
                //並び順項目チェック
                if (!string.IsNullOrEmpty(this.form.customSortHeader1.txboxSortSettingInfo.Text))
                {
                    this.SortDataTable(this.ShukkaSearchResult);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出荷タブー情報を取得
        /// </summary>
        private int ShukkaDate_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //すでに検索したら、再検索をしない
                //if (!this.logic.ShukkaFlg)
                //{
                //}
                // 検索
                if (this.ShukkaSearch() != 0)
                {
                    // 画面反映
                    if (!this.SetResultData(this.ShukkaSearchResult))
                    {
                        LogUtility.DebugMethodEnd(0);
                        return 0;
                    }
                }
                else
                {
                    // メッセージ表示後 → 画面クリアの順

                    // ゼロ件メッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001", "検索結果");

                    // ゼロ件表示
                    this.SetNoDataDisplay();
                    this.form.Shukka_Denpyou.DataSource = this.ShukkaSearchResult;

                    // 明細欄の行を全て削除
                    this.DetailLineRemove();

                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //バインド
                var table = this.ShukkaSearchResult;
                //行の編集可
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }
                table.BeginLoadData();
                this.form.Shukka_Denpyou.DataSource = table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 出荷明細情報を取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        internal int ShukkaDetailDate_Select(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //DB検索 :受入明細
                if (this.ShukkaDetailSearch(systemId, seq) == 0)
                {
                    this.form.MultiRow_ShukkaMeisai.DataSource = this.ShukkaDetailSearchResult;
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //バインド
                var table = this.ShukkaDetailSearchResult;

                table.BeginLoadData();

                this.form.MultiRow_ShukkaMeisai.DataSource = table;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ShukkaDetailDate_Select", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShukkaDetailDate_Select", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 出荷検索
        /// </summary>
        internal int ShukkaSearch()
        {
            LogUtility.DebugMethodStart();

            string kyotenCD = this.form.HeaderForm.txtBox_KyotenCd.Text;
            string updateFrom = string.Empty;
            string updateTo = string.Empty;
            if (this.form.HeaderForm.HIDUKE_FROM.Value != null)
            {
                updateFrom = this.form.HeaderForm.HIDUKE_FROM.Value.ToString();
            }
            if (this.form.HeaderForm.HIDUKE_TO.Value != null)
            {
                updateTo = this.form.HeaderForm.HIDUKE_TO.Value.ToString();
            }

            string appendWhere = makeWere(this.form.Shukka_Denpyou);
            try
            {
                // 拠点CDが99の場合は全件検索
                if (kyotenCD == "99")
                {
                    kyotenCD = string.Empty;
                }

                //出荷検索結果
                this.ShukkaSearchResult = this.ShukkaDao.GetDataBySqlFile(kyotenCD, updateFrom, updateTo, appendWhere);
                //
                if (this.ShukkaSearchResult == null || this.ShukkaSearchResult.Rows.Count == 0)
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

        #region 売上/支払

        /// <summary>
        /// 売上/支払タブー情報を取得
        /// </summary>
        internal void Denpyou_TabPage_UriageShiharai_Click()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (!this.JyoukenNullFlg)
                {
                    //売上/支払情報取得
                    if (this.UrShDate_Select() > 0)
                    {
                        //売上/支払明細の初期表示
                        //システムID
                        long systemId = this.UrShSearchResult.Rows[0].Field<long>("SYSTEM_ID");
                        //SEQ
                        int seq = this.UrShSearchResult.Rows[0].Field<int>("SEQ");
                        //売上/支払明細
                        this.UrShDetailDate_Select(systemId, seq);
                    }
                }
                //並び順項目チェック
                if (!string.IsNullOrEmpty(this.form.customSortHeader1.txboxSortSettingInfo.Text))
                {
                    this.SortDataTable(this.UrShSearchResult);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 売上/支払タブー情報を取得
        /// </summary>
        private int UrShDate_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //すでに検索したら、再検索をしない
                //if (!this.UrShFlg)
                //{
                //}
                // 検索
                if (this.UrShSearch() != 0)
                {
                    // 画面反映
                    if (!this.SetResultData(this.UrShSearchResult))
                    {
                        LogUtility.DebugMethodEnd(0);
                        return 0;
                    }
                }
                else
                {
                    // メッセージ表示後 → 画面クリアの順

                    // ゼロ件メッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001", "検索結果");

                    // ゼロ件表示
                    this.SetNoDataDisplay();
                    this.form.UriageShiharai_Denpyou.DataSource = this.UrShSearchResult;

                    // 明細欄の行を全て削除
                    this.DetailLineRemove();

                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //バインド
                var table = this.UrShSearchResult;

                table.BeginLoadData();

                //行の編集可
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.form.UriageShiharai_Denpyou.DataSource = table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 売上/支払明細情報を取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        internal int UrShDetailDate_Select(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //DB検索 :売上/支払明細
                if (this.UrShDetailSearch(systemId, seq) == 0)
                {
                    this.form.UriageShiharai_Meisai.DataSource = this.UrShDetailSearchResult;
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //バインド
                var table = this.UrShDetailSearchResult;

                table.BeginLoadData();

                this.form.UriageShiharai_Meisai.DataSource = table;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("UrShDetailDate_Select", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UrShDetailDate_Select", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 売上/支払検索
        /// </summary>
        internal int UrShSearch()
        {
            LogUtility.DebugMethodStart();

            string kyotenCD = this.form.HeaderForm.txtBox_KyotenCd.Text;
            string updateFrom = string.Empty;
            string updateTo = string.Empty;
            if (this.form.HeaderForm.HIDUKE_FROM.Value != null)
            {
                updateFrom = this.form.HeaderForm.HIDUKE_FROM.Value.ToString();
            }
            if (this.form.HeaderForm.HIDUKE_TO.Value != null)
            {
                updateTo = this.form.HeaderForm.HIDUKE_TO.Value.ToString();
            }

            string appendWhere = makeWere(this.form.UriageShiharai_Denpyou);

            try
            {
                // 拠点CDが99の場合は全件検索
                if (kyotenCD == "99")
                {
                    kyotenCD = string.Empty;
                }

                //売上/支払検索結果
                this.UrShSearchResult = this.UrShDao.GetDataBySqlFile(kyotenCD, updateFrom, updateTo, appendWhere);
                //
                if (this.UrShSearchResult == null || this.UrShSearchResult.Rows.Count == 0)
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
                            //this.UrShDetailSearchResult.Rows[index].SetField("JYOUKYOU", "未締");
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

        #endregion

        #region 代納

        /// <summary>
        /// 代納
        /// </summary>
        internal void Denpyou_TabPage_Dainou_Click()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (!this.JyoukenNullFlg)
                {
                    if (this.DainouDate_Select() > 0)
                    {
                        //支払
                        //システムID
                        long systemIdUkeire = !DBNull.Value.Equals(this.DainouSearchResult.Rows[0]["UKEIRE_SYSTEM_ID"]) ? this.DainouSearchResult.Rows[0].Field<long>("UKEIRE_SYSTEM_ID") : 0;
                        //SEQ
                        int seqUkeire = !DBNull.Value.Equals(this.DainouSearchResult.Rows[0]["UKEIRE_SEQ"]) ? this.DainouSearchResult.Rows[0].Field<int>("UKEIRE_SEQ") : 0;
                        //システムID
                        //売上
                        long systemIdShukka = !DBNull.Value.Equals(this.DainouSearchResult.Rows[0]["SHUKKA_SYSTEM_ID"]) ? this.DainouSearchResult.Rows[0].Field<long>("SHUKKA_SYSTEM_ID") : 0;
                        //SEQ
                        int seqShukka = !DBNull.Value.Equals(this.DainouSearchResult.Rows[0]["SHUKKA_SEQ"]) ? this.DainouSearchResult.Rows[0].Field<int>("SHUKKA_SEQ") : 0;
                        //売上/支払明細
                        this.DainouDetailDate_Select(systemIdUkeire, seqUkeire, systemIdShukka, seqShukka);
                    }
                }
                //並び順項目チェック
                if (!string.IsNullOrEmpty(this.form.customSortHeader1.txboxSortSettingInfo.Text))
                {
                    this.SortDataTable(this.DainouSearchResult);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 代納
        /// </summary>
        private int DainouDate_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //DB検索 :代納
                if (this.DainouSearch() != 0)
                {
                    // 画面反映
                    if (!this.SetResultData(this.DainouSearchResult))
                    {
                        LogUtility.DebugMethodEnd(0);
                        return 0;
                    }
                }
                else
                {
                    // メッセージ表示後 → 画面クリアの順

                    // ゼロ件メッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001", "検索結果");

                    // ゼロ件表示
                    this.SetNoDataDisplay();
                    this.form.MultiRow_DaiNouDenpyou.DataSource = this.DainouSearchResult;

                    // 明細欄の行を全て削除
                    this.DetailLineRemove();

                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }

                //バインド
                var table = this.DainouSearchResult;

                table.BeginLoadData();

                //行の編集可
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.form.MultiRow_DaiNouDenpyou.DataSource = table;
                this.form.dgvDainou_Denpyou_Sort.DataSource = table;

                //Dainou data for export
                var tableExport = this.DainouSearchExportResult;

                tableExport.BeginLoadData();

                //行の編集可
                for (int i = 0; i < tableExport.Columns.Count; i++)
                {
                    tableExport.Columns[i].ReadOnly = false;
                }
                this.form.dgvDainou_Denpyou.DataSource = tableExport;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 代納明細情報を取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        internal int DainouDetailDate_Select(long systemIdUkeire, int seqUkeire, long systemIdShukka, int seqShukka)
        {
            LogUtility.DebugMethodStart(systemIdUkeire, seqUkeire, systemIdShukka, seqShukka);
            try
            {
                //DB検索 :売上/支払明細
                if (this.DainouDetailSearch(systemIdUkeire, seqUkeire, systemIdShukka, seqShukka) == 0)
                {
                    this.form.MultiRow_DaiNouMeissai.DataSource = this.DainouDetailSearchResult;
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //バインド
                var table = this.DainouDetailSearchResult;

                table.BeginLoadData();

                this.form.MultiRow_DaiNouMeissai.DataSource = table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 代納
        /// </summary>
        internal int DainouSearch()
        {
            LogUtility.DebugMethodStart();

            string kyotenCD = this.form.HeaderForm.txtBox_KyotenCd.Text;
            string updateFrom = string.Empty;
            string updateTo = string.Empty;
            if (this.form.HeaderForm.HIDUKE_FROM.Value != null)
            {
                updateFrom = this.form.HeaderForm.HIDUKE_FROM.Value.ToString();
            }
            if (this.form.HeaderForm.HIDUKE_TO.Value != null)
            {
                updateTo = this.form.HeaderForm.HIDUKE_TO.Value.ToString();
            }

            string appendWhere = makeWere(this.form.MultiRow_DaiNouDenpyou);

            string appendWhereForExport = makeWere(this.form.dgvDainou_Denpyou);

            try
            {
                // 拠点CDが99の場合は全件検索
                if (kyotenCD == "99")
                {
                    kyotenCD = string.Empty;
                }

                //代納検索結果
                this.DainouSearchResult = this.UrShDao.GetDainouDataBySqlFile(kyotenCD, updateFrom, updateTo, appendWhere);
                this.DainouSearchExportResult = this.UrShDao.GetDainouDataBySqlFileForExportCSV(kyotenCD, updateFrom, updateTo, appendWhereForExport);
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

        #region 入金

        /// <summary>
        /// 入金タブー情報を取得
        /// </summary>
        internal void Denpyou_TabPage_Nyuukin_Click()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (!this.JyoukenNullFlg)
                {
                    //入金情報取得
                    if (this.NyuukinDate_Select() > 0)
                    {
                        //入金明細の初期表示
                        //システムID
                        long systemId = this.NyuukinSearchResult.Rows[0].Field<long>("SYSTEM_ID");
                        //SEQ
                        int seq = this.NyuukinSearchResult.Rows[0].Field<int>("SEQ");
                        //入金明細
                        this.NyuukinDetailDate_Select(systemId, seq);
                    }
                }
                //並び順項目チェック
                if (!string.IsNullOrEmpty(this.form.customSortHeader1.txboxSortSettingInfo.Text))
                {
                    this.SortDataTable(this.NyuukinSearchResult);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金タブー情報を取得
        /// </summary>
        private int NyuukinDate_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //すでに検索したら、再検索をしない
                //if (!this.logic.NyuukinFlg)
                //{
                //}
                // 検索
                if (this.NyuukinSearch() != 0)
                {
                    // 画面反映
                    if (!this.SetResultData(this.NyuukinSearchResult))
                    {
                        LogUtility.DebugMethodEnd(0);
                        return 0;
                    }
                }
                else
                {
                    // メッセージ表示後 → 画面クリアの順

                    // ゼロ件メッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001", "検索結果");

                    // ゼロ件表示
                    this.SetNoDataDisplay();
                    this.form.Nyuukin_Denpyou.DataSource = this.NyuukinSearchResult;

                    // 明細欄の行を全て削除
                    this.DetailLineRemove();

                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //バインド
                var table = this.NyuukinSearchResult;

                table.BeginLoadData();
                //行の編集可
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.form.Nyuukin_Denpyou.DataSource = table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 入金明細情報を取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        internal int NyuukinDetailDate_Select(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //DB検索 :入金明細
                if (this.NyuukinDetailSearch(systemId, seq) == 0)
                {
                    this.form.Nyuukin_Meisai.DataSource = this.NyuukinDetailSearchResult;
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //バインド
                var table = this.NyuukinDetailSearchResult;

                table.BeginLoadData();

                this.form.Nyuukin_Meisai.DataSource = table;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("NyuukinDetailDate_Select", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("NyuukinDetailDate_Select", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(1);
                return -1;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 入金検索
        /// </summary>
        internal int NyuukinSearch()
        {
            LogUtility.DebugMethodStart();

            string kyotenCD = this.form.HeaderForm.txtBox_KyotenCd.Text;
            string updateFrom = string.Empty;
            string updateTo = string.Empty;
            if (this.form.HeaderForm.HIDUKE_FROM.Value != null)
            {
                updateFrom = this.form.HeaderForm.HIDUKE_FROM.Value.ToString();
            }
            if (this.form.HeaderForm.HIDUKE_TO.Value != null)
            {
                updateTo = this.form.HeaderForm.HIDUKE_TO.Value.ToString();
            }

            string appendWhere = makeWere(this.form.Nyuukin_Denpyou);

            try
            {
                // 拠点CDが99の場合は全件検索
                if (kyotenCD == "99")
                {
                    kyotenCD = string.Empty;
                }

                //入金検索結果
                this.NyuukinSearchResult = this.NyuukinDao.GetDataBySqlFile(kyotenCD, updateFrom, updateTo, appendWhere);
                //
                if (this.NyuukinSearchResult == null || this.NyuukinSearchResult.Rows.Count == 0)
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

        #region 出金

        /// <summary>
        /// 出金タブー情報を取得
        /// </summary>
        internal void Denpyou_TabPage_Shukkin_Click()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (!this.JyoukenNullFlg)
                {
                    //出金情報取得
                    if (this.ShukkinDate_Select() > 0)
                    {
                        //出金明細の初期表示
                        //システムID
                        long systemId = this.ShukkinSearchResult.Rows[0].Field<long>("SYSTEM_ID");
                        //SEQ
                        int seq = this.ShukkinSearchResult.Rows[0].Field<int>("SEQ");
                        //出金明細
                        this.ShukkinDetailDate_Select(systemId, seq);
                    }
                }
                //並び順項目チェック
                if (!string.IsNullOrEmpty(this.form.customSortHeader1.txboxSortSettingInfo.Text))
                {
                    this.SortDataTable(this.ShukkinSearchResult);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出金タブー情報を取得
        /// </summary>
        private int ShukkinDate_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //すでに検索したら、再検索をしない
                //if (!this.logic.ShukkinFlg)
                //{
                //}
                // 検索
                if (this.ShukkinSearch() != 0)
                {
                    // 画面反映
                    if (!this.SetResultData(this.ShukkinSearchResult))
                    {
                        LogUtility.DebugMethodEnd(0);
                        return 0;
                    }
                }
                else
                {
                    // メッセージ表示後 → 画面クリアの順

                    // ゼロ件メッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001", "検索結果");

                    // ゼロ件表示
                    this.SetNoDataDisplay();
                    this.form.Shukkin_Denpyou.DataSource = this.ShukkinSearchResult;

                    // 明細欄の行を全て削除
                    this.DetailLineRemove();

                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //バインド
                var table = this.ShukkinSearchResult;

                table.BeginLoadData();
                //行の編集可
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }
                this.form.Shukkin_Denpyou.DataSource = table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 出金明細情報を取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        internal int ShukkinDetailDate_Select(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //DB検索 :入金明細
                if (this.ShukkinDetailSearch(systemId, seq) == 0)
                {
                    this.form.Shukkin_Meisai.DataSource = this.ShukkinDetailSearchResult;

                    return 0;
                }
                //バインド
                var table = this.ShukkinDetailSearchResult;

                table.BeginLoadData();

                this.form.Shukkin_Meisai.DataSource = table;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ShukkinDetailDate_Select", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShukkinDetailDate_Select", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 出金検索
        /// </summary>
        internal int ShukkinSearch()
        {
            LogUtility.DebugMethodStart();

            string kyotenCD = this.form.HeaderForm.txtBox_KyotenCd.Text;
            string updateFrom = string.Empty;
            string updateTo = string.Empty;
            if (this.form.HeaderForm.HIDUKE_FROM.Value != null)
            {
                updateFrom = this.form.HeaderForm.HIDUKE_FROM.Value.ToString();
            }
            if (this.form.HeaderForm.HIDUKE_TO.Value != null)
            {
                updateTo = this.form.HeaderForm.HIDUKE_TO.Value.ToString();
            }

            string appendWhere = makeWere(this.form.Shukkin_Denpyou);

            try
            {
                // 拠点CDが99の場合は全件検索
                if (kyotenCD == "99")
                {
                    kyotenCD = string.Empty;
                }

                //出金検索結果
                this.ShukkinSearchResult = this.ShukkinDao.GetDataBySqlFile(kyotenCD, updateFrom, updateTo, appendWhere);
                //
                if (this.ShukkinSearchResult == null || this.ShukkinSearchResult.Rows.Count == 0)
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

        #region 共通処理

        private string getDataPropertyName(DataGridView dg, string headerText)
        {
            if (!string.IsNullOrWhiteSpace(headerText))
            {
                foreach (DataGridViewColumn col in dg.Columns)
                {
                    if (headerText.Equals(col.HeaderText))
                    {
                        return col.DataPropertyName;
                    }
                }
            }
            return string.Empty;
        }

        private string getDataPropertyName(GcCustomMultiRow multiRow, string headerText)
        {
            if (!string.IsNullOrWhiteSpace(headerText))
            {
                for (int i = 0; i < multiRow.ColumnHeaders[0].Cells.Count; i++)
                {
                    if (headerText.Equals(multiRow.ColumnHeaders[0].Cells[i].Value))
                    {
                        if (multiRow.ColumnHeaders[0].Cells[i].Tag != null)
                        {
                            return multiRow.ColumnHeaders[0].Cells[i].Tag.ToString();
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 検索条件項目名毎に検索条件を追加する
        /// </summary>
        private void addDictionary(ref Dictionary<string, List<string>> condition, string item, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (!condition.ContainsKey(item))
                {
                    condition.Add(item, new List<string>());
                }
                condition[item].Add(value);
            }
        }

        /// <summary>
        /// 検索条件項目名毎に検索条件を作成する
        /// </summary>
        private string makeWere()
        {
            Dictionary<string, List<string>> condition = new Dictionary<string, List<string>>();
            addDictionary(ref condition, getDataPropertyName(this.form.Shukkin_Denpyou, this.form.CONDITION_ITEM1.Text), this.form.CONDITION_VALUE1.Text);
            addDictionary(ref condition, getDataPropertyName(this.form.Shukkin_Denpyou, this.form.CONDITION_ITEM2.Text), this.form.CONDITION_VALUE2.Text);
            addDictionary(ref condition, getDataPropertyName(this.form.Shukkin_Denpyou, this.form.CONDITION_ITEM3.Text), this.form.CONDITION_VALUE3.Text);
            addDictionary(ref condition, getDataPropertyName(this.form.Shukkin_Denpyou, this.form.CONDITION_ITEM4.Text), this.form.CONDITION_VALUE4.Text);
            addDictionary(ref condition, getDataPropertyName(this.form.Shukkin_Denpyou, this.form.CONDITION_ITEM5.Text), this.form.CONDITION_VALUE5.Text);
            addDictionary(ref condition, getDataPropertyName(this.form.Shukkin_Denpyou, this.form.CONDITION_ITEM6.Text), this.form.CONDITION_VALUE6.Text);

            string appendWhere = string.Empty;
            foreach (var fieldName in condition)
            {
                appendWhere += " AND (";
                //foreach (var value in fieldName.Value)
                for (var idx = 0; idx < fieldName.Value.Count; idx++)
                {
                    appendWhere += "T1." + fieldName.Key + " LIKE '%" + fieldName.Value[idx] + "%'";
                    if (idx < fieldName.Value.Count - 1)
                    {
                        appendWhere += " OR ";
                    }
                }
                appendWhere += ")\n";
            }
            return appendWhere;
        }

        private void addDictionary(ref List<string> condition, string item, string value)
        {
            if (!string.IsNullOrWhiteSpace(item)
            && !string.IsNullOrWhiteSpace(value))
            {
                condition.Add(item + "," + value);
            }
        }

        /// <summary>
        /// 検索条件生成
        /// </summary>
        /// <param name="dg"></param>
        /// <returns></returns>
        private string makeWere(DataGridView dg)
        {
            string appendWhere = string.Empty;
            List<string> condition = new List<string>();
            addDictionary(ref condition, getDataPropertyName(dg, this.form.CONDITION_ITEM1.Text), this.form.CONDITION_VALUE1.Text);
            addDictionary(ref condition, getDataPropertyName(dg, this.form.CONDITION_ITEM2.Text), this.form.CONDITION_VALUE2.Text);
            addDictionary(ref condition, getDataPropertyName(dg, this.form.CONDITION_ITEM3.Text), this.form.CONDITION_VALUE3.Text);
            addDictionary(ref condition, getDataPropertyName(dg, this.form.CONDITION_ITEM4.Text), this.form.CONDITION_VALUE4.Text);
            addDictionary(ref condition, getDataPropertyName(dg, this.form.CONDITION_ITEM5.Text), this.form.CONDITION_VALUE5.Text);
            addDictionary(ref condition, getDataPropertyName(dg, this.form.CONDITION_ITEM6.Text), this.form.CONDITION_VALUE6.Text);

            switch (condition.Count)
            {
                case 0:
                    return appendWhere;
                case 1:
                    string[] tmp = condition[0].Split(',');

                    // 検索文字列を整形
                    tmp[1] = this.SearchStringFix(tmp[1]);

                    appendWhere += " AND " + "(" + tmp[0] + " LIKE '%" + tmp[1] + "%'";
                    break;

                default:
                    // 検索済みの項目リスト
                    List<string> oldCondition = new List<string>();
                    // 一番最初の条件か判断するためのフラグ
                    bool fastQueryFlg = true;
                    // 各項目の先頭か判断するためのフラグ
                    bool beginningOfEachItemFlg = true;

                    // 第一ループ
                    for (var idxa = 0; idxa <= 5; idxa++)
                    {
                        // 検索条件が入力されていない項目は飛ばす
                        if (idxa >= condition.Count)
                        {
                            continue;
                        }

                        // 検索文字列を整形
                        string[] tmpa = condition[idxa].Split(',');

                        // 一度検索し終わった項目は検索しない
                        bool isNotSearch = false;
                        foreach (var SearchPre in oldCondition)
                        {
                            if (SearchPre == tmpa[0])
                            {
                                isNotSearch = true;
                                break;
                            }
                        }

                        if (!isNotSearch)
                        {
                            // 検索カラムを格納
                            oldCondition.Add(tmpa[0]);

                            // 検索文字列を整形
                            tmpa[1] = this.SearchStringFix(tmpa[1]);

                            // 最後の検索条件が認識されない対策
                            if (condition.Count - 1 == idxa)
                            {
                                idxa--;
                            }

                            // 各項目の先頭か判断するためのフラグを初期化
                            beginningOfEachItemFlg = true;

                            // 第二ループ
                            for (var idxb = idxa + 1; idxb <= 5; idxb++)
                            {
                                // 検索条件が入力されていない項目は飛ばす
                                if (idxb >= condition.Count)
                                {
                                    continue;
                                }

                                // 検索文字列を整形
                                string[] tmpb = condition[idxb].Split(',');

                                // 検索文字列を整形
                                tmpb[1] = this.SearchStringFix(tmpb[1]);

                                // 一番最初のみのクエリ
                                if (fastQueryFlg)
                                {
                                    appendWhere += "\n AND " + "(" + tmpa[0] + " LIKE '%" + tmpa[1] + "%'";
                                    fastQueryFlg = false;
                                }

                                // 二つ目以降の条件かつ、各項目の先頭条件だった場合
                                if (oldCondition.Count >= 2 && beginningOfEachItemFlg)
                                {
                                    // 検索済みの項目リストに含まれている場合
                                    if (oldCondition.Exists(x => x.Equals(tmpa[0])) && !string.IsNullOrEmpty(tmpa[0]))
                                    {
                                        appendWhere += ") \n AND (" + tmpa[0] + " LIKE '%" + tmpa[1] + "%'";
                                        beginningOfEachItemFlg = false;
                                    }
                                }

                                // OR条件（条件項目が同じものかつ、最後の検索条件以外）
                                if (tmpa[0].Equals(tmpb[0]) && condition.Count - 1 != idxa)
                                {
                                    appendWhere += "\n OR " + tmpb[0] + " LIKE '%" + tmpb[1] + "%'";
                                }
                            }
                        }
                    }

                    break;
            }

            appendWhere += ")";

            return appendWhere;
        }

        /// <summary>
        /// 検索条件生成
        /// </summary>
        /// <param name="dg"></param>
        /// <returns></returns>
        private string makeWere(GcCustomMultiRow multiRow)
        {
            string appendWhere = string.Empty;
            List<string> condition = new List<string>();
            addDictionary(ref condition, getDataPropertyName(multiRow, this.form.CONDITION_ITEM1.Text), this.form.CONDITION_VALUE1.Text);
            addDictionary(ref condition, getDataPropertyName(multiRow, this.form.CONDITION_ITEM2.Text), this.form.CONDITION_VALUE2.Text);
            addDictionary(ref condition, getDataPropertyName(multiRow, this.form.CONDITION_ITEM3.Text), this.form.CONDITION_VALUE3.Text);
            addDictionary(ref condition, getDataPropertyName(multiRow, this.form.CONDITION_ITEM4.Text), this.form.CONDITION_VALUE4.Text);
            addDictionary(ref condition, getDataPropertyName(multiRow, this.form.CONDITION_ITEM5.Text), this.form.CONDITION_VALUE5.Text);
            addDictionary(ref condition, getDataPropertyName(multiRow, this.form.CONDITION_ITEM6.Text), this.form.CONDITION_VALUE6.Text);

            foreach (var fieldName in condition)
            {
                appendWhere += " AND (";

                string[] tmp = fieldName.Split(',');

                // 検索文字列を整形
                tmp[1] = this.SearchStringFix(tmp[1]);

                string[] col = tmp[0].Split('|');
                for (int i = 0; i < col.Length; i++)
                {
                    if (i == 0)
                    {
                        appendWhere += col[i] + " LIKE '%" + tmp[1] + "%'";
                    }
                    else
                    {
                        appendWhere += " OR " + col[i] + " LIKE '%" + tmp[1] + "%'";
                    }
                }

                appendWhere += ")\n";
            }

            return appendWhere;
        }

        #endregion

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.form.ParentBaseForm.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        #endregion

        #region 初期表示イベント

        /// <summary>
        /// 表示イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            // フォーカス設定
            this.form.HeaderForm.Select();
            this.form.HeaderForm.Focus();
            this.form.HeaderForm.HIDUKE_FROM.Select();
            this.form.HeaderForm.HIDUKE_FROM.Focus();
        }

        #endregion

        #region 検索・検索結果反映

        /// <summary>
        /// 利用履歴管理検索処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int res = 0;
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        /// <summary>
        /// 検索データ反映
        /// </summary>
        private bool SetResultData(DataTable db)
        {
            LogUtility.DebugMethodStart(db);
            bool result = false;

            // 表示判定
            DialogResult dlgRes = DialogResult.Yes;

            // アラート件数判断
            int alertCount = this.form.HeaderForm.AlertCount;
            if (alertCount != 0 && alertCount < db.Rows.Count)
            {
                MessageBoxShowLogic showLogic = new MessageBoxShowLogic();
                dlgRes = showLogic.MessageBoxShow("C025");
            }

            // 表示処理
            if (DialogResult.Yes == dlgRes)
            {
                // 検索結果数設定
                this.form.HeaderForm.ReadDataNumber.Text = db.Rows.Count.ToString();

                result = true;
            }
            LogUtility.DebugMethodEnd(result);
            return result;
        }

        #endregion

        #region データ0件時の表示処理

        /// <summary>
        /// 表示データ0件時／初期起動時の表示処理
        /// </summary>
        private void SetNoDataDisplay()
        {
            LogUtility.DebugMethodStart();

            // 結果レコード数0更新
            this.form.HeaderForm.ReadDataNumber.Text = "0";

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 共通処理

        /// <summary>
        /// カンマ編集
        /// </summary>
        /// <returns></returns>
        private string SetComma(string value)
        {
            LogUtility.DebugMethodStart(value);

            string res = "0";
            if (!string.IsNullOrEmpty(value))
            {
                res = string.Format("{0:#,0}", Convert.ToDecimal(value));
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        /// <summary>
        /// 全角スペースを半角スペースに変換する
        /// </summary>
        /// <param name="param">半角へ変換する文字列</param>
        /// <returns></returns>
        public string ToHankakuSpace(string param)
        {
            LogUtility.DebugMethodStart(param);

            Regex re = new Regex("[　]+");
            string output = re.Replace(param, MyReplacer);

            LogUtility.DebugMethodEnd(output);
            return output;
        }

        /// <summary>
        /// 全角の英数字の文字列を半角に変換する
        /// </summary>
        /// <param name="param">半角へ変換する文字列</param>
        /// <returns></returns>
        public string ToHankaku(string param)
        {
            LogUtility.DebugMethodStart(param);

            Regex re = new Regex("[０-９Ａ-Ｚａ-ｚ　]+");
            string output = re.Replace(param, MyReplacer);

            LogUtility.DebugMethodEnd(output);
            return output;
        }

        /// <summary>
        /// 全角の英数字の文字列を半角に変換する
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private static string MyReplacer(Match m)
        {
            LogUtility.DebugMethodStart(m);

            string res = Strings.StrConv(m.Value, VbStrConv.Narrow, 0);

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// 開放処理
        /// </summary>
        public void Dispose()
        {
            this.EventDelete();
        }

        #endregion

        #region コメントアウト：排出事業場ロストフォーカス処理(実装ストップ）

        ///// <summary>
        ///// 業者CD検証後メソッド
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void numTxtBox_GyousyaCD_Validated(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);
        //    try
        //    {
        //        var msgLogic = new MessageBoxShowLogic();

        //        // (親)業者CD未入力チェック
        //        if (this.form.numTxtBox_GyousyaCD.Text.Equals(""))
        //        {
        //            this.form.numTxtBox_GbCD.Text = string.Empty;
        //            this.form.txtBox_GbName.Text = string.Empty;
        //        }

        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}

        ///// <summary>
        ///// 現場CD検証後メソッド
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void numTxtBox_GbCD_Validated(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);
        //    try
        //    {
        //        var msgLogic = new MessageBoxShowLogic();

        //        // (子)現場CD未入力
        //        if (this.form.numTxtBox_GbCD.Text.Equals(""))
        //        {
        //            //未入力の場合は処理なし
        //            this.form.numTxtBox_GbCD.IsInputErrorOccured = false;
        //            this.form.txtBox_GbName.Text = string.Empty;
        //            return;
        //        }

        //        // (親)業者CD未入力チェック
        //        // 現場CDが設定されている場合であり、業者CDが空文字の場合
        //        if (this.form.numTxtBox_GyousyaCD.Text.Equals(""))
        //        {
        //            this.form.numTxtBox_GbCD.Text = string.Empty;
        //            this.form.txtBox_GbName.Text = string.Empty;
        //            msgLogic.MessageBoxShow("E001", "業者");
        //            this.form.numTxtBox_GyousyaCD.Focus();
        //            return;
        //        }

        //        //現場マスタ検索
        //        M_GENBA mGenba = new M_GENBA();
        //        mGenba.GENBA_CD = this.form.numTxtBox_GbCD.Text;
        //        mGenba.GYOUSHA_CD = this.form.numTxtBox_GyousyaCD.Text;
        //        mGenba = genbaDao.GetDataByCd(mGenba);

        //        if (mGenba != null)
        //        {
        //            this.form.txtBox_GbName.Text = mGenba.GENBA_NAME_RYAKU;
        //        }
        //        else
        //        {
        //            this.form.txtBox_GbName.Text = string.Empty;
        //            this.form.numTxtBox_GbCD.Text = string.Empty;
        //            msgLogic.MessageBoxShow("E020", "現場");
        //            this.form.numTxtBox_GbCD.Focus();
        //            this.form.numTxtBox_GbCD.IsInputErrorOccured = true;
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }

        //}

        #endregion

        #region インターフェース処理(未実装)

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

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
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

        /// <summary>
        /// 検索文字列の整形
        /// </summary>
        /// <param name="tmp">検索文字列</param>
        /// <returns></returns>
        private string SearchStringFix(string tmp)
        {
            // DateTime
            DateTime datetime;
            bool testTime = false;
            testTime = DateTime.TryParse(tmp, out datetime);
            if (testTime)
            {
                tmp = tmp.Replace('-', '/');
            }
            return tmp;
        }

        ///// <summary>
        ///// システム設定に基づいて計量伝票のデータグリッドビューカラムを制御します
        ///// </summary>
        //internal void RenbanSwitchingKeiryou()
        //{
        //    var DGV = this.form.Keiryou_Denpyou;
        //    // システム設定取得
        //    M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode("0");
        //    if (sysInfo != null)
        //    {
        //        // システム情報から年連番区分を取得
        //        var renbanKbn = sysInfo.SYS_RENBAN_HOUHOU_KBN.ToString();

        //        // 連番区分＝１：日連番の場合
        //        if (renbanKbn == "1")
        //        {
        //            DGV.Columns["KEIRYOU_DENPYOU_HIRENBAN"].Visible = true;
        //            DGV.Columns["KEIRYOU_DENPYOU_NENRENBAN"].Visible = false;
        //        }
        //        // 連番区分＝２：年連番の場合
        //        else if (renbanKbn == "2")
        //        {
        //            DGV.Columns["KEIRYOU_DENPYOU_HIRENBAN"].Visible = false;
        //            DGV.Columns["KEIRYOU_DENPYOU_NENRENBAN"].Visible = true;
        //        }
        //    }
        //    return;
        //}

        /// <summary>
        /// 明細の制御(システム設定情報用)
        /// </summary>
        /// <param name="headerNames">非表示にするカラム名一覧</param>
        /// <param name="cellNames">非表示にするセル名一覧</param>
        /// <param name="visibleFlag">各カラム、セルのVisibleに設定するbool</param>
        internal void ChangePropertyForGC(GcCustomMultiRow MultiRow)
        {
            // システム設定取得
            M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode("0");
            if (sysInfo != null)
            {
                // システム情報からマニ区分を取得
                var maniKbn = sysInfo.SYS_MANI_KEITAI_KBN.ToString();

                MultiRow.SuspendLayout();
                var newTemplate = MultiRow.Template;

                // マニ登録携帯 = 1：伝票1に対してマニ1
                //                   伝票1に対してマニNの場合
                if (maniKbn == "1")
                {
                    if (newTemplate.ColumnHeaders["columnHeaderSection1"]["gcCustomColumnHeader14"].Visible)
                    {
                        newTemplate.ColumnHeaders["columnHeaderSection1"].Width -=
                            newTemplate.ColumnHeaders["columnHeaderSection1"]["gcCustomColumnHeader14"].Width;
                    }
                    newTemplate.ColumnHeaders["columnHeaderSection1"]["gcCustomColumnHeader14"].Visible = false;
                    newTemplate.Row.Cells["MEISAI_MANIFEST_ID"].Visible = false;
                }
                // マニ登録携帯 = 2：明細1に対してマニ1の場合
                else if (maniKbn == "2")
                {
                    if (!newTemplate.ColumnHeaders["columnHeaderSection1"]["gcCustomColumnHeader14"].Visible)
                    {
                        newTemplate.ColumnHeaders["columnHeaderSection1"].Width +=
                            newTemplate.ColumnHeaders["columnHeaderSection1"]["gcCustomColumnHeader14"].Width;
                    }
                    newTemplate.Row.Cells["MEISAI_MANIFEST_ID"].Visible = true;
                    newTemplate.ColumnHeaders["columnHeaderSection1"]["gcCustomColumnHeader14"].Visible = true;
                }

                MultiRow.Template = newTemplate;
                MultiRow.ResumeLayout();
            }
            return;
        }

        #region ダブルクリック時にFrom項目の入力内容をコピーする

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141201 teikyou ダブルクリックを追加する　start
        private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var hidukeFromTextBox = this.form.HeaderForm.HIDUKE_FROM;
            var hidukeToTextBox = this.form.HeaderForm.HIDUKE_TO;
            hidukeToTextBox.Text = hidukeFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }

        // 20141201 teikyou ダブルクリックを追加する　end

        #endregion

        /// 20141203 Houkakou 「利用履歴」の日付チェックを追加する　start

        #region 日付チェック

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.HeaderForm.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.HeaderForm.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.HeaderForm.HIDUKE_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.HeaderForm.HIDUKE_TO.Text))
            {
                return false;
            }

            DateTime date_from = Convert.ToDateTime(this.form.HeaderForm.HIDUKE_FROM.Value);
            DateTime date_to = Convert.ToDateTime(this.form.HeaderForm.HIDUKE_TO.Value);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.HeaderForm.HIDUKE_FROM.IsInputErrorOccured = true;
                this.form.HeaderForm.HIDUKE_TO.IsInputErrorOccured = true;
                this.form.HeaderForm.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.HeaderForm.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "更新日付From", "更新日付To" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.HeaderForm.HIDUKE_FROM.Focus();
                return true;
            }

            return false;
        }

        #endregion

        #region HIDUKE_FROM_Leaveイベント

        /// <summary>
        /// TEKIYOU_BEGIN_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void HIDUKE_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.HeaderForm.HIDUKE_TO.Text))
            {
                this.form.HeaderForm.HIDUKE_TO.IsInputErrorOccured = false;
                this.form.HeaderForm.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
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
            if (!string.IsNullOrEmpty(this.form.HeaderForm.HIDUKE_FROM.Text))
            {
                this.form.HeaderForm.HIDUKE_FROM.IsInputErrorOccured = false;
                this.form.HeaderForm.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            }
        }

        #endregion

        /// 20141203 Houkakou 「利用履歴」の日付チェックを追加する　end

        /// <summary>
        /// F10 並べ替え
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                //タブIndex
                if (string.IsNullOrWhiteSpace(this.form.txtDenpyouKind.Text))
                {
                    return;
                }
                int tabindex = int.Parse(this.form.txtDenpyouKind.Text);
                int cnt = 0;

                switch (tabindex)
                {
                    case 1:
                        //受付
                        cnt = this.form.Uketsuke_Denpyou.RowCount;
                        this.form.customSortHeader1.LinkedDataGridViewName = "Uketsuke_Denpyou";
                        linkedDataGridView = this.form.Uketsuke_Denpyou;
                        break;
                    //case 2:
                    //    //計量
                    //    cnt = this.form.Keiryou_Denpyou.RowCount;
                    //    this.form.customSortHeader1.LinkedDataGridViewName = "Keiryou_Denpyou";
                    //    linkedDataGridView = this.form.Keiryou_Denpyou;
                    //    break;
                    case 2:
                        //受入
                        cnt = this.form.Ukeire_Denpyou.RowCount;
                        this.form.customSortHeader1.LinkedDataGridViewName = "Ukeire_Denpyou";
                        linkedDataGridView = this.form.Ukeire_Denpyou;
                        break;

                    case 3:
                        //出荷
                        cnt = this.form.Shukka_Denpyou.RowCount;
                        this.form.customSortHeader1.LinkedDataGridViewName = "Shukka_Denpyou";
                        linkedDataGridView = this.form.Shukka_Denpyou;
                        break;

                    case 4:
                        //売上/支払
                        cnt = this.form.UriageShiharai_Denpyou.RowCount;
                        this.form.customSortHeader1.LinkedDataGridViewName = "UriageShiharai_Denpyou";
                        linkedDataGridView = this.form.UriageShiharai_Denpyou;
                        break;

                    case 5:
                        //代納
                        cnt = this.form.MultiRow_DaiNouDenpyou.RowCount;
                        this.form.customSortHeader1.LinkedDataGridViewName = "dgvDainou_Denpyou_Sort";
                        linkedDataGridView = this.form.dgvDainou_Denpyou_Sort;
                        break;

                    case 6:
                        //入金
                        cnt = this.form.Nyuukin_Denpyou.RowCount;
                        this.form.customSortHeader1.LinkedDataGridViewName = "Nyuukin_Denpyou";
                        linkedDataGridView = this.form.Nyuukin_Denpyou;
                        break;

                    case 7:
                        //出金
                        cnt = this.form.Shukkin_Denpyou.RowCount;
                        this.form.customSortHeader1.LinkedDataGridViewName = "Shukkin_Denpyou";
                        linkedDataGridView = this.form.Shukkin_Denpyou;
                        break;
                }
                //一覧に明細行がない場合
                if (cnt == 0)
                {
                    //アラートを表示し、画面遷移しない
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E076");
                }
                else
                {
                    //ソート設定ダイアログを呼び出す
                    if (sortSettingInfo != null && linkedDataGridView != null)
                    {
                        var dataTable = linkedDataGridView.DataSource as DataTable;
                        {
                            sortSettingInfo.SetDataGridViewColumns(linkedDataGridView);
                            foreach (var sortCol in this.SortColumns)
                            {
                                foreach (DataGridViewColumn column in this.linkedDataGridView.Columns)
                                {
                                    if (column.HeaderText.Equals(sortCol.HeaderText))
                                    {
                                        sortCol.Name = column.Name;
                                        break;
                                    }
                                }
                                sortSettingInfo.SortColumns.Add(sortCol);
                            }
                            SetDataGridViewColumns(linkedDataGridView);
                            var dlg = new SortSettingForm(sortSettingInfo);
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                if (dataTable != null)
                                {
                                    SortDataTable(dataTable);
                                }
                            }
                            dlg.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// ソート条件のユーザー変更
        /// </summary>
        public void SortDataTable(DataTable dataTable)
        {
            if (dataTable == null)
            {
                return;
            }

            if (sortSettingInfo == null)
            {
                return;
            }

            SetDataTableColumns(dataTable);
            var sb = new System.Text.StringBuilder();
            this.form.customSortHeader1.txboxSortSettingInfo.Text = sortSettingInfo.SortSettingCaption;
            foreach (var item in this.sortSettingInfo.SortColumns)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }
                if (this.form.txtDenpyouKind.Text == "1" && item.Name == "UPDATE_DATE")
                {
                    sb.AppendFormat("{0} {1}", "UPDATE_DATE_SORT", item.IsAsc ? "ASC" : "DESC");
                }
                else
                {
                    sb.AppendFormat("{0} {1}", item.Name, item.IsAsc ? "ASC" : "DESC");
                }
            }

            if (sb.Length > 0)
            {
                sb.Append(", ");
            }
            sb.AppendFormat("SYSTEM_ID DESC, SEQ ASC");

            dataTable.DefaultView.Sort = sb.ToString();
            linkedDataGridView.DataSource = dataTable;
            if(this.form.txtDenpyouKind.Text == "5")
            {
                // 代納の時のみ伝票がMultiRowであるため、DGVからMultiRowへの反映処理を追加
                this.form.MultiRow_DaiNouDenpyou.DataSource = linkedDataGridView.DataSource;
            }
        }

        public void SetDataGridViewColumns(DataGridView grid)
        {
            // ソート用に表示カラムのみコピー
            var gridColumns = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn gridColumn in grid.Columns)
            {
                if (gridColumn.IsDataBound && gridColumn.Visible)
                {
                    gridColumns.Add(gridColumn);
                }
            }

            // 表示インデックスでソート
            gridColumns.Sort(
                delegate(DataGridViewColumn x, DataGridViewColumn y)
                {
                    return x.DisplayIndex - y.DisplayIndex;
                }
            );

            // グリッドの表示列タイトルでリスト作成
            this.ViewColumns = new List<CustomSortColumn>();
            foreach (DataGridViewColumn gridColumn in gridColumns)
            {
                var viewColumn = new CustomSortColumn(gridColumn.DataPropertyName, gridColumn.HeaderText, true);
                this.ViewColumns.Add(viewColumn);
            }

            // 存在しているものだけをソート項目に残す
            var tempColumns = new List<CustomSortColumn>();
            foreach (var sortColumn in this.SortColumns)
            {
                foreach (var viewColumn in this.ViewColumns)
                {
                    if (viewColumn.Name.Equals(sortColumn.Name))
                    {
                        tempColumns.Add(sortColumn);
                        break;
                    }
                }
            }
            this.SortColumns = tempColumns;
        }

        public void SetDataTableColumns(DataTable dataTable)
        {
            // DataTableに存在しないソート項目を削除する
            var tempColumns = new List<CustomSortColumn>();
            foreach (var sortColumn in this.sortSettingInfo.SortColumns)
            {
                foreach (var viewColumn in this.ViewColumns)
                {
                    if (viewColumn.HeaderText.Equals(sortColumn.HeaderText))
                    {
                        sortColumn.Name = viewColumn.Name;
                        break;
                    }
                }
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    if (dataColumn.ColumnName.Equals(sortColumn.Name))
                    {
                        tempColumns.Add(sortColumn);
                        break;
                    }
                }
            }
            this.SortColumns = tempColumns;
        }

        /// <summary>
        /// ソート条件のクリア
        /// </summary>
        public void ClearCustomSortSetting()
        {
            if (this.sortSettingInfo != null)
            {
                this.sortSettingInfo.Clear();
                this.form.customSortHeader1.txboxSortSettingInfo.Text = sortSettingInfo.SortSettingCaption;

                this.SortColumns = new List<CustomSortColumn>();
            }
        }
    }
}