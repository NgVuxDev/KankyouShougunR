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
using Shougun.Core.Common.BusinessCommon.Dto;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Common.BusinessCommon.Dao
{
    /// <summary>
    /// 電子申請明細承認否認DAO
    /// 複数の画面から呼ばれるため共通化
    /// </summary>
    [Bean(typeof(T_DENSHI_SHINSEI_DETAIL_ACTION))]
    public interface IT_DENSHI_SHINSEI_DETAIL_ACTIONDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_DENSHI_SHINSEI_DETAIL_ACTION data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_DENSHI_SHINSEI_DETAIL_ACTION data);

        /// <summary>
        /// DELETE_FLGを見ずに電子申請明細承認否認Entityを取得します
        /// </summary>
        /// <param name="detailSystemId">DETAIL_SYSTEM_ID</param>
        /// <param name="seq">SEQ</param>
        /// <returns>電子申請明細承認否認Entity</returns>
        [Sql("SELECT * FROM T_DENSHI_SHINSEI_DETAIL_ACTION WHERE DETAIL_SYSTEM_ID = /*detailSystemId*/ AND SEQ = /*seq*/")]
        T_DENSHI_SHINSEI_DETAIL_ACTION GetDataByKey(string detailSystemId, string seq);
    }

}
