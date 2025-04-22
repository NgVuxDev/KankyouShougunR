using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Seasar.Dao.Attrs;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.SalesPayment.HannyushutsuIchiran.DAO
{
    [Bean(typeof(T_UKETSUKE_SS_ENTRY))]
    public interface HanshutsuYoteiDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.HannyushutsuIchiran.Sql.GetHanshutsuYoteiDataSql.sql")]
        new DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(T_UKETSUKE_MK_ENTRY))]
    public interface HannyuYoteiDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.HannyushutsuIchiran.Sql.GetHannyuYoteiDataSql.sql")]
        new DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(T_KEIRYOU_ENTRY))]
    public interface KeiryouEntryDaoCls : IS2Dao
    {
        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("UKETSUKE_NUMBER = /*uketsukeNumber*/ AND DELETE_FLG = 0")]
        DataTable GetDataByuketsukeNumber(string uketsukeNumber);
    }

    [Bean(typeof(T_UKEIRE_ENTRY))]
    public interface UkeireEntryDaoCls : IS2Dao
    {
        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("UKETSUKE_NUMBER = /*uketsukeNumber*/ AND DELETE_FLG = 0")]
        DataTable GetDataByuketsukeNumber(string uketsukeNumber);
    }

    [Bean(typeof(T_SHUKKA_ENTRY))]
    public interface ShukkaEntryDaoCls : IS2Dao
    {
        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("UKETSUKE_NUMBER = /*uketsukeNumber*/ AND DELETE_FLG = 0")]
        DataTable GetDataByuketsukeNumber(string uketsukeNumber);
    }

    [Bean(typeof(T_UR_SH_ENTRY))]
    public interface UrShEntryDaoCls : IS2Dao
    {
        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("UKETSUKE_NUMBER = /*uketsukeNumber*/ AND DELETE_FLG = 0")]
        DataTable GetDataByuketsukeNumber(string uketsukeNumber);
    }
}
