// $Id: HoukokushoBunruiHoshuConstans.cs 36299 2014-12-02 03:03:49Z wenjw@oec-h.com $
using System.Collections.ObjectModel;

namespace HoukokushoBunruiHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    class HoukokushoBunruiHoshuConstans
    {
        /// <summary>M_HOUKOKUSHO_BUNRUIのHOUKOKUSHO_BUNRUI_CD</summary>
        public static readonly string HOUKOKUSHO_BUNRUI_CD = "HOUKOKUSHO_BUNRUI_CD";

        /// <summary>M_HOUKOKUSHO_BUNRUIのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>画面表示項目の削除フラグ</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedRowList = System.Array.AsReadOnly(new string[] { "000001", "000002", "000003", "000004", "000005", "000006", "000007", "000008", "000009", "000010", "000011", "000012", "000013", "000014", "000015", "000016", "000017", "000018", "000019", "000020", "000021", "000022", "000023", "000024", "000070", "000071", "000072", "000073", "000074", "000075", "000076", "000077", "000078", "000079", "000080", "000081", "000082", "000083", "000084", "000085", "000086", "000087", "000088", "000089", "000091", "000099" });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "HOUKOKUSHO_BUNRUI_CD" });
    }
}
