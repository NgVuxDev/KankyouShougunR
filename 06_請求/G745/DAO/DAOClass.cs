using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao.Attrs;
using System.Data;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Billing.InxsSeikyuushoHakkou.DAO
{

    #region T_SEIKYUU_DENPYOU

    [Bean(typeof(T_SEIKYUU_DENPYOU))]
    public interface TSDDaoCls : IS2Dao
    {
        /// <summary>
        /// コードを元に請求伝票データを取得する
        /// </summary>
        /// <parameparam name="cd">業者コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("SEIKYUU_NUMBER = /*cd*/")]
        T_SEIKYUU_DENPYOU GetDataByCd(string cd);

        [SqlFile("Shougun.Core.Billing.InxsSeikyuushoHakkou.Sql.GetSeyikyuuEntities.sql")]
        T_SEIKYUU_DENPYOU[] GetSeyikyuuEntities(long[] SeikyuuNumbers);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SEIKYUU_DENPYOU data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SEIKYUU_DENPYOU data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_SEIKYUU_DENPYOU data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        System.Data.DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.InxsSeikyuushoHakkou.Sql.GetSeikyuuDenpyouDataSql.sql")]
        new DataTable GetDataForEntity(DTOClass data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        new DataTable GetDataForStringSql(string sql);

        /// <summary>
        /// すべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM T_SEIKYUU_DENPYOU")]
        T_SEIKYUU_DENPYOU[] GetAllData();

        [SqlFile("Shougun.Core.Billing.InxsSeikyuushoHakkou.Sql.GetPublishedUserSettingData.sql")]
        DataTable GetPublishedUserSettingData(long seikyuuNumber, long[] ignoreUserSysIds, long[] userSysIds);

        [SqlFile("Shougun.Core.Billing.InxsSeikyuushoHakkou.Sql.GetDataUpdateSeikyuu.sql")]
        DataTable GetDataUpdateSeikyuu(long[] SeikyuuNumbers);
    }
    #endregion


    [Bean(typeof(M_TORIHIKISAKI_SEIKYUU))]
    public interface MTSDaoCls : IS2Dao
    {
        /// <summary>
        /// コードを元に取引先_請求情報データを取得する
        /// </summary>
        /// <parameparam name="cd">業者コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_TORIHIKISAKI_SEIKYUU GetDataByCd(string cd);
    }


    [Bean(typeof(T_SEIKYUU_DENPYOU_INXS))]
    public interface IT_SEIKYUU_DENPYOU_INXSDao : IS2Dao
    {
        /// <summary>
        /// コードを元に請求伝票データを取得する
        /// </summary>
        /// <parameparam name="cd">業者コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("SEIKYUU_NUMBER = /*cd*/")]
        T_SEIKYUU_DENPYOU_INXS GetDataByCd(string cd);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SEIKYUU_DENPYOU_INXS data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SEIKYUU_DENPYOU_INXS data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_SEIKYUU_DENPYOU_INXS data);
    }

    [Bean(typeof(T_SEIKYUU_DENPYOU_KAGAMI_INXS))]
    public interface IT_SEIKYUU_DENPYOU_KAGAMI_INXSDao : IS2Dao
    {
        [Sql("SELECT TOP 1 POSTED_FILE_PATH FROM T_SEIKYUU_DENPYOU_KAGAMI_INXS WHERE SEIKYUU_NUMBER = /*seikyuuNumber*/")]
        string GetDataFolderPathUpload(string seikyuuNumber);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SEIKYUU_DENPYOU_KAGAMI_INXS data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SEIKYUU_DENPYOU_KAGAMI_INXS data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Sql("DELETE FROM T_SEIKYUU_DENPYOU_KAGAMI_INXS WHERE SEIKYUU_NUMBER = /*seikyuuNumber*/")]
        int DeleteBySeikyuu(string seikyuuNumber);
    }

    [Bean(typeof(T_SEIKYUU_DENPYOU_KAGAMI_USER_INXS))]
    public interface IT_SEIKYUU_DENPYOU_KAGAMI_USER_INXSDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SEIKYUU_DENPYOU_KAGAMI_USER_INXS data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SEIKYUU_DENPYOU_KAGAMI_USER_INXS data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Sql("DELETE FROM T_SEIKYUU_DENPYOU_KAGAMI_USER_INXS WHERE SEIKYUU_NUMBER = /*seikyuuNumber*/")]
        int DeleteBySeikyuu(string seikyuuNumber);
    }
}
