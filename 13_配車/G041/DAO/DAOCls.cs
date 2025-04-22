// $Id: DAOCls.cs 31270 2014-09-30 08:28:42Z takeda $
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

namespace Shougun.Core.Allocation.ContenaIchiran
{
    [Bean(typeof(SearchResult))]
    public interface DAOCls : IS2Dao
    {
        /// <summary>
        /// 設置コンテナ一覧画面用(固体管理)の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.ContenaIchiran.Sql.GetIchiranDataSql.sql")]
        List<SearchResult> GetIchiranDataSql(DTOCls data);

        /// <summary>
        /// 設置コンテナ一覧画面用(固体管理)の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.ContenaIchiran.Sql.GetIchiranJissekiDataSql.sql")]
        List<SearchResult> GetIchiranJissekiDataSql(DTOCls data);

        /// <summary>
        /// 現場情報データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.ContenaIchiran.Sql.GetGenbrDataSql.sql")]
        new DataTable GetGenbrDataSql(DTOCls data);

        /// <summary>
        /// コンテナ情報データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.ContenaIchiran.Sql.GetContenaDataSql.sql")]
        new DataTable GetContenaDataSql(DTOCls data);

        /// <summary>
        /// コンテナ情報データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.ContenaIchiran.Sql.GetContenaDataByCDSql.sql")]
        new DataTable GetContenaDataSqlByCD(DTOCls data);
    }

    [Bean(typeof(SearchResult))]
    public interface SuuryouKanriDAOClass : IS2Dao
    {
        /// <summary>
        /// 設置コンテナ一覧画面用(数量管理)の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.ContenaIchiran.Sql.GetIchiranDataSqlForSuuryouKanri.sql")]
        List<SearchResult> GetIchiranDataSqlForSuuryouKanri(DTOCls data);
    }
}
