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

namespace Shougun.Core.Master.SaishuShobunBasyoPatternIchiran.DAO
{
    /// <summary>
    /// M420：最終処分場所パターン一覧（最終・中間）
    /// </summary>
    [Bean(typeof(M_SBNB_PATTERN))]
    public interface SaisyuSyoriSyobunBasyoPatternIchiranDao : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        //[Sql("/*$sql*/")]
        //DataTable getdateforstringsql(string sql);
        [Sql("/*$sql*/")]
        new DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// Update ("DELETE_FLG","UPDATE_USER", "UPDATE_DATE", "UPDATE_PC"の更新)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("ITAKU_KEIYAKU_TYPE", "LAST_SBN_KBN", "PATTERN_NAME", "PATTERN_FURIGANA", "GYOUSHA_CD", "GYOUSHA_NAME", "GYOUSHA_ADDRESS", "GENBA_CD", "GENBA_NAME", "GENBA_ADDRESS", "SHOBUN_HOUHOU_CD", "SHORI_SPEC", "OTHER", "HOKAN_JOGEN", "HOKAN_JOGEN_UNIT_CD", "UNPAN_FROM", "UNPAN_END", "KONGOU", "SHUSENBETU", "BUNRUI", "END_KUBUN", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SBNB_PATTERN data);
    }

}
