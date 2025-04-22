using System.Data;
using System.Data.SqlTypes;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using Shougun.Core.Common.BusinessCommon.Dto;
using System;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PaperManifest.ManifestImport.DAO
{
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface ManifestEntryDaoClass : IS2Dao
    {
        [SqlFile("Shougun.Core.PaperManifest.ManifestImport.Sql.GetManifestRelationDataByNextSystemId.sql")]
        T_MANIFEST_ENTRY[] GetManifestRelationDataByNextSystemId(T_MANIFEST_ENTRY data);

        [SqlFile("Shougun.Core.PaperManifest.ManifestImport.Sql.GetManifestRelationDataByFirstSystemId.sql")]
        T_MANIFEST_ENTRY[] GetManifestRelationDataByFirstSystemId(T_MANIFEST_ENTRY data, string detailSystemId);

        [SqlFile("Shougun.Core.PaperManifest.ManifestImport.Sql.GetKamiManifestData.sql")]
        T_MANIFEST_ENTRY[] GetKamiManifestData(T_MANIFEST_ENTRY data);

        /// <summary>
        /// データを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ AND SEQ = /*data.SEQ*/")]
        T_MANIFEST_ENTRY GetDataByPrimaryKey(T_MANIFEST_ENTRY data);

        /// <summary>
        /// マニ伝票作成データを取得
        /// </summary>
        /// <param name="createFrom">作成日期間（From）</param>
        /// <param name="createTo">作成日期間（To）</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestImport.Sql.GetManifestCountByData.sql")]
        T_MANIFEST_ENTRY[] GetManifestCountByData(string createFrom, string createTo);
    }

    [Bean(typeof(T_MANIFEST_UPN))]
    public interface ManifestUpnDaoClass : IS2Dao
    {
        /// <summary>
        /// データを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ AND SEQ = /*data.SEQ*/ AND UPN_ROUTE_NO = /*data.UPN_ROUTE_NO*/")]
        T_MANIFEST_UPN GetDataByPrimaryKey(T_MANIFEST_UPN data);
    }

    /// <summary>
    /// マニ返却日(T_MANIFEST_RET_DATE)更新用
    /// </summary>
    [Bean(typeof(T_MANIFEST_RET_DATE))]
    public interface T_MANIFEST_RELATIONDaoCls : IS2Dao
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

        /// <summary>
        /// データを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ AND SEQ = /*data.SEQ*/")]
        T_MANIFEST_RET_DATE GetDataByPrimaryKey(T_MANIFEST_RET_DATE data);
    }

    /// <summary>
    /// マニ印字_建廃_形状更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_KP_KEIJYOU))]
    public interface KeijyouDaoCls : IS2Dao
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

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestImport.Sql.SerchKeijyou.sql")]
        DataTable GetDataForEntity(CommonSerchParameterDtoCls data);
    }

    /// <summary>
    /// マニ印字_建廃_荷姿更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_KP_NISUGATA))]
    public interface NisugataDaoCls : IS2Dao
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

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestImport.Sql.SerchNisugata.sql")]
        DataTable GetDataForEntity(CommonSerchParameterDtoCls data);
    }

    [Bean(typeof(T_MANIFEST_KP_SBN_HOUHOU))]
    public interface SbnHouhouDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestImport.Sql.SerchSbnHouhou.sql")]
        DataTable GetDataForEntity(CommonSerchParameterDtoCls data);
    }



    /// <summary>
    /// マニ明細(T_MANIFEST_DETAIL)
    /// </summary>
    [Bean(typeof(T_MANIFEST_DETAIL))]
    public interface ManifestDetailDaoClass : IS2Dao
    {
        [SqlFile("Shougun.Core.PaperManifest.ManifestImport.Sql.GetKamiManifestData.sql")]
        DataTable GetKamiManifestData(T_MANIFEST_ENTRY data, string HAIKI_SHURUI_CD, string HAIKI_NAME_CD);

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

    /// <summary>
    /// 電マニ(DT_R18)
    /// </summary>
    [Bean(typeof(DT_R18))]
    public interface DenManiDaoClass : IS2Dao
    {
        [SqlFile("Shougun.Core.PaperManifest.ManifestImport.Sql.GetDenshiManifestData.sql")]
        DataTable GetDenshiManifestData(DT_R18 data, string HAIKI_SHURUI_CD, string HAIKI_NAME_CD, bool firstFlg);
    }

    /// <summary>
    /// 電マニEX(DT_R18_EX)
    /// </summary>
    [Bean(typeof(DT_R18_EX))]
    public interface DenManiRelationDaoClass : IS2Dao
    {
        [SqlFile("Shougun.Core.PaperManifest.ManifestImport.Sql.GetManiRelationDataForDenshiByFirst.sql")]
        DT_R18_EX[] GetManiRelationDataForDenshiByFirst(string manifestId, string detailSystemId);

        [SqlFile("Shougun.Core.PaperManifest.ManifestImport.Sql.GetManiRelationDataForDenshiByNext.sql")]
        DT_R18_EX[] GetManiRelationDataForDenshiByNext(string manifestId);
    }

    /// <summary>
    /// 全て（紙と電子マニ）検索用Dao
    /// </summary>
    [Bean(typeof(T_MANIFEST_RELATION))]
    public interface PaperAndElecDaoCls : IS2Dao
    {
        /// <summary>
        /// マニ紐付作成データを取得
        /// </summary>
        /// <param name="IsFirstRelation">一次/二次フラグtrue:一次false:二次</param>
        /// <param name="createFrom">作成日期間（From）</param>
        /// <param name="createTo">作成日期間（To）</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestImport.Sql.GetManiHimodukeConutByData.sql")]
        DataTable GetManiHimodukeConutByData(bool IsFirstRelation, string createFrom, string createTo);

        /// <summary>
        /// 二次マニフェストのデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestImport.Sql.GetSecondPaperInfoSql.sql")]
        DataTable GetDataForEntitySecond(SqlInt64 NEXT_SYSTEM_ID);

        /// <summary>
        /// 二次マニフェストのデータを取得(電マニ用)
        /// </summary>
        /// <param name="NEXT_SYSTEM_ID"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestImport.Sql.GetSecondPaperInfoSqlForElecMani.sql")]
        DataTable GetDataForEntitySecondForElecMani(string SECOND_KANRI_ID);

        /// <summary>
        /// 二次マニフェストのデータ（最終処分業者、最終処分場所）を取得する
        /// </summary>
        /// <param name="NEXT_SYSTEM_ID"></param>
        /// <param name="SEQ"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestHimoduke.Sql.GetSecondLastSbnInfo.sql")]
        DataTable GetDataForEntitySecondLastSbn(SqlInt64 NEXT_SYSTEM_ID, SqlInt32 SEQ);

        /// <summary>
        /// 二次マニフェストのデータ（最終処分業者、最終処分場所）を取得(電マニ用)
        /// </summary>
        /// <param name="NEXT_SYSTEM_ID"></param>
        /// <param name="SEQ"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestHimoduke.Sql.GetSecondLastSbnInfoForElecMani.sql")]
        DataTable GetDataForEntitySecondLastSbnForElecMani(SqlInt64 NEXT_SYSTEM_ID, SqlInt32 SEQ);

        /// <summary>
        /// 最新紐付情報を取得
        /// </summary>
        /// <param name="NEXT_SYSTEM_ID">2次のシステムID</param>
        /// <param name="DELETE_FLG">false固定</param>
        /// <returns></returns>
        T_MANIFEST_RELATION[] SelectCurrent(SqlInt64 NEXT_SYSTEM_ID, SqlInt16 NEXT_HAIKI_KBN_CD, SqlBoolean DELETE_FLG);

        /// <summary>
        /// ManiRelrationCallParameterで絞り込んで最終処分情報情報を取得する(紙マニ用)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestImport.Sql.GetLastSbnJyouInfoForPaper.sql")]
        DataTable GetLastSbnJyouInfoForPaper(string systemId);

        /// <summary>
        /// ManiRelrationCallParameterで絞り込んで最終処分情報情報を取得する(電マニ用)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestImport.Sql.GetLastSbnJyouInfoForElec.sql")]
        DataTable GetLastSbnJyouInfoForElec(string kanriId);

        /// <summary>
        /// 紐付条件チェック(紙マニ用)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestImport.Sql.CheckHimodukeForPaper.sql")]
        DataTable CheckHimodukeForPaper(string systemId);

        /// <summary>
        /// 紐付条件チェック(電マニ用)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestImport.Sql.CheckHimodukeForElec.sql")]
        DataTable CheckHimodukeForElec(string kanriId);
    }
}
