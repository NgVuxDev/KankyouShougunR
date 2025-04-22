using System;
using System.Data.SqlTypes;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Function.ShougunCSCommon.Dao
{
    [Bean(typeof(T_UKEIRE_JISSEKI_ENTRY))]
    public interface IT_UKEIRE_JISSEKI_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKEIRE_JISSEKI_ENTRY data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UKEIRE_JISSEKI_ENTRY data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_UKEIRE_JISSEKI_ENTRY data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Function.ShougunCSCommon.Dao.SqlFile.UkeireJissekiEntry.IT_UKEIRE_JISSEKI_ENTRYDao_GetDataForEntity.sql")]
        T_UKEIRE_JISSEKI_ENTRY[] GetDataForEntity(T_UKEIRE_JISSEKI_ENTRY data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// T_UKEIRE_JISSEKI_ENTRY.SEQの最高値を取得する
        /// </summary>
        /// <param name="whereSql">絞り込み条件</param>
        /// <returns>SYSTEM_IDのMAX値</returns>
        [Sql("select ISNULL(MAX(SEQ),0) FROM T_UKEIRE_JISSEKI_ENTRY /*$whereSql*/")]
        SqlInt32 getMaxSEQ(string whereSql);
    }
}
