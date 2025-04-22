using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_CONTENA : SuperEntity
    {
        public string CONTENA_SHURUI_CD { get; set; }
        public string CONTENA_CD { get; set; }
        public string CONTENA_NAME { get; set; }
        public string CONTENA_NAME_RYAKU { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public string SHARYOU_CD { get; set; }
        public SqlDateTime SECCHI_DATE { get; set; }
        public SqlDateTime HIKIAGE_DATE { get; set; }
        public string SEARCH_SECCHI_DATE { get; set; }
        public SqlInt16 JOUKYOU_KBN { get; set; }
        public SqlDateTime KOUNYUU_DATE { get; set; }
        public string SEARCH_KOUNYUU_DATE { get; set; }
        public SqlDateTime LAST_SHUUFUKU_DATE { get; set; }
        public string SEARCH_LAST_SHUUFUKU_DATE { get; set; }
        public string CONTENA_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}