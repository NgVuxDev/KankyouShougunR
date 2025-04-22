
using System.Data;

namespace r_framework.Dao
{
    /// <summary>
    /// 一覧画面にて利用するDaoのベースとなるインタフェース
    /// </summary>
    public interface IchiranBaseDao : IS2Dao
    {
        /// <summary>
        /// 検索条件文字列を投げて検索を行うメソッド
        /// </summary>
        /// <parameparam name="searchString">検索条件文字列</parameparam>
        DataTable GetIchiranData(string searchString);

        /// <summary>
        /// SQL自体をStringにて動的生成を行い、実行するメソッド
        /// </summary>
        /// <parameparam name="sql">SQL文</parameparam>
        DataTable GetUserSettingData(string sql);
    }
}
