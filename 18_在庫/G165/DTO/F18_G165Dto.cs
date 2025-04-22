// $Id: F18_G165Dto.cs 11341 2013-12-16 12:04:22Z sys_dev_18 $

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;
using r_framework.Const;

namespace Shougun.Core.Stock.ZaikoMeisaiNyuuryoku.DTO
{
    public class F18_G165Dto
    {
        ///// <summary>
        ///// 検索条件 : 伝種区分CD
        ///// </summary>
        //public DENSHU_KBN DenshuKbnCd { get; set; }
        /// <summary>
        /// 検索条件 : 品名CD
        /// </summary>
        public String HinmeiCd { get; set; }
        /// <summary>
        /// 検索条件 : 在庫品名CD
        /// </summary>
        public String ZaikoHinmeiCd { get; set; }
    }

    public class ZaikoDetailDto
    {
        public SqlDecimal JYUURYOU { get; set; }
        public SqlDecimal KINGAKU { get; set; }
        public SqlDecimal TANKA { get; set; }
        public string ZAIKO_HINMEI_CD { get; set; }
        public SqlDecimal ZAIKO_RITSU { get; set; }
    }
}
