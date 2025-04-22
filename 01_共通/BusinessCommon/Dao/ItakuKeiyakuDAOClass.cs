using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using Shougun.Core.Common.BusinessCommon.Dto;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Common.BusinessCommon.Dao
{
    /// <summary>
    /// 委託契約基本マスタ検索
    /// </summary>
    [Bean(typeof(M_ITAKU_KEIYAKU_KIHON))]
    public interface ItakuKeiyakuDAO : IS2Dao
    {
        /// <summary>
        /// 委託契約基本情報データを取得する
        /// </summary>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Check_ItakuKeiyaku.sql")]
        DataTable GetItakuKeiyaku(ItakuKeiyakuDTO data);
    }

    /// <summary>
    /// 電子契約送付先
    /// </summary>
    [Bean(typeof(M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI))]
    public interface DenshiKeiyakuSouhusakiDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI data);

        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/")]
        M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI[] GetDataByCd(string systemId);

        /// <summary>
        /// システムIDをもとに優先番号でソートしたデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ ORDER BY PRIORITY_NO")]
        M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI[] GetDataSortNo(string systemId);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI data);
    }

    /// <summary>
    /// 共有先
    /// </summary>
    [Bean(typeof(M_KYOYUSAKI))]
    public interface KyoyusakiDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KYOYUSAKI data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KYOYUSAKI data);

        /// <summary>
        /// データを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_KYOYUSAKI ORDER BY KYOYUSAKI_CD")]
        M_KYOYUSAKI[] GetAllData();

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_KYOYUSAKI data);
    }

}