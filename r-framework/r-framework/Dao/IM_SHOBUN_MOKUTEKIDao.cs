using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_SHOBUN_MOKUTEKI))]
    public interface IM_SHOBUN_MOKUTEKIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_SHOBUN_MOKUTEKI")]
        M_SHOBUN_MOKUTEKI[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ShobunMokuteki.IM_SHOBUN_MOKUTEKIDao_GetAllValidData.sql")]
        M_SHOBUN_MOKUTEKI[] GetAllValidData(M_SHOBUN_MOKUTEKI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SHOBUN_MOKUTEKI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SHOBUN_MOKUTEKI data);

        int Delete(M_SHOBUN_MOKUTEKI data);

        [Sql("select M_SHOBUN_MOKUTEKI.SHOBUN_MOKUTEKI_CD AS CD,M_SHOBUN_MOKUTEKI.SHOBUN_MOKUTEKI_NAME_RYAKU AS NAME FROM M_SHOBUN_MOKUTEKI /*$whereSql*/ group by M_SHOBUN_MOKUTEKI.SHOBUN_MOKUTEKI_CD,M_SHOBUN_MOKUTEKI.SHOBUN_MOKUTEKI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SHOBUN_MOKUTEKI data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("SHOBUN_MOKUTEKI_CD = /*cd*/")]
        M_SHOBUN_MOKUTEKI GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_SHOBUN_MOKUTEKI data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);
    }
}
