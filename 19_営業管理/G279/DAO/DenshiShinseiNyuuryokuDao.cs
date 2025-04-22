using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.BusinessManagement.DenshiShinseiNyuuryoku.DAO
{
    [Bean(typeof(T_DENSHI_SHINSEI_ENTRY))]
    public interface IT_DENSHI_SHINSEI_ENTRYDao : IS2Dao
    {
        [SqlFile("Shougun.Core.BusinessManagement.DenshiShinseiNyuuryoku.Sql.GetDenshiShinseiEntry.sql")]
        DataTable GetDataByKey(T_DENSHI_SHINSEI_ENTRY data);
    }

    [Bean(typeof(T_DENSHI_SHINSEI_DETAIL))]
    public interface IT_DENSHI_SHINSEI_DETAILDao : IS2Dao
    {
        [SqlFile("Shougun.Core.BusinessManagement.DenshiShinseiNyuuryoku.Sql.GetDenshiShinseiDetail.sql")]
        DataTable GetDataByKey(T_DENSHI_SHINSEI_DETAIL data);
    }
}
