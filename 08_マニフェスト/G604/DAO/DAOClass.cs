using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao.Attrs;
using System.Data;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PaperManifest.JissekiHokokuSyuseiSisetsu.DAO
{
    [Bean(typeof(T_JISSEKI_HOUKOKU_ENTRY))]
    internal interface JissekiHokokuSyuseiSisetsuDao : IS2Dao
    {
        /// <summary>
        /// 実績報告書修正データを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.JissekiHokokuSyuseiSisetsu.Sql.GetJissekiHokokuData.sql")]
        DataTable GetJissekiHokokuData(string systemid);

        /// <summary>
        /// 実績報告書修正明細データを取得する
        /// </summary>
        /// <param name="systemid">SYSTEM_ID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [Sql("SELECT * FROM T_JISSEKI_HOUKOKU_ENTRY WHERE SYSTEM_ID = /*systemid*/ AND SEQ = /*seq*/")]
        T_JISSEKI_HOUKOKU_ENTRY GetJissekiHokokuHeadData(string systemid, string seq);

        [Sql("SELECT MAX(SEQ) FROM T_JISSEKI_HOUKOKU_ENTRY WHERE SYSTEM_ID = /*systemid*/")]
        int GetMaxSeq(string systemid);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_JISSEKI_HOUKOKU_ENTRY data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_JISSEKI_HOUKOKU_ENTRY data);
    }

    [Bean(typeof(T_JISSEKI_HOUKOKU_SHORI_DETAIL))]
    internal interface JissekiHokokuShoriDetailDao : IS2Dao
    {
        /// <summary>
        /// 実績報告書修正明細データを取得する
        /// </summary>
        /// <param name="systemid">SYSTEM_ID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [Sql("SELECT * FROM T_JISSEKI_HOUKOKU_SHORI_DETAIL WHERE SYSTEM_ID = /*systemid*/ AND SEQ = /*seq*/")]
        List<T_JISSEKI_HOUKOKU_SHORI_DETAIL> GetJissekiHokokuDetailData(string systemid, string seq);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_JISSEKI_HOUKOKU_SHORI_DETAIL data);
    }

    [Bean(typeof(M_CHIIKIBETSU_BUNRUI))]
    internal interface ChiikibetsuBunruiDao : IS2Dao
    {
        [SqlFile("Shougun.Core.PaperManifest.JissekiHokokuSyuseiSisetsu.Sql.GetChikiDataSql.sql")]
        DataTable GetChikiData(M_CHIIKIBETSU_BUNRUI data);
    }

    [Bean(typeof(M_CHIIKIBETSU_SHOBUN))]
    internal interface ChiikibetsuShobunDao : IS2Dao
    {
        [SqlFile("Shougun.Core.PaperManifest.JissekiHokokuSyuseiSisetsu.Sql.GetChikiShobunDataSql.sql")]
        DataTable GetChikiData(M_CHIIKIBETSU_SHOBUN data);
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

        [Sql("SELECT * FROM T_JISSEKI_HOUKOKU_MANIFEST_DETAIL WHERE SYSTEM_ID = /*stmId*/ AND SEQ = /*seq*/ AND DETAIL_SYSTEM_ID = /*detailStmId*/")]
        DataTable GetData(string stmId, string seq, string detailStmId);
    }

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
        [SqlFile("Shougun.Core.PaperManifest.JissekiHokokuSyuseiSisetsu.Sql.GetJissekiHoukokuManiDetail.sql")]
        DataTable GetDataForEntity(T_JISSEKI_HOUKOKU_ENTRY data);

        /// <summary>
        /// 詳細データ数を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.JissekiHokokuSyuseiSisetsu.Sql.GetDetailCount.sql")]
        int GetDetailCount(string systemID);
    }

    /// <summary>
    /// 分類データを取得用Dao
    /// </summary>
    [Bean(typeof(M_CHIIKIBETSU_BUNRUI))]
    public interface BunruiDAO : IS2Dao
    {
        /// <summary>
        /// 分類データを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.JissekiHokokuSyuseiSisetsu.Sql.GetBunRuiData.sql")]
        M_CHIIKIBETSU_BUNRUI GetBunRui(string CHIIKI_CD, string HOUKOKU_BUNRUI_CD);
    }
}