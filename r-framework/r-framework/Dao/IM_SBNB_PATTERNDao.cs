using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System;

namespace r_framework.Dao
{
    /// <summary>
    /// 社員マスタDao
    /// </summary>
    [Bean(typeof(M_SBNB_PATTERN))]
    public interface IM_SBNB_PATTERNDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SBNB_PATTERN data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SBNB_PATTERN data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_SBNB_PATTERN data);

        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_SBNB_PATTERN")]
        M_SBNB_PATTERN[] GetAllData();

        /// <summary>
        /// パターンデータの取得を行う
        /// </summary>
        /// <param name="path">パターン名</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetPatternDataSqlFile(string path, M_SBNB_PATTERN data);

        /// <summary>
        /// システムIDの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(SYSTEM_ID),0)+1 FROM M_SBNB_PATTERN WHERE ISNUMERIC(SYSTEM_ID) = 1")]
        Int64 GetMaxPlusKey();
    }
}