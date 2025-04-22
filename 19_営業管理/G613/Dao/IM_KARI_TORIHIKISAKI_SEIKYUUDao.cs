
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.GyoushaKakunin.Dao
{
    [Bean(typeof(M_KARI_TORIHIKISAKI_SEIKYUU))]
    public interface IM_KARI_TORIHIKISAKI_SEIKYUUDao : IS2Dao
    {
        /// <summary>
        /// �����CD�R�[�h�����ƂɎ����_�������}�X�^�̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_KARI_TORIHIKISAKI_SEIKYUU GetDataByCd(string cd);
    }
}
