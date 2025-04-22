using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_WORK_CLOSED_UNTENSHA))]
    public interface IM_WORK_CLOSED_UNTENSHADao : IS2Dao
    {

        [Sql("SELECT * FROM M_WORK_CLOSED_UNTENSHA")]
        M_WORK_CLOSED_UNTENSHA[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.WorkClosedUntensha.IM_WORK_CLOSED_UNTENSHADao_GetAllValidData.sql")]
        M_WORK_CLOSED_UNTENSHA[] GetAllValidData(M_WORK_CLOSED_UNTENSHA data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_WORK_CLOSED_UNTENSHA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_WORK_CLOSED_UNTENSHA data);

        int Delete(M_WORK_CLOSED_UNTENSHA data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_WORK_CLOSED_UNTENSHA data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_WORK_CLOSED_UNTENSHA data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);
    }
}
