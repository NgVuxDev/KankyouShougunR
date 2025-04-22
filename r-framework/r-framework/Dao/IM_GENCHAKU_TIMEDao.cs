using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_GENCHAKU_TIME))]
    public interface IM_GENCHAKU_TIMEDao : IS2Dao
    {

        [Sql("SELECT * FROM M_GENCHAKU_TIME")]
        M_GENCHAKU_TIME[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.GenchakuTime.IM_GENCHAKU_TIMEDao_GetAllValidData.sql")]
        M_GENCHAKU_TIME[] GetAllValidData(M_GENCHAKU_TIME data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_GENCHAKU_TIME data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_GENCHAKU_TIME data);

        int Delete(M_GENCHAKU_TIME data);

        [Sql("select RIGHT('000'+ CONVERT(nvarchar,M_GENCHAKU_TIME.GENCHAKU_TIME_CD), 3) AS CD,M_GENCHAKU_TIME.GENCHAKU_TIME_NAME_RYAKU AS NAME FROM M_GENCHAKU_TIME /*$whereSql*/ group by M_GENCHAKU_TIME.GENCHAKU_TIME_CD,M_GENCHAKU_TIME.GENCHAKU_TIME_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_GENCHAKU_TIME data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("GENCHAKU_TIME_CD = /*cd*/")]
        M_GENCHAKU_TIME GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_GENCHAKU_TIME data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);
    }
}
