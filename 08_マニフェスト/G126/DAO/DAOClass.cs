using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PaperManifest.ManifestIchiran.DAO
{
    //[Bean(typeof(M_SYS_INFO))]
    //public interface GetMSIDaoCls : IS2Dao
    //{
    //    /// <summary>
    //    /// Entityで絞り込んで値を取得する
    //    /// </summary>
    //    /// <param name="SYS_ID">ID</param>
    //    /// <returns>DAOClass</returns>
    //    [SqlFile("Shougun.Core.PaperManifest.ManifestIchiran.Sql.GetSystemInfo.sql")]
    //    DataTable GetDataForEntity(MSIDtoCls data);
    //}

    [Bean(typeof(M_OUTPUT_PATTERN))]
    public interface GetTMEDaoCls : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable getdateforstringsql(string sql);
    }

    [Bean(typeof(DT_MF_TOC))]
    public interface GetDMTDaoCls : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestIchiran.Sql.GetDennsiCsvSyuturyoku.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_HOUKOKUSHO_BUNRUI))]
    public interface HokokushoDaoCls : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestIchiran.Sql.GetHokokushoData.sql")]
        DataTable GetDataForEntity(HokokushoDtoCls data);
    }

    [Bean(typeof(M_HAIKI_SHURUI))]
    public interface HaikiShuruiDaoCls : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestIchiran.Sql.GetHaikiShuruiData.sql")]
        DataTable GetDataForEntity(HaikiShuruiDtoCls data);
    }

    [Bean(typeof(M_HAIKI_NAME))]
    public interface HaikiNameDaoCls : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestIchiran.Sql.GetHaikiNameData.sql")]
        DataTable GetDataForEntity(HaikiNameDtoCls data);
    }

    // 20140610 katen 不具合No.4712 start‏
    /// <summary>
    /// 電子廃棄物種類コード名称検索用Dao
    /// </summary>
    [Bean(typeof(M_DENSHI_HAIKI_SHURUI))]
    public interface DENSHI_HAIKI_SHURUIE_SearchDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestIchiran.Sql.DenshiHaikiShuruiSearchAndCheckSql.sql")]
        DataTable GetDataForEntity(SearchExistDTOCls data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestIchiran.Sql.GetDenshiHaikiShuruiByCd.sql")]
        DataTable GetDataByShuruiCD(SearchExistDTOCls data);
    }
    /// <summary>
    /// 電子廃棄物名称コードと名称検索用Dao
    /// </summary>
    [Bean(typeof(M_DENSHI_HAIKI_NAME))]
    public interface DENSHI_HAIKI_NAME_SearchDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestIchiran.Sql.DenshiHaikiNameSearchAndCheckSql.sql")]
        DataTable GetDataForEntity(SearchExistDTOCls data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestIchiran.Sql.GetDenshiHaikiNameByCd.sql")]
        DataTable GetDataByNameCD(SearchExistDTOCls data);

    }
    // 20140610 katen 不具合No.4712 end‏
}
