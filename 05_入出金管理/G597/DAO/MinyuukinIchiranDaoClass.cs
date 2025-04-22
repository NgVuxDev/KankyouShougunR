using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.ReceiptPayManagement.MinyuukinIchiran
{
    //
    // 画面固有で使用するDaoを定義する
    // アセンブリ内で共通のDaoは共通用のクラスに定義すること
    //

    /// <summary>
    /// 未入金一覧に出力するデータを取得するインタフェース
    /// </summary>
    [Bean(typeof(T_SEIKYUU_DENPYOU))]
    internal interface IMinyuukinIchiranDao : IS2Dao
    {
        /// <summary>
        /// 未入金一覧に出力するデータを取得します(取引先別)
        /// </summary>
        /// <param name="dto">抽出条件</param>
        /// <returns>抽出結果</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.MinyuukinIchiran.Sql.GetMinyuukinIchiranData.sql")]
        DataTable GetMinyuukinIchiranData(MinyuukinIchiranDtoClass dto);

        // <summary>
        /// 未入金一覧に出力するデータを取得します(業者別/現場別)
        /// </summary>
        /// <param name="dto">抽出条件</param>
        /// <returns>抽出結果</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.MinyuukinIchiran.Sql.GetMinyuukinIchiranData2.sql")]
        DataTable GetMinyuukinIchiranData2(MinyuukinIchiranDtoClass dto);
    }
}
