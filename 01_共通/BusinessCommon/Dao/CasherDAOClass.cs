// $Id: CasherDAOClass.cs 42061 2015-02-10 10:10:07Z j-kikuchi $
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Common.BusinessCommon.Dao
{
    /// <summary>
    /// キャッシャ連携DAO
    /// </summary>
    [Bean(typeof(T_CASHERDATA))]
    public interface CasherDAOClass : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("ID")]
        int Insert(T_CASHERDATA data);
    }
}
