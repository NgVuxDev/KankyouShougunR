using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_UNCHIN_ENTRY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public SqlInt16 DENSHU_KBN_CD { get; set; }
        public SqlInt64 RENKEI_NUMBER { get; set; }
        public SqlDateTime DENPYOU_DATE { get; set; }
        public SqlInt64 DENPYOU_NUMBER { get; set; }
        public string UNPAN_GYOUSHA_CD { get; set; }
        public string UNPAN_GYOUSHA_NAME { get; set; }
        public string SHARYOU_CD { get; set; }
        public string SHARYOU_NAME { get; set; }
        public string SHASHU_CD { get; set; }
        public string SHASHU_NAME { get; set; }
        public string UNTENSHA_CD { get; set; }
        public string UNTENSHA_NAME { get; set; }
        public SqlInt16 KEITAI_KBN_CD { get; set; }
        public string KEITAI_KBN_NAME { get; set; }
        public string NIZUMI_GYOUSHA_CD { get; set; }
        public string NIZUMI_GYOUSHA_NAME { get; set; }
        public string NIZUMI_GENBA_CD { get; set; }
        public string NIZUMI_GENBA_NAME { get; set; }
        public string NIOROSHI_GYOUSHA_CD { get; set; }
        public string NIOROSHI_GYOUSHA_NAME { get; set; }
        public string NIOROSHI_GENBA_CD { get; set; }
        public string NIOROSHI_GENBA_NAME { get; set; }
        public string DENPYOU_BIKOU { get; set; }
        public SqlDecimal NET_TOTAL { get; set; }
        public SqlDecimal KINGAKU_TOTAL { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}