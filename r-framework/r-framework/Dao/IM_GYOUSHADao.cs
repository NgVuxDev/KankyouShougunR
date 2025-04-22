using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 業者マスタDao
    /// </summary>
    [Bean(typeof(M_GYOUSHA))]
    public interface IM_GYOUSHADao : MasterAccess.Base.IMasterAccessDao<M_GYOUSHA>
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_GYOUSHA data);
        // 201400709 syunrei #947 №19　start
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int InsertGyosha(M_GYOUSHA data);
        // 201400709 syunrei #947 №19　end
        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_GYOUSHA data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_GYOUSHA data);

        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_GYOUSHA")]
        M_GYOUSHA[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Gyousha.IM_GYOUSHADao_GetAllValidData.sql")]
        M_GYOUSHA[] GetAllValidData(M_GYOUSHA data);

        /// <summary>
        /// コードを元に業者データを取得する
        /// </summary>
        /// <parameparam name="cd">業者コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("GYOUSHA_CD = /*cd*/")]
        M_GYOUSHA GetDataByCd(string cd);

        /// <summary>
        /// 業者コード、取引先コードを元に業者データを取得する
        /// </summary>
        /// <parameparam name="gyoushaCd">業者コード</parameparam>
        /// <parameparam name="torihikisakiCd">取引先コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("GYOUSHA_CD = /*gyoushaCd*/ and TORIHIKISAKI_CD = /*torihikisakiCd*/")]
        M_GYOUSHA GetDataByTorihikisakiCd(string gyoushaCd, string torihikisakiCd);

        /// <summary>
        /// 業者コードを元に運搬事業者を取得する
        /// </summary>
        /// <parameparam name="cd">業者コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("GYOUSHA_CD = /*cd*/ and UNPAN_JUTAKUSHA = 1")]
        M_GYOUSHA GetUnpanJutakusha(string cd);

        /// <summary>
        /// 運搬業者情報の取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("r_framework.Dao.SqlFile.Gyousha.IM_GYOUSHADao_GetUnpanGyoushaData.sql")]
        M_GYOUSHA GetUnpanGyoushaData(M_GYOUSHA data);

        [Sql("select M_GYOUSHA.GYOUSHA_CD AS CD,M_GYOUSHA.GYOUSHA_NAME_RYAKU AS NAME FROM M_GYOUSHA /*$whereSql*/ group by M_GYOUSHA.GYOUSHA_CD,M_GYOUSHA.GYOUSHA_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_GYOUSHA data);

        /// <summary>
        /// 業者コードの最大値を取得する
        /// </summary>
        /// <returns>最大値</returns>
        [Sql("SELECT ISNULL(MAX(GYOUSHA_CD),1) FROM M_GYOUSHA WHERE ISNUMERIC(GYOUSHA_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxKey();

        /// <summary>
        /// 業者コードの最小値を取得する
        /// </summary>
        /// <returns>最小値</returns>
        [Sql("SELECT ISNULL(MIN(GYOUSHA_CD),1) FROM M_GYOUSHA WHERE ISNUMERIC(GYOUSHA_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMinKey();

        /// <summary>
        /// 業者コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(GYOUSHA_CD),0)+1 FROM M_GYOUSHA WHERE ISNUMERIC(GYOUSHA_CD + '.0e0') = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxPlusKey();

        /// <summary>
        /// 業者コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT GYOUSHA_CD FROM M_GYOUSHA WHERE SHOKUCHI_KBN = 1 and ISNUMERIC(GYOUSHA_CD) = 1 order by SHOKUCHI_KBN ASC")]
        M_GYOUSHA[] GetDateByChokuchiKbn1();

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_GYOUSHA data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

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
        /// <param name="data">業者マスタエンティティ</param>
        /// <param name="oldPost">旧郵便番号</param>
        /// <param name="oldAddress">旧住所</param>
        /// <param name="newPost">新郵便番号</param>
        /// <param name="newAddress">新住所</param>
        /// <returns></returns>
        int UpdatePartData(string path, M_GYOUSHA data, string oldPost, string oldAddress, string newPost, string newAddress);

        /// <summary>
        /// 業者コードの最小の空き番を取得する
        /// </summary>
        /// <param name="data">nullを渡す</param>
        /// <returns>最小の空き番</returns>
        [SqlFile("r_framework.Dao.SqlFile.Gyousha.IM_GYOUSHADao_GetMinBlankNo.sql")]
        int GetMinBlankNo(M_GYOUSHA data);

        /// <summary>
        /// (代納入力)運搬業者情報の取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("r_framework.Dao.SqlFile.Gyousha.IM_GYOUSHADao_GetDainouUnpanGyoushaData.sql")]
        M_GYOUSHA GetDainouUnpanGyoushaData(M_GYOUSHA data);

        /// <summary>
        /// 業者のCTI連動データを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("r_framework.Dao.SqlFile.Gyousha.IM_GYOUSHADao_GetCtiRenkeiData.sql")]
        DataTable GetCtiRenkeiData(string tel, string gyousha, string selectType);

        // Begin: LANDUONG - 20220209 - refs#160050
        [Query("DELETE_FLG = 0 AND RAKURAKU_CUSTOMER_CD = /*code*/")]
        M_GYOUSHA[] GetDataByRakurakuCode(string code);
        // End: LANDUONG - 20220209 - refs#160050
    }
}