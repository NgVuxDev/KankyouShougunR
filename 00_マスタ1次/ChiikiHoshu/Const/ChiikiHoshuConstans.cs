// $Id: ChiikiHoshuConstans.cs 45367 2015-03-23 05:49:40Z sanbongi $
using System.Collections.ObjectModel;

namespace ChiikiHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ChiikiHoshuConstans
    {
        /// <summary>M_CHIIKIのCHIIKI_CD</summary>
        public static readonly string CHIIKI_CD = "CHIIKI_CD";

        /// <summary>M_CHIIKIのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_CHIIKIのDELETE_FLG</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>M_TODOUFUKENのTODOUFUKEN_CD</summary>
        public static readonly string TODOUFUKEN_CD = "TODOUFUKEN_CD";

        /// <summary>M_TODOUFUKENのTODOUFUKEN_NAME_RYAKU</summary>
        public static readonly string TODOUFUKEN_NAME_RYAKU = "TODOUFUKEN_NAME_RYAKU";

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedRowList = System.Array.AsReadOnly(new string[] { 
            "000001", "000002", "000003", "000004", "000005", "000006", "000007", "000008", "000009", "000010", 
            "000011", "000012", "000013", "000014", "000015", "000016", "000017", "000018", "000019", "000020", 
            "000021", "000022", "000023", "000024", "000025", "000026", "000027", "000028", "000029", "000030", 
            "000031", "000032", "000033", "000034", "000035", "000036", "000037", "000038", "000039", "000040", 
            "000041", "000042", "000043", "000044", "000045", "000046", "000047",                     "000050", 
            "000051", "000052",           "000054", "000055", "000056", "000057", "000058", "000059", "000060", 
            "000061", "000062", "000063", "000064", "000065", "000066", "000067", "000068", "000069", "000070", 
            "000071", "000072", "000073", "000074", "000075", "000076", "000077", "000078", "000079", "000080", 
            "000081", "000082", "000083", "000084", "000085", "000086", "000087", "000088", "000089", "000090", 
            "000091", "000092", "000093", "000094", "000095", "000096", "000097", "000098", "000099", "000100", 
            "000101", "000102", "000103", "000104", "000105", "000106",           "000108", "000109", "000110", 
            "000111", "000112",           "000114", "000115", "000116",           "000118", "000119", "000120",
            "000121", "000122",           "000124", "000125", "000126", "000127", "000128", "000129", "000130",
            "000131", "000132", "000133"
        });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "CHIIKI_CD", "CHIIKI_NAME", "CHIIKI_NAME_RYAKU", "CHIIKI_FURIGANA", "TODOUFUKEN_CD"});
    }
}
