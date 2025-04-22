using System.Data;
using r_framework.Dao;
using Seasar.Dao.Attrs;
using Shougun.Core.Stock.ZaikoShimeSyori.DTO;
using Shougun.Core.Stock.ZaikoShimeSyori.Entity;
using r_framework.Entity;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Stock.ZaikoShimeSyori.DAO
{
    /// <summary>
    /// 締元情報操作関連DAO
    /// </summary>
    [Bean(typeof(ShimeTargetInfo))]
    public interface ShimeTargetInfoDao : IS2Dao
    {
        /// <summary>
        /// 在庫締め対象データ取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Stock.ZaikoShimeSyori.Sql.SearchSimeTargetInfo.sql")]
        ShimeTargetInfo[] searchShimeTargetInfo(F18_G170Dto data);

    }
}
