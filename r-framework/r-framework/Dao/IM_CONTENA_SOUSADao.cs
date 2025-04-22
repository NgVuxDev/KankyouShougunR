using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_CONTENA_SOUSA))]
    public interface IM_CONTENA_SOUSADao : IS2Dao
    {

        [Sql("SELECT * FROM M_CONTENA_SOUSA")]
        M_CONTENA_SOUSA[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ContenaSousa.IM_CONTENA_SOUSADao_GetAllValidData.sql")]
        M_CONTENA_SOUSA[] GetAllValidData(M_CONTENA_SOUSA data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CONTENA_SOUSA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CONTENA_SOUSA data);

        int Delete(M_CONTENA_SOUSA data);

        [Sql("select M_CONTENA_SOUSA.CONTENA_SOUSA_CD AS CD,M_CONTENA_SOUSA.CONTENA_SOUSA_NAME_RYAKU AS NAME FROM M_CONTENA_SOUSA /*$whereSql*/ group by M_CONTENA_SOUSA.CONTENA_SOUSA_CD,M_CONTENA_SOUSA.CONTENA_SOUSA_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_CONTENA_SOUSA data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("CONTENA_SOUSA_CD = /*cd*/")]
        M_CONTENA_SOUSA GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_CONTENA_SOUSA data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);
    }
}
