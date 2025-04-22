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

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Allocation.MobileShougunTorikomi.DAO
{
    [Bean(typeof(S_NUMBER_DAY))]
    public interface IS_NUMBER_DAYDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(S_NUMBER_DAY data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(S_NUMBER_DAY data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(S_NUMBER_DAY data);

        /// <summary>
        /// 指定の条件でデータを取得
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        [Sql("SELECT S_NUMBER_DAY.CURRENT_NUMBER, S_NUMBER_DAY.TIME_STAMP FROM dbo.S_NUMBER_DAY /*$whereSql*/")]
        System.Data.DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.IS_NUMBER_DAYDao_GetDataForEntity.sql")]
        S_NUMBER_DAY[] GetDataForEntity(S_NUMBER_DAY data);

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
