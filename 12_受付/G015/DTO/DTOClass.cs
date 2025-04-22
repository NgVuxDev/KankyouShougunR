// $Id: DTOClass.cs 6372 2013-11-08 10:26:25Z sys_dev_18 $

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Reception.UketsukeSyuusyuuNyuuryoku
{
    public class DTOClass
    {
        /// <summary>
        /// 受付番号
        /// </summary>
        public long UketsukeNumber { get; set; }

        /// <summary>
        /// システムID
        /// </summary>
        public long SystemID { get; set; }

        /// <summary>
        /// シーケンス番号
        /// </summary>
        public int SEQ { get; set; }

        /// <summary>
        /// コンテナ設定区分
        /// </summary>
        public int ContenaSetKbn { get; set; }

        /// <summary>
        /// 作業日FROM
        /// </summary>
        public string SAGYOU_DATE_FROM { get; set; }

        /// <summary>
        /// 作業日TO
        /// </summary>
        public string SAGYOU_DATE_TO { get; set; }

        /// <summary>
        /// モバイル連携用：受付伝票番号
        /// </summary>
        public string Renkei_UketsukeNumber { get; set; }
    }
}
