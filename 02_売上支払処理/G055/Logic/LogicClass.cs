using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.SalesPayment.Denpyouichiran.APP;
using Shougun.Core.SalesPayment.Denpyouichiran.DAO;
using Shougun.Core.SalesPayment.Denpyouichiran.DBAccesser;
using Seasar.Framework.Exceptions;
using System.Data.SqlTypes;

namespace Shougun.Core.SalesPayment.Denpyouichiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicClass : IBuisinessLogic
    {
        #region フィールド

        // 日付CD
        const int HIDUKE_SENTAKU_DENPYOU = 1;
        const int HIDUKE_SENTAKU_NYUURYOKU = 2;

        // 伝票種類
        const int DENPYOU_TYPE_UKEIRE = 1;
        const int DENPYOU_TYPE_SHUKKA = 2;
        const int DENPYOU_TYPE_URIAGESHIHARAI = 3;
        //20150422 Jyokou 4935_4 STR
        const int DENPYOU_TYPE_DAINOU = 4;
        //20150422 Jyokou 4935_4 END
        const int DENPYOU_TYPE_SUBETE = 5; //PhuocLoc 2021/05/05 #148576

        // 伝票区分
        const int DENPYOU_KBN_URIAGE = 1;
        const int DENPYOU_KBN_SHIHARAI = 2;
        const int DENPYOU_KBN_ALL = 3;

        // 検収有無
        const int KENSHU_MUST_KBN_FALSE = 1;
        const int KENSHU_MUST_KBN_TRUE = 2;
        const int KENSHU_MUST_KBN_ALL = 3;

        // 検収状況
        const int KENSHU_JYOUKYOU_MIKENSHU = 1;
        const int KENSHU_JYOUKYOU_KENSHUZUMI = 2;

        #region 非表示にする必須項目
        /// <summary>
        /// 伝票番号
        /// </summary>
        private readonly string HIDDEN_DENPYOU_NUMBER = "HIDDEN_DENPYOU_NUMBER";

        /// <summary>
        /// 伝種区分CD
        /// </summary>
        private readonly string HIDDEN_DENSHU_KBN_CD = "HIDDEN_DENSHU_KBN_CD";

        /// <summary>
        /// システムID
        /// </summary>
        private readonly string HIDDEN_SYSTEM_ID = "HIDDEN_SYSTEM_ID";

        /// <summary>
        /// 明細システムID
        /// </summary>
        private readonly string HIDDEN_DETAIL_SYSTEM_ID = "HIDDEN_DETAIL_SYSTEM_ID";

        #endregion

        //入力チェックメッセージタイトル
        private const String DIALOGTITLE = "インフォメーション";

        //初期表示フラグ
        private static bool InitialFlg = false; // No.2320

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.SalesPayment.Denpyouichiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// DBアクセッサー
        /// </summary>
        private DBAccessor accessor;

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>	
        /// 拠点マスタ	
        /// </summary>	
        private IM_KYOTENDao mkyotenDao;

        /// <summary>
        /// 取引先マスタ
        /// </summary>
        private IM_TORIHIKISAKIDao mtorihikisakiDao;

        /// <summary>
        /// 業者マスタ
        /// </summary>
        private IM_GYOUSHADao mgyoushaDao;

        /// <summary>
        /// 現場マスタ
        /// </summary>
        private IM_GENBADao mgenbaDao;

        /// <summary>
        /// 運搬業者マスタ
        /// </summary>
        private IM_GYOUSHADao munbangyousyaDao;

        /// <summary>
        /// 荷卸業者マスタ
        /// </summary>
        private IM_GYOUSHADao mniotosugyousyaDao;

        /// <summary>
        /// 荷卸場マスタ
        /// </summary>
        private IM_GENBADao mniotosubaDao;

        /// <summary>
        /// パターン一覧のDao
        /// </summary>
        private UKEIREK_DETAIL_DaoCls ukeirek_detail_daocls;
        private SHUKKAK_DETAIL_DaoCls shukkak_detail_daocls;
        private UR_SHK_DETAIL_DaoCls ur_shk_detail_daocls;

        /// <summary>
        /// パターン一覧のDao
        /// </summary>
        private UKEIREK_ENTRY_DaoCls ukeirek_entry_daocls;
        private SHUKKAK_ENTRY_DaoCls shukkak_entry_daocls;
        private UR_SHK_ENTRY_DaoCls ur_shk_entry_daocls;

        /// <summary>
        /// UriageShiharaiIchiranForm form
        /// </summary>
        private DenpyouichiranForm form;

        /// <summary>
        /// 
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// HeaderForm headForm
        /// </summary>
        HeaderForm headForm;

        /// <summary>
        /// 社員コード
        /// </summary>
        public string syainCode { get; set; }

        /// <summary>
        /// 伝種区分
        /// </summary>
        public DENSHU_KBN denShu_Kbn { get; set; }

        /// <summary>
        /// 伝票種類フラグ
        /// </summary>
        public int disp_Flg { get; set; }

        /// <summary>
        /// 作成したSQL
        /// </summary>
        public string mcreateSql { get; set; }

        /// <summary>
        /// コントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// コントロール
        /// </summary>
        private KensakuControl control;

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private DAOClass daoIchiran;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;
        private KensakuControl kensakuControl;

        /// <summary>
        /// フッター
        /// </summary>
        private BusinessBaseForm parentForm;

        // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        /// <summary>
        /// 前回業者CD
        /// </summary>
        internal string BEFORE_GYOUSYA_CD;

        /// <summary>
        /// 前回荷卸業者CD
        /// </summary>
        internal string BEFORE_NIOROSHIGYOUSYA_CD;

        /// <summary>
        /// 前回荷積業者CD
        /// </summary>
        internal string BEFORE_NIDUMIGYOUSYA_CD;

        /// <summary>
        /// 前回出荷業者CD
        /// </summary>
        internal string BEFORE_SHUKKA_GYOUSYA_CD;
        // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();
        #endregion

        #region プロパティ
        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable searchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public string searchString { get; set; }

        /// <summary>
        /// SELECT句
        /// </summary>
        public string selectQuery { get; set; }

        /// <summary>
        /// ORDERBY句
        /// </summary>
        public string orderByQuery { get; set; }

        /// <summary>
        /// JOIN句
        /// </summary>
        public string joinQuery { get; set; }

        /// <summary>
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(DenpyouichiranForm targetForm)
        {
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.form = targetForm;
            this.dto = new DTOClass();
            this.daoIchiran = DaoInitUtility.GetComponent<DAOClass>();
            this.ukeirek_detail_daocls = DaoInitUtility.GetComponent<UKEIREK_DETAIL_DaoCls>();
            this.ukeirek_entry_daocls = DaoInitUtility.GetComponent<UKEIREK_ENTRY_DaoCls>();
            this.shukkak_detail_daocls = DaoInitUtility.GetComponent<SHUKKAK_DETAIL_DaoCls>();
            this.shukkak_entry_daocls = DaoInitUtility.GetComponent<SHUKKAK_ENTRY_DaoCls>();
            this.ur_shk_detail_daocls = DaoInitUtility.GetComponent<UR_SHK_DETAIL_DaoCls>();
            this.ur_shk_entry_daocls = DaoInitUtility.GetComponent<UR_SHK_ENTRY_DaoCls>();
            this.mkyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.mtorihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.mgyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.mgenbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.munbangyousyaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.mniotosugyousyaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.mniotosubaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            // Accessor
            this.accessor = new DBAccessor();

            //検索方法によって、簡易検索／汎用検索を初期化する。
            KensakuControl hannyousearch = new KensakuControl(this);
            hannyousearch.Top = 3;
            hannyousearch.Left = 3;
            // 簡易検索をデフォルト
            this.form.searchString.Visible = false;
            this.control = hannyousearch;
            //CongBinh 20200331 #134988 S
            for (var i = 0; i < hannyousearch.Controls.Count; i++)
            {
                this.form.Controls.Add((Control)hannyousearch.Controls[i]);
                i--;
            }
            //CongBinh 20200331 #134988 E
        }

        public LogicClass(KensakuControl kensakuControl)
        {
            // TODO: Complete member initialization
            this.kensakuControl = kensakuControl;
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode(this.form.SystemId.ToString());
                if (sysInfo != null)
                {
                    // システム情報からアラート件数を取得
                    this.alertCount = (int)sysInfo.ICHIRAN_ALERT_KENSUU;
                }

                //headerFormにSettingsの値
                if (!this.SetDenpyouHidukeInit())
                {
                    return false;
                }

                #region ポップアップ設定

                //前回保存値がない場合はシステム設定ファイルから拠点CDを設定する
                //拠点CDを取得  
                XMLAccessor fileAccess = new XMLAccessor();
                CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();
                this.headForm.KYOTEN_CD.Text = String.Format("{0:D2}", int.Parse(configProfile.ItemSetVal1));

                // ユーザ拠点名称の取得
                if (this.headForm.KYOTEN_CD.Text != null)
                {
                    M_KYOTEN mKyoten = new M_KYOTEN();
                    mKyoten = (M_KYOTEN)mkyotenDao.GetDataByCd(this.headForm.KYOTEN_CD.Text);
                    if (mKyoten == null || this.headForm.KYOTEN_CD.Text == "")
                    {
                        this.headForm.KYOTEN_NAME_RYAKU.Text = "";
                    }
                    else
                    {
                        this.headForm.KYOTEN_NAME_RYAKU.Text = mKyoten.KYOTEN_NAME_RYAKU;
                    }
                }

                // 検収有無
                this.form.txtNum_KenshuMustKbn.Text = KENSHU_MUST_KBN_ALL.ToString();      // 初期化
                var kenshuMustKbn = Properties.Settings.Default.SET_KENSHU_MUST_KBN;
                if (kenshuMustKbn != null && !string.IsNullOrEmpty(kenshuMustKbn))
                {
                    if (KENSHU_MUST_KBN_TRUE.ToString().Equals(kenshuMustKbn))
                    {
                        this.form.txtNum_KenshuMustKbn.Text = KENSHU_MUST_KBN_TRUE.ToString();
                    }
                    else if (KENSHU_MUST_KBN_FALSE.ToString().Equals(kenshuMustKbn))
                    {
                        this.form.txtNum_KenshuMustKbn.Text = KENSHU_MUST_KBN_FALSE.ToString();
                    }
                    else if (KENSHU_MUST_KBN_ALL.ToString().Equals(kenshuMustKbn))
                    {
                        this.form.txtNum_KenshuMustKbn.Text = KENSHU_MUST_KBN_ALL.ToString();
                    }
                }

                // 検収状況
                this.form.txtNum_KenshuJyoukyou.Text = KENSHU_JYOUKYOU_MIKENSHU.ToString();      // 初期化
                var kenshuJyoukyou = Properties.Settings.Default.SET_KENSHU_JYOUKYOU;
                if (kenshuJyoukyou != null && !string.IsNullOrEmpty(kenshuJyoukyou))
                {
                    if (KENSHU_JYOUKYOU_MIKENSHU.ToString().Equals(kenshuJyoukyou))
                    {
                        this.form.txtNum_KenshuJyoukyou.Text = KENSHU_JYOUKYOU_MIKENSHU.ToString();
                    }
                    else if (KENSHU_JYOUKYOU_KENSHUZUMI.ToString().Equals(kenshuJyoukyou))
                    {
                        this.form.txtNum_KenshuJyoukyou.Text = KENSHU_JYOUKYOU_KENSHUZUMI.ToString();
                    }
                }
                #endregion

                this.allControl = this.form.allControl;
                //行の追加オプション(false)
                this.form.customDataGridView1.AllowUserToAddRows = false;

                ////画面の初期表示時日付CDを設定する
                if (string.IsNullOrEmpty(Properties.Settings.Default.SET_HIDUKESENTAKU) || InitialFlg == false) // No.2320
                {
                    this.headForm.txtNum_HidukeSentaku.Text = HIDUKE_SENTAKU_DENPYOU.ToString();
                }
                else
                {
                    this.headForm.txtNum_HidukeSentaku.Text = Properties.Settings.Default.SET_HIDUKESENTAKU;
                }
                InitialFlg = false;


                //伝票種類、伝票区分の初期化
                SetInitialDenpyoShurui();

                ////汎用検索の取得
                control.TORIHIKISAKI_CD.Text = Properties.Settings.Default.SET_TORIHIKISAKI;
                control.GYOUSYA_CD.Text = Properties.Settings.Default.SET_GYOUSYA;
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                BEFORE_GYOUSYA_CD = control.GYOUSYA_CD.Text;
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                if (control.GYOUSYA_CD.Text != "")
                {
                    control.GENNBA_CD.Text = Properties.Settings.Default.SET_GENNBA;
                }
                control.UNNBANGYOUSYA_CD.Text = Properties.Settings.Default.SET_UNBANGYOUSYA;

                control.NIDUMIGYOUSYA_CD.Text = Properties.Settings.Default.SET_NIDUMIGYOUSYA;
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                BEFORE_NIDUMIGYOUSYA_CD = control.NIDUMIGYOUSYA_CD.Text;
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                if (control.NIDUMIGYOUSYA_CD.Text != "")
                {
                    control.NIDUMIGENBA_CD.Text = Properties.Settings.Default.SET_NIDUMIGENBA;
                }

                control.NIOROSHIGYOUSYA_CD.Text = Properties.Settings.Default.SET_NIOTOSUGYOUSYA;
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                BEFORE_NIOROSHIGYOUSYA_CD = control.NIOROSHIGYOUSYA_CD.Text;
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                if (control.NIOROSHIGYOUSYA_CD.Text != "")
                {
                    control.NIOROSHIJYOUMEI_CD.Text = Properties.Settings.Default.SET_NIOTOSUBA;
                }

                // ユーザ取引先名称の取得
                if (control.TORIHIKISAKI_CD.Text != string.Empty)
                {
                    M_TORIHIKISAKI mtorihikisaki = new M_TORIHIKISAKI();
                    mtorihikisaki = (M_TORIHIKISAKI)mtorihikisakiDao.GetDataByCd(control.TORIHIKISAKI_CD.Text);
                    if (mtorihikisaki == null)
                    {
                        control.TORIHIKISAKI_NAME_RYAKU.Text = "";
                    }
                    else
                    {
                        control.TORIHIKISAKI_NAME_RYAKU.Text = mtorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                }

                // ユーザ業者名称の取得
                if (control.GYOUSYA_CD.Text != string.Empty)
                {
                    M_GYOUSHA mGyousha = new M_GYOUSHA();
                    mGyousha = (M_GYOUSHA)mgyoushaDao.GetDataByCd(control.GYOUSYA_CD.Text);
                    if (mGyousha == null)
                    {
                        control.GYOUSYA_NAME_RYAKU.Text = "";
                    }
                    else
                    {
                        control.GYOUSYA_NAME_RYAKU.Text = mGyousha.GYOUSHA_NAME_RYAKU;
                    }
                }

                // ユーザ現場名称の取得
                if (control.GYOUSYA_CD.Text != string.Empty && control.GENNBA_CD.Text != string.Empty)
                {
                    M_GENBA mGenbaOut = new M_GENBA();
                    M_GENBA mGenbaIn = new M_GENBA();
                    mGenbaIn.GYOUSHA_CD = control.GYOUSYA_CD.Text;
                    mGenbaIn.GENBA_CD = control.GENNBA_CD.Text;
                    mGenbaOut = (M_GENBA)mgenbaDao.GetDataByCd(mGenbaIn);
                    if (mGenbaOut == null)
                    {
                        control.GENNBA_NAME_RYAKU.Text = "";
                    }
                    else
                    {
                        control.GENNBA_NAME_RYAKU.Text = mGenbaOut.GENBA_NAME_RYAKU;
                    }
                }

                // ユーザ運搬業者名称の取得
                if (control.UNNBANGYOUSYA_CD.Text != string.Empty)
                {
                    M_GYOUSHA mUnbangyousya = new M_GYOUSHA();
                    mUnbangyousya = (M_GYOUSHA)munbangyousyaDao.GetDataByCd(control.UNNBANGYOUSYA_CD.Text);
                    if (mUnbangyousya == null)
                    {
                        control.UNNBANGYOUSYA_NAME_RYAKU.Text = "";
                    }
                    else
                    {
                        control.UNNBANGYOUSYA_NAME_RYAKU.Text = mUnbangyousya.GYOUSHA_NAME_RYAKU;
                    }
                }

                // ユーザ荷卸業者名称の取得
                if (control.NIOROSHIGYOUSYA_CD.Text != string.Empty)
                {
                    M_GYOUSHA mNiotoshigyousya = new M_GYOUSHA();
                    mNiotoshigyousya = (M_GYOUSHA)mniotosugyousyaDao.GetDataByCd(control.NIOROSHIGYOUSYA_CD.Text);
                    if (mNiotoshigyousya == null)
                    {
                        control.NIOROSHIGYOUSYA_NAME_RYAKU.Text = "";
                    }
                    else
                    {
                        control.NIOROSHIGYOUSYA_NAME_RYAKU.Text = mNiotoshigyousya.GYOUSHA_NAME_RYAKU;
                    }
                }

                // ユーザ荷卸場名称の取得
                if (control.NIOROSHIGYOUSYA_CD.Text != string.Empty && control.NIOROSHIJYOUMEI_CD.Text != string.Empty)
                {
                    M_GENBA mNioroshiOut = new M_GENBA();
                    M_GENBA mNioroshiIn = new M_GENBA();
                    mNioroshiIn.GYOUSHA_CD = control.NIOROSHIGYOUSYA_CD.Text;
                    mNioroshiIn.GENBA_CD = control.NIOROSHIJYOUMEI_CD.Text;
                    mNioroshiOut = (M_GENBA)mgenbaDao.GetDataByCd(mNioroshiIn);
                    if (mNioroshiOut == null)
                    {
                        control.NIOROSHIJYOUMEI_NAME_RYAKU.Text = "";
                    }
                    else
                    {
                        control.NIOROSHIJYOUMEI_NAME_RYAKU.Text = mNioroshiOut.GENBA_NAME_RYAKU;
                    }
                }

                // ユーザ荷卸業者名称の取得
                if (control.NIDUMIGYOUSYA_CD.Text != string.Empty)
                {
                    M_GYOUSHA mNidumigyousya = new M_GYOUSHA();
                    mNidumigyousya = (M_GYOUSHA)mniotosugyousyaDao.GetDataByCd(control.NIDUMIGYOUSYA_CD.Text);
                    if (mNidumigyousya == null)
                    {
                        control.NIDUMIGYOUSYA_NAME_RYAKU.Text = "";
                    }
                    else
                    {
                        control.NIDUMIGYOUSYA_NAME_RYAKU.Text = mNidumigyousya.GYOUSHA_NAME_RYAKU;
                    }
                }


                // ユーザ荷積現場名称の取得
                if (control.NIDUMIGYOUSYA_CD.Text != string.Empty && control.NIDUMIGENBA_CD.Text != string.Empty)
                {
                    M_GENBA mNidumiOut = new M_GENBA();
                    M_GENBA mNidumiIn = new M_GENBA();
                    mNidumiIn.GYOUSHA_CD = control.NIDUMIGYOUSYA_CD.Text;
                    mNidumiIn.GENBA_CD = control.NIDUMIGENBA_CD.Text;
                    mNidumiOut = (M_GENBA)mgenbaDao.GetDataByCd(mNidumiIn);
                    if (mNidumiOut == null)
                    {
                        control.NIDUMIGENBA_NAME_RYAKU.Text = "";
                    }
                    else
                    {
                        control.NIDUMIGENBA_NAME_RYAKU.Text = mNidumiOut.GENBA_NAME_RYAKU;
                    }
                }

                // 上で子フォームへのデータバインドを行っている為、以下の初期化処理はそれより下に配置（二度呼ばれることを避ける）
                // ボタンのテキストを初期化
                this.ButtonInit();

                // 伝票種類のテキストボックスを初期化しておく
                this.form.txtNum_DenpyoKind.Text = string.Empty;

                // イベントの初期化処理
                this.EventInit();

                //伝票種類、伝票区分の初期化
                SetInitialDenpyoShurui();

                //検索ボタンの初期表示
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle("伝票一覧");

                Control[] copy = new Control[this.form.allControl.GetLength(0) + 1];

                this.form.allControl.CopyTo(copy, 1);
                copy[0] = this.headForm.txtNum_HidukeSentaku;
                this.allControl = copy;
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
            return ret;
        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }

        #endregion

        #region イベント処理の初期化
        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            parentForm = (BusinessBaseForm)this.form.Parent;

            //伝票種類切り替えイベント
            this.form.txtNum_DenpyoKind.TextChanged += new EventHandler(this.txtNum_DenpyouSyurui_TextChanged);
            // 伝票有無切り替えイベント
            this.form.txtNum_KenshuMustKbn.TextChanged += new EventHandler(this.txtNum_KenshuMustKbn_TextChanged);

            //Functionボタンのイベント生成
            parentForm.bt_func1.Click += new EventHandler(this.bt_func1_Click);              //F1 簡易検索／汎用検索
            parentForm.bt_func2.Click += new EventHandler(this.bt_func2_Click);              //F2 新規
            parentForm.bt_func3.Click += new System.EventHandler(this.bt_func3_Click);       //F3 修正
            parentForm.bt_func4.Click += new System.EventHandler(this.bt_func4_Click);       //F4 削除
            parentForm.bt_func5.Click += new System.EventHandler(this.bt_func5_Click);       //F5 複写
            parentForm.bt_func6.Click += new System.EventHandler(this.bt_func6_Click);       //F6 CSV出力
            parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);       //F7 検索条件クリア
            parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);       //F8 検索
            parentForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);     //F10 並び替え
            parentForm.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);     //F11 フィルタ
            parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);     //閉じる
            parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);             //パターン一覧画面へ遷移
            parentForm.bt_process2.Click += new EventHandler(bt_process2_Click);             //検索条件設定画面へ遷移
            parentForm.bt_process3.Click += new EventHandler(this.bt_process3_Click);             //産廃マニフェスト
            parentForm.bt_process4.Click += new EventHandler(this.bt_process4_Click);             //積替マニフェスト
            parentForm.bt_process5.Click += new EventHandler(this.bt_process5_Click);             //建廃マニフェスト
            parentForm.FormClosing += new FormClosingEventHandler(SetPrevStatus);

            //画面上でESCキー押下時のイベント生成
            this.form.PreviewKeyDown += new PreviewKeyDownEventHandler(form_PreviewKeyDown); //form上でのESCキー押下でFocus移動
            //  明細画面上でダブルクリック時のイベント生成
            this.form.customDataGridView1.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(customDataGridView1_MouseDoubleClick);

            /// 20141023 Houkakou 「運賃集計表」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.headForm.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDUKE_TO_MouseDoubleClick);
            /// 20141023 Houkakou 「運賃集計表」のダブルクリックを追加する　end
            //受入出荷画面サイズ選択取得
            HearerSysInfoInit();
            this.control.TORIHIKISAKI_CD.Validating += new System.ComponentModel.CancelEventHandler(TORIHIKISAKI_CD_Validating); //CongBinh 20200331 #134988
            this.control.SHUKKA_TORIHIKISAKI_CD.Validating += new System.ComponentModel.CancelEventHandler(SHUKKA_TORIHIKISAKI_CD_Validating); //CongBinh 20200331 #134988
            this.control.cmbShimebi.SelectedIndexChanged += new EventHandler(cmbShimebi_SelectedIndexChanged);//CongBinh 20200331 #134988
            this.control.cmbShihariaShimebi.SelectedIndexChanged += new EventHandler(cmbShihariaShimebi_SelectedIndexChanged);//CongBinh 20200331 #134988
            this.control.cmbDainoShimebi.SelectedIndexChanged += new EventHandler(cmbDainoShimebi_SelectedIndexChanged);  //CongBinh 20200331 #134988
        }     

        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void HearerSysInfoInit()
        {
            // システム情報を取得し、初期値をセットする
            M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
            if (sysInfo != null)
            {
                this.sysInfoEntity = sysInfo[0];
            }
        }

        #endregion

        #region Functionボタン 押下処理

        /// <summary>
        /// F1 簡易検索／汎用検索
        /// </summary>      
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// F2 新規
        /// </summary>      
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            if (disp_Flg != 0)
            {
                EditDetail(WINDOW_TYPE.NEW_WINDOW_FLAG, "", DENPYOU_TYPE_UKEIRE); //PhuocLoc 2021/05/05 #148576
            }
        }

        /// <summary>
        /// F3 修正
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;
            if (datagridviewcell != null)
            {
                string DenpyouNum = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_DENPYOU_NUMBER].Value.ToString();
                //PhuocLoc 2021/05/05 #148576 -Start
                int denshuKbn = disp_Flg;
                if (disp_Flg == DENPYOU_TYPE_SUBETE)
                {
                    denshuKbn = int.Parse(this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_DENSHU_KBN_CD].Value.ToString());
                }
                this.EditDetail(WINDOW_TYPE.UPDATE_WINDOW_FLAG, DenpyouNum, denshuKbn);
                //PhuocLoc 2021/05/05 #148576 -End
            }
            else
            {
                //アラートを表示し、画面遷移しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E051", "対象データ");
            }
        }

        /// <summary>
        /// F4 削除
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;
            if (datagridviewcell != null)
            {
                string DenpyouNum = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_DENPYOU_NUMBER].Value.ToString();
                //PhuocLoc 2021/05/05 #148576 -Start
                int denshuKbn = disp_Flg;
                if (disp_Flg == DENPYOU_TYPE_SUBETE)
                {
                    denshuKbn = int.Parse(this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_DENSHU_KBN_CD].Value.ToString());
                }
                EditDetail(WINDOW_TYPE.DELETE_WINDOW_FLAG, DenpyouNum, denshuKbn);
                //PhuocLoc 2021/05/05 #148576 -End
            }
            else
            {
                //アラートを表示し、画面遷移しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E051", "対象データ");
            }
        }

        /// <summary>
        /// F5 複写
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func5_Click(object sender, EventArgs e)
        {
            DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;
            if (datagridviewcell != null)
            {
                string DenpyouNum = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_DENPYOU_NUMBER].Value.ToString();
                //PhuocLoc 2021/05/05 #148576 -Start
                int denshuKbn = disp_Flg;
                if (disp_Flg == DENPYOU_TYPE_SUBETE)
                {
                    denshuKbn = int.Parse(this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_DENSHU_KBN_CD].Value.ToString());
                }
                EditDetail(WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DenpyouNum, denshuKbn);
                //PhuocLoc 2021/05/05 #148576 -End
            }
            else
            {
                //アラートを表示し、画面遷移しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E051", "対象データ");
            }
        }

        /// <summary>
        /// F6 CSV出力
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            // No.1537
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                // 一覧にデータ行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    // アラートを表示し、CSV出力処理はしない
                    msgLogic.MessageBoxShow("E044");
                }
                else
                {
                    if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                    {
                        CSVExport exp = new CSVExport();
                        exp.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, DENSHU_KBNExt.ToTitleString(this.form.DenshuKbn), this.form);
                    }
                }
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            this.form.searchString.Clear();

            // 一覧クリア
            DataTable cre = (DataTable)this.form.customDataGridView1.DataSource;
            if (cre == null)
            {
                return;
            }
            cre.Clear();
            this.form.customDataGridView1.DataSource = cre;

            // ヘッダ部クリア
            if (!this.SetDenpyouHidukeInit())
            {
                return;
            }
            this.headForm.ReadDataNumber.Text = "0";
            this.headForm.alertNumber.Clear();
            this.form.customSortHeader1.ClearCustomSortSetting();
            this.form.customSearchHeader1.ClearCustomSearchSetting();
            if (this.control.Visible == true)
            {
                this.control.TORIHIKISAKI_CD.Clear();
                this.control.TORIHIKISAKI_NAME_RYAKU.Clear();
                this.control.GYOUSYA_CD.Clear();
                this.control.GYOUSYA_NAME_RYAKU.Clear();
                this.control.GENNBA_CD.Clear();
                this.control.GENNBA_NAME_RYAKU.Clear();
                this.control.UNNBANGYOUSYA_CD.Clear();
                this.control.UNNBANGYOUSYA_NAME_RYAKU.Clear();
                this.control.NIOROSHIGYOUSYA_CD.Clear();
                this.control.NIOROSHIGYOUSYA_NAME_RYAKU.Clear();
                this.control.NIOROSHIJYOUMEI_CD.Clear();
                this.control.NIOROSHIJYOUMEI_NAME_RYAKU.Clear();
                this.control.NIDUMIGYOUSYA_CD.Clear();
                this.control.NIDUMIGYOUSYA_NAME_RYAKU.Clear();
                this.control.NIDUMIGENBA_CD.Clear();
                this.control.NIDUMIGENBA_NAME_RYAKU.Clear();
                //CongBinh 20200519 #137148 S
                this.control.cmbShimebi.SelectedIndex = 0;
                this.control.cmbShihariaShimebi.SelectedIndex = 0;
                this.control.cmbDainoShimebi.SelectedIndex = 0;
                //CongBinh 20200519 #137148 E
            }
        }

        /// <summary>
        /// F8検索
        /// </summary>                  
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            if (this.form.PatternNo == 0)
            {
                var msgLogic = new r_framework.Logic.MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }

            bool searchErrorFlag = false;
            this.headForm.HIDUKE_FROM.IsInputErrorOccured = false;
            this.headForm.HIDUKE_TO.IsInputErrorOccured = false;
            var allControlAndHeaderControls = allControl.ToList();
            allControlAndHeaderControls.AddRange(this.form.controlUtil.GetAllControls(this.headForm));
            var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());
            this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

            if (!this.form.RegistErrorFlag)
            {
                if (!string.IsNullOrEmpty(this.headForm.HIDUKE_FROM.GetResultText())
                    && !string.IsNullOrEmpty(this.headForm.HIDUKE_TO.GetResultText()))
                {
                    DateTime dtpFrom = DateTime.Parse(this.headForm.HIDUKE_FROM.GetResultText());
                    DateTime dtpTo = DateTime.Parse(this.headForm.HIDUKE_TO.GetResultText());
                    DateTime dtpFromWithoutTime = DateTime.Parse(dtpFrom.ToShortDateString());
                    DateTime dtpToWithoutTime = DateTime.Parse(dtpTo.ToShortDateString());

                    int diff = dtpFromWithoutTime.CompareTo(dtpToWithoutTime);

                    if (0 < diff)
                    {
                        //対象期間内でないならエラーメッセージ表示
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        this.headForm.HIDUKE_FROM.IsInputErrorOccured = true;
                        this.headForm.HIDUKE_TO.IsInputErrorOccured = true;
                        MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                        if (this.headForm.radbtnDenpyouHiduke.Checked)
                        {
                            string[] errorMsg = { "伝票日付From", "伝票日付To" };
                            msglogic.MessageBoxShow("E030", errorMsg);
                        }
                        else if (this.headForm.radbtnNyuuryokuHiduke.Checked)
                        {
                            string[] errorMsg = { "入力日付From", "入力日付To" };
                            msglogic.MessageBoxShow("E030", errorMsg);
                        }
                        else
                        {
                            string[] errorMsg = { "検収伝票日付From", "検収伝票日付To" };
                            msglogic.MessageBoxShow("E030", errorMsg);
                        }
                        this.headForm.HIDUKE_FROM.Select();
                        this.headForm.HIDUKE_FROM.Focus();
                        searchErrorFlag = true;
                    }
                }
            }

            // Ditailの行数チェックはFWでできないので自前でチェック
            if (!this.form.RegistErrorFlag && !searchErrorFlag)
            {
                //20150422 Jyokou 4935_4 STR
                if (this.form.radbtn_Ukeire.Checked || this.form.radbtn_Shuka.Checked || this.form.radbtn_Uriageshiharai.Checked
                    || this.form.radbtn_Dainou.Checked || this.form.radbtn_All.Checked)  //PhuocLoc 2021/05/05 #148576
                //20150422 Jyokou 4935_4 END
                {
                    //読込データ件数を取得
                    int count = this.Search();
                    if (count == -1)
                    {
                        return;
                    }
                    this.headForm.ReadDataNumber.Text = count.ToString();
                    //thongh 2015/09/14 #13032 start
                    if (this.form.customDataGridView1 != null)
                    {
                        this.headForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                    }
                    else
                    {
                        this.headForm.ReadDataNumber.Text = "0";
                    }
                    //thongh 2015/09/14 #13032 end
                    if (this.headForm.ReadDataNumber.Text == "0")
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("C001");
                    }
                }
            }
            //必須チェックエラーフォーカス処理
            this.SetErrorFocus();
        }

        /// <summary>
        /// F10 並び替え
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            this.form.customSortHeader1.ShowCustomSortSettingDialog();
        }

        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            this.form.customSearchHeader1.ShowCustomSearchSettingDialog();
            //読込データ件数           #13032
            if (this.form.customDataGridView1 != null)
            {
                this.headForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.headForm.ReadDataNumber.Text = "0";
            }
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.Close();
        }

        #endregion

        #region プロセスボタン押下処理（※処理未実装）
        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            var sysID = this.form.OpenPatternIchiran();

            if (!string.IsNullOrEmpty(sysID))
            {
                this.form.SetPatternBySysId(sysID);
                this.searchResult = this.form.Table;
                this.form.ShowData();
            }
            this.form.baseSelectQuery = this.form.SelectQuery;
            this.form.baseOrderByQuery = this.form.OrderByQuery;
            this.form.baseJoinQuery = this.form.JoinQuery;
        }

        /// <summary>
        /// 検索条件設定画面へ遷移
        /// </summary>
        private void bt_process2_Click(object sender, System.EventArgs e)
        {
        }
        #endregion

        #region ESCキー押下イベント
        void form_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            if (e.KeyCode == Keys.Escape)
            {
                //処理No(ESC)へカーソル移動
                parentForm.txb_process.Focus();
            }
        }
        #endregion

        #region 伝票種類、伝票区分初期値設定
        /// <summary>
        /// 画面の伝票種類、伝票区分を初期化する
        /// </summary>
        private void SetInitialDenpyoShurui()
        {
            //画面の伝票種類、伝票区分初期値設定
            switch (this.denShu_Kbn)
            {
                case DENSHU_KBN.UKEIRE:
                    this.form.txtNum_DenpyoKind.Text = DENPYOU_TYPE_UKEIRE.ToString();
                    this.form.txtNum_Denpyoukubun.Text = DENPYOU_KBN_ALL.ToString();
                    break;
                case DENSHU_KBN.SHUKKA:
                    this.form.txtNum_DenpyoKind.Text = DENPYOU_TYPE_SHUKKA.ToString();
                    this.form.txtNum_Denpyoukubun.Text = DENPYOU_KBN_ALL.ToString();
                    break;
                case DENSHU_KBN.URIAGE_SHIHARAI:
                    this.form.txtNum_DenpyoKind.Text = DENPYOU_TYPE_URIAGESHIHARAI.ToString();
                    this.form.txtNum_Denpyoukubun.Text = DENPYOU_KBN_ALL.ToString();
                    break;
                case DENSHU_KBN.DAINOU:
                    this.form.txtNum_DenpyoKind.Text = DENPYOU_TYPE_DAINOU.ToString();
                    this.form.txtNum_Denpyoukubun.Text = DENPYOU_KBN_ALL.ToString();
                    break;
                default:
                    //PhuocLoc 2021/05/05 #148576 -Start
                    this.form.txtNum_DenpyoKind.Text = DENPYOU_TYPE_SUBETE.ToString();
                    if (string.IsNullOrEmpty(Properties.Settings.Default.SET_DENPYO_KIND_CD))
                    {
                        this.form.txtNum_DenpyoKind.Text = DENPYOU_TYPE_UKEIRE.ToString();
                    }
                    else
                    {
                        this.form.txtNum_DenpyoKind.Text = Properties.Settings.Default.SET_DENPYO_KIND_CD;
                    }
                    //PhuocLoc 2021/05/05 #148576 -End

                    if (string.IsNullOrEmpty(Properties.Settings.Default.SET_DENPYO_KBN_CD))
                    {
                        this.form.txtNum_Denpyoukubun.Text = DENPYOU_KBN_ALL.ToString();
                    }
                    else
                    {
                        this.form.txtNum_Denpyoukubun.Text = Properties.Settings.Default.SET_DENPYO_KBN_CD;
                    }
                    break;
            }
            if (String.IsNullOrEmpty(this.form.txtNum_DenpyoKind.Text))
            {
                disp_Flg = 0;
            }
            else
            {
                disp_Flg = int.Parse(this.form.txtNum_DenpyoKind.Text);
            }
        }

        #endregion

        #region 明細データダブルクリックイベント
        private void customDataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DataGridViewCellMouseEventArgs datagridviewcell = (DataGridViewCellMouseEventArgs)e;
            if (datagridviewcell.RowIndex >= 0)
            {
                string denpyouNum = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_DENPYOU_NUMBER].Value.ToString();
                //PhuocLoc 2021/05/05 #148576 -Start
                int denshuKbn = disp_Flg;
                if (disp_Flg == DENPYOU_TYPE_SUBETE)
                {
                    denshuKbn = int.Parse(this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_DENSHU_KBN_CD].Value.ToString());
                }
                this.EditDetail(WINDOW_TYPE.UPDATE_WINDOW_FLAG, denpyouNum, denshuKbn);
                //PhuocLoc 2021/05/05 #148576 -End
            }
        }

        #endregion

        #region 検索売上確定入力一覧
        /// <summary>
        /// Where　条件
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeWhereSql(StringBuilder sql)
        {
            string tblName1 = string.Empty;
            string tblName2 = string.Empty;
            string tblName3 = string.Empty;
            string tblName4 = string.Empty;

            switch (disp_Flg)
            {
                case DENPYOU_TYPE_UKEIRE:
                    tblName1 = "T_UKEIRE_ENTRY";
                    tblName2 = "T_UKEIRE_DETAIL";
                    break;
                case DENPYOU_TYPE_SHUKKA:
                    tblName1 = "T_SHUKKA_ENTRY";
                    tblName2 = "T_SHUKKA_DETAIL";
                    break;
                case DENPYOU_TYPE_URIAGESHIHARAI:
                    tblName1 = "T_UR_SH_ENTRY";
                    tblName2 = "T_UR_SH_DETAIL";
                    break;
                case DENPYOU_TYPE_DAINOU:
                    tblName1 = "T_UR_SH_ENTRY";
                    tblName2 = "T_UR_SH_DETAIL";
                    tblName3 = "T_UR_SH_ENTRY1";
                    tblName4 = "T_UR_SH_DETAIL1";
                    break;
                //PhuocLoc 2021/05/05 #148576 -Start
                case DENPYOU_TYPE_SUBETE:
                    tblName1 = "SUMMARY_ENTRY";
                    tblName2 = "SUMMARY_DETAIL";
                    break;
                //PhuocLoc 2021/05/05 #148576 -End
            }
            sql.Append(" WHERE ");
            // 滞留登録されている伝票は受入一覧・出荷一覧・計量一覧に表示しない
            if (this.form.radbtn_Ukeire.Checked || this.form.radbtn_Shuka.Checked)
            {
                sql.AppendFormat(" {0}.TAIRYUU_KBN = 0 ", tblName1);
                sql.Append(" AND ");
            }
            sql.AppendFormat(" {0}.DELETE_FLG = 0 ", tblName1);
            //CongBinh 20200331 #134988 S
            //CongBinh 20200513 #136890 S
            if (!this.form.radbtn_Dainou.Checked)
            {
                if (!string.IsNullOrEmpty(this.control.cmbShimebi.Text))
                {
                    sql.Append("AND (M_TORIHIKISAKI_SEIKYUU.SHIMEBI1 = '" + this.control.cmbShimebi.Text + "' ");
                    sql.Append("OR M_TORIHIKISAKI_SEIKYUU.SHIMEBI2 = '" + this.control.cmbShimebi.Text + "' ");
                    sql.Append("OR M_TORIHIKISAKI_SEIKYUU.SHIMEBI3 = '" + this.control.cmbShimebi.Text + "') ");
                }
            }
            //CongBinh 20200513 #136890 E
            if (!string.IsNullOrEmpty(this.control.cmbShihariaShimebi.Text))
            {
                sql.Append("AND ( M_TORIHIKISAKI_SHIHARAI.SHIMEBI1 = '" + this.control.cmbShihariaShimebi.Text + "' ");
                sql.Append("OR M_TORIHIKISAKI_SHIHARAI.SHIMEBI2 = '" + this.control.cmbShihariaShimebi.Text + "' ");
                sql.Append("OR M_TORIHIKISAKI_SHIHARAI.SHIMEBI3 = '" + this.control.cmbShihariaShimebi.Text + "') ");
            }
            //CongBinh 20200331 #134988 E
            //PhuocLoc 2021/05/05 #148576 -Start
            if (!this.form.radbtn_DenshuSubete.Checked)
            {
                sql.AppendFormat(" AND {0}.SEQ = (SELECT MAX(TMP.SEQ) FROM {0} TMP WHERE TMP.SYSTEM_ID = {0}.SYSTEM_ID) ", tblName1);
            }
            //PhuocLoc 2021/05/05 #148576 -End
            if (this.headForm.radbtnDenpyouHiduke.Checked)
            {
                if (this.headForm.HIDUKE_FROM.Value != null || this.headForm.HIDUKE_TO.Value != null)
                {
                    sql.Append(" AND ");
                }
                // 日時は日付のみにしてから変換
                if (this.headForm.HIDUKE_FROM.Value != null)
                {
                    sql.AppendFormat(" {0}.DENPYOU_DATE >= CONVERT(DATETIME, CONVERT(nvarchar, '", tblName1);
                    sql.Append(DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString() + "', 111), 120) ");
                }
                if (this.headForm.HIDUKE_FROM.Value != null && this.headForm.HIDUKE_TO.Value != null)
                {
                    sql.Append(" AND ");
                }
                if (this.headForm.HIDUKE_TO.Value != null)
                {
                    sql.AppendFormat(" {0}.DENPYOU_DATE <= CONVERT(DATETIME, CONVERT(nvarchar, '", tblName1);
                    sql.Append(DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString() + "', 111), 120) ");
                }
            }
            else if (this.headForm.radbtnNyuuryokuHiduke.Checked)
            {
                if (this.headForm.HIDUKE_FROM.Value != null || this.headForm.HIDUKE_TO.Value != null)
                {
                    sql.Append(" AND ");
                }
                if (this.headForm.HIDUKE_FROM.Value != null)
                {
                    sql.AppendFormat(" CONVERT(DATETIME, CONVERT(nvarchar, {0}.UPDATE_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(nvarchar, '", tblName1);
                    sql.Append(DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString() + "', 111), 120) ");
                }
                if (this.headForm.HIDUKE_FROM.Value != null && this.headForm.HIDUKE_TO.Value != null)
                {
                    sql.Append(" AND ");
                }
                if (this.headForm.HIDUKE_TO.Value != null)
                {
                    sql.AppendFormat(" CONVERT(DATETIME, CONVERT(nvarchar, {0}.UPDATE_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(nvarchar, '", tblName1);
                    sql.Append(DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString() + "', 111), 120) ");
                }
            }
            else if (this.headForm.radbtnKenshuHiduke.Checked && this.form.radbtn_KenshuZumi.Checked)
            {
                if (this.headForm.HIDUKE_FROM.Value != null || this.headForm.HIDUKE_TO.Value != null)
                {
                    sql.Append(" AND ");
                }
                if (this.headForm.HIDUKE_FROM.Value != null)
                {
                    sql.AppendFormat(" {0}.KENSHU_DATE >= CONVERT(DATETIME, CONVERT(nvarchar, '", tblName1);
                    sql.Append(DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString() + "', 111), 120) ");
                }
                if (this.headForm.HIDUKE_FROM.Value != null && this.headForm.HIDUKE_TO.Value != null)
                {
                    sql.Append(" AND ");
                }
                if (this.headForm.HIDUKE_TO.Value != null)
                {
                    sql.AppendFormat(" {0}.KENSHU_DATE <= CONVERT(DATETIME, CONVERT(nvarchar, '", tblName1);
                    sql.Append(DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString() + "', 111), 120) ");
                }
            }
            if (!String.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text) && this.headForm.KYOTEN_CD.Text != "99")
            {
                sql.AppendFormat(" AND {0}.KYOTEN_CD = '{1}' ", tblName1, this.headForm.KYOTEN_CD.Text);
            }
            // 売上・支払
            if (this.form.radbtn_Uriageshiharai.Checked)
            {
                sql.AppendFormat(" AND ( {0}.DAINOU_FLG IS NULL OR {0}.DAINOU_FLG = 0 ) ", tblName1);
            }
            #region　代納用
            if (this.form.radbtn_Dainou.Checked)
            {
                if (control.Visible == true)
                {
                    if (!String.IsNullOrEmpty(this.control.TORIHIKISAKI_CD.Text))
                    {
                        sql.AppendFormat(" AND {0}.TORIHIKISAKI_CD = '{1}' ", tblName1, this.control.TORIHIKISAKI_CD.Text);
                    }
                    if (!String.IsNullOrEmpty(this.control.GYOUSYA_CD.Text))
                    {
                        sql.AppendFormat(" AND {0}.GYOUSHA_CD = '{1}' ", tblName1, this.control.GYOUSYA_CD.Text);
                    }
                    if (!String.IsNullOrEmpty(this.control.GENNBA_CD.Text))
                    {
                        sql.AppendFormat(" AND {0}.GENBA_CD = '{1}' ", tblName1, this.control.GENNBA_CD.Text);
                    }
                    if (!String.IsNullOrEmpty(this.control.UNNBANGYOUSYA_CD.Text))
                    {
                        sql.AppendFormat(" AND {0}.UNPAN_GYOUSHA_CD = '{1}' ", tblName1, this.control.UNNBANGYOUSYA_CD.Text);
                    }
                    if (!String.IsNullOrEmpty(this.control.SHUKKA_TORIHIKISAKI_CD.Text))
                    {
                        sql.AppendFormat(" AND {0}.TORIHIKISAKI_CD = '{1}' ", tblName3, this.control.SHUKKA_TORIHIKISAKI_CD.Text);
                    }
                    if (!String.IsNullOrEmpty(this.control.SHUKKA_GYOUSYA_CD.Text))
                    {
                        sql.AppendFormat(" AND {0}.GYOUSHA_CD = '{1}' ", tblName3, this.control.SHUKKA_GYOUSYA_CD.Text);
                    }
                    if (!String.IsNullOrEmpty(this.control.SHUKKA_GENNBA_CD.Text))
                    {
                        sql.AppendFormat(" AND {0}.GENBA_CD = '{1}' ", tblName3, this.control.SHUKKA_GENNBA_CD.Text);
                    }
                    //CongBinh 20200331 #134988 S
                    if (!string.IsNullOrEmpty(this.control.cmbDainoShimebi.Text))
                    {
                        sql.Append("AND (M_TORIHIKISAKI_SEIKYUU.SHIMEBI1 = '" + this.control.cmbDainoShimebi.Text + "' ");
                        sql.Append("OR M_TORIHIKISAKI_SEIKYUU.SHIMEBI2 = '" + this.control.cmbDainoShimebi.Text + "' ");
                        sql.Append("OR M_TORIHIKISAKI_SEIKYUU.SHIMEBI3 = '" + this.control.cmbDainoShimebi.Text + "') ");
                    }
                    //CongBinh 20200331 #134988 E                
                }
                return;
            }
            #endregion

            if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN != 3)
            {
                if (this.form.radbtn_Uriage.Checked)
                {
                    sql.AppendFormat(" AND {0}.DENPYOU_KBN_CD = 1 ", tblName2);
                }
                else if (this.form.radbtn_Shihari.Checked)
                {
                    sql.AppendFormat(" AND {0}.DENPYOU_KBN_CD = 2 ", tblName2);
                }
            }
            if (control.Visible == true)
            {
                if ((!String.IsNullOrEmpty(this.control.TORIHIKISAKI_CD.Text)))
                {
                    sql.AppendFormat(" AND {0}.TORIHIKISAKI_CD = '{1}' ", tblName1, this.control.TORIHIKISAKI_CD.Text);
                }
                if ((!String.IsNullOrEmpty(this.control.GYOUSYA_CD.Text)))
                {
                    sql.AppendFormat(" AND {0}.GYOUSHA_CD = '{1}' ", tblName1, this.control.GYOUSYA_CD.Text);
                }
                if ((!String.IsNullOrEmpty(this.control.GENNBA_CD.Text)))
                {
                    sql.AppendFormat(" AND {0}.GENBA_CD = '{1}' ", tblName1, this.control.GENNBA_CD.Text);
                }
                if ((!String.IsNullOrEmpty(this.control.UNNBANGYOUSYA_CD.Text)))
                {
                    sql.AppendFormat(" AND {0}.UNPAN_GYOUSHA_CD = '{1}' ", tblName1, this.control.UNNBANGYOUSYA_CD.Text);
                }
                if (!this.form.radbtn_Dainou.Checked)
                {
                    if (!this.form.radbtn_Shuka.Checked)
                    {
                        if ((!String.IsNullOrEmpty(this.control.NIOROSHIGYOUSYA_CD.Text)))
                        {
                            sql.AppendFormat(" AND {0}.NIOROSHI_GYOUSHA_CD = '{1}' ", tblName1, this.control.NIOROSHIGYOUSYA_CD.Text);
                        }
                        if ((!String.IsNullOrEmpty(this.control.NIOROSHIJYOUMEI_CD.Text)))
                        {
                            sql.AppendFormat(" AND {0}.NIOROSHI_GENBA_CD = '{1}' ", tblName1, this.control.NIOROSHIJYOUMEI_CD.Text);
                        }
                    }
                    if (!this.form.radbtn_Ukeire.Checked)
                    {
                        if ((!String.IsNullOrEmpty(this.control.NIDUMIGYOUSYA_CD.Text)))
                        {
                            sql.AppendFormat(" AND {0}.NIZUMI_GYOUSHA_CD = '{1}' ", tblName1, this.control.NIDUMIGYOUSYA_CD.Text);
                        }
                        if ((!String.IsNullOrEmpty(this.control.NIDUMIGENBA_CD.Text)))
                        {
                            sql.AppendFormat(" AND {0}.NIZUMI_GENBA_CD = '{1}' ", tblName1, this.control.NIDUMIGENBA_CD.Text);
                        }
                    }
                }
            }
            // 検収系
            if (disp_Flg == DENPYOU_TYPE_SHUKKA)
            {
                if (this.form.txtNum_KenshuMustKbn.Visible
                    && !string.IsNullOrEmpty(this.form.txtNum_KenshuMustKbn.Text))
                {
                    if (KENSHU_MUST_KBN_TRUE.ToString().Equals(this.form.txtNum_KenshuMustKbn.Text))
                    {
                        sql.AppendFormat(" AND {0}.KENSHU_MUST_KBN = 1 ", tblName1);
                    }
                    else if (KENSHU_MUST_KBN_FALSE.ToString().Equals(this.form.txtNum_KenshuMustKbn.Text))
                    {
                        sql.AppendFormat(" AND ( {0}.KENSHU_MUST_KBN != 1 OR {0}.KENSHU_MUST_KBN IS NULL )", tblName1);
                    }
                }
                if (this.form.txtNum_KenshuJyoukyou.Visible
                    && !string.IsNullOrEmpty(this.form.txtNum_KenshuJyoukyou.Text))
                {
                    if (KENSHU_JYOUKYOU_MIKENSHU.ToString().Equals(this.form.txtNum_KenshuJyoukyou.Text))
                    {
                        sql.AppendFormat(" AND {0}.KENSHU_DATE IS NULL ", tblName1);
                    }
                    else if (KENSHU_JYOUKYOU_KENSHUZUMI.ToString().Equals(this.form.txtNum_KenshuJyoukyou.Text))
                    {
                        sql.AppendFormat(" AND {0}.KENSHU_DATE IS NOT NULL ", tblName1);
                    }
                }
            }
        }

        /// <summary>
        /// 受入検索
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSearchJuchu(StringBuilder sql)
        {
            sql.Append(" T_UKEIRE_ENTRY ");
            if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == 3)
            {
                sql.Append(" LEFT JOIN T_UKEIRE_JISSEKI_ENTRY ");
                sql.Append(" ON T_UKEIRE_ENTRY.SYSTEM_ID = T_UKEIRE_JISSEKI_ENTRY.DENPYOU_SYSTEM_ID AND T_UKEIRE_JISSEKI_ENTRY.DENPYOU_SHURUI = 2 AND T_UKEIRE_JISSEKI_ENTRY.DELETE_FLG = 0 ");
                sql.Append(" LEFT JOIN T_UKEIRE_JISSEKI_DETAIL ");
                sql.Append(" ON T_UKEIRE_JISSEKI_DETAIL.DENPYOU_SHURUI = T_UKEIRE_JISSEKI_ENTRY.DENPYOU_SHURUI AND T_UKEIRE_JISSEKI_DETAIL.DENPYOU_SYSTEM_ID = T_UKEIRE_JISSEKI_ENTRY.DENPYOU_SYSTEM_ID AND T_UKEIRE_JISSEKI_DETAIL.SEQ = T_UKEIRE_JISSEKI_ENTRY.SEQ ");
                sql.Append(" LEFT JOIN M_FILE_LINK_UKEIRE_JISSEKI_ENTRY ");
                sql.Append(" ON T_UKEIRE_JISSEKI_ENTRY.DENPYOU_SHURUI = M_FILE_LINK_UKEIRE_JISSEKI_ENTRY.DENPYOU_SHURUI AND T_UKEIRE_JISSEKI_ENTRY.DENPYOU_SYSTEM_ID = M_FILE_LINK_UKEIRE_JISSEKI_ENTRY.DENPYOU_SYSTEM_ID AND T_UKEIRE_JISSEKI_ENTRY.DELETE_FLG = 0 ");
            }
            else
            {
                sql.Append(" LEFT JOIN T_UKEIRE_DETAIL ");
                sql.Append(" ON T_UKEIRE_ENTRY.SYSTEM_ID = T_UKEIRE_DETAIL.SYSTEM_ID AND T_UKEIRE_ENTRY.SEQ = T_UKEIRE_DETAIL.SEQ ");
            }
        }
        /// <summary>
        /// 出荷検索
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSearchShuka(StringBuilder sql)
        {
            sql.Append(" T_SHUKKA_ENTRY ");
            // パターンの区分が明細の場合、明細テーブルを結合する
            sql.Append(" LEFT JOIN T_SHUKKA_DETAIL ");
            sql.Append(" ON T_SHUKKA_ENTRY.SYSTEM_ID = T_SHUKKA_DETAIL.SYSTEM_ID AND T_SHUKKA_ENTRY.SEQ = T_SHUKKA_DETAIL.SEQ ");
            sql.Append(" LEFT JOIN T_KENSHU_DETAIL ");
            sql.Append(" ON T_KENSHU_DETAIL.SYSTEM_ID = T_SHUKKA_DETAIL.SYSTEM_ID AND T_KENSHU_DETAIL.SEQ = T_SHUKKA_DETAIL.SEQ AND T_KENSHU_DETAIL.DETAIL_SYSTEM_ID = T_SHUKKA_DETAIL.DETAIL_SYSTEM_ID ");
        }
        /// <summary>
        /// 売上_支払検索
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSearchUriageShiharai(StringBuilder sql)
        {
            sql.Append(" T_UR_SH_ENTRY ");
            // パターンの区分が明細の場合、明細テーブルを結合する
            sql.Append(" LEFT JOIN T_UR_SH_DETAIL ");
            sql.Append(" ON T_UR_SH_ENTRY.SYSTEM_ID = T_UR_SH_DETAIL.SYSTEM_ID AND T_UR_SH_ENTRY.SEQ = T_UR_SH_DETAIL.SEQ ");
        }
        /// <summary>
        /// 代納検索
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSearchDainou(StringBuilder sql)
        {
            sql.Append(" ( ");
            sql.Append(" SELECT T1.SYSTEM_ID AS UKEIRE_SYSTEM_ID, T1.SEQ AS UKEIRE_SEQ, T1.DETAIL_SYSTEM_ID AS UKEIRE_DETAIL_SYSTEM_ID ");
            sql.Append("       ,T2.SYSTEM_ID AS SHUKKA_SYSTEM_ID, T2.SEQ AS SHUKKA_SEQ, T2.DETAIL_SYSTEM_ID AS SHUKKA_DETAIL_SYSTEM_ID ");
            sql.Append("   FROM (SELECT H1.UR_SH_NUMBER, D1.SYSTEM_ID, D1.SEQ, D1.DETAIL_SYSTEM_ID, D1.ROW_NO FROM T_UR_SH_ENTRY H1 ");
            sql.Append("            INNER JOIN T_UR_SH_DETAIL D1 ON D1.SYSTEM_ID = H1.SYSTEM_ID AND D1.SEQ = H1.SEQ AND D1.DENPYOU_KBN_CD = 2 ");
            sql.Append("            WHERE H1.DELETE_FLG = 0 AND H1.DAINOU_FLG = 1) T1 ");
            sql.Append("  INNER JOIN ");
            sql.Append("        (SELECT H1.UR_SH_NUMBER, D1.SYSTEM_ID, D1.SEQ, D1.DETAIL_SYSTEM_ID, D1.ROW_NO FROM T_UR_SH_ENTRY H1 ");
            sql.Append("            INNER JOIN T_UR_SH_DETAIL D1 ON D1.SYSTEM_ID = H1.SYSTEM_ID AND D1.SEQ = H1.SEQ AND D1.DENPYOU_KBN_CD = 1 ");
            sql.Append("            WHERE H1.DELETE_FLG = 0 AND H1.DAINOU_FLG = 1) T2 ");
            sql.Append("     ON T2.UR_SH_NUMBER = T1.UR_SH_NUMBER ");
            sql.Append("    AND T2.ROW_NO = T1.ROW_NO ");
            sql.Append(" ) UKEIRE_SHUKKA_ENTRY ");
            // 代納_受入
            sql.Append(" INNER JOIN T_UR_SH_ENTRY ");
            sql.Append("    ON T_UR_SH_ENTRY.SYSTEM_ID = UKEIRE_SHUKKA_ENTRY.UKEIRE_SYSTEM_ID AND T_UR_SH_ENTRY.SEQ = UKEIRE_SHUKKA_ENTRY.UKEIRE_SEQ ");
            sql.Append(" INNER JOIN T_UR_SH_DETAIL ");
            sql.Append("    ON T_UR_SH_DETAIL.SYSTEM_ID = UKEIRE_SHUKKA_ENTRY.UKEIRE_SYSTEM_ID AND T_UR_SH_DETAIL.SEQ = UKEIRE_SHUKKA_ENTRY.UKEIRE_SEQ ");
            sql.Append("   AND T_UR_SH_DETAIL.DETAIL_SYSTEM_ID = UKEIRE_SHUKKA_ENTRY.UKEIRE_DETAIL_SYSTEM_ID ");
            // 代納_出荷
            sql.Append(" INNER JOIN T_UR_SH_ENTRY T_UR_SH_ENTRY1 ");
            sql.Append("    ON T_UR_SH_ENTRY1.SYSTEM_ID = UKEIRE_SHUKKA_ENTRY.SHUKKA_SYSTEM_ID AND T_UR_SH_ENTRY1.SEQ = UKEIRE_SHUKKA_ENTRY.SHUKKA_SEQ ");
            sql.Append(" INNER JOIN T_UR_SH_DETAIL T_UR_SH_DETAIL1 ");
            sql.Append("    ON T_UR_SH_DETAIL1.SYSTEM_ID = UKEIRE_SHUKKA_ENTRY.SHUKKA_SYSTEM_ID AND T_UR_SH_DETAIL1.SEQ = UKEIRE_SHUKKA_ENTRY.SHUKKA_SEQ ");
            sql.Append("   AND T_UR_SH_DETAIL1.DETAIL_SYSTEM_ID = UKEIRE_SHUKKA_ENTRY.SHUKKA_DETAIL_SYSTEM_ID ");
        }

        //PhuocLoc 2021/05/05 #148576 -Start
        /// <summary>
        /// Subete検索
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSearchSubete(StringBuilder sql)
        {
            sql.Append(" ( ");
            if (String.IsNullOrEmpty(this.control.NIDUMIGYOUSYA_CD.Text))
            {
                sql.Append(" SELECT  ");
                sql.Append(" SYSTEM_ID ");
                sql.Append(" ,SEQ ");
                sql.Append(" ,1 AS DENSHU_KBN_CD ");
                sql.Append(" ,'受入' AS DENSHU_KBN_NAME ");
                sql.Append(" ,TAIRYUU_KBN ");
                sql.Append(" ,KYOTEN_CD ");
                sql.Append(" ,UKEIRE_NUMBER	AS DENPYOU_NUMBER ");
                sql.Append(" ,DATE_NUMBER ");
                sql.Append(" ,YEAR_NUMBER ");
                sql.Append(" ,KAKUTEI_KBN ");
                sql.Append(" ,DENPYOU_DATE ");
                sql.Append(" ,URIAGE_DATE ");
                sql.Append(" ,SHIHARAI_DATE ");
                sql.Append(" ,TORIHIKISAKI_CD ");
                sql.Append(" ,TORIHIKISAKI_NAME ");
                sql.Append(" ,GYOUSHA_CD ");
                sql.Append(" ,GYOUSHA_NAME ");
                sql.Append(" ,GENBA_CD ");
                sql.Append(" ,GENBA_NAME ");
                sql.Append(" ,NIOROSHI_GYOUSHA_CD ");
                sql.Append(" ,NIOROSHI_GYOUSHA_NAME ");
                sql.Append(" ,NIOROSHI_GENBA_CD ");
                sql.Append(" ,NIOROSHI_GENBA_NAME ");
                sql.Append(" ,NULL AS NIZUMI_GYOUSHA_CD ");
                sql.Append(" ,NULL AS NIZUMI_GYOUSHA_NAME ");
                sql.Append(" ,NULL AS NIZUMI_GENBA_CD ");
                sql.Append(" ,NULL AS NIZUMI_GENBA_NAME ");
                sql.Append(" ,EIGYOU_TANTOUSHA_CD ");
                sql.Append(" ,EIGYOU_TANTOUSHA_NAME ");
                sql.Append(" ,NYUURYOKU_TANTOUSHA_CD ");
                sql.Append(" ,NYUURYOKU_TANTOUSHA_NAME ");
                sql.Append(" ,SHARYOU_CD ");
                sql.Append(" ,SHARYOU_NAME ");
                sql.Append(" ,SHASHU_CD ");
                sql.Append(" ,SHASHU_NAME ");
                sql.Append(" ,UNPAN_GYOUSHA_CD ");
                sql.Append(" ,UNPAN_GYOUSHA_NAME ");
                sql.Append(" ,UNTENSHA_CD ");
                sql.Append(" ,UNTENSHA_NAME ");
                sql.Append(" ,NULL AS NINZUU_CNT ");
                sql.Append(" ,KEITAI_KBN_CD ");
                sql.Append(" ,DAIKAN_KBN ");
                sql.Append(" ,CONTENA_SOUSA_CD ");
                sql.Append(" ,MANIFEST_SHURUI_CD ");
                sql.Append(" ,MANIFEST_TEHAI_CD ");
                sql.Append(" ,DENPYOU_BIKOU ");
                sql.Append(" ,TAIRYUU_BIKOU ");
                sql.Append(" ,UKETSUKE_NUMBER ");
                sql.Append(" ,KEIRYOU_NUMBER ");
                sql.Append(" ,RECEIPT_NUMBER ");
                sql.Append(" ,RECEIPT_NUMBER_YEAR ");
                sql.Append(" ,NET_TOTAL ");
                sql.Append(" ,URIAGE_SHOUHIZEI_RATE ");
                sql.Append(" ,URIAGE_KINGAKU_TOTAL	 AS URIAGE_KINGAKU_TOTAL ");
                sql.Append(" ,URIAGE_TAX_SOTO ");
                sql.Append(" ,URIAGE_TAX_UCHI ");
                sql.Append(" ,URIAGE_TAX_SOTO_TOTAL ");
                sql.Append(" ,URIAGE_TAX_UCHI_TOTAL ");
                sql.Append(" ,HINMEI_URIAGE_KINGAKU_TOTAL ");
                sql.Append(" ,HINMEI_URIAGE_TAX_SOTO_TOTAL ");
                sql.Append(" ,HINMEI_URIAGE_TAX_UCHI_TOTAL ");
                sql.Append(" ,SHIHARAI_SHOUHIZEI_RATE ");
                sql.Append(" ,SHIHARAI_KINGAKU_TOTAL  AS SHIHARAI_KINGAKU_TOTAL ");
                sql.Append(" ,SHIHARAI_TAX_SOTO ");
                sql.Append(" ,SHIHARAI_TAX_UCHI ");
                sql.Append(" ,SHIHARAI_TAX_SOTO_TOTAL ");
                sql.Append(" ,SHIHARAI_TAX_UCHI_TOTAL ");
                sql.Append(" ,HINMEI_SHIHARAI_KINGAKU_TOTAL ");
                sql.Append(" ,HINMEI_SHIHARAI_TAX_SOTO_TOTAL ");
                sql.Append(" ,HINMEI_SHIHARAI_TAX_UCHI_TOTAL ");
                sql.Append(" ,URIAGE_ZEI_KEISAN_KBN_CD ");
                sql.Append(" ,URIAGE_ZEI_KBN_CD ");
                sql.Append(" ,URIAGE_TORIHIKI_KBN_CD ");
                sql.Append(" ,SHIHARAI_ZEI_KEISAN_KBN_CD ");
                sql.Append(" ,SHIHARAI_ZEI_KBN_CD ");
                sql.Append(" ,SHIHARAI_TORIHIKI_KBN_CD ");
                sql.Append(" ,NULL AS KENSHU_MUST_KBN ");
                sql.Append(" ,NULL AS KENSHU_DATE ");
                sql.Append(" ,NULL AS KENSHU_URIAGE_DATE ");
                sql.Append(" ,NULL AS KENSHU_SHIHARAI_DATE ");
                sql.Append(" ,NULL AS SHUKKA_NET_TOTAL ");
                sql.Append(" ,NULL AS KENSHU_NET_TOTAL ");
                sql.Append(" ,NULL AS SABUN ");
                sql.Append(" ,NULL AS SHUKKA_KINGAKU_TOTAL ");
                sql.Append(" ,NULL AS KENSHU_KINGAKU_TOTAL ");
                sql.Append(" ,NULL AS SAGAKU ");
                sql.Append(" ,NULL AS KENSHU_URIAGE_SHOUHIZEI_RATE ");
                sql.Append(" ,NULL AS KENSHU_URIAGE_AMOUNT_TOTAL ");
                sql.Append(" ,NULL AS KENSHU_URIAGE_TAX_SOTO ");
                sql.Append(" ,NULL AS KENSHU_URIAGE_TAX_UCHI ");
                sql.Append(" ,NULL AS KENSHU_URIAGE_TAX_SOTO_TOTAL ");
                sql.Append(" ,NULL AS KENSHU_URIAGE_TAX_UCHI_TOTAL ");
                sql.Append(" ,NULL AS KENSHU_HINMEI_URIAGE_KINGAKU_TOTAL ");
                sql.Append(" ,NULL AS KENSHU_HINMEI_URIAGE_TAX_SOTO_TOTAL ");
                sql.Append(" ,NULL AS KENSHU_HINMEI_URIAGE_TAX_UCHI_TOTAL ");
                sql.Append(" ,NULL AS KENSHU_SHIHARAI_SHOUHIZEI_RATE ");
                sql.Append(" ,NULL AS KENSHU_SHIHARAI_AMOUNT_TOTAL ");
                sql.Append(" ,NULL AS KENSHU_SHIHARAI_TAX_SOTO ");
                sql.Append(" ,NULL AS KENSHU_SHIHARAI_TAX_UCHI ");
                sql.Append(" ,NULL AS KENSHU_SHIHARAI_TAX_SOTO_TOTAL ");
                sql.Append(" ,NULL AS KENSHU_SHIHARAI_TAX_UCHI_TOTAL ");
                sql.Append(" ,NULL AS KENSHU_HINMEI_SHIHARAI_KINGAKU_TOTAL ");
                sql.Append(" ,NULL AS KENSHU_HINMEI_SHIHARAI_TAX_SOTO_TOTAL ");
                sql.Append(" ,NULL AS KENSHU_HINMEI_SHIHARAI_TAX_UCHI_TOTAL ");
                sql.Append(" ,CREATE_USER ");
                sql.Append(" ,CREATE_DATE ");
                sql.Append(" ,CREATE_PC ");
                sql.Append(" ,UPDATE_USER ");
                sql.Append(" ,UPDATE_DATE ");
                sql.Append(" ,UPDATE_PC ");
                sql.Append(" ,DELETE_FLG ");
                sql.Append(" ,TIME_STAMP ");
                sql.Append(" ,MOD_SHUUKEI_KOUMOKU_CD ");
                sql.Append(" ,MOD_SHUUKEI_KOUMOKU_NAME ");
                sql.Append(" ,STACK_JYUURYOU ");
                sql.Append(" ,STACK_KEIRYOU_TIME ");
                sql.Append(" ,EMPTY_JYUURYOU ");
                sql.Append(" ,EMPTY_KEIRYOU_TIME ");
                sql.Append(" FROM T_UKEIRE_ENTRY ");
                sql.Append(" WHERE DELETE_FLG = 0 AND TAIRYUU_KBN = 0 ");
                if ((!String.IsNullOrEmpty(this.control.NIOROSHIGYOUSYA_CD.Text)))
                {
                    sql.AppendFormat(" AND NIOROSHI_GYOUSHA_CD = '{0}' ", this.control.NIOROSHIGYOUSYA_CD.Text);
                }
                if ((!String.IsNullOrEmpty(this.control.NIOROSHIJYOUMEI_CD.Text)))
                {
                    sql.AppendFormat(" AND NIOROSHI_GENBA_CD = '{0}' ", this.control.NIOROSHIJYOUMEI_CD.Text);
                }
                sql.Append(" UNION ALL ");
            }
            if (String.IsNullOrEmpty(this.control.NIOROSHIGYOUSYA_CD.Text))
            {
                sql.Append(" SELECT SYSTEM_ID ");
                sql.Append(" ,SEQ ");
                sql.Append(" ,2 AS DENSHU_KBN_CD ");
                sql.Append(" ,'出荷' AS DENSHU_KBN_NAME ");
                sql.Append(" ,TAIRYUU_KBN ");
                sql.Append(" ,KYOTEN_CD ");
                sql.Append(" ,SHUKKA_NUMBER AS DENPYOU_NUMBER ");
                sql.Append(" ,DATE_NUMBER ");
                sql.Append(" ,YEAR_NUMBER ");
                sql.Append(" ,KAKUTEI_KBN ");
                sql.Append(" ,DENPYOU_DATE ");
                sql.Append(" ,URIAGE_DATE ");
                sql.Append(" ,SHIHARAI_DATE ");
                sql.Append(" ,TORIHIKISAKI_CD ");
                sql.Append(" ,TORIHIKISAKI_NAME ");
                sql.Append(" ,GYOUSHA_CD ");
                sql.Append(" ,GYOUSHA_NAME ");
                sql.Append(" ,GENBA_CD ");
                sql.Append(" ,GENBA_NAME	   ");
                sql.Append(" ,NULL AS NIOROSHI_GYOUSHA_CD ");
                sql.Append(" ,NULL AS NIOROSHI_GYOUSHA_NAME ");
                sql.Append(" ,NULL AS NIOROSHI_GENBA_CD ");
                sql.Append(" ,NULL AS NIOROSHI_GENBA_NAME ");
                sql.Append(" ,NIZUMI_GYOUSHA_CD ");
                sql.Append(" ,NIZUMI_GYOUSHA_NAME ");
                sql.Append(" ,NIZUMI_GENBA_CD ");
                sql.Append(" ,NIZUMI_GENBA_NAME ");
                sql.Append(" ,EIGYOU_TANTOUSHA_CD ");
                sql.Append(" ,EIGYOU_TANTOUSHA_NAME ");
                sql.Append(" ,NYUURYOKU_TANTOUSHA_CD ");
                sql.Append(" ,NYUURYOKU_TANTOUSHA_NAME ");
                sql.Append(" ,SHARYOU_CD ");
                sql.Append(" ,SHARYOU_NAME ");
                sql.Append(" ,SHASHU_CD ");
                sql.Append(" ,SHASHU_NAME ");
                sql.Append(" ,UNPAN_GYOUSHA_CD ");
                sql.Append(" ,UNPAN_GYOUSHA_NAME ");
                sql.Append(" ,UNTENSHA_CD ");
                sql.Append(" ,UNTENSHA_NAME ");
                sql.Append(" ,NINZUU_CNT ");
                sql.Append(" ,KEITAI_KBN_CD ");
                sql.Append(" ,DAIKAN_KBN ");
                sql.Append(" ,NULL AS CONTENA_SOUSA_CD ");
                sql.Append(" ,MANIFEST_SHURUI_CD ");
                sql.Append(" ,MANIFEST_TEHAI_CD ");
                sql.Append(" ,DENPYOU_BIKOU ");
                sql.Append(" ,TAIRYUU_BIKOU ");
                sql.Append(" ,UKETSUKE_NUMBER ");
                sql.Append(" ,KEIRYOU_NUMBER ");
                sql.Append(" ,RECEIPT_NUMBER ");
                sql.Append(" ,RECEIPT_NUMBER_YEAR ");
                sql.Append(" ,NET_TOTAL ");
                sql.Append(" ,URIAGE_SHOUHIZEI_RATE ");
                sql.Append(" ,URIAGE_AMOUNT_TOTAL AS URIAGE_KINGAKU_TOTAL ");
                sql.Append(" ,URIAGE_TAX_SOTO ");
                sql.Append(" ,URIAGE_TAX_UCHI ");
                sql.Append(" ,URIAGE_TAX_SOTO_TOTAL ");
                sql.Append(" ,URIAGE_TAX_UCHI_TOTAL ");
                sql.Append(" ,HINMEI_URIAGE_KINGAKU_TOTAL ");
                sql.Append(" ,HINMEI_URIAGE_TAX_SOTO_TOTAL ");
                sql.Append(" ,HINMEI_URIAGE_TAX_UCHI_TOTAL ");
                sql.Append(" ,SHIHARAI_SHOUHIZEI_RATE ");
                sql.Append(" ,SHIHARAI_AMOUNT_TOTAL AS SHIHARAI_KINGAKU_TOTAL ");
                sql.Append(" ,SHIHARAI_TAX_SOTO ");
                sql.Append(" ,SHIHARAI_TAX_UCHI ");
                sql.Append(" ,SHIHARAI_TAX_SOTO_TOTAL ");
                sql.Append(" ,SHIHARAI_TAX_UCHI_TOTAL ");
                sql.Append(" ,HINMEI_SHIHARAI_KINGAKU_TOTAL ");
                sql.Append(" ,HINMEI_SHIHARAI_TAX_SOTO_TOTAL ");
                sql.Append(" ,HINMEI_SHIHARAI_TAX_UCHI_TOTAL ");
                sql.Append(" ,URIAGE_ZEI_KEISAN_KBN_CD ");
                sql.Append(" ,URIAGE_ZEI_KBN_CD ");
                sql.Append(" ,URIAGE_TORIHIKI_KBN_CD ");
                sql.Append(" ,SHIHARAI_ZEI_KEISAN_KBN_CD ");
                sql.Append(" ,SHIHARAI_ZEI_KBN_CD ");
                sql.Append(" ,SHIHARAI_TORIHIKI_KBN_CD ");
                sql.Append(" ,KENSHU_MUST_KBN ");
                sql.Append(" ,KENSHU_DATE ");
                sql.Append(" ,KENSHU_URIAGE_DATE ");
                sql.Append(" ,KENSHU_SHIHARAI_DATE ");
                sql.Append(" ,SHUKKA_NET_TOTAL ");
                sql.Append(" ,KENSHU_NET_TOTAL ");
                sql.Append(" ,SABUN ");
                sql.Append(" ,SHUKKA_KINGAKU_TOTAL ");
                sql.Append(" ,KENSHU_KINGAKU_TOTAL ");
                sql.Append(" ,SAGAKU ");
                sql.Append(" ,KENSHU_URIAGE_SHOUHIZEI_RATE ");
                sql.Append(" ,KENSHU_URIAGE_AMOUNT_TOTAL ");
                sql.Append(" ,KENSHU_URIAGE_TAX_SOTO ");
                sql.Append(" ,KENSHU_URIAGE_TAX_UCHI ");
                sql.Append(" ,KENSHU_URIAGE_TAX_SOTO_TOTAL ");
                sql.Append(" ,KENSHU_URIAGE_TAX_UCHI_TOTAL ");
                sql.Append(" ,KENSHU_HINMEI_URIAGE_KINGAKU_TOTAL ");
                sql.Append(" ,KENSHU_HINMEI_URIAGE_TAX_SOTO_TOTAL ");
                sql.Append(" ,KENSHU_HINMEI_URIAGE_TAX_UCHI_TOTAL ");
                sql.Append(" ,KENSHU_SHIHARAI_SHOUHIZEI_RATE ");
                sql.Append(" ,KENSHU_SHIHARAI_AMOUNT_TOTAL ");
                sql.Append(" ,KENSHU_SHIHARAI_TAX_SOTO ");
                sql.Append(" ,KENSHU_SHIHARAI_TAX_UCHI ");
                sql.Append(" ,KENSHU_SHIHARAI_TAX_SOTO_TOTAL ");
                sql.Append(" ,KENSHU_SHIHARAI_TAX_UCHI_TOTAL ");
                sql.Append(" ,KENSHU_HINMEI_SHIHARAI_KINGAKU_TOTAL ");
                sql.Append(" ,KENSHU_HINMEI_SHIHARAI_TAX_SOTO_TOTAL ");
                sql.Append(" ,KENSHU_HINMEI_SHIHARAI_TAX_UCHI_TOTAL ");
                sql.Append(" ,CREATE_USER ");
                sql.Append(" ,CREATE_DATE ");
                sql.Append(" ,CREATE_PC ");
                sql.Append(" ,UPDATE_USER ");
                sql.Append(" ,UPDATE_DATE ");
                sql.Append(" ,UPDATE_PC ");
                sql.Append(" ,DELETE_FLG ");
                sql.Append(" ,TIME_STAMP ");
                sql.Append(" ,MOD_SHUUKEI_KOUMOKU_CD ");
                sql.Append(" ,MOD_SHUUKEI_KOUMOKU_NAME ");
                sql.Append(" ,STACK_JYUURYOU ");
                sql.Append(" ,STACK_KEIRYOU_TIME ");
                sql.Append(" ,EMPTY_JYUURYOU ");
                sql.Append(" ,EMPTY_KEIRYOU_TIME ");
                sql.Append(" FROM T_SHUKKA_ENTRY ");
                sql.Append(" WHERE DELETE_FLG = 0 AND TAIRYUU_KBN = 0 ");
                if ((!String.IsNullOrEmpty(this.control.NIDUMIGYOUSYA_CD.Text)))
                {
                    sql.AppendFormat(" AND NIZUMI_GYOUSHA_CD = '{0}' ", this.control.NIDUMIGYOUSYA_CD.Text);
                }
                if ((!String.IsNullOrEmpty(this.control.NIDUMIGENBA_CD.Text)))
                {
                    sql.AppendFormat(" AND NIZUMI_GENBA_CD = '{0}' ", this.control.NIDUMIGENBA_CD.Text);
                }
                sql.Append(" UNION ALL ");
            }
            sql.Append(" SELECT SYSTEM_ID ");
            sql.Append(" ,SEQ ");
            sql.Append(" ,3 AS DENSHU_KBN_CD ");
            sql.Append(" ,'売上/支払' AS DENSHU_KBN_NAME ");
            sql.Append(" ,NULL AS TAIRYUU_KBN ");
            sql.Append(" ,KYOTEN_CD ");
            sql.Append(" ,UR_SH_NUMBER	AS DENPYOU_NUMBER ");
            sql.Append(" ,DATE_NUMBER ");
            sql.Append(" ,YEAR_NUMBER ");
            sql.Append(" ,KAKUTEI_KBN ");
            sql.Append(" ,DENPYOU_DATE ");
            sql.Append(" ,URIAGE_DATE ");
            sql.Append(" ,SHIHARAI_DATE ");
            sql.Append(" ,TORIHIKISAKI_CD ");
            sql.Append(" ,TORIHIKISAKI_NAME ");
            sql.Append(" ,GYOUSHA_CD ");
            sql.Append(" ,GYOUSHA_NAME ");
            sql.Append(" ,GENBA_CD ");
            sql.Append(" ,GENBA_NAME ");
            sql.Append(" ,NIOROSHI_GYOUSHA_CD ");
            sql.Append(" ,NIOROSHI_GYOUSHA_NAME ");
            sql.Append(" ,NIOROSHI_GENBA_CD ");
            sql.Append(" ,NIOROSHI_GENBA_NAME ");
            sql.Append(" ,NIZUMI_GYOUSHA_CD ");
            sql.Append(" ,NIZUMI_GYOUSHA_NAME ");
            sql.Append(" ,NIZUMI_GENBA_CD ");
            sql.Append(" ,NIZUMI_GENBA_NAME ");
            sql.Append(" ,EIGYOU_TANTOUSHA_CD ");
            sql.Append(" ,EIGYOU_TANTOUSHA_NAME ");
            sql.Append(" ,NYUURYOKU_TANTOUSHA_CD ");
            sql.Append(" ,NYUURYOKU_TANTOUSHA_NAME ");
            sql.Append(" ,SHARYOU_CD ");
            sql.Append(" ,SHARYOU_NAME ");
            sql.Append(" ,SHASHU_CD ");
            sql.Append(" ,SHASHU_NAME ");
            sql.Append(" ,UNPAN_GYOUSHA_CD ");
            sql.Append(" ,UNPAN_GYOUSHA_NAME ");
            sql.Append(" ,UNTENSHA_CD ");
            sql.Append(" ,UNTENSHA_NAME ");
            sql.Append(" ,NULL AS NINZUU_CNT ");
            sql.Append(" ,KEITAI_KBN_CD ");
            sql.Append(" ,NULL AS DAIKAN_KBN ");
            sql.Append(" ,CONTENA_SOUSA_CD ");
            sql.Append(" ,MANIFEST_SHURUI_CD ");
            sql.Append(" ,MANIFEST_TEHAI_CD ");
            sql.Append(" ,DENPYOU_BIKOU ");
            sql.Append(" ,NULL AS TAIRYUU_BIKOU ");
            sql.Append(" ,UKETSUKE_NUMBER ");
            sql.Append(" ,NULL AS KEIRYOU_NUMBER ");
            sql.Append(" ,RECEIPT_NUMBER ");
            sql.Append(" ,RECEIPT_NUMBER_YEAR ");
            sql.Append(" ,NULL AS NET_TOTAL ");
            sql.Append(" ,URIAGE_SHOUHIZEI_RATE ");
            sql.Append(" ,URIAGE_AMOUNT_TOTAL  AS URIAGE_KINGAKU_TOTAL ");
            sql.Append(" ,URIAGE_TAX_SOTO ");
            sql.Append(" ,URIAGE_TAX_UCHI ");
            sql.Append(" ,URIAGE_TAX_SOTO_TOTAL ");
            sql.Append(" ,URIAGE_TAX_UCHI_TOTAL ");
            sql.Append(" ,HINMEI_URIAGE_KINGAKU_TOTAL ");
            sql.Append(" ,HINMEI_URIAGE_TAX_SOTO_TOTAL ");
            sql.Append(" ,HINMEI_URIAGE_TAX_UCHI_TOTAL ");
            sql.Append(" ,SHIHARAI_SHOUHIZEI_RATE ");
            sql.Append(" ,SHIHARAI_AMOUNT_TOTAL   AS SHIHARAI_KINGAKU_TOTAL ");
            sql.Append(" ,SHIHARAI_TAX_SOTO ");
            sql.Append(" ,SHIHARAI_TAX_UCHI ");
            sql.Append(" ,SHIHARAI_TAX_SOTO_TOTAL ");
            sql.Append(" ,SHIHARAI_TAX_UCHI_TOTAL ");
            sql.Append(" ,HINMEI_SHIHARAI_KINGAKU_TOTAL ");
            sql.Append(" ,HINMEI_SHIHARAI_TAX_SOTO_TOTAL ");
            sql.Append(" ,HINMEI_SHIHARAI_TAX_UCHI_TOTAL ");
            sql.Append(" ,URIAGE_ZEI_KEISAN_KBN_CD ");
            sql.Append(" ,URIAGE_ZEI_KBN_CD ");
            sql.Append(" ,URIAGE_TORIHIKI_KBN_CD ");
            sql.Append(" ,SHIHARAI_ZEI_KEISAN_KBN_CD ");
            sql.Append(" ,SHIHARAI_ZEI_KBN_CD ");
            sql.Append(" ,SHIHARAI_TORIHIKI_KBN_CD ");
            sql.Append(" ,NULL AS KENSHU_MUST_KBN ");
            sql.Append(" ,NULL AS KENSHU_DATE ");
            sql.Append(" ,NULL AS KENSHU_URIAGE_DATE ");
            sql.Append(" ,NULL AS KENSHU_SHIHARAI_DATE ");
            sql.Append(" ,NULL AS SHUKKA_NET_TOTAL ");
            sql.Append(" ,NULL AS KENSHU_NET_TOTAL ");
            sql.Append(" ,NULL AS SABUN ");
            sql.Append(" ,NULL AS SHUKKA_KINGAKU_TOTAL ");
            sql.Append(" ,NULL AS KENSHU_KINGAKU_TOTAL ");
            sql.Append(" ,NULL AS SAGAKU ");
            sql.Append(" ,NULL AS KENSHU_URIAGE_SHOUHIZEI_RATE ");
            sql.Append(" ,NULL AS KENSHU_URIAGE_AMOUNT_TOTAL ");
            sql.Append(" ,NULL AS KENSHU_URIAGE_TAX_SOTO ");
            sql.Append(" ,NULL AS KENSHU_URIAGE_TAX_UCHI ");
            sql.Append(" ,NULL AS KENSHU_URIAGE_TAX_SOTO_TOTAL ");
            sql.Append(" ,NULL AS KENSHU_URIAGE_TAX_UCHI_TOTAL ");
            sql.Append(" ,NULL AS KENSHU_HINMEI_URIAGE_KINGAKU_TOTAL ");
            sql.Append(" ,NULL AS KENSHU_HINMEI_URIAGE_TAX_SOTO_TOTAL ");
            sql.Append(" ,NULL AS KENSHU_HINMEI_URIAGE_TAX_UCHI_TOTAL ");
            sql.Append(" ,NULL AS KENSHU_SHIHARAI_SHOUHIZEI_RATE ");
            sql.Append(" ,NULL AS KENSHU_SHIHARAI_AMOUNT_TOTAL ");
            sql.Append(" ,NULL AS KENSHU_SHIHARAI_TAX_SOTO ");
            sql.Append(" ,NULL AS KENSHU_SHIHARAI_TAX_UCHI ");
            sql.Append(" ,NULL AS KENSHU_SHIHARAI_TAX_SOTO_TOTAL ");
            sql.Append(" ,NULL AS KENSHU_SHIHARAI_TAX_UCHI_TOTAL ");
            sql.Append(" ,NULL AS KENSHU_HINMEI_SHIHARAI_KINGAKU_TOTAL ");
            sql.Append(" ,NULL AS KENSHU_HINMEI_SHIHARAI_TAX_SOTO_TOTAL ");
            sql.Append(" ,NULL AS KENSHU_HINMEI_SHIHARAI_TAX_UCHI_TOTAL ");
            sql.Append(" ,CREATE_USER ");
            sql.Append(" ,CREATE_DATE ");
            sql.Append(" ,CREATE_PC ");
            sql.Append(" ,UPDATE_USER ");
            sql.Append(" ,UPDATE_DATE ");
            sql.Append(" ,UPDATE_PC ");
            sql.Append(" ,DELETE_FLG ");
            sql.Append(" ,TIME_STAMP ");
            sql.Append(" ,MOD_SHUUKEI_KOUMOKU_CD ");
            sql.Append(" ,MOD_SHUUKEI_KOUMOKU_NAME ");
            sql.Append(" ,NULL AS STACK_JYUURYOU ");
            sql.Append(" ,NULL AS STACK_KEIRYOU_TIME ");
            sql.Append(" ,NULL AS EMPTY_JYUURYOU ");
            sql.Append(" ,NULL AS EMPTY_KEIRYOU_TIME ");
            sql.Append(" FROM T_UR_SH_ENTRY ");
            sql.Append(" WHERE DELETE_FLG = 0 ");
            sql.Append(" AND (DAINOU_FLG IS NULL OR DAINOU_FLG = 0 ) ");
            if ((!String.IsNullOrEmpty(this.control.NIOROSHIGYOUSYA_CD.Text)))
            {
                sql.AppendFormat(" AND NIOROSHI_GYOUSHA_CD = '{0}' ", this.control.NIOROSHIGYOUSYA_CD.Text);
            }
            if ((!String.IsNullOrEmpty(this.control.NIOROSHIJYOUMEI_CD.Text)))
            {
                sql.AppendFormat(" AND NIOROSHI_GENBA_CD = '{0}' ", this.control.NIOROSHIJYOUMEI_CD.Text);
            }
            
            if ((!String.IsNullOrEmpty(this.control.NIDUMIGYOUSYA_CD.Text)))
            {
                sql.AppendFormat(" AND NIZUMI_GYOUSHA_CD = '{0}' ", this.control.NIDUMIGYOUSYA_CD.Text);
            }
            if ((!String.IsNullOrEmpty(this.control.NIDUMIGENBA_CD.Text)))
            {
                sql.AppendFormat(" AND NIZUMI_GENBA_CD = '{0}' ", this.control.NIDUMIGENBA_CD.Text);
            }
            sql.Append(" ) AS SUMMARY_ENTRY ");
            
            sql.Append(" LEFT JOIN (");
            
            if (String.IsNullOrEmpty(this.control.NIDUMIGYOUSYA_CD.Text))
            {
                sql.Append(" SELECT SYSTEM_ID ");
                sql.Append(" ,SEQ ");
                sql.Append(" ,1 AS DENSHU_KBN_CD ");
                sql.Append(" ,'受入' AS DENSHU_KBN_NAME ");
                sql.Append(" ,DETAIL_SYSTEM_ID ");
                sql.Append(" ,UKEIRE_NUMBER AS DENPYOU_NUMBER ");
                sql.Append(" ,ROW_NO ");
                sql.Append(" ,KAKUTEI_KBN ");
                sql.Append(" ,URIAGESHIHARAI_DATE ");
                sql.Append(" ,STACK_JYUURYOU ");
                sql.Append(" ,EMPTY_JYUURYOU ");
                sql.Append(" ,WARIFURI_JYUURYOU ");
                sql.Append(" ,WARIFURI_PERCENT ");
                sql.Append(" ,CHOUSEI_JYUURYOU ");
                sql.Append(" ,CHOUSEI_PERCENT ");
                sql.Append(" ,YOUKI_CD ");
                sql.Append(" ,YOUKI_SUURYOU ");
                sql.Append(" ,YOUKI_JYUURYOU ");
                sql.Append(" ,DENPYOU_KBN_CD ");
                sql.Append(" ,HINMEI_CD ");
                sql.Append(" ,HINMEI_NAME ");
                sql.Append(" ,NET_JYUURYOU ");
                sql.Append(" ,SUURYOU ");
                sql.Append(" ,UNIT_CD ");
                sql.Append(" ,TANKA ");
                sql.Append(" ,KINGAKU ");
                sql.Append(" ,TAX_SOTO ");
                sql.Append(" ,TAX_UCHI ");
                sql.Append(" ,HINMEI_ZEI_KBN_CD ");
                sql.Append(" ,HINMEI_KINGAKU ");
                sql.Append(" ,HINMEI_TAX_SOTO ");
                sql.Append(" ,HINMEI_TAX_UCHI ");
                sql.Append(" ,MEISAI_BIKOU ");
                sql.Append(" ,NISUGATA_SUURYOU ");
                sql.Append(" ,NISUGATA_UNIT_CD ");
                sql.Append(" ,TIME_STAMP ");
                sql.Append(" ,KEIRYOU_TIME ");
                sql.Append(" FROM T_UKEIRE_DETAIL ");
                sql.Append(" UNION ALL ");
            }
            
            if (String.IsNullOrEmpty(this.control.NIOROSHIGYOUSYA_CD.Text))
            {
                sql.Append(" SELECT SYSTEM_ID ");
                sql.Append(" ,SEQ ");
                sql.Append(" ,2 AS DENSHU_KBN_CD ");
                sql.Append(" ,'出荷' AS DENSHU_KBN_NAME ");
                sql.Append(" ,DETAIL_SYSTEM_ID ");
                sql.Append(" ,SHUKKA_NUMBER AS DENPYOU_NUMBER ");
                sql.Append(" ,ROW_NO ");
                sql.Append(" ,KAKUTEI_KBN ");
                sql.Append(" ,URIAGESHIHARAI_DATE ");
                sql.Append(" ,STACK_JYUURYOU ");
                sql.Append(" ,EMPTY_JYUURYOU ");
                sql.Append(" ,WARIFURI_JYUURYOU ");
                sql.Append(" ,WARIFURI_PERCENT ");
                sql.Append(" ,CHOUSEI_JYUURYOU ");
                sql.Append(" ,CHOUSEI_PERCENT ");
                sql.Append(" ,YOUKI_CD ");
                sql.Append(" ,YOUKI_SUURYOU ");
                sql.Append(" ,YOUKI_JYUURYOU ");
                sql.Append(" ,DENPYOU_KBN_CD ");
                sql.Append(" ,HINMEI_CD ");
                sql.Append(" ,HINMEI_NAME ");
                sql.Append(" ,NET_JYUURYOU ");
                sql.Append(" ,SUURYOU ");
                sql.Append(" ,UNIT_CD ");
                sql.Append(" ,TANKA ");
                sql.Append(" ,KINGAKU ");
                sql.Append(" ,TAX_SOTO ");
                sql.Append(" ,TAX_UCHI ");
                sql.Append(" ,HINMEI_ZEI_KBN_CD ");
                sql.Append(" ,HINMEI_KINGAKU ");
                sql.Append(" ,HINMEI_TAX_SOTO ");
                sql.Append(" ,HINMEI_TAX_UCHI ");
                sql.Append(" ,MEISAI_BIKOU ");
                sql.Append(" ,NISUGATA_SUURYOU ");
                sql.Append(" ,NISUGATA_UNIT_CD ");
                sql.Append(" ,TIME_STAMP ");
                sql.Append(" ,KEIRYOU_TIME ");
                sql.Append(" FROM T_SHUKKA_DETAIL ");
                sql.Append(" UNION ALL ");
            }
            sql.Append(" SELECT SYSTEM_ID ");
            sql.Append(" ,SEQ ");
            sql.Append(" ,3 AS DENSHU_KBN_CD ");
            sql.Append(" ,'売上/支払' AS DENSHU_KBN_NAME ");
            sql.Append(" ,DETAIL_SYSTEM_ID ");
            sql.Append(" ,UR_SH_NUMBER	 AS DENPYOU_NUMBER ");
            sql.Append(" ,ROW_NO ");
            sql.Append(" ,KAKUTEI_KBN ");
            sql.Append(" ,URIAGESHIHARAI_DATE ");
            sql.Append(" ,NULL AS STACK_JYUURYOU ");
            sql.Append(" ,NULL AS EMPTY_JYUURYOU ");
            sql.Append(" ,NULL AS WARIFURI_JYUURYOU ");
            sql.Append(" ,NULL AS WARIFURI_PERCENT ");
            sql.Append(" ,NULL AS CHOUSEI_JYUURYOU ");
            sql.Append(" ,NULL AS CHOUSEI_PERCENT ");
            sql.Append(" ,NULL AS YOUKI_CD ");
            sql.Append(" ,NULL AS YOUKI_SUURYOU ");
            sql.Append(" ,NULL AS YOUKI_JYUURYOU ");
            sql.Append(" ,DENPYOU_KBN_CD ");
            sql.Append(" ,HINMEI_CD ");
            sql.Append(" ,HINMEI_NAME ");
            sql.Append(" ,NULL AS NET_JYUURYOU ");
            sql.Append(" ,SUURYOU ");
            sql.Append(" ,UNIT_CD ");
            sql.Append(" ,TANKA ");
            sql.Append(" ,KINGAKU ");
            sql.Append(" ,TAX_SOTO ");
            sql.Append(" ,TAX_UCHI ");
            sql.Append(" ,HINMEI_ZEI_KBN_CD ");
            sql.Append(" ,HINMEI_KINGAKU ");
            sql.Append(" ,HINMEI_TAX_SOTO ");
            sql.Append(" ,HINMEI_TAX_UCHI ");
            sql.Append(" ,MEISAI_BIKOU ");
            sql.Append(" ,NISUGATA_SUURYOU ");
            sql.Append(" ,NISUGATA_UNIT_CD ");
            sql.Append(" ,TIME_STAMP ");
            sql.Append(" ,NULL AS KEIRYOU_TIME ");
            sql.Append(" FROM T_UR_SH_DETAIL ");
            sql.Append(" ) AS SUMMARY_DETAIL ");
            sql.Append(" ON SUMMARY_ENTRY.SYSTEM_ID = SUMMARY_DETAIL.SYSTEM_ID AND SUMMARY_ENTRY.SEQ = SUMMARY_DETAIL.SEQ AND SUMMARY_ENTRY.DENSHU_KBN_CD = SUMMARY_DETAIL.DENSHU_KBN_CD");
            
            sql.Append(" LEFT JOIN T_KENSHU_DETAIL ");
            sql.Append(" ON T_KENSHU_DETAIL.SYSTEM_ID = SUMMARY_DETAIL.SYSTEM_ID AND T_KENSHU_DETAIL.SEQ = SUMMARY_DETAIL.SEQ AND T_KENSHU_DETAIL.DETAIL_SYSTEM_ID = SUMMARY_DETAIL.DETAIL_SYSTEM_ID AND SUMMARY_DETAIL.DENSHU_KBN_CD = 2 ");
        }
        //PhuocLoc 2021/05/05 #148576 -End
        #endregion

        #region ボタン情報の設定
        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }
        #endregion

        #region 全角の英数字の文字列を半角に変換
        /// <summary>
        /// 全角スペースを半角スペースに変換する
        /// </summary>
        /// <param name="param">半角へ変換する文字列</param>
        /// <returns></returns>
        public string ToHankakuSpace(string param)
        {
            Regex re = new Regex("[　]+");
            string output = re.Replace(param, MyReplacer);
            return output;
        }

        /// <summary>
        /// 全角の英数字の文字列を半角に変換する
        /// </summary>
        /// <param name="param">半角へ変換する文字列</param>
        /// <returns></returns>
        public string ToHankaku(string param)
        {
            Regex re = new Regex("[０-９Ａ-Ｚａ-ｚ　]+");
            string output = re.Replace(param, MyReplacer);
            return output;
        }

        static string MyReplacer(Match m)
        {
            return Strings.StrConv(m.Value, VbStrConv.Narrow, 0);
        }
        #endregion

        #region 検索処理
        /// <summary>
        /// 検索処理を行う
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int ret_cnt = 0;
            try
            {
                //SELECT句未取得なら検索できない
                if (!string.IsNullOrEmpty(this.form.baseSelectQuery))
                {
                    // ベースロジッククラスで作成したクエリをセット
                    this.selectQuery = this.form.baseSelectQuery;
                    this.orderByQuery = this.form.baseOrderByQuery;
                    this.joinQuery = this.form.baseJoinQuery;
                    string tblName1 = string.Empty;
                    string tblName2 = string.Empty;
                    string tblName3 = string.Empty;
                    string tblName4 = string.Empty;
                    string denpyouNum = string.Empty;
                    switch (disp_Flg)
                    {
                        case DENPYOU_TYPE_UKEIRE:
                            tblName1 = "T_UKEIRE_ENTRY";
                            tblName2 = "T_UKEIRE_DETAIL";
                            tblName3 = "T_UKEIRE_JISSEKI_DETAIL";
                            denpyouNum = "UKEIRE_NUMBER";
                            break;
                        case DENPYOU_TYPE_SHUKKA:
                            tblName1 = "T_SHUKKA_ENTRY";
                            tblName2 = "T_SHUKKA_DETAIL";
                            denpyouNum = "SHUKKA_NUMBER";
                            break;
                        case DENPYOU_TYPE_URIAGESHIHARAI:
                            tblName1 = "T_UR_SH_ENTRY";
                            tblName2 = "T_UR_SH_DETAIL";
                            denpyouNum = "UR_SH_NUMBER";
                            break;
                        case DENPYOU_TYPE_DAINOU:
                            tblName1 = "T_UR_SH_ENTRY";
                            tblName2 = "T_UR_SH_DETAIL";
                            tblName3 = "T_UR_SH_ENTRY1";
                            tblName4 = "T_UR_SH_DETAIL1";
                            denpyouNum = "UR_SH_NUMBER";
                            break;
                        //PhuocLoc 2021/05/05 #148576 -Start
                        case DENPYOU_TYPE_SUBETE:
                            tblName1 = "SUMMARY_ENTRY";
                            tblName2 = "SUMMARY_DETAIL";
                            denpyouNum = "DENPYOU_NUMBER";
                            break;
                        //PhuocLoc 2021/05/05 #148576 -End
                    }
                    var order = new StringBuilder();
                    var sql = new StringBuilder();
                    sql.Append(" SELECT DISTINCT ");
                    sql.Append(this.selectQuery);

                    #region システム上必須な項目をSELECT句に追加する（後で非表示にする）
                    sql.AppendFormat(" , {0}.{1} AS {2} ", tblName1, denpyouNum, this.HIDDEN_DENPYOU_NUMBER);
                    order.AppendFormat(" , {0} ASC ", this.HIDDEN_DENPYOU_NUMBER);

                    //PhuocLoc 2021/05/05 #148576 -Start
                    if (this.form.radbtn_DenshuSubete.Checked)
                    {
                        sql.AppendFormat(" , {0}.DENSHU_KBN_CD AS {1} ", tblName1, this.HIDDEN_DENSHU_KBN_CD);
                    }
                    //PhuocLoc 2021/05/05 #148576 -End

                    // 出力区分が明細の場合
                    if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == 2)
                    {
                        sql.AppendFormat(" , {0}.DETAIL_SYSTEM_ID AS {1} ", tblName2, this.HIDDEN_DETAIL_SYSTEM_ID);
                        order.AppendFormat(" , {0} ASC ", this.HIDDEN_DETAIL_SYSTEM_ID);
                    }
                    if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == 3)
                    {
                        sql.AppendFormat(" , {0}.DETAIL_SEQ AS {1} ", tblName3, this.HIDDEN_DETAIL_SYSTEM_ID);
                        order.AppendFormat(" , {0} ASC ", this.HIDDEN_DETAIL_SYSTEM_ID);
                    }

                    string errorDefault = "\'0\'";
                    //[伝票種類]＝「1」（受入）、「3」（売上支払）の場合
                    if (disp_Flg == DENPYOU_TYPE_UKEIRE || disp_Flg == DENPYOU_TYPE_URIAGESHIHARAI)
                    {
                        sql.AppendFormat(" ,{0} AS HST_GYOUSHA_CD_ERROR", errorDefault);
                        sql.AppendFormat(" ,{0} AS HST_GENBA_CD_ERROR", errorDefault);
                        sql.AppendFormat(" ,{0} AS HAIKI_SHURUI_CD_ERROR", errorDefault);
                        sql.AppendFormat(" , {0}.SYSTEM_ID AS {1} ", tblName1, "HIDDEN_SEARCH_SYSTEM_ID");
                    }
                    if (disp_Flg == DENPYOU_TYPE_SHUKKA || disp_Flg == DENPYOU_TYPE_SUBETE) //PhuocLoc 2021/05/05 #148576
                    {
                        sql.AppendFormat(" , {0}.SYSTEM_ID AS {1} ", tblName1, "HIDDEN_SEARCH_SYSTEM_ID");
                    }
                    #endregion

                    sql.Append(" FROM ");
                    switch (this.disp_Flg)
                    {
                        case DENPYOU_TYPE_UKEIRE:
                            this.MakeSearchJuchu(sql);
                            break;
                        case DENPYOU_TYPE_SHUKKA:
                            this.MakeSearchShuka(sql);
                            break;
                        case DENPYOU_TYPE_URIAGESHIHARAI:
                            this.MakeSearchUriageShiharai(sql);
                            break;
                        case DENPYOU_TYPE_DAINOU:
                            this.MakeSearchDainou(sql);
                            break;
                        //PhuocLoc 2021/05/05 #148576 -Start
                        case DENPYOU_TYPE_SUBETE:
                            this.MakeSearchSubete(sql);
                            break;
                        //PhuocLoc 2021/05/05 #148576 -End
                        default:
                            break;
                    }
                    // No.3508-->
                    int denpyouType = 0;
                    switch (this.disp_Flg)
                    {
                        case DENPYOU_TYPE_UKEIRE:
                            denpyouType = DENPYOU_TYPE_UKEIRE;
                            break;
                        case DENPYOU_TYPE_SHUKKA:
                            denpyouType = DENPYOU_TYPE_SHUKKA;
                            break;
                        case DENPYOU_TYPE_URIAGESHIHARAI:
                            denpyouType = DENPYOU_TYPE_URIAGESHIHARAI;
                            break;
                        case DENPYOU_TYPE_DAINOU:
                            denpyouType = DENPYOU_TYPE_URIAGESHIHARAI;
                            break;
                        //PhuocLoc 2021/05/05 #148576 -Start
                        case DENPYOU_TYPE_SUBETE:
                            denpyouType = DENPYOU_TYPE_SUBETE;
                            break;
                        //PhuocLoc 2021/05/05 #148576 -End
                        default:
                            break;
                    }
                    if (denpyouType != 0)
                    {
                        //PhuocLoc 2021/05/05 #148576 -Start
                        string denpyouTypeStr = denpyouType.ToString();
                        if (denpyouType == DENPYOU_TYPE_SUBETE)
                        {
                            denpyouTypeStr = "SUMMARY_DETAIL.DENSHU_KBN_CD";
                        }
                        //PhuocLoc 2021/05/05 #148576 -End
                        sql.Append("LEFT JOIN T_SEIKYUU_DETAIL T_SEIKYUU_DETAIL1 ");
                        sql.AppendFormat("ON T_SEIKYUU_DETAIL1.DENPYOU_SHURUI_CD = {0} ", denpyouTypeStr);
                        sql.AppendFormat("AND {0}.SYSTEM_ID = T_SEIKYUU_DETAIL1.DENPYOU_SYSTEM_ID ", tblName1);
                        sql.AppendFormat("AND {0}.SEQ = T_SEIKYUU_DETAIL1.DENPYOU_SEQ ", tblName1);
                        sql.Append("AND T_SEIKYUU_DETAIL1.DELETE_FLG = 0 ");
                        sql.Append("LEFT JOIN T_SEISAN_DETAIL T_SEISAN_DETAIL1 ");
                        sql.AppendFormat("ON T_SEISAN_DETAIL1.DENPYOU_SHURUI_CD = {0} ", denpyouTypeStr);
                        sql.AppendFormat("AND {0}.SYSTEM_ID = T_SEISAN_DETAIL1.DENPYOU_SYSTEM_ID ", tblName1);
                        sql.AppendFormat("AND {0}.SEQ = T_SEISAN_DETAIL1.DENPYOU_SEQ ", tblName1);
                        sql.Append("AND T_SEISAN_DETAIL1.DELETE_FLG = 0 ");
                        sql.Append("LEFT JOIN T_ZAIKO_UKEIRE_DETAIL T_ZAIKO_UKEIRE_DETAIL1 ");
                        sql.AppendFormat("ON T_ZAIKO_UKEIRE_DETAIL1.DENSHU_KBN_CD = {0} ", denpyouTypeStr);
                        sql.AppendFormat("AND {0}.SYSTEM_ID = T_ZAIKO_UKEIRE_DETAIL1.SYSTEM_ID ", tblName1);
                        sql.AppendFormat("AND {0}.SEQ = T_ZAIKO_UKEIRE_DETAIL1.SEQ ", tblName1);
                        sql.Append("AND T_ZAIKO_UKEIRE_DETAIL1.DELETE_FLG = 0 ");
                        //CongBinh 20200331 #134988 S
                        if (this.disp_Flg != DENPYOU_TYPE_DAINOU)
                        {
                            if (!string.IsNullOrEmpty(this.control.cmbShimebi.Text))
                            {
                                // 取引先請求テーブルJOIN
                                sql.Append(" LEFT JOIN M_TORIHIKISAKI_SEIKYUU ON ");
                                sql.AppendFormat(" {0}.TORIHIKISAKI_CD = M_TORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD ", tblName1);
                            }
                        }
                        if (!string.IsNullOrEmpty(this.control.cmbShihariaShimebi.Text))
                        {
                            // 取引先支払テーブルJOIN
                            sql.Append(" LEFT JOIN M_TORIHIKISAKI_SHIHARAI ON ");
                            sql.AppendFormat(" {0}.TORIHIKISAKI_CD = M_TORIHIKISAKI_SHIHARAI.TORIHIKISAKI_CD ", tblName1);
                        }
                     
                    }

                    // 代納_出荷締済情報の追加
                    if (this.disp_Flg == DENPYOU_TYPE_DAINOU && denpyouType == DENPYOU_TYPE_URIAGESHIHARAI)
                    {
                        sql.Append("LEFT JOIN T_SEIKYUU_DETAIL T_SEIKYUU_DETAIL2 ");
                        sql.AppendFormat("ON T_SEIKYUU_DETAIL2.DENPYOU_SHURUI_CD = {0} ", denpyouType);
                        sql.AppendFormat("AND {0}.SYSTEM_ID = T_SEIKYUU_DETAIL2.DENPYOU_SYSTEM_ID ", tblName3);
                        sql.AppendFormat("AND {0}.SEQ = T_SEIKYUU_DETAIL2.DENPYOU_SEQ ", tblName3);
                        sql.Append("AND T_SEIKYUU_DETAIL2.DELETE_FLG = 0 ");
                        sql.Append("LEFT JOIN T_SEISAN_DETAIL T_SEISAN_DETAIL2 ");
                        sql.AppendFormat("ON T_SEISAN_DETAIL2.DENPYOU_SHURUI_CD = {0} ", denpyouType);
                        sql.AppendFormat("AND {0}.SYSTEM_ID = T_SEISAN_DETAIL2.DENPYOU_SYSTEM_ID ", tblName3);
                        sql.AppendFormat("AND {0}.SEQ = T_SEISAN_DETAIL2.DENPYOU_SEQ ", tblName3);
                        sql.Append("AND T_SEISAN_DETAIL2.DELETE_FLG = 0 ");
                        sql.Append("LEFT JOIN T_ZAIKO_UKEIRE_DETAIL T_ZAIKO_UKEIRE_DETAIL2 ");
                        sql.AppendFormat("ON T_ZAIKO_UKEIRE_DETAIL2.DENSHU_KBN_CD = {0} ", denpyouType);
                        sql.AppendFormat("AND {0}.SYSTEM_ID = T_ZAIKO_UKEIRE_DETAIL2.SYSTEM_ID ", tblName3);
                        sql.AppendFormat("AND {0}.SEQ = T_ZAIKO_UKEIRE_DETAIL2.SEQ ", tblName3);
                        sql.Append("AND T_ZAIKO_UKEIRE_DETAIL2.DELETE_FLG = 0 ");
                        //CongBinh 20200331 #134988 S
                        if (!string.IsNullOrEmpty(this.control.cmbDainoShimebi.Text))
                        {
                            // 取引先請求テーブルJOIN
                            sql.Append(" LEFT JOIN M_TORIHIKISAKI_SEIKYUU ON ");
                            sql.AppendFormat(" {0}.TORIHIKISAKI_CD = M_TORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD ", tblName3);
                        }
                        //CongBinh 20200331 #134988 E
                    }
                    // No.3508<--
                    sql.Append(this.joinQuery);
                    this.MakeWhereSql(sql);
                    sql.Append(" ORDER BY ");
                    sql.Append(this.orderByQuery);
                    sql.Append(order);
                    this.mcreateSql = sql.ToString();
                    //検索実行
                    this.searchResult = new DataTable();
                    if (!string.IsNullOrEmpty(this.mcreateSql))
                    {
                        this.searchResult = this.daoIchiran.getdateforstringsql(this.mcreateSql);
                    }
                    ret_cnt = searchResult.Rows.Count;
                    //検索結果表示
                    this.form.ShowData();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret_cnt = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret_cnt = -1;
            }
            return ret_cnt;
        }
        #endregion

        #region 検索条件コントロール関連処理
        /// <summary>
        /// 伝票種類が変更されたときの処理
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void txtNum_DenpyouSyurui_TextChanged(object sender, EventArgs e)
        {
            //伝票種類に応じて状態を変更
            ChangeNyuukinsakiEnabled();
            //パターンを再表示する
            this.form.PatternUpdate();
            //パターン区分が受入実績の時は、伝票区分を非活性にする
            if (this.form.logic.currentPatternDto != null && this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == 3)
            {
                this.form.customPanel2.Enabled = false;
            }
            else
            {
                this.form.customPanel2.Enabled = true;
            }
            // 20140624 syunrei EV004430_受付一覧で検索後、伝票種類を変更してからサブファンクションで伝票紐付をするとシステムエラー　start
            if (this.form.customDataGridView1 != null && this.form.customDataGridView1.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)this.form.customDataGridView1.DataSource;
                dt.Clear();
                this.form.customDataGridView1.DataSource = dt;
            }
            // 20140624 syunrei EV004430_受付一覧で検索後、伝票種類を変更してからサブファンクションで伝票紐付をするとシステムエラー　end
        }

        /// <summary>
        /// 伝票種類に応じて入金先条件のコントロールの状態を変更します。
        /// </summary>
        private void ChangeNyuukinsakiEnabled()
        {
            //CongBinh 20200331 #134988 S
            this.control.cmbShimebi.Enabled = true;
            this.control.cmbShimebi.SelectedIndex = 0;
            this.control.cmbShihariaShimebi.Enabled = true;
            this.control.cmbShihariaShimebi.SelectedIndex = 0;
            this.control.cmbDainoShimebi.SelectedIndex = 0;
            this.control.label1.Visible = false;
            this.control.cmbDainoShimebi.Visible = false;
            this.control.TORIHIKISAKI_CD_LABEL.Text = "取引先";
            this.control.GYOUSYA_CD_LABEL.Text = "業者";
            this.control.GENNBA_CD_LABEL.Text = "現場";
            this.control.TORIHIKISAKI_CD.Text = string.Empty;
            this.control.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.control.GYOUSYA_CD.Text = string.Empty;
            this.control.GYOUSYA_NAME_RYAKU.Text = string.Empty;
            this.control.GENNBA_CD.Text = string.Empty;
            this.control.GENNBA_NAME_RYAKU.Text = string.Empty;
            this.control.UNNBANGYOUSYA_CD.Text = string.Empty;
            this.control.UNNBANGYOUSYA_NAME_RYAKU.Text = string.Empty;
            this.control.NIDUMIGYOUSYA_CD.Text = string.Empty;
            this.control.NIDUMIGYOUSYA_NAME_RYAKU.Text = string.Empty;
            this.control.NIDUMIGENBA_CD.Text = string.Empty;
            this.control.NIDUMIGENBA_NAME_RYAKU.Text = string.Empty;
            this.control.NIOROSHIGYOUSYA_CD.Text = string.Empty;
            this.control.NIOROSHIGYOUSYA_NAME_RYAKU.Text = string.Empty;
            this.control.NIOROSHIJYOUMEI_CD.Text = string.Empty;
            this.control.NIOROSHIJYOUMEI_NAME_RYAKU.Text = string.Empty;
            //CongBinh 20200331 #134988 E
            //伝票種類によって制御を変える
            if (!string.IsNullOrEmpty(this.form.txtNum_DenpyoKind.Text))
            {
                this.parentForm.bt_func4.Enabled = true;
                switch (int.Parse(this.form.txtNum_DenpyoKind.Text))
                {
                    //受入の場合
                    case DENPYOU_TYPE_UKEIRE:
                        // ヘッダータイトルラベル変更
                        this.headForm.lb_title.Text = DENSHU_KBN.UKEIRE_ICHIRAN.ToTitleString();
                        this.form.DenshuKbn = DENSHU_KBN.UKEIRE_ICHIRAN;
                        // 遷移画面フラグ切り替え
                        this.disp_Flg = DENPYOU_TYPE_UKEIRE;
                        this.parentForm.bt_func2.Enabled = true; //PhuocLoc 2021/05/05 #148576
                        // 検索条件の変更
                        this.control.NIDUMIGYOUSYA_CD.Enabled = false;
                        this.control.NIDUMIGYOUSYA_NAME_RYAKU.Enabled = false;
                        this.control.NIDUMIGENBA_CD.Enabled = false;
                        this.control.NIDUMIGENBA_NAME_RYAKU.Enabled = false;
                        this.control.NIOROSHIGYOUSYA_CD.Enabled = true;
                        this.control.NIOROSHIGYOUSYA_NAME_RYAKU.Enabled = true;
                        this.control.NIOROSHIJYOUMEI_CD.Enabled = true;
                        this.control.NIOROSHIJYOUMEI_NAME_RYAKU.Enabled = true;
                        break;
                    //出荷の場合
                    case DENPYOU_TYPE_SHUKKA:
                        // ヘッダータイトルラベル変更
                        this.headForm.lb_title.Text = DENSHU_KBN.SHUKKA_ICHIRAN.ToTitleString();
                        this.form.DenshuKbn = DENSHU_KBN.SHUKKA_ICHIRAN;
                        // 遷移画面フラグ切り替え
                        this.disp_Flg = DENPYOU_TYPE_SHUKKA;
                        this.parentForm.bt_func2.Enabled = true; //PhuocLoc 2021/05/05 #148576
                        // 検索条件の変更
                        this.control.NIDUMIGYOUSYA_CD.Enabled = true;
                        this.control.NIDUMIGYOUSYA_NAME_RYAKU.Enabled = true;
                        this.control.NIDUMIGENBA_CD.Enabled = true;
                        this.control.NIDUMIGENBA_NAME_RYAKU.Enabled = true;
                        this.control.NIOROSHIGYOUSYA_CD.Enabled = false;
                        this.control.NIOROSHIGYOUSYA_NAME_RYAKU.Enabled = false;
                        this.control.NIOROSHIJYOUMEI_CD.Enabled = false;
                        this.control.NIOROSHIJYOUMEI_NAME_RYAKU.Enabled = false;
                        // 検収系
                        this.KenshuJyoukyouTextChanged();
                        break;
                    //売上支払の場合
                    case DENPYOU_TYPE_URIAGESHIHARAI:
                        // ヘッダータイトルラベル変更
                        this.headForm.lb_title.Text = DENSHU_KBN.URIAGE_SHIHARAI_ICHIRAN.ToTitleString();
                        this.form.DenshuKbn = DENSHU_KBN.URIAGE_SHIHARAI_ICHIRAN;
                        // 遷移画面フラグ切り替え
                        this.disp_Flg = DENPYOU_TYPE_URIAGESHIHARAI;
                        this.parentForm.bt_func2.Enabled = true; //PhuocLoc 2021/05/05 #148576
                        // 検索条件の変更
                        this.control.NIDUMIGYOUSYA_CD.Enabled = true;
                        this.control.NIDUMIGYOUSYA_NAME_RYAKU.Enabled = true;
                        this.control.NIDUMIGENBA_CD.Enabled = true;
                        this.control.NIDUMIGENBA_NAME_RYAKU.Enabled = true;
                        this.control.NIOROSHIGYOUSYA_CD.Enabled = true;
                        this.control.NIOROSHIGYOUSYA_NAME_RYAKU.Enabled = true;
                        this.control.NIOROSHIJYOUMEI_CD.Enabled = true;
                        this.control.NIOROSHIJYOUMEI_NAME_RYAKU.Enabled = true;
                        break;
                    //代納の場合
                    case DENPYOU_TYPE_DAINOU:
                        // ヘッダータイトルラベル変更
                        this.headForm.lb_title.Text = DENSHU_KBN.DAINOU_ICHIRAN.ToTitleString();
                        this.form.DenshuKbn = DENSHU_KBN.DAINOU_ICHIRAN;
                        // 遷移画面フラグ切り替え
                        this.disp_Flg = DENPYOU_TYPE_DAINOU;
                        this.parentForm.bt_func2.Enabled = true; //PhuocLoc 2021/05/05 #148576
                        // 検索項目名の変更
                        this.control.TORIHIKISAKI_CD_LABEL.Text = "取引先（受入）";
                        this.control.GYOUSYA_CD_LABEL.Text = "業者（受入）";
                        this.control.GENNBA_CD_LABEL.Text = "現場（受入）";
                        // 検索条件の変更
                        this.control.NIDUMIGYOUSYA_CD.Enabled = false;
                        this.control.NIDUMIGYOUSYA_NAME_RYAKU.Enabled = false;
                        this.control.NIDUMIGENBA_CD.Enabled = false;
                        this.control.NIDUMIGENBA_NAME_RYAKU.Enabled = false;
                        this.control.NIOROSHIGYOUSYA_CD.Enabled = false;
                        this.control.NIOROSHIGYOUSYA_NAME_RYAKU.Enabled = false;
                        this.control.NIOROSHIJYOUMEI_CD.Enabled = false;
                        this.control.NIOROSHIJYOUMEI_NAME_RYAKU.Enabled = false;
                        //CongBinh 20200331 #134988 S
                        this.control.cmbShimebi.Enabled = false;
                        //this.control.cmbShihariaShimebi.Enabled = false; //CongBinh 20200513 #136890 
                        this.control.label1.Visible = true;
                        this.control.cmbDainoShimebi.Visible = true;
                        //CongBinh 20200331 #134988 E
                        break;
                    //PhuocLoc 2021/05/05 #148576 -Start
                    case DENPYOU_TYPE_SUBETE:
                        // ヘッダータイトルラベル変更
                        this.headForm.lb_title.Text = DENSHU_KBN.UKEIRE_SHUKKA_URSH_ICHIRAN.ToTitleString();
                        this.form.DenshuKbn = DENSHU_KBN.UKEIRE_SHUKKA_URSH_ICHIRAN;
                        // 遷移画面フラグ切り替え
                        this.disp_Flg = DENPYOU_TYPE_SUBETE;
                        this.parentForm.bt_func2.Enabled = false;
                        // 検索項目名の変更
                        this.control.TORIHIKISAKI_CD_LABEL.Text = "取引先";
                        this.control.GYOUSYA_CD_LABEL.Text = "業者";
                        this.control.GENNBA_CD_LABEL.Text = "現場";
                        // 検索条件の変更
                        this.control.NIDUMIGYOUSYA_CD.Enabled = true;
                        this.control.NIDUMIGYOUSYA_NAME_RYAKU.Enabled = true;
                        this.control.NIDUMIGENBA_CD.Enabled = true;
                        this.control.NIDUMIGENBA_NAME_RYAKU.Enabled = true;
                        this.control.NIOROSHIGYOUSYA_CD.Enabled = true;
                        this.control.NIOROSHIGYOUSYA_NAME_RYAKU.Enabled = true;
                        this.control.NIOROSHIJYOUMEI_CD.Enabled = true;
                        this.control.NIOROSHIJYOUMEI_NAME_RYAKU.Enabled = true;
                        break;
                    //PhuocLoc 2021/05/05 #148576 -End
                }
                // 検収系
                this.ChangeKenshuMustKbnVisible(int.Parse(this.form.txtNum_DenpyoKind.Text) == DENPYOU_TYPE_SHUKKA);
                if (int.Parse(this.form.txtNum_DenpyoKind.Text) != DENPYOU_TYPE_SHUKKA)
                {
                    this.ChangeKenshuJyoukyouVisible(false);
                }
                this.ChangeKenshuuDenpyouHidukeEnabled();
                if (int.Parse(this.form.txtNum_DenpyoKind.Text) != DENPYOU_TYPE_DAINOU)
                {
                    this.ChangeManifestButtonVisible(true);
                }
                else
                {
                    this.ChangeManifestButtonVisible(false);
                }
                this.form.PatternUpdate();
                // 代納用な項目制御設定
                this.SetDainouKoumokuStatus(int.Parse(this.form.txtNum_DenpyoKind.Text));
                // 伝票区分制御
                this.SetDenpoukbnStatus(int.Parse(this.form.txtNum_DenpyoKind.Text));
                this.parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(headForm.lb_title.Text);
            }
        }
        #endregion

        /// <summary>
        /// システム上必須な項目を非表示にします
        /// </summary>
        internal void HideColumnHeader()
        {
            foreach (DataGridViewColumn column in this.form.customDataGridView1.Columns)
            {
                if (column.Name.Equals(this.HIDDEN_DENPYOU_NUMBER) ||
                    column.Name.Equals(this.HIDDEN_SYSTEM_ID) ||
                    column.Name.Equals(this.HIDDEN_DENSHU_KBN_CD) ||
                    column.Name.Equals(this.HIDDEN_DETAIL_SYSTEM_ID) ||
                    column.Name.Equals("HST_GYOUSHA_CD_ERROR") ||
                    column.Name.Equals("HST_GENBA_CD_ERROR") ||
                    column.Name.Equals("HAIKI_SHURUI_CD_ERROR") ||
                    column.Name.Equals("HIDDEN_DENPYOU_SHURUI_CD") ||
                    column.Name.Equals("HIDDEN_SEARCH_SYSTEM_ID")
                    )
                {
                    column.Visible = false;
                }
            }
        }

        /// <summary>
        /// 現場チェック
        /// </summary>
        internal bool CheckGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.control.GENNBA_NAME_RYAKU.Text = string.Empty;
                this.control.GENNBA_NAME_RYAKU.ReadOnly = true;
                if (string.IsNullOrEmpty(this.control.GENNBA_CD.Text))
                {
                    this.control.GENNBA_NAME_RYAKU.Text = string.Empty;
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                if (string.IsNullOrEmpty(this.control.GYOUSYA_NAME_RYAKU.Text))
                {
                    // エラーメッセージ
                    if (this.form.txtNum_DenpyoKind.Text == "4")
                    {
                        msgLogic.MessageBoxShow("E051", "業者（受入）");
                    }
                    else
                    {
                        msgLogic.MessageBoxShow("E051", "業者");
                    }
                    this.control.GENNBA_CD.Text = string.Empty;
                    this.control.GENNBA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                var genbaEntityList = this.accessor.GetGenba(this.control.GENNBA_CD.Text);
                if (genbaEntityList == null || genbaEntityList.Length < 1)
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.control.GENNBA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                bool isContinue = false;
                M_GENBA genba = new M_GENBA();
                if (string.IsNullOrEmpty(this.control.TORIHIKISAKI_NAME_RYAKU.Text))
                {
                    if (string.IsNullOrEmpty(this.control.GYOUSYA_NAME_RYAKU.Text))
                    {
                        // エラーメッセージ
                        if (this.form.txtNum_DenpyoKind.Text == "4")
                        {
                            msgLogic.MessageBoxShow("E051", "業者（受入）");
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E051", "業者");
                        }
                        this.control.GENNBA_CD.Text = string.Empty;
                        this.control.GENNBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    foreach (M_GENBA genbaEntity in genbaEntityList)
                    {
                        if (this.control.GYOUSYA_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
                        {
                            isContinue = true;
                            genba = genbaEntity;
                            this.control.GENNBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
                            break;
                        }
                    }
                    if (!isContinue)
                    {
                        // 一致するものがないのでエラー
                        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                        msgLogic.MessageBoxShow("E020", "現場");
                        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                        this.control.GENNBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(this.control.GYOUSYA_NAME_RYAKU.Text))
                    {
                        // エラーメッセージ
                        if (this.form.txtNum_DenpyoKind.Text == "4")
                        {
                            msgLogic.MessageBoxShow("E051", "業者（受入）");
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E051", "業者");
                        }
                        this.control.GENNBA_CD.Text = string.Empty;
                        this.control.GENNBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                    foreach (M_GENBA genbaEntity in genbaEntityList)
                    {
                        if (this.control.GYOUSYA_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
                        {
                            isContinue = true;
                            genba = genbaEntity;
                            this.control.GENNBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
                            break;
                        }
                    }
                    if (!isContinue)
                    {
                        // 一致するものがないのでエラー
                        msgLogic.MessageBoxShow("E062", "業者");
                        this.control.GENNBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckGenba", ex2);
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
        /// 業者チェック
        /// </summary>
        internal bool CheckGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.control.GYOUSYA_NAME_RYAKU.Text = string.Empty;
                this.control.GYOUSYA_NAME_RYAKU.ReadOnly = true;
                if (string.IsNullOrEmpty(this.control.GYOUSYA_CD.Text))
                {
                    this.control.GENNBA_CD.Text = string.Empty;
                    this.control.GENNBA_NAME_RYAKU.Text = string.Empty;
                    // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                    BEFORE_GYOUSYA_CD = this.control.GYOUSYA_CD.Text;
                    // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                if (this.control.GYOUSYA_CD.Text != BEFORE_GYOUSYA_CD)
                {
                    this.control.GENNBA_CD.Text = string.Empty;
                    this.control.GENNBA_NAME_RYAKU.Text = string.Empty;
                    BEFORE_GYOUSYA_CD = this.control.GYOUSYA_CD.Text;
                }
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var gyoushaEntity = this.accessor.GetGyousha((this.control.GYOUSYA_CD.Text));
                if (gyoushaEntity == null)
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.control.GYOUSYA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                else
                {
                    this.control.GYOUSYA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                    // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                    BEFORE_GYOUSYA_CD = this.control.GYOUSYA_CD.Text;
                    // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckGyousha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGyousha", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 運搬業者チェック
        /// </summary>
        internal bool CheckUnpanGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.control.UNNBANGYOUSYA_NAME_RYAKU.Text = string.Empty;
                this.control.UNNBANGYOUSYA_NAME_RYAKU.ReadOnly = true;
                if (string.IsNullOrEmpty(this.control.UNNBANGYOUSYA_CD.Text))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var gyousha = this.accessor.GetGyousha(this.control.UNNBANGYOUSYA_CD.Text);
                if (gyousha == null)
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E020", "運搬業者");
                    this.control.UNNBANGYOUSYA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                // 事業場区分、荷卸現場区分チェック
                // 20151026 BUNN #12040 STR
                if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                // 20151026 BUNN #12040 END
                {
                    this.control.UNNBANGYOUSYA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    // 一致するデータがないのでエラー
                    msgLogic.MessageBoxShow("E020", "運搬業者");
                    this.control.UNNBANGYOUSYA_CD.Focus();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckUnpanGyousha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUnpanGyousha", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 荷降業者チェック
        /// </summary>
        internal bool CheckNioroshiGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.control.NIOROSHIGYOUSYA_NAME_RYAKU.Text = string.Empty;
                this.control.NIOROSHIGYOUSYA_NAME_RYAKU.ReadOnly = true;
                if (string.IsNullOrEmpty(this.control.NIOROSHIGYOUSYA_CD.Text))
                {
                    this.control.NIOROSHIJYOUMEI_CD.Text = string.Empty;
                    this.control.NIOROSHIJYOUMEI_NAME_RYAKU.Text = string.Empty;
                    // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                    BEFORE_NIOROSHIGYOUSYA_CD = this.control.NIOROSHIJYOUMEI_NAME_RYAKU.Text;
                    // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                if (this.control.NIOROSHIGYOUSYA_CD.Text != BEFORE_NIOROSHIGYOUSYA_CD)
                {
                    this.control.NIOROSHIJYOUMEI_CD.Text = string.Empty;
                    this.control.NIOROSHIJYOUMEI_NAME_RYAKU.Text = string.Empty;
                    BEFORE_NIOROSHIGYOUSYA_CD = this.control.NIOROSHIJYOUMEI_NAME_RYAKU.Text;
                }
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var gyousha = this.accessor.GetGyousha(this.control.NIOROSHIGYOUSYA_CD.Text);
                if (gyousha == null)
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.control.NIOROSHIGYOUSYA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                // 運搬受託者区分、処分受託者区分、荷卸現場区分チェック
                // 20151026 BUNN #12040 STR
                if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue || gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue)
                // 20151026 BUNN #12040 END
                {
                    this.control.NIOROSHIGYOUSYA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    // 一致するデータがないのでエラー
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.control.NIOROSHIGYOUSYA_CD.Focus();
                    // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                    BEFORE_NIOROSHIGYOUSYA_CD = this.control.NIOROSHIJYOUMEI_NAME_RYAKU.Text;
                    // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNioroshiGyousha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNioroshiGyousha", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 荷降場チェック
        /// </summary>
        internal bool CheckNioroshiba()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.control.NIOROSHIJYOUMEI_NAME_RYAKU.Text = string.Empty;
                this.control.NIOROSHIJYOUMEI_NAME_RYAKU.ReadOnly = true;
                if (string.IsNullOrEmpty(this.control.NIOROSHIJYOUMEI_CD.Text))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (string.IsNullOrEmpty(this.control.NIOROSHIGYOUSYA_NAME_RYAKU.Text))
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E051", "荷降業者");
                    this.control.NIOROSHIJYOUMEI_CD.Text = string.Empty;
                    this.control.NIOROSHIJYOUMEI_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                M_GENBA genba = new M_GENBA();
                M_GYOUSHA gyousha = new M_GYOUSHA();
                genba = this.accessor.GetGenba(this.control.NIOROSHIGYOUSYA_CD.Text, this.control.NIOROSHIJYOUMEI_CD.Text);
                // 荷降場チェック
                if (genba == null)
                {
                    // 一致するデータがないのでエラー
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.control.NIOROSHIJYOUMEI_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                // 積替え保管区分、処分事業場区分、最終処分場区分、荷卸現場区分チェック
                // 20151026 BUNN #12040 STR
                if (genba.TSUMIKAEHOKAN_KBN.IsTrue || genba.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || genba.SAISHUU_SHOBUNJOU_KBN.IsTrue)
                // 20151026 BUNN #12040 END
                {
                    this.control.NIOROSHIJYOUMEI_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;
                }
                else
                {
                    // 一致するデータがないのでエラー
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.control.NIOROSHIJYOUMEI_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckNioroshiba", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNioroshiba", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 荷積業者チェック
        /// </summary>
        internal bool CheckNidumiGyousya()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.control.NIDUMIGYOUSYA_NAME_RYAKU.Text = string.Empty;
                this.control.NIDUMIGYOUSYA_NAME_RYAKU.ReadOnly = true;
                if (string.IsNullOrEmpty(this.control.NIDUMIGYOUSYA_CD.Text))
                {
                    this.control.NIDUMIGENBA_CD.Text = string.Empty;
                    this.control.NIDUMIGENBA_NAME_RYAKU.Text = string.Empty;
                    // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                    BEFORE_NIDUMIGYOUSYA_CD = this.control.NIDUMIGYOUSYA_CD.Text;
                    // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                if (this.control.NIDUMIGYOUSYA_CD.Text != BEFORE_NIDUMIGYOUSYA_CD)
                {
                    this.control.NIDUMIGENBA_CD.Text = string.Empty;
                    this.control.NIDUMIGENBA_NAME_RYAKU.Text = string.Empty;
                    BEFORE_NIDUMIGYOUSYA_CD = this.control.NIDUMIGYOUSYA_CD.Text;
                }
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var gyousya = this.accessor.GetGyousha(this.control.NIDUMIGYOUSYA_CD.Text);
                if (gyousya == null)
                {
                    //エラーメッセージ
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.control.NIDUMIGYOUSYA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                // 排出事業者区分、荷積み現場区分、運搬受託者区分チェック
                // 20151026 BUNN #12040 STR
                if (gyousya.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue || gyousya.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                // 20151026 BUNN #12040 END
                {
                    this.control.NIDUMIGYOUSYA_NAME_RYAKU.Text = gyousya.GYOUSHA_NAME_RYAKU;
                    // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                    BEFORE_NIDUMIGYOUSYA_CD = this.control.NIDUMIGYOUSYA_CD.Text;
                    // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                }
                else
                {
                    // 一致するデータがないのでエラー
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.control.NIDUMIGYOUSYA_CD.Focus();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNidumiGyousya", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNidumiGyousya", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 荷積現場チェック
        /// </summary>
        internal bool CheckNidumiGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //初期化
                this.control.NIDUMIGENBA_NAME_RYAKU.Text = string.Empty;
                this.control.NIDUMIGENBA_NAME_RYAKU.ReadOnly = true;

                if (string.IsNullOrEmpty(this.control.NIDUMIGENBA_CD.Text))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (string.IsNullOrEmpty(this.control.NIDUMIGYOUSYA_NAME_RYAKU.Text))
                {
                    //エラーメッセージ
                    msgLogic.MessageBoxShow("E051", "荷積業者");
                    this.control.NIDUMIGENBA_CD.Text = string.Empty;
                    this.control.NIDUMIGENBA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                M_GENBA genba = new M_GENBA();
                M_GYOUSHA gyousha = new M_GYOUSHA();
                genba = this.accessor.GetGenba(this.control.NIDUMIGYOUSYA_CD.Text, this.control.NIDUMIGENBA_CD.Text);
                //荷積現場をチェック
                if (genba == null)
                {
                    //一致するデータがないのでエラー
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.control.NIDUMIGENBA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                // 排出事業場区分、積み替え保管区分、荷卸現場区分チェック
                // 20151026 BUNN #12040 STR
                if (genba.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue || genba.TSUMIKAEHOKAN_KBN.IsTrue)
                // 20151026 BUNN #12040 END
                {
                    this.control.NIDUMIGENBA_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;
                }
                else
                {
                    //一致するデータがないのでエラー
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.control.NIDUMIGENBA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNidumiGenba", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNidumiGenba", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;

        }

        /// <summary>
        /// 現場チェック
        /// </summary>
        internal bool CheckShukkaGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.control.SHUKKA_GENNBA_NAME_RYAKU.Text = string.Empty;
                this.control.SHUKKA_GENNBA_NAME_RYAKU.ReadOnly = true;

                if (string.IsNullOrEmpty(this.control.SHUKKA_GENNBA_CD.Text))
                {
                    this.control.SHUKKA_GENNBA_NAME_RYAKU.Text = string.Empty;
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (string.IsNullOrEmpty(this.control.SHUKKA_GYOUSYA_CD.Text))
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E051", "業者（出荷）");
                    this.control.SHUKKA_GENNBA_CD.Text = string.Empty;
                    this.control.SHUKKA_GENNBA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                var genbaEntityList = this.accessor.GetGenba(this.control.SHUKKA_GENNBA_CD.Text);
                if (genbaEntityList == null || genbaEntityList.Length < 1)
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.control.SHUKKA_GENNBA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                bool isContinue = false;
                M_GENBA genba = new M_GENBA();
                if (string.IsNullOrEmpty(this.control.SHUKKA_TORIHIKISAKI_NAME_RYAKU.Text))
                {
                    if (string.IsNullOrEmpty(this.control.SHUKKA_GYOUSYA_NAME_RYAKU.Text))
                    {
                        // エラーメッセージ
                        msgLogic.MessageBoxShow("E051", "業者");
                        this.control.SHUKKA_GENNBA_CD.Text = string.Empty;
                        this.control.SHUKKA_GENNBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    foreach (M_GENBA genbaEntity in genbaEntityList)
                    {
                        if (this.control.SHUKKA_GYOUSYA_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
                        {
                            isContinue = true;
                            genba = genbaEntity;
                            this.control.SHUKKA_GENNBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
                            break;
                        }
                    }
                    if (!isContinue)
                    {
                        // 一致するものがないのでエラー
                        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                        msgLogic.MessageBoxShow("E020", "現場");
                        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                        this.control.SHUKKA_GENNBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false ;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(this.control.SHUKKA_GYOUSYA_NAME_RYAKU.Text))
                    {
                        // エラーメッセージ
                        msgLogic.MessageBoxShow("E051", "業者");
                        this.control.SHUKKA_GENNBA_CD.Text = string.Empty;
                        this.control.SHUKKA_GENNBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                    foreach (M_GENBA genbaEntity in genbaEntityList)
                    {
                        if (this.control.SHUKKA_GYOUSYA_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
                        {
                            isContinue = true;
                            genba = genbaEntity;
                            this.control.SHUKKA_GENNBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
                            break;
                        }
                    }
                    if (!isContinue)
                    {
                        // 一致するものがないのでエラー
                        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                        msgLogic.MessageBoxShow("E020", "現場");
                        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                        this.control.SHUKKA_GENNBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckShukkaGenba", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckShukkaGenba", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 業者チェック
        /// </summary>
        internal bool CheckShukkaGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.control.SHUKKA_GYOUSYA_NAME_RYAKU.Text = string.Empty;
                this.control.SHUKKA_GYOUSYA_NAME_RYAKU.ReadOnly = true;
                if (string.IsNullOrEmpty(this.control.SHUKKA_GYOUSYA_CD.Text))
                {
                    this.control.SHUKKA_GENNBA_CD.Text = string.Empty;
                    this.control.SHUKKA_GENNBA_NAME_RYAKU.Text = string.Empty;
                    // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                    BEFORE_SHUKKA_GYOUSYA_CD = this.control.SHUKKA_GYOUSYA_CD.Text;
                    // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                if (this.control.SHUKKA_GYOUSYA_CD.Text != BEFORE_SHUKKA_GYOUSYA_CD)
                {
                    this.control.SHUKKA_GENNBA_CD.Text = string.Empty;
                    this.control.SHUKKA_GENNBA_NAME_RYAKU.Text = string.Empty;
                    BEFORE_SHUKKA_GYOUSYA_CD = this.control.SHUKKA_GYOUSYA_CD.Text;
                }
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var gyoushaEntity = this.accessor.GetGyousha((this.control.SHUKKA_GYOUSYA_CD.Text));
                if (gyoushaEntity == null)
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.control.SHUKKA_GYOUSYA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                else
                {
                    this.control.SHUKKA_GYOUSYA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                    // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                    BEFORE_SHUKKA_GYOUSYA_CD = this.control.SHUKKA_GYOUSYA_CD.Text;
                    // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckShukkaGyousha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckShukkaGyousha", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

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

        /// <summary>
        /// header設定
        /// </summary>
        /// /// <returns></returns>
        public void SetHeader(HeaderForm hs)
        {
            this.headForm = hs;
        }
        /// <summary>
        /// 入力画面表示
        /// </summary>
        /// <param name="wintype"></param>
        /// <param name="DenpyouNum"></param>
        private bool EditDetail(WINDOW_TYPE wintype, string denpyouNum, int denshuKbn)
        {
            try
            {
                long denpyo = -1;
                bool openPopup = false;
                if (!string.IsNullOrEmpty(denpyouNum))
                {
                    denpyo = long.Parse(denpyouNum);
                }
                string strFormId = "";
                switch (disp_Flg)
                {
                    case DENPYOU_TYPE_UKEIRE:
                        if (this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                        {
                            strFormId = "G051";
                        }
                        else
                        {
                            strFormId = "G721";
                        }
                        break;
                    case DENPYOU_TYPE_SHUKKA:
                        if (this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                        {
                            strFormId = "G053";
                        }
                        else
                        {
                            strFormId = "G722";
                        }
                        break;
                    case DENPYOU_TYPE_URIAGESHIHARAI:
                        strFormId = "G054";
                        break;
                    case DENPYOU_TYPE_DAINOU:
                        strFormId = "G161";
                        break;
                    //PhuocLoc 2021/05/05 #148576 -Start
                    case DENPYOU_TYPE_SUBETE:
                        switch (denshuKbn)
                        {
                            case DENPYOU_TYPE_UKEIRE:
                                if (this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                                {
                                    strFormId = "G051";
                                }
                                else
                                {
                                    strFormId = "G721";
                                }
                                break;
                            case DENPYOU_TYPE_SHUKKA:
                                if (this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                                {
                                    strFormId = "G053";
                                }
                                else
                                {
                                    strFormId = "G722";
                                }
                                break;
                            case DENPYOU_TYPE_URIAGESHIHARAI:
                                strFormId = "G054";
                                break;
                        }
                        //PhuocLoc 2021/05/05 #148576 -End
                        break;
                }
                if (!openPopup)
                {
                    switch (wintype)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            // 新規モードで起動
                            FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.NEW_WINDOW_FLAG);
                            break;
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            // 修正モードの権限チェック
                            if (Manager.CheckAuthority(strFormId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                            {
                                FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, denpyo);
                            }
                            // 参照モードの権限チェック
                            else if (Manager.CheckAuthority(strFormId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                            {
                                FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyo);
                            }
                            else
                            {
                                // 修正モードの権限なしのアラームを上げる
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShow("E158", "修正");
                            }
                            break;
                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            // 削除モードで起動
                            FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.DELETE_WINDOW_FLAG, WINDOW_TYPE.DELETE_WINDOW_FLAG, denpyo);
                            break;
                        case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                            // 複写モードで起動（新規モード）
                            FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, denpyo);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditDetail", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        internal void SetPrevStatus(object sender, EventArgs e)
        {
            InitialFlg = true; // No.2320
            //拠点、伝票日付From、伝票日付To、確定区分、伝票種類の項目をセッティングファイルに保存する。
            if (this.headForm.KYOTEN_CD.Text != "")
            {
                Properties.Settings.Default.SET_KYOTEN_CD = this.headForm.KYOTEN_CD.Text;
            }
            else
            {
                Properties.Settings.Default.SET_KYOTEN_CD = null;
            }
            Properties.Settings.Default.SET_HIDUKESENTAKU = this.headForm.txtNum_HidukeSentaku.Text;
            Properties.Settings.Default.SET_TORIHIKISAKI = this.control.TORIHIKISAKI_CD.Text;
            Properties.Settings.Default.SET_GYOUSYA = this.control.GYOUSYA_CD.Text;
            Properties.Settings.Default.SET_GENNBA = this.control.GENNBA_CD.Text;
            Properties.Settings.Default.SET_UNBANGYOUSYA = this.control.UNNBANGYOUSYA_CD.Text;
            Properties.Settings.Default.SET_NIOTOSUGYOUSYA = this.control.NIOROSHIGYOUSYA_CD.Text;
            Properties.Settings.Default.SET_NIOTOSUBA = this.control.NIOROSHIJYOUMEI_CD.Text;
            Properties.Settings.Default.SET_NIDUMIGYOUSYA = this.control.NIDUMIGYOUSYA_CD.Text;
            Properties.Settings.Default.SET_NIDUMIGENBA = this.control.NIDUMIGENBA_CD.Text;
            DateTime resultDt;
            if (!String.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text.Trim()) && DateTime.TryParse(this.headForm.HIDUKE_FROM.Text.Trim(), out resultDt))
            {
                Properties.Settings.Default.SET_HIDUKE_FROM = this.headForm.HIDUKE_FROM.Text;
            }
            if (!String.IsNullOrEmpty(this.headForm.HIDUKE_TO.Text.Trim()) && DateTime.TryParse(this.headForm.HIDUKE_TO.Text.Trim(), out resultDt))
            {
                Properties.Settings.Default.SET_HIDUKE_TO = this.headForm.HIDUKE_TO.Text;
            }
            Properties.Settings.Default.SET_DENPYO_KIND_CD = this.form.txtNum_DenpyoKind.Text;
            Properties.Settings.Default.SET_DENPYO_KBN_CD = this.form.txtNum_Denpyoukubun.Text;
            Properties.Settings.Default.SET_KENSHU_MUST_KBN = this.form.txtNum_KenshuMustKbn.Text;
            Properties.Settings.Default.SET_KENSHU_JYOUKYOU = this.form.txtNum_KenshuJyoukyou.Text;
            // 保存
            Properties.Settings.Default.Save();
        }
        #region 伝票区分表示制御

        /// <summary>
        /// 伝票区分表示制御
        /// </summary>
        /// <param name="denpyouType"></param>
        private void SetDenpoukbnStatus(int denpyouType)
        {
            switch (denpyouType)
            {
                // 代納
                case DENPYOU_TYPE_DAINOU:
                    this.form.txtNum_Denpyoukubun.Text = "3";
                    this.form.txtNum_Denpyoukubun.Tag = "【3】で入力してください";
                    this.form.radbtn_Uriage.Enabled = false;
                    this.form.radbtn_Shihari.Enabled = false;
                    break;
                default:
                    this.form.txtNum_Denpyoukubun.Tag = "【1～3】のいずれかで入力してください";
                    this.form.radbtn_Uriage.Enabled = true;
                    this.form.radbtn_Shihari.Enabled = true;
                    break;
            }
        }

        /// <summary>
        /// 代納用な項目制御設定
        /// </summary>
        /// <param name="denpyouType"></param>
        private void SetDainouKoumokuStatus(int denpyouType)
        {
            switch (denpyouType)
            {
                // 代納
                case DENPYOU_TYPE_DAINOU:
                    this.control.SHUKKA_TORIHIKISAKI_CD_LABEL.Visible = true;
                    this.control.SHUKKA_TORIHIKISAKI_CD.Visible = true;
                    this.control.SHUKKA_TORIHIKISAKI_NAME_RYAKU.Visible = true;
                    this.control.SHUKKA_GYOUSYA_CD_LABEL.Visible = true;
                    this.control.SHUKKA_GYOUSYA_CD.Visible = true;
                    this.control.SHUKKA_GYOUSYA_NAME_RYAKU.Visible = true;
                    this.control.SHUKKA_GENNBA_CD_LABEL.Visible = true;
                    this.control.SHUKKA_GENNBA_CD.Visible = true;
                    this.control.SHUKKA_GENNBA_NAME_RYAKU.Visible = true;
                    this.control.NIDUMIGYOUSYA_CD_LABEL.Visible = false;
                    this.control.NIDUMIGYOUSYA_CD.Visible = false;
                    this.control.NIDUMIGYOUSYA_NAME_RYAKU.Visible = false;
                    this.control.NIDUMIGENBA_CD_LABEL.Visible = false;
                    this.control.NIDUMIGENBA_CD.Visible = false;
                    this.control.NIDUMIGENBA_NAME_RYAKU.Visible = false;
                    this.control.NIOROSHIGYOUSYA_CD_LABEL.Visible = false;
                    this.control.NIOROSHIGYOUSYA_CD.Visible = false;
                    this.control.NIOROSHIGYOUSYA_NAME_RYAKU.Visible = false;
                    this.control.NIOROSHIJYOUMEI_CD_LABEL.Visible = false;
                    this.control.NIOROSHIJYOUMEI_CD.Visible = false;
                    this.control.NIOROSHIJYOUMEI_NAME_RYAKU.Visible = false;
                    break;

                default:
                    this.control.SHUKKA_TORIHIKISAKI_CD_LABEL.Visible = false;
                    this.control.SHUKKA_TORIHIKISAKI_CD.Visible = false;
                    this.control.SHUKKA_TORIHIKISAKI_NAME_RYAKU.Visible = false;
                    this.control.SHUKKA_GYOUSYA_CD_LABEL.Visible = false;
                    this.control.SHUKKA_GYOUSYA_CD.Visible = false;
                    this.control.SHUKKA_GYOUSYA_NAME_RYAKU.Visible = false;
                    this.control.SHUKKA_GENNBA_CD_LABEL.Visible = false;
                    this.control.SHUKKA_GENNBA_CD.Visible = false;
                    this.control.SHUKKA_GENNBA_NAME_RYAKU.Visible = false;
                    this.control.NIDUMIGYOUSYA_CD_LABEL.Visible = true;
                    this.control.NIDUMIGYOUSYA_CD.Visible = true;
                    this.control.NIDUMIGYOUSYA_NAME_RYAKU.Visible = true;
                    this.control.NIDUMIGENBA_CD_LABEL.Visible = true;
                    this.control.NIDUMIGENBA_CD.Visible = true;
                    this.control.NIDUMIGENBA_NAME_RYAKU.Visible = true;
                    this.control.NIOROSHIGYOUSYA_CD_LABEL.Visible = true;
                    this.control.NIOROSHIGYOUSYA_CD.Visible = true;
                    this.control.NIOROSHIGYOUSYA_NAME_RYAKU.Visible = true;
                    this.control.NIOROSHIJYOUMEI_CD_LABEL.Visible = true;
                    this.control.NIOROSHIJYOUMEI_CD.Visible = true;
                    this.control.NIOROSHIJYOUMEI_NAME_RYAKU.Visible = true;
                    break;
            }
        }
        #endregion

        #region 検収入力関係
        /// <summary>
        /// 検収有無が変更されたときの処理
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void txtNum_KenshuMustKbn_TextChanged(object sender, EventArgs e)
        {
            this.KenshuJyoukyouTextChanged();
            // 検収伝票日付の活性制御
            this.ChangeKenshuuDenpyouHidukeEnabled();
        }

        /// <summary>
        /// 検収状況のTextChangedイベント
        /// </summary>
        private void KenshuJyoukyouTextChanged()
        {
            // 検収状況は1桁入力であるため、TextChangedイベントで問題ないはず
            if (!DENPYOU_TYPE_SHUKKA.ToString().Equals(this.form.txtNum_DenpyoKind.Text))
            {
                this.ChangeKenshuJyoukyouVisible(false);
            }
            if (KENSHU_MUST_KBN_TRUE.ToString().Equals(this.form.txtNum_KenshuMustKbn.Text))
            {
                this.ChangeKenshuJyoukyouVisible(true);
            }
            else
            {
                this.ChangeKenshuJyoukyouVisible(false);
            }
        }

        /// <summary>
        /// 検収有無の活性状態を変更する
        /// </summary>
        /// <param name="isVisible">true：表示、false：非表示</param>
        private void ChangeKenshuMustKbnVisible(bool isVisible)
        {
            this.form.label2.Visible = isVisible;
            this.form.customPanel3.Visible = isVisible;
        }

        /// <summary>
        /// 検収状況の活性状態を変更する
        /// </summary>
        /// <param name="isEnabled">true：表示、false：非表示</param>
        private void ChangeKenshuJyoukyouVisible(bool isEnabled)
        {
            // 検収状況
            this.form.label4.Visible = isEnabled;
            this.form.customPanel4.Visible = isEnabled;
        }
        #endregion

        #region 検収伝票日付表示制御
        /// <summary>
        /// 検収伝票日付の活性状態を変える
        /// </summary>
        private void ChangeKenshuuDenpyouHidukeEnabled()
        {
            bool isEnabled = (DENPYOU_TYPE_SHUKKA.ToString().Equals(this.form.txtNum_DenpyoKind.Text) && this.form.radbtn_True.Checked);
            // ラジオボタンの制御
            this.headForm.radbtnKenshuHiduke.Enabled = isEnabled;

            // 値変更
            if (!isEnabled)
            {
                char[] limitList = { '1', '2' };
                if (this.headForm.radbtnKenshuHiduke.Checked)
                {
                    // 非活性対象が選択状態であればデフォルトへ戻す
                    this.headForm.radbtnDenpyouHiduke.Checked = true;
                }
            }
            else
            {
                char[] limitList = { '1', '2', '3' };
            }
        }
        #endregion

        #region 必須チェックエラーフォーカス処理
        /// <summary>
        /// 必須チェックエラーフォーカス処理
        /// </summary>
        /// <returns></returns>
        private void SetErrorFocus()
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
            foreach (Control control in this.headForm.allControl)
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
        #endregion 必須チェックエラーフォーカス処理
              
        // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            this.headForm.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.headForm.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
            // 入力されない場合
            if (string.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.headForm.HIDUKE_TO.Text))
            {
                return false;
            }
            DateTime date_from = DateTime.Parse(this.headForm.HIDUKE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.headForm.HIDUKE_TO.Text);
            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.headForm.HIDUKE_FROM.IsInputErrorOccured = true;
                this.headForm.HIDUKE_TO.IsInputErrorOccured = true;
                this.headForm.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                this.headForm.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                if (this.headForm.radbtnDenpyouHiduke.Checked)
                {
                    string[] errorMsg = { "伝票日付From", "伝票日付To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.headForm.radbtnNyuuryokuHiduke.Checked)
                {
                    string[] errorMsg = { "入力日付From", "入力日付To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else
                {
                    string[] errorMsg = { "検収伝票日付From", "検収伝票日付To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                this.headForm.HIDUKE_FROM.Focus();
                return true;
            }
            return false;
        }
        #endregion
        // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 end

        /// 20141023 Houkakou 「運賃一覧」のダブルクリックを追加する　start
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            var FromTextBox = this.headForm.HIDUKE_FROM;
            var ToTextBox = this.headForm.HIDUKE_TO;
            ToTextBox.Text = FromTextBox.Text;
            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141023 Houkakou 「運賃一覧」のダブルクリックを追加する　end

        // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        internal void GYOUSHA_CD_After()
        {
            if (this.control.GYOUSYA_CD.Text != BEFORE_GYOUSYA_CD)
            {
                this.control.GENNBA_CD.Text = string.Empty;
                this.control.GENNBA_NAME_RYAKU.Text = string.Empty;
                BEFORE_GYOUSYA_CD = this.control.GYOUSYA_CD.Text;
            }
        }

        internal void NIOROSHIGYOUSYA_CD_After()
        {
            if (this.control.NIOROSHIGYOUSYA_CD.Text != BEFORE_NIOROSHIGYOUSYA_CD)
            {
                this.control.NIOROSHIJYOUMEI_CD.Text = string.Empty;
                this.control.NIOROSHIJYOUMEI_NAME_RYAKU.Text = string.Empty;
                BEFORE_NIOROSHIGYOUSYA_CD = this.control.NIOROSHIJYOUMEI_NAME_RYAKU.Text;
            }
        }

        internal void SHUKKA_GYOUSHA_CD_After()
        {
            if (this.control.SHUKKA_GYOUSYA_CD.Text != BEFORE_SHUKKA_GYOUSYA_CD)
            {
                this.control.SHUKKA_GENNBA_CD.Text = string.Empty;
                this.control.SHUKKA_GENNBA_NAME_RYAKU.Text = string.Empty;
                BEFORE_SHUKKA_GYOUSYA_CD = this.control.SHUKKA_GYOUSYA_CD.Text;
            }
        }

        internal void NIDUMIGYOUSYA_CD_After()
        {
            if (this.control.NIDUMIGYOUSYA_CD.Text != BEFORE_NIDUMIGYOUSYA_CD)
            {
                this.control.NIDUMIGENBA_CD.Text = string.Empty;
                this.control.NIDUMIGENBA_NAME_RYAKU.Text = string.Empty;
                BEFORE_NIDUMIGYOUSYA_CD = this.control.NIDUMIGYOUSYA_CD.Text;
            }
        }

        internal void GENBA_CD_After()
        {
            BEFORE_GYOUSYA_CD = this.control.GYOUSYA_CD.Text;
        }

        internal void NIOROSHIGENBA_CD_After()
        {
            BEFORE_NIOROSHIGYOUSYA_CD = this.control.NIOROSHIGYOUSYA_CD.Text;
        }

        internal void SHUKKA_GENBA_CD_After()
        {
            BEFORE_SHUKKA_GYOUSYA_CD = this.control.SHUKKA_GYOUSYA_CD.Text;
        }

        internal void NIDUMIGENBA_CD_After()
        {
            BEFORE_NIDUMIGYOUSYA_CD = this.control.NIDUMIGYOUSYA_CD.Text;
        }
        // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

        //thongh 2015/09/14 #13030 start
        /// <summary>
        /// 伝票日付初期値設定
        /// </summary>
        private bool SetDenpyouHidukeInit()
        {
            try
            {
                //headerFormにSettingsの値            
                if (string.IsNullOrEmpty(Properties.Settings.Default.SET_HIDUKE_TO) || InitialFlg == false) // No.2320
                {
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    this.headForm.HIDUKE_TO.Value = this.parentForm.sysDate;
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                }
                else
                {
                    this.headForm.HIDUKE_TO.Value = Convert.ToDateTime(Properties.Settings.Default.SET_HIDUKE_TO.ToString());
                }
                if (string.IsNullOrEmpty(Properties.Settings.Default.SET_HIDUKE_FROM) || InitialFlg == false) // No.2320
                {
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    this.headForm.HIDUKE_FROM.Value = this.parentForm.sysDate;
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                }
                else
                {
                    this.headForm.HIDUKE_FROM.Value = Convert.ToDateTime(Properties.Settings.Default.SET_HIDUKE_FROM.ToString());
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDenpyouHidukeInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }
        //thongh 2015/09/14 #13030 end

        #region CongBinh 20200331 #134988
        public M_TORIHIKISAKI[] GetTorihikisakiInfo(string toriCd, out bool catchErr)
        {
            LogUtility.DebugMethodStart(toriCd);
            var toriEntitys = new M_TORIHIKISAKI[0];
            catchErr = true;
            try
            {
                var tDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                var toriEntity = new M_TORIHIKISAKI();
                toriEntity.TORIHIKISAKI_CD = toriCd;
                toriEntity.ISNOT_NEED_DELETE_FLG = true;
                // エンティティから取引先情報を絞り込んで取得
                toriEntitys = tDao.GetAllValidData(toriEntity);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetTorihikisakiInfo", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisakiInfo", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(toriEntitys, catchErr);
            }
            return toriEntitys;
        }
        public bool CheckShimebi(string toriCd, bool isShukka = false)
        {
            LogUtility.DebugMethodStart(toriCd, isShukka);
            var seikyuEntity = new M_TORIHIKISAKI_SEIKYUU();
            var shiharaiEntity = new M_TORIHIKISAKI_SHIHARAI();
            var ret = false;
            try
            {
                var seDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
                var shDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
                // 取引先CDを元に取引請求及び取引先支払マスタのエンティティを取得
                seikyuEntity = seDao.GetDataByCd(toriCd);
                shiharaiEntity = shDao.GetDataByCd(toriCd);

                if (isShukka)
                {  // 取引先請求でチェック
                    if (seikyuEntity != null &&
                        ((!seikyuEntity.SHIMEBI1.IsNull && seikyuEntity.SHIMEBI1.ToString() == this.control.cmbDainoShimebi.Text) ||
                         (!seikyuEntity.SHIMEBI2.IsNull && seikyuEntity.SHIMEBI2.ToString() == this.control.cmbDainoShimebi.Text) ||
                         (!seikyuEntity.SHIMEBI3.IsNull && seikyuEntity.SHIMEBI3.ToString() == this.control.cmbDainoShimebi.Text)))
                    {
                        ret = true;
                    }
                }
                else
                {
                    // 取引先請求でチェック
                    if (seikyuEntity != null &&
                        ((!seikyuEntity.SHIMEBI1.IsNull && seikyuEntity.SHIMEBI1.ToString() == this.control.cmbShimebi.Text) ||
                         (!seikyuEntity.SHIMEBI2.IsNull && seikyuEntity.SHIMEBI2.ToString() == this.control.cmbShimebi.Text) ||
                         (!seikyuEntity.SHIMEBI3.IsNull && seikyuEntity.SHIMEBI3.ToString() == this.control.cmbShimebi.Text)))
                    {
                        ret = true;
                    }
                    // 取引先支払でチャック
                    if (shiharaiEntity != null &&
                       ((!shiharaiEntity.SHIMEBI1.IsNull && shiharaiEntity.SHIMEBI1.ToString() == this.control.cmbShihariaShimebi.Text) ||
                       (!shiharaiEntity.SHIMEBI2.IsNull && shiharaiEntity.SHIMEBI2.ToString() == this.control.cmbShihariaShimebi.Text) ||
                       (!shiharaiEntity.SHIMEBI3.IsNull && shiharaiEntity.SHIMEBI3.ToString() == this.control.cmbShihariaShimebi.Text)))
                    {
                        ret = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckShimebi", ex1);
                this.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckShimebi", ex);
                this.errmessage.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        private void TORIHIKISAKI_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                string pToriCd = this.control.TORIHIKISAKI_CD.Text.ToString().Trim();
                if (pToriCd != "")
                {
                    pToriCd = pToriCd.PadLeft(6, '0');
                }
                else
                {
                    this.control.TORIHIKISAKI_NAME_RYAKU.Clear();
                    return;
                }
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                bool bRet = true;
                bool catchErr = true;
                var ret = this.GetTorihikisakiInfo(pToriCd, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (ret.Length == 0)
                {
                    this.control.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                    this.control.TORIHIKISAKI_NAME_RYAKU.Clear();
                    msgLogic.MessageBoxShow("E020", "取引先");
                    bRet = false;
                }
                if (bRet)
                {
                    bool isOk = this.CheckShimebi(pToriCd);
                    if (!catchErr)
                    {
                        return;
                    }
                    if ((string.IsNullOrEmpty(this.control.cmbShimebi.Text) && string.IsNullOrEmpty(this.control.cmbShihariaShimebi.Text)) || isOk)
                    {
                        this.control.TORIHIKISAKI_NAME_RYAKU.Text = ret[0].TORIHIKISAKI_NAME_RYAKU;
                    }
                    else
                    {
                        this.control.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                        this.control.TORIHIKISAKI_NAME_RYAKU.Clear();
                        msgLogic.MessageBoxShow("E062", "締日");
                        bRet = false;
                    }
                }
                if (!bRet)
                {
                    this.control.TORIHIKISAKI_CD.SelectAll();
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TORIHIKISAKI_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        private void SHUKKA_TORIHIKISAKI_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                string pToriCd = this.control.SHUKKA_TORIHIKISAKI_CD.Text.ToString().Trim();
                if (pToriCd != "")
                {
                    pToriCd = pToriCd.PadLeft(6, '0');
                }
                else
                {
                    this.control.SHUKKA_TORIHIKISAKI_NAME_RYAKU.Clear();
                    return;
                }
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                bool bRet = true;
                bool catchErr = true;
                var ret = this.GetTorihikisakiInfo(pToriCd, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (ret.Length == 0)
                {
                    this.control.SHUKKA_TORIHIKISAKI_CD.IsInputErrorOccured = true;
                    this.control.SHUKKA_TORIHIKISAKI_NAME_RYAKU.Clear();
                    msgLogic.MessageBoxShow("E020", "取引先");
                    bRet = false;
                }
                if (bRet)
                {
                    bool isOk = this.CheckShimebi(pToriCd, true);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (string.IsNullOrEmpty(this.control.cmbDainoShimebi.Text) || isOk)
                    {
                        this.control.SHUKKA_TORIHIKISAKI_NAME_RYAKU.Text = ret[0].TORIHIKISAKI_NAME_RYAKU;
                    }
                    else
                    {
                        this.control.SHUKKA_TORIHIKISAKI_CD.IsInputErrorOccured = true;
                        this.control.SHUKKA_TORIHIKISAKI_NAME_RYAKU.Clear();
                        msgLogic.MessageBoxShow("E062", "締日");
                        bRet = false;
                    }
                }
                if (!bRet)
                {
                    this.control.SHUKKA_TORIHIKISAKI_CD.SelectAll();
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHUKKA_TORIHIKISAKI_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        private void cmbShihariaShimebi_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.control.TORIHIKISAKI_CD.Text = string.Empty;
            this.control.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.control.cmbShimebi.SelectedIndexChanged -= new EventHandler(cmbShimebi_SelectedIndexChanged);
            this.control.cmbShimebi.SelectedIndex = 0;
            this.control.cmbShimebi.SelectedIndexChanged += new EventHandler(cmbShimebi_SelectedIndexChanged);
        }
        private void cmbShimebi_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.control.TORIHIKISAKI_CD.Text = string.Empty;
            this.control.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.control.cmbShihariaShimebi.SelectedIndexChanged -= new EventHandler(cmbShihariaShimebi_SelectedIndexChanged);
            this.control.cmbShihariaShimebi.SelectedIndex = 0;
            this.control.cmbShihariaShimebi.SelectedIndexChanged += new EventHandler(cmbShihariaShimebi_SelectedIndexChanged);
        }
        void cmbDainoShimebi_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.control.SHUKKA_TORIHIKISAKI_CD.Text = string.Empty;
            this.control.SHUKKA_TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
        }
        #endregion

        #region (産廃, 積替, 建廃)マニフェスト
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isVisible"></param>
        private void ChangeManifestButtonVisible(bool isVisible)
        {
            parentForm.bt_process3.Enabled = isVisible;
            parentForm.bt_process4.Enabled = isVisible;
            parentForm.bt_process5.Enabled = isVisible;

            if (this.CheckAuthorityManifest("G119"))
            {
                parentForm.bt_process3.Enabled = false;
            }
            if (this.CheckAuthorityManifest("G120"))
            {
                parentForm.bt_process4.Enabled = false;
            }
            if (this.CheckAuthorityManifest("G121"))
            {
                parentForm.bt_process5.Enabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        private bool CheckAuthorityManifest(string formId)
        {
            bool isOk = false;
            if (!r_framework.Authority.Manager.CheckAuthority(formId, WINDOW_TYPE.NEW_WINDOW_FLAG, false))
            {
                isOk = true;
            }
            return isOk;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_process3_Click(object sender, System.EventArgs e)
        {
            this.OpenManifest("G119");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_process4_Click(object sender, System.EventArgs e)
        {
            this.OpenManifest("G120");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_process5_Click(object sender, System.EventArgs e)
        {
            this.OpenManifest("G121");
        }

        /// <summary>
        /// 
        /// </summary>
        private void OpenManifest(string formId)
        {
            if (this.form.customDataGridView1.RowCount > 0
                && !string.IsNullOrEmpty(this.form.txtNum_DenpyoKind.Text))
            {
                string stringMode = "1";
                if (this.form.txtNum_DenpyoKind.Text.Equals("2"))
                {
                    stringMode = "2";
                }
                else if (this.form.txtNum_DenpyoKind.Text.Equals("3"))
                {
                    stringMode = "3";
                }
                //PhuocLoc 2021/05/05 #148576 -Start
                else if (this.form.txtNum_DenpyoKind.Text.Equals("5"))
                {
                    stringMode = this.form.customDataGridView1.CurrentRow.Cells[this.HIDDEN_DENSHU_KBN_CD].Value.ToString();
                }
                //PhuocLoc 2021/05/05 #148576 -End

                var currentRow = this.form.customDataGridView1.CurrentRow;
                string systemId = this.form.customDataGridView1.Rows[currentRow.Index].Cells["HIDDEN_SEARCH_SYSTEM_ID"].Value.ToString();
                string denpyouNumber = this.form.customDataGridView1.Rows[currentRow.Index].Cells["HIDDEN_DENPYOU_NUMBER"].Value.ToString();

                object[] args = new object[5];
                args[0] = WINDOW_TYPE.NEW_WINDOW_FLAG;
                args[1] = stringMode;
                args[2] = systemId;
                args[3] = "";
                args[4] = 1;
                if (this.IsDenshiManifest(denpyouNumber, stringMode))
                {
                    r_framework.FormManager.FormManager.OpenFormWithAuth(formId, WINDOW_TYPE.NEW_WINDOW_FLAG, args);
                }
                else
                {
                    this.errmessage.MessageBoxShow("E295");
                }
            }
            else
            {
                this.errmessage.MessageBoxShow("E296");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="systemId"></param>
        /// <returns></returns>
        private bool IsDenshiManifest(string denpyouNumber, string denshuKbn)
        {
            bool isOk = false;
            if (!string.IsNullOrEmpty(denpyouNumber))
            {
                if (denshuKbn.Equals("1"))
                {
                    T_UKEIRE_ENTRY ukeireEntity = ukeirek_entry_daocls.GetDataByCd(SqlInt64.Parse(denpyouNumber));
                    if (ukeireEntity != null && (ukeireEntity.MANIFEST_SHURUI_CD.IsNull || ukeireEntity.MANIFEST_SHURUI_CD.Value == 1))
                    {
                        isOk = true;
                    }
                }
                else if (denshuKbn.Equals("2"))
                {
                    T_SHUKKA_ENTRY shukkaEntity = shukkak_entry_daocls.GetDataByCd(SqlInt64.Parse(denpyouNumber));
                    if (shukkaEntity != null && (shukkaEntity.MANIFEST_SHURUI_CD.IsNull || shukkaEntity.MANIFEST_SHURUI_CD.Value == 1))
                    {
                        isOk = true;
                    }
                }
                else if (denshuKbn.Equals("3"))
                {
                    T_UR_SH_ENTRY urShEntity = ur_shk_entry_daocls.GetDataByCd(SqlInt64.Parse(denpyouNumber));
                    if (urShEntity != null && (urShEntity.MANIFEST_SHURUI_CD.IsNull || urShEntity.MANIFEST_SHURUI_CD.Value == 1))
                    {
                        isOk = true;
                    }
                }
            }
            return isOk;
        }
        #endregion
    }
}