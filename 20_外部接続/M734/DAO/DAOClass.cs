using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuSaishinShoukai
{
    /// <summary>
    /// 電子契約仮Entry
    /// </summary>
    [Bean(typeof(T_DENSHI_KEIYAKU_KARI_ENTRY))]
    public interface DenshiKeiyakuKariEntryDAO : IS2Dao
    {
        /// <summary>
        /// 全データを取得する
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM T_DENSHI_KEIYAKU_KARI_ENTRY")]
        T_DENSHI_KEIYAKU_KARI_ENTRY[] GetAllData();

        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_DENSHI_KEIYAKU_KARI_ENTRY data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_DENSHI_KEIYAKU_KARI_ENTRY data);

        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("DOCUMENT_ID = /*documentId*/ AND DELETE_FLG = 0")]
        T_DENSHI_KEIYAKU_KARI_ENTRY GetDataByCd(string documentId);

        [Sql("/*$sql*/")]
        DataTable getDateForStringSql(string sql);
    }

    /// <summary>
    /// 電子契約仮Detail
    /// </summary>
    [Bean(typeof(T_DENSHI_KEIYAKU_KARI_DETAIL))]
    public interface DenshiKeiyakuKariDetailDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_DENSHI_KEIYAKU_KARI_DETAIL data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_DENSHI_KEIYAKU_KARI_DETAIL data);
    }
}
