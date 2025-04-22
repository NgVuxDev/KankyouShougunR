using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_INXS_TANTOUSHA : SuperEntity
    {
        public string SHAIN_CD { get; set; }
        public string INXS_TANTOUSHA_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
