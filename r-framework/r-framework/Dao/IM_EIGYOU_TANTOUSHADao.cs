using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// �c�ƒS���҃}�X�^��Dao�N���X
    /// </summary>
    [Bean(typeof(M_EIGYOU_TANTOUSHA))]
    public interface IM_EIGYOU_TANTOUSHADao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_EIGYOU_TANTOUSHA data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_EIGYOU_TANTOUSHA data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_EIGYOU_TANTOUSHA data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM M_EIGYOU_TANTOUSHA")]
        M_EIGYOU_TANTOUSHA[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.EigyouTantousha.IM_EIGYOU_TANTOUSHADao_GetAllValidData.sql")]
        M_EIGYOU_TANTOUSHA[] GetAllValidData(M_EIGYOU_TANTOUSHA data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_EIGYOU_TANTOUSHA data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SHAIN_CD = /*cd*/")]
        M_EIGYOU_TANTOUSHA GetDataByCd(string cd);

        [SqlFile("r-framework.Dao.SqlFile.EigyouTantousha.IM_EIGYOU_TANTOUSHADao_GetAllMasterDataForPopup.sql")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_EIGYOU_TANTOUSHA data, bool deletechuFlg);
    }
}