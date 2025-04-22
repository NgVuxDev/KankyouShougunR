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

namespace Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoPopup
{
    /// <summary>
    /// マニフェスト
    /// </summary>
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        ///帳票データを取得する（紙）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoPopup.Sql.GetReportData.sql")]
        new DataTable GetReportData(DtoCls data);
        /// <summary>
        ///帳票データを取得する（電子）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoPopup.Sql.GetDensiReportData.sql")]
        new DataTable GetDensiReportData(DtoCls data);

        /// <summary>
        ///廃棄物種類データを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoPopup.Sql.GetHaikiShuruiData.sql")]
        new DataTable GetHaikiShuruiData(DtoCls data);  

        /// <summary>SQL構文からデータの取得を行う</summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>データーテーブル</returns>
        [Sql("/*$sql*/")]
         DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// Get Max 提出業者CD
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoPopup.Sql.GetMaxTeishutsuGyousha.sql")]
        new DataTable GetMaxTeishutsuGyousha(DtoCls data);

        /// <summary>
        /// Get Min 提出業者CD
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoPopup.Sql.GetMinTeishutsuGyousha.sql")]
        new DataTable GetMinTeishutsuGyousha(DtoCls data);

        /// <summary>
        /// Get Max 提出事業場CD
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoPopup.Sql.GetMaxTeishutsuJigyoujou.sql")]
        new DataTable GetMaxTeishutsuJigyoujou(DtoCls data);

        /// <summary>
        /// Get Min 提出事業場CD
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoPopup.Sql.GetMinTeishutsuJigyoujou.sql")]
        new DataTable GetMinTeishutsuJigyoujou(DtoCls data);  
    }
}
