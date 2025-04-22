// $Id: IT_TEIKI_HAISHA_ENTRYDao.cs 36292 2014-12-02 02:43:29Z fangjk@oec-h.com $
using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using r_framework.Dao;
using System.Data.SqlTypes;
namespace Shougun.Core.Allocation.CarTransferTeiki
{
    /// <summary>
    /// 定期配車マスタDao
    /// </summary>
    [Bean(typeof(T_TEIKI_HAISHA_ENTRY))]
    public interface IT_TEIKI_HAISHA_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM T_TEIKI_HAISHA_ENTRY")]
        T_TEIKI_HAISHA_ENTRY[] GetAllData();

        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        int Delete(T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// 配車番号により、定期配車情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferTeiki.Sql.GetAllValidDataByHaishaNumber.sql")]
        T_TEIKI_HAISHA_ENTRY[] GetAllValidDataByHaishaNumber(T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferTeiki.Sql.GetAllValidData.sql")]
        T_TEIKI_HAISHA_ENTRY[] GetAllValidData(T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// 定期配車検索ポップアップ画面用のデータを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferTeiki.Sql.GetAllDataSql.sql")]
        new DataTable GetAllTeikiData(DTOClass data);

        /// <summary>
        /// モバイル連携可能かチェックし、データを取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferTeiki.Sql.GetDetailForMiTourokuHaisha.sql")]
        DataTable GetDataToMRDataTable(DTOClass data);

        /// <summary>
        /// 定期配車荷降行取得
        /// </summary>
        /// <param name="HAISHA_DENPYOU_NO">HAISHA_DENPYOU_NO</param>
        /// <param name="NIOROSHI_NUMBER">NIOROSHI_NUMBER</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferTeiki.Sql.GetMobilNioroshiData.sql")]
        DataTable GetMobilNioroshiData(SqlInt64 TEIKI_HAISHA_NUMBER, int NIOROSHI_NUMBER);

        /// <summary>
        /// 定期配車荷降データを取得
        /// </summary>
        /// <param name="SYSTEM_ID">SYSTEM_ID</param>
        /// <param name="SEQ">SEQ</param>
        /// <param name="NIOROSHI_NUMBER">NIOROSHI_NUMBER</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferTeiki.Sql.GetTeikiHaishaNioroshiData.sql")]
        DataTable GetTeikiHaishaNioroshiData(SqlInt64 SYSTEM_ID, SqlInt32 SEQ, SqlInt32 NIOROSHI_NUMBER);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="whereSql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        System.Data.DataTable GetDateForStringSql(string sql);
    }
}
