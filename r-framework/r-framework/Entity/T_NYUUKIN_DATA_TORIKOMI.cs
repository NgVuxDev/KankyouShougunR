using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_NYUUKIN_DATA_TORIKOMI : SuperEntity
    {
        public SqlInt64 TORIKOMI_NUMBER { get; set; }
        public SqlInt32 ROW_NUMBER { get; set; }
        public string BANK_RENKEI_CD { get; set; }
        public string BANK_SHITEN_RENKEI_CD { get; set; }
        public string KOUZA_NO { get; set; }
        public SqlDateTime YONYUU_DATE { get; set; }
        public SqlDecimal KINGAKU { get; set; }
        public string FURIKOMI_JINMEI { get; set; }
        public string TEKIYOU_NAIYOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}