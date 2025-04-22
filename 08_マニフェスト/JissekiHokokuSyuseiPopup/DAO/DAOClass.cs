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

namespace Shougun.Core.PaperManifest.JissekiHokokuSyuseiPopup.DAO
{

    [Bean(typeof(T_JISSEKI_HOUKOKU_MANIFEST_DETAIL))]
    public interface JissekiHokokuSyuseiPopupDaoCls : IS2Dao
    {
        /// <summary>
        /// マニ明細ポップアップデータを取得する
        /// </summary>
        /// <param name="systemId">SYSTEM_ID</param>
        /// <param name="seq">SEQ</param>
        /// <param name="detailSystemId">DETAIL_SYSTEM_ID</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.JissekiHokokuSyuseiPopup.Sql.GetData.sql")]
        new DataTable GetData(string systemId, string seq, string detailSystemId);

        /// <summary>
        /// マニ明細ポップアップデータの存在チェック
        /// </summary>
        /// <param name="systemId">SYSTEM_ID</param>
        /// <param name="haikiKbn">HAIKI_KBN_CD</param>
        /// <param name="kanriID">KANRI_ID※電マニ(HAIKI_KBN_CD==4)時</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.JissekiHokokuSyuseiPopup.Sql.CheckData.sql")]
        new DataTable CheckData(string systemId, string haikiKbn, string kanriID);
    }
}