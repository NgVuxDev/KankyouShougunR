using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Master.BankIkkatsu
{
    public class DTO_Bank : SuperEntity
    {
        /// <summary>
        /// 銀行CD
        /// </summary>
        public String BankCd { get; set; }

        /// <summary>
        /// 銀行支店CD
        /// </summary>
        public String BankShitenCd { get; set; }

        /// <summary>
        /// 口座種類
        /// </summary>
        public String KouzaShurui { get; set; }

        /// <summary>
        /// 口座番号
        /// </summary>
        public String KouzaNo { get; set; }
    }
    public class DTO_Torihikisaki : DTO_Bank
    {
        /// <summary>
        /// 取引先CD
        /// </summary>
        public String TorihiksakiCd { get; set; }

        /// <summary>
        /// 口座名義
        /// </summary>
        public String KouzaName { get; set; }
    }
}
