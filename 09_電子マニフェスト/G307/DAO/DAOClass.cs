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

namespace Shougun.Core.ElectronicManifest.SyobunnShuryouHoukokuIkkatuNyuuryoku
{

    [Bean(typeof(DT_R24))]
    public interface GetInputInfoDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.SyobunnShuryouHoukokuIkkatuNyuuryoku.Sql.GetInputInfo.sql")]
        new DataTable GetDataForEntity(GetInputInfoDTOCls data);
    }

    [Bean(typeof(M_SHARYOU))]
    public interface GetSyaryouDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [Sql("/*$sql*/")]
        DataTable GetDataForEntity(string sql);
    }
}
