// $Id: IT_TEIKI_HAISHA_SHOUSAIDao.cs 5893 2013-11-05 00:21:12Z sys_dev_20 $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei
{
    [Bean(typeof(M_SHARYOU))]
    public interface M_SHARYOUDao : IS2Dao
    {
        /// <summary>
        /// 車輌名を取得する
        /// </summary>
        /// <param name="data">検索対象情報</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei.Sql.GetSharyouNameData.sql")]
        new DataTable GetSharyouNameData(M_SHARYOU data, SqlDateTime TEKIYOU_DATE);
    }
}
