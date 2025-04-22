using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_WANSIGN_KEIYAKU_INFO : SuperEntity
    {
        public SqlInt64 WANSIGN_SYSTEM_ID { get; set; }
        public string XID { get; set; }
        public string CONTROL_NUMBER { get; set; }
        public string ORIGINAL_CONTROL_NUMBER { get; set; }
        public string DOCUMENT_ID { get; set; }
        public string IS_VIEW_DOC_CONTROL { get; set; }
        public string BOX_NUMBER { get; set; }
        public SqlInt16 IS_VALID { get; set; }
        public string DOCUMENT_NAME { get; set; }
        public SqlDateTime CONTRACT_DATE { get; set; }
        public string SEARCH_CONTRACT_DATE { get; set; }
        public SqlDateTime CONTRACT_EXPIRATION_DATE { get; set; }
        public string SEARCH_CONTRACT_EXPIRATION_DATE { get; set; }
        public SqlInt16 IS_AUTO_UPDATING { get; set; }
        public string RENEWWAL_PERIOD { get; set; }
        public string RENEWWAL_PERIOD_UNIT { get; set; }
        public string CANCEL_PERIOD { get; set; }
        public string CANCEL_PERIOD_UNIT { get; set; }
        public SqlInt16 IS_REMINDER { get; set; }
        public string REMINDER_PERIOD { get; set; }
        public string REMINDER_PERIOD_UNIT { get; set; }
        public string POST_NM { get; set; }
        public string NAME_NM { get; set; }
        public SqlDecimal CONTRACT_DECIMAL { get; set; }
        public string STORAGE_LOCATION { get; set; }
        public string COMMENT_1 { get; set; }
        public string COMMENT_2 { get; set; }
        public string COMMENT_3 { get; set; }
        public string FIELD_1 { get; set; }
        public string FIELD_2 { get; set; }
        public string FIELD_3 { get; set; }
        public string FIELD_4 { get; set; }
        public string FIELD_5 { get; set; }
        public string PARTNER_ORGANIZE_NM { get; set; }
        public SqlDateTime SIGNING_DATETIME { get; set; }
        public string SEARCH_SIGNING_DATETIME { get; set; }
        public SqlDateTime CREATED_AT { get; set; }
        public string SEARCH_CREATED_AT { get; set; }
        public string PARENT_CONTROL_NUMBER { get; set; }
        public string PARENT_DOCUMENT_NAME { get; set; }
        public string CHILD_CONTROL_NUMBER { get; set; }
        public string CHILD_DOCUMENT_NAME { get; set; }
        public string REGISTERED_USER_NAME { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }

        //#161407 20220314 CongBinh S
        public string PARTNER_ORGANIZE_NM2 { get; set; }
        public string PARTNER_ORGANIZE_NM3 { get; set; }
        public string PARTNER_ORGANIZE_NM4 { get; set; }
        public string PARTNER_ORGANIZE_NM5 { get; set; }
        public string PARTNER_ORGANIZE_NM6 { get; set; }
        public string PARTNER_ORGANIZE_NM7 { get; set; }
        public string PARTNER_ORGANIZE_NM8 { get; set; }
        public string PARTNER_ORGANIZE_NM9 { get; set; }
        public string PARTNER_ORGANIZE_NM10 { get; set; }
        public string PARTNER_ORGANIZE_NM11 { get; set; }
        public string PARTNER_ORGANIZE_NM12 { get; set; }
        public string PARTNER_ORGANIZE_NM13 { get; set; }
        public string PARTNER_ORGANIZE_NM14 { get; set; }
        public string PARTNER_ORGANIZE_NM15 { get; set; }
        public string PARTNER_ORGANIZE_NM16 { get; set; }
        //#161407 20220314 CongBinh E
    }
}