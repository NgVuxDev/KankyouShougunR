// $Id: ItakuKeiyakuHoshuDto.cs 39662 2015-01-15 09:45:59Z wenjw@oec-h.com $
using System.Collections.Generic;
using r_framework.Entity;

namespace ItakuKeiyakuHoshu.Dto
{
    public struct GYOSHA_KYOKA
    {
        public string GYOUSHA_CD;
        public string GYOUSHA_NAME;
        public string GYOUSHA_ADDRESS;
        public string UNPAN_KYOKA_NO;
        public string SHOBN_KYOKA_NO;
        public int DAISU;
    }

    public class ItakuKeiyakuHoshuDto
    {
        public ItakuKeiyakuHoshuDto()
        {
            this.ItakuKeiyakuKihon = new M_ITAKU_KEIYAKU_KIHON();
        }

        public M_ITAKU_KEIYAKU_KIHON ItakuKeiyakuKihon { get; set; }

        public M_ITAKU_KEIYAKU_KIHON_HST_GENBA[] itakuKeiyakuKihonHstGenba { get; set; }

        public M_ITAKU_KEIYAKU_BETSU1_HST[] itakuKeiyakuBetsu1Hst { get; set; }

        public M_ITAKU_KEIYAKU_HINMEI[] itakuKeiyakuHinmei { get; set; }

        public M_ITAKU_KEIYAKU_BETSU1_YOTEI[] itakuKeiyakuBetsu1Yotei { get; set; }

        public M_ITAKU_KEIYAKU_BETSU2[] itakuKeiyakuBetsu2 { get; set; }

        public M_ITAKU_KEIYAKU_BETSU3[] itakuKeiyakuBetsu3 { get; set; }

        public M_ITAKU_KEIYAKU_BETSU4[] itakuKeiyakuBetsu4 { get; set; }

        public M_ITAKU_KEIYAKU_TSUMIKAE[] itakuKeiyakuTsumikae { get; set; }

        public M_ITAKU_KEIYAKU_SAISEIHINM[] itakuKeiyakuSaiseihinm { get; set; }

        public M_ITAKU_KEIYAKU_OBOE[] itakuKeiyakuOboe { get; set; }

        public M_ITAKU_UPN_KYOKASHO[] itakuUpnKyokasho { get; set; }

        public M_ITAKU_SBN_KYOKASHO[] itakuSbnKyokasho { get; set; }

        public M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI[] itakuKeiyakuDenshiSouhusaki { get; set; }

    }
}
