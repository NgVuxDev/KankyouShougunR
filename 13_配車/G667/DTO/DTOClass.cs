// $Id: DTOCls.cs 8292 2013-11-26 07:02:41Z sys_dev_22 $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Allocation.MobileJoukyouIchiran
{
    public class DTOClass
    {
        /// <summary>
        /// 配車区分
        /// </summary>
        public string HAISHA_KBN { get; set; }

        /// <summary>
        /// 連携
        /// </summary>
        public string RENKEI_KBN { get; set; }

        /// <summary>
        /// 作業日FROM
        /// </summary>
        public string SAGYOU_DATE_FROM { get; set; }

        /// <summary>
        /// 作業日TO
        /// </summary>
        public string SAGYOU_DATE_TO { get; set; }

        /// <summary>
        /// 業者CD
        /// </summary>
        public string GYOUSHA_CD { get; set; }

        /// <summary>
        /// 現場CD
        /// </summary>
        public string GENBA_CD { get; set; }

        /// <summary>
        /// 運搬業者CD
        /// </summary>
        public string UNPAN_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 車輌CD
        /// </summary>
        public string SHARYOU_CD { get; set; }

        /// <summary>
        /// 車種CD
        /// </summary>
        public string SHASHU_CD { get; set; }

        /// <summary>
        /// 運転者CD
        /// </summary>
        public string UNTENSHA_CD { get; set; }

        /// <summary>
        /// 回収状況
        /// </summary>
        public string KAISYUU_JYOUKYOU { get; set; }
    }
}
