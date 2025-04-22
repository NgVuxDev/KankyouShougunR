using r_framework.Dao;
using Seasar.Dao.Attrs;
using System.Collections.Generic;

namespace Shougun.Core.Carriage.UnchinShukeiHyo
{
    /// <summary>
    /// 運賃集計表Daoインタフェース
    /// </summary>
    [Bean(typeof(UnchinData))]
    internal interface UnchinShukeiHyoDao : IS2Dao
    {
        /// <summary>
        /// 運賃の運賃集計表データを取得します
        /// </summary>
        /// <param name="dto">運賃集計表DTOクラス</param>
        /// <returns>運賃伝票の運賃集計表データ</returns>
        [SqlFile("Shougun.Core.Carriage.UnchinShukeiHyo.Sql.GetShukeiHyoDataUnchin.sql")]
        List<UnchinData> GetShukeiHyoDataUnchin(UnchinShukeiHyoDto dto);
    }
}
