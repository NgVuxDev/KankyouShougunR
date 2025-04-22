using System.Data;
using System.Collections.Generic;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    [Bean(typeof(T_USER_LOGIN))]
    public interface IT_USER_LOGINDao : IS2Dao
    {
        /// <summary>
        /// CAL数の判断と更新/登録処理を行い、更新レコード数とクライアントコンピュータ名(再ログイン時)を返す
        /// ※テーブルロック
        /// </summary>
        /// <returns>LOGIN_COUNTER, CLIENT_COMPUTER_NAME</returns>
        [SqlFile("r_framework.Dao.SqlFile.UserRestrict.IT_USER_LOGINDao_UpdOrInsLoginInfo.sql")]
        T_USER_LOGIN UpdOrInsLoginInfo(int cal, string loginId, string computerName, string userName, bool isTerminal);

        /// <summary>
        /// LOGIN_COUNTERをゼロクリアする
        /// ※テーブルロック
        /// </summary>
        /// <returns>更新レコード数</returns>
        [SqlFile("r_framework.Dao.SqlFile.UserRestrict.IT_USER_LOGINDao_UpdateLoginCounter.sql")]
        int UpdateLoginCounter(string loginId, string computerName, string userName, bool isTerminal);

        /// <summary>
        /// LOGIN_COUNTERを更新する(再ログイン時)
        /// </summary>
        /// <returns>更新レコード数</returns>
        [Sql("UPDATE dbo.T_USER_LOGIN SET " +
             " LOGIN_ID = /*loginCd*/, " + 
             " LOGIN_COUNTER = LOGIN_COUNTER + 1, " + 
             " CLIENT_COMPUTER_NAME = /*computerName*/, " + 
             " CLIENT_USER_NAME = /*userName*/, " + 
             " LOGIN_TIME = CURRENT_TIMESTAMP " + 
             "WHERE " + 
             " /*IF !isTerminal*/" + 
             " CLIENT_COMPUTER_NAME = /*computerName*/" + 
             " /*END*/" + 
             " /*IF isTerminal*/" +
             " CLIENT_USER_NAME = /*userName*/ " +
             " /*END*/")]
        int UpdateLoginCounterReLogin(string loginCd, string computerName, string userName, bool isTerminal);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        new DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// SQL構文からデータの削除を行う
        /// </summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>削除件数</returns>
        [Sql("/*$sql*/")]
        int DeleteDateForStringSql(string sql);
    }
}
