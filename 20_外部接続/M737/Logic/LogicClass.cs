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

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuShoruiInfo
{
    /// <summary>
    /// 書類情報入力画面のビジネスロジック
    /// </summary>
    public class LogicClass : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.DenshiKeiyakuShoruiInfo.Setting.ButtonSetting.xml";

        private bool isSelect = false;

        /// <summary>
        /// 書類情報入力画面Form
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
        /// 書類情報入力のエンティティ
        /// </summary>
        private M_DENSHI_KEIYAKU_SHORUI_INFO[] entitys;

        /// <summary>
        /// 書類情報入力のDao
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

        /// <summary>
        /// メッセージ出力用のユーティリティ
        /// </summary>
        private MessageUtility MessageUtil;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        internal DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        private M_DENSHI_KEIYAKU_SHORUI_INFO SearchString { get; set; }

        #endregion

        #region 初期化処理

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            // メッセージ出力用のユーティリティ
            MessageUtil = new MessageUtility();
            this.dao = DaoInitUtility.GetComponent<DAOClass>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            LogUtility.DebugMethodEnd(targetForm);
        }

        # endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal void WindowInit()
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

                // システム情報を取得し、初期値をセットする
                //GetSysInfoInit();

                // 処理No（ESC)を入力不可にする
                this.parentForm.txb_process.Enabled = false;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    // システム情報を取得し、初期値をセットする
                    this.GetSysInfoInit();
                }

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M737", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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
                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (MasterBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
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
                var buttonSetting = new ButtonSetting();
                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
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
                var parentForm = (MasterBaseForm)this.form.Parent;

                // CSVボタン(F6)イベント生成
                parentForm.bt_func6.Click += new EventHandler(this.form.bt_func6_Click);

                // 検索ボタン(F8)イベント生成
                parentForm.bt_func8.Click += new EventHandler(this.form.Search);

                // 登録ボタン(F9)イベント生成
                this.form.C_MasterRegist(parentForm.bt_func9);
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                // 取消ボタン(F11)イベント生成
                parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

                // 閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
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

        #region 検索条件の設定

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private bool SetSearchString()
        {
            try
            {
                M_DENSHI_KEIYAKU_SHORUI_INFO entity = new M_DENSHI_KEIYAKU_SHORUI_INFO();

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
        }

        #endregion

        #region 検索結果を一覧に設定

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        private void SetIchiran()
        {
            try
            {
                var table = this.SearchResult;

                table.BeginLoadData();

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.form.Ichiran.DataSource = table;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                throw;
            }
        }

        #endregion

        #region システム情報を取得し、初期値をセットする

        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        internal void GetSysInfoInit()
        {
            try
            {
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
        }

        #endregion

        #endregion

        #region 業務処理

        #region CSV出力

        /// <summary>
        /// CSV出力
        /// </summary>
        private void CSVOutput()
        {
            try
            {
                if (this.form.msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_DENSHI_KEIYAKU_SHORUI_INFO), this.form);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVOutput", ex);
                throw;
            }
        }

        #endregion

        #region 条件取消

        /// <summary>
        /// 条件取消
        /// </summary>
        private void CancelCondition()
        {
            try
            {
                ClearCondition();
                SetIchiran();
            }
            catch (Exception ex)
            {
                LogUtility.Error("CancelCondition", ex);
                throw;
            }
        }

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        internal void ClearCondition()
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
                // エラーの場合、０件を戻る
                if (!SetSearchString())
                {
                    return 0;
                }

                this.SearchResult = dao.GetIchiranDataSql(this.SearchString, this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                if (this.SearchResult.Rows != null && this.SearchResult.Rows.Count > 0)
                {
                    count = 1;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.form.msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
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
                            foreach (M_DENSHI_KEIYAKU_SHORUI_INFO contenashuruiEntity in this.entitys)
                            {
                                M_DENSHI_KEIYAKU_SHORUI_INFO entity = this.dao.GetDataByCd(contenashuruiEntity.SHORUI_INFO_ID.ToString());
                                if (entity == null)
                                {
                                    this.dao.Insert(contenashuruiEntity);
                                }
                                else
                                {
                                    this.dao.Update(contenashuruiEntity);
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
                    this.form.msgLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.form.msgLogic.MessageBoxShow("E093");
                }
                return false;
            }
            return true;
        }

        #endregion

        #region 取消処理

        /// <summary>
        /// 取消処理
        /// </summary>
        internal void Cancel()
        {
            try
            {
                // システム情報を取得し、初期値をセットする
                GetSysInfoInit();

                // 検索項目を初期値にセットする
                this.form.CONDITION_VALUE.Text = "";
                this.form.CONDITION_VALUE.DBFieldsName = "";
                this.form.CONDITION_VALUE.ItemDefinedTypes = "";
                this.form.CONDITION_ITEM.Text = "";
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                throw;
            }
        }

        #endregion

        #region コントロールから対象のEntityを作成する

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        internal bool CreateEntity(bool isDelete)
        {
            try
            {
                var entityList = new M_DENSHI_KEIYAKU_SHORUI_INFO[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_DENSHI_KEIYAKU_SHORUI_INFO();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_DENSHI_KEIYAKU_SHORUI_INFO>(entityList);
                DataTable dt = this.form.Ichiran.DataSource as DataTable;

                if (dt == null || dt.Rows.Count == 0) { return false; }

                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(ConstClass.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                List<M_DENSHI_KEIYAKU_SHORUI_INFO> mContenaShuruiList = new List<M_DENSHI_KEIYAKU_SHORUI_INFO>();
                // 変更分のみ取得
                List<M_DENSHI_KEIYAKU_SHORUI_INFO> addList = new List<M_DENSHI_KEIYAKU_SHORUI_INFO>();
                if (dt.GetChanges() == null)
                {
                    this.entitys = new List<M_DENSHI_KEIYAKU_SHORUI_INFO>().ToArray();
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

                if (dt.GetChanges() == null) { return true; }

                // 変更したデータ取得
                var rows = dt.GetChanges().Select("DELETE_FLG = 0");

                // データ変更なし
                if (rows.Length == 0)
                {
                    this.entitys = new List<M_DENSHI_KEIYAKU_SHORUI_INFO>().ToArray();
                    return false;
                }

                var contenashuruiEntityList = CreateEntityForDataGrid(rows);
                for (int i = 0; i < contenashuruiEntityList.Count; i++)
                {
                    var contenashuruiEntity = contenashuruiEntityList[i];
                    for (int j = 0; j < this.form.Ichiran.Rows.Count; j++)
                    {
                        bool isFind = false;
                        if (this.form.Ichiran.Rows[j].Cells[ConstClass.SHORUI_INFO_ID].Value.ToString().Equals(Convert.ToString(contenashuruiEntity.SHORUI_INFO_ID)) &&
                                 bool.Parse(this.form.Ichiran.Rows[j].Cells[ConstClass.DELETE_FLG].FormattedValue.ToString()) == isDelete)
                        {
                            isFind = true;
                        }

                        if (isFind)
                        {
                            dataBinderLogic.SetSystemProperty(contenashuruiEntity, false);
                            var dataBinderEntry = new DataBinderLogic<M_DENSHI_KEIYAKU_SHORUI_INFO>(contenashuruiEntity);
                            dataBinderEntry.SetSystemProperty(contenashuruiEntity, false);
                            addList.Add(contenashuruiEntity);
                            break;
                        }
                    }
                    if (addList.Count > 0)
                    {
                        this.entitys = addList.ToArray();
                    }
                    else
                    {
                        this.entitys = new List<M_DENSHI_KEIYAKU_SHORUI_INFO>().ToArray();
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                throw;
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
        private List<M_DENSHI_KEIYAKU_SHORUI_INFO> CreateEntityForDataGrid(IEnumerable<DataRow> rows)
        {
            try
            {
                var entityList = rows.Select(r => CreateEntityForDataGridRow(r)).ToList();

                return entityList;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityForDataGrid", ex);
                throw;
            }
        }

        #endregion

        #region CreateEntityForDataGridRow

        /// <summary>
        /// CreateEntityForDataGridRow
        /// </summary>
        /// <returns>
        /// mContenaShurui
        /// </returns>
        private M_DENSHI_KEIYAKU_SHORUI_INFO CreateEntityForDataGridRow(DataRow row)
        {
            try
            {
                M_DENSHI_KEIYAKU_SHORUI_INFO mContenaShurui = new M_DENSHI_KEIYAKU_SHORUI_INFO();

                // SHORUI_INFO_ID
                if (!DBNull.Value.Equals(row.Field<string>(ConstClass.SHORUI_INFO_ID)))
                {
                    mContenaShurui.SHORUI_INFO_ID = row.Field<string>(ConstClass.SHORUI_INFO_ID);
                }

                // SHORUI_INFO_NAME
                if (!DBNull.Value.Equals(row.Field<string>("SHORUI_INFO_NAME")))
                {
                    mContenaShurui.SHORUI_INFO_NAME = row.Field<string>("SHORUI_INFO_NAME");
                }

                // DELETE_FLG
                if (!DBNull.Value.Equals(row.Field<bool>("DELETE_FLG")))
                {
                    mContenaShurui.DELETE_FLG = row.Field<bool>("DELETE_FLG");
                }
                else
                {
                    mContenaShurui.DELETE_FLG = false;
                }

                // TIME_STAMP
                if (!DBNull.Value.Equals(row.Field<byte[]>("TIME_STAMP")))
                {
                    mContenaShurui.TIME_STAMP = row.Field<byte[]>("TIME_STAMP");
                }
                return mContenaShurui;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityForDataGridRow", ex);
                throw;
            }
        }

        #endregion

        #region Mulit行メッセージを生成

        /// <summary>
        /// Mulit行メッセージを生成
        /// </summary>
        /// <param name="msgID">メッセージID</param>
        /// <param name="str">整形時に利用する文言のリスト</param>
        /// <returns>整形済みメッセージ</returns>
        private string CreateMulitMessage(string msgID, params string[] str)
        {
            // 整形済みメッセージ
            string msgResult = string.Empty;

            try
            {
                // メッセージ原本
                MessageUtil = new MessageUtility();
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

                return msgResult;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
        }

        #endregion

        #region DataGridViewデータ件数チェック処理

        /// <summary>
        /// DataGridViewデータ件数チェック処理
        /// </summary>
        internal bool ActionBeforeCheck()
        {
            try
            {
                if (this.form.Ichiran.Rows.Count > 1) { return false; }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ActionBeforeCheck", ex);
                throw;
            }
        }

        #endregion

        #region NOT NULL制約を一時的に解除

        /// <summary>
        /// NOT NULL制約を一時的に解除
        /// </summary>
        internal void ColumnAllowDBNull()
        {
            try
            {
                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(ConstClass.TIME_STAMP))
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
        }

        #endregion

        #region レコード選択チェック処理

        /// <summary>
        /// レコード選択チェック処理
        /// </summary>
        internal bool isSelectFlg()
        {
            try
            {
                if (!isSelect) { return false; }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("isSelectFlg", ex);
                throw;
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

        #region 主キー非活性処理

        /// <summary>
        /// 主キーが同一の行がDBに存在する場合、主キーを非活性にする
        /// </summary>
        internal bool EditableToPrimaryKey()
        {
            bool ret = true;
            try
            {
                // DBから主キーのListを取得
                var allEntityList = this.dao.GetAllData().Select(s => s.SHORUI_INFO_ID.ToString()).Where(s => !string.IsNullOrEmpty(s)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Cast<DataGridViewRow>().Select(r => r.Cells["SHORUI_INFO_ID"]).Where(c => c.Value != null).ToList().
                                        ForEach(c => c.ReadOnly = allEntityList.Contains(c.Value.ToString()));
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("EditableToPrimaryKey", ex1);
                this.form.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditableToPrimaryKey", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #endregion

        #region 未使用

        public virtual void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public virtual void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public virtual void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}