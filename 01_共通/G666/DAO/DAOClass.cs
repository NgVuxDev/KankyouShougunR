using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Common.CtiRenkeiSettei.DAO
{
    /// <summary>
    /// VIEWを処理する
    /// </summary>
    [Bean(typeof(SuperEntity))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// 新規VIEWをする
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.CtiRenkeiSettei.Sql.CreateCTICONNECT_DATA.sql")]
        int CreateCticonnect(string value);

        /// <summary>
        /// Viewが存在する場合、削除する
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.CtiRenkeiSettei.Sql.DropCTICONNECT_DATA.sql")]
        int DropCticonnect(string value);
    }
}