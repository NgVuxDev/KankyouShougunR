// $Id: IM_HIKIAI_GENBA_TSUKI_HINMEIDao.cs 12286 2013-12-22 14:15:20Z gai $
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.HikiaiGenbaHoshu.Dao
{
    /// <summary>
    /// 引合現場マスタDao
    /// </summary>
    [Bean(typeof(M_HIKIAI_GENBA_TSUKI_HINMEI))]
    public interface IM_HIKIAI_GENBA_TSUKI_HINMEIDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_HIKIAI_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_HIKIAI_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_HIKIAI_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// 引合現場に関連するデータ取得を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>取得したDataTable</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetTsukiHinmeiDataSql.sql")]
        DataTable GetTsukiHinmeiData(M_HIKIAI_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// 引合現場に関連するデータ構造の取得を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>取得したDataTable</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetTsukiHinmeiStructSql.sql")]
        DataTable GetTsukiHinmeiStruct(M_HIKIAI_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// 引合現場に関連するデータ削除を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>削除した件数</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.DeleteTsukiHinmeiSql.sql")]
        int DeleteTsukiHinmei(M_HIKIAI_GENBA_TSUKI_HINMEI data);
    }
}