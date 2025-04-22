using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.DAO
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
    }

    [Bean(typeof(M_DENSHI_HAIKI_SHURUI))]
    public interface DAOClass_CheckHaiki : IS2Dao
    {
        /// <summary>
        /// 廃棄物種類をチェックするSQL文
        /// </summary>
        /// <param name="HAIKI_KBN_CD"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.CheckHaiki.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_DENSHI_JIGYOUSHA))]
    public interface DAOClass_CheckJigyousya : IS2Dao
    {
        /// <summary>
        /// 排出事業者をチェックするSQL文
        /// </summary>
        /// <param name="JIGYOUSHA_CD"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.CheckJigyousya.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_DENSHI_JIGYOUSHA))]
    public interface DAOClass_CheckUnpan : IS2Dao
    {
        /// <summary>
        /// 報告収集運搬をチェックするSQL文
        /// </summary>
        /// <param name="JIGYOUSHA_CD"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.CheckUnpan.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_DENSHI_JIGYOUSHA))]
    public interface DAOClass_CheckUnpansha : IS2Dao
    {
        /// <summary>
        /// 運搬先事業者をチェックするSQL文
        /// </summary>
        /// <param name="JIGYOUSHA_CD"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.CheckUnpansha.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_DENSHI_HAIKI_SHURUI))]
    public interface DAOClass_PopupHaiki : IS2Dao
    {
        /// <summary>
        /// 廃棄物種類POPUP画面のデータソースを取得
        /// </summary>
        /// <param name=""></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.GetPopupHaiki.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_DENSHI_JIGYOUSHA))]
    public interface DAOClass_PopupJigyousya : IS2Dao
    {
        /// <summary>
        /// 排出事業者POPUP画面のデータソースを取得
        /// </summary>
        /// <param name=""></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.GetPopupJigyousya.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_DENSHI_JIGYOUSHA))]
    public interface DAOClass_PopupUnpan : IS2Dao
    {
        /// <summary>
        /// 報告収集運搬POPUP画面のデータソースを取得
        /// </summary>
        /// <param name=""></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.GetPopupUnpan.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_DENSHI_JIGYOUSHA))]
    public interface DAOClass_PopupUnpanSha : IS2Dao
    {
        /// <summary>
        /// 運搬先事業者POPUP画面のデータソースを取得
        /// </summary>
        /// <param name=""></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.GetPopupUnpanSha.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(QUE_INFO))]
    public interface DAOClass_QUE : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("REQUEST_CODE", "EDI_RECORD_ID", "TUUCHI_ID", "TIME_STAMP")]
        int Insert(QUE_INFO data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("SEQ", "REQUEST_CODE", "EDI_RECORD_ID", "FUNCTION_ID", "UPN_ROUTE_NO", "TUUCHI_ID", "CREATE_DATE","UPDATE_TS")]
        int Update(QUE_INFO data);

        /// <summary>
        /// 明細のheader部分の情報を取得する
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.GetMaxQueSeq.sql")]
        QUE_INFO GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(DT_MF_TOC))]
    public interface DAOClass_DT_MF_TOC : IS2Dao
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("KANRI_ID", "MANIFEST_ID", "LATEST_SEQ", "APPROVAL_SEQ", "STATUS_FLAG", "READ_FLAG", "CREATE_DATE", "UPDATE_TS")]
        int Update(DT_MF_TOC data);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_MF_TOC data);
    }

    [Bean(typeof(DT_D10))]
    public interface DAOClass_DT_D10 : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_D10 data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("KANRI_ID", "CREATE_DATE", "UPDATE_TS")]
        int Update(DT_D10 data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Delete(DT_D10 data);

        /// <summary>
        /// KANRI_IDを元にデータを取得する
        /// </summary>
        /// <param name="kanriId"></param>
        /// <returns></returns>
        [Query("KANRI_ID = /*kanriId*/")]
        DT_D10[] GetD10(string kanriId);
    }

    [Bean(typeof(DT_D10_MOD))]
    public interface DAOClass_DT_D10_MOD : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Insert(DT_D10_MOD data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("KANRI_ID","SBN_END_DATE", "CREATE_DATE", "UPDATE_TS")]
        int Update(DT_D10_MOD data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_TS")]
        int Delete(DT_D10_MOD data);
    }

    [Bean(typeof(M_DENSHI_TANTOUSHA))]
    public interface DAOClass_PopupUpnTantou : IS2Dao
    {
        /// <summary>
        /// 運搬担当者POPUP画面のデータソースを取得
        /// </summary>
        /// <param name=""></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.GetPopupUpnTantou.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_DENSHI_TANTOUSHA))]
    public interface DAOClass_PopupRepTantou : IS2Dao
    {
        /// <summary>
        /// 報告担当者POPUP画面のデータソースを取得
        /// </summary>
        /// <param name=""></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.GetPopupRepTantou.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_DENSHI_TANTOUSHA))]
    public interface DAOClass_PopupShobunTantou : IS2Dao
    {
        /// <summary>
        /// 報告担当者POPUP画面のデータソースを取得
        /// </summary>
        /// <param name=""></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.GetPopupShobunTantou.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_UNIT))]
    public interface DAOClass_PopupUpnTani : IS2Dao
    {
        /// <summary>
        /// 運搬単位POPUP画面のデータソースを取得
        /// </summary>
        /// <param name=""></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.GetPopupUpnTani.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }
    　
    [Bean(typeof(M_UNIT))]
    public interface DAOClass_PopupYuuTani : IS2Dao
    {
        /// <summary>
        /// 有価単位POPUP画面のデータソースを取得
        /// </summary>
        /// <param name=""></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.GetPopupYuuTani.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }
    [Bean(typeof(M_SHARYOU))]
    public interface DAOClass_PopupSharyo : IS2Dao
    {
        /// <summary>
        /// 有価単位POPUP画面のデータソースを取得
        /// </summary>
        /// <param name=""></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.GetPopupSharyo.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_NYUUSHUKKIN_KBN))]
    public interface DAOClass_PopupNyushuukkin_Kbn : IS2Dao
    {
        /// <summary>
        /// 入出金区分コードPOPUP画面のデータソースを取得
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.GetPopupNyuushukkin_kbn.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_DENSHI_TANTOUSHA))]
    public interface DAOClass_SearchUpnName : IS2Dao
    {
        /// <summary>
        /// 入出金区分コードPOPUP画面のデータソースを取得
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.SearchUpnName.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_DENSHI_TANTOUSHA))]
    public interface DAOClass_SearchRepName : IS2Dao
    {
        /// <summary>
        /// 入出金区分コードPOPUP画面のデータソースを取得
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.SearchRepName.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_DENSHI_TANTOUSHA))]
    public interface DAOClass_SearchSyobunName : IS2Dao
    {
        /// <summary>
        /// 入出金区分コードPOPUP画面のデータソースを取得
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.SearchShobunName.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_UNIT))]
    public interface DAOClass_SearchTaniName : IS2Dao
    {
        /// <summary>
        /// 入出金区分コードPOPUP画面のデータソースを取得
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.SearchTaniName.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_SHARYOU))]
    public interface DAOClass_SearchSharyoName : IS2Dao
    {
        /// <summary>
        /// 入出金区分コードPOPUP画面のデータソースを取得
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.Sql.SearchSharyoName.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(IMPORT_MEMBER_FILTER))]
    public interface ImportMemberFilterDaoClas : IS2Dao
    {
        /// <summary>
        /// IMPORT_MEMBER_FILTERを全件取得
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT   ISNULL(MEMBER_ID1,'') + ',' +    ISNULL(MEMBER_ID2,'') + ',' +    ISNULL(MEMBER_ID3,'') + ',' +    ISNULL(MEMBER_ID4,'') + ',' +    ISNULL(MEMBER_ID5,'') FROM IMPORT_MEMBER_FILTER ")]
        DataTable GetAllData();
    }
}
