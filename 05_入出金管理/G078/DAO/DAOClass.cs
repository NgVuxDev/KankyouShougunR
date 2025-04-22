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

namespace Shougun.Core.ReceiptPayManagement.NyuuSyutuKinIchiran.DAO
{
    [Bean(typeof(T_NYUUKIN_ENTRY))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable getdateforstringsql(string sql);

        /// <summary>
        /// 明細のheader部分の情報を取得する
        /// </summary>
        /// <param name="dataKbn">入出金区分</param>
        /// <param name="number">入出金番号</param>
        /// <param name="nyuukinKbn">入金区分</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.NyuuSyutuKinIchiran.Sql.GetHeader.sql")]
        DataTable GetDataForEntity(string dataKbn, string number, string nyuukinKbn);
    }

}
