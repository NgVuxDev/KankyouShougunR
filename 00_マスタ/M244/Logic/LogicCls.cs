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
using Shougun.Core.Master.ZaikoHinmeiHoshu.APP;
using Shougun.Core.Master.ZaikoHinmeiHoshu.DAO;
using Shougun.Core.Master.ZaikoHinmeiHoshu.DTO;

namespace Shougun.Core.Master.ZaikoHinmeiHoshu.Logic
{
    /// <summary>
    /// 在庫品名画面のビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.ZaikoHinmeiHoshu.Setting.ButtonSetting.xml";

        /// <summary>
        /// 在庫品名画面Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        public MasterBaseForm parentForm;

        /// <summary>
        /// 在庫品名Dao
        /// </summary>
        private IMZAIKOHINMEIDao mZaikoHinmeiDao;

        /// <summary>
        /// 在庫比率Dao
        /// </summary>
        private IMZAIKOHIRITSUDao mZaikoHiritsuDao;

        /// システム情報Dao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        ///単位マスタDao
        /// </summary>
        private IM_UNITDao mUnitdao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfo;

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        public DataTable bakSearchResult = null;

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// 在庫品名更新エンティティ
        /// </summary>
        public M_ZAIKO_HINMEI[] entities;

        /// <summary>
        /// 登録・更新・削除処理の成否
        /// </summary>
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
            //在庫品名Dao
            this.mZaikoHinmeiDao = DaoInitUtility.GetComponent<IMZAIKOHINMEIDao>();
            this.mZaikoHiritsuDao = DaoInitUtility.GetComponent<IMZAIKOHIRITSUDao>();
            // システム情報のDao
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.mUnitdao = DaoInitUtility.GetComponent<IM_UNITDao>();
            // DTO
            this.dto = new DTOClass();
            LogUtility.DebugMethodEnd();
        }

        #endregion

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
                this.parentForm = (MasterBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();
                // 検索条件初期化
                this.ClearCondition();

                //表示条件初期値設定処理
                // システム情報を取得し、初期値をセットする
                if (!this.GetSysInfoInit())
                {
                    return false;
                }

                //前の表示条件を初期値に設定する
                if (Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED)
                {
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                }

                this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.CONDITION_DBFIELD.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_DBFIELD.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M244", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return true;
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

            // FunctionButton
            this.parentForm.bt_func4.Enabled = false;
            this.parentForm.bt_func6.Enabled = true;
            this.parentForm.bt_func9.Enabled = false;
        }

        #endregion

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
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

            //閉じるボタン(F12)イベント生成
            this.parentForm.bt_func12.Click -= this.form.FormClose;
            this.parentForm.bt_func12.Click += this.form.FormClose;
            //this.parentForm.bt_func12.CausesValidation = false;

            // 処理No(ESC)を入力不可にする
            this.parentForm.txb_process.Enabled = false;
        }

        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public bool GetSysInfoInit()
        {
            try
            {
                // システム情報を取得し、初期値をセットする
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.sysInfo = sysInfo[0];
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = (bool)this.sysInfo.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetSysInfoInit", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSysInfoInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        public void ClearCondition()
        {
            this.form.CONDITION_VALUE.Text = string.Empty;
            this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
            this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
            this.form.CONDITION_ITEM.Text = string.Empty;
            this.form.CONDITION_DBFIELD.Text = string.Empty;
            this.form.CONDITION_DBFIELD.DBFieldsName = string.Empty;
            this.form.CONDITION_TYPE.Text = string.Empty;
        }

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetIchiran()
        {
            bool ret = true;
            try
            {
                var searchResult = this.SearchResult;
                searchResult.BeginLoadData();
                for (int i = 0; i < searchResult.Columns.Count; i++)
                {
                    searchResult.Columns[i].ReadOnly = false;
                    searchResult.Columns[i].AllowDBNull = true;
                }

                this.form.Ichiran.CellValidating -= this.form.Ichiran_CellValidating;
                //this.form.Ichiran.DataSource = null; // リロード
                this.form.Ichiran.DataSource = searchResult;
                this.form.Ichiran.CellValidating += this.form.Ichiran_CellValidating;

                searchResult.EndLoadData();
                searchResult.AcceptChanges();

                this.form.CONDITION_ITEM.Focus();

                ColumnAllowDBNull();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        #endregion

        #region イベント処理

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

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
                using (Transaction tran = new Transaction())
                {
                    foreach (M_ZAIKO_HINMEI inEntity in this.entities)
                    {
                        M_ZAIKO_HINMEI chkEntity = this.mZaikoHinmeiDao.GetDataByCd(inEntity.ZAIKO_HINMEI_CD.ToString());
                        if (chkEntity != null)
                        {
                            chkEntity.DELETE_FLG = true;
                            this.mZaikoHinmeiDao.Update(inEntity);
                        }
                    }
                    tran.Commit();
                    msgLogic.MessageBoxShow("I001", "削除");
                }
                this.isRegist = true;
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
                    LogUtility.Error(ex); //その他はエラー
                    this.form.errmessage.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            if (this.isRegist)
            {
                this.form.Search(null, null);
                this.SetIchiran();
            }
        }

        /// <summary>
        /// 削除できるかどうかチェックする
        /// </summary>
        public bool CheckDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                var zaikoHinmeiCd = string.Empty;
                string[] strList;

                foreach (DataGridViewRow row in this.form.Ichiran.Rows)
                {
                    if (row.Cells["DELETE_FLG"].Value != null && row.Cells["DELETE_FLG"].Value.ToString().Equals("True"))
                    {
                        zaikoHinmeiCd += row.Cells["ZAIKO_HINMEI_CD"].Value.ToString() + ",";
                    }
                }

                if (!string.IsNullOrEmpty(zaikoHinmeiCd))
                {
                    zaikoHinmeiCd = zaikoHinmeiCd.Substring(0, zaikoHinmeiCd.Length - 1);
                    strList = zaikoHinmeiCd.Split(',');
                    DataTable dtTable = this.mZaikoHinmeiDao.GetDataBySqlFileCheck(strList);
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        string strName = string.Empty;

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            strName += "\n" + dr["NAME"].ToString();
                        }

                        new MessageBoxShowLogic().MessageBoxShow("E258", "在庫品名", "在庫品名CD", strName);

                        ret = false;
                    }
                    else
                    {
                        ret = true;
                    }
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

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
                    csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_ZAIKO_HINMEI), this.form);
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
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            int count = 0;
            try
            {
                dto.FieldName = this.form.CONDITION_DBFIELD.DBFieldsName;
                dto.ConditionValue = this.form.CONDITION_VALUE.Text;
                this.SearchResult = mZaikoHinmeiDao.GetIchiranData(
                    dto, //this.SearchString
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                this.bakSearchResult = this.SearchResult.Copy();

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_DBFIELD.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_DBFIELD.ItemDefinedTypes;

                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

                Properties.Settings.Default.Save();

                if (this.SearchResult.Rows != null)
                {
                    count = this.SearchResult.Rows.Count;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return -1;
            }

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart(errorFlag);
                this.isRegist = false;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (errorFlag)
                {
                    return;
                }

                //エラーではない場合登録処理を行う
                using (Transaction tran = new Transaction())
                {
                    // トランザクション開始
                    foreach (M_ZAIKO_HINMEI inEntity in this.entities)
                    {
                        M_ZAIKO_HINMEI chkEntity = this.mZaikoHinmeiDao.GetDataByCd(inEntity.ZAIKO_HINMEI_CD);
                        if (chkEntity == null)
                        {
                            this.mZaikoHinmeiDao.Insert(inEntity);
                        }
                        else
                        {
                            this.mZaikoHinmeiDao.Update(inEntity);
                        }
                    }

                    // トランザクション終了
                    tran.Commit();
                }
                this.isRegist = true;
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
                    LogUtility.Error(ex); //その他はエラー
                    this.form.errmessage.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region チェック

        /// <summary>
        /// 在庫品名CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool ZaikoHinmeiCdCheck(string zaikoHinmeiCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 画面で在庫品名CD重複チェック
                int recCount = 0;
                for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
                {
                    DataGridViewRow row = this.form.Ichiran.Rows[i];
                    string compareZaikoHinmeiCd = this.form.GetCellValue(row, "ZAIKO_HINMEI_CD").PadLeft(6, '0').ToUpper();
                    if (zaikoHinmeiCd.Equals(compareZaikoHinmeiCd))
                    {
                        recCount++;
                    }
                }
                // 2行以上(該当行以外)は重複存在とする
                if (recCount > 1)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }

                // 検索結果でCD重複チェック
                for (int i = 0; i < bakSearchResult.Rows.Count; i++)
                {
                    if (zaikoHinmeiCd.Equals(bakSearchResult.Rows[i][1]))
                    {
                        LogUtility.DebugMethodEnd(false, catchErr);
                        return false;
                    }
                }

                // DBで在庫品名CD重複チェック
                M_ZAIKO_HINMEI entity = this.mZaikoHinmeiDao.GetDataByCd(zaikoHinmeiCd);
                if (entity != null)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("IsHinmeiChkOK", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsHinmeiChkOK", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            LogUtility.DebugMethodEnd(false, catchErr);
            return false;
        }

        /// <summary>
        /// 登録前のチェック
        /// </summary>
        /// <returns></returns>
        public Boolean RegistCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var msgLogic = new MessageBoxShowLogic();

                // 必須入力チェック
                for (int i = 0; i < this.form.Ichiran.RowCount - 1; i++)
                {
                    DataGridViewRow row = this.form.Ichiran.Rows[i];
                    //削除時チェックない。
                    if (!string.IsNullOrEmpty(row.Cells["DELETE_FLG"].Value.ToString()) && (Boolean)row.Cells["DELETE_FLG"].Value)
                    {
                        continue;
                    }
                    // 在庫品名CD
                    if (string.IsNullOrEmpty(this.form.GetCellValue(row, "ZAIKO_HINMEI_CD")))
                    {
                        msgLogic.MessageBoxShow("E001", "在庫品名CD");
                        this.form.Ichiran.CurrentCell = row.Cells["ZAIKO_HINMEI_CD"];
                        this.form.ActiveControl = this.form.Ichiran;
                        return false;
                    }

                    // 在庫品名
                    if (string.IsNullOrEmpty(this.form.GetCellValue(row, "ZAIKO_HINMEI")))
                    {
                        msgLogic.MessageBoxShow("E001", "在庫品名");
                        this.form.Ichiran.CurrentCell = row.Cells["ZAIKO_HINMEI"];
                        this.form.ActiveControl = this.form.Ichiran;
                        return false;
                    }

                    // 略称
                    if (string.IsNullOrEmpty(this.form.GetCellValue(row, "ZAIKO_HINMEI_RYAKU")))
                    {
                        msgLogic.MessageBoxShow("E001", "略称");
                        this.form.Ichiran.CurrentCell = row.Cells["ZAIKO_HINMEI_RYAKU"];
                        this.form.ActiveControl = this.form.Ichiran;
                        return false;
                    }

                    // フリガナ
                    if (string.IsNullOrEmpty(this.form.GetCellValue(row, "ZAIKO_HINMEI_FURIGANA")))
                    {
                        msgLogic.MessageBoxShow("E001", "フリガナ");
                        this.form.Ichiran.CurrentCell = row.Cells["ZAIKO_HINMEI_FURIGANA"];
                        this.form.ActiveControl = this.form.Ichiran;
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistCheck", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// DataGridViewデータ件数チェック処理
        /// </summary>
        public bool ActionBeforeCheck()
        {
            if (this.form.Ichiran.Rows.Count > 1)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region データ取得

        /// <summary>
        ///
        /// </summary>
        /// <param name="isDeleteFlag"></param>
        /// <returns></returns>
        public int CreateEntity(bool isDeleteFlag)
        {
            LogUtility.DebugMethodStart();
            var grdData = this.form.Ichiran.DataSource as DataTable;
            var entityList = new List<M_ZAIKO_HINMEI>();

            try
            {
                for (int i = 0; i < grdData.Rows.Count; i++)
                {
                    var row = grdData.Rows[i];
                    // 20150422 該当行の変更ステータス判断(一覧不具合67) Start
                    if (row.RowState != DataRowState.Unchanged)
                    {
                        if (!isDeleteFlag)
                        {
                            //保存と更新データを取得
                            if (!string.IsNullOrEmpty(row["DELETE_FLG"].ToString()) && (Boolean)row["DELETE_FLG"])
                            {
                                continue;
                            }
                            if (string.IsNullOrEmpty(row["ZAIKO_HINMEI_CD"].ToString()))
                            {
                                continue;
                            }
                            entityList.Add(GetEntity(row, isDeleteFlag));
                        }
                        else
                        {
                            //削除データを取得
                            if (!string.IsNullOrEmpty(row["DELETE_FLG"].ToString()) && (Boolean)row["DELETE_FLG"])
                            {
                                entityList.Add(GetEntity(row, isDeleteFlag));
                            }
                        }
                    }
                    // 20150422 該当行の変更ステータス判断(一覧不具合67) End
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return -1;
            }

            this.entities = entityList.ToArray();

            LogUtility.DebugMethodEnd();
            return entities.Length;
        }

        /// <summary>
        /// データを取得
        /// </summary>
        /// <param name="displayRow"></param>
        /// <param name="isDeleteFlag"></param>
        /// <returns></returns>
        internal M_ZAIKO_HINMEI GetEntity(DataRow displayRow, bool isDeleteFlag)
        {
            M_ZAIKO_HINMEI entity = new M_ZAIKO_HINMEI();
            entity.ZAIKO_HINMEI_CD = displayRow["ZAIKO_HINMEI_CD"].ToString();
            entity.ZAIKO_HINMEI_NAME = displayRow["ZAIKO_HINMEI_NAME"].ToString();
            entity.ZAIKO_HINMEI_NAME_RYAKU = displayRow["ZAIKO_HINMEI_NAME_RYAKU"].ToString();
            entity.ZAIKO_HINMEI_FURIGANA = displayRow["ZAIKO_HINMEI_FURIGANA"].ToString();
            if (!string.IsNullOrEmpty(displayRow["ZAIKO_TANKA"].ToString()))
            {
                entity.ZAIKO_TANKA = Decimal.Parse(displayRow["ZAIKO_TANKA"].ToString());
            }
            entity.BIKOU = displayRow["BIKOU"].ToString();
            // DELETE_FLG
            entity.DELETE_FLG = isDeleteFlag;

            // TIME_STAMP
            if (!DBNull.Value.Equals(displayRow["TIME_STAMP"]))
            {
                entity.TIME_STAMP = (byte[])displayRow["TIME_STAMP"];
            }

            // 更新者情報設定
            var dbLogic = new DataBinderLogic<r_framework.Entity.M_ZAIKO_HINMEI>(entity);
            dbLogic.SetSystemProperty(entity, false);
            return entity;
        }

        /// <summary>
        /// NOT NULL制約を一時的に解除
        /// </summary>
        public void ColumnAllowDBNull()
        {
            DataTable table = this.form.Ichiran.DataSource as DataTable;
            //DataTable preDt = new DataTable();
            foreach (DataColumn column in table.Columns)
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

        /// <summary>
        /// 単価フォーマット
        /// </summary>
        /// <param name="num"></param>
        /// <param name="type">0：単価；　1：数量；　2：重量</param>
        /// <returns></returns>
        public string FormatSystemTanka(Decimal num, int type = 0)
        {
            string format = "#,##0";
            if (!string.IsNullOrWhiteSpace(this.sysInfo.SYS_TANKA_FORMAT))
            {
                if (type == 0)
                {
                    format = this.sysInfo.SYS_TANKA_FORMAT;
                }
                else if (type == 1)
                {
                    format = this.sysInfo.SYS_SUURYOU_FORMAT;
                }
                else if (type == 2)
                {
                    format = this.sysInfo.SYS_JYURYOU_FORMAT;
                }

                switch (this.sysInfo.SYS_TANKA_FORMAT_CD.Value)
                {
                    case 1:
                    case 2:
                        num = Math.Floor(num);
                        break;

                    case 3:
                        num = Math.Floor(num * 10) / 10;
                        break;

                    case 4:
                        num = Math.Floor(num * 100) / 100;
                        break;

                    case 5:
                        num = Math.Floor(num * 1000) / 1000;
                        break;
                }
            }
            return string.Format("{0:" + format + "}", num);
        }

        #endregion

        /// <summary>
        /// 一覧セル編集開始時イベント処理
        /// </summary>
        /// <param name="e"></param>
        public void IchiranCellEnter(DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            // 新規行の場合には削除チェックさせない
            this.form.Ichiran[0, e.RowIndex].ReadOnly = this.form.Ichiran.Rows[e.RowIndex].IsNewRow;
            LogUtility.DebugMethodEnd();
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
                var allEntityList = this.mZaikoHinmeiDao.GetAllData().Select(s => s.ZAIKO_HINMEI_CD).Where(s => !string.IsNullOrEmpty(s)).ToList();

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