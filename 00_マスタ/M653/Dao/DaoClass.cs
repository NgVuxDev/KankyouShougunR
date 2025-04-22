// $Id: DaoClass.cs 56232 2015-07-21 06:20:31Z j-kikuchi $
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.DenManiKansanHoshu
{
    /// <summary>
    /// 電マニ換算値入力用DAO
    /// </summary>
    [Bean(typeof(M_DENSHI_MANIFEST_KANSAN))]
    internal interface DAOClass : IS2Dao
    {
        /// <summary>
        /// 電マニ換算値入力画面用の一覧データを取得
        /// </summary>
        /// <param name="data">検索条件dto</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns name="DataTable">一覧表示用DataTable</returns>
        [SqlFile("Shougun.Core.Master.DenManiKansanHoshu.Sql.getIchiranData.sql")]
        DataTable getIchiranData(findConditionDTO data);
    }
}
