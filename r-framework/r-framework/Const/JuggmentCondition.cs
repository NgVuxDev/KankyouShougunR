using System;

namespace r_framework.Const
{
    /// <summary>
    /// 判定用Enumクラス
    /// </summary>
    public enum JUGGMENT_CONDITION
    {
        //一致
        /// <summary> ＝ </summary>
        EQUALS = 0,
        //不一致
        /// <summary> ！＝ </summary>
        NOT_EQUALS = 1,
        //含む
        /// <summary> LIKE </summary>
        INCLUDING = 2,
        //含まない
        /// <summary> NOT LIKE </summary>
        NOT_INCLUDING = 3,
        //以上
        /// <summary> ＜ </summary>
        OR_MORE = 4,
        //以下
        /// <summary> ＞ </summary>
        FOLLOWING = 5,
        //未満
        /// <summary> ＜＝ </summary>
        LESS_THAN = 6,
        //超える
        /// <summary> ＞＝ </summary>
        MORE_THAN = 7,
        //列挙
        /// <summary> IN </summary>
        IN = 8,
        //列挙以外
        /// <summary> NO IN </summary>
        NOT_IN = 9,
    }

    /// <summary>
    /// 判定用のEnum拡張メソッド
    /// </summary>
    public static class JUGGMENT_CONDITIONExt
    {
        /// <summary>
        /// SQLで使用される判定文字列の取得
        /// </summary>
        /// <param name="e"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToConditionString(this JUGGMENT_CONDITION e, string str)
        {
            switch (e)
            {
                case JUGGMENT_CONDITION.EQUALS:
                    return " = " + str;
                case JUGGMENT_CONDITION.NOT_EQUALS:
                    return " != " + str;
                case JUGGMENT_CONDITION.INCLUDING:
                    return " LIKE '%" + str + "%' ";
                case JUGGMENT_CONDITION.NOT_INCLUDING:
                    return " NOT LIKE '%" + str + "%' ";
                case JUGGMENT_CONDITION.OR_MORE:
                    return " < " + str;
                case JUGGMENT_CONDITION.FOLLOWING:
                    return " > " + str;
                case JUGGMENT_CONDITION.LESS_THAN:
                    return " <= " + str;
                case JUGGMENT_CONDITION.MORE_THAN:
                    return " >= " + str;
                case JUGGMENT_CONDITION.IN:
                    return " IN (" + str + ") ";
                case JUGGMENT_CONDITION.NOT_IN:
                    return " NOT IN (" + str + ") ";
            }
            return String.Empty;
        }
    }
}
