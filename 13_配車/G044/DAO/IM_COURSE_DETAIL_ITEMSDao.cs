// $Id: IM_COURSE_DETAILDao.cs 6914 2013-11-14 04:12:37Z sys_dev_20 $
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
    [Bean(typeof(M_COURSE_DETAIL_ITEMS))]
    public interface IM_COURSE_DETAIL_ITEMSDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_COURSE_DETAIL_ITEMS data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_COURSE_DETAIL_ITEMS data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_COURSE_DETAIL_ITEMS data);

        /// <summary>
        /// コースCDをキーとしてコース明細のデータを取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei.Sql.GetCourseDetailData.sql")]
        new DataTable GetCourseDetailData(DTOClass data);

        /// <summary>
        /// コースCDをキーとしてコース荷降先のデータを取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei.Sql.GetCourseNioroshiData.sql")]
        new DataTable GetCourseNioroshiData(DTOClass data);

        /// <summary>
        /// コース名称CD、レコードIDをキーとしてコース_明細内訳のデータを取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei.Sql.GetCourseDetailItemsData.sql")]
        new DataTable GetCourseDetailItemsData(DTOClass data);
    }
}
