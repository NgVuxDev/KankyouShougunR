// $Id: ItakuKeiyakuHoshuConstants.cs 39862 2015-01-16 13:15:36Z huangxy@oec-h.com $
namespace ItakuKeiyakuHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ItakuKeiyakuHoshuConstans
    {
        /// <summary>
        /// 列挙型：入金先CDフォーカスアウト時チェック結果
        /// </summary>
        public enum SystemIdLeaveResult
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

        /// <summary>M_ITAKU_KEIYAKU_KIHONのSYSTEM_ID</summary>
        public static readonly string SYSTEM_ID = "SYSTEM_ID";

        /// <summary>M_ITAKU_KEIYAKU_KIHONのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_ITAKU_KEIYAKU_KIHONのDELETE_FLG</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>入力最大バイト数の定数名（CharactersNumber)</summary>
        public static readonly string CHARACTERS_NUMBER = "CharactersNumber";

        /// <summary>Report設定用XMLファイルパス(産廃・運搬)</summary>
        internal static readonly string ReportInfoXmlPath01 = "./Template/R001_01Report.xml";

        /// <summary>Report設定用XMLファイルパス(産廃・処分)</summary>
        internal static readonly string ReportInfoXmlPath02 = "./Template/R001_02Report.xml";

        /// <summary>Report設定用XMLファイルパス(産廃・運搬/処分)</summary>
        internal static readonly string ReportInfoXmlPath03 = "./Template/R001_03Report.xml";

        /// <summary>Report設定用XMLファイルパス(建廃)</summary>
        internal static readonly string ReportInfoXmlPathKenpai = "./Template/R546_01Report.xml";

        /// <summary>出力用セパレータ</summary>
        public static readonly string OUTPUT_SEPARATOR = "、";

        /// <summary>運搬区間(FROM)：排出</summary>
        public static readonly short UNPAN_ST_HST = 1;

        /// <summary>運搬区間(FROM)：積替・保管施設</summary>
        public static readonly short UNPAN_ST_TSUMIHO = 2;

        /// <summary>運搬区間(TO)：積替・保管施設</summary>
        public static readonly short UNPAN_END_TSUMIHO = 2; // No.3447

        /// <summary>運搬区間(TO)：処分施設</summary>
        public static readonly short UNPAN_END_SBN = 3; // No.3447

        /// <summary>地域別許可番号：許可区分(運搬)</summary>
        public static readonly short KYOKA_KBN_UPN = 1;

        /// <summary>地域別許可番号：許可区分(処分)</summary>
        public static readonly short KYOKA_KBN_SBN = 2;

        /// <summary>地域別許可番号：許可区分(最終処分)</summary>
        public static readonly short KYOKA_KBN_LASTSBN = 3;

        /// <summary>許可の有効期限（固定値）</summary>
        public static readonly string KYOUKA_KIGEN = "許可証記載の通り";

        /// <summary>事業範囲（固定値）</summary>
        public static readonly string JIGYOU_HANI = "許可証記載の通り";

        /// <summary>産業廃棄物の種類（固定値）</summary>
        public static readonly string HAIKI_SHURUI = "許可証記載の通り";

        /// <summary>許可条件（固定値）</summary>
        public static readonly string KYOKA_JOUKEN = "許可証記載の通り";

        /// <summary>契約日（固定値）</summary>
        public static readonly string KEIYAKUSHO_KEIYAKU_DATE_INIT = "　　　　年　　　月　　　日";

        /// <summary>契約日（フォーマット）</summary>
        public static readonly string KEIYAKUSHO_KEIYAKU_DATE_FORMAT = "　yyyy　年　MM　月　dd　日";

        /// <summary>日付（フォーマット）</summary>
        public static readonly string OUTPUT_DATE_FORMAT = "yyyy年MM月dd日";

        /// <summary>日付（フォーマット）</summary>
        public static readonly string OUTPUT_DATE_WAREKI_FORMAT = "gyy年MM月dd日";

        /// <summary>日付（フォーマット）</summary>
        public static readonly string OUTPUT_YUUKOU_KIKAN_WAREKI_FORMAT = "g  yy年  MM月  dd日";

        /// <summary>日付（フォーマット）初期値</summary>
        public static readonly string OUTPUT_DATE_WAREKI_FORMAT_INIT = "g    年    月    日";

        /// <summary>第14条(更新種別：自動更新, 有効期限：一年間)</summary>
        public static readonly string KEIYAKUSHO_KIKAN_1 = "本契約は、有効期間を{0}から{1}までの{2}とし、期間満了の１ヶ月前までに、甲乙の一方から相手方に対する書面による解約の申し入れがない限り、同一条件で更新されたものとし、その後も同様とする。";

        /// <summary>第14条(更新種別：単発, 有効期限：一年間以外)</summary>
        public static readonly string KEIYAKUSHO_KIKAN_2 = "本契約は、有効期間を{0}から{1}までとする。";

        /// <summary>第14条(更新種別：自動更新, 有効期限：一年間以外)</summary>
        public static readonly string KEIYAKUSHO_KIKAN_3 = "本契約は、有効期限を{0}から{1}までとし、期間満了の１ヶ月前までに、甲乙の一方から相手方に対する書面による解約の申し入れがない限り、同一条件で更新されたものとし、その後も同様とする。";

        /// <summary>第14条(更新種別：単発, 有効期限：一年間)</summary>
        public static readonly string KEIYAKUSHO_KIKAN_4 = "本契約は、有効期限を{0}から{1}までの{2}とする。";

        /// <summary>処理能力単位</summary>
        public static readonly string SHORI_SPEC_UNIT = "（ｔ／日）";

        /// <summary>委託契約書種類:処分</summary>
        public static readonly short ITAKU_KEIYAKU_SHURUI_SBN = 2;

        /// <summary>委託契約書種類:収集・運搬</summary>
        public static readonly short ITAKU_KEIYAKU_SHURUI_UPN = 1;

        /// <summary>委託契約書種類:収集・運搬/処分</summary>
        public static readonly short ITAKU_KEIYAKU_SHURUI_UPNSBN = 3;

        /// <summary>委託契約書登録方法:詳細</summary>
        public static readonly short ITAKU_KEIYAKU_TOUROKU_HOUHOU_SYOUSAI = 1;

        /// <summary>委託契約書登録方法:基本</summary>
        public static readonly short ITAKU_KEIYAKU_TOUROKU_HOUHOU_KIHON = 2;

        public enum FocusSwitch
        {
            NONE,
            IN,
            OUT
        }
    }
}