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
using System.Data.SqlTypes;

// http://s2dao.net.seasar.org/ja/index.html


namespace Shougun.Core.PaperManifest.ManifestIkkatsuKousin.DAO
{
    [Bean(typeof(T_MANIFEST_RET_DATE))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable getdateforstringsql(string sql);
    }
   
    [Bean(typeof(T_MANIFEST_RET_DATE))]
    public interface T_MANIFEST_RET_DATE_daocls : IS2Dao
    {
        /// <summary>
        /// コードをもとにデータを取得する
        /// 使用方法未定
        /// </summary>      
        [Query("SYSTEM_ID = /*systemId*/ and SEQ = /*Seq*/")]
        T_MANIFEST_RET_DATE GetDataByCd(SqlInt64 systemId, SqlInt32 Seq);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_RET_DATE data);


        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_RET_DATE data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_MANIFEST_RET_DATE data);

    }
   
    /// <summary>
    /// マニフェストDao
    /// </summary>
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface T_MANIFEST_ENTRYdaocls : IS2Dao
    {
        /// <summary>
        /// コードをもとにデータを取得する
        /// 使用方法未定
        /// </summary>      
        [Query("SYSTEM_ID = /*systemId*/ and SEQ = /*Seq*/")]
        T_MANIFEST_ENTRY GetDataByCd(SqlInt64 systemId, SqlInt32 Seq);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_ENTRY data);


        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_ENTRY data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_MANIFEST_ENTRY data);

    }
    /// <summary>
    /// マニフェスト印字
    /// </summary>
    [Bean(typeof(T_MANIFEST_PRT))]
    public interface T_MANIFEST_PRTdaocls : IS2Dao
    {
        /// <summary>
        /// コードをもとにデータを取得する
        /// 使用方法未定
        /// </summary>      
        [Query("SYSTEM_ID = /*systemId*/ and SEQ = /*Seq*/")]
        T_MANIFEST_PRT GetDataByCd(SqlInt64 systemId, SqlInt32 Seq);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_PRT data);


        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_PRT data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_MANIFEST_PRT data);
    }
    /// <summary>
    /// マニフェスト運搬
    /// </summary>
    [Bean(typeof(T_MANIFEST_UPN))]
    public interface T_MANIFEST_UPNdaocls : IS2Dao
    {
        /// <summary>
        /// コードをもとにデータを取得する
        /// 使用方法未定
        /// </summary>      
        [Query("SYSTEM_ID = /*systemId*/ and SEQ = /*Seq*/")]
        T_MANIFEST_UPN[] GetDataByCd(SqlInt64 systemId, SqlInt32 Seq);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_UPN data);


        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_UPN data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_MANIFEST_UPN data);
    }

    /// <summary>
    /// マニフェスト詳細
    /// </summary>
    [Bean(typeof(T_MANIFEST_DETAIL))]
    public interface T_MANIFEST_DETAILdaocls : IS2Dao
    {
        /// <summary>
        /// コードをもとにデータを取得する
        /// 使用方法未定
        /// </summary>      
        [Query("SYSTEM_ID = /*systemId*/ and SEQ = /*Seq*/")]
        T_MANIFEST_DETAIL[] GetDataByCd(SqlInt64 systemId, SqlInt32 Seq);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_DETAIL data);


        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_DETAIL data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_MANIFEST_DETAIL data);

    }

    /// <summary>
    /// マニフェスト印字詳細
    /// </summary>
    [Bean(typeof(T_MANIFEST_DETAIL_PRT))]
    public interface T_MANIFEST_DETAIL_PRTdaocls : IS2Dao
    {
        /// <summary>
        /// コードをもとにデータを取得する
        /// 使用方法未定
        /// </summary>      
        [Query("SYSTEM_ID = /*systemId*/ and SEQ = /*Seq*/")]
        T_MANIFEST_DETAIL_PRT[] GetDataByCd(SqlInt64 systemId, SqlInt32 Seq);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_DETAIL_PRT data);


        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_DETAIL_PRT data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_MANIFEST_DETAIL_PRT data);

    }

    /// <summary>
    /// マニ印字_建廃_形状
    /// </summary>
    [Bean(typeof(T_MANIFEST_KP_KEIJYOU))]
    public interface T_MANIFEST_KP_KEIJYOUdaocls : IS2Dao
    {
        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        [Query("SYSTEM_ID = /*systemId*/ and SEQ = /*Seq*/")]
        T_MANIFEST_KP_KEIJYOU[] GetDataByCd(SqlInt64 systemId, SqlInt32 Seq);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_KP_KEIJYOU data);
    }

    /// <summary>
    /// マニ印字_建廃_荷姿
    /// </summary>
    [Bean(typeof(T_MANIFEST_KP_NISUGATA))]
    public interface T_MANIFEST_KP_NISUGATAdaocls : IS2Dao
    {
        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        [Query("SYSTEM_ID = /*systemId*/ and SEQ = /*Seq*/")]
        T_MANIFEST_KP_NISUGATA[] GetDataByCd(SqlInt64 systemId, SqlInt32 Seq);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_KP_NISUGATA data);
    }

    /// <summary>
    /// マニ印字_建廃_処分方法
    /// </summary>
    [Bean(typeof(T_MANIFEST_KP_SBN_HOUHOU))]
    public interface T_MANIFEST_KP_SBN_HOUHOUdaocls : IS2Dao
    {
        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        [Query("SYSTEM_ID = /*systemId*/ and SEQ = /*Seq*/")]
        T_MANIFEST_KP_SBN_HOUHOU[] GetDataByCd(SqlInt64 systemId, SqlInt32 Seq);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_KP_SBN_HOUHOU data);
    }
}
