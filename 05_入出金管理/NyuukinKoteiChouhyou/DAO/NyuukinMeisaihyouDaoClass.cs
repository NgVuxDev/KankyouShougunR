using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.ReceiptPayManagement.NyuukinKoteiChouhyou
{
    //
    // 画面固有で使用するDaoを定義する
    // アセンブリ内で共通のDaoは共通用のクラスに定義すること
    //

    /// <summary>
    /// 入金明細表に出力するデータを取得するインタフェース
    /// </summary>
    [Bean(typeof(T_NYUUKIN_ENTRY))]
    internal interface INyuukinMeisaihyouDao : IS2Dao
    {
        /// <summary>
        /// 入金明細表に出力するデータを取得します（入金先用）
        /// </summary>
        /// <param name="dto">抽出条件</param>
        /// <returns>抽出結果</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.NyuukinKoteiChouhyou.Sql.GetNyuukinMeisaiDataForNyuukinsaki.sql")]
        DataTable GetNyuukinMeisaiDataForNyuukinsaki(NyuukinMeisaihyouDtoClass dto);

        /// <summary>
        /// 入金明細表に出力するデータを取得します（取引先用）
        /// </summary>
        /// <param name="dto">抽出条件</param>
        /// <returns>抽出結果</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.NyuukinKoteiChouhyou.Sql.GetNyuukinMeisaiDataForTorihikisaki.sql")]
        DataTable GetNyuukinMeisaiDataForTorihikisaki(NyuukinMeisaihyouDtoClass dto);
    }
}
