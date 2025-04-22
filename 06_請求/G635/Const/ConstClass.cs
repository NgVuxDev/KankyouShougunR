using System.Collections.Generic;

namespace Shougun.Core.Billing.SeikyuuMeisaihyouShutsuryoku
{
    /// <summary>
    /// 請求明細表 出力画面 で使用する定数を集めたクラス
    /// </summary>
    internal static class ConstClass
    {

       internal static readonly string CHECKMETHODNAME_REQUIRED = "必須チェック";
        /// <summary>
        /// 請求書書式「取引先別」
        /// </summary>
        internal static readonly int SHOSHIKI_T = 1;

        /// <summary>
        /// 請求書書式「業者別/現場別」
        /// </summary>
        internal static readonly int SHOSHIKI_G = 2;
    }
}
