using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_GURUPU_NYURYOKU : SuperEntity
    {
        public string GURUPU_CD { get; set; }
        public string GURUPU_NAME { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }

        //20250318
        public SqlInt16 DENPYOU_KBN_CD { get; set; }

        //20250324
        public SqlInt16 GURUPU_ID { get; set; }
    }
}