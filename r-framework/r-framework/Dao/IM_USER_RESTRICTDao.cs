using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 
    /// </summary>
    [Bean(typeof(M_USER_RESTRICT))]
    public interface IM_USER_RESTRICTDao : IS2Dao
    {
        /// <summary>
        /// 最新SYSTEM_IDのレコードを取得
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_USER_RESTRICT WHERE SYSTEM_ID = 0 AND SEQ = (SELECT MAX(SEQ) FROM M_USER_RESTRICT)")]
        M_USER_RESTRICT GetSign();

        /// <summary>
        /// 構成情報登録
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        int Insert(string sql);
    }
}
