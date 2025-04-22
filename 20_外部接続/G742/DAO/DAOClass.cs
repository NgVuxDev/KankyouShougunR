using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ExternalConnection.GenbamemoIchiran
{
    [Bean(typeof(T_GENBAMEMO_ENTRY))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// システムIDをもとに最大のSEQを取得する。
        /// </summary>
        /// <param name="systemId">SYSTEM_ID</param>
        /// <param name="seq">SEQ</param>
        /// <returns>現場メモEntity</returns>
        [Sql("SELECT MAX(SEQ) AS SEQ FROM T_GENBAMEMO_ENTRY WHERE DELETE_FLG = 0 AND SYSTEM_ID = /*systemId*/")]
        T_GENBAMEMO_ENTRY GetDataBySystemId(string systemId);

        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable getdateforstringsql(string sql);
    }
}
