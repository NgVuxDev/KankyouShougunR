using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 単位マスタDao
    /// </summary>
    [Bean(typeof(M_GURUPU_NYURYOKU))]
    public interface IM_GURUPU_NYURYOKUDao : IS2Dao
    {
        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_GURUPU_NYURYOKU")]
        M_GURUPU_NYURYOKU[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.GurupuNyuryoku.IM_GURUPU_NYURYOKUDao_GetAllValidData.sql")]
        M_GURUPU_NYURYOKU[] GetAllValidData(M_GURUPU_NYURYOKU data);

        //20250319
        [SqlFile("r_framework.Dao.SqlFile.GurupuNyuryoku.IM_GURUPU_URIAGEDao_GetAllValidData.sql")]
        M_GURUPU_NYURYOKU[] GetAllValidDataUriage(M_GURUPU_NYURYOKU data);

        [SqlFile("r_framework.Dao.SqlFile.GurupuNyuryoku.IM_GURUPU_SHIHARAIDao_GetAllValidData.sql")]
        M_GURUPU_NYURYOKU[] GetAllValidDataShiharai(M_GURUPU_NYURYOKU data);

        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam> 
        [NoPersistentProps("TIME_STAMP", "GURUPU_ID")]  //20250324
        int Insert(M_GURUPU_NYURYOKU data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "TIME_STAMP")] //, "CREATE_PC"
        int Update(M_GURUPU_NYURYOKU data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_GURUPU_NYURYOKU data);

        [Sql("select right('000' + convert(varchar, M_GURUPU_NYURYOKU.GURUPU_CD), 3) AS CD,M_GURUPU_NYURYOKU.GURUPU_NAME AS NAME FROM M_GURUPU_NYURYOKU /*$whereSql*/ group by M_GURUPU_NYURYOKU.GURUPU_CD,M_GURUPU_NYURYOKU.GURUPU_NAME")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_GURUPU_NYURYOKU data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] GURUPU_CD);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("GURUPU_CD = /*cd*/")]
        M_GURUPU_NYURYOKU GetDataByCd(string cd);

        //20250321
        [Query("GURUPU_CD = /*cd*/ AND DENPYOU_KBN_CD = /*den_cd*/")]
        M_GURUPU_NYURYOKU GetDataByCdAndDencd(string cd, int den_cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_GURUPU_NYURYOKU data, bool deletechuFlg);
    }
}