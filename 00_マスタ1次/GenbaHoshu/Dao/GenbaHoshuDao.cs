using System.Collections.Generic;
using Seasar.Dao.Attrs;
using r_framework.Dao;
using r_framework.Entity;
using System.Data;

namespace GenbaHoshu.Dao
{
    [Bean(typeof(T_DENSHI_SHINSEI_ENTRY))]
    public interface IDenshiShinseiEntryDao : IS2Dao
    {
        List<T_DENSHI_SHINSEI_ENTRY> GetDenshiShinseiEntryList(T_DENSHI_SHINSEI_ENTRY entity);

        [SqlFile("GenbaHoshu.Sql.GetShouninzumiDenshiShinseiEntryList.sql")]
        List<T_DENSHI_SHINSEI_ENTRY> GetShouninzumiDenshiShinseiEntryList(T_DENSHI_SHINSEI_ENTRY entity);

        [SqlFile("GenbaHoshu.Sql.CheckDeleteGenbaSql.sql")]
        DataTable GetDataBySqlFileCheck(string GYOUSHA_CD, string GENBA_CD);
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

    [Bean(typeof(M_SMS_RECEIVER))]
    public interface IM_SMS_RECEIVERDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="sqlPath">SQLファイルパス</param>
        /// <returns></returns>
        DataTable GetIchiranBySqlFile(string sqlPath);
    }
}
