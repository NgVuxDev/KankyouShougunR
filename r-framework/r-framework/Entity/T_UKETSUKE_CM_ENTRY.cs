using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_UKETSUKE_CM_ENTRY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public SqlInt64 UKETSUKE_NUMBER { get; set; }
        public SqlDateTime UKETSUKE_DATE { get; set; }
        public string SEARCH_UKETSUKE_DATE { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public string TORIHIKISAKI_NAME { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GYOUSHA_NAME { get; set; }
        public string GENBA_CD { get; set; }
        public string GENBA_NAME { get; set; }
        public string EIGYOU_TANTOUSHA_CD { get; set; }
        public string EIGYOU_TANTOUSHA_NAME { get; set; }
        public string TAIOU_END__DATE { get; set; }
        public string TITLE_NAME { get; set; }
        public string SENPOU_TOIAWASE_USER { get; set; }
        public string NAIYOU_1 { get; set; }
        public string NAIYOU_2 { get; set; }
        public string NAIYOU_3 { get; set; }
        public string NAIYOU_4 { get; set; }
        public string NAIYOU_5 { get; set; }
        public string NAIYOU_6 { get; set; }
        public string NAIYOU_7 { get; set; }
        public string NAIYOU_8 { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}