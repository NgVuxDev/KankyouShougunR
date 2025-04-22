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
using Shougun.Core.Common.BusinessCommon.Dto;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ElectronicManifest.DenshiCSVTorikomu
{
    /// <summary>
    /// 電子マニフェストテーブル登録更新削除用Dao
    /// </summary>
    [Bean(typeof(DT_R18))]
    public interface DT_R18DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_R18 data);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*"KANRI_ID","SEQ","MANIFEST_ID", "MANIFEST_KBN", "SHOUNIN_FLAG", "HIKIWATASHI_DATE", "UPN_ENDREP_FLAG",
            "SBN_ENDREP_FLAG", "LAST_SBN_ENDREP_FLAG", "KAKIN_DATE", "REGI_DATE", "UPN_SBN_REP_LIMIT_DATE", "LAST_SBN_REP_LIMIT_DATE",
            "RESV_LIMIT_DATE", "SBN_ENDREP_KBN", "HST_SHA_EDI_MEMBER_ID", "HST_SHA_NAME", "HST_SHA_POST", "HST_SHA_ADDRESS1",
            "HST_SHA_ADDRESS2", "HST_SHA_ADDRESS3", "HST_SHA_ADDRESS4", "HST_SHA_TEL", "HST_SHA_FAX", "HST_JOU_NAME", "HST_JOU_POST_NO",
            "HST_JOU_ADDRESS1", "HST_JOU_ADDRESS2", "HST_JOU_ADDRESS3", "HST_JOU_ADDRESS4", "HST_JOU_TEL", "REGI_TAN", "HIKIWATASHI_TAN_NAME",
            "HAIKI_DAI_CODE", "HAIKI_CHU_CODE", "HAIKI_SHO_CODE", "HAIKI_SAI_CODE", "HAIKI_BUNRUI", "HAIKI_SHURUI", "HAIKI_NAME", "HAIKI_SUU",
            "HAIKI_UNIT_CODE", "SUU_KAKUTEI_CODE", "HAIKI_KAKUTEI_SUU", "HAIKI_KAKUTEI_UNIT_CODE", "NISUGATA_CODE", "NISUGATA_NAME",
            "NISUGATA_SUU", "SBN_SHA_MEMBER_ID", "SBN_SHA_NAME", "SBN_SHA_POST", "SBN_SHA_ADDRESS1", "SBN_SHA_ADDRESS2", "SBN_SHA_ADDRESS3",
            "SBN_SHA_ADDRESS4", "SBN_SHA_TEL", "SBN_SHA_FAX", "SBN_SHA_KYOKA_ID", "SAI_SBN_SHA_MEMBER_ID", "SAI_SBN_SHA_NAME", "SAI_SBN_SHA_POST",
            "SAI_SBN_SHA_ADDRESS1", "SAI_SBN_SHA_ADDRESS2", "SAI_SBN_SHA_ADDRESS3", "SAI_SBN_SHA_ADDRESS4", "SAI_SBN_SHA_TEL", "SAI_SBN_SHA_FAX",
            "SAI_SBN_SHA_KYOKA_ID", "SBN_WAY_CODE", "SBN_WAY_NAME", "SBN_SHOUNIN_FLAG", "SBN_END_DATE", "HAIKI_IN_DATE", "RECEPT_SUU",
            "RECEPT_UNIT_CODE", "UPN_TAN_NAME", "CAR_NO", "REP_TAN_NAME", "SBN_TAN_NAME", "SBN_END_REP_DATE", "SBN_REP_BIKOU", "KENGEN_CODE",
            "LAST_SBN_JOU_KISAI_FLAG", "FIRST_MANIFEST_FLAG", "LAST_SBN_END_DATE", "LAST_SBN_END_REP_DATE", "SHUSEI_DATE", "CANCEL_FLAG",
            "CANCEL_DATE", "LAST_UPDATE_DATE", "YUUGAI_CNT", "UPN_ROUTE_CNT", "LAST_SBN_PLAN_CNT", "LAST_SBN_CNT", "RENRAKU_CNT", "BIKOU_CNT",
            "FIRST_MANIFEST_CNT", */"CREATE_DATE", "UPDATE_TS")]
        int Update(DT_R18 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R18 data);
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetDT_R18_InfoSql.sql")]
        DT_R18 GetDataForEntity(DT_R18 data);

        /// <summary>
        ///換算式と換算値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetKansanshikiKansanchiSql.sql")]
        DataTable GetKansanshikiKansanchiData(SearchMasterDataDTOCls data);

        [Procedure("get_next_dt_mf_toc_kanri_id")]
        int GetByJob(out string kanri_id);

        /// <summary>
        /// Entityで絞り込んで値を取得する touti
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetManifesutotyouhyousuuryou.sql")]
        DataTable GetManifesutotyouhyousuuryou();

        /// <summary>
        /// Entityで絞り込んで値を取得する touti
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetGenyoritsu.sql")]
        DataTable GetGenyoritsu(SearchMasterDataDTOCls data);

    }

    /// <summary>
    /// マニフェスト目次情報[DT_MF_TOC]用Dao
    /// </summary>
    [Bean(typeof(DT_MF_TOC))]
    public interface DT_MF_TOCDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_MF_TOC data);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*"KANRI_ID",*/"KANRI_ID", "UPDATE_TS")]
        int Update(DT_MF_TOC data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_MF_TOC data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetDT_MF_TOC_InfoSql.sql")]
        DT_MF_TOC GetDataForEntity(DT_MF_TOC data);

        /// <summary>
        /// DtoでLATEST_SEQとAPPROVAL_SEQ値取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Sql("SELECT KANRI_ID,MANIFEST_ID,LATEST_SEQ,APPROVAL_SEQ FROM DT_MF_TOC WHERE KANRI_ID = /*$KanriId*/")]
        DataTable GetLATEST_APPROVAL_SEQ(string KanriId);

    }

    /// <summary>
    /// 加入者番号[DT_MF_MEMBER]用Dao
    /// </summary>
    [Bean(typeof(DT_MF_MEMBER))]
    public interface DT_MF_MEMBERDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_MF_MEMBER data);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*"KANRI_ID", "HST_MEMBER_ID", "UPN1_MEMBER_ID", "UPN2_MEMBER_ID", "UPN3_MEMBER_ID", "UPN4_MEMBER_ID",
                            "UPN5_MEMBER_ID", "SBN_MEMBER_ID", */"CREATE_DATE", "UPDATE_TS")]
        int Update(DT_MF_MEMBER data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_MF_MEMBER data);

        /// <summary>
        /// Entityで絞り込んで値を取得する touti
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetDT_MF_MEMBER_InfoSql.sql")]
        DT_MF_MEMBER GetDataForEntity(DT_MF_MEMBER data);
    }

    /// <summary>
    /// 加入者情報[MS_JWNET_MEMBER]用Dao
    /// </summary>
    [Bean(typeof(MS_JWNET_MEMBER))]
    public interface MS_JWNET_MEMBERDaoCls : IS2Dao
    {
        [Sql("SELECT * FROM MS_JWNET_MEMBER")]
        DataTable GetAllData();
    }

    /// <summary>
    /// 収集運搬情報[DT_R19]用Dao
    /// </summary>
    [Bean(typeof(DT_R19))]
    public interface DT_R19DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_R19 data);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*"KANRI_ID", "SEQ",  "UPN_ROUTE_NO","MANIFEST_ID", "UPN_SHA_EDI_MEMBER_ID", "UPN_SHA_NAME", "UPN_SHA_POST",
            "UPN_SHA_ADDRESS1", "UPN_SHA_ADDRESS2", "UPN_SHA_ADDRESS3", "UPN_SHA_ADDRESS4", "UPN_SHA_TEL", "UPN_SHA_FAX",
            "UPN_SHA_KYOKA_ID", "SAI_UPN_SHA_EDI_MEMBER_ID", "SAI_UPN_SHA_NAME", "SAI_UPN_SHA_POST", "SAI_UPN_SHA_ADDRESS1",
            "SAI_UPN_SHA_ADDRESS2", "SAI_UPN_SHA_ADDRESS3", "SAI_UPN_SHA_ADDRESS4", "SAI_UPN_SHA_TEL", "SAI_UPN_SHA_FAX",
            "SAI_UPN_SHA_KYOKA_ID", "UPN_WAY_CODE", "UPN_TAN_NAME", "CAR_NO", "UPNSAKI_EDI_MEMBER_ID", "UPNSAKI_NAME",
            "UPNSAKI_JOU_ID", "UPNSAKI_JOU_KBN", "UPNSAKI_JOU_NAME", "UPNSAKI_JOU_POST", "UPNSAKI_JOU_ADDRESS1", "UPNSAKI_JOU_ADDRESS2",
            "UPNSAKI_JOU_ADDRESS3", "UPNSAKI_JOU_ADDRESS4", "UPNSAKI_JOU_TEL", "UPN_SHOUNIN_FLAG", "UPN_END_DATE", "UPNREP_UPN_TAN_NAME",
            "UPNREP_CAR_NO", "UPN_SUU", "UPN_UNIT_CODE", "YUUKA_SUU", "YUUKA_UNIT_CODE", "REP_TAN_NAME", "BIKOU", */"CREATE_DATE", "UPDATE_TS")]
        int Update(DT_R19 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R19 data);

        /// <summary>
        /// 運搬情報取得処理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetAllUnpanInfoSql.sql")]
        DT_R19 GetAllValidData(DT_R19 data);
        /// <summary>
        /// 運搬業者と現場情報を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetUnpanGyoushaInfoSql.sql")]
        DataTable GetGyoushaGenbaInfo(SearchMasterDataDTOCls data);

        /// <summary>
        /// 運搬業者と現場情報を取得する touti
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetUnpanGyoushaInfoSql.sql")]
        DT_R19[] GetGyoushaGenbaInfo();

        /// <summary>
        /// 運搬業者と現場情報を取得する touti
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetAllDT_R19_InfoSql.sql")]
        DT_R19[] GetDataForEntity(DT_R19 data);
    }

    /// <summary>
    ///有害物質情報[DT_R02]用Dao
    /// </summary>
    [Bean(typeof(DT_R02))]
    public interface DT_R02DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_R02 data);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*"KANRI_ID", "SEQ", "REC_SEQ", "MANIFEST_ID", "YUUGAI_CODE", "YUUGAI_NAME", */"CREATE_DATE", "UPDATE_TS")]
        int Update(DT_R02 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R02 data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetAllDT_R02_InfoSql.sql")]
        DT_R02[] GetDataForEntity(DT_R02 data);

    }

    /// <summary>
    ///最終処分事業場(予定)情報[DT_R04]用DAO
    /// </summary>
    [Bean(typeof(DT_R04))]
    public interface DT_R04DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_R04 data);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*"KANRI_ID", "SEQ", "REC_SEQ", "MANIFEST_ID", "LAST_SBN_JOU_NAME",
            "LAST_SBN_JOU_POST", "LAST_SBN_JOU_ADDRESS1", "LAST_SBN_JOU_ADDRESS2", "LAST_SBN_JOU_ADDRESS3",
            "LAST_SBN_JOU_ADDRESS4", "LAST_SBN_JOU_TEL", */"CREATE_DATE", "UPDATE_TS")]
        int Update(DT_R04 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R04 data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetAllDT_R04_InfoSql.sql")]
        DT_R04[] GetDataForEntity(DT_R04 data);

    }

    /// <summary>
    ///連絡番号情報[DT_R05]用DAO
    /// </summary>
    [Bean(typeof(DT_R05))]
    public interface DT_R05DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_R05 data);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*"KANRI_ID", "SEQ", "RENRAKU_ID_NO", "MANIFEST_ID", "RENRAKU_ID", */"CREATE_DATE", "UPDATE_TS")]
        int Update(DT_R05 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R05 data);

        /// <summary>
        /// Entityで絞り込んで値を取得する touti
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetAllDT_R05_InfoSql.sql")]
        DT_R05[] GetDataForEntity(DT_R05 data);

    }

    /// <summary>
    ///備考情報[DT_R06]用DAO
    /// </summary>
    [Bean(typeof(DT_R06))]
    public interface DT_R06DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_R06 data);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*"KANRI_ID", "SEQ", "BIKOU_NO","MANIFEST_ID", "BIKOU", */"CREATE_DATE", "UPDATE_TS")]
        int Update(DT_R06 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R06 data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetAllDT_R06_InfoSql.sql")]
        DT_R06[] GetDataForEntity(DT_R06 data);
    }

    /// <summary>
    ///最終処分終了日・事業場情報[DT_R13]用DAO
    /// </summary>
    [Bean(typeof(DT_R13))]
    public interface DT_R13DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_R13 data);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*"KANRI_ID", "SEQ", "REC_SEQ", "MANIFEST_ID", "LAST_SBN_JOU_NAME", "LAST_SBN_JOU_POST",
            "LAST_SBN_JOU_ADDRESS1", "LAST_SBN_JOU_ADDRESS2", "LAST_SBN_JOU_ADDRESS3", "LAST_SBN_JOU_ADDRESS4",
            "LAST_SBN_JOU_TEL", "LAST_SBN_END_DATE", */"CREATE_DATE", "UPDATE_TS")]
        int Update(DT_R13 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R13 data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetAllDT_R13_InfoSql.sql")]
        DT_R13[] GetDataForEntity(DT_R13 data);

    }

    /// <summary>
    /// 電子マニフェスト基本拡張テーブル[DT_R18_EX]登録更新削除用Dao
    /// </summary>
    [Bean(typeof(DT_R18_EX))]
    public interface DT_R18_EXDaoCls : IS2Dao
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
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("KANRI_ID", "MANIFEST_ID", "HST_GYOUSHA_CD", "HST_GENBA_CD", "SBN_GYOUSHA_CD",
            "SBN_GENBA_CD", "NO_REP_SBN_EDI_MEMBER_ID", "SBN_KYOKA_NO", "HAIKI_NAME_CD",
            "SBN_HOUHOU_CD", "HOUKOKU_TANTOUSHA_CD", "SBN_TANTOUSHA_CD", "UPN_TANTOUSHA_CD",
            "SHARYOU_CD", "KANSAN_SUU", "CREATE_USER", "CREATE_DATE", "CREATE_PC",
            "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(DT_R18_EX data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R18_EX data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetDT_R18_EX_InfoSql.sql")]
        DT_R18_EX GetDataForEntity(DT_R18_EX data);

    }

    /// <summary>
    /// 電子マニフェスト収集運搬拡張[DT_R19_EX]登録更新削除用Dao
    /// </summary>
    [Bean(typeof(DT_R19_EX))]
    public interface DT_R19_EXDaoCls : IS2Dao
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
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("SYSTEM_ID", "SEQ", "KANRI_ID", "UPN_ROUTE_NO", "MANIFEST_ID", "UPN_GYOUSHA_CD", "NO_REP_UPN_EDI_MEMBER_ID",
            "UPNSAKI_GYOUSHA_CD", "NO_REP_UPNSAKI_EDI_MEMBER_ID", "UPNSAKI_GENBA_CD", "UPN_TANTOUSHA_CD", "SHARYOU_CD", "UPNREP_UPN_TANTOUSHA_CD",
            "UPNREP_SHARYOU_CD", "HOUKOKU_TANTOUSHA_CD", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(DT_R19_EX data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R19_EX data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetDT_R19_EX_InfoSql.sql")]
        DT_R19_EX[] GetDataForEntity(DT_R19_EX data);

    }

    /// <summary>
    /// 電子マニフェスト最終処分（予定）拡張[DT_R04_EX]登録更新削除用Dao
    /// </summary>
    [Bean(typeof(DT_R04_EX))]
    public interface DT_R04_EXDaoCls : IS2Dao
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
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("SYSTEM_ID", "SEQ", "KANRI_ID", "REC_SEQ", "MANIFEST_ID", "LAST_SBN_GYOUSHA_CD", "LAST_SBN_GENBA_CD",
            "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(DT_R04_EX data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R04_EX data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetDT_R04_EX_InfoSql.sql")]
        DT_R04_EX[] GetDataForEntity(DT_R04_EX data);

    }

    /// <summary>
    /// 電子マニフェスト最終処分拡張[DT_R13_EX]登録更新削除用Dao
    /// </summary>
    [Bean(typeof(DT_R13_EX))]
    public interface DT_R13_EXDaoCls : IS2Dao
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
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("SYSTEM_ID", "SEQ", "KANRI_ID", "REC_SEQ", "MANIFEST_ID", "LAST_SBN_GYOUSHA_CD", "LAST_SBN_GENBA_CD",
            "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(DT_R13_EX data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R13_EX data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetDT_R13_EX_InfoSql.sql")]
        DT_R13_EX[] GetDataForEntity(DT_R13_EX data);

    }

    /// <summary>
    /// 電子マニフェスト存在チェック検索用Dao
    /// </summary>
    [Bean(typeof(DT_R18_EX))]
    public interface DT_R18SearchDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetElecDT_R18InfoSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

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
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.DenshiHaikiNameSearchAndCheckSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

    }

    /// <summary>
    /// 電子業者検索用Dao
    /// </summary>
    [Bean(typeof(M_DENSHI_JIGYOUSHA))]
    public interface DENSHI_JIGYOUSHA_SearchDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.DenshiGyoushaSearchAndCheckSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);
    }
    /// <summary>
    /// 電子事業場検索用Dao
    /// </summary>
    [Bean(typeof(M_DENSHI_JIGYOUJOU))]
    public interface DENSHI_JIGYOUJOU_SearchDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.DenshiGenbaSearchAndCheckSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

    }
    /// <summary>
    /// 有害物質検索用Dao
    /// </summary>
    [Bean(typeof(M_DENSHI_YUUGAI_BUSSHITSU))]
    public interface DENSHI_YUUGAI_BUSSHITSU_SearchDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.DenshiYougaibushituSearchAndCheckSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);
    }

    /// <summary>
    /// 電子担当者検索用DaoTANTOUSHA
    /// </summary>
    [Bean(typeof(M_DENSHI_TANTOUSHA))]
    public interface DENSHI_TANTOUSHA_SearchDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.DenshiTantoushaSearchAndCheckSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

    }

    /// <summary>
    /// 車両検索用Dao
    /// </summary>
    [Bean(typeof(M_SHARYOU))]
    public interface M_SHARYOUDao : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetDT_M_SHARYOU_InfoSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);
    }

    /// <summary>
    /// システム情報検索用Dao
    /// </summary>
    [Bean(typeof(M_SYS_INFO))]
    public interface M_SYS_INFODao : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetM_SYS_INFO.sql")]
        M_SYS_INFO GetDataForEntity(M_SYS_INFO data);
    }

    /// <summary>
    /// 単位Dao
    /// </summary>
    [Bean(typeof(M_UNIT))]
    public interface M_UNITDao : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetM_UNIT_InfoSql.sql")]
        DataTable GetDataForEntity(M_UNIT data);
    }

    /// <summary>
    /// 荷姿Dao
    /// </summary>
    [Bean(typeof(M_NISUGATA))]
    public interface M_NISUGATADao : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetM_NISUGATA_InfoSql.sql")]
        DataTable GetDataForEntity(M_NISUGATA data);
    }

      /// <summary>
    /// 運搬方法Dao
    /// </summary>
    [Bean(typeof(M_UNPAN_HOUHOU))]
    public interface M_UNPAN_HOUHOUDao : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetM_UNPAN_HOUHOU_InfoSql.sql")]
        DataTable GetDataForEntity(M_UNPAN_HOUHOU data);
    }

    /// <summary>
    /// 処分方法Dao
    /// </summary>
    [Bean(typeof(M_SHOBUN_HOUHOU))]
    public interface M_SHOBUN_HOUHOUDao : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Sql.GetM_SHOBUN_HOUHOU_InfoSql.sql")]
        DataTable GetDataForEntity(M_SHOBUN_HOUHOU data);
    }
}
