using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Function.ShougunCSCommon.Dto
{
    /// <summary>
    /// 入出金用
    /// </summary>
    public class NyuuShukkinDTOClass
    {
        /**
         * 入金系
         */
        /// <summary>T_NYUUKIN_SUM_ENTRY</summary>
        public T_NYUUKIN_SUM_ENTRY nyuukinSumEntry;

        /// <summary>T_NYUUKIN_SUM_ENTRY</summary>
        public List<T_NYUUKIN_SUM_DETAIL> nyuukinSumDetails;

        /// <summary>T_NYUUKIN_ENTRY</summary>
        public T_NYUUKIN_ENTRY nyuukinEntry;

        /// <summary>T_NYUUKIN_DETAIL</summary>
        public List<T_NYUUKIN_DETAIL> nyuukinDetials;

        /**
         * 出金系
         */
        /// <summary>T_SHUKKIN_ENTRY</summary>
        public T_SHUKKIN_ENTRY shukkinEntry;

        /// <summary>T_SHUKKIN_DETAIL</summary>
        public List<T_SHUKKIN_DETAIL> shukkinDetails;
    }
}
