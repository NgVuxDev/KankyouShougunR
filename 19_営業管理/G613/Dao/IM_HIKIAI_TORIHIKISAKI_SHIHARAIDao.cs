
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.GyoushaKakunin.Dao
{
    [Bean(typeof(M_HIKIAI_TORIHIKISAKI_SHIHARAI))]
    public interface IM_HIKIAI_TORIHIKISAKI_SHIHARAIDao : IS2Dao
    {

        /// <summary>
        ///�����R�[�h�����ƂɎ����_�x�����}�X�^�̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_HIKIAI_TORIHIKISAKI_SHIHARAI GetDataByCd(string cd);
    }
}
