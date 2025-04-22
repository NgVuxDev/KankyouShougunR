using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Allocation.KaraContenaIchiranHyou
{
    public class DTOClass
    {
        /// <summary>基準日</summary>
        public DateTime kijunbi { get; set; }

        /// <summary>受注・配車含む(1:含む、2:含まない)</summary>
        public string juchuHaishaHukumu {get; set;}

        /// <summary>コンテナ種類CDFrom</summary>
        public string contenaChuruiCdStart { get; set; }

        /// <summary>コンテナ種類CDTo</summary>
        public string contenaChuruiCdEnd { get; set; }
    }
}
