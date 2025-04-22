// $Id: IT_UKETSUKE_SS_DETAILDao.cs 6767 2013-11-13 05:16:21Z sys_dev_20 $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Allocation.TeikiHaishaNyuuryoku
{
    [Bean(typeof(T_UKETSUKE_SS_DETAIL))]
    public interface IT_UKETSUKE_SS_DETAILDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SS_DETAIL data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>件数</returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UKETSUKE_SS_DETAIL data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_UKETSUKE_SS_DETAIL data);        

        /// <summary>
        /// システムIDと枝番をもとに受付（収集）明細のデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetUketsukeSSDetailData.sql")]
        T_UKETSUKE_SS_DETAIL[] GetUketsukeSSDetailData(DTOClass data);
    }
}
