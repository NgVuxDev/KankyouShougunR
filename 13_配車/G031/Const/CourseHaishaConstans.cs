// $Id: CourseHaishaConstans.cs 192965 2021-07-26 04:23:45Z ueyama@e-mall.co.jp $

using System.Data.SqlTypes;
namespace Shougun.Core.Allocation.CourseHaishaIraiNyuuryoku
{
    /// <summary>
    /// コース配車依頼入力：定数クラス
    /// </summary>
    public class CourseHaishaConstans
    {
        /// <summary>
        /// 入力区分：直接入力
        /// </summary>
        public const string INPUT_KBN_CHOKUSETU = "1";

        /// <summary>
        /// 入力区分：組込
        /// </summary>
        public const string INPUT_KBN_KUMIKOMI = "2";

        /// <summary>
        /// 契約区分：単価
        /// </summary>
        public const string KEIYAKU_KBN_TANKA = "2";

        /// <summary>
        /// 計上区分：伝票
        /// </summary>
        public const string KEIJYOU_KBN_DENPYOU = "1";

        /// <summary>
        /// 単位：kg
        /// </summary>
        public const string UNIT_CD_KG = "3";

        /// <summary>mapbox連携で使用する項目</summary>
        public static readonly string ROW_NO = "ROW_NO";
        public static readonly string ROUND_NO = "ROUND_NO";
        public static readonly string COURSE_NAME = "COURSE_NAME";
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
        public static readonly string HINMEI_NAME = "HINMEI_NAME";

        public static readonly string GYOUSHA_ADDRESS1 = "GYOUSHA_ADDRESS1";
        public static readonly string GYOUSHA_ADDRESS2 = "GYOUSHA_ADDRESS2";
        public static readonly string GYOUSHA_POST = "GYOUSHA_POST";
        public static readonly string GYOUSHA_TEL = "GYOUSHA_TEL";
        public static readonly string GYOUSHA_LATITUDE = "GYOUSHA_LATITUDE";
        public static readonly string GYOUSHA_LONGITUDE = "GYOUSHA_LONGITUDE";
    }
}
