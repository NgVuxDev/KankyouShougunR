using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_JISSEKI_HOUKOKU_SBN_DETAIL : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlInt16 REPORT_ID { get; set; }
        public SqlInt16 HOUKOKU_SHOSHIKI_KBN { get; set; }
        public string HOZON_NAME { get; set; }
        public string HOUKOKU_YEAR { get; set; }
        public string TEISHUTSUSAKI_CHIIKI_CD { get; set; }
        public SqlInt16 TOKUBETSU_KANRI_KBN { get; set; }
        public SqlInt16 JIGYOUJOU_KBN { get; set; }
        public SqlInt32 KEN_KBN { get; set; }
        public SqlInt16 GYOUSHA_KBN { get; set; }
        public string HOUKOKUSHO_BUNRUI_CD { get; set; }
        public string HOUKOKUSHO_BUNRUI_NAME { get; set; }
        public string HAIKI_SHURUI_CD { get; set; }
        public string HAIKI_SHURUI_NAME { get; set; }
        public SqlBoolean SEKIMEN_KBN { get; set; }
        public SqlBoolean TOKUTEI_YUUGAI_KBN { get; set; }
        public string SAIITAKU_KYOKA_NO { get; set; }
        public string HST_GYOUSHA_CD { get; set; }
        public string HST_GYOUSHA_NAME { get; set; }
        public string HST_GYOUSHA_ADDRESS { get; set; }
        public string HST_GYOUSHA_GYOUSHU_CD { get; set; }
        public string HST_GENBA_CD { get; set; }
        public string HST_GENBA_NAME { get; set; }
        public string HST_GENBA_ADDRESS { get; set; }
        public string HST_GENBA_GYOUSHU_CD { get; set; }
        public string HST_GENBA_CHIIKI_CD { get; set; }
        public SqlDecimal JYUTAKU_RYOU { get; set; }
        public string JYUTAKU_KBN { get; set; }
        public string JYUTAKU_UNIT_NAME { get; set; }
        public string SHOBUN_HOUHOU_CD { get; set; }
        public string SHOBUN_HOUHOU_NAME { get; set; }
        public string SHOBUN_MOKUTEKI_CD { get; set; }
        public string SHOBUN_MOKUTEKI_NAME { get; set; }
        public string SBN_GYOUSHA_CD { get; set; }
        public string SBN_GYOUSHA_NAME { get; set; }
        public string SBN_GYOUSHA_ADDRESS { get; set; }
        public string SBN_GENBA_CD { get; set; }
        public string SBN_GENBA_NAME { get; set; }
        public string SBN_GENBA_ADDRESS { get; set; }
        public string SBN_GENBA_CHIIKI_CD { get; set; }
        public SqlDecimal SBN_RYOU { get; set; }
        public SqlDecimal SBN_AFTER_RYOU { get; set; }
        public string ITAKU_HINMOKU_CD { get; set; }
        public string ITAKU_HINMOKU_NAME { get; set; }
        public string ITAKUSAKI_CD { get; set; }
        public string ITAKU_GENBA_CD { get; set; }
        public string ITAKUSAKI_NAME { get; set; }
        public string ITAKUSAKI_ADDRESS { get; set; }
        public string ITAKUSAKI_CHIIKI_CD { get; set; }
        public string ITAKUSAKI_KYOKA_NO { get; set; }
        public string ITAKU_SHOBUN_HOUHOU_CD { get; set; }
        public string ITAKU_SHOBUN_HOUHOU_NAME { get; set; }
        public SqlDecimal ITAKU_RYOU { get; set; }
        public SqlInt16 ITAKU_KBN { get; set; }
        public string HST_JOU_CHIIKI_CD { get; set; }
        public SqlInt16 HST_JOU_TODOUFUKEN_CD { get; set; }
        public SqlInt16 HST_KEN_KBN { get; set; }
    }
}