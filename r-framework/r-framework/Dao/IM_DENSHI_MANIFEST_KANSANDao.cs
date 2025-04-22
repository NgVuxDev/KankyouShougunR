using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_DENSHI_MANIFEST_KANSAN))]
    public interface IM_DENSHI_MANIFEST_KANSANDao : IS2Dao
    {
        /// <summary>
        /// Insert����
        /// </summary>
        /// <param name="data">Insert�Ώ�Entity</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_MANIFEST_KANSAN data);

        /// <summary>
        /// �X�V����
        /// </summary>
        /// <param name="data">�X�V�Ώ�Entity</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_MANIFEST_KANSAN data);

        /// <summary>
        /// �폜����
        /// </summary>
        /// <param name="data">�폜�Ώ�Entity</param>
        /// <returns></returns>
        int Delete(M_DENSHI_MANIFEST_KANSAN data);

        /// <summary>
        /// �S�f�[�^���擾
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_DENSHI_MANIFEST_KANSAN")]
        M_DENSHI_MANIFEST_KANSAN[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">��������</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.ManifestKansan.IM_DENSHI_MANIFEST_KANSANDao_GetAllValidData.sql")]
        M_DENSHI_MANIFEST_KANSAN[] GetAllValidData(M_DENSHI_MANIFEST_KANSAN data);

        /// <summary>
        /// �R�[�h����Ƀf�[�^���擾����
        /// </summary>
        /// <param name="data">��������</param>
        /// <returns>�擾�����f�[�^</returns>
        [Query("EDI_MEMBER_ID = /*data.EDI_MEMBER_ID*/ and HAIKI_SHURUI_CD = /*data.HAIKI_SHURUI_CD*/ and HAIKI_SHURUI_SAIBUNRUI_CD = /*data.HAIKI_SHURUI_SAIBUNRUI_CD*/ and UNIT_CD = /*data.UNIT_CD*/")]
        M_DENSHI_MANIFEST_KANSAN GetDataByCd(M_DENSHI_MANIFEST_KANSAN data);
    }
}
