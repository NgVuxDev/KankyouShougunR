using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.ReceiptPayManagement.NyuukinYoteiIchiran
{
    //
    // 画面固有で使用するDaoを定義する
    // アセンブリ内で共通のDaoは共通用のクラスに定義すること
    //

    /// <summary>
    /// 入金予定一覧に出力するデータを取得するインタフェース
    /// </summary>
    [Bean(typeof(T_SEIKYUU_DENPYOU))]
    internal interface INyuukinYoteiIchiranDao : IS2Dao
    {
        /// <summary>
        /// 入金予定一覧に出力するデータを取得します
        /// </summary>
        /// <param name="dto">抽出条件</param>
        /// <returns>抽出結果</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.NyuukinYoteiIchiran.Sql.GetNyuukinYoteiIchiranData.sql")]
        DataTable GetNyuukinYoteiIchiranData(NyuukinYoteiIchiranDtoClass dto);
        /// <summary>
        /// 入金予定一覧に出力するデータを取得します
        /// </summary>
        /// <param name="dto">抽出条件</param>
        /// <returns>抽出結果</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.NyuukinYoteiIchiran.Sql.GetNyuukinYoteiData.sql")]
        DataTable GetNyuukinYoteiData(NyuukinYoteiIchiranDtoClass dto);
    }
}
