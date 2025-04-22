using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using Shougun.Core.ExternalConnection.FileUploadIchiran.DTO;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ExternalConnection.FileUploadIchiran.DAO
{
    [Bean(typeof(T_FILE_DATA))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// ファイルアップロード一覧画面用の一覧データを取得
        /// </summary>
        /// <param name="data">検索用DTO</param>
        /// <returns name="DataTable">検索結果</returns>
        [SqlFile("Shougun.Core.ExternalConnection.FileUploadIchiran.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(SearchDTO data);

    }
}
