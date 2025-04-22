using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Allocation.Teikihaisyajissekihyou
{
    public class TeikihaisyajissekihyouDTOClass
    {
        /// <summary>
        /// 拠点
        /// </summary>
        public String KyotenCD { get; set; }

        /// <summary>
        /// 出力区分
        /// </summary>
        public String SYUTSURYOKUKUBUN { get; set; }
        
        /// <summary>
        /// 期間From
        /// </summary>
        public String DENPYOU_DATE_FROM { get; set; }
        
        /// <summary>
        /// 期間To
        /// </summary>
        public String dtp_KikanTO { get; set; }

        /// <summary>
        /// 取引先CDFromを取得・設定します
        /// </summary>
        public string TORIHIKISAKI_CD_FROM { get; set; }

        /// <summary>
        /// 取引先CDToを取得・設定します
        /// </summary>
        public string TORIHIKISAKI_CD_TO { get; set; }

        /// <summary>
        /// 業者ＣＤ_From
        /// </summary>
        public String GYOUSHA_CD_FROM { get; set; }

        /// </summary> 
        /// 業者ＣＤ_To
        /// </summary>
        public String GYOUSHA_CD_TO { get; set; }

        /// <summary>
        /// 業者名From
        /// </summary>
        public String GYOUSHA_NAME_RYAKU_FROM { get; set; }

        /// <summary>
        /// 業者名To
        /// </summary>
        public String GYOUSHA_NAME_RYAKU_TO { get; set; }

        /// <summary>
        /// 現場ＣＤ_From
        /// </summary>
        public String GENBA_CD_FROM { get; set; }

        /// <summary>
        /// 現場ＣＤ_To
        /// </summary>
        public String GENBA_CD_TO { get; set; }

        /// <summary>
        /// 現場名From
        /// </summary>
        public String GENBA_NAME_RYAKU_FROM { get; set; }

        /// <summary>
        /// 現場名To
        /// </summary>
        public String GENBA_NAME_RYAKU_TO { get; set; }

        /// <summary>
        /// 種類CDFrom
        /// </summary>
        public string SHURUI_CD_FROM { get; set; }

        /// <summary>
        /// 種類CDTo
        /// </summary>
        public string SHURUI_CD_TO { get; set; }

        /// <summary>
        /// 年
        /// </summary>
        public String YEAR { get; set; }

		/// <summary>
		/// 集計対象数量
		/// </summary>
		public int SHUUKEISUURYOU { get; set; }
	}
}
