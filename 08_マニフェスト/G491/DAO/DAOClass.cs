using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PaperManifest.ManifestPatternTouroku
{
    /// <summary>
    /// マニフェストパターン更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_PT_ENTRY))]
    public interface PtEntryDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_PT_ENTRY data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("LIST_REGIST_KBN","HAIKI_KBN_CD","FIRST_MANIFEST_KBN","PATTERN_NAME","PATTERN_FURIGANA",
            "USE_DEFAULT_KBN","KYOTEN_CD","TORIHIKISAKI_CD","JIZEN_NUMBER","JIZEN_DATE","SEARCH_JIZEN_DATE",
            "KOUFU_DATE", "SEARCH_KOUFU_DATE", "KOUFU_KBN", "MANIFEST_ID", "SEIRI_ID", "KOUFU_TANTOUSHA", "KOUFU_TANTOUSHA_SHOZOKU", "HST_GYOUSHA_CD",
            "HST_GYOUSHA_NAME","HST_GYOUSHA_POST","HST_GYOUSHA_TEL","HST_GYOUSHA_ADDRESS","HST_GENBA_CD",
            "HST_GENBA_NAME","HST_GENBA_POST","HST_GENBA_TEL","HST_GENBA_ADDRESS","BIKOU","KONGOU_SHURUI_CD",
            "HAIKI_SUU","HAIKI_UNIT_CD","TOTAL_SUU","TOTAL_KANSAN_SUU","TOTAL_GENNYOU_SUU","CHUUKAN_HAIKI_KBN",
            "CHUUKAN_HAIKI","LAST_SBN_YOTEI_KBN","LAST_SBN_YOTEI_GYOUSHA_CD","LAST_SBN_YOTEI_GENBA_CD",
            "LAST_SBN_YOTEI_GENBA_NAME","LAST_SBN_YOTEI_GENBA_POST","LAST_SBN_YOTEI_GENBA_TEL",
            "LAST_SBN_YOTEI_GENBA_ADDRESS","SBN_GYOUSHA_CD","SBN_GYOUSHA_NAME","SBN_GYOUSHA_POST","SBN_GYOUSHA_TEL",
            "SBN_GYOUSHA_ADDRESS","TMH_GYOUSHA_CD","TMH_GYOUSHA_NAME","TMH_GENBA_CD","TMH_GENBA_NAME","TMH_GENBA_POST",
            "TMH_GENBA_TEL","TMH_GENBA_ADDRESS","YUUKA_KBN","YUUKA_SUU","YUUKA_UNIT_CD","SBN_JYURYOUSHA_CD",
            "SBN_JYURYOUSHA_NAME","SBN_JYURYOU_TANTOU_CD","SBN_JYURYOU_TANTOU_NAME","SBN_JYURYOU_DATE",
            "SEARCH_SBN_JYURYOU_DATE","SBN_JYUTAKUSHA_CD","SBN_JYUTAKUSHA_NAME","SBN_TANTOU_CD","SBN_TANTOU_NAME",
            "LAST_SBN_GYOUSHA_CD","LAST_SBN_GENBA_CD","LAST_SBN_GENBA_NAME","LAST_SBN_GENBA_POST","LAST_SBN_GENBA_TEL",
            "LAST_SBN_GENBA_ADDRESS","LAST_SBN_GENBA_NUMBER","LAST_SBN_CHECK_NAME","CHECK_B1","SEARCH_CHECK_B1",
            "CHECK_B2","SEARCH_CHECK_B2","CHECK_B4","SEARCH_CHECK_B4","CHECK_B6","SEARCH_CHECK_B6","CHECK_D",
            "SEARCH_CHECK_D","CHECK_E","SEARCH_CHECK_E","RENKEI_DENSHU_KBN_CD","RENKEI_SYSTEM_ID",
            "RENKEI_MEISAI_SYSTEM_ID", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_PT_ENTRY data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_PT_ENTRY data);

        /// <summary>
        /// 重複する名前が居たら取得
        /// </summary>
        /// <param name="LIST_REGIST_KBN">一括登録区分</param>
        /// <param name="HAIKI_KBN_CD">廃棄物区分CD</param>
        /// <param name="FIRST_MANIFEST_KBN">一次マニフェスト区分</param>
        /// <param name="PATTERN_NAME">パターン名</param>
        /// <param name="DELETE_FLG">削除フラグ。falseで検索すること。</param>
        /// <param name="KYOTEN_CD">拠点</param>
        /// <returns></returns>
        T_MANIFEST_PT_ENTRY[] SelectByName(SqlBoolean LIST_REGIST_KBN, SqlInt16 HAIKI_KBN_CD, SqlBoolean FIRST_MANIFEST_KBN, SqlString PATTERN_NAME, SqlBoolean DELETE_FLG, SqlInt16 KYOTEN_CD);

    }

    /// <summary>
    /// マニフェストパターン収集運搬更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_PT_UPN))]
    public interface PtUpnDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_PT_UPN data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_PT_UPN data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_PT_UPN data);
    }

    /// <summary>
    /// マニフェストパターン印字更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_PT_PRT))]
    public interface PtPrtDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_PT_PRT data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_PT_PRT data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_PT_PRT data);
    }

    /// <summary>
    /// マニフェストパターン印字明細更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_PT_DETAIL_PRT))]
    public interface PtDetailPrtDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_PT_DETAIL_PRT data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_PT_DETAIL_PRT data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_PT_DETAIL_PRT data);

    }

    /// <summary>
    /// マニフェストパターン印字_産廃_形状更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_PT_KP_KEIJYOU))]
    public interface PtKeijyouDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_PT_KP_KEIJYOU data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_PT_KP_KEIJYOU data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_PT_KP_KEIJYOU data);

    }

    /// <summary>
    /// マニフェストパターン印字_産廃_荷姿更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_PT_KP_NISUGATA))]
    public interface PtNisugataDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_PT_KP_NISUGATA data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_PT_KP_NISUGATA data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_PT_KP_NISUGATA data);

    }

    /// <summary>
    /// マニフェストパターン印字_産廃_処分方法更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_PT_KP_SBN_HOUHOU))]
    public interface PtHouhouDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_PT_KP_SBN_HOUHOU data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_PT_KP_SBN_HOUHOU data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_PT_KP_SBN_HOUHOU data);
    }

    /// <summary>
    /// マニフェストパターン明細更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_PT_DETAIL))]
    public interface PtDetailDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_PT_DETAIL data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_PT_DETAIL data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_PT_DETAIL data);
    }

    [Bean(typeof(T_MANIFEST_PT_ENTRY))]
    public interface PtMaxSeqDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestPatternTouroku.Sql.GetMaxPtSeq.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);
    }

    /// <summary>
    /// 紙マニフェストの場合、パターン名とパターンふりがな取得
    /// </summary>
    [Bean(typeof(T_MANIFEST_PT_ENTRY))]
    public interface GetPatternDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestPatternTouroku.Sql.GetPattern.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);

    }

    /// <summary>
    /// 電子マニフェストの場合、パターン名とパターンふりがな取得
    /// </summary>
    [Bean(typeof(DT_PT_R18))]
    public interface GetDenshiPatternDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestPatternTouroku.Sql.GetDenshiPattern.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);

    }
}
