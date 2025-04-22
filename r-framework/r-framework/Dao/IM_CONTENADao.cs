using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_CONTENA))]
    public interface IM_CONTENADao : IS2Dao
    {

        [Sql("SELECT * FROM M_CONTENA")]
        M_CONTENA[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Contena.IM_CONTENADao_GetAllValidData.sql")]
        M_CONTENA[] GetAllValidData(M_CONTENA data);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する(マスタ共通ポップアップ)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Contena.IM_CONTENADao_GetAllValidDataForPopUp.sql")]
        DataTable GetAllValidDataForPopUp(M_CONTENA data);

        int Insert(M_CONTENA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CONTENA data);

        int Delete(M_CONTENA data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, IM_CONTENADao data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <param name="contenaShuruiCd"></param>
        /// <param name="contenaCd"></param>
        /// <returns>取得したデータ</returns>
        [Query("CONTENA_SHURUI_CD = /*data.CONTENA_SHURUI_CD*/ and CONTENA_CD = /*data.CONTENA_CD*/")]
        M_CONTENA GetDataByCd(M_CONTENA data);

        [Sql("select M_CONTENA.CONTENA_CD as CD,M_CONTENA.CONTENA_NAME_RYAKU as NAME FROM M_CONTENA /*$whereSql*/ group by M_CONTENA.CONTENA_CD,M_CONTENA.CONTENA_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);
    }
}
