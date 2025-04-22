// $Id: IM_COURSE_DETAILDao.cs 9378 2013-12-03 07:33:31Z sys_dev_20 $
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
    [Bean(typeof(M_COURSE_DETAIL))]
    public interface IM_COURSE_DETAILDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_COURSE_DETAIL data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_COURSE_DETAIL data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_COURSE_DETAIL data);

        /// <summary>
        /// コースCDをキーとしてコース明細のデータを取得する
        /// </summary>
        /// <parameparam name="data">検索条件</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetCourseDetailData.sql")]
        new DataTable GetCourseDetailData(DTOClass data);

        /// <summary>
        /// コースCDをキーとしてコース荷降先のデータを取得する
        /// </summary>
        /// <parameparam name="data">検索条件</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetCourseNioroshiData.sql")]
        new DataTable GetCourseNioroshiData(DTOClass data);

        /// <summary>
        /// コース名称CD、レコードIDをキーとしてコース_明細内訳のデータを取得する
        /// </summary>
        /// <parameparam name="data">検索条件</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetCourseDetailItemsData.sql")]
        new DataTable GetCourseDetailItemsData(DTOClass data);

        /// <summary>
        /// コース名称ポップアップデータのリストを取得する
        /// </summary>
        /// <parameparam name="data">検索条件</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetCourseNameListForPopUp.sql")]
        new DataTable GetCourseNameListForPopUp(DTOClass data);
    }
}
