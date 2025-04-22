using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// トークン管理Dao
    /// </summary>
    [Bean(typeof(S_SYSTEC_TOKEN))]
    public interface IS_SYSTEC_TOKENDao : IS2Dao
    {
        /// <summary>
        /// 全データを取得する
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM S_SYSTEC_TOKEN")]
        S_SYSTEC_TOKEN[] GetAllData();

        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(S_SYSTEC_TOKEN data);

        /// <summary>
        /// Entityを元に更新処理を行う
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(S_SYSTEC_TOKEN data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(S_SYSTEC_TOKEN data);

        /// <summary>
        /// ユーザIDをもとにデータを取得する
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>取得したデータ</returns>
        [Query("USER_ID = /*userId*/")]
        S_SYSTEC_TOKEN GetDataByUserId(string userId);
    }
}
