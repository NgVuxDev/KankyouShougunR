using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.ReceiptPayManagement.MiShukkinIchiran
{
    //
    // 画面固有で使用するDaoを定義する
    // アセンブリ内で共通のDaoは共通用のクラスに定義すること
    //

    /// <summary>
    /// 未出金一覧表に出力するデータを取得するインタフェース
    /// </summary>
    [Bean(typeof(T_SEISAN_DENPYOU))]
    internal interface DaoClass : IS2Dao
    {
        /// <summary>
        /// 未出金一覧表に出力するデータを取得します(取引先別)
        /// </summary>
        /// <param name="dto">抽出条件</param>
        /// <returns>抽出結果</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.MiShukkinIchiran.Sql.GetMiShukkinIchiranData.sql")]
        DataTable GetMiShukkinIchiranData(DtoClass dto);

        // <summary>
        /// 未出金一覧表に出力するデータを取得します(業者別/現場別)
        /// </summary>
        /// <param name="dto">抽出条件</param>
        /// <returns>抽出結果</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.MiShukkinIchiran.Sql.GetMiShukkinIchiranData2.sql")]
        DataTable GetMiShukkinIchiranData2(DtoClass dto);
    }
}
