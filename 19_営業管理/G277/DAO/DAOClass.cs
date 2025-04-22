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

namespace Shougun.Core.BusinessManagement.MitumoriIchiran.DAO
{
    [Bean(typeof(T_MITSUMORI_ENTRY))]
    public interface TMITSUMORIENTRYDao : IS2Dao
    {
        /// <summary>
        /// 見積一覧の取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitumoriIchiran.Sql.GetMitsumoriItiran.sql")]
        DataTable GetMitumoriIchiran(MitumoriItiranKsjkDTO ksjk);
    }

    [Bean(typeof(T_MITSUMORI_ENTRY))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable getdateforstringsql(string sql);
    }
}
