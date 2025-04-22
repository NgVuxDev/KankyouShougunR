using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// �Ԏ�o�͍ς݃}�X�^Dao
    /// </summary>
    [Bean(typeof(M_NAVI_OUTPUT_SHASHU))]
    public interface IM_NAVI_OUTPUT_SHASHUDao : IS2Dao
    {
        /// <summary>
        /// SQL�\������f�[�^�̍X�V���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        int ExecuteForStringSql(string sql);

        /// <summary>
        /// �R�[�h����f�[�^���擾
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT COUNT(SHASHU_CD) FROM M_NAVI_OUTPUT_SHASHU WHERE SHASHU_CD = /*cd*/")]
        int GetDataByCd(string cd);
    }
}