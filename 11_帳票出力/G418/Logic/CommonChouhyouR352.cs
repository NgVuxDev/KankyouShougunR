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

    #region - CommonChouhyouR352 -

    /// <summary>帳票Nを表すクラス・コントロール</summary>
    public class CommonChouhyouR352 : CommonChouhyouBase
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="CommonChouhyouR352"/> class.</summary>
        /// <param name="windowID">画面ＩＤ</param>
        public CommonChouhyouR352(WINDOW_ID windowID)
            : base(windowID)
        {
            // 帳票出力フルパスフォーム名
            this.OutputFormFullPathName = CommonChouhyouBase.TemplatePath + "R352-Form.xml";

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";

            // 選択可能な集計項目グループ数
            this.SelectEnableSyuukeiKoumokuGroupCount = 3;

            // 選択可能な集計項目
            if (windowID == WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU)
            {   // R352(計量集計表)
                this.SelectEnableSyuukeiKoumokuList = new List<int>()
                {
                    0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, /*11,*/ 12, 13, 14, 15, 16, 17
                };

                // 対象テーブルリスト
                this.TaishouTableList = new List<TaishouTable>()
                {
                    new TaishouTable("T_KEIRYOU_ENTRY", "T_KEIRYOU_DETAIL"),    // 計量
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

                // 受け渡し用データーテーブル（明細）フィールド名の設定処理
                this.SetDetailFieldNameForUkewatashi();

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
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_1_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_2_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_3_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_4_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_5_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_6_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_7_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_8_VLB");

                // 集計項目３
                this.ChouhyouDataTable.Columns.Add("G4F_FILL_COND_ID3_KINGAKU_TOTAL_1_VLB");
                this.ChouhyouDataTable.Columns.Add("G4F_FILL_COND_ID3_KINGAKU_TOTAL_2_VLB");
                this.ChouhyouDataTable.Columns.Add("G4F_FILL_COND_ID3_KINGAKU_TOTAL_3_VLB");
                this.ChouhyouDataTable.Columns.Add("G4F_FILL_COND_ID3_KINGAKU_TOTAL_4_VLB");
                this.ChouhyouDataTable.Columns.Add("G4F_FILL_COND_ID3_KINGAKU_TOTAL_5_VLB");
                this.ChouhyouDataTable.Columns.Add("G4F_FILL_COND_ID3_KINGAKU_TOTAL_6_VLB");
                this.ChouhyouDataTable.Columns.Add("G4F_FILL_COND_ID3_KINGAKU_TOTAL_7_VLB");
                this.ChouhyouDataTable.Columns.Add("G4F_FILL_COND_ID3_KINGAKU_TOTAL_8_VLB");

                // 集計項目２
                this.ChouhyouDataTable.Columns.Add("G3F_FILL_COND_ID2_KINGAKU_TOTAL_1_VLB");
                this.ChouhyouDataTable.Columns.Add("G3F_FILL_COND_ID2_KINGAKU_TOTAL_2_VLB");
                this.ChouhyouDataTable.Columns.Add("G3F_FILL_COND_ID2_KINGAKU_TOTAL_3_VLB");
                this.ChouhyouDataTable.Columns.Add("G3F_FILL_COND_ID2_KINGAKU_TOTAL_4_VLB");
                this.ChouhyouDataTable.Columns.Add("G3F_FILL_COND_ID2_KINGAKU_TOTAL_5_VLB");
                this.ChouhyouDataTable.Columns.Add("G3F_FILL_COND_ID2_KINGAKU_TOTAL_6_VLB");
                this.ChouhyouDataTable.Columns.Add("G3F_FILL_COND_ID2_KINGAKU_TOTAL_7_VLB");
                this.ChouhyouDataTable.Columns.Add("G3F_FILL_COND_ID2_KINGAKU_TOTAL_8_VLB");

                // 集計項目１
                this.ChouhyouDataTable.Columns.Add("G2F_FILL_COND_ID1_KINGAKU_TOTAL_1_VLB");
                this.ChouhyouDataTable.Columns.Add("G2F_FILL_COND_ID1_KINGAKU_TOTAL_2_VLB");
                this.ChouhyouDataTable.Columns.Add("G2F_FILL_COND_ID1_KINGAKU_TOTAL_3_VLB");
                this.ChouhyouDataTable.Columns.Add("G2F_FILL_COND_ID1_KINGAKU_TOTAL_4_VLB");
                this.ChouhyouDataTable.Columns.Add("G2F_FILL_COND_ID1_KINGAKU_TOTAL_5_VLB");
                this.ChouhyouDataTable.Columns.Add("G2F_FILL_COND_ID1_KINGAKU_TOTAL_6_VLB");
                this.ChouhyouDataTable.Columns.Add("G2F_FILL_COND_ID1_KINGAKU_TOTAL_7_VLB");
                this.ChouhyouDataTable.Columns.Add("G2F_FILL_COND_ID1_KINGAKU_TOTAL_8_VLB");

                // 伝票合計
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_1_FLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_2_FLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_3_FLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_4_FLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_5_FLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_6_FLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_7_FLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_8_FLB");

                #endregion - テーブルカラム作成 -

                if (this.InputDataTable == null || this.InputDataTable.Length == 0)
                {
                    return;
                }

                string sql;
                int itemColumnIndex = 0;
                int index = 0;
                int indexTmp = 0;

                for (int inTable = 0; inTable < this.InputDataTable.Length; inTable++)
                {
                    if (this.InputDataTable[inTable].Rows.Count == 0)
                    {   // データレコードが存在しない
                        continue;
                    }

                    string tableName = string.Empty;
                    string fieldName = string.Empty;

                    object code = null;
                    object codeData = null;

                    // 有効な集計項目グループ数
                    int syuukeiKoumokuEnableGroupCount = this.SelectEnableSyuukeiKoumokuGroupCount;

                    decimal[] syuukeiKoumokuGoukei = new decimal[syuukeiKoumokuEnableGroupCount];
                    decimal[] syuukeiKoumokuGoukeiTotal = new decimal[syuukeiKoumokuEnableGroupCount];
                    string[] syuukeiKoumokuCode = new string[syuukeiKoumokuEnableGroupCount];
                    string[] syuukeiKoumokuCodeName = new string[syuukeiKoumokuEnableGroupCount];

                    decimal kingaku = 0;
                    decimal denpyouGoukei = 0;
                    decimal totalKingaku = 0;

                    decimal[,] decKingaku =
                {
                    { 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0 }
                };

                    string[,] strKingaku =
                {
                    { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty },
                    { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty },
                    { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty },
                    { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty }
                };

                    string[] syuukeiKoumokuPrevN = new string[syuukeiKoumokuEnableGroupCount];

                    bool isRecPrint = true;

                    for (int i = 0; i < syuukeiKoumokuEnableGroupCount; i++)
                    {
                        syuukeiKoumokuPrevN[i] = "INITIALIZE VALUE";
                    }

                    string[] syuukeiKoumokuNextN = new string[syuukeiKoumokuEnableGroupCount];

                    for (int rowCount = 0; rowCount < this.InputDataTable[inTable].Rows.Count; rowCount++)
                    {
                        dataRow = this.InputDataTable[inTable].Rows[rowCount];

                        DataRow dataRowNew = this.ChouhyouDataTable.NewRow();

                        // 受渡用DataRow作成
                        DataRow dataRowUkewatashi = this.DataTableUkewatashi.NewRow();

                        #region - 集計項目用タイトルカラム -

                        string gyoushaCd = string.Empty;
                        string gyoushaFieleName = string.Empty;
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
                                switch (syuukeiKoumoku.Type)
                                {
                                    case SYUKEUKOMOKU_TYPE.DensyuKubunBetsu:    // 伝種区分別
                                        code = (inTable + 1).ToString();

                                        break;
                                    case SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu: // 荷降業者別
                                        index = this.InputDataTable[inTable].Columns.IndexOf("NIOROSHI_GYOUSHA_CD");

                                        break;
                                    case SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu:  // 荷降現場別
                                        index = this.InputDataTable[inTable].Columns.IndexOf("NIOROSHI_GENBA_CD");

                                        break;
                                    case SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu:   // 荷積業者別
                                        index = this.InputDataTable[inTable].Columns.IndexOf("NIZUMI_GYOUSHA_CD");

                                        break;
                                    case SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu:    // 荷積現場別
                                        index = this.InputDataTable[inTable].Columns.IndexOf("NIZUMI_GENBA_CD");

                                        break;
                                    case SYUKEUKOMOKU_TYPE.UnpanGyoshaBetsu:    // 運搬業者別
                                        index = this.InputDataTable[inTable].Columns.IndexOf("UNPAN_GYOUSHA_CD");

                                        break;
                                    default:
                                        index = this.InputDataTable[inTable].Columns.IndexOf(syuukeiKoumoku.FieldCD);

                                        break;
                                }
                            }

                            if (index != -1)
                            {
                                if (!this.IsDBNull(dataRow.ItemArray[index]))
                                {   // データーがNULLでない
                                    code = dataRow.ItemArray[index];
                                }
                                else
                                {
                                    code = string.Empty;
                                }
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
                                    if (syuukeiKoumoku.FieldCD.Equals("GYOUSHA_CD") || syuukeiKoumoku.FieldCD.Equals("NIOROSHI_GYOUSHA_CD") || syuukeiKoumoku.FieldCD.Equals("NIZUMI_GYOUSHA_CD"))
                                    {
                                        // 現場名称の取得に備え、現場に紐づく業者CDを取得しておく
                                        gyoushaCd = code.ToString();
                                        gyoushaFieleName = syuukeiKoumoku.FieldCD;
                                    }

                                    if (syuukeiKoumoku.FieldCD.Equals("TORIHIKISAKI_CD"))
                                    {
                                        sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}'", tableName, fieldName, tableName, tableName, syuukeiKoumoku.FieldCD, code);
                                    }
                                    else if (syuukeiKoumoku.FieldCD.Equals("GYOUSHA_CD"))
                                    {
                                        sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}'", tableName, fieldName, tableName, tableName, syuukeiKoumoku.FieldCD, code);
                                    }
                                    else if (syuukeiKoumoku.FieldCD.Equals("HINMEI_CD"))
                                    {
                                        sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}'", tableName, fieldName, tableName, tableName, syuukeiKoumoku.FieldCD, code);
                                    }
                                    else if (syuukeiKoumoku.FieldCD.Equals("GENBA_CD"))
                                    {
                                        // 現場名称を取得
                                        sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}' AND {6}.{7} = '{8}'"
                                            , tableName, fieldName, tableName, tableName, syuukeiKoumoku.FieldCD, code, tableName, gyoushaFieleName, gyoushaCd);
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

                                for (int j = 0; j < 8; j++)
                                {
                                    decKingaku[itemColumnIndex, j] = 0;
                                    strKingaku[itemColumnIndex, j] = string.Empty;
                                }
                            }

                            syuukeiKoumokuCode[itemColumnIndex] = code.ToString();
                            syuukeiKoumokuCodeName[itemColumnIndex] = codeData.ToString();

                            switch (itemColumnIndex + 1)
                            {
                                case 1: // 集計項目１
                                    dataRowNew["PHY_KAHEN1_1_VLB"] = code.ToString();
                                    indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_1_CD");
                                    dataRowUkewatashi[indexTmp] = code.ToString();

                                    dataRowNew["PHY_KAHEN1_2_VLB"] = codeData.ToString();
                                    indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_1_NAME");
                                    dataRowUkewatashi[indexTmp] = codeData.ToString();

                                    break;
                                case 2: // 集計項目２
                                    dataRowNew["PHY_KAHEN1_3_VLB"] = code.ToString();
                                    indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_2_CD");
                                    dataRowUkewatashi[indexTmp] = code.ToString();

                                    dataRowNew["PHY_KAHEN1_4_VLB"] = codeData.ToString();
                                    indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_2_NAME");
                                    dataRowUkewatashi[indexTmp] = codeData.ToString();

                                    break;
                                case 3: // 集計項目３
                                    dataRowNew["PHY_KAHEN1_5_VLB"] = code.ToString();
                                    indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_3_CD");
                                    dataRowUkewatashi[indexTmp] = code.ToString();

                                    dataRowNew["PHY_KAHEN1_6_VLB"] = codeData.ToString();
                                    indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_3_NAME");
                                    dataRowUkewatashi[indexTmp] = codeData.ToString();

                                    break;
                                case 4: // 集計項目４
                                    dataRowNew["PHY_KAHEN1_7_VLB"] = code.ToString();
                                    indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_4_CD");
                                    dataRowUkewatashi[indexTmp] = code.ToString();

                                    dataRowNew["PHY_KAHEN1_8_VLB"] = codeData.ToString();
                                    indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_4_NAME");
                                    dataRowUkewatashi[indexTmp] = codeData.ToString();

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
                        bool isDenpyou = true;
                        for (int i = 0; i < maxCount; i++, itemColumnIndex++)
                        {
                            if (i < denpyouCount)
                            {
                                chouhyouOutKoumokuGroup = this.SelectChouhyouOutKoumokuDepyouList[i];
                                isDenpyou = true;
                            }
                            else if (i < maxCount)
                            {
                                chouhyouOutKoumokuGroup = this.SelectChouhyouOutKoumokuMeisaiList[i - denpyouCount];
                                isDenpyou = false;
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
                                        else if (chouhyouOutKoumoku.FieldName.Equals("YOUKI_CD"))
                                        {
                                            fieldNameRyaku = "YOUKI_NAME_RYAKU";
                                            tableNameRef = "M_YOUKI";
                                            sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}'", tableNameRef, fieldNameRyaku, tableNameRef, tableNameRef, fieldNameRef, code);
                                        }
                                        else if (chouhyouOutKoumoku.FieldName.Equals("KEITAI_KBN_CD"))
                                        {
                                            fieldNameRyaku = "KEITAI_KBN_NAME_RYAKU";
                                            tableNameRef = "M_KEITAI_KBN";
                                            sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}'", tableNameRef, fieldNameRyaku, tableNameRef, tableNameRef, fieldNameRef, code);
                                        }
                                        else if (chouhyouOutKoumoku.FieldName.Equals("MANIFEST_SHURUI_CD"))
                                        {
                                            fieldNameRyaku = "MANIFEST_SHURUI_NAME_RYAKU";
                                            tableNameRef = "M_MANIFEST_SHURUI";
                                            sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}'", tableNameRef, fieldNameRyaku, tableNameRef, tableNameRef, fieldNameRef, code);
                                        }
                                        else if (chouhyouOutKoumoku.FieldName.Equals("MANIFEST_TEHAI_CD"))
                                        {
                                            fieldNameRyaku = "MANIFEST_TEHAI_NAME_RYAKU";
                                            tableNameRef = "M_MANIFEST_TEHAI";
                                            sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}'", tableNameRef, fieldNameRyaku, tableNameRef, tableNameRef, fieldNameRef, code);
                                        }
                                        else if (chouhyouOutKoumoku.FieldName.Equals("NISUGATA_UNIT_CD"))
                                        {
                                            fieldNameRef = "UNIT_CD";
                                            fieldNameRyaku = "UNIT_NAME_RYAKU";
                                            tableNameRef = "M_UNIT";
                                            sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}'", tableNameRef, fieldNameRyaku, tableNameRef, tableNameRef, fieldNameRef, code);
                                        }
                                        else
                                        {
                                            sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = {5}", tableNameRef, fieldNameRyaku, tableNameRef, tableNameRef, fieldNameRef, code);
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
                                codeData = dataTmp.ToString(format);
                            }
                            else if (codeData.GetType() == typeof(double))
                            {   // 重量系
                                string format = chouhyouOutKoumoku.OutputFormat;
                                double dataTmp = (double)codeData;
                                codeData = dataTmp.ToString(format);
                            }

                            if (string.IsNullOrEmpty(codeData.ToString()))
                            {
                                if (itemColumnIndex == 0)
                                {
                                    isRecPrint = false;
                                }

                                continue;
                            }
                            else
                            {
                                isRecPrint = true;
                            }

                            switch (itemColumnIndex)
                            {
                                case 0: // 帳票出力可能項目１番目
                                    dataRowNew["PHY_KAHEN2_1_VLB"] = codeData;
                                    indexTmp = isDenpyou ? this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_1_VALUE") : this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_1_VALUE");
                                    dataRowUkewatashi[indexTmp] = codeData;

                                    if (chouhyouOutKoumoku.IsTotalKubun)
                                    {
                                        if (this.IsNumeric(codeData.ToString()))
                                        {
                                            for (int j = 0; j < 4; j++)
                                            {
                                                decKingaku[j, itemColumnIndex] += decimal.Parse(codeData.ToString());
                                                strKingaku[j, itemColumnIndex] = decKingaku[j, itemColumnIndex].ToString(chouhyouOutKoumoku.OutputFormat);
                                            }
                                        }
                                        else
                                        {
                                            for (int j = 0; j < 4; j++)
                                            {
                                                decKingaku[j, itemColumnIndex] += 0;
                                                strKingaku[j, itemColumnIndex] = decKingaku[j, itemColumnIndex].ToString(chouhyouOutKoumoku.OutputFormat);
                                            }
                                        }
                                    }

                                    break;
                                case 1: // 帳票出力可能項目２番目
                                    dataRowNew["PHY_KAHEN2_2_VLB"] = codeData;
                                    indexTmp = isDenpyou ? this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_2_VALUE") : this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_2_VALUE");
                                    dataRowUkewatashi[indexTmp] = codeData;

                                    if (chouhyouOutKoumoku.IsTotalKubun)
                                    {
                                        if (this.IsNumeric(codeData.ToString()))
                                        {
                                            for (int j = 0; j < 4; j++)
                                            {
                                                decKingaku[j, itemColumnIndex] += decimal.Parse(codeData.ToString());
                                                strKingaku[j, itemColumnIndex] = decKingaku[j, itemColumnIndex].ToString(chouhyouOutKoumoku.OutputFormat);
                                            }
                                        }
                                        else
                                        {
                                            for (int j = 0; j < 4; j++)
                                            {
                                                decKingaku[j, itemColumnIndex] += 0;
                                                strKingaku[j, itemColumnIndex] = decKingaku[j, itemColumnIndex].ToString(chouhyouOutKoumoku.OutputFormat);
                                            }
                                        }
                                    }

                                    break;
                                case 2: // 帳票出力可能項目３番目
                                    dataRowNew["PHY_KAHEN2_3_VLB"] = codeData;
                                    indexTmp = isDenpyou ? this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_3_VALUE") : this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_3_VALUE");
                                    dataRowUkewatashi[indexTmp] = codeData;

                                    if (chouhyouOutKoumoku.IsTotalKubun)
                                    {
                                        if (this.IsNumeric(codeData.ToString()))
                                        {
                                            for (int j = 0; j < 4; j++)
                                            {
                                                decKingaku[j, itemColumnIndex] += decimal.Parse(codeData.ToString());
                                                strKingaku[j, itemColumnIndex] = decKingaku[j, itemColumnIndex].ToString(chouhyouOutKoumoku.OutputFormat);
                                            }
                                        }
                                        else
                                        {
                                            for (int j = 0; j < 4; j++)
                                            {
                                                decKingaku[j, itemColumnIndex] += 0;
                                                strKingaku[j, itemColumnIndex] = decKingaku[j, itemColumnIndex].ToString(chouhyouOutKoumoku.OutputFormat);
                                            }
                                        }
                                    }

                                    break;
                                case 3: // 帳票出力可能項目４番目
                                    dataRowNew["PHY_KAHEN2_4_VLB"] = codeData;
                                    indexTmp = isDenpyou ? this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_4_VALUE") : this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_4_VALUE");
                                    dataRowUkewatashi[indexTmp] = codeData;

                                    if (chouhyouOutKoumoku.IsTotalKubun)
                                    {
                                        if (this.IsNumeric(codeData.ToString()))
                                        {
                                            for (int j = 0; j < 4; j++)
                                            {
                                                decKingaku[j, itemColumnIndex] += decimal.Parse(codeData.ToString());
                                                strKingaku[j, itemColumnIndex] = decKingaku[j, itemColumnIndex].ToString(chouhyouOutKoumoku.OutputFormat);
                                            }
                                        }
                                        else
                                        {
                                            for (int j = 0; j < 4; j++)
                                            {
                                                decKingaku[j, itemColumnIndex] += 0;
                                                strKingaku[j, itemColumnIndex] = decKingaku[j, itemColumnIndex].ToString(chouhyouOutKoumoku.OutputFormat);
                                            }
                                        }
                                    }

                                    break;
                                case 4: // 帳票出力可能項目５番目
                                    dataRowNew["PHY_KAHEN2_5_VLB"] = codeData;
                                    indexTmp = isDenpyou ? this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_5_VALUE") : this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_5_VALUE");
                                    dataRowUkewatashi[indexTmp] = codeData;

                                    if (chouhyouOutKoumoku.IsTotalKubun)
                                    {
                                        if (this.IsNumeric(codeData.ToString()))
                                        {
                                            for (int j = 0; j < 4; j++)
                                            {
                                                decKingaku[j, itemColumnIndex] += decimal.Parse(codeData.ToString());
                                                strKingaku[j, itemColumnIndex] = decKingaku[j, itemColumnIndex].ToString(chouhyouOutKoumoku.OutputFormat);
                                            }
                                        }
                                        else
                                        {
                                            for (int j = 0; j < 4; j++)
                                            {
                                                decKingaku[j, itemColumnIndex] += 0;
                                                strKingaku[j, itemColumnIndex] = decKingaku[j, itemColumnIndex].ToString(chouhyouOutKoumoku.OutputFormat);
                                            }
                                        }
                                    }

                                    break;
                                case 5: // 帳票出力可能項目６番目
                                    dataRowNew["PHY_KAHEN2_6_VLB"] = codeData;
                                    indexTmp = isDenpyou ? this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_6_VALUE") : this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_6_VALUE");
                                    dataRowUkewatashi[indexTmp] = codeData;

                                    if (chouhyouOutKoumoku.IsTotalKubun)
                                    {
                                        if (this.IsNumeric(codeData.ToString()))
                                        {
                                            for (int j = 0; j < 4; j++)
                                            {
                                                decKingaku[j, itemColumnIndex] += decimal.Parse(codeData.ToString());
                                                strKingaku[j, itemColumnIndex] = decKingaku[j, itemColumnIndex].ToString(chouhyouOutKoumoku.OutputFormat);
                                            }
                                        }
                                        else
                                        {
                                            for (int j = 0; j < 4; j++)
                                            {
                                                decKingaku[j, itemColumnIndex] += 0;
                                                strKingaku[j, itemColumnIndex] = decKingaku[j, itemColumnIndex].ToString(chouhyouOutKoumoku.OutputFormat);
                                            }
                                        }
                                    }

                                    break;
                                case 6: // 帳票出力可能項目７番目
                                    dataRowNew["PHY_KAHEN2_7_VLB"] = codeData;
                                    indexTmp = isDenpyou ? this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_7_VALUE") : this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_7_VALUE");
                                    dataRowUkewatashi[indexTmp] = codeData;

                                    if (chouhyouOutKoumoku.IsTotalKubun)
                                    {
                                        if (this.IsNumeric(codeData.ToString()))
                                        {
                                            for (int j = 0; j < 4; j++)
                                            {
                                                decKingaku[j, itemColumnIndex] += decimal.Parse(codeData.ToString());
                                                strKingaku[j, itemColumnIndex] = decKingaku[j, itemColumnIndex].ToString(chouhyouOutKoumoku.OutputFormat);
                                            }
                                        }
                                        else
                                        {
                                            for (int j = 0; j < 4; j++)
                                            {
                                                decKingaku[j, itemColumnIndex] += 0;
                                                strKingaku[j, itemColumnIndex] = decKingaku[j, itemColumnIndex].ToString(chouhyouOutKoumoku.OutputFormat);
                                            }
                                        }
                                    }

                                    break;
                                case 7: // 帳票出力可能項目８番目
                                    dataRowNew["PHY_KAHEN2_8_VLB"] = codeData;
                                    indexTmp = isDenpyou ? this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_8_VALUE") : this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_8_VALUE");
                                    dataRowUkewatashi[indexTmp] = codeData;

                                    if (chouhyouOutKoumoku.IsTotalKubun)
                                    {
                                        if (this.IsNumeric(codeData.ToString()))
                                        {
                                            for (int j = 0; j < 4; j++)
                                            {
                                                decKingaku[j, itemColumnIndex] += decimal.Parse(codeData.ToString());
                                                strKingaku[j, itemColumnIndex] = decKingaku[j, itemColumnIndex].ToString(chouhyouOutKoumoku.OutputFormat);
                                            }
                                        }
                                        else
                                        {
                                            for (int j = 0; j < 4; j++)
                                            {
                                                decKingaku[j, itemColumnIndex] += 0;
                                                strKingaku[j, itemColumnIndex] = decKingaku[j, itemColumnIndex].ToString(chouhyouOutKoumoku.OutputFormat);
                                            }
                                        }
                                    }

                                    break;
                            }
                        }

                        #endregion - 帳票出力項目（伝票部又は明細部）用タイトルカラム -

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
                                    //dataRowNew["G2F_FILL_COND_ID1_KINGAKU_TOTAL_1_VLB"] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];
                                    //indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_1_TOTAL_1");
                                    //dataRowUkewatashi[indexTmp] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];

                                    for (int j = 0; j < 8; j++)
                                    {
                                        dataRowNew[string.Format("G2F_FILL_COND_ID1_KINGAKU_TOTAL_{0}_VLB", (j + 1).ToString())] = strKingaku[itemColumnIndex, j];
                                        indexTmp = this.DataTableUkewatashi.Columns.IndexOf(string.Format("FILL_COND_1_TOTAL_{0}", (j + 1).ToString()));
                                        dataRowUkewatashi[indexTmp] = strKingaku[itemColumnIndex, j];
                                    }

                                    break;
                                case 1: // 集計項目２
                                    //dataRowNew["G3F_FILL_COND_ID2_KINGAKU_TOTAL_1_VLB"] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];
                                    //indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_2_TOTAL_1");
                                    //dataRowUkewatashi[indexTmp] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];

                                    for (int j = 0; j < 8; j++)
                                    {
                                        dataRowNew[string.Format("G3F_FILL_COND_ID2_KINGAKU_TOTAL_{0}_VLB", (j + 1).ToString())] = strKingaku[itemColumnIndex, j];
                                        indexTmp = this.DataTableUkewatashi.Columns.IndexOf(string.Format("FILL_COND_2_TOTAL_{0}", (j + 1).ToString()));
                                        dataRowUkewatashi[indexTmp] = strKingaku[itemColumnIndex, j];
                                    }

                                    break;
                                case 2: // 集計項目３
                                    //dataRowNew["G4F_FILL_COND_ID3_KINGAKU_TOTAL_1_VLB"] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];
                                    //indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_3_TOTAL_1");
                                    //dataRowUkewatashi[indexTmp] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];

                                    for (int j = 0; j < 8; j++)
                                    {
                                        dataRowNew[string.Format("G4F_FILL_COND_ID3_KINGAKU_TOTAL_{0}_VLB", (j + 1).ToString())] = strKingaku[itemColumnIndex, j];
                                        indexTmp = this.DataTableUkewatashi.Columns.IndexOf(string.Format("FILL_COND_3_TOTAL_{0}", (j + 1).ToString()));
                                        dataRowUkewatashi[indexTmp] = strKingaku[itemColumnIndex, j];
                                    }

                                    break;
                                case 3: // 集計項目４
                                    //dataRowNew["G5F_FILL_COND_ID4_KINGAKU_TOTAL_1_VLB"] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];
                                    //indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_4_TOTAL_1");
                                    //dataRowUkewatashi[indexTmp] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];

                                    break;
                            }
                        }

                        if (rowCount + 1 >= this.InputDataTable[inTable].Rows.Count)
                        {
                            for (int j = 0; j < syuukeiKoumokuEnableGroupCount; j++)
                            {
                                totalKingaku += syuukeiKoumokuGoukeiTotal[j];
                            }
                        }

                        // 総合計
                        //dataRowNew["G1F_KINGAKU_TOTAL_1_FLB"] = totalKingaku / syuukeiKoumokuEnableGroupCount;
                        //indexTmp = this.DataTableUkewatashi.Columns.IndexOf("ALL_TOTAL_1");
                        //dataRowUkewatashi[indexTmp] = 0;

                        for (int j = 0; j < 8; j++)
                        {
                            dataRowNew[string.Format("G1F_KINGAKU_TOTAL_{0}_FLB", (j + 1).ToString())] = strKingaku[3, j];
                            indexTmp = this.DataTableUkewatashi.Columns.IndexOf(string.Format("ALL_TOTAL_{0}", (j + 1).ToString()));
                            dataRowUkewatashi[indexTmp] = strKingaku[3, j];
                        }

                        if (isRecPrint)
                        {
                            this.ChouhyouDataTable.Rows.Add(dataRowNew);
                            this.DataTableUkewatashi.Rows.Add(dataRowUkewatashi);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        private bool IsNumeric(string strTarget)
        {
            try
            {
                decimal decNum;

                return decimal.TryParse(strTarget, System.Globalization.NumberStyles.Any, null, out decNum);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);

                return false;
            }
        }

        #endregion - Methods -
    }

    #endregion - CommonChouhyouR352 -

    #endregion - Classes -
}
