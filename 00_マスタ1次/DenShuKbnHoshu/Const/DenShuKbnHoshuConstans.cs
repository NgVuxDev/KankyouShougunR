// $Id: DenShuKbnHoshuConstans.cs 15621 2014-02-04 11:38:51Z sugioka $
using System.Collections.ObjectModel;

namespace DenShuKbnHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class DenShuKbnHoshuConstans
    {
        /// <summary>M_DENSHU_KBNのDENSHU_KBN_CD</summary>
        public static readonly string DENSHU_KBN_CD = "DENSHU_KBN_CD";

        /// <summary>M_DENSHU_KBNのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_DENSHU_KBNのDELETE_FLGフラグ</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>CDのMaxLength</summary>
        public static string CD_MAXLENGTH;

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<int> fixedRowList = System.Array.AsReadOnly(new int[] { 1, 2, 3, 9, 10, 20, 30, 40, 50, 60, 80, 90, 100, 110, 120, 130, 140, 150, 151, 155, 156, 160, 170, 180, 181, 182, 190, 191, 200, 210, 220, 230, 240, 250, 260 });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "DENSHU_KBN_CD", "DENSHU_KBN_NAME", "DENSHU_KBN_NAME_RYAKU" });
    }
}
