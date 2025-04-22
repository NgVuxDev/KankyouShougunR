// $Id: KyotenHoshuConstans.cs 36299 2014-12-02 03:03:49Z wenjw@oec-h.com $

using System.Collections.ObjectModel;
namespace KyotenHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class KyotenHoshuConstans
    {
        /// <summary>M_KYOTENのKYOTEN_CD</summary>
        public static readonly string KYOTEN_CD = "KYOTEN_CD";

        /// <summary>M_KYOTENのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_KYOTENのDELETE_FLG</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>CDのMaxLength</summary>
        public static string CD_MAXLENGTH;

        /// <summary>
        /// 変更不可処理を行うCDリスト
        /// </summary>
        public static ReadOnlyCollection<int> fixedRowList = System.Array.AsReadOnly(new int[] { 0, 99 });

        /// <summary>
        /// 変更不可処理を行う項目リスト
        /// </summary>
        public static ReadOnlyCollection<string> fixedColumnList = System.Array.AsReadOnly(new string[] { "DELETE_FLG", "KYOTEN_CD", "KYOTEN_NAME", "KYOTEN_NAME_RYAKU", "KYOTEN_FURIGANA", "KYOTEN_POST"});

        public static ReadOnlyCollection<string> fixedColumnListForKyoten0 = System.Array.AsReadOnly(new string[] { "KYOTEN_NAME", "KYOTEN_NAME_RYAKU", "KYOTEN_FURIGANA" });

        public static readonly string KYOTEN_POST = "KYOTEN_POST";
        public static readonly string KYOTEN_ADDRESS1 = "KYOTEN_ADDRESS1";
    }
}
