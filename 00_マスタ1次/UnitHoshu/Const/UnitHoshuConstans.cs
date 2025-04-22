// $Id: UnitHoshuConstans.cs 36299 2014-12-02 03:03:49Z wenjw@oec-h.com $
using System.Collections.ObjectModel;

namespace UnitHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class UnitHoshuConstans
    {
        /// <summary>M_UNITのUNIT_CD</summary>
        public static readonly string UNIT_CD = "UNIT_CD";

        /// <summary>M_UNITのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_UNITのDELETE_FLG</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>CDのMaxLength</summary>
        public static string CD_MAXLENGTH;

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<int> fixedRowList = System.Array.AsReadOnly(new int[] { 1, 2, 3, 4, 5});

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "UNIT_CD", "UNIT_NAME", "UNIT_NAME_RYAKU", "KAMI_USE_KBN", "DENSHI_USE_KBN"});
    }
}
