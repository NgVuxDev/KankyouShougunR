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

namespace Shougun.Core.Billing.SeikyuushoHakkou.DAO
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

        /// <summary>
        /// 請求伝票取得
        /// </summary>
        /// <param name="seikyuNumber"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuushoHakkou.Sql.GetSeikyuPrintData.sql")]
        DataTable GetSeikyudenpyo(string seikyuNumber, string nyuukinMeisaiKbn, string orderBy, bool IsCsvFlg, bool IsZeroKingakuTaishogai);//VAN 20210202 #146742, #146751

        /// <summary>
        /// 請求伝票取得(明細なし含む)
        /// </summary>
        /// <param name="seikyuNumber"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuushoHakkou.Sql.GetSeikyuPrintDataMeisaiNashi.sql")]
        DataTable GetSeikyudenpyoMeisaiNashi(string seikyuNumber, string nyuukinMeisaiKbn, string orderBy, string shoshikiKbn, bool IsCsvFlg, bool IsZeroKingakuTaishogai);//VAN 20210202 #146742, #146751

        /// <summary>
        /// 請求伝票取得
        /// </summary>
        /// <param name="seikyuNumber"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuushoHakkou.Sql.GetSeikyuUpdateData.sql")]
        DataTable GetSeikyudenpyoUpdateData(string seikyuNumber);

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
        [SqlFile("Shougun.Core.Billing.SeikyuushoHakkou.Sql.GetSeikyuuDenpyodataSql.sql")]
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
    }
    #endregion

    #region T_SEIKYUU_DENPYOU_KAGAMI thucp 電子請求書 #157799
    [Bean(typeof(T_SEIKYUU_DENPYOU_KAGAMI))]
    public interface TSDKDaoCls : IS2Dao
    {
        [Query("SEIKYUU_NUMBER = /*seikyuNumber*/")]
        T_SEIKYUU_DENPYOU_KAGAMI[] GetDataByCd(string seikyuNumber);
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
}
