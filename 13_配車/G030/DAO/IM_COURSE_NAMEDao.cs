// $Id: IM_COURSE_NAMEDao.cs 11465 2013-12-17 04:39:20Z sys_dev_20 $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Allocation.TeikiHaishaNyuuryoku
{
    [Bean(typeof(M_COURSE_NAME))]
    public interface IM_COURSE_NAMEDao : IS2Dao
    {
        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetCourseNameData.sql")]
        M_COURSE_NAME[] GetCourseNameData(DTOClass data);
    }
}
