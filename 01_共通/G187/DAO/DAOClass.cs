using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Common.IchiranSyu.DAO
{
    [Bean(typeof(S_OUTPUT_COLUMN_SELECT))]
    public interface GetSOCSDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.Common.IchiranSyu.Sql.GetSelect.sql")]
        DataTable GetDataForEntity(SOCSDtoCls data);
    }

    [Bean(typeof(M_OUTPUT_PATTERN))]
    public interface GetMOPDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="SYSTEM_ID"></param>
        /// <param name="SEQ"></param>
        /// <param name="OUTPUT_KBN"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.Common.IchiranSyu.Sql.GetOutPut.sql")]
        DataTable GetDataForEntity(MOPDtoCls data);
    }

    [Bean(typeof(M_OUTPUT_PATTERN))]
    public interface SetMOPDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_OUTPUT_PATTERN data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_OUTPUT_PATTERN data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(M_OUTPUT_PATTERN data);
    }

    [Bean(typeof(M_OUTPUT_PATTERN_COLUMN))]
    public interface SetMOPCDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_OUTPUT_PATTERN_COLUMN data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_OUTPUT_PATTERN_COLUMN data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(M_OUTPUT_PATTERN_COLUMN data);
    }
}