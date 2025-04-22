using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_NISUGATA))]
    public interface IM_NISUGATADao : MasterAccess.Base.IMasterAccessDao<M_NISUGATA>
    {

        [Sql("SELECT * FROM M_NISUGATA")]
        M_NISUGATA[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Nisugata.IM_NISUGATADao_GetAllValidData.sql")]
        M_NISUGATA[] GetAllValidData(M_NISUGATA data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_NISUGATA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_NISUGATA data);

        int Delete(M_NISUGATA data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_NISUGATA data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] NISUGATA_CD);
        
        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("NISUGATA_CD = /*cd*/")]
        M_NISUGATA GetDataByCd(string cd);

        [Sql("select M_NISUGATA.NISUGATA_CD AS CD,M_NISUGATA.NISUGATA_NAME_RYAKU AS NAME FROM M_NISUGATA /*$whereSql*/ group by M_NISUGATA.NISUGATA_CD,M_NISUGATA.NISUGATA_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_NISUGATA data, bool deletechuFlg);
    }
}
