namespace Shougun.Core.ExternalConnection.SetchiContenaIchiran
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>全社の拠点CD</summary>
        public static readonly string KYOTEN_CD_ALL = "99";

        /// <summary>画面連携に使用するキー取得項目名1（業者CD）</summary>
        public static readonly string KEY_ID1 = "KEY_GYOUSHA_CD";

        /// <summary>画面連携に使用するキー取得項目名2（現場CD）</summary>
        public static readonly string KEY_ID2 = "KEY_GENBA_CD";

        public static readonly string CELL_CHECKBOX = "CHECKBOX";

        /// <summary>
        /// CSV
        /// </summary>
        public const string CSV_NAME = "外部連携現場一覧";

        /// <summary>
        /// 外部連携用CSVのファイル名
        /// </summary>
        public const string RENKEI_CSV_NAME = "外部連携現場";

        /// <summary>
        /// 明細の非表示列
        /// </summary>
        public static readonly string HIDDEN_POINT_ID = "POINT_ID_HIDDEN";
        public static readonly string HIDDEN_POINT_NAME = "POINT_NAME_HIDDEN";
        public static readonly string HIDDEN_POINT_KANA_NAME = "POINT_KANA_NAME_HIDDEN";
        public static readonly string HIDDEN_MAP_NAME = "MAP_NAME_HIDDEN";
        public static readonly string HIDDEN_POST_CODE = "POST_CODE_HIDDEN";
        public static readonly string HIDDEN_PREFECTURES = "PREFECTURES_HIDDEN";
        public static readonly string HIDDEN_ADDRESS1 = "ADDRESS1_HIDDEN";
        public static readonly string HIDDEN_ADDRESS2 = "ADDRESS2_HIDDEN";
        public static readonly string HIDDEN_TEL_NO = "TEL_NO_HIDDEN";
        public static readonly string HIDDEN_FAX_NO = "FAX_NO_HIDDEN";
        public static readonly string HIDDEN_CONTACT_NAME = "CONTACT_NAME_HIDDEN";
        public static readonly string HIDDEN_MAIL_ADDRESS = "MAIL_ADDRESS_HIDDEN";
        public static readonly string HIDDEN_RANGE_RADIUS = "RANGE_RADIUS_HIDDEN";
        public static readonly string HIDDEN_REMARKS = "REMARKS_HIDDEN";
    }
}
