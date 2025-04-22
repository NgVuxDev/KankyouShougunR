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

namespace Shougun.Core.Allocation.Untenshakyudounyuuryoku
{
    [Bean(typeof(M_WORK_CLOSED_UNTENSHA))]
    public interface M_WORK_CLOSED_UNTENSHADao : IS2Dao
    {
        /// <summary>
        /// 運転者休動マスタを検索
        /// </summary>
        /// <param name="shainCd">運転者CD</param>
        /// <param name="searchDate">日付</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.Untenshakyudounyuuryoku.Sql.GetUntenshakyudouDataSql.sql")]
        DataTable GetUntenshakyudouData(String shainCd, String searchDate);

        /// <summary>
        /// 運転者休動マスタを検索
        /// </summary>
        /// <param name="shainCd">運転者CD</param>
        /// <param name="searchDate">日付</param>
        /// <returns></returns>
        [Sql("SELECT * FROM M_WORK_CLOSED_UNTENSHA WITH(TABLOCKX) WHERE SHAIN_CD = /*data.SHAIN_CD*/ AND CLOSED_DATE = /*data.CLOSED_DATE*/")]
        M_WORK_CLOSED_UNTENSHA GetUntenshakyudouDataWithTableLock(M_WORK_CLOSED_UNTENSHA data);

        /// <summary>
        /// レコード新規
        /// </summary>
        /// <parameparam name="data">エンティティ</parameparam>
        /// <returns>件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_WORK_CLOSED_UNTENSHA data);

        /// <summary>
        /// レコード更新
        /// </summary>
        /// <param name="data">エンティティ</param>
        /// <returns>件数</returns>
        [NoPersistentProps("TEKIYOU_BEGIN", "TEKIYOU_END", "DELETE_FLG", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_WORK_CLOSED_UNTENSHA data);

        /// <summary>
        /// データ削除
        /// </summary>
        /// <param name="data">エンティティ</param>
        /// <returns>件数</returns>
        int Delete(M_WORK_CLOSED_UNTENSHA data);
    }
}
