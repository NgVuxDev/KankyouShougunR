namespace Shougun.Core.ExternalConnection.DenshiKeiyakuShoruiInfo
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstClass
    {
        /// <summary>M_DENSHI_KEIYAKU_SHORUI_INFOのSHORUI_INFO_ID</summary>
        public static readonly string SHORUI_INFO_ID = "SHORUI_INFO_ID";

        /// <summary>M_DENSHI_KEIYAKU_SHORUI_INFOのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_DENSHI_KEIYAKU_SHORUI_INFOのDELETE_FLGフラグ</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

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
