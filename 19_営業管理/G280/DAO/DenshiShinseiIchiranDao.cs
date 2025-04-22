using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.DenshiShinseiIchiran
{
    /// <summary>
    /// G280 申請一覧DAOインタフェース
    /// </summary>
    [Bean(typeof(T_DENSHI_SHINSEI_ENTRY))]
    public interface IDenshiShinseiIchiranDao : IS2Dao
    {
        [SqlFile("Shougun.Core.BusinessManagement.DenshiShinseiIchiran.Sql.GetDenshiShinseiIchiran.sql")]
        DataTable GetDenshiShinseiIchiran(DenshiShinseiIchiranDto dto);
    }
}
