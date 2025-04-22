using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO
{
    /// <summary>
    /// 業者マスタDao
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
        /// 業者コード最大値を取得
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetMaxGyoushaCode.sql")]
        string GetMaxGyoushaCode(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.SqlFile.HikiaiGyousha.IM_HIKIAI_GYOUSHADao_GetAllValidData.sql")]
        M_HIKIAI_GYOUSHA[] GetAllValidData(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// コードを元に業者データを取得する
        /// </summary>
        /// <parameparam name="cd">業者コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("GYOUSHA_CD = /*cd*/")]
        M_HIKIAI_GYOUSHA GetDataByCd(string cd);

        /// <summary>
        /// 業者コード、取引先コードを元に業者データを取得する
        /// </summary>
        /// <parameparam name="gyoushaCd">業者コード</parameparam>
        /// <parameparam name="torihikisakiCd">取引先コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("GYOUSHA_CD = /*gyoushaCd*/ and TORIHIKISAKI_CD = /*torihikisakiCd*/")]
        M_HIKIAI_GYOUSHA GetDataByTorihikisakiCd(string gyoushaCd, string torihikisakiCd);

        /// <summary>
        /// 業者コードを元に運搬事業者を取得する
        /// </summary>
        /// <parameparam name="cd">業者コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("GYOUSHA_CD = /*cd*/ and UNPAN_JUTAKUSHA = 1")]
        M_HIKIAI_GYOUSHA GetUnpanJutakusha(string cd);

        [Sql("select M_HIKIAI_GYOUSHA.GYOUSHA_CD AS CD,M_HIKIAI_GYOUSHA.GYOUSHA_NAME_RYAKU AS NAME FROM M_HIKIAI_GYOUSHA /*$whereSql*/ group by M_HIKIAI_GYOUSHA.GYOUSHA_CD,M_HIKIAI_GYOUSHA.GYOUSHA_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// 業者コードの最大値を取得する
        /// </summary>
        /// <returns>最大値</returns>
        [Sql("SELECT ISNULL(MAX(GYOUSHA_CD),1) FROM M_HIKIAI_GYOUSHA WHERE ISNUMERIC(GYOUSHA_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxKey();

        /// <summary>
        /// 業者コード空き番号を取得
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetUselessGyoushaCode.sql")]
        string GetUselessGyoushaCode(M_HIKIAI_GYOUSHA data);

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
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetCustomDataGridViewDataSqlFile(string path, M_HIKIAI_GYOUSHA data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

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
        int UpdatePartData(string path, M_HIKIAI_GYOUSHA data, string oldPost, string oldAddress, string newPost, string newAddress);

    }
}