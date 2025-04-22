using System.Collections.Generic;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.SalesPayment.ShiharaiJunihyo
{
    /// <summary>
    /// 一覧表パターンDaoインタフェース
    /// </summary>
    [Bean(typeof(M_LIST_PATTERN))]
    public interface IM_LIST_PATTERNDao : IS2Dao
    {
        /// <summary>
        /// 一覧表パターンを追加します
        /// </summary>
        /// <param name="entity">追加するエンティティ</param>
        /// <returns>追加した件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_LIST_PATTERN entity);

        /// <summary>
        /// 一覧表パターンを更新します
        /// </summary>
        /// <param name="entity">更新するエンティティ</param>
        /// <returns>更新した件数</returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_LIST_PATTERN entity);

        /// <summary>
        /// パターンリストを取得します
        /// </summary>
        /// <param name="windowId">画面ID</param>
        /// <returns>一覧表パターンリスト</returns>
        [Sql("SELECT * FROM M_LIST_PATTERN WHERE WINDOW_ID = /*windowId*/0 AND DELETE_FLG = 0")]
        List<M_LIST_PATTERN> GetMListPatternList(int windowId);

        /// <summary>
        /// パターンリストを取得します
        /// </summary>
        /// <param name="windowId">条件エンティティ</param>
        /// <returns>一覧表パターンリスト</returns>
        List<M_LIST_PATTERN> GetListPatternList(M_LIST_PATTERN entity);
    }

    /// <summary>
    /// 一覧表パターン詳細Daoインタフェース
    /// </summary>
    [Bean(typeof(M_LIST_PATTERN_COLUMN))]
    public interface IM_LIST_PATTERN_COLUMNDao : IS2Dao
    {
        /// <summary>
        /// 一覧表パターン詳細を追加します
        /// </summary>
        /// <param name="entity">追加するエンティティ</param>
        /// <returns>追加した件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_LIST_PATTERN_COLUMN entity);

        /// <summary>
        /// 一覧表パターン詳細リストを取得します
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">枝番</param>
        /// <returns>一覧表パターン詳細リスト</returns>
        [Sql("SELECT * FROM M_LIST_PATTERN_COLUMN WHERE SYSTEM_ID = /*systemId*/0 AND SEQ = /*seq*/0")]
        List<M_LIST_PATTERN_COLUMN> GetMListPatternColumnList(long systemId, int seq);
    }

    /// <summary>
    /// 一覧表項目選択Daoインタフェース
    /// </summary>
    [Bean(typeof(S_LIST_COLUMN_SELECT))]
    public interface IS_LIST_COLUMN_SELECTDao : IS2Dao
    {
        /// <summary>
        /// 一覧表項目選択リストを取得します
        /// </summary>
        /// <param name="entity">条件エンティティ</param>
        /// <returns>一覧表項目選択リスト</returns>
        List<S_LIST_COLUMN_SELECT> GetSListColumnSelectList(S_LIST_COLUMN_SELECT entity);
    }

    /// <summary>
    /// 一覧表項目選択詳細Daoインタフェース
    /// </summary>
    [Bean(typeof(S_LIST_COLUMN_SELECT_DETAIL))]
    public interface IS_LIST_COLUMN_SELECT_DETAILDao : IS2Dao
    {
        /// <summary>
        /// 一覧表項目選択詳細リストを取得します
        /// </summary>
        /// <param name="entity">条件エンティティ</param>
        /// <returns>一覧表項目選択詳細リスト</returns>
        List<S_LIST_COLUMN_SELECT_DETAIL> GetSListColumnSelectDetailList(S_LIST_COLUMN_SELECT_DETAIL entity);
    }
}
