using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace Shougun.Core.Allocation.HaishaMeisai
{
    [Bean(typeof(M_SHAIN))]
    public interface UntenshaNameDAOClass : IS2Dao
    {
        /// <summary>
        /// 運転者ポップアップ画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.HaishaMeisai.Sql.GetUntenshaName.sql")]
        M_SHAIN[] GetAllValidData(M_SHAIN data);
    }
}