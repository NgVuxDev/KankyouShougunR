using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao.Attrs;
using System.Data;
using System.Data.SqlTypes;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ElectronicManifest.KongouHaikibutsuFuriwake
{
    /// <summary>
    /// T_MANIFEST_RELATION用DAO
    /// </summary>
    [Bean(typeof(T_MANIFEST_RELATION))]
    public interface T_MANIFEST_RELATIONDAOClass : IS2Dao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_RELATION data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_RELATION data);

        /// <summary>
        /// 一次マニの情報から紐付け情報を取得する
        /// </summary>
        /// <param name="firstSystemId">一次マニフェストのsystemid</param>
        /// <returns></returns>
        [Query("DELETE_FLG = 0 AND FIRST_HAIKI_KBN_CD = 4 AND FIRST_SYSTEM_ID = /*firstSystemId*/")]
        List<T_MANIFEST_RELATION> GetDataByFirstSystemId(long firstSystemId);

        [Sql("SELECT TOP 1 * FROM T_MANIFEST_RELATION WHERE DELETE_FLG = 0 AND NEXT_HAIKI_KBN_CD = /*nextHaikiKbnCd*/ AND NEXT_SYSTEM_ID = /*nextSystemId*/ ORDER BY REC_SEQ DESC")]
        T_MANIFEST_RELATION GetMaxReqSeqData(SqlInt64 nextSystemId, SqlInt16 nextHaikiKbnCd);
    }
}
