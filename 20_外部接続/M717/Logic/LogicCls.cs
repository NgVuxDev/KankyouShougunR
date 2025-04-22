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

namespace Shougun.Core.ExternalConnection.ClientIdNyuuryoku
{
    /// <summary>
    /// クライアントID入力画面のビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.ClientIdNyuuryoku.Setting.ButtonSetting.xml";

        private bool isSelect = false;

        /// <summary>
        /// クライアントID入力画面Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// クライアントID入力のエンティティ
        /// </summary>
        private M_DENSHI_KEIYAKU_CLIENT_ID[] entitys;

        /// <summary>
        /// クライアントIDのDao
        /// </summary>
        private DaoCls dao;

        /// <summary>
        /// 社員のDao
        /// </summary>
        private IM_SHAINDao shainDao;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        private GET_SYSDATEDao dateDao;

        /// <summary>
        /// メッセージクラス
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        internal DataTable SearchResult { get; set; }

        /// <summary>
        /// dtDetailList
        /// </summary>
        private DataTable dtDetailList = new DataTable();

        /// <summary>
        /// 検索条件(Dto)
        /// </summary>
        private DtoCls clientIdDto { get; set; }

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
            this.shainDao = DaoInitUtility.GetComponent<IM_SHAINDao>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            this.clientIdDto = new DtoCls();
            //メッセージ
            this.msgLogic = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        # endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            LogUtility.DebugMethodStart();

            // ボタンのテキストを初期化
            this.ButtonInit();

            // イベントの初期化処理
            this.EventInit();

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
            if (!r_framework.Authority.Manager.CheckAuthority("M717", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
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
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (MasterBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
        }

        #endregion

        #region ボタン設定の読込

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            ButtonSetting[] rtn;

            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();

            rtn = buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);

            return rtn;
        }

        #endregion

        #region イベントの初期化処理

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
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
        }

        #endregion

        #region 検索条件の設定
        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            M_DENSHI_KEIYAKU_CLIENT_ID entityClientId = new M_DENSHI_KEIYAKU_CLIENT_ID();
            M_SHAIN entityShain = new M_SHAIN();

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    bool isExistClientId = this.EntityExistCheck(entityClientId, this.form.CONDITION_VALUE.DBFieldsName);
                    bool isExistShain = this.EntityExistCheck(entityShain, this.form.CONDITION_VALUE.DBFieldsName);

                    if (isExistClientId)
                    {
                        // 検索条件の設定(クライアントIDマスタ)
                        entityClientId.SetValue(this.form.CONDITION_VALUE);
                    }
                    else if (isExistShain)
                    {
                        // 検索条件の設定(社員マスタ)
                        entityShain.SetValue(this.form.CONDITION_VALUE);
                    }
                }
            }
            this.clientIdDto.ClientIdSearchString = entityClientId;
            this.clientIdDto.ShainSearchString = entityShain;
        }

        /// <summary>
        /// Entity内のプロパティに指定プロパティが存在するかチェック
        /// </summary>
        /// <param name="entity">マスタEntity</param>
        /// <param name="dbFieldName">存在チェックしたいプロパティ名</param>
        /// <returns>true:プロパティあり、false:プロパティなし</returns>
        private bool EntityExistCheck(object entity, string dbFieldName)
        {
            bool result = false;

            // マスタEntityのプロパティ取得
            var properties = entity.GetType().GetProperties();

            // プロパティ名検索
            foreach (var property in properties)
            {
                if (property.Name == dbFieldName)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
        #endregion

        #region システム情報を取得し、初期値をセットする
        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        private bool GetSysInfoInit()
        {
            try
            {
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = (bool)sysInfo[0].ICHIRAN_HYOUJI_JOUKEN_DELETED;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetSysInfoInit", ex1);
                this.msgLogic.MessageBoxShow("E093");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSysInfoInit", ex);
                this.msgLogic.MessageBoxShow("E245");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
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
            // dtのスキーマや制約をコピー
            DataTable table = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                DataRow addRow = table.NewRow();

                // カラム情報をコピー
                addRow.ItemArray = row.ItemArray;

                table.Rows.Add(addRow);
            }
            return table;
        }

        #endregion

        #region 更新処理(未使用)
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
        public void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            try
            {
                if (!isSelect)
                {
                    this.msgLogic.MessageBoxShow("E075", "削除");
                }
                else
                {
                    var result = this.msgLogic.MessageBoxShow("C021");
                    if (result == DialogResult.Yes)
                    {
                        using (Transaction tran = new Transaction())
                        {
                            foreach (M_DENSHI_KEIYAKU_CLIENT_ID clientIdEntity in this.entitys)
                            {
                                M_DENSHI_KEIYAKU_CLIENT_ID entity = this.dao.GetDataByCd(clientIdEntity.SHAIN_CD.ToString());
                                if (entity != null)
                                {
                                    this.dao.Update(clientIdEntity);
                                }
                            }
                            tran.Commit();
                        }
                        this.msgLogic.MessageBoxShow("I001", "削除");
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);// 例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); // 排他は警告
                    this.msgLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error(ex); // その他はエラー
                    this.msgLogic.MessageBoxShow("E093");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 物理削除処理(未使用)
        /// <summary>
        /// 物理削除処理
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region CSV出力
        /// <summary>
        /// CSV出力
        /// </summary>
        internal void CSVOutput()
        {
            if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
            {
                CSVExport csvExport = new CSVExport();
                csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_DENSHI_KEIYAKU_CLIENT_ID), this.form);
            }
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

                // プロパティ設定有無チェック
                if (this.clientIdDto.PropertiesUnitExistsCheck())
                {
                    // 社員マスタの条件で取得
                    this.SearchResult = dao.GetIchiranShainDataSql(this.clientIdDto.ShainSearchString, this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }
                else
                {
                    // クライアントIDマスタの条件で取得
                    this.SearchResult = dao.GetIchiranClientIdDataSql(this.clientIdDto.ClientIdSearchString, this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }

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
                this.msgLogic.MessageBoxShow("E093");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.msgLogic.MessageBoxShow("E245");
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
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            //独自チェックの記述例を書く
            try
            {
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    using (Transaction tran = new Transaction())
                    {
                        // トランザクション開始
                        foreach (M_DENSHI_KEIYAKU_CLIENT_ID contenajoukyouEntity in this.entitys)
                        {
                            M_DENSHI_KEIYAKU_CLIENT_ID entity = this.dao.GetDataByCd(contenajoukyouEntity.SHAIN_CD.ToString());
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
                    this.msgLogic.MessageBoxShow("I001", "登録");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    this.msgLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.msgLogic.MessageBoxShow("E093");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 取消処理
        /// <summary>
        /// 取消処理
        /// </summary>
        internal bool Cancel()
        {
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

            return true;
        }

        #endregion

        #region 社員CDの重複チェック
        /// <summary>
        /// 社員CDの重複チェック
        /// </summary>
        /// <param name="shain_Cd">社員CD</param>
        /// <param name="catchErr"></param>
        /// <returns></returns>
        internal bool DuplicationCheck(string shain_Cd, out bool catchErr)
        {
            LogUtility.DebugMethodStart();
            catchErr = true;

            try
            {
                // 画面で社員CD重複チェック
                int recCount = 0;
                for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
                {
                    string strCD = this.form.Ichiran.Rows[i].Cells["SHAIN_CD"].Value.ToString().PadLeft(3, '0');
                    if (strCD.Equals(Convert.ToString(shain_Cd)))
                    {
                        recCount++;
                    }
                }

                if (recCount > 1)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }

                // 検索結果で社員CD重複チェック
                for (int i = 0; i < dtDetailList.Rows.Count; i++)
                {
                    if (shain_Cd.Equals(dtDetailList.Rows[i][1]))
                    {
                        LogUtility.DebugMethodEnd(false, catchErr);
                        return false;
                    }
                }

                // DBで社員CD重複チェック
                M_DENSHI_KEIYAKU_CLIENT_ID entity = this.dao.GetDataByCd(shain_Cd);

                if (entity != null)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DuplicationCheck", ex1);
                this.msgLogic.MessageBoxShow("E093");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.msgLogic.MessageBoxShow("E245");
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
        /// <param name="isDelete"></param>
        /// <param name="catchErr"></param>
        internal bool CreateEntity(bool isDelete, out bool catchErr)
        {
            LogUtility.DebugMethodStart(isDelete);
            catchErr = true;

            try
            {
                var entityList = new M_DENSHI_KEIYAKU_CLIENT_ID[this.form.Ichiran.Rows.Count - 1];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_DENSHI_KEIYAKU_CLIENT_ID();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_DENSHI_KEIYAKU_CLIENT_ID>(entityList);
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

                List<M_DENSHI_KEIYAKU_CLIENT_ID> mClientIdList = new List<M_DENSHI_KEIYAKU_CLIENT_ID>();
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
                            var m_ClientIdEntity = CreateEntityForDataGridRow(this.form.Ichiran.Rows[i]);
                            dataBinderLogic.SetSystemProperty(m_ClientIdEntity, true);
                            m_ClientIdEntity.DELETE_FLG = true;
                            mClientIdList.Add(m_ClientIdEntity);
                        }
                    }
                }

                if (isDelete)
                {
                    this.entitys = mClientIdList.ToArray();
                }
                else
                {
                    // 変更分のみ取得
                    List<M_DENSHI_KEIYAKU_CLIENT_ID> addList = new List<M_DENSHI_KEIYAKU_CLIENT_ID>();

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
                    var clientIdEntityList = CreateEntityForDataGrid(dtChange);
                    for (int i = 0; i < clientIdEntityList.Count; i++)
                    {
                        var clientIdEntity = clientIdEntityList[i];
                        for (int j = 0; j < this.form.Ichiran.Rows.Count - 1; j++)
                        {
                            bool isFind = false;
                            if (this.form.Ichiran.Rows[j].Cells[ConstCls.SHAIN_CD].Value.Equals(Convert.ToString(clientIdEntity.SHAIN_CD)) &&
                                     bool.Parse(this.form.Ichiran.Rows[j].Cells[ConstCls.DELETE_FLG].FormattedValue.ToString()) == isDelete)
                            {
                                isFind = true;
                            }

                            if (isFind)
                            {
                                dataBinderLogic.SetSystemProperty(clientIdEntity, false);

                                addList.Add(clientIdEntity);
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
                this.msgLogic.MessageBoxShow("E245");
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
        /// <param name="gridView"></param>
        /// <returns>entityList</returns>
        private List<M_DENSHI_KEIYAKU_CLIENT_ID> CreateEntityForDataGrid(DataTable gridView)
        {
            LogUtility.DebugMethodStart(gridView);

            var entityList = new List<M_DENSHI_KEIYAKU_CLIENT_ID>();
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
        /// mClientId
        /// </returns>
        private M_DENSHI_KEIYAKU_CLIENT_ID CreateEntityForDataGridRow(DataGridViewRow row)
        {
            LogUtility.DebugMethodStart(row);

            M_DENSHI_KEIYAKU_CLIENT_ID mClientId = new M_DENSHI_KEIYAKU_CLIENT_ID();

            // SHAIN_CD
            if (!DBNull.Value.Equals(row.Cells["SHAIN_CD"].Value))
            {
                mClientId.SHAIN_CD = (string)row.Cells["SHAIN_CD"].Value;
            }

            // DENSHI_KEIYAKU_CLIENT_ID
            if (!DBNull.Value.Equals(row.Cells["DENSHI_KEIYAKU_CLIENT_ID"].Value))
            {
                mClientId.DENSHI_KEIYAKU_CLIENT_ID = (string)row.Cells["DENSHI_KEIYAKU_CLIENT_ID"].Value;
            }

            // DELETE_FLG
            if (!DBNull.Value.Equals(row.Cells["DELETE_FLG"].Value))
            {
                mClientId.DELETE_FLG = (Boolean)row.Cells["DELETE_FLG"].Value;
            }
            else
            {
                mClientId.DELETE_FLG = false;
            }

            // TIME_STAMP
            if (!DBNull.Value.Equals(row.Cells["TIME_STAMP"].Value))
            {
                mClientId.TIME_STAMP = (byte[])row.Cells["TIME_STAMP"].Value;
            }

            LogUtility.DebugMethodEnd(mClientId);
            return mClientId;
        }

        #endregion

        #region CreateEntityForDataRow
        /// <summary>
        /// CreateEntityForDataRow
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private M_DENSHI_KEIYAKU_CLIENT_ID CreateEntityForDataRow(DataRow row)
        {
            LogUtility.DebugMethodStart(row);

            M_DENSHI_KEIYAKU_CLIENT_ID mClientId = new M_DENSHI_KEIYAKU_CLIENT_ID();

            // SHAIN_CD
            if (!DBNull.Value.Equals(row["SHAIN_CD"]))
            {
                mClientId.SHAIN_CD = (string)row["SHAIN_CD"];
            }

            // DENSHI_KEIYAKU_CLIENT_ID
            if (!DBNull.Value.Equals(row["DENSHI_KEIYAKU_CLIENT_ID"]))
            {
                mClientId.DENSHI_KEIYAKU_CLIENT_ID = (string)row["DENSHI_KEIYAKU_CLIENT_ID"];
            }

            // DELETE_FLG
            if (!DBNull.Value.Equals(row["DELETE_FLG"]))
            {
                mClientId.DELETE_FLG = (Boolean)row["DELETE_FLG"];
            }
            else
            {
                mClientId.DELETE_FLG = false;
            }

            // TIME_STAMP
            if (!DBNull.Value.Equals(row["TIME_STAMP"]))
            {
                mClientId.TIME_STAMP = (byte[])row["TIME_STAMP"];
            }

            LogUtility.DebugMethodEnd(mClientId);

            return mClientId;
        }

        #endregion

        #region 登録前のチェック

        /// <summary>
        /// 登録前のチェック
        /// </summary>
        /// <returns></returns>
        internal Boolean CheckBeforeUpdate()
        {
            LogUtility.DebugMethodStart();

            ArrayList errColName = new ArrayList();

            Boolean rtn = false;

            try
            {
                Boolean isErr;

                // 必須入力チェック
                // 社員CD
                isErr = false;
                for (int i = 0; i < this.form.Ichiran.RowCount - 1; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells["chb_delete"].Value != null && this.form.Ichiran.Rows[i].Cells["chb_delete"].Value.ToString() == "True"
                        && (this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value.ToString())))
                    {
                        continue;
                    }

                    if (null == this.form.Ichiran.Rows[i].Cells["SHAIN_CD"].Value ||
                        "".Equals(this.form.Ichiran.Rows[i].Cells["SHAIN_CD"].Value.ToString().Trim()))
                    {
                        if (false == isErr)
                        {
                            errColName.Add("社員CD");
                            isErr = true;
                        }
                        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells["SHAIN_CD"], true);
                    }
                }

                // クライアントID
                isErr = false;
                for (int i = 0; i < this.form.Ichiran.RowCount - 1; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells["chb_delete"].Value != null && this.form.Ichiran.Rows[i].Cells["chb_delete"].Value.ToString() == "True"
                       && (this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value.ToString())))
                    {
                        continue;
                    }

                    if (DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["DENSHI_KEIYAKU_CLIENT_ID"].Value) ||
                        "".Equals(this.form.Ichiran.Rows[i].Cells["DENSHI_KEIYAKU_CLIENT_ID"].Value.ToString().Trim()))
                    {
                        if (false == isErr)
                        {
                            errColName.Add("クライアントID");
                            isErr = true;
                        }
                        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells["DENSHI_KEIYAKU_CLIENT_ID"], true);
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
                this.msgLogic.MessageBoxShow("E245");
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
        internal bool ActionBeforeCheck()
        {
            if (this.form.Ichiran.Rows.Count > 1)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region NOT NULL制約を一時的に解除
        /// <summary>
        /// NOT NULL制約を一時的に解除
        /// </summary>
        internal void ColumnAllowDBNull()
        {
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

        #region その他
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
                this.msgLogic.MessageBoxShow("E001", "表示条件");
                ((CheckBox)sender).Checked = true;
            }
        }

        /// <summary>
        /// 主キーが同一の行がDBに存在する場合、主キーを非活性にする
        /// </summary>
        internal void EditableToPrimaryKey()
        {
            // DBから主キーのListを取得
            var allEntityList = this.dao.GetAllData().Select(s => s.SHAIN_CD).Where(s => !string.IsNullOrEmpty(s)).ToList();

            // DBに存在する行の主キーを非活性にする
            this.form.Ichiran.Rows.Cast<DataGridViewRow>().Select(r => r.Cells["SHAIN_CD"]).Where(c => c.Value != null).ToList().
                                    ForEach(c => c.ReadOnly = allEntityList.Contains(c.Value.ToString()));
        }
        #endregion
    }
}