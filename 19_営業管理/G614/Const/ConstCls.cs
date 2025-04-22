// $Id: ConstCls.cs 26055 2014-07-18 05:17:50Z katen_koec $

namespace Shougun.Core.BusinessManagement.GenbaKakunin.Const
{

    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>
        /// 列挙型：入金先CDフォーカスアウト時チェック結果
        /// </summary>
        public enum GenbaCdLeaveResult
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

        /// <summary>M_GENBAのGYOUSHA_CD</summary>
        public static readonly string GYOUSHA_CD = "GYOUSHA_CD";

        /// <summary>M_GENBAのGENBA_CD</summary>
        public static readonly string GENBA_CD = "GENBA_CD";

        /// <summary>M_GENBAのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_GENBAのDELETE_FLG</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>入力最大バイト数の定数名（CharactersNumber)</summary>
        public static readonly string CHARACTERS_NUMBER = "CharactersNumber";

        /// <summary>M_GENBAのGENBA_NAME_RYAKU</summary>
        public static readonly string GENBA_NAME_RYAKU = "GENBA_NAME_RYAKU";

        /// <summary>M_ITAKU_KEIYAKU_KIHONのITAKU_KEIYAKU_SHURUI</summary>
        public static readonly string ITAKU_SHURUI = "ITAKU_KEIYAKU_SHURUI";

        /// <summary>M_ITAKU_KEIYAKU_KIHONのITAKU_KEIYAKU_STATUS</summary>
        public static readonly string ITAKU_STATUS = "ITAKU_KEIYAKU_STATUS";

        /// <summary>M_GENBA_TEIKI_HINMEIのHINMEI_CD</summary>
        public static readonly string TEIKI_HINMEI_CD = "HINMEI_CD";

        /// <summary>M_GENBA_TEIKI_HINMEIのHINMEI_NAME_RYAKU</summary>
        public static readonly string TEIKI_HINMEI_NAME_RYAKU = "HINMEI_NAME_RYAKU";

        /// <summary>M_GENBA_TEIKI_HINMEIのUNIT_CD</summary>
        public static readonly string TEIKI_UNIT_CD = "UNIT_CD";

        /// <summary>M_GENBA_TEIKI_HINMEIのUNIT_NAME_RYAKU</summary>
        public static readonly string TEIKI_UNIT_NAME_RYAKU = "UNIT_NAME_RYAKU";

        ///// <summary>M_GENBA_TEIKI_HINMEIのSTANDARD_VALUE</summary>
        //public static readonly string TEIKI_STANDARD_VALUE = "STANDARD_VALUE";

        ///// <summary>M_GENBA_TEIKI_HINMEIのYOUKI_CD</summary>
        //public static readonly string TEIKI_YOUKI_CD = "YOUKI_CD";

        /// <summary>M_GENBA_TEIKI_HINMEIのKANSANCHI</summary>
        public static readonly string TEIKI_KANSANCHI = "KANSANCHI";

        /// <summary>M_GENBA_TEIKI_HINMEIのKANSAN_UNIT_NAME_CD</summary>
        public static readonly string TEIKI_KANSAN_UNIT_CD = "KANSAN_UNIT_CD";

        /// <summary>M_GENBA_TEIKI_HINMEIのKANSAN_UNIT_NAME_RYAKU</summary>
        public static readonly string TEIKI_KANSAN_UNIT_NAME_RYAKU = "KANSAN_UNIT_NAME_RYAKU";

        /// <summary>M_GENBA_TEIKI_HINMEIの要記入</summary>
        public static readonly string KANSAN_UNIT_MOBILE_OUTPUT_FLG = "KANSAN_UNIT_MOBILE_OUTPUT_FLG";

        /// <summary>M_GENBA_TEIKI_HINMEIのMONDAY</summary>
        public static readonly string TEIKI_MONDAY = "MONDAY";

        /// <summary>M_GENBA_TEIKI_HINMEIのTUESDAY</summary>
        public static readonly string TEIKI_TUESDAY = "TUESDAY";

        /// <summary>M_GENBA_TEIKI_HINMEIのWEDNESDAY</summary>
        public static readonly string TEIKI_WEDNESDAY = "WEDNESDAY";

        /// <summary>M_GENBA_TEIKI_HINMEIのTHURSDAY</summary>
        public static readonly string TEIKI_THURSDAY = "THURSDAY";

        /// <summary>M_GENBA_TEIKI_HINMEIのFRIDAY</summary>
        public static readonly string TEIKI_FRIDAY = "FRIDAY";

        /// <summary>M_GENBA_TEIKI_HINMEIのSATURDAY</summary>
        public static readonly string TEIKI_SATURDAY = "SATURDAY";

        /// <summary>M_GENBA_TEIKI_HINMEIのSUNDAY</summary>
        public static readonly string TEIKI_SUNDAY = "SUNDAY";

        /// <summary>M_GENBA_TEIKI_HINMEIのKANSAN_KEIYAKU_KBN</summary>
        public static readonly string TEIKI_KEIYAKU_KBN = "KEIYAKU_KBN";

        /// <summary>M_GENBA_TEIKI_HINMEIのKANSAN_KEIYAKU_KBN_NAME</summary>
        public static readonly string TEIKI_KEIYAKU_KBN_NAME = "KEIYAKU_KBN_NAME";

        /// <summary>M_GENBA_TEIKI_HINMEIのTSUKI_HINMEI_CD</summary>
        public static readonly string TEIKI_TSUKI_HINMEI_CD = "TSUKI_HINMEI_CD";

        /// <summary>M_GENBA_TEIKI_HINMEIのTSUKI_HINMEI_NAME_RYAKU</summary>
        public static readonly string TEIKI_TSUKI_HINMEI_NAME_RYAKU = "TSUKI_HINMEI_NAME_RYAKU";

        /// <summary>M_GENBA_TEIKI_HINMEIのKEIJYOU_KBN</summary>
        public static readonly string TEIKI_KEIJYOU_KBN = "KEIJYOU_KBN";

        /// <summary>M_GENBA_TEIKI_HINMEIのKEIJYOU_KBN_NAME</summary>
        public static readonly string TEIKI_KEIJYOU_KBN_NAME = "KEIJYOU_KBN_NAME";

        ///// <summary>M_GENBA_TEIKI_HINMEIのKAISHUU_DATE_END</summary>
        //public static readonly string TEIKI_KAISHUU_DATE_END = "KAISHUU_DATE_END";

        /// <summary>M_GENBA_TSUKI_HINMEIのHINMEI_CD</summary>
        public static readonly string TSUKI_HINMEI_CD = "HINMEI_CD";

        /// <summary>M_GENBA_TEIKI_HINMEIのTSUKI_HINMEI_NAME_RYAKU</summary>
        public static readonly string TSUKI_HINMEI_NAME_RYAKU = "HINMEI_NAME_RYAKU";

        /// <summary>M_GENBA_TSUKI_HINMEIのUNIT_CD</summary>
        public static readonly string TSUKI_UNIT_CD = "UNIT_CD";

        /// <summary>M_GENBA_TSUKI_HINMEIのUNIT_NAME_RYAKU</summary>
        public static readonly string TSUKI_UNIT_NAME_RYAKU = "UNIT_NAME_RYAKU";

        /// <summary>M_GENBA_TSUKI_HINMEIのTANKA</summary>
        public static readonly string TSUKI_TANKA = "TANKA";

        /// <summary>M_GENBA_TSUKI_HINMEIのCHOUKA_SETTING</summary>
        public static readonly string TSUKI_CHOUKA_SETTING = "CHOUKA_SETTING";

        /// <summary>M_GENBA_TSUKI_HINMEIのCHOUKA_LIMIT_AMOUNT</summary>
        public static readonly string TSUKI_CHOUKA_LIMIT_AMOUNT = "CHOUKA_LIMIT_AMOUNT";

        /// <summary>M_GENBA_TSUKI_HINMEIのCHOUKA_HINMEI_NAME</summary>
        public static readonly string TSUKI_CHOUKA_HINMEI_NAME = "CHOUKA_HINMEI_NAME";

        /// <summary>M_GENBA_TSUKI_HINMEIのTEIKI_JISSEKI_NO_SEIKYUU_KBN</summary>
        public static readonly string TSUKI_TEIKI_JISSEKI_NO_SEIKYUU_KBN = "TEIKI_JISSEKI_NO_SEIKYUU_KBN";

        /// <summary>M_GENBA_TSUKI_HINMEIのHINMEI_CD</summary>
        public static readonly string TEIKI_DENPYOU_KBN_CD = "DENPYOU_KBN_CD";

        /// <summary>M_GENBA_TSUKI_HINMEIのHINMEI_CD</summary>
        public static readonly string TEIKI_DENPYOU_KBN_NAME_RYAKU = "DENPYOU_KBN_NAME_RYAKU";

        /// <summary>M_GENBA_TSUKI_HINMEIのHINMEI_CD</summary>
        public static readonly string TSUKI_DENPYOU_KBN_CD = "DENPYOU_KBN_CD";

        /// <summary>M_GENBA_TSUKI_HINMEIのHINMEI_CD</summary>
        public static readonly string TSUKI_DENPYOU_KBN_NAME_RYAKU = "DENPYOU_KBN_NAME_RYAKU";

        /// <summary>
        /// 伝票区分CD(売上)文字列
        /// </summary>
        public const string DENPYOU_KBN_CD_URIAGE_STR = "1";

        /// <summary>
        /// 伝票区分CD(支払)文字列
        /// </summary>
        public const string DENPYOU_KBN_CD_SHIHARAI_STR = "2";

        /// <summary>
        /// 契約区分CD(定期)文字列
        /// </summary>
        public static readonly string KEIYAKU_KBN_CD_TEIKI = "1";

        /// <summary>
        /// 契約区分名
        /// </summary>
        public static readonly string KEIYAKU_KBN_NAME_TEIKI = "定期";
        public static readonly string KEIYAKU_KBN_NAME_TANKA = "単価";
        public static readonly string KEIYAKU_KBN_NAME_NASHI = "回収のみ";

        /// <summary>
        /// 集計区分名
        /// </summary>
        public static readonly string KEIJYOU_KBN_NAME_DENPYOU = "伝票";
        public static readonly string KEIJYOU_KBN_NAME_GASSAN = "合算";

        // 20140718 katen No.5292 引合現場入力-定期回収情報タブに実数項目が無い start‏
        /// <summary>M_GENBA_TEIKI_ANBUN_FLGのMONDAY</summary>
        public static readonly string TEIKI_ANBUN_FLG = "ANBUN_FLG";
        // 20140718 katen No.5292 引合現場入力-定期回収情報タブに実数項目が無い end‏
    }
}
