using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace Shougun.Core.Common.BusinessCommon.Dto
{

    public class SyuuryouBiKeikokuDTOClass
    {
        /// <summary>
        /// 検索条件 : MANIFEST_UNPAN_DAYS
        /// </summary>
        public SqlInt16 Mani_UNPAN_DAYS { get; set; }
        /// <summary>
        /// 検索条件 : MANIFEST_SBN_DAYS
        /// </summary>
        public SqlInt16 Mani_SBN_DAYS { get; set; }
        /// <summary>
        /// 検索条件 : MANIFEST_TOK_SBN_DAYS
        /// </summary>
        public SqlInt16 Mani_TOK_SBN_DAYS { get; set; }
        /// <summary>
        /// 検索条件 : MANIFEST_LAST_SBN_DAYS
        /// </summary>
        public SqlInt16 Mani_LAST_SBN_DAYS { get; set; }
    }
}
