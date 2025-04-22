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

namespace Shougun.Core.BusinessManagement.JuchuuYojitsuKanrihyou
{
    [Bean(typeof(M_OUTPUT_PATTERN))]
    internal interface JuchuuYojitsuDao : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで部署マスタ値(部署CD.部署略称名)を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.JuchuuYojitsuKanrihyou.Sql.GetBushoData.sql")]
        DataTable GetBushoDataForEntity(M_BUSHO data);

        /// <summary>
        /// Entityで絞り込んで自社情報マスタ値(期首月)を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.JuchuuYojitsuKanrihyou.Sql.GetCorpData.sql")]
        DataTable GetCorpDataForEntity(M_CORP_INFO data);

        /// <summary>
        /// Entityで絞り込んで明細部の表示項目を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.JuchuuYojitsuKanrihyou.Sql.GetDispDataGetsuji.sql")]
        DataTable GetDispDataGetsujiForEntity(JuchuuYojitsuDto data);
        /// <summary>
        /// Entityで絞り込んで明細部の表示項目を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.JuchuuYojitsuKanrihyou.Sql.GetDispDataNendo.sql")]
        DataTable GetDispDataNendoForEntity(JuchuuYojitsuDto data);

        /// <summary>
        /// Entityで絞り込んで明細部の表示項目を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.JuchuuYojitsuKanrihyou.Sql.GetHeaderDispData.sql")]
        DataTable GetHeaderDispDataForEntity(JuchuuYojitsuDto data);


        #region デフォルトメソッド

        //public int Insert(SuperEntity data)
        //{
        //    throw new NotImplementedException();
        //}

        //public int Update(SuperEntity data)
        //{
        //    throw new NotImplementedException();
        //}

        //public int Delete(SuperEntity data)
        //{
        //    throw new NotImplementedException();
        //}

        //public System.Data.DataTable GetAllMasterDataForPopup(string whereSql)
        //{
        //    throw new NotImplementedException();
        //}

        //public SuperEntity GetDataForEntity(SuperEntity date)
        //{
        //    throw new NotImplementedException();
        //}

        //public System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data)
        //{
        //    throw new NotImplementedException();
        //}

        //public System.Data.DataTable GetDateForStringSql(string sql)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
    }
}
