using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Collections.Generic;

namespace Shougun.Core.ExternalConnection.KyoyusakiNyuuryoku.DAO
{
    [Bean(typeof(M_KYOYUSAKI))]
    public interface DAOClass : IS2Dao
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
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_KYOYUSAKI data);

        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_KYOYUSAKI")]
        M_KYOYUSAKI[] GetAllData();

        /// <summary>
        /// 共有先入力画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.KyoyusakiNyuuryoku.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_KYOYUSAKI data, bool deletechuFlg);

        /// <summary>
        /// 共有先CDを元にデータを取得
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("KYOYUSAKI_CD = /*cd*/")]
        M_KYOYUSAKI GetDataByCd(string cd);

        /// <summary>
        /// メールアドレスを元にデータを取得
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [Query("KYOYUSAKI_MAIL_ADDRESS = /*address*/")]
        List<M_KYOYUSAKI> GetDataByAdress(string address);

        /// <summary>
        /// 共有先CDの最大値を取得する
        /// </summary>
        /// <returns>最大値</returns>
        [Sql("SELECT ISNULL(MAX(KYOYUSAKI_CD), 0) FROM M_KYOYUSAKI")]
        int GetMaxKey();
    }
}
