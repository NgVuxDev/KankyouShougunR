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
using System.Data;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.BusinessManagement.DenshiShinseiNaiyouSentakuNyuuryoku
{
    /// <summary>
    /// 申請内容選択入力用Dao
    /// </summary>
    [Bean(typeof(M_HIKIAI_GYOUSHA))]
    internal interface DAOClass : IS2Dao
    {
        /// <summary>
        /// コードをもとに引合業者を取得する
        /// </summary>
        /// <param name="cd">検索CD</param>
        /// <returns></returns>
        [Query("GYOUSHA_CD = /*cd*/")]
        M_HIKIAI_GYOUSHA GetHikiaiGyoushaByCd(string cd);

        #region 既存データ取得
        /// <summary>
        /// 既存取引先データの取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenshiShinseiNaiyouSentakuNyuuryoku.Sql.GetTorihikisakiData.sql")]
        DataTable GetTorihikisakiData(DTOClass data);

        /// <summary>
        /// 既存業者データの取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenshiShinseiNaiyouSentakuNyuuryoku.Sql.GetGyoushaData.sql")]
        DataTable GetGyoushaData(DTOClass data);

        /// <summary>
        /// 既存現場データの取得(業者が既存)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenshiShinseiNaiyouSentakuNyuuryoku.Sql.GetGenbaDataForKizonGyousha.sql")]
        DataTable GetGenbaDataForKizonGyousha(DTOClass data);
        #endregion

        #region 引合データ取得
        /// <summary>
        /// 引合取引先データの取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenshiShinseiNaiyouSentakuNyuuryoku.Sql.GetHikiaiTorihikisakiData.sql")]
        DataTable GetHikiaiTorihikisakiData(DTOClass data);

        /// <summary>
        /// 引合業者データの取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenshiShinseiNaiyouSentakuNyuuryoku.Sql.GetHikiaiGyoushaData.sql")]
        DataTable GetHikiaiGyoushaData(DTOClass data);

        /// <summary>
        /// 引合現場データの取得(業者が既存)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenshiShinseiNaiyouSentakuNyuuryoku.Sql.GetHikiaiGenbaDataForKizonGyousha.sql")]
        DataTable GetHikiaiGenbaDataForKizonGyousha(DTOClass data);

        /// <summary>
        /// 引合現場データの取得(業者も引合)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.DenshiShinseiNaiyouSentakuNyuuryoku.Sql.GetHikiaiGenbaDataForHikiaiGyousha.sql")]
        DataTable GetHikiaiGenbaDataForHikiaiGyousha(DTOClass data);
        #endregion
    }
}
