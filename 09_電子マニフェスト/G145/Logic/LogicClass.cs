using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.ElectronicManifest.CustomControls_Ex;
using Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.DAO;
using Shougun.Core.Message;
using Seasar.Dao;
using r_framework.Dao;
using Seasar.Framework.Exceptions;
using r_framework.Authority;

namespace Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku
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
        private string ButtonInfoXmlPath = "Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// DAO
        /// </summary>
        //public GetMSIDaoCls dao_GetMSI;
        private GetTMEDaoCls dao_GetTME;

        /// <summary>
        /// DAO
        /// </summary>
        //廃棄物種類チェック
        private DAOClass_CheckHaiki Dao_CheckHaiki;

        /// <summary>
        /// DAO
        /// </summary>
        //排出事業者チェック
        private DAOClass_CheckJigyousya Dao_CheckJigyousya;

        /// <summary>
        /// DAO
        /// </summary>
        //廃棄物種類チェック
        private DAOClass_CheckUnpan Dao_CheckUnpan;

        /// <summary>
        /// DAO
        /// </summary>
        //QueInfo
        private DAOClass_QUE Dao_QUE;

        /// <summary>
        /// DAO
        /// </summary>
        //QueInfo
        private DAOClass_DT_D10 Dao_D10;

        /// <summary>
        /// DAO
        /// </summary>
        //QueInfo
        private DAOClass_DT_D10_MOD Dao_D10_MOD;

        /// <summary>
        /// DAO
        /// </summary>
        //QueInfo
        private DAOClass_DT_MF_TOC Dao_DT_MF_TOC;

        /// <summary>
        /// DAO
        /// </summary>
        //廃棄物種類チェック
        private DAOClass_CheckUnpansha Dao_CheckUnpansha;

        // Popup営業担当者
        public DAOClass_PopupHaiki Dao_PopupHaiki;

        // Popup営業担当者
        public DAOClass_PopupJigyousya Dao_PopupJigyousya;

        // Popup営業担当者
        public DAOClass_PopupUnpan Dao_PopupUnpan;

        // Popup営業担当者
        public DAOClass_PopupUnpanSha Dao_PopupUnpanSha;

        /// <summary>
        /// フィルタ加入者番号
        /// </summary>
        private ImportMemberFilterDaoClas ImportMemberFilterDao;

        /// <summary>
        /// DTO
        /// </summary>
        //private MSIDtoCls dto_MSI;
        private TMEDtoCls dto_TME;

        /// <summary>
        /// Form
        /// </summary>
        private UIHeader header;
        private UIForm form;
        private BusinessBaseForm footer;

        //Queテーブルの管理番号とレコード枝番を保存する
        Hashtable Que_Kanri_Seq;
        private int[] array;

        // 検索条件の処理区分を保持する
        private string searchSyoriKubunCD;

        /// <summary>
        /// 処理区分：1.終了報告
        /// </summary>
        private static readonly string SyoriKubunShuryo = "1";

        /// <summary>
        /// 処理区分：2.終了報告の送信保留
        /// </summary>
        private static readonly string SyoriKubunHoryu = "2";

        /// <summary>
        /// 処理区分：3.終了報告の修正・取消
        /// </summary>
        private static readonly string SyoriKubunShusei = "3";

        // 他社ＥＤＩ使用:3.全て
        private static readonly string TASHAEDI_DEFUALT = "3";

        /// <summary>
        /// 作成したSQL
        /// </summary>
        public string createSql { get; set; }

        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
        internal BusinessBaseForm parentForm;
        // 20150922 katen #12048 「システム日付」の基準作成、適用 end

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        /// <summary>
        /// 電マニのフィルタモード
        /// </summary>
        private SqlInt16 SYS_INFO_COPY_MODE = 0;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果(システム設定)
        /// </summary>
        public DataTable Search_MSI { get; set; }

        /// <summary>
        /// SELECT句
        /// </summary>
        internal string selectQuery { get; set; }

        /// <summary>
        /// ORDERBY句
        /// </summary>
        internal string orderByQuery { get; set; }

        /// <summary>
        /// JOIN句
        /// </summary>
        internal string joinQuery { get; set; }

        /// <summary>
        /// 検索結果(マニフェストパターン)
        /// </summary>
        public DataTable Search_TME { get; set; }

        /// <summary>
        /// List<Bean>
        /// </summary>
        public List<QUE_INFO> ListQue { get; set; }
        public List<DT_D10> ListDT_D10_IN { get; set; }
        public List<DT_D10> ListDT_D10_UP { get; set; }
        public List<DT_D10> ListDT_D10_DEL { get; set; }
        public List<DT_MF_TOC> ListDT_MF { get; set; }
        public List<DT_D10_MOD> ListDT_D10_MOD_IN { get; set; }
        public List<DT_D10_MOD> ListDT_D10_MOD_UP { get; set; }
        public List<DT_D10_MOD> ListDT_D10_MOD_DEL { get; set; }

        /// <summary>
        /// Bean
        /// </summary>
        private QUE_INFO Cls_QUE;
        private DT_MF_TOC Cls_Dt_Mf_Toc;
        private DT_D10 Cls_Dt_D10;
        private DT_D10_MOD Cls_Dt_D10_Mod;

        /// <summary>
        /// Hashtable
        /// </summary>
        private Hashtable ResultTable;

        private Control[] allControl;

        // Popup営業担当者
        public DAOClass_PopupUpnTantou Dao_PopupUpnTantou;

        // Popup営業担当者
        public DAOClass_PopupRepTantou Dao_PopupRepTantou;

        // Popup営業担当者
        public DAOClass_PopupUpnTani Dao_PopupUpnTani;

        // Popup営業担当者
        public DAOClass_PopupYuuTani Dao_PopupYuuTani;

        // Popup営業担当者
        public DAOClass_PopupSharyo Dao_PopupSharyo;

        // 電子担当者(運搬)チェックDAO
        public DAOClass_SearchUpnName Dao_SearchUpnName;

        // 電子担当者(報告)チェックDAO
        public DAOClass_SearchRepName Dao_SearchRepName;

        // 電子担当者(処分)チェックDAO
        public DAOClass_SearchSyobunName Dao_SearchSyobunName;

        // 単位チェックDAO
        public DAOClass_SearchTaniName Dao_SearchTaniName;

        // 車輌チェックDAO
        public DAOClass_SearchSharyoName Dao_SearchSharyoName;

        //廃棄物区分CD（初期値：1 産廃（直行））
        public String HaikiKbnCD = "1";

        //初期表示フラグ
        public Boolean First_Flg = false;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            //DTO
            this.dto_TME = new TMEDtoCls();

            //DAO
            this.dao_GetTME = DaoInitUtility.GetComponent<DAO.GetTMEDaoCls>();
            this.Dao_CheckHaiki = DaoInitUtility.GetComponent<DAO.DAOClass_CheckHaiki>();
            this.Dao_CheckJigyousya = DaoInitUtility.GetComponent<DAO.DAOClass_CheckJigyousya>();
            this.Dao_CheckUnpan = DaoInitUtility.GetComponent<DAO.DAOClass_CheckUnpan>();
            this.Dao_CheckUnpansha = DaoInitUtility.GetComponent<DAO.DAOClass_CheckUnpansha>();
            this.Dao_QUE = DaoInitUtility.GetComponent<DAO.DAOClass_QUE>();
            this.Dao_D10 = DaoInitUtility.GetComponent<DAO.DAOClass_DT_D10>();
            this.Dao_D10_MOD = DaoInitUtility.GetComponent<DAO.DAOClass_DT_D10_MOD>();
            this.Dao_DT_MF_TOC = DaoInitUtility.GetComponent<DAO.DAOClass_DT_MF_TOC>();
            this.Dao_SearchUpnName = DaoInitUtility.GetComponent<DAO.DAOClass_SearchUpnName>();
            this.Dao_SearchRepName = DaoInitUtility.GetComponent<DAO.DAOClass_SearchRepName>();
            this.Dao_SearchSyobunName = DaoInitUtility.GetComponent<DAO.DAOClass_SearchSyobunName>();
            this.Dao_SearchTaniName = DaoInitUtility.GetComponent<DAO.DAOClass_SearchTaniName>();
            this.Dao_SearchSharyoName = DaoInitUtility.GetComponent<DAO.DAOClass_SearchSharyoName>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            this.ImportMemberFilterDao = DaoInitUtility.GetComponent<ImportMemberFilterDaoClas>();
            this.ResultTable = new Hashtable();

            this.Search_TME = new DataTable();

            // 電マニ代行登録用処理
            var sysInfos = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllData();
            this.SYS_INFO_COPY_MODE = sysInfos != null && sysInfos.Count() > 0 && !sysInfos[0].COPY_MODE.IsNull ? sysInfos[0].COPY_MODE : 0;


            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            UIHeader targetHeader = (UIHeader)parentForm.headerForm;
            this.header = targetHeader;
            this.header.lb_title.Text = "　　処分終了報告　　";

            //フッターの初期化
            BusinessBaseForm targetFooter = (BusinessBaseForm)parentForm;
            this.footer = targetFooter;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();

            //親フォームのボタン表示
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

            LogUtility.DebugMethodEnd();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;
            //受渡確認ボタン(F1)イベント生成
            parentForm.bt_func1.Click += new EventHandler(this.form.bt_func1_Click);

            //保留削除(F4)イベント生成
            parentForm.bt_func4.Enabled = false;
            parentForm.bt_func4.Click += new EventHandler(this.form.bt_func4_Click);

            parentForm.bt_func6.Click += new EventHandler(this.form.bt_func6_Click);

            // 並び替え(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.bt_func7_Click);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);

            //JWNET送信(F9)イベント生成
            parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);

            //保留保存(F10)イベント生成
            parentForm.bt_func10.Click += new EventHandler(this.form.bt_func10_Click);

            //検索条件クリア(F10)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.bt_func11_Click);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            //【1】パターン一覧(1)イベント生成
            parentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);

            //【2】検索条件設定(2)イベント生成
            parentForm.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);


            this.form.IchiranDgv1.CellDoubleClick += new DataGridViewCellEventHandler(this.form.IchiranDgv11_CellDoubleClick);

            /// 20141023 Houkakou 「処分終了報告」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.form.DATE_TO.MouseDoubleClick += new MouseEventHandler(DATE_TO_MouseDoubleClick);
            this.form.ManifestNoTo.MouseDoubleClick += new MouseEventHandler(ManifestNoTo_MouseDoubleClick);
            /// 20141023 Houkakou 「処分終了報告」のダブルクリックを追加する　end

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end

                //DTO
                //MSIDtoCls MSIDtoCls = new MSIDtoCls();
                TMEDtoCls TMPEDtoCls = new TMEDtoCls();

                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                //ロストフォカスイベント初期化
                this.LostfocusInit();

                //PopupDataSource設定
                this.PopupInit();

                this.allControl = this.form.allControl;
                this.form.customDataGridView1.AllowUserToAddRows = false;
                this.form.IchiranDgv1.AllowUserToAddRows = false;                                //行の追加オプション(false)
                this.form.DATE_FROM.Value = null;
                this.form.DATE_TO.Value = null;
                this.form.SyoriKubun_CD.Focus();
                this.form.cntb_TashaEDI_KBN.Text = TASHAEDI_DEFUALT;

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタンイベントの初期化処理
        /// </summary>
        internal void LostfocusInit()
        {
            LogUtility.DebugMethodStart();

            this.form.SyoriKubun_CD.Validated += new EventHandler(SyoriKubun_CD_Validated);

            this.form.ManifestNoFrom.Validated += new EventHandler(ManifestNoFrom_Validated);

            this.form.ManifestNoTo.Validated += new EventHandler(ManifestNoTo_Validated);

            this.form.HAIKI_KBN_CD.Validated += new EventHandler(HAIKI_KBN_Validated);


            this.form.IchiranDgv1.CellValidating += new DataGridViewCellValidatingEventHandler(IchiranDgv1_CellValidating);
            this.form.IchiranDgv1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.form.IchiranDgv1_CellBeginEdit);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// popupイベント初期化
        /// </summary>
        public void PopupInit()
        {
            LogUtility.DebugMethodStart();

            // 廃棄物種類
            this.form.HAIKI_KBN_CD.PopupWindowId = WINDOW_ID.M_DENSHI_HAIKI_SHURUI;
            this.form.Syuruyi_Btn.PopupWindowId = WINDOW_ID.M_DENSHI_HAIKI_SHURUI;
            // ポップアップに表示するデータ列(物理名)
            this.form.HAIKI_KBN_CD.PopupGetMasterField = "HAIKI_SHURUI_CD,HAIKI_SHURUI_NAME";
            this.form.Syuruyi_Btn.PopupGetMasterField = "HAIKI_SHURUI_CD,HAIKI_SHURUI_NAME";
            // 表示用データ取得＆加工
            var HaikiDataTable = this.GetPopUpHaiki(this.form.HAIKI_KBN_CD.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()));
            // 列名とデータソース設定
            this.form.HAIKI_KBN_CD.PopupDataHeaderTitle = new string[] { "廃棄物種類CD", "廃棄物種類名" };
            this.form.HAIKI_KBN_CD.PopupDataSource = HaikiDataTable;
            this.form.Syuruyi_Btn.PopupDataHeaderTitle = new string[] { "廃棄物種類CD", "廃棄物種類名" };
            this.form.Syuruyi_Btn.PopupDataSource = HaikiDataTable;
            //popup画面ヘッダ設定
            this.form.HAIKI_KBN_CD.PopupDataSource.TableName = "廃棄物種類検索";
            this.form.HAIKI_KBN_CD.PopupDataSource.TableName = "廃棄物種類検索";






            //業者+現場 共通化ロジック対応
            this.form.Jigyousya_CD.DisplayItemName = "排出事業者";
            this.form.Jigyoujou_CD.DisplayItemName = "排出事業場";
            this.form.Unpan_CD.DisplayItemName = "収集運搬業者";
            this.form.Unpansha_CD.DisplayItemName = "報告処分業者";


            //排出事業者
            Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                this.form.Jigyousya_CD, this.form.JIGYOUSHA_NAME, this.form.Jigyousha_Btn,
                this.form.Jigyoujou_CD, this.form.JIGYOUJOU_NAME, this.form.Jigyoujou_Btn,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.DENSHI,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.HAISHUTSU_NIZUMI_GYOUSHA | Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.SHOBUN_NIOROSHI_GYOUSHA, false, false,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.HAISHUTSU_NIZUMI_GENBA,
                true, true, true);


            //収集運搬業者
            Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                this.form.Unpan_CD, this.form.Unpan_Name, this.form.Unpan_Btn,
                null, null, null,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.DENSHI,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.UNPAN_JUTAKUSHA_KAISHA, false, false,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.NONE,
                false, true, false);

            //"報告処分業者
            Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                this.form.Unpansha_CD, this.form.Unpansha_Name, this.form.Unpansha_Btn,
                null, null, null,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.DENSHI,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.SHOBUN_NIOROSHI_GYOUSHA, true, false,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.NONE,
                false, true, false);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region Validated

        //廃棄物種類チェック
        private void HAIKI_KBN_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (this.form.HAIKI_KBN_CD.Text == "")
            {
                this.form.HAIKI_SHURUI_NAME.Text = "";
            }
            else
            {
                TMEDtoCls Dto_Haiki = new TMEDtoCls();
                Dto_Haiki.HAIKI_KBN_CD = this.form.HAIKI_KBN_CD.Text;
                DataTable Check_Haiki = new DataTable();
                Check_Haiki = Dao_CheckHaiki.GetDataForEntity(Dto_Haiki);
                var table = Check_Haiki;
                if (table.Rows.Count > 0)
                {
                    this.form.HAIKI_SHURUI_NAME.Text = table.Rows[0]["HAIKI_SHURUI_NAME"].ToString();
                }
                else
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "廃棄物種類");
                    this.form.HAIKI_KBN_CD.Focus();
                    this.form.HAIKI_SHURUI_NAME.Text = "";
                    this.form.HAIKI_KBN_CD.BackColor = Constans.ERROR_COLOR;
                }
            }
            LogUtility.DebugMethodEnd();
        }

        //マニフェスト番号FROM桁数チェック
        private void ManifestNoFrom_Validated(object sender, EventArgs e)
        {
            if (this.form.ManifestNoFrom.Text.Length < 11 && this.form.ManifestNoFrom.Text.Length != 0)
            {
                MessageBoxUtility.MessageBoxShow("E012", "11桁の数値");
                this.form.ManifestNoFrom.Focus();
                this.form.ManifestNoFrom.BackColor = Constans.ERROR_COLOR;
                return;
            }
        }

        //マニフェスト番号TO桁数チェック 
        private void ManifestNoTo_Validated(object sender, EventArgs e)
        {
            if (this.form.ManifestNoTo.Text.Length < 11 && this.form.ManifestNoTo.Text.Length != 0)
            {
                MessageBoxUtility.MessageBoxShow("E012", "11桁の数値"); ;
                this.form.ManifestNoTo.Focus();
                this.form.ManifestNoTo.BackColor = Constans.ERROR_COLOR;
                return;
            }
        }


        //処理区分チェック     
        private void SyoriKubun_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (this.form.SyoriKubun_CD.Text == "")
            {
                string errorMessage = "処理区分を選択してください。";
                MessageBox.Show(errorMessage, Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.form.SyoriKubun_CD.Focus();
                this.form.SyoriKubun_CD.BackColor = Constans.ERROR_COLOR;
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        /// <summary>
        /// 登録
        /// </summary>
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索
        /// </summary>
        public int Search()
        {
            try
            {

                LogUtility.DebugMethodStart();

                Int32 count_TME = 0;

                this.Get_Search_TME();

                if (this.Search_TME.Rows.Count == 0)
                {
                    new MessageBoxShowLogic().MessageBoxShow("C001");
                }

                //取得件数
                return count_TME;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                return -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">廃棄物種類</param>
        /// <returns></returns>
        public DataTable GetPopUpHaiki(IEnumerable<string> displayCol)
        {
            LogUtility.DebugMethodStart();

            this.Dao_PopupHaiki = DaoInitUtility.GetComponent<DAO.DAOClass_PopupHaiki>();

            TMEDtoCls search_Haiki = new TMEDtoCls();

            if (displayCol.Any(s => s.Length == 0))
            {
                return new DataTable();
            }

            var dt = Dao_PopupHaiki.GetDataForEntity(search_Haiki);
            if (dt.Rows.Count == 0)
            {
                return dt;
            }

            var sortedDt = new DataTable();
            foreach (var col in displayCol)
            {
                // 表示対象の列だけを順番に追加
                sortedDt.Columns.Add(dt.Columns[col].ColumnName, dt.Columns[col].DataType);
            }

            foreach (DataRow r in dt.Rows)
            {
                sortedDt.Rows.Add(sortedDt.Columns.OfType<DataColumn>().Select(s => r[s.ColumnName]).ToArray());
            }

            LogUtility.DebugMethodEnd();
            return sortedDt;
            //return null;
        }

        /// <summary>
        /// 廃棄物区分 必須チェック
        /// </summary>
        public Boolean Haiki_Kbn_CD_Check(out bool catchErr)
        {
            LogUtility.DebugMethodStart();
            catchErr = false;
            Boolean check = false;
            try
            {
                switch (this.form.SyoriKubun_CD.Text)
                {
                    case "1":
                    case "2":
                    case "3":
                        break;

                    default:
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("W001", "1", "5");
                        //フォーカスを出力区分へ移動
                        this.form.SyoriKubun_CD.Select();
                        this.form.SyoriKubun_CD.BackColor = Constans.ERROR_COLOR;
                        check = true;
                        break;
                }
                return check;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Haiki_Kbn_CD_Check", ex1);
                MessageBoxUtility.MessageBoxShow("E080", "");
                catchErr = true;
                check = false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Haiki_Kbn_CD_Check", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
                catchErr = true;
                check = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Haiki_Kbn_CD_Check", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = true;
                check = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(check, catchErr);
            }
            return check;

        }

        /// <summary>
        /// データ取得
        /// </summary>
        public void Get_Search_TME()
        {
            LogUtility.DebugMethodStart();

            // ファンクションボタンの活性/非活性切り替え
            this.setHoryuDelete();

            // 設定されているソート条件を保存
            var sort = ((DataTable)this.form.IchiranDgv1.DataSource).DefaultView.Sort;

            this.CreateSql();

            this.Search_TME = dao_GetTME.getdateforstringsql(this.createSql);
            this.Search_TME = this.GetFilteringData(this.Search_TME);

            // 検索条件保存
            this.searchSyoriKubunCD = this.form.SyoriKubun_CD.Text;

            var dataTable = this.Search_TME;

            // カラム編集のためにReadOnlyを解除
            foreach (DataColumn column in dataTable.Columns)
            {
                column.ReadOnly = false;
            }

            dataTable.BeginLoadData();

            // 操作CD、操作名に値を設定
            this.SetSousaColumn(dataTable);

            // 固定列作成
            this.CreateFixedColumn();

            // 可変列作成
            this.CreateVariableColumn(dataTable);

            this.form.SetAllSelectChecked(false);
            this.form.IchiranDgv1.AutoGenerateColumns = false;

            // 保存していたソート条件を復元
            dataTable.DefaultView.Sort = sort;

            this.form.IchiranDgv1.DataSource = dataTable;

            // カラム幅自動調整
            this.AdjustColumnSize();

            // ソート通りに描画されないときがあるので、再描画
            this.form.IchiranDgv1.Refresh();

            this.form.SetRowReadOnly();

            LogUtility.DebugMethodEnd();
        }

        private void CreateSql()
        {
            //SQL文格納StringBuilder
            var sql = new StringBuilder();

            #region SELECT句
            sql.Append(" SELECT  ");
            sql.Append("        CONVERT(bit, 0) AS 'CHECKBOX'");
            sql.Append("      , '詳細' AS '詳細ボタン'");
            sql.Append("      , '' AS '操作CD'");
            sql.Append("      , '' AS '操作'");
            sql.Append("      , SUMMARY.KANRI_ID AS '管理番号'");
            sql.Append("      , SUMMARY.SEQ AS '枝番'");
            sql.Append("      , SUMMARY.FUNCTION_ID AS '機能番号'");
            sql.Append("      , SUMMARY.MF_TIMESTAMP AS MF_TIMESTAMP");
            sql.Append("      , SUMMARY.D10_TIMESTAMP AS D10_TIMESTAMP");
            sql.Append("      , SUMMARY.D10_MOD_TIMESTAMP AS D10_MOD_TIMESTAMP");
            sql.Append("      , SUMMARY.QUE_TIMESTAMP AS QUE_TIMESTAMP");
            sql.Append("      , SUMMARY.QUE_SEQ AS QUE_SEQ");
            sql.Append("      , SUMMARY.MEJI_QUE_SEQ AS MEJI_QUE_SEQ");
            sql.Append("      , SUMMARY.GYOUSHA_CD AS GYOUSHA_CD");
            sql.Append("      , SUMMARY.UPN_SHA_EDI_MEMBER_ID AS UPN_SHA_EDI_MEMBER_ID");
            sql.Append("      , SUMMARY.UPNSAKI_EDI_MEMBER_ID AS UPNSAKI_EDI_MEMBER_ID");
            sql.Append("      , SUMMARY.SBN_SHA_MEMBER_ID AS SBN_SHA_MEMBER_ID");
            sql.Append("      , SUMMARY.SBN_END_REP_DATE AS SBN_END_REP_DATE");
            sql.Append("      , CASE WHEN SUMMARY.HIKIWATASHI_DATE IS NULL THEN '' WHEN SUMMARY.HIKIWATASHI_DATE = '' THEN '' ELSE SUBSTRING(SUMMARY.HIKIWATASHI_DATE, 1, 4) + '/' + SUBSTRING(SUMMARY.HIKIWATASHI_DATE, 5, 2) + '/' + SUBSTRING(SUMMARY.HIKIWATASHI_DATE, 7, 2) END AS HIKIWATASHIBI");
            sql.Append("      , SUMMARY.HST_MEMBER_ID AS HST_MEMBER_ID");
            sql.Append("      , SUMMARY.UPN1_MEMBER_ID AS UPN1_MEMBER_ID");
            sql.Append("      , SUMMARY.UPN2_MEMBER_ID AS UPN2_MEMBER_ID");
            sql.Append("      , SUMMARY.UPN3_MEMBER_ID AS UPN3_MEMBER_ID");
            sql.Append("      , SUMMARY.UPN4_MEMBER_ID AS UPN4_MEMBER_ID");
            sql.Append("      , SUMMARY.UPN5_MEMBER_ID AS UPN5_MEMBER_ID");
            sql.Append("      , SUMMARY.SBN_MEMBER_ID AS SBN_MEMBER_ID");
            String MOutputPatternSelect = this.selectQuery;
            if (String.IsNullOrEmpty(MOutputPatternSelect))
            {
            }
            else
            {
                sql.Append(", ");
                sql.Append(MOutputPatternSelect);
            }
            #endregion

            #region FROM句
            sql.Append(" FROM (");
            sql.Append(" SELECT MANI.KANRI_ID AS KANRI_ID");
            sql.Append("      , MANI.SEQ AS SEQ");
            sql.Append("      , MANI.MANIFEST_ID AS MANIFEST_ID");
            sql.Append("      , MANI.HIKIWATASHI_DATE AS HIKIWATASHI_DATE");
            sql.Append("      , MANI.HST_SHA_NAME AS HST_SHA_NAME");
            sql.Append("      , MANI.HST_JOU_NAME AS HST_JOU_NAME");
            sql.Append("      , MANI.HAIKI_SUU AS HAIKI_SUU");
            sql.Append("      , UNIT2.UNIT_NAME_RYAKU AS HAIKI_UNIT_NAME");
            sql.Append("      , MANI.HAIKI_SHURUI AS HAIKI_SHURUI");
            sql.Append("      , MANI.SBN_END_REP_DATE AS SBN_END_REP_DATE");
            sql.Append("      , DT_MF_MEMBER.HST_MEMBER_ID");
            sql.Append("      , DT_MF_MEMBER.UPN1_MEMBER_ID");
            sql.Append("      , DT_MF_MEMBER.UPN2_MEMBER_ID");
            sql.Append("      , DT_MF_MEMBER.UPN3_MEMBER_ID");
            sql.Append("      , DT_MF_MEMBER.UPN4_MEMBER_ID");
            sql.Append("      , DT_MF_MEMBER.UPN5_MEMBER_ID");
            sql.Append("      , DT_MF_MEMBER.SBN_MEMBER_ID");
            //1,終了報告
            if (this.form.SyoriKubun_Radio1.Checked)
            {
                sql.Append("      , '' AS FUNCTION_ID");
                sql.Append("      , '' AS SBN_END_DATE");
                sql.Append("      , '' AS HAIKI_IN_DATE");
                sql.Append("      , CAST(NULL AS DECIMAL) AS RECEPT_SUU");
                sql.Append("      , '' AS RECEPT_UNIT_CODE");
                sql.Append("      , '' AS RECEPT_UNIT_NAME");
                sql.Append("      , '' AS UPN_TAN_CD");
                sql.Append("      , '' AS UPN_TAN_NAME");
                sql.Append("      , '' AS CAR_NO");
                sql.Append("      , '' AS SHARYOU_NAME_RYAKU");
                sql.Append("      , '' AS REP_TAN_CD");
                sql.Append("      , '' AS REP_TAN_NAME");
                sql.Append("      , '' AS SBN_TAN_CD");
                sql.Append("      , '' AS SBN_TAN_NAME");
                sql.Append("      , '' AS REP_KBN");
                sql.Append("      , '' AS REP_KBN_NAME");
                sql.Append("      , '' AS BIKOU");
                sql.Append("      , '' AS MF_TIMESTAMP");
                sql.Append("      , '' AS D10_TIMESTAMP");
                sql.Append("      , '' AS D10_MOD_TIMESTAMP");
                sql.Append("      , '' AS QUE_TIMESTAMP");
                sql.Append("      , '' AS QUE_SEQ");
                sql.Append("      , '' AS MEJI_QUE_SEQ");
                //簡易検索条件
                sql.Append("      , MANI.HAIKI_DAI_CODE AS HAIKI_DAI_CODE");
                sql.Append("      , MANI.HAIKI_CHU_CODE AS HAIKI_CHU_CODE");
                sql.Append("      , MANI.HAIKI_SHO_CODE AS HAIKI_SHO_CODE");
                sql.Append("      , MANI.HST_SHA_EDI_MEMBER_ID AS HST_SHA_EDI_MEMBER_ID");
                sql.Append("      , SHUSHU.UPN_SHA_EDI_MEMBER_ID AS UPN_SHA_EDI_MEMBER_ID");
                sql.Append("      , SHUSHU.UPNSAKI_EDI_MEMBER_ID AS UPNSAKI_EDI_MEMBER_ID");
                sql.Append("      , DENSHI_JIGYOUSHA.GYOUSHA_CD AS GYOUSHA_CD");
                sql.Append("      , MANI.SBN_SHA_MEMBER_ID AS SBN_SHA_MEMBER_ID");
                sql.Append("      , MANI.SUU_KAKUTEI_CODE");
                sql.Append("      , CASE WHEN MANI.SUU_KAKUTEI_CODE = '01' THEN '排出事業者'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '02' THEN '処分業者'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '03' THEN '収集運搬業者（区間1）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '04' THEN '収集運搬業者（区間2）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '05' THEN '収集運搬業者（区間3）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '06' THEN '収集運搬業者（区間4）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '07' THEN '収集運搬業者（区間5）'");
                sql.Append("             ELSE ''");
                sql.Append("        END SUU_KAKUTEI_SHA");
                sql.Append("      , MANI.HAIKI_KAKUTEI_SUU");
                sql.Append("      , (SELECT UNIT_NAME_RYAKU FROM M_UNIT WHERE UNIT_CD = MANI.HAIKI_KAKUTEI_UNIT_CODE)AS HAIKI_KAKUTEI_UNIT_NAME");
                sql.Append("      , MANI.HAIKI_NAME");
                sql.Append("      , SHUSHU.UPN_SHA_NAME");
                sql.Append("      , SHUSHU.UPN_END_DATE");
                sql.Append("      , MANI.SBN_WAY_NAME");
                sql.Append("      , (SELECT SHOBUN_HOUHOU_NAME_RYAKU FROM M_SHOBUN_HOUHOU WHERE SHOBUN_HOUHOU_CD = MANI_EX.SBN_HOUHOU_CD) AS SHOBUN_HOUHOU_NAME");
                sql.Append("      , MANI.SBN_SHA_NAME");
                sql.Append("      , SHUSHU.UPNSAKI_JOU_NAME");
                sql.Append(" FROM DT_R18 MANI");
                sql.Append("      LEFT JOIN DT_R18_EX MANI_EX ON MANI_EX.KANRI_ID = MANI.KANRI_ID AND MANI_EX.DELETE_FLG = 0");
                sql.Append("      INNER JOIN DT_R19 SHUSHU ON SHUSHU.KANRI_ID = MANI.KANRI_ID AND SHUSHU.SEQ = MANI.SEQ ");
                sql.Append("         AND  SHUSHU.UPN_ROUTE_NO = (");
                sql.Append("            SELECT MAX(UPN_ROUTE_NO) FROM DT_R19 ");
                sql.Append("            WHERE  DT_R19.KANRI_ID = MANI.KANRI_ID AND DT_R19.SEQ = MANI.SEQ)");
                sql.Append("      INNER JOIN MS_JWNET_MEMBER MEMBER ON MANI.SBN_SHA_MEMBER_ID = MEMBER.EDI_MEMBER_ID ");
                sql.Append("      INNER JOIN DT_MF_TOC MANI_MEJI ON SHUSHU.KANRI_ID = MANI_MEJI.KANRI_ID AND SHUSHU.SEQ = MANI_MEJI.LATEST_SEQ");
                sql.Append("      LEFT JOIN DT_D10 UNPAN ON SHUSHU.KANRI_ID = UNPAN.KANRI_ID");
                sql.Append("      LEFT JOIN M_DENSHI_JIGYOUSHA DENSHI_JIGYOUSHA ON SHUSHU.UPN_SHA_EDI_MEMBER_ID = DENSHI_JIGYOUSHA.EDI_MEMBER_ID ");
                sql.Append("      LEFT JOIN  M_UNIT UNIT2 ON MANI.HAIKI_UNIT_CODE = UNIT2.UNIT_CD");
                sql.Append("      INNER JOIN DT_MF_MEMBER ON MANI.KANRI_ID = DT_MF_MEMBER.KANRI_ID ");
                sql.Append(" WHERE ");
                sql.Append("       MANI_MEJI.STATUS_FLAG = 4 ");
                sql.Append("       AND (MANI_MEJI.KIND = 4 OR MANI_MEJI.KIND IS NULL)");
                sql.Append("       AND MANI_MEJI.STATUS_DETAIL = 0 ");
                sql.Append("       AND (MANI.SBN_ENDREP_FLAG IS NULL OR MANI.SBN_ENDREP_FLAG = 0)");
                sql.Append("       AND NOT EXISTS ( SELECT DISTINCT KANRI_ID FROM QUE_INFO QUE WHERE MANI_MEJI.KANRI_ID = QUE.KANRI_ID AND QUE.FUNCTION_ID = '1500' AND QUE.STATUS_FLAG = 7 )");
                sql.Append("       AND NOT EXISTS ( SELECT QUE.KANRI_ID, QUE.SEQ,  QUE.UPN_ROUTE_NO FROM QUE_INFO QUE INNER JOIN ( ");
                sql.Append("       SELECT DISTINCT KANRI_ID,MAX(QUE_SEQ) MAX_QUESEQ FROM QUE_INFO QUE WHERE FUNCTION_ID IN ('1500','1600','1800') AND STATUS_FLAG IN (0,1,2,7)  GROUP BY KANRI_ID, SEQ, UPN_ROUTE_NO ) ");
                sql.Append("       LAST_QUE ON QUE.KANRI_ID = LAST_QUE.KANRI_ID AND QUE.QUE_SEQ = LAST_QUE.MAX_QUESEQ WHERE QUE.FUNCTION_ID = '1500' AND QUE.STATUS_FLAG IN (0,1,2,7) AND MANI_MEJI.KANRI_ID = QUE.KANRI_ID)");
                sql.Append("       AND NOT EXISTS ( SELECT * FROM DT_MF_TOC AS TOC INNER JOIN QUE_INFO AS QUE ON TOC.KANRI_ID = QUE.KANRI_ID AND TOC.LATEST_SEQ = QUE.SEQ WHERE MANI_MEJI.KANRI_ID = TOC.KANRI_ID AND (QUE.FUNCTION_ID = '0501' OR QUE.FUNCTION_ID = '0502') ) ");
                sql.Append(" ) AS SUMMARY ");
            }
            //2.終了報告の送信保留
            else if (this.form.SyoriKubun_Radio2.Checked)
            {
                sql.Append("      , QUE.FUNCTION_ID AS FUNCTION_ID");
                sql.Append("      , SHOBUN.SBN_END_DATE AS SBN_END_DATE");
                sql.Append("      , SHOBUN.HAIKI_IN_DATE AS HAIKI_IN_DATE");
                sql.Append("      , SHOBUN.RECEPT_SUU AS RECEPT_SUU");
                sql.Append("      , SHOBUN.RECEPT_UNIT_CODE AS RECEPT_UNIT_CODE");
                sql.Append("      , UNIT1.UNIT_NAME_RYAKU AS RECEPT_UNIT_NAME");
                sql.Append("      , '' AS UPN_TAN_CD");
                sql.Append("      , SHOBUN.UPN_TAN_NAME AS UPN_TAN_NAME");
                sql.Append("      , '' AS CAR_NO");
                sql.Append("      , SHOBUN.CAR_NO AS SHARYOU_NAME_RYAKU");
                sql.Append("      , '' AS REP_TAN_CD");
                sql.Append("      , SHOBUN.REP_TAN_NAME AS REP_TAN_NAME");
                sql.Append("      , '' AS SBN_TAN_CD");
                sql.Append("      , SHOBUN.SBN_TAN_NAME AS SBN_TAN_NAME");
                sql.Append("      , SHOBUN.REP_KBN AS REP_KBN");
                sql.Append("      , (CASE SHOBUN.REP_KBN ");
                sql.Append("        WHEN '1' THEN '中間' ");
                sql.Append("        WHEN '2' THEN '最終' ");
                sql.Append("        ELSE '' END");
                sql.Append("        ) AS REP_KBN_NAME");
                sql.Append("      , SHOBUN.BIKOU AS BIKOU");
                sql.Append("      , MANI_MEJI.UPDATE_TS AS MF_TIMESTAMP");
                sql.Append("      , SHOBUN.UPDATE_TS AS D10_TIMESTAMP");
                sql.Append("      , '' AS D10_MOD_TIMESTAMP");
                sql.Append("      , QUE.UPDATE_TS AS QUE_TIMESTAMP");
                sql.Append("      , QUE.QUE_SEQ AS QUE_SEQ");
                sql.Append("      , '' AS MEJI_QUE_SEQ");
                //簡易検索条件
                sql.Append("      , MANI.HAIKI_DAI_CODE AS HAIKI_DAI_CODE");
                sql.Append("      , MANI.HAIKI_CHU_CODE AS HAIKI_CHU_CODE");
                sql.Append("      , MANI.HAIKI_SHO_CODE AS HAIKI_SHO_CODE");
                sql.Append("      , MANI.HST_SHA_EDI_MEMBER_ID AS HST_SHA_EDI_MEMBER_ID");
                sql.Append("      , SHUSHU.UPN_SHA_EDI_MEMBER_ID AS UPN_SHA_EDI_MEMBER_ID");
                sql.Append("      , SHUSHU.UPNSAKI_EDI_MEMBER_ID AS UPNSAKI_EDI_MEMBER_ID");
                sql.Append("      , DENSHI_JIGYOUSHA.GYOUSHA_CD AS GYOUSHA_CD");
                sql.Append("      , MANI.SBN_SHA_MEMBER_ID AS SBN_SHA_MEMBER_ID");
                sql.Append("      , MANI.SUU_KAKUTEI_CODE");
                sql.Append("      , CASE WHEN MANI.SUU_KAKUTEI_CODE = '01' THEN '排出事業者'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '02' THEN '処分業者'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '03' THEN '収集運搬業者（区間1）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '04' THEN '収集運搬業者（区間2）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '05' THEN '収集運搬業者（区間3）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '06' THEN '収集運搬業者（区間4）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '07' THEN '収集運搬業者（区間5）'");
                sql.Append("             ELSE ''");
                sql.Append("        END SUU_KAKUTEI_SHA");
                sql.Append("      , MANI.HAIKI_KAKUTEI_SUU");
                sql.Append("      , (SELECT UNIT_NAME_RYAKU FROM M_UNIT WHERE UNIT_CD = MANI.HAIKI_KAKUTEI_UNIT_CODE)AS HAIKI_KAKUTEI_UNIT_NAME");
                sql.Append("      , MANI.HAIKI_NAME");
                sql.Append("      , SHUSHU.UPN_SHA_NAME");
                sql.Append("      , SHUSHU.UPN_END_DATE");
                sql.Append("      , MANI.SBN_WAY_NAME");
                sql.Append("      , (SELECT SHOBUN_HOUHOU_NAME_RYAKU FROM M_SHOBUN_HOUHOU WHERE SHOBUN_HOUHOU_CD = MANI_EX.SBN_HOUHOU_CD) AS SHOBUN_HOUHOU_NAME");
                sql.Append("      , MANI.SBN_SHA_NAME");
                sql.Append("      , SHUSHU.UPNSAKI_JOU_NAME");
                sql.Append(" FROM DT_R18 MANI");
                sql.Append("      LEFT JOIN DT_R18_EX MANI_EX ON MANI_EX.KANRI_ID = MANI.KANRI_ID AND MANI_EX.DELETE_FLG = 0");
                sql.Append("      INNER JOIN DT_R19 SHUSHU ON SHUSHU.KANRI_ID = MANI.KANRI_ID AND SHUSHU.SEQ = MANI.SEQ ");
                sql.Append("         AND  SHUSHU.UPN_ROUTE_NO = (");
                sql.Append("            SELECT MAX(UPN_ROUTE_NO) FROM DT_R19 ");
                sql.Append("            WHERE  DT_R19.KANRI_ID = MANI.KANRI_ID AND DT_R19.SEQ = MANI.SEQ)");
                sql.Append("      INNER JOIN MS_JWNET_MEMBER MEMBER ON MANI.SBN_SHA_MEMBER_ID = MEMBER.EDI_MEMBER_ID ");
                sql.Append("      INNER JOIN  QUE_INFO QUE ON QUE.KANRI_ID = MANI.KANRI_ID ");
                sql.Append("      INNER JOIN  DT_D10 SHOBUN ON SHOBUN.KANRI_ID = MANI.KANRI_ID");
                sql.Append("      LEFT JOIN  M_UNIT UNIT1 ON SHOBUN.RECEPT_UNIT_CODE = UNIT1.UNIT_CD");
                sql.Append("      INNER JOIN DT_MF_TOC MANI_MEJI ON MANI.KANRI_ID = MANI_MEJI.KANRI_ID AND MANI.SEQ = MANI_MEJI.LATEST_SEQ");
                sql.Append("      LEFT JOIN M_DENSHI_JIGYOUSHA DENSHI_JIGYOUSHA ON SHUSHU.UPN_SHA_EDI_MEMBER_ID = DENSHI_JIGYOUSHA.EDI_MEMBER_ID ");
                sql.Append("      LEFT JOIN  M_SHARYOU SHARYOU ON DENSHI_JIGYOUSHA.GYOUSHA_CD = SHARYOU.GYOUSHA_CD AND SHOBUN.CAR_NO = SHARYOU.SHARYOU_CD");
                sql.Append("      LEFT JOIN  M_UNIT UNIT2 ON MANI.HAIKI_UNIT_CODE = UNIT2.UNIT_CD");
                sql.Append("      INNER JOIN DT_MF_MEMBER ON MANI.KANRI_ID = DT_MF_MEMBER.KANRI_ID ");
                sql.Append(" WHERE ");
                sql.Append("       MANI_MEJI.STATUS_FLAG = 4 ");
                sql.Append("       AND (MANI_MEJI.KIND = 4 OR MANI_MEJI.KIND IS NULL)");
                sql.Append("       AND MANI_MEJI.STATUS_DETAIL = 0 ");
                sql.Append("       AND QUE.FUNCTION_ID = '1500'");
                sql.Append("       AND QUE.STATUS_FLAG = 7");
                sql.Append(" UNION ");
                sql.Append(" SELECT MANI.KANRI_ID AS KANRI_ID");
                sql.Append("      , MANI.SEQ AS SEQ");
                sql.Append("      , MANI.MANIFEST_ID AS MANIFEST_ID");
                sql.Append("      , MANI.HIKIWATASHI_DATE AS HIKIWATASHI_DATE");
                sql.Append("      , MANI.HST_SHA_NAME AS HST_SHA_NAME");
                sql.Append("      , MANI.HST_JOU_NAME AS HST_JOU_NAME");
                sql.Append("      , MANI.HAIKI_SUU AS HAIKI_SUU");
                sql.Append("      , UNIT2.UNIT_NAME_RYAKU AS HAIKI_UNIT_NAME");
                sql.Append("      , MANI.HAIKI_SHURUI AS HAIKI_SHURUI");
                sql.Append("      , MANI.SBN_END_REP_DATE AS SBN_END_REP_DATE");
                sql.Append("      , DT_MF_MEMBER.HST_MEMBER_ID");
                sql.Append("      , DT_MF_MEMBER.UPN1_MEMBER_ID");
                sql.Append("      , DT_MF_MEMBER.UPN2_MEMBER_ID");
                sql.Append("      , DT_MF_MEMBER.UPN3_MEMBER_ID");
                sql.Append("      , DT_MF_MEMBER.UPN4_MEMBER_ID");
                sql.Append("      , DT_MF_MEMBER.UPN5_MEMBER_ID");
                sql.Append("      , DT_MF_MEMBER.SBN_MEMBER_ID");
                sql.Append("      , QUE.FUNCTION_ID AS FUNCTION_ID");
                sql.Append("      , SHOBUN.SBN_END_DATE AS SBN_END_DATE");
                sql.Append("      , SHOBUN.HAIKI_IN_DATE AS HAIKI_IN_DATE");
                sql.Append("      , SHOBUN.RECEPT_SUU AS RECEPT_SUU");
                sql.Append("      , SHOBUN.RECEPT_UNIT_CODE AS RECEPT_UNIT_CODE");
                sql.Append("      , UNIT1.UNIT_NAME_RYAKU AS RECEPT_UNIT_NAME");
                sql.Append("      , '' AS UPN_TAN_CD");
                sql.Append("      , SHOBUN.UPN_TAN_NAME AS UPN_TAN_NAME");
                sql.Append("      , '' AS CAR_NO");
                sql.Append("      , SHOBUN.CAR_NO AS SHARYOU_NAME_RYAKU");
                sql.Append("      , '' AS REP_TAN_CD");
                sql.Append("      , SHOBUN.REP_TAN_NAME AS REP_TAN_NAME");
                sql.Append("      , '' AS SBN_TAN_CD");
                sql.Append("      , SHOBUN.SBN_TAN_NAME AS SBN_TAN_NAME");
                sql.Append("      , SHOBUN.REP_KBN AS REP_KBN");
                sql.Append("      , (CASE SHOBUN.REP_KBN ");
                sql.Append("        WHEN '1' THEN '中間' ");
                sql.Append("        WHEN '2' THEN '最終' ");
                sql.Append("        ELSE '' END");
                sql.Append("        ) AS REP_KBN_NAME");
                sql.Append("      , SHOBUN.BIKOU AS BIKOU");
                sql.Append("      , MANI_MEJI.UPDATE_TS AS MF_TIMESTAMP");
                sql.Append("      , '' AS D10_TIMESTAMP");
                sql.Append("      , SHOBUN.UPDATE_TS AS D10_MOD_TIMESTAMP");
                sql.Append("      , QUE.UPDATE_TS AS QUE_TIMESTAMP");
                sql.Append("      , QUE.QUE_SEQ AS QUE_SEQ");
                sql.Append("      , '' AS MEJI_QUE_SEQ");
                //簡易検索条件
                sql.Append("      , MANI.HAIKI_DAI_CODE AS HAIKI_DAI_CODE");
                sql.Append("      , MANI.HAIKI_CHU_CODE AS HAIKI_CHU_CODE");
                sql.Append("      , MANI.HAIKI_SHO_CODE AS HAIKI_SHO_CODE");
                sql.Append("      , MANI.HST_SHA_EDI_MEMBER_ID AS HST_SHA_EDI_MEMBER_ID");
                sql.Append("      , SHUSHU.UPN_SHA_EDI_MEMBER_ID AS UPN_SHA_EDI_MEMBER_ID");
                sql.Append("      , SHUSHU.UPNSAKI_EDI_MEMBER_ID AS UPNSAKI_EDI_MEMBER_ID");
                sql.Append("      , DENSHI_JIGYOUSHA.GYOUSHA_CD AS GYOUSHA_CD");
                sql.Append("      , MANI.SBN_SHA_MEMBER_ID AS SBN_SHA_MEMBER_ID");
                sql.Append("      , MANI.SUU_KAKUTEI_CODE");
                sql.Append("      , CASE WHEN MANI.SUU_KAKUTEI_CODE = '01' THEN '排出事業者'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '02' THEN '処分業者'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '03' THEN '収集運搬業者（区間1）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '04' THEN '収集運搬業者（区間2）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '05' THEN '収集運搬業者（区間3）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '06' THEN '収集運搬業者（区間4）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '07' THEN '収集運搬業者（区間5）'");
                sql.Append("             ELSE ''");
                sql.Append("        END SUU_KAKUTEI_SHA");
                sql.Append("      , MANI.HAIKI_KAKUTEI_SUU");
                sql.Append("      , (SELECT UNIT_NAME_RYAKU FROM M_UNIT WHERE UNIT_CD = MANI.HAIKI_KAKUTEI_UNIT_CODE)AS HAIKI_KAKUTEI_UNIT_NAME");
                sql.Append("      , MANI.HAIKI_NAME");
                sql.Append("      , SHUSHU.UPN_SHA_NAME");
                sql.Append("      , SHUSHU.UPN_END_DATE");
                sql.Append("      , MANI.SBN_WAY_NAME");
                sql.Append("      , (SELECT SHOBUN_HOUHOU_NAME_RYAKU FROM M_SHOBUN_HOUHOU WHERE SHOBUN_HOUHOU_CD = MANI_EX.SBN_HOUHOU_CD) AS SHOBUN_HOUHOU_NAME");
                sql.Append("      , MANI.SBN_SHA_NAME");
                sql.Append("      , SHUSHU.UPNSAKI_JOU_NAME");
                sql.Append(" FROM DT_R18 MANI");
                sql.Append("      LEFT JOIN DT_R18_EX MANI_EX ON MANI_EX.KANRI_ID = MANI.KANRI_ID AND MANI_EX.DELETE_FLG = 0");
                sql.Append("      INNER JOIN DT_R19 SHUSHU ON SHUSHU.KANRI_ID = MANI.KANRI_ID AND SHUSHU.SEQ = MANI.SEQ ");
                sql.Append("         AND  SHUSHU.UPN_ROUTE_NO = (");
                sql.Append("            SELECT MAX(UPN_ROUTE_NO) FROM DT_R19 ");
                sql.Append("            WHERE  DT_R19.KANRI_ID = MANI.KANRI_ID AND DT_R19.SEQ = MANI.SEQ)");
                sql.Append("      INNER JOIN MS_JWNET_MEMBER MEMBER ON MANI.SBN_SHA_MEMBER_ID = MEMBER.EDI_MEMBER_ID ");
                sql.Append("      INNER JOIN  QUE_INFO QUE ON QUE.KANRI_ID = MANI.KANRI_ID ");
                sql.Append("      INNER JOIN  DT_D10_MOD SHOBUN ON SHOBUN.KANRI_ID = MANI.KANRI_ID");
                sql.Append("      LEFT JOIN  M_UNIT UNIT1 ON SHOBUN.RECEPT_UNIT_CODE = UNIT1.UNIT_CD");
                sql.Append("      INNER JOIN DT_MF_TOC MANI_MEJI ON MANI.KANRI_ID = MANI_MEJI.KANRI_ID AND MANI.SEQ = MANI_MEJI.LATEST_SEQ");
                sql.Append("      LEFT JOIN M_DENSHI_JIGYOUSHA DENSHI_JIGYOUSHA ON SHUSHU.UPN_SHA_EDI_MEMBER_ID = DENSHI_JIGYOUSHA.EDI_MEMBER_ID ");
                sql.Append("      LEFT JOIN  M_SHARYOU SHARYOU ON DENSHI_JIGYOUSHA.GYOUSHA_CD = SHARYOU.GYOUSHA_CD AND SHOBUN.CAR_NO = SHARYOU.SHARYOU_CD");
                sql.Append("      LEFT JOIN  M_UNIT UNIT2 ON MANI.HAIKI_UNIT_CODE = UNIT2.UNIT_CD");
                sql.Append("      INNER JOIN DT_MF_MEMBER ON MANI.KANRI_ID = DT_MF_MEMBER.KANRI_ID ");
                sql.Append(" WHERE ");
                sql.Append("       MANI_MEJI.STATUS_FLAG = 4 ");
                sql.Append("       AND (MANI_MEJI.KIND = 4 OR MANI_MEJI.KIND IS NULL)");
                sql.Append("       AND MANI_MEJI.STATUS_DETAIL = 0 ");
                sql.Append("       AND QUE.FUNCTION_ID = '1600'");
                sql.Append("       AND QUE.STATUS_FLAG = 7");
                sql.Append(" UNION ");
                sql.Append(" SELECT MANI.KANRI_ID AS KANRI_ID");
                sql.Append("      , MANI.SEQ AS SEQ");
                sql.Append("      , MANI.MANIFEST_ID AS MANIFEST_ID");
                sql.Append("      , MANI.HIKIWATASHI_DATE AS HIKIWATASHI_DATE");
                sql.Append("      , MANI.HST_SHA_NAME AS HST_SHA_NAME");
                sql.Append("      , MANI.HST_JOU_NAME AS HST_JOU_NAME");
                sql.Append("      , MANI.HAIKI_SUU AS HAIKI_SUU");
                sql.Append("      , UNIT2.UNIT_NAME_RYAKU AS HAIKI_UNIT_NAME");
                sql.Append("      , MANI.HAIKI_SHURUI AS HAIKI_SHURUI");
                sql.Append("      , MANI.SBN_END_REP_DATE AS SBN_END_REP_DATE");
                sql.Append("      , DT_MF_MEMBER.HST_MEMBER_ID");
                sql.Append("      , DT_MF_MEMBER.UPN1_MEMBER_ID");
                sql.Append("      , DT_MF_MEMBER.UPN2_MEMBER_ID");
                sql.Append("      , DT_MF_MEMBER.UPN3_MEMBER_ID");
                sql.Append("      , DT_MF_MEMBER.UPN4_MEMBER_ID");
                sql.Append("      , DT_MF_MEMBER.UPN5_MEMBER_ID");
                sql.Append("      , DT_MF_MEMBER.SBN_MEMBER_ID");
                sql.Append("      , QUE.FUNCTION_ID AS FUNCTION_ID");
                sql.Append("      , '' AS SBN_END_DATE");
                sql.Append("      , '' AS HAIKI_IN_DATE");
                sql.Append("      , CAST(NULL AS DECIMAL) AS RECEPT_SUU");
                sql.Append("      , '' AS RECEPT_UNIT_CODE");
                sql.Append("      , '' AS RECEPT_UNIT_NAME");
                sql.Append("      , '' AS UPN_TAN_CD");
                sql.Append("      , '' AS UPN_TAN_NAME");
                sql.Append("      , '' AS CAR_NO");
                sql.Append("      , '' AS SHARYOU_NAME_RYAKU");
                sql.Append("      , '' AS REP_TAN_CD");
                sql.Append("      , '' AS REP_TAN_NAME");
                sql.Append("      , '' AS SBN_TAN_CD");
                sql.Append("      , '' AS SBN_TAN_NAME");
                sql.Append("      , '' AS REP_KBN");
                sql.Append("      , '' AS REP_KBN_NAME");
                sql.Append("      , '' AS BIKOU");
                sql.Append("      , MANI_MEJI.UPDATE_TS AS MF_TIMESTAMP");
                sql.Append("      , '' AS D10_TIMESTAMP");
                sql.Append("      , '' AS D10_MOD_TIMESTAMP");
                sql.Append("      , QUE.UPDATE_TS AS QUE_TIMESTAMP");
                sql.Append("      , QUE.QUE_SEQ AS QUE_SEQ");
                sql.Append("      , '' AS MEJI_QUE_SEQ");
                //簡易検索条件
                sql.Append("      , MANI.HAIKI_DAI_CODE AS HAIKI_DAI_CODE");
                sql.Append("      , MANI.HAIKI_CHU_CODE AS HAIKI_CHU_CODE");
                sql.Append("      , MANI.HAIKI_SHO_CODE AS HAIKI_SHO_CODE");
                sql.Append("      , MANI.HST_SHA_EDI_MEMBER_ID AS HST_SHA_EDI_MEMBER_ID");
                sql.Append("      , SHUSHU.UPN_SHA_EDI_MEMBER_ID AS UPN_SHA_EDI_MEMBER_ID");
                sql.Append("      , SHUSHU.UPNSAKI_EDI_MEMBER_ID AS UPNSAKI_EDI_MEMBER_ID");
                sql.Append("      , DENSHI_JIGYOUSHA.GYOUSHA_CD AS GYOUSHA_CD");
                sql.Append("      , MANI.SBN_SHA_MEMBER_ID AS SBN_SHA_MEMBER_ID");
                sql.Append("      , MANI.SUU_KAKUTEI_CODE");
                sql.Append("      , CASE WHEN MANI.SUU_KAKUTEI_CODE = '01' THEN '排出事業者'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '02' THEN '処分業者'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '03' THEN '収集運搬業者（区間1）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '04' THEN '収集運搬業者（区間2）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '05' THEN '収集運搬業者（区間3）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '06' THEN '収集運搬業者（区間4）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '07' THEN '収集運搬業者（区間5）'");
                sql.Append("             ELSE ''");
                sql.Append("        END SUU_KAKUTEI_SHA");
                sql.Append("      , MANI.HAIKI_KAKUTEI_SUU");
                sql.Append("      , (SELECT UNIT_NAME_RYAKU FROM M_UNIT WHERE UNIT_CD = MANI.HAIKI_KAKUTEI_UNIT_CODE)AS HAIKI_KAKUTEI_UNIT_NAME");
                sql.Append("      , MANI.HAIKI_NAME");
                sql.Append("      , SHUSHU.UPN_SHA_NAME");
                sql.Append("      , SHUSHU.UPN_END_DATE");
                sql.Append("      , MANI.SBN_WAY_NAME");
                sql.Append("      , (SELECT SHOBUN_HOUHOU_NAME_RYAKU FROM M_SHOBUN_HOUHOU WHERE SHOBUN_HOUHOU_CD = MANI_EX.SBN_HOUHOU_CD) AS SHOBUN_HOUHOU_NAME");
                sql.Append("      , MANI.SBN_SHA_NAME");
                sql.Append("      , SHUSHU.UPNSAKI_JOU_NAME");
                sql.Append(" FROM DT_R18 MANI");
                sql.Append("      LEFT JOIN DT_R18_EX MANI_EX ON MANI_EX.KANRI_ID = MANI.KANRI_ID AND MANI_EX.DELETE_FLG = 0");
                sql.Append("      INNER JOIN DT_R19 SHUSHU ON SHUSHU.KANRI_ID = MANI.KANRI_ID AND SHUSHU.SEQ = MANI.SEQ ");
                sql.Append("         AND  SHUSHU.UPN_ROUTE_NO = (");
                sql.Append("            SELECT MAX(UPN_ROUTE_NO) FROM DT_R19 ");
                sql.Append("            WHERE  DT_R19.KANRI_ID = MANI.KANRI_ID AND DT_R19.SEQ = MANI.SEQ)");
                sql.Append("      INNER JOIN MS_JWNET_MEMBER MEMBER ON MANI.SBN_SHA_MEMBER_ID = MEMBER.EDI_MEMBER_ID ");
                sql.Append("      INNER JOIN  QUE_INFO QUE ON QUE.KANRI_ID = MANI.KANRI_ID ");
                sql.Append("      INNER JOIN DT_MF_TOC MANI_MEJI ON MANI.KANRI_ID = MANI_MEJI.KANRI_ID AND MANI.SEQ = MANI_MEJI.LATEST_SEQ");
                sql.Append("      LEFT JOIN M_DENSHI_JIGYOUSHA DENSHI_JIGYOUSHA ON SHUSHU.UPN_SHA_EDI_MEMBER_ID = DENSHI_JIGYOUSHA.EDI_MEMBER_ID ");
                sql.Append("      LEFT JOIN  M_UNIT UNIT2 ON MANI.HAIKI_UNIT_CODE = UNIT2.UNIT_CD");
                sql.Append("      INNER JOIN DT_MF_MEMBER ON MANI.KANRI_ID = DT_MF_MEMBER.KANRI_ID ");
                sql.Append(" WHERE ");
                sql.Append("       MANI_MEJI.STATUS_FLAG = 4 ");
                sql.Append("       AND (MANI_MEJI.KIND = 4 OR MANI_MEJI.KIND IS NULL)");
                sql.Append("       AND MANI_MEJI.STATUS_DETAIL = 0 ");
                sql.Append("       AND QUE.FUNCTION_ID = '1800'");
                sql.Append("       AND QUE.STATUS_FLAG = 7");
                sql.Append(" ) AS SUMMARY ");
            }
            //3.終了報告の修正・削除
            else
            {
                sql.Append("      , '' AS FUNCTION_ID");
                sql.Append("      , MANI.SBN_END_DATE AS SBN_END_DATE");
                sql.Append("      , MANI.HAIKI_IN_DATE AS HAIKI_IN_DATE");
                sql.Append("      , MANI.RECEPT_SUU AS RECEPT_SUU");
                sql.Append("      , MANI.RECEPT_UNIT_CODE AS RECEPT_UNIT_CODE");
                sql.Append("      , UNIT1.UNIT_NAME_RYAKU AS RECEPT_UNIT_NAME");
                sql.Append("      , '' AS UPN_TAN_CD");
                sql.Append("      , MANI.UPN_TAN_NAME AS UPN_TAN_NAME");
                sql.Append("      , '' AS CAR_NO");
                sql.Append("      , MANI.CAR_NO AS SHARYOU_NAME_RYAKU");
                sql.Append("      , '' AS REP_TAN_CD");
                sql.Append("      , MANI.REP_TAN_NAME AS REP_TAN_NAME");
                sql.Append("      , '' AS SBN_TAN_CD");
                sql.Append("      , MANI.SBN_TAN_NAME AS SBN_TAN_NAME");
                sql.Append("      , MANI.SBN_ENDREP_KBN AS REP_KBN");
                sql.Append("      , (CASE MANI.SBN_ENDREP_KBN ");
                sql.Append("        WHEN '1' THEN '中間' ");
                sql.Append("        WHEN '2' THEN '最終' ");
                sql.Append("        ELSE '' END");
                sql.Append("        ) AS REP_KBN_NAME");
                sql.Append("      , MANI.SBN_REP_BIKOU AS BIKOU");
                sql.Append("      , MANI_MEJI.UPDATE_TS AS MF_TIMESTAMP");
                sql.Append("      , '' AS D10_TIMESTAMP");
                sql.Append("      , '' AS D10_MOD_TIMESTAMP");
                sql.Append("      , '' AS QUE_TIMESTAMP");
                sql.Append("      , '' AS QUE_SEQ");
                sql.Append("      , MANI_MEJI.LATEST_SEQ AS MEJI_QUE_SEQ");
                sql.Append("      , MANI.HAIKI_DAI_CODE AS HAIKI_DAI_CODE");
                sql.Append("      , MANI.HAIKI_CHU_CODE AS HAIKI_CHU_CODE");
                sql.Append("      , MANI.HAIKI_SHO_CODE AS HAIKI_SHO_CODE");
                sql.Append("      , MANI.HST_SHA_EDI_MEMBER_ID AS HST_SHA_EDI_MEMBER_ID");
                sql.Append("      , SHUSHU.UPN_SHA_EDI_MEMBER_ID AS UPN_SHA_EDI_MEMBER_ID");
                sql.Append("      , SHUSHU.UPNSAKI_EDI_MEMBER_ID AS UPNSAKI_EDI_MEMBER_ID");
                sql.Append("      , DENSHI_JIGYOUSHA.GYOUSHA_CD AS GYOUSHA_CD");
                sql.Append("      , MANI.SBN_SHA_MEMBER_ID AS SBN_SHA_MEMBER_ID");
                sql.Append("      , MANI.SUU_KAKUTEI_CODE");
                sql.Append("      , CASE WHEN MANI.SUU_KAKUTEI_CODE = '01' THEN '排出事業者'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '02' THEN '処分業者'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '03' THEN '収集運搬業者（区間1）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '04' THEN '収集運搬業者（区間2）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '05' THEN '収集運搬業者（区間3）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '06' THEN '収集運搬業者（区間4）'");
                sql.Append("             WHEN MANI.SUU_KAKUTEI_CODE = '07' THEN '収集運搬業者（区間5）'");
                sql.Append("             ELSE ''");
                sql.Append("        END SUU_KAKUTEI_SHA");
                sql.Append("      , MANI.HAIKI_KAKUTEI_SUU");
                sql.Append("      , (SELECT UNIT_NAME_RYAKU FROM M_UNIT WHERE UNIT_CD = MANI.HAIKI_KAKUTEI_UNIT_CODE)AS HAIKI_KAKUTEI_UNIT_NAME");
                sql.Append("      , MANI.HAIKI_NAME");
                sql.Append("      , SHUSHU.UPN_SHA_NAME");
                sql.Append("      , SHUSHU.UPN_END_DATE");
                sql.Append("      , MANI.SBN_WAY_NAME");
                sql.Append("      , (SELECT SHOBUN_HOUHOU_NAME_RYAKU FROM M_SHOBUN_HOUHOU WHERE SHOBUN_HOUHOU_CD = MANI_EX.SBN_HOUHOU_CD) AS SHOBUN_HOUHOU_NAME");
                sql.Append("      , MANI.SBN_SHA_NAME");
                sql.Append("      , SHUSHU.UPNSAKI_JOU_NAME");
                sql.Append(" FROM DT_R18 MANI ");
                sql.Append("      LEFT JOIN DT_R18_EX MANI_EX ON MANI_EX.KANRI_ID = MANI.KANRI_ID AND MANI_EX.DELETE_FLG = 0");
                sql.Append("      INNER JOIN DT_R19 SHUSHU ON SHUSHU.KANRI_ID = MANI.KANRI_ID AND SHUSHU.SEQ = MANI.SEQ ");
                sql.Append("         AND  SHUSHU.UPN_ROUTE_NO = (");
                sql.Append("            SELECT MAX(UPN_ROUTE_NO) FROM DT_R19 ");
                sql.Append("            WHERE  DT_R19.KANRI_ID = MANI.KANRI_ID AND DT_R19.SEQ = MANI.SEQ)");
                sql.Append("      LEFT JOIN  M_UNIT UNIT1 ON MANI.RECEPT_UNIT_CODE = UNIT1.UNIT_CD");
                sql.Append("      INNER JOIN MS_JWNET_MEMBER MEMBER ON MANI.SBN_SHA_MEMBER_ID = MEMBER.EDI_MEMBER_ID ");
                sql.Append("      INNER JOIN DT_MF_TOC MANI_MEJI ON SHUSHU.KANRI_ID = MANI_MEJI.KANRI_ID AND SHUSHU.SEQ = MANI_MEJI.LATEST_SEQ");
                sql.Append("      LEFT JOIN M_DENSHI_JIGYOUSHA DENSHI_JIGYOUSHA ON SHUSHU.UPN_SHA_EDI_MEMBER_ID = DENSHI_JIGYOUSHA.EDI_MEMBER_ID ");
                sql.Append("      LEFT JOIN  M_UNIT UNIT2 ON MANI.HAIKI_UNIT_CODE = UNIT2.UNIT_CD");
                sql.Append("      INNER JOIN DT_MF_MEMBER ON MANI.KANRI_ID = DT_MF_MEMBER.KANRI_ID ");
                sql.Append(" WHERE ");
                sql.Append("       MANI_MEJI.STATUS_FLAG = 4 ");
                sql.Append("       AND (MANI_MEJI.KIND = 4 OR MANI_MEJI.KIND IS NULL)");
                sql.Append("       AND MANI_MEJI.STATUS_DETAIL = 0 ");
                sql.Append("       AND MANI.SBN_ENDREP_FLAG = 1");
                sql.Append("       AND NOT EXISTS ( SELECT DISTINCT KANRI_ID FROM QUE_INFO QUE WHERE MANI_MEJI.KANRI_ID = QUE.KANRI_ID AND QUE.FUNCTION_ID IN ('1600', '1800') AND QUE.STATUS_FLAG = 7 )");
                sql.Append(" ) AS SUMMARY ");
            }

            sql.Append(this.joinQuery);

            #endregion

            //検索条件が一つもある場合、WHEREを付ける。
            SetJyoken();

            #region WHERE句

            if (this.First_Flg == true)
            {
                sql.Append(" WHERE 2 = 1");
            }
            else
            {
                //検索条件が一つもある場合、WHEREを付ける。
                if (CheckJyoken(0) > 0)
                {
                    sql.Append(" WHERE 1 = 1");
                }
            }

            //日付from
            if (this.form.DATE_FROM.Value != null)
            {
                sql.Append(" AND ");
                sql.Append("       SUMMARY.HIKIWATASHI_DATE >= '" + this.form.DATE_FROM.Text.Substring(0, 10).Replace("/", "") + "'");
            }

            //日付to
            if (this.form.DATE_TO.Value != null)
            {
                sql.Append(" AND ");
                sql.Append("       SUMMARY.HIKIWATASHI_DATE <= '" + this.form.DATE_TO.Text.Substring(0, 10).Replace("/", "") + "'");
            }

            //番号from
            if (this.form.ManifestNoFrom.Text != "")
            {
                sql.Append(" AND ");
                sql.Append("       SUMMARY.MANIFEST_ID >= '" + this.form.ManifestNoFrom.Text + "'");
            }

            //番号to
            if (this.form.ManifestNoTo.Text != "")
            {
                sql.Append(" AND ");
                sql.Append("       SUMMARY.MANIFEST_ID <= '" + this.form.ManifestNoTo.Text + "'");
            }

            //廃棄物種類
            if (this.form.HAIKI_KBN_CD.Text != "")
            {
                sql.Append(" AND ");
                sql.Append("       SUMMARY.HAIKI_DAI_CODE = '" + this.form.HAIKI_KBN_CD.Text.Substring(0, 2) + "' AND");
                sql.Append("       SUMMARY.HAIKI_CHU_CODE = '" + this.form.HAIKI_KBN_CD.Text.Substring(2, 1) + "' AND");
                sql.Append("       SUMMARY.HAIKI_SHO_CODE = '" + this.form.HAIKI_KBN_CD.Text.Substring(3, 1) + "'");
            }

            //排出事業者
            if (this.form.Jigyousya_CD.Text != "")
            {
                sql.Append(" AND ");
                sql.Append("       SUMMARY.HST_SHA_EDI_MEMBER_ID = '" + this.form.Jigyousya_CD.Text + "'");
            }

            //現場は将軍の現場マスタのCDが入る 将軍のCDはEXにしかないので、existsを利用
            //排出事業場
            if (this.form.Jigyoujou_CD.Text != "")
            {
                // 排出事業場はCDがないため、名称と所在地１～４の一致で検索する
                sql.Append(" AND ");
                sql.Append("   EXISTS (SELECT * FROM DT_R18 DTR18,M_DENSHI_JIGYOUJOU ");
                sql.Append("           WHERE  DTR18.KANRI_ID = SUMMARY.KANRI_ID ");
                sql.Append("              AND DTR18.SEQ = SUMMARY.SEQ ");
                sql.Append("              AND M_DENSHI_JIGYOUJOU.EDI_MEMBER_ID = '" + this.form.Jigyousya_CD.Text + "'");
                sql.Append("              AND M_DENSHI_JIGYOUJOU.JIGYOUJOU_CD = '" + this.form.Jigyoujou_CD.Text + "'");
                sql.Append("              AND DTR18.HST_JOU_NAME = M_DENSHI_JIGYOUJOU.JIGYOUJOU_NAME ");
                sql.Append("              AND (DTR18.HST_JOU_ADDRESS1 + DTR18.HST_JOU_ADDRESS2 + DTR18.HST_JOU_ADDRESS3 + DTR18.HST_JOU_ADDRESS4) ");
                sql.Append("                = (M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS1 + M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS2 + M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS3 + M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS4)) ");
            }

            //報告収集運搬
            if (this.form.Unpan_CD.Text != "")
            {
                sql.Append(" AND ");
                sql.Append("       SUMMARY.UPN_SHA_EDI_MEMBER_ID = '" + this.form.Unpan_CD.Text + "'");
            }

            //運搬先事業者
            if (this.form.Unpansha_CD.Text != "")
            {
                sql.Append(" AND ");
                sql.Append("       SUMMARY.UPNSAKI_EDI_MEMBER_ID = '" + this.form.Unpansha_CD.Text + "'");
            }

            // 他社ＥＤＩ使用
            switch (this.form.cntb_TashaEDI_KBN.Text)
            {
                // 使用排出表示
                case "1":
                    sql.Append("AND EXISTS ( ");
                    sql.Append("SELECT *  ");
                    sql.Append("FROM M_GYOUSHA  ");
                    sql.Append("LEFT JOIN M_DENSHI_JIGYOUSHA  ");
                    sql.Append("ON M_GYOUSHA.GYOUSHA_CD = M_DENSHI_JIGYOUSHA.GYOUSHA_CD ");
                    sql.Append("WHERE M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID = SUMMARY.HST_SHA_EDI_MEMBER_ID ");
                    sql.Append("AND M_GYOUSHA.TASYA_EDI = 1) ");
                    break;
                // 未使用排出表示
                case "2":
                    sql.Append("AND EXISTS ( ");
                    sql.Append("SELECT *  ");
                    sql.Append("FROM M_GYOUSHA  ");
                    sql.Append("LEFT JOIN M_DENSHI_JIGYOUSHA  ");
                    sql.Append("ON M_GYOUSHA.GYOUSHA_CD = M_DENSHI_JIGYOUSHA.GYOUSHA_CD ");
                    sql.Append("WHERE M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID = SUMMARY.HST_SHA_EDI_MEMBER_ID ");
                    sql.Append("AND M_GYOUSHA.TASYA_EDI = 2) ");
                    break;
                // 全て
                default:
                    break;
            }

            #endregion

            #region ORDERBY句

            sql.Append(" ORDER BY ");

            if (!string.IsNullOrEmpty(orderByQuery))
            {
                sql.Append(this.orderByQuery);
            }
            else
            {
                sql.Append(" SUMMARY.KANRI_ID ASC, ");
                sql.Append(" SUMMARY.SEQ ASC");
            }

            #endregion

            this.createSql = sql.ToString();
            sql.Append("");
        }

        #region カラム作成
        /// <summary>
        /// 非表示カラムを作成します
        /// </summary>
        /// <param name="columnName">カラム名</param>
        private void CreateHiddenColumn(string columnName)
        {
            var hiddenColumn = new DgvCustomTextBoxColumn();
            hiddenColumn.Name = columnName;
            hiddenColumn.DataPropertyName = columnName;
            hiddenColumn.ReadOnly = true;
            hiddenColumn.Visible = false;
            this.form.IchiranDgv1.Columns.Add(hiddenColumn);
        }

        /// <summary>
        /// CDカラムを作成します
        /// </summary>
        /// <param name="columnName">カラム名</param>
        private void CreateCdColumn(string columnName)
        {
            var cdColumn = new DgvCustomTextBoxColumn();
            cdColumn.Name = columnName;
            cdColumn.DataPropertyName = columnName;
            this.form.IchiranDgv1.Columns.Add(cdColumn);
        }

        /// <summary>
        /// 入力CDカラムを作成します
        /// </summary>
        /// <param name="columnName">カラム名</param>
        private void CreateInputCdColumn(string columnName)
        {
            var inputCdNumericColumn = new DgvCustomNumericTextBox2Column() { Name = columnName, DataPropertyName = columnName };
            var inputCdAlphaNumColumn = new DgvCustomAlphaNumTextBoxColumn() { Name = columnName, DataPropertyName = columnName };
            if (columnName == "報告区分CD")
            {
                inputCdNumericColumn.MaxInputLength = 1;
                inputCdNumericColumn.ToolTipText = "1:中間　2:最終　を設定してください";
                inputCdNumericColumn.HeaderText = "報告区分CD※";
                inputCdNumericColumn.RangeSetting.Min = 1;
                inputCdNumericColumn.RangeSetting.Max = 2;

                this.form.IchiranDgv1.Columns.Add(inputCdNumericColumn);
            }
            else if (columnName == "受入量単位CD")
            {
                inputCdAlphaNumColumn.MaxInputLength = 2;
                inputCdAlphaNumColumn.CharactersNumber = 2;
                inputCdAlphaNumColumn.ToolTipText = "受入量単位CDを入力してください";
                inputCdAlphaNumColumn.PopupWindowName = "マスタ共通ポップアップ";
                inputCdAlphaNumColumn.PopupWindowId = WINDOW_ID.M_UNIT;
                inputCdAlphaNumColumn.ZeroPaddengFlag = true;
                inputCdAlphaNumColumn.AlphabetLimitFlag = false;

                this.form.IchiranDgv1.Columns.Add(inputCdAlphaNumColumn);
            }
        }

        /// <summary>
        /// 名称カラムを作成します
        /// </summary>
        /// <param name="columnName">カラム名</param>
        private void CreateNameColumn(string columnName)
        {
            var nameColumn = new DgvCustomTextBoxColumn();
            nameColumn.Name = columnName;
            nameColumn.DataPropertyName = columnName;
            nameColumn.ReadOnly = true;
            this.form.IchiranDgv1.Columns.Add(nameColumn);
        }

        /// <summary>
        /// 入力名称カラムを作成します
        /// </summary>
        /// <param name="columnName">カラム名</param>
        private void CreateInputNameColumn(string columnName)
        {
            var inputNameColumn = new DgvCustomTextBoxColumn();
            inputNameColumn.Name = columnName;
            inputNameColumn.DataPropertyName = columnName;
            if (columnName == "処分担当者")
            {
                inputNameColumn.MaxInputLength = 24;
                inputNameColumn.CharactersNumber = 24;
                inputNameColumn.ToolTipText = "処分担当者を入力してください（スペースキー押下にて、検索画面を表示します）";
                inputNameColumn.PopupWindowName = "マスタ共通ポップアップ";
                inputNameColumn.PopupWindowId = WINDOW_ID.M_DENSHI_TANTOUSHA;
                inputNameColumn.HeaderText = "処分担当者※";
            }
            else if (columnName == "報告担当者")
            {
                inputNameColumn.MaxInputLength = 24;
                inputNameColumn.CharactersNumber = 24;
                inputNameColumn.ToolTipText = "報告担当者を入力してください（スペースキー押下にて、検索画面を表示します）";
                inputNameColumn.PopupWindowName = "マスタ共通ポップアップ";
                inputNameColumn.PopupWindowId = WINDOW_ID.M_DENSHI_TANTOUSHA;
            }
            else if (columnName == "運搬担当者")
            {
                inputNameColumn.MaxInputLength = 24;
                inputNameColumn.CharactersNumber = 24;
                inputNameColumn.ToolTipText = "運搬担当者を入力してください（スペースキー押下にて、検索画面を表示します）";
                inputNameColumn.PopupWindowName = "マスタ共通ポップアップ";
                inputNameColumn.PopupWindowId = WINDOW_ID.M_DENSHI_TANTOUSHA;
            }
            else if (columnName == "車輌")
            {
                inputNameColumn.MaxInputLength = 30;
                inputNameColumn.CharactersNumber = 30;
                inputNameColumn.ToolTipText = "車輌を入力してください（スペースキー押下にて、検索画面を表示します）";
                inputNameColumn.PopupWindowName = "マスタ共通ポップアップ";
                inputNameColumn.PopupWindowId = WINDOW_ID.M_SHARYOU;
            }
            else if (columnName == "備考")
            {
                inputNameColumn.MaxInputLength = 256;
                inputNameColumn.CharactersNumber = 256;
                inputNameColumn.ToolTipText = "備考を入力してください（任意）";
            }
            this.form.IchiranDgv1.Columns.Add(inputNameColumn);
        }

        /// <summary>
        /// 数値カラムを作成します
        /// </summary>
        /// <param name="columnName">カラム名</param>
        private void CreateNumericColumn(string columnName)
        {
            var numericColumn = new DgvCustomNumericTextBox2Column();
            numericColumn.Name = columnName;
            numericColumn.DataPropertyName = columnName;
            numericColumn.FormatSetting = "カスタム";
            numericColumn.CustomFormatSetting = "#,##0.000";
            numericColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            numericColumn.Width = 130;
            numericColumn.ReadOnly = true;
            this.form.IchiranDgv1.Columns.Add(numericColumn);
        }

        /// <summary>
        /// 入力数値カラムを作成します
        /// </summary>
        /// <param name="columnName">カラム名</param>
        private void CreateInputNumericColumn(string columnName)
        {
            var inputNumericColumn = new DgvCustomNumericTextBox2Column();
            inputNumericColumn.Name = columnName;
            inputNumericColumn.DataPropertyName = columnName;
            inputNumericColumn.MaxInputLength = 10;
            inputNumericColumn.FormatSetting = "カスタム";
            inputNumericColumn.CustomFormatSetting = "#,##0.000";
            inputNumericColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            inputNumericColumn.Width = 130;
            if (columnName == "受入量")
            {
                inputNumericColumn.ToolTipText = "受入量を入力してください";
                r_framework.Dto.RangeSettingDto rangeSetting = new r_framework.Dto.RangeSettingDto();
                rangeSetting.Max = new decimal(new int[] {
                    99999999,
                    0,
                    0,
                    196608});
                inputNumericColumn.RangeSetting = rangeSetting;
            }
            this.form.IchiranDgv1.Columns.Add(inputNumericColumn);
        }

        /// <summary>
        /// 日付カラムを作成します
        /// </summary>
        /// <param name="columnName">カラム名</param>
        private void CreateDateColumn(string columnName)
        {
            var dateColumn = new DgvCustomDataTimeColumn();
            dateColumn.Name = columnName;
            dateColumn.DataPropertyName = columnName;
            dateColumn.ReadOnly = true;
            dateColumn.ShowYoubi = false;
            dateColumn.DefaultCellStyle.Format = "yyyy/MM/dd";
            this.form.IchiranDgv1.Columns.Add(dateColumn);
        }

        /// <summary>
        /// 入力日付カラムを作成します
        /// </summary>
        /// <param name="columnName">カラム名</param>
        private void CreateInputDateColumn(string columnName)
        {
            var inputDateColumn = new DgvCustomDataTimeColumn();
            inputDateColumn.Name = columnName;
            inputDateColumn.DataPropertyName = columnName;
            inputDateColumn.ShowYoubi = false;
            inputDateColumn.DefaultCellStyle.Format = "yyyy/MM/dd";
            if (columnName == "処分終了日")
            {
                inputDateColumn.ToolTipText = "処分終了日を入力してください";
                inputDateColumn.HeaderText = "処分終了日※";
            }
            else if (columnName == "廃棄物受領日")
            {
                inputDateColumn.ToolTipText = "廃棄物受領日を入力してください";
            }
            this.form.IchiranDgv1.Columns.Add(inputDateColumn);
        }
        #endregion

        #region カラム名リスト作成
        /// <summary>
        /// 非表示項目カラム名リストを作成します
        /// </summary>
        /// <returns></returns>
        private List<String> CreateHiddenColumnNameList()
        {
            LogUtility.DebugMethodStart();

            var ret = new List<String>();

            ret.Add("UPN_SHA_EDI_MEMBER_ID");
            ret.Add("GYOUSHA_CD");
            ret.Add("HIKIWATASHIBI");
            ret.Add("管理番号");
            ret.Add("枝番");
            ret.Add("MF_TIMESTAMP");
            ret.Add("QUE_SEQ");
            ret.Add("QUE_TIMESTAMP");
            ret.Add("SBN_SHA_MEMBER_ID");
            ret.Add("機能番号");
            ret.Add("SBN_END_REP_DATE");
            ret.Add("D10_MOD_TIMESTAMP");
            ret.Add("D10_TIMESTAMP");

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// コード項目カラム名リストを作成します
        /// </summary>
        /// <returns>コード項目カラム名リスト</returns>
        private List<String> CreateCdColumnNameList()
        {
            LogUtility.DebugMethodStart();

            var ret = new List<String>();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 入力コード項目カラム名リストを作成します
        /// </summary>
        /// <returns>入力コード項目カラム名リスト</returns>
        private List<String> CreateInputCdColumnNameList()
        {
            LogUtility.DebugMethodStart();

            var ret = new List<String>();

            ret.Add("報告区分CD");
            ret.Add("受入量単位CD");

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 名称項目カラム名リストを作成します
        /// </summary>
        /// <returns>名称項目カラム名リスト</returns>
        private List<String> CreateNameColumnNameList()
        {
            LogUtility.DebugMethodStart();

            var ret = new List<String>();

            ret.Add("報告区分名");
            ret.Add("受入量単位名");
            ret.Add("マニフェスト番号");
            ret.Add("排出事業者名");
            ret.Add("排出事業場名");
            ret.Add("単位");
            ret.Add("数量確定者");
            ret.Add("確定単位");
            ret.Add("廃棄物種類");
            ret.Add("廃棄物名称");
            ret.Add("運搬受託者名（最終）");
            ret.Add("処分受託者名");
            ret.Add("処分事業場名");
            ret.Add("処分方法名");
            ret.Add("（将軍）処分方法名");

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 入力名称項目カラム名リストを作成します
        /// </summary>
        /// <returns>入力名称項目カラム名リスト</returns>
        private List<String> CreateInputNameColumnNameList()
        {
            LogUtility.DebugMethodStart();

            var ret = new List<String>();

            ret.Add("処分担当者");
            ret.Add("報告担当者");
            ret.Add("運搬担当者");
            ret.Add("車輌");
            ret.Add("備考");

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 数値項目カラム名リストを作成します
        /// </summary>
        /// <returns>数値項目カラム名リスト</returns>
        private List<String> CreateNumericColumnNameList()
        {
            LogUtility.DebugMethodStart();

            var ret = new List<String>();

            ret.Add("数量");
            ret.Add("確定数量");

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 入力数値項目カラム名リストを作成します
        /// </summary>
        /// <returns>数値項目カラム名リスト</returns>
        private List<String> CreateInputNumericColumnNameList()
        {
            LogUtility.DebugMethodStart();

            var ret = new List<String>();

            ret.Add("受入量");

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 日付項目カラム名リストを作成します
        /// </summary>
        /// <returns>日付項目カラム名リスト</returns>
        private List<String> CreateDateColumnNameList()
        {
            LogUtility.DebugMethodStart();

            var ret = new List<String>();

            ret.Add("引渡し日");
            ret.Add("運搬終了日（最終）");

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 入力日付項目カラム名リストを作成します
        /// </summary>
        /// <returns>入力日付項目カラム名リスト</returns>
        private List<String> CreateInputDateColumnNameList()
        {
            LogUtility.DebugMethodStart();

            var ret = new List<String>();

            ret.Add("処分終了日");
            ret.Add("廃棄物受領日");

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        #endregion

        #region データグリッドの列作成
        /// <summary>
        /// 固定列を作成します
        /// </summary>
        public void CreateFixedColumn()
        {
            LogUtility.DebugMethodStart();

            // Columnsを先にクリアすることで、フォーカスアウトイベント内で、不正値として扱ってもらう
            // DataSource = null から実行するとInvalidOperationExceptionが発生してしまう
            this.form.IchiranDgv1.Columns.Clear();
            this.form.IchiranDgv1.DataSource = null;

            // チェックボックス
            DataGridViewCheckBoxColumn Column_CheckBox = new DataGridViewCheckBoxColumn();
            Column_CheckBox.Name = "CHECKBOX";
            Column_CheckBox.DataPropertyName = "CHECKBOX";
            Column_CheckBox.ReadOnly = false;
            Column_CheckBox.Width = 35;
            Column_CheckBox.HeaderText = String.Empty;
            Column_CheckBox.ToolTipText = "処理対象とする場合はチェックしてください";
            this.form.IchiranDgv1.Columns.Add(Column_CheckBox);

            // 詳細ボタン
            DataGridViewButtonColumn Column_Button = new DataGridViewButtonColumn();
            Column_Button.Name = "詳細ボタン";
            Column_Button.DataPropertyName = "詳細ボタン";
            Column_Button.ReadOnly = false;
            Column_Button.Width = 45;
            Column_Button.HeaderText = String.Empty;
            this.form.IchiranDgv1.Columns.Add(Column_Button);

            // 操作CD
            DgvCustomNumericTextBox2Column Column_Sousa_CD = new DgvCustomNumericTextBox2Column();
            Column_Sousa_CD.Name = "操作CD";
            Column_Sousa_CD.DataPropertyName = "操作CD";
            Column_Sousa_CD.CharacterLimitList = new char[] { '2', '3' };
            Column_Sousa_CD.Width = 25;
            Column_Sousa_CD.MaxInputLength = 1;
            Column_Sousa_CD.HeaderText = "操作CD※";
            // 処理区分１・２はツールチップを表示しない
            if (this.form.SyoriKubun_Radio1.Checked || this.form.SyoriKubun_Radio2.Checked)
            {
                Column_Sousa_CD.ToolTipText = "";
                Column_Sousa_CD.ReadOnly = true;
            }
            else
            {
                Column_Sousa_CD.ToolTipText = "2:修正　3:取消　を指定してください";
            }
            this.form.IchiranDgv1.Columns.Add(Column_Sousa_CD);

            // 操作名
            DataGridViewTextBoxColumn Column_Sousa_Name = new DataGridViewTextBoxColumn();
            Column_Sousa_Name.Name = "操作";
            Column_Sousa_Name.DataPropertyName = "操作";
            Column_Sousa_Name.HeaderText = "操作";
            Column_Sousa_Name.ReadOnly = true;
            Column_Sousa_Name.Width = 50;
            this.form.IchiranDgv1.Columns.Add(Column_Sousa_Name);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 可変列を作成します
        /// </summary>
        /// <param name="dataTable">表示するデータ</param>
        internal void CreateVariableColumn(DataTable dataTable)
        {
            var hiddenColumnNameList = this.CreateHiddenColumnNameList();
            var cdColumnNameList = this.CreateCdColumnNameList();
            var inputCdColumnNameList = this.CreateInputCdColumnNameList();
            var nameColumnNameList = this.CreateNameColumnNameList();
            var inputNameColumnNameList = this.CreateInputNameColumnNameList();
            var numericColumnNameList = this.CreateNumericColumnNameList();
            var inputNumericColumnNameList = this.CreateInputNumericColumnNameList();
            var dateColumnNameList = this.CreateDateColumnNameList();
            var inputDateColumnNameList = this.CreateInputDateColumnNameList();
            foreach (var columnName in dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName))
            {
                if (hiddenColumnNameList.Contains(columnName))
                {
                    this.CreateHiddenColumn(columnName);
                }
                else if (cdColumnNameList.Contains(columnName))
                {
                    this.CreateCdColumn(columnName);
                }
                else if (inputCdColumnNameList.Contains(columnName))
                {
                    this.CreateInputCdColumn(columnName);
                }
                else if (nameColumnNameList.Contains(columnName))
                {
                    this.CreateNameColumn(columnName);
                }
                else if (inputNameColumnNameList.Contains(columnName))
                {
                    this.CreateInputNameColumn(columnName);
                }
                else if (numericColumnNameList.Contains(columnName))
                {
                    this.CreateNumericColumn(columnName);
                }
                else if (inputNumericColumnNameList.Contains(columnName))
                {
                    this.CreateInputNumericColumn(columnName);
                }
                else if (dateColumnNameList.Contains(columnName))
                {
                    this.CreateDateColumn(columnName);
                }
                else if (inputDateColumnNameList.Contains(columnName))
                {
                    this.CreateInputDateColumn(columnName);
                }
            }
        }
        #endregion

        /// <summary>
        /// 操作CD、操作名カラムに値をセットします
        /// </summary>
        private void SetSousaColumn(DataTable dataTable)
        {
            LogUtility.DebugMethodStart();

            foreach (DataRow row in dataTable.Rows)
            {
                var sousaCd = String.Empty;
                var sousaName = String.Empty;
                if (this.form.SyoriKubun_Radio1.Checked)
                {
                    sousaCd = "1";
                    sousaName = "報告";
                }
                else if (this.form.SyoriKubun_Radio2.Checked)
                {
                    if (row["機能番号"].ToString() == "1500")
                    {
                        sousaCd = "1";
                        sousaName = "報告";
                    }
                    else if (row["機能番号"].ToString() == "1600")
                    {
                        sousaCd = "2";
                        sousaName = "修正";
                    }
                    else
                    {
                        sousaCd = "3";
                        sousaName = "取消";
                    }
                }

                row["操作CD"] = sousaCd;
                row["操作"] = sousaName;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// IMPORT_MEMBER_FILTERテーブルでフィルタリングしたデータを取得する
        /// </summary>
        /// <returns></returns>
        private DataTable GetFilteringData(DataTable tempData)
        {
            var searchResult = tempData.Clone();
            /*
             *   0:フィルタ条件に関係なくデータ取得
             *   1:フィルタ条件に該当するデータのみ取得
             *   2:フィルタ条件に該当しないデータのみ取得
             */
            if (this.SYS_INFO_COPY_MODE > 0)
            {
                // フィルターデータ取得
                DataTable filterDt = this.ImportMemberFilterDao.GetAllData();
                // 削除 or 対象レコードのKanriID
                List<string> targetKanriID = new List<string>();
                // 対象データのLOOP
                foreach (DataRow row in tempData.Rows)
                {
                    // フィルターデータのLOOP
                    foreach (DataRow frow in filterDt.Rows)
                    {
                        string[] array = frow[0].ToString().Split(',');
                        // 空白を除いたフィルターデータの加入者番号の数
                        int cnt = array.Where(x => x != "").ToArray().Length;
                        string[] rowArray = new string[] {row["HST_MEMBER_ID"].ToString(),
                                                              row["UPN1_MEMBER_ID"].ToString(),
                                                              row["UPN2_MEMBER_ID"].ToString(),
                                                              row["UPN3_MEMBER_ID"].ToString(),
                                                              row["UPN4_MEMBER_ID"].ToString(),
                                                              row["UPN5_MEMBER_ID"].ToString(),
                                                              row["SBN_MEMBER_ID"].ToString()};
                        // 空白を除いた対象データの加入者番号の数
                        int rowCnt = rowArray.Where(x => x != "").ToArray().Length;
                        int matchCnt = 0;
                        // フィルターデータ(行)の1~5のLOOP
                        foreach (string a in array)
                        {
                            if (string.IsNullOrEmpty(a))
                            {
                                continue;
                            }
                            if (a == row["HST_MEMBER_ID"].ToString())
                            {
                                matchCnt++;
                                continue;
                            }
                            if (a == row["UPN1_MEMBER_ID"].ToString())
                            {
                                matchCnt++;
                                continue;
                            }
                            if (a == row["UPN2_MEMBER_ID"].ToString())
                            {
                                matchCnt++;
                                continue;
                            }
                            if (a == row["UPN3_MEMBER_ID"].ToString())
                            {
                                matchCnt++;
                                continue;
                            }
                            if (a == row["UPN4_MEMBER_ID"].ToString())
                            {
                                matchCnt++;
                                continue;
                            }
                            if (a == row["UPN5_MEMBER_ID"].ToString())
                            {
                                matchCnt++;
                                continue;
                            }
                            if (a == row["SBN_MEMBER_ID"].ToString())
                            {
                                matchCnt++;
                                continue;
                            }
                        }
                        if (matchCnt == cnt)
                        {
                            // 除外 or 取込み対象データ
                            targetKanriID.Add(row["管理番号"].ToString());
                        }
                    }
                }
                // 条件作成
                targetKanriID = targetKanriID.Distinct().ToList();
                string filterStr = "管理番号";
                DataRow[] rows = null;
                if (this.SYS_INFO_COPY_MODE == 1)
                {
                    rows = tempData.AsEnumerable().Where(w => targetKanriID.Contains(w.Field<string>(filterStr))).ToArray();
                }
                else
                {
                    rows = tempData.AsEnumerable().Where(w => !targetKanriID.Contains(w.Field<string>(filterStr))).ToArray();
                }
                // DataTableから抽出
                foreach (DataRow row in rows)
                {
                    searchResult.ImportRow(row);
                }
            }
            else
            {
                searchResult = tempData;
            }
            return searchResult;
        }

        /// <summary>
        /// 処理No ボタン選択
        /// </summary>
        public void SelectButton()
        {
            LogUtility.DebugMethodStart();
            try
            {
                switch (this.footer.txb_process.Text)
                {
                    case "1"://【1】パターン一覧
                        this.footer.bt_process1.PerformClick();
                        break;

                    case "2"://【2】検索条件設定
                        this.footer.bt_process2.PerformClick();
                        break;
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("SelectButton", ex1);
                MessageBoxUtility.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SelectButton", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("SelectButton", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// 処理No フォーカス移動
        /// </summary>
        public void SetFocusTxbProcess()
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.footer.txb_process.Focus();
                this.footer.lb_hint.Text = "処理Noを入力してください";
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {

                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// カラム幅自動調整
        /// </summary>
        private void AdjustColumnSize()
        {
            // 全カラムを自動調整すると入力項目は検索時ブランクのため、幅が狭くなってしまう。
            // そのため指摘のあったカラムのみ自動調整を行う。
            foreach (DataGridViewColumn c in this.form.IchiranDgv1.Columns)
            {
                if ("排出事業者名".Equals(c.Name) || "排出事業場名".Equals(c.Name) || "廃棄物種類".Equals(c.Name)
                    || "廃棄物名称".Equals(c.Name) || "処分受託者名".Equals(c.Name) || "処分事業場名".Equals(c.Name)
                    || "運搬受託者名（最終）".Equals(c.Name))
                {
                    this.form.IchiranDgv1.AutoResizeColumn(c.Index, DataGridViewAutoSizeColumnMode.DisplayedCells);
                }
            }

        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 保留削除更新処理
        /// </summary>
        [Transaction]
        public void HoryuDelete()
        {
            LogUtility.DebugMethodStart();

            try
            {
                ListQue = new List<QUE_INFO>();
                ListDT_D10_DEL = new List<DT_D10>();
                ListDT_D10_MOD_DEL = new List<DT_D10_MOD>();

                foreach (var row in this.form.IchiranDgv1.Rows.Cast<DataGridViewRow>().Where(r => SqlBoolean.Parse(r.Cells["CHECKBOX"].Value.ToString()).IsTrue))
                {
                    Cls_QUE = new QUE_INFO();
                    Cls_QUE.KANRI_ID = row.Cells["管理番号"].Value.ToString();
                    Cls_QUE.QUE_SEQ = SqlInt16.Parse(row.Cells["QUE_SEQ"].Value.ToString());
                    Cls_QUE.STATUS_FLAG = 6;
                    Cls_QUE.UPDATE_TS = (DateTime)row.Cells["QUE_TIMESTAMP"].Value;
                    ListQue.Add(Cls_QUE);

                    if (row.Cells["操作CD"].Value.ToString() == "1")
                    {
                        Cls_Dt_D10 = new DT_D10();
                        Cls_Dt_D10.KANRI_ID = row.Cells["管理番号"].Value.ToString();
                        Cls_Dt_D10.UPDATE_TS = (DateTime)row.Cells["D10_TIMESTAMP"].Value;
                        ListDT_D10_DEL.Add(Cls_Dt_D10);
                    }
                    if (row.Cells["操作CD"].Value.ToString() == "2")
                    {
                        Cls_Dt_D10_Mod = new DT_D10_MOD();
                        Cls_Dt_D10_Mod.KANRI_ID = row.Cells["管理番号"].Value.ToString();
                        Cls_Dt_D10_Mod.UPDATE_TS = (DateTime)row.Cells["D10_MOD_TIMESTAMP"].Value;
                        ListDT_D10_MOD_DEL.Add(Cls_Dt_D10_Mod);
                    }
                }

                //Dao_D09_MOD
                using (Transaction tran = new Transaction())
                {
                    //更新処理
                    foreach (QUE_INFO QUE in ListQue)
                    {
                        int i = Dao_QUE.Update(QUE);
                    }
                    //削除処理
                    foreach (DT_D10 D10 in ListDT_D10_DEL)
                    {
                        int i = Dao_D10.Delete(D10);
                    }
                    //削除処理
                    foreach (DT_D10_MOD D10_MOD in ListDT_D10_MOD_DEL)
                    {
                        int i = Dao_D10_MOD.Delete(D10_MOD);
                    }
                    tran.Commit();
                    MessageBox.Show("登録が完了しました。");

                    // 再検索
                    Get_Search_TME();
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("HoryuDelete", ex1);
                MessageBoxUtility.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("HoryuDelete", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("HoryuDelete", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// JWNET処理
        /// </summary>
        [Transaction]
        public void JWEInsert()
        {
            LogUtility.DebugMethodStart();

            try
            {
                if (!this.form.IsRowSelected())
                {
                    new MessageBoxShowLogic().MessageBoxShowError("登録するデータを選択してください。");
                    return;
                }

                //更新用
                ListQue = new List<QUE_INFO>();
                ListDT_D10_UP = new List<DT_D10>();
                ListDT_D10_MOD_UP = new List<DT_D10_MOD>();
                ListDT_MF = new List<DT_MF_TOC>();
                //登録用
                ListDT_D10_IN = new List<DT_D10>();
                ListDT_D10_MOD_IN = new List<DT_D10_MOD>();
                //Queテーブルの管理番号とレコード枝番を保存する
                Que_Kanri_Seq = new System.Collections.Hashtable();

                TMEDtoCls Que_CD;
                //必須項目チェック
                if (this.form.SyoriKubun_Radio3.Checked)
                {
                    var errorRows = this.form.IchiranDgv1.Rows.Cast<DataGridViewRow>().Where(r => SqlBoolean.Parse(r.Cells["CHECKBOX"].Value.ToString()).IsTrue
                                                                                               && (r.Cells["操作CD"].Value == null || String.IsNullOrEmpty(r.Cells["操作CD"].Value.ToString())));
                    if (errorRows.Count() > 0)
                    {
                        this.form.IchiranDgv1.CurrentCell = errorRows.FirstOrDefault().Cells["操作CD"];
                        new MessageBoxShowLogic().MessageBoxShow("E001", "操作");
                        return;
                    }
                }

                foreach (var row in this.form.IchiranDgv1.Rows.Cast<DataGridViewRow>().Where(r => SqlBoolean.Parse(r.Cells["CHECKBOX"].Value.ToString()).IsTrue))
                {
                    var sousaCd = row.Cells["操作CD"].Value.ToString();
                    var haikibutsuJuryoubi = String.Empty;
                    if (row.Cells["廃棄物受領日"].Value != null && !String.IsNullOrEmpty(row.Cells["廃棄物受領日"].Value.ToString()))
                    {
                        haikibutsuJuryoubi = row.Cells["廃棄物受領日"].Value.ToString();
                        haikibutsuJuryoubi = haikibutsuJuryoubi.Replace("/", "");
                        haikibutsuJuryoubi = haikibutsuJuryoubi.Substring(0, 4) + "/" + haikibutsuJuryoubi.Substring(4, 2) + "/" + haikibutsuJuryoubi.Substring(6, 2);
                    }
                    var shobunShuuryoubi = String.Empty;
                    if (row.Cells["処分終了日"].Value != null && !String.IsNullOrEmpty(row.Cells["処分終了日"].Value.ToString()))
                    {
                        shobunShuuryoubi = row.Cells["処分終了日"].Value.ToString();
                        shobunShuuryoubi = shobunShuuryoubi.Replace("/", "");
                        shobunShuuryoubi = shobunShuuryoubi.Substring(0, 4) + "/" + shobunShuuryoubi.Substring(4, 2) + "/" + shobunShuuryoubi.Substring(6, 2);
                    }
                    if (sousaCd != "3")
                    {
                        if (String.IsNullOrEmpty(shobunShuuryoubi))
                        {
                            this.form.IchiranDgv1.CurrentCell = row.Cells["処分終了日"];
                            MessageBoxUtility.MessageBoxShow("E012", "処分終了日");
                            return;
                        }
                        else
                        {
                            DateTime dt1 = DateTime.Parse(row.Cells["HIKIWATASHIBI"].Value.ToString());
                            DateTime dt2 = DateTime.Parse(shobunShuuryoubi);
                            if (DateTime.Compare(dt1, dt2) > 0)
                            {
                                this.form.IchiranDgv1.CurrentCell = row.Cells["処分終了日"];
                                MessageBoxUtility.MessageBoxShow("E030", "引渡し日", "処分終了日");
                                return;
                            }
                            // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                            //else if (DateTime.Today < dt2)
                            else if (this.parentForm.sysDate.Date < dt2)
                            // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                            {
                                this.form.IchiranDgv1.CurrentCell = row.Cells["処分終了日"];
                                MessageBoxUtility.MessageBoxShow("E034", "処分終了日には今日以前の日付");
                                return;
                            }
                            else if (!String.IsNullOrEmpty(haikibutsuJuryoubi))
                            {
                                DateTime hkbDate = DateTime.Parse(haikibutsuJuryoubi);
                                if (dt2 < hkbDate)
                                {
                                    this.form.IchiranDgv1.CurrentCell = row.Cells["処分終了日"];
                                    MessageBoxUtility.MessageBoxShow("E030", "廃棄物受領日", "処分終了日");
                                    return;
                                }
                            }

                            var sbnEndDate = new DateTime();
                            // 修正時は処分終了報告日も確認
                            if (sousaCd == "2"
                                && row.Cells["SBN_END_REP_DATE"].Value != null
                                && ToDateTime(row.Cells["SBN_END_REP_DATE"].Value.ToString(), out sbnEndDate))
                            {
                                if (sbnEndDate < dt2)
                                {
                                    this.form.IchiranDgv1.CurrentCell = row.Cells["処分終了日"];
                                    MessageBoxUtility.MessageBoxShow("E034", "処分終了日には処分終了報告日以前の日付");
                                    return;
                                }
                            }
                        }

                        if (row.Cells["処分担当者"].Value == null)
                        {
                            this.form.IchiranDgv1.CurrentCell = row.Cells["処分担当者"];
                            MessageBoxUtility.MessageBoxShow("E012", "処分担当者");
                            return;
                        }
                        if (row.Cells["処分担当者"].Value.ToString() == "")
                        {
                            this.form.IchiranDgv1.CurrentCell = row.Cells["処分担当者"];
                            MessageBoxUtility.MessageBoxShow("E012", "処分担当者");
                            return;
                        }
                        if (row.Cells["報告区分CD"].Value.ToString() == "" || row.Cells["報告区分CD"].Value == null)
                        {
                            this.form.IchiranDgv1.CurrentCell = row.Cells["報告区分CD"];
                            MessageBoxUtility.MessageBoxShow("E012", "報告区分CD");
                            return;
                        }

                        //廃棄物受領日
                        if (!String.IsNullOrEmpty(haikibutsuJuryoubi))
                        {
                            //引き渡し日比較
                            DateTime dt1 = DateTime.Parse(row.Cells["HIKIWATASHIBI"].Value.ToString());
                            DateTime dt2 = DateTime.Parse(haikibutsuJuryoubi);
                            if (DateTime.Compare(dt1, dt2) > 0)
                            {
                                this.form.IchiranDgv1.CurrentCell = row.Cells["廃棄物受領日"];
                                MessageBoxUtility.MessageBoxShow("E030", "引渡し日", "廃棄物受領日");
                                return;
                            }
                            // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                            //else if (DateTime.Today < dt2)
                            else if (this.parentForm.sysDate.Date < dt2)
                            // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                            {
                                this.form.IchiranDgv1.CurrentCell = row.Cells["廃棄物受領日"];
                                MessageBoxUtility.MessageBoxShow("E034", "廃棄物受領日には今日以前の日付");
                                return;
                            }

                            var sbnEndDate = new DateTime();
                            // 修正時は処分終了報告日も確認
                            if (sousaCd == "2"
                                && row.Cells["SBN_END_REP_DATE"].Value != null
                                && ToDateTime(row.Cells["SBN_END_REP_DATE"].Value.ToString(), out sbnEndDate))
                            {
                                if (sbnEndDate < dt2)
                                {
                                    this.form.IchiranDgv1.CurrentCell = row.Cells["廃棄物受領日"];
                                    MessageBoxUtility.MessageBoxShow("E034", "廃棄物受領日には処分終了報告日以前の日付");
                                    return;
                                }
                            }


                            //処分終了日比較
                            dt1 = DateTime.Parse(haikibutsuJuryoubi);
                            dt2 = DateTime.Parse(shobunShuuryoubi);
                            if (DateTime.Compare(dt1, dt2) > 0)
                            {
                                this.form.IchiranDgv1.CurrentCell = row.Cells["処分終了日"];
                                MessageBoxUtility.MessageBoxShow("E030", "廃棄物受領日", "処分終了日");
                                return;
                            }

                        }

                        //受入量
                        if (string.IsNullOrEmpty(row.Cells["受入量"].Value.ToString())
                            && !string.IsNullOrEmpty(row.Cells["受入量単位CD"].Value.ToString()))
                        {
                            this.form.IchiranDgv1.CurrentCell = row.Cells["受入量"];
                            MessageBoxUtility.MessageBoxShow("E001", "受入量");
                            return;
                        }

                        //受入量単位CD
                        if (!string.IsNullOrEmpty(row.Cells["受入量"].Value.ToString())
                            && string.IsNullOrEmpty(row.Cells["受入量単位CD"].Value.ToString()))
                        {
                            this.form.IchiranDgv1.CurrentCell = row.Cells["受入量単位CD"];
                            MessageBoxUtility.MessageBoxShow("E001", "受入量単位CD");
                            return;
                        }
                    }
                }

                //data取得
                foreach (var row in this.form.IchiranDgv1.Rows.Cast<DataGridViewRow>().Where(r => SqlBoolean.Parse(r.Cells["CHECKBOX"].Value.ToString()).IsTrue))
                {
                    var haikibutsuJuryoubi = String.Empty;
                    if (row.Cells["廃棄物受領日"].Value != null && !String.IsNullOrEmpty(row.Cells["廃棄物受領日"].Value.ToString()))
                    {
                        haikibutsuJuryoubi = row.Cells["廃棄物受領日"].Value.ToString();
                        haikibutsuJuryoubi = haikibutsuJuryoubi.Replace("/", "");
                        haikibutsuJuryoubi = haikibutsuJuryoubi.Substring(0, 4) + "/" + haikibutsuJuryoubi.Substring(4, 2) + "/" + haikibutsuJuryoubi.Substring(6, 2);
                    }
                    var shobunShuuryoubi = String.Empty;
                    if (row.Cells["処分終了日"].Value != null && !String.IsNullOrEmpty(row.Cells["処分終了日"].Value.ToString()))
                    {
                        shobunShuuryoubi = row.Cells["処分終了日"].Value.ToString();
                        shobunShuuryoubi = shobunShuuryoubi.Replace("/", "");
                        shobunShuuryoubi = shobunShuuryoubi.Substring(0, 4) + "/" + shobunShuuryoubi.Substring(4, 2) + "/" + shobunShuuryoubi.Substring(6, 2);
                    }

                    //DT_D09
                    Cls_Dt_D10 = new DT_D10();
                    if (!String.IsNullOrEmpty(shobunShuuryoubi))
                    {
                        Cls_Dt_D10.SBN_END_DATE = shobunShuuryoubi.Replace("/", "");
                    }
                    if (!String.IsNullOrEmpty(haikibutsuJuryoubi))
                    {
                        Cls_Dt_D10.HAIKI_IN_DATE = haikibutsuJuryoubi.Replace("/", "");
                    }
                    if (row.Cells["車輌"].Value != null)
                    {
                        Cls_Dt_D10.CAR_NO = row.Cells["車輌"].Value.ToString();
                    }
                    if (row.Cells["受入量"].Value.ToString() != "")
                    {
                        Cls_Dt_D10.RECEPT_SUU = SqlDecimal.Parse(row.Cells["受入量"].Value.ToString().Replace(",", ""));
                    }
                    if (row.Cells["受入量単位CD"].Value.ToString() != "")
                    {
                        Cls_Dt_D10.RECEPT_UNIT_CODE = row.Cells["受入量単位CD"].Value.ToString().Trim('0');
                    }
                    if (row.Cells["備考"].Value.ToString() != "")
                    {
                        Cls_Dt_D10.BIKOU = row.Cells["備考"].Value.ToString();
                    }
                    if (row.Cells["運搬担当者"].Value.ToString() != "")
                    {
                        Cls_Dt_D10.UPN_TAN_NAME = row.Cells["運搬担当者"].Value.ToString();
                    }
                    if (row.Cells["報告担当者"].Value.ToString() != "")
                    {
                        Cls_Dt_D10.REP_TAN_NAME = row.Cells["報告担当者"].Value.ToString();
                    }
                    if (row.Cells["処分担当者"].Value.ToString() != "")
                    {
                        Cls_Dt_D10.SBN_TAN_NAME = row.Cells["処分担当者"].Value.ToString();
                    }
                    if (row.Cells["報告区分CD"].Value.ToString() != "")
                    {
                        Cls_Dt_D10.REP_KBN = row.Cells["報告区分CD"].Value.ToString();
                    }
                    Cls_Dt_D10.KANRI_ID = row.Cells["管理番号"].Value.ToString();

                    //Dt_D09_Mod
                    Cls_Dt_D10_Mod = new DT_D10_MOD();
                    if (!String.IsNullOrEmpty(shobunShuuryoubi))
                    {
                        Cls_Dt_D10_Mod.SBN_END_DATE = shobunShuuryoubi.Replace("/", "");
                    }
                    if (!String.IsNullOrEmpty(haikibutsuJuryoubi))
                    {
                        Cls_Dt_D10_Mod.HAIKI_IN_DATE = haikibutsuJuryoubi.Replace("/", "");
                    }
                    if (row.Cells["車輌"].Value != null)
                    {
                        Cls_Dt_D10_Mod.CAR_NO = row.Cells["車輌"].Value.ToString();
                    }
                    if (row.Cells["受入量"].Value.ToString() != "")
                    {
                        Cls_Dt_D10_Mod.RECEPT_SUU = SqlDecimal.Parse(row.Cells["受入量"].Value.ToString().Replace(",", ""));
                    }
                    if (row.Cells["受入量単位CD"].Value.ToString() != "")
                    {
                        Cls_Dt_D10_Mod.RECEPT_UNIT_CODE = row.Cells["受入量単位CD"].Value.ToString().Trim('0');
                    }
                    if (row.Cells["備考"].Value.ToString() != "")
                    {
                        Cls_Dt_D10_Mod.BIKOU = row.Cells["備考"].Value.ToString();
                    }
                    if (row.Cells["運搬担当者"].Value.ToString() != "")
                    {
                        Cls_Dt_D10_Mod.UPN_TAN_NAME = row.Cells["運搬担当者"].Value.ToString();
                    }
                    if (row.Cells["報告担当者"].Value.ToString() != "")
                    {
                        Cls_Dt_D10_Mod.REP_TAN_NAME = row.Cells["報告担当者"].Value.ToString();
                    }
                    if (row.Cells["処分担当者"].Value.ToString() != "")
                    {
                        Cls_Dt_D10_Mod.SBN_TAN_NAME = row.Cells["処分担当者"].Value.ToString();
                    }
                    if (row.Cells["報告区分CD"].Value.ToString() != "")
                    {
                        Cls_Dt_D10_Mod.REP_KBN = row.Cells["報告区分CD"].Value.ToString();
                    }
                    Cls_Dt_D10_Mod.KANRI_ID = row.Cells["管理番号"].Value.ToString();


                    //QUE
                    Cls_QUE = new QUE_INFO();
                    var Ichiran_Kanri = row.Cells["管理番号"].Value.ToString();
                    Cls_QUE.KANRI_ID = Ichiran_Kanri;
                    if (Que_Kanri_Seq.Contains(Ichiran_Kanri))
                    {
                        object obj = Que_Kanri_Seq[Ichiran_Kanri];
                        Cls_QUE.QUE_SEQ = SqlInt16.Parse(obj.ToString()) + 1;
                        Que_Kanri_Seq.Remove(Ichiran_Kanri);
                        Que_Kanri_Seq.Add(Ichiran_Kanri, Cls_QUE.QUE_SEQ);
                    }
                    else
                    {
                        if (SyoriKubunHoryu.Equals(searchSyoriKubunCD))
                        {
                            // 送信保留→JWNET送信
                            Cls_QUE.QUE_SEQ = SqlInt16.Parse(row.Cells["QUE_SEQ"].Value.ToString());
                            Cls_QUE.UPDATE_TS = (DateTime)row.Cells["QUE_TIMESTAMP"].Value;
                            Cls_QUE.TRF_STATUS = 0;
                        }
                        else
                        {
                            Que_CD = new TMEDtoCls();
                            Que_CD.Search_CD = Cls_QUE.KANRI_ID;
                            //QueSeq取得
                            var entity = Dao_QUE.GetDataForEntity(Que_CD);
                            if (entity != null)
                            {
                                // 送信保留時以外はINSERTのためインクリメント
                                if (!SyoriKubunHoryu.Equals(searchSyoriKubunCD))
                                {
                                    Cls_QUE.QUE_SEQ = entity.QUE_SEQ + 1;
                                }
                                else
                                {
                                    Cls_QUE.QUE_SEQ = entity.QUE_SEQ;
                                    Cls_QUE.UPDATE_TS = entity.UPDATE_TS;
                                    Cls_QUE.TRF_STATUS = entity.TRF_STATUS;
                                }
                            }
                            else
                            {
                                Cls_QUE.QUE_SEQ = 1;
                            }
                        }

                        Que_Kanri_Seq.Add(Cls_QUE.KANRI_ID, Cls_QUE.QUE_SEQ);
                    }

                    Cls_QUE.SEQ = SqlInt16.Parse(row.Cells["枝番"].Value.ToString());
                    Cls_QUE.STATUS_FLAG = 0;
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //Cls_QUE.CREATE_DATE = System.DateTime.Now;
                    Cls_QUE.CREATE_DATE = this.getDBDateTime();
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end

                    if (SyoriKubunShuryo.Equals(this.searchSyoriKubunCD))
                    {
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                        //Cls_Dt_D10.CREATE_DATE = System.DateTime.Now;
                        Cls_Dt_D10.CREATE_DATE = this.getDBDateTime();
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                        //dt_d09の登録リストに追加
                        ListDT_D10_IN.Add(Cls_Dt_D10);
                        //queのFUNCTION_ID設定
                        Cls_QUE.FUNCTION_ID = "1500";
                    }
                    else if (SyoriKubunHoryu.Equals(this.searchSyoriKubunCD))
                    {
                        if (row.Cells["操作CD"].Value.ToString() == "1")
                        {
                            Cls_Dt_D10.UPDATE_TS = (DateTime)row.Cells["D10_TIMESTAMP"].Value;
                            //dt_d09の更新リストに追加
                            ListDT_D10_UP.Add(Cls_Dt_D10);
                        }
                        else
                        {
                            if (row.Cells["操作CD"].Value.ToString() == "2")
                            {
                                //DT_D09_MODの更新リストに追加
                                Cls_Dt_D10_Mod.UPDATE_TS = (DateTime)row.Cells["D10_MOD_TIMESTAMP"].Value;
                                ListDT_D10_MOD_UP.Add(Cls_Dt_D10_Mod);
                            }
                        }

                        Cls_QUE.FUNCTION_ID = row.Cells["機能番号"].Value.ToString();

                        //Dt_Mf_Tocの更新用リスト
                        if (row.Cells["操作CD"].Value.ToString() != "1")
                        {
                            Cls_Dt_Mf_Toc = new DT_MF_TOC();
                            Cls_Dt_Mf_Toc.KANRI_ID = row.Cells["管理番号"].Value.ToString();
                            Cls_Dt_Mf_Toc.STATUS_DETAIL = 1;
                            Cls_Dt_Mf_Toc.UPDATE_TS = (DateTime)row.Cells["MF_TIMESTAMP"].Value;
                            ListDT_MF.Add(Cls_Dt_Mf_Toc);
                        }

                    }
                    else
                    {
                        //DT_D09_MODの登録リストに追加
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                        //Cls_Dt_D10_Mod.CREATE_DATE = System.DateTime.Now;
                        Cls_Dt_D10_Mod.CREATE_DATE = this.getDBDateTime();
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                        if (row.Cells["操作CD"].Value.ToString() == "2")
                        {
                            ListDT_D10_MOD_IN.Add(Cls_Dt_D10_Mod);
                        }

                        //queのFUNCTION_ID設定
                        if (row.Cells["操作CD"].Value.ToString() == "2")
                        {
                            Cls_QUE.FUNCTION_ID = "1600";
                        }
                        else
                        {
                            Cls_QUE.FUNCTION_ID = "1800";
                        }

                        //Dt_Mf_Tocの更新用リスト
                        Cls_Dt_Mf_Toc = new DT_MF_TOC();
                        Cls_Dt_Mf_Toc.KANRI_ID = row.Cells["管理番号"].Value.ToString();
                        Cls_Dt_Mf_Toc.STATUS_DETAIL = 1;
                        Cls_Dt_Mf_Toc.UPDATE_TS = (DateTime)row.Cells["MF_TIMESTAMP"].Value;
                        ListDT_MF.Add(Cls_Dt_Mf_Toc);

                    }
                    //que更新リストに追加
                    if (!SyoriKubunHoryu.Equals(searchSyoriKubunCD))
                    {
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                        //Cls_QUE.UPDATE_TS = System.DateTime.Now;
                        Cls_QUE.UPDATE_TS = this.getDBDateTime();
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    }
                    ListQue.Add(Cls_QUE);
                }

                using (Transaction tran = new Transaction())
                {
                    if (SyoriKubunShuryo.Equals(searchSyoriKubunCD))
                    {
                        //DT_D09登録処理
                        foreach (DT_D10 DT in ListDT_D10_IN)
                        {
                            // ゴミが残っている場合があるためチェック
                            var delD10Data = Dao_D10.GetD10(DT.KANRI_ID);
                            foreach (var delData in delD10Data)
                            {
                                Dao_D10.Delete(delData);
                            }

                            int i = Dao_D10.Insert(DT);
                        }
                    }
                    else if (SyoriKubunHoryu.Equals(searchSyoriKubunCD))
                    {
                        //DT_D09更新処理
                        foreach (DT_D10 DT in ListDT_D10_UP)
                        {
                            int i = Dao_D10.Update(DT);
                        }
                        //ListDT_D09_MOD_IN更新処理
                        foreach (DT_D10_MOD DT in ListDT_D10_MOD_UP)
                        {
                            int i = Dao_D10_MOD.Update(DT);
                        }
                        //DT_MF_TOC更新処理
                        foreach (DT_MF_TOC DT in ListDT_MF)
                        {
                            int i = Dao_DT_MF_TOC.Update(DT);
                        }
                    }
                    else
                    {
                        //ListDT_D09_MOD_IN登録処理
                        foreach (DT_D10_MOD DT in ListDT_D10_MOD_IN)
                        {
                            int i = Dao_D10_MOD.Insert(DT);
                        }
                        //DT_MF_TOC更新処理
                        foreach (DT_MF_TOC DT in ListDT_MF)
                        {
                            int i = Dao_DT_MF_TOC.Update(DT);
                        }
                    }

                    //QUE_INFO登録処理
                    foreach (QUE_INFO QUE in ListQue)
                    {
                        if (SyoriKubunHoryu.Equals(searchSyoriKubunCD))
                        {
                            int i = Dao_QUE.Update(QUE);
                        }
                        else
                        {
                            int i = Dao_QUE.Insert(QUE);
                        }
                    }

                    tran.Commit();
                    MessageBox.Show("登録が完了しました。");

                    bool catchErr = false;
                    bool retCheck = this.DateCheck(out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (retCheck)
                    {
                        return;
                    }

                    // 再検索
                    Get_Search_TME();
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("JWEInsert", ex1);
                MessageBoxUtility.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("JWEInsert", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("JWEInsert", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// 保留保存処理
        /// </summary>
        [Transaction]
        public void HoryuInsert()
        {
            LogUtility.DebugMethodStart();

            try
            {
                if (!this.form.IsRowSelected())
                {
                    new MessageBoxShowLogic().MessageBoxShowError("登録するデータを選択してください。");
                    return;
                }

                //更新用
                ListQue = new List<QUE_INFO>();
                ListDT_D10_UP = new List<DT_D10>();
                ListDT_D10_MOD_UP = new List<DT_D10_MOD>();
                ListDT_MF = new List<DT_MF_TOC>();
                //登録用
                ListDT_D10_IN = new List<DT_D10>();
                ListDT_D10_MOD_IN = new List<DT_D10_MOD>();
                //Queテーブルの管理番号とレコード枝番を保存する
                Que_Kanri_Seq = new System.Collections.Hashtable();

                TMEDtoCls Que_CD;

                //必須項目チェック
                if (this.form.SyoriKubun_Radio3.Checked)
                {
                    var errorRows = this.form.IchiranDgv1.Rows.Cast<DataGridViewRow>().Where(r => SqlBoolean.Parse(r.Cells["CHECKBOX"].Value.ToString()).IsTrue
                                                                                               && (r.Cells["操作CD"].Value == null || String.IsNullOrEmpty(r.Cells["操作CD"].Value.ToString())));
                    if (errorRows.Count() > 0)
                    {
                        this.form.IchiranDgv1.CurrentCell = errorRows.FirstOrDefault().Cells["操作CD"];
                        new MessageBoxShowLogic().MessageBoxShow("E001", "操作");
                        return;
                    }
                }

                foreach (var row in this.form.IchiranDgv1.Rows.Cast<DataGridViewRow>().Where(r => SqlBoolean.Parse(r.Cells["CHECKBOX"].Value.ToString()).IsTrue))
                {
                    var sousaCd = row.Cells["操作CD"].Value.ToString();
                    var haikibutsuJuryoubi = String.Empty;
                    if (row.Cells["廃棄物受領日"].Value != null && !String.IsNullOrEmpty(row.Cells["廃棄物受領日"].Value.ToString()))
                    {
                        haikibutsuJuryoubi = row.Cells["廃棄物受領日"].Value.ToString();
                        haikibutsuJuryoubi = haikibutsuJuryoubi.Replace("/", "");
                        haikibutsuJuryoubi = haikibutsuJuryoubi.Substring(0, 4) + "/" + haikibutsuJuryoubi.Substring(4, 2) + "/" + haikibutsuJuryoubi.Substring(6, 2);
                    }
                    var shobunShuuryoubi = String.Empty;
                    if (row.Cells["処分終了日"].Value != null && !String.IsNullOrEmpty(row.Cells["処分終了日"].Value.ToString()))
                    {
                        shobunShuuryoubi = row.Cells["処分終了日"].Value.ToString();
                        shobunShuuryoubi = shobunShuuryoubi.Replace("/", "");
                        shobunShuuryoubi = shobunShuuryoubi.Substring(0, 4) + "/" + shobunShuuryoubi.Substring(4, 2) + "/" + shobunShuuryoubi.Substring(6, 2);
                    }
                    if (sousaCd != "3")
                    {
                        if (String.IsNullOrEmpty(shobunShuuryoubi))
                        {
                            this.form.IchiranDgv1.CurrentCell = row.Cells["処分終了日"];
                            MessageBoxUtility.MessageBoxShow("E012", "処分終了日");
                            return;
                        }
                        else
                        {
                            DateTime dt1 = DateTime.Parse(row.Cells["HIKIWATASHIBI"].Value.ToString());
                            DateTime dt2 = DateTime.Parse(shobunShuuryoubi);
                            if (DateTime.Compare(dt1, dt2) > 0)
                            {
                                this.form.IchiranDgv1.CurrentCell = row.Cells["処分終了日"];
                                MessageBoxUtility.MessageBoxShow("E030", "引渡し日", "処分終了日");
                                return;
                            }
                            // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                            //else if (DateTime.Today < dt2)
                            else if (this.parentForm.sysDate.Date < dt2)
                            // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                            {
                                this.form.IchiranDgv1.CurrentCell = row.Cells["処分終了日"];
                                MessageBoxUtility.MessageBoxShow("E034", "処分終了日には今日以前の日付");
                                return;
                            }
                            else if (!String.IsNullOrEmpty(haikibutsuJuryoubi))
                            {
                                DateTime hkbDate = DateTime.Parse(haikibutsuJuryoubi);
                                if (dt2 < hkbDate)
                                {
                                    this.form.IchiranDgv1.CurrentCell = row.Cells["処分終了日"];
                                    MessageBoxUtility.MessageBoxShow("E030", "廃棄物受領日", "処分終了日");
                                    return;
                                }
                            }

                            var sbnEndDate = new DateTime();
                            // 修正時は処分終了報告日も確認
                            if (sousaCd == "2"
                                && row.Cells["SBN_END_REP_DATE"].Value != null
                                && ToDateTime(row.Cells["SBN_END_REP_DATE"].Value.ToString(), out sbnEndDate))
                            {
                                if (sbnEndDate < dt2)
                                {
                                    this.form.IchiranDgv1.CurrentCell = row.Cells["処分終了日"];
                                    MessageBoxUtility.MessageBoxShow("E034", "処分終了日には処分終了報告日以前の日付");
                                    return;
                                }
                            }
                        }
                        //2013.12.26 touti 追加　引渡し日と運搬終了日比較チェック　end

                        if (row.Cells["処分担当者"].Value == null)
                        {
                            this.form.IchiranDgv1.CurrentCell = row.Cells["処分担当者"];
                            MessageBoxUtility.MessageBoxShow("E012", "処分担当者");
                            return;
                        }
                        if (row.Cells["処分担当者"].Value.ToString() == "")
                        {
                            this.form.IchiranDgv1.CurrentCell = row.Cells["処分担当者"];
                            MessageBoxUtility.MessageBoxShow("E012", "処分担当者");
                            return;
                        }
                        if (row.Cells["報告区分CD"].Value.ToString() == "" || row.Cells["報告区分CD"].Value == null)
                        {
                            this.form.IchiranDgv1.CurrentCell = row.Cells["報告区分CD"];
                            MessageBoxUtility.MessageBoxShow("E012", "報告区分CD");
                            return;
                        }

                        //廃棄物受領日
                        if (!String.IsNullOrEmpty(haikibutsuJuryoubi))
                        {
                            //引き渡し日比較
                            DateTime dt1 = DateTime.Parse(row.Cells["HIKIWATASHIBI"].Value.ToString());
                            DateTime dt2 = DateTime.Parse(haikibutsuJuryoubi);
                            if (DateTime.Compare(dt1, dt2) > 0)
                            {
                                this.form.IchiranDgv1.CurrentCell = row.Cells["廃棄物受領日"];
                                MessageBoxUtility.MessageBoxShow("E030", "引渡し日", "廃棄物受領日");
                                return;
                            }

                            //処分終了日比較
                            dt1 = DateTime.Parse(haikibutsuJuryoubi);
                            dt2 = DateTime.Parse(shobunShuuryoubi);
                            if (DateTime.Compare(dt1, dt2) > 0)
                            {
                                this.form.IchiranDgv1.CurrentCell = row.Cells["処分終了日"];
                                MessageBoxUtility.MessageBoxShow("E030", "廃棄物受領日", "処分終了日");
                                return;
                            }
                            // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                            //else if (DateTime.Today < dt2)
                            else if (this.parentForm.sysDate.Date < dt2)
                            // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                            {
                                this.form.IchiranDgv1.CurrentCell = row.Cells["廃棄物受領日"];
                                MessageBoxUtility.MessageBoxShow("E034", "廃棄物受領日には今日以前の日付");
                                return;
                            }

                            var sbnEndDate = new DateTime();
                            // 修正時は処分終了報告日も確認
                            if (sousaCd == "2"
                                && row.Cells["SBN_END_REP_DATE"].Value != null
                                && ToDateTime(row.Cells["SBN_END_REP_DATE"].Value.ToString(), out sbnEndDate))
                            {
                                if (sbnEndDate < dt2)
                                {
                                    this.form.IchiranDgv1.CurrentCell = row.Cells["廃棄物受領日"];
                                    MessageBoxUtility.MessageBoxShow("E034", "廃棄物受領日には処分終了報告日以前の日付");
                                    return;
                                }
                            }

                        }

                        //受入量
                        if (string.IsNullOrEmpty(row.Cells["受入量"].Value.ToString())
                            && !string.IsNullOrEmpty(row.Cells["受入量単位CD"].Value.ToString()))
                        {
                            this.form.IchiranDgv1.CurrentCell = row.Cells["受入量"];
                            MessageBoxUtility.MessageBoxShow("E001", "受入量");
                            return;
                        }

                        //受入量単位CD
                        if (!string.IsNullOrEmpty(row.Cells["受入量"].Value.ToString())
                            && string.IsNullOrEmpty(row.Cells["受入量単位CD"].Value.ToString()))
                        {
                            this.form.IchiranDgv1.CurrentCell = row.Cells["受入量単位CD"];
                            MessageBoxUtility.MessageBoxShow("E001", "受入量単位CD");
                            return;
                        }
                    }
                }
                // エンティティ作成
                foreach (var row in this.form.IchiranDgv1.Rows.Cast<DataGridViewRow>().Where(r => SqlBoolean.Parse(r.Cells["CHECKBOX"].Value.ToString()).IsTrue))
                {
                    var haikibutsuJuryoubi = String.Empty;
                    if (row.Cells["廃棄物受領日"].Value != null && !String.IsNullOrEmpty(row.Cells["廃棄物受領日"].Value.ToString()))
                    {
                        haikibutsuJuryoubi = row.Cells["廃棄物受領日"].Value.ToString();
                        haikibutsuJuryoubi = haikibutsuJuryoubi.Replace("/", "");
                        haikibutsuJuryoubi = haikibutsuJuryoubi.Substring(0, 4) + "/" + haikibutsuJuryoubi.Substring(4, 2) + "/" + haikibutsuJuryoubi.Substring(6, 2);
                    }
                    var shobunShuuryoubi = String.Empty;
                    if (row.Cells["処分終了日"].Value != null && !String.IsNullOrEmpty(row.Cells["処分終了日"].Value.ToString()))
                    {
                        shobunShuuryoubi = row.Cells["処分終了日"].Value.ToString();
                        shobunShuuryoubi = shobunShuuryoubi.Replace("/", "");
                        shobunShuuryoubi = shobunShuuryoubi.Substring(0, 4) + "/" + shobunShuuryoubi.Substring(4, 2) + "/" + shobunShuuryoubi.Substring(6, 2);
                    }

                    //DT_D09
                    Cls_Dt_D10 = new DT_D10();
                    if (!String.IsNullOrEmpty(shobunShuuryoubi))
                    {
                        Cls_Dt_D10.SBN_END_DATE = shobunShuuryoubi.Replace("/", "");
                    }
                    if (!String.IsNullOrEmpty(haikibutsuJuryoubi))
                    {
                        Cls_Dt_D10.HAIKI_IN_DATE = haikibutsuJuryoubi.Replace("/", "");
                    }
                    if (row.Cells["車輌"].Value != null)
                    {
                        Cls_Dt_D10.CAR_NO = row.Cells["車輌"].Value.ToString();
                    }
                    if (row.Cells["受入量"].Value.ToString() != "")
                    {
                        Cls_Dt_D10.RECEPT_SUU = SqlDecimal.Parse(row.Cells["受入量"].Value.ToString().Replace(",", ""));
                    }
                    if (row.Cells["受入量単位CD"].Value.ToString() != "")
                    {
                        Cls_Dt_D10.RECEPT_UNIT_CODE = row.Cells["受入量単位CD"].Value.ToString().Trim('0');
                    }
                    if (row.Cells["備考"].Value.ToString() != "")
                    {
                        Cls_Dt_D10.BIKOU = row.Cells["備考"].Value.ToString();
                    }
                    if (row.Cells["運搬担当者"].Value.ToString() != "")
                    {
                        Cls_Dt_D10.UPN_TAN_NAME = row.Cells["運搬担当者"].Value.ToString();
                    }
                    if (row.Cells["報告担当者"].Value.ToString() != "")
                    {
                        Cls_Dt_D10.REP_TAN_NAME = row.Cells["報告担当者"].Value.ToString();
                    }
                    if (row.Cells["処分担当者"].Value.ToString() != "")
                    {
                        Cls_Dt_D10.SBN_TAN_NAME = row.Cells["処分担当者"].Value.ToString();
                    }
                    if (row.Cells["報告区分CD"].Value.ToString() != "")
                    {
                        Cls_Dt_D10.REP_KBN = row.Cells["報告区分CD"].Value.ToString();
                    }
                    Cls_Dt_D10.KANRI_ID = row.Cells["管理番号"].Value.ToString();

                    //Dt_D09_Mod
                    Cls_Dt_D10_Mod = new DT_D10_MOD();
                    if (!String.IsNullOrEmpty(shobunShuuryoubi))
                    {
                        Cls_Dt_D10_Mod.SBN_END_DATE = shobunShuuryoubi.Replace("/", "");
                    }
                    if (!String.IsNullOrEmpty(haikibutsuJuryoubi))
                    {
                        Cls_Dt_D10_Mod.HAIKI_IN_DATE = haikibutsuJuryoubi.Replace("/", "");
                    }
                    if (row.Cells["車輌"].Value != null)
                    {
                        Cls_Dt_D10_Mod.CAR_NO = row.Cells["車輌"].Value.ToString();
                    }
                    if (row.Cells["受入量"].Value.ToString() != "")
                    {
                        Cls_Dt_D10_Mod.RECEPT_SUU = SqlDecimal.Parse(row.Cells["受入量"].Value.ToString().Replace(",", ""));
                    }
                    if (row.Cells["受入量単位CD"].Value.ToString() != "")
                    {
                        Cls_Dt_D10_Mod.RECEPT_UNIT_CODE = row.Cells["受入量単位CD"].Value.ToString().Trim('0');
                    }
                    if (row.Cells["備考"].Value.ToString() != "")
                    {
                        Cls_Dt_D10_Mod.BIKOU = row.Cells["備考"].Value.ToString();
                    }
                    if (row.Cells["運搬担当者"].Value.ToString() != "")
                    {
                        Cls_Dt_D10_Mod.UPN_TAN_NAME = row.Cells["運搬担当者"].Value.ToString();
                    }
                    if (row.Cells["報告担当者"].Value.ToString() != "")
                    {
                        Cls_Dt_D10_Mod.REP_TAN_NAME = row.Cells["報告担当者"].Value.ToString();
                    }
                    if (row.Cells["処分担当者"].Value.ToString() != "")
                    {
                        Cls_Dt_D10_Mod.SBN_TAN_NAME = row.Cells["処分担当者"].Value.ToString();
                    }
                    if (row.Cells["報告区分CD"].Value.ToString() != "")
                    {
                        Cls_Dt_D10_Mod.REP_KBN = row.Cells["報告区分CD"].Value.ToString();
                    }
                    Cls_Dt_D10_Mod.KANRI_ID = row.Cells["管理番号"].Value.ToString();

                    //QUE
                    Cls_QUE = new QUE_INFO();
                    var Ichiran_Kanri = row.Cells["管理番号"].Value.ToString();
                    Cls_QUE.KANRI_ID = Ichiran_Kanri;
                    //QUEのレコード枝番設定(同じ管理番号で複数の件数のばあい、hashtableから枝番を取得し+１する)
                    if (Que_Kanri_Seq.Contains(Ichiran_Kanri))
                    {
                        object obj = Que_Kanri_Seq[Ichiran_Kanri];
                        Cls_QUE.QUE_SEQ = SqlInt16.Parse(obj.ToString()) + 1;
                        Que_Kanri_Seq.Remove(Ichiran_Kanri);
                        Que_Kanri_Seq.Add(Ichiran_Kanri, Cls_QUE.QUE_SEQ);
                    }
                    //管理番号が重なっていない場合、テーブルから最大値を取得
                    else
                    {
                        Que_CD = new TMEDtoCls();
                        Que_CD.Search_CD = Cls_QUE.KANRI_ID;
                        //QueSeq取得
                        var entity = Dao_QUE.GetDataForEntity(Que_CD);
                        if (entity != null)
                        {
                            // 送信保留時以外はINSERTのためインクリメント
                            if (!SyoriKubunHoryu.Equals(searchSyoriKubunCD))
                            {
                                Cls_QUE.QUE_SEQ = entity.QUE_SEQ + 1;
                            }
                            else
                            {
                                Cls_QUE.QUE_SEQ = entity.QUE_SEQ;
                                Cls_QUE.UPDATE_TS = entity.UPDATE_TS;
                                Cls_QUE.TRF_STATUS = entity.TRF_STATUS;
                            }
                        }
                        else
                        {
                            Cls_QUE.QUE_SEQ = 1;
                        }
                        Que_Kanri_Seq.Add(Cls_QUE.KANRI_ID, Cls_QUE.QUE_SEQ);
                    }
                    Cls_QUE.SEQ = SqlInt16.Parse(row.Cells["枝番"].Value.ToString());
                    Cls_QUE.STATUS_FLAG = 7;
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //Cls_QUE.CREATE_DATE = System.DateTime.Now;
                    Cls_QUE.CREATE_DATE = this.getDBDateTime();
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end

                    if (SyoriKubunShuryo.Equals(this.searchSyoriKubunCD))
                    {
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                        //Cls_Dt_D10.CREATE_DATE = System.DateTime.Now;
                        Cls_Dt_D10.CREATE_DATE = this.getDBDateTime();
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                        //dt_d09の登録リストに追加
                        ListDT_D10_IN.Add(Cls_Dt_D10);
                        //queのFUNCTION_ID設定
                        Cls_QUE.FUNCTION_ID = "1500";
                    }
                    else if (SyoriKubunHoryu.Equals(this.searchSyoriKubunCD))
                    {
                        if (row.Cells["操作CD"].Value.ToString() == "1")
                        {
                            Cls_Dt_D10.UPDATE_TS = (DateTime)row.Cells["D10_TIMESTAMP"].Value;
                            //dt_d09の更新リストに追加
                            ListDT_D10_UP.Add(Cls_Dt_D10);
                        }
                        else
                        {
                            if (row.Cells["操作CD"].Value.ToString() == "2")
                            {
                                Cls_Dt_D10_Mod.UPDATE_TS = (DateTime)row.Cells["D10_MOD_TIMESTAMP"].Value;
                                //DT_D09_MODの更新リストに追加
                                ListDT_D10_MOD_UP.Add(Cls_Dt_D10_Mod);
                            }
                        }

                        Cls_QUE.FUNCTION_ID = row.Cells["機能番号"].Value.ToString();
                    }
                    else
                    {
                        //DT_D09_MODの登録リストに追加
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                        //Cls_Dt_D10_Mod.CREATE_DATE = System.DateTime.Now;
                        Cls_Dt_D10_Mod.CREATE_DATE = this.getDBDateTime();
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                        if (row.Cells["操作CD"].Value.ToString() == "2")
                        {
                            ListDT_D10_MOD_IN.Add(Cls_Dt_D10_Mod);
                        }

                        //queのFUNCTION_ID設定
                        if (row.Cells["操作CD"].Value.ToString() == "2")
                        {
                            Cls_QUE.FUNCTION_ID = "1600";
                        }
                        else
                        {
                            Cls_QUE.FUNCTION_ID = "1800";
                        }

                        //保留時はDt_Mf_Tocは更新しなくて良い

                    }
                    //que更新リストに追加
                    if (!SyoriKubunHoryu.Equals(searchSyoriKubunCD))
                    {
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                        //Cls_QUE.UPDATE_TS = System.DateTime.Now;
                        Cls_QUE.UPDATE_TS = this.getDBDateTime();
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    }
                    ListQue.Add(Cls_QUE);
                }

                using (Transaction tran = new Transaction())
                {
                    if (SyoriKubunShuryo.Equals(searchSyoriKubunCD))
                    {
                        //DT_D09登録処理
                        foreach (DT_D10 DT in ListDT_D10_IN)
                        {
                            // ゴミが残っている場合があるためチェック
                            var delD10Data = Dao_D10.GetD10(DT.KANRI_ID);
                            foreach (var delData in delD10Data)
                            {
                                Dao_D10.Delete(delData);
                            }

                            int i = Dao_D10.Insert(DT);
                        }
                    }
                    else if (SyoriKubunHoryu.Equals(searchSyoriKubunCD))
                    {
                        //DT_D09更新処理
                        foreach (DT_D10 DT in ListDT_D10_UP)
                        {
                            int i = Dao_D10.Update(DT);
                        }
                        //ListDT_D09_MOD_IN更新処理
                        foreach (DT_D10_MOD DT in ListDT_D10_MOD_UP)
                        {
                            int i = Dao_D10_MOD.Update(DT);
                        }
                    }
                    else
                    {
                        //ListDT_D09_MOD_IN登録処理
                        foreach (DT_D10_MOD DT in ListDT_D10_MOD_IN)
                        {
                            int i = Dao_D10_MOD.Insert(DT);
                        }
                        //DT_MF_TOC更新処理
                        foreach (DT_MF_TOC MF in ListDT_MF)
                        {
                            int i = Dao_DT_MF_TOC.Update(MF);
                        }
                    }

                    //QUE_INFO登録処理
                    foreach (QUE_INFO QUE in ListQue)
                    {
                        if (SyoriKubunHoryu.Equals(searchSyoriKubunCD))
                        {
                            int i = Dao_QUE.Update(QUE);
                        }
                        else
                        {
                            int i = Dao_QUE.Insert(QUE);
                        }
                    }

                    tran.Commit();
                    MessageBox.Show("登録が完了しました。");

                    bool catchErr = false;
                    bool retCheck = this.DateCheck(out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (retCheck)
                    {
                        return;
                    }

                    // 再検索
                    Get_Search_TME();
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("HoryuInsert", ex1);
                MessageBoxUtility.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("HoryuInsert", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("HoryuInsert", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
            }

            LogUtility.DebugMethodEnd();
        }

        #region 画面クリア

        /// <summary>
        /// 画面クリア
        /// </summary>
        public void Clear()
        {
            this.form.SyoriKubun_CD.Text = "1";
            this.form.DATE_FROM.Value = null;
            this.form.DATE_TO.Value = null;
            this.form.ManifestNoFrom.Text = "";
            this.form.ManifestNoTo.Text = "";
            this.form.HAIKI_KBN_CD.Text = "";
            this.form.HAIKI_SHURUI_NAME.Text = "";
            this.form.Jigyousya_CD.Text = "";
            this.form.JIGYOUSHA_NAME.Text = "";
            this.form.Jigyoujou_CD.Text = "";
            this.form.JIGYOUJOU_NAME.Text = "";
            this.form.Unpan_CD.Text = "";
            this.form.Unpan_Name.Text = "";
            this.form.Unpansha_CD.Text = "";
            this.form.Unpansha_Name.Text = "";

            this.form.InitializeDataGridView();
        }
        #endregion

        /// <summary>
        /// 実装しない
        /// </summary>
        public void LogicalDelete()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 実装しない
        /// </summary>
        public void PhysicalDelete()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 簡易条件有無判断
        /// </summary>
        public void SetJyoken()
        {
            array = new int[9];

            for (int i = 0; i < 8; i++)
            {
                array[i] = 0;
            }

            if (this.form.DATE_FROM.Value != null)
            {
                array[0] = 1;
            }

            if (this.form.DATE_TO.Value != null)
            {
                array[1] = 1;
            }

            if (this.form.ManifestNoFrom.Text != "")
            {
                array[2] = 1;
            }

            if (this.form.ManifestNoTo.Text != "")
            {
                array[3] = 1;
            }

            if (this.form.HAIKI_KBN_CD.Text != "")
            {
                array[4] = 1;
            }

            if (this.form.Jigyousya_CD.Text != "")
            {
                array[5] = 1;
            }

            if (this.form.Unpan_CD.Text != "")
            {
                array[6] = 1;
            }

            if (this.form.Unpansha_CD.Text != "")
            {
                array[7] = 1;
            }

            if (this.form.cntb_TashaEDI_KBN.Text != "" && this.form.cntb_TashaEDI_KBN.Text != "3")
            {
                array[8] = 1;
            }
        }

        public int CheckJyoken(int count)
        {
            int Sum = 0;
            for (int i = count; i < 9; i++)
            {
                Sum = Sum + array[i];
            }
            return Sum;
        }

        //IchiranDgv1_CellValidating
        private void IchiranDgv1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DgvCustomTextBoxCell c = this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DgvCustomTextBoxCell;

            var columnName = this.form.IchiranDgv1.Columns[e.ColumnIndex].Name;
            if (columnName == "操作CD")
            {
                return;
            }

            if (c != null)
            {
                if (c.ReadOnly == false)
                {
                    //フォマット未設定の場合、禁則文字チェックを行う
                    if (string.IsNullOrEmpty(c.CustomFormatSetting))
                    {
                        object tmpobj = c.EditedFormattedValue;
                        if (tmpobj != null)
                        {
                            if (this.KinsokuMoziCheck(tmpobj.ToString()) == false)
                            {
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShow("E071", "該当箇所");
                                e.Cancel = true;
                            }
                        }
                    }
                }
            }
            else
            {
                // 空なら検索中等なので、何もしない
                return;
            }

            string name = "";
            string CD = "";
            string Sousa = "";

            bool checkErrFlg = false;

            if (this.form.IchiranDgv1.Rows.Count != 0)
            {
                if (e.RowIndex > -1)
                {
                    if (this.form.IchiranDgv1.Rows[e.RowIndex].Cells["操作CD"].Value != null)
                    {
                        Sousa = this.form.IchiranDgv1.Rows[e.RowIndex].Cells["操作CD"].Value.ToString();
                    }
                    //運搬担当者check
                    if (this.form.IchiranDgv1.Columns[e.ColumnIndex].Name == "運搬担当者CD" && Sousa != "3")
                    {
                        if (this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                        {
                            this.form.IchiranDgv1.Rows[e.RowIndex].Cells["運搬担当者"].Value = "";
                        }
                        else
                        {
                            CD = this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            var Jigyousha = this.form.IchiranDgv1.Rows[e.RowIndex].Cells["SBN_SHA_MEMBER_ID"].Value.ToString();
                            name = SearchName("運搬担当者CD", CD, Jigyousha);
                            if (name == "false")
                            {
                                MessageBoxUtility.MessageBoxShow("E020", "運搬担当者");
                                this.form.IchiranDgv1.CurrentCell = this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                                this.form.IchiranDgv1.Rows[e.RowIndex].Cells["運搬担当者"].Value = "";
                                checkErrFlg = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                this.form.IchiranDgv1.Rows[e.RowIndex].Cells["運搬担当者"].Value = name;
                            }
                        }
                    }
                    //処分担当者check
                    if (this.form.IchiranDgv1.Columns[e.ColumnIndex].Name == "処分担当者CD" && Sousa != "3")
                    {
                        if (this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                        {
                            this.form.IchiranDgv1.Rows[e.RowIndex].Cells["処分担当者"].Value = "";
                        }
                        else
                        {
                            CD = this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            var Jigyousha = this.form.IchiranDgv1.Rows[e.RowIndex].Cells["SBN_SHA_MEMBER_ID"].Value.ToString();
                            name = SearchName("処分担当者CD", CD, Jigyousha);
                            if (name == "false")
                            {
                                MessageBoxUtility.MessageBoxShow("E020", "処分担当者");
                                this.form.IchiranDgv1.CurrentCell = this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                                this.form.IchiranDgv1.Rows[e.RowIndex].Cells["処分担当者"].Value = "";
                                checkErrFlg = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                this.form.IchiranDgv1.Rows[e.RowIndex].Cells["処分担当者"].Value = name;
                            }
                        }
                    }
                    //報告担当者check
                    else if (this.form.IchiranDgv1.Columns[e.ColumnIndex].Name == "報告担当者CD" && Sousa != "3")
                    {
                        if (this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                        {
                            this.form.IchiranDgv1.Rows[e.RowIndex].Cells["報告担当者"].Value = "";
                        }
                        else
                        {
                            CD = this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            var Member = this.form.IchiranDgv1.Rows[e.RowIndex].Cells["SBN_SHA_MEMBER_ID"].Value.ToString();
                            name = SearchName("報告担当者CD", CD, Member);
                            if (name == "false")
                            {
                                MessageBoxUtility.MessageBoxShow("E020", "報告担当者");
                                this.form.IchiranDgv1.CurrentCell = this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                                this.form.IchiranDgv1.Rows[e.RowIndex].Cells["報告担当者"].Value = "";
                                checkErrFlg = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                this.form.IchiranDgv1.Rows[e.RowIndex].Cells["報告担当者"].Value = name;
                            }
                        }
                    }
                    // 処分終了日
                    else if (this.form.IchiranDgv1.Columns[e.ColumnIndex].Name == "処分終了日" && Sousa != "3")
                    {
                        if (this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null
                            || string.IsNullOrEmpty(this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                        {
                            this.form.IchiranDgv1.Rows[e.RowIndex].Cells["処分終了日"].Value = "";
                        }
                        else
                        {
                            DateTime sbnDate = DateTime.Parse(this.form.IchiranDgv1.Rows[e.RowIndex].Cells["処分終了日"].Value.ToString());
                            DateTime hkDate = DateTime.Parse(this.form.IchiranDgv1.Rows[e.RowIndex].Cells["HIKIWATASHIBI"].Value.ToString());
                            if (sbnDate < hkDate)
                            {
                                MessageBoxUtility.MessageBoxShow("E030", "引渡し日", "処分終了日");
                                e.Cancel = true;
                            }
                            // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                            //else if (DateTime.Today < sbnDate)
                            else if (this.parentForm.sysDate.Date < sbnDate)
                            // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                            {
                                MessageBoxUtility.MessageBoxShow("E034", "処分終了日には今日以前の日付");
                                e.Cancel = true;
                            }
                            else if (this.form.IchiranDgv1.Rows[e.RowIndex].Cells["廃棄物受領日"].Value != null
                                     && !string.IsNullOrEmpty(this.form.IchiranDgv1.Rows[e.RowIndex].Cells["廃棄物受領日"].Value.ToString()))
                            {
                                DateTime hkbDate = DateTime.Parse(this.form.IchiranDgv1.Rows[e.RowIndex].Cells["廃棄物受領日"].Value.ToString());
                                if (sbnDate < hkbDate)
                                {
                                    MessageBoxUtility.MessageBoxShow("E030", "廃棄物受領日", "処分終了日");
                                    e.Cancel = true;
                                }
                            }
                            
                            var sbnEndDate = new DateTime();
                            // 修正時は処分終了報告日も確認
                            if (Sousa == "2"
                                && !e.Cancel
                                && this.form.IchiranDgv1.Rows[e.RowIndex].Cells["SBN_END_REP_DATE"].Value != null
                                && ToDateTime(this.form.IchiranDgv1.Rows[e.RowIndex].Cells["SBN_END_REP_DATE"].Value.ToString(), out sbnEndDate))
                            {
                                if (sbnEndDate < sbnDate)
                                {
                                    MessageBoxUtility.MessageBoxShow("E034", "処分終了日には処分終了報告日以前の日付");
                                    e.Cancel = true;
                                }
                            }

                            if (e.Cancel)
                            {
                                this.form.IchiranDgv1.CurrentCell = this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                                checkErrFlg = true;
                            }
                        }
                    }
                    // 廃棄物受領日
                    else if (this.form.IchiranDgv1.Columns[e.ColumnIndex].Name == "廃棄物受領日" && Sousa != "3")
                    {
                        if (this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null
                            || string.IsNullOrEmpty(this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                        {
                            this.form.IchiranDgv1.Rows[e.RowIndex].Cells["廃棄物受領日"].Value = "";
                        }
                        else
                        {
                            DateTime hkbDate = DateTime.Parse(this.form.IchiranDgv1.Rows[e.RowIndex].Cells["廃棄物受領日"].Value.ToString());
                            DateTime hkDate = DateTime.Parse(this.form.IchiranDgv1.Rows[e.RowIndex].Cells["HIKIWATASHIBI"].Value.ToString());
                            if (hkbDate < hkDate)
                            {
                                MessageBoxUtility.MessageBoxShow("E030", "引渡し日", "廃棄物受領日");
                                e.Cancel = true;
                            }
                            // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                            //else if (DateTime.Today < hkbDate)
                            else if (this.parentForm.sysDate.Date < hkbDate)
                            // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                            {
                                MessageBoxUtility.MessageBoxShow("E034", "廃棄物受領日には今日以前の日付");
                                e.Cancel = true;
                            }
                            else if (this.form.IchiranDgv1.Rows[e.RowIndex].Cells["処分終了日"].Value != null
                                     && !string.IsNullOrEmpty(this.form.IchiranDgv1.Rows[e.RowIndex].Cells["処分終了日"].Value.ToString()))
                            {
                                DateTime sbnDate = DateTime.Parse(this.form.IchiranDgv1.Rows[e.RowIndex].Cells["処分終了日"].Value.ToString());
                                if (sbnDate < hkbDate)
                                {
                                    MessageBoxUtility.MessageBoxShow("E034", "廃棄物受領日には処分終了日以前の日付");
                                    e.Cancel = true;
                                }
                            }

                            var sbnEndDate = new DateTime();
                            // 修正時は処分終了報告日も確認
                            if (Sousa == "2"
                                && !e.Cancel
                                && this.form.IchiranDgv1.Rows[e.RowIndex].Cells["SBN_END_REP_DATE"].Value != null
                                && ToDateTime(this.form.IchiranDgv1.Rows[e.RowIndex].Cells["SBN_END_REP_DATE"].Value.ToString(), out sbnEndDate))
                            {
                                if (sbnEndDate < hkbDate)
                                {
                                    MessageBoxUtility.MessageBoxShow("E034", "廃棄物受領日には処分終了報告日以前の日付");
                                    e.Cancel = true;
                                }
                            }

                            if (e.Cancel)
                            {
                                this.form.IchiranDgv1.CurrentCell = this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                                checkErrFlg = true;
                            }
                        }
                    }
                    //運搬量単位check
                    else if (this.form.IchiranDgv1.Columns[e.ColumnIndex].Name == "運搬量単位CD" && Sousa != "3")
                    {
                        if (this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                        {
                            this.form.IchiranDgv1.Rows[e.RowIndex].Cells["運搬量単位名"].Value = "";
                        }
                        else
                        {
                            CD = this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            name = SearchName("運搬量単位CD", CD, "");
                            if (name == "false")
                            {
                                MessageBoxUtility.MessageBoxShow("E020", "運搬量単位");
                                this.form.IchiranDgv1.CurrentCell = this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                                this.form.IchiranDgv1.Rows[e.RowIndex].Cells["運搬量単位名"].Value = "";
                                checkErrFlg = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                this.form.IchiranDgv1.Rows[e.RowIndex].Cells["運搬量単位名"].Value = name;
                            }
                        }
                    }
                    //運搬量単位check
                    else if (this.form.IchiranDgv1.Columns[e.ColumnIndex].Name == "受入量単位CD" && Sousa != "3" && Sousa != "3")
                    {
                        if (this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                        {
                            this.form.IchiranDgv1.Rows[e.RowIndex].Cells["受入量単位名"].Value = "";
                        }
                        else
                        {
                            CD = this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            name = SearchName("受入量単位CD", CD, "");
                            if (name == "false")
                            {
                                MessageBoxUtility.MessageBoxShow("E020", "受入量単位");
                                this.form.IchiranDgv1.CurrentCell = this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                                this.form.IchiranDgv1.Rows[e.RowIndex].Cells["受入量単位名"].Value = "";
                                checkErrFlg = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                this.form.IchiranDgv1.Rows[e.RowIndex].Cells["受入量単位名"].Value = name;
                            }
                        }
                    }
                    //車輌番号check
                    else if (this.form.IchiranDgv1.Columns[e.ColumnIndex].Name == "車輌番号CD" && Sousa != "3")
                    {
                        if (this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                        {
                            this.form.IchiranDgv1.Rows[e.RowIndex].Cells["車輌"].Value = "";
                        }
                        else
                        {
                            CD = this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            var Jigyousha = this.form.IchiranDgv1.Rows[e.RowIndex].Cells["GYOUSHA_CD"].Value.ToString();
                            name = SearchName("車輌番号CD", CD, Jigyousha);
                            if (name == "false")
                            {
                                MessageBoxUtility.MessageBoxShow("E020", "車輌番号");
                                this.form.IchiranDgv1.CurrentCell = this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                                this.form.IchiranDgv1.Rows[e.RowIndex].Cells["車輌"].Value = "";
                                checkErrFlg = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                this.form.IchiranDgv1.Rows[e.RowIndex].Cells["車輌"].Value = name;
                            }
                        }
                    }
                    else if (this.form.IchiranDgv1.Columns[e.ColumnIndex].Name == "報告区分CD")
                    {
                        if (this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                        {
                            if (this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "1")
                            {
                                this.form.IchiranDgv1.Rows[e.RowIndex].Cells["報告区分名"].Value = "中間";
                            }
                            else if (this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "2")
                            {
                                this.form.IchiranDgv1.Rows[e.RowIndex].Cells["報告区分名"].Value = "最終";
                            }
                            else
                            {
                                this.form.IchiranDgv1.Rows[e.RowIndex].Cells["報告区分名"].Value = "";
                            }
                        }
                        else
                        {
                            this.form.IchiranDgv1.Rows[e.RowIndex].Cells["報告区分名"].Value = "";
                        }
                    }
                    else if (this.form.IchiranDgv1.Columns[e.ColumnIndex].Name == "受入量")
                    {
                        if (this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                        {
                            string suu = this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            if (suu != "")
                            {
                                if (!suu.Contains("."))
                                {
                                    if (SqlInt32.Parse(suu) < 0 || SqlInt32.Parse(suu) >= 100000)
                                    {
                                        this.form.IchiranDgv1.CurrentCell = this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                                        MessageBoxUtility.MessageBoxShow("W001", "99999.999", "0.000");
                                        e.Cancel = true;
                                    }
                                }
                                else
                                {
                                    string valEx = @"^[0-9]{1,5}\.[0-9]{1,3}$";
                                    if (!System.Text.RegularExpressions.Regex.IsMatch(suu, valEx))
                                    {
                                        this.form.IchiranDgv1.CurrentCell = this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                                        MessageBoxUtility.MessageBoxShow("W001", "99999.999", "0.000");
                                        e.Cancel = true;
                                    }
                                }
                            }
                        }
                    }

                }
            }

            //異常の場合
            if (checkErrFlg)
            {
                if ((this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DgvCustomTextBoxCell) != null)
                {
                    (this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DgvCustomTextBoxCell).IsInputErrorOccured = true;
                    (this.form.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex]
                        as DgvCustomTextBoxCell).AutoChangeBackColorEnabled = true;
                }

                e.Cancel = true;
                return;
            }

        }

        /// <summary>
        /// 「YYYYMMDD」形式の文字列をDateTimeに変換
        /// </summary>
        /// <param name="str"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private bool ToDateTime(string str, out DateTime dt)
        {
            // 20150922 katen #12048 「システム日付」の基準作成、適用 start
            //dt = new DateTime();
            dt = this.parentForm.sysDate;
            // 20150922 katen #12048 「システム日付」の基準作成、適用 start
            int num = 0;
            if (string.IsNullOrEmpty(str)
                || str.Length != 8
                || !int.TryParse(str, out num))
            {
                return false;
            }

            // YYYY/MM/DDに分割
            int year = int.Parse(str.Substring(0, 4));
            int month = int.Parse(str.Substring(4, 2));
            int day = int.Parse(str.Substring(6, 2));

            dt = new DateTime(year, month, day);

            return true;
        }

        /// <summary>
        /// 禁則文字チェック
        /// </summary>
        /// <param name="insertVal">登録項目</param>
        public bool KinsokuMoziCheck(string insertVal)
        {
            Validator v = new Validator();

            if (!v.isJWNetValidShiftJisCharForSign(insertVal))
            {
                return false;
            }
            return true;
        }

        //運搬担当者CDなど、CDによってNameを取得
        private string SearchName(String ronriName, String cd, String jigyousha)
        {
            LogUtility.DebugMethodStart(ronriName, cd, jigyousha);
            String name = "";
            if (cd == "")
            {
                return name;
            }
            else
            {
                TMEDtoCls Dto_CD = new TMEDtoCls();
                Dto_CD.Search_CD = cd;
                DataTable SearchName = new DataTable();
                if (ronriName == "運搬担当者CD")
                {
                    Dto_CD.JIGYOUSHA_CD = jigyousha;
                    SearchName = Dao_SearchUpnName.GetDataForEntity(Dto_CD);
                }
                else if (ronriName == "報告担当者CD")
                {
                    Dto_CD.JIGYOUSHA_CD = jigyousha;
                    SearchName = Dao_SearchRepName.GetDataForEntity(Dto_CD);
                }
                else if (ronriName == "処分担当者CD")
                {
                    Dto_CD.JIGYOUSHA_CD = jigyousha;
                    SearchName = Dao_SearchSyobunName.GetDataForEntity(Dto_CD);
                }
                else if (ronriName == "受入量単位CD")
                {
                    SearchName = Dao_SearchTaniName.GetDataForEntity(Dto_CD);
                }
                else
                {
                    Dto_CD.JIGYOUSHA_CD = jigyousha;
                    SearchName = Dao_SearchSharyoName.GetDataForEntity(Dto_CD);
                }

                var table = SearchName;
                if (table.Rows.Count > 0)
                {
                    name = table.Rows[0]["NAME"].ToString();
                }
                else
                {
                    name = "false";
                }
            }
            LogUtility.DebugMethodEnd(ronriName, cd, jigyousha);
            return name;
        }

        public void CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
        }

        /// <summary>
        /// 一括入力画面で入力したデータを一覧に反映します
        /// </summary>
        /// <param name="dtoList"></param>
        public void SetIchiranData(SyobunnShuryouHoukokuIkkatuNyuuryoku.OutputInfoDTOCls[] dtoList)
        {
            try
            {
                if (dtoList == null)
                {
                    return;
                }

                var dataTable = (DataTable)this.form.IchiranDgv1.DataSource;

                foreach (var dto in dtoList)
                {
                    var row = dataTable.AsEnumerable().Where(r => r["管理番号"].ToString() == dto.KANRI_ID && r["枝番"].ToString() == dto.SEQ).FirstOrDefault();
                    if (row != null)
                    {
                        if (!String.IsNullOrEmpty(dto.SYOBUNN_SYUURYOUHI))
                        {
                            row["処分終了日"] = DateTime.Parse(dto.SYOBUNN_SYUURYOUHI.Substring(0, 4) + "/" + dto.SYOBUNN_SYUURYOUHI.Substring(4, 2) + "/" + dto.SYOBUNN_SYUURYOUHI.Substring(6, 2));
                        }
                        else
                        {
                            row["処分終了日"] = DBNull.Value;
                        }
                        row["報告区分CD"] = dto.HOUKOKU_KUBUNN;
                        if (dto.HOUKOKU_KUBUNN == "1")
                        {
                            row["報告区分名"] = "中間";
                        }
                        else if (dto.HOUKOKU_KUBUNN == "2")
                        {
                            row["報告区分名"] = "最終";
                        }
                        else
                        {
                            row["報告区分名"] = null;
                        }
                        row["処分担当者"] = dto.SYOBUNN_TANNTOUSYA_NAME;
                        row["報告担当者"] = dto.HOUKOKU_TANNTOUSYA_NAME;
                        if (!String.IsNullOrEmpty(dto.HAIKIBUTU_JYURYOUHI))
                        {
                            row["廃棄物受領日"] = DateTime.Parse(dto.HAIKIBUTU_JYURYOUHI.Substring(0, 4) + "/" + dto.HAIKIBUTU_JYURYOUHI.Substring(4, 2) + "/" + dto.HAIKIBUTU_JYURYOUHI.Substring(6, 2));
                        }
                        else
                        {
                            row["廃棄物受領日"] = DBNull.Value;
                        }
                        row["運搬担当者"] = dto.UNNPANN_TANNTOUSYA_NAME;
                        row["車輌"] = dto.SYARYOU_NAME;
                        if (!String.IsNullOrEmpty(dto.UKEIRERYOU))
                        {
                            row["受入量"] = Decimal.Parse(dto.UKEIRERYOU);
                        }
                        else
                        {
                            row["受入量"] = DBNull.Value;
                        }
                        row["受入量単位CD"] = dto.UKEIRERYOU_TANNI_CD;
                        row["受入量単位名"] = dto.UKEIRERYOU_TANNI_NAME;
                        row["備考"] = dto.BIKO;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetIchiranData", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranData", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
            }
        }

        /// 20141022 Houkakou 「処分終了報告」の日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck(out bool catchErr)
        {
            catchErr = false;
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                this.form.DATE_FROM.BackColor = Constans.NOMAL_COLOR;
                this.form.DATE_TO.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrEmpty(this.form.DATE_FROM.Text))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(this.form.DATE_TO.Text))
                {
                    return false;
                }

                DateTime date_from = DateTime.Parse(this.form.DATE_FROM.Text);
                DateTime date_to = DateTime.Parse(this.form.DATE_TO.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.DATE_FROM.IsInputErrorOccured = true;
                    this.form.DATE_TO.IsInputErrorOccured = true;
                    this.form.DATE_FROM.BackColor = Constans.ERROR_COLOR;
                    this.form.DATE_TO.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "引渡し日From", "引渡し日To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.DATE_FROM.Focus();
                    return true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DateCheck", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DateCheck", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = true;
            }
            return false;
        }
        #endregion
        /// 20141022 Houkakou 「処分終了報告」の日付チェックを追加する　end

        /// 20141128 Houkakou 「処分終了報告」のダブルクリックを追加する　start
        #region DATE_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.DATE_FROM;
            var ToTextBox = this.form.DATE_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ManifestNoToダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManifestNoTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.ManifestNoFrom;
            var ToTextBox = this.form.ManifestNoTo;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141128 Houkakou 「処分終了報告」のダブルクリックを追加する　end

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

        /// <summary>
        /// ファンクションボタンの活性/非活性切り替えのタイミングは、検索を行ったタイミングで切り替える
        /// </summary>
        internal void setHoryuDelete()
        {
            //footerのコントロール
            switch (this.form.SyoriKubun_CD.Text)
            {
                case "1":
                    this.form.ParentBaseForm.bt_func4.Enabled = false;
                    break;
                case "2":
                    // 権限チェック
                    if (Manager.CheckAuthority("G145", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        this.form.ParentBaseForm.bt_func4.Enabled = true;
                    }
                    else
                    {
                        this.form.ParentBaseForm.bt_func4.Enabled = false;
                    }
                    break;
                case "3":
                    this.form.ParentBaseForm.bt_func4.Enabled = false;
                    break;
                default:
                    this.form.ParentBaseForm.bt_func4.Enabled = false;
                    break;
            }
        }
    }
}
