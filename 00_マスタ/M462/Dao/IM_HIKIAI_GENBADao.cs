// $Id: IM_HIKIAI_GENBADao.cs 26123 2014-07-18 08:54:09Z ria_koec $
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.HikiaiGyousha.Dao
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
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.GetHikiaiGenbaAllValidData.sql")]
        M_HIKIAI_GENBA[] GetAllValidData(M_HIKIAI_GENBA data);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.GetHikiaiGenbaAllValidExistCheckData.sql")]
        M_HIKIAI_GENBA[] GetAllValidExistCheckData(M_HIKIAI_GENBA data);

        /// <summary>
        /// 業者コードの最大値を取得する
        /// </summary>
        /// <returns>最大値</returns>
        [Sql("SELECT ISNULL(MAX(GENBA_CD), 1) FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxKey();

        /// <summary>
        /// 業者コードの最小値を取得する
        /// </summary>
        /// <returns>最小値</returns>
        [Sql("SELECT ISNULL(MIN(GENBA_CD), 1) FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMinKey();

        /// <summary>
        /// 業者コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(GENBA_CD), 0) + 1 FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 and SHOKUCHI_KBN = 0 and GYOUSHA_CD = /*gyoushaCd*/")]
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
        [Query("GYOUSHA_CD = /*data.GYOUSHA_CD*/ and GENBA_CD = /*data.GENBA_CD*/")]
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

        // 2014007017 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする　start
        /// <summary>
        /// 移行なら、M_HIKIAI_GENBAに関連データを更新
        /// </summary>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        /// <param name="newGYOUSHA_CD">newGYOUSHA_CD</param>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.UpdateGenbaCD.sql")]
        bool UpdateGenba_CD(string oldGYOUSHA_CD, string newGYOUSHA_CD);
        // 2014007017 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする　end

        // 20140718 ria EV005242 引合現場を移行させるとき、定期回収情報タブと月極情報タブのみ移行されない start
        /// <summary>
        /// 現場_定期情報マスタ登録（M_GENBA_TEIKI_HINMEI）
        /// </summary>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        /// <param name="newGYOUSHA_CD">newGYOUSHA_CD</param>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.UpdateGenbaTeikiGyoushaCD.sql")]
        [NoPersistentProps("TIME_STAMP")]
        bool UpdateGenbaTEIKI(string oldGYOUSHA_CD, string newGYOUSHA_CD);

        /// <summary>
        /// 現場_月極情報（M_GENBA_TSUKI_HINMEI）を更新行う
        /// </summary>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        /// <param name="newGYOUSHA_CD">newGYOUSHA_CD</param>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.UpdateGenbaTsukiGyoushaCD.sql")]
        [NoPersistentProps("TIME_STAMP")]
        bool UpdateGenbaTSUKI(string oldGYOUSHA_CD, string newGYOUSHA_CD);
        // 20140718 ria EV005242 引合現場を移行させるとき、定期回収情報タブと月極情報タブのみ移行されない end
    }
}