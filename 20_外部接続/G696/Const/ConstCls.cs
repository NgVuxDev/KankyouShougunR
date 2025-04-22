using System.Data.SqlTypes;

namespace Shougun.Core.ExternalConnection.HaisouKeikakuIchiran.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>画面連携に使用するキー取得項目名1（システムID）</summary>
        public static readonly string KEY_ID = "SYSTEM_ID";

        /// <summary></summary>
        public static readonly string CELL_TARGET = "TARGET";
        /// <summary></summary>
        public static readonly string CELL_SHARYOU_CD = "SHARYOU_CD";
        /// <summary></summary>
        public static readonly string CELL_UPN_GYOUSHA_CD = "UPN_GYOUSHA_CD";
        /// <summary></summary>
        public static readonly string CELL_DELIVERY_DATE = "DELIVERY_DATE";
        /// <summary></summary>
        public static readonly string CELL_DELIVERY_NO = "DELIVERY_NO";

        /// <summary>配車状況:3</summary>
        public static readonly SqlInt16 HAISHA_JOKYO_CD_3 = 3;
        /// <summary>配車状況名:3_計上</summary>
        public static readonly string HAISHA_JOKYO_NAME_3 = "計上";
        /// <summary>配車状況:5</summary>
        public static readonly SqlInt16 HAISHA_JOKYO_CD_5 = 5;
        /// <summary>配車状況名:5_回収なし</summary>
        public static readonly string HAISHA_JOKYO_NAME_5 = "回収なし";

        /// <summary>入力区分_1_直接入力</summary>
        public static readonly SqlInt16 INPUT_KBN_1 = 1;
        /// <summary>入力区分_2_組込</summary>
        public static readonly SqlInt16 INPUT_KBN_2 = 2;

        /// <summary>連携状態：3.受信済(完了)</summary>
        public static readonly SqlInt16 LINK_STATUS_3 = 3;

        /// <summary>配送パスフラグ_1_パス</summary>
        public static readonly string DELIVERY_PASS_FLAG_PASS = "1";

        /// <summary>ロジこんぱす（事務側）で追加された場合の配送明細No初期値</summary>
        public static readonly int ADD_EXTERNAL_DETAIL_NO = 1001;

        #region T_LOGI_DELIVERY_DETAIL.DENPYOU_ATTRの判定
        /// <summary>収集受付</summary>
        public static readonly int DENPYOU_ATTR_SS = 1;
        /// <summary>出荷受付</summary>
        public static readonly int DENPYOU_ATTR_SK = 2;
        /// <summary>定期</summary>
        public static readonly int DENPYOU_ATTR_TEIKI = 3;
        #endregion
    }
}
