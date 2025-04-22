using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 引合現場定期品名Daoインタフェース
    /// </summary>
    [Bean(typeof(M_HIKIAI_GENBA_TEIKI_HINMEI))]
    public interface IM_HIKIAI_GENBA_TEIKI_HINMEIDao : IS2Dao
    {
        /// <summary>
        /// 指定されたSQLファイルを使用して一覧を取得します
        /// </summary>
        /// <param name="path">SQLファイルのパス</param>
        /// <param name="entity">条件エンティティ</param>
        /// <returns>引合現場定期品名の一覧</returns>
        DataTable GetDataBySqlFile(string path, M_HIKIAI_GENBA_TEIKI_HINMEI entity);
    }
}
