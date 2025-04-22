// $Id: SharyouHoshuLogic.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
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
using SharyouHoshu.APP;
using SharyouHoshu.Validator;
using Shougun.Core.Common.BusinessCommon;

namespace SharyouHoshu.Logic
{
    /// <summary>
    /// 車輌保守画面のビジネスロジック
    /// </summary>
    public class SharyouHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "SharyouHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_SHARYOU_DATA_SQL = "SharyouHoshu.Sql.GetIchiranDataSql.sql";

        private readonly string GET_SHARYOU_DATA_SQL = "SharyouHoshu.Sql.GetSharyouDataSql.sql";

        private readonly string GET_GYOUSHA_DATA_SQL = "SharyouHoshu.Sql.GetGyoushaDataSql.sql";

        private readonly string CHECK_DELETE_SHARYOU_SQL = "SharyouHoshu.Sql.CheckDeleteSharyouSql.sql";

        /// <summary>
        /// 車輌保守画面Form
        /// </summary>
        private SharyouHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_SHARYOU[] entitys;

        private bool isAllSearch;

        /// <summary>
        /// 車輌のDao
        /// </summary>
        private IM_SHARYOUDao dao;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// IM_SHAINDao
        /// </summary>
        private IM_SHAINDao shainDao;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// システム設定のエンティティ
        /// </summary>
        private M_SYS_INFO entitySysInfo;

        /// <summary>
        /// 取得した社員エンティティを保持する
        /// </summary>
        private List<M_SHAIN> shainList = new List<M_SHAIN>();

        // VUNGUYEN 20150525 #1294 START
        public Cell cell;
        // VUNGUYEN 20150525 #1294 END

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
        public M_SHARYOU SearchString { get; set; }

        /// <summary>
        /// 検索結果(業者)
        /// </summary>
        public DataTable SearchResultGyousha { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public SharyouHoshuLogic(SharyouHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.shainDao = DaoInitUtility.GetComponent<IM_SHAINDao>();

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

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;

                this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_VALUE.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;
                this.form.GYOUSHA_CD.Text = Properties.Settings.Default.GyoushaValue_Text.Trim();
                this.form.GYOUSHA_NAME_RYAKU.Text = Properties.Settings.Default.GyoushaName_Text;

                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

                this.form.Ichiran.AllowUserToAddRows = false;

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
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

                this.SearchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_SHARYOU_DATA_SQL
                                                            , this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                this.SearchResultCheck = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_SHARYOU_DATA_SQL
                                            , this.SearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);

                //現在の業者データに変更
                M_SHARYOU searchParams = new M_SHARYOU();
                searchParams.GYOUSHA_CD = this.SearchString.GYOUSHA_CD;
                this.SearchResultAll = dao.GetDataBySqlFile(this.GET_SHARYOU_DATA_SQL, searchParams);
                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;
                Properties.Settings.Default.GyoushaValue_Text = this.form.GYOUSHA_CD.Text;
                Properties.Settings.Default.GyoushaName_Text = this.form.GYOUSHA_NAME_RYAKU.Text;

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

                var entityList = new M_SHARYOU[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_SHARYOU();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_SHARYOU>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.SharyouHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                preDt = GetCloneDataTable(dt);

                // 元の値から全く変化がなければ、 RowState を元の状態に戻す
                foreach (DataRow row in dt.Rows)
                {
                    if (!DataTableUtility.IsDataRowChanged(row))
                    {
                        row.AcceptChanges();
                    }
                }

                // 変更分のみ取得
                this.form.Ichiran.DataSource = dt.GetChanges();

                var kansanEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<M_SHARYOU> addList = new List<M_SHARYOU>();
                foreach (var kansanEntity in kansanEntityList)
                {
                    foreach (Row row in this.form.Ichiran.Rows)
                    {
                        if (row.Cells.Any(n => (n.DataField.Equals(Const.SharyouHoshuConstans.SHARYOU_CD) && n.Value.ToString().Equals(kansanEntity.SHARYOU_CD))) &&
                            row.Cells.Any(n => (n.DataField.Equals(Const.SharyouHoshuConstans.DELETE_FLG) && bool.Parse(n.FormattedValue.ToString()) == isDelete)))
                        {
                            kansanEntity.SetValue(this.form.GYOUSHA_CD);
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), kansanEntity);
                            addList.Add(kansanEntity);
                            break;
                        }
                    }
                }

                this.form.Ichiran.DataSource = preDt;

                this.entitys = addList.ToArray();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
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

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 車輌の重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                SharyouHoshuValidator vali = new SharyouHoshuValidator();
                bool result = vali.SharyouCDValidator(this.form.Ichiran, this.SearchResultCheck, this.SearchResultAll, this.isAllSearch);

                LogUtility.DebugMethodEnd(result);

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
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
                var sharyouCd = string.Empty;
                string[] strList;

                foreach (Row gcRwos in this.form.Ichiran.Rows)
                {
                    if (gcRwos.Cells["DELETE_FLG"].Value != null && gcRwos.Cells["DELETE_FLG"].Value.ToString() == "True")
                    {
                        sharyouCd += gcRwos.Cells["SHARYOU_CD"].Value.ToString() + ",";
                    }
                }

                if (!string.IsNullOrEmpty(sharyouCd))
                {
                    sharyouCd = sharyouCd.Substring(0, sharyouCd.Length - 1);
                    strList = sharyouCd.Split(',');
                    DataTable dtTable = dao.GetDataBySqlFileCheck(this.CHECK_DELETE_SHARYOU_SQL, strList, this.SearchString.GYOUSHA_CD);
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        string strName = string.Empty;

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            strName += "\n" + dr["NAME"].ToString();
                        }

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E258", "車輌", "車輌CD", strName);

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
        /// プレビュー
        /// </summary>
        public void Preview()
        {
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            msgLogic.MessageBoxShow("C011", "車輌一覧表");

            MessageBox.Show("未実装");

            LogUtility.DebugMethodEnd();
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

                    // VUNGUYEN 20150525 #1294 START
                    CSVFileLogicCustom csvLogic = new CSVFileLogicCustom();
                    // VUNGUYEN 20150525 #1294 END

                    csvLogic.MultirowLocation = multirowLocationLogic.sortEndList;

                    csvLogic.Detail = this.form.Ichiran;

                    WINDOW_ID id = this.form.WindowId;

                    csvLogic.FileName = id.ToTitleString();
                    csvLogic.headerOutputFlag = true;

                    csvLogic.CreateCSVFile(this.form);
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSV", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
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

                ClearConditionF7();
                SetSearchString();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CancelCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
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
                        foreach (M_SHARYOU kansanEntity in this.entitys)
                        {
                            M_SHARYOU entity = this.dao.GetDataByCd(kansanEntity);
                            if (entity == null)
                            {
                                if (kansanEntity.DELETE_FLG)
                                {
                                    continue;
                                }
                                this.dao.Insert(kansanEntity);
                            }
                            else
                            {
                                this.dao.Update(kansanEntity);
                            }
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
                        foreach (M_SHARYOU kansanEntity in this.entitys)
                        {
                            M_SHARYOU entity = this.dao.GetDataByCd(kansanEntity);
                            if (entity != null)
                            {
                                this.dao.Update(kansanEntity);
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

            SharyouHoshuLogic localLogic = other as SharyouHoshuLogic;
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

                // テーブルデータの空判断
                if (table != null)
                {
                    table.BeginLoadData();

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        table.Columns[i].ReadOnly = false;
                    }
                }

                this.form.Ichiran.DataSource = table;

                if (r_framework.Authority.Manager.CheckAuthority("M207", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
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

            //削除ボタン(F4)イベント生成
            this.form.C_MasterRegist(parentForm.bt_func4);
            parentForm.bt_func4.Click += new EventHandler(this.form.LogicalDelete);
            parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

            ////ﾌﾟﾚﾋﾞｭｰボタン(F5)イベント生成
            //parentForm.bt_func5.Click += new EventHandler(this.form.Preview);

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

            //検索条件イベント生成
            this.form.CONDITION_VALUE.KeyPress += new KeyPressEventHandler(CONDITION_VALUE_KeyPress);
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
            M_SHARYOU entity = new M_SHARYOU();

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    // 検索条件の設定
                    entity.SetValue(this.form.CONDITION_VALUE);
                }
            }
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                // 検索条件の設定
                entity.SetValue(this.form.GYOUSHA_CD);
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
            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;

            this.form.CONDITION_VALUE.Text = string.Empty;
            this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
            this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
            this.form.CONDITION_ITEM.Text = string.Empty;

            this.SetHyoujiJoukenInit();
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

            this.form.Ichiran.DataSource = null;
            this.form.Ichiran.AllowUserToAddRows = false;
            this.SearchResult = null;
            this.SearchResultAll = null;
            this.SearchString = null;
        }

        #region 20140414 minhhoang edit #1478

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        private void ClearConditionF7()
        {
            this.form.CONDITION_VALUE.Text = string.Empty;
            this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
            this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
            this.form.CONDITION_ITEM.Text = string.Empty;

            this.SetHyoujiJoukenInit();
        }

        #endregion

        /// <summary>
        /// 業者名称情報の取得
        /// </summary>
        [Transaction]
        public virtual bool SearchGyoushaName()
        {
            try
            {
                bool ret = true;
                LogUtility.DebugMethodStart();

                this.SearchResultGyousha = gyoushaDao.GetDataBySqlFile(this.GET_GYOUSHA_DATA_SQL, new M_GYOUSHA());

                if (this.SearchResultGyousha.Rows != null)
                {
                    //this.SetGyoushaName();

                    if (!this.SetGyoushaName())
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "業者");
                        this.form.GYOUSHA_CD.Text = string.Empty;
                        this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                        ret = false;
                    }
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGyoushaName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// <summary>
        /// 業者名称の設定
        /// </summary>
        private bool SetGyoushaName()
        {
            if (this.SearchResultGyousha.Rows.Count == 0)
            {
                this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                return false;
            }

            foreach (DataRow row in this.SearchResultGyousha.Rows)
            {
                this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;

                if (this.form.GYOUSHA_CD.Text.PadLeft(6, '0').ToUpper() == row["GYOUSHA_CD"].ToString())
                {
                    this.form.GYOUSHA_NAME_RYAKU.Text = row["GYOUSHA_NAME_RYAKU"].ToString();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 重量フォーマット
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string FormatSystemJuuryou(decimal num, out bool catchErr)
        {
            try
            {
                catchErr = false;
                string format = "#,##0";
                if (this.entitySysInfo != null && !string.IsNullOrWhiteSpace(this.entitySysInfo.SYS_JYURYOU_FORMAT))
                {
                    format = this.entitySysInfo.SYS_JYURYOU_FORMAT;
                }
                return string.Format("{0:" + format + "}", num);
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("FormatSystemJuuryou", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return "";
            }
        }

        #region 運転者チェック

        /// <summary>
        /// 運転者チェック
        /// </summary>
        internal bool CheckUntenshasha(out bool catchErr)
        {
            bool returnVal = false;
            catchErr = false;

            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 初期化
                var rowIndex = this.form.Ichiran.CurrentRow.Index;
                this.form.Ichiran[rowIndex, "SHAIN_NAME_RYAKU"].Value = null;
                string untenshaCd = this.form.Ichiran[rowIndex, "SHAIN_CD"].Value.ToString();

                // 入力されてない場合
                if (string.IsNullOrEmpty(untenshaCd))
                {
                    returnVal = true;
                    return returnVal;
                }

                // 社員取得
                var shainEntity = this.GetShain(untenshaCd);
                if (shainEntity == null)
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E020", "社員");
                    return returnVal;
                }
                else if (!shainEntity.UNTEN_KBN.IsTrue)
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E020", "運転者");
                    return returnVal;
                }
                else
                {
                    this.form.Ichiran[rowIndex, "SHAIN_NAME_RYAKU"].Value = shainEntity.SHAIN_NAME_RYAKU;
                }

                returnVal = true;
                return returnVal;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("CheckUntenshasha", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
        }

        #endregion

        /// <summary>
        /// 社員CDで社員を取得します
        /// </summary>
        /// <param name="shainCd">社員CD</param>
        /// <returns>社員エンティティ</returns>
        public M_SHAIN GetShain(string shainCd)
        {
            LogUtility.DebugMethodStart(shainCd);

            // 取得済みの社員リストから取得
            var shain = this.shainList.Where(g => g.SHAIN_CD == shainCd).FirstOrDefault();
            if (null == shain)
            {
                // なければDBから取得
                var keyEntity = new M_SHAIN();
                keyEntity.SHAIN_CD = shainCd;
                shain = this.shainDao.GetAllValidData(keyEntity).FirstOrDefault();
                if (null != shain)
                {
                    this.shainList.Add(shain);
                }
            }

            LogUtility.DebugMethodEnd();

            return shain;
        }

        /// <summary>
        /// 主キーが同一の行がDBに存在する場合、主キーを非活性にする
        /// </summary>
        internal bool EditableToPrimaryKey()
        {
            try
            {
                // DBから主キーのListを取得
                var allEntityList = this.dao.GetAllData().Select(s => s.SHARYOU_CD).Where(s => !string.IsNullOrEmpty(s)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Select(r => r.Cells["SHARYOU_CD"]).Where(c => c.Value != null).ToList().
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

        /// <summary>
        /// 検索条件が数字のみ入力.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONDITION_VALUE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.form.CONDITION_VALUE.DBFieldsName.Equals(Const.SharyouHoshuConstans.SAIDAI_SEKISAI)
                || this.form.CONDITION_VALUE.DBFieldsName.Equals(Const.SharyouHoshuConstans.KUUSHA_JYURYO)
                || this.form.CONDITION_VALUE.DBFieldsName.Equals(Const.SharyouHoshuConstans.SAIDAI_WAKU_SUU))
            {
                if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Tab && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }
    }
}