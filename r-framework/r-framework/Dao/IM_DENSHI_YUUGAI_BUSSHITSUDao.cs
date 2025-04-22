using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_DENSHI_YUUGAI_BUSSHITSU))]
    public interface IM_DENSHI_YUUGAI_BUSSHITSUDao : IS2Dao
    {

        [Sql("SELECT * FROM M_DENSHI_YUUGAI_BUSSHITSU")]
        M_DENSHI_YUUGAI_BUSSHITSU[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_YUUGAI_BUSSHITSU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_YUUGAI_BUSSHITSU data);

        int Delete(M_DENSHI_YUUGAI_BUSSHITSU data);

        DataTable GetDataBySqlFile(string path, M_DENSHI_YUUGAI_BUSSHITSU data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_DENSHI_YUUGAI_BUSSHITSU data,bool deletechuFlg);

        /// <summary>
        /// 最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(YUUGAI_BUSSHITSU_CD),0)+1 FROM M_DENSHI_YUUGAI_BUSSHITSU WHERE ISNUMERIC(YUUGAI_BUSSHITSU_CD) = 1")]
        int GetMaxPlusKey();

        /// 情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.DenshiYuugaiBusshitsu.IM_DENSHI_YUUGAI_BUSSHITSUDao_GetAllValidData.sql")]
        M_DENSHI_YUUGAI_BUSSHITSU[] GetAllValidData(M_DENSHI_YUUGAI_BUSSHITSU data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("YUUGAI_BUSSHITSU_CD = /*cd*/")]
        M_DENSHI_YUUGAI_BUSSHITSU GetDataByCd(string cd);

    }
}
