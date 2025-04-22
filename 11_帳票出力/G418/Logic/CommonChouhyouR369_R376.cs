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

    #region - CommonChouhyouR369_R376 -

    /// <summary>帳票Nを表すクラス・コントロール</summary>
    public class CommonChouhyouR369_R376 : CommonChouhyouBase
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="CommonChouhyouR369_R376"/> class.</summary>
        /// <param name="windowID">画面ＩＤ</param>
        public CommonChouhyouR369_R376(WINDOW_ID windowID)
            : base(windowID)
        {
            // 帳票出力フルパスフォーム名
            this.OutputFormFullPathName = CommonChouhyouBase.TemplatePath + "R369_R376-Form.xml";

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";

            // 選択可能な集計項目グループ数
            this.SelectEnableSyuukeiKoumokuGroupCount = 1;

            // 選択可能な集計項目
            this.SelectEnableSyuukeiKoumokuList = new List<int>()
            {
                0, 1, 9,
            };

            if (windowID == WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU)
            {   // R369(未入金一覧表)

                // 対象テーブルリスト
                this.TaishouTableList = new List<TaishouTable>()
                {
                    new TaishouTable("T_SEIKYUU_DENPYOU"),
                };
            }
            else if (windowID == WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU)
            {   // R376(未出金一覧表)

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

                // 取引先CDインデックス
                int indexTorihikisakiCD = this.InputDataTable[0].Columns.IndexOf("TORIHIKISAKI_CD");

                DataRow dataRow = this.InputDataTable[0].Rows[0];

                // 入金予定日/支払予定日
                int indexNyuukinShiharaiYoteiBi = 0;
                if (this.WindowID == WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU)
                {   // R369(未入金一覧表)
                    indexNyuukinShiharaiYoteiBi = this.InputDataTable[0].Columns.IndexOf("NYUUKIN_YOTEI_BI");
                }
                else if (this.WindowID == WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU)
                {   // R376(未出金一覧表)
                    indexNyuukinShiharaiYoteiBi = this.InputDataTable[0].Columns.IndexOf("SHUKKIN_YOTEI_BI");
                }

                string nyuukinYoteiBi;
                if (!this.IsDBNull(dataRow.ItemArray[indexNyuukinShiharaiYoteiBi]))
                {
                    nyuukinYoteiBi = ((DateTime)dataRow.ItemArray[indexNyuukinShiharaiYoteiBi]).ToString("yyyy/MM/dd");
                }
                else
                {
                    nyuukinYoteiBi = string.Empty;
                }

                // 取引先CD
                string torihikisakiCDPrev;
                if (!this.IsDBNull(dataRow.ItemArray[indexTorihikisakiCD]))
                {
                    torihikisakiCDPrev = (string)dataRow.ItemArray[indexTorihikisakiCD];
                }
                else
                {
                    torihikisakiCDPrev = string.Empty;
                }

                string torihikisakiCDNext = torihikisakiCDPrev;

                // 営業担当者CDインデックス
                int indexEigyouTantoushaCD = this.InputDataTable[0].Columns.IndexOf("EIGYOU_TANTOU_CD");
                string eigyouTantoushaCD = string.Empty;
                int indexEigyouTantoushaCDName;
                string eigyouTantoushaCDName = string.Empty;
                DataTable dataTableTmp;

                // 取引先CD名(M_TORIHIKISAKI)
                sql = string.Format("SELECT M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU FROM M_TORIHIKISAKI WHERE TORIHIKISAKI_CD = '{0}'", torihikisakiCDPrev);
                dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);
                string torihikisakiCDName = string.Empty;
                int indexTorihikisakiCDName = dataTableTmp.Columns.IndexOf("TORIHIKISAKI_NAME_RYAKU");
                if (!this.IsDBNull(dataTableTmp.Rows[0].ItemArray[indexTorihikisakiCDName]))
                {
                    torihikisakiCDName = (string)dataTableTmp.Rows[0].ItemArray[indexTorihikisakiCDName];
                }
                else
                {
                    torihikisakiCDName = string.Empty;
                }

                int item = this.SelectSyuukeiKoumokuList[0];
                SyuukeiKoumoku syuukeiKoumoku = this.SyuukeiKomokuList[item];

                if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.EigyoTantoshaBetsu)
                {   // 営業担当者別

                    // 営業担当者CD
                    indexEigyouTantoushaCD = this.InputDataTable[0].Columns.IndexOf("EIGYOU_TANTOU_CD");
                    if (!this.IsDBNull(dataRow.ItemArray[indexEigyouTantoushaCD]))
                    {
                        eigyouTantoushaCD = (string)dataRow.ItemArray[indexEigyouTantoushaCD];

                        // 営業担当者CD名(M_TORIHIKISAKI, M_SHAIN)取得
                        sql = string.Format("SELECT M_TORIHIKISAKI.EIGYOU_TANTOU_CD, M_SHAIN.SHAIN_NAME_RYAKU FROM M_TORIHIKISAKI, M_SHAIN WHERE M_TORIHIKISAKI.EIGYOU_TANTOU_CD = '{0}' AND M_SHAIN.SHAIN_CD = '{1}'", eigyouTantoushaCD, eigyouTantoushaCD);
                        dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                        eigyouTantoushaCDName = string.Empty;
                        indexEigyouTantoushaCDName = dataTableTmp.Columns.IndexOf("SHAIN_NAME_RYAKU");
                        if (dataTableTmp.Rows.Count != 0)
                        {
                            if (!this.IsDBNull(dataTableTmp.Rows[0].ItemArray[indexEigyouTantoushaCDName]))
                            {
                                eigyouTantoushaCDName = (string)dataTableTmp.Rows[0].ItemArray[indexEigyouTantoushaCDName];
                            }
                            else
                            {
                                eigyouTantoushaCDName = string.Empty;
                            }
                        }
                    }
                    else
                    {
                        eigyouTantoushaCD = string.Empty;
                        eigyouTantoushaCDName = string.Empty;
                    }
                }

                // 今回売上額/今回支払額
                int indexKonkaiUriageShiharaiGaku = 0;
                if (this.WindowID == WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU)
                {   // R369(未入金一覧表)
                    indexKonkaiUriageShiharaiGaku = this.InputDataTable[0].Columns.IndexOf("KONKAI_URIAGE_GAKU");
                }
                else if (this.WindowID == WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU)
                {   // R376(未出金一覧表)
                    indexKonkaiUriageShiharaiGaku = this.InputDataTable[0].Columns.IndexOf("KONKAI_SHIHARAI_GAKU");
                }

                decimal konkaiUriageShiharaiGaku;
                if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiUriageShiharaiGaku]))
                {
                    konkaiUriageShiharaiGaku = (decimal)dataRow.ItemArray[indexKonkaiUriageShiharaiGaku];
                }
                else
                {
                    konkaiUriageShiharaiGaku = 0;
                }

                // 今回請内税額
                int indexKonkaiSeiUchizeiGaku = this.InputDataTable[0].Columns.IndexOf("KONKAI_SEI_UTIZEI_GAKU");
                decimal konkaiSeiUchizeiGaku;
                if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiSeiUchizeiGaku]))
                {
                    konkaiSeiUchizeiGaku = (decimal)dataRow.ItemArray[indexKonkaiSeiUchizeiGaku];
                }
                else
                {
                    konkaiSeiUchizeiGaku = 0;
                }

                // 今回請外税額
                int indexKonkaiSeiSotoGaku = this.InputDataTable[0].Columns.IndexOf("KONKAI_SEI_SOTOZEI_GAKU");
                decimal konkaiSeiSotoGaku;
                if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiSeiSotoGaku]))
                {
                    konkaiSeiSotoGaku = (decimal)dataRow.ItemArray[indexKonkaiSeiSotoGaku];
                }
                else
                {
                    konkaiSeiSotoGaku = 0;
                }

                // 今回伝票内税額
                int indexKonkaiDepyouUchizeiGaku = this.InputDataTable[0].Columns.IndexOf("KONKAI_DEN_UTIZEI_GAKU");
                decimal konkaiDepyouUchizeiGaku;
                if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiDepyouUchizeiGaku]))
                {
                    konkaiDepyouUchizeiGaku = (decimal)dataRow.ItemArray[indexKonkaiDepyouUchizeiGaku];
                }
                else
                {
                    konkaiDepyouUchizeiGaku = 0;
                }

                // 今回伝票内税額
                int indexKonkaiDepyouSotozeiGaku = this.InputDataTable[0].Columns.IndexOf("KONKAI_DEN_SOTOZEI_GAKU");
                decimal konkaiDepyouSotozeiGaku;
                if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiDepyouSotozeiGaku]))
                {
                    konkaiDepyouSotozeiGaku = (decimal)dataRow.ItemArray[indexKonkaiDepyouSotozeiGaku];
                }
                else
                {
                    konkaiDepyouSotozeiGaku = 0;
                }

                // 今回明細内税額
                int indexKonkaiMeisaiUchizeiGaku = this.InputDataTable[0].Columns.IndexOf("KONKAI_MEI_UTIZEI_GAKU");
                decimal konkaiMeisaiUchizeiGaku;
                if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiMeisaiUchizeiGaku]))
                {
                    konkaiMeisaiUchizeiGaku = (decimal)dataRow.ItemArray[indexKonkaiMeisaiUchizeiGaku];
                }
                else
                {
                    konkaiMeisaiUchizeiGaku = 0;
                }

                // 今回明細外税額
                int indexKonkaiMeisaiSotozeiGaku = this.InputDataTable[0].Columns.IndexOf("KONKAI_MEI_SOTOZEI_GAKU");
                decimal konkaiMeisaiSotozeiGaku;
                if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiMeisaiSotozeiGaku]))
                {
                    konkaiMeisaiSotozeiGaku = (decimal)dataRow.ItemArray[indexKonkaiMeisaiSotozeiGaku];
                }
                else
                {
                    konkaiMeisaiSotozeiGaku = 0;
                }

                // 請求額/支払額
                decimal konkaiSeikyuuShiharaiGaku = konkaiUriageShiharaiGaku +
                    konkaiSeiUchizeiGaku + konkaiSeiSotoGaku +
                    konkaiDepyouUchizeiGaku + konkaiDepyouSotozeiGaku +
                    konkaiMeisaiUchizeiGaku + konkaiMeisaiSotozeiGaku;

                int indexKeshikomiGaku = -1;
                decimal konkaiNyuukinGaku = 0;
                int indexZenkaiKurikoshiGaku = -1;
                decimal zenkaiKurikoshiGaku = 0;
                decimal minyuukinGaku = 0;

                int indexSeikyuuSeisanNo;
                if (this.WindowID == WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU)
                {   // R369(未入金一覧表)
                    indexSeikyuuSeisanNo = this.InputDataTable[0].Columns.IndexOf("SEIKYUU_NUMBER");

                    // 入金消込・消込額(T_SEIKYUU_DENPYOU, T_NYUUKIN_KESHIKOMI, T_SHUKKIN_KESHIKOMI)取得
                    sql = string.Format(
                        "SELECT T_NYUUKIN_KESHIKOMI.KESHIKOMI_GAKU " +
                        "FROM T_SEIKYUU_DENPYOU JOIN T_NYUUKIN_KESHIKOMI ON (T_SEIKYUU_DENPYOU.SEIKYUU_NUMBER = T_NYUUKIN_KESHIKOMI.SEIKYUU_NUMBER) " +
                        "WHERE T_NYUUKIN_KESHIKOMI.DELETE_FLG = 0 AND T_SEIKYUU_DENPYOU.TORIHIKISAKI_CD = T_NYUUKIN_KESHIKOMI.TORIHIKISAKI_CD AND T_NYUUKIN_KESHIKOMI.SEIKYUU_NUMBER = {0}",
                        dataRow[indexSeikyuuSeisanNo]);

                    dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                    // 今回入金額
                    indexKeshikomiGaku = dataTableTmp.Columns.IndexOf("KESHIKOMI_GAKU");
                    if (dataTableTmp.Rows.Count != 0)
                    {
                        konkaiNyuukinGaku = 0;

                        foreach (DataRow dataRow1 in dataTableTmp.Rows)
                        {
                            if (!this.IsDBNull(dataRow1.ItemArray[indexKeshikomiGaku]))
                            {
                                konkaiNyuukinGaku += (decimal)dataRow1.ItemArray[indexKeshikomiGaku];
                            }
                            else
                            {
                                konkaiNyuukinGaku += 0;
                            }
                        }
                    }

                    // 未入金額
                    minyuukinGaku = konkaiSeikyuuShiharaiGaku - konkaiNyuukinGaku;
                }
                else if (this.WindowID == WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU)
                {   // R376(未出金一覧表)
                    indexZenkaiKurikoshiGaku = this.InputDataTable[0].Columns.IndexOf("ZENKAI_KURIKOSI_GAKU");
                    zenkaiKurikoshiGaku = (decimal)dataRow.ItemArray[indexZenkaiKurikoshiGaku];

                    indexSeikyuuSeisanNo = this.InputDataTable[0].Columns.IndexOf("SEISAN_NUMBER");

                    // 出金額(T_SEIKYUU_DENPYOU, T_NYUUKIN_KESHIKOMI, T_SHUKKIN_KESHIKOMI)取得
                    sql = string.Format(
                        "SELECT T_SHUKKIN_KESHIKOMI.KESHIKOMI_GAKU " +
                        "FROM T_SEISAN_DENPYOU JOIN T_SHUKKIN_KESHIKOMI ON (T_SEISAN_DENPYOU.SEISAN_NUMBER = T_SHUKKIN_KESHIKOMI.SEISAN_NUMBER) " +
                        "WHERE T_SHUKKIN_KESHIKOMI.DELETE_FLG = 0 AND T_SHUKKIN_KESHIKOMI.SEISAN_NUMBER = {0}",
                        dataRow[indexSeikyuuSeisanNo]);
                    dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                    // 未入金額
                    indexKeshikomiGaku = dataTableTmp.Columns.IndexOf("KESHIKOMI_GAKU");
                    if (dataTableTmp.Rows.Count != 0)
                    {
                        konkaiNyuukinGaku = 0;

                        foreach (DataRow dataRow1 in dataTableTmp.Rows)
                        {
                            if (!this.IsDBNull(dataRow1.ItemArray[indexKeshikomiGaku]))
                            {
                                if (!this.IsDBNull(dataRow1.ItemArray[indexKeshikomiGaku]))
                                {
                                    konkaiNyuukinGaku += (decimal)dataRow1.ItemArray[indexKeshikomiGaku];
                                }
                                else
                                {
                                    konkaiNyuukinGaku += 0;
                                }
                            }
                        }
                    }

                    minyuukinGaku = konkaiSeikyuuShiharaiGaku - konkaiNyuukinGaku;

                    konkaiNyuukinGaku = 0;
                }

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
                if (this.WindowID == WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU)
                {   // R369(未入金一覧表)
                    indexSeikyuuSeisanDate = this.InputDataTable[0].Columns.IndexOf("SEIKYUU_DATE");
                }
                else if (this.WindowID == WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU)
                {   // R376(未出金一覧表)
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

                // 入出金区分
                if (this.WindowID == WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU)
                {   // R369(未入金一覧表)
                    sql = "SELECT M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME_RYAKU " +
                          "FROM M_TORIHIKISAKI_SEIKYUU JOIN M_NYUUSHUKKIN_KBN ON (M_TORIHIKISAKI_SEIKYUU.KAISHUU_HOUHOU = M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_CD) " +
                          "WHERE M_TORIHIKISAKI_SEIKYUU.KAISHUU_HOUHOU = M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_CD";
                }
                else if (this.WindowID == WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU)
                {   // R376(未出金一覧表)
                    sql = "SELECT M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME_RYAKU " +
                          "FROM M_TORIHIKISAKI_SHIHARAI JOIN M_NYUUSHUKKIN_KBN ON (M_TORIHIKISAKI_SHIHARAI.SHIHARAI_HOUHOU = M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_CD) " +
                          "WHERE M_TORIHIKISAKI_SHIHARAI.SHIHARAI_HOUHOU = M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_CD";
                }

                dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);
                string nyuuSyukkinKubun = string.Empty;
                int indexNyuuSyukkinKubun = dataTableTmp.Columns.IndexOf("NYUUSHUKKIN_KBN_NAME_RYAKU");
                if (dataTableTmp.Rows.Count != 0)
                {
                    nyuuSyukkinKubun = (string)dataTableTmp.Rows[0].ItemArray[indexNyuuSyukkinKubun];
                }
                else
                {
                    nyuuSyukkinKubun = string.Empty;
                }

                // 今回請求額総合計
                decimal konkaiSeikyuuGakuTotal = 0;

                // 今回入金額総合計
                decimal konkaiNyuukinGakuTotal = 0;

                // 今回未入金額総合計
                decimal konkaiMinyuukinGakuTotal = 0;

                this.ChouhyouDataTable.Columns.Add("PHN_TORIHIKISAKI_CD_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_TORIHIKISAKI_NAME_VLB");

                this.ChouhyouDataTable.Columns.Add("PHY_NYUUKIN_YOTEI_BI_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_EIGYOU_TANTOUSHA_CD_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_SHAIN_NAME_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_KONKAI_SEIKYU_GAKU_VLB");

                this.ChouhyouDataTable.Columns.Add("PHY_KONKAI_NYUUKIN_GAKU_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_MI_NYUUKIN_GAKU_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_SHIMEBI_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_SEIKYUU_DATE_VLB");
                this.ChouhyouDataTable.Columns.Add("PHY_NYUUSHUKKIN_KBN_NAME_VLB");

                this.ChouhyouDataTable.Columns.Add("PHN_KONKAI_SEIKYU_GAKU_TOTAL_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_KONKAI_NYUUKIN_GAKU_TOTAL_VLB");
                this.ChouhyouDataTable.Columns.Add("PHN_MI_NYUUKIN_GAKU_TOTAL_VLB");

                for (int i = 0; i < this.InputDataTable[0].Rows.Count; i++)
                {
                    dataRow = this.InputDataTable[0].Rows[i];

                    DataRow row = this.ChouhyouDataTable.NewRow();

                    // 入金予定日/支払予定日
                    if (!this.IsDBNull(dataRow.ItemArray[indexNyuukinShiharaiYoteiBi]))
                    {
                        nyuukinYoteiBi = ((DateTime)dataRow.ItemArray[indexNyuukinShiharaiYoteiBi]).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        nyuukinYoteiBi = string.Empty;
                    }

                    // 取引先CD
                    if (!this.IsDBNull(dataRow.ItemArray[indexTorihikisakiCD]))
                    {
                        torihikisakiCDNext = (string)dataRow.ItemArray[indexTorihikisakiCD];
                    }
                    else
                    {
                        torihikisakiCDNext = string.Empty;
                    }

                    // 営業担当者CD
                    eigyouTantoushaCD = string.Empty;
                    eigyouTantoushaCDName = string.Empty;

                    if (torihikisakiCDPrev != torihikisakiCDNext)
                    {   // 取引先CDが異なる

                        torihikisakiCDPrev = torihikisakiCDNext;

                        // 取引先CD名(M_TORIHIKISAKI)
                        sql = string.Format("SELECT M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU FROM M_TORIHIKISAKI WHERE TORIHIKISAKI_CD = '{0}'", torihikisakiCDPrev);
                        dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);
                        torihikisakiCDName = string.Empty;
                        if (dataTableTmp.Rows.Count != 0)
                        {
                            torihikisakiCDName = (string)dataTableTmp.Rows[0].ItemArray[indexTorihikisakiCDName];
                        }

                        konkaiSeikyuuShiharaiGaku = 0;

                        if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.EigyoTantoshaBetsu)
                        {   // 営業担当者別

                            // 今回請求額総合計
                            konkaiSeikyuuGakuTotal = 0;

                            // 今回入金額総合計
                            konkaiNyuukinGakuTotal = 0;

                            // 今回未入金額総合計
                            konkaiMinyuukinGakuTotal = 0;
                        }
                    }

                    item = this.SelectSyuukeiKoumokuList[0];
                    syuukeiKoumoku = this.SyuukeiKomokuList[item];

                    if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.EigyoTantoshaBetsu)
                    {   // 営業担当者別

                        // 営業担当者CD
                        indexEigyouTantoushaCD = this.InputDataTable[0].Columns.IndexOf("EIGYOU_TANTOU_CD");
                        if (!this.IsDBNull(dataRow.ItemArray[indexEigyouTantoushaCD]))
                        {
                            eigyouTantoushaCD = (string)dataRow.ItemArray[indexEigyouTantoushaCD];

                            if (!eigyouTantoushaCD.Equals(string.Empty))
                            {
                                // 営業担当者CD名(M_TORIHIKISAKI, M_SHAIN)取得
                                sql = string.Format("SELECT M_TORIHIKISAKI.EIGYOU_TANTOU_CD, M_SHAIN.SHAIN_NAME_RYAKU FROM M_TORIHIKISAKI, M_SHAIN WHERE M_TORIHIKISAKI.EIGYOU_TANTOU_CD = '{0}' AND M_SHAIN.SHAIN_CD = '{1}'", eigyouTantoushaCD, eigyouTantoushaCD);
                                dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                                eigyouTantoushaCDName = string.Empty;
                                indexEigyouTantoushaCDName = dataTableTmp.Columns.IndexOf("SHAIN_NAME_RYAKU");
                                if (dataTableTmp.Rows.Count != 0)
                                {
                                    eigyouTantoushaCDName = (string)dataTableTmp.Rows[0].ItemArray[indexEigyouTantoushaCDName];
                                }
                                else
                                {
                                    eigyouTantoushaCDName = string.Empty;
                                }
                            }
                            else
                            {
                                eigyouTantoushaCD = string.Empty;
                                eigyouTantoushaCDName = string.Empty;
                            }
                        }
                        else
                        {
                            eigyouTantoushaCD = string.Empty;
                            eigyouTantoushaCDName = string.Empty;
                        }
                    }

                    // 今回売上額
                    if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiUriageShiharaiGaku]))
                    {
                        konkaiUriageShiharaiGaku = (decimal)dataRow.ItemArray[indexKonkaiUriageShiharaiGaku];
                    }
                    else
                    {
                        konkaiUriageShiharaiGaku = 0;
                    }

                    // 今回請内税額
                    if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiSeiUchizeiGaku]))
                    {
                        konkaiSeiUchizeiGaku = (decimal)dataRow.ItemArray[indexKonkaiSeiUchizeiGaku];
                    }
                    else
                    {
                        konkaiSeiUchizeiGaku = 0;
                    }

                    // 今回請外税額
                    if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiSeiSotoGaku]))
                    {
                        konkaiSeiSotoGaku = (decimal)dataRow.ItemArray[indexKonkaiSeiSotoGaku];
                    }
                    else
                    {
                        konkaiSeiSotoGaku = 0;
                    }

                    // 今回伝票内税額
                    if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiDepyouUchizeiGaku]))
                    {
                        konkaiDepyouUchizeiGaku = (decimal)dataRow.ItemArray[indexKonkaiDepyouUchizeiGaku];
                    }
                    else
                    {
                        konkaiDepyouUchizeiGaku = 0;
                    }

                    // 今回伝票内税額
                    if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiDepyouSotozeiGaku]))
                    {
                        konkaiDepyouSotozeiGaku = (decimal)dataRow.ItemArray[indexKonkaiDepyouSotozeiGaku];
                    }
                    else
                    {
                        konkaiDepyouSotozeiGaku = 0;
                    }

                    // 今回明細内税額
                    if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiMeisaiUchizeiGaku]))
                    {
                        konkaiMeisaiUchizeiGaku = (decimal)dataRow.ItemArray[indexKonkaiMeisaiUchizeiGaku];
                    }
                    else
                    {
                        konkaiMeisaiUchizeiGaku = 0;
                    }

                    // 今回明細外税額
                    if (!this.IsDBNull(dataRow.ItemArray[indexKonkaiMeisaiSotozeiGaku]))
                    {
                        konkaiMeisaiSotozeiGaku = (decimal)dataRow.ItemArray[indexKonkaiMeisaiSotozeiGaku];
                    }
                    else
                    {
                        konkaiMeisaiSotozeiGaku = 0;
                    }

                    // 請求額/支払額
                    konkaiSeikyuuShiharaiGaku = 0;
                    konkaiSeikyuuShiharaiGaku += konkaiUriageShiharaiGaku +
                        konkaiSeiUchizeiGaku + konkaiSeiSotoGaku +
                        konkaiDepyouUchizeiGaku + konkaiDepyouSotozeiGaku +
                        konkaiMeisaiUchizeiGaku + konkaiMeisaiSotozeiGaku;

                    if (this.WindowID == WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU)
                    {   // R369(未入金一覧表)
                        indexSeikyuuSeisanNo = this.InputDataTable[0].Columns.IndexOf("SEIKYUU_NUMBER");

                        // 今回入金額・入金消込・消込額(T_SEIKYUU_DENPYOU, T_NYUUKIN_KESHIKOMI, T_SHUKKIN_KESHIKOMI)取得
                        sql = string.Format(
                            "SELECT T_NYUUKIN_KESHIKOMI.KESHIKOMI_GAKU " +
                            "FROM T_SEIKYUU_DENPYOU JOIN T_NYUUKIN_KESHIKOMI ON (T_SEIKYUU_DENPYOU.SEIKYUU_NUMBER = T_NYUUKIN_KESHIKOMI.SEIKYUU_NUMBER) " +
                            "WHERE T_NYUUKIN_KESHIKOMI.DELETE_FLG = 0 AND T_SEIKYUU_DENPYOU.TORIHIKISAKI_CD = T_NYUUKIN_KESHIKOMI.TORIHIKISAKI_CD AND T_NYUUKIN_KESHIKOMI.SEIKYUU_NUMBER = {0}",
                            dataRow[indexSeikyuuSeisanNo]);
                        dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                        // 今回入金額
                        konkaiNyuukinGaku = 0;
                        if (dataTableTmp.Rows.Count != 0)
                        {
                            foreach (DataRow dataRow1 in dataTableTmp.Rows)
                            {
                                if (!this.IsDBNull(dataRow1.ItemArray[indexKeshikomiGaku]))
                                {
                                    konkaiNyuukinGaku += (decimal)dataRow1.ItemArray[indexKeshikomiGaku];
                                }
                                else
                                {
                                    konkaiNyuukinGaku += 0;
                                }
                            }
                        }

                        // 未入金額
                        minyuukinGaku = konkaiSeikyuuShiharaiGaku - konkaiNyuukinGaku;
                    }
                    else if (this.WindowID == WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU)
                    {   // R376(未出金一覧表)
                        if (!this.IsDBNull(dataRow.ItemArray[indexZenkaiKurikoshiGaku]))
                        {
                            zenkaiKurikoshiGaku = (decimal)dataRow.ItemArray[indexZenkaiKurikoshiGaku];
                        }
                        else
                        {
                            zenkaiKurikoshiGaku = 0;
                        }

                        indexSeikyuuSeisanNo = this.InputDataTable[0].Columns.IndexOf("SEISAN_NUMBER");

                        // 出金額(T_SEIKYUU_DENPYOU, T_NYUUKIN_KESHIKOMI, T_SHUKKIN_KESHIKOMI)取得
                        sql = string.Format(
                            "SELECT T_SHUKKIN_KESHIKOMI.KESHIKOMI_GAKU " +
                            "FROM T_SEISAN_DENPYOU JOIN T_SHUKKIN_KESHIKOMI ON (T_SEISAN_DENPYOU.SEISAN_NUMBER = T_SHUKKIN_KESHIKOMI.SEISAN_NUMBER) " +
                            "WHERE T_SHUKKIN_KESHIKOMI.DELETE_FLG = 0 AND T_SHUKKIN_KESHIKOMI.SEISAN_NUMBER = {0}",
                            dataRow[indexSeikyuuSeisanNo]);
                        dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);

                        // 未入金額
                        if (dataTableTmp.Rows.Count != 0)
                        {
                            indexKeshikomiGaku = dataTableTmp.Columns.IndexOf("KESHIKOMI_GAKU");
                            konkaiNyuukinGaku = 0;

                            foreach (DataRow dataRow1 in dataTableTmp.Rows)
                            {
                                if (!this.IsDBNull(dataRow1.ItemArray[indexKeshikomiGaku]))
                                {
                                    konkaiNyuukinGaku += (decimal)dataRow1.ItemArray[indexKeshikomiGaku];
                                }
                                else
                                {
                                    konkaiNyuukinGaku += 0;
                                }
                            }
                        }

                        minyuukinGaku = konkaiSeikyuuShiharaiGaku - konkaiNyuukinGaku;

                        konkaiNyuukinGaku = 0;
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

                    // 入出金区分
                    if (this.WindowID == WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU)
                    {   // R369(未入金一覧表)
                        sql = "SELECT M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME_RYAKU " +
                              "FROM T_SEIKYUU_DENPYOU, M_TORIHIKISAKI_SEIKYUU, M_NYUUSHUKKIN_KBN " +
                              string.Format("WHERE T_SEIKYUU_DENPYOU.TORIHIKISAKI_CD = '{0}'", torihikisakiCDNext) + " " +
                              string.Format("AND M_TORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD = '{0}'", torihikisakiCDNext) + " " +
                              "AND M_TORIHIKISAKI_SEIKYUU.KAISHUU_HOUHOU = M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_CD";
                    }
                    else if (this.WindowID == WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU)
                    {   // R376(未出金一覧表)
                        sql = "SELECT M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME_RYAKU " +
                              "FROM T_SEISAN_DENPYOU, M_TORIHIKISAKI_SHIHARAI, M_NYUUSHUKKIN_KBN " +
                              string.Format("WHERE T_SEISAN_DENPYOU.TORIHIKISAKI_CD = '{0}'", torihikisakiCDNext) + " " +
                              string.Format("AND M_TORIHIKISAKI_SHIHARAI.TORIHIKISAKI_CD = '{0}'", torihikisakiCDNext) + " " +
                              "AND M_TORIHIKISAKI_SHIHARAI.SHIHARAI_HOUHOU = M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_CD";
                    }

                    dataTableTmp = this.MasterTorihikisakiDao.GetDateForStringSql(sql);
                    nyuuSyukkinKubun = string.Empty;
                    if (dataTableTmp.Rows.Count != 0)
                    {
                        nyuuSyukkinKubun = (string)dataTableTmp.Rows[0].ItemArray[indexNyuuSyukkinKubun];
                    }

                    // 今回請求額総合計
                    konkaiSeikyuuGakuTotal += konkaiSeikyuuShiharaiGaku;

                    // 今回入金額総合計
                    konkaiNyuukinGakuTotal += konkaiNyuukinGaku;

                    // 今回未入金額総合計
                    konkaiMinyuukinGakuTotal += minyuukinGaku;

                    if (minyuukinGaku == 0)
                    {   // 未入金額が０
                        continue;
                    }

                    row["PHN_TORIHIKISAKI_CD_VLB"] = torihikisakiCDNext;
                    row["PHN_TORIHIKISAKI_NAME_VLB"] = torihikisakiCDName;

                    row["PHY_NYUUKIN_YOTEI_BI_VLB"] = nyuukinYoteiBi;

                    if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.EigyoTantoshaBetsu)
                    {   // 営業担当者別
                        row["PHY_EIGYOU_TANTOUSHA_CD_VLB"] = eigyouTantoushaCD;
                        row["PHY_SHAIN_NAME_VLB"] = eigyouTantoushaCDName;
                    }
                    else
                    {   // 取引先別又はブランク
                        row["PHY_EIGYOU_TANTOUSHA_CD_VLB"] = torihikisakiCDNext;
                        row["PHY_SHAIN_NAME_VLB"] = torihikisakiCDName;
                    }

                    row["PHY_KONKAI_SEIKYU_GAKU_VLB"] = konkaiSeikyuuShiharaiGaku;
                    row["PHY_KONKAI_NYUUKIN_GAKU_VLB"] = konkaiNyuukinGaku;
                    row["PHY_MI_NYUUKIN_GAKU_VLB"] = minyuukinGaku;
                    row["PHY_SHIMEBI_VLB"] = shimeBi;
                    row["PHY_SEIKYUU_DATE_VLB"] = seikyuuSeisanDate;
                    row["PHY_NYUUSHUKKIN_KBN_NAME_VLB"] = nyuuSyukkinKubun;

                    row["PHN_KONKAI_SEIKYU_GAKU_TOTAL_VLB"] = konkaiSeikyuuGakuTotal;
                    row["PHN_KONKAI_NYUUKIN_GAKU_TOTAL_VLB"] = konkaiNyuukinGakuTotal;
                    row["PHN_MI_NYUUKIN_GAKU_TOTAL_VLB"] = konkaiMinyuukinGakuTotal;

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

    #endregion - CommonChouhyouR369_R376 -

    #endregion - Classes -
}
