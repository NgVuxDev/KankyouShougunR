// $Id: ConstClass.cs 17033 2014-03-06 11:53:08Z y-hosokawa@takumi-sys.co.jp $

using System.Data.SqlTypes;
namespace Shougun.Core.Reception.UketsukeMochikomiNyuuryoku
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstClass
    {

        /// <summary>
        /// ボタン設定ファイル
        /// </summary>
        public const string BUTTON_SETTING_XML = "Shougun.Core.Reception.UketsukeMochikomiNyuuryoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// SQLファイル
        /// </summary>
        public const string GET_ICHIRAN_DATA_SQL = "Shougun.Core.Reception.UketsukeMochikomiNyuuryoku.Sql.GetIchiranDataSql.sql";

        /// <summary>
        /// 受付持込一覧のカラム名
        /// </summary>
        public class ColName
        {
            public const string SALES_ZAIKO_HINMEI_CD = "SALES_ZAIKO_HINMEI_CD";
            public const string SALES_ZAIKO_HINMEI = "SALES_ZAIKO_HINMEI";
            public const string SALES_ZAIKO_HINMEI_RYAKU = "SALES_ZAIKO_HINMEI_RYAKU";
            public const string SALES_ZAIKO_HINMEI_FURIGANA = "SALES_ZAIKO_HINMEI_FURIGANA";
            public const string UNIT_CD = "UNIT_CD";
            public const string UNIT_NAME_RYAKU = "UNIT_NAME_RYAKU";
            public const string VOLUME = "VOLUME";
            public const string SALES_ZAIKO_BIKOU = "SALES_ZAIKO_BIKOU";
            public const string TEKIYOU_BEGIN = "TEKIYOU_BEGIN";
            public const string TEKIYOU_END = "TEKIYOU_END";
            public const string CREATE_USER = "CREATE_USER";
            public const string CREATE_DATE = "CREATE_DATE";
            public const string CREATE_PC = "CREATE_PC";
            public const string UPDATE_USER = "UPDATE_USER";
            public const string UPDATE_DATE = "UPDATE_DATE";
            public const string UPDATE_PC = "UPDATE_PC";
            public const string DELETE_FLG = "DELETE_FLG";
            public const string TIME_STAMP = "TIME_STAMP";
        }

        /// <summary>
        /// ヒント
        /// </summary>
        public class Hint
        {
            //public const string TORIHIKISAKI_NAME = "全角２０桁以内で入力してください";
            //public const string GYOUSHA_NAME = "全角２０桁以内で入力してください";
            //public const string GYOSHA_TEL = "半角１３桁以内で入力してください";
            //public const string GENBA_NAME = "全角２０桁以内で入力してください";
            //public const string GENBA_TEL = "半角１３桁以内で入力してください";
            //public const string TANTOSHA_NAME = "全角８桁以内で入力してください";
            //public const string TANTOSHA_TEL = "半角１３桁以内で入力してください";
            //public const string UNPAN_GYOUSHA_NAME = "全角２０桁以内で入力してください";
            //public const string NIOROSHI_GYOUSHA_NAME = "全角２０桁以内で入力してください";
            //public const string NIOROSHI_GENBA_NAME = "全角２０桁以内で入力してください";

            public const string ZENKAKU = "全角{0}桁以内で入力してください";
            public const string HANKAKU = "半角{0}桁以内で入力してください";
        }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public class ExceptionErrMsg
        {
            public const string HAITA = "排他エラーが発生しました。";
            public const string REIGAI = "例外エラーが発生しました。";
        }

        /// <summary>
        /// 伝票区分CD(売上)文字列
        /// </summary>
        public const string DENPYOU_KBN_CD_URIAGE_STR = "1";

        /// <summary>
        /// 伝票区分CD(支払)文字列
        /// </summary>
        public const string DENPYOU_KBN_CD_SHIHARAI_STR = "2";

        /// <summary>
        /// 伝票区分CD(共通)文字列
        /// </summary>
        public const string DENPYOU_KBN_CD_KYOUTU_STR = "3";

        /// <summary>
        /// 伝種区分CD(受入)
        /// </summary>
        public static SqlInt16 DENSHU_KBN_CD_UKEIRE = 1;

        /// <summary>
        /// 伝種区分CD(出荷)
        /// </summary>
        public static SqlInt16 DENSHU_KBN_CD_SHUKKA = 2;

        /// <summary>
        /// 伝種区分CD(売上・支払)
        /// </summary>
        public static SqlInt16 DENSHU_KBN_CD_UR_SH = 3;

        /// <summary>
        /// 伝種区分CD(共通)
        /// </summary>
        public static SqlInt16 DENSHU_KBN_CD_KYOTU = 9;

        #region CongBinh 20210713 #152804
        internal const string CELL_NAME_RIREKE_SS = "CELL_RIREKE_SS";
        internal const string CELL_NAME_RIREKE_SHITA1 = "CELL_RIREKE_SHITA1";
        internal const string CELL_NAME_RIREKE_SHITA2 = "CELL_RIREKE_SHITA2";
        internal const string CELL_NAME_RIREKE_SHITA3 = "CELL_RIREKE_SHITA3";
        internal const string CELL_NAME_RIREKE_SHITA4 = "CELL_RIREKE_SHITA4";
        internal const string CELL_NAME_RIREKI_KBN = "CELL_RIREKI_KBN";
        #endregion

        /// <summary>
        /// 予約状況選択ポップアップのウィンドウタイトル
        /// </summary>
        public static readonly string POPUP_TITLE_YOYAKU_JOKYO = "予約状況選択";

        /// <summary>
        /// 予約状況選択ポップアップのカラム名（予約状況CD）
        /// </summary>
        public static readonly string COLUMN_YOYAKU_JOKYO_CD = "YOYAKU_JOKYO_CD";

        /// <summary>
        /// 予約状況選択ポップアップのカラム名（予約状況）
        /// </summary>
        public static readonly string COLUMN_YOYAKU_JOKYO_NAME = "YOYAKU_JOKYO_NAME";

        /// <summary>
        /// 予約状況選択ポップアップのカラムヘッダ名（予約状況CD）
        /// </summary>
        public static readonly string HEADER_YOYAKU_JOKYO_CD = "予約状況CD";

        /// <summary>
        /// 予約状況選択ポップアップのカラムヘッダ名（予約状況名）
        /// </summary>
        public static readonly string HEADER_YOYAKU_JOKYO_NAME = "予約状況名";

        /// <summary>
        /// 予約状況CD「1:予約完了」
        /// </summary>
        public static readonly string YOYAKU_JOKYO_CD_1 = "1";

        /// <summary>
        /// 予約状況CD「2:保留」
        /// </summary>
        public static readonly string YOYAKU_JOKYO_CD_2 = "2";

        /// <summary>
        /// 予約状況CD「3:計上」
        /// </summary>
        public static readonly string YOYAKU_JOKYO_CD_3 = "3";

        /// <summary>
        /// 予約状況CD「4:キャンセル」
        /// </summary>
        public static readonly string YOYAKU_JOKYO_CD_4 = "4";

        /// <summary>
        /// 予約状況CD「5:持込なし」
        /// </summary>
        public static readonly string YOYAKU_JOKYO_CD_5 = "5";

        /// <summary>
        /// 予約状況「1:予約完了」
        /// </summary>
        public static readonly string YOYAKU_JOKYO_NAME_1 = "予約完了";

        /// <summary>
        /// 予約状況「2:保留」
        /// </summary>
        public static readonly string YOYAKU_JOKYO_NAME_2 = "保留";

        /// <summary>
        /// 予約状況「3:計上」
        /// </summary>
        public static readonly string YOYAKU_JOKYO_NAME_3 = "計上";

        /// <summary>
        /// 予約状況「4:キャンセル」
        /// </summary>
        public static readonly string YOYAKU_JOKYO_NAME_4 = "キャンセル";

        /// <summary>
        /// 予約状況「5:持込なし」
        /// </summary>
        public static readonly string YOYAKU_JOKYO_NAME_5 = "持込なし";

        #region ｼｮｰﾄﾒｯｾｰｼﾞオプション用

        /// <summary>
        /// 伝票種類「3.持込」
        /// </summary>
        public static readonly string DENPYOU_SHURUI_MK = "3";

        #endregion
    }
}
