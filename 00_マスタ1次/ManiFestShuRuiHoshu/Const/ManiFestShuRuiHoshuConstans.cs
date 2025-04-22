// $Id: ManiFestShuRuiHoshuConstans.cs 16193 2014-02-19 08:03:22Z sp.m.miki $

using System.Collections.ObjectModel;
namespace ManiFestShuRuiHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ManiFestShuRuiHoshuConstans
    {
        /// <summary>M_MANIFEST_SHURUIのMANIFEST_SHURUI_CD</summary>
        public static readonly string MANIFEST_SHURUI_CD = "MANIFEST_SHURUI_CD";

        /// <summary>M_MANIFEST_SHURUIのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_MANIFEST_SHURUIのDELETE_FLGフラグ</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>CDのMaxLength</summary>
        public static string CD_MAXLENGTH;

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<int> fixedRowList = System.Array.AsReadOnly(new int[] { 1, 2, 9 });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "MANIFEST_SHURUI_CD", "MANIFEST_SHURUI_NAME", "MANIFEST_SHURUI_NAME_RYAKU" });
    }
}
