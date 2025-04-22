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

namespace Shougun.Core.BusinessManagement.JuchuuMokuhyouKensuuNyuuryoku
{
    [Bean(typeof(T_JUCHU_M_KENSU))]
    internal interface JuchuuMokuhyouDao : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで部署マスタ値(部署CD.部署略称名)を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.JuchuuMokuhyouKensuuNyuuryoku.Sql.GetBushoData.sql")]
        DataTable GetBushoDataForEntity(M_BUSHO data);

        /// <summary>
        /// Entityで絞り込んで自社情報マスタ値(期首月)を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.JuchuuMokuhyouKensuuNyuuryoku.Sql.GetCorpData.sql")]
        DataTable GetCorpDataForEntity(M_CORP_INFO data);

        /// <summary>
        /// Entityで絞り込んで明細部の表示項目を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.JuchuuMokuhyouKensuuNyuuryoku.Sql.GetDispData.sql")]
        DataTable GetDispDataForEntity(JuchuuMokuhyouDto data);

        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_JUCHU_M_KENSU data);

        /// <summary>
        /// Entityを元にアップデート(論理削除)処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps(
            "MONTH_KENSU_01", "MONTH_KENSU_02", "MONTH_KENSU_03", "MONTH_KENSU_04", "MONTH_KENSU_05",
            "MONTH_KENSU_06", "MONTH_KENSU_07", "MONTH_KENSU_08", "MONTH_KENSU_09", "MONTH_KENSU_10",
            "MONTH_KENSU_11", "MONTH_KENSU_12", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER",
            "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_JUCHU_M_KENSU data);

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
