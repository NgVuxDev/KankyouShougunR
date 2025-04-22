using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_DENSHI_KEIYAKU_KARI_ENTRY : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public string DOCUMENT_ID { get; set; }
        public SqlInt32 PAGE { get; set; }
        public string SEND_TITLE { get; set; }
        public string SEND_MESSAGE { get; set; }
        public string SHANAI_BIKO { get; set; }
        public SqlInt32 KEIYAKU_JYOUKYOU { get; set; }
        public SqlDateTime KEIYAKUSHO_CREATE_DATE { get; set; }
        public SqlDateTime KEIYAKUSHO_UPDATE_DATE { get; set; }
        public string KEIYAKUSHA { get; set; }
        public SqlDateTime KEIYAKUSHO_KEIYAKU_DATE { get; set; }
        public SqlDateTime YUUKOU_BEGIN { get; set; }
        public SqlDateTime YUUKOU_END { get; set; }
        public SqlInt32 AUTO_UPDATE { get; set; }
        public string KANRI_TITLE { get; set; }
        public string LOCAL_ID { get; set; }
        public SqlDecimal AMOUNT { get; set; }
        public SqlDateTime KAIYAKU_TSUUCHI { get; set; }
        public string SHORUI_INFO_NAME1 { get; set; }
        public string SHORUI_INFO_NAME2 { get; set; }
        public string SHORUI_INFO_NAME3 { get; set; }
        public string SHORUI_INFO_NAME4 { get; set; }
        public string SHORUI_INFO_NAME5 { get; set; }
        public string SHORUI_INFO_NAME6 { get; set; }
        public string SHORUI_INFO_NAME7 { get; set; }
        public string SHORUI_INFO_NAME8 { get; set; }
        public string SHORUI_INFO_NAME9 { get; set; }
        public string SHORUI_INFO_NAME10 { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}