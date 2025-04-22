using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Adjustment.Shiharaimeisaishokakunin
{
    [Bean(typeof(T_SEISAN_DENPYOU))]
    public interface TSDDaoCls : IS2Dao
    {
        /// <summary>
        /// コードを元に精算伝票データを取得する
        /// </summary>
        /// <parameparam name="cd">業者コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("SEISAN_NUMBER = /*cd*/")]
        T_SEISAN_DENPYOU GetDataByCd(string cd);
        /// <summary>
        /// 精算伝票取得
        /// </summary>
        /// <param name="seisanNumber"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaimeisaishokakunin.Sql.GetSeisan.sql")]
        DataTable GetSeisandenpyo(string seisanNumber, string shukkinMeisaiKbn, string orderBy);
        /// <summary>
        /// 精算伝票取得（出金明細なし）
        /// </summary>
        /// <param name="seisanNumber"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaimeisaishokakunin.Sql.GetSeisanMeisaiNashi.sql")]
        DataTable GetSeisandenpyoMeisaiNashi(string seisanNumber, string shukkinMeisaiKbn, string orderBy);
        /// <summary>
        /// 精算伝票更新
        /// </summary>
        /// <param name="seisanNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaimeisaishokakunin.Sql.UpdateSeisan.sql")]
        int UpdateSeisan(string seisanNumber);
        // <summary>
        /// 精算伝票_鑑更新
        /// </summary>
        /// <param name="seisanNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaimeisaishokakunin.Sql.UpdateSeisanKagami.sql")]
        int UpdateSeisanKagami(string seisanNumber);
        // <summary>
        /// 精算明細
        /// </summary>
        /// <param name="seisanNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaimeisaishokakunin.Sql.UpdateSeisanDetail.sql")]
        int UpdateSeisanDetail(string seisanNumber);
        // <summary>
        /// 出金消込更新
        /// </summary>
        /// <param name="seisanNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaimeisaishokakunin.Sql.UpdateShukkin.sql")]
        int UpdateShukkin(string seisanNumber);
        // <summary>
        /// 精算伝票 最終更新日、更新者、更新ＰＣ
        /// </summary>
        /// <param name="seisanNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaimeisaishokakunin.Sql.UpdateSeisanInfo.sql")]
        int UpdateSeisanInfo(T_SEISAN_DENPYOU data);
        /// <summary>
        /// 精算伝票鏡_備考
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaimeisaishokakunin.Sql.UpdateSeisanKagamiBikou.sql")]
        int UpdateSeisanKagamiBikou(T_SEISAN_DENPYOU_KAGAMI data);
    }

    [Bean(typeof(M_TORIHIKISAKI_SHIHARAI))]
    public interface MTSDaoCls : IS2Dao
    {
        /// <summary>
        /// コードを元に取引先_支払情報データを取得する
        /// </summary>
        /// <parameparam name="cd">業者コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_TORIHIKISAKI_SHIHARAI GetDataByCd(string cd);
    }

    // 20150602 代納伝票対応(代納不具合一覧52) Start
    [Bean(typeof(T_UR_SH_ENTRY))]
    public interface TUSEDaoCls : IS2Dao
    {
        /// <summary>
        /// 売上支払Entityに該当するデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        T_UR_SH_ENTRY[] GetDataForEntity(T_UR_SH_ENTRY data);
    }
    // 20150602 代納伝票対応(代納不具合一覧52) End

    //160019 S
    /// <summary>
    /// 出金消込Dao
    /// </summary>
    [Bean(typeof(T_SHUKKIN_KESHIKOMI))]
    public interface TSKDao : IS2Dao
    {
        /// <summary>
        /// 出金消込Entityに該当するデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        T_SHUKKIN_KESHIKOMI[] GetDataForEntity(T_SHUKKIN_KESHIKOMI data);
    }
    //160019 E
}