using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_WORK_CLOSED_SHARYOU))]
    public interface IM_WORK_CLOSED_SHARYOUDao : IS2Dao
    {

        [Sql("SELECT * FROM M_WORK_CLOSED_SHARYOU")]
        M_WORK_CLOSED_SHARYOU[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.WorkClosedSharyou.IM_WORK_CLOSED_SHARYOUDao_GetAllValidData.sql")]
        M_WORK_CLOSED_SHARYOU[] GetAllValidData(M_WORK_CLOSED_SHARYOU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_WORK_CLOSED_SHARYOU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_WORK_CLOSED_SHARYOU data);

        int Delete(M_WORK_CLOSED_SHARYOU data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_WORK_CLOSED_SHARYOU data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_WORK_CLOSED_SHARYOU data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);
    }
}
