using System;
using System.Collections;
using System.Data.SqlTypes;
namespace Shougun.Core.PaperManifest.ManifestSyuuryoubiIchiran
{
   
    //マニフェスト
    public class TMEDtoCls
    {
        /// <summary>
        /// 検索条件  :システムID
        /// </summary>
        public String SYSTEM_ID { get; set; }

        /// <summary>
        /// 検索条件  :枝番
        /// </summary>
        public String SEQ { get; set; }

        /// <summary>
        /// 検索条件  :経過日数 運搬
        /// </summary>
        public SqlInt16 sys_nitsusuu_upn { get; set; }

        /// <summary>
        /// 検索条件  :経過日数　処分
        /// </summary>
        public SqlInt16 sys_nitsusuu_sbu { get; set; }

        /// <summary>
        /// 検索条件  :経過日数　特管処分
        /// </summary>
        public SqlInt16 sys_nitsusuu_tk_sbu { get; set; }

        /// <summary>
        /// 検索条件  :経過日数　最終処分
        /// </summary>
        public SqlInt16 sys_nitsusuu_last_sbu { get; set; }
        
    }


}
