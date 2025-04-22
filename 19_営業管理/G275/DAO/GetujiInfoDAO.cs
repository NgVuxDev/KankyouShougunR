using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Data;
using Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.Entity;
using Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.Dto;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.Dao
{
    [Bean(typeof(GetujiInfo))]
    internal interface GetujiInfoDAO : IS2Dao
    {
        /// <summary>
        /// 月次情報取得
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.Sql.SearchGetujiInfo.sql")]
        GetujiInfo[] getGetujiInfo(SearchGetujiCondition data);

    }
}
