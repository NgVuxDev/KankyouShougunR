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

namespace Shougun.Core.ElectronicManifest.TuusinnRirekiShoukai
{
    /// <summary>
    /// システム設定入力
    /// </summary>
    [Bean(typeof(M_SYS_INFO))]
    public interface GetSysInfoDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.TuusinnRirekiShoukai.Sql.GetSysInfo.sql")]
        new DataTable GetDataForEntity(M_SYS_INFO data);
    }

    [Bean(typeof(DT_R24))]
    public interface GetMeisaiInfoDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.TuusinnRirekiShoukai.Sql.GetMeisaiJyouhou.sql")]
        new DataTable GetDataForEntity(MeisaiInfoDTOCls data);

        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }

}
