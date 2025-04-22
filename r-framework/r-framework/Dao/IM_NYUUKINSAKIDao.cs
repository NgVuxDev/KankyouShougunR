using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_NYUUKINSAKI))]
    public interface IM_NYUUKINSAKIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_NYUUKINSAKI")]
        M_NYUUKINSAKI[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Nyuukinsaki.IM_NYUUKINSAKIDao_GetAllValidData.sql")]
        M_NYUUKINSAKI[] GetAllValidData(M_NYUUKINSAKI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_NYUUKINSAKI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_NYUUKINSAKI data);

        int Delete(M_NYUUKINSAKI data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_NYUUKINSAKI data);

        /// <summary>
        /// 入金先コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(N.NYUUKINSAKI_CD),0)+1 FROM M_NYUUKINSAKI N LEFT JOIN M_TORIHIKISAKI T ON T.TORIHIKISAKI_CD = N.NYUUKINSAKI_CD where ISNUMERIC(N.NYUUKINSAKI_CD) = 1 AND (T.SHOKUCHI_KBN IS NULL OR T.SHOKUCHI_KBN = 0)")]
        int GetMaxPlusKey();

        /// <summary>
        /// 入金先コードの最小の空き番を取得する
        /// </summary>
        /// <param name="data">nullを渡す</param>
        /// <returns>最小の空き番</returns>
        [SqlFile("r_framework.Dao.SqlFile.Nyuukinsaki.IM_NYUUKINSAKIDao_GetMinBlankNo.sql")]
        int GetMinBlankNo(M_NYUUKINSAKI data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_NYUUKINSAKI data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        /// <summary>
        /// 入金先コードを元にデータの取得を行う
        /// </summary>
        /// <parameparam name="cd">社員コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("NYUUKINSAKI_CD = /*cd*/")]
        M_NYUUKINSAKI GetDataByCd(string cd);

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
        /// <param name="data">入金先マスタエンティティ</param>
        /// <param name="oldPost">旧郵便番号</param>
        /// <param name="oldAddress">旧住所</param>
        /// <param name="newPost">新郵便番号</param>
        /// <param name="newAddress">新住所</param>
        /// <returns></returns>
        int UpdatePartData(string path, M_NYUUKINSAKI data, string oldPost, string oldAddress, string newPost, string newAddress);
    }
}
