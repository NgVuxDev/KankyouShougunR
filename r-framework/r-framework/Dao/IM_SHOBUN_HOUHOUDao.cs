using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_SHOBUN_HOUHOU))]
    public interface IM_SHOBUN_HOUHOUDao : IS2Dao
    {

        [Sql("SELECT * FROM M_SHOBUN_HOUHOU")]
        M_SHOBUN_HOUHOU[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ShobunHouhou.IM_SHOBUN_HOUHOUDao_GetAllValidData.sql")]
        M_SHOBUN_HOUHOU[] GetAllValidData(M_SHOBUN_HOUHOU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SHOBUN_HOUHOU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SHOBUN_HOUHOU data);

        int Delete(M_SHOBUN_HOUHOU data);

        [Sql("select M_SHOBUN_HOUHOU.SHOBUN_HOUHOU_CD AS CD,M_SHOBUN_HOUHOU.SHOBUN_HOUHOU_NAME_RYAKU AS NAME FROM M_SHOBUN_HOUHOU /*$whereSql*/ group by M_SHOBUN_HOUHOU.SHOBUN_HOUHOU_CD,M_SHOBUN_HOUHOU.SHOBUN_HOUHOU_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SHOBUN_HOUHOU data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] SHOBUN_HOUHOU_CD);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("SHOBUN_HOUHOU_CD = /*cd*/")]
        M_SHOBUN_HOUHOU GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_SHOBUN_HOUHOU data, bool deletechuFlg);
    }
}
