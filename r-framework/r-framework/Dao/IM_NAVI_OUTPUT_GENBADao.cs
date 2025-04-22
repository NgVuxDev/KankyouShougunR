using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// ����o�͍ς݃}�X�^Dao
    /// </summary>
    [Bean(typeof(M_NAVI_OUTPUT_GENBA))]
    public interface IM_NAVI_OUTPUT_GENBADao : IS2Dao
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
        [Query("SELECT COUNT(GENBA_CD) FROM M_NAVI_OUTPUT_GENBA WHERE GYOUSHA_CD = /*cd1*/ AND GENBA_CD = /*cd2*/")]
        int GetDataByCd(string cd1, string cd2);

        /// <summary>
        /// �f�W�^�R�A�g���p�̑S�f�[�^���擾
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_NAVI_OUTPUT_GENBA")]
        M_NAVI_OUTPUT_GENBA[] GetAllData();
    }
}