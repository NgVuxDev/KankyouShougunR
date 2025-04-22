// $Id: GyoushaHoshuConstans.cs 199 2013-06-25 10:02:50Z tecs_suzuki $

namespace GyoushaHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class GyoushaHoshuConstans
    {
        /// <summary>
        /// 列挙型：入金先CDフォーカスアウト時チェック結果
        /// </summary>
        public enum GyoushaCdLeaveResult
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
        
        /// <summary>
        /// 列挙型：取引先区分に基づくコントロールの変更処理
        /// </summary>
        public enum TorihikisakiKbnProcessType
        {
            /// <summary>
            /// 請求にともなう処理
            /// </summary>
            Seikyuu,

            /// <summary>
            /// 支払にともなう処理
            /// </summary>
            Siharai
        }

        /// <summary>M_GYOUSHAOのGYOUSHA_CD</summary>
        public static readonly string GYOUSHA_CD = "GYOUSHA_CD";

        /// <summary>M_GYOUSHAのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>画面表示項目の削除フラグ</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>入力最大バイト数の定数名（CharactersNumber)</summary>
        public static readonly string CHARACTERS_NUMBER = "CharactersNumber";

        /// <summary>M_GYOUSHAのGYOUSHA_NAME_RYAKU</summary>
        public static readonly string GYOUSHA_NAME_RYAKU = "GYOUSHA_NAME_RYAKU";

        /// <summary>M_ITAKU_KEIYAKU_KIHONのITAKU_KEIYAKU_SHURUI</summary>
        public static readonly string ITAKU_SHURUI = "ITAKU_SHURUI";

        /// <summary>M_ITAKU_KEIYAKU_KIHONのITAKU_KEIYAKU_STATUS</summary>
        public static readonly string ITAKU_STATUS = "ITAKU_STATUS";

        public const string MSG_CONF_B = "請求書書式１が業者別になっています。請求書送付先が未設定ですがよろしいですか？";
        public const string MSG_CONF_C = "請求書書式１が業者別になっています。送付先住所が未設定ですがよろしいですか？";
        public const string MSG_CONF_D = "支払明細書書式１が業者別になっています。支払明細書送付先が未設定ですがよろしいですか？";
        public const string MSG_CONF_E = "支払明細書書式１が業者別になっています。送付先住所が未設定ですがよろしいですか？";

	}
}
