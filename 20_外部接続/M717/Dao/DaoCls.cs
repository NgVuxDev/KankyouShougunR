using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.ExternalConnection.ClientIdNyuuryoku
{
    [Bean(typeof(M_DENSHI_KEIYAKU_CLIENT_ID))]
    public interface DaoCls : IS2Dao
    {
        [Sql("SELECT * FROM M_DENSHI_KEIYAKU_CLIENT_ID")]
        M_DENSHI_KEIYAKU_CLIENT_ID[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_KEIYAKU_CLIENT_ID data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_KEIYAKU_CLIENT_ID data);

        int Delete(M_DENSHI_KEIYAKU_CLIENT_ID data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("SHAIN_CD = /*cd*/")]
        M_DENSHI_KEIYAKU_CLIENT_ID GetDataByCd(string cd);

        /// <summary>
        /// クライアントID入力画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.ClientIdNyuuryoku.Sql.GetIchiranClientIdDataSql.sql")]
        DataTable GetIchiranClientIdDataSql(M_DENSHI_KEIYAKU_CLIENT_ID data, bool deletechuFlg);

        /// <summary>
        /// クライアントID入力画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.ClientIdNyuuryoku.Sql.GetIchiranShainDataSql.sql")]
        DataTable GetIchiranShainDataSql(M_SHAIN data, bool deletechuFlg);
    }
}
