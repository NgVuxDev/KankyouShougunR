using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_BIKO_UCHIWAKE_NYURYOKU : SuperEntity
    {
        public string BIKO_KBN_CD { get; set; }
        public string BIKO_CD { get; set; }
        public string BIKO_NOTE { get; set; }

        //20250417
        public string BIKO_NAME_RYAKU { get; set; }

    }
}