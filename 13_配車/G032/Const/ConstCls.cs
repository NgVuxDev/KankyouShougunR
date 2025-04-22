// $Id: ConstCls.cs 15945 2014-02-13 10:34:29Z y-sato $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Allocation.TeikiHaisyaIchiran
{
    class ConstCls
    {
        //拠点CD（全社）
        public const string KyouTenZenSya = "99";

        /// <summary>伝票日付Code</summary>
        public const string HidukeCD_DenPyou = "1";
        /// <summary>作業日Name</summary>
        public const string HidukeName_DenPyou = "作業日※";

        /// <summary>入力日付Code</summary>
        public const string HidukeCD_NyuuRyoku = "2";
        /// <summary>入力日付Name</summary>
        public const string HidukeName_NyuuRyoku = "入力日付※";

        /// <summary>アラート件数の最小値</summary>
        public const long AlertNumber_Min = 1;
        /// <summary>アラート件数の最大値</summary>
        public const long AlertNumber_Max = 99999;

        /// <summary>明細部の印刷列</summary>
        public const string ADD_COLUMN_INSATSU = "INSATSUFLG";
        public const string ADD_COLUMN_INSATSU_NAME = "印刷";

        /// <summary>明細部の非表示列（システムID）</summary>
        public const string HIDDEN_COLUMN_SYSTEM_ID = "SYSTEM_ID_HIDDEN";
        /// <summary>明細部の非表示列（枝番）</summary>
        public const string HIDDEN_COLUMN_SEQ = "SEQ_HIDDEN";
        /// <summary>明細部の非表示列（定期配車番号）</summary>
        public const string HIDDEN_COLUMN_HAISHA_NUMBER = "TEIKI_HAISHA_NUMBER";
        /// <summary>明細部の非表示列（作業日）</summary>
        public const string HIDDEN_COLUMN_SAGYOU_DATE = "SAGYOU_DATE_HIDDEN";
        /// <summary>明細部の非表示列（明細システムID）</summary>
        public const string HIDDEN_COLUMN_DETAIL_SYSTEM_ID = "DETAIL_SYSTEM_ID_HIDDEN";
        /// <summary>明細部の品名情報列</summary>
        public const string COLUMN_HINMEI_INFO_NAME = "品名情報";

        /// <summary>mapbox連携で使用する項目</summary>
        public static readonly string ROW_NO = "ROW_NO";
        public static readonly string ROUND_NO = "ROUND_NO";
        public static readonly string COURSE_NAME = "COURSE_NAME";
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
        public static readonly string HINMEI_NAME = "HINMEI_NAME";
        public static readonly string HIDDEN_LOCATION = "HIDDEN_LOCATION";
        
        public static readonly string DATA_TAISHO = "DATA_TAISHO";

        public static readonly string GYOUSHA_ADDRESS1 = "GYOUSHA_ADDRESS1";
        public static readonly string GYOUSHA_ADDRESS2 = "GYOUSHA_ADDRESS2";
        public static readonly string GYOUSHA_POST = "GYOUSHA_POST";
        public static readonly string GYOUSHA_TEL = "GYOUSHA_TEL";
        public static readonly string GYOUSHA_LATITUDE = "GYOUSHA_LATITUDE";
        public static readonly string GYOUSHA_LONGITUDE = "GYOUSHA_LONGITUDE";

        /// <summary>明細部の非表示列（明細行番号）</summary>
        public const string HIDDEN_COLUMN_DETAIL_ROW_NUMBER = "DETAIL_ROW_NUMBER_HIDDEN";
    }
}
