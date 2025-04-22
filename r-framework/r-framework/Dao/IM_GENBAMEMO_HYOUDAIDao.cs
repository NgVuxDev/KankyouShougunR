using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_GENBAMEMO_HYOUDAI))]
    public interface IM_GENBAMEMO_HYOUDAIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_GENBAMEMO_HYOUDAI")]
        M_GENBAMEMO_HYOUDAI[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Genbamemo_Hyoudai.IM_GENBAMEMO_HYOUDAIDao_GetAllValidData.sql")]
        M_GENBAMEMO_HYOUDAI[] GetAllValidData(M_GENBAMEMO_HYOUDAI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_GENBAMEMO_HYOUDAI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_GENBAMEMO_HYOUDAI data);

        int Delete(M_GENBAMEMO_HYOUDAI data);

        [Sql("select M_GENBAMEMO_HYOUDAI.GENBAMEMO_HYOUDAI_CD AS CD,M_GENBAMEMO_HYOUDAI.GENBAMEMO_HYOUDAI_NAME AS NAME FROM M_GENBAMEMO_HYOUDAI /*$whereSql*/ group by M_GENBAMEMO_HYOUDAI.GENBAMEMO_HYOUDAI_CD,M_GENBAMEMO_HYOUDAI.GENBAMEMO_HYOUDAI_NAME")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_GENBAMEMO_HYOUDAI data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("GENBAMEMO_HYOUDAI_CD = /*cd*/")]
        M_GENBAMEMO_HYOUDAI GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_GENBAMEMO_HYOUDAI data, bool deletechuFlg);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
