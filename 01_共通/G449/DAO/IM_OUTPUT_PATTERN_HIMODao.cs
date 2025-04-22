using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Common.DenpyouHimodukeIchiran.DAO
{
    [Bean(typeof(M_OUTPUT_PATTERN_HIMO))]
    public interface IM_OUTPUT_PATTERN_HIMODao : IS2Dao
    {

        [Sql("SELECT * FROM M_OUTPUT_PATTERN_HIMO")]
        M_OUTPUT_PATTERN_HIMO[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_OUTPUT_PATTERN_HIMO data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_OUTPUT_PATTERN_HIMO data);

        int Delete(M_OUTPUT_PATTERN_HIMO data);

        DataTable GetDataBySqlFile(string path, M_OUTPUT_PATTERN_HIMO data);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Common.DenpyouHimodukeIchiran.Sql.IM_OUTPUT_PATTERN_HIMODao_GetAllValidData.sql")]
        M_OUTPUT_PATTERN_HIMO[] GetAllValidData(M_OUTPUT_PATTERN_HIMO data);

    }
}
