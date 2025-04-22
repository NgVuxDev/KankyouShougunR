using System;
using r_framework.Dao;
using Seasar.Dao.Attrs;
using Shougun.Core.Stock.ZaikoShimeSyori.Entity;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Stock.ZaikoShimeSyori.DAO
{
    [Bean(typeof(ZaikoBaseTanka))]
    public interface MZaikoHinmeiDao : IS2Dao
    {
        /// <summary>
        /// 在庫基準単価を取得
        /// </summary>
        /// <param name="zaikoHinmeiCd">在庫品名CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Stock.ZaikoShimeSyori.Sql.SearchZaikoBaseTanka.sql")]
        ZaikoBaseTanka getZaikoBaseTanka(string zaikoHinmeiCd);

    }
}
