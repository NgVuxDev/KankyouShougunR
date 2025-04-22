// $Id: TodouFukenHoshuConstans.cs 36299 2014-12-02 03:03:49Z wenjw@oec-h.com $
using System.Collections.ObjectModel;

namespace TodouFukenHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class TodouFukenHoshuConstans
    {
        /// <summary>M_TODOUFUKENのTODOUFUKEN_CD</summary>
        public static readonly string TODOUFUKEN_CD = "TODOUFUKEN_CD";

        /// <summary>M_TODOUFUKENのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_TODOUFUKENのDELETE_FLG</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>CDのMaxLength</summary>
        public static string CD_MAXLENGTH;

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<int> fixedRowList = System.Array.AsReadOnly(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47 });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "TODOUFUKEN_CD", "TODOUFUKEN_NAME", "TODOUFUKEN_NAME_RYAKU", "TODOUFUKEN_NAME_FURIGANA"});
    }
}
