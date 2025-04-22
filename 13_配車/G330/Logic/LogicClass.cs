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
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.APP;
using Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.DAO;
using Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.DTO;
using Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.Entity;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Function.ShougunCSCommon.Const;
using Shougun.Function.ShougunCSCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Const;
using System.Text;

namespace Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic, IDisposable
    {
        #region Enum
        /// <summary>
        /// 税区分CD
        /// </summary>
        private enum ZeiKbnCd
        {
            /// <summary>
            /// なし
            /// </summary>
            None = 0,
            /// <summary>
            /// 外税(1)
            /// </summary>
            SotoZei = 1,
            /// <summary>
            /// 内税(2)
            /// </summary>
            UchiZei = 2,
            /// <summary>
            /// 非課税(3)
            /// </summary>
            HikaZei = 3,
        }

        /// <summary>
        /// 端数区分
        /// </summary>
        private enum TaxHasuuCd
        {
            /// <summary>
            /// なし
            /// </summary>
            None = 0,
            /// <summary>
            /// 切り上げ(1)
            /// </summary>
            KiriAge = 1,
            /// <summary>
            /// 切捨て(2)
            /// </summary>
            KiriSute = 2,
            /// <summary>
            /// 四捨五入(3)
            /// </summary>
            SisyaGonyu = 3,
        }

        /// <summary>
        /// 税計算区分
        /// </summary>
        private enum TaxSansyutuKbn
        {
            /// <summary>
            /// なし(0)
            /// </summary>
            None = 0,
            /// <summary>
            /// 伝票(1)
            /// </summary>
            Denpyo = 1,
            /// <summary>
            /// 請求(2)
            /// </summary>
            Seikyu = 2,
            /// <summary>
            /// 明細(3)
            /// </summary>
            Meisai = 3,
        }

        /// <summary>
        /// 締め日コンボインデックス
        /// </summary>
        private enum ShimebiSelcetIndex
        {
            Shime = 0,
            Shime00 = 1,
            Shime05 = 2,
            Shime10 = 3,
            Shime15 = 4,
            Shime20 = 5,
            Shime25 = 6,
            Shime31 = 7,
        }

        #endregion

        #region 内部定数

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.Setting.ButtonSetting.xml";

        /// <summary>
        /// グリッドカラム名：１列目(選択)
        /// </summary>
        private const string DEF_COLUMN_1_NAME = "CHECK_SELECT";
        /// <summary>
        /// グリッドカラム名：２列目(定期)
        /// </summary>
        private const string DEF_COLUMN_2_NAME = "TEIKI_FLG";
        /// <summary>
        /// グリッドカラム名：３列目(単価)
        /// </summary>
        private const string DEF_COLUMN_3_NAME = "TANKA_FLG";
        /// <summary>
        /// グリッドカラム名：２列目(取引先CD)
        /// </summary>
        private const string DEF_COLUMN_4_NAME = "TORIHIKISAKI_CD";
        /// <summary>
        /// グリッドカラム名：３列目(取引先略称)
        /// </summary>
        private const string DEF_COLUMN_5_NAME = "TORIHIKISAKI_NAME_RYAKU";
        /// <summary>
        /// グリッドカラム名：４列目(取引先フリガナ)
        /// </summary>
        private const string DEF_COLUMN_6_NAME = "TORIHIKISAKI_FURIGANA";

        #endregion

        #region 内部変数

        /// <summary>
        /// DBアクセス共通クラス
        /// </summary>
        private DBAccessor dbAccesser;

        public Accessor.DBAccessor dbAccesserForG303;

        /// <summary>
        /// グリッド表示用検索条件
        /// </summary>
        private SearchDTOClass whereDto;

        /// <summary>
        /// 実績売上支払確定用Dao
        /// </summary>
        private GetTorihikisakiDAOClass daoSelect;

        /// <summary>
        /// 売上支払出たい取得用Dao
        /// </summary>
        private GetUrShDataDao daoUrShDate;

        /// <summary>
        /// 受入／支払入力Dao
        /// </summary>
        private SetUrShEntryDao daoUrShEntry;

        /// <summary>
        /// 受入／支払明細Dao
        /// </summary>
        private SetUrShDetailDao daoUrShDetail;

        /// <summary>
        /// 定期実績入力Dao
        /// </summary>
        private SetTeikeiJissekiEntryDao daoTeikiJissekiEntry;

        /// <summary>
        /// 定期実績明細Dao
        /// </summary>
        private SetTeikeiJissekiDetailDao daoTeikiJissekiDetail;

        /// <summary>
        /// 締処理チェック用Dao
        /// </summary>
        private GetShimeDataDao daoShimeData;

        /// <summary>
        /// IM_KYOTENDao(拠点Dao)
        /// </summary>
        private IM_KYOTENDao kyotenDao;

        /// <summary>
        /// IM_TORIHIKISAKIDao(取引先Dao)
        /// </summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        /// IM_SHOUHIZEIDao(消費税Dao)
        /// </summary>
        private IM_SHOUHIZEIDao shouhizeiDao;

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// DGV表示データ有無
        /// </summary>
        public bool dispDataRecord = false;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        public BusinessBaseForm parentForm;

        private MessageBoxShowLogic MsgBox;

        /// <summary>
        /// BusinessCommonのDBAccesser
        /// </summary>
        private Shougun.Core.Common.BusinessCommon.DBAccessor commonAccesser;
        #endregion

        #region プロパティ

        /// <summary>
        /// Select結果
        /// </summary>
        public List<T_SELECT_RESULT> ResultList = null;

        /// <summary>
        /// Select結果(伝票区分：売上、支払以外)
        /// チェック用のList
        /// </summary>
        public List<T_SELECT_RESULT> ResultListForCheck = null;

        /// <summary>
        /// Select結果集計
        /// </summary>
        public List<GRID_ROW_VALUE> ResultGroupList = null;

        /// <summary>
        /// グリッド表示用DataTable
        /// </summary>
        public DataTable GridDataTbl { get; set; }

        /// <summary>
        /// DB更新データリスト
        /// </summary>
        public List<DB_UPDATE_DATA> DbUpdateList = null;

        /// <summary>
        /// 請求明細(締済みチェック用)
        /// </summary>
        internal List<SHIME_DATA> ShimeDataList = null;

        /// <summary>
        /// 取引先_請求締日マスタDAO
        /// </summary>
        private GetSeikyuuShimebiDao SeikyuuShimebiDao;

        /// <summary>
        /// 取引先_支払締日マスタDAO
        /// </summary>
        private GetShiharaiShimebiDao ShiharaiShimebiDao;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            // Form
            this.form = targetForm;
            // 拠点CD
            this.kyotenDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KYOTENDao>();
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            this.MsgBox = new MessageBoxShowLogic();
            this.commonAccesser = new Shougun.Core.Common.BusinessCommon.DBAccessor();
            // 取引先
            this.torihikisakiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKIDao>();
            // 消費税
            this.shouhizeiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHOUHIZEIDao>();

            // 取引先_請求締日
            this.SeikyuuShimebiDao = DaoInitUtility.GetComponent<GetSeikyuuShimebiDao>();

            // 取引先_支払締日
            this.ShiharaiShimebiDao = DaoInitUtility.GetComponent<GetShiharaiShimebiDao>();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <returns></returns>
        internal void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // フォームの初期化
                this.FormInit();

                // DTO初期化
                this.DaoInit();

                //20151021 hoanghm #13498 start
                this.LoadKyotenForPopup();
                //20151021 hoanghm #13498 end

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 拠点初期値設定
                this.SetInitKyoten();

                //取引先
                this.SetInitTorihikisaki();

                // 画面の初期化
                this.SetInitData();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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

            //プロセスボタンを非表示設定
            this.parentForm = this.form.ParentBaseForm;
            parentForm.ProcessButtonPanel.Visible = false;

            this.form.customDataGridView1.Anchor = (AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom);

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

            this.dbAccesser = new DBAccessor();
            this.dbAccesserForG303 = new Accessor.DBAccessor();
            this.daoSelect = DaoInitUtility.GetComponent<GetTorihikisakiDAOClass>();
            this.daoUrShEntry = DaoInitUtility.GetComponent<SetUrShEntryDao>();
            this.daoUrShDetail = DaoInitUtility.GetComponent<SetUrShDetailDao>();
            this.daoTeikiJissekiEntry = DaoInitUtility.GetComponent<SetTeikeiJissekiEntryDao>();
            this.daoTeikiJissekiDetail = DaoInitUtility.GetComponent<SetTeikeiJissekiDetailDao>();
            this.daoUrShDate = DaoInitUtility.GetComponent<GetUrShDataDao>();
            this.daoShimeData = DaoInitUtility.GetComponent<GetShimeDataDao>();

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
            parentForm.bt_func2.Enabled = true;
            parentForm.bt_func3.Enabled = true;
            parentForm.bt_func4.Enabled = true;
            parentForm.bt_func5.Enabled = true;
            parentForm.bt_func6.Enabled = false;
            parentForm.bt_func7.Enabled = false;
            parentForm.bt_func8.Enabled = true;
            parentForm.bt_func9.Enabled = true;
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

        #region 拠点設定
        /// <summary>
        /// 拠点初期値設定
        /// </summary>
        private void SetInitKyoten()
        {
            LogUtility.DebugMethodStart();

            // 拠点
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            this.form.txtBox_KyotenCd.Text = this.GetUserProfileValue(userProfile, "拠点CD");
            if (!string.IsNullOrEmpty(this.form.txtBox_KyotenCd.Text.ToString()))
            {
                this.form.txtBox_KyotenCd.Text = this.form.txtBox_KyotenCd.Text.ToString().PadLeft(this.form.txtBox_KyotenCd.MaxLength, '0');
                this.CheckKyotenCd();
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 取引先設定
        private void SetInitTorihikisaki()
        {
            LogUtility.DebugMethodStart();

            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_custom.Text.ToString()))
            {
                this.CheckTorihikisakiCd();
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ユーザー定義情報取得処理
        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile">ＸＭＬファイルにアクセスするためのクラス</param>
        /// <param name="key">キー</param>
        /// <returns>キーに紐づく値</returns>
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
                this.form.txtBox_KyotenNameRyaku.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.txtBox_KyotenCd.Text))
                {
                    this.form.txtBox_KyotenNameRyaku.Text = string.Empty;
                    return;
                }

                short kyoteCd = -1;
                if (!short.TryParse(this.form.txtBox_KyotenCd.Text, out kyoteCd))
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
                    this.form.txtBox_KyotenNameRyaku.Text = kyoten.KYOTEN_NAME_RYAKU.ToString();
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

        #region ヘッダーの取引先CDの存在チェック
        internal void CheckTorihikisakiCd()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 初期化
                this.form.TORIHIKISAKI_NAME_custom.Text = string.Empty;
                if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_custom.Text))
                {
                    this.form.TORIHIKISAKI_NAME_custom.Text = string.Empty;
                    return;
                }
                M_TORIHIKISAKI torihikisakiEntity = new M_TORIHIKISAKI();
                torihikisakiEntity.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD_custom.Text;
                torihikisakiEntity.ISNOT_NEED_DELETE_FLG = true;
                var torihikisakis = this.torihikisakiDao.GetAllValidData(torihikisakiEntity);

                // 存在チェック
                if (torihikisakis == null || torihikisakis.Length < 1)
                {                    
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "取引先");
                    this.form.TORIHIKISAKI_CD_custom.Focus();
                    return;
                }

                // 取引区分が掛けの場合、締日チェック
                if (this.form.TorihikiKbnValue.Text == SearchDTOClass.TORIHIKI_KBN_VALUE_KAKE)
                {
                    bool isErr = this.CheckTorihikisakiShimebi(this.form.TORIHIKISAKI_CD_custom.Text, this.form.cmb_Shimebi.Text);
                    if (!isErr)
                    {
                        MessageBoxShowLogic logic = new MessageBoxShowLogic();
                        logic.MessageBoxShow("E058");
                        this.form.TORIHIKISAKI_CD_custom.Focus();
                        return;
                    }
                }

                // キーが１つなので複数はヒットしないはず
                M_TORIHIKISAKI torihikisaki = torihikisakis[0];
                this.form.TORIHIKISAKI_NAME_custom.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU.ToString();
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

        #region 締日コンボの初期設定

        /// <summary>
        /// 締日コンボ、日付の初期設定
        /// </summary>
        private void InitShimebi()
        {
            LogUtility.DebugMethodStart();

            // 取引区分が現金の場合
            if (this.form.TorihikiKbnValue.Text == SearchDTOClass.TORIHIKI_KBN_VALUE_GENKIN)
            {
                this.form.cmb_Shimebi.Enabled = false;
                this.form.cmb_Shimebi.SelectedIndex = (int)ShimebiSelcetIndex.Shime;
            }
            // 取引区分が掛けの場合
            else if (this.form.TorihikiKbnValue.Text == SearchDTOClass.TORIHIKI_KBN_VALUE_KAKE)
            {
                // 締め日コンボ初期設定
                this.form.cmb_Shimebi.Enabled = true;
                this.form.dtp_KikanFrom.Enabled = true;
                this.form.dtp_KikanTo.Enabled = true;

                // 初期は空白に設定
                this.form.cmb_Shimebi.SelectedIndex = (int)ShimebiSelcetIndex.Shime;

                int day = parentForm.sysDate.Day;
                int endDay = DateTime.DaysInMonth(parentForm.sysDate.Year, parentForm.sysDate.Month);

                // 締日コンボは「5」を選択
                if (5 < day && day <= 10)
                {
                    this.form.cmb_Shimebi.SelectedIndex = (int)ShimebiSelcetIndex.Shime05;
                }

                // 締日コンボは「10」を選択
                else if (10 < day && day <= 15)
                {
                    this.form.cmb_Shimebi.SelectedIndex = (int)ShimebiSelcetIndex.Shime10;
                }

                // 締日コンボは「15」を選択
                else if (15 < day && day <= 20)
                {
                    this.form.cmb_Shimebi.SelectedIndex = (int)ShimebiSelcetIndex.Shime15;
                }

                // 締日コンボは「20」を選択
                else if (20 < day && day <= 25)
                {
                    this.form.cmb_Shimebi.SelectedIndex = (int)ShimebiSelcetIndex.Shime20;
                }

                // 締日コンボは「25」を選択
                else if (25 < day && day <= endDay)
                {
                    this.form.cmb_Shimebi.SelectedIndex = (int)ShimebiSelcetIndex.Shime25;
                }

                // 締日コンボは「31」を選択
                else
                {
                    this.form.cmb_Shimebi.SelectedIndex = (int)ShimebiSelcetIndex.Shime31;
                }
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 初期データの画面設定

        /// <summary>
        /// 初期データ設定
        /// </summary>
        private void SetInitData()
        {
            LogUtility.DebugMethodStart();

            // 検索条件
            this.form.fixConditionValue.Text = SearchDTOClass.FIX_CONDITION_VALUE_UNFIXED;
            this.form.TorihikiKbnValue.Text = SearchDTOClass.TORIHIKI_KBN_VALUE_KAKE;

            // 締日の初期化設定
            this.InitShimebi();

            //取引先
            this.form.TORIHIKISAKI_CD_custom.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_custom.Text = string.Empty;

            // 0件時の画面設定
            this.SetZeroGridData();

            // ソート条件のクリア
            this.form.customSortHeader1.Size = new System.Drawing.Size(941, 26);
            this.form.customSortHeader1.ClearCustomSortSetting();

            LogUtility.DebugMethodEnd();
        }

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
            parentForm.bt_func1.Click += new System.EventHandler(this.bt_func1_Click);            //全て選択
            parentForm.bt_func2.Click += new System.EventHandler(this.bt_func2_Click);            //全て解除
            parentForm.bt_func3.Click += new System.EventHandler(this.bt_func3_Click);            //前月
            parentForm.bt_func4.Click += new System.EventHandler(this.bt_func4_Click);            //翌月
            parentForm.bt_func5.Click += new System.EventHandler(this.bt_func5_Click);            //条件ｸﾘｱ
            parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);            //検索
            parentForm.bt_func9.Click += new System.EventHandler(this.bt_func9_Click);            //実行
            parentForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);          //並び替え
            parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);          //閉じる

            // コンボボックスのイベント生成
            this.form.cmb_Shimebi.SelectedIndexChanged += new System.EventHandler(this.cmb_Shimebi_SelectedIndexChanged);

            // Gridのイベント生成
            this.form.customDataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(customDataGridView1_CellContentClick);

            // 20141128 teikyou ダブルクリックを追加する　start
            this.form.dtp_KikanTo.MouseDoubleClick += new MouseEventHandler(dtp_KikanTo_MouseDoubleClick);
            // 20141128 teikyou ダブルクリックを追加する　end

            /// 20141203 Houkakou 「実績売上支払確定」の日付チェックを追加する　start
            this.form.dtp_KikanFrom.Leave += new System.EventHandler(dtp_KikanFrom_Leave);
            this.form.dtp_KikanTo.Leave += new System.EventHandler(dtp_KikanTo_Leave);
            /// 20141203 Houkakou 「実績売上支払確定」の日付チェックを追加する　end

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
            parentForm.bt_func1.Click -= new System.EventHandler(this.bt_func1_Click);            //全て選択
            parentForm.bt_func2.Click -= new System.EventHandler(this.bt_func2_Click);            //全て解除
            parentForm.bt_func3.Click -= new System.EventHandler(this.bt_func3_Click);            //前月
            parentForm.bt_func4.Click -= new System.EventHandler(this.bt_func4_Click);            //翌月
            parentForm.bt_func5.Click -= new System.EventHandler(this.bt_func5_Click);            //条件ｸﾘｱ
            parentForm.bt_func8.Click -= new System.EventHandler(this.bt_func8_Click);            //検索
            parentForm.bt_func9.Click -= new System.EventHandler(this.bt_func9_Click);            //実行
            parentForm.bt_func10.Click -= new System.EventHandler(this.bt_func10_Click);          //並び替え
            parentForm.bt_func12.Click -= new System.EventHandler(this.bt_func12_Click);          //閉じる

            // コンボボックスのイベント解除
            this.form.cmb_Shimebi.SelectedIndexChanged -= new System.EventHandler(this.cmb_Shimebi_SelectedIndexChanged);

            // Gridのイベント解除
            this.form.customDataGridView1.CellContentClick -= new System.Windows.Forms.DataGridViewCellEventHandler(customDataGridView1_CellContentClick);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region イベント：Fキー処理

        /// <summary>
        /// F1 全て選択
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.GridCheckAll();
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
        /// F2 全て解除
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.GridUnCheckAll();
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
        /// F3 前月
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 前月移動
                this.KikanPriv();
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
        /// F4 翌月
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 翌月移動
                this.KikanNext();
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
        /// F5 条件ｸﾘｱ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func5_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // ロック解除
                this.UnRockUIControl();

                // グリッドクリア
                this.SetZeroGridData();

                this.form.TORIHIKISAKI_CD_custom.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME_custom.Text = string.Empty;

                this.form.customSortHeader1.ClearCustomSortSetting();
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
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 入力チェック
                if (this.CheckInputDate())
                {
                    // 検索
                    if (0 < this.Search())
                    {
                        // 画面反映
                        this.SetResultData();

                        // 検索条件を設定不可にする
                        this.RockUIControl();
                    }
                    else
                    {
                        // ゼロ件メッセージ表示
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("C001", "検索結果");

                        // ゼロ件表示
                        this.SetZeroGridData();

                        // 検索条件を設定可にする
                        this.UnRockUIControl();
                    }
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
        /// F9 実行
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //表示データあり
                if (this.dispDataRecord)
                {
                    var hadSelected = this.GridDataTbl.AsEnumerable().Any(row => (int)row[DEF_COLUMN_1_NAME] == 1);
                    if (!hadSelected)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E034", "取引先CD");
                        return;
                    }
                    
                    // 伝票区分CDに売上、支払の定期実績明細が存在するかチェック
                    if (this.IsUnFixFlg() && this.CheckUnknownDenpyou())
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        var dialogResult = msgLogic.MessageBoxShow("C046", "選択した取引先の中に、確定処理ができない定期配車実績情報が含まれています。確定処理を続行");
                        if (dialogResult == DialogResult.No)
                        {
                            return;
                        }
                    }
                    if (this.CheckUrSh())
                    {
                        return;
                    }

                    // 20141128 ブン 締済期間チェックの追加 start
                    // データ登録処理
                    if (this.Execute())
                    {
                        // 完了メッセージ表示
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("I001", "確定処理");

                        // 検索条件を設定可にする
                        this.UnRockUIControl();

                        // 画面の初期化
                        this.SetInitData();
                    }
                    // 20141128 ブン 締済期間チェックの追加 end
                }
                else
                {
                    // 登録データなし表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E061");

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
        /// F10 並び替え
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // 無条件に表示でよい
                this.form.customSortHeader1.ShowCustomSortSettingDialog();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

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

        #region チェック処理

        /// <summary>
        /// 入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckInputDate()
        {
            LogUtility.DebugMethodStart();

            // 必須チェック 締日
            if (string.IsNullOrWhiteSpace(this.form.cmb_Shimebi.Text.Trim()) &&
                this.form.TorihikiKbnValue.Text == SearchDTOClass.TORIHIKI_KBN_VALUE_KAKE)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E001", "締日");
                this.form.cmb_Shimebi.Select();
                this.form.cmb_Shimebi.Focus();
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            // 必須チェック 伝票日付(FROM) 
            if (string.IsNullOrWhiteSpace(this.form.dtp_KikanFrom.Text.Trim()))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E001", "期間");
                this.form.dtp_KikanFrom.Select();
                this.form.dtp_KikanFrom.Focus();
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            // 必須チェック 伝票日付(TO) 
            if (string.IsNullOrWhiteSpace(this.form.dtp_KikanTo.Text.Trim()))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E001", "期間");
                this.form.dtp_KikanTo.Select();
                this.form.dtp_KikanTo.Focus();
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            // 整合性チェック 伝票日付 
            DateTime toDate = (DateTime)this.form.dtp_KikanTo.Value;
            DateTime fromDate = (DateTime)this.form.dtp_KikanFrom.Value;

            // 時間情報削除
            toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day);
            fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day);

            if (fromDate > toDate)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                /// 20141203 Houkakou 「実績売上支払確定」の日付チェックを追加する　start
                //msgLogic.MessageBoxShow("E043", "日付範囲");
                //this.form.dtp_KikanFrom.Select();
                //this.form.dtp_KikanFrom.Focus();
                this.form.dtp_KikanFrom.IsInputErrorOccured = true;
                this.form.dtp_KikanTo.IsInputErrorOccured = true;
                this.form.dtp_KikanFrom.BackColor = Constans.ERROR_COLOR;
                this.form.dtp_KikanTo.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "期間From", "期間To" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.dtp_KikanFrom.Focus();
                /// 20141203 Houkakou 「実績売上支払確定」の日付チェックを追加する　end
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 選択された取引先(に紐付く定期実績明細)の中に不明な伝票区分が存在するかチェック
        /// </summary>
        /// <returns>true：存在する、false：存在しない</returns>
        private bool CheckUnknownDenpyou()
        {
            LogUtility.DebugMethodStart();

            bool returnVal = false;

            // チェックされた取引先CDを取得
            List<string> torihikisakiCdList = new List<string>();
            foreach (DataRow gridRow in this.GridDataTbl.Rows)
            {
                if ((int)gridRow[DEF_COLUMN_1_NAME] == 1)
                {
                    string torihikisakiCd = gridRow[DEF_COLUMN_4_NAME].ToString().Trim();
                    torihikisakiCdList.Add(torihikisakiCd);
                }
            }

            // 取引先CDでグループ化
            var resultGroupList = new List<GRID_ROW_VALUE>();
            var groupList = this.ResultListForCheck.GroupBy(t => t.TORIHIKISAKI_CD).ToList();

            // 取引先CD単位でリスト化
            foreach (var groupKey in groupList)
            {
                var rowList = this.ResultListForCheck.Where(t => t.TORIHIKISAKI_CD == groupKey.Key).ToList();
                var result = rowList.FirstOrDefault();
                GRID_ROW_VALUE row = new GRID_ROW_VALUE();
                row.TORIHIKISAKI_CD = result.TORIHIKISAKI_CD;
                row.TORIHIKISAKI_NAME_RYAKU = result.TORIHIKISAKI_NAME_RYAKU;
                row.TORIHIKISAKI_FURIGANA = result.TORIHIKISAKI_FURIGANA;
                row.ResultList = rowList;
                resultGroupList.Add(row);
            }

            if (resultGroupList.Count > 0)
            {
                // チェックされた取引先CDが対象
                foreach (string torihikisakiCd in torihikisakiCdList.OrderBy(t => t))
                {
                    // 取引先CDに属するデータを取得
                    List<T_SELECT_RESULT> insertDataList = resultGroupList.FirstOrDefault(t =>
                                                                    t.TORIHIKISAKI_CD.Equals(torihikisakiCd)
                                                                ).ResultList;

                    foreach (var selectResult in insertDataList)
                    {
                        // 伝票区分：売上、支払以外かどうかはSQL文のほうで制御しているため
                        // 一件でもあれば返す
                        returnVal = true;
                        break;
                    }

                }
            }

            LogUtility.DebugMethodEnd(returnVal);
            return returnVal;
        }

        /// <summary>
        /// 確定区分：未確定が選択されているか
        /// form.fixConditionValueが空だった場合は、「未確定」と判断する
        /// </summary>
        /// <returns>true:未確定、false:未確定以外</returns>
        internal bool IsUnFixFlg()
        {
            LogUtility.DebugMethodStart();

            bool returnVal = true;

            if (!string.IsNullOrEmpty(this.form.fixConditionValue.Text)
                && !this.form.fixConditionValue.Text.Equals(SearchDTOClass.FIX_CONDITION_VALUE_UNFIXED))
            {
                returnVal = false;
            }

            LogUtility.DebugMethodEnd();

            return returnVal;
        }

        #endregion

        #region データ0件時の表示

        /// <summary>
        /// 表示データ0件時／初期起動時の表示処理
        /// </summary>
        void SetZeroGridData()
        {
            LogUtility.DebugMethodStart();

            //表示データなし
            this.dispDataRecord = false;

            // 空行作成
            DataTable tbl = this.GetZeroDataTable();

            // 検索結果初期化
            this.GridDataTbl = tbl;

            // 結果レコード登録
            this.form.customDataGridView1.IsBrowsePurpose = false;
            this.form.customDataGridView1.DataSource = this.GridDataTbl;
            this.form.customDataGridView1.IsBrowsePurpose = true;
            this.form.customDataGridView1.Refresh();

            // 選択不可
            this.form.customDataGridView1.ReadOnly = true;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ゼロ件DataTable取得
        /// </summary>
        /// <returns></returns>
        private DataTable GetZeroDataTable()
        {
            LogUtility.DebugMethodStart();
            DataTable tbl = new DataTable();
            tbl.Columns.Add(DEF_COLUMN_1_NAME, typeof(int));
            tbl.Columns.Add(DEF_COLUMN_2_NAME, typeof(int));
            tbl.Columns.Add(DEF_COLUMN_3_NAME, typeof(int));
            tbl.Columns.Add(DEF_COLUMN_4_NAME, typeof(string));
            tbl.Columns.Add(DEF_COLUMN_5_NAME, typeof(string));
            tbl.Columns.Add(DEF_COLUMN_6_NAME, typeof(string));

            /* 2013/12/25 空行1行表示はやめる */
            //tbl.Rows.Add(0, string.Empty, string.Empty, string.Empty);

            LogUtility.DebugMethodEnd(tbl);
            return tbl;
        }

        #endregion

        #region 《《検索・結果設定処理》》

        /// <summary>
        /// 表示データ取得
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            int res = 0;

            // 条件設定
            this.whereDto = new SearchDTOClass();
            this.whereDto.SetUIForm(this.form);

            // 検索
            this.ResultList = this.daoSelect.GetTorihikisakiDataForEntity(this.whereDto);

            foreach (T_SELECT_RESULT result in this.ResultList)
            {
                if (result.DENPYOU_KBN_CD.IsNull
                    || result.UNIT_CD.IsNull
                    || result.SAGYOU_DATE.IsNull)
                {
                    result.TANKA = 0;
                    continue;
                }

                if (result.KEIYAKU_KBN == 1 && !string.IsNullOrEmpty(result.TSUKI_HINMEI_CD))
                {
                    var kobetsuhinmeiTanka = this.commonAccesser.GetKobetsuhinmeiTanka(
                        (short)SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI,
                        result.DENPYOU_KBN_CD.Value,
                        result.TORIHIKISAKI_CD,
                        result.GYOUSHA_CD,
                        result.GENBA_CD,
                        result.UNPAN_GYOUSHA_CD,
                        result.NIOROSHI_GYOUSHA_CD,
                        result.NIOROSHI_GENBA_CD,
                        result.TSUKI_HINMEI_CD,
                        result.UNIT_CD.Value,
                        Convert.ToString(result.SAGYOU_DATE.Value)
                        );

                    // 個別品名単価から情報が取れない場合は基本品名単価の検索
                    if (kobetsuhinmeiTanka == null)
                    {
                        var kihonHinmeiTanka = this.commonAccesser.GetKihonHinmeitanka(
                            (short)SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI,
                            result.DENPYOU_KBN_CD.Value,
                        result.UNPAN_GYOUSHA_CD,
                        result.NIOROSHI_GYOUSHA_CD,
                        result.NIOROSHI_GENBA_CD,
                        result.TSUKI_HINMEI_CD,
                        result.UNIT_CD.Value,
                        Convert.ToString(result.SAGYOU_DATE.Value)
                            );
                        if (kihonHinmeiTanka != null)
                        {
                            result.TANKA = kihonHinmeiTanka.TANKA;
                        }
                        else
                        {
                            result.TANKA = 0;
                        }
                    }
                    else
                    {
                        result.TANKA = kobetsuhinmeiTanka.TANKA;
                    }
                }
                else if (result.KEIYAKU_KBN == 2 && !string.IsNullOrEmpty(result.HINMEI_CD))
                {
                    /**
                 * 単価設定
                 */
                    var kobetsuhinmeiTanka = this.commonAccesser.GetKobetsuhinmeiTanka(
                        (short)SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI,
                        result.DENPYOU_KBN_CD.Value,
                        result.TORIHIKISAKI_CD,
                        result.GYOUSHA_CD,
                        result.GENBA_CD,
                        result.UNPAN_GYOUSHA_CD,
                        result.NIOROSHI_GYOUSHA_CD,
                        result.NIOROSHI_GENBA_CD,
                        result.HINMEI_CD,
                        result.UNIT_CD.Value,
                        Convert.ToString(result.SAGYOU_DATE.Value)
                        );

                    // 個別品名単価から情報が取れない場合は基本品名単価の検索
                    if (kobetsuhinmeiTanka == null)
                    {
                        var kihonHinmeiTanka = this.commonAccesser.GetKihonHinmeitanka(
                            (short)SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI,
                            result.DENPYOU_KBN_CD.Value,
                        result.UNPAN_GYOUSHA_CD,
                        result.NIOROSHI_GYOUSHA_CD,
                        result.NIOROSHI_GENBA_CD,
                        result.HINMEI_CD,
                        result.UNIT_CD.Value,
                        Convert.ToString(result.SAGYOU_DATE.Value)
                            );
                        if (kihonHinmeiTanka != null)
                        {
                            result.TANKA = kihonHinmeiTanka.TANKA;
                        }
                        else
                        {
                            result.TANKA = 0;
                        }
                    }
                    else
                    {
                        result.TANKA = kobetsuhinmeiTanka.TANKA;
                    }
                }
                else
                {
                    result.TANKA = 0;
                }
            }

            // チェック用の変数にもセット
            this.ResultListForCheck = this.daoSelect.GetTeikiJissekiDetailDataForEntity(this.whereDto);

            // 締済かチェック用のリストをセット
            this.ShimeDataList = new List<SHIME_DATA>();
            List<long> whereList = new List<long>();
            var UrShNumberList = this.ResultList.Where(t => !t.UR_SH_NUMBER.IsNull).GroupBy(t => new { t.UR_SH_NUMBER });
            foreach (var urShNumber in UrShNumberList)
            {

                whereList.Add((long)urShNumber.Key.UR_SH_NUMBER);
            }

            if (whereList.Count > 0)
            {
                // 技術課題 #739
                // 上記のチェック用リストを要素200件毎に分割し、さらに、分割された200件毎のリストひとつひとつを要素とするメタリストを作成する
                // IN句に要素2100件以上を含むことができない、というSQL Serverの制限があるため
                var ListOfwhereList = whereList.Select((a, b) => new { a, b }).GroupBy(c => c.b / 200)
                                            .Select(d => d.Select(e => e.a).ToList())
                                                .ToList();

                foreach (var NumberList in ListOfwhereList)
                {
                    // 請求、精算データを取得し、締め済チェック用のListに追加
                    var seikyuData = this.daoShimeData.GetSeikyuuData(NumberList.ToArray());
                    var seisanData = this.daoShimeData.GetSeisanData(NumberList.ToArray());

                    if (seikyuData != null && seikyuData.Count > 0)
                    {
                        this.ShimeDataList.AddRange(seikyuData);
                    }
                    if (seisanData != null && seisanData.Count > 0)
                    {
                        this.ShimeDataList.AddRange(seisanData);
                    }
                }
            }

            // 検索結果集計、グリッド用DataTable生成
            res = this.ResultDataGroupBy();

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        /// <summary>
        /// 取得データからGrid用DataTableを作成
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private int ResultDataGroupBy()
        {
            LogUtility.DebugMethodStart();

            int res = 0;

            // 集計クラス初期化
            this.ResultGroupList = new List<GRID_ROW_VALUE>();

            // 取引先CDでグループ化
            var groupList = this.ResultList.GroupBy(t => t.TORIHIKISAKI_CD).ToList();

            // 取引先CD単位でリスト化
            foreach (var groupKey in groupList)
            {
                var rowList = this.ResultList.Where(t => t.TORIHIKISAKI_CD == groupKey.Key).ToList();

                int teikiFlg = 0;
                int tankaFlg = 0;
                foreach (var data in rowList)
                {
                    if (data.KEIYAKU_KBN == 1)
                    {
                        teikiFlg = 1;
                    }
                    else if (data.KEIYAKU_KBN == 2)
                    {
                        tankaFlg = 1;
                    }
                }

                var result = rowList.FirstOrDefault();
                GRID_ROW_VALUE row = new GRID_ROW_VALUE();
                row.TEIKI_FLG = teikiFlg;
                row.TANKA_FLG = tankaFlg;
                row.TORIHIKISAKI_CD = result.TORIHIKISAKI_CD;
                row.TORIHIKISAKI_NAME_RYAKU = result.TORIHIKISAKI_NAME_RYAKU;
                row.TORIHIKISAKI_FURIGANA = result.TORIHIKISAKI_FURIGANA;
                row.ResultList = rowList;
                this.ResultGroupList.Add(row);
            }

            // ソート
            this.ResultGroupList = this.ResultGroupList.OrderBy(t => t.TORIHIKISAKI_FURIGANA)
                                            .ThenBy(t => t.TORIHIKISAKI_CD)
                                            .ThenBy(t => t.TORIHIKISAKI_NAME_RYAKU)
                                            .ToList();

            // 集計結果カウント
            res = this.ResultGroupList.Count;

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        /// <summary>
        /// 検索データ反映
        /// </summary>
        private void SetResultData()
        {
            LogUtility.DebugMethodStart();

            //表示データあり
            this.dispDataRecord = true;

            // 表示用DataTable作成
            this.GridDataTbl = this.MakeGridDataTable(this.ResultGroupList);

            // ソートデータ登録
            this.form.customSortHeader1.SortDataTable(this.GridDataTbl);

            // グリッド初期化、設定
            this.form.customDataGridView1.IsBrowsePurpose = false;
            this.form.customDataGridView1.DataSource = this.GridDataTbl;
            this.form.customDataGridView1.IsBrowsePurpose = true;
            this.form.customDataGridView1.Refresh();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索結果集計後リストからグリッド表示データ作成
        /// </summary>
        /// <param name="groupList"></param>
        /// <returns></returns>
        private DataTable MakeGridDataTable(List<GRID_ROW_VALUE> groupList)
        {
            LogUtility.DebugMethodStart(groupList);

            DataTable tbl = new DataTable();
            tbl.Columns.Add(DEF_COLUMN_1_NAME, typeof(int));
            tbl.Columns.Add(DEF_COLUMN_2_NAME, typeof(int));
            tbl.Columns.Add(DEF_COLUMN_3_NAME, typeof(int));
            tbl.Columns.Add(DEF_COLUMN_4_NAME, typeof(string));
            tbl.Columns.Add(DEF_COLUMN_5_NAME, typeof(string));
            tbl.Columns.Add(DEF_COLUMN_6_NAME, typeof(string));
            foreach (var data in groupList)
            {
                tbl.Rows.Add(0, data.TEIKI_FLG, data.TANKA_FLG, data.TORIHIKISAKI_CD, data.TORIHIKISAKI_NAME_RYAKU, data.TORIHIKISAKI_FURIGANA);
            }

            LogUtility.DebugMethodEnd(tbl);
            return tbl;
        }

        #endregion

        #region 画面内コントロールロック・解除

        /// <summary>
        /// 画面内コントロールロック
        /// </summary>
        private void RockUIControl()
        {
            LogUtility.DebugMethodStart();

            // ロック開始
            this.form.txtBox_KyotenCd.Enabled = false;

            this.form.cmb_Shimebi.Enabled = false;
            this.form.dtp_KikanFrom.Enabled = false;
            this.form.dtp_KikanTo.Enabled = false;

            // 確定区分
            this.form.fixConditionValue.Enabled = false;
            this.form.fixedFlg.Enabled = false;
            this.form.unFixFlg.Enabled = false;

            //取引区分
            this.form.TorihikiKbnValue.Enabled = false;
            this.form.GenkinFlg.Enabled = false;
            this.form.KakeFlg.Enabled = false;

            //取引先
            this.form.TORIHIKISAKI_CD_custom.Enabled = false;
            this.form.TORIHIKISAKI_NAME_custom.Enabled = false;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面内コントロールロック解除
        /// </summary>
        private void UnRockUIControl()
        {
            LogUtility.DebugMethodStart();

            // ロック解除
            this.form.txtBox_KyotenCd.Enabled = true;

            // 締め日コンボ
            if (this.form.TorihikiKbnValue.Text == SearchDTOClass.TORIHIKI_KBN_VALUE_KAKE)
            {
                this.form.cmb_Shimebi.Enabled = true;
            }

            // 期間
            if (this.form.cmb_Shimebi.SelectedIndex == (int)ShimebiSelcetIndex.Shime00 ||
                this.form.cmb_Shimebi.SelectedIndex == (int)ShimebiSelcetIndex.Shime)
            {
                this.form.dtp_KikanFrom.Enabled = true;
                this.form.dtp_KikanTo.Enabled = true;
            }

            // 確定区分
            this.form.fixConditionValue.Enabled = true;
            this.form.fixedFlg.Enabled = true;
            this.form.unFixFlg.Enabled = true;

            //取引区分
            this.form.TorihikiKbnValue.Enabled = true;
            this.form.GenkinFlg.Enabled = true;
            this.form.KakeFlg.Enabled = true;

            //取引先
            this.form.TORIHIKISAKI_CD_custom.Enabled = true;
            this.form.TORIHIKISAKI_NAME_custom.Enabled = true;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region イベント処理：コンボボックス変更イベント

        /// <summary>
        /// コンボボックス変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_Shimebi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                CustomComboBox cmb = sender as CustomComboBox;

                if (cmb != null && cmb.Items[cmb.SelectedIndex] != null)
                {
                    string simebi = cmb.Items[cmb.SelectedIndex].ToString().Trim();
                    this.SetControlByShimebi(simebi);
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

        #endregion

        #region 締日コントロールによるUI設定

        /// <summary>
        /// 締日によるコントロール設定
        /// </summary>
        /// <param name="selectedValue"></param>
        private void SetControlByShimebi(string selectedValue)
        {
            LogUtility.DebugMethodStart(selectedValue);

            DateTime today = parentForm.sysDate;
            DateTime setDay;

            int shimebi = 0;
            if (int.TryParse(selectedValue, out shimebi))
            {
                // ユーザー任意
                if (shimebi == 0)
                {
                    // 有効化
                    this.form.dtp_KikanFrom.Enabled = true;
                    this.form.dtp_KikanTo.Enabled = true;
                }
                else
                {
                    // 前月初日から前月末日
                    if (shimebi == 31)
                    {
                        setDay = today.AddMonths(-1);
                        this.form.dtp_KikanFrom.Value = this.BeginOfMonth(setDay);
                        this.form.dtp_KikanTo.Value = this.EndOfMonth(setDay);
                    }

                    else if (shimebi >= today.Day)
                    {
                        // 前々月から前月
                        this.form.dtp_KikanFrom.Value = this.GetShimebi(today.AddMonths(-2), shimebi + 1);
                        this.form.dtp_KikanTo.Value = this.GetShimebi(today.AddMonths(-1), shimebi);
                    }

                    else
                    {
                        // 前月から当月
                        this.form.dtp_KikanFrom.Value = this.GetShimebi(today.AddMonths(-1), shimebi + 1);
                        this.form.dtp_KikanTo.Value = this.GetShimebi(today, shimebi);
                    }

                    // 無効化
                    this.form.dtp_KikanFrom.Enabled = false;
                    this.form.dtp_KikanTo.Enabled = false;
                }
            }
            else
            {
                // 有効化
                this.form.dtp_KikanFrom.Enabled = true;
                this.form.dtp_KikanTo.Enabled = true;
            }

            LogUtility.DebugMethodEnd(selectedValue);
        }

        /// <summary>
        /// 締日取得(From)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private DateTime GetShimebi(DateTime source, int shimebi)
        {
            return new DateTime(source.Year, source.Month, shimebi);
        }

        /// <summary>
        /// 締日取得(To)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private DateTime GetShimebiTo(DateTime source, int shimebi)
        {
            return new DateTime(source.Year, source.Month, shimebi);
        }

        /// <summary>
        /// 月初日を取得
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private DateTime BeginOfMonth(DateTime source)
        {
            return new DateTime(source.Year, source.Month, 1);
        }

        /// <summary>
        /// 月末日を取得  
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private DateTime EndOfMonth(DateTime source)
        {
            var day = DateTime.DaysInMonth(source.Year, source.Month);
            return new DateTime(source.Year, source.Month, day);
        }
        #endregion

        #region イベント処理：グリッドクリックイベント

        /// <summary>
        /// グリッドクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                CustomDataGridView grid = sender as CustomDataGridView;

                if (grid != null && e.RowIndex > -1 && this.dispDataRecord
                    && grid.Columns[e.ColumnIndex].Name.Equals(DEF_COLUMN_1_NAME))
                {
                    DataTable tbl = grid.DataSource as DataTable;

                    if ((int)tbl.DefaultView[e.RowIndex][0] == 1
                        && this.IsShimeZumi(tbl.DefaultView[e.RowIndex][DEF_COLUMN_4_NAME].ToString()))
                    {
                        // 完了メッセージ表示
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E150", "取引先CD");
                        tbl.DefaultView[e.RowIndex][0] = 0;
                    }
                    //else
                    //{
                    //    tbl.DefaultView[e.RowIndex][0] = (int)tbl.DefaultView[e.RowIndex][0] == 0 ? 1 : 0;
                    //}
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

        #endregion

        #region グリッドデータ全選択・全解除

        /// <summary>
        /// グリッドデータチェック解除
        /// </summary>
        private void GridCheckAll()
        {
            LogUtility.DebugMethodStart();

            if (this.dispDataRecord)
            {
                DataTable tbl = this.form.customDataGridView1.DataSource as DataTable;
                foreach (DataRow row in tbl.Rows)
                {
                    if (this.IsShimeZumi(row[DEF_COLUMN_4_NAME].ToString()))
                    {
                        row[DEF_COLUMN_1_NAME] = 0;
                    }
                    else
                    {
                        row[DEF_COLUMN_1_NAME] = 1;
                    }
                }
                this.form.customDataGridView1.Refresh();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// グリッドデータ全チェック解除
        /// </summary>
        private void GridUnCheckAll()
        {
            LogUtility.DebugMethodStart();

            if (this.dispDataRecord)
            {
                DataTable tbl = this.form.customDataGridView1.DataSource as DataTable;
                foreach (DataRow row in tbl.Rows)
                {
                    row[DEF_COLUMN_1_NAME] = 0;
                }
                this.form.customDataGridView1.Refresh();
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 締済みチェック
        /// <summary>
        /// 締済みかチェック
        /// </summary>
        /// <param name="torihikisakiCd">対象取引先CD</param>
        /// <returns>true:締済み、false:未締</returns>
        public bool IsShimeZumi(string torihikisakiCd)
        {
            LogUtility.DebugMethodStart();

            bool returnVal = false;

            if (this.ShimeDataList != null && this.ShimeDataList.Count > 0)
            {

                var targetList = this.ShimeDataList.Where(t => t.TORIHIKISAKI_CD.Equals(torihikisakiCd));

                foreach (var shimeData in targetList)
                {
                    if (!shimeData.SEIKYUU_NUMBER.IsNull
                        || !shimeData.SEISAN_NUMBER.IsNull)
                    {
                        returnVal = true;
                        break;
                    }
                }
            }

            LogUtility.DebugMethodEnd(returnVal);
            return returnVal;
        }
        #endregion

        #region 期間移動（前月・翌月）

        /// <summary>
        /// 前月移動
        /// </summary>
        private void KikanPriv()
        {
            LogUtility.DebugMethodStart();

            DateTime kikan;

            // from
            kikan = (DateTime)this.form.dtp_KikanFrom.Value;
            this.form.dtp_KikanFrom.Value = kikan.AddMonths(-1);

            // to
            kikan = (DateTime)this.form.dtp_KikanTo.Value;
            this.form.dtp_KikanTo.Value = this.form.cmb_Shimebi.SelectedIndex == (int)ShimebiSelcetIndex.Shime31 ? this.EndOfMonth(kikan.AddMonths(-1)) : kikan.AddMonths(-1);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 翌月移動
        /// </summary>
        private void KikanNext()
        {
            LogUtility.DebugMethodStart();

            DateTime kikan;

            // from
            kikan = (DateTime)this.form.dtp_KikanFrom.Value;
            this.form.dtp_KikanFrom.Value = kikan.AddMonths(1);

            // to
            kikan = (DateTime)this.form.dtp_KikanTo.Value;
            this.form.dtp_KikanTo.Value = this.form.cmb_Shimebi.SelectedIndex == (int)ShimebiSelcetIndex.Shime31 ? this.EndOfMonth(kikan.AddMonths(1)) : kikan.AddMonths(1);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 《《確定処理》》

        /// <summary>
        /// 確定処理実行
        /// </summary>
        private bool Execute()
        {
            LogUtility.DebugMethodStart();

            bool res = false;

            // 確定処理
            if (this.IsUnFixFlg())
            {
                // DB更新データ取得
                this.DbUpdateList = this.GetDbUpdateDataList();

                // 月次処理中チェック
                if (this.CheckGetsujiShoriChu())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E224", "実行");
                    return res;
                }

                // 月次処理ロックチェック
                if (this.CheckGetsujiShoriLock())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E223", "実行");
                    return res;
                }

                // 20141118 koukouei 締済期間チェックの追加 start
                // 締済期間チェック
                if (ShimeiDateCheck())
                {
                    // DB更新実行
                    res = this.DbExecute();
                }
                else
                {
                    return res;
                }
                // 20141118 koukouei 締済期間チェックの追加 end
            }
            else
            {
                // キャンセル処理
                // DB更新データ取得
                this.DbUpdateList = this.GetDbUpdateDataListForCancel();

                // 月次処理中チェック
                if (this.CheckGetsujiShoriChu())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E224", "実行");
                    return res;
                }

                // 月次処理ロックチェック
                if (this.CheckGetsujiShoriLock())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E223", "実行");
                    return res;
                }

                // DB更新実行
                res = this.DbExecuteForCancel();
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }
        #endregion

        #region DB更新実行

        /// <summary>
        /// DB更新実行
        /// </summary>
        private bool DbExecute()
        {
            LogUtility.DebugMethodStart();

            bool res = true;

            try
            {
                // 必要な
                M_CORP_INFO corpInfo = new M_CORP_INFO();
                var corpInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_CORP_INFODao>();
                var corpInfos = corpInfoDao.GetAllData();
                if (corpInfos != null && 0 < corpInfos.Length)
                {
                    corpInfo = corpInfos[0];
                }

                // 伝種区分 (売上／支払入力)
                Int16 densyuKbnCd = Int16.Parse(DENSHU_KBN.URIAGE_SHIHARAI.GetHashCode().ToString());

                // システムID、詳細システムID、売上／支払番号採番
                SqlInt64 systemId;
                SqlInt64 detailSystemId;
                SqlInt64 ur_sh_number = SqlInt64.Null;

                using (Transaction ts = new Transaction())
                {
                    // 同じレコードの複数回更新はNotSingleRowUpdatedRuntimeExceptionが発生する対処
                    List<T_TEIKI_JISSEKI_ENTRY> updateEntryList = new List<T_TEIKI_JISSEKI_ENTRY>();

                    // 更新ループ
                    foreach (DB_UPDATE_DATA u in DbUpdateList)
                    {
                        ur_sh_number = SqlInt64.Null;
                        if (u.InsertEntry != null)
                        {
                            // 採番（システムID、売上／支払番号）
                            systemId = this.dbAccesser.createSystemId(densyuKbnCd);
                            ur_sh_number = this.dbAccesser.createDenshuNumber(densyuKbnCd);

                            // 売上／支払(Entry)挿入
                            T_UR_SH_ENTRY use = u.InsertEntry;
                            use.SYSTEM_ID = systemId;
                            use.UR_SH_NUMBER = ur_sh_number;
                            use.DAINOU_FLG = SqlBoolean.False;
                            use.DELETE_FLG = SqlBoolean.False;

                            // 売上/支払伝票の日付を設定
                            // 売上／支払(Detail)挿入時にもDetailでループさせているが、Insertのタイミングから
                            // ここでもループさせる
                            SqlDateTime tempUriageDate = SqlDateTime.Null;
                            SqlDateTime tempShiharaiDate = SqlDateTime.Null;
                            foreach (T_UR_SH_DETAIL usd in u.InsertDetailList)
                            {
                                DateTime tempUrShDate;  // Dateプロパティを使いたいがためDateTime型で宣言
                                if (!usd.URIAGESHIHARAI_DATE.IsNull
                                    && !usd.DENPYOU_KBN_CD.IsNull)
                                {
                                    tempUrShDate = (DateTime)usd.URIAGESHIHARAI_DATE;

                                    if (usd.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
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
                                    else if (usd.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI)
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

                            use.DENPYOU_DATE = tempUriageDate;
                            if (use.DENPYOU_DATE.IsNull)
                            {
                                use.DENPYOU_DATE = tempShiharaiDate;
                            }
                            use.URIAGE_DATE = tempUriageDate;
                            use.SHIHARAI_DATE = tempShiharaiDate;

                            // 日、年連番の採番
                            if (!use.DENPYOU_DATE.IsNull && !use.KYOTEN_CD.IsNull)
                            {
                                var dateNumber = this.InsertOrUpdateOfNumberDayEntity((DateTime)use.DENPYOU_DATE, (short)use.KYOTEN_CD);
                                var yearNumber = this.InsertOrUpdateOfNumberYearEntity(CorpInfoUtility.GetCurrentYear((DateTime)use.DENPYOU_DATE, (short)corpInfo.KISHU_MONTH), (short)use.KYOTEN_CD);

                                if (0 < dateNumber)
                                {
                                    use.DATE_NUMBER = dateNumber;
                                }

                                if (0 < yearNumber)
                                {
                                    use.YEAR_NUMBER = yearNumber;
                                }
                            }

                            this.daoUrShEntry.Insert(use);

                            // 売上／支払(Detail)挿入
                            foreach (T_UR_SH_DETAIL usd in u.InsertDetailList)
                            {
                                // 採番(明細システムID) ※再度採番する形でよい
                                detailSystemId = this.dbAccesser.createSystemId(densyuKbnCd);

                                usd.SYSTEM_ID = systemId;
                                usd.SEQ = use.SEQ;
                                usd.DETAIL_SYSTEM_ID = detailSystemId;
                                usd.UR_SH_NUMBER = ur_sh_number;
                                this.daoUrShDetail.Insert(usd);
                            }
                        }

                        // 定期実績(Entry)更新対象登録
                        updateEntryList.AddRange(u.UpdateEntryList);

                        // 定期実績(Detail)更新
                        foreach (T_TEIKI_JISSEKI_DETAIL tjd in u.UpdateDetailList)
                        {
                            tjd.UR_SH_NUMBER = ur_sh_number;
                            tjd.KAKUTEI_FLG = true;
                            this.daoTeikiJissekiDetail.Update(tjd);
                        }
                    }

                    // 更新日付の更新
                    // 同じレコードの複数回更新はNotSingleRowUpdatedRuntimeExceptionが発生する対処
                    foreach (var g in updateEntryList.GroupBy(t => new { t.SYSTEM_ID, t.SEQ }).ToList())
                    {
                        T_TEIKI_JISSEKI_ENTRY tje = updateEntryList.Where(t => t.SYSTEM_ID.Equals(g.Key.SYSTEM_ID)
                                                                        && t.SEQ.Equals(g.Key.SEQ)
                                                                        ).FirstOrDefault();
                        this.daoTeikiJissekiEntry.Update(tje);
                    }

                    // コミット
                    ts.Commit();
                }

            }
            catch (Exception ex)
            {
                res = false;
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E252");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        /// <summary>
        /// DB更新(キャンセル用)
        /// </summary>
        /// <returns></returns>
        private bool DbExecuteForCancel()
        {
            LogUtility.DebugMethodStart();
            bool returnVal = true;

            try
            {
                using (Transaction ts = new Transaction())
                {
                    // 同じレコードの複数回更新はNotSingleRowUpdatedRuntimeExceptionが発生する対処
                    List<T_TEIKI_JISSEKI_ENTRY> updateEntryList = new List<T_TEIKI_JISSEKI_ENTRY>();

                    // 更新ループ
                    foreach (DB_UPDATE_DATA u in DbUpdateList)
                    {
                        // 売上／支払(Entry)挿入
                        T_UR_SH_ENTRY use = u.DeleteUrShEntry;

                        // 売上/支払入力で情報を削除されている可能性がある場合はスキップ
                        if (use != null)
                        {
                            this.daoUrShEntry.Update(use);
                            /// 20141119 Houkakou 「更新日、登録日の見直し」　start
                            use.SEQ = use.SEQ + 1;
                            this.daoUrShEntry.Insert(use);
                            /// 20141119 Houkakou 「更新日、登録日の見直し」　end
                        }

                        // 定期実績(Entry)更新対象登録
                        updateEntryList.AddRange(u.UpdateEntryList);

                        // 定期実績(Detail)更新
                        foreach (T_TEIKI_JISSEKI_DETAIL tjd in u.UpdateDetailList)
                        {
                            tjd.UR_SH_NUMBER = SqlInt64.Null;
                            tjd.KAKUTEI_FLG = false;
                            // UR_SH_NUMBERを設定しないことでNullにする
                            this.daoTeikiJissekiDetail.Update(tjd);
                        }
                    }

                    // 更新日付の更新
                    // 同じレコードの複数回更新はNotSingleRowUpdatedRuntimeExceptionが発生する対処
                    foreach (var g in updateEntryList.GroupBy(t => new { t.SYSTEM_ID, t.SEQ }).ToList())
                    {
                        T_TEIKI_JISSEKI_ENTRY tje = updateEntryList.Where(t => t.SYSTEM_ID.Equals(g.Key.SYSTEM_ID)
                                                                        && t.SEQ.Equals(g.Key.SEQ)
                                                                        ).FirstOrDefault();
                        this.daoTeikiJissekiEntry.Update(tje);
                    }

                    // コミット
                    ts.Commit();
                }

            }
            catch (Exception ex)
            {
                returnVal = false;
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E252");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
            }

            LogUtility.DebugMethodEnd(returnVal);
            return returnVal;
        }
        #endregion

        #region DB更新データ取得

        /// <summary>
        /// DB更新データ取得
        /// </summary>
        /// <returns></returns>
        private List<DB_UPDATE_DATA> GetDbUpdateDataList()
        {
            LogUtility.DebugMethodStart();

            // 挿入データリスト
            List<DB_UPDATE_DATA> dbUpdateDataList = new List<DB_UPDATE_DATA>();

            // チェックされた取引先CDを取得
            List<string> torihikisakiCdList = new List<string>();
            foreach (DataRow gridRow in this.GridDataTbl.Rows)
            {
                if ((int)gridRow[DEF_COLUMN_1_NAME] == 1)
                {
                    string torihikisakiCd = gridRow[DEF_COLUMN_4_NAME].ToString().Trim();
                    torihikisakiCdList.Add(torihikisakiCd);
                }
            }

            // チェックされた取引先CDが対象
            foreach (string torihikisakiCd in torihikisakiCdList.OrderBy(t => t))
            {
                // 取引先CDに属するデータを取得
                List<T_SELECT_RESULT> insertDataList = this.ResultGroupList.FirstOrDefault(t =>
                                                                t.TORIHIKISAKI_CD.Equals(torihikisakiCd)
                                                            ).ResultList;

                // 売上／支払入力レコード単位(契約区分、月極区分(1or2)、業者CD、現場CD、荷降業者CD、荷降現場CD)に分ける
                var EntryGroupList = insertDataList.GroupBy(t => new { t.KEIYAKU_KBN, t.TSUKIGIME_KBN, t.GYOUSHA_CD, t.GENBA_CD, t.NIOROSHI_GYOUSHA_CD, t.NIOROSHI_GENBA_CD })
                                            .OrderBy(t => t.Key.KEIYAKU_KBN)
                                            .ThenBy(t => t.Key.TSUKIGIME_KBN)
                                            .ThenBy(t => t.Key.GYOUSHA_CD)
                                            .ThenBy(t => t.Key.GENBA_CD)
                                            .ThenBy(t => t.Key.NIOROSHI_GYOUSHA_CD)
                                            .ThenBy(t => t.Key.NIOROSHI_GENBA_CD); ;

                // 売上／支払入力レコード単位でループ
                foreach (var entryKey in EntryGroupList)
                {
                    // 挿入データリスト
                    DB_UPDATE_DATA dbUpdateData = new DB_UPDATE_DATA();

                    // 売上／支払入力レコードに使用するデータ
                    var entryDataList = insertDataList.Where(t => t.KEIYAKU_KBN.Equals(entryKey.Key.KEIYAKU_KBN)
                                                                && t.TSUKIGIME_KBN.Equals(entryKey.Key.TSUKIGIME_KBN)
                                                                && t.GYOUSHA_CD.Equals(entryKey.Key.GYOUSHA_CD)
                                                                && t.GENBA_CD.Equals(entryKey.Key.GENBA_CD)
                                                                && Convert.ToString((Object)t.NIOROSHI_GYOUSHA_CD).Equals(Convert.ToString((Object)entryKey.Key.NIOROSHI_GYOUSHA_CD))
                                                                && Convert.ToString((Object)t.NIOROSHI_GENBA_CD).Equals(Convert.ToString((Object)entryKey.Key.NIOROSHI_GENBA_CD))
                                                        )
                                                        .OrderBy(t => t.KEIYAKU_KBN)
                                                        .ThenBy(t => t.TSUKIGIME_KBN)
                                                        .ThenBy(t => t.GYOUSHA_CD)
                                                        .ThenBy(t => t.GENBA_CD)
                                                        .ThenBy(t => t.HINMEI_CD)
                                                        .ThenBy(t => t.UNIT_CD)
                                                        .ThenBy(t => t.SYSTEM_ID)
                                                        .ThenBy(t => t.SEQ)
                                                        .ThenBy(t => t.DETAIL_SYSTEM_ID)
                                                        .ToList();

                    // 契約区分が定期(1)の場合
                    if (entryKey.Key.KEIYAKU_KBN == 1)
                    {
                        var EntryGroupListForTeiki = entryDataList.Where(t => t.KEIYAKU_KBN.Value == entryKey.Key.KEIYAKU_KBN.Value).ToList();

                        var teikiList = this.GetInsertListTeiki(EntryGroupListForTeiki);

                        // 挿入対象として登録
                        dbUpdateDataList.AddRange(teikiList);
                    }
                    // 月極区分が伝票(1)の場合
                    else if (entryKey.Key.TSUKIGIME_KBN == 1)
                    {
                        // 伝票毎の場合は、定期実績番号ごとにグルーピングしてデータを作成
                        var EntryGroupListForDenpyouTani = entryDataList.GroupBy(t => new { t.TEIKI_JISSEKI_NUMBER }).OrderBy(t => t.Key.TEIKI_JISSEKI_NUMBER);

                        foreach (var entryKeyForDenpyouTani in EntryGroupListForDenpyouTani)
                        {
                            dbUpdateData = new DB_UPDATE_DATA();

                            var entryDateListForDenpyouTani = entryDataList.Where(t => t.TEIKI_JISSEKI_NUMBER == entryKeyForDenpyouTani.Key.TEIKI_JISSEKI_NUMBER).ToList();

                            // レコード数分、明細レコードを作成
                            dbUpdateData.InsertDetailList = this.GetInsertDetailListGassan(entryDateListForDenpyouTani, entryKey.Key.TSUKIGIME_KBN);

                            // 日付を作業日にする
                            dbUpdateData.InsertDetailList.ForEach(i => i.URIAGESHIHARAI_DATE = entryDateListForDenpyouTani.FirstOrDefault().SAGYOU_DATE);

                            // 入力(Entry)レコードを作成
                            dbUpdateData.InsertEntry = this.GetInsertEntry(entryDateListForDenpyouTani, dbUpdateData.InsertDetailList, entryKey.Key.TSUKIGIME_KBN);

                            // 更新レコードを作成(定期実績明細)
                            dbUpdateData.UpdateDetailList = this.GetUpdateDetailList(entryDateListForDenpyouTani);

                            // 更新レコードを作成(定期実績入力)
                            dbUpdateData.UpdateEntryList = this.GetUpdateEntryList(entryDateListForDenpyouTani);

                            // 挿入対象として登録
                            dbUpdateDataList.Add(dbUpdateData);
                        }
                    }
                    // 月極区分が合算(2)の場合
                    else if (entryKey.Key.TSUKIGIME_KBN == 2)
                    {
                        // 品名CD、単位CDのブレイク単位で、明細レコードを作成
                        dbUpdateData.InsertDetailList = this.GetInsertDetailListGassan(entryDataList, entryKey.Key.TSUKIGIME_KBN);
                        // 入力(Entry)レコードを作成
                        dbUpdateData.InsertEntry = this.GetInsertEntry(entryDataList, dbUpdateData.InsertDetailList, entryKey.Key.TSUKIGIME_KBN);

                        // 更新レコードを作成(定期実績明細)
                        dbUpdateData.UpdateDetailList = this.GetUpdateDetailList(entryDataList);

                        // 更新レコードを作成(定期実績入力)
                        dbUpdateData.UpdateEntryList = this.GetUpdateEntryList(entryDataList);

                        // 挿入対象として登録
                        dbUpdateDataList.Add(dbUpdateData);
                    }
                }

            }

            LogUtility.DebugMethodEnd(dbUpdateDataList);
            return dbUpdateDataList;
        }

        /// <summary>
        /// DB更新データ取得(キャンセル処理用)
        /// </summary>
        /// <returns></returns>
        private List<DB_UPDATE_DATA> GetDbUpdateDataListForCancel()
        {
            LogUtility.DebugMethodStart();

            // 挿入データリスト
            List<DB_UPDATE_DATA> dbUpdateDataList = new List<DB_UPDATE_DATA>();

            // チェックされた取引先CDを取得
            List<string> torihikisakiCdList = this.GridDataTbl.Rows.Cast<DataRow>()
                                                    .Where(w => (int)w[DEF_COLUMN_1_NAME] == 1)
                                                    .Select(s => s[DEF_COLUMN_4_NAME].ToString()).ToList();

            // チェックされた取引先CDが対象
            foreach (string torihikisakiCd in torihikisakiCdList.OrderBy(t => t))
            {
                // 取引先CDに属するデータを取得
                List<T_SELECT_RESULT> insertDataList = this.ResultGroupList.FirstOrDefault(t =>
                                                                t.TORIHIKISAKI_CD.Equals(torihikisakiCd)
                                                            ).ResultList;

                // 売上／支払入力レコード単位(業者CD、現場CD)に分ける
                var EntryGroupList = insertDataList.GroupBy(t => new { t.GYOUSHA_CD, t.GENBA_CD, t.UR_SH_NUMBER })
                                            .OrderBy(t => t.Key.GYOUSHA_CD)
                                            .ThenBy(t => t.Key.GENBA_CD)
                                            .ThenBy(t => t.Key.UR_SH_NUMBER);

                // 売上／支払入力レコード単位でループ
                foreach (var entryKey in EntryGroupList)
                {
                    // 挿入データリスト
                    DB_UPDATE_DATA dbUpdateData = new DB_UPDATE_DATA();

                    // 売上／支払入力レコードに使用するデータ
                    var entryDataList = insertDataList.Where(t => t.GYOUSHA_CD.Equals(entryKey.Key.GYOUSHA_CD)
                                                                && t.GENBA_CD.Equals(entryKey.Key.GENBA_CD)
                                                                && t.UR_SH_NUMBER.Equals(entryKey.Key.UR_SH_NUMBER)
                                                        )
                                                        .OrderBy(t => t.TSUKIGIME_KBN)
                                                        .ThenBy(t => t.GYOUSHA_CD)
                                                        .ThenBy(t => t.GENBA_CD)
                                                        .ThenBy(t => t.HINMEI_CD)
                                                        .ThenBy(t => t.UNIT_CD)
                                                        .ThenBy(t => t.SYSTEM_ID)
                                                        .ThenBy(t => t.SEQ)
                                                        .ThenBy(t => t.DETAIL_SYSTEM_ID)
                                                        .ToList();

                    if (entryDataList.Any())
                    {
                        if (entryDataList.Any(c => !c.UR_SH_NUMBER.IsNull))
                        {
                            // 削除レコードを作成(売上/支払入力)
                            var selectResult = entryDataList.Where(e => !e.UR_SH_NUMBER.IsNull).First();     // 売上支払番号でGroupByしているので最初の行だけでOK
                            dbUpdateData.DeleteUrShEntry = this.daoUrShDate.GetDataByNumber(selectResult.UR_SH_NUMBER.ToString());
                            // 売上/支払入力で伝票を削除されている場合は処理をスキップ
                            if (dbUpdateData.DeleteUrShEntry != null)
                            {
                                dbUpdateData.DeleteUrShEntry.DELETE_FLG = SqlBoolean.True;
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                //dbUpdateData.DeleteUrShEntry.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                                dbUpdateData.DeleteUrShEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                dbUpdateData.DeleteUrShEntry.UPDATE_PC = SystemInformation.ComputerName;
                                dbUpdateData.DeleteUrShEntry.UPDATE_USER = SystemProperty.UserName;
                            }
                        }

                        // 更新レコードを作成(定期実績明細)
                        dbUpdateData.UpdateDetailList = this.GetUpdateDetailList(entryDataList);

                        // 更新レコードを作成(定期実績入力)
                        dbUpdateData.UpdateEntryList = this.GetUpdateEntryList(entryDataList);

                        // 挿入対象として登録
                        dbUpdateDataList.Add(dbUpdateData);
                    }
                }

            }

            LogUtility.DebugMethodEnd(dbUpdateDataList);
            return dbUpdateDataList;
        }

        #endregion

        #region DB挿入レコード取得(売上／支払：Detail)

        /// <summary>
        /// 定期の場合の明細レコード生成
        /// </summary>
        /// <param name="selectList"></param>
        /// <returns></returns>
        private List<DB_UPDATE_DATA> GetInsertListTeiki(List<T_SELECT_RESULT> selectList)
        {
            LogUtility.DebugMethodStart(selectList);

            List<DB_UPDATE_DATA> retList = new List<DB_UPDATE_DATA>();
            Dictionary<string, List<T_SELECT_RESULT>> list = new Dictionary<string,List<T_SELECT_RESULT>>();

            string key;
            string format = "{0}_{1}_{2}";
            foreach (T_SELECT_RESULT selectRow in selectList)
            {
                if (selectRow.CHOUKA_LIMIT_AMOUNT.IsNull)
                {
                    continue;
                }
                key = string.Format(format, selectRow.GYOUSHA_CD, selectRow.GENBA_CD, selectRow.TSUKI_HINMEI_CD);
                if (!list.ContainsKey(key))
                {
                   list.Add(key, new List<T_SELECT_RESULT>());
                }
                list[key].Add(selectRow);
            }

            foreach (string keys in list.Keys)
            {
                DB_UPDATE_DATA dbUpdateData = new DB_UPDATE_DATA();
                dbUpdateData.InsertDetailList = new List<T_UR_SH_DETAIL>();
                var teikiList = selectList.Where(t => t.GYOUSHA_CD.Equals(keys.Split('_')[0])
                                                 && t.GENBA_CD.Equals(keys.Split('_')[1])
                                                 && t.TSUKI_HINMEI_CD.Equals(keys.Split('_')[2])).ToList();
                decimal limit = list[keys][0].CHOUKA_LIMIT_AMOUNT.Value;
                decimal total = 0;
                foreach (T_SELECT_RESULT selectRow in list[keys])
                {
                    total += selectRow.SUURYOU.Value;
                }
                if (total > limit)
                {
                    SqlDecimal suuryou = list[keys][0].SUURYOU;
                    string hinmeiCd = list[keys][0].HINMEI_CD;
                    string hinmeiName = list[keys][0].HINMEI_NAME_RYAKU;
                    list[keys][0].SUURYOU = total - limit;
                    list[keys][0].HINMEI_CD = keys.Split('_')[2];
                    list[keys][0].HINMEI_NAME_RYAKU = list[keys][0].CHOUKA_HINMEI_NAME;
                    T_UR_SH_DETAIL detail = this.SetDatailData(list[keys][0]);
                    list[keys][0].SUURYOU = suuryou;
                    list[keys][0].HINMEI_CD = hinmeiCd;
                    list[keys][0].HINMEI_NAME_RYAKU = hinmeiName;
                    detail.ROW_NO = 1;
                    dbUpdateData.InsertDetailList.Add(detail);
                    dbUpdateData.InsertEntry = GetInsertEntry(teikiList, dbUpdateData.InsertDetailList, 0);
                }

                // 更新レコードを作成(定期実績明細)
                dbUpdateData.UpdateDetailList = this.GetUpdateDetailList(teikiList);

                // 更新レコードを作成(定期実績入力)
                dbUpdateData.UpdateEntryList = this.GetUpdateEntryList(teikiList);

                retList.Add(dbUpdateData);
            }

            LogUtility.DebugMethodEnd(retList);
            return retList;
        }

        /// <summary>
        /// 月極区分=伝票(1)の場合の明細レコード生成
        /// </summary>
        /// <param name="selectList"></param>
        /// <returns></returns>
        private List<T_UR_SH_DETAIL> GetInsertDetailListDenpyo(List<T_SELECT_RESULT> selectList)
        {
            LogUtility.DebugMethodStart(selectList);

            List<T_UR_SH_DETAIL> res = new List<T_UR_SH_DETAIL>();

            // １明細レコードは検索結果１レコード単位
            int row_no = 0;
            foreach (T_SELECT_RESULT selectRow in selectList)
            {
                T_UR_SH_DETAIL detail = this.SetDatailData(selectRow);
                detail.ROW_NO = SqlInt16.Parse((++row_no).ToString());
                res.Add(detail);
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        /// <summary>
        /// 月極区分=合算(2)の場合の明細レコード生成
        /// </summary>
        /// <param name="selectList"></param>
        /// <param name="tsukigimeKbn">月極区分</param>
        /// <returns></returns>
        private List<T_UR_SH_DETAIL> GetInsertDetailListGassan(List<T_SELECT_RESULT> selectList, int tsukigimeKbn)
        {
            LogUtility.DebugMethodStart(selectList, tsukigimeKbn);

            List<T_UR_SH_DETAIL> res = new List<T_UR_SH_DETAIL>();

            // １明細レコードは品名CD、単位CDブレイク単位
            var gassanGroup = selectList.GroupBy(t => new { t.HINMEI_CD, t.UNIT_CD, t.DENPYOU_KBN_CD })
                                        .OrderBy(t => t.Key.HINMEI_CD)
                                        .ThenBy(t => t.Key.UNIT_CD)
                                        .ThenBy(t => t.Key.DENPYOU_KBN_CD)
                                        .ToList();

            var seikyuuUtility = new SeiKyuuUtility();
            int row_no = 0;
            foreach (var g in gassanGroup)
            {
                // 品名CD、単位CDの切り替わりで明細レコードを作成する。
                var gassanList = selectList.Where(t => t.HINMEI_CD.Equals(g.Key.HINMEI_CD)
                                                    && t.UNIT_CD.Equals(g.Key.UNIT_CD)
                                                    && t.DENPYOU_KBN_CD.Equals(g.Key.DENPYOU_KBN_CD)
                                            )
                                            .OrderBy(t => t.HINMEI_CD)
                                            .ThenBy(t => t.UNIT_CD)
                                            .ThenBy(t => t.DENPYOU_KBN_CD)
                                            .ToList();

                // 検索結果レコード単位で明細レコードを作製
                List<T_UR_SH_DETAIL> detailList = new List<T_UR_SH_DETAIL>();
                foreach (var v in gassanList)
                {
                    T_UR_SH_DETAIL detail = this.SetDatailData(v);

                    // 集計単位：合算の場合、伝票日付を締日に設定
                    if (this.whereDto.SHIMEBI != null
                        && this.whereDto.SHIMEBI != 0)
                    {
                        var uriageShiharaiHiduke = seikyuuUtility.GetNearSeikyuDate(this.whereDto.KIKAN_DATE_TO.Value, new short[1] { (short)this.whereDto.SHIMEBI });
                        detail.URIAGESHIHARAI_DATE = uriageShiharaiHiduke.Date;
                    }
                    else
                    {
                        detail.URIAGESHIHARAI_DATE = this.whereDto.KIKAN_DATE_TO.Value.Date;
                    }

                    detailList.Add(detail);
                }

                // 売上支払日付時点での消費税を取得する。
                SqlDecimal shotokuzeiRate = SqlDecimal.Null;
                M_SHOUHIZEI shouhizei = this.shouhizeiDao.GetDataByDate(DateTime.Parse(detailList[0].URIAGESHIHARAI_DATE.ToString()));
                if (shouhizei != null)
                {
                    shotokuzeiRate = shouhizei.SHOUHIZEI_RATE;
                }

                if (tsukigimeKbn == 1)
                {
                    // 明細レコードを集計
                    T_UR_SH_DETAIL syukeiDetail = detailList.FirstOrDefault();
                    syukeiDetail.SUURYOU = detailList.Sum(t => t.SUURYOU.Value);

                    //syukeiDetail.KINGAKU = detailList.Sum(t => t.KINGAKU.Value);
                    SqlDecimal kingaku = 0;
                    if (!syukeiDetail.SUURYOU.IsNull && !syukeiDetail.TANKA.IsNull)
                    {
                        if (!syukeiDetail.DENPYOU_KBN_CD.IsNull)
                        {
                            decimal tanka = (decimal)syukeiDetail.TANKA;
                            //decimal suuryou = (decimal)syukeiDetail.SUURYOU.ToSqlDecimal();
                            decimal suuryou = Convert.ToDecimal(syukeiDetail.SUURYOU.Value);
                            short kingakuHasuuCd = 0;
                            var rowList = this.ResultList.Where(t => t.TORIHIKISAKI_CD == selectList[0].TORIHIKISAKI_CD).ToList();
                            var result = rowList.FirstOrDefault();

                            if ((short)syukeiDetail.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
                            {
                                kingakuHasuuCd = (short)result.KINGAKU_HASUU_CD;
                            }
                            else
                            {
                                kingakuHasuuCd = (short)result.SHIHARAI_KINGAKU_HASUU_CD;
                            }
                            kingaku = CommonCalc.FractionCalc(suuryou * tanka, kingakuHasuuCd);
                        }
                        else
                        {
                            kingaku = syukeiDetail.SUURYOU * syukeiDetail.TANKA;
                        }
                    }

                    syukeiDetail.KINGAKU = kingaku;
                    syukeiDetail.HINMEI_KINGAKU = kingaku;
                    syukeiDetail.TAX_SOTO = detailList.Sum(t => t.TAX_SOTO.Value);
                    syukeiDetail.TAX_UCHI = detailList.Sum(t => t.TAX_UCHI.Value);
                    syukeiDetail.HINMEI_TAX_SOTO = detailList.Sum(t => t.HINMEI_TAX_SOTO.Value);
                    syukeiDetail.HINMEI_TAX_UCHI = detailList.Sum(t => t.HINMEI_TAX_UCHI.Value);

                    /* 集計した結果にて税計算を行う 2013/12/25 start */
                    T_SELECT_RESULT selectOne = gassanList.FirstOrDefault();
                    SqlInt16 tempTaxHasuuCd = syukeiDetail.DENPYOU_KBN_CD == CommonConst.DENPYOU_KBN_SHIHARAI ? selectOne.SHIHARAI_TAX_HASUU_CD : selectOne.TAX_HASUU_CD;
                    SqlInt16 tempZeiKbnCd = syukeiDetail.DENPYOU_KBN_CD == CommonConst.DENPYOU_KBN_SHIHARAI ? selectOne.SHIHARAI_ZEI_KBN_CD : selectOne.ZEI_KBN_CD;  //20220203 GODA ADD
                    if (syukeiDetail.HINMEI_ZEI_KBN_CD.IsNull)
                    {
                        syukeiDetail.HINMEI_KINGAKU = 0;
                        syukeiDetail.TAX_SOTO = 0;
                        syukeiDetail.TAX_UCHI = 0;
                        syukeiDetail.HINMEI_TAX_SOTO = 0;
                        syukeiDetail.HINMEI_TAX_UCHI = 0;

                        // 税区分判断
                        ZeiKbnCd zeiKbnCd = this.GetZeiKbnCd(selectOne.ZEI_KBN_CD);
                        if ((short)selectOne.DENPYOU_KBN_CD != SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
                        {
                            zeiKbnCd = this.GetZeiKbnCd(selectOne.SHIHARAI_ZEI_KBN_CD);
                        }

                        // 外税
                        if (zeiKbnCd == ZeiKbnCd.SotoZei)
                        {
                            syukeiDetail.TAX_SOTO = this.GetSotoZei(syukeiDetail.KINGAKU, shotokuzeiRate, tempTaxHasuuCd);
                        }
                        // 内税
                        else if (zeiKbnCd == ZeiKbnCd.UchiZei)
                        {
                            syukeiDetail.TAX_UCHI = this.GetUchiZei(syukeiDetail.KINGAKU, shotokuzeiRate, tempTaxHasuuCd);
                        }
                    }
                    else
                    {
                        syukeiDetail.KINGAKU = 0;
                        syukeiDetail.TAX_SOTO = 0;
                        syukeiDetail.TAX_UCHI = 0;
                        syukeiDetail.HINMEI_TAX_SOTO = 0;
                        syukeiDetail.HINMEI_TAX_UCHI = 0;

                        // 税区分判断
                        //ZeiKbnCd zeiKbnCd = this.GetZeiKbnCd(selectOne.ZEI_KBN_CD);
                        ZeiKbnCd zeiKbnCd = this.GetZeiKbnCd(tempZeiKbnCd);              //20220203 GODA ADD

                        // 外税
                        if (zeiKbnCd == ZeiKbnCd.SotoZei)
                        {
                            syukeiDetail.HINMEI_TAX_SOTO = this.GetSotoZei(syukeiDetail.HINMEI_KINGAKU, shotokuzeiRate, tempTaxHasuuCd);
                        }
                        // 内税
                        else if (zeiKbnCd == ZeiKbnCd.UchiZei)
                        {
                            syukeiDetail.HINMEI_TAX_UCHI = this.GetUchiZei(syukeiDetail.HINMEI_KINGAKU, shotokuzeiRate, tempTaxHasuuCd);
                        }
                    }
                    /* 集計した結果にて税計算を行う 2013/12/25 end */


                    syukeiDetail.ROW_NO = SqlInt16.Parse((++row_no).ToString());

                    res.Add(syukeiDetail);
                }
                else if (tsukigimeKbn == 2)
                {
                    // 単価が異なる場合は別行で生成する
                    var tankaGroup = detailList.GroupBy(gr => new { gr.TANKA });
                    foreach (var tempTankaGroup in tankaGroup)
                    {

                        var temp = tempTankaGroup.Where(wh => (bool)(wh.TANKA == tempTankaGroup.Key.TANKA));

                        // 明細レコードを集計
                        T_UR_SH_DETAIL syukeiDetail = temp.FirstOrDefault();
                        syukeiDetail.SUURYOU = temp.Sum(t => t.SUURYOU.Value);

                        //syukeiDetail.KINGAKU = temp.Sum(t => t.KINGAKU.Value);
                        SqlDecimal kingaku = 0;
                        if (!syukeiDetail.SUURYOU.IsNull && !syukeiDetail.TANKA.IsNull)
                        {
                            if (!syukeiDetail.DENPYOU_KBN_CD.IsNull)
                            {
                                decimal tanka = (decimal)syukeiDetail.TANKA;
                                //decimal suuryou = (decimal)syukeiDetail.SUURYOU.ToSqlDecimal();
                                decimal suuryou = Convert.ToDecimal(syukeiDetail.SUURYOU.Value);
                                short kingakuHasuuCd = 0;
                                var rowList = this.ResultList.Where(t => t.TORIHIKISAKI_CD == selectList[0].TORIHIKISAKI_CD).ToList();
                                var result = rowList.FirstOrDefault();

                                if ((short)syukeiDetail.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
                                {
                                    kingakuHasuuCd = (short)result.KINGAKU_HASUU_CD;
                                }
                                else
                                {
                                    kingakuHasuuCd = (short)result.SHIHARAI_KINGAKU_HASUU_CD;
                                }
                                kingaku = CommonCalc.FractionCalc(suuryou * tanka, kingakuHasuuCd);
                            }
                            else
                            {
                                kingaku = syukeiDetail.SUURYOU * syukeiDetail.TANKA;
                            }
                        }
                        syukeiDetail.KINGAKU = kingaku;
                        syukeiDetail.HINMEI_KINGAKU = kingaku;
                        syukeiDetail.TAX_SOTO = temp.Sum(t => t.TAX_SOTO.Value);
                        syukeiDetail.TAX_UCHI = temp.Sum(t => t.TAX_UCHI.Value);
                        syukeiDetail.HINMEI_TAX_SOTO = temp.Sum(t => t.HINMEI_TAX_SOTO.Value);
                        syukeiDetail.HINMEI_TAX_UCHI = temp.Sum(t => t.HINMEI_TAX_UCHI.Value);

                        /* 集計した結果にて税計算を行う 2013/12/25 start */
                        T_SELECT_RESULT selectOne = gassanList.FirstOrDefault();
                        SqlInt16 tempTaxHasuuCd = syukeiDetail.DENPYOU_KBN_CD == CommonConst.DENPYOU_KBN_SHIHARAI ? selectOne.SHIHARAI_TAX_HASUU_CD : selectOne.TAX_HASUU_CD;
                        SqlInt16 tempZeiKbnCd = syukeiDetail.DENPYOU_KBN_CD == CommonConst.DENPYOU_KBN_SHIHARAI ? selectOne.SHIHARAI_ZEI_KBN_CD : selectOne.ZEI_KBN_CD;  //20220203 GODA ADD
                        if (syukeiDetail.HINMEI_ZEI_KBN_CD.IsNull)
                        {
                            syukeiDetail.HINMEI_KINGAKU = 0;
                            syukeiDetail.TAX_SOTO = 0;
                            syukeiDetail.TAX_UCHI = 0;
                            syukeiDetail.HINMEI_TAX_SOTO = 0;
                            syukeiDetail.HINMEI_TAX_UCHI = 0;

                            // 税区分判断
                            ZeiKbnCd zeiKbnCd = this.GetZeiKbnCd(selectOne.ZEI_KBN_CD);
                            if ((short)selectOne.DENPYOU_KBN_CD != SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
                            {
                                zeiKbnCd = this.GetZeiKbnCd(selectOne.SHIHARAI_ZEI_KBN_CD);
                            }

                            // 外税
                            if (zeiKbnCd == ZeiKbnCd.SotoZei)
                            {
                                syukeiDetail.TAX_SOTO = this.GetSotoZei(syukeiDetail.KINGAKU, shotokuzeiRate, tempTaxHasuuCd);
                            }
                            // 内税
                            else if (zeiKbnCd == ZeiKbnCd.UchiZei)
                            {
                                syukeiDetail.TAX_UCHI = this.GetUchiZei(syukeiDetail.KINGAKU, shotokuzeiRate, tempTaxHasuuCd);
                            }
                        }
                        else
                        {
                            syukeiDetail.KINGAKU = 0;
                            syukeiDetail.TAX_SOTO = 0;
                            syukeiDetail.TAX_UCHI = 0;
                            syukeiDetail.HINMEI_TAX_SOTO = 0;
                            syukeiDetail.HINMEI_TAX_UCHI = 0;

                            // 税区分判断
                            //ZeiKbnCd zeiKbnCd = this.GetZeiKbnCd(selectOne.ZEI_KBN_CD);
                            ZeiKbnCd zeiKbnCd = this.GetZeiKbnCd(tempZeiKbnCd);              //20220203 GODA ADD

                            // 外税
                            if (zeiKbnCd == ZeiKbnCd.SotoZei)
                            {
                                syukeiDetail.HINMEI_TAX_SOTO = this.GetSotoZei(syukeiDetail.HINMEI_KINGAKU, shotokuzeiRate, tempTaxHasuuCd);
                            }
                            // 内税
                            else if (zeiKbnCd == ZeiKbnCd.UchiZei)
                            {
                                syukeiDetail.HINMEI_TAX_UCHI = this.GetUchiZei(syukeiDetail.HINMEI_KINGAKU, shotokuzeiRate, tempTaxHasuuCd);
                            }
                        }
                        /* 集計した結果にて税計算を行う 2013/12/25 end */

                        syukeiDetail.ROW_NO = SqlInt16.Parse((++row_no).ToString());
                        res.Add(syukeiDetail);
                    }
                }
            }


            LogUtility.DebugMethodEnd(res);
            return res;
        }

        /// <summary>
        /// 検索結果レコードから売上／支払明細レコードを作製
        /// </summary>
        /// <param name="row"></param>
        /// <param name="row_no"></param>
        /// <returns></returns>
        private T_UR_SH_DETAIL SetDatailData(T_SELECT_RESULT row)
        {
            LogUtility.DebugMethodStart(row);
            T_UR_SH_DETAIL res = new T_UR_SH_DETAIL();

            //res.SYSTEM_ID = systemId;                 // Insert直前で設定
            //res.SEQ  = seq;                           // Insert直前で設定
            //res.UR_SH_NUMBER = ur_sh_number;          // Insert直前で設定
            //res.DETAIL_SYSTEM_ID = detail_system_id;  // Insert直前で設定
            //res.TIME_STAMP                            // Seaser2使用領域
            //res.ROW_NO                                // 呼び出し元が設定

            res.KAKUTEI_KBN = 1;
            res.DENPYOU_KBN_CD = row.DENPYOU_KBN_CD;
            if (!row.SAGYOU_DATE.IsNull)
            {
                // 集計単位：合算の場合、伝票日付を締日に設定
                if (this.whereDto.SHIMEBI != null
                    && this.whereDto.SHIMEBI != 0)
                {
                    var seikyuuUtility = new SeiKyuuUtility();
                    var uriageShiharaiHiduke = seikyuuUtility.GetNearSeikyuDate(this.whereDto.KIKAN_DATE_TO.Value, new short[1] { (short)this.whereDto.SHIMEBI });
                    res.URIAGESHIHARAI_DATE = uriageShiharaiHiduke.Date;
                }
                else
                {
                    res.URIAGESHIHARAI_DATE = this.whereDto.KIKAN_DATE_TO.Value.Date;
                }
            }

            res.HINMEI_CD = row.HINMEI_CD;
            res.HINMEI_NAME = row.HINMEI_NAME_RYAKU;
            res.SUURYOU = row.SUURYOU;
            res.UNIT_CD = row.UNIT_CD;
            res.TANKA = row.TANKA;

            SqlDecimal kingaku = 0;
            if (!row.SUURYOU.IsNull && !row.TANKA.IsNull)
            {
                // No.3368-->
                //kingaku = row.SUURYOU.ToSqlDecimal() * row.TANKA;
                if (!row.DENPYOU_KBN_CD.IsNull)
                {
                    //decimal suuryou = (decimal)row.TANKA;
                    //decimal tanka = (decimal)row.SUURYOU;
                    decimal suuryou = (decimal)row.SUURYOU;
                    decimal tanka = (decimal)row.TANKA;
                    short kingakuHasuuCd = 0;
                    var rowList = this.ResultList.Where(t => t.TORIHIKISAKI_CD == row.TORIHIKISAKI_CD).ToList();
                    var result = rowList.FirstOrDefault();

                    if ((short)row.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
                    {
                        kingakuHasuuCd = (short)result.KINGAKU_HASUU_CD;
                    }
                    else
                    {
                        kingakuHasuuCd = (short)result.SHIHARAI_KINGAKU_HASUU_CD;
                    }
                    kingaku = CommonCalc.FractionCalc(suuryou * tanka, kingakuHasuuCd);
                }
                else
                {
                    kingaku = row.SUURYOU * row.TANKA;
                }
                // No.3368<--
            }

            res.KINGAKU = 0;
            res.TAX_SOTO = 0;
            res.TAX_UCHI = 0;
            res.HINMEI_KINGAKU = 0;
            res.HINMEI_TAX_SOTO = 0;
            res.HINMEI_TAX_UCHI = 0;

            res.HINMEI_ZEI_KBN_CD = row.HINMEI_ZEI_KBN_CD;

            SqlInt16 tempTaxHasuuCd = row.DENPYOU_KBN_CD == CommonConst.DENPYOU_KBN_SHIHARAI ? row.SHIHARAI_TAX_HASUU_CD : row.TAX_HASUU_CD;

            // 品名マスタ．税区分CDが設定されている場合
            if (!row.HINMEI_ZEI_KBN_CD.IsNull)
            {
                // 品名金額を反映
                res.HINMEI_KINGAKU = kingaku;

                // 品名税区分判断
                ZeiKbnCd zeiKbnCd = this.GetZeiKbnCd(row.HINMEI_ZEI_KBN_CD);

                // 品名・外税
                if (zeiKbnCd == ZeiKbnCd.SotoZei)
                {
                    res.HINMEI_TAX_SOTO = this.GetSotoZei(res.HINMEI_KINGAKU, row.SHOUHIZEI_RATE, tempTaxHasuuCd);
                }
                // 品名・内税
                else if (zeiKbnCd == ZeiKbnCd.UchiZei)
                {
                    res.HINMEI_TAX_UCHI = this.GetUchiZei(res.HINMEI_KINGAKU, row.SHOUHIZEI_RATE, tempTaxHasuuCd);
                }
            }
            else
            {
                // 金額を反映
                res.KINGAKU = kingaku;

                // 税区分判断
                ZeiKbnCd zeiKbnCd = this.GetZeiKbnCd(row.ZEI_KBN_CD);
                if ((short)row.DENPYOU_KBN_CD != SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
                {
                    zeiKbnCd = this.GetZeiKbnCd(row.SHIHARAI_ZEI_KBN_CD);
                }

                // 外税
                if (zeiKbnCd == ZeiKbnCd.SotoZei)
                {
                    res.TAX_SOTO = this.GetSotoZei(res.KINGAKU, row.SHOUHIZEI_RATE, tempTaxHasuuCd);
                }
                // 内税
                else if (zeiKbnCd == ZeiKbnCd.UchiZei)
                {
                    res.TAX_UCHI = this.GetUchiZei(res.KINGAKU, row.SHOUHIZEI_RATE, tempTaxHasuuCd);
                }
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        #endregion

        #region DB挿入レコード取得(売上／支払：Entry)

        /// <summary>
        /// 売上／支払入力データ取得
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="ur_sh_number"></param>
        /// <param name="dataList"></param>
        /// <param name="DetailList"></param>
        /// <param name="tsukigimeKbn">集計単位(合算の場合、車輌CD等の情報は合算できないため、設定しない)</param>
        /// <returns></returns>
        private T_UR_SH_ENTRY GetInsertEntry(List<T_SELECT_RESULT> dataList, List<T_UR_SH_DETAIL> detailList, int tsukigimeKbn)
        {
            LogUtility.DebugMethodStart(dataList, detailList, tsukigimeKbn);

            // 共通データ(伝票日付が最新)
            SqlDateTime maxDateTime = dataList.Max<T_SELECT_RESULT, SqlDateTime>(t => t.SAGYOU_DATE);
            T_SELECT_RESULT v = dataList.Where(t => t.SAGYOU_DATE.Equals(maxDateTime)).FirstOrDefault();

            // 売上／支払入力テーブル 挿入データ作成
            T_UR_SH_ENTRY e = new T_UR_SH_ENTRY();

            //e.SYSTEM_ID = systemId;               // Insert直前で設定
            //e.UR_SH_NUMBER = ur_sh_number;        // Insert直前で設定
            //e.URIAGE_DATE, SHIHARAI_DATE          // Insert直前で設定

            e.SEQ = 1;

            e.KYOTEN_CD = v.KYOTEN_CD;
            e.KAKUTEI_KBN = 1;

            e.TORIHIKISAKI_CD = v.TORIHIKISAKI_CD;
            e.TORIHIKISAKI_NAME = v.TORIHIKISAKI_NAME_RYAKU;
            e.GYOUSHA_CD = v.GYOUSHA_CD;
            e.GYOUSHA_NAME = v.GYOUSHA_NAME_RYAKU;
            e.GENBA_CD = v.GENBA_CD;
            e.GENBA_NAME = v.GENBA_NAME_RYAKU;

            e.NIOROSHI_GYOUSHA_CD = v.NIOROSHI_GYOUSHA_CD;
            e.NIOROSHI_GYOUSHA_NAME = v.NIOROSHI_GYOUSHA_NAME_RYAKU;
            e.NIOROSHI_GENBA_CD = v.NIOROSHI_GENBA_CD;
            e.NIOROSHI_GENBA_NAME = v.NIOROSHI_GENBA_NAME_RYAKU;

            if (1 == tsukigimeKbn)
            {
                e.UNPAN_GYOUSHA_CD = v.UNPAN_GYOUSHA_CD;
                e.UNPAN_GYOUSHA_NAME = v.UNPAN_GYOUSHA_NAME;
            }

            e.NYUURYOKU_TANTOUSHA_CD = SystemProperty.Shain.CD;
            e.NYUURYOKU_TANTOUSHA_NAME = SystemProperty.Shain.Name;

            e.URIAGE_SHOUHIZEI_RATE = v.SHOUHIZEI_RATE;
            e.SHIHARAI_SHOUHIZEI_RATE = v.SHOUHIZEI_RATE;

            this.SetEigyouTantousha(e);

            /**
             * 売上
             */
            // ↓↓ サマリー算出開始 ↓↓
            e.URIAGE_AMOUNT_TOTAL = 0;
            // t.DENPYOU_KBN_CDがnullの場合のことを考慮していない
            var tempUriageKingakuList = detailList.Where(t => !t.DENPYOU_KBN_CD.IsNull && (short)t.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE);

            if (tempUriageKingakuList.Count() > 0)
            {
                e.URIAGE_AMOUNT_TOTAL = tempUriageKingakuList.Sum(t => t.KINGAKU.Value);
            }

            e.URIAGE_TAX_SOTO = 0;
            e.URIAGE_TAX_UCHI = 0;
            e.URIAGE_TAX_SOTO_TOTAL = 0;
            e.URIAGE_TAX_UCHI_TOTAL = 0;

            // 税算出区分を取得
            TaxSansyutuKbn taxSansyutuKbn = this.GetTaxSansyutuKbn(v.ZEI_KEISAN_KBN_CD);

            // 売上伝票毎消費税
            {
                SqlDecimal kingaku = e.URIAGE_AMOUNT_TOTAL;
                SqlDecimal shohiZei = v.SHOUHIZEI_RATE;
                SqlInt16 taxHasuuCd = v.TAX_HASUU_CD;

                // 税区分を取得
                ZeiKbnCd zeiKbnCd = this.GetZeiKbnCd(v.ZEI_KBN_CD);

                // 外税
                if (zeiKbnCd == ZeiKbnCd.SotoZei)
                {
                    e.URIAGE_TAX_SOTO = this.GetSotoZei(kingaku, shohiZei, taxHasuuCd);
                }

                // 内税
                if (zeiKbnCd == ZeiKbnCd.UchiZei)
                {
                    e.URIAGE_TAX_UCHI = this.GetUchiZei(kingaku, shohiZei, taxHasuuCd);
                }
            }

            // 売上明細毎消費税合計
            {
                var tempUriageTaxSotoList = detailList.Where(t => !t.DENPYOU_KBN_CD.IsNull && (short)t.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE);
                if (tempUriageTaxSotoList.Count() > 0)
                {
                    e.URIAGE_TAX_SOTO_TOTAL = tempUriageTaxSotoList.Sum(t => t.TAX_SOTO.Value);
                }

                var tempUriageTaxUchiList = detailList.Where(t => !t.DENPYOU_KBN_CD.IsNull && (short)t.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE);
                if (tempUriageTaxUchiList.Count() > 0)
                {
                    e.URIAGE_TAX_UCHI_TOTAL = tempUriageTaxUchiList.Sum(t => t.TAX_UCHI.Value);
                }
            }

            e.HINMEI_URIAGE_KINGAKU_TOTAL = 0;
            e.HINMEI_URIAGE_TAX_SOTO_TOTAL = 0;
            e.HINMEI_URIAGE_TAX_UCHI_TOTAL = 0;

            var tempHinmeiUrigageKingaku = detailList.Where(t => !t.DENPYOU_KBN_CD.IsNull && (short)t.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE);
            if (tempHinmeiUrigageKingaku.Count() > 0)
            {
                e.HINMEI_URIAGE_KINGAKU_TOTAL = tempHinmeiUrigageKingaku.Sum(t => t.HINMEI_KINGAKU.Value);
            }

            var tempHinmeiUriageTaxSoto = detailList.Where(t => !t.DENPYOU_KBN_CD.IsNull && (short)t.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE);
            if (tempHinmeiUriageTaxSoto.Count() > 0)
            {
                e.HINMEI_URIAGE_TAX_SOTO_TOTAL = tempHinmeiUriageTaxSoto.Sum(t => t.HINMEI_TAX_SOTO.Value);
            }

            var tempHinmeiUriageUchiList = detailList.Where(t => !t.DENPYOU_KBN_CD.IsNull && (short)t.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE);
            if (tempHinmeiUriageUchiList.Count() > 0)
            {
                e.HINMEI_URIAGE_TAX_UCHI_TOTAL = tempHinmeiUriageUchiList.Sum(t => t.HINMEI_TAX_UCHI.Value);
            }

            // ↑↑ サマリー算出終了 ↑↑

            /**
             * 支払
             */
            e.SHIHARAI_AMOUNT_TOTAL = 0;
            var tempShiharaiKingakuList = detailList.Where(t => !t.DENPYOU_KBN_CD.IsNull && (short)t.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI);
            if (tempShiharaiKingakuList.Count() > 0)
            {
                e.SHIHARAI_AMOUNT_TOTAL = tempShiharaiKingakuList.Sum(t => t.KINGAKU.Value);
            }

            e.SHIHARAI_TAX_SOTO = 0;
            e.SHIHARAI_TAX_UCHI = 0;
            e.SHIHARAI_TAX_SOTO_TOTAL = 0;
            e.SHIHARAI_TAX_UCHI_TOTAL = 0;

            // 税算出区分を取得
            TaxSansyutuKbn taxShiharaiSansyutuKbn = this.GetTaxSansyutuKbn(v.SHIHARAI_ZEI_KEISAN_KBN_CD);

            // 支払伝票毎消費税
            {
                SqlDecimal shiharaiKingaku = e.SHIHARAI_AMOUNT_TOTAL;
                SqlDecimal shiharaiShohiZei = v.SHOUHIZEI_RATE;
                SqlInt16 shiharaiTaxHasuuCd = v.SHIHARAI_TAX_HASUU_CD;

                // 税区分を取得
                ZeiKbnCd zeiKbnCd = this.GetZeiKbnCd(v.SHIHARAI_ZEI_KBN_CD);

                // 外税
                if (zeiKbnCd == ZeiKbnCd.SotoZei)
                {
                    e.SHIHARAI_TAX_SOTO = this.GetSotoZei(shiharaiKingaku, shiharaiShohiZei, shiharaiTaxHasuuCd);
                }

                // 内税
                if (zeiKbnCd == ZeiKbnCd.UchiZei)
                {
                    e.SHIHARAI_TAX_UCHI = this.GetUchiZei(shiharaiKingaku, shiharaiShohiZei, shiharaiTaxHasuuCd);
                }
            }

            // 支払明細毎消費税合計
            {
                var tempShiharaiTaxSotoList = detailList.Where(t => !t.DENPYOU_KBN_CD.IsNull && (short)t.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI);
                if (tempShiharaiTaxSotoList.Count() > 0)
                {
                    e.SHIHARAI_TAX_SOTO_TOTAL = tempShiharaiTaxSotoList.Sum(t => t.TAX_SOTO.Value);
                }

                var tempShiharaiTaxUchiList = detailList.Where(t => !t.DENPYOU_KBN_CD.IsNull && (short)t.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI);
                if (tempShiharaiTaxUchiList.Count() > 0)
                {
                    e.SHIHARAI_TAX_UCHI_TOTAL = tempShiharaiTaxUchiList.Sum(t => t.TAX_UCHI.Value);
                }
            }

            e.HINMEI_SHIHARAI_KINGAKU_TOTAL = 0;
            e.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = 0;
            e.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = 0;

            var tempHinmeiShiharaiKingakuList = detailList.Where(t => !t.DENPYOU_KBN_CD.IsNull && (short)t.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI);
            if (tempHinmeiShiharaiKingakuList.Count() > 0)
            {
                e.HINMEI_SHIHARAI_KINGAKU_TOTAL = tempHinmeiShiharaiKingakuList.Sum(t => t.HINMEI_KINGAKU.Value);
            }

            var tempHinmeiShiharaiSotoList = detailList.Where(t => !t.DENPYOU_KBN_CD.IsNull && (short)t.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI);
            if (tempHinmeiShiharaiSotoList.Count() > 0)
            {
                e.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = tempHinmeiShiharaiSotoList.Sum(t => t.HINMEI_TAX_SOTO.Value);
            }

            var tempHinmeiShiharaiTaxUchiList = detailList.Where(t => !t.DENPYOU_KBN_CD.IsNull && (short)t.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI);
            if (tempHinmeiShiharaiTaxUchiList.Count() > 0)
            {
                e.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = tempHinmeiShiharaiTaxUchiList.Sum(t => t.HINMEI_TAX_UCHI.Value);
            }

            e.URIAGE_ZEI_KEISAN_KBN_CD = v.ZEI_KEISAN_KBN_CD;
            e.URIAGE_ZEI_KBN_CD = v.ZEI_KBN_CD;
            e.URIAGE_TORIHIKI_KBN_CD = v.TORIHIKI_KBN_CD;

            e.SHIHARAI_ZEI_KEISAN_KBN_CD = v.SHIHARAI_ZEI_KEISAN_KBN_CD;
            e.SHIHARAI_ZEI_KBN_CD = v.SHIHARAI_ZEI_KBN_CD;
            e.SHIHARAI_TORIHIKI_KBN_CD = v.SHIHARAI_TORIHIKI_KBN_CD;

            e.TSUKI_CREATE_KBN = SqlBoolean.Zero;

            if (tsukigimeKbn == 1)
            {
                // 車輌系データ
                // 集計区分：合算の場合、異なる配車実績入力伝票を合算しているため特定の車輌CDを設定できない
                e.SHARYOU_CD = v.SHARYOU_CD;
                e.SHARYOU_NAME = v.SHARYOU_NAME;
                e.SHASHU_CD = v.SHASHU_CD;
                e.SHASHU_NAME = v.SHASHU_NAME;
                e.UNTENSHA_CD = v.UNTENSHA_CD;
                e.UNTENSHA_NAME = v.UNTENSHA_NAME;
            }

            // システム設定(Whoカラム)
            var binder = new DataBinderLogic<T_UR_SH_ENTRY>(e);
            binder.SetSystemProperty(e, false);
            e = binder.Entitys[0];

            LogUtility.DebugMethodEnd(e);
            return e;
        }
        #endregion

        #region DBレコード挿入(日連番)
        /// <summary>
        /// 引数の値からNumberDayを更新する
        /// </summary>
        /// <param name="date">売上支払伝票の伝票日付</param>
        /// <param name="kyotenCd">売上支払伝票の拠点CD</param>
        /// <returns>CurrentNumber(日連番)</returns>
        private int InsertOrUpdateOfNumberDayEntity(DateTime date, short kyotenCd)
        {
            int currentNumber = -1;

            S_NUMBER_DAY targetEntity = new S_NUMBER_DAY();

            targetEntity.NUMBERED_DAY = date.Date;
            targetEntity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;
            targetEntity.KYOTEN_CD = kyotenCd;
            targetEntity.DELETE_FLG = false;

            var dataBinderNumberDay = new DataBinderLogic<S_NUMBER_DAY>(targetEntity);
            dataBinderNumberDay.SetSystemProperty(targetEntity, false);

            // 既にレコードがあるかチェック
            S_NUMBER_DAY[] numberDays = this.dbAccesserForG303.GetNumberDay(date.Date, SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI, kyotenCd);

            if (numberDays == null || numberDays.Length < 1)
            {
                targetEntity.CURRENT_NUMBER = 1;
                this.dbAccesserForG303.InsertNumberDay(targetEntity);
            }
            else
            {
                targetEntity.CURRENT_NUMBER = numberDays[0].CURRENT_NUMBER + 1;
                targetEntity.TIME_STAMP = numberDays[0].TIME_STAMP;
                this.dbAccesserForG303.UpdateNumberDay(targetEntity);
            }

            currentNumber = (int)targetEntity.CURRENT_NUMBER;

            return currentNumber;
        }
        #endregion

        #region DBレコード挿入(年連番)
        /// <summary>
        /// 引数の値からNumberYearを更新する
        /// </summary>
        /// <param name="numberedYear">売上支払伝票.伝票日付が属している年</param>
        /// <param name="kyotenCd">売上支払伝票の拠点CD</param>
        /// <returns>CurrentNumber(年連番)</returns>
        private int InsertOrUpdateOfNumberYearEntity(int numberedYear, short kyotenCd)
        {
            int currentNumber = -1;

            S_NUMBER_YEAR targetEntity = new S_NUMBER_YEAR();

            targetEntity.NUMBERED_YEAR = numberedYear;
            targetEntity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;
            targetEntity.KYOTEN_CD = kyotenCd;
            targetEntity.DELETE_FLG = false;

            var dataBinderNumberYear = new DataBinderLogic<S_NUMBER_YEAR>(targetEntity);
            dataBinderNumberYear.SetSystemProperty(targetEntity, false);

            // 既にレコードがあるかチェック
            S_NUMBER_YEAR[] numberYeas = this.dbAccesserForG303.GetNumberYear(numberedYear, SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI, kyotenCd);

            if (numberYeas == null || numberYeas.Length < 1)
            {
                targetEntity.CURRENT_NUMBER = 1;
                this.dbAccesserForG303.InsertNumberYear(targetEntity);
            }
            else
            {
                targetEntity.CURRENT_NUMBER = numberYeas[0].CURRENT_NUMBER + 1;
                targetEntity.TIME_STAMP = numberYeas[0].TIME_STAMP;
                this.dbAccesserForG303.UpdateNumberYear(targetEntity);
            }

            currentNumber = (int)targetEntity.CURRENT_NUMBER;

            return currentNumber;
        }
        #endregion

        #region 外税・内税算出

        /// <summary>
        /// 税金(外税)算出
        /// </summary>
        /// <param name="kingaku"></param>
        /// <param name="taxRate"></param>
        /// <returns></returns>
        private SqlDecimal GetSotoZei(SqlDecimal kingaku, SqlDecimal taxRate, SqlInt16 hasuuCd)
        {
            LogUtility.DebugMethodStart(kingaku, taxRate, hasuuCd);

            SqlDecimal sotoZei = 0;

            if (!taxRate.IsNull && !kingaku.IsNull)
            {
                // 外税算出式
                decimal value = kingaku.Value * taxRate.Value;

                // 丸めCDを取得
                TaxHasuuCd taxHasuuCd = GetTaxHasuuCd(hasuuCd);

                // 丸め処理
                sotoZei = this.GetMarumeValue(value, taxHasuuCd);
            }

            LogUtility.DebugMethodEnd(sotoZei);
            return sotoZei;
        }

        /// <summary>
        /// 税金(内税)算出
        /// </summary>
        /// <param name="kingaku"></param>
        /// <param name="taxRate"></param>
        /// <returns></returns>
        private SqlDecimal GetUchiZei(SqlDecimal kingaku, SqlDecimal taxRate, SqlInt16 hasuuCd)
        {
            LogUtility.DebugMethodStart(kingaku, taxRate, hasuuCd);

            SqlDecimal uchiZei = 0;

            if (!taxRate.IsNull && !kingaku.IsNull)
            {
                // 内税算出式
                decimal value = kingaku.Value - (kingaku.Value / (taxRate.Value + 1));

                // 丸めCDを取得
                TaxHasuuCd taxHasuuCd = this.GetTaxHasuuCd(hasuuCd);

                // 丸め処理
                uchiZei = this.GetMarumeValue(value, taxHasuuCd);
            }

            LogUtility.DebugMethodEnd(uchiZei);
            return uchiZei;
        }

        /// <summary>
        /// 端数CDにより値を丸め
        /// </summary>
        /// <param name="value"></param>
        /// <param name="taxHasuuCd"></param>
        /// <returns></returns>
        private decimal GetMarumeValue(decimal value, TaxHasuuCd taxHasuuCd)
        {
            LogUtility.DebugMethodStart(value, taxHasuuCd);

            decimal res = 0;
            decimal sign = 1;
            if (value < 0)
            {
                sign = -1;
            }

            value = Math.Abs(value);

            // 切り上げ
            if (taxHasuuCd == TaxHasuuCd.KiriAge)
            {
                res = Math.Ceiling(value);
            }

            // 切捨て
            else if (taxHasuuCd == TaxHasuuCd.KiriSute)
            {
                res = Math.Floor(value);
            }

            // 四捨五入
            else if (taxHasuuCd == TaxHasuuCd.SisyaGonyu)
            {
                res = Math.Round(value, 0, MidpointRounding.AwayFromZero);
            }

            // その他
            else
            {
                // そのまま
                res = value;
            }

            res = res * sign;

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        #endregion

        #region 各種CD・区分取得

        /// <summary>
        /// 税計算区分を取得
        /// </summary>
        /// <param name="zeiKbnCd"></param>
        /// <returns></returns>
        private TaxSansyutuKbn GetTaxSansyutuKbn(SqlInt16 taxSansyutuKbn)
        {
            LogUtility.DebugMethodStart(taxSansyutuKbn);
            TaxSansyutuKbn res = TaxSansyutuKbn.None;

            if (!taxSansyutuKbn.IsNull)
            {
                res = (TaxSansyutuKbn)taxSansyutuKbn.Value;
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        /// <summary>
        /// 税区分CDを取得
        /// </summary>
        /// <param name="zeiKbnCd"></param>
        /// <returns></returns>
        private ZeiKbnCd GetZeiKbnCd(SqlInt16 zeiKbnCd)
        {
            LogUtility.DebugMethodStart(zeiKbnCd);
            ZeiKbnCd res = ZeiKbnCd.None;

            if (!zeiKbnCd.IsNull)
            {
                res = (ZeiKbnCd)zeiKbnCd.Value;
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        /// <summary>
        /// 消費税端数CDを取得
        /// </summary>
        /// <param name="taxHasuuCd"></param>
        /// <returns></returns>
        private TaxHasuuCd GetTaxHasuuCd(SqlInt16 taxHasuuCd)
        {
            LogUtility.DebugMethodStart(taxHasuuCd);
            TaxHasuuCd res = TaxHasuuCd.None;

            if (!taxHasuuCd.IsNull)
            {
                res = (TaxHasuuCd)taxHasuuCd.Value;
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }
        #endregion

        #region DB更新レコード取得(定期実績：Entry)

        /// <summary>
        /// 定期実績入力レコード生成(売上／支払番号更新)
        /// </summary>
        /// <param name="selectList"></param>
        /// <returns></returns>
        private List<T_TEIKI_JISSEKI_ENTRY> GetUpdateEntryList(List<T_SELECT_RESULT> selectList)
        {
            LogUtility.DebugMethodStart(selectList);

            List<T_TEIKI_JISSEKI_ENTRY> res = new List<T_TEIKI_JISSEKI_ENTRY>();

            var groupList = selectList.GroupBy(t => new { t.SYSTEM_ID, t.SEQ }).ToList();

            foreach (var g in groupList)
            {
                var s = selectList.Where(t => t.SYSTEM_ID.Equals(g.Key.SYSTEM_ID)
                                            && t.SEQ.Equals(g.Key.SEQ)
                                            )
                                        .FirstOrDefault();

                T_TEIKI_JISSEKI_ENTRY entry = new T_TEIKI_JISSEKI_ENTRY();
                entry = daoTeikiJissekiEntry.GetDataByKey(s.SYSTEM_ID, s.SEQ);
                if (entry == null)
                {
                    entry = new T_TEIKI_JISSEKI_ENTRY();
                    entry.SYSTEM_ID = s.SYSTEM_ID;
                    entry.SEQ = s.SEQ;
                }
                entry.TIME_STAMP = (byte[])s.ENTRY_TIME_STAMP;


                // システム設定(Whoカラム)
                var binder = new DataBinderLogic<T_TEIKI_JISSEKI_ENTRY>(entry);
                binder.SetSystemProperty(entry, false);

                // 削除フラグは自分で設定
                entry.DELETE_FLG = SqlBoolean.False;

                res.Add(entry);
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        #endregion

        #region DB更新レコード取得(定期実績：Detail)

        /// <summary>
        /// 定期実績明細レコード生成(売上／支払番号更新)
        /// </summary>
        /// <param name="selectList"></param>
        /// <returns></returns>
        private List<T_TEIKI_JISSEKI_DETAIL> GetUpdateDetailList(List<T_SELECT_RESULT> selectList)
        {
            LogUtility.DebugMethodStart(selectList);

            List<T_TEIKI_JISSEKI_DETAIL> res = new List<T_TEIKI_JISSEKI_DETAIL>();
            foreach (T_SELECT_RESULT s in selectList)
            {
                T_TEIKI_JISSEKI_DETAIL detail = new T_TEIKI_JISSEKI_DETAIL();
                detail = daoTeikiJissekiDetail.GetDataByKey(s.SYSTEM_ID, s.SEQ, s.DETAIL_SYSTEM_ID);
                if (detail == null)
                {
                    detail = new T_TEIKI_JISSEKI_DETAIL();
                    detail.SYSTEM_ID = s.SYSTEM_ID;
                    detail.SEQ = s.SEQ;
                    detail.DETAIL_SYSTEM_ID = s.DETAIL_SYSTEM_ID;
                }
                detail.ROUND_NO = s.ROUND_NO;
                detail.TIME_STAMP = s.DETAIL_TIME_STAMP;
                res.Add(detail);
            }

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

        #region IBuisinessLogic(未実装)
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
        public bool Equals(LogicClass other)
        {
            return this.Equals(other);
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

        #region 締日のEnabledの切り替え処理

        /// <summary>
        /// 取引区分によって締日のEnabledを切り替えます。
        /// 取引区分が現金の場合
        ///     Enabled = True
        /// 取引区分が掛けの場合
        ///     Enabled = False
        /// </summary>
        internal void ChangeTorihikiKbnValue()
        {
            try
            {
                this.InitShimebi();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeTorihikiKbnValue", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
        }

        #endregion

        private void SetEigyouTantousha(T_UR_SH_ENTRY entity)
        {
            LogUtility.DebugMethodStart(entity);

            var torihikisaki = this.GetTorihikisaki(entity.TORIHIKISAKI_CD);
            if (null != torihikisaki && !String.IsNullOrEmpty(torihikisaki.EIGYOU_TANTOU_CD))
            {
                entity.EIGYOU_TANTOUSHA_CD = torihikisaki.EIGYOU_TANTOU_CD;

            }
            var gyousha = this.GetGyousha(entity.GYOUSHA_CD);
            if (null != gyousha && !String.IsNullOrEmpty(gyousha.EIGYOU_TANTOU_CD))
            {
                entity.EIGYOU_TANTOUSHA_CD = gyousha.EIGYOU_TANTOU_CD;

            }
            var genba = this.GetGenba(entity.GYOUSHA_CD, entity.GENBA_CD);
            if (null != genba && !String.IsNullOrEmpty(genba.EIGYOU_TANTOU_CD))
            {
                entity.EIGYOU_TANTOUSHA_CD = genba.EIGYOU_TANTOU_CD;

            }

            if (!String.IsNullOrEmpty(entity.EIGYOU_TANTOUSHA_CD))
            {
                var shain = this.GetShain(entity.EIGYOU_TANTOUSHA_CD);
                if (null != shain)
                {
                    entity.EIGYOU_TANTOUSHA_NAME = shain.SHAIN_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        private M_TORIHIKISAKI GetTorihikisaki(string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(torihikisakiCd);

            M_TORIHIKISAKI ret = null;

            var dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            ret = dao.GetAllValidData(new M_TORIHIKISAKI() { TORIHIKISAKI_CD = torihikisakiCd }).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        private M_GYOUSHA GetGyousha(string gyoushaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd);

            M_GYOUSHA ret = null;

            var dao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            ret = dao.GetAllValidData(new M_GYOUSHA() { GYOUSHA_CD = gyoushaCd }).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        private M_GENBA GetGenba(string gyoushaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            M_GENBA ret = null;

            var dao = DaoInitUtility.GetComponent<IM_GENBADao>();
            ret = dao.GetAllValidData(new M_GENBA() { GYOUSHA_CD = gyoushaCd, GENBA_CD = genbaCd }).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        private M_SHAIN GetShain(string shainCd)
        {
            LogUtility.DebugMethodStart(shainCd);

            M_SHAIN ret = null;

            var dao = DaoInitUtility.GetComponent<IM_SHAINDao>();
            ret = dao.GetAllValidData(new M_SHAIN() { SHAIN_CD = shainCd }).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        #region 月次処理チェック

        /// <summary>
        /// 月次処理中かのチェックを行います
        /// </summary>
        /// <returns>True：月次処理中</returns>
        internal bool CheckGetsujiShoriChu()
        {
            bool val = false;

            // 最新月次処理中年月取得
            GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
            string strDate = getsujiShoriCheckLogic.GetLatestGetsjiShoriChuDateTime();

            if (string.IsNullOrEmpty(strDate))
            {
                // 月次処理は実行されていない
                return val;
            }

            DateTime getsujiShoriChuDate = DateTime.Parse(strDate);

            foreach (DB_UPDATE_DATA u in DbUpdateList)
            {
                #region Insertデータチェック

                if (u.InsertEntry != null && !string.IsNullOrEmpty(u.InsertEntry.TORIHIKISAKI_CD))
                {
                    // 売上/支払伝票の日付を取得
                    SqlDateTime tempUriageDate = SqlDateTime.Null;
                    SqlDateTime tempShiharaiDate = SqlDateTime.Null;
                    foreach (T_UR_SH_DETAIL usd in u.InsertDetailList)
                    {
                        DateTime tempUrShDate;  // Dateプロパティを使いたいがためDateTime型で宣言
                        if (!usd.URIAGESHIHARAI_DATE.IsNull
                            && !usd.DENPYOU_KBN_CD.IsNull)
                        {
                            tempUrShDate = (DateTime)usd.URIAGESHIHARAI_DATE;

                            if (usd.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
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
                            else if (usd.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI)
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

                    SqlDateTime denpyouDate;
                    denpyouDate = tempUriageDate;
                    if (denpyouDate.IsNull)
                    {
                        denpyouDate = tempShiharaiDate;
                    }

                    if (denpyouDate.Value.CompareTo(getsujiShoriChuDate) <= 0)
                    {
                        // 登録する伝票日付が月次処理中の日付より前の場合は伝票登録不可
                        val = true;
                        break;
                    }
                }

                #endregion

                #region Deleteデータチェック

                if (u.DeleteUrShEntry != null && !string.IsNullOrEmpty(u.DeleteUrShEntry.TORIHIKISAKI_CD))
                {
                    // 伝票日付を使用してチェック
                    SqlDateTime denpyouDate = u.DeleteUrShEntry.DENPYOU_DATE;
                    if (denpyouDate.Value.CompareTo(getsujiShoriChuDate) <= 0)
                    {
                        // 登録する伝票日付が月次処理中の日付より前の場合は伝票登録不可
                        val = true;
                        break;
                    }
                }

                #endregion
            }

            return val;
        }

        /// <summary>
        /// 月次処理によってロックされているかのチェックを行います
        /// </summary>
        /// <returns>True：ロックされている　False：ロックされていない</returns>
        internal bool CheckGetsujiShoriLock()
        {
            bool val = false;

            // 最新月次年月取得
            int year = 0;
            int month = 0;
            GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
            getsujiShoriCheckLogic.GetLatestGetsujiDate(ref year, ref month);

            if (year == 0 || month == 0)
            {
                // 月次処理データ無し
                return val;
            }

            // 月次年月日を最新月次年月末日にする
            DateTime getsujiShoriDate = new DateTime(year, month, 1);
            getsujiShoriDate = getsujiShoriDate.AddMonths(1).AddDays(-1);

            foreach (DB_UPDATE_DATA u in DbUpdateList)
            {
                #region Insertデータチェック

                if (u.InsertEntry != null && !string.IsNullOrEmpty(u.InsertEntry.TORIHIKISAKI_CD))
                {
                    // 売上/支払伝票の日付を取得
                    SqlDateTime tempUriageDate = SqlDateTime.Null;
                    SqlDateTime tempShiharaiDate = SqlDateTime.Null;
                    foreach (T_UR_SH_DETAIL usd in u.InsertDetailList)
                    {
                        DateTime tempUrShDate;  // Dateプロパティを使いたいがためDateTime型で宣言
                        if (!usd.URIAGESHIHARAI_DATE.IsNull
                            && !usd.DENPYOU_KBN_CD.IsNull)
                        {
                            tempUrShDate = (DateTime)usd.URIAGESHIHARAI_DATE;

                            if (usd.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE)
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
                            else if (usd.DENPYOU_KBN_CD == SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI)
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

                    SqlDateTime denpyouDate;
                    denpyouDate = tempUriageDate;
                    if (denpyouDate.IsNull)
                    {
                        denpyouDate = tempShiharaiDate;
                    }

                    if (denpyouDate.Value.CompareTo(getsujiShoriDate) <= 0)
                    {
                        // 登録する伝票日付が最新月次年月の日付より前の場合は伝票登録不可
                        val = true;
                        break;
                    }
                }

                #endregion

                #region Deleteデータチェック

                if (u.DeleteUrShEntry != null && !string.IsNullOrEmpty(u.DeleteUrShEntry.TORIHIKISAKI_CD))
                {
                    // 伝票日付を使用してチェック
                    SqlDateTime denpyouDate = u.DeleteUrShEntry.DENPYOU_DATE;
                    if (denpyouDate.Value.CompareTo(getsujiShoriDate) <= 0)
                    {
                        // 登録する伝票日付が最新月次年月の日付より前の場合は伝票登録不可
                        val = true;
                        break;
                    }
                }

                #endregion
            }

            return val;
        }

        #endregion

        // 20141118 koukouei 締済期間チェックの追加 start
        #region 締済期間チェック
        /// <summary>
        /// 締済期間チェック
        /// </summary>
        /// <returns></returns>
        internal bool ShimeiDateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            ShimeCheckLogic CheckShimeDate = new ShimeCheckLogic();
            List<ReturnDate> returnDate_U = new List<ReturnDate>();
            List<ReturnDate> returnDate_S = new List<ReturnDate>();
            List<CheckDate> uriageList = new List<CheckDate>();
            List<CheckDate> shiharaiList = new List<CheckDate>();

            CheckDate cd = new CheckDate();
            // 更新ループ
            foreach (DB_UPDATE_DATA u in DbUpdateList)
            {
                if (u.InsertEntry == null || u.InsertEntry.KYOTEN_CD.IsNull || string.IsNullOrEmpty(u.InsertEntry.TORIHIKISAKI_CD))
                {
                    continue;
                }

                foreach (T_UR_SH_DETAIL usd in u.InsertDetailList)
                {
                    if (usd.URIAGESHIHARAI_DATE.IsNull)
                    {
                        continue;
                    }

                    cd = new CheckDate();
                    // 取引先CD
                    cd.TORIHIKISAKI_CD = u.InsertEntry.TORIHIKISAKI_CD;
                    // 拠点CD
                    cd.KYOTEN_CD = Convert.ToString(u.InsertEntry.KYOTEN_CD).PadLeft(2, '0');
                    // 日付
                    cd.CHECK_DATE = usd.URIAGESHIHARAI_DATE.Value;

                    if (usd.DENPYOU_KBN_CD == 1)
                    {
                        uriageList.Add(cd);
                    }
                    else if (usd.DENPYOU_KBN_CD == 2)
                    {
                        shiharaiList.Add(cd);
                    }
                }
            }

            // 売上チェック
            returnDate_U = CheckShimeDate.GetNearShimeDate(uriageList, 1);
            // 支払チェック
            returnDate_S = CheckShimeDate.GetNearShimeDate(shiharaiList, 2);

            // 売上
            if (returnDate_U.Count != 0)
            {
                //例外日付が含まれる
                foreach (ReturnDate rdDate in returnDate_U)
                {
                    if (rdDate.dtDATE == SqlDateTime.MinValue.Value)
                    {
                        msgLogic.MessageBoxShow("E214");
                        return false;
                    }
                }
                if (msgLogic.MessageBoxShow("C085", "") == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            // 支払
            else if (returnDate_S.Count != 0)
            {
                //例外日付が含まれる
                foreach (ReturnDate rdDate in returnDate_S)
                {
                    if (rdDate.dtDATE == SqlDateTime.MinValue.Value)
                    {
                        msgLogic.MessageBoxShow("E214");
                        return false;
                    }
                }
                if (msgLogic.MessageBoxShow("C085", "") == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
        // 20141118 koukouei 締済期間チェックの追加 end

        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141128 teikyou ダブルクリックを追加する　start
        private void dtp_KikanTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var kikanFromTextBox = this.form.dtp_KikanFrom;
            var kikanToTextBox = this.form.dtp_KikanTo;
            kikanToTextBox.Text = kikanFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 20141128 teikyou ダブルクリックを追加する　end
        #endregion

        /// 20141203 Houkakou 「実績売上支払確定」の日付チェックを追加する　start
        #region dtp_KikanFrom_Leaveイベント
        /// <summary>
        /// dtp_KikanFrom_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void dtp_KikanFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.dtp_KikanTo.Text))
            {
                this.form.dtp_KikanTo.IsInputErrorOccured = false;
                this.form.dtp_KikanTo.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region dtp_KikanTo_Leaveイベント
        /// <summary>
        /// TEKIYOU_END_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void dtp_KikanTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.dtp_KikanFrom.Text))
            {
                this.form.dtp_KikanFrom.IsInputErrorOccured = false;
                this.form.dtp_KikanFrom.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion
        /// 20141203 Houkakou 「実績売上支払確定」の日付チェックを追加する　end

        #region 20151021 hoanghm #13498

        private void LoadKyotenForPopup()
        {
            DataTable dtKyoten = kyotenDao.GetAllMasterDataForPopup("WHERE M_KYOTEN.KYOTEN_CD <> 99");
            this.form.txtBox_KyotenCd.PopupGetMasterField = "CD,NAME";
            this.form.txtBox_KyotenCd.PopupDataHeaderTitle = new string[] { "拠点CD", "拠点名" };
            this.form.txtBox_KyotenCd.PopupDataSource = dtKyoten;
        }

        #endregion

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        public bool CheckUrSh()
        {
            LogUtility.DebugMethodStart();

            bool returnVal = false;
            if (this.form.TorihikiKbnValue.Text != SearchDTOClass.TORIHIKI_KBN_VALUE_KAKE
                || this.form.fixConditionValue.Text != SearchDTOClass.FIX_CONDITION_VALUE_UNFIXED)
            {
                return returnVal;
            }

            // チェックされた取引先CDを取得
            List<string> torihikisakiCdList = new List<string>();
            foreach (DataRow gridRow in this.GridDataTbl.Rows)
            {
                if ((int)gridRow[DEF_COLUMN_1_NAME] == 1)
                {
                    string torihikisakiCd = gridRow[DEF_COLUMN_4_NAME].ToString().Trim();
                    torihikisakiCdList.Add(torihikisakiCd);
                }
            }

            // 取引先CD単位でリスト化
            foreach (var key in torihikisakiCdList)
            {
                this.whereDto.TORIHIKISAKI_CD = key;
                DataTable dt = this.daoSelect.GetUrShData(this.whereDto);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DialogResult ret = this.MsgBox.MessageBoxShow("C099");
                    if (ret != DialogResult.Yes)
                    {
                        returnVal = true;
                    }
                    break;
                }
            }

            LogUtility.DebugMethodEnd(returnVal);
            return returnVal;
        }

        /// <summary>
        /// 取引先の締日チェックを行います
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="shimebi">締日</param>
        /// <returns>チェック結果</returns>
        private bool CheckTorihikisakiShimebi(string torihikisakiCd, string shimebi)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(torihikisakiCd, shimebi);

                var torihikisakiSeikyuu = this.SeikyuuShimebiDao.GetTorihikisakiSeikyuuByTorihikisakiCdAndShimebi1(torihikisakiCd, shimebi);
                if (torihikisakiSeikyuu != null)
                {
                    ret = true;
                }

                var torihikisakiShiharai = this.ShiharaiShimebiDao.GetTorihikisakiShiharaiByTorihikisakiCdAndShimebi1(torihikisakiCd, shimebi);
                if (torihikisakiShiharai != null)
                {
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
    }
}
