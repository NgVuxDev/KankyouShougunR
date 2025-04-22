// $Id: LogicClass.cs 24958 2014-07-08 06:41:18Z nagata $
using System;
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using Shougun.Core.Master.DenshiShinseiRoute.DTO;

namespace Shougun.Core.Master.DenshiShinseiRoute.DAO
{
    [Bean(typeof(M_DENSHI_SHINSEI_ROUTE))]
    public interface DaoClass : IS2Dao
    {
        /// <summary>
        /// すべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_DENSHI_SHINSEI_ROUTE")]
        M_DENSHI_SHINSEI_ROUTE[] GetAllData();

        /// <summary>
        /// 申請経路CDに紐付くマスタの最大行番号を取得
        /// </summary>
        /// <param name="dsrCd"></param>
        /// <returns></returns>
        [Sql("SELECT MAX(DENSHI_SHINSEI_ROW_NO) FROM M_DENSHI_SHINSEI_ROUTE WHERE DENSHI_SHINSEI_ROUTE_CD = /*dsrCd*/")]
        Int32 GetMaxRowNo(Int16 dsrCd);

        /// <summary>
        /// 申請経路入力画面用の一覧データを取得
        /// </summary>
        /// <param name="data">検索用DTO</param>
        /// <returns name="DataTable">検索結果</returns>
        [SqlFile("Shougun.Core.Master.DenshiShinseiRoute.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(DenshiShinseiRouteFindDto data);
    }
}