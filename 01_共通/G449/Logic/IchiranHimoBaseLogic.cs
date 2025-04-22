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
using r_framework.Logic;
using r_framework.Setting.PetternSetting;
using r_framework.Utility;
using Shougun.Core.Common.DenpyouHimodukeIchiran.DAO;
using System;

namespace Shougun.Core.Common.DenpyouHimodukeIchiran
{
    /// <summary>
    /// 共通一覧にて使用されるLogicクラス
    /// </summary>
    public class IchiranHimoBaseLogic
    {
        /// <summary>
        /// 共通一覧スーパーForm
        /// </summary>
        private IchiranHimoSuperForm form;

        /// <summary>
        /// 画面表示用のDao
        /// </summary>
        private IS2Dao dao;

        /// <summary>
        /// 一覧出力項目用のDao
        /// </summary>
        private IM_OUTPUT_PATTERN_HIMODao patternDao;

        /// <summary>
        /// 一覧出力項目個別
        /// </summary>
        private IM_OUTPUT_PATTERN_KOBETSU_HIMODao kobetsuPatternDao;

        /// <summary>
        /// 一覧出力項目詳細
        /// </summary>
        private IM_OUTPUT_PATTERN_COLUMN_HIMODao columnPatternDao;

        /// <summary>
        /// 一覧出力項目のEntity
        /// </summary>
        private M_OUTPUT_PATTERN_HIMO patternEntity;

        /// <summary>
        /// 一覧出力項目個別のEntity
        /// </summary>
        private M_OUTPUT_PATTERN_KOBETSU_HIMO patternKobetsuEntity;

        /// <summary>
        /// 一覧出力項目詳細のEntity
        /// </summary>
        private M_OUTPUT_PATTERN_COLUMN_HIMO patternColumnEntity;

        /// <summary>
        /// 一覧出力項目格納Dto
        /// </summary>
        private List<OutputPatternHimoDto> patternList;


        /// <summary>
        /// カレント一覧出力項目格納Dto
        /// </summary>
        private OutputPatternHimoDto currentPatternDto;

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
        public IchiranHimoBaseLogic(IchiranHimoSuperForm form, bool isDataload)
        {
            this.form = form;
            this.isDataLoad = isDataload;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="form">Form</param>
        public IchiranHimoBaseLogic(IchiranHimoSuperForm form)
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

            this.patternDao = DaoInitUtility.GetComponent<IM_OUTPUT_PATTERN_HIMODao>();
            this.kobetsuPatternDao = DaoInitUtility.GetComponent<IM_OUTPUT_PATTERN_KOBETSU_HIMODao>();
            this.columnPatternDao = DaoInitUtility.GetComponent<IM_OUTPUT_PATTERN_COLUMN_HIMODao>();

            this.patternEntity = new M_OUTPUT_PATTERN_HIMO();
            this.patternKobetsuEntity = new M_OUTPUT_PATTERN_KOBETSU_HIMO();
            this.patternColumnEntity = new M_OUTPUT_PATTERN_COLUMN_HIMO();

            this.patternList = new List<OutputPatternHimoDto>();
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
            //this.patternEntity.DENSHU_KBN_CD = (SqlInt16)((int)this.form.DenshuKbn);

            M_OUTPUT_PATTERN_HIMO[] outputPatter = this.patternDao.GetAllValidData(this.patternEntity);
            foreach (M_OUTPUT_PATTERN_HIMO pattern in outputPatter)
            {
                OutputPatternHimoDto patternDto = new OutputPatternHimoDto();
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
                this.patternKobetsuEntity.SHAIN_CD = this.form.ShainCd;  //TODO:一覧出力項目個別(M_OUTPUT_PATTERN_KOBETSU)の検索用にIchiranHimoSuperFormのShainCdプロパティを指定(仮)
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
                foreach (var patternDto in this.patternList)
                {
                    if (patternDto.OutputPattern != null && patternDto.OutputPatternKobetsu.DEFAULT_KBN.Value)
                    {
                        this.form.PatternNo = patternDto.OutputPatternKobetsu.DISP_NUMBER.Value;
                        break;
                    }
                }
            }
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
                if (selectSql.Length != 0)
                {
                    selectSql.Append(",");
                }
                if (!pattern.KOUMOKU_BUTSURI_NAME.Contains("."))
                {
                    //if (pattern.KINOU_NO.Value.Equals(6))
                    //{
                    //    selectSql.Append(pattern.TABLE_NAME + "6");
                    //}
                    //else if (pattern.KINOU_NO.Value.Equals(7))
                    //{
                    //    selectSql.Append(pattern.TABLE_NAME + "7");
                    //}
                    //else if (pattern.KINOU_NO.Value.Equals(9) && pattern.TABLE_NAME.Equals("T_UNCHIN_ENTRY"))
                    //{
                    //    selectSql.Append(pattern.TABLE_NAME + "9");
                    //}
                    //else if (pattern.KINOU_NO.Value.Equals(10) && pattern.TABLE_NAME.Equals("T_UNCHIN_ENTRY"))
                    //{
                    //    selectSql.Append(pattern.TABLE_NAME + "10");
                    //}
                    //else
                    //{
                    selectSql.Append(pattern.TABLE_NAME);
                    //}
                    
                    selectSql.Append(".");
                }
                selectSql.Append(pattern.KOUMOKU_BUTSURI_NAME);
                selectSql.Append(" AS \"");
                selectSql.Append("(" + pattern.KINOU_NAME + ")\r\n\r\n" + pattern.KOUMOKU_RONRI_NAME);
                selectSql.Append("\"");

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
                    orderBySql.Append(pattern.TABLE_NAME);
                    orderBySql.Append(".");
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
        public void CreateSettingData(OutputPatternHimoDto patternDto)
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
                string strColNm = pattern.TABLE_NAME + "-" + pattern.KOUMOKU_RONRI_NAME;
                dataTable.Columns.Add(new DataColumn(strColNm));
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
                                // 数値型ならヘッダテキストもセル値も右寄せにする
                                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                break;
                        }
                    }
                }
            }
        }
    }
}
