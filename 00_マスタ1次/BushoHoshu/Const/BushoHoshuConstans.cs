// $Id: BushoHoshuConstans.cs 36299 2014-12-02 03:03:49Z wenjw@oec-h.com $

using System.Collections.ObjectModel;
namespace BushoHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class BushoHoshuConstans
    {
        /// <summary>M_BUSHOのBUSHO_CD</summary>
        public static readonly string BUSHO_CD = "BUSHO_CD";

        /// <summary>M_BUSHOのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>画面表示項目の削除フラグ</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>M_BUSHOのBUSHO_NAME_RYAKU</summary>
        public static readonly string BUSHO_NAME_RYAKU = "BUSHO_NAME_RYAKU";

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedRowList = System.Array.AsReadOnly(new string[] { "999" });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "BUSHO_CD", "BUSHO_NAME", "BUSHO_NAME_RYAKU", "BUSHO_FURIGANA" });
    }
}
