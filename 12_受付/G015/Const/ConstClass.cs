// $Id: ConstClass.cs 17994 2014-03-26 01:08:11Z ogawa@takumi-sys.co.jp $

using System.Data.SqlTypes;
namespace Shougun.Core.Reception.UketsukeSyuusyuuNyuuryoku
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstClass
    {
        /// <summary>
        /// ボタン設定ファイル
        /// </summary>
        public const string BUTTON_SETTING_XML = "Shougun.Core.Reception.UketsukeSyuusyuuNyuuryoku.Setting.ButtonSetting.xml";

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
        /// 配車種類CD「1:通常」
        /// </summary>
        public static readonly string HAISHA_SHURUI_CD_TSUJO = "1";

        /// <summary>
        /// 配車種類CD「2:仮押」
        /// </summary>
        public static readonly string HAISHA_SHURUI_CD_KARIOSAE = "2";

        /// <summary>
        /// 配車種類CD「3:確定」
        /// </summary>
        public static readonly string HAISHA_SHURUI_CD_KAKUTEI = "3";

        /// <summary>
        /// 配車種類「1:通常」
        /// </summary>
        public static readonly string HAISHA_SHURUI_NAME_TSUJO = "通常";

        /// <summary>
        /// 配車種類「2:仮押」
        /// </summary>
        public static readonly string HAISHA_SHURUI_NAME_KARIOSAE = "仮押";

        /// <summary>
        /// 配車種類「3:確定」
        /// </summary>
        public static readonly string HAISHA_SHURUI_NAME_KAKUTEI = "確定";

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

        #region CongBinh 20210713 #152801
        internal const string CELL_NAME_RIREKE_SS = "CELL_RIREKE_SS";
        internal const string CELL_NAME_RIREKE_SHITA1 = "CELL_RIREKE_SHITA1";
        internal const string CELL_NAME_RIREKE_SHITA2 = "CELL_RIREKE_SHITA2";
        internal const string CELL_NAME_RIREKE_SHITA3 = "CELL_RIREKE_SHITA3";
        internal const string CELL_NAME_RIREKE_SHITA4 = "CELL_RIREKE_SHITA4";
        internal const string CELL_NAME_RIREKI_KBN = "CELL_RIREKI_KBN";
        #endregion

        #region ｼｮｰﾄﾒｯｾｰｼﾞオプション用

        /// <summary>
        /// 伝票種類「1.収集」
        /// </summary>
        public static readonly string DENPYOU_SHURUI_SS = "1";

        #endregion
    }
}
