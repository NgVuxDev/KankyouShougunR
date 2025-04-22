using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.PaperManifest.HensoSakiAnnaisho
{
    public class DTOClass
    {
        /// <summary>
        /// 検索条件  :返却日(From)
        /// </summary>
        public String RET_DATE_FROM { get; set; }
        /// <summary>
        /// 検索条件  :返却日(To)
        /// </summary>
        public String RET_DATE_TO { get; set; }       
        /// <summary>
        /// 検索条件  :排出業者CD（From)
        /// </summary>
        public String HST_GYOUSHA_CD_FROM { get; set; }
        /// <summary>
        /// 検索条件  :排出業者CD（To)
        /// </summary>
        public String HST_GYOUSHA_CD_TO { get; set; }
        /// <summary>
        /// 検索条件  :排出現場CD（From)   
        /// </summary>
        public String HST_GENBA_CD_FROM { get; set; }
        /// <summary>
        /// 検索条件  :排出現場CD（To)   
        /// </summary>
        public String HST_GENBA_CD_TO { get; set; }
        /// <summary>
        /// 検索条件  :取引先CD（From)
        /// </summary>
        public String TORIHIKISAKI_CD_FROM { get; set; }
        /// <summary>
        /// 検索条件  :取引先CD（To)
        /// </summary>
        public String TORIHIKISAKI_CD_TO { get; set; }
        /// <summary>
        /// 検索条件  :業者CD（From)   
        /// </summary>
        public String GYOUSHA_CD_FROM { get; set; }
        /// <summary>
        /// 検索条件  :業者CD（To)   
        /// </summary>
        public String GYOUSHA_CD_TO { get; set; }
        /// <summary>
        /// 検索条件  :現場CD（From)   
        /// </summary>
        public String GENBA_CD_FROM { get; set; }
        /// <summary>
        /// 検索条件  :現場CD（To)   
        /// </summary>
        public String GENBA_CD_TO { get; set; }
        /// <summary>
        /// 検索条件  :印刷区分   
        /// </summary>
        public String INSATU_KBN { get; set; }
        /// <summary>
        /// 検索条件  :出力内容区分 
        /// </summary>
        public String OUTPUT_KBN { get; set; }

        /// <summary>
        ///Header  :読込データ件数 
        /// </summary>
        public String readDataNumber { get; set; }
        /// <summary>
        ///Header  :システム情報からアラート件数 
        /// </summary>
        public String alertNumber { get; set; }

        /// <summary>
        /// 検索条件  :拠点 
        /// </summary>
        public String KYOTEN_CD { get; set; }//155770
    }
}
