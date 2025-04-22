// $Id: JushoSearchLogic.cs 15807 2014-02-10 07:45:12Z takeda $
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;

namespace r_framework.Logic
{
    /// <summary>
    /// 住所検索ロジック
    /// </summary>
    internal class JushoSearchLogic
    {
        /// <summary>
        /// 都道府県マスタ検索用Dao
        /// </summary>
        private IM_TODOUFUKENDao TodofukenDao;

        /// <summary>
        /// 郵便辞書マスタDao
        /// </summary>
        private IS_ZIP_CODEDao ZipCodeDao;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal JushoSearchLogic()
        {
            TodofukenDao = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            ZipCodeDao = DaoInitUtility.GetComponent<IS_ZIP_CODEDao>();
        }

        /// <summary>
        /// 郵便番号から郵便辞書マスタ取得
        /// </summary>
        /// <param name="post7"></param>
        /// <returns></returns>
        internal S_ZIP_CODE[] GetDataByPost7LikeSearch(string post7)
        {
            // 前方一致
            return ZipCodeDao.GetDataByPost7LikeSearch(post7 + "%");
        }

        /// <summary>
        /// 都道府県名からデータを取得
        /// </summary>
        /// <param name="tdfkName"></param>
        /// <returns></returns>
        internal M_TODOUFUKEN[] GetDataByTdfkName(string tdfkName)
        {
            M_TODOUFUKEN data = new M_TODOUFUKEN();
            data.TODOUFUKEN_NAME = tdfkName;

            return TodofukenDao.GetAllValidData(data);
        }

        /// <summary>
        /// 住所（市町村＋その他１）から郵便辞書マスタ取得
        /// </summary>
        /// <param name="jusho"></param>
        /// <returns></returns>
        internal S_ZIP_CODE[] GetDataByJushoLikeSearch(string jusho)
        {
            // 曖昧検索
            return ZipCodeDao.GetDataByJushoLikeSearch("%" + jusho + "%");
        }

        /// <summary>
        /// 住所（都道府県＋市町村＋その他１）から郵便辞書マスタ取得
        /// </summary>
        /// <param name="jusho"></param>
        /// <returns></returns>
        internal S_ZIP_CODE[] GetDataByTodoufukenJushoLikeSearch(string jusho)
        {
            // 曖昧検索
            return ZipCodeDao.GetDataByTodoufukenJushoLikeSearch("%" + jusho + "%");
        }
    }
}
