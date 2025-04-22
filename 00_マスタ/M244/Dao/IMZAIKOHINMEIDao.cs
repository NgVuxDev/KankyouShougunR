using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using r_framework.Dao;
using Shougun.Core.Master.ZaicohinnmeiHoshu.DTO;
namespace Shougun.Core.Master.ZaicohinnmeiHoshu.Dao
{
    [Bean(typeof(M_ZAIKO_HINMEI))]
    public interface IMZAIKOHINMEIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_ZAIKO_HINMEI")]
        M_CONTENA_SHURUI[] GetAllData();
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ZAIKO_HINMEI data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC","TIME_STAMP")]
        int Update(M_ZAIKO_HINMEI data);
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(M_ZAIKO_HINMEI data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("ZAIKO_HINMEI_CD = /*cd*/")]
        M_ZAIKO_HINMEI GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="data">DTOClass</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ZaicohinnmeiHoshu.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranData(DTOClass data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);
      
    }
}
