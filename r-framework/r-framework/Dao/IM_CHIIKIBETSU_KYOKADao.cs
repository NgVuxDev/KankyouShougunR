using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_CHIIKIBETSU_KYOKA))]
    public interface IM_CHIIKIBETSU_KYOKADao : IS2Dao
    {
        
        [Sql("SELECT * FROM M_CHIIKIBETSU_KYOKA")]
        M_CHIIKIBETSU_KYOKA[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ChiikibetsuKyoka.IM_CHIIKIBETSU_KYOKADao_GetAllValidData.sql")]
        M_CHIIKIBETSU_KYOKA[] GetAllValidData(M_CHIIKIBETSU_KYOKA data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CHIIKIBETSU_KYOKA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CHIIKIBETSU_KYOKA data);

        int Delete(M_CHIIKIBETSU_KYOKA data);

        /// <summary>
        /// 主キーをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("KYOKA_KBN = /*data.KYOKA_KBN*/ AND GYOUSHA_CD = /*data.GYOUSHA_CD*/ AND GENBA_CD = /*data.GENBA_CD*/ AND CHIIKI_CD = /*data.CHIIKI_CD*/")]
        M_CHIIKIBETSU_KYOKA GetDataByPrimaryKey(M_CHIIKIBETSU_KYOKA data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_CHIIKIBETSU_KYOKA data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_CHIIKIBETSU_KYOKA data, bool deletechuFlg);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="kyokashoKbn">許可証区分</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFileForKigenKanri(string path, M_CHIIKIBETSU_KYOKA data, string kyokashoKbn);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        DataTable GetDataForStringSql(string sql);
    }
}
