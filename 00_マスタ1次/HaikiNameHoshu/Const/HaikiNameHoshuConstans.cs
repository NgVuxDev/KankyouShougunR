// $Id: HaikiNameHoshuConstans.cs 20275 2014-05-07 02:57:20Z gai $
using System.Collections.ObjectModel;

namespace HaikiNameHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class HaikiNameHoshuConstans
    {
        /// <summary>M_HAIKI_NAMEのHAIKI_NAME_CD</summary>
        public static readonly string HAIKI_NAME_CD = "HAIKI_NAME_CD";

        /// <summary>M_HAIKI_NAMEのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>画面表示項目の削除フラグ</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedRowList = System.Array.AsReadOnly(new string[] { /*"000001", "000002", "000003", "000004", "000005", "000006", "000007", "000008", "000009", "000010", "000011", "000012", "000013", "000014", "000015", "000016", "000017", "000018", "000019", "000020", "000021", "000022", "000023", "000024", "000025", "000026", "000027", "000028", "000029", "000030", "000031", "000032", "000033", "000034", "000035", "000036", "000037", "000038", "000039", "000040", "000041", "000042", "000043", "000044", "000045", "000046", "000047", "000048", "000049", "000050", "000051"*/ });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "HAIKI_NAME_CD", "HAIKI_NAME", "HAIKI_NAME_RYAKU" });
    }
}
