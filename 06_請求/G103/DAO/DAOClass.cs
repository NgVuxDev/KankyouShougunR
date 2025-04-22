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

namespace Shougun.Core.Billing.Seikyuichiran.DAO
{

    [Bean(typeof(T_SEIKYUU_DENPYOU))]
    public interface TSDDaoCls : IS2Dao
    {
        
        [Sql("/*$sql*/")]
        new DataTable getdateforstringsql(string sql);

        /// <summary>
        /// 取引先CDで検索し、最も新しい請求番号を返す
        /// </summary>
        /// <param name="sysId">取引先CD</param>
        /// <returns>最も新しい請求番号</returns>
        [SqlFile("Shougun.Core.Billing.Seikyuichiran.Sql.CheckDelData.sql")]
        int CheckDelData(string torihikisakiCd);

        //PhuocLoc 2021/05/14 #148574 -Start
        [SqlFile("Shougun.Core.Billing.Seikyuichiran.Sql.GetListSeikyuuNumber.sql")]
        long[] GetListSeikyuuNumber(string torihikisakiCd, long seikyuuNumber);
        /// <summary>
        /// コードを元に請求伝票データを取得する
        /// </summary>
        /// <parameparam name="cd">業者コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("SEIKYUU_NUMBER = /*cd*/")]
        T_SEIKYUU_DENPYOU GetDataByCd(string cd);
        /// <summary>
        /// 請求伝票更新
        /// </summary>
        /// <param name="seikyuNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.Seikyuichiran.Sql.UpdateMultiSeikyu.sql")]
        int UpdateMultiSeikyu(long[] arrSeikyuuNumber);
        // <summary>
        /// 請求伝票_鑑更新
        /// </summary>
        /// <param name="seikyuNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.Seikyuichiran.Sql.UpdateMultiSeikyuKagami.sql")]
        int UpdateMultiSeikyuKagami(long[] arrSeikyuuNumber);
        // <summary>
        /// 請求明細
        /// </summary>
        /// <param name="seikyuNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.Seikyuichiran.Sql.UpdateMultiSeikyuDetail.sql")]
        int UpdateMultiSeikyuDetail(long[] arrSeikyuuNumber);
        // <summary>
        /// 入金消込更新
        /// </summary>
        /// <param name="seikyuNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.Seikyuichiran.Sql.UpdateMultiNyukin.sql")]
        int UpdateMultiNyukin(long[] arrSeikyuuNumber);
        //PhuocLoc 2021/05/14 #148574 -End
    }

    //PhuocLoc 2021/05/14 #148574 -Start
    [Bean(typeof(T_NYUUKIN_KESHIKOMI))]
    public interface TNKDao : IS2Dao
    {
        [SqlFile("Shougun.Core.Billing.Seikyuichiran.Sql.CheckDeleteMultiKeshikomi.sql")]
        T_NYUUKIN_KESHIKOMI[] GetDataForEntity(long[] arrSeikyuuNumber);
    }
    //PhuocLoc 2021/05/14 #148574 -End
}
