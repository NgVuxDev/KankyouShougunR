// $Id: IM_KARI_GYOUSHADao.cs 12067 2013-12-19 11:21:15Z gai $
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.GenbaKakunin.Dao
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

    }
}