// $Id: HaikiKbnHoshuConstans.cs 16193 2014-02-19 08:03:22Z sp.m.miki $
using System.Collections.ObjectModel;

namespace HaikiKbnHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class HaikiKbnHoshuConstans
    {
        /// <summary>M_HAIKI_KBNのHAIKI_KBN_CD</summary>
        public static readonly string HAIKI_KBN_CD = "HAIKI_KBN_CD";

        /// <summary>M_HAIKI_KBNのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_HAIKI_KBNのDELETE_FLG</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>CDのMaxLength</summary>
        public static string CD_MAXLENGTH;

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<int> fixedRowList = System.Array.AsReadOnly(new int[] { 1, 2, 3, 4 });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "HAIKI_KBN_CD", "HAIKI_KBN_NAME" });
    }
}
