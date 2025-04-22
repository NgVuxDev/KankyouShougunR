using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PaymentManagement.KaikakekinItiranHyo.DAO
{
    /// <summary>
    /// 買掛金一覧表Dao
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI))]
    public interface IT_KAKEKIN_ICHIRANDao : IS2Dao
    {
        /// <summary>
        /// 明細表示用一覧データテーブルの取得
        /// </summary>
        /// <param name="startCD">開始取引先CD</param>
        /// <param name="endCD">終了取引先CD</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaymentManagement.KaikakekinItiranHyo.Sql.GetIchiranMonthlyData.sql")]
        DataTable GetIchiranMonthlyData(string startCD, string endCD, int year, int month);

        /// <summary>
        /// 明細表示用の取引先マスタテーブルの取得
        /// </summary>
        /// <param name="startCD">開始取引先CD</param>
        /// <param name="endCD">終了取引先CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaymentManagement.KaikakekinItiranHyo.Sql.GetTorihikisakiShiharaiList.sql")]
        DataTable GetTorihikisakiShiharaiList(string startCD, string endCD);
    }
}
