using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Carriage.UntinSyuusyuuhyoPopup
{
    
    public class DtoCls
    {
        
            /// <summary>
            /// 検索条件  :拠点CD
            /// </summary>
            public String KYOTEN_CD { get; set; }
            /// <summary>
            /// 検索条件  :伝票日付(From)
            /// </summary>
            public String DENPYOU_DATE_FROM { get; set; }
            /// <summary>
            /// 検索条件  :伝票日付(To)
            /// </summary>
            public String DENPYOU_DATE_TO { get; set; }
            /// <summary>
            /// 検索条件  :運搬業者CD（From)
            /// </summary>
            public String UNPAN_GYOUSHA_CD_FROM { get; set; }
            /// <summary>
            /// 検索条件  :運搬業者CD（To)
            /// </summary>
            public String UNPAN_GYOUSHA_CD_TO { get; set; }
            /// <summary>
            /// 検索条件  :荷積業者CD（From)
            /// </summary>
            public String NIZUMI_GYOUSHA_CD_FROM { get; set; }
            /// <summary>
            /// 検索条件  :荷積業者CD（To)
            /// </summary>
            public String NIZUMI_GYOUSHA_CD_TO { get; set; }
            /// <summary>
            /// 検索条件  :荷積現場CD（From)   
            /// </summary>
            public String NIZUMI_GENBA_CD_FROM { get; set; }
            /// <summary>
            /// 検索条件  :荷積現場CD（To)   
            /// </summary>
            public String NIZUMI_GENBA_CD_TO { get; set; }
            /// <summary>
            /// 検索条件  :荷降業者CD（From)   
            /// </summary>
            public String NIOROSHI_GYOUSHA_CD_FROM { get; set; }
            /// <summary>
            /// 検索条件  :荷降業者CD（To)   
            /// </summary>
            public String NIOROSHI_GYOUSHA_CD_TO { get; set; }

            /// <summary>
            /// 検索条件  :荷降現場CD（From)   
            /// </summary>
            public String NIOROSHI_GENBA_CD_FROM { get; set; }
            /// <summary>
            /// 検索条件  :荷降現場CD（To)   
            /// </summary>
            public String NIOROSHI_GENBA_CD_TO { get; set; }
            /// <summary>
            ///Header  :読込データ件数 
            /// </summary>
            public String readDataNumber { get; set; }
            /// <summary>
            ///Header  :システム情報からアラート件数 
            /// </summary>
            public String alertNumber { get; set; }
        }   
}
