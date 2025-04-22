using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// ��s�x�X�}�X�^(�R�[�h�`�F�b�N�g��)��Dao�N���X
    /// </summary>
    [Bean(typeof(M_BANK_SHITEN_FOR_CODECHECK))]
    public interface IM_BANK_SHITEN_FOR_CODECHECKDao : IS2Dao
    {
        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����(�R�[�h�`�F�b�N)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.BankShiten.IM_BANK_SHITENDao_GetAllValidDataForCodeCheck.sql")]
        M_BANK_SHITEN_FOR_CODECHECK[] GetAllValidDataForCodeCheck(M_BANK_SHITEN data);
    }
}