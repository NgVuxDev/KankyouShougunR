using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_MANIFEST_SHURUI : SuperEntity
    {
        public SqlInt16 MANIFEST_SHURUI_CD { get; set; }
        public string MANIFEST_SHURUI_NAME { get; set; }
        public string MANIFEST_SHURUI_NAME_RYAKU { get; set; }
        public string MANIFEST_SHURUI_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}