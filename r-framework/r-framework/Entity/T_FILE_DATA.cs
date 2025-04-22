using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_FILE_DATA : SuperEntity
    {
        public SqlInt64 FILE_ID { get; set; }
        public SqlBinary BINARY_DATA { get; set; }
        public string FILE_PATH { get; set; }
        public string FILE_EXTENTION { get; set; }
        public SqlInt64 FILE_LENGTH { get; set; }
        public SqlDateTime FILE_CREATION_TIME { get; set; }
        public SqlDateTime FILE_LAST_WRITE_TIME { get; set; }
        public SqlBoolean IS_READ_ONLY { get; set; }
        public string WINDOW_NAME { get; set; }
    }
}
