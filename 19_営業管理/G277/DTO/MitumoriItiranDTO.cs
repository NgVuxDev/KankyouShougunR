using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.BusinessManagement.MitumoriIchiran
{
    public class MitumoriItiranDTO
    {
        /// <summary>
        /// 伝票番号    :   Mitsumori_number
        /// </summary>
        public String Mitsumori_number { get; set; }
        /// <summary>
        /// 伝票日付    :   Mitsumori_date
        /// </summary>
        public String Mitsumori_date { get; set; }
        /// <summary>
        /// 業者名    :   Gyousha_name
        /// </summary>
        public String Gyousha_name { get; set; }
        /// <summary>
        /// 現場名    :   Genba_name
        /// </summary>
        public String Genba_name { get; set; }
        /// <summary>
        /// 金額計    :   Kingaku_total
        /// </summary>
        public String Kingaku_total { get; set; }
        /// <summary>
        /// 外税    :   Tax_soto
        /// </summary>
        public String Tax_soto { get; set; }
        /// <summary>
        /// 内税    :   Tax_uchi
        /// </summary>
        public String Tax_uchi { get; set; }
        /// <summary>
        /// 経費計    :   Keihikei
        /// </summary>
        public String Keihikei { get; set; }
        /// <summary>
        /// 合計金額    :   Goukei_kingaku_total
        /// </summary>
        public String Goukei_kingaku_total { get; set; }
        /// <summary>
        /// 備考    :   Bikou
        /// </summary>
        public String Bikou { get; set; }
        /// <summary>
        /// 更新日付    :   Update_date
        /// </summary>
        public String Update_date { get; set; }
    }
}
