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
using System.Data.SqlTypes;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ExternalConnection.SmsNyuuryoku
{
    /// <summary>
    /// ｼｮｰﾄﾒｯｾｰｼﾞDao
    /// </summary>
    [Bean(typeof(T_SMS))]
    internal interface T_SMSDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SMS data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SMS data);

        /// <summary>
        /// SQLを実行してデータを取得する。
        /// </summary>
        /// <param name="sql"></param>
        [Sql("/*$sql*/")]
        DataTable GetDataTableSql(string sql);

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞEntityを取得します
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM T_SMS WHERE SYSTEM_ID = /*systemId*/ AND DENPYOU_NUMBER = /*denpyouNumber*/")]
        T_SMS GetSearchSMSEntity(string systemId, string denpyouNumber);

        /// <summary>
        /// SYSTEM_ID採番
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT ISNULL(MAX(SYSTEM_ID),0)+1 FROM T_SMS")]
        int GetMaxSystemId();

        /// <summary>
        /// システムIDをもとにデータを絞り込む
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/")]
        T_SMS GetDataBySystemId(string systemId);
    }

    /// <summary>
    /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者連携現場Dao
    /// </summary>
    [Bean(typeof(M_SMS_RECEIVER_LINK_GENBA))]
    internal interface SmsReceiverLinkGenbaDao : IS2Dao
    {
        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ入力画面チェック用
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_SMS_RECEIVER_LINK_GENBA WHERE GYOUSHA_CD = /*gyoushaCd*/ AND GENBA_CD = /*genbaCd*/")]
        M_SMS_RECEIVER_LINK_GENBA CheckDataByPhoneNumberAndCd(string gyoushaCd, string genbaCd);
    }
}
