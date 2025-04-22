// $Id: DAOClass.cs 39629 2015-01-15 07:18:00Z sanbongi $
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Billing.GetsujiShouhizeiChouseiNyuuryoku.DAO
{
    /// <summary>
    /// 売上月次締処理情報DAO
    /// </summary>
    [Bean(typeof(T_MONTHLY_LOCK_UR))]
    internal interface IT_MONTHLY_LOCK_URDao : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="isUR">true:売上, false:支払</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.GetsujiShouhizeiChouseiNyuuryoku.Sql.GetIchiran.sql")]
        DataTable GetIchiran(int year, int month, bool isUR);

        /// <summary>
        /// 削除フラグが立っていないデータを取得します
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.GetsujiShouhizeiChouseiNyuuryoku.Sql.GetMonthlyLockUrData.sql")]
        T_MONTHLY_LOCK_UR[] GetAllValidData(T_MONTHLY_LOCK_UR data);
    }

    /// <summary>
    /// 支払月次締処理情報DAO
    /// </summary>
    [Bean(typeof(T_MONTHLY_LOCK_SH))]
    internal interface IT_MONTHLY_LOCK_SHDao : IS2Dao
    {
        /// <summary>
        /// 削除フラグが立っていないデータを取得します
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.GetsujiShouhizeiChouseiNyuuryoku.Sql.GetMonthlyLockShData.sql")]
        T_MONTHLY_LOCK_SH[] GetAllValidData(T_MONTHLY_LOCK_SH data);
    }

    /// <summary>
    /// 売上月次調整情報DAO
    /// </summary>
    [Bean(typeof(T_MONTHLY_ADJUST_UR))]
    internal interface IT_MONTHLY_ADJUST_URDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MONTHLY_ADJUST_UR data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MONTHLY_ADJUST_UR data);

        /// <summary>
        /// 論理削除フラグ更新処理
        /// </summary>
        [NoPersistentProps("ADJUST_TAX", "ZANDAKA", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int UpdateLogicalDeleteFlag(T_MONTHLY_ADJUST_UR data);

        /// <summary>
        /// 指定された年月から取引先毎の最新履歴(削除データ含む)の調整データを取得
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.GetsujiShouhizeiChouseiNyuuryoku.Sql.GetLatestSeqMonthlyAdjustUrData.sql")]
        T_MONTHLY_ADJUST_UR[] GetLatestSeqMonthlyAdjustUrData(int year, int month);
    }

    /// <summary>
    /// 支払月次調整情報DAO
    /// </summary>
    [Bean(typeof(T_MONTHLY_ADJUST_SH))]
    internal interface IT_MONTHLY_ADJUST_SHDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MONTHLY_ADJUST_SH data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MONTHLY_ADJUST_SH data);

        /// <summary>
        /// 論理削除フラグ更新処理
        /// </summary>
        [NoPersistentProps("ADJUST_TAX", "ZANDAKA", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int UpdateLogicalDeleteFlag(T_MONTHLY_ADJUST_SH data);

        /// <summary>
        /// 指定された年月から取引先毎の最新履歴(削除データ含む)の調整データを取得
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.GetsujiShouhizeiChouseiNyuuryoku.Sql.GetLatestSeqMonthlyAdjustShData.sql")]
        T_MONTHLY_ADJUST_SH[] GetLatestSeqMonthlyAdjustShData(int year, int month);
    }
}
