using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO
{
    /// <summary>
    /// 取引先マスタDao
    /// </summary>
    [Bean(typeof(M_HIKIAI_TORIHIKISAKI_SEIKYUU))]
    public interface IM_HIKIAI_TORIHIKISAKI_SEIKYUUDao : IS2Dao
    {
        /// <summary>
        /// 取引先CDで絞り込んでデータを取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.SqlFile.HikiaiTorihikisakiSeikyuu.IM_HIKIAI_TORIHIKISAKI_SEIKYUUDao_GetAllValidData.sql")]
        M_HIKIAI_TORIHIKISAKI_SEIKYUU[] GetAllValidData(M_HIKIAI_TORIHIKISAKI_SEIKYUU data);
    }
}