// $Id:NyuushukkinKbnHoshuConstans.cs 199 2013-06-25 10:02:50Z tecs_suzuki $

using System.Collections.ObjectModel;
namespace NyuushukkinKbnHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class NyuushukkinKbnHoshuConstans
    {
        /// <summary>M_NYUUSHUKKIN_KBNのNYUUSHUKKIN_KBN_CD</summary>
        public static readonly string NYUUSHUKKIN_KBN_CD = "NYUUSHUKKIN_KBN_CD";

        /// <summary>M_NYUUSHUKKIN_KBNのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_NYUUSHUKKIN_KBNのDELETE_FLGフラグ</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>CDのMaxLength</summary>
        public static string CD_MAXLENGTH;

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<int> fixedRowList = System.Array.AsReadOnly(new int[] { 1, 2, 5, 21, 22, 51 });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "NYUUSHUKKIN_KBN_CD", "NYUUSHUKKIN_KBN_NAME", "NYUUSHUKKIN_KBN_NAME_RYAKU"});
    }
}
