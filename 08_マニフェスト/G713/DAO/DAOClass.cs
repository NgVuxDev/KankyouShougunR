using Seasar.Dao.Attrs;
using r_framework.Dao;
using r_framework.Entity;
using System.Data;

namespace Shougun.Core.PaperManifest.ManifestoJissekiIchiran.DAO
{
    [Bean(typeof(M_HOUKOKUSHO_BUNRUI))]
    public interface HokokushoDaoCls : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestoJissekiIchiran.Sql.GetHokokushoData.sql")]
        DataTable GetDataForEntity(HokokushoDtoCls data);
    }

    [Bean(typeof(M_OUTPUT_PATTERN))]
    public interface GetTMEDaoCls : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable getdateforstringsql(string sql);
    }

    [Bean(typeof(DT_MF_TOC))]
    public interface GetDMTDaoCls : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable getdateforstringsql(string sql);
    }
}