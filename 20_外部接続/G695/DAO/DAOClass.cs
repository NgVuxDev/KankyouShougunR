using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ExternalConnection.HaisouKeikakuNyuuryoku
{
    /// <summary>
    /// 配送計画DAO
    /// </summary>
    [Bean(typeof(T_LOGI_DELIVERY))]
    public interface LogiDeliveryDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_LOGI_DELIVERY data);

        /// <summary>
        /// Entityを元に更新処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_LOGI_DELIVERY data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_LOGI_DELIVERY data);

        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ AND DELETE_FLG = 0")]
        T_LOGI_DELIVERY GetDataByCd(long systemId);

    }

    /// <summary>
    /// 配送計画明細DAO
    /// </summary>
    [Bean(typeof(T_LOGI_DELIVERY_DETAIL))]
    public interface LogiDeliveryDetailDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_LOGI_DELIVERY_DETAIL data);

        /// <summary>
        /// Entityを元に更新処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Update(T_LOGI_DELIVERY_DETAIL data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_LOGI_DELIVERY_DETAIL data);

        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ AND DELETE_FLG = 0")]
        List<T_LOGI_DELIVERY_DETAIL> GetDataBySystemId(long systemId);

    }

    /// <summary>
    /// 配送計画連携状況管理DAO
    /// </summary>
    [Bean(typeof(T_LOGI_LINK_STATUS))]
    public interface LogiLinkStatusDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_LOGI_LINK_STATUS data);

        /// <summary>
        /// Entityを元に更新処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_LOGI_LINK_STATUS data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_LOGI_LINK_STATUS data);

        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ AND DELETE_FLG = 0")]
        T_LOGI_LINK_STATUS GetDataByCd(long systemId);
    }

    /// <summary>
    /// 配送計画候補DAO
    /// </summary>
    [Bean(typeof(DeliveryPlanDTO))]
    public interface DeliveryPlanDAO : IS2Dao
    {
        /// <summary>
        /// 配送計画候補（定期配車）データを取得
        /// </summary>
        /// <param name="data">DTOClass</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.HaisouKeikakuNyuuryoku.Sql.GetDeliveryPlanForTeiki.sql")]
        List<DeliveryPlanDTO> GetDeliveryPlanForTeiki(SearchDeliveryPlanDTO data);

        /// <summary>
        /// 配送計画候補（スポット）データを取得
        /// </summary>
        /// <param name="data">DTOClass</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.HaisouKeikakuNyuuryoku.Sql.GetDeliveryPlanForSpot.sql")]
        List<DeliveryPlanDTO> GetDeliveryPlanForSpot(SearchDeliveryPlanDTO data);
    }

    /// <summary>
    /// 配送データDAO
    /// </summary>
    [Bean(typeof(DeliveryDataDTO))]
    public interface DeliveryDataDAO : IS2Dao
    {
        /// <summary>
        /// 配送（定期配車）データを取得
        /// </summary>
        /// <param name="selectData"></param>
        /// <param name="searchData"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.HaisouKeikakuNyuuryoku.Sql.GetDeliveryDataForTeiki.sql")]
        List<DeliveryDataDTO> GetDeliveryDataForTeiki(DeliveryPlanDTO selectData, SearchDeliveryPlanDTO searchData);

        /// <summary>
        /// 配送（スポット）データを取得
        /// </summary>
        /// <param name="selectData"></param>
        /// <param name="searchData"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.HaisouKeikakuNyuuryoku.Sql.GetDeliveryDataForSpot.sql")]
        List<DeliveryDataDTO> GetDeliveryDataForSpot(DeliveryPlanDTO selectData, SearchDeliveryPlanDTO searchData);

        /// <summary>
        /// 配送（システムID）データを取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.HaisouKeikakuNyuuryoku.Sql.GetDeliveryDataForSystemId.sql")]
        List<DeliveryDataDTO> GetDeliveryDataForSystemId(string systemId);

    }

    /// <summary>
    /// リクエスト送信DAO
    /// </summary>
    [Bean(typeof(DeliveryDataDTO))]
    public interface DeliveryDataRequestDao : IS2Dao
    {
        [SqlFile("Shougun.Core.ExternalConnection.HaisouKeikakuNyuuryoku.Sql.GetDeliveryDataRequestForTeiki.sql")]
        DataTable GetRequestDataTeikiForSql(SqlInt64 SysId, SqlInt64 SysDetId);

        [SqlFile("Shougun.Core.ExternalConnection.HaisouKeikakuNyuuryoku.Sql.GetDeliveryDataRequestForSpot.sql")]
        DataTable GetRequestDataSpotForSql(SqlInt64 SysId);

        /// <summary>
        /// 収集受付入力を取得します
        /// </summary>
        /// <param name="uketsukeNumber"></param>
        [Sql("SELECT * FROM T_UKETSUKE_SS_ENTRY WHERE UKETSUKE_NUMBER = /*uketsukeNumber*/ AND DELETE_FLG = 0 ")]
        DataTable GetUketsukeSS(SqlInt64 uketsukeNumber);

        /// <summary>
        /// 出荷受付入力を取得します
        /// </summary>
        /// <param name="uketsukeNumber"></param>
        [Sql("SELECT * FROM T_UKETSUKE_SK_ENTRY WHERE UKETSUKE_NUMBER = /*uketsukeNumber*/ AND DELETE_FLG = 0 ")]
        DataTable GetUketsukeSK(SqlInt64 uketsukeNumber);

        [Sql("/*$sql*/")]
        DataTable CheckForStringSql(string sql);
    }
}
