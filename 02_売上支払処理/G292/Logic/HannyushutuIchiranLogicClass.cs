using System;
using System.Data;
using System.Reflection;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.SalesPayment.HannyushutsuIchiran.DAO;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Linq;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.SalesPayment.HannyushutsuIchiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class HannyushutsuIchiranLogicClass : IBuisinessLogic
    {
        #region フィールド
        /// <summary>
        /// Form
        /// </summary>
        private HannyushutsuIchiranForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// HeaderForm
        /// </summary>
        private HannyushutsuIchiranHeader headerForm;

        /// <summary>
        /// メッセージクラス
        /// </summary>
        private MessageBoxShowLogic msgcls;

        /// <summary>	
        /// 拠点マスタDao
        /// </summary>	
        private IM_KYOTENDao mkyotenDao;

        /// <summary>
        /// 車輌マスタDao
        /// </summary>
        private IM_SHARYOUDao msharyouDao;

        /// <summary>
        /// 業者マスタDao
        /// </summary>
        private IM_GYOUSHADao mgyoushaDao;

        /// <summary>
        /// 現場マスタDao
        /// </summary>
        private IM_GENBADao mgenbaDao;

        /// <summary>
        /// 搬入予定Dao
        /// </summary>
        private HannyuYoteiDaoCls HannyuYoteiDao;

        /// <summary>
        /// 搬出予定Dao
        /// </summary>
        private HanshutsuYoteiDaoCls HanshutsuYoteiDao;

        /// <summary>
        /// 計量入力Dao
        /// </summary>
        private KeiryouEntryDaoCls KeiryouEntryDao;

        /// <summary>
        /// 受入入力Dao
        /// </summary>
        private UkeireEntryDaoCls UkeireEntryDao;

        /// <summary>
        /// 出荷入力Dao
        /// </summary>
        private ShukkaEntryDaoCls ShukkaEntryDao;

        /// <summary>
        /// 売上_支払入力Dao
        /// </summary>
        private UrShEntryDaoCls UrShEntryDao;

        /// <summary>
        /// 計量受付番号
        /// </summary>
        private long? F1_UketsukeNumber = null;

        /// <summary>
        /// 受入受付番号
        /// </summary>
        private long? F2_UketsukeNumber = null;

        /// <summary>
        /// 出荷・売上支払受付番号
        /// </summary>
        private long? F3_F4_UketsukeNumber = null;

        /// <summary>
        /// 画面切替
        /// </summary>
        private Gamenkirikae gamenkirikae;

        /// <summary>
        /// 車輌CD
        /// </summary>
        private string sharyouCd = string.Empty;

        /// <summary>
        /// 業者CD
        /// </summary>
        private string gyoushaCd = string.Empty;

        /// <summary>
        /// 現場CD
        /// </summary>
        private string genbaCd = string.Empty;

        /// <summary>
        /// 荷降現場CD
        /// </summary>
        private string nioroshiGenbaCd = string.Empty;

        /// <summary>
        /// 車輌エラーフラグ
        /// </summary>
        internal bool isSharyouCdError = false;

        /// <summary>
        /// 現場エラーフラグ
        /// </summary>
        internal bool isGenbaCdError = false;

        /// <summary>
        /// 荷降現場エラーフラグ
        /// </summary>
        internal bool isNioroshiGenbaCdError = false;

        #endregion

        #region プロパティ
        /// <summary>
        /// 搬入予定検索結果
        /// </summary>
        public DataTable HannyuYoteiSearchResult { get; set; }

        /// <summary>
        /// 搬出予定検索結果
        /// </summary>
        public DataTable HanshutsuYoteiSearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public DTOClass SearchString { get; set; }
        #endregion

        #region 列挙体
        /// <summary>
        /// 画面切替
        /// </summary>
        enum Gamenkirikae
        {
            OUT_IN = 1, // 搬出と搬入
            IN = 2,     // 搬入
            OUT = 3     // 搬出
        }
        #endregion

        #region 定数
        // 搬出種別初期値
        private const String HANSHUTSUSHUBETSU_INIT = "1";

        // 搬入予定一覧ラベル初期値
        private const String GRIDTITLE_HANNYUYOTEI_INIT = "搬入予定（持込）";

        // 搬出予定一覧ラベル（1:全て）
        private const String GRIDTITLE_HANSHUTSUYOTEI_1 = "搬出予定（収集，出荷）";

        // 搬出予定一覧ラベル（2:収集）
        private const String GRIDTITLE_HANSHUTSUYOTEI_2 = "搬出予定（収集）";

        // 搬出予定一覧ラベル（3:出荷）
        private const String GRIDTITLE_HANSHUTSUYOTEI_3 = "搬出予定（出荷）";

        // グリッドタイトル１(Y)初期値
        private const int GRIDTITLE_1_Y_INIT = 72;

        // ソートヘッダー１(Y)初期値
        private const int SORTHEADER_1_Y_INIT = 92;

        // グリッド１(Y)初期値
        private const int GRID_1_Y_INIT = 118;

        // グリッドタイトル２(Y)初期値
        private const int GRIDTITLE_2_Y_INIT = 262;

        // ソートヘッダー２(Y)初期値
        private const int SORTHEADER_2_Y_INIT = 282;

        // グリッド２(Y)初期値
        private const int GRID_2_Y_INIT = 308;

        // グリッド２つ表示時のHEIGHT初期値
        private const int GRID_SINGLE_HEIGHT_INIT = 142;

        // グリッド１つ表示時のHEIGHT初期値
        private const int GRID_DOUBLE_HEIGHT_INIT = 332;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public HannyushutsuIchiranLogicClass(HannyushutsuIchiranForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.SearchString = new DTOClass();
            this.mkyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.msharyouDao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
            this.mgyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.mgenbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.HannyuYoteiDao = DaoInitUtility.GetComponent<HannyuYoteiDaoCls>();
            this.HanshutsuYoteiDao = DaoInitUtility.GetComponent<HanshutsuYoteiDaoCls>();
            this.KeiryouEntryDao = DaoInitUtility.GetComponent<KeiryouEntryDaoCls>();
            this.UkeireEntryDao = DaoInitUtility.GetComponent<UkeireEntryDaoCls>();
            this.ShukkaEntryDao = DaoInitUtility.GetComponent<ShukkaEntryDaoCls>();
            this.UrShEntryDao = DaoInitUtility.GetComponent<UrShEntryDaoCls>();

            // メッセージ出力用
            this.msgcls = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd(targetForm);
        }
        #endregion

        #region 各初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // オブジェクト初期化
                this.ObjectInit();

                // 画面切替
                this.gamenkirikae = Gamenkirikae.OUT_IN;

                // ファンクションキーの活性設定
                this.SetFuncEnabled();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgcls.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void ObjectInit()
        {
            LogUtility.DebugMethodStart();

            //================================CurrentUserCustomConfigProfile.xmlを読み込み============================
            XMLAccessor fileAccess = new XMLAccessor();
            CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();

            //前回保存値がない場合はシステム設定ファイルから拠点CDを設定する
            //拠点CDを取得  
            //前回値ありの場合
            if (!string.IsNullOrEmpty(Properties.Settings.Default.SET_KYOTEN_CD))
            {
                var kyotenCd = Properties.Settings.Default.SET_KYOTEN_CD;
                this.headerForm.txtKyotenCd.Text = string.Empty;
                var kyoten_cd = 0;
                //数字チェック + 空チェック
                var kyoten_res = int.TryParse(kyotenCd, out kyoten_cd);
                if (kyoten_res)
                {
                    M_KYOTEN mKyoten = new M_KYOTEN();
                    mKyoten.KYOTEN_CD = (SqlInt16)kyoten_cd;
                    //削除フラグがたっていない適用期間内の情報を取得する
                    var mKyotenList = mkyotenDao.GetAllValidData(mKyoten);
                    if (mKyotenList.Count() > 0)
                    {
                        this.headerForm.txtKyotenCd.Text = String.Format("{0:D2}", kyoten_cd);
                    }
                }
                //前回保存値がブランクの場合
            }
            else if (Properties.Settings.Default.SET_KYOTEN_CD == null)
            {
                this.headerForm.txtKyotenCd.Text = "";
            }
            //前回保存値がない場合
            else
            {
                this.headerForm.txtKyotenCd.Text = String.Format("{0:D2}", int.Parse(configProfile.ItemSetVal1));
            }


            // ユーザ拠点名称の取得
            if (this.headerForm.txtKyotenCd.Text != null)
            {
                M_KYOTEN mKyoten = new M_KYOTEN();
                mKyoten = (M_KYOTEN)mkyotenDao.GetDataByCd(this.headerForm.txtKyotenCd.Text);
                if (mKyoten == null || this.headerForm.txtKyotenCd.Text == "")
                {
                    this.headerForm.txtKyotenNameRyaku.Text = "";
                }
                else
                {
                    this.headerForm.txtKyotenNameRyaku.Text = mKyoten.KYOTEN_NAME_RYAKU;
                }
            }

            // 作業日FROM
            this.headerForm.dtpSagyouDateFrom.Value = this.parentForm.sysDate;
            // 作業日TO
            this.headerForm.dtpSagyouDateTo.Value = this.parentForm.sysDate;
            // 読込データ件数
            this.headerForm.txtReadDataCnt.Text = string.Empty;

            // 車輌CD
            this.form.txtSharyouCd.Text = string.Empty;
            // 車輌名
            this.form.txtSharyouName.Text = string.Empty;
            // 搬出種別
            this.form.txtHanshutsuShubetsu.Text = HANSHUTSUSHUBETSU_INIT;
            // 全てラジオボタン
            this.form.rdoAll.Checked = true;
            // 取引先CD
            this.form.txtTorihikisakiCd.Text = string.Empty;
            // 取引先名
            this.form.txtTorihikisakiName.Text = string.Empty;
            // 業者CD
            this.form.txtGyoushaCd.Text = string.Empty;
            // 業者名
            this.form.txtGyoushaName.Text = string.Empty;
            // 現場CD
            this.form.txtGenbaCd.Text = string.Empty;
            // 現場名
            this.form.txtGenbaName.Text = string.Empty;
            // 荷降業者CD
            this.form.txtNioroshiGyoushaCd.Text = string.Empty;
            // 荷降業者名
            this.form.txtNioroshiGyoushaName.Text = string.Empty;
            // 荷降現場CD
            this.form.txtNioroshiGenbaCd.Text = string.Empty;
            // 荷降現場名
            this.form.txtNioroshiGenbaName.Text = string.Empty;

            // 搬入予定一覧ラベル
            this.form.lblGridTitleHannyuYotei.Text = GRIDTITLE_HANNYUYOTEI_INIT;
            // 搬出予定一覧ラベル
            this.form.lblGridTitleHanshutsuYotei.Text = GRIDTITLE_HANSHUTSUYOTEI_1;

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
        /// <returns></returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            Type cType = this.GetType();
            string strButtonInfoXmlPath = cType.Namespace;
            strButtonInfoXmlPath += ".Setting.ButtonSetting.xml";
            LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, strButtonInfoXmlPath));

            return buttonSetting.LoadButtonSetting(thisAssembly, strButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            // 計量ボタン(F1)イベント
            parentForm.bt_func1.Click += new EventHandler(this.form.bt_func1_Click);

            // 受入ボタン(F2)イベント
            parentForm.bt_func2.Click += new EventHandler(this.form.bt_func2_Click);

            // 出荷ボタン(F3)イベント
            parentForm.bt_func3.Click += new EventHandler(this.form.bt_func3_Click);

            // 売上支払ボタン(F4)イベント
            parentForm.bt_func4.Click += new EventHandler(this.form.bt_func4_Click);

            // CSV出力ボタン(F6)イベント
            parentForm.bt_func6.Click += new EventHandler(this.form.bt_func6_Click);

            // 条件クリアボタン(F7)イベント
            parentForm.bt_func7.Click += new EventHandler(this.form.bt_func7_Click);

            // 検索ボタン(F8)イベント
            parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);

            // 画面切替ボタン(F9)イベント
            parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);

            // 搬入並替ボタン(F10)イベント
            parentForm.bt_func10.Click += new EventHandler(this.form.bt_func10_Click);

            // 搬出並替ボタン(F11)イベント
            parentForm.bt_func11.Click += new EventHandler(this.form.bt_func11_Click);

            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            // クローズイベント処理
            parentForm.FormClosing += new FormClosingEventHandler(SetPrevStatus);

            /// 20141203 Houkakou 「搬入出予定一覧」の日付チェックを追加する　start
            this.headerForm.dtpSagyouDateFrom.Leave += new System.EventHandler(dtpSagyouDateFrom_Leave);
            this.headerForm.dtpSagyouDateTo.Leave += new System.EventHandler(dtpSagyouDateTo_Leave);
            /// 20141203 Houkakou 「搬入出予定一覧」の日付チェックを追加する　end

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ファンクションキーのEnabled設定
        /// </summary>
        public void SetFuncEnabled()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            bool func1Flag = true;
            bool func2Flag = true;
            bool func3Flag = true;
            bool func4Flag = true;
            bool func10Flag = true;
            bool func11Flag = true;


            // 画面切替によるEnabled
            if (this.gamenkirikae == Gamenkirikae.OUT_IN)
            {
                func1Flag = true;
                func2Flag = true;
                func3Flag = true;
                func4Flag = true;
                func10Flag = true;
                func11Flag = true;
            }
            else if (this.gamenkirikae == Gamenkirikae.IN)
            {
                func1Flag = true;
                func2Flag = true;
                func3Flag = false;
                func4Flag = false;
                func10Flag = true;
                func11Flag = false;
            }
            else if (this.gamenkirikae == Gamenkirikae.OUT)
            {
                func1Flag = true;
                func2Flag = false;
                func3Flag = true;
                func4Flag = true;
                func10Flag = false;
                func11Flag = true;
            }

            // 伝種区分によるEnabled（画面切替の設定を上書きする）
            if (this.form.denshuKbn == DENSHU_KBN.KEIRYOU)
            {
                func2Flag = false;
                func3Flag = false;
                func4Flag = false;
            }
            else if (this.form.denshuKbn == DENSHU_KBN.UKEIRE)
            {
                func1Flag = false;
                func3Flag = false;
                func4Flag = false;
            }
            else if (this.form.denshuKbn == DENSHU_KBN.SHUKKA)
            {
                func1Flag = false;
                func2Flag = false;
                func4Flag = false;
            }
            else if (this.form.denshuKbn == DENSHU_KBN.URIAGE_SHIHARAI)
            {
                func1Flag = false;
                func2Flag = false;
                func3Flag = false;
            }

            // Enabled設定
            parentForm.bt_func1.Enabled = func1Flag;
            parentForm.bt_func2.Enabled = func2Flag;
            parentForm.bt_func3.Enabled = func3Flag;
            parentForm.bt_func4.Enabled = func4Flag;
            parentForm.bt_func10.Enabled = func10Flag;
            parentForm.bt_func11.Enabled = func11Flag;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// HeaderForm設定
        /// </summary>
        /// <param name="hs"></param>
        public void SetHeader(HannyushutsuIchiranHeader hs)
        {
            LogUtility.DebugMethodStart(hs);

            this.headerForm = hs;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region FUNCTION押下時

        #region [F1]計量押下時
        /// <summary>
        /// 計量ボタン押下時処理
        /// </summary>
        public bool Function1ClickLogic()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 受付番号選択チェック
                if (this.F1_UketsukeNumber == null)
                {
                    msgLogic.MessageBoxShow("E051", "遷移する行");
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                // データ存在チェック
                DataTable keiryouEntry = KeiryouEntryDao.GetDataByuketsukeNumber(this.F1_UketsukeNumber.ToString());
                if (keiryouEntry.Rows.Count > 0)
                {
                    msgLogic.MessageBoxShow("E003", "計量入力", "受付番号：" + this.F1_UketsukeNumber.ToString());
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                // 画面遷移
                FormManager.OpenForm("G045", WINDOW_TYPE.NEW_WINDOW_FLAG, -1, (long)this.F1_UketsukeNumber);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Function1ClickLogic", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Function1ClickLogic", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region [F2]受入押下時
        /// <summary>
        /// 受入ボタン押下時処理
        /// </summary>
        public bool Function2ClickLogic()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 受付番号選択チェック
                if (this.F2_UketsukeNumber == null)
                {
                    msgLogic.MessageBoxShow("E051", "遷移する行");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                // データ存在チェック
                DataTable ukeireEntry = UkeireEntryDao.GetDataByuketsukeNumber(this.F2_UketsukeNumber.ToString());
                if (ukeireEntry.Rows.Count > 0)
                {
                    msgLogic.MessageBoxShow("E003", "受入入力", "受付番号：" + this.F2_UketsukeNumber.ToString());
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                // 画面遷移
                FormManager.OpenForm("G051", WINDOW_TYPE.NEW_WINDOW_FLAG, -1, null, (long)this.F2_UketsukeNumber);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Function2ClickLogic", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Function2ClickLogic", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region [F3]出荷押下時
        /// <summary>
        /// 出荷ボタン押下時処理
        /// </summary>
        public bool Function3ClickLogic()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 受付番号選択チェック
                if (this.F3_F4_UketsukeNumber == null)
                {
                    msgLogic.MessageBoxShow("E051", "遷移する行");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                // データ存在チェック
                DataTable shukkaEntry = ShukkaEntryDao.GetDataByuketsukeNumber(this.F3_F4_UketsukeNumber.ToString());
                if (shukkaEntry.Rows.Count > 0)
                {
                    msgLogic.MessageBoxShow("E003", "出荷入力", "受付番号：" + this.F3_F4_UketsukeNumber.ToString());
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                // 画面遷移
                FormManager.OpenForm("G053", WINDOW_TYPE.NEW_WINDOW_FLAG, -1, null, (long)this.F3_F4_UketsukeNumber);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Function3ClickLogic", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Function3ClickLogic", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region [F4]売上支払押下時
        /// <summary>
        /// 売上支払ボタン押下時処理
        /// </summary>
        public bool Function4ClickLogic()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 受付番号選択チェック
                if (this.F3_F4_UketsukeNumber == null)
                {
                    msgLogic.MessageBoxShow("E051", "遷移する行");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                // データ存在チェック
                DataTable urShEntry = UrShEntryDao.GetDataByuketsukeNumber(this.F3_F4_UketsukeNumber.ToString());
                if (urShEntry.Rows.Count > 0)
                {
                    msgLogic.MessageBoxShow("E003", "売上支払入力", "受付番号：" + this.F3_F4_UketsukeNumber.ToString());
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                // 画面遷移
                FormManager.OpenForm("G054", WINDOW_TYPE.NEW_WINDOW_FLAG, -1, null, (long)this.F3_F4_UketsukeNumber);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Function4ClickLogic", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Function4ClickLogic", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region [F6]CSV出力押下時
        /// <summary>
        /// CSV出力ボタン押下時処理
        /// </summary>
        public void Function6ClickLogic()
        {
            LogUtility.DebugMethodStart();
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region [F7]条件クリア押下時
        /// <summary>
        /// 条件クリアボタン押下時処理
        /// </summary>
        public bool Function7ClickLogic()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 条件を初期化
                this.ObjectInit();

                // ソート条件をクリア
                this.form.sortHeaderHannyuYotei.ClearCustomSortSetting();
                this.form.sortHeaderHanshutsuYotei.ClearCustomSortSetting();

                // 搬入予定をクリア
                DataTable tmpHannyuYotei = (DataTable)this.form.dgvHannyuYotei.DataSource;
                if (tmpHannyuYotei != null)
                {
                    tmpHannyuYotei.Rows.Clear();
                    this.form.dgvHannyuYotei.DataSource = tmpHannyuYotei;
                }

                // 搬出予定をクリア
                DataTable tmpHanshutsuYotei = (DataTable)this.form.dgvHanshutsuYotei.DataSource;
                if (tmpHanshutsuYotei != null)
                {
                    tmpHanshutsuYotei.Rows.Clear();
                    this.form.dgvHanshutsuYotei.DataSource = tmpHanshutsuYotei;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Function7ClickLogic", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Function7ClickLogic", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region [F8]検索押下時
        /// <summary>
        /// 検索ボタン押下時処理
        /// </summary>
        public bool Function8ClickLogic()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 入力チェック
                if (this.headerForm.dtpSagyouDateFrom.Value != null && this.headerForm.dtpSagyouDateTo.Value == null)
                {
                    msgLogic.MessageBoxShow("E012", "終了日付");
                    this.headerForm.dtpSagyouDateTo.Focus();

                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                else if (this.headerForm.dtpSagyouDateFrom.Value == null && this.headerForm.dtpSagyouDateTo.Value != null)
                {
                    msgLogic.MessageBoxShow("E012", "開始日付");
                    this.headerForm.dtpSagyouDateFrom.Focus();

                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                /// 20141203 Houkakou 「搬入出予定一覧」の日付チェックを追加する　start
                if (this.DateCheck())
                {
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                /// 20141203 Houkakou 「搬入出予定一覧」の日付チェックを追加する　end

                this.SearchIchiran();
                this.SetIchiran();

                // 検索件数チェック
                if (this.form.dgvHanshutsuYotei.RowCount == 0 && this.form.dgvHannyuYotei.RowCount == 0)
                {
                    msgLogic.MessageBoxShow("C001");
                }

                // 初期化
                this.F1_UketsukeNumber = null;
                this.F2_UketsukeNumber = null;
                this.F3_F4_UketsukeNumber = null;
                this.form.dgvHanshutsuYotei.CurrentCell = null;
                this.form.dgvHannyuYotei.CurrentCell = null;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Function8ClickLogic", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Function8ClickLogic", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region [F9]画面切替押下時
        /// <summary>
        /// 画面切替ボタン押下時処理
        /// </summary>
        public bool Function9ClickLogic()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 搬出と搬入の場合、搬入に切替
                if (this.gamenkirikae == Gamenkirikae.OUT_IN)
                {
                    this.gamenkirikae = Gamenkirikae.IN;
                    if (this.form.dgvHannyuYotei.RowCount > 0)
                    {
                        this.F1_UketsukeNumber = (long?)this.form.dgvHannyuYotei.SelectedRows[0].Cells["HANNYU_UKETSUKE_NUMBER"].Value;
                        this.F2_UketsukeNumber = this.F1_UketsukeNumber;
                    }
                    this.F3_F4_UketsukeNumber = null;
                }
                // 搬入の場合、搬出に切替
                else if (this.gamenkirikae == Gamenkirikae.IN)
                {
                    this.gamenkirikae = Gamenkirikae.OUT;
                    if (this.form.dgvHannyuYotei.RowCount > 0)
                    {
                        this.F1_UketsukeNumber = (long?)this.form.dgvHanshutsuYotei.SelectedRows[0].Cells["HANSHUTSU_UKETSUKE_NUMBER"].Value;
                        this.F3_F4_UketsukeNumber = this.F1_UketsukeNumber;
                    }
                    this.F2_UketsukeNumber = null;
                }
                // 搬出の場合、搬出と搬入に切替
                else if (this.gamenkirikae == Gamenkirikae.OUT)
                {
                    this.gamenkirikae = Gamenkirikae.OUT_IN;
                    this.F1_UketsukeNumber = null;
                    this.F2_UketsukeNumber = null;
                    this.F3_F4_UketsukeNumber = null;
                }

                // 画面切替処理
                this.SetGamen();

                // ファンクションキーのEnabled設定
                this.SetFuncEnabled();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Function9ClickLogic", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region [F10]搬入並替押下時
        /// <summary>
        /// 搬入並替ボタン押下時処理
        /// </summary>
        public bool Function10ClickLogic()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.dgvHannyuYotei.Rows.Count < 1)
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                this.form.sortHeaderHannyuYotei.ShowCustomSortSettingDialog();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Function10ClickLogic", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region [F11]搬出並替押下時
        /// <summary>
        /// 搬出並替ボタン押下時処理
        /// </summary>
        public bool Function11ClickLogic()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.dgvHanshutsuYotei.Rows.Count < 1)
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                this.form.sortHeaderHanshutsuYotei.ShowCustomSortSettingDialog();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Function11ClickLogic", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region [F12]閉じる押下時
        /// <summary>
        /// 閉じる押下時処理
        /// </summary>
        public bool Function12ClickLogic()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.setSetting();

                var parentForm = (BusinessBaseForm)this.form.Parent;
                this.form.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Function12ClickLogic", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region Settingsの値の保存

        /// <summary>
        /// Settingsの値の保存
        /// </summary>
        public void setSetting()
        {
            LogUtility.DebugMethodStart();

            //拠点CD
            if (this.headerForm.txtKyotenCd.Text != "")
            {
                Properties.Settings.Default.SET_KYOTEN_CD = this.headerForm.txtKyotenCd.Text;
            }
            else
            {
                Properties.Settings.Default.SET_KYOTEN_CD = null;
            }

            // 保存
            Properties.Settings.Default.Save();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        /// <summary>
        /// クローズイベント時Settingsの値の保存処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SetPrevStatus(object sender, EventArgs e)
        {
            this.setSetting();
        }


        #endregion

        #region データ取得
        /// <summary>
        /// データ取得処理
        /// </summary>
        public virtual int SearchIchiran()
        {
            LogUtility.DebugMethodStart();

            int Count = 0;

            // 条件設定
            this.SearchString.KyotenCd = this.headerForm.txtKyotenCd.Text;
            if (this.headerForm.dtpSagyouDateFrom.Value == null)
            {
                this.SearchString.SagyouDateFrom = string.Empty;
            }
            else
            {
                this.SearchString.SagyouDateFrom = this.headerForm.dtpSagyouDateFrom.Value.ToString().Substring(0, 10);
            }
            if (this.headerForm.dtpSagyouDateTo.Value == null)
            {
                this.SearchString.SagyouDateTo = string.Empty;
            }
            else
            {
                this.SearchString.SagyouDateTo = this.headerForm.dtpSagyouDateTo.Value.ToString().Substring(0, 10);
            }
            this.SearchString.SharyouCd = this.form.txtSharyouCd.Text;
            this.SearchString.HanshutsuShubetsu = this.form.txtHanshutsuShubetsu.Text;
            this.SearchString.TorihikisakiCd = this.form.txtTorihikisakiCd.Text;
            this.SearchString.GyoushaCd = this.form.txtGyoushaCd.Text;
            this.SearchString.GenbaCd = this.form.txtGenbaCd.Text;
            this.SearchString.NioroshiGyoushaCd = this.form.txtNioroshiGyoushaCd.Text;
            this.SearchString.NioroshiGenbaCd = this.form.txtNioroshiGenbaCd.Text;

            // 搬入予定取得
            this.HannyuYoteiSearchResult = new DataTable();
            this.HannyuYoteiSearchResult = HannyuYoteiDao.GetDataForEntity(this.SearchString);

            // 搬出予定取得
            this.HanshutsuYoteiSearchResult = new DataTable();
            this.HanshutsuYoteiSearchResult = HanshutsuYoteiDao.GetDataForEntity(this.SearchString);

            LogUtility.DebugMethodEnd(Count);
            return Count;
        }
        #endregion

        #region データ表示
        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal void SetIchiran()
        {
            LogUtility.DebugMethodStart();

            // 搬出予定一覧ラベルの設定
            if (this.form.txtHanshutsuShubetsu.Text == "1")
            {
                this.form.lblGridTitleHanshutsuYotei.Text = GRIDTITLE_HANSHUTSUYOTEI_1;
            }
            else if (this.form.txtHanshutsuShubetsu.Text == "2")
            {
                this.form.lblGridTitleHanshutsuYotei.Text = GRIDTITLE_HANSHUTSUYOTEI_2;
            }
            else if (this.form.txtHanshutsuShubetsu.Text == "3")
            {
                this.form.lblGridTitleHanshutsuYotei.Text = GRIDTITLE_HANSHUTSUYOTEI_3;
            }

            // 搬入予定をクリア
            DataTable tmpHannyuYotei = (DataTable)this.form.dgvHannyuYotei.DataSource;
            if (tmpHannyuYotei != null)
            {
                tmpHannyuYotei.Rows.Clear();
                this.form.dgvHannyuYotei.DataSource = tmpHannyuYotei;
            }

            // 搬出予定をクリア
            DataTable tmpHanshutsuYotei = (DataTable)this.form.dgvHanshutsuYotei.DataSource;
            if (tmpHanshutsuYotei != null)
            {
                tmpHanshutsuYotei.Rows.Clear();
                this.form.dgvHanshutsuYotei.DataSource = tmpHanshutsuYotei;
            }

            var HannyuYoteitable = this.HannyuYoteiSearchResult;
            HannyuYoteitable.BeginLoadData();
            var HanshutsuYoteitable = this.HanshutsuYoteiSearchResult;
            HanshutsuYoteitable.BeginLoadData();

            // ソートヘッダーの内容でソートする
            this.form.sortHeaderHannyuYotei.SortDataTable(HannyuYoteitable);
            this.form.sortHeaderHanshutsuYotei.SortDataTable(HanshutsuYoteitable);

            // 列が自動的に作成されないようにする
            this.form.dgvHannyuYotei.AutoGenerateColumns = false;
            this.form.dgvHanshutsuYotei.AutoGenerateColumns = false;

            // データソースを設定する
            this.form.dgvHannyuYotei.DataSource = HannyuYoteitable;
            this.form.dgvHanshutsuYotei.DataSource = HanshutsuYoteitable;

            this.form.sortHeaderHannyuYotei.SortDataTable(HannyuYoteitable);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面切替処理
        /// <summary>
        /// 画面切替ボタン押下時処理
        /// </summary>
        private void SetGamen()
        {
            LogUtility.DebugMethodStart();

            int locationX = this.form.lblGridTitleHannyuYotei.Location.X;

            // 搬出と搬入
            if (this.gamenkirikae == Gamenkirikae.OUT_IN)
            {
                // 搬入予定一覧＝表示
                this.form.lblGridTitleHannyuYotei.Visible = true;
                this.form.lblGridTitleHannyuYotei.Location = new System.Drawing.Point(locationX, GRIDTITLE_1_Y_INIT);
                this.form.sortHeaderHannyuYotei.Visible = true;
                this.form.sortHeaderHannyuYotei.Location = new System.Drawing.Point(locationX, SORTHEADER_1_Y_INIT);
                this.form.dgvHannyuYotei.Visible = true;
                this.form.dgvHannyuYotei.Location = new System.Drawing.Point(locationX, GRID_1_Y_INIT);
                this.form.dgvHannyuYotei.Size = new System.Drawing.Size(this.form.dgvHannyuYotei.Size.Width, GRID_SINGLE_HEIGHT_INIT);

                // 搬出予定一覧＝表示
                this.form.lblGridTitleHanshutsuYotei.Visible = true;
                this.form.lblGridTitleHanshutsuYotei.Location = new System.Drawing.Point(locationX, GRIDTITLE_2_Y_INIT);
                this.form.sortHeaderHanshutsuYotei.Visible = true;
                this.form.sortHeaderHanshutsuYotei.Location = new System.Drawing.Point(locationX, SORTHEADER_2_Y_INIT);
                this.form.dgvHanshutsuYotei.Visible = true;
                this.form.dgvHanshutsuYotei.Location = new System.Drawing.Point(locationX, GRID_2_Y_INIT);
                this.form.dgvHanshutsuYotei.Size = new System.Drawing.Size(this.form.dgvHanshutsuYotei.Size.Width, GRID_SINGLE_HEIGHT_INIT);
            }
            // 搬入
            else if (this.gamenkirikae == Gamenkirikae.IN)
            {
                // 搬入予定一覧＝表示
                this.form.lblGridTitleHannyuYotei.Visible = true;
                this.form.lblGridTitleHannyuYotei.Location = new System.Drawing.Point(locationX, GRIDTITLE_1_Y_INIT);
                this.form.sortHeaderHannyuYotei.Visible = true;
                this.form.sortHeaderHannyuYotei.Location = new System.Drawing.Point(locationX, SORTHEADER_1_Y_INIT);
                this.form.dgvHannyuYotei.Visible = true;
                this.form.dgvHannyuYotei.Location = new System.Drawing.Point(locationX, GRID_1_Y_INIT);
                this.form.dgvHannyuYotei.Size = new System.Drawing.Size(this.form.dgvHannyuYotei.Size.Width, GRID_DOUBLE_HEIGHT_INIT);

                // 搬出予定一覧＝非表示
                this.form.lblGridTitleHanshutsuYotei.Visible = false;
                this.form.sortHeaderHanshutsuYotei.Visible = false;
                this.form.dgvHanshutsuYotei.Visible = false;
            }
            // 搬出
            else if (this.gamenkirikae == Gamenkirikae.OUT)
            {
                // 搬入予定一覧＝非表示
                this.form.lblGridTitleHannyuYotei.Visible = false;
                this.form.sortHeaderHannyuYotei.Visible = false;
                this.form.dgvHannyuYotei.Visible = false;

                // 搬出予定一覧＝表示
                this.form.lblGridTitleHanshutsuYotei.Visible = true;
                this.form.lblGridTitleHanshutsuYotei.Location = new System.Drawing.Point(locationX, GRIDTITLE_1_Y_INIT);
                this.form.sortHeaderHanshutsuYotei.Visible = true;
                this.form.sortHeaderHanshutsuYotei.Location = new System.Drawing.Point(locationX, SORTHEADER_1_Y_INIT);
                this.form.dgvHanshutsuYotei.Visible = true;
                this.form.dgvHanshutsuYotei.Location = new System.Drawing.Point(locationX, GRID_1_Y_INIT);
                this.form.dgvHanshutsuYotei.Size = new System.Drawing.Size(this.form.dgvHanshutsuYotei.Size.Width, GRID_DOUBLE_HEIGHT_INIT);
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region SQL実行

        #region 拠点マスタ
        /// <summary>
        /// 拠点マスタテーブル拠点名略称SELECT
        /// </summary>
        public String SelectKyotenNameRyaku(String kyotenCD)
        {
            LogUtility.DebugMethodStart(kyotenCD);

            M_KYOTEN mKyoten = new M_KYOTEN();
            mKyoten = (M_KYOTEN)mkyotenDao.GetDataByCd(kyotenCD);
            if (mKyoten == null)
            {
                LogUtility.DebugMethodEnd();
                return "";
            }
            else
            {
                LogUtility.DebugMethodEnd(mKyoten.KYOTEN_NAME_RYAKU);
                return mKyoten.KYOTEN_NAME_RYAKU;
            }
        }
        #endregion

        #region 車輌取得
        /// <summary>
        /// 車輌取得
        /// </summary>
        /// <param name="sharyouCd"></param>
        /// <returns></returns>
        public M_SHARYOU[] GetSharyou(string sharyouCd)
        {
            LogUtility.DebugMethodStart(sharyouCd);

            if (string.IsNullOrEmpty(sharyouCd))
            {
                LogUtility.DebugMethodEnd();
                return null;
            }

            M_SHARYOU keyEntity = new M_SHARYOU();
            keyEntity.SHARYOU_CD = sharyouCd;
            keyEntity.ISNOT_NEED_DELETE_FLG = true;
            var sharyous = this.msharyouDao.GetAllValidData(keyEntity);
            if (sharyous == null || sharyous.Length < 1)
            {
                LogUtility.DebugMethodEnd();
                return null;
            }

            LogUtility.DebugMethodEnd(sharyous);
            return sharyous;
        }
        #endregion

        #region 業者取得
        /// <summary>
        /// 業者取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_GYOUSHA GetGyousha(string gyoushaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd);

            if (string.IsNullOrEmpty(gyoushaCd))
            {
                LogUtility.DebugMethodEnd();
                return null;
            }

            M_GYOUSHA keyEntity = new M_GYOUSHA();
            keyEntity.GYOUSHA_CD = gyoushaCd;
            keyEntity.ISNOT_NEED_DELETE_FLG = true;
            var gyousha = this.mgyoushaDao.GetAllValidData(keyEntity);

            if (gyousha == null || gyousha.Length < 1)
            {
                LogUtility.DebugMethodEnd();
                return null;
            }
            else
            {
                LogUtility.DebugMethodEnd(gyousha[0]);
                return gyousha[0];
            }
        }
        #endregion

        #region 現場取得
        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA[] GetGenba(string gyoushaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            if (string.IsNullOrEmpty(genbaCd))
            {
                LogUtility.DebugMethodEnd();
                return null;
            }

            if (string.IsNullOrEmpty(gyoushaCd))
            {
                gyoushaCd = null;
            }

            M_GENBA keyEntity = new M_GENBA();
            keyEntity.GYOUSHA_CD = gyoushaCd;
            keyEntity.GENBA_CD = genbaCd;
            keyEntity.ISNOT_NEED_DELETE_FLG = true;
            var genba = this.mgenbaDao.GetAllValidData(keyEntity);

            if (genba == null || genba.Length < 1)
            {
                LogUtility.DebugMethodEnd();
                return null;
            }

            // PK指定のため1件
            LogUtility.DebugMethodEnd(genba);
            return genba;
        }
        #endregion

        #region 荷降現場取得
        /// <summary>
        /// 荷降現場取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public DataTable GetNioroshiGenba(string nioroshiGyoushaCd, string nioroshiGenbaCd)
        {
            LogUtility.DebugMethodStart(nioroshiGyoushaCd, nioroshiGenbaCd);

            if (string.IsNullOrEmpty(nioroshiGenbaCd))
            {
                LogUtility.DebugMethodEnd();
                return null;
            }

            // 検索SQLを作成
            System.Text.StringBuilder sql = new System.Text.StringBuilder();

            sql.Append("SELECT GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD");
            sql.Append("  FROM M_GENBA");
            sql.Append(" WHERE GENBA_CD = ");
            sql.Append("'");
            sql.Append(nioroshiGyoushaCd);
            sql.Append("'");
            if (!string.IsNullOrEmpty(nioroshiGyoushaCd))
            {
                sql.Append(" AND GYOUSHA_CD = '");
                sql.Append(nioroshiGyoushaCd);
                sql.Append("'");
            }
            sql.Append(" AND (SHOBUN_NIOROSHI_GENBA_KBN = 0 OR SAISHUU_SHOBUNJOU_KBN = 0)");

            var genba = this.mgenbaDao.GetDateForStringSql(sql.ToString());

            if (genba == null || genba.Rows.Count < 1)
            {
                LogUtility.DebugMethodEnd();
                return null;
            }

            // PK指定のため1件
            LogUtility.DebugMethodEnd(genba);
            return genba;
        }
        #endregion

        #endregion

        #region チェック

        #region 車輌チェック
        /// <summary>
        /// 車輌チェック
        /// </summary>
        internal bool CheckSharyou()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 変更チェック
                if (this.sharyouCd.Equals(this.form.txtSharyouCd.Text)
                    && gyoushaCd.Equals(this.form.txtGyoushaCd.Text)
                    && this.isSharyouCdError == false)
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                // 初期化
                this.form.txtSharyouName.Text = string.Empty;
                this.sharyouCd = this.form.txtSharyouCd.Text;
                this.gyoushaCd = this.form.txtGyoushaCd.Text;

                if (string.IsNullOrEmpty(this.form.txtSharyouCd.Text))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                var sharyouEntitys = this.GetSharyou(this.form.txtSharyouCd.Text);

                // マスタ存在チェック
                if (sharyouEntitys == null || sharyouEntitys.Length < 1)
                {
                    // エラーメッセージ
                    this.form.txtSharyouCd.IsInputErrorOccured = true;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "車輌");
                    this.form.txtSharyouCd.Focus();
                    this.isSharyouCdError = true;

                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                this.isSharyouCdError = false;

                // ポップアップから戻ってきたときに業者名が無いため取得
                if (!string.IsNullOrEmpty(this.form.txtGyoushaCd.Text) && string.IsNullOrEmpty(this.form.txtGyoushaName.Text))
                {
                    M_GYOUSHA gyousya = this.GetGyousha(this.form.txtGyoushaCd.Text);
                    if (gyousya == null)
                    {
                        // エラーメッセージ
                        this.form.txtSharyouCd.IsInputErrorOccured = true;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "業者");
                        this.form.txtSharyouCd.Focus();
                        this.isSharyouCdError = true;

                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                    else
                    {
                        this.form.txtGyoushaName.Text = gyousya.GYOUSHA_NAME_RYAKU;
                    }
                }

                if (!string.IsNullOrEmpty(this.form.txtGyoushaName.Text))
                {
                    M_SHARYOU sharyou = new M_SHARYOU();

                    // 業者チェック
                    bool isCheck = false;
                    foreach (M_SHARYOU sharyouEntity in sharyouEntitys)
                    {
                        if (sharyouEntity.GYOUSHA_CD.Equals(this.form.txtGyoushaCd.Text))
                        {
                            isCheck = true;
                            sharyou = sharyouEntity;
                            break;
                        }
                    }

                    if (isCheck)
                    {
                        // 車輌データセット
                        this.SetSharyou(sharyou);

                        LogUtility.DebugMethodEnd(true);
                        return true;
                    }
                    else
                    {
                        // エラーメッセージ
                        this.form.txtSharyouCd.IsInputErrorOccured = true;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "業者");
                        this.form.txtSharyouCd.Focus();
                        this.isSharyouCdError = true;

                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                }
                else
                {
                    if (sharyouEntitys.Length > 1)
                    {
                        // 複数レコード
                        this.sharyouCd = string.Empty;
                        this.gyoushaCd = string.Empty;
                        this.form.txtSharyouCd.Focus();

                        this.form.FocusOutErrorFlag = true;
                        // 検索ポップアップ起動
                        this.form.txtSharyouCd.PopupWindowName = "車両選択共通ポップアップ";
                        SendKeys.Send(" ");

                        this.form.FocusOutErrorFlag = false;

                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                    else
                    {
                        // 一意レコード
                        // 車輌データセット
                        this.SetSharyou(sharyouEntitys[0]);
                    }
                    this.isSharyouCdError = false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckSharyou", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckSharyou", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region 車輌情報をセット
        /// <summary>
        /// 車輌情報をセット
        /// </summary>
        /// <param name="sharyouEntity"></param>
        private void SetSharyou(M_SHARYOU sharyouEntity)
        {
            LogUtility.DebugMethodStart(sharyouEntity);

            this.form.txtSharyouName.Text = sharyouEntity.SHARYOU_NAME_RYAKU;

            // 業者情報セット
            var gyousha = this.GetGyousha(sharyouEntity.GYOUSHA_CD);
            if (gyousha != null)
            {
                this.form.txtGyoushaCd.Text = sharyouEntity.GYOUSHA_CD;
                this.form.txtGyoushaName.Text = gyousha.GYOUSHA_NAME_RYAKU;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 現場チェック
        /// <summary>
        /// 現場チェック
        /// </summary>
        internal bool ChechGenbaCd()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 変更チェック
                if (this.genbaCd.Equals(this.form.txtGenbaCd.Text) && this.isGenbaCdError == false)
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                bool isDataExist = true;

                // 初期化
                this.form.txtGenbaName.Text = string.Empty;
                this.genbaCd = this.form.txtGenbaCd.Text;

                if (string.IsNullOrEmpty(this.form.txtGenbaCd.Text))
                {
                    // コントロールを通常に戻す
                    this.isGenbaCdError = false;

                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                if (string.IsNullOrEmpty(this.form.txtGyoushaCd.Text))
                {
                    this.form.txtGenbaName.Text = string.Empty;

                    // コントロールをエラー状態にする
                    this.form.txtGenbaCd.IsInputErrorOccured = true;

                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E051", "業者");
                    this.form.txtGenbaCd.Focus();
                    this.isGenbaCdError = true;
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

                // データ検索
                var genba = GetGenba(this.form.txtGyoushaCd.Text, this.form.txtGenbaCd.Text);
                if (genba == null)
                {
                    isDataExist = false;
                }

                // データ有無判定
                if (isDataExist == true)
                {
                    if (genba.Length > 1)
                    {
                        // 複数レコード
                        this.genbaCd = string.Empty;
                        this.form.txtGenbaCd.Focus();

                        this.form.FocusOutErrorFlag = true;
                        // 検索ポップアップ起動
                        this.form.txtGenbaCd.PopupWindowName = "複数キー用検索共通ポップアップ";
                        SendKeys.Send(" ");

                        this.form.FocusOutErrorFlag = false;

                    }
                    else
                    {
                        // データ設定
                        this.form.txtGenbaName.Text = genba[0].GENBA_NAME_RYAKU;
                        this.form.txtGyoushaCd.Text = genba[0].GYOUSHA_CD;
                        M_GYOUSHA gyousya = this.GetGyousha(genba[0].GYOUSHA_CD);
                        if (gyousya != null)
                        {
                            this.form.txtGyoushaName.Text = gyousya.GYOUSHA_NAME_RYAKU;
                        }
                    }

                    // コントロールを通常に戻す
                    this.isGenbaCdError = false;
                }
                else
                {
                    this.form.txtGenbaName.Text = string.Empty;
                    //this.form.txtGyoushaCd.Text = string.Empty;

                    // コントロールをエラー状態にする
                    this.form.txtGenbaCd.IsInputErrorOccured = true;

                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.form.txtGenbaCd.Focus();
                    this.isGenbaCdError = true;
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChechGenbaCd", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechGenbaCd", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region 荷降業者チェック
        /// <summary>
        /// 荷降業者チェック
        /// </summary>
        internal bool CheckNioroshiGyoushaCd()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool isDataExist = true;

                // 初期化
                this.form.txtNioroshiGyoushaName.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.txtNioroshiGyoushaCd.Text))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                // PKは1つなので複数ヒットしない
                var gyousha = this.GetGyousha(this.form.txtNioroshiGyoushaCd.Text);
                if (gyousha == null)
                {
                    isDataExist = false;
                }

                // 処分受託者=TRUE or 荷降業者=TRUE
                if (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue)
                {
                    isDataExist = true;
                }
                else
                {
                    isDataExist = false;
                }

                // データ有無判定
                if (isDataExist == true)
                {
                    // データ設定
                    this.form.txtNioroshiGyoushaName.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.form.txtNioroshiGyoushaName.Text = string.Empty;

                    // コントロールをエラー状態にする
                    this.form.txtNioroshiGyoushaCd.IsInputErrorOccured = true;

                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.form.txtNioroshiGyoushaCd.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNioroshiGyoushaCd", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNioroshiGyoushaCd", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region 荷降現場チェック
        /// <summary>
        /// 荷降現場チェック
        /// </summary>
        internal bool ChechNioroshiGenbaCd()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 変更チェック
                if (this.nioroshiGenbaCd.Equals(this.form.txtNioroshiGenbaCd.Text) && this.isNioroshiGenbaCdError == false)
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                bool isDataExist = true;

                // 初期化
                this.form.txtNioroshiGenbaName.Text = string.Empty;
                this.nioroshiGenbaCd = this.form.txtNioroshiGenbaCd.Text;

                if (string.IsNullOrEmpty(this.form.txtNioroshiGenbaCd.Text))
                {
                    // コントロールを通常に戻す
                    this.isNioroshiGenbaCdError = false;

                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                if (string.IsNullOrEmpty(this.form.txtNioroshiGyoushaCd.Text))
                {
                    this.form.txtNioroshiGenbaName.Text = string.Empty;

                    // コントロールをエラー状態にする
                    this.form.txtNioroshiGenbaCd.IsInputErrorOccured = true;

                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E051", "荷降業者");
                    this.form.txtNioroshiGenbaCd.Focus();
                    this.isNioroshiGenbaCdError = true;
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

                // データ検索
                var genba = GetGenba(this.form.txtNioroshiGyoushaCd.Text, this.form.txtNioroshiGenbaCd.Text);
                if (genba == null)
                {
                    isDataExist = false;
                }
                else
                {
                    isDataExist = true;
                }

                // データ有無判定
                if (isDataExist == true)
                {
                    if (genba.Length > 1)
                    {
                        // 複数レコード
                        this.nioroshiGenbaCd = string.Empty;
                        this.form.txtNioroshiGenbaCd.Focus();

                        this.form.FocusOutErrorFlag = true;
                        // 検索ポップアップ起動
                        this.form.txtNioroshiGenbaCd.PopupWindowName = "複数キー用検索共通ポップアップ";
                        SendKeys.Send(" ");

                        this.form.FocusOutErrorFlag = false;

                    }
                    else
                    {
                        // データ設定
                        this.form.txtNioroshiGenbaName.Text = genba[0].GENBA_NAME_RYAKU;
                        this.form.txtNioroshiGyoushaCd.Text = genba[0].GYOUSHA_CD;
                        M_GYOUSHA gyousya = this.GetGyousha(genba[0].GYOUSHA_CD);
                        if (gyousya != null)
                        {
                            this.form.txtNioroshiGyoushaName.Text = gyousya.GYOUSHA_NAME_RYAKU;
                        }
                    }

                    // コントロールを通常に戻す
                    this.isNioroshiGenbaCdError = false;
                }
                else
                {
                    this.form.txtNioroshiGenbaName.Text = string.Empty;
                    //this.form.txtNioroshiGyoushaCd.Text = string.Empty;

                    // コントロールをエラー状態にする
                    this.form.txtNioroshiGenbaCd.IsInputErrorOccured = true;

                    // エラーメッセージ
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.form.txtNioroshiGenbaCd.Focus();
                    this.isNioroshiGenbaCdError = true;
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChechNioroshiGenbaCd", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechNioroshiGenbaCd", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #endregion

        #region 搬入予定一覧受付番号取得
        /// <summary>
        /// 搬入予定一覧受付番号取得
        /// </summary>
        /// <param name="e"></param>
        internal bool GetHannyuYoteiUketsukeNumber(DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                this.F1_UketsukeNumber = (long?)this.form.dgvHannyuYotei["HANNYU_UKETSUKE_NUMBER", e.RowIndex].Value;
                this.F2_UketsukeNumber = this.F1_UketsukeNumber;
                this.F3_F4_UketsukeNumber = null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetHannyuYoteiUketsukeNumber", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region 搬出予定一覧受付番号取得
        /// <summary>
        /// 搬出予定一覧受付番号取得
        /// </summary>
        /// <param name="e"></param>
        internal bool GetHanshutsuYoteiUketsukeNumber(DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                this.F1_UketsukeNumber = (long)this.form.dgvHanshutsuYotei["HANSHUTSU_UKETSUKE_NUMBER", e.RowIndex].Value;
                this.F2_UketsukeNumber = null;
                this.F3_F4_UketsukeNumber = this.F1_UketsukeNumber;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetHanshutsuYoteiUketsukeNumber", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region メソッド
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

        public void setHeaderForm(HannyushutsuIchiranHeader hs)
        {
            this.headerForm = hs;
        }

        #endregion

        /// 20141203 Houkakou 「搬入出予定一覧」の日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.headerForm.dtpSagyouDateFrom.BackColor = Constans.NOMAL_COLOR;
            this.headerForm.dtpSagyouDateTo.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.headerForm.dtpSagyouDateFrom.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.headerForm.dtpSagyouDateTo.Text))
            {
                return false;
            }

            DateTime date_from = Convert.ToDateTime(this.headerForm.dtpSagyouDateFrom.Value);
            DateTime date_to = Convert.ToDateTime(this.headerForm.dtpSagyouDateTo.Value);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.headerForm.dtpSagyouDateFrom.IsInputErrorOccured = true;
                this.headerForm.dtpSagyouDateTo.IsInputErrorOccured = true;
                this.headerForm.dtpSagyouDateFrom.BackColor = Constans.ERROR_COLOR;
                this.headerForm.dtpSagyouDateTo.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "作業日From", "作業日To" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.headerForm.dtpSagyouDateFrom.Focus();
                return true;
            }

            return false;
        }
        #endregion

        #region dtpSagyouDateFrom_Leaveイベント
        /// <summary>
        /// dtpSagyouDateFrom_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void dtpSagyouDateFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.headerForm.dtpSagyouDateTo.Text))
            {
                this.headerForm.dtpSagyouDateTo.IsInputErrorOccured = false;
                this.headerForm.dtpSagyouDateTo.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region dtpSagyouDateTo_Leaveイベント
        /// <summary>
        /// dtpSagyouDateTo_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void dtpSagyouDateTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.headerForm.dtpSagyouDateFrom.Text))
            {
                this.headerForm.dtpSagyouDateFrom.IsInputErrorOccured = false;
                this.headerForm.dtpSagyouDateFrom.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion
        /// 20141203 Houkakou 「搬入出予定一覧」の日付チェックを追加する　end

        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        #region 業者変化時、現場削除処理
        /// <summary>
        /// 業者変化時、現場削除処理
        /// </summary>
        /// <param name="e"></param>
        internal bool GyousyaCheck(string ZengyousyaCD)
        {
            try
            {
                LogUtility.DebugMethodStart(ZengyousyaCD);

                if (string.IsNullOrEmpty(this.form.txtGyoushaCd.Text) || this.form.txtGyoushaCd.Text != ZengyousyaCD)
                {
                    this.form.txtGenbaCd.Text = string.Empty;
                    this.form.txtGenbaName.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyousyaCheck", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region 荷降業者変化時、現場削除処理
        /// <summary>
        /// 荷降業者変化時、現場削除処理
        /// </summary>
        /// <param name="e"></param>
        internal bool NiorosiGyousyaCheck(string ZenNiorosigyousyaCD)
        {
            try
            {
                LogUtility.DebugMethodStart(ZenNiorosigyousyaCD);

                if (string.IsNullOrEmpty(this.form.txtNioroshiGyoushaCd.Text) || this.form.txtNioroshiGyoushaCd.Text != ZenNiorosigyousyaCD)
                {
                    this.form.txtNioroshiGenbaCd.Text = string.Empty;
                    this.form.txtNioroshiGenbaName.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("NiorosiGyousyaCheck", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion
        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
    }
}
