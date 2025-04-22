using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Stock.ZaikoIdouIchiran
{
    [Bean(typeof(T_ZAIKO_IDOU_ENTRY))]
    public interface DAOClass : IS2Dao
    {
        [Sql("/*$sql*/")]
        new DataTable GetDateForStringSql(string sql);
    }
}
