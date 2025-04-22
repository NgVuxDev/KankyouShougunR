using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.PaperManifest.JissekiHokokuUnpan
{
    /// <summary>
    /// パラメータ
    /// </summary>
    public class ManiDTOClass
    {
        /// <summary>SYSTEM_ID</summary>
		public List<string> SYSTEM_ID { get; set; }

        /// <summary>SEQ</summary>
        public List<string> SEQ { get; set; }

        /// <summary>DETAIL_SYSTEM_ID</summary>
        public List<int> DETAIL_SYSTEM_ID { get; set; }

        /// <summary>KANRI_ID</summary>
        public List<string> KANRI_ID { get; set; }

        /// <summary>DEN_SEQ</summary>
        public List<string> DEN_SEQ { get; set; }

        /// <summary>MANIFEST_ID</summary>
        public List<string> MANIFEST_ID { get; set; }

        /// <summary>HAIKI_KBN_CD</summary>
        public List<short> HAIKI_KBN_CD { get; set; }

        /// <summary>routeNoList</summary>
        public List<string> routeNoList { get; set; }

        /// <summary>routeNoList</summary>
        public List<SqlInt32> JYUTAKU_JISHA_KBN { get; set; }

        /// <summary>upnLis</summary>
        public  List<T_JISSEKI_HOUKOKU_UPN_DETAIL> upnList { get; set; }

        public ManiDTOClass()
        {
            SYSTEM_ID = new List<string>();
            SEQ = new List<string>();
            DETAIL_SYSTEM_ID = new List<int>();
            KANRI_ID = new List<string>();
            DEN_SEQ = new List<string>();
            MANIFEST_ID = new List<string>();
            HAIKI_KBN_CD = new List<short>();
            routeNoList = new List<string>();
            JYUTAKU_JISHA_KBN = new List<SqlInt32>();
            upnList = new List<T_JISSEKI_HOUKOKU_UPN_DETAIL>();
        }
    }
}
