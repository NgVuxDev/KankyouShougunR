using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Stock.ZaikoIdou
{
    #region 在庫移動入力
    /// <summary>
    /// 在庫移動入力Daoインタフェース
    /// </summary>
    [Bean(typeof(T_ZAIKO_IDOU_ENTRY))]
    public interface IT_ZAIKO_IDOU_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// 在庫移動入力レコードを追加します
        /// </summary>
        /// <param name="entity">追加するデータ</param>
        /// <returns>追加した行数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_ZAIKO_IDOU_ENTRY entity);

        /// <summary>
        /// 在庫移動入力レコードを更新します
        /// </summary>
        /// <param name="entity">更新するデータ</param>
        /// <returns>更新した行数</returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_ZAIKO_IDOU_ENTRY entity);

        /// <summary>
        /// 在庫移動入力レコードを取得します
        /// </summary>
        /// <param name="entity">条件エンティティ</param>
        /// <returns>在庫移動入力エンティティ</returns>
        T_ZAIKO_IDOU_ENTRY GetZaikoIdouEntry(T_ZAIKO_IDOU_ENTRY entity);

        /// <summary>
        /// 基準の移動番号より前で最大の移動番号を取得します
        /// </summary>
        /// <param name="idouNumber">基準の移動番号</param>
        /// <returns>移動番号</returns>
        [SqlFile("Shougun.Core.Stock.ZaikoIdou.Sql.GetPrevIdouNumber.sql")]
        T_ZAIKO_IDOU_ENTRY GetPrevIdouNumber(string idouNumber);

        /// <summary>
        /// 基準の移動番号より後ろで最小の移動番号を取得します
        /// </summary>
        /// <param name="idouNumber">基準の移動番号</param>
        /// <returns>移動番号</returns>
        [SqlFile("Shougun.Core.Stock.ZaikoIdou.Sql.GetNextIdouNumber.sql")]
        T_ZAIKO_IDOU_ENTRY GetNextIdouNumber(string idouNumber);

        /// <summary>
        /// 在庫量を取得します
        /// </summary>
        [SqlFile("Shougun.Core.Stock.ZaikoIdou.Sql.GetZaikoRyou.sql")]
        DataTable GetZaikoRyou(string gyoushaCd, string genbaCd, string zaikoHinmeiCd, SqlDateTime dateFrom, SqlDateTime dateTo);
    }
    #endregion

    #region 在庫移動明細
    /// <summary>
    /// 在庫移動明細Daoインタフェース
    /// </summary>
    [Bean(typeof(T_ZAIKO_IDOU_DETAIL))]
    public interface IT_ZAIKO_IDOU_DETAILDao : IS2Dao
    {
        /// <summary>
        /// 在庫移動明細レコードを追加します
        /// </summary>
        /// <param name="entity">追加するデータ</param>
        /// <returns>追加した行数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_ZAIKO_IDOU_DETAIL entity);

        /// <summary>
        /// 在庫移動明細レコードを追加します
        /// </summary>
        /// <param name="entity">追加するデータ</param>
        /// <returns>追加した行数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Update(T_ZAIKO_IDOU_DETAIL entity);

        /// <summary>
        /// 在庫移動明細レコードを更新します
        /// </summary>
        /// <param name="entity">更新するデータ</param>
        /// <returns>更新した行数</returns>
        [Query("ORDER BY ROW_NO")]
        List<T_ZAIKO_IDOU_DETAIL> GetZaikoIdouDetailList(T_ZAIKO_IDOU_DETAIL entity);
    }
    #endregion

    #region 在庫月次
    /// <summary>
    /// 在庫月次Daoインタフェース
    /// </summary>
    [Bean(typeof(T_MONTHLY_LOCK_ZAIKO))]
    public interface IT_MONTHLY_LOCK_ZAIKODao : IS2Dao
    {
        /// <summary>
        /// 在庫品名を取得します
        /// </summary>
        /// <param name="entity">在庫品名CD</param>
        /// <returns>在庫品名</returns>
        [SqlFile("Shougun.Core.Stock.ZaikoIdou.Sql.GetGetsujiData.sql")]
        T_MONTHLY_LOCK_ZAIKO GetGetsujiData(string gyoushaCd, string genbaCd, string zaikoHinmeiCd);
    }
    #endregion
}