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

namespace Shougun.Core.Allocation.HaishaWariateDay
{
    [Bean(typeof(T_HAISHA_WARIATE_DAY))]
    public interface DAO_T_HAISHA_WARIATE_DAY : IS2Dao
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP", "UPDATE_TS")]
        int Update(T_HAISHA_WARIATE_DAY data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_HAISHA_WARIATE_DAY data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SAGYOU_DATE = /*data.SagyouDate*/ AND SHARYOU_CD = /*data.SharyouCd*/ AND UNTENSHA_CD = /*data.ShainCd*/ AND GYOUSHA_CD = /*data.GyoushaCd*/ AND DELETE_FLG = 1 ORDER BY SEQ DESC")]
        T_HAISHA_WARIATE_DAY[] GetData(DTO_Haisha data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.HaishaWariateDay.Sql.HAISHA_DENPYO.sql")]
        DataTable GetHaishaDenpyo(DTO_Haisha data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.HaishaWariateDay.Sql.MIHAISHA.sql")]
        DataTable GetMihaisha(DTO_Haisha data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.HaishaWariateDay.Sql.T_SHAIN.sql")]
        DataTable GetShain(DTO_Haisha data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.HaishaWariateDay.Sql.T_SHARYOU.sql")]
        DataTable GetSharyou(DTO_Haisha data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.HaishaWariateDay.Sql.HAISHA_WARIATE_DAY.sql")]
        T_HAISHA_WARIATE_DAY[] GetHaishaWariateDay(DTO_Haisha data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.HaishaWariateDay.Sql.GetHaishaWariateData.sql")]
        DataTable GetHaishaWariateData(DTO_Haisha data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.HaishaWariateDay.Sql.GetHaishaSharyouData.sql")]
        DataTable GetHaishaSharyouData(DTO_Haisha data);
    }
    [Bean(typeof(T_HAISHA_MEMO))]
    public interface DAO_T_HAISHA_MEMO : IS2Dao
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP", "UPDATE_TS")]
        int Update(T_HAISHA_MEMO data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_HAISHA_MEMO data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.HaishaWariateDay.Sql.HAISHA_MEMO.sql")]
        T_HAISHA_MEMO GetData(DTO_Haisha data);

        [Sql("SELECT MAX(SYSTEM_ID) + 1 AS SYSTEM_ID FROM T_HAISHA_MEMO")]
        Int64 GetNextSystemId();
    }
    [Bean(typeof(T_UKETSUKE_SS_ENTRY))]
    public interface DAO_T_UKETSUKE_SS_ENTRY : IS2Dao
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP", "UPDATE_TS")]
        int Update(T_UKETSUKE_SS_ENTRY data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SS_ENTRY data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SystemId*/ AND SEQ = /*data.Seq*/ AND DELETE_FLG = 0")]
        T_UKETSUKE_SS_ENTRY GetData(DTO_IdSeq data);

        /// <summary>
        /// 最新のT_UKETSUKE_SS_ENTRYを取得する
        /// </summary>
        /// <param name="systemId"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SystemId*/ AND DELETE_FLG = 0")]
        T_UKETSUKE_SS_ENTRY GetDataForLatestData(DTO_IdSeq data);
    }
    [Bean(typeof(T_UKETSUKE_SS_DETAIL))]
    public interface DAO_T_UKETSUKE_SS_DETAIL : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SS_DETAIL data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SystemId*/ AND SEQ = /*data.Seq*/")]
        T_UKETSUKE_SS_DETAIL[] GetData(DTO_IdSeq data);
    }
    [Bean(typeof(T_CONTENA_RESERVE))]
    public interface DAO_T_CONTENA_RESERVE : IS2Dao
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP", "UPDATE_TS")]
        int Update(T_CONTENA_RESERVE data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_CONTENA_RESERVE data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SystemId*/ AND SEQ = /*data.Seq*/ AND DELETE_FLG = 0")]
        T_CONTENA_RESERVE[] GetData(DTO_IdSeq data);
    }
    [Bean(typeof(T_UKETSUKE_SK_ENTRY))]
    public interface DAO_T_UKETSUKE_SK_ENTRY : IS2Dao
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP", "UPDATE_TS")]
        int Update(T_UKETSUKE_SK_ENTRY data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SK_ENTRY data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SystemId*/ AND SEQ = /*data.Seq*/ AND DELETE_FLG = 0")]
        T_UKETSUKE_SK_ENTRY GetData(DTO_IdSeq data);

        /// <summary>
        /// 最新のT_UKETSUKE_SK_ENTRYを取得する
        /// </summary>
        /// <param name="systemId"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SystemId*/ AND DELETE_FLG = 0")]
        T_UKETSUKE_SK_ENTRY GetDataForLatestData(DTO_IdSeq data);
    }
    [Bean(typeof(T_UKETSUKE_SK_DETAIL))]
    public interface DAO_T_UKETSUKE_SK_DETAIL : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SK_DETAIL data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SystemId*/ AND SEQ = /*data.Seq*/")]
        T_UKETSUKE_SK_DETAIL[] GetData(DTO_IdSeq data);
    }
    [Bean(typeof(T_TEIKI_HAISHA_ENTRY))]
    public interface DAO_T_TEIKI_HAISHA_ENTRY : IS2Dao
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP", "UPDATE_TS")]
        int Update(T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SystemId*/ AND SEQ = /*data.Seq*/ AND DELETE_FLG = 0")]
        T_TEIKI_HAISHA_ENTRY GetData(DTO_IdSeq data);

        /// <summary>
        /// 最新のT_TEIKI_HAISHA_ENTRYを取得する
        /// </summary>
        /// <param name="systemId"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SystemId*/ AND DELETE_FLG = 0")]
        T_TEIKI_HAISHA_ENTRY GetDataForLatestData(DTO_IdSeq data);
    }
    [Bean(typeof(T_TEIKI_HAISHA_DETAIL))]
    public interface DAO_T_TEIKI_HAISHA_DETAIL : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_HAISHA_DETAIL data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SystemId*/ AND SEQ = /*data.Seq*/")]
        T_TEIKI_HAISHA_DETAIL[] GetData(DTO_IdSeq data);
    }
    [Bean(typeof(T_TEIKI_HAISHA_NIOROSHI))]
    public interface DAO_T_TEIKI_HAISHA_NIOROSHI : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_HAISHA_NIOROSHI data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SystemId*/ AND SEQ = /*data.Seq*/")]
        T_TEIKI_HAISHA_NIOROSHI[] GetData(DTO_IdSeq data);
    }
    [Bean(typeof(T_TEIKI_HAISHA_SHOUSAI))]
    public interface DAO_T_TEIKI_HAISHA_SHOUSAI : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_HAISHA_SHOUSAI data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SystemId*/ AND SEQ = /*data.Seq*/")]
        T_TEIKI_HAISHA_SHOUSAI[] GetData(DTO_IdSeq data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SystemId*/ AND SEQ = /*data.Seq*/ AND DETAIL_SYSTEM_ID = /*data.DetailSystemId*/")]
        T_TEIKI_HAISHA_SHOUSAI[] GetData2(DTO_IdSeqDetid data);
    }
    [Bean(typeof(M_HINMEI))]
    public interface DAO_M_HINMEI : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_HINMEI data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        [Query("HINMEI_CD = /*hinmeiCd*/")]
        M_HINMEI GetDataByCode(string hinmeiCd);
    }
    [Bean(typeof(SearchResult))]
    public interface DAO_MAP : IS2Dao
    {
        /// <summary>
        /// 地図表示設定画面用(固体管理)の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.HaishaWariateDay.Sql.GetMapDataContena.sql")]
        List<SearchResult> GetIchiranJissekiDataSql(DTOCls data);

        /// <summary>
        /// 設置コンテナ一覧画面用(数量管理)の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.HaishaWariateDay.Sql.GetIchiranDataSqlForSuuryouKanri.sql")]
        List<SearchResult> GetIchiranDataSqlForSuuryouKanri(DTOCls data);

    }
}
