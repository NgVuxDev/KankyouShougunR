// $Id: ConstCls.cs 20309 2014-05-08 05:15:32Z sanbongi $
namespace Shougun.Core.BusinessManagement.TorihikisakiKakunin.Const
{
    public class ConstCls
    {
        /// <summary>
        /// 列挙型：取引先CDフォーカスアウト時チェック結果
        /// </summary>
        public enum TorihikisakiCdLeaveResult
        {
            /// <summary>
            /// 重複あり＋修正モード確認無し
            /// </summary>
            FALSE_NONE,

            /// <summary>
            /// 重複あり＋修正モード表示
            /// </summary>
            FALSE_ON,

            /// <summary>
            /// 重複あり＋修正モード非表示
            /// </summary>
            FALSE_OFF,

            /// <summary>
            /// 重複なし
            /// </summary>
            TURE_NONE
        }

        /// <summary>M_HIKIAI_TORIHIKISAKIのTORIHIKISAKI_CD</summary>
        public static readonly string TORIHIKISAKI_CD = "TORIHIKISAKI_CD";

        /// <summary>入力最大バイト数の定数名（CharactersNumber)</summary>
        public static readonly string CHARACTERS_NUMBER = "CharactersNumber";

        #region 書式区分
        /// <summary>書式区分(請求/支払先別)</summary>
        public static readonly char SHOSHIKI_KBN_TORIHIKISAKI = '1';

        /// <summary>書式区分(業者別)</summary>
        public static readonly char SHOSHIKI_KBN_GYOUSHA = '2';

        /// <summary>書式区分(現場別)</summary>
        public static readonly char SHOSHIKI_KBN_GENBA = '3';
        #endregion

        #region 書式明細区分
        /// <summary>書式明細区分(なし)</summary>
        public static readonly char SHOSHIKI_MEISAI_KBN_NONE = '1';

        /// <summary>書式明細区分(業者毎)</summary>
        public static readonly char SHOSHIKI_MEISAI_KBN_GYOUSHA = '2';

        /// <summary>書式明細区分(現場毎)</summary>
        public static readonly char SHOSHIKI_MEISAI_KBN_GENBA = '3';
        #endregion

        #region 税計算区分
        /// <summary>税計算区分(伝票毎)</summary>
        public static readonly char ZEI_KEISAN_KBN_DENPYOU = '1';
        /// <summary>税計算区分(請求毎)</summary>
        public static readonly char ZEI_KEISAN_KBN_SEIKYUU = '2';
        #endregion

        #region 税区分
        /// <summary>税区分(外税)</summary>
        public static readonly char ZEI_KBN_SOTO = '1';

        /// <summary>税区分(内税)</summary>
        public static readonly char ZEI_KBN_UCHI = '2';

        /// <summary>税区分(非課税)</summary>
        public static readonly char ZEI_KBN_NONE = '3';
        #endregion

    }
}
