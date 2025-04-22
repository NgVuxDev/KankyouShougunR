using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO
{
    /// <summary>
    /// 現場マスタDao
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
        /// 現場コード最大値を取得
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetMaxGenbaCode.sql")]
        string GetMaxGenbaCode(M_HIKIAI_GENBA data);

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
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.SqlFile.HikiaiGenba.IM_HIKIAI_GENBADao_GetAllValidData.sql")]
        M_HIKIAI_GENBA[] GetAllValidData(M_HIKIAI_GENBA data);

        /// <summary>
        /// 顧客一覧表示用データ取得
        /// </summary>
        /// <parameparam name="searchString">検索条件</parameparam>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.SqlFile.HikiaiGenba.IM_HIKIAI_GENBADao_GetKokyakuCustomDataGridViewData.sql")]
        new DataTable GetCustomDataGridViewData(string searchString);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <parameparam name="sql">検索条件</parameparam>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.SqlFile.HikiaiGenba.IM_HIKIAI_GENBADao_GetUserSettingData.sql")]
        new DataTable GetUserSettingData(string sql);

        /// <summary>
        /// 顧客マスタデータ取得
        /// </summary>
        /// <parameparam name="gyoushaCD">業者コード</parameparam>
        /// <parameparam name="genbaCD">現場コード</parameparam>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.SqlFile.HikiaiGenba.IM_HIKIAI_GENBADao_GetKokyakuMasterData.sql")]
        DataTable GetKokyakuMasterData(string gyoushaCD, string genbaCD);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する(マスタ共通ポップアップ)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.SqlFile.HikiaiGenba.IM_HIKIAI_GENBADao_GetAllValidDataForPopUp.sql")]
        DataTable GetAllValidDataForPopUp(M_HIKIAI_GENBA data);

        /// <summary>
        /// 現場コードを元に現場情報を取得
        /// </summary>
        /// <param name="data"></param>
        [Query("GYOUSHA_CD = /*data.GYOUSHA_CD*/ and GENBA_CD = /*data.GENBA_CD*/")]
        M_HIKIAI_GENBA GetDataByCd(M_HIKIAI_GENBA data);

        /// <summary>
        /// 取引先コード、業者コード、現場コードを指定しマスタデータを取得
        /// </summary>
        /// <parameparam name="torihikisakiCd">取引先コード</parameparam>
        /// <parameparam name="gyoushaCd">業者コード</parameparam>
        /// <parameparam name="genbaCd">現場コード</parameparam>
        [Query("TORIHIKISAKI_CD = /*torihikisakiCd*/ and GYOUSHA_CD = /*gyoushaCd*/ and GENBA_CD = /*genbaCd*/")]
        M_HIKIAI_GENBA GetGenbaData(string torihikisakiCd, string gyoushaCd, string genbaCd);

        /// <summary>
        /// 現場コードを元に削除されていない情報を取得
        /// </summary>
        /// <parameparam name="genbaCd">現場コード</parameparam>
        [Query("GENBA_CD = /*genbaCd*/ and SHOBUN_JIGYOUJOU = 1")]
        M_HIKIAI_GENBA GetShobunjigyousya(string genbaCd);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_HIKIAI_GENBA data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得(業者エンティティ付)
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="gyousha">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileWithGyousha(string path, M_HIKIAI_GENBA data, M_HIKIAI_GYOUSHA gyousha);

        /// <summary>
        /// 業者コードの最大値を取得する
        /// </summary>
        /// <returns>最大値</returns>
        [Sql("SELECT ISNULL(MAX(GENBA_CD),1) FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxKey();

        /// <summary>
        /// 現場コード空き番号を取得
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetUselessGenbaCode.sql")]
        string GetUselessGenbaCode(M_HIKIAI_GENBA data);


        /// <summary>
        /// 業者コードの最小値を取得する
        /// </summary>
        /// <returns>最小値</returns>
        [Sql("SELECT ISNULL(MIN(GENBA_CD),1) FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMinKey();

        ///// <summary>
        ///// 業者コードの最大値+1を取得する
        ///// </summary>
        ///// <returns>最大値+1</returns>
        //[Sql("SELECT ISNULL(MAX(GENBA_CD),0)+1 FROM M_HIKIAI_GENBA WHERE SHOKUCHI_KBN = 0")]
        //int GetMaxPlusKey();

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
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetCustomDataGridViewDataSqlFile(string path, M_HIKIAI_GENBA data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        [Sql("select M_HIKIAI_GENBA.GENBA_CD as CD,M_HIKIAI_GENBA.GENBA_NAME_RYAKU as NAME FROM M_HIKIAI_GENBA /*$whereSql*/ group by M_HIKIAI_GENBA.GENBA_CD,M_HIKIAI_GENBA.GENBA_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// 住所の一部データ書き換え機能
        /// </summary>
        /// <param name="path">SQLファイルのパス</param>
        /// <param name="data">現場マスタエンティティ</param>
        /// <param name="oldPost">旧郵便番号</param>
        /// <param name="oldAddress">旧住所</param>
        /// <param name="newPost">新郵便番号</param>
        /// <param name="newAddress">新住所</param>
        /// <returns></returns>
        int UpdatePartData(string path, M_HIKIAI_GENBA data, string oldPost, string oldAddress, string newPost, string newAddress);
    }
}