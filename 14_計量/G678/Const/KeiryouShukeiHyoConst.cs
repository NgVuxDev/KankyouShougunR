using System;

namespace Shougun.Core.Scale.KeiryouShukeiHyo
{
    /// <summary>
    /// 計量集計表固定値クラス
    /// </summary>
    internal static class KeiryouShukeiHyoConst
    {
        /// <summary>
        /// ボタン設定ファイルのパス
        /// </summary>
        internal static readonly String BUTTON_SETTING_XML = "Shougun.Core.Scale.KeiryouShukeiHyo.Setting.ButtonSetting.xml";

        /// <summary>
        /// 帳票テンプレートファイルのパス
        /// </summary>
        internal static readonly String FORM_FILE = "./Template/R679-Form.xml";

        /// <summary>
        /// 帳票テンプレートで使用するレイアウト
        /// </summary>
        internal static readonly String LAYOUT = "LAYOUT1";

        /// <summary>
        /// 伝票種類CD「売上」
        /// </summary>
        internal static readonly String DENPYOU_KBN = "1";

        /// <summary>
        /// 伝票種類「売上」
        /// </summary>
        internal static readonly String DENPYOU_KBN_1 = "売上";

        /// <summary>
        /// 伝票種類「支払」
        /// </summary>
        internal static readonly String DENPYOU_KBN_2 = "支払";

        /// <summary>
        /// 日付種類CD「伝票日付」
        /// </summary>
        internal static readonly String DATE_SHURUI_CD_DENPYOU = "1";

        /// <summary>
        /// 日付種類「伝票日付」
        /// </summary>
        internal static readonly String DATE_SHURUI_1 = "伝票日付";

        /// <summary>
        /// 日付種類「入力日付」
        /// </summary>
        internal static readonly String DATE_SHURUI_2 = "入力日付";

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
        /// 基本計量CD「受入」
        /// </summary>
        internal static readonly String KIHON_KEIRYOU_CD_UKEIRE = "1";

        /// <summary>
        /// 基本計量CD「出荷」
        /// </summary>
        internal static readonly String KIHON_KEIRYOU_CD_SHUKKA = "2";

        /// <summary>
        /// 基本計量CD「全て」
        /// </summary>
        internal static readonly String KIHON_KEIRYOU_CD_SUBETE = "3";

        /// <summary>
        /// 伝票種類「受入」
        /// </summary>
        internal static readonly String DENPYOU_SHURUI_1 = "受入";

        /// <summary>
        /// 伝票種類「出荷」
        /// </summary>
        internal static readonly String DENPYOU_SHURUI_2 = "出荷";

        /// <summary>
        /// 伝票種類「全て」
        /// </summary>
        internal static readonly String DENPYOU_SHURUI_3 = "全て";

        /// <summary>
        /// 取引区分CD「現金」
        /// </summary>
        internal static readonly String TORIHIKI_KBN_CD_GENNKINN = "1";

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
        /// 拠点CD「99」
        /// </summary>
        internal static readonly String KYOTEN_CD_ZENSHA = "99";

        /// <summary>
        /// 拠点「全社」
        /// </summary>
        internal static readonly String KYOTEN_NAME_99 = "全社";
    }
}
