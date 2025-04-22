using System.Data;
using System.Data.SqlTypes;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_KOBETSU_HINMEI))]
    public interface IM_KOBETSU_HINMEIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_KOBETSU_HINMEI")]
        M_KOBETSU_HINMEI[] GetAllData();

        [SqlFile("r_framework.Dao.SqlFile.KobetsuHinmei.IM_KOBETSU_HINMEIDao_GetDataByCd.sql")]
        M_KOBETSU_HINMEI GetDataByCd(M_KOBETSU_HINMEI data);

        [SqlFile("r_framework.Dao.SqlFile.KobetsuHinmei.IM_KOBETSU_HINMEIDao_GetDataByHinmei.sql")]
        M_KOBETSU_HINMEI GetDataByHinmei(M_KOBETSU_HINMEI data, M_HINMEI hinmei);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.KobetsuHinmei.IM_KOBETSU_HINMEIDao_GetAllValidData.sql")]
        M_KOBETSU_HINMEI[] GetAllValidData(M_KOBETSU_HINMEI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KOBETSU_HINMEI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KOBETSU_HINMEI data);

        int Delete(M_KOBETSU_HINMEI data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_KOBETSU_HINMEI data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_KOBETSU_HINMEI data, bool deletechuFlg);
    }
}
