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

namespace Shougun.Core.Master.OboeGakiIkkatuIchiran
{
    [Bean(typeof(T_ITAKU_MEMO_IKKATSU_ENTRY))]
    public interface IM_ITAKUMEMOIKKATSUDao : IS2Dao
    {
        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_ITAKU_MEMO_IKKATSU_ENTRY data);

        /// <summary>
        ///使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_ITAKU_MEMO_IKKATSU_ENTRY data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_ITAKU_MEMO_IKKATSU_ENTRY data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
      //  System.Data.DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.OboeGakiIkkatuIchiran.Sql.GetOboeGakiIchiranDataSql.sql")]
        new DataTable GetDataForEntity(T_ITAKU_MEMO_IKKATSU_ENTRY data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        new DataTable GetDataForStringSql(string sql);

        [Sql("SELECT * FROM T_ITAKU_MEMO_IKKATSU_ENTRY WHERE SYSTEM_ID = /*systemId*/ AND DENPYOU_NUMBER = /*denpyouNmber*/ AND SEQ = /*seq*/")]
        T_ITAKU_MEMO_IKKATSU_ENTRY GetDataByKey(string systemId, string denpyouNmber, string seq);
    }
}
