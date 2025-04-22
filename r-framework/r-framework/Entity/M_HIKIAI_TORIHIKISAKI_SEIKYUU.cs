using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_HIKIAI_TORIHIKISAKI_SEIKYUU : SuperEntity
    {
        public string TORIHIKISAKI_CD { get; set; }
        public SqlInt16 TORIHIKI_KBN_CD { get; set; }
        public SqlInt16 SHIMEBI1 { get; set; }
        public SqlInt16 SHIMEBI2 { get; set; }
        public SqlInt16 SHIMEBI3 { get; set; }
        public SqlInt16 HICCHAKUBI { get; set; }
        public SqlInt16 KAISHUU_MONTH { get; set; }
        public SqlInt16 KAISHUU_DAY { get; set; }
        public SqlInt16 KAISHUU_HOUHOU { get; set; }
        public string SEIKYUU_JOUHOU1 { get; set; }
        public string SEIKYUU_JOUHOU2 { get; set; }
        public SqlDecimal KAISHI_URIKAKE_ZANDAKA { get; set; }
        public SqlInt16 SHOSHIKI_KBN { get; set; }
        public SqlInt16 SHOSHIKI_MEISAI_KBN { get; set; }
        public SqlDateTime LAST_TORIHIKI_DATE { get; set; }
        public string SEARCH_LAST_TORIHIKI_DATE { get; set; }
        public SqlInt16 SEIKYUU_KEITAI_KBN { get; set; }
        public SqlInt16 NYUUKIN_MEISAI_KBN { get; set; }
        public SqlInt16 YOUSHI_KBN { get; set; }
        public SqlInt16 ZEI_KEISAN_KBN_CD { get; set; }
        public SqlInt16 ZEI_KBN_CD { get; set; }
        public SqlInt16 TAX_HASUU_CD { get; set; }
        public SqlInt16 KINGAKU_HASUU_CD { get; set; }
        public string FURIKOMI_BANK_CD { get; set; }
        public string FURIKOMI_BANK_SHITEN_CD { get; set; }
        public string KOUZA_SHURUI { get; set; }
        public string KOUZA_NO { get; set; }
        public string KOUZA_NAME { get; set; }
        public string FURIKOMI_BANK_CD_2 { get; set; }
        public string FURIKOMI_BANK_SHITEN_CD_2 { get; set; }
        public string KOUZA_SHURUI_2 { get; set; }
        public string KOUZA_NO_2 { get; set; }
        public string KOUZA_NAME_2 { get; set; }
        public string FURIKOMI_BANK_CD_3 { get; set; }
        public string FURIKOMI_BANK_SHITEN_CD_3 { get; set; }
        public string KOUZA_SHURUI_3 { get; set; }
        public string KOUZA_NO_3 { get; set; }
        public string KOUZA_NAME_3 { get; set; }
        public string SEIKYUU_SOUFU_NAME1 { get; set; }
        public string SEIKYUU_SOUFU_NAME2 { get; set; }
        public string SEIKYUU_SOUFU_KEISHOU1 { get; set; }
        public string SEIKYUU_SOUFU_KEISHOU2 { get; set; }
        public string SEIKYUU_SOUFU_POST { get; set; }
        public string SEIKYUU_SOUFU_ADDRESS1 { get; set; }
        public string SEIKYUU_SOUFU_ADDRESS2 { get; set; }
        public string SEIKYUU_SOUFU_BUSHO { get; set; }
        public string SEIKYUU_SOUFU_TANTOU { get; set; }
        public string SEIKYUU_SOUFU_TEL { get; set; }
        public string SEIKYUU_SOUFU_FAX { get; set; }
        public string NYUUKINSAKI_CD { get; set; }
        public string SEIKYUU_TANTOU { get; set; }
        public SqlInt16 SEIKYUU_DAIHYOU_PRINT_KBN { get; set; }
        public SqlInt16 SEIKYUU_KYOTEN_PRINT_KBN { get; set; }
        public SqlInt16 SEIKYUU_KYOTEN_CD { get; set; }
        // 20160429 koukoukon v2.1_電子請求書 #16612 start
        public SqlInt16 OUTPUT_KBN { get; set; }
        public string HAKKOUSAKI_CD { get; set; }
        // 20160429 koukoukon v2.1_電子請求書 #16612 end
        public string FURIKOMI_NAME1 { get; set; }
        public string FURIKOMI_NAME2 { get; set; }

        //160026 S
        public SqlInt16 KAISHUU_BETSU_KBN { get; set; }
        public SqlInt16 KAISHUU_BETSU_NICHIGO { get; set; }
        //160026 E

    }
}