using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    [Bean(typeof(M_UNCHIN_HINMEI))]
    public interface IM_UNCHIN_HINMEIDao : IS2Dao
    {
        [Sql("SELECT * FROM M_UNCHIN_HINMEI")]
        M_UNCHIN_HINMEI[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.UnchinHinmei.IM_UNCHIN_HINMEIDao_GetAllValidData.sql")]
        M_UNCHIN_HINMEI[] GetAllValidData(M_UNCHIN_HINMEI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_UNCHIN_HINMEI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_UNCHIN_HINMEI data);

        int Delete(M_UNCHIN_HINMEI data);

        [Sql("SELECT M_UNCHIN_HINMEI.UNCHIN_HINMEI_CD AS CD, M_UNCHIN_HINMEI.UNCHIN_HINMEI_NAME AS NAME" +
            " FROM M_UNCHIN_HINMEI /*$whereSql*/" +
            " GROUP BY M_UNCHIN_HINMEI.UNCHIN_HINMEI_CD, M_UNCHIN_HINMEI.UNCHIN_HINMEI_NAME")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_UNCHIN_HINMEI data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("UNCHIN_HINMEI_CD = /*cd*/")]
        M_UNCHIN_HINMEI GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_UNCHIN_HINMEI data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}