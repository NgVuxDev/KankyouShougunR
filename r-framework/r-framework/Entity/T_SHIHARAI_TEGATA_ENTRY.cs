using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_SHIHARAI_TEGATA_ENTRY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 ROW_NUMBER { get; set; }
        public SqlInt64 SHUKKIN_NUMBER { get; set; }
        public string TEGATA_NUMBER { get; set; }
        public SqlDateTime FURIDASHI_DATE { get; set; }
        public string SEARCH_FURIDASHI_DATE { get; set; }
        public SqlDateTime TEGATA_KIJITSU { get; set; }
        public string SEARCH_TEGATA_KIJITSU { get; set; }
        public string HAKKOU_BANK { get; set; }
        public string MEIGI_NAME { get; set; }
        public string URAGAKI_NAME { get; set; }
    }
}