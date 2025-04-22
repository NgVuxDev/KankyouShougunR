// $Id: ConstClass.cs 51163 2015-06-01 07:48:00Z chenzz@oec-h.com $

namespace Shougun.Core.SalesPayment.DenpyouKakuteiNyuryoku.Const
{
    /// <summary>
    /// 伝票確定画面定義クラス
    /// </summary>
    public class ConstClass
    {
        /// <summary>確定区分検索条件 1:全て</summary>
        public static readonly int SEARCH_KAKUTEI_KBN_ALL = 1;
        /// <summary>確定区分検索条件 2:未確定</summary>
        public static readonly int SEARCH_KAKUTEI_KBN_MIKAKUTEI = 2;
        /// <summary>確定区分検索条件 3:確定済み</summary>
        public static readonly int SEARCH_KAKUTEI_KBN_KAKUTEI = 3;

        /// <summary>伝票区分検索条件 1:全て</summary>
        public static readonly int SEARCH_DENPYOU_KBN_ALL = 1;
        /// <summary>伝票区分検索条件 2:売上</summary>
        public static readonly int SEARCH_DENPYOU_KBN_URIAGE = 2;
        /// <summary>伝票区分検索条件　3:支払</summary>
        public static readonly int SEARCH_DENPYOU_KBN_SHIHARAI = 3;

        /// <summary>確定区分を表示(受入)　2:利用しない</summary>
        public static readonly int UKEIRE_KAKUTEI_USE_KBN_UNUSED = 2;

        /// <summary>確定区分を表示(出荷)　2:利用しない</summary>
        public static readonly int SHUKKA_KAKUTEI_USE_KBN_UNUSED = 2;

        /// <summary>確定区分を表示(売上支払)　2:利用しない</summary>
        public static readonly int UR_SH_KAKUTEI_USE_KBN_UNUSED = 2;

        /// <summary>確定区分を表示(代納)　2:利用しない</summary>
        public static readonly int DAINOU_KAKUTEI_USE_KBN_UNUSED = 2;
    }
}
