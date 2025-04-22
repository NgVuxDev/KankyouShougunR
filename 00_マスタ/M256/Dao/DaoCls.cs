using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using r_framework.Dao;

namespace Shougun.Core.Master.ContenaJoukyouHoshu.Dao
{
    [Bean(typeof(M_CONTENA_JOUKYOU))]
    public interface DaoCls : IS2Dao
    {

        [Sql("SELECT * FROM M_CONTENA_JOUKYOU")]
        M_CONTENA_JOUKYOU[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ContenaJoukyou.IM_CONTENA_JOUKYOUDao_GetAllValidData.sql")]
        M_CONTENA_JOUKYOU[] GetAllValidData(M_CONTENA_JOUKYOU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CONTENA_JOUKYOU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC","TIME_STAMP")]
        int Update(M_CONTENA_JOUKYOU data);

        int Delete(M_CONTENA_JOUKYOU data);

        [Sql("select M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_CD AS CD,M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_NAME_RYAKU AS NAME FROM M_CONTENA_JOUKYOU /*$whereSql*/ group by  M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_CD,M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">コース名データ</param>
        /// <returns></returns>
        [Sql("SELECT DISTINCT N'コンテナマスタ' AS NAME FROM M_CONTENA WHERE JOUKYOU_KBN IN /*CONTENA_JOUKYOU_CD*/('') AND DELETE_FLG = 'False'")]
        DataTable GetDataBySqlFileCheck(string[] CONTENA_JOUKYOU_CD);

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
        [Query("CONTENA_JOUKYOU_CD = /*cd*/")]
        M_CONTENA_JOUKYOU GetDataByCd(string cd);

        /// <summary>
        /// コンテナ状況画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        // [SqlFile("Shougun.Core.Master.ContenaJoukyouHoshu.Sql.GetIchiranDataSql.sql")]
        [SqlFile("Shougun.Core.Master.ContenaJoukyouHoshu.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_CONTENA_JOUKYOU data, bool deletechuFlg);
    }
}
