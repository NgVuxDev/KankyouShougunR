using System.Drawing;

namespace r_framework.Const
{
    /// <summary>
    /// 定数クラス
    /// 基本constではなく static readonlyで宣言してください。（シビアな処理速度が求められる場合のみconst）
    /// const→ビルド時に埋め込まれるため、値を変えたら他dllのリビルドが必要
    /// readonly→ビルド時に埋め込まれないのでこのdllだけ差し替えですべてに反映
    /// </summary>
    public class Constans
    {
        /// <summary>
        /// 最大表示画面数
        /// </summary>
        public static readonly int MAX_WINDOW_COUNT = 4;

        /// <summary>
        /// 新規入力画面区分
        /// </summary>
        public enum UKETSUKE_SHURUI_CD : int
        {
            SHUSHU_UKETSUKE = 1,
            BUPPAN_UKETSUKE = 2
        };

        ///// <summary>
        ///// DBにて利用されるBoolフラグ
        ///// </summary>
        //public enum DB_FLAG
        //{
        //    FALSE = 0,
        //    TRUE = 1
        //};

        /// <summary>
        /// 論理削除フラグ
        /// </summary>
        public enum LOGICAL_DELETE_FLAG
        {
            NOT_DELETE = 0,
            DELETE = 1
        };

        /// <summary>
        /// 「9」六桁
        /// </summary>
        public static readonly string ALL_NINE = "999999";

        /// <summary>
        /// 「Null」文字列
        /// </summary>
        public static readonly string NULL_STRING = "Null";

        /// <summary>
        /// ErrorMessageプロパティ名
        /// </summary>
        public static readonly string ERROR_MESSAGE = "ErrorMessage";

        /// <summary>
        /// 単票画面のヘッダーForm区分
        /// </summary>
        public static readonly int DETAILED_SCREEN_HEADER = 0;

        /// <summary>
        /// 一覧画面のヘッダーForm区分
        /// </summary>
        public static readonly int LIST_SCREEN_HEADER = 1;

        /// <summary>
        /// DB項目名の定数名（DBFieldsName)
        /// </summary>
        public static readonly string DB_FIELDS_NAME = "DBFieldsName";

        /// <summary>
        /// 項目定義型の定数名（ItemDefinedTypes)
        /// </summary>
        public static readonly string ITEM_DEFINED_TYPES = "ItemDefinedTypes";

        /// <summary>
        /// 文字数の定数名（CharactersNumber)
        /// </summary>
        public static readonly string CHARACTERS_NUMBER = "CharactersNumber";

        /// <summary>
        /// 表示項目名の定数名（DisplayItemName)
        /// </summary>
        public static readonly string DISPLAY_ITEM_NAME = "DisplayItemName";

        /// <summary>
        /// 短縮項目名の定数名（ShortItemName )
        /// </summary>
        public static readonly string SHORT_ITEM_NAME = "ShortItemName";

        /// <summary>
        /// 汎用検索画面表示フラグの定数名（SearchDisplayFlag)
        /// </summary>
        public static readonly string SEARCH_DISPLAY_FLAG = "SearchDisplayFlag";

        /// <summary>
        /// 登録時エラーチェックメソッド名の定数名（RegistCheckMethod)
        /// </summary>
        public static readonly string REGIST_CHECK_METHOD = "RegistCheckMethod";

        /// <summary>
        /// フォーカスアウト時チェックメソッドの定数名（FocusOutCheckMethod)
        /// </summary>
        public static readonly string FOCUS_OUT_CHECK_METHOD = "FocusOutCheckMethod";

        /// <summary>
        /// 呼出ポップアップ名の定数名（PopupWindowName)
        /// </summary>
        public static readonly string POPUP_WINDOW_NAME = "PopupWindowName";

        /// <summary>
        /// 検索先のマスタの定数名
        /// </summary>
        public static readonly string SEARCH_CODE_MASTER = "SearchCodeMaster";

        /// <summary>
        /// マスタから取得してくる対象カラムのプロパティの定数名
        /// </summary>
        public static readonly string GET_CODE_MASTER_FIELD = "GetCodeMasterField";

        /// <summary>
        /// 設定を行いFormのコントロール名を格納するプロパティの定数
        /// </summary>
        public static readonly string SET_FORM_FIELD = "SetFormField";

        /// <summary>
        /// 大文字へ自動変換するプロパティの定数
        /// </summary>
        public static readonly string CHANGE_UPPER_CASE = "ChangeUpperCase";

        /// <summary>
        /// 全角へ自動変換するプロパティの定数
        /// </summary>
        public static readonly string CHANGE_WIDE_CASE = "ChangeWideCase";

        /// <summary>
        /// 時間の配列定数
        /// </summary>
        public static readonly string[] HOUR_LIST ={
            "","0","1","2","3","4","5",
            "6","7","8","9","10","11",
            "12","13","14","15","16",
            "17","18","19","20","21","22","23"
            };

        /// <summary>
        /// 分の配列定数
        /// </summary>
        public static readonly string[] MINUTE_LIST = { "",
            "0","5","10","15",
            "20","25","30","35",
            "40","45","50","55"};

        /// <summary>
        /// 月の配列定数
        /// </summary>
        public static readonly string[] MONTH_LIST = { "",
            "1","2","3","4", "5",
            "6","7","8","9", "10",
            "11","12"};

        /// <summary>
        /// 日の配列定数
        /// </summary>
        public static readonly string[] DAY_LIST = { "",
             "1", "2", "3", "4",  "5",  "6",  "7",  "8",  "9", "10",
            "11","12","13","14", "15", "16", "17", "18", "19", "20",
            "21","22","23","24", "25", "26", "27", "28", "29", "30", "31"};

        /// <summary>
        /// エラーポップアップのタイトル名
        /// </summary>
        public static readonly string ERROR_TITLE = "アラート"; //"エラー"; 受入No892

        /// <summary>
        /// 警告ポップアップのタイトル名
        /// </summary>
        public static readonly string WORNING_TITLE = "警告";

        /// <summary>
        /// 確認ポップアップのタイトル名
        /// </summary>
        public static readonly string CONFIRM_TITLE = "確認";


        /// <summary>
        /// レイアウト区分名1
        /// </summary>
        public static readonly string REIAUTO_KBN_1 = "旧マニ";


        /// <summary>
        /// レイアウト区分名2
        /// </summary>
        public static readonly string REIAUTO_KBN_2 = "水銀マニ";

        /// <summary>
        /// ﾏﾆ用紙区分名1
        /// </summary>
        public static readonly string MANI_KBN_1 = "ﾏﾆ用紙へ印字";


        /// <summary>
        /// ﾏﾆ用紙区分名2
        /// </summary>
        public static readonly string MANI_KBN_2 = "ﾏﾆ用紙へ印字しない";

        #region Color

        /// <summary>
        /// エラー背景色
        /// </summary>
        public static readonly Color ERROR_COLOR = Color.FromArgb(255, 100, 100);
        /// <summary>
        /// エラー文字色
        /// </summary>
        public static readonly Color ERROR_COLOR_FORE = Color.Black;

        /// <summary>
        /// 通常背景色
        /// </summary>
        public static readonly Color NOMAL_COLOR = SystemColors.Window;
        /// <summary>
        /// 通常文字色
        /// </summary>
        public static readonly Color NOMAL_COLOR_FORE = Color.Black;

        // --20150912 ouken add 一覧で選択行の背景色を黄色に塗りつぶし start ---
        /// <summary>
        /// 選択行背景色
        /// </summary>
        public static readonly Color SELECT_COLOR = Color.Yellow;
        /// <summary>
        /// 選択行の文字色
        /// </summary>
        public static readonly Color SELECT_COLOR_FORE = Color.Black;
        // --20150912 ouken add 一覧で選択行の背景色を黄色に塗りつぶし end ---

        /// <summary>
        /// フォーカス時背景色
        /// </summary>
        public static readonly Color FOCUSED_COLOR = Color.Aqua;
        /// <summary>
        /// フォーカス時文字色
        /// </summary>
        public static readonly Color FOCUSED_COLOR_FORE = Color.Black;

        /// <summary>
        /// 読取専用時背景色
        /// </summary>
        public static readonly Color READONLY_COLOR = Color.FromArgb(240, 250, 230);
        /// <summary>
        /// 読取専用時文字色
        /// </summary>
        public static readonly Color READONLY_COLOR_FORE = Color.Black;

        /// <summary>
        /// 読取専用かつフォーカス時背景色
        /// </summary>
        public static readonly Color READONLY_FOCUSED_COLOR = READONLY_COLOR; //Color.Blue; //hack:値は仮
        /// <summary>
        /// 読取専用かつフォーカス時背景色
        /// </summary>
        public static readonly Color READONLY_FOCUSED_COLOR_FORE = READONLY_COLOR_FORE; //Color.White; //hack:値は仮

        /// <summary>
        /// 通常罫線色
        /// </summary>
        public static readonly Color NORMAL_BORDER_COLOR = Color.FromArgb(128, 128, 128);

        /// <summary>
        /// 読取専用時罫線色
        /// </summary>
        public static readonly Color READONLY_BORDER_COLOR = Color.FromArgb(132, 176, 162);

        /// <summary>
        /// 利用不可時色(Enable=false)
        /// </summary>
        public static readonly Color DISABLE_COLOR = Color.FromArgb(230, 230, 230); //薄いグレー
        /// <summary>
        /// 利用不可時色(Enable=false)
        /// </summary>
        public static readonly Color DISABLE_COLOR_FORE = Color.FromArgb(150, 150, 150); //濃いグレー

        #endregion

        /// <summary>
        /// 受付収集のアセンブリ情報
        /// </summary>
        public static readonly string UKETSUKE_SHUSHU_ASSEMBLY_NAME = "UketsukeShushu.dll";

        /// <summary>
        /// 受付収集のForm
        /// </summary>
        public static readonly string UKETSUKE_SHUSHU_FORM_NAME = "UketsukeShushu.APP.UketsukeShushuForm";

        /// <summary>
        /// MultiRow用デフォルトフォント
        /// </summary>
        public static readonly Font DEFAULT_MULTIROW_FONT = new Font("ＭＳ ゴシック", 9.75F);

        ///// <summary>
        ///// 英字専用テキスト許容文字
        ///// </summary>
        //public static readonly char[] ALLOW_KEY_CHARS_ALPHABET = new char[] { };

        ///// <summary>
        ///// 数字専用テキスト許容文字
        ///// </summary>
        //public static readonly char[] ALLOW_KEY_CHARS_NUMBER = new char[] { };

        /// <summary>
        /// 数値専用テキスト許容文字
        /// </summary>
        public static readonly char[] ALLOW_KEY_CHARS_NUMERIC = new char[] { ',', '.' };

        /// <summary>
        /// 常時許可する入力キー(バックスペース)
        /// </summary>
        public static readonly char[] ALLOW_KEY_CHARS_ALLINPUT = new char[] { '\b' };

        /// <summary>
        /// 複数行入力可能に対し許可する入力キー(エンター、改行)
        /// </summary>
        /// <remarks>
        /// コピーペーも併用、但し単行だけ入力可能の場合自動フィルターされる。
        /// </remarks>
        public static readonly char[] ALLOW_KEY_CHARS_NEWLINE = new char[] { '\r', '\n' };

        ///// <summary>
        ///// フリガナから削除する対象文字列
        ///// フリガナ設定処理を1次マスタに合わせる為にコメントアウト
        ///// </summary>
        //public static readonly string[] DELETE_FURIGANA_STR = new string[] {
        //    "株式会社", "（株)", "(株）", "（株）", "(株)", "㈱",
        //    "有限会社", "（有）", "（有)","(有）","(有)", "㈲",
        //    "合資会社", "（資）", "(資)", "（資)", "(資）",
        //    "合同会社", "（同）", "(同)", "（同)", "(同）" };

        /// <summary>
        /// フリガナから削除する対象文字列
        /// 1次マスタのフレームワークより
        /// </summary>
        public static readonly string[] DELETE_FURIGANA_STR = new string[] {
            " ", "　",
            "株式会社", "（株)", "(株）", "（株）", "(株)", "㈱", "カブシキガイシャ", "カブシキカイシャ", "（カブ)", "(カブ）", "（カブ）", "(カブ)",
            "有限会社", "（有）", "（有)","(有）","(有)", "㈲", "ユウゲンガイシャ", "ユウゲンカイシャ", "（ユウ）", "（ユウ)","(ユウ）","(ユウ)",
            "合資会社", "（資）", "(資)", "（資)", "(資）", "ゴウシガイシャ", "ゴウシカイシャ", "（シ）", "(シ)", "（シ)", "(シ）",
            "合同会社", "（同）", "(同)", "（同)", "(同）", "ゴウドウガイシャ", "ゴウドウカイシャ", "（ドウ）", "(ドウ)", "（ドウ)", "(ドウ）" };

        /// <summary>
        /// コピー時に削除を行う対象も字サル
        /// </summary>
        public static readonly string[] DELETE_COPY_STR = new string[] { "株式会社", "有限会社", "合資会社", "合同会社" };

        /// <summary>
        /// マスタ共通ポップアップにて利用する「あ」行リスト
        /// </summary>
        public static readonly string[] A_GYO_STR = new string[] { "ア", "イ", "ウ", "エ", "オ" };

        /// <summary>
        /// マスタ共通ポップアップにて利用する「か」行リスト
        /// </summary>
        public static readonly string[] KA_GYO_STR = new string[] { "カ", "キ", "ク", "ケ", "コ" };

        /// <summary>
        /// マスタ共通ポップアップにて利用する「さ」行リスト
        /// </summary>
        public static readonly string[] SA_GYO_STR = new string[] { "サ", "シ", "ス", "セ", "ソ" };

        /// <summary>
        /// マスタ共通ポップアップにて利用する「た」行リスト
        /// </summary>
        public static readonly string[] TA_GYO_STR = new string[] { "タ", "チ", "ツ", "テ", "ト" };

        /// <summary>
        /// マスタ共通ポップアップにて利用する「な」行リスト
        /// </summary>
        public static readonly string[] NA_GYO_STR = new string[] { "ナ", "ニ", "ヌ", "ネ", "ノ" };

        /// <summary>
        /// マスタ共通ポップアップにて利用する「は」行リスト
        /// </summary>
        public static readonly string[] HA_GYO_STR = new string[] { "ハ", "ヒ", "フ", "ヘ", "ホ" };

        /// <summary>
        /// マスタ共通ポップアップにて利用する「ま」行リスト
        /// </summary>
        public static readonly string[] MA_GYO_STR = new string[] { "マ", "ミ", "ム", "メ", "モ" };

        /// <summary>
        /// マスタ共通ポップアップにて利用する「や」行リスト
        /// </summary>
        public static readonly string[] YA_GYO_STR = new string[] { "ヤ", "", "ユ", "", "ヨ" };

        /// <summary>
        /// マスタ共通ポップアップにて利用する「ら」行リスト
        /// </summary>
        public static readonly string[] RA_GYO_STR = new string[] { "ラ", "リ", "ル", "レ", "ロ" };

        /// <summary>
        /// マスタ共通ポップアップにて利用する「わ」行リスト
        /// </summary>
        public static readonly string[] WA_GYO_STR = new string[] { "ワ", "", "", "", "" };

        // 頭文字フィルタのための文字列（母音用）
        // ※時間があれば、Unicodeから自動で以下の文字列を生成するのを作りたい
        // なお、DataGridViewのRowFilterには正規表現が使用できないようなので、以下のようにベタで文字を定義する
        /// <summary>頭文字フィルタのための文字列（母音用）ア行</summary>
        public static readonly string Agyou = "'ア', 'イ', 'ウ', 'エ', 'オ', 'ァ', 'ィ', 'ゥ', 'ェ', 'ォ'";
        /// <summary>頭文字フィルタのための文字列（母音用）カ行</summary>
        public static readonly string KAgyou = "'カ', 'キ', 'ク', 'ケ', 'コ', 'ガ', 'ギ', 'グ', 'ゲ', 'ゴ'";
        /// <summary>頭文字フィルタのための文字列（母音用）サ行</summary>
        public static readonly string SAgyou = "'サ', 'シ', 'ス', 'セ', 'ソ', 'ザ', 'ジ', 'ズ', 'ゼ', 'ゾ'";
        /// <summary>頭文字フィルタのための文字列（母音用）タ行</summary>
        public static readonly string TAgyou = "'タ', 'チ', 'ツ', 'テ', 'ト', 'ダ', 'ヂ', 'ヅ', 'デ', 'ド', 'ッ'";
        /// <summary>頭文字フィルタのための文字列（母音用）ナ行</summary>
        public static readonly string NAgyou = "'ナ', 'ニ', 'ヌ', 'ネ', 'ノ'";
        /// <summary>頭文字フィルタのための文字列（母音用）ハ行</summary>
        public static readonly string HAgyou = "'ハ', 'ヒ', 'フ', 'ヘ', 'ホ', 'バ', 'ビ', 'ブ', 'ベ', 'ボ', 'パ', 'ピ', 'プ', 'ペ', 'ポ'";
        /// <summary>頭文字フィルタのための文字列（母音用）マ行</summary>
        public static readonly string MAgyou = "'マ', 'ミ', 'ム', 'メ', 'モ'";
        /// <summary>頭文字フィルタのための文字列（母音用）ヤ行</summary>
        public static readonly string YAgyou = "'ヤ', 'ユ', 'ヨ', 'ャ', 'ュ', 'ョ'";
        /// <summary>頭文字フィルタのための文字列（母音用）ラ行</summary>
        public static readonly string RAgyou = "'ラ', 'リ', 'ル', 'レ', 'ロ'";
        /// <summary>頭文字フィルタのための文字列（母音用）ワ行</summary>
        public static readonly string WAgyou = "'ワ', 'ヮ', 'ヰ', 'ヱ', 'ヲ', 'ン', 'ヴ', 'ヵ', 'ヶ'";
        /// <summary>頭文字フィルタのための文字列（母音用）英数字</summary>
        public static readonly string alphanumeric = "'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', "
            + "'０', '１', '２', '３', '４', '５', '６', '７', '８', '９', "
            + "'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', "
            + "'Ａ', 'Ｂ', 'Ｃ', 'Ｄ', 'Ｅ', 'Ｆ', 'Ｇ', 'Ｈ', 'Ｉ', 'Ｊ', 'Ｋ', 'Ｌ', 'Ｍ', 'Ｎ', 'Ｏ', 'Ｐ', 'Ｑ', 'Ｒ', 'Ｓ', 'Ｔ', 'Ｕ', 'Ｖ', 'Ｗ', 'Ｘ', 'Ｙ', 'Ｚ', "
            + "'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', "
            + "'ａ', 'ｂ', 'ｃ', 'ｄ', 'ｅ', 'ｆ', 'ｇ', 'ｈ', 'ｉ', 'ｊ', 'ｋ', 'ｌ', 'ｍ', 'ｎ', 'ｏ', 'ｐ', 'ｑ', 'ｒ', 'ｓ', 'ｔ', 'ｕ', 'ｖ', 'ｗ', 'ｘ', 'ｙ', 'ｚ'";

        //子音用
        /// <summary>頭文字フィルタのための文字配列（子音用）ア行</summary>
        public static readonly string[] Agyou_SHIIN = new string[] { "'ア','ァ'", "'イ','ィ'", "'ウ','ゥ'", "'エ','ェ'", "'オ','ォ'" };
        /// <summary>頭文字フィルタのための文字配列（子音用）カ行</summary>
        public static readonly string[] KAgyou_SHIIN = new string[] { "'カ','ガ'", "'キ','ギ'", "'ク','グ'", "'ケ','ゲ'", "'コ','ゴ'" };
        /// <summary>頭文字フィルタのための文字配列（子音用）サ行</summary>
        public static readonly string[] SAgyou_SHIIN = new string[] { "'サ','ザ'", "'シ','ジ'", "'ス','ズ'", "'セ','ゼ'", "'ソ','ゾ'" };
        /// <summary>頭文字フィルタのための文字配列（子音用）タ行</summary>
        public static readonly string[] TAgyou_SHIIN = new string[] { "'タ','ダ'", "'チ','ヂ'", "'ツ','ヅ','ッ'", "'テ','デ'", "'ト','ド'" };
        /// <summary>頭文字フィルタのための文字配列（子音用）ナ行</summary>
        public static readonly string[] NAgyou_SHIIN = new string[] { "'ナ'", "'ニ'", "'ヌ'", "'ネ'", "'ノ'" };
        /// <summary>頭文字フィルタのための文字配列（子音用）ハ行</summary>
        public static readonly string[] HAgyou_SHIIN = new string[] { "'ハ','バ','パ'", "'ヒ','ビ','ピ'", "'フ','ブ','プ'", "'ヘ','ベ','ペ'", "'ホ','ボ','ポ'" };
        /// <summary>頭文字フィルタのための文字配列（子音用）マ行</summary>
        public static readonly string[] MAgyou_SHIIN = new string[] { "'マ'", "'ミ'", "'ム'", "'メ'", "'モ'" };
        /// <summary>頭文字フィルタのための文字配列（子音用）ヤ行</summary>
        public static readonly string[] YAgyou_SHIIN = new string[] { "'ヤ','ャ'", "''", "'ユ','ュ'", "''", "'ヨ','ョ'" };//ヤは1,3,5
        /// <summary>頭文字フィルタのための文字配列（子音用）ラ行</summary>
        public static readonly string[] RAgyou_SHIIN = new string[] { "'ラ'", "'リ'", "'ル'", "'レ'", "'ロ'" };
        /// <summary>頭文字フィルタのための文字配列（子音用）ワ行</summary>
        public static readonly string[] WAgyou_SHIIN = new string[] { "'ワ', 'ヮ', 'ヰ', 'ヱ', 'ヲ', 'ン', 'ヴ', 'ヵ', 'ヶ'", "''", "''", "''", "''" }; //ワは1だけ

        /// <summary>SQL Excetionで独自のエラーメッセージを出したい場合があるので定数を定義</summary>
        public static readonly int SQL_EXCEPTION_NUMBER_DUPLICATE = 2627;

        #region dicon関連
        // デフォルト引数として使用しているためconstを使用
        // Dao.dicon:componentsのnamespaceを定義
        /// <summary>環境将軍R用(Dao.dicon)のnamespace</summary>
        public const string DAO = "Dao";
        /// <summary>ファイルアップロードの環境将軍用(Dao_File.dicon)のnamespace</summary>
        public const string DAO_FILE = "DaoFile";
        /// <summary>ファイルアップロードの環境将軍用(Dao_Log.dicon)のnamespace</summary>
        public const string DAO_LOG = "DaoLog";
        #endregion

        #region Communicate InxsSubApplication

        public const int WM_COPYDATA = 0x4A;
        public const string StartFormText = "将軍-INXS　サブアプリ";
        public const string INXS_PROGRAM_PATH = "InxsSubApplication\\Inxs.SubApplication.exe";
        public const string INXS_DBCONNECTION_LIST_FILE_PATH = "InxsSubApplication\\DatabaseConnectList.xml";
        #endregion
    }
}