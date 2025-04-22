using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// �Ԏ�o�͍ς݃}�X�^Dao
    /// </summary>
    [Bean(typeof(M_DIGI_OUTPUT_SHASHU))]
    public interface IM_DIGI_OUTPUT_SHASHUDao : IS2Dao
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
        [Query("SELECT COUNT(DIGI_SHASHU_CD) FROM M_DIGI_OUTPUT_SHASHU WHERE SHASHU_CD <> /*cdpk*/ AND DIGI_SHASHU_CD = /*cd*/ AND OUTPUT_DATE IS NOT NULL AND JYOGAI_FLG = 0")]
        int GetDuplicationCountByCd(string cdpk, string cd);

        /// <summary>
        /// �R�[�h����f�[�^���擾
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT COUNT(SHASHU_CD) FROM M_DIGI_OUTPUT_SHASHU WHERE SHASHU_CD = /*cd*/")]
        int GetDataByCd(string cd);
        
    }
}