using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// ���[�]���ݒ�Dao
    /// </summary>
    [Bean(typeof(S_DEFAULTMARGINSETTINGS))]
    public interface IS_DEFAULTMARGINSETTINGSDao : IS2Dao
    {
        /// <summary>
        /// ���ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM S_DEFAULTMARGINSETTINGS")]
        S_DEFAULTMARGINSETTINGS[] GetAllData();

        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(S_DEFAULTMARGINSETTINGS data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(S_DEFAULTMARGINSETTINGS data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        int Delete(S_DEFAULTMARGINSETTINGS data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, S_DEFAULTMARGINSETTINGS data);
    }
}
