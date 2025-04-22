using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.UnchinTankaHoshu.Dao
{
    [Bean(typeof(M_UNCHIN_TANKA))]
    public interface IM_UNCHIN_TANKADao : IS2Dao
    {
        [Sql("SELECT * FROM M_UNCHIN_TANKA")]
        M_UNCHIN_TANKA[] GetAllData();

        [Sql("SELECT UNCHIN_HINMEI_CD, UNIT_CD, SHASHU_CD FROM M_UNCHIN_TANKA")]
        M_UNCHIN_TANKA[] GetAllKeys();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Hinmei.IM_UNCHIN_HINMEIDao_GetAllValidData.sql")]
        M_UNCHIN_TANKA[] GetAllValidData(M_UNCHIN_TANKA data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_UNCHIN_TANKA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_UNCHIN_TANKA data);

        int Delete(M_UNCHIN_TANKA data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.UnchinTankaHoshu.Sql.GetDataBySqlFile.sql")]
        M_UNCHIN_TANKA GetDataBySqlFile(M_UNCHIN_TANKA data);

        /// <summary>
        /// 運搬業者コードを元に運賃単価データを取得する
        /// </summary>
        /// <parameparam name="unpanGyoushaCd">運搬業者コード</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Master.UnchinTankaHoshu.Sql.GetDataByUnpanGyoushaCd.sql")]
        DataTable GetDataByUnpanGyoushaCd(M_UNCHIN_TANKA data);

        [SqlFile("Shougun.Core.Master.UnchinTankaHoshu.Sql.GetDataByUnpanGyoushaCdMinCols.sql")]
        DataTable GetDataByUnpanGyoushaCdMinCols(M_UNCHIN_TANKA data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns>取得したデータ</returns>
        [Query("UNPAN_GYOUSHA_CD = /*data.UNPAN_GYOUSHA_CD*/ AND UNCHIN_HINMEI_CD = /*data.UNCHIN_HINMEI_CD*/ AND UNIT_CD = /*data.UNIT_CD*/ AND SHASHU_CD = /*data.SHASHU_CD*/")]
        M_UNCHIN_TANKA GetDataByCd(M_UNCHIN_TANKA data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.UnchinTankaHoshu.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSqlFile(M_UNCHIN_TANKA data, bool deletechuFlg);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// ユーザ指定の更新条件によるデータ更新
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <param name="updateKey"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.UnchinTankaHoshu.Sql.UpdateUnchinTankaDataSql.sql")]
        int UpdateBySqlFile(M_UNCHIN_TANKA data, M_UNCHIN_TANKA updateKey);
    }
}