using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// M_JISSEKI_BUNRUI Dao Class.
    /// </summary>
    [Bean(typeof(M_JISSEKI_BUNRUI))]
    public interface IM_JISSEKI_BUNRUIDao : IS2Dao
    {
        /// <summary>
        /// 全データ取得
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_JISSEKI_BUNRUI")]
        M_JISSEKI_BUNRUI[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.JissekiBunrui.IM_JISSEKI_BUNRUIDao_GetAllValidData.sql")]
        M_JISSEKI_BUNRUI[] GetAllValidData(M_JISSEKI_BUNRUI data);

        /// <summary>
        /// 挿入
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_JISSEKI_BUNRUI data);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_JISSEKI_BUNRUI data);

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(M_JISSEKI_BUNRUI data);

        /// <summary>
        /// 全データ取得用
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        [Sql("select M_JISSEKI_BUNRUI.JISSEKI_BUNRUI_CD AS CD, M_JISSEKI_BUNRUI.JISSEKI_BUNRUI_NAME_RYAKU AS NAME FROM M_JISSEKI_BUNRUI /*$whereSql*/ group by M_JISSEKI_BUNRUI.JISSEKI_BUNRUI_CD, M_JISSEKI_BUNRUI.JISSEKI_BUNRUI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("r_framework.Dao.SqlFile.JissekiBunrui.GetJissekiBunruiDataSql.sql")]
        DataTable GetJissekiBunruiData(M_JISSEKI_BUNRUI data);

        /// <summary>
        /// どのマスタで使用されているか他マスタを検索する
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">実績分類CD</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] JISSEKI_BUNRUI_CD);

        /// <summary>
        /// 実績分類コードをもとに実績分類のデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("JISSEKI_BUNRUI_CD = /*cd*/")]
        M_JISSEKI_BUNRUI GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        [SqlFile("r_framework.Dao.SqlFile.JissekiBunrui.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSqlFile(M_JISSEKI_BUNRUI data, bool deletechuFlg);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile2(string path, M_JISSEKI_BUNRUI data, bool deletechuFlg);
    }
}
