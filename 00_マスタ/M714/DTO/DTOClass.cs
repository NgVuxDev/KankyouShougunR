using System;
using r_framework.Entity;

namespace Shougun.Core.Master.ChiikiIkkatsu
{
    public class DTOClass : SuperEntity
    {
        /// <summary>
        /// 検索条件：　変更方法
        /// </summary>
        public String Henkouhouhou { get; set; }

        /// <summary>
        /// 検索条件：　マスタ名
        /// </summary>
        public String ChiikiMasterName { get; set; }

        /// <summary>
        /// 検索条件：　変更前地域CD
        /// </summary>
        public String ChiikiCdOld { get; set; }

        /// <summary>
        /// 検索条件：　変更前地域住所
        /// </summary>
        public String ChiikiJyuushoOld { get; set; }
    }
}
