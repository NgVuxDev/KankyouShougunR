using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Adjustment.ShiharaiMeisaiMeisaihyou
{
    //
    // 画面固有で使用するDaoを定義する
    // アセンブリ内で共通のDaoは共通用のクラスに定義すること
    //

    /// <summary>
    /// 支払明細明細表に出力するデータを取得するインタフェース
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI))]
    internal interface DAOClass : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.ShiharaiMeisaiMeisaihyou.Sql.GetPrintDataReportR661_Torihikisaki.sql")]
        DataTable GetPrintDataReportR661_Torihikisaki(DTOClass data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.ShiharaiMeisaiMeisaihyou.Sql.GetPrintDataReportR661_GyoushaGenba.sql")]
        DataTable GetPrintDataReportR661_GyoushaGenba(DTOClass data);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT TOP 1 * FROM M_TORIHIKISAKI ORDER BY TORIHIKISAKI_CD DESC")]
        M_TORIHIKISAKI GetMaxTorihikisaki();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT TOP 1 * FROM M_TORIHIKISAKI ORDER BY TORIHIKISAKI_CD ASC")]
        M_TORIHIKISAKI GetMinTorihikisaki();
    }
}
