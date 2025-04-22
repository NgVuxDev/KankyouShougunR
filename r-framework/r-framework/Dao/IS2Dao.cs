

using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    /// <summary>
    /// すべてのDaoにて必須となるメソッドの定義を行うインタフェース
    /// </summary>
    public interface IS2Dao
    {
        /// <summary>
        /// Insert処理
        /// </summary>
        int Insert(SuperEntity data);

        /// <summary>
        /// 更新処理（"CREATE_USER", "CREATE_DATE", "CREATE_PC"、タイムスタンプ列を更新対象に含めない）
        /// </summary>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP", "UPDATE_TS")]
        int Update(SuperEntity data);

        /// <summary>
        /// レコード削除処理
        /// </summary>
        int Delete(SuperEntity data);

        DataTable GetAllMasterDataForPopup(string whereSql);

        SuperEntity GetDataForEntity(SuperEntity date);

        DataTable GetAllValidDataForPopUp(SuperEntity data);

        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
