using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_DENSHU_KBN))]
    public interface IM_DENSHU_KBNDao : IS2Dao
    {

        [Sql("SELECT * FROM M_DENSHU_KBN")]
        M_DENSHU_KBN[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.DenshuKbn.IM_DENSHU_KBNDao_GetAllValidData.sql")]
        M_DENSHU_KBN[] GetAllValidData(M_DENSHU_KBN data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHU_KBN data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHU_KBN data);

        int Delete(M_DENSHU_KBN data);

        [Sql("select M_DENSHU_KBN.DENSHU_KBN_CD as CD,M_DENSHU_KBN.DENSHU_KBN_NAME_RYAKU as NAME FROM M_DENSHU_KBN /*$whereSql*/ group by M_DENSHU_KBN.DENSHU_KBN_CD,M_DENSHU_KBN.DENSHU_KBN_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_DENSHU_KBN data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_DENSHU_KBN data, bool deletechuFlg);

        /// <summary>
        /// 社員コードを元にデータの取得を行う
        /// </summary>
        /// <parameparam name="cd">社員コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("DENSHU_KBN_CD = /*cd*/")]
        M_DENSHU_KBN GetDataByCd(string cd);
    }
}
