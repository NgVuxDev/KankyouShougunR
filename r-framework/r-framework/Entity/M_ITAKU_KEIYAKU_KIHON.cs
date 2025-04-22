using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_ITAKU_KEIYAKU_KIHON : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public string ITAKU_KEIYAKU_NO { get; set; }
        public SqlInt16 ITAKU_KEIYAKU_SHURUI { get; set; }
        public SqlInt16 ITAKU_KEIYAKU_TOUROKU_HOUHOU { get; set; }
        public SqlInt16 ITAKU_KEIYAKU_TYPE { get; set; }
        public SqlInt16 ITAKU_KEIYAKU_STATUS { get; set; }
        public string HAISHUTSU_JIGYOUSHA_CD { get; set; }
        public string HAISHUTSU_JIGYOUJOU_CD { get; set; }
        public string ITAKU_KEIYAKU_FILE_PATH { get; set; }
        public SqlInt16 KOUSHIN_SHUBETSU { get; set; }
        public SqlDateTime KOUSHIN_END_DATE { get; set; }
        public string SEARCH_KOUSHIN_END_DATE { get; set; }
        public SqlDateTime YUUKOU_BEGIN { get; set; }
        public string SEARCH_YUUKOU_BEGIN { get; set; }
        public SqlDateTime YUUKOU_END { get; set; }
        public string SEARCH_YUUKOU_END { get; set; }
        public string SHIHARAI_HOUHOU { get; set; }
        public SqlDateTime KEIYAKUSHO_KEIYAKU_DATE { get; set; }
        public string SEARCH_KEIYAKUSHO_KEIYAKU_DATE { get; set; }
        public SqlDateTime KEIYAKUSHO_CREATE_DATE { get; set; }
        public string SEARCH_KEIYAKUSHO_CREATE_DATE { get; set; }
        public SqlDateTime KEIYAKUSHO_SEND_DATE { get; set; }
        public string SEARCH_KEIYAKUSHO_SEND_DATE { get; set; }
        public SqlDateTime KEIYAKUSHO_RETURN_DATE { get; set; }
        public string SEARCH_KEIYAKUSHO_RETURN_DATE { get; set; }
        public SqlDateTime KEIYAKUSHO_END_DATE { get; set; }
        public string SEARCH_KEIYAKUSHO_END_DATE { get; set; }
        public string BIKOU1 { get; set; }
        public string BIKOU2 { get; set; }
        public string HITSUYOUNAJYOUHOU1 { get; set; }
        public string HITSUYOUNAJYOUHOU2 { get; set; }
        public string HITSUYOUNAJYOUHOU3 { get; set; }
        public string KYOUGIJIKOU1 { get; set; }
        public string KYOUGIJIKOU2 { get; set; }
        public SqlInt16 JIZEN_KYOUGI { get; set; }
        public SqlInt64 SHOBUN_PATTERN_SYSTEM_ID { get; set; }
        public SqlInt32 SHOBUN_PATTERN_SEQ { get; set; }
        public string SHOBUN_PATTERN_NAME { get; set; }
        public SqlInt64 LAST_SHOBUN_PATTERN_SYSTEM_ID { get; set; }
        public SqlInt32 LAST_SHOBUN_PATTERN_SEQ { get; set; }
        public string LAST_SHOBUN_PATTERN_NAME { get; set; }
        public SqlBoolean ITAKU_KEIYAKU_CHECK { get; set; }
        public SqlBoolean KOBETSU_SHITEI_CHECK { get; set; }
        public string HST_FREE_COMMENT { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}