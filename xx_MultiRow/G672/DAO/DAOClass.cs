using System.Data;
using System.Data.SqlTypes;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System;

namespace Shougun.Core.Scale.KeiryouNyuuryoku.DAO
{
    /// <summary>
    /// 取引先_請求情報DAO
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI_SEIKYUU))]
    internal interface MTSeiClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="torihikisakiCD">画面．取引先CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Scale.KeiryouNyuuryoku.Sql.GetTorihikisakiKBN_Seikyuu.sql")]
        string GetTorihikisakiKBN_Seikyuu(string torihikisakiCD);
    }

    /// <summary>
    /// 取引先_支払情報DAO
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI_SHIHARAI))]
    internal interface MTShihaClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="torihikisakiCD">画面．取引先CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Scale.KeiryouNyuuryoku.Sql.GetTorihikisakiKBN_Shiharai.sql")]
        string GetTorihikisakiKBN_Shiharai(string torihikisakiCD);
    }

    /// <summary>
    /// 形態区分DAO
    /// </summary>
    [Bean(typeof(M_KEITAI_KBN))]
    internal interface MKKClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値のうち、一番若いコードを取得
        /// </summary>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Scale.KeiryouNyuuryoku.Sql.GetKeitaiKbnCd.sql")]
        SqlInt16 GetKeitaiKbnCd(SqlInt16 denshuKbnCd);
    }

    /// <summary>
    /// 計量入力DAO
    /// </summary>
    [Bean(typeof(T_KEIRYOU_ENTRY))]
    internal interface TKEClass : IS2Dao
    {
        /// <summary>
        /// 指定された計量番号の次に小さい番号を取得
        /// </summary>
        /// <param name="KeiryouNumber">計量番号</param>
        /// <param name="KyotenCD">拠点CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Scale.KeiryouNyuuryoku.Sql.GetPreKeiryouNumber.sql")]
        SqlInt64 GetPreKeiryouNumber(SqlInt64 KeiryouNumber, string KyotenCD);

        /// <summary>
        /// 指定された計量番号の次に大きい番号を取得
        /// </summary>
        /// <param name="KeiryouNumber">計量番号</param>
        /// <param name="KyotenCD">拠点CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Scale.KeiryouNyuuryoku.Sql.GetNextKeiryouNumber.sql")]
        SqlInt64 GetNextKeiryouNumber(SqlInt64 KeiryouNumber, string KyotenCD);

        /// <summary>
        /// 滞留データを取得
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Scale.KeiryouNyuuryoku.Sql.GetTairyuuData.sql")]
        DataTable GetTairyuuData(TairyuuDTOClass data, string date = null);
    }
}
