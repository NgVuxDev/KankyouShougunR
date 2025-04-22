using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.PaperManifest.JissekiHokokuUnpanCsv
{
    /// <summary>
    /// パラメータ
    /// </summary>
    public class SearchParameterDtoCls
    {
        /// <summary>検索条件  :システムID</summary>
        public string SYSTEM_ID { get; set; }

        /// <summary>検索条件  :枝番</summary>
        public string SEQ { get; set; }
    }
}
