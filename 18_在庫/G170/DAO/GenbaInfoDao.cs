using System.Data;
using r_framework.Dao;
using Seasar.Dao.Attrs;
using Shougun.Core.Stock.ZaikoShimeSyori.DTO;
using Shougun.Core.Stock.ZaikoShimeSyori.Entity;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Stock.ZaikoShimeSyori.DAO
{
    [Bean(typeof(GenbaInfo))]
    public interface GenbaInfoDao : IS2Dao
    {
        /// <summary>
        /// 現場関連情報を取得
        /// </summary>
        /// <param name="gyoushaCD">業者CD</param>
        /// <param name="genbaCD">現場CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Stock.ZaikoShimeSyori.Sql.SearchGenbaInfo.sql")]
        GenbaInfo[] getGenbaInfo(string gyoushaCD, string genbaCD);

    }
}
