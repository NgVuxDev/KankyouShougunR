using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using Shougun.Core.Common.BusinessCommon.Dto;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Common.BusinessCommon.Dao
{
    #region 紙マニ

    /// <summary>
    /// 業者マスタ検索
    /// </summary>
    [Bean(typeof(M_GYOUSHA))]
    public interface CommonGyoushaDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_Gyousha.sql")]
        new DataTable GetDataForEntity(CommonGyoushaDtoCls data);
    }

    /// <summary>
    /// 現場マスタ検索
    /// </summary>
    [Bean(typeof(M_GENBA))]
    public interface CommonGenbaDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_Genba.sql")]
        new DataTable GetDataForEntity(CommonGenbaDtoCls data);
    }

    /// <summary>
    /// 現場マスタ検索(全業者版)
    /// </summary>
    [Bean(typeof(M_GENBA))]
    public interface CommonGenbaAllDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_GenbaAll.sql")]
        new DataTable GetDataForEntity(CommonGenbaDtoCls data);
    }

    /// <summary>
    /// 混合廃棄物マスタ検索
    /// </summary>
    [Bean(typeof(M_KONGOU_HAIKIBUTSU))]
    public interface CommonKongouHaikibutuDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_KongouHaikibutu.sql")]
        new DataTable GetDataForEntity(CommonKongouHaikibutsuDtoCls data);
    }

    /// <summary>
    /// 単位マスタ検索
    /// </summary>
    [Bean(typeof(M_UNIT))]
    public interface CommonUnitDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_Unit.sql")]
        new DataTable GetDataForEntity(CommonUnitDtoCls data);
    }

    /// <summary>
    /// 処分担当者
    /// </summary>
    [Bean(typeof(M_SHOBUN_TANTOUSHA))]
    public interface CommonShobunTantoushaDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_ShobunTantousha.sql")]
        new DataTable GetDataForEntity(CommonShobunTantoushaDtoCls data);
    }

    /// <summary>
    ///  換算値　取得処理
    /// </summary>
    [Bean(typeof(M_HAIKI_SHURUI))]
    public interface CommonKansanDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_KansanData.sql")]
        new DataTable GetDataForEntity(CommonKanSanDtoCls data);
    }

    /// <summary>
    ///  減容値　取得処理
    /// </summary>
    [Bean(typeof(M_HAIKI_SHURUI))]
    public interface CommonGenyouDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_GenyouData.sql")]
        new DataTable GetDataForEntity(CommonKanSanDtoCls data);
    }

    ///// <summary>
    /////  最大値シーケンス取得処理
    ///// </summary>
    //[Bean(typeof(T_MANIFEST_ENTRY))]
    //public interface MaxSeqDaoCls : IS2Dao
    //{
    //    /// <summary>
    //    /// Entityで絞り込んで値を取得する
    //    /// </summary>
    //    [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.ManifestoGetMaxSeq.sql")]
    //    new DataTable GetDataForEntity(SerchParameterDtoCls data);

    //}

    /// <summary>
    /// マニパターン取得
    /// </summary>
    [Bean(typeof(T_MANIFEST_PT_ENTRY))]
    public interface CommonPtEntryDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_PtMani.sql")]
        new DataTable GetDataForEntity(CommonSerchParameterDtoCls data);

        T_MANIFEST_PT_ENTRY SelectPtEntry(SqlInt64 SYSTEM_ID, SqlBoolean DELETE_FLG);

        [Sql("SELECT * FROM T_MANIFEST_PT_UPN WHERE SYSTEM_ID = /*SYSTEM_ID*/ AND SEQ = /*SEQ*/")]
        DataTable SelectPtUpn(SqlInt64 SYSTEM_ID, SqlInt32 SEQ);

        [Sql("SELECT * FROM T_MANIFEST_PT_DETAIL WHERE SYSTEM_ID = /*SYSTEM_ID*/ AND SEQ = /*SEQ*/")]
        DataTable SelectPtDetail(SqlInt64 SYSTEM_ID, SqlInt32 SEQ);
    }

    /// <summary>
    /// マニパターン印字明細取得
    /// </summary>
    [Bean(typeof(T_MANIFEST_PT_DETAIL_PRT))]
    public interface CommonPtDetailPrtDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_PtDetailPrt.sql")]
        new DataTable GetDataForEntity(CommonSerchParameterDtoCls data);
    }

    /// <summary>
    /// マニパターン明細取得
    /// </summary>
    [Bean(typeof(T_MANIFEST_PT_DETAIL))]
    public interface CommonPtDetailDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_PtDetail.sql")]
        new DataTable GetDataForEntity(CommonSerchParameterDtoCls data);
    }

    /// <summary>
    /// マニフェスト更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface CommonEntryDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_ENTRY data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_ENTRY data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_ENTRY data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_Mani.sql")]
        new DataTable GetDataForEntity(CommonSerchParameterDtoCls data);

        /// <summary>
        /// 楽観排他用
        /// </summary>
        /// <param name="data">更新日と更新ユーザーのみ更新</param>
        /// <returns></returns>
        [Sql(" UPDATE T_MANIFEST_ENTRY " +
             " SET UPDATE_USER = /*data.UPDATE_USER*/, UPDATE_DATE = /*data.UPDATE_DATE*/, UPDATE_PC = /*data.UPDATE_PC*/ " +
             " WHERE SYSTEM_ID = /*data.SYSTEM_ID*/ AND SEQ = /*data.SEQ*/ AND TIME_STAMP = /*data.TIME_STAMP*/ ")]
        int UpdateForRelation(T_MANIFEST_ENTRY data);

        /// <summary>
        /// システム明細IDからシステムIDを取得する
        /// </summary>
        /// <param name="DETAIL_SYSTEM_ID"></param>
        /// <returns></returns>
        [Sql("SELECT SYSTEM_ID FROM T_MANIFEST_DETAIL WHERE DETAIL_SYSTEM_ID = /*DETAIL_SYSTEM_ID*/0 ")]
        SqlInt64 GetSystemIdByDetailSystemId(SqlInt64 DETAIL_SYSTEM_ID);

        /// <summary>
        /// 1次更新対象に紐付があるかチェック
        /// </summary>
        /// <param name="SYSTEM_ID"></param>
        /// <returns></returns>
        [Sql(@" SELECT TMR.NEXT_SYSTEM_ID
                FROM
                    T_MANIFEST_ENTRY    TME
                    INNER JOIN T_MANIFEST_DETAIL   TMD ON TME.SYSTEM_ID = TMD.SYSTEM_ID AND TME.SEQ = TMD.SEQ
                    INNER JOIN T_MANIFEST_RELATION TMR ON TMR.DELETE_FLG = 0 AND TMR.FIRST_SYSTEM_ID = TMD.DETAIL_SYSTEM_ID AND TMR.FIRST_HAIKI_KBN_CD <> 4
                WHERE TME.DELETE_FLG = 0 AND TME.SYSTEM_ID = /*SYSTEM_ID*/0 ")]
        // 20140620 kayo 不具合#4926　紐付いたマニフェストが削除されたら、データの不整合に生じる start
        DataTable GetRelationF(SqlInt64 SYSTEM_ID);

        /// <summary>
        /// 2次更新対象に紐付があるかチェック
        /// </summary>
        /// <param name="SYSTEM_ID"></param>
        /// <returns></returns>
//        [Sql(@" SELECT TMR.FIRST_SYSTEM_ID
//                FROM
//                    T_MANIFEST_RELATION    TMR
//                    INNER JOIN T_MANIFEST_ENTRY TME ON TMR.NEXT_SYSTEM_ID = TME.SYSTEM_ID AND TME.DELETE_FLG = 0
//                WHERE TMR.DELETE_FLG = 0 AND TMR.NEXT_HAIKI_KBN_CD <> 4 AND TMR.NEXT_SYSTEM_ID = /*SYSTEM_ID*/0 ")]
        [Sql(@" SELECT TMR.FIRST_SYSTEM_ID
                FROM
                    T_MANIFEST_ENTRY    TME
                    INNER JOIN T_MANIFEST_DETAIL   TMD ON TME.SYSTEM_ID = TMD.SYSTEM_ID AND TME.SEQ = TMD.SEQ
                    INNER JOIN T_MANIFEST_RELATION TMR ON TMR.DELETE_FLG = 0 AND TMR.NEXT_SYSTEM_ID = TMD.DETAIL_SYSTEM_ID AND TMR.FIRST_HAIKI_KBN_CD <> 4
                WHERE TME.DELETE_FLG = 0 AND TME.SYSTEM_ID = /*SYSTEM_ID*/0 ")]
        DataTable GetRelationS(SqlInt64 SYSTEM_ID);

        /// <summary>
        /// 1次Detail更新対象に紐付があるかチェック
        /// </summary>
        /// <param name="FIRST_SYSTEM_ID"></param>
        /// <returns></returns>
        [Sql(@" SELECT
                    TMR.NEXT_SYSTEM_ID
                FROM
                    T_MANIFEST_RELATION TMR
                WHERE
                    TMR.DELETE_FLG = 0 AND TMR.FIRST_SYSTEM_ID = /*FIRST_SYSTEM_ID*/0 AND TMR.FIRST_HAIKI_KBN_CD <> 4 ")]
        DataTable GetRelationD(SqlInt64 FIRST_SYSTEM_ID);

        // 20140620 kayo 不具合#4926　紐付いたマニフェストが削除されたら、データの不整合に生じる end

        // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
        /// <summary>
        /// 2次Detail更新対象に紐付があるかチェック
        /// </summary>
        /// <param name="FIRST_SYSTEM_ID"></param>
        /// <returns></returns>
        [Sql(@" SELECT
                    TMR.NEXT_SYSTEM_ID
                FROM
                    T_MANIFEST_RELATION    TMR 
                WHERE
                    TMR.DELETE_FLG = 0 AND TMR.NEXT_SYSTEM_ID = /*SECOND_SYSTEM_ID*/0 AND TMR.NEXT_HAIKI_KBN_CD <> 4 ")]
        DataTable GetRelationD2(SqlInt64 SECOND_SYSTEM_ID);
        // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end

        /// <summary>
        /// Entityで絞り込んで値を取得する(DT_R18_EX.MANIFEST_ID専用)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("MANIFEST_ID = /*MANIFEST_ID*/ AND DELETE_FLG = 0")]
        T_MANIFEST_ENTRY GetDataForManiId(string MANIFEST_ID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        DataTable GetDataForStringSql(string sql);
    }

    /// <summary>
    /// マニ収集運搬更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_UPN))]
    public interface CommonUpnDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_UPN data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_UPN data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_UPN data);
    }

    /// <summary>
    /// マニ印字更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_PRT))]
    public interface CommonPrtDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_PRT data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_PRT data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_PRT data);
    }

    /// <summary>
    /// マニ印字明細更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_DETAIL_PRT))]
    public interface CommonDetailPrtDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_DETAIL_PRT data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_DETAIL_PRT data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_DETAIL_PRT data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_DetailPrt.sql")]
        new DataTable GetDataForEntity(CommonSerchParameterDtoCls data);

        /// <summary>
        /// データを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ AND SEQ = /*data.SEQ*/")]
        List<T_MANIFEST_DETAIL_PRT> GetDataListByPrimaryKey(T_MANIFEST_DETAIL_PRT data);
    }

    /// <summary>
    /// マニ印字_産廃_形状更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_KP_KEIJYOU))]
    public interface CommonKeijyouDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_KP_KEIJYOU data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_KP_KEIJYOU data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_KP_KEIJYOU data);
    }

    /// <summary>
    /// マニ印字_産廃_荷姿更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_KP_NISUGATA))]
    public interface CommonNisugataDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_KP_NISUGATA data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_KP_NISUGATA data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_KP_NISUGATA data);
    }

    /// <summary>
    /// マニ印字_産廃_処分方法更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_KP_SBN_HOUHOU))]
    public interface CommonHouhouDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_KP_SBN_HOUHOU data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_KP_SBN_HOUHOU data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_KP_SBN_HOUHOU data);
    }

    /// <summary>
    /// マニ明細更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_DETAIL))]
    public interface CommonDetailDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_DETAIL data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_DETAIL data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_DETAIL data);

        /// <summary>
        /// データを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ AND SEQ = /*data.SEQ*/")]
        List<T_MANIFEST_DETAIL> GetDataListByPrimaryKey(T_MANIFEST_DETAIL data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_Detail.sql")]
        new DataTable GetDataForEntity(CommonSerchParameterDtoCls data);

        // 20140708 ria EV005141 2次マニを修正モードにて登録しても紐づいた1次マニが更新されない start

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_MaxDetailSyobunDate.sql")]
        new DataTable GetSyobunDate(CommonSerchParameterDtoCls data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_GetDetailSyobunData.sql")]
        new DataTable GetDetailSyobunData(CommonSerchParameterDtoCls data);

        /// <summary>
        /// 2次マニの最終処分業者、最終処分場所の再取得
        /// </summary>
        /// <param name="NEXT_SYSTEM_ID"></param>
        /// <param name="SEQ"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_GetSecondLastSbnInfo.sql")]
        DataTable GetDetailSecondLastSbn(SqlInt64 NEXT_SYSTEM_ID, SqlInt32 SEQ);

        /// <summary>
        /// 楽観排他用
        /// </summary>
        /// <param name="data">二次の最終処分値は一次に更新</param>
        /// <returns></returns>
        [Sql(" UPDATE T_MANIFEST_DETAIL " +
             " SET LAST_SBN_END_DATE = /*data.LAST_SBN_END_DATE*/, LAST_SBN_GYOUSHA_CD = /*data.LAST_SBN_GYOUSHA_CD*/, LAST_SBN_GENBA_CD = /*data.LAST_SBN_GENBA_CD*/ " +
             " WHERE DETAIL_SYSTEM_ID = /*data.DETAIL_SYSTEM_ID*/ AND SEQ = /*data.SEQ*/")]
        int UpdateFirstDetail(T_MANIFEST_DETAIL data);

        // 20140708 ria EV005141 2次マニを修正モードにて登録しても紐づいた1次マニが更新されない end
    }

    /// <summary>
    /// マニ返却日更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_RET_DATE))]
    public interface CommonRetDateDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_RET_DATE data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_RET_DATE data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_RET_DATE data);
    }

    /// <summary>
    /// マニフェスト削除用
    /// </summary>
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface CommonEntryDelDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_ENTRY data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("HAIKI_KBN_CD", "FIRST_MANIFEST_KBN", "KYOTEN_CD", "TORIHIKISAKI_CD", "JIZEN_NUMBER"
                            , "JIZEN_DATE", "KOUFU_DATE", "KOUFU_KBN", "MANIFEST_ID", "SEIRI_ID", "KOUFU_TANTOUSHA", "KOUFU_TANTOUSHA_SHOZOKU", "HST_GYOUSHA_CD", "HST_GYOUSHA_NAME"
                            , "HST_GYOUSHA_POST", "HST_GYOUSHA_TEL", "HST_GYOUSHA_ADDRESS", "HST_GENBA_CD", "HST_GENBA_NAME", "HST_GENBA_POST", "HST_GENBA_TEL"
                            , "HST_GENBA_ADDRESS", "BIKOU", "KONGOU_SHURUI_CD", "HAIKI_SUU", "HAIKI_UNIT_CD", "TOTAL_SUU", "TOTAL_KANSAN_SUU", "TOTAL_GENNYOU_SUU"
                            , "CHUUKAN_HAIKI_KBN", "CHUUKAN_HAIKI", "LAST_SBN_YOTEI_KBN", "LAST_SBN_YOTEI_GYOUSHA_CD", "LAST_SBN_YOTEI_GENBA_CD", "LAST_SBN_YOTEI_GENBA_NAME"
                            , "LAST_SBN_YOTEI_GENBA_POST", "LAST_SBN_YOTEI_GENBA_TEL", "LAST_SBN_YOTEI_GENBA_ADDRESS", "SBN_GYOUSHA_CD", "SBN_GYOUSHA_NAME"
                            , "SBN_GYOUSHA_POST", "SBN_GYOUSHA_TEL", "SBN_GYOUSHA_ADDRESS", "TMH_GYOUSHA_CD", "TMH_GYOUSHA_NAME", "TMH_GENBA_CD", "TMH_GENBA_NAME"
                            , "TMH_GENBA_POST", "TMH_GENBA_TEL", "TMH_GENBA_ADDRESS", "YUUKA_KBN", "YUUKA_SUU", "YUUKA_UNIT_CD", "SBN_JYURYOUSHA_CD", "SBN_JYURYOUSHA_NAME", "SBN_JYURYOU_TANTOU_CD"
                            , "SBN_JYURYOU_TANTOU_NAME", "SBN_JYURYOU_DATE", "SBN_JYUTAKUSHA_CD", "SBN_JYUTAKUSHA_NAME", "SBN_TANTOU_CD", "SBN_TANTOU_NAME", "LAST_SBN_GYOUSHA_CD"
                            , "LAST_SBN_GENBA_CD", "LAST_SBN_GENBA_NAME", "LAST_SBN_GENBA_POST", "LAST_SBN_GENBA_TEL", "LAST_SBN_GENBA_ADDRESS", "LAST_SBN_GENBA_NUMBER", "LAST_SBN_CHECK_NAME"
                            , "CHECK_B1", "CHECK_B2", "CHECK_B4", "CHECK_B6", "CHECK_D", "CHECK_E", "RENKEI_DENSHU_KBN_CD", "RENKEI_SYSTEM_ID", "RENKEI_MEISAI_SYSTEM_ID"
                            , "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC"
                            , "TIME_STAMP")]
        int Update(T_MANIFEST_ENTRY data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_ENTRY data);

        /// <summary>
        /// データを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ AND SEQ = /*data.SEQ*/")]
        T_MANIFEST_ENTRY GetDataByPrimaryKey(T_MANIFEST_ENTRY data);
    }

    /// <summary>
    /// マニ返却日(T_MANIFEST_RET_DATE)更新用
    /// </summary>
    [Bean(typeof(T_MANIFEST_RET_DATE))]
    public interface CommonRetDateDelDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_RET_DATE data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("SEND_A", "SEND_B1", "SEND_B2", "SEND_B4", "SEND_B6", "SEND_C1",
            "SEND_C2", "SEND_D", "SEND_E", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_RET_DATE data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_RET_DATE data);

        /// <summary>
        /// データを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ AND SEQ = /*data.SEQ*/")]
        T_MANIFEST_RET_DATE GetDataByPrimaryKey(T_MANIFEST_RET_DATE data);
    }

    #endregion

    #region マニフェスト紐付更新用
    /// <summary>
    /// マニフェスト紐付更新用
    /// </summary>
    [Bean(typeof(T_MANIFEST_RELATION))]
    public interface CommonManifestRelationDaoCls : IS2Dao
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
        [NoPersistentProps("NEXT_HAIKI_KBN_CD", "FIRST_SYSTEM_ID", "FIRST_HAIKI_KBN_CD", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_RELATION data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_MANIFEST_RELATION data);
    }
    #endregion

    #region マニフェスト紐付検索

    /// <summary>
    /// マニフェスト紐付対象検索
    /// </summary>
    [Bean(typeof(T_MANIFEST_RELATION))]
    public interface GetManifestRelationDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetManifestRelation.sql")]
        new DataTable GetDataForEntity(GetManifestRelationDtoCls data);

        /// <summary>
        /// Entityで絞り込んで有効な紐付情報を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetAllValidManifestRelation.sql")]
        T_MANIFEST_RELATION[] GetAllValidData(T_MANIFEST_RELATION data);

        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetAllValidManifestRelationMinCols.sql")]
        T_MANIFEST_RELATION[] GetAllValidDataMinCols(T_MANIFEST_RELATION data);

        /// <summary>
        /// 一次電マニ(最終処分終了報告済み)の情報を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetManiRelationForFixedEndRepElec.sql")]
        DataTable GetManiRelationForFixedEndRepElec(SqlInt64 nextSysId, int nextHaikiKbnCd);

        /// <summary>
        /// 紐付いた一次マニ(紙、電子共に)の情報を取得する
        /// </summary>
        /// <param name="SYSTEM_ID"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_GetFirstManiInfo.sql")]
        DataTable GetFirstManiInfo(SqlInt64 SYSTEM_ID, SqlInt16 HAIKI_KBN_CD);

        /// <summary>
        /// 紐付いた二次マニ(紙、電子共に)の最終処分情報を取得する
        /// </summary>
        /// <param name="SYSTEM_ID"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_GetLastSbnInfoForNextMani.sql")]
        DataTable GetLastSbnInfoForNexttMani(SqlInt64 SYSTEM_ID);

        /// <summary>
        /// DT_R18_MIX.SBN_ENDREP_KBN=2のデータを取得する(最終処分終了報告用)
        /// <param name="data">KANRI_ID</param>
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetMixManifestForLastSbnData.sql")]
        DataTable GetMixManifestForLastSbnData(List<string> data);
    }

    #endregion

    #region マニフェスト紐付検索(取り消し用)

    /// <summary>
    /// マニフェスト紐付対象(取り消し)検索
    /// </summary>
    [Bean(typeof(T_MANIFEST_RELATION))]
    public interface GetManifestRelationCancelDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetManifestRelationCancel.sql")]
        new DataTable GetDataForEntity(GetManifestRelationDtoCls data);
    }

    #endregion

    #region 電マニ

    #region QUE_INFO

    /// <summary>
    /// キュー情報[QUE_INFO]用DAO
    /// </summary>
    [Bean(typeof(QUE_INFO))]
    public interface CommonQueInfoDaoCls : IS2Dao
    {
        /// <summary>
        /// QUE_INFOから最終処分終了報告、最終処分終了取消の実行中キューを取得
        /// FUNCTION_ID = 2000(最終処分終了報告), 2100(最終処分終了取消)
        /// かつ、STATUS_FLAG = 0(送信前), 1(送信中), 7(送信保留)
        /// </summary>
        /// <param name="data">管理番号</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.GetQue_SeqInfoForLastSbnEndRepFunction.sql")]
        DataTable GetQue_SeqInfoForLastSbnEndRepFunction(QUE_INFO data);

        /// <summary>
        /// TOCのAPPROVAL_SEQの情報が、
        /// QUE_INFOのSTATUS_FLAG = 6(保留削除), 9(エラー)かどうか確認する
        /// </summary>
        /// <param name="kanriid">管理番号</param>
        /// <param name="approvalSeq">承認待ちSEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.GetQue_SeqInfoForHoryuDel.sql")]
        DataTable GetQue_SeqInfoForHoryuDel(string kanriid, string approvalSeq);
    }

    #endregion

    #region DT_MF_TOC

    /// <summary>
    /// マニフェスト目次情報[DT_MF_TOC]用Dao
    /// </summary>
    [Bean(typeof(DT_MF_TOC))]
    public interface CommonDT_MF_TOCDaoCls : IS2Dao
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("KANRI_ID", "MANIFEST_ID", "APPROVAL_SEQ", "STATUS_FLAG",
                           "STATUS_DETAIL", "READ_FLAG", "CREATE_DATE", "UPDATE_TS,", "KIND")]
        int Update(DT_MF_TOC data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("KANRI_ID = /*data.KANRI_ID*/")]
        DT_MF_TOC GetDataForEntity(DT_MF_TOC data);
    }

    #endregion

    #region DT_R18

    /// <summary>
    /// 電子マニフェストテーブル登録更新削除用Dao
    /// </summary>
    [Bean(typeof(DT_R18))]
    public interface CommonDT_R18DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_R18 data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("KANRI_ID = /*data.KANRI_ID*/ AND SEQ = /*data.SEQ*/")]
        DT_R18 GetDataForEntity(DT_R18 data);
    }

    #endregion

    #region DT_R19

    /// <summary>
    /// 収集運搬情報[DT_R19]用Dao
    /// </summary>
    [Bean(typeof(DT_R19))]
    public interface CommonDT_R19DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_R19 data);

        /// <summary>
        /// 運搬情報取得処理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("KANRI_ID = /*data.KANRI_ID*/ AND SEQ = /*data.SEQ*/")]
        DT_R19[] GetDataForEntity(DT_R19 data);
    }

    #endregion

    #region DT_R02

    /// <summary>
    ///有害物質情報[DT_R02]用Dao
    /// </summary>
    [Bean(typeof(DT_R02))]
    public interface CommonDT_R02DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_R02 data);

        /// <summary>
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("KANRI_ID = /*data.KANRI_ID*/ AND SEQ = /*data.SEQ*/")]
        DT_R02[] GetDataForEntity(DT_R02 data);
    }

    #endregion

    #region DT_R04

    /// <summary>
    ///最終処分事業場(予定)情報[DT_R04]用DAO
    /// </summary>
    [Bean(typeof(DT_R04))]
    public interface CommonDT_R04DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_R04 data);

        /// <summary>
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("KANRI_ID = /*data.KANRI_ID*/ AND SEQ = /*data.SEQ*/")]
        DT_R04[] GetDataForEntity(DT_R04 data);
    }

    #endregion

    #region DT_R05

    /// <summary>
    ///連絡番号情報[DT_R05]用DAO
    /// </summary>
    [Bean(typeof(DT_R05))]
    public interface CommonDT_R05DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_R05 data);

        /// <summary>
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("KANRI_ID = /*data.KANRI_ID*/ AND SEQ = /*data.SEQ*/")]
        DT_R05[] GetDataForEntity(DT_R05 data);
    }

    #endregion

    #region DT_R06

    /// <summary>
    ///備考情報[DT_R06]用DAO
    /// </summary>
    [Bean(typeof(DT_R06))]
    public interface CommonDT_R06DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_R06 data);

        /// <summary>
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("KANRI_ID = /*data.KANRI_ID*/ AND SEQ = /*data.SEQ*/")]
        DT_R06[] GetDataForEntity(DT_R06 data);
    }

    #endregion

    #region DT_R08

    /// <summary>
    /// 1次マニフェスト情報
    /// </summary>
    [Bean(typeof(DT_R08))]
    public interface CommonDT_R08DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_R08 data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("KANRI_ID = /*data.KANRI_ID*/ AND SEQ = /*data.SEQ*/")]
        DT_R08[] GetDataForEntity(DT_R08 data);
    }

    #endregion

    #region DT_R13

    /// <summary>
    ///最終処分終了日・事業場情報[DT_R13]用DAO
    /// </summary>
    [Bean(typeof(DT_R13))]
    public interface CommonDT_R13DaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_R13 data);

        /// <summary>
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("KANRI_ID = /*data.KANRI_ID*/ AND SEQ = /*data.SEQ*/")]
        DT_R13[] GetDataForEntity(DT_R13 data);
    }

    #endregion

    #region DT_R18_EX

    /// <summary>
    /// 電子マニフェスト基本拡張テーブル[DT_R18_EX]登録更新削除用Dao
    /// </summary>
    [Bean(typeof(DT_R18_EX))]
    public interface CommonDT_R18_EXDaoCls : IS2Dao
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
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ AND SEQ = /*data.SEQ*/ AND DELETE_FLG = 0")]
        DT_R18_EX GetDataForEntity(DT_R18_EX data);

        /// <summary>
        /// SYSTEM_IDで絞り込んでDT_R18_EXを取得する(DT_R18_MIXが存在すれば、DT_R18_MIXのDETAIL_SYSTEM_IDで絞り込む)
        /// </summary>
        /// <param name="SYSTEM_ID"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Manifest_GetR18EX_ForSystemId.sql")]
        DT_R18_EX GetDataForSystemId(SqlInt64 SYSTEM_ID);

        /// <summary>
        /// Entityで絞り込んで値を取得する(DT_R18_EX.SYSTEM_ID専用)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*SYSTEM_ID*/ AND DELETE_FLG = 0")]
        DT_R18_EX GetDataForExSystemId(SqlInt64 SYSTEM_ID);

        /// <summary>
        /// Entityで絞り込んで値を取得する(DT_R18_EX.MANIFEST_ID専用)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("MANIFEST_ID = /*MANIFEST_ID*/ AND DELETE_FLG = 0")]
        DT_R18_EX GetDataForExManiId(string MANIFEST_ID);

        /// <summary>
        /// 楽観排他用
        /// </summary>
        /// <param name="data">更新日と更新ユーザーのみ更新</param>
        /// <returns></returns>
        [Sql(" UPDATE DT_R18_EX " +
             " SET UPDATE_USER = /*data.UPDATE_USER*/ , UPDATE_DATE = /*data.UPDATE_DATE*/ , UPDATE_PC = /*data.UPDATE_PC*/ " +
             " WHERE SYSTEM_ID = /*data.SYSTEM_ID*/ AND SEQ = /*data.SEQ*/ AND TIME_STAMP = /*data.TIME_STAMP*/ ")]
        int UpdateForRelation(DT_R18_EX data);
    }

    #endregion

    #region DT_R19_EX

    /// <summary>
    /// 電子マニフェスト収集運搬拡張[DT_R19_EX]登録更新削除用Dao
    /// </summary>
    [Bean(typeof(DT_R19_EX))]
    public interface CommonDT_R19_EXDaoCls : IS2Dao
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
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ AND SEQ = /*data.SEQ*/ AND DELETE_FLG = 0")]
        DT_R19_EX[] GetDataForEntity(DT_R19_EX data);
    }

    #endregion

    #region DT_R04_EX

    /// <summary>
    /// 電子マニフェスト最終処分（予定）拡張[DT_R04_EX]登録更新削除用Dao
    /// </summary>
    [Bean(typeof(DT_R04_EX))]
    public interface CommonDT_R04_EXDaoCls : IS2Dao
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
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ AND SEQ = /*data.SEQ*/ AND DELETE_FLG = 0")]
        DT_R04_EX[] GetDataForEntity(DT_R04_EX data);
    }

    #endregion

    #region DT_R08_EX

    /// <summary>
    /// 一次マニフェスト情報拡張
    /// </summary>
    [Bean(typeof(DT_R08_EX))]
    public interface CommonDT_R08_EXDaoCls : IS2Dao
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
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ AND SEQ = /*data.SEQ*/ AND DELETE_FLG = 0")]
        DT_R08_EX[] GetDataForEntity(DT_R08_EX data);
    }

    #endregion

    #region DT_R13_EX

    /// <summary>
    /// 電子マニフェスト最終処分拡張[DT_R13_EX]登録更新削除用Dao
    /// </summary>
    [Bean(typeof(DT_R13_EX))]
    public interface CommonDT_R13_EXDaoCls : IS2Dao
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
        /// Entityで絞り込んで全てデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ AND SEQ = /*data.SEQ*/ AND DELETE_FLG = 0")]
        DT_R13_EX[] GetDataForEntity(DT_R13_EX data);
    }

    #endregion

    #endregion
}