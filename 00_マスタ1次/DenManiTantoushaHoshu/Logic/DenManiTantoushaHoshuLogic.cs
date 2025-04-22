// $Id: DenManiTantoushaHoshuLogic.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DenManiTantoushaHoshu.APP;
using DenManiTantoushaHoshu.Validator;
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

namespace DenManiTantoushaHoshu.Logic
{
    /// <summary>
    /// 社員保守画面のビジネスロジック
    /// </summary>
    public class DenManiTantoushaHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "DenManiTantoushaHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_SHAIN_DATA_SQL = "DenManiTantoushaHoshu.Sql.GetIchiranDataSql.sql";

        private readonly string GET_DENSHI_TANTOUSHA_DATA_SQL = "DenManiTantoushaHoshu.Sql.GetDenshiTantoushaDataSql.sql";

        private readonly string GET_DENSHI_TANTOUSHA_BY_PKEY_SQL = "DenManiTantoushaHoshu.Sql.GetDenshiTantoushaByPkey.sql";

        private readonly string GET_DENSHI_JIGYOUSHA_BY_ID_SQL = "DenManiTantoushaHoshu.Sql.GetDenshiJigyoushaById.sql";

        /// <summary>
        /// 社員保守画面Form
        /// </summary>
        private DenManiTantoushaHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 電子担当者マスタのエンティティ
        /// </summary>
        private M_DENSHI_TANTOUSHA[] entitys;

        /// <summary>
        /// 電子事業者マスタのエンティティ
        /// </summary>
        private M_DENSHI_JIGYOUSHA jigyoushaEntity;

        /// <summary>
        /// 全件検索フラグ
        /// </summary>
        private bool isAllSearch;

        /// <summary>
        /// 電子担当者マスタのDao
        /// </summary>
        private IM_DENSHI_TANTOUSHADao dao;

        /// <summary>
        /// 電子事業者マスタのDao
        /// </summary>
        private IM_DENSHI_JIGYOUSHADao jigyoushaDao;

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
        public M_DENSHI_TANTOUSHA SearchString { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public DenManiTantoushaHoshuLogic(DenManiTantoushaHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_DENSHI_TANTOUSHADao>();
            this.jigyoushaDao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUSHADao>();
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

                this.form.GYOUSHA_KBN.Text = Properties.Settings.Default.GYOUSHA_KBN_TEXT;
                this.form.EDI_MEMBER_ID.Text = Properties.Settings.Default.EDI_MEMBER_ID_TEXT;
                this.form.TANTOUSHA_KBN.Text = Properties.Settings.Default.TANTOUSHA_KBN_TEXT;
                if (!string.IsNullOrEmpty(this.form.EDI_MEMBER_ID.Text))
                {
                    bool catchErr = false;
                    bool ret = this.SearchDenshiJigyoushaData(out catchErr);
                    if (catchErr)
                    {
                        return catchErr;
                    }
                }

                if (string.IsNullOrEmpty(this.form.GYOUSHA_KBN.Text))
                {
                    this.form.GYOUSHA_KBN.Text = "1";
                }
                if (string.IsNullOrEmpty(this.form.TANTOUSHA_KBN.Text))
                {
                    this.form.TANTOUSHA_KBN.Text = "1";
                }

                this.EditableToPrimaryKey();
                this.form.Ichiran.AllowUserToAddRows = false; // thongh 2015/12/30 #1980

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

                this.SearchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_SHAIN_DATA_SQL
                                                                       , this.SearchString
                                                                       , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                this.SearchResultCheck = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_SHAIN_DATA_SQL
                                                                       , this.SearchString
                                                                       , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                this.SearchResultAll = dao.GetDataBySqlFile(this.GET_DENSHI_TANTOUSHA_DATA_SQL, this.SearchString);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

                Properties.Settings.Default.GYOUSHA_KBN_TEXT = this.form.GYOUSHA_KBN.Text;
                Properties.Settings.Default.EDI_MEMBER_ID_TEXT = this.form.EDI_MEMBER_ID.Text;
                Properties.Settings.Default.TANTOUSHA_KBN_TEXT = this.form.TANTOUSHA_KBN.Text;
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

                var entityList = new M_DENSHI_TANTOUSHA[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_DENSHI_TANTOUSHA();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_DENSHI_TANTOUSHA>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.DenManiTantoushaHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }
                dt.BeginLoadData();
                preDt = GetCloneDataTable(dt);

                // 変更分のみ取得

                this.form.Ichiran.DataSource = dt.GetChanges();

                var shainEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<M_DENSHI_TANTOUSHA> addList = new List<M_DENSHI_TANTOUSHA>();
                foreach (var tantoushaEntity in shainEntityList)
                {
                    foreach (Row row in this.form.Ichiran.Rows)
                    {
                        if (row.Cells.Any(n => (n.DataField.Equals(Const.DenManiTantoushaHoshuConstans.TANTOUSHA_CD) && n.Value.ToString().Equals(tantoushaEntity.TANTOUSHA_CD))) &&
                            row.Cells.Any(n => (n.DataField.Equals(Const.DenManiTantoushaHoshuConstans.DELETE_FLG) && bool.Parse(n.FormattedValue.ToString()) == isDelete)))
                        {
                            tantoushaEntity.EDI_MEMBER_ID = this.form.EDI_MEMBER_ID.Text;
                            tantoushaEntity.TANTOUSHA_KBN = Int16.Parse(this.form.TANTOUSHA_KBN.Text);
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), tantoushaEntity);
                            addList.Add(tantoushaEntity);
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
        /// 担当者CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                DenManiTantoushaHoshuValidator vali = new DenManiTantoushaHoshuValidator();
                bool result = vali.TantoushaCDValidator(this.form.Ichiran, this.SearchResultCheck, this.SearchResultAll, this.isAllSearch);

                LogUtility.DebugMethodEnd(false);

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
        public void Preview()
        {
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            msgLogic.MessageBoxShow("C011", "担当者一覧表");

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

        /// <summary>
        /// 業者区分変更時の条件設定処理
        /// </summary>
        public bool ChangeGyoushaKbnCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ポップアップ条件初期化
                if (this.form.EDI_MEMBER_ID.popupWindowSetting == null)
                {
                    this.form.EDI_MEMBER_ID.popupWindowSetting = new Collection<r_framework.Dto.JoinMethodDto>();
                }
                else
                {
                    this.form.EDI_MEMBER_ID.popupWindowSetting.Clear();
                }

                // 結合条件設定
                var joinMethodDto = new r_framework.Dto.JoinMethodDto();
                joinMethodDto.Join = JOIN_METHOD.WHERE;
                joinMethodDto.LeftTable = "M_DENSHI_JIGYOUSHA";

                // 検索条件設定
                var searchConditionsDto = new r_framework.Dto.SearchConditionsDto();
                searchConditionsDto.Value = "1";
                searchConditionsDto.ValueColumnType = DB_TYPE.BIT;

                PopupSearchSendParamDto dto = new PopupSearchSendParamDto();
                dto.And_Or = CONDITION_OPERATOR.AND;
                
                switch (this.form.GYOUSHA_KBN.Text)
                {
                    case "1":
                        searchConditionsDto.LeftColumn = "HST_KBN";
                        joinMethodDto.SearchCondition = new Collection<r_framework.Dto.SearchConditionsDto>();
                        joinMethodDto.SearchCondition.Add(searchConditionsDto);
                        this.form.EDI_MEMBER_ID.popupWindowSetting.Add(joinMethodDto);
                        dto.KeyName = "HST_KBN";
                        dto.Value = "1";

                        this.form.TANTOUSHA_KBN.Text = "1";
                        this.form.TANTOUSHA_KBN_4.Enabled = false;
                        this.form.TANTOUSHA_KBN_5.Enabled = false;
                        break;

                    case "2":
                        searchConditionsDto.LeftColumn = "UPN_KBN";
                        joinMethodDto.SearchCondition = new Collection<r_framework.Dto.SearchConditionsDto>();
                        joinMethodDto.SearchCondition.Add(searchConditionsDto);
                        this.form.EDI_MEMBER_ID.popupWindowSetting.Add(joinMethodDto);
                        dto.KeyName = "UPN_KBN";
                        dto.Value = "1";

                        this.form.TANTOUSHA_KBN.Text = "3";
                        this.form.TANTOUSHA_KBN_5.Enabled = false;
                        break;

                    case "3":
                        searchConditionsDto.LeftColumn = "SBN_KBN";
                        joinMethodDto.SearchCondition = new Collection<r_framework.Dto.SearchConditionsDto>();
                        joinMethodDto.SearchCondition.Add(searchConditionsDto);
                        this.form.EDI_MEMBER_ID.popupWindowSetting.Add(joinMethodDto);
                        dto.KeyName = "SBN_KBN";
                        dto.Value = "1";

                        this.form.TANTOUSHA_KBN.Text = "5";
                        break;
                }

                // ポップアップでの絞り込み条件設定
                this.form.EDI_MEMBER_ID.PopupSearchSendParams.Clear();
                this.form.EDI_MEMBER_ID.PopupSearchSendParams.Add(dto);

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeGyoushaKbnCondition", ex);
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
                        foreach (M_DENSHI_TANTOUSHA tantoushaEntity in this.entitys)
                        {
                            // 加入者ID、担当者区分でDB検索(担当者マスタ)
                            M_DENSHI_TANTOUSHA denybSearchString = new M_DENSHI_TANTOUSHA();
                            denybSearchString.EDI_MEMBER_ID = tantoushaEntity.EDI_MEMBER_ID;
                            denybSearchString.TANTOUSHA_KBN = tantoushaEntity.TANTOUSHA_KBN;
                            denybSearchString.TANTOUSHA_CD = tantoushaEntity.TANTOUSHA_CD;
                            DataTable tantoushaTable = this.dao.GetDataBySqlFile(GET_DENSHI_TANTOUSHA_BY_PKEY_SQL, denybSearchString);

                            if (tantoushaTable.Rows.Count == 0)
                            {
                                // 削除チェックが付けられている場合は、新規登録を行わない
                                if (tantoushaEntity.DELETE_FLG)
                                {
                                    continue;
                                }
                                this.dao.Insert(tantoushaEntity);
                            }
                            else
                            {
                                this.dao.Update(tantoushaEntity);
                            }

                            UpdateRelationInfo(tantoushaEntity, false);
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
                        foreach (M_DENSHI_TANTOUSHA tantoushaEntity in this.entitys)
                        {
                            // 加入者ID、担当者区分でDB検索(担当者マスタ)
                            DataTable tantoushaTable = this.dao.GetDataBySqlFile(GET_DENSHI_TANTOUSHA_BY_PKEY_SQL, tantoushaEntity);

                            if (tantoushaTable.Rows.Count > 0)
                            {
                                this.dao.Update(tantoushaEntity);
                                UpdateRelationInfo(tantoushaEntity, true);
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

            DenManiTantoushaHoshuLogic localLogic = other as DenManiTantoushaHoshuLogic;
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
                if (r_framework.Authority.Manager.CheckAuthority("M315", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
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
                LogUtility.DebugMethodEnd(true);
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

            //キー処理削除
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
            M_DENSHI_TANTOUSHA entity = new M_DENSHI_TANTOUSHA();

            // 検索条件の設定
            entity.SetValue(this.form.EDI_MEMBER_ID);
            entity.SetValue(this.form.TANTOUSHA_KBN);

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
            this.form.GYOUSHA_KBN.Text = string.Empty;
            this.form.EDI_MEMBER_ID.Text = string.Empty;
            this.form.JIGYOUSHA_NAME.Text = string.Empty;
            this.form.TANTOUSHA_KBN.Text = string.Empty;

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

        #region 20150416 minhhoang edit #1748

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
        /// 担当者保守に関連する情報の更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isDelete"></param>
        private void UpdateRelationInfo(M_DENSHI_TANTOUSHA entity, bool isDelete)
        {
            if (entity == null
                || string.IsNullOrEmpty(entity.TANTOUSHA_CD))
            {
                return;
            }
        }

        /// <summary>
        /// 電子事業者情報の取得を行う
        /// </summary>
        public bool SearchDenshiJigyoushaData(out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart();

                catchErr = false;
                if (string.IsNullOrEmpty(this.form.EDI_MEMBER_ID.Text))
                {
                    this.form.JIGYOUSHA_NAME.Text = string.Empty;
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }

                bool ret = false;
                M_DENSHI_JIGYOUSHA searchEntity = new M_DENSHI_JIGYOUSHA();
                searchEntity.EDI_MEMBER_ID = this.form.EDI_MEMBER_ID.Text;
                switch (this.form.GYOUSHA_KBN.Text)
                {
                    case "1":
                        searchEntity.HST_KBN = true;
                        break;

                    case "2":
                        searchEntity.UPN_KBN = true;
                        break;

                    case "3":
                        searchEntity.SBN_KBN = true;
                        break;
                }
                DataTable dt = this.jigyoushaDao.GetDataBySqlFile(GET_DENSHI_JIGYOUSHA_BY_ID_SQL, searchEntity);

                if (dt.Rows.Count > 0)
                {
                    this.form.JIGYOUSHA_NAME.Text = dt.Rows[0]["JIGYOUSHA_NAME"].ToString();
                    ret = true;
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", this.form.EDI_MEMBER_ID.DisplayItemName);
                    this.form.EDI_MEMBER_ID.Text = "";
                }

                LogUtility.DebugMethodEnd(ret, catchErr);
                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("SearchDenshiJigyoushaData", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false, catchErr);
                return false;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("SearchDenshiJigyoushaData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false, catchErr);
                return false;
            }
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

        #region 20151019 hoanghm #11994

        public bool CheckInputSearch()
        {
            try
            {
                this.form.GYOUSHA_KBN.IsInputErrorOccured = false;
                this.form.TANTOUSHA_KBN.IsInputErrorOccured = false;

                MessageUtility msgUtil = new MessageUtility();
                string msg = string.Empty;
                if (string.IsNullOrEmpty(this.form.GYOUSHA_KBN.Text))
                {
                    msg = msgUtil.GetMessage("E253").MESSAGE;
                    this.form.GYOUSHA_KBN.IsInputErrorOccured = true;
                }
                if (string.IsNullOrEmpty(this.form.TANTOUSHA_KBN.Text))
                {
                    if (!string.IsNullOrEmpty(msg))
                    {
                        msg += "\n";
                    }
                    msg += msgUtil.GetMessage("E254").MESSAGE;
                    this.form.TANTOUSHA_KBN.IsInputErrorOccured = true;
                }
                if (!string.IsNullOrEmpty(msg))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShowError(msg);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckInputSearch", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        #endregion

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
                    TANTOUSHA_KBN = s.TANTOUSHA_KBN,
                    TANTOUSHA_CD = s.TANTOUSHA_CD
                }).ToList();

                // DBに存在する行の主キーを非活性にする
                if (!string.IsNullOrEmpty(this.form.EDI_MEMBER_ID.Text)
                    && !string.IsNullOrEmpty(this.form.TANTOUSHA_KBN.Text))
                {
                    foreach (Row r in this.form.Ichiran.Rows)
                    {
                        if (r.Cells["TANTOUSHA_CD"].Value != null)
                        {
                            int count = 0;
                            count += allPrimaryKeyList.Select(s => s.EDI_MEMBER_ID).ToList().Contains(this.form.EDI_MEMBER_ID.Text) ? 1 : 0;
                            count += allPrimaryKeyList.Select(s => s.TANTOUSHA_KBN).ToList().Contains(Convert.ToInt16(this.form.TANTOUSHA_KBN.Text)) ? 1 : 0;
                            count += allPrimaryKeyList.Select(s => s.TANTOUSHA_CD).ToList().Contains(r.Cells["TANTOUSHA_CD"].Value) ? 1 : 0;

                            if (count >= 3)
                            {
                                r.Cells["TANTOUSHA_CD"].ReadOnly = true;
                                r.Cells["TANTOUSHA_CD"].UpdateBackColor(false);
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