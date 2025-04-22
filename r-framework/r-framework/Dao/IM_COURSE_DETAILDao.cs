using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_COURSE_DETAIL))]
    public interface IM_COURSE_DETAILDao : IS2Dao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_COURSE_DETAIL data);

        [NoPersistentProps("DAY_CD", "COURSE_NAME_CD", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_COURSE_DETAIL data);

        /// <summary>
        /// コース詳細のレコードを削除します
        /// </summary>
        /// <param name="data">条件エンティティ</param>
        /// <returns>削除した件数</returns>
        int Delete(M_COURSE_DETAIL data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("DAY_CD = /*DAY_CD*/ AND COURSE_NAME_CD = /*COURSE_NAME_CD*/ AND REC_ID = /*REC_ID*/")]
        M_COURSE_DETAIL GetDataByCd(string DAY_CD, string COURSE_NAME_CD, string REC_ID);

        [Sql("SELECT * FROM M_COURSE_DETAIL")]
        M_COURSE_DETAIL[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.CourseDetail.IM_COURSE_DETAILDao_GetAllValidData.sql")]
        M_COURSE_DETAIL[] GetAllValidData(M_COURSE_DETAIL data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_COURSE_DETAIL data);

        /// <summary>
        /// 業者、現場に該当するM_COURSE_DETAILからM_COURSE_DETAIL_ITEMSの情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.CourseDetail.IM_COURSE_DETAILDao_GetCourseDetailItemsData.sql")]
        DataTable GetCourseDetailItemsData(M_COURSE_DETAIL data);

    }
}
