
namespace Shougun.Core.Scale.KeiryouHoukoku.Const
{
    /// <summary>
    /// 計量報告で使用する定数を集めたクラス
    /// </summary>
    internal static class ConstCls
    {
        /// <summary>ボタン情報を格納しているXMLファイルのパス（リソース）を保持するフィールド</summary>
        internal static readonly string BUTTON_INFO_XML_PATH = "Shougun.Core.Scale.KeiryouHoukoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// 帳票テンプレートのパス
        /// </summary>
        internal static readonly string R675OutputFormFullPathName = "./Template/R675-Form.xml";
        internal static readonly string R676OutputFormFullPathName = "./Template/R676-Form.xml";
        internal static readonly string R677OutputFormFullPathName = "./Template/R677-Form.xml";

        /// <summary>
        /// 帳票レイアウト名
        /// </summary>
        internal static readonly string LAYOUT_1 = "LAYOUT_1";
        internal static readonly string LAYOUT_2 = "LAYOUT_2";
        internal static readonly string LAYOUT_3 = "LAYOUT_3";

        /// <summary>
        /// 帳票タイトル「計量明細票」
        /// </summary>
        internal static readonly string KEIRYOU_MEISAIHYOU_TITLE = "計量明細表";

        /// <summary>
        /// 帳票タイトル「計量元帳」
        /// </summary>
        internal static readonly string KEIRYOU_MOTOCHO_TITLE = "計量元帳";

        /// <summary>
        /// 帳票タイトル「計量推移表」
        /// </summary>
        internal static readonly string KEIRYOU_SUIIHYOU_TITLE = "計量推移表";

        /// <summary>
        /// 帳票サブタイトル「取引先」
        /// </summary>
        internal static readonly string SORT_TORIHIKISAKI_SUB_TITLE = "（取引先）";

        /// <summary>
        /// 帳票サブタイトル「業者」
        /// </summary>
        internal static readonly string SORT_GYOUSHA_SUB_TITLE = "（業者）";

        /// <summary>
        /// 帳票サブタイトル「現場」
        /// </summary>
        internal static readonly string SORT_GENBA_SUB_TITLE = "（現場）";

        /// <summary>
        /// 帳票サブタイトル「取引先別」
        /// </summary>
        internal static readonly string SORT_TORIHIKISAKI_SUB_TITLE_SUIIHYOU = "（取引先別）";

        /// <summary>
        /// 帳票サブタイトル「業者別」
        /// </summary>
        internal static readonly string SORT_GYOUSHA_SUB_TITLE_SUIIHYOU = "（業者別）";

        /// <summary>
        /// 帳票サブタイトル「現場別」
        /// </summary>
        internal static readonly string SORT_GENBA_SUB_TITLE_SUIIHYOU = "（現場別）";

        /// <summary>
        /// 帳票サブタイトル「品名別」
        /// </summary>
        internal static readonly string SORT_HINMEI_SUB_TITLE_SUIIHYOU = "（品名別）";

        /// <summary>
        /// 帳票サブタイトル「種類別」
        /// </summary>
        internal static readonly string SORT_SHURUI_SUB_TITLE_SUIIHYOU = "（種類別）";

        /// <summary>
        /// 帳票サブタイトル「分類別」
        /// </summary>
        internal static readonly string SORT_BUNRUI_SUB_TITLE_SUIIHYOU = "（分類別）";

        /// <summary>
        /// 日付種類「伝票日付」
        /// </summary>
        internal static readonly string DATE_SHURUI_DENPYOU = "[伝票日付]";

        /// <summary>
        /// 日付種類「入力日付」
        /// </summary>
        internal static readonly string DATE_SHURUI_INPUT = "[入力日付]";

        /// <summary>
        /// 計量区分「受入」
        /// </summary>
        internal static readonly string KEIRYOU_KBN_1 = "受入";

        /// <summary>
        /// 計量区分「出荷」
        /// </summary>
        internal static readonly string KEIRYOU_KBN_2 = "出荷";

        /// <summary>
        /// 「全て」
        /// </summary>
        internal static readonly string ALL = "全て";
    }
}