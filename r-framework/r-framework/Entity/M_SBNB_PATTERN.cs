using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_SBNB_PATTERN : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 ROW_NO { get; set; }
        public SqlInt16 ITAKU_KEIYAKU_TYPE { get; set; }
        public SqlInt16 LAST_SBN_KBN { get; set; }
        public string PATTERN_NAME { get; set; }
        public string PATTERN_FURIGANA { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GYOUSHA_NAME { get; set; }
        public string GYOUSHA_ADDRESS { get; set; }
        public string GYOUSHA_ADDRESS1 { get; set; }
        public string GYOUSHA_ADDRESS2 { get; set; }        
        public string GENBA_CD { get; set; }
        public string GENBA_NAME { get; set; }
        public string GENBA_ADDRESS { get; set; }
        public string GENBA_ADDRESS1 { get; set; }
        public string GENBA_ADDRESS2 { get; set; }
        public string SHOBUN_HOUHOU_CD { get; set; }
        public string SHORI_SPEC { get; set; }
        public string OTHER { get; set; }
        public SqlDecimal HOKAN_JOGEN { get; set; }
        public SqlInt16 HOKAN_JOGEN_UNIT_CD { get; set; }
        public SqlInt16 UNPAN_FROM { get; set; }
        public SqlInt16 UNPAN_END { get; set; }
        public SqlInt16 KONGOU { get; set; }
        public SqlInt16 SHUSENBETU { get; set; }
        public SqlInt16 BUNRUI { get; set; }
        public SqlInt16 END_KUBUN { get; set; }
        public string HOUKOKUSHO_BUNRUI_CD { get; set; }
        public string HOUKOKUSHO_BUNRUI_NAME { get; set; }
        public string SHOBUNSAKI_NO { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}