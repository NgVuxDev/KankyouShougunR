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

namespace Shougun.Core.PaperManifest.HensoSakiAnnaisho
{
    /// <summary>
    /// マニフェスト
    /// </summary>
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        ///交付番号毎と現場毎Detailデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.HensoSakiAnnaisho.Sql.GetIchiranData.sql")]
        new DataTable GetIchiranData(DTOClass data);
        /// <summary>
        ///返却先集計データを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.HensoSakiAnnaisho.Sql.GetHenkyakusakiShukeiData.sql")]
        new DataTable GetHenkyakusakiShukeiData(DTOClass data);

        /// <summary>
        ///廃棄物種類データを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.HensoSakiAnnaisho.Sql.GetHaikiShuruiData.sql")]
        new DataTable GetHaikiShuruiData(DTOClass data);       
        /// <summary>
        ///返却先につて現場マスタ（A～E票）使用可否情報データを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.HensoSakiAnnaisho.Sql.GetGenbaUseInfoData.sql")]
        new DataTable GetGenbaUseInfoData(DTOClass data);

        /// <summary>SQL構文からデータの取得を行う</summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>データーテーブル</returns>
        [Sql("/*$sql*/")]
         DataTable GetDateForStringSql(string sql);       
    }
}
