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

namespace Shougun.Core.Allocation.Teikijissekihoukoku
{
    [Bean(typeof(TeikijissekihoukokuDTOClass))]
    public interface TeikijissekihoukokuDAOClass : IS2Dao
    {
        // 検索条件で：月報の廃棄物種類、日付、単位を取る
        [SqlFile("Shougun.Core.Allocation.Teikijissekihoukoku.Sql.GetReportDetailDataByMonth.sql")]
        DataTable GetReportDetailDataByMonth(TeikijissekihoukokuDTOClass data);
      
        //検索条件で：年報の廃棄物種類、日付、単位を取る
        [SqlFile("Shougun.Core.Allocation.Teikijissekihoukoku.Sql.GetReportDetailDataByYear.sql")]
        DataTable GetReportDetailDataByYear(TeikijissekihoukokuDTOClass data); 
    
    }
}
