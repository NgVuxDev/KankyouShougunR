using System;
using System.Collections.Generic;
using System.Data;
using r_framework.Const;
using r_framework.Utility;

namespace Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup
{
    #region - Classes -

    #region - CommonChouhyouR356_R359_R363 -

    /// <summary>帳票Nを表すクラス・コントロール</summary>
    public class CommonChouhyouR356_R359_R363 : CommonChouhyouBase
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="CommonChouhyouR356_R359_R363"/> class.</summary>
        /// <param name="windowID">画面ＩＤ</param>
        public CommonChouhyouR356_R359_R363(WINDOW_ID windowID)
            : base(windowID)
        {
            // 帳票出力フルパスフォーム名
            this.OutputFormFullPathName = CommonChouhyouBase.TemplatePath + "R356_R359_R363-Form.xml";

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";

            // 選択可能な集計項目グループ数
            this.SelectEnableSyuukeiKoumokuGroupCount = 3;

            // 選択可能な集計項目
            if (windowID == WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU)
            {   // R356(売上／支払集計表)

                this.IsDenpyouSyuruiEnable = true;
                this.IsDenpyouKubunEnable = true;

                this.SelectEnableSyuukeiKoumokuList = new List<int>()
                {
                    0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
                };

                // 対象テーブルリスト
                this.TaishouTableList = new List<TaishouTable>()
                {
                    new TaishouTable("T_UKEIRE_ENTRY", "T_UKEIRE_DETAIL"), new TaishouTable("T_SHUKKA_ENTRY", "T_SHUKKA_DETAIL"), new TaishouTable("T_UR_SH_ENTRY", "T_UR_SH_DETAIL"),
                };
            }
            else if (windowID == WINDOW_ID.R_URIAGE_SYUUKEIHYOU || windowID == WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU)
            {   // R359(売上集計表)またはR363(支払集計表)
                this.SelectEnableSyuukeiKoumokuList = new List<int>()
                {
                    0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 19,
                };

                // 対象テーブルリスト
                this.TaishouTableList = new List<TaishouTable>()
                {
                    new TaishouTable("T_UKEIRE_ENTRY", "T_UKEIRE_DETAIL"), new TaishouTable("T_SHUKKA_ENTRY", "T_SHUKKA_DETAIL"), new TaishouTable("T_UR_SH_ENTRY", "T_UR_SH_DETAIL"),
                };
            }

            // 出力可能項目（伝票）の有効・無効
            this.OutEnableKoumokuDenpyou = false;

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

        /// <summary>初期化処理を実行する</summary>
        public override void Initialize()
        {
            try
            {
                // 初期化処理
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
                // 初期化処理
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

                // 帳票出力項目領域（明細部）
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

                // 集計項目４
                this.ChouhyouDataTable.Columns.Add("G5F_JYUURYOU_TOTAL_FLB");
                this.ChouhyouDataTable.Columns.Add("G5F_KINGAKU_TOTAL_FLB");

                // 集計項目３
                this.ChouhyouDataTable.Columns.Add("G4F_JYUURYOU_TOTAL_FLB");
                this.ChouhyouDataTable.Columns.Add("G4F_KINGAKU_TOTAL_FLB");

                // 集計項目２
                this.ChouhyouDataTable.Columns.Add("G3F_JYUURYOU_TOTAL_FLB");
                this.ChouhyouDataTable.Columns.Add("G3F_KINGAKU_TOTAL_FLB");

                // 集計項目１
                this.ChouhyouDataTable.Columns.Add("G2F_JYUURYOU_TOTAL_FLB");
                this.ChouhyouDataTable.Columns.Add("G2F_KINGAKU_TOTAL_FLB");

                // 総合計
                this.ChouhyouDataTable.Columns.Add("G1F_JYUURYOU_TOTAL_FLB");   // No.3781
                this.ChouhyouDataTable.Columns.Add("G1F_KINGAKU_TOTAL_FLB");

                #endregion - テーブルカラム作成 -

                string sql = string.Empty;
                int itemColumnIndex = 0;

                string tableName = string.Empty;
                string fieldName = string.Empty;

                int index = 0;
                object code = null;
                object codeData = null;

                // 有効な集計項目グループ数
                int syuukeiKoumokuEnableGroupCount = this.SelectEnableSyuukeiKoumokuGroupCount;

                decimal ukeireKingaku = 0;
                decimal ukeireHinmeikingaku = 0;
                decimal ukeireTaxSoto = 0;
                decimal ukeireHinmeiTaxSoto = 0;

                decimal syutsukaKingaku = 0;
                decimal syutsukaHinmeikingaku = 0;
                decimal syutsukaTaxSoto = 0;
                decimal syutsukaHinmeiTaxSoto = 0;

                decimal uriageShiharaiKingaku = 0;
                decimal uriageShiharaiHinmeikingaku = 0;
                decimal uriageShiharaiTaxSoto = 0;
                decimal uriageShiharaiHinmeiTaxSoto = 0;

                decimal souGoukei = 0;

                // No.3781-->
                decimal ukeireJyuuryou = 0;
                decimal syutsukaJyuuryou = 0;
                decimal uriageShiharaiJyuuryou = 0;

                decimal souJyuuryou = 0;
                decimal[] syuukeiKoumokuGoukeiJyuuryou = new decimal[syuukeiKoumokuEnableGroupCount];
                decimal[] syuukeiKoumokuGoukeiTotal = new decimal[syuukeiKoumokuEnableGroupCount];
                string[] syuukeiKoumokuCode = new string[syuukeiKoumokuEnableGroupCount];
                string[] syuukeiKoumokuCodeName = new string[syuukeiKoumokuEnableGroupCount];
                decimal kingaku = 0;
                decimal jyuuryou = 0;
                decimal totalKingaku = 0;
                decimal totalJyuuryou = 0;
                string[] syuukeiKoumokuNextN = new string[syuukeiKoumokuEnableGroupCount];
                string[] syuukeiKoumokuPrevN = new string[syuukeiKoumokuEnableGroupCount];
                for (int i = 0; i < syuukeiKoumokuEnableGroupCount; i++)
                {
                    syuukeiKoumokuPrevN[i] = "INITIALIZE VALUE";
                }
                string jyuuryouFormat = this.MSysInfo.SYS_JYURYOU_FORMAT;
                string kingakuFormat = "#,##0";
                // No.3781<--

                for (int rowCount = 0; rowCount < this.DataTableMultiSort.DefaultView.Count; rowCount++)
                {
                    dataRowSort = this.DataTableMultiSort.DefaultView[rowCount].Row;
                    dataRow = this.DataTableMultiSort.DefaultView[rowCount].Row;

                    DataRow dataRowNew = this.ChouhyouDataTable.NewRow();

                    int indexTable = int.Parse((string)dataRowSort.ItemArray[this.SelectSyuukeiKoumokuList.Count]);
                    int indexTableRow = int.Parse((string)dataRowSort.ItemArray[this.SelectSyuukeiKoumokuList.Count + 1]);

                    // 伝票番号
                    index = -1;
                    switch (indexTable)
                    {
                        case 0: // 受入入力
                            index = this.InputDataTable[indexTable].Columns.IndexOf("UKEIRE_NUMBER");
                            break;
                        case 1: // 出荷入力
                            index = this.InputDataTable[indexTable].Columns.IndexOf("SHUKKA_NUMBER");
                            break;
                        case 2: // 売上支払入力
                            index = this.InputDataTable[indexTable].Columns.IndexOf("UR_SH_NUMBER");
                            break;
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
                            if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                            {
                                code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                            }
                            else
                            {
                                code = string.Empty;
                            }
                        }
                        else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.DensyuKubunBetsu)
                        {   // 伝種区分別
                            code = (indexTable + 1).ToString();
                        }
                        else
                        {   // その他
                            index = this.InputDataTable[indexTable].Columns.IndexOf(syuukeiKoumoku.FieldCD);
                            if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                            {
                                code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];
                            }
                            else
                            {
                                code = string.Empty;
                            }
                        }

                        if (code.GetType() == typeof(string))
                        {
                            code = ((string)code).Replace(" ", string.Empty);

                            if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.DensyuKubunBetsu)
                            {   // 伝種区分別
                                switch (indexTable)
                                {
                                    case 0: // 受入入力
                                        codeData = "受入";

                                        break;
                                    case 1: // 出荷入力
                                        codeData = "出荷";

                                        break;
                                    case 2: // 売上支払入力
                                        codeData = "売上支払";

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

                        // No.3781-->
                        syuukeiKoumokuNextN[itemColumnIndex] = code.ToString();

                        if (!syuukeiKoumokuPrevN[itemColumnIndex].Equals(syuukeiKoumokuNextN[itemColumnIndex]))
                        {   // 集計項目Nが変化した

                            syuukeiKoumokuPrevN[itemColumnIndex] = syuukeiKoumokuNextN[itemColumnIndex];

                            for (int j = 0; j < syuukeiKoumokuEnableGroupCount; j++)
                            {
                                totalKingaku += syuukeiKoumokuGoukeiTotal[j];
                                totalJyuuryou += syuukeiKoumokuGoukeiJyuuryou[j];
                            }

                            if (itemColumnIndex == 0)
                            {
                                syuukeiKoumokuGoukeiTotal[0] = 0;
                                syuukeiKoumokuGoukeiJyuuryou[0] = 0;
                            }

                            for (int j = itemColumnIndex; j < syuukeiKoumokuEnableGroupCount; j++)
                            {
                                syuukeiKoumokuGoukeiTotal[j] = 0;
                                syuukeiKoumokuGoukeiJyuuryou[j] = 0;
                            }
                        }

                        syuukeiKoumokuCode[itemColumnIndex] = code.ToString();
                        syuukeiKoumokuCodeName[itemColumnIndex] = codeData.ToString();
                        // No.3781<--

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

                    #region - 合計項目 -
                    switch (indexTable)
                    {
                        case 0: // 受入
                            index = this.InputDataTable[indexTable].Columns.IndexOf("KINGAKU");
                            ukeireKingaku = this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]) ? 0 : (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                            index = this.InputDataTable[indexTable].Columns.IndexOf("TAX_SOTO");
                            ukeireTaxSoto = this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]) ? 0 : (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                            souGoukei += ukeireKingaku + ukeireHinmeikingaku + ukeireTaxSoto + ukeireHinmeiTaxSoto;

                            // No.3781-->
                            index = this.InputDataTable[indexTable].Columns.IndexOf("NET_JYUURYOU");
                            if (index >= 0)
                            {
                                ukeireJyuuryou = this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]) ? 0 : Convert.ToDecimal(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]);
                                souJyuuryou += ukeireJyuuryou;
                            }
                            // No.3781<--

                            break;
                        case 1: // 出荷
                            index = this.InputDataTable[indexTable].Columns.IndexOf("KINGAKU");
                            syutsukaKingaku = this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]) ? 0 : (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                            index = this.InputDataTable[indexTable].Columns.IndexOf("TAX_SOTO");
                            syutsukaTaxSoto = this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]) ? 0 : (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                            souGoukei += syutsukaKingaku + syutsukaHinmeikingaku + syutsukaTaxSoto + syutsukaHinmeiTaxSoto;

                            // No.3781-->
                            index = this.InputDataTable[indexTable].Columns.IndexOf("NET_JYUURYOU");
                            if (index >= 0)
                            {
                                syutsukaJyuuryou = this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]) ? 0 : Convert.ToDecimal(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]);
                                souJyuuryou += syutsukaJyuuryou;
                            }
                            // No.3781<--
                            break;
                        case 2: // 売上／支払
                            index = this.InputDataTable[indexTable].Columns.IndexOf("KINGAKU");
                            uriageShiharaiKingaku = this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]) ? 0 : (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                            index = this.InputDataTable[indexTable].Columns.IndexOf("TAX_SOTO");
                            uriageShiharaiTaxSoto = this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]) ? 0 : (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                            souGoukei += uriageShiharaiKingaku + uriageShiharaiHinmeikingaku + uriageShiharaiTaxSoto + uriageShiharaiHinmeiTaxSoto;

                            // No.3781-->
                            index = this.InputDataTable[indexTable].Columns.IndexOf("NET_JYUURYOU");
                            if (index >= 0)
                            {
                                uriageShiharaiJyuuryou = this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]) ? 0 : Convert.ToDecimal(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]);
                                souJyuuryou += uriageShiharaiJyuuryou;
                            }
                            // No.3781<--
                            break;
                    }
                    #endregion - 合計項目 -

                    #region - 帳票出力項目（明細）用タイトルカラム -

                    itemColumnIndex = 0;
                    bool isDefault;
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

                                    string tmp = (string)code;
                                    if (!tmp.Equals(string.Empty))
                                    {
                                        sql = string.Format("SELECT M_SHURUI.SHURUI_NAME_RYAKU FROM M_SHURUI WHERE M_SHURUI.SHURUI_CD = '{0}'", code);

                                        dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                                        index = dataTableTmp.Columns.IndexOf("SHURUI_NAME_RYAKU");
                                        if (dataTableTmp.Rows.Count != 0)
                                        {
                                            codeData = (string)dataTableTmp.Rows[0].ItemArray[index];
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
                                    code = string.Empty;
                                    codeData = string.Empty;
                                }

                                break;
                            case "BUNRUI_NAME":
                                index = this.InputDataTable[indexTable].Columns.IndexOf("BUNRUI_CD");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                                    string tmp = (string)code;
                                    if (!tmp.Equals(string.Empty))
                                    {
                                        sql = string.Format("SELECT M_BUNRUI.BUNRUI_NAME_RYAKU FROM M_BUNRUI WHERE M_BUNRUI.BUNRUI_CD = '{0}'", code);

                                        dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                                        index = dataTableTmp.Columns.IndexOf("BUNRUI_NAME_RYAKU");
                                        if (dataTableTmp.Rows.Count != 0)
                                        {
                                            codeData = (string)dataTableTmp.Rows[0].ItemArray[index];
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
                                    code = string.Empty;
                                    codeData = string.Empty;
                                }

                                break;
                            case "HINMEI_NAME":
                                index = this.InputDataTable[indexTable].Columns.IndexOf("HINMEI_CD");
                                if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]))
                                {
                                    code = this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                                    string tmp = (string)code;
                                    if (!tmp.Equals(string.Empty))
                                    {
                                        sql = string.Format("SELECT M_HINMEI.HINMEI_NAME_RYAKU FROM M_HINMEI WHERE M_HINMEI.HINMEI_CD = '{0}'", code);

                                        dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                                        index = dataTableTmp.Columns.IndexOf("HINMEI_NAME_RYAKU");
                                        if (dataTableTmp.Rows.Count != 0)
                                        {
                                            codeData = (string)dataTableTmp.Rows[0].ItemArray[index];
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
                                            string tmp = (string)code;
                                            if (!tmp.Equals(string.Empty))
                                            {
                                                sql = string.Format("SELECT M_YOUKI.YOUKI_NAME_RYAKU FROM M_YOUKI WHERE M_YOUKI.YOUKI_CD = '{0}'", code);

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
                                            string tmp = (string)code;
                                            if (!tmp.Equals(string.Empty))
                                            {
                                                sql = string.Format("SELECT M_NISUGATA.NISUGATA_NAME_RYAKU FROM M_NISUGATA WHERE M_NISUGATA.NISUGATA_CD = '{0}'", code);

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
                                            if (!this.IsDBNull(code))
                                            {
                                                if (fieldName.Equals("KINGAKU"))
                                                {
                                                    index = this.InputDataTable[indexTable].Columns.IndexOf("TAX_UCHI");
                                                    decimal taxUchi = this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index]) ? 0 : (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[index];

                                                    // 金額 - 内税
                                                    codeData = this.ConvertNull2Zero(code) - taxUchi;
                                                }
                                                else
                                                {
                                                    codeData = code;
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
                            codeData = dataTmp.ToString(format);
                            // No.3781-->
                            if (fieldName.Equals("KINGAKU"))
                            {
                                codeData = dataTmp.ToString(kingakuFormat);
                            }
                            // No.3781<--
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

                    #endregion - 帳票出力項目（明細）用タイトルカラム -


                    // No.3781-->
                    // 金額
                    int tmpindex = this.InputDataTable[indexTable].Columns.IndexOf("KINGAKU");
                    if (!this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[tmpindex]))
                    {   // データーがNULLでない
                        kingaku = (decimal)this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[tmpindex];
                    }
                    else
                    {
                        kingaku = 0;
                    }
                    // 重量
                    tmpindex = this.InputDataTable[indexTable].Columns.IndexOf("NET_JYUURYOU");
                    if (tmpindex >= 0 && !this.IsDBNull(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[tmpindex]))
                    {   // データーがNULLでない
                        jyuuryou = Convert.ToDecimal(this.InputDataTable[indexTable].Rows[indexTableRow].ItemArray[tmpindex]);
                    }
                    else
                    {
                        jyuuryou = 0;
                    }

                    // 合計
                    for (itemColumnIndex = 0; itemColumnIndex < syuukeiKoumokuEnableGroupCount; itemColumnIndex++)
                    {
                        int itemIndex = this.SelectSyuukeiKoumokuList[itemColumnIndex];
                        SyuukeiKoumoku tmpsyuukeiKoumoku = this.SyuukeiKomokuList[itemIndex];

                        if (tmpsyuukeiKoumoku.MasterTableID == WINDOW_ID.NONE)
                        {
                            continue;
                        }

                        syuukeiKoumokuGoukeiJyuuryou[itemColumnIndex] += jyuuryou;
                        syuukeiKoumokuGoukeiTotal[itemColumnIndex] += kingaku;

                        string strjyuuryou = syuukeiKoumokuGoukeiJyuuryou[itemColumnIndex].ToString(jyuuryouFormat);

                        switch (itemColumnIndex)
                        {
                            case 0: // 集計項目１
                                dataRowNew["G2F_JYUURYOU_TOTAL_FLB"] = strjyuuryou;
                                dataRowNew["G2F_KINGAKU_TOTAL_FLB"] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];
                                break;
                            case 1: // 集計項目２
                                dataRowNew["G3F_JYUURYOU_TOTAL_FLB"] = strjyuuryou;
                                dataRowNew["G3F_KINGAKU_TOTAL_FLB"] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];
                                break;
                            case 2: // 集計項目３
                                dataRowNew["G4F_JYUURYOU_TOTAL_FLB"] = strjyuuryou;
                                dataRowNew["G4F_KINGAKU_TOTAL_FLB"] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];
                                break;
                            case 3: // 集計項目４
                                dataRowNew["G5F_JYUURYOU_TOTAL_FLB"] = strjyuuryou;
                                dataRowNew["G5F_KINGAKU_TOTAL_FLB"] = syuukeiKoumokuGoukeiTotal[itemColumnIndex];
                                break;
                        }
                    }

                    dataRowNew["G1F_JYUURYOU_TOTAL_FLB"] = souJyuuryou.ToString(jyuuryouFormat);
                    // No.3781<--

                    dataRowNew["G1F_KINGAKU_TOTAL_FLB"] = souGoukei;

                    this.ChouhyouDataTable.Rows.Add(dataRowNew);
                }
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        #endregion - Methods -
    }

    #endregion - CommonChouhyouR356_R359_R363 -

    #endregion - Classes -
}
