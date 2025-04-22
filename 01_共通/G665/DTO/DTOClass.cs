using System.Collections.Generic;
using r_framework.Entity;

namespace Shougun.Core.Common.HanyoCSVShutsuryoku.DTO
{
    #region 基本定義

    /// <summary>
    ///
    /// </summary>
    public class DTOClass
    {
    }

    #endregion

    /// <summary>
    /// 出力条件指定情報
    /// </summary>
    public class JokenDto
    {
        /// <summary>
        /// 画面区分(呼び出し画面)
        /// </summary>
        /// <remarks>
        /// 1: 販売管理、2: 入金・出金
        /// </remarks>
        public int HaniKbn { get; set; }

        /// <summary>
        /// 指定日付区分
        /// </summary>
        /// <remarks>
        /// 1: 伝票日付、2: 売上日付、3: 支払日付、4: 入力日付。
        /// </remarks>
        public int DateSpecify { get; set; }

        /// <summary>
        /// 指定日付区分2
        /// </summary>
        /// <remarks>
        /// 1: 当日、2: 当月、3: 期間指定。
        /// </remarks>
        public int DateSpecify2 { get; set; }

        /// <summary>
        /// 開始日付
        /// </summary>
        /// <remarks>
        /// yyyy/MM/dd
        /// 空文字を保持できるため
        /// </remarks>
        public string DateFrom { get; set; }

        /// <summary>
        /// 終了日付
        /// </summary>
        /// <remarks>
        /// yyyy/MM/dd
        /// 空文字を保持できるため
        /// </remarks>
        public string DateTo { get; set; }

        /// <summary>
        /// 拠点CD
        /// </summary>
        public string KyotenCd { get; set; }

        /// <summary>
        /// 開始取引先CD
        /// </summary>
        public string TorihikisakiCdFrom { get; set; }

        /// <summary>
        /// 終了取引先CD
        /// </summary>
        public string TorihikisakiCdTo { get; set; }

        /// <summary>
        /// 開始業者CD
        /// </summary>
        public string GyoushaCdFrom { get; set; }

        /// <summary>
        /// 終了業者CD
        /// </summary>
        public string GyoushaCdTo { get; set; }

        /// <summary>
        /// 開始現場CD
        /// </summary>
        public string GenbaCdFrom { get; set; }

        /// <summary>
        /// 終了現場CD
        /// </summary>
        public string GenbaCdTo { get; set; }

        /// <summary>
        /// 開始入金先CD
        /// </summary>
        public string NyuukinsakiCdFrom { get; set; }

        /// <summary>
        /// 終了入金先CD
        /// </summary>
        public string NyuukinsakiCdTo { get; set; }

        /// <summary>
        /// 開始銀行CD
        /// </summary>
        public string BankCdFrom { get; set; }

        /// <summary>
        /// 終了銀行CD
        /// </summary>
        public string BankCdTo { get; set; }

        /// <summary>
        /// 開始入金先CD
        /// </summary>
        public string BankShitenCdFrom { get; set; }

        /// <summary>
        /// 終了入金先CD
        /// </summary>
        public string BankShitenCdTo { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class PatternTypeDto
    {
        /// <summary>
        /// 画面区分
        /// </summary>
        /// <remarks>
        /// 1: 販売管理、2: 入金・出金
        /// </remarks>
        public int HaniKbn { get; internal set; }

        /// <summary>
        /// 画面子タイトル
        /// </summary>
        public string HaniName { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// 呼び出し画面
        /// </summary>
        public string PanelName { get; internal set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class PatternDto
    {
        /// <summary>
        ///
        /// </summary>
        public int HaniKbn { get; set; }

        /// <summary>
        ///
        /// </summary>
        public M_OUTPUT_CSV_PATTERN CsvPattern { get; set; }

        /// <summary>
        ///
        /// </summary>
        public List<M_OUTPUT_CSV_PATTERN_COLUMN> CsvPatternColumns { get; set; }

        /// <summary>
        ///
        /// </summary>
        public M_OUTPUT_CSV_PATTERN_HANBAIKANRI CsvPatternHanbaikanri { get; set; }

        /// <summary>
        ///
        /// </summary>
        public M_OUTPUT_CSV_PATTERN_NYUUSHUKKIN CsvPatternNyuushukkin { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="haniKbn"></param>
        public PatternDto(int haniKbn)
        {
            this.HaniKbn = haniKbn;
        }

        public static bool ContentEquals(PatternDto obj1, PatternDto obj2)
        {
            bool ret = true;

            if (ret)
                ret &= obj1.HaniKbn == obj2.HaniKbn;

            if (ret)
            {
                M_OUTPUT_CSV_PATTERN p1 = obj1.CsvPattern, p2 = obj2.CsvPattern;
                // 出力範囲区分
                ret &= p1.KBN == p2.KBN;
                // 出力区分
                ret &= (p1.OUTPUT_KBN.IsNull && p2.OUTPUT_KBN.IsNull) ||
                    (!p1.OUTPUT_KBN.IsNull && !p2.OUTPUT_KBN.IsNull && p1.OUTPUT_KBN.Value == p2.OUTPUT_KBN.Value);
                // パターン名
                ret &= p1.PATTERN_NAME == p2.PATTERN_NAME;
                // 備考
                ret &= p1.BIKOU == p2.BIKOU;
            }

            if (ret)
            {
                List<M_OUTPUT_CSV_PATTERN_COLUMN> pcs1 = obj1.CsvPatternColumns, pcs2 = obj2.CsvPatternColumns;
                ret &= (pcs1 == null && pcs2 == null) || (pcs1 != null && pcs2 != null && pcs1.Count == pcs2.Count);

                if (ret)
                {
                    for (int i = 0; i < pcs1.Count; i++)
                    {
                        M_OUTPUT_CSV_PATTERN_COLUMN pc1 = pcs1[i], pc2 = pcs2[i];
                        // 出力区分
                        ret &= (pc1.OUTPUT_KBN.IsNull && pc2.OUTPUT_KBN.IsNull) ||
                            (!pc1.OUTPUT_KBN.IsNull && !pc2.OUTPUT_KBN.IsNull && pc1.OUTPUT_KBN.Value == pc2.OUTPUT_KBN.Value);
                        // 項目ID
                        ret &= (pc1.KOUMOKU_ID.IsNull && pc2.KOUMOKU_ID.IsNull) ||
                            (!pc1.KOUMOKU_ID.IsNull && !pc2.KOUMOKU_ID.IsNull && pc1.KOUMOKU_ID.Value == pc2.KOUMOKU_ID.Value);
                        // 並び順
                        ret &= (pc1.SORT_NO.IsNull && pc2.SORT_NO.IsNull) ||
                            (!pc1.SORT_NO.IsNull && !pc2.SORT_NO.IsNull && pc1.SORT_NO.Value == pc2.SORT_NO.Value);

                        if (!ret)
                            break;
                    }
                }
            }

            if (ret)
            {
                switch (obj1.HaniKbn)
                {
                    case 1:
                        M_OUTPUT_CSV_PATTERN_HANBAIKANRI ph1 = obj1.CsvPatternHanbaikanri, ph2 = obj2.CsvPatternHanbaikanri;
                        // 伝票種類_受入
                        ret &= (ph1.DENPYOU_SHURUI_UKEIRE.IsNull && ph2.DENPYOU_SHURUI_UKEIRE.IsNull) ||
                            (!ph1.DENPYOU_SHURUI_UKEIRE.IsNull && !ph2.DENPYOU_SHURUI_UKEIRE.IsNull && ph1.DENPYOU_SHURUI_UKEIRE.Value == ph2.DENPYOU_SHURUI_UKEIRE.Value);
                        // 伝票種類_出荷
                        ret &= (ph1.DENPYOU_SHURUI_SHUKKA.IsNull && ph2.DENPYOU_SHURUI_SHUKKA.IsNull) ||
                            (!ph1.DENPYOU_SHURUI_SHUKKA.IsNull && !ph2.DENPYOU_SHURUI_SHUKKA.IsNull && ph1.DENPYOU_SHURUI_SHUKKA.Value == ph2.DENPYOU_SHURUI_SHUKKA.Value);
                        // 伝票種類_売上/支払
                        ret &= (ph1.DENPYOU_SHURUI_UR_SH.IsNull && ph2.DENPYOU_SHURUI_UR_SH.IsNull) ||
                            (!ph1.DENPYOU_SHURUI_UR_SH.IsNull && !ph2.DENPYOU_SHURUI_UR_SH.IsNull && ph1.DENPYOU_SHURUI_UR_SH.Value == ph2.DENPYOU_SHURUI_UR_SH.Value);
                        // 伝票種類_代納
                        ret &= (ph1.DENPYOU_SHURUI_DAINOU.IsNull && ph2.DENPYOU_SHURUI_DAINOU.IsNull) ||
                            (!ph1.DENPYOU_SHURUI_DAINOU.IsNull && !ph2.DENPYOU_SHURUI_DAINOU.IsNull && ph1.DENPYOU_SHURUI_DAINOU.Value == ph2.DENPYOU_SHURUI_DAINOU.Value);
                        // 伝票区分_売上
                        ret &= (ph1.DENPYOU_KBN_URIAGE.IsNull && ph2.DENPYOU_KBN_URIAGE.IsNull) ||
                            (!ph1.DENPYOU_KBN_URIAGE.IsNull && !ph2.DENPYOU_KBN_URIAGE.IsNull && ph1.DENPYOU_KBN_URIAGE.Value == ph2.DENPYOU_KBN_URIAGE.Value);
                        // 伝票区分_支払
                        ret &= (ph1.DENPYOU_KBN_SHIHARAI.IsNull && ph2.DENPYOU_KBN_SHIHARAI.IsNull) ||
                            (!ph1.DENPYOU_KBN_SHIHARAI.IsNull && !ph2.DENPYOU_KBN_SHIHARAI.IsNull && ph1.DENPYOU_KBN_SHIHARAI.Value == ph2.DENPYOU_KBN_SHIHARAI.Value);
                        // 取引区分
                        ret &= (ph1.TORIHIKI_KBN.IsNull && ph2.TORIHIKI_KBN.IsNull) ||
                            (!ph1.TORIHIKI_KBN.IsNull && !ph2.TORIHIKI_KBN.IsNull && ph1.TORIHIKI_KBN.Value == ph2.TORIHIKI_KBN.Value);
                        // 確定区分
                        ret &= (ph1.KAKUTEI_KBN.IsNull && ph2.KAKUTEI_KBN.IsNull) ||
                            (!ph1.KAKUTEI_KBN.IsNull && !ph2.KAKUTEI_KBN.IsNull && ph1.KAKUTEI_KBN.Value == ph2.KAKUTEI_KBN.Value);
                        // 締処理状況
                        ret &= (ph1.SHIME_SHORI_JOUKYOU.IsNull && ph2.SHIME_SHORI_JOUKYOU.IsNull) ||
                            (!ph1.SHIME_SHORI_JOUKYOU.IsNull && !ph2.SHIME_SHORI_JOUKYOU.IsNull && ph1.SHIME_SHORI_JOUKYOU.Value == ph2.SHIME_SHORI_JOUKYOU.Value);
                        break;

                    case 2:
                        M_OUTPUT_CSV_PATTERN_NYUUSHUKKIN pn1 = obj1.CsvPatternNyuushukkin, pn2 = obj2.CsvPatternNyuushukkin;
                        // 伝票種類_入金
                        ret &= (pn1.DENPYOU_SHURUI_NYUUKIN.IsNull && pn2.DENPYOU_SHURUI_NYUUKIN.IsNull) ||
                            (!pn1.DENPYOU_SHURUI_NYUUKIN.IsNull && !pn2.DENPYOU_SHURUI_NYUUKIN.IsNull && pn1.DENPYOU_SHURUI_NYUUKIN.Value == pn2.DENPYOU_SHURUI_NYUUKIN.Value);
                        // 伝票種類_出金
                        ret &= (pn1.DENPYOU_SHURUI_SHUKKIN.IsNull && pn2.DENPYOU_SHURUI_SHUKKIN.IsNull) ||
                            (!pn1.DENPYOU_SHURUI_SHUKKIN.IsNull && !pn2.DENPYOU_SHURUI_SHUKKIN.IsNull && pn1.DENPYOU_SHURUI_SHUKKIN.Value == pn2.DENPYOU_SHURUI_SHUKKIN.Value);
                        // 締処理状況
                        ret &= (pn1.SHIME_SHORI_JOUKYOU.IsNull && pn2.SHIME_SHORI_JOUKYOU.IsNull) ||
                            (!pn1.SHIME_SHORI_JOUKYOU.IsNull && !pn2.SHIME_SHORI_JOUKYOU.IsNull && pn1.SHIME_SHORI_JOUKYOU.Value == pn2.SHIME_SHORI_JOUKYOU.Value);

                        break;

                    default:
                        break;
                }
            }

            return ret;
        }
    }
}