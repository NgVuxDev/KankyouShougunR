using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Billing.SeikyuuMeisaihyouShutsuryoku
{
    //
    // 画面固有で使用するDaoを定義する
    // アセンブリ内で共通のDaoは共通用のクラスに定義すること
    //

    /// <summary>
    /// 未入金一覧に出力するデータを取得するインタフェース
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI))]
    internal interface DAOClass : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuuMeisaihyouShutsuryoku.Sql.GetPrintDataReportR636.sql")]
        DataTable GetPrintDataReportR636(DTOClass data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuuMeisaihyouShutsuryoku.Sql.GetPrintDataReportR637.sql")]
        DataTable GetPrintDataReportR637(DTOClass data);
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
