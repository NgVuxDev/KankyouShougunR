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

namespace Shougun.Core.Master.SystemKobetsuSetteiHoshu.DAO
{
//    [Bean(typeof(M_SYS_INFO))]
    [Bean(typeof(M_KYOTEN))]
    internal interface DAOCls : IS2Dao
    {
        /// <summary>
        /// 拠点のデータを取得
        /// </summary>
        /// <param name="path">M_KYOTEN</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.SystemKobetsuSetteiHoshu.Sql.GetKyotenDataSql.sql")]
        DataTable GetKyotenData(M_KYOTEN data);
    }
}
