// $Id: NyuukinsakiNyuuryokuHoshuConstans.cs 407 2013-08-04 23:01:29Z tecs_suzuki $
namespace NyuukinsakiNyuuryokuHoshu.Const
{
    public class NyuukinsakiNyuuryokuHoshuConstans
    {
        /// <summary>
        /// 列挙型：入金先CDフォーカスアウト時チェック結果
        /// </summary>
        public enum NyuukinCdLeaveResult
        {
            /// <summary>
            /// 重複あり＋修正モード確認無し
            /// </summary>
            FALSE_NONE,

            /// <summary>
            /// 重複あり＋修正モード表示
            /// </summary>
            FALSE_ON,

            /// <summary>
            /// 重複あり＋修正モード非表示
            /// </summary>
            FALSE_OFF,

            /// <summary>
            /// 重複なし
            /// </summary>
            TURE_NONE
        }

        /// <summary>M_NYUUKINSAKIのNYUUKINSAKI_CD</summary>
        public static readonly string NYUUKINSAKI_CD = "NYUUKINSAKI_CD";

        /// <summary>M_NYUUKINSAKI_FURIKOMIのFURIKOMI_NAME</summary>
        public static readonly string FURIKOMI_NAME = "FURIKOMI_NAME";

        /// <summary>入力最大バイト数の定数名（CharactersNumber)</summary>
        public static readonly string CHARACTERS_NUMBER = "CharactersNumber";

        /// <summary>フリコミ人名一覧のヘッダーセクション名</summary>
        public static readonly string COLUMN_HEADER_SECTION_NAME = "columnHeaderSection1";

        /// <summary>フリコミ人名一覧のチェックボックス名</summary>
        public static readonly string CUSTOM_CHECKBOX_NAME = "gcCustomCheckBoxCell1";

        /// <summary>フリコミ人名一覧の行チェックボックス名</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

    }
}
