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

namespace Shougun.Core.ReceiptPayManagement.NyuukinDataTorikomi.Dao
{
    [Bean(typeof(T_NYUUKIN_DATA_TORIKOMI))]
    public interface DAOClass : IS2Dao
    {
        [SqlFile("Shougun.Core.ReceiptPayManagement.NyuukinDataTorikomi.Sql.GetNyuukinDataTorikomi.sql")]
        DataTable GetNyuukinDataTorikomi(DTOClass data);

        [SqlFile("Shougun.Core.ReceiptPayManagement.NyuukinDataTorikomi.Sql.GetTorihikisakiData.sql")]
        DataTable GetTorihikisakiData(string torihikisakiCd ,string furikomiName);

        [SqlFile("Shougun.Core.ReceiptPayManagement.NyuukinDataTorikomi.Sql.GetKeshikomiData.sql")]
        DataTable GetKeshikomiData(string torihikisakiCd, DateTime denpyouDate);
    }
}
