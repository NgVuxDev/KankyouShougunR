using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Function.ShougunCSCommon.Dao
{
    [Bean(typeof(T_SEISAN_DETAIL))]
    public interface IT_SEISAN_DETAILDao : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Function.ShougunCSCommon.Dao.SqlFile.SeisanDetail.IT_SEISAN_DETAILDao_GetDataForEntity.sql")]
        DataTable GetDataForEntity(T_SEISAN_DETAIL data);
    }
}
