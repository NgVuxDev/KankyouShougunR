using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Carriage.UnchinNyuuRyoku

{
    [Bean(typeof(M_OUTPUT_PATTERN))]
   public interface UnchinEntryDAOClass : IS2Dao
   {
    
       /// <summary>
       /// Entityで絞り込んで拠点マスタ値を取得する
       /// </summary>
       /// <param name="date"></param>
       /// <returns></returns>
       [SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.GetKyotenData.sql")]
       DataTable GetKyotenDataForEntity(M_KYOTEN data);


        /// <summary>
        /// 業者&荷積業者&荷降業者
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.GetGyoushaData.sql")]
       DataTable GetGyoushaDataForEntity(M_GYOUSHA data);
        
        /// <summary>
        /// 車種
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.GetShashuData.sql")]
        DataTable GetShashuDataForEntity(M_SHASHU data);
        
        /// <summary>
        /// 車両
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.GetSharyouData.sql")]
        DataTable GetSharyouDataForEntity(M_SHARYOU data);
      
        /// <summary>
        /// 運転者
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.GetUntenshaData.sql")]
        DataTable GetUntenShaDataForEntity(M_SHAIN data);
      
        /// <summary>
        /// 荷積現場&荷降現場
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.GetGenbaData.sql")]
        DataTable GetGenbaDataForEntity(M_GENBA data);
        
        ///// <summary>
        ///// 絞り込んで値を取得する
        ///// </summary>
        ///// <param name="data">検索条件</param>
        ///// <returns>DataTable</returns>
        //[SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.GetUnchinEntryData.sql")]
        //DataTable GetDataToDataTable(UnchinEntryDTOClass data);

        ///// <summary>
        ///// 絞り込んで値を取得する
        ///// </summary>
        ///// <param name="data">検索条件</param>
        ///// <returns>DataTable</returns>
        //[SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.GetUnchiEntryDataFormOther.sql")]
        //DataTable GetUnchinEntryDataFormOtherToDataTable(UnchinEntryDTOClass data);


        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        [SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.InsertEntryData.sql")]
        int Insert(T_UNCHIN_ENTRY data);
            //,String CREATE_USER, 
            //DateTime CREATE_DATE, 
            //String CREATE_PC,
            //String UPDATE_USER,
            //DateTime UPDATE_DATE,
            //String UPDATE_PC);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        [SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.UpdateEntryData.sql")]
        int Update(T_UNCHIN_ENTRY data);
        
        /// <summary>
        /// Update(論理削除)
        /// </summary>
        /// <param name="data">T_UKETSUKE_SS_ENTRY</param>
        /// <returns>件数</returns>

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="whereSql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        System.Data.DataTable GetDateForStringSql(string sql);
    }

    [Bean(typeof(T_UNCHIN_ENTRY))]
    public interface T_UNCHIN_ENTRYDao : IS2Dao
    {

        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.GetUnchinEntryData.sql")]
        T_UNCHIN_ENTRY GetDataToDataTable(UnchinEntryDTOClass data);
        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.GetUnchinEntryDataNow.sql")]
        T_UNCHIN_ENTRY GetDataToDataTableNow(UnchinEntryDTOClass data);
        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.GetUnchinEntryDataPre.sql")]
        T_UNCHIN_ENTRY GetDataToDataTablePre(UnchinEntryDTOClass data);
        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.GetUnchinEntryDataNext.sql")]
        T_UNCHIN_ENTRY GetDataToDataTableNext(UnchinEntryDTOClass data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UNCHIN_ENTRY data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UNCHIN_ENTRY data);

        int Delete(T_UNCHIN_ENTRY data);
    }

    [Bean(typeof(T_UNCHIN_DETAIL))]
    public interface T_UNCHIN_DETAILDao : IS2Dao
    {
        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.GetUnchinDetailData.sql")]
        T_UNCHIN_DETAIL[] GetDataToDataTable(UnchinEntryDTOClass data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UNCHIN_DETAIL data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UNCHIN_DETAIL data);

        int Delete(T_UNCHIN_DETAIL data);
    }

    [Bean(typeof(T_UKEIRE_ENTRY))]
    public interface T_UKEIRE_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.GetUkeire.sql")]
        T_UKEIRE_ENTRY GetUkeire(T_UKEIRE_ENTRY data);
    }

    [Bean(typeof(T_SHUKKA_ENTRY))]
    public interface T_SHUKKA_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.GetShukka.sql")]
        T_SHUKKA_ENTRY GetShukka(T_SHUKKA_ENTRY data);
    }

    [Bean(typeof(T_UR_SH_ENTRY))]
    public interface T_UR_SH_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.GetUrsh.sql")]
        T_UR_SH_ENTRY GetUrsh(T_UR_SH_ENTRY data);

        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.GetDaino.sql")]
        T_UR_SH_ENTRY GetDaino(T_UR_SH_ENTRY data);
    }
}
