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
using Shougun.Core.ExternalConnection.ContenaKeikaDate.Const;
using Shougun.Core.ExternalConnection.ContenaKeikaDate.DAO;

namespace Shougun.Core.ExternalConnection.ContenaKeikaDate
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.ContenaKeikaDate.Setting.ButtonSetting.xml";

        private bool isSelect = false;
        
        public bool isRegist = true;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// フォームオブジェクト
        /// </summary>
        internal MasterBaseForm parentForm;

        /// <summary>
        /// コンテナ設置期間表示設定のエンティティ
        /// </summary>
        private M_CONTENA_KEIKA_DATE[] entitys;

        /// <summary>
        /// コンテナ設置期間表示設定のDao
        /// </summary>
        private DAOClass dao;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        public DataTable dtDetailList = new DataTable();

        private GET_SYSDATEDao dateDao;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_CONTENA_KEIKA_DATE SearchString { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<DAOClass>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao

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
                this.parentForm = (MasterBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // システム情報を取得し、初期値をセットする
                GetSysInfoInit();

                //前の表示条件を初期値に設定する
                if (Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED)
                {
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                }

                this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.CONDITION_DBFIELD.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_DBFIELD.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;

                // 選択された条件カラムにより、サブ情報、IMEを設定
                this.form.SetConditionControl(this.form.CONDITION_ITEM.Text);


                // システム設定によって表示を変更する箇所
                this.HeaderInit((int)this.sysInfoEntity.CONTENA_KANRI_HOUHOU);
                if (this.sysInfoEntity.CONTENA_KANRI_HOUHOU == 1)
                {
                    // 数量管理
                    this.form.Ichiran.Columns[ConstClass.CONTENA_KEIKA_DATE].HeaderText = "無回転日数（以内）※";
                }
                else
                { 
                    // 個体管理
                    this.form.Ichiran.Columns[ConstClass.CONTENA_KEIKA_DATE].HeaderText = "設置経過日数（以内）※";
                }

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M223", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
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

            var buttonSetting = new ButtonSetting();
            var parentForm = (MasterBaseForm)this.form.Parent;
            //var parentForm = (BusinessBaseForm)this.form.Parent;
            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();

            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        #endregion

        #region イベント初期化処理

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (MasterBaseForm)this.form.Parent;
            //var parentForm = (BusinessBaseForm)this.form.Parent;
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

            // 処理Noを非活性
            parentForm.txb_process.Enabled = false;

            LogUtility.DebugMethodEnd();
        }

        #endregion

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

        #region システム情報を取得し、初期値をセットする

        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void GetSysInfoInit()
        {
            LogUtility.DebugMethodStart();

            // システム情報を取得し、初期値をセットする
            M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
            if (sysInfo != null)
            {
                this.sysInfoEntity = sysInfo[0];
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = (bool)this.sysInfoEntity.ICHIRAN_HYOUJI_JOUKEN_DELETED;
            }

            LogUtility.DebugMethodEnd();
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
                            if (this.entitys != null)
                            {
                                foreach (M_CONTENA_KEIKA_DATE genchakuTimeEntity in this.entitys)
                                {
                                    M_CONTENA_KEIKA_DATE entity = this.dao.GetDataByCd(genchakuTimeEntity.CONTENA_KEIKA_DATE.ToString());

                                    if (entity != null)
                                    {
                                        this.dao.Update(genchakuTimeEntity);
                                    }
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
                    LogUtility.Error(ex); //DBエラー
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

        #endregion

        #region 物理削除処理

        /// <summary>
        /// 物理削除処理
        /// </summary>
        public void PhysicalDelete()
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();

            throw new NotImplementedException();
        }

        #endregion

        #region 登録処理

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            //独自チェックの記述例を書く
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            this.isRegist = true;

            try
            {
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    using (Transaction tran = new Transaction())
                    {
                        if (this.entitys != null)
                        {
                            foreach (M_CONTENA_KEIKA_DATE genchakuTimeEntity in this.entitys)
                            {
                                M_CONTENA_KEIKA_DATE entity = this.dao.GetDataByCd(genchakuTimeEntity.CONTENA_KEIKA_DATE.ToString());

                                if (entity == null)
                                {
                                    this.dao.Insert(genchakuTimeEntity);
                                }
                                else
                                {
                                    this.dao.Update(genchakuTimeEntity);
                                }
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
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
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

            try
            {
                GetSysInfoInit();

                // 検索項目を初期値にセットする
                this.form.CONDITION_VALUE.Text = "";
                this.form.CONDITION_VALUE.DBFieldsName = "";
                this.form.CONDITION_VALUE.ItemDefinedTypes = "";
                this.form.CONDITION_ITEM.Text = "";
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Cancel", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }

        #endregion

        #region 設置経過日数の重複チェック

        /// <summary>
        /// 設置経過日数の重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck(string Cotena_Keika_Date, out bool catchErr)
        {
            LogUtility.DebugMethodStart(Cotena_Keika_Date);
            catchErr = true;
            try
            {
                // 画面で設置経過日数重複チェック
                int recCount = 0;
                for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells[ConstClass.CONTENA_KEIKA_DATE].Value.ToString().Equals(Convert.ToString(Cotena_Keika_Date)))
                    {
                        recCount++;
                    }
                }

                if (recCount > 1)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }

                // 検索結果で設置経過日数重複チェック
                for (int i = 0; i < dtDetailList.Rows.Count; i++)
                {
                    if (Cotena_Keika_Date.Equals(dtDetailList.Rows[i][1].ToString()))
                    {
                        LogUtility.DebugMethodEnd(false, catchErr);
                        return false;
                    }
                }

                // DBで設置経過日数重複チェック
                M_CONTENA_KEIKA_DATE entity = this.dao.GetDataByCd(Cotena_Keika_Date.ToString());

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

        #region 更新処理

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
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
                var entityList = new M_CONTENA_KEIKA_DATE[this.form.Ichiran.Rows.Count - 1];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_CONTENA_KEIKA_DATE();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_CONTENA_KEIKA_DATE>(entityList);
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
                    if (column.ColumnName.Equals(Const.ConstClass.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();
                preDt = GetCloneDataTable(dt);

                List<M_CONTENA_KEIKA_DATE> mGenchakuTimeiList = new List<M_CONTENA_KEIKA_DATE>();
                if (isDelete)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (this.form.Ichiran.Rows[i].Cells["chb_delete"].Value != null && (bool)this.form.Ichiran.Rows[i].Cells["chb_delete"].Value)
                        {
                            if (this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value == null
                                || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value.ToString()))
                            {
                                isSelect = true;
                                continue;
                            }

                            isSelect = true;
                            var m_GenchakuTimeEntity = CreateEntityForDataGridRow(this.form.Ichiran.Rows[i]);
                            dataBinderLogic.SetSystemProperty(m_GenchakuTimeEntity, true);

                            m_GenchakuTimeEntity.DELETE_FLG = true;

                            mGenchakuTimeiList.Add(m_GenchakuTimeEntity);
                        }
                    }
                }

                if (isDelete)
                {
                    this.entitys = mGenchakuTimeiList.ToArray();
                }
                else
                {
                    // 変更分のみ取得
                    List<M_CONTENA_KEIKA_DATE> addList = new List<M_CONTENA_KEIKA_DATE>();

                    DataTable dtChange = new DataTable();

                    if (dt.GetChanges() == null)
                    {
                        LogUtility.DebugMethodEnd(false, catchErr);
                        return false;
                    }
                    else
                    {
                        // 元の値から全く変化がなければ、 RowState を元の状態に戻す
                        foreach (DataRow row in dt.Rows)
                        {
                            if (!DataTableUtility.IsDataRowChanged(row))
                            {
                                row.AcceptChanges();
                            }
                        }

                        dtChange = dt.GetChanges();
                    }

                    var genchakuTimeEntityList = CreateEntityForDataGrid(dtChange);

                    for (int i = 0; i < genchakuTimeEntityList.Count; i++)
                    {
                        var genchakuTimeEntity = genchakuTimeEntityList[i];
                        for (int j = 0; j < this.form.Ichiran.Rows.Count - 1; j++)
                        {
                            bool isFind = false;
                            if (this.form.Ichiran.Rows[j].Cells[ConstClass.CONTENA_KEIKA_DATE].Value != null)
                            {
                                if (Convert.ToString(genchakuTimeEntity.CONTENA_KEIKA_DATE).Equals(this.form.Ichiran.Rows[j].Cells[ConstClass.CONTENA_KEIKA_DATE].Value.ToString()) &&
                                        bool.Parse(this.form.Ichiran.Rows[j].Cells[ConstClass.DELETE_FLG].FormattedValue.ToString()) == isDelete)
                                {
                                    isFind = true;
                                }
                            }

                            if (isFind)
                            {
                                dataBinderLogic.SetSystemProperty(genchakuTimeEntity, false);
                                addList.Add(genchakuTimeEntity);
                                break;
                            }
                        }
                        this.form.Ichiran.DataSource = preDt;
                        this.entitys = addList.ToArray();
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CreateEntity", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
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
        /// データグリッドビューから登録用Entity作成
        /// </summary>
        /// <param name="gridView"></param>
        /// <returns></returns>
        internal List<M_CONTENA_KEIKA_DATE> CreateEntityForDataGrid(DataTable gridView)
        {
            LogUtility.DebugMethodStart(gridView);

            var entityList = new List<M_CONTENA_KEIKA_DATE>();
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

        #endregion

        #region CreateEntityForDataGridRow

        /// <summary>
        /// データグリッドのエンティティ作成
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        internal M_CONTENA_KEIKA_DATE CreateEntityForDataGridRow(DataGridViewRow row)
        {
            LogUtility.DebugMethodStart(row);

            M_CONTENA_KEIKA_DATE mGenchakuTime = new M_CONTENA_KEIKA_DATE();

            // CONTENA_KEIKA_DATE
            if (!DBNull.Value.Equals(row.Cells[ConstClass.CONTENA_KEIKA_DATE].Value))
            {
                mGenchakuTime.CONTENA_KEIKA_DATE = SqlInt16.Parse(row.Cells[ConstClass.CONTENA_KEIKA_DATE].Value.ToString());
            }

            // CONTENA_KEIKA_BACK_COLOR
            if (!DBNull.Value.Equals(row.Cells[ConstClass.CONTENA_KEIKA_BACK_COLOR].Value))
            {
                mGenchakuTime.CONTENA_KEIKA_BACK_COLOR = SqlInt32.Parse(row.Cells[ConstClass.CONTENA_KEIKA_BACK_COLOR].Value.ToString());
            }

            // CONTENA_KEIKA_BIKOU
            if (!DBNull.Value.Equals(row.Cells[ConstClass.CONTENA_KEIKA_BIKOU].Value))
            {
                mGenchakuTime.CONTENA_KEIKA_BIKOU = (string)row.Cells[ConstClass.CONTENA_KEIKA_BIKOU].Value;
            }

            // DELETE_FLG
            if (!DBNull.Value.Equals(row.Cells["chb_delete"].Value))
            {
                mGenchakuTime.DELETE_FLG = (Boolean)row.Cells["chb_delete"].Value;
            }
            else
            {
                mGenchakuTime.DELETE_FLG = false;
            }

            // TIME_STAMP
            if (!DBNull.Value.Equals(row.Cells["TIME_STAMP"].Value))
            {
                mGenchakuTime.TIME_STAMP = (byte[])row.Cells["TIME_STAMP"].Value;
            }

            LogUtility.DebugMethodEnd(mGenchakuTime);

            return mGenchakuTime;
        }

        #endregion

        #region CreateEntityForDataRow

        /// <summary>
        /// エンティティの中身作成
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        internal M_CONTENA_KEIKA_DATE CreateEntityForDataRow(DataRow row)
        {
            LogUtility.DebugMethodStart(row);

            M_CONTENA_KEIKA_DATE mGenchakuTime = new M_CONTENA_KEIKA_DATE();

            // CONTENA_KEIKA_DATE
            if (!DBNull.Value.Equals(row[ConstClass.CONTENA_KEIKA_DATE]))
            {
                mGenchakuTime.CONTENA_KEIKA_DATE = SqlInt16.Parse(row[ConstClass.CONTENA_KEIKA_DATE].ToString());
            }

            // CONTENA_KEIKA_BACK_COLOR
            if (!DBNull.Value.Equals(row[ConstClass.CONTENA_KEIKA_BACK_COLOR]))
            {
                mGenchakuTime.CONTENA_KEIKA_BACK_COLOR = SqlInt32.Parse(row[ConstClass.CONTENA_KEIKA_BACK_COLOR].ToString());
            }

            // CONTENA_KEIKA_BIKOU
            if (!DBNull.Value.Equals(row[ConstClass.CONTENA_KEIKA_BIKOU]))
            {
                mGenchakuTime.CONTENA_KEIKA_BIKOU = (string)row[ConstClass.CONTENA_KEIKA_BIKOU];
            }

            // DELETE_FLG
            if (!DBNull.Value.Equals(row["DELETE_FLG"]))
            {
                mGenchakuTime.DELETE_FLG = (Boolean)row["DELETE_FLG"];
            }
            else
            {
                mGenchakuTime.DELETE_FLG = false;
            }

            // TIME_STAMP
            if (!DBNull.Value.Equals(row["TIME_STAMP"]))
            {
                mGenchakuTime.TIME_STAMP = (byte[])row["TIME_STAMP"];
            }

            LogUtility.DebugMethodEnd(mGenchakuTime);

            return mGenchakuTime;
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
                if (column.ColumnName.Equals(Const.ConstClass.TIME_STAMP))
                {
                    column.Unique = false;
                }
            }

            LogUtility.DebugMethodEnd();
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
                    csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_CONTENA_KEIKA_DATE), this.form);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVOutput", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
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
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 検索条件の設定

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        public Boolean SetSearchString()
        {
            LogUtility.DebugMethodStart();

            M_CONTENA_KEIKA_DATE entity = new M_CONTENA_KEIKA_DATE();
            string errorColumn = string.Empty;

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_DBFIELD.DBFieldsName))
                {
                    // 検索条件の設定
                    // 削除
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.DBFieldsName.Equals("DELETE_FLG"))
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

                    // 設置経過日数
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.DBFieldsName.Equals(ConstClass.CONTENA_KEIKA_DATE))
                    {
                        try
                        {
                            entity.CONTENA_KEIKA_DATE = SqlInt16.Parse(this.form.CONDITION_VALUE.Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E084", this.form.CONDITION_ITEM.Text);

                            form.CONDITION_VALUE.Focus();

                            LogUtility.DebugMethodEnd(false);
                            return false;
                        }
                    }

                    // 背景色
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.DBFieldsName.Equals(ConstClass.CONTENA_KEIKA_BACK_COLOR))
                    {
                        try
                        {
                            entity.CONTENA_KEIKA_BACK_COLOR = SqlInt16.Parse(this.form.CONDITION_VALUE.Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E012", "背景色は数値");

                            form.CONDITION_VALUE.Focus();

                            LogUtility.DebugMethodEnd(false);
                            return false;
                        }
                    }

                    // 備考
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.DBFieldsName.Equals(ConstClass.CONTENA_KEIKA_BIKOU))
                    {
                        entity.CONTENA_KEIKA_BIKOU = this.form.CONDITION_VALUE.Text;
                    }

                    // 更新者
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.DBFieldsName.Equals("UPDATE_USER"))
                    {
                        entity.UPDATE_USER = this.form.CONDITION_VALUE.Text;
                    }

                    // 更新日
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.DBFieldsName.Equals("UPDATE_DATE"))
                    {
                        entity.SEARCH_UPDATE_DATE = this.form.CONDITION_VALUE.Text.Replace('/', '-');
                    }

                    // 作成者
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.DBFieldsName.Equals("CREATE_USER"))
                    {
                        entity.CREATE_USER = this.form.CONDITION_VALUE.Text;
                    }

                    // 作成日
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.DBFieldsName.Equals("CREATE_DATE"))
                    {
                        entity.SEARCH_CREATE_DATE = this.form.CONDITION_VALUE.Text.Replace('/', '-');
                    }
                }
            }

            this.SearchString = entity;

            LogUtility.DebugMethodEnd(true);

            return true;
        }

        #endregion

        #region データ取得処理

        /// <summary>
        /// 検索取得処理
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

                this.SearchResult = dao.GetIchiranDataSql(this.SearchString, this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
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

        #region 主キーの非活性

        /// <summary>
        /// 主キーが同一の行がDBに存在する場合、主キーを非活性にする
        /// </summary>
        internal bool EditableToPrimaryKey()
        {
            bool ret = true;
            try
            {
                // DBから主キーのListを取得
                var allEntityList = this.dao.GetAllData().Select(s => s.CONTENA_KEIKA_DATE).Where(s => !s.Value.Equals(DBNull.Value)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Cast<DataGridViewRow>().Select(r => r.Cells[ConstClass.CONTENA_KEIKA_DATE]).Where(c => !string.IsNullOrEmpty(Convert.ToString(c.Value))).ToList().
                                        ForEach(c => c.ReadOnly = allEntityList.Contains((Int16)c.Value));
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

        #endregion

        #region システム日付の取得

        /// <summary>
        /// システム日付の取得
        /// </summary>
        /// <returns></returns>
        internal DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }

        #endregion

        /// <summary>
        ///  ヘッダタイトル設定
        /// </summary>
        /// <param name="type">1、受入　2、出荷</param>
        public void HeaderInit(int type)
        {
            ControlUtility controlUtil = new ControlUtility();
            var titleControl = (Label)controlUtil.FindControl(this.parentForm, "lb_title");

            if (type == 1)
            {
                titleControl.Text = "コンテナ無回転日数表示設定";
            }
            else
            {
                titleControl.Text = "コンテナ設置期間表示設定";
            }
        }

    }
}