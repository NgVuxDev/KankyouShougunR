using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_MANIFEST_KANSAN))]
    public interface IM_MANIFEST_KANSANDao : IS2Dao
    {

        [Sql("SELECT * FROM M_MANIFEST_KANSAN")]
        M_MANIFEST_KANSAN[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.ManifestKansan.IM_MANIFEST_KANSANDao_GetAllValidData.sql")]
        M_MANIFEST_KANSAN[] GetAllValidData(M_MANIFEST_KANSAN data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����(�}�X�^���ʃ|�b�v�A�b�v)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.ManifestKansan.IM_MANIFEST_KANSANDao_GetAllValidDataForPopUp.sql")]
        DataTable GetAllValidDataForPopUp(M_BANK_SHITEN data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_MANIFEST_KANSAN data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_MANIFEST_KANSAN data);

        int Delete(M_MANIFEST_KANSAN data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_MANIFEST_KANSAN data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// �g�p���@����
        /// </summary>
        /// <param name="data"></param>
        /// <returns>�擾�����f�[�^</returns>
        [Query("HOUKOKUSHO_BUNRUI_CD = /*data.HOUKOKUSHO_BUNRUI_CD*/ and HAIKI_NAME_CD = /*data.HAIKI_NAME_CD*/ and UNIT_CD = /*data.UNIT_CD*/ and NISUGATA_CD = /*data.NISUGATA_CD*/")]
        M_MANIFEST_KANSAN GetDataByCd(M_MANIFEST_KANSAN data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_MANIFEST_KANSAN data, bool deletechuFlg);

        [Sql("select M_MANIFEST_KANSAN.HOUKOKUSHO_BUNRUI_CD,M_MANIFEST_KANSAN.HAIKI_NAME_CD FROM M_MANIFEST_KANSAN /*$whereSql*/ group by  M_MANIFEST_KANSAN.HOUKOKUSHO_BUNRUI_CD,M_MANIFEST_KANSAN.HAIKI_NAME_CD")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̍X�V�����ɂ��f�[�^�X�V
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <param name="updateKey"></param>
        /// <returns></returns>
        int UpdateBySqlFile(string path, M_MANIFEST_KANSAN data, M_MANIFEST_KANSAN updateKey);
    }
}
