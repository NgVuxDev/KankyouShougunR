// $Id: ShobunHouhouHoshuConstans.cs 36299 2014-12-02 03:03:49Z wenjw@oec-h.com $

using System.Collections.ObjectModel;
namespace ShobunHouhouHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ShobunHouhouHoshuConstans
    {
        /// <summary>M_SHOBUN_HOUHOUのSHOBUN_HOUHOU_CD</summary>
        public static readonly string SHOBUN_HOUHOU_CD = "SHOBUN_HOUHOU_CD";

        /// <summary>M_SHOBUN_HOUHOUのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_SHOBUN_HOUHOUのDELETE_FLG</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedRowList = System.Array.AsReadOnly(new string[] {
        "100", "101", "102", "103", "104", "105", "106", 
        "200", "201", "202", "203", "204", "205", "206", "207", "208", "209", 
        "210", "211", "212", "213", "214", "215", "216", "217", 
        "299", 
        "300", "301", "302", "303", "304", 
        "310"
        });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "SHOBUN_HOUHOU_CD", "SHOBUN_HOUHOU_NAME", "SHOBUN_HOUHOU_NAME_RYAKU", "KAMI_USE_KBN", "DENSHI_USE_KBN" });
    }
}
