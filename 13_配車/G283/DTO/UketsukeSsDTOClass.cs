using System;

namespace Shougun.Core.Allocation.MobileShougunTorikomi.DTO
{
    public class UketsukeSsDTOClass
    {
		/// <summary>
		/// システムID
		/// </summary>
		public Int64 SYSTEM_ID { get; set; }

		/// <summary>
		/// 枝番
		/// </summary>
		public Int32 SEQ { get; set; }
	
		/// <summary>
        /// 受付番号
        /// </summary>
		public Int64 UKETSUKE_NUMBER { get; set; }

        /// <summary>
        /// 行番号
        /// </summary>
        public Int16 ROW_NO { get; set; }

		/// <summary>
		/// 品名CD
		/// </summary>
		public string HINMEI_CD { get; set; }

		/// <summary>
		/// 単位CD
		/// </summary>
		public Int16 UNIT_CD { get; set; }
	}
}
