using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Carriage.UnchinNyuuRyoku

{
    [Bean(typeof(M_OUTPUT_PATTERN))]
   public interface UnchinDetailDAOClass : IS2Dao
   {

        ///// <summary>
        ///// 絞り込んで値を取得する
        ///// </summary>
        ///// <param name="data">検索条件</param>
        ///// <returns>DataTable</returns>
        //[SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.GetUnchinDetailData.sql")]
        //T_UNCHIN_DETAIL[] GetDataToDataTable(UnchinEntryDTOClass data);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        [SqlFile("Shougun.Core.Carriage.UnchinNyuuRyoku.Sql.InsertDetailData.sql")]
        int Insert(T_UNCHIN_DETAIL data);
               
    }
}
