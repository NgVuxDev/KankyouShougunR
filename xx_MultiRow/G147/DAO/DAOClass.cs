using System;
using System.Collections.Generic;
using System.Data;
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
using Shougun.Core.ElectronicManifest.MihimodukeIchiran.DTO;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ElectronicManifest.MihimodukeIchiran.DAO
{
    [Bean(typeof(M_DENSHI_JIGYOUJOU))]
    public interface DMTDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        [SqlFile("Shougun.Core.ElectronicManifest.MihimodukeIchiran.Sql.SelectMihimodukeIchiran.sql")]
        new DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(M_GYOUSHA))]
    public interface GYOUSHADaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="sql"></param>
        [Sql("/*$sql*/")]
        DataTable GetDataForEntity(string sql);
    }

    [Bean(typeof(M_GENBA))]
    public interface GENBADaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="sql"></param>
        [Sql("/*$sql*/")]
        DataTable GetDataForEntity(string sql);
    }

    [Bean(typeof(M_SHOBUN_HOUHOU))]
    public interface SHOBUNHDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="sql"></param>
        [Sql("/*$sql*/")]
        DataTable GetDataForEntity(string sql);
    }

    [Bean(typeof(M_DENSHI_JIGYOUSHA))]
    public interface MJSDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_JIGYOUSHA data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("EDI_PASSWORD", "JIGYOUSHA_NAME", "JIGYOUSHA_POST",
            "JIGYOUSHA_ADDRESS1", "JIGYOUSHA_ADDRESS2", "JIGYOUSHA_ADDRESS3", "JIGYOUSHA_ADDRESS4",
            "JIGYOUSHA_TEL", "JIGYOUSHA_FAX", "HST_KBN", "UPN_KBN", "SBN_KBN", "HOUKOKU_HUYOU_KBN",
            "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_JIGYOUSHA data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [Sql("/*$sql*/")]
        DataTable GetDataForEntity(string sql);
    }

    [Bean(typeof(M_DENSHI_JIGYOUSHA))]
    public interface DenshiJigyoujouDaoCls : IS2Dao
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("EDI_PASSWORD", "JIGYOUSHA_NAME", "JIGYOUSHA_POST",
            "JIGYOUSHA_ADDRESS1", "JIGYOUSHA_ADDRESS2", "JIGYOUSHA_ADDRESS3", "JIGYOUSHA_ADDRESS4",
            "JIGYOUSHA_TEL", "JIGYOUSHA_FAX", "HOUKOKU_HUYOU_KBN",
            "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_JIGYOUSHA data);

        /// <summary>
        /// 加入者番号をもとに電子事業者マスタのデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("EDI_MEMBER_ID = /*cd*/")]
        M_DENSHI_JIGYOUSHA GetDataByCd(string cd);
    }

    [Bean(typeof(M_DENSHI_JIGYOUJOU))]
    public interface MJJDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_JIGYOUJOU data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("JIGYOUSHA_KBN", "JIGYOUJOU_KBN","JIGYOUJOU_NAME", 
            "JIGYOUJOU_POST", "JIGYOUJOU_ADDRESS1", "JIGYOUJOU_ADDRESS2",
            "JIGYOUJOU_ADDRESS3", "JIGYOUJOU_ADDRESS4", "JIGYOUJOU_TEL", "CREATE_USER",
            "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_JIGYOUJOU data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="sql"></param>
        [Sql("/*$sql*/")]
        DataTable GetDataForEntity(string sql);

        /// <summary>
        /// 加入者番号をもとに新規事業場CDを取得する
        /// </summary>
        /// <param name="ediMemberID">加入者番号</param>
        [SqlFile("Shougun.Core.ElectronicManifest.MihimodukeIchiran.Sql.SelectNewJigyoujouCd.sql")]
        string GetNewJigyoujouCD(string ediMemberID);
    }

    [Bean(typeof(M_DENSHI_HAIKI_NAME))]
    public interface MHNDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="sql"></param>
        [Sql("/*$sql*/")]
        DataTable GetDataForEntity(string sql);
    }

    /// <summary>
    /// DT_R18検索用Dao
    /// </summary>
    [Bean(typeof(DT_R18))]
    public interface DT_R18DaoCls : IS2Dao
    {
        /// <summary>
        ///減容率を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.MihimodukeIchiran.Sql.GetGenYourituSql.sql")]
        DataTable GetGenYourituData(SearchDTOForDTExClass data);
    }

    [Bean(typeof(DT_R18_EX))]
    public interface R18EXDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_R18_EX data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">Entity</param>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(DT_R18_EX data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="sql"></param>
        [Sql("/*$sql*/")]
        DataTable GetDataForEntity(string sql);

        /// <summary>
        /// 指定された管理番号からデータを取得する
        /// </summary>
        /// <param name="kanriID">管理番号</param>
        /// <returns></returns>
        [Query("KANRI_ID = /*kanriID*/ AND DELETE_FLG = 0")]
        DT_R18_EX GetDataByKanriId(string kanriID);
    }

    [Bean(typeof(DT_R19_EX))]
    public interface R19EXDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_R19_EX data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">Entity</param>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(DT_R19_EX data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [Sql("/*$sql*/")]
        DataTable GetDataForEntity(string sql);

        /// <summary>
        /// 指定された管理番号からデータを取得する
        /// </summary>
        /// <param name="kanriID">管理番号</param>
        /// <returns></returns>
        [Query("KANRI_ID = /*kanriID*/ AND DELETE_FLG = 0 ORDER BY UPN_ROUTE_NO")]
        DT_R19_EX[] GetDataByKanriId(string kanriID);
    }

    [Bean(typeof(DT_R04_EX))]
    public interface R04EXDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_R04_EX data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">Entity</param>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(DT_R04_EX data);

        /// <summary>
        /// 指定された管理番号からデータを取得する
        /// </summary>
        /// <param name="kanriID">管理番号</param>
        /// <returns></returns>
        [Query("KANRI_ID = /*kanriID*/ AND DELETE_FLG = 0")]
        DT_R04_EX[] GetDataByKanriId(string kanriID);

        /// <summary>
        /// 指定された管理番号から最大SEQを取得する
        /// </summary>
        /// <param name="kanriID">管理番号</param>
        /// <returns></returns>
        [Sql("SELECT ISNULL(MAX(SEQ),0) FROM DT_R04_EX WHERE KANRI_ID = /*kanriID*/")]
        int GetMaxSeqByKanriId(string kanriID);
    }

    [Bean(typeof(DT_R08_EX))]
    public interface R08EXDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_R08_EX data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">Entity</param>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(DT_R08_EX data);

        /// <summary>
        /// 指定された管理番号からデータを取得する
        /// </summary>
        /// <param name="kanriID">管理番号</param>
        /// <returns></returns>
        [Query("KANRI_ID = /*kanriID*/ AND DELETE_FLG = 0")]
        DT_R08_EX[] GetDataByKanriId(string kanriID);
    }

    [Bean(typeof(DT_R13_EX))]
    public interface R13EXDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_R13_EX data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">Entity</param>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(DT_R13_EX data);

        /// <summary>
        /// 指定された管理番号からデータを取得する
        /// </summary>
        /// <param name="kanriID">管理番号</param>
        /// <returns></returns>
        [Query("KANRI_ID = /*kanriID*/ AND DELETE_FLG = 0")]
        DT_R13_EX[] GetDataByKanriId(string kanriID);
    }

    [Bean(typeof(IMPORT_MEMBER_FILTER))]
    public interface ImportMemberFilterDaoClas : IS2Dao
    {
        /// <summary>
        /// IMPORT_MEMBER_FILTERを全件取得
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT   ISNULL(MEMBER_ID1,'') + ',' +    ISNULL(MEMBER_ID2,'') + ',' +    ISNULL(MEMBER_ID3,'') + ',' +    ISNULL(MEMBER_ID4,'') + ',' +    ISNULL(MEMBER_ID5,'') FROM IMPORT_MEMBER_FILTER ")]
        DataTable GetAllData();
    }
}
