// $Id: DAOClass.cs 11893 2013-12-19 00:55:39Z sys_dev_30 $
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

namespace Shougun.Core.Common.ItakuKeiyakuSearch.Dao
{
    [Bean(typeof(T_JUCHU_M_KENSU))]
    internal interface ItakuKeiyakuSearchDao : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで部署マスタ値(部署CD.部署略称名)を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.ItakuKeiyakuSearch.Sql.GetBushoData.sql")]
        DataTable GetBushoDataForEntity(M_BUSHO data);

        /// <summary>
        /// Entityで絞り込んで自社情報マスタ値(期首月)を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.ItakuKeiyakuSearch.Sql.GetCorpData.sql")]
        DataTable GetCorpDataForEntity(M_CORP_INFO data);

        /// <summary>
        /// Entityで絞り込んで明細部の表示項目を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.ItakuKeiyakuSearch.Sql.GetDispData.sql")]
        DataTable GetDispDataForEntity(ItakuKeiyakuSearchDto data);

        ///// <summary>
        ///// Entityで絞り込んで明細部の表示項目を取得する
        ///// </summary>
        ///// <param name="date"></param>
        ///// <returns></returns>
        //[SqlFile("Shougun.Core.Common.ItakuKeiyakuSearch.Sql.GetGenbaData.sql")]
        //DataTable GetGenbaDataForEntity(ItakuKeiyakuSearchDto data);

        /// <summary>
        /// 各区分の業者略名を取得する。
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.ItakuKeiyakuSearch.Sql.GetGyoshaData.sql")]
        DataTable GetGyoshaDataForEntity(ItakuKeiyakuSearchDto data);

        /// <summary>
        /// 各区分の現場略名を取得する。
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.ItakuKeiyakuSearch.Sql.GetGenbaData.sql")]
        DataTable GetGenbaDataForEntity(ItakuKeiyakuSearchDto data);

        /// <summary>
        /// Entityで絞り込んで明細部の表示項目を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.ItakuKeiyakuSearch.Sql.GetHeaderDispData.sql")]
        DataTable GetHeaderDispDataForEntity(ItakuKeiyakuSearchDto data);


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
            "MONTH_KENSU_11", "MONTH_KENSU_12", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
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
    #region 削除
    ///// <summary>
    ///// 業者マスタDao
    ///// </summary>
    //[Bean(typeof(M_GYOUSHA))]
    //public interface IM_GyoushaDao : IS2Dao
    //{
    //    /// <summary>
    //    /// ユーザ指定の検索条件による一覧用データ取得
    //    /// </summary>
    //    /// <param name="torihikisakiCd">取引先コード</param>
    //    /// <param name="GyoushaCd">業者コード</param>
    //    /// <returns></returns>
    //    [SqlFile("Shougun.Core.Common.ItakuKeiyakuSearch.Sql.IM_GyoushaDao_GetGyoushaInfoData.sql")]
    //    DataTable GetDataBySqlFile(string gyoushaCd,string kbn);

    //    /// <summary>
    //    /// ユーザ指定の検索条件によるデータ取得
    //    /// </summary>
    //    /// <param name="gyoushaCd">業者コード</param>
    //    /// <returns></returns>
    //    [SqlFile("Shougun.Core.Common.ItakuKeiyakuSearch.Sql.IM_GyoushaDao_GetGyoushaData.sql")]
    //    DataTable GetDataBySqlFile2(string gyoushaCd, string kbn);
    //}
    ///// <summary>
    ///// 現場マスタDao
    ///// </summary>
    //[Bean(typeof(M_GENBA))]
    //public interface IM_GenbaDao : IS2Dao
    //{
    //    /// <summary>
    //    /// ユーザ指定の検索条件によるデータ取得
    //    /// </summary>
    //    /// <param name="torihikisakiCd">取引先コード</param>
    //    /// <param name="GyoushaCd">業者コード</param>
    //    /// <param name="GenbaCd">現場コード</param>
    //    /// <returns></returns>
    //    [SqlFile("Shougun.Core.Common.ItakuKeiyakuSearch.Sql.IM_GenbaDao_GetGenbaInfoData.sql")]
    //    DataTable GetDataBySqlFile(string gyoushaCd, string genbaCd ,string haishutsuKbn);

    //    /// <summary>
    //    /// ユーザ指定の検索条件によるデータ取得
    //    /// </summary>
    //    /// <param name="data">Entity</param>
    //    /// <returns></returns>
    //    [SqlFile("Shougun.Core.Common.ItakuKeiyakuSearch.Sql.IM_GenbaDao_GetGenbaData.sql")]
    //    DataTable GetDataBySqlFile2(M_GENBA data);
    //}
    // <summary>
    // 業者情報取得用用Dao
    // </summary>
    //[Bean(typeof(M_GYOUSHA))]
    //public interface HaisyutsuJigyoushaDao : IS2Dao
    //{
    //    [SqlFile("Shougun.Core.Common.ItakuKeiyakuSearch.Sql.GetHaishutsuShaDataSql.sql")]
    //    M_GYOUSHA GetHaishutsuShaData(string HaishutsushaCd);

    //    [SqlFile("Shougun.Core.Master.KobestuHinmeiTankaIkkatsu.Sql.GetNioroshiGyoushaDataSql.sql")]
    //    M_GYOUSHA GetNioroshiGyoushaData(string nioroshiGyoushaCd);
    //}
    #endregion
}
