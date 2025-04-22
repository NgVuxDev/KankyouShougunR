using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using r_framework.Dao;

namespace Shougun.Core.Master.ShikuchousonHoshu.Dao
{
    [Bean(typeof(M_SHIKUCHOUSON))]
    public interface DaoCls : IS2Dao
    {

        [Sql("SELECT * FROM M_SHIKUCHOUSON")]
        M_SHIKUCHOUSON[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Shikuchouson.IM_SHIKUCHOUSONDao_GetAllValidData.sql")]
        M_SHIKUCHOUSON[] GetAllValidData(M_SHIKUCHOUSON data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SHIKUCHOUSON data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC","TIME_STAMP")]
        int Update(M_SHIKUCHOUSON data);

        int Delete(M_SHIKUCHOUSON data);

        [Sql("select M_SHIKUCHOUSON.SHIKUCHOUSON_CD AS CD,M_SHIKUCHOUSON.SHIKUCHOUSON_NAME_RYAKU AS NAME FROM M_SHIKUCHOUSON /*$whereSql*/ group by  M_SHIKUCHOUSON.SHIKUCHOUSON_CD,M_SHIKUCHOUSON.SHIKUCHOUSON_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SHIKUCHOUSON data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="data">市区町村データ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ShikuchousonHoshu.Sql.CheckDeleteShikuchousonSql.sql")]
        DataTable GetDataBySqlFileCheck(string[] SHIKUCHOUSON_CD);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("SHIKUCHOUSON_CD = /*cd*/")]
        M_SHIKUCHOUSON GetDataByCd(string cd);

        /// <summary>
        /// 市区町村画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        // [SqlFile("Shougun.Core.Master.ShikuchousonHoshu.Sql.GetIchiranDataSql.sql")]
        [SqlFile("Shougun.Core.Master.ShikuchousonHoshu.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_SHIKUCHOUSON data, bool deletechuFlg);
    }
}
