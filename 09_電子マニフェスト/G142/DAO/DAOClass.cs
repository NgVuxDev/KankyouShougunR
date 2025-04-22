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

namespace Shougun.Core.ElectronicManifest.SousinhoryuuTouroku
{
    [Bean(typeof(QUE_INFO))]
    public interface GetSHTJDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(QUE_INFO data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("SEQ", "REQUEST_CODE", "EDI_RECORD_ID", "FUNCTION_ID", "UPN_ROUTE_NO", "TUUCHI_ID", "CREATE_DATE", "UPDATE_TS", "TRF_STATUS")]
        int Update(QUE_INFO data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(QUE_INFO data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.sousinhoryuutouroku.Sql.GetSousinhoryutouroku.sql")]
        DataTable GetDataForEntity(TOUROKUJYOUHOU_DTOCls data);

        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable getdataforstringsql(string sql);

    }

    [Bean(typeof(DT_MF_TOC))]
    public interface JWNETSoshinDaoCls : IS2Dao
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("MANIFEST_ID","LATEST_SEQ","APPROVAL_SEQ","STATUS_FLAG","READ_FLAG","CREATE_DATE","UPDATE_TS","KIND")]
        int Update(DT_MF_TOC data);

        /// <summary>
        /// Dtoで取得する
        /// </summary>
        /// <param name="KanriId"></param>
        /// <returns></returns>
        [Sql("SELECT * FROM DT_MF_TOC WHERE KANRI_ID = /*$KanriId*/")]
        DT_MF_TOC GetDataForEntity(string KanriId);
    }

    [Bean(typeof(M_DENSHI_JIGYOUSHA))]
    public interface SonzaiCheckDaoCls : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable getdataforstringsql(string sql);
    }

    [Bean(typeof(DT_R18))]
    public interface DT_R18DaoCls : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.SousinhoryuuTouroku.Sql.GetDTR18Data.sql")]
        DataTable GetDataByCd(DT_R18 data);
    }

    [Bean(typeof(DT_R18_EX))]
    public interface DT_R18_EXDaoCls : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.SousinhoryuuTouroku.Sql.GetDTR18EXData.sql")]
        DT_R18_EX GetDataByCd(DT_R18_EX data);
    }
}
