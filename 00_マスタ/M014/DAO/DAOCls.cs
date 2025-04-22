using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using Shougun.Core.Master.OboegakiIkkatuHoshu.DTO;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Master.OboegakiIkkatuHoshu.Dao
{
    [Bean(typeof(T_ITAKU_MEMO_IKKATSU_ENTRY))]
    public interface ItakuMemoIkkatsuEntryDAO : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_ITAKU_MEMO_IKKATSU_ENTRY data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps(
            "MEMO_UPDATE_DATE", "MEMO", "HST_GYOUSHA_CD", "HST_GYOUSHA_NAME", "HST_GENBA_CD", "HST_GENBA_NAME", "UNPAN_GYOUSHA_CD", "UNPAN_GYOUSHA_NAME",
            "SHOBUN_PATTERN_SYSTEM_ID", "SHOBUN_PATTERN_SEQ", "SHOBUN_PATTERN_NAME", "LAST_SHOBUN_PATTERN_SYSTEM_ID", "LAST_SHOBUN_PATTERN_SEQ",
            "LAST_SHOBUN_PATTERN_NAME", "KEIYAKU_BEGIN", "KEIYAKU_END", "UPDATE_SHUBETSU", "KEIYAKUSHO_SHURUI", "SHOBUN_UPDATE_KBN",
            "UPD_SHOBUN_PATTERN_SYSTEM_ID", "UPD_SHOBUN_PATTERN_SEQ", "UPD_SHOBUN_PATTERN_NAME", "LAST_SHOBUN_UPDATE_KBN",
            "UPD_LAST_SHOBUN_PATTERN_SYSTEM_ID", "UPD_LAST_SHOBUN_PATTERN_SEQ", "UPD_LAST_SHOBUN_PATTERN_NAME",
            "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_ITAKU_MEMO_IKKATSU_ENTRY data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_ITAKU_MEMO_IKKATSU_ENTRY data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        System.Data.DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.OboegakiIkkatuHoshu.Sql.GetItakuMemoIkkatsuEntrySql.sql")]
        new DataTable GetDataForEntryEntity(DTOCls data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.OboegakiIkkatuHoshu.Sql.GetItakuMemoIkkatsuDetailSql.sql")]
        new DataTable GetDataForDetailByDenpyouNumberEntity(T_ITAKU_MEMO_IKKATSU_ENTRY data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.OboegakiIkkatuHoshu.Sql.GetItakuMemoIkkatsuDetailSearchSql.sql")]
        new DataTable GetDataForDetailByJyokenEntity(DTOCls data);

        /// <summary>
        /// 伝票番号の一個前か一個次の番号を取得する
        /// （Delete_Flg=1は除く）
        /// </summary>
        /// <param name="isPrevious">前=True, 次=False</param>
        /// <param name="number">基準になる伝票番号</param>
        /// <returns>結果の伝票番号</returns>
        [SqlFile("Shougun.Core.Master.OboegakiIkkatuHoshu.Sql.GetPreviousNextNumber.sql")]
        long GetPreviousNextNumber(bool isPrevious, long number);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data);

        [SqlFile("Shougun.Core.Master.OboegakiIkkatuHoshu.Sql.GetPatternNameSql.sql")]
        DataTable GetPatternName(M_SBNB_PATTERN data);
    }

    [Bean(typeof(T_ITAKU_MEMO_IKKATSU_DETAIL))]
    public interface ItakuMemoIkkatsuDetailDAO : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_ITAKU_MEMO_IKKATSU_DETAIL data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_ITAKU_MEMO_IKKATSU_DETAIL data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_ITAKU_MEMO_IKKATSU_DETAIL data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        System.Data.DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.OboegakiIkkatuHoshu.Sql.GetItakuMemoIkkatsuDetailSql.sql")]
        new DataTable GetDataForDetailByDenpyouNumberEntity(T_ITAKU_MEMO_IKKATSU_ENTRY data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.OboegakiIkkatuHoshu.Sql.GetItakuMemoIkkatsuDetailSearchSql.sql")]
        new DataTable GetDataForDetailByJyokenEntity(DTOCls data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        new DataTable GetDataForStringSql(string sql);
    }

    [Bean(typeof(M_ITAKU_KEIYAKU_OBOE))]
    public interface ItakuKeiyakuOboeDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ITAKU_KEIYAKU_OBOE data);

        [SqlFile("Shougun.Core.Master.OboegakiIkkatuHoshu.Sql.GetItakuKeiyakuOboeByItakuKeiyakuNoToSysId.sql")]
        DataTable GetDataByItakuKeiyakuNo(M_ITAKU_KEIYAKU_OBOE data);

        [SqlFile("Shougun.Core.Master.OboegakiIkkatuHoshu.Sql.GetItakuKeiyakuOboeMaxSeqSql.sql")]
        DataTable GetMaxSeq(M_ITAKU_KEIYAKU_OBOE data);
    }

    [Bean(typeof(M_ITAKU_KEIYAKU_BETSU3))]
    public interface ItakuKeiyakuBetsu3Dao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ITAKU_KEIYAKU_BETSU3 data);

        [SqlFile("Shougun.Core.Master.OboegakiIkkatuHoshu.Sql.GetItakuKeiyakuBetsu3ByItakuKeiyakuNoToSysId.sql")]
        DataTable GetDataByItakuKeiyakuNo(M_ITAKU_KEIYAKU_BETSU3 data);

        [SqlFile("Shougun.Core.Master.OboegakiIkkatuHoshu.Sql.GetItakuKeiyakuBetsu3DeleteSql.sql")]
        int Delete(M_ITAKU_KEIYAKU_BETSU3 data);
    }

    [Bean(typeof(M_ITAKU_KEIYAKU_BETSU4))]
    public interface ItakuKeiyakuBetsu4Dao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ITAKU_KEIYAKU_BETSU4 data);

        [SqlFile("Shougun.Core.Master.OboegakiIkkatuHoshu.Sql.GetItakuKeiyakuBetsu4ByItakuKeiyakuNoToSysId.sql")]
        DataTable GetDataByItakuKeiyakuNo(M_ITAKU_KEIYAKU_BETSU4 data);

        [SqlFile("Shougun.Core.Master.OboegakiIkkatuHoshu.Sql.GetItakuKeiyakuBetsu4DeleteSql.sql")]
        int Delete(M_ITAKU_KEIYAKU_BETSU4 data);
    }

    [Bean(typeof(M_ITAKU_KEIYAKU_KIHON))]
    public interface IMITAKUKEIYAKUKIHONDao : IS2Dao
    {
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_ITAKU_KEIYAKU_KIHON data);

        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/")]
        M_ITAKU_KEIYAKU_KIHON GetDataBySystemId(M_ITAKU_KEIYAKU_KIHON data);
    }
}