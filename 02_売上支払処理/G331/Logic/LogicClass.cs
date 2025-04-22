using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
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
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.SalesPayment.TukigimeUriageDenpyoSakusei.DAO;
using Shougun.Function.ShougunCSCommon.Const;

namespace Shougun.Core.SalesPayment.TukigimeUriageDenpyoSakusei
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region プロパティ

        /// <summary>
        /// 検索結果①
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public DTOClass SearchString { get; set; }

        // 20151021 katen #13337 品名手入力に関する機能修正 start
        private IM_KOBETSU_HINMEIDao mkhDao;
        // 20151021 katen #13337 品名手入力に関する機能修正 end

        /// <summary>
        /// BusinessCommonのDBAccesser
        /// </summary>
        private Shougun.Core.Common.BusinessCommon.DBAccessor commonAccesser;
        #endregion プロパティ

        #region フィールド

        /// <summary>
        /// 社員マスタDAO
        /// </summary>
        private MSDaoCls msDao;

        /// <summary>
        /// 現場マスタDAO
        /// </summary>
        private MGDaoCls mgDao;

        /// <summary>
        /// 消費税マスタDAO
        /// </summary>
        private MShoDaoCls mshoDao;

        /// <summary>
        /// 取引先_請求情報マスタDAO
        /// </summary>
        private MTSeikyuuDaoCls mtseikyuuDao;

        /// <summary>
        /// 取引先_支払情報マスタDAO
        /// </summary>
        private MTShiharaiDaoCls mtshiharaiDao;

        /// <summary>
        /// 現場_月極品名マスタDAO
        /// </summary>
        private MGTHDaoCls mgthDao;

        /// <summary>
        /// 品名マスタDAO
        /// </summary>
        private MHDaoCls mhDao;

        /// <summary>
        /// 業者マスタDAO
        /// </summary>
        private MGyoushaDaoCls mGyoushaDao;

        /// <summary>
        /// 取引先マスタDAO
        /// </summary>
        private MTorihikiDaoCls mTorihikisakiDao;

        /// <summary>
        /// 取引区分マスタDAO
        /// </summary>
        private MTKDaoCls mtkDao;

        /// <summary>
        /// 売上／支払入力DAO
        /// </summary>
        private TUSEDaoCls tuseDao;

        /// <summary>
        /// 売上／支払明細DAO
        /// </summary>
        private TUSDDaoCls tusdDao;

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// フォーム
        /// </summary>
        private UIForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// ヘッダー
        /// </summary>
        private UIHeader header;

        /// <summary>
        /// メッセージボックス
        /// </summary>
        private MessageBoxShowLogic errMsg = new MessageBoxShowLogic();

        /// <summary>
        /// 現場_月極品名マスタ
        /// </summary>
        private M_GENBA_TSUKI_HINMEI mgth = new M_GENBA_TSUKI_HINMEI();

        /// <summary>
        /// 連番カウント
        /// </summary>
        private int rowCount = 0;

        /// <summary>
        /// DBアクセッサー
        /// </summary>
        private Shougun.Core.SalesPayment.TukigimeUriageDenpyoSakusei.Accessor.DBAccessor accessor;

        /// <summary>
        /// システムID
        /// </summary>
        private SqlInt64 sysId = 0;

        /// <summary>
        /// 売上／支払番号
        /// </summary>
        private SqlInt64 urshNum = 0;

        /// <summary>
        /// コンボボックス初期化フラグ
        /// </summary>
        private bool bInitialize = true;

        /// <summary>
        /// 月極区分判定リスト
        /// </summary>
        private Dictionary<string, int> dicCount = new Dictionary<string, int>();

        /// <summary>
        /// DAO
        /// </summary>
        private DAOClass daoC;

        /// <summary>
        /// 売上日付エラー判定
        /// </summary>
        private bool errdtpSeikyuu = false;

        /// <summary>
        /// 登録後再検索条件：拠点CD
        /// </summary>
        private string researchKyotenCD = string.Empty;

        /// <summary>
        /// 登録後再検索条件：締日
        /// </summary>
        private string researchShimebi = string.Empty;

        /// <summary>
        /// 登録後再検索条件：対象期間From
        /// </summary>
        private DateTime researchTaishouDateFrom;

        /// <summary>
        /// 登録後再検索条件：対象期間To
        /// </summary>
        private DateTime researchTaishouDateTo;

        /// <summary>
        /// 登録後再検索条件：取引先CD
        /// </summary>
        private string researchTorihikisakiCD = string.Empty;

        /// <summary>
        /// 登録後再検索条件：業者CD
        /// </summary>
        private string researchGyousyaCD = string.Empty;

        /// <summary>
        /// 登録後再検索条件：現場CD
        /// </summary>
        private string researchGenbaCD = string.Empty;

        /// <summary>
        /// 登録後再検索条件：売上日付
        /// </summary>
        private DateTime researchSeikyuuDate;

        /// <summary>
        /// 端数処理種別
        /// </summary>
        private enum fractionType : int
        {
            CEILING = 1,	// 切り上げ
            FLOOR,		// 切り捨て
            ROUND,		// 四捨五入
        }

        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">メインフォーム</param>
        /// <param name="targetHeader">ヘッダー</param>
        public LogicClass(UIForm targetForm, UIHeader targetHeader)
        {
            LogUtility.DebugMethodStart(targetForm, targetHeader);

            this.form = targetForm;
            this.dto = new DTOClass();
            this.header = targetHeader;
            this.header.logic = this;
            this.SearchResult = new DataTable();
            this.SearchString = new DTOClass();
            this.accessor = new Shougun.Core.SalesPayment.TukigimeUriageDenpyoSakusei.Accessor.DBAccessor();
            this.msDao = DaoInitUtility.GetComponent<MSDaoCls>();
            this.mgDao = DaoInitUtility.GetComponent<MGDaoCls>();
            this.mshoDao = DaoInitUtility.GetComponent<MShoDaoCls>();
            this.mtseikyuuDao = DaoInitUtility.GetComponent<MTSeikyuuDaoCls>();
            this.mtshiharaiDao = DaoInitUtility.GetComponent<MTShiharaiDaoCls>();
            this.mgthDao = DaoInitUtility.GetComponent<MGTHDaoCls>();
            this.mhDao = DaoInitUtility.GetComponent<MHDaoCls>();
            this.mGyoushaDao = DaoInitUtility.GetComponent<MGyoushaDaoCls>();
            this.mTorihikisakiDao = DaoInitUtility.GetComponent<MTorihikiDaoCls>();
            this.mtkDao = DaoInitUtility.GetComponent<MTKDaoCls>();
            this.tuseDao = DaoInitUtility.GetComponent<TUSEDaoCls>();
            this.tusdDao = DaoInitUtility.GetComponent<TUSDDaoCls>();
            this.daoC = new DAOClass();
            this.researchSeikyuuDate = DateTime.Now;
            // 20151021 katen #13337 品名手入力に関する機能修正 start
            this.mkhDao = DaoInitUtility.GetComponent<IM_KOBETSU_HINMEIDao>();
            // 20151021 katen #13337 品名手入力に関する機能修正 end
            this.commonAccesser = new Shougun.Core.Common.BusinessCommon.DBAccessor();
            LogUtility.DebugMethodEnd();
        }

        #endregion コンストラクタ

        #region 初期処理

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

                // 親フォームオブジェクト取得
                parentForm = (BusinessBaseForm)this.form.Parent;

                this.researchSeikyuuDate = this.parentForm.sysDate;

                //　ログイン情報取得処理
                GetLoginInfo();

                // 初期表示情報設定処理
                InitSetData();
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

            LogUtility.DebugMethodEnd(ret);
            return ret;
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
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            string ButtonInfoXmlPath = this.GetType().Namespace;
            ButtonInfoXmlPath = ButtonInfoXmlPath + ".Setting.ButtonSetting.xml";

            LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath));
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //前月ボタン(F3)イベント生成
            parentForm.bt_func3.Click += new EventHandler(this.form.Function3Click);

            //翌月ボタン(F4)イベント生成
            parentForm.bt_func4.Click += new EventHandler(this.form.Function4Click);

            //一覧ボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.Function7Click);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Function8Click);

            //実行ボタン(F9)イベント生成
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Function9Click);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.Function12Click);

            // 20141201 teikyou ダブルクリックを追加する　start
            this.form.dtpTaishoKikanTo.MouseDoubleClick += new MouseEventHandler(dtpTaishoKikanTo_MouseDoubleClick);
            // 20141201 teikyou ダブルクリックを追加する　end

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 初期データ設定処理
        /// </summary>
        private void InitSetData()
        {
            LogUtility.DebugMethodStart();

            DateTime dtToday = this.parentForm.sysDate; // 現在日付
            DateTime dtPrevMonth = dtToday.AddMonths(-1);
            DateTime dtEndDateOfMonth = new DateTime(dtToday.Year, dtToday.Month,   //月末日
                                        DateTime.DaysInMonth(dtToday.Year, dtToday.Month));

            //デフォルトで設定される締め日を求める。
            //締め日が5日単位で切り替わるので、5で割った商の値に
            //5を掛けることで締日を求める。
            //0の場合は、31を設定

            int iShimebi = 0;
            int iValue = dtToday.Day / 5;
            if (iValue == 0)
            {
                iShimebi = dtEndDateOfMonth.Day;
                form.cmbShimebi.SelectedIndex = 6;
            }
            else
            {
                iShimebi = iValue * 5;
                form.cmbShimebi.SelectedIndex = iValue;
            }

            bInitialize = false;

            //日付再設定処理
            ResetDate();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 日付再設定処理
        /// </summary>
        private void ResetDate()
        {
            LogUtility.DebugMethodStart();

            if (!bInitialize)
            {
                DateTime dtToday = this.parentForm.sysDate; // 現在日付
                DateTime dtPrevMonth = dtToday.AddMonths(-1);
                DateTime dtPrevBackMonth = dtPrevMonth.AddMonths(-1);

                int iShimebi = int.Parse(form.cmbShimebi.Items[form.cmbShimebi.SelectedIndex].ToString());

                //対象期間-FROM,対象期間-TOの設定
                if (iShimebi == 31)
                {
                    //前月の末日を取得(今月1日の位置に前)
                    DateTime dtEndDateOfPrevMonth = new DateTime(dtToday.Year, dtToday.Month, 1).AddDays(-1);
                    //Fromに前月の1日、Toに前月の末日を設定
                    form.dtpTaishoKikanFrom.Value = new DateTime(dtPrevMonth.Year, dtPrevMonth.Month, 1);
                    form.dtpTaishoKikanTo.Value = dtEndDateOfPrevMonth;
                }
                else
                {
                    if (iShimebi >= dtToday.Day)
                    {
                        form.dtpTaishoKikanFrom.Value = new DateTime(dtPrevBackMonth.Year, dtPrevBackMonth.Month, iShimebi + 1);
                        form.dtpTaishoKikanTo.Value = new DateTime(dtPrevMonth.Year, dtPrevMonth.Month, iShimebi);
                    }
                    else
                    {
                        form.dtpTaishoKikanFrom.Value = new DateTime(dtPrevMonth.Year, dtPrevMonth.Month, iShimebi + 1);
                        form.dtpTaishoKikanTo.Value = new DateTime(dtToday.Year, dtToday.Month, iShimebi);
                    }
                }

                //売上日付
                form.dtpSeikyuDate.Value = form.dtpTaishoKikanTo.Value;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion 初期処理

        #region メソッド

        /// <summary>
        /// 現場チェック
        /// </summary>
        internal bool CheckGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 初期化
                this.form.txtGenbaName.Text = string.Empty;
                this.form.txtGenbaName.ReadOnly = true;

                if (string.IsNullOrEmpty(this.form.txtGenbaCd.Text))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                if (string.IsNullOrEmpty(this.form.txtGyosyaCd.Text))
                {
                    // エラーメッセージ
                    errMsg.MessageBoxShow("E051", "業者");
                    this.form.txtGenbaCd.Text = string.Empty;
                    this.form.txtGenbaCd.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                var genbaEntity = daoC.GetGenba(this.form.txtGyosyaCd.Text, this.form.txtGenbaCd.Text);
                if (genbaEntity == null)
                {
                    errMsg.MessageBoxShow(ConstInfo.ERR_MSG_CD_E011, ConstInfo.ERR_ARGS_GENBA_MASTER);
                    this.form.txtGenbaCd.Focus();
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                bool isContinue = false;
                M_GENBA mGenba = new M_GENBA();

                foreach (M_GENBA entity in genbaEntity)
                {
                    if (this.form.txtGyosyaCd.Text.Equals(entity.GYOUSHA_CD))
                    {
                        isContinue = true;
                        mGenba = entity;
                        this.form.txtGenbaName.Text = entity.GENBA_NAME_RYAKU;
                        break;
                    }
                }

                if (!isContinue)
                {
                    // 一致するものがないのでエラー
                    errMsg.MessageBoxShow(ConstInfo.ERR_MSG_CD_E011, ConstInfo.ERR_ARGS_GENBA_MASTER);
                    this.form.txtGenbaCd.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
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

        #region ログイン情報取得処理

        /// <summary>
        /// ログイン情報取得処理
        /// </summary>
        public void GetLoginInfo()
        {
            LogUtility.DebugMethodStart();

            //ユーザ情報の登録
            XMLAccessor fileAccess = new XMLAccessor();
            CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();

            int kyotenCd = 0;
            if (int.TryParse(configProfile.ItemSetVal1, out kyotenCd))
            {
                this.form.txtKyotenCd.Text = String.Format("{0:D2}", kyotenCd);
            }
            else
            {
                this.form.txtKyotenCd.Text = string.Empty;
            }

            // ユーザ拠点名称の取得
            IM_KYOTENDao daoKyoten = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            M_KYOTEN mKyoten = null;

            // 拠点CDが空の場合、拠点CD=0で検索されるためチェック
            if (!string.IsNullOrEmpty(this.form.txtKyotenCd.Text))
            {
                mKyoten = (M_KYOTEN)daoKyoten.GetDataByCd(this.form.txtKyotenCd.Text);
            }

            if (mKyoten == null)
            {
                this.form.txtKyotenName.Text = "";
            }
            else
            {
                this.form.txtKyotenName.Text = mKyoten.KYOTEN_NAME_RYAKU;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion ログイン情報取得処理

        #region 未入力チェック

        //public bool InputCheck(out String strErrorKomoku, out Object focusControl)
        //{
        //    strErrorKomoku = null;
        //    focusControl = null;

        //    LogUtility.DebugMethodStart();

        //    bool checkOk = true;
        //    String separator = ",";

        //    //締日
        //    if (this.header.dtpTaishoKikanFrom.Text == string.Empty)
        //    {
        //        checkOk = false;
        //        errKomoku += DENPYOSHURUI;

        //        if (focusControl == null)
        //        {
        //            focusControl = (Object)this.header.dtpTaishoKikanFrom;
        //        }
        //    }

        //    //

        //    LogUtility.DebugMethodEnd(errKomoku, focusControl, checkOk);
        //    return checkOk;
        //}

        #endregion 未入力チェック

        #region 締日変更時処理

        /// <summary>
        /// 締日変更時処理
        /// </summary>
        public bool ChangeShimebiProcess()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (form.cmbShimebi.SelectedIndex == 0)
                {
                    form.dtpTaishoKikanFrom.Enabled = true;
                    form.dtpTaishoKikanTo.Enabled = true;
                }
                else
                {
                    form.dtpTaishoKikanFrom.Enabled = false;
                    form.dtpTaishoKikanTo.Enabled = false;

                    // 日付再設定処理
                    ResetDate();
                }

                this.form.txtTorihikisakiCd.Text = String.Empty;
                this.form.txtTorihikisakiName.Text = String.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeShimebiProcess", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion 締日変更時処理

        #region 検索

        /// <summary>
        /// 検索
        /// </summary>
        internal bool ExecuteSearch(bool isResearch = false)
        {
            LogUtility.DebugMethodStart();

            try
            {
                if (isResearch)
                {
                    // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 start
                    if (CheckDate())
                    {
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                    // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 end
                    // thongh 20150803 #11932 拠点必須のアラート表示 start
                    if (this.CheckKyotenToSearch())
                    {
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                    // thongh 20150803 #11932 拠点必須のアラート表示 end
                    if (this.CheckSeikyuDateToSearch())
                    {
                        return false;
                    }
                    //登録後再検索の場合

                    this.SearchString.KyotenCD = researchKyotenCD;
                    this.SearchString.Shimebi = researchShimebi;
                    this.SearchString.TaishouDateFrom = researchTaishouDateFrom;
                    this.SearchString.TaishouDateTo = researchTaishouDateTo;
                    this.SearchString.TorihikisakiCD = researchTorihikisakiCD;
                    this.SearchString.GyousyaCD = researchGyousyaCD;
                    this.SearchString.GenbaCD = researchGenbaCD;
                    this.SearchString.SeikyuuDate = researchSeikyuuDate;

                    this.SearchResult = mgDao.GetSearch(this.SearchString);
                }
                else
                {
                    //締日が0の場合、未入力チェック
                    if (form.cmbShimebi.Text == "0")
                    {
                        if (ErrCheck() == false)
                        {
                            LogUtility.DebugMethodEnd(false);
                            return false;
                        }
                    }

                    //日付チェック
                    //日付チェック
                    if (!ChangeSeikyuuDate()
                        || !UninputCheckFrom()
                        || !UninputCheckTo())
                    {
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    //対象データの抽出
                    //拠点CD
                    if (!string.IsNullOrEmpty(this.form.txtKyotenCd.Text))
                    {
                        this.SearchString.KyotenCD = this.form.txtKyotenCd.Text;
                    }
                    else
                    {
                        this.SearchString.KyotenCD = null;
                    }

                    //締日
                    if (!string.IsNullOrEmpty(this.form.cmbShimebi.Text))
                    {
                        this.SearchString.Shimebi = this.form.cmbShimebi.Text;
                    }
                    else
                    {
                        this.SearchString.Shimebi = null;
                    }

                    // 対象期間
                    if (!String.IsNullOrEmpty(this.form.dtpTaishoKikanFrom.Text))
                    {
                        this.SearchString.TaishouDateFrom = DateTime.Parse(this.form.dtpTaishoKikanFrom.Text);
                    }
                    else
                    {
                        this.SearchString.TaishouDateFrom = DateTime.Parse(null);
                    }
                    if (!String.IsNullOrEmpty(this.form.dtpTaishoKikanTo.Text))
                    {
                        this.SearchString.TaishouDateTo = DateTime.Parse(this.form.dtpTaishoKikanTo.Text);
                    }
                    else
                    {
                        this.SearchString.TaishouDateTo = DateTime.Parse(null);
                    }

                    //取引先CD
                    if (!string.IsNullOrEmpty(this.form.txtTorihikisakiCd.Text))
                    {
                        this.SearchString.TorihikisakiCD = this.form.txtTorihikisakiCd.Text;
                    }
                    else
                    {
                        this.SearchString.TorihikisakiCD = null;
                    }

                    //業者CD
                    if (!string.IsNullOrEmpty(this.form.txtGyosyaCd.Text))
                    {
                        this.SearchString.GyousyaCD = this.form.txtGyosyaCd.Text;
                    }
                    else
                    {
                        this.SearchString.GyousyaCD = null;
                    }

                    //現場CD
                    if (!string.IsNullOrEmpty(this.form.txtGenbaCd.Text))
                    {
                        this.SearchString.GenbaCD = this.form.txtGenbaCd.Text;
                    }
                    else
                    {
                        this.SearchString.GenbaCD = null;
                    }

                    //売上日付
                    if (!string.IsNullOrEmpty(this.form.dtpSeikyuDate.Text))
                    {
                        this.SearchString.SeikyuuDate = DateTime.Parse(this.form.dtpSeikyuDate.Text);
                    }
                    else
                    {
                        this.SearchString.SeikyuuDate = DateTime.Parse(null);
                    }

                    this.SearchResult = mgDao.GetSearch(this.SearchString);

                    //データが0件になった場合
                    if (this.SearchResult.Rows.Count == 0)
                    {
                        if (this.form.mrwTukigimeUriageDenpyo.RowCount != 0)
                            this.form.mrwTukigimeUriageDenpyo.Rows.Remove(0, this.form.mrwTukigimeUriageDenpyo.Rows.Count);
                        errMsg.MessageBoxShow(ConstInfo.ERR_MSG_CD_C001);
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                }

                this.SearchResult = Group(this.SearchResult);

                //一覧表示
                MakeRows();

                //登録後再検索のために現在の検索条件を保持
                researchKyotenCD = this.SearchString.KyotenCD;
                researchShimebi = this.SearchString.Shimebi;
                researchTaishouDateFrom = this.SearchString.TaishouDateFrom;
                researchTaishouDateTo = this.SearchString.TaishouDateTo;
                researchTorihikisakiCD = this.SearchString.TorihikisakiCD;
                researchGyousyaCD = this.SearchString.GyousyaCD;
                researchGenbaCD = this.SearchString.GenbaCD;
                researchSeikyuuDate = this.SearchString.SeikyuuDate;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ExecuteSearch", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ExecuteSearch", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion 検索

        #region 一覧表示

        /// <summary>
        /// 一覧表示
        /// </summary>
        internal void MakeRows()
        {
            LogUtility.DebugMethodStart();

            GcCustomMultiRow crtRow = this.form.mrwTukigimeUriageDenpyo;
            crtRow.IsBrowsePurpose = false;

            //前回データクリア
            if (crtRow.RowCount != 0)
            {
                crtRow.Rows.Remove(0, crtRow.RowCount);
            }

            for (int i = 0; i < this.SearchResult.Rows.Count; i++)
            {
                this.form.mrwTukigimeUriageDenpyo.Rows.Add();

                //チェックボックス
                crtRow.Rows[i].Cells["CHK_SAKUSEI"].Value = false;
                //取引先CD
                crtRow.Rows[i].Cells["TORIHIKISAKI_CD"].Value = SearchResult.Rows[i]["TORIHIKISAKI_CD"];
                //取引先名
                crtRow.Rows[i].Cells["TORIHIKISAKI_NAME"].Value = SearchResult.Rows[i]["TORIHIKISAKI_NAME_RYAKU"];
                //業者CD
                crtRow.Rows[i].Cells["GYOUSHA_CD"].Value = SearchResult.Rows[i]["GYOUSHA_CD"];
                //業者名
                M_GYOUSHA mg = new M_GYOUSHA();
                mg.GYOUSHA_CD = SearchResult.Rows[i]["GYOUSHA_CD"].ToString();
                mg = (M_GYOUSHA)mGyoushaDao.GetDataForEntity(mg);
                if (mg != null && mg.GYOUSHA_NAME_RYAKU != null)
                {
                    crtRow.Rows[i].Cells["GYOUSHA_NAME"].Value = mg.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    crtRow.Rows[i].Cells["GYOUSHA_NAME"].Value = null;
                }
                //現場CD
                crtRow.Rows[i].Cells["GENBA_CD"].Value = SearchResult.Rows[i]["GENBA_CD"];
                //現場名
                crtRow.Rows[i].Cells["GENBA_NAME"].Value = SearchResult.Rows[i]["GENBA_NAME_RYAKU"];

                // 営業担当者CD, 営業担当者名（現場→業者→取引先の優先順で担当者を使用する）
                var eigyouTantoushaCd = String.Empty;
                var eigyouTantoushaName = String.Empty;
                if (null != SearchResult.Rows[i]["GENBA_SHAIN_CD"] && !String.IsNullOrEmpty(SearchResult.Rows[i]["GENBA_SHAIN_CD"].ToString()))
                {
                    eigyouTantoushaCd = SearchResult.Rows[i]["GENBA_SHAIN_CD"].ToString();
                    eigyouTantoushaName = SearchResult.Rows[i]["GENBA_SHAIN_NAME_RYAKU"].ToString();
                }
                else if (null != SearchResult.Rows[i]["GYOUSHA_SHAIN_CD"] && !String.IsNullOrEmpty(SearchResult.Rows[i]["GYOUSHA_SHAIN_CD"].ToString()))
                {
                    eigyouTantoushaCd = SearchResult.Rows[i]["GYOUSHA_SHAIN_CD"].ToString();
                    eigyouTantoushaName = SearchResult.Rows[i]["GYOUSHA_SHAIN_NAME_RYAKU"].ToString();
                }
                else if (null != SearchResult.Rows[i]["TORIHIKISAKI_SHAIN_CD"] && !String.IsNullOrEmpty(SearchResult.Rows[i]["TORIHIKISAKI_SHAIN_CD"].ToString()))
                {
                    eigyouTantoushaCd = SearchResult.Rows[i]["TORIHIKISAKI_SHAIN_CD"].ToString();
                    eigyouTantoushaName = SearchResult.Rows[i]["TORIHIKISAKI_SHAIN_NAME_RYAKU"].ToString();
                }
                crtRow.Rows[i].Cells["EIGYOUSHA_CD"].Value = eigyouTantoushaCd;
                crtRow.Rows[i].Cells["EIGYOUSHA_NAME"].Value = eigyouTantoushaName;

                //品名CD
                crtRow.Rows[i].Cells["HINMEI_CD"].Value = SearchResult.Rows[i]["HINMEI_CD"];
                //品名
                // 20151021 katen #13337 品名手入力に関する機能修正 start
                M_KOBETSU_HINMEI mkh = new M_KOBETSU_HINMEI();
                mkh.GYOUSHA_CD = Convert.ToString(SearchResult.Rows[i]["GYOUSHA_CD"]);
                mkh.GENBA_CD = Convert.ToString(SearchResult.Rows[i]["GENBA_CD"]);
                mkh.HINMEI_CD = Convert.ToString(SearchResult.Rows[i]["HINMEI_CD"]);
                M_KOBETSU_HINMEI result = mkhDao.GetDataByCd(mkh);
                if (result != null && result.DELETE_FLG.IsFalse)
                {
                    crtRow.Rows[i].Cells["HINMEI_NAME"].Value = result.SEIKYUU_HINMEI_NAME;
                }
                else
                {
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                    // 20160112 chenzz #13337 品名手入力に関する機能修正 start
                    M_KOBETSU_HINMEI mkh2 = new M_KOBETSU_HINMEI();
                    mkh2.GYOUSHA_CD = Convert.ToString(SearchResult.Rows[i]["GYOUSHA_CD"]);
                    mkh2.GENBA_CD = "";
                    mkh2.HINMEI_CD = Convert.ToString(SearchResult.Rows[i]["HINMEI_CD"]);
                    M_KOBETSU_HINMEI result2 = mkhDao.GetDataByCd(mkh2);
                    if (result2 != null && result2.DELETE_FLG.IsFalse)
                    {
                        crtRow.Rows[i].Cells["HINMEI_NAME"].Value = result2.SEIKYUU_HINMEI_NAME;
                    }
                    else
                    {
                        M_HINMEI mh = new M_HINMEI();
                        mh.HINMEI_CD = SearchResult.Rows[i]["HINMEI_CD"].ToString();
                        mh = (M_HINMEI)mhDao.GetDataForEntity(mh);
                        if (mh != null && mh.HINMEI_NAME != null)
                        {
                            crtRow.Rows[i].Cells["HINMEI_NAME"].Value = mh.HINMEI_NAME;
                        }
                        else
                        {
                            crtRow.Rows[i].Cells["HINMEI_NAME"].Value = null;
                        }
                    }
                    // 20160112 chenzz #13337 品名手入力に関する機能修正 end
                }
                //単位
                crtRow.Rows[i].Cells["UNIT_CD"].Value = SearchResult.Rows[i]["UNIT_CD"];
                crtRow.Rows[i].Cells["UNIT_NAME"].Value = SearchResult.Rows[i]["UNIT_NAME"];
                //単価
                crtRow.Rows[i].Cells["TANKA"].Value = SearchResult.Rows[i]["TANKA"];
                //取引区分
                M_TORIHIKI_KBN mtk = new M_TORIHIKI_KBN();
                mtk.TORIHIKI_KBN_CD = SqlInt16.Parse(SearchResult.Rows[i]["TORIHIKI_KBN_CD"].ToString());
                mtk = (M_TORIHIKI_KBN)mtkDao.GetDataForEntity(mtk);
                if (mtk != null && mtk.TORIHIKI_KBN_NAME_RYAKU != null)
                {
                    crtRow.Rows[i].Cells["TORIHIKI_KBN"].Value = mtk.TORIHIKI_KBN_NAME_RYAKU;
                }
                else
                {
                    crtRow.Rows[i].Cells["TORIHIKI_KBN"].Value = null;
                }
                //締日
                crtRow.Rows[i].Cells["SHIMEBI"].Value = SearchResult.Rows[i]["SHIMEBI1"];
                //月極区分カウント
                TukigimeKbnCount(SearchResult.Rows[i]["TORIHIKISAKI_CD"].ToString(),
                                 SearchResult.Rows[i]["GYOUSHA_CD"].ToString(),
                                 SearchResult.Rows[i]["GENBA_CD"].ToString());
                //行番号(非表示)
                crtRow.Rows[i].Cells["ROW_NO"].Value = SearchResult.Rows[i]["ROW_NO"];
                //契約区分
                crtRow.Rows[i].Cells["KEIYAKU_KBN"].Value = SearchResult.Rows[i]["KEIYAKU_KBN"];
                //数量
                crtRow.Rows[i].Cells["SUURYOU"].Value = SearchResult.Rows[i]["SUURYOU"];
                //超過規定量
                crtRow.Rows[i].Cells["CHOUKA_LIMIT_AMOUNT"].Value = SearchResult.Rows[i]["CHOUKA_LIMIT_AMOUNT"];
                
                if (SearchResult.Rows[i]["KEIYAKU_KBN"].ToString() == "定期" && SearchResult.Rows[i]["CHOUKA_SETTING"].ToString() == "True")
                {
                    //超過量設定
                    crtRow.Rows[i].Cells["CHOUKA_SETTING"].Value = "有";

                    //単位(超過)
                    crtRow.Rows[i].Cells["UNIT_CD_CHOUKA"].Value = SearchResult.Rows[i]["CHOUKA_UNIT_CD"];
                    crtRow.Rows[i].Cells["UNIT_NAME_CHOUKA"].Value = SearchResult.Rows[i]["CHOUKA_UNIT_NAME"];

                    //単価(超過)
                    string torihikiCd = Convert.ToString(crtRow.Rows[i]["TORIHIKISAKI_CD"].Value);
                    string gyoushaCd = Convert.ToString(crtRow.Rows[i]["GYOUSHA_CD"].Value);
                    string genbaCd = Convert.ToString(crtRow.Rows[i]["GENBA_CD"].Value);
                    string hinmeiCd = Convert.ToString(crtRow.Rows[i].Cells["HINMEI_CD"].Value);
                    if (string.IsNullOrEmpty(Convert.ToString(crtRow.Rows[i].Cells["UNIT_CD_CHOUKA"].Value)))
                    {
                        crtRow.Rows[i].Cells["TANKA_CHOUKA"].Value = string.Empty;
                        continue;
                    }
                    Int16 unitCd = Convert.ToInt16(crtRow.Rows[i].Cells["UNIT_CD_CHOUKA"].Value);
                    var kobetsuhinmeiTanka = this.commonAccesser.GetKobetsuhinmeiTanka(
                        (short)SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI,
                        1,
                        torihikiCd,
                        gyoushaCd,
                        genbaCd,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        hinmeiCd,
                        unitCd,
                        this.form.dtpSeikyuDate.Text
                        );

                    // 個別品名単価から情報が取れない場合は基本品名単価の検索
                    if (kobetsuhinmeiTanka == null)
                    {
                        var kihonHinmeiTanka = this.commonAccesser.GetKihonHinmeitanka(
                            (short)SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI,
                            1,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            hinmeiCd,
                            unitCd,
                            this.form.dtpSeikyuDate.Text
                            );
                        if (kihonHinmeiTanka != null)
                        {
                            crtRow.Rows[i].Cells["TANKA_CHOUKA"].Value = kihonHinmeiTanka.TANKA.Value;
                        }
                        else
                        {
                            crtRow.Rows[i].Cells["TANKA_CHOUKA"].Value = string.Empty;
                        }
                    }
                    else
                    {
                        crtRow.Rows[i].Cells["TANKA_CHOUKA"].Value = kobetsuhinmeiTanka.TANKA.Value;
                    }
                }
                else
                {
                    //超過量設定
                    crtRow.Rows[i].Cells["CHOUKA_SETTING"].Value = string.Empty;
                    //単位(超過)
                    crtRow.Rows[i].Cells["UNIT_CD_CHOUKA"].Value = string.Empty;
                    crtRow.Rows[i].Cells["UNIT_NAME_CHOUKA"].Value = string.Empty;
                    //単価(超過)
                    crtRow.Rows[i].Cells["TANKA_CHOUKA"].Value = string.Empty;
                }
            }

            dicCount.Clear();
            crtRow.IsBrowsePurpose = true;

            LogUtility.DebugMethodEnd();
        }

        #endregion 一覧表示

        #region 月極区分カウント

        /// <summary>
        /// 月極区分カウント
        /// </summary>
        /// <param name="v_torihiki">取引先CD</param>
        /// <param name="v_gyousha">業者CD</param>
        /// <param name="v_genba">現場CD</param>
        internal void TukigimeKbnCount(string v_torihiki, string v_gyousha, string v_genba)
        {
            LogUtility.DebugMethodStart(v_torihiki, v_gyousha, v_genba);

            string sKey = v_torihiki + "/" + v_gyousha + "/" + v_genba;
            if (dicCount.Keys.Contains(sKey))
            {
                dicCount[sKey] += 1;
            }
            else
            {
                dicCount.Add(sKey, 1);
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion 月極区分カウント

        #region 月極区分取得

        /// <summary>
        /// 月極区分取得
        /// </summary>
        /// <param name="v_torihiki">取引先CD</param>
        /// <param name="v_gyousha">業者CD</param>
        /// <param name="v_genba">現場CD</param>
        /// <returns>区分判定</returns>
        internal bool GetTukigimeKbn(string v_torihiki, string v_gyousha, string v_genba)
        {
            LogUtility.DebugMethodStart(v_torihiki, v_gyousha, v_genba);

            bool retval = false;
            string sKey = v_torihiki + "/" + v_gyousha + "/" + v_genba;
            if (dicCount.Keys.Contains(sKey) && dicCount[sKey] > 1)
            {
                retval = true;
            }

            LogUtility.DebugMethodEnd(retval);

            return retval;
        }

        #endregion 月極区分取得

        #region 未入力エラーチェック

        /// <summary>
        /// 未入力エラーチェック
        /// </summary>
        internal Boolean ErrCheck()
        {
            LogUtility.DebugMethodStart();

            if (form.dtpTaishoKikanFrom.Value == null)
            {
                //【対象期間-FROM】が未入力の場合
                errMsg.MessageBoxShow(ConstInfo.ERR_MSG_CD_E012, ConstInfo.ERR_ARGS_TARGET_KIKAN_FROM);
                form.dtpTaishoKikanFrom.Focus();
                form.dtpTaishoKikanFrom.Select();
                return false;
            }
            else if (form.dtpTaishoKikanTo.Value == null)
            {
                //【対象期間-TO】が未入力の場合
                errMsg.MessageBoxShow(ConstInfo.ERR_MSG_CD_E012, ConstInfo.ERR_ARGS_TARGET_KIKAN_TO);
                form.dtpTaishoKikanTo.Focus();
                form.dtpTaishoKikanTo.Select();
                return false;
            }
            else if (form.dtpSeikyuDate.Value == null)
            {
                //【売上日付】が未入力の場合
                errMsg.MessageBoxShow(ConstInfo.ERR_MSG_CD_E012, ConstInfo.ERR_ARGS_SEIKYU_DATE);
                form.dtpSeikyuDate.Select();
                form.dtpSeikyuDate.Focus();
                return false;
            }

            LogUtility.DebugMethodEnd();

            return true;
        }

        #endregion 未入力エラーチェック

        #region 一月後に変更

        /// <summary>
        /// 一月後に変更
        /// </summary>
        internal bool PlusMonth()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.cmbShimebi != null)
                {
                    DateTime dtpFrom = DateTime.Parse(form.dtpTaishoKikanFrom.GetResultText());
                    DateTime dtpTo = DateTime.Parse(form.dtpTaishoKikanTo.GetResultText());

                    DateTime chkFrom = dtpFrom.AddDays(1);
                    DateTime chkTo = dtpTo.AddDays(1);

                    DateTime nextToMonth = dtpTo.AddMonths(1);
                    DateTime nextFromMonth = dtpFrom.AddMonths(1);

                    if (chkFrom.Month != dtpFrom.Month)
                    {
                        //入力された日付Fromが末日
                        int nextDays = DateTime.DaysInMonth(nextFromMonth.Year, nextFromMonth.Month);
                        DateTime setFromDtp = new DateTime(nextFromMonth.Year, nextFromMonth.Month, nextDays);

                        form.dtpTaishoKikanFrom.SetResultText(setFromDtp.ToString());
                    }
                    else
                    {
                        form.dtpTaishoKikanFrom.SetResultText(nextFromMonth.ToString());
                    }

                    if (chkTo.Month != dtpTo.Month)
                    {
                        //入力された日付Toが末日
                        int nextDays = DateTime.DaysInMonth(nextToMonth.Year, nextToMonth.Month);
                        DateTime setToDtp = new DateTime(nextToMonth.Year, nextToMonth.Month, nextDays);

                        form.dtpTaishoKikanTo.SetResultText(setToDtp.ToString());
                    }
                    else
                    {
                        form.dtpTaishoKikanTo.SetResultText(nextToMonth.ToString());
                    }

                    form.dtpSeikyuDate.SetResultText(form.dtpTaishoKikanTo.GetResultText());
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("PlusMonth", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion 一月後に変更

        #region 一か月前に変更

        /// <summary>
        /// 一か月前に変更
        /// </summary>
        internal bool MinusMonth()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.cmbShimebi != null)
                {
                    DateTime dtpFrom = DateTime.Parse(form.dtpTaishoKikanFrom.GetResultText());
                    DateTime dtpTo = DateTime.Parse(form.dtpTaishoKikanTo.GetResultText());

                    DateTime chkFrom = dtpFrom.AddDays(1);
                    DateTime chkTo = dtpTo.AddDays(1);

                    DateTime preToMonth = dtpTo.AddMonths(-1);
                    DateTime preFromMonth = dtpFrom.AddMonths(-1);

                    if (chkFrom.Month != dtpFrom.Month)
                    {
                        //入力された日付Fromが末日
                        int preDays = DateTime.DaysInMonth(preFromMonth.Year, preFromMonth.Month);
                        DateTime setFromDtp = new DateTime(preFromMonth.Year, preFromMonth.Month, preDays);

                        form.dtpTaishoKikanFrom.SetResultText(setFromDtp.ToString());
                    }
                    else
                    {
                        form.dtpTaishoKikanFrom.SetResultText(preFromMonth.ToString());
                    }

                    if (chkTo.Month != dtpTo.Month)
                    {
                        //入力された日付Toが末日
                        int preDays = DateTime.DaysInMonth(preToMonth.Year, preToMonth.Month);
                        DateTime setToDtp = new DateTime(preToMonth.Year, preToMonth.Month, preDays);

                        form.dtpTaishoKikanTo.SetResultText(setToDtp.ToString());
                    }
                    else
                    {
                        form.dtpTaishoKikanTo.SetResultText(preToMonth.ToString());
                    }

                    form.dtpSeikyuDate.SetResultText(form.dtpTaishoKikanTo.GetResultText());
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("MinusMonth", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion 一か月前に変更

        #region 売上日付変更時処理

        /// <summary>
        /// 売上日付変更時処理
        /// </summary>
        internal bool ChangeSeikyuuDate()
        {
            LogUtility.DebugMethodStart();

            if (string.IsNullOrEmpty(form.dtpSeikyuDate.Text) || form.dtpSeikyuDate.Text == " ")
            {
                //入力なしならエラーメッセージ表示
                errMsg.MessageBoxShow(ConstInfo.ERR_MSG_CD_E002, ConstInfo.ERR_ARGS_SEIKYU_DATE, ConstInfo.ERR_ARGS_TARGET_KIKAN);
                this.form.dtpSeikyuDate.Select();
                this.form.dtpSeikyuDate.Focus();
                //errdtpSeikyuu = true;
                return false;
            }

            DateTime dtpSeikyuu = DateTime.Parse(form.dtpSeikyuDate.GetResultText());
            DateTime dtpFrom = DateTime.Parse(form.dtpTaishoKikanFrom.GetResultText());
            DateTime dtpTo = DateTime.Parse(form.dtpTaishoKikanTo.GetResultText());

            DateTime DtpSeikyuuWithoutTime = DateTime.Parse(dtpSeikyuu.ToShortDateString());
            DateTime DtpFromWithoutTime = DateTime.Parse(dtpFrom.ToShortDateString());
            DateTime DtpToWithoutTime = DateTime.Parse(dtpTo.ToShortDateString());

            int diffFrom = DtpSeikyuuWithoutTime.CompareTo(DtpFromWithoutTime);
            int diffTo = DtpSeikyuuWithoutTime.CompareTo(DtpToWithoutTime);

            if (diffFrom < 0 || 0 < diffTo)
            {
                //対象期間内でないならエラーメッセージ表示
                errMsg.MessageBoxShow(ConstInfo.ERR_MSG_CD_E002, ConstInfo.ERR_ARGS_SEIKYU_DATE, ConstInfo.ERR_ARGS_TARGET_KIKAN);
                this.form.dtpSeikyuDate.Select();
                this.form.dtpSeikyuDate.Focus();
                return false;
                //errdtpSeikyuu = true;
            }

            LogUtility.DebugMethodEnd();

            return true;
        }

        #endregion 売上日付変更時処理

        #region 売上日付エラー時フォーカス指定

        /// <summary>
        /// 売上日付エラー時フォーカス指定
        /// </summary>
        internal void ErrSeikyuuDate()
        {
            LogUtility.DebugMethodStart();

            if (errdtpSeikyuu)
            {
                errMsg.MessageBoxShow(ConstInfo.ERR_MSG_CD_E002, ConstInfo.ERR_ARGS_SEIKYU_DATE, ConstInfo.ERR_ARGS_TARGET_KIKAN);
                this.form.dtpSeikyuDate.Select();
                this.form.dtpSeikyuDate.Focus();
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion 売上日付エラー時フォーカス指定

        #region 伝票一覧画面起動

        /// <summary>
        /// 伝票一覧画面起動
        /// </summary>
        internal void StartWindow()
        {
            LogUtility.DebugMethodStart();

            FormManager.OpenFormWithAuth("G055", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DENSHU_KBN.DENPYOU_ICHIRAN, SystemProperty.Shain.CD);

            LogUtility.DebugMethodEnd();
        }

        #endregion 伝票一覧画面起動

        #region 終了処理

        /// <summary>
        /// 終了処理
        /// </summary>
        internal void EndOfProcess()
        {
            LogUtility.DebugMethodStart();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            LogUtility.DebugMethodEnd();
            parentForm.Close();
        }

        #endregion 終了処理

        #region 登録実行処理

        /// <summary>
        /// 登録実行処理
        /// </summary>
        internal void ProcessStart()
        {
            LogUtility.DebugMethodStart();

            //チェックボックス未入力のチェック処理
            int cnt = chkboxCheck();
            if (cnt == 0)
            {
                //チェック状態の行が１件も存在しない場合、エラーメッセージ
                errMsg.MessageBoxShow(ConstInfo.ERR_MSG_CD_E027, ConstInfo.ERR_ARGS_CUREATE_DATA);

                return;
            }
            else if (cnt < 0)
            {
                return;
            }

            // 20141118 koukouei 締済期間チェックの追加 start
            if (!ShimeiDateCheck())
            {
                return;
            }
            // 20141118 koukouei 締済期間チェックの追加 end

            //データ登録
            if (!GridSetData())
            {
                return;
            }

            //グリッド再表示
            ExecuteSearch(true);

            LogUtility.DebugMethodEnd();
        }

        #endregion 登録実行処理

        #region チェックボックス入力チェック

        /// <summary>
        /// チェックボックス入力チェック
        /// </summary>
        /// <returns></returns>
        internal int chkboxCheck()
        {
            LogUtility.DebugMethodStart();

            int checkRows = 0;
            try
            {
                for (int i = 0; i < this.form.mrwTukigimeUriageDenpyo.Rows.Count; i++)
                {
                    if ((bool)this.form.mrwTukigimeUriageDenpyo.Rows[i].Cells["CHK_SAKUSEI"].Value)
                    {
                        checkRows += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("chkboxCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }

            LogUtility.DebugMethodEnd(checkRows);
            return checkRows;
        }

        #endregion チェックボックス入力チェック

        #region データ登録

        /// <summary>
        /// データ登録
        /// </summary>
        internal bool GridSetData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //連番リセット
                rowCount = 0;
                String UsrName = System.Environment.UserName;
                UsrName = UsrName.Length > 16 ? UsrName.Substring(0, 16) : UsrName;

                //データセット
                for (int i = 0; i < this.form.mrwTukigimeUriageDenpyo.Rows.Count; i++)
                {
                    //チェックの入っていない行は飛ばす
                    if ((bool)this.form.mrwTukigimeUriageDenpyo.Rows[i].Cells["CHK_SAKUSEI"].Value == false)
                    {
                        continue;
                    }

                    //売上／支払入力テーブル登録
                    TUSETableAdd(i);

                    // 行番号を設定(現状、1伝票：1明細なので、かならず1になる。将来ループで回すときにインクリメントするようにする)
                    short rowNo = 1;

                    //売上／支払い明細テーブル登録
                    TUSDTableAdd(i, rowNo);

                    rowCount += 1;
                }

                errMsg.MessageBoxShow(ConstInfo.CHK_MSG_CD_I001, rowCount.ToString() + "件の伝票作成");
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("GridSetData", ex1);
                this.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("GridSetData", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GridSetData", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion データ登録

        #region 売上／支払入力テーブル登録

        /// <summary>
        /// 売上／支払入力テーブル登録
        /// </summary>
        internal void TUSETableAdd(int v_num)
        {
            LogUtility.DebugMethodStart(v_num);

            GcCustomMultiRow crtRow = this.form.mrwTukigimeUriageDenpyo;

            //売上／支払入力テーブル
            T_UR_SH_ENTRY tuse = new T_UR_SH_ENTRY();
            //社員マスタ
            M_SHAIN ms = new M_SHAIN();
            //現場マスタ
            M_GENBA mg = new M_GENBA();
            //消費税マスタ
            M_SHOUHIZEI mSho = new M_SHOUHIZEI();
            //取引先_請求情報マスタ
            M_TORIHIKISAKI_SEIKYUU mTSeikyuu = new M_TORIHIKISAKI_SEIKYUU();
            if (!string.IsNullOrEmpty(crtRow.Rows[v_num].Cells["TORIHIKISAKI_CD"].Value.ToString()))
            {
                mTSeikyuu.TORIHIKISAKI_CD = crtRow.Rows[v_num].Cells["TORIHIKISAKI_CD"].Value.ToString();
                mTSeikyuu = (M_TORIHIKISAKI_SEIKYUU)mtseikyuuDao.GetDataForEntity(mTSeikyuu);
                if (mTSeikyuu == null)
                {
                    mTSeikyuu = new M_TORIHIKISAKI_SEIKYUU();
                }
            }
            //取引先_支払情報マスタ
            M_TORIHIKISAKI_SHIHARAI mTShiharai = new M_TORIHIKISAKI_SHIHARAI();
            if (!string.IsNullOrEmpty(crtRow.Rows[v_num].Cells["TORIHIKISAKI_CD"].Value.ToString()))
            {
                mTShiharai.TORIHIKISAKI_CD = crtRow.Rows[v_num].Cells["TORIHIKISAKI_CD"].Value.ToString();
                mTShiharai = (M_TORIHIKISAKI_SHIHARAI)mtshiharaiDao.GetDataForEntity(mTShiharai);
                if (mTShiharai == null)
                {
                    mTShiharai = new M_TORIHIKISAKI_SHIHARAI();
                }
            }
            //品名マスタ
            M_HINMEI mh = new M_HINMEI();
            // 売上消費税率取得結果
            M_SHOUHIZEI uriageZeiTable = new M_SHOUHIZEI();
            // 支払消費税率取得結果
            var shiharaiZeiTable = new M_SHOUHIZEI();
            //売上日付(DateTime)
            DateTime DTSeikyuDate = DateTime.Parse(form.dtpSeikyuDate.Text);
            //月極マスタ
            if (!string.IsNullOrEmpty(crtRow.Rows[v_num].Cells["GYOUSHA_CD"].Value.ToString()) && !string.IsNullOrEmpty(crtRow.Rows[v_num].Cells["GENBA_CD"].Value.ToString()))
            {
                mgth = new M_GENBA_TSUKI_HINMEI();
                mgth.GYOUSHA_CD = crtRow.Rows[v_num].Cells["GYOUSHA_CD"].Value.ToString();
                mgth.GENBA_CD = crtRow.Rows[v_num].Cells["GENBA_CD"].Value.ToString();
                mgth.ROW_NO = SqlInt16.Parse(crtRow.Rows[v_num].Cells["ROW_NO"].Value.ToString());
                mgth = (M_GENBA_TSUKI_HINMEI)mgthDao.GetDataForEntity(mgth);
                if (mgth == null)
                {
                    mgth = new M_GENBA_TSUKI_HINMEI();
                }
            }

            sysId = this.accessor.createSystemId();
            tuse.SYSTEM_ID = sysId;
            tuse.SEQ = 1;   //新規追加のみなので１
            if (form.txtKyotenCd.Text != "" && form.txtKyotenCd.Text != null)
            {
                tuse.KYOTEN_CD = SqlInt16.Parse(form.txtKyotenCd.Text);
            }
            else
            {
                tuse.KYOTEN_CD = SqlInt16.Null;
            }
            urshNum = this.accessor.createDenshuNumber();
            tuse.UR_SH_NUMBER = urshNum;

            M_CORP_INFO corpInfo = new M_CORP_INFO();
            var corpInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_CORP_INFODao>();
            var corpInfos = corpInfoDao.GetAllDataMinCols();
            if (corpInfos != null && 0 < corpInfos.Length)
            {
                corpInfo = corpInfos[0];
            }

            tuse.KAKUTEI_KBN = 1;
            tuse.DENPYOU_DATE = SqlDateTime.Parse(DTSeikyuDate.ToShortDateString());
            tuse.URIAGE_DATE = SqlDateTime.Parse(DTSeikyuDate.ToShortDateString());
            tuse.SHIHARAI_DATE = SqlDateTime.Parse(DTSeikyuDate.ToShortDateString());

            // 日、年連番の採番
            if (!tuse.DENPYOU_DATE.IsNull && !tuse.KYOTEN_CD.IsNull)
            {
                var dateNumber = this.InsertOrUpdateOfNumberDayEntity((DateTime)tuse.DENPYOU_DATE, (short)tuse.KYOTEN_CD);
                var yearNumber = this.InsertOrUpdateOfNumberYearEntity(CorpInfoUtility.GetCurrentYear((DateTime)tuse.DENPYOU_DATE, (short)corpInfo.KISHU_MONTH), (short)tuse.KYOTEN_CD);

                if (0 < dateNumber)
                {
                    tuse.DATE_NUMBER = dateNumber;
                }

                if (0 < yearNumber)
                {
                    tuse.YEAR_NUMBER = yearNumber;
                }
            }

            tuse.TORIHIKISAKI_CD = crtRow.Rows[v_num].Cells["TORIHIKISAKI_CD"].Value.ToString();
            tuse.TORIHIKISAKI_NAME = crtRow.Rows[v_num].Cells["TORIHIKISAKI_NAME"].Value.ToString();
            tuse.GYOUSHA_CD = crtRow.Rows[v_num].Cells["GYOUSHA_CD"].Value.ToString();
            tuse.GYOUSHA_NAME = crtRow.Rows[v_num].Cells["GYOUSHA_NAME"].Value.ToString();
            tuse.GENBA_CD = crtRow.Rows[v_num].Cells["GENBA_CD"].Value.ToString();
            tuse.GENBA_NAME = crtRow.Rows[v_num].Cells["GENBA_NAME"].Value.ToString();
            tuse.NIZUMI_GYOUSHA_CD = "";
            tuse.NIZUMI_GYOUSHA_NAME = "";
            tuse.NIZUMI_GENBA_CD = "";
            tuse.NIZUMI_GENBA_NAME = "";
            tuse.NIOROSHI_GYOUSHA_CD = "";
            tuse.NIOROSHI_GYOUSHA_NAME = "";
            tuse.NIOROSHI_GENBA_CD = "";
            tuse.NIOROSHI_GENBA_NAME = "";
            if (crtRow.Rows[v_num].Cells["EIGYOUSHA_CD"].Value != ""
                && crtRow.Rows[v_num].Cells["EIGYOUSHA_CD"].Value != null)
            {
                tuse.EIGYOU_TANTOUSHA_CD = crtRow.Rows[v_num].Cells["EIGYOUSHA_CD"].Value.ToString();
            }
            else
            {
                tuse.EIGYOU_TANTOUSHA_CD = null;
            }
            if (crtRow.Rows[v_num].Cells["EIGYOUSHA_NAME"].Value != ""
                && crtRow.Rows[v_num].Cells["EIGYOUSHA_NAME"].Value != null)
            {
                tuse.EIGYOU_TANTOUSHA_NAME = crtRow.Rows[v_num].Cells["EIGYOUSHA_NAME"].Value.ToString();
            }
            else
            {
                tuse.EIGYOU_TANTOUSHA_NAME = null;
            }
            //ms.SHAIN_CD = "10000";  //仮固定値
            ms.SHAIN_CD = SystemProperty.Shain.CD;
            ms = (M_SHAIN)msDao.GetDataForEntity(ms);
            if (ms == null)
            {
                ms = new M_SHAIN();
            }
            if (ms.NYUURYOKU_TANTOU_KBN.Value != false)
            {
                tuse.NYUURYOKU_TANTOUSHA_CD = ms.SHAIN_CD;
                tuse.NYUURYOKU_TANTOUSHA_NAME = ms.SHAIN_NAME_RYAKU;
            }
            else
            {
                tuse.NYUURYOKU_TANTOUSHA_CD = null;
                tuse.NYUURYOKU_TANTOUSHA_NAME = "";
            }
            tuse.SHARYOU_CD = "";
            tuse.SHARYOU_NAME = "";
            tuse.SHASHU_CD = "";
            tuse.SHASHU_NAME = "";
            tuse.UNPAN_GYOUSHA_CD = "";
            tuse.UNPAN_GYOUSHA_NAME = "";
            tuse.UNTENSHA_CD = "";
            tuse.UNTENSHA_NAME = "";
            tuse.NINZUU_CNT = 0;
            tuse.KEITAI_KBN_CD = SqlInt16.Null;
            tuse.CONTENA_SOUSA_CD = SqlInt16.Null;
            if (!string.IsNullOrEmpty(crtRow.Rows[v_num].Cells["GYOUSHA_CD"].Value.ToString()) && !string.IsNullOrEmpty(crtRow.Rows[v_num].Cells["GENBA_CD"].Value.ToString()))
            {
                mg.GYOUSHA_CD = crtRow.Rows[v_num].Cells["GYOUSHA_CD"].Value.ToString();
                mg.GENBA_CD = crtRow.Rows[v_num].Cells["GENBA_CD"].Value.ToString();
                mg = (M_GENBA)mgDao.GetDataForEntity(mg);
                if (mg == null)
                {
                    mg = new M_GENBA();
                }
            }
            tuse.DENPYOU_BIKOU = "";
            tuse.UKETSUKE_NUMBER = SqlInt64.Null;
            tuse.RECEIPT_NUMBER = SqlInt32.Null;
            uriageZeiTable = mshoDao.GetShouhizei(tuse.URIAGE_DATE.ToString());
            shiharaiZeiTable = mshoDao.GetShouhizei(tuse.SHIHARAI_DATE.ToString());
            decimal uriageZeiRate = decimal.Parse(uriageZeiTable.SHOUHIZEI_RATE.ToString());
            decimal shiharaiZeiRate = decimal.Parse(shiharaiZeiTable.SHOUHIZEI_RATE.ToString());
            tuse.URIAGE_SHOUHIZEI_RATE = uriageZeiTable.SHOUHIZEI_RATE;
            tuse.SHIHARAI_SHOUHIZEI_RATE = shiharaiZeiTable.SHOUHIZEI_RATE;
            if (!string.IsNullOrEmpty(crtRow.Rows[v_num].Cells["HINMEI_CD"].Value.ToString()))
            {
                mh.HINMEI_CD = crtRow.Rows[v_num].Cells["HINMEI_CD"].Value.ToString();
                mh = (M_HINMEI)mhDao.GetDataForEntity(mh);
                if (mh == null)
                {
                    mh = new M_HINMEI();
                }
            }
            if (!mh.ZEI_KBN_CD.Equals(SqlInt16.Null)) //②
            {
                tuse.URIAGE_AMOUNT_TOTAL = 0;
                tuse.URIAGE_TAX_SOTO = 0;
                tuse.URIAGE_TAX_UCHI = 0;
                tuse.URIAGE_TAX_SOTO_TOTAL = 0;
                tuse.URIAGE_TAX_UCHI_TOTAL = 0;
                decimal totalHinKin = decimal.Parse((mgth.TANKA * 1).ToString());
                tuse.HINMEI_URIAGE_KINGAKU_TOTAL = (SqlDecimal)this.FractionCalc((decimal)totalHinKin, (int)mTSeikyuu.KINGAKU_HASUU_CD);

                decimal soto_total = totalHinKin * uriageZeiRate;
                decimal uchi_total = totalHinKin - (totalHinKin / (uriageZeiRate + 1));
                int soto_sign = soto_total < 0 ? -1 : 1;
                int uchi_sign = uchi_total < 0 ? -1 : 1;

                if (mTSeikyuu.TAX_HASUU_CD == 1)
                {
                    //切り上げ
                    if (mh.ZEI_KBN_CD == 1)
                    {
                        //外税
                        tuse.HINMEI_URIAGE_TAX_SOTO_TOTAL = (SqlDecimal)(Math.Ceiling(Math.Abs(soto_total)) * soto_sign);
                        tuse.HINMEI_URIAGE_TAX_UCHI_TOTAL = 0;
                    }
                    else if (mh.ZEI_KBN_CD == 2)
                    {
                        //内税
                        tuse.HINMEI_URIAGE_TAX_SOTO_TOTAL = 0;
                        tuse.HINMEI_URIAGE_TAX_UCHI_TOTAL = (SqlDecimal)(Math.Ceiling(Math.Abs(uchi_total)) * uchi_sign);
                    }
                    else if (mh.ZEI_KBN_CD == 3)
                    {
                        //非課税
                        tuse.HINMEI_URIAGE_TAX_SOTO_TOTAL = 0;
                        tuse.HINMEI_URIAGE_TAX_UCHI_TOTAL = 0;
                    }
                }
                else if (mTSeikyuu.TAX_HASUU_CD == 2)
                {
                    //切り捨て
                    if (mh.ZEI_KBN_CD == 1)
                    {
                        //外税
                        tuse.HINMEI_URIAGE_TAX_SOTO_TOTAL = (SqlDecimal)(Math.Floor(Math.Abs(soto_total)) * soto_sign);
                        tuse.HINMEI_URIAGE_TAX_UCHI_TOTAL = 0;
                    }
                    else if (mh.ZEI_KBN_CD == 2)
                    {
                        //内税
                        tuse.HINMEI_URIAGE_TAX_SOTO_TOTAL = 0;
                        tuse.HINMEI_URIAGE_TAX_UCHI_TOTAL = (SqlDecimal)(Math.Floor(Math.Abs(uchi_total)) * uchi_sign);
                    }
                    else if (mh.ZEI_KBN_CD == 3)
                    {
                        //非課税
                        tuse.HINMEI_URIAGE_TAX_SOTO_TOTAL = 0;
                        tuse.HINMEI_URIAGE_TAX_UCHI_TOTAL = 0;
                    }
                }
                else if (mTSeikyuu.TAX_HASUU_CD == 3)
                {
                    //四捨五入
                    if (mh.ZEI_KBN_CD == 1)
                    {
                        //外税
                        tuse.HINMEI_URIAGE_TAX_SOTO_TOTAL = (SqlDecimal)(Math.Round(Math.Abs(soto_total), 0, MidpointRounding.AwayFromZero) * soto_sign);
                        tuse.HINMEI_URIAGE_TAX_UCHI_TOTAL = 0;
                    }
                    else if (mh.ZEI_KBN_CD == 2)
                    {
                        //内税
                        tuse.HINMEI_URIAGE_TAX_SOTO_TOTAL = 0;
                        tuse.HINMEI_URIAGE_TAX_UCHI_TOTAL = (SqlDecimal)(Math.Round(Math.Abs(uchi_total), 0, MidpointRounding.AwayFromZero) * uchi_sign);
                    }
                    else if (mh.ZEI_KBN_CD == 3)
                    {
                        //非課税
                        tuse.HINMEI_URIAGE_TAX_SOTO_TOTAL = 0;
                        tuse.HINMEI_URIAGE_TAX_UCHI_TOTAL = 0;
                    }
                }
            }
            else    //①
            {
                decimal totalHinKin = decimal.Parse((mgth.TANKA * 1).ToString());
                tuse.URIAGE_AMOUNT_TOTAL = (SqlDecimal)this.FractionCalc((decimal)totalHinKin, (int)mTSeikyuu.KINGAKU_HASUU_CD);

                decimal soto_total = totalHinKin * uriageZeiRate;
                decimal uchi_total = totalHinKin - (totalHinKin / (uriageZeiRate + 1));
                int soto_sign = soto_total < 0 ? -1 : 1;
                int uchi_sign = uchi_total < 0 ? -1 : 1;

                if (mTSeikyuu.TAX_HASUU_CD == 1)
                {
                    //切り上げ
                    if (mTSeikyuu.ZEI_KBN_CD == 1)
                    {
                        //外税
                        tuse.URIAGE_TAX_SOTO = (SqlDecimal)(Math.Ceiling(Math.Abs(soto_total)) * soto_sign);
                        tuse.URIAGE_TAX_SOTO_TOTAL = (SqlDecimal)(Math.Ceiling(Math.Abs(soto_total)) * soto_sign);
                        tuse.URIAGE_TAX_UCHI = 0;
                        tuse.URIAGE_TAX_UCHI_TOTAL = 0;
                    }
                    else if (mTSeikyuu.ZEI_KBN_CD == 2)
                    {
                        //内税
                        tuse.URIAGE_TAX_SOTO = 0;
                        tuse.URIAGE_TAX_SOTO_TOTAL = 0;
                        tuse.URIAGE_TAX_UCHI = (SqlDecimal)(Math.Ceiling(Math.Abs(uchi_total)) * uchi_sign);
                        tuse.URIAGE_TAX_UCHI_TOTAL = (SqlDecimal)(Math.Ceiling(Math.Abs(uchi_total)) * uchi_sign);
                    }
                    else if (mTSeikyuu.ZEI_KBN_CD == 3)
                    {
                        //非課税
                        tuse.URIAGE_TAX_SOTO = 0;
                        tuse.URIAGE_TAX_SOTO_TOTAL = 0;
                        tuse.URIAGE_TAX_UCHI = 0;
                        tuse.URIAGE_TAX_UCHI_TOTAL = 0;
                    }
                }
                else if (mTSeikyuu.TAX_HASUU_CD == 2)
                {
                    //切り捨て
                    if (mTSeikyuu.ZEI_KBN_CD == 1)
                    {
                        //外税
                        tuse.URIAGE_TAX_SOTO = (SqlDecimal)(Math.Floor(Math.Abs(soto_total)) * soto_sign);
                        tuse.URIAGE_TAX_SOTO_TOTAL = (SqlDecimal)(Math.Floor(Math.Abs(soto_total)) * soto_sign);
                        tuse.URIAGE_TAX_UCHI = 0;
                        tuse.URIAGE_TAX_UCHI_TOTAL = 0;
                    }
                    else if (mTSeikyuu.ZEI_KBN_CD == 2)
                    {
                        //内税
                        tuse.URIAGE_TAX_SOTO = 0;
                        tuse.URIAGE_TAX_SOTO_TOTAL = 0;
                        tuse.URIAGE_TAX_UCHI = (SqlDecimal)(Math.Floor(Math.Abs(uchi_total)) * uchi_sign);
                        tuse.URIAGE_TAX_UCHI_TOTAL = (SqlDecimal)(Math.Floor(Math.Abs(uchi_total)) * uchi_sign);
                    }
                    else if (mTSeikyuu.ZEI_KBN_CD == 3)
                    {
                        //非課税
                        tuse.URIAGE_TAX_SOTO = 0;
                        tuse.URIAGE_TAX_SOTO_TOTAL = 0;
                        tuse.URIAGE_TAX_UCHI = 0;
                        tuse.URIAGE_TAX_UCHI_TOTAL = 0;
                    }
                }
                else if (mTSeikyuu.TAX_HASUU_CD == 3)
                {
                    //四捨五入
                    if (mTSeikyuu.ZEI_KBN_CD == 1)
                    {
                        //外税
                        tuse.URIAGE_TAX_SOTO = (SqlDecimal)(Math.Round(Math.Abs(soto_total), 0, MidpointRounding.AwayFromZero) * soto_sign);
                        tuse.URIAGE_TAX_SOTO_TOTAL = (SqlDecimal)(Math.Round(Math.Abs(soto_total), 0, MidpointRounding.AwayFromZero) * soto_sign);
                        tuse.URIAGE_TAX_UCHI = 0;
                        tuse.URIAGE_TAX_UCHI_TOTAL = 0;
                    }
                    else if (mTSeikyuu.ZEI_KBN_CD == 2)
                    {
                        //内税
                        tuse.URIAGE_TAX_SOTO = 0;
                        tuse.URIAGE_TAX_SOTO_TOTAL = 0;
                        tuse.URIAGE_TAX_UCHI = (SqlDecimal)(Math.Round(Math.Abs(uchi_total), 0, MidpointRounding.AwayFromZero) * uchi_sign);
                        tuse.URIAGE_TAX_UCHI_TOTAL = (SqlDecimal)(Math.Round(Math.Abs(uchi_total), 0, MidpointRounding.AwayFromZero) * uchi_sign);
                    }
                    else if (mTSeikyuu.ZEI_KBN_CD == 3)
                    {
                        //非課税
                        tuse.URIAGE_TAX_SOTO = 0;
                        tuse.URIAGE_TAX_SOTO_TOTAL = 0;
                        tuse.URIAGE_TAX_UCHI = 0;
                        tuse.URIAGE_TAX_UCHI_TOTAL = 0;
                    }
                }
                tuse.HINMEI_URIAGE_KINGAKU_TOTAL = 0;
                tuse.HINMEI_URIAGE_TAX_SOTO_TOTAL = 0;
                tuse.HINMEI_URIAGE_TAX_UCHI_TOTAL = 0;
            }
            tuse.SHIHARAI_AMOUNT_TOTAL = 0;
            tuse.SHIHARAI_TAX_SOTO = 0;
            tuse.SHIHARAI_TAX_UCHI = 0;
            tuse.SHIHARAI_TAX_SOTO_TOTAL = 0;
            tuse.SHIHARAI_TAX_UCHI_TOTAL = 0;
            tuse.HINMEI_SHIHARAI_KINGAKU_TOTAL = 0;
            tuse.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = 0;
            tuse.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = 0;
            tuse.URIAGE_ZEI_KEISAN_KBN_CD = mTSeikyuu.ZEI_KEISAN_KBN_CD;
            tuse.URIAGE_ZEI_KBN_CD = mTSeikyuu.ZEI_KBN_CD;
            tuse.URIAGE_TORIHIKI_KBN_CD = mTSeikyuu.TORIHIKI_KBN_CD;
            tuse.SHIHARAI_ZEI_KEISAN_KBN_CD = mTShiharai.ZEI_KEISAN_KBN_CD;
            tuse.SHIHARAI_ZEI_KBN_CD = mTShiharai.ZEI_KBN_CD;
            tuse.SHIHARAI_TORIHIKI_KBN_CD = 2;
            tuse.TSUKI_CREATE_KBN = SqlBoolean.One;

            //既存の処理では入れない
            //tuse.CREATE_USER;
            //tuse.CREATE_DATE;
            //tuse.CREATE_PC;
            //tuse.UPDATE_USER;
            //tuse.UPDATE_DATE;
            //tuse.UPDATE_PC;
            tuse.DAINOU_FLG = SqlBoolean.Zero;
            tuse.DELETE_FLG = SqlBoolean.Zero;
            //tuse.TIME_STAMP

            var addData = new DataBinderLogic<T_UR_SH_ENTRY>(tuse);
            addData.SetSystemProperty(tuse, false);
            DaoInsert(tuse);

            LogUtility.DebugMethodEnd();
        }

        #endregion 売上／支払入力テーブル登録

        #region 売上／支払明細テーブル登録

        /// <summary>
        /// 売上／支払明細テーブル登録
        /// </summary>
        /// <param name="v_num"></param>
        /// <param name="rowNo">行番号</param>
        internal void TUSDTableAdd(int v_num, short rowNo)
        {
            LogUtility.DebugMethodStart(v_num, rowNo);

            GcCustomMultiRow crtRow = this.form.mrwTukigimeUriageDenpyo;

            //売上／支払明細テーブル
            T_UR_SH_DETAIL tusd = new T_UR_SH_DETAIL();
            //品名マスタ
            M_HINMEI mh = new M_HINMEI();
            //取引先_請求情報マスタ
            M_TORIHIKISAKI_SEIKYUU mTSeikyuu = new M_TORIHIKISAKI_SEIKYUU();
            if (!string.IsNullOrEmpty(crtRow.Rows[v_num].Cells["TORIHIKISAKI_CD"].Value.ToString()))
            {
                mTSeikyuu.TORIHIKISAKI_CD = crtRow.Rows[v_num].Cells["TORIHIKISAKI_CD"].Value.ToString();
                mTSeikyuu = (M_TORIHIKISAKI_SEIKYUU)mtseikyuuDao.GetDataForEntity(mTSeikyuu);
                if (mTSeikyuu == null)
                {
                    mTSeikyuu = new M_TORIHIKISAKI_SEIKYUU();
                }
            }
            //消費税率取得結果
            M_SHOUHIZEI zeiTable = new M_SHOUHIZEI();
            DateTime DTSeikyuDate = DateTime.Parse(form.dtpSeikyuDate.Text);
            zeiTable = mshoDao.GetShouhizei(DTSeikyuDate.ToShortDateString());
            decimal zeiRate = decimal.Parse(zeiTable.SHOUHIZEI_RATE.ToString());

            tusd.SYSTEM_ID = sysId;
            tusd.SEQ = 1;
            var detailSysId = this.accessor.createSystemId();
            tusd.DETAIL_SYSTEM_ID = detailSysId;
            tusd.UR_SH_NUMBER = urshNum;
            tusd.ROW_NO = rowNo;
            tusd.KAKUTEI_KBN = 1;
            tusd.URIAGESHIHARAI_DATE = SqlDateTime.Parse(DTSeikyuDate.ToShortDateString());
            tusd.DENPYOU_KBN_CD = 1;
            //mgth.GYOUSHA_CD = crtRow.Rows[v_num].Cells["GYOUSHA_CD"].Value.ToString();
            //mgth.GENBA_CD = crtRow.Rows[v_num].Cells["GENBA_CD"].Value.ToString();
            //mgth.ROW_NO = SqlInt16.Parse(rowCount.ToString());
            //mgth = (M_GENBA_TSUKI_HINMEI)mgthDao.GetDataForEntity(mgth);
            tusd.HINMEI_CD = mgth.HINMEI_CD;
            mh.HINMEI_CD = mgth.HINMEI_CD;
            mh = (M_HINMEI)mhDao.GetDataForEntity(mh);
            if (mh == null)
            {
                mh = new M_HINMEI();
            }
            // 20151021 katen #13337 品名手入力に関する機能修正 start
            //tusd.HINMEI_NAME = mh.HINMEI_NAME_RYAKU;
            tusd.HINMEI_NAME = Convert.ToString(crtRow.Rows[v_num].Cells["HINMEI_NAME"].Value);
            // 20151021 katen #13337 品名手入力に関する機能修正 end
            tusd.SUURYOU = 1;
            tusd.UNIT_CD = mgth.UNIT_CD;
            tusd.TANKA = mgth.TANKA;
            if (!mh.ZEI_KBN_CD.Equals(SqlInt16.Null))    //②
            {
                tusd.KINGAKU = 0;
                tusd.TAX_SOTO = 0;
                tusd.TAX_UCHI = 0;
                tusd.HINMEI_ZEI_KBN_CD = mh.ZEI_KBN_CD;
                decimal hinKin = decimal.Parse((mgth.TANKA * 1).ToString());
                tusd.HINMEI_KINGAKU = (SqlDecimal)this.FractionCalc(hinKin, (int)mTSeikyuu.KINGAKU_HASUU_CD);

                decimal soto_total = hinKin * zeiRate;
                decimal uchi_total = hinKin - (hinKin / (zeiRate + 1));
                int soto_sign = soto_total < 0 ? -1 : 1;
                int uchi_sign = uchi_total < 0 ? -1 : 1;

                if (mTSeikyuu.TAX_HASUU_CD == 1)
                {
                    //切り上げ
                    if (mh.ZEI_KBN_CD == 1)
                    {
                        //外税
                        tusd.HINMEI_TAX_SOTO = (SqlDecimal)(Math.Ceiling(Math.Abs(soto_total)) * soto_sign);
                        tusd.HINMEI_TAX_UCHI = 0;
                    }
                    else if (mh.ZEI_KBN_CD == 2)
                    {
                        //内税
                        tusd.HINMEI_TAX_SOTO = 0;
                        tusd.HINMEI_TAX_UCHI = (SqlDecimal)(Math.Ceiling(Math.Abs(uchi_total)) * uchi_sign);
                    }
                    else if (mh.ZEI_KBN_CD == 3)
                    {
                        //非課税
                        tusd.HINMEI_TAX_SOTO = 0;
                        tusd.HINMEI_TAX_UCHI = 0;
                    }
                }
                else if (mTSeikyuu.TAX_HASUU_CD == 2)
                {
                    //切り捨て
                    if (mh.ZEI_KBN_CD == 1)
                    {
                        //外税
                        tusd.HINMEI_TAX_SOTO = (SqlDecimal)(Math.Floor(Math.Abs(soto_total)) * soto_sign);
                        tusd.HINMEI_TAX_UCHI = 0;
                    }
                    else if (mh.ZEI_KBN_CD == 2)
                    {
                        //内税
                        tusd.HINMEI_TAX_SOTO = 0;
                        tusd.HINMEI_TAX_UCHI = (SqlDecimal)(Math.Floor(Math.Abs(uchi_total)) * uchi_sign);
                    }
                    else if (mh.ZEI_KBN_CD == 3)
                    {
                        //非課税
                        tusd.HINMEI_TAX_SOTO = 0;
                        tusd.HINMEI_TAX_UCHI = 0;
                    }
                }
                else if (mTSeikyuu.TAX_HASUU_CD == 3)
                {
                    //四捨五入
                    if (mh.ZEI_KBN_CD == 1)
                    {
                        //外税
                        tusd.HINMEI_TAX_SOTO = (SqlDecimal)(Math.Round(Math.Abs(soto_total), 0, MidpointRounding.AwayFromZero) * soto_sign);
                        tusd.HINMEI_TAX_UCHI = 0;
                    }
                    else if (mh.ZEI_KBN_CD == 2)
                    {
                        //内税
                        tusd.HINMEI_TAX_SOTO = 0;
                        tusd.HINMEI_TAX_UCHI = (SqlDecimal)(Math.Round(Math.Abs(uchi_total), 0, MidpointRounding.AwayFromZero) * uchi_sign);
                    }
                    else if (mh.ZEI_KBN_CD == 3)
                    {
                        //非課税
                        tusd.HINMEI_TAX_SOTO = 0;
                        tusd.HINMEI_TAX_UCHI = 0;
                    }
                }
            }
            else   //①
            {
                decimal hinKin = decimal.Parse((mgth.TANKA * 1).ToString());
                tusd.KINGAKU = (SqlDecimal)this.FractionCalc(hinKin, (int)mTSeikyuu.KINGAKU_HASUU_CD);

                decimal soto_total = hinKin * zeiRate;
                decimal uchi_total = hinKin - (hinKin / (zeiRate + 1));
                int soto_sign = soto_total < 0 ? -1 : 1;
                int uchi_sign = uchi_total < 0 ? -1 : 1;

                if (mTSeikyuu.TAX_HASUU_CD == 1)
                {
                    //切り上げ
                    if (mTSeikyuu.ZEI_KBN_CD == 1)
                    {
                        //外税
                        tusd.TAX_SOTO = (SqlDecimal)(Math.Ceiling(Math.Abs(soto_total)) * soto_sign);
                        tusd.TAX_UCHI = 0;
                    }
                    else if (mTSeikyuu.ZEI_KBN_CD == 2)
                    {
                        //内税
                        tusd.TAX_SOTO = 0;
                        tusd.TAX_UCHI = (SqlDecimal)(Math.Ceiling(Math.Abs(uchi_total)) * uchi_sign);
                    }
                    else if (mTSeikyuu.ZEI_KBN_CD == 3)
                    {
                        //非課税
                        tusd.TAX_SOTO = 0;
                        tusd.TAX_UCHI = 0;
                    }
                }
                else if (mTSeikyuu.TAX_HASUU_CD == 2)
                {
                    //切り捨て
                    if (mTSeikyuu.ZEI_KBN_CD == 1)
                    {
                        //外税
                        tusd.TAX_SOTO = (SqlDecimal)(Math.Floor(Math.Abs(soto_total)) * soto_sign);
                        tusd.TAX_UCHI = 0;
                    }
                    else if (mTSeikyuu.ZEI_KBN_CD == 2)
                    {
                        //内税
                        tusd.TAX_SOTO = 0;
                        tusd.TAX_UCHI = (SqlDecimal)(Math.Floor(Math.Abs(uchi_total)) * uchi_sign);
                    }
                    else if (mTSeikyuu.ZEI_KBN_CD == 3)
                    {
                        //非課税
                        tusd.TAX_SOTO = 0;
                        tusd.TAX_UCHI = 0;
                    }
                }
                else if (mTSeikyuu.TAX_HASUU_CD == 3)
                {
                    //四捨五入
                    if (mTSeikyuu.ZEI_KBN_CD == 1)
                    {
                        //外税
                        tusd.TAX_SOTO = (SqlDecimal)(Math.Round(Math.Abs(soto_total), 0, MidpointRounding.AwayFromZero) * soto_sign);
                        tusd.TAX_UCHI = 0;
                    }
                    else if (mTSeikyuu.ZEI_KBN_CD == 2)
                    {
                        //内税
                        tusd.TAX_SOTO = 0;
                        tusd.TAX_UCHI = (SqlDecimal)(Math.Round(Math.Abs(uchi_total), 0, MidpointRounding.AwayFromZero) * uchi_sign);
                    }
                    else if (mTSeikyuu.ZEI_KBN_CD == 3)
                    {
                        //非課税
                        tusd.TAX_SOTO = 0;
                        tusd.TAX_UCHI = 0;
                    }
                }
                tusd.HINMEI_ZEI_KBN_CD = SqlInt16.Null;
                tusd.HINMEI_KINGAKU = 0;
                tusd.HINMEI_TAX_SOTO = 0;
                tusd.HINMEI_TAX_UCHI = 0;
            }
            tusd.MEISAI_BIKOU = "";
            tusd.NISUGATA_SUURYOU = 0;
            tusd.NISUGATA_UNIT_CD = 0;

            //タイムスタンプは存在しない

            var addData = new DataBinderLogic<T_UR_SH_DETAIL>(tusd);
            addData.SetSystemProperty(tusd, false);
            DaoInsert(tusd);
            
            /* 一時コメントアウト
            decimal suuryou = 0;
            if (decimal.TryParse(Convert.ToString(crtRow.Rows[v_num].Cells["SUURYOU"].Value), out suuryou)
                && !mgth.CHOUKA_LIMIT_AMOUNT.IsNull
                && suuryou > mgth.CHOUKA_LIMIT_AMOUNT)
            {
                tusd = new T_UR_SH_DETAIL();
                tusd.SYSTEM_ID = sysId;
                tusd.SEQ = 1;
                tusd.DETAIL_SYSTEM_ID = this.accessor.createSystemId();
                tusd.UR_SH_NUMBER = urshNum;
                tusd.ROW_NO = ++rowNo;
                tusd.KAKUTEI_KBN = 1;
                tusd.URIAGESHIHARAI_DATE = SqlDateTime.Parse(DTSeikyuDate.ToShortDateString());
                tusd.DENPYOU_KBN_CD = 1;
                tusd.HINMEI_CD = mgth.HINMEI_CD;
                if (!string.IsNullOrEmpty(mgth.CHOUKA_HINMEI_NAME))
                {
                    tusd.HINMEI_NAME = mgth.CHOUKA_HINMEI_NAME;
                }
                else
                {
                    tusd.HINMEI_NAME = Convert.ToString(crtRow.Rows[v_num].Cells["HINMEI_NAME"].Value);
                }
                tusd.SUURYOU = suuryou - mgth.CHOUKA_LIMIT_AMOUNT;
                tusd.UNIT_CD = 3; // 単位は「3.kg」固定
                decimal tanka = 0;
                decimal.TryParse(Convert.ToString(crtRow.Rows[v_num].Cells["TANKA_CHOUKA"].Value), out tanka);
                tusd.TANKA = tanka;
                if (!mh.ZEI_KBN_CD.Equals(SqlInt16.Null))
                {
                    tusd.KINGAKU = 0;
                    tusd.TAX_SOTO = 0;
                    tusd.TAX_UCHI = 0;
                    tusd.HINMEI_ZEI_KBN_CD = mh.ZEI_KBN_CD;
                    decimal hinKin = decimal.Parse((tusd.TANKA * tusd.SUURYOU).ToString());
                    tusd.HINMEI_KINGAKU = (SqlDecimal)this.FractionCalc(hinKin, (int)mTSeikyuu.KINGAKU_HASUU_CD);

                    decimal soto_total = hinKin * zeiRate;
                    decimal uchi_total = hinKin - (hinKin / (zeiRate + 1));
                    int soto_sign = soto_total < 0 ? -1 : 1;
                    int uchi_sign = uchi_total < 0 ? -1 : 1;

                    if (mTSeikyuu.TAX_HASUU_CD == 1)
                    {
                        //切り上げ
                        if (mh.ZEI_KBN_CD == 1)
                        {
                            //外税
                            tusd.HINMEI_TAX_SOTO = (SqlDecimal)(Math.Ceiling(Math.Abs(soto_total)) * soto_sign);
                            tusd.HINMEI_TAX_UCHI = 0;
                        }
                        else if (mh.ZEI_KBN_CD == 2)
                        {
                            //内税
                            tusd.HINMEI_TAX_SOTO = 0;
                            tusd.HINMEI_TAX_UCHI = (SqlDecimal)(Math.Ceiling(Math.Abs(uchi_total)) * uchi_sign);
                        }
                        else if (mh.ZEI_KBN_CD == 3)
                        {
                            //非課税
                            tusd.HINMEI_TAX_SOTO = 0;
                            tusd.HINMEI_TAX_UCHI = 0;
                        }
                    }
                    else if (mTSeikyuu.TAX_HASUU_CD == 2)
                    {
                        //切り捨て
                        if (mh.ZEI_KBN_CD == 1)
                        {
                            //外税
                            tusd.HINMEI_TAX_SOTO = (SqlDecimal)(Math.Floor(Math.Abs(soto_total)) * soto_sign);
                            tusd.HINMEI_TAX_UCHI = 0;
                        }
                        else if (mh.ZEI_KBN_CD == 2)
                        {
                            //内税
                            tusd.HINMEI_TAX_SOTO = 0;
                            tusd.HINMEI_TAX_UCHI = (SqlDecimal)(Math.Floor(Math.Abs(uchi_total)) * uchi_sign);
                        }
                        else if (mh.ZEI_KBN_CD == 3)
                        {
                            //非課税
                            tusd.HINMEI_TAX_SOTO = 0;
                            tusd.HINMEI_TAX_UCHI = 0;
                        }
                    }
                    else if (mTSeikyuu.TAX_HASUU_CD == 3)
                    {
                        //四捨五入
                        if (mh.ZEI_KBN_CD == 1)
                        {
                            //外税
                            tusd.HINMEI_TAX_SOTO = (SqlDecimal)(Math.Round(Math.Abs(soto_total), 0, MidpointRounding.AwayFromZero) * soto_sign);
                            tusd.HINMEI_TAX_UCHI = 0;
                        }
                        else if (mh.ZEI_KBN_CD == 2)
                        {
                            //内税
                            tusd.HINMEI_TAX_SOTO = 0;
                            tusd.HINMEI_TAX_UCHI = (SqlDecimal)(Math.Round(Math.Abs(uchi_total), 0, MidpointRounding.AwayFromZero) * uchi_sign);
                        }
                        else if (mh.ZEI_KBN_CD == 3)
                        {
                            //非課税
                            tusd.HINMEI_TAX_SOTO = 0;
                            tusd.HINMEI_TAX_UCHI = 0;
                        }
                    }
                }
                else
                {
                    decimal hinKin = decimal.Parse((tusd.TANKA * tusd.SUURYOU).ToString());
                    tusd.KINGAKU = (SqlDecimal)this.FractionCalc(hinKin, (int)mTSeikyuu.KINGAKU_HASUU_CD);

                    decimal soto_total = hinKin * zeiRate;
                    decimal uchi_total = hinKin - (hinKin / (zeiRate + 1));
                    int soto_sign = soto_total < 0 ? -1 : 1;
                    int uchi_sign = uchi_total < 0 ? -1 : 1;

                    if (mTSeikyuu.TAX_HASUU_CD == 1)
                    {
                        //切り上げ
                        if (mTSeikyuu.ZEI_KBN_CD == 1)
                        {
                            //外税
                            tusd.TAX_SOTO = (SqlDecimal)(Math.Ceiling(Math.Abs(soto_total)) * soto_sign);
                            tusd.TAX_UCHI = 0;
                        }
                        else if (mTSeikyuu.ZEI_KBN_CD == 2)
                        {
                            //内税
                            tusd.TAX_SOTO = 0;
                            tusd.TAX_UCHI = (SqlDecimal)(Math.Ceiling(Math.Abs(uchi_total)) * uchi_sign);
                        }
                        else if (mTSeikyuu.ZEI_KBN_CD == 3)
                        {
                            //非課税
                            tusd.TAX_SOTO = 0;
                            tusd.TAX_UCHI = 0;
                        }
                    }
                    else if (mTSeikyuu.TAX_HASUU_CD == 2)
                    {
                        //切り捨て
                        if (mTSeikyuu.ZEI_KBN_CD == 1)
                        {
                            //外税
                            tusd.TAX_SOTO = (SqlDecimal)(Math.Floor(Math.Abs(soto_total)) * soto_sign);
                            tusd.TAX_UCHI = 0;
                        }
                        else if (mTSeikyuu.ZEI_KBN_CD == 2)
                        {
                            //内税
                            tusd.TAX_SOTO = 0;
                            tusd.TAX_UCHI = (SqlDecimal)(Math.Floor(Math.Abs(uchi_total)) * uchi_sign);
                        }
                        else if (mTSeikyuu.ZEI_KBN_CD == 3)
                        {
                            //非課税
                            tusd.TAX_SOTO = 0;
                            tusd.TAX_UCHI = 0;
                        }
                    }
                    else if (mTSeikyuu.TAX_HASUU_CD == 3)
                    {
                        //四捨五入
                        if (mTSeikyuu.ZEI_KBN_CD == 1)
                        {
                            //外税
                            tusd.TAX_SOTO = (SqlDecimal)(Math.Round(Math.Abs(soto_total), 0, MidpointRounding.AwayFromZero) * soto_sign);
                            tusd.TAX_UCHI = 0;
                        }
                        else if (mTSeikyuu.ZEI_KBN_CD == 2)
                        {
                            //内税
                            tusd.TAX_SOTO = 0;
                            tusd.TAX_UCHI = (SqlDecimal)(Math.Round(Math.Abs(uchi_total), 0, MidpointRounding.AwayFromZero) * uchi_sign);
                        }
                        else if (mTSeikyuu.ZEI_KBN_CD == 3)
                        {
                            //非課税
                            tusd.TAX_SOTO = 0;
                            tusd.TAX_UCHI = 0;
                        }
                    }
                    tusd.HINMEI_ZEI_KBN_CD = 0;
                    tusd.HINMEI_KINGAKU = 0;
                    tusd.HINMEI_TAX_SOTO = 0;
                    tusd.HINMEI_TAX_UCHI = 0;
                }
                tusd.MEISAI_BIKOU = "";
                tusd.NISUGATA_SUURYOU = 0;
                tusd.NISUGATA_UNIT_CD = 0;

                //タイムスタンプは存在しない

                addData = new DataBinderLogic<T_UR_SH_DETAIL>(tusd);
                addData.SetSystemProperty(tusd, false);
                DaoInsert(tusd);
             
            }
            */

            LogUtility.DebugMethodEnd();
        }

        #endregion 売上／支払明細テーブル登録

        #region 売上／支払入力新規追加

        /// <summary>
        /// 売上／支払入力新規追加
        /// </summary>
        /// <param name="daoTable">売上／支払入力データ</param>
        [Transaction]
        internal void DaoInsert(T_UR_SH_ENTRY daoTable)
        {
            LogUtility.DebugMethodStart();

            try
            {
                using (Transaction tran = new Transaction())
                {
                    tuseDao.Insert(daoTable);
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    errMsg.MessageBoxShow(ConstInfo.EX_ERR);
                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion 売上／支払入力

        #region 売上／支払明細新規追加

        /// <summary>
        /// 売上／支払明細新規追加
        /// </summary>
        /// <param name="daoTable">売上／支払明細データ</param>
        [Transaction]
        internal void DaoInsert(T_UR_SH_DETAIL daoTable)
        {
            LogUtility.DebugMethodStart();

            try
            {
                using (Transaction tran = new Transaction())
                {
                    tusdDao.Insert(daoTable);
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    errMsg.MessageBoxShow(ConstInfo.EX_ERR);
                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion 売上／支払明細

        #region 伝票日付-From未入力チェック

        /// <summary>
        /// 伝票日付-From未入力チェック
        /// </summary>
        internal bool UninputCheckFrom()
        {
            LogUtility.DebugMethodStart();

            //未入力チェック判定
            if (string.IsNullOrEmpty(this.form.dtpTaishoKikanFrom.Text) || this.form.dtpTaishoKikanFrom.Text == " ")
            {
                errMsg.MessageBoxShow(ConstInfo.ERR_MSG_CD_E012, ConstInfo.ERR_ARGS_TARGET_KIKAN_FROM);
                this.form.dtpTaishoKikanFrom.Select();
                this.form.dtpTaishoKikanFrom.Focus();
                return false;
            }

            ////伝票日付同士の年月が異なる場合チェック判定
            //string fromY = this.form.dtpTaishoKikanFrom.Text.Substring(0, 4);
            //string fromM = this.form.dtpTaishoKikanFrom.Text.Substring(5, 6);
            //string toY = this.form.dtpTaishoKikanTo.Text.Substring(0, 4);
            //string toM = this.form.dtpTaishoKikanTo.Text.Substring(5, 6);

            //if ((fromY != toY) && (fromM != toY))
            //{
            //    this.form.dtpTaishoKikanFrom.Select();
            //    this.form.dtpTaishoKikanFrom.Focus();
            //    return;
            //}

            LogUtility.DebugMethodEnd();
            return true;
        }

        #endregion 伝票日付-From未入力チェック

        #region 伝票日付-To未入力チェック

        /// <summary>
        /// 伝票日付-To未入力チェック
        /// </summary>
        internal bool UninputCheckTo()
        {
            LogUtility.DebugMethodStart();

            if (string.IsNullOrEmpty(this.form.dtpTaishoKikanTo.Text) || this.form.dtpTaishoKikanTo.Text == " ")
            {
                errMsg.MessageBoxShow(ConstInfo.ERR_MSG_CD_E012, ConstInfo.ERR_ARGS_TARGET_KIKAN_TO);
                this.form.dtpTaishoKikanTo.Select();
                this.form.dtpTaishoKikanTo.Focus();
                return false;
            }

            ////伝票日付同士の年月が異なる場合チェック判定
            //string fromY = this.form.dtpTaishoKikanFrom.Text.Substring(0, 4);
            //string fromM = this.form.dtpTaishoKikanFrom.Text.Substring(5, 6);
            //string toY = this.form.dtpTaishoKikanTo.Text.Substring(0, 4);
            //string toM = this.form.dtpTaishoKikanTo.Text.Substring(5, 6);

            //if ((fromY != toY) && (fromM != toY))
            //{
            //    this.form.dtpTaishoKikanTo.Select();
            //    this.form.dtpTaishoKikanTo.Focus();
            //    return;
            //}

            LogUtility.DebugMethodEnd();
            return true;
        }

        #endregion 伝票日付-To未入力チェック

        #endregion メソッド

        #region グリッド発行列制御関連

        /// <summary>
        /// 列ヘッダチェックボックス表示
        /// </summary>
        /// <param name="e"></param>
        internal bool CellPaintingLogic(GrapeCity.Win.MultiRow.CellPaintingEventArgs e)
        {
            try
            {
                // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
                if (e.CellIndex == 0 && e.RowIndex == -1)
                {
                    using (Bitmap bmp = new Bitmap(100, 100))
                    {
                        // チェックボックスの描画領域を確保
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.Clear(Color.Transparent);
                        }

                        // 描画領域の中央に配置
                        Point pt1 = new Point((bmp.Width - this.form.chkSakusei.Width) / 2, (bmp.Height - this.form.chkSakusei.Height) / 2);
                        if (pt1.X < 0) pt1.X = 0;
                        if (pt1.Y < 0) pt1.Y = 0;

                        // Bitmapに描画
                        this.form.chkSakusei.DrawToBitmap(bmp, new Rectangle(pt1.X, pt1.Y, bmp.Width, bmp.Height));

                        // DataGridViewの現在描画中のセルの中央に描画
                        int x = (e.CellBounds.Width - bmp.Width) / 2;
                        int y = ((e.CellBounds.Height - bmp.Height) / 4) - 2;

                        Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                        e.Paint(e.ClipBounds);
                        e.Graphics.DrawImage(bmp, pt2);
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CellPaintingLogic", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え
        /// </summary>
        /// <param name="e"></param>
        internal void CellClickLogic(GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            LogUtility.DebugMethodStart();

            if (e.CellIndex == 0 && e.RowIndex == -1)
            {
                this.form.chkSakusei.Checked = !this.form.chkSakusei.Checked;
                //this.form.mrwTukigimeUriageDenpyo.Refresh();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// すべての行のチェック状態を切り替える
        /// </summary>
        internal bool checkBoxAllCheckedChangedLogic()
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.form.mrwTukigimeUriageDenpyo.IsBrowsePurpose = false;
                this.form.mrwTukigimeUriageDenpyo.EndEdit();
                this.form.mrwTukigimeUriageDenpyo.BeginEdit(true);

                if (this.form.mrwTukigimeUriageDenpyo.Rows.Count != 0)
                {
                    int iCnt = 0;
                    foreach (GrapeCity.Win.MultiRow.Row row in this.form.mrwTukigimeUriageDenpyo.Rows)
                    {
                        row.Cells[0].Value = this.form.chkSakusei.Checked;
                        this.form.mrwTukigimeUriageDenpyo.RemoveSelection(iCnt);
                        iCnt++;
                    }
                    this.form.mrwTukigimeUriageDenpyo.EndEdit();

                    this.form.mrwTukigimeUriageDenpyo.Refresh();
                    this.form.mrwTukigimeUriageDenpyo.IsBrowsePurpose = true;
                }

                this.form.chkSakusei.Focus();

                /*
                bool isChecked = false;
                for (int i = 0; i < this.form.mrwTukigimeUriageDenpyo.Rows.Count; i++)
                {
                    this.form.mrwTukigimeUriageDenpyo.Rows[i].Cells[0].Value = this.form.chkSakusei.Checked;
                    isChecked = true;
                }
                if (isChecked)
                {
                    //this.form.dgvSeisanDenpyouItiran.CurrentCell = this.form.dgvSeisanDenpyouItiran.Rows[0].Cells[1];
                    this.form.mrwTukigimeUriageDenpyo.CurrentCell = this.form.mrwTukigimeUriageDenpyo.Rows[0].Cells[0];
                }

                this.form.mrwTukigimeUriageDenpyo.Refresh();
                */
            }
            catch (Exception ex)
            {
                LogUtility.Error("checkBoxAllCheckedChangedLogic", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion グリッド発行列制御関連

        #region Unused

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

        #endregion Unused

        /// <summary>
        /// ヘッダコントロールを取得します
        /// </summary>
        /// <returns>ヘッダコントロール</returns>
        internal UIHeader GetHeader()
        {
            return this.header;
        }

        /// <summary>
        /// 取引先の締日チェックを行います
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="shimebi">締日</param>
        /// <returns>チェック結果</returns>
        internal bool CheckTorihikisakiShimebi(string torihikisakiCd, string shimebi, out bool catchErr)
        {
            catchErr = true;
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(torihikisakiCd, shimebi);

                var mTorihikisakiSeikyuu = this.mtseikyuuDao.GetTorihikisakiSeikyuuByTorihikisakiCdAndShimebi1(torihikisakiCd, shimebi);
                if (null != mTorihikisakiSeikyuu)
                {
                    ret = true;
                }

                var mTorihikisakiShiharai = this.mtshiharaiDao.GetTorihikisakiShiharaiByTorihikisakiCdAndShimebi1(torihikisakiCd, shimebi);
                if (null != mTorihikisakiShiharai)
                {
                    ret = true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckTorihikisakiShimebi", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTorihikisakiShimebi", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(ret, catchErr);

            return ret;
        }

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
            S_NUMBER_DAY[] numberDays = this.accessor.GetNumberDay(date.Date, SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI, kyotenCd);

            if (numberDays == null || numberDays.Length < 1)
            {
                targetEntity.CURRENT_NUMBER = 1;
                this.accessor.InsertNumberDay(targetEntity);
            }
            else
            {
                targetEntity.CURRENT_NUMBER = numberDays[0].CURRENT_NUMBER + 1;
                targetEntity.TIME_STAMP = numberDays[0].TIME_STAMP;
                this.accessor.UpdateNumberDay(targetEntity);
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
            S_NUMBER_YEAR[] numberYeas = this.accessor.GetNumberYear(numberedYear, SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI, kyotenCd);

            if (numberYeas == null || numberYeas.Length < 1)
            {
                targetEntity.CURRENT_NUMBER = 1;
                this.accessor.InsertNumberYear(targetEntity);
            }
            else
            {
                targetEntity.CURRENT_NUMBER = numberYeas[0].CURRENT_NUMBER + 1;
                targetEntity.TIME_STAMP = numberYeas[0].TIME_STAMP;
                this.accessor.UpdateNumberYear(targetEntity);
            }

            currentNumber = (int)targetEntity.CURRENT_NUMBER;

            return currentNumber;
        }

        #endregion

        // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 start

        #region 日付チェック

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            try
            {
                this.form.dtpTaishoKikanFrom.BackColor = Constans.NOMAL_COLOR;
                this.form.dtpTaishoKikanTo.BackColor = Constans.NOMAL_COLOR;
                this.form.dtpSeikyuDate.BackColor = Constans.NOMAL_COLOR;
                // 入力されない場合
                if (string.IsNullOrEmpty(this.form.dtpTaishoKikanFrom.Text))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(this.form.dtpTaishoKikanTo.Text))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(this.form.dtpSeikyuDate.Text))
                {
                    return false;
                }

                DateTime date_from = DateTime.Parse(this.form.dtpTaishoKikanFrom.Text);
                DateTime date_to = DateTime.Parse(this.form.dtpTaishoKikanTo.Text);
                DateTime date_seikyu = DateTime.Parse(this.form.dtpSeikyuDate.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.dtpTaishoKikanFrom.IsInputErrorOccured = true;
                    this.form.dtpTaishoKikanTo.IsInputErrorOccured = true;
                    this.form.dtpTaishoKikanFrom.BackColor = Constans.ERROR_COLOR;
                    this.form.dtpTaishoKikanTo.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "対象期間From", "対象期間To" };
                    MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                    msglogic.MessageBoxShow("E030", errorMsg);
                    this.form.dtpTaishoKikanFrom.Focus();
                    return true;
                }

                // 日付FROM > 売上日付場合
                if (date_seikyu.CompareTo(date_from) < 0)
                {
                    this.form.dtpTaishoKikanFrom.IsInputErrorOccured = true;
                    this.form.dtpSeikyuDate.IsInputErrorOccured = true;
                    this.form.dtpTaishoKikanFrom.BackColor = Constans.ERROR_COLOR;
                    this.form.dtpSeikyuDate.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "対象期間From", "売上日付" };
                    MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                    msglogic.MessageBoxShow("E030", errorMsg);
                    this.form.dtpTaishoKikanFrom.Focus();
                    return true;
                }

                // 売上日付 > 日付TO 場合
                if (date_to.CompareTo(date_seikyu) < 0)
                {
                    this.form.dtpSeikyuDate.IsInputErrorOccured = true;
                    this.form.dtpTaishoKikanTo.IsInputErrorOccured = true;
                    this.form.dtpSeikyuDate.BackColor = Constans.ERROR_COLOR;
                    this.form.dtpTaishoKikanTo.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "売上日付", "対象期間To" };
                    MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                    msglogic.MessageBoxShow("E030", errorMsg);
                    this.form.dtpTaishoKikanTo.Focus();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDate", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }

            return false;
        }

        #endregion

        // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 end

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
            List<ReturnDate> returnDate = new List<ReturnDate>();
            List<CheckDate> uriageList = new List<CheckDate>();

            // 拠点
            string strKyotenCd = this.form.txtKyotenCd.Text;
            // 日付
            string strDenpyouDate = this.form.dtpSeikyuDate.Text;

            if (string.IsNullOrEmpty(strDenpyouDate))
            {
                return true;
            }
            if (string.IsNullOrEmpty(strKyotenCd))
            {
                return true;
            }

            DateTime denpyouDate = Convert.ToDateTime(strDenpyouDate);

            CheckDate cd = new CheckDate();
            foreach (Row row in this.form.mrwTukigimeUriageDenpyo.Rows)
            {
                //チェックの入っていない行は飛ばす
                if (!(bool)row.Cells["CHK_SAKUSEI"].Value)
                {
                    continue;
                }

                if (row.IsNewRow)
                {
                    break;
                }

                cd = new CheckDate();
                string strTorihikisakiCd = Convert.ToString(row.Cells["TORIHIKISAKI_CD"].Value);

                if (string.IsNullOrEmpty(strTorihikisakiCd))
                {
                    continue;
                }

                cd.TORIHIKISAKI_CD = strTorihikisakiCd;
                cd.CHECK_DATE = denpyouDate;
                cd.KYOTEN_CD = strKyotenCd;

                uriageList.Add(cd);
            }

            // 売上チェック
            returnDate = CheckShimeDate.GetNearShimeDate(uriageList, 1);

            if (returnDate.Count != 0)
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
        // 20141201 teikyou ダブルクリックを追加する　start
        private void dtpTaishoKikanTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var dtpTaishoKikanFromTextBox = this.form.dtpTaishoKikanFrom;
            var dtpTaishoKikanToTextBox = this.form.dtpTaishoKikanTo;
            dtpTaishoKikanToTextBox.Text = dtpTaishoKikanFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }

        // 20141201 teikyou ダブルクリックを追加する　end

        #endregion

        /// <summary>
        /// 指定された端数CDに従い、金額の端数処理を行う
        /// </summary>
        /// <param name="kingaku">端数処理対象金額</param>
        /// <param name="calcCD">端数CD</param>
        /// <returns name="decimal">端数処理後の金額</returns>
        public decimal FractionCalc(decimal kingaku, int calcCD)
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

        #region 拠点入力チェック thongh 20150803 #11933 拠点必須のアラート表示

        /// <summary>
        /// 拠点入力チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckKyotenToSearch()
        {
            LogUtility.DebugMethodStart();
            bool ret = false;
            if (string.IsNullOrEmpty(this.form.txtKyotenCd.Text))
            {
                // エラーメッセージ
                errMsg.MessageBoxShow("E001", "拠点CD");
                ret = true;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }

        #endregion 拠点入力チェック thongh 20150803 #11933 拠点必須のアラート表示

        #region 売上日付入力チェック
        /// <summary>
        /// 売上日付入力チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckSeikyuDateToSearch()
        {
            LogUtility.DebugMethodStart();
            bool ret = false;
            if (string.IsNullOrEmpty(this.form.dtpSeikyuDate.Text))
            {
                // エラーメッセージ
                errMsg.MessageBoxShow("E012", "売上日付");
                ret = true;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }
        #endregion 売上日付入力チェック

        public DataTable Group(DataTable table)
        {
            if (table == null || table.Rows.Count <= 1)
            {
                return table;
            }

            DataTable ret = table.Clone();
            ret.Columns["SUURYOU"].ReadOnly = false;
            DataRow dr = ret.NewRow();
            DataRow row = table.Rows[0];
            foreach (DataColumn col in table.Columns)
            {
                dr[col.ColumnName] = row[col.ColumnName];
            }
            ret.Rows.Add(dr);

            int index = 0;
            for (int i = 1; i < table.Rows.Count; i++)
            {
                dr = ret.Rows[index];
                row = table.Rows[i];
                if (Convert.ToString(dr["TORIHIKISAKI_CD"]) == Convert.ToString(row["TORIHIKISAKI_CD"])
                    && Convert.ToString(dr["GYOUSHA_CD"]) == Convert.ToString(row["GYOUSHA_CD"])
                    && Convert.ToString(dr["GENBA_CD"]) == Convert.ToString(row["GENBA_CD"])
                    && Convert.ToString(dr["HINMEI_CD"]) == Convert.ToString(row["HINMEI_CD"]))
                {
                    dr["SUURYOU"] = ToDecimal(dr["SUURYOU"]) + ToDecimal(row["SUURYOU"]);
                }
                else
                {
                    dr = ret.NewRow();
                    foreach (DataColumn col in table.Columns)
                    {
                        dr[col.ColumnName] = row[col.ColumnName];
                    }
                    ret.Rows.Add(dr);
                    index++;
                }
            }
            ret.Columns["SUURYOU"].ReadOnly = true;

            return ret;
        }

        public decimal ToDecimal(object obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
            {
                return 0;
            }
            decimal ret = 0;
            decimal.TryParse(obj.ToString(), out ret);
            return ret;
        }
    }
}