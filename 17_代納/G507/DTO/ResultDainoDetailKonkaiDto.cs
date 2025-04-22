using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace Shougun.Core.PayByProxy.DainoDenpyoHakkou.DTO
{
    /// <summary>
    /// 代納番号の明細情報保持クラス（今回）
    /// </summary>
    [Serializable()]
    internal class ResultDainoDetailKonkaiDto
    {
        /// <summary>
        ///	[デバッグ用]システムID
        /// </summary>		
        public SqlInt64 SYSTEM_ID { get; set; }
        /// <summary>
        ///	[デバッグ用]枝番
        /// </summary>		
        public SqlInt32 SEQ { get; set; }
        /// <summary>
        ///	[デバッグ用]明細システムID
        /// </summary>		
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }

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
        ///	[帳票用] 正味
        /// </summary>		
        public decimal NET_JYUURYOU { get; set; }
        /// <summary>
        ///	[帳票用] 数量
        /// </summary>		
        public decimal SUURYOU { get; set; }
        /// <summary>
        ///	[帳票用] 単位名略
        /// </summary>		
        public string UNIT_NAME_RYAKU { get; set; }
        /// <summary>
        ///	[帳票用] 品名CD
        /// </summary>		
        public string HINMEI_CD { get; set; }
        /// <summary>
        ///	[帳票用] 品名
        /// </summary>		
        public string HINMEI_NAME { get; set; }
        /// <summary>
        ///	[帳票用] 単価
        /// </summary>		
        public decimal TANKA { get; set; }
        /// <summary>
        ///	[帳票用] 行番号
        /// </summary>		
        public int ROW_NO { get; set; }
        /// <summary>
        ///	[帳票用] 伝票区分CD
        /// </summary>		
        public int DENPYOU_KBN_CD { get; set; }

    }

}
