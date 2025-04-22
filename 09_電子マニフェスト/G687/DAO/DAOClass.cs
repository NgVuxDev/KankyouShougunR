using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Collections.Generic;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ElectronicManifest.DenmaniSaishuShobun.DAO
{

    [Bean(typeof(M_OUTPUT_PATTERN))]
    public interface GetTMEDaoCls : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable getdateforstringsql(string sql);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.ElectronicManifest.DenmaniSaishuShobun.Sql.GetManifestRelation.sql")]
        new DataTable GetManifestRelation(TMEDtoCls data);

        /// <summary>
        /// DT_R18_MIX.SBN_ENDREP_KBN=2のデータを取得する(最終処分終了報告用)
        /// <param name="data">KANRI_ID</param>
        /// </summary>
        [SqlFile("Shougun.Core.ElectronicManifest.DenmaniSaishuShobun.Sql.GetMixManifestForLastSbnData.sql")]
        DataTable GetMixManifestForLastSbnData(List<string> data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.ElectronicManifest.DenmaniSaishuShobun.Sql.GetManifestRelationCancel.sql")]
        new DataTable GetManifestRelationCancel(TMEDtoCls data);
        
        /// <summary>
        /// DT_R18からデータを取得
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenmaniSaishuShobun.Sql.GetElecManiData.sql")]
        new DataTable GetElecManiData(List<string> data);

        /// <summary>
        /// DT_R18からデータを取得
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenmaniSaishuShobun.Sql.GetElecManiDataNasi.sql")]
        new DataTable GetElecManiDataNasi(string str);
    }

    [Bean(typeof(M_DENSHI_HAIKI_SHURUI))]
    public interface DAOClass_PopupHaiki : IS2Dao
    {
        /// <summary>
        /// 廃棄物種類POPUP画面のデータソースを取得
        /// </summary>
        /// <param name=""></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenmaniSaishuShobun.Sql.GetPopupHaiki.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_DENSHI_HAIKI_SHURUI))]
    public interface DAOClass_CheckHaiki : IS2Dao
    {
        /// <summary>
        /// 廃棄物種類をチェックするSQL文
        /// </summary>
        /// <param name="HAIKI_KBN_CD"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenmaniSaishuShobun.Sql.CheckHaiki.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_DENSHI_HAIKI_NAME))]
    public interface DAOClass_PopupHaikiName : IS2Dao
    {
        /// <summary>
        /// 廃棄物名称POPUP画面のデータソースを取得
        /// </summary>
        /// <param name=""></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenmaniSaishuShobun.Sql.GetPopupHaikiName.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_DENSHI_HAIKI_NAME))]
    public interface DAOClass_CheckHaikiName : IS2Dao
    {
        /// <summary>
        /// 廃棄物名称をチェックするSQL文
        /// </summary>
        /// <param name="HAIKI_NAME_CD"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenmaniSaishuShobun.Sql.CheckHaikiName.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(QUE_INFO))]
    public interface GetQueDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS", "TIME_STAMP")]
        int Insert(QUE_INFO data);

        /// <summary>
        /// キュー情報レコード最大枝番検索
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenmaniSaishuShobun.Sql.GetQueInfoMaxSeq.sql")]
        new DataTable GetMaxSeq(GetMaxSeqDtoCls data);
    }

    [Bean(typeof(DT_D12))]
    public interface GetmanifastDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS", "TIME_STAMP")]
        int Insert(DT_D12 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_D12 data);

        /// <summary>
        /// D12 2次マニフェスト情報レコード最大枝番検索
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenmaniSaishuShobun.Sql.GemanifastMaxSeq.sql")]
        new DataTable GetMaxSeq(GetMaxSeqDtoCls data);

        /// <summary>
        /// KANRI_IDを元にデータを取得する
        /// </summary>
        /// <param name="kanriId"></param>
        /// <returns></returns>
        [Query("KANRI_ID = /*kanriId*/")]
        DT_D12[] GetD12(string kanriId);
    }

    [Bean(typeof(DT_D13))]
    public interface GetjigyoubaDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS", "TIME_STAMP")]
        int Insert(DT_D13 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(DT_D13 data);

        /// <summary>
        /// D13 最終処分終了日・事業場情報レコード最大枝番検索
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.DenmaniSaishuShobun.Sql.GetjigyoubaMaxSeq.sql")]
        new DataTable GetMaxSeq(GetMaxSeqDtoCls data);

        /// <summary>
        /// KANRI_IDを元にデータを取得する
        /// </summary>
        /// <param name="kanriId"></param>
        /// <returns></returns>
        [Query("KANRI_ID = /*kanriId*/")]
        DT_D13[] GetD13(string kanriId);
    }

    [Bean(typeof(DT_MF_TOC))]
    public interface GetmokujiDaoCls : IS2Dao
    {
        /// <summary>
        /// マニフェスト目次情報更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("MANIFEST_ID", "LATEST_SEQ",
            "APPROVAL_SEQ", "STATUS_FLAG", "READ_FLAG", "KIND",
            "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_TS")]
        int Update(DT_MF_TOC data);
    }
}
