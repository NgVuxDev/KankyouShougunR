using System.Collections.ObjectModel;

namespace HaikiShuruiHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class HaikiShuruiHoshuConstans
    {
        /// <summary>M_HAIKI_SHURUIのHAIKI_SHURUI_CD</summary>
        public static readonly string HAIKI_SHURUI_CD = "HAIKI_SHURUI_CD";

        /// <summary>M_HAIKI_SHURUIのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>画面表示項目の削除フラグ</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>CDのMaxLength</summary>
        public static string CD_MAXLENGTH;

        /// <summary>
        /// 変更不可処理を行うCDリスト(廃棄物区分CD:1)
        /// </summary>
        public static ReadOnlyCollection<string> fixedRowListKbn1 = System.Array.AsReadOnly(new string[] { "0100", "0200", "0300", "0400", "0500", "0600", "0700", "0800", "0900", "1000", "1100", "1200", "1300", "1400", "1500", "1600", "1700", "1701", "1800", "1900", "4000", "4001", "4100", "4200", "5000", "7000", "7010", "7100", "7110", "7200", "7210", "7300", "7410", "7421", "7422", "7423", "7424", "7425", "7426", "7427", "7428", "7429", "7430", "7440", "9999" });

        /// <summary>
        /// 変更不可処理を行うCDリスト(廃棄物区分CD:2)
        /// </summary>
        public static ReadOnlyCollection<string> fixedRowListKbn2 = System.Array.AsReadOnly(new string[] { "0100", "0200", "0300", "0400", "0500", "0600", "0700", "0800", "1100", "1200", "1300", "1400", "1500", "1600", "1700", "2100", "2101", "2102" });

        /// <summary>
        /// 変更不可処理を行うCDリスト(廃棄物区分CD:3)
        /// </summary>
        public static ReadOnlyCollection<string> fixedRowListKbn3 = System.Array.AsReadOnly(new string[] { "0100", "0200", "0300", "0400", "0500", "0600", "0700", "0800", "0900", "1000", "1100", "1200", "1300", "1400", "1500", "1600", "1700", "1701", "1800", "1900", "2100", "4000", "4001", "4100", "4200", "5000", "7000", "7010", "7100", "7110", "7200", "7210", "7300", "7410", "7421", "7422", "7423", "7424", "7425", "7426", "7427", "7428", "7429", "7430", "7440", "9999" });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "HAIKI_SHURUI_CD", "HAIKI_SHURUI_NAME", "HAIKI_SHURUI_NAME_RYAKU", "HAIKI_SHURUI_FURIGANA"});
    }
}
