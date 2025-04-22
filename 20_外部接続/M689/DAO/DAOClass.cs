using System;
using System.Collections.Generic;
using System.Data;
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

namespace Shougun.Core.ExternalConnection.DigitachoMasterRenkei
{
    [Bean(typeof(M_UNIT))]
    public interface DAOClass : IS2Dao
    {
        [Sql("/*$sql*/")]
        DataTable getdateforstringsql(string sql);
    }
}
