
namespace Shougun.Core.Adjustment.ShiharaiMeisaishoHakko
{
    internal class ConstCls
    {
        public const string DialogTitle = "インフォメーション";
        public const string SearchEmptInfo = "検索結果は見つかりません。";
        public const string ErrStop2 = "締日は【0,5,10,15,20,25,31】のみ入力してください。";
        public const string ToolTipText1 = "支払明細書を印刷する明細をチェックしてください";

        //エラーメッセージID
        public const string ERR_MSG_CD_C001 = "C001";

        //書式区分（支払先別）
        public const string SHOSHIKI_KBN_1 = "1";
        //書式区分（業者別）
        public const string SHOSHIKI_KBN_2 = "2";
        //書式区分（現場別）
        public const string SHOSHIKI_KBN_3 = "3";
        //書式明細区分（なし）
        public const string SHOSHIKI_MEISAI_KBN_1 = "1";
        //書式明細区分（業者毎）
        public const string SHOSHIKI_MEISAI_KBN_2 = "2";
        //支払明細書書式3(1.現場名あり)
        public const string SHOSHIKI_GENBA_KBN_1 = "1";
        //支払明細書書式3(1.現場名なし)
        public const string SHOSHIKI_GENBA_KBN_2 = "2";
        //書式明細区分（現場毎）
        public const string SHOSHIKI_MEISAI_KBN_3 = "3";
        //伝票種類（受入）
        public const string DENPYOU_SHURUI_CD_1 = "1";
        //伝票種類（出荷）
        public const string DENPYOU_SHURUI_CD_2 = "2";
        //伝票種類（売上）
        public const string DENPYOU_SHURUI_CD_3 = "3";
        //伝票種類（出金）
        public const string DENPYOU_SHURUI_CD_20 = "20";
        //印字する
        public const string PRINT_KBN_1 = "1";
        //印字しない
        public const string PRINT_KBN_2 = "2";
        //単月精算
        public const string SEISAN_KEITAI_KBN_1 = "1";
        //品名税無し
        public const string DENPYOU_ZEI_KBN_CD_0 = "0";
        //内税
        public const string DENPYOU_ZEI_KBN_CD_2 = "2";
        //郵便マック
        public const string YUBIN = "〒";
        //全角スペース
        public const string ZENKAKU_SPACE = "　";
        //スラッシュ
        public const string SLASH = "/";
        /// <summary>支払明細書印刷日：1.締日</summary>
        public static readonly string SHIHARAI_PRINT_DAY_SIMEBI = "1";
        /// <summary>支払明細書印刷日：2.発行日</summary>
        public static readonly string SHIHARAI_PRINT_DAY_HAKKOBI = "2";
        /// <summary>支払明細書印刷日：3.無し</summary>
        public static readonly string SHIHARAI_PRINT_DAY_NASI = "3";
        /// <summary>支払明細書印刷日：4.指定</summary>
        public static readonly string SHIHARAI_PRINT_DAY_SITEI = "4";
        /// <summary>請求書発行日：1.印刷する</summary>
        public static readonly string SHIHARAI_HAKKOU_PRINT_SURU = "1";
        /// <summary>請求書発行日：2.印刷しない</summary>
        public static readonly string SHIHARAI_HAKKOU_PRINT_SHINAI = "2";
        /// <summary>支払形態：1.支払データ作成時</summary>
        public static readonly string SHIHARAI_KEITAI_DATA_SAKUSEIJI = "1";
        /// <summary>支払形態：2.単月請求</summary>
        public static readonly string SHIHARAI_KEITAI_TANGETU_SEIKYU = "2";
        /// <summary>支払形態：3.繰越請求</summary>
        public static readonly string SHIHARI_KEITAI_KURIKOSI_SEIKYU = "3";
        //単月精算
        public const string SHIHARAI_KEITAI_KBN_1 = "1";

        /// <summary>
        /// 支払用紙 : 1.支払データ作成時/自社
        /// </summary>
        public static readonly string SHIHARAI_PAPER_DATA_SAKUSEIJI_JISYA = "1";

        /// <summary>
        /// 支払用紙 : 2.支払データ作成時/指定
        /// </summary>
        public static readonly string SHIHARAI_PAPER_DATA_SAKUSEIJI_SHITEI = "2";

        /// <summary>
        /// 支払用紙 : 3.自社
        /// </summary>
        public static readonly string SHIHARAI_PAPER_DATA_JISYA = "3";

        /// <summary>
        /// 支払用紙 : 4.指定
        /// </summary>
        public static readonly string SHIHARAI_PAPER_DATA_SHITEI = "4";

        /// <summary>
        /// 明細 : 1.有り
        /// </summary>
        public static readonly string SHUKKIN_MEISAI_ARI = "1";

        /// <summary>
        /// 明細 : 2.無し
        /// </summary>
        public static readonly string SHUKKIN_MEISAI_NASHI = "2";

        /// <summary>
        /// 税区分 : 0.税無し
        /// </summary>
        public static readonly string ZEI_KBN_NASHI = "0";

        /// <summary>
        /// 税区分 : 1.外税
        /// </summary>
        public static readonly string ZEI_KBN_SOTO = "1";

        /// <summary>
        /// 税区分 : 2.内税
        /// </summary>
        public static readonly string ZEI_KBN_UCHI = "2";

        /// <summary>
        /// 税区分 : 3.非課税
        /// </summary>
        public static readonly string ZEI_KBN_HIKAZEI = "3";

        /// <summary>
        /// 税計算区分 : 1.伝票毎
        /// </summary>
        public static readonly string ZEI_KEISAN_KBN_DENPYOU = "1";

        /// <summary>
        /// 税計算区分 : 2.請求毎
        /// </summary>
        public static readonly string ZEI_KEISAN_KBN_SEIKYUU = "2";

        /// <summary>
        /// 税計算区分 : 3.明細毎
        /// </summary>
        public static readonly string ZEI_KEISAN_KBN_MEISAI = "3";

        /// <summary>発行区分：1.未発行</summary>
        public static readonly string HAKKOU_KBN_MIHAKKOU = "1";
        /// <summary>発行区分：2.発行済</summary>
        public static readonly string HAKKOU_KBN_HAKKOUZUMI = "2";
        /// <summary>発行区分：3.全て</summary>
        public static readonly string HAKKOU_KBN_SUBETE = "3";

        /// <summary>印刷順：１．フリガナ</summary>
        public static readonly string PRINT_ORDER_FURIGANA = "1";

        // 括弧（開始）
        public static string KAKKO_START = "(";
        // 括弧（終了）
        public static string KAKKO_END = ")";
        // 精算毎税は除く
        public static string SEISAN_ZEI_EXCEPT = "精算毎税は除く";

        // 抽出データ(支払発生なし含む)
        public static int FILTERING_DATA_INCLUDE_OTHER = 2;

        public const int MAX_ROW_CORP_INFO = 8;

        public const int MAX_ROW_PAGE_ONE = 20;
        public const int MAX_ROW_PAGE_ONE_OVER = 32;

        public const int GridColumHeaderHeight = 47;//明細のカラムの高さ

        //支払(控)印刷　ソート印刷
        public const string HIKAE_OUTPUT_SORT = "1";
        //支払(控)印刷　グループ印刷
        public const string HIKAE_OUTPUT_GROUP = "2";
        //支払(控)印刷　印刷しない
        public const string HIKAE_OUTPUT_NON = "3";
    }
}
