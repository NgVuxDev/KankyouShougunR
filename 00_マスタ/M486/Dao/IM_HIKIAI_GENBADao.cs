// $Id: IM_HIKIAI_GENBADao.cs 21035 2014-05-20 14:44:52Z gai $
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.HikiaiGenbaIchiran.Dao
{
    /// <summary>
    /// 引合現場マスタDao
    /// </summary>
    [Bean(typeof(M_HIKIAI_GENBA))]
    public interface IM_HIKIAI_GENBADao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_HIKIAI_GENBA data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_HIKIAI_GENBA data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_HIKIAI_GENBA data);

        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_HIKIAI_GENBA")]
        M_HIKIAI_GENBA[] GetAllData();

        /// <summary>
        /// 現場コードを元に現場情報を取得
        /// </summary>
        /// <param name="data"></param>
        [Query("HIKIAI_GYOUSHA_USE_FLG = /*flg*/ AND GYOUSHA_CD = /*cd1*/ AND GENBA_CD = /*cd2*/")]
        M_HIKIAI_GENBA GetDataByCd(bool flg, string cd1, string cd2);

        /// <summary>
        /// 現場コードを元に現場情報を取得
        /// </summary>
        /// <param name="data"></param>
        [Query("GENBA_CD = /*cd*/")]
        M_HIKIAI_GENBA[] GetDataByCdOnly(string cd);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}