
namespace Shougun.Core.ReceiptPayManagement.NyuuSyutuKinIchiran.Const
{
    /// <summary>
    /// G078_入出金一覧 で使用する定数を集めたクラス
    /// </summary>
    class ConstCls
    {
        /// <summary>
        /// 伝票種類 : 1.入金
        /// </summary>
        public const string DENPYO_SHURUI_NYUKIN = "1";

        /// <summary>
        /// 伝票種類 : 2.出金
        /// </summary>
        public const string DENPYO_SHURUI_SHUKKIN = "2";

        /// <summary>
        /// システム上必須な列名（システムID）
        /// </summary>
        internal static readonly string HIDDEN_SYSTEM_ID = "HIDDEN_SYSTEM_ID";

        /// <summary>
        /// システム上必須な列名（明細システムID）
        /// </summary>
        internal static readonly string HIDDEN_DETAIL_SYSTEM_ID = "HIDDEN_DETAIL_SYSTEM_ID";

        /// <summary>
        /// システム上必須な列名（入金番号）
        /// </summary>
        internal static readonly string HIDDEN_NYUUKIN_NUMBER = "HIDDEN_NYUUKIN_NUMBER";

        /// <summary>
        /// システム上必須な列名（特殊入力区分）
        /// </summary>
        internal static readonly string HIDDEN_TOK_INPUT_KBN = "HIDDEN_TOK_INPUT_KBN";

        /// <summary>
        /// システム上必須な列名（出金番号）
        /// </summary>
        internal static readonly string HIDDEN_SHUKKIN_NUMBER = "HIDDEN_SHUKKIN_NUMBER";

        /// <summary>
        /// 入金種類：1.入金（取引先）
        /// </summary>
        public const string NYUKIN_KBN_TORIHIKISAKI = "1";

        /// <summary>
        /// 入金種類：2.入金（入金先）
        /// </summary>
        public const string NYUKIN_KBN_NYUKINSAKI = "2";
    }
}
