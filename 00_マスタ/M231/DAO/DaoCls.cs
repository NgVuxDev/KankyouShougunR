using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using r_framework.Dao;
using Shougun.Core.Master.CourseNameHoshu.DTO;

namespace Shougun.Core.Master.CourseNameHoshu.DAO
{
    [Bean(typeof(M_COURSE_NAME))]
    public interface DaoCls : IS2Dao
    {

        [Sql("SELECT * FROM M_COURSE_NAME")]
        M_COURSE_NAME[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.BupanZaikoHokanBasyo.IM_COURSE_NAMEDao_GetAllValidData.sql")]
        M_COURSE_NAME[] GetAllValidData(M_COURSE_NAME data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_COURSE_NAME data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_COURSE_NAME data);

        int Delete(M_COURSE_NAME data);

        [Sql("select M_COURSE_NAME.COURSE_NAME_CD AS CD,M_COURSE_NAME.COURSE_NAME_RYAKU AS NAME FROM M_COURSE_NAME /*$whereSql*/ group by  M_COURSE_NAME.COURSE_NAME_CD,M_COURSE_NAME.COURSE_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);


        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">コース名データ</param>
        /// <returns></returns>
        [Sql("SELECT DISTINCT N'コースマスタ' AS NAME FROM M_COURSE WHERE COURSE_NAME_CD IN /*COURSE_NAME_CD*/('') AND DELETE_FLG = 'False'")]
        DataTable GetDataBySqlFileCheck(string[] COURSE_NAME_CD);

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
        [Query("COURSE_NAME_CD = /*cd*/")]
        M_COURSE_NAME GetDataByCd(string cd);

        /// <summary>
        /// コース名画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.CourseNameHoshu.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(CourseNameDto data, bool deletechuFlg);
    }
}