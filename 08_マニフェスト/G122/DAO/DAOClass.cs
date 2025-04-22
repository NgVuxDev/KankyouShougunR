using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PaperManifest.ManifestNyuryokuIkkatsu
{
    [Bean(typeof(M_OUTPUT_PATTERN_COLUMN))]
    public interface MOPCDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestNyuryokuIkkatsu.Sql.SelectItirannKoumokuMeisaiData.sql")]
        new DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(M_OUTPUT_PATTERN_COLUMN))]
    public interface PIDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestNyuryokuIkkatsu.Sql.SelectPatternItirannData.sql")]
        new DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(T_MANIFEST_PT_ENTRY))]
    public interface MPIDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestNyuryokuIkkatsu.Sql.SelectManiPatternItirannData.sql")]
        new DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(M_SHARYOU))]
    public interface MSDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestNyuryokuIkkatsu.Sql.SelectSetSharyouPopUpData.sql")]
        new DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(M_HAIKI_SHURUI))]
    public interface MHAIKIDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestNyuryokuIkkatsu.Sql.SelectHaikiShurui.sql")]
        new DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(M_GYOUSHA))]
    public interface GYOUSHADaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [Sql("/*$sql*/")]
        DataTable GetDataForEntity(string sql);
    }

    [Bean(typeof(M_GENBA))]
    public interface GENBADaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [Sql("/*$sql*/")]
        DataTable GetDataForEntity(string sql);
    }

    [Bean(typeof(M_SHARYOU))]
    public interface SHARYOUDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [Sql("/*$sql*/")]
        DataTable GetDataForEntity(string sql);
    }

    [Bean(typeof(M_TORIHIKISAKI))]
    public interface TORIHIKISAKIDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [Sql("/*$sql*/")]
        DataTable GetDataForEntity(string sql);
    }

    [Bean(typeof(M_GYOUSHA))]
    public interface GAddressDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestNyuryokuIkkatsu.Sql.SelectAddressGyousha.sql")]
        new DataTable GetDataForEntity(DTOClass data);
    }

    //2013.11.23 naitou add 交付番号重複チェック追加 start
    /// <summary>
    /// 交付番号検索
    /// </summary>
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface SearchKohuDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestNyuryokuIkkatsu.Sql.SearchKohu.sql")]
        new DataTable GetDataForEntity(SearchKohuDtoCls data);
    }
    //2013.11.23 naitou add 交付番号重複チェック追加 end
}
