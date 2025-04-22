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

namespace Shougun.Core.PaperManifest.SousinnHoryuuPopup
{

    [Bean(typeof(T_LAST_SBN_SUSPEND))]
    public interface GetlastSbnSusPendDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_LAST_SBN_SUSPEND data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_LAST_SBN_SUSPEND data);
    }

    [Bean(typeof(QUE_INFO))]
    public interface GetQueDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS", "TIME_STAMP")]
        int Insert(QUE_INFO data);

        /// <summary>
        /// キュー情報レコード最大枝番検索
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.SousinnHoryuuPopup.Sql.GetQueInfoMaxSeq.sql")]
        new DataTable GetMaxSeq(GetMaxSeqDtoCls data);
    }

    [Bean(typeof(DT_D12))]
    public interface GetmanifastDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS", "TIME_STAMP")]
        int Insert(DT_D12 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_D12 data);

        /// <summary>
        /// D12 2次マニフェスト情報レコード最大枝番検索
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.SousinnHoryuuPopup.Sql.GemanifastMaxSeq.sql")]
        new DataTable GetMaxSeq(GetMaxSeqDtoCls data);

        /// <summary>
        /// KANRI_IDを元にデータを取得する
        /// </summary>
        /// <param name="kanriId"></param>
        /// <returns></returns>
        [Query("KANRI_ID = /*kanriId*/")]
        DT_D12[] GetD12(string kanriId);
    }

    [Bean(typeof(DT_D13))]
    public interface GetjigyoubaDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS", "TIME_STAMP")]
        int Insert(DT_D13 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_D13 data);

        /// <summary>
        /// D13 最終処分終了日・事業場情報レコード最大枝番検索
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.SousinnHoryuuPopup.Sql.GetjigyoubaMaxSeq.sql")]
        new DataTable GetMaxSeq(GetMaxSeqDtoCls data);

        /// <summary>
        /// KANRI_IDを元にデータを取得する
        /// </summary>
        /// <param name="kanriId"></param>
        /// <returns></returns>
        [Query("KANRI_ID = /*kanriId*/")]
        DT_D13[] GetD13(string kanriId);
    }

    [Bean(typeof(DT_MF_TOC))]
    public interface GetmokujiDaoCls : IS2Dao
    {
        /// <summary>
        /// マニフェスト目次情報更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("MANIFEST_ID", "LATEST_SEQ",
            "APPROVAL_SEQ", "STATUS_FLAG", "READ_FLAG", "KIND",
            "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_TS")]
        int Update(DT_MF_TOC data);
    }

    /// <summary>
    /// DT_R18のデータを取得
    /// </summary>
    [Bean(typeof(DT_R18))]
    public interface GeElecManiDaoCls : IS2Dao
    {
        /// <summary>
        /// DT_R18からデータを取得
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.SousinnHoryuuPopup.Sql.GetElecManiData.sql")]
        new DataTable GetElecManiData(List<string> data);
    }

}
