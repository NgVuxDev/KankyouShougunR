// $Id: DAOClass.cs 50757 2015-05-27 05:37:50Z wuq@oec-h.com $
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Stock.ZaikoTyouseiIchiran
{
    [Bean(typeof(T_ZAIKO_TYOUSEI_DETAIL))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        new DataTable GetDateForStringSql(string sql);
    }
}
