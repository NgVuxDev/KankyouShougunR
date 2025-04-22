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

namespace Shougun.Core.Adjustment.Shiharaijimesyorierrorichiran
{
    /// <summary>
    /// 締処理エラーDAO
    /// </summary>
    [Bean(typeof(T_SHIME_SHORI_ERROR))]
    public interface ITSSEDaoCls : IS2Dao
    {
        /// <summary>
        /// テーブル追加
        /// </summary>
        /// <param name="data">締処理エラー</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SHIME_SHORI_ERROR data);

        /// <summary>
        /// テーブル更新
        /// </summary>
        /// <param name="data">締処理エラー</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC","TIME_STAMP")]
        int Update(T_SHIME_SHORI_ERROR data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data">締処理エラー</param>
        /// <returns></returns>
        T_SHIME_SHORI_ERROR GetDataForEntity(T_SHIME_SHORI_ERROR data);

    }
}
