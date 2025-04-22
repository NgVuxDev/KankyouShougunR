using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_CHIIKIBETSU_SHOBUN))]
    public interface IM_CHIIKIBETSU_SHOBUNDao : IS2Dao
    {
        
        [Sql("SELECT * FROM M_CHIIKIBETSU_SHOBUN")]
        M_CHIIKIBETSU_SHOBUN[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ChiikibetsuShobun.IM_CHIIKIBETSU_SHOBUNDao_GetAllValidData.sql")]
        M_CHIIKIBETSU_SHOBUN[] GetAllValidData(M_CHIIKIBETSU_SHOBUN data);

        [Sql("select M_CHIIKIBETSU_SHOBUN.SHOBUN_HOUHOU_CD AS CD,M_CHIIKIBETSU_SHOBUN.HOUKOKU_SHOBUN_HOUHOU_NAME AS NAME FROM M_CHIIKIBETSU_SHOBUN /*$whereSql*/ group by M_CHIIKIBETSU_SHOBUN.SHOBUN_HOUHOU_CD,M_CHIIKIBETSU_SHOBUN.HOUKOKU_SHOBUN_HOUHOU_NAME")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CHIIKIBETSU_SHOBUN data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CHIIKIBETSU_SHOBUN data);

        int Delete(M_CHIIKIBETSU_SHOBUN data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_CHIIKIBETSU_SHOBUN data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_CHIIKIBETSU_SHOBUN data, bool deletechuFlg);
    }
}
