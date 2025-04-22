// $Id: ShouhizeiHoshuConstans.cs 15621 2014-02-04 11:38:51Z sugioka $

using System.Collections.ObjectModel;
namespace ShouhizeiHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ShouhizeiHoshuConstans
    {
        /// <summary>M_SHOUHIZEIのSYS_ID</summary>
        public static readonly string SYS_ID = "SYS_ID";

        /// <summary>M_SHOUHIZEIのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_SHOUHIZEIのDELETE_FLG</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>M_SHOUHIZEIのSHOUHIZEI_RATE</summary>
        public static readonly string SHOUHIZEI_RATE = "SHOUHIZEI_RATE";

        /// <summary>M_SHOUHIZEIのTEKIYOU_BEGIN</summary>
        public static readonly string TEKIYOU_BEGIN = "TEKIYOU_BEGIN";

        /// <summary>M_SHOUHIZEIのTEKIYOU_END</summary>
        public static readonly string TEKIYOU_END = "TEKIYOU_END";

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<int> fixedRowList = System.Array.AsReadOnly(new int[] { 1 });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "SYS_ID", "SHOUHIZEI_RATE", "TEKIYOU_BEGIN", "TEKIYOU_END" });
    }
}
