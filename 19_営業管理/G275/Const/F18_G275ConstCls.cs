
namespace Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.Const
{
    class F18_G275ConstCls
    {
        /// <summary>ボタン定義ファイルパス</summary>
        public const string BUTTON_INFO_XML_PATH = "Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.Setting.ButtonSetting.xml";

        /// <summary>形式：年次</summary>
        public const string KEISHIKI_NENDO = "1";
        /// <summary>形式：月次</summary>
        public const string KEISHIKI_GETUJI = "2";

        /// <summary>SYS_ID</summary>
        public const int SYS_ID = 0;

        /// <summary>初期の読込データ件数</summary>
        public const string INIT_RESULT_COUNT = "0";

        /// <summary>ブランク</summary>
        public const string BLANK = "";

        /// <summary>
        /// 伝票区分：売上
        /// </summary>
        public const string DENPYOU_KBN_URIAGE = "1";

        /// <summary>
        /// 伝票区分：支払
        /// </summary>
        public const string DENPYOU_KBN_SHIHARAI = "2";

        /// <summary>月次一覧の表示月数</summary>
        public const int COUNT_MONTH = 12;
        /// <summary>月次一覧の月項目名の接頭文字</summary>
        public const string GETUJI_HEADER_NAME_START_STR = "MONTH_";
        /// <summary>月次一覧の月項目表示の終了文字</summary>
        public const string GETUJI_HEADER_VALUE_END_STR = "月";

        /// <summary>年度一覧の表示年数</summary>
        public const int COUNT_YEAR = 9;
        /// <summary>年度一覧の年項目名の接頭文字</summary>
        public const string NENDO_HEADER_NAME_START_STR = "NENDO_";
        /// <summary>年度一覧の年項目表示の終了文字</summary>
        public const string NENDO_HEADER_VALUE_END_STR = "年度";

        // 2014/01/24 oonaka delete CSVファイル名が不正 start
        ///// <summary>CSV出力初期ファイル名</summary>
        //public const string CSV_DIALOG_INIT_FILE_NAME = "営業予実管理表";
        // 2014/01/24 oonaka delete CSVファイル名が不正 end

        /// <summary>必須チェックエラー時のメッセージID</summary>
        public const string MSG_ID_REQUIRED = "E001";
        /// <summary>年度必須チェックエラー時のメッセージパラメータ</summary>
        public readonly static string[] MSG_PARAM_REQUIRED_NENDO = { "年度" };
        // No2661-->
        /// <summary>部署必須チェックエラー時のメッセージパラメータ</summary>
        public readonly static string[] MSG_PARAM_REQUIRED_BUSHO = { "部署" };
        // No2661<--
        /// <summary>伝票区分必須チェックエラー時のメッセージパラメータ</summary>
        public readonly static string[] MSG_PARAM_REQUIRED_DENPYOU_KBN = { "伝票区分" };
        /// <summary>出力形式必須チェックエラー時のメッセージパラメータ</summary>
        public readonly static string[] MSG_PARAM_REQUIRED_OUTPUT_STYLE = { "出力形式" };
        /// <summary>アラート件数必須チェックエラー時のメッセージパラメータ</summary>
        public readonly static string[] MSG_PARAM_REQUIRED_ALERT_COUNT = { "アラート件数" };

        /// <summary>検索結果なしのメッセージID</summary>
        public const string MSG_ID_NO_DATA = "C001";
        /// <summary>検索結果はアラート件数を超えるメッセージID</summary>
        public const string MSG_ID_OVER_ALERT_COUNT = "C025";

        /// <summary>CSV出力データなしのメッセージID</summary>
        public const string MSG_ID_NO_OUTPUT_DATA = "E044";
        /// <summary>CSV出力確認のメッセージID</summary>
        public const string MSG_ID_OUTPUT_COMFIRM = "C012";

        /// <summary>請求情報金額端数CD：切り上げ</summary>
        public const int SEIKYUU_KINGAKU_HASUU_CD_1 = 1;
        /// <summary>請求情報金額端数CD：切り捨て</summary>
        public const int SEIKYUU_KINGAKU_HASUU_CD_2 = 2;
        /// <summary>請求情報金額端数CD：四捨五入</summary>
        public const int SEIKYUU_KINGAKU_HASUU_CD_3 = 3;

        /// <summary>完了メッセージ</summary>
        public const string I_MSG_ID_PROCESS_FINISHED = "I001";
        public static readonly string[] I_MSG_PARAM_SELECT_FINISHED = { "検索" };

    }
}
