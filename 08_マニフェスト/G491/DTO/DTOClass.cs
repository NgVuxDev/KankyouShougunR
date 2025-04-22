using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.PaperManifest.ManifestPatternTouroku
{
    /// <summary>
    /// パラメータ
    /// </summary>
    public class SerchParameterDtoCls
    {
        /// <summary>検索条件  :システムID</summary>
        public SqlInt64 SYSTEM_ID { get; set; }

        /// <summary>検索条件  :seq</summary>
        public SqlInt32 SEQ { get; set; }
    }
}