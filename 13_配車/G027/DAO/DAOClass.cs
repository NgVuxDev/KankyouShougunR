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

namespace Shougun.Core.Allocation.SagyoubiHenkou
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
        //[Query("SAGYOU_DATE = /*data.SagyouDate*/ AND SHARYOU_CD = /*data.SharyouCd*/ AND UNTENSHA_CD = /*data.ShainCd*/ AND GYOUSHA_CD = /*data.GyoushaCd*/ AND DELETE_FLG = 0")]
        [Query("SAGYOU_DATE = /*data.SagyouDate*/ AND SHARYOU_CD = /*data.SharyouCd*/ AND UNTENSHA_CD = /*data.ShainCd*/ AND GYOUSHA_CD = /*data.GyoushaCd*/ AND DELETE_FLG = 1 ORDER BY SEQ DESC")]
        T_HAISHA_WARIATE_DAY[] GetData(DTO_Haisha data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SAGYOU_DATE = /*data.SagyouDate*/ AND SHARYOU_CD = /*data.SharyouCd*/ AND UNTENSHA_CD = /*data.ShainCd*/ AND GYOUSHA_CD = /*data.GyoushaCd*/ AND DELETE_FLG = 0")]
        T_HAISHA_WARIATE_DAY[] GetValidData(DTO_Haisha data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.SagyoubiHenkou.Sql.HAISHA_DENPYO.sql")]
        DataTable GetHaishaDenpyo(DTO_Haisha data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.SagyoubiHenkou.Sql.T_SHAIN.sql")]
        DataTable GetShain(DTO_Haisha data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.SagyoubiHenkou.Sql.T_SHARYOU.sql")]
        DataTable GetSharyou(DTO_Haisha data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.SagyoubiHenkou.Sql.GetHaishaWariateData.sql")]
        DataTable GetHaishaWariateData(DTO_Haisha data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.SagyoubiHenkou.Sql.HAISHA_WARIATE_DAY.sql")]
        T_HAISHA_WARIATE_DAY[] GetHaishaWariateDay(DTO_Haisha data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.SagyoubiHenkou.Sql.GetHaishaSharyouData.sql")]
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
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP", "UPDATE_TS")]
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
        [SqlFile("Shougun.Core.Allocation.SagyoubiHenkou.Sql.HAISHA_MEMO.sql")]
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
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP", "UPDATE_TS")]
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
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("DELETE_FLG = 0 AND SYSTEM_ID = /*data.SystemId*/ AND COURSE_KUMIKOMI_CD = 1 AND HAISHA_JOKYO_CD <> 4 "
            + "AND SHARYOU_CD = /*data.SharyouCd*/ AND UNPAN_GYOUSHA_CD = /*data.GyoushaCd*/ AND UNTENSHA_CD = /*data.ShainCd*/ "
            + "AND ((SAGYOU_DATE IS NOT NULL AND SAGYOU_DATE = /*data.SagyouDate*/) OR (SAGYOU_DATE IS NULL AND SAGYOU_DATE_BEGIN <= /*data.SagyouDate*/ AND SAGYOU_DATE_END >= /*data.SagyouDate*/))")]
        T_UKETSUKE_SS_ENTRY GetValidData(DTO_IdSeq data);
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
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP", "UPDATE_TS")]
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
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP", "UPDATE_TS")]
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
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("DELETE_FLG = 0 AND SYSTEM_ID = /*data.SystemId*/ AND COURSE_KUMIKOMI_CD = 1 AND HAISHA_JOKYO_CD <> 4 "
            + "AND SHARYOU_CD = /*data.SharyouCd*/ AND UNPAN_GYOUSHA_CD = /*data.GyoushaCd*/ AND UNTENSHA_CD = /*data.ShainCd*/ "
            + "AND ((SAGYOU_DATE IS NOT NULL AND SAGYOU_DATE = /*data.SagyouDate*/) OR (SAGYOU_DATE IS NULL AND SAGYOU_DATE_BEGIN <= /*data.SagyouDate*/ AND SAGYOU_DATE_END >= /*data.SagyouDate*/))")]
        T_UKETSUKE_SK_ENTRY GetValidData(DTO_IdSeq data);
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
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("DELETE_FLG = 0 AND SYSTEM_ID = /*data.SystemId*/ AND SHARYOU_CD = /*data.SharyouCd*/ "
            + "AND UNPAN_GYOUSHA_CD = /*data.GyoushaCd*/ AND UNTENSHA_CD = /*data.ShainCd*/ AND SAGYOU_DATE = /*data.SagyouDate*/")]
        T_TEIKI_HAISHA_ENTRY GetValidData(DTO_IdSeq data);
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
    }
}
