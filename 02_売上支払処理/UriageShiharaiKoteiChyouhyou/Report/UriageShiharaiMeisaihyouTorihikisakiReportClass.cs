// $Id:
using System;
using System.Data;
using System.Linq;
using CommonChouhyouPopup.App;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;

namespace Shougun.Core.SalesPayment.UriageShiharaiKoteiChouhyou
{
    /// <summary>
    /// 売上or支払明細表作成クラス（取引先CD順 or 取引先フリガナ順）
    /// </summary>
    internal class UriageShiharaiMeisaihyouTorihikisakiReportClass
    {
        /// <summary>
        /// システム設定エンティティ
        /// </summary>
        private M_SYS_INFO mSysInfo;

        /// <summary>
        /// 会社情報エンティティ
        /// </summary>
        private M_CORP_INFO mCorpInfo;

        /// <summary>
        /// 帳票テンプレートのパス
        /// </summary>
        private static readonly string OutputFormFullPathName = "./Template/R570_R573Torihikisaki-Form.xml";

        /// <summary>
        /// 帳票レイアウト名
        /// </summary>
        private static readonly string LAYOUT = "LAYOUT1";

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        internal UriageShiharaiMeisaihyouTorihikisakiReportClass()
        {
            LogUtility.DebugMethodStart();

            var mSysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.mSysInfo = mSysInfoDao.GetAllData().FirstOrDefault();

            var mCorpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            this.mCorpInfo = mCorpInfoDao.GetAllData().FirstOrDefault();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票を作成します
        /// </summary>
        /// <param name="dt">出力データ</param>
        /// <param name="dto">画面入力データ</param>
        internal void CreateReport(DataTable dt, UriageShiharaiMeisaihyouDtoClass dto)
        {
            LogUtility.DebugMethodStart(dt, dto);

            var chouhyouDataTable = this.EditChouhyouDataTable(dt, dto);

            // 現在表示されている一覧をレポート情報として生成
            ReportInfoBase reportInfo = new ReportInfoBase(chouhyouDataTable);
            reportInfo.Create(OutputFormFullPathName, LAYOUT, chouhyouDataTable);

            // グループの表示制御
            reportInfo.SetGroupVisible("TORIHIKISAKI", false, dto.IsGroupTorihikisaki);
            reportInfo.SetGroupVisible("TORIHIKISAKI_NAME", false, dto.IsGroupTorihikisaki);
            reportInfo.SetGroupVisible("GYOUSHA", false, dto.IsGroupGyousha);
            reportInfo.SetGroupVisible("GYOUSHA_NAME", false, dto.IsGroupGyousha);
            reportInfo.SetGroupVisible("GENBA", true, dto.IsGroupGenba);
            reportInfo.SetGroupVisible("GENBA_NAME", true, dto.IsGroupGenba);
            reportInfo.SetGroupVisible("DENPYOU_NUMBER", false, dto.IsGroupDenpyouNumber);

            // フィールドの表示制御
            if (dto.GyoushaCdFrom != dto.GyoushaCdTo)
            {
                reportInfo.SetFieldVisible("FH_GENBA_FLB", false);
                reportInfo.SetFieldVisible("FH_GENBA_VLB", false);
            }

            // 印刷ポップアップ表示
            FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo);

            reportPopup.ReportCaption = String.Empty;
            if (1 == dto.DenpyouKbnCd)
            {
                reportPopup.ReportCaption = ConstClass.URIAGE_MEISAIHYOU_TITLE;
            }
            else if (2 == dto.DenpyouKbnCd)
            {
                reportPopup.ReportCaption = ConstClass.SHIHARAI_MEISAIHYOU_TITLE;
            }

            if (1 == dto.Order)
            {
                reportPopup.ReportCaption += ConstClass.SORT_TORIHIKISAKI_CD_SUB_TITLE;
            }
            else if (2 == dto.Order)
            {
                reportPopup.ReportCaption += ConstClass.SORT_FURIGANA_SUB_TITLE;
            }

            reportPopup.ShowDialog();
            reportPopup.Dispose();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票に渡すデータを加工します
        /// </summary>
        /// <param name="dt">元となるデータ</param>
        /// <param name="dto">画面入力データ</param>
        /// <returns>帳票用に作成したデータ</returns>
        private DataTable EditChouhyouDataTable(DataTable dt, UriageShiharaiMeisaihyouDtoClass dto)
        {
            LogUtility.DebugMethodStart(dt, dto);

            DataTable ret = dt;

            // DBで取得できない項目のカラムを追加

            // 帳票タイトル
            dt.Columns.Add("FH_TITLE_VLB");
            // 自社名称
            dt.Columns.Add("FH_CORP_NAME_RYAKU_VLB");
            // 拠点名称
            dt.Columns.Add("FH_KYOTEN_NAME_RYAKU_VLB");
            // 出力日時
            dt.Columns.Add("FH_PRINT_DATE_VLB");
            // 伝票区分
            dt.Columns.Add("FH_DENPYOU_KBN_VLB");
            // 伝票種類
            dt.Columns.Add("FH_DENPYOU_SHURUI_VLB");
            // 日付
            dt.Columns.Add("FH_DATE_FLB");
            dt.Columns.Add("FH_DATE_VLB");
            // 入力担当者
            dt.Columns.Add("FH_TANTOUSHA_VLB");
            // 取引区分
            dt.Columns.Add("FH_TORIHIKI_KBN_VLB");
            // 締状況
            dt.Columns.Add("FH_SHIME_VLB");
            // 確定フラグ
            dt.Columns.Add("FH_KAKUTEI_FLG_VLB");
            // 取引先
            dt.Columns.Add("FH_TORIHIKISAKI_VLB");
            // 業者
            dt.Columns.Add("FH_GYOUSHA_VLB");
            // 現場
            dt.Columns.Add("FH_GENBA_VLB");
            // 売上or支払日付
            dt.Columns.Add("PH_URIAGE_SHIHARAI_DATE_VLB");
            // 書式設定後の正味
            dt.Columns.Add("FORMATTED_NET_JYUURYOU");
            // 書式設定後の数量
            dt.Columns.Add("FORMATTED_SUURYOU");
            // 書式設定後の単価
            dt.Columns.Add("FORMATTED_TANKA");
            // 書式設定後の金額
            dt.Columns.Add("FORMATTED_SUM_KINGAKU");
            // 書式設定後の税
            dt.Columns.Add("FORMATTED_ZEI");
            // 書式設定後の税込金額
            dt.Columns.Add("FORMATTED_ZEIKOMI_KINGAKU");
            // 伝票番号合計（正味）
            dt.Columns.Add("GF_DENPYOU_NUMBER_SHOUMI_VLB");
            // 伝票番号合計（金額）
            dt.Columns.Add("GF_DENPYOU_NUMBER_KINGAKU_VLB");
            // 伝票番号合計（税）
            dt.Columns.Add("GF_DENPYOU_NUMBER_ZEI_VLB");
            // 伝票番号合計（税込金額）
            dt.Columns.Add("GF_DENPYOU_NUMBER_ZEIKOMI_KINGAKU_VLB");
            // 現場合計（正味）
            dt.Columns.Add("GF_GENBA_SHOUMI_VLB");
            // 現場合計（金額）
            dt.Columns.Add("GF_GENBA_KINGAKU_VLB");
            // 現場合計（税）
            dt.Columns.Add("GF_GENBA_ZEI_VLB");
            // 現場合計（税込金額）
            dt.Columns.Add("GF_GENBA_ZEIKOMI_KINGAKU_VLB");
            // 業者合計（正味）
            dt.Columns.Add("GF_GYOUSHA_SHOUMI_VLB");
            // 業者合計（金額）
            dt.Columns.Add("GF_GYOUSHA_KINGAKU_VLB");
            // 業者合計（税）
            dt.Columns.Add("GF_GYOUSHA_ZEI_VLB");
            // 業者合計（税込金額）
            dt.Columns.Add("GF_GYOUSHA_ZEIKOMI_KINGAKU_VLB");
            // 取引先合計（正味）
            dt.Columns.Add("GF_TORIHIKISAKI_SHOUMI_VLB");
            // 取引先合計（金額）
            dt.Columns.Add("GF_TORIHIKISAKI_KINGAKU_VLB");
            // 取引先合計（税）
            dt.Columns.Add("GF_TORIHIKISAKI_ZEI_VLB");
            // 取引先合計（税込金額）
            dt.Columns.Add("GF_TORIHIKISAKI_ZEIKOMI_KINGAKU_VLB");
            // 総合計（正味）
            dt.Columns.Add("GF_ALL_SHOUMI_VLB");
            // 総合計（金額）
            dt.Columns.Add("GF_ALL_KINGAKU_VLB");
            // 総合計（税）
            dt.Columns.Add("GF_ALL_ZEI_VLB");
            // 総合計（税込金額）
            dt.Columns.Add("GF_ALL_ZEIKOMI_KINGAKU_VLB");

            // カラムを書き込み可にする
            dt.Columns.Cast<DataColumn>().ToList().ForEach(c => c.ReadOnly = false);

            // 業者CD,現場CDの最大桁数を広げる
            dt.Columns["GYOUSHA_CD"].MaxLength = 12;
            dt.Columns["GENBA_CD"].MaxLength = 18;

            // データ加工

            decimal denpyouNumberShoumi = 0;
            decimal denpyouNumberKingaku = 0;
            decimal denpyouNumberZei = 0;
            decimal denpyouNumberZeikomiKingaku = 0;
            decimal genbaShoumi = 0;
            decimal genbaKingaku = 0;
            decimal genbaZei = 0;
            decimal genbaZeikomiKingaku = 0;
            decimal gyoushaShoumi = 0;
            decimal gyoushaKingaku = 0;
            decimal gyoushaZei = 0;
            decimal gyoushaZeikomiKingaku = 0;
            decimal torihikisakiShoumi = 0;
            decimal torihikisakiKingaku = 0;
            decimal torihikisakiZei = 0;
            decimal torihikisakiZeikomiKingaku = 0;
            decimal shoumi = 0;
            decimal kingaku = 0;
            decimal zei = 0;
            decimal zeikomiKingaku = 0;

            string torihikisakiCd = ret.Rows[0]["TORIHIKISAKI_CD"].ToString();
            string torihikisakiName = ret.Rows[0]["TORIHIKISAKI_NAME"].ToString();
            string gyoushaCd = ret.Rows[0]["GYOUSHA_CD"].ToString();
            string genbaCd = ret.Rows[0]["GENBA_CD"].ToString();
            string gyoushaName = ret.Rows[0]["GYOUSHA_NAME"].ToString();
            string genbaName = ret.Rows[0]["GENBA_NAME"].ToString();
            string denpyouNumber = ret.Rows[0]["DENPYOU_NUMBER"].ToString();

            string hiduke = this.getDBDateTime().ToString("yyyy/MM/dd HH:mm");

            foreach (DataRow row in ret.Rows)
            {
                // タイトルと項目名の設定
                if (1 == dto.DenpyouKbnCd)
                {
                    row["FH_TITLE_VLB"] = ConstClass.URIAGE_MEISAIHYOU_TITLE;
                    row["PH_URIAGE_SHIHARAI_DATE_VLB"] = ConstClass.URIAGE_DATE_COLUMN_NAME;
                }
                else if (2 == dto.DenpyouKbnCd)
                {
                    row["FH_TITLE_VLB"] = ConstClass.SHIHARAI_MEISAIHYOU_TITLE;
                    row["PH_URIAGE_SHIHARAI_DATE_VLB"] = ConstClass.SHIHARAI_DATE_COLUMN_NAME;
                }

                // 並び順の指定でタイトル設定
                if (1 == dto.Order)
                {
                    row["FH_TITLE_VLB"] += ConstClass.SORT_TORIHIKISAKI_CD_SUB_TITLE;
                }
                else if (2 == dto.Order)
                {
                    row["FH_TITLE_VLB"] += ConstClass.SORT_FURIGANA_SUB_TITLE;
                }

                // 出力日付の書式設定
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //row["FH_PRINT_DATE_VLB"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm") + " 発行";

                // test
                //row["FH_PRINT_DATE_VLB"] = this.getDBDateTime().ToString("yyyy/MM/dd HH:mm") + " 発行";
                row["FH_PRINT_DATE_VLB"] = hiduke + " 発行";


                // 20151030 katen #12048 「システム日付」の基準作成、適用 end

                // 自社略称
                row["FH_CORP_NAME_RYAKU_VLB"] = this.mCorpInfo.CORP_RYAKU_NAME;

                // 拠点名称
                row["FH_KYOTEN_NAME_RYAKU_VLB"] = dto.KyotenName;

                // 伝票区分
                row["FH_DENPYOU_KBN_VLB"] = ConstClass.DenpyouKbn[dto.DenpyouKbnCd];

                // 伝票種類
                row["FH_DENPYOU_SHURUI_VLB"] = ConstClass.DenpyouShurui[dto.DenpyouShuruiCd];

                // 日付の文字列作成
                if (1 == dto.DateShuruiCd)
                {
                    row["FH_DATE_FLB"] = ConstClass.DATE_SHURUI_DENPYOU;
                    row["FH_DATE_VLB"] = dto.DateFrom + " ～ " + dto.DateTo;
                }
                else if (2 == dto.DateShuruiCd)
                {
                    if (1 == dto.DenpyouKbnCd)
                    {
                        row["FH_DATE_FLB"] = ConstClass.DATE_SHURUI_URIAGE;
                        row["FH_DATE_VLB"] = dto.DateFrom + " ～ " + dto.DateTo;
                    }
                    else if (2 == dto.DenpyouKbnCd)
                    {
                        row["FH_DATE_FLB"] = ConstClass.DATE_SHURUI_SHIHARAI;
                        row["FH_DATE_VLB"] = dto.DateFrom + " ～ " + dto.DateTo;
                    }
                }
                else if (3 == dto.DateShuruiCd)
                {
                    row["FH_DATE_FLB"] = ConstClass.DATE_SHURUI_INPUT;
                    row["FH_DATE_VLB"] = dto.DateFrom + " ～ " + dto.DateTo;
                }

                // 入力担当者
                if (String.IsNullOrEmpty(dto.NyuuryokuTantousyaName))
                {
                    row["FH_TANTOUSHA_VLB"] = ConstClass.ALL;
                }
                else
                {
                    row["FH_TANTOUSHA_VLB"] = dto.NyuuryokuTantousyaName;
                }

                // 取引区分
                row["FH_TORIHIKI_KBN_VLB"] = ConstClass.TorihikiKbn[dto.TorihikiKbnCd];

                // 締状況
                row["FH_SHIME_VLB"] = ConstClass.ShimeJoukyou[dto.ShimeJoukyouCd];

                // 確定フラグ
                row["FH_KAKUTEI_FLG_VLB"] = ConstClass.KakuteiKbn[dto.KakuteiKbnCd];

                // 取引先の文字列作成
                if (!String.IsNullOrEmpty(dto.TorihikisakiFrom) || !String.IsNullOrEmpty(dto.TorihikisakiTo))
                {
                    var from = String.Empty;
                    var to = String.Empty;
                    if (!String.IsNullOrEmpty(dto.TorihikisakiFrom))
                    {
                        from = dto.TorihikisakiCdFrom + " " + dto.TorihikisakiFrom;
                    }
                    if (!String.IsNullOrEmpty(dto.TorihikisakiTo))
                    {
                        to = dto.TorihikisakiCdTo + " " + dto.TorihikisakiTo;
                    }
                    row["FH_TORIHIKISAKI_VLB"] = from + " ～ " + "\n" + to;
                }

                // 業者の文字列作成
                if (!String.IsNullOrEmpty(dto.GyoushaFrom) || !String.IsNullOrEmpty(dto.GyoushaTo))
                {
                    var from = String.Empty;
                    var to = String.Empty;
                    if (!String.IsNullOrEmpty(dto.GyoushaFrom))
                    {
                        from = dto.GyoushaCdFrom + " " + dto.GyoushaFrom;
                    }
                    if (!String.IsNullOrEmpty(dto.GyoushaTo))
                    {
                        to = dto.GyoushaCdTo + " " + dto.GyoushaTo;
                    }
                    row["FH_GYOUSHA_VLB"] = from + " ～ " + "\n" + to;
                }

                // 現場の文字列作成
                if (String.IsNullOrEmpty(dto.GenbaFrom) && String.IsNullOrEmpty(dto.GenbaTo))
                {
                    row["FH_GENBA_VLB"] = ConstClass.ALL;
                }
                if (!String.IsNullOrEmpty(dto.GenbaFrom) || !String.IsNullOrEmpty(dto.GenbaTo))
                {
                    var from = String.Empty;
                    var to = String.Empty;
                    if (!String.IsNullOrEmpty(dto.GenbaFrom))
                    {
                        from = dto.GenbaCdFrom + " " + dto.GenbaFrom;
                    }
                    if (!String.IsNullOrEmpty(dto.GenbaTo))
                    {
                        to = dto.GenbaCdTo + " " + dto.GenbaTo;
                    }
                    row["FH_GENBA_VLB"] = from + " ～ " + "\n" + to;
                }

                // 伝票日付
                row["DENPYOU_DATE"] = Convert.ToDateTime(row["DENPYOU_DATE"]).ToString("yyyy/MM/dd");

                // 売上or支払日付
                if (DBNull.Value != row["URIAGE_SHIHARAI_DATE"])
                {
                    row["URIAGE_SHIHARAI_DATE"] = Convert.ToDateTime(row["URIAGE_SHIHARAI_DATE"]).ToString("yyyy/MM/dd");
                }

                // 税（明細税のときだけ表示）
                row["FORMATTED_ZEI"] = String.Empty;
                if ("3" == row["ZEI_KEISAN_KBN_CD"].ToString() || !String.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString()))
                {
                    row["FORMATTED_ZEI"] = ReportInfo.ConvertNullOrEmptyToZero(row["MEISAI_TAX"]);
                }

                // 税込金額
                row["FORMATTED_ZEIKOMI_KINGAKU"] = ReportInfo.ConvertNullOrEmptyToZero(row["ZEINUKI_KINGAKU"]) + ReportInfo.ConvertNullOrEmptyToZero(row["MEISAI_TAX"]);

                // 集計（全体）
                shoumi += ReportInfo.ConvertNullOrEmptyToZero(row["NET_JYUURYOU"]);
                kingaku += ReportInfo.ConvertNullOrEmptyToZero(row["ZEINUKI_KINGAKU"]);
                zei += ReportInfo.ConvertNullOrEmptyToZero(row["DENPYOU_TAX"]);
                zeikomiKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["FORMATTED_ZEIKOMI_KINGAKU"]);

                row["GF_ALL_SHOUMI_VLB"] = shoumi;
                row["GF_ALL_KINGAKU_VLB"] = kingaku;
                row["GF_ALL_ZEI_VLB"] = zei;
                row["GF_ALL_ZEIKOMI_KINGAKU_VLB"] = kingaku + zei;

                // 集計（伝票番号）
                if (denpyouNumber != row["DENPYOU_NUMBER"].ToString())
                {
                    denpyouNumberShoumi = 0;
                    denpyouNumberKingaku = 0;
                    denpyouNumberZei = 0;
                    denpyouNumberZeikomiKingaku = 0;
                    denpyouNumber = row["DENPYOU_NUMBER"].ToString();
                }
                denpyouNumberShoumi += ReportInfo.ConvertNullOrEmptyToZero(row["NET_JYUURYOU"]);
                denpyouNumberKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["ZEINUKI_KINGAKU"]);
                denpyouNumberZei += ReportInfo.ConvertNullOrEmptyToZero(row["DENPYOU_TAX"]);
                denpyouNumberZeikomiKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["FORMATTED_ZEIKOMI_KINGAKU"]);

                row["GF_DENPYOU_NUMBER_SHOUMI_VLB"] = denpyouNumberShoumi;
                row["GF_DENPYOU_NUMBER_KINGAKU_VLB"] = denpyouNumberKingaku;
                row["GF_DENPYOU_NUMBER_ZEI_VLB"] = denpyouNumberZei;
                row["GF_DENPYOU_NUMBER_ZEIKOMI_KINGAKU_VLB"] = denpyouNumberKingaku + denpyouNumberZei;

                // 集計（現場）
                if ((genbaCd != row["GENBA_CD"].ToString() || genbaName != row["GENBA_NAME"].ToString())
                    || (gyoushaCd != row["GYOUSHA_CD"].ToString() || gyoushaName != row["GYOUSHA_NAME"].ToString())
                    || (torihikisakiCd != row["TORIHIKISAKI_CD"].ToString() || torihikisakiName != row["TORIHIKISAKI_NAME"].ToString()))
                {
                    genbaShoumi = 0;
                    genbaKingaku = 0;
                    genbaZei = 0;
                    genbaZeikomiKingaku = 0;
                    genbaCd = row["GENBA_CD"].ToString();
                    genbaName = row["GENBA_NAME"].ToString();
                }
                genbaShoumi += ReportInfo.ConvertNullOrEmptyToZero(row["NET_JYUURYOU"]);
                genbaKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["ZEINUKI_KINGAKU"]);
                genbaZei += ReportInfo.ConvertNullOrEmptyToZero(row["DENPYOU_TAX"]);
                genbaZeikomiKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["FORMATTED_ZEIKOMI_KINGAKU"]);

                row["GF_GENBA_SHOUMI_VLB"] = genbaShoumi;
                row["GF_GENBA_KINGAKU_VLB"] = genbaKingaku;
                row["GF_GENBA_ZEI_VLB"] = genbaZei;
                row["GF_GENBA_ZEIKOMI_KINGAKU_VLB"] = genbaKingaku + genbaZei;

                // 集計（業者）
                if ((torihikisakiCd != row["TORIHIKISAKI_CD"].ToString() || torihikisakiName != row["TORIHIKISAKI_NAME"].ToString())
                    || (gyoushaCd != row["GYOUSHA_CD"].ToString() || gyoushaName != row["GYOUSHA_NAME"].ToString()))
                {
                    gyoushaShoumi = 0;
                    gyoushaKingaku = 0;
                    gyoushaZei = 0;
                    gyoushaZeikomiKingaku = 0;
                    gyoushaCd = row["GYOUSHA_CD"].ToString();
                    gyoushaName = row["GYOUSHA_NAME"].ToString();
                }
                gyoushaShoumi += ReportInfo.ConvertNullOrEmptyToZero(row["NET_JYUURYOU"]);
                gyoushaKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["ZEINUKI_KINGAKU"]);
                gyoushaZei += ReportInfo.ConvertNullOrEmptyToZero(row["DENPYOU_TAX"]);
                gyoushaZeikomiKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["FORMATTED_ZEIKOMI_KINGAKU"]);

                row["GF_GYOUSHA_SHOUMI_VLB"] = gyoushaShoumi;
                row["GF_GYOUSHA_KINGAKU_VLB"] = gyoushaKingaku;
                row["GF_GYOUSHA_ZEI_VLB"] = gyoushaZei;
                row["GF_GYOUSHA_ZEIKOMI_KINGAKU_VLB"] = gyoushaKingaku + gyoushaZei;

                // 集計（取引先）
                if (torihikisakiCd != row["TORIHIKISAKI_CD"].ToString() || torihikisakiName != row["TORIHIKISAKI_NAME"].ToString())
                {
                    torihikisakiShoumi = 0;
                    torihikisakiKingaku = 0;
                    torihikisakiZei = 0;
                    torihikisakiZeikomiKingaku = 0;
                    torihikisakiCd = row["TORIHIKISAKI_CD"].ToString();
                    torihikisakiName = row["TORIHIKISAKI_NAME"].ToString();
                }
                torihikisakiShoumi += ReportInfo.ConvertNullOrEmptyToZero(row["NET_JYUURYOU"]);
                torihikisakiKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["ZEINUKI_KINGAKU"]);  // 集計には税抜金額を使用する
                torihikisakiZei += ReportInfo.ConvertNullOrEmptyToZero(row["DENPYOU_TAX"]);
                torihikisakiZeikomiKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["FORMATTED_ZEIKOMI_KINGAKU"]);

                row["GF_TORIHIKISAKI_SHOUMI_VLB"] = torihikisakiShoumi;
                row["GF_TORIHIKISAKI_KINGAKU_VLB"] = torihikisakiKingaku;
                row["GF_TORIHIKISAKI_ZEI_VLB"] = torihikisakiZei;
                row["GF_TORIHIKISAKI_ZEIKOMI_KINGAKU_VLB"] = torihikisakiKingaku + torihikisakiZei;

                // 正味の列を書式設定
                row["FORMATTED_NET_JYUURYOU"] = Convert.ToDecimal(row["NET_JYUURYOU"]).ToString(mSysInfo.SYS_JYURYOU_FORMAT);
                row["GF_TORIHIKISAKI_SHOUMI_VLB"] = Convert.ToDecimal(row["GF_TORIHIKISAKI_SHOUMI_VLB"]).ToString(mSysInfo.SYS_JYURYOU_FORMAT);
                row["GF_GYOUSHA_SHOUMI_VLB"] = Convert.ToDecimal(row["GF_GYOUSHA_SHOUMI_VLB"]).ToString(mSysInfo.SYS_JYURYOU_FORMAT);
                row["GF_GENBA_SHOUMI_VLB"] = Convert.ToDecimal(row["GF_GENBA_SHOUMI_VLB"]).ToString(mSysInfo.SYS_JYURYOU_FORMAT);
                row["GF_DENPYOU_NUMBER_SHOUMI_VLB"] = Convert.ToDecimal(row["GF_DENPYOU_NUMBER_SHOUMI_VLB"]).ToString(mSysInfo.SYS_JYURYOU_FORMAT);
                row["GF_ALL_SHOUMI_VLB"] = Convert.ToDecimal(row["GF_ALL_SHOUMI_VLB"]).ToString(mSysInfo.SYS_JYURYOU_FORMAT);

                // 数量の列を書式設定
                row["FORMATTED_SUURYOU"] = Convert.ToDecimal(row["SUURYOU"]).ToString(mSysInfo.SYS_SUURYOU_FORMAT);

                // 単価の列を書式設定
                if (string.IsNullOrEmpty(row["TANKA"].ToString()))
                {
                    row["FORMATTED_TANKA"] = null;
                }
                else
                {
                    row["FORMATTED_TANKA"] = Convert.ToDecimal(row["TANKA"]).ToString(mSysInfo.SYS_TANKA_FORMAT);
                }

                // 金額の列を書式設定
                row["FORMATTED_SUM_KINGAKU"] = Convert.ToDecimal(row["ZEINUKI_KINGAKU"]).ToString("#,##0");
                row["GF_TORIHIKISAKI_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_TORIHIKISAKI_KINGAKU_VLB"]).ToString("#,##0");
                row["GF_GYOUSHA_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_GYOUSHA_KINGAKU_VLB"]).ToString("#,##0");
                row["GF_GENBA_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_GENBA_KINGAKU_VLB"]).ToString("#,##0");
                row["GF_DENPYOU_NUMBER_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_DENPYOU_NUMBER_KINGAKU_VLB"]).ToString("#,##0");
                row["GF_ALL_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_ALL_KINGAKU_VLB"]).ToString("#,##0");

                // 消費税の列を書式設定
                if (!String.IsNullOrEmpty(row["FORMATTED_ZEI"].ToString()))
                {
                    if (row["UCHIZEI"] != null && !String.IsNullOrEmpty(row["UCHIZEI"].ToString()))
                    {
                        row["FORMATTED_ZEI"] = Convert.ToDecimal(row["FORMATTED_ZEI"]).ToString("(#,##0)");
                    }
                    else
                    {
                        row["FORMATTED_ZEI"] = Convert.ToDecimal(row["FORMATTED_ZEI"]).ToString("#,##0");
                    }
                }
                row["GF_TORIHIKISAKI_ZEI_VLB"] = Convert.ToDecimal(row["GF_TORIHIKISAKI_ZEI_VLB"].ToString()).ToString("#,##0");
                row["GF_GYOUSHA_ZEI_VLB"] = Convert.ToDecimal(row["GF_GYOUSHA_ZEI_VLB"].ToString()).ToString("#,##0");
                row["GF_GENBA_ZEI_VLB"] = Convert.ToDecimal(row["GF_GENBA_ZEI_VLB"].ToString()).ToString("#,##0");
                row["GF_DENPYOU_NUMBER_ZEI_VLB"] = Convert.ToDecimal(row["GF_DENPYOU_NUMBER_ZEI_VLB"].ToString()).ToString("#,##0");
                row["GF_ALL_ZEI_VLB"] = Convert.ToDecimal(row["GF_ALL_ZEI_VLB"].ToString()).ToString("#,##0");

                // 税込金額の列を書式設定
                row["FORMATTED_ZEIKOMI_KINGAKU"] = Convert.ToDecimal(row["FORMATTED_ZEIKOMI_KINGAKU"]).ToString("#,##0");
                row["GF_TORIHIKISAKI_ZEIKOMI_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_TORIHIKISAKI_ZEIKOMI_KINGAKU_VLB"]).ToString("#,##0");
                row["GF_GYOUSHA_ZEIKOMI_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_GYOUSHA_ZEIKOMI_KINGAKU_VLB"]).ToString("#,##0");
                row["GF_GENBA_ZEIKOMI_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_GENBA_ZEIKOMI_KINGAKU_VLB"]).ToString("#,##0");
                row["GF_DENPYOU_NUMBER_ZEIKOMI_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_DENPYOU_NUMBER_ZEIKOMI_KINGAKU_VLB"]).ToString("#,##0");
                row["GF_ALL_ZEIKOMI_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_ALL_ZEIKOMI_KINGAKU_VLB"]).ToString("#,##0");

                // 集計ヘッダ表示用に現場CDを編集
                row["GENBA_CD"] = String.Format("{0,6}", row["TORIHIKISAKI_CD"]) + String.Format("{0,6}", row["GYOUSHA_CD"]) + String.Format("{0,6}", row["GENBA_CD"]);
                row["GYOUSHA_CD"] = String.Format("{0,6}", row["TORIHIKISAKI_CD"]) + String.Format("{0,6}", row["GYOUSHA_CD"]);
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            System.Data.DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
    }
}
