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

namespace Shougun.Core.PaperManifest.InsatsuBusuSettei
{

    /// <summary>
    /// 交付番号検索
    /// </summary>
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface GetKoufuDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.InsatsuBusuSettei.Sql.SerchKoufu.sql")]
        new DataTable GetDataForEntity(GetKoufuDtoCls data);
    }
}
