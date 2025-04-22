using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_JISSEKI_HOUKOKU_ENTRY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 REPORT_ID { get; set; }
        public string HOUKOKU_YEAR { get; set; }
        public SqlInt16 DENMANI_KBN { get; set; }
        public string HOZON_NAME { get; set; }
        public SqlDateTime TEISHUTU_DATE { get; set; }
        public string HOUKOKU_GYOUSHA_CD { get; set; }
        public string HOUKOKU_GENBA_CD { get; set; }
        public SqlInt16 GYOUSHA_KBN { get; set; }
        public SqlInt16 JIGYOUJOU_KBN { get; set; }
        public string TEISHUTSU_CHIIKI_CD { get; set; }
        public string TEISHUTSU_NAME { get; set; }
        public SqlInt16 HOUKOKU_SHOSHIKI { get; set; }
        public string HOUKOKU_TITLE1 { get; set; }
        public string HOUKOKU_TITLE2 { get; set; }
        public SqlInt16 TOKUBETSU_KANRI_KBN { get; set; }
        public SqlDateTime DATE_BEGIN { get; set; }
        public SqlDateTime DATE_END { get; set; }
        public SqlInt32 KEN_KBN { get; set; }
        public SqlInt16 HST_GYOUSHA_NAME_DISP_KBN { get; set; }
        public SqlInt16 ADDRESS_KBN { get; set; }
        public SqlInt16 SAI_ITAKU_KBN { get; set; }
        public SqlInt16 TMH_KBN { get; set; }
        public SqlInt16 UPN_SAI_ITAKU_KBN { get; set; }
        public SqlInt16 JISHA_HST_KBN { get; set; }
        public SqlInt16 TASHA_KYOKA_KBN { get; set; }
        public string UNIT_NAME { get; set; }
        public string HOUKOKU_TANTO_NAME { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}