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

    #region - CommonChouhyouR342 -

    /// <summary>R342(受付明細表)を表すクラス・コントロール</summary>
    public class CommonChouhyouR342 : CommonChouhyouBase
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="CommonChouhyouR342"/> class.</summary>
        /// <param name="windowID">画面ＩＤ</param>
        public CommonChouhyouR342(WINDOW_ID windowID)
            : base(windowID)
        {
            // 帳票出力フルパスフォーム名
            this.OutputFormFullPathName = CommonChouhyouBase.TemplatePath + "R342_R351_R398-Form.xml";

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";

            // 選択可能な集計項目グループ数
            this.SelectEnableSyuukeiKoumokuGroupCount = 3;

            // 選択可能な集計項目
            if (windowID == WINDOW_ID.R_UKETSUKE_MEISAIHYOU)
            {   // R342(受付明細表)

                this.IsDenpyouSyuruiEnable = true;
                this.IsDenpyouKubunEnable = false;

                this.SelectEnableSyuukeiKoumokuList = new List<int>()
                {
                    0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, /*11,*/ 12, 13, 14, 15, 16, 17
                };

                // 対象テーブルリスト
                this.TaishouTableList = new List<TaishouTable>()
                {
                    new TaishouTable("T_UKETSUKE_SS_ENTRY", "T_UKETSUKE_SS_DETAIL"),    // 受付(収集)
                    new TaishouTable("T_UKETSUKE_SK_ENTRY", "T_UKETSUKE_SK_DETAIL"),    // 受付(出荷)
                    new TaishouTable("T_UKETSUKE_MK_ENTRY", "T_UKETSUKE_MK_DETAIL"),    // 受付(持込)
                    new TaishouTable("T_UKETSUKE_BP_ENTRY", "T_UKETSUKE_BP_DETAIL"),    // 受付(物販)
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
                if (this.InputDataTable == null || this.InputDataTable.Length == 0)
                {
                    return;
                }

                // 受け渡し用データーテーブル（明細）フィールド名の設定処理
                this.SetDetailFieldNameForUkewatashi();

                string tmp;
                DataRow dataRow;
                DataRow dataRowSort;

                // 複数テーブルの並べ替え処理
                this.MultiSort();

                this.ChouhyouDataTable = new DataTable();

                SyuukeiKoumoku syuukeiKoumoku;

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
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN5_1_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_1_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN5_2_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_2_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN5_3_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_3_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN5_4_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_4_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN5_5_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_5_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN5_6_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_6_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN5_7_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_7_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN5_8_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_8_VLB");

                // 明細行番
                this.ChouhyouDataTable.Columns.Add("PHY_ROW_NO_FLB");

                // 帳票出力項目領域（明細部）
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN5_9_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN3_1_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN5_10_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN3_2_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN5_11_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN3_3_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN5_12_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN3_4_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN5_13_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN3_5_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN5_14_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN3_6_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN5_15_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN3_7_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KAHEN5_16_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN3_8_VLB");

                // 売上種類
                this.ChouhyouDataTable.Columns.Add("PHY_URIAGE_SHURUI_FLB");

                // 伝票合計
                this.ChouhyouDataTable.Columns.Add("PHY_KINGAKU_TOTAL_FLB");
                this.ChouhyouDataTable.Columns.Add("PHY_TAX_TOTAL_FLB");
                this.ChouhyouDataTable.Columns.Add("PHY_TOTAL_FLB");

                // 集計項目３
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_3_KAHEN1_5_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_3_KAHEN1_6_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_3_KINGAKU_TOTAL_FLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_3_TAX_TOTAL_FLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_3_TOTAL_FLB");

                // 集計項目２
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_2_KAHEN1_3_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_2_KAHEN1_4_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_2_KINGAKU_TOTAL_FLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_2_TAX_TOTAL_FLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_2_TOTAL_FLB");

                // 集計項目１
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_1_KAHEN1_1_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_1_KAHEN1_2_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_1_KINGAKU_TOTAL_FLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_1_TAX_TOTAL_FLB");
                this.ChouhyouDataTable.Columns.Add("PHN_FILL_COND_ID_1_TOTAL_FLB");

                // 総合計
                this.ChouhyouDataTable.Columns.Add("PHN_GOUKEI_KINGAKU_TOTAL_FLB");
                this.ChouhyouDataTable.Columns.Add("PHN_GOUKEI_TAX_TOTAL_FLB");
                this.ChouhyouDataTable.Columns.Add("PHN_GOUKEI_TOTAL_FLB");

                #endregion - テーブルカラム作成 -

                string sql = string.Empty;
                int itemColumnIndex = 0;

                string tableName = string.Empty;
                string fieldName = string.Empty;

                int index = 0;
                int indexTmp = 0;
                object code = null;
                object codeData = null;

                // 伝票番号
                long denpyouNoPrev = -1;
                long denpyouNoNext;

                // 有効な集計項目グループ数
                int syuukeiKoumokuEnableGroupCount = this.SelectEnableSyuukeiKoumokuGroupCount;

                decimal kingakuTotal = 0;
                decimal shouhizeiTotal = 0;
                decimal goukeiKingakuTotal = 0;

                decimal souGoukeikingakuTotal = 0;
                decimal souKingakuTotal = 0;
                decimal souShouhizeiTotal = 0;

                decimal[] syuukeiKoumokuKingakuKei = new decimal[syuukeiKoumokuEnableGroupCount];
                decimal[] syuukeiKoumokuShouhizei = new decimal[syuukeiKoumokuEnableGroupCount];
                decimal[] syuukeiKoumokuGoukeiTotal = new decimal[syuukeiKoumokuEnableGroupCount];
                string[] syuukeiKoumokuCode = new string[syuukeiKoumokuEnableGroupCount];
                string[] syuukeiKoumokuCodeName = new string[syuukeiKoumokuEnableGroupCount];

                string[] syuukeiKoumokuPrevN = new string[syuukeiKoumokuEnableGroupCount];
                for (int i = 0; i < syuukeiKoumokuEnableGroupCount; i++)
                {
                    syuukeiKoumokuPrevN[i] = "INITIALIZE VALUE";
                }

                string[] syuukeiKoumokuNextN = new string[syuukeiKoumokuEnableGroupCount];

                for (int rowCount = 0; rowCount < this.DataTableMultiSort.DefaultView.Count; rowCount++)
                {
                    dataRowSort = this.DataTableMultiSort.DefaultView[rowCount].Row;
                    dataRow = this.DataTableMultiSort.DefaultView[rowCount].Row;

                    DataRow dataRowNew = this.ChouhyouDataTable.NewRow();

                    // 受渡用DataRow作成
                    DataRow dataRowUkewatashi = this.DataTableUkewatashi.NewRow();

                    int indexTable = int.Parse((string)dataRowSort.ItemArray[this.SelectSyuukeiKoumokuList.Count]);
                    int indexTableRow = int.Parse((string)dataRowSort.ItemArray[this.SelectSyuukeiKoumokuList.Count + 1]);

                    // 伝票番号
                    index = this.InputDataTable[indexTable].Columns.IndexOf("UKETSUKE_NUMBER");

                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                    {
                        denpyouNoNext = (long)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                    }
                    else
                    {
                        denpyouNoNext = -1;
                    }

                    #region - 集計項目用タイトルカラム -

                    string gyoushaCd = string.Empty;
                    string gyoushaFieleName = string.Empty;
                    for (itemColumnIndex = 0; itemColumnIndex < syuukeiKoumokuEnableGroupCount; itemColumnIndex++)
                    {
                        int itemIndex = this.SelectSyuukeiKoumokuList[itemColumnIndex];
                        syuukeiKoumoku = this.SyuukeiKomokuList[itemIndex];

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
                            index = this.InputDataTable[indexTable].Columns.IndexOf("EIGYOU_TANTOUSHA_CD");

                            if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow]))
                            {
                                code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                            }
                            else
                            {
                                code = string.Empty;
                            }
                        }
                        else
                        {   // その他
                            switch (syuukeiKoumoku.Type)
                            {
                                case SYUKEUKOMOKU_TYPE.DensyuKubunBetsu:    // 伝種区分別
                                    code = (indexTable + 1).ToString();

                                    break;
                                case SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu: // 荷卸業者別

                                    index = this.InputDataTable[indexTable].Columns.IndexOf("NIOROSHI_GYOUSHA_CD");
                                    if (index == -1)
                                    {
                                        code = string.Empty;
                                    }
                                    else
                                    {
                                        if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                        {
                                            code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                        }
                                        else
                                        {
                                            code = string.Empty;
                                        }
                                    }

                                    break;
                                case SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu:  // 荷卸現場別
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("NIOROSHI_GENBA_CD");
                                    if (index == -1)
                                    {
                                        code = string.Empty;
                                    }
                                    else
                                    {
                                        if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                        {
                                            code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                        }
                                        else
                                        {
                                            code = string.Empty;
                                        }
                                    }

                                    break;
                                case SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu:   // 荷積業者別
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("NIZUMI_GYOUSHA_CD");
                                    if (index == -1)
                                    {
                                        code = string.Empty;
                                    }
                                    else
                                    {
                                        if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                        {
                                            code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                        }
                                        else
                                        {
                                            code = string.Empty;
                                        }
                                    }

                                    break;
                                case SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu:    // 荷積現場別
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("NIZUMI_GENBA_CD");
                                    if (index == -1)
                                    {
                                        code = string.Empty;
                                    }
                                    else
                                    {
                                        if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                        {
                                            code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                        }
                                        else
                                        {
                                            code = string.Empty;
                                        }
                                    }

                                    break;
                                case SYUKEUKOMOKU_TYPE.UnpanGyoshaBetsu:    // 運搬業者別
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("UNPAN_GYOUSHA_CD");
                                    if (index == -1)
                                    {
                                        code = string.Empty;
                                    }
                                    else
                                    {
                                        if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                        {
                                            code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                        }
                                        else
                                        {
                                            code = string.Empty;
                                        }
                                    }

                                    break;
                                default:
                                    index = this.InputDataTable[indexTable].Columns.IndexOf(syuukeiKoumoku.FieldCD);
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        code = string.Empty;
                                    }

                                    break;
                            }
                        }

                        if (code.GetType() == typeof(string))
                        {
                            code = ((string)code).Replace(" ", string.Empty);

                            if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.DensyuKubunBetsu)
                            {   // 伝種区分別
                                switch (indexTable)
                                {
                                    case 0: // 収集
                                        codeData = "収集";

                                        break;
                                    case 1: // 出荷
                                        codeData = "出荷";

                                        break;
                                    case 2: // 持込
                                        codeData = "持込";

                                        break;
                                }
                            }
                            else
                            {
                                // コード名称
                                if ((string)code != string.Empty)
                                {
                                    DataTable dataTableTmp;

                                    if (syuukeiKoumoku.FieldCD.Equals("GYOUSHA_CD") || syuukeiKoumoku.FieldCD.Equals("NIOROSHI_GYOUSHA_CD") || syuukeiKoumoku.FieldCD.Equals("NIZUMI_GYOUSHA_CD"))
                                    {
                                        // 現場名称の取得に備え、現場に紐づく業者CDを取得しておく
                                        gyoushaCd = code.ToString();
                                        gyoushaFieleName = syuukeiKoumoku.FieldCD;
                                    }

                                    if (syuukeiKoumoku.FieldCD.Equals("UNTENSHA_CD"))
                                    {
                                        sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}'", tableName, fieldName, tableName, "M_SHAIN", "SHAIN_CD", code);
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
                                    else if (syuukeiKoumoku.FieldCD.Equals("GENBA_CD") || syuukeiKoumoku.FieldCD.Equals("NIOROSHI_GENBA_CD") || syuukeiKoumoku.FieldCD.Equals("NIZUMI_GENBA_CD"))
                                    {
                                        // 現場名称を取得
                                        sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}' AND {6}.{7} = '{8}'"
                                            , tableName, fieldName, tableName, tableName, syuukeiKoumoku.FieldCD, code, tableName, gyoushaFieleName, gyoushaCd);
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
                                        sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}'", tableName, fieldName, tableName, tableName, syuukeiKoumoku.FieldCD, code);
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
                                else
                                {
                                    codeData = string.Empty;
                                }
                            }
                        }
                        else
                        {
                            if (!this.IsDBNull(code) && !code.Equals(string.Empty))
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
                            else
                            {
                                codeData = string.Empty;
                            }
                        }

                        syuukeiKoumokuNextN[itemColumnIndex] = code.ToString();

                        if (!syuukeiKoumokuPrevN[itemColumnIndex].Equals(syuukeiKoumokuNextN[itemColumnIndex]))
                        {   // 集計項目Nが変化した
                            if (!syuukeiKoumokuPrevN[itemColumnIndex].Equals("INITIALIZE VALUE"))
                            {
                                syuukeiKoumokuKingakuKei[itemColumnIndex] = 0;
                                syuukeiKoumokuShouhizei[itemColumnIndex] = 0;
                                syuukeiKoumokuGoukeiTotal[itemColumnIndex] = 0;
                            }

                            syuukeiKoumokuPrevN[itemColumnIndex] = syuukeiKoumokuNextN[itemColumnIndex];
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

                    if (denpyouNoPrev != denpyouNoNext)
                    {   // 伝票番号が変化した
                        denpyouNoPrev = denpyouNoNext;

                        // 金額
                        index = this.InputDataTable[indexTable].Columns.IndexOf("KINGAKU_TOTAL");
                        kingakuTotal = this.IsDBNull(InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]) ? 0 : (decimal)InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                        // 消費税金額
                        index = this.InputDataTable[indexTable].Columns.IndexOf("SHOUHIZEI_TOTAL");
                        shouhizeiTotal = this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]) ? 0 : (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                        // 合計金額
                        index = this.InputDataTable[indexTable].Columns.IndexOf("GOUKEI_KINGAKU_TOTAL");
                        goukeiKingakuTotal = this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]) ? 0 : (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                        // souGoukeikingakuTotal += goukeiKingakuTotal;

                        for (itemColumnIndex = 0; itemColumnIndex < syuukeiKoumokuEnableGroupCount; itemColumnIndex++)
                        {
                            int itemIndex = this.SelectSyuukeiKoumokuList[itemColumnIndex];
                            syuukeiKoumoku = this.SyuukeiKomokuList[itemIndex];

                            if (syuukeiKoumoku.MasterTableID == WINDOW_ID.NONE)
                            {
                                continue;
                            }

                            syuukeiKoumokuKingakuKei[itemColumnIndex] += kingakuTotal;
                            syuukeiKoumokuShouhizei[itemColumnIndex] += shouhizeiTotal;
                            syuukeiKoumokuGoukeiTotal[itemColumnIndex] += goukeiKingakuTotal;
                        }

                        // 総合計
                        souKingakuTotal += kingakuTotal;                    // 金額
                        souShouhizeiTotal += shouhizeiTotal;                // 消費税
                        souGoukeikingakuTotal += goukeiKingakuTotal;        // 金額計
                    }

                    // 受付番号
                    dataRowNew["PHY_DENPYOU_NUMBER_FLB"] = denpyouNoNext;
                    indexTmp = this.DataTableUkewatashi.Columns.IndexOf("DENPYOU_NUMBER");
                    dataRowUkewatashi[indexTmp] = denpyouNoNext.ToString();

                    // 受付日付
                    index = this.InputDataTable[indexTable].Columns.IndexOf("UKETSUKE_DATE");
                    indexTmp = this.DataTableUkewatashi.Columns.IndexOf("DENPYOU_DATE");

                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                    {
                        tmp = ((DateTime)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]).ToString("yyyy/MM/dd");
                        dataRowNew["PHY_DENPYOU_DATE_FLB"] = tmp;
                        dataRowUkewatashi[indexTmp] = tmp;
                    }
                    else
                    {
                        dataRowNew["PHY_DENPYOU_DATE_FLB"] = string.Empty;
                        dataRowUkewatashi[indexTmp] = string.Empty;
                    }

                    #region - 帳票出力項目（伝票）用タイトルカラム -

                    bool isDefault = false;
                    itemColumnIndex = 0;
                    for (int i = 0; i < this.SelectChouhyouOutKoumokuDepyouList.Count; i++, itemColumnIndex++)
                    {
                        ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = this.SelectChouhyouOutKoumokuDepyouList[i];
                        ChouhyouOutKoumoku chouhyouOutKoumoku = chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList[indexTable];

                        if (chouhyouOutKoumoku == null)
                        {
                            continue;
                        }

                        // テーブル名取得
                        tableName = chouhyouOutKoumoku.TableName;

                        // テーブルの該当フィールド名取得
                        fieldName = chouhyouOutKoumoku.FieldName;

                        // コード
                        isDefault = false;
                        switch (fieldName)
                        {
                            case "HAISHA_SIJISHO_FLG":  // 配車指示書
                                index = this.InputDataTable[indexTable].Columns.IndexOf("HAISHA_SIJISHO_FLG");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                                    codeData = (bool)code ? "済" : "未";
                                }
                                else
                                {
                                    code = string.Empty;
                                    codeData = string.Empty;
                                }

                                break;

                            case "MAIL_SEND_FLG":  // メール送信
                                index = this.InputDataTable[indexTable].Columns.IndexOf("MAIL_SEND_FLG");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                                    codeData = (bool)code ? "済" : "未";
                                }
                                else
                                {
                                    code = string.Empty;
                                    codeData = string.Empty;
                                }
                                break;

                            case "TAIRYUU_KBN":
                                index = this.InputDataTable[indexTable].Columns.IndexOf("TAIRYUU_KBN");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                                    codeData = (bool)code ? "滞留登録" : "通常登録";
                                }
                                else
                                {
                                    code = string.Empty;
                                    codeData = string.Empty;
                                }

                                break;
                            case "TORIHIKISAKI_NAME":
                                index = this.InputDataTable[indexTable].Columns.IndexOf("TORIHIKISAKI_CD");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    code = string.Empty;
                                }

                                index = this.InputDataTable[indexTable].Columns.IndexOf("TORIHIKISAKI_NAME");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    codeData = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    codeData = string.Empty;
                                }

                                break;
                            case "GYOUSHA_NAME":
                                index = this.InputDataTable[indexTable].Columns.IndexOf("GYOUSHA_CD");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    code = string.Empty;
                                }

                                index = this.InputDataTable[indexTable].Columns.IndexOf("GYOUSHA_NAME");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    codeData = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    codeData = string.Empty;
                                }

                                break;
                            case "GENBA_NAME":
                                index = this.InputDataTable[indexTable].Columns.IndexOf("GENBA_CD");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    code = string.Empty;
                                }

                                index = this.InputDataTable[indexTable].Columns.IndexOf("GENBA_NAME");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    codeData = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    codeData = string.Empty;
                                }

                                break;
                            case "NIZUMI_GYOUSHA_NAME":
                                if (tableName.Equals("T_SHUKKA_ENTRY") || tableName.Equals("T_UR_SH_ENTRY"))
                                {
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("NIZUMI_GYOUSHA_CD");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        code = string.Empty;
                                    }

                                    index = this.InputDataTable[indexTable].Columns.IndexOf("NIZUMI_GYOUSHA_NAME");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        codeData = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        codeData = string.Empty;
                                    }
                                }

                                break;
                            case "NIZUMI_GENBA_NAME":
                                if (tableName.Equals("T_SHUKKA_ENTRY") || tableName.Equals("T_UR_SH_ENTRY"))
                                {
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("NIZUMI_GENBA_CD");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        code = string.Empty;
                                    }

                                    index = this.InputDataTable[indexTable].Columns.IndexOf("NIZUMI_GENBA_NAME");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        codeData = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        codeData = string.Empty;
                                    }
                                }

                                break;
                            case "NIOROSHI_GYOUSHA_NAME":
                                if (tableName.Equals("T_UKEIRE_ENTRY") || tableName.Equals("T_UR_SH_ENTRY"))
                                {
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("NIOROSHI_GYOUSHA_CD");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        code = string.Empty;
                                    }

                                    index = this.InputDataTable[indexTable].Columns.IndexOf("NIOROSHI_GYOUSHA_NAME");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        codeData = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        codeData = string.Empty;
                                    }
                                }

                                break;
                            case "NIOROSHI_GENBA_NAME":
                                if (tableName.Equals("T_UKEIRE_ENTRY") || tableName.Equals("T_UR_SH_ENTRY"))
                                {
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("NIOROSHI_GENBA_CD");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        code = string.Empty;
                                    }

                                    index = this.InputDataTable[indexTable].Columns.IndexOf("NIOROSHI_GENBA_NAME");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        codeData = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        codeData = string.Empty;
                                    }
                                }

                                break;
                            case "EIGYOU_TANTOUSHA_NAME":
                                index = this.InputDataTable[indexTable].Columns.IndexOf("EIGYOU_TANTOUSHA_CD");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    code = string.Empty;
                                }

                                index = this.InputDataTable[indexTable].Columns.IndexOf("EIGYOU_TANTOUSHA_NAME");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    codeData = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    codeData = string.Empty;
                                }

                                break;
                            case "NYUURYOKU_TANTOUSHA_NAME":
                                index = this.InputDataTable[indexTable].Columns.IndexOf("NYUURYOKU_TANTOUSHA_CD");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    code = string.Empty;
                                }

                                index = this.InputDataTable[indexTable].Columns.IndexOf("NYUURYOKU_TANTOUSHA_NAME");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    codeData = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    codeData = string.Empty;
                                }

                                break;
                            case "SHARYOU_NAME":
                                index = this.InputDataTable[indexTable].Columns.IndexOf("SHARYOU_CD");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    code = string.Empty;
                                }

                                index = this.InputDataTable[indexTable].Columns.IndexOf("SHARYOU_NAME");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    codeData = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    codeData = string.Empty;
                                }

                                break;
                            case "SHASHU_NAME":
                                index = this.InputDataTable[indexTable].Columns.IndexOf("SHASHU_CD");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    code = string.Empty;
                                }

                                index = this.InputDataTable[indexTable].Columns.IndexOf("SHASHU_NAME");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    codeData = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    codeData = string.Empty;
                                }

                                break;
                            case "UNPAN_GYOUSHA_NAME":
                                index = this.InputDataTable[indexTable].Columns.IndexOf("UNPAN_GYOUSHA_CD");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    code = string.Empty;
                                }

                                index = this.InputDataTable[indexTable].Columns.IndexOf("UNPAN_GYOUSHA_NAME");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    codeData = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    codeData = string.Empty;
                                }

                                break;
                            case "UNTENSHA_NAME":
                                index = this.InputDataTable[indexTable].Columns.IndexOf("UNTENSHA_CD");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    code = string.Empty;
                                }

                                index = this.InputDataTable[indexTable].Columns.IndexOf("UNTENSHA_NAME");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    codeData = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    codeData = string.Empty;
                                }

                                break;
                            case "HINMEI_NAME":
                                index = this.InputDataTable[indexTable].Columns.IndexOf("HINMEI_CD");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    code = string.Empty;
                                }

                                index = this.InputDataTable[indexTable].Columns.IndexOf("HINMEI_NAME");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    codeData = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    codeData = string.Empty;
                                }

                                break;
                            case "KENSHU_DATE":
                                if (indexTable == 0)
                                {   // 受入
                                    code = string.Empty;
                                    codeData = string.Empty;
                                }
                                else if (indexTable == 1)
                                {   // 支払
                                    isDefault = true;
                                }
                                break;
                            case "SHUKKA_NET_TOTAL":
                                if (indexTable == 0)
                                {   // 受入
                                    code = string.Empty;
                                    codeData = string.Empty;
                                }
                                else if (indexTable == 1)
                                {   // 支払
                                    isDefault = true;
                                }
                                break;
                            default:
                                isDefault = true;

                                break;
                        }

                        Type type;
                        if (isDefault)
                        {
                            index = this.InputDataTable[indexTable].Columns.IndexOf(chouhyouOutKoumoku.FieldName);
                            if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                            {
                                code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                            }
                            else
                            {
                                code = string.Empty;
                            }

                            type = code.GetType();

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
                            else if (type == typeof(DBNull))
                            {
                                code = string.Empty;
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
                                else if (type == typeof(DBNull))
                                {
                                    codeData = string.Empty;
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
                                        else if (chouhyouOutKoumoku.FieldName.Equals("MANIFEST_SHURUI_CD"))
                                        {
                                            tableNameRef = "M_MANIFEST_SHURUI";
                                            fieldNameRyaku = "MANIFEST_SHURUI_NAME_RYAKU";
                                            fieldNameRef = "MANIFEST_SHURUI_CD";

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
                                        else if (chouhyouOutKoumoku.FieldName.Equals("MANIFEST_TEHAI_CD"))
                                        {
                                            tableNameRef = "M_MANIFEST_TEHAI";
                                            fieldNameRyaku = "MANIFEST_TEHAI_NAME_RYAKU";
                                            fieldNameRef = "MANIFEST_TEHAI_CD";

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
                                        else if (chouhyouOutKoumoku.FieldName.Equals("CONTENA_SOUSA_CD"))
                                        {
                                            tableNameRef = "M_CONTENA_SOUSA";
                                            fieldNameRyaku = "CONTENA_SOUSA_NAME_RYAKU";
                                            fieldNameRef = "CONTENA_SOUSA_CD";

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
                                        else if (chouhyouOutKoumoku.FieldName.Equals("COURSE_NAME_CD"))
                                        {
                                            tableNameRef = "M_COURSE_NAME";
                                            fieldNameRyaku = "COURSE_NAME_RYAKU";
                                            fieldNameRef = "COURSE_NAME_CD";

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
                                            if (fieldNameRef.Equals("URIAGE_ZEI_KEISAN_KBN_CD") || fieldNameRef.Equals("SHIHARAI_ZEI_KEISAN_KBN_CD"))
                                            {
                                                switch ((short)code)
                                                {
                                                    case 1:
                                                        codeData = "伝票毎";
                                                        break;
                                                    case 2:
                                                        codeData = "請求毎";
                                                        break;
                                                    case 3:
                                                        codeData = "明細毎";
                                                        break;
                                                    default:
                                                        codeData = string.Empty;
                                                        break;
                                                }
                                            }
                                            else if (fieldNameRef.Equals("URIAGE_ZEI_KBN_CD") || fieldNameRef.Equals("SHIHARAI_ZEI_KBN_CD"))
                                            {
                                                switch ((short)code)
                                                {
                                                    case 1:
                                                        codeData = "外税";
                                                        break;
                                                    case 2:
                                                        codeData = "内税";
                                                        break;
                                                    case 3:
                                                        codeData = "非課税";
                                                        break;
                                                    default:
                                                        codeData = string.Empty;
                                                        break;
                                                }
                                            }
                                            else if (fieldNameRef.Equals("URIAGE_TORIHIKI_KBN_CD") || fieldNameRef.Equals("SHIHARAI_TORIHIKI_KBN_CD"))
                                            {
                                                // マスターにデーターあり(M_TORIHIKI_KBN)
                                                switch ((short)code)
                                                {
                                                    case 1:
                                                        codeData = "1";
                                                        break;
                                                    case 2:
                                                        codeData = "2";
                                                        break;
                                                    default:
                                                        codeData = string.Empty;
                                                        break;
                                                }
                                            }
                                            else if (chouhyouOutKoumoku.FieldName.Equals("HAISHA_SIJISHO_FLG") || chouhyouOutKoumoku.FieldName.Equals("MAIL_SEND_FLG"))
                                            {
                                                switch ((short)code)
                                                {
                                                    case 0:
                                                        codeData = "未";
                                                        break;
                                                    case 1:
                                                        codeData = "済";
                                                        break;
                                                    default:
                                                        codeData = string.Empty;
                                                        break;
                                                }
                                            }
                                            else if (chouhyouOutKoumoku.FieldName.Equals("COURSE_KUMIKOMI_CD"))
                                            {
                                                switch ((short)code)
                                                {
                                                    case 1:
                                                        codeData = "臨時";
                                                        break;
                                                    case 2:
                                                        codeData = "組込";
                                                        break;
                                                    default:
                                                        codeData = string.Empty;
                                                        break;
                                                }
                                            }
                                            else
                                            {
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
                                        }
                                    }
                                    else
                                    {
                                        if (chouhyouOutKoumoku.FieldName.Equals("DENPYOU_BIKOU") || chouhyouOutKoumoku.FieldName.Equals("TAIRYUU_BIKOU"))
                                        {
                                            codeData = code;
                                        }
                                        else
                                        {
                                            sql = string.Format("SELECT {0}.{1} FROM {2} WHERE {3}.{4} = '{5}'", tableName, fieldName, tableName, tableName, fieldName, code);

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

                            type = codeData.GetType();

                            if (type == typeof(bool))
                            {   // 論理型
                                codeData = (bool)codeData ? 1 : 0;
                            }

                            if (!chouhyouOutKoumoku.FieldName.Contains("CD"))
                            {   // フィールド名にCD（コード）が含まれていない
                                code = string.Empty;
                            }
                        }
                        else
                        {
                            type = code.GetType();
                            if (type == typeof(bool))
                            {   // 論理型
                                code = (bool)code ? 1 : 0;
                            }
                        }

                        if (codeData.GetType() == typeof(decimal))
                        {   // 金額系
                            string format = chouhyouOutKoumoku.OutputFormat;
                            decimal dataTmp = (decimal)codeData;
                            codeData = (object)dataTmp;//(format);
                            //ThangNguyen Add 20150706 Start
                            if (codeData.ToString() != null && codeData.ToString() != "")
                            {
                                int valueKingaku = int.Parse(codeData.ToString().Substring(codeData.ToString().LastIndexOf(".") + 1));
                                if (valueKingaku == 0)
                                {
                                    codeData = dataTmp.ToString("#,##0");
                                }
                                else
                                {
                                    codeData = dataTmp.ToString("#,#.####");
                                }
                            }
                            //ThangNguyen Add 20150706 End
                        }
                        else if (codeData.GetType() == typeof(decimal))
                        {   // 重量系
                            string format = chouhyouOutKoumoku.OutputFormat;
                            decimal dataTmp = (decimal)codeData;
                            codeData = dataTmp.ToString(format);
                        }

                        switch (itemColumnIndex)
                        {
                            case 0: // 帳票出力可能項目１番目
                                dataRowNew["PHN_KAHEN5_1_VLB"] = code;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_1_CD");
                                dataRowUkewatashi[indexTmp] = code;

                                dataRowNew["PHY_KAHEN2_1_VLB"] = codeData;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_1_VALUE");
                                dataRowUkewatashi[indexTmp] = codeData;

                                break;
                            case 1: // 帳票出力可能項目２番目
                                dataRowNew["PHN_KAHEN5_2_VLB"] = code;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_2_CD");
                                dataRowUkewatashi[indexTmp] = code;

                                dataRowNew["PHY_KAHEN2_2_VLB"] = codeData;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_2_VALUE");
                                dataRowUkewatashi[indexTmp] = codeData;

                                break;
                            case 2: // 帳票出力可能項目３番目
                                dataRowNew["PHN_KAHEN5_3_VLB"] = code;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_3_CD");
                                dataRowUkewatashi[indexTmp] = code;

                                dataRowNew["PHY_KAHEN2_3_VLB"] = codeData;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_3_VALUE");
                                dataRowUkewatashi[indexTmp] = codeData;

                                break;
                            case 3: // 帳票出力可能項目４番目
                                dataRowNew["PHN_KAHEN5_4_VLB"] = code;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_4_CD");
                                dataRowUkewatashi[indexTmp] = code;

                                dataRowNew["PHY_KAHEN2_4_VLB"] = codeData;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_4_VALUE");
                                dataRowUkewatashi[indexTmp] = codeData;

                                break;
                            case 4: // 帳票出力可能項目５番目
                                dataRowNew["PHN_KAHEN5_5_VLB"] = code;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_5_CD");
                                dataRowUkewatashi[indexTmp] = code;

                                dataRowNew["PHY_KAHEN2_5_VLB"] = codeData;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_5_VALUE");
                                dataRowUkewatashi[indexTmp] = codeData;

                                break;
                            case 5: // 帳票出力可能項目６番目
                                dataRowNew["PHN_KAHEN5_6_VLB"] = code;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_6_CD");
                                dataRowUkewatashi[indexTmp] = code;

                                dataRowNew["PHY_KAHEN2_6_VLB"] = codeData;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_6_VALUE");
                                dataRowUkewatashi[indexTmp] = codeData;

                                break;
                            case 6: // 帳票出力可能項目７番目
                                dataRowNew["PHN_KAHEN5_7_VLB"] = code;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_7_CD");
                                dataRowUkewatashi[indexTmp] = code;

                                dataRowNew["PHY_KAHEN2_7_VLB"] = codeData;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_7_VALUE");
                                dataRowUkewatashi[indexTmp] = codeData;

                                break;
                            case 7: // 帳票出力可能項目８番目
                                dataRowNew["PHN_KAHEN5_8_VLB"] = code;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_8_CD");
                                dataRowUkewatashi[indexTmp] = code;

                                dataRowNew["PHY_KAHEN2_8_VLB"] = codeData;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_DENPYOU_8_VALUE");
                                dataRowUkewatashi[indexTmp] = codeData;

                                break;
                        }
                    }

                    #endregion - 帳票出力項目（伝票）用タイトルカラム -

                    // 明細行番
                    index = this.InputDataTable[indexTable].Columns.IndexOf("ROW_NO");
                    indexTmp = this.DataTableUkewatashi.Columns.IndexOf("ROW_NO");

                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                    {
                        tmp = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index].ToString();
                        dataRowNew["PHY_ROW_NO_FLB"] = tmp;
                        dataRowUkewatashi[indexTmp] = tmp;
                    }
                    else
                    {
                        dataRowNew["PHY_ROW_NO_FLB"] = string.Empty;
                        dataRowUkewatashi[indexTmp] = string.Empty;
                    }

                    #region - 帳票出力項目（明細）用タイトルカラム -

                    itemColumnIndex = 0;
                    for (int i = 0; i < this.SelectChouhyouOutKoumokuMeisaiList.Count; i++, itemColumnIndex++)
                    {
                        DataTable dataTableTmp;
                        ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = this.SelectChouhyouOutKoumokuMeisaiList[i];
                        ChouhyouOutKoumoku chouhyouOutKoumoku = chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList[indexTable];

                        if (chouhyouOutKoumoku == null)
                        {
                            continue;
                        }

                        // テーブル名取得
                        tableName = chouhyouOutKoumoku.TableName;

                        // テーブルの該当フィールド名取得
                        fieldName = chouhyouOutKoumoku.FieldName;

                        isDefault = false;
                        switch (fieldName)
                        {
                            case "SHURUI_NAME":
                                index = this.InputDataTable[indexTable].Columns.IndexOf("SHURUI_CD");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                    tmp = (string)code;
                                    if (!tmp.Equals(string.Empty))
                                    {
                                        sql = string.Format("SELECT M_SHURUI.SHURUI_NAME_RYAKU FROM M_SHURUI WHERE M_SHURUI.SHURUI_CD = {0}", code);

                                        dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                                        index = dataTableTmp.Columns.IndexOf("SHURUI_NAME_RYAKU");
                                        codeData = (string)dataTableTmp.Rows[0].ItemArray[index];
                                    }
                                    else
                                    {
                                        code = string.Empty;
                                        codeData = string.Empty;
                                    }
                                }
                                else
                                {
                                    code = string.Empty;
                                    codeData = string.Empty;
                                }

                                break;
                            case "BUNRUI_NAME":
                                index = this.InputDataTable[indexTable].Columns.IndexOf("BUNRUI_CD");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                                    tmp = (string)code;
                                    if (!tmp.Equals(string.Empty))
                                    {
                                        sql = string.Format("SELECT M_BUNRUI.BUNRUI_NAME_RYAKU FROM M_BUNRUI WHERE M_BUNRUI.BUNRUI_CD = {0}", code);

                                        dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                                        index = dataTableTmp.Columns.IndexOf("BUNRUI_NAME_RYAKU");
                                        codeData = (string)dataTableTmp.Rows[0].ItemArray[index];
                                    }
                                    else
                                    {
                                        code = string.Empty;
                                        codeData = string.Empty;
                                    }
                                }
                                else
                                {
                                    code = string.Empty;
                                    codeData = string.Empty;
                                }

                                break;
                            case "HINMEI_NAME":
                                index = this.InputDataTable[indexTable].Columns.IndexOf("HINMEI_CD");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    // 品名コード
                                    code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                                    index = this.InputDataTable[indexTable].Columns.IndexOf("HINMEI_NAME");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        // 品名
                                        codeData = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        codeData = string.Empty;
                                    }
                                }
                                else
                                {
                                    code = string.Empty;
                                    codeData = string.Empty;
                                }

                                break;
                            case "STACK_JYUURYOU":
                            case "EMPTY_JYUURYOU":
                            case "WARIFURI_JYUURYOU":
                            case "WARIFURI_PERCENT":
                            case "CHOUSEI_JYUURYOU":
                            case "CHOUSEI_PERCENT":
                            case "YOUKI_CD":
                            case "YOUKI_SUURYOU":
                            case "YOUKI_JYUURYOU":
                                if (indexTable == 2)
                                {
                                    code = string.Empty;
                                    codeData = string.Empty;
                                }
                                else
                                {
                                    isDefault = true;
                                }

                                break;

                            default:
                                isDefault = true;

                                break;
                        }

                        // コード
                        if (isDefault)
                        {
                            index = this.InputDataTable[indexTable].Columns.IndexOf(chouhyouOutKoumoku.FieldName);
                            if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                            {
                                code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
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

                                        if (fieldNameRef.Equals("YOUKI_CD"))
                                        {
                                            tmp = (string)code;
                                            if (!tmp.Equals(string.Empty))
                                            {
                                                sql = string.Format("SELECT M_YOUKI.YOUKI_NAME_RYAKU FROM M_YOUKI WHERE M_YOUKI.YOUKI_CD = {0}", code);

                                                dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                                                index = dataTableTmp.Columns.IndexOf("YOUKI_NAME_RYAKU");
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
                                        else if (fieldNameRef.Equals("UNIT_CD"))
                                        {
                                            sql = string.Format("SELECT M_UNIT.UNIT_NAME_RYAKU FROM M_UNIT WHERE M_UNIT.UNIT_CD = {0}", code);

                                            dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                                            index = dataTableTmp.Columns.IndexOf("UNIT_NAME_RYAKU");
                                            if (dataTableTmp.Rows.Count != 0)
                                            {
                                                codeData = dataTableTmp.Rows[0].ItemArray[index];
                                            }
                                            else
                                            {
                                                codeData = string.Empty;
                                            }
                                        }
                                        else if (fieldNameRef.Equals("HINMEI_ZEI_KBN_CD"))
                                        {
                                            switch ((short)code)
                                            {
                                                case 1: // 外税
                                                    codeData = "外税";

                                                    break;
                                                case 2: // 内税
                                                    codeData = "内税";

                                                    break;
                                                case 3: // 非課税
                                                    codeData = "非課税";

                                                    break;
                                                default:
                                                    codeData = string.Empty;

                                                    break;
                                            }
                                        }
                                        else if (fieldNameRef.Equals("NISUGATA_UNIT_CD"))
                                        {
                                            sql = string.Format("SELECT M_NISUGATA.NISUGATA_NAME_RYAKU FROM M_NISUGATA WHERE M_NISUGATA.NISUGATA_CD = {0}", code);

                                            dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                                            index = dataTableTmp.Columns.IndexOf("NISUGATA_NAME_RYAKU");
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
                                    }
                                    else
                                    {
                                        if (type == typeof(string))
                                        {   // 文字列型
                                            tmp = (string)code;
                                            if (!tmp.Equals(string.Empty))
                                            {
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
                                                codeData = string.Empty;
                                            }
                                        }
                                        else
                                        {
                                            if (!this.IsDBNull(code))
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
                        }

                        if (codeData.GetType() == typeof(decimal))
                        {   // 金額系
                            string format = chouhyouOutKoumoku.OutputFormat;
                            decimal dataTmp = (decimal)codeData;
                            codeData = dataTmp.ToString();

                            //ThangNguyen Add 20150706 Start
                            if (codeData.ToString() != null && codeData.ToString() != "")
                            {
                                int valueKingaku = int.Parse(codeData.ToString().Substring(codeData.ToString().LastIndexOf(".") + 1));
                                if (valueKingaku == 0)
                                {
                                    codeData = dataTmp.ToString("#,##0");
                                }
                                else
                                {
                                    codeData = dataTmp.ToString("#,#.####");
                                }
                            }
                            //ThangNguyen Add 20150706 End
                        }
                        else if (codeData.GetType() == typeof(decimal))
                        {   // 重量系
                            string format = chouhyouOutKoumoku.OutputFormat;

                            format = format.Replace(" ", string.Empty);

                            decimal dataTmp = (decimal)codeData;
                            if (!format.Equals(string.Empty))
                            {
                                codeData = dataTmp.ToString(format);
                            }
                            else
                            {
                                codeData = dataTmp.ToString("#,##0");
                            }
                        }

                        switch (itemColumnIndex)
                        {
                            case 0: // 帳票出力可能項目１番目
                                dataRowNew["PHN_KAHEN5_9_VLB"] = code;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_1_CD");
                                dataRowUkewatashi[indexTmp] = code;

                                dataRowNew["PHY_KAHEN3_1_VLB"] = codeData;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_1_VALUE");
                                dataRowUkewatashi[indexTmp] = codeData;

                                break;
                            case 1: // 帳票出力可能項目２番目
                                dataRowNew["PHN_KAHEN5_10_VLB"] = code;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_2_CD");
                                dataRowUkewatashi[indexTmp] = code;

                                dataRowNew["PHY_KAHEN3_2_VLB"] = codeData;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_2_VALUE");
                                dataRowUkewatashi[indexTmp] = codeData;

                                break;
                            case 2: // 帳票出力可能項目３番目
                                dataRowNew["PHN_KAHEN5_11_VLB"] = code;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_3_CD");
                                dataRowUkewatashi[indexTmp] = code;

                                dataRowNew["PHY_KAHEN3_3_VLB"] = codeData;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_3_VALUE");
                                dataRowUkewatashi[indexTmp] = codeData;

                                break;
                            case 3: // 帳票出力可能項目４番目
                                dataRowNew["PHN_KAHEN5_12_VLB"] = code;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_4_CD");
                                dataRowUkewatashi[indexTmp] = code;

                                dataRowNew["PHY_KAHEN3_4_VLB"] = codeData;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_4_VALUE");
                                dataRowUkewatashi[indexTmp] = codeData;

                                break;
                            case 4: // 帳票出力可能項目５番目
                                dataRowNew["PHN_KAHEN5_13_VLB"] = code;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_5_CD");
                                dataRowUkewatashi[indexTmp] = code;

                                dataRowNew["PHY_KAHEN3_5_VLB"] = codeData;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_5_VALUE");
                                dataRowUkewatashi[indexTmp] = codeData;

                                break;
                            case 5: // 帳票出力可能項目６番目
                                dataRowNew["PHN_KAHEN5_14_VLB"] = code;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_6_CD");
                                dataRowUkewatashi[indexTmp] = code;

                                dataRowNew["PHY_KAHEN3_6_VLB"] = codeData;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_6_VALUE");
                                dataRowUkewatashi[indexTmp] = codeData;

                                break;
                            case 6: // 帳票出力可能項目７番目
                                dataRowNew["PHN_KAHEN5_15_VLB"] = code;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_7_CD");
                                dataRowUkewatashi[indexTmp] = code;

                                dataRowNew["PHY_KAHEN3_7_VLB"] = codeData;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_7_VALUE");
                                dataRowUkewatashi[indexTmp] = codeData;

                                break;
                            case 7: // 帳票出力可能項目８番目
                                dataRowNew["PHN_KAHEN5_16_VLB"] = code;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_8_CD");
                                dataRowUkewatashi[indexTmp] = code;

                                dataRowNew["PHY_KAHEN3_8_VLB"] = codeData;
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_MEISAI_8_VALUE");
                                dataRowUkewatashi[indexTmp] = codeData;

                                break;
                        }
                    }

                    #endregion - 帳票出力項目（明細）用タイトルカラム -

                    // 合計
                    dataRowNew["PHY_KINGAKU_TOTAL_FLB"] = kingakuTotal.ToString("#,##0");
                    indexTmp = this.DataTableUkewatashi.Columns.IndexOf("DENPYOU_KINGAKU_TOTAL");
                    dataRowUkewatashi[indexTmp] = kingakuTotal.ToString("#,##0");

                    dataRowNew["PHY_TAX_TOTAL_FLB"] = shouhizeiTotal.ToString("#,##0");
                    indexTmp = this.DataTableUkewatashi.Columns.IndexOf("DENPYOU_TAX_TOTAL");
                    dataRowUkewatashi[indexTmp] = shouhizeiTotal.ToString("#,##0");

                    dataRowNew["PHY_TOTAL_FLB"] = goukeiKingakuTotal.ToString("#,##0");
                    indexTmp = this.DataTableUkewatashi.Columns.IndexOf("DENPYOU_TOTAL");
                    dataRowUkewatashi[indexTmp] = goukeiKingakuTotal.ToString("#,##0");

                    // 総合計
                    //dataRowNew["PHN_GOUKEI_KINGAKU_TOTAL_FLB"] = syuukeiKoumokuKingakuKei[0].ToString("#,##0");
                    dataRowNew["PHN_GOUKEI_KINGAKU_TOTAL_FLB"] = souKingakuTotal.ToString("#,##0");
                    indexTmp = this.DataTableUkewatashi.Columns.IndexOf("ALL_KINGAKU_TOTAL");
                    //dataRowUkewatashi[indexTmp] = syuukeiKoumokuKingakuKei[0].ToString("#,##0");
                    dataRowUkewatashi[indexTmp] = souKingakuTotal.ToString("#,##0");

                    //dataRowNew["PHN_GOUKEI_TAX_TOTAL_FLB"] = syuukeiKoumokuShouhizei[0].ToString("#,##0");
                    dataRowNew["PHN_GOUKEI_TAX_TOTAL_FLB"] = souShouhizeiTotal.ToString("#,##0");
                    indexTmp = this.DataTableUkewatashi.Columns.IndexOf("ALL_TAX_TOTAL");
                    //dataRowUkewatashi[indexTmp] = syuukeiKoumokuShouhizei[0].ToString("#,##0");
                    dataRowUkewatashi[indexTmp] = souShouhizeiTotal.ToString("#,##0");

                    //dataRowNew["PHN_GOUKEI_TOTAL_FLB"] = syuukeiKoumokuGoukeiTotal[0].ToString("#,##0");
                    dataRowNew["PHN_GOUKEI_TOTAL_FLB"] = souGoukeikingakuTotal.ToString("#,##0");
                    //indexTmp = this.DataTableUkewatashi.Columns.IndexOf("ALL_TOTAL");
                    //dataRowUkewatashi[indexTmp] = syuukeiKoumokuGoukeiTotal[0].ToString("#,##0");
                    indexTmp = this.DataTableUkewatashi.Columns.IndexOf("ALL_TOTAL_1");
                    //dataRowUkewatashi[indexTmp] = syuukeiKoumokuGoukeiTotal[0].ToString("#,##0");
                    dataRowUkewatashi[indexTmp] = souGoukeikingakuTotal.ToString("#,##0");

                    for (itemColumnIndex = 0; itemColumnIndex < syuukeiKoumokuEnableGroupCount; itemColumnIndex++)
                    {
                        switch (itemColumnIndex)
                        {
                            case 0: // 集計項目１
                                dataRowNew["PHN_FILL_COND_ID_1_KAHEN1_1_VLB"] = syuukeiKoumokuCode[itemColumnIndex];
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_1_TOTAL_CD");
                                dataRowUkewatashi[indexTmp] = syuukeiKoumokuCode[itemColumnIndex];

                                dataRowNew["PHN_FILL_COND_ID_1_KAHEN1_2_VLB"] = syuukeiKoumokuCodeName[itemColumnIndex];
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_1_TOTAL_NAME");
                                dataRowUkewatashi[indexTmp] = syuukeiKoumokuCodeName[itemColumnIndex];

                                dataRowNew["PHN_FILL_COND_ID_1_KINGAKU_TOTAL_FLB"] = syuukeiKoumokuKingakuKei[itemColumnIndex].ToString("#,##0");
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_1_KINGAKU_TOTAL");
                                dataRowUkewatashi[indexTmp] = syuukeiKoumokuKingakuKei[itemColumnIndex].ToString("#,##0");

                                dataRowNew["PHN_FILL_COND_ID_1_TAX_TOTAL_FLB"] = syuukeiKoumokuShouhizei[itemColumnIndex].ToString("#,##0");
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_1_TAX_TOTAL");
                                dataRowUkewatashi[indexTmp] = syuukeiKoumokuShouhizei[itemColumnIndex].ToString("#,##0");

                                dataRowNew["PHN_FILL_COND_ID_1_TOTAL_FLB"] = syuukeiKoumokuGoukeiTotal[itemColumnIndex].ToString("#,##0");
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_1_TOTAL");
                                dataRowUkewatashi[indexTmp] = syuukeiKoumokuGoukeiTotal[itemColumnIndex].ToString("#,##0");

                                break;
                            case 1: // 集計項目２
                                dataRowNew["PHN_FILL_COND_ID_2_KAHEN1_3_VLB"] = syuukeiKoumokuCode[itemColumnIndex];
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_2_TOTAL_CD");
                                dataRowUkewatashi[indexTmp] = syuukeiKoumokuCode[itemColumnIndex];

                                dataRowNew["PHN_FILL_COND_ID_2_KAHEN1_4_VLB"] = syuukeiKoumokuCodeName[itemColumnIndex];
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_2_TOTAL_NAME");
                                dataRowUkewatashi[indexTmp] = syuukeiKoumokuCodeName[itemColumnIndex];

                                dataRowNew["PHN_FILL_COND_ID_2_KINGAKU_TOTAL_FLB"] = syuukeiKoumokuKingakuKei[itemColumnIndex].ToString("#,##0");
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_2_KINGAKU_TOTAL");
                                dataRowUkewatashi[indexTmp] = syuukeiKoumokuKingakuKei[itemColumnIndex].ToString("#,##0");

                                dataRowNew["PHN_FILL_COND_ID_2_TAX_TOTAL_FLB"] = syuukeiKoumokuShouhizei[itemColumnIndex].ToString("#,##0");
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_2_TAX_TOTAL");
                                dataRowUkewatashi[indexTmp] = syuukeiKoumokuShouhizei[itemColumnIndex].ToString("#,##0");

                                dataRowNew["PHN_FILL_COND_ID_2_TOTAL_FLB"] = syuukeiKoumokuGoukeiTotal[itemColumnIndex].ToString("#,##0");
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_2_TOTAL");
                                dataRowUkewatashi[indexTmp] = syuukeiKoumokuGoukeiTotal[itemColumnIndex].ToString("#,##0");

                                break;
                            case 2: // 集計項目３
                                dataRowNew["PHN_FILL_COND_ID_3_KAHEN1_5_VLB"] = syuukeiKoumokuCode[itemColumnIndex];
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_3_TOTAL_CD");
                                dataRowUkewatashi[indexTmp] = syuukeiKoumokuCode[itemColumnIndex];

                                dataRowNew["PHN_FILL_COND_ID_3_KAHEN1_6_VLB"] = syuukeiKoumokuCodeName[itemColumnIndex];
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_3_TOTAL_NAME");
                                dataRowUkewatashi[indexTmp] = syuukeiKoumokuCodeName[itemColumnIndex];

                                dataRowNew["PHN_FILL_COND_ID_3_KINGAKU_TOTAL_FLB"] = syuukeiKoumokuKingakuKei[itemColumnIndex].ToString("#,##0");
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_3_KINGAKU_TOTAL");
                                dataRowUkewatashi[indexTmp] = syuukeiKoumokuKingakuKei[itemColumnIndex].ToString("#,##0");

                                dataRowNew["PHN_FILL_COND_ID_3_TAX_TOTAL_FLB"] = syuukeiKoumokuShouhizei[itemColumnIndex].ToString("#,##0");
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_3_TAX_TOTAL");
                                dataRowUkewatashi[indexTmp] = syuukeiKoumokuShouhizei[itemColumnIndex].ToString("#,##0");

                                dataRowNew["PHN_FILL_COND_ID_3_TOTAL_FLB"] = syuukeiKoumokuGoukeiTotal[itemColumnIndex].ToString("#,##0");
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_3_TOTAL");
                                dataRowUkewatashi[indexTmp] = syuukeiKoumokuGoukeiTotal[itemColumnIndex].ToString("#,##0");

                                break;
                            case 3: // 集計項目４
                                //dataRowNew["PHN_FILL_COND_ID_4_KAHEN1_5_VLB"] = syuukeiKoumokuCode[itemColumnIndex];
                                //indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_4_TOTAL_CD");
                                //dataRowUkewatashi[indexTmp] = syuukeiKoumokuCode[itemColumnIndex];
                                //dataRowNew["PHN_FILL_COND_ID_4_KAHEN1_6_VLB"] = syuukeiKoumokuCodeName[itemColumnIndex];
                                //indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_4_TOTAL_NAME");
                                //dataRowUkewatashi[indexTmp] = syuukeiKoumokuCodeName[itemColumnIndex];
                                //dataRowNew["PHN_FILL_COND_ID_4_KINGAKU_TOTAL_FLB"] = syuukeiKoumokuKingakuKei[itemColumnIndex];
                                //indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_4_KINGAKU_TOTAL");
                                //dataRowUkewatashi[indexTmp] = syuukeiKoumokuKingakuKei[itemColumnIndex].ToString();
                                //dataRowNew["PHN_FILL_COND_ID_4_TAX_TOTAL_FLB"] = syuukeiKoumokuShouhizei[itemColumnIndex];
                                //indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_4_TAX_TOTAL");
                                //dataRowUkewatashi[indexTmp] = syuukeiKoumokuShouhizei[itemColumnIndex].ToString();
                                //dataRowNew["PHN_FILL_COND_ID_4_TOTAL_FLB"] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];
                                //indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_ID_4_TOTAL");
                                //dataRowUkewatashi[indexTmp] = syuukeiKoumokuGoukeiTotal[itemColumnIndex].ToString();

                                break;
                        }
                    }

                    this.ChouhyouDataTable.Rows.Add(dataRowNew);
                    this.DataTableUkewatashi.Rows.Add(dataRowUkewatashi);
                }
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        #endregion - Methods -
    }

    #endregion - CommonChouhyouR342 -

    #endregion - Classes -
}
