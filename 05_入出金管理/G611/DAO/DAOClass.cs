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

namespace Shougun.Core.ReceiptPayManagement.NyukinKeshikomiNyuryoku
{
    [Bean(typeof(T_NYUUKIN_KESHIKOMI))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_NYUUKIN_KESHIKOMI data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_NYUUKIN_KESHIKOMI data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_NYUUKIN_KESHIKOMI data);

        /// <summary>
        /// GetDataForEntity
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        T_NYUUKIN_KESHIKOMI[] GetDataForEntity(T_NYUUKIN_KESHIKOMI data);

        /// <summary>
        /// 消込明細の取得
        /// </summary>
        /// <param name="entrySumSystemId"></param>
        /// <param name="denpyouDate"></param>
        /// <param name="iSort"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.NyukinKeshikomiNyuryoku.Sql.GetKeshikomi.sql")]
        DataTable GetKeshikomi(string torihikisakiCd, string denpyouDate, string nyuukinNumber);

        /// <summary>
        /// 消込明細情報を入金先CDで取得する。
        /// </summary>
        /// <param name="entrySumSystemId"></param>
        /// <param name="denpyouDate"></param>
        /// <param name="iSort"></param>
        /// <returns></returns>
        //[SqlFile("Shougun.Core.ReceiptPayManagement.NyukinKeshikomiNyuryoku.Sql.GetKeshikomiByNyuukinsakiCd.sql")]
        //DataTable GetKeshikomiByNyuukinsakiCd(string nyuukinsakiCd, string denpyouDate);

        /// <summary>
        /// 入金消込.KESHIKOMI_SEQのMAX値を取得
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seikyuuNumber"></param>
        /// <returns></returns>
        [Sql("SELECT MAX(KESHIKOMI_SEQ) FROM T_NYUUKIN_KESHIKOMI WHERE SYSTEM_ID = /*systemId*/ AND SEIKYUU_NUMBER = /*seikyuuNumber*/")]
        int GetKeshikomiMaxSeq(long systemId, long seikyuuNumber);

        /// <summary>
        /// MAX(SYSTEM_ID)で取得する。
        /// </summary>
        /// <param name="entrySumSystemId"></param>
        /// <param name="denpyouDate"></param>
        /// <param name="iSort"></param>
        /// <returns></returns>
        //[Sql("SELECT MAX(SYSTEM_ID) FROM T_NYUUKIN_KESHIKOMI")]
        //long GetKeshikomiMaxSystemId();

        /// <summary>
        /// 指定データを削除
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seikyuuNumber"></param>
        /// <returns></returns>
        [Sql("UPDATE T_NYUUKIN_KESHIKOMI SET DELETE_FLG = 1 WHERE SYSTEM_ID = /*systemId*/ AND SEIKYUU_NUMBER = /*seikyuuNumber*/ AND KESHIKOMI_SEQ = /*keshikomiSeq*/")]
        int DeleteDataByCd(long systemId, long seikyuuNumber, int keshikomiSeq);
    }

    [Bean(typeof(T_NYUUKIN_ENTRY))]
    public interface NyuukinEntryDAOClass : IS2Dao
    {
        /// <summary>
        /// 入金入力データの取得
        /// </summary>
        /// <param name="systemId"></param>
        /// <returns></returns>
        [Sql("SELECT * FROM T_NYUUKIN_ENTRY WHERE NYUUKIN_SUM_SYSTEM_ID = /*sumSystemId*/ AND DELETE_FLG = 0")]
        List<T_NYUUKIN_ENTRY> GetNyuukinEntryList(long sumSystemId);
    }
}
