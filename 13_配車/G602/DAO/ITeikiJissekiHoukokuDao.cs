using System.Data;
using r_framework.Dao;
using Seasar.Dao.Attrs;
using Shougun.Core.Allocation.TeikiJissekiHoukoku.Dto;

namespace Shougun.Core.Allocation.TeikiJissekiHoukoku.Dao
{
    /// <summary>
    /// 定期実績CSV出力DAO
    /// </summary>
    [Bean(typeof(TeikiJissekiHoukokuDto))]
    public interface ITeikiJissekiHoukokuDao : IS2Dao
    {
        /// <summary>
        /// 検索条件で：廃棄物種類、日付、単位を取る
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiJissekiHoukoku.Sql.GetReportDetailData_1.sql")]
        TeikiJissekiHoukokuDto[] GetReportDetailData_1(TeikiJissekiHoukokuDto data);
        /// <summary>
        /// 検索条件で：廃棄物種類、日付、単位を取る
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiJissekiHoukoku.Sql.GetReportDetailData_2.sql")]
        TeikiJissekiHoukokuDto[] GetReportDetailData_2(TeikiJissekiHoukokuDto data);
        /// <summary>
        /// 検索条件で：廃棄物種類、日付、単位を取る
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiJissekiHoukoku.Sql.GetReportDetailData_3.sql")]
        TeikiJissekiHoukokuDto[] GetReportDetailData_3(TeikiJissekiHoukokuDto data);
    }
}
