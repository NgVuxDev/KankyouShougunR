using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Common.BusinessCommon.Dao
{
    /// <summary>
    /// 電子申請状態DAO
    /// 複数の画面から呼ばれるため共通化
    /// </summary>
    [Bean(typeof(T_DENSHI_SHINSEI_STATUS))]
    public interface IT_DENSHI_SHINSEI_STATUSDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_DENSHI_SHINSEI_STATUS data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_DENSHI_SHINSEI_STATUS data);

        /// <summary>
        /// IT_DENSHI_SHINSEI_ENTRYの情報を利用して、T_DENSHI_SHINSEI_STATUSを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiShinseiSql.GetAllValidData.sql")]
        DataTable GetAllValidData(T_DENSHI_SHINSEI_ENTRY data);

        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiShinseiSql.GetAllValidDataMinCols.sql")]
        DataTable GetAllValidDataMinCols(T_DENSHI_SHINSEI_ENTRY data);
    }
}