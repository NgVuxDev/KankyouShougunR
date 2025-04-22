using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO
{
    /// <summary>
    /// �����}�X�^Dao
    /// </summary>
    [Bean(typeof(M_HIKIAI_TORIHIKISAKI_SHIHARAI))]
    public interface IM_HIKIAI_TORIHIKISAKI_SHIHARAIDao : IS2Dao
    {
        /// <summary>
        /// �����CD�ōi�荞��Ńf�[�^���擾
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.SqlFile.HikiaiTorihikisakiShiharai.IM_HIKIAI_TORIHIKISAKI_SHIHARAIDao_GetAllValidData.sql")]
        M_HIKIAI_TORIHIKISAKI_SHIHARAI[] GetAllValidData(M_HIKIAI_TORIHIKISAKI_SHIHARAI data);
    }
}