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

namespace Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku
{
    /// <summary>
    /// 電子マニフェストテーブル登録更新削除用Dao
    /// </summary>
    [Bean(typeof(DT_R18))]
    public interface DT_R18DaoCls: IS2Dao
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
        [NoPersistentProps(/*"KANRI_ID","SEQ",*/"MANIFEST_ID","MANIFEST_KBN","SHOUNIN_FLAG","HIKIWATASHI_DATE","UPN_ENDREP_FLAG",
            "SBN_ENDREP_FLAG","LAST_SBN_ENDREP_FLAG","KAKIN_DATE","REGI_DATE","UPN_SBN_REP_LIMIT_DATE","LAST_SBN_REP_LIMIT_DATE",
            "RESV_LIMIT_DATE","SBN_ENDREP_KBN","HST_SHA_EDI_MEMBER_ID","HST_SHA_NAME","HST_SHA_POST","HST_SHA_ADDRESS1",
            "HST_SHA_ADDRESS2","HST_SHA_ADDRESS3","HST_SHA_ADDRESS4","HST_SHA_TEL","HST_SHA_FAX","HST_JOU_NAME","HST_JOU_POST_NO",
            "HST_JOU_ADDRESS1","HST_JOU_ADDRESS2","HST_JOU_ADDRESS3","HST_JOU_ADDRESS4","HST_JOU_TEL","REGI_TAN","HIKIWATASHI_TAN_NAME",
            "HAIKI_DAI_CODE","HAIKI_CHU_CODE","HAIKI_SHO_CODE","HAIKI_SAI_CODE","HAIKI_BUNRUI","HAIKI_SHURUI","HAIKI_NAME","HAIKI_SUU",
            "HAIKI_UNIT_CODE","SUU_KAKUTEI_CODE","HAIKI_KAKUTEI_SUU","HAIKI_KAKUTEI_UNIT_CODE","NISUGATA_CODE","NISUGATA_NAME",
            "NISUGATA_SUU","SBN_SHA_MEMBER_ID","SBN_SHA_NAME","SBN_SHA_POST","SBN_SHA_ADDRESS1","SBN_SHA_ADDRESS2","SBN_SHA_ADDRESS3",
            "SBN_SHA_ADDRESS4","SBN_SHA_TEL","SBN_SHA_FAX","SBN_SHA_KYOKA_ID","SAI_SBN_SHA_MEMBER_ID","SAI_SBN_SHA_NAME","SAI_SBN_SHA_POST",
            "SAI_SBN_SHA_ADDRESS1","SAI_SBN_SHA_ADDRESS2","SAI_SBN_SHA_ADDRESS3","SAI_SBN_SHA_ADDRESS4","SAI_SBN_SHA_TEL","SAI_SBN_SHA_FAX",
            "SAI_SBN_SHA_KYOKA_ID","SBN_WAY_CODE","SBN_WAY_NAME","SBN_SHOUNIN_FLAG","SBN_END_DATE","HAIKI_IN_DATE","RECEPT_SUU",
            "RECEPT_UNIT_CODE","UPN_TAN_NAME","CAR_NO","REP_TAN_NAME","SBN_TAN_NAME","SBN_END_REP_DATE","SBN_REP_BIKOU","KENGEN_CODE",
            "LAST_SBN_JOU_KISAI_FLAG","FIRST_MANIFEST_FLAG","LAST_SBN_END_DATE","LAST_SBN_END_REP_DATE","SHUSEI_DATE","CANCEL_FLAG",
            "CANCEL_DATE","LAST_UPDATE_DATE","YUUGAI_CNT","UPN_ROUTE_CNT","LAST_SBN_PLAN_CNT","LAST_SBN_CNT","RENRAKU_CNT","BIKOU_CNT",
            "FIRST_MANIFEST_CNT","CREATE_DATE","UPDATE_TS")]
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
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetDT_R18_InfoSql.sql")]
        DT_R18 GetDataForEntity(DT_R18 data);

        /// <summary>
        ///減容率を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetGenYourituSql.sql")]
        DataTable GetGenYourituData(SearchMasterDataDTOCls data);

        /// <summary>
        /// 電子マニ管理番号を発番処理
        /// </summary>
        /// <param name="kanri_id"></param>
        /// <returns></returns>
        [Procedure("get_next_dt_mf_toc_kanri_id")]
        int GetByJob(out string kanri_id);

        /// <summary>
        /// 同管理IDで枝番号SEQの最大値取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetMaxSeqFromDT_R18Sql.sql")]
        DataTable GetMaxSeqFromDT_R18(SearchMasterDataDTOCls data);

    }

    /// <summary>
    /// キュー情報[QUE_INFO]用DAO
    /// </summary>
    [Bean(typeof(QUE_INFO))]
    public interface QUE_INFODaoCls : IS2Dao
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
        //[NoPersistentProps(/*"KANRI_ID", "QUE_SEQ", */"SEQ", "REQUEST_CODE", "EDI_RECORD_ID", "FUNCTION_ID", "UPN_ROUTE_NO", "TUUCHI_ID", "STATUS_FLAG", "CREATE_DATE", "UPDATE_TS")]
        [NoPersistentProps(/*"KANRI_ID", "QUE_SEQ", */"REQUEST_CODE", "EDI_RECORD_ID", "UPN_ROUTE_NO", "TUUCHI_ID", "CREATE_DATE", "UPDATE_TS")]
        int Update(QUE_INFO data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("SEQ", "REQUEST_CODE", "EDI_RECORD_ID", "FUNCTION_ID", "UPN_ROUTE_NO", "TUUCHI_ID", "CREATE_DATE", "UPDATE_TS", "TRF_STATUS")]
        int UpdateM(QUE_INFO data);

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
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetQue_InfoSql.sql")]
        QUE_INFO GetDataForEntity(QUE_INFO data);

        /// <summary>
        /// DtoでQue_Infoを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetQue_InfoSql.sql")]
        DataTable GetQue_SeqInfo(SearchMasterDataDTOCls data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetQue_InfoSql_Del.sql")]
        DataTable GetQue_SeqInfo_DEL(SearchMasterDataDTOCls data);

        /// <summary>
        /// Dtoで加入者情報[MS_JWNET_MEMBER]存在するチェック
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetMS_JWNET_MEMBER_InfoSql.sql")]
        DataTable GetMS_JWNET_MEMBERInfo(SearchMasterDataDTOCls data);
        /// <summary>
        /// SQL文で絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
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
        [NoPersistentProps(/*"KANRI_ID", "MANIFEST_ID", "LATEST_SEQ", "APPROVAL_SEQ", "STATUS_FLAG", 
                           "STATUS_DETAIL", "READ_FLAG", "CREATE_DATE",*/ "UPDATE_TS")]
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
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetDT_MF_TOC_InfoSql.sql")]
        DT_MF_TOC GetDataForEntity(DT_MF_TOC data);

        /// <summary>
        /// DtoでLATEST_SEQとAPPROVAL_SEQ値取得する
        /// </summary>
        /// <param name="KanriId"></param>
        /// <returns></returns>
        [Sql("SELECT KANRI_ID,MANIFEST_ID,LATEST_SEQ,APPROVAL_SEQ FROM DT_MF_TOC /*$KanriId*/")]
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
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetDT_MF_MEMBER_InfoSql.sql")]
        DT_MF_MEMBER GetDataForEntity(DT_MF_MEMBER data);

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
        [NoPersistentProps(/*"KANRI_ID", "SEQ",  "UPN_ROUTE_NO",*/"MANIFEST_ID", "UPN_SHA_EDI_MEMBER_ID", "UPN_SHA_NAME", "UPN_SHA_POST", 
            "UPN_SHA_ADDRESS1", "UPN_SHA_ADDRESS2", "UPN_SHA_ADDRESS3", "UPN_SHA_ADDRESS4", "UPN_SHA_TEL", "UPN_SHA_FAX", 
            "UPN_SHA_KYOKA_ID", "SAI_UPN_SHA_EDI_MEMBER_ID", "SAI_UPN_SHA_NAME", "SAI_UPN_SHA_POST", "SAI_UPN_SHA_ADDRESS1", 
            "SAI_UPN_SHA_ADDRESS2", "SAI_UPN_SHA_ADDRESS3", "SAI_UPN_SHA_ADDRESS4", "SAI_UPN_SHA_TEL", "SAI_UPN_SHA_FAX", 
            "SAI_UPN_SHA_KYOKA_ID", "UPN_WAY_CODE", "UPN_TAN_NAME", "CAR_NO", "UPNSAKI_EDI_MEMBER_ID", "UPNSAKI_NAME", 
            "UPNSAKI_JOU_ID", "UPNSAKI_JOU_KBN", "UPNSAKI_JOU_NAME", "UPNSAKI_JOU_POST", "UPNSAKI_JOU_ADDRESS1", "UPNSAKI_JOU_ADDRESS2", 
            "UPNSAKI_JOU_ADDRESS3", "UPNSAKI_JOU_ADDRESS4", "UPNSAKI_JOU_TEL", "UPN_SHOUNIN_FLAG", "UPN_END_DATE", "UPNREP_UPN_TAN_NAME", 
            "UPNREP_CAR_NO", "UPN_SUU", "UPN_UNIT_CODE", "YUUKA_SUU", "YUUKA_UNIT_CODE", "REP_TAN_NAME", "BIKOU", "CREATE_DATE", "UPDATE_TS")]
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
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetAllDT_R19_InfoSql.sql")]
        DT_R19[] GetAllValidData(DT_R19 data);
        /// <summary>
        /// 運搬業者と現場情報を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetUnpanGyoushaInfoSql.sql")]
        DataTable GetGyoushaGenbaInfo(SearchMasterDataDTOCls data);

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
        [NoPersistentProps(/*"KANRI_ID", "SEQ", "REC_SEQ", */"MANIFEST_ID", "YUUGAI_CODE", "YUUGAI_NAME", "CREATE_DATE", "UPDATE_TS")]
        int Update(DT_R02 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R02 data);
        /// <summary>
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetAllDT_R02_InfoSql.sql")]
        DT_R02[] GetAllValidData(DT_R02 data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetExistDataFromR18_EXSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

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
        [NoPersistentProps(/*"KANRI_ID", "SEQ", "REC_SEQ", */"MANIFEST_ID", "LAST_SBN_JOU_NAME", 
            "LAST_SBN_JOU_POST", "LAST_SBN_JOU_ADDRESS1", "LAST_SBN_JOU_ADDRESS2", "LAST_SBN_JOU_ADDRESS3", 
            "LAST_SBN_JOU_ADDRESS4", "LAST_SBN_JOU_TEL", "CREATE_DATE", "UPDATE_TS")]
        int Update(DT_R04 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R04 data);

        /// <summary>
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetAllDT_R04_InfoSql.sql")]
        DT_R04[] GetAllValidData(DT_R04 data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetExistDataFromR18_EXSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

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
        [NoPersistentProps(/*"KANRI_ID", "SEQ", "RENRAKU_ID_NO", */"MANIFEST_ID","RENRAKU_ID", "CREATE_DATE", "UPDATE_TS")]
        int Update(DT_R05 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R05 data);

        /// <summary>
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetAllDT_R05_InfoSql.sql")]
        DT_R05[] GetAllValidData(DT_R05 data);
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetExistDataFromR18_EXSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

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
        [NoPersistentProps(/*"KANRI_ID", "SEQ", "BIKOU_NO",*/"MANIFEST_ID", "BIKOU", "CREATE_DATE", "UPDATE_TS")]
        int Update(DT_R06 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R06 data);

        /// <summary>
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetAllDT_R06_InfoSql.sql")]
        DT_R06[] GetAllValidData(DT_R06 data);
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetExistDataFromR18_EXSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

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
        [NoPersistentProps(/*"KANRI_ID", "SEQ", "REC_SEQ", */"MANIFEST_ID", "LAST_SBN_JOU_NAME", "LAST_SBN_JOU_POST", 
            "LAST_SBN_JOU_ADDRESS1", "LAST_SBN_JOU_ADDRESS2", "LAST_SBN_JOU_ADDRESS3", "LAST_SBN_JOU_ADDRESS4", 
            "LAST_SBN_JOU_TEL", "LAST_SBN_END_DATE", "CREATE_DATE", "UPDATE_TS")]
        int Update(DT_R13 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R13 data);
        /// <summary>
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetAllDT_R13_InfoSql.sql")]
        DT_R13[] GetAllValidData(DT_R13 data);
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetExistDataFromR18_EXSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

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
            "SHARYOU_CD", "KANSAN_SUU", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER",
            "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
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
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetDT_R18_EX_InfoSql.sql")]
        DT_R18_EX GetDataForEntity(DT_R18_EX data);
        /// <summary>
        /// DTOで絞り込んでデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetFirstManifestInElecSql.sql")]
        DataTable GetDataFromSearchDTO(SearchMasterDataDTOCls data);

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
        [NoPersistentProps(/*"SYSTEM_ID", "SEQ", */"KANRI_ID", "UPN_ROUTE_NO", "MANIFEST_ID", "UPN_GYOUSHA_CD", "NO_REP_UPN_EDI_MEMBER_ID", 
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
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetAllDT_R19_EX_InfoSql.sql")]
        DT_R19_EX[] GetAllValidData(DT_R19_EX data);
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetFirstManifestInElecSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

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
        [NoPersistentProps(/*"SYSTEM_ID", "SEQ", */"KANRI_ID", "REC_SEQ", "MANIFEST_ID", "LAST_SBN_GYOUSHA_CD", "LAST_SBN_GENBA_CD",
            "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(DT_R04_EX data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R04_EX data);

        /// <summary>
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetAllDT_R04_EX_InfoSql.sql")]
        DT_R04_EX[] GetAllValidData(DT_R04_EX data);
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetFirstManifestInElecSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

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
        [NoPersistentProps(/*"SYSTEM_ID", "SEQ", */"KANRI_ID", "REC_SEQ", "MANIFEST_ID", "LAST_SBN_GYOUSHA_CD", "LAST_SBN_GENBA_CD",
            "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(DT_R13_EX data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R13_EX data);

        /// <summary>
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetAllDT_R13_EX_InfoSql.sql")]
        DT_R13_EX[] GetAllValidData(DT_R13_EX data);
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetFirstManifestInElecSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

        /// <summary>
        /// 二次マニフェストの最終処分業者、最終処分場所を取得
        /// </summary>
        /// <param name="NEXT_SYSTEM_ID"></param>
        /// <param name="SEQ"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetSecondLastSbnInfoForElecMani.sql")]
        DataTable GetDataForEntitySecondLastSbnForElecMani(SqlInt64 NEXT_SYSTEM_ID, SqlInt32 SEQ);

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
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetElecDT_R18InfoSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

    }
   
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
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.DenshiHaikiShuruiSearchAndCheckSql.sql")]
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
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.DenshiHaikiNameSearchAndCheckSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

    }

    /// <summary>
    /// 電子業者検索用Dao
    /// </summary>
    [Bean(typeof(M_DENSHI_JIGYOUSHA))]
    public interface DENSHI_JIGYOUSHA_SearchDaoCls : IS2Dao
    {
        /// <summary>
        /// EDI_MEMBER_IDを元にデータを取得する
        /// </summary>
        /// <param name="ediMemberId"></param>
        /// <returns></returns>
        [Query("EDI_MEMBER_ID = /*ediMemberId*/")]
        M_DENSHI_JIGYOUSHA GetDataByCd(string ediMemberId);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.DenshiGyoushaSearchAndCheckSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

    }
    /// <summary>
    /// 電子事業場検索用Dao
    /// </summary>
    [Bean(typeof(M_DENSHI_JIGYOUJOU))]
    public interface DENSHI_JIGYOUJOU_SearchDaoCls : IS2Dao
    {
        /// <summary>
        /// EDI_MEMBER_ID、JIGYOUJOU_CDを元にデータを取得する
        /// </summary>
        /// <param name="ediMemberId"></param>
        /// <param name="jigyoujouCd"></param>
        /// <returns></returns>
        [Query("EDI_MEMBER_ID = /*ediMemberId*/ AND JIGYOUJOU_CD = /*jigyoujouCd*/")]
        M_DENSHI_JIGYOUJOU GetDataByCd(string ediMemberId, string jigyoujouCd);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.DenshiGenbaSearchAndCheckSql.sql")]
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
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.DenshiYougaibushituSearchAndCheckSql.sql")]
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
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.DenshiTantoushaSearchAndCheckSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

    }


    /// <summary>
    ///電子マニフェストパターン有害物質[DT_PT_R02]用Dao
    /// </summary>
    [Bean(typeof(DT_PT_R02))]
    public interface DT_PT_R02DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_PT_R02 data);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*"SYSTEM_ID", "SEQ", "REC_SEQ", */"MANIFEST_ID", "YUUGAI_CODE", "YUUGAI_NAME", "CREATE_DATE", "TIME_STAMP")]
        int Update(DT_PT_R02 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_PT_R02 data);
        /// <summary>
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetAllDT_PT_R02_InfoSql.sql")]
        DT_PT_R02[] GetAllValidData(DT_PT_R02 data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetExistDataFromR18_EXSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

    }

    /// <summary>
    ///電子マニフェストパターン最終処分(予定)[DT_PT_R04]用DAO
    /// </summary>
    [Bean(typeof(DT_PT_R04))]
    public interface DT_PT_R04DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_PT_R04 data);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*"SYSTEM_ID", "SEQ", "REC_SEQ", */"MANIFEST_ID", "LAST_SBN_JOU_NAME",
            "LAST_SBN_JOU_POST", "LAST_SBN_JOU_ADDRESS1", "LAST_SBN_JOU_ADDRESS2", "LAST_SBN_JOU_ADDRESS3",
            "LAST_SBN_JOU_ADDRESS4", "LAST_SBN_JOU_TEL", "LAST_SBN_GYOUSHA_CD", "LAST_SBN_GENBA_CD", "CREATE_DATE", "TIME_STAMP")]
        int Update(DT_PT_R04 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_PT_R04 data);

        /// <summary>
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetAllDT_PT_R04_InfoSql.sql")]
        DT_PT_R04[] GetAllValidData(DT_PT_R04 data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetExistDataFromR18_EXSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

    }

    /// <summary>
    ///電子マニフェストパターン連絡番号[DT_PT_R05]用DAO
    /// </summary>
    [Bean(typeof(DT_PT_R05))]
    public interface DT_PT_R05DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_PT_R05 data);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*"SYSTEM_ID", "SEQ", "RENRAKU_ID_NO", */"MANIFEST_ID", "RENRAKU_ID", "CREATE_DATE", "TIME_STAMP")]
        int Update(DT_PT_R05 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_PT_R05 data);

        /// <summary>
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetAllDT_PT_R05_InfoSql.sql")]
        DT_PT_R05[] GetAllValidData(DT_PT_R05 data);
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetExistDataFromR18_EXSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

    }

    /// <summary>
    ///電子マニフェストパターン備考[DT_PT_R06]用DAO
    /// </summary>
    [Bean(typeof(DT_PT_R06))]
    public interface DT_PT_R06DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_PT_R06 data);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*"SYSTEM_ID", "SEQ", "BIKOU_NO",*/"MANIFEST_ID", "BIKOU", "CREATE_DATE", "TIME_STAMP")]
        int Update(DT_PT_R06 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_PT_R06 data);

        /// <summary>
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetAllDT_PT_R06_InfoSql.sql")]
        DT_PT_R06[] GetAllValidData(DT_PT_R06 data);
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetExistDataFromR18_EXSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

    }

    /// <summary>
    ///電子マニフェストパターン最終処分[DT_PT_R13]用DAO
    /// </summary>
    [Bean(typeof(DT_PT_R13))]
    public interface DT_PT_R13DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_PT_R13 data);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*"SYSTEM_ID", "SEQ", "REC_SEQ", */"MANIFEST_ID", "LAST_SBN_JOU_NAME", "LAST_SBN_JOU_POST",
            "LAST_SBN_JOU_ADDRESS1", "LAST_SBN_JOU_ADDRESS2", "LAST_SBN_JOU_ADDRESS3", "LAST_SBN_JOU_ADDRESS4",
            "LAST_SBN_JOU_TEL", "LAST_SBN_END_DATE", "LAST_SBN_GYOUSHA_CD", "LAST_SBN_GENBA_CD", "CREATE_DATE", "TIME_STAMP")]
        int Update(DT_PT_R13 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_PT_R13 data);
        /// <summary>
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetAllDT_PT_R13_InfoSql.sql")]
        DT_PT_R13[] GetAllValidData(DT_PT_R13 data);
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetExistDataFromR18_EXSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

    }
    /// <summary>
    /// 電子マニフェストパターン
    /// </summary>
    [Bean(typeof(DT_PT_R18))]
    public interface DT_PT_R18DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_PT_R18 data);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*"SYSTEM_ID","SEQ",*/"PATTERN_NAME", "PATTERN_FURIGANA", "MANIFEST_ID", "MANIFEST_KBN", "SHOUNIN_FLAG", "HIKIWATASHI_DATE", "UPN_ENDREP_FLAG",
             "SBN_ENDREP_FLAG", "LAST_SBN_ENDREP_FLAG", "KAKIN_DATE", "REGI_DATE", "UPN_SBN_REP_LIMIT_DATE", "LAST_SBN_REP_LIMIT_DATE",
            "RESV_LIMIT_DATE", "SBN_ENDREP_KBN", "HST_SHA_EDI_MEMBER_ID", "HST_SHA_NAME", "HST_SHA_POST", "HST_SHA_ADDRESS1", "HST_SHA_ADDRESS2",
            "HST_SHA_ADDRESS3", "HST_SHA_ADDRESS4", "HST_SHA_TEL", "HST_SHA_FAX", "HST_JOU_NAME", "HST_JOU_POST_NO", "HST_JOU_ADDRESS1",
            "HST_JOU_ADDRESS2", "HST_JOU_ADDRESS3", "HST_JOU_ADDRESS4", "HST_JOU_TEL", "REGI_TAN", "HIKIWATASHI_TAN_NAME", "HAIKI_DAI_CODE",
            "HAIKI_CHU_CODE", "HAIKI_SHO_CODE", "HAIKI_SAI_CODE", "HAIKI_BUNRUI", "HAIKI_SHURUI", "HAIKI_NAME", "HAIKI_SUU", "HAIKI_UNIT_CODE",
            "SUU_KAKUTEI_CODE", "HAIKI_KAKUTEI_SUU", "HAIKI_KAKUTEI_UNIT_CODE", "NISUGATA_CODE", "NISUGATA_NAME", "NISUGATA_SUU", "SBN_SHA_MEMBER_ID",
            "SBN_SHA_NAME", "SBN_SHA_POST", "SBN_SHA_ADDRESS1", "SBN_SHA_ADDRESS2", "SBN_SHA_ADDRESS3", "SBN_SHA_ADDRESS4", "SBN_SHA_TEL",
            "SBN_SHA_FAX", "SBN_SHA_KYOKA_ID", "SAI_SBN_SHA_MEMBER_ID", "SAI_SBN_SHA_NAME", "SAI_SBN_SHA_POST", "SAI_SBN_SHA_ADDRESS1",
            "SAI_SBN_SHA_ADDRESS2", "SAI_SBN_SHA_ADDRESS3", "SAI_SBN_SHA_ADDRESS4", "SAI_SBN_SHA_TEL", "SAI_SBN_SHA_FAX", "SAI_SBN_SHA_KYOKA_ID",
            "SBN_WAY_CODE", "SBN_WAY_NAME", "SBN_SHOUNIN_FLAG", "SBN_END_DATE", "HAIKI_IN_DATE", "RECEPT_SUU", "RECEPT_UNIT_CODE", "UPN_TAN_NAME",
            "CAR_NO", "REP_TAN_NAME", "SBN_TAN_NAME", "SBN_END_REP_DATE", "SBN_REP_BIKOU", "KENGEN_CODE", "LAST_SBN_JOU_KISAI_FLAG",
            "FIRST_MANIFEST_FLAG", "LAST_SBN_END_DATE", "LAST_SBN_END_REP_DATE", "SHUSEI_DATE", "CANCEL_FLAG", "CANCEL_DATE", "LAST_UPDATE_DATE",
            "YUUGAI_CNT", "UPN_ROUTE_CNT", "LAST_SBN_PLAN_CNT", "LAST_SBN_CNT", "RENRAKU_CNT", "BIKOU_CNT", "FIRST_MANIFEST_CNT", "HST_GYOUSHA_CD",
            "HST_GENBA_CD", "SBN_GYOUSHA_CD", "SBN_GENBA_CD", "NO_REP_SBN_EDI_MEMBER_ID", "SBN_KYOKA_NO", "HAIKI_NAME_CD", "SBN_HOUHOU_CD",
            "HOUKOKU_TANTOUSHA_CD", "SBN_TANTOUSHA_CD", "UPN_TANTOUSHA_CD", "SHARYOU_CD", "KANSAN_SUU", "CREATE_USER", "CREATE_DATE", "CREATE_PC",
            "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "DELETE_FLG", "TIME_STAMP")]
        int Update(DT_PT_R18 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_PT_R18 data);
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetDT_PT_R18_InfoSql.sql")]
        DT_PT_R18 GetDataForEntity(DT_PT_R18 data);


        //以下のメッソドを利用実行しない

        /// <summary>
        ///換算式と換算値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetKansanshikiKansanchiSql.sql")]
        DataTable GetKansanshikiKansanchiData(SearchMasterDataDTOCls data);

        [Procedure("get_next_dt_mf_toc_kanri_id")]
        int GetByJob(out string kanri_id);
        /// <summary>
        /// 同管理IDで枝番号SEQの最大値取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetMaxSeqFromDT_R18Sql.sql")]
        DataTable GetMaxSeqFromDT_R18(SearchMasterDataDTOCls data);

    }

    /// <summary>
    /// 電子マニフェストパターン収集運搬[DT_PT_R19]用Dao
    /// </summary>
    [Bean(typeof(DT_PT_R19))]
    public interface DT_PT_R19DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_PT_R19 data);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*"SYSTEM_ID","SEQ","MANIFEST_ID","UPN_ROUTE_NO",*/"UPN_SHA_EDI_MEMBER_ID", "UPN_SHA_NAME", "UPN_SHA_POST", "UPN_SHA_ADDRESS1",
            "UPN_SHA_ADDRESS2", "UPN_SHA_ADDRESS3", "UPN_SHA_ADDRESS4", "UPN_SHA_TEL", "UPN_SHA_FAX", "UPN_SHA_KYOKA_ID", "SAI_UPN_SHA_EDI_MEMBER_ID",
            "SAI_UPN_SHA_NAME", "SAI_UPN_SHA_POST", "SAI_UPN_SHA_ADDRESS1", "SAI_UPN_SHA_ADDRESS2", "SAI_UPN_SHA_ADDRESS3", "SAI_UPN_SHA_ADDRESS4",
            "SAI_UPN_SHA_TEL", "SAI_UPN_SHA_FAX", "SAI_UPN_SHA_KYOKA_ID", "UPN_WAY_CODE", "UPN_TAN_NAME", "CAR_NO", "UPNSAKI_EDI_MEMBER_ID",
            "UPNSAKI_NAME", "UPNSAKI_JOU_ID", "UPNSAKI_JOU_KBN", "UPNSAKI_JOU_NAME", "UPNSAKI_JOU_POST", "UPNSAKI_JOU_ADDRESS1", "UPNSAKI_JOU_ADDRESS2",
            "UPNSAKI_JOU_ADDRESS3", "UPNSAKI_JOU_ADDRESS4", "UPNSAKI_JOU_TEL", "UPN_SHOUNIN_FLAG", "UPN_END_DATE", "UPNREP_UPN_TAN_NAME", "UPNREP_CAR_NO", 
            "UPN_SUU", "UPN_UNIT_CODE", "YUUKA_SUU", "YUUKA_UNIT_CODE", "REP_TAN_NAME", "BIKOU", "UPN_GYOUSHA_CD", "NO_REP_UPN_EDI_MEMBER_ID",
            "UPNSAKI_GYOUSHA_CD", "NO_REP_UPNSAKI_EDI_MEMBER_ID", "UPNSAKI_GENBA_CD", "UPN_TANTOUSHA_CD", "SHARYOU_CD", "UPNREP_UPN_TANTOUSHA_CD",
            "UPNREP_SHARYOU_CD", "HOUKOKU_TANTOUSHA_CD", "CREATE_DATE", "TIME_STAMP")]
        int Update(DT_PT_R19 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_PT_R19 data);

        /// <summary>
        /// 運搬情報取得処理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetAllDT_PT_R19_InfoSql.sql")]
        DT_PT_R19[] GetAllValidData(DT_PT_R19 data);
        /// <summary>
        /// 運搬業者と現場情報を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetUnpanGyoushaInfoSql.sql")]
        DataTable GetGyoushaGenbaInfo(SearchMasterDataDTOCls data);

    }

    /// <summary>
    ///電子マニフェストパターン拡張[DT_PT_R18_EX]用Dao
    /// </summary>
    [Bean(typeof(DT_PT_R18_EX))]
    public interface DT_PT_R18EXDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_PT_R18_EX data);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*"SYSTEM_ID", "SEQ", "REC_SEQ", */"HAIKI_SHURUI_CODE", "HAIKI_SHURUI_NAME", "HAIKI_NAME_CODE", "HAIKI_NAME",
            "HAIKI_SUU", "UNIT_CODE", "UNIT_NAME", "KANSAN_SUU", "GENNYOU_SUU", "NISUGATA_CODE", "NISUGATA_NAME", "NISUGATA_SUU",
            "SUU_KAKUTEI_CODE", "SUU_KAKUTEI_NAME", "YUUGAI_CODE1", "YUUGAI_NAME1", "YUUGAI_CODE2", "YUUGAI_NAME2",
            "YUUGAI_CODE3", "YUUGAI_NAME3", "YUUGAI_CODE4", "YUUGAI_NAME4", "YUUGAI_CODE5", "YUUGAI_NAME5", "YUUGAI_CODE6", "YUUGAI_NAME6",
            "CREATE_DATE", "TIME_STAMP")]
        int Update(DT_PT_R18_EX data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_PT_R18_EX data);
        
        /// <summary>
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetAllDT_PT_R18_EX_InfoSql.sql")]
        DT_PT_R18_EX[] GetAllValidData(DT_PT_R18_EX data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetExistDataFromR18_EXSql.sql")]
        DataTable GetDataForEntity(SearchMasterDataDTOCls data);

    }

    /// <summary>
    /// 中間処理産業廃棄物情報
    /// </summary>
    [Bean(typeof(DT_MF_TOC))]
    public interface FirstManifestInfoDaoCls : IS2Dao
    {
        /// <summary>
        /// 中間処理産業廃棄物情報を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetFirstManifestInfo.sql")]
        DataTable GetFirstManifestInfo(SearchMasterDataDTOCls data);

        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetFirstManifestInfoall.sql")]
        DataTable GetFirstManifestInfoall(SearchMasterDataDTOCls search);
    }

    /// <summary>
    /// 最終処分終了報告の取消情報
    /// </summary>
    [Bean(typeof(DT_R18_EX))]
    public interface LastSbnEndrepCancelInfoDaoCls : IS2Dao
    {
        /// <summary>
        /// 最終処分終了報告の取消情報を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetLastSbnEndrepCancelInfo.sql")]
        DataTable GetLastSbnEndrepCancelInfo(SearchMasterDataDTOCls data);
    }

    /// <summary>
    /// 最終処分終了報告情報
    /// </summary>
    [Bean(typeof(DT_R18_EX))]
    public interface LastSbnEndrepInfoDaoCls : IS2Dao
    {
        /// <summary>
        /// 最終処分終了報告情報を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetLastSbnEndrepInfo.sql")]
        DataTable GetLastSbnEndrepInfo(SearchMasterDataDTOCls data);

        /// <summary>
        /// DT_R18_MIX.SBN_ENDREP_KBN=2のデータを取得する(最終処分終了報告用)
        /// <param name="data">KANRI_ID</param>
        /// </summary>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetMixManifestForLastSbnData.sql")]
        DataTable GetMixManifestForLastSbnData(List<string> data);
    }

    /// <summary>
    /// 1次マニフェスト情報
    /// </summary>
    [Bean(typeof(DT_R08))]
    public interface DT_R08DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_R08 data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*KANRI_ID,SEQ,REC_SEQ*/ 
            "MANIFEST_ID", "MEDIA_TYPE", "FIRST_MANIFEST_ID", "RENRAKU_ID", "KOUHU_DATE", "SBN_END_DATE",
            "HST_SHA_NAME","HST_JOU_NAME","HAIKI_SHURUI","HAIKI_SUU","HAIKI_SUU_UNIT","CREATE_DATE","UPDATE_TS")]
        int Update(DT_R08 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R08 data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetDT_R08_InfoSql.sql")]
        DT_R08[] GetAllValidData(DT_R08 data);
    }

    /// <summary>
    /// 一次マニフェスト情報拡張
    /// </summary>
    [Bean(typeof(DT_R08_EX))]
    public interface DT_R08_EXDaoCls : IS2Dao
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
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*SYSTEM_ID,SEQ,REC_SEQ*/
            "KANRI_ID", "MANIFEST_ID", "HST_GYOUSHA_CD", "HST_GENBA_CD", "HAIKI_SHURUI_CD", "CREATE_USER",
            "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(DT_R08_EX data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_R08_EX data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetDT_R08_EX_InfoSql.sql")]
        DT_R08_EX[] GetAllValidData(DT_R08_EX data);
    }

    /// <summary>
    /// マニフェスト紐付
    /// </summary>
    [Bean(typeof(T_MANIFEST_RELATION))]
    public interface T_MANIFEST_RELATIONDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_RELATION data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(/*NEXT_SYSTEM_ID,SEQ,REC_SEQ*/
            "NEXT_HAIKI_KBN_CD","FIRST_SYSTEM_ID","FIRST_HAIKI_KBN_CD","CREATE_USER","CREATE_DATE","CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_RELATION data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_MANIFEST_RELATION data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetT_MANIFEST_RELATION_InfoSql.sql")]
        T_MANIFEST_RELATION[] GetAllValidData(T_MANIFEST_RELATION data);

        /// <summary>
        /// 二次マニのシステムIDから紐付情報を取得する(一次紙マニのみ)
        /// </summary>
        /// <param name="NEXT_SYSTEM_ID"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.Get_MANIFEST_RELATION_SYSTEMID.sql")]
        DataTable GetManiRelationByNextSysId(SqlInt64 NEXT_SYSTEM_ID);

        /// <summary>
        /// 二次マニのシステムID(DT_R18_EX.SYSTEM_ID)から紐付け情報を取得する(一次電マニ含む)
        /// </summary>
        /// <param name="nextSysId"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetRelationFirstMani.sql")]
        DataTable GetRelationFirstMani(SqlInt64 nextSysId);

        /// <summary>
        /// 一次マニのシステムID(DT_R18_EX.SYSTEM_ID)から紐付け情報を取得する(一次電マニ含む)
        /// </summary>
        /// <param name="nextSysId"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Sql.GetRelationNextMani.sql")]
        DataTable GetRelationNexttMani(SqlInt64 firstExSysId);
    }

    /// <summary>
    /// 紐付されているマニフェスト明細の検索、更新用Dao
    /// </summary>
    [Bean(typeof(T_MANIFEST_DETAIL))]
    public interface TMDDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="SYSTEM_ID"></param>
        /// <param name="SEQ"></param>
        /// <param name="DETAIL_SYSTEM_ID"></param>
        /// <returns></returns>
        T_MANIFEST_DETAIL GetDataForEntity(SqlInt64 SYSTEM_ID, SqlInt32 SEQ, SqlInt64 DETAIL_SYSTEM_ID);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Update(T_MANIFEST_DETAIL data);
    }

    
}
