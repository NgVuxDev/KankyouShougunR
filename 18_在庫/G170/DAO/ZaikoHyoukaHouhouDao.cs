using System;
using r_framework.Dao;
using Seasar.Dao.Attrs;
using Shougun.Core.Stock.ZaikoShimeSyori.Entity;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Stock.ZaikoShimeSyori.DAO
{
    [Bean(typeof(ZaikoHyoukaHouhou))]
    public interface ZaikoHyoukaHouhouDao : IS2Dao
    {
        /// <summary>
        /// 評価方法を取得
        /// </summary>
        /// <param name="input">常にnull(検索条件なし)</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Stock.ZaikoShimeSyori.Sql.SearchZaikoHyoukaHouhou.sql")]
        ZaikoHyoukaHouhou getZaikoHyoukaHouhou(Object input);

    }
}
