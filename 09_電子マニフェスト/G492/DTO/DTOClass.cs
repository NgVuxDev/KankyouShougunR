using System;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.ElectronicManifest.DenshiManifestPatternTouroku.Dto
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