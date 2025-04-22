using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.SalesPayment.TukigimeUriageDenpyoSakusei
{
    /// <summary>
    /// 現場_月極品名マスタDAO
    /// </summary>
    [Bean(typeof(M_GENBA_TSUKI_HINMEI))]
    public interface MGTHDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data">現場_月極品名マスタ</param>
        /// <returns></returns>
        M_GENBA_TSUKI_HINMEI GetDataForEntity(M_GENBA_TSUKI_HINMEI data);
    }
}
