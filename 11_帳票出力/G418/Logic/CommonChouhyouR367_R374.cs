using System;
using System.Collections.Generic;
using System.Data;
using r_framework.Const;
using r_framework.Utility;

namespace Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup
{
    #region - Classes -

    #region - CommonChouhyouR367_R374 -

    /// <summary>入金集計表(R367)・出金集計表(R374)を表すクラス・コントロール</summary>
    public class CommonChouhyouR367_R374 : CommonChouhyouBase
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="CommonChouhyouR367_R374"/> class.</summary>
        /// <param name="windowID">画面ＩＤ</param>
        public CommonChouhyouR367_R374(WINDOW_ID windowID)
            : base(windowID)
        {
            // 帳票出力フルパスフォーム名
            this.OutputFormFullPathName = CommonChouhyouBase.TemplatePath + "R367_R374-Form.xml";

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";

            // 選択可能な集計項目グループ数
            this.SelectEnableSyuukeiKoumokuGroupCount = 3;

            // 選択可能な集計項目
            if (windowID == WINDOW_ID.R_NYUUKIN_SYUUKEIHYOU)
            {   // R367(入金集計表)
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
            else if (windowID == WINDOW_ID.R_SYUKKINN_ICHIRANHYOU)
            {   // R374(出金集計表)
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

                // 帳票出力項目領域（伝票部又は明細部）
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN3_1_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_1_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN3_2_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_2_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN3_3_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_3_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN3_4_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_4_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN3_5_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_5_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN3_6_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_6_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN3_7_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_7_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN3_8_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_8_VLB");

                // 集計項目３
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_3_CD_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_3_NAME_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_3_TOTAL_FLB");

                // 集計項目２
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_2_CD_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_2_NAME_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_2_TOTAL_FLB");

                // 集計項目１
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_1_CD_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_1_NAME_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_1_TOTAL_FLB");

                // 伝票合計
                this.ChouhyouDataTable.Columns.Add("PHN_TOTAL_KINGAKU_FLB");

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

                    // 有効な集計項目グループ数
                    int syuukeiKoumokuEnableGroupCount = this.SelectEnableSyuukeiKoumokuGroupCount;

                    decimal nyuukinSyutsukinAmountTotal = 0;
                    decimal chouseiAmountTotal = 0;
                    decimal[] syuukeiKoumokuGoukei = new decimal[syuukeiKoumokuEnableGroupCount];
                    decimal[] syuukeiKoumokuGoukeiTotal = new decimal[syuukeiKoumokuEnableGroupCount];
                    string[] syuukeiKoumokuCode = new string[syuukeiKoumokuEnableGroupCount];
                    string[] syuukeiKoumokuCodeName = new string[syuukeiKoumokuEnableGroupCount];

                    decimal kingaku = 0;
                    decimal denpyouGoukei = 0;
                    decimal totalKingaku = 0;

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
                                    if (syuukeiKoumoku.FieldCD.Equals("TORIHIKISAKI_CD") || syuukeiKoumoku.FieldCD.Equals("NYUUKINSAKI_CD"))
                                    {
                                        sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}'", tableName, fieldName, tableName, tableName, syuukeiKoumoku.FieldCD, code);
                                    }
                                    else
                                    {
                                        sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = {5}", tableName, fieldName, tableName, tableName, syuukeiKoumoku.FieldCD, code);
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

                                for (int j = 0; j < syuukeiKoumokuEnableGroupCount; j++)
                                {
                                    totalKingaku += syuukeiKoumokuGoukeiTotal[j];
                                }

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

                        #region - 帳票出力項目（伝票部又は明細部）用タイトルカラム -

                        itemColumnIndex = 0;

                        int denpyouCount = this.SelectChouhyouOutKoumokuDepyouList.Count;
                        int meisaiCount = this.SelectChouhyouOutKoumokuMeisaiList.Count;
                        int maxCount = denpyouCount + meisaiCount;

                        ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup;
                        ChouhyouOutKoumoku chouhyouOutKoumoku;
                        for (int i = 0; i < maxCount; i++, itemColumnIndex++)
                        {
                            if (i < denpyouCount)
                            {
                                chouhyouOutKoumokuGroup = this.SelectChouhyouOutKoumokuDepyouList[i];
                            }
                            else if (i < maxCount)
                            {
                                chouhyouOutKoumokuGroup = this.SelectChouhyouOutKoumokuMeisaiList[i - denpyouCount];
                            }
                            else
                            {
                                break;
                            }

                            chouhyouOutKoumoku = chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList[0];

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
                                        else if (chouhyouOutKoumoku.FieldName.Equals("TORIHIKISAKI_CD"))
                                        {
                                            sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}'", tableNameRef, fieldNameRyaku, tableNameRef, tableNameRef, fieldNameRef, code);
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
                                    else if (type == typeof(string))
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
                                    dataRowNew["PHN_KAHEN3_1_VLB"] = code;
                                    dataRowNew["PHY_KAHEN2_1_VLB"] = codeData;

                                    break;
                                case 1: // 帳票出力可能項目２番目
                                    dataRowNew["PHN_KAHEN3_2_VLB"] = code;
                                    dataRowNew["PHY_KAHEN2_2_VLB"] = codeData;

                                    break;
                                case 2: // 帳票出力可能項目３番目
                                    dataRowNew["PHN_KAHEN3_3_VLB"] = code;
                                    dataRowNew["PHY_KAHEN2_3_VLB"] = codeData;

                                    break;
                                case 3: // 帳票出力可能項目４番目
                                    dataRowNew["PHN_KAHEN3_4_VLB"] = code;
                                    dataRowNew["PHY_KAHEN2_4_VLB"] = codeData;

                                    break;
                                case 4: // 帳票出力可能項目５番目
                                    dataRowNew["PHN_KAHEN3_5_VLB"] = code;
                                    dataRowNew["PHY_KAHEN2_5_VLB"] = codeData;

                                    break;
                                case 5: // 帳票出力可能項目６番目
                                    dataRowNew["PHN_KAHEN3_6_VLB"] = code;
                                    dataRowNew["PHY_KAHEN2_6_VLB"] = codeData;

                                    break;
                                case 6: // 帳票出力可能項目７番目
                                    dataRowNew["PHN_KAHEN3_7_VLB"] = code;
                                    dataRowNew["PHY_KAHEN2_7_VLB"] = codeData;

                                    break;
                                case 7: // 帳票出力可能項目８番目
                                    dataRowNew["PHN_KAHEN3_8_VLB"] = code;
                                    dataRowNew["PHY_KAHEN2_8_VLB"] = codeData;
                                    break;
                            }
                        }

                        #endregion - 帳票出力項目（伝票部又は明細部）用タイトルカラム -

                        // 入金合計/出金合計
                        index = -1;
                        if (this.WindowID == WINDOW_ID.R_NYUUKIN_SYUUKEIHYOU)
                        {   // R367(入金集計表)
                            index = this.InputDataTable[inTable].Columns.IndexOf("NYUUKIN_AMOUNT_TOTAL");
                        }
                        else if (this.WindowID == WINDOW_ID.R_SYUKKINN_ICHIRANHYOU)
                        {   // R374(出金集計表)
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

                        // 総合金額
                        denpyouGoukei = nyuukinSyutsukinAmountTotal + chouseiAmountTotal;
                        dataRowNew["PHN_TOTAL_KINGAKU_FLB"] = denpyouGoukei;

                        // 金額
                        index = this.InputDataTable[inTable].Columns.IndexOf("KINGAKU");

                        if (!this.IsDBNull(dataRow.ItemArray[index]))
                        {   // データーがNULLでない
                            kingaku = (decimal)dataRow.ItemArray[index];
                        }
                        else
                        {
                            kingaku = 0;
                        }

                        int koumokuNum = 0;   // No.3735
                        for (itemColumnIndex = 0; itemColumnIndex < syuukeiKoumokuEnableGroupCount; itemColumnIndex++)
                        {
                            int itemIndex = this.SelectSyuukeiKoumokuList[itemColumnIndex];
                            SyuukeiKoumoku syuukeiKoumoku = this.SyuukeiKomokuList[itemIndex];

                            if (syuukeiKoumoku.MasterTableID == WINDOW_ID.NONE)
                            {
                                continue;
                            }

                            syuukeiKoumokuGoukei[itemColumnIndex] = denpyouGoukei;
                            syuukeiKoumokuGoukeiTotal[itemColumnIndex] += kingaku;

                            switch (itemColumnIndex)
                            {
                                case 0: // 集計項目１
                                    dataRowNew["PHN_FILL_COND_ID_1_CD_VLB"] = syuukeiKoumokuCode[itemColumnIndex];
                                    dataRowNew["PHN_FILL_COND_ID_1_NAME_VLB"] = syuukeiKoumokuCodeName[itemColumnIndex];
                                    dataRowNew["PHN_FILL_COND_ID_1_TOTAL_FLB"] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];

                                    break;
                                case 1: // 集計項目２
                                    dataRowNew["PHN_FILL_COND_ID_2_CD_VLB"] = syuukeiKoumokuCode[itemColumnIndex];
                                    dataRowNew["PHN_FILL_COND_ID_2_NAME_VLB"] = syuukeiKoumokuCodeName[itemColumnIndex];
                                    dataRowNew["PHN_FILL_COND_ID_2_TOTAL_FLB"] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];

                                    break;
                                case 2: // 集計項目３
                                    dataRowNew["PHN_FILL_COND_ID_3_CD_VLB"] = syuukeiKoumokuCode[itemColumnIndex];
                                    dataRowNew["PHN_FILL_COND_ID_3_NAME_VLB"] = syuukeiKoumokuCodeName[itemColumnIndex];
                                    dataRowNew["PHN_FILL_COND_ID_3_TOTAL_FLB"] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];

                                    break;
                                case 3: // 集計項目４
                                    //dataRowNew["PHN_FILL_COND_ID_4_CD_VLB"] = syuukeiKoumokuCode[itemColumnIndex];
                                    //dataRowNew["PHN_FILL_COND_ID_4_NAME_VLB"] = syuukeiKoumokuCodeName[itemColumnIndex];
                                    //dataRowNew["PHN_FILL_COND_ID_4_TOTAL_FLB"] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];

                                    break;
                            }
                            koumokuNum++;   // No.3735
                        }

                        if (rowCount + 1 >= this.InputDataTable[inTable].Rows.Count)
                        {
                            for (int j = 0; j < syuukeiKoumokuEnableGroupCount; j++)
                            {
                                totalKingaku += syuukeiKoumokuGoukeiTotal[j];
                            }
                        }

                        // 総合計
                        //dataRowNew["PHN_TOTAL_KINGAKU_FLB"] = totalKingaku / syuukeiKoumokuEnableGroupCount;
                        dataRowNew["PHN_TOTAL_KINGAKU_FLB"] = totalKingaku / koumokuNum;     // No.3735

                        this.ChouhyouDataTable.Rows.Add(dataRowNew);
                    }
                }
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        #endregion - Methods -
    }

    #endregion - CommonChouhyouR367_R374 -

    #endregion - Classes -
}
