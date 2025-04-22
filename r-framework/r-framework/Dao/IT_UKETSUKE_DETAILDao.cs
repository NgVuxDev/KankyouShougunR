using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 受付明細データDao
    /// </summary>
    [Bean(typeof(T_UKETSUKE_DETAIL))]
    public interface IT_UKETSUKE_DETAILDao : IS2Dao
    {
        /// <summary>
        /// 全レコード取得処理
        /// </summary>
        [Sql("SELECT * FROM T_UKETSUKE_DETAIL")]
        T_UKETSUKE_DETAIL[] GetAllData();

        /// <summary>
        /// insert処理
        /// </summary>
        int Insert(T_UKETSUKE_DETAIL data);

        /// <summary>
        /// 更新処理（"CREATE_USER", "CREATE_DATE", "CREATE_PC"を更新対象に含めない）
        /// </summary>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UKETSUKE_DETAIL data);

        /// <summary>
        /// 削除処理
        /// </summary>
        int Delete(T_UKETSUKE_DETAIL data);

        /// <summary>
        /// 論理削除フラグ更新処理（"DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC"のみを更新する）
        /// </summary>
        [PersistentProps("DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC")]
        int UpdateLogicalDeleteFlag(T_UKETSUKE_DETAIL data);

        /// <summary>
        /// 受付番号から対象の明細データを取得する
        /// </summary>
        [Query("UKETSUKE_NO = /*UketsukeNo*/")]
        T_UKETSUKE_DETAIL[] GetUketsukeData(int uketsukeNo);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, T_UKETSUKE_DETAIL data);
    }
}