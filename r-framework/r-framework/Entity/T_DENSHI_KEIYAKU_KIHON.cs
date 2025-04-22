using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_DENSHI_KEIYAKU_KIHON : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public string DENSHI_KEIYAKU_SYSTEM_ID { get; set; }
        public SqlInt32 KEIYAKU_JYOUKYOU { get; set; }
        public string SHAIN_CD { get; set; }
        public string DENSHI_KEIYAKU_CLIENT_ID { get; set; }
        public string SEND_TITLE { get; set; }
        public string SEND_MESSAGE { get; set; }
        public string SHANAI_BIKO { get; set; }
        public string DOCUMENT_ID { get; set; }
        public string ACCESS_CD { get; set; }
        public string KEIYAKU_NO { get; set; }
        public SqlDateTime KEIYAKUSHO_KEIYAKU_DATE { get; set; }
        public SqlDateTime KEIYAKUSHO_CREATE_DATE { get; set; }
        public SqlDateTime KEIYAKUSHO_SEND_DATE { get; set; }
        public SqlBoolean KOBETSU_SHITEI_CHECK { get; set; }
        public SqlInt16 KOUSHIN_SHUBETSU { get; set; }
        public SqlDateTime YUUKOU_BEGIN { get; set; }
        public SqlDateTime YUUKOU_END { get; set; }
        public SqlDateTime KOUSHIN_END_DATE { get; set; }
        public string HAISHUTSU_JIGYOUSHA_CD { get; set; }
        public string HAISHUTSU_JIGYOUSHA_NAME { get; set; }
        public string HAISHUTSU_JIGYOUJOU_CD { get; set; }
        public string BIKOU1 { get; set; }
        public string BIKOU2 { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
