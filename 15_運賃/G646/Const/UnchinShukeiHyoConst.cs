using System;

namespace Shougun.Core.Carriage.UnchinShukeiHyo
{
    /// <summary>
    /// 運賃集計表固定値クラス
    /// </summary>
    internal static class UnchinShukeiHyoConst
    {
        /// <summary>
        /// ボタン設定ファイルのパス
        /// </summary>
        internal static readonly string BUTTON_SETTING_XML = "Shougun.Core.Carriage.UnchinShukeiHyo.Setting.ButtonSetting.xml";

        /// <summary>
        /// 帳票テンプレートファイルのパス
        /// </summary>
        internal static readonly string FORM_FILE = "./Template/R648-Form.xml";

        /// <summary>
        /// 帳票テンプレートで使用するレイアウト
        /// </summary>
        internal static readonly string LAYOUT = "LAYOUT1";

        /// <summary>
        /// 日付種類CD「伝票日付」
        /// </summary>
        internal static readonly string DATE_SHURUI_CD_DENPYOU = "1";

        /// <summary>
        /// 日付種類「伝票日付」
        /// </summary>
        internal static readonly string DATE_SHURUI_1 = "伝票日付";
        /// <summary>
        /// 日付種類「入力日付」
        /// </summary>
        internal static readonly string DATE_SHURUI_2 = "入力日付";

        /// <summary>
        /// 日付範囲CD「当日」
        /// </summary>
        internal static readonly string DATE_RANGE_CD_TOUJITSU = "1";

        /// <summary>
        /// 日付範囲CD「当月」
        /// </summary>
        internal static readonly string DATE_RANGE_CD_TOUGETSU = "2";

        /// <summary>
        /// 日付範囲CD「期間指定」
        /// </summary>
        internal static readonly string DATE_RANGE_CD_SHITEI = "3";

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
        internal static readonly string DENPYOU_SHURUI_CD_URIAGE_SHIHARAI = "3";

        /// <summary>
        /// 伝票種類CD「代納」
        /// </summary>
        internal static readonly string DENPYOU_SHURUI_CD_DAINOU = "4";

        /// <summary>
        /// 伝票種類CD「運賃」
        /// </summary>
        internal static readonly string DENPYOU_SHURUI_CD_UNCHIN = "5";

        /// <summary>
        /// 伝票種類CD「全て」
        /// </summary>
        internal static readonly string DENPYOU_SHURUI_CD_SUBETE = "6";

        /// <summary>
        /// 伝票種類「受入」
        /// </summary>
        internal static readonly string DENPYOU_SHURUI_1 = "受入";

        /// <summary>
        /// 伝票種類「出荷」
        /// </summary>
        internal static readonly string DENPYOU_SHURUI_2 = "出荷";

        /// <summary>
        /// 伝票種類「売上／出荷」
        /// </summary>
        internal static readonly string DENPYOU_SHURUI_3 = "売上／支払";

        /// <summary>
        /// 伝票種類CD「代納」
        /// </summary>
        internal static readonly string DENPYOU_SHURUI_4 = "代納";
        /// <summary>
        /// 伝票種類CD「運賃」
        /// </summary>
        internal static readonly string DENPYOU_SHURUI_5 = "運賃";

        /// <summary>
        /// 伝票種類「全て」
        /// </summary>
        internal static readonly string DENPYOU_SHURUI_6 = "全て";

        /// <summary>
        /// 拠点CD「99」
        /// </summary>
        internal static readonly string KYOTEN_CD_ZENSHA = "99";

        /// <summary>
        /// 拠点「全社」
        /// </summary>
        internal static readonly string KYOTEN_NAME_99 = "全社";
    }
}
