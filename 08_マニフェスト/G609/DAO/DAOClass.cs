using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PaperManifest.JissekiHokokuCsv
{
    /// <summary>
    /// 実績報告書用Dao
    /// </summary>
    [Bean(typeof(T_JISSEKI_HOUKOKU_ENTRY))]
    public interface CsvDAO : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.JissekiHokokuCsv.Sql.GetJissekiHoukokuCsv.sql")]
        DataTable GetDataForEntity(T_JISSEKI_HOUKOKU_ENTRY data);
    }
}
