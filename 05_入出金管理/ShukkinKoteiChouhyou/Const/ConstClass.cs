using System.Collections.Generic;

namespace Shougun.Core.ReceiptPayManagement.ShukkinKoteiChouhyou
{
    /// <summary>
    /// 出金明細表で使用する定数を集めたクラス
    /// </summary>
    internal static class ConstClass
    {
        /// <summary>
        /// 帳票タイトル「出金明細票」
        /// </summary>
        internal static readonly string SHUKKIN_MEISAIHYOU_TITLE = "出金明細表";

        /// <summary>
        /// 帳票サブタイトル「取引先」
        /// </summary>
        internal static readonly string SORT_TORIHIKISAKI_SUB_TITLE = "取引先";

        /// <summary>
        /// 帳票サブタイトル「ＣＤ」
        /// </summary>
        internal static readonly string SORT_CD_SUB_TITLE = "ＣＤ";

        /// <summary>
        /// 帳票サブタイトル「フリガナ」
        /// </summary>
        internal static readonly string SORT_FURIGANA_SUB_TITLE = "フリガナ";

        /// <summary>
        /// 帳票サブタイトル「伝票日付」
        /// </summary>
        internal static readonly string SORT_DENPYOU_DATE_SUB_TITLE = "伝票日付";

        /// <summary>
        /// 帳票サブタイトル「伝票番号」
        /// </summary>
        internal static readonly string SORT_DENPYOU_NO_SUB_TITLE = "伝票番号";

        /// <summary>
        /// 帳票サブタイトル「出金区分」
        /// </summary>
        internal static readonly string SORT_NYUUSHUKKIN_KBN_SUB_TITLE = "出金区分";

        /// <summary>
        /// 日付種類「伝票日付」
        /// </summary>
        internal static readonly string DATE_SHURUI_DENPYOU = "伝票日付";

        /// <summary>
        /// 日付種類「入力日付」
        /// </summary>
        internal static readonly string DATE_SHURUI_INPUT = "入力日付";

        /// <summary>
        /// カラム「伝票日付」
        /// </summary>
        internal static readonly string COLUMN_DENPYOU_DATE = "伝票日付";

        /// <summary>
        /// カラム「出金番号」
        /// </summary>
        internal static readonly string COLUMN_SHUKKIN_NUMBER = "出金番号";

        /// <summary>
        /// カラム「取引先」
        /// </summary>
        internal static readonly string COLUMN_TORIHIKISAKI = "取引先";

        /// <summary>
        /// カラム「出金区分」
        /// </summary>
        internal static readonly string COLUMN_NYUUSHUKKIN_KBN = "出金区分";

        /// <summary>
        /// 並び順１「コード」
        /// </summary>
        internal static readonly int SORT_1_CD = 1;

        /// <summary>
        /// 並び順１「フリガナ」
        /// </summary>
        internal static readonly int SORT_1_FURIGANA = 2;

        /// <summary>
        /// 並び順１「伝票日付」
        /// </summary>
        internal static readonly int SORT_1_DENPYOU_DATE = 3;

        /// <summary>
        /// 並び順１「伝票番号」
        /// </summary>
        internal static readonly int SORT_1_DENPYOU_NUMBER = 4;

        /// <summary>
        /// 並び順１「入出金区分」
        /// </summary>
        internal static readonly int SORT_1_NYUUSHUKKIN_KBN = 5;
    }
}
