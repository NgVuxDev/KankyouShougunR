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
using Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.Dto;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.Dao
{
    /// <summary>
    /// システム設定入力
    /// </summary>
    [Bean(typeof(M_SYS_INFO))]
    public interface GetSysInfoDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.Sql.GetSysInfo.sql")]
        new DataTable GetDataForEntity(M_SYS_INFO data);
    }

    /// <summary>
    /// 通知情報結果
    /// </summary>
    [Bean(typeof(DT_R24))]
    public interface GetJyuuyouTsuuchiDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.Sql.GetJyuuyouTsuuchi.sql")]
        new DataTable GetDataForEntity(TsuuchiJyouhouDTOCls data);
    }

    /// <summary>
    /// 通知情報結果
    /// </summary>
    [Bean(typeof(DT_R24))]
    public interface GetOshiraseTsuuchiDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.Sql.GetOshiraseTsuuchi.sql")]
        new DataTable GetDataForEntity(TsuuchiJyouhouDTOCls data);
    }

    /// <summary>
    /// 通知情報結果
    /// </summary>
    [Bean(typeof(DT_R24))]
    public interface GetMeisaiInfoDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.Sql.GetMeisaiJyouhou.sql")]
        new DataTable GetDataForEntity(MeisaiInfoDTOCls data);
    }

    /// <summary>
    /// 通知情報結果更新
    /// </summary>
    [Bean(typeof(DT_R24))]
    public interface TuuchiInfoDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_R24 data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("MEMBER_ID", "TUUCHI_CODE" , "TUUCHI_STATUS" , "TUUCHI_DATE" , "TUUCHI_TIME" , "MANIFEST_ID"
            , "RENRAKU_ID1" , "RENRAKU_ID2" , "RENRAKU_ID3" , "HIKIWATASHI_DATE" , "HST_JOU_NAME" , "END_DATE" 
            , "UPN_ROUTE_NO" , "BIKOU" , "IMPORT_FLAG" , "CREATE_DATE", "UPDATE_TS")]
        int Update(DT_R24 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(DT_R24 data);
    }

    /// <summary>
    /// マニフェスト目次情報更新
    /// </summary>
    [Bean(typeof(DT_MF_TOC))]
    public interface MokujiInfoDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_MF_TOC data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("MANIFEST_ID","LATEST_SEQ","APPROVAL_SEQ","STATUS_FLAG","READ_FLAG",
            "UPDATE_TS","SEARCH_UPDATE_TS","KIND", "CREATE_DATE")]
        int Update(DT_MF_TOC data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(DT_MF_TOC data);
    }

    /// <summary>
    /// マニフェスト更新
    /// </summary>
    [Bean(typeof(QUE_INFO))]
    public interface QueInfoDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(QUE_INFO data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("SEQ", "REQUEST_CODE", "EDI_RECORD_ID", "FUNCTION_ID", "UPN_ROUTE_NO", "TUUCHI_ID",
            "STATUS_FLAG", "CREATE_DATE", "UPDATE_TS")]
        int Update(QUE_INFO data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(QUE_INFO data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.Sql.GetQueInfoMaxSeq.sql")]
        new DataTable GetMaxSeq(GetMaxSeqDtoCls data);
    }
}
