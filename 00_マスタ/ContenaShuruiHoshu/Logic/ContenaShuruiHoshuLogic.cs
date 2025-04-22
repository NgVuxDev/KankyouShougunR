// $Id: ContenaShuruiHoshuLogic.cs 4068 2013-10-18 04:37:06Z sys_dev_23 $
using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Collections.Generic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using r_framework.CustomControl;
using Seasar.Quill.Attrs;
using ContenaShuruiHoshu.APP;
using ContenaShuruiHoshu.Const;
using Shougun.Core.Common.BusinessCommon.Utility;
using System.Collections;

namespace ContenaShuruiHoshu.Logic
{
    /// <summary>
    /// コンテナ種類画面のビジネスロジック
    /// </summary>
    public class ContenaShuruiHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "ContenaShuruiHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_CONTENA_SHURUI_DATA_SQL = "ContenaShuruiHoshu.Sql.GetIchiranDataSql.sql";

        private readonly string GET_CONTENA_SHURUI_DATA_SQL = "ContenaShuruiHoshu.Sql.GetContenaShuRuiDataSql.sql";

        private bool isSelect = false;

        /// <summary>
        /// コンテナ種類画面Form
        /// </summary>
        private ContenaShuruiHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_CONTENA_SHURUI[] entitys;

        /// <summary>
        /// コンテナ種類のDao
        /// </summary>
        private IM_CONTENA_SHURUIDao dao;

        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }
        /// <summary>
        /// 検索条件
        /// </summary>
        public M_CONTENA_SHURUI SearchString { get; set; }

        private bool isAllSearch;

        /// <summary>
        /// 検索結果(全件)
        /// </summary>
        public DataTable SearchResultAll { get; set; }

        public DataTable dtDetailList = new DataTable();

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ContenaShuruiHoshuLogic(ContenaShuruiHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_CONTENA_SHURUIDao>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            LogUtility.DebugMethodStart();

            // ボタンのテキストを初期化
            this.ButtonInit();

            // イベントの初期化処理
            this.EventInit();

            this.allControl = this.form.allControl;

            // システム情報を取得し、初期値をセットする
            GetSysInfoInit();

            this.form.CONDITION_VALUE.Text = "";
            this.form.CONDITION_VALUE.DBFieldsName = "";
            this.form.CONDITION_VALUE.ItemDefinedTypes = "";
            this.form.CONDITION_ITEM.Text = "";

            LogUtility.DebugMethodEnd();
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

            // 削除ボタン(F4)イベント生成
            this.form.C_Regist(parentForm.bt_func4);
            parentForm.bt_func4.Click += new EventHandler(this.form.LogicalDelete);
            parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

            //CSV出力ボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.CSVOutput);

            //条件クリアボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.ClearCondition);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            //登録ボタン(F9)イベント生成
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //取消ボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            //[1]ボタン
            parentForm.bt_process1.Text = "[1]";
            parentForm.bt_process1.Enabled = false;

            //[2]ボタン
            parentForm.bt_process2.Text = "[2]";
            parentForm.bt_process2.Enabled = false;

            //[3]ボタン
            parentForm.bt_process3.Text = "[3]";
            parentForm.bt_process3.Enabled = false;

            //[4]ボタン
            parentForm.bt_process4.Text = "[4]";
            parentForm.bt_process4.Enabled = false;

            //[5]ボタン
            parentForm.bt_process5.Text = "[5]";
            parentForm.bt_process5.Enabled = false;
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
            LogUtility.DebugMethodStart();
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
                    foreach (M_CONTENA_SHURUI contenashuruiEntity in this.entitys)
                    {
                        if (contenashuruiEntity.CONTENA_SHURUI_CD == null)
                        {
                            msgLogic.MessageBoxShow("E075", "削除");
                            return;
                        }
                        M_CONTENA_SHURUI entity = this.dao.GetDataByCd(contenashuruiEntity.CONTENA_SHURUI_CD.ToString());
                        if (entity != null)
                        {
                            String PCName = System.Environment.MachineName;
                            String UsrName = System.Environment.UserName;
                            UsrName = UsrName.Length > 16 ? UsrName.Substring(0, 16) : UsrName;
                            contenashuruiEntity.DELETE_FLG = true;
                            contenashuruiEntity.UPDATE_USER = UsrName;
                            contenashuruiEntity.UPDATE_DATE = DateTime.Now;
                            contenashuruiEntity.UPDATE_PC = PCName;

                            this.dao.Update(contenashuruiEntity);
                        }
                    }
                    msgLogic.MessageBoxShow("I001", "削除");
                }
            }
            
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        public void CSVOutput()
        {
            LogUtility.DebugMethodStart();
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
            {
                CSVFileLogic csvLogic = new CSVFileLogic();
                CSVExport csvExport = new CSVExport();
                csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true,false);
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        public void CancelCondition()
        {
            LogUtility.DebugMethodStart();
            ClearCondition();
            SetIchiran();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            SetSearchString();

            this.SearchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_CONTENA_SHURUI_DATA_SQL
                                                        , this.SearchString
                                                        , this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked
                                                        , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked
                                                        , this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked);
            this.SearchResultAll = dao.GetDataBySqlFile(this.GET_CONTENA_SHURUI_DATA_SQL, new M_CONTENA_SHURUI());

            this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

            Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
            Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_DBFIELD.Text;
            Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_TYPE.Text;
            Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

            Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;
            Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked;
            Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked;

            Properties.Settings.Default.Save();

            dtDetailList = this.SearchResult.Copy();

            int count = 0;
            if (this.SearchResult.Rows != null && this.SearchResult.Rows.Count > 0)
            {
                count = 1;
            }
            
            LogUtility.DebugMethodEnd(count);
            
            return count;
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            //独自チェックの記述例を書く

            //エラーではない場合登録処理を行う
            if (!errorFlag)
            {
                foreach (M_CONTENA_SHURUI contenashuruiEntity in this.entitys)
                {
                    M_CONTENA_SHURUI entity = this.dao.GetDataByCd(contenashuruiEntity.CONTENA_SHURUI_CD.ToString());
                    String PCName = System.Environment.MachineName;
                    contenashuruiEntity.UPDATE_PC = PCName;
                    if (entity == null)
                    {
                        contenashuruiEntity.CREATE_PC = PCName;
                        this.dao.Insert(contenashuruiEntity);
                    }
                    else
                    {
                        this.dao.Update(contenashuruiEntity);
                    }
                }                                
                msgLogic.MessageBoxShow("I001", "登録");
            }
            LogUtility.DebugMethodEnd();
        }
        
        /// <summary>
        /// 取消処理
        /// </summary>
        public void Cancel()
        {
            LogUtility.DebugMethodStart();

            ClearCondition();
            SetSearchString();

            LogUtility.DebugMethodEnd();
        }
        
        /// <summary>
        /// コンテナ種類CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck(string contena_Shurui_Cd)
        {
            LogUtility.DebugMethodStart();

            // 画面で種類CD重複チェック
            int recCount = 0;
            for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
            {
                if (this.form.Ichiran.Rows[i].Cells[Const.ContenaShuruiHoshuConstans.CONTENA_SHURUI_CD].Value.Equals(Convert.ToString(contena_Shurui_Cd)))
                {
                    recCount++;
                }
            }

            if (recCount > 1)
            {
                return true;
            }

            // 検索結果で種類CD重複チェック
            for (int i = 0; i < dtDetailList.Rows.Count; i++)
            {
                if (contena_Shurui_Cd.Equals(dtDetailList.Rows[i][1]))
                {
                    return false;
                }
 
            }

            // DBで種類CD重複チェック
            M_CONTENA_SHURUI entity = this.dao.GetDataByCd(contena_Shurui_Cd);

            if (entity != null)
            {
                return true;
            }

            LogUtility.DebugMethodEnd();
            return false;
        }

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        public void ClearCondition()
        {
            this.form.CONDITION_VALUE.Text = string.Empty;
            this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
            this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
            this.form.CONDITION_ITEM.Text = string.Empty;
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = false;
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = false;
        }

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        public void SetSearchString()
        {
            M_CONTENA_SHURUI entity = new M_CONTENA_SHURUI();

            // CONDITION_TYPE
            if (string.IsNullOrEmpty(this.form.CONDITION_TYPE.Text) && 
                !string.IsNullOrEmpty(Properties.Settings.Default.ConditionValue_ItemDefinedTypes))
            {
                this.form.CONDITION_TYPE.Text = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
            }

            // CONDITION_DBFIELD
            if (string.IsNullOrEmpty(this.form.CONDITION_DBFIELD.Text) && 
                !string.IsNullOrEmpty(Properties.Settings.Default.ConditionValue_DBFieldsName))
            {
                this.form.CONDITION_DBFIELD.Text = Properties.Settings.Default.ConditionValue_DBFieldsName;
            }

            this.form.CONDITION_ITEM.Text = this.form.CONDITION_ITEM.Text.Replace("※", "");
            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_TYPE.Text))
                {
                    // 検索条件の設定
                    // 削除
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.Text.Equals("chb_delete"))
                    {
                        if ("TRUE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "1".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "１".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            entity.DELETE_FLG = true;
                            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = true;
                        }
                        if ("FALSE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "0".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "０".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            entity.DELETE_FLG = false;
                            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;
                        }                        
                    }

                    // コンテナ種類CD
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.Text.Equals("CONTENA_SHURUI_CD"))
                    {
                        entity.CONTENA_SHURUI_CD = this.form.CONDITION_VALUE.Text;
                    }

                    // コンテナ種類名
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.Text.Equals("CONTENA_SHURUI_NAME"))
                    {
                        entity.CONTENA_SHURUI_NAME = this.form.CONDITION_VALUE.Text;
                    }

                    // 略称
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.Text.Equals("CONTENA_SHURUI_NAME_RYAKU"))
                    {
                        entity.CONTENA_SHURUI_NAME_RYAKU = this.form.CONDITION_VALUE.Text;
                    }

                    // フリガナ
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.Text.Equals("CONTENA_SHURUI_FURIGANA"))
                    {
                        entity.CONTENA_SHURUI_FURIGANA = this.form.CONDITION_VALUE.Text;
                    }

                    // 備考
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.Text.Equals("CONTENA_SHURUI_BIKOU"))
                    {
                        entity.CONTENA_SHURUI_BIKOU = this.form.CONDITION_VALUE.Text;
                    }

                    // 適用開始日
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.Text.Equals("TEKIYOU_BEGIN"))
                    {
                        entity.SEARCH_TEKIYOU_BEGIN = this.form.CONDITION_VALUE.Text.Replace('/', '-');
                    }

                    // 適用終了日
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.Text.Equals("TEKIYOU_END"))
                    {
                        entity.SEARCH_TEKIYOU_END = this.form.CONDITION_VALUE.Text.Replace('/', '-');
                    }

                    // 更新者
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.Text.Equals("UPDATE_USER"))
                    {
                        entity.UPDATE_USER = this.form.CONDITION_VALUE.Text;
                    }

                    // 更新日
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.Text.Equals("UPDATE_DATE"))
                    {
                        entity.SEARCH_UPDATE_DATE = this.form.CONDITION_VALUE.Text.Replace('/', '-');
                    }

                    // 作成者
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.Text.Equals("CREATE_USER"))
                    {
                        entity.CREATE_USER = this.form.CONDITION_VALUE.Text;
                    }

                    // 作成日
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.Text.Equals("CREATE_DATE"))
                    {
                        entity.SEARCH_CREATE_DATE = this.form.CONDITION_VALUE.Text.Replace('/', '-');
                    }
                }
            }

            this.SearchString = entity;
        }

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal void SetIchiran()
        {
            var table = this.SearchResult;

            table.BeginLoadData();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].ReadOnly = false;
            }

            this.form.Ichiran.DataSource = table;          
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete)
        {
            LogUtility.DebugMethodStart(isDelete);

            var entityList = new M_CONTENA_SHURUI[this.form.Ichiran.Rows.Count - 1];
            for (int i = 0; i < entityList.Length; i++)
            {
                entityList[i] = new M_CONTENA_SHURUI();
            }

            var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_CONTENA_SHURUI>(entityList);
            DataTable dt = this.form.Ichiran.DataSource as DataTable;

            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }

            DataTable preDt = new DataTable();
            foreach (DataColumn column in dt.Columns)
            {
                // NOT NULL制約を一時的に解除(新規追加行対策)
                column.AllowDBNull = true;

                // TIME_STAMPがなぜか一意制約有のため、解除
                if (column.ColumnName.Equals(Const.ContenaShuruiHoshuConstans.TIME_STAMP))
                {
                    column.Unique = false;
                }
            }

            dt.BeginLoadData();
            preDt = GetCloneDataTable(dt);

            List<M_CONTENA_SHURUI> mContenaShuruiList = new List<M_CONTENA_SHURUI>();
            if (isDelete)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells["chb_delete"].Value != null && (bool)this.form.Ichiran.Rows[i].Cells["chb_delete"].Value)
                    {
                        isSelect = true;
                        mContenaShuruiList.Add(CreateEntityForDataGridDelete(this.form.Ichiran.Rows[i]));
                    }
                }
            }           
                                    
            if (isDelete)
            {
                this.entitys = mContenaShuruiList.ToArray();
            }
            else
            {
                // 変更分のみ取得
                List<M_CONTENA_SHURUI> addList = new List<M_CONTENA_SHURUI>();
                if (dt.GetChanges() == null)
                {
                    return false;
                }
                else
                {
                    this.form.Ichiran.DataSource = dt.GetChanges();
                }
                var contenashuruiEntityList = CreateEntityForDataGrid(this.form.Ichiran);
                for (int i = 0; i < contenashuruiEntityList.Count; i++)
                {
                    var contenashuruiEntity = contenashuruiEntityList[i];
                    for (int j = 0; j < this.form.Ichiran.Rows.Count; j++)
                    {
                        bool isFind = false;
                        if (this.form.Ichiran.Rows[j].Cells[Const.ContenaShuruiHoshuConstans.CONTENA_SHURUI_CD].Value.Equals(Convert.ToString(contenashuruiEntity.CONTENA_SHURUI_CD)) &&
                                 bool.Parse(this.form.Ichiran.Rows[j].Cells[Const.ContenaShuruiHoshuConstans.DELETE_FLG].FormattedValue.ToString()) == isDelete)
                        {
                            isFind = true;
                        }

                        if (isFind)
                        {
                            addList.Add(contenashuruiEntity);
                            break;
                        }
                    }
                    this.form.Ichiran.DataSource = preDt;
                    this.entitys = addList.ToArray();                    
                }
            }

            LogUtility.DebugMethodEnd();            
            return true;
        }

        /// <summary>
        /// CreateEntityForDataGrid
        /// </summary>
        internal List<M_CONTENA_SHURUI> CreateEntityForDataGrid(CustomDataGridView gridView)
        {            
            var entityList = new List<M_CONTENA_SHURUI>();
            if (gridView == null)
            {
                return entityList;
            }
            for (int i = 0; i < gridView.RowCount - 1; i++)
            {
                M_CONTENA_SHURUI mContenaShurui = new M_CONTENA_SHURUI();

                // CONTENA_SHURUI_CD
                mContenaShurui.CONTENA_SHURUI_CD = (string)gridView.Rows[i].Cells["CONTENA_SHURUI_CD"].Value;

                // CONTENA_SHURUI_NAME
                mContenaShurui.CONTENA_SHURUI_NAME = (string)gridView.Rows[i].Cells["CONTENA_SHURUI_NAME"].Value;

                // CONTENA_SHURUI_NAME_RYAKU
                mContenaShurui.CONTENA_SHURUI_NAME_RYAKU = (string)gridView.Rows[i].Cells["CONTENA_SHURUI_NAME_RYAKU"].Value;

                // CONTENA_SHURUI_FURIGANA
                mContenaShurui.CONTENA_SHURUI_FURIGANA = (string)gridView.Rows[i].Cells["CONTENA_SHURUI_FURIGANA"].Value;

                // CONTENA_SHURUI_BIKOU
                if (DBNull.Value.Equals(gridView.Rows[i].Cells["CONTENA_SHURUI_BIKOU"].Value))
                {
                    mContenaShurui.CONTENA_SHURUI_BIKOU = "";
                }
                else
                {
                    mContenaShurui.CONTENA_SHURUI_BIKOU = (string)gridView.Rows[i].Cells["CONTENA_SHURUI_BIKOU"].Value;
                }

                // DELETE_FLG
                if (DBNull.Value.Equals(gridView.Rows[i].Cells["DELETE_FLG"].Value))
                {
                    mContenaShurui.DELETE_FLG = false;
                }
                else
                {
                    mContenaShurui.DELETE_FLG = (Boolean)gridView.Rows[i].Cells["DELETE_FLG"].Value;
                }

                // TEKIYOU_BEGIN
                mContenaShurui.TEKIYOU_BEGIN = (DateTime)gridView.Rows[i].Cells["TEKIYOU_BEGIN"].Value;

                // TEKIYOU_END
                mContenaShurui.TEKIYOU_END = (DateTime)gridView.Rows[i].Cells["TEKIYOU_END"].Value;

                // UPDATE_USER
                if (DBNull.Value.Equals(gridView.Rows[i].Cells["UPDATE_USER"].Value))
                {
                    mContenaShurui.UPDATE_USER = "";
                }
                else
                {
                    mContenaShurui.UPDATE_USER = (string)gridView.Rows[i].Cells["UPDATE_USER"].Value;
                }

                // UPDATE_DATE
                mContenaShurui.UPDATE_DATE = (DateTime)gridView.Rows[i].Cells["UPDATE_DATE"].Value;

                // CREATE_USER
                if (DBNull.Value.Equals(gridView.Rows[i].Cells["CREATE_USER"].Value))
                {
                    mContenaShurui.CREATE_USER = "";
                }
                else
                {
                    mContenaShurui.CREATE_USER = (string)gridView.Rows[i].Cells["CREATE_USER"].Value;
                }

                // CREATE_DATE
                mContenaShurui.CREATE_DATE = (DateTime)gridView.Rows[i].Cells["CREATE_DATE"].Value;

                // CREATE_PC
                mContenaShurui.CREATE_PC = (string)gridView.Rows[i].Cells["CREATE_PC"].Value;

                // UPDATE_PC
                mContenaShurui.UPDATE_PC = (string)gridView.Rows[i].Cells["UPDATE_PC"].Value;

                entityList.Add(mContenaShurui);
            }
            return entityList;
        }
                
        /// <summary>
        /// CreateEntityForDataGridDelete
        /// </summary>
        internal M_CONTENA_SHURUI CreateEntityForDataGridDelete(DataGridViewRow row)
        {
            M_CONTENA_SHURUI mContenaShurui = new M_CONTENA_SHURUI();

            // CONTENA_SHURUI_CD
            if (!DBNull.Value.Equals(row.Cells["CONTENA_SHURUI_CD"].Value))
            {
                mContenaShurui.CONTENA_SHURUI_CD = (string)row.Cells["CONTENA_SHURUI_CD"].Value;
            }

            // CONTENA_SHURUI_NAME
            if (!DBNull.Value.Equals(row.Cells["CONTENA_SHURUI_NAME"].Value))
            {
                mContenaShurui.CONTENA_SHURUI_NAME = (string)row.Cells["CONTENA_SHURUI_NAME"].Value;
            }

            // CONTENA_SHURUI_NAME_RYAKU
            if (!DBNull.Value.Equals(row.Cells["CONTENA_SHURUI_NAME_RYAKU"].Value))
            {
                mContenaShurui.CONTENA_SHURUI_NAME_RYAKU = (string)row.Cells["CONTENA_SHURUI_NAME_RYAKU"].Value;
            }

            // CONTENA_SHURUI_FURIGANA
            if (!DBNull.Value.Equals(row.Cells["CONTENA_SHURUI_FURIGANA"].Value))
            {
                mContenaShurui.CONTENA_SHURUI_FURIGANA = (string)row.Cells["CONTENA_SHURUI_FURIGANA"].Value;
            }

            // CONTENA_SHURUI_BIKOU
            if (!DBNull.Value.Equals(row.Cells["CONTENA_SHURUI_BIKOU"].Value))
            {
                mContenaShurui.CONTENA_SHURUI_BIKOU = (string)row.Cells["CONTENA_SHURUI_BIKOU"].Value;
            }

            // DELETE_FLG
            mContenaShurui.DELETE_FLG = true;

            // TEKIYOU_BEGIN
            mContenaShurui.TEKIYOU_BEGIN = (DateTime)row.Cells["TEKIYOU_BEGIN"].Value;

            // TEKIYOU_END
            mContenaShurui.TEKIYOU_END = DateTime.Now;

            // UPDATE_USER
            if (!DBNull.Value.Equals(row.Cells["UPDATE_USER"].Value))
            {
                mContenaShurui.UPDATE_USER = (string)row.Cells["UPDATE_USER"].Value;
            }

            // CREATE_USER
            if (!DBNull.Value.Equals(row.Cells["CREATE_USER"].Value))
            {
                mContenaShurui.UPDATE_USER = (string)row.Cells["CREATE_USER"].Value;
            }

            // CREATE_DATE
            mContenaShurui.CREATE_DATE = (DateTime)row.Cells["CREATE_DATE"].Value;

            // CREATE_PC
            if (!DBNull.Value.Equals(row.Cells["CREATE_PC"].Value))
            {
                mContenaShurui.UPDATE_USER = (string)row.Cells["CREATE_PC"].Value;
            }

            // UPDATE_PC
            if (!DBNull.Value.Equals(row.Cells["UPDATE_PC"].Value))
            {
                mContenaShurui.UPDATE_USER = (string)row.Cells["UPDATE_PC"].Value;
            }

            return mContenaShurui;
        }
        
        /// <summary>
        /// 登録前のチェック
        /// </summary>
        /// <returns></returns>
        public Boolean CheckBeforeUpdate()
        {
            LogUtility.DebugMethodStart();

            var msgLogic = new MessageBoxShowLogic();

            // 必須入力チェック
            for (int i = 0; i < this.form.Ichiran.RowCount; i++)
            {
                // コンテナ種類CD
                if (DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["CONTENA_SHURUI_CD"].Value))
                {
                    msgLogic.MessageBoxShow("E001", "コンテナ種類CD");
                    return false;
                }

                // コンテナ種類名
                if (DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["CONTENA_SHURUI_NAME"].Value))
                {
                    msgLogic.MessageBoxShow("E001", "コンテナ種類名");
                    return false;
                }

                // 略称
                if (DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["CONTENA_SHURUI_NAME_RYAKU"].Value))
                {
                    msgLogic.MessageBoxShow("E001", "略称");
                    return false;
                }

                // フリガナ
                if (DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["CONTENA_SHURUI_FURIGANA"].Value))
                {
                    msgLogic.MessageBoxShow("E001", "フリガナ");
                    return false;
                }

                // 適用開始日
                if (DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["TEKIYOU_BEGIN"].Value))
                {
                    msgLogic.MessageBoxShow("E001", "適用開始日");
                    return false;
                }
            }

            LogUtility.DebugMethodEnd();
            return true;
        }

        /// <summary>
        /// DataGridViewデータ変更チェック処理
        /// </summary>
        public bool IsDataChanged()
        {
            DataTable dt = this.form.Ichiran.DataSource as DataTable;

            dt.BeginLoadData();

            DataTable dtClone = GetCloneDataTable(dt);

            if (null == dtClone.GetChanges())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// DataGridViewデータ件数チェック処理
        /// </summary>
        public bool ActionBeforeCheck()
        {
            if (this.form.Ichiran.Rows.Count > 1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// NOT NULL制約を一時的に解除
        /// </summary>
        public void ColumnAllowDBNull()
        {
            DataTable dt = this.form.Ichiran.DataSource as DataTable;
            DataTable preDt = new DataTable();
            foreach (DataColumn column in dt.Columns)
            {
                // NOT NULL制約を一時的に解除(新規追加行対策)
                column.AllowDBNull = true;

                // TIME_STAMPがなぜか一意制約有のため、解除
                if (column.ColumnName.Equals(Const.ContenaShuruiHoshuConstans.TIME_STAMP))
                {
                    column.Unique = false;
                }
            }
        }

        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void GetSysInfoInit()
        {
            // システム情報を取得し、初期値をセットする
            M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
            if (sysInfo != null)
            {
                this.sysInfoEntity = sysInfo[0];
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = (bool)this.sysInfoEntity.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = (bool)this.sysInfoEntity.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = (bool)this.sysInfoEntity.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI;
            }
        }

    }
}
