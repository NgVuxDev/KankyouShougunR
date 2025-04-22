using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao
{
    /// <summary>
    /// 見積もりDao
    /// </summary>
    [Bean(typeof(T_MITSUMORI_ENTRY))]
    public interface IT_MITSUMORI_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MITSUMORI_ENTRY data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MITSUMORI_ENTRY data);

        /// <summary>
        /// 取引先CDコードをもとに取引先_請求情報マスタのデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        T_MITSUMORI_ENTRY[] GetDataByCd(string cd);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="whereSql">見積もりデータを更新</param>
        /// <returns>0</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.UpdateMitsumoriEntryData.sql")]
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int UpdateTORIHIKISAKICD(string oldtorihikisakicd, string newtorihikisakicd);
    }
}