// $Id: F00Logic.cs 37912 2014-12-22 07:16:00Z fangjk@oec-h.com $
using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Collections;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using r_framework.CustomControl;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Master.ContenaSousaHoshu
{
    /// <summary>
    /// コンテナ操作画面のビジネスロジック
    /// </summary>
    public class M224Logic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.ContenaSousaHoshu.Setting.ButtonSetting.xml";

        private bool isSelect = false;
        public bool isRegist = true;

        /// <summary>
        /// コンテナ操作画面Form
        /// </summary>
        private M224Form form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_CONTENA_SOUSA[] entitys;

        /// <summary>
        /// コンテナ操作のDao
        /// </summary>
        private M_ContenaSousaDao dao;
        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;
        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }
        /// <summary>
        /// 検索条件
        /// </summary>
        public M_CONTENA_SOUSA SearchString { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable dtDetailList = new DataTable();

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public M224Logic(M224Form targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<M_ContenaSousaDao>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            LogUtility.DebugMethodStart();
            try
            {
                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;
                //システム情報
                if (!this.GetSysInfo())
                {
                    ret = false;
                    return ret;
                }

                //前の表示条件を初期値に設定する
                if (Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED)
                {
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                }

                //プロパテ設定
                this.form.CONDITION_DBFIELD.Text = Properties.Settings.Default.CONDITION_DBFIELD;
                this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.CONDITION_DBFIELD.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_DBFIELD.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return true;
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (MasterBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

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

            var parentForm = (MasterBaseForm)this.form.Parent;

            // 削除ボタン(F4)イベント生成
            this.form.C_Regist(parentForm.bt_func4);
            parentForm.bt_func4.Click += new EventHandler(this.form.LogicalDelete);
            parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

            //CSV出力ボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.CSVOutput);

            //条件クリアボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.ClearCondition);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            //登録ボタン(F9)イベント生成
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //取消ボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            //[1]ボタン
            parentForm.bt_process1.Enabled = false;

            //[2]ボタン
            parentForm.bt_process2.Enabled = false;

            //[3]ボタン
            parentForm.bt_process3.Enabled = false;

            //[4]ボタン
            parentForm.bt_process4.Enabled = false;

            //[5]ボタン
            parentForm.bt_process5.Enabled = false;
            //ESC
            parentForm.txb_process.Enabled = false;
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public bool GetSysInfo()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.sysInfoEntity = sysInfo[0];
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = (bool)this.sysInfoEntity.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetSysInfo", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSysInfo", ex);
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
        /// DataTableのクローン処理
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetCloneDataTable(DataTable dt)
        {
            LogUtility.DebugMethodStart(dt);

            // dtのスキーマや制約をコピー
            DataTable table = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                DataRow addRow = table.NewRow();

                // カラム情報をコピー
                addRow.ItemArray = row.ItemArray;

                table.Rows.Add(addRow);
            }
            LogUtility.DebugMethodEnd(table);

            return table;
        }

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
            this.isRegist = true;
            try
            {
                if (!isSelect)
                {
                    msgLogic.MessageBoxShow("E075", "削除");
                }
                else
                {

                    var result = msgLogic.MessageBoxShow("C021");

                    if (result == DialogResult.Yes)
                    {
                        using (Transaction tran = new Transaction())
                        {

                            foreach (M_CONTENA_SOUSA contenaSousaEntity in this.entitys)
                            {

                                M_CONTENA_SOUSA entity = this.dao.GetDataByCd(contenaSousaEntity.CONTENA_SOUSA_CD.ToString());
                                if (entity != null)
                                {
                                    contenaSousaEntity.DELETE_FLG = true;

                                    this.dao.Update(contenaSousaEntity);
                                }
                            }
                            tran.Commit();

                        }
                        msgLogic.MessageBoxShow("I001", "選択データの削除");
                    }
                }
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
                    LogUtility.Error(ex); //登録エラー
                    this.form.errmessage.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
            }

            LogUtility.DebugMethodEnd();
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

                if (this.form.Ichiran.RowCount < 1)
                {
                    msgLogic.MessageBoxShow("E044");
                }
                else if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_CONTENA_SOUSA), this.form);
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
                if (false == SetSearchString())
                {
                    LogUtility.DebugMethodEnd(-1);
                    return -1;
                }

                this.SearchResult = dao.GetIchiranDataSql(this.SearchString
                                               , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);


                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;
                Properties.Settings.Default.CONDITION_DBFIELD = this.form.CONDITION_DBFIELD.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

                Properties.Settings.Default.Save();

                dtDetailList = this.SearchResult.Copy();

                if (this.SearchResult.Rows != null && this.SearchResult.Rows.Count > 0)
                {
                    count = 1;
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
            LogUtility.DebugMethodStart(errorFlag);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            this.isRegist = true;
            //エラーではない場合登録処理を行う
            try
            {
                if (!errorFlag)
                {
                    using (Transaction tran = new Transaction())
                    {
                        if (null != this.entitys)
                        {
                            foreach (M_CONTENA_SOUSA contenaSousaEntity in this.entitys)
                            {
                                M_CONTENA_SOUSA entity = this.dao.GetDataByCd(contenaSousaEntity.CONTENA_SOUSA_CD.ToString());

                                if (entity == null)
                                {
                                    this.dao.Insert(contenaSousaEntity);
                                }
                                else
                                {
                                    this.dao.Update(contenaSousaEntity);
                                }
                            }
                            //コメット
                            tran.Commit();

                            msgLogic.MessageBoxShow("I001", "登録");
                        }
                    }
                }
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
                    LogUtility.Error(ex); //登録エラー
                    this.form.errmessage.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
            }

            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            bool ret = true;
            LogUtility.DebugMethodStart();
            try
            {
                //条件を消す
                this.ClearCondition();
                //システム情報
                if (!this.GetSysInfo())
                {
                    ret = false;
                    return ret;
                }
                //条件
                this.SetSearchString();
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
        /// コンテナ操作CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck(string contena_Sousa_Cd, out bool catchErr)
        {
            LogUtility.DebugMethodStart(contena_Sousa_Cd);
            catchErr = true;
            try
            {
                //
                bool chkFlg = false;

                // 画面で操作CD重複チェック
                int recCount = 0;
                for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells[ContenaSousaHoshuConstans.CONTENA_SOUSA_CD].Value.ToString().Equals(Convert.ToString(contena_Sousa_Cd)))
                    {
                        recCount++;
                    }
                }

                if (recCount > 1)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }

                // 検索結果で操作CD重複チェック
                recCount = 0;
                for (int i = 0; i < dtDetailList.Rows.Count; i++)
                {
                    string cd = dtDetailList.Rows[i][1].ToString();
                    if (contena_Sousa_Cd.Equals(cd))
                    {
                        recCount++;
                        chkFlg = true;
                    }
                }
                if (recCount > 1)
                {
                    LogUtility.DebugMethodEnd(false, catchErr);
                    return false;
                }

                // DBで操作CD重複チェック
                M_CONTENA_SOUSA entity = this.dao.GetDataByCd(contena_Sousa_Cd);


                if (entity != null && !chkFlg)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DuplicationCheck", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(false, catchErr);
            return false;
        }

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        public void ClearCondition()
        {
            LogUtility.DebugMethodStart();

            this.form.CONDITION_VALUE.Text = string.Empty;
            this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
            this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
            this.form.CONDITION_ITEM.Text = string.Empty;
            //this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = false;
            //this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;
            //this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = false;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        public bool SetSearchString()
        {
            LogUtility.DebugMethodStart();

            M_CONTENA_SOUSA entity = new M_CONTENA_SOUSA();

            // CONDITION_TYPE
            if (string.IsNullOrEmpty(this.form.CONDITION_TYPE.Text) &&
                !string.IsNullOrEmpty(Properties.Settings.Default.ConditionValue_ItemDefinedTypes))
            {
                this.form.CONDITION_TYPE.Text = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
            }

            // CONDITION_DBFIELD
            if (string.IsNullOrEmpty(this.form.CONDITION_DBFIELD.Text) &&
                !string.IsNullOrEmpty(Properties.Settings.Default.ConditionValue_DBFieldsName))
            {
                this.form.CONDITION_DBFIELD.Text = Properties.Settings.Default.ConditionValue_DBFieldsName;
            }

            this.form.CONDITION_ITEM.Text = this.form.CONDITION_ITEM.Text.Replace("※", "");
            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text))
            {

                // 削除
                if (this.form.CONDITION_DBFIELD.Text.Equals("chb_delete"))
                {
                    if ("TRUE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                        "1".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                        "１".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                    {
                        entity.DELETE_FLG = true;
                        this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = true;
                    }
                    if ("FALSE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                        "0".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                        "０".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                    {
                        entity.DELETE_FLG = false;
                        this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;
                    }
                }

                //コンテナ操作CD
                if (this.form.CONDITION_ITEM.Text.Equals(ContenaSousaHoshuConstans.ITEM_CONTENA_SOUSA_CD))
                {
                    try
                    {
                        if (SqlInt32.Parse(this.form.CONDITION_VALUE.Text) > 32767)
                        {
                            string cd = "32767";
                            entity.CONTENA_SOUSA_CD = SqlInt16.Parse(cd);
                        }
                        else
                        {
                            entity.CONTENA_SOUSA_CD = SqlInt16.Parse(this.form.CONDITION_VALUE.Text);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E012", "コンテナ操作CDは数値,「32767」より小さい値");

                        form.CONDITION_VALUE.Focus();
                        return false;
                    }
                }
                //コンテナ操作名
                else if (this.form.CONDITION_ITEM.Text.Equals(ContenaSousaHoshuConstans.ITEM_CONTENA_SOUSA_NAME))
                {
                    entity.CONTENA_SOUSA_NAME = this.form.CONDITION_VALUE.Text;
                }
                //コンテナ操作備考
                else if (this.form.CONDITION_ITEM.Text.Equals(ContenaSousaHoshuConstans.ITEM_CONTENA_SOUSA_BIKOU))
                {
                    entity.CONTENA_SOUSA_BIKOU = this.form.CONDITION_VALUE.Text;
                }
                //作成者
                else if (this.form.CONDITION_ITEM.Text.Equals(ContenaSousaHoshuConstans.ITEM_CREATE_USER))
                {
                    entity.CREATE_USER = this.form.CONDITION_VALUE.Text;
                }
                //作成日
                else if (this.form.CONDITION_ITEM.Text.Equals(ContenaSousaHoshuConstans.ITEM_CREATE_DATE))
                {
                    entity.SEARCH_CREATE_DATE = this.form.CONDITION_VALUE.Text.Replace('/', '-');
                }
                //更新者
                else if (this.form.CONDITION_ITEM.Text.Equals(ContenaSousaHoshuConstans.ITEM_UPDATE_USER))
                {
                    entity.UPDATE_USER = this.form.CONDITION_VALUE.Text;
                }
                //更新日
                else if (this.form.CONDITION_ITEM.Text.Equals(ContenaSousaHoshuConstans.ITEM_UPDATE_DATE))
                {
                    entity.SEARCH_UPDATE_DATE = this.form.CONDITION_VALUE.Text.Replace('/', '-');
                }

            }

            this.SearchString = entity;

            LogUtility.DebugMethodEnd(true);

            return true;
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete, out bool catchErr)
        {
            LogUtility.DebugMethodStart(isDelete);
            catchErr = true;
            try
            {
                var entityList = new M_CONTENA_SOUSA[this.form.Ichiran.Rows.Count - 1];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_CONTENA_SOUSA();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_CONTENA_SOUSA>(entityList);
                DataTable dt = this.form.Ichiran.DataSource as DataTable;

                if (dt == null || dt.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(false, catchErr);
                    return false;
                }

                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(ContenaSousaHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }
                dt.BeginLoadData();
                preDt = GetCloneDataTable(dt);

                List<M_CONTENA_SOUSA> mContenaSousaList = new List<M_CONTENA_SOUSA>();
                if (isDelete)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (this.form.Ichiran.Rows[i].Cells["chb_delete"].Value != null && (bool)this.form.Ichiran.Rows[i].Cells["chb_delete"].Value)
                        {
                            isSelect = true;
                            var m_ContenaSousaEntity = CreateEntityForDataGridRow(this.form.Ichiran.Rows[i]);
                            dataBinderLogic.SetSystemProperty(m_ContenaSousaEntity, true);
                            mContenaSousaList.Add(m_ContenaSousaEntity);
                        }
                    }
                }

                if (isDelete)
                {
                    this.entitys = mContenaSousaList.ToArray();
                }
                else
                {
                    // 変更分のみ取得
                    List<M_CONTENA_SOUSA> addList = new List<M_CONTENA_SOUSA>();
                    DataTable dtChange = new DataTable();

                    if (dt.GetChanges() == null)
                    {
                        LogUtility.DebugMethodEnd(false, catchErr);
                        return false;
                    }
                    else
                    {
                        dtChange = dt.GetChanges();
                    }
                    var contenaSousaEntityList = CreateEntityForDataGrid(dtChange);
                    for (int i = 0; i < contenaSousaEntityList.Count; i++)
                    {
                        var contenaSousaEntity = contenaSousaEntityList[i];
                        for (int j = 0; j < this.form.Ichiran.Rows.Count - 1; j++)
                        {
                            bool isFind = false;

                            if (Convert.ToString(contenaSousaEntity.CONTENA_SOUSA_CD).Equals(this.form.Ichiran.Rows[j].Cells[ContenaSousaHoshuConstans.CONTENA_SOUSA_CD].Value.ToString()) &&
                                    bool.Parse(this.form.Ichiran.Rows[j].Cells[ContenaSousaHoshuConstans.DELETE_FLG].FormattedValue.ToString()) == isDelete)
                            {
                                isFind = true;
                            }


                            if (isFind)
                            {
                                dataBinderLogic.SetSystemProperty(contenaSousaEntity, false);
                                addList.Add(contenaSousaEntity);
                                break;
                            }
                        }
                        this.form.Ichiran.DataSource = preDt;
                        this.entitys = addList.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(true, catchErr);
            return true;
        }

        /// <summary>
        /// CreateEntityForDataGrid
        /// </summary>
        internal List<M_CONTENA_SOUSA> CreateEntityForDataGrid(DataTable gridView)
        {
            LogUtility.DebugMethodStart(gridView);

            var entityList = new List<M_CONTENA_SOUSA>();
            if (gridView == null)
            {
                LogUtility.DebugMethodEnd(entityList);
                return entityList;
            }
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                entityList.Add(CreateEntityForDataRow(gridView.Rows[i]));
            }

            LogUtility.DebugMethodEnd(entityList);

            return entityList;

        }

        /// <summary>
        /// CreateEntityForDataRow
        /// </summary>
        internal M_CONTENA_SOUSA CreateEntityForDataRow(DataRow row)
        {
            LogUtility.DebugMethodStart(row);

            M_CONTENA_SOUSA mContenaSousa = new M_CONTENA_SOUSA();
            // CONTENA_SOUSA_CD
            if (!DBNull.Value.Equals(row["CONTENA_SOUSA_CD"]))
            {
                mContenaSousa.CONTENA_SOUSA_CD = (Int16)row["CONTENA_SOUSA_CD"];
            }
            // CONTENA_SOUSA_NAME
            if (!DBNull.Value.Equals(row["CONTENA_SOUSA_NAME"]))
            {
                mContenaSousa.CONTENA_SOUSA_NAME = (string)row["CONTENA_SOUSA_NAME"];
            }
            // CONTENA_SOUSA_NAME_RYAKU
            if (!DBNull.Value.Equals(row["CONTENA_SOUSA_NAME_RYAKU"]))
            {
                mContenaSousa.CONTENA_SOUSA_NAME_RYAKU = (string)row["CONTENA_SOUSA_NAME_RYAKU"];
            }

            // CONTENA_SOUSA_BIKOU
            if (!DBNull.Value.Equals(row["CONTENA_SOUSA_BIKOU"]))
            {
                mContenaSousa.CONTENA_SOUSA_BIKOU = (string)row["CONTENA_SOUSA_BIKOU"];
            }
            // DELETE_FLG
            mContenaSousa.DELETE_FLG = false;
            // UPDATE_USER
            if (!DBNull.Value.Equals(row["UPDATE_USER"]))
            {
                mContenaSousa.UPDATE_USER = (string)row["UPDATE_USER"];
            }

            // UPDATE_DATE
            mContenaSousa.UPDATE_DATE = (DateTime)row["UPDATE_DATE"];

            // CREATE_USER
            if (!DBNull.Value.Equals(row["CREATE_USER"]))
            {
                mContenaSousa.CREATE_USER = (string)row["CREATE_USER"];
            }
            // CREATE_DATE
            mContenaSousa.CREATE_DATE = (DateTime)row["CREATE_DATE"];
            // CREATE_PC
            if (!DBNull.Value.Equals(row["CREATE_PC"]))
            {
                mContenaSousa.CREATE_PC = (string)row["CREATE_PC"];
            }
            // UPDATE_PC
            if (!DBNull.Value.Equals(row["UPDATE_PC"]))
            {
                mContenaSousa.UPDATE_PC = (string)row["UPDATE_PC"];
            }
            // TIME_STAMP
            if (!DBNull.Value.Equals(row["TIME_STAMP"]))
            {
                mContenaSousa.TIME_STAMP = (byte[])row["TIME_STAMP"];
            }

            LogUtility.DebugMethodEnd(mContenaSousa);

            return mContenaSousa;
        }

        /// <summary>
        /// CreateEntityForDataGridRow
        /// </summary>
        internal M_CONTENA_SOUSA CreateEntityForDataGridRow(DataGridViewRow row)
        {
            LogUtility.DebugMethodStart(row);

            M_CONTENA_SOUSA mContenaSousa = new M_CONTENA_SOUSA();
            // CONTENA_SOUSA_CD
            if (!DBNull.Value.Equals(row.Cells["CONTENA_SOUSA_CD"].Value))
            {
                mContenaSousa.CONTENA_SOUSA_CD = (Int16)row.Cells["CONTENA_SOUSA_CD"].Value;
            }
            // CONTENA_SOUSA_NAME
            if (!DBNull.Value.Equals(row.Cells["CONTENA_SOUSA_NAME"].Value))
            {
                mContenaSousa.CONTENA_SOUSA_NAME = (string)row.Cells["CONTENA_SOUSA_NAME"].Value;
            }
            // CONTENA_SOUSA_NAME_RYAKU
            if (!DBNull.Value.Equals(row.Cells["CONTENA_SOUSA_NAME_RYAKU"].Value))
            {
                mContenaSousa.CONTENA_SOUSA_NAME_RYAKU = (string)row.Cells["CONTENA_SOUSA_NAME_RYAKU"].Value;
            }

            // CONTENA_SOUSA_BIKOU
            if (!DBNull.Value.Equals(row.Cells["CONTENA_SOUSA_BIKOU"].Value))
            {
                mContenaSousa.CONTENA_SOUSA_BIKOU = (string)row.Cells["CONTENA_SOUSA_BIKOU"].Value;
            }
            // DELETE_FLG
            mContenaSousa.DELETE_FLG = false;
            // UPDATE_USER
            if (!DBNull.Value.Equals(row.Cells["UPDATE_USER"].Value))
            {
                mContenaSousa.UPDATE_USER = (string)row.Cells["UPDATE_USER"].Value;
            }

            // UPDATE_DATE
            mContenaSousa.UPDATE_DATE = (DateTime)row.Cells["UPDATE_DATE"].Value;

            // CREATE_USER
            if (!DBNull.Value.Equals(row.Cells["CREATE_USER"].Value))
            {
                mContenaSousa.CREATE_USER = (string)row.Cells["CREATE_USER"].Value;
            }
            // CREATE_DATE
            mContenaSousa.CREATE_DATE = (DateTime)row.Cells["CREATE_DATE"].Value;
            // CREATE_PC
            if (!DBNull.Value.Equals(row.Cells["CREATE_PC"].Value))
            {
                mContenaSousa.CREATE_PC = (string)row.Cells["CREATE_PC"].Value;
            }
            // UPDATE_PC
            if (!DBNull.Value.Equals(row.Cells["UPDATE_PC"].Value))
            {
                mContenaSousa.UPDATE_PC = (string)row.Cells["UPDATE_PC"].Value;
            }
            // TIME_STAMP
            if (!DBNull.Value.Equals(row.Cells["TIME_STAMP"].Value))
            {
                mContenaSousa.TIME_STAMP = (byte[])row.Cells["TIME_STAMP"].Value;
            }

            LogUtility.DebugMethodEnd(mContenaSousa);

            return mContenaSousa;
        }


        /// <summary>
        /// DataGridViewデータ変更チェック処理
        /// </summary>
        public bool IsDataChanged()
        {
            LogUtility.DebugMethodStart();

            DataTable dt = this.form.Ichiran.DataSource as DataTable;

            dt.BeginLoadData();

            DataTable dtClone = GetCloneDataTable(dt);

            if (null == dtClone.GetChanges())
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);

            return true;
        }

        /// <summary>
        /// DataGridViewデータ件数チェック処理
        /// </summary>
        public bool ActionBeforeCheck()
        {
            LogUtility.DebugMethodStart();

            if (this.form.Ichiran.Rows.Count > 1)
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// NOT NULL制約を一時的に解除
        /// </summary>
        public void ColumnAllowDBNull()
        {
            LogUtility.DebugMethodStart();
            DataTable dt = this.form.Ichiran.DataSource as DataTable;
            DataTable preDt = new DataTable();
            foreach (DataColumn column in dt.Columns)
            {
                // NOT NULL制約を一時的に解除(新規追加行対策)
                column.AllowDBNull = true;

                // TIME_STAMPがなぜか一意制約有のため、解除
                if (column.ColumnName.Equals(ContenaSousaHoshuConstans.TIME_STAMP))
                {
                    column.Unique = false;
                }
            }

            LogUtility.DebugMethodEnd();
        }

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

        #region テーブル固定値編集不可処理

        /// <summary>
        /// テーブル固定値定義書に存在するデータの場合、削除、名称、略称、適用期間を変更不可にする
        /// </summary>
        internal void SetFixedIchiran()
        {
            foreach (DataGridViewRow row in this.form.Ichiran.Rows)
            {
                if (!row.IsNewRow)
                {
                    this.SetFixedRow(row);
                }
            }
        }

        /// <summary>
        /// テーブル固定値の変更不可処理を行います
        /// </summary>
        /// <param name="row">変更不可処理を行うための判定データが入力されているRow</param>
        internal void SetFixedRow(DataGridViewRow row)
        {
            if (this.CheckFixedRow(row))
            {
                foreach (var columnName in ContenaSousaHoshuConstans.FixedColumnList)
                {
                    row.Cells[columnName].ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// テーブル固定値のデータかどうかを調べる
        /// </summary>
        /// <param name="row">行データ</param>
        /// <returns>結果</returns>
        internal bool CheckFixedRow(DataGridViewRow row)
        {
            if (row != null)
            {
                var objValue = row.Cells[ContenaSousaHoshuConstans.CONTENA_SOUSA_CD].Value;
                if (objValue != null)
                {
                    var strCd = objValue.ToString();
                    if (ContenaSousaHoshuConstans.FixedRowList.Contains(strCd))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion
    }
}
