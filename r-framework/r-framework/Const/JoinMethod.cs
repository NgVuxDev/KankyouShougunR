using System;

namespace r_framework.Const
{
    /// <summary>
    /// Join句の情報を設定したEnum
    /// </summary>
    public enum JOIN_METHOD
    {
        INNER_JOIN = 0,
        OUTER_JOIN = 1,
        LEFT_JOIN = 2,
        RIGHT_JOIN = 3,
        WHERE = 4,
    }

    /// <summary>
    /// Join句の日本語文字列を取得
    /// </summary>
    public static class JOIN_METHODExt
    {
        /// <summary>
        /// Join句の文字列を取得する
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string ToString(this JOIN_METHOD e)
        {
            switch (e)
            {
                case JOIN_METHOD.INNER_JOIN:
                    return "INNER JOIN";
                case JOIN_METHOD.OUTER_JOIN:
                    return "OUTER JOIN";
                case JOIN_METHOD.LEFT_JOIN:
                    return "LEFT JOIN";
                case JOIN_METHOD.RIGHT_JOIN:
                    return "RIGHT JOIN";
                case JOIN_METHOD.WHERE:
                    return "WHERE";
            }
            return String.Empty;
        }
    }
}
