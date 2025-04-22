using System;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.MobileShougunTorikomi.DTO
{
    public class MobileShougunTorikomiDTOClass
    {
        #region - Properties -

        /// <summary>配車区分</summary>
        public int HAISHA_KBN { get; set; }

        /// <summary>業者CD</summary>
        public string GYOUSHA_CD { get; set; }

        /// <summary>車輌CD</summary>
        public string SHARYOU_CD { get; set; }

        /// <summary>車輌名</summary>
        public string SHARYOU_NAME { get; set; }

        /// <summary>車種CD</summary>
        public string SHASHU_CD { get; set; }

        /// <summary>車種名</summary>
        public string SHASHU_NAME { get; set; }

        /// <summary>社員CD</summary>
        public string SHAIN_CD { get; set; }

        /// <summary>運転者CD</summary>
        public string UNTENSHA_NAME { get; set; }

        /// <summary>コース名称CD</summary>
        public string COURSE_NAME_CD { get; set; }

        /// <summary>(配車)伝票番号</summary>
        public int HAISHA_DENPYOU_NO { get; set; }

        /// <summary>シーケンシャルナンバー</summary>
        public int SEQ_NO { get; set; }

        /// <summary>枝番</summary>
        public Int64 EDABAN { get; set; }

        /// <summary>枝番</summary>
        public Int64 NODE_EDABAN { get; set; }

        /// <summary>品名CD</summary>
        public string HINMEI_CD { get; set; }

        /// <summary>単位CD</summary>
        public SqlInt16 UNIT_CD { get; set; }

        /// <summary>現場CD</summary>
        public string GENBA_CD { get; set; }

		/// <summary>伝票区分CD</summary>
		public SqlInt16 DENPYOU_KBN_CD { get; set; }

        /// <summary>行番号</summary>
        public SqlInt16 ROW_NO { get; set; }

        /// <summary>回数</summary>
        public SqlInt32 ROUND_NO { get; set; }
        #endregion - Properties -
    }
}
