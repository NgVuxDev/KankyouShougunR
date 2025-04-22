using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_MANIFEST_PT_ENTRY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlBoolean LIST_REGIST_KBN { get; set; }
        public SqlInt16 HAIKI_KBN_CD { get; set; }
        public SqlBoolean FIRST_MANIFEST_KBN { get; set; }
        public string PATTERN_NAME { get; set; }
        public string PATTERN_FURIGANA { get; set; }
        public SqlBoolean USE_DEFAULT_KBN { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public string JIZEN_NUMBER { get; set; }
        public SqlDateTime JIZEN_DATE { get; set; }
        public string SEARCH_JIZEN_DATE { get; set; }
        public SqlDateTime KOUFU_DATE { get; set; }
        public string SEARCH_KOUFU_DATE { get; set; }
        public SqlInt16 KOUFU_KBN { get; set; }
        public string MANIFEST_ID { get; set; }
        public string SEIRI_ID { get; set; }
        public string KOUFU_TANTOUSHA { get; set; }
        public string KOUFU_TANTOUSHA_SHOZOKU { get; set; }
        public string HST_GYOUSHA_CD { get; set; }
        public string HST_GYOUSHA_NAME { get; set; }
        public string HST_GYOUSHA_POST { get; set; }
        public string HST_GYOUSHA_TEL { get; set; }
        public string HST_GYOUSHA_ADDRESS { get; set; }
        public string HST_GENBA_CD { get; set; }
        public string HST_GENBA_NAME { get; set; }
        public string HST_GENBA_POST { get; set; }
        public string HST_GENBA_TEL { get; set; }
        public string HST_GENBA_ADDRESS { get; set; }
        public string BIKOU { get; set; }
        public string KONGOU_SHURUI_CD { get; set; }
        public SqlDecimal HAIKI_SUU { get; set; }
        public SqlInt16 HAIKI_UNIT_CD { get; set; }
        public SqlDecimal TOTAL_SUU { get; set; }
        public SqlDecimal TOTAL_KANSAN_SUU { get; set; }
        public SqlDecimal TOTAL_GENNYOU_SUU { get; set; }
        public SqlInt16 CHUUKAN_HAIKI_KBN { get; set; }
        public string CHUUKAN_HAIKI { get; set; }
        public SqlInt16 LAST_SBN_YOTEI_KBN { get; set; }
        public string LAST_SBN_YOTEI_GYOUSHA_CD { get; set; }
        public string LAST_SBN_YOTEI_GENBA_CD { get; set; }
        public string LAST_SBN_YOTEI_GENBA_NAME { get; set; }
        public string LAST_SBN_YOTEI_GENBA_POST { get; set; }
        public string LAST_SBN_YOTEI_GENBA_TEL { get; set; }
        public string LAST_SBN_YOTEI_GENBA_ADDRESS { get; set; }
        public string SBN_GYOUSHA_CD { get; set; }
        public string SBN_GYOUSHA_NAME { get; set; }
        public string SBN_GYOUSHA_POST { get; set; }
        public string SBN_GYOUSHA_TEL { get; set; }
        public string SBN_GYOUSHA_ADDRESS { get; set; }
        public string TMH_GYOUSHA_CD { get; set; }
        public string TMH_GYOUSHA_NAME { get; set; }
        public string TMH_GENBA_CD { get; set; }
        public string TMH_GENBA_NAME { get; set; }
        public string TMH_GENBA_POST { get; set; }
        public string TMH_GENBA_TEL { get; set; }
        public string TMH_GENBA_ADDRESS { get; set; }
        public SqlInt16 YUUKA_KBN { get; set; }
        public SqlDecimal YUUKA_SUU { get; set; }
        public SqlInt16 YUUKA_UNIT_CD { get; set; }
        public string SBN_JYURYOUSHA_CD { get; set; }
        public string SBN_JYURYOUSHA_NAME { get; set; }
        public string SBN_JYURYOU_TANTOU_CD { get; set; }
        public string SBN_JYURYOU_TANTOU_NAME { get; set; }
        public SqlDateTime SBN_JYURYOU_DATE { get; set; }
        public string SEARCH_SBN_JYURYOU_DATE { get; set; }
        public string SBN_JYUTAKUSHA_CD { get; set; }
        public string SBN_JYUTAKUSHA_NAME { get; set; }
        public string SBN_TANTOU_CD { get; set; }
        public string SBN_TANTOU_NAME { get; set; }
        public string LAST_SBN_GYOUSHA_CD { get; set; }
        public string LAST_SBN_GENBA_CD { get; set; }
        public string LAST_SBN_GENBA_NAME { get; set; }
        public string LAST_SBN_GENBA_POST { get; set; }
        public string LAST_SBN_GENBA_TEL { get; set; }
        public string LAST_SBN_GENBA_ADDRESS { get; set; }
        public string LAST_SBN_GENBA_NUMBER { get; set; }
        public string LAST_SBN_CHECK_NAME { get; set; }
        public SqlDateTime CHECK_B1 { get; set; }
        public string SEARCH_CHECK_B1 { get; set; }
        public SqlDateTime CHECK_B2 { get; set; }
        public string SEARCH_CHECK_B2 { get; set; }
        public SqlDateTime CHECK_B4 { get; set; }
        public string SEARCH_CHECK_B4 { get; set; }
        public SqlDateTime CHECK_B6 { get; set; }
        public string SEARCH_CHECK_B6 { get; set; }
        public SqlDateTime CHECK_D { get; set; }
        public string SEARCH_CHECK_D { get; set; }
        public SqlDateTime CHECK_E { get; set; }
        public string SEARCH_CHECK_E { get; set; }
        public SqlInt16 RENKEI_DENSHU_KBN_CD { get; set; }
        public SqlInt64 RENKEI_SYSTEM_ID { get; set; }
        public SqlInt64 RENKEI_MEISAI_SYSTEM_ID { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
        public SqlInt16 MANIFEST_MERCURY_CHECK { get; set; }
        public SqlBoolean MERCURY_USED_SEIHIN_HAIKIBUTU_CHECK { get; set; }
        public SqlBoolean MERCURY_BAIJINNADO_HAIKIBUTU_CHECK { get; set; }
        public SqlBoolean ISIWAKANADO_HAIKIBUTU_CHECK { get; set; }
        public SqlBoolean TOKUTEI_SANGYOU_HAIKIBUTU_CHECK { get; set; }
        public SqlInt16 RENKEI_MEISAI_MODE { get; set; }
        /// <summary>
        /// ã∆é“ÅAåªèÍèÓïÒÇœ∆ópéÜÇ÷àÛéöÇ∑ÇÈÇ©Ç«Ç§Ç©
        /// </summary>
        public SqlInt16 MANIFEST_KAMI_CHECK { get; set; }
    }
}