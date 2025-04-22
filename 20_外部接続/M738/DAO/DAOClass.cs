using System.Data;
using Seasar.Dao.Attrs;
using r_framework.Dao;
using r_framework.Entity;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ExternalConnection.ContenaKeikaDate.DAO
{
    [Bean(typeof(M_CONTENA_KEIKA_DATE))]
    public interface DAOClass : IS2Dao
    {
        [Sql("SELECT * FROM M_CONTENA_KEIKA_DATE")]
        M_CONTENA_KEIKA_DATE[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ContenaKeikaDate.IM_CONTENA_KEIKA_DATE_Dao_GetAllValidData.sql")]
        M_CONTENA_KEIKA_DATE[] GetAllValidData(M_CONTENA_KEIKA_DATE data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CONTENA_KEIKA_DATE data);


        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CONTENA_KEIKA_DATE data);

        int Delete(M_CONTENA_KEIKA_DATE data);

        [Sql("select M_CONTENA_KEIKA_DATE.CONTENA_KEIKA_DATE AS CD,M_CONTENA_KEIKA_DATE.CONTENA_KEIKA_BACK_COLOR AS NAME FROM M_CONTENA_KEIKA_DATE /*$whereSql*/ group by  M_CONTENA_KEIKA_DATE.CONTENA_KEIKA_DATE,M_CONTENA_KEIKA_DATE.CONTENA_KEIKA_BACK_COLOR")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_MANIFEST_TEHAI data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("CONTENA_KEIKA_DATE = /*cd*/")]
        M_CONTENA_KEIKA_DATE GetDataByCd(string cd);

        /// <summary>
        /// 入力画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.ContenaKeikaDate.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_CONTENA_KEIKA_DATE data, bool deletechuFlg);
    }
}
