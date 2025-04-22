using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.DenshiShinSeiKeiroName.Dao
{
    /// <summary>
    /// 申請経路名入力マスタDao
    /// </summary>
    [Bean(typeof(M_DENSHI_SHINSEI_ROUTE_NAME))]
    public interface DaoCls : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_SHINSEI_ROUTE_NAME data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_SHINSEI_ROUTE_NAME data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_DENSHI_SHINSEI_ROUTE_NAME data);

        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_DENSHI_SHINSEI_ROUTE_NAME")]
        M_DENSHI_SHINSEI_ROUTE_NAME[] GetAllData();

        /// <summary>
        /// 現場コードを元に現場情報を取得
        /// </summary>
        /// <param name="data"></param>
        [Query("DENSHI_SHINSEI_ROUTE_CD = /*cd*/")]
        M_DENSHI_SHINSEI_ROUTE_NAME GetDataByCd(string cd);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_DENSHI_SHINSEI_ROUTE_NAME data);

        /// <summary>
        /// 申請経路名入力画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.DenshiShinSeiKeiroName.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_DENSHI_SHINSEI_ROUTE_NAME data, bool deletechuFlg);
    }
}