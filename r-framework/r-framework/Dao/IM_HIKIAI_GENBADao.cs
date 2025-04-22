// $Id: IM_HIKIAI_GENBADao.cs 26123 2014-07-18 08:54:09Z ria_koec $
using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Collections.Generic;

namespace r_framework.Dao
{
    /// <summary>
    /// 引合現場マスタDao
    /// </summary>
    [Bean(typeof(M_HIKIAI_GENBA))]
    public interface IM_HIKIAI_GENBADao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_HIKIAI_GENBA data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_HIKIAI_GENBA data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_HIKIAI_GENBA data);

        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_HIKIAI_GENBA")]
        M_HIKIAI_GENBA[] GetAllData();

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得（引合現場）
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileForHikiaiGenba(string path, M_HIKIAI_GENBA data);

        /// <summary>
        /// 指定された条件の引合現場マスタを取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        [Query("GYOUSHA_CD = /*gyoushaCd*/ AND JISHA_KBN = 1 AND DELETE_FLG = 0")]
        M_HIKIAI_GENBA[] GetHikiaiGenbaJisha(string gyoushaCd);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        List<M_HIKIAI_GENBA> GetHikiaiGenbaList(M_HIKIAI_GENBA entity);

        [Sql("UPDATE M_HIKIAI_GENBA SET HIKIAI_TORIHIKISAKI_USE_FLG = 0, TORIHIKISAKI_CD = /*afterTorihikisakiCd*/ WHERE HIKIAI_TORIHIKISAKI_USE_FLG = 1 AND TORIHIKISAKI_CD = /*torihikisakiCd*/ AND DELETE_FLG = 0")]
        int UpdateHikiaiTorihikisakiCd(string torihikisakiCd, string afterTorihikisakiCd);

        [Sql("UPDATE M_HIKIAI_GENBA SET HIKIAI_GYOUSHA_USE_FLG = 0, GYOUSHA_CD = /*afterGyoushaCd*/ WHERE HIKIAI_GYOUSHA_USE_FLG = 1 AND GYOUSHA_CD = /*gyoushaCd*/ AND DELETE_FLG = 0")]
        int UpdateHikiaiGyoushaCd(string gyoushaCd, string afterGyoushaCd);
		
        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGenba.IM_HIKIAI_GENBA_Dao_GetHikiaiGenbaAllValidData.sql")]
        M_HIKIAI_GENBA[] GetAllValidData(M_HIKIAI_GENBA data);

        /// <summary>
        /// 業者コードの最大値を取得する
        /// </summary>
        /// <returns>最大値</returns>
        [Sql("SELECT ISNULL(MAX(GENBA_CD),1) FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxKey();

        /// <summary>
        /// 業者コードの最小値を取得する
        /// </summary>
        /// <returns>最小値</returns>
        [Sql("SELECT ISNULL(MIN(GENBA_CD),1) FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMinKey();

        /// <summary>
        /// 業者コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(GENBA_CD),0)+1 FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 and SHOKUCHI_KBN = 0 and GYOUSHA_CD = /*gyoushaCd*/")]
        int GetMaxPlusKeyByGyoushaCd(string gyoushaCd);

        /// <summary>
        /// 業者コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT GENBA_CD FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 and SHOKUCHI_KBN = 1 and GYOUSHA_CD = /*gyoushaCd*/")]
        M_HIKIAI_GENBA[] GetDataByShokuchiKbn1(string gyoushaCd);

        /// <summary>
        /// 現場コードを元に現場情報を取得
        /// </summary>
        /// <param name="data"></param>
        [Query("HIKIAI_GYOUSHA_USE_FLG = /*data.HIKIAI_GYOUSHA_USE_FLG*/ and GYOUSHA_CD = /*data.GYOUSHA_CD*/ and GENBA_CD = /*data.GENBA_CD*/")]
        M_HIKIAI_GENBA GetDataByCd(M_HIKIAI_GENBA data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_HIKIAI_GENBA data);

        /// <summary>
        /// 引合業者に関連する地域のデータ取得を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>取得したDataTable</returns>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGenba.IM_HIKIAI_GENBA_Dao_GetChiikiDataSql.sql")]
        DataTable GetChiikiData(M_CHIIKI data);

        /// <summary>
        /// 引合業者に関連するポップアップデータ取得を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>取得したDataTable</returns>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGenba.IM_HIKIAI_GENBA_Dao_GetPopupDataSql.sql")]
        DataTable GetPopupData(M_SHAIN data);

        [SqlFile("r-framework.Dao.SqlFile.HikiaiGenba.IM_HIKIAI_GENBA_Dao_GetHinmeiUriageShiharaiDataSql.sql")]
        DataTable SqlGetHinmeiUriageShiharaiData(M_HINMEI data);

        // 201400709 syunrei #947 №19　start
        /// <summary>
        /// 修正モードにて業者コードを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGenba.IM_HIKIAI_GENBA_Dao_GetHikiaiGenbaData.sql")]
        M_HIKIAI_GENBA GetGenbaData(M_HIKIAI_GENBA data);

        /// <summary>
        /// 見積入力Data（T_MITUMORI_ENTRY）を更新行う
        /// </summary>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        /// <param name="newGYOUSHA_CD">newGYOUSHA_CD</param>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGenba.IM_HIKIAI_GENBA_Dao_UpdateMitsumoriEntryData.sql")]
        bool UpdateGYOUSHA_CD(string oldGENBA_CD, string newGENBA_CD, string oldGYOUSHA_CD);
        // 201400709 syunrei #947 №19　end

        // 20140718 ria EV005242 引合現場を移行させるとき、定期回収情報タブと月極情報タブのみ移行されない start
        /// <summary>
        /// 現場_定期情報マスタ登録（M_GENBA_TEIKI_HINMEI）
        /// </summary>
        /// <param name="oldGENBA_CD">oldGENBA_CD</param>
        /// <param name="newGENBA_CD">newGENBA_CD</param>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGenba.IM_HIKIAI_GENBA_Dao_InsertGenbaTeikiHinmeiData.sql")]
        [NoPersistentProps("TIME_STAMP")]
        int InsertTEIKI_HINMEI(string oldGENBA_CD, string newGENBA_CD, string oldGYOUSHA_CD);

        /// <summary>
        /// 現場_月極情報（M_GENBA_TSUKI_HINMEI）を更新行う
        /// </summary>
        /// <param name="oldGENBA_CD">oldGENBA_CD</param>
        /// <param name="newGENBA_CD">newGENBA_CD</param>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGenba.IM_HIKIAI_GENBA_Dao_InsertGenbaTsukiHinmeiData.sql")]
        [NoPersistentProps("TIME_STAMP")]
        int InsertTSUKI_HINMEI(string oldGENBA_CD, string newGENBA_CD, string oldGYOUSHA_CD);
        // 20140718 ria EV005242 引合現場を移行させるとき、定期回収情報タブと月極情報タブのみ移行されない end
    }
}