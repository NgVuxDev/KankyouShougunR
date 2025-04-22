// $Id: IM_HIKIAI_TORIHIKISAKI_SEIKYUUDao.cs 4688 2013-10-24 00:47:47Z sys_dev_20 $
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.TorihikisakiKakunin.Dao
{
    [Bean(typeof(M_HIKIAI_TORIHIKISAKI_SEIKYUU))]
    public interface IM_HIKIAI_TORIHIKISAKI_SEIKYUUDao : IS2Dao
    {
        /// <summary>
        /// �����CD�R�[�h�����ƂɎ����_�������}�X�^�̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_HIKIAI_TORIHIKISAKI_SEIKYUU GetDataByCd(string cd);
    }
}
