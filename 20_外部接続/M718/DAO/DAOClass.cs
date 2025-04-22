using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuShinseiKeiroName.Dao
{
    [Bean(typeof(M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME data);

        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME")]
        M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME[] GetAllData();

        /// <summary>
        /// 社内経路入力画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.DenshiKeiyakuShinseiKeiroName.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME data, bool deletechuFlg);

        /// <summary>
        /// 社内経路名CDを元にデータを取得
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD = /*cd*/")]
        M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME GetDataByCd(string cd);
    }
}
