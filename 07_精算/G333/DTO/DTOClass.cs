using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Adjustment.Shiharaijimesyorierrorichiran
{
    /// <summary>
    /// 締処理エラーDTO
    /// </summary>
    internal class DTOClass
    {
        /// <summary>
        /// 検索条件  :CheckName
        /// </summary>
        public String CheckName { get; set; }

        /// <summary>
        /// 検索条件  :KyotenName
        /// </summary>
        public String KyotenName { get; set; }

        /// <summary>
        /// 検索条件  :DenpyoDate
        /// </summary>
        public String DenpyoDate { get; set; }

        /// <summary>
        /// 検索条件  :DenpyoNumber
        /// </summary>
        public String DenpyoNumber { get; set; }

        /// <summary>
        /// 検索条件  :ErrorNaiyou
        /// </summary>
        public String ErrorNaiyou { get; set; }

        /// <summary>
        /// 検索条件  :Riyuu
        /// </summary>
        public String Riyuu { get; set; }

        /// <summary>
        /// 検索条件  :TorihikisakiCD
        /// </summary>
        public String TorihikisakiCD { get; set; }

        /// <summary>
        /// 検索条件  :TorihikisakiName
        /// </summary>
        public String TorihikisakiName { get; set; }

        /// <summary>
        /// 検索条件  :GyoNumber
        /// </summary>
        public String GyoNumber { get; set; }
    }
}
