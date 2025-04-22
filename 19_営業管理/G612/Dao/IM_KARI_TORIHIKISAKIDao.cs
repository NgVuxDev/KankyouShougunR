// $Id: IM_HIKIAI_TORIHIKISAKIDao.cs 25968 2014-07-17 10:08:34Z chenzz@oec-h.com $
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.TorihikisakiKakunin.Dao
{
    /// <summary>
    /// 引合取引先マスタDao
    /// </summary>
    [Bean(typeof(M_KARI_TORIHIKISAKI))]
    public interface IM_KARI_TORIHIKISAKIDao : IS2Dao
    {
        /// <summary>
        /// 取引先コードをもとに削除されていない取引先のデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_KARI_TORIHIKISAKI GetDataByCd(string cd);

        /// <summary>
        /// 取引先コードをもとに仮業者マスタのデータを取得する
        /// </summary>
        /// <param name="data">Entity</param>  
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.BusinessManagement.TorihikisakiKakunin.Sql.GetIchiranKariGyoushaDataSql.sql")]
        DataTable GetIchiranKariGyoushaData(M_KARI_GYOUSHA data);
    }
}