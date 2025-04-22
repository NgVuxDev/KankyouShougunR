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

    #region - CommonChouhyouR370_R377 -

    /// <summary>帳票Nを表すクラス・コントロール</summary>
    public class CommonChouhyouR370_R377 : CommonChouhyouBase
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="CommonChouhyouR370_R377"/> class.</summary>
        /// <param name="windowID">画面ＩＤ</param>
        public CommonChouhyouR370_R377(WINDOW_ID windowID)
            : base(windowID)
        {
            // 帳票出力フルパスフォーム名
            this.OutputFormFullPathName = CommonChouhyouBase.TemplatePath + "R370_R377-Form.xml";

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";

            // 選択可能な集計項目グループ数
            this.SelectEnableSyuukeiKoumokuGroupCount = 2;

            // 選択可能な集計項目
            this.SelectEnableSyuukeiKoumokuList = new List<int>()
            {
                0, 1, 23,
            };

            if (windowID == WINDOW_ID.R_NYUUKIN_YOTEI_ICHIRANHYOU)
            {   // R370(入金予定一覧表)
                
                // 対象テーブルリスト
                this.TaishouTableList = new List<TaishouTable>()
                {
                    new TaishouTable("T_SEIKYUU_DENPYOU"),
                };
            }
            else if (windowID == WINDOW_ID.R_SYUKKIN_YOTEI_ICHIRANHYOU)
            {   // R377(出金予定一覧表)

                // 対象テーブルリスト
                this.TaishouTableList = new List<TaishouTable>()
                {
                    new TaishouTable("T_SEISAN_DENPYOU"),
                };
            }

            // 入力関連テーブル名
            this.InKanrenTable = new List<string>()
            {
                string.Empty,
            };

            // 出力可能項目（伝票）の有効・無効
            this.OutEnableKoumokuDenpyou = false;

            // 出力可能項目（明細）の有効・無効
            this.OutEnableKoumokuMeisai = false;

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

                if (this.InputDataTable[0].Rows.Count == 0)
                {
                    return;
                }

                string sql;

                int indexNyuukinSyutsukinYoteiBi = 0;
                if (this.WindowID == WINDOW_ID.R_NYUUKIN_YOTEI_ICHIRANHYOU)
                {   // 入金予定一覧表
                    indexNyuukinSyutsukinYoteiBi = this.InputDataTable[0].Columns.IndexOf("NYUUKIN_YOTEI_BI");
                }
                else if (this.WindowID == WINDOW_ID.R_SYUKKIN_YOTEI_ICHIRANHYOU)
                {   // 出金予定一覧表
                    indexNyuukinSyutsukinYoteiBi = this.InputDataTable[0].Columns.IndexOf("SHUKKIN_YOTEI_BI");
                }

                DataRow dataRow = this.InputDataTable[0].Rows[0];

                // 入金予定日/支払予定日
                string nyuukinShiharaiYoteiBiPrev;
                if (!this.IsDBNull(dataRow.ItemArray[indexNyuukinSyutsukinYoteiBi]))
                {
                    nyuukinShiharaiYoteiBiPrev = ((DateTime)dataRow.ItemArray[indexNyuukinSyutsukinYoteiBi]).ToString("yyyy/MM/dd");
                }
                else
                {
                    nyuukinShiharaiYoteiBiPrev = string.Empty;
                }

                string nyuukinShiharaiYoteiBiNext = nyuukinShiharaiYoteiBiPrev;

                // 取引先CD
                int indexTorihikisakiCD = this.InputDataTable[0].Columns.IndexOf("TORIHIKISAKI_CD");
                string torihikisakiCD;
                if (!this.IsDBNull(dataRow.ItemArray[indexTorihikisakiCD]))
                {
                    torihikisakiCD = (string)dataRow.ItemArray[indexTorihikisakiCD];
                }
                else
                {
                    torihikisakiCD = string.Empty;
                }

                // 取引先CD名(M_TORIHIKISAKI)
                sql = string.Format("SELECT M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU FROM M_TORIHIKISAKI WHERE TORIHIKISAKI_CD = '{0}'", torihikisakiCD);
                DataTable dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                int indexTorihikisakiName = dataTableTmp.Columns.IndexOf("TORIHIKISAKI_NAME_RYAKU");
                string torihikisakiCDName;
                if (!this.IsDBNull(dataTableTmp.Rows[0].ItemArray[indexTorihikisakiName]))
                {
                    torihikisakiCDName = (string)dataTableTmp.Rows[0].ItemArray[indexTorihikisakiName];
                }
                else
                {
                    torihikisakiCDName = string.Empty;
                }

                // 入金額/支払額
                int indexKonkaiNyuukinShiharaigaku = 0;
                if (this.WindowID == WINDOW_ID.R_NYUUKIN_YOTEI_ICHIRANHYOU)
                {   // 入金予定一覧表
                    indexKonkaiNyuukinShiharaigaku = this.InputDataTable[0].Columns.IndexOf("KONKAI_NYUUKIN_GAKU");
                }
                else if (this.WindowID == WINDOW_ID.R_SYUKKIN_YOTEI_ICHIRANHYOU)
                {   // 出金予定一覧表
                    indexKonkaiNyuukinShiharaigaku = this.InputDataTable[0].Columns.IndexOf("KONKAI_SHUKKIN_GAKU");
                }

                decimal konkaiNyuukinShiharaigaku = 0;

                // 今回調整額
                int indexKonkaiChouseigaku = this.InputDataTable[0].Columns.IndexOf("KONKAI_CHOUSEI_GAKU");
                decimal konkaiChouseigaku = 0;

                // 今回請求支払額
                int indexKonkaiTorihikiGaku = 0;
                if (this.WindowID == WINDOW_ID.R_NYUUKIN_YOTEI_ICHIRANHYOU)
                {   // 入金予定一覧表
                    indexKonkaiTorihikiGaku = this.InputDataTable[0].Columns.IndexOf("KONKAI_URIAGE_GAKU");
                }
                else if (this.WindowID == WINDOW_ID.R_SYUKKIN_YOTEI_ICHIRANHYOU)
                {   // 出金予定一覧表
                    indexKonkaiTorihikiGaku = this.InputDataTable[0].Columns.IndexOf("KONKAI_SHIHARAI_GAKU");
                }
                decimal konkaiTorihikiGaku = 0;

                // 今回請求/精算内税額
                int indexKonkaiSeiUchizeiGaku = this.InputDataTable[0].Columns.IndexOf("KONKAI_SEI_UTIZEI_GAKU");
                decimal konkaiSeiUchizeiGaku = 0;

                // 今回請求/精算外税額
                int indexKonkaiSeiSotozeiGaku = this.InputDataTable[0].Columns.IndexOf("KONKAI_SEI_SOTOZEI_GAKU");
                decimal konkaiSeiSotozeiGaku = 0;

                // 今回伝票内税額
                int indexKonkaiDenUchizeiGaku = this.InputDataTable[0].Columns.IndexOf("KONKAI_DEN_UTIZEI_GAKU");
                decimal konkaiDenUchizeiGaku = 0;

                // 今回伝票外税額
                int indexKonkaiDenSotozeiGaku = this.InputDataTable[0].Columns.IndexOf("KONKAI_DEN_SOTOZEI_GAKU");
                decimal konkaiDenSotozeiGaku = 0;

                // 今回明細内税額
                int indexKonkaiMeiUchizeiGaku = this.InputDataTable[0].Columns.IndexOf("KONKAI_MEI_UTIZEI_GAKU");
                decimal konkaiMeiUchizeiGaku = 0;

                // 今回明細外税額
                int indexKonkaiMeiSotozeiGaku = this.InputDataTable[0].Columns.IndexOf("KONKAI_MEI_SOTOZEI_GAKU");
                decimal konkaiMeiSotozeiGaku = 0;

                // 締日
                int indexShimeBi = this.InputDataTable[0].Columns.IndexOf("SHIMEBI");
                string shimeBi;
                if (!this.IsDBNull(dataRow.ItemArray[indexShimeBi]))
                {
                    shimeBi = dataRow.ItemArray[indexShimeBi].ToString();
                }
                else
                {
                    shimeBi = string.Empty;
                }

                // 請求日/精算日
                int indexSeikyuuSeisanDate = 0;
                if (this.WindowID == WINDOW_ID.R_NYUUKIN_YOTEI_ICHIRANHYOU)
                {   // 入金予定一覧表
                    indexSeikyuuSeisanDate = this.InputDataTable[0].Columns.IndexOf("SEIKYUU_DATE");
                }
                else if (this.WindowID == WINDOW_ID.R_SYUKKIN_YOTEI_ICHIRANHYOU)
                {   // 出金予定一覧表
                    indexSeikyuuSeisanDate = this.InputDataTable[0].Columns.IndexOf("SEISAN_DATE");
                }

                string seikyuuSeisanDate;
                if (!this.IsDBNull(dataRow.ItemArray[indexSeikyuuSeisanDate]))
                {
                    seikyuuSeisanDate = ((DateTime)dataRow.ItemArray[indexSeikyuuSeisanDate]).ToString("yyyy/MM/dd");
                }
                else
                {
                    seikyuuSeisanDate = string.Empty;
                }

                decimal sougoukei = 0;

                this.ChouhyouDataTable.Columns.Add("PHY_NYUUKIN_YOTEI_BI_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_TORIHIKISAKI_CD_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_TORIHIKISAKI_NAME_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KONKAI_NYUUKIN_GAKU_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_SHIMEBI_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_SEIKYUU_DATE_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_SOUGOUKEI_VLB");

                for (int i = 0; i < this.InputDataTable[0].Rows.Count; i++)
                {
                    dataRow = this.InputDataTable[0].Rows[i];

                    DataRow row = this.ChouhyouDataTable.NewRow();

                    if (!this.IsDBNull(dataRow.ItemArray[indexNyuukinSyutsukinYoteiBi]))
                    {
                        nyuukinShiharaiYoteiBiNext = ((DateTime)dataRow.ItemArray[indexNyuukinSyutsukinYoteiBi]).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        nyuukinShiharaiYoteiBiNext = string.Empty;
                    }

                    if (nyuukinShiharaiYoteiBiPrev != nyuukinShiharaiYoteiBiNext)
                    {
                        // 入金予定日/支払予定日
                        if (!this.IsDBNull(dataRow.ItemArray[indexNyuukinSyutsukinYoteiBi]))
                        {
                            nyuukinShiharaiYoteiBiPrev = ((DateTime)dataRow.ItemArray[indexNyuukinSyutsukinYoteiBi]).ToString("yyyy/MM/dd");
                        }
                        else
                        {
                            nyuukinShiharaiYoteiBiPrev = string.Empty;
                        }

                        nyuukinShiharaiYoteiBiNext = nyuukinShiharaiYoteiBiPrev;

                        // 入金額/支払額
                        konkaiNyuukinShiharaigaku = 0;

                        konkaiChouseigaku = 0;
                    }

                    // 取引先CD
                    if (!this.IsDBNull(dataRow.ItemArray[indexTorihikisakiCD]))
                    {
                        torihikisakiCD = (string)dataRow.ItemArray[indexTorihikisakiCD];
                    }
                    else
                    {
                        torihikisakiCD = string.Empty;
                    }

                    // 取引先CD名(M_TORIHIKISAKI)
                    sql = string.Format("SELECT M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU FROM M_TORIHIKISAKI WHERE TORIHIKISAKI_CD = '{0}'", torihikisakiCD);
                    dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                    if (dataTableTmp.Rows.Count != 0)
                    {
                        torihikisakiCDName = (string)dataTableTmp.Rows[0].ItemArray[indexTorihikisakiName];
                    }
                    else
                    {
                        torihikisakiCDName = string.Empty;
                    }

                    // 今回入金額/今回出金額
                    if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiNyuukinShiharaigaku]))
                    {
                        konkaiNyuukinShiharaigaku = (decimal)dataRow.ItemArray[indexKonkaiNyuukinShiharaigaku];
                    }
                    else
                    {
                        konkaiNyuukinShiharaigaku = 0;
                    }

                    // 締日
                    if (!this.IsDBNull(dataRow.ItemArray[indexShimeBi]))
                    {
                        shimeBi = dataRow.ItemArray[indexShimeBi].ToString();
                    }
                    else
                    {
                        shimeBi = string.Empty;
                    }

                    // 請求日/精算日
                    if (!this.IsDBNull(dataRow.ItemArray[indexSeikyuuSeisanDate]))
                    {
                        seikyuuSeisanDate = ((DateTime)dataRow.ItemArray[indexSeikyuuSeisanDate]).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        seikyuuSeisanDate = string.Empty;
                    }

                    // 今回調整額
                    if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiChouseigaku]))
                    {
                        konkaiChouseigaku = (decimal)dataRow.ItemArray[indexKonkaiChouseigaku];
                    }
                    else
                    {
                        konkaiChouseigaku = 0;
                    }

                    // 今回請求支払額
                    if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiTorihikiGaku]))
                    {
                        konkaiTorihikiGaku = (decimal)dataRow.ItemArray[indexKonkaiTorihikiGaku];
                    }
                    else
                    {
                        konkaiTorihikiGaku = 0;
                    }
                    
                    // 今回請求/精算内税額
                    if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiSeiUchizeiGaku]))
                    {
                        konkaiSeiUchizeiGaku = (decimal)dataRow.ItemArray[indexKonkaiSeiUchizeiGaku];
                    }
                    else
                    {
                        konkaiSeiUchizeiGaku = 0;
                    }

                    // 今回請求/精算外税額
                    if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiSeiSotozeiGaku]))
                    {
                        konkaiSeiSotozeiGaku = (decimal)dataRow.ItemArray[indexKonkaiSeiSotozeiGaku];
                    }
                    else
                    {
                        konkaiSeiSotozeiGaku = 0;
                    }

                    // 今回伝票内税額
                    if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiDenUchizeiGaku]))
                    {
                        konkaiDenUchizeiGaku = (decimal)dataRow.ItemArray[indexKonkaiDenUchizeiGaku];
                    }
                    else
                    {
                        konkaiDenUchizeiGaku = 0;
                    }

                    // 今回伝票外税額
                    if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiDenSotozeiGaku]))
                    {
                        konkaiDenSotozeiGaku = (decimal)dataRow.ItemArray[indexKonkaiDenSotozeiGaku];
                    }
                    else
                    {
                        konkaiDenSotozeiGaku = 0;
                    }

                    // 今回明細内税額
                    if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiMeiUchizeiGaku]))
                    {
                        konkaiMeiUchizeiGaku = (decimal)dataRow.ItemArray[indexKonkaiMeiUchizeiGaku];
                    }
                    else
                    {
                        konkaiMeiUchizeiGaku = 0;
                    }

                    // 今回明細外税額
                    if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiMeiSotozeiGaku]))
                    {
                        konkaiMeiSotozeiGaku = (decimal)dataRow.ItemArray[indexKonkaiMeiSotozeiGaku];
                    }
                    else
                    {
                        konkaiMeiSotozeiGaku = 0;
                    }

                    // 総合計
                    sougoukei += (konkaiTorihikiGaku + konkaiSeiUchizeiGaku + konkaiSeiSotozeiGaku + konkaiDenUchizeiGaku + konkaiDenSotozeiGaku + konkaiMeiUchizeiGaku + konkaiMeiSotozeiGaku);

                    decimal YoteiGaku = (konkaiTorihikiGaku + konkaiSeiUchizeiGaku + konkaiSeiSotozeiGaku + konkaiDenUchizeiGaku + konkaiDenSotozeiGaku + konkaiMeiUchizeiGaku + konkaiMeiSotozeiGaku);

                    if (YoteiGaku == 0)
                    {   //予定額が0の場合は入金・出金の予定無しのため、表示対象外
                        continue;
                    }

                    row["PHY_NYUUKIN_YOTEI_BI_VLB"] = nyuukinShiharaiYoteiBiNext;
                    row["PHY_TORIHIKISAKI_CD_VLB"] = torihikisakiCD;
                    row["PHY_TORIHIKISAKI_NAME_VLB"] = torihikisakiCDName;
                    row["PHY_KONKAI_NYUUKIN_GAKU_VLB"] = (konkaiTorihikiGaku + konkaiSeiUchizeiGaku + konkaiSeiSotozeiGaku + konkaiDenUchizeiGaku + konkaiDenSotozeiGaku + konkaiMeiUchizeiGaku + konkaiMeiSotozeiGaku);
                    row["PHY_SHIMEBI_VLB"] = shimeBi;
                    row["PHY_SEIKYUU_DATE_VLB"] = seikyuuSeisanDate;
                    row["PHN_SOUGOUKEI_VLB"] = sougoukei;

                    this.ChouhyouDataTable.Rows.Add(row);
                }
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        #endregion - Methods -
    }

    #endregion - CommonChouhyouR370_R377 -

    #endregion - Classes -
}
