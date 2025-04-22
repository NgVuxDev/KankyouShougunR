using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_CONTENA_KEIKA_DATE))]
    public interface IM_CONTENA_KEIKA_DATEDao : IS2Dao
    {

        [Sql("SELECT * FROM M_CONTENA_KEIKA_DATE")]
        M_CONTENA_KEIKA_DATE[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CONTENA_KEIKA_DATE data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CONTENA_KEIKA_DATE data);

        int Delete(M_CONTENA_KEIKA_DATE data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_CONTENA_KEIKA_DATE data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("GENCHAKU_TIME_CD = /*cd*/")]
        M_CONTENA_KEIKA_DATE GetDataByCd(string cd);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
