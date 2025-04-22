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

namespace Shougun.Core.PaperManifest.JissekiHokokuIchiran
{
    [Bean(typeof(T_JISSEKI_HOUKOKU_ENTRY))]
    public interface DAOCls : IS2Dao
    {
        /// <summary>
        /// コンテナ情報データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.JissekiHokokuIchiran.Sql.GetJissekiHokokuData.sql")]
        DataTable GetJissekiHokokuData(DTOCls data);

        /// <summary>
        /// コンテナ情報データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.JissekiHokokuIchiran.Sql.GetDeleteJissekiHokokuData.sql")]
        DataTable GetDeleteJissekiHokokuData(string systemid);
    }
}
