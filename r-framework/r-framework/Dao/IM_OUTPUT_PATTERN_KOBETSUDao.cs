using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_OUTPUT_PATTERN_KOBETSU))]
    public interface IM_OUTPUT_PATTERN_KOBETSUDao : IS2Dao
    {
        [Sql("SELECT * FROM M_OUTPUT_PATTERN_KOBETSU")]
        M_OUTPUT_PATTERN_KOBETSU[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_OUTPUT_PATTERN_KOBETSU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_OUTPUT_PATTERN_KOBETSU data);

        int Delete(M_OUTPUT_PATTERN_KOBETSU data);

        DataTable GetDataBySqlFile(string path, M_OUTPUT_PATTERN_KOBETSU data);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.OutputPatternKobetsu.IM_OUTPUT_PATTERN_KOBETSUDao_GetAllValidData.sql")]
        M_OUTPUT_PATTERN_KOBETSU[] GetAllValidData(M_OUTPUT_PATTERN_KOBETSU data);
    }
}
