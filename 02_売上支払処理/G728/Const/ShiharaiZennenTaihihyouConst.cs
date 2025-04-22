using System;

namespace Shougun.Core.SalesPayment.ShiharaiZennenTaihihyou
{
    /// <summary>
    /// 支払集計表固定値クラス
    /// </summary>
    internal static class ShiharaiZennenTaihihyouConst
    {
        /// <summary>
        /// ボタン設定ファイルのパス
        /// </summary>
        internal static readonly String BUTTON_SETTING_XML = "Shougun.Core.SalesPayment.ShiharaiZennenTaihihyou.Setting.ButtonSetting.xml";

        /// <summary>
        /// 帳票テンプレートファイルのパス
        /// </summary>
        internal static readonly String FORM_FILE = "./Template/R729-Form.xml";

        /// <summary>
        /// 帳票テンプレートで使用するレイアウト
        /// </summary>
        internal static readonly String LAYOUT = "LAYOUT1";

        /// <summary>
        /// 日付種類CD「伝票日付」
        /// </summary>
        internal static readonly String DATE_SHURUI_CD_DENPYOU = "1";

        /// <summary>
        /// 日付種類「伝票日付」
        /// </summary>
        internal static readonly String DATE_SHURUI_1 = "伝票日付";

        /// <summary>
        /// 日付種類「支払日付」
        /// </summary>
        internal static readonly String DATE_SHURUI_2 = "支払日付";

        /// <summary>
        /// 日付種類「入力日付」
        /// </summary>
        internal static readonly String DATE_SHURUI_3 = "入力日付";

        /// <summary>
        /// 日付範囲CD「当日」
        /// </summary>
        internal static readonly String DATE_RANGE_CD_TOUJITSU = "1";

        /// <summary>
        /// 日付範囲CD「当月」
        /// </summary>
        internal static readonly String DATE_RANGE_CD_TOUGETSU = "2";

        /// <summary>
        /// 日付範囲CD「期間指定」
        /// </summary>
        internal static readonly String DATE_RANGE_CD_SHITEI = "3";

        /// <summary>
        /// 伝票種類CD「受入」
        /// </summary>
        internal static readonly String DENPYOU_SHURUI_CD_UKEIRE = "1";

        /// <summary>
        /// 伝票種類CD「出荷」
        /// </summary>
        internal static readonly String DENPYOU_SHURUI_CD_SHUKKA = "2";

        /// <summary>
        /// 伝票種類CD「支払／支払」
        /// </summary>
        internal static readonly String DENPYOU_SHURUI_CD_URIAGE_SHIHARAI = "3";

        // 20150513 伝種「4.代納」追加(不具合一覧(つ) 23) Start
        /// <summary>
        /// 伝票種類CD「代納」
        /// </summary>
        internal static readonly String DENPYOU_SHURUI_CD_DAINOU = "4";

        /// <summary>
        /// 伝票種類CD「全て」
        /// </summary>
        internal static readonly String DENPYOU_SHURUI_CD_SUBETE = "5";
        // 20150513 伝種「4.代納」追加(不具合一覧(つ) 23) End

        /// <summary>
        /// 伝票種類「受入」
        /// </summary>
        internal static readonly String DENPYOU_SHURUI_1 = "受入";

        /// <summary>
        /// 伝票種類「出荷」
        /// </summary>
        internal static readonly String DENPYOU_SHURUI_2 = "出荷";

        /// <summary>
        /// 伝票種類「支払／出荷」
        /// </summary>
        internal static readonly String DENPYOU_SHURUI_3 = "支払／支払";

        // 20150513 伝種「4.代納」追加(不具合一覧(つ) 23) Start
        /// <summary>
        /// 伝票種類「代納」
        /// </summary>
        internal static readonly String DENPYOU_SHURUI_4 = "代納";

        /// <summary>
        /// 伝票種類「全て」
        /// </summary>
        internal static readonly String DENPYOU_SHURUI_5 = "全て";
        // 20150513 伝種「4.代納」追加(不具合一覧(つ) 23) End

        /// <summary>
        /// 取引区分CD「全て」
        /// </summary>
        internal static readonly String TORIHIKI_KBN_CD_SUBETE = "3";

        /// <summary>
        /// 取引区分「現金」
        /// </summary>
        internal static readonly String TORIHIKI_KBN_1 = "現金";

        /// <summary>
        /// 取引区分「掛け」
        /// </summary>
        internal static readonly String TORIHIKI_KBN_2 = "掛け";

        /// <summary>
        /// 取引区分「全て」
        /// </summary>
        internal static readonly String TORIHIKI_KBN_3 = "全て";

        /// <summary>
        /// 確定区分CD「全て」
        /// </summary>
        internal static readonly String KAKUTEI_KBN_CD_SUBETE = "3";

        /// <summary>
        /// 確定区分「確定」
        /// </summary>
        internal static readonly String KAKUTEI_KBN_1 = "確定";

        /// <summary>
        /// 確定区分「未確定」
        /// </summary>
        internal static readonly String KAKUTEI_KBN_2 = "未確定";

        /// <summary>
        /// 確定区分「全て」
        /// </summary>
        internal static readonly String KAKUTEI_KBN_3 = "全て";

        /// <summary>
        /// 締区分CD「全て」
        /// </summary>
        internal static readonly String SHIME_KBN_CD_SUBETE = "3";

        /// <summary>
        /// 締区分「締」
        /// </summary>
        internal static readonly String SHHIME_KBN_1 = "締";

        /// <summary>
        /// 締区分「未締」
        /// </summary>
        internal static readonly String SHHIME_KBN_2 = "未締";

        /// <summary>
        /// 締区分「全て」
        /// </summary>
        internal static readonly String SHHIME_KBN_3 = "全て";

        /// <summary>
        /// 拠点CD「99」
        /// </summary>
        internal static readonly String KYOTEN_CD_ZENSHA = "99";

        /// <summary>
        /// 拠点「全社」
        /// </summary>
        internal static readonly String KYOTEN_NAME_99 = "全社";

        /// <summary>
        /// 台貫CD「自社」
        /// </summary>
        internal static readonly String DAIKAN_CD_JISHA = "1";

        /// <summary>
        /// 台貫CD「他社」
        /// </summary>
        internal static readonly String DAIKAN_CD_TASHA = "2";

        /// <summary>
        /// 台貫「自社」
        /// </summary>
        internal static readonly String DAIKAN_NAME_1 = "自社";

        /// <summary>
        /// 台貫「他社」
        /// </summary>
        internal static readonly String DAIKAN_NAME_2 = "他社";
    }
}
