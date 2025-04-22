using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Common.ContenaShitei
{
    //コンテナ指定一覧
    public class CNTSHIDtoCls
    {
        /// <summary>
        /// 検索条件  :コンテナ種類CD
        /// </summary>
        public String CONTENA_SHURUI_CD { get; set; }

        /// <summary>
        /// 検索条件  :コンテナ種類名
        /// </summary>
        public String CONTENA_SHURUI_NAME_RYAKU { get; set; }

        /// <summary>
        /// 検索条件  :コンテナCD
        /// </summary>
        public String CONTENA_CD { get; set; }

        /// <summary>
        /// 検索条件  :コンテナ名    
        /// </summary>
        public String CONTENA_NAME_RYAKU { get; set; }

        /// <summary>
        /// 検索条件  :台数
        /// </summary>
        public String DAISUU_CNT { get; set; }

        /// <summary>
        /// 検索条件  :削除フラグ
        /// </summary>
        public String DELETE_FLG { get; set; }

        /// <summary>
        /// TIME_STAMP
        /// </summary>
        public String TIME_STAMP { get; set; }

    }
}
