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

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Allocation.ContenaRirekiIchiranHyou
{
    /// <summary>
    /// コンテナ履歴一覧表用Dao
    /// </summary>
    [Bean(typeof(SearchResultDTO))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// コンテナ履歴一覧表用(数量管理)の一覧データを取得
        /// </summary>
        /// <param name="data">DTOClass</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.ContenaRirekiIchiranHyou.Sql.GetIchiranDataSqlForSuuryouKanri.sql")]
        List<SearchResultDTO> GetIchiranDataSqlForSuuryouKanri(DTOClass data);

        /// <summary>
        /// コンテナ履歴一覧表用(個体管理)の一覧データを取得
        /// </summary>
        /// <param name="data">DTOClass</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.ContenaRirekiIchiranHyou.Sql.GetIchiranDataSqlForKotaiKanri.sql")]
        List<SearchResultDTO> GetIchiranDataSqlForKotaiKanri(DTOClass data);
    }
}
