using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>
        /// 最終処分の場所（実績）
        /// </summary>
        public static readonly int LAST_SBN_JOU_LENGTH = 140;

        /// <summary>
        /// 最終処分場所（予定）
        /// </summary>
        public static readonly int LAST_SBN_PLAN_LENGTH = 140;

        /// <summary>
        /// 中間処理産業廃棄物
        /// </summary>
        public static readonly int FIRST_MANIFEST_LENGTH = 100;

        /// <summary>
        /// 最終処分事業場記載フラグ=0の場合　
        /// 委託契約書記載の通り
        /// </summary>
        public static readonly string LAST_SBN_JOU_KISAI = "委託契約書記載の通り";

        /// <summary>
        /// 最終処分事業場の場合
        /// 最終処分事業場記載フラグ=1
        /// </summary>
        public static readonly string LAST_SBN_JOU_TYPE = "1";

        /// <summary>
        /// 委託契約書記載の通りの場合
        /// 最終処分事業場記載フラグ=0
        /// </summary>
        public static readonly string LAST_SBN_JOU_KISAI_TYPE = "0";

        /// <summary>
        /// 受渡確認票
        /// </summary>
        public static readonly string REPORT_TITLE = "受渡確認票";

    }
}
