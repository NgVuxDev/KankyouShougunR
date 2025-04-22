// $Id: KeijouHoshuConstans.cs 15621 2014-02-04 11:38:51Z sugioka $

using System.Collections.ObjectModel;
namespace KeijouHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class KeijouHoshuConstans
    {
        /// <summary>M_KEIJOUのKEIJOU_CD</summary>
        public static readonly string KEIJOU_CD = "KEIJOU_CD";

        /// <summary>M_KEIJOUのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_KEIJOUのDELETE_FLG</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedRowList = System.Array.AsReadOnly(new string[] { "01", "02", "03" });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "KEIJOU_CD", "KEIJOU_NAME", "KEIJOU_NAME_RYAKU" });
    }
}
