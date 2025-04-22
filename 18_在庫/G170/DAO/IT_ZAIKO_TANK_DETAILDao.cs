using System.Data;
using r_framework.Dao;
using Seasar.Dao.Attrs;
using Shougun.Core.Stock.ZaikoShimeSyori.DTO;
using Shougun.Core.Stock.ZaikoShimeSyori.Entity;
using r_framework.Entity;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Stock.ZaikoShimeSyori.DAO
{
    [Bean(typeof(T_ZAIKO_TANK_DETAIL))]
    public interface IT_ZAIKO_TANK_DETAILDao : IS2Dao
    {
        /// <summary>
        /// ≪在庫締明細　T_ZAIKO_TANK_DETAIL≫データ登録
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_ZAIKO_TANK_DETAIL data);

    }
}
