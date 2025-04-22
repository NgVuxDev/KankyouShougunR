using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_OUTPUT_PATTERN_COLUMN))]
    public interface IM_OUTPUT_PATTERN_COLUMNDao : IS2Dao
    {

        [Sql("SELECT * FROM M_OUTPUT_PATTERN_COLUMN")]
        M_OUTPUT_PATTERN_COLUMN[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_OUTPUT_PATTERN_COLUMN data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_OUTPUT_PATTERN_COLUMN data);

        int Delete(M_OUTPUT_PATTERN_COLUMN data);

        DataTable GetDataBySqlFile(string path, M_OUTPUT_PATTERN_COLUMN data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.OutputPatternColumn.IM_OUTPUT_PATTERN_COLUMNDao_GetAllValidData.sql")]
        M_OUTPUT_PATTERN_COLUMN[] GetAllValidData(M_OUTPUT_PATTERN_COLUMN data);
    }
}
