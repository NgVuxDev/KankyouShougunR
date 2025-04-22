using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PaperManifest.Himodukeichiran.DAO
{
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable getdateforstringsql(string sql);

        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [SqlFile("Shougun.Core.PaperManifest.Himodukeichiran.Sql.GetManiData.sql")]
        DataTable GetDataForEntity(DTOClass data);
    }
    //[Bean(typeof(T_UKEIRE_DETAIL))]
    //public interface UKEIREK_DETAIL_DaoCls : IS2Dao
    //{
    //    /// <summary>
    //    /// Insert
    //    /// </summary>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    int Insert(T_UKEIRE_DETAIL data);


    //    /// Update
    //    /// </summary>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
    //    int Update(T_UKEIRE_DETAIL data);

    //    /// <summary>
    //    /// Delete
    //    /// </summary>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    int Delete(T_UKEIRE_DETAIL data);

    //}
    //[Bean(typeof(T_UKEIRE_ENTRY))]
    //public interface UKEIREK_ENTRY_DaoCls : IS2Dao
    //{
    //    /// <summary>
    //    /// Insert
    //    /// </summary>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    int Insert(T_UKEIRE_ENTRY data);


    //    /// Update
    //    /// </summary>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
    //    int Update(T_UKEIRE_ENTRY data);

    //    /// <summary>
    //    /// Delete
    //    /// </summary>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    int Delete(T_UKEIRE_ENTRY data);

    //}
    //[Bean(typeof(T_SHUKKA_DETAIL))]
    //public interface SHUKKAK_DETAIL_DaoCls : IS2Dao
    //{
    //    /// <summary>
    //    /// Insert
    //    /// </summary>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    int Insert(T_SHUKKA_DETAIL data);


    //    /// Update
    //    /// </summary>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
    //    int Update(T_SHUKKA_DETAIL data);

    //    /// <summary>
    //    /// Delete
    //    /// </summary>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    int Delete(T_SHUKKA_DETAIL data);

    //}
    //[Bean(typeof(T_SHUKKA_ENTRY))]
    //public interface SHUKKAK_ENTRY_DaoCls : IS2Dao
    //{
    //    /// <summary>
    //    /// Insert
    //    /// </summary>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    int Insert(T_SHUKKA_ENTRY data);


    //    /// Update
    //    /// </summary>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
    //    int Update(T_SHUKKA_ENTRY data);

    //    /// <summary>
    //    /// Delete
    //    /// </summary>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    int Delete(T_SHUKKA_ENTRY data);

    //}
    //[Bean(typeof(T_UR_SH_DETAIL))]
    //public interface UR_SHK_DETAIL_DaoCls : IS2Dao
    //{
    //    /// <summary>
    //    /// Insert
    //    /// </summary>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    int Insert(T_UR_SH_DETAIL data);


    //    /// Update
    //    /// </summary>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
    //    int Update(T_UR_SH_DETAIL data);

    //    /// <summary>
    //    /// Delete
    //    /// </summary>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    int Delete(T_UR_SH_DETAIL data);

    //}
    //[Bean(typeof(T_UR_SH_ENTRY))]
    //public interface UR_SHK_ENTRY_DaoCls : IS2Dao
    //{
    //    /// <summary>
    //    /// Insert
    //    /// </summary>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    int Insert(T_UR_SH_ENTRY data);


    //    /// Update
    //    /// </summary>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
    //    int Update(T_UR_SH_ENTRY data);

    //    /// <summary>
    //    /// Delete
    //    /// </summary>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    int Delete(T_UR_SH_ENTRY data);

    //}
}
