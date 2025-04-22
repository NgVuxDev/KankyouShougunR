using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    /// <summary>
    /// �G���[���b�Z�[�W�}�X�^��Dao�N���X
    /// </summary>
    [Bean(typeof(M_ERROR_MESSAGE))]
    public interface IM_ERROR_MESSAGEDao : IS2Dao
    {
        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM M_ERROR_MESSAGE")]
        M_ERROR_MESSAGE[] GetAllData();

        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ERROR_MESSAGE data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_ERROR_MESSAGE data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_ERROR_MESSAGE data);

        /// <summary>
        /// ���b�Z�[�WID������DB���烁�b�Z�[�W���擾���郁�\�b�h
        /// </summary>
        /// <parameparam name="messageId">���b�Z�[�WID</parameparam>
        [Query("MESSAGE_ID = /*messageId*/")]
        M_ERROR_MESSAGE GetMessage(string messageId);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_ERROR_MESSAGE data);
    }
}
