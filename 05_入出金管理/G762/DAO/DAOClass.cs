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

// http://s2dao.net.seasar.org/ja/index.html

namespace ShukkinDataShutsuryoku
{
    [Bean(typeof(T_SHUKKIN_ENTRY))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SHUKKIN_ENTRY data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SHUKKIN_ENTRY data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        [Sql("SELECT * FROM T_SHUKKIN_ENTRY WHERE SYSTEM_ID = /*systemId*/ AND SEQ = /*seq*/")]
        T_SHUKKIN_ENTRY GetShukkinEntity(Int64 systemId, Int32 seq);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Sql("SELECT GETDATE() AS SYS_DATE")]
        DateTime GetSystemDateTime(string where);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="denpyouDate"></param>
        /// <returns></returns>
        [SqlFile("ShukkinDataShutsuryoku.Sql.GetShukkinData.sql")]
        DataTable GetShukkinData(DTOClass data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gamenId"></param>
        /// <returns></returns>
        [SqlFile("ShukkinDataShutsuryoku.Sql.GetPrevBankData.sql")]
        DataTable GetPrevBankData(string gamenId);
    }
}
