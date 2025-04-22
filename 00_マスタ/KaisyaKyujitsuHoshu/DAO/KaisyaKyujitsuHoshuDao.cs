// $Id: KaisyaKyujitsuHoshuDao.cs 3868 2013-10-17 01:38:09Z sys_dev_22 $
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

namespace KaisyaKyujitsuHoshu.DAO
{
    [Bean(typeof(M_CORP_CLOSED))]
    public interface KaisyaKyujitsuHoshuDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の条件によるデータ削除
        /// </summary>
        /// <param name="path"></param>
        /// <param name="strdata"></param>
        /// <param name="enddata"></param>
        /// <returns></returns>
        DataTable DelDataBySqlFile(string path, M_CORP_CLOSED strdata, M_CORP_CLOSED enddata);

        /// <summary>
        /// ユーザ指定のSQLファイルによる一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_CORP_CLOSED data);

        /// <summary>
        /// ユーザ指定のSQL文による一覧用データ取得
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
