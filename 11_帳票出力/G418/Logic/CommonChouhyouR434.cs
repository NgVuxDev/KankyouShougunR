using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Utility;

namespace Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup
{
    #region - Classes -

    #region - CommonChouhyouR434 -

    /// <summary>R434(前年対比表)を表すクラス・コントロール</summary>
    public class CommonChouhyouR434 : CommonChouhyouBase
    {
        #region - Fields -

        /// <summary>データーテーブル（今年・去年）を保持するフィールド</summary>
        private List<DataTable[]> dataTableInfo = new List<DataTable[]>();

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="CommonChouhyouR434"/> class.</summary>
        /// <param name="windowID">画面ＩＤ</param>
        public CommonChouhyouR434(WINDOW_ID windowID)
            : base(windowID)
        {
            // 帳票出力フルパスフォーム名
            this.OutputFormFullPathName = CommonChouhyouBase.TemplatePath + "R434-Form.xml";

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";

            // 選択可能な集計項目グループ数
            this.SelectEnableSyuukeiKoumokuGroupCount = 4;

            // 選択可能な集計項目
            if (windowID == WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU ||
                windowID == WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU ||
                windowID == WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU)
            {   // R434(売上前年対比表・支払前年対比表・売上/支払前年対比表)

                this.SelectEnableSyuukeiKoumokuList = new List<int>()
                {
                    0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, /*11,*/ 12, 13, 14, 15, 16, 17, 19
                };

                // 対象テーブルリスト
                this.TaishouTableList = new List<TaishouTable>()
                {
                    new TaishouTable("T_UKEIRE_ENTRY", "T_UKEIRE_DETAIL"),      // 受入
                    new TaishouTable("T_SHUKKA_ENTRY", "T_SHUKKA_DETAIL"),      // 出荷
                    new TaishouTable("T_UR_SH_ENTRY", "T_UR_SH_DETAIL"),        // 売上／支払
                };
            }
            else
            {   // R434(計量前年対比表)

                if (windowID == WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU)
                {
                    this.SelectEnableSyuukeiKoumokuList = new List<int>()
                    {
                        0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, /*11,*/ 12, 13, 14, 15, 16, 17, 18, 19
                    };

                    // 対象テーブルリスト
                    this.TaishouTableList = new List<TaishouTable>()
                    {
                        new TaishouTable("T_KEIRYOU_ENTRY", "T_KEIRYOU_DETAIL"),    // 計量
                    };
                }
            }

            // 出力可能項目（伝票）の有効・無効
            this.OutEnableKoumokuDenpyou = false;

            // 出力可能項目（明細）の有効・無効
            this.OutEnableKoumokuMeisai = false;

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

                DateTime dateTimeStartTmp;
                DateTime dateTimeEndTmp;

                dateTimeStartTmp = this.DateTimeStart;
                dateTimeEndTmp = this.DateTimeEnd;

                this.dataTableInfo.Clear();
                for (int i = 0; i < 2; i++)
                {
                    if (i == 1)
                    {   // 去年
                        this.DateTimeStart = dateTimeStartTmp.AddYears(-1);
                        this.DateTimeEnd = dateTimeEndTmp.AddYears(-1);
                    }

                    // 入力関連データテーブルから取得したデータテーブルリスト
                    base.GetOutDataTable();

                    this.dataTableInfo.Add(this.InputDataTable);
                }

                // 元に戻す
                this.DateTimeStart = dateTimeStartTmp;
                this.DateTimeEnd = dateTimeEndTmp;

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
                // 受け渡し用データーテーブル（明細）フィールド名の設定処理
                this.SetDetailFieldNameForUkewatashi();

                string tmp;
                DataRow dataRow;
                DataRow dataRowSort;

                // 複数テーブル（今年及び去年）の並べ替え処理(テーブルインデックス・伝票区分ソート)
                this.MultiSortTableDenpyouKubun();

                this.ChouhyouDataTable = new DataTable();

                SyuukeiKoumoku syuukeiKoumoku;

                #region - テーブルカラム作成 -

                // 集計項目領域
                this.ChouhyouDataTable.Columns.Add("DTL_KAHEN1_CD_1_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN1_1_VLB");
                this.ChouhyouDataTable.Columns.Add("DTL_KAHEN1_CD_2_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN1_2_VLB");
                this.ChouhyouDataTable.Columns.Add("DTL_KAHEN1_CD_3_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN1_3_VLB");
                this.ChouhyouDataTable.Columns.Add("DTL_KAHEN1_CD_4_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN1_4_VLB");

                // 売上-受入等々
                this.ChouhyouDataTable.Columns.Add("G2H_KAHEN1_1_VLB");
                this.ChouhyouDataTable.Columns.Add("GROUP2_CHANGE");

                // 帳票出力項目領域（明細換算）
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN1_5_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN1_6_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN1_7_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN1_8_VLB");

                // 明細換算総合計
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_1_VLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_2_VLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_3_VLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_4_VLB");

                #endregion - テーブルカラム作成 -

                string sql = string.Empty;
                int itemColumnIndex = 0;

                string tableName = string.Empty;
                string fieldName = string.Empty;

                int index = 0;
                int indexTmp = 0;
                object code = null;
                object codeData = null;

                // 有効な集計項目グループ数
                int syuukeiKoumokuEnableGroupCount = this.SelectEnableSyuukeiKoumokuGroupCount;

                // 金額
                decimal[] goukeiM = new decimal[2];
                decimal[] goukeiAllTotalM = new decimal[2];
                decimal[] goukeiTmpM = new decimal[2];
                decimal[] hinmeiGoukeiTmpM = new decimal[2];
                decimal tmpM;

                // 重量
                decimal[] goukeiT = new decimal[2];
                decimal[] goukeiAllTotalT = new decimal[2];
                decimal[] goukeiTmpT = new decimal[2];
                decimal[] hinmeiGoukeiTmpT = new decimal[2];
                decimal tmpT;

                int indexTable;
                int indexTableRow;
                int denpyouKubunCode;
                // 今年(0)・去年(1)・
                int kotoshiFlag = 0;

                DateTime denpyouDate;
                int denpyouKubunCD;
                string hinmeiCD;
                int unitCD;
                bool isTon = false;
                bool isKansanShikiUse = false;
                int kansanShiki = 0;
                decimal kansanValue = 0;

                DataTable[] inputDataTableInfo = null;

                DataRow dataRowNew;

                // 受渡用DataRow作成
                DataRow dataRowUkewatashi;

                Encoding encoding = Encoding.GetEncoding("Shift_JIS");
                byte[] byteArray;

                string[] syuukeiKoumokuPrevN = new string[syuukeiKoumokuEnableGroupCount];
                string[] syuukeiKoumokuNextN = new string[syuukeiKoumokuEnableGroupCount];

                int indexTablePrev = -1;
                int denpyouKubunPrev = -1;

                for (int rowCount = 0; rowCount < this.DataTableMultiSort.DefaultView.Count; rowCount++)
                {
                    dataRowSort = this.DataTableMultiSort.DefaultView[rowCount].Row;
                    dataRow = this.DataTableMultiSort.DefaultView[rowCount].Row;

                    dataRowNew = this.ChouhyouDataTable.NewRow();

                    // 受渡用DataRow作成
                    dataRowUkewatashi = this.DataTableUkewatashi.NewRow();

                    indexTable = int.Parse((string)dataRowSort.ItemArray[this.SelectSyuukeiKoumokuList.Count]);
                    indexTableRow = int.Parse((string)dataRowSort.ItemArray[this.SelectSyuukeiKoumokuList.Count + 1]);
                    denpyouKubunCode = int.Parse((string)dataRowSort.ItemArray[this.SelectSyuukeiKoumokuList.Count + 2]);
                    kotoshiFlag = int.Parse((string)dataRowSort.ItemArray[this.SelectSyuukeiKoumokuList.Count + 3]);

                    if (denpyouKubunPrev != denpyouKubunCode || indexTablePrev != indexTable)
                    {   // グループ名表示

                        denpyouKubunPrev = denpyouKubunCode;
                        indexTablePrev = indexTable;

                        if (this.WindowID == WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU)
                        {   // 売上前年対比表・支払前年対比表

                            #region - 売上前年対比表・支払前年対比表 -

                            if (this.DenpyouSyurui == DENPYOU_SYURUI.Subete)
                            {   // 全て

                                if (this.IsDenpyouSyuruiGroupKubun)
                                {   // グループ区分有
                                    indexTmp = this.DataTableUkewatashi.Columns.IndexOf("GROUP_LABEL");

                                    if (indexTable == 0)
                                    {   // 受入
                                        dataRowNew["G2H_KAHEN1_1_VLB"] = "受入";
                                        dataRowUkewatashi[indexTmp] = "受入";
                                    }
                                    else if (indexTable == 1)
                                    {   // 出荷
                                        dataRowNew["G2H_KAHEN1_1_VLB"] = "出荷";
                                        dataRowUkewatashi[indexTmp] = "出荷";
                                    }
                                    else if (indexTable == 2)
                                    {   // 売上／支払
                                        dataRowNew["G2H_KAHEN1_1_VLB"] = "売上／支払";
                                        dataRowUkewatashi[indexTmp] = "売上／支払";
                                    }
                                }
                            }

                            #endregion - 売上前年対比表・支払前年対比表 -
                        }
                        else if (this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU)
                        {   // 売上／支払前年対比表

                            #region - 売上／支払前年対比表 -

                            if (this.IsDenpyouSyuruiGroupKubun)
                            {   // グループ区分有
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("GROUP_LABEL");

                                if (indexTable == 0)
                                {   // 受入
                                    if (denpyouKubunCode == (int)DENPYOU_KUBUN.Uriage)
                                    {   // 売上
                                        dataRowNew["G2H_KAHEN1_1_VLB"] = "受入・売上";
                                        dataRowUkewatashi[indexTmp] = "受入・売上";
                                    }
                                    else if (denpyouKubunCode == (int)DENPYOU_KUBUN.Shiharai)
                                    {   // 支払
                                        dataRowNew["G2H_KAHEN1_1_VLB"] = "受入・支払";
                                        dataRowUkewatashi[indexTmp] = "受入・支払";
                                    }
                                }
                                else if (indexTable == 1)
                                {   // 出荷
                                    if (denpyouKubunCode == (int)DENPYOU_KUBUN.Uriage)
                                    {   // 売上
                                        dataRowNew["G2H_KAHEN1_1_VLB"] = "出荷・売上";
                                        dataRowUkewatashi[indexTmp] = "出荷・売上";
                                    }
                                    else if (denpyouKubunCode == (int)DENPYOU_KUBUN.Shiharai)
                                    {   // 支払
                                        dataRowNew["G2H_KAHEN1_1_VLB"] = "出荷・支払";
                                        dataRowUkewatashi[indexTmp] = "出荷・支払";
                                    }
                                }
                                else if (indexTable == 2)
                                {   // 売上／支払
                                    if (denpyouKubunCode == (int)DENPYOU_KUBUN.Uriage)
                                    {   // 売上
                                        dataRowNew["G2H_KAHEN1_1_VLB"] = "売上／支払・売上";
                                        dataRowUkewatashi[indexTmp] = "売上／支払・売上";
                                    }
                                    else if (denpyouKubunCode == (int)DENPYOU_KUBUN.Shiharai)
                                    {   // 支払
                                        dataRowNew["G2H_KAHEN1_1_VLB"] = "売上／支払・支払";
                                        dataRowUkewatashi[indexTmp] = "売上／支払・支払";
                                    }
                                }
                            }

                            #endregion - 売上／支払前年対比表 -
                        }
                        else if (this.WindowID == WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU)
                        {   // 計量前年対比表

                            #region - 計量前年対比表 -

                            if (this.DenpyouSyurui == DENPYOU_SYURUI.Subete && this.IsDenpyouSyuruiGroupKubun)
                            {   // 伝票種類が全てかつグループ区分有

                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("GROUP_LABEL");

                                if (denpyouKubunCode == (int)DENPYOU_KUBUN.Uriage)
                                {   // 売上
                                    dataRowNew["G2H_KAHEN1_1_VLB"] = "売上";
                                    dataRowUkewatashi[indexTmp] = "売上";
                                }
                                else if (denpyouKubunCode == (int)DENPYOU_KUBUN.Shiharai)
                                {   // 支払
                                    dataRowNew["G2H_KAHEN1_1_VLB"] = "支払";
                                    dataRowUkewatashi[indexTmp] = "支払";
                                }
                            }

                            #endregion - 計量前年対比表 -
                        }
                    }

                    if (this.WindowID == WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU)
                    {   // 売上前年対比表・支払前年対比表
                        dataRowNew["GROUP2_CHANGE"] = indexTable;
                    }
                    else
                    {   // 売上／支払前年対比表・計量前年対比表
                        dataRowNew["GROUP2_CHANGE"] = denpyouKubunCode;
                    }

                    string[] targetFieldData = new string[4];

                    inputDataTableInfo = this.dataTableInfo[kotoshiFlag];

                    #region - 集計項目用タイトルカラム -

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
                            index = inputDataTableInfo[indexTable].Columns.IndexOf("EIGYOU_TANTOUSHA_CD");

                            if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow]))
                            {
                                code = inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
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

                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("NIOROSHI_GYOUSHA_CD");
                                    if (index == -1)
                                    {
                                        code = string.Empty;
                                    }
                                    else
                                    {
                                        if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                        {
                                            code = inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
                                        }
                                        else
                                        {
                                            code = string.Empty;
                                        }
                                    }

                                    break;
                                case SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu:  // 荷卸現場別
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("NIOROSHI_GENBA_CD");
                                    if (index == -1)
                                    {
                                        code = string.Empty;
                                    }
                                    else
                                    {
                                        if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                        {
                                            code = inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
                                        }
                                        else
                                        {
                                            code = string.Empty;
                                        }
                                    }

                                    break;
                                case SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu:   // 荷積業者別
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("NIZUMI_GYOUSHA_CD");
                                    if (index == -1)
                                    {
                                        code = string.Empty;
                                    }
                                    else
                                    {
                                        if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                        {
                                            code = inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
                                        }
                                        else
                                        {
                                            code = string.Empty;
                                        }
                                    }

                                    break;
                                case SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu:    // 荷積現場別
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("NIZUMI_GENBA_CD");
                                    if (index == -1)
                                    {
                                        code = string.Empty;
                                    }
                                    else
                                    {
                                        if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                        {
                                            code = inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
                                        }
                                        else
                                        {
                                            code = string.Empty;
                                        }
                                    }

                                    break;
                                default:
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf(syuukeiKoumoku.FieldCD);
                                    if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        code = inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
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

                                    byteArray = encoding.GetBytes((string)codeData);
                                    if (byteArray.Length > 40)
                                    {
                                        codeData = encoding.GetString(byteArray, 0, 40);
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

                        targetFieldData[itemColumnIndex] = syuukeiKoumoku.FieldCD;

                        switch (itemColumnIndex + 1)
                        {
                            case 1: // 集計項目１
                                dataRowNew["DTL_KAHEN1_CD_1_VLB"] = code.ToString();
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_1_CD");
                                dataRowUkewatashi[indexTmp] = code.ToString();

                                dataRowNew["PHY_KAHEN1_1_VLB"] = codeData.ToString();
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_1_NAME");
                                dataRowUkewatashi[indexTmp] = codeData.ToString();

                                break;
                            case 2: // 集計項目２
                                dataRowNew["DTL_KAHEN1_CD_2_VLB"] = code.ToString();
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_2_CD");
                                dataRowUkewatashi[indexTmp] = code.ToString();

                                dataRowNew["PHY_KAHEN1_2_VLB"] = codeData.ToString();
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_2_NAME");
                                dataRowUkewatashi[indexTmp] = codeData.ToString();

                                break;
                            case 3: // 集計項目３
                                dataRowNew["DTL_KAHEN1_CD_3_VLB"] = code.ToString();
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_3_CD");
                                dataRowUkewatashi[indexTmp] = code.ToString();

                                dataRowNew["PHY_KAHEN1_3_VLB"] = codeData.ToString();
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_3_NAME");
                                dataRowUkewatashi[indexTmp] = codeData.ToString();

                                break;
                            case 4: // 集計項目４
                                dataRowNew["DTL_KAHEN1_CD_4_VLB"] = code.ToString();
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_4_CD");
                                dataRowUkewatashi[indexTmp] = code.ToString();

                                dataRowNew["PHY_KAHEN1_4_VLB"] = codeData.ToString();
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("FILL_COND_4_NAME");
                                dataRowUkewatashi[indexTmp] = codeData.ToString();

                                break;
                        }
                    }

                    #endregion - 集計項目用タイトルカラムテキスト -

                    for (; rowCount < this.DataTableMultiSort.DefaultView.Count; rowCount++)
                    {
                        dataRowSort = this.DataTableMultiSort.DefaultView[rowCount].Row;
                        dataRow = this.DataTableMultiSort.DefaultView[rowCount].Row;

                        indexTable = int.Parse((string)dataRowSort.ItemArray[this.SelectSyuukeiKoumokuList.Count]);
                        indexTableRow = int.Parse((string)dataRowSort.ItemArray[this.SelectSyuukeiKoumokuList.Count + 1]);
                        denpyouKubunCode = int.Parse((string)dataRowSort.ItemArray[this.SelectSyuukeiKoumokuList.Count + 2]);
                        kotoshiFlag = int.Parse((string)dataRowSort.ItemArray[this.SelectSyuukeiKoumokuList.Count + 3]);

                        inputDataTableInfo = this.dataTableInfo[kotoshiFlag];

                        for (itemColumnIndex = 0; itemColumnIndex < syuukeiKoumokuEnableGroupCount; itemColumnIndex++)
                        {
                            int itemIndex = this.SelectSyuukeiKoumokuList[itemColumnIndex];
                            syuukeiKoumoku = this.SyuukeiKomokuList[itemIndex];

                            if (syuukeiKoumoku.MasterTableID == WINDOW_ID.NONE)
                            {
                                continue;
                            }

                            index = inputDataTableInfo[indexTable].Columns.IndexOf(targetFieldData[itemColumnIndex]);
                            if (targetFieldData[itemColumnIndex].Equals("KYOTEN_CD"))
                            {
                                syuukeiKoumokuNextN[itemColumnIndex] = ((short)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]).ToString();
                            }
                            else
                            {
                                if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    syuukeiKoumokuNextN[itemColumnIndex] = (string)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
                                }
                                else
                                {
                                    syuukeiKoumokuNextN[itemColumnIndex] = string.Empty;
                                }
                            }
                        }

                        bool isChange = false;
                        for (itemColumnIndex = 0; itemColumnIndex < syuukeiKoumokuEnableGroupCount; itemColumnIndex++)
                        {
                            int itemIndex = this.SelectSyuukeiKoumokuList[itemColumnIndex];
                            syuukeiKoumoku = this.SyuukeiKomokuList[itemIndex];

                            if (syuukeiKoumoku.MasterTableID == WINDOW_ID.NONE)
                            {
                                continue;
                            }

                            if (syuukeiKoumokuNextN[itemColumnIndex] != syuukeiKoumokuPrevN[itemColumnIndex])
                            {
                                syuukeiKoumokuPrevN[itemColumnIndex] = syuukeiKoumokuNextN[itemColumnIndex];
                                isChange = true;
                            }
                        }

                        if (isChange && rowCount != 0)
                        {   // 変化あり

                            if (this.WindowID == WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU)
                            {   // 売上前年対比表・支払前年対比表

                                #region - 売上前年対比表・支払前年対比表 -

                                // 今年売上額（円）・今年支払額（円）
                                if (goukeiM[0] != 0)
                                {
                                    dataRowNew["PHY_KAHEN1_5_VLB"] = goukeiM[0].ToString("#,0");
                                }
                                else
                                {
                                    //dataRowNew["PHY_KAHEN1_5_VLB"] = string.Empty;
                                    dataRowNew["PHY_KAHEN1_5_VLB"] = 0;
                                }

                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_ITEM_1");
                                dataRowUkewatashi[indexTmp] = goukeiM[0].ToString("#,0");

                                // 昨年売上額（円）・昨年支払額（円）
                                if (goukeiM[1] != 0)
                                {
                                    dataRowNew["PHY_KAHEN1_6_VLB"] = goukeiM[1].ToString("#,0");
                                }
                                else
                                {
                                    //dataRowNew["PHY_KAHEN1_6_VLB"] = string.Empty;
                                    dataRowNew["PHY_KAHEN1_6_VLB"] = 0;
                                }

                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_ITEM_2");
                                dataRowUkewatashi[indexTmp] = goukeiM[1].ToString("#,0");

                                // 差額（円）
                                tmpM = goukeiM[0] - goukeiM[1];
                                if (tmpM != 0)
                                {
                                    dataRowNew["PHY_KAHEN1_7_VLB"] = tmpM.ToString("#,0");
                                }
                                else
                                {
                                    //dataRowNew["PHY_KAHEN1_7_VLB"] = string.Empty;
                                    dataRowNew["PHY_KAHEN1_7_VLB"] = 0;
                                }

                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_ITEM_3");
                                dataRowUkewatashi[indexTmp] = tmpM.ToString("#,0");

                                // 対比率（％）
                                tmpT = 0;

                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_ITEM_4");

                                if (goukeiM[1] != 0)
                                {
                                    tmpT = (decimal)(goukeiM[0] / goukeiM[1] * 100);
                                }

                                if (tmpT != 0)
                                {
                                    dataRowNew["PHY_KAHEN1_8_VLB"] = tmpT.ToString("#,0");

                                    dataRowUkewatashi[indexTmp] = tmpT.ToString("#,0");
                                }
                                else
                                {
                                    //dataRowNew["PHY_KAHEN1_8_VLB"] = string.Empty;
                                    dataRowNew["PHY_KAHEN1_8_VLB"] = "-";

                                    dataRowUkewatashi[indexTmp] = "-";
                                }

                                for (int i = 0; i < goukeiM.Length; i++)
                                {
                                    goukeiM[i] = 0;
                                }

                                #endregion - 売上前年対比表・支払前年対比表 -
                            }
                            else
                            {   // 売上／支払前年対比表・計量前年対比表

                                #region - 売上／支払前年対比表・計量前年対比表 -

                                // 今年換算数量（ｔ）
                                if (goukeiT[0] != 0)
                                {
                                    dataRowNew["PHY_KAHEN1_5_VLB"] = goukeiT[0].ToString("#,0");
                                }
                                else
                                {
                                    //dataRowNew["PHY_KAHEN1_5_VLB"] = string.Empty;
                                    dataRowNew["PHY_KAHEN1_5_VLB"] = 0;
                                }

                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_ITEM_1");
                                dataRowUkewatashi[indexTmp] = goukeiT[0].ToString("#,0");

                                // 昨年換算数量（ｔ）
                                if (goukeiT[1] != 0)
                                {
                                    dataRowNew["PHY_KAHEN1_6_VLB"] = goukeiT[1].ToString("#,0");
                                }
                                else
                                {
                                    //dataRowNew["PHY_KAHEN1_6_VLB"] = string.Empty;
                                    dataRowNew["PHY_KAHEN1_6_VLB"] = 0;
                                }

                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_ITEM_2");
                                dataRowUkewatashi[indexTmp] = goukeiT[1].ToString("#,0");

                                // 換算数量差（ｔ）
                                tmpT = goukeiT[0] - goukeiT[1];

                                if (tmpT != 0)
                                {
                                    dataRowNew["PHY_KAHEN1_7_VLB"] = tmpT.ToString("#,0");
                                }
                                else
                                {
                                    //dataRowNew["PHY_KAHEN1_7_VLB"] = string.Empty;
                                    dataRowNew["PHY_KAHEN1_7_VLB"] = 0;
                                }

                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_ITEM_3");
                                dataRowUkewatashi[indexTmp] = tmpT.ToString("#,0");

                                // 対比率（％）
                                tmpT = 0;

                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_ITEM_4");

                                if (goukeiT[1] != 0)
                                {
                                    tmpT = goukeiT[0] / goukeiT[1] * 100;
                                }

                                if (tmpT != 0)
                                {
                                    dataRowNew["PHY_KAHEN1_8_VLB"] = tmpT.ToString("#,0");
                                    dataRowUkewatashi[indexTmp] = tmpT.ToString("#,0");
                                }
                                else
                                {
                                    //dataRowNew["PHY_KAHEN1_8_VLB"] = string.Empty;
                                    dataRowNew["PHY_KAHEN1_8_VLB"] = "-";
                                    dataRowUkewatashi[indexTmp] = "-";
                                }

                                for (int i = 0; i < goukeiT.Length; i++)
                                {
                                    goukeiT[i] = 0;
                                }

                                #endregion - 売上／支払前年対比表・計量前年対比表 -
                            }

                            rowCount--;

                            break;
                        }

                        isChange = false;

                        switch (this.DenpyouSyurui)
                        {
                            case DENPYOU_SYURUI.Ukeire:         // 受入

                                #region - 受入 -

                                if (this.WindowID == WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU)
                                {   // 売上前年対比表・支払前年対比表

                                    #region - 売上前年対比表・支払前年対比表 -

                                    // 金額
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("KINGAKU");
                                    if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        goukeiTmpM[kotoshiFlag] = (decimal)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        goukeiTmpM[kotoshiFlag] = 0;
                                    }

                                    // 品名別金額
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("HINMEI_KINGAKU");
                                    if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        hinmeiGoukeiTmpM[kotoshiFlag] = (decimal)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        hinmeiGoukeiTmpM[kotoshiFlag] = 0;
                                    }

                                    #endregion - 売上前年対比表・支払前年対比表 -
                                }
                                else if (this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU)
                                {   // 売上／支払前年対比表

                                    #region - 売上／支払前年対比表 -

                                    // 正味重量
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("NET_JYUURYOU");
                                    if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        goukeiTmpT[kotoshiFlag] = (decimal)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        goukeiTmpT[kotoshiFlag] = 0;
                                    }

                                    // 品名別金額
                                    hinmeiGoukeiTmpT[kotoshiFlag] = 0;

                                    #endregion - 売上／支払前年対比表 -
                                }
                                else
                                {   // 計量推移表
                                }

                                if (this.WindowID == WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU)
                                {   // 売上前年対比表・支払前年対比表

                                    goukeiTmpM[kotoshiFlag] = goukeiTmpM[kotoshiFlag] + hinmeiGoukeiTmpM[kotoshiFlag];
                                    goukeiM[kotoshiFlag] += goukeiTmpM[kotoshiFlag];

                                    goukeiAllTotalM[kotoshiFlag] += goukeiTmpM[kotoshiFlag];
                                }
                                else
                                {   // 売上／支払前年対比表・計量前年対比表
                                    goukeiTmpT[kotoshiFlag] = (decimal)(goukeiTmpT[kotoshiFlag] + hinmeiGoukeiTmpT[kotoshiFlag]) * 0.001m;
                                    goukeiT[kotoshiFlag] += goukeiTmpT[kotoshiFlag];

                                    goukeiAllTotalT[kotoshiFlag] += goukeiTmpT[kotoshiFlag];
                                }

                                #endregion - 受入 -

                                break;
                            case DENPYOU_SYURUI.Syutsuka:       // 出荷

                                #region - 出荷 -

                                if (this.WindowID == WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU)
                                {   // 売上前年対比表・支払前年対比表

                                    #region - 売上推移表・支払推移表 -

                                    // 金額
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("KINGAKU");
                                    if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        goukeiTmpM[kotoshiFlag] = (decimal)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        goukeiTmpM[kotoshiFlag] = 0;
                                    }

                                    // 品名別金額
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("HINMEI_KINGAKU");
                                    if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        hinmeiGoukeiTmpM[kotoshiFlag] = (decimal)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        hinmeiGoukeiTmpM[kotoshiFlag] = 0;
                                    }

                                    #endregion - 売上推移表・支払推移表 -
                                }
                                else if (this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU)
                                {   // 売上／支払前年対比表

                                    #region - 売上／支払推移表 -

                                    // 正味重量
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("NET_JYUURYOU");
                                    if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        goukeiTmpT[kotoshiFlag] = (decimal)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        goukeiTmpT[kotoshiFlag] = 0;
                                    }

                                    // 品名別金額
                                    hinmeiGoukeiTmpT[kotoshiFlag] = 0;

                                    #endregion - 売上／支払推移表 -
                                }
                                else if (this.WindowID == WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU)
                                {   // 計量前年対比表
                                }

                                if (this.WindowID == WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU)
                                {   // 売上前年対比表・支払前年対比表
                                    goukeiTmpM[kotoshiFlag] = goukeiTmpM[kotoshiFlag] + hinmeiGoukeiTmpM[kotoshiFlag];
                                    goukeiM[kotoshiFlag] += goukeiTmpM[kotoshiFlag];

                                    goukeiAllTotalM[kotoshiFlag] += goukeiTmpM[kotoshiFlag];
                                }
                                else
                                {   // 売上／支払前年対比表・計量前年対比表
                                    goukeiTmpT[kotoshiFlag] = (decimal)(goukeiTmpT[kotoshiFlag] + hinmeiGoukeiTmpT[kotoshiFlag]) * 0.001m;
                                    goukeiT[kotoshiFlag] += goukeiTmpT[kotoshiFlag];

                                    goukeiAllTotalT[kotoshiFlag] += goukeiTmpT[kotoshiFlag];
                                }

                                #endregion - 出荷 -

                                break;
                            case DENPYOU_SYURUI.UriageShiharai: // 売上／支払

                                #region - 売上／支払 -

                                if (this.WindowID == WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU)
                                {   // 売上前年対比表・支払前年対比表

                                    #region - 売上前年対比表・支払前年対比表 -

                                    // 金額
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("KINGAKU");
                                    if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        goukeiTmpM[kotoshiFlag] = (decimal)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        goukeiTmpM[kotoshiFlag] = 0;
                                    }

                                    // 品名別金額
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("HINMEI_KINGAKU");
                                    if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        hinmeiGoukeiTmpM[kotoshiFlag] = (decimal)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        hinmeiGoukeiTmpM[kotoshiFlag] = 0;
                                    }

                                    #endregion - 売上前年対比表・支払前年対比表 -
                                }
                                else if (this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU)
                                {   // 売上／支払前年対比表

                                    #region - 売上／支払前年対比表 -

                                    // 正味重量
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("SUURYOU");
                                    if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        goukeiTmpT[kotoshiFlag] = (decimal)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        goukeiTmpT[kotoshiFlag] = 0;
                                    }

                                    // 伝票日付
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("DENPYOU_DATE");
                                    denpyouDate = (DateTime)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];

                                    // 伝票区分コード
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("DENPYOU_KBN_CD");
                                    denpyouKubunCD = (short)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];

                                    // 品名コード
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("HINMEI_CD");
                                    hinmeiCD = (string)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];

                                    // 単位コード
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("UNIT_CD");
                                    unitCD = (short)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];

                                    // 対象データーか否か
                                    if (!this.IsTaishou(denpyouDate, denpyouKubunCD, hinmeiCD, unitCD, ref isTon, ref isKansanShikiUse, ref kansanShiki, ref kansanValue))
                                    {   // 集計対象外
                                        continue;
                                    }

                                    if (isTon)
                                    {   // t
                                        if (isKansanShikiUse)
                                        {   // 換算式を使用する
                                            if (kansanShiki == 0)
                                            {   // 乗算
                                                goukeiTmpT[kotoshiFlag] *= kansanValue;
                                            }
                                            else
                                            {   // 除算
                                                if (kansanValue != 0)
                                                {
                                                    goukeiTmpT[kotoshiFlag] /= kansanValue;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {   // kg
                                        if (isKansanShikiUse)
                                        {   // 換算式を使用する
                                            if (kansanShiki == 0)
                                            {   // 乗算
                                                goukeiTmpT[kotoshiFlag] *= kansanValue;
                                            }
                                            else
                                            {   // 除算
                                                if (kansanValue != 0)
                                                {
                                                    goukeiTmpT[kotoshiFlag] /= kansanValue;
                                                }
                                            }
                                        }
                                    }

                                    // 品名別金額
                                    hinmeiGoukeiTmpT[kotoshiFlag] = 0;

                                    #endregion - 売上／支払前年対比表 -
                                }
                                else if (this.WindowID == WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU)
                                {   // 計量前年対比表
                                }

                                if (this.WindowID == WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU)
                                {   // 売上前年対比表・支払前年対比表
                                    goukeiTmpM[kotoshiFlag] = goukeiTmpM[kotoshiFlag] + hinmeiGoukeiTmpM[kotoshiFlag];
                                    goukeiM[kotoshiFlag] += goukeiTmpM[kotoshiFlag];

                                    goukeiAllTotalM[kotoshiFlag] += goukeiTmpM[kotoshiFlag];
                                }
                                else
                                {   // 売上／支払前年対比表・計量前年対比表
                                    goukeiTmpT[kotoshiFlag] = (decimal)(goukeiTmpT[kotoshiFlag] + hinmeiGoukeiTmpT[kotoshiFlag]) * 0.001m;
                                    goukeiT[kotoshiFlag] += goukeiTmpT[kotoshiFlag];

                                    goukeiAllTotalT[kotoshiFlag] += goukeiTmpT[kotoshiFlag];
                                }

                                #endregion - 売上／支払 -

                                break;
                            case DENPYOU_SYURUI.Subete:         // 全て

                                #region - 全て -

                                if (this.WindowID == WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU)
                                {   // 売上前年対比表・支払前年対比表

                                    #region - 売上前年対比表・支払前年対比表 -

                                    // 金額
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("KINGAKU");
                                    if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        goukeiTmpM[kotoshiFlag] = (decimal)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        goukeiTmpM[kotoshiFlag] = 0;
                                    }

                                    // 品名別金額
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("HINMEI_KINGAKU");
                                    if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        hinmeiGoukeiTmpM[kotoshiFlag] = (decimal)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        hinmeiGoukeiTmpM[kotoshiFlag] = 0;
                                    }

                                    #endregion - 売上前年対比表・支払前年対比表 -
                                }
                                else if (this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU)
                                {   // 売上／支払前年対比表

                                    #region - 売上／支払前年対比表 -

                                    if (indexTable == 0 || indexTable == 1)
                                    {   // 受入・出荷

                                        #region - 受入・出荷 -

                                        // 正味重量
                                        index = inputDataTableInfo[indexTable].Columns.IndexOf("NET_JYUURYOU");
                                        if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                        {
                                            goukeiTmpT[kotoshiFlag] = (decimal)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
                                        }
                                        else
                                        {
                                            goukeiTmpT[kotoshiFlag] = 0;
                                        }

                                        // 品名別金額
                                        hinmeiGoukeiTmpT[kotoshiFlag] = 0;

                                        #endregion - 受入・出荷 -
                                    }
                                    else
                                    {   // 売上／支払

                                        #region - 売上／支払 -

                                        // 正味重量
                                        index = inputDataTableInfo[indexTable].Columns.IndexOf("SUURYOU");
                                        if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                        {
                                            goukeiTmpT[kotoshiFlag] = (decimal)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
                                        }
                                        else
                                        {
                                            goukeiTmpT[kotoshiFlag] = 0;
                                        }

                                        // 伝票日付
                                        index = inputDataTableInfo[indexTable].Columns.IndexOf("DENPYOU_DATE");
                                        denpyouDate = (DateTime)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];

                                        // 伝票区分コード
                                        index = inputDataTableInfo[indexTable].Columns.IndexOf("DENPYOU_KBN_CD");
                                        denpyouKubunCD = (short)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];

                                        // 品名コード
                                        index = inputDataTableInfo[indexTable].Columns.IndexOf("HINMEI_CD");
                                        hinmeiCD = (string)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];

                                        // 単位コード
                                        index = inputDataTableInfo[indexTable].Columns.IndexOf("UNIT_CD");
                                        unitCD = (short)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];

                                        // 対象データーか否か
                                        if (!this.IsTaishou(denpyouDate, denpyouKubunCD, hinmeiCD, unitCD, ref isTon, ref isKansanShikiUse, ref kansanShiki, ref kansanValue))
                                        {   // 集計対象外
                                            continue;
                                        }

                                        if (isTon)
                                        {   // t
                                            if (isKansanShikiUse)
                                            {   // 換算式を使用する
                                                if (kansanShiki == 0)
                                                {   // 乗算
                                                    goukeiTmpT[kotoshiFlag] *= kansanValue;
                                                }
                                                else
                                                {   // 除算
                                                    if (kansanValue != 0)
                                                    {
                                                        goukeiTmpT[kotoshiFlag] /= kansanValue;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {   // kg
                                            if (isKansanShikiUse)
                                            {   // 換算式を使用する
                                                if (kansanShiki == 0)
                                                {   // 乗算
                                                    goukeiTmpT[kotoshiFlag] *= kansanValue;
                                                }
                                                else
                                                {   // 除算
                                                    if (kansanValue != 0)
                                                    {
                                                        goukeiTmpT[kotoshiFlag] /= kansanValue;
                                                    }
                                                }
                                            }
                                        }

                                        // 品名別金額
                                        hinmeiGoukeiTmpT[kotoshiFlag] = 0;

                                        #endregion - 売上／支払 -
                                    }

                                    #endregion - 売上／支払前年対比表 -
                                }
                                else
                                {   // 計量前年対比表

                                    #region - 計量前年対比表 -

                                    // 正味重量
                                    index = inputDataTableInfo[indexTable].Columns.IndexOf("NET_JYUURYOU");
                                    if (!this.IsDBNull(inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        goukeiTmpT[kotoshiFlag] = (decimal)inputDataTableInfo[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        goukeiTmpT[kotoshiFlag] = 0;
                                    }

                                    // 品名別金額
                                    hinmeiGoukeiTmpT[kotoshiFlag] = 0;

                                    #endregion - 計量前年対比表 -
                                }

                                if (this.WindowID == WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU)
                                {   // 売上前年対比表・支払前年対比表
                                    goukeiTmpM[kotoshiFlag] = goukeiTmpM[kotoshiFlag] + hinmeiGoukeiTmpM[kotoshiFlag];
                                    goukeiM[kotoshiFlag] += goukeiTmpM[kotoshiFlag];

                                    goukeiAllTotalM[kotoshiFlag] += goukeiTmpM[kotoshiFlag];
                                }
                                else
                                {   // 売上／支払前年対比表・計量前年対比表
                                    goukeiTmpT[kotoshiFlag] = (decimal)(goukeiTmpT[kotoshiFlag] + hinmeiGoukeiTmpT[kotoshiFlag]) * 0.001m;
                                    goukeiT[kotoshiFlag] += goukeiTmpT[kotoshiFlag];

                                    goukeiAllTotalT[kotoshiFlag] += goukeiTmpT[kotoshiFlag];
                                }

                                #endregion - 全て -

                                break;
                        }
                    }

                    if (rowCount == this.DataTableMultiSort.DefaultView.Count)
                    {
                        if (this.WindowID == WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU)
                        {   // 売上前年対比表・支払前年対比表

                            #region - 売上前年対比表・支払前年対比表 -

                            // 今年売上額（円）・今年支払額（円）
                            if (goukeiM[0] != 0)
                            {
                                dataRowNew["PHY_KAHEN1_5_VLB"] = goukeiM[0].ToString("#,0");
                            }
                            else
                            {
                                //dataRowNew["PHY_KAHEN1_5_VLB"] = string.Empty;
                                dataRowNew["PHY_KAHEN1_5_VLB"] = 0;
                            }

                            indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_ITEM_1");
                            dataRowUkewatashi[indexTmp] = goukeiM[0].ToString("#,0");

                            // 昨年売上額（円）・昨年支払額（円）
                            if (goukeiM[1] != 0)
                            {
                                dataRowNew["PHY_KAHEN1_6_VLB"] = goukeiM[1].ToString("#,0");
                            }
                            else
                            {
                                //dataRowNew["PHY_KAHEN1_6_VLB"] = string.Empty;
                                dataRowNew["PHY_KAHEN1_6_VLB"] = 0;
                            }

                            indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_ITEM_2");
                            dataRowUkewatashi[indexTmp] = goukeiM[1].ToString("#,0");

                            // 差額（円）
                            tmpM = goukeiM[0] - goukeiM[1];

                            if (tmpM != 0)
                            {
                                dataRowNew["PHY_KAHEN1_7_VLB"] = tmpM.ToString("#,0");
                            }
                            else
                            {
                                //dataRowNew["PHY_KAHEN1_7_VLB"] = string.Empty;
                                dataRowNew["PHY_KAHEN1_7_VLB"] = 0;
                            }

                            indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_ITEM_3");
                            dataRowUkewatashi[indexTmp] = tmpM.ToString("#,0");

                            // 対比率（％）
                            tmpT = 0;

                            indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_ITEM_4");

                            if (goukeiM[1] != 0)
                            {
                                tmpT = (decimal)(goukeiM[0] / goukeiM[1]) * 100;
                            }

                            if (tmpT != 0)
                            {
                                dataRowNew["PHY_KAHEN1_8_VLB"] = tmpT.ToString("#,0");
                                dataRowUkewatashi[indexTmp] = tmpT.ToString("#,0");
                            }
                            else
                            {
                                dataRowNew["PHY_KAHEN1_8_VLB"] = "-";
                                dataRowUkewatashi[indexTmp] = "-";
                            }

                            // 今年・去年金額総合系
                            for (int i = 0; i < goukeiAllTotalM.Length; i++)
                            {
                                tmp = string.Format("G1F_KINGAKU_TOTAL_{0}_VLB", i + 1);
                                dataRowNew[tmp] = goukeiAllTotalM[i].ToString("#,0");

                                tmp = string.Format("ALL_TOTAL_{0}", i + 1);
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf(tmp);
                                dataRowUkewatashi[indexTmp] = goukeiAllTotalM[i].ToString("#,0");
                            }

                            // 差額総合計
                            tmpM = goukeiAllTotalM[0] - goukeiAllTotalM[1];
                            dataRowNew["G1F_KINGAKU_TOTAL_3_VLB"] = tmpM.ToString("#,0");
                            indexTmp = this.DataTableUkewatashi.Columns.IndexOf("ALL_TOTAL_3");
                            dataRowUkewatashi[indexTmp] = tmpM.ToString("#,0");

                            // 対比率総合計（％）
                            indexTmp = this.DataTableUkewatashi.Columns.IndexOf("ALL_TOTAL_4");

                            if (goukeiAllTotalM[1] != 0)
                            {
                                tmpT = (decimal)(goukeiAllTotalM[0] / goukeiAllTotalM[1]) * 100;

                                dataRowNew["G1F_KINGAKU_TOTAL_4_VLB"] = tmpT.ToString("#,0");
                                dataRowUkewatashi[indexTmp] = tmpT.ToString("#,0");
                            }
                            else
                            {
                                tmpT = 0;
                                
                                dataRowNew["G1F_KINGAKU_TOTAL_4_VLB"] = "-";
                                dataRowUkewatashi[indexTmp] = "-";
                            }

                            #endregion - 売上前年対比表・支払前年対比表 -
                        }
                        else
                        {   // 売上／支払前年対比表・計量前年対比表

                            #region - 売上／支払前年対比表・計量前年対比表 -

                            // 今年換算数量（ｔ）
                            if (goukeiT[0] != 0)
                            {
                                dataRowNew["PHY_KAHEN1_5_VLB"] = goukeiT[0].ToString("#,0");
                            }
                            else
                            {
                                //dataRowNew["PHY_KAHEN1_5_VLB"] = string.Empty;
                                dataRowNew["PHY_KAHEN1_5_VLB"] = 0;
                            }

                            indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_ITEM_1");
                            dataRowUkewatashi[indexTmp] = goukeiT[0].ToString("#,0");

                            // 昨年換算数量（ｔ）
                            if (goukeiT[1] != 0)
                            {
                                dataRowNew["PHY_KAHEN1_6_VLB"] = goukeiT[1].ToString("#,0");
                            }
                            else
                            {
                                //dataRowNew["PHY_KAHEN1_6_VLB"] = string.Empty;
                                dataRowNew["PHY_KAHEN1_6_VLB"] = 0;
                            }

                            indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_ITEM_2");
                            dataRowUkewatashi[indexTmp] = goukeiT[1].ToString("#,0");

                            // 換算数量差（ｔ）
                            tmpT = goukeiT[0] - goukeiT[1];

                            if (tmpT != 0)
                            {
                                dataRowNew["PHY_KAHEN1_7_VLB"] = tmpT.ToString("#,0");
                            }
                            else
                            {
                                //dataRowNew["PHY_KAHEN1_7_VLB"] = string.Empty;
                                dataRowNew["PHY_KAHEN1_7_VLB"] = 0;
                            }

                            indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_ITEM_3");
                            dataRowUkewatashi[indexTmp] = tmpT.ToString("#,0");

                            // 対比率（％）
                            tmpT = 0;

                            indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_ITEM_4");

                            if (goukeiT[1] != 0)
                            {
                                tmpT = goukeiT[0] / goukeiT[1] * 100;
                            }

                            if (tmpT != 0)
                            {
                                dataRowNew["PHY_KAHEN1_8_VLB"] = tmpT.ToString("#,0");
                                dataRowUkewatashi[indexTmp] = tmpT.ToString("#,0");
                            }
                            else
                            {
                                dataRowNew["PHY_KAHEN1_8_VLB"] = "-";
                                dataRowUkewatashi[indexTmp] = "-";
                            }

                            // 今年・去年重量総合計
                            for (int i = 0; i < goukeiAllTotalT.Length; i++)
                            {
                                tmp = string.Format("G1F_KINGAKU_TOTAL_{0}_VLB", i + 1);
                                dataRowNew[tmp] = goukeiAllTotalT[i].ToString("#,0");

                                tmp = string.Format("ALL_TOTAL_{0}", i + 1);
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf(tmp);
                                dataRowUkewatashi[indexTmp] = goukeiAllTotalT[i].ToString("#,0");
                            }

                            // 換算数量差分総合計
                            tmpT = goukeiAllTotalT[0] - goukeiAllTotalT[1];
                            dataRowNew["G1F_KINGAKU_TOTAL_3_VLB"] = tmpT.ToString("#,0");
                            indexTmp = this.DataTableUkewatashi.Columns.IndexOf("ALL_TOTAL_3");
                            dataRowUkewatashi[indexTmp] = tmpT.ToString("#,0");

                            // 対比率総合計（％）
                            indexTmp = this.DataTableUkewatashi.Columns.IndexOf("ALL_TOTAL_4");

                            if (goukeiAllTotalT[1] != 0)
                            {
                                tmpT = goukeiAllTotalT[0] / goukeiAllTotalT[1] * 100;

                                dataRowNew["G1F_KINGAKU_TOTAL_4_VLB"] = tmpT.ToString("#,0");
                                dataRowUkewatashi[indexTmp] = tmpT.ToString("#,0");
                            }
                            else
                            {
                                tmpT = 0;

                                dataRowNew["G1F_KINGAKU_TOTAL_4_VLB"] = "-";
                                dataRowUkewatashi[indexTmp] = "-";
                            }

                            #endregion - 売上／支払前年対比表・計量前年対比表 -
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

        /// <summary>複数テーブルの並べ替え処理(テーブルインデックス・伝票区分ソート)を実行する</summary>
        private void MultiSortTableDenpyouKubun()
        {
            try
            {
                DataGrid dataGrid = new DataGrid();
                DataSet dataSet = new DataSet("MultiSort");
                this.DataTableMultiSort = dataSet.Tables.Add("MultiSortTable");

                for (int k = 0; k < this.SelectSyuukeiKoumokuList.Count; k++)
                {
                    this.DataTableMultiSort.Columns.Add(string.Format("Field{0}", k));
                }

                this.DataTableMultiSort.Columns.Add("TableIndex");
                this.DataTableMultiSort.Columns.Add("RowIndex");
                this.DataTableMultiSort.Columns.Add("DenpyouKubun");
                this.DataTableMultiSort.Columns.Add("KotoshiKyonenFlag");

                SyuukeiKoumoku syuukeiKoumoku;
                int item;
                int index;
                int denpyouKubunCode;

                for (int ii = 0; ii < 2; ii++)
                {
                    DataTable[] inputDataTableInfo = this.dataTableInfo[ii];

                    if (inputDataTableInfo == null)
                    {
                        continue;
                    }

                    for (int i = 0; i < inputDataTableInfo.Length; i++)
                    {
                        if (inputDataTableInfo[i] == null)
                        {
                            continue;
                        }

                        if (this.DenpyouSyurui == DENPYOU_SYURUI.Ukeire)
                        {   // 受入のみ
                            if (i != ((int)DENPYOU_SYURUI.Ukeire - 1))
                            {
                                continue;
                            }
                        }
                        else if (this.DenpyouSyurui == DENPYOU_SYURUI.Syutsuka)
                        {   // 出荷のみ
                            if (i != ((int)DENPYOU_SYURUI.Syutsuka - 1))
                            {
                                continue;
                            }
                        }
                        else if (this.DenpyouSyurui == DENPYOU_SYURUI.UriageShiharai)
                        {   // 売上／支払のみ
                            if (i != ((int)DENPYOU_SYURUI.UriageShiharai - 1))
                            {
                                continue;
                            }
                        }

                        object code;
                        for (int j = 0; j < inputDataTableInfo[i].Rows.Count; j++)
                        {
                            if (this.SelectSyuukeiKoumokuList.Count > 0)
                            {
                                DataRow dataRowNew = this.DataTableMultiSort.NewRow();

                                for (int k = 0; k < this.SelectSyuukeiKoumokuList.Count; k++)
                                {
                                    item = this.SelectSyuukeiKoumokuList[k];

                                    syuukeiKoumoku = this.SyuukeiKomokuList[item];

                                    if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.None)
                                    {
                                        continue;
                                    }

                                    switch (syuukeiKoumoku.Type)
                                    {
                                        case SYUKEUKOMOKU_TYPE.DensyuKubunBetsu:    // 伝種区分別
                                            code = i;

                                            break;
                                        case SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu: // 荷卸業者別
                                            index = inputDataTableInfo[i].Columns.IndexOf("NIOROSHI_GYOUSHA_CD");
                                            if (index == -1)
                                            {
                                                code = string.Empty;
                                            }
                                            else
                                            {
                                                code = inputDataTableInfo[i].Rows[j].ItemArray[index];
                                            }

                                            break;
                                        case SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu:  // 荷卸現場別
                                            index = inputDataTableInfo[i].Columns.IndexOf("NIOROSHI_GENBA_CD");
                                            if (index == -1)
                                            {
                                                code = string.Empty;
                                            }
                                            else
                                            {
                                                code = inputDataTableInfo[i].Rows[j].ItemArray[index];
                                            }

                                            break;
                                        case SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu:   // 荷積業者別
                                            index = inputDataTableInfo[i].Columns.IndexOf("NIZUMI_GYOUSHA_CD");
                                            if (index == -1)
                                            {
                                                code = string.Empty;
                                            }
                                            else
                                            {
                                                code = inputDataTableInfo[i].Rows[j].ItemArray[index];
                                            }

                                            break;
                                        case SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu:    // 荷積現場別
                                            index = inputDataTableInfo[i].Columns.IndexOf("NIZUMI_GENBA_CD");
                                            if (index == -1)
                                            {
                                                code = string.Empty;
                                            }
                                            else
                                            {
                                                code = inputDataTableInfo[i].Rows[j].ItemArray[index];
                                            }

                                            break;
                                        case SYUKEUKOMOKU_TYPE.EigyoTantoshaBetsu:    // 営業担当者別
                                            index = inputDataTableInfo[i].Columns.IndexOf("EIGYOU_TANTOU_CD");
                                            if (index == -1)
                                            {
                                                code = string.Empty;
                                            }
                                            else
                                            {
                                                code = inputDataTableInfo[i].Rows[j].ItemArray[index];
                                            }

                                            break;
                                        default:
                                            index = inputDataTableInfo[i].Columns.IndexOf(syuukeiKoumoku.FieldCD);
                                            code = inputDataTableInfo[i].Rows[j].ItemArray[index];

                                            break;
                                    }

                                    dataRowNew[string.Format("Field{0}", k)] = code;
                                }

                                dataRowNew["TableIndex"] = i.ToString();
                                dataRowNew["RowIndex"] = j.ToString();

                                index = inputDataTableInfo[i].Columns.IndexOf("DENPYOU_KBN_CD");
                                if (!this.IsDBNull(inputDataTableInfo[i].Rows[j].ItemArray[index]))
                                {
                                    denpyouKubunCode = (short)inputDataTableInfo[i].Rows[j].ItemArray[index];
                                    dataRowNew["DenpyouKubun"] = denpyouKubunCode.ToString();
                                }
                                else
                                {
                                    dataRowNew["DenpyouKubun"] = string.Empty;
                                }

                                dataRowNew["KotoshiKyonenFlag"] = ii;

                                this.DataTableMultiSort.Rows.Add(dataRowNew);
                            }
                        }
                    }
                }

                // 並べ替え条件
                string sortJyouken = string.Empty;
                if (this.IsDenpyouSyuruiGroupKubun)
                {   // グループ区分有
                    sortJyouken = "TableIndex ASC,";
                    sortJyouken += "DenpyouKubun ASC,";
                }
                else
                {   // グループ区分無
                    if (this.DenpyouSyurui == DENPYOU_SYURUI.Subete)
                    {   // 全て
                    }
                    else
                    {   // 受入・出荷・売上／支払
                        sortJyouken = "TableIndex ASC,";
                    }
                }

                for (int i = 0; i < this.SelectSyuukeiKoumokuList.Count; i++)
                {
                    item = this.SelectSyuukeiKoumokuList[i];

                    syuukeiKoumoku = this.SyuukeiKomokuList[item];

                    if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.None)
                    {
                        continue;
                    }

                    sortJyouken += string.Format("Field{0} ", i) + "ASC,";
                }

                if (this.IsDenpyouSyuruiGroupKubun)
                {   // グループ区分有
                }
                else
                {   // グループ区分無
                    if (this.DenpyouSyurui == DENPYOU_SYURUI.Subete)
                    {   // 全て
                        sortJyouken += "TableIndex ASC,";
                        sortJyouken += "DenpyouKubun ASC,";
                    }
                    else
                    {   // 受入・出荷・売上／支払
                        sortJyouken += "DenpyouKubun ASC,";
                    }
                }

                if (sortJyouken.Length != 0)
                {
                    sortJyouken = sortJyouken.Substring(0, sortJyouken.Length - 1);

                    // 並べ替え
                    this.DataTableMultiSort.DefaultView.Sort = sortJyouken;
                    dataGrid.SetDataBinding(this.DataTableMultiSort.DefaultView, string.Empty);
                }
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        #endregion - Methods -
    }

    #endregion - CommonChouhyouR434 -

    #endregion - Classes -
}
