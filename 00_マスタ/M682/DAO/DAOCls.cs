using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Data.SqlTypes;
using Shougun.Core.Master.KobetsuHimeiTankaUpdate.Dto;

// http://s2dao.net.seasar.org/ja/index.html
namespace Shougun.Core.Master.KobetsuHimeiTankaUpdate.DAO
{
    [Bean(typeof(MeisaiSearchJoukenDto))]
    public interface IchiranDao : IS2Dao
    {
        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.KobetsuHimeiTankaUpdate.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranData(MeisaiSearchJoukenDto data);

        /// <summary>
        /// キーが同一の明細を取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.KobetsuHimeiTankaUpdate.Sql.GetOnajiKeyMeisaiDataSql.sql")]
        DataTable GetOnajiKeyMeisaiData(MeisaiSearchJoukenDto data);

        /// <summary>
        /// 削除したデータを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.KobetsuHimeiTankaUpdate.Sql.GetDeleteMeisaiDataCountSql.sql")]
        DataTable GetDeleteMeisaiDataCount(MeisaiSearchJoukenDto data);
    }
}