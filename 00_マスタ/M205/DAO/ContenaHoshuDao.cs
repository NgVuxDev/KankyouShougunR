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

namespace Shougun.Core.Master.ContenaHoshu.Dao
{
    [Bean(typeof(M_CONTENA))]
    public interface ContenaDao : IS2Dao
    {


        /// <summary>
        /// Entityで絞り込んでコンテナ情報を取得
        /// </summary>
        /// <param name="data"></param>
        /// <param name="tekiyounaiFlg"></param>
        /// <param name="deletechuFlg"></param>
        /// <param name="tekiyougaiFlg"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ContenaHoshu.Sql.GetContenaData.sql")]
        DataTable GetContenaDataForEntity(DTOClass data,bool deletechuFlg);

        /// <summary>
        /// Entityで絞り込んでコンテナ情報(全件数)を取得
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ContenaHoshu.Sql.GetContenaAllData.sql")]
        DataTable GetContenaAllDataForEntity(M_CONTENA data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CONTENA data);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CONTENA data);

    }

}
