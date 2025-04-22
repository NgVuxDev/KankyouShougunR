using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ReceiptPayManagement.Syukinnyuryoku
{
    #region 出金入力

    /// <summary>
    /// 出金入力Daoインタフェース
    /// </summary>
    [Bean(typeof(T_SHUKKIN_ENTRY))]
    public interface IT_SHUKKIN_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// 出金入力レコードを追加します
        /// </summary>
        /// <param name="entity">追加するデータ</param>
        /// <returns>追加した行数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SHUKKIN_ENTRY entity);

        /// <summary>
        /// 出金入力レコードを更新します
        /// </summary>
        /// <param name="entity">更新するデータ</param>
        /// <returns>更新した行数</returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_SHUKKIN_ENTRY entity);

        /// <summary>
        /// 出金入力エンティティリストを取得します
        /// </summary>
        /// <param name="entity">条件エンティティ</param>
        /// <returns>出金入力エンティティリスト</returns>
        T_SHUKKIN_ENTRY GetShukkinEntry(T_SHUKKIN_ENTRY entity);

        /// <summary>
        /// 出金入力エンティティリストを取得します
        /// </summary>
        /// <param name="entity">条件エンティティ</param>
        /// <returns>出金入力エンティティリスト</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Syukinnyuryoku.Sql.GetKeshikomiData.sql")]
        DataTable GetKeshikomiData(T_SHUKKIN_ENTRY data);

        /// <summary>
        /// 基準の出金番号より前で最大の出金番号を取得します
        /// </summary>
        /// <param name="ShukkinNumber">基準の出金番号</param>
        /// <returns>出金番号</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Syukinnyuryoku.Sql.GetPrevShukkinNumber.sql")]
        T_SHUKKIN_ENTRY GetPrevShukkinNumber(string ShukkinNumber, string kyotenCd);

        /// <summary>
        /// 基準の出金番号より後ろで最小の出金番号を取得します
        /// </summary>
        /// <param name="nyuukinNumber">基準の出金番号</param>
        /// <returns>出金番号</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Syukinnyuryoku.Sql.GetNextShukkinNumber.sql")]
        T_SHUKKIN_ENTRY GetNextShukkinNumber(string ShukkinNumber, string kyotenCd);
    }

    #endregion

    #region 出金明細

    /// <summary>
    /// 出金明細Daoインタフェース
    /// </summary>
    [Bean(typeof(T_SHUKKIN_DETAIL))]
    public interface IT_SHUKKIN_DETAILDao : IS2Dao
    {
        /// <summary>
        /// 出金明細レコードを追加します
        /// </summary>
        /// <param name="entity">追加するデータ</param>
        /// <returns>追加した行数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SHUKKIN_DETAIL entity);

        /// <summary>
        /// 出金明細レコードを追加します
        /// </summary>
        /// <param name="entity">追加するデータ</param>
        /// <returns>追加した行数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Update(T_SHUKKIN_DETAIL entity);

        /// <summary>
        /// 出金明細レコードを更新します
        /// </summary>
        /// <param name="entity">更新するデータ</param>
        /// <returns>更新した行数</returns>
        [Query("ORDER BY ROW_NUMBER")]
        List<T_SHUKKIN_DETAIL> GetShukkinDetailList(T_SHUKKIN_DETAIL entity);
    }

    #endregion

    #region 出金消込

    /// <summary>
    /// 出金消込Daoインタフェース
    /// </summary>
    [Bean(typeof(T_SHUKKIN_KESHIKOMI))]
    public interface IT_SHUKKIN_KESHIKOMIDao : IS2Dao
    {
        /// <summary>
        /// 出金消込レコードを追加します
        /// </summary>
        /// <param name="entity">追加するデータ</param>
        /// <returns>追加した行数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SHUKKIN_KESHIKOMI entity);

        /// <summary>
        /// 出金消込レコードを追加します
        /// </summary>
        /// <param name="entity">追加するデータ</param>
        /// <returns>追加した行数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Update(T_SHUKKIN_KESHIKOMI entity);

        /// <summary>
        /// 出金番号で出金消込エンティティリストを取得します
        /// </summary>
        /// <param name="ShukkinNumber">出金番号</param>
        /// <returns>出金消込エンティティリスト</returns>
        [Query("SHUKKIN_NUMBER = /*ShukkinNumber*/ AND DELETE_FLG = 0")]
        List<T_SHUKKIN_KESHIKOMI> GetShukkinKeshikomi(SqlInt64 ShukkinNumber);

        /// <summary>
        /// 出金番号で出金消込エンティティリストを取得します
        /// </summary>
        /// <param name="nyuukinNumber">出金番号</param>
        /// <returns>出金消込エンティティリスト</returns>
        T_SHUKKIN_KESHIKOMI[] GetKeshikomi(T_SHUKKIN_KESHIKOMI data);
    }

    #endregion

    #region 取引先精算

    /// <summary>
    /// 取引先精算Daoインタフェース
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI_SHIHARAI))]
    public interface IM_TORIHIKISAKI_SHIHARAIDao : IS2Dao
    {
        /// <summary>
        /// 取引先CDで取引先精算エンティティを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns>取引先精算エンティティ</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_TORIHIKISAKI_SHIHARAI GetDataByCd(string cd);
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
        [Query("HIDUKE_HANI_BEGIN <= /*denpyouDate*/ AND HIDUKE_HANI_END >= /*denpyouDate*/ AND TORIHIKISAKI_CD = /*cd*/ AND SHORI_KBN = 2")]
        List<T_SHIME_SHORI_CHUU> GetShimeShoriChuuList(DateTime denpyouDate, string cd);
    }

    #endregion

    #region 精算伝票

    /// <summary>
    /// 精算伝票Daoインタフェース
    /// </summary>
    [Bean(typeof(T_SEISAN_DENPYOU))]
    public interface IT_SEISAN_DENPYOUDao : IS2Dao
    {
        /// <summary>
        /// 指定された取引先の出金予定日が伝票日付に直近の精算伝票エンティティを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="denpyouDate">伝票日付</param>
        /// <returns>精算伝票エンティティ</returns>
        [Query("TORIHIKISAKI_CD = /*torihikisakiCd*/ AND SHUKKIN_YOTEI_BI <= /*denpyouDate*/ AND DELETE_FLG = 0 ORDER BY SEISAN_DATE DESC")]
        T_SEISAN_DENPYOU GetLastSeisanDenpyou(string torihikisakiCd, DateTime denpyouDate);
    }

    #endregion

    #region 精算明細

    /// <summary>
    /// 精算明細Daoインタフェース
    /// </summary>
    [Bean(typeof(T_SEISAN_DETAIL))]
    public interface IT_SEISAN_DETAILDao : IS2Dao
    {
        /// <summary>
        /// 出金番号で精算明細エンティティリストを取得します
        /// </summary>
        /// <param name="nyuukinNumber">出金番号</param>
        /// <returns>精算明細エンティティリスト</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Syukinnyuryoku.Sql.GetDataByShukkinNumber.sql")]
        List<T_SEISAN_DETAIL> GetDataByShukkinNumber(SqlInt64 ShukkinNumber);

        /// <summary>
        /// 精算番号で伝票種類が出金の精算明細エンティティリストを取得します
        /// </summary>
        /// <param name="seikyuuNumber">精算番号</param>
        /// <returns>精算明細エンティティリスト</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Syukinnyuryoku.Sql.GetDataBySeisanNumber.sql")]
        List<T_SEISAN_DETAIL> GetDataBySeisanNumber(SqlInt64 seisanNumber);
    }

    #endregion
}