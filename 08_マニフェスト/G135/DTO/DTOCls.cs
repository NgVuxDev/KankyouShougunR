// $Id: DTOCls.cs 21477 2014-05-27 01:55:33Z takeda $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.PaperManifest.JissekiHokokuIchiran
{
    public class DTOCls
    {

        /// <summary>
        /// 実績報告書種類
        /// </summary>
        public SqlInt16 REPORT_KBN { get; set; }

        /// <summary>
        /// 年度種類
        /// </summary>
        public SqlInt16 YEAR_KBN { get; set; }

        /// <summary>
        /// 報告年度
        /// </summary>
        public string HOKOKU_NENDO { get; set; }
    }
}
