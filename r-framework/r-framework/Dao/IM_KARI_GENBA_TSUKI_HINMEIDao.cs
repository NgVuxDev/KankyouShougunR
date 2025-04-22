using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// ������_���ɕi���}�X�^Dao
    /// </summary>
    [Bean(typeof(M_KARI_GENBA_TSUKI_HINMEI))]
    public interface IM_KARI_GENBA_TSUKI_HINMEIDao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KARI_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KARI_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_KARI_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// �ƎҁA����R�[�h�����ɏ����擾
        /// </summary>
        /// <parameparam name="genbaCd">����R�[�h</parameparam>
        [Query("GYOUSHA_CD = /*gyoushaCd*/ AND GENBA_CD = /*genbaCd*/")]
        M_KARI_GENBA_TSUKI_HINMEI[] GetKariGenbaTsukiHinmeiData(string gyoushaCd, string genbaCd);

        /// <summary>
        /// �w�肳�ꂽSQL�t�@�C�����g�p���Ĉꗗ���擾���܂�
        /// </summary>
        /// <param name="path">SQL�t�@�C���̃p�X</param>
        /// <param name="entity">�����G���e�B�e�B</param>
        /// <returns>�����ꌎ�ɕi���̈ꗗ</returns>
        DataTable GetDataBySqlFile(string path, M_KARI_GENBA_TSUKI_HINMEI data);
    }
}