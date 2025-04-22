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

namespace Shougun.Core.SalesPayment.DenpyouHakou
{
    [Bean(typeof(T_SEIKYUU_DENPYOU))]
    public interface TSEIKYUUDENPYOUDao : IS2Dao
    {
        /// <summary>
        /// 請求の前回繰越残高の取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="seikyuuDate"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.DenpyouHakou.Sql.GetSeikyuZenkaiZentaka.sql")]
        DataTable GetSeikyuZenkaiZentaka(string torihikisakiCd, string startDay);
        /// <summary>
        /// 支払の前回繰越残高の取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="seikyuuDate"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.DenpyouHakou.Sql.GetShiharaiZenkaiZentaka.sql")]
        DataTable GetShiharaiZenkaiZentaka(string torihikisakiCd, string startDay);
        /// <summary>
        /// 指定された範囲の取引先Listを取得
        /// </summary>
        /// <param name="startCD">開始取引先CD</param>
        /// <param name="endCD">終了取引先CD</param>
        /// <returns name="M_TORIHIKISAKI[]">取引先CDのリスト</returns>
        [SqlFile("Shougun.Core.SalesPayment.DenpyouHakou.Sql.GetTorihikisakiList.sql")]
        M_TORIHIKISAKI[] GetTorihikisakiList(string startCD, string endCD);
        /// <summary>
        /// 明細表示用一覧データテーブルの取得
        /// </summary>
        /// <param name="startCD">開始取引先CD</param>
        /// <param name="endCD">終了取引先CD</param>
        /// <param name="startDay">開始伝票日付</param>
        /// <param name="endDay">終了伝票日付</param>
        /// <param name="densyuKbn">伝種区分</param>
        /// <param name="systemID">システムID</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.SalesPayment.DenpyouHakou.Sql.GetSeikyuIchiranData.sql")]
        DataTable GetSeikyuIchiranData(string startCD, string endCD, string startDay, string endDay, string densyuKbn, string systemID);
        /// <summary>
        /// 明細表示用一覧データテーブルの取得
        /// </summary>
        /// <param name="startCD">開始取引先CD</param>
        /// <param name="endCD">終了取引先CD</param>
        /// <param name="startDay">開始伝票日付</param>
        /// <param name="endDay">終了伝票日付</param>
        /// <param name="densyuKbn">伝種区分</param>
        /// <param name="systemID">システムID</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.SalesPayment.DenpyouHakou.Sql.GetShiharaiIchiranData.sql")]
        DataTable GetShiharaiIchiranData(string startCD, string endCD, string startDay, string endDay, string densyuKbn, string systemID);
    }
    [Bean(typeof(M_SHOUHIZEI))]
    public interface MSHOUHIZEIDao : IS2Dao
    {
        /// <summary>
        /// 消費税マスタの取得
        /// </summary>
        /// <param name="syoriDate"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.DenpyouHakou.Sql.GetSyohizei.sql")]
        string GetSyohizei(string syoriDate);
    }
    [Bean(typeof(T_UKEIRE_ENTRY))]
    public interface TUKEIREENTRYDao : IS2Dao
    {
        /// <summary>
        /// 受入金額の取得
        /// </summary>
        /// <param name="torihikiCd"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.DenpyouHakou.Sql.GetUkeireiKingaku.sql")]
        string GetUkeireiKingaku(string torihikiCd,string denpyoKbn,string densyuKbn,string zeikeisanKbn,Boolean seisanShiharaiKbn);
    }
}
