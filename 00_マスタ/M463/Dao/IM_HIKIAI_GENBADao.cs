// $Id: IM_HIKIAI_GENBADao.cs 32015 2014-10-09 08:23:38Z y-hosokawa@takumi-sys.co.jp $
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.HikiaiGenbaHoshu.Dao
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
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetHikiaiGenbaAllValidData.sql")]
        M_HIKIAI_GENBA[] GetAllValidData(M_HIKIAI_GENBA data);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetHikiaiGenbaAllValidDataMinCols.sql")]
        M_HIKIAI_GENBA[] GetAllValidDataMinCols(M_HIKIAI_GENBA data);

        /// <summary>
        /// 業者コードの最大値を取得する
        /// </summary>
        /// <returns>最大値</returns>
        [Sql("SELECT ISNULL(MAX(GENBA_CD), 1) FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 AND SHOKUCHI_KBN = 0")]
        int GetMaxKey();

        /// <summary>
        /// 業者コードの最小値を取得する
        /// </summary>
        /// <returns>最小値</returns>
        [Sql("SELECT ISNULL(MIN(GENBA_CD), 1) FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 AND SHOKUCHI_KBN = 0")]
        int GetMinKey();

        /// <summary>
        /// 業者コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(GENBA_CD), 0) + 1 FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 AND SHOKUCHI_KBN = 0 AND GYOUSHA_CD = /*gyoushaCd*/ AND HIKIAI_GYOUSHA_USE_FLG = /*hikiaiFlg*/")]
        int GetMaxPlusKeyByGyoushaCd(string gyoushaCd, string hikiaiFlg);

        /// <summary>
        /// 業者コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT GENBA_CD FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 AND SHOKUCHI_KBN = 1 AND GYOUSHA_CD = /*gyoushaCd*/ AND HIKIAI_GYOUSHA_USE_FLG = /*hikiaiFlg*/")]
        M_HIKIAI_GENBA[] GetDataByShokuchiKbn1(string gyoushaCd, string hikiaiFlg);

        /// <summary>
        /// 現場コードを元に現場情報を取得
        /// </summary>
        /// <param name="data"></param>
        [Query("HIKIAI_GYOUSHA_USE_FLG = /*data.HIKIAI_GYOUSHA_USE_FLG*/ AND GYOUSHA_CD = /*data.GYOUSHA_CD*/ AND GENBA_CD = /*data.GENBA_CD*/")]
        M_HIKIAI_GENBA GetDataByCd(M_HIKIAI_GENBA data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_HIKIAI_GENBA data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// 引合業者に関連する地域のデータ取得を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>取得したDataTable</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetChiikiDataSql.sql")]
        DataTable GetChiikiData(M_CHIIKI data);

        /// <summary>
        /// 引合業者に関連するポップアップデータ取得を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>取得したDataTable</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetPopupDataSql.sql")]
        DataTable GetPopupData(M_SHAIN data);

        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetHinmeiUriageShiharaiDataSql.sql")]
        DataTable SqlGetHinmeiUriageShiharaiData(M_KOBETSU_HINMEI data);

        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetHinmeiUriageShiharaiDataMinCols.sql")]
        DataTable SqlGetHinmeiUriageShiharaiDataMinCols(M_HINMEI data);

        // 201400709 syunrei #947 №19 start
        /// <summary>
        /// 修正モードにて業者コードを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetHikiaiGenbaData.sql")]
        M_HIKIAI_GENBA GetGenbaData(M_HIKIAI_GENBA data);

        /// <summary>
        /// 見積入力Data（T_MITUMORI_ENTRY）を更新行う
        /// </summary>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        /// <param name="newGYOUSHA_CD">newGYOUSHA_CD</param>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.UpdateMitsumoriEntryData.sql")]
        bool UpdateGYOUSHA_CD(string oldGENBA_CD, string newGENBA_CD, string oldGYOUSHA_CD);

        // 201400709 syunrei #947 №19 end

        // 20140718 ria EV005242 引合現場を移行させるとき、定期回収情報タブと月極情報タブのみ移行されない start
        /// <summary>
        /// 現場_定期情報マスタ登録（M_GENBA_TEIKI_HINMEI）
        /// </summary>
        /// <param name="oldGENBA_CD">oldGENBA_CD</param>
        /// <param name="newGENBA_CD">newGENBA_CD</param>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.InsertGenbaTeikiHinmeiData.sql")]
        [NoPersistentProps("TIME_STAMP")]
        int InsertTEIKI_HINMEI(string oldGENBA_CD, string newGENBA_CD, string oldGYOUSHA_CD);

        /// <summary>
        /// 現場_月極情報（M_GENBA_TSUKI_HINMEI）を更新行う
        /// </summary>
        /// <param name="oldGENBA_CD">oldGENBA_CD</param>
        /// <param name="newGENBA_CD">newGENBA_CD</param>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.InsertGenbaTsukiHinmeiData.sql")]
        [NoPersistentProps("TIME_STAMP")]
        int InsertTSUKI_HINMEI(string oldGENBA_CD, string newGENBA_CD, string oldGYOUSHA_CD);

        // 20140718 ria EV005242 引合現場を移行させるとき、定期回収情報タブと月極情報タブのみ移行されない end

        /// <summary>
        /// 現場コードの最小の空き番を取得する
        /// </summary>
        /// <param name="data">nullを渡す</param>
        /// <returns>最小の空き番</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetMinBlankNo.sql")]
        int GetMinBlankNo(string gyoushaCd, string hikiaiFlg);
    }
}