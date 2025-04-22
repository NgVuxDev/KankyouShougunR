using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Reception.UketsukeMeisaihyo
{
    [Bean(typeof(T_UKETSUKE_SS_ENTRY))]
    internal interface DAOClass : IS2Dao
    {
        /// <summary>
        /// 受付明細表 受付明細表 Detail部データを検索します
        /// </summary>
        /// <param name="dto">受付明細表 帳票データ検索用DTO</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Reception.UketsukeMeisaihyo.Sql.GetPrintDataReportR659.sql")]
        DataTable GetPrintData(SearchDtoClass data);
    }
}
