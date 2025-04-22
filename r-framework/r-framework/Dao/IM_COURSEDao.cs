using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_COURSE))]
    public interface IM_COURSEDao : IS2Dao
    {
        
        [Sql("SELECT * FROM M_COURSE")]
        M_COURSE[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Course.IM_COURSEDao_GetAllValidData.sql")]
        M_COURSE[] GetAllValidData(M_COURSE data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_COURSE data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_COURSE data);

        int Delete(M_COURSE data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_COURSE data);
    }
}
