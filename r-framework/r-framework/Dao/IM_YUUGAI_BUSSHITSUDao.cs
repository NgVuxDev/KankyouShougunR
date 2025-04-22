using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_YUUGAI_BUSSHITSU))]
    public interface IM_YUUGAI_BUSSHITSUDao : IS2Dao
    {

        [Sql("SELECT * FROM M_YUUGAI_BUSSHITSU")]
        M_YUUGAI_BUSSHITSU[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.YuugaiBusshitsu.IM_YUUGAI_BUSSHITSUDao_GetAllValidData.sql")]
        M_YUUGAI_BUSSHITSU[] GetAllValidData(M_YUUGAI_BUSSHITSU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_YUUGAI_BUSSHITSU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_YUUGAI_BUSSHITSU data);

        int Delete(M_YUUGAI_BUSSHITSU data);

        [Sql("select M_YUUGAI_BUSSHITSU.YUUGAI_BUSSHITSU_CD AS CD,M_YUUGAI_BUSSHITSU.YUUGAI_BUSSHITSU_NAME_RYAKU AS NAME FROM M_YUUGAI_BUSSHITSU /*$whereSql*/ group by M_YUUGAI_BUSSHITSU.YUUGAI_BUSSHITSU_CD,M_YUUGAI_BUSSHITSU.YUUGAI_BUSSHITSU_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_YUUGAI_BUSSHITSU data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("YUUGAI_BUSSHITSU_CD = /*cd*/")]
        M_YUUGAI_BUSSHITSU GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_YUUGAI_BUSSHITSU data, bool deletechuFlg);
    }
}
