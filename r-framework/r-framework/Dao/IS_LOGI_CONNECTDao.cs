using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// ロジこん接続管理Dao
    /// </summary>
    [Bean(typeof(S_LOGI_CONNECT))]
    public interface IS_LOGI_CONNECTDao : IS2Dao
    {
        /// <summary>
        /// 全データを取得する
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM S_LOGI_CONNECT")]
        S_LOGI_CONNECT[] GetAllData();

        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(S_LOGI_CONNECT data);

        /// <summary>
        /// Entityを元に更新処理を行う
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(S_LOGI_CONNECT data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(S_LOGI_CONNECT data);

        /// <summary>
        /// 目次をもとにデータを取得する
        /// </summary>
        /// <param name="contentName">目次</param>
        /// <returns>取得したデータ</returns>
        [Query("CONTENT_NAME = /*contentName*/")]
        S_LOGI_CONNECT GetDataByContentName(string contentName);
    }
}
