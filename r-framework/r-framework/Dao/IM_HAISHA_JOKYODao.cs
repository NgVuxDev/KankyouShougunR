using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 配車状況Dao
    /// </summary>
    [Bean(typeof(M_HAISHA_JOKYO))]
    public interface IM_HAISHA_JOKYODao : IS2Dao
    {
        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_HAISHA_JOKYO")]
        M_HAISHA_JOKYO[] GetAllData();

        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_HAISHA_JOKYO data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_HAISHA_JOKYO data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_HAISHA_JOKYO data);

        /// <summary>
        /// 削除されていない配車状況データをすべて取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        M_HAISHA_JOKYO[] GetData();

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_HAISHA_JOKYO data);

        /// <summary>
        /// コードを元に配車状況データを取得する
        /// </summary>
        /// <parameparam name="cd">配車状況コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("M_HAISHA_JOKYO = /*code*/")]
        M_HAISHA_JOKYO GetDataByCd(string code);
    }
}
