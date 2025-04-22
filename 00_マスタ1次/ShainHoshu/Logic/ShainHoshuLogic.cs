// $Id: ShainHoshuLogic.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using MasterCommon.Logic;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Configuration;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using ShainHoshu.APP;
using ShainHoshu.Const;
using ShainHoshu.Validator;
using Shougun.Core.Common.BusinessCommon;
using r_framework.Dto;
using System.Data.SqlTypes;

using System.Collections.ObjectModel;

namespace ShainHoshu.Logic
{
    /// <summary>
    /// 社員保守画面のビジネスロジック
    /// </summary>
    public class ShainHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "ShainHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_SHAIN_DATA_SQL = "ShainHoshu.Sql.GetIchiranDataSql.sql";

        private readonly string GET_SHAIN_DATA_SQL = "ShainHoshu.Sql.GetShainDataSql.sql";

        private readonly string CHECK_DELETE_SHAIN_SQL = "ShainHoshu.Sql.CheckDeleteShainSql.sql";

        /// <summary>
        /// 社員保守画面Form
        /// </summary>
        private ShainHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_SHAIN[] entitys;

        private bool isAllSearch;

        /// <summary>
        /// 社員のDao
        /// </summary>
        private IM_SHAINDao dao;

        /// <summary>
        /// 最大表示画面数のDao
        /// </summary>
        private IM_SHAIN_MAX_WINDOWDao shainMaxDao;

        /// <summary>
        /// 部署のDao
        /// </summary>
        private IM_BUSHODao daoBusho;

        /// <summary>
        /// 営業担当者のDao
        /// </summary>
        private IM_EIGYOU_TANTOUSHADao daoEigyou;

        /// <summary>
        /// 運転者のDao
        /// </summary>
        private IM_UNTENSHADao daoUntensha;

        /// <summary>
        /// 処分担当者のDao
        /// </summary>
        private IM_SHOBUN_TANTOUSHADao daoShobun;

        /// <summary>
        /// 手形保管者のDao
        /// </summary>
        private IM_TEGATA_HOKANSHADao daoTegata;

        /// <summary>
        /// 入力担当者のDao
        /// </summary>
        private IM_NYUURYOKU_TANTOUSHADao daoNyuuryoku;

        /// <summary>
        /// INXS担当者のDao
        /// </summary>
        private IM_INXS_TANTOUSHADao daoInxsTantousha;

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
        #region VANTRUONG #128450 20190807
        private IM_OUTPUT_PATTERN_KOBETSUDao daoOutputPatternKobetsu;

        #endregion

        private IM_OUTPUT_PATTERNDao daoOutputPattern;

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
        public M_SHAIN SearchString { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public ShainHoshuLogic(ShainHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_SHAINDao>();
            this.daoBusho = DaoInitUtility.GetComponent<IM_BUSHODao>();
            this.daoEigyou = DaoInitUtility.GetComponent<IM_EIGYOU_TANTOUSHADao>();
            this.daoUntensha = DaoInitUtility.GetComponent<IM_UNTENSHADao>();
            this.daoShobun = DaoInitUtility.GetComponent<IM_SHOBUN_TANTOUSHADao>();
            this.daoTegata = DaoInitUtility.GetComponent<IM_TEGATA_HOKANSHADao>();
            this.daoNyuuryoku = DaoInitUtility.GetComponent<IM_NYUURYOKU_TANTOUSHADao>();
            this.daoInxsTantousha = DaoInitUtility.GetComponent<IM_INXS_TANTOUSHADao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.shainMaxDao = DaoInitUtility.GetComponent<IM_SHAIN_MAX_WINDOWDao>();

            this.entitySysInfo = null;
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            if (sysInfo != null && sysInfo.Length > 0)
            {
                this.entitySysInfo = sysInfo[0];
            }

            #region VANTRUONG #128450 20190807
            this.daoOutputPatternKobetsu = DaoInitUtility.GetComponent<IM_OUTPUT_PATTERN_KOBETSUDao>();
            #endregion
            this.daoOutputPattern = DaoInitUtility.GetComponent<IM_OUTPUT_PATTERNDao>();
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
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.S;

                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

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

                this.SearchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_SHAIN_DATA_SQL
                                                            , this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                this.SearchResultCheck = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_SHAIN_DATA_SQL
                                                            , this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);

                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName)
                    && this.form.CONDITION_VALUE.DBFieldsName.Equals(ShainHoshuConstans.BUSHO_NAME_RYAKU))
                {
                    string where = Regex.Replace(this.form.CONDITION_VALUE.Text.Replace("'", "''"), @"([\[\]*%])", "[$1]");
                    DataRow[] rows = this.SearchResult.Select(string.Format("{0} LIKE '%{1}%'", this.form.CONDITION_VALUE.DBFieldsName, where), string.Format("{0} ASC", ShainHoshuConstans.SHAIN_CD));

                    DataTable table = this.SearchResult.Clone();
                    DataTable tableCheck = this.SearchResultCheck.Clone();

                    foreach (DataRow row in rows)
                    {
                        table.ImportRow(row);
                        tableCheck.ImportRow(row);  //同一SQL文の為、同じデータをインポート
                    }

                    this.SearchResult = table;
                    this.SearchResultCheck = tableCheck;
                }

                this.SearchResultAll = dao.GetShainDataSqlFile(this.GET_SHAIN_DATA_SQL, new M_SHAIN());

                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.S = this.form.CONDITION_ITEM.Text;

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

                var entityList = new M_SHAIN[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_SHAIN();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_SHAIN>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.ShainHoshuConstans.TIME_STAMP))
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

                var shainEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<M_SHAIN> addList = new List<M_SHAIN>();
                foreach (var shainEntity in shainEntityList)
                {
                    foreach (Row row in this.form.Ichiran.Rows)
                    {
                        if (row.Cells.Any(n => (n.DataField.Equals(Const.ShainHoshuConstans.SHAIN_CD) && n.Value.ToString().Equals(shainEntity.SHAIN_CD))) &&
                            row.Cells.Any(n => (n.DataField.Equals(Const.ShainHoshuConstans.DELETE_FLG) && bool.Parse(n.FormattedValue.ToString()) == isDelete)))
                        {
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), shainEntity);
                            addList.Add(shainEntity);
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
        /// 社員CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                ShainHoshuValidator vali = new ShainHoshuValidator();
                bool result = vali.ShainCDValidator(this.form.Ichiran, this.SearchResultCheck, this.SearchResultAll, this.isAllSearch);

                LogUtility.DebugMethodEnd();

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
        /// ログインIDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheckLoginId()
        {
            try
            {
                LogUtility.DebugMethodStart();

                ShainHoshuValidator vali = new ShainHoshuValidator();
                bool result = vali.LoginIDValidator(this.form.Ichiran, this.SearchResultCheck, this.SearchResultAll, this.isAllSearch);

                LogUtility.DebugMethodEnd();

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheckLoginId", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// <summary>
        /// 部署CDの値チェック
        /// </summary>
        public bool BushoCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                var bushoCd = string.Empty;
                if (this.form.Ichiran[this.form.Ichiran.CurrentRow.Index, Const.ShainHoshuConstans.BUSHO_CD].Value != null)
                {
                    bushoCd = this.form.Ichiran[this.form.Ichiran.CurrentRow.Index, Const.ShainHoshuConstans.BUSHO_CD].Value.ToString();
                }
                this.form.Ichiran[this.form.Ichiran.CurrentRow.Index, Const.ShainHoshuConstans.BUSHO_NAME_RYAKU].Value = string.Empty;
                if (!string.IsNullOrEmpty(bushoCd))
                {
                    M_BUSHO search = new M_BUSHO();
                    search.BUSHO_CD = bushoCd;
                    M_BUSHO[] busho = this.daoBusho.GetAllValidData(search);
                    if (busho != null && busho.Length > 0 && !busho[0].BUSHO_CD.Equals("999"))
                    {
                        this.form.Ichiran[this.form.Ichiran.CurrentRow.Index, Const.ShainHoshuConstans.BUSHO_CD].Value = busho[0].BUSHO_CD;
                        this.form.Ichiran[this.form.Ichiran.CurrentRow.Index, Const.ShainHoshuConstans.BUSHO_NAME_RYAKU].Value = busho[0].BUSHO_NAME_RYAKU;
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "部署");
                        ret = false;
                    }
                }

                if (!ret)
                {
                    this.form.Ichiran[this.form.Ichiran.CurrentRow.Index, Const.ShainHoshuConstans.BUSHO_CD].Selected = true; //.Value = string.Empty;
                    this.form.Ichiran[this.form.Ichiran.CurrentRow.Index, Const.ShainHoshuConstans.BUSHO_NAME_RYAKU].Value = string.Empty;
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("BushoCdValidated", ex);
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
            LogUtility.DebugMethodStart();

            bool ret = true;
            var shaInCd = string.Empty;
            string[] strList;

            foreach (Row gcRwos in this.form.Ichiran.Rows)
            {
                if (gcRwos.Cells["DELETE_FLG"].Value != null && gcRwos.Cells["DELETE_FLG"].Value.ToString() == "True")
                {
                    if (gcRwos.Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(gcRwos.Cells["CREATE_USER"].Value.ToString()))
                    {
                        continue;
                    }
                    shaInCd += gcRwos.Cells["SHAIN_CD"].Value.ToString() + ",";
                }
            }

            if (!string.IsNullOrEmpty(shaInCd))
            {
                shaInCd = shaInCd.Substring(0, shaInCd.Length - 1);
                strList = shaInCd.Split(',');
                DataTable dtTable = dao.GetDataBySqlFileCheck(this.CHECK_DELETE_SHAIN_SQL, strList);
                if (dtTable != null && dtTable.Rows.Count > 0)
                {
                    string strName = string.Empty;

                    foreach (DataRow dr in dtTable.Rows)
                    {
                        strName += "\n" + dr["NAME"].ToString();
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E258", "社員", "社員CD", strName);

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

        /// <summary>
        /// プレビュー
        /// </summary>
        public bool Preview()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("C011", "社員一覧表");

                MessageBox.Show("未実装");

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Preview", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
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

                ClearCondition();
                bool catchErr = SetIchiran();

                LogUtility.DebugMethodEnd();
                return catchErr;
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
                        foreach (M_SHAIN shainEntity in this.entitys)
                        {
                            M_SHAIN entity = this.dao.GetDataByCd(shainEntity.SHAIN_CD);
                            M_SHAIN_MAX_WINDOW entity2 = new M_SHAIN_MAX_WINDOW();
                            if (entity == null)
                            {
                                // 削除チェックが付けられている場合は、新規登録を行わない
                                if (shainEntity.DELETE_FLG)
                                {
                                    continue;
                                }
                                this.dao.Insert(shainEntity);
                                entity2.SHAIN_CD = shainEntity.SHAIN_CD;
                                entity2.MAX_WINDOW_COUNT = entitySysInfo.MAX_WINDOW_COUNT;
                                entity2.CREATE_DATE = shainEntity.CREATE_DATE;
                                entity2.CREATE_PC = shainEntity.CREATE_PC;
                                entity2.CREATE_USER = shainEntity.CREATE_USER;
                                entity2.UPDATE_DATE = shainEntity.UPDATE_DATE;
                                entity2.UPDATE_PC = shainEntity.UPDATE_PC;
                                entity2.UPDATE_USER = shainEntity.UPDATE_USER;
                                this.shainMaxDao.Insert(entity2);
                            }
                            else
                            {
                                this.dao.Update(shainEntity);
                            }

                            UpdateRelationInfo(shainEntity, false);



                            #region VANTRUONG #128450 20190807
                            this.InserPartern(shainEntity);
                            #endregion
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
                        foreach (M_SHAIN shainEntity in this.entitys)
                        {
                            M_SHAIN entity = this.dao.GetDataByCd(shainEntity.SHAIN_CD);
                            if (entity != null)
                            {
                                this.dao.Update(shainEntity);
                                UpdateRelationInfo(shainEntity, true);
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

            ShainHoshuLogic localLogic = other as ShainHoshuLogic;
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
                if (this.SetFixedIchiran()) { return true; }

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M192", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
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
            M_SHAIN entity = new M_SHAIN();

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName)
                && !this.form.CONDITION_VALUE.DBFieldsName.Equals(ShainHoshuConstans.BUSHO_NAME_RYAKU))
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
        /// 社員保守に関連する情報の更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isDelete"></param>
        private void UpdateRelationInfo(M_SHAIN entity, bool isDelete)
        {
            if (entity == null
                || string.IsNullOrEmpty(entity.SHAIN_CD))
            {
                return;
            }

            // 営業担当者
            M_EIGYOU_TANTOUSHA eigyoEntity = daoEigyou.GetDataByCd(entity.SHAIN_CD);
            if (eigyoEntity != null)
            {
                if (isDelete || !entity.EIGYOU_TANTOU_KBN)
                {
                    eigyoEntity.DELETE_FLG = true;
                    eigyoEntity.EIGYOU_TANTOUSHA_BIKOU = string.Empty;
                }
                else
                {
                    eigyoEntity.DELETE_FLG = false;
                }

                daoEigyou.Update(eigyoEntity);
            }
            else
            {
                if (entity.EIGYOU_TANTOU_KBN)
                {
                    eigyoEntity = new M_EIGYOU_TANTOUSHA();
                    var dataBinder = new DataBinderLogic<M_EIGYOU_TANTOUSHA>(eigyoEntity);
                    dataBinder.SetSystemProperty(eigyoEntity, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), eigyoEntity);

                    eigyoEntity.SHAIN_CD = entity.SHAIN_CD;
                    eigyoEntity.DELETE_FLG = entity.DELETE_FLG;

                    daoEigyou.Insert(eigyoEntity);
                }
            }

            // 運転者
            M_UNTENSHA untenshaEntity = daoUntensha.GetDataByCd(entity.SHAIN_CD);
            if (untenshaEntity != null)
            {
                if (isDelete || !entity.UNTEN_KBN)
                {
                    untenshaEntity.DELETE_FLG = true;
                    untenshaEntity.UNTENSHA_BIKOU = string.Empty;
                }
                else
                {
                    untenshaEntity.DELETE_FLG = false;
                }

                daoUntensha.Update(untenshaEntity);
            }
            else
            {
                if (entity.UNTEN_KBN)
                {
                    untenshaEntity = new M_UNTENSHA();
                    var dataBinder = new DataBinderLogic<M_UNTENSHA>(untenshaEntity);
                    dataBinder.SetSystemProperty(untenshaEntity, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), untenshaEntity);

                    untenshaEntity.SHAIN_CD = entity.SHAIN_CD;
                    untenshaEntity.DELETE_FLG = entity.DELETE_FLG;

                    daoUntensha.Insert(untenshaEntity);
                }
            }

            // 処分担当者
            M_SHOBUN_TANTOUSHA shobunEntity = daoShobun.GetDataByCd(entity.SHAIN_CD);
            if (shobunEntity != null)
            {
                if (isDelete || !entity.SHOBUN_TANTOU_KBN)
                {
                    shobunEntity.DELETE_FLG = true;
                    shobunEntity.SHOBUN_TANTOUSHA_BIKOU = string.Empty;
                }
                else
                {
                    shobunEntity.DELETE_FLG = false;
                }

                daoShobun.Update(shobunEntity);
            }
            else
            {
                if (entity.SHOBUN_TANTOU_KBN)
                {
                    shobunEntity = new M_SHOBUN_TANTOUSHA();
                    var dataBinder = new DataBinderLogic<M_SHOBUN_TANTOUSHA>(shobunEntity);
                    dataBinder.SetSystemProperty(shobunEntity, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), shobunEntity);

                    shobunEntity.SHAIN_CD = entity.SHAIN_CD;
                    shobunEntity.DELETE_FLG = entity.DELETE_FLG;

                    daoShobun.Insert(shobunEntity);
                }
            }

            // 手形保管者
            M_TEGATA_HOKANSHA tegataEntity = daoTegata.GetDataByCd(entity.SHAIN_CD);
            if (tegataEntity != null)
            {
                if (isDelete || !entity.TEGATA_HOKAN_KBN)
                {
                    tegataEntity.DELETE_FLG = true;
                    tegataEntity.TEGATA_HOKANSHA_BIKOU = string.Empty;
                }
                else
                {
                    tegataEntity.DELETE_FLG = false;
                }

                daoTegata.Update(tegataEntity);
            }
            else
            {
                if (entity.TEGATA_HOKAN_KBN)
                {
                    tegataEntity = new M_TEGATA_HOKANSHA();
                    var dataBinder = new DataBinderLogic<M_TEGATA_HOKANSHA>(tegataEntity);
                    dataBinder.SetSystemProperty(tegataEntity, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), tegataEntity);

                    tegataEntity.SHAIN_CD = entity.SHAIN_CD;
                    tegataEntity.DELETE_FLG = entity.DELETE_FLG;

                    daoTegata.Insert(tegataEntity);
                }
            }

            // 入力担当者
            M_NYUURYOKU_TANTOUSHA nyuuryokuEntity = daoNyuuryoku.GetDataByCd(entity.SHAIN_CD);
            if (nyuuryokuEntity != null)
            {
                if (isDelete || !entity.NYUURYOKU_TANTOU_KBN)
                {
                    nyuuryokuEntity.DELETE_FLG = true;
                    nyuuryokuEntity.NYUURYOKU_TANTOUSHA_BIKOU = string.Empty;
                }
                else
                {
                    nyuuryokuEntity.DELETE_FLG = false;
                }

                daoNyuuryoku.Update(nyuuryokuEntity);
            }
            else
            {
                if (entity.NYUURYOKU_TANTOU_KBN)
                {
                    nyuuryokuEntity = new M_NYUURYOKU_TANTOUSHA();
                    var dataBinder = new DataBinderLogic<M_NYUURYOKU_TANTOUSHA>(nyuuryokuEntity);
                    dataBinder.SetSystemProperty(nyuuryokuEntity, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), nyuuryokuEntity);

                    nyuuryokuEntity.SHAIN_CD = entity.SHAIN_CD;
                    nyuuryokuEntity.DELETE_FLG = entity.DELETE_FLG;

                    daoNyuuryoku.Insert(nyuuryokuEntity);
                }
            }

            // INXS担当者
            M_INXS_TANTOUSHA inxsTantoushaEntity = daoInxsTantousha.GetDataByCd(entity.SHAIN_CD);
            if (inxsTantoushaEntity != null)
            {
                if (isDelete || !entity.INXS_TANTOU_FLG)
                {
                    inxsTantoushaEntity.DELETE_FLG = true;
                    inxsTantoushaEntity.INXS_TANTOUSHA_BIKOU = string.Empty;
                }
                else
                {
                    inxsTantoushaEntity.DELETE_FLG = false;
                }

                daoInxsTantousha.Update(inxsTantoushaEntity);
            }
            else
            {
                if (entity.INXS_TANTOU_FLG)
                {
                    inxsTantoushaEntity = new M_INXS_TANTOUSHA();
                    var dataBinder = new DataBinderLogic<M_INXS_TANTOUSHA>(inxsTantoushaEntity);
                    dataBinder.SetSystemProperty(inxsTantoushaEntity, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), inxsTantoushaEntity);

                    inxsTantoushaEntity.SHAIN_CD = entity.SHAIN_CD;
                    inxsTantoushaEntity.DELETE_FLG = entity.DELETE_FLG;

                    daoInxsTantousha.Insert(inxsTantoushaEntity);
                }
            }
        }

        /// <summary>
        /// パスワード文字設定処理
        /// </summary>
        /// <param name="e"></param>
        public bool SetPasswordChars(CellFormattingEventArgs e)
        {
            try
            {
                if (this.entitySysInfo == null || this.entitySysInfo.COMMON_PASSWORD_DISP.IsNull || this.entitySysInfo.COMMON_PASSWORD_DISP != 1)
                {
                    string formattedStr = string.Empty;
                    if (e.Value != null && e.Value.ToString().Length > 0)
                    {
                        e.Value = formattedStr.PadLeft(e.Value.ToString().Length, '*');
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetPasswordChars", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
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
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetFixedIchiran", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        /// <summary>
        /// テーブル固定値の変更不可処理を行います
        /// </summary>
        /// <param name="eRow">変更不可処理を行うための判定データが入力されているRow</param>
        internal void SetFixedRow(Row row)
        {
            var objValue = row[Const.ShainHoshuConstans.SHAIN_CD].Value;

            bool catchErr = false;
            if (this.CheckFixedRow(row, out catchErr) && !catchErr)
            {
                foreach (var columnName in Const.ShainHoshuConstans.fixedColumnList)
                {
                    row[columnName].ReadOnly = true;
                    row[columnName].Selectable = false;
                    row[columnName].UpdateBackColor(false);
                }
            }

            //20250311
            for (int i = 0; i < this.form.Ichiran.Rows.Count; i++)
            {
                this.form.Ichiran.CommitEdit(DataErrorContexts.Commit);

                row = this.form.Ichiran.Rows[i];

                object unten_kbn = row.Cells[ShainHoshuConstans.UNTEN_KBN].Value;
                object nin_i = row.Cells[ShainHoshuConstans.NYUURYOKU_TANTOU_KBN].Value;


                if (unten_kbn != null && bool.TryParse(unten_kbn.ToString(), out bool result) && !result)
                {
                    row.Cells[ShainHoshuConstans.WARIATE_JUN].ReadOnly = true;
                    row.Cells[ShainHoshuConstans.WARIATE_JUN].UpdateBackColor(false);
                }

                if(nin_i != null && bool.TryParse(nin_i.ToString(), out bool result1) && !result1)
                {
                    row.Cells[ShainHoshuConstans.NIN_I_TORIHIKISAKI_FUKA].Enabled = false;
                }
            }

            this.form.Ichiran.Invalidate();
            this.form.Ichiran.Refresh();


            if (catchErr)
            {
                throw new Exception("");
            }
        }

        /// <summary>
        /// テーブル固定値のデータかどうかを調べる
        /// </summary>
        /// <param name="eRow">行データ</param>
        /// <returns>結果</returns>
        internal bool CheckFixedRow(Row row, out bool catchErr)
        {
            try
            {
                catchErr = false;
                var objValue = row[Const.ShainHoshuConstans.SHAIN_CD].Value;

                if (row != null && objValue != null)
                {
                    var strCd = objValue.ToString();
                    if (Const.ShainHoshuConstans.fixedRowList.Contains(strCd))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("CheckFixedRow", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 主キーが同一の行がDBに存在する場合、主キーを非活性にする
        /// </summary>
        internal bool EditableToPrimaryKey()
        {
            try
            {
                // DBから主キーのListを取得
                var allEntityList = this.dao.GetAllData().Select(s => s.SHAIN_CD).Where(s => !string.IsNullOrEmpty(s)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Select(r => r.Cells["SHAIN_CD"]).Where(c => c.Value != null).ToList().
                                            ForEach(c =>
                                            {
                                                c.ReadOnly = allEntityList.Contains(c.Value.ToString());
                                                c.UpdateBackColor(false);  	// MultiRowの場合、ここで背景色をセットする。
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
        /// 登録時のチェック処理
        /// </summary>
        /// <param name="isDelete">true:削除時, false:登録時</param>
        /// <returns>true:エラー有, false:エラー無</returns>
        internal bool HasErrorRegistCheck(bool isDelete)
        {
            DataTable dt = this.form.Ichiran.DataSource as DataTable;

            #region メールアドレス
            {
                // DELETE_FLG、MAIL_ADDRESSのDBNullまで考慮
                var d = dt.AsEnumerable().Where(n => (DBNull.Value.Equals(n[Const.ShainHoshuConstans.DELETE_FLG])
                                                  || bool.Parse(n[Const.ShainHoshuConstans.DELETE_FLG].ToString()) == isDelete)
                                                  && !DBNull.Value.Equals(n[Const.ShainHoshuConstans.MAIL_ADDRESS])
                                                  && !string.IsNullOrEmpty(n[Const.ShainHoshuConstans.MAIL_ADDRESS].ToString()));

                // 登録上限値チェック
                var limit = Const.ShainHoshuConstans.REGIST_LIMIT_MAIL_ADDRESS;
                var limitOverList = d.Where(n => limit < n[Const.ShainHoshuConstans.MAIL_ADDRESS].ToString().Split(',').Length)
                                     .Select(n => n[Const.ShainHoshuConstans.SHAIN_CD].ToString())
                                     .ToList();

                if (limitOverList.Any())
                {
                    this.SetErrorColorCell(limitOverList, Const.ShainHoshuConstans.MAIL_ADDRESS);
                    this.form.errmessage.MessageBoxShowError(string.Format("登録出来るメールアドレスは{0}つまでとなります。", limit));
                    return true;
                }

                // メールアドレス妥当性チェック
                var mailList = d.Select((shainCd, mailAddress) => new
                {
                    ShainCd = shainCd[Const.ShainHoshuConstans.SHAIN_CD].ToString(),
                    MailAddress = shainCd[Const.ShainHoshuConstans.MAIL_ADDRESS].ToString().Split(',')
                });

                var errMailList = mailList.Where(n => !IsValidMailAddress(n.MailAddress))
                                          .Select(n => n.ShainCd)
                                          .ToList();
                if (errMailList.Any())
                {
                    this.SetErrorColorCell(errMailList, Const.ShainHoshuConstans.MAIL_ADDRESS);
                    this.form.errmessage.MessageBoxShowError("電子メールアドレスに必要な形式ではありません。");
                    return true;
                }
            }
            #endregion

            #region モバイル将軍CAL数チェック
            // モバイル将軍CAL数を取得
            int mobileCal = AppConfig.MobileCal;

            int mobileUserKbnNum = 0;
            foreach (Row gcRwos in this.form.Ichiran.Rows)
            {
                // モバイル利用者のチェックがON
                if (gcRwos.Cells["MOBILE_USER_KBN"].Value != null && gcRwos.Cells["MOBILE_USER_KBN"].Value.ToString() == "True")
                {
                    // モバイル利用者をカウントする。
                    mobileUserKbnNum++;
                }
            }
            
            // モバイル利用者の数がモバイル将軍CAL数を超えていないかチェックする。
            if (mobileCal > 0 && mobileUserKbnNum > mobileCal)
            {
                this.form.errmessage.MessageBoxShowError("モバイル利用者の数がモバイル将軍で利用可能な上限数を超えています。\n(モバイル将軍ライセンス数：" + mobileCal + "CAL)");
                return true;
            }

            #endregion

            return false;
        }

        /// <summary>
        /// 対象の社員CDと指定されたセルをエラー色に変更
        /// </summary>
        /// <param name="shainCdList">社員CDリスト</param>
        /// <param name="targetCell">セル名称</param>
        private void SetErrorColorCell(List<string> shainCdList, string targetCell)
        {
            this.form.Ichiran.Rows.Where(n => shainCdList.Contains(n.Cells[Const.ShainHoshuConstans.SHAIN_CD].Value.ToString()))
                                  .ToList()
                                  .ForEach(row =>
                                  {
                                      var cell = (GcCustomTextBoxCell)row.Cells[targetCell];
                                      cell.IsInputErrorOccured = true;
                                      cell.Style.BackColor = Constans.ERROR_COLOR;
                                  });
        }

        /// <summary>
        /// 指定された文字列がメールアドレスとして正しい形式か検証する
        /// </summary>
        /// <param name="address">検証する文字列</param>
        /// <returns>正しい時はTrue。正しくない時はFalse。</returns>
        private bool IsValidMailAddress(string[] addressList)
        {
            if (addressList == null)
            {
                return false;
            }

            foreach (var address in addressList)
            {
                if (!IsValidMailAddress(address))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 指定された文字列がメールアドレスとして正しい形式か検証する
        /// </summary>
        /// <param name="address">検証する文字列</param>
        /// <returns>正しい時はTrue。正しくない時はFalse。</returns>
        private bool IsValidMailAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return false;
            }

            try
            {
                MailAddress mail = new MailAddress(address);
            }
            catch (FormatException)
            {
                //FormatExceptionがスローされた時は、正しくない
                return false;
            }

            return true;
        }

        #region VANTRUONG #128450 20190807
        private void InserPartern(M_SHAIN shain)
        {

            //M_OUTPUT_PATTERNを取得
            var itemParternLogin = daoOutputPattern.GetAllValidData(new M_OUTPUT_PATTERN { });

            var itemPartern = daoOutputPatternKobetsu.GetAllValidData(new M_OUTPUT_PATTERN_KOBETSU
            {
                SHAIN_CD = shain.SHAIN_CD,
                //DELETE_FLG = false,
            });

            if (itemParternLogin != null && itemParternLogin.Length > 0 && (itemParternLogin == null || itemPartern.Length == 0))
            {
                itemParternLogin.Where(T => T.SYSTEM_ID.Value > 900000000000000).ToList().ForEach(k =>
                {
                    daoOutputPatternKobetsu.Insert(new M_OUTPUT_PATTERN_KOBETSU
                    {
                        CREATE_DATE = shain.CREATE_DATE,
                        CREATE_PC = shain.CREATE_PC,
                        CREATE_USER = shain.CREATE_USER,
                        DEFAULT_KBN = true,
                        DELETE_FLG = k.DELETE_FLG,
                        DISP_NUMBER = 1,            //表示位置はパターン1固定
                        SEQ = 1,
                        SHAIN_CD = shain.SHAIN_CD,
                        SYSTEM_ID = k.SYSTEM_ID,
                        UPDATE_DATE = shain.UPDATE_DATE,
                        UPDATE_PC = shain.UPDATE_PC,
                        UPDATE_USER = shain.UPDATE_USER,
                    });
                });
            }
        }
        #endregion

        //20250311
        public bool RegistCheck()
        {
            if (DuplicationCheckWariateJun())
            {
                return false;
            }

            for (int i = 0; i < this.form.Ichiran.Rows.Count; i++) 
            {
                Row r = this.form.Ichiran.Rows[i];
                object unten_kbn = r.Cells[ShainHoshuConstans.UNTEN_KBN].Value;
                var cell = (GcCustomNumericTextBox2Cell)r.Cells[ShainHoshuConstans.WARIATE_JUN];


                if (unten_kbn != null && bool.TryParse(unten_kbn.ToString(), out bool result) && result)
                {
                    if (string.IsNullOrEmpty(r.Cells[ShainHoshuConstans.WARIATE_JUN].Value?.ToString()))
                    {
                        SelectCheckDto existCheck = new SelectCheckDto { CheckMethodName = "必須チェック" };

                        Collection<SelectCheckDto> existChecks = new Collection<SelectCheckDto> { existCheck };

                        cell.RegistCheckMethod = existChecks;
                    }
                    else
                    {
                        cell.RegistCheckMethod = null;
                    }
                }
                else
                {
                    cell.RegistCheckMethod = null;
                }
            }
            return true;
        }

        //20250321
        public bool DuplicationCheckWariateJun()
        {
            //try
            //{
                HashSet<string> wariateJunSet = new HashSet<string>();

                for (int i = 0; i < this.form.Ichiran.Rows.Count; i++)
                {
                    Row r = this.form.Ichiran.Rows[i];
                    object wariateJun = r.Cells[ShainHoshuConstans.WARIATE_JUN].Value;
                var cell = (GcCustomNumericTextBox2Cell)r.Cells[ShainHoshuConstans.WARIATE_JUN];

                    if (wariateJun != null)
                    {
                        string wariateValue = wariateJun.ToString();

                        if (!string.IsNullOrEmpty(wariateValue))
                        {
                            if (!wariateJunSet.Add(wariateValue))
                            {
                                this.form.errmessage.MessageBoxShowError("割り当ての順序は重複できません");
                                cell.IsInputErrorOccured = true;
                                return true;
                            }
                        }
                    }
                }
                return false;
            //}
            //catch(Exception ex)
            //{
            //    LogUtility.Error("DuplicationCheckWariateJun", ex);
            //    this.form.errmessage.MessageBoxShowError("");
            //    return false;
            //}
        }

    }
}