using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// �Ј��o�͍ς݃}�X�^Dao
    /// </summary>
    [Bean(typeof(M_NAVI_OUTPUT_SHAIN))]
    public interface IM_NAVI_OUTPUT_SHAINDao : IS2Dao
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
        [Query("SELECT COUNT(NAVI_SHAIN_CD) FROM M_NAVI_OUTPUT_SHAIN WHERE SHAIN_CD <> /*cdpk*/ AND NAVI_SHAIN_CD = /*cd*/ AND OUTPUT_DATE IS NOT NULL AND JYOGAI_FLG = 0")]
        int GetDuplicationCountByCd(string cdpk, string cd);

        /// <summary>
        /// �R�[�h����f�[�^���擾
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT COUNT(SHAIN_CD) FROM M_NAVI_OUTPUT_SHAIN WHERE SHAIN_CD = /*cd*/")]
        int GetDataByCd(string cd);

        /// <summary>
        /// �R�[�h����f�[�^���擾
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT NAVI_SHAIN_CD FROM M_NAVI_OUTPUT_SHAIN WHERE OUTPUT_DATE IS NOT NULL AND JYOGAI_FLG = 0 AND SHAIN_CD = /*cd*/")]
        string GetStringDataByCd(string cd);

        /// <summary>
        /// �R�[�h����f�[�^���擾
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        DataTable GetDataForStringSql(string sql);

        /// <summary>
        /// �R�[�h����f�[�^���擾
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT COUNT(SHAIN_CD) FROM M_NAVI_OUTPUT_SHAIN WHERE NAVI_SHAIN_CD = /*cd*/ AND OUTPUT_DATE IS NOT NULL AND JYOGAI_FLG = 0")]
        int GetDataNaviCd(string cd);
    }
}