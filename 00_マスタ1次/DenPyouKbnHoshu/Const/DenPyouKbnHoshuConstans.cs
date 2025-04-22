// $Id: DenPyouKbnHoshuConstans.cs 16193 2014-02-19 08:03:22Z sp.m.miki $
using System.Collections.ObjectModel;

namespace DenPyouKbnHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class DenPyouKbnHoshuConstans
    {
        /// <summary>M_DENPYOU_KBNのDENPYOU_KBN_CD</summary>
        public static readonly string DENPYOU_KBN_CD = "DENPYOU_KBN_CD";

        /// <summary>M_DENPYOU_KBNのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_DENPYOU_KBNのDELETE_FLGフラグ</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<int> fixedRowList = System.Array.AsReadOnly(new int[] { 1, 2, 9 });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "DENPYOU_KBN_CD", "DENPYOU_KBN_NAME", "DENPYOU_KBN_NAME_RYAKU" });
    }
}
