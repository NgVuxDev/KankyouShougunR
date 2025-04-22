using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Master.BankIkkatsu
{
    [Bean(typeof(M_TORIHIKISAKI_SEIKYUU))]
    public interface DAO_M_TORIHIKISAKI_SEIKYUU : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.BankIkkatsu.Sql.M_TORIHIKISAKI_SEIKYUU_SELECT.sql")]
        new DataTable GetDataForEntity(DTO_Bank data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.BankIkkatsu.Sql.M_TORIHIKISAKI_SEIKYUU_UPDATE.sql")]
        int UpdateBank(DTO_Torihikisaki data);
    }
    [Bean(typeof(M_TORIHIKISAKI))]
    public interface DAO_M_TORIHIKISAKI : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.BankIkkatsu.Sql.M_TORIHIKISAKI_UPDATE.sql")]
        int UpdateBank(DTO_Torihikisaki data);
    }
    [Bean(typeof(M_BANK_SHITEN))]
    public interface DAO_M_BANK_SHITEN : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.BankIkkatsu.Sql.M_BANK_SHITEN.sql")]
        new DataTable GetDataForEntity(DTO_Bank data);
    }
}
