// $Id: DenManiHaikiShuruiHoshuLogic.cs 50187 2015-05-20 08:27:22Z takeda $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DenManiHaikiShuruiHoshu.APP;
using DenManiHaikiShuruiHoshu.Validator;
using GrapeCity.Win.MultiRow;
using MasterCommon.Logic;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;

namespace DenManiHaikiShuruiHoshu.Logic
{
    /// <summary>
    /// 社員保守画面のビジネスロジック
    /// </summary>
    public class DenManiHaikiShuruiHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "DenManiHaikiShuruiHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_INPUTCD_DATA_HAIKI_SHURUI_SQL = "DenManiHaikiShuruiHoshu.Sql.GetInputCddataDenManiHaikiShuruiSql.sql";

        private readonly string GET_ICHIRAN_HAIKI_SHURUI_DATA_SQL = "DenManiHaikiShuruiHoshu.Sql.GetIchiranDataSql.sql";

        private readonly string GET_HAIKI_SHURUI_DATA_SQL = "DenManiHaikiShuruiHoshu.Sql.GetDenManiHaikiShuruiDataSql.sql";

        private readonly string CHECK_DELETE_HAIKI_SHURUI_SQL = "DenManiHaikiShuruiHoshu.Sql.CheckDeleteDenManiHaikiShuruiSql.sql";

        /// <summary>
        /// 電マニ廃棄種類保守画面Form
        /// </summary>
        private DenManiHaikiShuruiHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_DENSHI_HAIKI_SHURUI[] entitys;

        private bool isAllSearch;

        /// <summary>
        /// 電子廃棄種類のDao
        /// </summary>
        private IM_DENSHI_HAIKI_SHURUIDao dao;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// システム設定のエンティティ
        /// </summary>
        private M_SYS_INFO entitySysInfo;

        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
        internal MasterBaseForm parentForm;
        // 20150922 katen #12048 「システム日付」の基準作成、適用 end

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索結果(重複チェック用)
        /// </summary>
        public DataTable SearchResultCheck { get; set; }

        /// <summary>
        /// 検索結果(全件)
        /// </summary>
        public DataTable SearchResultAll { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_DENSHI_HAIKI_SHURUI SearchString { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public DenManiHaikiShuruiHoshuLogic(DenManiHaikiShuruiHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_DENSHI_HAIKI_SHURUIDao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            this.entitySysInfo = null;
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            if (sysInfo != null && sysInfo.Length > 0)
            {
                this.entitySysInfo = sysInfo[0];
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                this.parentForm = (MasterBaseForm)this.form.Parent;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                //QN_QUAN add 20220225 #160799 S
                var titleControl = (Label)this.parentForm.controlUtil.FindControl(parentForm, "lb_title");
                if (titleControl != null)
                {
                    titleControl.Text = titleControl.Text.Replace("電子", "").Trim();
                }
                //QN_QUAN add 20220225 #160799 S
                // ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;

                this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_VALUE.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;

                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 表示条件初期値設定処理
        /// </summary>
        public void SetHyoujiJoukenInit()
        {
            LogUtility.DebugMethodStart();

            if (this.entitySysInfo != null)
            {
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = this.entitySysInfo.ICHIRAN_HYOUJI_JOUKEN_DELETED.Value;
            }
            else

                LogUtility.DebugMethodEnd();
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
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                SetSearchString();

                this.SearchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_HAIKI_SHURUI_DATA_SQL
                                                                       , this.SearchString
                                                                       , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                this.SearchResultCheck = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_HAIKI_SHURUI_DATA_SQL
                                                                       , this.SearchString
                                                                       , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);

                this.SearchResultAll = dao.GetDataBySqlFile(this.GET_HAIKI_SHURUI_DATA_SQL, new M_DENSHI_HAIKI_SHURUI());

                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

                Properties.Settings.Default.Save();

                int count = this.SearchResult.Rows == null ? 0 : 1;

                LogUtility.DebugMethodEnd(count);
                return count;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Search", ex2);
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
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete)
        {
            try
            {
                LogUtility.DebugMethodStart(isDelete);

                var entityList = new M_DENSHI_HAIKI_SHURUI[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_DENSHI_HAIKI_SHURUI();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_DENSHI_HAIKI_SHURUI>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.DenManiHaikiShuruiHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }
                dt.BeginLoadData();
                preDt = GetCloneDataTable(dt);

                // 変更分のみ取得
                this.form.Ichiran.DataSource = dt.GetChanges();
                var denybEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<M_DENSHI_HAIKI_SHURUI> addList = new List<M_DENSHI_HAIKI_SHURUI>();
                foreach (var denybEntity in denybEntityList)
                {
                    foreach (Row row in this.form.Ichiran.Rows)
                    {
                        if (row.Cells.Any(n => (n.DataField.Equals(Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_CD) && n.Value.ToString().Equals(denybEntity.HAIKI_SHURUI_CD))) &&
                            row.Cells.Any(n => (n.DataField.Equals(Const.DenManiHaikiShuruiHoshuConstans.DELETE_FLG) && bool.Parse(n.FormattedValue.ToString()) == isDelete)))
                        {
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), denybEntity);
                            addList.Add(denybEntity);
                            break;
                        }
                    }
                }

                this.form.Ichiran.DataSource = preDt;
                this.entitys = addList.ToArray();

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            try
            {
                LogUtility.DebugMethodStart();

                ClearCondition();
                SetSearchString();

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 廃棄物種類CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                DenManiHaikiShuruiHoshuValidator vali = new DenManiHaikiShuruiHoshuValidator();
                bool result = vali.HaikiShuruiCDValidator(this.form.Ichiran, this.SearchResultCheck, this.SearchResultAll, this.isAllSearch);

                LogUtility.DebugMethodEnd();

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        public bool Preview()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("C011", "廃棄物種類一覧表");

                MessageBox.Show("未実装");

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Preview", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// CSV
        /// </summary>
        public bool CSV()
        {
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    MultiRowIndexCreateLogic multirowLocationLogic = new MultiRowIndexCreateLogic();
                    multirowLocationLogic.multiRow = this.form.Ichiran;

                    multirowLocationLogic.CreateLocations();

                    CSVFileLogic csvLogic = new CSVFileLogic();

                    csvLogic.MultirowLocation = multirowLocationLogic.sortEndList;

                    csvLogic.Detail = this.form.Ichiran;

                    WINDOW_ID id = this.form.WindowId;

                    csvLogic.FileName = id.ToTitleString();
                    csvLogic.headerOutputFlag = true;

                    csvLogic.CreateCSVFile(this.form);
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSV", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        public bool CancelCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();

                ClearCondition();
                SetSearchString();

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CancelCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        #region 登録/更新/削除

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

                //独自チェックの記述例を書く
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (M_DENSHI_HAIKI_SHURUI haikiShuruiEntity in this.entitys)
                        {
                            // 廃棄物種類CDでDB検索(廃棄物種類マスタ)
                            M_DENSHI_HAIKI_SHURUI denybSearchString = new M_DENSHI_HAIKI_SHURUI();
                            denybSearchString.HAIKI_SHURUI_CD = haikiShuruiEntity.HAIKI_SHURUI_CD;
                            DataTable haikiShuruiTable = this.dao.GetDataBySqlFile(GET_INPUTCD_DATA_HAIKI_SHURUI_SQL, denybSearchString);

                            if (haikiShuruiTable.Rows.Count == 0)
                            {
                                // 削除チェックが付けられている場合は、新規登録を行わない
                                if (haikiShuruiEntity.DELETE_FLG)
                                {
                                    continue;
                                }
                                this.dao.Insert(haikiShuruiEntity);
                            }
                            else
                            {
                                this.dao.Update(haikiShuruiEntity);
                            }

                            UpdateRelationInfo(haikiShuruiEntity, false);
                        }
                        // トランザクション終了
                        tran.Commit();
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("I001", "登録");
                }
                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
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
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var result = msgLogic.MessageBoxShow("C021");
                if (result == DialogResult.Yes)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (M_DENSHI_HAIKI_SHURUI haikiShuruiEntity in this.entitys)
                        {
                            DataTable haikiShuruiTable = this.dao.GetDataBySqlFile(GET_INPUTCD_DATA_HAIKI_SHURUI_SQL, haikiShuruiEntity);
                            if (haikiShuruiTable.Rows.Count > 0)
                            {
                                this.dao.Update(haikiShuruiEntity);
                                UpdateRelationInfo(haikiShuruiEntity, true);
                            }
                        }
                        // トランザクション終了
                        tran.Commit();
                    }

                    msgLogic.MessageBoxShow("I001", "削除");
                }

                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
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
                var cd = string.Empty;
                string[] strList;

                foreach (var row in this.form.Ichiran.Rows)
                {
                    if (row.Cells["DELETE_FLG"].Value != null && row.Cells["DELETE_FLG"].Value.ToString().Equals("True"))
                    {
                        if (row.Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(row.Cells["CREATE_USER"].Value.ToString()))
                        {
                            continue;
                        }
                        cd += row.Cells["HAIKI_SHURUI_CD"].Value.ToString() + ",";
                    }
                }

                if (!string.IsNullOrEmpty(cd))
                {
                    cd = cd.Substring(0, cd.Length - 1);
                    strList = cd.Split(',');
                    DataTable dtTable = this.dao.GetDataBySqlFileCheck(this.CHECK_DELETE_HAIKI_SHURUI_SQL, strList);
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        string strName = string.Empty;

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            strName += "\n" + dr["NAME"].ToString();
                        }

                        new MessageBoxShowLogic().MessageBoxShow("E258", "電子廃棄物種類", "廃棄物種類CD", strName);

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

            DenManiHaikiShuruiHoshuLogic localLogic = other as DenManiHaikiShuruiHoshuLogic;
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
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetIchiran()
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

                // テーブル固定値定義書に存在するデータの場合、削除、名称、略称、適用期間を変更不可に修正
                this.SetFixedIchiran();

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M320", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, true);
                }
                else
                {
                    this.DispReferenceMode();
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
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
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (MasterBaseForm)this.form.Parent;

            //詳細ボタン(F1)イベント生成
            parentForm.bt_func1.Click += new EventHandler(this.form.ShowSaibunrui);

            //削除ボタン(F4)イベント生成
            this.form.C_MasterRegist(parentForm.bt_func4);
            parentForm.bt_func4.Click += new EventHandler(this.form.LogicalDelete);
            parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

            //ﾌﾟﾚﾋﾞｭｰボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.Preview);

            //CSVボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.CSV);

            //条件取消ボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.CancelCondition);

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

            //表示時処理
            this.form.Shown += new System.EventHandler(this.form.Form_Shown);
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
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            M_DENSHI_HAIKI_SHURUI entity = new M_DENSHI_HAIKI_SHURUI();

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    // 検索条件の設定
                    entity.SetValue(this.form.CONDITION_VALUE);
                }
            }
            this.SearchString = entity;
        }

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

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        private void ClearCondition()
        {
            this.form.CONDITION_VALUE.Text = string.Empty;
            this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
            this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
            this.form.CONDITION_ITEM.Text = string.Empty;

            this.SetHyoujiJoukenInit();
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);
        }

        /// <summary>
        /// 電マニ廃棄種類保守に関連する情報の更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isDelete"></param>
        private void UpdateRelationInfo(M_DENSHI_HAIKI_SHURUI entity, bool isDelete)
        {
            if (entity == null
                || string.IsNullOrEmpty(entity.HAIKI_SHURUI_CD))
            {
                return;
            }
        }

        /// <summary>
        /// テーブル固定値定義書に存在するデータの場合、削除、名称、略称、適用期間を変更不可にする
        /// </summary>
        internal bool SetFixedIchiran()
        {
            try
            {
                foreach (Row row in this.form.Ichiran.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        this.SetFixedRow(row);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetFixedIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// テーブル固定値の変更不可処理を行います
        /// </summary>
        /// <param name="eRow">変更不可処理を行うための判定データが入力されているRow</param>
        internal void SetFixedRow(Row row)
        {
            var objValue = row[Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_CD].Value;

            if (this.CheckFixedRow(row))
            {
                foreach (var columnName in Const.DenManiHaikiShuruiHoshuConstans.fixedColumnList)
                {
                    row[columnName].ReadOnly = true;
                    row[columnName].Selectable = false;
                    row[columnName].UpdateBackColor(false);
                }
            }
        }

        /// <summary>
        /// テーブル固定値のデータかどうかを調べる
        /// </summary>
        /// <param name="eRow">行データ</param>
        /// <returns>結果</returns>
        internal bool CheckFixedRow(Row row)
        {
            var objValue = row[Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_CD].Value;

            if (row != null && objValue != null)
            {
                var strCd = objValue.ToString();
                if (Const.DenManiHaikiShuruiHoshuConstans.fixedRowList.Contains(strCd))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 区分設定処理
        /// </summary>
        /// <param name="rowIdx"></param>
        public bool SetHaikiKbn(int rowIdx)
        {
            try
            {
                int shuruiCd;
                if (this.form.Ichiran[rowIdx, Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_CD] != null)
                {
                    if (int.TryParse(Convert.ToString(this.form.Ichiran[rowIdx, Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_CD].Value), out shuruiCd))
                    {
                        if (shuruiCd <= 1999)
                        {
                            this.form.Ichiran[rowIdx, "HAIKI_KBN"].Value = "1";
                        }
                        else if (shuruiCd <= 4999)
                        {
                            this.form.Ichiran[rowIdx, "HAIKI_KBN"].Value = "2";
                        }
                        else if (shuruiCd <= 5999)
                        {
                            this.form.Ichiran[rowIdx, "HAIKI_KBN"].Value = "4";
                        }
                        else if (shuruiCd <= 6999)
                        {
                            this.form.Ichiran[rowIdx, "HAIKI_KBN"].Value = "6";
                        }
                        else if (shuruiCd <= 7499)
                        {
                            this.form.Ichiran[rowIdx, "HAIKI_KBN"].Value = "3";
                        }
                        else
                        {
                            this.form.Ichiran[rowIdx, "HAIKI_KBN"].Value = "5";
                        }
                    }
                    else
                    {
                        this.form.Ichiran[rowIdx, "HAIKI_KBN"].Value = DBNull.Value;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHaikiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 分類設定処理
        /// </summary>
        /// <param name="rowIdx"></param>
        public bool SetHaikiBunrui(int rowIdx)
        {
            try
            {
                if (!this.form.Ichiran.Rows[rowIdx].IsNewRow && this.form.Ichiran[rowIdx, Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_CD] != null)
                {
                    string shuruiCd = Convert.ToString(this.form.Ichiran[rowIdx, Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_CD].Value);

                    if (!string.IsNullOrWhiteSpace(shuruiCd))
                    {
                        if (shuruiCd.Substring(2).Equals("00"))
                        {
                            this.form.Ichiran[rowIdx, "HAIKI_BUNRUI"].Value = "1";
                        }
                        else if (shuruiCd.Substring(3).Equals("0"))
                        {
                            this.form.Ichiran[rowIdx, "HAIKI_BUNRUI"].Value = "2";
                        }
                        else
                        {
                            this.form.Ichiran[rowIdx, "HAIKI_BUNRUI"].Value = "3";
                        }
                    }
                    else
                    {
                        this.form.Ichiran[rowIdx, "HAIKI_BUNRUI"].Value = DBNull.Value;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHaikiBunrui", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 区分設定処理
        /// </summary>
        /// <param name="e"></param>
        public bool SetHaikiKbnName(CellFormattingEventArgs e)
        {
            try
            {
                if (e.Value != null)
                {
                    switch (e.Value.ToString())
                    {
                        case "1":
                            e.Value = "普通の産廃";
                            break;

                        case "2":
                            e.Value = "不可分一体";
                            break;

                        case "3":
                            e.Value = "特別管理型";
                            break;

                        case "4":
                            e.Value = "特定産業廃棄物";
                            break;

                        case "5":
                            e.Value = "特定産業廃棄物（特別管理型）";
                            break;

                        case "6":
                            e.Value = "事業系一般廃棄物";
                            break;

                        default:
                            e.Value = string.Empty;
                            break;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHaikiKbnName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 分類設定処理
        /// </summary>
        /// <param name="e"></param>
        public bool SetHaikiBunruiName(CellFormattingEventArgs e)
        {
            try
            {
                if (e.Value != null)
                {
                    switch (e.Value.ToString())
                    {
                        case "1":
                            e.Value = "大分類";
                            break;

                        case "2":
                            e.Value = "中分類";
                            break;

                        case "3":
                            e.Value = "小分類";
                            break;

                        default:
                            e.Value = string.Empty;
                            break;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHaikiBunruiName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #region 主キーの入力制限

        /// <summary>
        /// 主キーが同一の行がDBに存在する場合、主キーを非活性にする
        /// </summary>
        internal bool EditableToPrimaryKey()
        {
            try
            {
                // DBから主キーのListを取得
                var allEntityList = this.dao.GetAllData().Select(s => s.HAIKI_SHURUI_CD).Where(s => !string.IsNullOrEmpty(s)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Select(r => r.Cells["HAIKI_SHURUI_CD"]).Where(c => c.Value != null).ToList().
                                            ForEach(c =>
                                            {
                                                c.ReadOnly = allEntityList.Contains(c.Value.ToString());
                                                c.UpdateBackColor(false);
                                            });
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditableToPrimaryKey", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion
    }
}