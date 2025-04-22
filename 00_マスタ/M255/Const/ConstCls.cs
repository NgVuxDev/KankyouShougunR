// $Id: ConstCls.cs 16965 2014-03-05 12:02:04Z y-sato $
using System.Collections.ObjectModel;

namespace Shougun.Core.Master.ManiFestTeHaiHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>M_MANIFEST_TEHAIのMANIFEST_TEHAI_CD</summary>
        public static readonly string MANIFEST_TEHAI_CD = "MANIFEST_TEHAI_CD";

        /// <summary>M_MANIFEST_TEHAIのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_MANIFEST_TEHAIのDELETE_FLGフラグ</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>変更不可処理を行うCDリスト</summary>
        public static ReadOnlyCollection<string> FixedRowList = System.Array.AsReadOnly(new string[] { "1", "2", "9" });

        /// <summary>変更不可処理を行う項目リスト</summary>
        public static ReadOnlyCollection<string> FixedColumnList = System.Array.AsReadOnly(new string[] { "MANIFEST_TEHAI_CD", "MANIFEST_TEHAI_NAME", "MANIFEST_TEHAI_NAME_RYAKU", "DELETE_FLG" });

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public class ExceptionErrMsg
        {
            public const string HAITA = "排他エラーが発生しました。";
            public const string REIGAI = "例外エラーが発生しました。";
        }
    }
}
