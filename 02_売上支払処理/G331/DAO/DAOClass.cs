using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;
using Seasar.Dao.Attrs;

namespace Shougun.Core.SalesPayment.TukigimeUriageDenpyoSakusei.DAO
{
    #region コンストラクタ

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public class DAOClass
    {
        private r_framework.Dao.IM_GENBADao fGenbaDao;

        public DAOClass()
        {
            fGenbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
        }

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA[] GetGenba(string gyoushaCd, string genbaCd)
        {
            if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(genbaCd))
            {
                return null;
            }

            M_GENBA keyEntity = new M_GENBA();
            keyEntity.GYOUSHA_CD = gyoushaCd;
            keyEntity.GENBA_CD = genbaCd;
            keyEntity.ISNOT_NEED_DELETE_FLG = false;
            var genba = this.fGenbaDao.GetAllValidData(keyEntity);

            if (genba == null || genba.Length < 1)
            {
                return null;
            }

            return genba;
        }
    }

    #endregion コンストラクタ

    #region DAO

    /// <summary>
    /// 現場マスタDAO
    /// </summary>
    [Bean(typeof(M_GENBA))]
    public interface MGDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data">現場マスタ</param>
        /// <returns></returns>
        M_GENBA GetDataForEntity(M_GENBA data);

        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.TukigimeUriageDenpyoSakusei.Sql.GetSearch.sql")]
        DataTable GetSearch(DTOClass data);
    }

    /// <summary>
    /// 品名マスタDAO
    /// </summary>
    [Bean(typeof(M_HINMEI))]
    public interface MHDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data">品名マスタ</param>
        /// <returns></returns>
        M_HINMEI GetDataForEntity(M_HINMEI data);
    }

    /// <summary>
    /// 社員マスタDAO
    /// </summary>
    [Bean(typeof(M_SHAIN))]
    public interface MSDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data">社員マスタ</param>
        /// <returns></returns>
        M_SHAIN GetDataForEntity(M_SHAIN data);
    }

    /// <summary>
    /// 消費税マスタDAO
    /// </summary>
    [Bean(typeof(M_SHOUHIZEI))]
    public interface MShoDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data">消費税マスタ</param>
        /// <returns></returns>
        M_SHOUHIZEI GetDataForEntity(M_SHOUHIZEI data);

        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.TukigimeUriageDenpyoSakusei.Sql.GetShouhizei.sql")]
        M_SHOUHIZEI GetShouhizei(string denpyouHiduke);
    }

    /// <summary>
    /// 取引先_請求情報マスタDAO
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI_SEIKYUU))]
    public interface MTSeikyuuDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data">現場マスタ</param>
        /// <returns></returns>
        M_TORIHIKISAKI_SEIKYUU GetDataForEntity(M_TORIHIKISAKI_SEIKYUU data);

        /// <summary>
        /// 取引先CDで取引先請求エンティティを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="shimebi">締日</param>
        /// <returns>取引先請求エンティティ</returns>
        [SqlFile("Shougun.Core.SalesPayment.TukigimeUriageDenpyoSakusei.Sql.GetTorihikisakiSeikyuuByTorihikisakiAndShimebi1.sql")]
        M_TORIHIKISAKI_SEIKYUU GetTorihikisakiSeikyuuByTorihikisakiCdAndShimebi1(string torihikisakiCd, string shimebi);
    }

    /// <summary>
    /// 取引先_支払情報マスタDAO
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI_SHIHARAI))]
    public interface MTShiharaiDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data">現場マスタ</param>
        /// <returns></returns>
        M_TORIHIKISAKI_SHIHARAI GetDataForEntity(M_TORIHIKISAKI_SHIHARAI data);

        /// <summary>
        /// 取引先CDで取引先支払エンティティを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="shimebi">締日</param>
        /// <returns>取引先支払エンティティ</returns>
        [SqlFile("Shougun.Core.SalesPayment.TukigimeUriageDenpyoSakusei.Sql.GetTorihikisakiShiharaiByTorihikisakiAndShimebi1.sql")]
        M_TORIHIKISAKI_SHIHARAI GetTorihikisakiShiharaiByTorihikisakiCdAndShimebi1(string torihikisakiCd, string shimebi);
    }

    /// <summary>
    /// 現場_月極品名マスタDAO
    /// </summary>
    [Bean(typeof(M_GENBA_TSUKI_HINMEI))]
    public interface MGTHDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data">現場_月極品名マスタ</param>
        /// <returns></returns>
        M_GENBA_TSUKI_HINMEI GetDataForEntity(M_GENBA_TSUKI_HINMEI data);
    }

    /// <summary>
    /// 業者マスタDAO
    /// </summary>
    [Bean(typeof(M_GYOUSHA))]
    public interface MGyoushaDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data">業者マスタ</param>
        /// <returns></returns>
        M_GYOUSHA GetDataForEntity(M_GYOUSHA data);
    }

    /// <summary>
    /// 取引先マスタDAO
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI))]
    public interface MTorihikiDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data">取引先マスタ</param>
        /// <returns></returns>
        M_TORIHIKISAKI GetDataForEntity(M_TORIHIKISAKI data);
    }

    /// <summary>
    /// 取引区分マスタDAO
    /// </summary>
    [Bean(typeof(M_TORIHIKI_KBN))]
    public interface MTKDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data">取引区分マスタ</param>
        /// <returns></returns>
        M_TORIHIKI_KBN GetDataForEntity(M_TORIHIKI_KBN data);
    }

    /// <summary>
    /// 売上／支払入力DAO
    /// </summary>
    [Bean(typeof(T_UR_SH_ENTRY))]
    public interface TUSEDaoCls : IS2Dao
    {
        /// <summary>
        /// テーブル追加
        /// </summary>
        /// <param name="data">売上／支払入力</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UR_SH_ENTRY data);
    }

    /// <summary>
    /// 売上／支払明細DAO
    /// </summary>
    [Bean(typeof(T_UR_SH_DETAIL))]
    public interface TUSDDaoCls : IS2Dao
    {
        /// <summary>
        /// テーブル追加
        /// </summary>
        /// <param name="data">売上／支払明細</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UR_SH_DETAIL data);
    }

    #endregion DAO
}