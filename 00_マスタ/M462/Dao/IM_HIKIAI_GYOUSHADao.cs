// $Id: IM_HIKIAI_GYOUSHADao.cs 31689 2014-10-06 09:30:11Z y-hosokawa@takumi-sys.co.jp $
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.HikiaiGyousha.Dao
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
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.GetHikiaiGyoushaAllValidData.sql")]
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
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// 引合業者に関連する引合現場のデータ取得を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>取得したDataTable</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.GetIchiranGenbaDataSql.sql")]
        DataTable GetIchiranGenbaData(M_HIKIAI_GENBA data);

        /// <summary>
        /// 引合業者に関連する地域のデータ取得を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>取得したDataTable</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.GetChiikiDataSql.sql")]
        DataTable GetChiikiData(M_CHIIKI data);

        /// <summary>
        /// 引合業者に関連するポップアップデータ取得を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>取得したDataTable</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.GetPopupDataSql.sql")]
        DataTable GetPopupData(M_SHAIN data);

        // 201400709 syunrei #947 №19　start
        /// <summary>
        /// 修正モードにて業者コードを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.GetHikiaiGyoushaData.sql")]
        M_HIKIAI_GYOUSHA GetGyoushaData(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// 見積入力Data（T_MITUMORI_ENTRY）を更新行う
        /// </summary>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        /// <param name="newGYOUSHA_CD">newGYOUSHA_CD</param>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.UpdateMitsumoriEntryData.sql")]
        bool UpdateGYOUSHA_CD(string oldGYOUSHA_CD, string newGYOUSHA_CD);
        // 201400709 syunrei #947 №19　end

        // 2014007016 chinchisi EV005237_引合取引先を既存取引先に本登録(移行)した時に、引合取引先を使用している引合業者・引合現場の取引先も本登録先に変更する　start
        /// <summary>
        /// 連携キを更新
        /// </summary>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        /// <param name="newGYOUSHA_CD">newGYOUSHA_CD</param>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.UpdateGyoushaCD.sql")]
        bool UpdateGYOUSHA_CD_AFTER(string oldGYOUSHA_CD, string newGYOUSHA_CD);
        // 2014007016 chinchisi EV005237_引合取引先を既存取引先に本登録(移行)した時に、引合取引先を使用している引合業者・引合現場の取引先も本登録先に変更する　end

        /// <summary>
        /// 業者コードの最小の空き番を取得する
        /// </summary>
        /// <param name="data">nullを渡す</param>
        /// <returns>最小の空き番</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.GetMinBlankNo.sql")]
        int GetMinBlankNo(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// 取引先を使用している業者現場の適用開始日を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.GetTeikiyouBeginDateSql.sql")]
        DataTable GetTekiyouBegin(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// 取引先を使用している業者現場の適用終了日を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.GetTeikiyouEndDateSql.sql")]
        DataTable GetTekiyouEnd(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// 他マスタで使用されいているかチェックする
        /// </summary>
        /// <param name="GYOUSHA_CD"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.CheckDeleteHikiaiGyoushaSql.sql")]
        DataTable GetDataBySqlFileCheck(string GYOUSHA_CD);
    }
}