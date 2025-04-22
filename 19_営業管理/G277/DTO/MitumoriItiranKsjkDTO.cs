using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.BusinessManagement.MitumoriIchiran
{
    public class MitumoriItiranKsjkDTO
    {
        /// <summary>
        /// 拠点CD    :   Kyoten_cd
        /// </summary>
        public String Kyoten_cd { get; set; }
        /// <summary>
        /// 見積FROM日付    :   Mitsumori_Fdate
        /// </summary>
        public String Mitsumori_Fdate { get; set; }
        /// <summary>
        /// 見積TO日付    :   Mitsumori_Tdate
        /// </summary>
        public String Mitsumori_Tdate { get; set; }
        /// <summary>
        /// 最終更新FROM日時    :   Update_Fdate
        /// </summary>
        public String Update_Fdate { get; set; }
        /// <summary>
        /// 最終更新TO日時    :   Update_Tdate
        /// </summary>
        public String Update_Tdate { get; set; }
        /// <summary>
        /// 営業者CD    :   Shain_cd
        /// </summary>
        public String Shain_cd { get; set; }
        /// <summary>
        /// 状況    :   Jokyo_flg
        /// </summary>
        public String Jokyo_flg { get; set; }
        /// <summary>
        /// 取引先CD    :   Torihikisaki_cd
        /// </summary>
        public String Torihikisaki_cd { get; set; }
        /// <summary>
        /// 業者CD    :   Gyousha_cd
        /// </summary>
        public String Gyousha_cd { get; set; }
        /// <summary>
        /// 現場CD    :   Genba_cd
        /// </summary>
        public String Genba_cd { get; set; }
        /// <summary>
        /// 引合取引先フラグ    :   Hikiai_torihikisaki_flg
        /// </summary>
        public String Hikiai_torihikisaki_flg { get; set; }
        /// <summary>
        /// 引合業者フラグ    :   Hikiai_gyousha_flg
        /// </summary>
        public String Hikiai_gyousha_flg { get; set; }
        /// <summary>
        /// 引合現場フラグ    :   Hikiai_genba_flg
        /// </summary>
        public String Hikiai_genba_flg { get; set; }
    }
}
