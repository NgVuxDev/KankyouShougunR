using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 部署マスタのDaoクラス
    /// </summary>
    [Bean(typeof(M_BUSHO))]
    public interface IM_BUSHODao : IS2Dao
    {
        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_BUSHO")]
        M_BUSHO[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Busho.IM_BUSHODao_GetAllValidData.sql")]
        M_BUSHO[] GetAllValidData(M_BUSHO data);

        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_BUSHO data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_BUSHO data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>  
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_BUSHO data);

        /// <summary>
        /// 論理削除フラグ更新処理（"DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC"のみを更新する）
        /// </summary>
        [PersistentProps("DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC")]
        int UpdateLogicalDeleteFlag(M_BUSHO data);

        [Sql("select M_BUSHO.BUSHO_CD as CD,M_BUSHO.BUSHO_NAME_RYAKU as NAME FROM M_BUSHO /*$whereSql*/ group by M_BUSHO.BUSHO_CD,M_BUSHO.BUSHO_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_BUSHO data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] BUSHO_CD);

        /// <summary>
        /// 部署コードをもとに部署のデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("BUSHO_CD = /*cd*/")]
        M_BUSHO GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_BUSHO data, bool deletechuFlg);

    }
}