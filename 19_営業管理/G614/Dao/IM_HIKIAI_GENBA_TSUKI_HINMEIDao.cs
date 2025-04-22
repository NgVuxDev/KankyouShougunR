// $Id: IM_HIKIAI_GENBA_TSUKI_HINMEIDao.cs 12286 2013-12-22 14:15:20Z gai $
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.GenbaKakunin.Dao
{
    /// <summary>
    /// 引合現場マスタDao
    /// </summary>
    [Bean(typeof(M_HIKIAI_GENBA_TSUKI_HINMEI))]
    public interface IM_HIKIAI_GENBA_TSUKI_HINMEIDao : IS2Dao
    { 
        /// <summary>
        /// 引合現場に関連するデータ取得を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>取得したDataTable</returns>
        [SqlFile("Shougun.Core.BusinessManagement.GenbaKakunin.Sql.GetTsukiHinmeiDataSql.sql")]
        DataTable GetTsukiHinmeiData(M_HIKIAI_GENBA_TSUKI_HINMEI data);
         
    }
}