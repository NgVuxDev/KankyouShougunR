using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 銀行マスタのDaoクラス
    /// </summary>
    [Bean(typeof(M_SYS_PREV_VALUE))]
    public interface IM_SYS_PREV_VALUEDao : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_SYS_PREV_VALUE")]
        M_SYS_PREV_VALUE[] GetAllData();
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SYS_PREV_VALUE data);
        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SYS_PREV_VALUE data);
        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SYS_PREV_VALUE data);
        /// <summary>
        /// 銀行コードをもとに部署のデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("GAMEN_ID = /*gamenId*/")]
        M_SYS_PREV_VALUE[] GetAllByGamenId(string gamenId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gamenId"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        [Query("GAMEN_ID = /*gamenId*/ AND FIELD_NAME = /*fieldName*/")]
        M_SYS_PREV_VALUE GetById(string gamenId, string fieldName);
    }
}