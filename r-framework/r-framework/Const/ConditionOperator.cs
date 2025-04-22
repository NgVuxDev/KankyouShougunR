using System;

namespace r_framework.Const
{
    /// <summary>
    /// Enum
    /// </summary>
    public enum CONDITION_OPERATOR
    {
        AND = 0,
        OR = 1,
    }
    /// <summary>
    /// 画面タイプ拡張
    /// </summary>
    public static class CONDITION_OPERATORExt
    {
        /// <summary>
        /// 画面タイプから画面区分名を取得
        /// </summary>
        /// <param name="e">画面タイプ</param>
        /// <returns>画面タイプ名</returns>
        public static string ToString(this CONDITION_OPERATOR e)
        {
            switch (e)
            {
                case CONDITION_OPERATOR.AND:
                    return "and";
                case CONDITION_OPERATOR.OR:
                    return "or";
            }
            return String.Empty;
        }
    }
}
