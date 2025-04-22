using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.DTO;
using Seasar.Dao.Attrs;
using r_framework.Dao;
using System.Data;

namespace Shougun.Core.PayByProxy.DainoDenpyoHakkou.DAO
{
    /// <summary>
    /// 代納番号から前回明細情報(代納番号が紐づいていないデータ)を取得
    /// </summary>
    [Bean(typeof(MeiseiDTOClass))]
    internal interface DainoDetailZenkaiDao : IS2Dao
    {
        /// <summary>
        /// [受入支払] 前回値を取得
        /// </summary>
        /// <param name="dainouNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoDenpyoHakkou.Sql.GetZenkaiUkeireShiharai.sql")]
        DataTable GetZenkaiUkeireShiharai(string torihikisakiCd, string startDay);

        /// <summary>
        /// [出荷売上] 前回値を取得
        /// </summary>
        /// <param name="dainouNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoDenpyoHakkou.Sql.GetZenkaiShukkaUriage.sql")]
        DataTable GetZenkaiShukkaShiharai(string torihikisakiCd, string startDay);

        /// <summary>
        /// 明細表示用一覧データテーブルの取得
        /// </summary>
        /// <param name="startCD">開始取引先CD</param>
        /// <param name="endCD">終了取引先CD</param>
        /// <param name="startDay">開始伝票日付</param>
        /// <param name="endDay">終了伝票日付</param>
        /// <param name="densyuKbn">伝種区分</param>
        /// <param name="systemID">システムID</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoDenpyoHakkou.Sql.GetSeikyuIchiranData.sql")]
        DataTable GetSeikyuIchiranData(string CD, string startDay, string endDay);
        /// <summary>
        /// 明細表示用一覧データテーブルの取得
        /// </summary>
        /// <param name="startCD">開始取引先CD</param>
        /// <param name="endCD">終了取引先CD</param>
        /// <param name="startDay">開始伝票日付</param>
        /// <param name="endDay">終了伝票日付</param>
        /// <param name="densyuKbn">伝種区分</param>
        /// <param name="systemID">システムID</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoDenpyoHakkou.Sql.GetShiharaiIchiranData.sql")]
        DataTable GetShiharaiIchiranData(string CD, string startDay, string endDay);

        /// <summary>
        /// 消費税マスタの取得
        /// </summary>
        /// <param name="syoriDate"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoDenpyoHakkou.Sql.GetSyohizei.sql")]
        string GetSyohizei(string syoriDate);
    }

}
