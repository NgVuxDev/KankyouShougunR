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
using System.Data;
using Seasar.Dao.Attrs;
using System.Data;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PayByProxy.ShukeiHyoJokenShiteiPoppup
{
    [Bean(typeof(M_OUTPUT_PATTERN))]
    public interface ShukeiHyoJokenShiteiPoppupDAO : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで取引先マスタ値を取得する
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        /// 
        [SqlFile("Shougun.Core.PayByProxy.ShukeiHyoJokenShiteiPoppup.Sql.GetData.sql")]
        DataTable GetDataForEntity(ShukeiHyoJokenDTO data);

        [SqlFile("Shougun.Core.PayByProxy.ShukeiHyoJokenShiteiPoppup.Sql.GetDataReport488.sql")]
        DataTable GetDataReport488(ShukeiHyoJokenDTO data);

    }
}
