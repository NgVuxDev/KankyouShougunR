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
using Shougun.Core.Common.TenpyouTankaIkatsuHenkou.DTO;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Common.TenpyouTankaIkatsuHenkou.Dao
{
    /// <summary>
    /// 伝票単価明細
    /// </summary>
    [Bean(typeof(T_SHUKKA_DETAIL))]
    public interface GetDenpyouTankaDetailDao : IS2Dao
    {
        /// <summary>
        /// Insert(使用しない)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SHUKKA_DETAIL data);

        /// <summary>
        /// Update(使用しない)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SHUKKA_DETAIL data);

        /// <summary>
        /// Delete(使用しない)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_SHUKKA_DETAIL data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Common.TenpyouTankaIkatsuHenkou.Sql.GetDenpyouTankaDetail.sql")]
        DataTable GetDataToDataTable(DTOCls data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="whereSql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        System.Data.DataTable GetDateForStringSql(string sql);

    }
    /// <summary>
    /// 伝票単価明細
    /// </summary>
    [Bean(typeof(T_SHUKKA_DETAIL))]
    public interface GetUkeireTankaSabunDetailDao : IS2Dao
    {
        /// <summary>
        /// Insert(使用しない)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SHUKKA_DETAIL data);

        /// <summary>
        /// Update(使用しない)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SHUKKA_DETAIL data);

        /// <summary>
        /// Delete(使用しない)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_SHUKKA_DETAIL data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Common.TenpyouTankaIkatsuHenkou.Sql.GetUkeireTankaSabunDetail.sql")]
        DataTable GetDataToDataTable(DTOCls data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="whereSql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        System.Data.DataTable GetDateForStringSql(string sql);

    }
    /// <summary>
    /// 伝票単価明細
    /// </summary>
    [Bean(typeof(T_SHUKKA_DETAIL))]
    public interface GetShukkaTankaSabunDetailDao : IS2Dao
    {
        /// <summary>
        /// Insert(使用しない)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SHUKKA_DETAIL data);

        /// <summary>
        /// Update(使用しない)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SHUKKA_DETAIL data);

        /// <summary>
        /// Delete(使用しない)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_SHUKKA_DETAIL data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Common.TenpyouTankaIkatsuHenkou.Sql.GetShukkaTankaSabunDetail.sql")]
        DataTable GetDataToDataTable(DTOCls data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="whereSql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        System.Data.DataTable GetDateForStringSql(string sql);

    }
    /// <summary>
    /// 伝票単価明細
    /// </summary>
    [Bean(typeof(T_SHUKKA_DETAIL))]
    public interface GetUrShTankaSabunDetailDao : IS2Dao
    {
        /// <summary>
        /// Insert(使用しない)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SHUKKA_DETAIL data);

        /// <summary>
        /// Update(使用しない)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SHUKKA_DETAIL data);

        /// <summary>
        /// Delete(使用しない)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_SHUKKA_DETAIL data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Common.TenpyouTankaIkatsuHenkou.Sql.GetUrShTankaSabunDetail.sql")]
        DataTable GetDataToDataTable(DTOCls data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="whereSql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        System.Data.DataTable GetDateForStringSql(string sql);

    }
}
