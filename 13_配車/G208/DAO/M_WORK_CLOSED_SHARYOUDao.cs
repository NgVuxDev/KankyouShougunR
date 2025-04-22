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

namespace Shougun.Core.Allocation.Sharyoukyuudounyuryoku
{
    [Bean(typeof(M_WORK_CLOSED_SHARYOU))]
    public interface M_WORK_CLOSED_SHARYOUDao : IS2Dao
    {
        /// <summary>
        /// 車輌休動マスタを検索
        /// </summary>
        /// <param name="shainCd">業者CD</param>
        /// /// <param name="shainCd">車輌CD</param>
        /// <param name="searchDate">日付</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.Sharyoukyuudounyuryoku.Sql.GetSharyoukyudouDataSql.sql")]
        DataTable GetSharyoukyudouData(String gyoushaCd, String sharyouCd, String searchDate);

        /// <summary>
        /// [TableLock付] 車輌休動マスタを検索
        /// </summary>
        /// <param name="data">車輌休動マスタEntity</param>
        /// <returns></returns>
        [Sql("SELECT * FROM M_WORK_CLOSED_SHARYOU WITH(TABLOCKX) WHERE GYOUSHA_CD = /*data.GYOUSHA_CD*/ AND SHARYOU_CD = /*data.SHARYOU_CD*/ AND CLOSED_DATE = /*data.CLOSED_DATE*/")]
        M_WORK_CLOSED_SHARYOU GetSharyoukyudouWithTableLock(M_WORK_CLOSED_SHARYOU data);

        /// <summary>
        /// レコード新規
        /// </summary>
        /// <parameparam name="data">エンティティ</parameparam>
        /// <returns>件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_WORK_CLOSED_SHARYOU data);

        /// <summary>
        /// レコード更新
        /// </summary>
        /// <param name="data">エンティティ</param>
        /// <returns>件数</returns>
        [NoPersistentProps("TEKIYOU_BEGIN", "TEKIYOU_END", "DELETE_FLG", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_WORK_CLOSED_SHARYOU data);

        /// <summary>
        /// データ削除
        /// </summary>
        /// <param name="data">エンティティ</param>
        /// <returns>件数</returns>
        int Delete(M_WORK_CLOSED_SHARYOU data);
    }
}
