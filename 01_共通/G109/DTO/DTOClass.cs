using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Billing.AtenaLabel
{
    public class DTOClass
    {
        /// <summary>
        /// 印刷方法
        /// </summary>
        public string printHouhou { get; set; }

        /// <summary>
        /// 印刷区分
        /// </summary>
        public string printKubun { get; set; }

        /// <summary>
        /// 取引先
        /// </summary>
        public string TorihikisakiCd { get; set; }

        /// <summary>
        /// 業者
        /// </summary>
        public string GyoushaCd { get; set; }

        /// <summary>
        /// 現場
        /// </summary>
        public string GenbaCd { get; set; }

        /// <summary>
        /// 個別指定
        /// </summary>
        public string kobetsuShitei { get; set; }
    }
}
