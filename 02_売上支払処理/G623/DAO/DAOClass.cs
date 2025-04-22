using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.SalesPayment.UriageJunihyo
{
    /// <summary>
    /// 売上順位表に出力するデータを取得するインタフェース
    /// </summary>
    [Bean(typeof(T_UKEIRE_ENTRY))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// 売上順位表に出力するデータを取得します
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UriageJunihyo.Sql.GetUriageJunihyo.sql")]
        DataTable GetGetUriageJunihyoData(UriageJunihyoDto dto);
    }
}
