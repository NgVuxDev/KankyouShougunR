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

namespace Shougun.Core.Common.IchiranSyuDenpyou.DAO
{
    [Bean(typeof(S_OUTPUT_COLUMN_SELECT))]
    public interface GetSOCSDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="NYUKIN_NO"></param>     
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.Common.IchiranSyuDenpyou.Sql.GetSelect.sql")]
        DataTable GetDataForEntity(SOCSDtoCls data);
    }

    [Bean(typeof(M_OUTPUT_PATTERN_HIMO))]
    public interface GetMOPDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="SYSTEM_ID"></param>        
        /// <param name="SEQ"></param>
        /// <param name="OUTPUT_KBN"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.Common.IchiranSyuDenpyou.Sql.GetOutPut.sql")]
        DataTable GetDataForEntity(MOPDtoCls data);
    }

    [Bean(typeof(M_OUTPUT_PATTERN_HIMO))]
    public interface SetMOPDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_OUTPUT_PATTERN_HIMO data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_OUTPUT_PATTERN_HIMO data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(M_OUTPUT_PATTERN_HIMO data);
    }

    [Bean(typeof(M_OUTPUT_PATTERN_COLUMN_HIMO))]
    public interface SetMOPCDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_OUTPUT_PATTERN_COLUMN_HIMO data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_OUTPUT_PATTERN_COLUMN_HIMO data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(M_OUTPUT_PATTERN_COLUMN_HIMO data);
    }
}