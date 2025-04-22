using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PaperManifest.ManifestShukeihyo
{
    /// <summary>
    /// マニフェスト集計表に出力するデータを取得するインタフェース
    /// </summary>
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface IManifestShukeihyoDao : IS2Dao
    {
        /// <summary>
        /// マニフェスト集計表に出力するデータを取得します
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestShukeihyo.Sql.GetManifest.sql")]
        DataTable GetManifestData(ManifestShukeihyoDto dto);
    }
}
