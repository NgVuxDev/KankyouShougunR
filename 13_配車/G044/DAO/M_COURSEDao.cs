// $Id: IT_TEIKI_HAISHA_SHOUSAIDao.cs 5893 2013-11-05 00:21:12Z sys_dev_20 $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei
{
    [Bean(typeof(M_COURSE))]
    public interface M_COURSEDao : IS2Dao
    {
        /// <summary>
        /// コース名を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei.Sql.GetCourseNameData.sql")]
        new DataTable GetCourseData(DTOClass data);
    }
}
