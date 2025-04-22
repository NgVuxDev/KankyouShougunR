using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace Shougun.Core.PayByProxy.DainoDenpyoHakkou.DTO
{
    /// <summary>
    /// 代納番号の明細情報保持クラス（前回）
    /// </summary>
    [Serializable()]
    internal class ResultDainoDetailZenkaiDto
    {
        /// <summary>
        ///	取引先CD
        /// </summary>		
        public string TORIHIKISAKI_CD { get; set; }

        /// <summary>
        ///	金額
        /// </summary>		
        public decimal KINGAKU { get; set; }
        /// <summary>
        ///	品名金額
        /// </summary>		
        public decimal HINMEI_KINGAKU { get; set; }
        /// <summary>
        ///	外税
        /// </summary>		
        public decimal TAX_SOTO { get; set; }
        /// <summary>
        ///	品名外税
        /// </summary>		
        public decimal HINMEI_TAX_SOTO { get; set; }
        /// <summary>
        ///	内税
        /// </summary>		
        public decimal TAX_UCHI { get; set; }
        /// <summary>
        ///	品名内税
        /// </summary>		
        public decimal HINMEI_TAX_UCHI { get; set; }

        /// <summary>
        ///	[デバッグ用]今回のシステムID
        /// </summary>		
        public SqlInt64 KONKAI_SYSTEM_ID { get; set; }
        /// <summary>
        ///	[デバッグ用]今回の枝番
        /// </summary>		
        public SqlInt32 KONKAI_SEQ { get; set; }
        /// <summary>
        ///	[デバッグ用]前回のシステムID
        /// </summary>		
        public SqlInt64 ZENKAI_SYSTEM_ID { get; set; }
        /// <summary>
        ///	[デバッグ用]前回の枝番
        /// </summary>		
        public SqlInt32 ZENKAI_SEQ { get; set; }
        /// <summary>
        ///	[デバッグ用]前回の明細システムID
        /// </summary>		
        public SqlInt64 ZENKAI_DETAIL_SYSTEM_ID { get; set; }

    }

}
