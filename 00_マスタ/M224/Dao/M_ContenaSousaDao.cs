using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using r_framework.Dao;

namespace Shougun.Core.Master.ContenaSousaHoshu
{
    [Bean(typeof(M_CONTENA_SOUSA))]
    public interface M_ContenaSousaDao : IS2Dao
    {

        [Sql("SELECT * FROM M_CONTENA_SOUSA")]
        M_CONTENA_SOUSA[] GetAllData();

      
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CONTENA_SOUSA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC","TIME_STAMP")]
        int Update(M_CONTENA_SOUSA data);

        int Delete(M_CONTENA_SOUSA data);

 
        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        //DataTable GetDataBySqlFile(string path, M_MANIFEST_TEHAI data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("CONTENA_SOUSA_CD = /*cd*/")]
        M_CONTENA_SOUSA GetDataByCd(string cd);

        /// <summary>
        /// コンテナ状況画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ContenaSousaHoshu.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_CONTENA_SOUSA data, bool deletechuFlg);
    }
}
