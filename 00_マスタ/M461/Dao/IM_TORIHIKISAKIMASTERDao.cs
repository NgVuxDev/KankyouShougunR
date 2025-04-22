// 20140708 ria No.947 営業管理機能改修 start
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;
using System.Data.SqlTypes;

namespace Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao
{
    /// <summary>
    /// 通常マスタDao
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI))]
    public interface IM_TORIHIKISAKIMASTERDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_TORIHIKISAKI data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_TORIHIKISAKI data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_TORIHIKISAKI data);

        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_TORIHIKISAKI")]
        M_TORIHIKISAKI[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Torihikisaki.IM_TORIHIKISAKIDao_GetAllValidData.sql")]
        M_TORIHIKISAKI[] GetAllValidData(M_TORIHIKISAKI data);

        /// <summary>
        /// 取引先コードの最大値を取得する
        /// </summary>
        /// <returns>最大値</returns>
        [Sql("SELECT ISNULL(MAX(TORIHIKISAKI_CD),1) FROM M_TORIHIKISAKI  where ISNUMERIC(TORIHIKISAKI_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxKey();

        /// <summary>
        /// 取引先コードの最小値を取得する
        /// </summary>
        /// <returns>最小値</returns>
        [Sql("SELECT ISNULL(MIN(TORIHIKISAKI_CD),1) FROM M_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMinKey();

        /// <summary>
        /// 取引先コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(TORIHIKISAKI_CD),0)+1 FROM M_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxPlusKey();

        /// <summary>
        /// 取引先コードをもとに取引先のデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.MakeMtorikisakiData.sql")]
        [NoPersistentProps("TIME_STAMP")]
        int MoveData(string oldTORIHIKISAKI_CD, string newTORIHIKISAKI_CD, SqlDateTime CREATE_DATE, string CREATE_USER, string CREATE_PC);

    }

    /// <summary>
    /// 通常マスタDao
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI_SEIKYUU))]
    public interface IM_TORIHIKISAKI_SEIKYUUMASTERDao : IS2Dao
    {
        /// <summary>
        /// 取引先コードをもとに取引先のデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.MakeMtorikisakiSeikyuuData.sql")]
        [NoPersistentProps("TIME_STAMP")]
        int MoveData(string oldTORIHIKISAKI_CD, string newTORIHIKISAKI_CD, SqlInt16 NYUUKINSAKI_KBN);
    }

    /// <summary>
    /// 通常マスタDao
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI_SHIHARAI))]
    public interface IM_TORIHIKISAKI_SHIHARAIMASTERDao : IS2Dao
    {
        /// <summary>
        /// 取引先コードをもとに取引先のデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.MakeMtorikisakiShiharaiData.sql")]
        [NoPersistentProps("TIME_STAMP")]
        int MoveData(string oldTORIHIKISAKI_CD, string newTORIHIKISAKI_CD);
    }

    // 20140715 ria EV005233 引合取引先から取引先へ移行する際に入金先・出金先が作成されない start
    /// <summary>
    /// 入金先マスタDao
    /// </summary>
    [Bean(typeof(M_NYUUKINSAKI))]
    public interface IM_NYUUKINSAKIMASTERDao : IS2Dao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_NYUUKINSAKI data);
    }

    /// <summary>
    /// 出金先マスタDao
    /// </summary>
    [Bean(typeof(M_SYUKKINSAKI))]
    public interface IM_SYUKKINSAKIMASTERDao : IS2Dao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SYUKKINSAKI data);
    }
    // 20140715 ria EV005233 引合取引先から取引先へ移行する際に入金先・出金先が作成されない end

}
// 20140708 ria No.947 営業管理機能改修 end