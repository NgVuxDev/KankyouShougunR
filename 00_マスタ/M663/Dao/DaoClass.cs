// $Id: DaoClass.cs 56232 2015-07-21 06:20:31Z j-kikuchi $
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.CourseIchiran
{
    /// <summary>
    /// コース一覧用DAO
    /// </summary>
    [Bean(typeof(M_COURSE))]
    internal interface DAOClass : IS2Dao
    {
        /// <summary>
        /// コース一覧用の一覧データを取得
        /// </summary>
        /// <param name="data">検索条件dto</param>
        /// <returns name="DataTable">一覧表示用DataTable</returns>
        [SqlFile("Shougun.Core.Master.CourseIchiran.Sql.GetIchiranData.sql")]
        DataTable getIchiranData(DTOClass data);

        /// <summary>
        /// コース_荷降先の一覧データを取得
        /// </summary>
        /// <param name="data">M_COURSE data</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.CourseIchiran.Sql.GetCourseNameData.sql")]
        DataTable GetCourseNameData(M_COURSE_NAME data);

        /// <summary>
        /// コース_荷降先の一覧データを取得
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        DataTable GetDataBySql(string sql);
    }
}
