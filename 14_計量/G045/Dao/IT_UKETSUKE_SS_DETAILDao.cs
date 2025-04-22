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
using System.Data.SqlTypes;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Scale.Keiryou.Dao
{
    [Bean(typeof(T_UKETSUKE_SS_DETAIL))]
    public interface IT_UKETSUKE_SS_DETAILDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SS_DETAIL data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UKETSUKE_SS_DETAIL data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_UKETSUKE_SS_DETAIL data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Scale.Keiryou.Sql.IT_UKETSUKE_DETAILDao_GetDataForEntity.sql")]
        T_UKETSUKE_SS_DETAIL[] GetDataForEntity(T_UKETSUKE_SS_DETAIL data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// T_UKETSUKE_SS_DETAIL.SYSTEM_IDの最高値を取得する
        /// </summary>
        /// <param name="whereSql">絞り込み条件</param>
        /// <returns>SYSTEM_IDのMAX値</returns>
        [Sql("select ISNULL(MAX(DETAIL_SYSTEM_ID),1) FROM T_UKETSUKE_SS_DETAIL /*$whereSql*/")]
        SqlInt64 getMaxSystemId(string whereSql);
    }

    [Bean(typeof(T_UKETSUKE_SK_ENTRY))]
    public interface IT_UKETSUKE_SK_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SK_ENTRY data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UKETSUKE_SK_ENTRY data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_UKETSUKE_SK_ENTRY data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="uketukeNumber"></param>
        /// <returns></returns>
        [Query("UKETSUKE_NUMBER = /*uketukeNumber*/ AND DELETE_FLG = 1 ")]
        T_UKETSUKE_SK_ENTRY[] GetDataByCd(long uketukeNumber);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetDateForStringSql(string sql);
    }
}
