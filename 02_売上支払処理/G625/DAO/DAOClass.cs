using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.SalesPayment.ShiharaiJunihyo
{
    /// <summary>
    /// 支払順位表に出力するデータを取得するインタフェース
    /// </summary>
    [Bean(typeof(T_UKEIRE_ENTRY))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// 支払順位表に出力するデータを取得します
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.ShiharaiJunihyo.Sql.GetShiharaiJunihyo.sql")]
        DataTable GetGetShiharaiJunihyoData(ShiharaiJunihyoDto dto);
    }
}
