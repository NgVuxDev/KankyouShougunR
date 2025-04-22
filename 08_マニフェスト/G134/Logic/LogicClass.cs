using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.PaperManifest.JissekiHokoku.DAO;

namespace Shougun.Core.PaperManifest.JissekiHokoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.JissekiHokoku.Setting.ButtonSetting.xml";


        /// <summary> 親フォーム</summary>
        public BasePopForm parentbaseform { get; set; }

        public T_JISSEKI_HOUKOKU_ENTRY tjHoukokuEntry { get; set; }

        private List<T_JISSEKI_HOUKOKU_MANIFEST_DETAIL> tjHoukokuManiEntryList { get; set; }

        public List<T_JISSEKI_HOUKOKU_SBN_DETAIL> tjHoukokuSbnEntryList { get; set; }

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        public M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// 検索用Dao
        /// </summary>
        private EntryDAO EntryDao { get; set; }
        private IM_GENBADao GenbaDAO { get; set; }

        private ManiDetailDAO ManiDetailDao { get; set; }

        private SBNDetailDAO SbnDetailDao { get; set; }

        private IS_NUMBER_SYSTEMDao numberSystemDao { get; set; }

        /// <summary>
        /// システム情報のDao
        /// </summary>
        public IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// Form
        /// </summary>
        private UIHeader header;
        private UIForm form;
        private BasePopForm footer;

        /// <summary>共通</summary>
        Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic mlogic = null;

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


            this.Search_TME = new DataTable();
            this.Search_TME_Check = new DataTable();

            this.tjHoukokuEntry = new T_JISSEKI_HOUKOKU_ENTRY();

            this.tjHoukokuManiEntryList = new List<T_JISSEKI_HOUKOKU_MANIFEST_DETAIL>();

            this.tjHoukokuSbnEntryList = new List<T_JISSEKI_HOUKOKU_SBN_DETAIL>();

            this.EntryDao = DaoInitUtility.GetComponent<EntryDAO>();
            this.GenbaDAO = DaoInitUtility.GetComponent<IM_GENBADao>();


            this.ManiDetailDao = DaoInitUtility.GetComponent<ManiDetailDAO>();

            this.SbnDetailDao = DaoInitUtility.GetComponent<SBNDetailDAO>();

            this.numberSystemDao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();

            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

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

            var parentForm = (BasePopForm)this.form.Parent;

            //ヘッダーの初期化
            UIHeader targetHeader = (UIHeader)parentForm.headerForm;
            this.header = targetHeader;

            //フッターの初期化
            BasePopForm targetFooter = (BasePopForm)parentForm;
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
            var parentForm = (BasePopForm)this.form.Parent;
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

            var parentForm = (BasePopForm)this.form.Parent;

            //前年ボタン(F1)イベント生成
            parentForm.bt_func1.Click += new EventHandler(this.form.btnPrevious_Click);

            //次年ボタン(F2)イベント生成
            parentForm.bt_func2.Click += new EventHandler(this.form.btnNext_Click); 

            //処理施設ボタン(F3)イベント生成
            parentForm.bt_func3.Click += new EventHandler(this.form.bt_func3_Click);

            //運搬実績ボタン(F4)イベント生成
            parentForm.bt_func4.Click += new EventHandler(this.form.bt_func4_Click);

            // CSV出力ボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.bt_func6_Click);

            //実行ボタン(F9)イベント生成
            parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            //報告事業者Validatingイベント生成
            this.form.HOUKOKU_GYOUSHA_CD.Validating += new CancelEventHandler(this.form.HOUKOKU_GYOUSHA_CD_Validating);

            //報告事業場Validatingイベント生成
            this.form.HOUKOKU_GENBA_CD.Validating += new CancelEventHandler(this.form.HOUKOKU_GENBA_CD_Validating);

            //前回値保存の仕組み初期化
            this.form.EnterEventInit();
            LogUtility.DebugMethodEnd();
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

                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                KoufuDateFrom = this.footer.sysDate.ToString();
                KoufuDateTo = this.footer.sysDate.ToString();

                // 画面初期値をセット
                this.InitDisplay();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
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
        /// 画面初期化処理
        /// </summary>
        private void InitDisplay()
        {
            LogUtility.DebugMethodStart();
            try
            {
                this.header.HOKOKU_TANTO_NAME.SetResultText(string.Empty);
                this.header.txtDENMANI_KBN.Text = "1";
                this.header.txtNum_Syukeiari.Text = "1";

                this.form.HOZON_NAME.SetResultText(string.Empty);
                this.form.TEISHUTU_DATE.Value = this.footer.sysDate;
                this.form.HOUKOKU_GYOUSHA_CD.Text = string.Empty;
                this.form.HOUKOKU_GYOUSHA_NAME.Text = string.Empty;
                this.form.HOUKOKU_GENBA_CD.Text = string.Empty;
                this.form.HOUKOKU_GENBA_NAME.Text = string.Empty;
                this.form.txtGYOUSHA_KBN.Text = "1";
                this.form.txtJIGYOUJOU_KBN.Text = "1";
                this.form.CHIIKI_CD.Text = string.Empty;
                this.form.CHIIKI_NAME.Text = string.Empty;
                this.form.txtHOUKOKU_SHOSHIKI.Text = "1";
                this.form.txtTOKUBETSU_KANRI_KBN.Text = "1";
                this.form.txtKEN_KBN.Text = "3";
                this.form.txtHST_GYOUSHA_NAME_DISP_KBN.Text = "1";
                this.form.txtADDRESS_KBN.Text = "1";
                this.form.txtSAI_ITAKU_KBN.Text = "1";
                this.form.DATE_BEGIN.Value = (Convert.ToInt32(this.footer.sysDate.ToString("yyyy")) - 1).ToString() + "-04-01";
                this.form.DATE_END.Value = Convert.ToInt32(this.footer.sysDate.ToString("yyyy")) + "-03-31";

                //前年・翌年・FROM日付変更時に同様の処理を入れているので、見出し１行目の文言変更時はそちらも実施する
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP", false);
                ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();
                this.form.HOUKOKU_TITLE1.SetResultText(Convert.ToDateTime(this.form.DATE_BEGIN.Value).ToString("gg", ci) + "○○年度の産業廃棄物の処理の実績について、廃棄物の処理及び清掃に関する");
                this.form.HOUKOKU_TITLE2.SetResultText("法律施行細則第１４条第２項の規定により、次のとおり報告します。");
                // システム設定を読み込む
                M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
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

        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <returns></returns>
        internal Boolean SearchCheck()
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

                LogUtility.DebugMethodEnd(true);
                return true;
            }

            LogUtility.DebugMethodEnd(false);
            return false;
        }

        /// <summary>
        /// 必須チェックエラーフォーカス処理
        /// </summary>
        /// <returns></returns>
        public void SetErrorFocus()
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

        #region  実行処理(F9)

        /// <summary>
        /// 実行処理(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public void Jikou()
        {
            LogUtility.DebugMethodStart();
            try
            {
                this.form.isSearchErr = false;
                if (this.CheckDate())
                {
                    return;
                }
                // mainデータ
                this.setHoukokuEntity();

                this.SearchResult = this.EntryDao.GetDataForEntity(this.tjHoukokuEntry);

                int result = this.setDetailEntity();

                if (result == -1)
                {
                    return;
                }

                // トランザクション開始
                using (Transaction tran = new Transaction())
                {
                    this.EntryDao.Insert(this.tjHoukokuEntry);
                    for (int i = 0; i < this.tjHoukokuSbnEntryList.Count; i++)
                    {
                        T_JISSEKI_HOUKOKU_SBN_DETAIL SbnData = new T_JISSEKI_HOUKOKU_SBN_DETAIL();
                        SbnData = this.tjHoukokuSbnEntryList[i];
                        this.SbnDetailDao.Insert(SbnData);
                    }
                    for (int i = 0; i < this.tjHoukokuManiEntryList.Count; i++)
                    {
                        T_JISSEKI_HOUKOKU_MANIFEST_DETAIL ManiData = new T_JISSEKI_HOUKOKU_MANIFEST_DETAIL();
                        ManiData = this.tjHoukokuManiEntryList[i];
                        this.ManiDetailDao.Insert(ManiData);
                    }
                    tran.Commit();
                }

                // 帳票を印刷
                this.PrintView();

            }
            catch (Exception ex)
            {
                LogUtility.Error("Jikou", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                this.form.isSearchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        private void setHoukokuEntity()
        {
            LogUtility.DebugMethodStart();
            try
            {
                this.tjHoukokuEntry = new T_JISSEKI_HOUKOKU_ENTRY();


                this.tjHoukokuEntry.SYSTEM_ID = this.createSystemIdForJissekiHokoku();
                this.tjHoukokuEntry.SEQ = 1;
                this.tjHoukokuEntry.REPORT_ID = 1;

                // 和暦でDataTimeを文字列に変換する
                /*System.Globalization.CultureInfo ci =
                    new System.Globalization.CultureInfo("ja-JP", false);
                ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();

                this.tjHoukokuEntry.HOUKOKU_YEAR = Convert.ToDateTime(this.form.DATE_BEGIN.Value).ToString("gy年", ci);*/
                this.tjHoukokuEntry.HOUKOKU_YEAR = Convert.ToDateTime(this.form.DATE_BEGIN.Value).ToString("yyyy") + "年度";

                this.tjHoukokuEntry.DENMANI_KBN = Convert.ToInt16(this.header.txtDENMANI_KBN.Text);

                this.tjHoukokuEntry.HOZON_NAME = this.form.HOZON_NAME.Text;

                this.tjHoukokuEntry.TEISHUTU_DATE = Convert.ToDateTime(this.form.TEISHUTU_DATE.Value);

                this.tjHoukokuEntry.HOUKOKU_GYOUSHA_CD = this.form.HOUKOKU_GYOUSHA_CD.Text;

                this.tjHoukokuEntry.HOUKOKU_GENBA_CD = this.form.HOUKOKU_GENBA_CD.Text;

                this.tjHoukokuEntry.GYOUSHA_KBN = Convert.ToInt16(this.form.txtGYOUSHA_KBN.Text);

                this.tjHoukokuEntry.JIGYOUJOU_KBN = Convert.ToInt16(this.form.txtJIGYOUJOU_KBN.Text);

                this.tjHoukokuEntry.TEISHUTSU_CHIIKI_CD = this.form.CHIIKI_CD.Text;

                this.tjHoukokuEntry.TEISHUTSU_NAME = this.form.CHIIKI_NAME.Text;

                this.tjHoukokuEntry.HOUKOKU_SHOSHIKI = Convert.ToInt16(this.form.txtHOUKOKU_SHOSHIKI.Text);

                this.tjHoukokuEntry.HOUKOKU_TITLE1 = this.form.HOUKOKU_TITLE1.Text;

                this.tjHoukokuEntry.HOUKOKU_TITLE2 = this.form.HOUKOKU_TITLE2.Text;

                this.tjHoukokuEntry.TOKUBETSU_KANRI_KBN = Convert.ToInt16(this.form.txtTOKUBETSU_KANRI_KBN.Text);

                this.tjHoukokuEntry.DATE_BEGIN = Convert.ToDateTime(this.form.DATE_BEGIN.Value);

                this.tjHoukokuEntry.DATE_END = Convert.ToDateTime(this.form.DATE_END.Value);

                this.tjHoukokuEntry.KEN_KBN = Convert.ToInt16(this.form.txtKEN_KBN.Text);

                this.tjHoukokuEntry.HST_GYOUSHA_NAME_DISP_KBN = Convert.ToInt16(this.form.txtHST_GYOUSHA_NAME_DISP_KBN.Text);

                this.tjHoukokuEntry.ADDRESS_KBN = Convert.ToInt16(this.form.txtADDRESS_KBN.Text);

                this.tjHoukokuEntry.SAI_ITAKU_KBN = Convert.ToInt16(this.form.txtSAI_ITAKU_KBN.Text);

                this.tjHoukokuEntry.HOUKOKU_TANTO_NAME = this.header.HOKOKU_TANTO_NAME.Text;

                new DataBinderLogic<T_JISSEKI_HOUKOKU_ENTRY>(this.tjHoukokuEntry).SetSystemProperty(this.tjHoukokuEntry, false);

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

        /// <summary>
        /// のSYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt64 createSystemIdForJissekiHokoku()
        {
            SqlInt64 returnInt = 1;

            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = 400;

            using (Transaction tran = new Transaction())
            {
                var updateEntity = this.numberSystemDao.GetNumberSystemDataWithTableLock(entity);
                returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_SYSTEM();
                    updateEntity.DENSHU_KBN_CD = 400;
                    updateEntity.CURRENT_NUMBER = returnInt;
                    updateEntity.DELETE_FLG = false;
                    var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                    dataBinderEntry.SetSystemProperty(updateEntity, false);

                    this.numberSystemDao.Insert(updateEntity);
                }
                else
                {
                    updateEntity.CURRENT_NUMBER = returnInt;
                    this.numberSystemDao.Update(updateEntity);
                }

                tran.Commit();
            }

            return returnInt;
        }

        private int setDetailEntity()
        {
            LogUtility.DebugMethodStart();
            var messageShowLogic = new MessageBoxShowLogic();
            this.tjHoukokuManiEntryList = new List<T_JISSEKI_HOUKOKU_MANIFEST_DETAIL>();
            this.tjHoukokuSbnEntryList = new List<T_JISSEKI_HOUKOKU_SBN_DETAIL>();
            bool bunruiErrFlag = false;
            bool gyoushaErrFlag = false;

            long DETAIL_SYSTEM_ID = 0;

            if (this.SearchResult.Rows.Count == 0)
            {
                DialogResult result = messageShowLogic.MessageBoxShow("C001");
                return -1;
            }
            if (this.SearchResult != null && this.SearchResult.Rows.Count > 0)
            {
                foreach (DataRow dt in this.SearchResult.Rows)
                {
                    if (string.IsNullOrEmpty(dt["HST_GYOUSHA_CD"].ToString())
                        || string.IsNullOrEmpty(dt["HST_GENBA_CD"].ToString())
                        || string.IsNullOrEmpty(dt["SBN_GYOUSHA_CD"].ToString())
                        || string.IsNullOrEmpty(dt["SBN_GENBA_CD"].ToString())
                        || ("1".Equals(dt["ITAKU_KBN"].ToString()) && (string.IsNullOrEmpty(dt["ITAKUSAKI_CD"].ToString())
                                                       || string.IsNullOrEmpty(dt["ITAKU_GENBA_CD"].ToString())))
                        )
                    {
                        DialogResult result = messageShowLogic.MessageBoxShow("C077", "マニフェスト伝票", "マニチェック表");
                        if (result == DialogResult.Yes)
                        {
                            FormManager.OpenFormWithAuth("G124", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
                            // 20141209 koukouei 実績報告書　フィードバック対応 start
                            return -1;
                            // 20141209 koukouei 実績報告書　フィードバック対応 end
                        }
                        // 20141209 koukouei 実績報告書　フィードバック対応 start
                        break;
                        //return -1;
                        // 20141209 koukouei 実績報告書　フィードバック対応 end
                    }
                }

                Dictionary<string, int> tempDic = new Dictionary<string, int>();

                foreach (DataRow dt in this.SearchResult.Rows)
                {

                    if (string.IsNullOrEmpty(dt["HOUKOKUSHO_BUNRUI_CD"].ToString()))
                    {
                        if (!bunruiErrFlag)
                        {
                            DialogResult result = messageShowLogic.MessageBoxShow("C076", "マスター保守＞地域別分類", "マニチェック表", "分類コード");
                            if (result == DialogResult.No)
                            {
                                return -1;
                            }

                            bunruiErrFlag = true;
                        }
                    }

                    T_JISSEKI_HOUKOKU_MANIFEST_DETAIL tjHoukokuManiEntry = new T_JISSEKI_HOUKOKU_MANIFEST_DETAIL();
                    T_JISSEKI_HOUKOKU_SBN_DETAIL tjHoukokuSbnEntry = new T_JISSEKI_HOUKOKU_SBN_DETAIL();

                    bool new_row = true;
                    long row_detail_id = 0;
                    if (tempDic.ContainsKey(Convert.ToString(dt["HOUKOKUSHO_BUNRUI_CD"]) + "_" + Convert.ToString(dt["HST_GYOUSHA_CD"]) + "_" + Convert.ToString(dt["HST_GENBA_CD"]) + "_" + Convert.ToString(dt["SBN_GYOUSHA_CD"]) + "_" + Convert.ToString(dt["SBN_GENBA_CD"]) + "_" + Convert.ToString(dt["ITAKUSAKI_CD"]) + "_" + Convert.ToString(dt["ITAKU_GENBA_CD"]) + "_" + Convert.ToString(dt["SHOBUN_HOUHOU_CD"])))
                    {
                        var i = tempDic[Convert.ToString(dt["HOUKOKUSHO_BUNRUI_CD"]) + "_" + Convert.ToString(dt["HST_GYOUSHA_CD"]) + "_" + Convert.ToString(dt["HST_GENBA_CD"]) + "_" + Convert.ToString(dt["SBN_GYOUSHA_CD"]) + "_" + Convert.ToString(dt["SBN_GENBA_CD"]) + "_" + Convert.ToString(dt["ITAKUSAKI_CD"]) + "_" + Convert.ToString(dt["ITAKU_GENBA_CD"]) + "_" + Convert.ToString(dt["SHOBUN_HOUHOU_CD"])];
                        T_JISSEKI_HOUKOKU_SBN_DETAIL sbnData = this.tjHoukokuSbnEntryList[i];

                        if (!string.IsNullOrEmpty(dt["KANSAN_SUU"].ToString()))
                        {
                            if (this.tjHoukokuSbnEntryList[i].JYUTAKU_RYOU.IsNull)
                            {
                                this.tjHoukokuSbnEntryList[i].JYUTAKU_RYOU = 0;
                            }
                            if (this.tjHoukokuSbnEntryList[i].SBN_RYOU.IsNull)
                            {
                                this.tjHoukokuSbnEntryList[i].SBN_RYOU = 0;
                            }
                            this.tjHoukokuSbnEntryList[i].JYUTAKU_RYOU = this.tjHoukokuSbnEntryList[i].JYUTAKU_RYOU.Value + Convert.ToDecimal(dt["KANSAN_SUU"].ToString());
                            this.tjHoukokuSbnEntryList[i].SBN_RYOU = this.tjHoukokuSbnEntryList[i].SBN_RYOU.Value + Convert.ToDecimal(dt["KANSAN_SUU"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dt["GENNYOU_SUU"].ToString()))
                        {
                            if (this.tjHoukokuSbnEntryList[i].SBN_AFTER_RYOU.IsNull)
                            {
                                this.tjHoukokuSbnEntryList[i].SBN_AFTER_RYOU = 0;
                            }
                            this.tjHoukokuSbnEntryList[i].SBN_AFTER_RYOU = this.tjHoukokuSbnEntryList[i].SBN_AFTER_RYOU.Value + Convert.ToDecimal(dt["GENNYOU_SUU"].ToString());

                            if (!string.IsNullOrEmpty(dt["ITAKU_GENBA_CD"].ToString()) && !string.IsNullOrEmpty(dt["ITAKUSAKI_CD"].ToString()))
                            {
                                //158212 S
                                //if ((this.tjHoukokuEntry.SAI_ITAKU_KBN == 2 && "1".Equals(dt["UPN_SAKI_KBN"].ToString()) && dt["ITAKU_JISHA_KBN"].Equals(false)) || this.tjHoukokuEntry.SAI_ITAKU_KBN == 1)
                                if ((this.tjHoukokuEntry.SAI_ITAKU_KBN == 2 && Convert.ToBoolean(dt["ITAKU_JISHA_KBN"]).Equals(false)) || this.tjHoukokuEntry.SAI_ITAKU_KBN == 1)
                                //158212 E
                                {
                                    if (this.tjHoukokuSbnEntryList[i].ITAKU_RYOU.IsNull)
                                    {
                                        this.tjHoukokuSbnEntryList[i].ITAKU_RYOU = 0;
                                    }
                                    this.tjHoukokuSbnEntryList[i].ITAKU_RYOU = this.tjHoukokuSbnEntryList[i].ITAKU_RYOU.Value + Convert.ToDecimal(dt["GENNYOU_SUU"].ToString());
                                }
                            }
                        }
                        new_row = false;
                        row_detail_id = sbnData.DETAIL_SYSTEM_ID.Value;
                    }

                    if (new_row)
                    {
                        SqlInt64 ID = this.createSystemIdForJissekiHokoku();
                        DETAIL_SYSTEM_ID = ID.Value;
                        // システムID
                        tjHoukokuSbnEntry.SYSTEM_ID = this.tjHoukokuEntry.SYSTEM_ID;

                        // 枝番
                        tjHoukokuSbnEntry.SEQ = this.tjHoukokuEntry.SEQ;

                        // 明細システムID
                        tjHoukokuSbnEntry.DETAIL_SYSTEM_ID = DETAIL_SYSTEM_ID;

                        // 帳票ID
                        tjHoukokuSbnEntry.REPORT_ID = 1;

                        // 報告書式
                        tjHoukokuSbnEntry.HOUKOKU_SHOSHIKI_KBN = this.tjHoukokuEntry.HOUKOKU_SHOSHIKI;

                        // 保存名
                        tjHoukokuSbnEntry.HOZON_NAME = this.tjHoukokuEntry.HOZON_NAME;

                        // 報告年度
                        //tjHoukokuSbnEntry.HOUKOKU_YEAR = this.tjHoukokuEntry.HOUKOKU_YEAR;
                        // 和暦でDataTimeを文字列に変換する
                        System.Globalization.CultureInfo ci =
                        new System.Globalization.CultureInfo("ja-JP", false);
                        ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();

                        tjHoukokuSbnEntry.HOUKOKU_YEAR = Convert.ToDateTime(this.form.DATE_BEGIN.Value).ToString("gy年", ci);

                        // 提出先地域CD
                        tjHoukokuSbnEntry.TEISHUTSUSAKI_CHIIKI_CD = this.tjHoukokuEntry.TEISHUTSU_CHIIKI_CD;

                        // 特管区分
                        tjHoukokuSbnEntry.TOKUBETSU_KANRI_KBN = this.tjHoukokuEntry.TOKUBETSU_KANRI_KBN;

                        // 事業場区分
                        tjHoukokuSbnEntry.JIGYOUJOU_KBN = this.tjHoukokuEntry.JIGYOUJOU_KBN;

                        tjHoukokuSbnEntry.KEN_KBN = this.tjHoukokuEntry.KEN_KBN;

                        tjHoukokuSbnEntry.GYOUSHA_KBN = this.tjHoukokuEntry.GYOUSHA_KBN;

                        tjHoukokuSbnEntry.HOUKOKUSHO_BUNRUI_CD = dt["HOUKOKUSHO_BUNRUI_CD"].ToString();
                        tjHoukokuSbnEntry.HOUKOKUSHO_BUNRUI_NAME = dt["HOUKOKUSHO_BUNRUI_NAME"].ToString();

                        //tjHoukokuSbnEntry.HAIKI_SHURUI_CD = dt["HAIKI_SHURUI_CD"].ToString();

                        //tjHoukokuSbnEntry.HAIKI_SHURUI_NAME = dt["HAIKI_SHURUI_NAME"].ToString();

                        tjHoukokuSbnEntry.SAIITAKU_KYOKA_NO = dt["SAIITAKU_KYOKA_NO"].ToString();

                        tjHoukokuSbnEntry.HST_GYOUSHA_CD = dt["HST_GYOUSHA_CD"].ToString();

                        tjHoukokuSbnEntry.HST_GYOUSHA_NAME = dt["HST_GYOUSHA_NAME"].ToString();

                        tjHoukokuSbnEntry.HST_GYOUSHA_ADDRESS = this.SetAddressString(dt["HST_GYOUSHA_ADDRESS"].ToString());

                        tjHoukokuSbnEntry.HST_GENBA_CD = dt["HST_GENBA_CD"].ToString();

                        tjHoukokuSbnEntry.HST_GENBA_NAME = dt["HST_GENBA_NAME"].ToString();

                        tjHoukokuSbnEntry.HST_GENBA_ADDRESS = this.SetAddressString(dt["HST_GENBA_ADDRESS"].ToString());

                        tjHoukokuSbnEntry.HST_GENBA_CHIIKI_CD = dt["HST_GENBA_CHIIKI_CD"].ToString();

                        tjHoukokuSbnEntry.JYUTAKU_KBN = dt["JYUTAKU_KBN"].ToString();

                        if (!string.IsNullOrEmpty(dt["KANSAN_SUU"].ToString()))
                        {
                            tjHoukokuSbnEntry.JYUTAKU_RYOU = Convert.ToDecimal(dt["KANSAN_SUU"].ToString());
                            tjHoukokuSbnEntry.SBN_RYOU = Convert.ToDecimal(dt["KANSAN_SUU"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dt["GENNYOU_SUU"].ToString()))
                        {
                            tjHoukokuSbnEntry.SBN_AFTER_RYOU = Convert.ToDecimal(dt["GENNYOU_SUU"].ToString());
                            if (!string.IsNullOrEmpty(dt["ITAKU_GENBA_CD"].ToString()) && !string.IsNullOrEmpty(dt["ITAKUSAKI_CD"].ToString()))
                            {
                                //158212 S
                                //if ((this.tjHoukokuEntry.SAI_ITAKU_KBN == 2 && "1".Equals(dt["UPN_SAKI_KBN"].ToString()) && dt["ITAKU_JISHA_KBN"].Equals(false)) || this.tjHoukokuEntry.SAI_ITAKU_KBN == 1)
                                if ((this.tjHoukokuEntry.SAI_ITAKU_KBN == 2 && Convert.ToBoolean(dt["ITAKU_JISHA_KBN"]).Equals(false)) || this.tjHoukokuEntry.SAI_ITAKU_KBN == 1)
                                //158212 E
                                {
                                    tjHoukokuSbnEntry.ITAKU_RYOU = Convert.ToDecimal(dt["GENNYOU_SUU"].ToString());
                                }
                            }
                        }

                        tjHoukokuSbnEntry.JYUTAKU_UNIT_NAME = dt["JYUTAKU_UNIT_NAME"].ToString();

                        tjHoukokuSbnEntry.SHOBUN_HOUHOU_CD = dt["SHOBUN_HOUHOU_CD"].ToString();
                        tjHoukokuSbnEntry.SHOBUN_HOUHOU_NAME = dt["SHOBUN_HOUHOU_NAME"].ToString();
                        tjHoukokuSbnEntry.SBN_GYOUSHA_CD = dt["SBN_GYOUSHA_CD"].ToString();
                        tjHoukokuSbnEntry.SBN_GYOUSHA_NAME = dt["SBN_GYOUSHA_NAME"].ToString();
                        tjHoukokuSbnEntry.SBN_GYOUSHA_ADDRESS = this.SetAddressString(dt["SBN_GYOUSHA_ADDRESS"].ToString());
                        tjHoukokuSbnEntry.SBN_GENBA_CD = dt["SBN_GENBA_CD"].ToString();
                        tjHoukokuSbnEntry.SBN_GENBA_NAME = dt["SBN_GENBA_NAME"].ToString();
                        tjHoukokuSbnEntry.SBN_GENBA_ADDRESS = this.SetAddressString(dt["SBN_GENBA_ADDRESS"].ToString());
                        tjHoukokuSbnEntry.SBN_GENBA_CHIIKI_CD = dt["SBN_GENBA_CHIIKI_CD"].ToString();
                        tjHoukokuSbnEntry.ITAKUSAKI_CD = dt["ITAKUSAKI_CD"].ToString();
                        tjHoukokuSbnEntry.ITAKU_GENBA_CD = dt["ITAKU_GENBA_CD"].ToString();
                        tjHoukokuSbnEntry.ITAKUSAKI_NAME = dt["ITAKUSAKI_NAME"].ToString();
                        tjHoukokuSbnEntry.ITAKUSAKI_ADDRESS = this.SetAddressString(dt["ITAKUSAKI_ADDRESS"].ToString());
                        tjHoukokuSbnEntry.ITAKUSAKI_CHIIKI_CD = dt["ITAKUSAKI_CHIIKI_CD"].ToString();
                        tjHoukokuSbnEntry.ITAKUSAKI_KYOKA_NO = dt["ITAKUSAKI_KYOKA_NO"].ToString();
                        tjHoukokuSbnEntry.HST_JOU_CHIIKI_CD = dt["HST_JOU_CHIIKI_CD"].ToString();

                        if (!string.IsNullOrEmpty(dt["HST_JOU_TODOUFUKEN_CD"].ToString()))
                        {
                            tjHoukokuSbnEntry.HST_JOU_TODOUFUKEN_CD = Convert.ToInt16(dt["HST_JOU_TODOUFUKEN_CD"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dt["HST_KEN_KBN"].ToString()))
                        {
                            tjHoukokuSbnEntry.HST_KEN_KBN = Convert.ToInt16(dt["HST_KEN_KBN"].ToString());
                        }

                        this.tjHoukokuSbnEntryList.Add(tjHoukokuSbnEntry);
                        tempDic.Add(Convert.ToString(dt["HOUKOKUSHO_BUNRUI_CD"]) + "_" + Convert.ToString(dt["HST_GYOUSHA_CD"]) + "_" + Convert.ToString(dt["HST_GENBA_CD"]) + "_" + Convert.ToString(dt["SBN_GYOUSHA_CD"]) + "_" + Convert.ToString(dt["SBN_GENBA_CD"]) + "_" + Convert.ToString(dt["ITAKUSAKI_CD"]) + "_" + Convert.ToString(dt["ITAKU_GENBA_CD"]) + "_" + Convert.ToString(dt["SHOBUN_HOUHOU_CD"]), tjHoukokuSbnEntryList.Count() - 1);
                        row_detail_id = DETAIL_SYSTEM_ID;
                    }

                    bool mani_new_row = true;
                    int detail_row_no = 0;
                    for (int i = 0; i < this.tjHoukokuManiEntryList.Count; i++)
                    {
                        T_JISSEKI_HOUKOKU_MANIFEST_DETAIL maniData = this.tjHoukokuManiEntryList[i];
                        if (maniData.DETAIL_SYSTEM_ID.Value.Equals(row_detail_id))
                        {
                            if (!string.IsNullOrEmpty(dt["SYSTEM_ID"].ToString()))
                            {
                                long system_id = Convert.ToInt64(dt["SYSTEM_ID"].ToString());
                                int seq = Convert.ToInt32(dt["SEQ"].ToString());
                                if (maniData.MANI_SYSTEM_ID == system_id && maniData.MANI_SEQ == seq)
                                {
                                    mani_new_row = false;
                                    break;
                                }
                            }
                            else if (!string.IsNullOrEmpty(dt["KANRI_ID"].ToString()))
                            {
                                string kanri_id = dt["KANRI_ID"].ToString();
                                int den_seq = Convert.ToInt32(dt["DEN_SEQ"].ToString());
                                if (maniData.DEN_MANI_KANRI_ID.Equals(kanri_id) && maniData.DEN_MANI_SEQ == den_seq)
                                {
                                    mani_new_row = false;
                                    break;
                                }
                            }

                            if (detail_row_no < maniData.DETAIL_ROW_NO.Value)
                            {
                                detail_row_no = maniData.DETAIL_ROW_NO.Value;
                            }
                        }
                    }

                    if (mani_new_row)
                    {
                        tjHoukokuManiEntry.SYSTEM_ID = this.tjHoukokuEntry.SYSTEM_ID;
                        tjHoukokuManiEntry.SEQ = this.tjHoukokuEntry.SEQ;
                        tjHoukokuManiEntry.DETAIL_SYSTEM_ID = row_detail_id;
                        tjHoukokuManiEntry.DETAIL_ROW_NO = detail_row_no + 1;
                        tjHoukokuManiEntry.REPORT_ID = 1;
                        tjHoukokuManiEntry.HAIKI_KBN_CD = Convert.ToInt16(dt["HAIKI_KBN_CD"].ToString());
                        if (!string.IsNullOrEmpty(dt["SYSTEM_ID"].ToString()))
                        {
                            tjHoukokuManiEntry.MANI_SYSTEM_ID = Convert.ToInt64(dt["SYSTEM_ID"].ToString());
                            tjHoukokuManiEntry.MANI_SEQ = Convert.ToInt32(dt["SEQ"].ToString());
                        }
                        else if (!string.IsNullOrEmpty(dt["KANRI_ID"].ToString()))
                        {
                            tjHoukokuManiEntry.DEN_MANI_KANRI_ID = dt["KANRI_ID"].ToString();
                            tjHoukokuManiEntry.DEN_MANI_SEQ = Convert.ToInt32(dt["DEN_SEQ"].ToString());
                        }
                        tjHoukokuManiEntry.MANIFEST_ID = dt["MANIFEST_ID"].ToString();
                        this.tjHoukokuManiEntryList.Add(tjHoukokuManiEntry);
                    }

                }

                this.tjHoukokuSbnEntryList = (from temp in this.tjHoukokuSbnEntryList
                                              orderby temp.HST_KEN_KBN ascending,
                                                      temp.HST_GENBA_CHIIKI_CD ascending,
                                                      temp.HOUKOKUSHO_BUNRUI_CD ascending,
                                                      temp.HST_JOU_TODOUFUKEN_CD ascending,
                                                      temp.HST_GYOUSHA_CD ascending,
                                                      temp.HST_GENBA_CD ascending,
                                                      temp.SHOBUN_HOUHOU_CD ascending,
                                                      temp.SBN_GYOUSHA_CD ascending,
                                                      temp.SBN_GENBA_CD ascending,
                                                      temp.SBN_GENBA_CHIIKI_CD ascending,
                                                      temp.ITAKUSAKI_CD ascending,
                                                      temp.ITAKU_GENBA_CD ascending,
                                                      temp.ITAKUSAKI_CHIIKI_CD ascending
                                              select temp).ToList();

            }

            LogUtility.DebugMethodEnd();
            return 0;
        }

        public virtual void PrintView()
        {
            if (this.tjHoukokuSbnEntryList.Count == 0)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("I008", "データ");
                return;
            }
            FormReportPrintPopup formReport;

            DataTable headerData = new DataTable();
            headerData.Columns.Add("HOUKOKU_YEAR");                      // 報告日期
            headerData.Columns.Add("TEISHUTSU_NAME");                    // 提出先名
            headerData.Columns.Add("GYOUSHA_ADDRESS");                   // 住所
            headerData.Columns.Add("GYOUSHA_NAME");                      // 氏名又は名称1(業者名1+業者名2)
            headerData.Columns.Add("GYOUSHA_DAIHYOU");                   // 氏名又は名称2(代表者)
            headerData.Columns.Add("GYOUSHA_TEL");                       // 電話番号
            headerData.Columns.Add("HOUKOKU_TANTO_NAME");                // 担当者の氏名
            headerData.Columns.Add("HOUKOKU_TITLE1");                    // 報告書見出1
            headerData.Columns.Add("HOUKOKU_TITLE2");                    // 報告書見出2
            headerData.Columns.Add("KYOKA_DATA");                        // 許可年月日
            headerData.Columns.Add("KYOKA_NO");                          // 許可番号
            headerData.Columns.Add("HOUKOKU_SHOSHIKI");                  // HOKOKU_SYOSHIKI
            headerData.Columns.Add("TOKUBETSU_KANRI_KBN");               // 特管区分

            DataTable detailData = new DataTable();
            detailData.Columns.Add("HOUKOKUSHO_BUNRUI_NAME");                 // (特別管理)産業廃棄物の種類
            detailData.Columns.Add("HOUKOKUSHO_BUNRUI_CD");                   // 廃棄物コード
            detailData.Columns.Add("GYOUSHA_NAME");                      // 明細-排出事業者(氏名又は名称)
            detailData.Columns.Add("HST_GENBA_ADDRESS");                 // 排出事業場(排出元)の所在地
            detailData.Columns.Add("JYUTAKU_RYOU");                      // 受託量
            detailData.Columns.Add("JYUTAKU_KBN");                       // 備考
            detailData.Columns.Add("SHOBUN_HOUHOU_NAME");                // 処分方法
            detailData.Columns.Add("SBN_RYOU");                          // 処分量
            detailData.Columns.Add("SBN_AFTER_RYOU");                    // 処分後量
            detailData.Columns.Add("SBN_GENBA_ADDRESS");                 // 処分の場所
            detailData.Columns.Add("ITAKUSAKI_KYOKA_NO");                // 許可番号
            detailData.Columns.Add("ITAKUSAKI_NAME");                    // 委託先の氏名又は名称
            detailData.Columns.Add("ITAKUSAKI_ADDRESS");                 // 委託先の住所
            detailData.Columns.Add("ITAKU_RYOU");                        // 委託量
            detailData.Columns.Add("GROUP_KEY");                         // 排出事業場地域CD
            detailData.Columns.Add("HST_GENBA_CHIIKI_NAME");             // 排出事業場地域名称

            IM_CHIIKIDao mChiikiDao = DaoInitUtility.GetComponent<IM_CHIIKIDao>();
            M_CHIIKI mChiikiData = new M_CHIIKI();

            IM_CHIIKIBETSU_KYOKADao mchiikiBetsuDao = DaoInitUtility.GetComponent<IM_CHIIKIBETSU_KYOKADao>();
            M_CHIIKIBETSU_KYOKA mchiikiBetsuData = new M_CHIIKIBETSU_KYOKA();

            IM_GYOUSHADao mGyoshaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            M_GYOUSHA mGyoushaData = new M_GYOUSHA();

            IM_TODOUFUKENDao mTodoufukenDao = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            M_TODOUFUKEN mTodoufukenData = new M_TODOUFUKEN();

            List<T_JISSEKI_HOUKOKU_SBN_DETAIL> detailList = new List<T_JISSEKI_HOUKOKU_SBN_DETAIL>();

            T_JISSEKI_HOUKOKU_ENTRY entry = new T_JISSEKI_HOUKOKU_ENTRY();

            detailList = this.tjHoukokuSbnEntryList;
            entry = this.tjHoukokuEntry;

            DataRow head = headerData.NewRow();
            // Header情報
            // 提出先名
            mChiikiData = mChiikiDao.GetDataByCd(entry.TEISHUTSU_CHIIKI_CD);

            // 知事または市長名(GOV_OR_MAY_NAME)あります。
            // CHIIKI_NAMEに、xxx県orxxx市があります。
            if (mChiikiData != null)
            {
                if (!string.IsNullOrEmpty(mChiikiData.GOV_OR_MAY_NAME) && !string.IsNullOrEmpty(mChiikiData.CHIIKI_NAME))
                {
                    switch (mChiikiData.CHIIKI_NAME.Substring(mChiikiData.CHIIKI_NAME.Length - 1, 1))
                    {
                        case "県":
                        case "都":
                        case "道":
                        case "府":
                            head["TEISHUTSU_NAME"] = mChiikiData.CHIIKI_NAME + "知事　　" + mChiikiData.GOV_OR_MAY_NAME + "　殿";
                            break;
                        case "市":
                            head["TEISHUTSU_NAME"] = mChiikiData.CHIIKI_NAME + "市長　　" + mChiikiData.GOV_OR_MAY_NAME + "　殿";
                            break;
                    }
                }
            }

            // 産業廃棄物処分業 ・ 特別管理産業廃棄物処分業許可データを取得する
            // 20141230 ブン 許可区分設定の変更 start
            //mchiikiBetsuData.KYOKA_KBN = entry.GYOUSHA_KBN;
            if (entry.GYOUSHA_KBN == 1)
            {
                mchiikiBetsuData.KYOKA_KBN = 2;
            }
            else if (entry.GYOUSHA_KBN == 2)
            {
                mchiikiBetsuData.KYOKA_KBN = 3;
            }
            // 20141230 ブン 許可区分設定の変更 end
            mchiikiBetsuData.GYOUSHA_CD = entry.HOUKOKU_GYOUSHA_CD;
            mchiikiBetsuData.GENBA_CD = entry.HOUKOKU_GENBA_CD;
            mchiikiBetsuData.CHIIKI_CD = entry.TEISHUTSU_CHIIKI_CD;
            mchiikiBetsuData = mchiikiBetsuDao.GetDataByPrimaryKey(mchiikiBetsuData);
            // (1.普通)廃棄物
            if (entry.TOKUBETSU_KANRI_KBN.Value == 1 && mchiikiBetsuData != null)
            {
                if (!mchiikiBetsuData.FUTSUU_KYOKA_BEGIN.IsNull)
                {
                    head["KYOKA_DATA"] = mchiikiBetsuData.FUTSUU_KYOKA_BEGIN.Value.ToString();
                }
                head["KYOKA_NO"] = mchiikiBetsuData.FUTSUU_KYOKA_NO;
            }
            // (2.特別)廃棄物
            if (entry.TOKUBETSU_KANRI_KBN.Value == 2 && mchiikiBetsuData != null)
            {
                if (!mchiikiBetsuData.TOKUBETSU_KYOKA_BEGIN.IsNull)
                {
                    head["KYOKA_DATA"] = mchiikiBetsuData.TOKUBETSU_KYOKA_BEGIN.Value.ToString();
                }
                head["KYOKA_NO"] = mchiikiBetsuData.TOKUBETSU_KYOKA_NO;
            }

            mGyoushaData = mGyoshaDao.GetDataByCd(entry.HOUKOKU_GYOUSHA_CD);

            if (mGyoushaData != null)
            {

                mTodoufukenData = mTodoufukenDao.GetDataByCd(mGyoushaData.GYOUSHA_TODOUFUKEN_CD.Value.ToString());

            }

            if (mGyoushaData != null)
            {
                head["GYOUSHA_ADDRESS"] = mTodoufukenData.TODOUFUKEN_NAME + mGyoushaData.GYOUSHA_ADDRESS1 + mGyoushaData.GYOUSHA_ADDRESS2;
                head["GYOUSHA_NAME"] = mGyoushaData.GYOUSHA_NAME1 + mGyoushaData.GYOUSHA_NAME2;
                head["GYOUSHA_DAIHYOU"] = mGyoushaData.GYOUSHA_DAIHYOU;
                head["GYOUSHA_TEL"] = mGyoushaData.GYOUSHA_TEL;
            }
            head["HOUKOKU_YEAR"] = entry.DATE_BEGIN;
            head["HOUKOKU_TANTO_NAME"] = entry.HOUKOKU_TANTO_NAME;
            head["HOUKOKU_TITLE1"] = entry.HOUKOKU_TITLE1;
            head["HOUKOKU_TITLE2"] = entry.HOUKOKU_TITLE2;
            head["HOUKOKU_SHOSHIKI"] = entry.HOUKOKU_SHOSHIKI.Value.ToString();
            head["TOKUBETSU_KANRI_KBN"] = entry.TOKUBETSU_KANRI_KBN;

            headerData.Rows.Add(head);

            //detailList = (from temp in detailList
            //              orderby temp.HST_KEN_KBN ascending,
            //                      temp.HST_GENBA_CHIIKI_CD ascending,
            //                      temp.HOUKOKUSHO_BUNRUI_CD ascending,
            //                      temp.HST_JOU_TODOUFUKEN_CD ascending,
            //                      temp.HST_GYOUSHA_CD ascending
            //              select temp).ToList();
            // 明細-明細情報
            foreach (T_JISSEKI_HOUKOKU_SBN_DETAIL dt in detailList)
            {
                DataRow row = detailData.NewRow();
                row["HOUKOKUSHO_BUNRUI_NAME"] = dt.HOUKOKUSHO_BUNRUI_NAME;
                row["HOUKOKUSHO_BUNRUI_CD"] = dt.HOUKOKUSHO_BUNRUI_CD;
                // 業者名
                if (entry.HST_GYOUSHA_NAME_DISP_KBN == 1)
                {
                    row["GYOUSHA_NAME"] = dt.HST_GYOUSHA_NAME;
                }

                // 現場名
                if (entry.HST_GYOUSHA_NAME_DISP_KBN == 2)
                {
                    row["GYOUSHA_NAME"] = dt.HST_GENBA_NAME;
                }

                row["HST_GENBA_ADDRESS"] = dt.HST_GENBA_ADDRESS;
                if (!dt.JYUTAKU_RYOU.IsNull)
                {
                    row["JYUTAKU_RYOU"] = ConvertSuuryo(dt.JYUTAKU_RYOU);
                }
                else
                {
                    row["JYUTAKU_RYOU"] = "0";
                }
                row["JYUTAKU_KBN"] = dt.JYUTAKU_KBN;
                row["SHOBUN_HOUHOU_NAME"] = dt.SHOBUN_HOUHOU_NAME;
                if (!dt.SBN_RYOU.IsNull)
                {
                    row["SBN_RYOU"] = ConvertSuuryo(dt.SBN_RYOU);
                }
                else
                {
                    row["SBN_RYOU"] = "0";
                }
                if (!dt.SBN_AFTER_RYOU.IsNull)
                {
                    row["SBN_AFTER_RYOU"] = ConvertSuuryo(dt.SBN_AFTER_RYOU);
                }
                row["SBN_GENBA_ADDRESS"] = dt.SBN_GENBA_ADDRESS;
                row["ITAKUSAKI_KYOKA_NO"] = dt.ITAKUSAKI_KYOKA_NO;
                row["ITAKUSAKI_NAME"] = dt.ITAKUSAKI_NAME;
                row["ITAKUSAKI_ADDRESS"] = dt.ITAKUSAKI_ADDRESS;
                if (!dt.ITAKU_RYOU.IsNull)
                {
                    row["ITAKU_RYOU"] = ConvertSuuryo(dt.ITAKU_RYOU);
                }
                else
                {
                    if (!dt.ITAKU_KBN.IsNull)
                    {
                        if ("1".Equals(dt.ITAKU_KBN.Value.ToString()))
                        {
                            row["ITAKU_RYOU"] = "0";
                        }
                    }
                }
                // GROUPKEY
                row["GROUP_KEY"] = dt.HOUKOKUSHO_BUNRUI_CD + dt.HST_GENBA_CHIIKI_CD + dt.HST_KEN_KBN.Value.ToString();

                mChiikiData = mChiikiDao.GetDataByCd(dt.HST_GENBA_CHIIKI_CD);
                if (mChiikiData != null)
                {
                    row["HST_GENBA_CHIIKI_NAME"] = dt.HST_GENBA_CHIIKI_CD == dt.SBN_GENBA_CHIIKI_CD ?
                                                   mChiikiData.CHIIKI_NAME + "内排出元処分量合計" :
                                                   mChiikiData.CHIIKI_NAME + "の排出元からの処分量合計";
                    if (dt.HST_KEN_KBN.Value == 1)
                    {
                        row["HST_GENBA_CHIIKI_NAME"] = mChiikiData.CHIIKI_NAME + "内排出元処分量合計";
                    }
                    else if (dt.HST_KEN_KBN.Value == 2)
                    {
                        row["HST_GENBA_CHIIKI_NAME"] = mChiikiData.CHIIKI_NAME + "の排出元からの処分量合計";
                    }
                }

                detailData.Rows.Add(row);
            }

            ReportInfoR396 reportInfo = new ReportInfoR396();
            reportInfo.Title = "実績報告書（処分実績）";
            reportInfo.R396_Reprt(headerData, detailData);
            formReport = new FormReportPrintPopup(reportInfo, "R396");
            formReport.Caption = "業務報告書";

            // 印刷アプリ初期動作(プレビュー)
            formReport.PrintInitAction = 2;
            formReport.PrintXPS();

            formReport.Dispose();
        }

        #endregion

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
            //取得件数
            return 0;
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
        /// 数量フォーマット
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertSuuryo(SqlDecimal value)
        {
            string result = string.Empty;
            string format = "#,###.#";
            if (!string.IsNullOrEmpty(this.sysInfoEntity.MANIFEST_SUURYO_FORMAT))
            {
                format = this.sysInfoEntity.MANIFEST_SUURYO_FORMAT;
            }
            if (!value.IsNull)
            {
                result = value.Value.ToString(format);
            }
            return result;
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
            this.header.label3.BackColor = BackColor;
            LogUtility.DebugMethodEnd(strTitleName, BackColor);
        }

        /// <summary>
        /// ＣＳＶ出力項目選択（処分実績）ポップアップ表示
        /// </summary>
        /// <returns>true:実行された場合, false:キャンセルされた場合</returns>
        internal bool ShowCsvPopup()
        {
            bool ret = false;
            try
            {
                if (this.CheckDate())
                {
                    return ret;
                }
                // mainデータ
                this.setHoukokuEntity();

                bool isSyukei = (this.header.txtNum_Syukeiari.Text == "1");

                FormManager.OpenFormModal("G609", this.tjHoukokuEntry, isSyukei);
                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowCsvPopup", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            return ret;
        }

        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            this.form.DATE_BEGIN.BackColor = Constans.NOMAL_COLOR;
            this.form.DATE_END.BackColor = Constans.NOMAL_COLOR;
            // 入力されない場合
            if (string.IsNullOrEmpty(this.form.DATE_BEGIN.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.DATE_END.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.form.DATE_BEGIN.Text);
            DateTime date_to = DateTime.Parse(this.form.DATE_END.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.DATE_BEGIN.IsInputErrorOccured = true;
                this.form.DATE_END.IsInputErrorOccured = true;
                this.form.DATE_BEGIN.BackColor = Constans.ERROR_COLOR;
                this.form.DATE_END.BackColor = Constans.ERROR_COLOR;
                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                string[] errorMsg = { "対象期間From", "対象期間To" };
                msglogic.MessageBoxShow("E030", errorMsg);
                this.form.DATE_BEGIN.Focus();
                return true;
            }
            return false;
        }
        #endregion

        #region マスタ検索処理
        /// <summary>
        /// 業者検索処理
        /// </summary>
        /// <param name="cd">CD</param>
        internal M_GYOUSHA[] GetGyousha(string cd)
        {
            LogUtility.DebugMethodStart(cd);
            M_GYOUSHA dto = new M_GYOUSHA();
            dto.GYOUSHA_CD = cd;
            dto.JISHA_KBN = true;
            // 20151023 BUNN #12040 STR
            dto.SHOBUN_NIOROSHI_GYOUSHA_KBN = true;
            dto.GYOUSHAKBN_MANI = true;
            // 20151023 BUNN #12040 END
            dto.ISNOT_NEED_DELETE_FLG = true;
            IM_GYOUSHADao dao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            M_GYOUSHA[] results = dao.GetAllValidData(dto);
            LogUtility.DebugMethodEnd();
            return results;
        }

        /// <summary>
        /// 現場検索処理
        /// </summary>
        /// <param name="cd">CD</param>
        internal M_GENBA[] GetGenba(string gyoushaCd, string genbaCd, out bool catchErr)
        {
            catchErr = false;
            M_GENBA[] results = null;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd, genbaCd);
                M_GENBA dto = new M_GENBA();
                dto.GYOUSHA_CD = gyoushaCd;
                dto.GENBA_CD = genbaCd;
                dto.JISHA_KBN = true;
                // 20151023 BUNN #12040 STR
                dto.SHOBUN_NIOROSHI_GENBA_KBN = true;
                // 20151023 BUNN #12040 END
                dto.ISNOT_NEED_DELETE_FLG = true;
                IM_GENBADao dao = DaoInitUtility.GetComponent<IM_GENBADao>();
                results = dao.GetAllValidData(dto);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenba", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(results, catchErr);
            }
            return results;
        }

        /// <summary>
        /// 現場検索処理
        /// </summary>
        /// <param name="cd">CD</param>
        internal M_GENBA[] GetGenba1(string gyoushaCd, string genbaCd, out bool catchErr)
        {
            catchErr = false;
            M_GENBA[] results = null;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd, genbaCd);
                M_GENBA dto = new M_GENBA();
                dto.GYOUSHA_CD = gyoushaCd;
                dto.GENBA_CD = genbaCd;
                dto.JISHA_KBN = true;
                dto.SAISHUU_SHOBUNJOU_KBN = true;
                IM_GENBADao dao = DaoInitUtility.GetComponent<IM_GENBADao>();
                results = dao.GetAllValidData(dto);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenba", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(results, catchErr);
            }
            return results;
        }
        #endregion

        // 20141229 ブン 現場マスタのAddress1に詳細住所が入力されている可能性もあるの対応 start
        /// <summary>
        /// 住所設定
        /// </summary>
        /// <param name="address"></param>
        private string SetAddressString(string address)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(address))
            {
                if (this.tjHoukokuEntry != null && this.tjHoukokuEntry.ADDRESS_KBN == 3)
                {
                    result = Regex.Match(address, ".+?郡.+?町|.+?郡.+?村|.+?市|.+?区|.+?町|.+?村").Value;
                }
                else
                {
                    result = address;
                }
            }

            return result;
        }
        // 20141229 ブン 現場マスタのAddress1に詳細住所が入力されている可能性もあるの対応 end

    }
}
