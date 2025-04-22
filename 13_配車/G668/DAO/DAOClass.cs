// $Id: DAOClass.cs 9694 2013-12-05 05:27:09Z sys_dev_22 $
using r_framework.Dao;
using Seasar.Dao.Attrs;
using System.Data;
using System.Data.SqlTypes;
using r_framework.Entity;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Allocation.MobileJoukyouInfo
{
    [Bean(typeof(T_MOBISYO_RT))]
    public interface ITeikihaishaDao : IS2Dao
    {
        /// <summary>
        /// 回収品名情報を取得
        /// </summary>
        /// <param name="haishaDenpyouNo"></param>
        /// <param name="haishaKbn"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouInfo.Sql.GetHinmeiDetail.sql")]
        DataTable GetHinmeiDetail(SqlInt64 haishaDenpyouNo, SqlInt32 haishaKbn);

        /// <summary>
        /// 搬入情報を取得
        /// </summary>
        /// <param name="haishaDenpyouNo"></param>
        /// <param name="haishaKbn"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouInfo.Sql.GetHannyuuDetail.sql")]
        DataTable GetHannyuuDetail(SqlInt64 haishaDenpyouNo, SqlInt32 haishaKbn);

        /// <summary>
        /// コンテナを取得
        /// </summary>
        /// <param name="SEQ_NO"></param>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouInfo.Sql.GetContenaDetail.sql")]
        DataTable GetContenaDetail(SqlInt64 SEQ_NO);

        /// <summary>
        /// コンテナシーケンス番号を取得
        /// </summary>
        /// <param name="haishaDenpyouNo"></param>
        /// <param name="haishaKbn"></param>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouInfo.Sql.GetContenaDetailSEQ.sql")]
        DataTable GetContenaDetailSEQ(SqlInt64 haishaDenpyouNo, SqlInt32 haishaKbn);
    }
}
