using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace Shougun.Core.Common.BusinessCommon.Dto
{
    public class GetusjiShoriZaikoDTOClass
    {
        /// <summary>
        /// 業者CD
        /// </summary>
        public string GYOUSHA_CD { get; set; }

        /// <summary>
        /// 現場CD
        /// </summary>
        public string GENBA_CD { get; set; }

        /// <summary>
        /// 在庫品名CD
        /// </summary>
        public string ZAIKO_HINMEI_CD { get; set; }

        /// <summary>
        /// 月次処理対象開始年月日
        /// </summary>
        public SqlDateTime FROM_DATE { get; set; }

        /// <summary>
        /// 月次処理対象終了年月日
        /// </summary>
        public SqlDateTime TO_DATE { get; set; }

        /// <summary>
        /// 繰越在庫量
        /// </summary>
        public decimal PREVIOUS_MONTH_ZAIKO_RYOU { get; set; }

        /// <summary>
        /// 繰越在庫金額  
        /// </summary>
        public decimal PREVIOUS_MONTH_KINGAKU { get; set; }
    }
}
