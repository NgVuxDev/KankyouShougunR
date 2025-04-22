using System;

namespace Shougun.Core.Scale.KeiryouNyuuryoku
{
    /// <summary>
    /// 計量入力滞留DTO
    /// </summary>
    public class TairyuuDTOClass
    {
        /// <summary>
        /// kyotenCd
        /// </summary>
        public Int16 kyotenCd { get; set; }

        /// <summary>
        /// upnGyoushaCd
        /// </summary>
        public string upnGyoushaCd { get; set; }

        /// <summary>
        /// sharyouCd
        /// </summary>
        public string sharyouCd { get; set; }
    }
}