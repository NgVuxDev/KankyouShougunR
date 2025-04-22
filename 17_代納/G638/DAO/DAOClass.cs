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
using System.Data;
using Shougun.Core.Common.BusinessCommon.Dto;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PayByProxy.DainoMeisaihyoOutput
{

    [Bean(typeof(SuperEntity))]
    public interface DainoMeisaiDao : IS2Dao
    {
        /// <summary>
        /// 代納明細表
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoMeisaihyoOutput.Sql.GetData.sql")]
        DataTable GetData(SearchParameterDto data);
    }
}
