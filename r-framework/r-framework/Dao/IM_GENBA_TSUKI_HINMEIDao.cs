using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// ����_���ɕi���}�X�^Dao
    /// </summary>
    [Bean(typeof(M_GENBA_TSUKI_HINMEI))]
    public interface IM_GENBA_TSUKI_HINMEIDao : IchiranBaseDao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾(�Ǝ҃G���e�B�e�B�t)
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="gyousha">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileWithGyousha(string path, M_GENBA_TSUKI_HINMEI data, M_GYOUSHA gyousha);
    }
}