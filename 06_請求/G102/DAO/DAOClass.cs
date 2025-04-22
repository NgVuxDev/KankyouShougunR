using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Billing.Seikyushokakunin
{
    [Bean(typeof(T_SEIKYUU_DENPYOU))]
    public interface TSDDaoCls : IS2Dao
    {
        /// <summary>
        /// コードを元に請求伝票データを取得する
        /// </summary>
        /// <parameparam name="cd">業者コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("SEIKYUU_NUMBER = /*cd*/")]
        T_SEIKYUU_DENPYOU GetDataByCd(string cd);
        /// <summary>
        /// 請求伝票取得
        /// </summary>
        /// <param name="seikyuNumber"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.Seikyushokakunin.Sql.GetSeikyu.sql")]
        DataTable GetSeikyudenpyo(string seikyuNumber, string nyuukinMeisaiKbn, string orderBy);
        /// <summary>
        /// 請求伝票取得(入金明細なし）
        /// </summary>
        /// <param name="seikyuNumber"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.Seikyushokakunin.Sql.GetSeikyuMeisaiNashi.sql")]
        DataTable GetSeikyudenpyoMeisaiNashi(string seikyuNumber, string nyuukinMeisaiKbn, string orderBy);
        /// <summary>
        /// 請求伝票更新
        /// </summary>
        /// <param name="seikyuNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.Seikyushokakunin.Sql.UpdateSeikyu.sql")]
        int UpdateSeikyu(string seikyuNumber);
        // <summary>
        /// 請求伝票_鑑更新
        /// </summary>
        /// <param name="seikyuNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.Seikyushokakunin.Sql.UpdateSeikyuKagami.sql")]
        int UpdateSeikyuKagami(string seikyuNumber);
        // <summary>
        /// 請求明細
        /// </summary>
        /// <param name="seikyuNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.Seikyushokakunin.Sql.UpdateSeikyuDetail.sql")]
        int UpdateSeikyuDetail(string seikyuNumber);
        // <summary>
        /// 入金消込更新
        /// </summary>
        /// <param name="seikyuNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.Seikyushokakunin.Sql.UpdateNyukin.sql")]
        int UpdateNyukin(string seikyuNumber);

        // <summary>
        /// 請求伝票 最終更新日、更新者、更新ＰＣ
        /// </summary>
        /// <param name="seisanNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.Seikyushokakunin.Sql.UpdateSeikyuuInfo.sql")]
        int UpdateSeikyuuInfo(T_SEIKYUU_DENPYOU data);
        /// <summary>
        /// 請求伝票鏡_備考
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.Seikyushokakunin.Sql.UpdateSeikyuuKagamiBikou.sql")]
        int UpdateSeikyuuKagamiBikou(T_SEIKYUU_DENPYOU_KAGAMI data);
    }

    [Bean(typeof(M_TORIHIKISAKI_SEIKYUU))]
    public interface MTSDaoCls : IS2Dao
    {
        /// <summary>
        /// コードを元に取引先_請求情報データを取得する
        /// </summary>
        /// <parameparam name="cd">業者コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_TORIHIKISAKI_SEIKYUU GetDataByCd(string cd);
    }

    [Bean(typeof(T_NYUUKIN_KESHIKOMI))]
    public interface TNKDao : IS2Dao
    {
        /// <summary>
        /// 入金消込Entityに該当するデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        T_NYUUKIN_KESHIKOMI[] GetDataForEntity(T_NYUUKIN_KESHIKOMI data);
    }

    [Bean(typeof(T_NYUUKIN_ENTRY))]
    public interface TNEDao : IS2Dao
    {
        /// <summary>
        /// 入金Entityに該当するデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        T_NYUUKIN_ENTRY[] GetDataForEntity(T_NYUUKIN_ENTRY data);
    }

    // 20150602 代納伝票対応(代納不具合一覧52) Start
    [Bean(typeof(T_UR_SH_ENTRY))]
    public interface TUSEDao : IS2Dao
    {
        /// <summary>
        /// 売上支払Entityに該当するデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        T_UR_SH_ENTRY[] GetDataForEntity(T_UR_SH_ENTRY data);
    }
    // 20150602 代納伝票対応(代納不具合一覧52) End
}
