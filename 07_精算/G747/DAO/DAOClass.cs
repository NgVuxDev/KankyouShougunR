using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Adjustment.InxsShiharaiMeisaishoHakko
{
    #region T_SEISAN_DENPYOU

    [Bean(typeof(T_SEISAN_DENPYOU))]
    public interface TSDDaoCls : IS2Dao
    {
        /// <summary>
        /// コードを元に精算伝票データを取得する
        /// </summary>
        /// <parameparam name="cd">業者コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("SEISAN_NUMBER = /*cd*/")]
        T_SEISAN_DENPYOU GetDataByCd(string cd);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SEISAN_DENPYOU data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SEISAN_DENPYOU data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_SEISAN_DENPYOU data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.InxsShiharaiMeisaishoHakko.Sql.GetSeisanDenpyouDataSql.sql")]
        DataTable GetDataForEntity(DTOClass data);

        /// <summary>
        /// すべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM T_SEISAN_DENPYOU")]
        T_SEISAN_DENPYOU[] GetAllData();

        [SqlFile("Shougun.Core.Adjustment.InxsShiharaiMeisaishoHakko.Sql.GetSeisanEntities.sql")]
        T_SEISAN_DENPYOU[] GetSeisanEntities(long[] seisanNumbers);

        [SqlFile("Shougun.Core.Adjustment.InxsShiharaiMeisaishoHakko.Sql.GetPublishedUserSettingData.sql")]
        DataTable GetPublishedUserSettingData(long seisanNumber, long[] ignoreUserSysIds, long[] userSysIds);

        [SqlFile("Shougun.Core.Adjustment.InxsShiharaiMeisaishoHakko.Sql.GetDataUpdateSeisan.sql")]
        DataTable GetDataUpdateSeisan(long[] seisanNumber);
    }

    #endregion

    #region M_TORIHIKISAKI

    [Bean(typeof(M_TORIHIKISAKI))]
    public interface MTDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_TORIHIKISAKI data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_TORIHIKISAKI data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(M_TORIHIKISAKI data);
    }

    #endregion

    #region M_TORIHIKISAKI_SHIHARAI

    [Bean(typeof(M_TORIHIKISAKI_SHIHARAI))]
    public interface MTSDaoCls : IS2Dao
    {
        /// <summary>
        /// コードを元に取引先_支払情報データを取得する
        /// </summary>
        /// <parameparam name="cd">業者コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_TORIHIKISAKI_SHIHARAI GetDataByCd(string cd);
    }

    #endregion

    [Bean(typeof(T_SEISAN_DENPYOU_INXS))]
    public interface IT_SEISAN_DENPYOU_INXSDao : IS2Dao
    {
        /// <summary>
        /// コードを元に請求伝票データを取得する
        /// </summary>
        /// <parameparam name="cd">業者コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("SEISAN_NUMBER = /*cd*/")]
        T_SEISAN_DENPYOU_INXS GetDataByCd(string cd);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SEISAN_DENPYOU_INXS data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SEISAN_DENPYOU_INXS data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_SEISAN_DENPYOU_INXS data);
    }

    [Bean(typeof(T_SEISAN_DENPYOU_KAGAMI_INXS))]
    public interface IT_SEISAN_DENPYOU_KAGAMI_INXSDao : IS2Dao
    {
        [Sql("SELECT TOP 1 POSTED_FILE_PATH FROM T_SEISAN_DENPYOU_KAGAMI_INXS WHERE SEISAN_NUMBER = /*seisanNumber*/")]
        string GetDataFolderPathUpload(string seisanNumber);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SEISAN_DENPYOU_KAGAMI_INXS data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SEISAN_DENPYOU_KAGAMI_INXS data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Sql("DELETE FROM T_SEISAN_DENPYOU_KAGAMI_INXS WHERE SEISAN_NUMBER = /*seisanNumber*/")]
        int DeleteBySeisan(string seisanNumber);
    }

    [Bean(typeof(T_SEISAN_DENPYOU_KAGAMI_USER_INXS))]
    public interface IT_SEISAN_DENPYOU_KAGAMI_USER_INXSDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SEISAN_DENPYOU_KAGAMI_USER_INXS data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SEISAN_DENPYOU_KAGAMI_USER_INXS data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Sql("DELETE FROM T_SEISAN_DENPYOU_KAGAMI_USER_INXS WHERE SEISAN_NUMBER = /*seisanNumber*/")]
        int DeleteBySeisan(string seisanNumber);
    }
}