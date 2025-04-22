using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Data;

namespace r_framework.Dao
{
    /// <summary>
    /// 現場_デジタコ連携情報Dao
    /// </summary>
    [Bean(typeof(M_GENBA_DIGI))]
    public interface IM_GENBA_DIGIDao : IS2Dao
    {
        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_GENBA_DIGI data);

        /// <summary>
        /// Entityを元に更新処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Update(M_GENBA_DIGI data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_GENBA_DIGI data);

        /// <summary>
        /// 全データを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_GENBA_DIGI")]
        M_GENBA_DIGI[] GetAllData();

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// 業者CD、現場CDをもとにデータを取得する
        /// </summary>
        /// <param name="gyoushaCD">業者CD</param>
        /// <param name="genbaCD">現場CD</param>
        /// <returns>取得したデータ</returns>
        [Query("GYOUSHA_CD = /*gyoushaCD*/ and GENBA_CD = /*genbaCD*/")]
        M_GENBA_DIGI GetDataByCd(string gyoushaCD, string genbaCD);

        /// <summary>
        /// 地点IDをもとにデータを取得する
        /// </summary>
        /// <param name="pointId">地点ID</param>
        /// <returns>取得したデータ</returns>
        [Query("POINT_ID = /*pointId*/")]
        M_GENBA_DIGI[] GetDataByPointId(string pointId);

        /// <summary>
        /// 地点コードの最小の空き番を取得する
        /// </summary>
        /// <param name="data">nullを渡す</param>
        /// <returns>最小の空き番</returns>
        [SqlFile("r_framework.Dao.SqlFile.GenbaDigi.IM_GENBA_DIGIDao_GetMinBlankNo.sql")]
        int GetMinBlankNo(M_GENBA_DIGI data);

        /// <summary>
        /// 地点コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(POINT_ID),0)+1 FROM M_GENBA_DIGI WHERE ISNUMERIC(POINT_ID) = 1")]
        int GetMaxPlusKey();

        /// <summary>
        /// 地点コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT POINT_ID FROM M_GENBA_DIGI WHERE ISNUMERIC(POINT_ID) = 1")]
        M_GENBA_DIGI[] GetDateByChokuchiKbn1();
    }
}
