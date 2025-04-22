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

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Allocation.Sharyoukyuudounyuryoku 
{
    [Bean(typeof(M_SHARYOU))]
    public interface M_SHARYOUDao : IS2Dao
    {
        /// <summary>
        /// 社員マスタを取得
        /// </summary>
        /// <param name="dto">条件データ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.Sharyoukyuudounyuryoku.Sql.GetSharyouDataSql.sql")]
        DataTable GetSharyouData(SearchDTOClass data);

    }
}
