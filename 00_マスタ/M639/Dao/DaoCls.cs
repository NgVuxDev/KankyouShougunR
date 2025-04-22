using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.UnchiHinmeiHoshu.Dao
{
    [Bean(typeof(M_UNCHIN_HINMEI))]
    public interface DaoCls : IS2Dao
    {
        [Sql("SELECT * FROM M_UNCHIN_HINMEI")]
        M_UNCHIN_HINMEI[] GetAllData();

        [Sql("SELECT UNCHIN_HINMEI_CD FROM M_UNCHIN_HINMEI")]
        M_UNCHIN_HINMEI[] GetAllKeys();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Hinmei.IM_UNCHIN_HINMEIDao_GetAllValidData.sql")]
        M_UNCHIN_HINMEI[] GetAllValidData(M_UNCHIN_HINMEI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_UNCHIN_HINMEI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_UNCHIN_HINMEI data);

        int Delete(M_UNCHIN_HINMEI data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.UnchiHinmeiHoshu.Sql.GetHinmeiDataSql.sql")]
        DataTable GetDataBySqlFile(M_UNCHIN_HINMEI data);
        
        [SqlFile("Shougun.Core.Master.UnchiHinmeiHoshu.Sql.GetHinmeiDataMinColsSql.sql")]
        DataTable GetDataMinColsBySqlFile(M_UNCHIN_HINMEI data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("UNCHIN_HINMEI_CD = /*cd*/")]
        M_UNCHIN_HINMEI GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.UnchiHinmeiHoshu.Sql.GetIchiranHinmeidataSql.sql")]
        DataTable GetIchiranDataSqlFile(M_UNCHIN_HINMEI data, bool deletechuFlg);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}