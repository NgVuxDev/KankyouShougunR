using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;
using System.Data;

namespace Shougun.Core.Carriage.UnchinDaichou
{
    /// <summary>
    /// 運賃台帳用DTO
    /// </summary>
    public class DTOClass
    {
        public string UNPAN_GYOUSHA_CD_FROM { get; set; }
        public string UNPAN_GYOUSHA_CD_TO { get; set; }
        public SqlDateTime DENPYOU_DATE_FROM { get; set; }
        public SqlDateTime DENPYOU_DATE_TO { get; set; }
    }
}
