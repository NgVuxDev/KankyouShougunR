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
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.IchiranCommon.Const;
using Shougun.Core.Message;
using Shougun.Core.PaperManifest.JissekiHokokuSisetsu.DAO;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using r_framework.Dao;
using Seasar.Quill.Attrs;
using CommonChouhyouPopup.App;
using System.Data.SqlTypes;
using Shougun.Core.Common.BusinessCommon.Utility;
using System.ComponentModel;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.PaperManifest.JissekiHokokuSisetsu
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
        private string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.JissekiHokokuSisetsu.Setting.ButtonSetting.xml";


        /// <summary> 親フォーム</summary>
        public BasePopForm parentbaseform { get; set; }

        public T_JISSEKI_HOUKOKU_ENTRY tjHoukokuEntry { get; set; }

        private List<T_JISSEKI_HOUKOKU_MANIFEST_DETAIL> tjHoukokuManiEntryList { get; set; }

        public List<T_JISSEKI_HOUKOKU_SHORI_DETAIL> tjHoukokuSyoriEntryList { get; set; }

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

        private SYORIDetailDAO SyoriDetailDao { get; set; }

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

            this.tjHoukokuSyoriEntryList = new List<T_JISSEKI_HOUKOKU_SHORI_DETAIL>();

            this.EntryDao = DaoInitUtility.GetComponent<EntryDAO>();
            this.GenbaDAO = DaoInitUtility.GetComponent<IM_GENBADao>();


            this.ManiDetailDao = DaoInitUtility.GetComponent<ManiDetailDAO>();

            this.SyoriDetailDao = DaoInitUtility.GetComponent<SYORIDetailDAO>();

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

            //処分実績ボタン(F3)イベント生成
            parentForm.bt_func3.Click += new EventHandler(this.form.bt_func3_Click);

            //運搬実績ボタン(F4)イベント生成
            parentForm.bt_func4.Click += new EventHandler(this.form.bt_func4_Click);

            //ＣＳＶ出力ボタン(F6)イベント生成
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
        internal void WindowInit()
        {
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
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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
                this.form.txtJIGYOUJOU_KBN.Text = "1";
                this.form.CHIIKI_CD.Text = string.Empty;
                this.form.CHIIKI_NAME.Text = string.Empty;
                this.form.txtHOUKOKU_SHOSHIKI.Text = "1";
                this.form.txtTOKUBETSU_KANRI_KBN.Text = "1";
                this.form.DATE_BEGIN.Value = (Convert.ToInt32(this.footer.sysDate.ToString("yyyy")) - 1).ToString() + "-04-01";
                this.form.DATE_END.Value = Convert.ToInt32(this.footer.sysDate.ToString("yyyy")) + "-03-31";
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
                    for (int i = 0; i < this.tjHoukokuSyoriEntryList.Count; i++)
                    {
                        T_JISSEKI_HOUKOKU_SHORI_DETAIL SyoriData = new T_JISSEKI_HOUKOKU_SHORI_DETAIL();
                        SyoriData = this.tjHoukokuSyoriEntryList[i];
                        this.SyoriDetailDao.Insert(SyoriData);
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
                this.tjHoukokuEntry.REPORT_ID = 2;

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

                this.tjHoukokuEntry.JIGYOUJOU_KBN = Convert.ToInt16(this.form.txtJIGYOUJOU_KBN.Text);

                this.tjHoukokuEntry.GYOUSHA_KBN = 0;

                this.tjHoukokuEntry.TEISHUTSU_CHIIKI_CD = this.form.CHIIKI_CD.Text;

                this.tjHoukokuEntry.TEISHUTSU_NAME = this.form.CHIIKI_NAME.Text;

                this.tjHoukokuEntry.HOUKOKU_SHOSHIKI = Convert.ToInt16(this.form.txtHOUKOKU_SHOSHIKI.Text);

                this.tjHoukokuEntry.TOKUBETSU_KANRI_KBN = Convert.ToInt16(this.form.txtTOKUBETSU_KANRI_KBN.Text);

                this.tjHoukokuEntry.DATE_BEGIN = Convert.ToDateTime(this.form.DATE_BEGIN.Value);

                this.tjHoukokuEntry.DATE_END = Convert.ToDateTime(this.form.DATE_END.Value);

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
            this.tjHoukokuSyoriEntryList = new List<T_JISSEKI_HOUKOKU_SHORI_DETAIL>();
            bool bunruiErrFlag = false;

            long DETAIL_SYSTEM_ID = 0;

            if (this.SearchResult.Rows.Count == 0)
            {
                DialogResult result = messageShowLogic.MessageBoxShow("C001");
                return -1;
            }

            List<T_JISSEKI_HOUKOKU_SHORI_DETAIL> dataList = new List<T_JISSEKI_HOUKOKU_SHORI_DETAIL>();
            List<DTOClass> dtoList = new List<DTOClass>();
            List<string> haikiSyuruiList = new List<string>();
            List<string> haikiSyuruiNameList = new List<string>();
            if (this.SearchResult != null && this.SearchResult.Rows.Count > 0)
            {
                foreach (DataRow dt in this.SearchResult.Rows)
                {
                    if (string.IsNullOrEmpty(dt["SHORI_SHISETSU_NAME"].ToString())
                        || string.IsNullOrEmpty(dt["SHORI_SHISETSU_CD"].ToString())
                        || string.IsNullOrEmpty(dt["HAIKI_SHURUI_CD"].ToString())
                        || string.IsNullOrEmpty(dt["HST_JOU_CHIIKI_CD"].ToString())
                        || string.IsNullOrEmpty(dt["HST_KEN_KBN"].ToString())
                        || string.IsNullOrEmpty(dt["HST_JOU_TODOUFUKEN_CD"].ToString())
                        || (("1".Equals(dt["NEXT_KBN"].ToString()) && (string.IsNullOrEmpty(dt["SBN_AFTER_HAIKI_NAME"].ToString())
                                                       || string.IsNullOrEmpty(dt["SHOBUN_HOUHOU_CD"].ToString()))))
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
                foreach (DataRow dt in this.SearchResult.Rows)
                {
                    if (string.IsNullOrEmpty(dt["HOUKOKUSHO_BUNRUI_CD"].ToString()) && !dt["NEXT_KBN"].Equals(0))
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

                    DTOClass dto = new DTOClass();

                    if (!string.IsNullOrEmpty(dt["SYSTEM_ID"].ToString()))
                    {
                        dto.SYSTEM_ID = Convert.ToInt64(dt["SYSTEM_ID"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt["SEQ"].ToString()))
                    {
                        dto.SEQ = Convert.ToInt32(dt["SEQ"].ToString());
                    }
                    dto.MANIFEST_ID = dt["MANIFEST_ID"].ToString();
                    dto.KANRI_ID = dt["KANRI_ID"].ToString();
                    if (!string.IsNullOrEmpty(dt["DEN_SEQ"].ToString()))
                    {
                        dto.DEN_SEQ = Convert.ToInt32(dt["DEN_SEQ"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt["HAIKI_KBN_CD"].ToString()))
                    {
                        dto.HAIKI_KBN_CD = Convert.ToInt16(dt["HAIKI_KBN_CD"].ToString());
                    }
                    dto.SHORI_SHISETSU_NAME = dt["SHORI_SHISETSU_NAME"].ToString();
                    if (!string.IsNullOrEmpty(dt["NEXT_KBN"].ToString()))
                    {
                        dto.NEXT_KBN = Convert.ToInt16(dt["NEXT_KBN"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dt["NEXT_SYSTEM_ID"].ToString()))
                    {
                        dto.NEXT_SYSTEM_ID = Convert.ToInt64(dt["NEXT_SYSTEM_ID"].ToString());
                    }

                    dto.HOUKOKUSHO_BUNRUI_CD = dt["HOUKOKUSHO_BUNRUI_CD"].ToString();
                    dto.SBN_AFTER_HAIKI_NAME = dt["SBN_AFTER_HAIKI_NAME"].ToString();
                    dto.SHORI_SHISETSU_CD = dt["SHORI_SHISETSU_CD"].ToString();
                    dto.HAIKI_SHURUI_CD = dt["HAIKI_SHURUI_CD"].ToString();
                    dto.HAIKI_SHURUI_NAME = dt["HAIKI_SHURUI_NAME"].ToString();

                    if (dto.NEXT_KBN.Value == 1)
                    {
                        int count = 1;
                        string nextSystemID = dt["NEXT_SYSTEM_ID"].ToString();
                        if (!string.IsNullOrEmpty(nextSystemID))
                        {
                            //count = this.EntryDao.GetDetailCount(nextSystemID);
                            count = Convert.ToInt32(dt["DETAIL_COUNT"]);
                        }

                        if (!string.IsNullOrEmpty(dt["KANSAN_SUU"].ToString()) && count != 0)
                        {
                            dto.KANSAN_SUU = Convert.ToDecimal(dt["KANSAN_SUU"].ToString()) / count;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(dt["KANSAN_SUU"].ToString()))
                        {
                            dto.KANSAN_SUU = Convert.ToDecimal(dt["KANSAN_SUU"].ToString());
                        }
                    }

                    dto.UNIT_NAME = dt["UNIT_NAME"].ToString();

                    if (dto.NEXT_KBN.Value == 1)
                    {
                        dto.SHOBUN_HOUHOU_CD = dt["SHOBUN_HOUHOU_CD"].ToString();
                        dto.SHOBUN_HOUHOU_NAME = dt["SHOBUN_HOUHOU_NAME"].ToString();
                        if (!string.IsNullOrEmpty(dt["GENNYOU_SUU"].ToString()))
                        {
                            dto.GENNYOU_SUU = Convert.ToDecimal(dt["GENNYOU_SUU"].ToString());
                        }
                    }
                    dto.HST_JOU_CHIIKI_CD = dt["HST_JOU_CHIIKI_CD"].ToString();

                    if (!string.IsNullOrEmpty(dt["HST_KEN_KBN"].ToString()))
                    {
                        dto.HST_KEN_KBN = Convert.ToInt16(dt["HST_KEN_KBN"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dt["HST_JOU_TODOUFUKEN_CD"].ToString()))
                    {
                        dto.HST_JOU_TODOUFUKEN_CD = Convert.ToInt16(dt["HST_JOU_TODOUFUKEN_CD"].ToString());
                    }

                    bool new_haiki = true;
                    for (int i = 0; i < haikiSyuruiList.Count; i++)
                    {
                        string hiki = haikiSyuruiList[i];
                        if (dto.HAIKI_SHURUI_CD.Equals(hiki))
                        {
                            new_haiki = false;
                        }
                    }

                    if (new_haiki)
                    {
                        haikiSyuruiList.Add(dto.HAIKI_SHURUI_CD);
                        haikiSyuruiNameList.Add(dto.HAIKI_SHURUI_NAME);
                    }
                    dtoList.Add(dto);
                }
            }

            foreach (DTOClass data in dtoList)
            {
                T_JISSEKI_HOUKOKU_MANIFEST_DETAIL tjHoukokuManiEntry = new T_JISSEKI_HOUKOKU_MANIFEST_DETAIL();
                T_JISSEKI_HOUKOKU_SHORI_DETAIL tjHoukokuSyoriEntry = new T_JISSEKI_HOUKOKU_SHORI_DETAIL();

                bool new_row = true;
                long row_detail_id = 0;
                int index = 0;
                int page_no = 0;

                for (int j = 0; j < haikiSyuruiList.Count; j++)
                {
                    if (data.HAIKI_SHURUI_CD.Equals(haikiSyuruiList[j]))
                    {
                        index = j % 4 + 1;
                        page_no = j / 4 + 1;
                    }
                }
                #region tjHoukokuSyoriEntryList
                for (int i = 0; i < this.tjHoukokuSyoriEntryList.Count; i++)
                {
                    T_JISSEKI_HOUKOKU_SHORI_DETAIL syoriData = this.tjHoukokuSyoriEntryList[i];
                    if (syoriData.HST_JOU_CHIIKI_CD.Equals(data.HST_JOU_CHIIKI_CD)
                            && syoriData.SHORI_SHISETSU_NAME.Equals(data.SHORI_SHISETSU_NAME)
                            && syoriData.SHORI_SHISETSU_CD.Equals(data.SHORI_SHISETSU_CD)
                            && syoriData.SBN_AFTER_HAIKI_NAME == data.SBN_AFTER_HAIKI_NAME
                            && syoriData.SHOBUN_HOUHOU_CD == data.SHOBUN_HOUHOU_CD)
                    {
                        this.tjHoukokuSyoriEntryList[i].HST_RYOU += data.GENNYOU_SUU;
                        this.tjHoukokuSyoriEntryList[i].SBN_RYOU += data.GENNYOU_SUU;

                        switch (index)
                        {
                            case 1:
                                if (syoriData.HAIKI_SHURUI_CD1 == data.HAIKI_SHURUI_CD)
                                {
                                    if (!string.IsNullOrEmpty(data.HAIKI_SHURUI_CD))
                                    {
                                        if (!data.KANSAN_SUU.IsNull)
                                        {
                                            if (this.tjHoukokuSyoriEntryList[i].SBN_RYOU1.IsNull)
                                            {
                                                this.tjHoukokuSyoriEntryList[i].SBN_RYOU1 = 0;
                                            }

                                            this.tjHoukokuSyoriEntryList[i].SBN_RYOU1 = this.tjHoukokuSyoriEntryList[i].SBN_RYOU1.Value + data.KANSAN_SUU.Value;
                                        }
                                    }

                                    new_row = false;
                                    row_detail_id = syoriData.DETAIL_SYSTEM_ID.Value;
                                }
                                break;
                            case 2:
                                if (syoriData.HAIKI_SHURUI_CD2 == data.HAIKI_SHURUI_CD)
                                {
                                    if (!string.IsNullOrEmpty(data.HAIKI_SHURUI_CD))
                                    {
                                        if (!data.KANSAN_SUU.IsNull)
                                        {
                                            if (this.tjHoukokuSyoriEntryList[i].SBN_RYOU2.IsNull)
                                            {
                                                this.tjHoukokuSyoriEntryList[i].SBN_RYOU2 = 0;
                                            }

                                            this.tjHoukokuSyoriEntryList[i].SBN_RYOU2 = this.tjHoukokuSyoriEntryList[i].SBN_RYOU2.Value + data.KANSAN_SUU.Value;
                                        }
                                    }

                                    new_row = false;
                                    row_detail_id = syoriData.DETAIL_SYSTEM_ID.Value;
                                }
                                break;
                            case 3:
                                if (syoriData.HAIKI_SHURUI_CD3 == data.HAIKI_SHURUI_CD)
                                {
                                    if (!string.IsNullOrEmpty(data.HAIKI_SHURUI_CD))
                                    {
                                        if (!data.KANSAN_SUU.IsNull)
                                        {
                                            if (this.tjHoukokuSyoriEntryList[i].SBN_RYOU3.IsNull)
                                            {
                                                this.tjHoukokuSyoriEntryList[i].SBN_RYOU3 = 0;
                                            }
                                            this.tjHoukokuSyoriEntryList[i].SBN_RYOU3 = this.tjHoukokuSyoriEntryList[i].SBN_RYOU3.Value + data.KANSAN_SUU.Value;
                                        }
                                    }

                                    new_row = false;
                                    row_detail_id = syoriData.DETAIL_SYSTEM_ID.Value;
                                }
                                break;
                            case 4:
                                if (syoriData.HAIKI_SHURUI_CD4 == data.HAIKI_SHURUI_CD)
                                {
                                    if (!string.IsNullOrEmpty(data.HAIKI_SHURUI_CD))
                                    {
                                        if (!data.KANSAN_SUU.IsNull)
                                        {
                                            if (this.tjHoukokuSyoriEntryList[i].SBN_RYOU4.IsNull)
                                            {
                                                this.tjHoukokuSyoriEntryList[i].SBN_RYOU4 = 0;
                                            }
                                            this.tjHoukokuSyoriEntryList[i].SBN_RYOU4 = this.tjHoukokuSyoriEntryList[i].SBN_RYOU4.Value + data.KANSAN_SUU.Value;
                                        }
                                    }

                                    new_row = false;
                                    row_detail_id = syoriData.DETAIL_SYSTEM_ID.Value;
                                }
                                break;
                        }
                    }
                }
                #endregion
                if (new_row)
                {
                    SqlInt64 ID = this.createSystemIdForJissekiHokoku();
                    DETAIL_SYSTEM_ID = ID.Value;
                    tjHoukokuSyoriEntry.SYSTEM_ID = this.tjHoukokuEntry.SYSTEM_ID;

                    // 枝番
                    tjHoukokuSyoriEntry.SEQ = this.tjHoukokuEntry.SEQ;

                    // 明細システムID
                    tjHoukokuSyoriEntry.DETAIL_SYSTEM_ID = DETAIL_SYSTEM_ID;

                    // 帳票ID
                    tjHoukokuSyoriEntry.REPORT_ID = 2;

                    // 報告書式
                    tjHoukokuSyoriEntry.HOUKOKU_SHOSHIKI_KBN = this.tjHoukokuEntry.HOUKOKU_SHOSHIKI;

                    // 保存名
                    tjHoukokuSyoriEntry.HOZON_NAME = this.tjHoukokuEntry.HOZON_NAME;

                    // 報告年度
                    //tjHoukokuSbnEntry.HOUKOKU_YEAR = this.tjHoukokuEntry.HOUKOKU_YEAR;
                    // 和暦でDataTimeを文字列に変換する
                    System.Globalization.CultureInfo ci =
                    new System.Globalization.CultureInfo("ja-JP", false);
                    ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();

                    tjHoukokuSyoriEntry.HOUKOKU_YEAR = Convert.ToDateTime(this.form.DATE_BEGIN.Value).ToString("gy年", ci);

                    // 提出先地域CD
                    tjHoukokuSyoriEntry.TEISHUTSUSAKI_CHIIKI_CD = this.tjHoukokuEntry.TEISHUTSU_CHIIKI_CD;

                    // 処理施設CD
                    tjHoukokuSyoriEntry.SHORI_SHISETSU_CD = data.SHORI_SHISETSU_CD;

                    // 処理施設名
                    tjHoukokuSyoriEntry.SHORI_SHISETSU_NAME = data.SHORI_SHISETSU_NAME;

                    // 事業場区分
                    tjHoukokuSyoriEntry.JIGYOUJOU_KBN = 1;

                    // 県区分
                    tjHoukokuSyoriEntry.KEN_KBN = 0;

                    tjHoukokuSyoriEntry.SBN_AFTER_HAIKI_NAME = data.SBN_AFTER_HAIKI_NAME;

                    tjHoukokuSyoriEntry.PAGE_NO = page_no;

                    int haiki_no = 1;
                    int end_no = 0;
                    if (haikiSyuruiList.Count >= 4 * page_no)
                    {
                        end_no = 4 * page_no;
                    }
                    else
                    {
                        end_no = haikiSyuruiList.Count;
                    }

                    for (int k = (page_no - 1) * 4; k < end_no; k++)
                    {
                        switch (haiki_no)
                        {
                            case 1:
                                tjHoukokuSyoriEntry.HAIKI_SHURUI_CD1 = haikiSyuruiList[k];
                                tjHoukokuSyoriEntry.HAIKI_SHURUI_NAME1 = haikiSyuruiNameList[k];
                                //tjHoukokuSyoriEntry.SBN_RYOU1 = 0;
                                break;
                            case 2:
                                tjHoukokuSyoriEntry.HAIKI_SHURUI_CD2 = haikiSyuruiList[k];
                                tjHoukokuSyoriEntry.HAIKI_SHURUI_NAME2 = haikiSyuruiNameList[k];
                                //tjHoukokuSyoriEntry.SBN_RYOU2 = 0;
                                break;
                            case 3:
                                tjHoukokuSyoriEntry.HAIKI_SHURUI_CD3 = haikiSyuruiList[k];
                                tjHoukokuSyoriEntry.HAIKI_SHURUI_NAME3 = haikiSyuruiNameList[k];
                                //tjHoukokuSyoriEntry.SBN_RYOU3 = 0;
                                break;
                            case 4:
                                tjHoukokuSyoriEntry.HAIKI_SHURUI_CD4 = haikiSyuruiList[k];
                                tjHoukokuSyoriEntry.HAIKI_SHURUI_NAME4 = haikiSyuruiNameList[k];
                                //tjHoukokuSyoriEntry.SBN_RYOU4 = 0;
                                break;
                        }
                        haiki_no++;
                    }

                    //if (data.KANSAN_SUU.IsNull)
                   // {
                    //    data.KANSAN_SUU = 0;
                   // }

                    switch (index)
                    {
                        case 1:
                            if (!data.KANSAN_SUU.IsNull)
                            {
                                tjHoukokuSyoriEntry.SBN_RYOU1 = data.KANSAN_SUU.Value;
                            }
                            else
                            {
                                tjHoukokuSyoriEntry.SBN_RYOU1 = 0;
                            }
                            break;
                        case 2:
                            if (!data.KANSAN_SUU.IsNull)
                            {
                                tjHoukokuSyoriEntry.SBN_RYOU2 = data.KANSAN_SUU.Value;
                            }
                            else
                            {
                                tjHoukokuSyoriEntry.SBN_RYOU2 = 0;
                            }
                            break;
                        case 3:
                            if (!data.KANSAN_SUU.IsNull)
                            {
                                tjHoukokuSyoriEntry.SBN_RYOU3 = data.KANSAN_SUU.Value;
                            }
                            else
                            {
                                tjHoukokuSyoriEntry.SBN_RYOU3 = 0;
                            }
                            break;
                        case 4:
                            if (!data.KANSAN_SUU.IsNull)
                            {
                                tjHoukokuSyoriEntry.SBN_RYOU4 = data.KANSAN_SUU.Value;
                            }
                            else
                            {
                                tjHoukokuSyoriEntry.SBN_RYOU4 = 0;
                            }
                            break;
                    }

                    tjHoukokuSyoriEntry.UNIT_NAME = data.UNIT_NAME;
                    if (data.NEXT_KBN.Value == 1)
                    {
                        //if (data.GENNYOU_SUU.IsNull)
                        //{
                            //data.GENNYOU_SUU = 0;
                        //}
                        if (!data.GENNYOU_SUU.IsNull)
                        {
                            tjHoukokuSyoriEntry.HST_RYOU = data.GENNYOU_SUU;
                        }
                        tjHoukokuSyoriEntry.SHOBUN_HOUHOU_CD = data.SHOBUN_HOUHOU_CD;
                        tjHoukokuSyoriEntry.SHOBUN_HOUHOU_NAME = data.SHOBUN_HOUHOU_NAME;
                        tjHoukokuSyoriEntry.SBN_RYOU = data.GENNYOU_SUU;

                    }
                    tjHoukokuSyoriEntry.HST_KEN_KBN = data.HST_KEN_KBN;

                    tjHoukokuSyoriEntry.HST_JOU_TODOUFUKEN_CD = data.HST_JOU_TODOUFUKEN_CD;

                    tjHoukokuSyoriEntry.HST_JOU_CHIIKI_CD = data.HST_JOU_CHIIKI_CD;

                    tjHoukokuSyoriEntry.SYUKEI_KBN = 0;

                    this.tjHoukokuSyoriEntryList.Add(tjHoukokuSyoriEntry);
                    row_detail_id = DETAIL_SYSTEM_ID;
                }

                this.tjHoukokuSyoriEntryList = (from temp in this.tjHoukokuSyoriEntryList
                                                orderby temp.PAGE_NO ascending,
                                                        temp.HST_KEN_KBN ascending,
                                                        temp.SHORI_SHISETSU_CD ascending,
                                                        temp.HST_JOU_CHIIKI_CD ascending,
                                                        temp.SHOBUN_HOUHOU_CD ascending,
                                                        temp.SBN_AFTER_HAIKI_NAME ascending,
                                                        temp.UNIT_NAME ascending
                                                select temp).ToList();
                bool mani_new_row = true;
                int detail_row_no = 0;
                for (int i = 0; i < this.tjHoukokuManiEntryList.Count; i++)
                {
                    T_JISSEKI_HOUKOKU_MANIFEST_DETAIL maniData = this.tjHoukokuManiEntryList[i];
                    if (maniData.DETAIL_SYSTEM_ID.Value.Equals(row_detail_id))
                    {
                        if (!data.SYSTEM_ID.IsNull)
                        {
                            long system_id = data.SYSTEM_ID.Value;
                            int seq = data.SEQ.Value;
                            if (maniData.MANI_SYSTEM_ID == system_id && maniData.MANI_SEQ == seq)
                            {
                                mani_new_row = false;
                                break;
                            }
                        }
                        else if (!string.IsNullOrEmpty(data.KANRI_ID))
                        {
                            string kanri_id = data.KANRI_ID;
                            int den_seq = data.DEN_SEQ.Value;
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
                    tjHoukokuManiEntry.HAIKI_KBN_CD = data.HAIKI_KBN_CD.Value;
                    if (!data.SYSTEM_ID.IsNull)
                    {
                        tjHoukokuManiEntry.MANI_SYSTEM_ID = data.SYSTEM_ID.Value;
                        tjHoukokuManiEntry.MANI_SEQ = data.SEQ.Value;
                    }
                    else if (!string.IsNullOrEmpty(data.KANRI_ID))
                    {
                        tjHoukokuManiEntry.DEN_MANI_KANRI_ID = data.KANRI_ID;
                        tjHoukokuManiEntry.DEN_MANI_SEQ = data.DEN_SEQ.Value;
                    }
                    tjHoukokuManiEntry.MANIFEST_ID = data.MANIFEST_ID;
                    this.tjHoukokuManiEntryList.Add(tjHoukokuManiEntry);
                }
            }

            LogUtility.DebugMethodEnd();
            return 0;
        }

        public virtual void PrintView()
        {
            if (this.tjHoukokuSyoriEntryList.Count == 0)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("I008", "データ");
                return;
            }
            FormReportPrintPopup formReport;

            DataTable headerData = new DataTable();
            headerData.Columns.Add("HOUKOKU_YEAR");                        // 報告年
            headerData.Columns.Add("HAIKI_SHURUI_CD1");                    // 種類CD1
            headerData.Columns.Add("HAIKI_SHURUI_NAME1");                  // 種類名1
            headerData.Columns.Add("HAIKI_SHURUI_CD2");                    // 種類CD2
            headerData.Columns.Add("HAIKI_SHURUI_NAME2");                  // 種類名2
            headerData.Columns.Add("HAIKI_SHURUI_CD3");                    // 種類CD3
            headerData.Columns.Add("HAIKI_SHURUI_NAME3");                  // 種類名3
            headerData.Columns.Add("HAIKI_SHURUI_CD4");                    // 種類CD4
            headerData.Columns.Add("HAIKI_SHURUI_NAME4");                  // 種類名4

            DataTable detailData = new DataTable();
            detailData.Columns.Add("SYORI_SHISETSU_NAME");                 // (特別管理)産業廃棄物の種類
            detailData.Columns.Add("SYORI_SHISETSU_CD");                   // 施設コード
            detailData.Columns.Add("SBN_RYOU1");                           // 処分量1
            detailData.Columns.Add("SBN_RYOU2");                           // 処分量2
            detailData.Columns.Add("SBN_RYOU3");                           // 処分量3
            detailData.Columns.Add("SBN_RYOU4");                           // 処分量4
            detailData.Columns.Add("SBN_GO_HAIKI_NAME");                   // 種類
            detailData.Columns.Add("HST_RYOU");                            // 排出量
            detailData.Columns.Add("SHOBUN_HOUHOU_NAME");                  // 処理方法
            detailData.Columns.Add("SBN_RYOU");                            // 処分量

            detailData.Columns.Add("GROUP_KEY");                           // GROUP_KEY
            detailData.Columns.Add("HST_GENBA_CHIIKI_NAME");               // 排出事業場地域名称

            IM_CHIIKIDao mChiikiDao = DaoInitUtility.GetComponent<IM_CHIIKIDao>();
            M_CHIIKI mChiikiData = new M_CHIIKI();

            List<T_JISSEKI_HOUKOKU_SHORI_DETAIL> detailList = new List<T_JISSEKI_HOUKOKU_SHORI_DETAIL>();

            T_JISSEKI_HOUKOKU_ENTRY entry = new T_JISSEKI_HOUKOKU_ENTRY();

            detailList = this.tjHoukokuSyoriEntryList;
            //IEnumerable<T_JISSEKI_HOUKOKU_SHORI_DETAIL> query = null;
            //query = from items in detailList orderby items.PAGE_NO, items.HST_JOU_CHIIKI_CD select items;
            entry = this.tjHoukokuEntry;

            // 明細-明細情報
            int Count = 0;
            string BeforePagNo = string.Empty;
            foreach (T_JISSEKI_HOUKOKU_SHORI_DETAIL dt in detailList)
            {
                if (BeforePagNo == dt.PAGE_NO.Value.ToString())
                {
                    DataRow headerRow = headerData.NewRow();
                    headerRow["HOUKOKU_YEAR"] = dt.HOUKOKU_YEAR;
                    headerRow["HAIKI_SHURUI_CD1"] = dt.HAIKI_SHURUI_CD1;
                    headerRow["HAIKI_SHURUI_NAME1"] = dt.HAIKI_SHURUI_NAME1;
                    headerRow["HAIKI_SHURUI_CD2"] = dt.HAIKI_SHURUI_CD2;
                    headerRow["HAIKI_SHURUI_NAME2"] = dt.HAIKI_SHURUI_NAME2;
                    headerRow["HAIKI_SHURUI_CD3"] = dt.HAIKI_SHURUI_CD3;
                    headerRow["HAIKI_SHURUI_NAME3"] = dt.HAIKI_SHURUI_NAME3;
                    headerRow["HAIKI_SHURUI_CD4"] = dt.HAIKI_SHURUI_CD4;
                    headerRow["HAIKI_SHURUI_NAME4"] = dt.HAIKI_SHURUI_NAME4;

                    headerData.Rows.Add(headerRow);

                    DataRow row = detailData.NewRow();
                    row["SYORI_SHISETSU_NAME"] = dt.SHORI_SHISETSU_NAME;
                    row["SYORI_SHISETSU_CD"] = dt.SHORI_SHISETSU_CD;
                    if (dt.SYUKEI_KBN == -1)
                    {
                        row["SBN_RYOU1_SUM"] = ConvertSuuryo(dt.SBN_RYOU1);
                        row["SBN_RYOU2_SUM"] = ConvertSuuryo(dt.SBN_RYOU2);
                        row["SBN_RYOU3_SUM"] = ConvertSuuryo(dt.SBN_RYOU3);
                        row["SBN_RYOU4_SUM"] = ConvertSuuryo(dt.SBN_RYOU4);
                    }
                    else
                    {
                        row["SBN_RYOU1"] = ConvertSuuryo(dt.SBN_RYOU1);
                        row["SBN_RYOU2"] = ConvertSuuryo(dt.SBN_RYOU2);
                        row["SBN_RYOU3"] = ConvertSuuryo(dt.SBN_RYOU3);
                        row["SBN_RYOU4"] = ConvertSuuryo(dt.SBN_RYOU4);
                    }
                    row["SBN_GO_HAIKI_NAME"] = dt.SBN_AFTER_HAIKI_NAME;
                    if (!dt.HST_RYOU.IsNull)
                    {
                        if (dt.SYUKEI_KBN == -1)
                        {
                            row["HST_RYOU_SUM"] = ConvertSuuryo(dt.HST_RYOU);
                        }
                        else
                        {
                            row["HST_RYOU"] = ConvertSuuryo(dt.HST_RYOU);
                        }
                    }
                    row["SHOBUN_HOUHOU_NAME"] = dt.SHOBUN_HOUHOU_NAME;
                    if (!dt.SBN_RYOU.IsNull)
                    {
                        if (dt.SYUKEI_KBN == -1)
                        {
                            row["SBN_RYOU_SUM"] = ConvertSuuryo(dt.SBN_RYOU.Value);
                        }
                        else
                        {
                            row["SBN_RYOU"] = ConvertSuuryo(dt.SBN_RYOU);
                        }
                    }
                    // GROUPKEY
                    row["GROUP_KEY"] = dt.HST_JOU_CHIIKI_CD + dt.SBN_AFTER_HAIKI_NAME + dt.SHOBUN_HOUHOU_NAME + dt.SHORI_SHISETSU_CD;
                    mChiikiData = mChiikiDao.GetDataByCd(dt.HST_JOU_CHIIKI_CD);
                    if (mChiikiData != null)
                    {
                        row["HST_GENBA_CHIIKI_NAME"] = mChiikiData.CHIIKI_NAME + "からの処分量合計";
                    }

                    detailData.Rows.Add(row);
                }
                else
                {
                    if (!string.IsNullOrEmpty(BeforePagNo) && BeforePagNo != dt.PAGE_NO.Value.ToString())
                    {
                        ReportInfoR605 reportInfo = new ReportInfoR605();
                        reportInfo.Title = "実績報告書（処理施設）";
                        reportInfo.R605_Reprt(headerData, detailData);
                        formReport = new FormReportPrintPopup(reportInfo, "R605");
                        formReport.Caption = "業務報告書";

                        // 印刷アプリ初期動作(プレビュー)
                        formReport.PrintInitAction = 2;
                        formReport.PrintXPS();

                        formReport.Dispose();
                        headerData.Clear();
                        detailData.Clear();
                    }
                    DataRow headerRow = headerData.NewRow();
                    headerRow["HOUKOKU_YEAR"] = dt.HOUKOKU_YEAR;
                    headerRow["HAIKI_SHURUI_CD1"] = dt.HAIKI_SHURUI_CD1;
                    headerRow["HAIKI_SHURUI_NAME1"] = dt.HAIKI_SHURUI_NAME1;
                    headerRow["HAIKI_SHURUI_CD2"] = dt.HAIKI_SHURUI_CD2;
                    headerRow["HAIKI_SHURUI_NAME2"] = dt.HAIKI_SHURUI_NAME2;
                    headerRow["HAIKI_SHURUI_CD3"] = dt.HAIKI_SHURUI_CD3;
                    headerRow["HAIKI_SHURUI_NAME3"] = dt.HAIKI_SHURUI_NAME3;
                    headerRow["HAIKI_SHURUI_CD4"] = dt.HAIKI_SHURUI_CD4;
                    headerRow["HAIKI_SHURUI_NAME4"] = dt.HAIKI_SHURUI_NAME4;

                    headerData.Rows.Add(headerRow);

                    DataRow row = detailData.NewRow();
                    row["SYORI_SHISETSU_NAME"] = dt.SHORI_SHISETSU_NAME;
                    row["SYORI_SHISETSU_CD"] = dt.SHORI_SHISETSU_CD;
                    if (dt.SYUKEI_KBN == -1)
                    {
                        row["SBN_RYOU1_SUM"] = ConvertSuuryo(dt.SBN_RYOU1);
                        row["SBN_RYOU2_SUM"] = ConvertSuuryo(dt.SBN_RYOU2);
                        row["SBN_RYOU3_SUM"] = ConvertSuuryo(dt.SBN_RYOU3);
                        row["SBN_RYOU4_SUM"] = ConvertSuuryo(dt.SBN_RYOU4);
                    }
                    else
                    {
                        row["SBN_RYOU1"] = ConvertSuuryo(dt.SBN_RYOU1);
                        row["SBN_RYOU2"] = ConvertSuuryo(dt.SBN_RYOU2);
                        row["SBN_RYOU3"] = ConvertSuuryo(dt.SBN_RYOU3);
                        row["SBN_RYOU4"] = ConvertSuuryo(dt.SBN_RYOU4);
                    }
                    row["SBN_GO_HAIKI_NAME"] = dt.SBN_AFTER_HAIKI_NAME;
                    if (!dt.HST_RYOU.IsNull)
                    {
                        if (dt.SYUKEI_KBN == -1)
                        {
                            row["HST_RYOU_SUM"] = ConvertSuuryo(dt.HST_RYOU);
                        }
                        else
                        {
                            row["HST_RYOU"] = ConvertSuuryo(dt.HST_RYOU);
                        }
                    }
                    row["SHOBUN_HOUHOU_NAME"] = dt.SHOBUN_HOUHOU_NAME;
                    if (!dt.SBN_RYOU.IsNull)
                    {
                        if (dt.SYUKEI_KBN == -1)
                        {
                            row["SBN_RYOU_SUM"] = ConvertSuuryo(dt.SBN_RYOU);
                        }
                        else
                        {
                            row["SBN_RYOU"] = ConvertSuuryo(dt.SBN_RYOU);
                        }
                    }

                    // GROUPKEY
                    row["GROUP_KEY"] = dt.HST_JOU_CHIIKI_CD + dt.SBN_AFTER_HAIKI_NAME + dt.SHOBUN_HOUHOU_NAME + dt.SHORI_SHISETSU_CD;
                    mChiikiData = mChiikiDao.GetDataByCd(dt.HST_JOU_CHIIKI_CD);
                    if (mChiikiData != null)
                    {
                        row["HST_GENBA_CHIIKI_NAME"] = mChiikiData.CHIIKI_NAME + "からの処分量合計";
                    }

                    detailData.Rows.Add(row);
                }
                Count++;
                BeforePagNo = dt.PAGE_NO.ToString();
            }
            if (detailList.Count == Count)
            {
                ReportInfoR605 reportInfo = new ReportInfoR605();
                reportInfo.Title = "実績報告書（処理施設）";
                reportInfo.R605_Reprt(headerData, detailData);
                formReport = new FormReportPrintPopup(reportInfo, "R605");
                formReport.Caption = "業務報告書";

                // 印刷アプリ初期動作(プレビュー)
                formReport.PrintInitAction = 2;
                formReport.PrintXPS();

                formReport.Dispose();
            }
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

        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <returns></returns>
        internal Boolean InputCheck()
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

            DateTime dtpFrom = DateTime.Parse(this.form.DATE_BEGIN.GetResultText());
            DateTime dtpTo = DateTime.Parse(this.form.DATE_END.GetResultText());
            DateTime dtpFromWithoutTime = DateTime.Parse(dtpFrom.ToShortDateString());
            DateTime dtpToWithoutTime = DateTime.Parse(dtpTo.ToShortDateString());

            int diff = dtpFromWithoutTime.CompareTo(dtpToWithoutTime);

            if (0 < diff)
            {
                //対象期間内でないならエラーメッセージ表示
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                this.form.DATE_BEGIN.IsInputErrorOccured = true;
                this.form.DATE_END.IsInputErrorOccured = true;
                msgLogic.MessageBoxShow("E030", this.form.DATE_BEGIN.DisplayItemName, this.form.DATE_END.DisplayItemName);
                this.form.DATE_BEGIN.Select();
                this.form.DATE_BEGIN.Focus();

                LogUtility.DebugMethodEnd(true);
                return true;
            }
            LogUtility.DebugMethodEnd(false);
            return false;
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        internal void CSVOutput()
        {
            try
            {
                LogUtility.DebugMethodStart();
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
                DataTable dt = new DataTable();
                DataColumn dc1 = new DataColumn("処理施設名");
                dc1.DataType = typeof(string);
                DataColumn dc2 = new DataColumn("種類名1");
                dc2.DataType = typeof(string);
                DataColumn dc3 = new DataColumn("処分量1");
                dc3.DataType = typeof(string);
                DataColumn dc4 = new DataColumn("単位1");
                dc4.DataType = typeof(string);
                DataColumn dc5 = new DataColumn("種類名2");
                dc5.DataType = typeof(string);
                DataColumn dc6 = new DataColumn("処分量2");
                dc6.DataType = typeof(string);
                DataColumn dc7 = new DataColumn("単位2");
                dc7.DataType = typeof(string);
                DataColumn dc8 = new DataColumn("種類名3");
                dc8.DataType = typeof(string);
                DataColumn dc9 = new DataColumn("処分量3");
                dc9.DataType = typeof(string);
                DataColumn dc10 = new DataColumn("単位3");
                dc10.DataType = typeof(string);
                DataColumn dc11 = new DataColumn("種類名4");
                dc11.DataType = typeof(string);
                DataColumn dc12 = new DataColumn("処分量4");
                dc12.DataType = typeof(string);
                DataColumn dc13 = new DataColumn("単位4");
                dc13.DataType = typeof(string);
                DataColumn dc14 = new DataColumn("処分後廃棄物名");
                dc14.DataType = typeof(string);
                DataColumn dc15 = new DataColumn("排出量");
                dc15.DataType = typeof(string);
                DataColumn dc16 = new DataColumn("排出単位");
                dc16.DataType = typeof(string);
                DataColumn dc17 = new DataColumn("処分方法名");
                dc17.DataType = typeof(string);
                DataColumn dc18 = new DataColumn("処分量");
                dc18.DataType = typeof(string);
                DataColumn dc19 = new DataColumn("処分単位");
                dc19.DataType = typeof(string);
                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);
                dt.Columns.Add(dc3);
                dt.Columns.Add(dc4);
                dt.Columns.Add(dc5);
                dt.Columns.Add(dc6);
                dt.Columns.Add(dc7);
                dt.Columns.Add(dc8);
                dt.Columns.Add(dc9);
                dt.Columns.Add(dc10);
                dt.Columns.Add(dc11);
                dt.Columns.Add(dc12);
                dt.Columns.Add(dc13);
                dt.Columns.Add(dc14);
                dt.Columns.Add(dc15);
                dt.Columns.Add(dc16);
                dt.Columns.Add(dc17);
                dt.Columns.Add(dc18);
                dt.Columns.Add(dc19);
                if (this.header.txtNum_Syukeiari.Text == "1")
                {
                    var csv = from temp in this.tjHoukokuSyoriEntryList
                              group temp by
                              new
                              {
                                  temp.SHORI_SHISETSU_NAME,
                                  temp.HAIKI_SHURUI_NAME1,
                                  temp.HAIKI_SHURUI_NAME2,
                                  temp.HAIKI_SHURUI_NAME3,
                                  temp.HAIKI_SHURUI_NAME4,
                                  temp.SBN_AFTER_HAIKI_NAME,
                                  temp.SHOBUN_HOUHOU_NAME,
                                  temp.UNIT_NAME
                              } into rst
                              select
                              new
                              {
                                  SHORI_SHISETSU_NAME = rst.Key.SHORI_SHISETSU_NAME,
                                  HAIKI_SHURUI_NAME1 = rst.Key.HAIKI_SHURUI_NAME1,
                                  HAIKI_SHURUI_NAME2 = rst.Key.HAIKI_SHURUI_NAME2,
                                  HAIKI_SHURUI_NAME3 = rst.Key.HAIKI_SHURUI_NAME3,
                                  HAIKI_SHURUI_NAME4 = rst.Key.HAIKI_SHURUI_NAME4,
                                  SBN_AFTER_HAIKI_NAME = rst.Key.SBN_AFTER_HAIKI_NAME,
                                  SHOBUN_HOUHOU_NAME = rst.Key.SHOBUN_HOUHOU_NAME,
                                  UNIT_NAME = rst.Key.UNIT_NAME,
                                  DATA = rst.ToArray()
                              };
                    foreach (var c in csv)
                    {
                        decimal? sbn1 = null;
                        decimal? sbn2 = null;
                        decimal? sbn3 = null;
                        decimal? sbn4 = null;
                        decimal? hst = null;
                        decimal? sbn = null;
                        foreach (T_JISSEKI_HOUKOKU_SHORI_DETAIL groupResult in c.DATA)
                        {
                            this.Sum(ref sbn1, this.ToNDecima(groupResult.SBN_RYOU1));
                            this.Sum(ref sbn2, this.ToNDecima(groupResult.SBN_RYOU2));
                            this.Sum(ref sbn3, this.ToNDecima(groupResult.SBN_RYOU3));
                            this.Sum(ref sbn4, this.ToNDecima(groupResult.SBN_RYOU4));
                            this.Sum(ref hst, this.ToNDecima(groupResult.HST_RYOU));
                            this.Sum(ref sbn, this.ToNDecima(groupResult.SBN_RYOU));
                        }
                        DataRow dr = dt.NewRow();
                        dr["処理施設名"] = c.SHORI_SHISETSU_NAME;
                        dr["種類名1"] = c.HAIKI_SHURUI_NAME1;
                        if (!string.IsNullOrEmpty(c.HAIKI_SHURUI_NAME1))
                        {
                            dr["単位1"] = c.UNIT_NAME;
                            dr["処分量1"] = this.ConvertSuuryoCSV(sbn1) ?? "";
                        }
                        dr["種類名2"] = c.HAIKI_SHURUI_NAME2;
                        if (!string.IsNullOrEmpty(c.HAIKI_SHURUI_NAME2))
                        {
                            dr["単位2"] = c.UNIT_NAME;
                            dr["処分量2"] = this.ConvertSuuryoCSV(sbn2) ?? "";
                        }
                        dr["種類名3"] = c.HAIKI_SHURUI_NAME3;
                        if (!string.IsNullOrEmpty(c.HAIKI_SHURUI_NAME3))
                        {
                            dr["単位3"] = c.UNIT_NAME;
                            dr["処分量3"] = this.ConvertSuuryoCSV(sbn3) ?? "";
                        }
                        dr["種類名4"] = c.HAIKI_SHURUI_NAME4;
                        if (!string.IsNullOrEmpty(c.HAIKI_SHURUI_NAME4))
                        {
                            dr["単位4"] = c.UNIT_NAME;
                            dr["処分量4"] = this.ConvertSuuryoCSV(sbn4) ?? "";
                        }
                        dr["処分後廃棄物名"] = c.SBN_AFTER_HAIKI_NAME;
                        if (!string.IsNullOrEmpty(c.SBN_AFTER_HAIKI_NAME))
                        {
                            dr["排出単位"] = c.UNIT_NAME;
                            dr["排出量"] = this.ConvertSuuryoCSV(hst) ?? "";
                            dr["処分方法名"] = c.SHOBUN_HOUHOU_NAME;
                            dr["処分単位"] = c.UNIT_NAME;
                            dr["処分量"] = this.ConvertSuuryoCSV(sbn) ?? "";
                        }
                        dt.Rows.Add(dr);
                    }
                }
                else
                {
                    foreach (T_JISSEKI_HOUKOKU_SHORI_DETAIL c in this.tjHoukokuSyoriEntryList)
                    {
                        DataRow dr = dt.NewRow();
                        dr["処理施設名"] = c.SHORI_SHISETSU_NAME;
                        dr["種類名1"] = c.HAIKI_SHURUI_NAME1;
                        dr["処分量1"] = c.SBN_RYOU1.IsNull ? "" : this.ConvertSuuryo(c.SBN_RYOU1);
                        dr["単位1"] = c.UNIT_NAME;
                        dr["種類名2"] = c.HAIKI_SHURUI_NAME2;
                        dr["処分量2"] = c.SBN_RYOU2.IsNull ? "" : this.ConvertSuuryo(c.SBN_RYOU2);
                        dr["単位2"] = c.UNIT_NAME;
                        dr["種類名3"] = c.HAIKI_SHURUI_NAME3;
                        dr["処分量3"] = c.SBN_RYOU3.IsNull ? "" : this.ConvertSuuryo(c.SBN_RYOU3);
                        dr["単位3"] = c.UNIT_NAME;
                        dr["種類名4"] = c.HAIKI_SHURUI_NAME4;
                        dr["処分量4"] = c.SBN_RYOU4.IsNull ? "" : this.ConvertSuuryo(c.SBN_RYOU4);
                        dr["単位4"] = c.UNIT_NAME;
                        dr["処分後廃棄物名"] = c.SBN_AFTER_HAIKI_NAME;
                        dr["排出量"] = c.HST_RYOU.IsNull ? "" : this.ConvertSuuryo(c.HST_RYOU);
                        dr["排出単位"] = c.UNIT_NAME;
                        dr["処分方法名"] = c.SHOBUN_HOUHOU_NAME;
                        dr["処分量"] = c.SBN_RYOU.IsNull ? "" : this.ConvertSuuryo(c.SBN_RYOU);
                        dr["処分単位"] = c.UNIT_NAME;
                        dt.Rows.Add(dr);
                    }
                }
                this.form.dgv_csv.DataSource = dt;
                CSVExport CSVExp = new CSVExport();
                CSVExp.ConvertCustomDataGridViewToCsv(this.form.dgv_csv, true, true, "処理施設実績報告書", this.form);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVOutput", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// double?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal decimal? ToNDecima(object o)
        {
            decimal? ret = null;
            decimal parse = 0;
            if (decimal.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        /// <summary>
        /// 計算
        /// </summary>
        /// <param name="o">o</param>
        internal void Sum(ref decimal? ret, decimal? add)
        {
            if (add != null)
            {
                if (ret == null)
                {
                    ret = 0;
                }
                ret += add;
            }
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
        /// 数量フォーマット(CSV用)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertSuuryoCSV(decimal? value)
        {
            string result = string.Empty;
            string format = "#,###.#";
            if (!string.IsNullOrEmpty(this.sysInfoEntity.MANIFEST_SUURYO_FORMAT))
            {
                format = this.sysInfoEntity.MANIFEST_SUURYO_FORMAT;
            }
            if (value != null)
            {
                result = value.Value.ToString(format);
            }
            return result;
        }

        #region マスタ検索処理
        /// <summary>
        /// 業者検索処理
        /// </summary>
        /// <param name="cd">CD</param>
        internal M_GYOUSHA[] GetGyousha(string cd, out bool catchErr)
        {
            M_GYOUSHA[] results = null;
            catchErr = false;
            try
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
                results = dao.GetAllValidData(dto);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousha", ex);
                this.MsgBox.MessageBoxShow("E245", "");
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
        internal M_GENBA[] GetGenba(string gyoushaCd, string genbaCd, out bool catchErr)
        {
            M_GENBA[] results = null;
            catchErr = false;
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
                this.MsgBox.MessageBoxShow("E245", "");
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
            M_GENBA[] results = null;
            catchErr = false;
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
                LogUtility.Error("GetGenba1", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(results, catchErr);
            }
            return results;
        }
        #endregion
    }
}
