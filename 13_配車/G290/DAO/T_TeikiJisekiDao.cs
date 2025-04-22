// $Id: T_TeikiJisekiDao.cs 47097 2015-04-10 08:16:29Z chenzz@oec-h.com $
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

namespace Shougun.Core.Allocation.TeikiHaisyaJisekiIchiran.DAO
{
    [Bean(typeof(T_ZAIKO_TYOUSEI_DETAIL))]
    public interface T_TeikiJisekiDao : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable getdateforstringsql(string sql);
        /// <summary>
        /// 車輌情報データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaisyaJisekiIchiran.Sql.GetSharyouDataSql.sql")]
        new DataTable GetSharyouDataSql(string gosyaCd, string sharyouCd);
    } 
}
