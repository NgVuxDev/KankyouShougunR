using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.IchiranCommon.Const;
using Shougun.Core.Message;
using Shougun.Core.PaperManifest.ManifestIchiran.DAO;

namespace Shougun.Core.PaperManifest.ManifestIchiran
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
        private string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.ManifestIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// DAO
        /// </summary>
        private GetTMEDaoCls dao_GetTME;
        private GetDMTDaoCls dao_GetDMT;

        /// <summary>
        /// DTO
        /// </summary>
        private TMEDtoCls dto_TME;

        // 20140604 katen 不具合No.4131 start‏
        /// <summary>
        /// DAO
        /// </summary>
        private HokokushoDaoCls dao_Hokokusho;
        private HaikiShuruiDaoCls dao_HaikiShurui;
        private HaikiNameDaoCls dao_HaikiName;

        /// <summary>
        /// DTO
        /// </summary>
        private HokokushoDtoCls dto_Hokosho;
        private HaikiShuruiDtoCls dto_HaikiShurui;
        private HaikiNameDtoCls dto_HaikiName;

        /// <summary>マニFlag</summary>
        internal int maniFlag = 1;

        /// <summary> 親フォーム</summary>
        public BusinessBaseForm parentbaseform { get; set; }
        // 20140604 katen 不具合No.4131 end‏

        // 20140610 katen 不具合No.4712 start‏
        /// <summary>
        /// 電子廃棄物名称CDマスタ情報
        /// </summary>
        public DataTable DenshiHaikiNameCodeResult { get; set; }

        /// <summary>
        /// 電子廃棄物種類マスタ情報
        /// </summary>
        public DataTable DenshiHaikiShuruiCodeResult { get; set; }

        /// <summary>
        /// 存在するチェック検索条件DTO
        /// </summary>
        public SearchExistDTOCls SearchExistDTO { get; set; }
        /// <summary>
        /// 電子廃棄物種類コード名称検索用Dao
        /// </summary>
        private DENSHI_HAIKI_SHURUIE_SearchDaoCls DENSHI_HAIKI_SHURUIE_SearchDao;

        /// <summary>
        /// 電子廃棄物名称コードと名称検索用Dao
        /// </summary>
        private DENSHI_HAIKI_NAME_SearchDaoCls DENSHI_HAIKI_NAME_SearchDao;
        // 20140610 katen 不具合No.4712 end‏

        /// <summary>
        /// Form
        /// </summary>
        private UIHeader header;
        private UIForm form;
        internal BusinessBaseForm footer;

        /// <summary>共通</summary>
        Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic mlogic = null;

        /// <summary>
        /// 作成したSQL
        /// </summary>
        public string createSql { get; set; }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        private MessageBoxShowLogic MsgBox;

        #endregion

        #region プロパティ

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
        /// 検索結果(マニフェストパターン)_画面遷移の確認用
        /// </summary>
        public DataTable Search_TME_Check { get; set; }

        /// <summary>
        /// 検索結果(マニフェストパターン)
        /// </summary>
        public DataTable Search_TME { get; set; }

        /// <summary>
        /// 検索結果(共通)
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 更新条件(マニフェストパターン)
        /// </summary>
        public List<T_MANIFEST_PT_ENTRY> TmeList { get; set; }

        /// <summary>
        /// コントロール
        /// </summary>
        internal Control[] allControl;

        //交付年月日（初期値：当日）
        public String KoufuDateFrom = DateTime.Now.Date.ToString();
        public String KoufuDateTo = DateTime.Now.Date.ToString();

        //2013.11.23 naitou update 交付年月日区分の追加 start
        //交付年月日区分（初期値：1 交付年月日あり）
        public String KoufuDateKbn = "1";
        //2013.11.23 naitou update 交付年月日区分の追加 end

        //廃棄物区分CD（初期値：1 産廃（直行））
        public String HaikiKbnCD = "1";

        #region 一覧列名

        /// <summary>SYSTEM_ID</summary>
        internal readonly string HIDDEN_SYSTEM_ID = "HIDDEN_SYSTEM_ID";

        /// <summary>SEQ</summary>
        internal readonly string HIDDEN_SEQ = "HIDDEN_SEQ";

        /// <summary>LATEST_SEQ</summary>
        internal readonly string HIDDEN_LATEST_SEQ = "HIDDEN_LATEST_SEQ";

        /// <summary>HAIKI_KBN</summary>
        internal readonly string HIDDEN_HAIKI_KBN = "HIDDEN_HAIKI_KBN";

        /// <summary>KANRI_ID</summary>
        internal readonly string HIDDEN_KANRI_ID = "HIDDEN_KANRI_ID";

        /// <summary>DT_MF_TOC.STATUS_FLAG</summary>
        internal readonly string HIDDEN_TOC_STATUS_FLAG = "HIDDEN_TOC_STATUS_FLAG";

        /// <summary>QUE_INFO.STATUS_FLAG</summary>
        internal readonly string HIDDEN_QUE_STATUS_FLAG = "HIDDEN_QUE_STATUS_FLAG";

        /// <summary>DETAIL_SYSTEM_ID</summary>
        internal readonly string HIDDEN_DETAIL_SYSTEM_ID = "HIDDEN_DETAIL_SYSTEM_ID";

        /// <summary>T_MANIFEST_DETAIL_PRT.REC_NO</summary>
        internal readonly string HIDDEN_PRT_REC = "HIDDEN_PRT_REC";

        /// <summary>T_MANIFEST_KP_KEIJYOU.REC_NO</summary>
        internal readonly string HIDDEN_KEIJYOU_REC = "HIDDEN_KEIJYOU_REC";

        /// <summary>T_MANIFEST_KP_NISUGATA.REC_NO</summary>
        internal readonly string HIDDEN_NISUGATA_REC = "HIDDEN_NISUGATA_REC";

        /// <summary>T_MANIFEST_KP_SBN_HOUHOU1.REC_NO（中間）</summary>
        internal readonly string HIDDEN_SBN_REC1 = "HIDDEN_SBN_REC1";

        /// <summary>T_MANIFEST_KP_SBN_HOUHOU2.REC_NO（最終）</summary>
        internal readonly string HIDDEN_SBN_REC2 = "HIDDEN_SBN_REC2";

        /// <summary>HST_GYOUSHA_CD</summary>
        internal readonly string HIDDEN_HST_GYOUSHA_CD = "HIDDEN_HST_GYOUSHA_CD";

        /// <summary>印字明細まとめ列名（直行、建廃のみ）</summary>
        internal readonly string PRT_DETAIL_COLUMN = "廃棄物種類(原本)";

        /// <summary>処分方法中間処理まとめ列名（建廃のみ）</summary>
        internal readonly string SBN_HOUHOU_CHUKAN_COLUMN = "処分方法中間処理";

        /// <summary>処分方法最終処分まとめ列名（建廃のみ）</summary>
        internal readonly string SBN_HOUHOU_SAISHU_COLUMN = "処分方法最終処分";

        /// <summary>形状まとめ列名（建廃のみ）</summary>
        internal readonly string KEIJOU_COLUMN = "形状(原本)";

        /// <summary>荷姿まとめ列名（建廃のみ）</summary>
        internal readonly string NISUGATA_COLUMN = "荷姿(原本)";

        #endregion

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.mlogic = new Common.BusinessCommon.Logic.ManifestoLogic();

            //DTO
            this.dto_TME = new TMEDtoCls();

            //DAO
            this.dao_GetTME = DaoInitUtility.GetComponent<DAO.GetTMEDaoCls>();
            this.dao_GetDMT = DaoInitUtility.GetComponent<DAO.GetDMTDaoCls>();

            // 20140604 katen 不具合No.4131 start‏
            //DTO
            this.dto_Hokosho = new HokokushoDtoCls();
            this.dto_HaikiShurui = new HaikiShuruiDtoCls();
            this.dto_HaikiName = new HaikiNameDtoCls();
            //DAO
            this.dao_Hokokusho = DaoInitUtility.GetComponent<DAO.HokokushoDaoCls>();
            this.dao_HaikiShurui = DaoInitUtility.GetComponent<DAO.HaikiShuruiDaoCls>();
            this.dao_HaikiName = DaoInitUtility.GetComponent<DAO.HaikiNameDaoCls>();
            // 20140604 katen 不具合No.4131 end‏

            this.Search_TME = new DataTable();
            this.Search_TME_Check = new DataTable();

            // 20140610 katen 不具合No.4712 start‏
            this.DENSHI_HAIKI_SHURUIE_SearchDao = DaoInitUtility.GetComponent<DENSHI_HAIKI_SHURUIE_SearchDaoCls>();
            this.DENSHI_HAIKI_NAME_SearchDao = DaoInitUtility.GetComponent<DENSHI_HAIKI_NAME_SearchDaoCls>();
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            //マスタデータを取得
            //this.GetPopUpDenshiHaikiNameData();
            this.GetPopUpDenshiHaikiShuruiData();
            // 20140610 katen 不具合No.4712 end‏
            this.MsgBox = new MessageBoxShowLogic();

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

            //新規ボタン(F2)イベント生成
            parentForm.bt_func2.Click += new EventHandler(this.form.bt_func2_Click);

            //修正ボタン(F3)イベント生成
            parentForm.bt_func3.Click += new EventHandler(this.form.bt_func3_Click);

            //削除ボタン(F4)イベント生成
            parentForm.bt_func4.Click += new EventHandler(this.form.bt_func4_Click);

            //CSV出力ボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.bt_func6_Click);

            //条件クリアボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.bt_func7_Click);

            //検索ボタン(F8)イベント生成
            this.form.C_Regist(parentForm.bt_func8);
            parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);
            parentForm.bt_func8.ProcessKbn = PROCESS_KBN.NEW;

            //並替移動ボタン(F10)イベント生成
            parentForm.bt_func10.Click += new EventHandler(this.form.bt_func10_Click);

            //フィルタボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.bt_func11_Click);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            //【1】パターン一覧(1)イベント生成
            parentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);

            //【2】検索条件設定(2)イベント生成
            parentForm.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);

            //前回値保存の仕組み初期化
            this.form.EnterEventInit();

            /// 20141128 Houkakou 「マニ一覧」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.form.KOUFU_DATE_TO.MouseDoubleClick += new MouseEventHandler(KOUFU_DATE_TO_MouseDoubleClick);
            /// 20141128 Houkakou 「マニ一覧」のダブルクリックを追加する　end

            this.form.KOUFUBANNGOTo.MouseDoubleClick += new MouseEventHandler(KOUFUBANNGOTo_MouseDoubleClick);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// マニライト(C8)モード用初期化処理
        /// </summary>
        private void ManiLiteInit()
        {
            // サブファンクション2(2次マニ)機能の非表示

            // ボタン初期化
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.bt_process2.Text = string.Empty;
            parentForm.bt_process2.Enabled = false;

            // イベント削除
            //【2】1次・2次マニイベント
            parentForm.bt_process2.Click -= new EventHandler(this.form.bt_process2_Click);
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                //DTO
                TMEDtoCls TMPEDtoCls = new TMEDtoCls();

                // 20140604 katen 不具合No.4131 start‏
                this.parentbaseform = (BusinessBaseForm)this.form.Parent;
                // 20140604 katen 不具合No.4131 end‏

                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                if (AppConfig.IsManiLite)
                {
                    // マニライト版(C8)の初期化処理
                    ManiLiteInit();
                }

                //交付年月日（初期値：当日）
                KoufuDateFrom = this.parentbaseform.sysDate.ToString();
                KoufuDateTo = this.parentbaseform.sysDate.ToString();

                this.allControl = this.form.allControl;

                // 継承したフォームのDGVのプロパティはデザイナで変更できない為、ここで設定
                this.form.customDataGridView1.AllowUserToAddRows = false;                                //行の追加オプション(false)
                this.form.customDataGridView1.Height = 263;

                // 20140603 katen 不具合No.4131 start‏
                this.form.customDataGridView1.TabIndex = 30;
                this.form.HAIKI_KBN_CD.Focus();
                // 20140603 katen 不具合No.4131 end‏

                //アラート件数
                M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
                this.header.InitialNumberAlert = int.Parse(mSysInfo.ICHIRAN_ALERT_KENSUU.ToString());
                this.header.NumberAlert = this.header.InitialNumberAlert;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion
        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <returns></returns>
        internal Boolean SearchCheck()
        {
            bool isErr = true;
            try
            {
                LogUtility.DebugMethodStart();

                var allControlAndHeaderControls = allControl.ToList();
                allControlAndHeaderControls.AddRange(this.form.controlUtil.GetAllControls(this.header));
                var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());
                this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
                if (this.form.RegistErrorFlag)
                {
                    //必須チェックエラーフォーカス処理
                    this.SetErrorFocus();

                    LogUtility.DebugMethodEnd(isErr);
                    return isErr;
                }

                // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 start
                if (CheckDate())
                {
                    LogUtility.DebugMethodEnd(isErr);
                    return isErr;
                }
                // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 end
                isErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                isErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isErr);
            }
            return isErr;
        }

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

        //アラート件数
        public Boolean CheckNumberAlert(Int32 Kensu)
        {
            LogUtility.DebugMethodStart();

            Boolean check = false;
            if (Int32.Parse(this.header.NumberAlert.ToString()) < Kensu)
            {
                switch (MessageBoxUtility.MessageBoxShow("C025"))
                {
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                        check = true;
                        break;
                }
            }

            LogUtility.DebugMethodEnd();
            return check;
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        public void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            try
            {

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
        /// 物理削除処理
        /// </summary>
        public void PhysicalDelete()
        {
            LogUtility.DebugMethodStart();

            try
            {

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
        /// 登録
        /// </summary>
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart();

            try
            {

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
        /// 検索
        /// </summary>
        public int Search()
        {
            LogUtility.DebugMethodStart();
            Int32 count_TME = 0;
            try
            {
                var DateFrom = string.Empty;
                var DateTo = string.Empty;

                // 日付FROMのNULL対策
                if (this.form.KOUFU_DATE_FROM.Value != null)
                {
                    DateFrom = this.form.KOUFU_DATE_FROM.Value.ToString();
                }

                // 日付TOのNULL対策
                if (this.form.KOUFU_DATE_TO.Value != null)
                {
                    DateTo = this.form.KOUFU_DATE_TO.Value.ToString();
                }

                count_TME = this.Get_Search_TME(
                        DateFrom,
                        DateTo,
                        this.form.HAIKI_KBN_CD.Text.ToString(),
                        "false",
                        "",
                        "",
                        this.form.KOUFU_DATE_KBN.Text.ToString(), //2013.11.23 naitou update 交付年月日区分の追加
                        "",
                        "");

                this.Set_Search_TME();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                count_TME = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count_TME);
            }
            //取得件数
            return count_TME;
        }

        /// <summary>
        /// 廃棄物区分 必須チェック
        /// </summary>
        public Boolean Haiki_Kbn_CD_Check()
        {
            LogUtility.DebugMethodStart();

            Boolean check = false;
            try
            {
                switch (this.form.HAIKI_KBN_CD.Text)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                        break;

                    default:
                        MessageBoxUtility.MessageBoxShow("W001", "1", "5");
                        //フォーカスを出力区分へ移動
                        this.form.HAIKI_KBN_CD.Select();
                        check = true;
                        break;
                }
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
            return check;
        }

        /// <summary>
        /// データ取得
        /// </summary>
        public int Get_Search_TME(String KOUFU_DATE_FROM, String KOUFU_DATE_TO, String HAIKI_KBN_CD, String DELETE_FLG,
                                    String SYSTEM_ID, String SEQ, String KOUFU_DATE_KBN,
                                    String KANRI_ID, String LATEST_SEQ)
        {
            LogUtility.DebugMethodStart(KOUFU_DATE_FROM, KOUFU_DATE_TO, HAIKI_KBN_CD, DELETE_FLG, SYSTEM_ID, SEQ, KOUFU_DATE_KBN, KANRI_ID, LATEST_SEQ);

            var count = -1;

            // 検索文字列取得
            this.selectQuery = this.form.SelectQuery;
            this.orderByQuery = this.form.OrderByQuery;
            this.joinQuery = this.form.JoinQuery;

            switch (HAIKI_KBN_CD)
            {
                case "1":
                case "2":
                case "3":
                    // 産廃直行、産廃積替、建廃の場合はIchiranCommonを利用
                    count = this.Get_Search_PaperMani(KOUFU_DATE_FROM, KOUFU_DATE_TO, HAIKI_KBN_CD, DELETE_FLG, SYSTEM_ID, SEQ, KOUFU_DATE_KBN, KANRI_ID, LATEST_SEQ);
                    break;
                case "4":
                    count = this.Get_Search_DenshiMani(KOUFU_DATE_FROM, KOUFU_DATE_TO, HAIKI_KBN_CD, DELETE_FLG, SYSTEM_ID, SEQ, KOUFU_DATE_KBN, KANRI_ID, LATEST_SEQ);
                    break;
                case "5":
                    count = this.Get_Search_AllMani(KOUFU_DATE_FROM, KOUFU_DATE_TO, HAIKI_KBN_CD, DELETE_FLG, SYSTEM_ID, SEQ, KOUFU_DATE_KBN, KANRI_ID, LATEST_SEQ);
                    break;
                default:
                    return -1;
            }

            LogUtility.DebugMethodEnd(count);

            return count;
        }

        /// <summary>
        /// データ取得（紙マニ）
        /// </summary>
        /// <param name="KOUFU_DATE_FROM"></param>
        /// <param name="KOUFU_DATE_TO"></param>
        /// <param name="HAIKI_KBN_CD"></param>
        /// <param name="DELETE_FLG"></param>
        /// <param name="SYSTEM_ID"></param>
        /// <param name="SEQ"></param>
        /// <param name="KOUFU_DATE_KBN"></param>
        /// <param name="KANRI_ID"></param>
        /// <param name="LATEST_SEQ"></param>
        /// <returns></returns>
        private int Get_Search_PaperMani(String KOUFU_DATE_FROM, String KOUFU_DATE_TO, String HAIKI_KBN_CD, String DELETE_FLG,
                                    String SYSTEM_ID, String SEQ, String KOUFU_DATE_KBN, String KANRI_ID, String LATEST_SEQ)
        {
            LogUtility.DebugMethodStart(KOUFU_DATE_FROM, KOUFU_DATE_TO, HAIKI_KBN_CD, DELETE_FLG, SYSTEM_ID, SEQ, KOUFU_DATE_KBN, KANRI_ID, LATEST_SEQ);

            int count = 0;

            try
            {
                var sql = new StringBuilder();

                #region SELECT句

                sql.Append("SELECT DISTINCT ");
                sql.Append(this.selectQuery);
                sql.AppendFormat(", T_MANIFEST_ENTRY.SYSTEM_ID AS {0} ", this.HIDDEN_SYSTEM_ID);
                sql.AppendFormat(", T_MANIFEST_ENTRY.SEQ AS {0} ", this.HIDDEN_SEQ);
                sql.AppendFormat(", '' AS {0} ", this.HIDDEN_LATEST_SEQ);
                sql.AppendFormat(", '' AS {0} ", this.HIDDEN_KANRI_ID);
                sql.AppendFormat(", T_MANIFEST_ENTRY.HAIKI_KBN_CD AS {0} ", this.HIDDEN_HAIKI_KBN);
                sql.AppendFormat(", T_MANIFEST_ENTRY.HST_GYOUSHA_CD AS {0} ", this.HIDDEN_HST_GYOUSHA_CD);

                if (this.form.CurrentPatternOutputKbn == (int)OUTPUT_KBN.MEISAI)
                {
                    sql.AppendFormat(", T_MANIFEST_DETAIL.DETAIL_SYSTEM_ID AS {0} ", this.HIDDEN_DETAIL_SYSTEM_ID);
                }
                // 印字明細キー列
                if (this.selectQuery.Contains("T_MANIFEST_DETAIL_PRT") || this.joinQuery.Contains("T_MANIFEST_DETAIL_PRT"))
                {
                    sql.AppendFormat(", T_MANIFEST_DETAIL_PRT.REC_NO AS {0} ", this.HIDDEN_PRT_REC);
                }

                // 形状キー列
                if (this.selectQuery.Contains("T_MANIFEST_KP_KEIJYOU") || this.joinQuery.Contains("T_MANIFEST_KP_KEIJYOU"))
                {
                    sql.AppendFormat(", T_MANIFEST_KP_KEIJYOU.REC_NO AS {0} ", this.HIDDEN_KEIJYOU_REC);
                }

                // 荷姿キー列
                if (this.selectQuery.Contains("T_MANIFEST_KP_NISUGATA") || this.joinQuery.Contains("T_MANIFEST_KP_NISUGATA"))
                {
                    sql.AppendFormat(", T_MANIFEST_KP_NISUGATA.REC_NO AS {0} ", this.HIDDEN_NISUGATA_REC);
                }

                // 中間処分方法キー列
                if (this.selectQuery.Contains("T_MANIFEST_KP_SBN_HOUHOU1") || this.joinQuery.Contains("T_MANIFEST_KP_SBN_HOUHOU1"))
                {
                    sql.AppendFormat(", T_MANIFEST_KP_SBN_HOUHOU1.REC_NO AS {0} ", this.HIDDEN_SBN_REC1);
                }

                // 最終処分方法キー列
                if (this.selectQuery.Contains("T_MANIFEST_KP_SBN_HOUHOU2") || this.joinQuery.Contains("T_MANIFEST_KP_SBN_HOUHOU2"))
                {
                    sql.AppendFormat(", T_MANIFEST_KP_SBN_HOUHOU2.REC_NO AS {0} ", this.HIDDEN_SBN_REC2);
                }

                string errorDefault = "\'0\'";
                sql.AppendFormat(" ,{0} AS HST_GYOUSHA_CD_ERROR", errorDefault);
                sql.AppendFormat(" ,{0} AS HST_GENBA_CD_ERROR", errorDefault);
                sql.AppendFormat(" ,{0} AS HAIKI_SHURUI_CD_ERROR", errorDefault);

                #endregion

                #region FROM句

                // マニ
                sql.Append(" FROM T_MANIFEST_ENTRY ");

                if (this.selectQuery.Contains("T_MANIFEST_PRT") || this.joinQuery.Contains("T_MANIFEST_PRT"))
                {
                    // マニ印字
                    sql.Append(" LEFT JOIN T_MANIFEST_PRT ");
                    sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_PRT.SYSTEM_ID ");
                    sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_PRT.SEQ ");
                }

                if (this.selectQuery.Contains("T_MANIFEST_DETAIL_PRT") || this.joinQuery.Contains("T_MANIFEST_DETAIL_PRT"))
                {
                    // マニ印字明細
                    sql.Append(" LEFT JOIN T_MANIFEST_DETAIL_PRT ");
                    sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_DETAIL_PRT.SYSTEM_ID ");
                    sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_DETAIL_PRT.SEQ ");
                }

                if (this.selectQuery.Contains("T_MANIFEST_KP_KEIJYOU") || this.joinQuery.Contains("T_MANIFEST_KP_KEIJYOU"))
                {
                    // 形状（建廃）
                    sql.Append(" LEFT JOIN T_MANIFEST_KP_KEIJYOU ");
                    sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_KP_KEIJYOU.SYSTEM_ID ");
                    sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_KP_KEIJYOU.SEQ ");
                }

                if (this.selectQuery.Contains("T_MANIFEST_KP_NISUGATA") || this.joinQuery.Contains("T_MANIFEST_KP_NISUGATA"))
                {
                    // 荷姿（建廃）
                    sql.Append(" LEFT JOIN T_MANIFEST_KP_NISUGATA ");
                    sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_KP_NISUGATA.SYSTEM_ID ");
                    sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_KP_NISUGATA.SEQ ");
                }

                if (this.selectQuery.Contains("T_MANIFEST_KP_SBN_HOUHOU1") || this.joinQuery.Contains("T_MANIFEST_KP_SBN_HOUHOU1"))
                {
                    // 中間処分方法（建廃）
                    sql.Append(" LEFT JOIN T_MANIFEST_KP_SBN_HOUHOU T_MANIFEST_KP_SBN_HOUHOU1 ");
                    sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_KP_SBN_HOUHOU1.SYSTEM_ID ");
                    sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_KP_SBN_HOUHOU1.SEQ ");
                    sql.Append(" AND T_MANIFEST_KP_SBN_HOUHOU1.REC_NO < 10 ");
                }

                if (this.selectQuery.Contains("T_MANIFEST_KP_SBN_HOUHOU2") || this.joinQuery.Contains("T_MANIFEST_KP_SBN_HOUHOU2"))
                {
                    // 最終処分方法（建廃）
                    sql.Append(" LEFT JOIN T_MANIFEST_KP_SBN_HOUHOU T_MANIFEST_KP_SBN_HOUHOU2 ");
                    sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_KP_SBN_HOUHOU2.SYSTEM_ID ");
                    sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_KP_SBN_HOUHOU2.SEQ ");
                    sql.Append(" AND T_MANIFEST_KP_SBN_HOUHOU2.REC_NO >= 10 ");
                }

                if (this.selectQuery.Contains("T_MANIFEST_UPN1") || this.joinQuery.Contains("T_MANIFEST_UPN1"))
                {
                    // マニ運搬1
                    sql.Append(" LEFT JOIN T_MANIFEST_UPN T_MANIFEST_UPN1 ");
                    sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_UPN1.SYSTEM_ID ");
                    sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_UPN1.SEQ ");
                    sql.Append(" AND T_MANIFEST_UPN1.UPN_ROUTE_NO = 1 ");
                }

                if (this.selectQuery.Contains("T_MANIFEST_UPN2") || this.joinQuery.Contains("T_MANIFEST_UPN2"))
                {
                    // マニ運搬2
                    sql.Append(" LEFT JOIN T_MANIFEST_UPN T_MANIFEST_UPN2 ");
                    sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_UPN2.SYSTEM_ID ");
                    sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_UPN2.SEQ ");
                    sql.Append(" AND T_MANIFEST_UPN2.UPN_ROUTE_NO = 2 ");
                }

                if (this.selectQuery.Contains("T_MANIFEST_UPN3") || this.joinQuery.Contains("T_MANIFEST_UPN3"))
                {
                    // マニ運搬3
                    sql.Append(" LEFT JOIN T_MANIFEST_UPN T_MANIFEST_UPN3 ");
                    sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_UPN3.SYSTEM_ID ");
                    sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_UPN3.SEQ ");
                    sql.Append(" AND T_MANIFEST_UPN3.UPN_ROUTE_NO = 3 ");
                }

                if (this.selectQuery.Contains("T_MANIFEST_PT_KP_KEIJYOU") || this.joinQuery.Contains("T_MANIFEST_PT_KP_KEIJYOU"))
                {
                    // 建廃形状
                    sql.Append(" LEFT JOIN T_MANIFEST_PT_KP_KEIJYOU ");
                    sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_PT_KP_KEIJYOU.SYSTEM_ID ");
                    sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_PT_KP_KEIJYOU.SEQ ");
                }

                if (this.selectQuery.Contains("T_MANIFEST_PT_KP_NISUGATA") || this.joinQuery.Contains("T_MANIFEST_PT_KP_NISUGATA"))
                {
                    // 建廃荷姿
                    sql.Append(" LEFT JOIN T_MANIFEST_PT_KP_NISUGATA ");
                    sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_PT_KP_NISUGATA.SYSTEM_ID ");
                    sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_PT_KP_NISUGATA.SEQ ");
                }

                if (this.selectQuery.Contains("T_MANIFEST_PT_KP_SBN_HOUHOU") || this.joinQuery.Contains("T_MANIFEST_PT_KP_SBN_HOUHOU"))
                {
                    // 建廃処分方法
                    sql.Append(" LEFT JOIN T_MANIFEST_PT_KP_SBN_HOUHOU ");
                    sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_PT_KP_SBN_HOUHOU.SYSTEM_ID ");
                    sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_PT_KP_SBN_HOUHOU.SEQ ");
                }

                if (this.selectQuery.Contains("T_MANIFEST_RET_DATE") || this.joinQuery.Contains("T_MANIFEST_RET_DATE"))
                {
                    // 返却日
                    sql.Append(" LEFT JOIN T_MANIFEST_RET_DATE ");
                    sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_RET_DATE.SYSTEM_ID ");
                    sql.Append(" AND T_MANIFEST_RET_DATE.DELETE_FLG = 0 ");
                }

                if ((this.form.CurrentPatternOutputKbn != (int)OUTPUT_KBN.MEISAI && (!string.IsNullOrEmpty(this.form.cantxt_HokokushoBunrui.Text) || !string.IsNullOrEmpty(this.form.cantxt_HaikibutuShurui.Text)
                    || !string.IsNullOrEmpty(this.form.cantxt_HaikibutuName.Text) || KOUFU_DATE_KBN == "3" || KOUFU_DATE_KBN == "4"
                    // 20140609 kayo 不具合No.4710 報告書分類を選択できるように修正 start‏
                    || this.selectQuery.Contains("M_HOUKOKUSHO_BUNRUI")
                    // 20140609 kayo 不具合No.4710 報告書分類を選択できるように修正 end‏
                    ))
                    || (this.selectQuery.Contains("T_MANIFEST_DETAIL") || this.joinQuery.Contains("T_MANIFEST_DETAIL") || this.form.CurrentPatternOutputKbn == (int)OUTPUT_KBN.MEISAI)
                    )
                {
                    sql.Append("      LEFT OUTER JOIN T_MANIFEST_DETAIL WITH(NOLOCK) ");
                    sql.Append("                   ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_DETAIL.SYSTEM_ID ");
                    sql.Append("                  AND T_MANIFEST_ENTRY.SEQ       = T_MANIFEST_DETAIL.SEQ ");
                }

                if (this.form.CurrentPatternOutputKbn == (int)OUTPUT_KBN.MEISAI)
                {

                    // 二次マニ
                    sql.Append(" LEFT JOIN T_MANIFEST_RELATION ");
                    sql.Append(" ON T_MANIFEST_DETAIL.DETAIL_SYSTEM_ID = T_MANIFEST_RELATION.FIRST_SYSTEM_ID ");
                    sql.Append(" AND T_MANIFEST_RELATION.DELETE_FLG = 0 ");
                    sql.Append(" AND T_MANIFEST_RELATION.FIRST_HAIKI_KBN_CD <> 4 ");
                    sql.Append(" AND T_MANIFEST_RELATION.REC_SEQ = (SELECT MAX(TMP.REC_SEQ) FROM T_MANIFEST_RELATION TMP ");
                    sql.Append(" WHERE TMP.FIRST_SYSTEM_ID = T_MANIFEST_RELATION.FIRST_SYSTEM_ID AND TMP.DELETE_FLG = 0 AND TMP.FIRST_HAIKI_KBN_CD <> 4) ");
                    sql.Append(" LEFT JOIN (");
                    sql.Append(" SELECT DISTINCT");
                    sql.Append(" MR.NEXT_SYSTEM_ID AS SYSTEM_ID");
                    sql.Append(" ,MR.NEXT_HAIKI_KBN_CD AS HAIKI_KBN_CD");
                    sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN EX.MANIFEST_ID ELSE ME.MANIFEST_ID END) AS MANIFEST_ID");
                    sql.Append(" FROM T_MANIFEST_RELATION AS MR");
                    sql.Append(" LEFT JOIN");

                    // 2016.11.23 chinkeigen マニ入力と一覧 #101092 
                    //sql.Append(" (SELECT * FROM T_MANIFEST_ENTRY WHERE DELETE_FLG = 0) AS ME");
                    //sql.Append(" ON MR.NEXT_SYSTEM_ID = ME.SYSTEM_ID");
                    sql.Append(" (SELECT T_MANIFEST_ENTRY.*,T_MANIFEST_DETAIL.DETAIL_SYSTEM_ID ");
                    sql.Append("    FROM T_MANIFEST_ENTRY LEFT JOIN T_MANIFEST_DETAIL ON ");
                    sql.Append("    T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_DETAIL.SYSTEM_ID ");
                    sql.Append("  WHERE DELETE_FLG = 0) AS ME");
                    sql.Append(" ON MR.NEXT_SYSTEM_ID = ME.DETAIL_SYSTEM_ID");
                    // 2016.11.23 chinkeigen マニ入力と一覧 #101092 

                    sql.Append(" AND MR.NEXT_HAIKI_KBN_CD = ME.HAIKI_KBN_CD");
                    sql.Append(" LEFT JOIN");
                    sql.Append(" (SELECT * FROM DT_R18_EX WHERE DELETE_FLG = 0) AS EX");
                    sql.Append(" ON MR.NEXT_SYSTEM_ID = EX.SYSTEM_ID");
                    sql.Append(" AND MR.NEXT_HAIKI_KBN_CD = 4");
                    sql.Append(" WHERE MR.DELETE_FLG = 0");
                    sql.Append(" ) AS T_MANIFEST_ENTRY_NIJI");
                    sql.Append(" ON T_MANIFEST_RELATION.NEXT_SYSTEM_ID = T_MANIFEST_ENTRY_NIJI.SYSTEM_ID");
                    sql.Append(" AND T_MANIFEST_RELATION.NEXT_HAIKI_KBN_CD = T_MANIFEST_ENTRY_NIJI.HAIKI_KBN_CD");
                }

                // 20140603 katen 不具合No.4131 start‏
                if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyutakuNameCd.Text) || !string.IsNullOrEmpty(this.form.cantxt_UnpanJyugyobaNameCd.Text)
                    || KOUFU_DATE_KBN == "2" || !string.IsNullOrEmpty(this.form.cantxt_TsumikaehokanGyoushaCd.Text) || !string.IsNullOrEmpty(this.form.cantxt_TsumikaehokanGyoushaCd.Text))
                {
                    sql.Append("            LEFT JOIN T_MANIFEST_UPN AS TMU_CONDITION WITH(NOLOCK)");
                    sql.Append("                   ON T_MANIFEST_ENTRY.SYSTEM_ID = TMU_CONDITION.SYSTEM_ID");
                    sql.Append("                  AND T_MANIFEST_ENTRY.SEQ       = TMU_CONDITION.SEQ");
                    sql.Append("            LEFT JOIN (SELECT T_MANIFEST_ENTRY.SYSTEM_ID,");
                    sql.Append("                              T_MANIFEST_ENTRY.SEQ,");
                    sql.Append("                              MAX(T_MANIFEST_UPN.UPN_ROUTE_NO) AS UPN_ROUTE_NO");
                    sql.Append("                         FROM T_MANIFEST_ENTRY WITH(NOLOCK)");
                    sql.Append("                   INNER JOIN T_MANIFEST_UPN WITH(NOLOCK)");
                    sql.Append("                           ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_UPN.SYSTEM_ID");
                    sql.Append("                          AND T_MANIFEST_ENTRY.SEQ       = T_MANIFEST_UPN.SEQ");
                    sql.Append("                          AND T_MANIFEST_UPN.UPN_END_DATE IS NOT NULL");
                    sql.Append("                        WHERE T_MANIFEST_ENTRY.DELETE_FLG = 'false'");
                    sql.Append("                     GROUP BY T_MANIFEST_ENTRY.SYSTEM_ID,");
                    sql.Append("                              T_MANIFEST_ENTRY.SEQ) TMU_SEARCH");
                    sql.Append("                   ON TMU_CONDITION.SYSTEM_ID    = TMU_SEARCH.SYSTEM_ID");
                    sql.Append("                  AND TMU_CONDITION.SEQ          = TMU_SEARCH.SEQ");
                    sql.Append("                  AND TMU_CONDITION.UPN_ROUTE_NO = TMU_SEARCH.UPN_ROUTE_NO ");
                }

                if (!string.IsNullOrEmpty(this.form.cantxt_HokokushoBunrui.Text)
                    // 20140609 kayo 不具合No.4710 報告書分類を選択できるように修正 start‏
                    || this.selectQuery.Contains("M_HOUKOKUSHO_BUNRUI")
                    || this.selectQuery.Contains("M_HAIKI_SHURUI")
                    // 20140609 kayo 不具合No.4710 報告書分類を選択できるように修正 end‏
                    )
                {
                    sql.Append("            LEFT JOIN M_HAIKI_SHURUI WITH(NOLOCK) ");
                    sql.Append("                   ON T_MANIFEST_DETAIL.HAIKI_SHURUI_CD = M_HAIKI_SHURUI.HAIKI_SHURUI_CD ");
                    // 20140610 kayo 不具合No.4710 報告書分類を選択できるように修正 start‏
                    sql.Append("                   AND T_MANIFEST_ENTRY.HAIKI_KBN_CD = M_HAIKI_SHURUI.HAIKI_KBN_CD ");
                    // 20140610 kayo 不具合No.4710 報告書分類を選択できるように修正 end
                }

                // 20140609 kayo 不具合No.4710 報告書分類を選択できるように修正 start‏
                if (this.selectQuery.Contains("M_HOUKOKUSHO_BUNRUI"))
                {
                    sql.Append("            LEFT JOIN M_HOUKOKUSHO_BUNRUI WITH(NOLOCK) ");
                    sql.Append("                   ON M_HAIKI_SHURUI.HOUKOKUSHO_BUNRUI_CD = M_HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_CD ");
                }
                // 20140609 kayo 不具合No.4710 報告書分類を選択できるように修正 end

                // 20140603 katen 不具合No.4131 end‏

                // 20140605 kayo 不具合No.4523 連携番号と連携明細行項目が正しく表示されてない対応 start‏
                if (this.selectQuery.Contains("RENKEI_TBL_E") || this.joinQuery.Contains("RENKEI_TBL_E"))
                {
                    sql.Append(" LEFT JOIN (SELECT ");
                    sql.Append("                " + (int)DENSHU_KBN.UKEIRE + " RENKEI_FLG,");
                    sql.Append("                RE.SYSTEM_ID SYSTEM_ID,");
                    sql.Append("                RE.UKEIRE_NUMBER RENKEI_NUMBER");
                    sql.Append("            FROM T_UKEIRE_ENTRY RE");
                    sql.Append("            UNION");
                    sql.Append("            SELECT");
                    sql.Append("                " + (int)DENSHU_KBN.SHUKKA + " RENKEI_FLG,");
                    sql.Append("                RE.SYSTEM_ID SYSTEM_ID,");
                    sql.Append("                RE.SHUKKA_NUMBER RENKEI_NUMBER");
                    sql.Append("            FROM T_SHUKKA_ENTRY RE ");
                    sql.Append("            WHERE RE.DELETE_FLG = 0");
                    sql.Append("            UNION");
                    sql.Append("            SELECT");
                    sql.Append("                " + (int)DENSHU_KBN.URIAGE_SHIHARAI + " RENKEI_FLG,");
                    sql.Append("                RE.SYSTEM_ID SYSTEM_ID,");
                    sql.Append("                RE.UR_SH_NUMBER RENKEI_NUMBER");
                    sql.Append("            FROM T_UR_SH_ENTRY RE ");
                    sql.Append("            WHERE RE.DELETE_FLG = 0");
                    sql.Append("            UNION");
                    sql.Append("            SELECT");
                    sql.Append("                " + (int)DENSHU_KBN.KEIRYOU + " RENKEI_FLG,");
                    sql.Append("                RE.SYSTEM_ID SYSTEM_ID,");
                    sql.Append("                RE.KEIRYOU_NUMBER RENKEI_NUMBER");
                    sql.Append("            FROM T_KEIRYOU_ENTRY RE ");
                    sql.Append("            WHERE RE.DELETE_FLG = 0");
                    sql.Append("            UNION");
                    sql.Append("            SELECT");
                    sql.Append("                " + (int)DENSHU_KBN.UKETSUKE + " RENKEI_FLG,");
                    sql.Append("                RE.SYSTEM_ID SYSTEM_ID,");
                    sql.Append("                RE.UKETSUKE_NUMBER RENKEI_NUMBER");
                    sql.Append("            FROM T_UKETSUKE_MK_ENTRY RE ");
                    sql.Append("            WHERE RE.DELETE_FLG = 0");
                    sql.Append("            UNION");
                    sql.Append("            SELECT");
                    sql.Append("                " + (int)DENSHU_KBN.UKETSUKE + " RENKEI_FLG,");
                    sql.Append("                RE.SYSTEM_ID SYSTEM_ID,");
                    sql.Append("                RE.UKETSUKE_NUMBER RENKEI_NUMBER");
                    sql.Append("            FROM T_UKETSUKE_SK_ENTRY RE ");
                    sql.Append("            WHERE RE.DELETE_FLG = 0");
                    sql.Append("            UNION");
                    sql.Append("            SELECT");
                    sql.Append("                " + (int)DENSHU_KBN.UKETSUKE + " RENKEI_FLG,");
                    sql.Append("                RE.SYSTEM_ID SYSTEM_ID,");
                    sql.Append("                RE.UKETSUKE_NUMBER RENKEI_NUMBER");
                    sql.Append("            FROM T_UKETSUKE_SS_ENTRY RE ");
                    sql.Append("            WHERE RE.DELETE_FLG = 0");
                    sql.Append("           ) RENKEI_TBL_E");
                    sql.Append(" ON T_MANIFEST_ENTRY.RENKEI_SYSTEM_ID = RENKEI_TBL_E.SYSTEM_ID");
                    sql.Append("   AND T_MANIFEST_ENTRY.RENKEI_DENSHU_KBN_CD = RENKEI_TBL_E.RENKEI_FLG");
                }

                if (this.selectQuery.Contains("RENKEI_TBL_D") || this.joinQuery.Contains("RENKEI_TBL_D"))
                {

                    sql.Append(" LEFT JOIN (SELECT ");
                    sql.Append("                " + (int)DENSHU_KBN.UKEIRE + " RENKEI_FLG, ");
                    sql.Append("                RE.SYSTEM_ID SYSTEM_ID,");
                    sql.Append("                RD.DETAIL_SYSTEM_ID DETAIL_SYSTEM_ID, ");
                    sql.Append("                RD.ROW_NO RENKEI_ROW_NUMBER");
                    sql.Append("           FROM (SELECT TUE.* FROM T_UKEIRE_ENTRY TUE WHERE TUE.DELETE_FLG = 0) RE ");
                    sql.Append("            LEFT JOIN T_UKEIRE_DETAIL RD ON RE.SYSTEM_ID = RD.SYSTEM_ID AND RE.SEQ = RD.SEQ  ");
                    sql.Append("           UNION ");
                    sql.Append("           SELECT ");
                    sql.Append("                " + (int)DENSHU_KBN.SHUKKA + " RENKEI_FLG, ");
                    sql.Append("                RE.SYSTEM_ID SYSTEM_ID,");
                    sql.Append("                RD.DETAIL_SYSTEM_ID DETAIL_SYSTEM_ID, ");
                    sql.Append("                RD.ROW_NO RENKEI_ROW_NUMBER");
                    sql.Append("           FROM (SELECT TSE.* FROM T_SHUKKA_ENTRY TSE WHERE TSE.DELETE_FLG = 0) RE ");
                    sql.Append("            LEFT JOIN T_SHUKKA_DETAIL RD ON RE.SYSTEM_ID = RD.SYSTEM_ID AND RE.SEQ = RD.SEQ  ");
                    sql.Append("           UNION ");
                    sql.Append("           SELECT ");
                    sql.Append("                " + (int)DENSHU_KBN.URIAGE_SHIHARAI + " RENKEI_FLG, ");
                    sql.Append("                RE.SYSTEM_ID SYSTEM_ID,");
                    sql.Append("                RD.DETAIL_SYSTEM_ID DETAIL_SYSTEM_ID, ");
                    sql.Append("                RD.ROW_NO RENKEI_ROW_NUMBER");
                    sql.Append("           FROM (SELECT TUS.* FROM T_UR_SH_ENTRY TUS WHERE TUS.DELETE_FLG = 0) RE ");
                    sql.Append("            LEFT JOIN T_UR_SH_DETAIL RD ON RE.SYSTEM_ID = RD.SYSTEM_ID AND RE.SEQ = RD.SEQ  ");
                    sql.Append("           UNION ");
                    sql.Append("           SELECT ");
                    sql.Append("                " + (int)DENSHU_KBN.KEIRYOU + " RENKEI_FLG, ");
                    sql.Append("                RE.SYSTEM_ID SYSTEM_ID,");
                    sql.Append("                RD.DETAIL_SYSTEM_ID DETAIL_SYSTEM_ID, ");
                    sql.Append("                RD.ROW_NO RENKEI_ROW_NUMBER");
                    sql.Append("           FROM (SELECT TKE.* FROM T_KEIRYOU_ENTRY TKE WHERE TKE.DELETE_FLG = 0) RE ");
                    sql.Append("            LEFT JOIN T_KEIRYOU_DETAIL RD ON RE.SYSTEM_ID = RD.SYSTEM_ID AND RE.SEQ = RD.SEQ  ");
                    sql.Append("           UNION ");
                    sql.Append("           SELECT ");
                    sql.Append("                " + (int)DENSHU_KBN.UKETSUKE + " RENKEI_FLG, ");
                    sql.Append("                RE.SYSTEM_ID SYSTEM_ID,");
                    sql.Append("                RD.DETAIL_SYSTEM_ID DETAIL_SYSTEM_ID, ");
                    sql.Append("                RD.ROW_NO RENKEI_ROW_NUMBER");
                    sql.Append("           FROM (SELECT TUMKE.* FROM T_UKETSUKE_MK_ENTRY TUMKE WHERE TUMKE.DELETE_FLG = 0) RE ");
                    sql.Append("            LEFT JOIN T_UKETSUKE_MK_DETAIL RD ON RE.SYSTEM_ID = RD.SYSTEM_ID AND RE.SEQ = RD.SEQ  ");
                    sql.Append("           UNION ");
                    sql.Append("           SELECT ");
                    sql.Append("                " + (int)DENSHU_KBN.UKETSUKE + " RENKEI_FLG, ");
                    sql.Append("                RE.SYSTEM_ID SYSTEM_ID,");
                    sql.Append("                RD.DETAIL_SYSTEM_ID DETAIL_SYSTEM_ID, ");
                    sql.Append("                RD.ROW_NO RENKEI_ROW_NUMBER");
                    sql.Append("           FROM (SELECT TUSKE.* FROM T_UKETSUKE_SK_ENTRY TUSKE WHERE TUSKE.DELETE_FLG = 0) RE ");
                    sql.Append("            LEFT JOIN T_UKETSUKE_SK_DETAIL RD ON RE.SYSTEM_ID = RD.SYSTEM_ID AND RE.SEQ = RD.SEQ  ");
                    sql.Append("           UNION ");
                    sql.Append("           SELECT ");
                    sql.Append("                " + (int)DENSHU_KBN.UKETSUKE + " RENKEI_FLG, ");
                    sql.Append("                RE.SYSTEM_ID SYSTEM_ID,");
                    sql.Append("                RD.DETAIL_SYSTEM_ID DETAIL_SYSTEM_ID, ");
                    sql.Append("                RD.ROW_NO RENKEI_ROW_NUMBER");
                    sql.Append("           FROM (SELECT TUSSE.* FROM T_UKETSUKE_SS_ENTRY TUSSE WHERE TUSSE.DELETE_FLG = 0) RE ");
                    sql.Append("            LEFT JOIN T_UKETSUKE_SS_DETAIL RD ON RE.SYSTEM_ID = RD.SYSTEM_ID AND RE.SEQ = RD.SEQ  ");
                    sql.Append("          ) RENKEI_TBL_D ");
                    sql.Append(" ON T_MANIFEST_ENTRY.RENKEI_SYSTEM_ID = RENKEI_TBL_D.SYSTEM_ID   ");
                    sql.Append("    AND T_MANIFEST_ENTRY.RENKEI_MEISAI_SYSTEM_ID = RENKEI_TBL_D.DETAIL_SYSTEM_ID  ");
                    sql.Append("    AND T_MANIFEST_ENTRY.RENKEI_DENSHU_KBN_CD = RENKEI_TBL_D.RENKEI_FLG  ");
                }

                // 20140605 kayo 不具合No.4523 連携番号と連携明細行項目が正しく表示されてない対応 end‏

                sql.Append(this.joinQuery);

                #endregion

                #region WHERE句

                sql.AppendFormat(" WHERE T_MANIFEST_ENTRY.DELETE_FLG = '" + DELETE_FLG + "'");

                //廃棄物区分CD
                switch (HAIKI_KBN_CD)
                {
                    case "1"://産廃（直行）
                        sql.Append(" AND T_MANIFEST_ENTRY.HAIKI_KBN_CD = 1 ");
                        break;
                    case "2"://産廃（積替）
                        sql.Append(" AND T_MANIFEST_ENTRY.HAIKI_KBN_CD = 3 ");
                        break;
                    case "3"://建廃
                        sql.Append(" AND T_MANIFEST_ENTRY.HAIKI_KBN_CD = 2 ");
                        break;
                    default:
                        break;
                }

                // 20140603 katen 不具合No.4131 start‏
                //if (KOUFU_DATE_KBN == "1")
                //{
                //    //交付年月日（開始）
                //    if (KOUFU_DATE_FROM != "")
                //    {
                //        sql.AppendFormat(" AND T_MANIFEST_ENTRY.KOUFU_DATE >= '{0}' ", KOUFU_DATE_FROM);
                //    }

                //    //交付年月日（終了）
                //    if (KOUFU_DATE_TO != "")
                //    {
                //        sql.AppendFormat(" AND T_MANIFEST_ENTRY.KOUFU_DATE <= '{0}' ", KOUFU_DATE_TO);
                //    }
                //}
                //else
                //{
                //    //交付年月日なし
                //    sql.Append(" AND T_MANIFEST_ENTRY.KOUFU_DATE IS NULL ");
                //}
                string columnName = "";
                //抽出日付区分
                switch (KOUFU_DATE_KBN)
                {
                    case "1":
                        //交付年月日
                        columnName = "T_MANIFEST_ENTRY.KOUFU_DATE";
                        break;
                    case "2":
                        //運搬終了日
                        columnName = "TMU_CONDITION.UPN_END_DATE";
                        break;
                    case "3":
                        //処分終了日
                        columnName = "T_MANIFEST_DETAIL.SBN_END_DATE";
                        break;
                    case "4":
                        //最終処分終了日
                        columnName = "T_MANIFEST_DETAIL.LAST_SBN_END_DATE";
                        break;
                }
                StringBuilder dateCondition = new StringBuilder();
                if (this.form.txtNum_HimodukeJyoukyou.Text == "1" || this.form.txtNum_HimodukeJyoukyou.Text == "3")
                {
                    //処理区分が入力済または全ての場合
                    //年月日（開始）
                    if (KOUFU_DATE_FROM != "")
                    {
                        dateCondition.AppendFormat("                    {0} >= '{1}' ", new object[] { columnName, KOUFU_DATE_FROM });
                    }

                    //年月日（終了）
                    if (KOUFU_DATE_TO != "")
                    {
                        if (!string.IsNullOrEmpty(dateCondition.ToString()))
                        {
                            dateCondition.Append(" AND ");
                        }
                        dateCondition.AppendFormat("                    {0} <= '{1}' ", new object[] { columnName, KOUFU_DATE_TO });
                    }

                    // 20140610 katen 不具合No.4714 start‏
                    if (string.IsNullOrEmpty(dateCondition.ToString()) && this.form.txtNum_HimodukeJyoukyou.Text == "1")
                    {
                        //処理区分が入力済、そして年月日（開始）と年月日（終了）が入力しなかった場合
                        dateCondition.AppendFormat(" {0} IS NOT NULL", columnName);
                    }
                    // 20140610 katen 不具合No.4714 end‏
                }
                if (!string.IsNullOrEmpty(dateCondition.ToString()) && this.form.txtNum_HimodukeJyoukyou.Text == "3")
                {
                    //処理区分が全ての場合
                    dateCondition.Append(" OR ");
                }
                if (this.form.txtNum_HimodukeJyoukyou.Text == "2" ||
                   (this.form.txtNum_HimodukeJyoukyou.Text == "3" && !string.IsNullOrEmpty(dateCondition.ToString())))
                {
                    //処理区分が未入力または全ての場合
                    dateCondition.AppendFormat(" {0} IS NULL", columnName);
                }

                if (!string.IsNullOrEmpty(dateCondition.ToString()))
                {
                    sql.AppendFormat(" AND ( {0} ) ", dateCondition.ToString());
                }

                //取引先
                if (!string.IsNullOrEmpty(this.form.cantxt_TorihikiCd.Text))
                {
                    sql.AppendFormat(" AND T_MANIFEST_ENTRY.TORIHIKISAKI_CD = '{0}' ", this.form.cantxt_TorihikiCd.Text);
                }

                //排出事業者
                if (!string.IsNullOrEmpty(this.form.cantxt_HaisyutuGyousyaCd.Text))
                {
                    sql.AppendFormat(" AND T_MANIFEST_ENTRY.HST_GYOUSHA_CD = '{0}' ", this.form.cantxt_HaisyutuGyousyaCd.Text);
                }

                //排出事業場
                if (!string.IsNullOrEmpty(this.form.cantxt_HaisyutuJigyoubaName.Text))
                {
                    sql.AppendFormat(" AND T_MANIFEST_ENTRY.HST_GENBA_CD = '{0}' ", this.form.cantxt_HaisyutuJigyoubaName.Text);
                }

                //運搬受託者
                if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyutakuNameCd.Text))
                {
                    sql.AppendFormat(" AND TMU_CONDITION.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                }

                //処分受託者
                if (!string.IsNullOrEmpty(this.form.cantxt_SyobunJyutakuNameCd.Text))
                {
                    sql.AppendFormat(" AND T_MANIFEST_ENTRY.SBN_GYOUSHA_CD = '{0}' ", this.form.cantxt_SyobunJyutakuNameCd.Text);
                }

                //処分事業場
                if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyugyobaNameCd.Text))
                {
                    sql.AppendFormat(" AND TMU_CONDITION.UPN_SAKI_GENBA_CD = '{0}' ", this.form.cantxt_UnpanJyugyobaNameCd.Text);
                    //産廃（積替）
                    if (HAIKI_KBN_CD == "2")
                    {
                        sql.AppendFormat(" AND TMU_CONDITION.UPN_SAKI_KBN = 1 ");
                    }
                }

                //積替え保管業者
                string tsumikaehokanGyoushaCd = this.form.cantxt_TsumikaehokanGyoushaCd.Text;
                string tsumikaehokanGyoubaCd = this.form.cantxt_TsumikaehokanGyoubaCd.Text;
                if (!string.IsNullOrEmpty(tsumikaehokanGyoushaCd))
                {
                    //廃棄物区分CD
                    switch (HAIKI_KBN_CD)
                    {
                        case "1"://産廃（直行）
                            sql.AppendFormat(" AND T_MANIFEST_ENTRY.TMH_GYOUSHA_CD = '{0}' ", tsumikaehokanGyoushaCd);
                            break;
                        case "3"://建廃
                            sql.AppendFormat(" AND T_MANIFEST_ENTRY.TMH_GYOUSHA_CD = '{0}' ", tsumikaehokanGyoushaCd);
                            break;
                        default:
                            break;
                    }
                }

                //積替え保管場
                if (!string.IsNullOrEmpty(tsumikaehokanGyoubaCd))
                {
                    //廃棄物区分CD
                    switch (HAIKI_KBN_CD)
                    {
                        case "1"://産廃（直行）
                            sql.AppendFormat(" AND T_MANIFEST_ENTRY.TMH_GENBA_CD = '{0}' ", tsumikaehokanGyoubaCd);
                            break;
                        case "3"://建廃
                            sql.AppendFormat(" AND T_MANIFEST_ENTRY.TMH_GENBA_CD = '{0}' ", tsumikaehokanGyoubaCd);
                            break;
                        default:
                            break;
                    }
                }

                //産廃（積替）
                if (HAIKI_KBN_CD == "2")
                {
                    if (!string.IsNullOrEmpty(tsumikaehokanGyoushaCd) || !string.IsNullOrEmpty(tsumikaehokanGyoubaCd))
                    {
                        sql.AppendFormat(" AND ( ");
                        sql.Append("( TMU_CONDITION.UPN_SAKI_KBN = 2 ");
                        sql.AppendFormat(" AND ");
                        if (!string.IsNullOrEmpty(tsumikaehokanGyoushaCd))
                        {
                            sql.AppendFormat(" TMU_CONDITION.UPN_SAKI_GYOUSHA_CD = '{0}' ", tsumikaehokanGyoushaCd);
                        }
                        if (!string.IsNullOrEmpty(tsumikaehokanGyoubaCd))
                        {
                            sql.AppendFormat(" AND TMU_CONDITION.UPN_SAKI_GENBA_CD = '{0}' ", tsumikaehokanGyoubaCd);
                        }
                        sql.AppendFormat(" ) ");
                        sql.AppendFormat(" OR ( ");
                        if (!string.IsNullOrEmpty(tsumikaehokanGyoushaCd))
                        {
                            sql.AppendFormat(" T_MANIFEST_ENTRY.TMH_GYOUSHA_CD = '{0}' ", tsumikaehokanGyoushaCd);
                        }
                        if (!string.IsNullOrEmpty(tsumikaehokanGyoubaCd))
                        {
                            sql.AppendFormat(" AND T_MANIFEST_ENTRY.TMH_GENBA_CD = '{0}' ", tsumikaehokanGyoubaCd);
                        }
                        sql.AppendFormat(" ) ");
                        sql.AppendFormat(" ) ");
                    }
                }

                //報告書分類
                if (!string.IsNullOrEmpty(this.form.cantxt_HokokushoBunrui.Text))
                {
                    sql.AppendFormat(" AND M_HAIKI_SHURUI.HOUKOKUSHO_BUNRUI_CD = '{0}' ", this.form.cantxt_HokokushoBunrui.Text);
                }

                //廃棄物種類
                if (!string.IsNullOrEmpty(this.form.cantxt_HaikibutuShurui.Text))
                {
                    sql.AppendFormat(" AND T_MANIFEST_DETAIL.HAIKI_SHURUI_CD = '{0}' ", this.form.cantxt_HaikibutuShurui.Text);
                }

                //廃棄物名称
                if (!string.IsNullOrEmpty(this.form.cantxt_HaikibutuName.Text))
                {
                    sql.AppendFormat(" AND T_MANIFEST_DETAIL.HAIKI_NAME_CD = '{0}' ", this.form.cantxt_HaikibutuName.Text);
                }

                //マニフェスト一次区分
                sql.AppendFormat(" AND T_MANIFEST_ENTRY.FIRST_MANIFEST_KBN = {0} ", (this.maniFlag == 1 ? "0" : "1"));
                // 20140603 katen 不具合No.4131 end‏
                // システムID
                if (SYSTEM_ID != string.Empty)
                {
                    sql.AppendFormat(" AND T_MANIFEST_ENTRY.SYSTEM_ID = {0} ", SYSTEM_ID);
                }

                // 枝番
                if (string.IsNullOrEmpty(SEQ))
                {
                    sql.Append(" AND T_MANIFEST_ENTRY.SEQ = ( SELECT MAX(SEQ) ");
                    sql.Append(" FROM T_MANIFEST_ENTRY E2 WITH(NOLOCK) ");
                    sql.Append(" WHERE T_MANIFEST_ENTRY.SYSTEM_ID = E2.SYSTEM_ID) ");
                }
                else
                {
                    sql.AppendFormat(" AND T_MANIFEST_ENTRY.SEQ = {0} ", SEQ);
                }

                // 拠点CD
                if (!string.IsNullOrEmpty(this.header.KYOTEN_CD.Text) && this.header.KYOTEN_CD.Text != "99")
                {
                    sql.AppendFormat(" AND T_MANIFEST_ENTRY.KYOTEN_CD = {0} ", Int32.Parse(this.header.KYOTEN_CD.Text));
                }

                //マニフェスト／交付番号（開始）
                string Manifesutobangou_From = this.form.KOUFUBANNGOFrom.Text.ToString();
                if (!string.IsNullOrEmpty(Manifesutobangou_From))
                {
                    sql.Append(" AND RIGHT('00000000000' + ISNULL(T_MANIFEST_ENTRY.MANIFEST_ID,''), 11) >= '" + Manifesutobangou_From.PadLeft(11, '0') + "'");
                }
                //マニフェスト／交付番号（終了）
                string Manifesutobangou_To = this.form.KOUFUBANNGOTo.Text.ToString();
                if (!string.IsNullOrEmpty(Manifesutobangou_To))
                {
                    sql.Append(" AND RIGHT('00000000000' + ISNULL(T_MANIFEST_ENTRY.MANIFEST_ID,''), 11) <= '" + Manifesutobangou_To.PadLeft(11, '0') + "'");
                }

                #endregion

                #region ORDER BY句

                if (!string.IsNullOrEmpty(this.orderByQuery))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(this.orderByQuery);
                }

                #endregion

                this.createSql = sql.ToString();
                sql.Append("");

                if (string.IsNullOrEmpty(SYSTEM_ID))
                {
                    this.Search_TME = dao_GetTME.getdateforstringsql(this.createSql);
                    if (HAIKI_KBN_CD == "1" || HAIKI_KBN_CD == "3")
                    {
                        this.Search_TME = this.CorrectDetails(this.Search_TME);
                    }
                    count = this.Search_TME.Rows.Count;
                }
                else
                {
                    this.Search_TME_Check = dao_GetTME.getdateforstringsql(this.createSql);
                    if (HAIKI_KBN_CD == "1" || HAIKI_KBN_CD == "3")
                    {
                        this.Search_TME_Check = this.CorrectDetails(this.Search_TME_Check);
                    }
                    count = this.Search_TME_Check.Rows.Count;
                }

                return count;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }
        }

        /// <summary>
        /// データ取得（電マニ）
        /// </summary>
        /// <param name="KOUFU_DATE_FROM"></param>
        /// <param name="KOUFU_DATE_TO"></param>
        /// <param name="HAIKI_KBN_CD"></param>
        /// <param name="DELETE_FLG"></param>
        /// <param name="SYSTEM_ID"></param>
        /// <param name="SEQ"></param>
        /// <param name="KOUFU_DATE_KBN"></param>
        /// <param name="KANRI_ID"></param>
        /// <param name="LATEST_SEQ"></param>
        /// <returns></returns>
        private int Get_Search_DenshiMani(String KOUFU_DATE_FROM, String KOUFU_DATE_TO, String HAIKI_KBN_CD, String DELETE_FLG,
                                    String SYSTEM_ID, String SEQ, String KOUFU_DATE_KBN,
                                    String KANRI_ID, String LATEST_SEQ)
        {
            LogUtility.DebugMethodStart(KOUFU_DATE_FROM, KOUFU_DATE_TO, HAIKI_KBN_CD, DELETE_FLG, SYSTEM_ID, SEQ, KOUFU_DATE_KBN, KANRI_ID, LATEST_SEQ);

            int count = 0;

            try
            {
                var sql = new StringBuilder();

                #region SELECT句

                sql.Append("SELECT DISTINCT ");
                sql.Append(this.selectQuery);

                sql.AppendFormat(", DT_R18_EX.SYSTEM_ID AS {0} ", this.HIDDEN_SYSTEM_ID);
                sql.AppendFormat(", DT_R18_EX.SEQ AS {0} ", this.HIDDEN_SEQ);
                sql.AppendFormat(", DT_MF_TOC.LATEST_SEQ AS {0} ", this.HIDDEN_LATEST_SEQ);
                sql.AppendFormat(", DT_MF_TOC.KANRI_ID AS {0} ", this.HIDDEN_KANRI_ID);
                sql.AppendFormat(", DT_MF_TOC.STATUS_FLAG AS {0} ", this.HIDDEN_TOC_STATUS_FLAG);
                sql.AppendFormat(", QUE2.STATUS_FLAG AS {0} ", this.HIDDEN_QUE_STATUS_FLAG);
                sql.AppendFormat(", 4 AS {0} ", this.HIDDEN_HAIKI_KBN);
                sql.AppendFormat(", DT_R18_EX.SYSTEM_ID AS {0} ", this.HIDDEN_DETAIL_SYSTEM_ID);
                sql.AppendFormat(", DT_R18_EX.HST_GYOUSHA_CD AS {0} ", this.HIDDEN_HST_GYOUSHA_CD);

                string errorDefault = "\'0\'";
                sql.AppendFormat(" ,{0} AS HST_GYOUSHA_CD_ERROR", errorDefault);
                sql.AppendFormat(" ,{0} AS HST_GENBA_CD_ERROR", errorDefault);
                sql.AppendFormat(" ,{0} AS HAIKI_SHURUI_CD_ERROR", errorDefault);

                #endregion

                #region FROM句

                // マニ目次
                sql.Append(" FROM DT_MF_TOC ");

                // マニ情報
                sql.Append(" INNER JOIN DT_R18 ");
                sql.Append(" ON DT_MF_TOC.KANRI_ID = DT_R18.KANRI_ID AND DT_MF_TOC.LATEST_SEQ = DT_R18.SEQ ");

                // マニ情報拡張
                //sql.Append(" LEFT JOIN DT_R18_EX ");
                sql.Append("LEFT JOIN  ");
                sql.Append("( ");
                sql.Append("SELECT ");
                sql.Append("  R18EX.SYSTEM_ID ");
                sql.Append(" ,R18EX.SEQ ");
                sql.Append(" ,R18EX.KANRI_ID ");
                sql.Append(" ,R18EX.MANIFEST_ID ");
                sql.Append(" ,R18EX.HST_GYOUSHA_CD ");
                sql.Append(" ,R18EX.HST_GENBA_CD ");
                sql.Append(" ,R18EX.SBN_GYOUSHA_CD ");
                sql.Append(" ,R18EX.SBN_GENBA_CD ");
                sql.Append(" ,R18EX.NO_REP_SBN_EDI_MEMBER_ID ");
                sql.Append(" ,R18EX.SBN_HOUHOU_CD ");
                sql.Append(" ,R18EX.HOUKOKU_TANTOUSHA_CD ");
                sql.Append(" ,R18EX.SBN_TANTOUSHA_CD ");
                sql.Append(" ,R18EX.UPN_TANTOUSHA_CD ");
                sql.Append(" ,R18EX.SHARYOU_CD ");
                sql.Append(" ,R18EX.KANSAN_SUU ");
                sql.Append(" ,CREATE_DATA.CREATE_USER ");
                sql.Append(" ,R18EX.CREATE_DATE ");
                sql.Append(" ,CREATE_DATA.CREATE_PC ");
                sql.Append(" ,R18EX.UPDATE_USER ");
                sql.Append(" ,R18EX.UPDATE_DATE ");
                sql.Append(" ,R18EX.UPDATE_PC ");
                sql.Append(" ,R18EX.DELETE_FLG ");
                sql.Append(" ,R18EX.TIME_STAMP ");
                sql.Append(" ,R18EX.HAIKI_NAME_CD ");
                sql.Append(" ,R18EX.GENNYOU_SUU ");
                sql.Append("FROM  ");
                sql.Append("  DT_R18_EX R18EX ");
                sql.Append(" ,( ");
                sql.Append("   SELECT ");
                sql.Append("    R18EX.SYSTEM_ID ");
                sql.Append("   ,R18EX.SEQ ");
                sql.Append("   ,R18EX.KANRI_ID ");
                sql.Append("   ,R18EX.CREATE_USER ");
                sql.Append("   ,R18EX.CREATE_DATE ");
                sql.Append("   ,R18EX.CREATE_PC ");
                sql.Append("  FROM ");
                sql.Append("   DT_R18_EX R18EX ");
                sql.Append("   ,(SELECT ");
                sql.Append("      SYSTEM_ID ");
                sql.Append("     ,MIN(SEQ) MIN_SEQ ");
                sql.Append("     FROM ");
                sql.Append("      DT_R18_EX ");
                sql.Append("     GROUP BY  ");
                sql.Append("      SYSTEM_ID ");
                sql.Append("     ) SEQ_DATA ");
                sql.Append("  WHERE ");
                sql.Append("       R18EX.SYSTEM_ID = seq_data.SYSTEM_ID ");
                sql.Append("   AND R18EX.SEQ = SEQ_DATA.MIN_SEQ ");
                sql.Append("   ) CREATE_DATA ");
                sql.Append("WHERE ");
                sql.Append(" R18EX.SYSTEM_ID = CREATE_DATA.SYSTEM_ID ");
                sql.Append(") DT_R18_EX ");

                sql.Append(" ON DT_R18.KANRI_ID = DT_R18_EX.KANRI_ID ");
                sql.Append(" AND DT_R18_EX.SYSTEM_ID = ( SELECT MAX(SYSTEM_ID) FROM DT_R18_EX TMP WHERE DT_R18.KANRI_ID = TMP.KANRI_ID ) ");
                sql.Append(" AND DT_R18_EX.SEQ = ( SELECT MAX(SEQ) FROM DT_R18_EX TMP WHERE TMP.SYSTEM_ID = ( SELECT MAX(SYSTEM_ID) FROM DT_R18_EX TMP WHERE DT_R18.KANRI_ID = TMP.KANRI_ID )  ) ");

                // 1次マニ情報
                sql.Append(" LEFT JOIN DT_R08 ");
                sql.Append(" ON DT_MF_TOC.KANRI_ID = DT_R08.KANRI_ID AND DT_MF_TOC.LATEST_SEQ = DT_R08.SEQ ");

                // 1次マニ情報拡張
                sql.Append(" LEFT JOIN DT_R08_EX ");
                sql.Append(" ON DT_R18_EX.SYSTEM_ID = DT_R08_EX.SYSTEM_ID AND DT_R18_EX.SEQ = DT_R08_EX.SEQ");
                sql.Append(" AND DT_R18_EX.KANRI_ID = DT_R08_EX.KANRI_ID AND DT_R08.REC_SEQ = DT_R08_EX.REC_SEQ ");

                // マニ運搬情報
                sql.Append(" LEFT JOIN DT_R19 ");
                sql.Append(" ON DT_MF_TOC.KANRI_ID = DT_R19.KANRI_ID AND DT_MF_TOC.LATEST_SEQ = DT_R19.SEQ ");

                //マニ運搬情報（自社排出・自社運搬用の終了日取得用）
                            //運搬区間に、直近の他社区間情報付与start
                sql.Append(" LEFT JOIN (");
                sql.Append("    SELECT R19M.KANRI_ID, R19M.SEQ, R19M.UPN_ROUTE_NO, MAX(Other_KUKAN.UPN_ROUTE_NO) AS 表示日付区間");
                sql.Append("    FROM DT_MF_TOC INNER JOIN DT_R19 AS R19M ON DT_MF_TOC.KANRI_ID = R19M.KANRI_ID AND DT_MF_TOC.LATEST_SEQ = R19M.SEQ");
                sql.Append("    LEFT JOIN (");
                                    //R18(交付情報)とR19(他社区間データ)の取得start
                sql.Append("        SELECT R18.KANRI_ID, R18.SEQ, UPN_ROUTE_NO ");
                sql.Append("            FROM DT_MF_TOC toc");
                sql.Append("            LEFT JOIN DT_R18 R18 on toc.KANRI_ID = R18.KANRI_ID AND toc.LATEST_SEQ = R18.SEQ");
                sql.Append("            LEFT JOIN DT_R19 R19 on R18.KANRI_ID = R19.KANRI_ID AND R18.SEQ = R19.SEQ AND R18.HST_SHA_EDI_MEMBER_ID != R19.UPN_SHA_EDI_MEMBER_ID");
                sql.Append("        UNION SELECT R18.KANRI_ID, R18.SEQ, 0 AS UPN_ROUTE_NO ");
                sql.Append("            FROM DT_MF_TOC toc");
                sql.Append("            LEFT JOIN DT_R18 R18 on toc.KANRI_ID = R18.KANRI_ID AND toc.LATEST_SEQ = R18.SEQ");
                sql.Append("    ) AS Other_KUKAN");
                sql.Append("    ON R19M.KANRI_ID = Other_KUKAN.KANRI_ID AND R19M.SEQ = Other_KUKAN.SEQ AND R19M.UPN_ROUTE_NO >= Other_KUKAN.UPN_ROUTE_NO");
                sql.Append("    GROUP BY R19M.KANRI_ID, R19M.SEQ, R19M.UPN_ROUTE_NO");
                sql.Append(") Other_KUKAN_F");
                sql.Append(" ON DT_R19.KANRI_ID = Other_KUKAN_F.KANRI_ID AND DT_R19.SEQ = Other_KUKAN_F.SEQ AND DT_R19.UPN_ROUTE_NO = Other_KUKAN_F.UPN_ROUTE_NO");
                            //付与された直近の他社区間の運搬終了日（または、交付年月日）を取得
                sql.Append(" LEFT JOIN (");
                sql.Append("    SELECT R19U.KANRI_ID, R19U.SEQ, R19U.UPN_ROUTE_NO, R19U.UPN_END_DATE");
                sql.Append("           FROM DT_MF_TOC toc INNER JOIN DT_R19 R19U ON toc.KANRI_ID = R19U.KANRI_ID AND toc.LATEST_SEQ = R19U.SEQ");
                sql.Append("    UNION SELECT R18U.KANRI_ID, R18U.SEQ, 0 AS UPN_ROUTE_NO, R18U.HIKIWATASHI_DATE AS UPN_END_DATE");
                sql.Append("           FROM DT_MF_TOC toc INNER JOIN DT_R18 R18U ON toc.KANRI_ID = R18U.KANRI_ID AND toc.LATEST_SEQ = R18U.SEQ");
                sql.Append("    ) ROUTE_DATA ON Other_KUKAN_F.KANRI_ID = ROUTE_DATA.KANRI_ID and Other_KUKAN_F.SEQ = ROUTE_DATA.SEQ and Other_KUKAN_F.表示日付区間 = ROUTE_DATA.UPN_ROUTE_NO");

                // マニ運搬情報拡張
                sql.Append(" LEFT JOIN DT_R19_EX ");
                sql.Append(" ON DT_R18_EX.SYSTEM_ID = DT_R19_EX.SYSTEM_ID AND DT_R18_EX.SEQ = DT_R19_EX.SEQ");
                sql.Append(" AND DT_R18_EX.KANRI_ID = DT_R19_EX.KANRI_ID AND DT_R19.UPN_ROUTE_NO = DT_R19_EX.UPN_ROUTE_NO ");

                // マニ運搬情報（最終区間）
                sql.Append(" LEFT JOIN DT_R19 DT_R19_LAST");
                sql.Append(" ON DT_MF_TOC.KANRI_ID = DT_R19_LAST.KANRI_ID AND DT_MF_TOC.LATEST_SEQ = DT_R19_LAST.SEQ ");
                sql.Append(" AND DT_R19_LAST.UPN_ROUTE_NO = (SELECT MAX(UPN_ROUTE_NO) FROM DT_R19 R19 WHERE DT_MF_TOC.KANRI_ID = R19.KANRI_ID AND DT_MF_TOC.LATEST_SEQ = R19.SEQ) ");

                //他社区間マニ運搬情報（運搬終了日のFROMTO抽出用。他社最終区間/DT_R19_LAST_DIF）
                sql.Append(" LEFT JOIN (");
                sql.Append(" SELECT DT_R19.KANRI_ID, DT_R19.SEQ, MAX(DT_R19.UPN_ROUTE_NO) RNO");
                sql.Append(" FROM DT_MF_TOC");
                sql.Append(" INNER JOIN DT_R18 ON DT_MF_TOC.KANRI_ID = DT_R18.KANRI_ID AND DT_MF_TOC.LATEST_SEQ = DT_R18.SEQ");
                sql.Append(" INNER JOIN DT_R19 ON DT_MF_TOC.KANRI_ID = DT_R19.KANRI_ID AND DT_MF_TOC.LATEST_SEQ = DT_R19.SEQ");
                sql.Append(" WHERE DT_R18.HST_SHA_EDI_MEMBER_ID != DT_R19.UPN_SHA_EDI_MEMBER_ID");
                sql.Append(" GROUP BY DT_R19.KANRI_ID, DT_R19.SEQ) DT_R19_LAST_d");
                sql.Append(" ON DT_R19_LAST_d.KANRI_ID = DT_MF_TOC.KANRI_ID AND DT_R19_LAST_d.SEQ = DT_MF_TOC.LATEST_SEQ");
                sql.Append(" LEFT JOIN DT_R19 DT_R19_LAST_DIF ON DT_R19_LAST_DIF.KANRI_ID = DT_R19_LAST_d.KANRI_ID AND DT_R19_LAST_DIF.SEQ = DT_R19_LAST_d.SEQ AND DT_R19_LAST_DIF.UPN_ROUTE_NO = DT_R19_LAST_d.RNO");

                // 20140610 katen 不具合No.4131 start‏
                // 電子廃棄物種類
                sql.Append(" LEFT OUTER JOIN M_DENSHI_HAIKI_SHURUI WITH (NOLOCK) ");
                sql.Append(" ON DT_R18.HAIKI_DAI_CODE + DT_R18.HAIKI_CHU_CODE + DT_R18.HAIKI_SHO_CODE = M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_CD ");
                // 報告書分類
                sql.Append(" LEFT OUTER JOIN M_HOUKOKUSHO_BUNRUI WITH (NOLOCK) ");
                sql.Append(" ON M_HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_CD = M_DENSHI_HAIKI_SHURUI.HOUKOKUSHO_BUNRUI_CD ");

                // 20140610 katen 不具合No.4131 end

                // 最終処分場（予定）情報
                sql.Append(" LEFT JOIN DT_R04 ");
                sql.Append(" ON DT_MF_TOC.KANRI_ID = DT_R04.KANRI_ID AND DT_MF_TOC.LATEST_SEQ = DT_R04.SEQ ");

                // 最終処分場（予定）情報拡張
                sql.Append(" LEFT JOIN DT_R04_EX ");
                sql.Append(" ON DT_R18_EX.SYSTEM_ID = DT_R04_EX.SYSTEM_ID AND DT_R18_EX.SEQ = DT_R04_EX.SEQ");
                sql.Append(" AND DT_R18_EX.KANRI_ID = DT_R04_EX.KANRI_ID AND DT_R04.REC_SEQ = DT_R04_EX.REC_SEQ ");

                // 最終処分場（実績）情報
                sql.Append(" LEFT JOIN DT_R13 ");
                sql.Append(" ON DT_MF_TOC.KANRI_ID = DT_R13.KANRI_ID AND DT_MF_TOC.LATEST_SEQ = DT_R13.SEQ ");

                // 最終処分場（実績）情報拡張
                sql.Append(" LEFT JOIN DT_R13_EX ");
                sql.Append(" ON DT_R18_EX.SYSTEM_ID = DT_R13_EX.SYSTEM_ID AND DT_R18_EX.SEQ = DT_R13_EX.SEQ");
                sql.Append(" AND DT_R18_EX.KANRI_ID = DT_R13_EX.KANRI_ID AND DT_R13.REC_SEQ = DT_R13_EX.REC_SEQ ");

                // 備考情報
                sql.Append(" LEFT JOIN DT_R06 ");
                sql.Append(" ON DT_MF_TOC.KANRI_ID = DT_R06.KANRI_ID AND DT_MF_TOC.LATEST_SEQ = DT_R06.SEQ ");

                // 排出事業者情報
                sql.Append(" LEFT JOIN M_GYOUSHA AS HST_GYOUSHA ");
                sql.Append(" ON DT_R18_EX.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD ");

                //QUE_INFO
                //FUNCTION_ID(0101,0102,0501,0502)新規登録のデータの内、最終QUE_SEQのFUNCTION_IDの取得
                //基本的に、上書きで使用しているはずなので問題ないと思うが念のため最終QUE_SEQ情報を取得する
                sql.Append(" LEFT JOIN ");
                sql.Append(" (SELECT KANRI_ID, MAX(QUE_SEQ) QUE_SEQ FROM QUE_INFO ");
                sql.Append(" WHERE FUNCTION_ID IN ('0101','0102','0501','0502')");
                sql.Append(" GROUP BY KANRI_ID) QUE ON DT_MF_TOC.KANRI_ID = QUE.KANRI_ID ");
                sql.Append(" LEFT JOIN ");
                sql.Append(" (SELECT KANRI_ID, QUE_SEQ, STATUS_FLAG FROM QUE_INFO) QUE2 ON QUE.KANRI_ID = QUE2.KANRI_ID AND QUE.QUE_SEQ = QUE2.QUE_SEQ");


                sql.Append(this.joinQuery);

                #endregion

                #region WHERE句

                sql.Append(" WHERE (DT_MF_TOC.KIND = 4 or DT_MF_TOC.KIND = 5 or DT_MF_TOC.KIND IS NULL) ");

                //STATUS_FLAG
                //1:予約未送信→一覧上は不要、報告時に失敗している(QUE：STATUS_FLAG = 8 or 9)可能性もあるデータ（長時間の放置データはJWNET送信が失敗している）
                //2:マニ未送信→一覧上は不要、報告時に失敗している(QUE：STATUS_FLAG = 8 or 9)可能性もあるデータ（長時間の放置データはJWNET送信が失敗している）
                //  1,2：新規登録の最終QUE_SEQの情報が保留削除なら、抽出対象外とする
                //  STATUS_FLAG [0、1]:送信中　[2]:送信完了　[6]:保留削除　[7]:保留　[8、9]:エラー
                //3:予約→必要
                //4:マニ→必要
                //9:取消済→不要
                sql.Append(" AND ( ");
                sql.Append("((DT_MF_TOC.STATUS_FLAG = 1 OR DT_MF_TOC.STATUS_FLAG = 2) AND QUE2.STATUS_FLAG <> 6 ) ");
                sql.Append(" OR (DT_MF_TOC.STATUS_FLAG = 3 OR DT_MF_TOC.STATUS_FLAG = 4) ");
                sql.Append(") ");


                if (SYSTEM_ID != "")
                {
                    sql.Append(" AND DT_MF_TOC.KANRI_ID IN ( ");
                    sql.Append("              SELECT DT_R18_EX.KANRI_ID ");
                    sql.Append("                FROM DT_R18_EX ");
                    sql.Append("               WHERE DT_R18_EX.SYSTEM_ID =" + SYSTEM_ID);
                    sql.Append("                 AND DT_R18_EX.SEQ =" + SEQ);
                    sql.Append("          ) ");
                }

                if (KANRI_ID != "")
                {
                    sql.Append("      AND DT_MF_TOC.KANRI_ID ='" + KANRI_ID + "'");
                }

                if (LATEST_SEQ != "")
                {
                    sql.Append("      AND DT_MF_TOC.LATEST_SEQ =" + LATEST_SEQ);
                }

                // 20140603 katen 不具合No.4131 start‏
                ////2013.11.23 naitou update 交付年月日区分の追加 start
                ////交付年月日あり
                //if (KOUFU_DATE_KBN == "1")
                //{
                //    //交付年月日（開始）
                //    if (KOUFU_DATE_FROM != "")
                //    {
                //        sql.Append("      AND DT_R18.HIKIWATASHI_DATE >= CONVERT(NVARCHAR(8),CONVERT(DATETIME,'" + KOUFU_DATE_FROM + "'),112) ");
                //    }
                //    //交付年月日（終了）
                //    if (KOUFU_DATE_TO != "")
                //    {
                //        sql.Append("      AND DT_R18.HIKIWATASHI_DATE <= CONVERT(NVARCHAR(8),CONVERT(DATETIME,'" + KOUFU_DATE_TO + "'),112) ");
                //    }
                //}
                ////交付年月日なし
                //else
                //{
                //    sql.Append("      AND (DT_R18.HIKIWATASHI_DATE IS NULL OR DT_R18.HIKIWATASHI_DATE = '') ");
                //}
                string columnName = "";
                //抽出日付区分
                switch (KOUFU_DATE_KBN)
                {
                    case "1":
                        //交付年月日
                        columnName = "CASE WHEN ISDATE(DT_R18.HIKIWATASHI_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, DT_R18.HIKIWATASHI_DATE) END";
                        break;
                    case "2":
                        //運搬終了日
                        //以下の日付で検索を行う
                        //予約マニ⇒NULL(終了日無し)。他社運搬が無⇒交付年月日。最終運搬が他社⇒最終区間の運搬終了日。最終運搬が自社⇒最終他社運搬区間の運搬終了日
                        columnName = "CASE WHEN DT_MF_TOC.STATUS_FLAG != 4 THEN "
                                   + "          NULL "
                                   + "     WHEN DT_R19_LAST_DIF.KANRI_ID IS NULL THEN "
                                   + "          CASE WHEN ISDATE(DT_R18.HIKIWATASHI_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, DT_R18.HIKIWATASHI_DATE) END "
                                   + "     WHEN DT_R19_LAST.UPN_ROUTE_NO = DT_R19_LAST_DIF.UPN_ROUTE_NO THEN "
                                   + "          CASE WHEN ISDATE(DT_R19_LAST.UPN_END_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, DT_R19_LAST.UPN_END_DATE) END "
                                   + "     ELSE "
                                   + "          CASE WHEN ISDATE(DT_R19_LAST_DIF.UPN_END_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, DT_R19_LAST_DIF.UPN_END_DATE) END END ";
                        break;
                    case "3":
                        //処分終了日
                        columnName = "CASE WHEN ISDATE(DT_R18.SBN_END_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, DT_R18.SBN_END_DATE) END";
                        break;
                    case "4":
                        //最終処分終了日
                        columnName = "CASE WHEN ISDATE(DT_R18.LAST_SBN_END_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, DT_R18.LAST_SBN_END_DATE) END";
                        break;
                }
                StringBuilder dateCondition = new StringBuilder();
                if (this.form.txtNum_HimodukeJyoukyou.Text == "1" || this.form.txtNum_HimodukeJyoukyou.Text == "3")
                {
                    //処理区分が入力済または全ての場合
                    //年月日（開始）
                    if (KOUFU_DATE_FROM != "")
                    {
                        dateCondition.AppendFormat("                    {0} >= '{1}' ", new object[] { columnName, KOUFU_DATE_FROM });
                    }

                    //年月日（終了）
                    if (KOUFU_DATE_TO != "")
                    {
                        if (!string.IsNullOrEmpty(dateCondition.ToString()))
                        {
                            dateCondition.Append(" AND ");
                        }
                        dateCondition.AppendFormat("                    {0} <= '{1}' ", new object[] { columnName, KOUFU_DATE_TO });
                    }

                    // 20140610 katen 不具合No.4714 start‏
                    if (string.IsNullOrEmpty(dateCondition.ToString()) && this.form.txtNum_HimodukeJyoukyou.Text == "1")
                    {
                        //処理区分が入力済、そして年月日（開始）と年月日（終了）が入力しなかった場合
                        dateCondition.AppendFormat(" {0} IS NOT NULL", columnName);
                    }
                    // 20140610 katen 不具合No.4714 end‏
                }
                if (!string.IsNullOrEmpty(dateCondition.ToString()) && this.form.txtNum_HimodukeJyoukyou.Text == "3")
                {
                    //処理区分が全ての場合
                    dateCondition.Append(" OR ");
                }
                if (this.form.txtNum_HimodukeJyoukyou.Text == "2" ||
                   (this.form.txtNum_HimodukeJyoukyou.Text == "3" && !string.IsNullOrEmpty(dateCondition.ToString())))
                {
                    //処理区分が未入力または全ての場合
                    dateCondition.AppendFormat(" {0} IS NULL", columnName);
                }
                if (!string.IsNullOrEmpty(dateCondition.ToString()))
                {
                    sql.AppendFormat(" AND ( {0} ) ", dateCondition.ToString());
                }

                // 処理区分項目が「未入力」、かつ年月日項目が「処分終了日」か「最終処分終了日」の場合は、
                // 処分受託者が報告不要業者(EDI_MEMBER_ID = 0000000)の電マニ表示は行わない
                if (this.form.txtNum_HimodukeJyoukyou.Text == "2")
                {
                    if (KOUFU_DATE_KBN == "3" || KOUFU_DATE_KBN == "4")
                    {
                        sql.AppendFormat(" AND DT_R18.SBN_SHA_MEMBER_ID <> '0000000'");
                    }
                }

                //排出事業者
                if (!string.IsNullOrEmpty(this.form.cantxt_HaisyutuGyousyaCd.Text))
                {
                    sql.AppendFormat(" AND DT_R18_EX.HST_GYOUSHA_CD = '{0}' ", this.form.cantxt_HaisyutuGyousyaCd.Text);
                }

                //排出事業場
                if (!string.IsNullOrEmpty(this.form.cantxt_HaisyutuJigyoubaName.Text))
                {
                    sql.AppendFormat(" AND DT_R18_EX.HST_GENBA_CD = '{0}' ", this.form.cantxt_HaisyutuJigyoubaName.Text);
                }

                //運搬受託者
                if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyutakuNameCd.Text))
                {
                    sql.AppendFormat(" AND DT_R19_EX.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                }

                //処分受託者
                if (!string.IsNullOrEmpty(this.form.cantxt_SyobunJyutakuNameCd.Text))
                {
                    sql.AppendFormat(" AND DT_R18_EX.SBN_GYOUSHA_CD = '{0}' ", this.form.cantxt_SyobunJyutakuNameCd.Text);
                }

                //処分事業場
                if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyugyobaNameCd.Text))
                {
                    sql.AppendFormat(" AND DT_R18_EX.SBN_GENBA_CD = '{0}' ", this.form.cantxt_UnpanJyugyobaNameCd.Text);
                }

                //積替え保管業者
                bool tumikaehokanflg = false;
                string tsumikaehokanGyoushaCd = this.form.cantxt_TsumikaehokanGyoushaCd.Text;
                if (!string.IsNullOrEmpty(tsumikaehokanGyoushaCd))
                {
                    tumikaehokanflg = true;
                    sql.AppendFormat(" AND DT_R19_EX.UPNSAKI_GYOUSHA_CD = '{0}' ", tsumikaehokanGyoushaCd);
                }

                //積替え保管場
                string tsumikaehokanGyoubaCd = this.form.cantxt_TsumikaehokanGyoubaCd.Text;
                if (!string.IsNullOrEmpty(tsumikaehokanGyoubaCd))
                {
                    tumikaehokanflg = true;
                    sql.AppendFormat(" AND DT_R19_EX.UPNSAKI_GENBA_CD = '{0}' ", tsumikaehokanGyoubaCd);
                }
                if (tumikaehokanflg)
                {
                    sql.AppendFormat(" AND DT_R19.UPNSAKI_JOU_KBN = 1 ");
                }

                //報告書分類
                if (!string.IsNullOrEmpty(this.form.cantxt_HokokushoBunrui.Text))
                {
                    sql.AppendFormat(" AND M_DENSHI_HAIKI_SHURUI.HOUKOKUSHO_BUNRUI_CD = '{0}' ", this.form.cantxt_HokokushoBunrui.Text);
                }

                //廃棄物種類
                if (!string.IsNullOrEmpty(this.form.cantxt_ElecHaikiShurui.Text))
                {
                    sql.AppendFormat(" AND ISNULL(DT_R18.HAIKI_DAI_CODE, '') + ISNULL(DT_R18.HAIKI_CHU_CODE, '') + ISNULL(DT_R18.HAIKI_SHO_CODE, '') = '{0}' ", this.form.cantxt_ElecHaikiShurui.Text);
                }

                //廃棄物名称
                if (!string.IsNullOrEmpty(this.form.cantxt_ElecHaikiName.Text))
                {
                    sql.AppendFormat(" AND DT_R18_EX.HAIKI_NAME_CD = '{0}' ", this.form.cantxt_ElecHaikiName.Text);
                }

                //マニフェスト一次区分
                if (this.maniFlag == 1)
                {
                    sql.Append(" AND (ISNULL(DT_R18.FIRST_MANIFEST_FLAG, '') = '' OR ISNULL(HST_GYOUSHA.JISHA_KBN, 0) = 0)");
                }
                else
                {
                    sql.Append(" AND (ISNULL(DT_R18.FIRST_MANIFEST_FLAG, '') <> '' AND ISNULL(HST_GYOUSHA.JISHA_KBN, 0) = 1)");
                };
                sql.Append(" AND DT_R18_EX.DELETE_FLG = 0");

                //マニフェスト／交付番号（開始）
                string Manifesutobangou_From = this.form.KOUFUBANNGOFrom.Text.ToString();
                if (!string.IsNullOrEmpty(Manifesutobangou_From))
                {
                    sql.Append(" AND RIGHT('00000000000' + ISNULL(DT_R18.MANIFEST_ID,''), 11) >= '" + Manifesutobangou_From.PadLeft(11, '0') + "'");
                }
                //マニフェスト／交付番号（終了）
                string Manifesutobangou_To = this.form.KOUFUBANNGOTo.Text.ToString();
                if (!string.IsNullOrEmpty(Manifesutobangou_To))
                {
                    sql.Append(" AND RIGHT('00000000000' + ISNULL(DT_R18.MANIFEST_ID,''), 11) <= '" + Manifesutobangou_To.PadLeft(11, '0') + "'");
                }

                #endregion

                #region ORDER BY句

                if (!string.IsNullOrEmpty(this.orderByQuery))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(this.orderByQuery);
                }

                #endregion

                this.createSql = sql.ToString();

                if (string.IsNullOrEmpty(SYSTEM_ID))
                {
                    this.Search_TME = dao_GetTME.getdateforstringsql(this.createSql);
                    count = this.Search_TME.Rows.Count;
                }
                else
                {
                    this.Search_TME_Check = dao_GetTME.getdateforstringsql(this.createSql);
                    count = this.Search_TME_Check.Rows.Count;
                }

                return count;

            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }
        }

        /// <summary>
        /// データ取得（全て）
        /// </summary>
        /// <param name="KOUFU_DATE_FROM"></param>
        /// <param name="KOUFU_DATE_TO"></param>
        /// <param name="HAIKI_KBN_CD"></param>
        /// <param name="DELETE_FLG"></param>
        /// <param name="SYSTEM_ID"></param>
        /// <param name="SEQ"></param>
        /// <param name="KOUFU_DATE_KBN"></param>
        /// <param name="KANRI_ID"></param>
        /// <param name="LATEST_SEQ"></param>
        /// <returns></returns>
        private int Get_Search_AllMani(String KOUFU_DATE_FROM, String KOUFU_DATE_TO, String HAIKI_KBN_CD, String DELETE_FLG,
                                    String SYSTEM_ID, String SEQ, String KOUFU_DATE_KBN, String KANRI_ID, String LATEST_SEQ)
        {
            LogUtility.DebugMethodStart(KOUFU_DATE_FROM, KOUFU_DATE_TO, HAIKI_KBN_CD, DELETE_FLG, SYSTEM_ID, SEQ, KOUFU_DATE_KBN, KANRI_ID, LATEST_SEQ);

            int count = 0;

            try
            {
                var sql = new StringBuilder();

                #region SELECT句

                sql.Append(" SELECT DISTINCT ");
                sql.Append(this.selectQuery);
                sql.AppendFormat(", SUMMARY.SYSTEM_ID AS {0} ", this.HIDDEN_SYSTEM_ID);
                sql.AppendFormat(", SUMMARY.SEQ AS {0} ", this.HIDDEN_SEQ);
                sql.AppendFormat(", SUMMARY.LATEST_SEQ AS {0} ", this.HIDDEN_LATEST_SEQ);
                sql.AppendFormat(", SUMMARY.KANRI_ID AS {0} ", this.HIDDEN_KANRI_ID);
                sql.AppendFormat(", SUMMARY.TOC_STATUS_FLAG AS {0} ", this.HIDDEN_TOC_STATUS_FLAG);
                sql.AppendFormat(", SUMMARY.QUE_STATUS_FLAG AS {0} ", this.HIDDEN_QUE_STATUS_FLAG);
                sql.AppendFormat(", SUMMARY.HAIKI_KBN_CD AS {0} ", this.HIDDEN_HAIKI_KBN);
                sql.AppendFormat(", SUMMARY.HST_GYOUSHA_CD AS {0} ", this.HIDDEN_HST_GYOUSHA_CD);
                if (this.form.CurrentPatternOutputKbn == 2)
                {
                    sql.AppendFormat(", SUMMARY.DETAIL_SYSTEM_ID AS {0} ", this.HIDDEN_DETAIL_SYSTEM_ID);
                }

                string errorDefault = "\'0\'";
                sql.AppendFormat(" ,{0} AS HST_GYOUSHA_CD_ERROR", errorDefault);
                sql.AppendFormat(" ,{0} AS HST_GENBA_CD_ERROR", errorDefault);
                sql.AppendFormat(" ,{0} AS HAIKI_SHURUI_CD_ERROR", errorDefault);

                #endregion

                #region FROM句

                sql.Append(" FROM ( ");

                #region 紙マニ

                //sql.Append(" SELECT DISTINCT ");
                sql.Append(" SELECT ");

                // マニ
                sql.Append("   TME.SYSTEM_ID ");                                            // システムID
                sql.Append(" , TME.SEQ ");                                                  // 枝番
                sql.Append(" , NULL AS LATEST_SEQ ");                                       // 最終枝番（紙マニは必ず空）
                sql.Append(" , NULL AS KANRI_ID ");                                         // 管理ID（紙マニは必ず空）
                sql.Append(" , NULL AS TOC_STATUS_FLAG ");
                sql.Append(" , NULL AS QUE_STATUS_FLAG ");
                sql.Append(" , TME.HAIKI_KBN_CD ");                                         // 廃棄区分CD
                sql.Append(" , TME.FIRST_MANIFEST_KBN ");                                   // 一次マニ区分
                sql.Append(" , TME.KOUFU_DATE ");                                           // 交付年月日
                sql.Append(" , TME.KOUFU_KBN ");                                            // 交付番号区分
                sql.Append(" , TME.MANIFEST_ID ");                                          // 交付番号
                sql.Append(" , TME.SEIRI_ID ");                                             // 整理番号
                sql.Append(" , TME.KOUFU_TANTOUSHA ");                                      // 交付担当者
                sql.Append(" , TME.KYOTEN_CD ");                                            // 拠点CD
                sql.Append(" , TME.TORIHIKISAKI_CD ");                                      // 取引先CD
                sql.Append(" , TME.HST_GYOUSHA_CD ");                                       // 排出事業者CD
                sql.Append(" , LTRIM(RTRIM(SUBSTRING(ISNULL(TME.HST_GYOUSHA_NAME, ''),1, 40)) + SUBSTRING(ISNULL(TME.HST_GYOUSHA_NAME, ''),41, 40)) AS HST_GYOUSHA_NAME ");          // 排出事業者名
                sql.Append(" , TME.HST_GYOUSHA_POST ");                                     // 排出事業者郵便番号
                sql.Append(" , TME.HST_GYOUSHA_TEL ");                                      // 排出事業者電話番号
                sql.Append(" , TME.HST_GYOUSHA_ADDRESS ");                                  // 排出事業者住所
                sql.Append(" , TME.HST_GENBA_CD ");                                         // 排出事業場CD
                sql.Append(" , LTRIM(RTRIM(SUBSTRING(ISNULL(TME.HST_GENBA_NAME, ''),1, 40)) + SUBSTRING(ISNULL(TME.HST_GENBA_NAME, ''),41, 40)) AS HST_GENBA_NAME ");              // 排出事業場名
                sql.Append(" , TME.HST_GENBA_POST ");                                       // 排出事業場郵便番号
                sql.Append(" , TME.HST_GENBA_TEL ");                                        // 排出事業場電話番号
                sql.Append(" , TME.HST_GENBA_ADDRESS ");                                    // 排出事業場住所

                // 20140603 katen 不具合No.4131 start‏
                sql.Append(" , TMU_CONDITION.UPN_END_DATE ");                               // 運搬終了年月日(検索用)
                sql.Append(" , TMU_CONDITION.UPN_GYOUSHA_CD ");                          // 運搬受託者(検索用)
                sql.Append(" , TME.SBN_JYUTAKUSHA_CD ");                                    // 処分受託者(検索用)
                sql.Append(" , TMU_CONDITION.UPN_SAKI_GENBA_CD ");                          // 運搬先の事業場(検索用)
                sql.Append(" , HAISHU.HOUKOKUSHO_BUNRUI_CD ");                      // 報告書分類
                sql.Append(" , TMD.HAIKI_NAME_CD ");                                        // 廃棄物名称(検索用)
                // 20140603 katen 不具合No.4131 end‏

                // 収集運搬1
                sql.Append(" , UPN1.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD1 ");                   // 運搬業者CD
                sql.Append(" , UPN1.UPN_GYOUSHA_NAME AS UPN_GYOUSHA_NAME1 ");               // 運搬業者名
                sql.Append(" , UPN1.UPN_GYOUSHA_POST AS UPN_GYOUSHA_POST1 ");               // 運搬業者郵便番号
                sql.Append(" , UPN1.UPN_GYOUSHA_TEL AS UPN_GYOUSHA_TEL1 ");                 // 運搬業者電話番号
                sql.Append(" , UPN1.UPN_GYOUSHA_ADDRESS AS UPN_GYOUSHA_ADDRESS1 ");         // 運搬業者住所
                sql.Append(" , UPN1.SHASHU_CD AS SHASHU_CD1 ");                             // 車種CD
                sql.Append(" , UPN1.SHASHU_NAME AS INPUT_SHASHU_NAME1 ");                   // 車種名(建廃用)
                sql.Append(" , UPN1.SHARYOU_CD AS SHARYOU_CD1 ");                           // 車輌CD
                sql.Append(" , SHARYOU1.SHARYOU_NAME_RYAKU AS SHARYOU_NAME1 ");             // 車輌名
                sql.Append(" , UPN1.SHARYOU_NAME AS INPUT_SHARYOU_NAME1 ");                 // 車輌名(電マニ以外用)
                sql.Append(" , UPN1.UPN_HOUHOU_CD AS UPN_HOUHOU_CD1 ");                     // 運搬方法CD

                // 20140619 kayo 不具合No.4634 運搬席の事業場が区間１と区間２の両方に出てしまっている start‏
                //sql.Append(" , UPN1.UPN_SAKI_KBN AS UPN_SAKI_KBN1 ");                       // 運搬先区分
                //sql.Append(" , UPN1.UPN_SAKI_GYOUSHA_CD AS UPN_SAKI_GYOUSHA_CD1 ");         // 運搬先業者CD
                //sql.Append(" , GYO1.GYOUSHA_NAME_RYAKU AS UPN_SAKI_GYOUSHA_NAME1 ");        // 運搬先業者名
                //sql.Append(" , UPN1.UPN_SAKI_GENBA_CD AS UPN_SAKI_GENBA_CD1 ");             // 運搬先現場CD
                //sql.Append(" , UPN1.UPN_SAKI_GENBA_NAME AS UPN_SAKI_GENBA_NAME1 ");         // 運搬先現場名
                //sql.Append(" , UPN1.UPN_SAKI_GENBA_POST AS UPN_SAKI_GENBA_POST1 ");         // 運搬先現場郵便番号
                //sql.Append(" , UPN1.UPN_SAKI_GENBA_TEL AS UPN_SAKI_GENBA_TEL1 ");           // 運搬先現場電話番号
                //sql.Append(" , UPN1.UPN_SAKI_GENBA_ADDRESS AS UPN_SAKI_GENBA_ADDRESS1 ");   // 運搬先住所
                sql.Append(" , CASE WHEN TME.HAIKI_KBN_CD = 2 THEN ");
                sql.Append("       CASE WHEN ISNULL(TME.TMH_GENBA_CD, '') != '' THEN 2 ELSE 1 END ");
                sql.Append("   ELSE UPN1.UPN_SAKI_KBN END AS UPN_SAKI_KBN1 ");                // 運搬先区分
                sql.Append(" , CASE WHEN TME.HAIKI_KBN_CD = 2 THEN ");
                sql.Append("       CASE WHEN ISNULL(TME.TMH_GENBA_CD, '') != '' THEN TME.TMH_GYOUSHA_CD ELSE UPN1.UPN_SAKI_GYOUSHA_CD END ");
                sql.Append("   ELSE UPN1.UPN_SAKI_GYOUSHA_CD END AS UPN_SAKI_GYOUSHA_CD1 ");             // 運搬先業者CD
                sql.Append(" , CASE WHEN TME.HAIKI_KBN_CD = 2 THEN ");
                sql.Append("       CASE WHEN ISNULL(TME.TMH_GENBA_CD, '') != '' THEN TME.TMH_GYOUSHA_NAME ELSE GYO1.GYOUSHA_NAME_RYAKU END ");
                sql.Append("   ELSE GYO1.GYOUSHA_NAME_RYAKU END AS UPN_SAKI_GYOUSHA_NAME1 ");     // 運搬先業者名CD
                sql.Append(" , CASE WHEN TME.HAIKI_KBN_CD = 2 THEN ");
                sql.Append("       CASE WHEN ISNULL(TME.TMH_GENBA_CD, '') != '' THEN TME.TMH_GENBA_CD ELSE UPN1.UPN_SAKI_GENBA_CD END ");
                sql.Append("   ELSE UPN1.UPN_SAKI_GENBA_CD END AS UPN_SAKI_GENBA_CD1 ");          // 運搬先現場CD
                sql.Append(" , CASE WHEN TME.HAIKI_KBN_CD = 2 THEN ");
                sql.Append("       CASE WHEN ISNULL(TME.TMH_GENBA_CD, '') != '' THEN TME.TMH_GENBA_NAME ELSE UPN1.UPN_SAKI_GENBA_NAME END ");
                sql.Append("   ELSE UPN1.UPN_SAKI_GENBA_NAME END AS UPN_SAKI_GENBA_NAME1 ");      // 運搬先現場名
                sql.Append(" , CASE WHEN TME.HAIKI_KBN_CD = 2 THEN ");
                sql.Append("       CASE WHEN ISNULL(TME.TMH_GENBA_CD, '') != '' THEN TME.TMH_GENBA_POST ELSE UPN1.UPN_SAKI_GENBA_POST END ");
                sql.Append("   ELSE UPN1.UPN_SAKI_GENBA_POST END AS UPN_SAKI_GENBA_POST1 ");      // 運搬先現場郵便番号
                sql.Append(" , CASE WHEN TME.HAIKI_KBN_CD = 2 THEN ");
                sql.Append("       CASE WHEN ISNULL(TME.TMH_GENBA_CD, '') != '' THEN TME.TMH_GENBA_TEL ELSE UPN1.UPN_SAKI_GENBA_TEL END ");
                sql.Append("   ELSE UPN1.UPN_SAKI_GENBA_TEL END AS UPN_SAKI_GENBA_TEL1 ");        // 運搬先現場電話番号
                sql.Append(" , CASE WHEN TME.HAIKI_KBN_CD = 2 THEN ");
                sql.Append("       CASE WHEN ISNULL(TME.TMH_GENBA_CD, '') != '' THEN TME.TMH_GENBA_ADDRESS ELSE UPN1.UPN_SAKI_GENBA_ADDRESS END ");
                sql.Append("   ELSE UPN1.UPN_SAKI_GENBA_ADDRESS END AS UPN_SAKI_GENBA_ADDRESS1 ");// 運搬先住所
                // 20140619 kayo 不具合No.4634 運搬席の事業場が区間１と区間２の両方に出てしまっている start‏
                sql.Append(" , UPN1.UPN_END_DATE AS UPN_END_DATE1 ");                       // 運搬終了年月日

                // 収集運搬2
                sql.Append(" , UPN2.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD2 ");                   // 運搬業者CD
                sql.Append(" , UPN2.UPN_GYOUSHA_NAME AS UPN_GYOUSHA_NAME2 ");               // 運搬業者名
                sql.Append(" , UPN2.UPN_GYOUSHA_POST AS UPN_GYOUSHA_POST2 ");               // 運搬業者郵便番号
                sql.Append(" , UPN2.UPN_GYOUSHA_TEL AS UPN_GYOUSHA_TEL2 ");                 // 運搬業者電話番号
                sql.Append(" , UPN2.UPN_GYOUSHA_ADDRESS AS UPN_GYOUSHA_ADDRESS2 ");         // 運搬業者住所
                sql.Append(" , UPN2.SHASHU_CD AS SHASHU_CD2 ");                             // 車種CD
                sql.Append(" , UPN2.SHASHU_NAME AS INPUT_SHASHU_NAME2 ");                   // 車種名(建廃用)
                sql.Append(" , UPN2.SHARYOU_CD AS SHARYOU_CD2 ");                           // 車輌CD
                sql.Append(" , SHARYOU2.SHARYOU_NAME_RYAKU AS SHARYOU_NAME2 ");             // 車輌名
                sql.Append(" , UPN2.SHARYOU_NAME AS INPUT_SHARYOU_NAME2 ");                 // 車輌名(電マニ以外用)
                sql.Append(" , UPN2.UPN_HOUHOU_CD AS UPN_HOUHOU_CD2 ");                     // 運搬方法CD
                // 20140619 kayo 不具合No.4634 運搬席の事業場が区間１と区間２の両方に出てしまっている start‏
                //sql.Append(" , UPN2.UPN_SAKI_KBN AS UPN_SAKI_KBN2 ");                       // 運搬先区分
                //sql.Append(" , UPN2.UPN_SAKI_GYOUSHA_CD AS UPN_SAKI_GYOUSHA_CD2 ");         // 運搬先業者CD
                //sql.Append(" , GYO2.GYOUSHA_NAME_RYAKU AS UPN_SAKI_GYOUSHA_NAME2 ");        // 運搬先業者名
                //sql.Append(" , UPN2.UPN_SAKI_GENBA_CD AS UPN_SAKI_GENBA_CD2 ");             // 運搬先現場CD
                //sql.Append(" , UPN2.UPN_SAKI_GENBA_NAME AS UPN_SAKI_GENBA_NAME2 ");         // 運搬先現場名
                //sql.Append(" , UPN2.UPN_SAKI_GENBA_POST AS UPN_SAKI_GENBA_POST2 ");         // 運搬先現場郵便番号
                //sql.Append(" , UPN2.UPN_SAKI_GENBA_TEL AS UPN_SAKI_GENBA_TEL2 ");           // 運搬先現場電話番号
                //sql.Append(" , UPN2.UPN_SAKI_GENBA_ADDRESS AS UPN_SAKI_GENBA_ADDRESS2 ");   // 運搬先住所
                sql.Append(" , CASE WHEN TME.HAIKI_KBN_CD = 2 THEN ");
                sql.Append("       CASE WHEN ISNULL(TME.TMH_GENBA_CD, '') != '' THEN 1 ELSE '' END ");
                sql.Append("   ELSE UPN2.UPN_SAKI_KBN END AS UPN_SAKI_KBN2 ");                // 運搬先区分
                sql.Append(" , CASE WHEN TME.HAIKI_KBN_CD = 2 THEN ");
                sql.Append("       CASE WHEN ISNULL(TME.TMH_GENBA_CD, '') != '' THEN UPN1.UPN_SAKI_GYOUSHA_CD ELSE '' END ");
                sql.Append("   ELSE UPN2.UPN_SAKI_GYOUSHA_CD END AS UPN_SAKI_GYOUSHA_CD2 ");      // 運搬先業者CD
                sql.Append(" , CASE WHEN TME.HAIKI_KBN_CD = 2 THEN ");
                sql.Append("       CASE WHEN ISNULL(TME.TMH_GENBA_CD, '') != '' THEN GYO2.GYOUSHA_NAME_RYAKU ELSE '' END ");
                sql.Append("   ELSE GYO2.GYOUSHA_NAME_RYAKU END AS UPN_SAKI_GYOUSHA_NAME2 ");     // 運搬先業者名CD
                sql.Append(" , CASE WHEN TME.HAIKI_KBN_CD = 2 THEN ");
                sql.Append("       CASE WHEN ISNULL(TME.TMH_GENBA_CD, '') != '' THEN UPN1.UPN_SAKI_GENBA_CD ELSE '' END ");
                sql.Append("   ELSE UPN2.UPN_SAKI_GENBA_CD END AS UPN_SAKI_GENBA_CD2 ");          // 運搬先現場CD
                sql.Append(" , CASE WHEN TME.HAIKI_KBN_CD = 2 THEN ");
                sql.Append("       CASE WHEN ISNULL(TME.TMH_GENBA_CD, '') != '' THEN UPN1.UPN_SAKI_GENBA_NAME ELSE '' END ");
                sql.Append("   ELSE UPN2.UPN_SAKI_GENBA_NAME END AS UPN_SAKI_GENBA_NAME2 ");      // 運搬先現場名
                sql.Append(" , CASE WHEN TME.HAIKI_KBN_CD = 2 THEN ");
                sql.Append("       CASE WHEN ISNULL(TME.TMH_GENBA_CD, '') != '' THEN UPN1.UPN_SAKI_GENBA_POST ELSE '' END ");
                sql.Append("   ELSE UPN2.UPN_SAKI_GENBA_POST END AS UPN_SAKI_GENBA_POST2 ");      // 運搬先現場郵便番号
                sql.Append(" , CASE WHEN TME.HAIKI_KBN_CD = 2 THEN ");
                sql.Append("       CASE WHEN ISNULL(TME.TMH_GENBA_CD, '') != '' THEN UPN1.UPN_SAKI_GENBA_TEL ELSE '' END ");
                sql.Append("   ELSE UPN2.UPN_SAKI_GENBA_TEL END AS UPN_SAKI_GENBA_TEL2 ");        // 運搬先現場電話番号
                sql.Append(" , CASE WHEN TME.HAIKI_KBN_CD = 2 THEN ");
                sql.Append("       CASE WHEN ISNULL(TME.TMH_GENBA_CD, '') != '' THEN UPN1.UPN_SAKI_GENBA_ADDRESS ELSE '' END ");
                sql.Append("   ELSE UPN2.UPN_SAKI_GENBA_ADDRESS END AS UPN_SAKI_GENBA_ADDRESS2 ");// 運搬先住所
                // 20140619 kayo 不具合No.4634 運搬席の事業場が区間１と区間２の両方に出てしまっている start‏
                sql.Append(" , UPN2.UPN_END_DATE AS UPN_END_DATE2 ");                       // 運搬終了年月日
                // 収集運搬3
                sql.Append(" , UPN3.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD3 ");                   // 運搬業者CD
                sql.Append(" , UPN3.UPN_GYOUSHA_NAME AS UPN_GYOUSHA_NAME3 ");               // 運搬業者名
                sql.Append(" , UPN3.UPN_GYOUSHA_POST AS UPN_GYOUSHA_POST3 ");               // 運搬業者郵便番号
                sql.Append(" , UPN3.UPN_GYOUSHA_TEL AS UPN_GYOUSHA_TEL3 ");                 // 運搬業者電話番号
                sql.Append(" , UPN3.UPN_GYOUSHA_ADDRESS AS UPN_GYOUSHA_ADDRESS3 ");         // 運搬業者住所
                sql.Append(" , UPN3.SHASHU_CD AS SHASHU_CD3 ");                             // 車種CD
                sql.Append(" , UPN3.SHARYOU_CD AS SHARYOU_CD3 ");                           // 車輌CD
                sql.Append(" , SHARYOU3.SHARYOU_NAME_RYAKU AS SHARYOU_NAME3 ");             // 車輌名
                sql.Append(" , UPN3.SHARYOU_NAME AS INPUT_SHARYOU_NAME3 ");                 // 車輌名(電マニ以外用)
                sql.Append(" , UPN3.UPN_HOUHOU_CD AS UPN_HOUHOU_CD3 ");                     // 運搬方法CD
                sql.Append(" , UPN3.UPN_SAKI_KBN AS UPN_SAKI_KBN3 ");                       // 運搬先区分
                sql.Append(" , UPN3.UPN_SAKI_GYOUSHA_CD AS UPN_SAKI_GYOUSHA_CD3 ");         // 運搬先業者CD
                sql.Append(" , GYO3.GYOUSHA_NAME_RYAKU AS UPN_SAKI_GYOUSHA_NAME3 ");        // 運搬先業者名
                sql.Append(" , UPN3.UPN_SAKI_GENBA_CD AS UPN_SAKI_GENBA_CD3 ");             // 運搬先現場CD
                sql.Append(" , UPN3.UPN_SAKI_GENBA_NAME AS UPN_SAKI_GENBA_NAME3 ");         // 運搬先現場名
                sql.Append(" , UPN3.UPN_SAKI_GENBA_POST AS UPN_SAKI_GENBA_POST3 ");         // 運搬先現場郵便番号
                sql.Append(" , UPN3.UPN_SAKI_GENBA_TEL AS UPN_SAKI_GENBA_TEL3 ");           // 運搬先現場電話番号
                sql.Append(" , UPN3.UPN_SAKI_GENBA_ADDRESS AS UPN_SAKI_GENBA_ADDRESS3 ");   // 運搬先住所
                sql.Append(" , UPN3.UPN_END_DATE AS UPN_END_DATE3 ");                       // 運搬終了年月日

                // 処分受託者情報
                sql.Append(" , TME.SBN_GYOUSHA_CD ");                                       // 処分業者CD
                sql.Append(" , TME.SBN_GYOUSHA_NAME ");                                     // 処分業者名
                sql.Append(" , TME.SBN_GYOUSHA_POST ");                                     // 処分業者郵便番号
                sql.Append(" , TME.SBN_GYOUSHA_TEL ");                                      // 処分業者電話番号
                sql.Append(" , TME.SBN_GYOUSHA_ADDRESS ");                                  // 処分業者住所
                sql.Append(" , TMD_SBN.SBN_END_DATE ");                                     // 処分終了年月日

                // 積替保管情報
                sql.Append(" , TME.TMH_GYOUSHA_CD ");                                       // 積保業者CD
                sql.Append(" , TME.TMH_GYOUSHA_NAME ");                                     // 積保業者名
                sql.Append(" , TME.TMH_GENBA_CD ");                                         // 積保現場CD
                sql.Append(" , TME.TMH_GENBA_NAME ");                                       // 積保現場名
                sql.Append(" , TME.TMH_GENBA_POST ");                                       // 積保現場郵便番号
                sql.Append(" , TME.TMH_GENBA_TEL ");                                        // 積保現場電話番号
                sql.Append(" , TME.TMH_GENBA_ADDRESS ");                                    // 積保現場住所

                // 伝票には最終処分は不要
                //// 最終処分情報
                //sql.Append(" , TME.LAST_SBN_GYOUSHA_CD ");                                  // 最終処分業者CD
                //sql.Append(" , GYO1.GYOUSHA_NAME_RYAKU AS LAST_SBN_GYOUSHA_NAME ");         // 最終処分業者名(業者マスタ)
                //sql.Append(" , TME.LAST_SBN_GENBA_CD ");                                    // 最終処分現場CD
                //sql.Append(" , TME.LAST_SBN_GENBA_NAME ");                                  // 最終処分現場名
                //sql.Append(" , TME.LAST_SBN_GENBA_POST ");                                  // 最終処分郵便番号
                //sql.Append(" , TME.LAST_SBN_GENBA_TEL ");                                   // 最終処分電話番号
                //sql.Append(" , TME.LAST_SBN_GENBA_ADDRESS ");                               // 最終処分現場住所
                //sql.Append(" , TME.LAST_SBN_GENBA_NUMBER ");                                // 最終処分現場番号
                sql.Append(" , TMD_LAST_SBN.LAST_SBN_END_DATE ");                              // 最終処分終了年月日

                // システム情報
                sql.Append(" , TME.CREATE_USER ");                                          // 作成者
                sql.Append(" , TME.CREATE_DATE ");                                          // 作成日時
                sql.Append(" , TME.CREATE_PC ");                                            // 作成PC
                sql.Append(" , TME.UPDATE_USER ");                                          // 最終更新者
                sql.Append(" , TME.UPDATE_DATE ");                                          // 最終更新日時
                sql.Append(" , TME.UPDATE_PC ");                                            // 最終更新PC
                sql.Append(" , TME.DELETE_FLG ");                                           // 削除フラグ

                // 明細
                sql.Append(" , TMD.DETAIL_SYSTEM_ID AS DETAIL_SYSTEM_ID ");                 // 明細システムID
                sql.Append(" , TMD.HAIKI_SHURUI_CD AS DETAIL_HAIKI_SHURUI_CD ");             // 廃棄種類CD
                sql.Append(" , HAISHU.HAIKI_SHURUI_NAME_RYAKU AS DETAIL_HAIKI_SHURUI_NAME ");// 廃棄種類名(廃棄種類マスタ)
                sql.Append(" , TMD.HAIKI_NAME_CD AS DETAIL_HAIKI_NAME_CD ");                 // 廃棄物CD
                sql.Append(" , HAIKI.HAIKI_NAME_RYAKU AS DETAIL_HAIKI_NAME ");               // 廃棄物名(廃棄物マスタ)
                // 20140609 kayo 不具合No.4710 報告書分類を選択できるように修正 start‏
                sql.Append(" , M_HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_NAME_RYAKU AS HOUKOKUSHO_BUNRUI_NAME_RYAKU "); // 報告書分類
                // 20140609 kayo 不具合No.4710 報告書分類を選択できるように修正 end‏
                sql.Append(" , TMD.NISUGATA_CD AS DETAIL_NISUGATA_CD ");                     // 荷姿CD
                sql.Append(" , NISU.NISUGATA_NAME_RYAKU AS DETAIL_NISUGATA_NAME ");          // 荷姿名
                sql.Append(" , TMD.HAIKI_SUU AS DETAIL_HAIKI_SUU ");                         // 数量
                sql.Append(" , TMD.HAIKI_UNIT_CD AS DETAIL_HAIKI_UNIT_CD ");                 // 単位CD
                sql.Append(" , TMD.KANSAN_SUU AS DETAIL_KANSAN_SUU ");                       // 換算数量
                sql.Append(" , TMD.GENNYOU_SUU AS DETAIL_GENNYOU_SUU ");                     // 減容数
                sql.Append(" , TMD.SBN_HOUHOU_CD AS DETAIL_SBN_HOUHOU_CD ");                 // 処分方法CD
                sql.Append(" , TMD.SBN_END_DATE AS DETAIL_SBN_END_DATE ");                   // 処分終了年月日
                sql.Append(" , TMD.LAST_SBN_END_DATE AS DETAIL_LAST_SBN_END_DATE ");         // 最終処分終了年月日
                sql.Append(" , TMD.LAST_SBN_GYOUSHA_CD AS DETAIL_LAST_SBN_GYOUSHA_CD ");     // 最終処分業者CD
                sql.Append(" , GYO.GYOUSHA_NAME_RYAKU AS DETAIL_LAST_SBN_GYOUSHA_NAME ");    // 最終処分業者名(業者マスタ)
                sql.Append(" , TMD.LAST_SBN_GENBA_CD AS DETAIL_LAST_SBN_GENBA_CD ");         // 最終処分現場CD
                sql.Append(" , GEN.GENBA_NAME_RYAKU AS DETAIL_LAST_SBN_GENBA_NAME ");        // 最終処分現場名(現場マスタ)
                sql.Append(" , TME2.MANIFEST_ID AS DETAIL_NIJI_MANIFEST_ID ");               // 二次マニ交付番号
                sql.Append(" , TMRD.SEND_A AS SEND_A ");               // 返却日入力A票
                sql.Append(" , TMRD.SEND_B1 AS SEND_B1 ");               // 返却日入力B1票
                sql.Append(" , TMRD.SEND_B2 AS SEND_B2 ");               // 返却日入力B2票
                sql.Append(" , TMRD.SEND_B4 AS SEND_B4 ");               // 返却日入力B4票
                sql.Append(" , TMRD.SEND_B6 AS SEND_B6 ");               // 返却日入力B6票
                sql.Append(" , TMRD.SEND_C1 AS SEND_C1 ");               // 返却日入力C1票
                sql.Append(" , TMRD.SEND_C2 AS SEND_C2 ");               // 返却日入力C2票
                sql.Append(" , TMRD.SEND_D AS SEND_D ");               // 返却日入力D票
                sql.Append(" , TMRD.SEND_E AS SEND_E ");               // 返却日入力E票
                // マニ入力
                sql.Append(" FROM T_MANIFEST_ENTRY TME ");

                // 処分明細
                sql.Append(" LEFT JOIN ");
                sql.Append(" ( SELECT T_MANIFEST_ENTRY.SYSTEM_ID, ");
                sql.Append(" T_MANIFEST_ENTRY.SEQ, ");
                sql.Append(" MAX (SBN_END_DATE) AS SBN_END_DATE ");
                sql.Append(" FROM T_MANIFEST_ENTRY ");
                sql.Append(" INNER JOIN T_MANIFEST_DETAIL ");
                sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_DETAIL.SYSTEM_ID ");
                sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_DETAIL.SEQ ");
                sql.Append(" AND T_MANIFEST_DETAIL.SBN_END_DATE IS NOT NULL ");
                sql.Append(" WHERE T_MANIFEST_ENTRY.DELETE_FLG = 'false' ");
                sql.Append(" AND NOT EXISTS ( SELECT * ");
                sql.Append(" FROM T_MANIFEST_DETAIL ");
                sql.Append(" WHERE T_MANIFEST_ENTRY.DELETE_FLG = 'false' ");
                sql.Append(" AND T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_DETAIL.SYSTEM_ID ");
                sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_DETAIL.SEQ ");
                sql.Append(" and T_MANIFEST_DETAIL.SBN_END_DATE IS NULL) ");
                sql.Append(" GROUP BY T_MANIFEST_ENTRY.SYSTEM_ID, ");
                sql.Append(" T_MANIFEST_ENTRY.SEQ ");
                sql.Append(" UNION ");
                sql.Append(" SELECT T_MANIFEST_ENTRY.SYSTEM_ID, ");
                sql.Append(" T_MANIFEST_ENTRY.SEQ, ");
                sql.Append(" SBN_END_DATE ");
                sql.Append(" FROM T_MANIFEST_ENTRY ");
                sql.Append(" LEFT JOIN T_MANIFEST_DETAIL ");
                sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_DETAIL.SYSTEM_ID ");
                sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_DETAIL.SEQ ");
                sql.Append(" WHERE T_MANIFEST_ENTRY.DELETE_FLG = 'false' ");
                sql.Append(" and T_MANIFEST_DETAIL.SBN_END_DATE IS NULL ");
                sql.Append(" GROUP BY T_MANIFEST_ENTRY.SYSTEM_ID, ");
                sql.Append(" T_MANIFEST_ENTRY.SEQ, ");
                sql.Append(" T_MANIFEST_DETAIL.SBN_END_DATE) AS TMD_SBN ");
                sql.Append(" ON TME.SYSTEM_ID = TMD_SBN.SYSTEM_ID ");
                sql.Append(" AND TME.SEQ = TMD_SBN.SEQ ");


                // 最終処分明細
                sql.Append(" LEFT JOIN ");
                sql.Append(" ( SELECT T_MANIFEST_ENTRY.SYSTEM_ID, ");
                sql.Append(" T_MANIFEST_ENTRY.SEQ, ");
                sql.Append(" MAX (LAST_SBN_END_DATE) AS LAST_SBN_END_DATE ");
                sql.Append(" FROM T_MANIFEST_ENTRY ");
                sql.Append(" INNER JOIN T_MANIFEST_DETAIL ");
                sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_DETAIL.SYSTEM_ID ");
                sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_DETAIL.SEQ ");
                sql.Append(" AND T_MANIFEST_DETAIL.LAST_SBN_END_DATE IS NOT NULL ");
                sql.Append(" WHERE T_MANIFEST_ENTRY.DELETE_FLG = 'false' ");
                sql.Append(" AND NOT EXISTS ( SELECT * ");
                sql.Append(" FROM T_MANIFEST_DETAIL ");
                sql.Append(" WHERE T_MANIFEST_ENTRY.DELETE_FLG = 'false' ");
                sql.Append(" AND T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_DETAIL.SYSTEM_ID ");
                sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_DETAIL.SEQ ");
                sql.Append(" and T_MANIFEST_DETAIL.LAST_SBN_END_DATE IS NULL) ");
                sql.Append(" GROUP BY T_MANIFEST_ENTRY.SYSTEM_ID, ");
                sql.Append(" T_MANIFEST_ENTRY.SEQ ");
                sql.Append(" UNION ");
                sql.Append(" SELECT T_MANIFEST_ENTRY.SYSTEM_ID, ");
                sql.Append(" T_MANIFEST_ENTRY.SEQ, ");
                sql.Append(" LAST_SBN_END_DATE ");
                sql.Append(" FROM T_MANIFEST_ENTRY ");
                sql.Append(" LEFT JOIN T_MANIFEST_DETAIL ");
                sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_DETAIL.SYSTEM_ID ");
                sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_DETAIL.SEQ ");
                sql.Append(" WHERE T_MANIFEST_ENTRY.DELETE_FLG = 'false' ");
                sql.Append(" and T_MANIFEST_DETAIL.LAST_SBN_END_DATE IS NULL ");
                sql.Append(" GROUP BY T_MANIFEST_ENTRY.SYSTEM_ID, ");
                sql.Append(" T_MANIFEST_ENTRY.SEQ, ");
                sql.Append(" T_MANIFEST_DETAIL.LAST_SBN_END_DATE) AS TMD_LAST_SBN ");
                sql.Append(" ON TME.SYSTEM_ID = TMD_LAST_SBN.SYSTEM_ID ");
                sql.Append(" AND TME.SEQ = TMD_LAST_SBN.SEQ ");

                // 収集運搬1
                sql.Append(" LEFT JOIN T_MANIFEST_UPN UPN1 ");
                sql.Append(" ON TME.SYSTEM_ID = UPN1.SYSTEM_ID AND TME.SEQ = UPN1.SEQ ");
                sql.Append(" AND UPN1.UPN_ROUTE_NO = 1 ");
                // 車輌1
                sql.Append(" LEFT JOIN M_SHARYOU SHARYOU1 ");
                sql.Append(" ON UPN1.SHARYOU_CD = SHARYOU1.SHARYOU_CD AND UPN1.UPN_GYOUSHA_CD = SHARYOU1.GYOUSHA_CD ");

                // 収集運搬2
                sql.Append(" LEFT JOIN T_MANIFEST_UPN UPN2 ");
                sql.Append(" ON TME.SYSTEM_ID = UPN2.SYSTEM_ID AND TME.SEQ = UPN2.SEQ ");
                sql.Append(" AND UPN2.UPN_ROUTE_NO = 2 ");
                // 車輌2
                sql.Append(" LEFT JOIN M_SHARYOU SHARYOU2 ");
                sql.Append(" ON UPN2.SHARYOU_CD = SHARYOU2.SHARYOU_CD AND UPN2.UPN_GYOUSHA_CD = SHARYOU2.GYOUSHA_CD ");

                // 収集運搬3
                sql.Append(" LEFT JOIN T_MANIFEST_UPN UPN3 ");
                sql.Append(" ON TME.SYSTEM_ID = UPN3.SYSTEM_ID AND TME.SEQ = UPN3.SEQ ");
                sql.Append(" AND UPN3.UPN_ROUTE_NO = 3 ");
                // 車輌3
                sql.Append(" LEFT JOIN M_SHARYOU SHARYOU3 ");
                sql.Append(" ON UPN3.SHARYOU_CD = SHARYOU3.SHARYOU_CD AND UPN3.UPN_GYOUSHA_CD = SHARYOU3.GYOUSHA_CD ");

                // 明細
                sql.Append(" LEFT JOIN T_MANIFEST_DETAIL TMD ");
                sql.Append(" ON TME.SYSTEM_ID = TMD.SYSTEM_ID AND TME.SEQ = TMD.SEQ ");
                // 二次マニ
                sql.Append(" LEFT JOIN T_MANIFEST_RELATION REL ");
                sql.Append(" ON TMD.DETAIL_SYSTEM_ID = REL.FIRST_SYSTEM_ID ");
                sql.Append(" AND REL.DELETE_FLG = 0 ");
                sql.Append(" AND REL.FIRST_HAIKI_KBN_CD <> 4 ");
                sql.Append(" AND REL.REC_SEQ = (SELECT MAX(TMP.REC_SEQ) FROM T_MANIFEST_RELATION TMP ");
                sql.Append(" WHERE TMP.FIRST_SYSTEM_ID = REL.FIRST_SYSTEM_ID AND TMP.DELETE_FLG = 0 AND TMP.FIRST_HAIKI_KBN_CD <> 4 ) ");
                sql.Append(" LEFT JOIN (");
                sql.Append(" SELECT DISTINCT");
                sql.Append(" MR.NEXT_SYSTEM_ID AS SYSTEM_ID");
                sql.Append(" ,MR.NEXT_HAIKI_KBN_CD AS HAIKI_KBN_CD");
                sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN EX.MANIFEST_ID ELSE ME.MANIFEST_ID END) AS MANIFEST_ID");
                sql.Append(" FROM T_MANIFEST_RELATION AS MR");
                sql.Append(" LEFT JOIN");
                // 2016.11.23 chinkeigen マニ入力と一覧 #101092 
                //sql.Append(" (SELECT * FROM T_MANIFEST_ENTRY WHERE DELETE_FLG = 0) AS ME");
                //sql.Append(" ON MR.NEXT_SYSTEM_ID = ME.SYSTEM_ID");
                sql.Append(" (SELECT T_MANIFEST_ENTRY.*,T_MANIFEST_DETAIL.DETAIL_SYSTEM_ID ");
                sql.Append("    FROM T_MANIFEST_ENTRY LEFT JOIN T_MANIFEST_DETAIL ON ");
                sql.Append("    T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_DETAIL.SYSTEM_ID ");
                sql.Append("  WHERE DELETE_FLG = 0) AS ME");
                sql.Append(" ON MR.NEXT_SYSTEM_ID = ME.DETAIL_SYSTEM_ID");
                // 2016.11.23 chinkeigen マニ入力と一覧 #101092 

                sql.Append(" AND MR.NEXT_HAIKI_KBN_CD = ME.HAIKI_KBN_CD");
                sql.Append(" LEFT JOIN");
                sql.Append(" (SELECT * FROM DT_R18_EX WHERE DELETE_FLG = 0) AS EX");
                sql.Append(" ON MR.NEXT_SYSTEM_ID = EX.SYSTEM_ID");
                sql.Append(" AND MR.NEXT_HAIKI_KBN_CD = 4");
                sql.Append(" WHERE MR.DELETE_FLG = 0");
                sql.Append(" ) AS TME2");
                sql.Append(" ON REL.NEXT_SYSTEM_ID = TME2.SYSTEM_ID");
                sql.Append(" AND REL.NEXT_HAIKI_KBN_CD = TME2.HAIKI_KBN_CD");

                // 各種マスタ
                // 運搬先業者1
                sql.Append(" LEFT JOIN M_GYOUSHA GYO1 ");
                sql.Append(" ON UPN1.UPN_SAKI_GYOUSHA_CD = GYO1.GYOUSHA_CD");
                // 運搬先業者2
                sql.Append(" LEFT JOIN M_GYOUSHA GYO2 ");
                sql.Append(" ON UPN2.UPN_SAKI_GYOUSHA_CD = GYO2.GYOUSHA_CD");
                // 運搬先業者1
                sql.Append(" LEFT JOIN M_GYOUSHA GYO3 ");
                sql.Append(" ON UPN3.UPN_SAKI_GYOUSHA_CD = GYO3.GYOUSHA_CD");
                // 最終処分業者
                sql.Append(" LEFT JOIN M_GYOUSHA GYO ");
                sql.Append(" ON TMD.LAST_SBN_GYOUSHA_CD = GYO.GYOUSHA_CD AND GYO.SHOBUN_NIOROSHI_GYOUSHA_KBN = 1");
                // 最終処分現場
                sql.Append(" LEFT JOIN M_GENBA GEN ");
                sql.Append(" ON TMD.LAST_SBN_GENBA_CD = GEN.GENBA_CD AND GEN.SAISHUU_SHOBUNJOU_KBN = 1");
                sql.Append(" AND TMD.LAST_SBN_GYOUSHA_CD = GEN.GYOUSHA_CD");
                // 廃棄物種類
                sql.Append(" LEFT JOIN M_HAIKI_SHURUI HAISHU ");
                sql.Append(" ON TMD.HAIKI_SHURUI_CD = HAISHU.HAIKI_SHURUI_CD ");
                sql.Append(" AND TME.HAIKI_KBN_CD = HAISHU.HAIKI_KBN_CD ");
                // 廃棄物
                sql.Append(" LEFT JOIN M_HAIKI_NAME HAIKI ");
                sql.Append(" ON TMD.HAIKI_NAME_CD = HAIKI.HAIKI_NAME_CD ");

                // 20140609 kayo 不具合No.4710 報告書分類を選択できるように修正 start‏
                // 報告書分類
                sql.Append(" LEFT JOIN M_HOUKOKUSHO_BUNRUI M_HOUKOKUSHO_BUNRUI ");
                sql.Append(" ON HAISHU.HOUKOKUSHO_BUNRUI_CD = M_HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_CD ");
                // 20140609 kayo 不具合No.4710 報告書分類を選択できるように修正 end
                // 荷姿
                sql.Append(" LEFT JOIN M_NISUGATA NISU ");
                sql.Append(" ON TMD.NISUGATA_CD = NISU.NISUGATA_CD ");
                // 返却日
                sql.Append(" LEFT JOIN T_MANIFEST_RET_DATE TMRD ");
                sql.Append(" ON TME.SYSTEM_ID = TMRD.SYSTEM_ID ");
                sql.Append(" AND TMRD.DELETE_FLG = 0 ");
                // 20140603 katen 不具合No.4131 start‏
                sql.Append("            LEFT JOIN T_MANIFEST_UPN AS TMU_CONDITION WITH(NOLOCK)");
                sql.Append("                   ON TME.SYSTEM_ID = TMU_CONDITION.SYSTEM_ID");
                sql.Append("                  AND TME.SEQ       = TMU_CONDITION.SEQ");
                sql.Append("            LEFT JOIN (SELECT T_MANIFEST_ENTRY.SYSTEM_ID,");
                sql.Append("                              T_MANIFEST_ENTRY.SEQ,");
                sql.Append("                              MAX(T_MANIFEST_UPN.UPN_ROUTE_NO) AS UPN_ROUTE_NO");
                sql.Append("                         FROM T_MANIFEST_ENTRY WITH(NOLOCK)");
                sql.Append("                   INNER JOIN T_MANIFEST_UPN WITH(NOLOCK)");
                sql.Append("                           ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_UPN.SYSTEM_ID");
                sql.Append("                          AND T_MANIFEST_ENTRY.SEQ       = T_MANIFEST_UPN.SEQ");
                sql.Append("                          AND T_MANIFEST_UPN.UPN_END_DATE IS NOT NULL");
                sql.Append("                        WHERE T_MANIFEST_ENTRY.DELETE_FLG = 'false'");
                sql.Append("                     GROUP BY T_MANIFEST_ENTRY.SYSTEM_ID,");
                sql.Append("                              T_MANIFEST_ENTRY.SEQ) TMU_SEARCH");
                sql.Append("                   ON TMU_CONDITION.SYSTEM_ID    = TMU_SEARCH.SYSTEM_ID");
                sql.Append("                  AND TMU_CONDITION.SEQ          = TMU_SEARCH.SEQ");
                sql.Append("                  AND TMU_CONDITION.UPN_ROUTE_NO = TMU_SEARCH.UPN_ROUTE_NO ");
                // 20140618 kayo 不具合No.4797 「5.全て」にて検索を行うと電マニ情報が表示されない start‏
                sql.AppendFormat(" WHERE TME.DELETE_FLG = '" + DELETE_FLG + "'");
                // 20140618 kayo 不具合No.4797 「5.全て」にて検索を行うと電マニ情報が表示されない end‏
                // 20140609 kayo 不具合No.4710 報告書分類を選択できるように修正 start‏
                //sql.Append("            LEFT JOIN M_HAIKI_SHURUI WITH(NOLOCK) ");
                //sql.Append("                   ON TMD.HAIKI_SHURUI_CD = M_HAIKI_SHURUI.HAIKI_SHURUI_CD ");
                // 20140609 kayo 不具合No.4710 報告書分類を選択できるように修正 end
                // 20140603 katen 不具合No.4131 end‏

                //処分事業場
                if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyugyobaNameCd.Text))
                {
                    //産廃（積替） 
                    sql.AppendFormat(" AND (");
                    sql.AppendFormat(" TME.HAIKI_KBN_CD <> 3 OR ( ");
                    sql.AppendFormat(" TME.HAIKI_KBN_CD = 3 AND ( ");
                    sql.AppendFormat(" TMU_CONDITION.UPN_SAKI_KBN = 1 ");
                    sql.AppendFormat(" AND TMU_CONDITION.UPN_SAKI_GENBA_CD = '{0}' ", this.form.cantxt_UnpanJyugyobaNameCd.Text);
                    sql.AppendFormat(" ) ");
                    sql.AppendFormat(" ) ");
                    sql.AppendFormat(" ) ");
                }
                string tsumikaehokanGyoushaCd = this.form.cantxt_TsumikaehokanGyoushaCd.Text;
                string tsumikaehokanGyoubaCd = this.form.cantxt_TsumikaehokanGyoubaCd.Text;
                if (!string.IsNullOrEmpty(tsumikaehokanGyoushaCd) || !string.IsNullOrEmpty(tsumikaehokanGyoubaCd))
                {
                    sql.AppendFormat(" AND ( ");
                    //産廃（直行）   
                    sql.AppendFormat("( TME.HAIKI_KBN_CD = 1 ");
                    //積替え保管業者                   
                    if (!string.IsNullOrEmpty(tsumikaehokanGyoushaCd))
                    {
                        sql.AppendFormat(" AND TME.TMH_GYOUSHA_CD = '{0}' ", tsumikaehokanGyoushaCd);
                    }
                    //積替え保管場
                    if (!string.IsNullOrEmpty(tsumikaehokanGyoubaCd))
                    {
                        sql.AppendFormat(" AND TME.TMH_GENBA_CD = '{0}' ", tsumikaehokanGyoubaCd);
                    }
                    sql.AppendFormat(" ) ");
                    //建廃
                    sql.AppendFormat(" OR ( TME.HAIKI_KBN_CD = 2 ");
                    //積替え保管業者                   
                    if (!string.IsNullOrEmpty(tsumikaehokanGyoushaCd))
                    {
                        sql.AppendFormat(" AND TME.TMH_GYOUSHA_CD = '{0}' ", tsumikaehokanGyoushaCd);
                    }
                    //積替え保管場
                    if (!string.IsNullOrEmpty(tsumikaehokanGyoubaCd))
                    {
                        sql.AppendFormat(" AND TME.TMH_GENBA_CD = '{0}' ", tsumikaehokanGyoubaCd);
                    }
                    sql.AppendFormat(" ) ");
                    //産廃（積替）
                    sql.AppendFormat(" OR ( TME.HAIKI_KBN_CD = 3 AND ");
                    sql.AppendFormat(" ( ( UPN1.UPN_SAKI_KBN = 2 ");
                    //積替え保管業者                   
                    if (!string.IsNullOrEmpty(tsumikaehokanGyoushaCd))
                    {
                        sql.AppendFormat(" AND UPN1.UPN_SAKI_GYOUSHA_CD = '{0}' ", tsumikaehokanGyoushaCd);
                    }
                    //積替え保管場
                    if (!string.IsNullOrEmpty(tsumikaehokanGyoubaCd))
                    {
                        sql.AppendFormat(" AND UPN1.UPN_SAKI_GENBA_CD = '{0}' ", tsumikaehokanGyoubaCd);
                    }
                    sql.AppendFormat(" ) ");
                    sql.AppendFormat(" OR ( UPN2.UPN_SAKI_KBN = 2 ");
                    //積替え保管業者                   
                    if (!string.IsNullOrEmpty(tsumikaehokanGyoushaCd))
                    {
                        sql.AppendFormat(" AND UPN2.UPN_SAKI_GYOUSHA_CD = '{0}' ", tsumikaehokanGyoushaCd);
                    }
                    //積替え保管場
                    if (!string.IsNullOrEmpty(tsumikaehokanGyoubaCd))
                    {
                        sql.AppendFormat(" AND UPN2.UPN_SAKI_GENBA_CD = '{0}' ", tsumikaehokanGyoubaCd);
                    }
                    sql.AppendFormat(" ) ");
                    sql.AppendFormat(" OR ( UPN3.UPN_SAKI_KBN = 2 ");
                    //積替え保管業者                   
                    if (!string.IsNullOrEmpty(this.form.cantxt_TsumikaehokanGyoushaCd.Text))
                    {
                        sql.AppendFormat(" AND UPN3.UPN_SAKI_GYOUSHA_CD = '{0}' ", this.form.cantxt_TsumikaehokanGyoushaCd.Text);
                    }
                    //積替え保管場
                    if (!string.IsNullOrEmpty(this.form.cantxt_TsumikaehokanGyoubaCd.Text))
                    {
                        sql.AppendFormat(" AND UPN3.UPN_SAKI_GENBA_CD = '{0}' ", this.form.cantxt_TsumikaehokanGyoubaCd.Text);
                    }
                    sql.AppendFormat(" ) ");
                    sql.AppendFormat(" OR ( ");
                    if (!string.IsNullOrEmpty(tsumikaehokanGyoushaCd))
                    {
                        sql.AppendFormat(" TME.TMH_GYOUSHA_CD = '{0}' ", tsumikaehokanGyoushaCd);
                    }
                    if (!string.IsNullOrEmpty(tsumikaehokanGyoubaCd))
                    {
                        sql.AppendFormat(" AND TME.TMH_GENBA_CD = '{0}' ", tsumikaehokanGyoubaCd);
                    }
                    sql.AppendFormat(" ) ");
                    sql.AppendFormat(" ) ");
                    sql.AppendFormat(" ) ");
                    sql.AppendFormat(" ) ");
                }
                #endregion
                // 20140603 katen 不具合No.4131 start‏
                if (string.IsNullOrEmpty(this.form.cantxt_TorihikiCd.Text))
                {
                    // 20140603 katen 不具合No.4131 end‏
                    sql.Append(" UNION ALL ");

                    #region 電マニ

                    //sql.Append(" SELECT DISTINCT ");
                    sql.Append(" SELECT ");

                    // マニ基本情報
                    sql.Append("   R18EX.SYSTEM_ID ");                                              // システムID
                    sql.Append(" , R18EX.SEQ ");                                                    // 枝番
                    sql.Append(" , DMT.LATEST_SEQ AS LATEST_SEQ ");                                 // 最終枝番
                    sql.Append(" , DMT.KANRI_ID AS KANRI_ID ");                                     // 管理ID
                    sql.Append(" , DMT.STATUS_FLAG AS TOC_STATUS_FLAG");
                    sql.Append(" , QUE2.STATUS_FLAG AS QUE_STATUS_FLAG");
                    sql.Append(" , 4 AS HAIKI_KBN_CD ");                                            // 廃棄区分CD
                    sql.AppendLine(",");
                    sql.AppendLine("CASE");
                    sql.AppendLine("    WHEN R18.FIRST_MANIFEST_FLAG IS NULL THEN 0");
                    sql.AppendLine("    WHEN R18.FIRST_MANIFEST_FLAG = '' THEN 0");
                    sql.AppendLine("    WHEN ISNULL(FIRST_HST_GYOUSHA.JISHA_KBN, 0) = 0 THEN 0");
                    sql.AppendLine("ELSE 1");
                    sql.AppendLine("END AS FIRST_MANIFEST_KBN,");                                   // 一次マニ区分
                    sql.Append(" CASE WHEN ISDATE(R18.HIKIWATASHI_DATE) = 1 THEN CONVERT(DATETIME, R18.HIKIWATASHI_DATE) ");
                    sql.Append(" ELSE NULL END AS KOUFU_DATE ");                                    // 交付年月日
                    sql.Append(" , NULL AS KOUFU_KBN ");                                            // 交付番号区分
                    sql.Append(" , R18.MANIFEST_ID ");                                              // 交付番号
                    sql.Append(" , NULL AS SEIRI_ID ");                                             // 整理番号
                    sql.Append(" , NULL AS KOUFU_TANTOUSHA ");                                      // 交付担当者
                    sql.Append(" , NULL AS KYOTEN_CD ");                                            // 拠点CD
                    sql.Append(" , NULL AS TORIHIKISAKI_CD ");                                      // 取引先CD
                    sql.Append(" , R18EX.HST_GYOUSHA_CD ");                                         // 排出事業者CD
                    sql.Append(" , R18.HST_SHA_NAME AS HST_GYOUSHA_NAME ");                         // 排出事業者名
                    sql.Append(" , R18.HST_SHA_POST AS HST_GYOUSHA_POST ");                         // 排出事業者郵便番号
                    sql.Append(" , R18.HST_SHA_TEL AS HST_GYOUSHA_TEL ");                           // 排出事業者電話番号
                    sql.Append(" , ISNULL(R18.HST_SHA_ADDRESS1, '') + ISNULL(R18.HST_SHA_ADDRESS2, '') ");
                    sql.Append(" + ISNULL(R18.HST_SHA_ADDRESS3, '') + ISNULL(R18.HST_SHA_ADDRESS4, '') ");
                    sql.Append(" AS HST_GYOUSHA_ADDRESS ");                                         // 排出事業者住所
                    sql.Append(" , R18EX.HST_GENBA_CD ");                                           // 排出事業場CD
                    sql.Append(" , R18.HST_JOU_NAME AS HST_GENBA_NAME ");                           // 排出事業場名
                    sql.Append(" , R18.HST_JOU_POST_NO AS HST_GENBA_POST ");                        // 排出事業者郵便番号
                    sql.Append(" , R18.HST_JOU_TEL AS HST_GENBA_TEL ");                             // 排出事業者電話番号
                    sql.Append(" , ISNULL(R18.HST_JOU_ADDRESS1, '') + ISNULL(R18.HST_JOU_ADDRESS2, '') ");
                    sql.Append(" + ISNULL(R18.HST_JOU_ADDRESS3, '') + ISNULL(R18.HST_JOU_ADDRESS4, '') ");
                    sql.Append(" AS HST_GENBA_ADDRESS ");                                           // 排出事業場住所

                    // 20140603 katen 不具合No.4131 start‏
                    //予約マニ⇒NULL(終了日無し)。他社運搬が無⇒交付年月日。最終運搬が他社⇒最終区間の運搬終了日。最終運搬が自社⇒最終他社運搬区間の運搬終了日
                    //sql.Append(" , CASE WHEN ISDATE(UPN_LAST.UPN_END_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, UPN_LAST.UPN_END_DATE) END ");
                    sql.Append(",CASE WHEN DMT.STATUS_FLAG != 4 THEN ");
                    sql.Append("           NULL");
                    sql.Append("      WHEN DT_R19_LAST_DIF.KANRI_ID IS NULL THEN ");
                    sql.Append("           CASE WHEN ISDATE(R18.HIKIWATASHI_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, R18.HIKIWATASHI_DATE) END ");
                    sql.Append("      WHEN UPN_LAST.UPN_ROUTE_NO = DT_R19_LAST_DIF.UPN_ROUTE_NO THEN ");
                    sql.Append("           CASE WHEN ISDATE(UPN_LAST.UPN_END_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, UPN_LAST.UPN_END_DATE) END ");
                    sql.Append( "     ELSE ");
                    sql.Append("           CASE WHEN ISDATE(DT_R19_LAST_DIF.UPN_END_DATE) = 0 THEN NULL ELSE CONVERT(DATETIME, DT_R19_LAST_DIF.UPN_END_DATE) END END");
                    sql.Append(" AS UPN_END_DATE ");                                                // 運搬終了年月日(検索用)
                    sql.Append(" , UPN_LAST_EX.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD ");              // 運搬受託者(検索用)
                    sql.Append(" , R18EX.SBN_GYOUSHA_CD AS SBN_JYUTAKUSHA_CD ");                    // 処分受託者(検索用)
                    sql.Append(" , UPN_LAST_EX.UPNSAKI_GENBA_CD ");                                 // 運搬先の事業場(検索用)
                    sql.Append(" , M_HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_CD ");                     // 報告書分類CD
                    sql.Append(" , R18EX.HAIKI_NAME_CD ");                                          // 廃棄物名称(検索用)
                    // 20140603 katen 不具合No.4131 end‏

                    // 収集運搬1
                    sql.Append(" , UPN_EX1.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD1 ");                    // 運搬業者CD
                    sql.Append(" , UPN1.UPN_SHA_NAME AS UPN_GYOUSHA_NAME1 ");                       // 運搬業者名
                    sql.Append(" , UPN1.UPN_SHA_POST AS UPN_GYOUSHA_POST1 ");                       // 運搬業者郵便番号
                    sql.Append(" , UPN1.UPN_SHA_TEL AS UPN_GYOUSHA_TEL1 ");                         // 運搬業者電話番号
                    sql.Append(" , ISNULL(UPN1.UPN_SHA_ADDRESS1, '') + ISNULL(UPN1.UPN_SHA_ADDRESS2, '') ");
                    sql.Append(" + ISNULL(UPN1.UPN_SHA_ADDRESS3, '') + ISNULL(UPN1.UPN_SHA_ADDRESS4, '') ");
                    sql.Append(" AS UPN_GYOUSHA_ADDRESS1 ");                                        // 運搬業者住所
                    sql.Append(" , NULL AS SHASHU_CD1 ");                                           // 車種CD
                    sql.Append(" , NULL AS INPUT_SHASHU_NAME1 ");                                   // 車種名(ダミー)
                    sql.Append(" , UPN_EX1.SHARYOU_CD AS SHARYOU_CD1 ");                            // 車輌CD
                    sql.Append(" , UPN1.CAR_NO AS SHARYOU_NAME1 ");                                 // 車輌名
                    sql.Append(" , UPN1.CAR_NO AS INPUT_SHARYOU_NAME1 ");                           // 車輌名(ダミー)
                    sql.Append(" , UPN1.UPN_WAY_CODE AS UPN_HOUHOU_CD1 ");                          // 運搬方法CD
                    sql.Append(" , CASE WHEN ISNULL(UPN1.UPNSAKI_JOU_KBN, 0) != 0 THEN CASE WHEN UPN1.UPNSAKI_JOU_KBN = 1 THEN 2 ELSE 1 END ELSE NULL END ");
                    sql.Append(" AS UPN_SAKI_KBN1 ");                                               // 運搬先区分
                    sql.Append(" , UPN_EX1.UPNSAKI_GYOUSHA_CD AS UPN_SAKI_GYOUSHA_CD1 ");           // 運搬先業者CD
                    sql.Append(" , UPN1.UPNSAKI_NAME AS UPN_SAKI_GYOUSHA_NAME1 ");                  // 運搬先業者名
                    sql.Append(" , UPN_EX1.UPNSAKI_GENBA_CD AS UPN_SAKI_GENBA_CD1 ");               // 運搬先現場CD
                    sql.Append(" , UPN1.UPNSAKI_JOU_NAME AS UPN_SAKI_GENBA_NAME1 ");                // 運搬先現場名
                    sql.Append(" , UPN1.UPNSAKI_JOU_POST AS UPN_SAKI_GENBA_POST1 ");                // 運搬先現場郵便番号
                    sql.Append(" , UPN1.UPNSAKI_JOU_TEL AS UPN_SAKI_GENBA_TEL1 ");                  // 運搬先現場電話番号
                    sql.Append(" , ISNULL(UPN1.UPNSAKI_JOU_ADDRESS1, '') + ISNULL(UPN1.UPNSAKI_JOU_ADDRESS2, '') ");
                    sql.Append(" + ISNULL(UPN1.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(UPN1.UPNSAKI_JOU_ADDRESS4, '') ");
                    sql.Append(" AS UPN_SAKI_GENBA_ADDRESS1 ");                                     // 運搬先住所
                    sql.Append(" , CASE WHEN DMT.STATUS_FLAG != 4 THEN NULL");
                    sql.Append("        WHEN ISNULL(UPN1.KANRI_ID,'') = '' THEN NULL");
                    sql.Append("        WHEN UPN1.UPN_SHA_EDI_MEMBER_ID != R18.HST_SHA_EDI_MEMBER_ID THEN ");
                    sql.Append("             CASE WHEN ISDATE(UPN1.UPN_END_DATE) = 1 THEN CONVERT(DATETIME, UPN1.UPN_END_DATE) ELSE NULL END");
                    sql.Append("        ELSE CASE WHEN ISDATE(R18.HIKIWATASHI_DATE) = 1 THEN CONVERT(DATETIME, R18.HIKIWATASHI_DATE) ELSE NULL END");
                    sql.Append(" END AS UPN_END_DATE1 ");                                 // 運搬終了年月日

                    // 収集運搬2
                    sql.Append(" , UPN_EX2.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD2 ");                    // 運搬業者CD
                    sql.Append(" , UPN2.UPN_SHA_NAME AS UPN_GYOUSHA_NAME2 ");                       // 運搬業者名
                    sql.Append(" , UPN2.UPN_SHA_POST AS UPN_GYOUSHA_POST2 ");                       // 運搬業者郵便番号
                    sql.Append(" , UPN2.UPN_SHA_TEL AS UPN_GYOUSHA_TEL2 ");                         // 運搬業者電話番号
                    sql.Append(" , ISNULL(UPN2.UPN_SHA_ADDRESS1, '') + ISNULL(UPN2.UPN_SHA_ADDRESS2, '') ");
                    sql.Append(" + ISNULL(UPN2.UPN_SHA_ADDRESS3, '') + ISNULL(UPN2.UPN_SHA_ADDRESS4, '') ");
                    sql.Append(" AS UPN_GYOUSHA_ADDRESS2 ");                                        // 運搬業者住所
                    sql.Append(" , NULL AS SHASHU_CD2 ");                                           // 車種CD
                    sql.Append(" , NULL AS INPUT_SHASHU_NAME2 ");                                   // 車種名(ダミー)
                    sql.Append(" , UPN_EX2.SHARYOU_CD AS SHARYOU_CD2 ");                            // 車輌CD
                    sql.Append(" , UPN2.CAR_NO AS SHARYOU_NAME2 ");                                 // 車輌名
                    sql.Append(" , UPN2.CAR_NO AS INPUT_SHARYOU_NAME2 ");                           // 車輌名(ダミー)
                    sql.Append(" , UPN2.UPN_WAY_CODE AS UPN_HOUHOU_CD2 ");                          // 運搬方法CD
                    sql.Append(" , CASE WHEN ISNULL(UPN2.UPNSAKI_JOU_KBN, 0) != 0 THEN CASE WHEN UPN2.UPNSAKI_JOU_KBN = 1 THEN 2 ELSE 1 END ELSE NULL END ");
                    sql.Append(" AS UPN_SAKI_KBN2 ");                                               // 運搬先区分
                    sql.Append(" , UPN_EX2.UPNSAKI_GYOUSHA_CD AS UPN_SAKI_GYOUSHA_CD2 ");           // 運搬先業者CD
                    sql.Append(" , UPN2.UPNSAKI_NAME AS UPN_SAKI_GYOUSHA_NAME2 ");                  // 運搬先業者名
                    sql.Append(" , UPN_EX2.UPNSAKI_GENBA_CD AS UPN_SAKI_GENBA_CD2 ");               // 運搬先現場CD
                    sql.Append(" , UPN2.UPNSAKI_JOU_NAME AS UPN_SAKI_GENBA_NAME2 ");                // 運搬先現場名
                    sql.Append(" , UPN2.UPNSAKI_JOU_POST AS UPN_SAKI_GENBA_POST2 ");                // 運搬先現場郵便番号
                    sql.Append(" , UPN2.UPNSAKI_JOU_TEL AS UPN_SAKI_GENBA_TEL2 ");                  // 運搬先現場電話番号
                    sql.Append(" , ISNULL(UPN2.UPNSAKI_JOU_ADDRESS1, '') + ISNULL(UPN2.UPNSAKI_JOU_ADDRESS2, '') ");
                    sql.Append(" + ISNULL(UPN2.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(UPN2.UPNSAKI_JOU_ADDRESS4, '') ");
                    sql.Append(" AS UPN_SAKI_GENBA_ADDRESS2 ");                                     // 運搬先住所
                    sql.Append(" , CASE WHEN DMT.STATUS_FLAG != 4 THEN NULL");
                    sql.Append("        WHEN ISNULL(UPN2.KANRI_ID,'') = '' THEN NULL");
                    sql.Append("        WHEN ISNULL(UPN22.KANRI_ID, '') = '' THEN ");
                    sql.Append("              CASE WHEN ISDATE(R18.HIKIWATASHI_DATE) = 1 THEN CONVERT(DATETIME, R18.HIKIWATASHI_DATE) ELSE NULL END");
                    sql.Append("        ELSE CASE WHEN ISDATE(UPN22.ENDDAY) = 1 THEN CONVERT(DATETIME, UPN22.ENDDAY) ELSE NULL END");
                    sql.Append(" END AS UPN_END_DATE2 ");                                 // 運搬終了年月日
                    // 収集運搬3
                    sql.Append(" , UPN_EX3.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD3 ");                    // 運搬業者CD
                    sql.Append(" , UPN3.UPN_SHA_NAME AS UPN_GYOUSHA_NAME3 ");                       // 運搬業者名
                    sql.Append(" , UPN3.UPN_SHA_POST AS UPN_GYOUSHA_POST3 ");                       // 運搬業者郵便番号
                    sql.Append(" , UPN3.UPN_SHA_TEL AS UPN_GYOUSHA_TEL3 ");                         // 運搬業者電話番号
                    sql.Append(" , ISNULL(UPN3.UPN_SHA_ADDRESS1, '') + ISNULL(UPN3.UPN_SHA_ADDRESS2, '') ");
                    sql.Append(" + ISNULL(UPN3.UPN_SHA_ADDRESS3, '') + ISNULL(UPN3.UPN_SHA_ADDRESS4, '') ");
                    sql.Append(" AS UPN_GYOUSHA_ADDRESS3 ");                                        // 運搬業者住所
                    sql.Append(" , NULL AS SHASHU_CD3 ");                                           // 車種CD
                    sql.Append(" , UPN_EX3.SHARYOU_CD AS SHARYOU_CD3 ");                            // 車輌CD
                    sql.Append(" , UPN3.CAR_NO AS SHARYOU_NAME3 ");                                 // 車輌名
                    sql.Append(" , UPN3.CAR_NO AS INPUT_SHARYOU_NAME3 ");                           // 車輌名(ダミー)
                    sql.Append(" , UPN3.UPN_WAY_CODE AS UPN_HOUHOU_CD3 ");                          // 運搬方法CD
                    sql.Append(" , CASE WHEN ISNULL(UPN3.UPNSAKI_JOU_KBN, 0) != 0 THEN CASE WHEN UPN3.UPNSAKI_JOU_KBN = 1 THEN 2 ELSE 1 END ELSE NULL END ");
                    sql.Append(" AS UPN_SAKI_KBN3 ");                                               // 運搬先区分
                    sql.Append(" , UPN_EX3.UPNSAKI_GYOUSHA_CD AS UPN_SAKI_GYOUSHA_CD3 ");           // 運搬先業者CD
                    sql.Append(" , UPN3.UPNSAKI_NAME AS UPN_SAKI_GYOUSHA_NAME3 ");                  // 運搬先業者名
                    sql.Append(" , UPN_EX3.UPNSAKI_GENBA_CD AS UPN_SAKI_GENBA_CD3 ");               // 運搬先現場CD
                    sql.Append(" , UPN3.UPNSAKI_JOU_NAME AS UPN_SAKI_GENBA_NAME3 ");                // 運搬先現場名
                    sql.Append(" , UPN3.UPNSAKI_JOU_POST AS UPN_SAKI_GENBA_POST3 ");                // 運搬先現場郵便番号
                    sql.Append(" , UPN3.UPNSAKI_JOU_TEL AS UPN_SAKI_GENBA_TEL3 ");                  // 運搬先現場電話番号
                    sql.Append(" , ISNULL(UPN3.UPNSAKI_JOU_ADDRESS1, '') + ISNULL(UPN3.UPNSAKI_JOU_ADDRESS2, '') ");
                    sql.Append(" + ISNULL(UPN3.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(UPN3.UPNSAKI_JOU_ADDRESS4, '') ");
                    sql.Append(" AS UPN_SAKI_GENBA_ADDRESS3 ");                                     // 運搬先住所
                    sql.Append(" , CASE WHEN DMT.STATUS_FLAG != 4 THEN NULL");
                    sql.Append("        WHEN ISNULL(UPN3.KANRI_ID, '') = '' THEN NULL");
                    sql.Append("        WHEN ISNULL(UPN33.KANRI_ID, '') = '' THEN ");
                    sql.Append("             CASE WHEN ISDATE(R18.HIKIWATASHI_DATE) = 1 THEN CONVERT(DATETIME, R18.HIKIWATASHI_DATE) ELSE NULL END");
                    sql.Append("        ELSE CASE WHEN ISDATE(UPN33.ENDDAY) = 1 THEN CONVERT(DATETIME, UPN33.ENDDAY) ELSE NULL END");
                    sql.Append(" END AS UPN_END_DATE3 ");                                 // 運搬終了年月日
                    // 処分受託者情報
                    sql.Append(" , R18EX.SBN_GYOUSHA_CD ");                                         // 処分業者CD
                    sql.Append(" , R18.SBN_SHA_NAME AS SBN_GYOUSHA_NAME ");                         // 処分業者名
                    sql.Append(" , R18.SBN_SHA_POST AS SBN_GYOUSHA_POST ");                         // 処分業者郵便番号
                    sql.Append(" , R18.SBN_SHA_TEL AS SBN_GYOUSHA_TEL ");                           // 処分業者電話番号
                    sql.Append(" , ISNULL(R18.SBN_SHA_ADDRESS1, '') + ISNULL(R18.SBN_SHA_ADDRESS2, '') ");
                    sql.Append(" + ISNULL(R18.SBN_SHA_ADDRESS3, '') + ISNULL(R18.SBN_SHA_ADDRESS4, '') ");
                    sql.Append(" AS SBN_GYOUSHA_ADDRESS ");                                         // 処分業者住所
                    sql.Append(" , CASE WHEN ISDATE(R18.SBN_END_DATE) = 1 THEN CONVERT(DATETIME, R18.SBN_END_DATE) ");
                    sql.Append(" ELSE NULL END AS SBN_END_DATE ");                                  // 処分終了年月日

                    // 積替保管情報
                    sql.Append(" , NULL AS TMH_GYOUSHA_CD ");                                        // 積保業者CD
                    sql.Append(" , NULL AS TMH_GYOUSHA_NAME ");                                      // 積保業者名
                    sql.Append(" , NULL AS TMH_GENBA_CD ");                                          // 積保現場CD
                    sql.Append(" , NULL AS TMH_GENBA_NAME ");                                        // 積保現場名
                    sql.Append(" , NULL AS TMH_GENBA_POST ");                                        // 積保現場郵便番号
                    sql.Append(" , NULL AS TMH_GENBA_TEL ");                                         // 積保現場電話番号
                    sql.Append(" , NULL AS TMH_GENBA_ADDRESS ");                                     // 積保現場住所

                    // 伝票には最終処分は不要
                    //// 最終処分情報
                    //sql.Append(" , TME.LAST_SBN_GYOUSHA_CD ");                                  // 最終処分業者CD
                    //sql.Append(" , GYO1.GYOUSHA_NAME_RYAKU AS LAST_SBN_GYOUSHA_NAME ");         // 最終処分業者名(業者マスタ)
                    //sql.Append(" , TME.LAST_SBN_GENBA_CD ");                                    // 最終処分現場CD
                    //sql.Append(" , TME.LAST_SBN_GENBA_NAME ");                                  // 最終処分現場名
                    //sql.Append(" , TME.LAST_SBN_GENBA_POST ");                                  // 最終処分郵便番号
                    //sql.Append(" , TME.LAST_SBN_GENBA_TEL ");                                   // 最終処分電話番号
                    //sql.Append(" , TME.LAST_SBN_GENBA_ADDRESS ");                               // 最終処分現場住所
                    //sql.Append(" , TME.LAST_SBN_GENBA_NUMBER ");                                // 最終処分現場番号
                    sql.Append(" , CASE WHEN ISDATE(R18.LAST_SBN_END_DATE) = 1 THEN CONVERT(DATETIME, R18.LAST_SBN_END_DATE) ");
                    sql.Append(" ELSE NULL END AS LAST_SBN_END_DATE ");                             // 最終処分終了年月日

                    // システム情報
                    sql.Append(" , R18EX.CREATE_USER ");                                            // 作成者
                    sql.Append(" , DMT.CREATE_DATE ");                                              // 作成日時
                    sql.Append(" , R18EX.CREATE_PC ");                                              // 作成PC
                    sql.Append(" , R18EX.UPDATE_USER ");                                            // 最終更新者
                    sql.Append(" , R18EX.UPDATE_DATE ");                                            // 最終更新日時
                    sql.Append(" , R18EX.UPDATE_PC ");                                              // 最終更新PC
                    sql.Append(" , R18EX.DELETE_FLG ");                                             // 削除フラグ

                    // 明細
                    sql.Append(" , R18EX.SYSTEM_ID AS DETAIL_SYSTEM_ID ");                          // 明細システムID
                    sql.Append(" , ISNULL(R18.HAIKI_DAI_CODE, '') + ISNULL(R18.HAIKI_CHU_CODE, '') ");
                    sql.Append(" + ISNULL(R18.HAIKI_SHO_CODE, '') + ISNULL(R18.HAIKI_SAI_CODE, '') ");
                    sql.Append(" AS DETAIL_HAIKI_SHURUI_CD ");                                      // 廃棄種類CD
                    sql.Append(" , R18.HAIKI_SHURUI AS DETAIL_HAIKI_SHURUI_NAME ");                 // 廃棄種類名
                    sql.Append(" , R18EX.HAIKI_NAME_CD AS DETAIL_HAIKI_NAME_CD ");                  // 廃棄物CD
                    sql.Append(" , R18.HAIKI_NAME AS DETAIL_HAIKI_NAME ");                          // 廃棄物名
                    // 20140609 kayo 不具合No.4710 報告書分類を選択できるように修正 start‏
                    sql.Append(" , M_HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_NAME_RYAKU ");             // 報告書分類
                    // 20140609 kayo 不具合No.4710 報告書分類を選択できるように修正 end‏
                    sql.Append(" , R18.NISUGATA_CODE AS DETAIL_NISUGATA_CD ");                      // 荷姿CD
                    sql.Append(" , R18.NISUGATA_NAME AS DETAIL_NISUGATA_NAME ");                    // 荷姿名
                    sql.Append(" , R18.HAIKI_SUU AS DETAIL_HAIKI_SUU ");                            // 数量
                    sql.Append(" , R18.HAIKI_UNIT_CODE AS DETAIL_HAIKI_UNIT_CD ");                  // 単位CD
                    sql.Append(" , R18EX.KANSAN_SUU AS DETAIL_KANSAN_SUU ");                        // 換算数量
                    sql.Append(" , R18EX.GENNYOU_SUU AS DETAIL_GENNYOU_SUU ");                      // 減容数
                    sql.Append(" , R18EX.SBN_HOUHOU_CD AS DETAIL_SBN_HOUHOU_CD ");                  // 処分方法CD
                    sql.Append(" , CASE WHEN ISDATE(R18.SBN_END_DATE) = 1 THEN CONVERT(DATETIME, R18.SBN_END_DATE) ");
                    sql.Append(" ELSE NULL END AS DETAIL_SBN_END_DATE ");                           // 処分終了年月日
                    sql.Append(" , CASE WHEN ISDATE(DT_R13.LAST_SBN_END_DATE) = 0 THEN NULL ");
                    sql.Append(" ELSE CONVERT(DATETIME, DT_R13.LAST_SBN_END_DATE) END AS DETAIL_LAST_SBN_END_DATE ");   // 最終処分終了年月日
                    sql.Append(" , DT_R13_EX.LAST_SBN_GYOUSHA_CD AS DETAIL_LAST_SBN_GYOUSHA_CD ");                      // 最終処分業者CD
                    sql.Append(" , M_DENSHI_JIGYOUSHA2.JIGYOUSHA_NAME AS DETAIL_LAST_SBN_GYOUSHA_NAME ");               // 最終処分業者名
                    sql.Append(" , DT_R13_EX.LAST_SBN_GENBA_CD AS DETAIL_LAST_SBN_GENBA_CD ");                          // 最終処分現場CD
                    sql.Append(" , DT_R13.LAST_SBN_JOU_NAME AS DETAIL_LAST_SBN_GENBA_NAME ");                           // 最終処分現場名
                    sql.Append(" , NIJI.MANIFEST_ID AS DETAIL_NIJI_MANIFEST_ID ");                  // 二次マニ交付番号
                    sql.Append(" , NULL AS SEND_A ");               // 返却日入力A票
                    sql.Append(" , NULL AS SEND_B1 ");               // 返却日入力B1票
                    sql.Append(" , NULL AS SEND_B2 ");               // 返却日入力B2票
                    sql.Append(" , NULL AS SEND_B4 ");               // 返却日入力B4票
                    sql.Append(" , NULL AS SEND_B6 ");               // 返却日入力B6票
                    sql.Append(" , NULL AS SEND_C1 ");               // 返却日入力C1票
                    sql.Append(" , NULL AS SEND_C2 ");               // 返却日入力C2票
                    sql.Append(" , NULL AS SEND_D ");               // 返却日入力D票
                    sql.Append(" , NULL AS SEND_E ");               // 返却日入力E票

                    // マニ目次
                    sql.Append(" FROM DT_MF_TOC DMT ");

                    // マニ情報
                    sql.Append(" INNER JOIN DT_R18 R18 ON DMT.KANRI_ID = R18.KANRI_ID AND DMT.LATEST_SEQ = R18.SEQ ");
                    // マニ情報拡張
                    // 20140610 katen 不具合No.4131 start‏
                    //sql.Append(" INNER JOIN DT_R18_EX R18EX ");
                    //sql.Append(" LEFT JOIN DT_R18_EX R18EX ");
                    sql.Append(" LEFT JOIN ");
                    sql.Append("( ");
                    sql.Append("SELECT ");
                    sql.Append("  R18EX.SYSTEM_ID ");
                    sql.Append(" ,R18EX.SEQ ");
                    sql.Append(" ,R18EX.KANRI_ID ");
                    sql.Append(" ,R18EX.MANIFEST_ID ");
                    sql.Append(" ,R18EX.HST_GYOUSHA_CD ");
                    sql.Append(" ,R18EX.HST_GENBA_CD ");
                    sql.Append(" ,R18EX.SBN_GYOUSHA_CD ");
                    sql.Append(" ,R18EX.SBN_GENBA_CD ");
                    sql.Append(" ,R18EX.NO_REP_SBN_EDI_MEMBER_ID ");
                    sql.Append(" ,R18EX.SBN_HOUHOU_CD ");
                    sql.Append(" ,R18EX.HOUKOKU_TANTOUSHA_CD ");
                    sql.Append(" ,R18EX.SBN_TANTOUSHA_CD ");
                    sql.Append(" ,R18EX.UPN_TANTOUSHA_CD ");
                    sql.Append(" ,R18EX.SHARYOU_CD ");
                    sql.Append(" ,R18EX.KANSAN_SUU ");
                    sql.Append(" ,CREATE_DATA.CREATE_USER ");
                    sql.Append(" ,R18EX.CREATE_DATE ");
                    sql.Append(" ,CREATE_DATA.CREATE_PC ");
                    sql.Append(" ,R18EX.UPDATE_USER ");
                    sql.Append(" ,R18EX.UPDATE_DATE ");
                    sql.Append(" ,R18EX.UPDATE_PC ");
                    sql.Append(" ,R18EX.DELETE_FLG ");
                    sql.Append(" ,R18EX.TIME_STAMP ");
                    sql.Append(" ,R18EX.HAIKI_NAME_CD ");
                    sql.Append(" ,R18EX.GENNYOU_SUU ");
                    sql.Append("FROM  ");
                    sql.Append("  DT_R18_EX R18EX ");
                    sql.Append(" ,( ");
                    sql.Append("   SELECT ");
                    sql.Append("    R18EX.SYSTEM_ID ");
                    sql.Append("   ,R18EX.SEQ ");
                    sql.Append("   ,R18EX.KANRI_ID ");
                    sql.Append("   ,R18EX.CREATE_USER ");
                    sql.Append("   ,R18EX.CREATE_DATE ");
                    sql.Append("   ,R18EX.CREATE_PC ");
                    sql.Append("  FROM ");
                    sql.Append("   DT_R18_EX R18EX ");
                    sql.Append("   ,(SELECT ");
                    sql.Append("      SYSTEM_ID ");
                    sql.Append("     ,MIN(SEQ) MIN_SEQ ");
                    sql.Append("     FROM ");
                    sql.Append("      DT_R18_EX ");
                    sql.Append("     GROUP BY  ");
                    sql.Append("      SYSTEM_ID ");
                    sql.Append("     ) SEQ_DATA ");
                    sql.Append("  WHERE ");
                    sql.Append("       R18EX.SYSTEM_ID = seq_data.SYSTEM_ID ");
                    sql.Append("   AND R18EX.SEQ = SEQ_DATA.MIN_SEQ ");
                    sql.Append("   ) CREATE_DATA ");
                    sql.Append("WHERE ");
                    sql.Append(" R18EX.SYSTEM_ID = CREATE_DATA.SYSTEM_ID ");
                    sql.Append(") R18EX ");

                    // 20140610 katen 不具合No.4131 end‏
                    sql.Append(" ON R18.KANRI_ID = R18EX.KANRI_ID ");
                    sql.Append(" AND R18EX.SYSTEM_ID = ( SELECT MAX(SYSTEM_ID) FROM DT_R18_EX TMP WHERE R18.KANRI_ID = TMP.KANRI_ID ) ");
                    sql.Append(" AND R18EX.SEQ = ( SELECT MAX(SEQ) FROM DT_R18_EX TMP1 WHERE TMP1.SYSTEM_ID = ( SELECT MAX(SYSTEM_ID) FROM DT_R18_EX TMP2 WHERE R18.KANRI_ID = TMP2.KANRI_ID )  ) ");

                    // 収集運搬1
                    sql.Append(" LEFT JOIN DT_R19 UPN1 ");
                    sql.Append(" ON R18.KANRI_ID = UPN1.KANRI_ID AND R18.SEQ = UPN1.SEQ ");
                    sql.Append(" AND UPN1.UPN_ROUTE_NO = 1 ");
                    // 収集運搬1拡張
                    // 20140610 katen 不具合No.4131 start‏
                    //sql.Append(" INNER JOIN DT_R19_EX UPN_EX1 ");
                    sql.Append(" LEFT JOIN DT_R19_EX UPN_EX1 ");
                    // 20140610 katen 不具合No.4131 end‏
                    sql.Append(" ON UPN1.KANRI_ID = UPN_EX1.KANRI_ID ");
                    sql.Append(" AND R18EX.SYSTEM_ID = UPN_EX1.SYSTEM_ID AND R18EX.SEQ = UPN_EX1.SEQ AND UPN_EX1.UPN_ROUTE_NO = 1 ");

                    // 収集運搬2
                    sql.Append(" LEFT JOIN DT_R19 UPN2 ");
                    sql.Append(" ON R18.KANRI_ID = UPN2.KANRI_ID AND R18.SEQ = UPN2.SEQ ");
                    sql.Append(" AND UPN2.UPN_ROUTE_NO = 2 ");
                    //自社排出・自社運搬の運搬終了日の取得
                    sql.Append(" LEFT JOIN (SELECT DT_R18.KANRI_ID, DT_R18.SEQ, MAX(UPN_ROUTE_NO) AS ROUTENO");
                    sql.Append("            FROM DT_MF_TOC");
                    sql.Append("            INNER JOIN DT_R18 ON DT_MF_TOC.KANRI_ID = DT_R18.KANRI_ID AND DT_MF_TOC.LATEST_SEQ = DT_R18.SEQ");
                    sql.Append("            LEFT JOIN DT_R19 ON DT_R18.KANRI_ID  = DT_R19.KANRI_ID AND DT_R18.SEQ = DT_R19.SEQ");
                    sql.Append("            WHERE DT_R19.UPN_ROUTE_NO <= 2 AND DT_R18.HST_SHA_EDI_MEMBER_ID != DT_R19.UPN_SHA_EDI_MEMBER_ID GROUP BY DT_R18.KANRI_ID,DT_R18.SEQ) UPN12");
                    sql.Append("            ON UPN2.KANRI_ID = UPN12.KANRI_ID AND UPN2.SEQ = UPN12.SEQ");
                    sql.Append(" LEFT JOIN (SELECT DT_R19.KANRI_ID, DT_R19.SEQ, DT_R19.UPN_ROUTE_NO, DT_R19.UPN_END_DATE AS ENDDAY");
                    sql.Append("                   FROM DT_MF_TOC toc INNER JOIN DT_R19 ON toc.KANRI_ID = DT_R19.KANRI_ID AND toc.LATEST_SEQ = DT_R19.SEQ) UPN22");
                    sql.Append("            ON UPN12.KANRI_ID  = UPN22.KANRI_ID AND UPN12.SEQ = UPN22.SEQ AND UPN12.ROUTENO = UPN22.UPN_ROUTE_NO");

                    // 収集運搬2拡張
                    // 20140610 katen 不具合No.4131 start‏
                    //sql.Append(" INNER JOIN DT_R19_EX UPN_EX2 ");
                    sql.Append(" LEFT JOIN DT_R19_EX UPN_EX2 ");
                    // 20140610 katen 不具合No.4131 end‏
                    sql.Append(" ON UPN2.KANRI_ID = UPN_EX2.KANRI_ID ");
                    sql.Append(" AND R18EX.SYSTEM_ID = UPN_EX2.SYSTEM_ID AND R18EX.SEQ = UPN_EX2.SEQ AND UPN_EX2.UPN_ROUTE_NO = 2 ");

                    // 収集運搬3
                    sql.Append(" LEFT JOIN DT_R19 UPN3 ");
                    sql.Append(" ON R18.KANRI_ID = UPN3.KANRI_ID AND R18.SEQ = UPN3.SEQ ");
                    sql.Append(" AND UPN3.UPN_ROUTE_NO = 3 ");
                    //自社排出・自社運搬の運搬終了日の取得
                    sql.Append(" LEFT JOIN (SELECT DT_R18.KANRI_ID, DT_R18.SEQ, MAX(UPN_ROUTE_NO) AS ROUTENO");
                    sql.Append("            FROM DT_MF_TOC");
                    sql.Append("            INNER JOIN DT_R18 ON DT_MF_TOC.KANRI_ID = DT_R18.KANRI_ID AND DT_MF_TOC.LATEST_SEQ = DT_R18.SEQ");
                    sql.Append("            LEFT JOIN DT_R19 ON DT_R18.KANRI_ID  = DT_R19.KANRI_ID AND DT_R18.SEQ = DT_R19.SEQ");
                    sql.Append("            WHERE DT_R19.UPN_ROUTE_NO <= 3 AND DT_R18.HST_SHA_EDI_MEMBER_ID != DT_R19.UPN_SHA_EDI_MEMBER_ID GROUP BY DT_R18.KANRI_ID,DT_R18.SEQ) UPN13");
                    sql.Append("            ON UPN3.KANRI_ID = UPN13.KANRI_ID AND UPN3.SEQ = UPN13.SEQ");
                    sql.Append(" LEFT JOIN (SELECT DT_R19.KANRI_ID, DT_R19.SEQ, DT_R19.UPN_ROUTE_NO, DT_R19.UPN_END_DATE AS ENDDAY");
                    sql.Append("                   FROM DT_MF_TOC toc INNER JOIN DT_R19 ON toc.KANRI_ID = DT_R19.KANRI_ID AND toc.LATEST_SEQ = DT_R19.SEQ ) UPN33");
                    sql.Append("            ON UPN13.KANRI_ID  = UPN33.KANRI_ID AND UPN13.SEQ = UPN33.SEQ AND UPN13.ROUTENO = UPN33.UPN_ROUTE_NO");

                    // 収集運搬3拡張
                    // 20140610 katen 不具合No.4131 start‏
                    //sql.Append(" INNER JOIN DT_R19_EX UPN_EX3 ");
                    sql.Append(" LEFT JOIN DT_R19_EX UPN_EX3 ");
                    // 20140610 katen 不具合No.4131 end‏
                    sql.Append(" ON UPN3.KANRI_ID = UPN_EX3.KANRI_ID ");
                    sql.Append(" AND R18EX.SYSTEM_ID = UPN_EX3.SYSTEM_ID AND R18EX.SEQ = UPN_EX3.SEQ AND UPN_EX3.UPN_ROUTE_NO = 3 ");

                    // 収集運搬（最終区間）
                    sql.Append(" LEFT JOIN DT_R19 UPN_LAST");
                    sql.Append(" ON DMT.KANRI_ID = UPN_LAST.KANRI_ID AND DMT.LATEST_SEQ = UPN_LAST.SEQ ");
                    sql.Append(" AND UPN_LAST.UPN_ROUTE_NO = (SELECT MAX(UPN_ROUTE_NO) FROM DT_R19 ");
                    sql.Append(" WHERE DMT.KANRI_ID = DT_R19.KANRI_ID AND DMT.LATEST_SEQ = DT_R19.SEQ) ");
                    // 収集運搬（最終区間）拡張
                    // 20140610 katen 不具合No.4131 start‏
                    //sql.Append(" INNER JOIN DT_R19_EX UPN_LAST_EX ");
                    sql.Append(" LEFT JOIN DT_R19_EX UPN_LAST_EX ");
                    // 20140610 katen 不具合No.4131 end‏
                    sql.Append(" ON UPN_LAST.KANRI_ID = UPN_LAST_EX.KANRI_ID ");
                    sql.Append(" AND R18EX.SYSTEM_ID = UPN_LAST_EX.SYSTEM_ID AND R18EX.SEQ = UPN_LAST_EX.SEQ ");
                    sql.Append(" AND UPN_LAST_EX.UPN_ROUTE_NO = UPN_LAST.UPN_ROUTE_NO ");

                    //他社区間マニ運搬情報（他社最終区間/DT_R19_LAST_DIF）
                    sql.Append(" LEFT JOIN (");
                    sql.Append(" SELECT DT_R19.KANRI_ID, DT_R19.SEQ, MAX(DT_R19.UPN_ROUTE_NO) RNO");
                    sql.Append(" FROM DT_MF_TOC");
                    sql.Append(" INNER JOIN DT_R18 ON DT_MF_TOC.KANRI_ID = DT_R18.KANRI_ID AND DT_MF_TOC.LATEST_SEQ = DT_R18.SEQ");
                    sql.Append(" INNER JOIN DT_R19 ON DT_MF_TOC.KANRI_ID = DT_R19.KANRI_ID AND DT_MF_TOC.LATEST_SEQ = DT_R19.SEQ");
                    sql.Append(" WHERE DT_R18.HST_SHA_EDI_MEMBER_ID != DT_R19.UPN_SHA_EDI_MEMBER_ID");
                    sql.Append(" GROUP BY DT_R19.KANRI_ID, DT_R19.SEQ) DT_R19_LAST_d");
                    sql.Append(" ON DT_R19_LAST_d.KANRI_ID = DMT.KANRI_ID AND DT_R19_LAST_d.SEQ = DMT.LATEST_SEQ");
                    sql.Append(" LEFT JOIN DT_R19 DT_R19_LAST_DIF ON DT_R19_LAST_d.KANRI_ID = DT_R19_LAST_DIF.KANRI_ID AND DT_R19_LAST_d.SEQ = DT_R19_LAST_DIF.SEQ AND DT_R19_LAST_DIF.UPN_ROUTE_NO = DT_R19_LAST_d.RNO");


                    // 最終処分情報
                    sql.Append(" LEFT JOIN DT_R13 ON DMT.KANRI_ID = DT_R13.KANRI_ID AND DMT.LATEST_SEQ = DT_R13.SEQ ");
                    sql.Append(" LEFT JOIN DT_R13_EX ");
                    sql.Append(" ON R18EX.KANRI_ID = DT_R13_EX.KANRI_ID AND R18EX.SYSTEM_ID = DT_R13_EX.SYSTEM_ID ");
                    sql.Append(" AND R18EX.SEQ = DT_R13_EX.SEQ AND DT_R13.REC_SEQ = DT_R13_EX.REC_SEQ ");
                    sql.Append(" LEFT JOIN M_DENSHI_JIGYOUSHA M_DENSHI_JIGYOUSHA2 ON DT_R13_EX.LAST_SBN_GYOUSHA_CD = M_DENSHI_JIGYOUSHA2.GYOUSHA_CD AND M_DENSHI_JIGYOUSHA2.SBN_KBN = 1 ");

                    // 一次排出事業者
                    sql.Append(" LEFT JOIN M_GYOUSHA FIRST_HST_GYOUSHA ");
                    sql.Append(" ON R18EX.HST_GYOUSHA_CD = FIRST_HST_GYOUSHA.GYOUSHA_CD ");

                    // 二次マニ
                    sql.Append(" LEFT JOIN T_MANIFEST_RELATION REL ");
                    sql.Append(" ON R18EX.SYSTEM_ID = REL.FIRST_SYSTEM_ID ");
                    sql.Append(" AND REL.DELETE_FLG = 0 ");
                    sql.Append(" AND REL.FIRST_HAIKI_KBN_CD = 4 ");
                    sql.Append(" AND REL.REC_SEQ = (SELECT MAX(TMP.REC_SEQ) FROM T_MANIFEST_RELATION TMP ");
                    sql.Append(" WHERE TMP.FIRST_SYSTEM_ID = REL.FIRST_SYSTEM_ID AND TMP.DELETE_FLG = 0 AND TMP.FIRST_HAIKI_KBN_CD = 4 ) ");
                    sql.Append(" LEFT JOIN (");
                    sql.Append(" SELECT DISTINCT");
                    sql.Append(" MR.NEXT_SYSTEM_ID AS SYSTEM_ID");
                    sql.Append(" ,MR.NEXT_HAIKI_KBN_CD AS HAIKI_KBN_CD");
                    sql.Append(" ,(CASE WHEN MR.NEXT_HAIKI_KBN_CD = 4 THEN EX.MANIFEST_ID ELSE ME.MANIFEST_ID END) AS MANIFEST_ID");
                    sql.Append(" FROM T_MANIFEST_RELATION AS MR");
                    sql.Append(" LEFT JOIN");
                    sql.Append(" (SELECT ET.*,TMD.DETAIL_SYSTEM_ID FROM T_MANIFEST_ENTRY ET ");
                    sql.Append(" LEFT JOIN T_MANIFEST_DETAIL TMD ON ET.SYSTEM_ID = TMD.SYSTEM_ID");
                    sql.Append(" WHERE DELETE_FLG = 0) AS ME");
                    sql.Append(" ON MR.NEXT_SYSTEM_ID = ME.DETAIL_SYSTEM_ID");
                    sql.Append(" AND MR.NEXT_HAIKI_KBN_CD = ME.HAIKI_KBN_CD");
                    sql.Append(" LEFT JOIN");
                    sql.Append(" (SELECT * FROM DT_R18_EX WHERE DELETE_FLG = 0) AS EX");
                    sql.Append(" ON MR.NEXT_SYSTEM_ID = EX.SYSTEM_ID");
                    sql.Append(" AND MR.NEXT_HAIKI_KBN_CD = 4");
                    sql.Append(" WHERE MR.DELETE_FLG = 0");
                    sql.Append(" ) AS NIJI");
                    sql.Append(" ON REL.NEXT_SYSTEM_ID = NIJI.SYSTEM_ID  ");
                    sql.Append(" AND REL.NEXT_HAIKI_KBN_CD = NIJI.HAIKI_KBN_CD");
                    // 20140611 katen 不具合No.4131 start‏
                    sql.Append(" LEFT OUTER JOIN M_DENSHI_HAIKI_SHURUI WITH (NOLOCK) ");
                    sql.Append(" ON R18.HAIKI_DAI_CODE + R18.HAIKI_CHU_CODE + R18.HAIKI_SHO_CODE = M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_CD ");
                    sql.Append(" LEFT OUTER JOIN M_HOUKOKUSHO_BUNRUI WITH (NOLOCK) ");
                    sql.Append(" ON M_DENSHI_HAIKI_SHURUI.HOUKOKUSHO_BUNRUI_CD = M_HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_CD ");

                    //QUE_INFO
                    //FUNCTION_ID(0101,0102,0501,0502)新規登録のデータの内、最終QUE_SEQを取得する
                    sql.Append(" LEFT JOIN ");
                    sql.Append(" (SELECT KANRI_ID, MAX(QUE_SEQ) QUE_SEQ FROM QUE_INFO ");
                    sql.Append(" WHERE FUNCTION_ID IN ('0101','0102','0501','0502')");
                    sql.Append(" GROUP BY KANRI_ID) QUE ON DMT.KANRI_ID = QUE.KANRI_ID ");
                    sql.Append(" LEFT JOIN ");
                    sql.Append(" (SELECT KANRI_ID, QUE_SEQ, STATUS_FLAG FROM QUE_INFO) QUE2 ON QUE.KANRI_ID = QUE2.KANRI_ID AND QUE.QUE_SEQ = QUE2.QUE_SEQ");

                    // 20140618 kayo 不具合No.4797 「5.全て」にて検索を行うと電マニ情報が表示されない start‏
                    sql.Append(" WHERE (DMT.KIND = 4 or DMT.KIND = 5 or DMT.KIND IS NULL) ");

                    //STATUS_FLAG
                    //1:予約未送信→一覧上は不要、報告時に失敗している(QUE：STATUS_FLAG = 8 or 9)可能性もあるデータ（長時間の放置データはJWNET送信が失敗している）
                    //2:マニ未送信→一覧上は不要、報告時に失敗している(QUE：STATUS_FLAG = 8 or 9)可能性もあるデータ（長時間の放置データはJWNET送信が失敗している）
                    //  1,2：新規登録の最終QUE_SEQの情報が保留削除なら、抽出対象外とする
                    //  STATUS_FLAG [0、1]:送信中　[2]:送信完了　[6]:保留削除　[7]:保留　[8、9]:エラー
                    //3:予約→必要
                    //4:マニ→必要
                    //9:取消済→不要
                    sql.Append(" AND ( ");
                    sql.Append("((DMT.STATUS_FLAG = 1 OR DMT.STATUS_FLAG = 2) AND QUE2.STATUS_FLAG <> 6 ) ");
                    sql.Append(" OR (DMT.STATUS_FLAG = 3 OR DMT.STATUS_FLAG = 4) ");
                    sql.Append(") ");
                    // 20140618 kayo 不具合No.4797 「5.全て」にて検索を行うと電マニ情報が表示されない end

                    // 20140611 katen 不具合No.4131 end‏
                    sql.Append(" AND R18EX.DELETE_FLG = 0");

                    // 処理区分項目が「未入力」、かつ年月日項目が「処分終了日」か「最終処分終了日」の場合は、
                    // 処分受託者が報告不要業者(EDI_MEMBER_ID = 0000000)の電マニ表示は行わない
                    if (this.form.txtNum_HimodukeJyoukyou.Text == "2")
                    {
                        if (KOUFU_DATE_KBN == "3" || KOUFU_DATE_KBN == "4")
                        {
                            sql.AppendFormat(" AND R18.SBN_SHA_MEMBER_ID <> '0000000'");
                        }
                    }

                    if (!string.IsNullOrEmpty(tsumikaehokanGyoushaCd) || !string.IsNullOrEmpty(tsumikaehokanGyoubaCd))
                    {
                        sql.AppendFormat(" AND ( ");
                        sql.AppendFormat(" ( UPN1.UPNSAKI_JOU_KBN = 1 ");
                        //積替え保管業者
                        if (!string.IsNullOrEmpty(tsumikaehokanGyoushaCd))
                        {
                            sql.AppendFormat(" AND UPN_EX1.UPNSAKI_GYOUSHA_CD = '{0}' ", tsumikaehokanGyoushaCd);
                        }
                        //積替え保管場
                        if (!string.IsNullOrEmpty(tsumikaehokanGyoubaCd))
                        {
                            sql.AppendFormat(" AND UPN_EX1.UPNSAKI_GENBA_CD = '{0}' ", tsumikaehokanGyoubaCd);
                        }
                        sql.AppendFormat(" ) ");
                        sql.AppendFormat(" OR ( UPN2.UPNSAKI_JOU_KBN = 1 ");
                        //積替え保管業者
                        if (!string.IsNullOrEmpty(tsumikaehokanGyoushaCd))
                        {
                            sql.AppendFormat(" AND UPN_EX2.UPNSAKI_GYOUSHA_CD = '{0}' ", tsumikaehokanGyoushaCd);
                        }
                        //積替え保管場
                        if (!string.IsNullOrEmpty(tsumikaehokanGyoubaCd))
                        {
                            sql.AppendFormat(" AND UPN_EX2.UPNSAKI_GENBA_CD = '{0}' ", tsumikaehokanGyoubaCd);
                        }
                        sql.AppendFormat(" ) ");
                        sql.AppendFormat(" OR ( UPN3.UPNSAKI_JOU_KBN = 1 ");
                        //積替え保管業者
                        if (!string.IsNullOrEmpty(tsumikaehokanGyoushaCd))
                        {
                            sql.AppendFormat(" AND UPN_EX3.UPNSAKI_GYOUSHA_CD = '{0}' ", tsumikaehokanGyoushaCd);
                        }
                        //積替え保管場
                        if (!string.IsNullOrEmpty(tsumikaehokanGyoubaCd))
                        {
                            sql.AppendFormat(" AND UPN_EX3.UPNSAKI_GENBA_CD = '{0}' ", tsumikaehokanGyoubaCd);
                        }
                        sql.AppendFormat(" ) ");
                        sql.AppendFormat(" ) ");
                    }
                    #endregion
                    // 20140603 katen 不具合No.4131 start‏
                }
                // 20140603 katen 不具合No.4131 end‏
                sql.Append(" ) AS SUMMARY ");

                sql.Append(this.joinQuery);

                #endregion

                #region WHERE句

                // 20140618 kayo 不具合No.4797 「5.全て」にて検索を行うと電マニ情報が表示されない start‏
                //sql.AppendFormat(" WHERE SUMMARY.DELETE_FLG = '" + DELETE_FLG + "'");
                sql.AppendFormat(" WHERE 1 = 1");
                // 20140618 kayo 不具合No.4797 「5.全て」にて検索を行うと電マニ情報が表示されない end

                //廃棄物区分CD
                switch (HAIKI_KBN_CD)
                {
                    case "1"://産廃（直行）
                        sql.Append(" AND SUMMARY.HAIKI_KBN_CD = 1 ");
                        break;
                    case "2"://産廃（積替）
                        sql.Append(" AND SUMMARY.HAIKI_KBN_CD = 3 ");
                        break;
                    case "3"://建廃
                        sql.Append(" AND SUMMARY.HAIKI_KBN_CD = 2 ");
                        break;
                    default:
                        break;
                }

                // 20140603 katen 不具合No.4131 start‏
                //if (KOUFU_DATE_KBN == "1")
                //{
                //    //交付年月日（開始）
                //    if (KOUFU_DATE_FROM != "")
                //    {
                //        sql.AppendFormat(" AND SUMMARY.KOUFU_DATE >= '{0}' ", KOUFU_DATE_FROM);
                //    }

                //    //交付年月日（終了）
                //    if (KOUFU_DATE_TO != "")
                //    {
                //        sql.AppendFormat(" AND SUMMARY.KOUFU_DATE <= '{0}' ", KOUFU_DATE_TO);
                //    }
                //}
                //else
                //{
                //    //交付年月日なし
                //    sql.Append(" AND SUMMARY.KOUFU_DATE IS NULL ");
                //}

                string columnName = "";
                //抽出日付区分
                switch (KOUFU_DATE_KBN)
                {
                    case "1":
                        //交付年月日
                        columnName = "SUMMARY.KOUFU_DATE";
                        break;
                    case "2":
                        //運搬終了日
                        columnName = "SUMMARY.UPN_END_DATE";
                        break;
                    case "3":
                        //処分終了日
                        columnName = "SUMMARY.DETAIL_SBN_END_DATE";
                        break;
                    case "4":
                        //最終処分終了日
                        columnName = "SUMMARY.DETAIL_LAST_SBN_END_DATE";
                        break;
                }
                StringBuilder dateCondition = new StringBuilder();
                if (this.form.txtNum_HimodukeJyoukyou.Text == "1" || this.form.txtNum_HimodukeJyoukyou.Text == "3")
                {
                    //処理区分が入力済または全ての場合
                    //年月日（開始）
                    if (KOUFU_DATE_FROM != "")
                    {
                        dateCondition.AppendFormat("                    {0} >= '{1}' ", new object[] { columnName, KOUFU_DATE_FROM });
                    }

                    //年月日（終了）
                    if (KOUFU_DATE_TO != "")
                    {
                        if (!string.IsNullOrEmpty(dateCondition.ToString()))
                        {
                            dateCondition.Append(" AND ");
                        }
                        dateCondition.AppendFormat("                    {0} <= '{1}' ", new object[] { columnName, KOUFU_DATE_TO });
                    }

                    // 20140610 katen 不具合No.4714 start‏
                    if (string.IsNullOrEmpty(dateCondition.ToString()) && this.form.txtNum_HimodukeJyoukyou.Text == "1")
                    {
                        //処理区分が入力済、そして年月日（開始）と年月日（終了）が入力しなかった場合
                        dateCondition.AppendFormat(" {0} IS NOT NULL", columnName);
                    }
                    // 20140610 katen 不具合No.4714 end‏
                }
                if (!string.IsNullOrEmpty(dateCondition.ToString()) && this.form.txtNum_HimodukeJyoukyou.Text == "3")
                {
                    //処理区分が全ての場合
                    dateCondition.Append(" OR ");
                }
                if (this.form.txtNum_HimodukeJyoukyou.Text == "2" ||
                   (this.form.txtNum_HimodukeJyoukyou.Text == "3" && !string.IsNullOrEmpty(dateCondition.ToString())))
                {
                    //処理区分が未入力または全ての場合
                    dateCondition.AppendFormat(" {0} IS NULL", columnName);
                }

                if (!string.IsNullOrEmpty(dateCondition.ToString()))
                {
                    sql.AppendFormat(" AND ( {0} ) ", dateCondition.ToString());
                }

                //取引先
                if (!string.IsNullOrEmpty(this.form.cantxt_TorihikiCd.Text))
                {
                    sql.AppendFormat(" AND SUMMARY.TORIHIKISAKI_CD = '{0}' ", this.form.cantxt_TorihikiCd.Text);
                }

                //排出事業者
                if (!string.IsNullOrEmpty(this.form.cantxt_HaisyutuGyousyaCd.Text))
                {
                    sql.AppendFormat(" AND SUMMARY.HST_GYOUSHA_CD = '{0}' ", this.form.cantxt_HaisyutuGyousyaCd.Text);
                }

                //排出事業場
                if (!string.IsNullOrEmpty(this.form.cantxt_HaisyutuJigyoubaName.Text))
                {
                    sql.AppendFormat(" AND SUMMARY.HST_GENBA_CD = '{0}' ", this.form.cantxt_HaisyutuJigyoubaName.Text);
                }

                //運搬受託者
                if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyutakuNameCd.Text))
                {
                    sql.AppendFormat(" AND SUMMARY.UPN_GYOUSHA_CD = '{0}' ", this.form.cantxt_UnpanJyutakuNameCd.Text);
                }

                //処分受託者
                if (!string.IsNullOrEmpty(this.form.cantxt_SyobunJyutakuNameCd.Text))
                {
                    sql.AppendFormat(" AND SUMMARY.SBN_GYOUSHA_CD = '{0}' ", this.form.cantxt_SyobunJyutakuNameCd.Text);
                }

                //処分事業場
                if (!string.IsNullOrEmpty(this.form.cantxt_UnpanJyugyobaNameCd.Text))
                {
                    sql.AppendFormat(" AND SUMMARY.UPN_SAKI_GENBA_CD = '{0}' ", this.form.cantxt_UnpanJyugyobaNameCd.Text);
                }

                //報告書分類
                if (!string.IsNullOrEmpty(this.form.cantxt_HokokushoBunrui.Text))
                {
                    sql.AppendFormat(" AND SUMMARY.HOUKOKUSHO_BUNRUI_CD = '{0}' ", this.form.cantxt_HokokushoBunrui.Text);
                }
                // 20140603 katen 不具合No.4131 end‏

                // システムID
                if (SYSTEM_ID != string.Empty)
                {
                    sql.AppendFormat(" AND SUMMARY.SYSTEM_ID = {0} ", SYSTEM_ID);
                }

                // 枝番
                if (!string.IsNullOrEmpty(SEQ))
                {
                    sql.AppendFormat(" AND SUMMARY.SEQ = {0} ", SEQ);
                }

                // 管理ID
                if (!string.IsNullOrEmpty(KANRI_ID))
                {
                    sql.AppendFormat(" AND SUMMARY.KANRI_ID = '{0}' ", KANRI_ID);
                }

                // 拠点CD
                if (!string.IsNullOrEmpty(this.header.KYOTEN_CD.Text) && this.header.KYOTEN_CD.Text != "99")
                {
                    sql.AppendFormat(" AND ( SUMMARY.KYOTEN_CD = {0} OR SUMMARY.HAIKI_KBN_CD = 4 ) ",
                        Int32.Parse(this.header.KYOTEN_CD.Text));
                }

                //マニフェスト一次区分
                sql.Append(" AND SUMMARY.FIRST_MANIFEST_KBN = " + (this.maniFlag == 1 ? "0" : "1") + " ");

                //マニフェスト／交付番号（開始）
                string Manifesutobangou_From = this.form.KOUFUBANNGOFrom.Text.ToString();
                if (!string.IsNullOrEmpty(Manifesutobangou_From))
                {
                    sql.Append(" AND RIGHT('00000000000' + ISNULL(SUMMARY.MANIFEST_ID,''), 11) >= '" + Manifesutobangou_From.PadLeft(11, '0') + "'");
                }
                //マニフェスト／交付番号（終了）
                string Manifesutobangou_To = this.form.KOUFUBANNGOTo.Text.ToString();
                if (!string.IsNullOrEmpty(Manifesutobangou_To))
                {
                    sql.Append(" AND RIGHT('00000000000' + ISNULL(SUMMARY.MANIFEST_ID,''), 11) <= '" + Manifesutobangou_To.PadLeft(11, '0') + "'");
                }

                #endregion

                #region ORDER BY句

                if (!string.IsNullOrEmpty(this.orderByQuery))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(this.orderByQuery);
                }

                #endregion

                this.createSql = sql.ToString();
                sql.Append("");

                if (string.IsNullOrEmpty(SYSTEM_ID))
                {
                    this.Search_TME = dao_GetTME.getdateforstringsql(this.createSql);
                    if (HAIKI_KBN_CD == "1" || HAIKI_KBN_CD == "3")
                    {
                        this.Search_TME = this.CorrectDetails(this.Search_TME);
                    }
                    count = this.Search_TME.Rows.Count;
                }
                else
                {
                    this.Search_TME_Check = dao_GetTME.getdateforstringsql(this.createSql);
                    if (HAIKI_KBN_CD == "1" || HAIKI_KBN_CD == "3")
                    {
                        this.Search_TME_Check = this.CorrectDetails(this.Search_TME_Check);
                    }
                    count = this.Search_TME_Check.Rows.Count;
                }

                return count;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }
        }


        /// <summary>
        /// 原本タブの内容をまとめる
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private DataTable CorrectDetails(DataTable baseDt)
        {
            int recPrt = baseDt.Columns.IndexOf(this.HIDDEN_PRT_REC);
            int recKei = baseDt.Columns.IndexOf(this.HIDDEN_KEIJYOU_REC);
            int recNis = baseDt.Columns.IndexOf(this.HIDDEN_NISUGATA_REC);
            int recChu = baseDt.Columns.IndexOf(this.HIDDEN_SBN_REC1);
            int recSai = baseDt.Columns.IndexOf(this.HIDDEN_SBN_REC2);

            int idxPrt = baseDt.Columns.IndexOf(this.PRT_DETAIL_COLUMN);
            int idxSysID = baseDt.Columns.IndexOf(this.HIDDEN_SYSTEM_ID);
            int idxSeq = baseDt.Columns.IndexOf(this.HIDDEN_SEQ);
            int idxDetID = baseDt.Columns.IndexOf(this.HIDDEN_DETAIL_SYSTEM_ID);

            int[] index = new[] { recPrt, recKei, recNis, recChu, recSai };

            if (new[] { idxPrt, idxSysID, idxSeq }.Contains(-1))
            {
                // いずれかが含まれていない場合は何もせず返す
                return baseDt;
            }
            else if (index.Count(n => n >= 0) < 1)
            {
                // 全て含まれていない場合は何もせず返す
                return baseDt;
            }

            // 新しいテーブル作成
            var newDt = baseDt.Clone();

            while (0 < baseDt.Rows.Count)
            {
                DataRow[] rows;

                if (idxDetID >= 0)
                {
                    // 元データテーブルから同じ明細のデータを抽出
                    rows = baseDt.Rows.Cast<DataRow>().Where(n =>
                        n[idxSysID].ToString() == baseDt.Rows[0][idxSysID].ToString() &&
                        n[idxSeq].ToString() == baseDt.Rows[0][idxSeq].ToString() &&
                        n[idxDetID].ToString() == baseDt.Rows[0][idxDetID].ToString()).ToArray();
                }
                else
                {
                    // 元データテーブルから同じ伝票のデータを抽出
                    rows = baseDt.Rows.Cast<DataRow>().Where(n =>
                        n[idxSysID].ToString() == baseDt.Rows[0][idxSysID].ToString() &&
                        n[idxSeq].ToString() == baseDt.Rows[0][idxSeq].ToString()).ToArray();
                }


                // 新しい行を作成（まとめる行以外は全て同じなはず）
                DataRow row = newDt.NewRow();
                for (int i = 0; i < row.ItemArray.Length; i++)
                {
                    row.SetField(i, rows[0][i]);
                }

                int currentIdx;
                string currentVal;

                // 印字明細
                currentIdx = baseDt.Columns.IndexOf(this.PRT_DETAIL_COLUMN);
                if (currentIdx >= 0)
                {
                    if (this.form.HAIKI_KBN_CD.Text.ToString() == "1")
                    {
                        currentVal = this.CorrectColumns(recPrt, currentIdx, rows, false);
                    }
                    else
                    {
                        currentVal = this.CorrectColumns(recPrt, currentIdx, rows, true);
                    }
                    
                    // 格納する値に合わせて列の最大長を調整
                    if (newDt.Columns[currentIdx].MaxLength < currentVal.Length)
                    {
                        newDt.Columns[currentIdx].MaxLength = currentVal.Length;
                    }
                    row.SetField(currentIdx, currentVal);
                }

                // 荷姿
                currentIdx = baseDt.Columns.IndexOf(this.NISUGATA_COLUMN);
                if (currentIdx >= 0)
                {
                    currentVal = this.CorrectColumns(recNis, currentIdx, rows, false);
                    // 格納する値に合わせて列の最大長を調整
                    if (newDt.Columns[currentIdx].MaxLength < currentVal.Length)
                    {
                        newDt.Columns[currentIdx].MaxLength = currentVal.Length;
                    }
                    row.SetField(currentIdx, currentVal);
                }

                // 形状
                currentIdx = baseDt.Columns.IndexOf(this.KEIJOU_COLUMN);
                if (currentIdx >= 0)
                {
                    currentVal = this.CorrectColumns(recKei, currentIdx, rows, false);
                    // 格納する値に合わせて列の最大長を調整
                    if (newDt.Columns[currentIdx].MaxLength < currentVal.Length)
                    {
                        newDt.Columns[currentIdx].MaxLength = currentVal.Length;
                    }
                    row.SetField(currentIdx, currentVal);
                }

                // 中間処分
                currentIdx = baseDt.Columns.IndexOf(this.SBN_HOUHOU_CHUKAN_COLUMN);
                if (currentIdx >= 0)
                {
                    currentVal = this.CorrectColumns(recChu, currentIdx, rows, false);
                    // 格納する値に合わせて列の最大長を調整
                    if (newDt.Columns[currentIdx].MaxLength < currentVal.Length)
                    {
                        newDt.Columns[currentIdx].MaxLength = currentVal.Length;
                    }
                    row.SetField(currentIdx, currentVal);
                }

                // 最終処分
                currentIdx = baseDt.Columns.IndexOf(this.SBN_HOUHOU_SAISHU_COLUMN);
                if (currentIdx >= 0)
                {
                    currentVal = this.CorrectColumns(recSai, currentIdx, rows, false);
                    // 格納する値に合わせて列の最大長を調整
                    if (newDt.Columns[currentIdx].MaxLength < currentVal.Length)
                    {
                        newDt.Columns[currentIdx].MaxLength = currentVal.Length;
                    }
                    row.SetField(currentIdx, currentVal);
                }

                // 新しいDataTableに追加
                newDt.Rows.Add(row);

                // 処理を終えた列を元データテーブルから削除する
                for (int k = 0; k < rows.Length; k++)
                {
                    baseDt.Rows.Remove(rows[k]);
                }
            }

            return newDt;
        }

        /// <summary>
        /// キー列名と値列名によって行データから
        /// 値列の文字列を結合して一つの文字列として返します。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private string CorrectColumns(int keyIdx, int valIdx, DataRow[] rows, bool isPrt)
        {
            LogUtility.DebugMethodStart(keyIdx, valIdx, rows, isPrt);

            var res = new StringBuilder();

            try
            {
                if (keyIdx < 0 || valIdx < 0)
                {
                    return res.ToString();
                }

                var got = new List<string>();
                for (int i = 0; i < rows.Length; i++)
                {
                    if (rows[i][keyIdx] == null || string.IsNullOrEmpty(rows[i][keyIdx].ToString()))
                    {
                        // キー列がnullまたは空だった場合は次へ
                        continue;
                    }
                    else if (got.Contains(rows[i][keyIdx].ToString()))
                    {
                        // 実施済みは次へ
                        continue;
                    }
                    else if (rows[i][valIdx] == null || string.IsNullOrEmpty(rows[i][valIdx].ToString()))
                    {
                        // 値列がnullまたは空だった場合は次へ
                        continue;
                    }

                    if (res.Length > 0)
                    {
                        res.Append("/");
                    }

                    if (isPrt)
                    {
                        res.Append(this.FormatManiSuuryou(rows[i][valIdx].ToString()));
                    }
                    else
                    {
                        res.Append(rows[i][valIdx].ToString());
                    }

                    // 取得済みリストに追加
                    got.Add(rows[i][keyIdx].ToString());
                }

                return res.ToString();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(res);
            }
        }

        /// <summary>
        /// マニ数量フォーマットを適用する
        /// </summary>
        /// <param name="val"></param>
        private string FormatManiSuuryou(string val)
        {
            string[] sub = val.Split(' ');
            if (sub.Length < 2)
            {
                return string.Empty;
            }

            decimal suuryou;
            decimal.TryParse(sub[sub.Length - 1], out suuryou);

            var sb = new StringBuilder();
            for (int i = 0; i < sub.Length - 1; i++)
            {
                sb.Append(sub[i]);
                sb.Append(" ");
            }
            sb.Append(suuryou.ToString(SystemProperty.Format.ManifestSuuryou));

            return sb.ToString();
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        public void Set_Search_TME()
        {
            LogUtility.DebugMethodStart();

            //初期化
            this.form.customDataGridView1.DataSource = null;
            this.form.customDataGridView1.Rows.Clear();
            this.form.customDataGridView1.Columns.Clear();

            this.form.Table = this.Search_TME;

            //一覧へデータをセット
            this.form.ShowData();

            this.HideColumnHeader();

            // "4.電マニ"を選択した場合しか"電子CSV"列を表示しないので、不要
            // "5.全て"選択時に"電子CSV"列を表示する場合は復活させる
            // this.ChackDenshiCSVColumn();

            //読込データ件数
            this.header.ReadDataNumber.Text = this.Search_TME.Rows.Count.ToString();
            //thongh 2015/09/14 #13032 start
            if (this.form.customDataGridView1 != null)
            {
                this.header.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.header.ReadDataNumber.Text = "0";
            }
            //thongh 2015/09/14 #13032 end

            //フォーカス初期化
            if (this.form.customDataGridView1.Columns.Count > 0 && this.form.customDataGridView1.Rows.Count > 0)
            {
                this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1[0, 0];
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 電子以外の電子CSVセルを空にします
        /// </summary>
        internal void ChackDenshiCSVColumn()
        {
            if ("5".Equals(this.form.HAIKI_KBN_CD.Text.ToString()))
            {
                for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                {
                    // 電子以外の場合
                    if (!"4".Equals(this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_HAIKI_KBN].Value.ToString()))
                    {
                        this.form.customDataGridView1.Rows[i].Cells["電子CSV"].ReadOnly = true;
                        DataGridViewCell textColumn = new DataGridViewTextBoxCell();
                        this.form.customDataGridView1.Rows[i].Cells["電子CSV"] = textColumn;
                        textColumn.Value = "";
                        textColumn.ReadOnly = true;
                        textColumn.UpdateBackColor(false);
                    }
                }
            }
        }

        /// <summary>
        /// データ整理
        /// </summary>
        private void DataSeiri()
        {
            // 運搬終了日列の整理
            if (this.Search_TME.Columns["運搬終了日"] != null) //パターン次第では列がない場合あり
            {
                this.Search_TME.Columns["運搬終了日"].ReadOnly = false;
                for (int i = 0; i < this.Search_TME.Rows.Count; i++)
                {
                    string upnDate = this.DbToString(this.Search_TME.Rows[i]["運搬終了日"]);
                    if (upnDate.Length > 0 && upnDate.Contains(","))
                    {
                        string rinnjiDate = string.Empty;
                        string[] arrUpnDate = upnDate.Split(',');
                        for (int j = 0; j < arrUpnDate.Length; j++)
                        {
                            if (!String.IsNullOrEmpty(arrUpnDate[j]))
                            {
                                rinnjiDate += arrUpnDate[j] + ",";
                            }
                        }
                        if (rinnjiDate.Length > 1)
                        {
                            rinnjiDate = rinnjiDate.Substring(0, rinnjiDate.Length - 1);
                        }
                        this.Search_TME.Rows[i]["運搬終了日"] = rinnjiDate;
                    }

                }
            }


            // 運搬受託者列の整理
            if (this.Search_TME.Columns["運搬受託者"] != null) //パターン次第では列がない場合あり
            {
                this.Search_TME.Columns["運搬受託者"].ReadOnly = false;
                for (int i = 0; i < this.Search_TME.Rows.Count; i++)
                {
                    string upnJutakusha = this.DbToString(this.Search_TME.Rows[i]["運搬受託者"]);
                    if (upnJutakusha.Length > 0 && upnJutakusha.Contains(","))
                    {
                        string rinnjiJutakusha = string.Empty;
                        string[] arrUpnJutakusha = upnJutakusha.Split(',');
                        for (int j = 0; j < arrUpnJutakusha.Length; j++)
                        {
                            if (!String.IsNullOrEmpty(arrUpnJutakusha[j]))
                            {
                                rinnjiJutakusha += arrUpnJutakusha[j] + ",";
                            }
                        }
                        if (rinnjiJutakusha.Length > 1)
                        {
                            rinnjiJutakusha = rinnjiJutakusha.Substring(0, rinnjiJutakusha.Length - 1);
                        }
                        this.Search_TME.Rows[i]["運搬受託者"] = rinnjiJutakusha;
                    }
                }
            }

        }

        /// <summary>
        /// 画面クリア
        /// </summary>
        public bool ClearScreen(String Kbn)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(Kbn);
                switch (Kbn)
                {
                    case "Initial"://初期表示
                        //タイトル
                        string titleText = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_MANIFEST_ICHIRAN);
                        this.header.lb_title.Text = titleText;

                        //拠点
                        this.header.KYOTEN_CD.Text = string.Empty;
                        this.header.KYOTEN_NAME.Text = string.Empty;
                        this.mlogic.SetKyoten(this.header.KYOTEN_CD, this.header.KYOTEN_NAME);

                        //廃棄物区分
                        // 20140530 katen 不具合No.4129 start‏
                        HaikiKbnCD = string.IsNullOrEmpty(this.form.fromKbn) ? "5" : this.form.fromKbn;
                        // 20140530 katen 不具合No.4129 end‏

                        //検索条件
                        this.form.searchString.Text = "";

                        //ヒント
                        this.footer.lb_hint.Text = "";

                        //処理No（ESC）
                        this.footer.txb_process.Text = "";

                        //2013-12-25 Add touti PT 電マニ No.620 横展開 追加 start
                        //並び順ソートヘッダー
                        this.form.customSortHeader1.ClearCustomSortSetting();
                        //2013-12-25 Add touti PT 電マニ No.620 横展開 追加 end
                        this.form.customSearchHeader1.ClearCustomSearchSetting();

                        break;

                    case "ClsSearchCondition"://検索条件をクリア
                        //アラート件数
                        this.header.NumberAlert = this.header.InitialNumberAlert;

                        //交付年月日
                        KoufuDateFrom = this.parentbaseform.sysDate.ToString();
                        KoufuDateTo = this.parentbaseform.sysDate.ToString();

                        //2013.11.23 naitou update 交付年月日区分の追加 start
                        //交付年月日区分
                        KoufuDateKbn = "1";
                        //2013.11.23 naitou update 交付年月日区分の追加 end

                        //廃棄物区分
                        //HaikiKbnCD = "1";
                        // 20140530 katen 不具合No.4129 start‏
                        HaikiKbnCD = string.IsNullOrEmpty(this.form.fromKbn) ? "5" : this.form.fromKbn;
                        // 20140530 katen 不具合No.4129 end‏

                        //一覧の項目を消去
                        this.Search_TME.Clear();

                        //2013-12-25 Add touti PT 電マニ No.620 横展開 追加 start
                        //並び順ソートヘッダー
                        this.form.customSortHeader1.ClearCustomSortSetting();
                        //2013-12-25 Add touti PT 電マニ No.620 横展開 追加 end
                        this.form.customSearchHeader1.ClearCustomSearchSetting();

                        break;
                }

                //読込データ件数
                this.header.ReadDataNumber.Text = "0";

                //アラート件数
                this.header.AlertNumber.Text = this.header.NumberAlert.ToString();

                //交付年月日
                switch (KoufuDateFrom)
                {
                    case "":
                        this.form.KOUFU_DATE_FROM.Text = KoufuDateFrom;
                        break;

                    default:
                        this.form.KOUFU_DATE_FROM.Value = DateTime.Parse(KoufuDateFrom);
                        break;
                }

                switch (KoufuDateTo)
                {
                    case "":
                        this.form.KOUFU_DATE_TO.Text = KoufuDateTo;
                        break;

                    default:
                        this.form.KOUFU_DATE_TO.Value = DateTime.Parse(KoufuDateTo);
                        break;
                }

                //2013.11.23 naitou update 交付年月日区分の追加 start
                //交付年月日区分
                if (String.IsNullOrEmpty(KoufuDateKbn))
                {
                    KoufuDateKbn = "1";
                }
                this.form.KOUFU_DATE_KBN.Text = KoufuDateKbn;
                //2013.11.23 naitou update 交付年月日区分の追加 end

                // 20140603 katen 不具合No.4131 start‏
                //処理区分
                this.form.txtNum_HimodukeJyoukyou.Text = "1";
                //取引先
                this.form.cantxt_TorihikiCd.Text = string.Empty;
                this.form.ctxt_TorihikiName.Text = string.Empty;
                //排出事業者
                this.form.cantxt_HaisyutuGyousyaCd.Text = string.Empty;
                this.form.ctxt_HaisyutuGyousyaName.Text = string.Empty;
                //排出事業場
                this.form.cantxt_HaisyutuJigyoubaName.Text = string.Empty;
                this.form.ctxt_HaisyutuJigyoubaName.Text = string.Empty;
                //運搬受託者
                this.form.cantxt_UnpanJyutakuNameCd.Text = string.Empty;
                this.form.ctxt_UnpanJyutakuName.Text = string.Empty;
                //処分受託者
                this.form.cantxt_SyobunJyutakuNameCd.Text = string.Empty;
                this.form.ctxt_SyobunJyutakuName.Text = string.Empty;
                //運搬先の事業場
                this.form.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
                this.form.ctxt_UnpanJyugyobaName.Text = string.Empty;
                //積替え保管業者
                this.form.cantxt_TsumikaehokanGyoushaCd.Text = string.Empty;
                this.form.cantxt_TsumikaehokanGyoushaName.Text = string.Empty;
                //積替え保管場
                this.form.cantxt_TsumikaehokanGyoubaCd.Text = string.Empty;
                this.form.cantxt_TsumikaehokanGyoubaName.Text = string.Empty;
                //報告書分類
                this.form.cantxt_HokokushoBunrui.Text = string.Empty;
                this.form.ctxt_HokokushoBunrui.Text = string.Empty;
                //廃棄物種類
                this.form.cantxt_HaikibutuShurui.Text = string.Empty;
                this.form.ctxt_HaikibutuShurui.Text = string.Empty;
                //廃棄物名称
                this.form.cantxt_HaikibutuName.Text = string.Empty;
                this.form.ctxt_HaikibutuName.Text = string.Empty;

                // F7 検索条件クリアでは一次二次を変更しない
                if (Kbn != "ClsSearchCondition")
                {
                    this.maniFlag = this.form.fromManiFlag;
                    if (!this.SetManifestFrom("Non")) { return false; }
                }

                // 20140603 katen 不具合No.4131 end‏

                //廃棄物区分
                if (String.IsNullOrEmpty(HaikiKbnCD))
                {
                    //HaikiKbnCD = "1";
                    //廃棄物区分を「5:全て」に初期化
                    HaikiKbnCD = "5";
                }
                this.form.HAIKI_KBN_CD.Text = HaikiKbnCD;

                this.form.label17.BackColor = this.form.BackColor;

                this.form.KOUFUBANNGOFrom.Text = string.Empty;
                this.form.KOUFUBANNGOTo.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearScreen", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart();

            try
            {

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
        /// 画面遷移
        /// </summary>
        public void FormChanges(WINDOW_TYPE WindowType)
        {
            LogUtility.DebugMethodStart();

            String kanriId = string.Empty;
            String latestSeq = string.Empty;
            try
            {
                #region 新規　暫定
                if (WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    // 全てが選択される場合
                    if ("5".Equals(this.form.HAIKI_KBN_CD.Text))
                    {
                        MessageBoxUtility.MessageBoxShow("E051", "廃棄物区分は、「5.全て」以外の区分");
                        this.form.HAIKI_KBN_CD.Focus();
                        return;
                    }
                    //画面起動
                    this.form.ParamOut_WinType = (int)WindowType;
                    this.form.ParamOut_SysID = string.Empty;

                    switch (this.form.HAIKI_KBN_CD.Text)
                    {
                        case "1"://G119 産廃（直行）マニフェスト一覧
                            FormManager.OpenFormWithAuth("G119", WindowType, WindowType, "", this.form.ParamOut_SysID, "", this.form.ParamOut_WinType);
                            break;

                        case "2"://G120 産廃（積替）マニフェスト一覧
                            FormManager.OpenFormWithAuth("G120", WindowType, WindowType, "", this.form.ParamOut_SysID, "", this.form.ParamOut_WinType);
                            break;

                        case "3"://G121 建廃マニフェスト一覧
                            FormManager.OpenFormWithAuth("G121", WindowType, WindowType, "", this.form.ParamOut_SysID, "", this.form.ParamOut_WinType);
                            break;

                        case "4"://電子
                            FormManager.OpenFormWithAuth("G141", WindowType, WindowType, kanriId, latestSeq);
                            break;
                    }
                    return;
                }
                #endregion


                string koufuDateFrom = this.form.KOUFU_DATE_FROM.Value == null ? string.Empty : this.form.KOUFU_DATE_FROM.Value.ToString();
                string koufuDateTo = this.form.KOUFU_DATE_TO.Value == null ? string.Empty : this.form.KOUFU_DATE_TO.Value.ToString();


                //検索結果(マニフェストパターン)が1件もない場合
                if (this.form.customDataGridView1.Rows.Count <= 0)
                {
                    switch (WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG://新規
                            // 全てが選択される場合
                            if ("5".Equals(this.form.HAIKI_KBN_CD.Text))
                            {
                                MessageBoxUtility.MessageBoxShow("E051", "廃棄物区分は、「5.全て」以外の区分");
                                this.form.HAIKI_KBN_CD.Focus();
                                return;
                            }
                            //画面起動
                            this.form.ParamOut_WinType = (int)WindowType;
                            this.form.ParamOut_SysID = string.Empty;

                            switch (this.form.HAIKI_KBN_CD.Text)
                            {
                                case "1"://G119 産廃（直行）マニフェスト一覧
                                    FormManager.OpenFormWithAuth("G119", WindowType, WindowType, "", this.form.ParamOut_SysID, "", this.form.ParamOut_WinType);
                                    break;

                                case "2"://G120 産廃（積替）マニフェスト一覧
                                    FormManager.OpenFormWithAuth("G120", WindowType, WindowType, "", this.form.ParamOut_SysID, "", this.form.ParamOut_WinType);
                                    break;

                                case "3"://G121 建廃マニフェスト一覧
                                    FormManager.OpenFormWithAuth("G121", WindowType, WindowType, "", this.form.ParamOut_SysID, "", this.form.ParamOut_WinType);
                                    break;

                                case "4"://電子
                                    FormManager.OpenFormWithAuth("G141", WindowType, WindowType, kanriId, latestSeq);
                                    break;
                            }
                            return;
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正
                            MessageBoxUtility.MessageBoxShow("E029", "修正するマニフェスト", "マニフェスト一覧");
                            //break;
                            return;

                        case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除
                            MessageBoxUtility.MessageBoxShow("E029", "削除するマニフェスト", "マニフェスト一覧");
                            //break;
                            return;
                    }
                }

                //画面で行が選択されていない場合
                //if (this.form.customDataGridView1.Rows.Count > 0
                //    && this.form.customDataGridView1.Rows[0].Cells[0].Selected)
                if (this.form.customDataGridView1.Rows.Count > 0
                    && this.form.customDataGridView1.CurrentRow == null)
                {
                    switch (WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG://新規
                            MessageBoxUtility.MessageBoxShow("E029", "追加するマニフェスト", "マニフェスト一覧");
                            //break ;
                            return;

                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正
                            MessageBoxUtility.MessageBoxShow("E029", "修正するマニフェスト", "マニフェスト一覧");
                            //break;
                            return;

                        case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除
                            MessageBoxUtility.MessageBoxShow("E029", "削除するマニフェスト", "マニフェスト一覧");
                            //break;
                            return;
                    }
                    return;
                }

                Int32 count_TME = 0;
                int i = this.form.customDataGridView1.CurrentRow.Index;

                switch (Convert.ToString(this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_HAIKI_KBN].Value))
                {
                    case "1"://産廃（直行）
                    case "2"://産廃（積替）
                    case "3"://建廃

                        //SYSTEM_IDが取得できない場合。
                        if (string.IsNullOrEmpty(Convert.ToString(this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_SYSTEM_ID].Value)) ||
                        string.IsNullOrEmpty(Convert.ToString(this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_SEQ].Value)))
                        {
                            switch (WindowType)
                            {
                                case WINDOW_TYPE.NEW_WINDOW_FLAG://新規
                                    break;

                                case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正
                                case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除
                                    MessageBoxUtility.MessageBoxShow("E045");
                                    return;
                            }
                        }

                        //SYSTEM_IDが取得できた場合の存在チェック。
                        count_TME = this.Get_Search_TME(
                            koufuDateFrom,
                            koufuDateTo,
                            this.form.HAIKI_KBN_CD.Text.ToString(),
                            "false",
                            Convert.ToString(this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_SYSTEM_ID].Value),
                            "", //SEQは無しで検索すること(一覧→更新→そのまま一覧から参照　で開けないのを回避するため）
                            this.form.KOUFU_DATE_KBN.Text.ToString(), //2013.11.23 naitou update 交付年月日区分の追加
                            "",
                            "");
                        if (count_TME <= 0)
                        {
                            switch (WindowType)
                            {
                                case WINDOW_TYPE.NEW_WINDOW_FLAG://新規
                                    break;

                                case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正
                                case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除
                                    MessageBoxUtility.MessageBoxShow("E045");
                                    return;
                            }
                        }
                        break;

                    case "4"://電子
                        //KANRI_IDが取得できない場合。
                        if (string.IsNullOrEmpty(Convert.ToString(this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_KANRI_ID].Value)) ||
                        string.IsNullOrEmpty(Convert.ToString(this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_LATEST_SEQ].Value)))
                        {
                            switch (WindowType)
                            {
                                case WINDOW_TYPE.NEW_WINDOW_FLAG://新規
                                    break;

                                case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正
                                case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除
                                    MessageBoxUtility.MessageBoxShow("E045");
                                    return;
                            }
                        }

                        //KANRI_IDが取得できた場合の存在チェック。
                        count_TME = this.Get_Search_TME(
                            koufuDateFrom,
                            koufuDateTo,
                            this.form.HAIKI_KBN_CD.Text.ToString(),
                            "false",
                            "",
                            "",
                            this.form.KOUFU_DATE_KBN.Text.ToString(), //2013.11.23 naitou update 交付年月日区分の追加
                            this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_KANRI_ID].Value.ToString(),
                            "");
                        if (count_TME <= 0)
                        {
                            switch (WindowType)
                            {
                                case WINDOW_TYPE.NEW_WINDOW_FLAG://新規
                                    break;

                                case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正
                                case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除
                                    MessageBoxUtility.MessageBoxShow("E045");
                                    return;
                            }
                        }
                        break;
                }

                //画面起動
                this.form.ParamOut_WinType = (int)WindowType;
                this.form.ParamOut_SysID = this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_SYSTEM_ID].Value.ToString();
                kanriId = this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_KANRI_ID].Value.ToString();
                latestSeq = this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_LATEST_SEQ].Value.ToString().Trim();
                string haikiKbn = this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_HAIKI_KBN].Value.ToString();

                var winType = WINDOW_TYPE.NONE;
                var paramOutWinType = (int)WindowType;
                switch (haikiKbn)
                {
                    case "1"://G119 産廃（直行）マニフェスト一覧
                        if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == WindowType)
                        {
                            // 修正権限の場合、参照権限降格も含めチェック
                            if (Manager.CheckAuthority("G119", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                            {
                                winType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                            }
                            else if (Manager.CheckAuthority("G119", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                            {
                                winType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                                paramOutWinType = (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                            }
                            else
                            {
                                var messageShowLogic = new MessageBoxShowLogic();
                                messageShowLogic.MessageBoxShow("E158", "修正");

                                break;
                            }
                        }
                        else
                        {
                            winType = WindowType;
                        }

                        FormManager.OpenFormWithAuth("G119", winType, winType, "", this.form.ParamOut_SysID, "", paramOutWinType);
                        break;

                    case "2"://G121 建廃マニフェスト一覧
                        if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == WindowType)
                        {
                            // 修正権限の場合、参照権限降格も含めチェック
                            if (Manager.CheckAuthority("G121", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                            {
                                winType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                            }
                            else if (Manager.CheckAuthority("G121", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                            {
                                winType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                                paramOutWinType = (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                            }
                            else
                            {
                                var messageShowLogic = new MessageBoxShowLogic();
                                messageShowLogic.MessageBoxShow("E158", "修正");

                                break;
                            }
                        }
                        else
                        {
                            winType = WindowType;
                        }

                        FormManager.OpenFormWithAuth("G121", winType, winType, "", this.form.ParamOut_SysID, "", paramOutWinType);
                        break;

                    case "3"://G120 産廃（積替）マニフェスト一覧
                        if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == WindowType)
                        {
                            // 修正権限の場合、参照権限降格も含めチェック
                            if (Manager.CheckAuthority("G120", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                            {
                                winType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                            }
                            else if (Manager.CheckAuthority("G120", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                            {
                                winType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                                paramOutWinType = (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                            }
                            else
                            {
                                var messageShowLogic = new MessageBoxShowLogic();
                                messageShowLogic.MessageBoxShow("E158", "修正");

                                break;
                            }
                        }
                        else
                        {
                            winType = WindowType;
                        }

                        FormManager.OpenFormWithAuth("G120", winType, winType, "", this.form.ParamOut_SysID, "", paramOutWinType);
                        break;

                    case "4"://電子
                        if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == WindowType)
                        {
                            // 修正権限の場合、参照権限降格も含めチェック
                            if (Manager.CheckAuthority("G141", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                            {
                                winType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                            }
                            else if (Manager.CheckAuthority("G141", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                            {
                                winType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                            }
                            else
                            {
                                var messageShowLogic = new MessageBoxShowLogic();
                                messageShowLogic.MessageBoxShow("E158", "修正");

                                break;
                            }
                        }
                        else
                        {
                            winType = WindowType;
                        }

                        //現状のステータスで画面移動する為、存在チェックで取得したデータで、チェックを行う
                        //先述のKANRI_IDの存在チェックで、削除されたマニフェスト
                        if (Convert.ToString(this.Search_TME.Rows[0]["HIDDEN_TOC_STATUS_FLAG"]).Equals("3")
                            || Convert.ToString(this.Search_TME.Rows[0]["HIDDEN_TOC_STATUS_FLAG"]).Equals("4"))
                        {
                            //TOCのステータスが、予約/マニ登録済(3,4)のデータ⇒通常通り
                            FormManager.OpenFormWithAuth("G141", winType, winType, kanriId, string.Empty);
                        }
                        else
                        {
                            //TOCのステータスが、新規予約/マニ登録中(1,2)のデータ
                            if (Convert.ToString(this.Search_TME.Rows[0]["HIDDEN_QUE_STATUS_FLAG"]).Equals("0")
                                || Convert.ToString(this.Search_TME.Rows[0]["HIDDEN_QUE_STATUS_FLAG"]).Equals("1")
                                || Convert.ToString(this.Search_TME.Rows[0]["HIDDEN_QUE_STATUS_FLAG"]).Equals("2"))
                            {
                                //QUEのステータスが、送信処理中(0,1,2)⇒参照モード
                                FormManager.OpenFormWithAuth("G141", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, kanriId, string.Empty);
                            }
                            else
                            {
                                //QUEのステータスが、保留/JWNETエラー(7,8,9)⇒権限で切り分ける
                                switch (winType)
                                {
                                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                                        //F3修正⇒新規/修正権限あり⇒新規モード(G142からの処理と同じ)で開く
                                        //送信保留も同じ引数で、マニ入力画面へ遷移しているので注意！！
                                        if (Convert.ToString(this.Search_TME.Rows[0]["HIDDEN_QUE_STATUS_FLAG"]).Equals("8")
                                            || Convert.ToString(this.Search_TME.Rows[0]["HIDDEN_QUE_STATUS_FLAG"]).Equals("9"))
                                        {
                                            MessageBoxUtility.MessageBoxShowInformation("JWNET登録が失敗したマニフェストになります。\r\n再度登録を行う場合は、通信履歴照会画面よりエラー内容の確認を行い登録してください。");
                                        }
                                        FormManager.OpenFormWithAuth("G141", WINDOW_TYPE.NEW_WINDOW_FLAG, winType, kanriId, string.Empty, null, null, null, null, true);
                                        break;
                                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                                        //F4削除⇒削除権限あり⇒削除モード(G142から流用)で開く
                                        //送信保留も同じ引数で、マニ入力画面へ遷移しているので注意！！
                                        FormManager.OpenFormWithAuth("G141", WINDOW_TYPE.DELETE_WINDOW_FLAG, winType, kanriId, string.Empty, null, null, null, null, true);
                                        break;
                                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                                        //F3修正⇒修正権限無⇒参照モードで開く
                                        FormManager.OpenFormWithAuth("G141", winType, winType, kanriId, string.Empty);
                                        break;
                                }
                            }
                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormChanges", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }

            LogUtility.DebugMethodEnd();

            return;
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        public void CsvSyuturyoku()
        {
            LogUtility.DebugMethodStart();

            int dataCnt = 0;
            string strHeader = string.Empty;
            this.dto_TME = new TMEDtoCls();
            DataTable result = new DataTable();
            DataTable rstTouroku = new DataTable();
            DataTable rstTourokuNiji = new DataTable();
            DataTable rstYoyaku = new DataTable();

            // 管理番号
            this.dto_TME.KANRI_ID = new ArrayList();

            // 管理番号を設定する
            for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
            {
                if (this.form.customDataGridView1.Rows[i].Cells["電子CSV"].Value != null
                    && "true".Equals(this.form.customDataGridView1.Rows[i].Cells["電子CSV"].Value.ToString().ToLower()))
                {
                    this.dto_TME.KANRI_ID.Add(this.form.customDataGridView1.Rows[i].Cells[this.HIDDEN_KANRI_ID].Value.ToString());
                }
            }

            // 電子CSVデータ取得
            result = dao_GetDMT.GetDataForEntity(dto_TME);
            dataCnt = result.Rows.Count;

            // 検索件数が０件の場合
            if (dataCnt == 0)
            {
                //エラーメッセージ表示
                MessageBoxUtility.MessageBoxShow("E044");
                return;
            }

            //出力ファイルパス
            string csvpath = "未設定";

            var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
            var title = "CSVファイルの出力場所を選択してください。";
            var initialPath = @"C:\Temp";
            var windowHandle = this.form.Handle;
            var isFileSelect = false;
            var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

            browserForFolder = null;

            string strLine = string.Empty;
            string strHeader1 = "連絡番号１,連絡番号２,連絡番号３,引渡し日,";
            string strHeader2 = "排出事業場コード,";
            // タイトル
            string strHeader3 = "引渡し担当者,登録担当者,廃棄物の種類コード,廃棄物の名称,廃棄物の数量,"
                           + "廃棄物の数量単位コード,荷姿コード,荷姿の数量,数量の確定者コード,有害物質１コード,"
                           + "有害物質２コード,有害物質３コード,有害物質４コード,有害物質５コード,有害物質６コード,"
                           + "[区間１]収集運搬業者加入者番号,[区間１]再委託収集運搬業者加入者番号,"
                           + "[区間１]運搬方法コード,[区間１]車両番号,[区間１]運搬担当者,[区間１]運搬先事業場加入者番号,"
                           + "[区間１]運搬先事業場,[区間２]収集運搬業者加入者番号,[区間２]再委託収集運搬業者加入者番号,"
                           + "[区間２]運搬方法コード,[区間２]車両番号,[区間２]運搬担当者,[区間２]運搬先事業場加入者番号,"
                           + "[区間２]運搬先事業場,[区間３]収集運搬業者加入者番号,[区間３]再委託収集運搬業者加入者番号,"
                           + "[区間３]運搬方法コード,[区間３]車両番号,[区間３]運搬担当者,[区間３]運搬先事業場加入者番号,"
                           + "[区間３]運搬先事業場,[区間４]収集運搬業者加入者番号,[区間４]再委託収集運搬業者加入者番号,"
                           + "[区間４]運搬方法コード,[区間４]車両番号,[区間４]運搬担当者,[区間４]運搬先事業場加入者番号,"
                           + "[区間４]運搬先事業場,[区間５]収集運搬業者加入者番号,[区間５]再委託収集運搬業者加入者番号,"
                           + "[区間５]運搬方法コード,[区間５]車両番号,[区間５]運搬担当者,[区間５]運搬先事業場加入者番号,"
                           + "[区間５]運搬先事業場,処分業者加入者番号,再委託処分業者加入者番号,処分事業場,処分方法コード,"
                           + "最終処分事業場登録区分,最終処分事業場１コード,最終処分事業場２コード,最終処分事業場３コード,"
                           + "最終処分事業場４コード,最終処分事業場５コード,最終処分事業場６コード,最終処分事業場７コード,"
                           + "最終処分事業場８コード,最終処分事業場９コード,最終処分事業場１０コード,備考１,備考２,備考３,備考４,備考５";
            if (false == String.IsNullOrEmpty(filePath))
            {
                //ログ用
                csvpath = filePath;

                rstTouroku = result.Clone();
                rstTourokuNiji = result.Clone();
                rstYoyaku = result.Clone();
                for (int i = 0; i < dataCnt; i++)
                {
                    // 予約/ﾏﾆﾌｪｽﾄ区分 =1(予約)の場合
                    if ("1".Equals(this.DbToString(result.Rows[i]["MANIFEST_KBN"])))
                    {
                        // 予約登録
                        rstYoyaku.ImportRow(result.Rows[i]);
                    }
                    // 予約/ﾏﾆﾌｪｽﾄ区分 =2(マニフェスト)の場合
                    else if ("2".Equals(this.DbToString(result.Rows[i]["MANIFEST_KBN"])))
                    {
                        // マニフェスト情報.中間処理産業廃棄物情報管理方法フラグがNULLの場合
                        if (this.IsNullOrEmpty(result.Rows[i]["FIRST_MANIFEST_FLAG"]))
                        {
                            // 新規登録
                            rstTouroku.ImportRow(result.Rows[i]);
                        }
                        else
                        {
                            // 新規登録２次
                            rstTourokuNiji.ImportRow(result.Rows[i]);
                        }
                        // マニフェスト情報.中間処理産業廃棄物情報管理方法フラグがNULL以外の場合
                    }
                }

                // 新規登録
                if (rstTouroku.Rows.Count > 0)
                {
                    strHeader = strHeader1 + strHeader2 + strHeader3;
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //filePath = csvpath + "\\" + "電子マニフェスト情報_新規登録_"
                    //              + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                    filePath = csvpath + "\\" + "電子マニフェスト情報_新規登録_"
                                  + this.getDBDateTime().ToString("yyyyMMdd_HHmmss") + ".csv";
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    this.ShinkiTourokuCsv(strHeader, rstTouroku, filePath);
                }

                // 新規登録２次
                if (rstTourokuNiji.Rows.Count > 0)
                {
                    strHeader = strHeader1 + strHeader3 + ",中間処理産業廃棄物登録区分";
                    for (int i = 1; i < 19; i++)
                    {
                        strHeader += ",[1次マニフェスト情報 " + i.ToString().PadLeft(2, '0') + "]電子/紙区分";
                        strHeader += ",[1次マニフェスト情報 " + i.ToString().PadLeft(2, '0') + "]マニフェスト番号／交付番号";
                        strHeader += ",[1次マニフェスト情報 " + i.ToString().PadLeft(2, '0') + "]交付年月日";
                        strHeader += ",[1次マニフェスト情報 " + i.ToString().PadLeft(2, '0') + "]連絡番号";
                        strHeader += ",[1次マニフェスト情報 " + i.ToString().PadLeft(2, '0') + "]処分終了日";
                        strHeader += ",[1次マニフェスト情報 " + i.ToString().PadLeft(2, '0') + "]排出事業者";
                        strHeader += ",[1次マニフェスト情報 " + i.ToString().PadLeft(2, '0') + "]排出事業場";
                        strHeader += ",[1次マニフェスト情報 " + i.ToString().PadLeft(2, '0') + "]廃棄物の種類";
                        strHeader += ",[1次マニフェスト情報 " + i.ToString().PadLeft(2, '0') + "]廃棄物の数量";
                        strHeader += ",[1次マニフェスト情報 " + i.ToString().PadLeft(2, '0') + "]廃棄物の数量単位コード";
                    }

                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //filePath = csvpath + "\\" + "電子マニフェスト情報_新規登録(２次)_"
                    //                                  + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                    filePath = csvpath + "\\" + "電子マニフェスト情報_新規登録(２次)_"
                                                      + this.getDBDateTime().ToString("yyyyMMdd_HHmmss") + ".csv";
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    this.ShinkiTourokuNijiCsv(strHeader, rstTourokuNiji, filePath);
                }

                // 予約登録
                if (rstYoyaku.Rows.Count > 0)
                {
                    strHeader = strHeader1 + strHeader2 + strHeader3 + ",修正許可,発行件数";
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //filePath = csvpath + "\\" + "電子マニフェスト情報_予約登録_"
                    //                                  + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                    filePath = csvpath + "\\" + "電子マニフェスト情報_予約登録_"
                                                      + this.getDBDateTime().ToString("yyyyMMdd_HHmmss") + ".csv";
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    this.YoyakuTourokuCsv(strHeader, rstYoyaku, filePath);
                }
                // 正常終了メッセージ出力
                MessageBoxUtility.MessageBoxShow("I000");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 新規登録CSV出力
        /// </summary>
        private void ShinkiTourokuCsv(string strHeader, DataTable result, string filePath)
        {
            int dataCnt = result.Rows.Count;
            string strLine = string.Empty;

            try
            {
                //ファイルを開く,追記しない(上書き）、エンコードはデフォルト（日本語WindowsではSJIS)
                using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.GetEncoding(0)))
                {
                    // ヘッダ出力
                    //sw.WriteLine(strHeader);

                    // 明細データ
                    for (int i = 0; i < dataCnt; i++)
                    {
                        // 共通部分設定
                        strLine = this.KyoutuuSakusei("1", result.Rows[i]);
                        sw.WriteLine(strLine);
                    }
                }
            }
            catch (IOException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                if (ex.Message.Contains("別のプロセスで使用されているため"))
                {
                    MessageBox.Show("ファイルのオープンに失敗しました。\r\n他のアプリケーションでファイルを開いている可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    throw; // 想定外の場合は再スローする
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                MessageBox.Show("ファイルのオープンに失敗しました。\r\n選択したファイルへの書き込み権限が無い可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 新規登録２次CSV出力
        /// </summary>
        private void ShinkiTourokuNijiCsv(string strHeader, DataTable result, string filePath)
        {
            int dataCnt = result.Rows.Count;
            string strLine = string.Empty;

            try
            {
                //ファイルを開く,追記しない(上書き）、エンコードはデフォルト（日本語WindowsではSJIS)
                using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.GetEncoding(0)))
                {
                    // ヘッダ出力
                    //sw.WriteLine(strHeader);

                    // 明細データ
                    for (int i = 0; i < dataCnt; i++)
                    {
                        // 共通部分設定
                        strLine = this.KyoutuuSakusei("2", result.Rows[i]) + ",";

                        // 中間処理産業廃棄物登録区分
                        strLine += result.Rows[i]["FIRST_MANIFEST_FLAG"] + ",";

                        for (int j = 1; j < 19; j++)
                        {
                            // 電子/紙区分
                            strLine += result.Rows[i]["MEDIA_TYPE_" + j.ToString().PadLeft(2, '0')] + ",";
                            // マニフェスト番号／交付番号
                            strLine += result.Rows[i]["MANIFEST_ID_" + j.ToString().PadLeft(2, '0')] + ",";

                            // 1次マニフェスト情報.電子/紙区分=2(紙)の場合のみ
                            if ("2".Equals(this.DbToString(result.Rows[i]["MEDIA_TYPE_" + j.ToString().PadLeft(2, '0')])))
                            {
                                // 交付年月日
                                strLine += this.DateFormat(result.Rows[i]["KOUHU_DATE_" + j.ToString().PadLeft(2, '0')]) + ",";
                                // 連絡番号
                                strLine += result.Rows[i]["RENRAKU_ID_" + j.ToString().PadLeft(2, '0')] + ",";
                                // 処分終了日
                                strLine += this.DateFormat(result.Rows[i]["SBN_END_DATE_" + j.ToString().PadLeft(2, '0')]) + ",";
                                // 排出事業者
                                strLine += result.Rows[i]["HST_SHA_NAME_" + j.ToString().PadLeft(2, '0')] + ",";
                                // 排出事業場
                                strLine += result.Rows[i]["HST_JOU_NAME_" + j.ToString().PadLeft(2, '0')] + ",";
                                // 廃棄物の種類
                                strLine += result.Rows[i]["HAIKI_SHURUI_" + j.ToString().PadLeft(2, '0')] + ",";
                                // 廃棄物の数量
                                strLine += result.Rows[i]["HAIKI_SUU_" + j.ToString().PadLeft(2, '0')] + ",";
                                // 廃棄物の数量単位コード
                                strLine += result.Rows[i]["HAIKI_SUU_UNIT_" + j.ToString().PadLeft(2, '0')];
                                if (j < 18)
                                {
                                    strLine += ",";
                                }
                            }
                            else
                            {
                                // 交付年月日
                                strLine += string.Empty + ",";
                                // 連絡番号
                                strLine += string.Empty + ",";
                                // 処分終了日
                                strLine += string.Empty + ",";
                                // 排出事業者
                                strLine += string.Empty + ",";
                                // 排出事業場
                                strLine += string.Empty + ",";
                                // 廃棄物の種類
                                strLine += string.Empty + ",";
                                // 廃棄物の数量
                                strLine += string.Empty + ",";
                                // 廃棄物の数量単位コード
                                strLine += string.Empty;
                                if (j < 18)
                                {
                                    strLine += ",";
                                }
                            }
                        }

                        sw.WriteLine(strLine);
                    }
                }
            }
            catch (IOException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                if (ex.Message.Contains("別のプロセスで使用されているため"))
                {
                    MessageBox.Show("ファイルのオープンに失敗しました。\r\n他のアプリケーションでファイルを開いている可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    throw; // 想定外の場合は再スローする
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                MessageBox.Show("ファイルのオープンに失敗しました。\r\n選択したファイルへの書き込み権限が無い可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 予約登録CSV出力
        /// </summary>
        private void YoyakuTourokuCsv(string strHeader, DataTable result, string filePath)
        {
            int dataCnt = result.Rows.Count;
            string strLine = string.Empty;

            try
            {
                //ファイルを開く,追記しない(上書き）、エンコードはデフォルト（日本語WindowsではSJIS)
                using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.GetEncoding(0)))
                {
                    // ヘッダ出力
                    //sw.WriteLine(strHeader);

                    // 明細データ
                    for (int i = 0; i < dataCnt; i++)
                    {
                        // 共通部分設定
                        strLine = this.KyoutuuSakusei("3", result.Rows[i]) + ",";
                        // 修正許可
                        strLine += result.Rows[i]["KENGEN_CODE"] + ",";
                        // 発行件数
                        strLine += string.Empty;
                        sw.WriteLine(strLine);
                    }
                }
            }
            catch (IOException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                if (ex.Message.Contains("別のプロセスで使用されているため"))
                {
                    MessageBox.Show("ファイルのオープンに失敗しました。\r\n他のアプリケーションでファイルを開いている可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    throw; // 想定外の場合は再スローする
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                MessageBox.Show("ファイルのオープンに失敗しました。\r\n選択したファイルへの書き込み権限が無い可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 共通部分作成
        /// </summary>
        /// <param name="sakuseiKubunn">
        /// 1:新規登録
        /// 2:新規登録２次
        /// 3:予約登録
        /// </param>
        private string KyoutuuSakusei(string sakuseiKubunn, DataRow resultRow)
        {
            string strLine = string.Empty;

            // 連絡番号１
            strLine += resultRow["RENRAKU_ID_1"] + ",";
            // 連絡番号２
            strLine += resultRow["RENRAKU_ID_2"] + ",";
            // 連絡番号３
            strLine += resultRow["RENRAKU_ID_3"] + ",";
            // 引渡し日
            strLine += this.DateFormat(resultRow["HIKIWATASHI_DATE"]) + ",";

            if ("1".Equals(sakuseiKubunn) || "3".Equals(sakuseiKubunn))
            {
                // 排出事業場コード
                strLine += resultRow["JIGYOUJOU_CD_HS"] + ",";
            }
            // 引渡し担当者
            strLine += resultRow["HIKIWATASHI_TAN_NAME"] + ",";
            // 登録担当者
            strLine += resultRow["REGI_TAN"] + ",";
            // 廃棄物の種類コード
            strLine += DbToString(resultRow["HAIKI_DAI_CODE"])
                     + DbToString(resultRow["HAIKI_CHU_CODE"])
                     + DbToString(resultRow["HAIKI_SHO_CODE"])
                     + DbToString(resultRow["HAIKI_SAI_CODE"]) + ",";
            // 廃棄物の名称
            strLine += resultRow["HAIKI_NAME"] + ",";
            // 廃棄物の数量
            strLine += resultRow["HAIKI_SUU"] + ",";
            // 廃棄物の数量単位コード
            strLine += resultRow["HAIKI_UNIT_CODE"] + ",";
            // 荷姿コード
            strLine += resultRow["NISUGATA_CODE"] + ",";
            // 荷姿の数量
            strLine += resultRow["NISUGATA_SUU"] + ",";
            // 数量の確定者コード
            strLine += resultRow["SUU_KAKUTEI_CODE"] + ",";
            // 有害物質１コード
            strLine += resultRow["YUUGAI_CODE_1"] + ",";
            // 有害物質２コード
            strLine += resultRow["YUUGAI_CODE_2"] + ",";
            // 有害物質３コード
            strLine += resultRow["YUUGAI_CODE_3"] + ",";
            // 有害物質４コード
            strLine += resultRow["YUUGAI_CODE_4"] + ",";
            // 有害物質５コード
            strLine += resultRow["YUUGAI_CODE_5"] + ",";
            // 有害物質６コード
            strLine += resultRow["YUUGAI_CODE_6"] + ",";
            // [区間１]収集運搬業者加入者番号
            strLine += resultRow["UPN_SHA_EDI_MEMBER_ID_1"] + ",";
            // [区間１]再委託収集運搬業者加入者番号
            strLine += resultRow["SAI_UPN_SHA_EDI_MEMBER_ID_1"] + ",";
            // [区間１]運搬方法コード
            strLine += resultRow["UPN_WAY_CODE_1"] + ",";
            // [区間１]車両番号
            strLine += resultRow["CAR_NO_1"] + ",";
            // [区間１]運搬担当者
            strLine += resultRow["UPN_TAN_NAME_1"] + ",";
            // [区間１]運搬先事業場加入者番号
            strLine += resultRow["UPNSAKI_EDI_MEMBER_ID_1"] + ",";

            // 区間番号が最大の場合
            if (this.IsNullOrEmpty(resultRow["UPN_ROUTE_NO_2"]))
            {
                // [区間１]運搬先事業場
                strLine += string.Empty + ",";
            }
            else
            {
                // 報告不要区分=１の場合　
                if ("TRUE".Equals(this.DbToString(resultRow["HOUKOKU_HUYOU_KBN_1"]).ToUpper()))
                {
                    // [区間１]運搬先事業場
                    strLine += resultRow["JIGYOUJOU_CD_1"] + ",";
                }
                else
                {
                    // [区間１]運搬先事業場
                    strLine += resultRow["UPNSAKI_JOU_ID_1"] + ",";
                }
            }

            // [区間２]収集運搬業者加入者番号
            strLine += resultRow["UPN_SHA_EDI_MEMBER_ID_2"] + ",";
            // [区間２]再委託収集運搬業者加入者番号
            strLine += resultRow["SAI_UPN_SHA_EDI_MEMBER_ID_2"] + ",";
            // [区間２]運搬方法コード
            strLine += resultRow["UPN_WAY_CODE_2"] + ",";
            // [区間２]車両番号
            strLine += resultRow["CAR_NO_2"] + ",";
            // [区間２]運搬担当者
            strLine += resultRow["UPN_TAN_NAME_2"] + ",";
            // [区間２]運搬先事業場加入者番号
            strLine += resultRow["UPNSAKI_EDI_MEMBER_ID_2"] + ",";

            // 区間番号が最大の場合
            if (this.IsNullOrEmpty(resultRow["UPN_ROUTE_NO_3"]))
            {
                // [区間２]運搬先事業場
                strLine += string.Empty + ",";
            }
            else
            {
                // 報告不要区分=１の場合　
                if ("TRUE".Equals(this.DbToString(resultRow["HOUKOKU_HUYOU_KBN_2"]).ToUpper()))
                {
                    // [区間２]運搬先事業場
                    strLine += resultRow["JIGYOUJOU_CD_2"] + ",";
                }
                else
                {
                    // [区間２]運搬先事業場
                    strLine += resultRow["UPNSAKI_JOU_ID_2"] + ",";
                }
            }

            // [区間３]収集運搬業者加入者番号
            strLine += resultRow["UPN_SHA_EDI_MEMBER_ID_3"] + ",";
            // [区間３]再委託収集運搬業者加入者番号
            strLine += resultRow["SAI_UPN_SHA_EDI_MEMBER_ID_3"] + ",";
            // [区間３]運搬方法コード
            strLine += resultRow["UPN_WAY_CODE_3"] + ",";
            // [区間３]車両番号
            strLine += resultRow["CAR_NO_3"] + ",";
            // [区間３]運搬担当者
            strLine += resultRow["UPN_TAN_NAME_3"] + ",";
            // [区間３]運搬先事業場加入者番号
            strLine += resultRow["UPNSAKI_EDI_MEMBER_ID_3"] + ",";

            // 区間番号が最大の場合
            if (this.IsNullOrEmpty(resultRow["UPN_ROUTE_NO_4"]))
            {
                // [区間３]運搬先事業場
                strLine += string.Empty + ",";
            }
            else
            {
                // 報告不要区分=１の場合　
                if ("TRUE".Equals(this.DbToString(resultRow["HOUKOKU_HUYOU_KBN_3"]).ToUpper()))
                {
                    // [区間３]運搬先事業場
                    strLine += resultRow["JIGYOUJOU_CD_3"] + ",";
                }
                else
                {
                    // [区間３]運搬先事業場
                    strLine += resultRow["UPNSAKI_JOU_ID_3"] + ",";
                }
            }

            // [区間４]収集運搬業者加入者番号
            strLine += resultRow["UPN_SHA_EDI_MEMBER_ID_4"] + ",";
            // [区間４]再委託収集運搬業者加入者番号
            strLine += resultRow["SAI_UPN_SHA_EDI_MEMBER_ID_4"] + ",";
            // [区間４]運搬方法コード
            strLine += resultRow["UPN_WAY_CODE_4"] + ",";
            // [区間４]車両番号
            strLine += resultRow["CAR_NO_4"] + ",";
            // [区間４]運搬担当者
            strLine += resultRow["UPN_TAN_NAME_4"] + ",";
            // [区間４]運搬先事業場加入者番号
            strLine += resultRow["UPNSAKI_EDI_MEMBER_ID_4"] + ",";
            // 区間番号が最大の場合
            if (this.IsNullOrEmpty(resultRow["UPN_ROUTE_NO_5"]))
            {
                // [区間４]運搬先事業場
                strLine += string.Empty + ",";
            }
            else
            {
                // 報告不要区分=１の場合　
                if ("TRUE".Equals(this.DbToString(resultRow["HOUKOKU_HUYOU_KBN_4"]).ToUpper()))
                {
                    // [区間４]運搬先事業場
                    strLine += resultRow["JIGYOUJOU_CD_4"] + ",";
                }
                else
                {
                    // [区間４]運搬先事業場
                    strLine += resultRow["UPNSAKI_JOU_ID_4"] + ",";
                }
            }

            // [区間５]収集運搬業者加入者番号
            strLine += resultRow["UPN_SHA_EDI_MEMBER_ID_5"] + ",";
            // [区間５]再委託収集運搬業者加入者番号
            strLine += resultRow["SAI_UPN_SHA_EDI_MEMBER_ID_5"] + ",";
            // [区間５]運搬方法コード
            strLine += resultRow["UPN_WAY_CODE_5"] + ",";
            // [区間５]車両番号
            strLine += resultRow["CAR_NO_5"] + ",";
            // [区間５]運搬担当者
            strLine += resultRow["UPN_TAN_NAME_5"] + ",";
            // [区間５]運搬先事業場加入者番号
            strLine += resultRow["UPNSAKI_EDI_MEMBER_ID_5"] + ",";
            // [区間５]運搬先事業場
            strLine += string.Empty + ",";

            // 処分業者加入者番号
            strLine += resultRow["SBN_SHA_MEMBER_ID"] + ",";
            // 再委託処分業者加入者番号
            strLine += resultRow["SAI_SBN_SHA_MEMBER_ID"] + ",";

            // 区間番号が最大の場合
            if (!this.IsNullOrEmpty(resultRow["UPN_ROUTE_NO_5"]))
            {
                // 報告不要区分=１の場合　
                if ("TRUE".Equals(this.DbToString(resultRow["HOUKOKU_HUYOU_KBN_5"]).ToUpper()))
                {
                    // 処分事業場
                    strLine += resultRow["JIGYOUJOU_CD_5"] + ",";
                }
                else
                {
                    // 処分事業場
                    strLine += resultRow["UPNSAKI_JOU_ID_5"] + ",";
                }
            }
            else if (!this.IsNullOrEmpty(resultRow["UPN_ROUTE_NO_4"]))
            {
                // 報告不要区分=１の場合　
                if ("TRUE".Equals(this.DbToString(resultRow["HOUKOKU_HUYOU_KBN_4"]).ToUpper()))
                {
                    // 処分事業場
                    strLine += resultRow["JIGYOUJOU_CD_4"] + ",";
                }
                else
                {
                    // 処分事業場
                    strLine += resultRow["UPNSAKI_JOU_ID_4"] + ",";
                }
            }
            else if (!this.IsNullOrEmpty(resultRow["UPN_ROUTE_NO_3"]))
            {
                // 報告不要区分=１の場合　
                if ("TRUE".Equals(this.DbToString(resultRow["HOUKOKU_HUYOU_KBN_3"]).ToUpper()))
                {
                    // 処分事業場
                    strLine += resultRow["JIGYOUJOU_CD_3"] + ",";
                }
                else
                {
                    // 処分事業場
                    strLine += resultRow["UPNSAKI_JOU_ID_3"] + ",";
                }
            }
            else if (!this.IsNullOrEmpty(resultRow["UPN_ROUTE_NO_2"]))
            {
                // 報告不要区分=１の場合　
                if ("TRUE".Equals(this.DbToString(resultRow["HOUKOKU_HUYOU_KBN_2"]).ToUpper()))
                {
                    // 処分事業場
                    strLine += resultRow["JIGYOUJOU_CD_2"] + ",";
                }
                else
                {
                    // 処分事業場
                    strLine += resultRow["UPNSAKI_JOU_ID_2"] + ",";
                }
            }
            else
            {
                // 報告不要区分=１の場合　
                if ("TRUE".Equals(this.DbToString(resultRow["HOUKOKU_HUYOU_KBN_1"]).ToUpper()))
                {
                    // 処分事業場
                    strLine += resultRow["JIGYOUJOU_CD_1"] + ",";
                }
                else
                {
                    // 処分事業場
                    strLine += resultRow["UPNSAKI_JOU_ID_1"] + ",";
                }
            }

            // 処分方法コード
            strLine += resultRow["SBN_WAY_CODE"] + ",";
            // 最終処分事業場登録区分
            strLine += resultRow["LAST_SBN_JOU_KISAI_FLAG"] + ",";
            // 最終処分事業場１コード
            strLine += resultRow["JIGYOUJOU_CD_SBYT_1"] + ",";
            // 最終処分事業場２コード
            strLine += resultRow["JIGYOUJOU_CD_SBYT_2"] + ",";
            // 最終処分事業場３コード
            strLine += resultRow["JIGYOUJOU_CD_SBYT_3"] + ",";
            // 最終処分事業場４コード
            strLine += resultRow["JIGYOUJOU_CD_SBYT_4"] + ",";
            // 最終処分事業場５コード
            strLine += resultRow["JIGYOUJOU_CD_SBYT_5"] + ",";
            // 最終処分事業場６コード
            strLine += resultRow["JIGYOUJOU_CD_SBYT_6"] + ",";
            // 最終処分事業場７コード
            strLine += resultRow["JIGYOUJOU_CD_SBYT_7"] + ",";
            // 最終処分事業場８コード
            strLine += resultRow["JIGYOUJOU_CD_SBYT_8"] + ",";
            // 最終処分事業場９コード
            strLine += resultRow["JIGYOUJOU_CD_SBYT_9"] + ",";
            // 最終処分事業場１０コード
            strLine += resultRow["JIGYOUJOU_CD_SBYT_10"] + ",";
            // 備考１
            strLine += resultRow["BIKOU_1"] + ",";
            // 備考２
            strLine += resultRow["BIKOU_2"] + ",";
            // 備考３
            strLine += resultRow["BIKOU_3"] + ",";
            // 備考４
            strLine += resultRow["BIKOU_4"] + ",";
            // 備考５
            strLine += resultRow["BIKOU_5"];

            return strLine;
        }

        /// <summary>
        /// DB値有無判断
        /// </summary>
        private bool IsNullOrEmpty(object obj)
        {
            if (obj == System.DBNull.Value || string.Empty.Equals(obj.ToString().Trim()))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// DB値変換
        /// </summary>
        private string DbToString(object obj)
        {
            if (IsNullOrEmpty(obj))
            {
                return string.Empty;
            }

            return obj.ToString();
        }

        /// <summary>
        /// 日付フォーマット
        /// </summary>
        private string DateFormat(object obj)
        {
            string objStr = DbToString(obj);
            if (objStr.Length == 8)
            {
                objStr = objStr.Substring(0, 4) + "/" + objStr.Substring(4, 2) + "/" + objStr.Substring(6, 2);
            }

            return objStr;
        }

        // 20140603 katen 不具合No.4131 start‏
        /// <summary>
        /// 業者チェック(排出事業者、運搬受託者、処分受託者) 
        /// </summary>
        /// <param name="obj">チェックコントロール</param>
        /// <param name="colname">チェックカラム名称</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkGyosya(object obj, string colname)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(obj, colname);

                TextBox txt = (TextBox)obj;
                if (txt.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new CommonGenbaDtoCls();
                Serch.GYOUSHA_CD = txt.Text;
                Serch.GYOUSHAKBN_MANI = "True";
                Serch.ISNOT_NEED_DELETE_FLG = true;
                //最終処分業者の場合、最終処分場区分の条件を追加した
                this.SearchResult = new DataTable();
                DataTable dt = mlogic.GetGenbaAll(Serch);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][colname].ToString() == "True")
                        {
                            ret = 0;
                            LogUtility.DebugMethodEnd(ret);
                            return ret;
                        }
                    }
                }
                this.form.messageShowLogic.MessageBoxShow("E020", "業者");

                txt.Focus();
                txt.SelectAll();
                ret = 2;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGyosya", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 現場チェック(排出事業者、運搬受託者、処分受託者) 
        /// </summary>
        /// <param name="genba">現場CD</param>
        /// <param name="gyosya">事業者CD</param>
        /// <param name="colname">チェックカラム名称</param>
        /// <param name="colname2">[処分事業場専用]チェックカラム名称2</param>
        /// <param name="genba">[処分事業場専用]現場名</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkJigyouba(object genba, object gyosya, string colname, string colname2 = "", object genbaName = null)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(genba, gyosya, colname, colname2, genbaName);

                TextBox txt1 = (TextBox)genba;
                TextBox txt2 = (TextBox)gyosya;

                //空
                if (txt1.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                if (txt2.Text == string.Empty)
                {
                    switch (colname)
                    {
                        case "HAISHUTSU_NIZUMI_GENBA_KBN":
                            this.form.messageShowLogic.MessageBoxShow("E051", "排出事業者");
                            break;

                        case "SHOBUN_NIOROSHI_GENBA_KBN":
                            this.form.messageShowLogic.MessageBoxShow("E051", "処分受託者");
                            break;

                        case "TSUMIKAEHOKAN_KBN":
                            this.form.messageShowLogic.MessageBoxShow("E051", "積替保管業者");
                            break;
                    }
                    txt1.Text = string.Empty;
                    txt1.Focus();
                    txt1.SelectAll();

                    ret = 2;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END

                var Serch = new CommonGenbaDtoCls();
                Serch.GENBA_CD = txt1.Text;
                Serch.GYOUSHA_CD = txt2.Text;
                Serch.ISNOT_NEED_DELETE_FLG = true;

                this.SearchResult = new DataTable();
                DataTable dt = this.mlogic.GetGenba(Serch);
                switch (dt.Rows.Count)
                {
                    case 0:
                        this.form.messageShowLogic.MessageBoxShow("E020", "現場");
                        break;

                    case 1:
                        if (dt.Rows[0][colname].ToString() == "True" && string.IsNullOrEmpty(colname2))
                        {
                            txt1.Text = dt.Rows[0]["GENBA_CD"].ToString();
                            txt2.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();

                            ret = 0;
                            LogUtility.DebugMethodEnd(ret);
                            return ret;
                        }
                        else if (!string.IsNullOrEmpty(colname2) && (dt.Rows[0][colname].ToString() == "True" || dt.Rows[0][colname2].ToString() == "True"))
                        {
                            /* 処分事業場用(現場の名称も同時に設定) */
                            txt1.Text = dt.Rows[0]["GENBA_CD"].ToString();
                            txt2.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                            ((TextBox)genbaName).Text = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();

                            ret = 0;
                            LogUtility.DebugMethodEnd(ret);
                            return ret;
                        }
                        this.form.messageShowLogic.MessageBoxShow("E058");
                        break;

                    default:
                        switch (colname)
                        {
                            case "HAISHUTSU_NIZUMI_GENBA_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "排出事業者");
                                break;

                            case "SAISHUU_SHOBUNJOU_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "最終処分の業者");
                                break;

                            case "SHOBUN_NIOROSHI_GENBA_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "処分受託者");
                                break;

                            case "TSUMIKAEHOKAN_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "積替保管業者");
                                break;

                            case "UNPAN_JUTAKUSHA_KAISHA_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "運搬の受託者");
                                break;
                        }
                        break;
                }
                txt1.Focus();
                txt1.SelectAll();
                ret = 2;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkJigyouba", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 報告書分類チェック
        /// </summary>
        /// <param name="houkokushoBunrui">報告書分類CD</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkHoukokushoBunrui(object houkokushoBunrui)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(houkokushoBunrui);

                TextBox txt1 = (TextBox)houkokushoBunrui;

                //空
                if (txt1.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new HokokushoDtoCls();
                Serch.HOUKOKUSHO_BUNRUI_CD = txt1.Text;

                this.SearchResult = new DataTable();
                DataTable dt = this.dao_Hokokusho.GetDataForEntity(Serch);
                switch (dt.Rows.Count)
                {
                    case 0:
                        this.form.messageShowLogic.MessageBoxShow("E020", "報告書分類");
                        break;

                    case 1:
                        this.form.ctxt_HokokushoBunrui.Text = Convert.ToString(dt.Rows[0]["HOUKOKUSHO_BUNRUI_NAME_RYAKU"]);
                        ret = 0;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;

                    default:
                        break;
                }
                txt1.Focus();
                txt1.SelectAll();
                ret = 2;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkHoukokushoBunrui", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 廃棄物種類チェック
        /// </summary>
        /// <param name="haikiKbn">廃棄区分</param>
        /// <param name="haikibutuShurui">廃棄物種類CD</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkHaikibutuShurui(object haikiKbn, object haikibutuShurui)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(haikiKbn, haikibutuShurui);

                TextBox txt1 = (TextBox)haikiKbn;
                TextBox txt2 = (TextBox)haikibutuShurui;

                //空
                if (txt2.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new HaikiShuruiDtoCls();
                switch (txt1.Text)
                {
                    case "1":
                        Serch.HAIKI_KBN_CD = "1";
                        break;
                    case "2":
                        Serch.HAIKI_KBN_CD = "3";
                        break;
                    case "3":
                        Serch.HAIKI_KBN_CD = "2";
                        break;
                    case "4":
                        Serch.HAIKI_KBN_CD = "4";
                        break;
                }
                Serch.HAIKI_SHURUI_CD = txt2.Text;

                DataTable dt = this.dao_HaikiShurui.GetDataForEntity(Serch);
                switch (dt.Rows.Count)
                {
                    case 0:
                        this.form.messageShowLogic.MessageBoxShow("E020", "廃棄物種類");
                        break;

                    case 1:
                        this.form.ctxt_HaikibutuShurui.Text = Convert.ToString(dt.Rows[0]["HAIKI_SHURUI_NAME_RYAKU"]);
                        ret = 0;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;

                    default:
                        break;
                }
                txt2.Focus();
                txt2.SelectAll();
                ret = 2;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkHaikibutuShurui", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 廃棄物名称チェック
        /// </summary>
        /// <param name="haikibutuShurui">廃棄物名称CD</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkHaikibutuName(object haikibutuName)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(haikibutuName);

                TextBox txt1 = (TextBox)haikibutuName;

                //空
                if (txt1.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new HaikiNameDtoCls();
                Serch.HAIKI_NAME_CD = txt1.Text;

                DataTable dt = this.dao_HaikiName.GetDataForEntity(Serch);
                switch (dt.Rows.Count)
                {
                    case 0:
                        this.form.messageShowLogic.MessageBoxShow("E020", "廃棄物名称");
                        break;

                    case 1:
                        this.form.ctxt_HaikibutuName.Text = Convert.ToString(dt.Rows[0]["HAIKI_NAME_RYAKU"]);
                        ret = 0;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;

                    default:
                        break;
                }
                txt1.Focus();
                txt1.SelectAll();
                ret = 2;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkHaikibutuName", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        // 20140610 katen 不具合No.4712 start‏
        /// <summary>
        /// 廃棄物種類チェック
        /// </summary>
        /// <param name="haikiKbn">廃棄区分</param>
        /// <param name="haikibutuShurui">廃棄物種類CD</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkDenshiHaikibutuShurui(object haikibutuShurui)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(haikibutuShurui);

                TextBox txt1 = (TextBox)haikibutuShurui;

                //空
                if (txt1.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new SearchExistDTOCls();
                Serch.HAIKI_SHURUI_CD = txt1.Text;

                this.SearchResult = new DataTable();
                DataTable dt = this.DENSHI_HAIKI_SHURUIE_SearchDao.GetDataByShuruiCD(Serch);
                switch (dt.Rows.Count)
                {
                    case 0:
                        this.form.messageShowLogic.MessageBoxShow("E020", "廃棄物種類");
                        break;

                    case 1:
                        this.form.ctxt_HaikibutuShurui.Text = Convert.ToString(dt.Rows[0]["HAIKI_SHURUI_NAME"]);
                        ret = 0;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;

                    default:
                        break;
                }
                txt1.Focus();
                txt1.SelectAll();
                ret = 2;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkDenshiHaikibutuShurui", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 廃棄物名称チェック
        /// </summary>
        /// <param name="haikibutuShurui">廃棄物名称CD</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkDenshiHaikibutuName(object haikibutuName)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(haikibutuName);

                TextBox txt1 = (TextBox)haikibutuName;

                //空
                if (txt1.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new SearchExistDTOCls();
                Serch.HAIKI_NAME_CD = txt1.Text;

                DataTable dt = this.DENSHI_HAIKI_NAME_SearchDao.GetDataByNameCD(Serch);
                switch (dt.Rows.Count)
                {
                    case 0:
                        this.form.messageShowLogic.MessageBoxShow("E020", "廃棄物名称");
                        break;

                    default:
                        this.form.ctxt_HaikibutuName.Text = Convert.ToString(dt.Rows[0]["HAIKI_NAME"]);
                        ret = 0;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                }
                txt1.Focus();
                txt1.SelectAll();
                ret = 2;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkDenshiHaikibutuShurui", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        // 20140610 katen 不具合No.4712 end‏

        /// <summary>
        /// 1次/2次マニフェスト設定
        /// </summary>
        public virtual bool SetManifestFrom(string kbn)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                //タイトル
                string strTitelName = this.form.DenshuKbn.ToTitleString();

                //背景色
                Color BackColor = Color.FromArgb(0, 105, 51);
                switch (maniFlag)
                {
                    case 1://１次マニフェスト
                        switch (kbn)
                        {
                            case "btn_process2":
                                strTitelName = "二次" + strTitelName;
                                BackColor = Color.FromArgb(0, 51, 160);
                                parentbaseform.bt_process2.Text = "[2]1次マニ";
                                maniFlag = 2;
                                break;
                            case "Non":
                                strTitelName = "一次" + strTitelName;
                                parentbaseform.bt_process2.Text = "[2]2次マニ";
                                break;
                        }
                        break;

                    case 2://２次マニフェスト
                        switch (kbn)
                        {
                            case "btn_process2":
                                strTitelName = "一次" + strTitelName;
                                parentbaseform.bt_process2.Text = "[2]2次マニ";
                                maniFlag = 1;
                                break;
                            case "Non":
                                strTitelName = "二次" + strTitelName;
                                BackColor = Color.FromArgb(0, 51, 160);
                                parentbaseform.bt_process2.Text = "[2]1次マニ";
                                break;
                        }
                        break;
                }

                if (AppConfig.IsManiLite)
                {
                    // マニライトの場合、二次マニは無し
                    parentbaseform.bt_process2.Text = string.Empty;
                }

                // 20140530 katen No.679 end‏
                //ヘッダーの設定
                SetHeader(strTitelName, BackColor);

                //ボディーの設定
                SetBody(BackColor);

                // 20140613 syunrei EV004715_タイトルラベルが入力画面と合わない start
                this.form.SetTitleString();
                // 20140613 syunrei EV004715_タイトルラベルが入力画面と合わない start

                this.form.label17.BackColor = this.form.BackColor;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetManifestFrom", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// ヘッダーフォームの設定
        /// タイトルラベル、ラベルの背景色を変更
        /// </summary>
        public void SetHeader(string strTitleName, Color BackColor)
        {
            LogUtility.DebugMethodStart(strTitleName, BackColor);
            this.header.lb_title.Text = strTitleName;
            this.header.lb_title.BackColor = BackColor;
            this.header.label1.BackColor = BackColor;
            this.header.label2.BackColor = BackColor;
            this.header.label4.BackColor = BackColor;
            LogUtility.DebugMethodEnd(strTitleName, BackColor);
        }

        /// <summary>
        /// ラベルの背景色を変更
        /// </summary>
        /// <param name="BackColor">設定する色</param>
        public void SetBody(Color BackColor)
        {
            LogUtility.DebugMethodStart(BackColor);

            this.form.customDataGridView1.ColumnHeadersDefaultCellStyle.BackColor = BackColor;
            this.allControl.OfType<Label>().ToList().ForEach(c => c.BackColor = BackColor);
            this.form.label3.BackColor = Color.Transparent;
            LogUtility.DebugMethodEnd(BackColor);
        }

        /// <summary>
        /// 現場マスタから住所情報を取得してTextBoxに設定
        /// </summary>
        /// <param name="NameFlg">現場名 部分採用 All:正式名称1+正式名称2、Part1:正式名称1のみ</param>
        /// <param name="txt_gyoshaCd">業者CD</param>
        /// <param name="txt_JigyoubaCd">現場CD</param>
        /// <param name="txt_JigyoubaName">現場名</param>
        /// <param name="HAISHUTSU_NIZUMI_GENBA_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="SAISHUU_SHOBUNJOU_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="SHOBUN_NIOROSHI_GENBA_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="TSUMIKAEHOKAN_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="ISNOT_NEED_DELETE_FLG">削除チェックいるかどうかの判断フラッグ</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int SetAddressJigyouba(string NameFlg, CustomTextBox txt_gyoshaCd, CustomTextBox txt_JigyoubaCd, CustomTextBox txt_JigyoubaName
            , bool HAISHUTSU_NIZUMI_GENBA_KBN
            , bool SAISHUU_SHOBUNJOU_KBN
            , bool SHOBUN_NIOROSHI_GENBA_KBN
            , bool TSUMIKAEHOKAN_KBN
            , bool ISNOT_NEED_DELETE_FLG = false
            )
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(NameFlg, txt_gyoshaCd, txt_JigyoubaCd, txt_JigyoubaName
                    , HAISHUTSU_NIZUMI_GENBA_KBN, SAISHUU_SHOBUNJOU_KBN, SHOBUN_NIOROSHI_GENBA_KBN, TSUMIKAEHOKAN_KBN, ISNOT_NEED_DELETE_FLG);

                //空
                if (txt_gyoshaCd.Text == string.Empty || txt_JigyoubaCd.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new CommonGenbaDtoCls();
                Serch.GYOUSHA_CD = txt_gyoshaCd.Text;
                Serch.GENBA_CD = txt_JigyoubaCd.Text;
                Serch.ISNOT_NEED_DELETE_FLG = ISNOT_NEED_DELETE_FLG;

                //区分
                if (HAISHUTSU_NIZUMI_GENBA_KBN)
                {
                    Serch.HAISHUTSU_NIZUMI_GYOUSHA_KBN = "1";
                    Serch.HAISHUTSU_NIZUMI_GENBA_KBN = "1";
                }
                if (SAISHUU_SHOBUNJOU_KBN)
                {
                    Serch.SAISHUU_SHOBUNJOU_KBN = "1";
                    Serch.SHOBUN_NIOROSHI_GYOUSHA_KBN = "1";
                }
                if (SHOBUN_NIOROSHI_GENBA_KBN)
                {
                    Serch.SHOBUN_NIOROSHI_GENBA_KBN = "1";
                    Serch.SHOBUN_NIOROSHI_GYOUSHA_KBN = "1";
                }
                if (TSUMIKAEHOKAN_KBN)
                {
                    Serch.TSUMIKAEHOKAN_KBN = "1";
                }

                DataTable dt = this.mlogic.GetGenba(Serch);
                switch (dt.Rows.Count)
                {
                    case 1://正常
                        break;

                    default://エラー
                        ret = 2;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                }

                //現場名
                if (txt_JigyoubaName != null)
                {

                    switch (NameFlg)
                    {
                        case "All"://「正式名称1 + 正式名称2」をセットする。
                            txt_JigyoubaName.Text = dt.Rows[0]["GENBA_NAME"].ToString();
                            break;

                        case "Part1"://「正式名称1」をセットする。
                            txt_JigyoubaName.Text = dt.Rows[0]["GENBA_NAME1"].ToString();
                            break;

                        case "Ryakushou_Name"://「略称名」をセットする。
                            txt_JigyoubaName.Text = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetAddressJigyouba", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        // 20140603 katen 不具合No.4131 end‏

        // 20140610 katen 不具合No.4712 start‏
        #region マスタコードのチェック処理初期化（DataTableの準備）
        /// <summary>
        /// 電子廃棄物名称選択ポップアップ用データテーブル取得
        /// </summary>
        /// <param name="HaisyutuGyousyaCd">排出事業者CD。(加入者番号を絞り込むために利用)</param>
        /// <returns></returns>
        public void GetPopUpDenshiHaikiNameData(string HaisyutuGyousyaCd)
        {
            LogUtility.DebugMethodStart();
            try
            {
                DenshiHaikiNameCodeResult = new DataTable();
                SearchExistDTO = new SearchExistDTOCls();
                SearchExistDTO.HST_GYOUSHA_CD = HaisyutuGyousyaCd;
                DenshiHaikiNameCodeResult = DENSHI_HAIKI_NAME_SearchDao.GetDataForEntity(SearchExistDTO);

                // 列名とデータソース設
                this.form.cantxt_ElecHaikiName.PopupDataHeaderTitle = new string[] { "電子廃棄物名称CD", "電子廃棄物名称" };
                this.form.cantxt_ElecHaikiName.PopupDataSource = DenshiHaikiNameCodeResult;
                this.form.cbtn_ElecHaikibutuNameSan.PopupDataHeaderTitle = new string[] { "電子廃棄物名称CD", "電子廃棄物名称" };
                this.form.cbtn_ElecHaikibutuNameSan.PopupDataSource = DenshiHaikiNameCodeResult;
                //検索画面のタイトルを設定
                this.form.cantxt_ElecHaikiName.PopupDataSource.TableName = "電子廃棄物名称";
                this.form.cbtn_ElecHaikibutuNameSan.PopupDataSource.TableName = "電子廃棄物名称";
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 電子廃棄物種類選択ポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(物理名)</param>
        /// <returns></returns>
        public void GetPopUpDenshiHaikiShuruiData()
        {
            LogUtility.DebugMethodStart();
            try
            {
                DenshiHaikiShuruiCodeResult = new DataTable();
                DenshiHaikiShuruiCodeResult.TableName = "";
                SearchExistDTO = new SearchExistDTOCls();
                DenshiHaikiShuruiCodeResult = DENSHI_HAIKI_SHURUIE_SearchDao.GetDataForEntity(SearchExistDTO);

                // 列名とデータソース設
                this.form.cantxt_ElecHaikiShurui.PopupDataHeaderTitle = new string[] { "電子廃棄物種類CD", "電子廃棄物種類名称", "報告書分類CD", "報告書分類名" };
                this.form.cantxt_ElecHaikiShurui.PopupDataSource = DenshiHaikiShuruiCodeResult;
                this.form.cbtn_ElecHaikibutuShuruiSan.PopupDataHeaderTitle = new string[] { "電子廃棄物種類CD", "電子廃棄物種類名称", "報告書分類CD", "報告書分類名" };
                this.form.cbtn_ElecHaikibutuShuruiSan.PopupDataSource = DenshiHaikiShuruiCodeResult;
                //検索画面のタイトルを設定
                this.form.cantxt_ElecHaikiShurui.PopupDataSource.TableName = "電子廃棄物種類";
                this.form.cbtn_ElecHaikibutuShuruiSan.PopupDataSource.TableName = "電子廃棄物種類";
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion
        // 20140610 katen 不具合No.4712 end‏

        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            this.form.KOUFU_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.KOUFU_DATE_TO.BackColor = Constans.NOMAL_COLOR;
            // 入力されない場合
            if (string.IsNullOrEmpty(this.form.KOUFU_DATE_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.KOUFU_DATE_TO.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.form.KOUFU_DATE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.form.KOUFU_DATE_TO.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.KOUFU_DATE_FROM.IsInputErrorOccured = true;
                this.form.KOUFU_DATE_TO.IsInputErrorOccured = true;
                this.form.KOUFU_DATE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.KOUFU_DATE_TO.BackColor = Constans.ERROR_COLOR;
                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                if (this.form.KOUFU_DATE_KBN.Text == "1")
                {
                    string[] errorMsg = { "交付年月日From", "交付年月日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.KOUFU_DATE_KBN.Text == "2")
                {
                    string[] errorMsg = { "運搬終了日From", "運搬終了日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.KOUFU_DATE_KBN.Text == "3")
                {
                    string[] errorMsg = { "処分終了日From", "処分終了日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.KOUFU_DATE_KBN.Text == "4")
                {
                    string[] errorMsg = { "最終処分終了日From", "最終処分終了日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                this.form.KOUFU_DATE_FROM.Focus();
                return true;
            }
            return false;
        }
        #endregion
        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 end

        // 20141030 Houkakou 委託契約チェック start
        /// <summary>
        /// システム上必須な項目を非表示にします
        /// </summary>
        internal void HideColumnHeader()
        {
            foreach (DataGridViewColumn column in this.form.customDataGridView1.Columns)
            {
                if (column.Name.Equals("HST_GYOUSHA_CD_ERROR") ||
                    column.Name.Equals("HST_GENBA_CD_ERROR") ||
                    column.Name.Equals("HAIKI_SHURUI_CD_ERROR"))
                {
                    column.Visible = false;
                }
            }
        }

        /// 20141128 Houkakou 「運賃集計表」のダブルクリックを追加する　start
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KOUFU_DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.KOUFU_DATE_FROM;
            var ToTextBox = this.form.KOUFU_DATE_TO;
            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141128 Houkakou 「運賃集計表」のダブルクリックを追加する　end

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

        private void KOUFUBANNGOTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.KOUFUBANNGOFrom;
            var ToTextBox = this.form.KOUFUBANNGOTo;
            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
    }
}
