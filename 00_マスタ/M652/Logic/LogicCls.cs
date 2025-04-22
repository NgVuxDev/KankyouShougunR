// $Id: LogicCls.cs 48256 2015-04-24 07:49:36Z wuq@oec-h.com $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Master.KaishiZaikoJouhouHoshu.APP;
using Shougun.Core.Master.KaishiZaikoJouhouHoshu.DAO;

namespace Shougun.Core.Master.KaishiZaikoJouhouHoshu.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.KaishiZaikoJouhouHoshu.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        public MasterBaseForm parentForm;

        /// <summary>
        /// 品名dao
        /// </summary>
        private IM_HINMEIDao HinmeiDao;

        /// <summary>
        /// 在庫比率dao
        /// </summary>
        private DaoCls daoCls;

        /// <summary>
        /// 在庫品名dao
        /// </summary>
        //private ZaikoHinmeiDao ZaikoHinmeiDao;
        private IM_KAISHI_ZAIKO_INFODao kaishiZaikoInfoDao;

        private IM_ZAIKO_HINMEIDao zaikoHinmeiDao;

        private IM_GYOUSHADao gyoushaDao;

        private IM_GENBADao genbaDao;

        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// メッセージ出力用のユーティリティ
        /// </summary>
        private MessageUtility MessageUtil;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索結果copy
        /// </summary>
        public DataTable dtSearchResult { get; set; }

        /// <summary>
        /// 在庫品名
        /// </summary>
        public M_ZAIKO_HINMEI[] SearchZaikoHinmei { get; set; }

        /// <summary>
        /// テーブルクリア用
        /// </summary>
        public DataTable ClearTable { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        private M_KAISHI_ZAIKO_INFO SearchString { get; set; }

        /// <summary>
        /// 品名読込み用
        /// 表示されている一覧を保持し、登録時に削除
        /// </summary>
        public M_KAISHI_ZAIKO_INFO[] deleteEntitys;

        /// <summary>
        /// 基本品名マスタ読み込み中
        /// True＝読み込み中
        /// </summary>
        internal bool isNowLoadingZaikoHinmeiMaster = false;

        private MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        public M_GYOUSHA prevGyousha = null;

        public M_GENBA prevGenba = null;

        public bool isRegist = true;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.form = targetForm;

                this.HinmeiDao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
                this.daoCls = DaoInitUtility.GetComponent<DaoCls>();
                this.zaikoHinmeiDao = DaoInitUtility.GetComponent<IM_ZAIKO_HINMEIDao>();
                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.kaishiZaikoInfoDao = DaoInitUtility.GetComponent<IM_KAISHI_ZAIKO_INFODao>();
                this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
                // メッセージ出力用のユーティリティ
                MessageUtil = new MessageUtility();
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicCls", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 親フォームオブジェクト取得
                this.parentForm = (MasterBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                //画面初期値設定
                this.DefaultInit();

                this.HearderInit();

                //前の表示条件を初期値に設定する
                if (Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED)
                {
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                }

                this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_VALUE.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M652", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }

                this.form.Ichiran.AllowUserToAddRows = false;

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #region 参照モード表示

        /// <summary>
        /// 参照モード表示に変更します
        /// </summary>
        private void DispReferenceMode()
        {
            // MainForm
            this.form.Ichiran.ReadOnly = true;
            this.form.Ichiran.AllowUserToAddRows = false;
            this.form.Ichiran.IsBrowsePurpose = true;

            this.form.Ichiran.CausesValidation = false;

            // FunctionButton
            this.parentForm.bt_func4.Enabled = false;
            this.parentForm.bt_func6.Enabled = true;
            this.parentForm.bt_func9.Enabled = false;

            this.parentForm.bt_process1.Enabled = false;
        }

        #endregion

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();

                ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, this.form.WindowType);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();
                var thisAssembly = Assembly.GetExecutingAssembly();

                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 削除ボタン(F4)イベント生成
                this.form.C_MasterRegist(this.parentForm.bt_func4);
                this.parentForm.bt_func4.Click -= this.form.LogicalDelete;
                this.parentForm.bt_func4.Click += this.form.LogicalDelete;
                this.parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

                //CSV出力ボタン(F6)イベント生成
                this.parentForm.bt_func6.Click -= this.form.CSVOutput;
                this.parentForm.bt_func6.Click += this.form.CSVOutput;

                //条件クリアボタン(F7)イベント生成
                this.parentForm.bt_func7.Click -= this.form.ClearCondition;
                this.parentForm.bt_func7.Click += this.form.ClearCondition;
                //this.parentForm.bt_func7.CausesValidation = false;

                //検索ボタン(F8)イベント生成
                this.parentForm.bt_func8.Click -= this.form.Search;
                this.parentForm.bt_func8.Click += this.form.Search;
                //this.parentForm.bt_func8.CausesValidation = false;

                //登録ボタン(F9)イベント生成
                this.form.C_MasterRegist(this.parentForm.bt_func9);
                this.parentForm.bt_func9.Click -= this.form.Regist;
                this.parentForm.bt_func9.Click += this.form.Regist;
                this.parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                //取消ボタン(F11)イベント生成
                this.parentForm.bt_func11.Click -= this.form.Cancel;
                this.parentForm.bt_func11.Click += this.form.Cancel;
                //this.parentForm.bt_func11.CausesValidation = false;

                //閉じるボタン(F12)イベント生成
                this.parentForm.bt_func12.Click -= this.form.FormClose;
                this.parentForm.bt_func12.Click += this.form.FormClose;
                //this.parentForm.bt_func12.CausesValidation = false;

                //this.parentForm.bt_process1.Text = "[1]";
                this.parentForm.bt_process1.Click -= this.form.ZaikoHinmeiLoad;
                this.parentForm.bt_process1.Click += this.form.ZaikoHinmeiLoad;
                this.parentForm.bt_process1.Enabled = true;
                //this.parentForm.bt_process2.Text = "[2]";
                this.parentForm.bt_process2.Enabled = false;
                //this.parentForm.bt_process3.Text = "[3]";
                this.parentForm.bt_process3.Enabled = false;
                //this.parentForm.bt_process4.Text = "[4]";
                this.parentForm.bt_process4.Enabled = false;
                //this.parentForm.bt_process5.Text = "[5]";
                this.parentForm.bt_process5.Enabled = false;
                this.parentForm.txb_process.Enabled = true;

                /// 20141217 Houkakou 「在庫比率入力」の日付チェックを追加する　start
                this.form.Ichiran.CellValidated -= this.form.Ichiran_CellValidated;
                this.form.Ichiran.CellValidated += this.form.Ichiran_CellValidated;
                /// 20141217 Houkakou 「在庫比率入力」の日付チェックを追加する　end

                this.form.GYOUSHA_CD.Enter += new EventHandler(this.form.GYOUSHA_CD_Enter);
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region Func処理

        /// <summary>
        /// CSV出力
        /// </summary>
        public bool CSVOutput()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                if (this.form.Ichiran.RowCount <= 1)
                {
                    msgLogic.MessageBoxShow("E044");
                }
                else if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_KAISHI_ZAIKO_INFO), this.form);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVOutput", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 画面データ検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int count = 0;
            try
            {
                LogUtility.DebugMethodStart();

                if (String.IsNullOrEmpty(this.form.GYOUSHA_CD.Text.ToString()) || String.IsNullOrEmpty(this.form.GENBA_CD.Text.ToString()))
                {
                    return 0;
                }

                this.form.Ichiran.AllowUserToAddRows = true;

                if (!SetSearchString())
                {
                    this.dtSearchResult.Rows.Clear();
                    this.SetIchiranData();
                    return 0;
                }

                this.SearchResult = this.daoCls.GetIchiranDataSql(
                    this.SearchString,
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked
                    );
                this.dtSearchResult = this.SearchResult.Copy();
                this.form.Ichiran.CellValidated -= this.form.Ichiran_CellValidated;
                foreach (DataGridViewColumn DGVcolumn in this.form.Ichiran.Columns)
                {
                    switch (DGVcolumn.Name)
                    {
                        case "DELETE_FLG":
                        case "ZAIKO_HINMEI_CD":
                        case "KAISHI_ZAIKO_RYOU":
                        case "KAISHI_ZAIKO_KINGAKU":
                            DGVcolumn.ReadOnly = false;
                            break;
                    }
                }
                this.parentForm.bt_func4.Enabled = true;
                this.parentForm.bt_func6.Enabled = true;
                this.parentForm.bt_func9.Enabled = true;
                this.SetIchiranData();
                this.form.Ichiran.CellValidated += this.form.Ichiran_CellValidated;

                SaveDefaultCondition();
                this.isNowLoadingZaikoHinmeiMaster = false;
                count = this.SearchResult.Rows == null ? 0 : 1;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                count = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                count = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }
            return count;
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);
            //if (!CheckBeforeUpdate())
            //{
            //    //入力チェック失敗、処理を終了する。
            //    return;
            //}
            List<M_KAISHI_ZAIKO_INFO> entitys = GetIchiranList();

            //bool ret = true;
            this.isRegist = true;
            try
            {
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    using (Transaction tran = new Transaction())
                    {
                        // トランザクション開始
                        foreach (M_KAISHI_ZAIKO_INFO kaishiZaikoInfo in entitys)
                        {
                            var entity = this.kaishiZaikoInfoDao.GetDataByCd(kaishiZaikoInfo.GYOUSHA_CD, kaishiZaikoInfo.GENBA_CD, kaishiZaikoInfo.ZAIKO_HINMEI_CD);

                            if (entity == null)
                            {
                                this.kaishiZaikoInfoDao.Insert(kaishiZaikoInfo);
                            }
                            else
                            {
                                kaishiZaikoInfo.TIME_STAMP = entity.TIME_STAMP;
                                this.kaishiZaikoInfoDao.Update(kaishiZaikoInfo);
                            }
                        }
                        tran.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    // 排他エラーの場合
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
                this.isRegist = false;
            }

            if (this.isRegist)
            {
                msgLogic.MessageBoxShow("I001", "登録");
                //this.Search();
                //setDefaultCondition();
            }

            LogUtility.DebugMethodEnd(errorFlag);
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                CancelInit();
                //this.Search();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Cancel", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// クリア
        /// </summary>
        public bool ClearCondition()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.form.CONDITION_VALUE.Text = string.Empty;
                this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
                this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
                this.form.CONDITION_ITEM.Text = string.Empty;

                GetSysInfoInit();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ClearCondition", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region データ設定

        /// <summary>
        ///  画面初期値設定
        /// </summary>
        private void DefaultInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //[表示条件]システム設定(M_SYS_INFO)より値を取得、初期値としてセット。
                GetSysInfoInit();

                //品名CD、品名を設定
                this.form.GYOUSHA_CD.Text = Properties.Settings.Default.GYOUSHA_CD;
                this.form.GYOUSHA_CD.prevText = Properties.Settings.Default.GYOUSHA_CD;
                this.form.GYOUSHA_NAME_RYAKU.Text = Properties.Settings.Default.GYOUSHA_NAME_RYAKU;
                if (!String.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    this.prevGyousha = gyoushaDao.GetDataByCd(this.form.GYOUSHA_CD.Text);
                }

                this.form.GENBA_CD.Text = Properties.Settings.Default.GENBA_CD;
                this.form.GENBA_CD.prevText = Properties.Settings.Default.GENBA_CD;
                this.form.GENBA_NAME_RYAKU.Text = Properties.Settings.Default.GENBA_NAME_RYAKU;
                if (!String.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) && !String.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    this.prevGenba = genbaDao.GetDataByCd(new M_GENBA() { GYOUSHA_CD = this.form.GYOUSHA_CD.Text, GENBA_CD = this.form.GENBA_CD.Text });
                }

                //[検索条件]は全てクリア
                this.form.CONDITION_VALUE.Text = string.Empty;
                this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
                this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
                this.form.CONDITION_ITEM.Text = string.Empty;

                //明細部クリア
                CreateNoDataRecord();
            }
            catch (Exception ex)
            {
                LogUtility.Error("DefaultInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        ///  ヘットタイトル設定
        /// </summary>
        /// <param name="type">1、受入　2、出荷</param>
        public void HearderInit()
        {
            ControlUtility controlUtil = new ControlUtility();

            var titleControl = (Label)controlUtil.FindControl(this.parentForm, "lb_title");

            titleControl.Text = this.form.WindowId.ToTitleString();
        }

        /// <summary>
        ///  取り消し設定
        /// </summary>
        private void CancelInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //[表示条件]システム設定(M_SYS_INFO)より値を取得、初期値としてセット。
                GetSysInfoInit();

                //品名CD、品名を設定
                this.form.GYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.prevGyousha = null;

                this.form.GENBA_CD.Text = string.Empty;
                this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                this.prevGenba = null;

                //[検索条件]は全てクリア
                this.form.CONDITION_VALUE.Text = string.Empty;
                this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
                this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
                this.form.CONDITION_ITEM.Text = string.Empty;

                //明細部クリア
                CreateNoDataRecord();
            }
            catch (Exception ex)
            {
                LogUtility.Error("DefaultInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 一覧データの検索条件設定
        /// </summary>
        private bool SetSearchString()
        {
            try
            {
                LogUtility.DebugMethodStart();
                M_KAISHI_ZAIKO_INFO entityKaishiZaiko = new M_KAISHI_ZAIKO_INFO();

                if (!this.form.GYOUSHA_CD.Text.Equals(null) && !this.form.GYOUSHA_CD.Text.ToString().Trim().Equals(string.Empty))
                {
                    entityKaishiZaiko.GYOUSHA_CD = this.form.GYOUSHA_CD.Text.ToString().Trim();
                }
                if (!this.form.GENBA_CD.Text.Equals(null) && !this.form.GENBA_CD.Text.ToString().Trim().Equals(string.Empty))
                {
                    entityKaishiZaiko.GENBA_CD = this.form.GENBA_CD.Text.ToString().Trim();
                }

                //検索条件の設定
                entityKaishiZaiko.SetValue(this.form.CONDITION_VALUE);

                this.SearchString = entityKaishiZaiko;
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSearchString", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// Properties設定
        /// </summary>
        public void SaveDefaultCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();
                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;

                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

                Properties.Settings.Default.GENBA_CD = this.form.GENBA_CD.Text;
                Properties.Settings.Default.GENBA_NAME_RYAKU = this.form.GENBA_NAME_RYAKU.Text;

                Properties.Settings.Default.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                Properties.Settings.Default.GYOUSHA_NAME_RYAKU = this.form.GYOUSHA_NAME_RYAKU.Text;

                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                LogUtility.Error("setDefaultCondition", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 表示条件の初期値設定
        /// </summary>
        private void GetSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // システム情報を取得し、初期値をセットする
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.sysInfoEntity = sysInfo[0];
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = (bool)this.sysInfoEntity.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSysInfoInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 一覧データの設定
        /// </summary>
        public bool SetIchiranData()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                this.ColumnAllowDBNull(this.dtSearchResult);

                this.form.Ichiran.CellValidating -= this.form.Ichiran_CellValidating;
                this.form.Ichiran.DataSource = this.dtSearchResult;
                this.form.Ichiran.CellValidating += this.form.Ichiran_CellValidating;

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M652", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        ///  画面から一覧データを取得する
        /// </summary>
        private List<M_KAISHI_ZAIKO_INFO> GetIchiranList()
        {
            LogUtility.DebugMethodStart();
            List<M_KAISHI_ZAIKO_INFO> entitys = new List<M_KAISHI_ZAIKO_INFO>();
            try
            {
                DataTable dtIchiran = this.form.Ichiran.DataSource as DataTable;
                if (dtIchiran == null || dtIchiran.Rows.Count == 0)
                {
                    return entitys;
                }

                ColumnAllowDBNull(dtIchiran);
                dtIchiran.BeginLoadData();

                //変更したデータを取得
                if (dtIchiran.GetChanges() == null)
                {
                    return entitys;
                }
                else
                {
                    dtIchiran = dtIchiran.GetChanges();
                }

                // 変更分のみ取得
                DataTable addList = new DataTable();
                addList = dtIchiran.Clone();
                for (int i = 0; i < dtIchiran.Rows.Count; i++)
                {
                    if ((!DBNull.Value.Equals(dtIchiran.Rows[i]["ZAIKO_HINMEI_CD"])) &&
                              dtIchiran.Rows[i]["ZAIKO_HINMEI_CD"] != null &&
                              (!"".Equals(dtIchiran.Rows[i]["ZAIKO_HINMEI_CD"])))
                    {
                        if (this.SearchResult != null && this.SearchResult.Rows.Count > 0)
                        {
                            //DataView dv = this.SearchResult.DefaultView;
                            //dv.RowFilter = ("ZAIKO_HINMEI_CD=" + dtIchiran.Rows[i]["ZAIKO_HINMEI_CD"]);
                            //DataRow[] dtRows = this.SearchResult.Select("ZAIKO_HINMEI_CD=" + dtIchiran.Rows[i]["ZAIKO_HINMEI_CD"].ToString());
                            //if (dtRows.Length > 0)
                            //{
                            DataRow row = addList.NewRow();
                            row.ItemArray = dtIchiran.Rows[i].ItemArray;
                            addList.Rows.Add(row);
                            continue;
                            //}
                        }

                        //新規データかつ削除フラグ=falseの場合
                        bool deleteFlg = string.IsNullOrEmpty(Convert.ToString(dtIchiran.Rows[i]["DELETE_FLG"])) ? false : Convert.ToBoolean(Convert.ToString(dtIchiran.Rows[i]["DELETE_FLG"]));
                        if (!deleteFlg)
                        {
                            DataRow row = addList.NewRow();
                            row.ItemArray = dtIchiran.Rows[i].ItemArray;
                            addList.Rows.Add(row);
                        }
                    }
                }

                if (addList.Rows.Count > 0)
                {
                    entitys = CreateEntityData(addList);
                }
                return entitys;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetIchiranList", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 登録データ準備
        /// </summary>
        /// <param name="addList"></param>
        /// <returns></returns>
        public List<M_KAISHI_ZAIKO_INFO> CreateEntityData(DataTable addList, bool isDeleteFlg = false)
        {
            LogUtility.DebugMethodStart(addList, isDeleteFlg);
            var entityList = new List<M_KAISHI_ZAIKO_INFO>();
            try
            {
                for (int i = 0; i < addList.Rows.Count; i++)
                {
                    M_KAISHI_ZAIKO_INFO mZaikoHiritsu = new M_KAISHI_ZAIKO_INFO();
                    DataRow row = addList.Rows[i];
                    //var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_KAISHI_ZAIKO_INFO>(mZaikoHiritsu);
                    if (!isDeleteFlg)
                    {
                        //保存と更新データを取得
                        bool deleteFlg = string.IsNullOrEmpty(Convert.ToString(row["DELETE_FLG"])) ? false : Convert.ToBoolean(Convert.ToString(row["DELETE_FLG"]));
                        if (deleteFlg)
                        {
                            continue;
                        }
                        if (string.IsNullOrEmpty(row["ZAIKO_HINMEI_CD"].ToString()))
                        {
                            continue;
                        }

                        //dataBinderLogic.SetSystemProperty(mZaikoHiritsu, false);
                        entityList.Add(GetEntity(row, isDeleteFlg));
                    }
                    else
                    {
                        bool deleteFlg = string.IsNullOrEmpty(Convert.ToString(row["DELETE_FLG"])) ? false : Convert.ToBoolean(Convert.ToString(row["DELETE_FLG"]));
                        //削除データを取得
                        if (deleteFlg)
                        {
                            entityList.Add(GetEntity(row, isDeleteFlg));
                        }
                    }
                }
                return entityList;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(entityList, isDeleteFlg);
            }
        }

        /// <summary>
        /// データを取得
        /// </summary>
        /// <param name="disPlayRow"></param>
        /// <param name="isDleteFlg"></param>
        /// <returns></returns>
        internal M_KAISHI_ZAIKO_INFO GetEntity(DataRow disPlayRow, bool isDleteFlg)
        {
            M_KAISHI_ZAIKO_INFO entity = new M_KAISHI_ZAIKO_INFO();
            var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_KAISHI_ZAIKO_INFO>(entity);

            if (this.form.GYOUSHA_CD.Text != null && this.form.GYOUSHA_CD.Text.ToString().Trim() != "")
            {
                entity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text.ToString();
            }

            if (this.form.GENBA_CD.Text != null && this.form.GENBA_CD.Text.ToString().Trim() != "")
            {
                entity.GENBA_CD = this.form.GENBA_CD.Text.ToString();
            }

            //在庫品名CD
            if (!DBNull.Value.Equals(disPlayRow["ZAIKO_HINMEI_CD"])
                && disPlayRow["ZAIKO_HINMEI_CD"] != null
                && !string.IsNullOrEmpty(disPlayRow["ZAIKO_HINMEI_CD"].ToString()))
            {
                entity.ZAIKO_HINMEI_CD = disPlayRow["ZAIKO_HINMEI_CD"].ToString();
            }
            //在庫品名
            if (!DBNull.Value.Equals(disPlayRow["ZAIKO_HINMEI_NAME_RYAKU"])
                && disPlayRow["ZAIKO_HINMEI_NAME_RYAKU"] != null
                && !string.IsNullOrEmpty(disPlayRow["ZAIKO_HINMEI_NAME_RYAKU"].ToString()))
            {
                entity.ZAIKO_HINMEI_NAME_RYAKU = disPlayRow["ZAIKO_HINMEI_NAME_RYAKU"].ToString();
            }

            if (!DBNull.Value.Equals(disPlayRow["KAISHI_ZAIKO_RYOU"])
                && disPlayRow["KAISHI_ZAIKO_RYOU"] != null
                && !string.IsNullOrEmpty(disPlayRow["KAISHI_ZAIKO_RYOU"].ToString()))
            {
                entity.KAISHI_ZAIKO_RYOU = Decimal.Parse(disPlayRow["KAISHI_ZAIKO_RYOU"].ToString());
            }
            if (!DBNull.Value.Equals(disPlayRow["KAISHI_ZAIKO_KINGAKU"])
                  && disPlayRow["KAISHI_ZAIKO_KINGAKU"] != null
                  && !string.IsNullOrEmpty(disPlayRow["KAISHI_ZAIKO_KINGAKU"].ToString()))
            {
                entity.KAISHI_ZAIKO_KINGAKU = Decimal.Parse(disPlayRow["KAISHI_ZAIKO_KINGAKU"].ToString());
            }
            if (!DBNull.Value.Equals(disPlayRow["KAISHI_ZAIKO_TANKA"])
                 && disPlayRow["KAISHI_ZAIKO_TANKA"] != null
                 && !string.IsNullOrEmpty(disPlayRow["KAISHI_ZAIKO_TANKA"].ToString()))
            {
                entity.KAISHI_ZAIKO_TANKA = Decimal.Parse(disPlayRow["KAISHI_ZAIKO_TANKA"].ToString());
            }
            //削除フラグ
            entity.DELETE_FLG = isDleteFlg;

            if (!DBNull.Value.Equals(disPlayRow["TIME_STAMP"]))
            {
                entity.TIME_STAMP = (byte[])disPlayRow["TIME_STAMP"];
            }
            dataBinderLogic.SetSystemProperty(entity, isDleteFlg);
            return entity;
        }

        /// <summary>
        /// NOT NULL制約を一時的に解除
        /// </summary>
        public void ColumnAllowDBNull(DataTable dt)
        {
            try
            {
                LogUtility.DebugMethodStart(dt);
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals("TIME_STAMP"))
                    {
                        column.Unique = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ColumnAllowDBNull", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 入力チェック

        /// <summary>
        /// 登録前のチェック
        /// </summary>
        /// <returns></returns>
        private bool CheckBeforeUpdate()
        {
            return true;
            //bool ret = false;
            //try
            //{
            //    LogUtility.DebugMethodStart();

            //    //DataGridViewデータ0件チェック
            //    if (this.form.Ichiran.Rows.Count < 2)
            //    {
            //        msgLogic.MessageBoxShow("E061");
            //        return ret;
            //    }

            //    if (SearchString != null)
            //    {
            //        string msgPram = "";
            //        if (!this.form.GYOUSHA_CD.Text.Equals(SearchString.GYOUSHA_CD) || !this.form.GENBA_CD.Text.Equals(SearchString.GENBA_CD))
            //        {
            //            msgPram += "品名CD";
            //            this.form.GYOUSHA_CD.Focus();
            //        }

            //        if (msgPram != "")
            //        {
            //            msgLogic.MessageBoxShow("E095", msgPram);
            //            return ret;
            //        }
            //    }

            //    ret = true;
            //    return ret;
            //}
            //catch (Exception ex)
            //{
            //    LogUtility.Error("CheckBeforeUpdate", ex);
            //    throw;
            //}
            //finally
            //{
            //    LogUtility.DebugMethodEnd();
            //}
        }

        /// <summary>
        /// 明細部・在庫品名CD入力チェック
        /// </summary>
        /// <param name="zaikoHinmeiCD"></param>
        /// <returns></returns>
        public bool ZaikoHinmeCDCheck(string zaikoHinmeiCD)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(zaikoHinmeiCD);
                if (!DuplicationCheck(zaikoHinmeiCD))
                {
                    msgLogic.MessageBoxShow("E003", "在庫品名CD", zaikoHinmeiCD);
                    ret = false;
                }
                else if (!ExistZaikoHinmei(zaikoHinmeiCD))
                {
                    msgLogic.MessageBoxShow("E020", "在庫品名");
                    ret = false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ZaikoHinmeCDCheck", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZaikoHinmeCDCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 明細部・在庫品名CD存在チェック
        /// </summary>
        /// <param name="zaikoHinmeiCD"></param>
        /// <returns></returns>
        private bool ExistZaikoHinmei(string zaikoHinmeiCD)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(zaikoHinmeiCD);
                //存在チェック
                this.SearchZaikoHinmei = this.zaikoHinmeiDao.GetAllValidData(new M_ZAIKO_HINMEI() { ZAIKO_HINMEI_CD = zaikoHinmeiCD });
                if (SearchZaikoHinmei.Length < 1)
                {
                    ret = false;
                    return false;
                }
                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ExistZaikoHinmei", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 明細部・在庫品名CD重複チェック
        /// </summary>
        /// <returns></returns>
        private bool DuplicationCheck(string zaikoHinmeiCD)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(zaikoHinmeiCD);

                // 画面で在庫品名CD重複チェック
                int recCount = 0;
                for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells[Const.Constans.ZAIKO_HINMEI_CD].Value.ToString().PadLeft(6, '0').Equals(Convert.ToString(zaikoHinmeiCD)))
                    {
                        recCount++;
                    }
                }

                if (recCount > 1)
                {
                    ret = false;
                    return ret;
                }

                // 検索結果で在庫品名CD重複チェック
                if (this.SearchResult != null)
                {
                    for (int i = 0; i < this.SearchResult.Rows.Count; i++)
                    {
                        if (zaikoHinmeiCD.Equals(this.SearchResult.Rows[i][Const.Constans.ZAIKO_HINMEI_CD]))
                        {
                            return ret;
                        }
                    }
                }
                if (!this.isNowLoadingZaikoHinmeiMaster)
                {
                    // DBで在庫品名CD重複チェック
                    M_KAISHI_ZAIKO_INFO data = new M_KAISHI_ZAIKO_INFO();
                    data.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    data.GENBA_CD = this.form.GENBA_CD.Text;
                    data.ZAIKO_HINMEI_CD = zaikoHinmeiCD;
                    var kaishiZaikoInfo = this.kaishiZaikoInfoDao.GetDataByCd(data.GYOUSHA_CD, data.GENBA_CD, data.ZAIKO_HINMEI_CD);

                    if (kaishiZaikoInfo != null)
                    {
                        ret = false;
                        return ret;
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
        }

        #region Mulit行メッセージを生成(削除)

        /// <summary>
        /// Mulit行メッセージを生成
        /// </summary>
        /// <param name="msgID">メッセージID</param>
        /// <param name="str">整形時に利用する文言のリスト</param>
        /// <returns>整形済みメッセージ</returns>
        private string CreateMulitMessage(string msgID, params string[] str)
        {
            LogUtility.DebugMethodStart(msgID, str);
            // 整形済みメッセージ
            string msgResult = string.Empty;
            try
            {
                // メッセージ原本
                string msg = MessageUtil.GetMessage("E001").MESSAGE;

                for (int i = 0; i < str.Length; i++)
                {
                    string msgTmp = string.Format(msg, str[i]);
                    if (!string.IsNullOrEmpty(msgResult))
                    {
                        msgResult += "\r\n";
                    }
                    msgResult += msgTmp;
                }
                LogUtility.DebugMethodEnd();
                return msgResult;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateMulitMessage", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(msgResult);
            }
        }

        #endregion

        #endregion

        #region 空白1レコードを追加

        /// <summary>
        /// 空白1レコードを追加する
        /// </summary>
        public void CreateNoDataRecord()
        {
            // LogUtility.DebugMethodStart();
            try
            {
                #region テーブル作成

                this.ClearTable = this.daoCls.GetIchiranDataSql(
                    new r_framework.Entity.M_KAISHI_ZAIKO_INFO() { GYOUSHA_CD = "------", GENBA_CD = "------", ZAIKO_HINMEI_CD = "------" },
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);

                #endregion

                this.ColumnAllowDBNull(this.ClearTable);
                this.form.Ichiran.CellValidating -= this.form.Ichiran_CellValidating;
                this.form.Ichiran.CellValidated -= this.form.Ichiran_CellValidated;

                foreach (DataGridViewColumn DGVcolumn in this.form.Ichiran.Columns)
                {
                    switch (DGVcolumn.Name)
                    {
                        case "DELETE_FLG":
                        case "ZAIKO_HINMEI_CD":
                        case "KAISHI_ZAIKO_RYOU":
                        case "KAISHI_ZAIKO_KINGAKU":
                            DGVcolumn.ReadOnly = true;
                            break;
                    }
                }
                this.parentForm.bt_func4.Enabled = false;
                this.parentForm.bt_func6.Enabled = false;
                this.parentForm.bt_func9.Enabled = false;

                this.form.Ichiran.DataSource = this.ClearTable;
                this.form.Ichiran.AllowUserToAddRows = false;

                this.form.Ichiran.CellValidating += this.form.Ichiran_CellValidating;
                this.form.Ichiran.CellValidated += this.form.Ichiran_CellValidated;

                //LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        #region IBuisinessLogicで必須実装(論理削除以外未使用)

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete(List<M_KAISHI_ZAIKO_INFO> listDelete)
        {
            LogUtility.DebugMethodStart(listDelete);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                this.isRegist = true;
                using (Transaction tran = new Transaction())
                {
                    foreach (M_KAISHI_ZAIKO_INFO kaishiZaikoEntity in listDelete)
                    {
                        M_KAISHI_ZAIKO_INFO entity = this.kaishiZaikoInfoDao.GetDataByCd(kaishiZaikoEntity.GYOUSHA_CD, kaishiZaikoEntity.GENBA_CD, kaishiZaikoEntity.ZAIKO_HINMEI_CD);
                        if (entity != null)
                        {
                            entity.DELETE_FLG = true;
                            this.kaishiZaikoInfoDao.Update(entity);
                        }
                    }
                    tran.Commit();
                    msgLogic.MessageBoxShow("I001", "削除");
                }
                this.form.Search(null, null);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    this.form.errmessage.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex); //DBエラー
                    this.form.errmessage.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.form.errmessage.MessageBoxShow("E245");
                }
                this.isRegist = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(listDelete);
            }
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Ichiran_Leaveイベント

        /// <summary>
        /// Ichiran_Leaveイベント
        /// </summary>
        /// <returns></returns>
        internal bool IchiranCellValidated(DataGridViewCellEventArgs e)
        {
            bool ret = true;
            try
            {
                if (this.form.Ichiran.Columns[e.ColumnIndex].Name.Equals("KAISHI_ZAIKO_RYOU") ||
                    this.form.Ichiran.Columns[e.ColumnIndex].DataPropertyName.Equals("KAISHI_ZAIKO_KINGAKU"))
                {
                    decimal KAISHI_ZAIKO_RYOU = 0;
                    decimal KAISHI_ZAIKO_KINGAKU = 0;
                    decimal KAISHI_ZAIKO_TANKA = 0;
                    string strKAISHI_ZAIKO_RYOU = Convert.ToString(this.form.Ichiran.Rows[e.RowIndex].Cells["KAISHI_ZAIKO_RYOU"].Value);
                    string strKAISHI_ZAIKO_KINGAKU = Convert.ToString(this.form.Ichiran.Rows[e.RowIndex].Cells["KAISHI_ZAIKO_KINGAKU"].Value);
                    if (!string.IsNullOrEmpty(strKAISHI_ZAIKO_RYOU) && !string.IsNullOrEmpty(strKAISHI_ZAIKO_KINGAKU))
                    {
                        decimal.TryParse(strKAISHI_ZAIKO_RYOU, out KAISHI_ZAIKO_RYOU);
                        decimal.TryParse(strKAISHI_ZAIKO_KINGAKU, out KAISHI_ZAIKO_KINGAKU);
                        if (KAISHI_ZAIKO_RYOU != 0)
                        {
                            KAISHI_ZAIKO_TANKA = KAISHI_ZAIKO_KINGAKU / KAISHI_ZAIKO_RYOU;
                        }
                        else
                        {
                            KAISHI_ZAIKO_TANKA = 0;
                        }
                        this.form.Ichiran.Rows[e.RowIndex].Cells["KAISHI_ZAIKO_TANKA"].Value = KAISHI_ZAIKO_TANKA;
                    }
                    else
                    {
                        this.form.Ichiran.Rows[e.RowIndex].Cells["KAISHI_ZAIKO_TANKA"].Value = DBNull.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("IchiranCellValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        /// <summary>
        /// 業者チェック
        /// </summary>
        internal bool CheckGyousha()
        {
            LogUtility.DebugMethodStart();

            var msgLogic = new MessageBoxShowLogic();
            var inputGyoushaCd = this.form.GYOUSHA_CD.Text;

            var prevGyoushaCd = prevGyousha == null ? string.Empty : prevGyousha.GYOUSHA_CD;
            var prevGyoushaName = prevGyousha == null ? string.Empty : prevGyousha.GYOUSHA_NAME_RYAKU;
            if (!String.IsNullOrEmpty(inputGyoushaCd))
            {
                if (!prevGyoushaCd.Equals(inputGyoushaCd))
                {
                    if (!string.IsNullOrEmpty(prevGyoushaCd) && IsChangeIchiran())
                    {
                        if (msgLogic.MessageBoxShow("C088") != DialogResult.Yes)
                        {
                            this.form.GYOUSHA_CD.Text = prevGyoushaCd;
                            this.form.GYOUSHA_CD.prevText = prevGyoushaCd;
                            this.form.GYOUSHA_NAME_RYAKU.Text = prevGyoushaName;

                            this.form.GYOUSHA_CD.Focus();
                            return true;
                        }
                    }

                    this.form.GYOUSHA_NAME_RYAKU.Text = String.Empty;
                    this.form.GENBA_CD.Text = String.Empty;
                    this.form.GENBA_NAME_RYAKU.Text = String.Empty;

                    this.prevGenba = null;

                    var gyoushaEntity = this.gyoushaDao.GetAllValidData(new M_GYOUSHA() { GYOUSHA_CD = inputGyoushaCd, JISHA_KBN = true }).FirstOrDefault();
                    if (null == gyoushaEntity)
                    {
                        // エラーメッセージ
                        this.form.GYOUSHA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E020", "業者");
                        this.CreateNoDataRecord();
                        this.prevGyousha = null;

                        this.form.GYOUSHA_CD.Focus();
                        return false;
                    }
                    else
                    {
                        this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        this.prevGyousha = gyoushaEntity;
                        this.CreateNoDataRecord();
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(prevGyoushaCd) && IsChangeIchiran())
                {
                    if (msgLogic.MessageBoxShow("C088") != DialogResult.Yes)
                    {
                        this.form.GYOUSHA_CD.Text = prevGyoushaCd;
                        this.form.GYOUSHA_CD.prevText = prevGyoushaCd;
                        this.form.GYOUSHA_NAME_RYAKU.Text = prevGyoushaName;
                        this.form.GYOUSHA_CD.Focus();
                        return true;
                    }
                }
                this.form.GYOUSHA_NAME_RYAKU.Text = String.Empty;
                this.form.GENBA_CD.Text = String.Empty;
                this.form.GENBA_NAME_RYAKU.Text = String.Empty;
                this.prevGyousha = null;
                this.prevGenba = null;
                this.CreateNoDataRecord();
            }
            LogUtility.DebugMethodEnd();

            return true;
        }

        /// <summary>
        /// 現場チェック
        /// </summary>
        internal bool CheckGenba()
        {
            LogUtility.DebugMethodStart();

            var msgLogic = new MessageBoxShowLogic();
            var inputGyoushaCd = this.form.GYOUSHA_CD.Text;
            var inputGenbaCd = this.form.GENBA_CD.Text;
            var prevGenbaCd = prevGenba == null ? string.Empty : prevGenba.GENBA_CD;
            var prevGenbaName = prevGenba == null ? string.Empty : prevGenba.GENBA_NAME_RYAKU;

            // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
            if (!String.IsNullOrEmpty(inputGenbaCd))
            {
                if (!prevGenbaCd.Equals(inputGenbaCd))
                {
                    if (!string.IsNullOrEmpty(prevGenbaCd) && IsChangeIchiran())
                    {
                        if (msgLogic.MessageBoxShow("C088") != DialogResult.Yes)
                        {
                            this.form.GENBA_CD.Text = prevGenbaCd;
                            this.form.GENBA_CD.prevText = prevGenbaCd;
                            this.form.GENBA_NAME_RYAKU.Text = prevGenbaName;
                            this.form.GENBA_CD.Focus();

                            return true;
                        }
                    }

                    this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                    if (string.IsNullOrEmpty(inputGyoushaCd))
                    {
                        this.form.GENBA_CD.IsInputErrorOccured = true;
                        // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                        msgLogic.MessageBoxShow("E051", "業者");
                        // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
                        this.CreateNoDataRecord();
                        this.prevGenba = null;
                        this.form.GENBA_CD.Text = string.Empty;
                        this.form.GENBA_CD.Focus();
                        return false;
                    }

                    var genbaEntity = this.genbaDao.GetAllValidData(new M_GENBA() { GYOUSHA_CD = inputGyoushaCd, GENBA_CD = inputGenbaCd, JISHA_KBN = true }).FirstOrDefault();
                    if (genbaEntity == null)
                    {
                        // エラーメッセージ
                        this.form.GENBA_CD.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E020", "現場");
                        this.CreateNoDataRecord();
                        this.prevGenba = null;
                        this.form.GENBA_CD.Focus();
                        return false;
                    }
                    else
                    {
                        this.form.GENBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
                        this.prevGenba = genbaEntity;
                        this.CreateNoDataRecord();
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(prevGenbaCd) && IsChangeIchiran())
                {
                    if (msgLogic.MessageBoxShow("C088") != DialogResult.Yes)
                    {
                        this.form.GENBA_CD.Text = prevGenbaCd;
                        this.form.GENBA_CD.prevText = prevGenbaCd;
                        this.form.GENBA_NAME_RYAKU.Text = prevGenbaName;
                        this.form.GENBA_CD.Focus();

                        return true;
                    }
                }
                this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                this.prevGenba = null;
                this.CreateNoDataRecord();
            }

            LogUtility.DebugMethodEnd();

            return true;
        }

        public bool IsChangeIchiran()
        {
            DataTable dtChange = this.form.Ichiran.DataSource as DataTable;
            if (dtChange == null || dtChange.Rows.Count == 0)
            {
                return false;
            }
            dtChange.BeginLoadData();

            //変更したデータを取得
            if (dtChange.GetChanges() == null)
            {
                return false;
            }
            else
            {
                dtChange = dtChange.GetChanges();
            }
            bool flagChange = false;
            foreach (DataRow dtRow in dtChange.Rows)
            {
                if (dtRow["ZAIKO_HINMEI_CD"] != null && !DBNull.Value.Equals(dtRow["ZAIKO_HINMEI_CD"]) && !String.IsNullOrEmpty(dtRow["ZAIKO_HINMEI_CD"].ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 品名読込み時の既存データ削除用
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateDeleteEntity()
        {
            bool ret = true;
            try
            {
                List<M_KAISHI_ZAIKO_INFO> entitys = new List<M_KAISHI_ZAIKO_INFO>();

                DataTable dtIchiran = this.form.Ichiran.DataSource as DataTable;
                if (dtIchiran == null || dtIchiran.Rows.Count == 0)
                {
                    deleteEntitys = new M_KAISHI_ZAIKO_INFO[0];
                }

                ColumnAllowDBNull(dtIchiran);
                dtIchiran.BeginLoadData();

                // 変更分のみ取得
                DataTable addList = new DataTable();
                addList = dtIchiran.Copy();

                entitys = CreateEntityData(addList);

                foreach (var kaishiZaikoInfo in entitys)
                {
                    kaishiZaikoInfo.DELETE_FLG = true;
                }

                //登録時まで保持
                this.deleteEntitys = entitys.ToArray();
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateDeleteEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 基本品名マスタから一覧を取得して、一覧にセット
        /// </summary>
        /// <param name="e"></param>
        public virtual void LoadingZaikoHinmeiListToIchiran()
        {
            try
            {
                var zaikoHinmeiList = zaikoHinmeiDao.GetAllValidData(new M_ZAIKO_HINMEI());
                zaikoHinmeiList = zaikoHinmeiList.OrderBy(zaikoHinmei => zaikoHinmei.ZAIKO_HINMEI_CD).ToArray();
                if (zaikoHinmeiList.Length <= 0)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "在庫品名");
                    return;
                }

                foreach (M_ZAIKO_HINMEI zaikoHinmei in zaikoHinmeiList)
                {
                    DataRow newRow = this.dtSearchResult.NewRow();

                    newRow["ZAIKO_HINMEI_CD"] = zaikoHinmei.ZAIKO_HINMEI_CD;
                    newRow["ZAIKO_HINMEI_NAME_RYAKU"] = zaikoHinmei.ZAIKO_HINMEI_NAME_RYAKU;
                    newRow["GYOUSHA_CD"] = this.form.GYOUSHA_CD.Text;
                    newRow["GENBA_CD"] = this.form.GENBA_CD.Text;

                    this.dtSearchResult.Rows.Add(newRow);
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("LoadingZaikoHinmeiListToIchiran", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("LoadingZaikoHinmeiListToIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 論理削除　品名読込用
        /// </summary>
        [Transaction]
        public virtual void LogicalDeleteForZaikoHinmei()
        {
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            // トランザクション開始
            using (var tran = new Transaction())
            {
                //削除処理
                foreach (M_KAISHI_ZAIKO_INFO kaishiZaikoInfo in this.deleteEntitys)
                {
                    M_KAISHI_ZAIKO_INFO entity = this.kaishiZaikoInfoDao.GetDataByCd(kaishiZaikoInfo.GYOUSHA_CD, kaishiZaikoInfo.GENBA_CD, kaishiZaikoInfo.ZAIKO_HINMEI_CD);
                    if (entity != null)
                    {
                        entity.DELETE_FLG = true;
                        this.kaishiZaikoInfoDao.Update(entity);
                    }
                }

                // トランザクション終了
                tran.Commit();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 品名ロード終了
        /// </summary>
        public void ResetZaikoHinmeiLoad()
        {
            this.isNowLoadingZaikoHinmeiMaster = false;//リセット
        }

        /// <summary>
        /// 品名読込モード時
        /// 削除Flgのついている物を一覧から消す
        /// </summary>
        public bool DeleteForZaikoHinmeiLoading()
        {
            bool ret = true;
            try
            {
                var dtIchiran = this.form.Ichiran.DataSource as DataTable;

                for (int i = dtIchiran.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow row = dtIchiran.Rows[i];

                    if (row["DELETE_FLG"].ToString() == "True")
                    {
                        row.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DeleteForZaikoHinmeiLoading", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 主キーが同一の行がDBに存在する場合、主キーを非活性にする
        /// </summary>
        internal bool EditableToPrimaryKey()
        {
            bool ret = true;
            try
            {
                // DBから主キーのListを取得
                var allEntityList = this.kaishiZaikoInfoDao.GetAllData().Select(s => s.ZAIKO_HINMEI_CD).Where(s => !string.IsNullOrEmpty(s)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Cast<DataGridViewRow>().Select(r => r.Cells["ZAIKO_HINMEI_CD"]).Where(c => c.Value != null).ToList().
                                        ForEach(c => c.ReadOnly = allEntityList.Contains(c.Value.ToString()));
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("EditableToPrimaryKey", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditableToPrimaryKey", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }
    }
}