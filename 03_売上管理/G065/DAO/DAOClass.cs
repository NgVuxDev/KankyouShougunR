using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.SalesManagement.UrikakekinItiranHyo.DAO
{
    /// <summary>
    /// 売掛金一覧表Dao
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI))]
    public interface IT_KAKEKIN_ICHIRANDao : IS2Dao
    {
        /// <summary>
        /// 明細表示用一覧データテーブルの取得(月次データ取得用)
        /// </summary>
        /// <param name="startCD">開始取引先CD</param>
        /// <param name="endCD">終了取引先CD</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesManagement.UrikakekinItiranHyo.Sql.GetIchiranMonthlyData.sql")]
        DataTable GetIchiranMonthlyData(string startCD, string endCD, int year, int month);

        /// <summary>
        /// 明細表示用の取引先マスタテーブルの取得
        /// </summary>
        /// <param name="startCD">開始取引先CD</param>
        /// <param name="endCD">終了取引先CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesManagement.UrikakekinItiranHyo.Sql.GetTorihikisakiSeikyuList.sql")]
        DataTable GetTorihikisakiSeikyuList(string startCD, string endCD);
    }
}
