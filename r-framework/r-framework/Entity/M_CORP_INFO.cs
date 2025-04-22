using System;
using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Serializable()]
    public class M_CORP_INFO : SuperEntity
    {
        public SqlInt16 SYS_ID { get; set; }
        public string CORP_NAME { get; set; }
        public string CORP_RYAKU_NAME { get; set; }
        public string CORP_FURIGANA { get; set; }
        public string CORP_DAIHYOU { get; set; }
        public SqlInt16 KISHU_MONTH { get; set; }
        public SqlInt16 SHIMEBI { get; set; }
        public SqlInt16 SHIHARAI_MONTH { get; set; }
        public SqlInt16 SHIHARAI_DAY { get; set; }
        public SqlInt16 SHIHARAI_HOUHOU { get; set; }
        public string BANK_CD { get; set; }
        public string BANK_SHITEN_CD { get; set; }
        public string KOUZA_SHURUI { get; set; }
        public string KOUZA_NO { get; set; }
        public string KOUZA_NAME { get; set; }
        public string BANK_CD_2 { get; set; }
        public string BANK_SHITEN_CD_2 { get; set; }
        public string KOUZA_SHURUI_2 { get; set; }
        public string KOUZA_NO_2 { get; set; }
        public string KOUZA_NAME_2 { get; set; }
        public string BANK_CD_3 { get; set; }
        public string BANK_SHITEN_CD_3 { get; set; }
        public string KOUZA_SHURUI_3 { get; set; }
        public string KOUZA_NO_3 { get; set; }
        public string KOUZA_NAME_3 { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }

        public string FURIKOMI_MOTO_BANK_CD { get; set; }
        public string FURIKOMI_MOTO_BANK_SHITEN_CD { get; set; }
        public string FURIKOMI_MOTO_KOUZA_SHURUI { get; set; }
        public string FURIKOMI_MOTO_KOUZA_NO { get; set; }
        public string FURIKOMI_MOTO_KOUZA_NAME { get; set; }
        public string FURIKOMI_IRAIJIN_CD { get; set; }

        public string TOUROKU_NO { get; set; }
    }
}