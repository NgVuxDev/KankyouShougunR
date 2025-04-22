using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Data;

namespace r_framework.Dao
{
    [Bean(typeof(M_BIKO_UCHIWAKE_NYURYOKU))]
    public interface IM_BIKO_UCHIWAKE_NYURYOKUDao : IS2Dao
    {
        [Sql("SELECT * FROM M_BIKO_UCHIWAKE_NYURYOKU")]
        M_BIKO_UCHIWAKE_NYURYOKU[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.BikoUchiwakeNyuryoku.IM_BIKO_UCHIWAKE_NYURYOKUDao_GetAllValidData.sql")]
        M_BIKO_UCHIWAKE_NYURYOKU[] GetAllValidData(M_BIKO_UCHIWAKE_NYURYOKU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_BIKO_UCHIWAKE_NYURYOKU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_BIKO_UCHIWAKE_NYURYOKU data);

        int Delete(M_BIKO_UCHIWAKE_NYURYOKU data);

        [Sql("select M_BIKO_UCHIWAKE_NYURYOKU.BIKO_CD AS CD,M_BIKO_UCHIWAKE_NYURYOKU.BIKO_NOTE AS NOTE FROM M_BIKO_UCHIWAKE_NYURYOKU /*$whereSql*/ group by M_BIKO_UCHIWAKE_NYURYOKU.BIKO_CD,M_BIKO_UCHIWAKE_NYURYOKU.BIKO_NOTE")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_BIKO_UCHIWAKE_NYURYOKU data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">品名データ</param>
        /// <returns></returns>
        [Sql("SELECT DISTINCT N'見積入力' AS NAME FROM T_MITSUMORI_ENTRY WHERE BIKO_KBN_CD IN /*BIKO_KBN_CD*/('') AND DELETE_FLG = 'False'")]
        DataTable GetDataBySqlFileCheck(string[] BIKO_KBN_CD);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("BIKO_KBN_CD = /*data.BIKO_KBN_CD*/ AND BIKO_CD = /*data.BIKO_CD*/")]
        M_BIKO_UCHIWAKE_NYURYOKU GetDataByCd(M_BIKO_UCHIWAKE_NYURYOKU data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_BIKO_UCHIWAKE_NYURYOKU data); //, bool deletechuFlg
    }
}