using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// ���q�o�͍ς݃}�X�^Dao
    /// </summary>
    [Bean(typeof(M_DIGI_OUTPUT_SHARYOU))]
    public interface IM_DIGI_OUTPUT_SHARYOUDao : IS2Dao
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
        [Query("SELECT COUNT(DIGI_SHARYOU_CD) FROM M_DIGI_OUTPUT_SHARYOU WHERE (GYOUSHA_CD <> /*cdpk1*/ OR SHARYOU_CD <> /*cdpk2*/) AND DIGI_SHARYOU_CD = /*cd*/ AND OUTPUT_DATE IS NOT NULL AND JYOGAI_FLG = 0")]
        int GetDuplicationCountByCd(string cdpk1, string cdpk2, string cd);

        /// <summary>
        /// �R�[�h����f�[�^���擾
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT COUNT(SHARYOU_CD) FROM M_DIGI_OUTPUT_SHARYOU WHERE GYOUSHA_CD = /*cd1*/ AND SHARYOU_CD = /*cd2*/")]
        int GetDataByCd(string cd1, string cd2);

        /// <summary>
        /// �f�W�^�R�A�g���p�̑S�f�[�^���擾
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_DIGI_OUTPUT_SHARYOU")]
        M_DIGI_OUTPUT_SHARYOU[] GetAllData();
    }
}