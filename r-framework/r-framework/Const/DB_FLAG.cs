using System;

namespace r_framework.Const
{
    /// <summary>
    /// DBにて利用されるBoolフラグ
    /// </summary>
    public enum DB_FLAG
    {
        FALSE = 0,
        TRUE = 1
    };

    /// <summary>
    /// DBフラグの拡張メソッド
    /// </summary>
    public static class DB_FLAGExt
    {
        /// <summary>
        /// DBにて利用されるBoolフラグからEntityにて利用される値を取得
        /// </summary>
        /// <param name="e">DB型</param>
        /// <returns>Entityで利用される値</returns>
        public static string ToEntityDataTypeString(this DB_FLAG e)
        {
            switch (e)
            {
                case DB_FLAG.FALSE:
                    return "False";
                case DB_FLAG.TRUE:
                    return "True";
            }
            return String.Empty;
        }
    }
}
