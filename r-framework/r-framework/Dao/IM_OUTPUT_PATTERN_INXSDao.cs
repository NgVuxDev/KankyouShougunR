using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_OUTPUT_PATTERN_INXS))]
    public interface IM_OUTPUT_PATTERN_INXSDao : IS2Dao
    {

        [Sql("SELECT * FROM M_OUTPUT_PATTERN_INXS")]
        M_OUTPUT_PATTERN_INXS[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_OUTPUT_PATTERN_INXS data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_OUTPUT_PATTERN_INXS data);

        int Delete(M_OUTPUT_PATTERN_INXS data);

        DataTable GetDataBySqlFile(string path, M_OUTPUT_PATTERN_INXS data);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.OutputPatternInxs.IM_OUTPUT_PATTERN_INXSDao_GetAllValidData.sql")]
        M_OUTPUT_PATTERN_INXS[] GetAllValidData(M_OUTPUT_PATTERN_INXS data);

    }
}
