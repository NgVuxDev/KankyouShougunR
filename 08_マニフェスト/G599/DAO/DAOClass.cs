using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Dto;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PaperManifest.ManifestSyuuryoubiIchiran
{
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface GetSyuuryobiIchiranDaoCls : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestSyuuryoubiIchiran.Sql.GetSyuuryobiIchiranData.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }

    [Bean(typeof(M_SYS_INFO))]
    public interface M_SYS_INFODaoCls : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestSyuuryoubiIchiran.Sql.GetSyuuryobiNitsuSuu.sql")]
        DataTable GetDataForEntity(TMEDtoCls data);
    }
}
