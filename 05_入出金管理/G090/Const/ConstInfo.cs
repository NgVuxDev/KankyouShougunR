
using System.Collections.Generic;
namespace Shougun.Core.ReceiptPayManagement.Syukinnyuryoku
{
    /// <summary>
    /// 出金入力で使用する定数を集めたクラス
    /// </summary>
    internal class ConstInfo
    {
        /// <summary>
        /// 出金区分 1:現金
        /// </summary>
        public const string SHUKKIN_KBN_GENKIN = "1";

        /// <summary>
        /// 出金区分 2:振込
        /// </summary>
        public const string SHUKKIN_KBN_FURIKOMI = "2";
        /// <summary>
        /// 振込出力 1.する
        /// </summary>
        public const string FURIKOMI_SHUTSURYOKU_ARI = "1";
        /// <summary>
        /// 振込出力 2.しない
        /// </summary>
        public const string FURIKOMI_SHUTSURYOKU_NASHI = "2";

        public const string TE_SUURYOU_JISHA = "自社負担";

        public const string TE_SUURYOU_SENPOU = "先方負担";
        /// <summary>
        /// 口座種類
        /// </summary>
        public static List<M_KOUZA_SHURUI> LIST_KOUZA_SHURUI = new List<M_KOUZA_SHURUI>()
        {
            new M_KOUZA_SHURUI(){KOUZA_SHURUI_CD = "1", KOUZA_SHURUI_NAME = "普通預金"},
            new M_KOUZA_SHURUI(){KOUZA_SHURUI_CD = "2", KOUZA_SHURUI_NAME = "当座預金"},
            new M_KOUZA_SHURUI(){KOUZA_SHURUI_CD = "9", KOUZA_SHURUI_NAME = "その他"}
        };
    }
    /// <summary>
    /// 口座種類
    /// </summary>
    internal class M_KOUZA_SHURUI
    {
        public string KOUZA_SHURUI_CD { get; set; }
        public string KOUZA_SHURUI_NAME { get; set; }
    }
}
