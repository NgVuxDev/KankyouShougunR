using System;
using System.Data.SqlTypes;
using r_framework.Entity;
using System.Collections.Generic;

namespace Shougun.Core.BusinessManagement.DenpyouIkkatuPopupUpdate.DTO
{
    public class NyuuryokuParamDto
    {
        public SqlDecimal tanka { get; set; }

        public SqlDecimal kingkaku { get; set; }

        public SqlDecimal tankaZougenn { get; set; }

        public string hinmeiCd { get; set; }

        public string hinmeiName { get; set; }

        public string denpyouKbnCd { get; set; }

        public string denpyouKbnName { get; set; }

        public SqlDecimal suuryou { get; set; }

        public string unitCd { get; set; }

        public string unitName { get; set; }

        public string meisaiBikou { get; set; }

    }   
}
