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

namespace Shougun.Core.Carriage.UntinSyuusyuuhyoPopup
{

    [Bean(typeof(T_UNCHIN_ENTRY))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UNCHIN_ENTRY data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UNCHIN_ENTRY data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_UNCHIN_ENTRY data);
        /// <summary>
        ///帳票データを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Carriage.UntinSyuusyuuhyoPopup.Sql.GetReportData.sql")]
        new DataTable GetReportData(DtoCls data);

        /// <summary>SQL構文からデータの取得を行う</summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>データーテーブル</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }

}