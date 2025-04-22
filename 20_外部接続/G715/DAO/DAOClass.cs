using System.Collections.Generic;
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuNyuryoku
{
    /// <summary>
    /// 電子契約基本
    /// </summary>
    [Bean(typeof(T_DENSHI_KEIYAKU_KIHON))]
    public interface DenshiKeiyakuKihonDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_DENSHI_KEIYAKU_KIHON data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_DENSHI_KEIYAKU_KIHON data);

        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ AND DENSHI_KEIYAKU_SYSTEM_ID = /*denshiSystemId*/ AND DELETE_FLG = 0")]
        T_DENSHI_KEIYAKU_KIHON GetDataByCd(string systemId, string denshiSystemId);

        /// <summary>
        /// システムIDをもとにデータを取得する(削除済も包含)
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ AND DENSHI_KEIYAKU_SYSTEM_ID = /*denshiSystemId*/")]
        T_DENSHI_KEIYAKU_KIHON GetDataByCdContainDel(string systemId, string denshiSystemId);

        /// <summary>
        /// SQLを実行してデータを取得する。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        DataTable getdateforstringsql(string sql);
    }

    /// <summary>
    /// 電子契約送付情報
    /// </summary>
    [Bean(typeof(T_DENSHI_KEIYAKU_SOUHUINFO))]
    public interface DenshiKeiyakuSouhuinfoDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_DENSHI_KEIYAKU_SOUHUINFO data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_DENSHI_KEIYAKU_SOUHUINFO data);

        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ AND DENSHI_KEIYAKU_SYSTEM_ID = /*denshiSystemId*/ AND DELETE_FLG = 0")]
        T_DENSHI_KEIYAKU_SOUHUINFO[] GetDataByCd(string systemId, string denshiSystemId);

        /// <summary>
        /// システムIDをもとにデータを取得する(削除済も包含)
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ AND DENSHI_KEIYAKU_SYSTEM_ID = /*denshiSystemId*/")]
        T_DENSHI_KEIYAKU_SOUHUINFO[] GetDataByCdContainDel(string systemId, string denshiSystemId);
    }

    /// <summary>
    /// 電子契約入力送付先
    /// </summary>
    [Bean(typeof(T_DENSHI_KEIYAKU_SOUHUSAKI))]
    public interface DenshiKeiyakuNyuuryokuSouhusakiDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_DENSHI_KEIYAKU_SOUHUSAKI data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_DENSHI_KEIYAKU_SOUHUSAKI data);

        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ AND DENSHI_KEIYAKU_SYSTEM_ID = /*denshiSystemId*/ AND DELETE_FLG = 0")]
        T_DENSHI_KEIYAKU_SOUHUSAKI[] GetDataByCd(string systemId, string denshiSystemId);
    }

    /// <summary>
    /// 電子契約契約情報
    /// </summary>
    [Bean(typeof(T_DENSHI_KEIYAKU_KEIYAKUINFO))]
    public interface DenshiKeiyakuKeiyakuinfoDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_DENSHI_KEIYAKU_KEIYAKUINFO data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_DENSHI_KEIYAKU_KEIYAKUINFO data);

        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ AND DENSHI_KEIYAKU_SYSTEM_ID = /*denshiSystemId*/ AND DELETE_FLG = 0")]
        T_DENSHI_KEIYAKU_KEIYAKUINFO[] GetDataByCd(string systemId, string denshiSystemId);
    }

    /// <summary>
    /// 電子契約委託情報DAO
    /// </summary>
    [Bean(typeof(DenshiKeiyakuItakuDataDTO))]
    public interface DenshiKeiyakuItakuDataDao : IS2Dao
    {
        /// <summary>
        /// 電子契約委託情報データを取得
        /// </summary>
        /// <param name="data">DTOClass</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.DenshiKeiyakuNyuryoku.Sql.GetDenshiKeiyakuItakuData.sql")]
        List<DenshiKeiyakuItakuDataDTO> GetDenshiKeiyakuItakuData(M_ITAKU_KEIYAKU_KIHON searchData);
    }

    /// <summary>
    /// 電子契約入力の契約情報タブで使用する情報DAO
    /// </summary>
    [Bean(typeof(KeiyakuInfoDataDTO))]
    public interface KeiyakuInfoDataDAO : IS2Dao
    {
        /// <summary>
        /// 電子契約入力の契約情報タブで使用する情報を取得
        /// </summary>
        /// <param name="selectData"></param>
        /// <param name="searchData"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.DenshiKeiyakuNyuryoku.Sql.GetKeiyakuInfoData.sql")]
        List<KeiyakuInfoDataDTO> GetKeiyakuInfoData(M_ITAKU_KEIYAKU_KIHON searchData);
    }

    /// <summary>
    /// 電子契約共有先先
    /// </summary>
    [Bean(typeof(T_DENSHI_KEIYAKU_KYOYUSAKI))]
    public interface DenshiKeiyakuKyoyusakiDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_DENSHI_KEIYAKU_KYOYUSAKI data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_DENSHI_KEIYAKU_KYOYUSAKI data);

        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ AND DENSHI_KEIYAKU_SYSTEM_ID = /*denshiSystemId*/")]
        T_DENSHI_KEIYAKU_KYOYUSAKI[] GetDataByCd(string systemId, string denshiSystemId);
    }
}
