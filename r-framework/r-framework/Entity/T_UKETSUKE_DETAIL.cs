using System.Data.SqlTypes;
namespace r_framework.Entity
{
    public class T_UKETSUKE_DETAIL : SuperEntity
    {
        public SqlInt32 UKETSUKE_NO { get; set; }

        public SqlInt16 ROW { get; set; }

        public string SHURUI_CD { get; set; }

        public string MEIGARA_CD { get; set; }

        public string MEIGARA_NAME { get; set; }

        public string SUURYOU { get; set; }

        public SqlInt16 UNIT_KBN_CD { get; set; }

        public SqlDecimal UNIT_PRICE { get; set; }

        public SqlDecimal KINGAKU { get; set; }

        public SqlDecimal SHOUHIZEI { get; set; }

        public SqlInt16 TAX_KBN_CD { get; set; }

        public string MEISAI_BIKOU { get; set; }


    }
}