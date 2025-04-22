using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(CHANGE_LOG_M_COURSE_NIOROSHI))]
    public interface ICHANGE_LOG_M_COURSE_NIOROSHIDao : IS2Dao
    {

        [Sql("SELECT * FROM CHANGE_LOG_M_COURSE")]
        CHANGE_LOG_M_COURSE_NIOROSHI[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(CHANGE_LOG_M_COURSE_NIOROSHI data);

        [NoPersistentProps("TIME_STAMP")]
        int Update(CHANGE_LOG_M_COURSE_NIOROSHI data);

        int Delete(CHANGE_LOG_M_COURSE_NIOROSHI data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, CHANGE_LOG_M_COURSE_NIOROSHI data);
    }
}
