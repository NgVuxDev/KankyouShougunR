using System;
using System.Data;
using System.Data.SqlTypes;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Collections.Generic;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ExternalConnection.HaisouKeikakuTeiki
{
    /// <summary>
    /// コース用
    /// </summary>
    [Bean(typeof(M_COURSE))]
    public interface DAOClass : IS2Dao
    {
        [Sql("/*$sql*/")]
        int ExecuteForStringSql(string sql);

        [SqlFile("Shougun.Core.ExternalConnection.HaisouKeikakuTeiki.Sql.GetCourseDetail.sql")]
        DataTable GetCourseDetail(SqlInt32 dayCd, string courseNameCd);

        [SqlFile("Shougun.Core.ExternalConnection.HaisouKeikakuTeiki.Sql.GetTeikiDetail.sql")]
        DataTable GetTeikiDetail(SqlInt64 system_id, SqlInt32 seq);

        /// <summary>
        /// コースマスタ抽出
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.HaisouKeikakuTeiki.Sql.GetNaviCourseData.sql")]
        DataTable GetNaviCourseData(SearchDto data);

        /// <summary>
        /// 定期配車抽出
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.HaisouKeikakuTeiki.Sql.GetNaviTeikiData.sql")]
        DataTable GetNaviTeikiData(SearchDto data);

    }

    /// <summary>
    /// 配車計画用一時テーブルDAO
    /// </summary>
    [Bean(typeof(T_NAVI_COLLABORATION_EVENTS))]
    public interface NaviCollaboEventDAO : IS2Dao
    {
        [Sql("select SHAIN_CD from T_NAVI_COLLABORATION_EVENTS WHERE SHAIN_CD = /*userCd*/")]
        string GetUserCd(string userCd);
    }
    /// <summary>
    /// 配車計画DAO
    /// </summary>
    [Bean(typeof(T_NAVI_DELIVERY))]
    public interface NaviDeliveryDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_NAVI_DELIVERY data);

        /// <summary>
        /// Entityを元に更新処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_NAVI_DELIVERY data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_NAVI_DELIVERY data);

        [Sql("/*$sql*/")]
        DataTable getdateforstringsql(string sql);

        /// <summary>
        /// 同一日付、同一作業者で削除されていないデータの件数を取得
        /// </summary>
        /// <param name="userCd"></param>
        /// <returns></returns>
        [Sql("SELECT MAX(ND.BIN_NO) FROM T_NAVI_DELIVERY ND INNER JOIN T_NAVI_LINK_STATUS NL ON ND.SYSTEM_ID = NL.SYSTEM_ID WHERE NL.LINK_STATUS != 3 AND ND.DELETE_FLG = 0 AND ND.SAGYOUSHA_CD = /*userCd*/ AND ND.DELIVERY_DATE = /*deliveryDate*/")]
        int GetDeliveryBinCnt(string userCd, SqlDateTime deliveryDate);

        /// <summary>
        /// データを取得する
        /// </summary>
        /// <param name="dayCd"></param>
        /// <param name="courseNameCd"></param>
        /// <param name="deliveryDate"></param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*system_id*/ AND DELETE_FLG = 0")]
        T_NAVI_DELIVERY GetDataByCourse(SqlInt64 system_id);

        /// <summary>
        /// 同一日付、同一作業者で登録済みデータのチェック(LINK_STATUS=3の取得までいったデータは除く)
        /// </summary>
        /// <param name="userCd"></param>
        /// <returns></returns>
        [Sql("SELECT COUNT(ND.SAGYOUSHA_CD) CNT FROM T_NAVI_DELIVERY ND INNER JOIN T_NAVI_LINK_STATUS NL ON ND.SYSTEM_ID = NL.SYSTEM_ID WHERE NL.LINK_STATUS != 3 AND ND.DELETE_FLG = 0 AND ND.SAGYOUSHA_CD = /*userCd*/ AND ND.DELIVERY_DATE = /*deliveryDate*/")]
        int GetDeliveryCnt(string userCd, SqlDateTime deliveryDate);
    }

    /// <summary>
    /// 配車計画連携状況管理DAO
    /// </summary>
    [Bean(typeof(T_NAVI_LINK_STATUS))]
    public interface NaviLinkStatusDAO : IS2Dao
    {
        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/")]
        T_NAVI_LINK_STATUS GetDataByCd(long systemId);

        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_NAVI_LINK_STATUS data);

        /// <summary>
        /// Entityを元に更新処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_NAVI_LINK_STATUS data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_NAVI_LINK_STATUS data);
    }
}
