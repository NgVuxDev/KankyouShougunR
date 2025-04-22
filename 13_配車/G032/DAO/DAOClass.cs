// $Id: DAOClass.cs 9694 2013-12-05 05:27:09Z sys_dev_22 $
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

namespace Shougun.Core.Allocation.TeikiHaisyaIchiran
{
    [Bean(typeof(T_TEIKI_HAISHA_ENTRY))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        //[Sql("/*$sql*/")]
        //DataTable getdateforstringsql(string sql);
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// 車輌情報データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaisyaIchiran.Sql.GetSharyouDataSql.sql")]
        new DataTable GetSharyouDataSql(string gosyaCd, string sharyouCd);
    }
}
