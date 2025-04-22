using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class S_DENSHI_CONNECT : SuperEntity
    {
        public SqlInt32 ID { get; set; }
        public string CONTENT_NAME { get; set; }
        public string URL { get; set; }
        public string CONTENT_TYPE { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
