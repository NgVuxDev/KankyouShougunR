// $Id: LogicCls.cs 37791 2014-12-19 08:22:08Z fangjk@oec-h.com $
using System;
using System.Collections;
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
using Shougun.Core.Master.ShikuchousonHoshu.APP;
using Shougun.Core.Master.ShikuchousonHoshu.Const;
using Shougun.Core.Master.ShikuchousonHoshu.Dao;

namespace Shougun.Core.Master.ShikuchousonHoshu.Logic
{
    /// <summary>
    /// 市区町村画面のビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.ShikuchousonHoshu.Setting.ButtonSetting.xml";

        private bool isSelect = false;

        /// <summary>
        /// 市区町村画面Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 市区町村のエンティティ
        /// </summary>
        private M_SHIKUCHOUSON[] entitys;

        /// <summary>
        /// 市区町村のDao
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

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_SHIKUCHOUSON SearchString { get; set; }

        /// <summary>
        /// dtDetailList
        /// </summary>
        public DataTable dtDetailList = new DataTable();

        #endregion

        #region 初期化処理

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<DaoCls>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            LogUtility.DebugMethodEnd();
        }

        # endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            LogUtility.DebugMethodStart();

            // ボタンのテキストを初期化
            this.ButtonInit();

            // イベントの初期化処理
            this.EventInit();

            this.allControl = this.form.allControl;

            // システム情報を取得し、初期値をセットする
            if (!GetSysInfoInit())
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }

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
            if (!r_framework.Authority.Manager.CheckAuthority("M262", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                this.DispReferenceMode();
            }

            LogUtility.DebugMethodEnd(true);
            return true;
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

        #region ボタン初期化処理

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

        #endregion

        #region ボタン設定の読込

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            ButtonSetting[] rtn;

            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();

            rtn = buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);

            LogUtility.DebugMethodEnd(rtn);
            return rtn;
        }

        #endregion

        #region イベントの初期化処理

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
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

            // 処理No
            parentForm.txb_process.Enabled = false;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 検索条件初期化

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

        #endregion

        #region 検索条件の設定

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        public void SetSearchString()
        {
            LogUtility.DebugMethodStart();

            M_SHIKUCHOUSON entity = new M_SHIKUCHOUSON();

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    //必要？
                    // 削除項目が選択された場合
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("DELETE_FLG"))
                    {
                        if ("TRUE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "1".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "１".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = true;
                        }
                        if ("FALSE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "0".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "０".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;
                        }
                    }

                    //検索条件の設定
                    entity.SetValue(this.form.CONDITION_VALUE);
                }
            }

            this.SearchString = entity;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 検索結果を一覧に設定

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal void SetIchiran()
        {
            LogUtility.DebugMethodStart();

            var table = this.SearchResult;

            table.BeginLoadData();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].ReadOnly = false;
            }

            this.form.Ichiran.DataSource = table;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region システム情報を取得し、初期値をセットする

        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public bool GetSysInfoInit()
        {
            LogUtility.DebugMethodStart();
            try
            {
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
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSysInfoInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion

        #endregion

        #region 業務処理

        #region DataTableのクローン処理

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

        #endregion

        #region 更新処理

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);
            LogUtility.DebugMethodEnd();
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
                            foreach (M_SHIKUCHOUSON contenashuruiEntity in this.entitys)
                            {
                                //if (contenashuruiEntity.SHIKUCHOUSON_CD == null)
                                //{
                                //    msgLogic.MessageBoxShow("E075", "削除");
                                //    return;
                                //}
                                M_SHIKUCHOUSON entity = this.dao.GetDataByCd(contenashuruiEntity.SHIKUCHOUSON_CD.ToString());
                                if (entity != null)
                                {
                                    String PCName = System.Environment.MachineName;
                                    contenashuruiEntity.DELETE_FLG = true;
                                    // 20151102 katen #12048 「システム日付」の基準作成、適用 start
                                    //contenashuruiEntity.UPDATE_DATE = DateTime.Now
                                    contenashuruiEntity.UPDATE_DATE = this.getDBDateTime();
                                    // 20151102 katen #12048 「システム日付」の基準作成、適用 end
                                    contenashuruiEntity.UPDATE_PC = PCName;

                                    this.dao.Update(contenashuruiEntity);
                                }
                            }
                            tran.Commit();
                        }
                        msgLogic.MessageBoxShow("I001", "削除");
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
            LogUtility.DebugMethodStart();
            LogUtility.DebugMethodEnd();
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// 削除できるかどうかチェックする
        /// </summary>
        public bool CheckDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                var shikuchousonCd = string.Empty;
                string[] strList;

                foreach (DataGridViewRow Row in this.form.Ichiran.Rows)
                {
                    if (Row.Cells["DELETE_FLG"].Value != null && Row.Cells["DELETE_FLG"].Value.ToString() == "True")
                    {
                        if (Row.Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(Row.Cells["CREATE_USER"].Value.ToString()))
                        {
                            continue;
                        }
                        shikuchousonCd += Row.Cells["SHIKUCHOUSON_CD"].Value.ToString() + ",";
                    }
                }

                if (!string.IsNullOrEmpty(shikuchousonCd))
                {
                    shikuchousonCd = shikuchousonCd.Substring(0, shikuchousonCd.Length - 1);
                    strList = shikuchousonCd.Split(',');
                    DataTable dtTable = dao.GetDataBySqlFileCheck(strList);
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        string strName = string.Empty;

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            strName += "\n" + dr["NAME"].ToString();
                        }

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E258", "市区町村", "市区町村CD", strName);

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

        #region CSV出力

        /// <summary>
        /// CSV出力
        /// </summary>
        public void CSVOutput()
        {
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
            {
                //CSVFileLogic csvLogic = new CSVFileLogic();
                CSVExport csvExport = new CSVExport();
                csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_SHIKUCHOUSON), this.form);
                //msgLogic.MessageBoxShow("I000", "CSV出力");
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 条件取消

        /// <summary>
        /// 条件取消
        /// </summary>
        public void CancelCondition()
        {
            LogUtility.DebugMethodStart();

            ClearCondition();
            SetIchiran();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region データ取得処理

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
                SetSearchString();

                this.SearchResult = dao.GetIchiranDataSql(this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

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
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        #endregion

        #region 登録処理

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            //独自チェックの記述例を書く
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    using (Transaction tran = new Transaction())
                    {
                        // トランザクション開始
                        foreach (M_SHIKUCHOUSON contenajoukyouEntity in this.entitys)
                        {
                            M_SHIKUCHOUSON entity = this.dao.GetDataByCd(contenajoukyouEntity.SHIKUCHOUSON_CD.ToString());
                            if (entity == null)
                            {
                                this.dao.Insert(contenajoukyouEntity);
                            }
                            else
                            {
                                this.dao.Update(contenajoukyouEntity);
                            }
                        }
                        tran.Commit();
                    }
                    msgLogic.MessageBoxShow("I001", "登録");
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
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 取消処理

        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            LogUtility.DebugMethodStart();

            // システム情報を取得し、初期値をセットする
            if (!GetSysInfoInit())
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            // 検索項目を初期値にセットする
            this.form.CONDITION_VALUE.Text = "";
            this.form.CONDITION_VALUE.DBFieldsName = "";
            this.form.CONDITION_VALUE.ItemDefinedTypes = "";
            this.form.CONDITION_ITEM.Text = "";

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion

        #region 市区町村CDの重複チェック

        /// <summary>
        /// 市区町村CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck(string contena_Shurui_Cd, out bool catchErr)
        {
            LogUtility.DebugMethodStart();
            catchErr = true;

            try
            {
                // 画面で種類CD重複チェック
                int recCount = 0;
                for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
                {
                    string strCD = this.form.Ichiran.Rows[i].Cells["SHIKUCHOUSON_CD"].Value.ToString().PadLeft(3, '0');
                    if (strCD.Equals(Convert.ToString(contena_Shurui_Cd)))
                    {
                        recCount++;
                    }
                }

                if (recCount > 1)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }

                // 検索結果で種類CD重複チェック
                for (int i = 0; i < dtDetailList.Rows.Count; i++)
                {
                    if (contena_Shurui_Cd.Equals(dtDetailList.Rows[i][1]))
                    {
                        LogUtility.DebugMethodEnd(false, catchErr);
                        return false;
                    }
                }

                // DBで種類CD重複チェック
                M_SHIKUCHOUSON entity = this.dao.GetDataByCd(contena_Shurui_Cd);

                if (entity != null)
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

        #endregion

        #region コントロールから対象のEntityを作成する

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete, out bool catchErr)
        {
            LogUtility.DebugMethodStart(isDelete);
            catchErr = true;

            try
            {
                var entityList = new M_SHIKUCHOUSON[this.form.Ichiran.Rows.Count - 1];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_SHIKUCHOUSON();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_SHIKUCHOUSON>(entityList);
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
                    if (column.ColumnName.Equals(ConstCls.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();
                preDt = GetCloneDataTable(dt);

                List<M_SHIKUCHOUSON> mContenaShuruiList = new List<M_SHIKUCHOUSON>();
                if (isDelete)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (this.form.Ichiran.Rows[i].Cells["chb_delete"].Value != null && (bool)this.form.Ichiran.Rows[i].Cells["chb_delete"].Value)
                        {
                            if (this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value.ToString()))
                            {
                                isSelect = true;
                                continue;
                            }
                            isSelect = true;
                            var m_ContenaShuruiEntity = CreateEntityForDataGridRow(this.form.Ichiran.Rows[i]);
                            dataBinderLogic.SetSystemProperty(m_ContenaShuruiEntity, true);
                            m_ContenaShuruiEntity.DELETE_FLG = true;
                            mContenaShuruiList.Add(m_ContenaShuruiEntity);
                        }
                    }
                }

                if (isDelete)
                {
                    this.entitys = mContenaShuruiList.ToArray();
                }
                else
                {
                    // 変更分のみ取得
                    List<M_SHIKUCHOUSON> addList = new List<M_SHIKUCHOUSON>();

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
                    //var contenashuruiEntityList = CreateEntityForDataGrid(this.form.Ichiran);
                    var contenashuruiEntityList = CreateEntityForDataGrid(dtChange);
                    for (int i = 0; i < contenashuruiEntityList.Count; i++)
                    {
                        var contenashuruiEntity = contenashuruiEntityList[i];
                        for (int j = 0; j < this.form.Ichiran.Rows.Count - 1; j++)
                        {
                            bool isFind = false;
                            //if (Convert.ToString(contenashuruiEntity.SHIKUCHOUSON_CD).Equals(this.form.Ichiran.Rows[j].Cells[ConstCls.SHIKUCHOUSON_CD].Value.ToString()) &&
                            //    bool.Parse(this.form.Ichiran.Rows[j].Cells[ConstCls.DELETE_FLG].FormattedValue.ToString()) == isDelete)
                            if (this.form.Ichiran.Rows[j].Cells[ConstCls.SHIKUCHOUSON_CD].Value.Equals(Convert.ToString(contenashuruiEntity.SHIKUCHOUSON_CD)) &&
                                     bool.Parse(this.form.Ichiran.Rows[j].Cells[ConstCls.DELETE_FLG].FormattedValue.ToString()) == isDelete)
                            {
                                isFind = true;
                            }

                            if (isFind)
                            {
                                dataBinderLogic.SetSystemProperty(contenashuruiEntity, false);

                                addList.Add(contenashuruiEntity);
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

        #endregion

        #region CreateEntityForDataGrid

        /// <summary>
        /// CreateEntityForDataGrid
        /// </summary>
        /// <returns>
        /// entityList
        /// </returns>
        internal List<M_SHIKUCHOUSON> CreateEntityForDataGrid(DataTable gridView)
        {
            LogUtility.DebugMethodStart(gridView);

            var entityList = new List<M_SHIKUCHOUSON>();
            if (gridView == null)
            {
                return entityList;
            }
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                entityList.Add(CreateEntityForDataRow(gridView.Rows[i]));
            }

            LogUtility.DebugMethodEnd(entityList);
            return entityList;
        }

        #endregion

        #region CreateEntityForDataGridRow

        /// <summary>
        /// CreateEntityForDataGridRow
        /// </summary>
        /// <returns>
        /// mContenaShurui
        /// </returns>
        internal M_SHIKUCHOUSON CreateEntityForDataGridRow(DataGridViewRow row)
        {
            LogUtility.DebugMethodStart(row);

            M_SHIKUCHOUSON mContenaShurui = new M_SHIKUCHOUSON();

            // SHIKUCHOUSON_CD
            if (!DBNull.Value.Equals(row.Cells["SHIKUCHOUSON_CD"].Value))
            {
                mContenaShurui.SHIKUCHOUSON_CD = (string)row.Cells["SHIKUCHOUSON_CD"].Value;
            }

            // SHIKUCHOUSON_NAME
            if (!DBNull.Value.Equals(row.Cells["SHIKUCHOUSON_NAME"].Value))
            {
                mContenaShurui.SHIKUCHOUSON_NAME = (string)row.Cells["SHIKUCHOUSON_NAME"].Value;
            }

            // SHIKUCHOUSON_NAME_RYAKU
            if (!DBNull.Value.Equals(row.Cells["SHIKUCHOUSON_NAME_RYAKU"].Value))
            {
                mContenaShurui.SHIKUCHOUSON_NAME_RYAKU = (string)row.Cells["SHIKUCHOUSON_NAME_RYAKU"].Value;
            }

            // SHIKUCHOUSON_FURIGANA
            if (!DBNull.Value.Equals(row.Cells["SHIKUCHOUSON_FURIGANA"].Value))
            {
                mContenaShurui.SHIKUCHOUSON_FURIGANA = (string)row.Cells["SHIKUCHOUSON_FURIGANA"].Value;
            }

            // SHIKUCHOUSON_BIKOU
            if (!DBNull.Value.Equals(row.Cells["SHIKUCHOUSON_BIKOU"].Value))
            {
                mContenaShurui.SHIKUCHOUSON_BIKOU = (string)row.Cells["SHIKUCHOUSON_BIKOU"].Value;
            }

            // DELETE_FLG
            if (!DBNull.Value.Equals(row.Cells["DELETE_FLG"].Value))
            {
                mContenaShurui.DELETE_FLG = (Boolean)row.Cells["DELETE_FLG"].Value;
            }
            else
            {
                mContenaShurui.DELETE_FLG = false;
            }

            // TIME_STAMP
            if (!DBNull.Value.Equals(row.Cells["TIME_STAMP"].Value))
            {
                mContenaShurui.TIME_STAMP = (byte[])row.Cells["TIME_STAMP"].Value;
            }

            LogUtility.DebugMethodEnd(mContenaShurui);
            return mContenaShurui;
        }

        #endregion

        #region CreateEntityForDataRow

        /// <summary>
        /// CreateEntityForDataRow
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        internal M_SHIKUCHOUSON CreateEntityForDataRow(DataRow row)
        {
            LogUtility.DebugMethodStart(row);

            M_SHIKUCHOUSON mContenaShurui = new M_SHIKUCHOUSON();

            // SHIKUCHOUSON_CD
            if (!DBNull.Value.Equals(row["SHIKUCHOUSON_CD"]))
            {
                mContenaShurui.SHIKUCHOUSON_CD = (string)row["SHIKUCHOUSON_CD"];
            }

            // SHIKUCHOUSON_NAME
            if (!DBNull.Value.Equals(row["SHIKUCHOUSON_NAME"]))
            {
                mContenaShurui.SHIKUCHOUSON_NAME = (string)row["SHIKUCHOUSON_NAME"];
            }

            // SHIKUCHOUSON_NAME_RYAKU
            if (!DBNull.Value.Equals(row["SHIKUCHOUSON_NAME_RYAKU"]))
            {
                mContenaShurui.SHIKUCHOUSON_NAME_RYAKU = (string)row["SHIKUCHOUSON_NAME_RYAKU"];
            }

            // SHIKUCHOUSON_FURIGANA
            if (!DBNull.Value.Equals(row["SHIKUCHOUSON_FURIGANA"]))
            {
                mContenaShurui.SHIKUCHOUSON_FURIGANA = (string)row["SHIKUCHOUSON_FURIGANA"];
            }

            // SHIKUCHOUSON_BIKOU
            if (!DBNull.Value.Equals(row["SHIKUCHOUSON_BIKOU"]))
            {
                mContenaShurui.SHIKUCHOUSON_BIKOU = (string)row["SHIKUCHOUSON_BIKOU"];
            }

            // DELETE_FLG
            if (!DBNull.Value.Equals(row["DELETE_FLG"]))
            {
                mContenaShurui.DELETE_FLG = (Boolean)row["DELETE_FLG"];
            }
            else
            {
                mContenaShurui.DELETE_FLG = false;
            }

            // TIME_STAMP
            if (!DBNull.Value.Equals(row["TIME_STAMP"]))
            {
                mContenaShurui.TIME_STAMP = (byte[])row["TIME_STAMP"];
            }

            LogUtility.DebugMethodEnd(mContenaShurui);

            return mContenaShurui;
        }

        #endregion

        #region 登録前のチェック

        /// <summary>
        /// 登録前のチェック
        /// </summary>
        /// <returns></returns>
        public Boolean CheckBeforeUpdate()
        {
            LogUtility.DebugMethodStart();

            var msgLogic = new MessageBoxShowLogic();

            ArrayList errColName = new ArrayList();

            Boolean rtn = false;

            try
            {
                Boolean isErr;

                // 必須入力チェック
                isErr = false;
                for (int i = 0; i < this.form.Ichiran.RowCount - 1; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells["chb_delete"].Value != null && this.form.Ichiran.Rows[i].Cells["chb_delete"].Value.ToString() == "True"
                        && (this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value.ToString())))
                    {
                        continue;
                    }

                    if (null == this.form.Ichiran.Rows[i].Cells["SHIKUCHOUSON_CD"].Value ||
                        "".Equals(this.form.Ichiran.Rows[i].Cells["SHIKUCHOUSON_CD"].Value.ToString().Trim()))
                    {
                        if (false == isErr)
                        {
                            errColName.Add("市区町村CD");
                            isErr = true;
                        }
                        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells["SHIKUCHOUSON_CD"], true);
                    }
                }

                // 市区町村名
                isErr = false;
                for (int i = 0; i < this.form.Ichiran.RowCount - 1; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells["chb_delete"].Value != null && this.form.Ichiran.Rows[i].Cells["chb_delete"].Value.ToString() == "True"
                       && (this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value.ToString())))
                    {
                        continue;
                    }

                    if (DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["SHIKUCHOUSON_NAME"].Value) ||
                        "".Equals(this.form.Ichiran.Rows[i].Cells["SHIKUCHOUSON_NAME"].Value.ToString().Trim()))
                    {
                        if (false == isErr)
                        {
                            errColName.Add("市区町村名");
                            isErr = true;
                        }
                        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells["SHIKUCHOUSON_NAME"], true);
                    }
                }

                // 略称
                isErr = false;
                for (int i = 0; i < this.form.Ichiran.RowCount - 1; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells["chb_delete"].Value != null && this.form.Ichiran.Rows[i].Cells["chb_delete"].Value.ToString() == "True"
                        && (this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value.ToString())))
                    {
                        continue;
                    }

                    if (DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["SHIKUCHOUSON_NAME_RYAKU"].Value) ||
                        "".Equals(this.form.Ichiran.Rows[i].Cells["SHIKUCHOUSON_NAME_RYAKU"].Value.ToString().Trim()))
                    {
                        if (false == isErr)
                        {
                            errColName.Add("略称");
                            isErr = true;
                        }
                        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells["SHIKUCHOUSON_NAME_RYAKU"], true);
                    }
                }

                // フリガナ
                isErr = false;
                for (int i = 0; i < this.form.Ichiran.RowCount - 1; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells["chb_delete"].Value != null && this.form.Ichiran.Rows[i].Cells["chb_delete"].Value.ToString() == "True"
                        && (this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value.ToString())))
                    {
                        continue;
                    }

                    if (DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["SHIKUCHOUSON_FURIGANA"].Value) ||
                        "".Equals(this.form.Ichiran.Rows[i].Cells["SHIKUCHOUSON_FURIGANA"].Value.ToString().Trim()))
                    {
                        if (false == isErr)
                        {
                            errColName.Add("フリガナ");
                            isErr = true;
                        }
                        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells["SHIKUCHOUSON_FURIGANA"], true);
                    }
                }

                rtn = (errColName.Count == 0);

                MessageUtility messageUtility = new MessageUtility();
                string message = messageUtility.GetMessage("E001").MESSAGE;

                string errMsg = "";
                if (!rtn)
                {
                    foreach (string colName in errColName)
                    {
                        if (errMsg.Length > 0)
                        {
                            errMsg += "\n";
                        }
                        errMsg += message.Replace("{0}", colName);
                    }
                    MessageBox.Show(errMsg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckBeforeUpdate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                rtn = false;
            }

            LogUtility.DebugMethodEnd(rtn);
            return rtn;
        }

        #endregion

        #region DataGridViewデータ件数チェック処理

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

        #endregion

        #region NOT NULL制約を一時的に解除

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
                if (column.ColumnName.Equals(ConstCls.TIME_STAMP))
                {
                    column.Unique = false;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

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

        /// <summary>
        /// 表示条件の選択状態が変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void ICHIRAN_HYOUJI_JOUKEN_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == false
                && this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked == false)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E001", "表示条件");
                ((CheckBox)sender).Checked = true;
            }
        }

        /// 20141217 Houkakou 「市区町村入力」の日付チェックを追加する　end

        /// <summary>
        /// 主キーが同一の行がDBに存在する場合、主キーを非活性にする
        /// </summary>
        internal void EditableToPrimaryKey()
        {
            // DBから主キーのListを取得
            var allEntityList = this.dao.GetAllData().Select(s => s.SHIKUCHOUSON_CD).Where(s => !string.IsNullOrEmpty(s)).ToList();

            // DBに存在する行の主キーを非活性にする
            this.form.Ichiran.Rows.Cast<DataGridViewRow>().Select(r => r.Cells["SHIKUCHOUSON_CD"]).Where(c => c.Value != null).ToList().
                                    ForEach(c => c.ReadOnly = allEntityList.Contains(c.Value.ToString()));
        }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
    }
}