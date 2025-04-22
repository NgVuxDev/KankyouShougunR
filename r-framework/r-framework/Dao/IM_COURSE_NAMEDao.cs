using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_COURSE_NAME))]
    public interface IM_COURSE_NAMEDao : IS2Dao
    {
        
        [Sql("SELECT * FROM M_COURSE_NAME")]
        M_COURSE_NAME[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.CourseName.IM_COURSE_NAMEDao_GetAllValidData.sql")]
        M_COURSE_NAME[] GetAllValidData(M_COURSE_NAME data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_COURSE_NAME data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_COURSE_NAME data);

        int Delete(M_COURSE_NAME data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_COURSE_NAME data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_COURSE_NAME data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);
    }
}
