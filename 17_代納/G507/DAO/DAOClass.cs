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
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.DTO;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PayByProxy.DainoDenpyoHakkou.DAO
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
        [SqlFile("Shougun.Core.PayByProxy.DainoDenpyoHakkou.Sql.GetSeikyuZenkaiZentaka.sql")]
        DataTable GetSeikyuZenkaiZentaka(string torihikisakiCd, string startDay);
        /// <summary>
        /// 支払の前回繰越残高の取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="seikyuuDate"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoDenpyoHakkou.Sql.GetShiharaiZenkaiZentaka.sql")]
        DataTable GetShiharaiZenkaiZentaka(string torihikisakiCd, string startDay);
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
        [SqlFile("Shougun.Core.PayByProxy.DainoDenpyoHakkou.Sql.GetSeikyuIchiranData.sql")]
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
        [SqlFile("Shougun.Core.PayByProxy.DainoDenpyoHakkou.Sql.GetShiharaiIchiranData.sql")]
        DataTable GetShiharaiIchiranData(string startCD, string endCD, string startDay, string endDay, string densyuKbn, string systemID);
    }

    /// <summary>
    /// [更新用DAO]代納入力_受入
    /// </summary>
    [Bean(typeof(T_DAINOU_UKEIRE_ENTRY))]
    internal interface UpdateUkeireEntry : IS2Dao
    {
        /// <summary>
        /// 受入系Update ("URIAGE_ZEI_KEISAN_KBN_CD", "URIAGE_ZEI_KBN_CD","SHIHARAI_ZEI_KEISAN_KBN_CD", "SHIHARAI_ZEI_KBN_CD"の更新)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("DAINOU_NUMBER", "KAKUTEI_KBN", "URIAGE_DATE", "SHIHARAI_DATE", "TORIHIKISAKI_CD", "TORIHIKISAKI_NAME", "GYOUSHA_CD", "GYOUSHA_NAME", "GENBA_CD", "GENBA_NAME", "NET_TOTAL", "URIAGE_SHOUHIZEI_RATE", "URIAGE_KINGAKU_TOTAL", "URIAGE_TAX_SOTO", "URIAGE_TAX_UCHI", "URIAGE_TAX_SOTO_TOTAL", "URIAGE_TAX_UCHI_TOTAL", "HINMEI_URIAGE_KINGAKU_TOTAL", "HINMEI_URIAGE_TAX_SOTO_TOTAL", "HINMEI_URIAGE_TAX_UCHI_TOTAL", "SHIHARAI_SHOUHIZEI_RATE", "SHIHARAI_KINGAKU_TOTAL", "SHIHARAI_TAX_SOTO", "SHIHARAI_TAX_UCHI", "SHIHARAI_TAX_SOTO_TOTAL", "SHIHARAI_TAX_UCHI_TOTAL", "HINMEI_SHIHARAI_KINGAKU_TOTAL", "HINMEI_SHIHARAI_TAX_SOTO_TOTAL", "HINMEI_SHIHARAI_TAX_UCHI_TOTAL", "URIAGE_TORIHIKI_KBN_CD", "SHIHARAI_TORIHIKI_KBN_CD", "TIME_STAMP")]
        int UpdateDainou(T_DAINOU_UKEIRE_ENTRY data);

    }

    /// <summary>
    /// [更新用DAO]代納入力_出荷
    /// </summary>
    [Bean(typeof(T_DAINOU_SHUKKA_ENTRY))]
    internal interface UpdateShukkaEntry : IS2Dao
    {
        /// <summary>
        /// 売上系Update ("URIAGE_ZEI_KEISAN_KBN_CD", "URIAGE_ZEI_KBN_CD","SHIHARAI_ZEI_KEISAN_KBN_CD", "SHIHARAI_ZEI_KBN_CD"の更新)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("DAINOU_NUMBER", "KAKUTEI_KBN", "URIAGE_DATE", "SHIHARAI_DATE", "TORIHIKISAKI_CD", "TORIHIKISAKI_NAME", "GYOUSHA_CD", "GYOUSHA_NAME", "GENBA_CD", "GENBA_NAME", "NET_TOTAL", "URIAGE_SHOUHIZEI_RATE", "URIAGE_KINGAKU_TOTAL", "URIAGE_TAX_SOTO", "URIAGE_TAX_UCHI", "URIAGE_TAX_SOTO_TOTAL", "URIAGE_TAX_UCHI_TOTAL", "HINMEI_URIAGE_KINGAKU_TOTAL", "HINMEI_URIAGE_TAX_SOTO_TOTAL", "HINMEI_URIAGE_TAX_UCHI_TOTAL", "SHIHARAI_SHOUHIZEI_RATE", "SHIHARAI_KINGAKU_TOTAL", "SHIHARAI_TAX_SOTO", "SHIHARAI_TAX_UCHI", "SHIHARAI_TAX_SOTO_TOTAL", "SHIHARAI_TAX_UCHI_TOTAL", "HINMEI_SHIHARAI_KINGAKU_TOTAL", "HINMEI_SHIHARAI_TAX_SOTO_TOTAL", "HINMEI_SHIHARAI_TAX_UCHI_TOTAL", "URIAGE_TORIHIKI_KBN_CD", "SHIHARAI_TORIHIKI_KBN_CD", "TIME_STAMP")]
        int UpdateDainou(T_DAINOU_SHUKKA_ENTRY data);

    }

}
