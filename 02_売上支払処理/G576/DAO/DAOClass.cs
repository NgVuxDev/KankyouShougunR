// $Id: DAOClass.cs 48145 2015-04-23 09:28:47Z huangxy@oec-h.com $
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using Shougun.Core.SalesPayment.DenpyouKakuteiNyuryoku.DTO;
using System.Collections.Generic;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.SalesPayment.DenpyouKakuteiNyuryoku.DAO
{
    [Bean(typeof(T_NYUUKIN_ENTRY))]
    //[Bean(typeof(M_OUTPUT_PATTERN))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable getDataForStringSql(string sql);

        /// <summary>
        /// 一覧データ取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.DenpyouKakuteiNyuryoku.Sql.GetIchiranData.sql")]
        DataTable GetIchiranData(DTOClass data);
    }

    /// <summary>
    /// コンテナ情報DAO
    /// </summary>
    [Bean(typeof(T_CONTENA_RESULT))]
    internal interface TCRClass : IS2Dao
    {
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
    }

    /// <summary>
    /// 在庫品名振分DAO
    /// </summary>
    [Bean(typeof(T_ZAIKO_HINMEI_HURIWAKE))]
    internal interface TZHHClass : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_ZAIKO_HINMEI_HURIWAKE data);

        /// <summary>
        /// 在庫データ取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.DenpyouKakuteiNyuryoku.Sql.GetZaikoData.sql")]
        List<T_ZAIKO_HINMEI_HURIWAKE> GetZaikoInfo(T_ZAIKO_HINMEI_HURIWAKE data);

    }

    /// <summary>
    /// 検収明細DAO
    /// </summary>
    [Bean(typeof(T_KENSHU_DETAIL))]
    internal interface TKDClass : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_KENSHU_DETAIL data);
    }
}
