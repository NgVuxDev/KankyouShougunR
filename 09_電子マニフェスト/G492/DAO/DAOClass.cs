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
using Shougun.Core.ElectronicManifest.DenshiManifestPatternTouroku.Dto;
using System.Data.SqlTypes;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ElectronicManifest.DenshiManifestPatternTouroku.Dao
{
    /// <summary>
    /// 電子マニフェストの場合、パターン名とパターンふりがな取得
    /// </summary>
    [Bean(typeof(DT_PT_R18))]
    public interface GetElectronicPatternDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.ElectronicManifest.DenshiManifestPatternTouroku.Sql.GetElectronicPattern.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);

    }

    /// <summary>
    /// 電子マニフェストパターン
    /// </summary>
    [Bean(typeof(DT_PT_R18))]
    public interface EpEntryDaoCls : IS2Dao
    {

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_PT_R18 data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("PATTERN_NAME", "PATTERN_FURIGANA","MANIFEST_ID", "MANIFEST_KBN", "SHOUNIN_FLAG", 
            "HIKIWATASHI_DATE", "UPN_ENDREP_FLAG", "SBN_ENDREP_FLAG", "LAST_SBN_ENDREP_FLAG", "KAKIN_DATE",
            "REGI_DATE", "UPN_SBN_REP_LIMIT_DATE", "LAST_SBN_REP_LIMIT_DATE", "RESV_LIMIT_DATE", "SBN_ENDREP_KBN",
            "HST_SHA_EDI_MEMBER_ID", "HST_SHA_NAME", "HST_SHA_POST", "HST_SHA_ADDRESS1", "HST_SHA_ADDRESS2",
            "HST_SHA_ADDRESS3", "HST_SHA_ADDRESS4", "HST_SHA_TEL", "HST_SHA_FAX", "HST_JOU_NAME", "HST_JOU_POST_NO",
            "HST_JOU_ADDRESS1", "HST_JOU_ADDRESS2", "HST_JOU_ADDRESS3", "HST_JOU_ADDRESS4", "HST_JOU_TEL", "REGI_TAN",
            "HIKIWATASHI_TAN_NAME", "HAIKI_DAI_CODE", "HAIKI_CHU_CODE", "HAIKI_SHO_CODE", "HAIKI_SAI_CODE", 
            "HAIKI_BUNRUI", "HAIKI_SHURUI", "HAIKI_NAME", "HAIKI_SUU", "HAIKI_UNIT_CODE", "SUU_KAKUTEI_CODE", 
            "HAIKI_KAKUTEI_SUU", "HAIKI_KAKUTEI_UNIT_CODE", "NISUGATA_CODE", "NISUGATA_NAME", "NISUGATA_SUU",
            "SBN_SHA_MEMBER_ID", "SBN_SHA_NAME", "SBN_SHA_POST", "SBN_SHA_ADDRESS1", "SBN_SHA_ADDRESS2", 
            "SBN_SHA_ADDRESS3", "SBN_SHA_ADDRESS4", "SBN_SHA_TEL", "SBN_SHA_FAX", "SBN_SHA_KYOKA_ID", 
            "SAI_SBN_SHA_MEMBER_ID", "SAI_SBN_SHA_NAME", "SAI_SBN_SHA_POST", "SAI_SBN_SHA_ADDRESS1", 
            "SAI_SBN_SHA_ADDRESS2", "SAI_SBN_SHA_ADDRESS3", "SAI_SBN_SHA_ADDRESS4", "SAI_SBN_SHA_TEL", 
            "SAI_SBN_SHA_FAX", "SAI_SBN_SHA_KYOKA_ID", "SBN_WAY_CODE", "SBN_WAY_NAME", "SBN_SHOUNIN_FLAG", 
            "SBN_END_DATE", "HAIKI_IN_DATE", "RECEPT_SUU", "RECEPT_UNIT_CODE", "UPN_TAN_NAME", "CAR_NO", "REP_TAN_NAME",
            "SBN_TAN_NAME", "SBN_END_REP_DATE", "SBN_REP_BIKOU", "KENGEN_CODE", "LAST_SBN_JOU_KISAI_FLAG", 
            "FIRST_MANIFEST_FLAG", "LAST_SBN_END_DATE", "LAST_SBN_END_REP_DATE", "SHUSEI_DATE", "CANCEL_FLAG", 
            "CANCEL_DATE", "LAST_UPDATE_DATE", "YUUGAI_CNT", "UPN_ROUTE_CNT", "LAST_SBN_PLAN_CNT", "LAST_SBN_CNT",
            "RENRAKU_CNT", "BIKOU_CNT", "FIRST_MANIFEST_CNT", "HST_GYOUSHA_CD", "HST_GENBA_CD", "SBN_GYOUSHA_CD", 
            "SBN_GENBA_CD", "NO_REP_SBN_EDI_MEMBER_ID", "SBN_KYOKA_NO", "HAIKI_NAME_CD", "SBN_HOUHOU_CD", 
            "HOUKOKU_TANTOUSHA_CD", "SBN_TANTOUSHA_CD", "UPN_TANTOUSHA_CD", "SHARYOU_CD", "KANSAN_SUU",
            "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(DT_PT_R18 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(DT_PT_R18 data);

        /// <summary>
        /// 重複する名前が居たら取得
        /// </summary>
        /// <param name="PATTERN_NAME">パターン名</param>
        /// <param name="DELETE_FLG">削除フラグ。falseで検索すること。</param>
        /// <returns></returns>
        DT_PT_R18[] SelectByName(SqlString PATTERN_NAME, SqlBoolean DELETE_FLG);

    }

    /// <summary>
    /// 電子マニフェストパターン収集運搬
    /// </summary>
    [Bean(typeof(DT_PT_R19))]
    public interface EpUpnDaoCls : IS2Dao
    {

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_PT_R19 data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(DT_PT_R19 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(DT_PT_R19 data);
    }

    /// <summary>
    /// 電子マニフェストパターン最終処分
    /// </summary>
    [Bean(typeof(DT_PT_R13))]
    public interface EpLastSbnDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_PT_R13 data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(DT_PT_R13 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(DT_PT_R13 data);
    }

    /// <summary>
    /// 電子マニフェストパターン備考
    /// </summary>
    [Bean(typeof(DT_PT_R06))]
    public interface EpBikouDaoCls : IS2Dao
    {

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_PT_R06 data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(DT_PT_R06 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(DT_PT_R06 data);
    }

    /// <summary>
    /// 電子マニフェストパターン連絡番号
    /// </summary>
    [Bean(typeof(DT_PT_R05))]
    public interface EpRenrakuDaoCls : IS2Dao
    {

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_PT_R05 data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(DT_PT_R05 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(DT_PT_R05 data);
    }

    /// <summary>
    /// 電子マニフェストパターン最終処分(予定)
    /// </summary>
    [Bean(typeof(DT_PT_R04))]
    public interface EpLastSbnYoteiDaoCls : IS2Dao
    {

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_PT_R04 data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(DT_PT_R04 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(DT_PT_R04 data);
    }

    /// <summary>
    /// 電子マニフェストパターン有害物質
    /// </summary>
    [Bean(typeof(DT_PT_R02))]
    public interface EpYuugaiDaoCls : IS2Dao
    {

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_PT_R02 data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(DT_PT_R02 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(DT_PT_R02 data);
    }

    /// <summary>
    /// 電子マニフェストパターン拡張
    /// </summary>
    [Bean(typeof(DT_PT_R18_EX))]
    public interface EpEntryEXDaoCls : IS2Dao
    {

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_PT_R18_EX data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(DT_PT_R18_EX data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(DT_PT_R18_EX data);
    }

}
