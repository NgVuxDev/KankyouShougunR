using System.Data;
using KensakuKyoutsuuPopupForMultiKey.DTO;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace KensakuKyoutsuuPopupForMultiKey.DAO
{
    /// <summary>
    /// 個別品名単価DAO
    /// </summary>
    [Bean(typeof(M_KOBETSU_HINMEI_TANKA))]
    internal interface MKHTClass : IS2Dao
    {
        [SqlFile("KensakuKyoutsuuPopupForMultiKey.Sql.GetHinmeiDataForDenpyouSql.sql")]
        DataTable GetHinmeiDataForDenpyouSql(DTOClass data);
    }
}
