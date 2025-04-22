using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.SalesPayment.TankaRirekiIchiran
{
    /// <summary>
    /// G725 単価履歴DAOインタフェース
    /// </summary>
    [Bean(typeof(T_UR_SH_ENTRY))]
    public interface ITankaRirekiIchiranDao : IS2Dao
    {
        [SqlFile("Shougun.Core.SalesPayment.TankaRirekiIchiran.Sql.GetDataUkeireForTankaRirekiIchiran.sql")]
        DataTable GetDataUkeireForTankaRirekiIchiran(TankaRirekiIchiranDto dto);

        [SqlFile("Shougun.Core.SalesPayment.TankaRirekiIchiran.Sql.GetDataShukkaForTankaRirekiIchiran.sql")]
        DataTable GetDataShukkaForTankaRirekiIchiran(TankaRirekiIchiranDto dto);

        [SqlFile("Shougun.Core.SalesPayment.TankaRirekiIchiran.Sql.GetDataUrShForTankaRirekiIchiran.sql")]
        DataTable GetDataUrShForTankaRirekiIchiran(TankaRirekiIchiranDto dto);

        [SqlFile("Shougun.Core.SalesPayment.TankaRirekiIchiran.Sql.GetDataUketsukeSSForTankaRirekiIchiran.sql")]
        DataTable GetDataUketsukeSSForTankaRirekiIchiran(TankaRirekiIchiranDto dto);

        [SqlFile("Shougun.Core.SalesPayment.TankaRirekiIchiran.Sql.GetDataUketsukeSKForTankaRirekiIchiran.sql")]
        DataTable GetDataUketsukeSKForTankaRirekiIchiran(TankaRirekiIchiranDto dto);

        [SqlFile("Shougun.Core.SalesPayment.TankaRirekiIchiran.Sql.GetDataUketsukeMKForTankaRirekiIchiran.sql")]
        DataTable GetDataUketsukeMKForTankaRirekiIchiran(TankaRirekiIchiranDto dto);

        [SqlFile("Shougun.Core.SalesPayment.TankaRirekiIchiran.Sql.GetDataKenshuForTankaRirekiIchiran.sql")]
        DataTable GetDataKenshuForTankaRirekiIchiran(TankaRirekiIchiranDto dto);

        [SqlFile("Shougun.Core.SalesPayment.TankaRirekiIchiran.Sql.GetDataDainoUkeireForTankaRirekiIchiran.sql")]
        DataTable GetDataDainoUkeireForTankaRirekiIchiran(TankaRirekiIchiranDto dto);

        [SqlFile("Shougun.Core.SalesPayment.TankaRirekiIchiran.Sql.GetDataDainoShukkaForTankaRirekiIchiran.sql")]
        DataTable GetDataDainoShukkaForTankaRirekiIchiran(TankaRirekiIchiranDto dto);
    }
}
