using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_KANSAN))]
    public interface IM_KANSANDao : IS2Dao
    {

        [Sql("SELECT * FROM M_KANSAN")]
        M_KANSAN[] GetAllData();
        
        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Kansan.IM_KANSANDao_GetAllValidData.sql")]
        M_KANSAN[] GetAllValidData(M_KANSAN data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����(�}�X�^���ʃ|�b�v�A�b�v)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Kansan.IM_KANSANDao_GetAllValidDataForPopUp.sql")]
        DataTable GetAllValidDataForPopUp(M_KANSAN data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KANSAN data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KANSAN data);

        int Delete(M_KANSAN data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_KANSAN data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// ���p���@����
        /// </summary>
        /// <param name="data"></param>
        /// <returns>�擾�����f�[�^</returns>
        [Query("DENPYOU_KBN_CD = /*data.DENPYOU_KBN_CD*/ and HINMEI_CD = /*data.HINMEI_CD*/ and UNIT_CD = /*data.UNIT_CD*/")]
        M_KANSAN GetDataByCd(M_KANSAN data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_KANSAN data, bool deletechuFlg);

        [Sql("select M_KANSAN.DENPYOU_KBN_CD, M_KANSAN.HINMEI_CD FROM M_KANSAN /*$whereSql*/")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̍X�V�����ɂ��f�[�^�X�V
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <param name="updateKey"></param>
        /// <returns></returns>
        int UpdateBySqlFile(string path, M_KANSAN data, M_KANSAN updateKey);
    }
}
