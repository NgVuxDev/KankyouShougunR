namespace Shougun.Core.Master.UnchiHinmeiHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class UnchiHinmeiHoshuConstans
    {
        /// <summary>M_UNCHIN_HINMEIのUNCHIN_HINMEI_CD</summary>
        public static readonly string UNCHIN_HINMEI_CD = "UNCHIN_HINMEI_CD";

        /// <summary>M_UNCHIN_HINMEIのUNCHIN_HINMEI_NAME</summary>
        public static readonly string UNCHIN_HINMEI_NAME = "UNCHIN_HINMEI_NAME";

        /// <summary>M_UNCHIN_HINMEIのUNCHIN_HINMEI_FURIGANA</summary>
        public static readonly string UNCHIN_HINMEI_FURIGANA = "UNCHIN_HINMEI_FURIGANA";

        /// <summary>M_UNCHIN_HINMEIのUNIT_CD</summary>
        public static readonly string UNIT_CD = "UNIT_CD";

        /// <summary>M_UNITのUNIT_NAME_RYAKU</summary>
        public static readonly string UNIT_NAME_RYAKU = "UNIT_NAME_RYAKU";

        /// <summary>M_UNCHIN_HINMEIのBIKOU</summary>
        public static readonly string BIKOU = "BIKOU";

        /// <summary>M_UNCHIN_HINMEIのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>画面表示項目の削除フラグ</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>一覧の運賃品名CD"ヘッダー名</summary>
        public const string HINMEI_CD_HEADERNAME = "運賃品名CD";

        /// <summary>一覧の運賃品名ヘッダー名</summary>
        public const string HINMEI_NAME_HEADERNAME = "運賃品名";

        /// <summary>一覧のフリガナヘッダー名</summary>
        public const string HINMEI_FURIGANA_HEADERNAME = "フリガナ";

        /// <summary>一覧の単位ヘッダー名</summary>
        public const string UNIT_CD_HEADERNAME = "単位";

        /// <summary>一覧の備考ヘッダー名</summary>
        public const string BIKOU_HEADERNAM = "備考";

        /// <summary>一覧のUPDATE_USERヘッダー名</summary>
        public const string UPDATE_USER_HEADERNAME = "更新者";

        /// <summary>一覧のUPDATE_DATEヘッダー名</summary>
        public const string UPDATE_DATE_HEADERNAME = "更新日";

        /// <summary>一覧のCREATE_USERヘッダー名</summary>
        public const string CREATE_USER_HEADERNAME = "作成者";

        /// <summary>一覧のCREATE_DATEヘッダー名</summary>
        public const string CREATE_DATE_HEADERNAME = "作成日";

        /// <summary>CDのMaxLength</summary>
        public static string CD_MAXLENGTH;

        public enum FocusSwitch
        {
            NONE,
            IN,
            OUT
        }
    }
}