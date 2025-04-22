using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.PayByProxy.ShukeiHyoJokenShiteiPoppup
{
    public class ShukeiHyoJokenDTO
    {
        public string M_KYOTEN_CD { get; set; }
        public string M_KYOTEN_NAME { get; set; }

        public DateTime DAINOU_ENTRY_DENPYOU_DATE_FROM { get; set; }
        public DateTime DAINOU_ENTRY_DENPYOU_DATE_TO { get; set; }

        public string UKEIRE_ENTRY_TORIHIKISAKI_CD_FROM { get; set; }
        public string UKEIRE_ENTRY_TORIHIKISAKI_NAME_FROM { get; set; }
        public string UKEIRE_ENTRY_TORIHIKISAKI_CD_TO { get; set; }
        public string UKEIRE_ENTRY_TORIHIKISAKI_NAME_TO { get; set; }

        public string SHUKKA_ENTRY_TORIHIKISAKI_CD_FROM { get; set; }
        public string SHUKKA_ENTRY_TORIHIKISAKI_NAME_FROM { get; set; }
        public string SHUKKA_ENTRY_TORIHIKISAKI_CD_TO { get; set; }
        public string SHUKKA_ENTRY_TORIHIKISAKI_NAME_TO { get; set; }

        public string UKEIRE_ENTRY_GYOUSHA_CD_FROM { get; set; }
        public string UKEIRE_ENTRY_GYOUSHA_NAME_FROM { get; set; }
        public string UKEIRE_ENTRY_GYOUSHA_CD_TO { get; set; }
        public string UKEIRE_ENTRY_GYOUSHA_NAME_TO { get; set; }

        public string SHUKKA_ENTRY_GYOUSHA_CD_FROM { get; set; }
        public string SHUKKA_ENTRY_GYOUSHA_NAME_FROM { get; set; }
        public string SHUKKA_ENTRY_GYOUSHA_CD_TO { get; set; }
        public string SHUKKA_ENTRY_GYOUSHA_NAME_TO { get; set; }


        public string UKEIRE_ENTRY_GENBA_CD_FROM { get; set; }
        public string UKEIRE_ENTRY_GENBA_NAME_FROM{ get; set; }
        public string UKEIRE_ENTRY_GENBA_CD_TO { get; set; }
        public string UKEIRE_ENTRY_GENBA_NAME_TO { get; set; }

        public string SHUKKA_ENTRY_GENBA_CD_FROM { get; set; }
        public string SHUKKA_ENTRY_GENBA_NAME_FROM { get; set; }
        public string SHUKKA_ENTRY_GENBA_CD_TO { get; set; }
        public string SHUKKA_ENTRY_GENBA_NAME_TO { get; set; }

        public string UKEIRE_DETAIL_HINMEI_CD_FROM { get; set; }
        public string UKEIRE_DETAIL_HINMEI_NAME_FROM { get; set; }
        public string UKEIRE_DETAIL_HINMEI_CD_TO { get; set; }
        public string UKEIRE_DETAIL_HINMEI_NAME_TO { get; set; }

        public string SHUKKA_DETAIL_HINMEI_CD_FROM { get; set; }
        public string SHUKKA_DETAIL_HINMEI_NAME_FROM { get; set; }
        public string SHUKKA_DETAIL_HINMEI_CD_TO { get; set; }
        public string SHUKKA_DETAIL_HINMEI_NAME_TO { get; set; }

        public string UNCHIN_ENTRY_UNPAN_GYOUSHA_CD_FROM { get; set; }
        public string UNCHIN_ENTRY_UNPAN_GYOUSHA_NAME_FROM { get; set; }
        public string UNCHIN_ENTRY_UNPAN_GYOUSHA_CD_TO { get; set; }
        public string UNCHIN_ENTRY_UNPAN_GYOUSHA_NAME_TO { get; set; }
    }
}
