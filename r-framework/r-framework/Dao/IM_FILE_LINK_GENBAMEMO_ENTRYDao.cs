using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seasar.Dao.Attrs;
using r_framework.Entity;
using System.Data;

namespace r_framework.Dao
{
    [Bean(typeof(M_FILE_LINK_GENBAMEMO_ENTRY))]
    public interface IM_FILE_LINK_GENBAMEMO_ENTRYDao : IS2Dao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_FILE_LINK_GENBAMEMO_ENTRY data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_FILE_LINK_GENBAMEMO_ENTRY data);

        int Delete(M_FILE_LINK_GENBAMEMO_ENTRY data);

        [Query("SYSTEM_ID = /*systemId*/")]
        List<M_FILE_LINK_GENBAMEMO_ENTRY> GetDataBySystemId(string systemId);

        /// <summary>
        /// SQLを実行してデータを取得する。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
