// $Id: ConstClass.cs 38457 2014-12-29 07:31:12Z j-kikuchi $

namespace Shougun.Core.Inspection.KongouHaikibutsuJoukyouIchiran
{
	/// <summary>
    /// 混合廃棄物状況一覧定義
	/// </summary>
	public class ConstClass
    {
        /// <summary>
        /// ボタン設定XMLパス定義
        /// </summary>
        internal const string ButtonInfoXmlPath = "Shougun.Core.ElectronicManifest.KongouHaikibutsuJoukyouIchiran.Setting.ButtonSetting.xml";
        /// <summary>
        /// 日付定義
        /// </summary>
        internal const string DATE_KBN_KOUFU = "1";       // 交付年月日
        internal const string DATE_KBN_UNPAN = "2";       // 運搬終了日
        internal const string DATE_KBN_SHOBUN = "3";      // 処分終了日
        internal const string DATE_KBN_LAST = "4";        // 最終処分終了日
        /// <summary>
        /// 表示区分定義
        /// </summary>
        internal const string SHOW_KBN_COMPLETE = "1";      // 完了
        internal const string SHOW_KBN_INCOMPLETE = "2";    // 未完了
        internal const string SHOW_KBN_ALL = "3";           // 全て
        /// <summary>
        /// 入力種別
        /// </summary>
        internal enum INPUT_TYPE
        {
            INPUT_TYPE_HST_GYOUSHA = 0,         // 排出事業者
            INPUT_TYPE_HST_GENBA,               // 排出事業場
            INPUT_TYPE_SBN_GYOUSHA,             // 処分受託者
            INPUT_TYPE_UPN_SAKI_GENBA,          // 運搬先の次行亜
            INPUT_TYPE_UPN_JYUTAKUSHA,          // 運搬受託者
            INPUT_TYPE_HOUKOKUSHO_BUNRUI,       // 報告書分類
            INPUT_TYPE_DENSHI_HAIKI_SHURUI,     // 電子廃棄物種類
            INPUT_TYPE_DATE_TO,                 // 日付TO
            INPUT_TYPE_MANIFEST_ID_TO,          // マニフェスト番号TO
            INPUT_TYPE_MAX,                 // MAX
        }
    }
}
