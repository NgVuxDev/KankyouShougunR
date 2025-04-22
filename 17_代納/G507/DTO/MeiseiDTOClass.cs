using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.PayByProxy.DainoDenpyoHakkou.DTO
{
    public class MeiseiDTOClass
    {
        /// <summary>
        ///	確定区分	:	Kakutei_Kbn	
        /// </summary>		
        public String Kakutei_Kbn { get; set; }
        /// <summary>
        ///	売上/支払区分	:	Uriageshiharai_Kbn	
        /// </summary>		
        public String Uriageshiharai_Kbn { get; set; }
        /// <summary>
        ///	品名CD	:	Hinmei_Cd
        /// </summary>		
        public String Hinmei_Cd { get; set; }
        /// <summary>
        ///	金額（税ぬき）	:	Kingaku	
        /// </summary>		
        public String Kingaku { get; set; }

        /// <summary>
        ///		　消費税外税	:	Tax_Soto	
        /// </summary>		
        public String Tax_Soto { get; set; }
        /// <summary>
        ///		　消費税内税	:	Tax_Uchi	
        /// </summary>		
        public String Tax_Uchi { get; set; }
        /// <summary>
        ///		　品名別税区分CD	:	Hinmei_Cd	
        /// </summary>		
        public String Hinmei_Zei_Kbn_Cd { get; set; }
        /// <summary>
        ///		　品名別税消費税外税	
        /// </summary>		
        public String Hinmei_Tax_Soto { get; set; }
        /// <summary>
        ///		　品名別税消費税内税	:	Hinmei_Tax_Uchi	
        /// </summary>		
        public String Hinmei_Tax_Uchi { get; set; }
    }
}
