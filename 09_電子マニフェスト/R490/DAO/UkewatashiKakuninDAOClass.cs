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
using Shougun.Core.ElectronicManifest.UkewatashiKakuninHyou.Dto;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ElectronicManifest.UkewatashiKakuninHyou.Dao
{
    /// <summary>
    /// 受渡確認票データを取得
    /// </summary>
    [Bean(typeof(DT_MF_TOC))]
    public interface ManifestInfoDaoCls : IS2Dao
    {
        /// <summary>
        /// マニフェスト目次情報
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.UkewatashiKakuninHyou.Sql.GetManifestTocInfo.sql")]
        new DataTable GetManifestTocInfo(UkewatashiKakuninDTOCls data);

        /// <summary>
        /// 最終処分事業場（予定）情報の取得
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.UkewatashiKakuninHyou.Sql.GetLastSbnPlanInfo.sql")]
        new DataTable GetLastSbnPlanInfo(UkewatashiKakuninDTOCls data);

        /// <summary>
        /// 最終処分終了日・事業場情報の取得
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.UkewatashiKakuninHyou.Sql.GetLastSbnJouInfo.sql")]
        new DataTable GetLastSbnJouInfo(UkewatashiKakuninDTOCls data);

        /// <summary>
        /// 1次マニフェスト情報の取得
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.UkewatashiKakuninHyou.Sql.GetFirstManifestInfo.sql")]
        new DataTable GetFirstManifestInfo(UkewatashiKakuninDTOCls data);
    }
}
