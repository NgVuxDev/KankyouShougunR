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
using Shougun.Core.Common.BusinessCommon.Dto;

//// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Common.BusinessCommon.Dao
{
    /// <summary>
    /// DAOクラス
    /// </summary>
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface SyuuryouBiKeikokuDAOClass : IS2Dao
    {
        /// <summary>
        /// 終了日警告データを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetSyuuryouBiKeikokuData.sql")]
        new DataTable GetDataForEntity(SyuuryouBiKeikokuDTOClass data);
    }
}
