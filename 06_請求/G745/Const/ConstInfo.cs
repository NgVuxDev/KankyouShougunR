
namespace Shougun.Core.Billing.InxsSeikyuushoHakkou.Const
{
    internal class ConstCls
    {
        //書式区分（請求先別）
        public const string SHOSHIKI_KBN_1 = "1";
        //書式区分（業者別）
        public const string SHOSHIKI_KBN_2 = "2";
        //書式区分（現場別）
        public const string SHOSHIKI_KBN_3 = "3";
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
        public static readonly string HIKAE_INSATSU_KBN_MIINSATSU = "1";
        /// <summary>発行区分：2.発行済</summary>
        public static readonly string HIKAE_INSATSU_KBN_INSATSUZUMI = "2";
        /// <summary>発行区分：3.全て</summary>
        public static readonly string HIKAE_INSATSU_KBN_SUBETE = "3";

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


        /// <summary>発行：COL_HAKKOU</summary>
        public static readonly string COL_HAKKOU = "COL_HAKKOU";

        /// <summary>公開ユーザー確認：COL_PUBLIC_USER_CONFIRM</summary>
        public static readonly string COL_PUBLIC_USER_CONFIRM = "COL_PUBLIC_USER_CONFIRM";

        public const string NEED_USER_CONFIRMATION_TEXT = "要確認";

        /// <summary>公開ユーザー設定：COL_PUBLISHED_USER_SETTING_BUTTON</summary>
        public static readonly string COL_PUBLISHED_USER_SETTING_BUTTON = "COL_PUBLISHED_USER_SETTING_BUTTON";

        /// <summary>公開ユーザー設定Value：COL_PUBLISHED_USER_SETTING</summary>
        public static readonly string COL_PUBLISHED_USER_SETTING = "COL_PUBLISHED_USER_SETTING";

        /// <summary>COL_PUBLIC_USER_SETTINGS</summary>
        public static readonly string COL_PUBLIC_USER_SETTINGS = "COL_PUBLIC_USER_SETTINGS";

        /// <summary>アップロード状況：COL_UPLOAD_STATUS</summary>
        public static readonly string COL_UPLOAD_STATUS = "COL_UPLOAD_STATUS";

        /// <summary>ダウンロード状況：COL_DOWNLOAD_STATUS</summary>
        public static readonly string COL_DOWNLOAD_STATUS = "COL_DOWNLOAD_STATUS";

        /// <summary>伝票番号：COL_SEIKYUU_NUMBER</summary>
        public static readonly string COL_SEIKYUU_NUMBER = "COL_SEIKYUU_NUMBER";

        /// <summary>請求日付：COL_SEIKYUU_DATE</summary>
        public static readonly string COL_SEIKYUU_DATE = "COL_SEIKYUU_DATE";

        /// <summary>取引先CD：COL_TORIHIKISAKI_CD</summary>
        public static readonly string COL_TORIHIKISAKI_CD = "COL_TORIHIKISAKI_CD";

        /// <summary>取引先名：COL_TORIHIKISAKI_NAME</summary>
        public static readonly string COL_TORIHIKISAKI_NAME = "COL_TORIHIKISAKI_NAME";

        /// <summary>締日：COL_SHIMEBI</summary>
        public static readonly string COL_SHIMEBI = "COL_SHIMEBI";

        /// <summary>前回請求額：COL_ZENKAI_KURIKOSI_GAKU</summary>
        public static readonly string COL_ZENKAI_KURIKOSI_GAKU = "COL_ZENKAI_KURIKOSI_GAKU";

        /// <summary>入金額：COL_KONKAI_NYUUKIN_GAKU</summary>
        public static readonly string COL_KONKAI_NYUUKIN_GAKU = "COL_KONKAI_NYUUKIN_GAKU";

        /// <summary>調整額：COL_KONKAI_CHOUSEI_GAKU</summary>
        public static readonly string COL_KONKAI_CHOUSEI_GAKU = "COL_KONKAI_CHOUSEI_GAKU";

        /// <summary>売上金額：COL_KONKAI_URIAGE_GAKU</summary>
        public static readonly string COL_KONKAI_URIAGE_GAKU = "COL_KONKAI_URIAGE_GAKU";

        /// <summary>消費税：COL_SHOHIZEI_GAKU</summary>
        public static readonly string COL_SHOHIZEI_GAKU = "COL_SHOHIZEI_GAKU";

        /// <summary>今回請求額：COL_KONKAI_SEIKYU_GAKU</summary>
        public static readonly string COL_KONKAI_SEIKYU_GAKU = "COL_KONKAI_SEIKYU_GAKU";

        /// <summary>入金予定日：COL_NYUUKIN_YOTEI_BI</summary>
        public static readonly string COL_NYUUKIN_YOTEI_BI = "COL_NYUUKIN_YOTEI_BI";

        /// <summary>タイムスタンプ：COL_TIME_STAMP</summary>
        public static readonly string COL_TIME_STAMP = "COL_TIME_STAMP";

        /// <summary>控え済：COL_HIKAE_SUMI</summary>
        public static readonly string COL_HIKAE_INSATSU_KBN = "COL_HIKAE_INSATSU_KBN";
    }
}
