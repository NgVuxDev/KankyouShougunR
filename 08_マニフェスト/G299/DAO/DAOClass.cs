using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Collections.Generic;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PaperManifest.ManifestPattern.DAO
{
    //[Bean(typeof(M_SYS_INFO))]
    //public interface GetMSIDaoCls : IS2Dao
    //{
    //    /// <summary>
    //    /// Entityで絞り込んで値を取得する
    //    /// </summary>
    //    /// <param name="SYS_ID">ID</param>
    //    /// <returns>DAOClass</returns>
    //    [SqlFile("Shougun.Core.PaperManifest.ManifestPattern.Sql.GetSystemInfo.sql")]
    //    DataTable GetDataForEntity(MSIDtoCls data);
    //}

    [Bean(typeof(M_OUTPUT_PATTERN))]
    public interface GetResultDaoCls : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable getdateforstringsql(string sql);
    }

    //[Bean(typeof(T_MANIFEST_PT_ENTRY))]
    //public interface GetTMPEDaoCls : IS2Dao
    //{
    //    /// <summary>
    //    /// Entityで絞り込んで値を取得する
    //    /// </summary>
    //    /// <param name="SYSTEM_ID">システムID</param>
    //    /// <param name="SEQ">枝番</param>
    //    /// <param name="LIST_REGIST_KBN">一括登録区分</param>
    //    /// <param name="HAIKI_KBN_CD">廃棄物区分CD</param>
    //    /// <param name="FIRST_MANIFEST_KBN">一次マニフェスト区分</param>
    //    /// <param name="DELETE_FLG">削除フラグ</param>
    //    [SqlFile("Shougun.Core.PaperManifest.ManifestPattern.Sql.GetManifestPattern.sql")]
    //    DataTable GetDataForEntity(TMPEDtoCls data);
    //}

    [Bean(typeof(T_MANIFEST_PT_ENTRY))]
    public interface SetTMPEDaoCls : IS2Dao
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
        [NoPersistentProps("SYSTEM_ID"
            , "SEQ"
            , "LIST_REGIST_KBN"
            , "HAIKI_KBN_CD"
            , "FIRST_MANIFEST_KBN"
            , "PATTERN_NAME"
            , "PATTERN_FURIGANA"
            , "USE_DEFAULT_KBN"
            , "KYOTEN_CD"
            , "TORIHIKISAKI_CD"
            , "JIZEN_NUMBER"
            , "JIZEN_DATE"
            , "KOUFU_DATE"
            , "KOUFU_KBN"
            , "MANIFEST_ID"
            , "SEIRI_ID"
            , "KOUFU_TANTOUSHA"
            , "KOUFU_TANTOUSHA_SHOZOKU"
            , "HST_GYOUSHA_CD"
            , "HST_GYOUSHA_NAME"
            , "HST_GYOUSHA_POST"
            , "HST_GYOUSHA_TEL"
            , "HST_GYOUSHA_ADDRESS"
            , "HST_GENBA_CD"
            , "HST_GENBA_NAME"
            , "HST_GENBA_POST"
            , "HST_GENBA_TEL"
            , "HST_GENBA_ADDRESS"
            , "BIKOU"
            , "KONGOU_SHURUI_CD"
            , "HAIKI_SUU"
            , "HAIKI_UNIT_CD"
            , "TOTAL_SUU"
            , "TOTAL_KANSAN_SUU"
            , "TOTAL_GENNYOU_SUU"
            , "CHUUKAN_HAIKI_KBN"
            , "CHUUKAN_HAIKI"
            , "LAST_SBN_YOTEI_KBN"
            , "LAST_SBN_YOTEI_GYOUSHA_CD"
            , "LAST_SBN_YOTEI_GENBA_CD"
            , "LAST_SBN_YOTEI_GENBA_NAME"
            , "LAST_SBN_YOTEI_GENBA_POST"
            , "LAST_SBN_YOTEI_GENBA_TEL"
            , "LAST_SBN_YOTEI_GENBA_ADDRESS"
            , "SBN_GYOUSHA_CD"
            , "SBN_GYOUSHA_NAME"
            , "SBN_GYOUSHA_POST"
            , "SBN_GYOUSHA_TEL"
            , "SBN_GYOUSHA_ADDRESS"
            , "TMH_GYOUSHA_CD"
            , "TMH_GYOUSHA_NAME"
            , "TMH_GENBA_CD"
            , "TMH_GENBA_NAME"
            , "TMH_GENBA_POST"
            , "TMH_GENBA_TEL"
            , "TMH_GENBA_ADDRESS"
            , "YUUKA_KBN"
            , "YUUKA_SUU"
            , "YUUKA_UNIT_CD"
            , "SBN_JYURYOUSHA_CD"
            , "SBN_JYURYOUSHA_NAME"
            , "SBN_JYURYOU_TANTOU_CD"
            , "SBN_JYURYOU_TANTOU_NAME"
            , "SBN_JYURYOU_DATE"
            , "SBN_JYUTAKUSHA_CD"
            , "SBN_JYUTAKUSHA_NAME"
            , "SBN_TANTOU_CD"
            , "SBN_TANTOU_NAME"
            , "LAST_SBN_GYOUSHA_CD"
            , "LAST_SBN_GENBA_CD"
            , "LAST_SBN_GENBA_NAME"
            , "LAST_SBN_GENBA_POST"
            , "LAST_SBN_GENBA_TEL"
            , "LAST_SBN_GENBA_ADDRESS"
            , "LAST_SBN_GENBA_NUMBER"
            , "LAST_SBN_CHECK_NAME"
            , "CHECK_B1"
            , "CHECK_B2"
            , "CHECK_B4"
            , "CHECK_B6"
            , "CHECK_D"
            , "CHECK_E"
            , "RENKEI_DENSHU_KBN_CD"
            , "RENKEI_SYSTEM_ID"
            , "RENKEI_MEISAI_SYSTEM_ID"
            , "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_PT_ENTRY data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_PT_ENTRY data);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="SYSTEM_ID">画面．システムID</param>
        /// <param name="SEQ">画面．SEQ</param>
        /// <returns></returns>
        T_MANIFEST_PT_ENTRY GetDataForEntity(int SYSTEM_ID, int SEQ);
    }

    // 20140529 syunrei No.730 マニフェストパターン一覧 start

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

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="SYSTEM_ID">画面．システムID</param>
        /// <param name="SEQ">画面．SEQ</param>
        /// <returns></returns>
        T_MANIFEST_PT_UPN[] GetDataForEntity(int SYSTEM_ID, int SEQ);
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

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="SYSTEM_ID">画面．システムID</param>
        /// <param name="SEQ">画面．SEQ</param>
        /// <returns></returns>
        T_MANIFEST_PT_PRT[] GetDataForEntity(int SYSTEM_ID, int SEQ);
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

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="SYSTEM_ID">画面．システムID</param>
        /// <param name="SEQ">画面．SEQ</param>
        /// <returns></returns>
        T_MANIFEST_PT_DETAIL_PRT[] GetDataForEntity(int SYSTEM_ID, int SEQ);
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

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="SYSTEM_ID">画面．システムID</param>
        /// <param name="SEQ">画面．SEQ</param>
        /// <returns></returns>
        T_MANIFEST_PT_KP_KEIJYOU[] GetDataForEntity(int SYSTEM_ID, int SEQ);

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

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="SYSTEM_ID">画面．システムID</param>
        /// <param name="SEQ">画面．SEQ</param>
        /// <returns></returns>
        T_MANIFEST_PT_KP_NISUGATA[] GetDataForEntity(int SYSTEM_ID, int SEQ);

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

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="SYSTEM_ID">画面．システムID</param>
        /// <param name="SEQ">画面．SEQ</param>
        /// <returns></returns>
        T_MANIFEST_PT_KP_SBN_HOUHOU[] GetDataForEntity(int SYSTEM_ID, int SEQ);
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

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="SYSTEM_ID">画面．システムID</param>
        /// <param name="SEQ">画面．SEQ</param>
        /// <returns></returns>
        T_MANIFEST_PT_DETAIL[] GetDataForEntity(int SYSTEM_ID, int SEQ);
    }
    // 20140529 syunrei No.730 マニフェストパターン一覧 end
    [Bean(typeof(DT_PT_R18))]
    public interface SetDPR18DaoCls : IS2Dao
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
        [NoPersistentProps("SYSTEM_ID"
            , "SEQ"
            , "PATTERN_NAME"
            , "PATTERN_FURIGANA"
            , "MANIFEST_ID"
            , "MANIFEST_KBN"
            , "SHOUNIN_FLAG"
            , "HIKIWATASHI_DATE"
            , "UPN_ENDREP_FLAG"
            , "SBN_ENDREP_FLAG"
            , "LAST_SBN_ENDREP_FLAG"
            , "KAKIN_DATE"
            , "REGI_DATE"
            , "UPN_SBN_REP_LIMIT_DATE"
            , "LAST_SBN_REP_LIMIT_DATE"
            , "RESV_LIMIT_DATE"
            , "SBN_ENDREP_KBN"
            , "HST_SHA_EDI_MEMBER_ID"
            , "HST_SHA_NAME"
            , "HST_SHA_POST"
            , "HST_SHA_ADDRESS1"
            , "HST_SHA_ADDRESS2"
            , "HST_SHA_ADDRESS3"
            , "HST_SHA_ADDRESS4"
            , "HST_SHA_TEL"
            , "HST_SHA_FAX"
            , "HST_JOU_NAME"
            , "HST_JOU_POST_NO"
            , "HST_JOU_ADDRESS1"
            , "HST_JOU_ADDRESS2"
            , "HST_JOU_ADDRESS3"
            , "HST_JOU_ADDRESS4"
            , "HST_JOU_TEL"
            , "REGI_TAN"
            , "HIKIWATASHI_TAN_NAME"
            , "HAIKI_DAI_CODE"
            , "HAIKI_CHU_CODE"
            , "HAIKI_SHO_CODE"
            , "HAIKI_SAI_CODE"
            , "HAIKI_BUNRUI"
            , "HAIKI_SHURUI"
            , "HAIKI_NAME"
            , "HAIKI_SUU"
            , "HAIKI_UNIT_CODE"
            , "SUU_KAKUTEI_CODE"
            , "HAIKI_KAKUTEI_SUU"
            , "HAIKI_KAKUTEI_UNIT_CODE"
            , "NISUGATA_CODE"
            , "NISUGATA_NAME"
            , "NISUGATA_SUU"
            , "SBN_SHA_MEMBER_ID"
            , "SBN_SHA_NAME"
            , "SBN_SHA_POST"
            , "SBN_SHA_ADDRESS1"
            , "SBN_SHA_ADDRESS2"
            , "SBN_SHA_ADDRESS3"
            , "SBN_SHA_ADDRESS4"
            , "SBN_SHA_TEL"
            , "SBN_SHA_FAX"
            , "SBN_SHA_KYOKA_ID"
            , "SAI_SBN_SHA_MEMBER_ID"
            , "SAI_SBN_SHA_NAME"
            , "SAI_SBN_SHA_POST"
            , "SAI_SBN_SHA_ADDRESS1"
            , "SAI_SBN_SHA_ADDRESS2"
            , "SAI_SBN_SHA_ADDRESS3"
            , "SAI_SBN_SHA_ADDRESS4"
            , "SAI_SBN_SHA_TEL"
            , "SAI_SBN_SHA_FAX"
            , "SAI_SBN_SHA_KYOKA_ID"
            , "SBN_WAY_CODE"
            , "SBN_WAY_NAME"
            , "SBN_SHOUNIN_FLAG"
            , "SBN_END_DATE"
            , "HAIKI_IN_DATE"
            , "RECEPT_SUU"
            , "RECEPT_UNIT_CODE"
            , "UPN_TAN_NAME"
            , "CAR_NO"
            , "REP_TAN_NAME"
            , "SBN_TAN_NAME"
            , "SBN_END_REP_DATE"
            , "SBN_REP_BIKOU"
            , "KENGEN_CODE"
            , "LAST_SBN_JOU_KISAI_FLAG"
            , "FIRST_MANIFEST_FLAG"
            , "LAST_SBN_END_DATE"
            , "LAST_SBN_END_REP_DATE"
            , "SHUSEI_DATE"
            , "CANCEL_FLAG"
            , "CANCEL_DATE"
            , "LAST_UPDATE_DATE"
            , "YUUGAI_CNT"
            , "UPN_ROUTE_CNT"
            , "LAST_SBN_PLAN_CNT"
            , "LAST_SBN_CNT"
            , "RENRAKU_CNT"
            , "BIKOU_CNT"
            , "FIRST_MANIFEST_CNT"
            , "HST_GYOUSHA_CD"
            , "HST_GENBA_CD"
            , "SBN_GYOUSHA_CD"
            , "SBN_GENBA_CD"
            , "NO_REP_SBN_EDI_MEMBER_ID"
            , "HAIKI_NAME_CD"
            , "SBN_HOUHOU_CD"
            , "HOUKOKU_TANTOUSHA_CD"
            , "SBN_TANTOUSHA_CD"
            , "UPN_TANTOUSHA_CD"
            , "SHARYOU_CD"
            , "KANSAN_SUU"
            , "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(DT_PT_R18 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(DT_PT_R18 data);

        //155789 S
        [Sql(@" UPDATE DT_PT_R18 
                SET USE_DEFAULT_KBN = /*data.USE_DEFAULT_KBN*/0,
                    UPDATE_USER =/*data.UPDATE_USER*/'',
                    UPDATE_DATE =/*data.UPDATE_DATE*/'',
                    UPDATE_PC =/*data.UPDATE_PC*/'' 
                WHERE SYSTEM_ID = /*data.SYSTEM_ID*/0
                      AND SEQ = /*data.SEQ*/0")]
        int UpdateMod(DT_PT_R18 data);
        //155789 E
    }

}
