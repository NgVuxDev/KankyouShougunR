using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Data;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Function.ShougunCSCommon.Dao
{
    [Bean(typeof(T_SEIKYUU_DETAIL))]
    public interface IT_SEIKYUU_DETAILDao : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Function.ShougunCSCommon.Dao.SqlFile.SeikyuuDetail.IT_SEIKYUU_DETAILDao_GetDataForEntity.sql")]
        DataTable GetDataForEntity(T_SEIKYUU_DETAIL data);
    }
}
