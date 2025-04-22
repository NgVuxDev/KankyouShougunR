using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Collections.Generic;

namespace Shougun.Core.BusinessManagement.ShouninzumiDenshiShinseiIchiran
{
    /// <summary>
    /// G561 承認済電子申請一覧DAOインタフェース
    /// </summary>
    [Bean(typeof(T_DENSHI_SHINSEI_ENTRY))]
    public interface IShouninzumiDenshiShinseiIchiranDao : IS2Dao
    {
        [SqlFile("Shougun.Core.BusinessManagement.ShouninzumiDenshiShinseiIchiran.Sql.GetShouninzumiDenshiShinseiIchiran.sql")]
        DataTable GetShouninzumiDenshiShinseiIchiran(ShouninzumiDenshiShinseiIchiranDto dto);

        [SqlFile("Shougun.Core.BusinessManagement.ShouninzumiDenshiShinseiIchiran.Sql.GetIchiranData.sql")]
        DataTable GetIchiranData(ShouninzumiDenshiShinseiIchiranDto dto);
    }

    [Bean(typeof(T_DENSHI_SHINSEI_STATUS))]
    public interface IDenshiShinseiStatusDao : IS2Dao
    {
        List<T_DENSHI_SHINSEI_STATUS> GetDenshiShinseiStatusList(T_DENSHI_SHINSEI_STATUS entity);
    }
}
