using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Dto.PetternSettingDto;
using r_framework.Entity;
using r_framework.OriginalException;
using r_framework.Setting.PetternSetting;
using r_framework.Utility;
using System.Linq;
using System;

namespace r_framework.Logic
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
        /// 一覧出力項目格納Dto
        /// </summary>
        private List<OutputPatternDto> patternList;


        /// <summary>
        /// カレント一覧出力項目格納Dto
        /// </summary>
        public OutputPatternDto currentPatternDto { get; private set; }

        /// <summary>
        /// アクセスするテーブル名のリストを格納
        /// </summary>
        private List<string> tableNameList;

        /// <summary>
        /// SQL文
        /// </summary>
        private string sql = string.Empty;

        private Dictionary<string, string> joinSqlMap;

        /// <summary>
        /// データをFW側で取得するかどうか
        /// </summary>
        private bool isDataLoad;

        /// <summary>
        /// 一覧ベースのコンストラクタ
        /// </summary>
        /// <param name="form">対象フォーム</param>
        /// <param name="isDataload">FW側でデータを取得するかどうか</param>
        public IchiranBaseLogic(IchiranSuperForm form, bool isDataload)
        {
            this.form = form;
            this.isDataLoad = isDataload;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="form">Form</param>
        public IchiranBaseLogic(IchiranSuperForm form)
        {
            this.form = form;
            this.isDataLoad = true;
        }

        /// <summary>
        /// 設定ファイルを格納しているアセンブリ
        /// </summary>
        public Assembly SettingAssembly { get; set; }

        /// <summary>
        /// パターン設定を格納しているXMLパス
        /// </summary>
        public string PatternXmlPath { get; set; }

        /// <summary>
        /// ジョイン情報のXMLパス
        /// </summary>
        public string JoinXmlPath { get; set; }

        /// <summary>
        /// SELECT句
        /// </summary>
        public string SelectQeury { get; private set; }

        /// <summary>
        /// ORDER BY句
        /// </summary>
        public string OrderByQuery { get; private set; }

        /// <summary>
        /// アラートを出力する件数
        /// </summary>
        public int AlertCount { get; set; }

        public void FieldInit()
        {
            this.dao = this.form.DenshuKbn.GetDao();

            this.patternDao = DaoInitUtility.GetComponent<IM_OUTPUT_PATTERNDao>();
            this.kobetsuPatternDao = DaoInitUtility.GetComponent<IM_OUTPUT_PATTERN_KOBETSUDao>();
            this.columnPatternDao = DaoInitUtility.GetComponent<IM_OUTPUT_PATTERN_COLUMNDao>();

            this.patternEntity = new M_OUTPUT_PATTERN();
            this.patternKobetsuEntity = new M_OUTPUT_PATTERN_KOBETSU();
            this.patternColumnEntity = new M_OUTPUT_PATTERN_COLUMN();

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
            this.InitPatternNo(); //PatternNo=0の時は、デフォルトパターンをPatternNoに設定。（なければ0のまま）
            this.form.Table = null;
            if (this.form.PatternNo != 0) //デフォルトパターン未設定の時は一覧は表示しない方針（ガイドライン）
            {
                this.GetPattern();
                if (this.isDataLoad)
                {
                    //データの抽出
                    this.form.Table = this.dao.GetDateForStringSql(this.sql);
                }
                else
                {
                    this.form.Table = this.GetColumnHeaderOnlyDataTable();
                }
            }
        }

        /// <summary>
        /// 一覧出力項目取得処理
        /// </summary>
        public void GetPatternData()
        {
            this.patternEntity.DENSHU_KBN_CD = (SqlInt16)((int)this.form.DenshuKbn);

            M_OUTPUT_PATTERN[] outputPatter = this.patternDao.GetAllValidData(this.patternEntity);
            foreach (M_OUTPUT_PATTERN pattern in outputPatter)
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
                this.patternKobetsuEntity.SHAIN_CD = this.form.ShainCd;  //TODO:一覧出力項目個別(M_OUTPUT_PATTERN_KOBETSU)の検索用にIchiranSuperFormのShainCdプロパティを指定(仮)
                this.patternKobetsuEntity.SYSTEM_ID = patternDto.OutputPattern.SYSTEM_ID;
                ///this.patternKobetsuEntity.SEQ = patternDto.OutputPattern.SEQ; //SEQの連携は不要のため削除
                
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

                    foreach (var column in columnPattern)
                    {
                        patternDto.OutputPatternColumn.Add(column);
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
            //2013.12.15 naitou upd パターン更新 start
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
            //2013.12.15 naitou upd パターン更新 end
        }

        /// <summary>
        /// パターン一覧テーブルから出力パターンの生成を行う
        /// </summary>
        public void GetPattern()
        {
            this.currentPatternDto = null;
            foreach (var patternDto in this.patternList)
            {
                if (patternDto.OutputPattern != null && !patternDto.OutputPatternKobetsu.DISP_NUMBER.IsNull
                 && patternDto.OutputPatternKobetsu.DISP_NUMBER.Value == this.form.PatternNo)
                {
                    this.currentPatternDto = patternDto;
                    break;
                }
            }

            this.tableNameList = new List<string>();
            StringBuilder selectSql = new StringBuilder();
            StringBuilder table = new StringBuilder();
            StringBuilder orderBySql = new StringBuilder();
            if (this.isDataLoad)
            {
                this.CreateSettingData(this.currentPatternDto);
            }
            else
            {
                this.joinSqlMap = new Dictionary<string, string>();
            }
                     
            foreach (var pattern in this.currentPatternDto.OutputPatternColumn)
            {
                //if (selectSql.Length != 0)
                //{
                //    selectSql.Append(",");
                //}
                //if (!pattern.KOUMOKU_BUTSURI_NAME.Contains("."))
                //{
                //    selectSql.Append(pattern.TABLE_NAME);
                //    selectSql.Append(".");
                //}
                //selectSql.Append(pattern.KOUMOKU_BUTSURI_NAME);
                //selectSql.Append(" AS \"");
                //selectSql.Append(pattern.KOUMOKU_RONRI_NAME);
                //selectSql.Append("\"");

                if (!this.tableNameList.Contains(pattern.TABLE_NAME))
                {
                    this.tableNameList.Add(pattern.TABLE_NAME);
                    if (!this.joinSqlMap.ContainsKey(pattern.TABLE_NAME))
                    {
                        table.Append(pattern.TABLE_NAME);
                        table.Append(",");
                    }
                }

                if (pattern.SORT_NO != 0)
                {
                    if (!string.IsNullOrWhiteSpace(pattern.TABLE_NAME) && !pattern.KOUMOKU_BUTSURI_NAME.Contains("."))
                    {
                        orderBySql.Append(pattern.TABLE_NAME);
                        orderBySql.Append(".");
                    }
                    if (pattern.SORT_NO == 1)
                    {
                        orderBySql.Append(pattern.KOUMOKU_BUTSURI_NAME);
                        orderBySql.Append(" ASC");
                    }
                    else if (pattern.SORT_NO == 2)
                    {
                        orderBySql.Append(pattern.KOUMOKU_BUTSURI_NAME);
                        orderBySql.Append(" DESC");
                    }
                    else
                    {
                        throw new Exception();
                    }
                    orderBySql.Append(",");
                }
            }

            this.currentPatternDto.OutputPatternColumn.Sort(new SortByDetailSystemId());
            foreach (var pattern in this.currentPatternDto.OutputPatternColumn)
            {
                if (selectSql.Length != 0)
                {
                    selectSql.Append(",");
                }
                if (!string.IsNullOrWhiteSpace(pattern.TABLE_NAME) && !pattern.KOUMOKU_BUTSURI_NAME.Contains("."))
                {
                    selectSql.Append(pattern.TABLE_NAME);
                    selectSql.Append(".");
                }
                selectSql.Append(pattern.KOUMOKU_BUTSURI_NAME);
                selectSql.Append(" AS \"");
                selectSql.Append(pattern.KOUMOKU_RONRI_NAME);
                selectSql.Append("\"");
            }
            
            if (table.Length < 1) return;
            table.Remove(table.Length - 1, 1);
            orderBySql.Remove(orderBySql.Length - 1, 1);

            if (this.isDataLoad)
            {
                this.CreateSql(selectSql.ToString(), table.ToString(), orderBySql.ToString());
            }
            this.SelectQeury = selectSql.ToString();
            this.OrderByQuery = orderBySql.ToString();
        }
        
        /// <summary>
        /// 実行するSQL文を生成する
        /// </summary>
        /// <param name="selectSql">select句文字列</param>
        /// <param name="table">テーブル情報文字列</param>
        /// <param name="orderBySql">order by句文字列</param>
        public void CreateSql(string selectSql, string table, string orderBySql)
        {
            this.sql = "select ";
            this.sql += selectSql;
            this.sql += " from ";
            this.sql += table;
            foreach (string joinSql in this.joinSqlMap.Values)
            {
                this.sql += joinSql.ToString();
            }
            if (!string.IsNullOrEmpty(this.form.SimpleSearchSettings)
                || !string.IsNullOrEmpty(this.form.SerachSetting))
            {
                this.sql += " where " + this.form.SimpleSearchSettings + " " + this.form.SerachSetting;
            }
            this.sql += " order by " + orderBySql;
        }

      


        /// <summary>
        /// 設定ファイルを読み込み、出力内容・絞込み内容の生成を行う
        /// </summary>
        /// <param name="patternDto"></param>
        public void CreateSettingData(OutputPatternDto patternDto)
        {
            PatternJoinSetting joinSetting = new PatternJoinSetting();
            PatternSetting patternSetting = new PatternSetting();

            joinSetting.LoadPatternJoinSetting(this.SettingAssembly, this.JoinXmlPath);
            patternSetting.LoadPatternSetting(this.SettingAssembly, this.PatternXmlPath);
            this.joinSqlMap = new Dictionary<string, string>();
            foreach (var pattern in patternDto.OutputPatternColumn)
            {
                PatternSettingDto patternSettingDto = patternSetting.GetSetting(pattern.KOUMOKU_BUTSURI_NAME);
                if (!string.IsNullOrEmpty(patternSettingDto.JoinTableName))
                {
                    string joinString = string.Empty;
                    string joinPrevParam = string.Empty;
                    bool addJoinParam = false;

                    if (this.joinSqlMap.ContainsKey(patternSettingDto.JoinTableName))
                    {
                        joinPrevParam = this.joinSqlMap[patternSettingDto.JoinTableName];
                        addJoinParam = true;
                    }
                    if (string.IsNullOrEmpty(joinString) && !addJoinParam)
                    {
                        joinString = " LEFT JOIN " + patternSettingDto.JoinTableName;
                    }
                    StringBuilder joinSqlBuilder = new StringBuilder();
                    if (!string.IsNullOrEmpty(patternSettingDto.JoinTableName))
                    {
                        for (int i = 0; i < patternSettingDto.SendColumnName.Count; i++)
                        {
                            string keyColmun = patternSettingDto.SendColumnName[i];
                            if (joinSqlBuilder.Length == 0 && !addJoinParam)
                            {
                                joinSqlBuilder.Append(" ON ");
                            }
                            else
                            {
                                joinSqlBuilder.Append(" AND ");
                            }
                            joinSqlBuilder.Append(joinSetting.GetSetting(patternSettingDto.JoinTableName).TableName);
                            joinSqlBuilder.Append(".");
                            joinSqlBuilder.Append(joinSetting.GetSetting(patternSettingDto.JoinTableName).KeyColumn[i]);
                            joinSqlBuilder.Append(" = ");
                            if (keyColmun.IndexOf(".") > 0)
                            {
                                joinSqlBuilder.Append(keyColmun);
                            }
                            else
                            {
                                joinSqlBuilder.Append(patternSettingDto.JoinTableName);
                                joinSqlBuilder.Append(".");
                                joinSqlBuilder.Append(keyColmun);
                            }
                        }
                    }
                    if (!this.joinSqlMap.ContainsKey(patternSettingDto.JoinTableName))
                    {
                        this.joinSqlMap.Add(patternSettingDto.JoinTableName, joinString + joinSqlBuilder.ToString());
                    }
                    else
                    {
                        this.joinSqlMap.Remove(patternSettingDto.JoinTableName);
                        this.joinSqlMap.Add(patternSettingDto.JoinTableName, joinPrevParam + joinSqlBuilder.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// SQLを発行し取得されたDataTableをFormに設定する
        /// </summary>
        public DataTable GetColumnHeaderOnlyDataTable()
        {
            var dataTable = new DataTable();

            foreach (var pattern in this.currentPatternDto.OutputPatternColumn)
            {
                dataTable.Columns.Add(new DataColumn(pattern.KOUMOKU_RONRI_NAME));
            }

            return dataTable;
        }

        /// <summary>
        /// DataGridViewに値の設定を行う
        /// </summary>
        /// <param name="table"></param>
        public void CreateDataGridView(DataTable table)
        {
            DialogResult result = DialogResult.Yes;

            if (this.AlertCount != 0 && this.AlertCount < table.Rows.Count)
            {
                MessageBoxShowLogic showLogic = new MessageBoxShowLogic();
                result = showLogic.MessageBoxShow("C025");
            }
            if (result == DialogResult.Yes)
            {
                this.form.customSortHeader1.SortDataTable(table);
                // ここで一度nullで初期化しないと列順がおかしくなる（DGVの列の自動生成の不具合？）
                this.form.customDataGridView1.DataSource = null;
                this.form.customDataGridView1.DataSource = table;

                foreach (DataGridViewColumn column in this.form.customDataGridView1.Columns)
                {
                    column.Width = (column.HeaderText.Length * 10) + 55;
                    column.ReadOnly = true;
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;

                    if (column.ValueType != null)
                    {
                        switch (column.ValueType.Name)
                        {
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
                        }
                    }
                }
                
                if (!this.form.customDataGridView1.IsBrowsePurpose)
                {
                    //背景色対応
                    //読み取り専用の色を付ける
                    foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {

                            var ia = cell as r_framework.CustomControl.ICustomAutoChangeBackColor;
                            if (ia != null)
                            {
                                ia.UpdateBackColor(); //色設定がない場合に対応させる
                            }
                            else
                            {
                                cell.UpdateBackColor(false); //読み取り専用だと最初に色を付ける
                            }
                        }
                    }
                }

                this.form.customDataGridView1.Columns.Cast<DataGridViewColumn>().Where(c => c.GetType() != typeof(DataGridViewImageColumn) && c.Name != "TIME_STAMP")
                                                                                .Select(c => c.Index).ToList()
                                                                                .ForEach(i => this.form.customDataGridView1.AutoResizeColumn(i));
            }
        }
    }   
}
