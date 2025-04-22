using System.Data;
using System.Data.SqlTypes;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using Shougun.Core.Stock.ZaikoKanriHyo.DTO;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Stock.ZaikoKanriHyo.DAO
{
    /// <summary>
    /// 在庫管理表Dao
    /// </summary>
    [Bean(typeof(T_MONTHLY_LOCK_ZAIKO))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// 在庫品名を取得します
        /// </summary>
        /// <param name="entity">在庫品名CD</param>
        /// <returns>在庫品名</returns>
        [SqlFile("Shougun.Core.Stock.ZaikoKanriHyo.Sql.GetGetsujiData.sql")]
        T_MONTHLY_LOCK_ZAIKO GetGetsujiData(GetsujiDTO data);

        /// <summary>
        /// 明細表示用一覧データテーブルの取得(月次データ取得用)
        /// </summary>
        /// <param name="startCD">開始取引先CD</param>
        /// <param name="endCD">終了取引先CD</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Stock.ZaikoKanriHyo.Sql.GetIchiranData1.sql")]
        DataTable GetIchiranData1(DTOClass data);

        /// <summary>
        /// 明細表示用の取引先マスタテーブルの取得
        /// </summary>
        /// <param name="startCD">開始取引先CD</param>
        /// <param name="endCD">終了取引先CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Stock.ZaikoKanriHyo.Sql.GetIchiranData2.sql")]
        DataTable GetIchiranData2(DTOClass data);

        /// <summary>
        /// 在庫量を取得します
        /// </summary>
        [SqlFile("Shougun.Core.Stock.ZaikoKanriHyo.Sql.GetList.sql")]
        DataTable GetList(string zaikoHinmeiCd, int year, int month);

        /// <summary>
        /// 在庫量を取得します
        /// </summary>
        [SqlFile("Shougun.Core.Stock.ZaikoKanriHyo.Sql.GetZaikoRyou1.sql")]
        DataTable GetZaikoRyou1(string gyoushaCd, string genbaCd, string zaikoHinmeiCd, SqlDateTime dateFrom, SqlDateTime dateTo);
    }
}
