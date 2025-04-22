using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2
{
    #region 入金一括入力

    /// <summary>
    /// 入金一括入力Daoインタフェース
    /// </summary>
    [Bean(typeof(T_NYUUKIN_SUM_ENTRY))]
    public interface IT_NYUUKIN_SUM_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// 入金一括入力レコードを追加します
        /// </summary>
        /// <param name="entity">追加するデータ</param>
        /// <returns>追加した行数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_NYUUKIN_SUM_ENTRY entity);

        /// <summary>
        /// 入金一括入力レコードを更新します
        /// </summary>
        /// <param name="entity">更新するデータ</param>
        /// <returns>更新した行数</returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_NYUUKIN_SUM_ENTRY entity);

        /// <summary>
        /// 入金一括入力レコードを取得します
        /// </summary>
        /// <param name="entity">条件エンティティ</param>
        /// <returns>入金一括入力エンティティ</returns>
        T_NYUUKIN_SUM_ENTRY GetNyuukinSumEntry(T_NYUUKIN_SUM_ENTRY entity);

        /// <summary>
        /// 基準の入金番号より前で最大の入金番号を取得します
        /// </summary>
        /// <param name="nyuukinNumber">基準の入金番号</param>
        /// <returns>入金番号</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2.Sql.GetPrevNyuukinNumber.sql")]
        T_NYUUKIN_SUM_ENTRY GetPrevNyuukinNumber(string nyuukinNumber, string kyotenCd);

        /// <summary>
        /// 基準の入金番号より後ろで最小の入金番号を取得します
        /// </summary>
        /// <param name="nyuukinNumber">基準の入金番号</param>
        /// <returns>入金番号</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2.Sql.GetNextNyuukinNumber.sql")]
        T_NYUUKIN_SUM_ENTRY GetNextNyuukinNumber(string nyuukinNumber, string kyotenCd);
    }

    #endregion

    #region 入金一括明細

    /// <summary>
    /// 入金一括明細Daoインタフェース
    /// </summary>
    [Bean(typeof(T_NYUUKIN_SUM_DETAIL))]
    public interface IT_NYUUKIN_SUM_DETAILDao : IS2Dao
    {
        /// <summary>
        /// 入金一括明細レコードを追加します
        /// </summary>
        /// <param name="entity">追加するデータ</param>
        /// <returns>追加した行数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_NYUUKIN_SUM_DETAIL entity);

        /// <summary>
        /// 入金一括明細レコードを更新します
        /// </summary>
        /// <param name="entity">更新するデータ</param>
        /// <returns>更新した行数</returns>
        [Query("ORDER BY ROW_NUMBER")]
        List<T_NYUUKIN_SUM_DETAIL> GetNyuukinSumDetailList(T_NYUUKIN_SUM_DETAIL entity);
    }

    #endregion

    #region 入金入力

    /// <summary>
    /// 入金入力Daoインタフェース
    /// </summary>
    [Bean(typeof(T_NYUUKIN_ENTRY))]
    public interface IT_NYUUKIN_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// 入金入力レコードを追加します
        /// </summary>
        /// <param name="entity">追加するデータ</param>
        /// <returns>追加した行数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_NYUUKIN_ENTRY entity);

        /// <summary>
        /// 入金入力レコードを更新します
        /// </summary>
        /// <param name="entity">更新するデータ</param>
        /// <returns>更新した行数</returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_NYUUKIN_ENTRY entity);

        /// <summary>
        /// 入金入力エンティティリストを取得します
        /// </summary>
        /// <param name="entity">条件エンティティ</param>
        /// <returns>入金入力エンティティリスト</returns>
        [Query("ORDER BY SYSTEM_ID")]
        List<T_NYUUKIN_ENTRY> GetNyuukinEntryList(T_NYUUKIN_ENTRY entity);
    }

    #endregion

    #region 入金明細

    /// <summary>
    /// 入金明細Daoインタフェース
    /// </summary>
    [Bean(typeof(T_NYUUKIN_DETAIL))]
    public interface IT_NYUUKIN_DETAILDao : IS2Dao
    {
        /// <summary>
        /// 入金明細レコードを追加します
        /// </summary>
        /// <param name="entity">追加するデータ</param>
        /// <returns>追加した行数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_NYUUKIN_DETAIL entity);

        /// <summary>
        /// 入金明細レコードを更新します
        /// </summary>
        /// <param name="entity">更新するデータ</param>
        /// <returns>更新した行数</returns>
        [Query("ORDER BY ROW_NUMBER")]
        List<T_NYUUKIN_DETAIL> GetNyuukinDetailList(T_NYUUKIN_DETAIL entity);
    }

    #endregion

    #region 入金消込

    /// <summary>
    /// 入金消込Daoインタフェース
    /// </summary>
    [Bean(typeof(T_NYUUKIN_KESHIKOMI))]
    public interface IT_NYUUKIN_KESHIKOMIDao : IS2Dao
    {
        /// <summary>
        /// 入金消込レコードを追加します
        /// </summary>
        /// <param name="entity">追加するデータ</param>
        /// <returns>追加した行数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_NYUUKIN_KESHIKOMI entity);

        /// <summary>
        /// 入金消込レコードを追加します
        /// </summary>
        /// <param name="entity">追加するデータ</param>
        /// <returns>追加した行数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Update(T_NYUUKIN_KESHIKOMI entity);

        /// <summary>
        /// 入金番号で入金消込エンティティリストを取得します
        /// </summary>
        /// <param name="nyuukinNumber">入金番号</param>
        /// <returns>入金消込エンティティリスト</returns>
        [Query("NYUUKIN_NUMBER = /*nyuukinNumber*/ AND DELETE_FLG = 0")]
        List<T_NYUUKIN_KESHIKOMI> GetNyuukinKeshikomiByNyuukinNumber(SqlInt64 nyuukinNumber);
    }

    #endregion

    #region 仮受金調整

    /// <summary>
    /// 仮受金調整Daoインタフェース
    /// </summary>
    [Bean(typeof(T_KARIUKE_CHOUSEI))]
    public interface IT_KARIUKE_CHOUSEIDao : IS2Dao
    {
        /// <summary>
        /// 仮受金調整レコードを追加します
        /// </summary>
        /// <param name="entity">追加するデータ</param>
        /// <returns>追加した行数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_KARIUKE_CHOUSEI entity);

        /// <summary>
        /// 仮受金調整レコードを更新します
        /// </summary>
        /// <param name="entity">更新するデータ</param>
        /// <returns>更新した行数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Update(T_KARIUKE_CHOUSEI entity);

        /// <summary>
        /// 仮受金調整エンティティを取得します
        /// </summary>
        /// <param name="entity">条件エンティティ</param>
        /// <returns>仮受金調整エンティティ</returns>
        T_KARIUKE_CHOUSEI GetKariukeChousei(T_KARIUKE_CHOUSEI entity);
    }

    #endregion

    #region 仮受金管理

    /// <summary>
    /// 仮受金管理Daoインタフェース
    /// </summary>
    [Bean(typeof(T_KARIUKE_CONTROL))]
    public interface IT_KARIUKE_CONTROLDao : IS2Dao
    {
        /// <summary>
        /// 仮受金管理レコードを追加します
        /// </summary>
        /// <param name="entity">追加するデータ</param>
        /// <returns>追加した行数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_KARIUKE_CONTROL entity);

        /// <summary>
        /// 仮受金管理レコードを更新します
        /// </summary>
        /// <param name="entity">更新するデータ</param>
        /// <returns>更新した行数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Update(T_KARIUKE_CONTROL entity);

        /// <summary>
        /// 入金先CDで仮受金管理エンティティを取得します
        /// </summary>
        /// <param name="nyukinsakiCd">入金先CD</param>
        /// <returns>仮受金管理エンティティ</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2.Sql.GetKariukekinByNyukinsakiCd.sql")]
        T_KARIUKE_CONTROL GetKariukekinByNyukinSakiCd(string nyukinsakiCd);
    }

    #endregion

    #region 締処理中

    /// <summary>
    /// 締処理中Daoインタフェース
    /// </summary>
    [Bean(typeof(T_SHIME_SHORI_CHUU))]
    public interface IT_SHIME_SHORI_CHUUDao : IS2Dao
    {
        /// <summary>
        /// 伝票日付、取引先CDリストで締処理中エンティティリストを取得します
        /// </summary>
        /// <param name="denpyouDate">伝票日付</param>
        /// <param name="torihikisakiCdList">取引先CD</param>
        /// <returns>締処理中エンティティリスト</returns>
        [Query("HIDUKE_HANI_BEGIN <= /*denpyouDate*/ AND HIDUKE_HANI_END >= /*denpyouDate*/ AND TORIHIKISAKI_CD IN /*torihikisakiCdList*/('aaa', 'bbb') AND SHORI_KBN = 1")]
        List<T_SHIME_SHORI_CHUU> GetShimeShoriChuuList(DateTime denpyouDate, List<String> torihikisakiCdList);
    }

    #endregion

    #region 請求

    /// <summary>
    /// 請求伝票Daoインタフェース
    /// </summary>
    [Bean(typeof(T_SEIKYUU_DENPYOU))]
    public interface IT_SEIKYUU_DENPYOUDao : IS2Dao
    {
        /// <summary>
        /// 指定された取引先の入金予定日が伝票日付に直近の請求伝票エンティティを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="denpyouDate">伝票日付</param>
        /// <returns>請求伝票エンティティ</returns>
        //[Query("TORIHIKISAKI_CD = /*torihikisakiCd*/ AND NYUUKIN_YOTEI_BI <= /*denpyouDate*/ AND DELETE_FLG = 0 ORDER BY SEIKYUU_DATE DESC")]
        [SqlFile("Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2.Sql.GetLastSeikyuuDenpyouByTorihikisakiCdAndNyuukinYoteiBi.sql")]
        DataTable GetLastSeikyuuDenpyouByTorihikisakiCdAndNyuukinYoteiBi(string torihikisakiCd, DateTime denpyouDate);
    }

    #endregion

    #region 請求明細

    /// <summary>
    /// 請求明細Daoインタフェース
    /// </summary>
    [Bean(typeof(T_SEIKYUU_DETAIL))]
    public interface IT_SEIKYUU_DETAILDao : IS2Dao
    {
        /// <summary>
        /// 入金番号で請求明細エンティティリストを取得します
        /// </summary>
        /// <param name="nyuukinNumber">入金番号</param>
        /// <returns>請求明細エンティティリスト</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2.Sql.GetSeikyuuDetailListByNyuukinNumber.sql")]
        List<T_SEIKYUU_DETAIL> GetSeikyuuDetailListByNyuukinNumber(SqlInt64 nyuukinNumber);

        /// <summary>
        /// 請求番号で伝票種類が入金の請求明細エンティティリストを取得します
        /// </summary>
        /// <param name="seikyuuNumber">請求番号</param>
        /// <returns>請求明細エンティティリスト</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2.Sql.GetSeikyuuDetailListBySeikyuuNumberAndNyuukinDenpyou.sql")]
        List<T_SEIKYUU_DETAIL> GetSeikyuuDetailListBySeikyuuNumberAndNyuukinDenpyou(SqlInt64 seikyuuNumber);
    }

    #endregion

    #region 取引先請求

    /// <summary>
    /// 取引先請求Daoインタフェース
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI_SEIKYUU))]
    public interface IM_TORIHIKISAKI_SEIKYUUDao : IS2Dao
    {
        /// <summary>
        /// 取引先CDで取引先請求エンティティを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns>取引先請求エンティティ</returns>
        [Query("TORIHIKISAKI_CD = /*torihikisakiCd*/")]
        M_TORIHIKISAKI_SEIKYUU GetTorihikisakiSeikyuuByTorihikisakiCd(string torihikisakiCd);

        /// <summary>
        /// 取引先CD、入金先CDで取引先請求エンティティを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="nyuukinsakiCd">入金先CD</param>
        /// <returns>取引先請求エンティティ</returns>
        [Query("TORIHIKISAKI_CD = /*torihikisakiCd*/ AND NYUUKINSAKI_CD = /*nyuukinsakiCd*/")]
        M_TORIHIKISAKI_SEIKYUU GetTorihikisakiSeikyuuByTorihikisakiCdAndNyuukinsakiCd(string torihikisakiCd, string nyuukinsakiCd);

        /// <summary>
        /// 入金先CDで取引先請求エンティティリストを取得します
        /// </summary>
        /// <param name="nyuukinsakiCd">入金先CD</param>
        /// <param name="denpyouDate">伝票日付</param>
        /// <returns>取引先請求エンティティリスト</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2.Sql.GetTorihikisakiSeikyuuListByNyuukinsakiCdAndDenpyouDate.sql")]
        List<M_TORIHIKISAKI_SEIKYUU> GetTorihikisakiSeikyuuListByNyuukinsakiCdAndDenpyouDate(string nyuukinsakiCd, DateTime denpyouDate);
    }

    #endregion
}