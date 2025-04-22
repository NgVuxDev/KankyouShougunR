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

    #region - CommonChouhyouR379_R384 -

    /// <summary>帳票Nを表すクラス・コントロール</summary>
    public class CommonChouhyouR379_R384 : CommonChouhyouBase
    {
        #region - Const -
        // 請求/精算形態区分 - 単月
        private const string KEITAI_KBN_TANGETSU = "1";
        // 請求/精算形態区分 - 繰越
        private const string KEITAI_KBN_KURIKOSHI = "2";
        #endregion - Const -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="CommonChouhyouR379_R384"/> class.</summary>
        /// <param name="windowID">画面ＩＤ</param>
        public CommonChouhyouR379_R384(WINDOW_ID windowID)
            : base(windowID)
        {
            // 帳票出力フルパスフォーム名
            this.OutputFormFullPathName = CommonChouhyouBase.TemplatePath + "R379_R384-Form.xml";

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";

            // 選択可能な集計項目グループ数
            this.SelectEnableSyuukeiKoumokuGroupCount = 1;

            // 選択可能な集計項目
            this.SelectEnableSyuukeiKoumokuList = new List<int>()
            {
                0, 1,
            };

            if (windowID == WINDOW_ID.R_SEIKYUU_MEISAIHYOU)
            {   // R379(請求明細表)

                // 対象テーブルリスト
                this.TaishouTableList = new List<TaishouTable>()
                {
                    new TaishouTable("T_SEIKYUU_DENPYOU"),
                };
            }
            else if (windowID == WINDOW_ID.R_SHIHARAIMEISAI_MEISAIHYOU)
            {   // R384(支払明細明細表)

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

                DataRow dataRow = this.InputDataTable[0].Rows[0];

                string sql;

                this.ChouhyouDataTable.Columns.Add("PHY_TORIHIKISAKI_CD_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_TORIHIKISAKI_NAME_RYAKU_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_SHIMEBI_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_ZENKAI_KURIKOSI_GAKU_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KONKAI_NYUUKIN_GAKU_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KONKAI_CHOUSEI_GAKU_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KURIKOSI_GAKU_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KONKAI_URIAGE_GAKU_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_SHOUHIZEI_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KONKAI_TORIHIKI_GAKU_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KONKAI_KURIKOSI_GAKU_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_NYUUKIN_YOTEI_BI_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_SEIKYUU_DATE_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_ZENKAI_KURIKOSI_GAKU_TOTAL_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KONKAI_NYUUKIN_GAKU_TOTAL_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KONKAI_CHOUSEI_GAKU_TOTAL_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KURIKOSI_GAKU_TOTAL_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KONKAI_URIAGE_GAKU_TOTAL_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_SHOUHIZEI_TOTAL_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KONKAI_TORIHIKI_GAKU_TOTAL_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KONKAI_KURIKOSI_GAKU_TOTAL_VLB");

                int index;

                // 請求先CD/取引先CD
                string torihikisakiCDPrev = "DEFAULT";
                string torihikisakiCDNext = string.Empty;

                // 取引先CD名(M_TORIHIKISAKI)
                string torihikisakiCDName = string.Empty;

                // 締日
                string shimeBi = string.Empty;

                // 前回請求額/前回精算額
                decimal zenkaiKurikoshiGaku = 0;
                decimal zenkaiKurikoshiGakuTotal = 0;

                // 入金額/出金額
                decimal konkaiNyuukinSyutsukinGaku = 0;
                decimal konkaiNyuukinSyutsukinGakuTotal = 0;

                // 調整額
                decimal konkaiChouseigaku = 0;
                decimal konkaiChouseigakuTotal = 0;

                // 繰越額
                decimal KurikosiGaku = 0;
                decimal KurikosiGakuTotal = 0;

                // 今回取引額(税抜)
                decimal KonkaiUriageGaku = 0;
                decimal KonkaiUriageGakuTotal = 0;

                // 消費税
                decimal Shouhizei = 0;
                decimal ShouhizeiTotal = 0;

                // 今回取引額
                decimal KonkaiTorihikiGaku = 0;
                decimal KonkaiTorihikiGakuTotal = 0;

                // 今回御請求額
                decimal KonkaiKurikosiGaku = 0;
                decimal KonkaiKurikosiGakuTotal = 0;

                // 入金予定日/出金予定日
                string nyuukinnSyutsukinYoteibi = string.Empty;

                // 請求年月日/支払年月日
                string seikyuuSeisanbi = string.Empty;

                // 今回請/精内税額
                decimal konkaiSeiUtizeiGaku = 0;

                // 今回請/精外税額
                decimal konaiSeiSotozeiGaku = 0;

                // 今回伝内税額
                decimal konkaiDenUtizeiGaku = 0;

                // 今回伝外税額
                decimal konkaiDenSotozeiGaku = 0;

                // 今回明内税額
                decimal konkaiMeiUtizeiGaku = 0;

                // 今回明外税額
                decimal konkaiMeiSotozeiGaku = 0;

                for (int i = 0; i < this.InputDataTable[0].Rows.Count; i++)
                {
                    dataRow = this.InputDataTable[0].Rows[i];

                    DataRow row = this.ChouhyouDataTable.NewRow();

                    // 請求先CD/取引先CD
                    index = this.InputDataTable[0].Columns.IndexOf("TORIHIKISAKI_CD");
                    if (!this.IsDBNull(dataRow.ItemArray[index]))
                    {
                        torihikisakiCDNext = (string)dataRow.ItemArray[index];
                    }
                    else
                    {
                        torihikisakiCDNext = string.Empty;
                    }

                    if (torihikisakiCDPrev != torihikisakiCDNext)
                    {   // 取引先コードが変化
                        torihikisakiCDPrev = torihikisakiCDNext;

                        // 前回請求額/前回精算額
                        zenkaiKurikoshiGaku = 0;

                        // 入金額/出金額
                        konkaiNyuukinSyutsukinGaku = 0;

                        // 調整額
                        konkaiChouseigaku = 0;

                        // 繰越額
                        KurikosiGaku = 0;

                        // 今回取引額(税抜)
                        KonkaiUriageGaku = 0;

                        // 消費税
                        Shouhizei = 0;

                        // 今回取引額
                        KonkaiTorihikiGaku = 0;

                        // 今回御請求額
                        KonkaiKurikosiGaku = 0;
                    }

                    // 請求/支払形態区分
                    if (this.WindowID == WINDOW_ID.R_SEIKYUU_MEISAIHYOU)
                    {   // R379(請求明細表)
                        index = this.InputDataTable[0].Columns.IndexOf("SEIKYUU_KEITAI_KBN");
                    }
                    else if (this.WindowID == WINDOW_ID.R_SHIHARAIMEISAI_MEISAIHYOU)
                    {   // R384(支払明細明細表)
                        index = this.InputDataTable[0].Columns.IndexOf("SHIHARAI_KEITAI_KBN");
                    }

                    // 取引先CD名(M_TORIHIKISAKI)
                    sql = string.Format("SELECT M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU FROM M_TORIHIKISAKI WHERE TORIHIKISAKI_CD = '{0}'", torihikisakiCDNext);
                    DataTable dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                    index = dataTableTmp.Columns.IndexOf("TORIHIKISAKI_NAME_RYAKU");
                    if (dataTableTmp.Rows.Count != 0)
                    {
                        torihikisakiCDName = (string)dataTableTmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        torihikisakiCDName = string.Empty;
                    }

                    // 締日
                    index = this.InputDataTable[0].Columns.IndexOf("SHIMEBI");
                    if (!this.IsDBNull(dataRow.ItemArray[index]))
                    {
                        shimeBi = dataRow.ItemArray[index].ToString();
                    }
                    else
                    {
                        shimeBi = string.Empty;
                    }

                    // 前回請求額/前回精算額
                    index = this.InputDataTable[0].Columns.IndexOf("ZENKAI_KURIKOSI_GAKU");
                    // 20141126 teikyou 単月請求の場合も繰越請求と同様の形で表示する　start
                    if (!this.IsDBNull(dataRow.ItemArray[index]))
                    {
                        // 請求/精算形態区分が繰越の場合
                        zenkaiKurikoshiGaku = (decimal)dataRow.ItemArray[index];
                    }
                    else
                    {
                        // 請求/精算形態区分が単月の場合
                        zenkaiKurikoshiGaku = 0;
                    }
                    // 20141126 teikyou 単月請求の場合も繰越請求と同様の形で表示する　end
                    zenkaiKurikoshiGakuTotal += Math.Round(zenkaiKurikoshiGaku, MidpointRounding.AwayFromZero);

                    // 入金額/出金額
                    if (this.WindowID == WINDOW_ID.R_SEIKYUU_MEISAIHYOU)
                    {   // R379(請求明細表)
                        index = this.InputDataTable[0].Columns.IndexOf("KONKAI_NYUUKIN_GAKU");
                    }
                    else if (this.WindowID == WINDOW_ID.R_SHIHARAIMEISAI_MEISAIHYOU)
                    {   // R384(支払明細明細表)
                        index = this.InputDataTable[0].Columns.IndexOf("KONKAI_SHUKKIN_GAKU");
                    }
                    if (!this.IsDBNull(dataRow.ItemArray[index]))
                    {
                        // 請求/精算形態区分が繰越の場合
                        konkaiNyuukinSyutsukinGaku = (decimal)dataRow.ItemArray[index];
                    }
                    else
                    {
                        // 請求/精算形態区分が単月の場合
                        konkaiNyuukinSyutsukinGaku = 0;
                    }

                    konkaiNyuukinSyutsukinGakuTotal += Math.Round(konkaiNyuukinSyutsukinGaku, MidpointRounding.AwayFromZero);

                    // 調整額
                    index = this.InputDataTable[0].Columns.IndexOf("KONKAI_CHOUSEI_GAKU");
                    if (!this.IsDBNull(dataRow.ItemArray[index]))
                    {
                        konkaiChouseigaku = (decimal)dataRow.ItemArray[index];
                    }
                    else
                    {
                        konkaiChouseigaku = 0;
                    }

                    konkaiChouseigakuTotal += Math.Round(konkaiChouseigaku, MidpointRounding.AwayFromZero);

                    // 繰越額 = 前回請求/精算額 - 入金/出金額 - 調整額
                    // 20141126 teikyou 単月請求の場合も繰越請求と同様の形で表示する　start
                    KurikosiGaku = zenkaiKurikoshiGaku - konkaiNyuukinSyutsukinGaku - konkaiChouseigaku;
                    // 20141126 teikyou 単月請求の場合も繰越請求と同様の形で表示する　end
                    KurikosiGakuTotal += Math.Round(KurikosiGaku, MidpointRounding.AwayFromZero);

                    // 今回取引額(税抜)
                    if (this.WindowID == WINDOW_ID.R_SEIKYUU_MEISAIHYOU)
                    {   // R379(請求明細表)
                        index = this.InputDataTable[0].Columns.IndexOf("KONKAI_URIAGE_GAKU");
                    }
                    else if (this.WindowID == WINDOW_ID.R_SHIHARAIMEISAI_MEISAIHYOU)
                    {   // R384(支払明細明細表)
                        index = this.InputDataTable[0].Columns.IndexOf("KONKAI_SHIHARAI_GAKU");
                    }
                    if (!this.IsDBNull(dataRow.ItemArray[index]))
                    {
                        // 請求/精算形態区分が繰越の場合
                        KonkaiUriageGaku = (decimal)dataRow.ItemArray[index];
                    }
                    else
                    {
                        // 請求/精算形態区分が単月の場合
                        KonkaiUriageGaku = 0;
                    }

                    KonkaiUriageGakuTotal += Math.Round(KonkaiUriageGaku, MidpointRounding.AwayFromZero);
                    #region 消費税
                    // 今回請/精内税額
                    index = this.InputDataTable[0].Columns.IndexOf("KONKAI_SEI_UTIZEI_GAKU");
                    if (!this.IsDBNull(dataRow.ItemArray[index]))
                    {
                        konkaiSeiUtizeiGaku = (decimal)dataRow.ItemArray[index];
                    }
                    else
                    {
                        konkaiSeiUtizeiGaku = 0;
                    }

                    // 今回請/精外税額
                    index = this.InputDataTable[0].Columns.IndexOf("KONKAI_SEI_SOTOZEI_GAKU");
                    if (!this.IsDBNull(dataRow.ItemArray[index]))
                    {
                        konaiSeiSotozeiGaku = (decimal)dataRow.ItemArray[index];
                    }
                    else
                    {
                        konaiSeiSotozeiGaku = 0;
                    }

                    // 今回伝内税額
                    index = this.InputDataTable[0].Columns.IndexOf("KONKAI_DEN_UTIZEI_GAKU");
                    if (!this.IsDBNull(dataRow.ItemArray[index]))
                    {
                        konkaiDenUtizeiGaku = (decimal)dataRow.ItemArray[index];
                    }
                    else
                    {
                        konkaiDenUtizeiGaku = 0;
                    }

                    // 今回伝外税額
                    index = this.InputDataTable[0].Columns.IndexOf("KONKAI_DEN_SOTOZEI_GAKU");
                    if (!this.IsDBNull(dataRow.ItemArray[index]))
                    {
                        konkaiDenSotozeiGaku = (decimal)dataRow.ItemArray[index];
                    }
                    else
                    {
                        konkaiDenSotozeiGaku = 0;
                    }

                    // 今回明内税額
                    index = this.InputDataTable[0].Columns.IndexOf("KONKAI_MEI_UTIZEI_GAKU");
                    if (!this.IsDBNull(dataRow.ItemArray[index]))
                    {
                        konkaiMeiUtizeiGaku = (decimal)dataRow.ItemArray[index];
                    }
                    else
                    {
                        konkaiMeiUtizeiGaku = 0;
                    }

                    // 今回明外税額
                    index = this.InputDataTable[0].Columns.IndexOf("KONKAI_MEI_SOTOZEI_GAKU");
                    if (!this.IsDBNull(dataRow.ItemArray[index]))
                    {
                        konkaiMeiSotozeiGaku = (decimal)dataRow.ItemArray[index];
                    }
                    else
                    {
                        konkaiMeiSotozeiGaku = 0;
                    }
                    #endregion
                    // 消費税
                    // 20141126 teikyou 単月請求の場合も繰越請求と同様の形で表示する　start
                    Shouhizei = konkaiSeiUtizeiGaku + konaiSeiSotozeiGaku + konkaiDenUtizeiGaku
                         + konkaiDenSotozeiGaku + konkaiMeiUtizeiGaku + konkaiMeiSotozeiGaku;

                    ShouhizeiTotal += Math.Round(Shouhizei, MidpointRounding.AwayFromZero);

                    // 今回取引額 ＝ 今回取引額(税抜) ＋ 消費税
                    // 20141126 teikyou 今回取引額の計算が今回取引額＝今回取引額(税抜)＋消費税とする　start
                    KonkaiTorihikiGaku = KonkaiUriageGaku + Shouhizei;
                    // 20141126 teikyou 今回取引額の計算が今回取引額＝今回取引額(税抜)＋消費税とする　end

                    // 20141126 teikyou 単月請求の場合も繰越請求と同様の形で表示する　end
                    KonkaiTorihikiGakuTotal += Math.Round(KonkaiTorihikiGaku, MidpointRounding.AwayFromZero);

                    // 今回御請求額
                    KonkaiKurikosiGaku = KurikosiGaku + KonkaiTorihikiGaku;

                    KonkaiKurikosiGakuTotal += Math.Round(KonkaiKurikosiGaku, MidpointRounding.AwayFromZero);

                    // 入金予定日/出金予定日
                    if (this.WindowID == WINDOW_ID.R_SEIKYUU_MEISAIHYOU)
                    {   // R379(請求明細表)
                        index = this.InputDataTable[0].Columns.IndexOf("NYUUKIN_YOTEI_BI");
                    }
                    else if (this.WindowID == WINDOW_ID.R_SHIHARAIMEISAI_MEISAIHYOU)
                    {   // R384(支払明細明細表)
                        index = this.InputDataTable[0].Columns.IndexOf("SHUKKIN_YOTEI_BI");
                    }

                    if (!this.IsDBNull(dataRow.ItemArray[index]))
                    {
                        nyuukinnSyutsukinYoteibi = ((DateTime)dataRow.ItemArray[index]).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        nyuukinnSyutsukinYoteibi = string.Empty;
                    }

                    // 締実行日
                    if (this.WindowID == WINDOW_ID.R_SEIKYUU_MEISAIHYOU)
                    {   // R379(請求明細表)
                        index = this.InputDataTable[0].Columns.IndexOf("SEIKYUU_DATE");
                    }
                    else if (this.WindowID == WINDOW_ID.R_SHIHARAIMEISAI_MEISAIHYOU)
                    {   // R384(支払明細明細表)
                        index = this.InputDataTable[0].Columns.IndexOf("SEISAN_DATE");
                    }

                    if (!this.IsDBNull(dataRow.ItemArray[index]))
                    {
                        seikyuuSeisanbi = ((DateTime)dataRow.ItemArray[index]).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        seikyuuSeisanbi = string.Empty;
                    }

                    row["PHY_TORIHIKISAKI_CD_VLB"] = torihikisakiCDNext;
                    row["PHY_TORIHIKISAKI_NAME_RYAKU_VLB"] = torihikisakiCDName;
                    row["PHY_SHIMEBI_VLB"] = shimeBi;
                    row["PHY_ZENKAI_KURIKOSI_GAKU_VLB"] = zenkaiKurikoshiGaku;
                    row["PHY_KONKAI_NYUUKIN_GAKU_VLB"] = konkaiNyuukinSyutsukinGaku;
                    row["PHY_KONKAI_CHOUSEI_GAKU_VLB"] = konkaiChouseigaku;
                    row["PHY_KURIKOSI_GAKU_VLB"] = KurikosiGaku;
                    row["PHY_KONKAI_URIAGE_GAKU_VLB"] = KonkaiUriageGaku;
                    row["PHY_SHOUHIZEI_VLB"] = Shouhizei;
                    row["PHY_KONKAI_TORIHIKI_GAKU_VLB"] = KonkaiTorihikiGaku;
                    row["PHY_KONKAI_KURIKOSI_GAKU_VLB"] = KonkaiKurikosiGaku;
                    row["PHY_NYUUKIN_YOTEI_BI_VLB"] = nyuukinnSyutsukinYoteibi;
                    row["PHY_SEIKYUU_DATE_VLB"] = seikyuuSeisanbi;
                    row["PHN_ZENKAI_KURIKOSI_GAKU_TOTAL_VLB"] = zenkaiKurikoshiGakuTotal;
                    row["PHN_KONKAI_NYUUKIN_GAKU_TOTAL_VLB"] = konkaiNyuukinSyutsukinGakuTotal;
                    row["PHN_KONKAI_CHOUSEI_GAKU_TOTAL_VLB"] = konkaiChouseigakuTotal;
                    row["PHN_KURIKOSI_GAKU_TOTAL_VLB"] = KurikosiGakuTotal;
                    row["PHN_KONKAI_URIAGE_GAKU_TOTAL_VLB"] = KonkaiUriageGakuTotal;
                    row["PHN_SHOUHIZEI_TOTAL_VLB"] = ShouhizeiTotal;
                    row["PHN_KONKAI_TORIHIKI_GAKU_TOTAL_VLB"] = KonkaiTorihikiGakuTotal;
                    row["PHN_KONKAI_KURIKOSI_GAKU_TOTAL_VLB"] = KonkaiKurikosiGakuTotal;

                    var rowCount = Int16.Parse(dataRow.ItemArray[this.InputDataTable[0].Columns.IndexOf("ROW_COUNT")].ToString());
                    if (rowCount > 0 || konkaiChouseigaku != 0 || konkaiNyuukinSyutsukinGaku != 0 || zenkaiKurikoshiGaku != 0)
                    {
                        this.ChouhyouDataTable.Rows.Add(row);
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

    #endregion - CommonChouhyouR379_R384 -

    #endregion - Classes -
}
