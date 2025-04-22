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

namespace Shougun.Core.ElectronicManifest.SousinHoryuSaisyuSyobunhoukoku
{
    [Bean(typeof(T_LAST_SBN_SUSPEND))]
    public interface SearchTLSSDaoCls : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }

    [Bean(typeof(T_LAST_SBN_SUSPEND))]
    public interface UpdateTLSSDaoCls : IS2Dao
    {
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_LAST_SBN_SUSPEND data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_LAST_SBN_SUSPEND data);

    }

    [Bean(typeof(QUE_INFO))]
    public interface UpdateQIDaoCls : IS2Dao
    {
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("REQUEST_CODE", "TUUCHI_ID", "CREATE_DATE", "EDI_RECORD_ID", "SEQ", "FUNCTION_ID", "UPN_ROUTE_NO", "UPDATE_TS")]
        int Update(QUE_INFO data);

    }

    [Bean(typeof(DT_MF_TOC))]
    public interface UpdateDMTDaoCls : IS2Dao
    {
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("MANIFEST_ID", "LATEST_SEQ", "APPROVAL_SEQ", "STATUS_FLAG", "READ_FLAG", "CREATE_DATE", "KIND", "UPDATE_TS")]
        int Update(DT_MF_TOC data);

    }

}
