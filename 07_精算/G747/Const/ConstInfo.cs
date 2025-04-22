
namespace Shougun.Core.Adjustment.InxsShiharaiMeisaishoHakko
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
        public static readonly string HIKAE_INSATSU_KBN_MIINSATUS = "1";
        /// <summary>発行区分：2.発行済</summary>
        public static readonly string HIKAE_INSATSU_KBN_INSATSUZUMI = "2";
        /// <summary>発行区分：3.全て</summary>
        public static readonly string HIKAE_INSATSU_KBN_SUBETE = "3";

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



        /// <summary>発行</summary>
        public const string COL_HAKKOU = "colHakkou";

        /// <summary>公開ユーザー確認</summary>
        public const string COL_PUBLIC_USER_CONFIRM = "COL_PUBLIC_USER_CONFIRM";

        /// <summary>公開ユーザー設定</summary>
        public const string COL_PUBLISHED_USER_SETTING_BUTTON = "COL_PUBLISHED_USER_SETTING_BUTTON";

        /// <summary>アップロード状況</summary>
        public const string COL_UPLOAD_STATUS = "COL_UPLOAD_STATUS";

        /// <summary>ダウンロード状況</summary>
        public const string COL_DOWNLOAD_STATUS = "COL_DOWNLOAD_STATUS";

        /// <summary>伝票番号</summary>
        public const string COL_SEISAN_NUMBER = "colDenpyoNumber";

        /// <summary>精算日付</summary>
        public const string COL_SEISAN_DATE = "colSeisanDate";

        /// <summary>取引先CD</summary>
        public const string COL_TORIHIKISAKI_CD = "colTorihikisakiCd";

        /// <summary>取引先名</summary>
        public const string COL_TORIHIKISAKI_NAME = "colTorihikisakiName";

        /// <summary>締日</summary>
        public const string COL_SHIMEBI = "colShimebi";

        /// <summary>前回繰越額</summary>
        public const string COL_ZENKAIKURIKOSHI_GAKU = "colZenkaiKurikoshiGaku";

        /// <summary>支払額</summary>
        public const string COL_SHIHARAI_GAKU = "colShiharaiGaku";

        /// <summary>調整額</summary>
        public const string COL_CHOUSEI_GAKU = "colChouseiGaku";

        /// <summary>今回支払金額</summary>
        public const string COL_KONKAI_SHIHARAI_GAKU = "colKonkaiShiharaiGaku";

        /// <summary>消費税</summary>
        public const string COL_SHOHIZEI = "colShohizei";

        /// <summary>今回精算額</summary>
        public const string COL_KONKAI_SEISAN_GAKU = "colKonkaiSeisanGaku";

        /// <summary>支払予定日</summary>
        public const string COL_SHIHARAI_YOTEI_BI = "colShiharaiYoteiBi";

        /// <summary>タイムスタンプ</summary>
        public const string COL_TIME_STAMP = "colTimeStamp";

        /// <summary>COL_PUBLISHED_USER_SETTING</summary>
        public const string COL_PUBLISHED_USER_SETTING = "COL_PUBLISHED_USER_SETTING";

        /// <summary>COL_PUBLIC_USER_SETTINGS</summary>
        public const string COL_PUBLIC_USER_SETTINGS = "COL_PUBLIC_USER_SETTINGS";

        /// <summary>COL_PUBLIC_USER_SETTINGS</summary>
        public const string COL_HIKAE_INSATSU_KBN = "colHikaeInsatsuKbn";
    }
}
