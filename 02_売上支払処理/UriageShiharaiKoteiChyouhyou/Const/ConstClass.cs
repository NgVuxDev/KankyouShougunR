using System.Collections.Generic;

namespace Shougun.Core.SalesPayment.UriageShiharaiKoteiChouhyou
{
    /// <summary>
    /// 売上支払明細表で使用する定数を集めたクラス
    /// </summary>
    internal static class ConstClass
    {
        /// <summary>
        /// 帳票タイトル「売上明細票」
        /// </summary>
        internal static readonly string URIAGE_MEISAIHYOU_TITLE = "売上明細表";

        /// <summary>
        /// 帳票タイトル「支払明細表」
        /// </summary>
        internal static readonly string SHIHARAI_MEISAIHYOU_TITLE = "支払明細表";

        /// <summary>
        /// 帳票タイトル「受入明細表」
        /// </summary>
        internal static readonly string UKEIRE_MEISAIHYOU_TITLE = "受入明細表";

        /// <summary>
        /// 帳票タイトル「出荷明細表」
        /// </summary>
        internal static readonly string SHUKKA_MEISAIHYOU_TITLE = "出荷明細表";

        /// <summary>
        /// 帳票サブタイトル「取引先CD順」
        /// </summary>
        internal static readonly string SORT_TORIHIKISAKI_CD_SUB_TITLE = "（取引先ＣＤ順）";

        /// <summary>
        /// 帳票サブタイトル「業者CD順」
        /// </summary>
        internal static readonly string SORT_GYOUSHA_CD_SUB_TITLE = "（業者ＣＤ順）";

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
        /// 帳票カラム名「売上日付」
        /// </summary>
        internal static readonly string URIAGE_DATE_COLUMN_NAME = "売上日付";

        /// <summary>
        /// 帳票カラム名「支払日付」
        /// </summary>
        internal static readonly string SHIHARAI_DATE_COLUMN_NAME = "支払日付";

        /// <summary>
        /// 日付種類「伝票日付」
        /// </summary>
        internal static readonly string DATE_SHURUI_DENPYOU = "[伝票日付]";

        /// <summary>
        /// 日付種類「売上日付」
        /// </summary>
        internal static readonly string DATE_SHURUI_URIAGE = "[売上日付]";

        /// <summary>
        /// 日付種類「支払日付」
        /// </summary>
        internal static readonly string DATE_SHURUI_SHIHARAI = "[支払日付]";

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
        /// 入出荷区分「品名」
        /// </summary>
        internal static readonly string NYUUSHUKKA_KBN_HINMEI = "1";

        /// <summary>
        /// 入出荷区分「種類」
        /// </summary>
        internal static readonly string NYUUSHUKKA_KBN_SHURUI = "2";

        /// <summary>
        /// 入出荷区分「分類」
        /// </summary>
        internal static readonly string NYUUSHUKKA_KBN_BUNRUI = "3";

        /// <summary>
        /// 伝票区分
        /// </summary>
        internal static readonly Dictionary<int, string> DenpyouKbn = new Dictionary<int, string>()
        {
          {1, "売上"},  
          {2, "支払"},
        };

        /// <summary>
        /// 伝票種類
        /// </summary>
        internal static readonly Dictionary<int, string> DenpyouShurui = new Dictionary<int, string>()
        {
            {1, "受入"},
            {2, "出荷"},
            {3, "売上／支払"},
            {4, "代納"},
            {5, "全て"},
        };

        /// <summary>
        /// 日付種類
        /// </summary>
        internal static readonly Dictionary<int, string> DateShurui = new Dictionary<int, string>()
        {
            {1, "伝票日付"},
            {2, "売上日付"},
            {3, "入力日付"},
        };

        /// <summary>
        /// 締め状況
        /// </summary>
        internal static readonly Dictionary<int, string> ShimeJoukyou = new Dictionary<int, string>()
        {
            {1, "済"},
            {2, "未締"},
            {3, "全て"},
        };

        /// <summary>
        /// 確定区分
        /// </summary>
        internal readonly static Dictionary<int, string> KakuteiKbn = new Dictionary<int, string>()
        {
            {1, "確定"},
            {2, "未確定"},
            {3, "全て"},
        };

        /// <summary>
        /// 取引区分
        /// </summary>
        internal readonly static Dictionary<int, string> TorihikiKbn = new Dictionary<int, string>()
        {
            {1, "現金"},
            {2, "掛け"},
            {3, "全て"},
        };
    }
}
