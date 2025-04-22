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
using Shougun.Core.PaperManifest.Manifestmeisaihyo;

namespace Shougun.Core.PaperManifest.Manifestmeisaihyo
{
    /// <summary>
    /// マニフェスト明細表に出力するデータを取得するインタフェース
    /// </summary>
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface IManifestMeisaihyouDao : IS2Dao
    {
        /// <summary>
        /// マニフェスト明細表に出力するデータを取得します
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.Manifestmeisaihyo.Sql.GetManifest.sql")]
        DataTable GetManifestData(ManifestMeisaihyouDto dto);
    }
}
