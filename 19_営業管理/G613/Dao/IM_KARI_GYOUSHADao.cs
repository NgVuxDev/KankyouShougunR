
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.GyoushaKakunin.Dao
{
    /// <summary>
    /// 引合業者マスタDao
    /// </summary>
    [Bean(typeof(M_KARI_GYOUSHA))]
    public interface IM_KARI_GYOUSHADao : IS2Dao
    {

        /// <summary>
        /// 業者コードをもとに削除されていない業者のデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("GYOUSHA_CD = /*cd*/")]
        M_KARI_GYOUSHA GetDataByCd(string cd);

        /// <summary>
        /// 引合業者に関連する引合現場のデータ取得を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>取得したDataTable</returns>
        [SqlFile("Shougun.Core.BusinessManagement.GyoushaKakunin.Sql.GetKariIchiranGenbaDataSql.sql")]
        DataTable GetIchiranGenbaData(M_KARI_GENBA data);

        /// <summary>
        /// 引合業者に関連する地域のデータ取得を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>取得したDataTable</returns>
        [SqlFile("Shougun.Core.BusinessManagement.GyoushaKakunin.Sql.GetChiikiDataSql.sql")]
        DataTable GetChiikiData(M_CHIIKI data);
    }
}