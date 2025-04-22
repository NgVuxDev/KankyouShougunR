// $Id: LogicClass.cs 24958 2014-07-08 06:41:18Z nagata $
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using Shougun.Core.ExternalConnection.DenshiKeiyakuShinseiKeiro.DTO;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuShinseiKeiro.DAO
{
    [Bean(typeof(M_DENSHI_KEIYAKU_SHANAI_KEIRO))]
    public interface DaoClass : IS2Dao
    {
        /// <summary>
        /// すべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_DENSHI_KEIYAKU_SHANAI_KEIRO")]
        M_DENSHI_KEIYAKU_SHANAI_KEIRO[] GetAllData();

        /// <summary>
        /// 社内経路入力画面用の一覧データを取得
        /// </summary>
        /// <param name="data">検索用DTO</param>
        /// <returns name="DataTable">検索結果</returns>
        [SqlFile("Shougun.Core.ExternalConnection.DenshiKeiyakuShinseiKeiro.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(DenshiKeiyakuShanaiKeiroFindDto data);
    }
}