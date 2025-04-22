// $Id: ConstCls.cs 7096 2013-11-15 03:52:43Z sys_dev_26 $

namespace Shougun.Core.Master.CourseNyuryoku.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>M_CONTENA_JOUKYOUのCONTENA_JOUKYOU_CD</summary>
        public static readonly string CONTENA_JOUKYOU_CD = "CONTENA_JOUKYOU_CD";

        /// <summary>M_CONTENA_JOUKYOUのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_CONTENA_JOUKYOUのDELETE_FLGフラグ</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>mapbox連携で使用する項目</summary>
        public static readonly string GYOUSHA_CD = "GYOUSHA_CD";
        public static readonly string GYOUSHA_NAME_RYAKU = "GYOUSHA_NAME_RYAKU";
        public static readonly string GENBA_CD = "GENBA_CD";
        public static readonly string GENBA_NAME_RYAKU = "GENBA_NAME_RYAKU";
        public static readonly string GENBA_ADDRESS1 = "GENBA_ADDRESS1";
        public static readonly string GENBA_ADDRESS2 = "GENBA_ADDRESS2";
        public static readonly string GENBA_POST = "GENBA_POST";
        public static readonly string GENBA_TEL = "GENBA_TEL";
        public static readonly string BIKOU1 = "BIKOU1";
        public static readonly string BIKOU2 = "BIKOU2";
        public static readonly string GENBA_LATITUDE = "GENBA_LATITUDE";
        public static readonly string GENBA_LONGITUDE = "GENBA_LONGITUDE";
        public static readonly string TODOUFUKEN_NAME = "TODOUFUKEN_NAME";

        public static readonly string GYOUSHA_ADDRESS1 = "GYOUSHA_ADDRESS1";
        public static readonly string GYOUSHA_ADDRESS2 = "GYOUSHA_ADDRESS2";
        public static readonly string GYOUSHA_POST = "GYOUSHA_POST";
        public static readonly string GYOUSHA_TEL = "GYOUSHA_TEL";
        public static readonly string GYOUSHA_LATITUDE = "GYOUSHA_LATITUDE";
        public static readonly string GYOUSHA_LONGITUDE = "GYOUSHA_LONGITUDE";

        /// <summary>
        /// 回収明細部（定期配車明細）のカラム名
        /// </summary>
        public class DetailColName
        {
            /// <summary>No</summary>
            public const string NO = "ROW_NO";
            /// <summary>順番</summary>
            public const string JUNNBANN = "ROW_NO2";
            /// <summary>回数</summary>
            public const string ROUND_NO = "ROUND_NO";
            /// <summary>業者CD</summary>
            public const string GYOUSHA_CD = "GYOUSHA_CD";
            /// <summary>業者名</summary>
            public const string GYOUSHA_NAME_RYAKU = "GYOUSHA_NAME_RYAKU";
            /// <summary>現場CD</summary>
            public const string GENBA_CD = "GENBA_CD";
            /// <summary>現場名</summary>
            public const string GENBA_NAME_RYAKU = "GENBA_NAME_RYAKU";
            /// <summary>詳細ボタン</summary>
            public const string DETAIL_BUTTON = "DETAIL_BUTTON";
            /// <summary>品名情報</summary>
            public const string KAISYUUHIN_NAME = "KAISYUUHIN_NAME";
            /// <summary>明細備考</summary>
            public const string BIKOU = "BIKOU";
            /// <summary>明細システムID</summary>
            public const string DETAIL_SYSTEM_ID = "DETAIL_SYSTEM_ID";
            /// <summary>レコードID</summary>
            public const string REC_ID = "REC_ID";
        }

        /// <summary>
        /// 荷降明細部（定期配車荷降）のカラム名
        /// </summary>
        public class NioroshiColName
        {
            /// <summary>荷降No</summary>
            public const string NIOROSHI_NUMBER = "M_COURSE_NIOROSHI_NIOROSHI_NO";
            /// <summary>荷降業者CD</summary>
            public const string NIOROSHI_GYOUSHA_CD = "M_COURSE_NIOROSHI_GYOUSHA_CD";
            /// <summary>荷降業者名</summary>
            public const string NIOROSHI_GYOUSHA_NAME_RYAKU = "M_COURSE_NIOROSHI_GYOUSHA_NAME_RYAKU";
            /// <summary>荷降現場CD</summary>
            public const string NIOROSHI_GENBA_CD = "M_COURSE_NIOROSHI_GENBA_CD";
            /// <summary>荷降現場名</summary>
            public const string NIOROSHI_GENBA_NAME_RYAKU = "M_COURSE_NIOROSHI_GENBA_NAME_RYAKU";
        }

        /// <summary>
        /// 定期配車詳細（品名情報）のカラム名
        /// </summary>
        public class ShousaiColName
        {
            /// <summary>削除フラグ</summary>
            public const string DELETE_FLG = "DELETE_FLG";
            /// <summary>レコードID/明細システムID</summary>
            public const string REC_ID = "REC_ID";
            /// <summary>レコードSEQ/行番号</summary>
            public const string REC_SEQ = "REC_SEQ";
            /// <summary>品名CD</summary>
            public const string HINMEI_CD = "HINMEI_CD";
            /// <summary>品名名</summary>
            public const string HINMEI_NAME_RYAKU = "HINMEI_NAME_RYAKU";
            /// <summary>単位CD</summary>
            public const string UNIT_CD = "UNIT_CD";
            /// <summary>単位名</summary>
            public const string UNIT_NAME = "UNIT_NAME";
            /// <summary>換算値</summary>
            public const string KANSANCHI = "KANSANCHI";
            /// <summary>換算後単位CD</summary>
            public const string KANSAN_UNIT_CD = "KANSAN_UNIT_CD";

            /// <summary>契約区分</summary>
            public const string KEIYAKU_KBN = "KEIYAKU_KBN";
            /// <summary>計上区分</summary>
            public const string KEIJYOU_KBN = "KEIJYOU_KBN";
            /// <summary>伝票区分CD</summary>
            public const string DENPYOU_KBN_CD = "DENPYOU_KBN_CD";
            /// <summary>換算後単位モバイル出力フラグ</summary>
            public const string KANSAN_UNIT_MOBILE_OUTPUT_FLG = "KANSAN_UNIT_MOBILE_OUTPUT_FLG";

            /// <summary>入力区分</summary>
            public const string INPUT_KBN = "INPUT_KBN";
            /// <summary>荷降No</summary>
            public const string NIOROSHI_NUMBER = "NIOROSHI_NUMBER";
            /// <summary>実数</summary>
            public const string ANBUN_FLG = "ANBUN_FLG";

        }
    }

}
