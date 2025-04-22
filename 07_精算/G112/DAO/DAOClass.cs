using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Seasar.Dao.Attrs;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Adjustment.Shiharaiichiran
{
    [Bean(typeof(T_SEISAN_DENPYOU))]
    public interface TSDDaoCls : IS2Dao
    {
        
        [Sql("/*$sql*/")]
        new DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// 取引先CDで検索し、最も新しい請求番号を返す
        /// </summary>
        /// <param name="sysId">取引先CD</param>
        /// <returns>最も新しい請求番号</returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaiichiran.Sql.CheckDelData.sql")]
        int CheckDelData(string torihikisakiCd);

        //PhuocLoc 2021/05/14 #148575 -Start
        [SqlFile("Shougun.Core.Adjustment.Shiharaiichiran.Sql.GetListShiharaiNumber.sql")]
        long[] GetListShiharaiNumber(string torihikisakiCd, long ShiharaiNumber);
        /// <summary>
        /// コードを元に清算伝票データを取得する
        /// </summary>
        /// <parameparam name="cd">業者コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("SEISAN_NUMBER = /*cd*/")]
        T_SEISAN_DENPYOU GetDataByCd(string cd);
        /// <summary>
        /// 清算伝票更新
        /// </summary>
        /// <param name="ShiharaiNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaiichiran.Sql.UpdateMultiShiharai.sql")]
        int UpdateMultiShiharai(long[] arrShiharaiNumber);
        // <summary>
        /// 清算伝票_鑑更新
        /// </summary>
        /// <param name="ShiharaiNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaiichiran.Sql.UpdateMultiShiharaiKagami.sql")]
        int UpdateMultiShiharaiKagami(long[] arrShiharaiNumber);
        // <summary>
        /// 清算明細
        /// </summary>
        /// <param name="ShiharaiNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaiichiran.Sql.UpdateMultiShiharaiDetail.sql")]
        int UpdateMultiShiharaiDetail(long[] arrShiharaiNumber);
        //PhuocLoc 2021/05/14 #148575 -End
    }

}
