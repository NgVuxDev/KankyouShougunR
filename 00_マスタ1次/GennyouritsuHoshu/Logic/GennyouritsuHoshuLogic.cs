// $Id: GennyouritsuHoshuLogic.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GennyouritsuHoshu.APP;
using GennyouritsuHoshu.Dto;
using GennyouritsuHoshu.Validator;
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
using System.Data.SqlTypes;

namespace GennyouritsuHoshu.Logic
{
    /// <summary>
    /// 減容率保守画面のビジネスロジック
    /// </summary>
    public class GennyouritsuHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "GennyouritsuHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_GENNYOURITSU_DATA_SQL = "GennyouritsuHoshu.Sql.GetIchiranGennyouritsudataSql.sql";

        private readonly string GET_GENNYOURITSU_DATA_SQL = "GennyouritsuHoshu.Sql.GetGennyouritsudataSql.sql";

        private readonly string GET_HOUKOKUSHOBUNRUI_DATA_SQL = "GennyouritsuHoshu.Sql.GetHoukokushoBunruidataSql.sql";

        private readonly string GET_ICHIRAN_HAIKI_NAME_DATA_SQL = "GennyouritsuHoshu.Sql.GetIchiranHaikiNamedataSql.sql";

        private readonly string GET_ICHIRAN_SHOBUN_HOUHOU_DATA_SQL = "GennyouritsuHoshu.Sql.GetIchiranShobunHouhoudataSql.sql";

        private readonly string UPDATE_GENNRYOURITSU_DATA_SQL = "GennyouritsuHoshu.Sql.UpdateGennyouritsuDataSql.sql";

        /// <summary>
        /// 減容率保守画面Form
        /// </summary>
        private GennyouritsuHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private DataDto[] entitys;

        private bool isAllSearch;

        /// <summary>
        /// 減容率のDao
        /// </summary>
        private IM_GENNYOURITSUDao dao;

        /// <summary>
        /// 報告書分類のDao
        /// </summary>
        private IM_HOUKOKUSHO_BUNRUIDao houkokushoDao;

        /// <summary>
        /// 廃棄物名称のDao
        /// </summary>
        private IM_HAIKI_NAMEDao haikiNameDao;

        /// <summary>
        /// 処分方法のDao
        /// </summary>
        private IM_SHOBUN_HOUHOUDao shobunHouhouDao;

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
        /// 検索結果(全件)
        /// </summary>
        public DataTable SearchResultAll { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_GENNYOURITSU SearchString { get; set; }

        /// <summary>
        /// 検索結果(報告書)
        /// </summary>
        public DataTable SearchResultHoukokusho { get; set; }

        /// <summary>
        /// 検索条件(Dto)
        /// </summary>
        public GennyouritsuHoshuDto gennyouritsuHoshuDto { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public GennyouritsuHoshuLogic(GennyouritsuHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_GENNYOURITSUDao>();
            this.houkokushoDao = DaoInitUtility.GetComponent<IM_HOUKOKUSHO_BUNRUIDao>();
            this.haikiNameDao = DaoInitUtility.GetComponent<IM_HAIKI_NAMEDao>();
            this.shobunHouhouDao = DaoInitUtility.GetComponent<IM_SHOBUN_HOUHOUDao>();
            this.gennyouritsuHoshuDto = new GennyouritsuHoshuDto();
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
                this.form.Ichiran.ReadOnly = true;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

                // 報告書分類指定の初期設定
                this.form.HOUKOKUSHO_BUNRUI_NAME_RYAKU.Text = Properties.Settings.Default.HoukokushoBunruiNameRyaku_Text;
                this.form.HOUKOKUSHO_BUNRUI_CD.Text = Properties.Settings.Default.HoukokushoBunruiCd_Text;
                this.form.HOUKOKUSHO_BUNRUI_CD.DBFieldsName = Properties.Settings.Default.HoukokushoBunruiCd_DBFieldsName;
                this.form.HOUKOKUSHO_BUNRUI_CD.ItemDefinedTypes = Properties.Settings.Default.HoukokushoBunruiCd_ItemDefinedTypes;
                this.form.Ichiran.AllowUserToAddRows = false; // thongh 2015/12/28 #1979

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

                // プロパティ設定有無チェック
                if (this.gennyouritsuHoshuDto.PropertiesHaikiNameExistCheck())
                {
                    // 廃棄名称マスタの条件で取得
                    this.SearchResult = haikiNameDao.GetIchiranDataSqlFile(this.GET_ICHIRAN_HAIKI_NAME_DATA_SQL
                                            , this.gennyouritsuHoshuDto.HaikiNameSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }
                else if (this.gennyouritsuHoshuDto.PropertiesShobunHouhouExistCheck())
                {
                    // 処分方法マスタの条件で取得
                    this.SearchResult = shobunHouhouDao.GetIchiranDataSqlFile(this.GET_ICHIRAN_SHOBUN_HOUHOU_DATA_SQL
                                            , this.gennyouritsuHoshuDto.ShobunHouhouSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }
                else
                {
                    // 減容率マスタの条件で取得
                    this.SearchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_GENNYOURITSU_DATA_SQL
                                            , this.gennyouritsuHoshuDto.GennyouritsuSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }

                this.SearchResultAll = dao.GetDataBySqlFile(this.GET_GENNYOURITSU_DATA_SQL, this.gennyouritsuHoshuDto.AllSearchString);

                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

                // 報告書分類指定の保存
                Properties.Settings.Default.HoukokushoBunruiNameRyaku_Text = this.form.HOUKOKUSHO_BUNRUI_NAME_RYAKU.Text;
                Properties.Settings.Default.HoukokushoBunruiCd_Text = this.form.HOUKOKUSHO_BUNRUI_CD.Text;
                Properties.Settings.Default.HoukokushoBunruiCd_DBFieldsName = this.form.HOUKOKUSHO_BUNRUI_CD.DBFieldsName;
                Properties.Settings.Default.HoukokushoBunruiCd_ItemDefinedTypes = this.form.HOUKOKUSHO_BUNRUI_CD.ItemDefinedTypes;

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

                var entityList = new M_GENNYOURITSU[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_GENNYOURITSU();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_GENNYOURITSU>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.GennyouritsuHoshuConstans.TIME_STAMP))
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

                var GennyouritsuEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<DataDto> addList = new List<DataDto>();
                foreach (var GennyouritsuEntity in GennyouritsuEntityList)
                {
                    DataDto data = new DataDto();
                    GennyouritsuEntity.HOUKOKUSHO_BUNRUI_CD = this.form.HOUKOKUSHO_BUNRUI_CD.Text;

                    foreach (Row row in this.form.Ichiran.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }

                        if (row.Cells.Any(n => (n.DataField.Equals(Const.GennyouritsuHoshuConstans.HAIKI_NAME_CD) && n.Value.ToString().Equals(GennyouritsuEntity.HAIKI_NAME_CD))) &&
                            row.Cells.Any(n => (n.DataField.Equals(Const.GennyouritsuHoshuConstans.SHOBUN_HOUHOU_CD) && n.Value.ToString().Equals(GennyouritsuEntity.SHOBUN_HOUHOU_CD))) &&
                            row.Cells.Any(n => (n.DataField.Equals(Const.GennyouritsuHoshuConstans.DELETE_FLG) && bool.Parse(n.FormattedValue.ToString()) == isDelete)))
                        {
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), GennyouritsuEntity);
                            data.entity = GennyouritsuEntity;
                            DataRow tempRowData = ((DataTable)this.form.Ichiran.DataSource).Rows[row.Index];
                            if (tempRowData != null)
                            {
                                data.updateKey.HOUKOKUSHO_BUNRUI_CD = tempRowData["UK_HOUKOKUSHO_BUNRUI_CD"] != DBNull.Value ? tempRowData["UK_HOUKOKUSHO_BUNRUI_CD"].ToString() : null;
                                data.updateKey.HAIKI_NAME_CD = tempRowData["UK_HAIKI_NAME_CD"] != DBNull.Value ? tempRowData["UK_HAIKI_NAME_CD"].ToString() : null;
                                data.updateKey.SHOBUN_HOUHOU_CD = tempRowData["UK_SHOBUN_HOUHOU_CD"] != DBNull.Value ? tempRowData["UK_SHOBUN_HOUHOU_CD"].ToString() : null;
                            }
                            addList.Add(data);
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
                this.form.Ichiran.AllowUserToAddRows = false; // thongh 2015/12/28 #1979

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
        /// 廃棄物名称CD、所分方法CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                GennyouritsuHoshuValidator vali = new GennyouritsuHoshuValidator();
                bool result = vali.HaikiNameCDValidator(this.form.Ichiran, this.SearchResult, this.SearchResultAll, this.isAllSearch);

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
                msgLogic.MessageBoxShow("C011", "減容率一覧表");

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

                bool catchErr = false;
                if (this.SearchResult != null)
                {
                    DataRow[] dr = this.SearchResult.Select(String.Format("HOUKOKUSHO_BUNRUI_CD = '{0}'", this.form.HOUKOKUSHO_BUNRUI_CD.Text));

                    DataTable table = this.SearchResult.Clone();

                    foreach (DataRow row in dr)
                    {
                        table.ImportRow(row);
                    }

                    if (dr == null)
                    {
                        return catchErr;
                    }

                    table.BeginLoadData();

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        table.Columns[i].ReadOnly = false;
                    }

                    this.form.Ichiran.DataSource = table;
                }

                LogUtility.DebugMethodEnd(catchErr);
                return catchErr;
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
            // 重複エラー時用のメッセージで使用
            string dispHaikiNameCd = string.Empty;
            string dispSbnHouhouCd = string.Empty;

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
                        foreach (DataDto data in this.entitys)
                        {
                            M_GENNYOURITSU entity = this.dao.GetDataByCd(data.updateKey);
                            dispHaikiNameCd = string.Empty;
                            dispSbnHouhouCd = string.Empty;

                            if (entity == null)
                            {
                                //削除チェックが付けられている場合は、新規登録を行わない
                                if (data.entity.DELETE_FLG)
                                {
                                    continue;
                                }

                                this.dao.Insert(data.entity);
                            }
                            else
                            {
                                dispHaikiNameCd = data.entity.HAIKI_NAME_CD;
                                dispSbnHouhouCd = data.entity.SHOBUN_HOUHOU_CD;
                                this.dao.UpdateBySqlFile(this.UPDATE_GENNRYOURITSU_DATA_SQL, data.entity, data.updateKey);
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

                var tempEx = ex2.Args[0] as SqlException;
                if ((!string.IsNullOrEmpty(dispHaikiNameCd)
                    || !string.IsNullOrEmpty(dispSbnHouhouCd))
                    && (tempEx != null && tempEx.Number == Constans.SQL_EXCEPTION_NUMBER_DUPLICATE))
                {
                    this.form.errmessage.MessageBoxShow("E259", "減容率または備考", "・廃棄物名称CD：" + dispHaikiNameCd + "、処分方法CD：" + dispSbnHouhouCd);
                }
                else
                {
                    this.form.errmessage.MessageBoxShow("E093", "");
                }

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
                        foreach (DataDto data in this.entitys)
                        {
                            M_GENNYOURITSU entity = this.dao.GetDataByCd(data.entity);
                            if (entity != null)
                            {
                                this.dao.Update(data.entity);
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

            GennyouritsuHoshuLogic localLogic = other as GennyouritsuHoshuLogic;
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
        /// 報告書分類未指定時ボタン押下処理
        /// </summary>
        [Transaction]
        public virtual bool NotSetHoukoushoBunruiCD(out bool catchErr)
        {
            try
            {
                bool result = true;
                catchErr = false;

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (string.IsNullOrEmpty(this.form.HOUKOKUSHO_BUNRUI_CD.Text))
                {
                    msgLogic.MessageBoxShow("E027", "報告書分類");
                    this.form.HOUKOKUSHO_BUNRUI_CD.Focus();
                    result = false;
                }

                return result;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("NotSetHoukoushoBunruiCD", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("NotSetHoukoushoBunruiCD", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 必須項目内容チェック
        /// </summary>
        [Transaction]
        public virtual bool CheckSetCD()
        {
            bool result = true;

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            for (int i = 0; i < this.form.Ichiran.Rows.Count; i++)
            {
                if (i >= this.form.Ichiran.Rows.Count - 1)
                {
                    break;
                }

                Row row = this.form.Ichiran.Rows[i];

                //廃棄物CDは""可能とする
                /*if (string.IsNullOrEmpty(eRow.Cells[Const.GennyouritsuHoshuConstans.HAIKI_NAME_CD].Value.ToString()))
                {
                    msgLogic.MessageBoxShow("E001", "廃棄物名称");
                    this.form.Ichiran.CurrentCellPosition =
                        new GrapeCity.Win.MultiRow.CellPosition(i, eRow.Cells[Const.GennyouritsuHoshuConstans.HAIKI_NAME_CD].CellIndex);
                    this.form.Ichiran.Focus();
                    result = false;
                    break;
                }*/

                if (string.IsNullOrEmpty(row.Cells[Const.GennyouritsuHoshuConstans.SHOBUN_HOUHOU_CD].Value.ToString()))
                {
                    msgLogic.MessageBoxShow("E001", "処分方法");
                    this.form.Ichiran.CurrentCellPosition =
                        new GrapeCity.Win.MultiRow.CellPosition(i, row.Cells[Const.GennyouritsuHoshuConstans.SHOBUN_HOUHOU_CD].CellIndex);
                    this.form.Ichiran.Focus();
                    result = false;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        public bool SetIchiran()
        {
            try
            {
                if (this.SearchResult != null)
                {
                    DataRow[] dr = this.SearchResult.Select(String.Format("HOUKOKUSHO_BUNRUI_CD = '{0}'", this.form.HOUKOKUSHO_BUNRUI_CD.Text));

                    DataTable table = this.SearchResult.Clone();

                    foreach (DataRow row in dr)
                    {
                        table.ImportRow(row);
                    }

                    if (dr == null)
                    {
                        return false;
                    }

                    table.BeginLoadData();

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        table.Columns[i].ReadOnly = false;
                    }

                    this.form.Ichiran.DataSource = table;
                }

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M221", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
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
            M_GENNYOURITSU entityGennyouritu = new M_GENNYOURITSU();
            M_GENNYOURITSU entityAll = new M_GENNYOURITSU();
            M_HAIKI_NAME entityHaikiName = new M_HAIKI_NAME();
            M_SHOBUN_HOUHOU entityShobunHouhou = new M_SHOBUN_HOUHOU();

            // 報告書分類の入力チェック
            if (string.IsNullOrEmpty(this.form.HOUKOKUSHO_BUNRUI_CD.Text))
            {
                return;
            }

            this.form.HOUKOKUSHO_BUNRUI_CD.DBFieldsName = "HOUKOKUSHO_BUNRUI_CD";
            this.form.HOUKOKUSHO_BUNRUI_CD.ItemDefinedTypes = "varchar";

            entityGennyouritu.SetValue(this.form.HOUKOKUSHO_BUNRUI_CD);
            entityAll.SetValue(this.form.HOUKOKUSHO_BUNRUI_CD);

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    bool isExistGennyouritu = this.EntityExistCheck(entityGennyouritu, this.form.CONDITION_VALUE.DBFieldsName);
                    bool isExistHaikiName = this.EntityExistCheck(entityHaikiName, this.form.CONDITION_VALUE.DBFieldsName);

                    if (isExistGennyouritu)
                    {
                        if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) 
                            && this.form.CONDITION_VALUE.DBFieldsName.Equals("GENNYOURITSU"))
                        {
                            decimal outNumeric = 0;
                            if (decimal.TryParse(this.form.CONDITION_VALUE.Text, out outNumeric))
                            {
                                entityGennyouritu.SetValue(this.form.CONDITION_VALUE);
                            }
                            else
                            {
                                entityGennyouritu.GENNYOURITSU = SqlDecimal.Parse("-9999");
                            }
                        }
                        else
                        {
                            // 検索条件の設定(減容率マスタ)
                            entityGennyouritu.SetValue(this.form.CONDITION_VALUE);
                        }
                    }
                    else if (isExistHaikiName)
                    {
                        // 検索条件の設定(廃棄物名称マスタ)
                        entityHaikiName.SetValue(this.form.CONDITION_VALUE);
                    }
                    else
                    {
                        // 検索条件の設定(処分方法マスタ)
                        entityShobunHouhou.SetValue(this.form.CONDITION_VALUE);
                    }
                }
            }

            gennyouritsuHoshuDto.HaikiNameSearchString = entityHaikiName;
            gennyouritsuHoshuDto.ShobunHouhouSearchString = entityShobunHouhou;
            gennyouritsuHoshuDto.GennyouritsuSearchString = entityGennyouritu;
            gennyouritsuHoshuDto.AllSearchString = entityAll;
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

            // 報告書分類指定の初期化
            this.form.HOUKOKUSHO_BUNRUI_CD.Text = string.Empty;
            this.form.HOUKOKUSHO_BUNRUI_NAME_RYAKU.Text = string.Empty;
            this.form.Ichiran.ReadOnly = true;
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
        /// 検索条件が数字のみ入力.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONDITION_VALUE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.form.CONDITION_VALUE.DBFieldsName.Equals(Const.GennyouritsuHoshuConstans.GENNYOURITSU))
            {
                if (e.KeyChar == '-')
                {
                }
                else if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Tab && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }
    }
}