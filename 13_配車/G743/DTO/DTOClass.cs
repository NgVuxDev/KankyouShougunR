using System;

namespace Shougun.Core.Allocation.CarTransferSpot
{
    public class DTOClass
    {
        /// <summary>
        /// 検索条件 : 定期配車番号
        /// </summary>
        public String UketsukeNumber { get; set; }
        /// <summary>
        /// 検索条件  :SYSTEM_ID
        /// </summary>
        public long SystemId { get; set; }
        /// <summary>
        /// 検索条件 : SEQ
        /// </summary>
        public int Seq { get; set; }
        /// <summary>
        /// 検索条件 : 明細システムID
        /// </summary>
        public long DetailSystemId { get; set; }

        /// <summary>
        /// モバイル連携用：作業日FROM
        /// </summary>
        public string SAGYOU_DATE_FROM { get; set; }

        /// <summary>
        /// モバイル連携用：作業日TO
        /// </summary>
        public string SAGYOU_DATE_TO { get; set; }

        /// <summary>
        /// モバイル連携用：DETAIL_SYSTEM_ID
        /// </summary>
        public string DETAIL_SYSTEM_ID { get; set; }
    }
}
