using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Master.EigyoTantoushaIkkatsu
{
    [Bean(typeof(M_TORIHIKISAKI))]
    public interface DAO_M_TORIHIKISAKI : IS2Dao
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_TORIHIKISAKI data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_TORIHIKISAKI GetDataByCd(string cd);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.EigyoTantoushaIkkatsu.Sql.M_TORIHIKISAKI_SELECT.sql")]
        new DataTable GetDataForEntity(DTO_EigyouTantou data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.EigyoTantoushaIkkatsu.Sql.M_TORIHIKISAKI_UPDATE.sql")]
        int UpdateEigyouTantou(DTO_M_TORIHIKISAKI data);
    }
    [Bean(typeof(M_GYOUSHA))]
    public interface DAO_M_GYOUSHA : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.EigyoTantoushaIkkatsu.Sql.M_GYOUSHA_SELECT.sql")]
        new DataTable GetDataForEntity(DTO_EigyouTantou data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.EigyoTantoushaIkkatsu.Sql.M_GYOUSHA_UPDATE.sql")]
        int UpdateEigyouTantou(DTO_M_GYOUSHA data);
    }
    [Bean(typeof(M_GENBA))]
    public interface DAO_M_GENBA : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.EigyoTantoushaIkkatsu.Sql.M_GENBA_SELECT.sql")]
        new DataTable GetDataForEntity(DTO_EigyouTantou data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.EigyoTantoushaIkkatsu.Sql.M_GENBA_UPDATE.sql")]
        int UpdateEigyouTantou(DTO_M_GENBA data);
    }
    [Bean(typeof(M_SHAIN))]
    public interface DAO_M_SHAIN : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.EigyoTantoushaIkkatsu.Sql.M_SHAIN.sql")]
        new DataTable GetDataForEntity(DTO_EigyouTantou data);
    }
}
