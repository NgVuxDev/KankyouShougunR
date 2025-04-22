using System.Data.SqlTypes;
using System.Drawing;

namespace Shougun.Core.ExternalConnection.SmsResult
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        // 伝票種類
        /// <summary>収集</summary>
        public static readonly string SHUSHU = "収集";

        /// <summary>出荷</summary>
        public static readonly string SHUKKA = "出荷";

        /// <summary>持込</summary>
        public static readonly string MOCHIKOMI = "持込";

        /// <summary>定期</summary>
        public static readonly string TEIKI = "定期";

        // 配車状況
        /// <summary>受注</summary>
        public static readonly string JYU = "収集";

        /// <summary>エラー背景色（赤）</summary>
        public static readonly Color ERROR_COLOR = Color.FromArgb(255, 100, 100);

        /// <summary>非表示列（システムID）</summary>
        public static readonly string HIDDEN_SYSTEM_ID = "SYSTEM_ID_HIDDEN";
    }
}
