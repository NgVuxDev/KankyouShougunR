using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_OUTPUT_PATTERN))]
    public interface IM_OUTPUT_PATTERNDao : IS2Dao
    {

        [Sql("SELECT * FROM M_OUTPUT_PATTERN")]
        M_OUTPUT_PATTERN[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_OUTPUT_PATTERN data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_OUTPUT_PATTERN data);

        int Delete(M_OUTPUT_PATTERN data);

        DataTable GetDataBySqlFile(string path, M_OUTPUT_PATTERN data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.OutputPattern.IM_OUTPUT_PATTERNDao_GetAllValidData.sql")]
        M_OUTPUT_PATTERN[] GetAllValidData(M_OUTPUT_PATTERN data);

    }
}
