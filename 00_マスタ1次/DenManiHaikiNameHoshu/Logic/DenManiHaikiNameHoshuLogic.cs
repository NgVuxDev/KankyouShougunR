// $Id: DenManiHaikiNameHoshuLogic.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DenManiHaikiNameHoshu.APP;
using DenManiHaikiNameHoshu.Validator;
using GrapeCity.Win.MultiRow;
using MasterCommon.Logic;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;

namespace DenManiHaikiNameHoshu.Logic
{
    /// <summary>
    /// 電マニ廃棄名称保守画面のビジネスロジック
    /// </summary>
    public class DenManiHaikiNameHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "DenManiHaikiNameHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_INPUTCD_DATA_HAIKI_NAME_SQL = "DenManiHaikiNameHoshu.Sql.GetInputCddataDenManiHaikiNameSql.sql";

        private readonly string GET_INPUTCD_DATA_JIGYOUSHA_SQL = "DenManiHaikiNameHoshu.Sql.GetInputCddataDenManiJigyoushaSql.sql";

        private readonly string GET_ICHIRAN_DENSHI_HAIKI_NAME_DATA_SQL = "DenManiHaikiNameHoshu.Sql.GetIchiranDataSql.sql";

        private readonly string GET_DENSHI_HAIKI_NAME_DATA_SQL = "DenManiHaikiNameHoshu.Sql.GetDenManiHaikiNameDataSql.sql";

        /// <summary>
        /// 電マニ廃棄名称保守画面Form
        /// </summary>
        private DenManiHaikiNameHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_DENSHI_HAIKI_NAME[] entitys;

        private bool isAllSearch;

        /// <summary>
        /// 電子廃棄種類名称のDao
        /// </summary>
        private IM_DENSHI_HAIKI_NAMEDao dao;

        /// <summary>
        /// 電子事業者のDao
        /// </summary>
        private IM_DENSHI_JIGYOUSHADao denshiJigyoushaDao;

        /// <summary>
        /// 廃棄物名称のDao
        /// </summary>
        private IM_HAIKI_NAMEDao haikiNameDao;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// システム設定のエンティティ
        /// </summary>
        private M_SYS_INFO entitySysInfo;

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
        public M_DENSHI_HAIKI_NAME SearchString { get; set; }

        /// <summary>
        /// 廃棄物名称CDの前回値
        /// </summary>
        public string beforeHaikiCd { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public DenManiHaikiNameHoshuLogic(DenManiHaikiNameHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_DENSHI_HAIKI_NAMEDao>();
            this.denshiJigyoushaDao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUSHADao>();
            this.haikiNameDao = DaoInitUtility.GetComponent<IM_HAIKI_NAMEDao>();
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

                this.form.EDI_MEMBER_ID.Text = Properties.Settings.Default.EDI_MEMBER_ID_TEXT;
                this.SetJigyoushaName(this.form.EDI_MEMBER_ID.Text);
                // ポップアップの設定
                var KBNSendParams = new PopupSearchSendParamDto();
                var KBNSubSendParams = new PopupSearchSendParamDto();

                KBNSendParams.And_Or = CONDITION_OPERATOR.AND;

                KBNSubSendParams = new PopupSearchSendParamDto();
                KBNSubSendParams.And_Or = CONDITION_OPERATOR.AND;
                KBNSubSendParams.KeyName = "HST_KBN";
                KBNSubSendParams.Value = "1";
                KBNSendParams.SubCondition.Add(KBNSubSendParams);

                KBNSubSendParams = new PopupSearchSendParamDto();
                KBNSubSendParams.And_Or = CONDITION_OPERATOR.OR;
                KBNSubSendParams.KeyName = "SBN_KBN";
                KBNSubSendParams.Value = "1";
                KBNSendParams.SubCondition.Add(KBNSubSendParams);

                this.form.EDI_MEMBER_ID.PopupSearchSendParams.Clear();
                this.form.EDI_MEMBER_ID.PopupSearchSendParams.Add(KBNSendParams);


                this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_VALUE.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;

                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }
                this.form.Ichiran.AllowUserToAddRows = false; // thongh 2015/12/30 #1980
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

                this.SearchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_DENSHI_HAIKI_NAME_DATA_SQL
                                                                       , this.SearchString
                                                                       , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                this.SearchResultCheck = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_DENSHI_HAIKI_NAME_DATA_SQL
                                                       , this.SearchString
                                                       , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                M_DENSHI_HAIKI_NAME searchParams = new M_DENSHI_HAIKI_NAME();
                searchParams.EDI_MEMBER_ID = this.SearchString.EDI_MEMBER_ID;
                this.SearchResultAll = dao.GetDataBySqlFile(this.GET_DENSHI_HAIKI_NAME_DATA_SQL, searchParams);

                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                Properties.Settings.Default.EDI_MEMBER_ID_TEXT = this.form.EDI_MEMBER_ID.Text;

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
                LogUtility.DebugMethodStart();

                var entityList = new M_DENSHI_HAIKI_NAME[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_DENSHI_HAIKI_NAME();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_DENSHI_HAIKI_NAME>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.DenManiHaikiNameHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }
                dt.BeginLoadData();
                preDt = GetCloneDataTable(dt);

                // 変更分のみ取得
                this.form.Ichiran.DataSource = dt.GetChanges();
                var haikiNameEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<M_DENSHI_HAIKI_NAME> addList = new List<M_DENSHI_HAIKI_NAME>();
                foreach (var haikiNameEntity in haikiNameEntityList)
                {
                    foreach (Row row in this.form.Ichiran.Rows)
                    {
                        if (row.Cells.Any(n => (n.DataField.Equals(Const.DenManiHaikiNameHoshuConstans.HAIKI_NAME_CD) && n.Value.ToString().Equals(haikiNameEntity.HAIKI_NAME_CD))) &&
                            row.Cells.Any(n => (n.DataField.Equals(Const.DenManiHaikiNameHoshuConstans.DELETE_FLG) && bool.Parse(n.FormattedValue.ToString()) == isDelete)))
                        {
                            haikiNameEntity.EDI_MEMBER_ID = this.form.EDI_MEMBER_ID.Text;
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), haikiNameEntity);
                            addList.Add(haikiNameEntity);
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
        /// 廃棄物名称の取得
        /// </summary>
        /// <param name="cd"></param>
        public string GetHaikiName(string cd)
        {
            LogUtility.DebugMethodStart();

            string result = string.Empty;

            // 廃棄物名称CDが入力され、廃棄物名称が未入力の場合に廃棄物名称マスタに同コードがあるかチェックする
            // 存在する場合は、廃棄物マスタの名称略称をセットし、存在しない場合でもエラーとしない
            M_HAIKI_NAME haikiName = this.haikiNameDao.GetDataByCd(cd.PadLeft(Const.DenManiHaikiNameHoshuConstans.HAIKI_NAME_CD_LENGTH, '0'));
            if (haikiName != null && !haikiName.DELETE_FLG.IsTrue)
            {
                result = haikiName.HAIKI_NAME_RYAKU;
            }

            LogUtility.DebugMethodEnd();
            return result;
        }

        /// <summary>
        /// 廃棄物名称CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                DenManiHaikiNameHoshuValidator vali = new DenManiHaikiNameHoshuValidator();
                bool result = vali.HaikiNameCDValidator(this.form.Ichiran, this.SearchResultCheck, this.SearchResultAll, this.isAllSearch);

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
                msgLogic.MessageBoxShow("C011", "廃棄名称一覧表");

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

                ClearConditionF7();
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

                // 独自チェックの記述例を書く
                // エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (M_DENSHI_HAIKI_NAME haikiMeishouEntity in this.entitys)
                        {
                            // 廃棄物名称CDでDB検索(廃棄物名称マスタ)
                            M_DENSHI_HAIKI_NAME denybSearchString = new M_DENSHI_HAIKI_NAME();
                            denybSearchString.EDI_MEMBER_ID = haikiMeishouEntity.EDI_MEMBER_ID;
                            denybSearchString.HAIKI_NAME_CD = haikiMeishouEntity.HAIKI_NAME_CD;
                            DataTable haikiShuruiTable = this.dao.GetDataBySqlFile(GET_INPUTCD_DATA_HAIKI_NAME_SQL, denybSearchString);

                            if (haikiShuruiTable.Rows.Count == 0)
                            {
                                // 削除チェックが付けられている場合は、新規登録を行わない
                                if (haikiMeishouEntity.DELETE_FLG)
                                {
                                    continue;
                                }
                                this.dao.Insert(haikiMeishouEntity);
                            }
                            else
                            {
                                this.dao.Update(haikiMeishouEntity);
                            }

                            UpdateRelationInfo(haikiMeishouEntity, false);
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
                        foreach (M_DENSHI_HAIKI_NAME haikiNameEntity in this.entitys)
                        {
                            DataTable haikiShuruiTable = this.dao.GetDataBySqlFile(GET_INPUTCD_DATA_HAIKI_NAME_SQL, haikiNameEntity);
                            if (haikiShuruiTable.Rows.Count > 0)
                            {
                                this.dao.Update(haikiNameEntity);
                                UpdateRelationInfo(haikiNameEntity, true);
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

            DenManiHaikiNameHoshuLogic localLogic = other as DenManiHaikiNameHoshuLogic;
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

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M321", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
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
            M_DENSHI_HAIKI_NAME entity = new M_DENSHI_HAIKI_NAME();

            // 検索条件の設定
            entity.SetValue(this.form.EDI_MEMBER_ID);

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
            this.form.EDI_MEMBER_ID.Text = string.Empty;
            this.form.JIGYOUSHA_NAME.Text = string.Empty;

            this.form.CONDITION_VALUE.Text = string.Empty;
            this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
            this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
            this.form.CONDITION_ITEM.Text = string.Empty;

            this.SetHyoujiJoukenInit();
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

            this.form.Ichiran.DataSource = null;
            this.form.Ichiran.AllowUserToAddRows = false; // thongh 2015/12/30 #1980
            SearchResult = null;
            SearchResultAll = null;
            SearchString = null;
        }

        #region minhhoang edit #1748

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
        /// 電マニ廃棄名称保守に関連する情報の更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isDelete"></param>
        private void UpdateRelationInfo(M_DENSHI_HAIKI_NAME entity, bool isDelete)
        {
            if (entity == null
                || string.IsNullOrEmpty(entity.HAIKI_NAME_CD))
            {
                return;
            }
        }

        /// <summary>
        /// EDI_MEMBER_IDを元に、事業者名称をセットする
        /// </summary>
        /// <param name="ediMemberId"></param>
        /// <returns></returns>
        public bool SetJigyoushaName(string ediMemberId)
        {
            LogUtility.DebugMethodStart();

            bool ret = false;
            M_DENSHI_JIGYOUSHA searchParam = new M_DENSHI_JIGYOUSHA();
            searchParam.EDI_MEMBER_ID = ediMemberId;
            DataTable table = this.denshiJigyoushaDao.GetDataBySqlFile(GET_INPUTCD_DATA_JIGYOUSHA_SQL, searchParam);
            if (table.Rows.Count > 0)
            {
                this.form.JIGYOUSHA_NAME.Text = table.Rows[0]["JIGYOUSHA_NAME"].ToString();
                ret = true;
            }

            LogUtility.DebugMethodEnd();
            return ret;
        }

        /// <summary>
        /// 登録時チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool RegistCheck(object sender, r_framework.Event.RegistCheckEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                bool result = false;
                MessageUtility msgUtil = new MessageUtility();

                // 加入者番号のチェックを行う
                var isControl = sender is Control;
                if (isControl && ((Control)sender).Name.Equals("EDI_MEMBER_ID"))
                {
                    string id = ((Control)sender).Text;
                    if (!this.HanSuujiCheck(id))
                    {
                        e.errorMessages.Add(string.Format(msgUtil.GetMessage("E140").MESSAGE, this.form.EDI_MEMBER_ID.DisplayItemName));
                    }
                    else
                    {
                        result = true;
                    }
                }

                LogUtility.DebugMethodEnd(result);
                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 半角数字形式かチェックする
        /// </summary>
        internal bool HanSuujiCheck(string value)
        {
            var result = true;

            if (string.IsNullOrEmpty(value))
            {
                return result;
            }

            // 半角数字形式か調べる
            result = System.Text.RegularExpressions.Regex.IsMatch(
                value,
                @"^[0-9]+$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            return result;
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
                var allPrimaryKeyList = this.dao.GetAllData().Select(s => new
                {
                    EDI_MEMBER_ID = s.EDI_MEMBER_ID,
                    HAIKI_NAME_CD = s.HAIKI_NAME_CD
                }).ToList();

                // DBに存在する行の主キーを非活性にする
                if (!string.IsNullOrEmpty(this.form.EDI_MEMBER_ID.Text))
                {
                    foreach (Row r in this.form.Ichiran.Rows)
                    {
                        if (r.Cells["HAIKI_NAME_CD"].Value != null)
                        {
                            int count = 0;
                            count += allPrimaryKeyList.Select(s => s.EDI_MEMBER_ID).ToList().Contains(this.form.EDI_MEMBER_ID.Text) ? 1 : 0;
                            count += allPrimaryKeyList.Select(s => s.HAIKI_NAME_CD).ToList().Contains(r.Cells["HAIKI_NAME_CD"].Value) ? 1 : 0;

                            if (count >= 2)
                            {
                                r.Cells["HAIKI_NAME_CD"].ReadOnly = true;
                                r.Cells["HAIKI_NAME_CD"].UpdateBackColor(false);
                            }
                        }
                    }
                }
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("EditableToPrimaryKey", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditableToPrimaryKey", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        #endregion
    }
}