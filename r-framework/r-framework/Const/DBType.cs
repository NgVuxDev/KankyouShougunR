
using System;
namespace r_framework.Const
{
    /// <summary>
    /// DBのカラムの型enum
    /// </summary>
    public enum DB_TYPE
    {
        NONE = 0,
        DATETIME = 1,
        BIT = 2,
        SMALLINT = 3,
        VARCHAR = 4,
        INT = 5,
        MONEY = 6,
        FLOAT = 7,
        BIGINT = 8,
        DECIMAL = 9,
        TEXT = 10,
        IN_VARCHAR = 11,
        IN_INT = 12,
        IN_SMALLINT = 13,
    }

    /// <summary>
    /// DBタイプの拡張メソッド
    /// </summary>
    public static class DB_TYPEExt
    {
        /// <summary>
        /// 型名を取得する
        /// </summary>
        /// <param name="e">DB型のEnum</param>
        /// <returns>DBの型名</returns>
        public static string ToTypeString(this DB_TYPE e)
        {
            switch (e)
            {
                case DB_TYPE.VARCHAR:
                    return "varchar";
                case DB_TYPE.DATETIME:
                    return "datetime";
                case DB_TYPE.MONEY:
                    return "money";
                case DB_TYPE.INT:
                    return "int";
                case DB_TYPE.BIT:
                    return "bit";
                case DB_TYPE.SMALLINT:
                    return "smallint";
                case DB_TYPE.FLOAT:
                    return "float";
                case DB_TYPE.BIGINT:
                    return "bigint";
                case DB_TYPE.DECIMAL:
                    return "decimal";
                case DB_TYPE.TEXT:
                    return "text";
                case DB_TYPE.IN_VARCHAR:
                    return "varchar";
                case DB_TYPE.IN_INT:
                    return "int";
                case DB_TYPE.IN_SMALLINT:
                    return "smallint";
            }
            return String.Empty;
        }

        /// <summary>
        /// DBの型名からEntityにて利用される型を取得
        /// </summary>
        /// <param name="e">DB型</param>
        /// <returns>Entityで利用されるデータ型</returns>
        public static string ToEntityDataTypeString(this DB_TYPE e)
        {
            switch (e)
            {
                case DB_TYPE.VARCHAR:
                    return "String";
                case DB_TYPE.DATETIME:
                    return "SqlDateTime";
                case DB_TYPE.INT:
                    return "SqlInt32";
                case DB_TYPE.BIT:
                    return "SqlBoolean";
                case DB_TYPE.SMALLINT:
                    return "SqlInt16";
                case DB_TYPE.FLOAT:
                    return "SqlDouble";
                case DB_TYPE.BIGINT:
                    return "SqlInt64";
                case DB_TYPE.MONEY:
                case DB_TYPE.DECIMAL:
                    return "SqlDecimal";
                case DB_TYPE.TEXT:
                    return "String";
                case DB_TYPE.IN_VARCHAR:
                    return "String";
                case DB_TYPE.IN_INT:
                    return "SqlInt32";
                case DB_TYPE.IN_SMALLINT:
                    return "SqlInt16";

            }
            return String.Empty;
        }

        /// <summary>
        /// DBの型名からコンバート方式を取得する
        /// </summary>
        /// <param name="e">DBの型</param>
        /// <param name="date">登録を行うデータ</param>
        /// <returns>コンバート済みの文字列</returns>
        public static string ToConvertString(this DB_TYPE e, string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return String.Empty;
            }
            switch (e)
            {
                case DB_TYPE.VARCHAR:
                    return "'" + date + "'";
                case DB_TYPE.DATETIME:
                    return "CONVERT(datetime,'" + date + "',20)";
                case DB_TYPE.MONEY:
                    return "CONVERT(money,'" + date + "',2)";
                case DB_TYPE.INT:
                    return "CONVERT(int,'" + date + "')";
                case DB_TYPE.BIT:
                    return "CONVERT(bit,'" + date + "')";
                case DB_TYPE.SMALLINT:
                    return "CONVERT(smallint,'" + date + "')";
                case DB_TYPE.FLOAT:
                    return "CONVERT(float,'" + date + "'2)";
                case DB_TYPE.BIGINT:
                    return "CONVERT(bigint,'" + date + "')";
                case DB_TYPE.DECIMAL:
                    return "CONVERT(decimal,'" + date + "')";
                case DB_TYPE.TEXT:
                    return "'" + date + "'";
                case DB_TYPE.IN_VARCHAR:
                    return DB_TYPEExt.SetCommaSeparateString(date);
                case DB_TYPE.IN_INT:
                    return DB_TYPEExt.SetCommaSeparateNumeric(e, date);
                case DB_TYPE.IN_SMALLINT:
                    return DB_TYPEExt.SetCommaSeparateNumeric(e, date);

            }
            return String.Empty;
        }

        /// <summary>
        /// カンマ区切りの文字列に対し、シングルクォート処理を行う
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SetCommaSeparateString(string str)
        {
            string returnStr = string.Empty;
            string[] valueList = str.Replace(" ", "").Split(',');
            foreach (string tempValue in valueList)
            {
                if (string.IsNullOrEmpty(returnStr))
                {
                    returnStr = "'" + tempValue + "'";
                }
                else
                {
                    returnStr = returnStr + ", '" + tempValue + "'";
                }
            }
            return returnStr;
        }

        /// <summary>
        /// カンマ区切りの数値に対し、Convert処理を行う
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SetCommaSeparateNumeric(this DB_TYPE e, string str)
        {
            string returnStr = string.Empty;
            string[] valueList = str.Replace(" ", "").Split(',');
            foreach (string tempValue in valueList)
            {
                if (string.IsNullOrEmpty(returnStr))
                {
                    returnStr = "CONVERT(int,'" + tempValue + "')";
                }
                else
                {
                    returnStr = returnStr + ", CONVERT(" + DB_TYPEExt.ToTypeString(e) + ",'" + tempValue + "')";
                }
            }
            return returnStr;
        }
    }
}
