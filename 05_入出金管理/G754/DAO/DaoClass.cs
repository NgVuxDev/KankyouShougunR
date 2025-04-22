using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.ReceiptPayManagement.ShukkinYoteiIchiran
{
    //
    // 画面固有で使用するDaoを定義する
    // アセンブリ内で共通のDaoは共通用のクラスに定義すること
    //

    /// <summary>
    /// 出金予定一覧に出力するデータを取得するインタフェース
    /// </summary>
    [Bean(typeof(T_SEISAN_DENPYOU))]
    internal interface DaoClass : IS2Dao
    {
        /// <summary>
        /// 出金予定一覧に出力するデータを取得します
        /// </summary>
        /// <param name="dto">抽出条件</param>
        /// <returns>抽出結果</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.ShukkinYoteiIchiran.Sql.GetShukkinYoteiIchiranData.sql")]
        DataTable GetShukkinYoteiIchiranData(DtoClass dto);
        /// <summary>
        /// 出金予定一覧に出力するデータを取得します
        /// </summary>
        /// <param name="dto">抽出条件</param>
        /// <returns>抽出結果</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.ShukkinYoteiIchiran.Sql.GetShukkinYoteiData.sql")]
        DataTable GetShukkinYoteiData(DtoClass dto);
    }
}
