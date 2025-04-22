using System.Data.SqlTypes;
namespace Shougun.Core.Common.DenpyouRenkeiIchiran
{
    internal class DTOClass
    {
        public SqlInt16 KYOTEN_CD { get; set; }
        public SqlDateTime DENPYOU_DATE_FROM { get; set; }
        public SqlDateTime DENPYOU_DATE_TO { get; set; }
        public string DENPYOU_NO { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string RENKEI_KBN { get; set; }
        public string RENKEI_NUMBER { get; set; }
        public string UKETSUKE_NUMBER { get; set; }
        public string SYSTEM_ID { get; set; }
        public string RENKEI_DENSHU_KBN_CD { get; set; }
        public string RENKEI_SYSTEM_ID { get; set; }
        public bool UKETSUKE_FLG { get; set; }
        public bool UKEIRE_FLG { get; set; }
        public bool SHUKKA_FLG { get; set; }
        public bool UR_SH_FLG { get; set; }
        public bool MANI_FLG { get; set; }
        public bool UNCHIN_FLG { get; set; }
        public bool DAINOU_FLG { get; set; }
    }
}
