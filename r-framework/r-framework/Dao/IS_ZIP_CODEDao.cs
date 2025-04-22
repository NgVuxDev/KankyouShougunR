using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Collections.Generic;
namespace r_framework.Dao
{
    /// <summary>
    /// 郵便辞書マスタDao
    /// </summary>
    [Bean(typeof(S_ZIP_CODE))]
    public interface IS_ZIP_CODEDao : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM S_ZIP_CODE")]
        S_ZIP_CODE[] GetAllData();

        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(S_ZIP_CODE data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(S_ZIP_CODE data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        int Delete(S_ZIP_CODE data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, S_ZIP_CODE data);

        /// <summary>
        /// 郵便番号７からLIKE検索によるデータ取得
        /// </summary>
        /// <param name="post7"></param>
        /// <returns></returns>
        [Query("POST7 LIKE /*post7*/ ")]
        S_ZIP_CODE[] GetDataByPost7LikeSearch(string post7);

        /// <summary>
        /// 「市町村＋その他１」からLIKE検索によるデータ取得
        /// </summary>
        /// <param name="jusho"></param>
        /// <returns></returns>
        [Query("SIKUCHOUSON + OTHER1 LIKE /*jusho*/ ")]
        S_ZIP_CODE[] GetDataByJushoLikeSearch(string jusho);

        /// <summary>
        /// 「都道府県＋市町村＋その他１」からLIKE検索によるデータ取得
        /// </summary>
        /// <param name="jusho"></param>
        /// <returns></returns>
        [Query("TODOUFUKEN + SIKUCHOUSON + OTHER1 LIKE /*jusho*/ ")]
        S_ZIP_CODE[] GetDataByTodoufukenJushoLikeSearch(string jusho);

        /// <summary>
        /// 住所の一部データ書き換え機能
        /// </summary>
        /// <param name="path">SQLファイルのパス</param>
        /// <param name="oldPost">旧郵便番号</param>
        /// <param name="oldAddress">旧住所</param>
        /// <param name="newPost">新郵便番号</param>
        /// <param name="newAddress">新住所</param>
        /// <returns></returns>
        int UpdatePartData(string path, string oldPost, string oldAddress, string newPost, string newAddress);

        /// <summary>
        /// 都道府県、市区町村の組み合わせチェック
        /// </summary>
        /// <param name="todofuken">都道府県</param>
        /// <param name="sikuchouson">市区町村</param>
        /// <returns></returns>
        [Query("TODOUFUKEN = /*todofuken*/ AND SIKUCHOUSON = /*sikuchouson*/ ")]
        S_ZIP_CODE[] GetDataByTdkScsSearch(string todofuken, string sikuchouson);

        /// <summary>
        /// 都道府県、市区町村（LIKE検索）の組み合わせチェック
        /// </summary>
        /// <param name="todofuken">都道府県</param>
        /// <param name="sikuchouson">市区町村</param>
        /// <returns></returns>
        [Query("TODOUFUKEN = /*todofuken*/ AND SIKUCHOUSON LIKE /*sikuchouson*/ ")]
        S_ZIP_CODE[] GetDataByTdkScsLikeSearch(string todofuken, string sikuchouson);

        /// <summary>
        /// 「都道府県＋市町村」からLIKE検索による結果数取得
        /// </summary>
        /// <param name="jusho">住所（都道府県＋市町村）</param>
        /// <returns></returns>
        [Sql("SELECT COUNT(*) FROM S_ZIP_CODE WHERE /*jusho*/ LIKE TODOUFUKEN + SIKUCHOUSON + '%'")]
        int GetDataByJushoCountLikeSearch(string jusho);

        /// <summary>
        /// 「都道府県＋市町村＋その他１」からLIKE検索による住所分割チェック
        /// </summary>
        /// <param name="jusho">住所（都道府県＋市町村＋その他１）</param>
        /// <returns></returns>
        [Query("/*jusho*/ LIKE TODOUFUKEN + SIKUCHOUSON + OTHER1 + '%' COLLATE Japanese_CS_AS_KS_WS")]
        List<S_ZIP_CODE> GetDataByJushoSplitLikeSearch(string jusho);
    }
}
