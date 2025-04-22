using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_ERROR_MESSAGE : SuperEntity
    {
        public string MESSAGE_ID { get; set; }
        public string MESSAGE { get; set; }
        public SqlInt16 MESSAGE_KUBUN { get; set; }
    }
}