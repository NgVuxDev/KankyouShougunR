

using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    /// <summary>
    /// すべてのDaoにて必須となるメソッドの定義を行うインタフェース
    /// </summary>
    public interface BaseDao
    {
        /// <summary>
        /// Insert処理
        /// </summary>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(SuperEntity data);

        /// <summary>
        /// 更新処理（"CREATE_USER", "CREATE_DATE", "CREATE_PC"を更新対象に含めない）
        /// </summary>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(SuperEntity data);

        /// <summary>
        /// レコード削除処理
        /// </summary>
        int Delete(SuperEntity data);

        DataTable GetAllMasterDataForPopup(string whereSql);

        SuperEntity GetDataForEntity(SuperEntity date);

        DataTable GetAllValidDataForPopUp(SuperEntity data);

        DataTable GetDateForStringSql(string sql);
    }
}
