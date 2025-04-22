using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// �Ј��o�͍ς݃}�X�^Dao
    /// </summary>
    [Bean(typeof(M_DIGI_OUTPUT_SHAIN))]
    public interface IM_DIGI_OUTPUT_SHAINDao : IS2Dao
    {
        /// <summary>
        /// SQL�\������f�[�^�̍X�V���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        int ExecuteForStringSql(string sql);

        /// <summary>
        /// �d���`�F�b�N�p
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT COUNT(DIGI_SHAIN_CD) FROM M_DIGI_OUTPUT_SHAIN WHERE SHAIN_CD <> /*cdpk*/ AND DIGI_SHAIN_CD = /*cd*/ AND OUTPUT_DATE IS NOT NULL AND JYOGAI_FLG = 0")]
        int GetDuplicationCountByCd(string cdpk, string cd);

        /// <summary>
        /// �R�[�h����f�[�^���擾
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT COUNT(SHAIN_CD) FROM M_DIGI_OUTPUT_SHAIN WHERE SHAIN_CD = /*cd*/")]
        int GetDataByCd(string cd);
    }
}