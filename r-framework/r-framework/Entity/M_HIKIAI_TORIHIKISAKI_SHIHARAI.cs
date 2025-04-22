using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_HIKIAI_TORIHIKISAKI_SHIHARAI : SuperEntity
    {
        public string TORIHIKISAKI_CD { get; set; }
        public SqlInt16 TORIHIKI_KBN_CD { get; set; }
        public SqlInt16 SHIMEBI1 { get; set; }
        public SqlInt16 SHIMEBI2 { get; set; }
        public SqlInt16 SHIMEBI3 { get; set; }
        public SqlInt16 SHIHARAI_MONTH { get; set; }
        public SqlInt16 SHIHARAI_DAY { get; set; }
        public SqlInt16 SHIHARAI_HOUHOU { get; set; }
        public string SHIHARAI_JOUHOU1 { get; set; }
        public string SHIHARAI_JOUHOU2 { get; set; }
        public SqlDecimal KAISHI_KAIKAKE_ZANDAKA { get; set; }
        public SqlInt16 SHOSHIKI_KBN { get; set; }
        public SqlInt16 SHOSHIKI_MEISAI_KBN { get; set; }
        public SqlDateTime LAST_TORIHIKI_DATE { get; set; }
        public string SEARCH_LAST_TORIHIKI_DATE { get; set; }
        public SqlInt16 SHIHARAI_KEITAI_KBN { get; set; }
        public SqlInt16 SHUKKIN_MEISAI_KBN { get; set; }
        public SqlInt16 YOUSHI_KBN { get; set; }
        public SqlInt16 ZEI_KEISAN_KBN_CD { get; set; }
        public SqlInt16 ZEI_KBN_CD { get; set; }
        public SqlInt16 TAX_HASUU_CD { get; set; }
        public SqlInt16 KINGAKU_HASUU_CD { get; set; }
        public string SHIHARAI_SOUFU_NAME1 { get; set; }
        public string SHIHARAI_SOUFU_NAME2 { get; set; }
        public string SHIHARAI_SOUFU_KEISHOU1 { get; set; }
        public string SHIHARAI_SOUFU_KEISHOU2 { get; set; }
        public string SHIHARAI_SOUFU_POST { get; set; }
        public string SHIHARAI_SOUFU_ADDRESS1 { get; set; }
        public string SHIHARAI_SOUFU_ADDRESS2 { get; set; }
        public string SHIHARAI_SOUFU_BUSHO { get; set; }
        public string SHIHARAI_SOUFU_TANTOU { get; set; }
        public string SHIHARAI_SOUFU_TEL { get; set; }
        public string SHIHARAI_SOUFU_FAX { get; set; }
        public string SYUUKINSAKI_CD { get; set; }
        public SqlInt16 SHIHARAI_KYOTEN_PRINT_KBN { get; set; }
        public SqlInt16 SHIHARAI_KYOTEN_CD { get; set; }

        //160026 S
        public SqlInt16 SHIHARAI_BETSU_KBN { get; set; }
        public SqlInt16 SHIHARAI_BETSU_NICHIGO { get; set; }
        
        public SqlInt16 FURIKOMI_EXPORT_KBN { get; set; }
        public string FURIKOMI_SAKI_BANK_CD { get; set; }
        public string FURIKOMI_SAKI_BANK_NAME { get; set; }
        public string FURIKOMI_SAKI_BANK_SHITEN_CD { get; set; }
        public string FURIKOMI_SAKI_BANK_SHITEN_NAME { get; set; }
        public SqlInt16 FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD { get; set; }
        public string FURIKOMI_SAKI_BANK_KOUZA_SHURUI { get; set; }
        public string FURIKOMI_SAKI_BANK_KOUZA_NO { get; set; }
        public string FURIKOMI_SAKI_BANK_KOUZA_NAME { get; set; }
        public SqlInt16 TEI_SUU_RYOU_KBN { get; set; }
        public string FURI_KOMI_MOTO_BANK_CD { get; set; }
        public string FURI_KOMI_MOTO_BANK_SHITTEN_CD { get; set; }
        public string FURI_KOMI_MOTO_SHURUI { get; set; }
        public string FURI_KOMI_MOTO_NO { get; set; }
        public string FURI_KOMI_MOTO_NAME { get; set; }

        //160026 E


    }
}