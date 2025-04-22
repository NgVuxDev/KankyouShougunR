using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_SYS_INFO_CHANGE_LOG))]
    public interface IM_SYS_INFO_CHANGE_LOGDao : IS2Dao
    {

        [Sql("SELECT * FROM M_SYS_INFO_CHANGE_LOG")]
        M_SYS_INFO_CHANGE_LOG[] GetAllData();

        /// <summary>
        /// 最大IDを取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.SysInfo.GetMaxId.sql")]
        DataTable GetMaxId(M_SYS_INFO_CHANGE_LOG data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SYS_INFO_CHANGE_LOG data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC")]
        int Update(M_SYS_INFO_CHANGE_LOG data);

        int Delete(M_SYS_INFO_CHANGE_LOG data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SYS_INFO_CHANGE_LOG data);

        [Query("ID = /*id*/")]
        M_SYS_INFO_CHANGE_LOG GetAllDataForCode(string id);

        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
