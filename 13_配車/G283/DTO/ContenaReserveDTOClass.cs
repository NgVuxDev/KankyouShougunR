using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Allocation.MobileShougunTorikomi.DTO
{
    public class ContenaReserveDTOClass
    {
        /// <summary>
        /// 設置引揚区分
        /// </summary>
        public Int16 CONTENA_SET_KBN { get; set; }

        /// <summary>
        /// コンテナ種類CD
        /// </summary>
        public string CONTENA_SHURUI_CD { get; set; }

        /// <summary>
        /// コンテナCD
        /// </summary>
        public string CONTENA_CD { get; set; }

    }
}
