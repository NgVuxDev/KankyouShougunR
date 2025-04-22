using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku
{
    public class DTOClass
    {
        /// <summary>
        /// 検索条件  :入金番号
        /// </summary>
        public String Nyukin_number { get; set; }

        /// <summary>
        /// 検索条件  :取引先CD
        /// </summary>
        public String Torihikisaki_cd { get; set; }

        /// <summary>
        /// 検索条件  :伝票日付
        /// </summary>
        public DateTime Denpyou_Date { get; set; }

        /// <summary>
        /// 検索条件  :システムID
        /// </summary>
        public String System_Id { get; set; }

        /// <summary>
        /// 検索条件  :システムID
        /// </summary>
        public String Kesikomi_Seq { get; set; }

        /// <summary>
        /// 検索条件  :拠点CD
        /// </summary>
        public String Kyoten_cd { get; set; } 

        /// <summary>
        /// 検索条件  :社員CD
        /// </summary>
        public String Shain_cd { get; set; }

        /// <summary>
        /// 検索条件  :入出金CD
        /// </summary>
        public String NyuushukinKbn { get; set; } 
    }
}
