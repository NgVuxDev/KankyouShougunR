// $Id: TorihikikubunHoshuConstans.cs 16193 2014-02-19 08:03:22Z sp.m.miki $
using System.Collections.ObjectModel;

namespace TorihikikubunHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class TorihikikubunHoshuConstans
    {
        /// <summary>M_TORIHIKI_KBNのTORIHIKI_KBN_CD</summary>
        public static readonly string TORIHIKI_KBN_CD = "TORIHIKI_KBN_CD";

        /// <summary>M_TORIHIKI_KBNのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_TORIHIKI_KBNのDELETE_FLG</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<int> fixedRowList = System.Array.AsReadOnly(new int[] { 1, 2 });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "TORIHIKI_KBN_CD", "TORIHIKI_KBN_NAME", "TORIHIKI_KBN_NAME_RYAKU" });
    }
}
