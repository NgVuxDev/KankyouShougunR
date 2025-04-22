using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.SalesPayment.TukigimeUriageDenpyoSakusei
{
    /// <summary>
    /// 検索条件DTO
    /// </summary>
    public class DTOClass
    {
        /// <summary>
        /// 検索条件  :GyoushaCD
        /// </summary>
        public String GyousyaCD { get; set; }

        /// <summary>
        /// 検索条件  :GenbaCD
        /// </summary>
        public String GenbaCD { get; set; }

        /// <summary>
        /// 検索条件  :KyotenCD
        /// </summary>
        public String KyotenCD { get; set; }

        /// <summary>
        /// 検索条件  :TorihikisakiCD
        /// </summary>
        public String TorihikisakiCD { get; set; }

        /// <summary>
        /// 検索条件  :Shimebi
        /// </summary>
        public String Shimebi { get; set; }

        /// <summary>
        /// 検索条件  :SeikyuuDate
        /// </summary>
        public DateTime SeikyuuDate { get; set; }

        /// <summary>
        /// 検索条件 : 対象期間From
        /// </summary>
        public DateTime TaishouDateFrom { get; set; }

        /// <summary>
        /// 検索条件 : 対象期間To
        /// </summary>
        public DateTime TaishouDateTo { get; set; }
    }
}
