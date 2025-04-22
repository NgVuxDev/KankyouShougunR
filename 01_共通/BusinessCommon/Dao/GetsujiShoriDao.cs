using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using Shougun.Core.Common.BusinessCommon.Dto;

namespace Shougun.Core.Common.BusinessCommon.Dao
{
    [Bean(typeof(T_MONTHLY_LOCK_UR))]
    public interface GetsujiShoriDao : IS2Dao
    {
        #region 月次ロック用

        /// <summary>
        /// 月次処理 - ロック判定用データを取得取得します
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetsujiShoriLockCheck.sql")]
        DataTable GetGetusjiShoriLockCheckData(GetusjiShoriCheckDTOClass data);

        /// <summary>
        /// 月次処理 - 削除されていない最新月次年月でソートしたデータを取得します
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetsujiShoriLatestData.sql")]
        DataTable GetLatestGetsujiDate(GetusjiShoriCheckDTOClass data);

        #endregion

        #region 月次処理用

        /// <summary>
        /// 前回売上月次処理の残高を取得します
        /// </summary>
        /// <param name="data">取得したい取引先、年月(前回月次処理の年・月)を設定したDTO</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetsujiShori.GetUriageZandaka.sql")]
        DataTable GetUriageZandaka(string TORIHIKISAKI_CD, int YEAR, int MONTH);

        /// <summary>
        /// 前回支払月次処理の残高を取得します
        /// </summary>
        /// <param name="data">取得したい取引先、年月(前回月次処理の年・月)を設定したDTO</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetsujiShori.GetShiharaiZandaka.sql")]
        DataTable GetShiharaiZandaka(string TORIHIKISAKI_CD, int YEAR, int MONTH);

        /// <summary>
        /// 受入、出荷、売上/支払何れかの支払データを取得します
        /// </summary>
        /// <param name="DENPYO_SHURUI">2：受入　3：出荷　4：売上/支払</param>
        /// <param name="SHIHARAI_CD">取引先CD</param>
        /// <param name="SHIHARAISHIMEBI_FROM">[任意] 取得する期間FROM(yyy/MM/dd形式)</param>
        /// <param name="SHIHARAISHIMEBI_TO">取得する期間TO(yyy/MM/dd形式)</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetsujiShori.GetShiharaiData.sql")]
        DataTable GetShiharaiData(int DENPYO_SHURUI, string SHIHARAI_CD, string SHIHARAISHIMEBI_FROM, string SHIHARAISHIMEBI_TO);

        /// <summary>
        /// 受入、出荷、売上/支払何れかの売上データを取得します
        /// </summary>
        /// <param name="DENPYO_SHURUI">2：受入　3：出荷　4：売上/支払</param>
        /// <param name="SEIKYU_CD">取引先CD</param>
        /// <param name="SEIKYUSHIMEBI_FROM">[任意] 取得する期間FROM(yyy/MM/dd形式)</param>
        /// <param name="SEIKYUSHIMEBI_TO">取得する期間TO(yyy/MM/dd形式)</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetsujiShori.GetUriageData.sql")]
        DataTable GetUriageData(int DENPYO_SHURUI, string SEIKYU_CD, string SEIKYUSHIMEBI_FROM, string SEIKYUSHIMEBI_TO);

        /// <summary>
        /// 出金伝票データを取得します
        /// </summary>
        /// <param name="SHIHARAI_CD">取引先CD</param>
        /// <param name="SHIHARAISHIMEBI_FROM">[任意] 取得する期間FROM(yyy/MM/dd形式)</param>
        /// <param name="SHIHARAISHIMEBI_TO">取得する期間TO(yyy/MM/dd形式)</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetsujiShori.GetShukkinData.sql")]
        DataTable GetShukkinData(string SHIHARAI_CD, string SHIHARAISHIMEBI_FROM, string SHIHARAISHIMEBI_TO);

        /// <summary>
        /// 入金伝票データを取得します
        /// </summary>
        /// <param name="SEIKYU_CD">取引先CD</param>
        /// <param name="SEIKYUSHIMEBI_FROM">[任意] 取得する期間FROM(yyy/MM/dd形式)</param>
        /// <param name="SEIKYUSHIMEBI_TO">取得する期間TO(yyy/MM/dd形式)</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetsujiShori.GetNyuukinData.sql")]
        DataTable GetNyuukinData(string SEIKYU_CD, string SEIKYUSHIMEBI_FROM, string SEIKYUSHIMEBI_TO);

        /// <summary>
        /// 売上月次処理から締済チェック用データを取得します
        /// </summary>
        /// <param name="whereSql">WHERE条件文</param>
        /// <returns></returns>
        [Sql("SELECT DISTINCT TORIHIKISAKI_CD, YEAR, MONTH FROM T_MONTHLY_LOCK_UR /*$whereSql*/ ORDER BY TORIHIKISAKI_CD")]
        DataTable GetMonthlyLockUrCheckData(string whereSql);

        /// <summary>
        /// 支払月次処理から締済チェック用データを取得します
        /// </summary>
        /// <param name="whereSql">WHERE条件文</param>
        /// <returns></returns>
        [Sql("SELECT DISTINCT TORIHIKISAKI_CD, YEAR, MONTH FROM T_MONTHLY_LOCK_SH /*$whereSql*/ ORDER BY TORIHIKISAKI_CD")]
        DataTable GetMonthlyLockShCheckData(string whereSql);

        /// <summary>
        /// 在庫月次処理から締済チェック用データを取得します
        /// </summary>
        /// <param name="whereSql">WHERE条件文</param>
        /// <returns></returns>
        [Sql("SELECT DISTINCT GYOUSHA_CD, GENBA_CD, ZAIKO_HINMEI_CD, YEAR, MONTH FROM T_MONTHLY_LOCK_ZAIKO /*$whereSql*/ ORDER BY GYOUSHA_CD, GENBA_CD, ZAIKO_HINMEI_CD")]
        DataTable GetMonthlyLockZaikoCheckData(string whereSql);

        /// <summary>
        /// 指定した取引先の指定日付より前の削除されていない最新月次年月でソートしたデータを取得します
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetsujiShori.GetsujiShoriLatestData.sql")]
        DataTable GetLatestGetsujiDateByTorihikisakiCd(string TORIHIKISAKI_CD, string SHIME_DATE);

        /// <summary>
        /// 受入、出荷、売上/支払何れかの売上データを取得します
        /// </summary>
        /// <param name="DENPYO_SHURUI">2：受入　3：出荷　4：売上/支払</param>
        /// <param name="SEIKYU_CD">取引先CD</param>
        /// <param name="SEIKYUSHIMEBI_FROM">[任意] 取得する期間FROM(yyy/MM/dd形式)</param>
        /// <param name="SEIKYUSHIMEBI_TO">取得する期間TO(yyy/MM/dd形式)</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetsujiShori.GetUriageSeikyuuData.sql")]
        DataTable GetUriageSeikyuuData(int DENPYO_SHURUI, string SEIKYU_CD, string SEIKYUSHIMEBI_FROM, string SEIKYUSHIMEBI_TO);

        /// <summary>
        /// 入金伝票データを取得します
        /// </summary>
        /// <param name="SEIKYU_CD">取引先CD</param>
        /// <param name="SEIKYUSHIMEBI_FROM">[任意] 取得する期間FROM(yyy/MM/dd形式)</param>
        /// <param name="SEIKYUSHIMEBI_TO">取得する期間TO(yyy/MM/dd形式)</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetsujiShori.GetNyuukinSeikyuuData.sql")]
        DataTable GetNyuukinSeikyuuData(string SEIKYU_CD, string SEIKYUSHIMEBI_FROM, string SEIKYUSHIMEBI_TO);

        /// <summary>
        /// 受入、出荷、売上/支払何れかの支払データを取得します
        /// </summary>
        /// <param name="DENPYO_SHURUI">2：受入　3：出荷　4：売上/支払</param>
        /// <param name="SHIHARAI_CD">取引先CD</param>
        /// <param name="SHIHARAISHIMEBI_FROM">[任意] 取得する期間FROM(yyy/MM/dd形式)</param>
        /// <param name="SHIHARAISHIMEBI_TO">取得する期間TO(yyy/MM/dd形式)</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetsujiShori.GetShiharaiSeikyuuData.sql")]
        DataTable GetShiharaiSeikyuuData(int DENPYO_SHURUI, string SHIHARAI_CD, string SHIHARAISHIMEBI_FROM, string SHIHARAISHIMEBI_TO);

        /// <summary>
        /// 出金伝票データを取得します
        /// </summary>
        /// <param name="SHIHARAI_CD">取引先CD</param>
        /// <param name="SHIHARAISHIMEBI_FROM">[任意] 取得する期間FROM(yyy/MM/dd形式)</param>
        /// <param name="SHIHARAISHIMEBI_TO">取得する期間TO(yyy/MM/dd形式)</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetsujiShori.GetShukkinShiharaiData.sql")]
        DataTable GetShukkinShiharaiData(string SHIHARAI_CD, string SHIHARAISHIMEBI_FROM, string SHIHARAISHIMEBI_TO);

        #region 在庫月次処理用 chenzz

        /// <summary>
        /// 在庫月次処理データを取得します
        /// </summary>
        /// <param name="data">取得したい業者、現場、在庫品名、年月を設定したDTO</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetsujiShori.GetMonthlyLockZaiko.sql")]
        DataTable GetMonthlyLockZaiko(GetusjiShoriZaikoDTOClass data);

        /// <summary>
        /// 変更在庫量を取得します
        /// </summary>
        /// <param name="data">取得したい業者、現場、在庫品名、年月を設定したDTO</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetsujiShori.GetHenkouZaikoRyou.sql")]
        DataTable GetHenkouZaikoRyou(GetusjiShoriZaikoDTOClass data);

        #endregion

        #endregion
    }

    [Bean(typeof(T_GETSUJI_SHORI_CHU))]
    public interface T_GETSUJI_SHORI_CHUDao : IS2Dao
    {
        /* 月次処理中データはInsert&物理Deleteで管理する。Selectは基本は1件しか取得されない */

        /// <summary>
        /// データを取得します
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM T_GETSUJI_SHORI_CHU")]
        DataTable GetAllData();

        [Sql("SELECT * FROM T_GETSUJI_SHORI_CHU WHERE YEAR = /*YEAR*/ AND MONTH = /*MONTH*/")]
        DataTable GetDataByKey(int YEAR, int MONTH);

        [Sql("SELECT * FROM T_GETSUJI_SHORI_CHU ORDER BY YEAR DESC, MONTH DESC")]
        DataTable GetLatestData();

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_GETSUJI_SHORI_CHU data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_GETSUJI_SHORI_CHU data);
    }
}