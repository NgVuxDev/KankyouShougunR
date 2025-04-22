using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Shougun.Core.Billing.GetsujiShori
{
    [Bean(typeof(T_MONTHLY_LOCK_UR))]
    public interface IT_GETSUJI_SHORIDao : IS2Dao
    {
        /// <summary>
        /// 指定した期間内に受入・出荷・売上/支払伝票で未確定または滞留伝票が存在するかをチェックします
        /// </summary>
        /// <param name="FROM_DATE">[任意] 検索期間From(yyyy/MM/dd形式)</param>
        /// <param name="TO_DATE">検索期間To(yyyy/MM/dd形式)</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.GetsujiShori.Sql.CheckNotAvailableDenpyou.sql")]
        DataTable CheckExistNotAvailableDenpyou(string FROM_DATE, string TO_DATE);
    }

    [Bean(typeof(T_MONTHLY_LOCK_UR))]
    public interface IT_MONTHLY_LOCK_URDao : IS2Dao
    {
        /// <summary>
        /// 削除フラグが立っていないデータを取得します
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.GetsujiShori.Sql.GetMonthlyLockUrData.sql")]
        T_MONTHLY_LOCK_UR[] GetAllValidData(T_MONTHLY_LOCK_UR data);

        /// <summary>
        /// [登録処理用] 指定した期間内の削除データも含めたSEQが最大の全取引先データを取得します
        /// </summary>
        /// <param name="YEAR"></param>
        /// <param name="MONTH"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.GetsujiShori.Sql.GetLatestMonthlyLockUrData.sql")]
        DataTable GetLatestMonthlyLockUrData(int YEAR, int MONTH);

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MONTHLY_LOCK_UR data);

        /// <summary>
        /// 更新処理（"CREATE_USER", "CREATE_DATE", "CREATE_PC"、タイムスタンプ列を更新対象に含めない）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MONTHLY_LOCK_UR data);
    }

    [Bean(typeof(T_MONTHLY_LOCK_SH))]
    public interface IT_MONTHLY_LOCK_SHDao : IS2Dao
    {
        /// <summary>
        /// 削除フラグが立っていないデータを取得します
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.GetsujiShori.Sql.GetMonthlyLockShData.sql")]
        T_MONTHLY_LOCK_SH[] GetAllValidData(T_MONTHLY_LOCK_SH data);

        /// <summary>
        /// [登録処理用] 指定した期間内の削除データも含めたSEQが最大の全取引先データを取得します
        /// </summary>
        /// <param name="YEAR"></param>
        /// <param name="MONTH"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.GetsujiShori.Sql.GetLatestMonthlyLockShData.sql")]
        DataTable GetLatestMonthlyLockShData(int YEAR, int MONTH);

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MONTHLY_LOCK_SH data);

        /// <summary>
        /// 更新処理（"CREATE_USER", "CREATE_DATE", "CREATE_PC"、タイムスタンプ列を更新対象に含めない）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MONTHLY_LOCK_SH data);
    }

    [Bean(typeof(T_MONTHLY_ADJUST_UR))]
    public interface IT_MONTHLY_ADJUST_URDao : IS2Dao
    {
        /// <summary>
        /// 削除フラグが立っていないデータを取得します
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.GetsujiShori.Sql.GetMonthlyAdjustUrData.sql")]
        T_MONTHLY_ADJUST_UR[] GetAllValidData(T_MONTHLY_ADJUST_UR data);

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MONTHLY_ADJUST_UR data);

        /// <summary>
        /// 更新処理（"CREATE_USER", "CREATE_DATE", "CREATE_PC"、タイムスタンプ列を更新対象に含めない）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MONTHLY_ADJUST_UR data);
    }

    [Bean(typeof(T_MONTHLY_ADJUST_SH))]
    public interface IT_MONTHLY_ADJUST_SHDao : IS2Dao
    {
        /// <summary>
        /// 削除フラグが立っていないデータを取得します
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.GetsujiShori.Sql.GetMonthlyAdjustShData.sql")]
        T_MONTHLY_ADJUST_SH[] GetAllValidData(T_MONTHLY_ADJUST_SH data);

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MONTHLY_ADJUST_SH data);

        /// <summary>
        /// 更新処理（"CREATE_USER", "CREATE_DATE", "CREATE_PC"、タイムスタンプ列を更新対象に含めない）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MONTHLY_ADJUST_SH data);
    }

    [Bean(typeof(T_MONTHLY_LOCK_ZAIKO))]
    public interface IT_MONTHLY_LOCK_ZAIKODao : IS2Dao
    {
        /// <summary>
        /// 削除フラグが立っていないデータを取得します
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.GetsujiShori.Sql.GetMonthlyLockZaikoData.sql")]
        T_MONTHLY_LOCK_ZAIKO[] GetAllValidData(T_MONTHLY_LOCK_ZAIKO data);

        /// <summary>
        /// [登録処理用] 指定した期間内の削除データも含めたSEQが最大の全取引先データを取得します
        /// </summary>
        /// <param name="YEAR"></param>
        /// <param name="MONTH"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.GetsujiShori.Sql.GetLatestMonthlyLockZaikoData.sql")]
        DataTable GetLatestMonthlyLockZaikoData(int YEAR, int MONTH);

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MONTHLY_LOCK_ZAIKO data);

        /// <summary>
        /// 更新処理（"CREATE_USER", "CREATE_DATE", "CREATE_PC"、タイムスタンプ列を更新対象に含めない）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MONTHLY_LOCK_ZAIKO data);
    }
}
