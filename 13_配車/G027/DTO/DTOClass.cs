using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Allocation.SagyoubiHenkou
{
    public class DTO_Haisha : SuperEntity
    {
        /// <summary>
        /// 拠点CD
        /// </summary>
        public String KyotenCd { get; set; }

        /// <summary>
        /// 作業日
        /// </summary>
        public String SagyouDate { get; set; }

        /// <summary>
        /// 車種CD
        /// </summary>
        public String ShashuCd { get; set; }

        /// <summary>
        /// 社員CD
        /// </summary>
        public String ShainCd { get; set; }

        /// <summary>
        /// 車両CD
        /// </summary>
        public String SharyouCd { get; set; }

        /// <summary>
        /// 業者CD
        /// </summary>
        public string GyoushaCd { get; set; }

        /// <summary>
        /// 配車入区分
        /// </summary>
        public bool HaisyaKubun { get; set; }

        /// <summary>
        /// 運転者CD
        /// </summary>
        public String UntenshaCd { get; set; }
    }
    public class DTO_IdSeq : SuperEntity
    {
        /// <summary>
        /// システムID
        /// </summary>
        public Int64 SystemId { get; set; }

        /// <summary>
        /// 枝番
        /// </summary>
        public int Seq { get; set; }

        /// <summary>
        /// 作業日
        /// </summary>
        public String SagyouDate { get; set; }

        /// <summary>
        /// 車種CD
        /// </summary>
        public String ShashuCd { get; set; }

        /// <summary>
        /// 社員CD
        /// </summary>
        public String ShainCd { get; set; }

        /// <summary>
        /// 車両CD
        /// </summary>
        public String SharyouCd { get; set; }

        /// <summary>
        /// 業者CD
        /// </summary>
        public string GyoushaCd { get; set; }
    }
}
