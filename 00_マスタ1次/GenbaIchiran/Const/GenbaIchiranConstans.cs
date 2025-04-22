// $Id: GenbaIchiranConstans.cs 17036 2014-03-06 12:44:54Z y-sato $

namespace GenbaIchiran.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class GenbaIchiranConstans
    {
        /// <summary>全社の拠点CD</summary>
        public static readonly string KYOTEN_CD_ALL = "99";

        /// <summary>画面連携に使用するキー取得項目名1（業者CD）</summary>
        public static readonly string KEY_ID1 = "KEY_GYOUSHA_CD";

        /// <summary>画面連携に使用するキー取得項目名2（現場CD）</summary>
        public static readonly string KEY_ID2 = "KEY_GENBA_CD";

        /// <summary>mapbox連携で使用する項目/// </summary>
        public static readonly string HIDDEN_GENBA_LATITUDE = "HIDDEN_GENBA_LATITUDE";
        public static readonly string HIDDEN_GENBA_LONGITUDE = "HIDDEN_GENBA_LONGITUDE";
        public static readonly string HIDDEN_GYOUSHA_NAME_RYAKU = "HIDDEN_GYOUSHA_NAME_RYAKU";
        public static readonly string HIDDEN_GENBA_NAME_RYAKU = "HIDDEN_GENBA_NAME_RYAKU";
        public static readonly string HIDDEN_GENBA_ADDRESS1 = "HIDDEN_GENBA_ADDRESS1";
        public static readonly string HIDDEN_GENBA_ADDRESS2 = "HIDDEN_GENBA_ADDRESS2";
        public static readonly string HIDDEN_GENBA_POST = "HIDDEN_GENBA_POST";
        public static readonly string HIDDEN_GENBA_TEL = "HIDDEN_GENBA_TEL";
        public static readonly string HIDDEN_BIKOU1 = "HIDDEN_BIKOU1";
        public static readonly string HIDDEN_BIKOU2 = "HIDDEN_BIKOU2";
        public static readonly string HIDDEN_TODOUFUKEN_NAME = "HIDDEN_TODOUFUKEN_NAME";

        // チェックボックス
        public static readonly string DATA_TAISHO = "DATA_TAISHO";

        //// ｼｮｰﾄﾒｯｾｰｼﾞオプション利用フラグ
        //public static readonly string HIDDEN_SMS_USE = "HIDDEN_SMS_USE";
    }
}
