using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Data;

namespace r_framework.Dao
{
    /// <summary>
    /// mapbox地図アクセス回数管理Dao
    /// </summary>
    [Bean(typeof(S_MAPBOX_ACCESS_COUNT))]
    public interface IS_MAPBOX_ACCESS_COUNTDao : IS2Dao
    {
        /// <summary>
        /// 全データを取得する
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM S_MAPBOX_ACCESS_COUNT")]
        S_MAPBOX_ACCESS_COUNT[] GetAllData();

        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(S_MAPBOX_ACCESS_COUNT data);

        /// <summary>
        /// Entityを元に更新処理を行う
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //[NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        [NoPersistentProps("TIME_STAMP")]
        int Update(S_MAPBOX_ACCESS_COUNT data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(S_MAPBOX_ACCESS_COUNT data);

        /// <summary>
        /// PKをもとにデータを取得する
        /// </summary>
        /// <param name="userName">ユーザー名</param>
        /// <param name="pcName">PC名</param>
        /// <param name="menuName">メニュー名</param>
        /// <returns>取得したデータ</returns>
        [Query("USER_NAME = /*userName*/ AND PC_NAME = /*pcName*/ AND MENU_NAME = /* menuName */")]
        S_MAPBOX_ACCESS_COUNT GetDataByKey(string userName, string pcName, string menuName);

        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
