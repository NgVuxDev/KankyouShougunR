using System.Collections.Generic;
using System.Data;
using Seasar.Dao.Attrs;
using r_framework.Dao;
using r_framework.Entity;

namespace GyoushaHoshu.Dao
{
    [Bean(typeof(T_DENSHI_SHINSEI_ENTRY))]
    public interface IDenshiShinseiEntryDao : IS2Dao
    {
        List<T_DENSHI_SHINSEI_ENTRY> GetDenshiShinseiEntryList(T_DENSHI_SHINSEI_ENTRY entity);

        [SqlFile("GyoushaHoshu.Sql.GetShouninzumiDenshiShinseiEntryList.sql")]
        List<T_DENSHI_SHINSEI_ENTRY> GetShouninzumiDenshiShinseiEntryList(T_DENSHI_SHINSEI_ENTRY entity);

        [SqlFile("GyoushaHoshu.Sql.CheckDeleteGyoushaSql.sql")]
        DataTable GetDataBySqlFileCheck(string GYOUSHA_CD);
    }

    [Bean(typeof(T_DENSHI_SHINSEI_STATUS))]
    public interface IDenshiShinseiStatusDao : IS2Dao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_DENSHI_SHINSEI_STATUS entity);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_DENSHI_SHINSEI_STATUS entity);

        List<T_DENSHI_SHINSEI_STATUS> GetDenshiShinseiStatusList(T_DENSHI_SHINSEI_STATUS entity);
    }

    [Bean(typeof(T_MITSUMORI_ENTRY))]
    public interface IMitsumoriEntryDao : IS2Dao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MITSUMORI_ENTRY entity);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MITSUMORI_ENTRY entity);

        List<T_MITSUMORI_ENTRY> GetMitsumoriEntryList(T_MITSUMORI_ENTRY entity);
    }

    [Bean(typeof(T_MITSUMORI_DETAIL))]
    public interface IMitsumoriDetailDao : IS2Dao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MITSUMORI_DETAIL entity);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MITSUMORI_DETAIL entity);

        List<T_MITSUMORI_DETAIL> GetMitsumoriDetailList(T_MITSUMORI_DETAIL entity);
    }

    [Bean(typeof(M_GENBA))]
    public interface GenbaDao : IS2Dao
    {
        /// <summary>
        /// 移行なら、M_HIKIAI_GENBAに関連データを更新
        /// </summary>
        /// <param name="oldGYOUSHA_CD">oldTORIHIKISAKI_CD</param>
        /// <param name="newGYOUSHA_CD">newTORIHIKISAKI_CD</param>
        [SqlFile("GyoushaHoshu.Sql.UpdateGenbaCD.sql")]
        bool UpdateGenba(string oldGYOUSHA_CD, string newGYOUSHA_CD);
    }
}
