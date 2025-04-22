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

namespace Shougun.Core.PayByProxy.DainoNyuryuku
{
    [Bean(typeof(T_UR_SH_ENTRY))]
    public interface T_UR_SH_ENTRYDao : IS2Dao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UR_SH_ENTRY data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UR_SH_ENTRY data);

        int Delete(T_UR_SH_ENTRY data);

        /// <summary>
        /// 売上支払データ取得
        /// </summary>
        /// <param name="data">代納エンティティ</param>
        /// <returns></returns>
        [Sql("SELECT * FROM T_UR_SH_ENTRY WHERE DELETE_FLG = 0 AND (DAINOU_FLG IS NULL OR DAINOU_FLG = 0) AND UR_SH_NUMBER = /*data.UR_SH_NUMBER*/0")]
        DataTable GetUrshData(T_UR_SH_ENTRY data);

        /// <summary>
        /// 代納受入,代納出荷のデータ取得
        /// </summary>
        /// <param name="data">代納エンティティ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoNyuryuku.Sql.GetDainouData.sql")]
        T_UR_SH_ENTRY GetDainouData(DainoNyuryukuDTO data);

        /// <summary>
        /// 代納,代納受入,代納出荷のデータ取得
        /// </summary>
        /// <param name="data">代納エンティティ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoNyuryuku.Sql.GetDainouUkeireShukkaData.sql")]
        DataTable GetDainoUkeireShukkaData(T_UR_SH_ENTRY data);

        /// <summary>
        /// 最大代納番号を取得
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoNyuryuku.Sql.GetMaxDainouNumber.sql")]
        long GetMaxDainouNumber(T_UR_SH_ENTRY data);

        /// <summary>
        /// 最小代納番号を取得
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoNyuryuku.Sql.GetMinDainouNumber.sql")]
        long GetMinDainouNumber(T_UR_SH_ENTRY data);


        /// <summary>
        /// 一つ前の代納番号のデータ取得
        /// </summary>
        /// <param name="data">代納エンティティ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoNyuryuku.Sql.GetDainouDataFrontDainouNumber.sql")]
        T_UR_SH_ENTRY GetDainoDataForFrontDainounumber(T_UR_SH_ENTRY data);

        /// <summary>
        /// 一つ後の代納番号のデータ取得
        /// </summary>
        /// <param name="data">代納エンティティ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoNyuryuku.Sql.GetDainouDataNextDainouNumber.sql")]
        T_UR_SH_ENTRY GetDainoDataForNextDainounumber(T_UR_SH_ENTRY data);
    }

    [Bean(typeof(T_UR_SH_DETAIL))]
    public interface T_UR_SH_DETAILDao : IS2Dao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UR_SH_DETAIL data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UR_SH_DETAIL data);

        int Delete(T_UR_SH_DETAIL data);

        [Query("SYSTEM_ID = /*systemid*/0 AND SEQ = /*seq*/0")]
        List<T_UR_SH_DETAIL> GetDainouDetail(SqlInt64 systemid, SqlInt32 seq);
    }

    [Bean(typeof(M_HINMEI))]
    public interface M_HINMEIDao : IS2Dao
    {
        /// <summary>
        /// 品名情報を取得
        /// </summary>
        /// <param name="hinmeiCD"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoNyuryuku.Sql.GetHinmeiDataForShukka.sql")]
        DataTable GetHinmeiDataForShukka(string hinmeiCD);

        /// <summary>
        /// 品名情報を取得
        /// </summary>
        /// <param name="hinmeiCD"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoNyuryuku.Sql.GetHinmeiDataForUkeire.sql")]
        DataTable GetHinmeiDataForUkeire(string hinmeiCD);
    }
}
