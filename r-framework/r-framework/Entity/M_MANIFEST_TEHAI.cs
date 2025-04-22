using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_MANIFEST_TEHAI : SuperEntity
    {
        public SqlInt16 MANIFEST_TEHAI_CD { get; set; }
        public string MANIFEST_TEHAI_NAME { get; set; }
        public string MANIFEST_TEHAI_NAME_RYAKU { get; set; }
        public string MANIFEST_TEHAI_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}