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
    [Bean(typeof(M_COURSE))]
    public interface IM_COURSE_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// コース名称ポップアップデータのリストを取得する
        /// </summary>
        /// <parameparam name="data">検索条件</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetCourseEntryData.sql")]
        new DataTable GetCourseEntryData(DTOClass data);
    }
}
