using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using r_framework.Dao;

namespace Shougun.Core.Master.ContenaShuruiHoshu.Dao
{
    [Bean(typeof(M_CONTENA_SHURUI))]
    public interface DaoCls : IS2Dao
    {

        [Sql("SELECT * FROM M_CONTENA_SHURUI")]
        M_CONTENA_SHURUI[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ContenaShurui.IM_CONTENA_SHURUIDao_GetAllValidData.sql")]
        M_CONTENA_SHURUI[] GetAllValidData(M_CONTENA_SHURUI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CONTENA_SHURUI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC","TIME_STAMP")]
        int Update(M_CONTENA_SHURUI data);

        int Delete(M_CONTENA_SHURUI data);

        [Sql("select M_CONTENA_SHURUI.CONTENA_SHURUI_CD AS CD,M_CONTENA_SHURUI.CONTENA_SHURUI_NAME_RYAKU AS NAME FROM M_CONTENA_SHURUI /*$whereSql*/ group by  M_CONTENA_SHURUI.CONTENA_SHURUI_CD,M_CONTENA_SHURUI.CONTENA_SHURUI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_CONTENA_SHURUI data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("CONTENA_SHURUI_CD = /*cd*/")]
        M_CONTENA_SHURUI GetDataByCd(string cd);

        /// <summary>
        /// コンテナ種類画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ContenaShuruiHoshu.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_CONTENA_SHURUI data,bool deletechuFlg);

        /// <summary>
        /// コンテナ種類画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ContenaShuruiHoshu.Sql.CheckDeleteContenaSql.sql")]
        DataTable GetDataContena(string[] CONTENA_SHURUI_CD);
    }
}
