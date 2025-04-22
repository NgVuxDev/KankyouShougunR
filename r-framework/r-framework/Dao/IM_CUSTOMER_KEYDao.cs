using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 
    /// </summary>
    [Bean(typeof(M_CUSTOMER_KEY))]
    public interface IM_CUSTOMER_KEYDao : IS2Dao
    {
        /// <summary>
        /// 公開鍵取得
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_CUSTOMER_KEY")]
        M_CUSTOMER_KEY GetCustomerKey();
    }
}
