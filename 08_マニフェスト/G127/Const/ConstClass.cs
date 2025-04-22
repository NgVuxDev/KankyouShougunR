using System.Collections.Generic;

namespace Shougun.Core.PaperManifest.Manifestmeisaihyo
{
    /// <summary>
    /// マニフェスト明細表で使用する定数を集めたクラス
    /// </summary>
    internal static class ConstClass
    {
        /// <summary>
        /// 帳票タイトル「マニフェスト明細票」
        /// </summary>
        internal static readonly string MANIFEST_MEISAIHYOU_TITLE = "マニフェスト明細表";

        /// <summary>
        /// 帳票サブタイトル「交付年月日」
        /// </summary>
        internal static readonly string SORT_KOFU_DATE_SUB_TITLE = "交付年月日";

        /// <summary>
        /// 帳票サブタイトル「運搬終了日」
        /// </summary>
        internal static readonly string SORT_UPN_END_DATE_SUB_TITLE = "運搬終了日";

        /// <summary>
        /// 帳票サブタイトル「処分終了日」
        /// </summary>
        internal static readonly string SORT_SBN_END_DATE_SUB_TITLE = "処分終了日";

        /// <summary>
        /// 帳票サブタイトル「最終処分終了日」
        /// </summary>
        internal static readonly string SORT_LAST_SBN_END_DATE_SUB_TITLE = "最終処分終了日";

        /// <summary>
        /// 帳票サブタイトル「排出事業者」
        /// </summary>
        internal static readonly string SORT_HST_GYOUSHA_SUB_TITLE = "排出事業者";

        /// <summary>
        /// 帳票サブタイトル「運搬受託者（１）」
        /// </summary>
        internal static readonly string SORT_UPN_GYOUSHA_1_SUB_TITLE = "運搬受託者（１）";

        /// <summary>
        /// 帳票サブタイトル「運搬受託者（２）」
        /// </summary>
        internal static readonly string SORT_UPN_GYOUSHA_2_SUB_TITLE = "運搬受託者（２）";

        /// <summary>
        /// 帳票サブタイトル「処分受託者」
        /// </summary>
        internal static readonly string SORT_SBN_GYOUSHA_SUB_TITLE = "処分受託者";

        /// <summary>
        /// 帳票サブタイトル「最終処分業者」
        /// </summary>
        internal static readonly string SORT_LAST_SBN_GYOUSHA_SUB_TITLE = "最終処分業者";

        /// <summary>
        /// 帳票サブタイトル「廃棄物種類」
        /// </summary>
        internal static readonly string SORT_HOUKOKUSHO_BUNRUI_SUB_TITLE = "廃棄物種類";

        /// <summary>
        /// 帳票サブタイトル「処分方法」
        /// </summary>
        internal static readonly string SORT_SBN_HOUHOU_SUB_TITLE = "処分方法";

        /// <summary>
        /// 日付種類「伝票日付」
        /// </summary>
        internal static readonly string DATE_SHURUI_DENPYOU = "伝票日付";

        /// <summary>
        /// 日付種類「入力日付」
        /// </summary>
        internal static readonly string DATE_SHURUI_INPUT = "入力日付";

        /// <summary>
        /// 並び順「交付年月日」
        /// </summary>
        internal static readonly int SORT_KOFU_DATE = 1;

        /// <summary>
        /// 並び順「運搬終了日」
        /// </summary>
        internal static readonly int SORT_UPN_END_DATE = 2;

        /// <summary>
        /// 並び順「処分終了日」
        /// </summary>
        internal static readonly int SORT_SBN_END_DATE = 3;

        /// <summary>
        /// 並び順「最終処分終了日」
        /// </summary>
        internal static readonly int SORT_LAST_SBN_END_DATE = 4;

        /// <summary>
        /// 並び順「排出事業者」
        /// </summary>
        internal static readonly int SORT_HST_GYOUSHA = 5;

        /// <summary>
        /// 並び順「運搬受託者１」
        /// </summary>
        internal static readonly int SORT_UPN_GYOUSHA_1 = 6;

        /// <summary>
        /// 並び順「運搬受託者２」
        /// </summary>
        internal static readonly int SORT_UPN_GYOUSHA_2 = 7;

        /// <summary>
        /// 並び順「処分受託者」
        /// </summary>
        internal static readonly int SORT_SBN_GYOUSHA = 8;

        /// <summary>
        /// 並び順「最終処分受託者」
        /// </summary>
        internal static readonly int SORT_LAST_SBN_GYOUSHA = 9;

        /// <summary>
        /// 並び順「廃棄物種類」
        /// </summary>
        internal static readonly int SORT_HOUKOKUSHO_BUNRUI = 10;

        /// <summary>
        /// 並び順「処分方法」
        /// </summary>
        internal static readonly int SORT_SBN_HOUHOU = 11;

        /// <summary>
        /// 集計行ラベル「交付年月日合計」
        /// </summary>
        internal static readonly string GROUP_FOOTER_KOFU_DATE = "交付年月日合計";

        /// <summary>
        /// 集計行ラベル「運搬終了日合計」
        /// </summary>
        internal static readonly string GROUP_FOOTER_UPN_END_DATE = "運搬終了日合計";

        /// <summary>
        /// 集計行ラベル「処分終了日合計」
        /// </summary>
        internal static readonly string GROUP_FOOTER_SBN_END_DATE = "処分終了日合計";

        /// <summary>
        /// 集計行ラベル「最終処分終了日合計」
        /// </summary>
        internal static readonly string GROUP_FOOTER_LAST_SBN_END_DATE = "最終処分終了日合計";

        /// <summary>
        /// 集計行ラベル「運搬受託者（１）合計」
        /// </summary>
        internal static readonly string GROUP_FOOTER_UPN_GYOUSHA_1 = "運搬受託者（１）合計";

        /// <summary>
        /// 集計行ラベル「運搬受託者（２）合計」
        /// </summary>
        internal static readonly string GROUP_FOOTER_UPN_GYOUSHA_2 = "運搬受託者（２）合計";
    }
}
