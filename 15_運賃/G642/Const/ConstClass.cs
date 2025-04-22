using System.Collections.Generic;

namespace Shougun.Core.Carriage.UnchinMeisaihyou
{
    /// <summary>
    /// 運賃明細表で使用する定数を集めたクラス
    /// </summary>
    internal static class ConstClass
    {
        /// <summary>
        /// 帳票タイトル「運賃明細表」
        /// </summary>
        internal static readonly string UNCHIN_MEISAI_TITLE = "運賃明細表";
        /// <summary>
        /// 帳票サブタイトル「運搬業者CD順」
        /// </summary>
        internal static readonly string SORT_UNPAN_GYOUSHA_CD_SUB_TITLE = "（運搬業者CD順）";

        /// <summary>
        /// 帳票サブタイトル「フリガナ順」
        /// </summary>
        internal static readonly string SORT_FURIGANA_SUB_TITLE = "（フリガナ順）";

        /// <summary>
        /// 帳票サブタイトル「伝票日付順」
        /// </summary>
        internal static readonly string SORT_DENPYOU_DATE_SUB_TITLE = "（伝票日付順）";

        /// <summary>
        /// 帳票サブタイトル「伝票番号順」
        /// </summary>
        internal static readonly string SORT_DENPYOU_NO_SUB_TITLE = "（伝票番号順）";

        /// <summary>
        /// 日付種類「伝票日付」
        /// </summary>
        internal static readonly string DATE_SHURUI_DENPYOU = "[伝票日付]";

        /// <summary>
        /// 日付種類「入力日付」
        /// </summary>
        internal static readonly string DATE_SHURUI_INPUT = "[入力日付]";

        /// <summary>
        /// ヘッダ用文字列「[品名]」
        /// </summary>
        internal static readonly string LABEL_NYUUSHUKKA_KBN_HINMEI = "[品名]";

        /// <summary>
        /// ヘッダ用文字列「[種類]」
        /// </summary>
        internal static readonly string LABEL_NYUUSHUKKA_KBN_SHURUI = "[種類]";

        /// <summary>
        /// ヘッダ用文字列「[分類]」
        /// </summary>
        internal static readonly string LABEL_NYUUSHUKKA_KBN_BUNRUI = "[分類]";

        /// <summary>
        /// 文字列「全て」
        /// </summary>
        internal static readonly string ALL = "全て";

        /// <summary>
        /// 伝種区分
        /// </summary>
        internal static readonly Dictionary<int, string> DenshuKbn = new Dictionary<int, string>()
        {
            {1, "受入"},
            {2, "出荷"},
            {3, "売上／支払"},
            {170, "代納"},
            {160, "運賃"},
            {6, "全て"},
        };
    }
}
