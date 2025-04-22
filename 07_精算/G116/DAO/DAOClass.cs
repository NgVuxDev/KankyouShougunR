using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Adjustment.ShiharaiMeisaishoHakko
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
        /// 精算伝票取得
        /// </summary>
        /// <param name="seisanNumber"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.ShiharaiMeisaishoHakko.Sql.GetSeisanPrintData.sql")]
        DataTable GetSeisanDenpyou(string seisanNumber, string shukkinMeisaiKbn, string orderBy, bool IsZeroKingakuTaishogai);//VAN 20210202 #146742, #146751

        /// <summary>
        /// 精算伝票取得(明細なし含む)
        /// </summary>
        /// <param name="seisanNumber"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.ShiharaiMeisaishoHakko.Sql.GetSeisanPrintDataMeisaiNashi.sql")]
        DataTable GetSeisanDenpyouMeisaiNashi(string seisanNumber, string shukkinMeisaiKbn, string orderBy, string shoshikiKbn, bool IsZeroKingakuTaishogai);//VAN 20210202 #146742, #146751

        /// <summary>
        /// 精算伝票取得
        /// </summary>
        /// <param name="seisanNumber"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.ShiharaiMeisaishoHakko.Sql.GetSeisanUpdateData.sql")]
        DataTable GetSeisanDenpyouUpdateData(string seisanNumber);

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
        [SqlFile("Shougun.Core.Adjustment.ShiharaiMeisaishoHakko.Sql.GetSeisanDenpyouDataSql.sql")]
        DataTable GetDataForEntity(DTOClass data);

        /// <summary>
        /// すべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM T_SEISAN_DENPYOU")]
        T_SEISAN_DENPYOU[] GetAllData();
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
}