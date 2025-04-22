
namespace Shougun.Core.Billing.SeikyuushoHakkou.Const
{
    internal class ConstCls
    {
        //書式区分（請求先別）
        public const string SHOSHIKI_KBN_1 = "1";
        //書式区分（業者別）
        public const string SHOSHIKI_KBN_2 = "2";
        //書式区分（現場別）
        public const string SHOSHIKI_KBN_3 = "3";
        //請求書書式3(1.現場名あり)
        public const string SHOSHIKI_GENBA_KBN_1 = "1";
        //請求書書式3(1.現場名なし)
        public const string SHOSHIKI_GENBA_KBN_2 = "2";
        //書式明細区分（なし）
        public const string SHOSHIKI_MEISAI_KBN_1 = "1";
        //書式明細区分（業者毎）
        public const string SHOSHIKI_MEISAI_KBN_2 = "2";
        //書式明細区分（現場毎）
        public const string SHOSHIKI_MEISAI_KBN_3 = "3";
        //伝票種類（受入）
        public const string DENPYOU_SHURUI_CD_1 = "1";
        //伝票種類（出荷）
        public const string DENPYOU_SHURUI_CD_2 = "2";
        //伝票種類（売上）
        public const string DENPYOU_SHURUI_CD_3 = "3";
        //伝票種類（入金）
        public const string DENPYOU_SHURUI_CD_10 = "10";
        //印字する
        public const string PRINT_KBN_1 = "1";
        //印字しない
        public const string PRINT_KBN_2 = "2";
        //単月請求
        public const string SEIKYUU_KEITAI_KBN_1 = "1";
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
        // 受入入力のアセンブリ情報
        public static readonly string UKEIRE_NYUURYOKU_ASSEMBLY_NAME = "UkeireNyuuryoku.dll";
        // 受入入力のForm
        public static readonly string UKEIRE_NYUURYOKU_FORM_NAME = "Shougun.Core.SalesPayment.UkeireNyuuryoku.UIForm";
        // 受入入力のFooterForm
        public static readonly string UKEIRE_NYUURYOKU_FOOTERFORM_NAME = "Shougun.Core.SalesPayment.UkeireNyuuryoku.UIFooterForm";
        // 出荷入力のアセンブリ情報
        public static readonly string SYUKKA_NYUURYOKU_ASSEMBLY_NAME = "SyukkaNyuuryoku.dll";
        // 出荷入力のForm
        public static readonly string SYUKKA_NYUURYOKU_FORM_NAME = "Shougun.Core.SalesPayment.SyukkaNyuuryoku.UIForm";
        // 出荷入力のFooterForm
        public static readonly string SYUKKA_NYUURYOKU_FOOTERFORM_NAME = "Shougun.Core.SalesPayment.SyukkaNyuuryoku.UIFooterForm";
        // 売上支払入力のアセンブリ情報
        public static readonly string URIAGESHIHARAI_NYUURYOKU_ASSEMBLY_NAME = "UriageShiharaiNyuuryoku.dll";
        // 売上支払入力のForm
        public static readonly string URIAGESHIHARAI_NYUURYOKU_FORM_NAME = "Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.UIForm";
        // 売上支払入力のFooterForm
        public static readonly string URIAGESHIHARAI_NYUURYOKU_FOOTERFORM_NAME = "Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.UIFooterForm";
        // 請求一覧のアセンブリ情報
        public static readonly string SEIKYU_ICHIRAN_ASSEMBLY_NAME = "Seikyuichiran.dll";
        // 請求一覧のHeaderForm
        public static readonly string SEIKYU_ICHIRAN_HEADERFORM_NAME = "Shougun.Core.Billing.Seikyuichiran.UIHeader";
        // 請求一覧のForm
        public static readonly string SEIKYU_ICHIRAN_FORM_NAME = "Shougun.Core.Billing.Seikyuichiran.SeikyuuIchiranForm";

        /// <summary>
        /// 請求用紙 : 1.請求データ作成時/自社
        /// </summary>
        public static readonly string SEIKYU_PAPER_DATA_SAKUSEIJI_JISYA = "1";

        /// <summary>
        /// 請求用紙 : 2.請求データ作成時/指定
        /// </summary>
        public static readonly string SEIKYU_PAPER_DATA_SAKUSEIJI_SHITEI = "2";

        /// <summary>
        /// 請求用紙 : 3.自社
        /// </summary>
        public static readonly string SEIKYU_PAPER_DATA_JISYA = "3";

        /// <summary>
        /// 請求用紙 : 4.指定
        /// </summary>
        public static readonly string SEIKYU_PAPER_DATA_SHITEI = "4";

        /// <summary>請求形態：1.請求データ作成時</summary>
        public static readonly string SEIKYU_KEITAI_DATA_SAKUSEIJI = "1";
        /// <summary>請求形態：2.単月請求</summary>
        public static readonly string SEIKYU_KEITAI_TANGETU_SEIKYU = "2";
        /// <summary>請求形態：3.繰越請求</summary>
        public static readonly string SEIKYU_KEITAI_KURIKOSI_SEIKYU = "3";

        /// <summary>
        /// 明細 : 1.有り
        /// </summary>
        public static readonly string NYUKIN_MEISAI_ARI = "1";

        /// <summary>
        /// 明細 : 2.無し
        /// </summary>
        public static readonly string NYUKIN_MEISAI_NASHI = "2";

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

        /// <summary>請求書印刷日：1.締日</summary>
        public static readonly string SEIKYU_PRINT_DAY_SIMEBI = "1";
        /// <summary>請求書印刷日：2.発行日</summary>
        public static readonly string SEIKYU_PRINT_DAY_HAKKOBI = "2";
        /// <summary>請求書印刷日：3.無し</summary>
        public static readonly string SEIKYU_PRINT_DAY_NASI = "3";
        /// <summary>請求書印刷日：4.指定</summary>
        public static readonly string SEIKYU_PRINT_DAY_SITEI = "4";

        /// <summary>請求書発行日：1.印刷する</summary>
        public static readonly string SEIKYU_HAKKOU_PRINT_SURU = "1";
        /// <summary>請求書発行日：2.印刷しない</summary>
        public static readonly string SEIKYU_HAKKOU_PRINT_SHINAI = "2";

        /// <summary>発行区分：1.未発行</summary>
        public static readonly string HAKKOU_KBN_MIHAKKOU = "1";
        /// <summary>発行区分：2.発行済</summary>
        public static readonly string HAKKOU_KBN_HAKKOUZUMI = "2";
        /// <summary>発行区分：3.全て</summary>
        public static readonly string HAKKOU_KBN_SUBETE = "3";

        // 20160429 koukoukon v2.1_電子請求書 #16612 start
        /// <summary>出力区分：1.紙</summary>
        public static readonly string OUTPUT_KBN_KAMI = "1";
        /// <summary>出力区分：2.電子CSV</summary>
        public static readonly string OUTPUT_KBN_DENSICSV = "2";
        // 20211207 thucp 電子請求書 #157799 begin
        /// <summary>出力区分：3.楽楽CSV</summary>
        public static readonly string OUTPUT_KBN_RAKURAKUCSV = "3";
        /// <summary>出力区分：4.全て</summary>
        public static readonly string OUTPUT_KBN_SUBETE = "4";
        // 20211207 thucp 電子請求書 #157799 end
        // 20160429 koukoukon v2.1_電子請求書 #16612 end

        /// <summary>印刷順：１．フリガナ</summary>
        public static readonly string PRINT_ORDER_FURIGANA = "1";

        public const string DialogTitle = "インフォメーション";
        public const string SearchEmptInfo = "検索結果は見つかりません。";
        public const string ErrStop1 = "遷移先画面がまだ作成中ですので、処理中止します。";
        public const string ErrStop2 = "締日は【0,5,10,15,20,25,31】のみ入力してください。";
        public const string ToolTipText1 = "請求書を印刷する明細をチェックしてください";

        //エラーメッセージID
        public const string ERR_MSG_CD_C001 = "C001";
        // 括弧（開始）
        public static string KAKKO_START = "(";
        // 括弧（終了）
        public static string KAKKO_END = ")";
        // 請求税毎は除く
        public static string SEIKYU_ZEI_EXCEPT = "請求毎税は除く";

        // 抽出データ(売上発生なし含む)
        public static int FILTERING_DATA_INCLUDE_OTHER = 2;

        public const int MAX_ROW_CORP_INFO = 10;

        public const int MAX_ROW_PAGE_ONE = 20;
        public const int MAX_ROW_PAGE_ONE_OVER = 32;

        public const int GridColumHeaderHeight = 47;//明細のカラムの高さ

        //請求(控)印刷　ソート印刷
        public const string HIKAE_OUTPUT_SORT = "1";
        //請求(控)印刷　グループ印刷
        public const string HIKAE_OUTPUT_GROUP = "2";
        //請求(控)印刷　印刷しない
        public const string HIKAE_OUTPUT_NON = "3";

        public const int RAKURAKU_CSV_COLUMNS_COUNT = 86; // 20211208 thucp v2.24_電子請求書 #157799
    }
}
