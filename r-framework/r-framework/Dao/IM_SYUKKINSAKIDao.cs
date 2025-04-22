using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_SYUKKINSAKI))]
    public interface IM_SYUKKINSAKIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_SYUKKINSAKI")]
        M_SYUKKINSAKI[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Syukkinsaki.IM_SYUKKINSAKIDao_GetAllValidData.sql")]
        M_SYUKKINSAKI[] GetAllValidData(M_SYUKKINSAKI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SYUKKINSAKI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SYUKKINSAKI data);

        int Delete(M_SYUKKINSAKI data);

        /// <summary>
        /// 出金先コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(S.SYUKKINSAKI_CD),0)+1 FROM M_SYUKKINSAKI S LEFT JOIN M_TORIHIKISAKI T ON T.TORIHIKISAKI_CD = S.SYUKKINSAKI_CD  where ISNUMERIC(S.SYUKKINSAKI_CD) = 1 AND (T.SHOKUCHI_KBN IS NULL OR T.SHOKUCHI_KBN = 0)")]
        int GetMaxPlusKey();

        /// <summary>
        /// 出金先コードの最小の空き番を取得する
        /// </summary>
        /// <param name="data">nullを渡す</param>
        /// <returns>最小の空き番</returns>
        [SqlFile("r_framework.Dao.SqlFile.Syukkinsaki.IM_SYUKKINSAKIDao_GetMinBlankNo.sql")]
        int GetMinBlankNo(M_SYUKKINSAKI data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SYUKKINSAKI data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_SYUKKINSAKI data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        /// <summary>
        /// 出金先コードを元にデータの取得を行う
        /// </summary>
        /// <parameparam name="cd">出金先コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("SYUKKINSAKI_CD = /*cd*/")]
        M_SYUKKINSAKI GetDataByCd(string cd);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// 住所の一部データ書き換え機能
        /// </summary>
        /// <param name="path">SQLファイルのパス</param>
        /// <param name="data">出金先マスタエンティティ</param>
        /// <param name="oldPost">旧郵便番号</param>
        /// <param name="oldAddress">旧住所</param>
        /// <param name="newPost">新郵便番号</param>
        /// <param name="newAddress">新住所</param>
        /// <returns></returns>
        int UpdatePartData(string path, M_SYUKKINSAKI data, string oldPost, string oldAddress, string newPost, string newAddress);
    }
}
