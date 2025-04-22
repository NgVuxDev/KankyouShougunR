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

namespace r_framework.Dao
{
    /// <summary>
    /// 印刷設定帳票リストDao
    /// </summary>
    [Bean(typeof(S_REPORTLISTPRINTERSETTINGS))]
    public interface IS_REPORTLISTPRINTERSETTINGSDao : IS2Dao
    {
        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="DELETE_FLG">削除フラグ</param>
        /// <returns></returns>
        S_REPORTLISTPRINTERSETTINGS[] GetAllReportList(bool DELETE_FLG);
    }
}
