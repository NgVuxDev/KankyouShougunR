using r_framework.Dao;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Allocation.HaishaMeisai
{
    [Bean(typeof(HaishaMeisaiDTOClass))]
    public interface HaishaMeisaiDAOClass : IS2Dao
    {
        // 検索条件
        [SqlFile("Shougun.Core.Allocation.HaishaMeisai.Sql.GetReportDetailData.sql")]
        HaishaMeisaiDTOClass[] GetReportDetailData(HaishaMeisaiDTOClass data);
    
    }
}
