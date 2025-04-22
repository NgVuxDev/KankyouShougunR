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

namespace Shougun.Core.Common.InsatsuSettei.DAO
{
    [Bean(typeof(S_DEFAULTMARGINSETTINGS))]
    public interface IS_DEFAULTMARGINSETTINGSDao : IS2Dao
    {
        /// <summary>
        /// デフォルトプリンタマージン取得
        /// </summary>
        /// <param name="REPORTDISPNAME">帳票名</param>
        /// <param name="PRINTERNAME">プリンタ名</param>
        /// <param name="DELETE_FLG">削除フラグ</param>
        /// <returns></returns>
        S_DEFAULTMARGINSETTINGS[] GetDefaultMarginSetting(string REPORTDISPNAME, string PRINTERNAME, bool DELETE_FLG);

        /// <summary>
        /// フォルトプリンタマージンデータ取得
        /// </summary>
        /// <returns></returns>
        S_DEFAULTMARGINSETTINGS[] GetAllDefaultMarginSetting();

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(S_DEFAULTMARGINSETTINGS data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(S_DEFAULTMARGINSETTINGS data);
    }   
}
