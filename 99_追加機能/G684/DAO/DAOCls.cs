using System.Collections.Generic;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.DTO;
using System.Data.SqlTypes;

// http://s2dao.net.seasar.org/ja/index.html
namespace Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.DAO
{
    [Bean(typeof(T_UKEIRE_ENTRY))]
    public interface T_UKEIRE_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Sql.GetUkeireEntryData.sql")]
        T_UKEIRE_ENTRY[] GetUkeireEntryData(SearchDto data);

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKEIRE_ENTRY data);

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UKEIRE_ENTRY data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UkeireNumber"></param>
        /// <returns></returns>
        [Sql("SELECT * FROM T_UKEIRE_ENTRY WHERE UKEIRE_NUMBER = /*UkeireNumber*/ AND DELETE_FLG = 0")]
        T_UKEIRE_ENTRY GetUkeireDataByNumber(long UkeireNumber);
    }

    [Bean(typeof(T_UKEIRE_DETAIL))]
    public interface T_UKEIRE_DETAILDao : IS2Dao
    {
        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Sql.GetUkeireDetailData.sql")]
        T_UKEIRE_DETAIL[] GetUkeireDetailData(T_UKEIRE_ENTRY data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKEIRE_DETAIL data);
    }

    [Bean(typeof(T_SHUKKA_ENTRY))]
    public interface T_SHUKKA_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Sql.GetShukkaEntryData.sql")]
        T_SHUKKA_ENTRY[] GetShukkaEntryData(SearchDto data);

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SHUKKA_ENTRY data);

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SHUKKA_ENTRY data);
    }

    [Bean(typeof(T_SHUKKA_DETAIL))]
    public interface T_SHUKKA_DETAILDao : IS2Dao
    {
        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Sql.GetShukkaDetailData.sql")]
        T_SHUKKA_DETAIL[] GetShukkaDetailData(T_SHUKKA_ENTRY data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SHUKKA_DETAIL data);
    }

    [Bean(typeof(T_UR_SH_ENTRY))]
    public interface T_UR_SH_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Sql.GetUrshEntryData.sql")]
        T_UR_SH_ENTRY[] GetUrshEntryData(SearchDto data);

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UR_SH_ENTRY data);

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UR_SH_ENTRY data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UkeireNumber"></param>
        /// <returns></returns>
        [Sql("SELECT * FROM T_UR_SH_ENTRY WHERE UR_SH_NUMBER = /*UrshNumber*/ AND DELETE_FLG = 0")]
        T_UR_SH_ENTRY GetUrshDataByNumber(long UrshNumber);
    }

    [Bean(typeof(T_UR_SH_DETAIL))]
    public interface T_UR_SH_DETAILDao : IS2Dao
    {
        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Sql.GetUrshDetailData.sql")]
        T_UR_SH_DETAIL[] GetUrshDetailData(T_UR_SH_ENTRY data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UR_SH_DETAIL data);
    }

    /// <summary>
    /// 在庫情報DAO
    /// </summary>
    [Bean(typeof(T_ZAIKO_UKEIRE_DETAIL))]
    internal interface TZUDClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="sysId">明細部．明細システムID</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Sql.GetUkeireZaikoInfo.sql")]
        List<T_ZAIKO_UKEIRE_DETAIL> GetZaikoInfo(T_ZAIKO_UKEIRE_DETAIL data);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns></returns>
        T_ZAIKO_UKEIRE_DETAIL GetDataForEntity(T_ZAIKO_UKEIRE_DETAIL data);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_ZAIKO_UKEIRE_DETAIL data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_ZAIKO_UKEIRE_DETAIL data);
    }

    /// <summary>
    /// 在庫情報DAO
    /// </summary>
    [Bean(typeof(T_ZAIKO_SHUKKA_DETAIL))]
    internal interface TZSDClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="sysId">明細部．明細システムID</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Sql.GetShukkaZaikoInfo.sql")]
        List<T_ZAIKO_SHUKKA_DETAIL> GetZaikoInfo(T_ZAIKO_SHUKKA_DETAIL data);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns></returns>
        T_ZAIKO_SHUKKA_DETAIL GetDataForEntity(T_ZAIKO_SHUKKA_DETAIL data);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_ZAIKO_SHUKKA_DETAIL data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_ZAIKO_SHUKKA_DETAIL data);
    }

    /// <summary>
    /// 在庫品名振分DAO
    /// </summary>
    [Bean(typeof(T_ZAIKO_HINMEI_HURIWAKE))]
    internal interface TZHHClass : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Sql.GetZaikoInfo2.sql")]
        List<T_ZAIKO_HINMEI_HURIWAKE> GetZaikoInfo(T_ZAIKO_HINMEI_HURIWAKE data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        T_ZAIKO_HINMEI_HURIWAKE GetDataForEntity(T_ZAIKO_HINMEI_HURIWAKE data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_ZAIKO_HINMEI_HURIWAKE data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_ZAIKO_HINMEI_HURIWAKE data);
    }

    /// <summary>
    /// コンテナ情報DAO
    /// </summary>
    [Bean(typeof(T_CONTENA_RESULT))]
    internal interface TCRClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="sysId">受入入力．システムID</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Sql.GetContena.sql")]
        T_CONTENA_RESULT[] GetContena(string sysId, int denshuKbn);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_CONTENA_RESULT data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_CONTENA_RESULT data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UkeireNumber"></param>
        /// <returns></returns>
        [Sql("SELECT * FROM T_CONTENA_RESULT WHERE SYSTEM_ID = /*SystemId*/ AND DENSHU_KBN_CD = /*DenshuKbn*/ AND DELETE_FLG = 0")]
        T_CONTENA_RESULT[] GetContenaData(SqlInt64 SystemId, SqlInt16 DenshuKbn);
    }

    /// <summary>
    /// コンテナ稼動予定情報DAO
    /// </summary>
    [Bean(typeof(T_CONTENA_RESERVE))]
    internal interface TCREClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="sysId">受入入力．システムID</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Sql.GetContenaReserveData.sql")]
        T_CONTENA_RESERVE[] GetContenaReserve(string sysId, string SEQ);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_CONTENA_RESERVE data);
    }

    /// <summary>
    /// コンテナマスタDAO
    /// </summary>
    [Bean(typeof(M_CONTENA))]
    public interface MCClass : IS2Dao
    {
        /// <summary>
        /// キー項目よりコンテナマスタ取得
        /// </summary>
        /// <param name="ContenaShuruiCd">コンテナ種類CD</param>
        /// <param name="ContenaCd">コンテナCD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Sql.GetContenaMaster.sql")]
        M_CONTENA GetContenaMasterEntity(string ContenaShuruiCd, string ContenaCd);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CONTENA data);
    }

    /// <summary>
    /// 検収明細DAO
    /// </summary>
    [Bean(typeof(T_KENSHU_DETAIL))]
    internal interface TKDClass : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Sql.GetKenshuDetail.sql")]
        List<T_KENSHU_DETAIL> GetKenshuInfo(T_KENSHU_DETAIL data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        T_KENSHU_DETAIL GetDataForEntity(T_KENSHU_DETAIL data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_KENSHU_DETAIL data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_KENSHU_DETAIL data);
    }
}