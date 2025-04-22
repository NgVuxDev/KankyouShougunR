// $Id: DTOClass.cs 15793 2014-02-10 04:08:44Z nagata $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Common.NioroshiNoSettei
{
    /// <summary>
    /// 荷降のカラム名
    /// </summary>
    public class NioroshiDto
    {
        public string NIOROSHI_NUMBER { get; set; }
        public string NIOROSHI_GYOUSHA_CD { get; set; }
        public string NIOROSHI_GYOUSHA_NAME { get; set; }
        public string NIOROSHI_GENBA_CD { get; set; }
        public string NIOROSHI_GENBA_NAME { get; set; }
    }

    /// <summary>
    /// 明細のカラム名
    /// </summary>
    public class DetailDto
    {
        public int ROW_NO { get; set; }
        public string TABLE_NAME { get; set; }
        public int TABLE_ROW_NO { get; set; }
        public string ROUND_NO { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GYOUSHA_NAME { get; set; }
        public string GENBA_CD { get; set; }
        public string GENBA_NAME { get; set; }
        public string HINMEI_CD { get; set; }
        public string HINMEI_NAME { get; set; }
        public string UNIT_CD { get; set; }
        public string UNIT_NAME { get; set; }
        public string NIOROSHI_NUMBER_DETAIL { get; set; }
        public bool isRenkei { get; set; }
    }
}
