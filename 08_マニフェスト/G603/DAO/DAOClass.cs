using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PaperManifest.JissekiHokokuSisetsu.DAO
{
    /// <summary>
    /// 実績報告書用Dao
    /// </summary>
    [Bean(typeof(T_JISSEKI_HOUKOKU_ENTRY))]
    public interface EntryDAO : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.JissekiHokokuSisetsu.Sql.GetJissekiHoukokuManiDetail.sql")]
        DataTable GetDataForEntity(T_JISSEKI_HOUKOKU_ENTRY data);

        /// <summary>
        /// 詳細データ数を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.JissekiHokokuSisetsu.Sql.GetDetailCount.sql")]
        int GetDetailCount(string systemID);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_JISSEKI_HOUKOKU_ENTRY data);
    }

    /// <summary>
    /// 実績報告書_マニ明細
    /// </summary>
    [Bean(typeof(T_JISSEKI_HOUKOKU_MANIFEST_DETAIL))]
    public interface ManiDetailDAO : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_JISSEKI_HOUKOKU_MANIFEST_DETAIL data);
    }

    /// <summary>
    /// 実績報告書_処理施設明細
    /// </summary>
    [Bean(typeof(T_JISSEKI_HOUKOKU_SHORI_DETAIL))]
    public interface SYORIDetailDAO : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_JISSEKI_HOUKOKU_SHORI_DETAIL data);
    }


}
