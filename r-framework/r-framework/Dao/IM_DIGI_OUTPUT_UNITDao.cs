using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// �P�ʏo�͍ς݃}�X�^Dao
    /// </summary>
    [Bean(typeof(M_DIGI_OUTPUT_UNIT))]
    public interface IM_DIGI_OUTPUT_UNITDao : IS2Dao
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
        [Query("SELECT COUNT(DIGI_UNIT_CD) FROM M_DIGI_OUTPUT_UNIT WHERE UNIT_CD <> /*cdpk*/ AND DIGI_UNIT_CD = /*cd*/ AND OUTPUT_DATE IS NOT NULL AND JYOGAI_FLG = 0")]
        int GetDuplicationCountByCd(string cdpk, string cd);

        /// <summary>
        /// �R�[�h����f�[�^���擾
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT COUNT(UNIT_CD) FROM M_DIGI_OUTPUT_UNIT WHERE UNIT_CD = /*cd*/")]
        int GetDataByCd(string cd);

        /// <summary>
        /// �f�W�^�R�A�g�P�ʂ̑S�f�[�^���擾
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_DIGI_OUTPUT_UNIT")]
        M_DIGI_OUTPUT_UNIT[] GetAllData();
    }
}