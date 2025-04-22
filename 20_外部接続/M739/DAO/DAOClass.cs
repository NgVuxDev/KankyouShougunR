using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.ExternalConnection.MapRenkei.DAO
{
    /// <summary>
    ///
    /// </summary>
    [Bean(typeof(SuperEntity))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        DataTable GetCsvOutputData(string sql);
    }
}