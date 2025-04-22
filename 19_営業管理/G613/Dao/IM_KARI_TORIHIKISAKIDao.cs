
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.GyoushaKakunin.Dao
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

    }
}