using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using Shougun.Core.ExternalConnection.SmsResult.DTO;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ExternalConnection.SmsResult.DAO
{
    [Bean(typeof(T_SMS))]
    public interface DAOClass : IS2Dao
    {
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SMS data);

        /// <summary>
        /// 全データを取得する（SMSの送信リクエストを正常に送れたデータ）
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM T_SMS WHERE SEND_DATE_R IS NOT NULL")]
        T_SMS[] GetAllData();

        /// <summary>
        /// システムIDをもとにEntity取得
        /// </summary>
        /// <param name="systemId">SYSTEM_ID</param>
        /// <param name="seq">SEQ</param>
        /// <returns>現場メモEntity</returns>
        [Query("SYSTEM_ID = /*systemId*/")]
        T_SMS GetDataBySystemId(string systemId);

        /// <summary>
        /// メッセージIDをもとにEntity取得
        /// </summary>
        /// <param name="systemId">SYSTEM_ID</param>
        /// <param name="seq">SEQ</param>
        /// <returns>現場メモEntity</returns>
        [Query("MESSAGE_ID = /*messageId*/")]
        T_SMS GetDataByMessageId(string messageId);

        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDataForSql(string sql);
    }
}
