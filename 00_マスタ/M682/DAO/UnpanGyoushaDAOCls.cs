using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao.Attrs;
using System.Data;

// http://s2dao.net.seasar.org/ja/index.html
namespace Shougun.Core.Master.KobetsuHimeiTankaUpdate.DAO
{
    [Bean(typeof(M_GYOUSHA))]
    public interface UnpanGyoushaDAO : IS2Dao
    {
        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.KobetsuHimeiTankaUpdate.Sql.GetUnpanGyoushaDataSql.sql")]
        DataTable GetUnpanGyoushaData(M_GYOUSHA data);
    }
}