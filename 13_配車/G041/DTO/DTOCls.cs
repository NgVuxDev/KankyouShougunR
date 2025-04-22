// $Id: DTOCls.cs 21477 2014-05-27 01:55:33Z takeda $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.ContenaIchiran
{
    public class DTOCls
    {
        /// <summary>
        /// 部署CD
        /// </summary>
        public string BUSHO_CD { get; set; }

        /// <summary>
        /// 業者CD
        /// </summary>
        public string GYOUSHA_CD { get; set; }

        /// <summary>
        /// コンテナ種類CD
        /// </summary>
        public string CONTENA_SHURUI_CD { get; set; }

        /// <summary>
        /// 現場CD
        /// </summary>
        public string GENBA_CD { get; set; }

        /// <summary>
        /// コンテナCD
        /// </summary>
        public string CONTENA_CD { get; set; }

        /// <summary>
        /// 営業担当者CD
        /// </summary>
        public string EIGYOU_TANTOU_CD { get; set; }

        /// <summary>
        /// 設置日
        /// </summary>
        public string SECCHI_DATE { get; set; }

        /// <summary>
        /// 経過日数
        /// </summary>
        public Int16 ELAPSED_DAYS { get; set; }

        /// <summary>
        /// システム設定ファイルの日数
        /// </summary>
        public SqlInt16 SYS_DAYS_COUNT { get; set; }
    }
}
