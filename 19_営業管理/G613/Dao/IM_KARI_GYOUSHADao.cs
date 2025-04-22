
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.GyoushaKakunin.Dao
{
    /// <summary>
    /// �����Ǝ҃}�X�^Dao
    /// </summary>
    [Bean(typeof(M_KARI_GYOUSHA))]
    public interface IM_KARI_GYOUSHADao : IS2Dao
    {

        /// <summary>
        /// �Ǝ҃R�[�h�����Ƃɍ폜����Ă��Ȃ��Ǝ҂̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("GYOUSHA_CD = /*cd*/")]
        M_KARI_GYOUSHA GetDataByCd(string cd);

        /// <summary>
        /// �����Ǝ҂Ɋ֘A�����������̃f�[�^�擾���s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>�擾����DataTable</returns>
        [SqlFile("Shougun.Core.BusinessManagement.GyoushaKakunin.Sql.GetKariIchiranGenbaDataSql.sql")]
        DataTable GetIchiranGenbaData(M_KARI_GENBA data);

        /// <summary>
        /// �����Ǝ҂Ɋ֘A����n��̃f�[�^�擾���s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>�擾����DataTable</returns>
        [SqlFile("Shougun.Core.BusinessManagement.GyoushaKakunin.Sql.GetChiikiDataSql.sql")]
        DataTable GetChiikiData(M_CHIIKI data);
    }
}