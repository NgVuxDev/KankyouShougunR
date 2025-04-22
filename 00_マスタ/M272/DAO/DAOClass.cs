using System;
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Master.RiyouRirekiKanri.Dao
{
    /// <summary>
    /// 受付Dao
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SS_ENTRY))]
    public interface IT_UketsukeDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <param name="GyoushaCd">業者コード</param>
        /// <param name="GenbaCd">現場コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_UketsukeDao_GetUketsukeData.sql")]
        DataTable GetDataBySqlFile(string kyotenCD, string updateFrom, string updateTo, string appendWhere);
    }

    /// <summary>
    /// 受付(収集)明細Dao
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SS_DETAIL))]
    public interface IT_UketsukeSSDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_UketsukeSSDetailDao_GetUketsukeSSDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);
    }

    /// <summary>
    /// 受付(出荷)明細Dao
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SK_DETAIL))]
    public interface IT_UketsukeSKDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_UketsukeSKDetailDao_GetUketsukeSKDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);
    }

    /// <summary>
    /// 受付(持込)明細Dao
    /// </summary>
    [Bean(typeof(T_UKETSUKE_MK_DETAIL))]
    public interface IT_UketsukeMKDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_UketsukeMKDetailDao_GetUketsukeMKDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);
    }

    /// <summary>
    /// 受付クレームDao
    /// </summary>
    [Bean(typeof(T_UKETSUKE_CM_ENTRY))]
    public interface IT_UketsukeCMDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <param name="GyoushaCd">業者コード</param>
        /// <param name="GenbaCd">現場コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_UketsukeCMDao_GetUketsukeCMData.sql")]
        DataTable GetDataBySqlFile(string torihikisakiCd, string gyoushaCd, string genbaCd);
    }

    /// <summary>
    /// 計量Dao
    /// </summary>
    [Bean(typeof(T_KEIRYOU_ENTRY))]
    public interface IT_KeiryouDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <param name="GyoushaCd">業者コード</param>
        /// <param name="GenbaCd">現場コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_KeiryouDao_GetKeiryouData.sql")]
        DataTable GetDataBySqlFile(string kyotenCD, string updateFrom, string updateTo, string appendWhere);
    }

    /// <summary>
    /// 計量明細Dao
    /// </summary>
    [Bean(typeof(T_KEIRYOU_DETAIL))]
    public interface IT_KeiryouDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_KeiryouDetailDao_GetKeiryouDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);
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
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_UkeireDao_GetUkeireData.sql")]
        DataTable GetDataBySqlFile(string kyotenCD, string updateFrom, string updateTo, string appendWhere);
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
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_UkeireDetailDao_GetUkeireDetailData.sql")]
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
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_ShukkaDao_GetShukkaData.sql")]
        DataTable GetDataBySqlFile(string kyotenCD, string updateFrom, string updateTo, string appendWhere);
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
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_ShukkaDetailDao_GetShukkaDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);
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
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_UrShDao_GetUrShData.sql")]
        DataTable GetDataBySqlFile(string kyotenCD, string updateFrom, string updateTo, string appendWhere);

        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="kyotenCD">拠点</param>
        /// <param name="updateFrom">更新日From</param>
        /// <param name="updateTo">更新日To</param>
        /// <param name="appendWhere">検索条件</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_UrShDao_GetDainouData.sql")]
        DataTable GetDainouDataBySqlFile(string kyotenCD, string updateFrom, string updateTo, string appendWhere);

        /// <summary>
        /// Get Dainou data for export CSV
        /// </summary>
        /// <param name="kyotenCD">拠点</param>
        /// <param name="updateFrom">更新日From</param>
        /// <param name="updateTo">更新日To</param>
        /// <param name="appendWhere">検索条件</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_UrShDao_GetDainouData_ForExport.sql")]
        DataTable GetDainouDataBySqlFileForExportCSV(string kyotenCD, string updateFrom, string updateTo, string appendWhere);

        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        T_UR_SH_ENTRY[] GetUrShEntry(T_UR_SH_ENTRY data);
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
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_UrShDetailDao_GetUrShDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);

        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">受入システムID</param>
        /// <param name="seq">受入SEQ</param>
        /// <param name="systemId">出荷システムID</param>
        /// <param name="seq">出荷SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_UrShDetailDao_GetDainouDetailData.sql")]
        DataTable GetDainouDataBySqlFile(long systemIduUkeire, int seqUkeire, long systemIdShukka, int seqShukka);
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
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_SeikyuuDetailDao_GetSeikyuuDetailCount.sql")]
        int GetDataBySqlFile(long denpyouSystemId, int denpyouSeq, long detailSystemId, long denpyouNumber, int denpyouShuruiCd);
    }

    /// <summary>
    /// 入金Dao
    /// </summary>
    [Bean(typeof(T_NYUUKIN_ENTRY))]
    public interface IT_NyuukinDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_NyuukinDao_GetNyuukinData.sql")]
        DataTable GetDataBySqlFile(string kyotenCD, string updateFrom, string updateTo, string appendWhere);

        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        T_NYUUKIN_ENTRY[] GetNyuukinEntry(T_NYUUKIN_ENTRY data);
    }

    /// <summary>
    /// 入金明細Dao
    /// </summary>
    [Bean(typeof(T_NYUUKIN_DETAIL))]
    public interface IT_NyuukinDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_NyuukinDetailDao_GetNyuukinDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);
    }

    /// <summary>
    /// 出金Dao
    /// </summary>
    [Bean(typeof(T_SHUKKIN_ENTRY))]
    public interface IT_ShukkinDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_ShukkinDao_GetShukkinData.sql")]
        DataTable GetDataBySqlFile(string kyotenCD, string updateFrom, string updateTo, string appendWhere);
    }

    /// <summary>
    /// 出金明細Dao
    /// </summary>
    [Bean(typeof(T_SHUKKIN_DETAIL))]
    public interface IT_ShukkinDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.RiyouRirekiKanri.Sql.IT_ShukkinDetailDao_GetShukkinDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);
    }

    internal class DAOClass : IS2Dao
    {
        public int Insert(SuperEntity data)
        {
            throw new NotImplementedException();
        }

        public int Update(SuperEntity data)
        {
            throw new NotImplementedException();
        }

        public int Delete(SuperEntity data)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetAllMasterDataForPopup(string whereSql)
        {
            throw new NotImplementedException();
        }

        public SuperEntity GetDataForEntity(SuperEntity date)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetDateForStringSql(string sql)
        {
            throw new NotImplementedException();
        }
    }
}
