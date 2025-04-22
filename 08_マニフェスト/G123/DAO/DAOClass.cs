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

namespace Shougun.Core.PaperManifest.ManifestHimoduke
{

    [Bean(typeof(T_MANIFEST_RELATION))]
    public interface MRLDaoCls : IS2Dao
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
        [NoPersistentProps("NEXT_HAIKI_KBN_CD", "FIRST_SYSTEM_ID", "FIRST_HAIKI_KBN_CD", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
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
        [SqlFile("Shougun.Core.PaperManifest.ManifestHimoduke.Sql.GetFirstManiOnHimodukeSql.sql")]
        DataTable GetDataForEntity(HIMODUKE_DTOCls data);


        /// <summary>
        /// 重複チェック
        /// </summary>
        /// <param name="FIRST_SYSTEM_ID"></param>
        /// <param name="NEXT_SYSTEM_ID"></param>
        /// <returns></returns>
        [Query("DELETE_FLG = 0 /*IF FIRST_SYSTEM_ID != null*/ AND FIRST_HAIKI_KBN_CD = /*FIRST_HAIKI_KBN_CD*/ AND FIRST_SYSTEM_ID IN /*FIRST_SYSTEM_ID*/(1,2) /*END*/"
            + "/*IF !NEXT_SYSTEM_ID.IsNull */ AND NOT (NEXT_SYSTEM_ID = /*NEXT_SYSTEM_ID*/ AND NEXT_HAIKI_KBN_CD = /*NEXT_HAIKI_KBN_CD*/0 ) /*END*/ ")]
        T_MANIFEST_RELATION[] GetDuplications(SqlInt16 FIRST_HAIKI_KBN_CD, List<SqlInt64> FIRST_SYSTEM_ID, SqlInt64 NEXT_SYSTEM_ID, SqlInt16 NEXT_HAIKI_KBN_CD);

    }

    /// <summary>
    /// 紐付テーブルのシーケンスの最大値検出用Dao
    /// </summary>
    [Bean(typeof(T_MANIFEST_RELATION))]
    public interface MAXSeqDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestHimoduke.Sql.GetMaxSeqFromRelationSql.sql")]
        DataTable GetDataForEntity(HIMODUKE_DTOCls data);

    }

    /// <summary>
    /// 電子マニフェスト基本拡張で既存データ判断用Dao
    /// </summary>
    [Bean(typeof(DT_R18_EX))]
    public interface R18_EXDataExistDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestHimoduke.Sql.GetExistDataFromR18_EXSql.sql")]
        DataTable GetDataForEntity(SearchExistDTOCls data);

        DT_R18_EX SelectPK(SqlInt64 SYSTEM_ID,SqlInt32 SEQ);
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
        [SqlFile("Shougun.Core.PaperManifest.ManifestHimoduke.Sql.GetElecDT_R18InfoSql.sql")]
        DataTable GetDataForEntity(SearchExistDTOCls data);

    }

    /// <summary>
    /// 紙マニフェスト存在チェック検索用Dao
    /// </summary>
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface PaperExistDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestHimoduke.Sql.GetPaperInfoExistSql.sql")]
        DataTable GetDataForEntity(SearchExistDTOCls data);

    }
    /// <summary>
    /// 紙マニ検索処理用Dao
    /// </summary>
    [Bean(typeof(T_MANIFEST_RELATION))]
    public interface PaperDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestHimoduke.Sql.GetFirstManifestInPaperSql.sql")]
         DataTable GetDataForEntity(FirstManifestDTOCls data);
    }
    /// <summary>
    /// 電子マニ処理用Dao
    /// </summary>
    [Bean(typeof(DT_R18_EX))]
    public interface ElecDaoCls : IS2Dao
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
            "SHARYOU_CD", "KANSAN_SUU", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
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
        [SqlFile("Shougun.Core.PaperManifest.ManifestHimoduke.Sql.GetFirstManifestInElecSql.sql")]
        DataTable GetDataForEntity(FirstManifestDTOCls data);

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
    /// <summary>
    /// 全て（紙と電子マニ）検索用Dao
    /// </summary>
    [Bean(typeof(T_MANIFEST_RELATION))]
    public interface PaperAndElecDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestHimoduke.Sql.GetFirstPaperAndElecManiSql.sql")]
        DataTable GetDataForEntity(FirstManifestDTOCls data);

        // 20140519 kayo No.734 機能追加 start
        /// <summary>
        /// 二次マニフェストのデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestHimoduke.Sql.GetSecondPaperInfoSql.sql")]
        DataTable GetDataForEntitySecond(SqlInt64 NEXT_SYSTEM_ID);
        // 20140519 kayo No.734 機能追加 end

        /// <summary>
        /// 二次マニフェストのデータを取得(電マニ用)
        /// </summary>
        /// <param name="NEXT_SYSTEM_ID"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestHimoduke.Sql.GetSecondPaperInfoSqlForElecMani.sql")]
        DataTable GetDataForEntitySecondForElecMani(ManiRelrationCallParameter data);

        /// <summary>
        /// 二次マニフェストのデータ（最終処分業者、最終処分場所）を取得する
        /// </summary>
        /// <param name="NEXT_SYSTEM_ID"></param>
        /// <param name="SEQ"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestHimoduke.Sql.GetSecondLastSbnInfo.sql")]
        DataTable GetDataForEntitySecondLastSbn(SqlInt64 NEXT_SYSTEM_ID,SqlInt32 SEQ);

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
        [SqlFile("Shougun.Core.PaperManifest.ManifestHimoduke.Sql.GetLastSbnJyouInfoForPaper.sql")]
        DataTable GetLastSbnJyouInfoForPaper(ManiRelrationCallParameter data);

        /// <summary>
        /// ManiRelrationCallParameterで絞り込んで最終処分情報情報を取得する(電マニ用)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestHimoduke.Sql.GetLastSbnJyouInfoForElec.sql")]
        DataTable GetLastSbnJyouInfoForElec(ManiRelrationCallParameter data);
        
        /// <summary>
        /// KANRI_IDで絞り込んで最終処分情報情報を取得する(電マニ用)
        /// </summary>
        /// <param name="KANRI_ID"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestHimoduke.Sql.GetLastSbnJyouInfoForElecByKanriID.sql")]
        DataTable GetLastSbnJyouInfoForElecByKanriID(string KANRI_ID);

        /// <summary>
        /// SYSTEM_IDより最終処分終了状況の取得
        /// </summary>
        /// <param name="KANRI_ID"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestHimoduke.Sql.GetLastSbnEndFlgForElec.sql")]
        DataTable GetLastSbnEndFlgForElecBySystemID(string SYSTEM_ID);

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
        [SqlFile("Shougun.Core.PaperManifest.ManifestHimoduke.Sql.DenshiHaikiShuruiSearchAndCheckSql.sql")]
        DataTable GetDataForEntity(SearchExistDTOCls data);

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
        [SqlFile("Shougun.Core.PaperManifest.ManifestHimoduke.Sql.DenshiHaikiNameSearchAndCheckSql.sql")]
        DataTable GetDataForEntity(SearchExistDTOCls data);

    }

    // 20140519 kayo No.734 機能追加 start
    /// <summary>
    /// マニフェスト明細相関の検索、更新用Dao
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
    // 20140519 kayo No.734 機能追加 end

}
