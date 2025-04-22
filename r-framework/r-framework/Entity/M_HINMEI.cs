using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_HINMEI : SuperEntity
    {
        public string HINMEI_CD { get; set; }
        public string HINMEI_NAME { get; set; }
        public string HINMEI_NAME_RYAKU { get; set; }
        public string HINMEI_FURIGANA { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public SqlInt16 DENSHU_KBN_CD { get; set; }
        public SqlInt16 DENPYOU_KBN_CD { get; set; }
        public SqlInt16 ZEI_KBN_CD { get; set; }
        public string SHURUI_CD { get; set; }
        public string BUNRUI_CD { get; set; }
        public string HOUKOKUSHO_BUNRUI_CD { get; set; }
        public string JISSEKI_BUNRUI_CD { get; set; }
        public string SP_CHOKKOU_HAIKI_SHURUI_CD { get; set; }
        public string SP_TSUMIKAE_HAIKI_SHURUI_CD { get; set; }
        public string KP_HAIKI_SHURUI_CD { get; set; }
        public string DM_HAIKI_SHURUI_CD { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }

        //20250312
        public SqlDecimal TC_KOME_KANZANKEISU { get; set; }

        //20250313
        public string HAIKI_MONO_MEISHO_CD { get; set; }
        public string NISUGATA_CD { get; set; }
        public string SHOBUN_HOHO_CD { get; set; }
    }
}