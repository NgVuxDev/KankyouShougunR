// $Id: NisugataHoshuConstans.cs 36299 2014-12-02 03:03:49Z wenjw@oec-h.com $
using System.Collections.ObjectModel;

namespace NisugataHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class NisugataHoshuConstans
    {
        /// <summary>M_NISUGATAのNISUGATA_CD</summary>
        public static readonly string NISUGATA_CD = "NISUGATA_CD";

        /// <summary>M_NISUGATAのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_NISUGATAのDELETE_FLG</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedRowList = System.Array.AsReadOnly(new string[] { "01", "02", "03", "04", "05", "06", "07", "08", "09" });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "NISUGATA_CD", "NISUGATA_NAME", "NISUGATA_NAME_RYAKU", "KAMI_USE_KBN", "DENSHI_USE_KBN"});
    }
}
