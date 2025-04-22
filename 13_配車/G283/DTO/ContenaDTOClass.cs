using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.MobileShougunTorikomi.DTO
{
    public class ContenaDTOClass
    {
        /// <summary>
        /// コンテナ種類CD
        /// </summary>
        public string CONTENA_SHURUI_CD { get; set; }

        /// <summary>
        /// コンテナCD
        /// </summary>
        public string CONTENA_CD { get; set; }

        /// <summary>
        /// 現場CD
        /// </summary>
        public string GENBA_CD { get; set; }

        /// <summary>
        /// 拠点CD
        /// </summary>
        public SqlInt16 KYOTEN_CD { get; set; }

        /// <summary>
        /// 設置日
        /// </summary>
        public SqlDateTime SECCHI_DATE { get; set; }

        /// <summary>
        /// 状況区分
        /// </summary>
        public SqlInt16 JOUKYOU_KBN { get; set; }

        /// <summary>
        /// 最終更新者
        /// </summary>
        public string UPDATE_USER { get; set; }

        /// <summary>
        /// 最終更新日時
        /// </summary>
        public SqlDateTime UPDATE_DATE { get; set; }

        /// <summary>
        /// 最終更新PC
        /// </summary>
        public string UPDATE_PC { get; set; }
    }
}
