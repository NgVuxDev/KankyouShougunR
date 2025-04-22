using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using Shougun.Core.Carriage.UnchinMeisaihyouDto;

namespace Shougun.Core.Carriage.UnchinMeisaihyou.DAO
{
    //
    // 画面固有で使用するDaoを定義する
    // アセンブリ内で共通のDaoは共通用のクラスに定義すること
    //

    /// <summary>
    /// 運賃明細表に出力するデータを取得するインタフェース
    /// </summary>
    [Bean(typeof(T_UNCHIN_ENTRY))]
    internal interface IUnchinMeisaihyouDao : IS2Dao
    {
        /// <summary>
        /// 運賃明細表に出力するデータを取得します
        /// </summary>
        /// <param name="dto">抽出条件</param>
        /// <returns>抽出結果</returns>
        [SqlFile("Shougun.Core.Carriage.UnchinMeisaihyou.Sql.GetUnchinMeisaiData.sql")]
        DataTable GetUnchinMeisaiData(DtoClass dto);
    }
}
