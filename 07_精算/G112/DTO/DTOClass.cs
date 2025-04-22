using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Adjustment.Shiharaiichiran
{
    public class DTOClass
    {
        /// <summary>
        /// 画面初期表示：拠点CD
        /// </summary>
        public string InitKyotenCd { get; set; }

        /// <summary>
        /// 画面初期表示：取引先CD
        /// </summary>
        public string InitTorihiksiakiCd { get; set; }

        /// <summary>
        /// 画面初期表示：伝票日付
        /// </summary>
        public string InitDenpyouHiduke { get; set; }
    }

    //PhuocLoc 2021/05/14 #148575 -Start
    public class ShiharaiDeleteDto
    {
        public long ShiharaiNumber { get; set; }
        public string TorihikisakiCd { get; set; }
    }
    //PhuocLoc 2021/05/14 #148575 -End
}
