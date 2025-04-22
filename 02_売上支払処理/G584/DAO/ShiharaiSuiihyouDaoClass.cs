using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.SalesPayment.ShiharaiSuiiChouhyou
{
    //
    // 画面固有で使用するDaoを定義する
    // アセンブリ内で共通のDaoは共通用のクラスに定義すること
    //

    /// <summary>
    /// 支払推移表に出力するデータを取得するインタフェース
    /// </summary>
    [Bean(typeof(T_UR_SH_ENTRY))]
    internal interface IShiharaiSuiiHyouDao : IS2Dao
    {
        /// <summary>
        /// 支払推移表に出力するデータを取得します
        /// </summary>
        /// <returns>抽出結果</returns>
        [Sql("/*$sql*/")]
        DataTable GetSuiiHyouDataShiharai(string sql);
    }
}
