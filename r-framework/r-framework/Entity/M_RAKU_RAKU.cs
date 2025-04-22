using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_RAKU_RAKU : SuperEntity
    {
        public SqlInt64 RAKU_ID { get; set; }
        public SqlInt16 RAKURAKU_MEISAI_KBN { get; set; }
        public string RAKURAKU_CUSTOMER_CD { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public SqlInt16 SHOSHIKI_KBN { get; set; }
        public string SEIKYUU_SHO_SOUFU_SAKI { get; set; }
        public string SOUFU_SAKI_BUSHO { get; set; }
        public string SOUFU_SAKI_TANTOUSHA { get; set; }
        public string KEISHOU { get; set; }
        public string SOUFU_SAKI_POST { get; set; }
        public string SOUFU_SAKI_ADDRESS1 { get; set; }
        public string SOUFU_SAKI_ADDRESS2 { get; set; }
        public string EMAIL { get; set; }
        public string EMAIL_ADDRESS1 { get; set; }
        public string EMAIL_ADDRESS2 { get; set; }
        public string EMAIL_ADDRESS3 { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}