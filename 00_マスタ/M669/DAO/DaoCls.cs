// $Id: DaoCls.cs 48144 2015-04-23 09:11:43Z chenzz@oec-h.com $
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Data;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Master.ContenaQrHakkou.DAO
{
    [Bean(typeof(M_CONTENA))]
    public interface DaoCls : IS2Dao
    {
        /// <summary>
        /// マスタ画面用の一覧データを取得数量管理
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ContenaQrHakkou.Sql.GetIchiranDataSqlForSuuryoukanRi.sql")]
        DataTable GetIchiranDataSqlForSuuryoukanRi(M_CONTENA data);

        /// <summary>
        /// マスタ画面用の一覧データを取得個体管理
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ContenaQrHakkou.Sql.GetIchiranDataSqlForKotaikanRi.sql")]
        DataTable GetIchiranDataSqlForKotaikanRi(M_CONTENA data);
    }

}