
namespace Shougun.Core.SalesPayment.UriageJunihyo
{
    /// <summary>
    /// 売上順位表で使用する定数を集めたクラス
    /// </summary>
    internal static class ConstClass
    {
        /// <summary>
        /// 日付CD「伝票日付」
        /// </summary>
        internal static readonly string DATE_CD_DENPYOU = "1";

        /// <summary>
        /// 日付CD「売上日付」
        /// </summary>
        internal static readonly string DATE_CD_URIAGE = "2";

        /// <summary>
        /// 日付CD「入力日付」
        /// </summary>
        internal static readonly string DATE_CD_INPUT = "3";

        /// <summary>
        /// 日付名称「伝票日付」
        /// </summary>
        internal static readonly string DATE_NAME_DENPYOU = "伝票日付";

        /// <summary>
        /// 日付名称「売上日付」
        /// </summary>
        internal static readonly string DATE_NAME_URIAGE = "売上日付";

        /// <summary>
        /// 日付名称「入力日付」
        /// </summary>
        internal static readonly string DATE_NAME_INPUT = "入力日付";

        /// <summary>
        /// 日付範囲CD「当日」
        /// </summary>
        internal static readonly string DATE_RANGE_CD_DAY = "1";

        /// <summary>
        /// 日付範囲CD「当月」
        /// </summary>
        internal static readonly string DATE_RANGE_CD_MONTH = "2";

        /// <summary>
        /// 日付範囲CD「期間指定」
        /// </summary>
        internal static readonly string DATE_RANGE_CD_SHITEI = "3";

        /// <summary>
        /// 日付範囲名称「当日」
        /// </summary>
        internal static readonly string DATE_RANGE_NAME_DAY = "当日";

        /// <summary>
        /// 日付範囲名称「当月」
        /// </summary>
        internal static readonly string DATE_RANGE_NAME_MONTH = "当月";

        /// <summary>
        /// 日付範囲名称「期間指定」
        /// </summary>
        internal static readonly string DATE_RANGE_NAME_SHITEI = "期間指定";

        /// <summary>
        /// 伝票種類CD「受入」
        /// </summary>
        internal static readonly string DENPYOU_SHURUI_CD_UKEIRE = "1";

        /// <summary>
        /// 伝票種類CD「出荷」
        /// </summary>
        internal static readonly string DENPYOU_SHURUI_CD_SHUKKA = "2";

        /// <summary>
        /// 伝票種類CD「売上／支払」
        /// </summary>
        internal static readonly string DENPYOU_SHURUI_CD_UR_SH = "3";

        // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) Start
        /// <summary>
        /// 伝票種類CD「代納」
        /// </summary>
        internal static readonly string DENPYOU_SHURUI_CD_DAINOU = "4";

        /// <summary>
        /// 伝票種類CD「全て」
        /// </summary>
        internal static readonly string DENPYOU_SHURUI_CD_ALL = "5";
        // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) End

        /// <summary>
        /// 伝票種類名称「受入」
        /// </summary>
        internal static readonly string DENPYOU_SHURUI_NAME_UKEIRE = "受入";

        /// <summary>
        /// 伝票種類名称「出荷」
        /// </summary>
        internal static readonly string DENPYOU_SHURUI_NAME_SHUKKA = "出荷";

        /// <summary>
        /// 伝票種類名称「売上／支払」
        /// </summary>
        internal static readonly string DENPYOU_SHURUI_NAME_UR_SH = "売上／支払";

        // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) Start
        /// <summary>
        /// 伝票種類名称「代納」
        /// </summary>
        internal static readonly string DENPYOU_SHURUI_NAME_DAINOU = "代納";
        // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) End

        /// <summary>
        /// 伝票種類名称「全て」
        /// </summary>
        internal static readonly string DENPYOU_SHURUI_NAME_ALL = "全て";

        /// <summary>
        /// 取引区分CD「現金」
        /// </summary>
        internal static readonly string TORIHIKI_KBN_CD_GENKIN = "1";

        /// <summary>
        /// 取引区分CD「掛け」
        /// </summary>
        internal static readonly string TORIHIKI_KBN_CD_KAKE = "2";

        /// <summary>
        /// 取引区分CD「全て」
        /// </summary>
        internal static readonly string TORIHIKI_KBN_CD_ALL = "3";

        /// <summary>
        /// 取引区分名称「現金」
        /// </summary>
        internal static readonly string TORIHIKI_KBN_NAME_GENKIN = "現金";

        /// <summary>
        /// 取引区分名称「掛け」
        /// </summary>
        internal static readonly string TORIHIKI_KBN_NAME_KAKE = "掛け";

        /// <summary>
        /// 取引区分名称「全て」
        /// </summary>
        internal static readonly string TORIHIKI_KBN_NAME_ALL = "全て";

        /// <summary>
        /// 確定区分CD「確定」
        /// </summary>
        internal static readonly string KAKUTEI_KBN_CD_KAKUTEI = "1";

        /// <summary>
        /// 確定区分CD「未確定」
        /// </summary>
        internal static readonly string KAKUTEI_KBN_CD_MIKAKUTEI = "2";

        /// <summary>
        /// 確定区分CD「全て」
        /// </summary>
        internal static readonly string KAKUTEI_KBN_CD_ALL = "3";

        /// <summary>
        /// 確定区分名称「確定」
        /// </summary>
        internal static readonly string KAKUTEI_KBN_NAME_KAKUTEI = "確定";

        /// <summary>
        /// 確定区分名称「未確定」
        /// </summary>
        internal static readonly string KAKUTEI_KBN_NAME_MIKAKUTEI = "未確定";

        /// <summary>
        /// 確定区分名称「全て」
        /// </summary>
        internal static readonly string KAKUTEI_KBN_NAME_ALL = "全て";

        /// <summary>
        /// 締処理状況CD「済」
        /// </summary>
        internal static readonly string SHIME_KBN_CD_ZUMI = "1";

        /// <summary>
        /// 締処理状況CD「未締」
        /// </summary>
        internal static readonly string SHIME_KBN_CD_MISHIME = "2";

        /// <summary>
        /// 締処理状況CD「全て」
        /// </summary>
        internal static readonly string SHIME_KBN_CD_ALL = "3";

        /// <summary>
        /// 締処理状況名称「済」
        /// </summary>
        internal static readonly string SHIME_KBN_NAME_ZUMI = "済";

        /// <summary>
        /// 締処理状況名称「未締」
        /// </summary>
        internal static readonly string SHIME_KBN_NAME_MISHIME = "未締";

        /// <summary>
        /// 締処理状況名称「全て」
        /// </summary>
        internal static readonly string SHIME_KBN_NAME_ALL = "全て";

        /// <summary>
        /// 拠点CD「全社」
        /// </summary>
        internal static readonly string KYOTEN_CD_ALL = "99";

        /// <summary>
        /// 拠点名「全社」
        /// </summary>
        internal static readonly string KYOTEN_NAME_ALL = "全社";

        /// <summary>
        /// コンボボックスの表示に使用するプロパティ名
        /// </summary>
        internal static readonly string DISPLAY_MEMBER = "KOUMOKU_RONRI_NAME";

        /// <summary>
        /// コンボボックスの選択値に使用するプロパティ名
        /// </summary>
        internal static readonly string VALUE_MEMBER = "KOUMOKU_ID";

        /// <summary>
        /// 業者 項目ID
        /// </summary>
        internal static readonly int GYOUSHA_CD_KOUMOKU_ID = 3;

        /// <summary>
        /// 現場 項目ID
        /// </summary>
        internal static readonly int GENBA_CD_CD_KOUMOKU_ID = 4;
    }
}
