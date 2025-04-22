using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.SalesPayment.DenpyouHakou
{
    public class MeiseiDTOClass
    {
        /// <summary>
        ///		　確定区分	:	Kakutei_Kbn	
        /// </summary>		
        public String Kakutei_Kbn { get; set; }
        /// <summary>
        ///		　売上/支払区分	:	Uriageshiharai_Kbn	
        /// </summary>		
        public String Uriageshiharai_Kbn { get; set; }
        /// <summary>
        ///		　品名CD	:	Hinmei_Cd
        /// </summary>		
        public String Hinmei_Cd { get; set; }
        /// <summary>
        ///		　金額（税ぬき）	:	Kingaku	
        /// </summary>		
        public String Kingaku { get; set; }

        /// <summary>
        ///		　税区分	:	ZeiKbn	
        /// </summary>		
        public String ZeiKbn { get; set; }
    }
}
