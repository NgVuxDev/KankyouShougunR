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

namespace Shougun.Core.Billing.AtenaLabel.DAO
{
    /// <summary>
    /// 宛名ラベルDao
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI))]
    public interface DaoClass : IS2Dao
    {
        /// <summary>
        /// 指定された請求送付先を取得
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.Billing.AtenaLabel.Sql.GetAtenaSeikyuuData.sql")]
        DataTable GetAtenaSeikyuuData(string printHouhou, string printKubun, string TorihikisakiCd, string GyoushaCd, string GenbaCd, string kobetsuShitei);

        /// <summary>
        /// 指定された支払送付先を取得
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.Billing.AtenaLabel.Sql.GetAtenaShiharaiData.sql")]
        DataTable GetAtenaShiharaiData(string printHouhou, string printKubun, string TorihikisakiCd, string GyoushaCd, string GenbaCd, string kobetsuShitei);

        /// <summary>
        /// 指定されたマニフェスト返送先を取得
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.Billing.AtenaLabel.Sql.GetAtenaManiData.sql")]
        DataTable GetAtenaManiData(string printHouhou, string printKubun, string TorihikisakiCd, string GyoushaCd, string GenbaCd, string kobetsuShitei);

        // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
        /// <summary>
        /// 指定されたマニフェスト返送先を取得
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.Billing.AtenaLabel.Sql.GetGenbaManiData.sql")]
        DataTable GetGenbaManiData(string printHouhou, string GyoushaCd, string GenbaCd, string kobetsuShitei, string hensouKbn);
        // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start

        /// <summary>
        /// 指定された条件で取引先CDを取得
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.Billing.AtenaLabel.Sql.GetTorihikisakiCd.sql")]
        DataTable GetTorihikisakiCd(string torihikisakiCd, string joken);

        /// <summary>
        /// 指定された条件で業者CDを取得
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.Billing.AtenaLabel.Sql.GetGyoshaCd.sql")]
        DataTable GetGyoshaCd(string gyoshaCd, string joken);

        /// <summary>
        /// 指定された条件で現場CDを取得
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.Billing.AtenaLabel.Sql.GetGenbaCd.sql")]
        DataTable GetGenbaCd(string gyoshaCd, string genbaCd, string joken);

        // No.3883-->
        /// <summary>
        /// 指定された条件で請求データを取得
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.Billing.AtenaLabel.Sql.GetSeikyuuDenpyou.sql")]
        DataTable GetSeikyuuDenpyou(string torihikisakiCd, string selectFromDate, string selectToDate);

        /// <summary>
        /// 指定された条件で精算データを取得
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.Billing.AtenaLabel.Sql.GetSeisanDenpyou.sql")]
        DataTable GetSeisanDenpyou(string torihikisakiCd, string selectFromDate, string selectToDate);
        // No.3883<--
    }
}
