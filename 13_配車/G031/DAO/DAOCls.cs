// $Id: DAOCls.cs 24123 2014-06-27 02:52:37Z sanbongi $
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

namespace Shougun.Core.Allocation.CourseHaishaIraiNyuuryoku
{
    [Bean(typeof(DTOCls))]
    public interface DAOCls : IS2Dao
    {
        /// <summary>
        /// 設置コンテナ一覧画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.CourseHaishaIraiNyuuryoku.Sql.GetIchiranDataSql.sql")]
        new DataTable GetIchiranDataSql(DTOCls data);
    }

    [Bean(typeof(PopupDTOCls))]
    public interface PopupDAOCls : IS2Dao
    {

        /// <summary>
        /// 定期配車検索ポップアップ画面用のデータを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.CourseHaishaIraiNyuuryoku.Sql.GetPopupDataSql.sql")]
        new DataTable GetPopupData(PopupDTOCls data);
        /// <summary>
        /// 定期配車検索ポップアップ画面用のデータを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.CourseHaishaIraiNyuuryoku.Sql.GetCoursePopupDataSql.sql")]
        new DataTable GetCoursePopupData(PopupDTOCls data);
    }
}
