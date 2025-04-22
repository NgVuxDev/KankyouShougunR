using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.SalesPayment.HannyushutsuIchiran
{
    public class DTOClass
    {
        /// <summary>
        /// 検索条件  :拠点CD
        /// </summary>
        public String KyotenCd { get; set; }

        /// <summary>
        /// 検索条件  :作業日FROM
        /// </summary>
        public String SagyouDateFrom { get; set; }

        /// <summary>
        /// 検索条件  :作業日TO
        /// </summary>
        public String SagyouDateTo { get; set; }

        /// <summary>
        /// 検索条件  :車輌CD
        /// </summary>
        public String SharyouCd { get; set; }

        /// <summary>
        /// 検索条件  :搬出種別
        /// </summary>
        public String HanshutsuShubetsu { get; set; }

        /// <summary>
        /// 検索条件  :取引先CD
        /// </summary>
        public String TorihikisakiCd { get; set; }

        /// <summary>
        /// 検索条件  :業者CD
        /// </summary>
        public String GyoushaCd { get; set; }

        /// <summary>
        /// 検索条件  :現場CD
        /// </summary>
        public String GenbaCd { get; set; }

        /// <summary>
        /// 検索条件  :荷降業者CD
        /// </summary>
        public String NioroshiGyoushaCd { get; set; }

        /// <summary>
        /// 検索条件  :荷降現場CD
        /// </summary>
        public String NioroshiGenbaCd { get; set; }

    }
}
