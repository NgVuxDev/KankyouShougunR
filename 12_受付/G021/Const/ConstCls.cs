// $Id: ConstCls.cs 55371 2015-07-10 11:07:15Z t-thanhson@e-mall.co.jp $

namespace Shougun.Core.Reception.UketukeiIchiran
{
    class ConstCls
    {
        /// <summary>画面Title</summary>
        public const string Title = "受付一覧";

        /// <summary>拠点CD（全社：99）</summary>
        public const string KyouTenZenSya = "99";

        /// <summary>伝票日付Code</summary>
        public const string HidukeCD_DenPyou = "1";
        /// <summary>伝票日付Name</summary>
        public const string HidukeName_DenPyou = "伝票日付※";

        /// <summary>入力日付Code</summary>
        public const string HidukeCD_NyuuRyoku = "2";
        /// <summary>入力日付Name</summary>
        public const string HidukeName_NyuuRyoku = "入力日付※";

        /// <summary>作業日Code</summary>
        public const string HidukeCD_Sagyou = "3";
        /// <summary>作業日Name</summary>
        public const string HidukeName_Sagyou = "作業日※";
        /// <summary> 納品予定日Name </summary>
        public const string HidukeName_NouhinYotei = "納品予定日※";

        /// <summary>伝票種類Code</summary>
        public const string DenPyouDefultCD = "1";

        /// <summary>アラート件数の最小値</summary>
        public const long AlertNumber_Min = 1;
        /// <summary>アラート件数の最大値</summary>
        public const long AlertNumber_Max = 99999;

        /// <summary>明細部の印刷列</summary>
        public const string ADD_COLUMN_INSATSU = "INSATSUFLG";
        public const string ADD_COLUMN_INSATSU_NAME = "印刷";

        /// <summary>明細部の非表示列（システムID）</summary>
        public static readonly string HIDDEN_SYSTEM_ID = "SYSTEM_ID_HIDDEN";
        /// <summary>明細部の非表示列（枝番）</summary>
        public static readonly string HIDDEN_SEQ = "SEQ_HIDDEN";
        /// <summary>明細部の非表示列（受付番号）</summary>
        public static readonly string HIDDEN_UKETSUKE_NUMBER = "UKETSUKE_NUMBER_HIDDEN";
        /// <summary>明細部の非表示列（受付日）</summary>
        public static readonly string HIDDEN_UKETSUKE_DATE = "UKETSUKE_DATE_HIDDEN";
        /// <summary>明細部の非表示列（配車状況CD）</summary>
        public static readonly string HIDDEN_HAISHA_JOKYO_CD = "HAISHA_JOKYO_CD_HIDDEN";
        /// <summary>明細部の非表示列（明細システムID）</summary>
        public static readonly string HIDDEN_DETAIL_SYSTEM_ID = "HIDDEN_DETAIL_SYSTEM_ID";
        
        /// <summary>INXS明細部の非表示列（INXS明細システムID）</summary>
        public static readonly string HIDDEN_INXS_DETAIL_SYSTEM_ID = "HIDDEN_INXS_DETAIL_SYSTEM_ID";

        /// <summary>明細部の非表示列（緯度）</summary>
        public static readonly string HIDDEN_GENBA_LATITUDE = "HIDDEN_GENBA_LATITUDE";
        /// <summary>明細部の非表示列（経度）</summary>
        public static readonly string HIDDEN_GENBA_LONGITUDE = "HIDDEN_GENBA_LONGITUDE";
        public static readonly string HIDDEN_DENPYOU_SHURUI_NAME = "DENPYOU_SHURUI_NAME";

        // VUNGUYEN 20150703 START
        /// <summary>明細部の非表示列（配車種類CD）</summary>
        public static readonly string HIDDEN_HAISHA_SHURUI_CD = "HAISHA_SHURUI_CD_HIDDEN";
        // VUNGUYEN 20150703 END

        public const string CSV_NAME = "受付一覧";

        /// <summary>
        /// 伝票種類CD 1:収集
        /// </summary>
        public static readonly string DENPYOU_SHURUI_CD_SYUSYU = "1";

        /// <summary>
        /// 伝票種類CD 2:出荷
        /// </summary>
        public static readonly string DENPYOU_SHURUI_CD_SYUKKA = "2";

        /// <summary>
        /// 伝票種類CD 3:持込
        /// </summary>
        public static readonly string DENPYOU_SHURUI_CD_MOCHIKOMI = "3";

        /// <summary>
        /// 伝票種類CD 4:クレーム
        /// </summary>
        public static readonly string DENPYOU_SHURUI_CD_KUREMU = "4";

        // VUNGUYEN 20150703 START
        /// <summary>
        /// 伝票種類CD 5:収集＋出荷
        /// </summary>
        public static readonly string DENPYOU_SHURUI_CD_SS_SK = "5";

        /// <summary>
        /// 伝票種類CD 6:収集＋持込
        /// </summary>
        public static readonly string DENPYOU_SHURUI_CD_SS_MK = "6";
        // VUNGUYEN 20150703 END

        // SUMMARYテーブル名
        public const string SUMMARY_SYUSYU = "T_UKETSUKE_SS";
        public const string SUMMARY_SYUKKA = "T_UKETSUKE_SK";
        public const string SUMMARY_MOCHIKOMI = "T_UKETSUKE_MK";
        public const string SUMMARY_BUPPAN = "T_UKETSUKE_BP";
        public const string SUMMARY_CUREMU = "T_UKETSUKE_CM";

        // VUNGUYEN 20150703 START
        public const string SUMMARY_SS_SK = "T_UKETSUKE_SS_SK";
        public const string SUMMARY_SS_MK = "T_UKETSUKE_SS_MK";
        // VUNGUYEN 20150703 END

        /// <summary>
        /// 配車状況選択ポップアップのウィンドウタイトル
        /// </summary>
        public static readonly string POPUP_TITLE_HAISHA_JOKYO = "配車状況選択";

        /// <summary>
        /// 配車状況選択ポップアップのカラム名（配車状況CD）
        /// </summary>
        public static readonly string COLUMN_HAISHA_JOKYO_CD = "HAISHA_JOKYO_CD";

        /// <summary>
        /// 配車状況選択ポップアップのカラム名（配車状況）
        /// </summary>
        public static readonly string COLUMN_HAISHA_JOKYO_NAME = "HAISHA_JOKYO_NAME";

        /// <summary>
        /// 配車状況選択ポップアップのカラムヘッダ名（配車状況CD）
        /// </summary>
        public static readonly string HEADER_HAISHA_JOKYO_CD = "配車状況CD";

        /// <summary>
        /// 配車状況選択ポップアップのカラムヘッダ名（配車状況名）
        /// </summary>
        public static readonly string HEADER_HAISHA_JOKYO_NAME = "配車状況名";

        /// <summary>
        /// 配車状況CD「1:受注」
        /// </summary>
        public static readonly string HAISHA_JOKYO_CD_JUCHU = "1";

        /// <summary>
        /// 配車状況CD「2:配車」
        /// </summary>
        public static readonly string HAISHA_JOKYO_CD_HAISHA = "2";

        /// <summary>
        /// 配車状況CD「3:計上」
        /// </summary>
        public static readonly string HAISHA_JOKYO_CD_KEIJO = "3";

        /// <summary>
        /// 配車状況CD「4:キャンセル」
        /// </summary>
        public static readonly string HAISHA_JOKYO_CD_CANCEL = "4";

        /// <summary>
        /// 配車状況CD「5:回収なし」
        /// </summary>
        public static readonly string HAISHA_JOKYO_CD_NASHI = "5";

        /// <summary>
        /// 配車状況「1:受注」
        /// </summary>
        public static readonly string HAISHA_JOKYO_NAME_JUCHU = "受注";

        /// <summary>
        /// 配車状況「2:配車」
        /// </summary>
        public static readonly string HAISHA_JOKYO_NAME_HAISHA = "配車";

        /// <summary>
        /// 配車状況「3:計上」
        /// </summary>
        public static readonly string HAISHA_JOKYO_NAME_KEIJO = "計上";

        /// <summary>
        /// 配車状況「4:キャンセル」
        /// </summary>
        public static readonly string HAISHA_JOKYO_NAME_CANCEL = "キャンセル";

        /// <summary>
        /// 配車状況「5:回収なし」
        /// </summary>
        public static readonly string HAISHA_JOKYO_NAME_NASHI = "回収なし";

        /// <summary>
        /// サブファンクションボタンのテキスト 「[4]受入入力」
        /// </summary>
        public static readonly string PROCESS_BUTTON_TEXT_UKEIRE = "[4]受入入力";

        /// <summary>
        /// サブファンクションボタンのテキスト 「[4]出荷入力」
        /// </summary>
        public static readonly string PROCESS_BUTTON_TEXT_SHUKKA = "[4]出荷入力";

        // VUNGUYEN 20150703 START
        /// <summary>
        /// サブファンクションボタンのテキスト 「[4]受入／出荷入力」
        /// </summary>
        public static readonly string PROCESS_BUTTON_TEXT_UKEIRE_SHUKKA = "[4]受入／出荷入力";

        /// <summary>
        /// 収集データ
        /// </summary>
        public static readonly string DATA_SYUUSYUU = "SS";

        /// <summary>
        /// 出荷データ
        /// </summary>
        public static readonly string DATA_SYUKKA = "SK";

        /// <summary>
        /// 持込データ
        /// </summary>
        public static readonly string DATA_MOCHIKOMI = "MK";
       
        /// <summary>
        /// 配車状況チェックしない
        /// </summary>
        public static readonly string NOT_CHECK_HAISHA_JYOUKYOU_CD = "-1";
        // VUNGUYEN 20150703 END

        public static readonly string CELL_CHECKBOX = "CHECKBOX";

        /// <summary>
        /// mapbox連携用項目
        /// </summary>
        public static readonly string GYOUSHA_CD = "GYOUSHA_CD";
        public static readonly string GYOUSHA_NAME_RYAKU = "GYOUSHA_NAME_RYAKU";
        public static readonly string GENBA_CD = "GENBA_CD";
        public static readonly string GENBA_NAME_RYAKU = "GENBA_NAME_RYAKU";
        public static readonly string ADDRESS1 = "ADDRESS1";
        public static readonly string ADDRESS2 = "ADDRESS2";
        public static readonly string POST = "POST";
        public static readonly string TEL = "TEL";
        public static readonly string BIKOU1 = "BIKOU1";
        public static readonly string BIKOU2 = "BIKOU2";
        public static readonly string LATITUDE = "LATITUDE";
        public static readonly string LONGITUDE = "LONGITUDE";
        public static readonly string TODOUFUKEN_NAME = "TODOUFUKEN_NAME";

        public static readonly string DENSHU_KBN = "DENSHU_KBN";
        public static readonly string SAGYOU_DATE = "SAGYOU_DATE";
        public static readonly string GENCHAKU_TIME_NAME = "GENCHAKU_TIME_NAME";
        public static readonly string GENCHAKU_TIME = "GENCHAKU_TIME";
        public static readonly string SHASHU_NAME = "SHASHU_NAME";
        public static readonly string SHARYOU_NAME = "SHARYOU_NAME";
        public static readonly string UNTENSHA_NAME = "UNTENSHA_NAME";
        public static readonly string UNTENSHA_SIJIJIKOU1 = "UNTENSHA_SIJIJIKOU1";
        public static readonly string UNTENSHA_SIJIJIKOU2 = "UNTENSHA_SIJIJIKOU2";
        public static readonly string UNTENSHA_SIJIJIKOU3 = "UNTENSHA_SIJIJIKOU3";


        public static readonly string DATA_TAISHO = "DATA_TAISHO";
    }
}
