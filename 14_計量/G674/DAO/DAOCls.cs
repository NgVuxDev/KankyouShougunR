using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using Shougun.Core.Scale.KeiryouHoukoku.DTO;

namespace Shougun.Core.Scale.KeiryouHoukoku.DAO
{
    /// <summary>
    /// 計量報告に出力するデータを取得するインタフェース
    /// </summary>
    [Bean(typeof(T_KEIRYOU_ENTRY))]
    internal interface DAOClass : IS2Dao
    {
        /// <summary>
        /// 計量報告に出力するデータを取得します
        /// </summary>
        /// <param name="dto">抽出条件</param>
        /// <returns>抽出結果</returns>
        [SqlFile("Shougun.Core.Scale.KeiryouHoukoku.Sql.GetMeisaiData.sql")]
        DataTable GetDetailData(DTOCls dto);
    }
}