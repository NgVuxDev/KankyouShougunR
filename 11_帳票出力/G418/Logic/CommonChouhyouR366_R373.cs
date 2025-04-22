using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r_framework.Const;
using r_framework.Utility;

namespace Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup
{
    #region - Classes -

    #region - CommonChouhyouR366_R373 -

    /// <summary>帳票Nを表すクラス・コントロール</summary>
    public class CommonChouhyouR366_R373 : CommonChouhyouBase
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="CommonChouhyouR366_R373"/> class.</summary>
        /// <param name="windowID">画面ＩＤ</param>
        public CommonChouhyouR366_R373(WINDOW_ID windowID)
            : base(windowID)
        {
            // 帳票出力フルパスフォーム名
            this.OutputFormFullPathName = CommonChouhyouBase.TemplatePath + "R366_R373-Form.xml";

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";

            // 選択可能な集計項目グループ数
            this.SelectEnableSyuukeiKoumokuGroupCount = 3;

            // 選択可能な集計項目
            if (windowID == WINDOW_ID.R_NYUUKIN_MEISAIHYOU)
            {   // R366(入金明細表)
                this.SelectEnableSyuukeiKoumokuList = new List<int>()
                {
                    0, 1, 9, 10, 11, 20, 21, 22,
                };

                // 対象テーブルリスト
                this.TaishouTableList = new List<TaishouTable>()
                {
                    new TaishouTable("T_NYUUKIN_ENTRY", "T_NYUUKIN_DETAIL"),
                };
            }
            else if (windowID == WINDOW_ID.R_SYUKKINN_MEISAIHYOU)
            {   // R373(出金明細表)
                this.SelectEnableSyuukeiKoumokuList = new List<int>()
                {
                    0, 1, 9, 10, 11,
                };

                // 対象テーブルリスト
                this.TaishouTableList = new List<TaishouTable>()
                {
                    new TaishouTable("T_SHUKKIN_ENTRY", "T_SHUKKIN_DETAIL"),
                };
            }

            // 出力可能項目（伝票）の有効・無効
            this.OutEnableKoumokuDenpyou = true;

            // 出力可能項目（明細）の有効・無効
            this.OutEnableKoumokuMeisai = true;

            // 入力関連テーブル名
            this.InKanrenTable = new List<string>()
            {
                string.Empty,
            };

            // 入力関連データテーブルから取得したデータテーブルリスト
            this.InDataTable = new List<DataTable>();

            // 出力関連テーブル名
            this.OutKanrenTable = new List<string>()
            {
                string.Empty,
            };

            // 出力関連データテーブルから取得したデータテーブルリスト
            this.OutDataTable = new DataTable();
        }

        #endregion - Constructors -

        #region - Methods -

        /// <summary>初期化処理理を実行する</summary>
        public override void Initialize()
        {
            try
            {
                // 初期化処理理
                base.Initialize();
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
                throw e;
            }
        }

        /// <summary>帳票出力用データーテーブルの取得処理を実行する</summary>
        public override void GetOutDataTable()
        {
            try
            {
                // 初期化処理理
                this.Initialize();

                // 入力関連データテーブルから取得したデータテーブルリスト
                base.GetOutDataTable();

                // 出力帳票用データーテーブル作成処理
                this.MakeOutDataTable();
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
                throw e;
            }
        }

        /// <summary>出力帳票用データーテーブル作成処理を実行する</summary>
        private void MakeOutDataTable()
        {
            try
            {
                this.ChouhyouDataTable = new DataTable();

                DataRow dataRow;

                #region - テーブルカラム作成 -

                // 集計項目領域
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN1_1_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN1_2_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN1_3_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN1_4_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN1_5_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN1_6_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN1_7_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN1_8_VLB");

                // 伝票番号
                this.ChouhyouDataTable.Columns.Add("PHY_DENPYOU_NUMBER_FLB");

                // 伝票日付
                this.ChouhyouDataTable.Columns.Add("PHY_DENPYOU_DATE_FLB");

                // 帳票出力項目領域（伝票部）
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN4_1_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_1_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN4_2_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_2_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN4_3_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_3_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN4_4_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_4_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN4_5_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_5_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN4_6_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_6_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN4_7_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_7_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN4_8_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_8_VLB");

                // 明細行番
                this.ChouhyouDataTable.Columns.Add("PHY_ROW_NO_FLB");

                // 帳票出力項目領域（明細部）
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN4_9_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN3_1_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN4_10_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN3_2_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN4_11_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN3_3_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN4_12_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN3_4_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN4_13_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN3_5_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN4_14_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN3_6_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN4_15_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN3_7_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN4_16_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN3_8_VLB");

                // 伝票合計
                this.ChouhyouDataTable.Columns.Add("PHN_DENPYOU_TOTAL_FLB");

                // 集計項目３
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_3_KAHEN1_5_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_3_KAHEN1_6_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_3_TOTAL_FLB");

                // 集計項目２
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_2_KAHEN1_3_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_2_KAHEN1_4_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_2_TOTAL_FLB");

                // 集計項目１
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_1_KAHEN1_1_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_1_KAHEN1_2_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_1_TOTAL_FLB");

                #endregion - テーブルカラム作成 -

                if (this.InputDataTable == null || this.InputDataTable.Length == 0)
                {
                    return;
                }

                string sql;
                int itemColumnIndex = 0;

                for (int inTable = 0; inTable < this.InputDataTable.Length; inTable++)
                {
                    if (this.InputDataTable[inTable].Rows.Count == 0)
                    {   // データレコードが存在しない
                        continue;
                    }

                    string tableName = string.Empty;
                    string fieldName = string.Empty;

                    int index = 0;
                    object code = null;
                    object codeData = null;

                    // 伝票番号
                    long denpyouNoPrev = -1;
                    long denpyouNoNext;

                    // 有効な集計項目グループ数
                    int syuukeiKoumokuEnableGroupCount = this.SelectEnableSyuukeiKoumokuGroupCount;

                    decimal nyuukinSyutsukinAmountTotal = 0;
                    decimal chouseiAmountTotal = 0;
                    decimal[] syuukeiKoumokuGoukei = new decimal[syuukeiKoumokuEnableGroupCount];
                    decimal[] syuukeiKoumokuGoukeiTotal = new decimal[syuukeiKoumokuEnableGroupCount];
                    string[] syuukeiKoumokuCode = new string[syuukeiKoumokuEnableGroupCount];
                    string[] syuukeiKoumokuCodeName = new string[syuukeiKoumokuEnableGroupCount];

                    string[] syuukeiKoumokuPrevN = new string[syuukeiKoumokuEnableGroupCount];
                    for (int i = 0; i < syuukeiKoumokuEnableGroupCount; i++)
                    {
                        syuukeiKoumokuPrevN[i] = "INITIALIZE VALUE";
                    }

                    string[] syuukeiKoumokuNextN = new string[syuukeiKoumokuEnableGroupCount];

                    for (int rowCount = 0; rowCount < this.InputDataTable[inTable].Rows.Count; rowCount++)
                    {
                        dataRow = this.InputDataTable[inTable].Rows[rowCount];

                        DataRow dataRowNew = this.ChouhyouDataTable.NewRow();

                        // 伝票番号
                        index = -1;
                        if (this.WindowID == WINDOW_ID.R_NYUUKIN_MEISAIHYOU)
                        {   // R366(入金明細表)
                            index = this.InputDataTable[inTable].Columns.IndexOf("NYUUKIN_NUMBER");
                        }
                        else if (this.WindowID == WINDOW_ID.R_SYUKKINN_MEISAIHYOU)
                        {   // R373(出金明細表)
                            index = this.InputDataTable[inTable].Columns.IndexOf("SHUKKIN_NUMBER");
                        }

                        if (!this.IsDBNull(dataRow.ItemArray[index]))
                        {   // データーがNULLでない
                            denpyouNoNext = (long)dataRow.ItemArray[index];
                        }
                        else
                        {
                            denpyouNoNext = -1;
                        }

                        if (denpyouNoPrev != denpyouNoNext)
                        {   // 伝票番号が変化した
                            denpyouNoPrev = denpyouNoNext;

                            nyuukinSyutsukinAmountTotal = 0;
                            chouseiAmountTotal = 0;
                        }

                        #region - 集計項目用タイトルカラム -

                        for (itemColumnIndex = 0; itemColumnIndex < syuukeiKoumokuEnableGroupCount; itemColumnIndex++)
                        {
                            int itemIndex = this.SelectSyuukeiKoumokuList[itemColumnIndex];
                            SyuukeiKoumoku syuukeiKoumoku = this.SyuukeiKomokuList[itemIndex];

                            if (syuukeiKoumoku.MasterTableID == WINDOW_ID.NONE)
                            {
                                continue;
                            }

                            // マスターテーブル名取得
                            tableName = Enum.GetName(typeof(WINDOW_ID), syuukeiKoumoku.MasterTableID);

                            // マスターテーブルの該当フィールド名取得
                            fieldName = syuukeiKoumoku.FieldCDName;

                            // コード
                            if (syuukeiKoumoku.FieldCD == "SHAIN_CD")
                            {   // 営業担当者別
                                index = this.InputDataTable[inTable].Columns.IndexOf("EIGYOU_TANTOUSHA_CD");
                            }
                            else
                            {   // その他
                                index = this.InputDataTable[inTable].Columns.IndexOf(syuukeiKoumoku.FieldCD);
                            }

                            if (!this.IsDBNull(dataRow.ItemArray[index]))
                            {   // データーがNULLでない
                                code = dataRow.ItemArray[index];
                            }
                            else
                            {
                                code = string.Empty;
                            }

                            if (code.GetType() == typeof(string))
                            {
                                code = ((string)code).Replace(" ", string.Empty);

                                // コード名称
                                if ((string)code != string.Empty)
                                {
                                    if (syuukeiKoumoku.FieldCD.Equals("KYOTEN_CD"))
                                    {
                                        sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = {5}", tableName, fieldName, tableName, tableName, syuukeiKoumoku.FieldCD, code);                                        
                                    }
                                    else
                                    {
                                        sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}'", tableName, fieldName, tableName, tableName, syuukeiKoumoku.FieldCD, code);
                                    }

                                    DataTable dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);
                                    index = dataTableTmp.Columns.IndexOf(fieldName);
                                    if (dataTableTmp.Rows.Count != 0)
                                    {
                                        codeData = dataTableTmp.Rows[0].ItemArray[index];
                                    }
                                    else
                                    {
                                        codeData = string.Empty;
                                    }
                                }
                                else
                                {
                                    codeData = string.Empty;
                                }
                            }
                            else
                            {
                                // コード名称
                                sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = {5}", tableName, fieldName, tableName, tableName, syuukeiKoumoku.FieldCD, code);
                                DataTable dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);
                                index = dataTableTmp.Columns.IndexOf(fieldName);
                                if (dataTableTmp.Rows.Count != 0)
                                {
                                    codeData = dataTableTmp.Rows[0].ItemArray[index];
                                }
                                else
                                {
                                    codeData = string.Empty;
                                }
                            }

                            syuukeiKoumokuNextN[itemColumnIndex] = code.ToString();

                            if (!syuukeiKoumokuPrevN[itemColumnIndex].Equals(syuukeiKoumokuNextN[itemColumnIndex]))
                            {   // 集計項目Nが変化した

                                syuukeiKoumokuPrevN[itemColumnIndex] = syuukeiKoumokuNextN[itemColumnIndex];

                                syuukeiKoumokuGoukei[itemColumnIndex] = 0;
                                //syuukeiKoumokuGoukeiTotal[itemColumnIndex] = 0;

                                if (itemColumnIndex == 0)
                                {
                                    syuukeiKoumokuGoukeiTotal[0] = 0;
                                }

                                for (int j = itemColumnIndex; j < syuukeiKoumokuEnableGroupCount; j++)
                                {
                                    syuukeiKoumokuGoukeiTotal[j] = 0;
                                }
                            }

                            syuukeiKoumokuCode[itemColumnIndex] = code.ToString();
                            syuukeiKoumokuCodeName[itemColumnIndex] = codeData.ToString();

                            switch (itemColumnIndex + 1)
                            {
                                case 1: // 集計項目１
                                    dataRowNew["PHY_KAHEN1_1_VLB"] = code.ToString();
                                    dataRowNew["PHY_KAHEN1_2_VLB"] = codeData.ToString();

                                    break;
                                case 2: // 集計項目２
                                    dataRowNew["PHY_KAHEN1_3_VLB"] = code.ToString();
                                    dataRowNew["PHY_KAHEN1_4_VLB"] = codeData.ToString();

                                    break;
                                case 3: // 集計項目３
                                    dataRowNew["PHY_KAHEN1_5_VLB"] = code.ToString();
                                    dataRowNew["PHY_KAHEN1_6_VLB"] = codeData.ToString();

                                    break;
                                case 4: // 集計項目４
                                    dataRowNew["PHY_KAHEN1_7_VLB"] = code.ToString();
                                    dataRowNew["PHY_KAHEN1_8_VLB"] = codeData.ToString();

                                    break;
                            }
                        }

                        #endregion - 集計項目用タイトルカラムテキスト -

                        // 伝票番号
                        dataRowNew["PHY_DENPYOU_NUMBER_FLB"] = denpyouNoNext;

                        // 伝票日付
                        index = this.InputDataTable[inTable].Columns.IndexOf("DENPYOU_DATE");
                        if (!this.IsDBNull(dataRow.ItemArray[index]))
                        {
                            dataRowNew["PHY_DENPYOU_DATE_FLB"] = ((DateTime)dataRow.ItemArray[index]).ToString("yyyy/MM/dd");
                        }
                        else
                        {
                            dataRowNew["PHY_DENPYOU_DATE_FLB"] = string.Empty;
                        }

                        #region - 帳票出力項目（伝票）用タイトルカラム -

                        itemColumnIndex = 0;
                        for (int i = 0; i < this.SelectChouhyouOutKoumokuDepyouList.Count; i++, itemColumnIndex++)
                        {
                            ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = this.SelectChouhyouOutKoumokuDepyouList[i];
                            ChouhyouOutKoumoku chouhyouOutKoumoku = chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList[0];

                            // テーブル名取得
                            tableName = chouhyouOutKoumoku.TableName;

                            // テーブルの該当フィールド名取得
                            fieldName = chouhyouOutKoumoku.FieldName;

                            // コード
                            index = this.InputDataTable[inTable].Columns.IndexOf(chouhyouOutKoumoku.FieldName);

                            if (!this.IsDBNull(dataRow.ItemArray[index]))
                            {   // データーがNULLでない
                                code = dataRow.ItemArray[index];
                            }
                            else
                            {
                                code = string.Empty;
                            }

                            Type type = code.GetType();

                            bool isSqlExec = true;

                            if (type == typeof(string))
                            {   // 文字列型
                                code = ((string)code).Replace(" ", string.Empty);
                                if (((string)code).Equals(string.Empty))
                                {
                                    isSqlExec = false;
                                }
                            }
                            else if (type == typeof(bool))
                            {   // 論理型
                                code = (bool)code ? 1 : 0;
                            }

                            // コード名称
                            if (isSqlExec)
                            {   // SQL実行
                                DataTable dataTableTmp;

                                if (type == typeof(DateTime))
                                {   // 日付型
                                    sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}'", tableName, fieldName, tableName, tableName, fieldName, code);

                                    dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                                    index = dataTableTmp.Columns.IndexOf(fieldName);
                                    if (dataTableTmp.Rows.Count != 0)
                                    {
                                        codeData = ((DateTime)dataTableTmp.Rows[0].ItemArray[index]).ToString("yyyy/MM/dd");
                                    }
                                    else
                                    {
                                        codeData = string.Empty;
                                    }
                                }
                                else
                                {   // 通常
                                    if (chouhyouOutKoumoku.FieldName.Contains("CD"))
                                    {   // CD（コードを含む）
                                        string tableNameRef = string.Empty;
                                        string fieldNameRef = string.Empty;
                                        string fieldNameRyaku = string.Empty;

                                        // 参照すべきテーブル・フィールド名の取得
                                        chouhyouOutKoumoku.GetTableAndFieldNameRef(tableName, ref tableNameRef, ref fieldNameRef, ref fieldNameRyaku);

                                        if (chouhyouOutKoumoku.FieldName.Equals("EIGYOU_TANTOUSHA_CD"))
                                        {
                                            tableNameRef = "M_SHAIN";
                                            fieldNameRyaku = "SHAIN_NAME_RYAKU";
                                            fieldNameRef = "SHAIN_CD";

                                            sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}'", tableNameRef, fieldNameRyaku, tableNameRef, tableNameRef, fieldNameRef, code);
                                        }
                                        else if (chouhyouOutKoumoku.FieldName.Equals("KYOTEN_CD"))
                                        {
                                            sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = {5}", tableNameRef, fieldNameRyaku, tableNameRef, tableNameRef, fieldNameRef, code);                                            
                                        }
                                        else
                                        {
                                            sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}'", tableNameRef, fieldNameRyaku, tableNameRef, tableNameRef, fieldNameRef, code);
                                        }

                                        dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                                        index = dataTableTmp.Columns.IndexOf(fieldNameRyaku);
                                        if (dataTableTmp.Rows.Count != 0)
                                        {
                                            codeData = dataTableTmp.Rows[0].ItemArray[index];
                                        }
                                        else
                                        {
                                            codeData = string.Empty;
                                        }
                                    }
                                    else
                                    {
                                        if (type == typeof(string))
                                        {
                                            sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}'", tableName, fieldName, tableName, tableName, fieldName, code);
                                        }
                                        else
                                        {
                                            sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = {5}", tableName, fieldName, tableName, tableName, fieldName, code);
                                        }

                                        dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                                        index = dataTableTmp.Columns.IndexOf(fieldName);
                                        if (dataTableTmp.Rows.Count != 0)
                                        {
                                            codeData = dataTableTmp.Rows[0].ItemArray[index];
                                        }
                                        else
                                        {
                                            codeData = string.Empty;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                code = string.Empty;
                                codeData = string.Empty;
                            }

                            type = codeData.GetType();

                            if (type == typeof(bool))
                            {   // 論理型
                                codeData = (bool)codeData ? 1 : 0;
                            }

                            if (!chouhyouOutKoumoku.FieldName.Contains("CD"))
                            {   // フィールド名にCD（コード）が含まれていない
                                code = string.Empty;
                            }

                            if (codeData.GetType() == typeof(decimal))
                            {   // 金額系
                                string format = chouhyouOutKoumoku.OutputFormat;
                                decimal dataTmp = (decimal)codeData;
                                codeData = dataTmp.ToString("#,0");
                            }
                            else if (codeData.GetType() == typeof(double))
                            {   // 重量系
                                string format = chouhyouOutKoumoku.OutputFormat;
                                double dataTmp = (double)codeData;
                                codeData = dataTmp.ToString(format);
                            }

                            switch (itemColumnIndex)
                            {
                                case 0: // 帳票出力可能項目１番目
                                    dataRowNew["PHN_KAHEN4_1_VLB"] = code;
                                    dataRowNew["PHY_KAHEN2_1_VLB"] = codeData;

                                    break;
                                case 1: // 帳票出力可能項目２番目
                                    dataRowNew["PHN_KAHEN4_2_VLB"] = code;
                                    dataRowNew["PHY_KAHEN2_2_VLB"] = codeData;

                                    break;
                                case 2: // 帳票出力可能項目３番目
                                    dataRowNew["PHN_KAHEN4_3_VLB"] = code;
                                    dataRowNew["PHY_KAHEN2_3_VLB"] = codeData;

                                    break;
                                case 3: // 帳票出力可能項目４番目
                                    dataRowNew["PHN_KAHEN4_4_VLB"] = code;
                                    dataRowNew["PHY_KAHEN2_4_VLB"] = codeData;

                                    break;
                                case 4: // 帳票出力可能項目５番目
                                    dataRowNew["PHN_KAHEN4_5_VLB"] = code;
                                    dataRowNew["PHY_KAHEN2_5_VLB"] = codeData;

                                    break;
                                case 5: // 帳票出力可能項目６番目
                                    dataRowNew["PHN_KAHEN4_6_VLB"] = code;
                                    dataRowNew["PHY_KAHEN2_6_VLB"] = codeData;

                                    break;
                                case 6: // 帳票出力可能項目７番目
                                    dataRowNew["PHN_KAHEN4_7_VLB"] = code;
                                    dataRowNew["PHY_KAHEN2_7_VLB"] = codeData;

                                    break;
                                case 7: // 帳票出力可能項目８番目
                                    dataRowNew["PHN_KAHEN4_8_VLB"] = code;
                                    dataRowNew["PHY_KAHEN2_8_VLB"] = codeData;
                                    break;
                            }
                        }

                        #endregion - 帳票出力項目（伝票）用タイトルカラム -

                        // 明細行番
                        index = this.InputDataTable[inTable].Columns.IndexOf("ROW_NUMBER");
                        if (!this.IsDBNull(dataRow.ItemArray[index]))
                        {   // データーがNULLでない
                            dataRowNew["PHY_ROW_NO_FLB"] = dataRow.ItemArray[index];
                        }
                        else
                        {
                            dataRowNew["PHY_ROW_NO_FLB"] = string.Empty;
                        }

                        #region - 帳票出力項目（明細）用タイトルカラム -

                        itemColumnIndex = 0;
                        for (int i = 0; i < this.SelectChouhyouOutKoumokuMeisaiList.Count; i++, itemColumnIndex++)
                        {
                            ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = this.SelectChouhyouOutKoumokuMeisaiList[i];
                            ChouhyouOutKoumoku chouhyouOutKoumoku = chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList[0];

                            // テーブル名取得
                            tableName = chouhyouOutKoumoku.TableName;

                            // テーブルの該当フィールド名取得
                            fieldName = chouhyouOutKoumoku.FieldName;

                            // コード
                            index = this.InputDataTable[inTable].Columns.IndexOf(chouhyouOutKoumoku.FieldName);
                            if (!this.IsDBNull(dataRow.ItemArray[index]))
                            {   // データーがNULLでない
                                code = dataRow.ItemArray[index];
                            }
                            else
                            {
                                code = string.Empty;
                            }

                            Type type = code.GetType();

                            bool isSqlExec = true;

                            if (type == typeof(string))
                            {   // 文字列型
                                code = ((string)code).Replace(" ", string.Empty);
                                if (((string)code).Equals(string.Empty))
                                {
                                    isSqlExec = false;
                                }
                            }

                            // コード名称
                            if (isSqlExec)
                            {   // SQL実行
                                DataTable dataTableTmp;

                                if (type == typeof(DateTime))
                                {   // 日付型
                                    sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}'", tableName, fieldName, tableName, tableName, fieldName, code);

                                    dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                                    index = dataTableTmp.Columns.IndexOf(fieldName);
                                    if (dataTableTmp.Rows.Count != 0)
                                    {
                                        codeData = ((DateTime)dataTableTmp.Rows[0].ItemArray[index]).ToString("yyyy/MM/dd");
                                    }
                                    else
                                    {
                                        codeData = string.Empty;
                                    }
                                }
                                else
                                {   // 通常
                                    if (chouhyouOutKoumoku.FieldName.Contains("CD"))
                                    {   // CD（コードを含む）
                                        string tableNameRef = string.Empty;
                                        string fieldNameRef = string.Empty;
                                        string fieldNameRyaku = string.Empty;

                                        // 参照すべきテーブル・フィールド名の取得
                                        chouhyouOutKoumoku.GetTableAndFieldNameRef(tableName, ref tableNameRef, ref fieldNameRef, ref fieldNameRyaku);

                                        sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = {5}", tableNameRef, fieldNameRyaku, tableNameRef, tableNameRef, fieldNameRef, code);

                                        dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                                        index = dataTableTmp.Columns.IndexOf(fieldNameRyaku);
                                        if (dataTableTmp.Rows.Count != 0)
                                        {
                                            codeData = dataTableTmp.Rows[0].ItemArray[index];
                                        }
                                        else
                                        {
                                            codeData = string.Empty;
                                        }
                                    }
                                    else
                                    {
                                        if (type == typeof(string))
                                        {   // 文字列型
                                            code = "'" + code + "'";
                                            sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = {5}", tableName, fieldName, tableName, tableName, fieldName, code);

                                            dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                                            index = dataTableTmp.Columns.IndexOf(fieldName);
                                            if (dataTableTmp.Rows.Count != 0)
                                            {
                                                codeData = dataTableTmp.Rows[0].ItemArray[index];
                                            }
                                            else
                                            {
                                                codeData = string.Empty;
                                            }
                                        }
                                        else
                                        {
                                            sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = {5}", tableName, fieldName, tableName, tableName, fieldName, code);

                                            dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                                            index = dataTableTmp.Columns.IndexOf(fieldName);
                                            if (dataTableTmp.Rows.Count != 0)
                                            {
                                                codeData = dataTableTmp.Rows[0].ItemArray[index];
                                            }
                                            else
                                            {
                                                codeData = string.Empty;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                code = string.Empty;
                                codeData = string.Empty;
                            }

                            if (!chouhyouOutKoumoku.FieldName.Contains("CD"))
                            {   // フィールド名にCD（コード）が含まれていない
                                code = string.Empty;
                            }

                            if (codeData.GetType() == typeof(decimal))
                            {   // 金額系
                                string format = chouhyouOutKoumoku.OutputFormat;
                                decimal dataTmp = (decimal)codeData;
                                codeData = dataTmp.ToString("#,0"); 
                            }
                            else if (codeData.GetType() == typeof(double))
                            {   // 重量系
                                string format = chouhyouOutKoumoku.OutputFormat;
                                double dataTmp = (double)codeData;
                                codeData = dataTmp.ToString(format);
                            }

                            switch (itemColumnIndex)
                            {
                                case 0: // 帳票出力可能項目１番目
                                    dataRowNew["PHN_KAHEN4_9_VLB"] = code;
                                    dataRowNew["PHY_KAHEN3_1_VLB"] = codeData;

                                    break;
                                case 1: // 帳票出力可能項目２番目
                                    dataRowNew["PHN_KAHEN4_10_VLB"] = code;
                                    dataRowNew["PHY_KAHEN3_2_VLB"] = codeData;

                                    break;
                                case 2: // 帳票出力可能項目３番目
                                    dataRowNew["PHN_KAHEN4_11_VLB"] = code;
                                    dataRowNew["PHY_KAHEN3_3_VLB"] = codeData;

                                    break;
                                case 3: // 帳票出力可能項目４番目
                                    dataRowNew["PHN_KAHEN4_12_VLB"] = code;
                                    dataRowNew["PHY_KAHEN3_4_VLB"] = codeData;

                                    break;
                                case 4: // 帳票出力可能項目５番目
                                    dataRowNew["PHN_KAHEN4_13_VLB"] = code;
                                    dataRowNew["PHY_KAHEN3_5_VLB"] = codeData;

                                    break;
                                case 5: // 帳票出力可能項目６番目
                                    dataRowNew["PHN_KAHEN4_14_VLB"] = code;
                                    dataRowNew["PHY_KAHEN3_6_VLB"] = codeData;

                                    break;
                                case 6: // 帳票出力可能項目７番目
                                    dataRowNew["PHN_KAHEN4_15_VLB"] = code;
                                    dataRowNew["PHY_KAHEN3_7_VLB"] = codeData;

                                    break;
                                case 7: // 帳票出力可能項目８番目
                                    dataRowNew["PHN_KAHEN4_16_VLB"] = code;
                                    dataRowNew["PHY_KAHEN3_8_VLB"] = codeData;

                                    break;
                            }
                        }

                        #endregion - 帳票出力項目（明細）用タイトルカラム -

                        // 入金合計/出金合計
                        index = -1;
                        if (this.WindowID == WINDOW_ID.R_NYUUKIN_MEISAIHYOU)
                        {   // R366(入金明細表)
                            index = this.InputDataTable[inTable].Columns.IndexOf("NYUUKIN_AMOUNT_TOTAL");
                        }
                        else if (this.WindowID == WINDOW_ID.R_SYUKKINN_MEISAIHYOU)
                        {   // R373(出金明細表)
                            index = this.InputDataTable[inTable].Columns.IndexOf("SHUKKIN_AMOUNT_TOTAL");
                        }

                        if (!this.IsDBNull(dataRow.ItemArray[index]))
                        {   // データーがNULLでない
                            nyuukinSyutsukinAmountTotal = (decimal)dataRow.ItemArray[index];
                        }
                        else
                        {
                            nyuukinSyutsukinAmountTotal = 0;
                        }

                        // 調整
                        index = this.InputDataTable[inTable].Columns.IndexOf("CHOUSEI_AMOUNT_TOTAL");
                        if (!this.IsDBNull(dataRow.ItemArray[index]))
                        {   // データーがNULLでない
                            chouseiAmountTotal = (decimal)dataRow.ItemArray[index];
                        }
                        else
                        {
                            chouseiAmountTotal = 0;
                        }

                        // 伝票合計
                        decimal denpyouGoukei = nyuukinSyutsukinAmountTotal + chouseiAmountTotal;
                        dataRowNew["PHN_DENPYOU_TOTAL_FLB"] = denpyouGoukei;

                        // 金額
                        index = this.InputDataTable[inTable].Columns.IndexOf("KINGAKU");

                        decimal kingaku;
                        if (!this.IsDBNull(dataRow.ItemArray[index]))
                        {   // データーがNULLでない
                            kingaku = (decimal)dataRow.ItemArray[index];
                        }
                        else
                        {
                            kingaku = 0;
                        }

                        for (itemColumnIndex = 0; itemColumnIndex < syuukeiKoumokuEnableGroupCount; itemColumnIndex++)
                        {
                            syuukeiKoumokuGoukei[itemColumnIndex] = denpyouGoukei;
                            syuukeiKoumokuGoukeiTotal[itemColumnIndex] += kingaku;

                            switch (itemColumnIndex)
                            {
                                case 0: // 集計項目１
                                    dataRowNew["PHN_FILL_COND_ID_1_KAHEN1_1_VLB"] = syuukeiKoumokuCode[itemColumnIndex];
                                    dataRowNew["PHN_FILL_COND_ID_1_KAHEN1_2_VLB"] = syuukeiKoumokuCodeName[itemColumnIndex];
                                    dataRowNew["PHN_FILL_COND_ID_1_TOTAL_FLB"] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];

                                    break;
                                case 1: // 集計項目２
                                    dataRowNew["PHN_FILL_COND_ID_2_KAHEN1_3_VLB"] = syuukeiKoumokuCode[itemColumnIndex];
                                    dataRowNew["PHN_FILL_COND_ID_2_KAHEN1_4_VLB"] = syuukeiKoumokuCodeName[itemColumnIndex];
                                    dataRowNew["PHN_FILL_COND_ID_2_TOTAL_FLB"] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];

                                    break;
                                case 2: // 集計項目３
                                    dataRowNew["PHN_FILL_COND_ID_3_KAHEN1_5_VLB"] = syuukeiKoumokuCode[itemColumnIndex];
                                    dataRowNew["PHN_FILL_COND_ID_3_KAHEN1_6_VLB"] = syuukeiKoumokuCodeName[itemColumnIndex];
                                    dataRowNew["PHN_FILL_COND_ID_3_TOTAL_FLB"] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];

                                    break;
                                case 3: // 集計項目４
                                    //dataRowNew["PHN_FILL_COND_ID_4_KAHEN1_5_VLB"] = "1000";
                                    //dataRowNew["PHN_FILL_COND_ID_4_KAHEN1_6_VLB"] = "1001";
                                    //dataRowNew["PHN_FILL_COND_ID_4_TOTAL_FLB"] = "1002";

                                    break;
                            }
                        }

                        this.ChouhyouDataTable.Rows.Add(dataRowNew);
                    }
                }
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
                throw e;
            }
        }

        #endregion - Methods -
    }

    #endregion - CommonChouhyouR366_R373 -

    #endregion - Classes -
}
