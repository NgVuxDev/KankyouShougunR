// $Id:UnpanHouhouHoshuConstans.cs 199 2013-06-25 10:02:50Z tecs_suzuki $
using System.Collections.ObjectModel;
namespace UnpanHouhouHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class UnpanHouhouHoshuConstans
    {
        /// <summary>M_UNPAN_HOUHOUのUNPAN_HOUHOU_CD</summary>
        public static readonly string UNPAN_HOUHOU_CD = "UNPAN_HOUHOU_CD";

        /// <summary>M_UNPAN_HOUHOUのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_UNPAN_HOUHOUのDELETE_FLG</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedRowList = System.Array.AsReadOnly(new string[] { "1", "2", "3", "4" });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "UNPAN_HOUHOU_CD", "UNPAN_HOUHOU_NAME", "UNPAN_HOUHOU_NAME_RYAKU", "KAMI_USE_KBN", "DENSHI_USE_KBN" });
    }
}
