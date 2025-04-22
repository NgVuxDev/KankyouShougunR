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
using Shougun.Core.BusinessManagement.EigyoYosanNyuuryoku.DTO;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.BusinessManagement.EigyoYosanNyuuryoku.DAO
{
    [Bean(typeof(T_EIGYO_YOSAN))]
    internal interface EigyoYosanNyuuryokuDao : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで部署マスタ値(部署CD.部署略称名)を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.EigyoYosanNyuuryoku.Sql.GetBushoData.sql")]
        DataTable GetBushoDataForEntity(M_BUSHO data);

        /// <summary>
        /// Entityで絞り込んで自社情報マスタ値(期首月)を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.EigyoYosanNyuuryoku.Sql.GetCorpData.sql")]
        DataTable GetCorpDataForEntity(M_CORP_INFO data);

        /// <summary>
        /// Entityで絞り込んで明細部の表示項目を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.EigyoYosanNyuuryoku.Sql.GetDispData.sql")]
        DataTable GetDispDataForEntity(EigyoYosanDto data);

        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_EIGYO_YOSAN data);

        /// <summary>
        /// Entityを元にアップデート(論理削除)処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps(
            "MONTH_YOSAN_01", "MONTH_YOSAN_02", "MONTH_YOSAN_03", "MONTH_YOSAN_04", "MONTH_YOSAN_05",
            "MONTH_YOSAN_06", "MONTH_YOSAN_07", "MONTH_YOSAN_08", "MONTH_YOSAN_09", "MONTH_YOSAN_10",
            "MONTH_YOSAN_11", "MONTH_YOSAN_12", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_EIGYO_YOSAN data);

        [Sql("SELECT TOP 1 * FROM T_EIGYO_YOSAN WHERE NUMBERED_YEAR = /*data.NUMBERED_YEAR*/ AND DENPYOU_KBN_CD = /*data.DENPYOU_KBN_CD*/ AND BUSHO_CD = /*data.BUSHO_CD*/ AND SHAIN_CD = /*data.SHAIN_CD*/ AND DELETE_FLG = 0")]
        T_EIGYO_YOSAN GetDataByKey(T_EIGYO_YOSAN data);

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
