using System.Data.SqlTypes;
namespace r_framework.Entity
{
    public class M_HAISHA_JOKYO : SuperEntity
    {
        public SqlInt16 SYSTEM_ID { get; set; }

        public string HAISHA_JOKYO_CD { get; set; }

        public string HAISHA_JOKYO_NAME { get; set; }


    }
}