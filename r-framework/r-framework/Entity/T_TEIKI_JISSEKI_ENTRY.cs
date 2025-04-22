using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_TEIKI_JISSEKI_ENTRY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public SqlInt64 TEIKI_JISSEKI_NUMBER { get; set; }
        public string WEATHER { get; set; }
        public SqlDateTime DENPYOU_DATE { get; set; }
        public string SEARCH_DENPYOU_DATE { get; set; }
        public SqlDateTime SAGYOU_DATE { get; set; }
        public string SEARCH_SAGYOU_DATE { get; set; }
        public string COURSE_NAME_CD { get; set; }
        public string SHARYOU_CD { get; set; }
        public string SHASHU_CD { get; set; }
        public string UNTENSHA_CD { get; set; }
        public string UNPAN_GYOUSHA_CD { get; set; }
        public string HOJOIN_CD { get; set; }
        public SqlDecimal SHUKKO_METER { get; set; }
        public SqlInt16 SHUKKO_HOUR { get; set; }
        public SqlInt16 SHUKKO_MINUTE { get; set; }
        public SqlDecimal KIKO_METER { get; set; }
        public SqlInt16 KIKO_HOUR { get; set; }
        public SqlInt16 KIKO_MINUTE { get; set; }
        public SqlInt64 TEIKI_HAISHA_NUMBER { get; set; }
        public string MOBILE_SHOGUN_FILE_NAME { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
        public SqlInt16 FURIKAE_HAISHA_KBN { get; set; }
        public SqlInt16 DAY_CD { get; set; }
    }
}