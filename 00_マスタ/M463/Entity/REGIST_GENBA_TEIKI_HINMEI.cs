using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Master.HikiaiGenbaHoshu.Entity
{
    /// <summary>
    /// 現場_定期情報_更新用クラス
    /// </summary>
    public class REGIST_GENBA_TEIKI_HINMEI : M_HIKIAI_GENBA_TEIKI_HINMEI
    {
        /// <summary>
        /// DBには存在しないカラム、明細データを削除するために使用
        /// </summary>
        public bool DELETE_FLG { get; set; }
    }
}
