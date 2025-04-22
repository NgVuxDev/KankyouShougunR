// $Id: M_ZaikoHinmeiDao.cs 11341 2013-12-16 12:04:22Z sys_dev_18 $

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
using Shougun.Core.Stock.ZaikoMeisaiNyuuryoku.DTO;
// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Stock.ZaikoMeisaiNyuuryoku.DAO
{

    [Bean(typeof(M_ZAIKO_HINMEI))]
    public interface M_ZaikoHinmeiDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ZAIKO_HINMEI data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_ZAIKO_HINMEI data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(M_ZAIKO_HINMEI data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        System.Data.DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Stock.ZaikoMeisaiNyuuryoku.Sql.M_ZaikoHinmeiSql.sql")]
        new DataTable GetDataForEntity(F18_G165Dto data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Stock.ZaikoMeisaiNyuuryoku.Sql.GetZaikoHinmei.sql")]
        new DataTable GetZaikoHinmei(M_ZAIKO_HINMEI data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        new DataTable GetDataForStringSql(string sql);
    }
}
