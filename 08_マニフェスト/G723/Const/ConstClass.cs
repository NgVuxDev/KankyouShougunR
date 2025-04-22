
namespace Shougun.Core.PaperManifest.ManifestSuiihyoData
{
    /// <summary>
    /// マニ推移表で使用する定数を集めたクラス
    /// </summary>
    internal static class ConstClass
    {
        /// <summary>
        /// コンボボックスの表示に使用するプロパティ名
        /// </summary>
        internal static readonly string DISPLAY_MEMBER = "KOUMOKU_RONRI_NAME";

        /// <summary>
        /// コンボボックスの選択値に使用するプロパティ名
        /// </summary>
        internal static readonly string VALUE_MEMBER = "KOUMOKU_ID";

        /// <summary>
        /// パターン登録の項目「運搬受託者」
        /// </summary>
        internal static readonly string UPN_GYOUSHA_CD = "UPN_GYOUSHA_CD";

        /// <summary>
        /// パターン登録の項目「処分事業場」
        /// </summary>
        internal static readonly string UPN_SAKI_GENBA_CD = "UPN_SAKI_GENBA_CD";

        /// <summary>
        /// パターン登録の項目「廃棄物種類（報告書分類）」
        /// </summary>
        internal static readonly string HAIKI_SHURUI_CD = "HAIKI_SHURUI_CD";

        /// <summary>
        /// 廃棄物種類マスタ検索用の項目「廃棄物区分CD」
        /// </summary>
        /// <remarks>
        /// パターン登録の項目「廃棄物種類（報告書分類）」選択時に使用
        /// </remarks>
        internal static readonly string HAIKI_KBN_CD = "HAIKI_KBN_CD";
    }
}
