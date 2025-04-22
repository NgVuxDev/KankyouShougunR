// $Id: IM_HIKIAI_TORIHIKISAKIDao.cs 25968 2014-07-17 10:08:34Z chenzz@oec-h.com $
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.TorihikisakiKakunin.Dao
{
    /// <summary>
    /// ���������}�X�^Dao
    /// </summary>
    [Bean(typeof(M_KARI_TORIHIKISAKI))]
    public interface IM_KARI_TORIHIKISAKIDao : IS2Dao
    {
        /// <summary>
        /// �����R�[�h�����Ƃɍ폜����Ă��Ȃ������̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_KARI_TORIHIKISAKI GetDataByCd(string cd);

        /// <summary>
        /// �����R�[�h�����Ƃɉ��Ǝ҃}�X�^�̃f�[�^���擾����
        /// </summary>
        /// <param name="data">Entity</param>  
        /// <returns>�擾�����f�[�^</returns>
        [SqlFile("Shougun.Core.BusinessManagement.TorihikisakiKakunin.Sql.GetIchiranKariGyoushaDataSql.sql")]
        DataTable GetIchiranKariGyoushaData(M_KARI_GYOUSHA data);
    }
}