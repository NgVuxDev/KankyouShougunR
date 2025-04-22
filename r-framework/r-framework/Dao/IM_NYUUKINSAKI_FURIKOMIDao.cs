using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_NYUUKINSAKI_FURIKOMI))]
    public interface IM_NYUUKINSAKI_FURIKOMIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_NYUUKINSAKI_FURIKOMI")]
        M_NYUUKINSAKI_FURIKOMI[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_NYUUKINSAKI_FURIKOMI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_NYUUKINSAKI_FURIKOMI data);

        int Delete(M_NYUUKINSAKI_FURIKOMI data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_NYUUKINSAKI_FURIKOMI data);

        /// <summary>
        /// 入金先CDを元にデータの取得を行う
        /// </summary>
        /// <parameparam name="nyuukinsakiCd">入金先コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("NYUUKINSAKI_CD = /*nyuukinsakiCd*/")]
        M_NYUUKINSAKI_FURIKOMI[] GetDataByNyuukinsakiCd(string nyuukinsakiCd);
    }
}
