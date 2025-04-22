using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 銀行支店マスタのDaoクラス
    /// </summary>
    [Bean(typeof(M_BANK_SHITEN))]
    public interface IM_BANK_SHITENDao : MasterAccess.Base.IMasterAccessDao<M_BANK_SHITEN>
    {
        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>]
        [Sql("SELECT * FROM M_BANK_SHITEN")]
        M_BANK_SHITEN[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.BankShiten.IM_BANK_SHITENDao_GetAllValidData.sql")]
        M_BANK_SHITEN[] GetAllValidData(M_BANK_SHITEN data);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する(マスタ共通ポップアップ)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.BankShiten.IM_BANK_SHITENDao_GetAllValidDataForPopUp.sql")]
        DataTable GetAllValidDataForPopUp(M_BANK_SHITEN data);

        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_BANK_SHITEN data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_BANK_SHITEN data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_BANK_SHITEN data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_BANK_SHITEN data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">銀行支店データ</param>
        /// <returns></returns>
        //20211108 UPD ST Nakayama #157047
        //DataTable GetDataBySqlFileCheck(string path, string BANK_CD, string[] BANK_SHITEN_CD);
        DataTable GetDataBySqlFileCheck(string path, string BANK_CD, string BANK_SHITEN_CD, string BANK_KOUZA_NO);
        //20211108 UPD ED Nakayama #157047

        /// <summary>
        /// 銀行支店コードをもとに部署のデータを取得する
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>取得したデータ</returns>
        [Query("BANK_CD = /*data.BANK_CD*/ and BANK_SHITEN_CD = /*data.BANK_SHITEN_CD*/ and KOUZA_NO = /*data.KOUZA_NO*/")]
        M_BANK_SHITEN GetDataByCd(M_BANK_SHITEN data);

        [Sql("select M_BANK_SHITEN.BANK_SHITEN_CD as CD,M_BANK_SHITEN.BANK_SHIETN_NAME_RYAKU as NAME FROM M_BANK_SHITEN /*$whereSql*/ group by M_BANK_SHITEN.BANK_SHITEN_CD,M_BANK_SHITEN.BANK_SHIETN_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_BANK_SHITEN data, bool deletechuFlg);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}