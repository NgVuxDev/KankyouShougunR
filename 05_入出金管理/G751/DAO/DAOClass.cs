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

namespace Shougun.Core.ReceiptPayManagement.ShukkinKeshikomiShusei
{
    [Bean(typeof(T_SHUKKIN_KESHIKOMI))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SHUKKIN_KESHIKOMI data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SHUKKIN_KESHIKOMI data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_SHUKKIN_KESHIKOMI data);

        /// <summary>
        /// GetDataForEntity
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        T_SHUKKIN_KESHIKOMI[] GetDataForEntity(T_SHUKKIN_KESHIKOMI data);

        /// <summary>
        /// 消込明細の取得
        /// </summary>
        /// <param name="entrySumSystemId"></param>
        /// <param name="denpyouDate"></param>
        /// <param name="iSort"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.ShukkinKeshikomiShusei.Sql.GetKeshikomi.sql")]
        DataTable GetKeshikomi(SearchDTO data);

        /// <summary>
        /// MAX(SYSTEM_ID)で取得する。
        /// </summary>
        /// <param name="entrySumSystemId"></param>
        /// <param name="denpyouDate"></param>
        /// <param name="iSort"></param>
        /// <returns></returns>
        //[Sql("SELECT MAX(SYSTEM_ID) FROM T_SHUKKIN_KESHIKOMI")]
        //long GetKeshikomiMaxSystemId();

        /// <summary>
        /// 出金消込.KESHIKOMI_SEQのMAX値を取得
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seisanNumber"></param>
        /// <returns></returns>
        [Sql("SELECT MAX(KESHIKOMI_SEQ) FROM T_SHUKKIN_KESHIKOMI WHERE SYSTEM_ID = /*systemId*/ AND SEISAN_NUMBER = /*seisanNumber*/")]
        int GetKeshikomiMaxSeq(long systemId, long seisanNumber);

        /// <summary>
        /// 指定データを削除
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seisanNumber"></param>
        /// <returns></returns>
        [Sql("UPDATE T_SHUKKIN_KESHIKOMI SET DELETE_FLG = 1 WHERE SYSTEM_ID = /*systemId*/ AND SEISAN_NUMBER = /*seisanNumber*/ AND KESHIKOMI_SEQ = /*keshikomiSeq*/")]
        int DeleteDataByCd(long systemId, long seisanNumber, int keshikomiSeq);
    }
}
