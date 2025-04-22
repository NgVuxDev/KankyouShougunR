using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.MobileShougunShutsuryoku
{
    public class MobileShougunShutsuryokuDTOClass
    {
		/// <summary>
        /// 拠点マスタ／拠点コード
        /// </summary>
        public SqlInt16 KYOTEN_CD { get; set; }

        /// <summary>
        /// 業者マスタ／業者コード
        /// </summary>
        public String GYOUSHA_CD { get; set; }

        /// <summary>
        /// 定期配車／拠点コード
        /// </summary>
        public SqlInt16 KYOTEN_CD_TEIKI { get; set; }

        /// <summary>
        /// 定期配車／作業開始日
        /// </summary>
        public String SAGYOU_DATE_TEIKI_FROM { get; set; }

        /// <summary>
        /// 定期配車／作業終了日
        /// </summary>
        public String SAGYOU_DATE_TEIKI_TO { get; set; }

        /// <summary>
        /// スポット配車（収集）／拠点コード
        /// </summary>
        public SqlInt16 KYOTEN_CD_SPOT_SS { get; set; }

        /// <summary>
        /// スポット配車（収集）／作業開始日
        /// </summary>
        public String SAGYOU_DATE_SPOT_SS_FROM { get; set; }

        /// <summary>
        /// スポット配車（収集）／作業終了日
        /// </summary>
        public String SAGYOU_DATE_SPOT_SS_TO { get; set; }

        /// <summary>
        /// スポット配車（収集）／システムID
        /// </summary>
        public String SYSTEM_ID { get; set; }

        /// <summary>
        /// スポット配車（収集）／枝番
        /// </summary>
        public SqlInt16 SEQ { get; set; }

		/// <summary>
		/// スポット配車（収集）／配車状況CD
		/// </summary>
		public SqlInt16 HAISHA_JOKYO_CD_SPOT_SS { get; set; }
    }
}
