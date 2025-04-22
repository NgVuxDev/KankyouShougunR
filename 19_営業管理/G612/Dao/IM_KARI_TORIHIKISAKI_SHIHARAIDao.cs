// $Id: IM_HIKIAI_TORIHIKISAKI_SHIHARAIDao.cs 4688 2013-10-24 00:47:47Z sys_dev_20 $
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.TorihikisakiKakunin.Dao
{
    [Bean(typeof(M_KARI_TORIHIKISAKI_SHIHARAI))]
    public interface IM_KARI_TORIHIKISAKI_SHIHARAIDao : IS2Dao
    {

        /// <summary>
        ///�����R�[�h�����ƂɎ����_�x�����}�X�^�̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_KARI_TORIHIKISAKI_SHIHARAI GetDataByCd(string cd);
    }
}
