using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
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
using Shougun.Core.Master.DenshiShinseiJyuyoudoHoshu.APP;
using Shougun.Core.Master.DenshiShinseiJyuyoudoHoshu.Const;
using Shougun.Core.Master.DenshiShinseiJyuyoudoHoshu.Dao;

namespace Shougun.Core.Master.DenshiShinseiJyuyoudoHoshu.Logic
{
    /// <summary>
    /// 重要度入力画面のビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.DenshiShinseiJyuyoudoHoshu.Setting.ButtonSetting.xml";

        private bool isSelect = false;

        /// <summary>
        ///  重要度入力画面Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        public MasterBaseForm parentForm;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        ///  重要度入力のエンティティ
        /// </summary>
        private M_DENSHI_SHINSEI_JYUYOUDO[] entitys;

        /// <summary>
        ///  重要度入力のDao
        /// </summary>
        private DaoCls dao;

        /// <summary>
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

        /// <summary>
        /// チェックするか判断するフラグ
        /// </summary>
        internal bool isCheck = true;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_DENSHI_SHINSEI_JYUYOUDO SearchString { get; set; }

        public bool isRegist { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            // メッセージ出力用のユーティリティ
            MessageUtil = new MessageUtility();
            this.dao = DaoInitUtility.GetComponent<DaoCls>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            LogUtility.DebugMethodEnd(targetForm);
        }

        # endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 親フォームオブジェクト取得
                parentForm = (MasterBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;

                // 処理No（ESC)を入力不可にする
                this.parentForm.txb_process.Enabled = false;

                this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_VALUE.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;

                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    // システム情報を取得し、初期値をセットする
                    if (!this.GetSysInfoInit())
                    {
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                }

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M559", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.form.msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion

        #region ボタン初期化処理

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (MasterBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
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

        #endregion

        #region ボタン設定の読込

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

        #endregion

        #region イベントの初期化処理

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (MasterBaseForm)this.form.Parent;

                // 削除ボタン(F4)イベント生成
                this.form.C_MasterRegist(parentForm.bt_func4);
                parentForm.bt_func4.Click += new EventHandler(this.form.LogicalDelete);
                parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

                //CSV出力ボタン(F6)イベント生成
                parentForm.bt_func6.Click += new EventHandler(this.form.CSVOutput);

                //条件クリアボタン(F7)イベント生成
                parentForm.bt_func7.Click += new EventHandler(this.form.ClearCondition);

                //検索ボタン(F8)イベント生成
                parentForm.bt_func8.Click += new EventHandler(this.form.Search);

                //登録ボタン(F9)イベント生成
                this.form.C_MasterRegist(parentForm.bt_func9);
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                //取消ボタン(F11)イベント生成
                parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
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

            // FunctionButton
            var parentForm = (MasterBaseForm)this.form.Parent;
            parentForm.bt_func4.Enabled = false;
            parentForm.bt_func6.Enabled = true;
            parentForm.bt_func9.Enabled = false;
        }

        #endregion

        #region 検索条件初期化

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        public void ClearCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.CONDITION_VALUE.Text = string.Empty;
                this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
                this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
                this.form.CONDITION_ITEM.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearCondition", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 検索条件チェック

        /// <summary>
        /// 検索文字列が検索項目に対して不正な文字かのチェックを行う
        /// </summary>
        /// <returns>True：正常　False：不正</returns>
        public bool CheckSearchString()
        {
            // SetSearchStringメソッド中のentity.SetValueで値によってはFormatでシステムエラーになるためチェックを行う。
            // 現在は、数値項目のみエラーが発生するため、該当項目のみチェックを行っている。
            // 汎用的に行うのであればSetValueで扱っている全ての型に対してチェックを行う。

            LogUtility.DebugMethodStart();

            bool retVal = true;

            if (string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName) || string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
            {
                return retVal;
            }

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text))
            {
                /* チェック実行 */
                if (this.form.CONDITION_VALUE.ItemDefinedTypes.ToLower() == DB_TYPE.SMALLINT.ToTypeString())
                {
                    short dummy = 0;
                    retVal = short.TryParse(this.form.CONDITION_VALUE.Text, out dummy);
                }
            }

            LogUtility.DebugMethodEnd(retVal);
            return retVal;
        }

        #endregion

        #region 検索条件の設定

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        public bool SetSearchString()
        {
            try
            {
                LogUtility.DebugMethodStart();

                M_DENSHI_SHINSEI_JYUYOUDO entity = new M_DENSHI_SHINSEI_JYUYOUDO();

                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
                {
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                    {
                        //検索条件の設定
                        entity.SetValue(this.form.CONDITION_VALUE);
                    }
                }

                this.SearchString = entity;
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

        #endregion

        #region システム情報を取得し、初期値をセットする

        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public bool GetSysInfoInit()
        {
            bool ret = true;
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
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetSysInfoInit", ex1);
                this.form.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSysInfoInit", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region 更新処理

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 論理削除処理

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            LogUtility.DebugMethodStart();
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            try
            {
                this.isRegist = true;
                if (!isSelect)
                {
                    msgLogic.MessageBoxShow("E075", "削除");
                }
                else
                {
                    using (Transaction tran = new Transaction())
                    {
                        var result = msgLogic.MessageBoxShow("C021");
                        if (result == DialogResult.Yes)
                        {
                            if (this.entitys != null)
                            {
                                foreach (M_DENSHI_SHINSEI_JYUYOUDO jyuyoudoEntity in this.entitys)
                                {
                                    M_DENSHI_SHINSEI_JYUYOUDO entity = this.dao.GetDataByCd(jyuyoudoEntity.JYUYOUDO_CD.ToString());
                                    if (entity != null)
                                    {
                                        this.dao.Update(jyuyoudoEntity);
                                    }
                                }
                            }
                            tran.Commit();
                            msgLogic.MessageBoxShow("I001", "削除");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //例外はここで処理
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    // 排他エラーの場合
                    //LogUtility.Error(ConstCls.ExceptionErrMsg.HAITA, ex);
                    LogUtility.Error("LogicalDelete", ex);
                    this.form.msgLogic.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    //LogUtility.Error(ConstCls.ExceptionErrMsg.REIGAI, ex);
                    LogUtility.Error("LogicalDelete", ex);
                    this.form.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    LogUtility.Error("LogicalDelete", ex);
                    this.form.msgLogic.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 物理削除処理

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region CSV出力

        /// <summary>
        /// CSV出力
        /// </summary>
        public bool CSVOutput()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_DENSHI_SHINSEI_JYUYOUDO), this.form);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVOutput", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region データ取得処理

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int count = 0;
            try
            {
                LogUtility.DebugMethodStart();

                // エラーの場合、０件を戻る
                if (!SetSearchString())
                {
                    return 0;
                }

                this.SearchResult = dao.GetIchiranDataSql(this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

                Properties.Settings.Default.Save();

                if (this.SearchResult.Rows != null && this.SearchResult.Rows.Count > 0)
                {
                    count = 1;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.form.msgLogic.MessageBoxShow("E093", "");
                count = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                count = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }
            return count;
        }

        #endregion

        #region 登録処理

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual bool RegistData(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    using (Transaction tran = new Transaction())
                    {
                        if (this.entitys != null)
                        {
                            // トランザクション開始
                            foreach (M_DENSHI_SHINSEI_JYUYOUDO jyuyoudoEntity in this.entitys)
                            {
                                M_DENSHI_SHINSEI_JYUYOUDO entity = this.dao.GetDataByCd(jyuyoudoEntity.JYUYOUDO_CD.ToString());
                                if (entity == null)
                                {
                                    this.dao.Insert(jyuyoudoEntity);
                                }
                                else
                                {
                                    this.dao.Update(jyuyoudoEntity);
                                }
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
                return false;
            }

            LogUtility.DebugMethodEnd(errorFlag);
            return true;
        }

        #endregion

        #region 取消処理

        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                if (!GetSysInfoInit())
                {
                    ret = false;
                    return ret;
                }

                // 検索項目を初期値にセットする
                this.form.CONDITION_VALUE.Text = "";
                this.form.CONDITION_VALUE.DBFieldsName = "";
                this.form.CONDITION_VALUE.ItemDefinedTypes = "";
                this.form.CONDITION_ITEM.Text = "";
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Cancel", ex1);
                this.form.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region 重要度CDの重複チェック

        /// <summary>
        /// 重要度CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck(string jyuyoudo_Cd, DataTable dtDetailList, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(jyuyoudo_Cd, dtDetailList);

                // 画面で申請経路CD重複チェック
                int rowCount = 0;
                int currentRowIndex = this.form.Ichiran.CurrentRow.Index;
                foreach (DataRow row in this.SearchResult.Rows)
                {
                    if (row[ConstCls.JYUYOUDO_CD].ToString().Equals(Convert.ToString(jyuyoudo_Cd)))
                    {
                        rowCount++;
                    }
                }

                if (rowCount > 1)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }

                // 検索結果でPK重複チェック
                for (int i = 0; i < dtDetailList.Rows.Count; i++)
                {
                    if (jyuyoudo_Cd.Equals(dtDetailList.Rows[i][ConstCls.JYUYOUDO_CD].ToString()))
                    {
                        return false;
                    }
                }

                // DBで重要度CD重複チェック
                M_DENSHI_SHINSEI_JYUYOUDO entity = this.dao.GetDataByCd(jyuyoudo_Cd);
                if (entity != null)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(jyuyoudo_Cd, dtDetailList, catchErr);
            }
            return false;
        }

        #endregion

        /// <summary>
        /// 申請初期値の一意チェック
        /// </summary>
        /// <param name="DtDetailList"></param>
        /// <returns></returns>
        public int UniqueCheck()
        {
            int checkCount = 0;
            try
            {
                LogUtility.DebugMethodStart();

                foreach (DataGridViewRow row in this.form.Ichiran.Rows)
                {
                    if (row.Cells[ConstCls.JYUYOUDO_DEFAULT].Value != null)
                    {
                        // カレントROW以外の申請初期値にtrueがあった場合
                        if (row.Cells[ConstCls.JYUYOUDO_DEFAULT].Value.Equals(true))
                        {
                            checkCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UniqueCheck", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                checkCount = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(checkCount);
            }
            return checkCount;
        }

        #region コントロールから対象のEntityを作成する

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(isDelete);

                var entityList = new M_DENSHI_SHINSEI_JYUYOUDO[this.form.Ichiran.Rows.Count - 1];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_DENSHI_SHINSEI_JYUYOUDO();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_DENSHI_SHINSEI_JYUYOUDO>(entityList);
                DataTable dt = this.form.Ichiran.DataSource as DataTable;

                if (dt == null || dt.Rows.Count == 0)
                {
                    return false;
                }

                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(ConstCls.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                int count = 0;
                List<M_DENSHI_SHINSEI_JYUYOUDO> mJyuyoudoList = new List<M_DENSHI_SHINSEI_JYUYOUDO>();
                if (isDelete)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() != "" && this.form.Ichiran.Rows[i].Cells["chb_delete"].Value != null && (bool)this.form.Ichiran.Rows[i].Cells["chb_delete"].Value)
                        {
                            if (string.IsNullOrEmpty(dt.Rows[i]["CREATE_USER"].ToString())
                                && (this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value == null
                                || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value.ToString())))
                            {
                                isSelect = true;
                                count++;
                                continue;
                            }

                            isSelect = true;
                            var m_JyuyoudoEntity = CreateEntityForDataGridRow(dt.Rows[i]);
                            var dataBinderEntry = new DataBinderLogic<M_DENSHI_SHINSEI_JYUYOUDO>(m_JyuyoudoEntity);
                            dataBinderEntry.SetSystemProperty(m_JyuyoudoEntity, true);
                            m_JyuyoudoEntity.DELETE_FLG = true;
                            mJyuyoudoList.Add(m_JyuyoudoEntity);
                        }
                    }
                }

                if (isDelete)
                {
                    this.entitys = mJyuyoudoList.ToArray();
                    if (this.entitys.Length == 0 && count == 0)
                    {
                        return false;
                    }
                }
                else
                {
                    // 変更分のみ取得
                    List<M_DENSHI_SHINSEI_JYUYOUDO> addList = new List<M_DENSHI_SHINSEI_JYUYOUDO>();
                    if (dt.GetChanges() == null)
                    {
                        this.entitys = new List<M_DENSHI_SHINSEI_JYUYOUDO>().ToArray();
                        return false;
                    }

                    // 元の値から全く変化がなければ、 RowState を元の状態に戻す
                    foreach (DataRow row in dt.Rows)
                    {
                        if (!DataTableUtility.IsDataRowChanged(row))
                        {
                            row.AcceptChanges();
                        }
                    }

                    if (dt.GetChanges() == null)
                    {
                        return true;
                    }

                    // 変更したデータ取得
                    var rows = dt.GetChanges().Select("DELETE_FLG = 0 and JYUYOUDO_CD IS NOT NULL");

                    // データ変更なし
                    if (rows.Length == 0)
                    {
                        this.entitys = new List<M_DENSHI_SHINSEI_JYUYOUDO>().ToArray();
                        return false;
                    }

                    var jyuyoudoEntityList = CreateEntityForDataGrid(rows);
                    for (int i = 0; i < jyuyoudoEntityList.Count; i++)
                    {
                        var jyuyoudoEntity = jyuyoudoEntityList[i];
                        for (int j = 0; j < this.form.Ichiran.Rows.Count - 1; j++)
                        {
                            bool isFind = false;
                            if (this.form.Ichiran.Rows[j].Cells[ConstCls.JYUYOUDO_CD].Value.ToString().PadLeft(2, '0').Equals(Convert.ToString(jyuyoudoEntity.JYUYOUDO_CD)) &&
                                     bool.Parse(this.form.Ichiran.Rows[j].Cells[ConstCls.DELETE_FLG].FormattedValue.ToString()) == isDelete)
                            {
                                isFind = true;
                            }

                            if (isFind)
                            {
                                dataBinderLogic.SetSystemProperty(jyuyoudoEntity, false);
                                var dataBinderEntry = new DataBinderLogic<M_DENSHI_SHINSEI_JYUYOUDO>(jyuyoudoEntity);
                                dataBinderEntry.SetSystemProperty(jyuyoudoEntity, false);
                                addList.Add(jyuyoudoEntity);
                                break;
                            }
                        }
                        if (addList.Count > 0)
                        {
                            this.entitys = addList.ToArray();
                        }
                        else
                        {
                            this.entitys = new List<M_DENSHI_SHINSEI_JYUYOUDO>().ToArray();
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                return true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isDelete, catchErr);
            }
        }

        #endregion

        #region CreateEntityForDataGrid

        /// <summary>
        /// CreateEntityForDataGrid
        /// </summary>
        /// <returns>
        /// entityList
        /// </returns>
        internal List<M_DENSHI_SHINSEI_JYUYOUDO> CreateEntityForDataGrid(IEnumerable<DataRow> rows)
        {
            try
            {
                LogUtility.DebugMethodStart(rows);

                var entityList = rows.Select(r => CreateEntityForDataGridRow(r)).ToList();

                return entityList;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityForDataGrid", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(rows);
            }
        }

        #endregion

        #region CreateEntityForDataGridRow

        /// <summary>
        /// CreateEntityForDataGridRow
        /// </summary>
        /// <returns>
        /// mJyuyoudoShurui
        /// </returns>
        internal M_DENSHI_SHINSEI_JYUYOUDO CreateEntityForDataGridRow(DataRow row)
        {
            try
            {
                LogUtility.DebugMethodStart(row);

                M_DENSHI_SHINSEI_JYUYOUDO mJyuyoudo = new M_DENSHI_SHINSEI_JYUYOUDO();

                // JYUYOUDO_CD
                if (!DBNull.Value.Equals(row.Field<string>("JYUYOUDO_CD")))
                {
                    mJyuyoudo.JYUYOUDO_CD = row.Field<string>("JYUYOUDO_CD").PadLeft(2, '0');
                }

                // JYUYOUDO_NAME
                if (!DBNull.Value.Equals(row.Field<string>("JYUYOUDO_NAME")))
                {
                    mJyuyoudo.JYUYOUDO_NAME = row.Field<string>("JYUYOUDO_NAME");
                }

                // JYUYOUDO_DEFAULT
                if (!string.IsNullOrEmpty(row["JYUYOUDO_DEFAULT"].ToString()))
                {
                    mJyuyoudo.JYUYOUDO_DEFAULT = row.Field<bool>("JYUYOUDO_DEFAULT");
                }
                else
                {
                    mJyuyoudo.JYUYOUDO_DEFAULT = false;
                }

                // DELETE_FLG
                if (!DBNull.Value.Equals(row.Field<bool>("DELETE_FLG")))
                {
                    mJyuyoudo.DELETE_FLG = row.Field<bool>("DELETE_FLG");
                }
                else
                {
                    mJyuyoudo.DELETE_FLG = false;
                }

                // TIME_STAMP
                if (!DBNull.Value.Equals(row.Field<byte[]>("TIME_STAMP")))
                {
                    mJyuyoudo.TIME_STAMP = row.Field<byte[]>("TIME_STAMP");
                }
                return mJyuyoudo;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityForDataGridRow", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(row);
            }
        }

        #endregion

        #region DataGridViewデータ件数チェック処理

        /// <summary>
        /// DataGridViewデータ件数チェック処理
        /// </summary>
        public bool ActionBeforeCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.Ichiran.Rows.Count > 1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ActionBeforeCheck", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region NOT NULL制約を一時的に解除

        /// <summary>
        /// NOT NULL制約を一時的に解除
        /// </summary>
        public void ColumnAllowDBNull()
        {
            try
            {
                LogUtility.DebugMethodStart();

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(ConstCls.TIME_STAMP))
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

        #region レコード選択チェック処理

        /// <summary>
        /// レコード選択チェック処理
        /// </summary>
        public bool isSelectFlg()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!isSelect)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("isSelectFlg", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        public virtual void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 主キーが同一の行がDBに存在する場合、主キーを非活性にする
        /// </summary>
        internal bool EditableToPrimaryKey()
        {
            try
            {
                // DBから主キーのListを取得
                var allEntityList = this.dao.GetAllData().Select(s => s.JYUYOUDO_CD).Where(s => !s.Equals(DBNull.Value)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Cast<DataGridViewRow>().Select(r => r.Cells["JYUYOUDO_CD"]).Where(c => !string.IsNullOrEmpty(Convert.ToString(c.Value))).ToList().
                                        ForEach(c => c.ReadOnly = allEntityList.Contains(c.Value));
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("EditableToPrimaryKey", ex1);
                this.form.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditableToPrimaryKey", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }
    }
}