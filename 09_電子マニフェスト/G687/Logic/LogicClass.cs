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
using Shougun.Core.ElectronicManifest.DenmaniSaishuShobun.DAO;
using Shougun.Core.Message;
using Seasar.Dao;
using r_framework.Dao;
using Seasar.Framework.Exceptions;
using DataGridViewCheckBoxColumnHeeader;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Const;

namespace Shougun.Core.ElectronicManifest.DenmaniSaishuShobun
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
        private string ButtonInfoXmlPath = "Shougun.Core.ElectronicManifest.DenmaniSaishuShobun.Setting.ButtonSetting.xml";

        /// <summary>
        /// DAO
        /// </summary>
        private GetTMEDaoCls dao_GetTME;

        /// <summary>
        /// DTO
        /// </summary>
        private TMEDtoCls dto_TME;

        // Popup営業担当者
        public DAOClass_PopupHaiki Dao_PopupHaiki;

        /// <summary>
        /// DAO
        /// </summary>
        //廃棄物種類チェック
        private DAOClass_CheckHaiki Dao_CheckHaiki;

        // Popup営業担当者
        public DAOClass_PopupHaikiName Dao_PopupHaikiName;

        /// <summary>
        /// DAO
        /// </summary>
        //廃棄物名称チェック
        private DAOClass_CheckHaikiName Dao_CheckHaikiName;

        /// <summary>
        /// Form
        /// </summary>
        private UIHeader header;
        private UIForm form;

        internal BusinessBaseForm parentForm;

        /// <summary>
        /// キュー情報DAO
        /// </summary>
        private GetQueDaoCls queDao;

        /// <summary>
        /// D12 2次マニフェスト情報DAO
        /// </summary>
        private GetmanifastDaoCls manifastDao;

        /// <summary>
        /// D13 最終処分終了日・事業場情報DAO
        /// </summary>
        private GetjigyoubaDaoCls jigyoubaDao;

        /// <summary>
        /// マニフェスト目次情報DAO
        /// </summary>
        private GetmokujiDaoCls mokujiDao;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果(マニフェストパターン)
        /// </summary>
        public DataTable Search_TME { get; set; }

        private Control[] allControl;

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
            this.Dao_CheckHaiki = DaoInitUtility.GetComponent<DAO.DAOClass_CheckHaiki>();
            this.Dao_CheckHaikiName = DaoInitUtility.GetComponent<DAO.DAOClass_CheckHaikiName>();

            //DAO
            this.dao_GetTME = DaoInitUtility.GetComponent<DAO.GetTMEDaoCls>();
            queDao = DaoInitUtility.GetComponent<GetQueDaoCls>();
            manifastDao = DaoInitUtility.GetComponent<GetmanifastDaoCls>();
            jigyoubaDao = DaoInitUtility.GetComponent<GetjigyoubaDaoCls>();
            mokujiDao = DaoInitUtility.GetComponent<GetmokujiDaoCls>();

            this.Search_TME = new DataTable();

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

            //ヘッダーの初期化
            UIHeader targetHeader = (UIHeader)parentForm.headerForm;
            this.header = targetHeader;
            this.header.lb_title.Text = "紐付1次最終処分終了報告";

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

            parentForm.bt_func6.Click += new EventHandler(this.form.bt_func6_Click);

            // 並び替え(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.bt_func7_Click);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);

            //JWNET送信(F9)イベント生成
            parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);

            //取消(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.bt_func11_Click);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            // 「To」のイベント生成
            this.form.DATE_TO.MouseDoubleClick += new MouseEventHandler(DATE_TO_MouseDoubleClick);
            this.form.ManifestNoTo.MouseDoubleClick += new MouseEventHandler(ManifestNoTo_MouseDoubleClick);

            // DataGridView系のイベント生成
            this.form.Ichiran.CellClick += new DataGridViewCellEventHandler(this.form.Ichiran_CellClick);
            this.form.Ichiran.CellPainting += new DataGridViewCellPaintingEventHandler(this.form.Ichiran_CellPainting);

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

                this.parentForm = (BusinessBaseForm)this.form.Parent;

                //DTO
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
                this.form.Ichiran.AllowUserToAddRows = false;                                //行の追加オプション(false)
                this.form.DATE_FROM.Value = null;
                this.form.DATE_TO.Value = null;
                this.form.SyoriKubun_CD.Text = "1";
                this.form.SyoriKubun_CD.Focus();

                // Formサイズに合わせてDataGridViewサイズを動的に変更する
                this.form.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;

                // 明細項目固定となったため親FormのDGV、汎用検索は非表示
                // ※継承Formを変更すると影響大のため
                this.form.customDataGridView1.Visible = false;
                this.form.searchString.Visible = false;

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

            this.form.HAIKI_SHURUI_CD.Validated += new EventHandler(HAIKI_SHURUI_Validated);

            this.form.HAIKI_NAME_CD.Validated += new EventHandler(HAIKI_NAME_Validated);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// popupイベント初期化
        /// </summary>
        public void PopupInit()
        {
            LogUtility.DebugMethodStart();

            // 廃棄物種類
            this.form.HAIKI_SHURUI_CD.PopupWindowId = WINDOW_ID.M_DENSHI_HAIKI_SHURUI;
            this.form.Syuruyi_Btn.PopupWindowId = WINDOW_ID.M_DENSHI_HAIKI_SHURUI;
            // ポップアップに表示するデータ列(物理名)
            this.form.HAIKI_SHURUI_CD.PopupGetMasterField = "HAIKI_SHURUI_CD,HAIKI_SHURUI_NAME";
            this.form.Syuruyi_Btn.PopupGetMasterField = "HAIKI_SHURUI_CD,HAIKI_SHURUI_NAME";
            // 表示用データ取得＆加工
            var HaikiDataTable = this.GetPopUpHaiki(this.form.HAIKI_SHURUI_CD.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()));
            // 列名とデータソース設定
            this.form.HAIKI_SHURUI_CD.PopupDataHeaderTitle = new string[] { "廃棄物種類CD", "廃棄物種類名" };
            this.form.HAIKI_SHURUI_CD.PopupDataSource = HaikiDataTable;
            this.form.Syuruyi_Btn.PopupDataHeaderTitle = new string[] { "廃棄物種類CD", "廃棄物種類名" };
            this.form.Syuruyi_Btn.PopupDataSource = HaikiDataTable;
            //popup画面ヘッダ設定
            this.form.HAIKI_SHURUI_CD.PopupDataSource.TableName = "廃棄物種類検索";
            this.form.Syuruyi_Btn.PopupDataSource.TableName = "廃棄物種類検索";

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

            //報告処分業者
            Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                this.form.Unpansha_CD, this.form.Unpansha_Name, this.form.Unpansha_Btn,
                null, null, null,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.DENSHI,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.SHOBUN_NIOROSHI_GYOUSHA, true, false,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.NONE,
                false, true, false);

            //廃棄物名称
            this.form.HAIKI_NAME_CD.PopupWindowId = WINDOW_ID.M_DENSHI_HAIKI_NAME;
            this.form.cbtn_ElecHaikibutuNameSan.PopupWindowId = WINDOW_ID.M_DENSHI_HAIKI_NAME;
            // ポップアップに表示するデータ列(物理名)
            this.form.HAIKI_NAME_CD.PopupGetMasterField = "HAIKI_NAME_CD,HAIKI_NAME";
            this.form.cbtn_ElecHaikibutuNameSan.PopupGetMasterField = "HAIKI_NAME_CD,HAIKI_NAME";
            // 表示用データ取得＆加工
            var HaikiNameDataTable = this.GetPopUpHaikiName(this.form.HAIKI_NAME_CD.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()));
            // 列名とデータソース設定
            this.form.HAIKI_NAME_CD.PopupDataHeaderTitle = new string[] { "電子廃棄物CD", "電子廃棄物名称" };
            this.form.HAIKI_NAME_CD.PopupDataSource = HaikiNameDataTable;
            this.form.cbtn_ElecHaikibutuNameSan.PopupDataHeaderTitle = new string[] { "電子廃棄物CD", "電子廃棄物名称" };
            this.form.cbtn_ElecHaikibutuNameSan.PopupDataSource = HaikiNameDataTable;
            //popup画面ヘッダ設定
            this.form.HAIKI_NAME_CD.PopupDataSource.TableName = "電子廃棄物名称";
            this.form.cbtn_ElecHaikibutuNameSan.PopupDataSource.TableName = "電子廃棄物名称";

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region Validated

        //廃棄物種類チェック
        private void HAIKI_SHURUI_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (this.form.HAIKI_SHURUI_CD.Text == "")
            {
                this.form.HAIKI_SHURUI_NAME.Text = "";
            }
            else
            {
                TMEDtoCls Dto_Haiki = new TMEDtoCls();
                Dto_Haiki.HAIKI_SHURUI_CD = this.form.HAIKI_SHURUI_CD.Text;
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
                    this.form.HAIKI_SHURUI_CD.Focus();
                    this.form.HAIKI_SHURUI_NAME.Text = "";
                    this.form.HAIKI_SHURUI_CD.BackColor = Constans.ERROR_COLOR;
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 電子廃棄物名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAIKI_NAME_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (this.form.HAIKI_NAME_CD.Text == "")
            {
                this.form.HAIKI_NAME.Text = "";
            }
            else
            {
                TMEDtoCls Dto_Haiki = new TMEDtoCls();
                Dto_Haiki.HAIKI_NAME_CD = this.form.HAIKI_NAME_CD.Text;
                DataTable Check_Haiki = new DataTable();
                Check_Haiki = Dao_CheckHaikiName.GetDataForEntity(Dto_Haiki);
                var table = Check_Haiki;
                if (table.Rows.Count > 0)
                {
                    this.form.HAIKI_NAME.Text = table.Rows[0]["HAIKI_NAME"].ToString();
                }
                else
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "廃棄物名称");
                    this.form.HAIKI_NAME_CD.Focus();
                    this.form.HAIKI_NAME.Text = "";
                    this.form.HAIKI_NAME_CD.BackColor = Constans.ERROR_COLOR;
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

                this.dto_TME = new TMEDtoCls();

                //実行区分
                this.dto_TME.pageExeKbn = this.form.SyoriKubun_CD.Text;

                //日付from
                if (!string.IsNullOrEmpty(this.form.DATE_FROM.Text))
                {
                    this.dto_TME.HIKIWATASHI_DATE_FROM = this.form.DATE_FROM.Text.Substring(0, 10).Replace("/", "");
                }
                else
                {
                    this.dto_TME.HIKIWATASHI_DATE_FROM = string.Empty;
                }

                //日付to
                if (!string.IsNullOrEmpty(this.form.DATE_TO.Text))
                {
                    this.dto_TME.HIKIWATASHI_DATE_TO = this.form.DATE_TO.Text.Substring(0, 10).Replace("/", "");
                }
                else
                {
                    this.dto_TME.HIKIWATASHI_DATE_TO = string.Empty;
                }

                //番号from
                this.dto_TME.MANIFEST_ID_FROM = this.form.ManifestNoFrom.Text;

                //番号to
                this.dto_TME.MANIFEST_ID_TO = this.form.ManifestNoTo.Text;

                //廃棄物種類
                if (!string.IsNullOrEmpty(this.form.HAIKI_SHURUI_CD.Text))
                {
                    this.dto_TME.HAIKI_SHURUI_CD = this.form.HAIKI_SHURUI_CD.Text;
                    this.dto_TME.HAIKI_DAI_CODE = this.form.HAIKI_SHURUI_CD.Text.Substring(0, 2);
                    this.dto_TME.HAIKI_CHU_CODE = this.form.HAIKI_SHURUI_CD.Text.Substring(2, 1);
                    this.dto_TME.HAIKI_SHO_CODE = this.form.HAIKI_SHURUI_CD.Text.Substring(3, 1);
                }
                else
                {
                    this.dto_TME.HAIKI_SHURUI_CD = string.Empty;
                    this.dto_TME.HAIKI_DAI_CODE = string.Empty;
                    this.dto_TME.HAIKI_CHU_CODE = string.Empty;
                    this.dto_TME.HAIKI_SHO_CODE = string.Empty;
                }

                //廃棄物名称
                this.dto_TME.HAIKI_NAME_CD = this.form.HAIKI_NAME_CD.Text;

                //排出事業者
                this.dto_TME.HST_GYOUSHA_CD = this.form.Jigyousya_CD.Text;

                //排出事業場
                this.dto_TME.HST_GENBA_CD = this.form.Jigyoujou_CD.Text;

                //収集運搬業者
                this.dto_TME.UPN_GYOUSHA_CD = this.form.Unpan_CD.Text;

                //報告処分業者
                this.dto_TME.SBN_GYOUSHA_CD = this.form.Unpansha_CD.Text;

                this.Search_TME = new DataTable();

                bool exeFlg = true;
                if (this.dto_TME.pageExeKbn == "1")                           //終了報告
                {
                    exeFlg = true;
                    // 最終処分終了報告
                    this.Search_TME = this.dao_GetTME.GetManifestRelation(this.dto_TME);

                    // 電マニ混廃用ロジック
                    List<string> tempKanriIds = new List<string>();
                    foreach (DataRow tempDataRow in this.Search_TME.Rows)
                    {
                        if (!tempKanriIds.Contains(tempDataRow["KANRI_ID"].ToString()))
                        {
                            tempKanriIds.Add(tempDataRow["KANRI_ID"].ToString());
                        }
                    }

                    if (tempKanriIds.Count > 0)
                    {
                        // 電マニ混廃振分で区分：最終にしたデータを取得
                        // 最終処分終了報告できるようにする
                        var tempDt = this.dao_GetTME.GetMixManifestForLastSbnData(tempKanriIds);
                        if (tempDt != null && tempDt.Rows.Count > 0)
                        {
                            this.Search_TME.Merge(tempDt);
                        }
                    }
                }
                else if (this.dto_TME.pageExeKbn == "2")                      //終了報告の修正・取消
                {
                    exeFlg = false;
                    // 最終処分終了報告の取消
                    this.Search_TME = dao_GetTME.GetManifestRelationCancel(this.dto_TME);
                }

                List<string> unModifiedKanriIdList = new List<string>();
                ManifestoLogic maniLogic = new ManifestoLogic();
                maniLogic.ChkLastSbnEndrepReport(this.Search_TME, exeFlg, out unModifiedKanriIdList);

                for (int i = this.Search_TME.Rows.Count; i > 0; i--)
                {
                    DataRow row = this.Search_TME.Rows[i - 1];

                    if (unModifiedKanriIdList.Contains(row["KANRI_ID"].ToString()))
                    {
                        this.Search_TME.Rows.Remove(row);
                    }
                }

                List<string> kanriIdList = new List<string>();
                foreach (DataRow row in this.Search_TME.Rows)
                {
                    if (!kanriIdList.Contains(row["KANRI_ID"].ToString()))
                    {
                        kanriIdList.Add(row["KANRI_ID"].ToString());
                    }
                }

                DataTable dt = new DataTable();
                if (kanriIdList.Count > 0)
                {
                    dt = dao_GetTME.GetElecManiData(kanriIdList);
                }
                else
                {
                    dt = dao_GetTME.GetElecManiDataNasi(String.Empty);
                }

                count_TME = dt.Rows.Count;

                dt.Columns["CHECKBOX"].ReadOnly = false;
                dt.Columns["CHECKBOX"].AllowDBNull = true;
                this.form.Ichiran.DataSource = dt;

                this.form.customSortHeader1.SortDataTable(dt);

                if (this.form.Ichiran.RowCount > 0)
                {
                    // 初期フォーカスセット
                    this.form.Ichiran.Focus();
                    this.form.Ichiran.CurrentCell = this.form.Ichiran.Rows[0].Cells[0];
                }

                // 不要項目を非表示
                this.form.Ichiran.Columns["SYSTEM_ID"].Visible = false;
                this.form.Ichiran.Columns["KANRI_ID"].Visible = false;
                this.form.Ichiran.Columns["LATEST_SEQ"].Visible = false;

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
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">廃棄物名称</param>
        /// <returns></returns>
        public DataTable GetPopUpHaikiName(IEnumerable<string> displayCol)
        {
            LogUtility.DebugMethodStart();

            this.Dao_PopupHaikiName = DaoInitUtility.GetComponent<DAO.DAOClass_PopupHaikiName>();

            TMEDtoCls search_Haiki = new TMEDtoCls();

            if (displayCol.Any(s => s.Length == 0))
            {
                return new DataTable();
            }

            var dt = Dao_PopupHaikiName.GetDataForEntity(search_Haiki);
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
        /// 更新
        /// </summary>
        public void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// JWNET処理
        /// </summary>
        [Transaction]
        public void JWEInsert()
        {
            LogUtility.DebugMethodStart();
            List<string> kanriIds = new List<string>();

            try
            {
                if (!this.form.IsRowSelected())
                {
                    new MessageBoxShowLogic().MessageBoxShowError("登録するデータを選択してください。");
                    return;
                }

                List<string> systemIdList = new List<string>();
                // キュー情報
                List<QUE_INFO> queList = new List<QUE_INFO>();
                List<string> kanriIdList = new List<string>();
                // D12 2次マニフェスト情報
                List<DT_D12> manifastList = new List<DT_D12>();
                // D13 最終処分終了日・事業場情報
                List<DT_D13> jigyoubaList = new List<DT_D13>();
                // マニフェスト目次情報
                List<DT_MF_TOC> mokujiList = new List<DT_MF_TOC>();

                bool exeFlg = (this.dto_TME.pageExeKbn == "1" ? true : false);
                DataTable dt = this.Search_TME.Clone();
                DataTable table = (DataTable)this.form.Ichiran.DataSource;
                foreach (DataRow row in table.Rows)
                {
                    if (this.ConvertToSqlBooleanDefaultFalse(row["CHECKBOX"]).IsTrue)
                    {
                        DataRow[] rows = this.Search_TME.Select("KANRI_ID =" + row["KANRI_ID"].ToString());
                        DataRow[] dtRows = dt.Select("KANRI_ID =" + row["KANRI_ID"].ToString());
                        if (dtRows == null || dtRows.Length == 0)
                        {
                            dt.ImportRow(rows[0]);
                        }
                    }
                }

                // 最終処分終了日 ≦ 最終処分終了の報告日チェック
                if (exeFlg)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (Convert.ToDateTime(row["LAST_SBN_END_DATE"]).Date.CompareTo(this.parentForm.sysDate.Date) > 0)
                        {
                            Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E218");
                            return;
                        }
                    }
                }

                // 件数
                int index = 0;
                // 登録データ作成
                for (; index < dt.Rows.Count; index++)
                {
                    // キュー情報(QUE_INFO)データ作成
                    QUE_INFO Que = new QUE_INFO();
                    // 管理番号
                    Que.KANRI_ID = dt.Rows[index]["KANRI_ID"].ToString();
                    kanriIdList.Add(dt.Rows[index]["KANRI_ID"].ToString());
                    // 枝番
                    Que.SEQ = Convert.ToInt16(dt.Rows[index]["LATEST_SEQ"].ToString());
                    if (exeFlg)
                    {
                        // 機能番号
                        Que.FUNCTION_ID = CommonConst.FUNCTION_ID_2000;
                    }
                    else
                    {
                        // 機能番号
                        Que.FUNCTION_ID = CommonConst.FUNCTION_ID_2100;
                    }
                    // キュー状態フラグ
                    Que.STATUS_FLAG = 0;
                    // レコード作成日
                    // タイムスタンプ
                    var dataBinderQue = new DataBinderLogic<QUE_INFO>(Que);
                    dataBinderQue.SetSystemProperty(Que, true);
                    queList.Add(Que);

                    // マニフェスト目次情報データ作成
                    DT_MF_TOC mokuji = new DT_MF_TOC();
                    // 管理番号
                    mokuji.KANRI_ID = dt.Rows[index]["KANRI_ID"].ToString();
                    // 状態詳細フラグ
                    if (exeFlg)
                    {
                        mokuji.STATUS_DETAIL = 0;
                    }
                    else
                    {
                        mokuji.STATUS_DETAIL = 1;
                    }
                    mokuji.UPDATE_TS = (DateTime)dt.Rows[index]["UPDATE_TS"];
                    // レコード作成日
                    // タイムスタンプ
                    var dataBinderMokuji = new DataBinderLogic<DT_MF_TOC>(mokuji);
                    dataBinderMokuji.SetSystemProperty(mokuji, false);
                    mokujiList.Add(mokuji);

                    // 最終処分終了報告の判断
                    if (exeFlg)
                    {
                        // D12 2次マニフェスト情報データ作成
                        DT_D12 manifast = new DT_D12();
                        // 管理番号
                        manifast.KANRI_ID = dt.Rows[index]["KANRI_ID"].ToString();
                        // 2次マニフェスト番号
                        manifast.SCND_MANIFEST_ID = dt.Rows[index]["MANIFEST_ID"].ToString();
                        // レコード作成日
                        // タイムスタンプ
                        var dataBinderManifast = new DataBinderLogic<DT_D12>(manifast);
                        dataBinderManifast.SetSystemProperty(manifast, true);
                        manifastList.Add(manifast);

                        // 最終処分終了日・事業場情報データ作成
                        DT_D13 jigyouba = new DT_D13();
                        // 管理番号
                        jigyouba.KANRI_ID = dt.Rows[index]["KANRI_ID"].ToString();
                        //2013/12/11 tyou add start

                        // 禁則文字チェック + 住所分割修正(CHIIKI_NAMEは都道府県+政令指定都市なので不適切。あとGENBA_ADDRESS1、2に都道府県が入っていた場合出さないようにする)
                        Validator v = new Validator();
                        string tempCheckName = (dt.Rows[index]["GENBA_NAME1"] == null) ? null : dt.Rows[index]["GENBA_NAME1"].ToString();
                        string tempCheckTodoufuken = (dt.Rows[index]["TODOUFUKEN_NAME"] == null) ? null : dt.Rows[index]["TODOUFUKEN_NAME"].ToString();
                        string tempCheckAddress1 = (dt.Rows[index]["GENBA_ADDRESS1"] == null) ? string.Empty : dt.Rows[index]["GENBA_ADDRESS1"].ToString();
                        string tempCheckAddress2 = (dt.Rows[index]["GENBA_ADDRESS2"] == null) ? string.Empty : dt.Rows[index]["GENBA_ADDRESS2"].ToString();
                        string tempCheckTel = (dt.Rows[index]["GENBA_TEL"] == null) ? null : dt.Rows[index]["GENBA_TEL"].ToString();

                        if (!v.isJWNetValidShiftJisCharForSign(tempCheckName))
                        {
                            Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E071", string.Format("実績タブに入力した最終処分場所名：「{0}」", tempCheckName));
                            return;
                        }

                        if (!v.isJWNetValidShiftJisCharForSign(tempCheckTodoufuken))
                        {
                            Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E071", string.Format("実績タブに入力した最終処分場所名：「{0}」の登録情報の都道府県", tempCheckName));
                            return;
                        }

                        // 住所1
                        if (!v.isJWNetValidShiftJisCharForSign(tempCheckAddress1))
                        {
                            Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E071", string.Format("実績タブに入力した最終処分場所名：「{0}」の登録情報の住所(1行目)", tempCheckName));
                            return;
                        }

                        // 住所2
                        if (!v.isJWNetValidShiftJisCharForSign(tempCheckAddress2))
                        {
                            Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E071", string.Format("実績タブに入力した最終処分場所名：「{0}」の登録情報の住所(2行目)", tempCheckName));
                            return;
                        }

                        if (!v.isTelNumberValid(tempCheckTel))
                        {
                            Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E071", string.Format("実績タブに入力した最終処分場所名：「{0}」の登録情報の電話番号", tempCheckName));
                            return;
                        }

                        /**
                         * データセット
                         */

                        // 最終処分事業場名称
                        jigyouba.LAST_SBN_JOU_NAME = tempCheckName;
                        // 最終処分事業場所在地の郵便番号
                        jigyouba.LAST_SBN_JOU_POST = (dt.Rows[index]["GENBA_POST"] == null) ? null : dt.Rows[index]["GENBA_POST"].ToString();
                        // 最終処分事業場所在地1
                        jigyouba.LAST_SBN_JOU_ADDRESS1 = tempCheckTodoufuken;

                        var maniLogic = new ManifestoLogic();
                        string tempAddress1;
                        string tempAddress2;
                        string tempAddress3;
                        string tempAddress4;
                        // 住所分割
                        maniLogic.SetAddress1ToAddress4(tempCheckTodoufuken + tempCheckAddress1 + tempCheckAddress2,
                            out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);

                        // 所在地1に都道府県名が設定されている可能性があるので、都道府県が設定されていなかった場合には分割した住所をセットする
                        if (string.IsNullOrEmpty(jigyouba.LAST_SBN_JOU_ADDRESS1))
                        {
                            jigyouba.LAST_SBN_JOU_ADDRESS1 = tempAddress1;
                        }

                        // 最終処分事業場所在地2
                        jigyouba.LAST_SBN_JOU_ADDRESS2 = tempAddress2;
                        // 最終処分事業場所在地3
                        jigyouba.LAST_SBN_JOU_ADDRESS3 = tempAddress3;
                        // 最終処分事業場所在地4
                        jigyouba.LAST_SBN_JOU_ADDRESS4 = tempAddress4;

                        // 最終処分事業場電話番号
                        jigyouba.LAST_SBN_JOU_TEL = tempCheckTel;

                        // 最終処分終了日
                        if (!string.IsNullOrEmpty((dt.Rows[index]["LAST_SBN_END_DATE"] == null) ? string.Empty : dt.Rows[index]["LAST_SBN_END_DATE"].ToString()))
                            jigyouba.LAST_SBN_END_DATE = Convert.ToDateTime(dt.Rows[index]["LAST_SBN_END_DATE"]).ToString("d").Replace("/", "");
                        //2013/12/11 tyou add end
                        // レコード作成日時
                        // タイムスタンプ
                        var dataBinderjigyouba = new DataBinderLogic<DT_D13>(jigyouba);
                        dataBinderjigyouba.SetSystemProperty(jigyouba, true);
                        jigyoubaList.Add(jigyouba);
                    }
                }

                // 登録対象存在する場合
                if (exeFlg)
                {
                    // DBの情報で最終処分終了報告を行うため、ユーザへ知らせる。
                    string confMsg = "登録済みのデータで最終処分報告を行います。\n未登録のデータは破棄されますがよろしいですか。";

                    if (!(Shougun.Core.Message.MessageBoxUtility.MessageBoxShowConfirm(confMsg) == DialogResult.Yes))
                    {
                        return;
                    }
                }

                using (Transaction tran = new Transaction())
                {
                    // キュー情報
                    if (queList != null && queList.Count() > 0)
                    {
                        foreach (QUE_INFO que in queList)
                        {
                            // D12, D13のゴミデータ削除用にKANRI_IDをセット
                            kanriIds.Add(que.KANRI_ID);

                            // レコード枝番
                            que.QUE_SEQ = this.GetQueSeq(que.KANRI_ID, this.queDao);
                            // DBへ登録する
                            queDao.Insert(que);
                        }
                    }

                    // D12, D13に登録されているデータがそのままJWNETへ送信されるため
                    // ゴミが残っている場合は削除する
                    foreach (var kanriId in kanriIds)
                    {
                        // D12削除
                        var d12s = this.manifastDao.GetD12(kanriId);
                        foreach (var d12 in d12s)
                        {
                            this.manifastDao.Delete(d12);
                        }

                        // D13削除
                        var d13s = this.jigyoubaDao.GetD13(kanriId);
                        foreach (var d13 in d13s)
                        {
                            this.jigyoubaDao.Delete(d13);
                        }
                    }

                    // D12 2次マニフェスト情報
                    // D13 最終処分終了日・事業場情報
                    for (int i = 0; i < manifastList.Count(); i++)
                    {
                        // D12 2次マニフェスト情報
                        DT_D12 manifast = manifastList[i];
                        // レコード枝番
                        manifast.D12_SEQ = this.GetmanifastSeq(manifast.KANRI_ID, this.manifastDao);
                        // DBへ登録する
                        manifastDao.Insert(manifast);

                        // D13 最終処分終了日・事業場情報
                        DT_D13 jigyouba = jigyoubaList[i];
                        // レコード枝番
                        jigyouba.D12_SEQ = SqlInt16.Parse(Convert.ToString(manifast.D12_SEQ));
                        // レコード枝番
                        jigyouba.D13_SEQ = this.GetjigyoubaSeq(jigyouba.KANRI_ID, this.jigyoubaDao);
                        // DBへ登録する
                        jigyoubaDao.Insert(jigyouba);

                    }

                    // マニフェスト目次情報
                    if (mokujiList != null && mokujiList.Count() > 0)
                    {
                        foreach (DT_MF_TOC mokuji in mokujiList)
                        {
                            // DB更新
                            mokujiDao.Update(mokuji);
                        }
                    }
                    tran.Commit();
                }
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("I001", "登録");
                this.Search();
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
        /// オブジェクトを SqlBoolean に変換します
        /// </summary>
        /// <param name="value">変換するオブジェクト</param>
        /// <returns>変換したオブジェクト（変換するオブジェクトが null の場合は False）</returns>
        internal SqlBoolean ConvertToSqlBooleanDefaultFalse(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = SqlBoolean.False;
            if (false == this.IsNullOrEmpty(value))
            {
                ret = SqlBoolean.Parse(value.ToString());
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 指定されたオブジェクトが null または Empty文字列であるかどうかを示します
        /// </summary>
        /// <param name="value">テストする文字列</param>
        /// <returns>null または Empty文字列の場合は true それ以外の場合は false</returns>
        internal bool IsNullOrEmpty(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = false;
            if (null == value)
            {
                ret = true;
            }
            else if (true == String.IsNullOrEmpty(value.ToString()))
            {
                ret = true;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// キュー情報レコード最大枝番検索
        /// </summary>
        private SqlInt16 GetQueSeq(string kanriId, GetQueDaoCls dao)
        {
            SqlInt16 seq = 0;
            try
            {
                // レコード枝番
                DataTable dtSeq = new DataTable();
                GetMaxSeqDtoCls search = new GetMaxSeqDtoCls();
                search.kanriId = kanriId;
                dtSeq = dao.GetMaxSeq(search);
                //seqを設定する
                if (dtSeq != null && dtSeq.Rows.Count > 0)
                {
                    seq = Convert.ToInt16(dtSeq.Rows[0][0]);
                }
                seq += 1;
                // 取得した連番を返す
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            return seq;
        }

        /// <summary>
        /// D12 2次マニフェスト情報レコード最大枝番検索
        /// </summary>
        private SqlDecimal GetmanifastSeq(string kanriId, GetmanifastDaoCls dao)
        {
            SqlDecimal seq = 1;
            try
            {
                // レコード枝番
                DataTable dtSeq = new DataTable();
                GetMaxSeqDtoCls search = new GetMaxSeqDtoCls();
                search.kanriId = kanriId;
                dtSeq = dao.GetMaxSeq(search);
                //seqを設定する
                if (dtSeq != null && dtSeq.Rows.Count > 0)
                {
                    seq = Convert.ToInt16(dtSeq.Rows[0][0]) + 1;

                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            // 取得した連番を返す
            return seq;
        }

        /// <summary>
        /// D13 最終処分終了日・事業場情報レコード最大枝番検索
        /// </summary>
        private SqlInt16 GetjigyoubaSeq(string kanriId, GetjigyoubaDaoCls dao)
        {
            SqlInt16 seq = 0;
            try
            {
                // レコード枝番
                DataTable dtSeq = new DataTable();
                GetMaxSeqDtoCls search = new GetMaxSeqDtoCls();
                search.kanriId = kanriId;
                dtSeq = dao.GetMaxSeq(search);
                //seqを設定する
                if (dtSeq != null && dtSeq.Rows.Count > 0)
                {
                    seq = Convert.ToInt16(dtSeq.Rows[0][0]);
                }
                seq += 1;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            // 取得した連番を返す
            return seq;
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
            this.form.HAIKI_SHURUI_CD.Text = "";
            this.form.HAIKI_SHURUI_NAME.Text = "";
            this.form.HAIKI_NAME_CD.Text = "";
            this.form.HAIKI_NAME.Text = "";
            this.form.HAIKI_NAME_CD.Text = "";
            this.form.HAIKI_NAME.Text = "";
            this.form.Jigyousya_CD.Text = "";
            this.form.JIGYOUSHA_NAME.Text = "";
            this.form.Jigyoujou_CD.Text = "";
            this.form.JIGYOUJOU_NAME.Text = "";
            this.form.Unpan_CD.Text = "";
            this.form.Unpan_Name.Text = "";
            this.form.Unpansha_CD.Text = "";
            this.form.Unpansha_Name.Text = "";

            //並び順ソートヘッダー
            this.form.customSortHeader1.ClearCustomSortSetting();
            this.form.Ichiran.DataSource = this.dao_GetTME.GetElecManiDataNasi(string.Empty);
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
    }
}
