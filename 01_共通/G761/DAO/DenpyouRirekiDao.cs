using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Common.DenpyouRireki.DAO
{
    /// <summary>
    /// 取引先マスタDao
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI))]
    public interface IM_TorihikisakiDao : IS2Dao
    {
        /// <summary>
        /// 取引先コードをもとに取引先のデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRireki.Sql.IM_TorihikisakiDao_GetTorihikisakiData.sql")]
        M_TORIHIKISAKI GetTorihikisakiData(string torihikisakiCd);
    }

    /// <summary>
    /// 業者マスタDao
    /// </summary>
    [Bean(typeof(M_GYOUSHA))]
    public interface IM_GyoushaDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="gyoushaCd">業者コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.DenpyouRireki.Sql.IM_GyoushaDao_GetGyoushaData.sql")]
        DataTable GetDataBySqlFile2(string gyoushaCd);

        [SqlFile("Shougun.Core.Common.DenpyouRireki.Sql.IM_GyoushaDao_GetUpnGyoushaData.sql")]
        DataTable GetDataBySqlFileUpn(string gyoushaCd);
    }

    /// <summary>
    /// 現場マスタDao
    /// </summary>
    [Bean(typeof(M_GENBA))]
    public interface IM_GenbaDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.DenpyouRireki.Sql.IM_GenbaDao_GetGenbaData.sql")]
        DataTable GetDataBySqlFile2(M_GENBA data);

    }

    /// <summary>
    /// 受入Dao
    /// </summary>
    [Bean(typeof(T_UKEIRE_ENTRY))]
    public interface IT_UkeireDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <param name="GyoushaCd">業者コード</param>
        /// <param name="GenbaCd">現場コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.DenpyouRireki.Sql.IT_UkeireDao_GetUkeireData.sql")]
        DataTable GetDataBySqlFile(string torihikisakiCd, string gyoushaCd, string genbaCd, string kyotenCd, string fromDate, string toDate, string upnGyoushaCd, string sharyouCd, string sharyouName);
    }

    /// <summary>
    /// 受入明細Dao
    /// </summary>
    [Bean(typeof(T_UKEIRE_DETAIL))]
    public interface IT_UkeireDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.DenpyouRireki.Sql.IT_UkeireDetailDao_GetUkeireDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);
    }

    /// <summary>
    /// 出荷Dao
    /// </summary>
    [Bean(typeof(T_SHUKKA_ENTRY))]
    public interface IT_ShukkaDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <param name="GyoushaCd">業者コード</param>
        /// <param name="GenbaCd">現場コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.DenpyouRireki.Sql.IT_ShukkaDao_GetShukkaData.sql")]
        DataTable GetDataBySqlFile(string torihikisakiCd, string gyoushaCd, string genbaCd, string kyotenCd, string fromDate, string toDate, string upnGyoushaCd, string sharyouCd, string sharyouName);

        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得(検収データ)
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <param name="GyoushaCd">業者コード</param>
        /// <param name="GenbaCd">現場コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.DenpyouRireki.Sql.IT_ShukkaDao_GetShukkaKenshuuEntryData.sql")]
        DataTable GetDataBySqlFileKenshuuData(string torihikisakiCd, string gyoushaCd, string genbaCd, string kyotenCd, string fromDate, string toDate);
    }

    /// <summary>
    /// 出荷明細Dao
    /// </summary>
    [Bean(typeof(T_SHUKKA_DETAIL))]
    public interface IT_ShukkaDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.DenpyouRireki.Sql.IT_ShukkaDetailDao_GetShukkaDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);

        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.DenpyouRireki.Sql.IT_ShukkaDao_GetShukkaKenshuuData.sql")]
        DataTable GetShukkaKenshuuData(long systemId, int seq);
    }

    /// <summary>
    /// 売上/支払Dao
    /// </summary>
    [Bean(typeof(T_UR_SH_ENTRY))]
    public interface IT_UrShDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <param name="GyoushaCd">業者コード</param>
        /// <param name="GenbaCd">現場コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.DenpyouRireki.Sql.IT_UrShDao_GetUrShData.sql")]
        DataTable GetDataBySqlFile(string torihikisakiCd, string gyoushaCd, string genbaCd, string kyotenCd, string fromDate, string toDate, string upnGyoushaCd, string sharyouCd, string sharyouName);

        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        T_UR_SH_ENTRY[] GetDataForEntity(T_UR_SH_ENTRY data);
    }

    /// <summary>
    /// 売上/支払明細Dao
    /// </summary>
    [Bean(typeof(T_UR_SH_DETAIL))]
    public interface IT_UrShDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.DenpyouRireki.Sql.IT_UrShDetailDao_GetUrShDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);

    }

    /// <summary>
    /// 請求明細Dao
    /// </summary>
    [Bean(typeof(T_SEIKYUU_DETAIL))]
    public interface IT_SeikyuuDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="denpyouSystemId">システムID</param>
        /// <param name="denpyouSeq">枝番</param>
        /// <param name="detailSystemId">明細システムID</param>
        /// <param name="urShNumber">伝票番号</param>
        /// <param name="denpyouShuruiCd">伝票種類CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.DenpyouRireki.Sql.IT_SeikyuuDetailDao_GetSeikyuuDetailCount.sql")]
        int GetDataBySqlFile(long denpyouSystemId, int denpyouSeq, long detailSystemId, long denpyouNumber, int denpyouShuruiCd);

        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="denpyouSystemId">システムID</param>
        /// <param name="denpyouSeq">枝番</param>
        /// <param name="denpyouShuruiCd">伝票種類CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.DenpyouRireki.Sql.IT_SeikyuuDetailDao_GetSeikyuuDetailCount2.sql")]
        int GetDataBySqlFile2(long denpyouSystemId, int denpyouSeq, int denpyouShuruiCd);
    }

    /// <summary>
    /// 清算明細Dao
    /// </summary>
    [Bean(typeof(T_SEISAN_DETAIL))]
    public interface IT_SeisanDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="denpyouSystemId">システムID</param>
        /// <param name="denpyouSeq">枝番</param>
        /// <param name="denpyouShuruiCd">伝票種類CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.DenpyouRireki.Sql.IT_SeisanDetailDao_GetSeisanDetailCount.sql")]
        int GetDataBySqlFile(long denpyouSystemId, int denpyouSeq, int denpyouShuruiCd);
    }

}

