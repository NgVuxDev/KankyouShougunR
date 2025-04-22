using System.Collections.Generic;
using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 引合業者マスタDao
    /// </summary>
    [Bean(typeof(M_HIKIAI_GYOUSHA))]
    public interface IM_HIKIAI_GYOUSHADao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_HIKIAI_GYOUSHA")]
        M_HIKIAI_GYOUSHA[] GetAllData();

        /// <summary>
        /// 当該業者マスタが引合業者から移行された場合の、移行元の引合業者マスタを取得
        /// </summary>
        /// <param name="gyoushaCdAfter"></param>
        /// <returns></returns>
        [Query("GYOUSHA_CD_AFTER = /*gyoushaCdAfter*/ AND DELETE_FLG = 1")]
        M_HIKIAI_GYOUSHA GetHikiaiGyousha(string gyoushaCdAfter);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        List<M_HIKIAI_GYOUSHA> GetHikiaiGyoushaList(M_HIKIAI_GYOUSHA entity);

        [Sql("UPDATE M_HIKIAI_GYOUSHA SET HIKIAI_TORIHIKISAKI_USE_FLG = 0, TORIHIKISAKI_CD = /*afterTorihikisakiCd*/ WHERE HIKIAI_TORIHIKISAKI_USE_FLG = 1 AND TORIHIKISAKI_CD = /*torihikisakiCd*/ AND DELETE_FLG = 0")]
        int UpdateHikiaiTorihikisakiCd(string torihikisakiCd, string afterTorihikisakiCd);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGyousha.IM_HIKIAI_GYOUSHADao_GetHikiaiGyoushaAllValidData.sql")]
        M_HIKIAI_GYOUSHA[] GetAllValidData(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// 業者コードの最大値を取得する
        /// </summary>
        /// <returns>最大値</returns>
        [Sql("SELECT ISNULL(MAX(GYOUSHA_CD),1) FROM M_HIKIAI_GYOUSHA WHERE ISNUMERIC(GYOUSHA_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxKey();

        /// <summary>
        /// 業者コードの最小値を取得する
        /// </summary>
        /// <returns>最小値</returns>
        [Sql("SELECT ISNULL(MIN(GYOUSHA_CD),1) FROM M_HIKIAI_GYOUSHA WHERE ISNUMERIC(GYOUSHA_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMinKey();

        /// <summary>
        /// 業者コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(GYOUSHA_CD),0)+1 FROM M_HIKIAI_GYOUSHA WHERE ISNUMERIC(GYOUSHA_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxPlusKey();

        /// <summary>
        /// 業者コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT GYOUSHA_CD FROM M_HIKIAI_GYOUSHA WHERE SHOKUCHI_KBN = 1 and ISNUMERIC(GYOUSHA_CD) = 1 order by SHOKUCHI_KBN ASC")]
        M_HIKIAI_GYOUSHA[] GetDateByChokuchiKbn1();

        /// <summary>
        /// 業者コードをもとに削除されていない業者のデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("GYOUSHA_CD = /*cd*/")]
        M_HIKIAI_GYOUSHA GetDataByCd(string cd);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// 引合業者に関連する引合現場のデータ取得を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>取得したDataTable</returns>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGyousha.IM_HIKIAI_GYOUSHADao_GetIchiranGenbaDataSql.sql")]
        DataTable GetIchiranGenbaData(M_HIKIAI_GENBA data);

        /// <summary>
        /// 引合業者に関連する地域のデータ取得を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>取得したDataTable</returns>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGyousha.IM_HIKIAI_GYOUSHADao_GetChiikiDataSql.sql")]
        DataTable GetChiikiData(M_CHIIKI data);

        /// <summary>
        /// 引合業者に関連するポップアップデータ取得を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>取得したDataTable</returns>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGyousha.IM_HIKIAI_GYOUSHADao_GetPopupDataSql.sql")]
        DataTable GetPopupData(M_SHAIN data);

        // 201400709 syunrei #947 №19　start
        /// <summary>
        /// 修正モードにて業者コードを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGyousha.IM_HIKIAI_GYOUSHADao_GetHikiaiGyoushaData.sql")]
        M_HIKIAI_GYOUSHA GetGyoushaData(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// 見積入力Data（T_MITUMORI_ENTRY）を更新行う
        /// </summary>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        /// <param name="newGYOUSHA_CD">newGYOUSHA_CD</param>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGyousha.IM_HIKIAI_GYOUSHADao_UpdateMitsumoriEntryData.sql")]
        bool UpdateGYOUSHA_CD(string oldGYOUSHA_CD, string newGYOUSHA_CD);
        // 201400709 syunrei #947 №19　end

        // 2014007016 chinchisi EV005237_引合取引先を既存取引先に本登録(移行)した時に、引合取引先を使用している引合業者・引合現場の取引先も本登録先に変更する　start
        /// <summary>
        /// 連携キを更新
        /// </summary>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        /// <param name="newGYOUSHA_CD">newGYOUSHA_CD</param>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGyousha.IM_HIKIAI_GYOUSHADao_UpdateGyoushaCD.sql")]
        bool UpdateGYOUSHA_CD_AFTER(string oldGYOUSHA_CD, string newGYOUSHA_CD);
        // 2014007016 chinchisi EV005237_引合取引先を既存取引先に本登録(移行)した時に、引合取引先を使用している引合業者・引合現場の取引先も本登録先に変更する　end
    }
}