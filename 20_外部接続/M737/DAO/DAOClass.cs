using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuShoruiInfo
{
    [Bean(typeof(M_DENSHI_KEIYAKU_SHORUI_INFO))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// 全データ取得
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Sql("SELECT * FROM M_DENSHI_KEIYAKU_SHORUI_INFO")]
        M_DENSHI_KEIYAKU_SHORUI_INFO[] GetAllData();

        /// <summary>
        /// 新規登録
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_KEIYAKU_SHORUI_INFO data);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC","TIME_STAMP")]
        int Update(M_DENSHI_KEIYAKU_SHORUI_INFO data);

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        int Delete(M_DENSHI_KEIYAKU_SHORUI_INFO data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <param name="id">SHORUI_INFO_ID</param>
        /// <returns>取得したデータ</returns>
        [Query("SHORUI_INFO_ID = /*id*/")]
        M_DENSHI_KEIYAKU_SHORUI_INFO GetDataByCd(string id);

        /// <summary>
        /// 書類情報入力画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.ExternalConnection.DenshiKeiyakuShoruiInfo.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_DENSHI_KEIYAKU_SHORUI_INFO data, bool deletechuFlg);
    }
}
