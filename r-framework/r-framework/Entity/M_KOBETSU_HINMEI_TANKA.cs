using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_KOBETSU_HINMEI_TANKA : SuperEntity
    {
        public SqlInt64 SYS_ID { get; set; }
        public SqlInt16 DENPYOU_KBN_CD { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string HINMEI_CD { get; set; }
        public SqlInt16 DENSHU_KBN_CD { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public string UNPAN_GYOUSHA_CD { get; set; }
        public string NIOROSHI_GYOUSHA_CD { get; set; }
        public string NIOROSHI_GENBA_CD { get; set; }
        public SqlDecimal TANKA { get; set; }
        public string BIKOU { get; set; }
        public SqlDateTime TEKIYOU_BEGIN { get; set; }
        public string SEARCH_TEKIYOU_BEGIN { get; set; }
        public SqlDateTime TEKIYOU_END { get; set; }
        public string SEARCH_TEKIYOU_END { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}