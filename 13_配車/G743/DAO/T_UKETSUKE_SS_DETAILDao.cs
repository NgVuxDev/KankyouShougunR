// $Id: T_UKETSUKE_SS_DETAILDao.cs 6059 2013-11-06 09:02:40Z sys_dev_18 $

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
using System.Data.SqlTypes;
using Seasar.Extension.ADO;
using Seasar.Quill.Attrs;
using System.Data;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Allocation.CarTransferSpot
{
    /// <summary>
    /// 
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SS_DETAIL))]
    public interface T_UKETSUKE_SS_DETAILDao : IS2Dao
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
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferSpot.Sql.GetUketsukeSSDetailData.sql")]
        T_UKETSUKE_SS_DETAIL[] GetDataForEntity(T_UKETSUKE_SS_DETAIL data);

        ///// <summary>
        ///// 絞り込んで値を取得する
        ///// </summary>
        ///// <param name="data">検索条件</param>
        ///// <returns>DataTable</returns>
        //[SqlFile("Shougun.Core.Allocation.CarTransferSpot.Sql.GetUketsukeSSDetailData.sql")]
        //DataTable GetDataToDataTable(DTOClass data);

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

        /////// <summary>
        /////// T_UKEIRE_ENTRY.SYSTEM_IDの最高値を取得する
        /////// </summary>
        /////// <param name="whereSql">絞り込み条件</param>
        /////// <returns>SYSTEM_IDのMAX値</returns>
        ////[Sql("select ISNULL(MAX(SYSTEM_ID),1) FROM T_UKEIRE_ENTRY /*$whereSql*/")]
        ////SqlInt64 getMaxSystemId(string whereSql);

        /// <summary>
        /// モバイル連携)収集受付データを取得
        /// </summary>
        /// <param name="SYSTEM_ID">SYSTEM_ID</param>
        /// <param name="SEQ">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferSpot.Sql.GetSyuusyuuUketukeDetailData.sql")]
        DataTable GetSyuusyuuUketukeDetailData(SqlInt64 SYSTEM_ID, SqlInt32 SEQ);

    }
}
