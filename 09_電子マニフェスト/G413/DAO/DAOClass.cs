using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ElectronicManifest.RealInfoSearch
{
    [Bean(typeof(M_OUTPUT_PATTERN))]
    public interface GetInfoDaoCls : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable getdataforstringsql(string sql);
    }

    [Bean(typeof(QUE_INFO))]
    public interface InsertQIDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(QUE_INFO data);

        [Sql("/*$sql*/")]
        DataTable GetRequestDataInDay(string sql);
    }
}
