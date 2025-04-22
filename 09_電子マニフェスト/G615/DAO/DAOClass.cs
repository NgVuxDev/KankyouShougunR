// $Id: DAOClass.cs 36525 2014-12-04 08:56:53Z j-kikuchi $
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.ElectronicManifest.KongouHaikibutsuJoukyouIchiran
{
    /// <summary>
    /// 混合廃棄物状況一覧用DAO
    /// </summary>
    [Bean(typeof(M_OUTPUT_PATTERN))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// 一覧表示用Data抽出
        /// </summary>
        /// <param name="data">抽出条件</param>
        /// <returns name="DataTable">一覧表示用DataTable</returns>
        [SqlFile("Shougun.Core.ElectronicManifest.KongouHaikibutsuJoukyouIchiran.Sql.getIchiranData.sql")]
        DataTable getIchiranData(findConditionDTO data);

        /// <summary>
        /// 指定したSQL構文を発行する
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns name="DataTable">DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
