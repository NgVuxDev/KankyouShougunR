using System.Collections.Generic;

namespace Shougun.Core.Adjustment.ShiharaiMeisaiMeisaihyou
{
    /// <summary>
    /// 支払明細明細表 出力画面 で使用する定数を集めたクラス
    /// </summary>
    internal static class ConstClass
    {

       internal static readonly string CHECKMETHODNAME_REQUIRED = "必須チェック";
        /// <summary>
        /// 支払明細書書式「取引先別」
        /// </summary>
        internal static readonly int SHOSHIKI_T = 1;

        /// <summary>
        /// 支払明細書書式「業者別/現場別」
        /// </summary>
        internal static readonly int SHOSHIKI_G = 2;
    }
}
