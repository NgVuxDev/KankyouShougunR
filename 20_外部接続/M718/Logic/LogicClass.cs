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
using Shougun.Core.ExternalConnection.DenshiKeiyakuShinseiKeiroName.APP;
using Shougun.Core.ExternalConnection.DenshiKeiyakuShinseiKeiroName.Dao;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuShinseiKeiroName.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        private readonly string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.DenshiKeiyakuShinseiKeiroName.Setting.ButtonSetting.xml";
        private bool isSelect = false;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// 社内経路名マスタ
        /// </summary>
        private M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME[] entitys;

        /// <summary>
        /// 社内経路名（電子）のdao
        /// </summary>
        private DAOClass dao;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// dtDetailList
        /// </summary>
        private DataTable dtDetailList = new DataTable();

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        internal DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        internal M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME SearchString { get; set; }

        /// <summary>
        /// 登録・更新・削除処理の成否
        /// </summary>
        internal bool isRegist { get; set; }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<DAOClass>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

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
                this.form.CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_VALUE.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M718", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.form.errmessage.MessageBoxShow("E093");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #region LogicalDelete/PhysicalDelete/Regist/Search/Update
        /// <summary>
        /// 論理削除処理
        /// </summary>
        public void LogicalDelete()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (!isSelect)
                {
                    msgLogic.MessageBoxShow("E075", "削除");
                }
                else
                {
                    var result = msgLogic.MessageBoxShow("C021");
                    if (result == DialogResult.Yes)
                    {
                        // トランザクション開始
                        using (var tran = new Transaction())
                        {
                            if (this.entitys != null)
                            {
                                foreach (M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME denshiKeiroShanaiKeiroNameEntity in this.entitys)
                                {
                                    M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME entity = this.dao.GetDataByCd(denshiKeiroShanaiKeiroNameEntity.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.ToString());
                                    if (entity != null)
                                    {
                                        this.dao.Update(denshiKeiroShanaiKeiroNameEntity);
                                    }
                                }
                            }
                            // トランザクション終了
                            tran.Commit();
                        }

                        msgLogic.MessageBoxShow("I001", "削除");
                    }
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
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            try
            {
                //独自チェックの記述例を書く

                this.isRegist = false;
                //エラーではない場合登録処理を行う
                if (errorFlag)
                {
                    return;
                }
                // トランザクション開始
                using (var tran = new Transaction())
                {
                    if (this.entitys != null)
                    {
                        foreach (M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME denshiKeiyakuShanaiKeiroNameEntity in this.entitys)
                        {
                            M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME entity = this.dao.GetDataByCd(denshiKeiyakuShanaiKeiroNameEntity.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.ToString());
                            if (entity == null)
                            {
                                this.dao.Insert(denshiKeiyakuShanaiKeiroNameEntity);
                            }
                            else
                            {
                                this.dao.Update(denshiKeiyakuShanaiKeiroNameEntity);
                            }
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
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        internal bool CreateEntity(bool isDelete, out bool catchErr)
        {
            catchErr = true;
            try
            {
                var entityList = new M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME[this.form.Ichiran.Rows.Count - 1];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME>(entityList);
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
                    if (column.ColumnName.Equals("TIME_STAMP"))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                int count = 0;
                List<M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME> mDenshiKeiyakuShanaiKeiroNameList = new List<M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME>();
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
                            var m_DenshiKeiyakuShanaiKeiroNameEntity = CreateEntityForDataGridRow(this.form.Ichiran.Rows[i]);
                            dataBinderLogic.SetSystemProperty(m_DenshiKeiyakuShanaiKeiroNameEntity, true);
                            m_DenshiKeiyakuShanaiKeiroNameEntity.DELETE_FLG = true;
                            mDenshiKeiyakuShanaiKeiroNameList.Add(m_DenshiKeiyakuShanaiKeiroNameEntity);
                        }
                    }
                }

                if (isDelete)
                {
                    this.entitys = mDenshiKeiyakuShanaiKeiroNameList.ToArray();
                    if (this.entitys.Length == 0 && count == 0)
                    {
                        return false;
                    }
                }
                else
                {
                    // 変更分のみ取得
                    List<M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME> addList = new List<M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME>();
                    if (dt.GetChanges() == null)
                    {
                        this.entitys = new List<M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME>().ToArray();
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

                    var rows = dt.GetChanges().Select("DELETE_FLG = 0 and DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD IS NOT NULL");

                    if (rows.Length == 0)
                    {
                        this.entitys = new List<M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME>().ToArray();
                        return false;
                    }

                    var DenshiKeiyakuShanaiKeiroNameEntityList = CreateEntityForDataGrid(rows);
                    for (int i = 0; i < DenshiKeiyakuShanaiKeiroNameEntityList.Count; i++)
                    {
                        var DenshiKeiyakuShanaiKeiroNameEntity = DenshiKeiyakuShanaiKeiroNameEntityList[i];
                        for (int j = 0; j < this.form.Ichiran.Rows.Count - 1; j++)
                        {
                            bool isFind = false;

                            if (this.form.Ichiran.Rows[j].Cells["DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD"].Value.Equals(DenshiKeiyakuShanaiKeiroNameEntity.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Value) &&
                                bool.Parse(this.form.Ichiran.Rows[j].Cells["DELETE_FLG"].FormattedValue.ToString()) == isDelete)
                            {
                                isFind = true;
                            }

                            if (isFind)
                            {
                                dataBinderLogic.SetSystemProperty(DenshiKeiyakuShanaiKeiroNameEntity, false);
                                addList.Add(DenshiKeiyakuShanaiKeiroNameEntity);
                                break;
                            }
                        }
                        if (0 < addList.Count)
                        {
                            this.entitys = addList.ToArray();
                        }
                        else
                        {
                            this.entitys = new List<M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME>().ToArray();
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245");
                catchErr = false;
                return false;
            }
        }

        /// <summary>
        /// CreateEntityForDataGridRow
        /// </summary>
        internal M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME CreateEntityForDataGridRow(DataGridViewRow row)
        {
            try
            {
                M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME mDenshiKeiyakuShanaiKeiroName = new M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME();

                if (!DBNull.Value.Equals(row.Cells["DELETE_FLG"].Value))
                {
                    mDenshiKeiyakuShanaiKeiroName.DELETE_FLG = (Boolean)row.Cells["DELETE_FLG"].Value;
                }
                else
                {
                    mDenshiKeiyakuShanaiKeiroName.DELETE_FLG = false;
                }

                if (!DBNull.Value.Equals(row.Cells["DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD"].Value))
                {
                    mDenshiKeiyakuShanaiKeiroName.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD = SqlInt16.Parse(row.Cells["DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD"].Value.ToString());
                }

                if (!DBNull.Value.Equals(row.Cells["DENSHI_KEIYAKU_SHANAI_KEIRO_NAME"].Value))
                {
                    mDenshiKeiyakuShanaiKeiroName.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME = (string)row.Cells["DENSHI_KEIYAKU_SHANAI_KEIRO_NAME"].Value;
                }

                if (!DBNull.Value.Equals(row.Cells["DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA"].Value))
                {
                    mDenshiKeiyakuShanaiKeiroName.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA = (string)row.Cells["DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA"].Value;
                }

                if (!DBNull.Value.Equals(row.Cells["CREATE_USER"].Value))
                {
                    mDenshiKeiyakuShanaiKeiroName.CREATE_USER = (string)row.Cells["CREATE_USER"].Value;
                }

                if (!DBNull.Value.Equals(row.Cells["UPDATE_USER"].Value))
                {
                    mDenshiKeiyakuShanaiKeiroName.UPDATE_USER = (string)row.Cells["UPDATE_USER"].Value;
                }

                if (!string.IsNullOrEmpty(row.Cells["UPDATE_DATE"].Value.ToString()))
                {
                    mDenshiKeiyakuShanaiKeiroName.UPDATE_DATE = (DateTime)row.Cells["UPDATE_DATE"].Value;
                }

                if (!string.IsNullOrEmpty(row.Cells["CREATE_DATE"].Value.ToString()))
                {
                    mDenshiKeiyakuShanaiKeiroName.CREATE_DATE = (DateTime)row.Cells["CREATE_DATE"].Value;
                }

                if (!DBNull.Value.Equals(row.Cells["TIME_STAMP"].Value))
                {
                    mDenshiKeiyakuShanaiKeiroName.TIME_STAMP = (byte[])row.Cells["TIME_STAMP"].Value;
                }

                return mDenshiKeiyakuShanaiKeiroName;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// CreateEntityForDataGrid
        /// </summary>
        private List<M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME> CreateEntityForDataGrid(IEnumerable<DataRow> rows)
        {
            try
            {
                var entityList = rows.Select(r => CreateEntityForDataRow(r)).ToList();
                return entityList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// CreateEntityForDataRow
        /// </summary>
        private M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME CreateEntityForDataRow(DataRow row)
        {
            try
            {
                M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME mDenshiKeiyakuShanaiKeiroName = new M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME();

                if (!DBNull.Value.Equals(row["DELETE_FLG"]))
                {
                    mDenshiKeiyakuShanaiKeiroName.DELETE_FLG = (Boolean)row["DELETE_FLG"];
                }
                else
                {
                    mDenshiKeiyakuShanaiKeiroName.DELETE_FLG = false;
                }

                if (!DBNull.Value.Equals(row["DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD"]))
                {
                    mDenshiKeiyakuShanaiKeiroName.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD = SqlInt16.Parse(row["DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD"].ToString());
                }

                if (!DBNull.Value.Equals(row["DENSHI_KEIYAKU_SHANAI_KEIRO_NAME"]))
                {
                    mDenshiKeiyakuShanaiKeiroName.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME = (string)row["DENSHI_KEIYAKU_SHANAI_KEIRO_NAME"];
                }

                if (!DBNull.Value.Equals(row["DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA"]))
                {
                    mDenshiKeiyakuShanaiKeiroName.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA = (string)row["DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA"];
                }

                if (!DBNull.Value.Equals(row["CREATE_USER"]))
                {
                    mDenshiKeiyakuShanaiKeiroName.CREATE_USER = (string)row["CREATE_USER"];
                }

                if (!DBNull.Value.Equals(row["UPDATE_USER"]))
                {
                    mDenshiKeiyakuShanaiKeiroName.UPDATE_USER = (string)row["UPDATE_USER"];
                }

                if (!string.IsNullOrEmpty(row["UPDATE_DATE"].ToString()))
                {
                    mDenshiKeiyakuShanaiKeiroName.UPDATE_DATE = (DateTime)row["UPDATE_DATE"];
                }

                if (!string.IsNullOrEmpty(row["CREATE_DATE"].ToString()))
                {
                    mDenshiKeiyakuShanaiKeiroName.CREATE_DATE = (DateTime)row["CREATE_DATE"];
                }

                if (!DBNull.Value.Equals(row["TIME_STAMP"]))
                {
                    mDenshiKeiyakuShanaiKeiroName.TIME_STAMP = (byte[])row["TIME_STAMP"];
                }

                return mDenshiKeiyakuShanaiKeiroName;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public int Search()
        {
            int count = 0;
            try
            {
                LogUtility.DebugMethodStart();

                SetSearchString();

                this.SearchResult = this.dao.GetIchiranDataSql(this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;

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
                this.form.errmessage.MessageBoxShow("E093");
                count = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245");
                count = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            try
            {
                M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME entity = new M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME();

                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
                {
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                    {
                        //検索条件の設定
                        entity.SetValue(this.form.CONDITION_VALUE);
                    }
                }

                this.SearchString = entity;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSearchString", ex);
                throw;
            }
        }

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal void SetIchiran()
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

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Equals/GetHashCode/ToString

        /// <summary>
        /// クラスが等しいかどうか判定
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            //objがnullか、型が違うときは、等価でない
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            LogicClass localLogic = other as LogicClass;
            return localLogic == null ? false : true;
        }

        /// <summary>
        /// ハッシュコード取得
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 該当するオブジェクトを文字列形式で取得
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (MasterBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
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
            var parentForm = (MasterBaseForm)this.form.Parent;

            //削除ボタン(F4)イベント生成
            this.form.C_MasterRegist(parentForm.bt_func4);
            parentForm.bt_func4.Click += new EventHandler(this.form.LogicalDelete);
            parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

            //CSVボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.CSV);

            //条件取消ボタン(F7)イベント生成
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

        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        private void GetSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                M_SYS_INFO sysInfo = this.daoSysInfo.GetAllDataForCode("0");
                if (sysInfo != null)
                {
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = sysInfo.ICHIRAN_HYOUJI_JOUKEN_DELETED.Value;
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

        /// <summary>
        /// 検索文字列が検索項目に対して不正な文字かのチェックを行う
        /// </summary>
        /// <returns>True：正常　False：不正</returns>
        internal bool CheckSearchString()
        {
            // SetSearchStringメソッド中のentity.SetValueで値によってはFormatでシステムエラーになるため、
            // ここで一度チェックをする。
            // 現在は、経路CDのみエラーが発生するため、経路CDのみチェックを行う。
            // 汎用的に行うのであればSetValueで扱っている全ての型に対してチェックを行う。
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
            return retVal;
        }

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
        }

        #endregion

        /// <summary>
        /// 主キーが同一の行がDBに存在する場合、主キーを非活性にする
        /// </summary>
        internal bool EditableToPrimaryKey()
        {
            try
            {
                // DBから主キーのListを取得
                var allEntityList = this.dao.GetAllData().Select(s => s.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD).Where(s => !s.Value.Equals(DBNull.Value)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Cast<DataGridViewRow>().Select(r => r.Cells["DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD"]).Where(c => !string.IsNullOrEmpty(Convert.ToString(c.Value))).ToList().
                                        ForEach(c => c.ReadOnly = allEntityList.Contains((Int16)c.Value));
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("EditableToPrimaryKey", ex2);
                this.form.errmessage.MessageBoxShow("E093");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditableToPrimaryKey", ex);
                this.form.errmessage.MessageBoxShow("E245");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 社内経路名CDの重複チェック
        /// </summary>
        /// <returns></returns>
        internal bool DuplicationCheck(string strDenshiKeiyakuShanaiKeiroNameCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                // 画面で社内経路CD重複チェック
                int recCount = 0;
                for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
                {
                    string strCD1 = this.form.Ichiran.Rows[i].Cells["DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD"].Value.ToString();
                    if (strCD1.Equals(Convert.ToString(strDenshiKeiyakuShanaiKeiroNameCd)))
                    {
                        recCount++;
                    }
                }

                if (recCount > 1)
                {
                    return true;
                }

                // 検索結果でPK重複チェック
                for (int i = 0; i < dtDetailList.Rows.Count; i++)
                {
                    if (strDenshiKeiyakuShanaiKeiroNameCd.Equals(dtDetailList.Rows[i]["DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD"].ToString()))
                    {
                        return false;
                    }
                }

                // DBでPK重複チェック
                M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME entity = this.dao.GetDataByCd(strDenshiKeiyakuShanaiKeiroNameCd);

                if (entity != null)
                {
                    return true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DuplicationCheck", ex1);
                this.form.errmessage.MessageBoxShow("E093");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.errmessage.MessageBoxShow("E245");
                catchErr = false;
            }
            return false;
        }

        /// <summary>
        /// CSV
        /// </summary>
        internal void CSV()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
            {
                CSVExport csvExport = new CSVExport();
                csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME), this.form);
            }
        }

        /// <summary>
        /// 取り消し初期化
        /// </summary>
        internal void InitCondition()
        {
            try
            {
                this.form.CONDITION_VALUE.Text = string.Empty;
                this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
                this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
                this.form.CONDITION_ITEM.Text = string.Empty;

                // システム情報を取得し、初期値をセットする
                GetSysInfoInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("InitCondition", ex);
                throw;
            }
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        internal bool Cancel()
        {
            bool ret = true;
            try
            {
                InitCondition();
                DataTable dt = new DataTable();
                dt = this.form.Ichiran.DataSource as DataTable;
                if (dt != null)
                {
                    dt.Clear();
                }
                this.form.Ichiran.DataSource = dt;
                SetSearchString();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245");
                ret = false;
            }

            return ret;
        }
    }
}
