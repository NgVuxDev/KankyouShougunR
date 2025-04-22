using System.Data;
using r_framework.Dao;
using Seasar.Dao.Attrs;
using Shougun.Core.Stock.ZaikoShimeSyori.DTO;
using Shougun.Core.Stock.ZaikoShimeSyori.Entity;
using r_framework.Entity;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Stock.ZaikoShimeSyori.DAO
{
    /// <summary>
    /// 締情報操作関連DAO
    /// </summary>
    [Bean(typeof(object))]
    public interface ShimeInfoDao : IS2Dao
    {
        /// <summary>
        /// 締情報を取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Stock.ZaikoShimeSyori.Sql.SearchSimeInfo.sql")]
        DataTable searchShimeInfo(F18_G170Dto data);

        /// <summary>
        /// 在庫締データを削除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Stock.ZaikoShimeSyori.Sql.DeleteTZaikoTank.sql")]
        int deleteTZaikoTank(F18_G170Dto data);

        /// <summary>
        /// 在庫締明細を削除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Stock.ZaikoShimeSyori.Sql.DeleteTZaikoTankDetail.sql")]
        int deleteTZaikoTankDetail(F18_G170Dto data);

    }
}
