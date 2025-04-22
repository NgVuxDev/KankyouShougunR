using System.Collections.Generic;

namespace Shougun.Core.ReceiptPayManagement.MiShukkinIchiran
{
    /// <summary>
    /// 未出金一覧表で使用する定数を集めたクラス
    /// </summary>
    internal static class ConstClass
    {
        /// <summary>
        /// 帳票タイトル「未出金一覧表」
        /// </summary>
        internal static readonly string MISHUKKIN_ICHIRAN_TITLE = "未出金一覧表";

        /// <summary>
        /// 帳票サブタイトル「営業担当者」
        /// </summary>
        internal static readonly string SORT_EIGYOUSHA_SUB_TITLE = "営業担当者";

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
        /// カラム「営業担当者CD」
        /// </summary>
        internal static readonly string COLUMN_EIGYOUSHA_CD = "営業担当者CD";

        /// <summary>
        /// カラム「営業担当者名」
        /// </summary>
        internal static readonly string COLUMN_EIGYOUSHA = "営業担当者名";

        /// <summary>
        /// カラム「取引先CD」
        /// </summary>
        internal static readonly string COLUMN_TORIHIKISAKI_CD = "取引先CD";

        /// <summary>
        /// カラム「取引先名」
        /// </summary>
        internal static readonly string COLUMN_TORIHIKISAKI = "取引先名";

        /// <summary>
        /// 並び順１「営業担当者」
        /// </summary>
        internal static readonly int SORT_1_EIGYOUSHA = 1;

        /// <summary>
        /// 並び順１「取引先」
        /// </summary>
        internal static readonly int SORT_1_TORIHIKISAKI = 2;

        /// <summary>
        /// 並び順２「コード」
        /// </summary>
        internal static readonly int SORT_2_CD = 1;

        /// <summary>
        /// 並び順２「フリガナ」
        /// </summary>
        internal static readonly int SORT_2_FURIGANA = 2;

        /// <summary>
        /// 精算書書式「取引先別」
        /// </summary>
        internal static readonly int SHOSHIKI_T = 1;

        /// <summary>
        /// 精算書書式「業者別/現場別」
        /// </summary>
        internal static readonly int SHOSHIKI_G = 2;
    }
}
