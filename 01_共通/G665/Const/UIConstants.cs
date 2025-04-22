using System;
using r_framework.Const;

namespace Shougun.Core.Common.HanyoCSVShutsuryoku.Const
{
    public class UIConstants
    {
        /// <summary>
        /// Button設定用XMLファイルパス
        /// </summary>
        internal static readonly string ButtonInfoXmlPath = "Shougun.Core.Common.HanyoCSVShutsuryoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// パターン画面Button設定用XMLファイルパス
        /// </summary>
        internal static readonly string PatternButtonInfoXmlPath = "Shougun.Core.Common.HanyoCSVShutsuryoku.Setting.PatternButtonSetting.xml";

        /// <summary>
        /// CSVファイル名
        /// </summary>
        internal static readonly string CSV_FILE_PREFIX_FORMAT = "汎用CSV出力_{0}";

        /// <summary>
        /// 出力区分
        /// </summary>
        /// <remarks>
        /// 伝票
        /// 明細
        /// </remarks>
        public static readonly string[] OUTPUT_KBNS = { "伝票", "明細" };

        /// <summary>
        ///
        /// </summary>
        internal static readonly Tuple<int, string, string, string>[] OUTPUT_HANI_KBNS =
        {
            new Tuple<int, string, string, string>(
                1,
                "販売管理",
                "(受入・出荷・売上/支払・代納)",
                "HanbaikanriPatternPanel"
                ),
            new Tuple<int, string, string, string>(
                2,
                "入金・出金",
                string.Empty,
                "NyuushukkinPatternPanel"
                ),
        };

        /// <summary>
        ///
        /// </summary>
        internal static readonly Tuple<int, string, string>[] DATE_SPECIFY_KBNS =
        {
            new Tuple<int, string, string>(1, "伝票日付", "DENPYOU_DATE"),
            new Tuple<int, string, string>(2, "売上日付", "URIAGE_DATE"),
            new Tuple<int, string, string>(3, "支払日付", "SHIHARAI_DATE"),
            new Tuple<int, string, string>(4, "入力日付","UPDATE_DATE")
        };

        /// <summary>
        ///
        /// </summary>
        internal static readonly Tuple<int, string>[] DATE_SPECIFY2_KBNS =
        {
            new Tuple<int, string>(1, "当日"),
            new Tuple<int, string>(2, "当月"),
            new Tuple<int, string>(3, "期日指定")
        };

        /// <summary>
        ///
        /// </summary>
        internal static readonly Tuple<int, DENSHU_KBN, string[]>[] PATTERN_SETTING_XML_PATHS =
        {
            // 1, 1, 受入, { 001_UKEIRE }
            new Tuple<int, DENSHU_KBN, string[]>(1, DENSHU_KBN.UKEIRE, new string[] { "UKEIRE" }),
            // 1, 2, 出荷, { 001_SHUKKA, 001_SHUKKA_KEN }
            new Tuple<int, DENSHU_KBN, string[]>(1, DENSHU_KBN.SHUKKA, new string[] { "SHUKKA", "SHUKKA_KEN" }),
            // 1, 3, 売上/支払, { 001_UR_SH }
            new Tuple<int, DENSHU_KBN, string[]>(1, DENSHU_KBN.URIAGE_SHIHARAI, new string[] { "UR_SH" }),
            // 1, 4, 代納, { 001_DAINOU_UKE, 001_DAINOU_SHU }
            new Tuple<int, DENSHU_KBN, string[]>(1, DENSHU_KBN.DAINOU, new string[] { "DAINOU_UKE", "DAINOU_SHU" }),
            // 2, 10, 入金, { 002_NYUUKIN_NYUU, 002_NYUUKIN_NYUU }
            new Tuple<int, DENSHU_KBN, string[]>(2, DENSHU_KBN.NYUUKIN, new string[] { "NYUUKIN_NYUU", "NYUUKIN_TORI" }),
            // 2, 20, 出金, { 002_SHUKKIN }
            new Tuple<int, DENSHU_KBN, string[]>(2, DENSHU_KBN.SHUKKIN, new string[] { "SHUKKIN" }),
        };
    }
}