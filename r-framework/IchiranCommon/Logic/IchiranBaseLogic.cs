using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.OriginalException;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.IchiranCommon.APP;
using Shougun.Core.Common.IchiranCommon.Const;
using Shougun.Core.Common.IchiranCommon.Dto;
using Shougun.Core.Message;
using System.Linq;
using System;
using r_framework.CustomControl.DataGridCustomControl;

namespace Shougun.Core.Common.IchiranCommon.Logic
{
    /// <summary>
    /// 共通一覧にて使用されるLogicクラス
    /// </summary>
    public class IchiranBaseLogic
    {
        /// <summary>
        /// パターン一覧のソート
        /// </summary>
        public class SortByDetailSystemId : IComparer<M_OUTPUT_PATTERN_COLUMN>
        {
            public int Compare(M_OUTPUT_PATTERN_COLUMN x, M_OUTPUT_PATTERN_COLUMN y)
            {
                return x.DETAIL_SYSTEM_ID.CompareTo(y.DETAIL_SYSTEM_ID);
            }
        }

        /// <summary>
        /// 共通一覧スーパーForm
        /// </summary>
        private IchiranSuperForm form;

        /// <summary>
        /// adjustColumnSizeメソッド実行時にdataGridにFocusするか否かのフラグ
        /// 使用画面：返却日入力(G137)
        /// </summary>
        public bool IsFocusDGV = true;

        /// <summary>
        /// 画面表示用のDao
        /// </summary>
        private IS2Dao dao;

        /// <summary>
        /// 一覧出力項目用のDao
        /// </summary>
        private IM_OUTPUT_PATTERNDao patternDao;

        /// <summary>
        /// 一覧出力項目個別
        /// </summary>
        private IM_OUTPUT_PATTERN_KOBETSUDao kobetsuPatternDao;

        /// <summary>
        /// 一覧出力項目詳細
        /// </summary>
        private IM_OUTPUT_PATTERN_COLUMNDao columnPatternDao;

        /// <summary>
        /// Inxs pattern Dao
        /// </summary>
        private IM_OUTPUT_PATTERN_INXSDao patternInxsDao;

        /// <summary>
        /// Inxs pattern column dao
        /// </summary>
        private IM_OUTPUT_PATTERN_COLUMN_INXSDao columnPatternInxsDao;

        /// <summary>
        /// 一覧出力項目のEntity
        /// </summary>
        private M_OUTPUT_PATTERN patternEntity;

        /// <summary>
        /// 一覧出力項目個別のEntity
        /// </summary>
        private M_OUTPUT_PATTERN_KOBETSU patternKobetsuEntity;

        /// <summary>
        /// 一覧出力項目詳細のEntity
        /// </summary>
        private M_OUTPUT_PATTERN_COLUMN patternColumnEntity;

        /// <summary>
        /// Inxs pattern entity
        /// </summary>
        private M_OUTPUT_PATTERN_INXS patternInxsEntity;

        /// <summary>
        /// Inxs pattern column entity
        /// </summary>
        private M_OUTPUT_PATTERN_COLUMN_INXS patternColumnInxsEntity;

        /// <summary>
        /// 一覧出力項目格納Dto
        /// </summary>
        private List<OutputPatternDto> patternList;

        /// <summary>
        /// 検索後、非表示処理を行う列名
        /// </summary>
        internal string[] hiddenColumns;

        /// <summary>
        /// カレント一覧出力項目格納Dto
        /// </summary>
        public OutputPatternDto currentPatternDto { get; private set; }

        /// <summary>
        /// SQL文
        /// </summary>
        private string sql = string.Empty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="form">Form</param>
        public IchiranBaseLogic(IchiranSuperForm form)
        {
            this.form = form;
        }

        /// <summary>
        /// 設定ファイルを格納しているアセンブリ
        /// </summary>
        public Assembly SettingAssembly { get; set; }

        /// <summary>
        /// SELECT句
        /// </summary>
        public string SelectQeury { get; private set; }

        /// <summary>
        /// JOIN句
        /// </summary>
        public string JoinQuery { get; private set; }

        /// <summary>
        /// ORDER BY句
        /// </summary>
        public string OrderByQuery { get; private set; }

        /// <summary>
        /// アラートを出力する件数
        /// </summary>
        public int AlertCount { get; set; }

        public int PositionActive { get; set; }  //ThangNguyen [Add] 20150728 #11434

        public void FieldInit()
        {
            this.dao = this.form.DenshuKbn.GetDao();

            this.patternDao = DaoInitUtility.GetComponent<IM_OUTPUT_PATTERNDao>();
            this.kobetsuPatternDao = DaoInitUtility.GetComponent<IM_OUTPUT_PATTERN_KOBETSUDao>();
            this.columnPatternDao = DaoInitUtility.GetComponent<IM_OUTPUT_PATTERN_COLUMNDao>();

            this.patternEntity = new M_OUTPUT_PATTERN();
            this.patternKobetsuEntity = new M_OUTPUT_PATTERN_KOBETSU();
            this.patternColumnEntity = new M_OUTPUT_PATTERN_COLUMN();

            //Communicate InxsSubApplication Start
            this.patternInxsDao = DaoInitUtility.GetComponent<IM_OUTPUT_PATTERN_INXSDao>();
            this.columnPatternInxsDao = DaoInitUtility.GetComponent<IM_OUTPUT_PATTERN_COLUMN_INXSDao>();

            this.patternInxsEntity = new M_OUTPUT_PATTERN_INXS();
            this.patternColumnInxsEntity = new M_OUTPUT_PATTERN_COLUMN_INXS();
            //Communicate InxsSubApplication End

            this.patternList = new List<OutputPatternDto>();
        }

        /// <summary>
        /// ソートヘッダー初期化処理
        /// </summary>
        public void SortHeaderInit()
        {
        }

        /// <summary>
        /// パターン情報初期化処理
        /// </summary>
        public void PatternInit()
        {
            this.FieldInit();
            this.GetPatternData();
            this.GetPatternKobetsu();
            this.GetPatternColumn();
            //Communicate InxsSubApplication Start
            this.GetPatternDataInxs();
            this.GetPatternColumnInxs();
            //Communicate InxsSubApplication End
            this.InitPatternNo(); //PatternNo=0の時は、デフォルトパターンをPatternNoに設定。（なければ0のまま）
            this.form.Table = null;
            if (this.form.PatternNo != 0) //デフォルトパターン未設定の時は一覧は表示しない方針（ガイドライン）
            {
                this.SetCurrentPattern();
                this.SetSearchQuery();
                this.form.Table = this.GetColumnHeaderOnlyDataTable();
            }
        }

        /// <summary>
        /// 一覧出力項目取得処理
        /// </summary>
        public void GetPatternData()
        {
            this.patternEntity.DENSHU_KBN_CD = (SqlInt16)((int)this.form.DenshuKbn);

            M_OUTPUT_PATTERN[] outputPattern = this.patternDao.GetAllValidData(this.patternEntity);
            foreach (M_OUTPUT_PATTERN pattern in outputPattern)
            {
                OutputPatternDto patternDto = new OutputPatternDto();
                patternDto.OutputPattern = pattern;
                this.patternList.Add(patternDto);
            }
        }

        /// <summary>
        /// 一覧出力項目個別項目取得処理
        /// </summary>
        public void GetPatternKobetsu()
        {
            foreach (var patternDto in this.patternList)
            {
                this.patternKobetsuEntity.SHAIN_CD = this.form.ShainCd;
                this.patternKobetsuEntity.SYSTEM_ID = patternDto.OutputPattern.SYSTEM_ID;
                
                int len = this.kobetsuPatternDao.GetAllValidData(this.patternKobetsuEntity).Length;
                if (len > 0)
                {
                    patternDto.OutputPatternKobetsu = this.kobetsuPatternDao.GetAllValidData(this.patternKobetsuEntity)[0];
                }
                else
                {
                    patternDto.OutputPattern = null;
                }

            }
        }

        /// <summary>
        /// 一覧出力項目詳細取得処理
        /// </summary>
        public void GetPatternColumn()
        {
            foreach (var patternDto in this.patternList)
            {
                if (patternDto.OutputPattern != null)
                {
                    this.patternColumnEntity.SYSTEM_ID = patternDto.OutputPattern.SYSTEM_ID;
                    this.patternColumnEntity.SEQ = patternDto.OutputPattern.SEQ;

                    var columnPattern = this.columnPatternDao.GetAllValidData(this.patternColumnEntity);

                    //Communicate InxsSubApplication Start
                    //patternDto.OutputPatternColumn.AddRange(columnPattern);
                    if (columnPattern != null && columnPattern.Length > 0)
                    {
                        patternDto.OutputPatternColumnShougun.AddRange(columnPattern);
                        patternDto.OutputPatternColumn.AddRange(columnPattern);
                    }
                    //Communicate InxsSubApplication End
                }
            }
        }

        /// <summary>
        /// Get Inxs pattern data
        /// </summary>
        public void GetPatternDataInxs()
        {
            if (r_framework.Configuration.AppConfig.AppOptions.IsInxsUketsuke())
            {
                foreach (var patternDto in this.patternList)
                {
                    if (patternDto.OutputPattern != null)
                    {
                        this.patternInxsEntity.SYSTEM_ID = patternDto.OutputPattern.SYSTEM_ID;
                        this.patternInxsEntity.SEQ = patternDto.OutputPattern.SEQ;

                        var entities = this.patternInxsDao.GetAllValidData(this.patternInxsEntity);
                        if (entities != null && entities.Length > 0)
                        {
                            patternDto.OutputPatternInxs = entities[0];
                        }
                        else
                        {
                            patternDto.OutputPatternInxs = null;
                        }
                    }

                }
            }
        }

        /// <summary>
        /// Get Inxs pattern data
        /// </summary>
        public void GetPatternColumnInxs()
        {
            if (r_framework.Configuration.AppConfig.AppOptions.IsInxsUketsuke())
            {
                foreach (var patternDto in this.patternList)
                {
                    if (patternDto.OutputPattern != null)
                    {
                        this.patternColumnInxsEntity.SYSTEM_ID = patternDto.OutputPattern.SYSTEM_ID;
                        this.patternColumnInxsEntity.SEQ = patternDto.OutputPattern.SEQ;

                        var columnPatternInxsList = this.columnPatternInxsDao.GetAllValidData(this.patternColumnInxsEntity);
                        if (columnPatternInxsList != null && columnPatternInxsList.Length > 0)
                        {
                            foreach (var columnPatternInxs in columnPatternInxsList)
                            {
                                patternDto.OutputPatternColumnInxs.Add(columnPatternInxs);
                                patternDto.OutputPatternColumn.Add(CopyEntity<M_OUTPUT_PATTERN_COLUMN_INXS, M_OUTPUT_PATTERN_COLUMN>.Copy(columnPatternInxs));
                            }
                            patternDto.OutputPatternColumn.Sort(new SortByDetailSystemId());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// パターンボタン名設定
        /// </summary>
        public void SetPatternButton()
        {
            //課題 #1540 パターンの設定がない場合は パターン1～5と表記するルール
            //ボタンはTextChangeでEnabled=trueにしているので、Text設定後にEnabeled=falseにする必要あり。
            this.form.bt_ptn1.Text = "パターン1";
            this.form.bt_ptn2.Text = "パターン2";
            this.form.bt_ptn3.Text = "パターン3";
            this.form.bt_ptn4.Text = "パターン4";
            this.form.bt_ptn5.Text = "パターン5";

            this.form.bt_ptn1.Enabled = false;
            this.form.bt_ptn2.Enabled = false;
            this.form.bt_ptn3.Enabled = false;
            this.form.bt_ptn4.Enabled = false;
            this.form.bt_ptn5.Enabled = false;

            bool isCurrentPatternNoExist = false;

            foreach (var patternDto in this.patternList)
            {
                if (patternDto.OutputPattern == null
                 || patternDto.OutputPatternKobetsu.DISP_NUMBER.IsNull)
                {
                    continue;
                }
                ControlUtility contUtil = new ControlUtility();
                contUtil.ControlCollection = this.form.Controls;
                var setControl = contUtil.GetSettingField("bt_ptn" + patternDto.OutputPatternKobetsu.DISP_NUMBER);

                PropertyUtility.SetValue(setControl, "Text", patternDto.OutputPattern.PATTERN_NAME.ToString());

                if (setControl != null)
                {
                    if (this.form.PatternNo == patternDto.OutputPatternKobetsu.DISP_NUMBER.Value)
                    {
                        isCurrentPatternNoExist = true;
                    }
                    setControl.Enabled = true;
                }
            }
            if (!isCurrentPatternNoExist)
            {
                this.form.PatternNo = 0;
            }
        }

        /// <summary>
        /// 画面表示パターンの設定
        /// </summary>
        public void InitPatternNo()
        {
            if (this.form.PatternNo == 0)
            {
                //パターンが未選択の場合、デフォルト設定をチェック
                foreach (var patternDto in this.patternList)
                {
                    if (patternDto.OutputPattern != null && patternDto.OutputPatternKobetsu.DEFAULT_KBN.Value)
                    {
                        this.form.PatternNo = patternDto.OutputPatternKobetsu.DISP_NUMBER.Value;
                        break;
                    }
                }
            }
            else
            {
                //パターンが選択されている場合、マスタに存在するかチェック
                bool existPtnNo = false;

                foreach (var patternDto in this.patternList)
                {
                    if (patternDto.OutputPattern != null && !patternDto.OutputPatternKobetsu.DISP_NUMBER.IsNull
                        && patternDto.OutputPatternKobetsu.DISP_NUMBER.Value == this.form.PatternNo)
                    {
                        existPtnNo = true;
                        break;
                    }
                }

                //マスタに存在しない場合は、パターン無しとする(0とする)
                if (existPtnNo == false)
                {
                    this.form.PatternNo = 0;
                }
            }
        }

        /// <summary>
        /// DB情報から選択パターンを決定し、セットします。
        /// </summary>
        public void SetCurrentPattern()
        {
            this.currentPatternDto = null;
            this.PositionActive = 0;    //ThangNguyen [Add] 20150728 #11434
            foreach (var patternDto in this.patternList)
            {
                if (patternDto.OutputPattern != null && !patternDto.OutputPatternKobetsu.DISP_NUMBER.IsNull
                 && patternDto.OutputPatternKobetsu.DISP_NUMBER.Value == this.form.PatternNo)
                {
                    this.currentPatternDto = patternDto;
                    this.PositionActive = int.Parse(patternDto.OutputPatternKobetsu.DISP_NUMBER.Value.ToString());  //ThangNguyen [Add] 20150728 #11434
                    break;
                }
            }
        }

        /// <summary>
        /// システムIDから選択パターンを決定し、セットします。
        /// </summary>
        /// <param name="sysID"></param>
        public void SetCurrentPattern(string sysID)
        {
            var patternDto = new OutputPatternDto();
            this.patternEntity.DENSHU_KBN_CD = (SqlInt16)((int)this.form.DenshuKbn);
            this.patternEntity.SYSTEM_ID = SqlInt64.Parse(sysID);
            M_OUTPUT_PATTERN[] outputPattern = this.patternDao.GetAllValidData(this.patternEntity);
            if (outputPattern == null || outputPattern.Length == 0)
            {
                return;
            }
            
            patternDto.OutputPattern = outputPattern[outputPattern.Length - 1];

            patternDto.OutputPatternKobetsu.DISP_NUMBER = 6;
            patternDto.OutputPatternKobetsu.SYSTEM_ID = patternDto.OutputPattern.SYSTEM_ID;
            patternDto.OutputPatternKobetsu.SEQ = patternDto.OutputPattern.SEQ;
            patternDto.OutputPatternKobetsu.SHAIN_CD = this.form.ShainCd;
            patternDto.OutputPatternKobetsu.DEFAULT_KBN = false;
            patternDto.OutputPatternKobetsu.DELETE_FLG = false;
            patternDto.OutputPatternKobetsu.CREATE_DATE = patternDto.OutputPattern.CREATE_DATE;
            patternDto.OutputPatternKobetsu.CREATE_PC = patternDto.OutputPattern.CREATE_PC;
            patternDto.OutputPatternKobetsu.CREATE_USER = patternDto.OutputPattern.CREATE_USER;
            patternDto.OutputPatternKobetsu.UPDATE_DATE = patternDto.OutputPattern.UPDATE_DATE;
            patternDto.OutputPatternKobetsu.UPDATE_PC = patternDto.OutputPattern.UPDATE_PC;
            patternDto.OutputPatternKobetsu.UPDATE_USER = patternDto.OutputPattern.UPDATE_USER;

            this.patternColumnEntity.SYSTEM_ID = patternDto.OutputPattern.SYSTEM_ID;
            this.patternColumnEntity.SEQ = patternDto.OutputPattern.SEQ;
            //Communicate InxsSubApplication Start
            //patternDto.OutputPatternColumn.AddRange(this.columnPatternDao.GetAllValidData(this.patternColumnEntity));
            var patternColumnEntities = this.columnPatternDao.GetAllValidData(this.patternColumnEntity);
            if (patternColumnEntities != null && patternColumnEntities.Length > 0)
            {
                patternDto.OutputPatternColumnShougun.AddRange(patternColumnEntities);
                patternDto.OutputPatternColumn.AddRange(patternColumnEntities);
            }

            if (r_framework.Configuration.AppConfig.AppOptions.IsInxsUketsuke())
            {
                this.patternInxsEntity.SYSTEM_ID = patternDto.OutputPattern.SYSTEM_ID;
                this.patternInxsEntity.SEQ = patternDto.OutputPattern.SEQ;
                var outputPatternInxsEntities = this.patternInxsDao.GetAllValidData(this.patternInxsEntity);
                if (outputPatternInxsEntities != null && outputPatternInxsEntities.Length > 0)
                {
                    patternDto.OutputPatternInxs = outputPatternInxsEntities[0];
                }

                this.patternColumnInxsEntity.SYSTEM_ID = patternDto.OutputPattern.SYSTEM_ID;
                this.patternColumnInxsEntity.SEQ = patternDto.OutputPattern.SEQ;
                var patternColumnInxsEntities = this.columnPatternInxsDao.GetAllValidData(this.patternColumnInxsEntity);
                if (patternColumnInxsEntities != null && patternColumnInxsEntities.Length > 0)
                {
                    foreach (var columnPatternInxs in patternColumnInxsEntities)
                    {
                        patternDto.OutputPatternColumnInxs.Add(columnPatternInxs);
                        patternDto.OutputPatternColumn.Add(CopyEntity<M_OUTPUT_PATTERN_COLUMN_INXS, M_OUTPUT_PATTERN_COLUMN>.Copy(columnPatternInxs));
                    }
                    patternDto.OutputPatternColumn.Sort(new SortByDetailSystemId());
                }
            }
            //Communicate InxsSubApplication End

            this.currentPatternDto = patternDto;
        }

        /// <summary>
        /// 選択しているパターンの検索文字列をセットします
        /// </summary>
        public void SetSearchQuery()
        {
            var patternSetting = PatternManager.GetPatternSetting((int)this.form.DenshuKbn);
            var selectSql = new StringBuilder();
            var joinSql = new StringBuilder();
            var orderBySql = new StringBuilder();
            var joinTableList = new List<int>();
            OutputColumn col;

            // OREDER BY句の作成
            foreach (var pattern in this.currentPatternDto.OutputPatternColumn)
            {
                if (pattern.SORT_NO != 1 && pattern.SORT_NO != 2)
                {
                    // ソート番号0はソートしない
                    continue;
                }

                col = patternSetting.GetColumn(pattern.OUTPUT_KBN, pattern.KOUMOKU_ID, 
                    this.currentPatternDto.OutputPattern.OUTPUT_KBN);

                if (col == null)
                {
                    continue;
                }

                if (orderBySql.Length != 0)
                {
                    orderBySql.Append(",");
                }

                // SELECTした列名
                orderBySql.AppendFormat(" \"{0}\" ", col.DispName);

                if (pattern.SORT_NO == 1)
                {
                    // 1は昇順
                    orderBySql.Append(" ASC ");
                }
                else if (pattern.SORT_NO == 2)
                {
                    // 2は降順
                    orderBySql.Append(" DESC");
                }
            }

            // DETAIL_SYSTEM_IDでソートしてからSELECT句作成
            this.currentPatternDto.OutputPatternColumn.Sort(new SortByDetailSystemId());
            
            foreach (var pattern in this.currentPatternDto.OutputPatternColumn)
            {
                col = patternSetting.GetColumn(pattern.OUTPUT_KBN, pattern.KOUMOKU_ID, 
                    this.currentPatternDto.OutputPattern.OUTPUT_KBN);

                if (col == null)
                {
                    continue;
                }

                if (selectSql.Length != 0)
                {
                    selectSql.Append(",");
                }

                if (!col.IsTableEmpty && !col.Name.Contains("."))
                {
                    selectSql.Append(patternSetting.GetTableName(col.TableID));
                    selectSql.Append(".");
                }

                selectSql.Append(col.Name);
                selectSql.Append(" AS \"");
                selectSql.Append(col.DispName);
                selectSql.Append("\"");

                // テーブルリストに追加。JOIN句生成で使用
                if (!col.IsTableEmpty && !joinTableList.Contains(col.TableID))
                {
                    joinTableList.Add(col.TableID);
                }
            }

            // JOIN句作成
            foreach (var id in joinTableList)
            {
                if (patternSetting.JoinConditions.ContainsKey(id))
                {
                    joinSql.Append(" ");
                    joinSql.Append(patternSetting.JoinConditions[id].Query);
                }
            }

            this.SelectQeury = selectSql.ToString();
            this.JoinQuery = joinSql.ToString();
            this.OrderByQuery = orderBySql.ToString();
        }

        /// <summary>
        /// SQLを発行し取得されたDataTableをFormに設定する
        /// </summary>
        public DataTable GetColumnHeaderOnlyDataTable()
        {
            var dataTable = new DataTable();
            var patternSetting = PatternManager.GetPatternSetting((int)this.form.DenshuKbn);
            OutputColumn col;
            foreach (var pattern in this.currentPatternDto.OutputPatternColumn)
            {
                col = patternSetting.GetColumn(pattern.OUTPUT_KBN, pattern.KOUMOKU_ID,
                    this.currentPatternDto.OutputPattern.OUTPUT_KBN);

                if (col == null)
                {
                    continue;
                }

                dataTable.Columns.Add(col.DispName);
            }

            return dataTable;
        }

        /// <summary>
        /// DataGridViewに値の設定を行う
        /// </summary>
        /// <param name="table"></param>
        public void CreateDataGridView(DataTable table)
        {
            if (this.AlertCount != 0 && this.AlertCount < table.Rows.Count)
            {
                // 件数アラート
                DialogResult result = MessageBoxUtility.MessageBoxShow("C025");
                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            //ヘッダチェックボックスがある場合、データソースに追加
            if (table != null && this.form.customDataGridView1.ListHeaderCheckbox != null && this.form.customDataGridView1.ListHeaderCheckbox.Length > 0)
            {
                foreach (var item in this.form.customDataGridView1.ListHeaderCheckbox)
                {
                    string colName = item.COLUMN_NAME;

                    //列名が不正
                    if (String.IsNullOrEmpty(colName))
                    {
                        continue;
                    }

                    //インデックスが不正
                    if (item.CHECKBOX_INDEX < 0)
	                {
		                continue;
	                }

                    //既存列名
                    if (table.Columns.Contains(colName))
                    {
                        continue;
                    }

                    //列を追加
                    DataColumn col = new DataColumn();
                    col.ColumnName = colName;
                    col.DataType = typeof(Boolean);
                    col.DefaultValue = false;

                    table.Columns.Add(col);

                    //列の位置を設定
                    col.SetOrdinal(item.CHECKBOX_INDEX);
                }
            }

            // ヘッダを残して全行クリアした状態を描画して見せる
            var ds = this.form.customDataGridView1.DataSource as DataTable;
            if (ds != null)
            {
                ds.Clear();
                this.form.customDataGridView1.DataSource = ds;
                this.form.customDataGridView1.Refresh();
            }

            this.form.customDataGridView1.SuspendLayout();

            // 表示用のソート
            this.form.customSortHeader1.SortDataTable(table);

            // 表示時の場合のみ適用
            if (this.form.customSearchHeader1.Visible)
            {
                this.form.customSearchHeader1.SearchDataTable(table);
            }

            // ここで一度nullで初期化しないと列順がおかしくなる（DGVの列の自動生成の不具合？）
            this.form.customDataGridView1.DataSource = null;
            this.form.customDataGridView1.DataSource = table;

            //ヘッダチェックボックスがある場合、
            if (this.form.customDataGridView1.Columns.Count > 0 && this.form.customDataGridView1.ListHeaderCheckbox != null && this.form.customDataGridView1.ListHeaderCheckbox.Length > 0)
            {
                var dataSource = this.form.customDataGridView1.DataSource as System.Data.DataTable;
                foreach (var item in this.form.customDataGridView1.ListHeaderCheckbox)
                {
                    string name = item.COLUMN_NAME;

                    //グリッドビューに列名が含まれない場合、処理しない
                    if (!this.form.customDataGridView1.Columns.Contains(name))
                    {
                        continue;
                    }
                    //列が編集できる
                    if (dataSource != null && dataSource.Columns.Contains(name))
                    {
                        dataSource.Columns[name].ReadOnly = false;
                    }

                    var dgvCol = this.form.customDataGridView1.Columns[name];

                    DgvCustomCheckBoxHeaderCell headerCell = new DgvCustomCheckBoxHeaderCell(item.HEADER_TEXT, item.CHECKBOX_POSITON);
                    headerCell.Value = name;
                    headerCell.OnCheckBoxClicked += new DgvCustomCheckBoxHeaderCell.CheckboxHeaderClickEventHander(this.form.customDataGridView1.headerCell_OnCheckBoxClicked);
                    dgvCol.HeaderCell = headerCell;
                    dgvCol.HeaderText = item.HEADER_TEXT;
                    dgvCol.ToolTipText = item.HINT_TEXT;
                }
            }

            var patternSetting = PatternManager.GetPatternSetting((int)this.form.DenshuKbn);

            // システム列を非表示にする
            this.HideColumns();

            foreach (DataGridViewColumn column in this.form.customDataGridView1.Columns)
            {
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    column.Width = (column.HeaderText.Length * 10) + 55;
                }
                if (this.form.customDataGridView1.ListHeaderCheckbox != null &&
                    this.form.customDataGridView1.ListHeaderCheckbox.Length > 0 &&
                    this.form.customDataGridView1.ListHeaderCheckbox.Where(w => w.COLUMN_NAME == column.Name).Count() > 0)
                {
                    column.ReadOnly = false;
                }
                else
                {
                    column.ReadOnly = true;
                }
                column.SortMode = DataGridViewColumnSortMode.NotSortable;

                if (column.ValueType != null)
                {
                    switch (column.ValueType.Name)
                    {
                        case "Int16":
                        case "Int32":
                        case "Int64":
                        case "UInt32":
                        case "UInt64":
                        case "Single":
                        case "Double":
                        case "Decimal":
                            // 数値型ならセル値を右寄せにする
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                        case "DateTime":
                            // 日付型は表示幅を「yyyy/mm/dd」が表示出来るよう固定長で表示を行う
                            column.Width = 107;
                            break;
                    }
                }

                var format = patternSetting.GetFormat(column.Name);
                switch (format)
                {
                    case "":
                        break;

                    // 以下のフォーマットはシステム設定を参照せず、カンマ付+小数点以下最大4桁まで表示する
                    case Const.FORMAT_TYPE.SUURYOU:
                    case Const.FORMAT_TYPE.JYURYOU:
                    case Const.FORMAT_TYPE.TANKA:
                    case Const.FORMAT_TYPE.ITAKU_KEIYAKU_SUURYOU:
                    case Const.FORMAT_TYPE.ITAKU_KEIYAKU_TANKA:
                    case Const.FORMAT_TYPE.MANIFEST_SUURYOU:
                        column.DefaultCellStyle.Format = "#,##0.####";
                        break;
                    case "#,##0.####":
                        column.DefaultCellStyle.Format = "#,##0.####";
                        column.DefaultCellStyle.NullValue = "0";
                        break;
                    case "00":
                    case "000":
                    case "0000":
                        // 数値型だが0詰めする項目はコードなので、文字列として扱う
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        column.DefaultCellStyle.Format = format;
                        break;
                    default :
                        column.DefaultCellStyle.Format = format;
                        break;
                }


            }

            this.form.customDataGridView1.ResumeLayout();

            // 各フォームでこのメソッド呼び出し直後に列をいじったりカレントセルを無効にする
            // 処理をいろいろやっているのでBeginInvokeで非同期に実行させる。
            if (this.form.customDataGridView1.RowCount > 0 && table.Rows.Count > 0)
            {
                this.form.BeginInvoke((MethodInvoker)adjustColumnSize);
            }
        }
            
        /// <summary>
        /// 各列の幅調整と先頭セルへのフォーカス設定
        /// 各フォームで列を追加したり独自処理がいろいろあるので、それが終わった後に処理すべく、
        /// CreateDataGridViewからBeginInvokeで非同期に実行される。
        /// </summary>
        private void adjustColumnSize()
        {
            var dgv = this.form.customDataGridView1;

            if (dgv == null || dgv.ColumnCount == 0)
            {
                return;
            }

            if (dgv.RowCount == 0 || dgv.DataSource == null || ((DataTable)dgv.DataSource).Rows.Count == 0)
            {
                return;
            }

            dgv.SuspendLayout();

            // TIME_STAMP列はバイナリなのでDataGridViewImageColumnとなり、AutoResizeColumnsメソッドでエラーとなってしまう
            // そのため、列名が"TIME_STAMP"でDataGridViewImageColumn以外をリサイズ対象とする
            // また、入力項目についてはリサイズを行わない(入力項目は初期状態ブランクの場合、幅が小さくなってしまため)
            // ※画面によってはCheckBoxも影響を受けてしまうため、返却日入力用にDgvCustomDataTimeColumnだけリサイズしないようにしている。
            foreach (DataGridViewColumn c in dgv.Columns)
            {
                if (c.Visible && !(c is DataGridViewImageColumn) && !c.Name.Equals("TIME_STAMP")
                    && (c.ReadOnly || c.GetType() != typeof(DgvCustomDataTimeColumn)))
                {
                    dgv.AutoResizeColumn(c.Index, DataGridViewAutoSizeColumnMode.DisplayedCells);
                }
            }

            dgv.ResumeLayout();


            if (IsFocusDGV)
            {
                // 先頭セルをカレントセルに設定
                var firstDisplayColumnIndex = (from DataGridViewColumn c in dgv.Columns where c.Visible orderby c.DisplayIndex select c.Index).First();
                dgv.CurrentCell = dgv[firstDisplayColumnIndex, 0];

                dgv.Focus();
            }

            this.form.AdjustColumnSizeComplete();
        }



        /// <summary>
        /// 列の非表示化を行います。
        /// </summary>
        private void HideColumns()
        {
            if (this.hiddenColumns == null)
            {
                // nullは何もしない
                return;
            }

            foreach (var col in this.hiddenColumns)
            {
                if (this.form.customDataGridView1.Columns.Contains(col))
                {
                    this.form.customDataGridView1.Columns[col].Visible = false;
                }
            }
        }

        /// <summary>
        /// パターン一覧画面を呼び出します。
        /// </summary>
        /// <param name="denshuKbn">伝種区分CD(NONEの場合はフォームの伝種区分をロード)</param>
        /// <returns>パターンのシステムID</returns>
        internal string OpenPatternIchiran(int denshuKbn)
        {
            if (denshuKbn == (int)DENSHU_KBN.NONE)
            {
                denshuKbn = (int)this.form.DenshuKbn;
            }

            var sysID = string.Empty;
            var piForm = new PatternIchiranForm(this.form.ShainCd, denshuKbn.ToString());
            var piHeader = new PatternIchiranHeader();
            using (var baseForm = new BasePopForm(piForm, piHeader))
            {
                baseForm.ShowDialog();
                if (piForm.ParamOut_UpdateFlag)
                {
                    this.form.PatternReload(true);
                }

                sysID = piForm.ParamOut_SysID;
                //baseForm.Dispose();
            }

            return sysID;
        }
    }   
}
