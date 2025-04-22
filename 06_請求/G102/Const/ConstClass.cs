
namespace Shougun.Core.Billing.Seikyushokakunin.Const
{
    class ConstClass
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
        // 請求書印刷日：1.締日
        public static readonly string SEIKYU_PRINT_DAY_SIMEBI = "1";
        // 請求書発行日：2.印刷しない
        public static readonly string SEIKYU_HAKKOU_PRINT_SHINAI = "2";

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

        // 請求形態：1.請求データ作成時
        public static readonly string SEIKYU_KEITAI_DATA_SAKUSEIJI = "1";

        /// <summary>
        /// 入金明細区分：1.表示する
        /// </summary>
        public static readonly string NYUUKIN_MEISAI_KBN1 = "1";
        /// <summary>
        /// 入金明細区分：2.表示なし
        /// </summary>
        public static readonly string NYUUKIN_MEISAI_KBN2 = "2";

        //【1、2、3】のいずれかで入力してください
        public const string TOOL_TIP_TXT_1 = "【1、2、3】のいずれかで入力してください";
        //用紙区分を請求データ作成時に決定する場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_2 = "用紙区分を請求データ作成時に決定する場合にはチェックを付けてください";
        //用紙区分を自社請求に決定する場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_3 = "用紙区分を自社請求に決定する場合にはチェックを付けてください";
        //用紙区分を指定請求に決定する場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_4 = "用紙区分を指定請求に決定する場合にはチェックを付けてください";
        //【1、2、3】のいずれかで入力してください
        public const string TOOL_TIP_TXT_5 = "【1、2、3】のいずれかで入力してください";
        //繰越請求を請求データ作成時に決定する場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_6 = "繰越請求を請求データ作成時に決定する場合にはチェックを付けてください";
        //繰越請求を有りに決定する場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_7 = "繰越請求を有りに決定する場合にはチェックを付けてください";
        //繰越請求を無しに決定する場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_8 = "繰越請求を無しに決定する場合にはチェックを付けてください";
        //【1、2、3、4】のいずれかで入力してください
        public const string TOOL_TIP_TXT_9 = "【1、2、3、4】のいずれかで入力してください";
        //請求書印刷日を締日に決定する場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_10 = "請求書印刷日を締日に決定する場合にはチェックを付けてください";
        //請求書印刷日を発行日に決定する場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_11 = "請求書印刷日を発行日に決定する場合にはチェックを付けてください";
        //請求書印刷日を無しに決定する場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_12 = "請求書印刷日を無しに決定する場合にはチェックを付けてください";
        //請求書印刷日を指定に決定する場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_13 = "請求書印刷日を指定に決定する場合にはチェックを付けてください";
        //請求書印刷日を入力してください
        public const string TOOL_TIP_TXT_14 = "請求書印刷日を入力してください";
        //【1、2】のいずれかで入力してください
        public const string TOOL_TIP_TXT_15 = "【1、2】のいずれかで入力してください";
        //印刷順をフリガナに決定する場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_16 = "印刷順をフリガナに決定する場合にはチェックを付けてください";
        //印刷順を取引先CDに決定する場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_17 = "印刷順を取引先CDに決定する場合にはチェックを付けてください";
        //前の業者又は現場の請求書を表示します。
        public const string TOOL_TIP_TXT_18 = "前の業者又は現場の請求書を表示します。";
        //次の業者又は現場の請求書を表示します。
        public const string TOOL_TIP_TXT_19 = "次の業者又は現場の請求書を表示します。";
        //既存データを参照する画面に切り替わります
        public const string TOOL_TIP_TXT_20 = "既存データを参照する画面に切り替わります";
        //請求書の印刷を行います。
        public const string TOOL_TIP_TXT_21 = "請求書の印刷を行います。";
        //一覧画面に移動します
        public const string TOOL_TIP_TXT_22 = "一覧画面に移動します";
        //請求データの削除を行います。
        public const string TOOL_TIP_TXT_23 = "請求データの削除を行います。";
        //画面を閉じます
        public const string TOOL_TIP_TXT_24 = "画面を閉じます";
        //半角1桁以内で入力してください
        public const string TOOL_TIP_TXT_25 = "半角1桁以内で入力してください";
        // 請求税毎は除く
        public static string SEIKYU_ZEI_EXCEPT = "請求毎税は除く";
        //インフォメーション
        public const string DialogTitle = "インフォメーション";
        //請求書発行日は入力必須です。
        public const string ErrStop1 = "請求書発行日は入力必須です。";

        #region GridColumn
        //月日
        public const string COL_DENPYOU_DATE = "DENPYOU_DATE";
        //売上No
        public const string COL_DENPYOU_NUMBER = "DENPYOU_NUMBER";
        //品名
        public const string COL_HINMEI_NAME = "HINMEI_NAME";
        //数量
        public const string COL_SUURYOU = "SUURYOU";
        //単位
        public const string COL_UNIT_NAME = "UNIT_NAME";
        //単価
        public const string COL_TANKA = "TANKA";
        //金額
        public const string COL_KINGAKU = "KINGAKU";
        //消費税
        public const string COL_SHOUHIZEI = "SHOUHIZEI";
        //備考
        public const string COL_MEISAI_BIKOU = "MEISAI_BIKOU";
        //明細行判断用フラグ
        public const string COL_OUT_FLG = "OUT_FLG";
        //システムID_DENPYOU_SYSTEM_ID
        public const string COL_DENPYOU_SYSTEM_ID = "DENPYOU_SYSTEM_ID";
        //枝番_DENPYOU_SEQ
        public const string COL_DENPYOU_SEQ = "DENPYOU_SEQ";
        //伝票種類
        public const string COL_DENPYOU_SHURUI_CD = "DENPYOU_SHURUI_CD";
        #endregion
    }
}
