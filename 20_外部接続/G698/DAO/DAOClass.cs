using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ExternalConnection.CourseSaitekikaNyuuryoku
{
    /// <summary>
    /// コース最適化DAO
    /// </summary>
    [Bean(typeof(SaitekikaDTO))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// 曜日CDとコース名CDをもとにデータを取得する
        /// </summary>
        /// <param name="dayCd">曜日CD</param>
        /// <param name="courseNameCd">コース名CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.CourseSaitekikaNyuuryoku.Sql.GetCourseDetail.sql")]
        DataTable GetCourseDetail(SqlInt16 dayCd, string courseNameCd);

        /// <summary>
        /// 曜日CDとコース名CDをもとにデータを取得する
        /// </summary>
        /// <param name="dayCd">曜日CD</param>
        /// <param name="courseNameCd">コース名CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.CourseSaitekikaNyuuryoku.Sql.GetCourseDetail.sql")]
        List<SaitekikaDTO> GetCourseDetailDto(SqlInt16 dayCd, string courseNameCd);

        /// <summary>
        /// システムIDとSEQをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.CourseSaitekikaNyuuryoku.Sql.GetTeikiHaishaDetail.sql")]
        DataTable GetTeikiHaishaDetail(SqlInt64 systemId, SqlInt32 seq);

        /// <summary>
        /// システムIDとSEQをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.CourseSaitekikaNyuuryoku.Sql.GetTeikiHaishaDetail.sql")]
        List<SaitekikaDTO> GetTeikiHaishaDetailDto(SqlInt64 systemId, SqlInt32 seq);

        /// <summary>
        /// 業者CDと現場CDをもとに連携済みの現場情報を取得する
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.CourseSaitekikaNyuuryoku.Sql.GetOutputGenba.sql")]
        DataTable GetOutputGenba(string gyoushaCd, string genbaCd);

        /// <summary>
        /// 指定されたSQL文を実行する
        /// </summary>
        /// <param name="sql">sql文字列</param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        int ExecuteForStringSql(string sql);
    }

    /// <summary>
    /// 配送計画連携状況管理DAO
    /// </summary>
    [Bean(typeof(T_NAVI_LINK_STATUS))]
    public interface NaviLinkStatusDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元に更新処理を行う
        /// </summary>
        /// <param name="data">Entity</param>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_NAVI_LINK_STATUS data);

        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/")]
        T_NAVI_LINK_STATUS GetDataByCd(long systemId);
    }

    /// <summary>
    /// 配送計画DAO
    /// </summary>
    [Bean(typeof(T_NAVI_DELIVERY_DTO))]
    public interface NaviDeliveryDtoDAO : IS2Dao
    {
        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.ExternalConnection.CourseSaitekikaNyuuryoku.Sql.GetNaviDelivery.sql")]
        T_NAVI_DELIVERY_DTO GetDataByCd(long systemId);
    }

    /// <summary>
    /// 定期配車入力DAO
    /// </summary>
    [Bean(typeof(T_TEIKI_HAISHA_ENTRY))]
    public interface TeikiHaishaEntryDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <param name="data">Entity</param>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// Entityを元に更新処理を行う
        /// </summary>
        /// <param name="data">Entity</param>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ AND SEQ = /*seq*/ AND DELETE_FLG = 0")]
        T_TEIKI_HAISHA_ENTRY GetDataByCd(long systemId, int seq);
    }

    /// <summary>
    /// 定期配車明細DAO
    /// </summary>
    [Bean(typeof(T_TEIKI_HAISHA_DETAIL))]
    public interface TeikiHaishaDetailDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <param name="data">Entity</param>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_HAISHA_DETAIL data);

        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ AND SEQ = /*seq*/")]
        List<T_TEIKI_HAISHA_DETAIL> GetDataListBySeq(long systemId, int seq);
    }

    /// <summary>
    /// 定期配車詳細DAO
    /// </summary>
    [Bean(typeof(T_TEIKI_HAISHA_SHOUSAI))]
    public interface TeikiHaishaShousaiDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <param name="data">Entity</param>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_HAISHA_SHOUSAI data);

        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ AND SEQ = /*seq*/")]
        List<T_TEIKI_HAISHA_SHOUSAI> GetDataListBySeq(long systemId, int seq);
    }

    /// <summary>
    /// 定期配車荷卸DAO
    /// </summary>
    [Bean(typeof(T_TEIKI_HAISHA_NIOROSHI))]
    public interface TeikiHaishaNioroshiDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <param name="data">Entity</param>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_HAISHA_NIOROSHI data);

        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ AND SEQ = /*seq*/")]
        List<T_TEIKI_HAISHA_NIOROSHI> GetDataListBySeq(long systemId, int seq);
    }
}
