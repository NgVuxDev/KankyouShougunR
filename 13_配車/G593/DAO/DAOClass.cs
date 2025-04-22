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
using System.Data;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Allocation.KaraContenaIchiranHyou
{
    /// <summary>
    /// 待機コンテナ一覧表で使用するDao
    /// </summary>
    [Bean(typeof(SearchResultDTO))]
    internal interface DAOClass : IS2Dao
    {
        /// <summary>
        /// 待機コンテナ一覧表用のデータを取得(数量管理)
        /// </summary>
        /// <param name="data">絞込条件</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.KaraContenaIchiranHyou.Sql.GetContenaReserveAndResultData.sql")]
        List<SearchResultDTO> GetContenaReserveAndResultData(DTOClass data);

        /// <summary>
        /// 待機コンテナ一覧表用のデータを取得(個体管理)
        /// </summary>
        /// <param name="data">絞込条件</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.KaraContenaIchiranHyou.Sql.GetContenaData.sql")]
        List<SearchResultDTO> GetContenaData(DTOClass data);
    }
}
