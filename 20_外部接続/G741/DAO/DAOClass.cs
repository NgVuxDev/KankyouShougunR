using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ExternalConnection.GenbamemoNyuryoku
{
    /// <summary>
    /// 現場メモDao
    /// </summary>
    [Bean(typeof(T_GENBAMEMO_ENTRY))]
    internal interface GenbamemoEntryDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_GENBAMEMO_ENTRY data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_GENBAMEMO_ENTRY data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_GENBAMEMO_ENTRY data);

        /// <summary>
        /// 現場メモEntityを取得します
        /// </summary>
        /// <param name="systemId">SYSTEM_ID</param>
        /// <param name="seq">SEQ</param>
        /// <returns>現場メモEntity</returns>
        [Sql("SELECT * FROM T_GENBAMEMO_ENTRY WHERE SYSTEM_ID = /*systemId*/ AND SEQ = /*seq*/ AND DELETE_FLG = 0")]
        T_GENBAMEMO_ENTRY GetDataByKey(string systemId, string seq);

        /// <summary>
        /// 現場メモEntityを取得します(現場メモ番号)
        /// </summary>
        /// <param name="genbamemoNumber">GENBAMEMO_NUMBER</param>
        /// <returns>現場メモEntity</returns>
        [Sql("SELECT * FROM T_GENBAMEMO_ENTRY WHERE GENBAMEMO_NUMBER = /*genbamemoNumber*/ AND DELETE_FLG = 0")]
        T_GENBAMEMO_ENTRY GetDataByGenbamemoNumber(string genbamemoNumber);

        /// <summary>
        /// SQLを実行してデータを取得する。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        DataTable getdateforstringsql(string sql);
    }

    /// <summary>
    /// 現場メモ詳細Dao
    /// </summary>
    [Bean(typeof(T_GENBAMEMO_DETAIL))]
    internal interface GenbamemoDetailDAO : IS2Dao
    {
        /// <summary>
        /// 現場メモ詳細Entityを追加します
        /// </summary>
        /// <param name="tUketsukeSsDetail">追加するEntity</param>
        /// <returns>追加した件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_GENBAMEMO_DETAIL tUketsukeSsDetail);

        /// <summary>
        /// 現場メモ詳細Entityを取得します
        /// </summary>
        /// <param name="systemId">SYSTEM_ID</param>
        /// <param name="seq">SEQ</param>
        /// <returns>現場メモ詳細Entity</returns>
        [Sql("SELECT * FROM T_GENBAMEMO_DETAIL WHERE SYSTEM_ID = /*systemId*/ AND SEQ = /*seq*/")]
        List<T_GENBAMEMO_DETAIL> GetDataByKey(string systemId, string seq);
    }
}
