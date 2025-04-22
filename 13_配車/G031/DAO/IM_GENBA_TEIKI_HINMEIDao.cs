// $Id: IM_GENBA_TEIKI_HINMEIDao.cs 9436 2013-12-04 00:50:08Z sys_dev_23 $
using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using r_framework.Dao;

namespace Shougun.Core.Allocation.CourseHaishaIraiNyuuryoku
{
    /// <summary>
    /// 現場定期品名マスタDao
    /// </summary>
    [Bean(typeof(M_GENBA_TEIKI_HINMEI))]
    public interface IM_GENBA_TEIKI_HINMEIDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
         [NoPersistentProps("TIME_STAMP")]
        int Insert(M_GENBA_TEIKI_HINMEI data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_GENBA_TEIKI_HINMEI data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_GENBA_TEIKI_HINMEI data);

        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_GENBA_TEIKI_HINMEI")]
        M_GENBA_TEIKI_HINMEI[] GetAllData();

        /// <summary>
        /// 現場定期品名データ取得
        /// </summary>
        /// <parameparam name="data">検索条件</parameparam>
        [SqlFile("Shougun.Core.Allocation.CourseHaishaIraiNyuuryoku.Sql.GetGenbaTeikiHinmeiData.sql")]
        M_GENBA_TEIKI_HINMEI[] GetDataForEntity(M_GENBA_TEIKI_HINMEI data);
    }
}