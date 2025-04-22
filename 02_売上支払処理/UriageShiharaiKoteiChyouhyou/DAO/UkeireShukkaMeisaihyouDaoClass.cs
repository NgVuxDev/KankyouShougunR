using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.SalesPayment.UriageShiharaiKoteiChouhyou.DAO
{
    //
    // 画面固有で使用するDaoを定義する
    // アセンブリ内で共通のDaoは共通用のクラスに定義すること
    //

    /// <summary>
    /// 売上支払明細表に出力するデータを取得するインタフェース
    /// </summary>
    [Bean(typeof(T_UKEIRE_ENTRY))]
    internal interface IUkeireShukkaMeisaihyouDao : IS2Dao
    {
        /// <summary>
        /// 受入出荷明細表に出力するデータを取得します
        /// </summary>
        /// <param name="dto">抽出条件</param>
        /// <returns>抽出結果</returns>
        [SqlFile("Shougun.Core.SalesPayment.UriageShiharaiKoteiChouhyou.Sql.GetUkeireShukkaMeisaiData.sql")]
        DataTable GetUkeireShukkaMeisaiData(UkeireShukkaMeisaihyouDtoClass dto);
    }
}
