// $Id: IM_KARI_GENBADao.cs 26123 2014-07-18 08:54:09Z ria_koec $
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.GenbaKakunin.Dao
{
    /// <summary>
    /// 引合現場マスタDao
    /// </summary>
    [Bean(typeof(M_KARI_GENBA))]
    public interface IM_KARI_GENBADao : IS2Dao
    {
        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_KARI_GENBA")]
        M_KARI_GENBA[] GetAllData();

        /// <summary>
        /// 現場コードを元に現場情報を取得
        /// </summary>
        /// <param name="data"></param>
        [Query("GYOUSHA_CD = /*data.GYOUSHA_CD*/ and GENBA_CD = /*data.GENBA_CD*/")]
        M_KARI_GENBA GetDataByCd(M_KARI_GENBA data);
    }
}