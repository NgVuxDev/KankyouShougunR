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

    #region - CommonChouhyouR432 -

    /// <summary>推移表を表すクラス・コントロール</summary>
    public class CommonChouhyouR432 : CommonChouhyouBase
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="CommonChouhyouR432"/> class.</summary>
        /// <param name="windowID">画面ＩＤ</param>
        public CommonChouhyouR432(WINDOW_ID windowID)
            : base(windowID)
        {
            // 帳票出力フルパスフォーム名
            this.OutputFormFullPathName = CommonChouhyouBase.TemplatePath + "R432-Form.xml";

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";

            // 選択可能な集計項目グループ数
            this.SelectEnableSyuukeiKoumokuGroupCount = 4;

            // 選択可能な集計項目
            if (windowID == WINDOW_ID.R_URIAGE_SUIIHYOU ||
                windowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU ||
                windowID == WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU)
            {   // R342(売上推移表・支払推移表・売上/支払推移表)

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
            {   // R342(計量推移表)

                this.SelectEnableSyuukeiKoumokuList = new List<int>()
                {
                    0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19
                };

                // 対象テーブルリスト
                this.TaishouTableList = new List<TaishouTable>()
                {
                    new TaishouTable("T_KEIRYOU_ENTRY", "T_KEIRYOU_DETAIL"),    // 計量
                };
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
                throw;
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
                throw;
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

                // 重量フォーマット
                string jyuuryouFormat = this.MSysInfo.SYS_JYURYOU_FORMAT;

                // 受け渡し用データーテーブル（明細）フィールド名の設定処理
                this.SetDetailFieldNameForUkewatashi();

                string tmp;
                DataRow dataRow;
                DataRow dataRowSort;

                // 複数テーブルの並べ替え処理(テーブルインデックス・伝票区分ソート)
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

                // 帳票出力項目領域（明細伝票月）
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_1_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_2_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_3_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_4_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_5_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_6_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_7_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_8_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_9_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_10_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_11_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KAHEN2_12_VLB");

                // 伝票合計
                this.ChouhyouDataTable.Columns.Add("PHY_TOTAL_FLB");

                // 総合計
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_1_VLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_2_VLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_3_VLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_4_VLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_5_VLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_6_VLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_7_VLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_8_VLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_9_VLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_10_VLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_11_VLB");
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_12_VLB");

                // 総合計
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_13_VLB");

                #endregion - テーブルカラム作成 -

                DateTime dateTimeStartTmp = this.DateTimeStart;
                DateTime dateTimeTmp;
                int[] indexMonth = new int[12];

                for (int i = 0; i < 12; i++)
                {
                    dateTimeTmp = dateTimeStartTmp.AddMonths(i);

                    indexMonth[dateTimeTmp.Month - 1] = i;
                }

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
                decimal[] goukeiMonthM = new decimal[12 + 1];
                decimal[] goukeiMonthTotalM = new decimal[12 + 1];
                decimal goukeiAllTotalM = 0;
                decimal goukeiTmpM = 0;
                decimal hinmeiGoukeiTmpM = 0;

                // 重量
                decimal[] goukeiMonthT = new decimal[12 + 1];
                decimal[] goukeiMonthTotalT = new decimal[12 + 1];
                decimal goukeiAllTotalT = 0;
                decimal goukeiTmpT = 0;
                decimal hinmeiGoukeiTmpT = 0;

                int monthTmp;
                int indexTable;
                int indexTableRow;
                int denpyouKubunCode;

                DateTime denpyouDate;
                int denpyouKubunCD = 0;
                string hinmeiCD;
                int unitCD = 0;
                bool isTon = false;
                bool isKansanShikiUse = false;
                int kansanShiki = 0;
                decimal kansanValue = 0;

                DataRow dataRowNew;

                // 受渡用DataRow作成
                DataRow dataRowUkewatashi;

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

                    if (denpyouKubunPrev != denpyouKubunCode || indexTablePrev != indexTable)
                    {   // グループ名表示

                        denpyouKubunPrev = denpyouKubunCode;
                        indexTablePrev = indexTable;

                        if (this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU)
                        {   // 売上推移表・支払推移表

                            #region - 売上推移表・支払推移表 -

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

                            #endregion - 売上推移表・支払推移表 -
                        }
                        else if (this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU)
                        {   // 売上／支払推移表

                            #region - 売上／支払推移表 -

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

                            #endregion - 売上／支払推移表 -
                        }
                        else if (this.WindowID == WINDOW_ID.R_KEIRYOU_SUIIHYOU)
                        {   // 計量推移表

                            #region - 計量推移表 -

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

                            #endregion - 計量推移表 -
                        }
                    }

                    if (this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU)
                    {   // 売上推移表・支払推移表
                        dataRowNew["GROUP2_CHANGE"] = indexTable;
                    }
                    else
                    {   // 売上／支払推移表・計量推移表
                        dataRowNew["GROUP2_CHANGE"] = denpyouKubunCode;
                    }

                    string[] targetFieldData = new string[4];

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
                        {
							// FieldCDが社員CDの場合は営業担当者CDを抽出対象とする
							// TODO:そもそもSyuukeiKomokuListの営業担当者CD項目のFieldCDが社員CDになってしまっているため
							// 根本対応はSyuukeiKomokuListを直すべき。だが、対症療法的な修正をしている箇所が複数あるため
							// 根本対応を行う場合は全て対応する必要あり
							// 営業担当者別
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
                                        var gyoushaCd = String.Empty;

                                        var columnName = String.Empty;
                                        if (syuukeiKoumoku.FieldCD.Equals("GENBA_CD"))
                                        {
                                            columnName = "GYOUSHA_CD";
                                        }
                                        else if (syuukeiKoumoku.FieldCD.Equals("NIOROSHI_GENBA_CD"))
                                        {
                                            columnName = "NIOROSHI_GYOUSHA_CD";
                                        }
                                        else if (syuukeiKoumoku.FieldCD.Equals("NIZUMI_GENBA_CD"))
                                        {
                                            columnName = "NIZUMI_GYOUSHA_CD";
                                        }
                                        var value = this.InputDataTable[indexTable].Rows[indexTableRow][columnName];
                                        if (null != value)
                                        {
                                            gyoushaCd = value.ToString();
                                        }

                                        sql = string.Format("SELECT M_GENBA.GENBA_NAME_RYAKU FROM M_GENBA WHERE M_GENBA.GYOUSHA_CD = '{0}' AND M_GENBA.GENBA_CD = '{1}'", gyoushaCd, code);
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

						if(syuukeiKoumoku.FieldCD == "SHAIN_CD")
						{
							// FieldCDが社員CDの場合は営業担当者CDを抽出対象とする
							// TODO:そもそもSyuukeiKomokuListの営業担当者CD項目のFieldCDが社員CDになってしまっているため
							// 根本対応はSyuukeiKomokuListを直すべき。だが、対症療法的な修正をしている箇所が複数あるため
							// 根本対応を行う場合は全て対応する必要あり
							targetFieldData[itemColumnIndex] = "EIGYOU_TANTOUSHA_CD";
						}
						else
						{
							targetFieldData[itemColumnIndex] = syuukeiKoumoku.FieldCD;
						}

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

                        for (itemColumnIndex = 0; itemColumnIndex < syuukeiKoumokuEnableGroupCount; itemColumnIndex++)
                        {
                            int itemIndex = this.SelectSyuukeiKoumokuList[itemColumnIndex];
                            syuukeiKoumoku = this.SyuukeiKomokuList[itemIndex];

                            if (syuukeiKoumoku.MasterTableID == WINDOW_ID.NONE)
                            {
                                continue;
                            }

                            index = this.InputDataTable[indexTable].Columns.IndexOf(targetFieldData[itemColumnIndex]);
                            if (targetFieldData[itemColumnIndex].Equals("KYOTEN_CD"))
                            {
                                syuukeiKoumokuNextN[itemColumnIndex] = ((short)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]).ToString();
                            }
                            else
                            {
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    syuukeiKoumokuNextN[itemColumnIndex] = (string)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
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

                            if (this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU)
                            {   // 売上推移表・支払推移表

                                #region - 売上推移表・支払推移表 -

                                for (int i = 0; i < goukeiMonthM.Length; i++)
                                {
                                    if (i != 12)
                                    {
                                        tmp = string.Format("PHY_KAHEN2_{0}_VLB", i + 1);

                                        if (goukeiMonthM[i] != 0)
                                        {
                                            dataRowNew[tmp] = goukeiMonthM[i].ToString("#,0");

                                            tmp = string.Format("OUTPUT_YEAR_MONTH_{0}", i + 1);
                                            indexTmp = this.DataTableUkewatashi.Columns.IndexOf(tmp);
                                            dataRowUkewatashi[indexTmp] = goukeiMonthM[i].ToString("#,0");
                                        }
                                        else
                                        {
                                            dataRowNew[tmp] = string.Empty;
                                        }
                                    }
                                    else
                                    {
                                        if (goukeiMonthM[i] != 0)
                                        {
                                            dataRowNew["PHY_TOTAL_FLB"] = goukeiMonthM[i].ToString("#,0");

                                            indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_YEAR_MONTH_TOTAL");
                                            dataRowUkewatashi[indexTmp] = goukeiMonthM[i].ToString("#,0");
                                        }
                                        else
                                        {
                                            dataRowNew["PHY_TOTAL_FLB"] = string.Empty;
                                        }
                                    }
                                }

                                for (int i = 0; i < goukeiMonthM.Length; i++)
                                {
                                    goukeiMonthM[i] = 0;
                                }

                                #endregion - 売上推移表・支払推移表 -
                            }
                            else
                            {   // 売上／支払推移表・計量推移表

                                #region - 売上／支払推移表・計量推移表 -

                                for (int i = 0; i < goukeiMonthT.Length; i++)
                                {
                                    if (i != 12)
                                    {
                                        tmp = string.Format("PHY_KAHEN2_{0}_VLB", i + 1);

                                        if (goukeiMonthT[i] != 0)
                                        {
                                            dataRowNew[tmp] = goukeiMonthT[i].ToString(jyuuryouFormat);

                                            tmp = string.Format("OUTPUT_YEAR_MONTH_{0}", i + 1);
                                            indexTmp = this.DataTableUkewatashi.Columns.IndexOf(tmp);
                                            dataRowUkewatashi[indexTmp] = goukeiMonthT[i].ToString(jyuuryouFormat);
                                        }
                                        else
                                        {
                                            dataRowNew[tmp] = string.Empty;
                                        }
                                    }
                                    else
                                    {
                                        if (goukeiMonthT[i] != 0)
                                        {
                                            dataRowNew["PHY_TOTAL_FLB"] = goukeiMonthT[i].ToString(jyuuryouFormat);

                                            indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_YEAR_MONTH_TOTAL");
                                            dataRowUkewatashi[indexTmp] = goukeiMonthT[i].ToString(jyuuryouFormat);
                                        }
                                        else
                                        {
                                            dataRowNew["PHY_TOTAL_FLB"] = string.Empty;
                                        }
                                    }
                                }

                                for (int i = 0; i < goukeiMonthT.Length; i++)
                                {
                                    goukeiMonthT[i] = 0;
                                }

                                #endregion - 売上／支払推移表・計量推移表 -
                            }

                            rowCount--;

                            break;
                        }

                        isChange = false;

                        // 伝票日付
                        index = this.InputDataTable[indexTable].Columns.IndexOf("DENPYOU_DATE");
                        if (this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                        {
                            continue;
                        }

                        dateTimeTmp = (DateTime)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                        monthTmp = dateTimeTmp.Month;

                        switch (this.DenpyouSyurui)
                        {
                            case DENPYOU_SYURUI.Ukeire:         // 受入

                                #region - 受入 -

                                if (this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU)
                                {   // 売上推移表・支払推移表

                                    #region - 売上推移表・支払推移表 -

                                    // 金額
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("KINGAKU");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        goukeiTmpM = (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        goukeiTmpM = 0;
                                    }

                                    // 品名別金額
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("HINMEI_KINGAKU");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        hinmeiGoukeiTmpM = (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        hinmeiGoukeiTmpM = 0;
                                    }

                                    #endregion - 売上推移表・支払推移表 -
                                }
                                else if (this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU)
                                {   // 売上／支払推移表

                                    #region - 売上／支払推移表 -

                                    // 正味重量
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("NET_JYUURYOU");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        //goukeiTmpT = (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                        goukeiTmpT = 0;
                                    }
                                    else
                                    {
                                        goukeiTmpT = 0;
                                    }

                                    // 品名別金額
                                    hinmeiGoukeiTmpT = 0;

                                    #endregion - 売上／支払推移表 -
                                }
                                else
                                {   // 計量推移表
                                }

                                index = indexMonth[monthTmp - 1];

                                if (this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU)
                                {   // 売上推移表・支払推移表

                                    goukeiTmpM = (goukeiTmpM + hinmeiGoukeiTmpM) * (decimal)0.001;
                                    goukeiMonthM[index] += goukeiTmpM;
                                    goukeiMonthM[12] += goukeiTmpM;
                                    goukeiMonthTotalM[index] += goukeiTmpM;

                                    goukeiAllTotalM += goukeiTmpM;
                                }
                                else
                                {   // 売上／支払推移表・計量推移表
                                    goukeiTmpT = (goukeiTmpT + hinmeiGoukeiTmpT) * 0.001m;
                                    goukeiMonthT[index] += goukeiTmpT;
                                    goukeiMonthT[12] += goukeiTmpT;
                                    goukeiMonthTotalT[index] += goukeiTmpT;

                                    goukeiAllTotalT += goukeiTmpT;
                                }

                                #endregion - 受入 -

                                break;
                            case DENPYOU_SYURUI.Syutsuka:       // 出荷

                                #region - 出荷 -

                                if (this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU)
                                {   // 売上推移表・支払推移表

                                    #region - 売上推移表・支払推移表 -

                                    // 金額
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("KINGAKU");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        goukeiTmpM = (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        goukeiTmpM = 0;
                                    }

                                    // 品名別金額
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("HINMEI_KINGAKU");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        hinmeiGoukeiTmpM = (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        hinmeiGoukeiTmpM = 0;
                                    }

                                    #endregion - 売上推移表・支払推移表 -
                                }
                                else if (this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU)
                                {   // 売上／支払推移表

                                    #region - 売上／支払推移表 -

                                    // 正味重量
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("NET_JYUURYOU");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        //goukeiTmpT = (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                        goukeiTmpT = 0;
                                    }
                                    else
                                    {
                                        goukeiTmpT = 0;
                                    }

                                    // 品名別金額
                                    hinmeiGoukeiTmpT = 0;

                                    #endregion - 売上／支払推移表 -
                                }
                                else if (this.WindowID == WINDOW_ID.R_KEIRYOU_SUIIHYOU)
                                {   // 計量推移表
                                }

                                index = indexMonth[monthTmp - 1];

                                if (this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU)
                                {   // 売上推移表・支払推移表
                                    goukeiTmpM = (goukeiTmpM + hinmeiGoukeiTmpM) * (decimal)0.001;
                                    goukeiMonthM[index] += goukeiTmpM;
                                    goukeiMonthM[12] += goukeiTmpM;
                                    goukeiMonthTotalM[index] += goukeiTmpM;

                                    goukeiAllTotalM += goukeiTmpM;
                                }
                                else
                                {   // 売上／支払推移表・計量推移表
                                    goukeiTmpT = (goukeiTmpT + hinmeiGoukeiTmpT) * 0.001m;
                                    goukeiMonthT[index] += goukeiTmpT;
                                    goukeiMonthT[12] += goukeiTmpT;
                                    goukeiMonthTotalT[index] += goukeiTmpT;

                                    goukeiAllTotalT += goukeiTmpT;
                                }

                                #endregion - 出荷 -

                                break;
                            case DENPYOU_SYURUI.UriageShiharai: // 売上／支払

                                #region - 売上／支払 -

                                if (this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU)
                                {   // 売上推移表・支払推移表

                                    #region - 売上推移表・支払推移表 -

                                    // 金額
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("KINGAKU");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        goukeiTmpM = (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        goukeiTmpM = 0;
                                    }

                                    // 品名別金額
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("HINMEI_KINGAKU");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        hinmeiGoukeiTmpM = (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        hinmeiGoukeiTmpM = 0;
                                    }

                                    #endregion - 売上推移表・支払推移表 -
                                }
                                else if (this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU)
                                {   // 売上／支払推移表

                                    #region - 売上／支払推移表 -

                                    // 正味重量
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("SUURYOU");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        //goukeiTmpT = (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                        goukeiTmpT = 0;
                                    }
                                    else
                                    {
                                        goukeiTmpT = 0;
                                    }

                                    // 伝票日付
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("DENPYOU_DATE");
                                    denpyouDate = (DateTime)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                                    // 伝票区分コード
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("DENPYOU_KBN_CD");
                                    if (index > -1)
                                    {
                                        int.TryParse(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index].ToString(), out denpyouKubunCD);
                                    }

                                    // 品名コード
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("HINMEI_CD");
                                    hinmeiCD = (string)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                                    // 単位コード
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("UNIT_CD");
                                    if (index > -1)
                                    {
                                        int.TryParse(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index].ToString(), out unitCD);
                                    }

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
                                                goukeiTmpT *= kansanValue;
                                            }
                                            else
                                            {   // 除算
                                                goukeiTmpT /= kansanValue;
                                            }
                                        }
                                    }
                                    else
                                    {   // kg
                                        if (isKansanShikiUse)
                                        {   // 換算式を使用する
                                            if (kansanShiki == 0)
                                            {   // 乗算
                                                goukeiTmpT *= kansanValue;
                                            }
                                            else
                                            {   // 除算
                                                goukeiTmpT /= kansanValue;
                                            }
                                        }
                                    }

                                    // 品名別金額
                                    hinmeiGoukeiTmpT = 0;

                                    #endregion - 売上／支払推移表 -
                                }
                                else if (this.WindowID == WINDOW_ID.R_KEIRYOU_SUIIHYOU)
                                {   // 計量推移表
                                }

                                index = indexMonth[monthTmp - 1];

                                if (this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU)
                                {   // 売上推移表・支払推移表
                                    goukeiTmpM = (goukeiTmpM + hinmeiGoukeiTmpM) * (decimal)0.001;
                                    goukeiMonthM[index] += goukeiTmpM;
                                    goukeiMonthM[12] += goukeiTmpM;
                                    goukeiMonthTotalM[index] += goukeiTmpM;

                                    goukeiAllTotalM += goukeiTmpM;
                                }
                                else
                                {   // 売上／支払推移表・計量推移表
                                    goukeiTmpT = (goukeiTmpT + hinmeiGoukeiTmpT) * 0.001m;
                                    goukeiMonthT[index] += goukeiTmpT;
                                    goukeiMonthT[12] += goukeiTmpT;
                                    goukeiMonthTotalT[index] += goukeiTmpT;

                                    goukeiAllTotalT += goukeiTmpT;
                                }

                                #endregion - 売上／支払 -

                                break;
                            case DENPYOU_SYURUI.Subete:         // 全て

                                #region - 全て -

                                if (this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU)
                                {   // 売上推移表・支払推移表

                                    #region - 売上推移表・支払推移表 -

                                    // 金額
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("KINGAKU");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        goukeiTmpM = (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        goukeiTmpM = 0;
                                    }

                                    // 品名別金額
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("HINMEI_KINGAKU");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        hinmeiGoukeiTmpM = (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                    }
                                    else
                                    {
                                        hinmeiGoukeiTmpM = 0;
                                    }

                                    #endregion - 売上推移表・支払推移表 -
                                }
                                else if (this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU)
                                {   // 売上／支払推移表

                                    #region - 売上／支払推移表 -

                                    if (indexTable == 0 || indexTable == 1)
                                    {   // 受入・出荷

                                        #region - 受入・出荷 -

                                        // 正味重量
                                        index = this.InputDataTable[indexTable].Columns.IndexOf("NET_JYUURYOU");
                                        if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                        {
                                            //goukeiTmpT = (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                            goukeiTmpT = 0;
                                        }
                                        else
                                        {
                                            goukeiTmpT = 0;
                                        }

                                        // 品名別金額
                                        hinmeiGoukeiTmpT = 0;

                                        #endregion - 受入・出荷 -
                                    }
                                    else
                                    {   // 売上／支払

                                        #region - 売上／支払 -

                                        // 正味重量
                                        index = this.InputDataTable[indexTable].Columns.IndexOf("SUURYOU");
                                        if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                        {
                                            //goukeiTmpT = (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                            goukeiTmpT = 0;
                                        }
                                        else
                                        {
                                            goukeiTmpT = 0;
                                        }

                                        // 伝票日付
                                        index = this.InputDataTable[indexTable].Columns.IndexOf("DENPYOU_DATE");
                                        denpyouDate = (DateTime)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                                        // 伝票区分コード
                                        index = this.InputDataTable[indexTable].Columns.IndexOf("DENPYOU_KBN_CD");
                                        if (index > -1)
                                        {
                                            int.TryParse(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index].ToString(), out denpyouKubunCD);
                                        }

                                        // 品名コード
                                        index = this.InputDataTable[indexTable].Columns.IndexOf("HINMEI_CD");
                                        hinmeiCD = (string)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                                        // 単位コード
                                        index = this.InputDataTable[indexTable].Columns.IndexOf("UNIT_CD");
                                        if (index > -1)
                                        {
                                            int.TryParse(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index].ToString(), out unitCD);
                                        }

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
                                                    goukeiTmpT *= kansanValue;
                                                }
                                                else
                                                {   // 除算
                                                    goukeiTmpT /= kansanValue;
                                                }
                                            }
                                        }
                                        else
                                        {   // kg
                                            if (isKansanShikiUse)
                                            {   // 換算式を使用する
                                                if (kansanShiki == 0)
                                                {   // 乗算
                                                    goukeiTmpT *= kansanValue;
                                                }
                                                else
                                                {   // 除算
                                                    goukeiTmpT /= kansanValue;
                                                }
                                            }
                                        }

                                        // 品名別金額
                                        hinmeiGoukeiTmpT = 0;

                                        #endregion - 売上／支払 -
                                    }

                                    #endregion - 売上／支払推移表 -
                                }
                                else
                                {   // 計量推移表

                                    #region - 計量推移表 -

                                    // 正味重量
                                    index = this.InputDataTable[indexTable].Columns.IndexOf("NET_JYUURYOU");
                                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                    {
                                        //goukeiTmpT = (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                                        goukeiTmpT = 0;
                                    }
                                    else
                                    {
                                        goukeiTmpT = 0;
                                    }

                                    // 品名別金額
                                    hinmeiGoukeiTmpT = 0;

                                    #endregion - 計量推移表 -
                                }

                                index = indexMonth[monthTmp - 1];

                                if (this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU)
                                {   // 売上推移表・支払推移表
                                    goukeiTmpM = (goukeiTmpM + hinmeiGoukeiTmpM) * (decimal)0.001;
                                    goukeiMonthM[index] += goukeiTmpM;
                                    goukeiMonthM[12] += goukeiTmpM;
                                    goukeiMonthTotalM[index] += goukeiTmpM;

                                    goukeiAllTotalM += goukeiTmpM;
                                }
                                else
                                {   // 売上／支払推移表・計量推移表
                                    goukeiTmpT = (goukeiTmpT + hinmeiGoukeiTmpT) * 0.001m;
                                    goukeiMonthT[index] += goukeiTmpT;
                                    goukeiMonthT[12] += goukeiTmpT;
                                    goukeiMonthTotalT[index] += goukeiTmpT;

                                    goukeiAllTotalT += goukeiTmpT;
                                }

                                #endregion - 全て -

                                break;
                        }
                    }

                    if (this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU)
                    {   // 売上推移表・支払推移表

                        #region - 売上推移表・支払推移表 -

                        if (rowCount == this.DataTableMultiSort.DefaultView.Count)
                        {
                            for (int i = 0; i < goukeiMonthM.Length; i++)
                            {
                                if (i != 12)
                                {
                                    tmp = string.Format("PHY_KAHEN2_{0}_VLB", i + 1);
                                    if (goukeiMonthM[i] != 0)
                                    {
                                        dataRowNew[tmp] = goukeiMonthM[i].ToString("#,0");

                                        tmp = string.Format("OUTPUT_YEAR_MONTH_{0}", i + 1);
                                        indexTmp = this.DataTableUkewatashi.Columns.IndexOf(tmp);
                                        dataRowUkewatashi[indexTmp] = goukeiMonthM[i].ToString("#,0");
                                    }
                                    else
                                    {
                                        dataRowNew[tmp] = string.Empty;
                                    }
                                }
                                else
                                {
                                    if (goukeiMonthM[i] != 0)
                                    {
                                        dataRowNew["PHY_TOTAL_FLB"] = goukeiMonthM[i].ToString("#,0");

                                        indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_YEAR_MONTH_TOTAL");
                                        dataRowUkewatashi[indexTmp] = goukeiMonthM[i].ToString("#,0");
                                    }
                                    else
                                    {
                                        dataRowNew["PHY_TOTAL_FLB"] = string.Empty;
                                    }
                                }
                            }
                        }

                        for (int i = 0; i < goukeiMonthM.Length - 1; i++)
                        {
                            tmp = string.Format("G1F_KINGAKU_TOTAL_{0}_VLB", i + 1);
                            if (goukeiMonthTotalM[i] != 0)
                            {
                                dataRowNew[tmp] = goukeiMonthTotalM[i].ToString("#,0");

                                tmp = string.Format("ALL_YEAR_MONTH_TOTAL_{0}", i + 1);
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf(tmp);
                                dataRowUkewatashi[indexTmp] = goukeiMonthTotalM[i].ToString("#,0");
                            }
                            else
                            {
                                dataRowNew[tmp] = string.Empty;
                            }
                        }

                        if (goukeiAllTotalM != 0)
                        {
                            dataRowNew["G1F_KINGAKU_TOTAL_13_VLB"] = goukeiAllTotalM.ToString("#,0");

                            indexTmp = this.DataTableUkewatashi.Columns.IndexOf("ALL_TOTAL_1");
                            dataRowUkewatashi[indexTmp] = goukeiAllTotalM.ToString("#,0");
                        }
                        else
                        {
                            dataRowNew["G1F_KINGAKU_TOTAL_13_VLB"] = string.Empty;
                        }

                        #endregion - 売上推移表・支払推移表 -
                    }
                    else
                    {   // 売上／支払推移表・計量推移表

                        #region - 売上／支払推移表・計量推移表 -

                        if (rowCount == this.DataTableMultiSort.DefaultView.Count)
                        {
                            for (int i = 0; i < goukeiMonthT.Length; i++)
                            {
                                if (i != 12)
                                {
                                    tmp = string.Format("PHY_KAHEN2_{0}_VLB", i + 1);
                                    if (goukeiMonthT[i] != 0)
                                    {
                                        dataRowNew[tmp] = goukeiMonthT[i].ToString(jyuuryouFormat);

                                        tmp = string.Format("OUTPUT_YEAR_MONTH_{0}", i + 1);
                                        indexTmp = this.DataTableUkewatashi.Columns.IndexOf(tmp);
                                        dataRowUkewatashi[indexTmp] = goukeiMonthT[i].ToString(jyuuryouFormat);
                                    }
                                    else
                                    {
                                        dataRowNew[tmp] = string.Empty;
                                    }
                                }
                                else
                                {
                                    if (goukeiMonthT[i] != 0)
                                    {
                                        dataRowNew["PHY_TOTAL_FLB"] = goukeiMonthT[i].ToString(jyuuryouFormat);

                                        indexTmp = this.DataTableUkewatashi.Columns.IndexOf("OUTPUT_YEAR_MONTH_TOTAL");
                                        dataRowUkewatashi[indexTmp] = goukeiMonthT[i].ToString(jyuuryouFormat);
                                    }
                                    else
                                    {
                                        dataRowNew["PHY_TOTAL_FLB"] = string.Empty;
                                    }
                                }
                            }
                        }

                        for (int i = 0; i < goukeiMonthT.Length - 1; i++)
                        {
                            tmp = string.Format("G1F_KINGAKU_TOTAL_{0}_VLB", i + 1);
                            if (goukeiMonthTotalT[i] != 0)
                            {
                                dataRowNew[tmp] = goukeiMonthTotalT[i].ToString(jyuuryouFormat);

                                tmp = string.Format("ALL_YEAR_MONTH_TOTAL_{0}", i + 1);
                                indexTmp = this.DataTableUkewatashi.Columns.IndexOf(tmp);
                                dataRowUkewatashi[indexTmp] = goukeiMonthTotalT[i].ToString(jyuuryouFormat);
                            }
                            else
                            {
                                dataRowNew[tmp] = string.Empty;
                            }
                        }

                        if (goukeiAllTotalT != 0)
                        {
                            dataRowNew["G1F_KINGAKU_TOTAL_13_VLB"] = goukeiAllTotalT.ToString(jyuuryouFormat);

                            indexTmp = this.DataTableUkewatashi.Columns.IndexOf("ALL_TOTAL_1");
                            dataRowUkewatashi[indexTmp] = goukeiAllTotalT.ToString(jyuuryouFormat);
                        }
                        else
                        {
                            dataRowNew["G1F_KINGAKU_TOTAL_13_VLB"] = string.Empty;
                        }

                        #endregion - 売上／支払推移表・計量推移表 -
                    }

                    this.ChouhyouDataTable.Rows.Add(dataRowNew);
                    this.DataTableUkewatashi.Rows.Add(dataRowUkewatashi);
                }
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
                throw;
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

                SyuukeiKoumoku syuukeiKoumoku;
                int item;
                int index;
                int denpyouKubunCode = 0;
                for (int i = 0; i < this.InputDataTable.Length; i++)
                {
                    if (this.InputDataTable[i] == null)
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
                    for (int j = 0; j < this.InputDataTable[i].Rows.Count; j++)
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
                                        index = this.InputDataTable[i].Columns.IndexOf("NIOROSHI_GYOUSHA_CD");
                                        if (index == -1)
                                        {
                                            code = string.Empty;
                                        }
                                        else
                                        {
                                            code = this.InputDataTable[i].Rows[j].ItemArray[index];
                                        }

                                        break;
                                    case SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu:  // 荷卸現場別
                                        index = this.InputDataTable[i].Columns.IndexOf("NIOROSHI_GENBA_CD");
                                        if (index == -1)
                                        {
                                            code = string.Empty;
                                        }
                                        else
                                        {
                                            code = this.InputDataTable[i].Rows[j].ItemArray[index];
                                        }

                                        break;
                                    case SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu:   // 荷積業者別
                                        index = this.InputDataTable[i].Columns.IndexOf("NIZUMI_GYOUSHA_CD");
                                        if (index == -1)
                                        {
                                            code = string.Empty;
                                        }
                                        else
                                        {
                                            code = this.InputDataTable[i].Rows[j].ItemArray[index];
                                        }

                                        break;
                                    case SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu:    // 荷積現場別
                                        index = this.InputDataTable[i].Columns.IndexOf("NIZUMI_GENBA_CD");
                                        if (index == -1)
                                        {
                                            code = string.Empty;
                                        }
                                        else
                                        {
                                            code = this.InputDataTable[i].Rows[j].ItemArray[index];
                                        }

                                        break;
                                    case SYUKEUKOMOKU_TYPE.EigyoTantoshaBetsu:    // 営業担当者別
                                        index = this.InputDataTable[i].Columns.IndexOf("EIGYOU_TANTOU_CD");
                                        if (index == -1)
                                        {
                                            code = string.Empty;
                                        }
                                        else
                                        {
                                            code = this.InputDataTable[i].Rows[j].ItemArray[index];
                                        }

                                        break;
                                    default:
                                        index = this.InputDataTable[i].Columns.IndexOf(syuukeiKoumoku.FieldCD);
                                        code = this.InputDataTable[i].Rows[j].ItemArray[index];

                                        break;
                                }

                                dataRowNew[string.Format("Field{0}", k)] = code;
                            }

                            dataRowNew["TableIndex"] = i.ToString();
                            dataRowNew["RowIndex"] = j.ToString();

                            index = this.InputDataTable[i].Columns.IndexOf("DENPYOU_KBN_CD");
                            if (index > -1)
                            {
                                int.TryParse(this.InputDataTable[i].Rows[j].ItemArray[index].ToString(), out denpyouKubunCode);
                            }
                            dataRowNew["DenpyouKubun"] = denpyouKubunCode.ToString();

                            this.DataTableMultiSort.Rows.Add(dataRowNew);
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
                throw;
            }
        }

        #endregion - Methods -
    }

    #endregion - CommonChouhyouR432 -

    #endregion - Classes -
}
