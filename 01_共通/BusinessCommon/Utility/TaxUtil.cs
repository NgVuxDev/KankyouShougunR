using System;
using System.Data;
using r_framework.Entity;
using System.Text;
using r_framework.Utility;
using r_framework.Dao;
using System.Collections.Generic;
using System.Linq;
using Shougun.Function.ShougunCSCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Const;

namespace Shougun.Core.Common.BusinessCommon.Utility
{
    /// <summary>
    /// 消費税率を取得するクラス
    /// </summary>
    public class TaxUtil
    {
        /// <summary>
        /// 消費税率取得します
        /// </summary>
        /// <param name="dateTime">対象日付</param>
        /// <returns>税率</returns>
        public static Decimal GetShouhizeiRate(DateTime tekiyouDate)
        {
            M_SHOUHIZEI returnEntity = new M_SHOUHIZEI();
            IM_SHOUHIZEIDao shouhizeiDao = DaoInitUtility.GetComponent<IM_SHOUHIZEIDao>();

            //対象日付を入力されない場合、RETURN 0
            if (tekiyouDate == null)
            {
                return 0;
            }

            returnEntity = shouhizeiDao.GetDataByDate(tekiyouDate);

            //データがなければ、RETURN 0
            if (returnEntity == null)
            {
                return 0;
            }

            return Decimal.Parse(returnEntity.SHOUHIZEI_RATE.ToString());
        }

        #region 取引先の請求・精算の各消費税額を算出

        /// <summary>
        /// 請求締処理と同じように取引先の「請求毎、伝票毎、明細毎」の消費税額を算出
        /// </summary>
        /// <param name="uriageData">「受入・出荷・売上支払伝票」の売上データ (１つだけの取引先のデータ)</param>
        /// <returns>
        /// [請求書書式１]により「T_SEIKYUU_DENPYOU_KAGAMI」のリスト
        /// </returns>
        /// <remarks>    
        /// ※売上データは１つだけの取引先のデータので、複数取引先にたいして使用したい場合、ループをしてください。
        /// ※伝票毎の消費税も再算出（一部の明細を抽出の場合）  
        /// ※DataTableにある必要な各項目 (請求締処理と同じ)
        ///     伝票種類                 ：    DENPYOUSHURUI         (1.受入、2.出荷、3.売上支払)
        ///     伝票番号                 ：    DENPYONO              (受入番号、出荷番号、売上支払番号)
        ///     取引先の請求書書式１     ：    SHOSHIKI_KBN
        ///     伝票の取引先CD           ：    TORIHIKISAKI_CD
        ///     伝票の業者CD             ：    GYOUSHA_CD
        ///     伝票の現場CD             ：    GENBA_CD
        ///     伝票の売上消費税率       ：    URIAGE_SHOUHIZEI_RATE
        ///     伝票の売上税計算区分     ：    URIAGE_ZEI_KEISAN_KBN_CD
        ///     伝票の売上税区分         ：    URIAGE_ZEI_KBN_CD
        ///     明細の税区分             ：    HINMEI_ZEI_KBN_CD
        ///     明細の金額               ：    KINGAKU
        ///     明細の品名金額           ：    HINMEI_KINGAKU
        ///     明細の内税               ：    TAX_UCHI
        ///     明細の品名内税           ：    HINMEI_TAX_UCHI
        ///     明細の外税               ：    TAX_SOTO
        ///     明細の品名外税           ：    HINMEI_TAX_SOTO
        /// 
        /// ※使用の時、以下の各項目のみのデータを取得してください。以外はデータがないので、使用しないでください。
        ///     [T_SEIKYUU_DENPYOU_KAGAMI]のテーブル
        ///     取引先CD                 ：    TORIHIKISAKI_CD
        ///     業者CD                   ：    GYOUSHA_CD              ([請求書書式１]が「1.請求先別」の場合、BLANKを設定)
        ///     現場CD                   ：    GENBA_CD                ([請求書書式１]が「1.請求先別」、「2.業者別」の場合、BLANKを設定)
        ///     今回取引額(税抜)         ：    KONKAI_URIAGE_GAKU
        ///     請求毎外税合計           ：    KONKAI_SEI_UTIZEI_GAKU
        ///     請求毎内税合計           ：    KONKAI_SEI_SOTOZEI_GAKU
        ///     伝票毎外税合計           ：    KONKAI_DEN_UTIZEI_GAKU
        ///     伝票毎内税合計           ：    KONKAI_DEN_SOTOZEI_GAKU
        ///     明細毎外税合計           ：    KONKAI_MEI_UTIZEI_GAKU
        ///     明細毎内税合計           ：    KONKAI_MEI_SOTOZEI_GAKU
        /// </remarks>
        public static List<T_SEIKYUU_DENPYOU_KAGAMI> CalcSeikyuuTaxKingaku(DataTable uriageData)
        {
            if (uriageData == null || uriageData.Rows.Count == 0)
            {
                return null;
            }

            // Copy to new Datatable
            DataTable dataCheck = uriageData.Copy();

            //税区分リスト
            string[] ZEI_KBN_LIST = { CommonConst.ZEI_KBN_SOTO.ToString(), CommonConst.ZEI_KBN_UCHI.ToString(), CommonConst.ZEI_KBN_EXEMPTION.ToString() };

            List<T_SEIKYUU_DENPYOU_KAGAMI> listKagami = new List<T_SEIKYUU_DENPYOU_KAGAMI>();
            T_SEIKYUU_DENPYOU_KAGAMI kagami = null;

            //今回売上額	
            decimal konkaiUriagegaku = 0;
            //今回請内税額	
            decimal konkaiSeiUtizeigaku = 0;
            //今回請外税額	
            decimal konkaiSeisotozeigaku = 0;
            //今回伝内税額	
            decimal konkaiDenUtizeigaku = 0;
            //今回伝外税額	
            decimal konkaiDensotozeigaku = 0;
            //今回明内税額	
            decimal konkaiMeiUtizeigaku = 0;
            //今回明外税額	
            decimal konkaiMeisotozeigaku = 0;

            IM_TORIHIKISAKI_SEIKYUUDao TorihikisakiSeikyuuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
            var torihikisakiSeikyuu = TorihikisakiSeikyuuDao.GetDataByCd(dataCheck.Rows[0]["TORIHIKISAKI_CD"].ToString());

            //消費税端数
            int taxHasuu = (int)torihikisakiSeikyuu.TAX_HASUU_CD;
            //請求書書式１
            string shoshikiKbn = dataCheck.Rows[0]["SHOSHIKI_KBN"].ToString();

            // Data for 請求毎
            //税計算区分：2.請求毎
            //品名税区分: 未設定
            var seikyuuGotoRowList = dataCheck.AsEnumerable()
                                                    .Where(w => w["URIAGE_ZEI_KEISAN_KBN_CD"].ToString() == CommonConst.ZEI_KEISAN_KBN_SEIKYUU.ToString() &&
                                                                ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()) == false)
                                                    .ToList();
            switch (shoshikiKbn)
            {
                //請求先別：1
                case "1":
                    {
                        #region 伝票毎
                        //Sum 伝票毎 Kingaku
                        //税計算区分：1.伝票毎
                        //品名税区分: 未設定
                        var groupDenpyouGotoRow = dataCheck.AsEnumerable()
                                                    .Where(w => w["URIAGE_ZEI_KEISAN_KBN_CD"].ToString() == CommonConst.ZEI_KEISAN_KBN_DENPYOU.ToString() &&
                                                                ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()) == false)
                                                    .GroupBy(g => new
                                                    {
                                                        DENPYOU_SHURUI_CD = g["DENPYOUSHURUI"].ToString(),
                                                        DENPYOU_NUMBER = g["DENPYONO"].ToString(),
                                                    })
                                                    .Select(s => new
                                                    {
                                                        DENPYOU_SHURUI_CD = s.Key.DENPYOU_SHURUI_CD,
                                                        DENPYOU_NUMBER = s.Key.DENPYOU_NUMBER,
                                                        KINGAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_KINGAKU"].ToString()))
                                                    }).ToList();

                        //伝票毎計算
                        foreach (var denpyou in groupDenpyouGotoRow)
                        {
                            DataRow infoRow = dataCheck.AsEnumerable().Where(w => w["DENPYOUSHURUI"].ToString() == denpyou.DENPYOU_SHURUI_CD &&
                                                                                  w["DENPYONO"].ToString() == denpyou.DENPYOU_NUMBER).FirstOrDefault();

                            int zeikubun = Convert.ToInt32(infoRow["URIAGE_ZEI_KBN_CD"].ToString());
                            decimal shouhizeiRate = decimal.Parse(infoRow["URIAGE_SHOUHIZEI_RATE"].ToString());

                            //金額
                            konkaiUriagegaku += denpyou.KINGAKU;

                            //外税
                            if (zeikubun == 1)
                            {
                                konkaiDensotozeigaku += CalTaxSoto(denpyou.KINGAKU, shouhizeiRate, taxHasuu);
                            }

                            //内税
                            if (zeikubun == 2)
                            {
                                konkaiDenUtizeigaku += CalTaxUti(denpyou.KINGAKU, shouhizeiRate, taxHasuu);
                            }
                        }
                        #endregion

                        #region 明細毎・品名明細毎
                        //Sum 明細毎 Kingaku
                        //税計算区分：3.明細毎
                        //品名税区分: 未設定
                        var groupUriageMeisaiRow = dataCheck.AsEnumerable()
                                                    .Where(w => w["URIAGE_ZEI_KEISAN_KBN_CD"].ToString() == CommonConst.ZEI_KEISAN_KBN_MEISAI.ToString() &&
                                                                ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()) == false)
                                                    .GroupBy(g => 1)
                                                    .Select(s => new
                                                    {
                                                        KINGAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_KINGAKU"].ToString())),
                                                        UCHIZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_UCHI"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_UCHI"].ToString())),
                                                        SOTOZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_SOTO"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_SOTO"].ToString())),
                                                    }).ToList();

                        //Zei by 明細
                        if (groupUriageMeisaiRow.Count > 0)
                        {
                            konkaiUriagegaku += groupUriageMeisaiRow.FirstOrDefault().KINGAKU;
                            konkaiMeiUtizeigaku += groupUriageMeisaiRow.FirstOrDefault().UCHIZEI_GAKU;
                            konkaiMeisotozeigaku += groupUriageMeisaiRow.FirstOrDefault().SOTOZEI_GAKU;
                        }

                        //Sum 品名明細毎
                        //品名税区分: 設定済（外税・内税・非課税）
                        var groupHinmeiMeisaiRow = dataCheck.AsEnumerable()
                                                    .Where(w => ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()))
                                                    .GroupBy(g => 1)
                                                    .Select(s => new
                                                    {
                                                        KINGAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_KINGAKU"].ToString())),
                                                        UCHIZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_UCHI"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_UCHI"].ToString())),
                                                        SOTOZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_SOTO"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_SOTO"].ToString())),
                                                    }).ToList();

                        //Zei by 明細
                        if (groupHinmeiMeisaiRow.Count > 0)
                        {
                            konkaiUriagegaku += groupHinmeiMeisaiRow.FirstOrDefault().KINGAKU;
                            konkaiMeiUtizeigaku += groupHinmeiMeisaiRow.FirstOrDefault().UCHIZEI_GAKU;
                            konkaiMeisotozeigaku += groupHinmeiMeisaiRow.FirstOrDefault().SOTOZEI_GAKU;
                        }
                        #endregion

                        #region 請求毎
                        // Data for 請求毎
                        //税計算区分：2.請求毎
                        //品名税区分: 未設定
                        var groupKeySeikyuuZei = seikyuuGotoRowList.GroupBy(g => new
                        {
                            URIAGE_ZEI_KBN_CD = g["URIAGE_ZEI_KBN_CD"].ToString(),
                            URIAGE_SHOUHIZEI_RATE = g["URIAGE_SHOUHIZEI_RATE"].ToString(),
                        })
                        .Select(s => s.First()).ToList();

                        foreach (DataRow key in groupKeySeikyuuZei)
                        {
                            var listDataByKey = seikyuuGotoRowList.AsEnumerable().Where(w => w["URIAGE_ZEI_KBN_CD"].ToString() == key["URIAGE_ZEI_KBN_CD"].ToString() &&
                                                                                             w["URIAGE_SHOUHIZEI_RATE"].ToString() == key["URIAGE_SHOUHIZEI_RATE"].ToString()
                                                                                        ).ToList();

                            //今回売上額
                            konkaiUriagegaku += listDataByKey.Sum(s => ConvertNullOrEmptyToZero(s["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(s["HINMEI_KINGAKU"].ToString()));

                            //税計算区分別消費税率別の消費税
                            decimal tmpSeisotozei = 0;
                            decimal tmpSeiuchizei = 0;

                            CalcSeikyuuTax(listDataByKey, taxHasuu, ref tmpSeisotozei, ref tmpSeiuchizei);

                            konkaiSeisotozeigaku += tmpSeisotozei;
                            konkaiSeiUtizeigaku += tmpSeiuchizei;
                        }
                        #endregion

                        //今回売上額(税抜)
                        konkaiUriagegaku = konkaiUriagegaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);

                        kagami = new T_SEIKYUU_DENPYOU_KAGAMI();
                        kagami.TORIHIKISAKI_CD = dataCheck.Rows[0]["TORIHIKISAKI_CD"].ToString();
                        kagami.GYOUSHA_CD = string.Empty;
                        kagami.GENBA_CD = string.Empty;
                        kagami.KONKAI_URIAGE_GAKU = konkaiUriagegaku;
                        kagami.KONKAI_SEI_SOTOZEI_GAKU = konkaiSeisotozeigaku;
                        kagami.KONKAI_SEI_UTIZEI_GAKU = konkaiSeiUtizeigaku;
                        kagami.KONKAI_DEN_SOTOZEI_GAKU = konkaiDensotozeigaku;
                        kagami.KONKAI_DEN_UTIZEI_GAKU = konkaiDenUtizeigaku;
                        kagami.KONKAI_MEI_SOTOZEI_GAKU = konkaiMeisotozeigaku;
                        kagami.KONKAI_MEI_UTIZEI_GAKU = konkaiMeiUtizeigaku;

                        listKagami.Add(kagami);

                        //クリア
                        konkaiUriagegaku = 0;
                        konkaiSeiUtizeigaku = 0;
                        konkaiSeisotozeigaku = 0;
                        konkaiDenUtizeigaku = 0;
                        konkaiDensotozeigaku = 0;
                        konkaiMeiUtizeigaku = 0;
                        konkaiMeisotozeigaku = 0;
                    }
                    break;
                //業者別：2
                case "2":
                    {
                        //業者別の鏡データ
                        var gyoushaList = dataCheck.AsEnumerable().GroupBy(g => new
                        {
                            GYOUSHA_CD = g["GYOUSHA_CD"].ToString(),
                        }).Select(s => new
                        {
                            GYOUSHA_CD = s.Key.GYOUSHA_CD,
                        }).ToList();

                        foreach (var item in gyoushaList)
                        {
                            #region 伝票毎
                            //Sum 伝票毎 Kingaku
                            //税計算区分：1.伝票毎
                            //品名税区分: 未設定
                            var groupDenpyouGotoRow = dataCheck.AsEnumerable()
                                                        .Where(w => w["GYOUSHA_CD"].ToString() == item.GYOUSHA_CD &&
                                                                    w["URIAGE_ZEI_KEISAN_KBN_CD"].ToString() == CommonConst.ZEI_KEISAN_KBN_DENPYOU.ToString() &&
                                                                    ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()) == false)
                                                        .GroupBy(g => new
                                                        {
                                                            DENPYOU_SHURUI_CD = g["DENPYOUSHURUI"].ToString(),
                                                            DENPYOU_NUMBER = g["DENPYONO"].ToString(),
                                                        })
                                                        .Select(s => new
                                                        {
                                                            DENPYOU_SHURUI_CD = s.Key.DENPYOU_SHURUI_CD,
                                                            DENPYOU_NUMBER = s.Key.DENPYOU_NUMBER,
                                                            KINGAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_KINGAKU"].ToString()))
                                                        }).ToList();

                            //伝票毎計算
                            foreach (var denpyou in groupDenpyouGotoRow)
                            {
                                DataRow infoRow = dataCheck.AsEnumerable().Where(w => w["DENPYOUSHURUI"].ToString() == denpyou.DENPYOU_SHURUI_CD &&
                                                                                      w["DENPYONO"].ToString() == denpyou.DENPYOU_NUMBER).FirstOrDefault();

                                int zeikubun = Convert.ToInt32(infoRow["URIAGE_ZEI_KBN_CD"].ToString());
                                decimal shouhizeiRate = decimal.Parse(infoRow["URIAGE_SHOUHIZEI_RATE"].ToString());

                                //金額
                                konkaiUriagegaku += denpyou.KINGAKU;

                                //外税
                                if (zeikubun == 1)
                                {
                                    konkaiDensotozeigaku += CalTaxSoto(denpyou.KINGAKU, shouhizeiRate, taxHasuu);
                                }

                                //内税
                                if (zeikubun == 2)
                                {
                                    konkaiDenUtizeigaku += CalTaxUti(denpyou.KINGAKU, shouhizeiRate, taxHasuu);
                                }
                            }
                            #endregion

                            #region 明細毎・品名明細毎
                            //Sum 明細毎 Kingaku
                            //税計算区分：3.明細毎
                            //品名税区分: 未設定
                            var groupUriageMeisaiRow = dataCheck.AsEnumerable()
                                                        .Where(w =>
                                                            w["GYOUSHA_CD"].ToString() == item.GYOUSHA_CD.ToString() &&
                                                            w["URIAGE_ZEI_KEISAN_KBN_CD"].ToString() == CommonConst.ZEI_KEISAN_KBN_MEISAI.ToString() &&
                                                            ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()) == false)
                                                        .GroupBy(g => 1)
                                                        .Select(s => new
                                                        {
                                                            KINGAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_KINGAKU"].ToString())),
                                                            UCHIZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_UCHI"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_UCHI"].ToString())),
                                                            SOTOZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_SOTO"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_SOTO"].ToString())),
                                                        }).ToList();

                            //Zei by 明細
                            if (groupUriageMeisaiRow.Count > 0)
                            {
                                konkaiUriagegaku += groupUriageMeisaiRow.FirstOrDefault().KINGAKU;
                                konkaiMeiUtizeigaku += groupUriageMeisaiRow.FirstOrDefault().UCHIZEI_GAKU;
                                konkaiMeisotozeigaku += groupUriageMeisaiRow.FirstOrDefault().SOTOZEI_GAKU;
                            }

                            //Sum 品名明細毎
                            //品名税区分: 設定済（外税・内税・非課税）
                            var groupHinmeiMeisaiRow = dataCheck.AsEnumerable()
                                                        .Where(w => w["GYOUSHA_CD"].ToString() == item.GYOUSHA_CD.ToString() &&
                                                                    ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()))
                                                        .GroupBy(g => 1)
                                                        .Select(s => new
                                                        {
                                                            KINGAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_KINGAKU"].ToString())),
                                                            UCHIZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_UCHI"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_UCHI"].ToString())),
                                                            SOTOZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_SOTO"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_SOTO"].ToString())),
                                                        }).ToList();

                            //Zei by 明細
                            if (groupHinmeiMeisaiRow.Count > 0)
                            {
                                konkaiUriagegaku += groupHinmeiMeisaiRow.FirstOrDefault().KINGAKU;
                                konkaiMeiUtizeigaku += groupHinmeiMeisaiRow.FirstOrDefault().UCHIZEI_GAKU;
                                konkaiMeisotozeigaku += groupHinmeiMeisaiRow.FirstOrDefault().SOTOZEI_GAKU;
                            }
                            #endregion

                            #region 請求毎
                            // Data for 請求毎
                            //税計算区分：2.請求毎
                            //品名税区分: 未設定
                            var groupKeySeikyuuZei = seikyuuGotoRowList.Where(w => w["GYOUSHA_CD"].ToString() == item.GYOUSHA_CD.ToString())
                            .GroupBy(g => new
                            {
                                GYOUSHA_CD = g["GYOUSHA_CD"].ToString(),
                                URIAGE_ZEI_KBN_CD = g["URIAGE_ZEI_KBN_CD"].ToString(),
                                URIAGE_SHOUHIZEI_RATE = g["URIAGE_SHOUHIZEI_RATE"].ToString(),
                            }).Select(s => s.First()).ToList();

                            foreach (DataRow key in groupKeySeikyuuZei)
                            {
                                var listDataByKey = seikyuuGotoRowList.Where(w => w["GYOUSHA_CD"].ToString() == key["GYOUSHA_CD"].ToString() && 
                                                                                w["URIAGE_ZEI_KBN_CD"].ToString() == key["URIAGE_ZEI_KBN_CD"].ToString() &&
                                                                                w["URIAGE_SHOUHIZEI_RATE"].ToString() == key["URIAGE_SHOUHIZEI_RATE"].ToString()
                                                                            ).ToList();

                                decimal tmpKingaku = 0;
                                //今回売上額
                                tmpKingaku = listDataByKey.Sum(s => ConvertNullOrEmptyToZero(s["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(s["HINMEI_KINGAKU"].ToString()));

                                //業者別税計算区分別消費税率別の消費税
                                decimal tmpSeisotozei = 0;
                                decimal tmpSeiuchizei = 0;

                                CalcSeikyuuTax(listDataByKey, taxHasuu, ref tmpSeisotozei, ref tmpSeiuchizei);

                                //今回売上額
                                konkaiUriagegaku += tmpKingaku;

                                konkaiSeisotozeigaku += tmpSeisotozei;
                                konkaiSeiUtizeigaku += tmpSeiuchizei;
                            }
                            #endregion

                            //今回売上額(税抜)
                            konkaiUriagegaku = konkaiUriagegaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);

                            kagami = new T_SEIKYUU_DENPYOU_KAGAMI();
                            kagami.TORIHIKISAKI_CD = dataCheck.Rows[0]["TORIHIKISAKI_CD"].ToString();
                            kagami.GYOUSHA_CD = item.GYOUSHA_CD;
                            kagami.GENBA_CD = string.Empty;
                            kagami.KONKAI_URIAGE_GAKU = konkaiUriagegaku;
                            kagami.KONKAI_SEI_SOTOZEI_GAKU = konkaiSeisotozeigaku;
                            kagami.KONKAI_SEI_UTIZEI_GAKU = konkaiSeiUtizeigaku;
                            kagami.KONKAI_DEN_SOTOZEI_GAKU = konkaiDensotozeigaku;
                            kagami.KONKAI_DEN_UTIZEI_GAKU = konkaiDenUtizeigaku;
                            kagami.KONKAI_MEI_SOTOZEI_GAKU = konkaiMeisotozeigaku;
                            kagami.KONKAI_MEI_UTIZEI_GAKU = konkaiMeiUtizeigaku;

                            listKagami.Add(kagami);

                            //クリア
                            konkaiUriagegaku = 0;
                            konkaiSeiUtizeigaku = 0;
                            konkaiSeisotozeigaku = 0;
                            konkaiDenUtizeigaku = 0;
                            konkaiDensotozeigaku = 0;
                            konkaiMeiUtizeigaku = 0;
                            konkaiMeisotozeigaku = 0;
                        }

                    }
                    break;
                //現場別：3
                case "3":
                    {
                        //現場別の鏡データ
                        var genbaList = dataCheck.AsEnumerable().GroupBy(g => new
                        {
                            GYOUSHA_CD = g["GYOUSHA_CD"].ToString(),
                            GENBA_CD = g["GENBA_CD"].ToString(),
                        }).Select(s => new
                        {
                            GYOUSHA_CD = s.Key.GYOUSHA_CD,
                            GENBA_CD = s.Key.GENBA_CD,
                        }).ToList();

                        foreach (var item in genbaList)
                        {
                            #region 伝票毎
                            //Data for 伝票毎
                            //税計算区分：1.伝票毎
                            //品名税区分: 未設定
                            var groupDenpyouGotoRow = dataCheck.AsEnumerable()
                                                        .Where(w => w["GYOUSHA_CD"].ToString() == item.GYOUSHA_CD &&
                                                                    w["GENBA_CD"].ToString() == item.GENBA_CD &&
                                                                    w["URIAGE_ZEI_KEISAN_KBN_CD"].ToString() == CommonConst.ZEI_KEISAN_KBN_DENPYOU.ToString() &&
                                                                    ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()) == false)
                                                        .GroupBy(g => new
                                                        {
                                                            DENPYOU_SHURUI_CD = g["DENPYOUSHURUI"].ToString(),
                                                            DENPYOU_NUMBER = g["DENPYONO"].ToString(),
                                                        })
                                                        .Select(s => new
                                                        {
                                                            DENPYOU_SHURUI_CD = s.Key.DENPYOU_SHURUI_CD,
                                                            DENPYOU_NUMBER = s.Key.DENPYOU_NUMBER,
                                                            KINGAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_KINGAKU"].ToString()))
                                                        }).ToList();

                            //伝票毎計算
                            foreach (var denpyou in groupDenpyouGotoRow)
                            {
                                DataRow infoRow = dataCheck.AsEnumerable().Where(w => w["DENPYOUSHURUI"].ToString() == denpyou.DENPYOU_SHURUI_CD &&
                                                                                      w["DENPYONO"].ToString() == denpyou.DENPYOU_NUMBER).FirstOrDefault();

                                int zeikubun = Convert.ToInt32(infoRow["URIAGE_ZEI_KBN_CD"].ToString());
                                decimal shouhizeiRate = decimal.Parse(infoRow["URIAGE_SHOUHIZEI_RATE"].ToString());

                                //金額
                                konkaiUriagegaku += denpyou.KINGAKU;

                                //外税
                                if (zeikubun == 1)
                                {
                                    konkaiDensotozeigaku += CalTaxSoto(denpyou.KINGAKU, shouhizeiRate, taxHasuu);
                                }

                                //内税
                                if (zeikubun == 2)
                                {
                                    konkaiDenUtizeigaku += CalTaxUti(denpyou.KINGAKU, shouhizeiRate, taxHasuu);
                                }
                            }
                            #endregion

                            #region 明細毎・品名明細毎
                            //Data for 明細毎
                            //税計算区分：3.明細毎
                            //品名税区分: 未設定
                            var groupUriageMeisaiRow = dataCheck.AsEnumerable()
                                                        .Where(w =>
                                                            w["GYOUSHA_CD"].ToString() == item.GYOUSHA_CD.ToString() &&
                                                            w["GENBA_CD"].ToString() == item.GENBA_CD.ToString() &&
                                                            w["URIAGE_ZEI_KEISAN_KBN_CD"].ToString() == CommonConst.ZEI_KEISAN_KBN_MEISAI.ToString() &&
                                                            ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()) == false)
                                                        .GroupBy(g => 1)
                                                        .Select(s => new
                                                        {
                                                            KINGAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_KINGAKU"].ToString())),
                                                            UCHIZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_UCHI"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_UCHI"].ToString())),
                                                            SOTOZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_SOTO"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_SOTO"].ToString())),
                                                        }).ToList();

                            //Zei by 明細
                            if (groupUriageMeisaiRow.Count > 0)
                            {
                                konkaiUriagegaku += groupUriageMeisaiRow.FirstOrDefault().KINGAKU;
                                konkaiMeiUtizeigaku += groupUriageMeisaiRow.FirstOrDefault().UCHIZEI_GAKU;
                                konkaiMeisotozeigaku += groupUriageMeisaiRow.FirstOrDefault().SOTOZEI_GAKU;
                            }

                            //Data for 品名明細毎
                            //品名税区分: 設定済（外税・内税・非課税）
                            var groupHinmeiMeisaiRow = dataCheck.AsEnumerable()
                                                        .Where(w =>
                                                            w["GYOUSHA_CD"].ToString() == item.GYOUSHA_CD.ToString() &&
                                                            w["GENBA_CD"].ToString() == item.GENBA_CD.ToString() &&
                                                            ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()))
                                                        .GroupBy(g => 1)
                                                        .Select(s => new
                                                        {
                                                            KINGAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_KINGAKU"].ToString())),
                                                            UCHIZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_UCHI"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_UCHI"].ToString())),
                                                            SOTOZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_SOTO"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_SOTO"].ToString())),
                                                        }).ToList();

                            //Zei by 明細
                            if (groupHinmeiMeisaiRow.Count > 0)
                            {
                                konkaiUriagegaku += groupHinmeiMeisaiRow.FirstOrDefault().KINGAKU;
                                konkaiMeiUtizeigaku += groupHinmeiMeisaiRow.FirstOrDefault().UCHIZEI_GAKU;
                                konkaiMeisotozeigaku += groupHinmeiMeisaiRow.FirstOrDefault().SOTOZEI_GAKU;
                            }
                            #endregion

                            #region 請求毎
                            // Data for 請求毎
                            //税計算区分：2.請求毎
                            //品名税区分: 未設定
                            var groupKeySeikyuuZei = seikyuuGotoRowList.Where(w => w["GYOUSHA_CD"].ToString() == item.GYOUSHA_CD &&
                                                                                   w["GENBA_CD"].ToString() == item.GENBA_CD)
                            .GroupBy(g => new
                            {
                                GYOUSHA_CD = g["GYOUSHA_CD"].ToString(),
                                GENBA_CD = g["GENBA_CD"].ToString(),
                                URIAGE_ZEI_KBN_CD = g["URIAGE_ZEI_KBN_CD"].ToString(),
                                URIAGE_SHOUHIZEI_RATE = g["URIAGE_SHOUHIZEI_RATE"].ToString(),
                            }).Select(s => s.First()).ToList();

                            foreach (DataRow key in groupKeySeikyuuZei)
                            {
                                var listDataByKey = seikyuuGotoRowList.Where(w => w["GYOUSHA_CD"].ToString() == key["GYOUSHA_CD"].ToString() &&
                                                                                  w["GENBA_CD"].ToString() == key["GENBA_CD"].ToString() &&
                                                                                  w["URIAGE_ZEI_KBN_CD"].ToString() == key["URIAGE_ZEI_KBN_CD"].ToString() &&
                                                                                  w["URIAGE_SHOUHIZEI_RATE"].ToString() == key["URIAGE_SHOUHIZEI_RATE"].ToString()
                                                                            ).ToList();

                                decimal tmpKingaku = 0;
                                //今回売上額
                                tmpKingaku = listDataByKey.Sum(s => ConvertNullOrEmptyToZero(s["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(s["HINMEI_KINGAKU"].ToString()));

                                //現場別税計算区分別消費税率別の消費税
                                decimal tmpSeisotozei = 0;
                                decimal tmpSeiuchizei = 0;

                                CalcSeikyuuTax(listDataByKey, taxHasuu, ref tmpSeisotozei, ref tmpSeiuchizei);

                                //今回売上額
                                konkaiUriagegaku += tmpKingaku;
                                
                                konkaiSeisotozeigaku += tmpSeisotozei;
                                konkaiSeiUtizeigaku += tmpSeiuchizei;
                            }
                            #endregion

                            //今回売上額(税抜)
                            konkaiUriagegaku = konkaiUriagegaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);

                            kagami = new T_SEIKYUU_DENPYOU_KAGAMI();
                            kagami.TORIHIKISAKI_CD = dataCheck.Rows[0]["TORIHIKISAKI_CD"].ToString();
                            kagami.GYOUSHA_CD = item.GYOUSHA_CD;
                            kagami.GENBA_CD = item.GENBA_CD;
                            kagami.KONKAI_URIAGE_GAKU = konkaiUriagegaku;
                            kagami.KONKAI_SEI_SOTOZEI_GAKU = konkaiSeisotozeigaku;
                            kagami.KONKAI_SEI_UTIZEI_GAKU = konkaiSeiUtizeigaku;
                            kagami.KONKAI_DEN_SOTOZEI_GAKU = konkaiDensotozeigaku;
                            kagami.KONKAI_DEN_UTIZEI_GAKU = konkaiDenUtizeigaku;
                            kagami.KONKAI_MEI_SOTOZEI_GAKU = konkaiMeisotozeigaku;
                            kagami.KONKAI_MEI_UTIZEI_GAKU = konkaiMeiUtizeigaku;

                            listKagami.Add(kagami);

                            //クリア
                            konkaiUriagegaku = 0;
                            konkaiSeiUtizeigaku = 0;
                            konkaiSeisotozeigaku = 0;
                            konkaiDenUtizeigaku = 0;
                            konkaiDensotozeigaku = 0;
                            konkaiMeiUtizeigaku = 0;
                            konkaiMeisotozeigaku = 0;
                        }
                    }
                    break;
            }

            return listKagami;
        }

        /// <summary>
        /// 支払締処理と同じように取引先の「精算毎、伝票毎、明細毎」の消費税額を算出
        /// </summary>
        /// <param name="shiharaiData">「受入・出荷・売上支払伝票」の支払データ (１つだけの取引先のデータ)</param>
        /// <returns>
        /// [支払明細書書式１]により「T_SEISAN_DENPYOU_KAGAMI」のリスト
        /// </returns>
        /// <remarks>
        /// ※支払データは１つだけの取引先のデータので、複数取引先にたいして使用したい場合、ループをしてください。
        /// ※伝票毎の消費税も再算出（一部の明細を抽出の場合）
        /// ※DataTableにある必要な各項目 (支払締処理と同じ)
        ///     伝票種類                 ：    DENPYOUSHURUI         (1.受入、2.出荷、3.売上支払)
        ///     伝票番号                 ：    DENPYONO              (受入番号、出荷番号、売上支払番号)
        ///     取引先の支払明細書書式１ ：    SHOSHIKI_KBN
        ///     伝票の取引先CD           ：    TORIHIKISAKI_CD
        ///     伝票の業者CD             ：    GYOUSHA_CD
        ///     伝票の現場CD             ：    GENBA_CD
        ///     伝票の支払消費税率       ：    SHIHARAI_SHOUHIZEI_RATE
        ///     伝票の支払税計算区分     ：    SHIHARAI_ZEI_KEISAN_KBN_CD
        ///     伝票の支払税区分         ：    SHIHARAI_ZEI_KBN_CD
        ///     明細の税区分             ：    HINMEI_ZEI_KBN_CD
        ///     明細の金額               ：    KINGAKU
        ///     明細の品名金額           ：    HINMEI_KINGAKU
        ///     明細の内税               ：    TAX_UCHI
        ///     明細の品名内税           ：    HINMEI_TAX_UCHI
        ///     明細の外税               ：    TAX_SOTO
        ///     明細の品名外税           ：    HINMEI_TAX_SOTO
        ///     
        /// ※使用の時、以下の各項目のみのデータを取得してください。以外はデータがないので、使用しないでください。
        ///     [T_SEISAN_DENPYOU_KAGAMI]のテーブル
        ///     取引先CD                 ：    TORIHIKISAKI_CD
        ///     業者CD                   ：    GYOUSHA_CD             ([支払明細書書式１]が「1.支払先別」の場合、BLANKを設定)
        ///     現場CD                   ：    GENBA_CD               ([支払明細書書式１]が「1.支払先別」、「2.業者別」の場合、BLANKを設定)
        ///     今回取引額(税抜)         ：    KONKAI_SHIHARAI_GAKU
        ///     精算毎外税合計           ：    KONKAI_SEI_UTIZEI_GAKU
        ///     精算毎内税合計           ：    KONKAI_SEI_SOTOZEI_GAKU
        ///     伝票毎外税合計           ：    KONKAI_DEN_UTIZEI_GAKU
        ///     伝票毎内税合計           ：    KONKAI_DEN_SOTOZEI_GAKU
        ///     明細毎外税合計           ：    KONKAI_MEI_UTIZEI_GAKU
        ///     明細毎内税合計           ：    KONKAI_MEI_SOTOZEI_GAKU
        /// </remarks>
        public static List<T_SEISAN_DENPYOU_KAGAMI> CalcSeisanTaxKingaku(DataTable shiharaiData)
        {
            if (shiharaiData == null || shiharaiData.Rows.Count == 0)
            {
                return null;
            }

            // Copy to new Datatable
            DataTable dataCheck = shiharaiData.Copy();

            //税区分リスト
            string[] ZEI_KBN_LIST = { CommonConst.ZEI_KBN_SOTO.ToString(), CommonConst.ZEI_KBN_UCHI.ToString(), CommonConst.ZEI_KBN_EXEMPTION.ToString() };

            List<T_SEISAN_DENPYOU_KAGAMI> listKagami = new List<T_SEISAN_DENPYOU_KAGAMI>();
            T_SEISAN_DENPYOU_KAGAMI kagami = null;

            //今回支払額	
            decimal konkaiShiharaigaku = 0;
            //今回請内税額	
            decimal konkaiSeiUtizeigaku = 0;
            //今回請外税額	
            decimal konkaiSeisotozeigaku = 0;
            //今回伝内税額	
            decimal konkaiDenUtizeigaku = 0;
            //今回伝外税額	
            decimal konkaiDensotozeigaku = 0;
            //今回明内税額	
            decimal konkaiMeiUtizeigaku = 0;
            //今回明外税額	
            decimal konkaiMeisotozeigaku = 0;

            IM_TORIHIKISAKI_SHIHARAIDao TorihikisakiSeisanDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
            var torihikisakiSeisan = TorihikisakiSeisanDao.GetDataByCd(dataCheck.Rows[0]["TORIHIKISAKI_CD"].ToString());

            //消費税端数
            int taxHasuu = (int)torihikisakiSeisan.TAX_HASUU_CD;
            //支払明細書書式１
            string shoshikiKbn = dataCheck.Rows[0]["SHOSHIKI_KBN"].ToString();

            // Data for 精算毎
            //税計算区分：2.精算毎
            //品名税区分: 未設定
            var seikyuuGotoRowList = dataCheck.AsEnumerable()
                                                    .Where(w => w["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString() == CommonConst.ZEI_KEISAN_KBN_SEIKYUU.ToString() &&
                                                                ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()) == false)
                                                    .ToList();
            switch (shoshikiKbn)
            {
                //精算先別：1
                case "1":
                    {
                        #region 伝票毎
                        //Sum 伝票毎 Kingaku
                        //税計算区分：1.伝票毎
                        //品名税区分: 未設定
                        var groupDenpyouGotoRow = dataCheck.AsEnumerable()
                                                    .Where(w => w["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString() == CommonConst.ZEI_KEISAN_KBN_DENPYOU.ToString() &&
                                                                ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()) == false)
                                                    .GroupBy(g => new
                                                    {
                                                        DENPYOU_SHURUI_CD = g["DENPYOUSHURUI"].ToString(),
                                                        DENPYOU_NUMBER = g["DENPYONO"].ToString(),
                                                    })
                                                    .Select(s => new
                                                    {
                                                        DENPYOU_SHURUI_CD = s.Key.DENPYOU_SHURUI_CD,
                                                        DENPYOU_NUMBER = s.Key.DENPYOU_NUMBER,
                                                        KINGAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_KINGAKU"].ToString()))
                                                    }).ToList();

                        //伝票毎計算
                        foreach (var denpyou in groupDenpyouGotoRow)
                        {
                            DataRow infoRow = dataCheck.AsEnumerable().Where(w => w["DENPYOUSHURUI"].ToString() == denpyou.DENPYOU_SHURUI_CD &&
                                                                                  w["DENPYONO"].ToString() == denpyou.DENPYOU_NUMBER).FirstOrDefault();

                            int zeikubun = Convert.ToInt32(infoRow["SHIHARAI_ZEI_KBN_CD"].ToString());
                            decimal shouhizeiRate = decimal.Parse(infoRow["SHIHARAI_SHOUHIZEI_RATE"].ToString());

                            //金額
                            konkaiShiharaigaku += denpyou.KINGAKU;

                            //外税
                            if (zeikubun == 1)
                            {
                                konkaiDensotozeigaku += CalTaxSoto(denpyou.KINGAKU, shouhizeiRate, taxHasuu);
                            }

                            //内税
                            if (zeikubun == 2)
                            {
                                konkaiDenUtizeigaku += CalTaxUti(denpyou.KINGAKU, shouhizeiRate, taxHasuu);
                            }
                        }
                        #endregion

                        #region 明細毎・品名明細毎
                        //Sum 明細毎 Kingaku
                        //税計算区分：3.明細毎
                        //品名税区分: 未設定
                        var groupShiharaiMeisaiRow = dataCheck.AsEnumerable()
                                                    .Where(w => w["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString() == CommonConst.ZEI_KEISAN_KBN_MEISAI.ToString() &&
                                                                ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()) == false)
                                                    .GroupBy(g => 1)
                                                    .Select(s => new
                                                    {
                                                        KINGAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_KINGAKU"].ToString())),
                                                        UCHIZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_UCHI"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_UCHI"].ToString())),
                                                        SOTOZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_SOTO"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_SOTO"].ToString())),
                                                    }).ToList();

                        //Zei by 明細
                        if (groupShiharaiMeisaiRow.Count > 0)
                        {
                            konkaiShiharaigaku += groupShiharaiMeisaiRow.FirstOrDefault().KINGAKU;
                            konkaiMeiUtizeigaku += groupShiharaiMeisaiRow.FirstOrDefault().UCHIZEI_GAKU;
                            konkaiMeisotozeigaku += groupShiharaiMeisaiRow.FirstOrDefault().SOTOZEI_GAKU;
                        }

                        //Sum 品名明細毎
                        //品名税区分: 設定済（外税・内税・非課税）
                        var groupHinmeiMeisaiRow = dataCheck.AsEnumerable()
                                                    .Where(w => ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()))
                                                    .GroupBy(g => 1)
                                                    .Select(s => new
                                                    {
                                                        KINGAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_KINGAKU"].ToString())),
                                                        UCHIZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_UCHI"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_UCHI"].ToString())),
                                                        SOTOZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_SOTO"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_SOTO"].ToString())),
                                                    }).ToList();

                        //Zei by 明細
                        if (groupHinmeiMeisaiRow.Count > 0)
                        {
                            konkaiShiharaigaku += groupHinmeiMeisaiRow.FirstOrDefault().KINGAKU;
                            konkaiMeiUtizeigaku += groupHinmeiMeisaiRow.FirstOrDefault().UCHIZEI_GAKU;
                            konkaiMeisotozeigaku += groupHinmeiMeisaiRow.FirstOrDefault().SOTOZEI_GAKU;
                        }
                        #endregion

                        #region 精算毎
                        // Data for 精算毎
                        //税計算区分：2.精算毎
                        //品名税区分: 未設定
                        var groupKeySeikyuuZei = seikyuuGotoRowList.GroupBy(g => new
                        {
                            SHIHARAI_ZEI_KBN_CD = g["SHIHARAI_ZEI_KBN_CD"].ToString(),
                            SHIHARAI_SHOUHIZEI_RATE = g["SHIHARAI_SHOUHIZEI_RATE"].ToString(),
                        })
                        .Select(s => s.First()).ToList();

                        foreach (DataRow key in groupKeySeikyuuZei)
                        {
                            var listDataByKey = seikyuuGotoRowList.AsEnumerable().Where(w => w["SHIHARAI_ZEI_KBN_CD"].ToString() == key["SHIHARAI_ZEI_KBN_CD"].ToString() &&
                                                                                             w["SHIHARAI_SHOUHIZEI_RATE"].ToString() == key["SHIHARAI_SHOUHIZEI_RATE"].ToString()
                                                                                        ).ToList();

                            //今回支払額
                            konkaiShiharaigaku += listDataByKey.Sum(s => ConvertNullOrEmptyToZero(s["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(s["HINMEI_KINGAKU"].ToString()));

                            //税計算区分別消費税率別の消費税
                            decimal tmpSeisotozei = 0;
                            decimal tmpSeiuchizei = 0;

                            CalcSeisanTax(listDataByKey, taxHasuu, ref tmpSeisotozei, ref tmpSeiuchizei);

                            konkaiSeisotozeigaku += tmpSeisotozei;
                            konkaiSeiUtizeigaku += tmpSeiuchizei;
                        }
                        #endregion

                        //今回支払額(税抜)
                        konkaiShiharaigaku = konkaiShiharaigaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);

                        kagami = new T_SEISAN_DENPYOU_KAGAMI();
                        kagami.TORIHIKISAKI_CD = dataCheck.Rows[0]["TORIHIKISAKI_CD"].ToString();
                        kagami.GYOUSHA_CD = string.Empty;
                        kagami.GENBA_CD = string.Empty;
                        kagami.KONKAI_SHIHARAI_GAKU = konkaiShiharaigaku;
                        kagami.KONKAI_SEI_SOTOZEI_GAKU = konkaiSeisotozeigaku;
                        kagami.KONKAI_SEI_UTIZEI_GAKU = konkaiSeiUtizeigaku;
                        kagami.KONKAI_DEN_SOTOZEI_GAKU = konkaiDensotozeigaku;
                        kagami.KONKAI_DEN_UTIZEI_GAKU = konkaiDenUtizeigaku;
                        kagami.KONKAI_MEI_SOTOZEI_GAKU = konkaiMeisotozeigaku;
                        kagami.KONKAI_MEI_UTIZEI_GAKU = konkaiMeiUtizeigaku;

                        listKagami.Add(kagami);

                        //クリア
                        konkaiShiharaigaku = 0;
                        konkaiSeiUtizeigaku = 0;
                        konkaiSeisotozeigaku = 0;
                        konkaiDenUtizeigaku = 0;
                        konkaiDensotozeigaku = 0;
                        konkaiMeiUtizeigaku = 0;
                        konkaiMeisotozeigaku = 0;
                    }
                    break;
                //業者別：2
                case "2":
                    {
                        //業者別の鏡データ
                        var gyoushaList = dataCheck.AsEnumerable().GroupBy(g => new
                        {
                            GYOUSHA_CD = g["GYOUSHA_CD"].ToString(),
                        }).Select(s => new
                        {
                            GYOUSHA_CD = s.Key.GYOUSHA_CD,
                        }).ToList();

                        foreach (var item in gyoushaList)
                        {
                            #region 伝票毎
                            //Sum 伝票毎 Kingaku
                            //税計算区分：1.伝票毎
                            //品名税区分: 未設定
                            var groupDenpyouGotoRow = dataCheck.AsEnumerable()
                                                        .Where(w => w["GYOUSHA_CD"].ToString() == item.GYOUSHA_CD &&
                                                                    w["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString() == CommonConst.ZEI_KEISAN_KBN_DENPYOU.ToString() &&
                                                                    ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()) == false)
                                                        .GroupBy(g => new
                                                        {
                                                            DENPYOU_SHURUI_CD = g["DENPYOUSHURUI"].ToString(),
                                                            DENPYOU_NUMBER = g["DENPYONO"].ToString(),
                                                        })
                                                        .Select(s => new
                                                        {
                                                            DENPYOU_SHURUI_CD = s.Key.DENPYOU_SHURUI_CD,
                                                            DENPYOU_NUMBER = s.Key.DENPYOU_NUMBER,
                                                            KINGAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_KINGAKU"].ToString()))
                                                        }).ToList();

                            //伝票毎計算
                            foreach (var denpyou in groupDenpyouGotoRow)
                            {
                                DataRow infoRow = dataCheck.AsEnumerable().Where(w => w["DENPYOUSHURUI"].ToString() == denpyou.DENPYOU_SHURUI_CD &&
                                                                                      w["DENPYONO"].ToString() == denpyou.DENPYOU_NUMBER).FirstOrDefault();

                                int zeikubun = Convert.ToInt32(infoRow["SHIHARAI_ZEI_KBN_CD"].ToString());
                                decimal shouhizeiRate = decimal.Parse(infoRow["SHIHARAI_SHOUHIZEI_RATE"].ToString());

                                //金額
                                konkaiShiharaigaku += denpyou.KINGAKU;

                                //外税
                                if (zeikubun == 1)
                                {
                                    konkaiDensotozeigaku += CalTaxSoto(denpyou.KINGAKU, shouhizeiRate, taxHasuu);
                                }

                                //内税
                                if (zeikubun == 2)
                                {
                                    konkaiDenUtizeigaku += CalTaxUti(denpyou.KINGAKU, shouhizeiRate, taxHasuu);
                                }
                            }
                            #endregion

                            #region 明細毎・品名明細毎
                            //Sum 明細毎 Kingaku
                            //税計算区分：3.明細毎
                            //品名税区分: 未設定
                            var groupShiharaiMeisaiRow = dataCheck.AsEnumerable()
                                                        .Where(w =>
                                                            w["GYOUSHA_CD"].ToString() == item.GYOUSHA_CD.ToString() &&
                                                            w["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString() == CommonConst.ZEI_KEISAN_KBN_MEISAI.ToString() &&
                                                            ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()) == false)
                                                        .GroupBy(g => 1)
                                                        .Select(s => new
                                                        {
                                                            KINGAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_KINGAKU"].ToString())),
                                                            UCHIZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_UCHI"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_UCHI"].ToString())),
                                                            SOTOZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_SOTO"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_SOTO"].ToString())),
                                                        }).ToList();

                            //Zei by 明細
                            if (groupShiharaiMeisaiRow.Count > 0)
                            {
                                konkaiShiharaigaku += groupShiharaiMeisaiRow.FirstOrDefault().KINGAKU;
                                konkaiMeiUtizeigaku += groupShiharaiMeisaiRow.FirstOrDefault().UCHIZEI_GAKU;
                                konkaiMeisotozeigaku += groupShiharaiMeisaiRow.FirstOrDefault().SOTOZEI_GAKU;
                            }

                            //Sum 品名明細毎
                            //品名税区分: 設定済（外税・内税・非課税）
                            var groupHinmeiMeisaiRow = dataCheck.AsEnumerable()
                                                        .Where(w => w["GYOUSHA_CD"].ToString() == item.GYOUSHA_CD.ToString() &&
                                                                    ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()))
                                                        .GroupBy(g => 1)
                                                        .Select(s => new
                                                        {
                                                            KINGAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_KINGAKU"].ToString())),
                                                            UCHIZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_UCHI"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_UCHI"].ToString())),
                                                            SOTOZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_SOTO"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_SOTO"].ToString())),
                                                        }).ToList();

                            //Zei by 明細
                            if (groupHinmeiMeisaiRow.Count > 0)
                            {
                                konkaiShiharaigaku += groupHinmeiMeisaiRow.FirstOrDefault().KINGAKU;
                                konkaiMeiUtizeigaku += groupHinmeiMeisaiRow.FirstOrDefault().UCHIZEI_GAKU;
                                konkaiMeisotozeigaku += groupHinmeiMeisaiRow.FirstOrDefault().SOTOZEI_GAKU;
                            }
                            #endregion

                            #region 精算毎
                            // Data for 精算毎
                            //税計算区分：2.精算毎
                            //品名税区分: 未設定
                            var groupKeySeikyuuZei = seikyuuGotoRowList.Where(w => w["GYOUSHA_CD"].ToString() == item.GYOUSHA_CD.ToString())
                            .GroupBy(g => new
                            {
                                GYOUSHA_CD = g["GYOUSHA_CD"].ToString(),
                                SHIHARAI_ZEI_KBN_CD = g["SHIHARAI_ZEI_KBN_CD"].ToString(),
                                SHIHARAI_SHOUHIZEI_RATE = g["SHIHARAI_SHOUHIZEI_RATE"].ToString(),
                            }).Select(s => s.First()).ToList();

                            foreach (DataRow key in groupKeySeikyuuZei)
                            {
                                var listDataByKey = seikyuuGotoRowList.Where(w => w["GYOUSHA_CD"].ToString() == key["GYOUSHA_CD"].ToString() &&
                                                                                w["SHIHARAI_ZEI_KBN_CD"].ToString() == key["SHIHARAI_ZEI_KBN_CD"].ToString() &&
                                                                                w["SHIHARAI_SHOUHIZEI_RATE"].ToString() == key["SHIHARAI_SHOUHIZEI_RATE"].ToString()
                                                                            ).ToList();

                                decimal tmpKingaku = 0;
                                //今回支払額
                                tmpKingaku = listDataByKey.Sum(s => ConvertNullOrEmptyToZero(s["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(s["HINMEI_KINGAKU"].ToString()));

                                //業者別税計算区分別消費税率別の消費税
                                decimal tmpSeisotozei = 0;
                                decimal tmpSeiuchizei = 0;

                                CalcSeisanTax(listDataByKey, taxHasuu, ref tmpSeisotozei, ref tmpSeiuchizei);

                                //今回支払額
                                konkaiShiharaigaku += tmpKingaku;

                                konkaiSeisotozeigaku += tmpSeisotozei;
                                konkaiSeiUtizeigaku += tmpSeiuchizei;
                            }
                            #endregion

                            //今回支払額(税抜)
                            konkaiShiharaigaku = konkaiShiharaigaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);

                            kagami = new T_SEISAN_DENPYOU_KAGAMI();
                            kagami.TORIHIKISAKI_CD = dataCheck.Rows[0]["TORIHIKISAKI_CD"].ToString();
                            kagami.GYOUSHA_CD = item.GYOUSHA_CD;
                            kagami.GENBA_CD = string.Empty;
                            kagami.KONKAI_SHIHARAI_GAKU = konkaiShiharaigaku;
                            kagami.KONKAI_SEI_SOTOZEI_GAKU = konkaiSeisotozeigaku;
                            kagami.KONKAI_SEI_UTIZEI_GAKU = konkaiSeiUtizeigaku;
                            kagami.KONKAI_DEN_SOTOZEI_GAKU = konkaiDensotozeigaku;
                            kagami.KONKAI_DEN_UTIZEI_GAKU = konkaiDenUtizeigaku;
                            kagami.KONKAI_MEI_SOTOZEI_GAKU = konkaiMeisotozeigaku;
                            kagami.KONKAI_MEI_UTIZEI_GAKU = konkaiMeiUtizeigaku;

                            listKagami.Add(kagami);

                            //クリア
                            konkaiShiharaigaku = 0;
                            konkaiSeiUtizeigaku = 0;
                            konkaiSeisotozeigaku = 0;
                            konkaiDenUtizeigaku = 0;
                            konkaiDensotozeigaku = 0;
                            konkaiMeiUtizeigaku = 0;
                            konkaiMeisotozeigaku = 0;
                        }

                    }
                    break;
                //現場別：3
                case "3":
                    {
                        //現場別の鏡データ
                        var genbaList = dataCheck.AsEnumerable().GroupBy(g => new
                        {
                            GYOUSHA_CD = g["GYOUSHA_CD"].ToString(),
                            GENBA_CD = g["GENBA_CD"].ToString(),
                        }).Select(s => new
                        {
                            GYOUSHA_CD = s.Key.GYOUSHA_CD,
                            GENBA_CD = s.Key.GENBA_CD,
                        }).ToList();

                        foreach (var item in genbaList)
                        {
                            #region 伝票毎
                            //Data for 伝票毎
                            //税計算区分：1.伝票毎
                            //品名税区分: 未設定
                            var groupDenpyouGotoRow = dataCheck.AsEnumerable()
                                                        .Where(w => w["GYOUSHA_CD"].ToString() == item.GYOUSHA_CD &&
                                                                    w["GENBA_CD"].ToString() == item.GENBA_CD &&
                                                                    w["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString() == CommonConst.ZEI_KEISAN_KBN_DENPYOU.ToString() &&
                                                                    ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()) == false)
                                                        .GroupBy(g => new
                                                        {
                                                            DENPYOU_SHURUI_CD = g["DENPYOUSHURUI"].ToString(),
                                                            DENPYOU_NUMBER = g["DENPYONO"].ToString(),
                                                        })
                                                        .Select(s => new
                                                        {
                                                            DENPYOU_SHURUI_CD = s.Key.DENPYOU_SHURUI_CD,
                                                            DENPYOU_NUMBER = s.Key.DENPYOU_NUMBER,
                                                            KINGAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_KINGAKU"].ToString()))
                                                        }).ToList();

                            //伝票毎計算
                            foreach (var denpyou in groupDenpyouGotoRow)
                            {
                                DataRow infoRow = dataCheck.AsEnumerable().Where(w => w["DENPYOUSHURUI"].ToString() == denpyou.DENPYOU_SHURUI_CD &&
                                                                                      w["DENPYONO"].ToString() == denpyou.DENPYOU_NUMBER).FirstOrDefault();

                                int zeikubun = Convert.ToInt32(infoRow["SHIHARAI_ZEI_KBN_CD"].ToString());
                                decimal shouhizeiRate = decimal.Parse(infoRow["SHIHARAI_SHOUHIZEI_RATE"].ToString());

                                //金額
                                konkaiShiharaigaku += denpyou.KINGAKU;

                                //外税
                                if (zeikubun == 1)
                                {
                                    konkaiDensotozeigaku += CalTaxSoto(denpyou.KINGAKU, shouhizeiRate, taxHasuu);
                                }

                                //内税
                                if (zeikubun == 2)
                                {
                                    konkaiDenUtizeigaku += CalTaxUti(denpyou.KINGAKU, shouhizeiRate, taxHasuu);
                                }
                            }
                            #endregion

                            #region 明細毎・品名明細毎
                            //Data for 明細毎
                            //税計算区分：3.明細毎
                            //品名税区分: 未設定
                            var groupShiharaiMeisaiRow = dataCheck.AsEnumerable()
                                                        .Where(w =>
                                                            w["GYOUSHA_CD"].ToString() == item.GYOUSHA_CD.ToString() &&
                                                            w["GENBA_CD"].ToString() == item.GENBA_CD.ToString() &&
                                                            w["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString() == CommonConst.ZEI_KEISAN_KBN_MEISAI.ToString() &&
                                                            ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()) == false)
                                                        .GroupBy(g => 1)
                                                        .Select(s => new
                                                        {
                                                            KINGAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_KINGAKU"].ToString())),
                                                            UCHIZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_UCHI"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_UCHI"].ToString())),
                                                            SOTOZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_SOTO"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_SOTO"].ToString())),
                                                        }).ToList();

                            //Zei by 明細
                            if (groupShiharaiMeisaiRow.Count > 0)
                            {
                                konkaiShiharaigaku += groupShiharaiMeisaiRow.FirstOrDefault().KINGAKU;
                                konkaiMeiUtizeigaku += groupShiharaiMeisaiRow.FirstOrDefault().UCHIZEI_GAKU;
                                konkaiMeisotozeigaku += groupShiharaiMeisaiRow.FirstOrDefault().SOTOZEI_GAKU;
                            }

                            //Data for 品名明細毎
                            //品名税区分: 設定済（外税・内税・非課税）
                            var groupHinmeiMeisaiRow = dataCheck.AsEnumerable()
                                                        .Where(w =>
                                                            w["GYOUSHA_CD"].ToString() == item.GYOUSHA_CD.ToString() &&
                                                            w["GENBA_CD"].ToString() == item.GENBA_CD.ToString() &&
                                                            ZEI_KBN_LIST.Contains(w["HINMEI_ZEI_KBN_CD"].ToString()))
                                                        .GroupBy(g => 1)
                                                        .Select(s => new
                                                        {
                                                            KINGAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_KINGAKU"].ToString())),
                                                            UCHIZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_UCHI"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_UCHI"].ToString())),
                                                            SOTOZEI_GAKU = s.Sum(x => ConvertNullOrEmptyToZero(x["TAX_SOTO"].ToString()) + ConvertNullOrEmptyToZero(x["HINMEI_TAX_SOTO"].ToString())),
                                                        }).ToList();

                            //Zei by 明細
                            if (groupHinmeiMeisaiRow.Count > 0)
                            {
                                konkaiShiharaigaku += groupHinmeiMeisaiRow.FirstOrDefault().KINGAKU;
                                konkaiMeiUtizeigaku += groupHinmeiMeisaiRow.FirstOrDefault().UCHIZEI_GAKU;
                                konkaiMeisotozeigaku += groupHinmeiMeisaiRow.FirstOrDefault().SOTOZEI_GAKU;
                            }
                            #endregion

                            #region 精算毎
                            // Data for 精算毎
                            //税計算区分：2.精算毎
                            //品名税区分: 未設定
                            var groupKeySeikyuuZei = seikyuuGotoRowList.Where(w => w["GYOUSHA_CD"].ToString() == item.GYOUSHA_CD &&
                                                                                   w["GENBA_CD"].ToString() == item.GENBA_CD)
                            .GroupBy(g => new
                            {
                                GYOUSHA_CD = g["GYOUSHA_CD"].ToString(),
                                GENBA_CD = g["GENBA_CD"].ToString(),
                                SHIHARAI_ZEI_KBN_CD = g["SHIHARAI_ZEI_KBN_CD"].ToString(),
                                SHIHARAI_SHOUHIZEI_RATE = g["SHIHARAI_SHOUHIZEI_RATE"].ToString(),
                            }).Select(s => s.First()).ToList();

                            foreach (DataRow key in groupKeySeikyuuZei)
                            {
                                var listDataByKey = seikyuuGotoRowList.Where(w => w["GYOUSHA_CD"].ToString() == key["GYOUSHA_CD"].ToString() &&
                                                                                  w["GENBA_CD"].ToString() == key["GENBA_CD"].ToString() &&
                                                                                  w["SHIHARAI_ZEI_KBN_CD"].ToString() == key["SHIHARAI_ZEI_KBN_CD"].ToString() &&
                                                                                  w["SHIHARAI_SHOUHIZEI_RATE"].ToString() == key["SHIHARAI_SHOUHIZEI_RATE"].ToString()
                                                                            ).ToList();

                                decimal tmpKingaku = 0;
                                //今回支払額
                                tmpKingaku = listDataByKey.Sum(s => ConvertNullOrEmptyToZero(s["KINGAKU"].ToString()) + ConvertNullOrEmptyToZero(s["HINMEI_KINGAKU"].ToString()));

                                //現場別税計算区分別消費税率別の消費税
                                decimal tmpSeisotozei = 0;
                                decimal tmpSeiuchizei = 0;

                                CalcSeisanTax(listDataByKey, taxHasuu, ref tmpSeisotozei, ref tmpSeiuchizei);

                                //今回支払額
                                konkaiShiharaigaku += tmpKingaku;

                                konkaiSeisotozeigaku += tmpSeisotozei;
                                konkaiSeiUtizeigaku += tmpSeiuchizei;
                            }
                            #endregion

                            //今回支払額
                            konkaiShiharaigaku = konkaiShiharaigaku - (konkaiSeiUtizeigaku + konkaiDenUtizeigaku + konkaiMeiUtizeigaku);

                            kagami = new T_SEISAN_DENPYOU_KAGAMI();
                            kagami.TORIHIKISAKI_CD = dataCheck.Rows[0]["TORIHIKISAKI_CD"].ToString();
                            kagami.GYOUSHA_CD = item.GYOUSHA_CD;
                            kagami.GENBA_CD = item.GENBA_CD;
                            kagami.KONKAI_SHIHARAI_GAKU = konkaiShiharaigaku;
                            kagami.KONKAI_SEI_SOTOZEI_GAKU = konkaiSeisotozeigaku;
                            kagami.KONKAI_SEI_UTIZEI_GAKU = konkaiSeiUtizeigaku;
                            kagami.KONKAI_DEN_SOTOZEI_GAKU = konkaiDensotozeigaku;
                            kagami.KONKAI_DEN_UTIZEI_GAKU = konkaiDenUtizeigaku;
                            kagami.KONKAI_MEI_SOTOZEI_GAKU = konkaiMeisotozeigaku;
                            kagami.KONKAI_MEI_UTIZEI_GAKU = konkaiMeiUtizeigaku;

                            listKagami.Add(kagami);

                            //クリア
                            konkaiShiharaigaku = 0;
                            konkaiSeiUtizeigaku = 0;
                            konkaiSeisotozeigaku = 0;
                            konkaiDenUtizeigaku = 0;
                            konkaiDensotozeigaku = 0;
                            konkaiMeiUtizeigaku = 0;
                            konkaiMeisotozeigaku = 0;
                        }
                    }
                    break;
            }

            return listKagami;
        }

        /// <summary>
        /// オブジェクトをDecimal型に変換します
        /// </summary>
        /// <param name="value">対象のオブジェクト</param>
        /// <returns>NullかString.Emptyの場合、Decimal.Zeroを返します</returns>
        private static decimal ConvertNullOrEmptyToZero(object value)
        {
            decimal ret = Decimal.Zero;

            if (!String.IsNullOrEmpty(Convert.ToString(value)))
            {
                Decimal.TryParse(Convert.ToString(value), out ret);
            }

            return ret;
        }

        /// <summary>
        ///  請求毎の「外税、内税」を計算
        /// </summary>
        /// <param name="listDataByKey"></param>
        /// <param name="taxHasuu"></param>
        /// <param name="konkaiSeisotozeigaku"></param>
        /// <param name="konkaiSeiUtizeigaku"></param>
        private static void CalcSeikyuuTax(List<DataRow> listDataByKey, int taxHasuu, ref decimal konkaiSeisotozeigaku, ref decimal konkaiSeiUtizeigaku)
        {
            decimal kingakuTotal = 0;
            kingakuTotal = listDataByKey.Sum(s => ConvertNullOrEmptyToZero(s["KINGAKU"].ToString()));

            int zeikubun = Convert.ToInt32(listDataByKey[0]["URIAGE_ZEI_KBN_CD"].ToString());
            decimal shohizeirate = ConvertNullOrEmptyToZero(listDataByKey[0]["URIAGE_SHOUHIZEI_RATE"].ToString());

            //税区分が外税:1
            if (zeikubun == CommonConst.ZEI_KBN_SOTO)
            {
                //請求毎の消費税額算出
                konkaiSeisotozeigaku += CalTaxSoto(kingakuTotal, shohizeirate, taxHasuu);

            }
            //税区分が内税
            else if (zeikubun == CommonConst.ZEI_KBN_UCHI)
            {
                //請求毎の消費税額算出
                konkaiSeiUtizeigaku += CalTaxUti(kingakuTotal, shohizeirate, taxHasuu);
            }
        }

        /// <summary>
        ///  精算毎の「外税、内税」を計算
        /// </summary>
        /// <param name="listDataByKey"></param>
        /// <param name="taxHasuu"></param>
        /// <param name="konkaiSeisotozeigaku"></param>
        /// <param name="konkaiSeiUtizeigaku"></param>
        private static void CalcSeisanTax(List<DataRow> listDataByKey, int taxHasuu, ref decimal konkaiSeisotozeigaku, ref decimal konkaiSeiUtizeigaku)
        {
            decimal kingakuTotal = 0;
            kingakuTotal = listDataByKey.Sum(s => ConvertNullOrEmptyToZero(s["KINGAKU"].ToString()));

            int zeikubun = Convert.ToInt32(listDataByKey[0]["SHIHARAI_ZEI_KBN_CD"].ToString());
            decimal shohizeirate = ConvertNullOrEmptyToZero(listDataByKey[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString());

            //税区分が外税:1
            if (zeikubun == CommonConst.ZEI_KBN_SOTO)
            {
                //請求毎の消費税額算出
                konkaiSeisotozeigaku += CalTaxSoto(kingakuTotal, shohizeirate, taxHasuu);

            }
            //税区分が内税
            else if (zeikubun == CommonConst.ZEI_KBN_UCHI)
            {
                //請求毎の消費税額算出
                konkaiSeiUtizeigaku += CalTaxUti(kingakuTotal, shohizeirate, taxHasuu);
            }
        }

        /// <summary>
        /// 消費税額算出(外税)
        /// </summary>
        /// <param name="kingakuTotal">金額</param>
        /// <param name="shohizeirate">税率</param>
        /// <returns>算出消費税</returns>
        private static decimal CalTaxSoto(decimal kingakuTotal, decimal shohizeirate, int hasuuCD)
        {
            LogUtility.DebugMethodStart(kingakuTotal, shohizeirate, hasuuCD);
            decimal shohizei = 0;
            decimal sign = 1;
            if (kingakuTotal < 0)
            {
                // 金額が負の場合
                sign = -1;
            }

            if (hasuuCD == (short)CommonConst.TAX_HASUU_CD.CEILING)
            {
                //切り上げ
                shohizei = Math.Ceiling(Math.Abs(kingakuTotal) * shohizeirate) * sign;
            }
            else if (hasuuCD == (short)CommonConst.TAX_HASUU_CD.FLOOR)
            {
                //切り捨て
                shohizei = Math.Floor(Math.Abs(kingakuTotal) * shohizeirate) * sign;
            }
            else if (hasuuCD == (short)CommonConst.TAX_HASUU_CD.ROUND)
            {
                //四捨五入
                shohizei = Math.Round(Math.Abs(kingakuTotal * shohizeirate), 0, MidpointRounding.AwayFromZero) * sign;
            }
            LogUtility.DebugMethodEnd(kingakuTotal, shohizeirate, hasuuCD);

            return shohizei;
        }

        /// <summary>
        /// 消費税額算出(内税)
        /// </summary>
        /// <param name="kingakuTotal">金額</param>
        /// <param name="shohizeirate">税率</param>
        /// <returns>算出消費税</returns>
        private static decimal CalTaxUti(decimal kingakuTotal, decimal shohizeirate, int hasuuCD)
        {
            LogUtility.DebugMethodStart(kingakuTotal, shohizeirate, hasuuCD);

            decimal shohizei = 0;
            decimal sign = 1;
            if (kingakuTotal < 0)
            {
                // 金額が負の場合
                sign = -1;
            }

            if (hasuuCD == (short)CommonConst.TAX_HASUU_CD.CEILING)
            {
                //切り上げ
                shohizei = Math.Ceiling(Math.Abs(kingakuTotal) - (Math.Abs(kingakuTotal) / (1 + shohizeirate))) * sign;
            }
            else if (hasuuCD == (short)CommonConst.TAX_HASUU_CD.FLOOR)
            {
                //切り捨て
                shohizei = Math.Floor(Math.Abs(kingakuTotal) - (Math.Abs(kingakuTotal) / (1 + shohizeirate))) * sign;
            }
            else if (hasuuCD == (short)CommonConst.TAX_HASUU_CD.ROUND)
            {
                //四捨五入
                shohizei = Math.Round((Math.Abs(kingakuTotal) - (Math.Abs(kingakuTotal) / (1 + shohizeirate))), 0, MidpointRounding.AwayFromZero) * sign;
            }
            LogUtility.DebugMethodEnd(kingakuTotal, shohizeirate, hasuuCD);

            return shohizei;
        }

        #endregion
    }
}
