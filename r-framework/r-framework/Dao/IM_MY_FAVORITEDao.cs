using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    [Bean(typeof(M_MY_FAVORITE))]
    public interface IM_MY_FAVORITEDao : IS2Dao
    {
        [Sql("SELECT * FROM M_MY_FAVORITE")]
        M_MY_FAVORITE[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.MyFavorite.IM_MY_FAVORITEDao_GetAllValidData.sql")]
        M_MY_FAVORITE[] GetAllValidData(M_MY_FAVORITE data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_MY_FAVORITE data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_MY_FAVORITE data);

        int Delete(M_MY_FAVORITE data);

        [Sql("DELETE FROM M_MY_FAVORITE WHERE BUSHO_CD = /*data.BUSHO_CD*/'' AND SHAIN_CD = /*data.SHAIN_CD*/''")]
        int DeleteByPrimaryKey(M_MY_FAVORITE data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_MY_FAVORITE data);
    }
}
