using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace ShukkinDataShutsuryoku
{
    public class DTOClass
    {
        public string BANK_CD { get; set; }
        public string BANK_SHITEN_CD { get; set; }
        public string KOUZA_SHURUI { get; set; }
        public string KOUZA_NO { get; set; }
        public SqlDateTime FURIKOMI_DATE { get; set; }
        public SqlInt16 SHUTSURYOKU_JOUKYOU { get; set; }
        public string SHUTSURYOKU_SAKI { get; set; }
    }
}
