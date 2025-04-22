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
using SyaryoSentaku.DTO;

// http://s2dao.net.seasar.org/ja/index.html

namespace SyaryoSentaku.DAO
{
    [Bean(typeof(M_SHARYOU))]
    public interface  MSYDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SHARYOU data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SHARYOU data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(M_SHARYOU data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        System.Data.DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("SyaryoSentaku.Sql.GetCarData.sql")]
        // 20140620 ria EV004826 車輌選択画面に検索条件が無い為、車輌の検索ができない start
        //new DataTable GetDataForEntity(M_SHARYOU data);
        new DataTable GetDataForEntity(SerchParameterDtoCls data);
        // 20140620 ria EV004826 車輌選択画面に検索条件が無い為、車輌の検索ができない end

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("SyaryoSentaku.Sql.GetNaviCarData.sql")]
        new DataTable GetNaviDataForEntity(SerchParameterDtoCls data);

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

        // #115564 車輛CDを検索方法を部分一致にしたい start
        [SqlFile("SyaryoSentaku.Sql.GetCarDataMod.sql")]
        new DataTable GetDataForEntityMod(SerchParameterDtoCls data);
        // #115564 車輛CDを検索方法を部分一致にしたい end
    }
}
