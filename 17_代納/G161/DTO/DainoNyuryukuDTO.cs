using System;
using System.Data.SqlTypes;
using r_framework.Entity;

namespace Shougun.Core.PayByProxy.DainoNyuryuku
{
    public class DainoNyuryukuDTO
    {
        // 伝票番号
        public SqlInt64 UR_SH_NUMBER { get; set; }

        // 伝票区分
        public SqlInt16 DENPYOU_KBN_CD { get; set; }

        // 受入
        public string UKEIRE_HINMEI_CD { get; set; }
        public string UKEIRE_HINMEI_NAME { get; set; }
        public Int16  UKEIRE_DENPYOU_KBN_CD { get; set; }
        public string UKEIRE_DENPYOU_KBN_NAME { get; set; }
        public Int16  UKEIRE_UNIT_CD { get; set; }
        public decimal UKEIRE_TANKA { get; set; }

        public Int16   ROW_NO { get; set; }
        public decimal UKEIRE_STACK_JYUURYOU { get; set; }
        public decimal UKEIRE_CHOUSEI_JYUURYOU { get; set; }
        public decimal UKEIRE_NET_JYUURYOU { get; set; }
        public decimal UKEIRE_SUURYOU { get; set; }
        public string UKEIRE_UNIT_NAME { get; set; }
        public decimal UKEIRE_KINGAKU { get; set; }
        public string UKEIRE_MEISAI_BIKOU { get; set; }

        // 出荷
        public string SHUKKA_HINMEI_CD { get; set; }
        public string SHUKKA_HINMEI_NAME { get; set; }
        public Int16  SHUKKA_DENPYOU_KBN_CD { get; set; }
        public string SHUKKA_DENPYOU_KBN_NAME { get; set; }
        public Int16  SHUKKA_UNIT_CD { get; set; }
        public decimal SHUKKA_TANKA { get; set; }

        public decimal SHUKKA_STACK_JYUURYOU { get; set; }
        public decimal SHUKKA_CHOUSEI_JYUURYOU { get; set; }
        public decimal SHUKKA_NET_JYUURYOU { get; set; }
        public decimal SHUKKA_SUURYOU { get; set; }
        public string SHUKKA_UNIT_NAME { get; set; }
        public decimal SHUKKA_KINGAKU { get; set; }
        public string SHUKKA_MEISAI_BIKOU { get; set; }
    }
}
