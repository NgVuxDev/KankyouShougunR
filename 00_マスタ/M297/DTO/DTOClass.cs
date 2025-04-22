using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Master.EigyoTantoushaIkkatsu
{
    public class DTO_EigyouTantou : SuperEntity
    {
        /// <summary>
        /// 営業担当部署CD
        /// </summary>
        public String EigyouTantouBushoCd { get; set; }

        /// <summary>
        /// 営業担当者CD
        /// </summary>
        public String EigyouTantouCd { get; set; }

        //MOD NHU 20211006 #155767 S
        public string ToriCd { get; set; }
        public string ToriName { get; set; }
        public string GyoushaCd { get; set; }
        public string GyoushaName { get; set; }
        public string GenbaCd { get; set; }
        public string GenbaName { get; set; }

        public string Address { get; set; }
        public string Tel { get; set; }
        public string BushoCdBf { get; set; }
        public string BushoNameBf { get; set; }
        public string TantoushaCdBf { get; set; }
        //MOD NHU 20211006 #155767 E
    }
    public class DTO_M_TORIHIKISAKI : DTO_EigyouTantou
    {
        /// <summary>
        /// 取引先CD
        /// </summary>
        public String TorihiksakiCd { get; set; }
    }
    public class DTO_M_GYOUSHA : DTO_EigyouTantou
    {
        /// <summary>
        /// 業者CD
        /// </summary>
        public String GyoushaCd { get; set; }
    }
    public class DTO_M_GENBA : DTO_EigyouTantou
    {
        /// <summary>
        /// 業者CD
        /// </summary>
        public String GyoushaCd { get; set; }
        /// <summary>
        /// 現場CD
        /// </summary>
        public String GenbaCd { get; set; }
    }
}
