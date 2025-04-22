using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using HaikiShuruiHoshu.APP;
using HaikiShuruiHoshu.Dto;
using HaikiShuruiHoshu.Validator;
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

namespace HaikiShuruiHoshu.Logic
{
    /// <summary>
    /// 廃棄種類保守画面のビジネスロジック
    /// </summary>
    public class HaikiShuruiHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "HaikiShuruiHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_HAIKI_SHURUI_DATA_SQL = "HaikiShuruiHoshu.Sql.GetIchiranDataSql.sql";
        private readonly string GET_ICHIRAN_HOUKOKUSHO_BUNRUI_DATA_SQL = "HaikiShuruiHoshu.Sql.GetIchiranHoukokushoBunruiDataSql.sql";
        private readonly string GET_HAIKI_SHURUI_DATA_SQL = "HaikiShuruiHoshu.Sql.GetHaikiShuruidataSql.sql";
        private readonly string CHECK_DELETE_HAIKI_SHURUI_SQL = "HaikiShuruiHoshu.Sql.CheckDeleteHaikiShuruiSql.sql";
        private readonly string CHECK_DELETE_HAIKI_SHURUI_TSUMIKAE_SQL = "HaikiShuruiHoshu.Sql.CheckDeleteHaikiShuruiTsumikaeSql.sql";
        private readonly string CHECK_DELETE_HAIKI_SHURUI_KENPAI_SQL = "HaikiShuruiHoshu.Sql.CheckDeleteHaikiShuruiKenpaiSql.sql";

        private IM_HAIKI_KBNDao haikiKBNDao;

        /// <summary>
        /// 廃棄物種類保守画面Form
        /// </summary>
        private HaikiShuruiHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_HAIKI_SHURUI[] entitys;

        private bool isAllSearch;

        /// <summary>
        /// 廃棄物種類のDao
        /// </summary>
        private IM_HAIKI_SHURUIDao dao;

        /// <summary>
        /// 報告書分類のDao
        /// </summary>
        private IM_HOUKOKUSHO_BUNRUIDao daoHoukokushoBunrui;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// システム設定のエンティティ
        /// </summary>
        private M_SYS_INFO entitySysInfo;

        /// <summary>
        /// 検索条件(Dto)
        /// </summary>
        public HaikiShuruiHoshuDto haikiShuruiHoshuDto { get; set; }

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
        public M_HAIKI_SHURUI SearchString { get; set; }

        /// <summary>
        /// 廃棄物種類CD
        /// </summary>
        public string HaiKBNCd { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public HaikiShuruiHoshuLogic(HaikiShuruiHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_HAIKI_SHURUIDao>();
            this.daoHoukokushoBunrui = DaoInitUtility.GetComponent<IM_HOUKOKUSHO_BUNRUIDao>();
            this.haikiShuruiHoshuDto = new HaikiShuruiHoshuDto();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            this.entitySysInfo = null;
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            if (sysInfo != null && sysInfo.Length > 0)
            {
                this.entitySysInfo = sysInfo[0];
            }

            this.haikiKBNDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_HAIKI_KBNDao>();

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

                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED; ;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }
                this.form.Ichiran.AllowUserToAddRows = false; // thongh 2015/12/28 #1979
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

                this.form.HAIKI_KBN_CD.Text = this.HaiKBNCd;
                if (!this.form.HAIKI_KBN_CD.Text.Equals(""))
                {
                    M_HAIKI_KBN condition = new M_HAIKI_KBN();
                    condition.HAIKI_KBN_CD = Convert.ToInt16(this.form.HAIKI_KBN_CD.Text);
                    M_HAIKI_KBN[] haikiKBN = this.haikiKBNDao.GetAllValidData(condition);
                    if (haikiKBN != null)
                    {
                        this.form.HAIKI_KBN_NAME_RYAKU.Text = haikiKBN[0].HAIKI_KBN_NAME_RYAKU;
                    }
                }

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
                if (this.haikiShuruiHoshuDto.PropertiesHoukokushoBunruiExistCheck())
                {
                    // 報告書区分マスタの条件で取得
                    this.SearchResult = daoHoukokushoBunrui.GetIchiranHokokuDataSqlFile(this.GET_ICHIRAN_HOUKOKUSHO_BUNRUI_DATA_SQL
                                            , this.haikiShuruiHoshuDto.HoukokushoBunruiSearchString
                                            , this.haikiShuruiHoshuDto.HaikiShuruiSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                    this.SearchResultCheck = daoHoukokushoBunrui.GetIchiranHokokuDataSqlFile(this.GET_ICHIRAN_HOUKOKUSHO_BUNRUI_DATA_SQL
                                            , this.haikiShuruiHoshuDto.HoukokushoBunruiSearchString
                                            , this.haikiShuruiHoshuDto.HaikiShuruiSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }
                else
                {
                    //  廃棄物種類マスタの条件で取得
                    this.SearchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_HAIKI_SHURUI_DATA_SQL
                                                                , this.haikiShuruiHoshuDto.HaikiShuruiSearchString
                                                                , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                    this.SearchResultCheck = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_HAIKI_SHURUI_DATA_SQL
                                                                , this.haikiShuruiHoshuDto.HaikiShuruiSearchString
                                                                , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }

                M_HAIKI_SHURUI cond = new M_HAIKI_SHURUI();
                cond.HAIKI_KBN_CD = Int16.Parse(this.form.HAIKI_KBN_CD.Text);
                this.SearchResultAll = dao.GetDataBySqlFile(this.GET_HAIKI_SHURUI_DATA_SQL, cond);

                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;
                Properties.Settings.Default.HaikiKbnValue_Text = this.form.HAIKI_KBN_CD.Text;
                Properties.Settings.Default.HaikiKbnName_Text = this.form.HAIKI_KBN_NAME_RYAKU.Text;

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

                var entityList = new M_HAIKI_SHURUI[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_HAIKI_SHURUI();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_HAIKI_SHURUI>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                if (!isDelete)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        // NOT NULL制約を一時的に解除(新規追加行対策)
                        column.AllowDBNull = true;

                        // TIME_STAMPがなぜか一意制約有のため、解除
                        if (column.ColumnName.Equals(Const.HaikiShuruiHoshuConstans.TIME_STAMP))
                            column.Unique = false;
                    }

                    dt.BeginLoadData();

                    preDt = GetCloneDataTable(dt);

                    // 変更分のみ取得
                    this.form.Ichiran.DataSource = dt.GetChanges();
                }

                var HaikishuruiEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<M_HAIKI_SHURUI> addList = new List<M_HAIKI_SHURUI>();
                foreach (var haikishuruiEntity in HaikishuruiEntityList)
                {
                    foreach (Row row in this.form.Ichiran.Rows)
                    {
                        if (row.Cells.Any(n => (n.DataField.Equals(Const.HaikiShuruiHoshuConstans.HAIKI_SHURUI_CD) && n.Value.ToString().Equals(haikishuruiEntity.HAIKI_SHURUI_CD))) &&
                            row.Cells.Any(n => (n.DataField.Equals(Const.HaikiShuruiHoshuConstans.DELETE_FLG) && bool.Parse(n.FormattedValue.ToString()) == isDelete)))
                        {
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), haikishuruiEntity);
                            addList.Add(haikishuruiEntity);
                            break;
                        }
                    }
                }

                if (!isDelete)
                {
                    this.form.Ichiran.DataSource = preDt;
                }

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

                if (this.form.Ichiran.DataSource != null)
                {
                    // 一覧クリア
                    var table = (DataTable)this.form.Ichiran.DataSource;
                    this.form.Ichiran.DataSource = table.Clone();
                }

                LogUtility.DebugMethodEnd();
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

                HaikiShuruiHoshuValidator vali = new HaikiShuruiHoshuValidator();
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
        /// 削除できるかどうかチェックする
        /// </summary>
        public bool CheckDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                var haikiShuruiCd = string.Empty;
                string[] strList;

                foreach (Row gcRwos in this.form.Ichiran.Rows)
                {
                    if (gcRwos.Cells["DELETE_FLG"].Value != null && gcRwos.Cells["DELETE_FLG"].Value.ToString() == "True")
                    {
                        if (gcRwos.Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(gcRwos.Cells["CREATE_USER"].Value.ToString()))
                        {
                            continue;
                        }
                        haikiShuruiCd += gcRwos.Cells["HAIKI_SHURUI_CD"].Value.ToString() + ",";
                    }
                }

                if (!string.IsNullOrEmpty(haikiShuruiCd))
                {
                    haikiShuruiCd = haikiShuruiCd.Substring(0, haikiShuruiCd.Length - 1);
                    strList = haikiShuruiCd.Split(',');
                    // 廃棄区分コードにより、使用するSQLを分ける。
                    int haikiKbn = Int16.Parse(this.form.HAIKI_KBN_CD.Text);
                    string sqlPath = "";
                    // 産廃の場合
                    if (haikiKbn == 1)
                    {
                        sqlPath = this.CHECK_DELETE_HAIKI_SHURUI_SQL;
                    }
                    // 建廃の場合
                    else if (haikiKbn == 2)
                    {
                        sqlPath = this.CHECK_DELETE_HAIKI_SHURUI_KENPAI_SQL;
                    }
                    // 積替の場合
                    else if (haikiKbn == 3)
                    {
                        sqlPath = this.CHECK_DELETE_HAIKI_SHURUI_TSUMIKAE_SQL;
                    }
                    DataTable dtTable = dao.GetDataBySqlFileCheck(sqlPath, strList);
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        string strName = string.Empty;

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            strName += "\n" + dr["NAME"].ToString();
                        }

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E258", "廃棄物種類", "廃棄物種類CD", strName);

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
            msgLogic.MessageBoxShow("C011", "廃棄物種類一覧表");

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
        /// 検索結果を一覧に設定
        /// </summary>
        public bool SetIchiran()
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

                // テーブル固定値定義書に存在するデータの場合、削除、名称、略称、適用期間を変更不可に修正
                this.SetFixedIchiran();

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M229", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
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
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetIchiran", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
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
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                //独自チェックの記述例を書く
                if (!String.IsNullOrEmpty(this.form.HAIKI_KBN_CD.Text.ToString()))
                {
                    //エラーではない場合登録処理を行う
                    if (!errorFlag)
                    {
                        // トランザクション開始
                        using (var tran = new Transaction())
                        {
                            foreach (M_HAIKI_SHURUI haikishuruiEntity in this.entitys)
                            {
                                haikishuruiEntity.HAIKI_KBN_CD = Int16.Parse(this.form.HAIKI_KBN_CD.Text);
                                M_HAIKI_SHURUI m = this.dao.GetDataByCd(haikishuruiEntity);
                                if (m == null)
                                {
                                    //削除チェックが付けられている場合は、新規登録を行わない
                                    if (haikishuruiEntity.DELETE_FLG)
                                    {
                                        continue;
                                    }

                                    this.dao.Insert(haikishuruiEntity);
                                }
                                else
                                {
                                    this.dao.Update(haikishuruiEntity);
                                }
                            }
                            // トランザクション終了
                            tran.Commit();
                        }
                        msgLogic.MessageBoxShow("I001", "登録");
                    }
                }
                else
                {
                    msgLogic.MessageBoxShow("E012", "廃棄物区分");
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
                        foreach (M_HAIKI_SHURUI haikishuruiEntity in this.entitys)
                        {
                            haikishuruiEntity.HAIKI_KBN_CD = Int16.Parse(this.form.HAIKI_KBN_CD.Text);
                            M_HAIKI_SHURUI entity = this.dao.GetDataByCd(haikishuruiEntity);
                            if (entity != null)
                            {
                                this.dao.Update(haikishuruiEntity);
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

            HaikiShuruiHoshuLogic localLogic = other as HaikiShuruiHoshuLogic;
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

            //プレビュ機能削除
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

            // CD表示変更
            //this.form.Ichiran.CellFormatting += new EventHandler<CellFormattingEventArgs>(this.form.CdFormatting);

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
            M_HAIKI_SHURUI entity = new M_HAIKI_SHURUI();
            M_HOUKOKUSHO_BUNRUI entityHoukokushoBunrui = new M_HOUKOKUSHO_BUNRUI();

            if (!string.IsNullOrEmpty(this.form.HAIKI_KBN_CD.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.HAIKI_KBN_CD.ItemDefinedTypes) && !string.IsNullOrEmpty(this.form.HAIKI_KBN_CD.Text))
                {
                    entity.HAIKI_KBN_CD = Int16.Parse(this.form.HAIKI_KBN_CD.Text);
                }
            }

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    bool isExist = this.EntityExistCheck(entity, this.form.CONDITION_VALUE.DBFieldsName);

                    if (isExist)
                    {
                        // 検索条件の設定(廃棄物種類マスタ)
                        entity.SetValue(this.form.CONDITION_VALUE);
                    }
                    else
                    {
                        // 検索条件の設定(報告書分類マスタ)
                        entityHoukokushoBunrui.SetValue(this.form.CONDITION_VALUE);
                    }
                }
            }
            haikiShuruiHoshuDto.HaikiShuruiSearchString = entity;
            haikiShuruiHoshuDto.HoukokushoBunruiSearchString = entityHoukokushoBunrui;
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

            this.form.HAIKI_KBN_CD.Text = string.Empty;
            this.form.HAIKI_KBN_NAME_RYAKU.Text = string.Empty;
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

        ///// <summary>
        ///// CD表示変更処理
        ///// </summary>
        ///// <param name="e"></param>
        //public void CdFormatting(CellFormattingEventArgs e)
        //{
        //    if ((e.Value == null) || (string.IsNullOrEmpty(e.Value.ToString())))
        //    {
        //        return;
        //    }

        //    // ゼロパディング後、テキストへ設定
        //    e.Value = String.Format("{0:D" + HaikiShuruiHoshuConstans.CD_MAXLENGTH + "}", int.Parse(e.Value.ToString()));
        //}

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
            var objValue = row[Const.HaikiShuruiHoshuConstans.HAIKI_SHURUI_CD].Value;

            bool catchErr = false;
            if (this.CheckFixedRow(row, out catchErr) && !catchErr)
            {
                foreach (var columnName in Const.HaikiShuruiHoshuConstans.fixedColumnList)
                {
                    row[columnName].ReadOnly = true;
                    row[columnName].Selectable = false;
                    row[columnName].UpdateBackColor(false);
                }
            }
            else if (catchErr)
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
                var objValue = row[Const.HaikiShuruiHoshuConstans.HAIKI_SHURUI_CD].Value;

                if (row != null && objValue != null)
                {
                    var strCd = objValue.ToString();
                    int haikiKbn;

                    if (int.TryParse(this.form.HAIKI_KBN_CD.Text, out haikiKbn))
                    {
                        if (haikiKbn == 1 && Const.HaikiShuruiHoshuConstans.fixedRowListKbn1.Contains(strCd))
                        {
                            return true;
                        }
                        else if (haikiKbn == 2 && Const.HaikiShuruiHoshuConstans.fixedRowListKbn2.Contains(strCd))
                        {
                            return true;
                        }
                        else if (haikiKbn == 3 && Const.HaikiShuruiHoshuConstans.fixedRowListKbn3.Contains(strCd))
                        {
                            return true;
                        }
                    }

                    return false;
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
    }
}