using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_MANIFEST_PT_PRT : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlBoolean PRT_FUTSUU_HAIKIBUTSU { get; set; }
        public SqlBoolean PRT_TOKUBETSU_HAIKIBUTSU { get; set; }
        public string PRT_HAIKI_SHURUI_CD { get; set; }
        public string PRT_HAIKI_SHURUI_NAME { get; set; }
        public SqlDecimal PRT_SUU { get; set; }
        public SqlInt16 PRT_UNIT_CD { get; set; }
        public string PRT_NISUGATA_CD { get; set; }
        public string PRT_NISUGATA_NAME { get; set; }
        public string PRT_HAIKI_NAME_CD { get; set; }
        public string PRT_HAIKI_NAME { get; set; }
        public string PRT_YUUGAI_CD { get; set; }
        public string PRT_YUUGAI_NAME { get; set; }
        public string PRT_SBN_HOUHOU_CD { get; set; }
        public string PRT_SBN_HOUHOU_NAME { get; set; }
        public SqlBoolean SLASH_YUUGAI_FLG { get; set; }
        public SqlBoolean SLASH_BIKOU_FLG { get; set; }
        public SqlBoolean SLASH_CHUUKAN_FLG { get; set; }
        public SqlBoolean SLASH_TSUMIHO_FLG { get; set; }
        public SqlBoolean SLASH_JIZENKYOUGI_FLG { get; set; }
        public SqlBoolean SLASH_UPN_GYOUSHA2_FLG { get; set; }
        public SqlBoolean SLASH_UPN_GYOUSHA3_FLG { get; set; }
        public SqlBoolean SLASH_UPN_JYUTAKUSHA2_FLG { get; set; }
        public SqlBoolean SLASH_UPN_JYUTAKUSHA3_FLG { get; set; }
        public SqlBoolean SLASH_UPN_SAKI_GENBA2_FLG { get; set; }
        public SqlBoolean SLASH_UPN_SAKI_GENBA3_FLG { get; set; }
        public SqlBoolean SLASH_B1_FLG { get; set; }
        public SqlBoolean SLASH_B2_FLG { get; set; }
        public SqlBoolean SLASH_B4_FLG { get; set; }
        public SqlBoolean SLASH_B6_FLG { get; set; }
        public SqlBoolean SLASH_D_FLG { get; set; }
        public SqlBoolean SLASH_E_FLG { get; set; }
        public SqlBoolean SLASH_MERCURY_FLG { get; set; }
    }
}